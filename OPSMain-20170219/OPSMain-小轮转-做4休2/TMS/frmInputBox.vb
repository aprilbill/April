Public Class frmInputBox

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        StrInputBoxText = Trim(Me.txtText.Text)
        StrInputBoxCombText = Trim(Me.cmbText.Text)
        bCancelInput = 0
        Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        StrInputBoxText = ""
        StrInputBoxCombText = ""
        bCancelInput = 1
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
        bCancelInput = 1
        Me.KeyPreview = True
    End Sub
End Class