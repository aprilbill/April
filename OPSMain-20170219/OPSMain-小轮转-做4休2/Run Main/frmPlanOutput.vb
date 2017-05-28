Public Class frmPlanOutput

    Private Sub frmPlanOutput_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ComboBox1.SelectedIndex = 0
        Me.ComboBox2.SelectedIndex = 1
        Me.DateTimePicker1.Enabled = False
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
        Me.Dispose()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If Me.ComboBox2.Text = "特定日期" Then
            Me.DateTimePicker1.Enabled = True
        Else
            Me.DateTimePicker1.Enabled = False
        End If
    End Sub
End Class