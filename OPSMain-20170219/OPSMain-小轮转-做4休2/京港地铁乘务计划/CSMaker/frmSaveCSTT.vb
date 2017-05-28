Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class frmSaveCSTT
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim tmpName As String = Me.txtCurCSSKB.Text.ToString.Trim
        If tmpName = "" Then
            MsgBox("请输入乘务计划表名称！", MsgBoxStyle.OkOnly)
        Else
            Try
                Call SaveResults(tmpName)
            Catch ex As Exception
                MsgBox("遇到错误：" & ex.ToString() & vbCrLf & "乘务计划表没有保存成功！", MsgBoxStyle.OkOnly)
            End Try
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
End Class