Imports System.IO

Public Module Mod1

    Public CurLineName As String = ""
    Public CurrentUserRole As String
    
    Public Sub UpdateData(ByVal SqlStr As String)        '更新数据
        Globle.Method.UpdateDataForAccess(SqlStr)
    End Sub

    Public Function ReadData(ByVal SqlStr As String) As DataTable          '读取数据
        ReadData = Nothing
        Dim tempTab As New DataTable
        tempTab = Globle.Method.ReadDataForAccess(SqlStr)
        ReadData = tempTab
    End Function
    Public Sub OutPutToEXCELFileFormDataGrid(ByVal ExcelTitle As String, ByVal Dtg As DataGridView, ByVal frmForm As Form)
        Try
            If Dtg.RowCount > 0 Then
                frmForm.Cursor = Cursors.WaitCursor
                Dim i, j, p As Integer
                Dim rows As Integer = Dtg.Rows.Count
                Dim cols As Integer = Dtg.ColumnCount
                Dim DataArray(rows - 1, cols - 1) As String
                Dim myExcel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application
                Dim myBook As Microsoft.Office.Interop.Excel.Workbook
                Dim mySheet As Microsoft.Office.Interop.Excel.Worksheet
                myBook = myExcel.Workbooks.Add(System.Reflection.Missing.Value)     '添加一个新的BOOK

                mySheet = myBook.Worksheets.Add     '添加一个新的SHEET
                mySheet.Name = ExcelTitle

                For i = 0 To rows - 1
                    For j = 0 To cols - 1
                        DataArray(i, j) = Dtg.Rows(i).Cells(j).Value.ToString
                    Next
                Next

                For j = 0 To cols - 1
                    myExcel.Cells(1, j + 1) = Dtg.Columns(j).HeaderText '.name
                Next

                For i = 0 To DataArray.GetLength(0) - 1
                    For j = 0 To DataArray.GetLength(1) - 1
                        myExcel.Cells(i + 2, j + 1) = DataArray(i, j)
                    Next
                Next

                For p = 1 To cols
                    mySheet.Columns(p).EntireColumn.AutoFit()
                Next p

                myExcel.Visible = True
                GC.Collect()
            Else
                MessageBox.Show("没有数据!", frmForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch exp As Exception
            MessageBox.Show("数据导出失败!请查看是否已经安装了Excel", frmForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            frmForm.Cursor = Cursors.Default
        End Try
    End Sub
    Public Sub LoadAllDrivers(ByVal TeamList As List(Of DriverTeam), ByVal LineID As String)
        TeamList.Clear()
        Dim str As String = "select * from cs_driverinf where lineid='" & LineID & "' and (beteam is not null and beteam<>0) order by beteam,rdriverno"
        Dim tab As DataTable = ReadData(str)
        If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
            For i As Integer = 0 To tab.Rows.Count - 1
                If i > 0 AndAlso tab.Rows(i).Item("beteam").ToString = tab.Rows(i - 1).Item("beteam").ToString Then
                    Dim beteam As String = tab.Rows(i).Item("beteam").ToString
                    Dim tempTeam As DriverTeam = TeamList.Find(Function(value As DriverTeam)
                                                                   Return value.TeamNo = beteam
                                                               End Function)
                    Dim tempDriver As New DriverDetial(tab.Rows(i).Item("rdriverno").ToString, tab.Rows(i).Item("drivername").ToString, tab.Rows(i).Item("gender").ToString, _
                        tab.Rows(i).Item("nationality").ToString, tab.Rows(i).Item("birthday").ToString, tab.Rows(i).Item("enrolltime").ToString, tab.Rows(i).Item("lineid").ToString, _
                        tab.Rows(i).Item("beclass").ToString, tab.Rows(i).Item("techgrade").ToString, tab.Rows(i).Item("post").ToString, tab.Rows(i).Item("phone").ToString, tab.Rows(i).Item("bezone").ToString, tab.Rows(i).Item("beteam").ToString)
                    tempTeam.Drivers.Add(tempDriver)
                Else
                    Dim tempDriver As New DriverDetial(tab.Rows(i).Item("rdriverno").ToString, tab.Rows(i).Item("drivername").ToString, tab.Rows(i).Item("gender").ToString, _
                        tab.Rows(i).Item("nationality").ToString, tab.Rows(i).Item("birthday").ToString, tab.Rows(i).Item("enrolltime").ToString, tab.Rows(i).Item("lineid").ToString, _
                        tab.Rows(i).Item("beclass").ToString, tab.Rows(i).Item("techgrade").ToString, tab.Rows(i).Item("post").ToString, tab.Rows(i).Item("phone").ToString, tab.Rows(i).Item("bezone").ToString, tab.Rows(i).Item("beteam").ToString)
                    Dim tempTeam As New DriverTeam(LineID, tempDriver.BeClass, tempDriver.BeTeam)
                    tempTeam.Drivers.Add(tempDriver)
                    TeamList.Add(tempTeam)
                End If
            Next
        End If
    End Sub

    Public Function GetWeekDayString(ByVal da As Date) As String
        GetWeekDayString = ""
        Select Case da.DayOfWeek.ToString
            Case "Monday"
                Return "星期一"
            Case "Tuesday"
                Return "星期二"
            Case "Wednesday"
                Return "星期三"
            Case "Thursday"
                Return "星期四"
            Case "Friday"
                Return "星期五"
            Case "Saturday"
                Return "星期六"
            Case "Sunday"
                Return "星期天"
        End Select
    End Function

End Module
