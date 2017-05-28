Public Class FrmChangeDury

    Public CurJob As Coordination2.DriverDayJob
    Public CurTimetable As Coordination2.CSTimeTable
    Public CurArea As String
    Public ChangeDri As Coordination2.CSDriver

    Private Sub FrmChangeDury_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.PreNo.Text = CurJob.OutPutCSDriverNO
        Me.PreDis.Text = CurJob.DriveDistance
        Me.PreDutySort.Text = CurJob.DutySort
        Me.PreStart.Text = Coordination2.Global.BeTime(CurJob.starttime) & "/" & CurJob.StartStaName
        Me.PreEnd.Text = Coordination2.Global.BeTime(CurJob.endtime) & "/" & CurJob.OffStaName

        Me.CurNo.Items.Clear()
        For Each dri As Coordination2.CSDriver In CurTimetable.MCSDrivers
            If dri.BelongArea = CurArea Then
                Me.CurNo.Items.Add(dri.OutPutCSDriverNo)
            End If
        Next
        For Each dri As Coordination2.CSDriver In CurTimetable.NCSDrivers
            If dri.BelongArea = CurArea Then
                Me.CurNo.Items.Add(dri.OutPutCSDriverNo)
            End If
        Next
        For Each dri As Coordination2.CSDriver In CurTimetable.CCSDrivers
            If dri.BelongArea = CurArea Then
                Me.CurNo.Items.Add(dri.OutPutCSDriverNo)
            End If
        Next
        For Each dri As Coordination2.CSDriver In CurTimetable.ACSDrivers
            If dri.BelongArea = CurArea Then
                Me.CurNo.Items.Add(dri.OutPutCSDriverNo)
            End If
        Next
    End Sub

    Public Sub New(ByVal _cjob As Coordination2.DriverDayJob, ByVal timetable As Coordination2.CSTimeTable, ByVal cArea As String)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        CurJob = _cjob
        CurTimetable = timetable
        CurArea = cArea
    End Sub

    Private Sub CurNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CurNo.TextChanged
        Dim drino As String = Me.CurNo.Text.Trim
        Dim dutysort As String = Me.CmbDutySort.Text.Trim
        If drino <> "" Then
            Dim iffind As Boolean = False
            If iffind = False Then
                For Each dri As Coordination2.CSDriver In CurTimetable.MCSDrivers
                    If dri.OutPutCSDriverNo = drino AndAlso dri.DutySort = dutysort Then
                        ChangeDri = dri
                        iffind = True
                        Exit For
                    End If
                Next
            End If
            If iffind = False Then
                For Each dri As Coordination2.CSDriver In CurTimetable.NCSDrivers
                    If dri.OutPutCSDriverNo = drino AndAlso dri.DutySort = dutysort Then
                        ChangeDri = dri
                        iffind = True
                        Exit For
                    End If
                Next
            End If
            If iffind = False Then
                For Each dri As Coordination2.CSDriver In CurTimetable.CCSDrivers
                    If dri.OutPutCSDriverNo = drino AndAlso dri.DutySort = dutysort Then
                        ChangeDri = dri
                        iffind = True
                        Exit For
                    End If
                Next
            End If
            If iffind = False Then
                For Each dri As Coordination2.CSDriver In CurTimetable.ACSDrivers
                    If dri.OutPutCSDriverNo = drino AndAlso dri.DutySort = dutysort Then
                        ChangeDri = dri
                        iffind = True
                        Exit For
                    End If
                Next
            End If
            If iffind Then
                Me.CurDis.Text = ChangeDri.DriveDistance
                Me.CurStart.Text = Coordination2.Global.BeTime(ChangeDri.starttime) & "/" & ChangeDri.StartStaName
                Me.CurEnd.Text = Coordination2.Global.BeTime(ChangeDri.endtime) & "/" & ChangeDri.OffStaName
            Else
                Me.CurDis.Text = ""
                Me.CurStart.Text = ""
                Me.CurEnd.Text = ""
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.CurStart.Text.Trim = "" Then
            MsgBox("选择任务错误，请重新选择！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
            Exit Sub
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub CmbDutySort_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbDutySort.TextChanged
        Dim drino As String = Me.CurNo.Text.Trim
        Dim dutysort As String = Me.CmbDutySort.Text.Trim
        If drino <> "" Then
            Dim iffind As Boolean = False
            If iffind = False Then
                For Each dri As Coordination2.CSDriver In CurTimetable.MCSDrivers
                    If dri.OutPutCSDriverNo = drino AndAlso dri.DutySort = dutysort Then
                        ChangeDri = dri
                        iffind = True
                        Exit For
                    End If
                Next
            End If
            If iffind = False Then
                For Each dri As Coordination2.CSDriver In CurTimetable.NCSDrivers
                    If dri.OutPutCSDriverNo = drino AndAlso dri.DutySort = dutysort Then
                        ChangeDri = dri
                        iffind = True
                        Exit For
                    End If
                Next
            End If
            If iffind = False Then
                For Each dri As Coordination2.CSDriver In CurTimetable.CCSDrivers
                    If dri.OutPutCSDriverNo = drino AndAlso dri.DutySort = dutysort Then
                        ChangeDri = dri
                        iffind = True
                        Exit For
                    End If
                Next
            End If
            If iffind = False Then
                For Each dri As Coordination2.CSDriver In CurTimetable.ACSDrivers
                    If dri.OutPutCSDriverNo = drino AndAlso dri.DutySort = dutysort Then
                        ChangeDri = dri
                        iffind = True
                        Exit For
                    End If
                Next
            End If
            If iffind Then
                Me.CurDis.Text = ChangeDri.DriveDistance
                Me.CurStart.Text = Coordination2.Global.BeTime(ChangeDri.starttime) & "/" & ChangeDri.StartStaName
                Me.CurEnd.Text = Coordination2.Global.BeTime(ChangeDri.endtime) & "/" & ChangeDri.OffStaName
            Else
                Me.CurDis.Text = ""
                Me.CurStart.Text = ""
                Me.CurEnd.Text = ""
            End If
        End If
    End Sub
End Class