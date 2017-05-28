Public Class frmLoginForm

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Dim UserName As String = Me.cmbUser.Text.Trim
        Dim Password As String = Me.PasswordTextBox.Text

        If Password = "" OrElse UserName = "" Then
            MsgBox("用户名或密码不能为空!", MsgBoxStyle.OkOnly, "提醒")
            Exit Sub
        Else
            If IfUserExit(UserName, Password) Then   '表明用户存在
                CurrentUserName = UserName
                CurrentUserRole = GetUserRole(CurrentUserName)

                Me.Hide()
                frmODSMain.Show()
            Else
                MsgBox("用户名或密码不存在!", MsgBoxStyle.OkOnly, "提醒")
                Exit Sub
            End If
        End If

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub

    Private Sub frmLoginForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            cmbUser.Items.Clear()
            Dim str As String = "select * from pd_userdetailinfo t order by t.username"
            Dim tab As DataTable = Globle.Method.ReadDataForAccess(str)
            If tab IsNot Nothing Then
                For Each row As DataRow In tab.Rows
                    cmbUser.Items.Add(row.Item("username").ToString.Trim)
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.ToString(), , "提示") '& vbCrLf & ex.ToString
            End
        End Try
        Dim stream As New System.IO.StreamReader("Config\\LineInf.dat", System.Text.Encoding.Default)
        CurLineName = stream.ReadLine

    End Sub

    Public Function IfUserExit(ByVal name As String, ByVal word As String) As Boolean
        IfUserExit = False
        Dim str As String = "select * from pd_userdetailinfo t where t.username='" & name & "' and t.userpassword='" & word & "'"
        Dim UserTab As Data.DataTable = Globle.Method.ReadDataForAccess(str)
        If UserTab.Rows.Count = 1 Then   '表明用户存在
            IfUserExit = True
        Else
            IfUserExit = False
        End If
    End Function

    Public Function GetUserRole(ByVal name As String) As String
        GetUserRole = ""
        Dim str As String = "select * from pd_userdetailinfo t where t.username='" & name & "'"
        Dim UserTab As Data.DataTable = Globle.Method.ReadDataForAccess(str)
        If UserTab.Rows.Count = 1 Then   '表明用户存在
            GetUserRole = UserTab.Rows(0).Item("department").ToString.Trim
        End If
    End Function
  
End Class
