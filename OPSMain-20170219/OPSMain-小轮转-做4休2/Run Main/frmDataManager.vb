Public Class frmDataManager
    Dim conn_state As Boolean = False
    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Dim tempconnection As New OleDb.OleDbConnection
        If Me.ChkdListBox.Items.Count <> 0 Then
            Me.ChkdListBox.Items.Clear()
        End If
        tempconnection.ConnectionString = "Provider=oraoledb.oracle;Data Source=" & Me.TextboxSource.Text.Trim & ";User ID=" & Me.TextBoxUsername.Text.Trim & ";Password=" & Me.TextBoxPassword.Text.Trim & ";"
        Try
            tempconnection.Open()
            conn_state = True
            Dim tempcommand As New OleDb.OleDbCommand
            Dim _reader As OleDb.OleDbDataReader
            tempcommand.Connection = tempconnection
            tempcommand.CommandType = CommandType.Text
            tempcommand.CommandText = "select table_name from user_tables order by table_name "
            _reader = tempcommand.ExecuteReader
            While (_reader.Read)
                If _reader.GetValue(0).ToString.Length > 2 Then
                    If _reader.GetValue(0).ToString.Substring(0, 3) <> "BIN" Then
                        Me.ChkdListBox.Items.Add(_reader.GetValue(0).ToString)
                        Me.ChkdListBox.SetItemChecked(Me.ChkdListBox.Items.Count - 1, True)
                    End If
                End If
            End While
            tempcommand.Dispose()
            tempconnection.Close()
            tempconnection.Dispose()
        Catch ex As Exception
            tempconnection.Dispose()
            MessageBox.Show("无法连接到数据库" & Me.TextboxSource.Text.Trim & "!", "提示")
        End Try

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Dispose()
    End Sub

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        If Me.ChkdListBox.Items.Count <> 0 Then
            Dim OperateArray As New ArrayList
            Dim i As Integer
            For i = 0 To Me.ChkdListBox.Items.Count - 1
                If Me.ChkdListBox.GetItemChecked(i) = True Then
                    OperateArray.Add(Me.ChkdListBox.Items(i).ToString)
                End If
            Next
            If OperateArray.Count = 0 Then
                MessageBox.Show("首先选择你要操作的数据表")
                Exit Sub
            End If
            Dim tablename As String
            tablename = OperateArray.Item(0).ToString
            For i = 0 To OperateArray.Count - 2
                tablename = tablename & "," & OperateArray.Item(i + 1).ToString
            Next
            If Me.RadioBtn1.Checked = True Then
                Dim savefile As New SaveFileDialog
                With savefile
                    .InitialDirectory = Application.StartupPath
                    .Filter = "ORACLE数据|*.dmp;"
                    .Title = "备份数据库"
                    .FileName = "ODS_DBBAK"
                    .RestoreDirectory = True
                End With
                If savefile.ShowDialog = Windows.Forms.DialogResult.OK Then
                    Dim backcommand As String
                    backcommand = "exp " & Me.TextBoxUsername.Text.Trim & "/" & Me.TextBoxPassword.Text.Trim & "@" & Me.TextboxSource.Text.Trim & " file=" & savefile.FileName & " tables=(" & tablename & ")full=n；"
                    Dim p As New Process
                    p.StartInfo.FileName = "cmd.exe"
                    p.StartInfo.UseShellExecute = False
                    p.StartInfo.RedirectStandardInput = True
                    p.StartInfo.RedirectStandardOutput = True
                    p.StartInfo.RedirectStandardError = True
                    p.StartInfo.CreateNoWindow = True
                    Try
                        p.Start()
                        p.StandardInput.WriteLine("dir")
                        p.StandardInput.WriteLine(backcommand)
                        p.StandardInput.WriteLine("exit")
                        p.Close()
                        MessageBox.Show("提示：数据库正在备份当中，等到命令行窗体结束后再进行操作！")
                    Catch ex As Exception
                        MessageBox.Show("提示：数据库备份失败！" + ex.Message & "请联系开发人员！")
                    End Try
                End If
            ElseIf Me.RadioBtn2.Checked = True Then
                Dim _connection As New OleDb.OleDbConnection
                Dim _command As New OleDb.OleDbCommand
                _connection.ConnectionString = "Provider=oraoledb.oracle;Data Source=" & Me.TextboxSource.Text.Trim & ";User ID=" & Me.TextBoxUsername.Text.Trim & ";Password=" & Me.TextBoxPassword.Text.Trim & ";"
                _command.Connection = _connection
                _command.CommandType = CommandType.Text
                Try
                    _connection.Open()
                    Dim openfile As New OpenFileDialog
                    If openfile.ShowDialog = Windows.Forms.DialogResult.OK Then
                        For Each table As String In OperateArray
                            Application.DoEvents()
                            _command.CommandText = "delete from " & table.ToString.Trim
                            _command.ExecuteNonQuery()
                        Next
                        _command.Dispose()
                        _connection.Close()
                        _connection.Dispose()
                        Dim backcommand As String
                        backcommand = "imp " & Me.TextBoxUsername.Text.Trim & "/" & Me.TextBoxPassword.Text.Trim & "@" & Me.TextboxSource.Text.Trim & " file=" & openfile.FileName & " ignore=y destroy=y full=n tables=(" & tablename & ")"
                        Dim p As New Process
                        p.StartInfo.FileName = "cmd.exe"
                        p.StartInfo.UseShellExecute = False
                        p.StartInfo.RedirectStandardInput = True
                        p.StartInfo.RedirectStandardOutput = True
                        p.StartInfo.RedirectStandardError = True
                        p.StartInfo.CreateNoWindow = False
                        p.Start()
                        p.StandardInput.WriteLine("dir")
                        p.StandardInput.WriteLine(backcommand)
                        p.StandardInput.WriteLine("exit")
                        MessageBox.Show("提示：数据库恢复成功！")
                        p.Close()
                    End If
                Catch ex As Exception
                    MessageBox.Show("无法恢复指定的数据库，请联系开发人员！" & ex.Message)
                End Try
            Else
                MessageBox.Show("请首先输入正确的数据库连接信息来通过连接测试后，再进行数据库备份操作！")
            End If
        Else
            MessageBox.Show("请点击刷新列表按钮，获得要操作的数据表")
        End If
    End Sub

    Private Sub BtnChkAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnChkAll.Click
        Dim i As Integer
        For i = 0 To Me.ChkdListBox.Items.Count - 1
            Me.ChkdListBox.SetItemChecked(i, True)
        Next
    End Sub

    '刷新选择的表
    Private Sub RefreshSelectTable()
        Dim i, j As Integer
        For i = 1 To Me.chkSelect.Items.Count
            Select Case Me.chkSelect.Items(i - 1)
                Case "路网基础数据表"
                    If Me.chkSelect.GetItemChecked(i - 1) = True Then
                        For j = 0 To Me.ChkdListBox.Items.Count - 1
                            If Me.ChkdListBox.Items(j).ToString.Substring(0, 2) = "PD" Then
                                Me.ChkdListBox.SetItemChecked(j, True)
                            End If
                        Next
                    Else
                        For j = 0 To Me.ChkdListBox.Items.Count - 1
                            If Me.ChkdListBox.Items(j).ToString.Substring(0, 2) = "PD" Then
                                Me.ChkdListBox.SetItemChecked(j, False)
                            End If
                        Next
                    End If
                Case "客流相关数据表"
                    If Me.chkSelect.GetItemChecked(i - 1) = True Then
                        For j = 0 To Me.ChkdListBox.Items.Count - 1
                            If Me.ChkdListBox.Items(j).ToString.Substring(0, 3) = "MPS" Or Me.ChkdListBox.Items(j).ToString.Substring(0, 3) = "PTL" Then
                                Me.ChkdListBox.SetItemChecked(j, True)
                            End If
                        Next
                    Else
                        For j = 0 To Me.ChkdListBox.Items.Count - 1
                            If Me.ChkdListBox.Items(j).ToString.Substring(0, 3) = "MPS" Or Me.ChkdListBox.Items(j).ToString.Substring(0, 3) = "PTL" Then
                                Me.ChkdListBox.SetItemChecked(j, False)
                            End If
                        Next
                    End If

                Case "运行图相关数据表"
                    If Me.chkSelect.GetItemChecked(i - 1) = True Then
                        For j = 0 To Me.ChkdListBox.Items.Count - 1
                            If Me.ChkdListBox.Items(j).ToString.Substring(0, 3) = "TMS" Then
                                Me.ChkdListBox.SetItemChecked(j, True)
                            End If
                        Next
                    Else
                        For j = 0 To Me.ChkdListBox.Items.Count - 1
                            If Me.ChkdListBox.Items(j).ToString.Substring(0, 3) = "TMS" Then
                                Me.ChkdListBox.SetItemChecked(j, False)
                            End If
                        Next
                    End If

                Case "大客流处置相关数据表"
                    If Me.chkSelect.GetItemChecked(i - 1) = True Then
                        For j = 0 To Me.ChkdListBox.Items.Count - 1
                            If Me.ChkdListBox.Items(j).ToString.Substring(0, 3) = "PAS" Then
                                Me.ChkdListBox.SetItemChecked(j, True)
                            End If
                        Next
                    Else
                        For j = 0 To Me.ChkdListBox.Items.Count - 1
                            If Me.ChkdListBox.Items(j).ToString.Substring(0, 3) = "PAS" Then
                                Me.ChkdListBox.SetItemChecked(j, False)
                            End If
                        Next
                    End If

                Case "首末班车与换乘衔接数据表"
                    If Me.chkSelect.GetItemChecked(i - 1) = True Then
                        For j = 0 To Me.ChkdListBox.Items.Count - 1
                            If Me.ChkdListBox.Items(j).ToString.Substring(0, 3) = "TBL" Then
                                Me.ChkdListBox.SetItemChecked(j, True)
                            End If
                        Next
                    Else
                        For j = 0 To Me.ChkdListBox.Items.Count - 1
                            If Me.ChkdListBox.Items(j).ToString.Substring(0, 3) = "TBL" Then
                                Me.ChkdListBox.SetItemChecked(j, False)
                            End If
                        Next
                    End If

            End Select
        Next
    End Sub
    Private Sub frmDataManager_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call BtnRefresh_Click(Nothing, Nothing)
        Dim i As Integer
        For i = 1 To Me.chkSelect.Items.Count
            Me.chkSelect.SetItemChecked(i - 1, True)
        Next
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Call RefreshSelectTable()
    End Sub
End Class
