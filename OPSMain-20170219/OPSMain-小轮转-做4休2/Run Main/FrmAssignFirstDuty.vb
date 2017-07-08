Public Class FrmAssignFirstDuty

    Public CmbDuty As New ComboBox
    Public CmbDutySort As New ComboBox
    Public PreAMCon As AMDutyConnect
    Public NextAMCon As AMDutyConnect            '次日的夜早班联合
    Public CurLinename As String
    Public PreDayTimeTable As Coordination2.CSTimeTable
    Public FirDayTimeTable As Coordination2.CSTimeTable
    Public FirDayDate As Date
    Public AreaYunzhuanS As List(Of AreaYunZhuan)
    Public DutyDGVS As New List(Of DataGridView)
    Public CurrentCell As DataGridViewCell
    Public CSTimeTable As Coordination2.CSTimeTable
    Public lunzhuansets As List(Of Dictionary(Of Integer, String))   '轮转班制集合
    Public lunzhuanset As New Dictionary(Of Integer, String)         '轮转班制
    Public arealunzhuan As New Dictionary(Of String, Dictionary(Of Integer, String))   '区域与轮转班制对应关系
    Public lunzhuanbanzhongs As New List(Of String)
    Public CurLine As Coordination2.Line                              '当前线路
    Public cstimetableNameList() As String                              '乘务计划ID数组

    Private Property CurMousex As Integer
    Private Property CurMousey As Integer
    Private Property CurCellx As Integer
    Private Property CurCelly As Integer

    Private Sub Btn_Cancle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancle.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub FrmAssignFirstDuty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandler CmbDuty.TextChanged, AddressOf CmbDutyValueChanged
        AddHandler CmbDutySort.TextChanged, AddressOf CmbDutySortValueChanged
        For Each dgv As DataGridView In DutyDGVS
            AddHandler dgv.CellClick, AddressOf DGV_AssignFirst_CellClick
            AddHandler dgv.CellMouseDown, AddressOf DGV_AssignFirst_CellMouseDown
            AddHandler dgv.MouseDown, AddressOf DGV_AssignFirst_MouseDown
        Next

        Call LoadPreDayDutys()


    End Sub
    ''' <summary>
    ''' 加载未安排任务至CmbDuty
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub RefreshCmbItems()
        CmbDuty.Items.Clear()
        Me.CmbDuty.Items.Add("早班/无任务")
        Me.CmbDuty.Items.Add("白班/无任务")
        Me.CmbDuty.Items.Add("夜班/无任务")
        Me.CmbDuty.Items.Add("日勤班/无任务")
        Me.CmbDuty.Items.Add("休息/无任务")
        If FirDayTimeTable IsNot Nothing Then
            For Each dri As Coordination2.CSDriver In FirDayTimeTable.MCSDrivers
                If dri.BelongArea = CType(Me.CurrentCell.Tag, DataGridView).Parent.Text Then
                    Dim IFEXIT As Boolean = False
                    For Each row As DataGridViewRow In CType(Me.CurrentCell.Tag, DataGridView).Rows
                        If row.Cells("首日任务").Value IsNot Nothing AndAlso row.Cells("首日任务").Value.ToString = dri.DutySort & "/" & dri.OutPutCSDriverNo & "(" & dri.StartStaName & ")" Then
                            IFEXIT = True
                            Exit For
                        End If
                    Next
                    If IFEXIT = False Then
                        Me.CmbDuty.Items.Add(dri.DutySort & "/" & dri.OutPutCSDriverNo & "(" & dri.StartStaName & ")")
                    End If
                End If
            Next
            For Each dri As Coordination2.CSDriver In FirDayTimeTable.NCSDrivers
                If dri.BelongArea = CType(Me.CurrentCell.Tag, DataGridView).Parent.Text Then
                    Dim IFEXIT As Boolean = False
                    For Each row As DataGridViewRow In CType(Me.CurrentCell.Tag, DataGridView).Rows
                        If row.Cells("首日任务").Value IsNot Nothing AndAlso row.Cells("首日任务").Value.ToString = dri.DutySort & "/" & dri.OutPutCSDriverNo Then
                            IFEXIT = True
                            Exit For
                        End If
                    Next
                    If IFEXIT = False Then
                        Me.CmbDuty.Items.Add(dri.DutySort & "/" & dri.OutPutCSDriverNo)
                    End If
                End If
            Next
            For Each dri As Coordination2.CSDriver In FirDayTimeTable.ACSDrivers
                If dri.BelongArea = CType(Me.CurrentCell.Tag, DataGridView).Parent.Text Then
                    Dim IFEXIT As Boolean = False
                    For Each row As DataGridViewRow In CType(Me.CurrentCell.Tag, DataGridView).Rows
                        If row.Cells("首日任务").Value IsNot Nothing AndAlso row.Cells("首日任务").Value.ToString = dri.DutySort & "/" & dri.OutPutCSDriverNo Then
                            IFEXIT = True
                            Exit For
                        End If
                    Next
                    If IFEXIT = False Then
                        Me.CmbDuty.Items.Add(dri.DutySort & "/" & dri.OutPutCSDriverNo)
                    End If
                End If
            Next
            For Each dri As Coordination2.CSDriver In FirDayTimeTable.CCSDrivers
                If dri.BelongArea = CType(Me.CurrentCell.Tag, DataGridView).Parent.Text Then
                    Dim IFEXIT As Boolean = False
                    For Each row As DataGridViewRow In CType(Me.CurrentCell.Tag, DataGridView).Rows
                        If row.Cells("首日任务").Value IsNot Nothing AndAlso row.Cells("首日任务").Value.ToString.Split("/")(1) = dri.OutPutCSDriverNo Then
                            IFEXIT = True
                            Exit For
                        End If
                    Next
                    If IFEXIT = False Then
                        Me.CmbDuty.Items.Add(dri.DutySort & "/" & dri.OutPutCSDriverNo)
                    End If
                End If
            Next
        End If
    End Sub

    Public Sub LoadPreDayDutys()          '加载前一天任务
        Dim TempTeamList As New List(Of CrewTrainingManager.DriverTeam)
        If Me.PreDayTimeTable IsNot Nothing Then
            Dim str As String = "select t.*,m.beclass,m.beteam,m.drivername,m.bezone from cs_corresponding t,cs_driverinf m " & _
            "where t.lineid=m.lineid and t.rdriverno=m.rdriverno and t.lineid='" & Me.CurLinename & _
            "' and datediff('d',t.dateno ,Format('" & Me.FirDayDate.Date.AddDays(-1).ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))=0 order by t.dateno,m.beclass,m.beteam,t.rdriverno"
            Dim tab As DataTable = Globle.Method.ReadDataForAccess(str)
            If tab IsNot Nothing Then
                For Each row As DataRow In tab.Rows
                    Dim classno As String = row.Item("beclass").ToString.Trim
                    Dim teamno As String = row.Item("beteam").ToString.Trim
                    Dim rdriverno As String = row.Item("rdriverno").ToString.Trim
                    Dim dateno As Date = CDate(row.Item("dateno")).Date
                    Dim driverno As String = row.Item("driverno").ToString.Trim
                    Dim dutysort As String = row.Item("dutysort").ToString.Trim
                    Dim BelongArea As String = row.Item("Bezone").ToString.Trim
                    Dim ForDutySort As String = row.Item("ForDutySort").ToString.Trim
                    Dim tempCSDri As Coordination2.CSDriver = GetCSDriverFromTimetable(driverno, dutysort, Me.PreDayTimeTable)
                    For Each dgv As DataGridView In DutyDGVS
                        For Each gridrow As DataGridViewRow In dgv.Rows
                            If gridrow.Cells("组号").Value.ToString.Trim = teamno Then
                                If dutysort = "夜班" Then
                                    If tempCSDri IsNot Nothing Then
                                        gridrow.Cells("前日任务").Value = dutysort & "/" & tempCSDri.OutPutCSDriverNo & "(" & tempCSDri.OffStaName & ")"
                                    Else
                                        If tempCSDri IsNot Nothing Then
                                            gridrow.Cells("前日任务").Value = dutysort & "/" & tempCSDri.OutPutCSDriverNo & "()"
                                        Else
                                            gridrow.Cells("前日任务").Value = dutysort & "/" & driverno & "()"
                                        End If
                                    End If
                                Else
                                    If tempCSDri IsNot Nothing Then
                                        gridrow.Cells("前日任务").Value = dutysort & "/" & tempCSDri.OutPutCSDriverNo
                                    Else
                                        gridrow.Cells("前日任务").Value = dutysort & "/" & driverno
                                    End If
                                End If
                                gridrow.Cells("前日任务").Tag = ForDutySort
                                gridrow.Cells("前日班种").Value = ForDutySort
                                GoTo L
                            End If
                        Next
                    Next
L:
                Next
                tab.Dispose()
            End If
        End If
    End Sub

    Public Sub SetFirDayDutys()
        For Each dgv As DataGridView In DutyDGVS
            Dim AreaName As String = dgv.Parent.Text
            Dim YunZhuanPara As String = ""
            For Each area As AreaYunZhuan In AreaYunzhuanS
                If area.AreaName = AreaName Then
                    YunZhuanPara = area.YunZhuanPara
                End If
            Next
            Dim MDrivers As New List(Of Coordination2.CSDriver)
            Dim NDrivers As New List(Of Coordination2.CSDriver)
            Dim CDrivers As New List(Of Coordination2.CSDriver)
            Dim ADrivers As New List(Of Coordination2.CSDriver)
            For Each Dri As Coordination2.CSDriver In FirDayTimeTable.MCSDrivers
                If Dri.BelongArea = AreaName Then
                    MDrivers.Add(Dri)
                End If
            Next
            For Each Dri As Coordination2.CSDriver In FirDayTimeTable.NCSDrivers
                If Dri.BelongArea = AreaName Then
                    NDrivers.Add(Dri)
                End If
            Next
            For Each Dri As Coordination2.CSDriver In FirDayTimeTable.CCSDrivers
                If Dri.BelongArea = AreaName Then
                    CDrivers.Add(Dri)
                End If
            Next
            For Each Dri As Coordination2.CSDriver In FirDayTimeTable.ACSDrivers
                If Dri.BelongArea = AreaName Then
                    ADrivers.Add(Dri)
                End If
            Next
            For Each row As DataGridViewRow In dgv.Rows

                Dim cellStr As String = row.Cells("前日任务").Value.ToString.Trim        '根据前日任务判断首日任务
                If cellStr = "" Then
                    Continue For
                End If
                Dim Dutysort As String = cellStr.Split("/")(0)
                Dim ForDutySort As String = row.Cells("前日班种").Value.ToString
                Dim DriverNo As String = ""
                If Dutysort = "夜班" Then
                    DriverNo = cellStr.Substring(cellStr.IndexOf("/") + 1, cellStr.IndexOf("(") - cellStr.IndexOf("/") - 1)
                Else
                    DriverNo = cellStr.Split("/")(1)
                End If
                Dim decidemode As Integer = 0
                If AreaName = "主区域" Or arealunzhuan.Keys.Contains(AreaName) = False Then      '没有定制轮转匹配的区域按主区域处理
                    decidemode = 1
                Else
                    decidemode = 2
                End If
            Select decidemode
                        Case 1
                            If YunZhuanPara = "四班两转" Then
                                Select Case ForDutySort
                                    Case "早班"
                                        row.Cells("首日任务").Value = "休息/无任务"
                                        row.Cells("首日任务").Tag = "休息"
                                        row.Cells("首日班种").Value = "休息"
                                    Case "白班"
                                        'If ADrivers.Count > 0 Then
                                        '    row.Cells("首日任务").Value = ADrivers(0).DutySort & "/" & ADrivers(0).OutPutCSDriverNo
                                        '    ADrivers.RemoveAt(0)
                                        'Else
                                        row.Cells("首日任务").Value = "夜班/无任务"
                                        'End If
                                        row.Cells("首日任务").Tag = "夜班"
                                        row.Cells("首日班种").Value = "夜班"
                                    Case "夜班"
                                        Dim iFAssigned As Boolean = False
                                        If PreAMCon IsNot Nothing Then
                                            For Each amdri As AMDriver In PreAMCon.AMDrivers
                                                If amdri.ADriver IsNot Nothing AndAlso amdri.ADriver.OutPutCSDriverNo = DriverNo Then
                                                    If amdri.MDriver IsNot Nothing Then
                                                        row.Cells("首日任务").Value = amdri.MDriver.DutySort & "/" & amdri.MDriver.OutPutCSDriverNo & "(" & amdri.MDriver.StartStaName & ")"
                                                        MDrivers.Remove(amdri.MDriver)
                                                        iFAssigned = True
                                                        Exit For
                                                    End If
                                                End If
                                            Next
                                        End If
                                        If iFAssigned = False Then
                                            row.Cells("首日任务").Value = "早班/SP()"
                                        End If
                                        row.Cells("首日任务").Tag = "早班"
                                        row.Cells("首日班种").Value = "早班"
                                    Case "休息"
                                        'If NDrivers.Count > 0 Then
                                        '    row.Cells("首日任务").Value = NDrivers(0).DutySort & "/" & NDrivers(0).OutPutCSDriverNo
                                        '    NDrivers.RemoveAt(0)
                                        'Else
                                        row.Cells("首日任务").Value = "白班/无任务"
                                        'End If
                                        row.Cells("首日任务").Tag = "白班"
                                        row.Cells("首日班种").Value = "白班"
                                End Select
                            ElseIf YunZhuanPara = "五班三转" Then '上海白夜早峰休
                                Select Case ForDutySort
                                    Case "早班"
                                        row.Cells("首日任务").Value = "日勤班/无任务"
                                        row.Cells("首日任务").Tag = "日勤班"
                                        row.Cells("首日班种").Value = "日勤班"
                                    Case "白班"
                                        'If ADrivers.Count > 0 Then
                                        '    row.Cells("首日任务").Value = ADrivers(0).DutySort & "/" & ADrivers(0).OutPutCSDriverNo
                                        '    ADrivers.RemoveAt(0)
                                        'Else
                                        row.Cells("首日任务").Value = "夜班/无任务"
                                        'End If
                                        row.Cells("首日任务").Tag = "夜班"
                                        row.Cells("首日班种").Value = "夜班"
                                    Case "夜班"
                                        Dim iFAssigned As Boolean = False
                                        If PreAMCon IsNot Nothing Then
                                            For Each amdri As AMDriver In PreAMCon.AMDrivers
                                                If amdri.ADriver IsNot Nothing AndAlso amdri.ADriver.OutPutCSDriverNo = DriverNo Then
                                                    If amdri.MDriver IsNot Nothing Then
                                                        row.Cells("首日任务").Value = amdri.MDriver.DutySort & "/" & amdri.MDriver.OutPutCSDriverNo & "(" & amdri.MDriver.StartStaName & ")"
                                                        MDrivers.Remove(amdri.MDriver)
                                                        iFAssigned = True
                                                        Exit For
                                                    End If
                                                End If
                                            Next
                                        End If
                                        If iFAssigned = False Then
                                            row.Cells("首日任务").Value = "早班/SP()"
                                        End If
                                        row.Cells("首日任务").Tag = "早班"
                                        row.Cells("首日班种").Value = "早班"
                                    Case "休息"
                                        'If NDrivers.Count > 0 Then
                                        '    row.Cells("首日任务").Value = NDrivers(0).DutySort & "/" & NDrivers(0).OutPutCSDriverNo
                                        '    NDrivers.RemoveAt(0)
                                        'Else
                                        row.Cells("首日任务").Value = "白班/无任务"
                                        'End If
                                        row.Cells("首日任务").Tag = "白班"
                                        row.Cells("首日班种").Value = "白班"
                                    Case "日勤班"
                                        row.Cells("首日任务").Value = "休息/无任务"
                                        row.Cells("首日任务").Tag = "休息"
                                        row.Cells("首日班种").Value = "休息"
                                End Select

                                'ElseIf YunZhuanPara = "日勤班轮转" Then
                                '    For i As Integer = 0 To lunzhuanbanzhongs.Count - 1
                                '        If ForDutySort = lunzhuanbanzhongs(i) Then
                                '            row.Cells("首日任务").Value = "日勤班/无任务"
                                '            'End If
                                '            row.Cells("首日任务").Tag = lunzhuanbanzhongs(i + 1)
                                '            row.Cells("首日班种").Value = lunzhuanbanzhongs(i + 1)
                                '        End If
                                '    Next
                            End If
                    Case 2
                        For Each key As String In arealunzhuan.Keys
                            If key = AreaName Then
                                For i As Integer = 1 To arealunzhuan(AreaName).Count - 1
                                    If ForDutySort = arealunzhuan(AreaName)(i) Then
                                        row.Cells("首日任务").Value = "日勤班/无任务"
                                        'End If
                                        row.Cells("首日任务").Tag = arealunzhuan(AreaName)(i + 1)
                                        row.Cells("首日班种").Value = arealunzhuan(AreaName)(i + 1)
                                    End If
                                Next
                            End If
                        Next
                End Select
            Next
            Next
    End Sub

    Private Sub DGV_AssignFirst_CellMouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)
        CmbDuty.Visible = False
        CmbDutySort.Visible = False
        CurCellx = e.X
        CurCelly = e.Y
    End Sub

    Private Sub DGV_AssignFirst_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        CurMousex = e.X
        CurMousey = e.Y
    End Sub

    Private Sub DGV_AssignFirst_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Me.CurrentCell = CType(sender, DataGridView).CurrentCell
        Me.CurrentCell.Tag = sender
        Select Case sender.Columns(sender.SelectedCells(0).ColumnIndex).Name
            Case "首日任务"
                Call RefreshCmbItems()
                sender.Controls.Add(Me.CmbDuty)
                Me.CmbDuty.Size = sender.SelectedCells(0).Size
                Me.CmbDuty.Location = New Point(CurMousex - CurCellx + sender.Left, CurMousey - CurCelly + sender.Top)
                If sender.Rows(sender.CurrentCell.RowIndex).Cells("首日任务").Value Is Nothing Then
                    Me.CmbDuty.Text = ""
                Else
                    Me.CmbDuty.Text = sender.Rows(sender.CurrentCell.RowIndex).Cells("首日任务").Value.ToString
                End If
                Me.CmbDuty.Visible = True




            Case "首日班种"
                Dim YunZhuanPara As String = ""
                Dim areaname As String = CType(Me.CurrentCell.Tag, DataGridView).Parent.Text
                For Each area As AreaYunZhuan In AreaYunzhuanS
                    If area.AreaName = areaname Then
                        YunZhuanPara = area.YunZhuanPara
                    End If
                Next

                Dim decidemode As Integer = 0
                If areaname = "主区域" Or arealunzhuan.Keys.Contains(areaname) = False Then      '没有定制轮转匹配的区域按主区域处理
                    decidemode = 1
                Else
                    decidemode = 2
                End If
                    Select Case decidemode
                        Case 1
                            Select Case YunZhuanPara
                                Case "四班两转"
                                    CmbDutySort.Items.Clear()
                                    CmbDutySort.Items.Add("早班")
                                    CmbDutySort.Items.Add("白班")
                                    CmbDutySort.Items.Add("夜班")
                                    CmbDutySort.Items.Add("日勤班")
                                    CmbDutySort.Items.Add("休息")
                                Case "五班三转"
                                    CmbDutySort.Items.Clear()
                                    CmbDutySort.Items.Add("早班")
                                    CmbDutySort.Items.Add("白班")
                                    CmbDutySort.Items.Add("夜班")
                                    CmbDutySort.Items.Add("日勤班")
                                    CmbDutySort.Items.Add("休息")
                                    'Case "日勤班轮转"
                                    '    CmbDutySort.Items.Clear()

                                    '    For i As Integer = 0 To RQbanzhongs.Count - 1
                                    '        CmbDutySort.Items.Add(RQbanzhongs(i))
                                    '    Next
                            End Select
                    Case 2
                        CmbDutySort.Items.Clear()   '清空目前的可排列班种
                        For m As Integer = 1 To arealunzhuan(areaname).Count
                            CmbDutySort.Items.Add(arealunzhuan(areaname)(m))
                        Next


                        '    CSTimeTable = New Coordination2.CSTimeTable(Me.CurLine.GetCSTimeTableFromName(Me.cstimetableNameList(0)).ID, Me.CurLine.Name)
                        '    lunzhuanbanzhongs.Clear()
                        '    For Each duty As Coordination2.CSDriver In CSTimeTable.CCSDrivers
                        '        If duty.BelongArea = areaname Then
                        '            lunzhuanbanzhongs.Add(duty.CSdriverNo)
                        '        End If
                        '    Next
                        '    For Each duty As Coordination2.CSDriver In CSTimeTable.ACSDrivers
                        '        If duty.BelongArea = areaname Then
                        '            lunzhuanbanzhongs.Add(duty.CSdriverNo)
                        '        End If
                        '    Next
                        '    For Each duty As Coordination2.CSDriver In CSTimeTable.MCSDrivers
                        '        If duty.BelongArea = areaname Then
                        '            lunzhuanbanzhongs.Add(duty.CSdriverNo)
                        '        End If
                        '    Next
                        '    For Each duty As Coordination2.CSDriver In CSTimeTable.NCSDrivers
                        '        If duty.BelongArea = areaname Then
                        '            lunzhuanbanzhongs.Add(duty.CSdriverNo)
                        '        End If
                        '    Next

                        '    If arealunzhuan.Keys.Contains(areaname) Then
                        '        Dim n As Integer = 1
                        '        For m As Integer = 1 To arealunzhuan(areaname).Count
                        '            If arealunzhuan(areaname)(m) = "休息" Then
                        '                lunzhuanbanzhongs.Add("休息-" + (n).ToString)
                        '                n = n + 1
                        '            End If
                        '        Next
                        '        For i As Integer = 0 To lunzhuanbanzhongs.Count - 1
                        '            CmbDutySort.Items.Add(lunzhuanbanzhongs(i))
                        '        Next
                        '    End If
                End Select


                sender.Controls.Add(Me.CmbDutySort)
                Me.CmbDutySort.Size = sender.SelectedCells(0).Size
                Me.CmbDutySort.Location = New Point(CurMousex - CurCellx + sender.Left, CurMousey - CurCelly + sender.Top)
                If sender.Rows(sender.CurrentCell.RowIndex).Cells("首日班种").Value Is Nothing Then
                    Me.CmbDutySort.Text = ""
                Else
                    For sss As Integer = 0 To Me.CmbDutySort.Items.Count - 1
                        If Me.CmbDutySort.Items(sss).ToString.Contains(sender.Rows(sender.CurrentCell.RowIndex).Cells("首日班种").Value.ToString) Then
                            If Me.CmbDutySort.Items(sss).ToString = sender.Rows(sender.CurrentCell.RowIndex).Cells("首日班种").Value.ToString Then
                                Me.CmbDutySort.Text = sender.Rows(sender.CurrentCell.RowIndex).Cells("首日班种").Value.ToString
                            Else
                                Me.CmbDutySort.Text = Me.CmbDutySort.Items(sss).ToString
                            End If
                            Exit For
                        End If
                    Next
                    ' Me.CmbDutySort.Text = sender.Rows(sender.CurrentCell.RowIndex).Cells("首日班种").Value.ToString
                End If
                Me.CmbDutySort.Visible = True
        End Select
    End Sub

    Private Sub CmbDutyValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.CmbDuty.Text.Trim = "" Then
            Me.CurrentCell.Tag.CurrentCell.Value = "休息/无任务"
        Else
            Me.CurrentCell.Tag.CurrentCell.Value = CmbDuty.Text.Trim
        End If
        Me.CmbDuty.Visible = False
    End Sub

    Private Sub CmbDutySortValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.CmbDutySort.Text.Trim = "" Then
            Me.CurrentCell.Tag.CurrentCell.Value = ""
        Else
            Me.CurrentCell.Tag.CurrentCell.Value = CmbDutySort.Text.Trim
        End If
        Me.CmbDutySort.Visible = False
    End Sub

    Public Function GetCSDriverFromStr(ByVal DutyStr As String, ByVal CSTtable As Coordination2.CSTimeTable) As Coordination2.CSDriver
        GetCSDriverFromStr = Nothing
        Dim dutySort As String = ""
        Dim DriverNo As String = ""
        If DutyStr.Contains("/") Then
            dutySort = DutyStr.Substring(0, DutyStr.IndexOf("/"))
            If DutyStr.Contains("(") Then
                DriverNo = DutyStr.Substring(DutyStr.IndexOf("/") + 1, DutyStr.IndexOf("(") - DutyStr.IndexOf("/") - 1)
            Else
                DriverNo = DutyStr.Substring(DutyStr.IndexOf("/") + 1)
            End If
        Else
            dutySort = DutyStr
        End If
        GetCSDriverFromStr = GetCSDriverFromTimetable(DriverNo, dutySort, CSTtable)
    End Function

    Private Sub Btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_OK.Click
        For Each dgv As DataGridView In DutyDGVS
            Dim AreaName As String = dgv.Parent.Text
            Dim SelectArea As AreaYunZhuan = AreaYunzhuanS.Find(Function(value As AreaYunZhuan)
                                                                    Return value.AreaName = AreaName
                                                                End Function)
            For Each row As DataGridViewRow In dgv.Rows
                Dim dutystr As String = row.Cells("首日任务").Value.ToString
                Dim dutySort As String = dutystr.Split("/")(0) ''早班，夜班，白班，日勤班，。。。
                If dutySort.Contains("日勤班") Then dutySort = "日勤班"
                Dim ForDutySort As String = row.Cells("首日班种").Value.ToString.Trim
                Dim teamno As String = row.Cells("组号").Value.ToString
                'If ForDutySort = "" _
                '    OrElse (ForDutySort <> "早班" AndAlso ForDutySort <> "白班" AndAlso ForDutySort <> "日勤班" _
                '            AndAlso ForDutySort <> "日勤班-1" _
                '                           AndAlso ForDutySort <> "日勤班-2" _
                '                           AndAlso ForDutySort <> "日勤班-3" _
                '                           AndAlso ForDutySort <> "日勤班-4" _
                '                           AndAlso ForDutySort <> "休息-1" _
                '                           AndAlso ForDutySort <> "休息-2" _
                '                            AndAlso ForDutySort <> "夜班" AndAlso ForDutySort <> "休息") Then
                If ForDutySort = "" _
                    OrElse (ForDutySort <> "早班" AndAlso ForDutySort <> "白班" AndAlso ForDutySort <> "日勤班" _
                                AndAlso ForDutySort <> "夜班" AndAlso ForDutySort <> "休息" _
                                ) Then
                    If arealunzhuan.Keys.Contains(AreaName) AndAlso arealunzhuan(AreaName).Values.Contains(ForDutySort) = False Then
                        MsgBox("第'" & teamno & "'没有安排首日班种,请安排完整！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")

                        Exit Sub
                    End If
                End If
                Dim DriverNo As String = ""

                'If dutySort = "无任务" Then
                '    DriverNo = dutySort
                'Else
                If dutystr.Contains("SP") Then
                Else
                    If dutySort = "早班" Then
                        DriverNo = dutystr.Substring(dutystr.IndexOf("/") + 1, dutystr.IndexOf("(") - dutystr.IndexOf("/") - 1)
                    Else
                        DriverNo = dutystr.Split("/")(1)
                    End If

                End If

                'End If

                Dim SelectTeam As CrewTrainingManager.DriverTeam = SelectArea.AvaDrivers.Find(Function(value As CrewTrainingManager.DriverTeam)
                                                                                                  Return value.TeamNo = teamno
                                                                                              End Function)
                Dim temCSDriver As Coordination2.CSDriver = GetCSDriverFromTimetableByOutPutNo(DriverNo, dutySort, FirDayTimeTable)
                If temCSDriver IsNot Nothing Then
                    'If temCSDriver.DutySort = "夜班" AndAlso NextAMCon IsNot Nothing Then
                    If temCSDriver.DutySort = "夜班" AndAlso NextAMCon.AMDrivers.Count <> 0 Then
                        For Each dri As Coordination2.Driver In SelectTeam.CoDrivers
                            Dim temAMDuty As AMDriver = NextAMCon.AMDrivers.Find(Function(value As AMDriver)
                                                                                     Return value.ADriver.OutPutCSDriverNo = DriverNo
                                                                                 End Function)                                                      '如果是夜班，则安排夜早联合班
                            AddCSDriver(dri, temAMDuty, Me.FirDayDate.Date)
                        Next
                    Else
                        For Each dri As Coordination2.Driver In SelectTeam.CoDrivers
                            AddCSDriver(dri, temCSDriver, Me.FirDayDate.Date)
                        Next
                    End If
                Else
                    temCSDriver = New Coordination2.CSDriver()
                    'Select ForDutySort
                    '    Case "早班"
                    '        'temCSDriver.startdutytime = 5 * 3600
                    '        'temCSDriver.endtime = 9 * 3600
                    '        'temCSDriver.TotalDayDriveTime = 4 * 3600
                    '        'temCSDriver.TotalDayWorkTime = 4 * 3600
                    '        temCSDriver.DutySort = ForDutySort
                    '        'temCSDriver.CSdriverNo = "SP"
                    '        'temCSDriver.OutPutCSDriverNo = "SP"
                    '    Case "白班"
                    '        'temCSDriver.startdutytime = 9 * 3600
                    '        'temCSDriver.endtime = 17 * 3600
                    '        'temCSDriver.TotalDayDriveTime = 8 * 3600
                    '        'temCSDriver.TotalDayWorkTime = 8 * 3600
                    '        temCSDriver.DutySort = ForDutySort
                    '        'temCSDriver.CSdriverNo = "SP"
                    '        'temCSDriver.OutPutCSDriverNo = "SP"
                    '    Case "日勤班"
                    '        'temCSDriver.startdutytime = 9 * 3600
                    '        'temCSDriver.endtime = 17 * 3600
                    '        'temCSDriver.TotalDayDriveTime = 8 * 3600
                    '        'temCSDriver.TotalDayWorkTime = 8 * 3600
                    '        temCSDriver.DutySort = dutySort
                    '        'temCSDriver.CSdriverNo = "SP"
                    '        'temCSDriver.OutPutCSDriverNo = "SP"
                    '    Case "夜班"
                    '        'temCSDriver.startdutytime = 17 * 3600
                    '        'temCSDriver.endtime = 21 * 3600
                    '        'temCSDriver.TotalDayDriveTime = 4 * 3600
                    '        'temCSDriver.TotalDayWorkTime = 4 * 3600
                    '        temCSDriver.DutySort = ForDutySort
                    '        'temCSDriver.CSdriverNo = "SP"
                    '        'temCSDriver.OutPutCSDriverNo = "SP"
                    '    Case Else
                    '        temCSDriver.DutySort = ForDutySort
                    'End Select
                    'If dutySort = "日勤班" Then
                    '    temCSDriver.DutySort = dutySort
                    'End If
                    temCSDriver.DutySort = dutySort
                    temCSDriver.CSdriverNo = "无任务"
                    temCSDriver.OutPutCSDriverNo = "无任务"
                    temCSDriver.DriveDistance = 0
                    For Each dri As Coordination2.Driver In SelectTeam.CoDrivers
                        AddCSDriver(dri, temCSDriver, Me.FirDayDate.Date)
                    Next
                End If
                For Each dri As Coordination2.Driver In SelectTeam.CoDrivers
                    dri.DriverDayJobs(0).ForDutySort = ForDutySort
                Next
            Next
        Next
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        For Each dgv As DataGridView In DutyDGVS
            For Each row As DataGridViewRow In dgv.Rows
                row.Cells("首日任务").Value = "休息/无任务"
                row.Cells("首日班种").Value = "休息"
            Next
        Next
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call SetFirDayDutys()
    End Sub

    Private Sub SetFirDayDutyJobs()
        For Each dgv As DataGridView In DutyDGVS
            Dim AreaName As String = dgv.Parent.Text
            Dim YunZhuanPara As String = ""
            For Each area As AreaYunZhuan In AreaYunzhuanS
                If area.AreaName = AreaName Then
                    YunZhuanPara = area.YunZhuanPara
                End If
            Next
            Dim MDrivers As New List(Of Coordination2.CSDriver)
            Dim NDrivers As New List(Of Coordination2.CSDriver)
            Dim CDrivers As New List(Of Coordination2.CSDriver)
            Dim ADrivers As New List(Of Coordination2.CSDriver)
            For Each Dri As Coordination2.CSDriver In FirDayTimeTable.MCSDrivers
                If Dri.BelongArea = AreaName Then
                    MDrivers.Add(Dri)
                End If
            Next
            For Each Dri As Coordination2.CSDriver In FirDayTimeTable.NCSDrivers
                If Dri.BelongArea = AreaName Then
                    NDrivers.Add(Dri)
                End If
            Next
            For Each Dri As Coordination2.CSDriver In FirDayTimeTable.CCSDrivers
                If Dri.BelongArea = AreaName Then
                    CDrivers.Add(Dri)
                End If
            Next
            For Each Dri As Coordination2.CSDriver In FirDayTimeTable.ACSDrivers
                If Dri.BelongArea = AreaName Then
                    ADrivers.Add(Dri)
                End If
            Next
            For Each row As DataGridViewRow In dgv.Rows
                Me.CurrentCell = row.Cells("首日任务")
                Me.CurrentCell.Tag = dgv
                RefreshCmbItems()
                Dim FirDayDutySort As String = row.Cells("首日班种").Value.ToString.Trim
                If FirDayDutySort = "" Then
                    Continue For
                End If

                Dim cellStr As String = row.Cells("前日任务").Value.ToString.Trim

                Dim RealDutysort As String = cellStr.Split("/")(0)  '前日任务班种
                Dim ForDutySort As String = row.Cells("前日班种").Value.ToString
                Dim DriverNo As String = ""
                If RealDutysort = "夜班" Then
                    DriverNo = cellStr.Substring(cellStr.IndexOf("/") + 1, cellStr.IndexOf("(") - cellStr.IndexOf("/") - 1)
                ElseIf RealDutysort = "" Then
                    DriverNo = ""
                Else

                    DriverNo = cellStr.Split("/")(1)
                End If

               Dim decidemode As Integer = 0
                If AreaName = "主区域" Or arealunzhuan.Keys.Contains(AreaName) = False Then      '没有定制轮转匹配的区域按主区域处理
                    decidemode = 1
                Else
                    decidemode = 2
                End If
                Static Dim Adriveroff As Integer = 1  '晚班休息
                Static Dim Ndriveroff As Integer = 1  '白班休息
                Static Dim Mdriveroff As Integer = 1  '早班休息
                Select Case decidemode
                    Case 1
                        If YunZhuanPara = "四班两转" Then
                            Select Case FirDayDutySort
                                Case "早班"
                                    Dim iFAssigned As Boolean = False
                                    If PreAMCon IsNot Nothing Then
                                        For Each amdri As AMDriver In PreAMCon.AMDrivers
                                            If amdri.ADriver IsNot Nothing AndAlso amdri.ADriver.OutPutCSDriverNo = DriverNo Then
                                                If amdri.MDriver IsNot Nothing Then
                                                    row.Cells("首日任务").Value = amdri.MDriver.DutySort & "/" & amdri.MDriver.OutPutCSDriverNo & "(" & amdri.MDriver.StartStaName & ")"
                                                    MDrivers.Remove(amdri.MDriver)
                                                    iFAssigned = True
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                    End If
                                    If iFAssigned = False Then
                                        row.Cells("首日任务").Value = "早班/SP" + Mdriveroff.ToString
                                        Mdriveroff += 1
                                    End If
                                    row.Cells("首日任务").Tag = "早班"
                                Case "白班"
                                    Dim IsAssigned As Boolean = False
                                    For Each v As String In CmbDuty.Items
                                        Dim tmpDutySort As String = v.Split("/")(0)
                                        If tmpDutySort = "白班" AndAlso v <> "白班/无任务" Then
                                            row.Cells("首日任务").Value = v.ToString()
                                            IsAssigned = True
                                            Exit For
                                        End If

                                    Next
                                    If IsAssigned = False Then
                                        row.Cells("首日任务").Value = "白班/SP" + Ndriveroff.ToString
                                        Ndriveroff += 1
                                    End If
                                Case "夜班"
                                    Dim IsAssigned As Boolean = False
                                    For Each v As String In CmbDuty.Items
                                        Dim tmpDutySort As String = v.Split("/")(0)
                                        If tmpDutySort = "夜班" AndAlso v <> "夜班/无任务" Then
                                            row.Cells("首日任务").Value = v.ToString()
                                            IsAssigned = True
                                            Exit For
                                        End If

                                    Next
                                    If IsAssigned = False Then
                                        row.Cells("首日任务").Value = "夜班/SP" + Adriveroff.ToString
                                        Adriveroff += 1
                                    End If
                                    'row.Cells("首日班种").Value = "早班"
                                Case "休息"
                                    Dim IsAssigned As Boolean = False
                                    For Each v As String In CmbDuty.Items
                                        Dim tmpDutySort As String = v.Split("/")(0)
                                        If tmpDutySort = "休息" AndAlso v <> "休息/无任务" Then
                                            row.Cells("首日任务").Value = v.ToString()
                                            IsAssigned = True
                                            Exit For
                                        End If

                                    Next
                                    If IsAssigned = False Then
                                        row.Cells("首日任务").Value = "休息/无任务"
                                    End If
                                    'Case "日勤班"
                                    '    Dim IsAssigned As Boolean = False
                                    '    For Each v As String In CmbDuty.Items
                                    '        Dim tmpDutySort As String = v.Split("/")(0)
                                    '        If tmpDutySort = "日勤班" AndAlso v <> "日勤班/无任务" Then
                                    '            row.Cells("首日任务").Value = v.ToString()
                                    '            IsAssigned = True
                                    '            Exit For
                                    '        End If

                                    '    Next
                                    '    If IsAssigned = False Then
                                    '        row.Cells("首日任务").Value = "日勤班/无任务"
                                    '    End If
                            End Select
                        ElseIf YunZhuanPara = "五班三转" Then '上海白夜早峰休
                            Select Case FirDayDutySort
                                Case "早班"
                                    Dim iFAssigned As Boolean = False
                                    If PreAMCon IsNot Nothing Then
                                        For Each amdri As AMDriver In PreAMCon.AMDrivers
                                            If amdri.ADriver IsNot Nothing AndAlso amdri.ADriver.OutPutCSDriverNo = DriverNo Then
                                                If amdri.MDriver IsNot Nothing Then
                                                    row.Cells("首日任务").Value = amdri.MDriver.DutySort & "/" & amdri.MDriver.OutPutCSDriverNo & "(" & amdri.MDriver.StartStaName & ")"
                                                    MDrivers.Remove(amdri.MDriver)
                                                    iFAssigned = True
                                                    Exit For
                                                End If
                                            End If
                                        Next
                                    End If
                                    If iFAssigned = False Then
                                        row.Cells("首日任务").Value = "早班/SP" + Mdriveroff.ToString
                                        Mdriveroff += 1
                                    End If
                                    row.Cells("首日任务").Tag = "早班"
                                Case "白班"
                                    Dim IsAssigned As Boolean = False
                                    For Each v As String In CmbDuty.Items
                                        Dim tmpDutySort As String = v.Split("/")(0)
                                        If tmpDutySort = "白班" AndAlso v <> "白班/无任务" Then
                                            row.Cells("首日任务").Value = v.ToString()
                                            IsAssigned = True
                                            Exit For
                                        End If

                                    Next
                                    If IsAssigned = False Then
                                        row.Cells("首日任务").Value = "白班/SP" + Ndriveroff.ToString
                                        Ndriveroff += 1
                                    End If
                                Case "夜班"
                                    Dim IsAssigned As Boolean = False
                                    For Each v As String In CmbDuty.Items
                                        Dim tmpDutySort As String = v.Split("/")(0)
                                        If tmpDutySort = "夜班" AndAlso v <> "夜班/无任务" Then
                                            row.Cells("首日任务").Value = v.ToString()
                                            IsAssigned = True
                                            Exit For
                                        End If

                                    Next
                                    If IsAssigned = False Then
                                        row.Cells("首日任务").Value = "夜班/SP" + Adriveroff.ToString
                                        Adriveroff += 1
                                    End If
                                    'row.Cells("首日班种").Value = "早班"
                                Case "休息"
                                    Dim IsAssigned As Boolean = False
                                    For Each v As String In CmbDuty.Items
                                        Dim tmpDutySort As String = v.Split("/")(0)
                                        If tmpDutySort = "休息" AndAlso v <> "休息/无任务" Then
                                            row.Cells("首日任务").Value = v.ToString()
                                            IsAssigned = True
                                            Exit For
                                        End If

                                    Next
                                    If IsAssigned = False Then
                                        row.Cells("首日任务").Value = "休息/无任务"
                                    End If
                                Case "日勤班"
                                    Dim IsAssigned As Boolean = False
                                    For Each v As String In CmbDuty.Items
                                        Dim tmpDutySort As String = v.Split("/")(0)
                                        If v <> "日勤班/无任务" AndAlso tmpDutySort = "日勤班" Then
                                            row.Cells("首日任务").Value = v.ToString()
                                            IsAssigned = True
                                            Exit For
                                        End If

                                    Next
                                    If IsAssigned = False Then
                                        row.Cells("首日任务").Value = "休息/无任务"
                                    End If
                            End Select
                        End If
                    Case 2
                        Dim IsAssigned As Boolean = False
                        If FirDayDutySort.Contains("值班") Then
                            For Each v As String In CmbDuty.Items
                                Dim tmpDutySort As String = v.Split("/")(1)
                                If tmpDutySort <> "无任务" Then
                                    row.Cells("首日任务").Value = v.ToString()
                                    IsAssigned = True
                                    Exit For
                                End If
                            Next
                        ElseIf FirDayDutySort.Contains("休息") Then
                            row.Cells("首日任务").Value = "休息/无任务"
                            row.Cells("首日任务").Tag = FirDayDutySort
                            'row.Cells("首日班种").Value = FirDayDutySort
                        End If
                        'ElseIf YunZhuanPara = "日勤班轮转" Then
                        '    If FirDayDutySort.Contains("日勤班") Then
                        '        Dim IsAssigned As Boolean = False
                        '        For Each v As String In CmbDuty.Items
                        '            Dim tmpDutySort As String = v.Split("/")(0)
                        '            If tmpDutySort = "日勤班" AndAlso v <> "日勤班/无任务" Then
                        '                row.Cells("首日任务").Value = v.ToString()
                        '                IsAssigned = True
                        '                Exit For
                        '            End If

                        '        Next
                        '        If row.Cells("首日任务").Value.ToString.Contains("日勤班") Then
                        '            IsAssigned = True
                        '        End If
                        '        If IsAssigned = False Then
                        '            row.Cells("首日任务").Value = "日勤班" & "/无任务"
                        '        End If
                        '    ElseIf FirDayDutySort.Contains("休息") Then
                        '        row.Cells("首日任务").Value = "休息/无任务"
                        '        row.Cells("首日任务").Tag = FirDayDutySort
                        '        row.Cells("首日班种").Value = FirDayDutySort
                        '        'ElseIf FirDayDutySort.Contains("休息-2") Then
                        '        '    row.Cells("首日任务").Value = "休息/无任务"
                        '        '    row.Cells("首日任务").Tag = "休息-2"
                        '        '    row.Cells("首日班种").Value = "休息-2"
                        '    End If
                        'Select Case FirDayDutySort
                        '    Case "日勤班"
                        '        ''If CDrivers.Count > 0 Then
                        '        ''    row.Cells("首日任务").Value = CDrivers(0).DutySort & "/" & CDrivers(0).OutPutCSDriverNo
                        '        ''    CDrivers.RemoveAt(0)
                        '        ''Else
                        '        'row.Cells("首日任务").Value = "日勤班/无任务"
                        '        ''End If
                        '        'row.Cells("首日任务").Tag = "日勤班/2"
                        '        'row.Cells("首日班种").Value = "日勤班/2"

                        '    Case "日勤班02"
                        '        Dim IsAssigned As Boolean = False
                        '        For Each v As String In CmbDuty.Items
                        '            Dim tmpDutySort As String = v.Split("/")(0)
                        '            If tmpDutySort = "日勤班" AndAlso v <> "日勤班/无任务" Then
                        '                row.Cells("首日任务").Value = v.ToString()
                        '                IsAssigned = True
                        '                Exit For
                        '            End If

                        '        Next
                        '        If IsAssigned = False Then
                        '            row.Cells("首日任务").Value = ForDutySort & "/无任务"
                        '        End If
                        '    Case "日勤班-2"
                        '        Dim IsAssigned As Boolean = False
                        '        For Each v As String In CmbDuty.Items
                        '            Dim tmpDutySort As String = v.Split("/")(0)
                        '            If tmpDutySort = "日勤班" AndAlso v <> "日勤班/无任务" Then
                        '                row.Cells("首日任务").Value = v.ToString()
                        '                IsAssigned = True
                        '                Exit For
                        '            End If

                        '        Next
                        '        If IsAssigned = False Then
                        '            row.Cells("首日任务").Value = "日勤班" & "/无任务"
                        '        End If
                        '    Case "日勤班-3"
                        '        Dim IsAssigned As Boolean = False
                        '        For Each v As String In CmbDuty.Items
                        '            Dim tmpDutySort As String = v.Split("/")(0)
                        '            If tmpDutySort = "日勤班" AndAlso v <> "日勤班/无任务" Then
                        '                row.Cells("首日任务").Value = v.ToString()
                        '                IsAssigned = True
                        '                Exit For
                        '            End If

                        '        Next
                        '        If IsAssigned = False Then
                        '            row.Cells("首日任务").Value = "日勤班" & "/无任务"
                        '        End If
                        '    Case "日勤班-4"
                        '        Dim IsAssigned As Boolean = False
                        '        For Each v As String In CmbDuty.Items
                        '            Dim tmpDutySort As String = v.Split("/")(0)
                        '            If tmpDutySort = "日勤班" AndAlso v <> "日勤班/无任务" Then
                        '                row.Cells("首日任务").Value = v.ToString()
                        '                IsAssigned = True
                        '                Exit For
                        '            End If

                        '        Next
                        '        If IsAssigned = False Then
                        '            row.Cells("首日任务").Value = "日勤班" & "/无任务"
                        '        End If
                        'Case "日勤班-5"
                        '    Dim IsAssigned As Boolean = False
                        '    For Each v As String In CmbDuty.Items
                        '        Dim tmpDutySort As String = v.Split("/")(0)
                        '        If tmpDutySort = "日勤班" AndAlso v <> "日勤班/无任务" Then
                        '            row.Cells("首日任务").Value = v.ToString()
                        '            IsAssigned = True
                        '            Exit For
                        '        End If

                        '    Next
                        '    If IsAssigned = False Then
                        '        row.Cells("首日任务").Value = "日勤班" & "/无任务"
                        ''    End If
                        '    Case "休息-1"
                        'row.Cells("首日任务").Value = "休息/无任务"
                        'row.Cells("首日任务").Tag = "休息-1"
                        'row.Cells("首日班种").Value = "休息-1"

                        '    Case "休息-2"
                        'row.Cells("首日任务").Value = "休息/无任务"
                        'row.Cells("首日任务").Tag = "休息-2"
                        'row.Cells("首日班种").Value = "休息-2"
                        ''Case "休息-2"
                        ''    'If CDrivers.Count > 0 Then
                        ''    '    row.Cells("首日任务").Value = CDrivers(0).DutySort & "/" & CDrivers(0).OutPutCSDriverNo
                        ''    '    CDrivers.RemoveAt(0)
                        ''    'Else
                        ''    row.Cells("首日任务").Value = "日勤班/无任务"
                        ''    'End If
                        ''    row.Cells("首日任务").Tag = "日勤班/1"
                        ''    row.Cells("首日班种").Value = "日勤班/1"
                        'End Select
                End Select

            Next
        Next

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call SetFirDayDutyJobs()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        For Each dgv As DataGridView In DutyDGVS
            For Each row As DataGridViewRow In dgv.Rows
                row.Cells("首日任务").Value = "休息/无任务"
            Next
        Next
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
            Dim frm As New FrmPilianganpai
            frm.ShowDialog()
            For Each dgv As DataGridView In DutyDGVS
                For Each row As DataGridViewRow In dgv.Rows
                    If frm.beclass2Duty.Keys.Contains(row.Cells("班组").Value.ToString) Then
                        row.Cells("首日班种").Value = frm.beclass2Duty(row.Cells("班组").Value.ToString)
                    End If
                Next
            Next
    End Sub

End Class