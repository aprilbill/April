Public Class frmInputBox
    Public selectindex As Integer = -1
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        StrInputBoxText = Trim(Me.txtText.Text)
        StrInputBoxCombText = Trim(Me.cmbText.Text)
        If Me.cmbText.Text.Trim <> "" Then
            selectindex = Me.cmbText.SelectedIndex
        End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        StrInputBoxText = ""
        StrInputBoxCombText = ""
        selectindex = -1
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmInputBox_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Enter Then
            Call cmdOK_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmInputBox_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        StrInputBoxText = ""
        StrInputBoxCombText = ""
    End Sub
End Class