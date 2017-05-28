Public Class FrmYearVocation

    Public TeamList As New List(Of DriverTeam)
    Public SelectTeamList As New List(Of DriverTeam)

    Public ReadOnly Property SpanDate As TimeSpan
        Get
            Return Me.EndTimePicker.Value.Date - Me.StartTimePicker.Value.Date.AddDays(-1)
        End Get
    End Property

    Private Sub BtnSeqSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSeqSetting.Click
        Call LoadAllDrivers(TeamList, Me.CmbLine.Text.Trim)
        Dim nf As New FrmVocSeqSetting(TeamList)
        nf.SelectTeamList = Me.SelectTeamList
        If nf.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.SelectTeamList = nf.SelectTeamList
            Call LoadVocDrivers()
        End If
    End Sub

    Public Sub LoadVocDrivers()
        Me.DGVDriversSeq.Rows.Clear()
        Me.DGVYearVocDetial.Rows.Clear()

        For Each team As DriverTeam In SelectTeamList
            Me.DGVDriversSeq.Rows.Add(team.VocSeq, team.TeamNo, team.NameStr)
            If Me.DGVYearVocDetial.Columns.Count > 0 Then
                Me.DGVYearVocDetial.Rows.Add(team.TeamNo)
            End If
        Next
    End Sub

    Public Sub SetDGV_VovColumns()
        If SpanDate.Days <= 0 Then
            MsgBox("日期设置不对，请重新设置日期！", MsgBoxStyle.OkOnly, "提醒")
            Exit Sub
        End If
        Me.DGVYearVocDetial.Columns.Clear()
        Me.DGVYearVocDetial.Columns.Add("组号", "组号")
        For i As Integer = 0 To SpanDate.Days - 1
            Me.DGVYearVocDetial.Columns.Add(Me.StartTimePicker.Value.Date.AddDays(i).ToString("yyyy/MM/dd"), Me.StartTimePicker.Value.Date.AddDays(i).ToString("yyyy/MM/dd") & " " & GetWeekDayString(Me.StartTimePicker.Value.Date.AddDays(i)))
        Next
        Me.DGVYearVocDetial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        Me.DGVYearVocDetial.Columns("组号").Frozen = True
        Me.DGVYearVocDetial.Columns("组号").DefaultCellStyle.BackColor = Color.LightGreen
        DateSpan = Me.EndTimePicker.Value.Date.AddDays(1) - Me.StartTimePicker.Value.Date
        Call LoadVocDrivers()
    End Sub

    Public DateSpan As TimeSpan

    Private Sub FrmYearVocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If CurLineName = "" Then
            Dim str As String = "select linename from pd_lineinfo t order by lineid"
            Dim tab As DataTable = ReadData(str)
            If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
                Me.CmbLine.DataSource = tab
                Me.CmbLine.DisplayMember = "linename"
            End If
        Else
            Me.CmbLine.Items.Add(CurLineName)
            Me.CmbLine.Text = CurLineName
        End If
        
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.DGVYearVocDetial.ColumnCount = 0 Then
            MsgBox("请先选择日期再安排相应计划！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
            Exit Sub
        End If
        If SelectTeamList.Count > 0 Then
            Dim SpanDays As Integer = DateSpan.Days
            For i As Integer = 0 To SelectTeamList.Count - 1
                SelectTeamList(i).VocDates.Clear()
                If SpanDays >= SelectTeamList(i).VocDay Then
                    For j As Integer = 0 To SelectTeamList(i).VocDay - 1
                        Dim temVac As New TypeVac
                        temVac.vDate = Me.StartTimePicker.Value.Date.AddDays(j)
                        temVac.vType = SelectTeamList(i).VocType
                        SelectTeamList(i).VocDates.Add(temVac)
                    Next
                Else
                    For j As Integer = 0 To SpanDays - 1
                        Dim temVac As New TypeVac
                        temVac.vDate = Me.StartTimePicker.Value.Date.AddDays(j)
                        temVac.vType = SelectTeamList(i).VocType
                        SelectTeamList(i).VocDates.Add(temVac)
                    Next
                End If
            Next
            Call DrawDGV_VOC()
        End If
    End Sub

    Public Sub DrawDGV_VOC()
        If SelectTeamList.Count > 0 Then
            Me.DGVYearVocDetial.Rows.Clear()
            For Each team As DriverTeam In SelectTeamList
                Me.DGVYearVocDetial.Rows.Add(team.TeamNo)
            Next
            Me.ProgressBar1.Maximum = SelectTeamList.Count
            Me.ProgressBar1.Value = 0
            Me.ProgressBar1.Visible = True
            For i As Integer = 0 To SelectTeamList.Count - 1
                For Each _date As TypeVac In SelectTeamList(i).VocDates
                    Dim datestr As String = _date.vDate.ToString("yyyy/MM/dd")
                    Me.DGVYearVocDetial.Rows(i).Cells(datestr).Value = _date.vType
                    Me.DGVYearVocDetial.Rows(i).Cells(datestr).Style.BackColor = Color.LightBlue
                Next
                Me.ProgressBar1.PerformStep()
                Application.DoEvents()
            Next
            Me.ProgressBar1.Visible = False
        End If
    End Sub

    Private Sub StartTimePicker_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartTimePicker.ValueChanged
        Call SetDGV_VovColumns()
    End Sub

    Private Sub EndTimePicker_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EndTimePicker.ValueChanged
        Call SetDGV_VovColumns()
    End Sub

    Private Sub 安排年休NToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 安排年休NToolStripMenuItem.Click
        Call HandAssign("年假")
    End Sub

    Public Sub HandAssign(ByVal sType As String)
        If Me.DGVYearVocDetial.SelectedCells.Count > 0 Then
            For Each cell As DataGridViewCell In Me.DGVYearVocDetial.SelectedCells
                Dim ColumnIndex As Integer = cell.ColumnIndex
                Dim RowIndex As Integer = cell.RowIndex
                Dim TeamNo As String = Me.DGVYearVocDetial.Rows(RowIndex).Cells("组号").Value.ToString
                Dim temTeam As DriverTeam = SelectTeamList.Find(Function(value As DriverTeam)
                                                                    Return value.TeamNo = TeamNo
                                                                End Function)
                Dim VocDate As Date = CDate(Me.DGVYearVocDetial.Columns(ColumnIndex).Name)
                Dim index As Integer = temTeam.VocDates.FindIndex(Function(value As TypeVac)
                                                                      Return value.vDate = VocDate
                                                                  End Function)
                If index <> -1 Then
                    temTeam.VocDates.RemoveAt(index)
                End If
                Dim temVac As New TypeVac
                temVac.vDate = VocDate
                temVac.vType = sType
                temTeam.VocDates.Add(temVac)
                Me.DGVYearVocDetial.Rows(RowIndex).Cells(ColumnIndex).Value = temVac.vType
                Me.DGVYearVocDetial.Rows(RowIndex).Cells(ColumnIndex).Style.BackColor = Color.LightBlue
            Next
        End If
    End Sub

    Private Sub Btn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Save.Click
        If Me.SelectTeamList.Count > 0 Then
            Dim str As String
            str = "select lineid,teamno,count(vacdate) as spanday from cs_vacinf where lineid='" & Me.CmbLine.Text.Trim & "' and vacdate>='" & Me.StartTimePicker.Value.Date.ToString("yyyy/MM/dd") & _
                "' and vacdate<='" & Me.EndTimePicker.Value.Date.ToString("yyyy/MM/dd") & "' and vactype='年假' group by lineid,teamno"
            Dim tab As Data.DataTable = ReadData(str)
            If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
                For Each row As DataRow In tab.Rows
                    Dim teamno As String = row.Item("teamno").ToString
                    Dim vacday As Integer = row.Item("spanday")
    
                Next
            End If
            tab.Dispose()
            str = "delete from cs_vacinf where lineid='" & Me.CmbLine.Text.Trim & "' and vacdate>='" & Me.StartTimePicker.Value.Date.ToString("yyyy/MM/dd") & _
                "' and vacdate<='" & Me.EndTimePicker.Value.Date.ToString("yyyy/MM/dd") & "'"
            UpdateData(str)
            Dim ErrorStr As String = ""
            Me.ProgressBar1.Maximum = SelectTeamList.Count
            Me.ProgressBar1.Value = 0
            Me.ProgressBar1.Visible = True
            Try
                For Each team As DriverTeam In SelectTeamList
                    For Each _date As TypeVac In team.VocDates
                        str = "insert into cs_vacinf (vacatdid,lineid,teamno,vacdate,vactype,detail) " & _
                              "values('" & team.VocSeq & "','" & Me.CmbLine.Text.Trim & "','" & team.TeamNo & "','" & _date.vDate.ToString("yyyy/MM/dd") & _
                              "','" & _date.vType & "','"& team.DrivernoStr &"')"
                        UpdateData(str)
                    Next
                    Me.ProgressBar1.PerformStep()
                    Application.DoEvents()
                Next
                Me.ProgressBar1.Visible = False
                MsgBox("保存成功!" & ErrorStr.Trim(","), MsgBoxStyle.OkOnly, "成功")
            Catch ex As Exception
                Me.ProgressBar1.Visible = False
                MsgBox("保存失败，请检查相关安排的合理性!" & ErrorStr.Trim(","), MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
            End Try
        End If
    End Sub

    Private Sub DGVYearVocDetial_CellMouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGVYearVocDetial.CellMouseDown
        If Me.DGVYearVocDetial.Columns(e.ColumnIndex).Name = "组号" Then
            Me.DGVYearVocDetial.ContextMenuStrip = Nothing
        Else
            Me.DGVYearVocDetial.ContextMenuStrip = Me.CMDGV_Voc
        End If
    End Sub

    Private Sub Btn_CheckExist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_CheckExist.Click
        Call LoadAllDrivers(TeamList, Me.CmbLine.Text.Trim)
        SelectTeamList.Clear()
        Dim str As String = "select * from cs_vacinf t where lineid='" & Me.CmbLine.Text.Trim & "' and vacdate>='" & Me.StartTimePicker.Value.Date.ToString("yyyy/MM/dd") & _
                             "' and vacdate<= '" & Me.EndTimePicker.Value.Date.ToString("yyyy/MM/dd") & _
                             "' order by vacatdid"
        Dim temTab As DataTable = ReadData(str)
        If temTab IsNot Nothing AndAlso temTab.Rows.Count > 0 Then
            For Each row As DataRow In temTab.Rows
                Dim teamNo As String = row.Item("teamno").ToString
                Dim temTeam As DriverTeam = TeamList.Find(Function(value As DriverTeam)
                                                              Return value.TeamNo = teamNo
                                                          End Function)
                If temTeam IsNot Nothing Then
                    Dim index As Integer = SelectTeamList.FindIndex(Function(value As DriverTeam)
                                                                        Return value.TeamNo = temTeam.TeamNo
                                                                    End Function)
                    If index = -1 Then
                        temTeam.VocSeq = row.Item("vacatdid")
                        temTeam.LineID = row.Item("lineid")
                        temTeam.VocType = row.Item("vactype")
                        Dim temVac As New TypeVac
                        temVac.vDate = CDate(row.Item("vacdate").ToString).Date
                        temVac.vType = row.Item("vactype")
                        temTeam.VocDates.Add(temVac)
                        SelectTeamList.Add(temTeam)
                    Else
                        Dim temVac As New TypeVac
                        temVac.vDate = CDate(row.Item("vacdate").ToString).Date
                        temVac.vType = row.Item("vactype")
                        temTeam.VocDates.Add(temVac)
                    End If
                End If
            Next
            Call LoadVocDrivers()
            Call DrawDGV_VOC()
        End If
    End Sub

    Private Sub Btn_DeleteExist_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_DeleteExist.Click
        If MsgBox("确定删除'" & Me.StartTimePicker.Value.ToString("yyyy/MM/dd") & "'到'" & Me.EndTimePicker.Value.ToString("yyyy/MM/dd") & "'的年假计划？", MsgBoxStyle.OkCancel, "提醒") = MsgBoxResult.Ok Then
            Dim Str As String = "delete from cs_vacinf where lineid='" & Me.CmbLine.Text.Trim & "' and vacdate>='" & Me.StartTimePicker.Value.Date.ToString("yyyy/MM/dd") & _
                "' and vacdate<='" & Me.EndTimePicker.Value.Date.ToString("yyyy/MM/dd") & "'"
            UpdateData(Str)
            Me.SelectTeamList.Clear()
            Me.DGVDriversSeq.Rows.Clear()
            Me.DGVYearVocDetial.Rows.Clear()
            Call DrawDGV_VOC()
        End If
    End Sub

    Private Sub FrmYearVocation_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            Call DrawDGV_VOC()
        ElseIf e.KeyCode = Keys.Delete Then
            If Me.DGVYearVocDetial.SelectedCells.Count > 0 Then
                Call 删除安排DToolStripMenuItem_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub 安排培训RToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 安排培训RToolStripMenuItem.Click
        Call HandAssign("培训")
    End Sub

    Private Sub 删除安排DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除安排DToolStripMenuItem.Click
        If Me.DGVYearVocDetial.SelectedCells.Count > 0 Then
            If MsgBox("确实要删除该安排？", MsgBoxStyle.OkCancel + MsgBoxStyle.Information, "提醒") = MsgBoxResult.Ok Then
                For Each cell As DataGridViewCell In Me.DGVYearVocDetial.SelectedCells
                    Dim sDate As Date = CDate(Me.DGVYearVocDetial.Columns(cell.ColumnIndex).Name).Date
                    Dim sTeamNo As String = Me.DGVYearVocDetial.Rows(cell.RowIndex).Cells("组号").Value.ToString.Trim
                    Dim temTeam As DriverTeam = TeamList.Find(Function(value As DriverTeam)
                                                                  Return value.TeamNo = sTeamNo
                                                              End Function)
                    Dim temVac As New TypeVac
                    temVac.vDate = sDate
                    If cell.Value IsNot Nothing Then
                        temVac.vType = cell.Value.ToString.Trim
                    Else
                        temVac.vType = ""
                    End If
                    temTeam.VocDates.Remove(temVac)
                    cell.Value = ""
                    cell.Style.BackColor = Color.White
                Next
            End If
        End If
    End Sub
End Class

Public Class DriverDetial
    Public DriverNo As String
    Public DriverName As String
    Public Gender As String
    Public Minority As String
    Public BirthDay As String
    Public EnRollTime As String
    Public BeLine As String
    Public BeClass As String
    Public BeTeam As String
    Public TechGrade As String
    Public Postion As String
    Public PhoneNum As String
    Public WorkYear As Decimal
    Public BelongArea As String
    Public AvaAlDay As Integer

    Public Sub New()

    End Sub

    Public Sub New(ByVal _driverno As String, ByVal _name As String, ByVal _gender As String, ByVal _minority As String, ByVal _birth As String, ByVal _enroll As String _
                   , ByVal _beline As String, ByVal _beclass As String, ByVal _tech As String, ByVal _position As String, ByVal _phone As String, ByVal _BelongArea As String, Optional ByVal _beteam As String = "", Optional ByVal _avaAlDay As String = "")
        DriverNo = _driverno
        DriverName = _name
        Gender = _gender
        Minority = _minority
        BirthDay = _birth
        EnRollTime = _enroll
        BeLine = _beline
        BeClass = _beclass
        BeTeam = _beteam
        TechGrade = _tech
        Postion = _position
        PhoneNum = _phone
        BelongArea = _BelongArea
        If _avaAlDay = "" Then
            AvaAlDay = 0
        Else
            AvaAlDay = Integer.Parse(_avaAlDay)
        End If

        If Me.EnRollTime = "" Then
            WorkYear = 0
        Else
            WorkYear = (((Now.Date - CDate(Me.EnRollTime).Date.AddDays(-1)).Days) / 365)
        End If
    End Sub

End Class

Public Class DriverTeam
    Public LineID As String
    Public ClassName As String
    Public TeamNo As String
    Public VocDay As Integer    '休假天数
    Public VocDates As List(Of TypeVac)
    Public VocSeq As Integer    '休假顺序
    Public VocType As String
    Public Drivers As New List(Of DriverDetial)         '司机具体信息
    Public CoDrivers As New List(Of Coordination2.Driver)        '轮转时的司机
    Public IFVacation As Boolean                                 '轮转时判断是否有假期安排


    Public ReadOnly Property NameStr As String
        Get
            Dim tempstr As String = ""
            For Each dri As DriverDetial In Drivers
                tempstr &= dri.DriverName & "/"
            Next
            If tempstr = "" Then
                For Each dri As Coordination2.Driver In CoDrivers
                    tempstr &= dri.name & "/"
                Next
            End If
            Return tempstr.Trim("/")
        End Get
    End Property

    Public ReadOnly Property DrivernoStr As String
        Get
            Dim tempstr As String = ""
            For Each dri As DriverDetial In Drivers
                tempstr &= dri.DriverNo  & "/"
            Next
            If tempstr = "" Then
                For Each dri As Coordination2.Driver In CoDrivers
                    tempstr &= dri.rdriverno & "/"
                Next
            End If
            Return tempstr.Trim("/")
        End Get
    End Property

    Public ReadOnly Property XiuxiFengbanNums As Integer
        Get
            Dim temXiuxiDayNums As Integer = 0
            For Each dri As Coordination2.Driver In CoDrivers
                If dri.XiuxiFengBanNum > temXiuxiDayNums Then
                    temXiuxiDayNums = dri.XiuxiFengBanNum
                End If
            Next
            Return temXiuxiDayNums
        End Get
    End Property
    Public ReadOnly Property WorkYear As Decimal
        Get
            Dim MaxYear As Decimal = 0
            For Each dri As DriverDetial In Drivers
                If dri.WorkYear > MaxYear Then
                    MaxYear = dri.WorkYear
                End If
            Next
            Return MaxYear
        End Get
    End Property

    Public ReadOnly Property AvaAlDay As Decimal
        Get
            Dim MaxDay As Decimal = 0
            For Each dri As DriverDetial In Drivers
                If dri.AvaAlDay > MaxDay Then
                    MaxDay = dri.AvaAlDay
                End If
            Next
            Return MaxDay
        End Get
    End Property

    Public ReadOnly Property DriveTime As Integer
        Get
            Dim temtime As Integer = 0
            For Each dri As Coordination2.Driver In CoDrivers
                If dri.DriveTime > temtime Then
                    temtime = dri.DriveTime
                End If
            Next
            Return temtime
        End Get
    End Property

    Public ReadOnly Property DriveDistance As Integer
        Get
            Dim temdistance As Integer = 0
            For Each dri As Coordination2.Driver In CoDrivers
                If dri.DriveDistance > temdistance Then
                    temdistance = dri.DriveDistance
                End If
            Next
            Return temdistance
        End Get
    End Property

    Public ReadOnly Property PreDriveDistance As Integer
        Get
            Dim temdistance As Integer = 0
            For Each dri As Coordination2.Driver In CoDrivers
                If dri.PreDistance > temdistance Then
                    temdistance = dri.PreDistance
                End If
            Next
            Return temdistance
        End Get
    End Property

    Public ReadOnly Property ChuBanCount As Integer
        Get
            Dim temCount As Integer = 0
            For Each dri As Coordination2.Driver In CoDrivers
                If dri.DeadheadingNum > temCount Then
                    temCount = dri.DeadheadingNum
                End If
            Next
            Return temCount
        End Get
    End Property

    Public ReadOnly Property nightOutDepotCount As Integer
        Get
            Dim temCount As Integer = 0
            For Each dri As Coordination2.Driver In CoDrivers
                If dri.NightOutFromDepotNum > temCount Then
                    temCount = dri.NightOutFromDepotNum
                End If
            Next
            Return temCount
        End Get
    End Property

    Public ReadOnly Property ConWorkHours As Integer
        Get
            Dim temtime As Integer = 0
            For Each dri As Coordination2.Driver In CoDrivers
                If dri.ConWorkHours > temtime Then
                    temtime = dri.ConWorkHours
                End If
            Next
            Return temtime
        End Get
    End Property

    Public ReadOnly Property CulTime As Date
        Get
            Dim temtime As New Date("1900", "01", "01")
            For Each dri As Coordination2.Driver In CoDrivers
                If dri.CulTime > temtime Then
                    temtime = dri.CulTime
                End If
            Next
            Return temtime
        End Get
    End Property

    Public Property IFCTeam As Boolean
        Get
            Return Me.CoDrivers(0).IfCDriver
        End Get
        Set(ByVal value As Boolean)
            For Each dri As Coordination2.Driver In Me.CoDrivers
                dri.IfCDriver = value
            Next
        End Set
    End Property

    Public ReadOnly Property WorkTimes As Integer
        Get
            Return Me.CoDrivers(0).Worktimes
        End Get
    End Property

    Public Sub GetWorkLoad()
        For Each dri As Coordination2.Driver In CoDrivers
            dri.GetWorkLoad()
        Next
    End Sub

    Public Sub New(ByVal _lineid As String, ByVal _classname As String, ByVal _teamno As String)
        Drivers.Clear()
        LineID = _lineid
        ClassName = _classname
        TeamNo = _teamno
        VocDates = New List(Of TypeVac)

        IFVacation = False
    End Sub

End Class

Public Structure TypeVac
    Public vDate As Date
    Public vType As String
End Structure