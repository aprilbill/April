Imports System.IO

Public Class FrmCorNitMor

    Public cmbDri As ComboBox
    Private Property CurMousex As Integer
    Private Property CurMousey As Integer
    Private Property CurCellx As Integer
    Private Property CurCelly As Integer
    Public Enum CorStyle
        自身夜早班衔接 = 1
        自身夜班被动衔接 = 2
        自身早班主动衔接 = 3
    End Enum
    Public tCorStyle As CorStyle

    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
        Me.Close()
    End Sub

    Private Sub FrmCorNitMor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbDri = New ComboBox
        AddHandler cmbDri.TextChanged, AddressOf cmbDriValueChanged
        Select Case tCorStyle
            Case CorStyle.自身夜早班衔接
                Call LoadMainCorInfo()
            Case CorStyle.自身夜班被动衔接
                Call LoadNegtiveCorInfo()
            Case CorStyle.自身早班主动衔接
                Call LoadPositiveCorInfo()
        End Select
        Call RefreshUnAssignInfo()
    End Sub

    Private Sub DGVMain_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVMain.CellClick
        If sender.SelectedCells.count = 1 Then
            Select Case sender.Columns(sender.SelectedCells(0).ColumnIndex).Name
                Case "早班任务"
                    Call RefreshCmbItems()
                    sender.Controls.Add(Me.cmbDri)
                    Me.cmbDri.Size = sender.SelectedCells(0).Size
                    Me.cmbDri.Location = New Point(CurMousex - CurCellx + sender.Left, CurMousey - CurCelly + sender.Top)
                    Me.cmbDri.Text = sender.SelectedCells(0).value.ToString().Trim
                    Me.cmbDri.Visible = True
            End Select
        End If
    End Sub

    Private Sub DGVMain_CellMouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGVMain.CellMouseDown
        cmbDri.Visible = False
        CurCellx = e.X
        CurCelly = e.Y
    End Sub

    Private Sub DGVMain_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DGVMain.MouseDown
        CurMousex = e.X
        CurMousey = e.Y
    End Sub

    Public Sub LoadMainCorInfo()
        Me.DGVMain.Rows.Clear()
        Dim ncount As Integer = 1
        For Each tempDri As CSDriver In CSTrainsAndDrivers.NightDrivers
            If tempDri.LinkedMorDriver IsNot Nothing AndAlso tempDri.LinkedMorDriver.CSdriverNo <> "#" Then
                Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.BeginWorkTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.WorkTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                    tempDri.LinkedMorDriver.CSdriverNo, tempDri.LinkedMorDriver.OutPutCSdriverNo, BeTime(tempDri.LinkedMorDriver.BeginWorkTime), BeTime(tempDri.LinkedMorDriver.EndEorkTime), tempDri.LinkedMorDriver.DriveDistance, BeTime(tempDri.LinkedMorDriver.WorkTime), _
                                    tempDri.LinkedMorDriver.CSLinkTrain(1).StartStaName, tempDri.LinkedMorDriver.CSLinkTrain(UBound(tempDri.LinkedMorDriver.CSLinkTrain)).EndStaName, BeTime(tempDri.WorkTime + tempDri.LinkedMorDriver.WorkTime), tempDri.DriveDistance + tempDri.LinkedMorDriver.DriveDistance)
                ncount += 1
            Else
                Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.BeginWorkTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.WorkTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                    "", "", "", "", "", "", "", "", BeTime(tempDri.WorkTime), tempDri.DriveDistance)
                For i As Integer = 0 To Me.DGVMain.ColumnCount - 1
                    Me.DGVMain.Rows(ncount - 1).Cells(i).Style.BackColor = Color.Red
                Next
                ncount += 1
            End If
        Next
        For Each tempDri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
            If tempDri.DutySort = "夜班" Then
                If tempDri.LinkedMorDriver IsNot Nothing AndAlso tempDri.LinkedMorDriver.CSdriverNo <> "#" Then
                    Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.BeginWorkTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.WorkTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                    tempDri.LinkedMorDriver.CSdriverNo, tempDri.LinkedMorDriver.OutPutCSdriverNo, BeTime(tempDri.LinkedMorDriver.BeginWorkTime), BeTime(tempDri.LinkedMorDriver.EndEorkTime), tempDri.LinkedMorDriver.DriveDistance, BeTime(tempDri.LinkedMorDriver.WorkTime), _
                                    tempDri.LinkedMorDriver.CSLinkTrain(1).StartStaName, tempDri.LinkedMorDriver.CSLinkTrain(UBound(tempDri.LinkedMorDriver.CSLinkTrain)).EndStaName, BeTime(tempDri.WorkTime + tempDri.LinkedMorDriver.WorkTime), tempDri.DriveDistance + tempDri.LinkedMorDriver.DriveDistance)
                    ncount += 1
                Else
                    Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.BeginWorkTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.WorkTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                    "", "", "", "", "", "", "", "", BeTime(tempDri.WorkTime), tempDri.DriveDistance)
                    For i As Integer = 0 To Me.DGVMain.ColumnCount - 1
                        Me.DGVMain.Rows(ncount - 1).Cells(i).Style.BackColor = Color.Red
                    Next
                    ncount += 1
                End If
            End If
        Next
        For Each tempDri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
            If tempDri.DutySort = "夜班" Then
                If tempDri.LinkedMorDriver IsNot Nothing AndAlso tempDri.LinkedMorDriver.CSdriverNo <> "#" Then
                    Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.BeginWorkTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.WorkTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                    tempDri.LinkedMorDriver.CSdriverNo, tempDri.LinkedMorDriver.OutPutCSdriverNo, BeTime(tempDri.LinkedMorDriver.BeginWorkTime), BeTime(tempDri.LinkedMorDriver.EndEorkTime), tempDri.LinkedMorDriver.DriveDistance, BeTime(tempDri.LinkedMorDriver.WorkTime), _
                                    tempDri.LinkedMorDriver.CSLinkTrain(1).StartStaName, tempDri.LinkedMorDriver.CSLinkTrain(UBound(tempDri.LinkedMorDriver.CSLinkTrain)).EndStaName, BeTime(tempDri.WorkTime + tempDri.LinkedMorDriver.WorkTime), tempDri.DriveDistance + tempDri.LinkedMorDriver.DriveDistance)
                    ncount += 1
                Else
                    Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.BeginWorkTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.WorkTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                    "", "", "", "", "", "", "", "", BeTime(tempDri.WorkTime), tempDri.DriveDistance)
                    For i As Integer = 0 To Me.DGVMain.ColumnCount - 1
                        Me.DGVMain.Rows(ncount - 1).Cells(i).Style.BackColor = Color.Red
                    Next
                    ncount += 1
                End If
            End If
        Next
    End Sub

    Public Sub LoadPositiveCorInfo()
        Me.DGVMain.Rows.Clear()
        Dim ncount As Integer = 1
        For Each tempDri As CSDriver In CSTrainsAndDrivers.CorNightDrivers
            Dim Plan As CorCSPlan = Nothing
            For Each p As CorCSPlan In CSTrainsAndDrivers.PostiveCorCSPlans
                If p.NightDriver Is tempDri Then
                    Plan = p
                    Exit For
                End If
            Next
            If Plan IsNot Nothing Then
                Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.BeginWorkTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.WorkTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                    Plan.MorningDriver.CSdriverNo, Plan.MorningDriver.OutPutCSdriverNo, BeTime(Plan.MorningDriver.BeginWorkTime), BeTime(Plan.MorningDriver.EndEorkTime), Plan.MorningDriver.DriveDistance, BeTime(Plan.MorningDriver.WorkTime), _
                                    Plan.MorningDriver.CSLinkTrain(1).StartStaName, Plan.MorningDriver.CSLinkTrain(UBound(Plan.MorningDriver.CSLinkTrain)).EndStaName, BeTime(tempDri.EndEorkTime - tempDri.CSLinkTrain(1).StartTime + Plan.MorningDriver.WorkTime), tempDri.DriveDistance + Plan.MorningDriver.DriveDistance)
                ncount += 1
            Else
                Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.CSLinkTrain(1).StartTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.EndEorkTime - tempDri.CSLinkTrain(1).StartTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                    "", "", "", "", "", "", "", "", BeTime(tempDri.EndEorkTime - tempDri.CSLinkTrain(1).StartTime), tempDri.DriveDistance)
                For i As Integer = 0 To Me.DGVMain.ColumnCount - 1
                    Me.DGVMain.Rows(ncount - 1).Cells(i).Style.BackColor = Color.Red
                Next
                ncount += 1
            End If
        Next
        For Each tempDri As CSDriver In CSTrainsAndDrivers.CorPreParedDutyDrivers
            If tempDri.DutySort = "夜班" Then
                Dim Plan As CorCSPlan = Nothing
                For Each p As CorCSPlan In CSTrainsAndDrivers.PostiveCorCSPlans
                    If p.NightDriver Is tempDri Then
                        Plan = p
                        Exit For
                    End If
                Next
                If Plan IsNot Nothing Then
                    Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.CSLinkTrain(1).StartTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.EndEorkTime - tempDri.CSLinkTrain(1).StartTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                    Plan.MorningDriver.CSdriverNo, Plan.MorningDriver.OutPutCSdriverNo, BeTime(Plan.MorningDriver.BeginWorkTime), BeTime(Plan.MorningDriver.EndEorkTime), Plan.MorningDriver.DriveDistance, BeTime(Plan.MorningDriver.WorkTime), _
                                    Plan.MorningDriver.CSLinkTrain(1).StartStaName, Plan.MorningDriver.CSLinkTrain(UBound(Plan.MorningDriver.CSLinkTrain)).EndStaName, BeTime(tempDri.EndEorkTime - tempDri.CSLinkTrain(1).StartTime + Plan.MorningDriver.WorkTime), tempDri.DriveDistance + Plan.MorningDriver.DriveDistance)
                    ncount += 1
                Else
                    Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.CSLinkTrain(1).StartTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.EndEorkTime - tempDri.CSLinkTrain(1).StartTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                        "", "", "", "", "", "", "", "", BeTime(tempDri.EndEorkTime - tempDri.CSLinkTrain(1).StartTime), tempDri.DriveDistance)
                    For i As Integer = 0 To Me.DGVMain.ColumnCount - 1
                        Me.DGVMain.Rows(ncount - 1).Cells(i).Style.BackColor = Color.Red
                    Next
                    ncount += 1
                End If
            End If
        Next
        For Each tempDri As CSDriver In CSTrainsAndDrivers.CorPreParedTrainDrivers
            If tempDri.DutySort = "夜班" Then
                Dim Plan As CorCSPlan = Nothing
                For Each p As CorCSPlan In CSTrainsAndDrivers.PostiveCorCSPlans
                    If p.NightDriver Is tempDri Then
                        Plan = p
                        Exit For
                    End If
                Next
                If Plan IsNot Nothing Then
                    Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.CSLinkTrain(1).StartTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.EndEorkTime - tempDri.CSLinkTrain(1).StartTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                    Plan.MorningDriver.CSdriverNo, Plan.MorningDriver.OutPutCSdriverNo, BeTime(Plan.MorningDriver.BeginWorkTime), BeTime(Plan.MorningDriver.EndEorkTime), Plan.MorningDriver.DriveDistance, BeTime(Plan.MorningDriver.WorkTime), _
                                    Plan.MorningDriver.CSLinkTrain(1).StartStaName, Plan.MorningDriver.CSLinkTrain(UBound(Plan.MorningDriver.CSLinkTrain)).EndStaName, BeTime(tempDri.EndEorkTime - tempDri.CSLinkTrain(1).StartTime + Plan.MorningDriver.WorkTime), tempDri.DriveDistance + Plan.MorningDriver.DriveDistance)
                    ncount += 1
                Else
                    Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.CSLinkTrain(1).StartTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.EndEorkTime - tempDri.CSLinkTrain(1).StartTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                        "", "", "", "", "", "", "", "", BeTime(tempDri.EndEorkTime - tempDri.CSLinkTrain(1).StartTime), tempDri.DriveDistance)
                    For i As Integer = 0 To Me.DGVMain.ColumnCount - 1
                        Me.DGVMain.Rows(ncount - 1).Cells(i).Style.BackColor = Color.Red
                    Next
                    ncount += 1
                End If
            End If
        Next
    End Sub

    Public Sub LoadNegtiveCorInfo()
        Me.DGVMain.Rows.Clear()
        Dim ncount As Integer = 1
        For Each tempDri As CSDriver In CSTrainsAndDrivers.NightDrivers
            Dim Plan As CorCSPlan = Nothing
            For Each p As CorCSPlan In CSTrainsAndDrivers.NegtiveCorCSPlans
                If p.NightDriver Is tempDri Then
                    Plan = p
                    Exit For
                End If
            Next
            If Plan IsNot Nothing Then
                Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.BeginWorkTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.WorkTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                    Plan.MorningDriver.CSdriverNo, Plan.MorningDriver.OutPutCSdriverNo, BeTime(Plan.MorningDriver.CSLinkTrain(1).StartTime), BeTime(Plan.MorningDriver.EndEorkTime), Plan.MorningDriver.DriveDistance, BeTime(Plan.MorningDriver.EndEorkTime - Plan.MorningDriver.CSLinkTrain(1).StartTime), _
                                    Plan.MorningDriver.CSLinkTrain(1).StartStaName, Plan.MorningDriver.CSLinkTrain(UBound(Plan.MorningDriver.CSLinkTrain)).EndStaName, BeTime(tempDri.WorkTime + Plan.MorningDriver.EndEorkTime - Plan.MorningDriver.CSLinkTrain(1).StartTime), tempDri.DriveDistance + Plan.MorningDriver.DriveDistance)
                ncount += 1
            Else
                Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.BeginWorkTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.WorkTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                    "", "", "", "", "", "", "", "", BeTime(tempDri.WorkTime), tempDri.DriveDistance)
                For i As Integer = 0 To Me.DGVMain.ColumnCount - 1
                    Me.DGVMain.Rows(ncount - 1).Cells(i).Style.BackColor = Color.Red
                Next
                ncount += 1
            End If
        Next
        For Each tempDri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
            If tempDri.DutySort = "夜班" Then
                Dim Plan As CorCSPlan = Nothing
                For Each p As CorCSPlan In CSTrainsAndDrivers.NegtiveCorCSPlans
                    If p.NightDriver Is tempDri Then
                        Plan = p
                        Exit For
                    End If
                Next
                If Plan IsNot Nothing Then
                    Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.BeginWorkTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.WorkTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                    Plan.MorningDriver.CSdriverNo, Plan.MorningDriver.OutPutCSdriverNo, BeTime(Plan.MorningDriver.CSLinkTrain(1).StartTime), BeTime(Plan.MorningDriver.EndEorkTime), Plan.MorningDriver.DriveDistance, BeTime(Plan.MorningDriver.EndEorkTime - Plan.MorningDriver.CSLinkTrain(1).StartTime), _
                                    Plan.MorningDriver.CSLinkTrain(1).StartStaName, Plan.MorningDriver.CSLinkTrain(UBound(Plan.MorningDriver.CSLinkTrain)).EndStaName, BeTime(tempDri.WorkTime + Plan.MorningDriver.EndEorkTime - Plan.MorningDriver.CSLinkTrain(1).StartTime), tempDri.DriveDistance + Plan.MorningDriver.DriveDistance)
                    ncount += 1
                Else
                    Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.BeginWorkTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.WorkTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                        "", "", "", "", "", "", "", "", BeTime(tempDri.WorkTime), tempDri.DriveDistance)
                    For i As Integer = 0 To Me.DGVMain.ColumnCount - 1
                        Me.DGVMain.Rows(ncount - 1).Cells(i).Style.BackColor = Color.Red
                    Next
                    ncount += 1
                End If
            End If
        Next
        For Each tempDri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
            If tempDri.DutySort = "夜班" Then
                Dim Plan As CorCSPlan = Nothing
                For Each p As CorCSPlan In CSTrainsAndDrivers.NegtiveCorCSPlans
                    If p.NightDriver Is tempDri Then
                        Plan = p
                        Exit For
                    End If
                Next
                If Plan IsNot Nothing Then
                    Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.BeginWorkTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.WorkTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                    Plan.MorningDriver.CSdriverNo, Plan.MorningDriver.OutPutCSdriverNo, BeTime(Plan.MorningDriver.CSLinkTrain(1).StartTime), BeTime(Plan.MorningDriver.EndEorkTime), Plan.MorningDriver.DriveDistance, BeTime(Plan.MorningDriver.EndEorkTime - Plan.MorningDriver.CSLinkTrain(1).StartTime), _
                                    Plan.MorningDriver.CSLinkTrain(1).StartStaName, Plan.MorningDriver.CSLinkTrain(UBound(Plan.MorningDriver.CSLinkTrain)).EndStaName, BeTime(tempDri.WorkTime + Plan.MorningDriver.EndEorkTime - Plan.MorningDriver.CSLinkTrain(1).StartTime), tempDri.DriveDistance + Plan.MorningDriver.DriveDistance)
                    ncount += 1
                Else
                    Me.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, BeTime(tempDri.BeginWorkTime), BeTime(tempDri.EndEorkTime), tempDri.DriveDistance, BeTime(tempDri.WorkTime), tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                        "", "", "", "", "", "", "", "", BeTime(tempDri.WorkTime), tempDri.DriveDistance)
                    For i As Integer = 0 To Me.DGVMain.ColumnCount - 1
                        Me.DGVMain.Rows(ncount - 1).Cells(i).Style.BackColor = Color.Red
                    Next
                    ncount += 1
                End If
            End If
        Next
    End Sub

    Public Sub cmbDriValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If cmbDri.Text.Trim = "清除" Then
            Me.DGVMain.CurrentCell.Value = ""
        Else
            Me.DGVMain.CurrentCell.Value = cmbDri.Text.Trim
        End If
        Select Case tCorStyle
            Case CorStyle.自身夜早班衔接
                If Me.DGVMain.CurrentCell.Value.ToString = "" Then
                    Dim NitDriverNo As String = Me.DGVMain.Rows(Me.DGVMain.CurrentCell.RowIndex).Cells("夜班任务").Value.ToString.Trim
                    Dim dri As CSDriver = CSTrainsAndDrivers.NightDrivers.Find(Function(value As CSDriver)
                                                                                   Return (value.CSdriverNo = NitDriverNo)
                                                                               End Function)
                    If dri Is Nothing Then
                        dri = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                              Return (value.CSdriverNo = NitDriverNo AndAlso value.DutySort = "夜班")
                                                                          End Function)
                        If dri Is Nothing Then
                            dri = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                   Return (value.CSdriverNo = NitDriverNo AndAlso value.DutySort = "夜班")
                                                                               End Function)
                        End If
                    End If
                    If dri IsNot Nothing Then
                        If dri.LinkedMorDriver IsNot Nothing Then
                            dri.LinkedMorDriver.LinkedNightDriver = Nothing
                            dri.LinkedMorDriver = Nothing
                        End If
                        Call UpdateDriverDGVInfo(dri, Me.DGVMain.CurrentCell.RowIndex, Me.DGVMain)
                    End If
                Else
                    Dim NitDriverNo As String = Me.DGVMain.Rows(Me.DGVMain.CurrentCell.RowIndex).Cells("夜班任务").Value.ToString.Trim
                    Dim dri As CSDriver = CSTrainsAndDrivers.NightDrivers.Find(Function(value As CSDriver)
                                                                                   Return (value.CSdriverNo = NitDriverNo)
                                                                               End Function)
                    If dri Is Nothing Then
                        dri = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                              Return (value.CSdriverNo = NitDriverNo AndAlso value.DutySort = "夜班")
                                                                          End Function)
                        If dri Is Nothing Then
                            dri = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                   Return (value.CSdriverNo = NitDriverNo AndAlso value.DutySort = "夜班")
                                                                               End Function)
                        End If
                    End If
                    If dri IsNot Nothing Then
                        If dri.LinkedMorDriver IsNot Nothing Then
                            dri.LinkedMorDriver.LinkedNightDriver = Nothing
                        End If
                        Dim MorDriverNo As String = cmbDri.Text.Trim
                        Dim Mordri As CSDriver = CSTrainsAndDrivers.MorningDrivers.Find(Function(value As CSDriver)
                                                                                            Return (value.CSdriverNo = MorDriverNo)
                                                                                        End Function)
                        If Mordri Is Nothing Then
                            Mordri = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                                     Return (value.CSdriverNo = MorDriverNo AndAlso value.DutySort = "早班")
                                                                                 End Function)
                            If Mordri Is Nothing Then
                                Mordri = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                          Return (value.CSdriverNo = MorDriverNo AndAlso value.DutySort = "早班")
                                                                                      End Function)
                            End If
                        End If
                        If Mordri IsNot Nothing Then
                            dri.LinkedMorDriver = Mordri
                            Mordri.LinkedNightDriver = dri
                        End If
                        Call UpdateDriverDGVInfo(dri, Me.DGVMain.CurrentCell.RowIndex, Me.DGVMain)
                        If dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName <> Mordri.CSLinkTrain(1).StartStaName Then
                            MsgBox("出退勤地点不一致!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                        End If
                    End If
                End If
            Case CorStyle.自身夜班被动衔接
                If Me.DGVMain.CurrentCell.Value.ToString = "" Then
                    Dim NitDriverNo As String = Me.DGVMain.Rows(Me.DGVMain.CurrentCell.RowIndex).Cells("夜班任务").Value.ToString.Trim
                    Dim dri As CSDriver = CSTrainsAndDrivers.NightDrivers.Find(Function(value As CSDriver)
                                                                                   Return (value.CSdriverNo = NitDriverNo)
                                                                               End Function)
                    If dri Is Nothing Then
                        dri = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                              Return (value.CSdriverNo = NitDriverNo AndAlso value.DutySort = "夜班")
                                                                          End Function)
                        If dri Is Nothing Then
                            dri = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                   Return (value.CSdriverNo = NitDriverNo AndAlso value.DutySort = "夜班")
                                                                               End Function)
                        End If
                    End If
                    If dri IsNot Nothing Then
                        Dim Plan As CorCSPlan = CSTrainsAndDrivers.NegtiveCorCSPlans.Find(Function(value As CorCSPlan)
                                                                                              Return value.NightDriver Is dri
                                                                                          End Function)
                        If Plan IsNot Nothing Then
                            CSTrainsAndDrivers.NegtiveCorCSPlans.Remove(Plan)
                        End If
                    End If
                    Call UpdateDriverDGVInfo(dri, Me.DGVMain.CurrentCell.RowIndex, Me.DGVMain)
                Else
                    Dim NitDriverNo As String = Me.DGVMain.Rows(Me.DGVMain.CurrentCell.RowIndex).Cells("夜班任务").Value.ToString.Trim
                    Dim dri As CSDriver = CSTrainsAndDrivers.NightDrivers.Find(Function(value As CSDriver)
                                                                                   Return (value.CSdriverNo = NitDriverNo)
                                                                               End Function)
                    If dri Is Nothing Then
                        dri = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                              Return (value.CSdriverNo = NitDriverNo AndAlso value.DutySort = "夜班")
                                                                          End Function)
                        If dri Is Nothing Then
                            dri = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                   Return (value.CSdriverNo = NitDriverNo AndAlso value.DutySort = "夜班")
                                                                               End Function)
                        End If
                    End If
                    If dri IsNot Nothing Then
                        Dim MorDriverNo As String = cmbDri.Text.Trim
                        Dim Mordri As CSDriver = CSTrainsAndDrivers.CorMorningDrivers.Find(Function(value As CSDriver)
                                                                                               Return (value.CSdriverNo = MorDriverNo)
                                                                                           End Function)
                        If Mordri Is Nothing Then
                            Mordri = CSTrainsAndDrivers.CorPreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                                        Return (value.CSdriverNo = MorDriverNo AndAlso value.DutySort = "早班")
                                                                                    End Function)
                            If Mordri Is Nothing Then
                                Mordri = CSTrainsAndDrivers.CorPreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                             Return (value.CSdriverNo = MorDriverNo AndAlso value.DutySort = "早班")
                                                                                         End Function)
                            End If
                        End If
                        If Mordri IsNot Nothing Then
                            Dim Plan As CorCSPlan = CSTrainsAndDrivers.NegtiveCorCSPlans.Find(Function(value As CorCSPlan)
                                                                                                  Return value.NightDriver Is dri
                                                                                              End Function)
                            If Plan IsNot Nothing Then
                                Plan.MorningDriver = Mordri
                            Else
                                Dim tmpPlan As New CorCSPlan(dri, Mordri)
                                CSTrainsAndDrivers.NegtiveCorCSPlans.Add(tmpPlan)
                            End If
                        End If
                        Call UpdateDriverDGVInfo(dri, Me.DGVMain.CurrentCell.RowIndex, Me.DGVMain)
                        If dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName <> Mordri.CSLinkTrain(1).StartStaName Then
                            MsgBox("出退勤地点不一致!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                        End If
                    End If
                End If
            Case CorStyle.自身早班主动衔接
                If Me.DGVMain.CurrentCell.Value.ToString = "" Then
                    Dim NitDriverNo As String = Me.DGVMain.Rows(Me.DGVMain.CurrentCell.RowIndex).Cells("夜班任务").Value.ToString.Trim
                    Dim dri As CSDriver = CSTrainsAndDrivers.CorNightDrivers.Find(Function(value As CSDriver)
                                                                                      Return (value.CSdriverNo = NitDriverNo)
                                                                                  End Function)
                    If dri Is Nothing Then
                        dri = CSTrainsAndDrivers.CorPreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                                 Return (value.CSdriverNo = NitDriverNo AndAlso value.DutySort = "夜班")
                                                                             End Function)
                        If dri Is Nothing Then
                            dri = CSTrainsAndDrivers.CorPreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                      Return (value.CSdriverNo = NitDriverNo AndAlso value.DutySort = "夜班")
                                                                                  End Function)
                        End If
                    End If
                    If dri IsNot Nothing Then
                        Dim Plan As CorCSPlan = CSTrainsAndDrivers.PostiveCorCSPlans.Find(Function(value As CorCSPlan)
                                                                                              Return value.NightDriver Is dri
                                                                                          End Function)
                        If Plan IsNot Nothing Then
                            CSTrainsAndDrivers.PostiveCorCSPlans.Remove(Plan)
                        End If
                    End If
                    Call UpdateDriverDGVInfo(dri, Me.DGVMain.CurrentCell.RowIndex, Me.DGVMain)
                Else
                    Dim NitDriverNo As String = Me.DGVMain.Rows(Me.DGVMain.CurrentCell.RowIndex).Cells("夜班任务").Value.ToString.Trim
                    Dim dri As CSDriver = CSTrainsAndDrivers.CorNightDrivers.Find(Function(value As CSDriver)
                                                                                      Return (value.CSdriverNo = NitDriverNo)
                                                                                  End Function)
                    If dri Is Nothing Then
                        dri = CSTrainsAndDrivers.CorPreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                                 Return (value.CSdriverNo = NitDriverNo AndAlso value.DutySort = "夜班")
                                                                             End Function)
                        If dri Is Nothing Then
                            dri = CSTrainsAndDrivers.CorPreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                      Return (value.CSdriverNo = NitDriverNo AndAlso value.DutySort = "夜班")
                                                                                  End Function)
                        End If
                    End If
                    If dri IsNot Nothing Then
                        Dim MorDriverNo As String = cmbDri.Text.Trim
                        Dim Mordri As CSDriver = CSTrainsAndDrivers.MorningDrivers.Find(Function(value As CSDriver)
                                                                                            Return (value.CSdriverNo = MorDriverNo)
                                                                                        End Function)
                        If Mordri Is Nothing Then
                            Mordri = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                                     Return (value.CSdriverNo = MorDriverNo AndAlso value.DutySort = "早班")
                                                                                 End Function)
                            If Mordri Is Nothing Then
                                Mordri = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                          Return (value.CSdriverNo = MorDriverNo AndAlso value.DutySort = "早班")
                                                                                      End Function)
                            End If
                        End If
                        If Mordri IsNot Nothing Then
                            Dim Plan As CorCSPlan = CSTrainsAndDrivers.PostiveCorCSPlans.Find(Function(value As CorCSPlan)
                                                                                                  Return value.NightDriver Is dri
                                                                                              End Function)
                            If Plan IsNot Nothing Then
                                Plan.MorningDriver = Mordri
                            Else
                                Dim tmpPlan As New CorCSPlan(dri, Mordri)
                                CSTrainsAndDrivers.PostiveCorCSPlans.Add(tmpPlan)
                            End If
                        End If
                        Call UpdateDriverDGVInfo(dri, Me.DGVMain.CurrentCell.RowIndex, Me.DGVMain)
                        If dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName <> Mordri.CSLinkTrain(1).StartStaName Then
                            MsgBox("出退勤地点不一致!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                        End If
                    End If
                End If
        End Select
        Call RefreshUnAssignInfo()
        Me.cmbDri.Visible = False
    End Sub

    Public Sub UpdateDriverDGVInfo(ByVal Dri As CSDriver, ByVal RowIndex As Integer, ByVal DGV As DataGridView)
        Select Case tCorStyle
            Case CorStyle.自身夜早班衔接
                If Dri.LinkedMorDriver IsNot Nothing AndAlso Dri.LinkedMorDriver.CSdriverNo <> "#" Then
                    Me.DGVMain.Rows(RowIndex).Cells(9).Value = Dri.LinkedMorDriver.CSdriverNo
                    Me.DGVMain.Rows(RowIndex).Cells(10).Value = Dri.LinkedMorDriver.OutPutCSdriverNo
                    Me.DGVMain.Rows(RowIndex).Cells(11).Value = BeTime(Dri.LinkedMorDriver.BeginWorkTime)
                    Me.DGVMain.Rows(RowIndex).Cells(12).Value = BeTime(Dri.LinkedMorDriver.EndEorkTime)
                    Me.DGVMain.Rows(RowIndex).Cells(13).Value = Dri.LinkedMorDriver.DriveDistance
                    Me.DGVMain.Rows(RowIndex).Cells(14).Value = BeTime(Dri.LinkedMorDriver.WorkTime)
                    Me.DGVMain.Rows(RowIndex).Cells(15).Value = Dri.LinkedMorDriver.CSLinkTrain(1).StartStaName
                    Me.DGVMain.Rows(RowIndex).Cells(16).Value = Dri.LinkedMorDriver.CSLinkTrain(UBound(Dri.LinkedMorDriver.CSLinkTrain)).EndStaName
                    Me.DGVMain.Rows(RowIndex).Cells(17).Value = BeTime(Dri.WorkTime + Dri.LinkedMorDriver.WorkTime)
                    Me.DGVMain.Rows(RowIndex).Cells(18).Value = Dri.DriveDistance + Dri.LinkedMorDriver.DriveDistance
                    For i As Integer = 0 To Me.DGVMain.ColumnCount - 1
                        Me.DGVMain.Rows(RowIndex).Cells(i).Style.BackColor = Me.DGVMain.Columns(i).DefaultCellStyle.BackColor
                    Next
                Else
                    Me.DGVMain.Rows(RowIndex).Cells(9).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(10).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(11).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(12).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(13).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(14).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(15).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(16).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(17).Value = BeTime(Dri.WorkTime)
                    Me.DGVMain.Rows(RowIndex).Cells(18).Value = Dri.DriveDistance
                    For i As Integer = 0 To Me.DGVMain.ColumnCount - 1
                        Me.DGVMain.Rows(RowIndex).Cells(i).Style.BackColor = Color.Red
                    Next
                End If
            Case CorStyle.自身夜班被动衔接
                Dim Plan As CorCSPlan = CSTrainsAndDrivers.NegtiveCorCSPlans.Find(Function(value As CorCSPlan)
                                                                                      Return value.NightDriver Is Dri
                                                                                  End Function)
                If Plan IsNot Nothing Then
                    Me.DGVMain.Rows(RowIndex).Cells(9).Value = Plan.MorningDriver.CSdriverNo
                    Me.DGVMain.Rows(RowIndex).Cells(10).Value = Plan.MorningDriver.OutPutCSdriverNo
                    Me.DGVMain.Rows(RowIndex).Cells(11).Value = BeTime(Plan.MorningDriver.CSLinkTrain(1).StartTime)
                    Me.DGVMain.Rows(RowIndex).Cells(12).Value = BeTime(Plan.MorningDriver.EndEorkTime)
                    Me.DGVMain.Rows(RowIndex).Cells(13).Value = Plan.MorningDriver.DriveDistance
                    Me.DGVMain.Rows(RowIndex).Cells(14).Value = BeTime(Plan.MorningDriver.EndEorkTime - Plan.MorningDriver.CSLinkTrain(1).StartTime)
                    Me.DGVMain.Rows(RowIndex).Cells(15).Value = Plan.MorningDriver.CSLinkTrain(1).StartStaName
                    Me.DGVMain.Rows(RowIndex).Cells(16).Value = Plan.MorningDriver.CSLinkTrain(UBound(Plan.MorningDriver.CSLinkTrain)).EndStaName
                    Me.DGVMain.Rows(RowIndex).Cells(17).Value = BeTime(Dri.WorkTime + Plan.MorningDriver.EndEorkTime - Plan.MorningDriver.CSLinkTrain(1).StartTime)
                    Me.DGVMain.Rows(RowIndex).Cells(18).Value = Dri.DriveDistance + Plan.MorningDriver.DriveDistance
                    For i As Integer = 0 To Me.DGVMain.ColumnCount - 1
                        Me.DGVMain.Rows(RowIndex).Cells(i).Style.BackColor = Me.DGVMain.Columns(i).DefaultCellStyle.BackColor
                    Next
                Else
                    Me.DGVMain.Rows(RowIndex).Cells(9).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(10).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(11).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(12).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(13).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(14).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(15).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(16).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(17).Value = BeTime(Dri.WorkTime)
                    Me.DGVMain.Rows(RowIndex).Cells(18).Value = Dri.DriveDistance
                    For i As Integer = 0 To Me.DGVMain.ColumnCount - 1
                        Me.DGVMain.Rows(RowIndex).Cells(i).Style.BackColor = Color.Red
                    Next
                End If
            Case CorStyle.自身早班主动衔接
                Dim Plan As CorCSPlan = CSTrainsAndDrivers.PostiveCorCSPlans.Find(Function(value As CorCSPlan)
                                                                                      Return value.NightDriver Is Dri
                                                                                  End Function)
                If Plan IsNot Nothing Then
                    Me.DGVMain.Rows(RowIndex).Cells(9).Value = Plan.MorningDriver.CSdriverNo
                    Me.DGVMain.Rows(RowIndex).Cells(10).Value = Plan.MorningDriver.OutPutCSdriverNo
                    Me.DGVMain.Rows(RowIndex).Cells(11).Value = BeTime(Plan.MorningDriver.BeginWorkTime)
                    Me.DGVMain.Rows(RowIndex).Cells(12).Value = BeTime(Plan.MorningDriver.EndEorkTime)
                    Me.DGVMain.Rows(RowIndex).Cells(13).Value = Plan.MorningDriver.DriveDistance
                    Me.DGVMain.Rows(RowIndex).Cells(14).Value = BeTime(Plan.MorningDriver.WorkTime)
                    Me.DGVMain.Rows(RowIndex).Cells(15).Value = Plan.MorningDriver.CSLinkTrain(1).StartStaName
                    Me.DGVMain.Rows(RowIndex).Cells(16).Value = Plan.MorningDriver.CSLinkTrain(UBound(Plan.MorningDriver.CSLinkTrain)).EndStaName
                    Me.DGVMain.Rows(RowIndex).Cells(17).Value = BeTime(Dri.EndEorkTime - Dri.CSLinkTrain(1).StartTime + Plan.MorningDriver.WorkTime)
                    Me.DGVMain.Rows(RowIndex).Cells(18).Value = Dri.DriveDistance + Plan.MorningDriver.DriveDistance
                    For i As Integer = 0 To Me.DGVMain.ColumnCount - 1
                        Me.DGVMain.Rows(RowIndex).Cells(i).Style.BackColor = Me.DGVMain.Columns(i).DefaultCellStyle.BackColor
                    Next
                Else
                    Me.DGVMain.Rows(RowIndex).Cells(9).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(10).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(11).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(12).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(13).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(14).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(15).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(16).Value = ""
                    Me.DGVMain.Rows(RowIndex).Cells(17).Value = BeTime(Dri.EndEorkTime - Dri.CSLinkTrain(1).StartTime)
                    Me.DGVMain.Rows(RowIndex).Cells(18).Value = Dri.DriveDistance
                    For i As Integer = 0 To Me.DGVMain.ColumnCount - 1
                        Me.DGVMain.Rows(RowIndex).Cells(i).Style.BackColor = Color.Red
                    Next
                End If
        End Select

    End Sub

    Public Sub RefreshCmbItems()
        Select Case tCorStyle
            Case CorStyle.自身夜早班衔接
                Me.cmbDri.Items.Clear()
                For Each dri As CSDriver In CSTrainsAndDrivers.MorningDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.LinkedNightDriver Is Nothing OrElse dri.LinkedNightDriver.CSdriverNo = "#" Then
                            Me.cmbDri.Items.Add(dri.CSdriverNo)
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.DutySort = "早班" Then
                            If dri.LinkedNightDriver Is Nothing OrElse dri.LinkedNightDriver.CSdriverNo = "#" Then
                                Me.cmbDri.Items.Add(dri.CSdriverNo)
                            End If
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.DutySort = "早班" Then
                            If dri.LinkedNightDriver Is Nothing OrElse dri.LinkedNightDriver.CSdriverNo = "#" Then
                                Me.cmbDri.Items.Add(dri.CSdriverNo)
                            End If
                        End If
                    End If
                Next
                Me.cmbDri.Items.Add("清除")
            Case CorStyle.自身夜班被动衔接
                Me.cmbDri.Items.Clear()
                For Each dri As CSDriver In CSTrainsAndDrivers.CorMorningDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        Dim Mdri As CSDriver = dri
                        Dim index As Integer = CSTrainsAndDrivers.NegtiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                  Return value.MorningDriver Is Mdri
                                                                                              End Function)
                        If index = -1 Then
                            Me.cmbDri.Items.Add(dri.CSdriverNo)
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.CorPreParedDutyDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.DutySort = "早班" Then
                            Dim Mdri As CSDriver = dri
                            Dim index As Integer = CSTrainsAndDrivers.NegtiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                      Return value.MorningDriver Is Mdri
                                                                                                  End Function)
                            If index = -1 Then
                                Me.cmbDri.Items.Add(dri.CSdriverNo)
                            End If
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.CorPreParedTrainDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.DutySort = "早班" Then
                            Dim Mdri As CSDriver = dri
                            Dim index As Integer = CSTrainsAndDrivers.NegtiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                      Return value.MorningDriver Is Mdri
                                                                                                  End Function)
                            If index = -1 Then
                                Me.cmbDri.Items.Add(dri.CSdriverNo)
                            End If
                        End If
                    End If
                Next
                Me.cmbDri.Items.Add("清除")
            Case CorStyle.自身早班主动衔接
                Me.cmbDri.Items.Clear()
                For Each dri As CSDriver In CSTrainsAndDrivers.MorningDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        Dim Mdri As CSDriver = dri
                        Dim index As Integer = CSTrainsAndDrivers.PostiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                  Return value.MorningDriver Is Mdri
                                                                                              End Function)
                        If index = -1 Then
                            Me.cmbDri.Items.Add(dri.CSdriverNo)
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.DutySort = "早班" Then
                            Dim Mdri As CSDriver = dri
                            Dim index As Integer = CSTrainsAndDrivers.PostiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                      Return value.MorningDriver Is Mdri
                                                                                                  End Function)
                            If index = -1 Then
                                Me.cmbDri.Items.Add(dri.CSdriverNo)
                            End If
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.DutySort = "早班" Then
                            Dim Mdri As CSDriver = dri
                            Dim index As Integer = CSTrainsAndDrivers.PostiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                      Return value.MorningDriver Is Mdri
                                                                                                  End Function)
                            If index = -1 Then
                                Me.cmbDri.Items.Add(dri.CSdriverNo)
                            End If
                        End If
                    End If
                Next
                Me.cmbDri.Items.Add("清除")
        End Select
    End Sub

    Public Sub RefreshUnAssignInfo()
        Me.DGV_UnAssignInfo.Rows.Clear()
        Select Case tCorStyle
            Case CorStyle.自身夜早班衔接
                Dim ncount As Integer = 1
                For Each dri As CSDriver In CSTrainsAndDrivers.MorningDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.LinkedNightDriver Is Nothing OrElse dri.LinkedNightDriver.CSdriverNo = "#" Then
                            Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.BeginWorkTime), dri.CSLinkTrain(1).StartStaName, _
                                                         BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.WorkTime))
                            ncount += 1
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.NightDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.LinkedMorDriver Is Nothing OrElse dri.LinkedMorDriver.CSdriverNo = "#" Then
                            Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.BeginWorkTime), dri.CSLinkTrain(1).StartStaName, _
                                                         BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.WorkTime))
                            ncount += 1
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.DutySort = "早班" Then
                            If dri.LinkedNightDriver Is Nothing OrElse dri.LinkedNightDriver.CSdriverNo = "#" Then
                                Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.BeginWorkTime), dri.CSLinkTrain(1).StartStaName, _
                                                         BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.WorkTime))
                                ncount += 1
                            End If
                        ElseIf dri.DutySort = "夜班" Then
                            If dri.LinkedMorDriver Is Nothing OrElse dri.LinkedMorDriver.CSdriverNo = "#" Then
                                Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.BeginWorkTime), dri.CSLinkTrain(1).StartStaName, _
                                                         BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.WorkTime))
                                ncount += 1
                            End If
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.DutySort = "早班" Then
                            If dri.LinkedNightDriver Is Nothing OrElse dri.LinkedNightDriver.CSdriverNo = "#" Then
                                Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.BeginWorkTime), dri.CSLinkTrain(1).StartStaName, _
                                                         BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.WorkTime))
                                ncount += 1
                            End If
                        ElseIf dri.DutySort = "夜班" Then
                            If dri.LinkedMorDriver Is Nothing OrElse dri.LinkedMorDriver.CSdriverNo = "#" Then
                                Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.BeginWorkTime), dri.CSLinkTrain(1).StartStaName, _
                                                         BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.WorkTime))
                                ncount += 1
                            End If
                        End If
                    End If
                Next
            Case CorStyle.自身夜班被动衔接
                Dim ncount As Integer = 1
                For Each dri As CSDriver In CSTrainsAndDrivers.CorMorningDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        Dim Mdri As CSDriver = dri
                        Dim index As Integer = CSTrainsAndDrivers.NegtiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                  Return value.MorningDriver Is Mdri
                                                                                              End Function)
                        If index = -1 Then
                            Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.CSLinkTrain(1).StartTime), dri.CSLinkTrain(1).StartStaName, _
                                                         BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.EndEorkTime - dri.CSLinkTrain(1).StartTime))
                            ncount += 1
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.NightDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        Dim Ndri As CSDriver = dri
                        Dim index As Integer = CSTrainsAndDrivers.NegtiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                  Return value.NightDriver Is Ndri
                                                                                              End Function)
                        If index = -1 Then
                            Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.BeginWorkTime), dri.CSLinkTrain(1).StartStaName, _
                                                         BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.WorkTime))
                            ncount += 1
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.CorPreParedDutyDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.DutySort = "早班" Then
                            Dim Mdri As CSDriver = dri
                            Dim index As Integer = CSTrainsAndDrivers.NegtiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                      Return value.MorningDriver Is Mdri
                                                                                                  End Function)
                            If index = -1 Then
                                Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.CSLinkTrain(1).StartTime), dri.CSLinkTrain(1).StartStaName, _
                                                             BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.EndEorkTime - dri.CSLinkTrain(1).StartTime))
                                ncount += 1
                            End If
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.DutySort = "夜班" Then
                            Dim Ndri As CSDriver = dri
                            Dim index As Integer = CSTrainsAndDrivers.NegtiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                      Return value.NightDriver Is Ndri
                                                                                                  End Function)
                            If index = -1 Then
                                Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.BeginWorkTime), dri.CSLinkTrain(1).StartStaName, _
                                                             BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.WorkTime))
                                ncount += 1
                            End If
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.CorPreParedTrainDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.DutySort = "早班" Then
                            If dri.LinkedNightDriver Is Nothing OrElse dri.LinkedNightDriver.CSdriverNo = "#" Then
                                Dim Mdri As CSDriver = dri
                                Dim index As Integer = CSTrainsAndDrivers.NegtiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                          Return value.MorningDriver Is Mdri
                                                                                                      End Function)
                                If index = -1 Then
                                    Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.CSLinkTrain(1).StartTime), dri.CSLinkTrain(1).StartStaName, _
                                                                 BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.EndEorkTime - dri.CSLinkTrain(1).StartTime))
                                    ncount += 1
                                End If
                            End If
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.DutySort = "夜班" Then
                            Dim Ndri As CSDriver = dri
                            Dim index As Integer = CSTrainsAndDrivers.NegtiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                      Return value.NightDriver Is Ndri
                                                                                                  End Function)
                            If index = -1 Then
                                Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.BeginWorkTime), dri.CSLinkTrain(1).StartStaName, _
                                                             BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.WorkTime))
                                ncount += 1
                            End If
                        End If
                    End If
                Next
            Case CorStyle.自身早班主动衔接
                Dim ncount As Integer = 1
                For Each dri As CSDriver In CSTrainsAndDrivers.MorningDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        Dim Mdri As CSDriver = dri
                        Dim index As Integer = CSTrainsAndDrivers.PostiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                  Return value.MorningDriver Is Mdri
                                                                                              End Function)
                        If index = -1 Then
                            Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.BeginWorkTime), dri.CSLinkTrain(1).StartStaName, _
                                                         BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.WorkTime))
                            ncount += 1
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.CorNightDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        Dim Ndri As CSDriver = dri
                        Dim index As Integer = CSTrainsAndDrivers.PostiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                  Return value.NightDriver Is Ndri
                                                                                              End Function)
                        If index = -1 Then
                            Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.CSLinkTrain(1).StartTime), dri.CSLinkTrain(1).StartStaName, _
                                                         BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.EndEorkTime - dri.CSLinkTrain(1).StartTime))
                            ncount += 1
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.DutySort = "早班" Then
                            Dim Mdri As CSDriver = dri
                            Dim index As Integer = CSTrainsAndDrivers.PostiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                      Return value.MorningDriver Is Mdri
                                                                                                  End Function)
                            If index = -1 Then
                                Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.BeginWorkTime), dri.CSLinkTrain(1).StartStaName, _
                                                             BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.WorkTime))
                                ncount += 1
                            End If
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.CorPreParedDutyDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.DutySort = "夜班" Then
                            Dim Ndri As CSDriver = dri
                            Dim index As Integer = CSTrainsAndDrivers.PostiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                      Return value.NightDriver Is Ndri
                                                                                                  End Function)
                            If index = -1 Then
                                Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.CSLinkTrain(1).StartTime), dri.CSLinkTrain(1).StartStaName, _
                                                             BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.EndEorkTime - dri.CSLinkTrain(1).StartTime))
                                ncount += 1
                            End If
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.DutySort = "早班" Then
                            If dri.LinkedNightDriver Is Nothing OrElse dri.LinkedNightDriver.CSdriverNo = "#" Then
                                Dim Mdri As CSDriver = dri
                                Dim index As Integer = CSTrainsAndDrivers.PostiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                          Return value.MorningDriver Is Mdri
                                                                                                      End Function)
                                If index = -1 Then
                                    Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.BeginWorkTime), dri.CSLinkTrain(1).StartStaName, _
                                                                 BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.WorkTime))
                                    ncount += 1
                                End If
                            End If
                        End If
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.CorPreParedTrainDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo <> "#" Then
                        If dri.DutySort = "夜班" Then
                            Dim Ndri As CSDriver = dri
                            Dim index As Integer = CSTrainsAndDrivers.PostiveCorCSPlans.FindIndex(Function(value As CorCSPlan)
                                                                                                      Return value.NightDriver Is Ndri
                                                                                                  End Function)
                            If index = -1 Then
                                Me.DGV_UnAssignInfo.Rows.Add(ncount, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.CSLinkTrain(1).StartTime), dri.CSLinkTrain(1).StartStaName, _
                                                             BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.DriveDistance, BeTime(dri.EndEorkTime - dri.CSLinkTrain(1).StartTime))
                                ncount += 1
                            End If
                        End If
                    End If
                Next
        End Select
    End Sub
  

    Private Sub BtnAssign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAssign.Click
        Dim gonglishu As Boolean = True
        If MsgBox("是否考虑公里数均衡？", MsgBoxStyle.OkCancel, "提示") = MsgBoxResult.Ok Then
            gonglishu = True
        Else
            gonglishu = False
        End If
        Select Case tCorStyle
            Case CorStyle.自身夜早班衔接
                If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
                    Dim CorPlans As List(Of CorCSPlan) = FormCorCSPlan(CSTrainsAndDrivers.NightDrivers, CSTrainsAndDrivers.MorningDrivers, tCorStyle.ToString, gonglishu)
                    For Each NDri As CSDriver In CSTrainsAndDrivers.NightDrivers
                        For Each plan As CorCSPlan In CorPlans
                            If NDri Is plan.NightDriver Then
                                NDri.LinkedMorDriver = plan.MorningDriver
                                Exit For
                            End If
                        Next
                    Next
                    For Each Mdri As CSDriver In CSTrainsAndDrivers.MorningDrivers
                        For Each plan As CorCSPlan In CorPlans
                            If Mdri Is plan.MorningDriver Then
                                Mdri.LinkedNightDriver = plan.NightDriver
                            End If
                        Next
                    Next
                    Dim MPreDutyDris As New List(Of CSDriver)         '=======备班的衔接
                    Dim NPreDutyDris As New List(Of CSDriver)
                    For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
                        If dri.DutySort = "早班" Then
                            MPreDutyDris.Add(dri)
                        ElseIf dri.DutySort = "夜班" Then
                            NPreDutyDris.Add(dri)
                        End If
                    Next
                    Dim CorPDPlans As List(Of CorCSPlan) = FormCorCSPlan(NPreDutyDris, MPreDutyDris, tCorStyle.ToString, gonglishu)
                    For Each NDri As CSDriver In NPreDutyDris
                        For Each plan As CorCSPlan In CorPDPlans
                            If NDri Is plan.NightDriver Then
                                NDri.LinkedMorDriver = plan.MorningDriver
                                Exit For
                            End If
                        Next
                    Next
                    For Each Mdri As CSDriver In MPreDutyDris
                        For Each plan As CorCSPlan In CorPDPlans
                            If Mdri Is plan.MorningDriver Then
                                Mdri.LinkedNightDriver = plan.NightDriver
                            End If
                        Next
                    Next
                    Dim MPreTrainDris As New List(Of CSDriver)         '=======备车的衔接
                    Dim NPreTrainDris As New List(Of CSDriver)
                    For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
                        If dri.DutySort = "早班" Then
                            MPreTrainDris.Add(dri)
                        ElseIf dri.DutySort = "夜班" Then
                            NPreTrainDris.Add(dri)
                        End If
                    Next
                    Dim CorPTPlans As List(Of CorCSPlan) = FormCorCSPlan(NPreTrainDris, MPreTrainDris, tCorStyle.ToString, gonglishu)
                    For Each NDri As CSDriver In NPreTrainDris
                        For Each plan As CorCSPlan In CorPTPlans
                            If NDri Is plan.NightDriver Then
                                NDri.LinkedMorDriver = plan.MorningDriver
                                Exit For
                            End If
                        Next
                    Next
                    For Each Mdri As CSDriver In MPreTrainDris
                        For Each plan As CorCSPlan In CorPTPlans
                            If Mdri Is plan.MorningDriver Then
                                Mdri.LinkedNightDriver = plan.NightDriver
                            End If
                        Next
                    Next
                    Call LoadMainCorInfo()
                    Call RefreshUnAssignInfo()
                End If
            Case CorStyle.自身夜班被动衔接
                CSTrainsAndDrivers.NegtiveCorCSPlans = FormCorCSPlan(CSTrainsAndDrivers.NightDrivers, CSTrainsAndDrivers.CorMorningDrivers, tCorStyle.ToString, gonglishu)
                Dim MPreDutyDris As New List(Of CSDriver)         '=======备班的衔接
                Dim NPreDutyDris As New List(Of CSDriver)
                For Each dri As CSDriver In CSTrainsAndDrivers.CorPreParedDutyDrivers
                    If dri.DutySort = "早班" Then
                        MPreDutyDris.Add(dri)
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
                    If dri.DutySort = "夜班" Then
                        NPreDutyDris.Add(dri)
                    End If
                Next
                CSTrainsAndDrivers.NegtiveCorCSPlans.AddRange(FormCorCSPlan(NPreDutyDris, MPreDutyDris, tCorStyle.ToString, gonglishu))
                Dim MPreTrainDris As New List(Of CSDriver)         '=======备车的衔接
                Dim NPreTrainDris As New List(Of CSDriver)
                For Each dri As CSDriver In CSTrainsAndDrivers.CorPreParedTrainDrivers
                    If dri.DutySort = "早班" Then
                        MPreTrainDris.Add(dri)
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
                    If dri.DutySort = "夜班" Then
                        NPreTrainDris.Add(dri)
                    End If
                Next
                CSTrainsAndDrivers.NegtiveCorCSPlans.AddRange(FormCorCSPlan(NPreTrainDris, MPreTrainDris, tCorStyle.ToString, gonglishu))
                Call LoadNegtiveCorInfo()
                Call RefreshUnAssignInfo()
            Case CorStyle.自身早班主动衔接
                CSTrainsAndDrivers.PostiveCorCSPlans = FormCorCSPlan(CSTrainsAndDrivers.CorNightDrivers, CSTrainsAndDrivers.MorningDrivers, tCorStyle.ToString, gonglishu)
                Dim MPreDutyDris As New List(Of CSDriver)         '=======备班的衔接
                Dim NPreDutyDris As New List(Of CSDriver)
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
                    If dri.DutySort = "早班" Then
                        MPreDutyDris.Add(dri)
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.CorPreParedDutyDrivers
                    If dri.DutySort = "夜班" Then
                        NPreDutyDris.Add(dri)
                    End If
                Next
                CSTrainsAndDrivers.PostiveCorCSPlans.AddRange(FormCorCSPlan(NPreDutyDris, MPreDutyDris, tCorStyle.ToString, gonglishu))
                Dim MPreTrainDris As New List(Of CSDriver)         '=======备车的衔接
                Dim NPreTrainDris As New List(Of CSDriver)
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
                    If dri.DutySort = "早班" Then
                        MPreTrainDris.Add(dri)
                    End If
                Next
                For Each dri As CSDriver In CSTrainsAndDrivers.CorPreParedTrainDrivers
                    If dri.DutySort = "夜班" Then
                        NPreTrainDris.Add(dri)
                    End If
                Next
                CSTrainsAndDrivers.PostiveCorCSPlans.AddRange(FormCorCSPlan(NPreTrainDris, MPreTrainDris, tCorStyle.ToString, gonglishu))
                Call LoadPositiveCorInfo()
                Call RefreshUnAssignInfo()
        End Select
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Select Case tCorStyle
            Case CorStyle.自身夜早班衔接
                If CSTrainsAndDrivers.MorningDrivers.Count > 0 Then
                    For Each dri As CSDriver In CSTrainsAndDrivers.MorningDrivers
                        dri.LinkedNightDriver = Nothing
                    Next
                End If
                If CSTrainsAndDrivers.NightDrivers.Count > 0 Then
                    For Each dri As CSDriver In CSTrainsAndDrivers.NightDrivers
                        dri.LinkedMorDriver = Nothing
                    Next
                End If
                If CSTrainsAndDrivers.PreParedDutyDrivers.Count > 0 Then
                    For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
                        If dri.DutySort = "早班" Then
                            dri.LinkedNightDriver = Nothing
                        ElseIf dri.DutySort = "夜班" Then
                            dri.LinkedMorDriver = Nothing
                        End If
                    Next
                End If
                If CSTrainsAndDrivers.PreParedTrainDrivers.Count > 0 Then
                    For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
                        If dri.DutySort = "早班" Then
                            dri.LinkedNightDriver = Nothing
                        ElseIf dri.DutySort = "夜班" Then
                            dri.LinkedMorDriver = Nothing
                        End If
                    Next
                End If
                Call LoadMainCorInfo()
                Call RefreshUnAssignInfo()
            Case CorStyle.自身夜班被动衔接
                CSTrainsAndDrivers.NegtiveCorCSPlans.Clear()
                Call LoadNegtiveCorInfo()
                Call RefreshUnAssignInfo()
            Case CorStyle.自身早班主动衔接
                CSTrainsAndDrivers.PostiveCorCSPlans.Clear()
                Call LoadPositiveCorInfo()
                Call RefreshUnAssignInfo()
        End Select
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call OutPutToEXCELFileFormDataGrid("夜早班衔接", Me.DGVMain, Me)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim New0penFile As New OpenFileDialog
        Dim strExcelFilePath As String
        New0penFile.Filter = "xls files (*.xls)|*.xls|xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        New0penFile.FilterIndex = 1
        New0penFile.RestoreDirectory = True
        strExcelFilePath = ""

        If New0penFile.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            strExcelFilePath = New0penFile.FileName
        End If
        '获得数据库的名称
        If strExcelFilePath <> "" Then

            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
            Dim MyConnection As System.Data.OleDb.OleDbConnection
            MyConnection = New System.Data.OleDb.OleDbConnection( _
                          "provider=Microsoft.ACE.OLEDB.12.0; " & _
                          "data source='" & strExcelFilePath & "'; " & _
                          "Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'")
            Dim tmpStr As String
            tmpStr = "select * from" & "[夜早班衔接$]"
            MyCommand = New System.Data.OleDb.OleDbDataAdapter(tmpStr, MyConnection)
            MyConnection.Open()

            Dim objDataset1 As New DataSet
            Try
                MyCommand.Fill(objDataset1, "XLData")
                Dim temTab As Data.DataTable = objDataset1.Tables(0)
                If temTab IsNot Nothing Then
                    Select Case tCorStyle
                        Case CorStyle.自身夜早班衔接
                            Call ClearNirCorPlan()
                            For Each row As DataRow In temTab.Rows
                                Dim NitDriNo As String = row.Item("夜班任务").ToString.Trim
                                Dim MorDriNo As String = row.Item("早班任务").ToString.Trim
                                Dim NitDri As CSDriver = GetDriFromDriNo(NitDriNo, "夜班")
                                Dim MorDri As CSDriver = GetDriFromDriNo(MorDriNo, "早班")
                                If NitDri IsNot Nothing AndAlso MorDri IsNot Nothing Then
                                    NitDri.LinkedMorDriver = MorDri
                                    MorDri.LinkedNightDriver = NitDri
                                End If
                            Next
                            Call LoadMainCorInfo()
                        Case CorStyle.自身夜班被动衔接
                            CSTrainsAndDrivers.NegtiveCorCSPlans.Clear()
                            For Each row As DataRow In temTab.Rows
                                Dim NitDriNo As String = row.Item("夜班任务").ToString.Trim
                                Dim MorDriNo As String = row.Item("早班任务").ToString.Trim
                                Dim NitDri As CSDriver = GetDriFromDriNo(NitDriNo, "夜班")
                                Dim MorDri As CSDriver = GetCorDriFromDriNo(MorDriNo, "早班")
                                If NitDri IsNot Nothing AndAlso MorDri IsNot Nothing Then
                                    Dim templan As New CorCSPlan(NitDri, MorDri)
                                    CSTrainsAndDrivers.NegtiveCorCSPlans.Add(templan)
                                End If
                            Next
                            Call LoadNegtiveCorInfo()
                        Case CorStyle.自身早班主动衔接
                            CSTrainsAndDrivers.PostiveCorCSPlans.Clear()
                            For Each row As DataRow In temTab.Rows
                                Dim NitDriNo As String = row.Item("夜班任务").ToString.Trim
                                Dim MorDriNo As String = row.Item("早班任务").ToString.Trim
                                Dim NitDri As CSDriver = GetCorDriFromDriNo(NitDriNo, "夜班")
                                Dim MorDri As CSDriver = GetDriFromDriNo(MorDriNo, "早班")
                                If NitDri IsNot Nothing AndAlso MorDri IsNot Nothing Then
                                    Dim templan As New CorCSPlan(NitDri, MorDri)
                                    CSTrainsAndDrivers.PostiveCorCSPlans.Add(templan)
                                End If
                            Next
                            Call LoadPositiveCorInfo()
                    End Select
                    Call RefreshUnAssignInfo()
                End If
            Catch ex As Exception
                MsgBox("EXCEL数据库不正确，请确定打开的数据库格式正确!")
            End Try
            MyConnection.Close()
            GC.Collect()
        End If
    End Sub

    Public Function GetDriFromDriNo(ByVal DriNo As String, ByVal DutySort As String) As CSDriver
        GetDriFromDriNo = Nothing
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo = DriNo AndAlso CSTrainsAndDrivers.CSDrivers(i).DutySort = DutySort Then
                    GetDriFromDriNo = CSTrainsAndDrivers.CSDrivers(i)
                    Exit For
                End If
            Next
        End If
        If GetDriFromDriNo Is Nothing Then
            GetDriFromDriNo = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                              Return (value.CSdriverNo = DriNo AndAlso value.DutySort = DutySort)
                                                                          End Function)
            If GetDriFromDriNo Is Nothing Then
                GetDriFromDriNo = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                   Return (value.CSdriverNo = DriNo AndAlso value.DutySort = DutySort)
                                                                               End Function)
            End If
        End If
    End Function

    Public Function GetCorDriFromDriNo(ByVal DriNo As String, ByVal DutySort As String) As CSDriver
        GetCorDriFromDriNo = Nothing
        If CSTrainsAndDrivers.CorCSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CorCSDrivers) > 0 Then
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.CorCSDrivers)
                If CSTrainsAndDrivers.CorCSDrivers(i).CSdriverNo = DriNo AndAlso CSTrainsAndDrivers.CorCSDrivers(i).DutySort = DutySort Then
                    GetCorDriFromDriNo = CSTrainsAndDrivers.CorCSDrivers(i)
                    Exit For
                End If
            Next
        End If
        If GetCorDriFromDriNo Is Nothing Then
            GetCorDriFromDriNo = CSTrainsAndDrivers.CorPreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                                    Return (value.CSdriverNo = DriNo AndAlso value.DutySort = DutySort)
                                                                                End Function)
            If GetCorDriFromDriNo Is Nothing Then
                GetCorDriFromDriNo = CSTrainsAndDrivers.CorPreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                         Return (value.CSdriverNo = DriNo AndAlso value.DutySort = DutySort)
                                                                                     End Function)
            End If
        End If
    End Function

    Public Sub ClearNirCorPlan()
        For Each dri As CSDriver In CSTrainsAndDrivers.MorningDrivers
            dri.LinkedNightDriver = Nothing
        Next
        For Each dri As CSDriver In CSTrainsAndDrivers.NightDrivers
            dri.LinkedMorDriver = Nothing
        Next
        For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
            dri.LinkedMorDriver = Nothing
            dri.LinkedNightDriver = Nothing
        Next
        For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
            dri.LinkedMorDriver = Nothing
            dri.LinkedNightDriver = Nothing
        Next
    End Sub

 
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Me.DGV_UnAssignInfo.Rows.Count > 0 Then
            MsgBox("尚有位置没有衔接！")
            Exit Sub
        End If
        If MsgBox("原有的相同衔接计划将会覆盖", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Dim str As String = ""
            Select Case tCorStyle
                Case CorStyle.自身夜早班衔接
                    str = "delete from cs_amdrivercorrespond where lineid='" & CurLineName & "' and adrivertimetableid='" & strQCurCSPlanID & "' and mdrivertimetableid='" & strQCurCSPlanID & "'"
                    Globle.Method.UpdateDataForAccess(str)
                    System.Threading.Thread.Sleep(50)
                    For i As Integer = 0 To Me.DGVMain.Rows.Count - 1
                        str = "insert into cs_amdrivercorrespond values('" & CurLineName & "','" & strQCurCSPlanID & "','" & strQCurCSPlanID & "','" & Me.DGVMain.Rows(i).Cells("夜班任务").Value.ToString & "','" & Me.DGVMain.Rows(i).Cells("早班任务").Value.ToString & "')"
                         Globle.Method.UpdateDataForAccess(str)
                        System.Threading.Thread.Sleep(50)
                    Next
                Case CorStyle.自身夜班被动衔接
                    str = "delete from cs_amdrivercorrespond where lineid='" & CurLineName & "' and adrivertimetableid='" & strQCurCSPlanID & "' and mdrivertimetableid='" & GetCSPlanIDFromCSPlanName(CSTrainsAndDrivers.CorCSPlanName) & "'"
                    Globle.Method.UpdateDataForAccess(str)
                    System.Threading.Thread.Sleep(50)
                    For i As Integer = 0 To Me.DGVMain.Rows.Count - 1
                        str = "insert into cs_amdrivercorrespond values('" & CurLineName & "','" & strQCurCSPlanID & "','" & GetCSPlanIDFromCSPlanName(CSTrainsAndDrivers.CorCSPlanName) & "','" & Me.DGVMain.Rows(i).Cells("夜班任务").Value.ToString & "','" & Me.DGVMain.Rows(i).Cells("早班任务").Value.ToString & "')"
                         Globle.Method.UpdateDataForAccess(str)
                        System.Threading.Thread.Sleep(50)
                    Next
                Case CorStyle.自身早班主动衔接
                    str = "delete from cs_amdrivercorrespond where lineid='" & CurLineName & "' and adrivertimetableid='" & GetCSPlanIDFromCSPlanName(CSTrainsAndDrivers.CorCSPlanName) & "' and mdrivertimetableid='" & strQCurCSPlanID & "'"
                    Globle.Method.UpdateDataForAccess(str)
                    System.Threading.Thread.Sleep(50)
                    For i As Integer = 0 To Me.DGVMain.Rows.Count - 1
                        str = "insert into cs_amdrivercorrespond values('" & CurLineName & "','" & GetCSPlanIDFromCSPlanName(CSTrainsAndDrivers.CorCSPlanName) & "','" & strQCurCSPlanID & "','" & Me.DGVMain.Rows(i).Cells("夜班任务").Value.ToString & "','" & Me.DGVMain.Rows(i).Cells("早班任务").Value.ToString & "')"
                         Globle.Method.UpdateDataForAccess(str)
                        System.Threading.Thread.Sleep(50)
                    Next
            End Select
            MsgBox("保存成功！")
        End If
      
    End Sub
  
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        FrmDiffSta.ShowDialog()
    End Sub
End Class