
Public Class JGexcel
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Dim xlapp As Microsoft.Office.Interop.Excel.Application
        Dim xlbook As Microsoft.Office.Interop.Excel.Workbook
        Dim xlsheet As Microsoft.Office.Interop.Excel.Worksheet

        xlapp = New Microsoft.Office.Interop.Excel.Application()
        xlbook = xlapp.Workbooks.Add
        xlsheet = xlbook.Sheets(1)
        xlsheet.Name = "轮值表"


        Dim str As String
        Dim tab As New DataTable
        str = "select driverno,Lineid,starttime,startstaname,endtime,endstaname,trainno,upordown from cs_crewschedule t where cstimetableid='" & TextBox1.Text & "' order by driverno,starttime"
        tab = Globle.Method.ReadDataForAccess(str)

        Dim nf As FrmProgress = New FrmProgress(tab.Rows.Count * 2, "正在导入Excel...")
        xlsheet.Cells.Font.Name = "Arial Unicode MS"  ' 设置字体
        xlsheet.Cells.Font.Size = 9                   '字体大小
        'xlsheet.Cells.WrapText = True                '自动换行

        Dim k As Integer = 2
        Dim j As Integer = 3
        For i As Integer = 0 To tab.Rows.Count - 1
            If i = 0 Then
                xlsheet.Cells(1, 1) = "编号：" & tab.Rows(i).Item("driverno")
                xlsheet.Cells(2, 1) = tab.Rows(i).Item("startstaname") & Coordination2.Global.BeTime(tab.Rows(i).Item("starttime"))
                xlsheet.Cells(3, 2).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown).LineStyle = 1
                xlsheet.Cells(4, 3).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown).LineStyle = 1

            ElseIf tab.Rows(i).Item("driverno") = tab.Rows(i - 1).Item("driverno") Then
                If tab.Rows(i).Item("upordown") = tab.Rows(i - 1).Item("upordown") Then
                    Continue For
                Else
                    j = j + 1
                    k = k + 2
                    'If j <= 2 Then
                    '    xlsheet.Cells(k, 4) = tab.Rows(i - 1).Item("endstaname") & Coordination2.Global.BeTime(tab.Rows(i - 1).Item("endtime"))
                    'Else
                    If j Mod 2 = 0 Then
                        xlsheet.Cells(k, 4) = tab.Rows(i - 1).Item("endstaname") & Coordination2.Global.BeTime(tab.Rows(i - 1).Item("endtime"))
                        xlsheet.Cells(k + 1, 4) = tab.Rows(i).Item("startstaname") & Coordination2.Global.BeTime(tab.Rows(i).Item("starttime"))
                        xlsheet.Cells(k + 1, 3).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalUp).LineStyle = 1
                        xlsheet.Cells(k + 2, 2).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalUp).LineStyle = 1
                    Else
                        xlsheet.Cells(k, 1) = tab.Rows(i - 1).Item("endstaname") & Coordination2.Global.BeTime(tab.Rows(i - 1).Item("endtime"))
                        xlsheet.Cells(k + 1, 1) = tab.Rows(i).Item("startstaname") & Coordination2.Global.BeTime(tab.Rows(i).Item("starttime"))
                        xlsheet.Cells(k + 1, 2).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown).LineStyle = 1
                        xlsheet.Cells(k + 2, 3).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown).LineStyle = 1
                    End If
                End If

            ElseIf tab.Rows(i).Item("driverno") <> tab.Rows(i - 1).Item("driverno") Then
                k = k + 6
                xlsheet.Cells(k - 1, 1) = "编号：" & tab.Rows(i).Item("driverno")
                xlsheet.Cells(k, 1) = tab.Rows(i).Item("startstaname") & Coordination2.Global.BeTime(tab.Rows(i).Item("starttime"))
                xlsheet.Cells(k + 1, 2).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown).LineStyle = 1
                xlsheet.Cells(k + 2, 3).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown).LineStyle = 1
                j = 3
                xlsheet.Range(xlsheet.Cells(k - 5, 2), xlsheet.Cells(k - 4, 3)).Clear()
            End If
            nf.Performstep()
        Next

        xlapp.Visible = True
        nf.EndProgress()
    End Sub
End Class