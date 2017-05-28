Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class frmCSStaQuery
    Private mainForm As frmCSTimeTableMain
    Public Sub New(ByVal form1 As frmCSTimeTableMain)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        mainForm = form1
    End Sub
    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
        'frmCSShowAll.Show()

    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Call OutPutToEXCELFileFormDataGrid("统计数据表", Me.DataGrdStatQuery, Me)
    End Sub

    Private Sub BtnChaXun_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnChaXun.Click
        Call StatQuery()
    End Sub
    Public Sub StatQuery()
        Dim i, j As Integer
        Dim VarTime As Long = 0
        Dim AveTime As Long = 0
        Dim MaxTime As Integer = 0
        Dim MinTime As Integer = 0

        '填充指标
        If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
            Me.StaSta.Items.Clear()
            For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain Is Nothing Or UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain) = 0 Then
                    Continue For
                End If
                Dim flag As Boolean = False
                For j = 0 To Me.StaSta.Items.Count - 1
                    If CSTrainsAndDrivers.CSDrivers(i).DutySort = Me.StaSta.Items(j) Then
                        flag = True
                        Exit For
                    End If
                Next
                If flag = False And CSTrainsAndDrivers.CSDrivers(i).DutySort <> "" Then
                    Me.StaSta.Items.Add(CSTrainsAndDrivers.CSDrivers(i).DutySort)
                End If
            Next
            Me.StaSta.Items.Add("全部")
            Me.StaSta.SelectedIndex = StaSta.Items.Count - 1
        End If

        '填充datagridview,和初始指标

        If StaSta.Text = "全部" Or StaSta.Text = "" Then
            If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
                Dim TotalDriveTimeList As List(Of Integer) = New List(Of Integer)

                For Each driver As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If driver Is Nothing = False Then
                        TotalDriveTimeList.Add(driver.DriveDistance)
                    End If
                Next

                AveTime = TotalDriveTimeList.Average
                Me.AveTime.Text = (AveTime)
                For Each s As Integer In TotalDriveTimeList
                    VarTime = VarTime + (Math.Abs(CInt(s) - AveTime)) ^ 2
                Next
                Me.VarTime.Text = (Math.Sqrt(VarTime / UBound(CSTrainsAndDrivers.CSDrivers))).ToString("0.00")
                Me.MaxTime.Text = (TotalDriveTimeList.Max)
                Me.MinTime.Text = (TotalDriveTimeList.Min)

            Else
                Me.AveTime.Text = 0
                Me.VarTime.Text = 0
                Me.MaxTime.Text = 0
                Me.MinTime.Text = 0
            End If

        Else '分班种
            If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
                Dim Banzhong As String = Me.StaSta.Text.ToString.Trim

                Dim TotalDriveTimeList As List(Of Integer) = New List(Of Integer)

                For Each driver As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If driver Is Nothing = False Then
                        If driver.DutySort = Banzhong Then
                            TotalDriveTimeList.Add(driver.DriveDistance)
                        End If
                    End If
                Next

                AveTime = TotalDriveTimeList.Average
                Me.AveTime.Text = (AveTime)
                For Each s As Integer In TotalDriveTimeList
                    VarTime = VarTime + (Math.Abs(CInt(s) - AveTime)) ^ 2
                Next
                Me.VarTime.Text = (Math.Sqrt(VarTime / UBound(CSTrainsAndDrivers.CSDrivers))).ToString("0.00")
                Me.MaxTime.Text = (TotalDriveTimeList.Max)
                Me.MinTime.Text = (TotalDriveTimeList.Min)
            Else
                Me.AveTime.Text = 0
                Me.VarTime.Text = 0
                Me.MaxTime.Text = 0
                Me.MinTime.Text = 0
            End If
        End If

        Call DrawDGV()
    End Sub

    Public tabDriver As DataTable
    Public Sub DrawDGV(Optional ByVal DutySort As String = "全部")
        If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
            tabDriver = New DataTable
            tabDriver.Columns.Add("线路号", GetType(String))
            tabDriver.Columns.Add("编号", GetType(String))
            tabDriver.Columns.Add("司机编号", GetType(String))
            tabDriver.Columns.Add("输出编号", GetType(String))
            tabDriver.Columns.Add("班种", GetType(String))
            tabDriver.Columns.Add("出勤时间", GetType(String))
            tabDriver.Columns.Add("退勤时间", GetType(String))
            tabDriver.Columns.Add("工作时间", GetType(String))
            tabDriver.Columns.Add("驾驶公里", GetType(Single))
            For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                If DutySort <> "全部" Then
                    If CSTrainsAndDrivers.CSDrivers(i).DutySort <> DutySort Then
                        Continue For
                    End If
                End If
                tabDriver.Rows.Add()
                tabDriver.Rows(tabDriver.Rows.Count - 1).Item("线路号") = strCurlineID
                tabDriver.Rows(tabDriver.Rows.Count - 1).Item("编号") = CSTrainsAndDrivers.CSDrivers(i).CSDriverID
                tabDriver.Rows(tabDriver.Rows.Count - 1).Item("司机编号") = CSTrainsAndDrivers.CSDrivers(i).CSdriverNo
                tabDriver.Rows(tabDriver.Rows.Count - 1).Item("输出编号") = CSTrainsAndDrivers.CSDrivers(i).OutPutCSdriverNo
                tabDriver.Rows(tabDriver.Rows.Count - 1).Item("班种") = CSTrainsAndDrivers.CSDrivers(i).DutySort
                tabDriver.Rows(tabDriver.Rows.Count - 1).Item("工作时间") = BeTime(CSTrainsAndDrivers.CSDrivers(i).WorkTime)
                tabDriver.Rows(tabDriver.Rows.Count - 1).Item("驾驶公里") = CSTrainsAndDrivers.CSDrivers(i).DriveDistance

                If UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain) > 0 Then
                    tabDriver.Rows(tabDriver.Rows.Count - 1).Item("出勤时间") = BeTime(CSTrainsAndDrivers.CSDrivers(i).BeginWorkTime)
                    tabDriver.Rows(tabDriver.Rows.Count - 1).Item("退勤时间") = BeTime(CSTrainsAndDrivers.CSDrivers(i).EndEorkTime)
                End If
            Next
            Me.DataGrdStatQuery.DataSource = tabDriver
            Me.DataGrdStatQuery.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End If
    End Sub

    Private Sub frmCSStaQuery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.AveTime.Text = 0
        Me.VarTime.Text = 0
        Me.MaxTime.Text = 0
        Me.MinTime.Text = 0
        Call StatQuery()

        If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
            Dim tempWorkTime, tempTOTALZFTIME, tempDRIVEDistance, tempTotalDiverNum, tempTotalDutyNum As Integer
            tempWorkTime = 0
            tempTOTALZFTIME = 0
            tempDRIVEDistance = 0
            tempTotalDiverNum = 0
            tempTotalDutyNum = 0
            For Each driver As CSDriver In CSTrainsAndDrivers.CSDrivers
                If driver Is Nothing = False Then
                    tempWorkTime = tempWorkTime + driver.TotalDriveTime
                    tempDRIVEDistance = tempDRIVEDistance + driver.DriveDistance
                    tempTOTALZFTIME = tempTOTALZFTIME + driver.ZFTime
                    tempTotalDutyNum = tempTotalDutyNum + driver.ModifiedDutyNumber
                End If
            Next

            tempTotalDiverNum = UBound(CSTrainsAndDrivers.CSDrivers)

            'Me.TxtTotalTime.Text = CStr(BeTime(tempWorkTime))
            Me.TxtTotalDriveTime.Text = CStr((tempDRIVEDistance))
            'Me.TxtTotalZFTime.Text = CStr(BeTime(tempTOTALZFTIME))
            Me.TxtDutyNum.Text = CStr(tempTotalDutyNum)
            Me.TxtTotalMember.Text = CStr(tempTotalDiverNum)
        End If
    End Sub
    Private Sub StaSta_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StaSta.SelectedIndexChanged
        Dim VarTime As Long = 0
        Dim AveTime As Long = 0
        Dim MaxTime As Integer = 0
        Dim MinTime As Integer = 0
        '填充datagridview,和指标
        If StaSta.Text = "全部" Or StaSta.Text = "" Then
            If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
                Dim TotalDriveTimeList As List(Of Integer) = New List(Of Integer)

                For Each driver As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If driver Is Nothing = False Then
                        TotalDriveTimeList.Add(driver.DriveDistance)
                    End If
                Next

                AveTime = TotalDriveTimeList.Average
                Me.AveTime.Text = (AveTime)
                For Each s As Integer In TotalDriveTimeList
                    VarTime = VarTime + (Math.Abs(CInt(s) - AveTime)) ^ 2
                Next
                Me.VarTime.Text = (Math.Sqrt(VarTime / UBound(CSTrainsAndDrivers.CSDrivers))).ToString("0.00")
                Me.MaxTime.Text = (TotalDriveTimeList.Max)
                Me.MinTime.Text = (TotalDriveTimeList.Min)
                Me.CSDriverNum.Text = TotalDriveTimeList.Count
                Call DrawDGV()
            Else
                Me.AveTime.Text = 0
                Me.VarTime.Text = 0
                Me.MaxTime.Text = 0
                Me.MinTime.Text = 0
                Me.CSDriverNum.Text = 0
            End If
        Else '分班种
            If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
                Dim Banzhong As String = Me.StaSta.Text.ToString.Trim

                Dim TotalDriveTimeList As List(Of Integer) = New List(Of Integer)

                For Each driver As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If driver Is Nothing = False Then
                        If driver.DutySort = Banzhong Then
                            TotalDriveTimeList.Add(driver.DriveDistance)
                        End If
                    End If
                Next

                AveTime = TotalDriveTimeList.Average
                Me.AveTime.Text = (AveTime)
                For Each s As Integer In TotalDriveTimeList
                    VarTime = VarTime + (Math.Abs(CInt(s) - AveTime)) ^ 2
                Next
                Me.VarTime.Text = (Math.Sqrt(VarTime / UBound(CSTrainsAndDrivers.CSDrivers))).ToString("0.00")
                Me.MaxTime.Text = (TotalDriveTimeList.Max)
                Me.MinTime.Text = (TotalDriveTimeList.Min)
                Me.CSDriverNum.Text = TotalDriveTimeList.Count
                Call DrawDGV(Banzhong)
            Else
                Me.AveTime.Text = 0
                Me.VarTime.Text = 0
                Me.MaxTime.Text = 0
                Me.MinTime.Text = 0
                Me.CSDriverNum.Text = 0
            End If
        End If

    End Sub


    Private Sub DataGrdStatQuery_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGrdStatQuery.CellClick
        If Me.DataGrdStatQuery.CurrentRow.Index > -1 Then
            CSTimeTablePara.nPubCheDi = Me.DataGrdStatQuery.CurrentRow.Cells("编号").Value.ToString()
            If CSTimeTablePara.nPubCheDi > 0 Then
                mainForm.SelectDriver(CSTimeTablePara.nPubCheDi)
                Call mainForm.ListCurDutyInfo()
            Else
                CSTimeTablePara.picPubDiagram.Refresh()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.TextBoxpx.Text.ToString() <> "" Then
            Dim s() As String = Me.TextBoxpx.Text.ToString.Split(";")
            Dim sortstring As String = s(0) & " desc "
            For i As Integer = 1 To s.Length - 1
                sortstring = sortstring & ", " & s(i) & " desc "

            Next
            Dim dv As New DataView(tabDriver)
            dv.Sort = sortstring
            Me.DataGrdStatQuery.DataSource = dv
        End If
    End Sub

    Private Sub 排序_Click(sender As Object, e As EventArgs) Handles 排序.Click
        Dim nf As New FrmSortColName
        nf.ShowDialog()
        Me.TextBoxpx.Text = nf.ziduan
    End Sub


    Private Sub DataGrdStatQuery_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGrdStatQuery.CellMouseDown
        If IsNothing(DataGrdStatQuery.Rows) Then
            Exit Sub
        End If
        For i As Integer = 0 To DataGrdStatQuery.Rows.Count - 1
            DataGrdStatQuery.Rows(i).Selected = False
        Next
        If e.Button = Windows.Forms.MouseButtons.Right And e.ColumnIndex > -1 And e.RowIndex > -1 Then
            DataGrdStatQuery.Rows(e.RowIndex).Selected = True
            ContextMenuStrip1.Show(MousePosition.X, MousePosition.Y)
        End If
       
    End Sub

    Private Sub 断开ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 断开ToolStripMenuItem.Click
        If IsNothing(DataGrdStatQuery.SelectedRows) = False Then
            If DataGrdStatQuery.SelectedRows.Count = 1 Then
                AddUnReDoInfo(True)
                Dim frm As New Frmdicon(DataGrdStatQuery.SelectedRows(0).Cells("编号").Value.ToString)
                frm.ShowDialog()
                mainForm.ListAllViewInfo()
                Dim preClass As String = StaSta.Text.Trim
                frmCSStaQuery_Load(sender, e)
                If preClass <> "全部" Then
                    StaSta.Text = preClass
                    StaSta_SelectedIndexChanged(sender, e)
                End If
            End If
        End If
    End Sub

    Private Sub 优化处理ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 优化处理ToolStripMenuItem.Click
        If IsNothing(DataGrdStatQuery.SelectedRows) = False Then
            If DataGrdStatQuery.SelectedRows.Count = 1 Then
                AddUnReDoInfo(True)
                Dim frm As New FrmOptiSchedule(DataGrdStatQuery.SelectedRows(0).Cells("编号").Value.ToString)
                frm.ShowDialog()
                mainForm.ListAllViewInfo()
                Dim preClass As String = StaSta.Text.Trim
                frmCSStaQuery_Load(sender, e)
                If preClass <> "全部" Then
                    StaSta.Text = preClass
                    StaSta_SelectedIndexChanged(sender, e)
                End If
            End If
        End If
    End Sub

    Private Sub 早班ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 早班ToolStripMenuItem.Click
        If IsNothing(DataGrdStatQuery.SelectedRows) = False Then
            If DataGrdStatQuery.SelectedRows.Count = 1 Then
                AddUnReDoInfo(True)
                If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
                    For Each Driver As CSDriver In CSTrainsAndDrivers.CSDrivers
                        If Driver IsNot Nothing AndAlso Driver.CSDriverID = DataGrdStatQuery.SelectedRows(0).Cells("编号").Value.ToString Then
                            If Driver.DutySort = "早班" Then
                                MsgBox("当前已是早班！")
                                Exit Sub
                            End If
                            Select Case Driver.DutySort
                                Case "白班"
                                    CSTrainsAndDrivers.DayDrivers.Remove(Driver)
                                Case "夜班"
                                    CSTrainsAndDrivers.NightDrivers.Remove(Driver)
                                Case "日勤班"
                                    CSTrainsAndDrivers.CDayDrivers.Remove(Driver)
                            End Select
                            Driver.DutySort = "早班"
                            Dim dutyno As Integer = CSTrainsAndDrivers.MorningDrivers.Count + 1
                            Dim haveone As Boolean = True
                            While haveone
                                haveone = False
                                For Each mordriver As CSDriver In CSTrainsAndDrivers.MorningDrivers
                                    If mordriver.CSdriverNo = "早班" & dutyno.ToString("00") Then
                                        haveone = True
                                        dutyno += 1
                                        Exit For
                                    End If
                                Next
                            End While
                            Driver.CSdriverNo = "早班" & dutyno.ToString("00")
                            Driver.OutPutCSdriverNo = "早班" & dutyno.ToString("00")
                            CSTrainsAndDrivers.MorningDrivers.Add(Driver)
                            Driver.RefreshState()
                            Call CSRefreshDiagram()
                            mainForm.ListAllViewInfo()
                            Dim preClass As String = StaSta.Text.Trim
                            frmCSStaQuery_Load(sender, e)
                            If preClass <> "全部" Then
                                StaSta.Text = preClass
                                StaSta_SelectedIndexChanged(sender, e)
                            End If
                           
                            Exit Sub
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub 白班ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 白班ToolStripMenuItem.Click
        If IsNothing(DataGrdStatQuery.SelectedRows) = False Then
            If DataGrdStatQuery.SelectedRows.Count = 1 Then
                AddUnReDoInfo(True)
                If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
                    For Each Driver As CSDriver In CSTrainsAndDrivers.CSDrivers
                        If Driver IsNot Nothing AndAlso Driver.CSDriverID = DataGrdStatQuery.SelectedRows(0).Cells("编号").Value.ToString Then
                            If Driver.DutySort = "白班" Then
                                MsgBox("当前已是白班！")
                                Exit Sub
                            End If
                            Select Case Driver.DutySort
                                Case "早班"
                                    CSTrainsAndDrivers.MorningDrivers.Remove(Driver)
                                Case "夜班"
                                    CSTrainsAndDrivers.NightDrivers.Remove(Driver)
                                Case "日勤班"
                                    CSTrainsAndDrivers.CDayDrivers.Remove(Driver)
                            End Select
                            Driver.DutySort = "白班"
                            Dim dutyno As Integer = CSTrainsAndDrivers.DayDrivers.Count + 1
                            Dim haveone As Boolean = True
                            While haveone
                                haveone = False
                                For Each mordriver As CSDriver In CSTrainsAndDrivers.DayDrivers
                                    If mordriver.CSdriverNo = "白班" & dutyno.ToString("00") Then
                                        haveone = True
                                        dutyno += 1
                                        Exit For
                                    End If
                                Next
                            End While
                            Driver.CSdriverNo = "白班" & dutyno.ToString("00")
                            Driver.OutPutCSdriverNo = "白班" & dutyno.ToString("00")
                            CSTrainsAndDrivers.DayDrivers.Add(Driver)
                            Driver.RefreshState()
                            Call CSRefreshDiagram()
                            mainForm.ListAllViewInfo()
                            Dim preClass As String = StaSta.Text.Trim
                            frmCSStaQuery_Load(sender, e)
                            If preClass <> "全部" Then
                                StaSta.Text = preClass
                                StaSta_SelectedIndexChanged(sender, e)
                            End If
                            Exit For
                        End If
                    Next
                End If

            End If
        End If
    End Sub

    Private Sub 夜班ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 夜班ToolStripMenuItem.Click
        If IsNothing(DataGrdStatQuery.SelectedRows) = False Then
            If DataGrdStatQuery.SelectedRows.Count = 1 Then
                AddUnReDoInfo(True)
                If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
                    For Each Driver As CSDriver In CSTrainsAndDrivers.CSDrivers
                        If Driver IsNot Nothing AndAlso Driver.CSDriverID = DataGrdStatQuery.SelectedRows(0).Cells("编号").Value.ToString Then
                            If Driver.DutySort = "夜班" Then
                                MsgBox("当前已是夜班！")
                                Exit Sub
                            End If
                            Select Case Driver.DutySort
                                Case "早班"
                                    CSTrainsAndDrivers.MorningDrivers.Remove(Driver)
                                Case "白班"
                                    CSTrainsAndDrivers.DayDrivers.Remove(Driver)
                                Case "日勤班"
                                    CSTrainsAndDrivers.CDayDrivers.Remove(Driver)
                            End Select
                            Driver.DutySort = "夜班"
                            Dim dutyno As Integer = CSTrainsAndDrivers.NightDrivers.Count + 1
                            Dim haveone As Boolean = True
                            While haveone
                                haveone = False
                                For Each mordriver As CSDriver In CSTrainsAndDrivers.NightDrivers
                                    If mordriver.CSdriverNo = "夜班" & dutyno.ToString("00") Then
                                        haveone = True
                                        dutyno += 1
                                        Exit For
                                    End If
                                Next
                            End While
                            Driver.CSdriverNo = "夜班" & dutyno.ToString("00")
                            Driver.OutPutCSdriverNo = "夜班" & dutyno.ToString("00")
                            CSTrainsAndDrivers.NightDrivers.Add(Driver)
                            Driver.RefreshState()
                            Call CSRefreshDiagram()
                            mainForm.ListAllViewInfo()
                            Dim preClass As String = StaSta.Text.Trim
                            frmCSStaQuery_Load(sender, e)
                            If preClass <> "全部" Then
                                StaSta.Text = preClass
                                StaSta_SelectedIndexChanged(sender, e)
                            End If
                            Exit For
                        End If
                    Next
                End If

            End If
        End If
    End Sub

    Private Sub 日勤班ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 日勤班ToolStripMenuItem.Click
        If IsNothing(DataGrdStatQuery.SelectedRows) = False Then
            If DataGrdStatQuery.SelectedRows.Count = 1 Then
                AddUnReDoInfo(True)
                If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
                    For Each Driver As CSDriver In CSTrainsAndDrivers.CSDrivers
                        If Driver IsNot Nothing AndAlso Driver.CSDriverID = DataGrdStatQuery.SelectedRows(0).Cells("编号").Value.ToString Then
                            If Driver.DutySort = "日勤班" Then
                                MsgBox("当前已是日勤班！")
                                Exit Sub
                            End If
                            Select Case Driver.DutySort
                                Case "早班"
                                    CSTrainsAndDrivers.MorningDrivers.Remove(Driver)
                                Case "白班"
                                    CSTrainsAndDrivers.DayDrivers.Remove(Driver)
                                Case "夜班"
                                    CSTrainsAndDrivers.NightDrivers.Remove(Driver)
                            End Select
                            Driver.DutySort = "日勤班"
                            Dim dutyno As Integer = CSTrainsAndDrivers.CDayDrivers.Count + 1
                            Dim haveone As Boolean = True
                            While haveone
                                haveone = False
                                For Each mordriver As CSDriver In CSTrainsAndDrivers.CDayDrivers
                                    If mordriver.CSdriverNo = "日勤班" & dutyno.ToString("00") Then
                                        haveone = True
                                        dutyno += 1
                                        Exit For
                                    End If
                                Next
                            End While
                            Driver.CSdriverNo = "日勤班" & dutyno.ToString("00")
                            Driver.OutPutCSdriverNo = "日勤班" & dutyno.ToString("00")
                            CSTrainsAndDrivers.CDayDrivers.Add(Driver)
                            Driver.RefreshState()
                            Call CSRefreshDiagram()
                            mainForm.ListAllViewInfo()
                            Dim preClass As String = StaSta.Text.Trim
                            frmCSStaQuery_Load(sender, e)
                            If preClass <> "全部" Then
                                StaSta.Text = preClass
                                StaSta_SelectedIndexChanged(sender, e)
                            End If
                            Exit For
                        End If
                    Next
                End If

            End If
        End If
    End Sub
End Class