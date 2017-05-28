Imports System.IO
Imports System.Data.OleDb
Public Class DriverInfoLoad

    Private Sub ViewButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewButton.Click
        'Dim fName As String = ""
        'Me.OpenFileDialog.InitialDirectory = "C:\Documents and Settings\Administrator\桌面\"
        'Me.OpenFileDialog.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*"
        'Me.OpenFileDialog.FilterIndex = 1
        'Me.OpenFileDialog.RestoreDirectory = True
        'If (OpenFileDialog.ShowDialog() = Windows.Forms.DialogResult.OK) Then
        '    fName = OpenFileDialog.FileName
        '    Me.FilePathTextBox.Text = fName

        '    Dim TextLine As String = ""
        '    Dim SplitLine() As String

        '    If System.IO.File.Exists(fName) = True Then
        '        Me.AddFileDataGridView.Rows.Clear()
        '        Dim objReader As New System.IO.StreamReader(fName, System.Text.Encoding.Default)
        '        If objReader.EndOfStream = False Then        '读标题行
        '            TextLine = objReader.ReadLine()
        '            SplitLine = Split(TextLine, ",")
        '            Dim ifCoorect As Boolean = True
        '            If SplitLine.Length <> Me.AddFileDataGridView.Columns.Count Then
        '                ifCoorect = False
        '            Else
        '                For i As Integer = 0 To SplitLine.Length - 1
        '                    If SplitLine(i).Trim <> Me.AddFileDataGridView.Columns(i).Name Then
        '                        ifCoorect = False
        '                    End If
        '                Next
        '            End If
        '            If ifCoorect = False Then
        '                MsgBox("文件格式不匹配！")
        '                Exit Sub
        '            End If
        '        End If
        '        While objReader.EndOfStream = False
        '            TextLine = objReader.ReadLine()
        '            SplitLine = Split(TextLine, ",")
        '            Me.AddFileDataGridView.Rows.Add(SplitLine)
        '        End While
        '    Else
        '        MsgBox("文件不存在")
        '    End If
        'End If

        Dim New0penFile As New OpenFileDialog
        Dim strExcelFilePath As String
        New0penFile.Filter = "xls files (*.xls)|*.xls|xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        New0penFile.FilterIndex = 1
        New0penFile.RestoreDirectory = True
        strExcelFilePath = ""

        If New0penFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strExcelFilePath = New0penFile.FileName
        End If
        '获得数据库的名称
        If strExcelFilePath <> "" Then

            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
            Dim MyConnection As System.Data.OleDb.OleDbConnection
            MyConnection = New System.Data.OleDb.OleDbConnection( _
                          "provider=Microsoft.Jet.OLEDB.4.0; " & _
                          "data source='" & strExcelFilePath & "'; " & _
                          "Extended Properties=Excel 8.0;")
            Dim tmpStr As String
            tmpStr = "select * from" & "[乘务员信息表$]"
            MyCommand = New System.Data.OleDb.OleDbDataAdapter(tmpStr, MyConnection)
            MyConnection.Open()

            Dim objDataset1 As New DataSet
            Try
                MyCommand.Fill(objDataset1, "XLData")
                Dim temTab As Data.DataTable = objDataset1.Tables(0)
                Me.AddFileDataGridView.DataSource = objDataset1.Tables(0)
                MsgBox("打开正确，请检查数据的正确性！")
            Catch ex As Exception
                MsgBox("EXCEL数据库不正确，请确定打开的数据库格式正确!")
            End Try
            MyConnection.Close()
            GC.Collect()
        End If
    End Sub

    Private Sub ImportDBButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportDBButton.Click

        If MsgBox("导入数据后将覆盖以前所有数据，确定导入？", MsgBoxStyle.OkCancel, "提示") = MsgBoxResult.Ok Then
            Dim str As String = ""
            Me.ProgressBar1.Visible = True
            Me.ProgressBar1.Step = 1
            Me.ProgressBar1.Maximum = (Me.AddFileDataGridView.Rows.Count)
            If Me.AddFileDataGridView.Rows.Count > 0 Then
                Try
                    str = "delete from cs_driverinf where lineid='" & CurLineName & "'"
                    UpdateData(str)
                    For i As Integer = 0 To Me.AddFileDataGridView.Rows.Count - 1
                        str = "INSERT INTO CS_DRIVERINF " _
                                                & "(lineid,beclass,rdriverno,drivername, techgrade, post, phone,bezone,idleornot, reasonforunavailable, KM,enrolltime,starlevel,apprentice,marelation,unicode)" _
                                                & "VALUES(" _
                                                        & "'" & Me.AddFileDataGridView.Rows(i).Cells("线路").Value.ToString.Trim _
                                                        & "','" & Me.AddFileDataGridView.Rows(i).Cells("班组").Value.ToString.Trim _
                                                        & "','" & Me.AddFileDataGridView.Rows(i).Cells("工号").Value.ToString.Trim _
                                                        & "','" & Me.AddFileDataGridView.Rows(i).Cells("姓名").Value.ToString.Trim _
                                                        & "','" & Me.AddFileDataGridView.Rows(i).Cells("职称").Value.ToString.Trim _
                                                        & "','" & Me.AddFileDataGridView.Rows(i).Cells("岗位").Value.ToString.Trim _
                                                        & "','" & Me.AddFileDataGridView.Rows(i).Cells("电话").Value.ToString.Trim _
                                                         & "','" & Me.AddFileDataGridView.Rows(i).Cells("区域").Value.ToString.Trim _
                                                        & "','" & Me.AddFileDataGridView.Rows(i).Cells("是否可用").Value.ToString.Trim _
                                                        & "','" & Me.AddFileDataGridView.Rows(i).Cells("原因").Value.ToString.Trim _
                                                        & "','" & Me.AddFileDataGridView.Rows(i).Cells("安全公里").Value.ToString.Trim _
                                                          & "','" & Me.AddFileDataGridView.Rows(i).Cells("开始统计").Value.ToString.Trim _
                                                        & "','" & Me.AddFileDataGridView.Rows(i).Cells("星级").Value.ToString.Trim _
                                                        & "','" & Me.AddFileDataGridView.Rows(i).Cells("学徒").Value.ToString.Trim _
                                                        & "','" & Me.AddFileDataGridView.Rows(i).Cells("师徒备注").Value.ToString.Trim _
                                                         & "','" & Me.AddFileDataGridView.Rows(i).Cells("工作证编号").Value.ToString.Trim _
                                                & "')"
                        UpdateData(str)
                        Me.ProgressBar1.PerformStep()
                    Next
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.OkCancel, "提示")
                    Exit Sub
                End Try
            End If
            Me.ProgressBar1.Visible = False
            MsgBox("导入成功！")
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub BtnCancle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancle.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New FolderBrowserDialog
        If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            If File.Exists(frm.SelectedPath + "\乘务员信息表模板.xls") Then
                MsgBox("模板已经存在!")
                Exit Sub
            End If
            File.Copy(Application.StartupPath + "\File\乘务员信息表模板.xls", frm.SelectedPath + "\乘务员信息表模板.xls")
            MsgBox("导出成功！")
        End If
    End Sub
End Class