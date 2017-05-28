Public Class FrmAssignUnDuty

    Public UnAssignDuties As List(Of Coordination2.CSDriver)
    Public SelectDuty As Coordination2.CSDriver
    Public IFDetailed As Boolean = False

    Private Sub Btn_Cancle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancle.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Btn_Ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Ok.Click
        If Me.Cmb_Duties.Text.Trim <> "" Then
            Me.SelectDuty = UnAssignDuties(Me.Cmb_Duties.SelectedIndex)
        Else
            MsgBox("请选择分配任务!", MsgBoxStyle.OkOnly, "提醒")
            Exit Sub
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub FrmAssignUnDuty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Size = New Size(340, 135)
        Me.PictureBox1.Visible = False
        If UnAssignDuties IsNot Nothing Then
            For Each csdri As Coordination2.CSDriver In UnAssignDuties
                Me.Cmb_Duties.Items.Add(csdri.DutySort & "/" & csdri.OutPutCSDriverNo & "||" & (csdri.TotalDayWorkTime / 3600).ToString("0.0") & "||" & csdri.DriveDistance.ToString("0.0"))
            Next
            If Me.Cmb_Duties.Items.Count > 0 Then
                Me.Cmb_Duties.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.IFDetailed = False Then
            Me.Size = New Size(New Point(340, 353))
            Me.PictureBox1.Visible = True
            Me.IFDetailed = True
            Me.Button1.Text = "收起↑"
        Else
            Me.Size = New Size(New Point(340, 130))
            Me.PictureBox1.Visible = False
            Me.IFDetailed = False
            Me.Button1.Text = "详细信息↓"
        End If
    End Sub

    Private Sub Cmb_Duties_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmb_Duties.SelectedIndexChanged
        If Me.UnAssignDuties.Count > 0 Then
            Me.PropertyGrid1.SelectedObject = Me.UnAssignDuties(Me.Cmb_Duties.SelectedIndex)
        End If
    End Sub
End Class