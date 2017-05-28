Imports Microsoft.Office.Interop.Excel

Public Class GlobalFunc

    Public Shared Function ConvStringToDate(ByVal DateExpression As String) As Date
        Dim Year As String
        Dim Month As String
        Dim Day As String

        Year = Left$(DateExpression, 4)
        Month = Mid$(DateExpression, 5, 2)
        Day = Right$(DateExpression, 2)

        ConvStringToDate = CDate(Year & "-" & Month & "-" & Day)
    End Function

    Public Shared Function ConvStringToTime(ByVal timestring As String) As DateTime
        Dim str() As String = Split(timestring, ".")
        If str.Length > 1 Then
            If str(1).Length = 1 Then
                str(1) &= "0"
            End If
        Else
            ReDim Preserve str(1)
            str(1) &= "0"
        End If

        Dim temstr As String = str(0) & ":" & str(1)
        ConvStringToTime = CDate(temstr)
    End Function

    Public Shared Function IsWeekend(ByVal da As Date) As Boolean
        Select Case da.DayOfWeek.ToString
            Case "Monday"
                Return False
            Case "Tuesday"
                Return False
            Case "Wednesday"
                Return False
            Case "Thursday"
                Return False
            Case "Friday"
                Return False
            Case "Saturday"
                Return True
            Case "Sunday"
                Return True
        End Select
    End Function

    Public Shared Function GetWeekDayString(ByVal da As Date) As String
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

    Public Shared Sub DivideClass(ByVal Linename As String, ByVal Yunzhuanpara As String, ByVal DriverNum As Integer)
     

        Select Case Yunzhuanpara
            Case "四班二转"
                Dim Str As String
                Str = "declare " & _
                            "cursor cs_cursor is " & _
                            "select * from cs_driverinf t " & _
                            "where t.lineid='" & Linename & "' order by t.rdriverno; " & _
                            "begin " & _
                            "for my_cur in cs_cursor " & _
                            "loop " & _
                            "update cs_driverinf m " & _
                            "set m.beclass= '其它' " & _
                            "where m.lineid=my_cur.lineid and m.rdriverno=my_cur.rdriverno; " & _
                            "end loop; " & _
                            "commit; " & _
                            "end; "

                Globle.Method.UpdateDataForAccess(Str)

                For i As Integer = 3 To 0 Step -1
                    Str = "declare " & _
                            "cursor cs_cursor is " & _
                            "select * from (select * from cs_driverinf order by rdriverno) t " & _
                            "where rownum<=" & (i + 1) * (DriverNum / 4) & " and t.lineid='" & Linename & "' ; " & _
                            "begin " & _
                            "for my_cur in cs_cursor " & _
                            "loop " & _
                            "update cs_driverinf m " & _
                            "set m.beclass='" & (i + 1) & "班' " & _
                            "where m.lineid=my_cur.lineid and m.rdriverno=my_cur.rdriverno; " & _
                            "end loop; " & _
                            "commit; " & _
                            "end;"
                     Globle.Method.UpdateDataForAccess(Str)
                Next

            Case "四班三转"

                Dim Str As String
                Str = "declare " & _
                            "cursor cs_cursor is " & _
                            "select * from cs_driverinf t " & _
                            "where t.lineid='" & Linename & "' order by t.rdriverno; " & _
                            "begin " & _
                            "for my_cur in cs_cursor " & _
                            "loop " & _
                            "update cs_driverinf m " & _
                            "set m.beclass= '其它' " & _
                            "where m.lineid=my_cur.lineid and m.rdriverno=my_cur.rdriverno; " & _
                            "end loop; " & _
                            "commit; " & _
                            "end; "

               Globle.Method.UpdateDataForAccess(Str)

                For i As Integer = 3 To 0 Step -1
                    Str = "declare " & _
                            "cursor cs_cursor is " & _
                            "select * from (select * from cs_driverinf order by rdriverno) t " & _
                            "where rownum<=" & (i + 1) * (DriverNum / 4) & " and t.lineid='" & Linename & "' ; " & _
                            "begin " & _
                            "for my_cur in cs_cursor " & _
                            "loop " & _
                            "update cs_driverinf m " & _
                            "set m.beclass='" & (i + 1) & "班' " & _
                            "where m.lineid=my_cur.lineid and m.rdriverno=my_cur.rdriverno; " & _
                            "end loop; " & _
                            "commit; " & _
                            "end;"
                     Globle.Method.UpdateDataForAccess(Str)
                Next
            Case "五班三转"

                Dim Str As String
                Str = "declare " & _
                            "cursor cs_cursor is " & _
                            "select * from cs_driverinf t " & _
                            "where t.lineid='" & Linename & "' order by t.rdriverno; " & _
                            "begin " & _
                            "for my_cur in cs_cursor " & _
                            "loop " & _
                            "update cs_driverinf m " & _
                            "set m.beclass= '其它' " & _
                            "where m.lineid=my_cur.lineid and m.rdriverno=my_cur.rdriverno; " & _
                            "end loop; " & _
                            "commit; " & _
                            "end; "
                 Globle.Method.UpdateDataForAccess(Str)

                For i As Integer = 4 To 0 Step -1
                    Str = "declare " & _
                            "cursor cs_cursor is " & _
                            "select * from (select * from cs_driverinf order by rdriverno) t " & _
                            "where rownum<=" & (i + 1) * (DriverNum / 5) & " and t.lineid='" & Linename & "' ; " & _
                            "begin " & _
                            "for my_cur in cs_cursor " & _
                            "loop " & _
                            "update cs_driverinf m " & _
                            "set m.beclass='" & (i + 1) & "班' " & _
                            "where m.lineid=my_cur.lineid and m.rdriverno=my_cur.rdriverno; " & _
                            "end loop; " & _
                            "commit; " & _
                            "end;"
                      Globle.Method.UpdateDataForAccess(Str)
                Next
            Case "六班三转"
                Dim Str As String
                Str = "declare " & _
                            "cursor cs_cursor is " & _
                            "select * from cs_driverinf t " & _
                            "where t.lineid='" & Linename & "' order by t.rdriverno; " & _
                            "begin " & _
                            "for my_cur in cs_cursor " & _
                            "loop " & _
                            "update cs_driverinf m " & _
                            "set m.beclass= '其它' " & _
                            "where m.lineid=my_cur.lineid and m.rdriverno=my_cur.rdriverno; " & _
                            "end loop; " & _
                            "commit; " & _
                            "end; "
                 Globle.Method.UpdateDataForAccess(Str)

                For i As Integer = 5 To 0 Step -1
                    Str = "declare " & _
                            "cursor cs_cursor is " & _
                            "select * from (select * from cs_driverinf order by rdriverno) t " & _
                            "where rownum<=" & (i + 1) * (DriverNum / 6) & " and t.lineid='" & Linename & "' ; " & _
                            "begin " & _
                            "for my_cur in cs_cursor " & _
                            "loop " & _
                            "update cs_driverinf m " & _
                            "set m.beclass='" & (i + 1) & "班' " & _
                            "where m.lineid=my_cur.lineid and m.rdriverno=my_cur.rdriverno; " & _
                            "end loop; " & _
                            "commit; " & _
                            "end;"
                    Globle.Method.UpdateDataForAccess(Str)
                Next
        End Select

    End Sub

    Public Shared Sub OutPutToEXCELFileFormDataGrid(ByVal DtgList As List(Of DataGridView), ByVal frmForm As Form)
        frmForm.Cursor = Cursors.WaitCursor
        Dim myExcel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()
        myExcel.Application.DisplayAlerts = False
        Dim myBook As Microsoft.Office.Interop.Excel.Workbook = myExcel.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet)
        For Each Dtg As DataGridView In DtgList
            If Dtg.Rows.Count > 0 Then
                Dim rows As Integer = Dtg.RowCount
                Dim cols As Integer = Dtg.ColumnCount
                Dim DataArray(rows - 1, cols - 1) As String
                Dim mySheet As Microsoft.Office.Interop.Excel.Worksheet = New Microsoft.Office.Interop.Excel.Worksheet()
                mySheet = myBook.Sheets.Add()
                mySheet.Name = Dtg.Parent.Text

                For i As Integer = 0 To rows - 1
                    For j As Integer = 0 To cols - 1
                        DataArray(i, j) = Dtg.Rows(i).Cells(j).Value.ToString
                    Next
                Next

                For j As Integer = 0 To cols - 1
                    mySheet.Cells(1, j + 1) = Dtg.Columns(j).HeaderText
                Next

                Dim pro As New FrmProgress(rows * cols, "正在导出" & mySheet.Name & "的班表...")
                For i As Integer = 0 To rows - 1
                    For j As Integer = 0 To cols - 1
                        mySheet.Cells(i + 2, j + 1) = DataArray(i, j).ToString.Trim()
                        If DataArray(i, j).Length >= 2 Then
                            Select Case DataArray(i, j).ToString.Trim.Substring(0, 2)
                                Case "早班"
                                    mySheet.Range(mySheet.Cells(i + 2, j + 1), mySheet.Cells(i + 2, j + 1)).Font.Color = RGB(32, 11, 225)
                                Case "白班"
                                    mySheet.Range(mySheet.Cells(i + 2, j + 1), mySheet.Cells(i + 2, j + 1)).Font.Color = RGB(0, 100, 0)
                                Case "夜班"
                                    mySheet.Range(mySheet.Cells(i + 2, j + 1), mySheet.Cells(i + 2, j + 1)).Font.Color = RGB(0, 100, 0)
                                Case "休息"
                                    mySheet.Range(mySheet.Cells(i + 2, j + 1), mySheet.Cells(i + 2, j + 1)).Font.Color = RGB(255, 0, 0)
                                Case Else
                            End Select
                        End If
                        pro.Performstep()
                    Next
                Next
                pro.Close()

                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(1, cols)).Interior.ColorIndex = 20       '格式
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(rows + 1, 1)).Interior.ColorIndex = 20
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(1, cols)).Font.Name = "宋体"
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(1, cols)).Font.Size = 9
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(1, cols)).Font.Bold = True
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(rows + 1, 1)).Interior.ColorIndex = 20
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(rows + 1, 1)).Font.Name = "宋体"
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(rows + 1, 1)).Font.Size = 9
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(rows + 1, 1)).Font.Bold = True
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(rows + 1, cols)).Borders.LineStyle = 1
                mySheet.Range(mySheet.Cells(2, 2), mySheet.Cells(rows + 1, cols)).Font.Name = "华文细黑"
                mySheet.Range(mySheet.Cells(2, 2), mySheet.Cells(rows + 1, cols)).Font.Size = 9
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(rows + 1, cols)).HorizontalAlignment = XlHAlign.xlHAlignCenter
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(rows + 1, cols)).VerticalAlignment = XlHAlign.xlHAlignCenter
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(rows + 1, cols)).Columns.ColumnWidth = 9
                mySheet.Columns.AutoFit()
            Else
                MessageBox.Show("没有数据!", frmForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Next
        myExcel.Visible = True
        GC.Collect()
        frmForm.Cursor = Cursors.Default
    End Sub

    Public Shared Sub LoadDepotData()               '加载一些车场、轮换站、以及一些基本参数信息
        DepotPlaces = New List(Of String)
        SheftPlaces = New List(Of String)
        ChangePlaces = New List(Of String)
        '===================================================================================读取车站类型信息
        Dim Str As String = "select * from cs_shiftplaceinf where lineid='" & CurLineName & "'"
        Dim ShiftPlacestable As New Data.DataTable
        ShiftPlacestable = Globle.Method.ReadDataForAccess(Str)
      

        Str = "select t.changeplace from cs_changeplaceinf t where lineid='" & CurLineName & "' group by t.changeplace"
        Dim ChangePlacestable As New Data.DataTable
        ChangePlacestable = Globle.Method.ReadDataForAccess(Str)


        Str = "select t.stationname from tms_stationinfo t where t.stationtype='车场' group by t.stationname"
        Dim DepotPlacestable As New Data.DataTable
        DepotPlacestable = Globle.Method.ReadDataForAccess(Str)
       
        Str = "select * from cs_crewbasicinf t where lineid='" & CurLineName & "'"
        Dim Basicparatab As New Data.DataTable
        Basicparatab = Globle.Method.ReadDataForAccess(Str)
        

        If DepotPlacestable.Rows.Count > 0 Then             '====车场
            For i As Integer = 0 To DepotPlacestable.Rows.Count - 1
                DepotPlaces.Add(DepotPlacestable.Rows(i)("stationname").ToString().Trim())
            Next
        End If
        DepotPlacestable.Dispose()
        If ShiftPlacestable.Rows.Count > 0 Then
            For i As Integer = 0 To ShiftPlacestable.Rows.Count - 1
                Dim isexit As Boolean = False
                For j As Integer = 0 To DepotPlaces.Count - 1
                    If ShiftPlacestable.Rows(i)("shiftplace").ToString().Trim() = DepotPlaces(j) Then
                        isexit = True
                    End If
                Next
                If isexit = False Then
                    SheftPlaces.Add(ShiftPlacestable.Rows(i)("shiftplace").ToString().Trim())
                End If
            Next
        End If
        ShiftPlacestable.Dispose()

        If ChangePlacestable.Rows.Count > 0 Then
            For i As Integer = 0 To ChangePlacestable.Rows.Count - 1
                Dim isexit As Boolean = False
                For j As Integer = 0 To SheftPlaces.Count - 1
                    If ChangePlacestable.Rows(i)("changeplace").ToString().Trim() = SheftPlaces(j) Then
                        isexit = True
                    End If
                Next
                If isexit = False Then
                    ChangePlaces.Add(ChangePlacestable.Rows(i)("changeplace").ToString().Trim())
                End If
            Next
        End If
        ChangePlacestable.Dispose()

        If Basicparatab.Rows.Count > 0 Then
            BasicPara = New BasicParameter
            BasicPara.Time1 = CDate(Basicparatab.Rows(0).Item("time1")).TimeOfDay.TotalSeconds
            BasicPara.Time2 = CDate(Basicparatab.Rows(0).Item("time2")).TimeOfDay.TotalSeconds
            BasicPara.Time3 = CDate(Basicparatab.Rows(0).Item("time3")).TimeOfDay.TotalSeconds
            BasicPara.ConDrivetime = CDate(Basicparatab.Rows(0).Item("ConDrivetime")).TimeOfDay.TotalSeconds
            'BasicPara.DayWorktime = CDate(Basicparatab.Rows(0).Item("DayWorktime")).TimeOfDay.TotalSeconds
            BasicPara.PrepareDutytime = CDate(Basicparatab.Rows(0).Item("PrepareDutytime")).TimeOfDay.TotalSeconds
            BasicPara.PrepareTraintime = CDate(Basicparatab.Rows(0).Item("PrepareTraintime")).TimeOfDay.TotalSeconds
            'BasicPara.Resttime = CDate(Basicparatab.Rows(0).Item("Resttime")).TimeOfDay.TotalSeconds
        End If
        Basicparatab.Dispose()
    End Sub

    Public Shared Function GetMaxValue(ByVal a As List(Of Decimal)) As Decimal
        'GetMaxValue = -10000
        If a.Count > 0 Then
            GetMaxValue = a(0)
            For i As Integer = 0 To a.Count - 1
                If a(i) > GetMaxValue Then
                    GetMaxValue = a(i)
                End If
            Next
        End If
    End Function

    Public Shared Function GetMinValue(ByVal a As List(Of Decimal)) As Decimal
        'GetMinValue = 10000
        If a.Count > 0 Then
            GetMinValue = a(0)
            For i As Integer = 0 To a.Count - 1
                If a(i) < GetMinValue Then
                    GetMinValue = a(i)
                End If
            Next
        End If
    End Function

    Public Shared Function GetSumValue(ByVal a As List(Of Decimal)) As Decimal
        GetSumValue = 0
        If a.Count > 0 Then
            For i As Integer = 0 To a.Count - 1
                GetSumValue += a(i)
            Next
        End If
    End Function

    Public Shared Function GetAveValue(ByVal a As List(Of Decimal)) As Decimal
        If a.Count > 0 Then
            GetAveValue = GetSumValue(a) / a.Count
        End If
    End Function

    Public Shared Function GetVarValue(ByVal a As List(Of Decimal)) As Decimal
        If a.Count > 0 Then
            Dim AveValue As Decimal = GetAveValue(a)
            Dim Var2 As Decimal = 0
            For i As Integer = 0 To a.Count - 1
                Var2 += (a(i) - AveValue) ^ 2
            Next
            GetVarValue = Var2 / a.Count
        End If
    End Function

    Public Shared Function MyCeiLing(ByVal _Para As Decimal, ByVal Value As Decimal) As Decimal
        MyCeiLing = IIf(Value Mod _Para = 0, Value, Int(Value / _Para) * _Para + _Para)
    End Function

End Class

Public Class BasicParameter     '基本参数
    Public Time1 As Integer
    Public Time2 As Integer
    Public Time3 As Integer
    Public DinBetime As Integer
    Public DinEndtime As Integer
    Public Dinnertime As Integer
    Public ConDrivetime As Integer
    'Public Resttime As Integer
    Public DayWorktime As Integer
    Public PrepareTraintime As Integer
    Public PrepareDutytime As Integer
End Class