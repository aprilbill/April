Public Class FrmUserManager
    Public BigSize As New Size(673, 386)
    Public SmallSize As New Size(453, 386)
    Public ModifyName As String

    Private Sub BtnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRegister.Click
        Call OpenRegiste()
    End Sub

    Private Sub BtnCloseRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCloseRegister.Click
        Call CloseRegiste()
    End Sub

    Private Sub FrmUserManager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Str As String
        Str = "select linename from pd_lineinfo "
        Dim tab As System.Data.DataTable = Globle.Method.ReadDataForAccess(Str)
        If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
            For i As Integer = 0 To tab.Rows.Count - 1
                CmbLineName.Items.Add(tab.Rows(i).Item("linename").ToString)
            Next
        End If


        Call ResetTool()      '根据用户设置修改权限
        Call CloseRegiste()
        Call CloseModify()
        Call refreshData()

    End Sub

    Public Sub OpenRegiste()
        Me.CmbLineName.Text = ""
        Me.TXTNewConfrimPwd.Text = ""
        Me.CmbNewDepart.Text = ""
        Me.TXTNewPwd.Text = ""
        Me.TXTNewName.Text = ""
        If Me.Panel1.Visible = False Then
            Me.Size = BigSize
            Me.Panel1.Visible = True
            Me.Panel2.Visible = False
        End If
        Me.TXTNewName.Focus()
    End Sub

    Public Sub CloseRegiste()
        If Me.Panel1.Visible = True Then
            Me.Panel1.Visible = False
            Me.Panel2.Visible = False
            Me.Size = SmallSize
        End If
    End Sub

    Public Sub OpenModify()
        Me.TTLineName.Text = Me.DGVUser.SelectedRows(0).Cells("线路名").Value
        Me.TTName.Text = Me.DGVUser.SelectedRows(0).Cells("用户名").Value
        Me.ModifyName = Me.DGVUser.SelectedRows(0).Cells("用户名").Value
        Me.TTPwd.Text = ""
        Me.TTComfirmPwd.Text = ""
        Me.CmbDepart.Text = Me.DGVUser.SelectedRows(0).Cells("用户角色").Value
        If Me.Panel2.Visible = False Then
            Me.Size = BigSize
            Me.Panel2.Visible = True
            Me.Panel1.Visible = False
        End If
        Me.TTName.Focus()
    End Sub

    Public Sub CloseModify()
        If Me.Panel2.Visible = True Then
            Me.Panel2.Visible = False
            Me.Panel1.Visible = False
            Me.Size = SmallSize
        End If
    End Sub

    Public Sub refreshData()
        Me.DGVUser.Rows.Clear()
        Dim str As String
        If CurrentUserName = "admin" Then
            str = "select t.linename,t.username,t.userpassword,t.department from pd_userdetailinfo t"
        Else
            str = "select t.linename,t.username,t.userpassword,t.department from pd_userdetailinfo t where t.username='" & CurrentUserName & "'"
        End If
        Dim UserTab As New Data.DataTable
        UserTab = Globle.Method.ReadDataForAccess(str)
        If UserTab.Rows.Count > 0 Then
            For i As Integer = 0 To UserTab.Rows.Count - 1
                Me.DGVUser.Rows.Add(UserTab.Rows(i).Item("linename").ToString, UserTab.Rows(i).Item("username").ToString, UserTab.Rows(i).Item("userpassword").ToString, UserTab.Rows(i).Item("department").ToString)
            Next
        End If
    End Sub

    Private Sub 删除用户信息DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除用户信息DToolStripMenuItem.Click
        If Me.DGVUser.SelectedRows.Count > 0 Then
            If MsgBox("确定删除这些用户?", MsgBoxStyle.OkCancel, "提醒") = MsgBoxResult.Ok Then
                For Each row As DataGridViewRow In Me.DGVUser.SelectedRows
                    If row.Cells("用户名").Value.ToString.Trim = "admin" Then
                        MsgBox("管理员角色不能够被删除！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                    Else
                        Dim str As String = "delete from pd_userdetailinfo t where t.username='" & row.Cells("用户名").Value & "'"
                        Globle.Method.UpdateDataForAccess(str)
                    End If
                Next
                Call refreshData()
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub 修改用户信息MToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改用户信息MToolStripMenuItem.Click
        Call OpenModify()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Call CloseModify()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click      '提交注册信息
        If Me.TXTNewConfrimPwd.Text.Trim = "" OrElse Me.CmbNewDepart.Text.Trim = "" OrElse _
            Me.TXTNewName.Text.Trim = "" OrElse Me.TXTNewPwd.Text.Trim = "" Then
            MsgBox("请将注册信息填写完整！", MsgBoxStyle.OkCancel, "提醒")
            Exit Sub
        Else
            Try
                If Me.TXTNewPwd.Text <> Me.TXTNewConfrimPwd.Text Then
                    MsgBox("密码不一致！", MsgBoxStyle.OkCancel, "提醒")
                    Exit Sub
                End If
                Dim str As String = "insert into pd_userdetailinfo t (linename,username,userpassword,department) values('" & Me.CmbLineName.Text.Trim & "','" & Me.TXTNewName.Text.Trim & _
                                    "','" & Me.TXTNewPwd.Text.Trim & "','" & Me.CmbNewDepart.Text.Trim & "')"
                Globle.Method.UpdateDataForAccess(str)
                MsgBox("注册成功！", MsgBoxStyle.OkCancel, "恭喜")
                Call CloseRegiste()
                Call refreshData()
            Catch ex As Exception
                MsgBox("用户名已经存在或网络故障！", MsgBoxStyle.OkCancel, "提醒")
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Me.CmbLineName.Text.Trim = "" OrElse Me.TTComfirmPwd.Text.Trim = "" OrElse Me.CmbDepart.Text.Trim = "" OrElse _
            Me.TTName.Text.Trim = "" OrElse Me.TTPwd.Text.Trim = "" Then
            MsgBox("请将修改信息填写完整！", MsgBoxStyle.OkCancel, "提醒")
            Exit Sub
        Else
            If Me.TTPwd.Text <> Me.TTComfirmPwd.Text Then
                MsgBox("密码不一致！", MsgBoxStyle.OkCancel, "提醒")
                Exit Sub
            End If
            Dim str As String = "update pd_userdetailinfo t set linename='" & Me.CmbLineName.Text.Trim & "',username='" & Me.TTName.Text.Trim & "',userpassword='" & Me.TTPwd.Text.Trim & "',department='" & Me.CmbDepart.Text.Trim & "' where username='" & ModifyName & "'"
            Globle.Method.UpdateDataForAccess(str)
            MsgBox("修改成功！", MsgBoxStyle.OkCancel, "恭喜")
            Call CloseRegiste()
            Call refreshData()
        End If
    End Sub

    Private Sub ResetTool()
        Me.BtnRegister.Visible = False
        修改用户信息MToolStripMenuItem.Visible = False
        删除用户信息DToolStripMenuItem.Visible = False
        If CurrentUserName = "admin" Then
            Me.BtnRegister.Visible = True
            修改用户信息MToolStripMenuItem.Visible = True
            删除用户信息DToolStripMenuItem.Visible = True
        Else
            修改用户信息MToolStripMenuItem.Visible = True
        End If
    End Sub

    Private Sub DGVUser_CellFormatting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DGVUser.CellFormatting
        If Me.DGVUser.Columns(e.ColumnIndex).Name = "密码" Then
            If e.Value <> Nothing AndAlso e.Value.ToString.Length > 0 Then
                e.Value = New String("*", e.Value.ToString.Length)
            End If
        End If
    End Sub

  
End Class