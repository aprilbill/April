Imports System.IO
Imports System.Data.OleDb
Public Class FrmAvaAlDayInput

    Private Sub ViewButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewButton.Click

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
        If MsgBox("导入数据后将更新所有司机的年假信息，确定导入？", MsgBoxStyle.OkCancel, "提示") = MsgBoxResult.Ok Then
            Dim str As String = ""
            Me.ProgressBar1.Visible = True
            Me.ProgressBar1.Step = 1
            Me.ProgressBar1.Maximum = (Me.AddFileDataGridView.Rows.Count)
            If Me.AddFileDataGridView.Rows.Count > 0 Then
                Try
                    For i As Integer = 0 To Me.AddFileDataGridView.Rows.Count - 1
                        Dim LineName As String = Me.AddFileDataGridView.Rows(i).Cells("线路").Value
                        Dim AvaAlDay As Integer = Me.AddFileDataGridView.Rows(i).Cells("剩余年假").Value
                        Dim DriverNo As String = Me.AddFileDataGridView.Rows(i).Cells("工号").Value.ToString
                        str = "Update CS_DRIVERINF set avaalday=" & AvaAlDay & " where lineid='" & LineName & "' and rdriverno='" & DriverNo & "'"
                        UpdateData(str)
                        Me.ProgressBar1.PerformStep()
                    Next
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.OkCancel, "提示")
                    Exit Sub
                End Try
            End If
            Me.ProgressBar1.Visible = False
            MsgBox("更新完成！")
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub BtnCancle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancle.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

   
End Class