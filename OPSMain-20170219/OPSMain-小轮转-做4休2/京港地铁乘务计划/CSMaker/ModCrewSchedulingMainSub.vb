Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Office.Interop

Public Module ModCrewSchedulingMainSub

    '将运行图数据转化为CSLinkTrain数据
    Public Sub ChangCSTrainToCSLinkTrain()
        If sState <> "乘务计划编制" Then
            Exit Sub
        End If
        Dim i, j, n As Integer
        ReDim CSTrainsAndDrivers.CSLinkTrains(0)
        ReDim CSTrainsAndDrivers.MergedCSLinkTrains(0)
        ErrorInfoList = New List(Of String)
        OutputStationName = New Dictionary(Of String, String)

        For i = 1 To UBound(CSTrainsAndDrivers.CSChedi)                  '合并车场出库和入库的短任务段
            For j = 1 To UBound(CSTrainsAndDrivers.CSChedi(i).CrewSta) - 1
                Dim isRouting As Boolean = False  '判断是否是出库短交路
                If CSTrainsAndDrivers.CSChedi(i).CrewSta(j).IsLast = True Or CSTrainsAndDrivers.CSChedi(i).CrewSta(j).isEqual(CSTrainsAndDrivers.CSChedi(i).CrewSta(j + 1)) Then
                    Continue For
                End If

                Dim tempCSTrain As New CSLinkTrain(CSTrainsAndDrivers.CSChedi(i).CrewSta(j), CSTrainsAndDrivers.CSChedi(i).CrewSta(j + 1), "", CSTrainsAndDrivers.CSChedi(i))
                For n = 0 To ChangeStationList.Count - 1
                    If ChangeStationList(n).JiaoLuName = CSTrainsAndDrivers.CSChedi(i).CrewSta(j).CrewStaName + "-->" + CSTrainsAndDrivers.CSChedi(i).CrewSta(j + 1).CrewStaName Then
                        If ChangeStationList(n).TimeSpanList.StartTime <> ChangeStationList(n).TimeSpanList.EndTime Then
                            If AddLitterTime(ChangeStationList(n).TimeSpanList.StartTime) > AddLitterTime(CSTrainsAndDrivers.CSChedi(i).CrewSta(j).ArriveTime) Or AddLitterTime(ChangeStationList(n).TimeSpanList.EndTime) < AddLitterTime(CSTrainsAndDrivers.CSChedi(i).CrewSta(j).ArriveTime) Then
                                Exit For
                            End If
                        End If
                        isRouting = True
                    End If
                Next
                If tempCSTrain.FirstStation.IsYard = True AndAlso (tempCSTrain.distance < minChuDis And isRouting = False) AndAlso tempCSTrain.SecondStation.IsLast = False Then
                    tempCSTrain = New CSLinkTrain(CSTrainsAndDrivers.CSChedi(i).CrewSta(j), CSTrainsAndDrivers.CSChedi(i).CrewSta(j + 2), "", CSTrainsAndDrivers.CSChedi(i))
                    j += 1
                    ReDim Preserve CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains) + 1)
                    CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains)) = tempCSTrain
                    CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains)).CSTrainID = UBound(CSTrainsAndDrivers.CSLinkTrains)
                    CSTrainsAndDrivers.CSChedi(i).CSLinkTrains.Add(CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains)))
                ElseIf tempCSTrain.SecondStation.IsYard = True AndAlso (tempCSTrain.distance <= minRuDis And isRouting = False) AndAlso tempCSTrain.FirstStation.IsFirst = False Then     '
                    tempCSTrain = New CSLinkTrain(CSTrainsAndDrivers.CSChedi(i).CrewSta(j - 1), CSTrainsAndDrivers.CSChedi(i).CrewSta(j + 1), "", CSTrainsAndDrivers.CSChedi(i))
                    CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains)) = tempCSTrain
                    CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains)).CSTrainID = UBound(CSTrainsAndDrivers.CSLinkTrains)
                    CSTrainsAndDrivers.CSChedi(i).CSLinkTrains.RemoveAt(CSTrainsAndDrivers.CSChedi(i).CSLinkTrains.Count - 1)
                    CSTrainsAndDrivers.CSChedi(i).CSLinkTrains.Add(CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains)))
                ElseIf tempCSTrain.FirstStation.IsLast Then
                    Continue For
                Else
                    ReDim Preserve CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains) + 1)
                    CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains)) = tempCSTrain
                    CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains)).CSTrainID = UBound(CSTrainsAndDrivers.CSLinkTrains)
                    CSTrainsAndDrivers.CSChedi(i).CSLinkTrains.Add(CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains)))
                End If
            Next
        Next

        If ErrorInfoList.Count > 0 Then
            Dim ErrorStr As String = "以下区间未能找到距离信息:"
            For Each temstr As String In ErrorInfoList
                ErrorStr &= vbCrLf & temstr
            Next
            MsgBox(ErrorStr, MsgBoxStyle.OkOnly, "提示信息")
        End If
    End Sub
   

    '五班三转，对车底进行传值
    Public Sub FiveThreeTurn_FitCSChedi()

        Dim i, k, j, x As Integer
        ReDim CSTrainsAndDrivers.CSChedi(0)

        For i = 1 To UBound(CSchediInfo)
            Dim chukuTime, Rukutime As Integer
            chukuTime = CSTrainInf(CSchediInfo(i).nLinkTrain(1)).Starting(CSTrainInf(CSchediInfo(i).nLinkTrain(1)).nPathID(1))
            Rukutime = CSTrainInf(CSchediInfo(i).nLinkTrain(UBound(CSchediInfo(i).nLinkTrain))).Arrival(CSTrainInf(CSchediInfo(i).nLinkTrain(UBound(CSchediInfo(i).nLinkTrain))).nPathID(UBound(CSTrainInf(CSchediInfo(i).nLinkTrain(UBound(CSchediInfo(i).nLinkTrain))).nPathID)))
            CSchediInfo(i).ChukuTime = chukuTime
            CSchediInfo(i).RukuTime = Rukutime
            CSchediInfo(i).sChuKuSta = StationInf(CSTrainInf(CSchediInfo(i).nLinkTrain(1)).nPathID(1)).sStationName
            CSchediInfo(i).sRuKuSta = StationInf(CSTrainInf(CSchediInfo(i).nLinkTrain(UBound(CSchediInfo(i).nLinkTrain))).nPathID(UBound(CSTrainInf(CSchediInfo(i).nLinkTrain(UBound(CSchediInfo(i).nLinkTrain))).nPathID))).sStationName
        Next


        For i = 1 To UBound(CSchediInfo)
            If UBound(CSchediInfo(i).nLinkTrain) > 0 Then
                ReDim Preserve CSTrainsAndDrivers.CSChedi(UBound(CSTrainsAndDrivers.CSChedi) + 1)
                CSTrainsAndDrivers.CSChedi(UBound(CSTrainsAndDrivers.CSChedi)) = New typeCSCheDi(UBound(CSTrainsAndDrivers.CSChedi), CSchediInfo(i).sCheCiHao)
                CSTrainsAndDrivers.CSChedi(UBound(CSTrainsAndDrivers.CSChedi)).StartStaName = CSTrainInf(CSchediInfo(i).nLinkTrain(1)).ComeStation
                CSTrainsAndDrivers.CSChedi(UBound(CSTrainsAndDrivers.CSChedi)).StartTime = CSTrainInf(CSchediInfo(i).nLinkTrain(1)).Starting(CSTrainInf(CSchediInfo(i).nLinkTrain(1)).nPathID(1))
                CSTrainsAndDrivers.CSChedi(UBound(CSTrainsAndDrivers.CSChedi)).EndStaName = CSTrainInf(CSchediInfo(i).nLinkTrain(UBound(CSchediInfo(i).nLinkTrain))).EndStation
                CSTrainsAndDrivers.CSChedi(UBound(CSTrainsAndDrivers.CSChedi)).EndTime = CSTrainInf(CSchediInfo(i).nLinkTrain(UBound(CSchediInfo(i).nLinkTrain))).Arrival(CSTrainInf(CSchediInfo(i).nLinkTrain(UBound(CSchediInfo(i).nLinkTrain))).nPathID(UBound(CSTrainInf(CSchediInfo(i).nLinkTrain(UBound(CSchediInfo(i).nLinkTrain))).nPathID)))

                '对每个车次循环
                For k = 1 To UBound(CSchediInfo(i).nLinkTrain) '连接车次
                    '对每个车次的每个车站循环
                    Dim FlagUpDown As Integer
                    Dim TrainId As Integer = CSchediInfo(i).nLinkTrain(k)
                    If TrainId Mod 2 = 0 Then
                        FlagUpDown = 0 '上行
                    Else
                        FlagUpDown = 1 '下行
                    End If

                    For j = 1 To UBound(CSTrainInf(TrainId).nPathID)
                        Dim nPathID_StationId As Integer = CSTrainInf(TrainId).nPathID(j)
                        Dim StaName As String = StationInf(nPathID_StationId).sStationName
                        Dim tempCrewStation = New typeCrewStation(CSTrainInf(TrainId), TrainId, j, "")
                        If tempCrewStation.IsTransitSta = True Or tempCrewStation.IsFirst = True Or tempCrewStation.IsLast = True Or tempCrewStation.IsShiftSta = True Then
                            If CSTrainInf(TrainId).BeiCheState.Contains("进备车线") Then
                                tempCrewStation.IsBeiche = 1
                            ElseIf CSTrainInf(TrainId).BeiCheState.Contains("出备车线") Then
                                tempCrewStation.IsBeiche = 2
                            End If
                            CSTrainsAndDrivers.CSChedi(UBound(CSTrainsAndDrivers.CSChedi)).AddCrewSta(tempCrewStation)
                        End If
                    Next j
                Next k
            End If
        Next i

    End Sub


    Public Sub SortCSLinkTrain(Optional ByVal Down As Boolean = False)
        Dim i, j, flag As Integer
        For i = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
            For j = i + 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
                If Down Then
                    If CSTrainsAndDrivers.CSLinkTrains(i).CulEndTime < CSTrainsAndDrivers.CSLinkTrains(j).CulEndTime Then
                        Dim tempCSlinkTrain As CSLinkTrain
                        tempCSlinkTrain = CSTrainsAndDrivers.CSLinkTrains(i)
                        CSTrainsAndDrivers.CSLinkTrains(i) = CSTrainsAndDrivers.CSLinkTrains(j)
                        CSTrainsAndDrivers.CSLinkTrains(j) = tempCSlinkTrain
                    End If
                Else
                    If CSTrainsAndDrivers.CSLinkTrains(i).CulStartTime > CSTrainsAndDrivers.CSLinkTrains(j).CulStartTime Then
                        Dim tempCSlinkTrain As CSLinkTrain
                        tempCSlinkTrain = CSTrainsAndDrivers.CSLinkTrains(i)
                        CSTrainsAndDrivers.CSLinkTrains(i) = CSTrainsAndDrivers.CSLinkTrains(j)
                        CSTrainsAndDrivers.CSLinkTrains(j) = tempCSlinkTrain
                    End If
                End If
            Next
        Next

        flag = 1
        For i = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
            If CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown = 0 Then
                CSTrainsAndDrivers.CSLinkTrains(i).UpOrDownNum = flag
                flag = flag + 1
            End If
        Next

        flag = 1
        For i = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
            If CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown = 1 Then
                CSTrainsAndDrivers.CSLinkTrains(i).UpOrDownNum = flag
                flag = flag + 1
            End If
        Next
        For i = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
            CSTrainsAndDrivers.CSLinkTrains(i).CSTrainID = i
        Next
    End Sub

  
    Public Sub SortMergedCSLinkTrain(ByVal addDirection As Boolean)
        Dim i, j As Integer
        For i = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
            For j = i + 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                If addDirection = True Then
                    If CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains(1).CulStartTime > CSTrainsAndDrivers.MergedCSLinkTrains(j).CSLinkTrains(1).CulStartTime Then
                        Dim tempCSlinkTrain As MergedCSLinkTrain
                        tempCSlinkTrain = CSTrainsAndDrivers.MergedCSLinkTrains(i)
                        CSTrainsAndDrivers.MergedCSLinkTrains(i) = CSTrainsAndDrivers.MergedCSLinkTrains(j)
                        CSTrainsAndDrivers.MergedCSLinkTrains(j) = tempCSlinkTrain
                    End If
                Else
                    If CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains)).CulEndTime < CSTrainsAndDrivers.MergedCSLinkTrains(j).CSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains(j).CSLinkTrains)).CulEndTime Then
                        Dim tempCSlinkTrain As MergedCSLinkTrain
                        tempCSlinkTrain = CSTrainsAndDrivers.MergedCSLinkTrains(i)
                        CSTrainsAndDrivers.MergedCSLinkTrains(i) = CSTrainsAndDrivers.MergedCSLinkTrains(j)
                        CSTrainsAndDrivers.MergedCSLinkTrains(j) = tempCSlinkTrain
                    End If
                End If
            Next
        Next
    End Sub
    '对CSLinkTrain排序 按时间排序
    Public Function CalJiangeAsInteger(ByVal tempCSDrier As CSDriver, ByVal tempCSLinkTrain2 As CSLinkTrain) As Integer '找到是第几个间隔后的列车
        Dim i As Integer
        CalJiangeAsInteger = 0
        Dim tempCSLinkTrain1 As CSLinkTrain
        tempCSLinkTrain1 = tempCSDrier.CSLinkTrain(UBound(tempCSDrier.CSLinkTrain))
        For i = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
            If tempCSLinkTrain1.UpOrDown = CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown And tempCSLinkTrain2.UpOrDown <> CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown Then
                If CSTrainsAndDrivers.CSLinkTrains(i).CulEndTime > tempCSLinkTrain1.CulEndTime And CSTrainsAndDrivers.CSLinkTrains(i).CulEndTime < tempCSLinkTrain2.CulStartTime _
                  And CSTrainsAndDrivers.CSLinkTrains(i).EndStaName = tempCSLinkTrain1.EndStaName And CSTrainsAndDrivers.CSLinkTrains(i).EndStaName = tempCSLinkTrain2.StartStaName Then
                    CalJiangeAsInteger = CalJiangeAsInteger + 1
                End If
            End If
        Next

    End Function
    '对CSLinkTrain排序 按时间排序
    Public Function CalJiangeAsBoolean(ByVal tempCSDrier As CSDriver, ByVal tempCSLinkTrain2 As CSLinkTrain) As Boolean  '判断是否满足休息间隔
        CalJiangeAsBoolean = False
        Dim i, CalJiangeAsInteger As Integer
        CalJiangeAsInteger = 0
        Dim tempCSLinkTrain1 As CSLinkTrain
        tempCSLinkTrain1 = tempCSDrier.CSLinkTrain(UBound(tempCSDrier.CSLinkTrain))
        For i = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
            If tempCSLinkTrain1.UpOrDown = CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown And tempCSLinkTrain2.UpOrDown <> CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown Then
                If CSTrainsAndDrivers.CSLinkTrains(i).CulEndTime > tempCSLinkTrain1.CulEndTime And CSTrainsAndDrivers.CSLinkTrains(i).CulEndTime < tempCSLinkTrain2.CulStartTime _
                  And CSTrainsAndDrivers.CSLinkTrains(i).EndStaName = tempCSLinkTrain1.EndStaName And CSTrainsAndDrivers.CSLinkTrains(i).EndStaName = tempCSLinkTrain2.StartStaName Then
                    CalJiangeAsInteger = CalJiangeAsInteger + 1
                End If
            End If
        Next
        '有间隔
        If CalJiangeAsInteger >= 0 Then
            For i = 1 To UBound(RestInterval)
                If tempCSLinkTrain1.CulEndTime >= RestInterval(i).BEGINTIME And tempCSLinkTrain1.CulEndTime < RestInterval(i).ENDTIME Then
                    If CalJiangeAsInteger >= RestInterval(i).INTERVAL Or RestInterval(i).INTERVAL = 0 Then
                        CalJiangeAsBoolean = True
                    End If
                End If
            Next
            'Else '没有间隔的话，要判断是不是该间隔中允许没有间隔
            '    For i = 1 To UBound(RestInterval)
            '        If tempCSLinkTrain1.CulEndTime >= RestInterval(i).BEGINTIME And tempCSLinkTrain1.CulEndTime < RestInterval(i).ENDTIME Then
            '            If CalJiangeAsInteger >= RestInterval(i).INTERVAL Then
            '                CalJiangeAsBoolean = True
            '            End If
            '        End If
            '    Next
        End If
    End Function

    Public Sub SaveSchedule()

        'frmCrewScheduling.DataGridView1.Item

    End Sub

    Public Function BeTime(ByVal i As Integer) As String
        Dim str As String
        If i < 0 Then
            str = CStr(i)
        Else
            Dim n1, n2, n3 As Integer
            n1 = Int(i / 3600)
            n2 = Int((i - n1 * 3600) / 60)
            n3 = i Mod 60
            'str = CStr(n1)

            If n1 < 10 Then
                str = "0" & CStr(n1)
            Else
                str = CStr(n1)
            End If
            If n2 < 10 Then
                str = str & ":0" & CStr(n2)
            Else
                str = str & ":" & CStr(n2)
            End If
            If n3 < 10 Then
                str = str & ":0" & CStr(n3)
            Else
                str = str & ":" & CStr(n3)
            End If
        End If
        Return str
    End Function

    '计算车站的乘务交路图Y坐标值
    Public Function CalYValue(ByVal StaName As String, ByVal YUp As Integer, ByVal YDown As Integer, ByVal sta() As String) As Integer
        Dim i, YDis, YValue, flag As Integer
        YDis = (YDown - YUp) / (UBound(sta) - 1)
        For i = 1 To UBound(sta)
            If sta(i) = StaName Then
                flag = i
                Exit For
            End If
        Next
        If flag > 0 Then
            YValue = YUp + YDis * (flag - 1)
        End If
        Return YValue

    End Function
    Public Function CalXValue(ByVal StationID As Integer, ByVal XLeft As Integer, ByVal XRight As Integer) As Integer
        Dim i, Min, Max As Integer
        Dim Alpha As Single
        CalXValue = 0
        Min = CSLinkStationInf(1).YPicValue
        Max = CSLinkStationInf(1).YPicValue
        For i = 1 To UBound(CSLinkStationInf)
            If Min > CSLinkStationInf(i).YPicValue Then
                Min = CSLinkStationInf(i).YPicValue
            End If
            If Max < CSLinkStationInf(i).YPicValue Then
                Max = CSLinkStationInf(i).YPicValue
            End If
        Next
        Alpha = (XRight - XLeft) / (Max - Min)
        CalXValue = XLeft + Alpha * (CSLinkStationInf(StationID).YPicValue - XLeft)
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
                myBook = myExcel.Workbooks.Add     '添加一个新的BOOK

                mySheet = myBook.Worksheets.Add     '添加一个新的SHEET
                mySheet.Name = ExcelTitle

            
                '生成与显示表格一致的excel:
                For i = 0 To rows - 1
                    For j = 0 To cols - 1
                        DataArray(i, j) = Dtg.Rows(i).Cells(j).Value
                    Next
                Next

                For j = 0 To cols - 1
                    myExcel.Cells(1, j + 1) = Dtg.Columns(j).HeaderText '.name
                Next
                mySheet.Range("A2").Resize(rows, cols).Value = DataArray

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
    Public Sub OutPutToEXCELFileFormDataGrid15(ByVal ExcelTitle As String, ByVal Dtg As DataGridView, ByVal frmForm As Form)
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
                myBook = myExcel.Workbooks.Add     '添加一个新的BOOK

                mySheet = myBook.Worksheets.Add     '添加一个新的SHEET
                mySheet.Name = ExcelTitle



                For i = 0 To rows - 1
                    For j = 0 To cols - 1
                        DataArray(i, j) = Dtg.Rows(i).Cells(j).Value
                    Next
                Next

                For j = 0 To cols - 1
                    myExcel.Cells(1, j + 1) = Dtg.Columns(j).HeaderText '.name
                Next
                mySheet.Range("A2").Resize(rows, cols).Value = DataArray

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
    Public Sub OutPutLunzhi(ByVal BeiCheList As Dictionary(Of Integer, String), ByVal ifArrival As Boolean, ByVal ifzefanTime As Boolean)
        Dim xlapp As Microsoft.Office.Interop.Excel.Application
        Dim xlbook As Microsoft.Office.Interop.Excel.Workbook
        Dim missing As Object = System.Reflection.Missing.Value

        Dim dutyType As List(Of String) = New List(Of String)
        Dim maxDutyNum As Integer = -1
        Dim maxAllDuty As Integer = 0
        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            If CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain Is Nothing = False Then
                If dutyType.Count = 0 Then
                    dutyType.Add(CSTrainsAndDrivers.CSDrivers(i).DutySort)
                Else
                    If dutyType.Contains(CSTrainsAndDrivers.CSDrivers(i).DutySort) = False Then
                        dutyType.Add(CSTrainsAndDrivers.CSDrivers(i).DutySort)
                    End If
                End If
                maxAllDuty = maxAllDuty + UBound(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain)
                If UBound(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain) > maxDutyNum Then
                    maxDutyNum = UBound(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain)
                End If
            End If
        Next
        xlapp = New Microsoft.Office.Interop.Excel.Application()
        xlbook = xlapp.Workbooks.Add
        xlbook.Worksheets.Add(missing, missing, dutyType.Count, Excel.XlSheetType.xlWorksheet) '添加新的SHEET
        Dim startrow As Integer = 1
        Dim frmPro As New FrmProgress(dutyType.Count * 3, "正在导出")
        If dutyType.Count > 0 Then
            For i As Integer = 0 To dutyType.Count - 1
                startrow = 1
                Dim mySheet1 As Microsoft.Office.Interop.Excel.Worksheet
                mySheet1 = xlbook.Sheets(i + 1)
                mySheet1.Name = dutyType(i).ToString()
                For j As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                    Dim dri As CSDriver = CSTrainsAndDrivers.CSDrivers(j)
                    If dri IsNot Nothing AndAlso dri.DutySort = dutyType(i) Then
                        If BeiCheList.Keys.Contains(j) Then
                            startrow = DrawDataToExcel(dri, xlbook.Sheets(i + 1), startrow, BeiCheList(j), ifArrival, ifzefanTime)
                        Else
                            startrow = DrawDataToExcel(dri, xlbook.Sheets(i + 1), startrow, "", ifArrival, ifzefanTime)
                        End If
                    End If
                Next

                frmPro.Performstep()
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
                    If dri IsNot Nothing AndAlso dri.DutySort = dutyType(i) Then
                        startrow = DrawDataToExcel(dri, xlbook.Sheets(i + 1), startrow)
                    End If
                Next
                frmPro.Performstep()
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
                    If dri IsNot Nothing AndAlso dri.DutySort = dutyType(i) Then
                        startrow = DrawDataToExcel(dri, xlbook.Sheets(i + 1), startrow)
                    End If
                Next
                frmPro.Performstep()
            Next
        End If
        frmPro.EndProgress()
        xlapp.Visible = True
    End Sub
    Public Function DrawDataToExcel(ByVal csd As CSDriver, ByVal sheet As Excel.Worksheet, ByVal Startrow As Integer, Optional ByVal BeiChe As String = "", Optional ifArrial As Boolean = True, Optional ifZhefanTime As Boolean = False) As Integer
        sheet.Cells(Startrow, 1) = "任务编号"
        sheet.Cells(Startrow, 2) = "任务开始时间"
        sheet.Cells(Startrow, 3) = "接车时间"
        sheet.Cells(Startrow, 4) = "接车地点"
        sheet.Cells(Startrow, 5) = "接车车次"
        sheet.Cells(Startrow, 6) = "车底号"
        sheet.Cells(Startrow, 7) = "下车地点"
        sheet.Cells(Startrow, 8) = "下车车次"
        sheet.Cells(Startrow, 9) = "下车时间"
        sheet.Cells(Startrow, 10) = "任务结束时间"

        If csd.ModifiedCSLinkTrain(1).FirstStation IsNot Nothing AndAlso csd.ModifiedCSLinkTrain(1).FirstStation.IsYard = True Then
            sheet.Cells(Startrow + 1, 2) = BeTime(csd.ModifiedCSLinkTrain(1).StartTime - csd.PreTrainTime)
        Else
            sheet.Cells(Startrow + 1, 2) = BeTime(csd.ModifiedCSLinkTrain(1).StartTime - csd.PreDutyTime)
        End If
        Dim CurPos As Integer = Startrow
        Dim firstBeiChe As Boolean = False
        If BeiChe <> "" AndAlso BeiChe.Split("-")(0) = "1" Then
            CurPos += 1
            sheet.Cells(CurPos, 2) = BeTime(Convert.ToInt32(BeiChe.Split("-")(1)) - csd.PreDutyTime)
            sheet.Cells(CurPos, 3) = CDate(Coordination2.Global.BeTime(Convert.ToInt32(BeiChe.Split("-")(1)))).ToString("HH:mm")
            sheet.Range(sheet.Cells(CurPos, 4), sheet.Cells(CurPos, 8)).Merge()
            sheet.Cells(CurPos, 4) = "备车" & csd.ModifiedCSLinkTrain(1).StartStaName
            sheet.Cells(CurPos, 9) = CDate(Coordination2.Global.BeTime(csd.ModifiedCSLinkTrain(1).StartTime)).ToString("HH:mm")
            firstBeiChe = True
        End If


        If csd.IsPrepareDri = False Then
            For i As Integer = 1 To UBound(csd.ModifiedCSLinkTrain)
                CurPos += 1
                If csd.FlagDinner = True Then
                    If (i = 1 AndAlso csd.DinnerStartTime < csd.ModifiedCSLinkTrain(i).StartTime) OrElse _
                        (i > 1 AndAlso csd.DinnerStartTime < csd.ModifiedCSLinkTrain(i).StartTime AndAlso csd.DinnerStartTime > csd.ModifiedCSLinkTrain(i - 1).StartTime) Then
                        sheet.Cells(CurPos, 3) = CDate(Coordination2.Global.BeTime(csd.DinnerStartTime)).ToString("HH:mm")
                        sheet.Range(sheet.Cells(CurPos, 4), sheet.Cells(CurPos, 8)).Merge()
                        sheet.Cells(CurPos, 4) = "用餐时间"
                        sheet.Cells(CurPos, 9) = CDate(Coordination2.Global.BeTime(csd.ModifiedCSLinkTrain(i).StartTime)).ToString("HH:mm")
                        CurPos += 1
                    End If
                End If
                sheet.Cells(CurPos, 3) = Coordination2.Global.BeTime(csd.ModifiedCSLinkTrain(i).StartTime)
                sheet.Cells(CurPos, 9) = CDate(Coordination2.Global.BeTime(csd.ModifiedCSLinkTrain(i).EndTime)).ToString("HH:mm")
                If csd.ModifiedCSLinkTrain(i).IsDeadHeading = False Then
                    sheet.Cells(CurPos, 4) = csd.ModifiedCSLinkTrain(i).StartStaName
                    sheet.Cells(CurPos, 5) = csd.ModifiedCSLinkTrain(i).OutputCheCi
                    sheet.Cells(CurPos, 6) = csd.ModifiedCSLinkTrain(i).sCheDiHao
                    sheet.Cells(CurPos, 7) = csd.ModifiedCSLinkTrain(i).EndStaName
                    sheet.Cells(CurPos, 8) = csd.ModifiedCSLinkTrain(i).OffCheCi

                    If ifArrial = False And firstBeiChe = False Then
                        Dim arriveCheci As String = ""
                        Dim maxTime As Integer = -1
                        For z As Integer = 0 To CSTrainsAndDrivers.CSChedi(csd.ModifiedCSLinkTrain(i).nCheDiID).CSLinkTrains.Count - 1
                            If AddLitterTime(CSTrainsAndDrivers.CSChedi(csd.ModifiedCSLinkTrain(i).nCheDiID).CSLinkTrains(z).EndTime) < AddLitterTime(csd.ModifiedCSLinkTrain(i).CulStartTime) Then
                                If AddLitterTime(maxTime) < AddLitterTime(CSTrainsAndDrivers.CSChedi(csd.ModifiedCSLinkTrain(i).nCheDiID).CSLinkTrains(z).EndTime) Then
                                    maxTime = CSTrainsAndDrivers.CSChedi(csd.ModifiedCSLinkTrain(i).nCheDiID).CSLinkTrains(z).EndTime
                                    arriveCheci = CSTrainsAndDrivers.CSChedi(csd.ModifiedCSLinkTrain(i).nCheDiID).CSLinkTrains(z).OutputCheCi
                                End If
                            End If
                        Next
                        If maxTime <> -1 Then
                            sheet.Cells(CurPos, 3) = BeTime(maxTime)
                            sheet.Cells(CurPos, 5) = arriveCheci
                            If csd.ModifiedCSLinkTrain(1).FirstStation.IsYard = False Then
                                sheet.Cells(Startrow + 1, 2) = BeTime(maxTime - csd.PreDutyTime)
                            End If
                        End If
                    End If
                    If ifZhefanTime = True And firstBeiChe = False Then
                        If i = UBound(csd.ModifiedCSLinkTrain) And csd.ModifiedCSLinkTrain(i).SecondStation.IsYard = True Then
                            Continue For
                        End If
                        If i + 1 <= UBound(csd.ModifiedCSLinkTrain) Then
                            If csd.ModifiedCSLinkTrain(i).UpOrDown = csd.ModifiedCSLinkTrain(i + 1).UpOrDown Then
                                Continue For
                            End If
                        End If
                        Dim offCheci1 As String = ""
                        Dim finaltime As Integer = 10000000
                        Dim maxTime As Integer = 10000000
                        For z As Integer = 0 To CSTrainsAndDrivers.CSChedi(csd.ModifiedCSLinkTrain(i).nCheDiID).CSLinkTrains.Count - 1
                            If AddLitterTime(CSTrainsAndDrivers.CSChedi(csd.ModifiedCSLinkTrain(i).nCheDiID).CSLinkTrains(z).StartTime) > AddLitterTime(csd.ModifiedCSLinkTrain(i).CulEndTime) Then
                                If AddLitterTime(maxTime) > AddLitterTime(CSTrainsAndDrivers.CSChedi(csd.ModifiedCSLinkTrain(i).nCheDiID).CSLinkTrains(z).FirstStation.ArriveTime) - AddLitterTime(csd.ModifiedCSLinkTrain(i).CulEndTime) Then
                                    maxTime = AddLitterTime(CSTrainsAndDrivers.CSChedi(csd.ModifiedCSLinkTrain(i).nCheDiID).CSLinkTrains(z).FirstStation.ArriveTime) - AddLitterTime(csd.ModifiedCSLinkTrain(i).CulEndTime)
                                    finaltime = CSTrainsAndDrivers.CSChedi(csd.ModifiedCSLinkTrain(i).nCheDiID).CSLinkTrains(z).FirstStation.ArriveTime
                                    offCheci1 = CSTrainsAndDrivers.CSChedi(csd.ModifiedCSLinkTrain(i).nCheDiID).CSLinkTrains(z).OutputCheCi
                                End If
                            End If
                        Next
                        If maxTime <> 10000000 Then
                            sheet.Cells(CurPos, 9) = CDate(BeTime(maxTime)).ToString("HH:mm")
                            sheet.Cells(CurPos, 8) = offCheci1
                        End If
                    End If
                Else
                    If i > 1 AndAlso i < UBound(csd.ModifiedCSLinkTrain) Then
                        sheet.Range(sheet.Cells(CurPos, 4), sheet.Cells(CurPos, 8)).Merge()
                        sheet.Cells(CurPos, 4) = "前往" & csd.ModifiedCSLinkTrain(i).EndStaName & "接车"
                    Else
                        sheet.Range(sheet.Cells(CurPos, 4), sheet.Cells(CurPos, 8)).Merge()
                        sheet.Cells(CurPos, 4) = "坐" & csd.ModifiedCSLinkTrain(i).sCheDiHao & " " & csd.ModifiedCSLinkTrain(i).OutputCheCi & "到" & csd.ModifiedCSLinkTrain(i).EndStaName
                    End If
                End If
            Next
        Else
            CurPos += 1
            sheet.Range(sheet.Cells(CurPos, 3), sheet.Cells(CurPos, 9)).Merge()
            sheet.Cells(CurPos, 3) = csd.CSLinkTrain(1).StartStaName & csd.CSdriverNo & "备车"
        End If


        If BeiChe <> "" AndAlso BeiChe.Split("-")(0) = "2" Then
            CurPos += 1
            sheet.Cells(CurPos, 3) = CDate(Coordination2.Global.BeTime(csd.ModifiedCSLinkTrain(UBound(csd.ModifiedCSLinkTrain)).EndTime)).ToString("HH:mm")
            sheet.Range(sheet.Cells(CurPos, 4), sheet.Cells(CurPos, 8)).Merge()
            sheet.Cells(CurPos, 4) = "备车" & csd.ModifiedCSLinkTrain(UBound(csd.ModifiedCSLinkTrain)).EndStaName
            sheet.Cells(CurPos, 9) = CDate(Coordination2.Global.BeTime(Convert.ToInt32(BeiChe.Split("-")(1)))).ToString("HH:mm")
            sheet.Cells(CurPos, 10) = CDate(Coordination2.Global.BeTime(Convert.ToInt32(BeiChe.Split("-")(1)))).ToString("HH:mm")
        Else
            sheet.Cells(CurPos, 10) = CDate(Coordination2.Global.BeTime(csd.EndEorkTime)).ToString("HH:mm")
        End If

        If csd.DutySort = "夜班" Then
            Dim str As String = "select t.*,m.cstimetablename,n.outputcsdriverno from cs_amdrivercorrespond t,cs_cstimetableinf m,cs_workload n " & _
           "where t.mdrivertimetableid=m.cstimetableid and t.adrivertimetableid='" & strQCurCSPlanID & "' and" & _
           " t.adriverno='" & csd.CSdriverNo & "' and t.mdrivertimetableid=n.cstimetableid and t.mdriverno=n.driverno"
            Dim temtab As Data.DataTable = ReadData(str)
            If temtab IsNot Nothing AndAlso temtab.Rows.Count > 0 Then
                For Each row As DataRow In temtab.Rows
                    CurPos += 1
                    sheet.Range(sheet.Cells(CurPos, 3), sheet.Cells(CurPos, 9)).Merge()
                    sheet.Cells(CurPos, 3) = "早班衔接转 " & row.Item("cstimetablename").ToString & " 见 " & row.Item("outputcsdriverno").ToString
                Next
            End If
            str = "select t.*,m.cstimetablename,n.outputcsdriverno from cs_amdrivercorrespond t,cs_cstimetableinf m,cs_result_prepareddutyinf n " & _
                "where t.mdrivertimetableid=m.cstimetableid and t.adrivertimetableid='" & strQCurCSPlanID & "' and" & _
                " t.adriverno='" & csd.CSdriverNo & "' and t.mdrivertimetableid=n.cstimetableid and t.mdriverno=n.name and n.dutysort='早班'"
            temtab = ReadData(str)
            If temtab IsNot Nothing AndAlso temtab.Rows.Count > 0 Then
                For Each row As DataRow In temtab.Rows
                    CurPos += 1
                    sheet.Range(sheet.Cells(CurPos, 3), sheet.Cells(CurPos, 9)).Merge()
                    sheet.Cells(CurPos, 3) = "早班衔接转 " & row.Item("cstimetablename").ToString & " 见 " & row.Item("outputcsdriverno").ToString
                Next
            End If
            str = "select t.*,m.cstimetablename,n.outputcsdriverno from cs_amdrivercorrespond t,cs_cstimetableinf m,cs_result_preparedtraininf n " & _
                "where t.mdrivertimetableid=m.cstimetableid and t.adrivertimetableid='" & strQCurCSPlanID & "' and" & _
                " t.adriverno='" & csd.CSdriverNo & "' and t.mdrivertimetableid=n.cstimetableid and t.mdriverno=n.name and n.dutysort='早班'"
            temtab = ReadData(str)
            If temtab IsNot Nothing AndAlso temtab.Rows.Count > 0 Then
                For Each row As DataRow In temtab.Rows
                    CurPos += 1
                    sheet.Range(sheet.Cells(CurPos, 3), sheet.Cells(CurPos, 9)).Merge()
                    sheet.Cells(CurPos, 3) = "早班衔接转 " & row.Item("cstimetablename").ToString & " 见 " & row.Item("outputcsdriverno").ToString
                Next
            End If
            temtab.Dispose()
        End If

        If CurPos - Startrow - 1 >= 3 Then
            Dim interval As Integer = (CurPos - Startrow - 1) / 3
            sheet.Cells(Startrow + interval + 1, 1) = (MyCeiLing(0.25, Convert.ToDecimal(csd.WorkTime) / 3600)) & "h"
            sheet.Cells(Startrow + interval * 2 + 1, 1) = (csd.DriveDistance).ToString("0.0") & "km"
            sheet.Range(sheet.Cells(Startrow + 1, 1), sheet.Cells(Startrow + 1, 1)).NumberFormat = "@"
            sheet.Cells(Startrow + 1, 1) = csd.OutPutCSdriverNo

            sheet.Range(sheet.Cells(Startrow + 1, 1), sheet.Cells(Startrow + interval, 1)).Merge()
            sheet.Range(sheet.Cells(Startrow + interval + 1, 1), sheet.Cells(Startrow + interval * 2, 1)).Merge()
            sheet.Range(sheet.Cells(Startrow + interval * 2 + 1, 1), sheet.Cells(CurPos, 1)).Merge()
        ElseIf CurPos - Startrow - 1 = 2 Then
            sheet.Cells(Startrow + 2, 1) = (MyCeiLing(0.25, Convert.ToDecimal(csd.WorkTime) / 3600)) & "h  " & (csd.DriveDistance).ToString("0.0") & "km"
            sheet.Range(sheet.Cells(Startrow + 1, 1), sheet.Cells(Startrow + 1, 1)).NumberFormat = "@"
            sheet.Cells(Startrow + 1, 1) = csd.OutPutCSdriverNo
        Else
            sheet.Range(sheet.Cells(Startrow + 1, 1), sheet.Cells(Startrow + 1, 1)).NumberFormat = "@"
            sheet.Cells(Startrow + 1, 1) = csd.OutPutCSdriverNo & " (" & (MyCeiLing(0.25, Convert.ToDecimal(csd.WorkTime) / 3600)) & "h  " & (csd.DriveDistance).ToString("0.0") & "km)"
        End If


        sheet.Range(sheet.Cells(Startrow, 1), sheet.Cells(CurPos, 10)).Borders.LineStyle = 1
        sheet.Range(sheet.Cells(Startrow, 1), sheet.Cells(CurPos, 10)).Font.Name = "Arial Unicode MS"
        sheet.Range(sheet.Cells(Startrow, 1), sheet.Cells(CurPos, 10)).Font.Size = 11
        sheet.Range(sheet.Cells(Startrow, 1), sheet.Cells(CurPos, 10)).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        sheet.Range(sheet.Cells(Startrow, 1), sheet.Cells(CurPos, 10)).VerticalAlignment = Excel.XlHAlign.xlHAlignCenter
        sheet.Range(sheet.Cells(Startrow, 1), sheet.Cells(CurPos, 10)).ColumnWidth = 10   '固定列宽
        sheet.Range(sheet.Cells(Startrow, 1), sheet.Cells(CurPos, 10)).WrapText = True    '文本自动换行
        DrawDataToExcel = CurPos + 2
        sheet.Columns.AutoFit()
    End Function
    Public Sub OutPutDriverPosition(ByVal ExcelTitle As String, ByVal ifArriveTime As Boolean, ByVal ifZhefanTime As Boolean, ByVal BeicheList As Dictionary(Of Integer, String)) 'april如果接车为开车时间，则为true
        Dim myExcel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application
        Dim myBook As Microsoft.Office.Interop.Excel.Workbook
        Dim i, j, s, n, n1, n2, n3 As Integer
        Dim missing As Object = System.Reflection.Missing.Value
        myBook = myExcel.Workbooks.Add     '添加一个新的BOOK

        Dim dutyType As List(Of String) = New List(Of String)

        Dim maxAllDuty As Integer = 0
        Dim dutyTypeNo As New Dictionary(Of String, Integer)
        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            If CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain Is Nothing = False Then
                If dutyType.Contains(CSTrainsAndDrivers.CSDrivers(i).DutySort) = False Then
                    dutyType.Add(CSTrainsAndDrivers.CSDrivers(i).DutySort)
                    dutyTypeNo.Add(CSTrainsAndDrivers.CSDrivers(i).DutySort, UBound(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain))
                Else
                    If dutyTypeNo(CSTrainsAndDrivers.CSDrivers(i).DutySort) < UBound(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain) Then
                        dutyTypeNo(CSTrainsAndDrivers.CSDrivers(i).DutySort) = UBound(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain)
                    End If
                End If
                maxAllDuty = maxAllDuty + UBound(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain)

            End If
        Next
        Dim frmpro As New FrmProgress(maxAllDuty, "正在导出请稍后")
        frmpro.Show()

        myBook.Worksheets.Add(missing, missing, dutyType.Count + 1, Excel.XlSheetType.xlWorksheet) '添加新的SHEET
        Dim mySheet4 As Microsoft.Office.Interop.Excel.Worksheet
        mySheet4 = myBook.Sheets(dutyType.Count + 1)
        mySheet4.Name = "技术说明"

        Dim tmpCol, tmpRow As Integer '当前列和行
        If dutyType.Count > 0 Then
            For i = 0 To dutyType.Count - 1
                Dim maxDutyNum As Integer = dutyTypeNo(dutyType(i))
                Dim mySheet1 As Microsoft.Office.Interop.Excel.Worksheet
                mySheet1 = myBook.Sheets(i + 1)
                mySheet1.Name = dutyType(i).ToString()
                tmpCol = 1
                tmpRow = 2
                Dim RecordRow0 As Integer = tmpRow
                myBook.Sheets(i + 1).Cells(tmpRow, tmpCol) = strCurlineID + strDiagram + " 司机位置图    " + dutyType(i) + " " + DiagramCurID + "版"
                myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, tmpCol), myBook.Sheets(i + 1).Cells(tmpRow, tmpCol + 2 + maxDutyNum * 6 + maxDutyNum - 1)).Font.Size = "18"
                myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, tmpCol), myBook.Sheets(i + 1).Cells(tmpRow, tmpCol)).Font.Name = "黑体"
                myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, tmpCol), myBook.Sheets(i + 1).Cells(tmpRow, tmpCol + 2 + maxDutyNum * 6 + maxDutyNum - 1)).Merge()
                With myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, tmpCol), myBook.Sheets(i + 1).Cells(tmpRow, tmpCol + 2 + maxDutyNum * 6 + maxDutyNum - 1))
                    .HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                    .VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter
                    '.AutoFit()
                End With

                tmpRow = tmpRow + 1
                myBook.Sheets(i + 1).Cells(tmpRow, 1) = "位置"
                myBook.Sheets(i + 1).Cells(tmpRow, 2) = "出勤时间"
                For j = 0 To maxDutyNum - 1
                    myBook.Sheets(i + 1).Cells(tmpRow, 3 + 7 * j) = "接车时间"
                    myBook.Sheets(i + 1).Cells(tmpRow, 4 + 7 * j) = "接车站名"
                    myBook.Sheets(i + 1).Cells(tmpRow, 5 + 7 * j) = "接车车次"
                    myBook.Sheets(i + 1).Cells(tmpRow, 6 + 7 * j) = "开车车次"
                    myBook.Sheets(i + 1).Cells(tmpRow, 7 + 7 * j) = "到达时间"
                    myBook.Sheets(i + 1).Cells(tmpRow, 8 + 7 * j) = "到达站名"
                    If j <> maxDutyNum - 1 And j Mod 2 = 0 Then
                        myBook.Sheets(i + 1).Cells(tmpRow, 9 + 7 * j) = "->"
                    Else
                        myBook.Sheets(i + 1).Cells(tmpRow, 9 + 7 * j) = "∞"
                    End If
                Next
                myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, tmpCol), myBook.Sheets(i + 1).Cells(tmpRow, tmpCol + 2 + maxDutyNum * 6 + maxDutyNum - 1)).Font.Size = "11"
                myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, tmpCol), myBook.Sheets(i + 1).Cells(tmpRow, tmpCol + 2 + maxDutyNum * 6 + maxDutyNum - 1)).Font.Bold = FontStyle.Bold
                myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, tmpCol), myBook.Sheets(i + 1).Cells(tmpRow, tmpCol)).Font.Name = "宋体"
                With myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, tmpCol), myBook.Sheets(i + 1).Cells(tmpRow, tmpCol + 2 + maxDutyNum * 6 + maxDutyNum - 1))
                    .HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                    .VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter
                    '.AutoFit()
                End With
                With myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, tmpCol), myBook.Sheets(i + 1).Cells(tmpRow, tmpCol + 2 + maxDutyNum * 6 + maxDutyNum - 1))
                    .Interior.ColorIndex = 15
                End With
                tmpRow = tmpRow + 1
                For s = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                    Dim dinnerTime = -1
                    If CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain Is Nothing = False Then
                        If dutyType(i) = CSTrainsAndDrivers.CSDrivers(s).DutySort Then
                            myBook.Sheets(i + 1).Cells(tmpRow, 1) = CSTrainsAndDrivers.CSDrivers(s).CSdriverNo
                            If CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(1).FirstStation.IsYard = True Then
                                myBook.Sheets(i + 1).Cells(tmpRow, 2) = BeTime(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(1).StartTime - CSTrainsAndDrivers.CSDrivers(s).PreTrainTime)
                            Else
                                myBook.Sheets(i + 1).Cells(tmpRow, 2) = BeTime(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(1).StartTime - CSTrainsAndDrivers.CSDrivers(s).PreDutyTime)
                            End If
                            '备车
                            Dim addLine As Integer = 0
                            If BeicheList.Keys.Contains(s) Then
                                If BeicheList(s).Split("-")(0).ToString = "1" Then
                                    If CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(1).FirstStation.IsYard = True Then
                                        myBook.Sheets(i + 1).Cells(tmpRow, 2) = BeTime(Integer.Parse(BeicheList(s).Split("-")(1)) - CSTrainsAndDrivers.CSDrivers(s).PreTrainTime)
                                    Else
                                        myBook.Sheets(i + 1).Cells(tmpRow, 2) = BeTime(Integer.Parse(BeicheList(s).Split("-")(1)) - CSTrainsAndDrivers.CSDrivers(s).PreDutyTime)
                                    End If
                                    myBook.Sheets(i + 1).Cells(tmpRow, 3) = BeTime(Integer.Parse(BeicheList(s).Split("-")(1)))
                                    myBook.Sheets(i + 1).Cells(tmpRow, 4) = CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(1).StartStaName
                                    myBook.Sheets(i + 1).Cells(tmpRow, 5) = "备车" + CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(1).OutputCheCi
                                    myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, 5), myBook.Sheets(i + 1).Cells(tmpRow, 6)).Merge()
                                    myBook.Sheets(i + 1).Cells(tmpRow, 7) = BeTime(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(1).StartTime)
                                    myBook.Sheets(i + 1).Cells(tmpRow, 8) = CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(1).StartStaName
                                    myBook.Sheets(i + 1).Cells(tmpRow, 9) = "->"
                                    addLine = 1
                                End If
                            End If

                            For j = 1 To UBound(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain)
                                myBook.Sheets(i + 1).Cells(tmpRow, 3 + 7 * (j - 1 + addLine)) = BeTime(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).StartTime)
                                myBook.Sheets(i + 1).Cells(tmpRow, 4 + 7 * (j - 1 + addLine)) = CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).StartStaName
                                If CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).IsDeadHeading = True Then
                                    myBook.Sheets(i + 1).Cells(tmpRow, 5 + 7 * (j - 1 + addLine)) = "坐" + CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).OutputCheCi
                                    myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, 5 + 7 * (j - 1 + addLine)), myBook.Sheets(i + 1).Cells(tmpRow, 6 + 7 * (j - 1 + addLine))).Merge()
                                    myBook.Sheets(i + 1).Cells(tmpRow, 7 + 7 * (j - 1 + addLine)) = BeTime(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).EndTime)
                                    myBook.Sheets(i + 1).Cells(tmpRow, 8 + 7 * (j - 1 + addLine)) = CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).EndStaName
                                    If j <> UBound(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain) And (j - 1 + addLine) Mod 2 = 0 Then
                                        myBook.Sheets(i + 1).Cells(tmpRow, 9 + 7 * (j - 1 + addLine)) = "->"
                                    Else
                                        myBook.Sheets(i + 1).Cells(tmpRow, 9 + 7 * (j - 1 + addLine)) = "∞"
                                    End If
                                    Continue For
                                End If
                                myBook.Sheets(i + 1).Cells(tmpRow, 5 + 7 * (j - 1 + addLine)) = CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).OutputCheCi
                                If ifArriveTime = False Then
                                    Dim arriveCheci As String = ""
                                    Dim maxTime As Integer = -1
                                    For z As Integer = 0 To CSTrainsAndDrivers.CSChedi(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains.Count - 1
                                        If AddLitterTime(CSTrainsAndDrivers.CSChedi(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains(z).EndTime) < AddLitterTime(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).CulStartTime) Then
                                            If AddLitterTime(maxTime) < AddLitterTime(CSTrainsAndDrivers.CSChedi(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains(z).EndTime) Then
                                                maxTime = CSTrainsAndDrivers.CSChedi(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains(z).EndTime
                                                arriveCheci = CSTrainsAndDrivers.CSChedi(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains(z).OutputCheCi
                                            End If
                                        End If
                                    Next
                                    If maxTime = -1 Then
                                        myBook.Sheets(i + 1).Cells(tmpRow, 3 + 7 * (j - 1 + addLine)) = BeTime(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).StartTime)
                                        myBook.Sheets(i + 1).Cells(tmpRow, 5 + 7 * (j - 1 + addLine)) = CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).OutputCheCi
                                    Else
                                        myBook.Sheets(i + 1).Cells(tmpRow, 3 + 7 * (j - 1 + addLine)) = BeTime(maxTime)
                                        myBook.Sheets(i + 1).Cells(tmpRow, 5 + 7 * (j - 1 + addLine)) = arriveCheci
                                        If j = 1 AndAlso CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).FirstStation.IsYard = False Then
                                            myBook.Sheets(i + 1).Cells(tmpRow, 2) = BeTime(maxTime - CSTrainsAndDrivers.CSDrivers(s).PreDutyTime)
                                        End If
                                    End If
                                End If
                                myBook.Sheets(i + 1).Cells(tmpRow, 6 + 7 * (j - 1 + addLine)) = CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).OutputCheCi
                                myBook.Sheets(i + 1).Cells(tmpRow, 7 + 7 * (j - 1 + addLine)) = BeTime(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).EndTime)
                                myBook.Sheets(i + 1).Cells(tmpRow, 8 + 7 * (j - 1 + addLine)) = CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).EndStaName
                                If ifZhefanTime = True AndAlso ((j = UBound(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain) AndAlso CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).SecondStation.IsYard = False) OrElse (j + 1 <= UBound(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain) AndAlso CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).UpOrDown <> CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j + 1).UpOrDown)) Then
                                    Dim offCheci As String = ""
                                    Dim maxTime As Integer = 10000000
                                    Dim minDis As Integer = 10000000
                                    For z As Integer = 0 To CSTrainsAndDrivers.CSChedi(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains.Count - 1
                                        If AddLitterTime(CSTrainsAndDrivers.CSChedi(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains(z).StartTime) > AddLitterTime(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).CulEndTime) Then
                                            If minDis > AddLitterTime(CSTrainsAndDrivers.CSChedi(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains(z).FirstStation.ArriveTime) - AddLitterTime(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).CulEndTime) Then
                                                minDis = AddLitterTime(CSTrainsAndDrivers.CSChedi(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains(z).FirstStation.ArriveTime) - AddLitterTime(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).CulEndTime)
                                                maxTime = AddLitterTime(CSTrainsAndDrivers.CSChedi(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains(z).FirstStation.ArriveTime)
                                                offCheci = CSTrainsAndDrivers.CSChedi(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains(z).OutputCheCi
                                            End If
                                        End If
                                    Next
                                    If maxTime = 10000000 Then
                                        myBook.Sheets(i + 1).Cells(tmpRow, 7 + 7 * (j - 1 + addLine)) = BeTime(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).EndTime)
                                    Else
                                        myBook.Sheets(i + 1).Cells(tmpRow, 7 + 7 * (j - 1 + addLine)) = BeTime(maxTime)
                                    End If
                                End If
                                If j <> UBound(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain) And (j - 1 + addLine) Mod 2 = 0 Then
                                    myBook.Sheets(i + 1).Cells(tmpRow, 9 + 7 * (j - 1 + addLine)) = "->"
                                Else
                                    myBook.Sheets(i + 1).Cells(tmpRow, 9 + 7 * (j - 1 + addLine)) = "∞"
                                End If

                                If j <> UBound(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain) Then
                                    For n2 = 1 To UBound(sysDinnerStation)
                                        If CSTrainsAndDrivers.CSDrivers(s).DutySort = sysDinnerStation(n2).dutySort Then
                                            If CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).CulStartTime <= AddLitterTime(sysDinnerStation(n2).dinnerStartTime) And CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).CulEndTime >= AddLitterTime(sysDinnerStation(n2).dinnerEndTime) And CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j + 1).CulStartTime - CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).CulEndTime >= sysDinnerStation(n2).DinnerTime Then
                                                If dinnerTime <> n2 Then
                                                    For n3 = 1 To sysDinnerStation.Count - 1
                                                        If CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).RoutingName = sysDinnerStation(n3).Routing And CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).EndStaName = sysDinnerStation(n3).DinnerStationName And (CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(j).UpOrDown = sysDinnerStation(n3).Direction Or sysDinnerStation(n3).Direction = 2) Then
                                                            myBook.Sheets(i + 1).Cells(tmpRow, 9 + 7 * (j - 1 + addLine)) = "用餐"
                                                            dinnerTime = n2
                                                        End If
                                                    Next
                                                End If
                                            End If
                                        End If
                                    Next
                                End If
                                frmpro.Performstep()
                            Next

                            '备车
                            If BeicheList.Keys.Contains(s) Then
                                If BeicheList(s).Split("-")(0).ToString = "2" Then
                                    myBook.Sheets(i + 1).Cells(tmpRow, 3 + 7 * (j - 1 + addLine)) = BeTime(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain)).EndTime)
                                    myBook.Sheets(i + 1).Cells(tmpRow, 4 + 7 * (j - 1 + addLine)) = CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain)).EndStaName
                                    myBook.Sheets(i + 1).Cells(tmpRow, 5 + 7 * (j - 1 + addLine)) = "备车" + CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain)).OutputCheCi
                                    myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, 5 + 7 * (j - 1 + addLine)), myBook.Sheets(i + 1).Cells(tmpRow, 6 + 7 * (j - 1 + addLine))).Merge()
                                    myBook.Sheets(i + 1).Cells(tmpRow, 7 + 7 * (j - 1 + addLine)) = BeTime(Integer.Parse(BeicheList(s).Split("-")(1)))
                                    myBook.Sheets(i + 1).Cells(tmpRow, 8 + 7 * (j - 1 + addLine)) = CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(s).ModifiedCSLinkTrain)).EndStaName
                                    myBook.Sheets(i + 1).Cells(tmpRow, 9 + 7 * (j - 1 + addLine)) = "->"
                                    addLine = 1
                                End If
                            End If


                            myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, tmpCol), myBook.Sheets(i + 1).Cells(tmpRow, tmpCol + 2 + maxDutyNum * 6 + maxDutyNum - 1)).Font.Size = "9"
                            myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, tmpCol), myBook.Sheets(i + 1).Cells(tmpRow, tmpCol)).Font.Name = "宋体"
                            With myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, tmpCol), myBook.Sheets(i + 1).Cells(tmpRow, tmpCol + 2 + maxDutyNum * 6 + maxDutyNum - 1))
                                .HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                                .VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter
                                '.AutoFit()
                            End With
                            tmpRow = tmpRow + 1
                        End If
                    End If
                Next

                For s = 0 To CSTrainsAndDrivers.PreParedDutyDrivers.Count - 1
                    If CSTrainsAndDrivers.PreParedDutyDrivers(s).DutySort = dutyType(i) Then
                        myBook.Sheets(i + 1).Cells(tmpRow, 1) = CSTrainsAndDrivers.PreParedDutyDrivers(s).CSdriverNo
                        myBook.Sheets(i + 1).Cells(tmpRow, 2) = BeTime(CSTrainsAndDrivers.PreParedDutyDrivers(s).CSLinkTrain(1).StartTime - CSTrainsAndDrivers.PreParedDutyDrivers(s).PreDutyTime)
                        myBook.Sheets(i + 1).Cells(tmpRow, 3) = BeTime(CSTrainsAndDrivers.PreParedDutyDrivers(s).CSLinkTrain(1).StartTime)
                        myBook.Sheets(i + 1).Cells(tmpRow, 4) = CSTrainsAndDrivers.PreParedDutyDrivers(s).CSLinkTrain(1).StartStaName
                        myBook.Sheets(i + 1).Cells(tmpRow, 5) = "备车"
                        myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, 5), myBook.Sheets(i + 1).Cells(tmpRow, 6)).Merge()
                        myBook.Sheets(i + 1).Cells(tmpRow, 7) = BeTime(CSTrainsAndDrivers.PreParedDutyDrivers(s).CSLinkTrain(1).EndTime)
                        myBook.Sheets(i + 1).Cells(tmpRow, 8) = CSTrainsAndDrivers.PreParedDutyDrivers(s).CSLinkTrain(1).EndStaName
                        myBook.Sheets(i + 1).Cells(tmpRow, 9) = "->"
                        tmpRow = tmpRow + 1
                    End If
                Next
                For s = 0 To CSTrainsAndDrivers.PreParedTrainDrivers.Count - 1
                    If CSTrainsAndDrivers.PreParedTrainDrivers(s).DutySort = dutyType(i) Then
                        myBook.Sheets(i + 1).Cells(tmpRow, 1) = CSTrainsAndDrivers.PreParedTrainDrivers(s).CSdriverNo
                        myBook.Sheets(i + 1).Cells(tmpRow, 2) = BeTime(CSTrainsAndDrivers.PreParedTrainDrivers(s).CSLinkTrain(1).StartTime - CSTrainsAndDrivers.PreParedTrainDrivers(s).PreDutyTime)
                        myBook.Sheets(i + 1).Cells(tmpRow, 3) = BeTime(CSTrainsAndDrivers.PreParedTrainDrivers(s).CSLinkTrain(1).StartTime)
                        myBook.Sheets(i + 1).Cells(tmpRow, 4) = CSTrainsAndDrivers.PreParedTrainDrivers(s).CSLinkTrain(1).StartStaName
                        myBook.Sheets(i + 1).Cells(tmpRow, 5) = "备车"
                        myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, 5), myBook.Sheets(i + 1).Cells(tmpRow, 6)).Merge()
                        myBook.Sheets(i + 1).Cells(tmpRow, 7) = BeTime(CSTrainsAndDrivers.PreParedTrainDrivers(s).CSLinkTrain(1).EndTime)
                        myBook.Sheets(i + 1).Cells(tmpRow, 8) = CSTrainsAndDrivers.PreParedTrainDrivers(s).CSLinkTrain(1).EndStaName
                        myBook.Sheets(i + 1).Cells(tmpRow, 9) = "->"
                        tmpRow = tmpRow + 1
                    End If
                Next

                For s = 0 To CSTrainsAndDrivers.PreParedDutyDrivers.Count - 1
                    myBook.Sheets(i + 1).Cells(tmpRow, 1) = CSTrainsAndDrivers.PreParedDutyDrivers(s).CSdriverNo
                    myBook.Sheets(i + 1).Cells(tmpRow, 2) = BeTime(CSTrainsAndDrivers.PreParedDutyDrivers(s).CSLinkTrain(1).StartTime - CSTrainsAndDrivers.PreParedDutyDrivers(s).PreDutyTime)
                    myBook.Sheets(i + 1).Cells(tmpRow, 3) = BeTime(CSTrainsAndDrivers.PreParedDutyDrivers(s).CSLinkTrain(1).StartTime)
                    myBook.Sheets(i + 1).Cells(tmpRow, 4) = CSTrainsAndDrivers.PreParedDutyDrivers(s).CSLinkTrain(1).StartStaName
                    myBook.Sheets(i + 1).Cells(tmpRow, 5) = "备车"
                    myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(tmpRow, 5), myBook.Sheets(i + 1).Cells(tmpRow, 6)).Merge()
                    myBook.Sheets(i + 1).Cells(tmpRow, 7) = BeTime(CSTrainsAndDrivers.PreParedDutyDrivers(s).CSLinkTrain(1).EndTime)
                    myBook.Sheets(i + 1).Cells(tmpRow, 8) = CSTrainsAndDrivers.PreParedDutyDrivers(s).CSLinkTrain(1).EndStaName
                    myBook.Sheets(i + 1).Cells(tmpRow, 9) = "->"
                    tmpRow = tmpRow + 1
                Next

                With myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(RecordRow0, tmpCol), myBook.Sheets(i + 1).Cells(tmpRow, tmpCol + 2 + maxDutyNum * 6 + maxDutyNum - 1))

                    .Borders(1).LineStyle = 1
                    .Borders(2).LineStyle = 1
                    .Borders(3).LineStyle = 1
                    .Borders(4).LineStyle = 1
                    .Borders(1).Weight = Excel.XlBorderWeight.xlThin
                    .Borders(2).Weight = Excel.XlBorderWeight.xlThin
                    .Borders(3).Weight = Excel.XlBorderWeight.xlThin
                    .Borders(4).Weight = Excel.XlBorderWeight.xlThin
                    .HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                    .VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter
                    '.AutoFit()
                End With
                With myBook.Sheets(i + 1).Range(myBook.Sheets(i + 1).Cells(RecordRow0 + 1, tmpCol), myBook.Sheets(i + 1).Cells(tmpRow, tmpCol + 2 + maxDutyNum * 6 + maxDutyNum - 1))

                    .Borders(1).LineStyle = -4115
                    .Borders(2).LineStyle = -4115
                    .Borders(3).LineStyle = -4115
                    .Borders(4).LineStyle = -4115
                    .Borders(1).Weight = Excel.XlBorderWeight.xlThin
                    .Borders(2).Weight = Excel.XlBorderWeight.xlThin
                    .Borders(3).Weight = Excel.XlBorderWeight.xlThin
                    .Borders(4).Weight = Excel.XlBorderWeight.xlThin
                    .HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                    .VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter
                    '.AutoFit()
                End With
            Next

        End If
        myExcel.Application.ActiveWindow.DisplayGridlines = False
        myExcel.Visible = True
        frmpro.EndProgress()
        GC.Collect()
        System.Runtime.InteropServices.Marshal.ReleaseComObject(myBook)
        System.Runtime.InteropServices.Marshal.ReleaseComObject(myExcel)
    End Sub

    '系统变量初始化
    Public Sub InitSystemVariant(ByVal nState As Integer)
        Dim i As Integer
        If nState = 0 Then
            ReDim ChediInfo(0)
        Else
            For i = 1 To UBound(ChediInfo)
                ReDim ChediInfo(i).nLinkTrain(0)
            Next
        End If

        ReDim TrainInf(0)
        ReDim CopyTrainInf(0)
    End Sub '系统变量初始化
    Public Sub CSInitSystemVariant()
        ReDim CSchediInfo(0) '车底
        ReDim CSTrainInf(0) '列车
        ReDim CopyTrainInf(0)
    End Sub

    '通过运行图名称找版本号
    Public Function GetDiagramVersionFromName(ByVal sName As String) As String
        Dim sqlstr As String = ""
        sqlstr = "SELECT * FROM TMS_TRAINDIAGRAMINFO "
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        GetDiagramVersionFromName = "空"
        Dim i As Integer
        For i = 1 To tempTable.Rows.Count
            If tempTable.Rows(i - 1).Item("TIMETABLENAME") = sName Then
                Return tempTable.Rows(i - 1).Item("TRAINDIAGRAMID")
                Exit For
            End If
        Next
    End Function
    ''' <summary>
    ''' 读取编制参数
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub MakeParaSet()
        Call ReadCSUniformInfFromOracle()
        Call ReadCSTransitStationInfFromOracle() 'ChangeStationList
        Call ReadCSDinnerStationInfFromOracle() 'DinnerStation
        Call ReadCSJiaoluInfFromOracle() 'Jiaolu
        Call ReadCSShiftPlaceInfFromOracle() 'ShiftPlace
        Call ReadPreParedInfo()
    End Sub

    Public Sub MakeParaSet(ByVal CSTimeTableID As String)
        Call ReadCSUniformInfFromOracle()
        Call ReadCSUniformInfFromOracle(CSTimeTableID)
        Call ReadCSTransitStationInfFromOracle() 'ChangeStationList
        Call ReadCSDinnerStationInfFromOracle() 'DinnerStation
        Call ReadCSJiaoluInfFromOracle() 'Jiaolu
        Call ReadCSShiftPlaceInfFromOracle() 'ShiftPlace
        Call ReadPreParedInfo(CSTimeTableID)
    End Sub
    ''' <summary>
    ''' 读取制度信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReadCSUniformInfFromOracle()
        Dim sqlstr As String = ""
        Try
            sqlstr = "SELECT * FROM CS_CREWBASICINF WHERE LINEID='" & CStr(strCurlineID) & "'"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            If tempTable.Rows.Count > 0 Then
                MStartTime = CDate(tempTable.Rows(0).Item("Time1").ToString.Trim).TimeOfDay.TotalSeconds
                NStartTime = CDate(tempTable.Rows(0).Item("Time2").ToString.Trim).TimeOfDay.TotalSeconds
                AStartTime = CDate(tempTable.Rows(0).Item("Time3").ToString.Trim).TimeOfDay.TotalSeconds
                ConDriveTime = CDate(tempTable.Rows(0).Item("CONDRIVETIME").ToString.Trim).TimeOfDay.TotalSeconds
                PrepareTrainTime = CDate(tempTable.Rows(0).Item("PrepareTrainTime").ToString.Trim).TimeOfDay.TotalSeconds
                PrepareDutyTime = CDate(tempTable.Rows(0).Item("PrepareDutyTime").ToString.Trim).TimeOfDay.TotalSeconds
                PrepareDutyOffTime = CDate(tempTable.Rows(0).Item("PreDutyOffTime").ToString.Trim).TimeOfDay.TotalSeconds
                CS_MorningMaxLength = CDec(tempTable.Rows(0).Item("MorningDistance").ToString.Trim)
                CS_DayMaxLength = CDec(tempTable.Rows(0).Item("DayDistance").ToString.Trim)
                CS_NightMaxLength = CDec(tempTable.Rows(0).Item("NightDistance").ToString.Trim)
                CS_CDayMaxLength = CDec(tempTable.Rows(0).Item("CDayDistance").ToString.Trim)
                CS_MorningMinLength = CDec(tempTable.Rows(0).Item("MorningMinDistance").ToString.Trim)
                CS_DayMinLength = CDec(tempTable.Rows(0).Item("DayMinDistance").ToString.Trim)
                CS_NightMinLength = CDec(tempTable.Rows(0).Item("NightMinDistance").ToString.Trim)
                CS_CDayMinLength = CDec(tempTable.Rows(0).Item("CDayMinDistance").ToString.Trim)
                MornWorkTime = CDate(tempTable.Rows(0).Item("DAYWTIME").ToString.Trim).TimeOfDay.TotalSeconds
                NoonWorkTime = CDate(tempTable.Rows(0).Item("NOONWTIME").ToString.Trim).TimeOfDay.TotalSeconds
                NightWorkTime = CDate(tempTable.Rows(0).Item("NIGHTWTIME").ToString.Trim).TimeOfDay.TotalSeconds
                minRuDis = CDec(tempTable.Rows(0).Item("SPECIALCHURU").ToString.Trim.Split(";")(0))
                minChuDis = CDec(tempTable.Rows(0).Item("SPECIALCHURU").ToString.Trim.Split(";")(1))
                Dim checkpara As String = tempTable.Rows(0).Item("uniform").ToString.Trim
                If checkpara.Split("|")(0).Trim = "1" Then
                    ForceDutyTime = 1
                Else
                    ForceDutyTime = 0
                End If
                If checkpara.Split("|")(1).Trim = "1" Then
                    ForceDriveLength = 1
                Else
                    ForceDriveLength = 0
                End If
            End If
            tempTable.Dispose()
            CSAutoPlanPara = Nothing
        Catch ex As Exception

        End Try
    End Sub

    ''' <summary>
    ''' 读取制度信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReadCSUniformInfFromOracle(ByVal CSTimeTableID As String)
        Dim sqlstr As String = ""
        Try
          
            ''======自动编制参数
            CSAutoPlanPara = Nothing
            sqlstr = "SELECT * FROM cs_autoplanpara WHERE cstimetableid='" & CSTimeTableID & "'"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            If tempTable.Rows.Count = 1 Then
                CSAutoPlanPara = New AutoPara(CInt(tempTable.Rows(0).Item("moringdutynum")), CInt(tempTable.Rows(0).Item("daydutynum")), CInt(tempTable.Rows(0).Item("cdaydutynum")), CInt(tempTable.Rows(0).Item("nightdutynum")))
            End If

            tempTable.Dispose()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub ReadCSTransitStationInfFromOracle() 'april’Zhou
        Dim i As Integer
        Dim sqlstr As String = ""
        '读入换班点信息
        sqlstr = "SELECT * FROM CS_CHANGEPLACEINF WHERE LINEID='" & CStr(strCurlineID) & "' and timetableid='" & CStr(DiagramCurID) & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        ChangeStationList = New List(Of ChangeStation)

        For i = 1 To tempTable.Rows.Count
            Dim rn, name As String
            Dim dir, dir1 As Integer
            Dim sdate, edate, restdate As Date
            rn = tempTable.Rows(i - 1).Item("RoutingName").ToString.Trim
            name = tempTable.Rows(i - 1).Item("ChangePlace").ToString.Trim
            dir = tempTable.Rows(i - 1).Item("DirectionMatch").ToString.Trim
            If tempTable.Rows(i - 1).Item("DirectionMatch1").ToString.Trim <> "" Then
                dir1 = tempTable.Rows(i - 1).Item("DirectionMatch1").ToString.Trim
            End If
            If tempTable.Rows(i - 1).Item("BETIME").ToString.Trim <> "" Then
                sdate = CDate(tempTable.Rows(i - 1).Item("BETIME").ToString.Trim)
            End If

            If tempTable.Rows(i - 1).Item("ENDTIME").ToString.Trim <> "" Then
                edate = CDate(tempTable.Rows(i - 1).Item("ENDTIME").ToString.Trim)
            End If
            restdate = CDate(tempTable.Rows(i - 1).Item("resttime").ToString.Trim)
            Dim d As New ChangeStation(rn, name, dir, dir1, restdate.TimeOfDay.TotalSeconds)
            d.TimeSpanList = New clsTimeSpan(sdate, edate)
            d.IfMustChange = tempTable.Rows(i - 1).Item("IFMustChange")
            d.FollowNo = Integer.Parse(tempTable.Rows(i - 1).Item("FOLLOWDRIVER").ToString)
            ChangeStationList.Add(d)
        Next
        tempTable.Dispose()
    End Sub

   

    ''' <summary>
    '''  '读取用餐点信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReadCSDinnerStationInfFromOracle()
        Dim i As Integer
        Dim sqlstr As String = ""
        sqlstr = "SELECT * FROM CS_DINNERTIME WHERE LINEID='" & CStr(strCurlineID) & "' and timetableid='" & CStr(DiagramCurID) & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        ReDim sysDinnerStation(tempTable.Rows.Count)
        For i = 1 To tempTable.Rows.Count
            sysDinnerStation(i) = New typeDinnerStation
            sysDinnerStation(i).DinnerStationName = tempTable.Rows(i - 1).Item("STATIONNAME").ToString.Trim
            sysDinnerStation(i).Direction = tempTable.Rows(i - 1).Item("DirectionMatch").ToString.Trim
            sysDinnerStation(i).RiDirection = tempTable.Rows(i - 1).Item("DirectionMatch1").ToString.Trim
            sysDinnerStation(i).Routing = tempTable.Rows(i - 1).Item("routing").ToString.Trim
            sysDinnerStation(i).dinnerStartTime = tempTable.Rows(i - 1).Item("starttime").ToString.Trim
            sysDinnerStation(i).dinnerEndTime = tempTable.Rows(i - 1).Item("endtime").ToString.Trim
            sysDinnerStation(i).dutySort = tempTable.Rows(i - 1).Item("dutysort").ToString.Trim
            sysDinnerStation(i).DinnerTime = tempTable.Rows(i - 1).Item("dinnertime").ToString.Trim
            sysDinnerStation(i).dinnerType = tempTable.Rows(i - 1).Item("dinnertype").ToString.Trim
        Next
        tempTable.Dispose()
    End Sub

    ''' <summary>
    '''  '读取交路信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReadCSJiaoluInfFromOracle()
        Dim i As Integer
        Dim sqlstr As String = ""
        sqlstr = "SELECT * FROM CS_ROUTINGINF WHERE LINEID='" & CStr(strCurlineID) & "' and timetableid='" & CStr(DiagramCurID) & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        ReDim Jiaolu(tempTable.Rows.Count)
        For i = 1 To tempTable.Rows.Count
            Jiaolu(i).JiaoluName = tempTable.Rows(i - 1).Item("RoutingName").ToString.Trim
            Jiaolu(i).CrewType = tempTable.Rows(i - 1).Item("CrewType").ToString.Trim
            Jiaolu(i).StartStationName = tempTable.Rows(i - 1).Item("StartStaName").ToString.Trim
            Jiaolu(i).EndStationName = tempTable.Rows(i - 1).Item("EndStaName").ToString.Trim
            Jiaolu(i).ReJiaoluName = tempTable.Rows(i - 1).Item("ReRoutingName").ToString.Trim
        Next
        tempTable.Dispose()
    End Sub

  

    ''' <summary>
    '''  '读取上班点信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReadCSShiftPlaceInfFromOracle()
        Dim i As Integer
        Dim sqlstr As String = ""
        '============平常班的上下班地点信息
        sqlstr = "SELECT * FROM CS_SHIFTPLACEINF WHERE LINEID='" & CStr(strCurlineID) & "' and timetableid='" & CStr(DiagramCurID) & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        ReDim ShiftPlace(tempTable.Rows.Count)
        For i = 1 To tempTable.Rows.Count
            ShiftPlace(i).ShiftStationStationName = tempTable.Rows(i - 1).Item("ShiftPlace").ToString.Trim
            ShiftPlace(i).Direction = tempTable.Rows(i - 1).Item("DirectionMatch").ToString.Trim
            ShiftPlace(i).DayDutyStartTime = CDate(tempTable.Rows(i - 1).Item("DayDutyStartTime")).TimeOfDay.TotalSeconds
            ShiftPlace(i).DayDutyEndTime = CDate(tempTable.Rows(i - 1).Item("NightDutyStartTime")).TimeOfDay.TotalSeconds
            ShiftPlace(i).Routing = tempTable.Rows(i - 1).Item("Routing").ToString.Trim
            ShiftPlace(i).AvaliableOffPlaces = tempTable.Rows(i - 1).Item("AvaDutyOffPlace").ToString.Split("|")
        Next

        tempTable.Dispose()
    End Sub

  
 
  

    ''' <summary>
    ''' 读取备车和备班信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReadPreParedInfo()
        CSTrainsAndDrivers.PreParedTrainDrivers.Clear()
        CSTrainsAndDrivers.PreParedDutyDrivers.Clear()
        tempTable = New Data.DataTable
        tempTable = ReadData("select * from cs_preparedtraininf t WHERE t.LINEID='" & CStr(strCurlineID) & "' order by t.dutysort,t.name")
        If tempTable.Rows.Count > 0 Then
            For Each row As DataRow In tempTable.Rows
                Dim tempDri As New CSDriver(0)
                tempDri.CSdriverNo = row.Item("Name").ToString.Trim
                tempDri.DutySort = row.Item("DutySort").ToString.Trim
                tempDri.BelongArea = row.Item("Remark").ToString.Trim
                tempDri.IsPrepareDri = True
                Dim tempTrain As New CSLinkTrain()
                tempTrain.StartStaName = row.Item("place").ToString.Trim
                tempTrain.StartTime = row.Item("starttime").ToString.Trim
                tempTrain.EndStaName = row.Item("place").ToString.Trim
                tempTrain.EndTime = row.Item("endtime").ToString.Trim
                tempDri.AddTrain(tempTrain)
                CSTrainsAndDrivers.PreParedTrainDrivers.Add(tempDri)
            Next
        End If
        tempTable = ReadData("select * from cs_prepareddutyinf t WHERE t.LINEID='" & CStr(strCurlineID) & "' order by t.dutysort,t.name")
        If tempTable.Rows.Count > 0 Then
            For Each row As DataRow In tempTable.Rows
                Dim tempDri As New CSDriver(0)
                tempDri.CSdriverNo = row.Item("Name").ToString.Trim
                tempDri.DutySort = row.Item("DutySort").ToString.Trim
                tempDri.BelongArea = row.Item("Remark").ToString.Trim
                tempDri.IsPrepareDri = True
                Dim tempTrain As New CSLinkTrain()
                tempTrain.StartStaName = row.Item("place").ToString.Trim
                tempTrain.StartTime = row.Item("starttime").ToString.Trim
                tempTrain.EndStaName = row.Item("place").ToString.Trim
                tempTrain.EndTime = row.Item("endtime").ToString.Trim
                tempDri.AddTrain(tempTrain)
                CSTrainsAndDrivers.PreParedDutyDrivers.Add(tempDri)
            Next
        End If
        tempTable.Dispose()
    End Sub

    ''' <summary>
    ''' 读取备车和备班信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ReadPreParedInfo(ByVal CSTimeTableID As String)
        CSTrainsAndDrivers.PreParedTrainDrivers.Clear()
        CSTrainsAndDrivers.PreParedDutyDrivers.Clear()
        tempTable = New Data.DataTable
        tempTable = ReadData("select * from cs_result_preparedtraininf t WHERE t.LINEID='" & CStr(strCurlineID) & "' and cstimetableid='" & CSTimeTableID & "' order by t.dutysort,t.name")
        If tempTable.Rows.Count > 0 Then
            For Each row As DataRow In tempTable.Rows
                Dim tempDri As New CSDriver(0)
                tempDri.CSdriverNo = row.Item("Name").ToString.Trim
                tempDri.DutySort = row.Item("DutySort").ToString.Trim
                tempDri.BelongArea = row.Item("Remark").ToString.Trim
                tempDri.OutPutCSdriverNo = row.Item("outputcsdriverno").ToString.Trim
                tempDri.IsPrepareDri = True
                Dim tempTrain As New CSLinkTrain()
                tempTrain.StartStaName = row.Item("place").ToString.Trim
                tempTrain.StartTime = row.Item("starttime").ToString.Trim
                tempTrain.EndStaName = row.Item("place").ToString.Trim
                tempTrain.EndTime = row.Item("endtime").ToString.Trim
                tempDri.AddTrain(tempTrain)
                CSTrainsAndDrivers.PreParedTrainDrivers.Add(tempDri)
            Next
        End If
        tempTable = ReadData("select * from cs_result_prepareddutyinf t WHERE t.LINEID='" & CStr(strCurlineID) & "' and cstimetableid='" & CSTimeTableID & "' order by t.dutysort,t.name")
        If tempTable.Rows.Count > 0 Then
            For Each row As DataRow In tempTable.Rows
                Dim tempDri As New CSDriver(0)
                tempDri.CSdriverNo = row.Item("Name").ToString.Trim
                tempDri.DutySort = row.Item("DutySort").ToString.Trim
                tempDri.BelongArea = row.Item("Remark").ToString.Trim
                tempDri.OutPutCSdriverNo = row.Item("outputcsdriverno").ToString.Trim
                tempDri.IsPrepareDri = True
                Dim tempTrain As New CSLinkTrain()
                tempTrain.StartStaName = row.Item("place").ToString.Trim
                tempTrain.StartTime = row.Item("starttime").ToString.Trim
                tempTrain.EndStaName = row.Item("place").ToString.Trim
                tempTrain.EndTime = row.Item("endtime").ToString.Trim
                tempDri.AddTrain(tempTrain)
                CSTrainsAndDrivers.PreParedDutyDrivers.Add(tempDri)
            Next
        End If
        tempTable.Dispose()
    End Sub

    ''' <summary>
    '''  '判断是否为白班上班点或早班退勤点
    ''' </summary>
    ''' <remarks></remarks>
    Public Function IsDayDutyOnPlace(ByVal StationName As String, ByVal tempFlagUpDown As Integer, ByVal Routing As String, ByVal temptime As Integer) As Boolean
        IsDayDutyOnPlace = False
        Dim i As Integer
        For i = 1 To UBound(ShiftPlace)
            If StationName = ShiftPlace(i).ShiftStationStationName And (ShiftPlace(i).Direction = tempFlagUpDown Or ShiftPlace(i).Direction = 2) And ShiftPlace(i).Routing = Routing Then
                If ShiftPlace(i).DayDutyStartTime = ShiftPlace(i).DayDutyEndTime Then
                    IsDayDutyOnPlace = True
                    Exit For
                Else
                    If AddLitterTime(temptime) >= AddLitterTime(ShiftPlace(i).DayDutyStartTime) And AddLitterTime(temptime) <= AddLitterTime(ShiftPlace(i).DayDutyEndTime) Then
                        IsDayDutyOnPlace = True
                        Exit For
                    End If
                End If
            End If
        Next
    End Function

    ''' <summary>
    '''  '判断是否为白班退勤地点
    ''' </summary>
    ''' <remarks></remarks>
    Public Function IsDayDutyOffPlace(ByVal StationName As String, ByVal Routing As String, ByVal OnDutyStaName As String, ByVal OnDutyRouting As String, ByVal temptime As Integer) As Boolean
        IsDayDutyOffPlace = False
        Dim i As Integer
        For i = 1 To UBound(ShiftPlace)
            If OnDutyStaName = ShiftPlace(i).ShiftStationStationName Then
                For z As Integer = 1 To UBound(Jiaolu)
                    If Jiaolu(z).JiaoluName = ShiftPlace(i).Routing AndAlso Jiaolu(z).ReJiaoluName = OnDutyRouting Then
                        If ShiftPlace(i).DayDutyStartTime = ShiftPlace(i).DayDutyEndTime Then
                            For j As Integer = 0 To UBound(ShiftPlace(i).AvaliableOffPlaces)
                                Dim RoutANDSta() As String = ShiftPlace(i).AvaliableOffPlaces(j).Split(":")
                                If RoutANDSta(0) = Routing And RoutANDSta(1) = StationName Then
                                    Return True
                                End If
                            Next
                        Else
                            If AddLitterTime(temptime) >= AddLitterTime(ShiftPlace(i).DayDutyStartTime) And AddLitterTime(temptime) <= AddLitterTime(ShiftPlace(i).DayDutyEndTime) Then
                                For j As Integer = 0 To UBound(ShiftPlace(i).AvaliableOffPlaces)
                                    Dim RoutANDSta() As String = ShiftPlace(i).AvaliableOffPlaces(j).Split(":")
                                    If RoutANDSta(0) = Routing And RoutANDSta(1) = StationName Then
                                        Return True
                                    End If
                                Next
                            End If
                        End If
                    End If
                Next
            End If
        Next
    End Function

    Public Function IsShiftPlace(ByVal StationName As String, ByVal Routing As String, ByVal temptime As Integer) As Boolean
        For i = 1 To UBound(ShiftPlace)
            For j = 0 To UBound(ShiftPlace(i).AvaliableOffPlaces)
                If ShiftPlace(i).AvaliableOffPlaces(j) <> "" Then
                    Dim RoutANDSta() As String = ShiftPlace(i).AvaliableOffPlaces(j).Split(":")
                    If RoutANDSta(0) = Routing And RoutANDSta(1) = StationName Then
                        Return True
                    End If
                End If
            Next
        Next

        For i = 1 To UBound(ShiftPlace)
            If StationName = ShiftPlace(i).ShiftStationStationName And Routing = ShiftPlace(i).Routing Then
                If ShiftPlace(i).DayDutyStartTime = ShiftPlace(i).DayDutyEndTime Then
                    Return True
                Else
                    If AddLitterTime(temptime) >= AddLitterTime(ShiftPlace(i).DayDutyStartTime) And AddLitterTime(temptime) <= AddLitterTime(ShiftPlace(i).DayDutyEndTime) Then
                        Return True
                    End If
                End If
            End If
        Next
        Return False
    End Function
    Public Function IsNightDutyon(ByVal StationName As String, ByVal Routing As String, ByVal temptime As Integer) As Boolean '逆向条件下
        For i = 1 To UBound(ShiftPlace)
            If ShiftPlace(i).DayDutyStartTime <> ShiftPlace(i).DayDutyEndTime Then
                If AddLitterTime(temptime) < AddLitterTime(ShiftPlace(i).DayDutyStartTime) Or AddLitterTime(temptime) > AddLitterTime(ShiftPlace(i).DayDutyEndTime) Then
                    Continue For
                End If
            End If
            For j = 0 To UBound(ShiftPlace(i).AvaliableOffPlaces)
                If ShiftPlace(i).AvaliableOffPlaces(j) <> "" Then
                    Dim RoutANDSta() As String = ShiftPlace(i).AvaliableOffPlaces(j).Split(":")
                    If RoutANDSta(1) = StationName Then
                        For z As Integer = 0 To UBound(Jiaolu)
                            If Jiaolu(z).JiaoluName = RoutANDSta(0) And Jiaolu(z).ReJiaoluName = Routing Then
                                Return True
                            End If
                        Next
                    End If
                End If
            Next
        Next
        Return False
    End Function

    ''' <summary>
    '''  '判断是否为用餐点
    ''' </summary>
    ''' <remarks></remarks>
    Public Function IsDinnerPlace(ByVal StationName As String, ByVal tempFlagUpDown As Integer, ByVal Routing As String, Optional ByVal direction As Boolean = True) As Boolean 'direction 正向
        IsDinnerPlace = False
        Dim i As Integer
        For i = 1 To UBound(sysDinnerStation)
            If direction = True Then
                If StationName = sysDinnerStation(i).DinnerStationName And (sysDinnerStation(i).Direction = tempFlagUpDown Or sysDinnerStation(i).Direction = 2) And Routing = sysDinnerStation(i).Routing Then
                    IsDinnerPlace = True
                    Exit For
                End If
            Else
                For j As Integer = 1 To Jiaolu.Count - 1
                    If Jiaolu(j).JiaoluName = sysDinnerStation(i).Routing Then
                        If StationName = sysDinnerStation(i).DinnerStationName And (sysDinnerStation(i).RiDirection = tempFlagUpDown Or sysDinnerStation(i).RiDirection = 2) And Routing = Jiaolu(j).ReJiaoluName Then
                            IsDinnerPlace = True
                            Exit For
                        End If
                    End If
                Next
            End If
        Next
    End Function
    ''' <summary>
    '''  '判断是否为轮换点
    ''' </summary>
    ''' <remarks></remarks>
    Public Function IsTransitStationPlace(ByVal StationName As String, ByVal tempsJiaoLuName As String, ByVal tempFlagUpDown As Integer, ByVal temptime As Integer) As Boolean
        IsTransitStationPlace = False
        Dim i As Integer
        Dim tempResJiaoLuName As String = ""
        For i = 0 To ChangeStationList.Count - 1
            If ChangeStationList(i).Name = StationName And ChangeStationList(i).JiaoLuName = tempsJiaoLuName Then
                If (ChangeStationList(i).Direction = tempFlagUpDown Or ChangeStationList(i).Direction = 2) Then
                    If ChangeStationList(i).TimeSpanList.EndTime = ChangeStationList(i).TimeSpanList.StartTime Then '没有时间限制
                        IsTransitStationPlace = True
                        Exit Function
                    End If
                    If AddLitterTime(ChangeStationList(i).TimeSpanList.StartTime) <= AddLitterTime(temptime) And AddLitterTime(temptime) <= AddLitterTime(ChangeStationList(i).TimeSpanList.EndTime) Then
                        IsTransitStationPlace = True
                        Exit Function
                    End If
                    Exit Function
                End If
            End If
        Next
    End Function
    Public Function TransitStationPlaceforUptrain(ByVal StationName As String, ByVal tempsJiaoLuName As String, ByVal tempFlagUpDown As Integer, ByVal temptime As Integer) As Integer '返回接车方向
        TransitStationPlaceforUptrain = -1
        For i As Integer = 0 To ChangeStationList.Count - 1
            If ChangeStationList(i).Name = StationName And ChangeStationList(i).JiaoLuName = tempsJiaoLuName Then
                If (ChangeStationList(i).Direction = tempFlagUpDown Or ChangeStationList(i).Direction = 2) Then
                    If ChangeStationList(i).TimeSpanList.EndTime = ChangeStationList(i).TimeSpanList.StartTime Then '没有时间限制
                        TransitStationPlaceforUptrain = ChangeStationList(i).UpTrainDirection
                        Exit Function
                    End If
                    If AddLitterTime(ChangeStationList(i).TimeSpanList.StartTime) <= AddLitterTime(temptime) And AddLitterTime(temptime) <= AddLitterTime(ChangeStationList(i).TimeSpanList.EndTime) Then
                        TransitStationPlaceforUptrain = ChangeStationList(i).UpTrainDirection
                        Exit Function
                    End If
                End If
            End If
        Next
    End Function
    Public Function ChangePlaceRestTime(ByVal StationName As String, ByVal tempsJiaoLuName As String, ByVal tempFlagUpDown As Integer, ByVal temptime As Integer, Optional ByVal searDir As Boolean = True) As Integer  '返回各休息点的休息时间
        ChangePlaceRestTime = 0
        For i As Integer = 0 To ChangeStationList.Count - 1
            If ChangeStationList(i).Name = StationName Then
                If searDir = True Then
                    If ChangeStationList(i).JiaoLuName = tempsJiaoLuName Then
                        If (ChangeStationList(i).Direction = tempFlagUpDown Or ChangeStationList(i).Direction = 2) Then

                            If ChangeStationList(i).TimeSpanList.EndTime = ChangeStationList(i).TimeSpanList.StartTime Then '没有时间限制
                                ChangePlaceRestTime = ChangeStationList(i).RestTime
                                Exit Function
                            End If
                            If AddLitterTime(ChangeStationList(i).TimeSpanList.StartTime) <= AddLitterTime(temptime) And AddLitterTime(temptime) <= AddLitterTime(ChangeStationList(i).TimeSpanList.EndTime) Then
                                ChangePlaceRestTime = ChangeStationList(i).RestTime
                                Exit Function
                            End If
                        End If
                    End If
                Else
                    For j As Integer = 1 To UBound(Jiaolu)
                        If Jiaolu(j).ReJiaoluName = tempsJiaoLuName AndAlso Jiaolu(j).JiaoluName = ChangeStationList(i).JiaoLuName Then
                            If (ChangeStationList(i).UpTrainDirection = tempFlagUpDown Or ChangeStationList(i).Direction = 2) Then

                                If ChangeStationList(i).TimeSpanList.EndTime = ChangeStationList(i).TimeSpanList.StartTime Then '没有时间限制
                                    ChangePlaceRestTime = ChangeStationList(i).RestTime
                                    Exit Function
                                End If
                                If AddLitterTime(ChangeStationList(i).TimeSpanList.StartTime) <= AddLitterTime(temptime) - ChangeStationList(i).RestTime And AddLitterTime(temptime) - ChangeStationList(i).RestTime <= AddLitterTime(ChangeStationList(i).TimeSpanList.EndTime) Then
                                    ChangePlaceRestTime = ChangeStationList(i).RestTime
                                    Exit Function
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        Next
    End Function
    Public Function GetJiaoLuName(ByVal ReJiaoLu As String) As String '逆向交路找正向交路
        Dim JiaoluName As String = ""
        For i As Integer = 1 To UBound(Jiaolu)
            If Jiaolu(i).ReJiaoluName = ReJiaoLu Then
                JiaoluName = Jiaolu(i).JiaoluName
            End If
        Next
        Return JiaoluName
    End Function
    Public Function GetRJiaoLuName(ByVal JiaoLuName1 As String) As String '正向交路找逆向交路
        Dim RJiaoluName As String = ""
        For i As Integer = 1 To UBound(Jiaolu)
            If Jiaolu(i).JiaoluName = JiaoLuName1 Then
                RJiaoluName = Jiaolu(i).ReJiaoluName
            End If
        Next
        Return RJiaoluName
    End Function
    ''' <summary>
    '''  '判断是否为必换点
    ''' </summary>
    ''' <remarks></remarks>
    Public Function IsTransitMustChange(ByVal StationName As String, ByVal tempsJiaoLuName As String, ByVal tempFlagUpDown As Integer, ByVal temptime As Integer) As Boolean
        IsTransitMustChange = False
        Dim i As Integer
        For i = 0 To ChangeStationList.Count - 1
            If ChangeStationList(i).Name = StationName AndAlso ChangeStationList(i).IfMustChange AndAlso ChangeStationList(i).JiaoLuName = tempsJiaoLuName Then
                If (ChangeStationList(i).Direction = tempFlagUpDown Or ChangeStationList(i).Direction = 2) Then

                    If ChangeStationList(i).TimeSpanList.EndTime = ChangeStationList(i).TimeSpanList.StartTime Then
                        IsTransitMustChange = True
                        Exit Function
                    End If
                    If AddLitterTime(ChangeStationList(i).TimeSpanList.StartTime) <= AddLitterTime(temptime) And AddLitterTime(temptime) <= AddLitterTime(ChangeStationList(i).TimeSpanList.EndTime) Then
                        IsTransitMustChange = True
                        Exit Function
                    End If
                End If
            End If
        Next
    End Function

    Public Function ReadData(ByVal SqlStr As String) As DataTable          '读取数据
        ReadData = Nothing
        Dim tempTab As New DataTable
        tempTab = Globle.Method.ReadDataForAccess(SqlStr)
        ReadData = tempTab
    End Function

    Public Sub ReadCSTableDataFromOracle()
        Dim DaTab As New DataTable
        Dim DaAdapter As New OleDb.OleDbDataAdapter
        Dim i, j, flag, nDriverNum As Integer
        Dim Str As String
        If strQCurCSPlanName = "" Then
            MsgBox("请先选择乘务计划！", MsgBoxStyle.OkOnly, "提示")
            Exit Sub
        End If
        strQCurCSPlanID = GetCSPlanIDFromCSPlanName(strQCurCSPlanName)
        Str = "SELECT * FROM  CS_WORKLOAD t WHERE t.LINEID='" & CStr(strCurlineID) & "' AND t.CSTIMETABLEID='" & CStr(strQCurCSPlanID) & "' order by t.DriverNo"
        DaTab = New DataTable
        DaTab = Globle.Method.ReadDataForAccess(Str)
        nDriverNum = DaTab.Rows.Count
        If DaTab.Rows.Count = 0 Then
            MsgBox("没有数据")
            Exit Sub
        Else
            ReDim CSTrainsAndDrivers.CSDrivers(nDriverNum)
            For i = 1 To nDriverNum
                CSTrainsAndDrivers.CSDrivers(i) = New CSDriver()
                CSTrainsAndDrivers.CSDrivers(i).CSDriverID = i
                CSTrainsAndDrivers.CSDrivers(i).CSdriverNo = DaTab.Rows(i - 1).Item("DriverNo").ToString
                CSTrainsAndDrivers.CSDrivers(i).DutySort = DaTab.Rows(i - 1).Item("DUTYSORT").ToString

                CSTrainsAndDrivers.CSDrivers(i).BelongArea = DaTab.Rows(i - 1).Item("BelongArea").ToString
                CSTrainsAndDrivers.CSDrivers(i).OutPutCSdriverNo = DaTab.Rows(i - 1).Item("OutPutCSdriverNo").ToString
                If DaTab.Rows(i - 1).Item("DefaultColor").ToString = "" Then
                    CSTrainsAndDrivers.CSDrivers(i).DefaultColor = Nothing
                Else
                    CSTrainsAndDrivers.CSDrivers(i).DefaultColor = ColorTranslator.FromHtml(DaTab.Rows(i - 1).Item("DefaultColor").ToString)
                End If
                CSTrainsAndDrivers.CSDrivers(i).State = DaTab.Rows(i - 1).Item("State").ToString
                ReDim CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(0)
            Next
            Str = "SELECT * FROM  cs_dinnerinf t WHERE t.LINEID='" & CStr(strCurlineID) & "' AND t.CSTIMETABLEID='" & CStr(strQCurCSPlanID) & "' order by t.DriverNo"
            DaTab = ReadData(Str)
            If DaTab IsNot Nothing AndAlso DaTab.Rows.Count > 0 Then
                For i = 1 To nDriverNum
                    For j = 1 To DaTab.Rows.Count
                        If DaTab.Rows(j - 1).Item("DRIVERNO").ToString.Trim = CSTrainsAndDrivers.CSDrivers(i).CSdriverNo Then
                            CSTrainsAndDrivers.CSDrivers(i).DinnerStartTime = Val(DaTab.Rows(j - 1).Item("DINNERBEGINTIME").ToString)
                            CSTrainsAndDrivers.CSDrivers(i).DinnerEndTime = Val(DaTab.Rows(j - 1).Item("DINNERENDTIME").ToString)
                            CSTrainsAndDrivers.CSDrivers(i).FlagDinner = CType(DaTab.Rows(j - 1).Item("havedinner").ToString, Boolean)
                            CSTrainsAndDrivers.CSDrivers(i).DinnerStation = DaTab.Rows(j - 1).Item("DINNERPLACE").ToString
                            Dim dinnerTimeitem As typeDinnerStation = Nothing
                            For z As Integer = 1 To sysDinnerStation.Count - 1
                                If sysDinnerStation(z).dutySort = CSTrainsAndDrivers.CSDrivers(i).DutySort Then
                                    If CSTrainsAndDrivers.CSDrivers(i).DinnerStation = sysDinnerStation(z).DinnerStationName And ((AddLitterTime(CSTrainsAndDrivers.CSDrivers(i).DinnerStartTime) >= AddLitterTime(sysDinnerStation(z).dinnerStartTime) And AddLitterTime(CSTrainsAndDrivers.CSDrivers(i).DinnerStartTime) <= AddLitterTime(sysDinnerStation(z).dinnerEndTime)) Or (AddLitterTime(CSTrainsAndDrivers.CSDrivers(i).DinnerEndTime) >= AddLitterTime(sysDinnerStation(z).dinnerStartTime) And AddLitterTime(CSTrainsAndDrivers.CSDrivers(i).DinnerEndTime) <= AddLitterTime(sysDinnerStation(z).dinnerEndTime))) Then '防止白班出现2次吃饭，也只有白班可能出现
                                        dinnerTimeitem = sysDinnerStation(z)
                                        CSTrainsAndDrivers.CSDrivers(i).AllDinnerInfo.Add(CSTrainsAndDrivers.CSDrivers(i).DinnerStation & "-" & CSTrainsAndDrivers.CSDrivers(i).DinnerStartTime.ToString & "-" & CSTrainsAndDrivers.CSDrivers(i).DinnerEndTime.ToString, dinnerTimeitem)
                                        Exit For
                                    End If
                                End If
                            Next
                            Exit For
                        End If
                    Next
                Next
            End If

            Str = "Select * from cs_amdrivercorrespond where adrivertimetableid='" & strQCurCSPlanID & "' and mdrivertimetableid='" & strQCurCSPlanID & "'"
            DaTab = ReadData(Str)
            If DaTab IsNot Nothing Then
                For Each row As DataRow In DaTab.Rows
                    Dim MorDriNo As String = row.Item("MDriverno").ToString
                    Dim NitDriNo As String = row.Item("ADriverno").ToString
                    Dim MDriver As CSDriver = Array.Find(CSTrainsAndDrivers.CSDrivers, Function(value As CSDriver)
                                                                                           Return (value IsNot Nothing AndAlso value.CSdriverNo = MorDriNo)
                                                                                       End Function)
                    If MDriver Is Nothing Then
                        MDriver = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                                  Return (value.CSdriverNo = MorDriNo AndAlso value.DutySort = "早班")
                                                                              End Function)
                        If MDriver Is Nothing Then
                            MDriver = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                       Return (value.CSdriverNo = MorDriNo AndAlso value.DutySort = "早班")
                                                                                   End Function)
                        End If
                    End If
                    Dim ADriver As CSDriver = Array.Find(CSTrainsAndDrivers.CSDrivers, Function(value As CSDriver)
                                                                                           Return (value IsNot Nothing AndAlso value.CSdriverNo = NitDriNo)
                                                                                       End Function)
                    If ADriver Is Nothing Then
                        ADriver = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                                  Return (value.CSdriverNo = NitDriNo AndAlso value.DutySort = "夜班")
                                                                              End Function)
                        If ADriver Is Nothing Then
                            ADriver = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                       Return (value.CSdriverNo = NitDriNo AndAlso value.DutySort = "夜班")
                                                                                   End Function)
                        End If
                    End If
                    If MDriver IsNot Nothing AndAlso ADriver IsNot Nothing Then
                        ADriver.LinkedMorDriver = MDriver
                        MDriver.LinkedNightDriver = ADriver
                    ElseIf MDriver Is Nothing AndAlso ADriver IsNot Nothing Then
                        ADriver.LinkedMorDriver = Nothing
                    ElseIf MDriver IsNot Nothing AndAlso ADriver Is Nothing Then
                        MDriver.LinkedNightDriver = Nothing
                    End If
                Next
            End If

            Str = "SELECT * FROM  CS_CREWSCHEDULE WHERE LINEID='" & CStr(strCurlineID) & "' AND CSTIMETABLEID='" & CStr(strQCurCSPlanID) & "'  and  trainno <>'热备' and trainno <>'预备' order by ID "
            DaTab = New DataTable
            DaTab = Globle.Method.ReadDataForAccess(Str)
            Dim Point As Integer = 1
            ReDim CSTrainsAndDrivers.CSLinkTrains(0)
            ReDim CSTrainsAndDrivers.MergedCSLinkTrains(DaTab.Rows.Count)
            For i = 1 To DaTab.Rows.Count
                Dim TempCSLinkTrain As New CSLinkTrain
                If CInt(DaTab.Rows(i - 1).Item("DateNo").ToString.Trim) > 0 Then
                    TempCSLinkTrain.CSTrainID = Point
                    Point += 1
                Else
                    TempCSLinkTrain.CSTrainID = -1
                End If
                TempCSLinkTrain.RoutingName = DaTab.Rows(i - 1).Item("ROUTING").ToString.Trim
                TempCSLinkTrain.OutputCheCi = DaTab.Rows(i - 1).Item("TrainNo").ToString.Trim
                TempCSLinkTrain.OffCheCi = DaTab.Rows(i - 1).Item("OffTrainNo").ToString.Trim
                TempCSLinkTrain.StartStaName = DaTab.Rows(i - 1).Item("StartStaName").ToString.Trim
                TempCSLinkTrain.StartTime = CInt(DaTab.Rows(i - 1).Item("StartTime").ToString.Trim)
                TempCSLinkTrain.StartStaID = CSFromStaNameToStaIDByStationinf(TempCSLinkTrain.StartStaName, DaTab.Rows(i - 1).Item("STASEQ1").ToString.Trim)
                TempCSLinkTrain.STASEQ1 = DaTab.Rows(i - 1).Item("STASEQ1").ToString.Trim
                TempCSLinkTrain.STASEQ2 = DaTab.Rows(i - 1).Item("STASEQ2").ToString.Trim
                TempCSLinkTrain.EndStaName = DaTab.Rows(i - 1).Item("EndStaName").ToString.Trim
                TempCSLinkTrain.EndTime = CInt(DaTab.Rows(i - 1).Item("EndTime").ToString.Trim)
                TempCSLinkTrain.EndStaID = CSFromStaNameToStaIDByStationinf(TempCSLinkTrain.EndStaName, DaTab.Rows(i - 1).Item("STASEQ2").ToString.Trim)
                TempCSLinkTrain.nCheDiID = CInt(DaTab.Rows(i - 1).Item("CheDiID").ToString.Trim)
                TempCSLinkTrain.nPathID1 = CInt(DaTab.Rows(i - 1).Item("Path1").ToString.Trim)
                TempCSLinkTrain.nPathID2 = CInt(DaTab.Rows(i - 1).Item("Path2").ToString.Trim)
                TempCSLinkTrain.nTrainID = CInt(DaTab.Rows(i - 1).Item("TrainID").ToString.Trim)
                TempCSLinkTrain.UpOrDown = CInt(DaTab.Rows(i - 1).Item("UpOrDown").ToString.Trim)
                TempCSLinkTrain.ZFTime = CInt(DaTab.Rows(i - 1).Item("ZFTime").ToString.Trim)
                TempCSLinkTrain.CulStartTime = AddLitterTime(TempCSLinkTrain.StartTime)
                TempCSLinkTrain.CulEndTime = AddLitterTime(TempCSLinkTrain.EndTime)
                Dim dis As SectionDistance = SectionDistanceList.Find(Function(value As SectionDistance)
                                                                          Return value.StartName = TempCSLinkTrain.StartStaName AndAlso value.EndName = TempCSLinkTrain.EndStaName
                                                                      End Function)
                If dis IsNot Nothing Then
                    TempCSLinkTrain.distance = dis.Distance
                Else
                    TempCSLinkTrain.distance = Val(DaTab.Rows(i - 1).Item("distance").ToString.Trim)
                End If
                TempCSLinkTrain.sCheDiHao = DaTab.Rows(i - 1).Item("VEHICLENO").ToString.Trim
                Call InputCSlinkTrainStaInfo(TempCSLinkTrain)
                If DaTab.Rows(i - 1).Item("DriverNo").ToString = "##" Then
                    TempCSLinkTrain.IsLinked = False
                Else
                    TempCSLinkTrain.IsLinked = True
                End If
                If CInt(DaTab.Rows(i - 1).Item("DateNo").ToString.Trim) > 0 Then
                    Dim merindex As Integer = CInt(DaTab.Rows(i - 1).Item("DateNo").ToString.Trim)
                    TempCSLinkTrain.IsDeadHeading = False
                    ReDim Preserve CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains) + 1)
                    CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains)) = TempCSLinkTrain
                    '==========形成MerTrains
                    If CSTrainsAndDrivers.MergedCSLinkTrains(merindex) Is Nothing Then
                        CSTrainsAndDrivers.MergedCSLinkTrains(merindex) = New MergedCSLinkTrain
                    End If
                    CSTrainsAndDrivers.MergedCSLinkTrains(merindex).AddCSLinkTrain(TempCSLinkTrain)
                Else
                    TempCSLinkTrain.IsDeadHeading = True
                End If

                '赋给驾驶员
                For j = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                    flag = 0
                    If CSTrainsAndDrivers.CSDrivers(j).CSdriverNo = DaTab.Rows(i - 1).Item("DriverNo").ToString Then
                        flag = j
                        Exit For
                    End If
                Next

                If flag > 0 Then
                    ReDim Preserve CSTrainsAndDrivers.CSDrivers(flag).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(flag).CSLinkTrain) + 1)
                    CSTrainsAndDrivers.CSDrivers(flag).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(flag).CSLinkTrain)) = TempCSLinkTrain
                End If
            Next
            CSTrainsAndDrivers.MorningDrivers = New List(Of CSDriver)
            CSTrainsAndDrivers.DayDrivers = New List(Of CSDriver)
            CSTrainsAndDrivers.NightDrivers = New List(Of CSDriver)
            CSTrainsAndDrivers.OtherDrivers = New List(Of CSDriver)
            For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                If dri IsNot Nothing Then
                    Select Case dri.DutySort
                        Case "早班"
                            CSTrainsAndDrivers.MorningDrivers.Add(dri)
                        Case "白班"
                            CSTrainsAndDrivers.DayDrivers.Add(dri)
                        Case "日勤班"
                            CSTrainsAndDrivers.CDayDrivers.Add(dri)
                        Case "夜班"
                            CSTrainsAndDrivers.NightDrivers.Add(dri)
                        Case Else
                            CSTrainsAndDrivers.OtherDrivers.Add(dri)
                    End Select
                End If
            Next

            Call ZhengliMerTrains()
            Call SortCSLinkTrain()
            '=======================赋值车站
            For Each train As CSLinkTrain In CSTrainsAndDrivers.CSLinkTrains
                If train IsNot Nothing Then
                    For Each chedi As typeCSCheDi In CSTrainsAndDrivers.CSChedi
                        If chedi IsNot Nothing AndAlso chedi.CSCheDiId = train.nCheDiID Then
                            For Each sta As typeCrewStation In chedi.CrewSta
                                If sta IsNot Nothing Then
                                    If sta.CrewStaName = train.StartStaName AndAlso sta.CheCi = train.OutputCheCi And sta.DepartTime = train.StartTime Then
                                        train.FirstStation = sta
                                    End If
                                    If sta.CrewStaName = train.EndStaName AndAlso sta.CheCi = train.OffCheCi And sta.ArriveTime = train.EndTime Then
                                        train.SecondStation = sta
                                        train.OffUpOrDown = sta.FlagUpDown
                                    End If
                                End If
                            Next
                            chedi.CSLinkTrains.Add(train)
                            Exit For
                        End If
                    Next
                End If
            Next

            Dim tableName As List(Of String) = Globle.Method.GetAllTableNamefromAccess
            If tableName.Contains("CS_SPECIALDUTYWORK") = False Then
                Str = "create table CS_SPECIALDUTYWORK(LINEID char(30),CSTIMETABLEID char(100),DUTYSORT char(50),DUTYTYPE char(30),DUTYWORK char(100))"
                Globle.Method.UpdateDataForAccess(Str)
            End If
            Str = "select * from CS_SPECIALDUTYWORK where lineid='" & CurLineName & "' and cstimetableid='" & CStr(strQCurCSPlanID) & "'"
            DaTab = New DataTable
            DaTab = Globle.Method.ReadDataForAccess(Str)
            If IsNothing(DaTab) = False AndAlso DaTab.Rows.Count > 0 Then
                For z As Integer = 0 To DaTab.Rows.Count - 1
                    If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
                        For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                            If dri IsNot Nothing Then
                                If dri.CSdriverNo = DaTab.Rows(z).Item("dutysort").ToString.Trim And dri.DutySort = DaTab.Rows(z).Item("DUTYTYPE").ToString.Trim Then
                                    dri.dutyWork = DaTab.Rows(z).Item("dutywork").ToString.Trim
                                    Exit For
                                End If
                            End If
                            
                        Next
                    End If
                Next
            End If


            If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
                For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If dri IsNot Nothing Then
                        For Each train As CSLinkTrain In dri.CSLinkTrain
                            If train IsNot Nothing AndAlso (train.FirstStation Is Nothing OrElse train.SecondStation Is Nothing) Then
                                For Each chedi As typeCSCheDi In CSTrainsAndDrivers.CSChedi
                                    If chedi IsNot Nothing AndAlso chedi.CSCheDiId = train.nCheDiID Then
                                        For Each sta As typeCrewStation In chedi.CrewSta
                                            If sta IsNot Nothing Then
                                                If sta.CrewStaName = train.StartStaName AndAlso sta.CheCi = train.OutputCheCi Then
                                                    train.FirstStation = sta
                                                End If
                                                If sta.CrewStaName = train.EndStaName AndAlso sta.CheCi = train.OffCheCi Then
                                                    train.SecondStation = sta
                                                    train.OffUpOrDown = sta.FlagUpDown
                                                End If
                                            End If
                                        Next
                                        chedi.CSLinkTrains.Add(train)
                                        Exit For
                                    End If
                                Next
                            End If
                        Next
                    End If
                Next
            End If
        End If
    End Sub

    Public Sub InputCSlinkTrainStaInfo(ByVal Train As CSLinkTrain)
        Dim sChedi As typeCSCheDi = Nothing
        For Each chedi As typeCSCheDi In CSTrainsAndDrivers.CSChedi
            If chedi IsNot Nothing AndAlso chedi.CSCheDiId = Train.nCheDiID Then
                sChedi = chedi
                Exit For
            End If
        Next
        If sChedi IsNot Nothing Then
            For Each sta As typeCrewStation In sChedi.CrewSta
                If sta IsNot Nothing AndAlso sta.CrewStaName = Train.StartStaName AndAlso sta.DepartTime = Train.StartTime Then
                    Train.FirstStation = sta
                    If sta.IsBeiche <> 0 Then
                        Train.isBeiChe = sta.IsBeiche
                    End If
                    Exit For
                End If
            Next
            For Each sta As typeCrewStation In sChedi.CrewSta
                If sta IsNot Nothing AndAlso sta.CrewStaName = Train.EndStaName AndAlso sta.ArriveTime = Train.EndTime Then
                    Train.SecondStation = sta
                    Exit For
                End If
            Next
        End If
    End Sub

    Public Sub DeleteNullDriverAndCodeDriver()
        Dim i As Integer
        If CSTrainsAndDrivers.CSDrivers Is Nothing Or UBound(CSTrainsAndDrivers.CSDrivers) = 0 Then
            Exit Sub
        End If
        Dim tempCSDriver() As CSDriver
        ReDim tempCSDriver(0)
        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain Is Nothing = False AndAlso UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain) >= 1 Then
                ReDim Preserve tempCSDriver(UBound(tempCSDriver) + 1)
                tempCSDriver(UBound(tempCSDriver)) = CSTrainsAndDrivers.CSDrivers(i)
            End If
        Next
        ReDim CSTrainsAndDrivers.CSDrivers(0)
        CSTrainsAndDrivers.CSDrivers = tempCSDriver

        ''不再重新编号
        'For i = 1 To UBound(CSDrivers)
        '    If CSDrivers(i).CSdriverNo <> "#" Then
        '        CSDrivers(i).CSdriverNo = CStr(Format(CInt(i), "000"))
        '    End If
        'Next
    End Sub



    Public Sub CorPreParedDrivers(ByVal PreDrivers As List(Of CSDriver))           '进行备班备车的夜早班搭配
        Dim MorPreDrivers As New List(Of CSDriver)
        Dim NitPreDrivers As New List(Of CSDriver)
        For Each dri As CSDriver In PreDrivers
            If dri.DutySort = "早班" Then
                MorPreDrivers.Add(dri)
            ElseIf dri.DutySort = "夜班" Then
                NitPreDrivers.Add(dri)
            End If
        Next
        For Each dri As CSDriver In MorPreDrivers
            Dim MorDri As CSDriver = dri
            Dim tempdri As CSDriver = NitPreDrivers.Find(Function(value As CSDriver)
                                                             Return (value.CurStationName = MorDri.CurStationName AndAlso value.BelongArea = MorDri.BelongArea)
                                                         End Function)
            If tempdri IsNot Nothing Then
                MorDri.LinkedNightDriver = tempdri
                tempdri.LinkedMorDriver = MorDri
                NitPreDrivers.Remove(tempdri)
            End If
        Next
    End Sub

    Public Sub GetLinkedMorDriver(ByVal NightDriver As CSDriver)
        If NightDriver.CSLinkTrain(UBound(NightDriver.CSLinkTrain)).SecondStation.IsYard Then
            Dim LateIndex As Integer = 1
            Dim MorningIndex As Integer = 1
            For Each dri As CSDriver In CSTrainsAndDrivers.NightDrivers
                If dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName = NightDriver.CSLinkTrain(UBound(NightDriver.CSLinkTrain)).EndStaName Then
                    LateIndex += 1
                End If
            Next
            For i As Integer = CSTrainsAndDrivers.MorningDrivers.Count - 1 To 0 Step -1
                If CSTrainsAndDrivers.MorningDrivers(i).CSLinkTrain(1).StartStaName = NightDriver.CSLinkTrain(UBound(NightDriver.CSLinkTrain)).EndStaName Then
                    If MorningIndex = LateIndex Then
                        NightDriver.LinkedMorDriver = CSTrainsAndDrivers.MorningDrivers(i)
                        CSTrainsAndDrivers.MorningDrivers(i).LinkedNightDriver = NightDriver
                        Exit For
                    End If
                    MorningIndex += 1
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' 将一些必须连在一起的任务段结合起来
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub FormMergeCSLinktrain()
        ReDim CSTrainsAndDrivers.MergedCSLinkTrains(0)
        If UBound(CSTrainsAndDrivers.CSChedi) > 0 Then
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSChedi)
                If CSTrainsAndDrivers.CSChedi(i).CSLinkTrains.Count >= 0 Then
                    Dim tempMertrain As New MergedCSLinkTrain

                    For j As Integer = 0 To CSTrainsAndDrivers.CSChedi(i).CSLinkTrains.Count - 1
                        Dim curtrain As CSLinkTrain = CSTrainsAndDrivers.CSChedi(i).CSLinkTrains(j)
                        tempMertrain.AddCSLinkTrain(curtrain)
                        If curtrain.isBeiChe <> 0 Then
                            tempMertrain.beiche = curtrain.isBeiChe
                        End If
                        If curtrain.IsDeadHeading = False Then
                            If IsShiftPlace(curtrain.EndStaName, curtrain.RoutingName, curtrain.EndTime) OrElse IsTransitStationPlace(curtrain.EndStaName, curtrain.RoutingName, curtrain.UpOrDown, curtrain.CulEndTime) Then
                                ''===========出库小段处理
                                If UBound(CSTrainsAndDrivers.MergedCSLinkTrains) > 0 AndAlso UBound(CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains)).CSLinkTrains) = 1 AndAlso CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains)).CSLinkTrains(1).FirstStation.IsYard = True AndAlso CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains)).CSLinkTrains(1).distance <= minChuDis Then
                                    CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains)).AddCSLinkTrain(curtrain)
                                    If curtrain.isBeiChe <> 0 Then
                                        CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains)).beiche = curtrain.isBeiChe
                                    End If
                                    tempMertrain = New MergedCSLinkTrain
                                    Continue For
                                End If
                                ''===========入库小段处理
                                If curtrain.SecondStation.IsYard = True AndAlso curtrain.distance <= minRuDis AndAlso UBound(tempMertrain.CSLinkTrains) = 1 AndAlso curtrain.UpOrDown = CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains)).CSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains)).CSLinkTrains)).UpOrDown Then
                                    CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains)).AddCSLinkTrain(curtrain)
                                    tempMertrain = New MergedCSLinkTrain
                                    Continue For
                                End If
                                '===========普通任务段在轮换点和上下班地点将被断开
                                ReDim Preserve CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains) + 1)
                                CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains)) = tempMertrain
                                tempMertrain = New MergedCSLinkTrain
                            End If
                        Else
                            tempMertrain = New MergedCSLinkTrain
                            Exit For
                        End If
                    Next
                    If tempMertrain IsNot Nothing AndAlso UBound(tempMertrain.CSLinkTrains) > 0 Then
                        ReDim Preserve CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains) + 1)
                        CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains)) = tempMertrain
                    End If
                End If
            Next
        End If
        '从小到大排序
        If UBound(CSTrainsAndDrivers.MergedCSLinkTrains) > 0 Then
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                For j As Integer = i To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                    If CSTrainsAndDrivers.MergedCSLinkTrains(i).CulStartTime > CSTrainsAndDrivers.MergedCSLinkTrains(j).CulStartTime Then
                        Dim tmp As MergedCSLinkTrain = CSTrainsAndDrivers.MergedCSLinkTrains(i)
                        CSTrainsAndDrivers.MergedCSLinkTrains(i) = CSTrainsAndDrivers.MergedCSLinkTrains(j)
                        CSTrainsAndDrivers.MergedCSLinkTrains(j) = tmp
                    End If
                Next
            Next
        End If
    End Sub

    Public Function GetDayDriDeadHead(ByVal train As CSLinkTrain) As CSLinkTrain         '安排早班出库司机
        GetDayDriDeadHead = Nothing
        Dim tempCSLinkTrain As CSLinkTrain = Nothing
        Dim staname As String = train.StartStaName
        If UBound(CSchediInfo) > 1 Then
            Dim MinTime As Integer = 86400
            Dim nChediID As Integer = -1           '车底ID
            Dim nTrainID As Integer = -1          '列车ID
            Dim nPathID As Integer = -1           '车站ID
            Dim nPathIndex As Integer = -1
            Dim nSelectChediID As Integer = -1           '车底ID
            Dim nSelectTrainID As Integer = -1          '列车ID
            Dim nSelectPathID As Integer = -1           '车站ID
            Dim nSelectPathIndex As Integer = -1
            Dim DHTrains As New List(Of DHTrainInfo)
            For i As Integer = 1 To UBound(CSchediInfo)
                nChediID = i
                For j As Integer = 1 To UBound(CSchediInfo(i).nLinkTrain)
                    nTrainID = CSchediInfo(i).nLinkTrain(j)
                    For k As Integer = 1 To UBound(CSTrainInf(nTrainID).nPathID)
                        nPathID = CSTrainInf(nTrainID).nPathID(k)
                        nPathIndex = k
                        If StationInf(nPathID).sStationName = staname AndAlso k > 1 Then
                            If ((nTrainID Mod 2) = train.UpOrDown AndAlso IIf(CSTrainInf(nTrainID).Arrival(nPathID) < 10800, CSTrainInf(nTrainID).Arrival(nPathID) + 86400, CSTrainInf(nTrainID).Arrival(nPathID)) < train.CulStartTime) _
                                OrElse ((nTrainID Mod 2) <> train.UpOrDown AndAlso IIf(CSTrainInf(nTrainID).Arrival(nPathID) < 10800, CSTrainInf(nTrainID).Arrival(nPathID) + 86400, CSTrainInf(nTrainID).Arrival(nPathID)) + ChangePlaceRestTime(staname, CSTrainInf(nTrainID).sJiaoLuName, train.UpOrDown, IIf(CSTrainInf(nTrainID).Arrival(nPathID) < 10800, CSTrainInf(nTrainID).Arrival(nPathID) + 86400, CSTrainInf(nTrainID).Arrival(nPathID))) <= train.CulStartTime) Then
                                Dim waittime As Integer = train.CulStartTime - IIf(CSTrainInf(nTrainID).Arrival(nPathID) < 10800, CSTrainInf(nTrainID).Arrival(nPathID) + 86400, CSTrainInf(nTrainID).Arrival(nPathID))
                                Dim temDH As New DHTrainInfo
                                temDH.WaitTime = waittime
                                temDH.nChediID = nChediID
                                temDH.nTrainID = nTrainID
                                temDH.nPathID = nPathID
                                temDH.nPathIndex = nPathIndex
                                DHTrains.Add(temDH)
                            End If
                        End If
                    Next
                Next
            Next

            Call SortDHTrainByWaittime(DHTrains)
            If DHTrains.Count > 1 Then
                nSelectChediID = DHTrains(1).nChediID
                nSelectTrainID = DHTrains(1).nTrainID
                nSelectPathID = DHTrains(1).nPathID
                nSelectPathIndex = DHTrains(1).nPathIndex
            ElseIf DHTrains.Count > 0 Then
                nSelectChediID = DHTrains(0).nChediID
                nSelectTrainID = DHTrains(0).nTrainID
                nSelectPathID = DHTrains(0).nPathID
                nSelectPathIndex = DHTrains(0).nPathIndex
            End If

            If nSelectChediID > 0 AndAlso nSelectTrainID > 0 AndAlso nSelectPathID > 0 Then
                Dim ifstart As Boolean = False
                For j As Integer = UBound(CSTrainInf(nSelectTrainID).nPathID) To 1 Step -1
                    If j < nSelectPathIndex AndAlso IsShiftPlace(StationInf(CSTrainInf(nSelectTrainID).nPathID(j)).sStationName, CSTrainInf(nSelectTrainID).sJiaoLuName, CSTrainInf(nSelectTrainID).Arrival(CSTrainInf(nSelectTrainID).nPathID(j))) Then
                        Dim FirCrewSta As New typeCrewStation(CSTrainInf(nSelectTrainID), nSelectTrainID, j)
                        Dim SecCrewSta As New typeCrewStation(CSTrainInf(nSelectTrainID), nSelectTrainID, nSelectPathIndex)
                        tempCSLinkTrain = New CSLinkTrain(FirCrewSta, SecCrewSta, "", CSTrainsAndDrivers.CSChedi(nSelectChediID))
                        tempCSLinkTrain.IsDeadHeading = True
                        tempCSLinkTrain.CSTrainID = -1
                        Exit For
                    End If
                Next
            End If
        End If
        GetDayDriDeadHead = tempCSLinkTrain
    End Function

    Public Function GetAllDeadHead(ByVal train As CSLinkTrain, ByVal ObjSta As String) As CSLinkTrain       '  逆向安排随乘司机
        GetAllDeadHead = Nothing
        Dim tempCSLinkTrain As CSLinkTrain = Nothing
        Dim staname As String = train.StartStaName
        If UBound(CSchediInfo) > 1 Then
            Dim MinTime As Integer = 86400
            Dim nChediID As Integer = -1           '车底ID
            Dim nTrainID As Integer = -1          '列车ID
            Dim nPathID As Integer = -1           '车站ID
            Dim nPathIndex As Integer = -1
            Dim nSelectChediID As Integer = -1           '车底ID
            Dim nSelectTrainID As Integer = -1          '列车ID
            Dim nSelectPathID As Integer = -1           '车站ID
            Dim nSelectPathIndex As Integer = -1
            Dim DHTrains As New List(Of DHTrainInfo)
            For i As Integer = 1 To UBound(CSchediInfo)
                nChediID = i
                For j As Integer = 1 To UBound(CSchediInfo(i).nLinkTrain)
                    nTrainID = CSchediInfo(i).nLinkTrain(j)
                    For k As Integer = 1 To UBound(CSTrainInf(nTrainID).nPathID)
                        nPathID = CSTrainInf(nTrainID).nPathID(k)
                        nPathIndex = k
                        If StationInf(nPathID).sStationName = staname AndAlso k > 1 Then
                            If ((nTrainID Mod 2) = train.UpOrDown AndAlso IIf(CSTrainInf(nTrainID).Arrival(nPathID) < 10800, CSTrainInf(nTrainID).Arrival(nPathID) + 86400, CSTrainInf(nTrainID).Arrival(nPathID)) < train.CulStartTime) _
                                OrElse ((nTrainID Mod 2) <> train.UpOrDown AndAlso IIf(CSTrainInf(nTrainID).Arrival(nPathID) < 10800, CSTrainInf(nTrainID).Arrival(nPathID) + 86400, CSTrainInf(nTrainID).Arrival(nPathID)) + ChangePlaceRestTime(staname, CSTrainInf(nTrainID).sJiaoLuName, train.UpOrDown, IIf(CSTrainInf(nTrainID).Arrival(nPathID) < 10800, CSTrainInf(nTrainID).Arrival(nPathID) + 86400, CSTrainInf(nTrainID).Arrival(nPathID))) <= train.CulStartTime) Then
                                Dim waittime As Integer = train.CulStartTime - IIf(CSTrainInf(nTrainID).Arrival(nPathID) < 10800, CSTrainInf(nTrainID).Arrival(nPathID) + 86400, CSTrainInf(nTrainID).Arrival(nPathID))
                                Dim temDH As New DHTrainInfo
                                temDH.WaitTime = waittime
                                temDH.nChediID = nChediID
                                temDH.nTrainID = nTrainID
                                temDH.nPathID = nPathID
                                temDH.nPathIndex = nPathIndex
                                DHTrains.Add(temDH)
                            End If
                        End If
                    Next
                Next
            Next

            Call SortDHTrainByWaittime(DHTrains)
            If DHTrains.Count > 1 Then
                nSelectChediID = DHTrains(1).nChediID
                nSelectTrainID = DHTrains(1).nTrainID
                nSelectPathID = DHTrains(1).nPathID
                nSelectPathIndex = DHTrains(1).nPathIndex
            ElseIf DHTrains.Count > 0 Then
                nSelectChediID = DHTrains(0).nChediID
                nSelectTrainID = DHTrains(0).nTrainID
                nSelectPathID = DHTrains(0).nPathID
                nSelectPathIndex = DHTrains(0).nPathIndex
            End If

            If nSelectChediID > 0 AndAlso nSelectTrainID > 0 AndAlso nSelectPathID > 0 Then
                Dim ifstart As Boolean = False
                For j As Integer = UBound(CSTrainInf(nSelectTrainID).nPathID) To 1 Step -1
                    If j < nSelectPathIndex AndAlso StationInf(CSTrainInf(nSelectTrainID).nPathID(j)).sStationName = ObjSta Then
                        Dim FirCrewSta As New typeCrewStation(CSTrainInf(nSelectTrainID), nSelectTrainID, j)
                        Dim SecCrewSta As New typeCrewStation(CSTrainInf(nSelectTrainID), nSelectTrainID, nSelectPathIndex)
                        tempCSLinkTrain = New CSLinkTrain(FirCrewSta, SecCrewSta, "", CSTrainsAndDrivers.CSChedi(nSelectChediID))
                        tempCSLinkTrain.IsDeadHeading = True
                        tempCSLinkTrain.CSTrainID = nSelectTrainID
                        Exit For
                    End If
                Next
            End If
        End If
        GetAllDeadHead = tempCSLinkTrain

    End Function

    Public Function GetDayDriDeadHeadTo(ByVal train As CSLinkTrain) As CSLinkTrain         '安排早班出库司机
        GetDayDriDeadHeadTo = Nothing
        Dim tempCSLinkTrain As CSLinkTrain = Nothing
        Dim staname As String = train.EndStaName
        If UBound(CSchediInfo) > 1 Then
            Dim MinTime As Integer = 86400
            Dim nChediID As Integer = -1           '车底ID
            Dim nTrainID As Integer = -1          '列车ID
            Dim nPathID As Integer = -1           '车站ID
            Dim nPathIndex As Integer = -1
            Dim nSelectChediID As Integer = -1           '车底ID
            Dim nSelectTrainID As Integer = -1          '列车ID
            Dim nSelectPathID As Integer = -1           '车站ID
            Dim nSelectPathIndex As Integer = -1
            Dim DHTrains As New List(Of DHTrainInfo)
            For i As Integer = 1 To UBound(CSchediInfo)
                nChediID = i
                For j As Integer = 1 To UBound(CSchediInfo(i).nLinkTrain)
                    nTrainID = CSchediInfo(i).nLinkTrain(j)
                    For k As Integer = 1 To UBound(CSTrainInf(nTrainID).nPathID)
                        nPathID = CSTrainInf(nTrainID).nPathID(k)
                        nPathIndex = k
                        If StationInf(nPathID).sStationName = staname AndAlso k < UBound(CSTrainInf(nTrainID).nPathID) Then
                            If ((nTrainID Mod 2) = train.UpOrDown AndAlso IIf(CSTrainInf(nTrainID).Starting(nPathID) < 10800, CSTrainInf(nTrainID).Starting(nPathID) + 86400, CSTrainInf(nTrainID).Starting(nPathID)) > train.CulEndTime) _
                                OrElse ((nTrainID Mod 2) <> train.UpOrDown AndAlso IIf(CSTrainInf(nTrainID).Starting(nPathID) < 10800, CSTrainInf(nTrainID).Starting(nPathID) + 86400, CSTrainInf(nTrainID).Starting(nPathID)) >= train.CulEndTime + ChangePlaceRestTime(staname, train.RoutingName, train.UpOrDown, train.CulEndTime)) Then
                                Dim waittime As Integer = IIf(CSTrainInf(nTrainID).Starting(nPathID) < 10800, CSTrainInf(nTrainID).Starting(nPathID) + 86400, CSTrainInf(nTrainID).Starting(nPathID)) - train.CulStartTime
                                Dim temDH As New DHTrainInfo
                                temDH.WaitTime = waittime
                                temDH.nChediID = nChediID
                                temDH.nTrainID = nTrainID
                                temDH.nPathID = nPathID
                                temDH.nPathIndex = nPathIndex
                                DHTrains.Add(temDH)
                            End If
                        End If
                    Next
                Next
            Next

            Call SortDHTrainByWaittime(DHTrains)
            If DHTrains.Count > 1 Then
                nSelectChediID = DHTrains(1).nChediID
                nSelectTrainID = DHTrains(1).nTrainID
                nSelectPathID = DHTrains(1).nPathID
                nSelectPathIndex = DHTrains(1).nPathIndex
            ElseIf DHTrains.Count > 0 Then
                nSelectChediID = DHTrains(0).nChediID
                nSelectTrainID = DHTrains(0).nTrainID
                nSelectPathID = DHTrains(0).nPathID
                nSelectPathIndex = DHTrains(0).nPathIndex
            End If

            If nSelectChediID > 0 AndAlso nSelectTrainID > 0 AndAlso nSelectPathID > 0 Then
                Dim ifstart As Boolean = False
                For j As Integer = 1 To UBound(CSTrainInf(nSelectTrainID).nPathID)
                    If j > nSelectPathIndex AndAlso IsShiftPlace(StationInf(CSTrainInf(nSelectTrainID).nPathID(j)).sStationName, CSTrainInf(nSelectTrainID).sJiaoLuName, CSTrainInf(nSelectTrainID).Arrival(CSTrainInf(nSelectTrainID).nPathID(j))) Then
                        Dim FirCrewSta As New typeCrewStation(CSTrainInf(nSelectTrainID), nSelectTrainID, nSelectPathIndex)
                        Dim SecCrewSta As New typeCrewStation(CSTrainInf(nSelectTrainID), nSelectTrainID, j)
                        tempCSLinkTrain = New CSLinkTrain(FirCrewSta, SecCrewSta, "", CSTrainsAndDrivers.CSChedi(nSelectChediID))
                        tempCSLinkTrain.IsDeadHeading = True
                        tempCSLinkTrain.CSTrainID = -1
                        Exit For
                    End If
                Next
            End If
        End If
        GetDayDriDeadHeadTo = tempCSLinkTrain

    End Function

    Structure DHTrainInfo
        Dim nChediID As Integer         '车底ID
        Dim nTrainID As Integer         '列车ID
        Dim nPathID As Integer          '车站ID
        Dim nPathIndex As Integer
        Dim WaitTime As Integer    '等待时间
    End Structure

    Public Sub SortDHTrainByWaittime(ByVal DHTrains As List(Of DHTrainInfo))
        If DHTrains.Count > 1 Then
            For i As Integer = 0 To DHTrains.Count - 2
                For j As Integer = i + 1 To DHTrains.Count - 1
                    If DHTrains(j).WaitTime < DHTrains(i).WaitTime Then
                        Dim temDH As DHTrainInfo
                        temDH = DHTrains(j)
                        DHTrains(j) = DHTrains(i)
                        DHTrains(i) = temDH
                    End If
                Next
            Next
        End If
    End Sub

    Public Function GetDeadHead(ByVal train As CSLinkTrain, Optional ByVal DepotName As String = "") As MergedCSLinkTrain        '安排早班出库司机
        GetDeadHead = Nothing
        Dim tempMergeTrain As MergedCSLinkTrain = Nothing
        Dim staname As String = train.StartStaName
        If UBound(CSchediInfo) > 1 Then
            Dim WaitTime As Integer = 0
            Dim nChediID As Integer = -1           '车底ID
            Dim nTrainID As Integer = -1          '列车ID
            Dim nPathID As Integer = -1           '车站ID
            Dim nPathIndex As Integer = -1
            Dim nSelectChediID As Integer         '车底ID
            Dim nSelectTrainID As Integer         '列车ID
            Dim nSelectPathID As Integer          '车站ID
            Dim nSelectPathIndex As Integer
            Dim DHTrains As New List(Of DHTrainInfo)
            Dim nChediIDList1(0) As Integer
            Dim n As Integer = 1
            For i As Integer = 1 To UBound(CSchediInfo)
                If DepotName <> "" Then
                    If CSchediInfo(i).sChuKuSta <> DepotName Then
                        Continue For
                    End If
                    ReDim Preserve nChediIDList1(n)
                    nChediIDList1(n) = i
                    n += 1
                End If
            Next

            '添加20151015
            frmInputBox.Text = "安排随乘出勤列车"
            frmInputBox.labTitle.Text = "出勤车次号:"
            frmInputBox.cmbText.Visible = True
            frmInputBox.txtText.Visible = False
            frmInputBox.cmbText.Items.Clear()
            For i As Integer = 1 To nChediIDList1.Count - 1
                frmInputBox.cmbText.Items.Add(CSchediInfo(nChediIDList1(i)).sCheCiHao)
            Next
            If frmInputBox.cmbText.Items.Count > 0 Then
                frmInputBox.cmbText.SelectedIndex = 0
            End If
            If frmInputBox.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                For i As Integer = 1 To UBound(CSchediInfo)
                    If CSchediInfo(i).sCheCiHao = StrInputBoxCombText Then
                        nChediID = i
                    End If
                Next
            End If
            If nChediID = -1 Then
                Exit Function
            End If
            For j As Integer = UBound(CSchediInfo(nChediID).nLinkTrain) To 1 Step -1
                nTrainID = CSchediInfo(nChediID).nLinkTrain(j)
                For k As Integer = 1 To UBound(CSTrainInf(nTrainID).nPathID)
                    nPathID = CSTrainInf(nTrainID).nPathID(k)
                    nPathIndex = k
                    If StationInf(nPathID).sStationName = staname Then
                        If ((nTrainID Mod 2) = train.UpOrDown AndAlso IIf(CSTrainInf(nTrainID).Arrival(nPathID) < 10800, CSTrainInf(nTrainID).Arrival(nPathID) + 86400, CSTrainInf(nTrainID).Arrival(nPathID)) <= train.CulStartTime) _
                            OrElse ((nTrainID Mod 2) <> train.UpOrDown AndAlso IIf(CSTrainInf(nTrainID).Arrival(nPathID) < 10800, CSTrainInf(nTrainID).Arrival(nPathID) + 86400, CSTrainInf(nTrainID).Arrival(nPathID)) + ChangePlaceRestTime(staname, CSTrainInf(nTrainID).sJiaoLuName, train.UpOrDown, IIf(CSTrainInf(nTrainID).Arrival(nPathID) < 10800, CSTrainInf(nTrainID).Arrival(nPathID) + 86400, CSTrainInf(nTrainID).Arrival(nPathID))) <= train.CulStartTime) Then
                            Dim temDH As New DHTrainInfo
                            temDH.WaitTime = train.StartTime - CSchediInfo(nChediID).ChukuTime
                            temDH.nChediID = nChediID
                            temDH.nTrainID = nTrainID
                            temDH.nPathID = nPathID
                            temDH.nPathIndex = nPathIndex
                            DHTrains.Add(temDH)
                            GoTo L
                        End If
                    End If
                Next
L:
            Next

            Call SortDHTrainByWaittime(DHTrains)
            If DHTrains.Count > 1 Then
                nSelectChediID = DHTrains(1).nChediID
                nSelectTrainID = DHTrains(1).nTrainID
                nSelectPathID = DHTrains(1).nPathID
                nSelectPathIndex = DHTrains(1).nPathIndex
            ElseIf DHTrains.Count > 0 Then
                nSelectChediID = DHTrains(0).nChediID
                nSelectTrainID = DHTrains(0).nTrainID
                nSelectPathID = DHTrains(0).nPathID
                nSelectPathIndex = DHTrains(0).nPathIndex
            End If


            If nSelectChediID > 0 AndAlso nSelectTrainID > 0 AndAlso nSelectPathID > 0 Then
                tempMergeTrain = New MergedCSLinkTrain
                For i As Integer = 1 To UBound(CSchediInfo(nSelectChediID).nLinkTrain)
                    If CSchediInfo(nSelectChediID).nLinkTrain(i) <> nSelectTrainID Then
                        Dim FirCrewSta As New typeCrewStation(CSTrainInf(CSchediInfo(nSelectChediID).nLinkTrain(i)), CSchediInfo(nSelectChediID).nLinkTrain(i), 1)
                        Dim SecCrewSta As New typeCrewStation(CSTrainInf(CSchediInfo(nSelectChediID).nLinkTrain(i)), CSchediInfo(nSelectChediID).nLinkTrain(i), UBound(CSTrainInf(CSchediInfo(nSelectChediID).nLinkTrain(i)).nPathID))
                        Dim tempCSlinkTrain As New CSLinkTrain(FirCrewSta, SecCrewSta, "", CSTrainsAndDrivers.CSChedi(nSelectChediID))
                        tempCSlinkTrain.IsDeadHeading = True
                        tempCSlinkTrain.CSTrainID = CSchediInfo(nSelectChediID).nLinkTrain(i)
                        tempMergeTrain.AddCSLinkTrain(tempCSlinkTrain)
                    Else
                        Dim FirCrewSta As New typeCrewStation(CSTrainInf(nSelectTrainID), nSelectTrainID, 1)
                        Dim SecCrewSta As New typeCrewStation(CSTrainInf(nSelectTrainID), nSelectTrainID, nSelectPathIndex)
                        If FirCrewSta.CrewStaName <> SecCrewSta.CrewStaName Then
                            Dim tempCSlinkTrain As New CSLinkTrain(FirCrewSta, SecCrewSta, "", CSTrainsAndDrivers.CSChedi(nSelectChediID))
                            tempCSlinkTrain.IsDeadHeading = True
                            tempCSlinkTrain.CSTrainID = nSelectTrainID
                            tempMergeTrain.AddCSLinkTrain(tempCSlinkTrain)
                        End If
                        Exit For
                    End If
                Next
            End If
        End If
        GetDeadHead = tempMergeTrain

    End Function

    Public Function GetDeadHeadTo(ByVal train As CSLinkTrain, Optional ByVal DepotName As String = "") As MergedCSLinkTrain      '安排退勤司机

        GetDeadHeadTo = Nothing
        Dim tempMergeTrain As MergedCSLinkTrain = Nothing
        Dim staname As String = train.EndStaName
        If UBound(CSchediInfo) > 1 Then
            Dim MinTime As Integer = 86400
            Dim nChediID As Integer = -1           '车底ID
            Dim nTrainID As Integer = -1          '列车ID
            Dim nPathID As Integer = -1           '车站ID
            Dim nPathIndex As Integer = -1
            Dim nSelectChediID As Integer = -1           '车底ID
            Dim nSelectTrainID As Integer = -1          '列车ID
            Dim nSelectPathID As Integer = -1           '车站ID
            Dim nSelectPathIndex As Integer = -1
            Dim DHTrains As New List(Of DHTrainInfo)
            For i As Integer = 1 To UBound(CSchediInfo)
                If DepotName <> "" Then
                    If CSchediInfo(i).sRuKuSta <> DepotName Then
                        Continue For
                    End If
                End If
                nChediID = i
                For j As Integer = UBound(CSchediInfo(i).nLinkTrain) To 1 Step -1
                    nTrainID = CSchediInfo(i).nLinkTrain(j)
                    For k As Integer = 1 To UBound(CSTrainInf(nTrainID).nPathID)
                        nPathID = CSTrainInf(nTrainID).nPathID(k)
                        nPathIndex = k
                        If StationInf(nPathID).sStationName = staname Then
                            If ((nTrainID Mod 2) = train.UpOrDown AndAlso IIf(CSTrainInf(nTrainID).Starting(nPathID) < 10800, CSTrainInf(nTrainID).Starting(nPathID) + 86400, CSTrainInf(nTrainID).Starting(nPathID)) >= train.CulEndTime) _
                                OrElse ((nTrainID Mod 2) <> train.UpOrDown AndAlso IIf(CSTrainInf(nTrainID).Starting(nPathID) < 10800, CSTrainInf(nTrainID).Starting(nPathID) + 86400, CSTrainInf(nTrainID).Starting(nPathID)) >= train.CulEndTime + ChangePlaceRestTime(train.EndStaName, train.RoutingName, train.UpOrDown, train.CulEndTime)) Then
                                Dim temDH As New DHTrainInfo
                                temDH.WaitTime = IIf(CSchediInfo(nChediID).RukuTime < 10800, CSchediInfo(nChediID).RukuTime + 86400, CSchediInfo(nChediID).RukuTime) - train.CulEndTime
                                temDH.nChediID = nChediID
                                temDH.nTrainID = nTrainID
                                temDH.nPathID = nPathID
                                temDH.nPathIndex = nPathIndex
                                DHTrains.Add(temDH)
                                GoTo L
                            End If
                        End If
                    Next
                Next
L:
            Next

            Call SortDHTrainByWaittime(DHTrains)
            If DHTrains.Count > 0 Then
                nSelectChediID = DHTrains(0).nChediID
                nSelectTrainID = DHTrains(0).nTrainID
                nSelectPathID = DHTrains(0).nPathID
                nSelectPathIndex = DHTrains(0).nPathIndex
            End If

            If nSelectChediID > 0 AndAlso nSelectTrainID > 0 AndAlso nSelectPathID > 0 Then
                tempMergeTrain = New MergedCSLinkTrain
                Dim StartOrNot As Boolean = False
                For i As Integer = 1 To UBound(CSchediInfo(nSelectChediID).nLinkTrain)
                    If CSchediInfo(nSelectChediID).nLinkTrain(i) <> nSelectTrainID Then
                        If StartOrNot = True Then
                            Dim FirCrewSta As New typeCrewStation(CSTrainInf(CSchediInfo(nSelectChediID).nLinkTrain(i)), CSchediInfo(nSelectChediID).nLinkTrain(i), 1)
                            Dim SecCrewSta As New typeCrewStation(CSTrainInf(CSchediInfo(nSelectChediID).nLinkTrain(i)), CSchediInfo(nSelectChediID).nLinkTrain(i), UBound(CSTrainInf(CSchediInfo(nSelectChediID).nLinkTrain(i)).nPathID))
                            Dim tempCSlinkTrain As New CSLinkTrain(FirCrewSta, SecCrewSta, "", CSTrainsAndDrivers.CSChedi(nSelectChediID))
                            tempCSlinkTrain.IsDeadHeading = True
                            tempCSlinkTrain.CSTrainID = nSelectTrainID
                            tempMergeTrain.AddCSLinkTrain(tempCSlinkTrain)
                        End If
                    Else
                        Dim FirCrewSta As New typeCrewStation(CSTrainInf(nSelectTrainID), nSelectTrainID, nSelectPathIndex)
                        Dim SecCrewSta As New typeCrewStation(CSTrainInf(CSchediInfo(nSelectChediID).nLinkTrain(i)), CSchediInfo(nSelectChediID).nLinkTrain(i), UBound(CSTrainInf(CSchediInfo(nSelectChediID).nLinkTrain(i)).nPathID))
                        If FirCrewSta.CrewStaName <> SecCrewSta.CrewStaName Then               '折返现象
                            Dim tempCSlinkTrain As New CSLinkTrain(FirCrewSta, SecCrewSta, "", CSTrainsAndDrivers.CSChedi(nSelectChediID))
                            tempCSlinkTrain.IsDeadHeading = True
                            tempCSlinkTrain.CSTrainID = nSelectTrainID
                            tempMergeTrain.AddCSLinkTrain(tempCSlinkTrain)
                        End If
                        StartOrNot = True
                        'Exit For 
                    End If
                Next
            End If
        End If
        GetDeadHeadTo = tempMergeTrain
    End Function
    Public MChediNum, NChediNum, AChediNum As Integer
    Public MRukuNum, NChukuNum As Integer '早入库，晚出库
    Public Sub GetEachDutyChediNum()
        MChediNum = 0
        NChediNum = 0
        AChediNum = 0

        For j As Integer = 1 To UBound(CSchediInfo)
            If UBound(CSchediInfo(j).nLinkTrain) > 1 Then
                If CSTrainInf(CSchediInfo(j).nLinkTrain(1)).lAllStartTime < 8 * 3600 _
                    And AddLitterTime(CSTrainInf(CSchediInfo(j).nLinkTrain(UBound(CSchediInfo(j).nLinkTrain))).lAllEndTime) > 8 * 3600 Then
                    MChediNum += 1
                End If
            End If
            If UBound(CSchediInfo(j).nLinkTrain) > 1 Then
                If CSTrainInf(CSchediInfo(j).nLinkTrain(1)).lAllStartTime < 12 * 3600 _
                    And AddLitterTime(CSTrainInf(CSchediInfo(j).nLinkTrain(UBound(CSchediInfo(j).nLinkTrain))).lAllEndTime) > 12 * 3600 Then
                    NChediNum += 1
                End If
            End If
            If UBound(CSchediInfo(j).nLinkTrain) > 1 Then
                If AddLitterTime(CSTrainInf(CSchediInfo(j).nLinkTrain(UBound(CSchediInfo(j).nLinkTrain))).lAllEndTime) < 12 * 3600 Then
                    MRukuNum += 1
                End If
            End If
            If UBound(CSchediInfo(j).nLinkTrain) > 1 Then
                If CSTrainInf(CSchediInfo(j).nLinkTrain(1)).lAllStartTime > 12 * 3600 AndAlso CSTrainInf(CSchediInfo(j).nLinkTrain(1)).lAllStartTime < 20 * 3600 Then
                    NChukuNum += 1
                End If
            End If
            If UBound(CSchediInfo(j).nLinkTrain) > 1 Then
                If CSTrainInf(CSchediInfo(j).nLinkTrain(1)).lAllStartTime < 18 * 3600 _
                    And AddLitterTime(CSTrainInf(CSchediInfo(j).nLinkTrain(UBound(CSchediInfo(j).nLinkTrain))).lAllEndTime) > 18 * 3600 Then
                    AChediNum += 1
                End If
            End If

        Next
    End Sub

    'Public Sub FormdicStationTrain()
    '    CSTrainsAndDrivers.dicStationTrain = New Dictionary(Of String, List(Of MergedCSLinkTrain))
    '    For Each train As MergedCSLinkTrain In CSTrainsAndDrivers.MergedCSLinkTrains
    '        If train Is Nothing Then
    '            Continue For
    '        End If
    '        If CSTrainsAndDrivers.dicStationTrain.ContainsKey(train.StartStaName & "出发") Then
    '            CSTrainsAndDrivers.dicStationTrain(train.StartStaName & "出发").Add(train)
    '        Else
    '            CSTrainsAndDrivers.dicStationTrain.Add(train.StartStaName & "出发", New List(Of MergedCSLinkTrain))
    '            CSTrainsAndDrivers.dicStationTrain(train.StartStaName & "出发").Add(train)

    '        End If

    '        If CSTrainsAndDrivers.dicStationTrain.ContainsKey(train.EndStaName & "到达") Then
    '            CSTrainsAndDrivers.dicStationTrain(train.EndStaName & "到达").Add(train)
    '        Else
    '            CSTrainsAndDrivers.dicStationTrain.Add(train.EndStaName & "到达", New List(Of MergedCSLinkTrain))
    '            CSTrainsAndDrivers.dicStationTrain(train.EndStaName & "到达").Add(train)
    '        End If
    '    Next


    '    '时间升序排列
    '    If CSTrainsAndDrivers.dicStationTrain.Count > 0 Then
    '        For Each l As KeyValuePair(Of String, List(Of MergedCSLinkTrain)) In CSTrainsAndDrivers.dicStationTrain
    '            Dim tempKey As String = l.Key
    '            Dim tempValue As List(Of MergedCSLinkTrain) = l.Value
    '            If tempKey.ToString.Contains("出发") Then
    '                For i As Integer = 0 To tempValue.Count - 1 - 1
    '                    For j As Integer = i + 1 To tempValue.Count - 1
    '                        If tempValue(i).CulStartTime > tempValue(j).CulStartTime Then
    '                            Dim temp As MergedCSLinkTrain = tempValue(i)
    '                            tempValue(i) = tempValue(j)
    '                            tempValue(j) = temp
    '                        End If
    '                    Next
    '                Next
    '            ElseIf tempKey.ToString.Contains("到达") Then
    '                For i As Integer = 0 To tempValue.Count - 1 - 1
    '                    For j As Integer = i + 1 To tempValue.Count - 1
    '                        If tempValue(i).CulEndTime > tempValue(j).CulEndTime Then
    '                            Dim temp As MergedCSLinkTrain = tempValue(i)
    '                            tempValue(i) = tempValue(j)
    '                            tempValue(j) = temp
    '                        End If
    '                    Next
    '                Next
    '            End If
    '        Next
    '    End If
    'End Sub
    Public Sub SaveXiuGai() 'ByVal strCSTableName As String)
        Dim j, k, key As Integer
        key = 1

        Dim sqlstr As String = ""
        Dim dateNow As Date
        Common.Global.StartProgress(7, "乘务计划保存中，请稍等...")
        dateNow = Now().ToString

        sqlstr = "UPDATE CS_CSTIMETABLEINF SET MODIFYTIME='" & CStr(dateNow) & "',schedulestate=" & CInt(CSTrainsAndDrivers.ScheduleState) & _
                                     ",ifcorschedule=" & CInt(CSTrainsAndDrivers.IfCorSchedule) & ",cortimetableid='" & CSTrainsAndDrivers.CorCSTimetableID & _
                                     "' WHERE LINEID='" & CStr(strCurlineID) & "' AND CSTIMETABLEID= '" & CStr(strQCurCSPlanID) & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)

        '更新自动编制参数
        If CSAutoPlanPara IsNot Nothing Then
            Dim str As String = "delete from cs_autoplanpara where cstimetableid='" & CStr(strQCurCSPlanID) & "'"
             Globle.Method.UpdateDataForAccess(str)
            str = "Insert into cs_autoplanpara values('" & CStr(strQCurCSPlanID) & "','" & CSAutoPlanPara.MoringDutyNum & _
                "','" & CSAutoPlanPara.DayDutyNum & "','" & CSAutoPlanPara.CDayDutyNum & "','" & CSAutoPlanPara.NightDutyNum & "')"
            Call  Globle.Method.UpdateDataForAccess(str)
        End If

        '更新乘务计划表
        sqlstr = "DELETE FROM CS_CREWSCHEDULE WHERE LINEID='" & CStr(strCurlineID) & "' AND CSTIMETABLEID='" & CStr(strQCurCSPlanID) & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)

        sqlstr = "SELECT * FROM CS_CREWSCHEDULE "
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        For j = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            For k = 1 To UBound(CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain)
                Dim TempCSLinkTrain As New CSLinkTrain
                TempCSLinkTrain = CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain(k)
                If TempCSLinkTrain.IsDeadHeading = False Then
                    tempTable.Rows.Add(CStr(strCurlineID), CStr(strQCurCSPlanID), CStr(key), CStr(CSTrainsAndDrivers.CSDrivers(j).CSdriverNo), CStr(GetMerTrainID(TempCSLinkTrain)), CStr(CSTrainsAndDrivers.CSDrivers(j).DutySort), CStr(TempCSLinkTrain.OutputCheCi), CStr(TempCSLinkTrain.StartTime), CStr(TempCSLinkTrain.StartStaName), CStr(TempCSLinkTrain.EndTime), CStr(TempCSLinkTrain.EndStaName), CStr(TempCSLinkTrain.nTrainID), CStr(TempCSLinkTrain.nPathID1), CStr(TempCSLinkTrain.nPathID2), CStr(TempCSLinkTrain.nCheDiID), CStr(TempCSLinkTrain.UpOrDown), CStr(TempCSLinkTrain.ZFTime), CStr(TempCSLinkTrain.STASEQ1), CStr(TempCSLinkTrain.STASEQ2), TempCSLinkTrain.distance, TempCSLinkTrain.sCheDiHao, TempCSLinkTrain.OffCheCi, TempCSLinkTrain.RoutingName)
                Else
                    tempTable.Rows.Add(CStr(strCurlineID), CStr(strQCurCSPlanID), CStr(key), CStr(CSTrainsAndDrivers.CSDrivers(j).CSdriverNo), CStr(-1), CStr(CSTrainsAndDrivers.CSDrivers(j).DutySort), CStr(TempCSLinkTrain.OutputCheCi), CStr(TempCSLinkTrain.StartTime), CStr(TempCSLinkTrain.StartStaName), CStr(TempCSLinkTrain.EndTime), CStr(TempCSLinkTrain.EndStaName), CStr(TempCSLinkTrain.nTrainID), CStr(TempCSLinkTrain.nPathID1), CStr(TempCSLinkTrain.nPathID2), CStr(TempCSLinkTrain.nCheDiID), CStr(TempCSLinkTrain.UpOrDown), CStr(TempCSLinkTrain.ZFTime), CStr(TempCSLinkTrain.STASEQ1), CStr(TempCSLinkTrain.STASEQ2), TempCSLinkTrain.distance, TempCSLinkTrain.sCheDiHao, TempCSLinkTrain.OffCheCi, TempCSLinkTrain.RoutingName)
                End If
                key = key + 1
            Next
        Next j
        Common.Global.PerformStep()
        For j = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
            If CSTrainsAndDrivers.CSLinkTrains(j).IsLinked = False Then
                Dim TempCSLinkTrain As New CSLinkTrain
                TempCSLinkTrain = CSTrainsAndDrivers.CSLinkTrains(j)
                tempTable.Rows.Add(CStr(strCurlineID), CStr(strQCurCSPlanID), CStr(key), "##", CStr(GetMerTrainID(TempCSLinkTrain)), "##", CStr(TempCSLinkTrain.OutputCheCi), CStr(TempCSLinkTrain.StartTime), CStr(TempCSLinkTrain.StartStaName), CStr(TempCSLinkTrain.EndTime), CStr(TempCSLinkTrain.EndStaName), CStr(TempCSLinkTrain.nTrainID), CStr(TempCSLinkTrain.nPathID1), CStr(TempCSLinkTrain.nPathID2), CStr(TempCSLinkTrain.nCheDiID), CStr(TempCSLinkTrain.UpOrDown), CStr(TempCSLinkTrain.ZFTime), CStr(TempCSLinkTrain.STASEQ1), CStr(TempCSLinkTrain.STASEQ2), TempCSLinkTrain.distance, TempCSLinkTrain.sCheDiHao, TempCSLinkTrain.OffCheCi, TempCSLinkTrain.RoutingName)
                key = key + 1
            End If
        Next
        Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        Common.Global.PerformStep()



        '更新乘务详细列表
        sqlstr = "DELETE FROM CS_DUTYONOFFDETAIL WHERE LINEID='" & CStr(strCurlineID) & "' AND CSTIMETABLEID='" & CStr(strQCurCSPlanID) & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)

        sqlstr = "SELECT * FROM CS_DUTYONOFFDETAIL "
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            Dim starttime As Integer = -1
            Dim endtime As Integer = -1
            Dim dutyonplace As String = ""
            Dim dutyoffplace As String = ""
            Dim startTrainno As String = ""
            If UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain) > 0 Then
                starttime = CSTrainsAndDrivers.CSDrivers(i).BeginWorkTime
                endtime = CSTrainsAndDrivers.CSDrivers(i).EndEorkTime
                dutyonplace = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).StartStaName
                dutyoffplace = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)).EndStaName
                startTrainno = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).sCheDiHao
            End If
            tempTable.Rows.Add(CStr(strQCurCSPlanID), CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, CSTrainsAndDrivers.CSDrivers(i).DutySort, starttime, dutyonplace, endtime, dutyoffplace, strCurlineID, startTrainno)
        Next
        For i = 0 To CSTrainsAndDrivers.PreParedDutyDrivers.Count - 1
            Dim starttime As Integer = -1
            Dim endtime As Integer = -1
            Dim dutyonplace As String = ""
            Dim dutyoffplace As String = ""
            Dim startTrainno As String = ""
            starttime = CSTrainsAndDrivers.PreParedDutyDrivers(i).CSLinkTrain(1).StartTime
            endtime = CSTrainsAndDrivers.PreParedDutyDrivers(i).CSLinkTrain(1).EndTime
            dutyonplace = CSTrainsAndDrivers.PreParedDutyDrivers(i).CSLinkTrain(1).StartStaName
            dutyoffplace = CSTrainsAndDrivers.PreParedDutyDrivers(i).CSLinkTrain(1).EndStaName
            startTrainno = "-"
            tempTable.Rows.Add(CStr(strQCurCSPlanID), CSTrainsAndDrivers.PreParedDutyDrivers(i).CSdriverNo, CSTrainsAndDrivers.PreParedDutyDrivers(i).DutySort, starttime, dutyonplace, endtime, dutyoffplace, strCurlineID, startTrainno)
        Next
        For i = 0 To CSTrainsAndDrivers.PreParedTrainDrivers.Count - 1
            Dim starttime As Integer = -1
            Dim endtime As Integer = -1
            Dim dutyonplace As String = ""
            Dim dutyoffplace As String = ""
            Dim startTrainno As String = ""
            starttime = CSTrainsAndDrivers.PreParedTrainDrivers(i).CSLinkTrain(1).StartTime
            endtime = CSTrainsAndDrivers.PreParedTrainDrivers(i).CSLinkTrain(1).EndTime
            dutyonplace = CSTrainsAndDrivers.PreParedTrainDrivers(i).CSLinkTrain(1).StartStaName
            dutyoffplace = CSTrainsAndDrivers.PreParedTrainDrivers(i).CSLinkTrain(1).EndStaName
            startTrainno = "-"
            tempTable.Rows.Add(CStr(strQCurCSPlanID), CSTrainsAndDrivers.PreParedTrainDrivers(i).CSdriverNo, CSTrainsAndDrivers.PreParedTrainDrivers(i).DutySort, starttime, dutyonplace, endtime, dutyoffplace, strCurlineID, startTrainno)
        Next
        Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        Common.Global.PerformStep()

        '插入劳动工作量
        sqlstr = "DELETE FROM CS_WORKLOAD WHERE LINEID='" & CStr(strCurlineID) & "' AND CSTIMETABLEID='" & CStr(strQCurCSPlanID) & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)

        sqlstr = "SELECT * FROM CS_WORKLOAD "
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        For Each driver As CSDriver In CSTrainsAndDrivers.CSDrivers
            If driver Is Nothing = False Then
                Dim ShowColor As String = ColorTranslator.ToHtml(driver.ShowColor)
                Dim DefaultColor As String
                If driver.DefaultColor <> Nothing Then
                    DefaultColor = ColorTranslator.ToHtml(driver.DefaultColor)
                Else
                    DefaultColor = ""
                End If
                tempTable.Rows.Add(CStr(strCurlineID), CStr(strQCurCSPlanID), CStr(driver.CSdriverNo), CStr(1), CStr(driver.DutySort), CStr(driver.WorkTime), _
                                   CStr(driver.ZFTime), driver.DriveTime, driver.DriveDistance, driver.PreTrainTime, driver.PreDutyTime, driver.BeginWorkTime, _
                                   driver.EndEorkTime, driver.PreDutyOffTime, driver.BelongArea, driver.OutPutCSdriverNo, ShowColor, DefaultColor, driver.State)
            End If
        Next

        Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        tempTable.Dispose()
        Common.Global.PerformStep()

        '插入用餐信息
        sqlstr = "DELETE FROM cs_dinnerinf WHERE LINEID='" & CStr(strCurlineID) & "' AND CSTIMETABLEID='" & CStr(strQCurCSPlanID) & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        For Each driver As CSDriver In CSTrainsAndDrivers.CSDrivers
            If driver Is Nothing = False Then
                'Dim str As String
                If driver.FlagDinner = True Then
                    For Each strkey As String In driver.AllDinnerInfo.Keys
                        sqlstr = "insert into cs_dinnerinf values ('" & CStr(strCurlineID) & "','" & CStr(strQCurCSPlanID) & "','" & CStr(driver.CSdriverNo) & "'," & strkey.Split("-")(1) & "," & strkey.Split("-")(2) & ",'" & strkey.Split("-")(0) & "','" & driver.FlagDinner & "' )"
                        Globle.Method.UpdateDataForAccess(sqlstr)
                    Next
                Else
                    sqlstr = "insert into cs_dinnerinf values ('" & CStr(strCurlineID) & "','" & CStr(strQCurCSPlanID) & "','" & CStr(driver.CSdriverNo) & "'," & driver.DinnerStartTime & "," & driver.DinnerEndTime & ",'" & driver.DinnerStation & "','" & driver.FlagDinner & "' )"
                    Globle.Method.UpdateDataForAccess(sqlstr)
                End If
            End If
        Next
        Common.Global.PerformStep()

        '插入特殊限定的任务信息
        Dim tableName As List(Of String) = Globle.Method.GetAllTableNamefromAccess
        If tableName.Contains("CS_SPECIALDUTYWORK") = False Then
            sqlstr = "create table CS_SPECIALDUTYWORK(LINEID char(30),CSTIMETABLEID char(100),DUTYSORT char(50),DUTYTYPE char(30),DUTYWORK char(100))"
            Globle.Method.UpdateDataForAccess(sqlstr)
        End If
        sqlstr = "DELETE FROM CS_SPECIALDUTYWORK WHERE LINEID='" & CStr(strCurlineID) & "' AND CSTIMETABLEID='" & CStr(strQCurCSPlanID) & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        For Each driver As CSDriver In CSTrainsAndDrivers.CSDrivers
            If driver Is Nothing = False Then
                'Dim str As String
                If driver.dutyWork <> "" Then
                    sqlstr = "insert into CS_SPECIALDUTYWORK values ('" & CStr(strCurlineID) & "','" & CStr(strQCurCSPlanID) & "','" & driver.CSdriverNo & "','" & driver.DutySort & "','" & driver.dutyWork & "' )"
                    Globle.Method.UpdateDataForAccess(sqlstr)
                End If
            End If
        Next

        '更新备班输出编号
        Call UpdatePreParedAreaInfo()

        Common.Global.EndProgress()
        MsgBox("乘务计划表保存成功！", MsgBoxStyle.OkOnly, "成功")

    End Sub

    '更新数据库中备班区域信息
    Public Sub UpdatePreParedAreaInfo()
        Dim str As String = ""
        If CSTrainsAndDrivers.PreParedDutyDrivers IsNot Nothing AndAlso CSTrainsAndDrivers.PreParedDutyDrivers.Count > 0 Then
            For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
                str = "update cs_result_prepareddutyinf t set t.remark='" & dri.BelongArea & "',t.outputcsdriverno='" & dri.OutPutCSdriverNo _
                    & "' where t.lineid='" & CStr(strCurlineID) & "' and t.cstimetableid='" & CStr(strQCurCSPlanID) & _
                    "' and t.name='" & dri.CSdriverNo & "' and t.dutysort='" & dri.DutySort & "'"
                 Globle.Method.UpdateDataForAccess(str)
            Next
        End If
        If CSTrainsAndDrivers.PreParedTrainDrivers IsNot Nothing AndAlso CSTrainsAndDrivers.PreParedTrainDrivers.Count > 0 Then
            For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
                str = "update cs_result_preparedtraininf t set t.remark='" & dri.BelongArea & "',t.outputcsdriverno='" & dri.OutPutCSdriverNo _
                    & "' where t.lineid='" & CStr(strCurlineID) & "' and t.cstimetableid='" & CStr(strQCurCSPlanID) & _
                    "' and t.name='" & dri.CSdriverNo & "' and t.dutysort='" & dri.DutySort & "'"
                 Globle.Method.UpdateDataForAccess(str)
            Next
        End If
    End Sub

    '填充数据库Oracle
    Public Sub SaveResults(ByVal tmpName As String)
        Dim i, j, k, key As Integer
        key = 1
        Dim str As String = ""
        Dim cmd As New OleDbCommand
        Dim dtable As New DataTable
        Dim sqlstr As String = ""
        Dim dateNow As Date
        dateNow = Now()

        Dim tmpNewCSSKBTime As String = dateNow.Year & dateNow.Month & dateNow.Day & "_" & dateNow.Hour & dateNow.Minute & dateNow.Second
        Dim tmpNewCSSKBID As String = CStr(tmpNewCSSKBTime) & "CS"

        If CStr(tmpName).Length > 20 Then
            tmpName = tmpName.Substring(0, 20)
        End If

        sqlstr = "SELECT * FROM CS_CSTIMETABLEINF "
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        For i = 0 To tempTable.Rows.Count - 1
            If CStr(strCurlineID) = tempTable.Rows(i).Item("LINEID").ToString.Trim And tmpName = tempTable.Rows(i).Item("CSTIMETABLENAME").ToString.Trim Then
                MsgBox("重复命名！")
                Exit Sub
            End If
        Next
        Common.Global.StartProgress(6, "乘务计划保存中，请稍等...")

        tempTable.Rows.Add(CStr(strCurlineID), CStr(tmpName), tmpNewCSSKBID, CStr(dateNow), CStr(dateNow), CStr(strDiagram), CStr(DiagramCurID), CInt(CSTrainsAndDrivers.ScheduleState), CInt(CSTrainsAndDrivers.IfCorSchedule), CSTrainsAndDrivers.CorCSTimetableID)
        Globle.Method.UpdateDataForAccess(sqlstr, tempTable)

        strQCurCSPlanName = tmpName
        strQCurCSPlanID = tmpNewCSSKBID '当前乘务计划表ID

        If CSAutoPlanPara IsNot Nothing Then      '保存自动编制参数
            str = "Insert into cs_autoplanpara values('" & CStr(strQCurCSPlanID) & "','" & CSAutoPlanPara.MoringDutyNum & _
                "','" & CSAutoPlanPara.DayDutyNum & "','" & CSAutoPlanPara.CDayDutyNum & "','" & CSAutoPlanPara.NightDutyNum & "')"
            Call  Globle.Method.UpdateDataForAccess(str)
        End If

        sqlstr = "SELECT * FROM CS_CREWSCHEDULE"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        For j = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            For k = 1 To UBound(CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain)
                Dim TempCSLinkTrain As New CSLinkTrain
                TempCSLinkTrain = CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain(k)
                If TempCSLinkTrain.IsDeadHeading = False Then
                    tempTable.Rows.Add(CStr(strCurlineID), CStr(strQCurCSPlanID), CStr(key), CStr(CSTrainsAndDrivers.CSDrivers(j).CSdriverNo), CStr(GetMerTrainID(TempCSLinkTrain)), CStr(CSTrainsAndDrivers.CSDrivers(j).DutySort), CStr(TempCSLinkTrain.OutputCheCi), CStr(TempCSLinkTrain.StartTime), CStr(TempCSLinkTrain.StartStaName), CStr(TempCSLinkTrain.EndTime), CStr(TempCSLinkTrain.EndStaName), CStr(TempCSLinkTrain.nTrainID), CStr(TempCSLinkTrain.nPathID1), CStr(TempCSLinkTrain.nPathID2), CStr(TempCSLinkTrain.nCheDiID), CStr(TempCSLinkTrain.UpOrDown), CStr(TempCSLinkTrain.ZFTime), CStr(TempCSLinkTrain.STASEQ1), CStr(TempCSLinkTrain.STASEQ2), TempCSLinkTrain.distance, TempCSLinkTrain.sCheDiHao, TempCSLinkTrain.OffCheCi, TempCSLinkTrain.RoutingName)
                Else
                    tempTable.Rows.Add(CStr(strCurlineID), CStr(strQCurCSPlanID), CStr(key), CStr(CSTrainsAndDrivers.CSDrivers(j).CSdriverNo), CStr(-1), CStr(CSTrainsAndDrivers.CSDrivers(j).DutySort), CStr(TempCSLinkTrain.OutputCheCi), CStr(TempCSLinkTrain.StartTime), CStr(TempCSLinkTrain.StartStaName), CStr(TempCSLinkTrain.EndTime), CStr(TempCSLinkTrain.EndStaName), CStr(TempCSLinkTrain.nTrainID), CStr(TempCSLinkTrain.nPathID1), CStr(TempCSLinkTrain.nPathID2), CStr(TempCSLinkTrain.nCheDiID), CStr(TempCSLinkTrain.UpOrDown), CStr(TempCSLinkTrain.ZFTime), CStr(TempCSLinkTrain.STASEQ1), CStr(TempCSLinkTrain.STASEQ2), TempCSLinkTrain.distance, TempCSLinkTrain.sCheDiHao, TempCSLinkTrain.OffCheCi, TempCSLinkTrain.RoutingName)
                End If
                key = key + 1
            Next
        Next j
        Common.Global.PerformStep()

        For j = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
            If CSTrainsAndDrivers.CSLinkTrains(j).IsLinked = False Then
                Dim TempCSLinkTrain As New CSLinkTrain
                TempCSLinkTrain = CSTrainsAndDrivers.CSLinkTrains(j)
                tempTable.Rows.Add(CStr(strCurlineID), CStr(strQCurCSPlanID), CStr(key), "##", CStr(GetMerTrainID(TempCSLinkTrain)), "##", CStr(TempCSLinkTrain.OutputCheCi), CStr(TempCSLinkTrain.StartTime), CStr(TempCSLinkTrain.StartStaName), CStr(TempCSLinkTrain.EndTime), CStr(TempCSLinkTrain.EndStaName), CStr(TempCSLinkTrain.nTrainID), CStr(TempCSLinkTrain.nPathID1), CStr(TempCSLinkTrain.nPathID2), CStr(TempCSLinkTrain.nCheDiID), CStr(TempCSLinkTrain.UpOrDown), CStr(TempCSLinkTrain.ZFTime), CStr(TempCSLinkTrain.STASEQ1), CStr(TempCSLinkTrain.STASEQ2), TempCSLinkTrain.distance, TempCSLinkTrain.sCheDiHao, TempCSLinkTrain.OffCheCi, TempCSLinkTrain.RoutingName)
                key = key + 1
            End If
        Next
        Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        Common.Global.PerformStep()

        '更新乘务详细列表
        sqlstr = "DELETE FROM CS_DUTYONOFFDETAIL WHERE LINEID='" & CStr(strCurlineID) & "' AND CSTIMETABLEID='" & CStr(strQCurCSPlanID) & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)

        sqlstr = "SELECT * FROM CS_DUTYONOFFDETAIL "
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            Dim starttime As Integer = -1
            Dim endtime As Integer = -1
            Dim dutyonplace As String = ""
            Dim dutyoffplace As String = ""
            Dim startTrainno As String = ""
            If UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain) > 0 Then
                starttime = CSTrainsAndDrivers.CSDrivers(i).BeginWorkTime
                endtime = CSTrainsAndDrivers.CSDrivers(i).EndEorkTime
                dutyonplace = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).StartStaName
                dutyoffplace = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)).EndStaName
                startTrainno = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).sCheDiHao
            End If
            tempTable.Rows.Add(CStr(strQCurCSPlanID), CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, CSTrainsAndDrivers.CSDrivers(i).DutySort, starttime, dutyonplace, endtime, dutyoffplace, strCurlineID, startTrainno)
        Next
        For i = 0 To CSTrainsAndDrivers.PreParedDutyDrivers.Count - 1
            Dim starttime As Integer = -1
            Dim endtime As Integer = -1
            Dim dutyonplace As String = ""
            Dim dutyoffplace As String = ""
            Dim startTrainno As String = ""
            starttime = CSTrainsAndDrivers.PreParedDutyDrivers(i).CSLinkTrain(1).StartTime
            endtime = CSTrainsAndDrivers.PreParedDutyDrivers(i).CSLinkTrain(1).EndTime
            dutyonplace = CSTrainsAndDrivers.PreParedDutyDrivers(i).CSLinkTrain(1).StartStaName
            dutyoffplace = CSTrainsAndDrivers.PreParedDutyDrivers(i).CSLinkTrain(1).EndStaName
            startTrainno = "-"
            tempTable.Rows.Add(CStr(strQCurCSPlanID), CSTrainsAndDrivers.PreParedDutyDrivers(i).CSdriverNo, CSTrainsAndDrivers.PreParedDutyDrivers(i).DutySort, starttime, dutyonplace, endtime, dutyoffplace, strCurlineID, startTrainno)
        Next
        For i = 0 To CSTrainsAndDrivers.PreParedTrainDrivers.Count - 1
            Dim starttime As Integer = -1
            Dim endtime As Integer = -1
            Dim dutyonplace As String = ""
            Dim dutyoffplace As String = ""
            Dim startTrainno As String = ""
            starttime = CSTrainsAndDrivers.PreParedTrainDrivers(i).CSLinkTrain(1).StartTime
            endtime = CSTrainsAndDrivers.PreParedTrainDrivers(i).CSLinkTrain(1).EndTime
            dutyonplace = CSTrainsAndDrivers.PreParedTrainDrivers(i).CSLinkTrain(1).StartStaName
            dutyoffplace = CSTrainsAndDrivers.PreParedTrainDrivers(i).CSLinkTrain(1).EndStaName
            startTrainno = "-"
            tempTable.Rows.Add(CStr(strQCurCSPlanID), CSTrainsAndDrivers.PreParedTrainDrivers(i).CSdriverNo, CSTrainsAndDrivers.PreParedTrainDrivers(i).DutySort, starttime, dutyonplace, endtime, dutyoffplace, strCurlineID, startTrainno)
        Next
        Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        Common.Global.PerformStep()

        '插入劳动工作量
        sqlstr = "DELETE FROM CS_WORKLOAD WHERE LINEID='" & CStr(strCurlineID) & "' AND CSTIMETABLEID='" & CStr(strQCurCSPlanID) & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "SELECT * FROM CS_WORKLOAD "
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        For Each driver As CSDriver In CSTrainsAndDrivers.CSDrivers
            If driver Is Nothing = False Then
                Dim ShowColor As String = ColorTranslator.ToHtml(driver.ShowColor)
                Dim DefaultColor As String
                If driver.DefaultColor <> Nothing Then
                    DefaultColor = ColorTranslator.ToHtml(driver.DefaultColor)
                Else
                    DefaultColor = ""
                End If
                tempTable.Rows.Add(CStr(strCurlineID), CStr(strQCurCSPlanID), CStr(driver.CSdriverNo), CStr(1), CStr(driver.DutySort), CStr(driver.WorkTime), _
                                   CStr(driver.ZFTime), CStr(driver.DriveTime), driver.DriveDistance, driver.PreTrainTime, driver.PreDutyTime, driver.BeginWorkTime, _
                                   driver.EndEorkTime, driver.PreDutyOffTime, driver.BelongArea, driver.OutPutCSdriverNo, ShowColor, DefaultColor, driver.State)
            End If
        Next
        Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        Common.Global.PerformStep()

        '插入用餐信息
        sqlstr = "DELETE FROM cs_dinnerinf WHERE LINEID='" & CStr(strCurlineID) & "' AND CSTIMETABLEID='" & CStr(strQCurCSPlanID) & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        For Each driver As CSDriver In CSTrainsAndDrivers.CSDrivers
            If driver Is Nothing = False Then
                If driver.FlagDinner = True Then
                    For Each strkey As String In driver.AllDinnerInfo.Keys
                        sqlstr = "insert into cs_dinnerinf values ('" & CStr(strCurlineID) & "','" & CStr(strQCurCSPlanID) & "','" & CStr(driver.CSdriverNo) & "'," & strkey.Split("-")(1) & "," & strkey.Split("-")(2) & ",'" & strkey.Split("-")(0) & "','" & driver.FlagDinner & "' )"
                        Globle.Method.UpdateDataForAccess(sqlstr)
                    Next
                Else
                    sqlstr = "insert into cs_dinnerinf values ('" & CStr(strCurlineID) & "','" & CStr(strQCurCSPlanID) & "','" & CStr(driver.CSdriverNo) & "'," & driver.DinnerStartTime & "," & driver.DinnerEndTime & ",'" & driver.DinnerStation & "','" & driver.FlagDinner & "' )"
                    Globle.Method.UpdateDataForAccess(sqlstr)
                End If
            End If
        Next
        Common.Global.PerformStep()

        '插入特殊限定的任务信息
        Dim tableName As List(Of String) = Globle.Method.GetAllTableNamefromAccess
        If tableName.Contains("CS_SPECIALDUTYWORK") = False Then
            sqlstr = "create table CS_SPECIALDUTYWORK(LINEID char(30),CSTIMETABLEID char(100),DUTYSORT char(50),DUTYTYPE char(30),DUTYWORK char(100))"
            Globle.Method.UpdateDataForAccess(sqlstr)
        End If
        sqlstr = "DELETE FROM CS_SPECIALDUTYWORK WHERE LINEID='" & CStr(strCurlineID) & "' AND CSTIMETABLEID='" & CStr(strQCurCSPlanID) & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        For Each driver As CSDriver In CSTrainsAndDrivers.CSDrivers
            If driver Is Nothing = False Then
                'Dim str As String
                If driver.dutyWork <> "" Then
                    sqlstr = "insert into CS_SPECIALDUTYWORK values ('" & CStr(strCurlineID) & "','" & CStr(strQCurCSPlanID) & "','" & driver.CSdriverNo & "','" & driver.DutySort & "','" & driver.dutyWork & "' )"
                    Globle.Method.UpdateDataForAccess(sqlstr)
                End If

            End If
        Next

        '保存设置的备车备班信息       
        str = "delete from CS_RESULT_PREPAREDDUTYINF where cstimetableid='" & CStr(strQCurCSPlanID) & "'"
         Globle.Method.UpdateDataForAccess(str)
        str = "delete from CS_RESULT_PREPAREDTRAININF where cstimetableid='" & CStr(strQCurCSPlanID) & "'"
         Globle.Method.UpdateDataForAccess(str)
        Call CopyBasicSetIntoResult("CS_RESULT_PREPAREDDUTYINF", "CS_PREPAREDDUTYINF")
        Call CopyBasicSetIntoResult("CS_RESULT_PREPAREDTRAININF", "CS_PREPAREDTRAININF")
        Common.Global.EndProgress()

        MsgBox("乘务计划表保存成功！", MsgBoxStyle.OkOnly, "提示")

    End Sub
    Public Sub CopyBasicSetIntoResult(ByVal tab1 As String, ByVal tab2 As String)
        Dim sqlstr As String = ""
        sqlstr = "DELETE FROM " & tab1 & " WHERE LINEID='" & CStr(strCurlineID) & "' AND CSTIMETABLEID='" & CStr(strQCurCSPlanID) & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        'sqlstr = " insert into " & tab1 & " select t.*, '" & CStr(strQCurCSPlanID) & "' from  " & tab2 & " t"
        sqlstr = " insert into " & tab1 & " select t.*,s.cstimetableid from " & tab2 & " t,cs_cstimetableinf s where t.lineid='" & CStr(strCurlineID) & "' and s.cstimetableid='" & CStr(strQCurCSPlanID) & "' and s.lineid=t.lineid"
        Globle.Method.UpdateDataForAccess(sqlstr)

    End Sub

  

    Public Sub ReFromDriverIndex()
        Try
            If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
                Call SortCSDriversByBeginTime()
                Dim nCount As Integer = 0
                For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If dri IsNot Nothing Then
                        If dri.DutySort = "早班" Then
                            nCount += 1
                            dri.OutPutCSdriverNo = dri.DutySort & nCount.ToString("000")
                        End If
                    End If
                Next
                nCount = 0
                For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If dri IsNot Nothing Then
                        If dri.DutySort = "白班" Then
                            nCount += 1
                            dri.OutPutCSdriverNo = dri.DutySort & nCount.ToString("000")
                        End If
                    End If
                Next
                nCount = 0
                For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If dri IsNot Nothing Then
                        If dri.DutySort = "夜班" Then
                            nCount += 1
                            dri.OutPutCSdriverNo = dri.DutySort & nCount.ToString("000")
                        End If
                    End If
                Next
                nCount = 0
                For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If dri IsNot Nothing Then
                        If dri.DutySort = "日勤班" Then
                            nCount += 1
                            dri.OutPutCSdriverNo = "日勤班" & nCount.ToString("000")
                        End If
                    End If
                Next
                If UBound(CSTrainsAndDrivers.CSDrivers) > 1 Then
                    For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers) - 1
                        For j As Integer = i + 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                            If CSTrainsAndDrivers.CSDrivers(j).OutPutCSdriverNo < CSTrainsAndDrivers.CSDrivers(i).OutPutCSdriverNo Then
                                Dim tempdri As CSDriver
                                tempdri = CSTrainsAndDrivers.CSDrivers(i)
                                CSTrainsAndDrivers.CSDrivers(i) = CSTrainsAndDrivers.CSDrivers(j)
                                CSTrainsAndDrivers.CSDrivers(j) = tempdri
                            End If
                        Next
                    Next
                    For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                        CSTrainsAndDrivers.CSDrivers(i).CSDriverID = i
                    Next
                End If
                Call CSRefreshDiagram()
            End If
        Catch ex As Exception
            MsgBox("请先整理任务，然后重新编号！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
        End Try
    End Sub

    Public Sub SortCSDriversByBeginTime()
        If UBound(CSTrainsAndDrivers.CSDrivers) > 1 Then
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers) - 1
                For j As Integer = i + 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                    If CSTrainsAndDrivers.CSDrivers(j).BeginWorkTime < CSTrainsAndDrivers.CSDrivers(i).BeginWorkTime Then
                        Dim tempdri As CSDriver = CSTrainsAndDrivers.CSDrivers(j)
                        CSTrainsAndDrivers.CSDrivers(j) = CSTrainsAndDrivers.CSDrivers(i)
                        CSTrainsAndDrivers.CSDrivers(i) = tempdri
                    End If
                Next
            Next
        End If
    End Sub

    Public Sub SortCSDriversByOutPutNum()
        If UBound(CSTrainsAndDrivers.CSDrivers) > 1 Then
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers) - 1
                For j As Integer = i + 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                    If CSTrainsAndDrivers.CSDrivers(j).OutPutCSdriverNo < CSTrainsAndDrivers.CSDrivers(i).OutPutCSdriverNo Then
                        Dim tempdri As CSDriver = CSTrainsAndDrivers.CSDrivers(j)
                        CSTrainsAndDrivers.CSDrivers(j) = CSTrainsAndDrivers.CSDrivers(i)
                        CSTrainsAndDrivers.CSDrivers(i) = tempdri
                    End If
                Next
            Next
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                CSTrainsAndDrivers.CSDrivers(i).CSDriverID = i
            Next
            CSTrainsAndDrivers.MorningDrivers.Clear()
            CSTrainsAndDrivers.DayDrivers.Clear()
            CSTrainsAndDrivers.CDayDrivers.Clear()
            CSTrainsAndDrivers.NightDrivers.Clear()
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                Select Case CSTrainsAndDrivers.CSDrivers(i).DutySort
                    Case "早班"
                        CSTrainsAndDrivers.MorningDrivers.Add(CSTrainsAndDrivers.CSDrivers(i))
                    Case "白班"
                        CSTrainsAndDrivers.DayDrivers.Add(CSTrainsAndDrivers.CSDrivers(i))
                    Case "夜班"
                        CSTrainsAndDrivers.NightDrivers.Add(CSTrainsAndDrivers.CSDrivers(i))
                    Case "日勤班"
                        CSTrainsAndDrivers.CDayDrivers.Add(CSTrainsAndDrivers.CSDrivers(i))
                End Select
            Next
            MsgBox("重新排序完成！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "完成")
        End If
    End Sub

    Public Sub AddUnReDoInfo(ByVal IfUnDo As Boolean)
        Dim sfFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
        Dim filePath As String = ""
        If IfUnDo Then
            filePath = Application.StartupPath & "\UndoFile\"
        Else
            filePath = Application.StartupPath & "\RedoFile\"
        End If
        Dim index As Integer = 0
        Dim i As Integer = 0
        While True                      '确定撤销文件名称
            Dim filename As String = filePath & i.ToString & ".dat"
            If File.Exists(filename) Then
                i += 1
                If i >= 100 Then
                    index = 99
                    File.Delete(filePath & "0.dat")
                    For j As Integer = 0 To 98
                        File.Move(filePath & (j + 1).ToString & ".dat", filePath & j.ToString & ".dat")
                    Next
                    Exit While
                End If
            Else
                index = i
                Exit While
            End If
        End While
        Dim fStream As New FileStream(filePath & index.ToString & ".dat", FileMode.Create)
        sfFormatter.Serialize(fStream, CSTrainsAndDrivers)
        fStream.Close()
    End Sub

    Public Sub SetAllDrivers()
        ReDim CSTrainsAndDrivers.CSDrivers(0)
        If CSTrainsAndDrivers.MorningDrivers IsNot Nothing Then
            For Each dri As CSDriver In CSTrainsAndDrivers.MorningDrivers
                ReDim Preserve CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers) + 1)
                CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)) = dri
                CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID = UBound(CSTrainsAndDrivers.CSDrivers)
                'CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSdriverNo = CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID.ToString("000")
            Next
        End If
        If CSTrainsAndDrivers.DayDrivers IsNot Nothing Then
            Dim ncount As Integer = 0
            For Each dri As CSDriver In CSTrainsAndDrivers.DayDrivers
                ReDim Preserve CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers) + 1)
                CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)) = dri
                CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID = UBound(CSTrainsAndDrivers.CSDrivers)
                'CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSdriverNo = CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID.ToString("000")
                ncount += 1
            Next
        End If
        If CSTrainsAndDrivers.CDayDrivers IsNot Nothing Then
            For Each dri As CSDriver In CSTrainsAndDrivers.CDayDrivers
                ReDim Preserve CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers) + 1)
                CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)) = dri
                CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID = UBound(CSTrainsAndDrivers.CSDrivers)
                'CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSdriverNo = CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID.ToString("000")
            Next
        End If
        If CSTrainsAndDrivers.NightDrivers IsNot Nothing Then
            For Each dri As CSDriver In CSTrainsAndDrivers.NightDrivers
                ReDim Preserve CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers) + 1)
                CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)) = dri
                CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID = UBound(CSTrainsAndDrivers.CSDrivers)
                'CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSdriverNo = CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID.ToString("000")
            Next
        End If
        If CSTrainsAndDrivers.OtherDrivers IsNot Nothing Then
            For Each dri As CSDriver In CSTrainsAndDrivers.OtherDrivers
                ReDim Preserve CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers) + 1)
                CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)) = dri
                CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID = UBound(CSTrainsAndDrivers.CSDrivers)
                'CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSdriverNo = CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID.ToString("000")
            Next
        End If
        Call ReFromDriverIndex()
    End Sub

    Public Sub LoadAreaInfo()
        '========加载区域信息
        CSTrainsAndDrivers.AreaInfoS = New List(Of AreaInfo)
        Dim Str As String = "select * from cs_areainfo where lineid='" & strCurlineID & "' order by id"
        Dim tab As System.Data.DataTable = ReadData(Str)
        If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
            For i As Integer = 0 To tab.Rows.Count - 1
                Dim tempArea As New AreaInfo(tab.Rows(i).Item("LineID").ToString, tab.Rows(i).Item("AreaName").ToString)
                tempArea.OnDutyPlaces = tab.Rows(i).Item("OnDutyPlaces").ToString.Split(",")
                tempArea.ForDutySorts = tab.Rows(i).Item("DutySort").ToString.Split(",")
                tempArea.YunZhuanPara = tab.Rows(i).Item("YunZhuanPara").ToString
                CSTrainsAndDrivers.AreaInfoS.Add(tempArea)
            Next
        End If
    End Sub

    Public Sub ReadCorDriversAndTrains(ByVal CorCSPlanName)
        Dim DaTab As New DataTable
        Dim i, j, flag, nDriverNum As Integer
        Dim Str As String
        Dim CorCSPlanID As String = GetCSPlanIDFromCSPlanName(CorCSPlanName)
        CSTrainsAndDrivers.CorCSTimetableID = CorCSPlanID
        Str = "SELECT *  FROM  CS_WORKLOAD t WHERE t.LINEID='" & CStr(strCurlineID) & "' AND t.CSTIMETABLEID='" & CStr(CorCSPlanID) & "' order by t.DriverNo"
        DaTab = ReadData(Str)
        nDriverNum = DaTab.Rows.Count
        If nDriverNum = 0 Then
            MsgBox("没有找到衔接计划数据！")
            Exit Sub
        Else
            ReDim CSTrainsAndDrivers.CorCSDrivers(nDriverNum)
            For i = 1 To nDriverNum
                CSTrainsAndDrivers.CorCSDrivers(i) = New CSDriver()
                CSTrainsAndDrivers.CorCSDrivers(i).CSDriverID = i
                CSTrainsAndDrivers.CorCSDrivers(i).CSdriverNo = DaTab.Rows(i - 1).Item("DriverNo").ToString
                CSTrainsAndDrivers.CorCSDrivers(i).DutySort = DaTab.Rows(i - 1).Item("DUTYSORT").ToString
                CSTrainsAndDrivers.CorCSDrivers(i).PreDutyTime = Val(DaTab.Rows(i - 1).Item("PREPAREDUTYTIME").ToString)
                'CSTrainsAndDrivers.CorCSDrivers(i).PreTrainTime = Val(DaTab.Rows(i - 1).Item("PREPARETRAINTIME").ToString)
                CSTrainsAndDrivers.CorCSDrivers(i).PreDutyOffTime = Val(DaTab.Rows(i - 1).Item("PREPAREDUTYOFFTIME").ToString)
                CSTrainsAndDrivers.CorCSDrivers(i).OutPutCSdriverNo = DaTab.Rows(i - 1).Item("OutPutCSdriverNo").ToString
                ReDim CSTrainsAndDrivers.CorCSDrivers(i).CSLinkTrain(0)
            Next
            Str = "SELECT *  FROM  cs_dinnerinf t  WHERE t.LINEID='" & CStr(strCurlineID) & "' AND t.CSTIMETABLEID='" & CStr(CorCSPlanID) & "' order by t.DriverNo"
            DaTab = ReadData(Str)
            If DaTab IsNot Nothing AndAlso DaTab.Rows.Count > 0 Then
                For i = 1 To UBound(CSTrainsAndDrivers.CorCSDrivers)
                    For j = 1 To DaTab.Rows.Count
                        If DaTab.Rows(j - 1).Item("driverno").ToString.Trim = CSTrainsAndDrivers.CorCSDrivers(i).CSdriverNo Then
                            CSTrainsAndDrivers.CorCSDrivers(i).DinnerStartTime = Val(DaTab.Rows(j - 1).Item("DINNERBEGINTIME").ToString)
                            CSTrainsAndDrivers.CorCSDrivers(i).DinnerEndTime = Val(DaTab.Rows(j - 1).Item("DINNERENDTIME").ToString)
                            CSTrainsAndDrivers.CorCSDrivers(i).FlagDinner = CType(DaTab.Rows(j - 1).Item("havedinner").ToString, Boolean)
                            CSTrainsAndDrivers.CorCSDrivers(i).DinnerStation = DaTab.Rows(j - 1).Item("DINNERPLACE").ToString
                            Exit For
                        End If
                    Next

                Next
            End If


            Str = "Select * from cs_amdrivercorrespond where adrivertimetableid='" & CorCSPlanID & "' and mdrivertimetableid='" & CorCSPlanID & "'"
            DaTab = ReadData(Str)
            If DaTab IsNot Nothing Then
                For Each row As DataRow In DaTab.Rows
                    Dim MorDriNo As String = row.Item("MDriverno").ToString
                    Dim NigDriNo As String = row.Item("ADriverno").ToString
                    Dim MDriver As CSDriver = Array.Find(CSTrainsAndDrivers.CorCSDrivers, Function(value As CSDriver)
                                                                                              Return (value IsNot Nothing AndAlso value.CSdriverNo = MorDriNo)
                                                                                          End Function)
                    Dim ADriver As CSDriver = Array.Find(CSTrainsAndDrivers.CorCSDrivers, Function(value As CSDriver)
                                                                                              Return (value IsNot Nothing AndAlso value.CSdriverNo = NigDriNo)
                                                                                          End Function)
                    If MDriver IsNot Nothing AndAlso ADriver IsNot Nothing Then
                        ADriver.LinkedMorDriver = MDriver
                        MDriver.LinkedNightDriver = ADriver
                    End If
                Next
            End If

            Str = "SELECT * FROM  CS_CREWSCHEDULE WHERE LINEID='" & CStr(strCurlineID) & "' AND CSTIMETABLEID='" & CStr(CorCSPlanID) & "'  order by ID "
            DaTab = ReadData(Str)

            Dim Point As Integer = 1
            ReDim CSTrainsAndDrivers.CorCSLinkTrains(0)
            For i = 1 To DaTab.Rows.Count
                Dim TempCSLinkTrain As New CSLinkTrain
                If CInt(DaTab.Rows(i - 1).Item("DateNo").ToString.Trim) > 0 Then
                    TempCSLinkTrain.CSTrainID = Point
                    Point += 1
                Else
                    TempCSLinkTrain.CSTrainID = -1
                End If
                TempCSLinkTrain.OutputCheCi = DaTab.Rows(i - 1).Item("TrainNo").ToString.Trim
                TempCSLinkTrain.StartStaName = DaTab.Rows(i - 1).Item("StartStaName").ToString.Trim
                TempCSLinkTrain.StartTime = CInt(DaTab.Rows(i - 1).Item("StartTime").ToString.Trim)
                TempCSLinkTrain.StartStaID = CSFromStaNameToStaIDByStationinf(TempCSLinkTrain.StartStaName, DaTab.Rows(i - 1).Item("STASEQ1").ToString.Trim)
                TempCSLinkTrain.STASEQ1 = DaTab.Rows(i - 1).Item("STASEQ1").ToString.Trim
                TempCSLinkTrain.STASEQ2 = DaTab.Rows(i - 1).Item("STASEQ2").ToString.Trim
                TempCSLinkTrain.EndStaName = DaTab.Rows(i - 1).Item("EndStaName").ToString.Trim
                TempCSLinkTrain.EndTime = CInt(DaTab.Rows(i - 1).Item("EndTime").ToString.Trim)
                TempCSLinkTrain.EndStaID = CSFromStaNameToStaIDByStationinf(TempCSLinkTrain.EndStaName, DaTab.Rows(i - 1).Item("STASEQ2").ToString.Trim)
                TempCSLinkTrain.nCheDiID = CInt(DaTab.Rows(i - 1).Item("CheDiID").ToString.Trim)
                TempCSLinkTrain.nPathID1 = CInt(DaTab.Rows(i - 1).Item("Path1").ToString.Trim)
                TempCSLinkTrain.nPathID2 = CInt(DaTab.Rows(i - 1).Item("Path2").ToString.Trim)
                TempCSLinkTrain.nTrainID = CInt(DaTab.Rows(i - 1).Item("TrainID").ToString.Trim)
                TempCSLinkTrain.UpOrDown = CInt(DaTab.Rows(i - 1).Item("UpOrDown").ToString.Trim)
                TempCSLinkTrain.ZFTime = CInt(DaTab.Rows(i - 1).Item("ZFTime").ToString.Trim)
                TempCSLinkTrain.CulStartTime = AddLitterTime(TempCSLinkTrain.StartTime)
                TempCSLinkTrain.CulEndTime = AddLitterTime(TempCSLinkTrain.EndTime)
                TempCSLinkTrain.distance = Val(DaTab.Rows(i - 1).Item("distance").ToString.Trim)
                TempCSLinkTrain.sCheDiHao = DaTab.Rows(i - 1).Item("VEHICLENO").ToString.Trim
                If DaTab.Rows(i - 1).Item("DriverNo").ToString = "##" Then
                    TempCSLinkTrain.IsLinked = False
                Else
                    TempCSLinkTrain.IsLinked = True
                End If
                If CInt(DaTab.Rows(i - 1).Item("DateNo").ToString.Trim) > 0 Then
                    TempCSLinkTrain.IsDeadHeading = False
                    ReDim Preserve CSTrainsAndDrivers.CorCSLinkTrains(UBound(CSTrainsAndDrivers.CorCSLinkTrains) + 1)
                    CSTrainsAndDrivers.CorCSLinkTrains(UBound(CSTrainsAndDrivers.CorCSLinkTrains)) = TempCSLinkTrain
                Else
                    TempCSLinkTrain.IsDeadHeading = True
                End If

                '赋给驾驶员
                For j = 1 To UBound(CSTrainsAndDrivers.CorCSDrivers)
                    flag = 0
                    If CSTrainsAndDrivers.CorCSDrivers(j).CSdriverNo = DaTab.Rows(i - 1).Item("DriverNo").ToString Then
                        flag = j
                        Exit For
                    End If
                Next

                If flag > 0 Then
                    ReDim Preserve CSTrainsAndDrivers.CorCSDrivers(flag).CSLinkTrain(UBound(CSTrainsAndDrivers.CorCSDrivers(flag).CSLinkTrain) + 1)
                    CSTrainsAndDrivers.CorCSDrivers(flag).CSLinkTrain(UBound(CSTrainsAndDrivers.CorCSDrivers(flag).CSLinkTrain)) = TempCSLinkTrain
                End If
            Next
            CSTrainsAndDrivers.CorMorningDrivers = New List(Of CSDriver)
            CSTrainsAndDrivers.CorDayDrivers = New List(Of CSDriver)
            CSTrainsAndDrivers.CorCDayDrivers = New List(Of CSDriver)
            CSTrainsAndDrivers.CorNightDrivers = New List(Of CSDriver)
            CSTrainsAndDrivers.CorOtherDrivers = New List(Of CSDriver)
            For Each dri As CSDriver In CSTrainsAndDrivers.CorCSDrivers
                If dri IsNot Nothing Then
                    Select Case dri.DutySort
                        Case "早班"
                            CSTrainsAndDrivers.CorMorningDrivers.Add(dri)
                        Case "白班"
                            CSTrainsAndDrivers.CorDayDrivers.Add(dri)
                        Case "日勤班"
                            CSTrainsAndDrivers.CorCDayDrivers.Add(dri)
                        Case "夜班"
                            CSTrainsAndDrivers.CorNightDrivers.Add(dri)
                        Case Else
                            CSTrainsAndDrivers.CorOtherDrivers.Add(dri)
                    End Select
                End If
            Next
        End If
    End Sub

    'Public Sub ReadTrainingInfo()
    '    Dim str As String = "select * from cs_traininginfo t where t.cstimetableid='" & CStr(strQCurCSPlanID) & "' and t.istraining='True'"
    '    Dim tab As System.Data.DataTable = ReadData(str)
    '    If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
    '        For Each row As DataRow In tab.Rows
    '            Dim driNo As String = row.Item("DriverNo").ToString
    '            Dim DutySort As String = row.Item("DutySort").ToString
    '            Dim dri As CSDriver = Nothing
    '            If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
    '                For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
    '                    If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo = driNo AndAlso CSTrainsAndDrivers.CSDrivers(i).DutySort = DutySort Then
    '                        dri = CSTrainsAndDrivers.CSDrivers(i)
    '                        Exit For
    '                    End If
    '                Next
    '            End If
    '            If dri Is Nothing Then
    '                If CSTrainsAndDrivers.PreParedTrainDrivers IsNot Nothing AndAlso CSTrainsAndDrivers.PreParedTrainDrivers.Count > 0 Then
    '                    dri = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
    '                                                                           Return value.CSdriverNo = driNo AndAlso value.DutySort = DutySort
    '                                                                       End Function)
    '                    If dri Is Nothing Then
    '                        If CSTrainsAndDrivers.PreParedDutyDrivers IsNot Nothing AndAlso CSTrainsAndDrivers.PreParedDutyDrivers.Count > 0 Then
    '                            dri = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
    '                                                                                  Return value.CSdriverNo = driNo AndAlso value.DutySort = DutySort
    '                                                                              End Function)
    '                        End If
    '                    End If
    '                End If
    '            End If
    '            If dri IsNot Nothing Then
    '                dri.IsTraining = True
    '                dri.TrainingTime = CInt(row.Item("trainingtime").ToString)
    '            End If
    '        Next
    '    End If
    'End Sub

    Public Sub ReadCorPrepareDrivers(ByVal CorCSPlanName)
        Dim CorCSPlanID As String = GetCSPlanIDFromCSPlanName(CorCSPlanName)
        CSTrainsAndDrivers.CorPreParedDutyDrivers.Clear()
        CSTrainsAndDrivers.CorPreParedTrainDrivers.Clear()
        tempTable = New Data.DataTable
        tempTable = ReadData("select * from cs_result_preparedtraininf t WHERE t.LINEID='" & CStr(strCurlineID) & "' and cstimetableid='" & CorCSPlanID & "' order by t.dutysort,t.name")
        If tempTable.Rows.Count > 0 Then
            For Each row As DataRow In tempTable.Rows
                Dim tempDri As New CSDriver(0)
                tempDri.CSdriverNo = row.Item("Name").ToString.Trim
                tempDri.DutySort = row.Item("DutySort").ToString.Trim
                tempDri.BelongArea = row.Item("Remark").ToString.Trim
                tempDri.OutPutCSdriverNo = row.Item("outputcsdriverno").ToString.Trim
                tempDri.IsPrepareDri = True
                Dim tempTrain As New CSLinkTrain()
                tempTrain.StartStaName = row.Item("place").ToString.Trim
                tempTrain.StartTime = row.Item("starttime").ToString.Trim
                tempTrain.EndStaName = row.Item("place").ToString.Trim
                tempTrain.EndTime = row.Item("endtime").ToString.Trim
                tempDri.AddTrain(tempTrain)
                CSTrainsAndDrivers.CorPreParedTrainDrivers.Add(tempDri)
            Next
        End If
        tempTable = ReadData("select * from cs_result_prepareddutyinf t WHERE t.LINEID='" & CStr(strCurlineID) & "' and cstimetableid='" & CorCSPlanID & "' order by t.dutysort,t.name")
        If tempTable.Rows.Count > 0 Then
            For Each row As DataRow In tempTable.Rows
                Dim tempDri As New CSDriver(0)
                tempDri.CSdriverNo = row.Item("Name").ToString.Trim
                tempDri.DutySort = row.Item("DutySort").ToString.Trim
                tempDri.BelongArea = row.Item("Remark").ToString.Trim
                tempDri.OutPutCSdriverNo = row.Item("outputcsdriverno").ToString.Trim
                tempDri.IsPrepareDri = True
                Dim tempTrain As New CSLinkTrain()
                tempTrain.StartStaName = row.Item("place").ToString.Trim
                tempTrain.StartTime = row.Item("starttime").ToString.Trim
                tempTrain.EndStaName = row.Item("place").ToString.Trim
                tempTrain.EndTime = row.Item("endtime").ToString.Trim
                tempDri.AddTrain(tempTrain)
                CSTrainsAndDrivers.CorPreParedDutyDrivers.Add(tempDri)
            Next
        End If
        tempTable.Dispose()
    End Sub

    Public Sub ReadCorCSPlan()
        CSTrainsAndDrivers.PostiveCorCSPlans = New List(Of CorCSPlan)
        Dim str As String = "select * from cs_amdrivercorrespond t where t.adrivertimetableid='" & CSTrainsAndDrivers.CorCSTimetableID & "' and t.mdrivertimetableid='" & CStr(strQCurCSPlanID) & "'"
        Dim tab As Data.DataTable = ReadData(str)
        If tab IsNot Nothing Then
            For Each row As DataRow In tab.Rows
                Dim MdriverNo As String = row.Item("Mdriverno").ToString.Trim
                Dim AdriverNo As String = row.Item("Adriverno").ToString.Trim
                Dim MDri As CSDriver = CSTrainsAndDrivers.MorningDrivers.Find(Function(value As CSDriver)
                                                                                  Return value.CSdriverNo = MdriverNo
                                                                              End Function)
                If MDri Is Nothing Then
                    MDri = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                           Return (value.CSdriverNo = MdriverNo AndAlso value.DutySort = "早班")
                                                                       End Function)
                    If MDri Is Nothing Then
                        MDri = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                Return (value.CSdriverNo = MdriverNo AndAlso value.DutySort = "早班")
                                                                            End Function)
                    End If
                End If
                Dim Adri As CSDriver = CSTrainsAndDrivers.CorNightDrivers.Find(Function(value As CSDriver)
                                                                                   Return value.CSdriverNo = AdriverNo
                                                                               End Function)
                If Adri Is Nothing Then
                    Adri = CSTrainsAndDrivers.CorPreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                              Return (value.CSdriverNo = AdriverNo AndAlso value.DutySort = "夜班")
                                                                          End Function)
                    If Adri Is Nothing Then
                        Adri = CSTrainsAndDrivers.CorPreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                   Return (value.CSdriverNo = AdriverNo AndAlso value.DutySort = "夜班")
                                                                               End Function)
                    End If
                End If
                If MDri IsNot Nothing AndAlso Adri IsNot Nothing Then
                    Dim tempcorplan As New CorCSPlan(Adri, MDri)
                    CSTrainsAndDrivers.PostiveCorCSPlans.Add(tempcorplan)
                End If
            Next
        End If
        CSTrainsAndDrivers.NegtiveCorCSPlans = New List(Of CorCSPlan)
        str = "select * from cs_amdrivercorrespond t where t.adrivertimetableid='" & CStr(strQCurCSPlanID) & "' and t.mdrivertimetableid='" & CSTrainsAndDrivers.CorCSTimetableID & "'"
        tab = ReadData(str)
        If tab IsNot Nothing Then
            For Each row As DataRow In tab.Rows
                Dim MdriverNo As String = row.Item("Mdriverno").ToString.Trim
                Dim AdriverNo As String = row.Item("Adriverno").ToString.Trim
                Dim MDri As CSDriver = CSTrainsAndDrivers.CorMorningDrivers.Find(Function(value As CSDriver)
                                                                                     Return value.CSdriverNo = MdriverNo
                                                                                 End Function)
                If MDri Is Nothing Then
                    MDri = CSTrainsAndDrivers.CorPreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                              Return (value.CSdriverNo = MdriverNo AndAlso value.DutySort = "早班")
                                                                          End Function)
                    If MDri Is Nothing Then
                        MDri = CSTrainsAndDrivers.CorPreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                   Return (value.CSdriverNo = MdriverNo AndAlso value.DutySort = "早班")
                                                                               End Function)
                    End If
                End If
                Dim Adri As CSDriver = CSTrainsAndDrivers.NightDrivers.Find(Function(value As CSDriver)
                                                                                Return value.CSdriverNo = AdriverNo
                                                                            End Function)
                If Adri Is Nothing Then
                    Adri = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                           Return (value.CSdriverNo = AdriverNo AndAlso value.DutySort = "夜班")
                                                                       End Function)
                    If Adri Is Nothing Then
                        Adri = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                Return (value.CSdriverNo = AdriverNo AndAlso value.DutySort = "夜班")
                                                                            End Function)
                    End If
                End If
                If MDri IsNot Nothing AndAlso Adri IsNot Nothing Then
                    Dim tempcorplan As New CorCSPlan(Adri, MDri)
                    CSTrainsAndDrivers.NegtiveCorCSPlans.Add(tempcorplan)
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' 形成衔接计划
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub FormTotalCSPlan()
        CSTrainsAndDrivers.PostiveCorCSPlans.Clear()
        CSTrainsAndDrivers.NegtiveCorCSPlans.Clear()
        CSTrainsAndDrivers.PostiveCorCSPlans = FormCorCSPlan(CSTrainsAndDrivers.CorNightDrivers, CSTrainsAndDrivers.MorningDrivers)               '形成不同图之间衔接的乘务计划
        Dim corPrepareDutyDri As New List(Of CSDriver)
        Dim PrepareDutyDri As New List(Of CSDriver)
        For Each dri As CSDriver In CSTrainsAndDrivers.CorPreParedDutyDrivers
            If dri IsNot Nothing AndAlso dri.DutySort = "夜班" Then
                corPrepareDutyDri.Add(dri)
            End If
        Next
        For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
            If dri IsNot Nothing AndAlso dri.DutySort = "早班" Then
                PrepareDutyDri.Add(dri)
            End If
        Next
        CSTrainsAndDrivers.PostiveCorCSPlans.AddRange(FormCorCSPlan(corPrepareDutyDri, PrepareDutyDri))
        corPrepareDutyDri.Clear()
        PrepareDutyDri.Clear()
        For Each dri As CSDriver In CSTrainsAndDrivers.CorPreParedTrainDrivers
            If dri IsNot Nothing AndAlso dri.DutySort = "夜班" Then
                corPrepareDutyDri.Add(dri)
            End If
        Next
        For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
            If dri IsNot Nothing AndAlso dri.DutySort = "早班" Then
                PrepareDutyDri.Add(dri)
            End If
        Next
        CSTrainsAndDrivers.PostiveCorCSPlans.AddRange(FormCorCSPlan(corPrepareDutyDri, PrepareDutyDri))
        CSTrainsAndDrivers.NegtiveCorCSPlans = FormCorCSPlan(CSTrainsAndDrivers.NightDrivers, CSTrainsAndDrivers.CorMorningDrivers)               '形成不同图之间衔接的乘务计划
        corPrepareDutyDri.Clear()
        PrepareDutyDri.Clear()
        For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
            If dri IsNot Nothing AndAlso dri.DutySort = "夜班" Then
                corPrepareDutyDri.Add(dri)
            End If
        Next
        For Each dri As CSDriver In CSTrainsAndDrivers.CorPreParedDutyDrivers
            If dri IsNot Nothing AndAlso dri.DutySort = "早班" Then
                PrepareDutyDri.Add(dri)
            End If
        Next
        CSTrainsAndDrivers.NegtiveCorCSPlans.AddRange(FormCorCSPlan(corPrepareDutyDri, PrepareDutyDri))
        corPrepareDutyDri.Clear()
        PrepareDutyDri.Clear()
        For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
            If dri IsNot Nothing AndAlso dri.DutySort = "夜班" Then
                corPrepareDutyDri.Add(dri)
            End If
        Next
        For Each dri As CSDriver In CSTrainsAndDrivers.CorPreParedTrainDrivers
            If dri IsNot Nothing AndAlso dri.DutySort = "早班" Then
                PrepareDutyDri.Add(dri)
            End If
        Next
        CSTrainsAndDrivers.NegtiveCorCSPlans.AddRange(FormCorCSPlan(corPrepareDutyDri, PrepareDutyDri))
    End Sub

    Public Function FormCorCSPlan(ByVal NightDrivers As List(Of CSDriver), ByVal MorningDrivers As List(Of CSDriver), Optional ByVal Style As String = "自身夜早班衔接", Optional ByVal kmAvg As Boolean = True) As List(Of CorCSPlan)
        FormCorCSPlan = New List(Of CorCSPlan)
        Dim RuKuDrivers As New List(Of InDepotDriverList)
        Dim ChuKuDrivers As New List(Of InDepotDriverList)
        For Each dri As CSDriver In NightDrivers
            '如果有与该夜班接续的早班，则跳过
            If Style = "自身夜早班衔接" Then
                If dri.LinkedMorDriver IsNot Nothing Then
                    Continue For
                End If
            ElseIf Style = "自身夜班被动衔接" Then
                If CSTrainsAndDrivers.NegtiveCorCSPlans IsNot Nothing Then
                    Dim corplan As CorCSPlan = CSTrainsAndDrivers.NegtiveCorCSPlans.Find(Function(value As CorCSPlan)
                                                                                             Return value.NightDriver Is dri
                                                                                         End Function)
                    If corplan IsNot Nothing AndAlso corplan.MorningDriver IsNot Nothing Then
                        Continue For
                    End If
                End If
            ElseIf Style = "自身早班主动衔接" Then
                If CSTrainsAndDrivers.PostiveCorCSPlans IsNot Nothing Then
                    Dim corplan As CorCSPlan = CSTrainsAndDrivers.PostiveCorCSPlans.Find(Function(value As CorCSPlan)
                                                                                             Return value.NightDriver Is dri
                                                                                         End Function)
                    If corplan IsNot Nothing AndAlso corplan.MorningDriver IsNot Nothing Then
                        Continue For
                    End If
                End If
            End If
            '如果没有接续的早班，继续：
            Dim InDepotName As String = dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName   '夜班终点站
            Dim tdri As CSDriver = dri
            Dim AreaName As String
            If dri.BelongArea.Trim = "" Then
                AreaName = CSTrainsAndDrivers.AreaInfoS.Find(Function(value As AreaInfo)
                                                                 Return value.ForDutySorts.Contains(tdri.DutySort) AndAlso value.OnDutyPlaces.Contains(tdri.CSLinkTrain(1).StartStaName)
                                                             End Function).AreaName
            Else
                AreaName = dri.BelongArea
            End If
            '如果入库集合中有index的离站是该dri终点站，且区域一致的，加入该index的集合中
            Dim index As Integer = RuKuDrivers.FindIndex(Function(value As InDepotDriverList)
                                                             Return value.DepotName = InDepotName AndAlso value.AreaName = AreaName
                                                         End Function)
            If index <> -1 Then
                RuKuDrivers(index).CSRukuDrivers.Add(dri)
            Else
                '如果存在于异站衔接中，也加入该站的入库车集合
                Dim ifnewOne As Boolean = True
                For Each indepot As InDepotDriverList In RuKuDrivers
                    For i As Integer = 0 To notSameStationInfo.Count - 1
                        If notSameStationInfo(i).Contains(indepot.DepotName) And notSameStationInfo(i).Contains(InDepotName) And indepot.DepotName <> InDepotName Then
                            indepot.CSRukuDrivers.Add(dri)
                            ifnewOne = False
                            Exit For
                        End If
                    Next
                    If ifnewOne = False Then
                        Exit For
                    End If
                Next
                '如果是新的终点站，新建一个入库集
                If ifnewOne = True Then
                    Dim tempInDrilist As New InDepotDriverList(InDepotName, AreaName)
                    tempInDrilist.CSRukuDrivers.Add(dri)
                    RuKuDrivers.Add(tempInDrilist)
                End If
            End If
        Next

        For Each dri As CSDriver In MorningDrivers
            If Style = "自身夜早班衔接" Then
                If dri.LinkedNightDriver IsNot Nothing Then
                    Continue For
                End If
            ElseIf Style = "自身夜班被动衔接" Then
                If CSTrainsAndDrivers.NegtiveCorCSPlans IsNot Nothing Then
                    Dim corplan As CorCSPlan = CSTrainsAndDrivers.NegtiveCorCSPlans.Find(Function(value As CorCSPlan)
                                                                                             Return value.MorningDriver Is dri
                                                                                         End Function)
                    If corplan IsNot Nothing AndAlso corplan.NightDriver IsNot Nothing Then
                        Continue For
                    End If
                End If
            ElseIf Style = "自身早班主动衔接" Then
                If CSTrainsAndDrivers.PostiveCorCSPlans IsNot Nothing Then
                    Dim corplan As CorCSPlan = CSTrainsAndDrivers.PostiveCorCSPlans.Find(Function(value As CorCSPlan)
                                                                                             Return value.MorningDriver Is dri
                                                                                         End Function)
                    If corplan IsNot Nothing AndAlso corplan.NightDriver IsNot Nothing Then
                        Continue For
                    End If
                End If
            End If
            'If dri.LinkedNightDriver Is Nothing Then
            Dim OutDepotName As String = dri.CSLinkTrain(1).StartStaName   '早班始发站
            Dim tdri As CSDriver = dri
            Dim AreaName As String
            If dri.BelongArea.Trim = "" Then
                AreaName = CSTrainsAndDrivers.AreaInfoS.Find(Function(value As AreaInfo)
                                                                 Return value.ForDutySorts.Contains(tdri.DutySort) AndAlso value.OnDutyPlaces.Contains(tdri.CSLinkTrain(UBound(tdri.CSLinkTrain)).EndStaName)
                                                             End Function).AreaName
            Else
                AreaName = dri.BelongArea
            End If
            Dim index As Integer = ChuKuDrivers.FindIndex(Function(value As InDepotDriverList)
                                                              Return value.DepotName = OutDepotName AndAlso value.AreaName = AreaName
                                                          End Function)
            If index <> -1 Then
                ChuKuDrivers(index).CSRukuDrivers.Add(dri)
            Else
                Dim ifnewOne As Boolean = True
                For Each indepot As InDepotDriverList In ChuKuDrivers
                    For i As Integer = 0 To notSameStationInfo.Count - 1
                        If notSameStationInfo(i).Contains(indepot.DepotName) And notSameStationInfo(i).Contains(OutDepotName) And indepot.DepotName <> OutDepotName Then
                            indepot.CSRukuDrivers.Add(dri)
                            ifnewOne = False
                            Exit For
                        End If
                    Next
                    If ifnewOne = False Then
                        Exit For
                    End If
                Next
                If ifnewOne = True Then
                    Dim tempOutDrilist As New InDepotDriverList(OutDepotName, AreaName)
                    tempOutDrilist.CSRukuDrivers.Add(dri)
                    ChuKuDrivers.Add(tempOutDrilist)
                End If
            End If
            '将在相同站结束和开始的夜班、早班集合组合
        Next
        For Each rukuDri As InDepotDriverList In RuKuDrivers
            For Each chukudri As InDepotDriverList In ChuKuDrivers
                If chukudri.AreaName = rukuDri.AreaName Then
                    If chukudri.DepotName = rukuDri.DepotName Then
                        FormCorCSPlan.AddRange(FormCorCSPlan(rukuDri, chukudri, kmAvg))
                    Else
                        For i As Integer = 0 To notSameStationInfo.Count - 1
                            If notSameStationInfo(i).Contains(chukudri.DepotName) And notSameStationInfo(i).Contains(rukuDri.DepotName) Then
                                FormCorCSPlan.AddRange(FormCorCSPlan(rukuDri, chukudri, kmAvg))
                                Exit For
                            End If
                        Next
                    End If
                End If
            Next
        Next
    End Function

    Public Function FormCorCSPlan(ByVal RukuDirvers As InDepotDriverList, ByVal ChukuDrivers As InDepotDriverList, Optional ByVal gonglishu As Boolean = True) As List(Of CorCSPlan)
        Dim tempcsplan As New List(Of CorCSPlan)
        If RukuDirvers.CSRukuDrivers.Count > 1 Then            '按早入库时间排序，结束时间从晚到早
            For i As Integer = 0 To RukuDirvers.CSRukuDrivers.Count - 2
                For j As Integer = i + 1 To RukuDirvers.CSRukuDrivers.Count - 1
                    If AddLitterTime(RukuDirvers.CSRukuDrivers(j).EndEorkTime) > AddLitterTime(RukuDirvers.CSRukuDrivers(i).EndEorkTime) Then
                        Dim tempdri As CSDriver = RukuDirvers.CSRukuDrivers(i)
                        RukuDirvers.CSRukuDrivers(i) = RukuDirvers.CSRukuDrivers(j)
                        RukuDirvers.CSRukuDrivers(j) = tempdri
                    End If
                Next
            Next
        End If
        If ChukuDrivers.CSRukuDrivers.Count > 1 Then             '按照出库时间排序，开始时间从晚到早
            For i As Integer = 0 To ChukuDrivers.CSRukuDrivers.Count - 2
                For j As Integer = i + 1 To ChukuDrivers.CSRukuDrivers.Count - 1
                    If AddLitterTime(ChukuDrivers.CSRukuDrivers(j).BeginWorkTime) > AddLitterTime(ChukuDrivers.CSRukuDrivers(i).BeginWorkTime) Then
                        Dim tempdri As CSDriver = ChukuDrivers.CSRukuDrivers(i)
                        ChukuDrivers.CSRukuDrivers(i) = ChukuDrivers.CSRukuDrivers(j)
                        ChukuDrivers.CSRukuDrivers(j) = tempdri
                    End If
                Next
            Next
        End If
        If gonglishu Then
            ' 接续站相同，夜班起始站和早班终点站也要相同，满足公里约束
            If RukuDirvers.CSRukuDrivers.Count > 0 AndAlso ChukuDrivers.CSRukuDrivers.Count > 0 Then
                For i As Integer = 0 To RukuDirvers.CSRukuDrivers.Count - 1
                    For j As Integer = 0 To ChukuDrivers.CSRukuDrivers.Count - 1
                        If i <= RukuDirvers.CSRukuDrivers.Count - 1 AndAlso j <= ChukuDrivers.CSRukuDrivers.Count - 1 Then
                            If RukuDirvers.CSRukuDrivers(i).DriveDistance + ChukuDrivers.CSRukuDrivers(j).DriveDistance > CS_MorningMaxLength + CS_NightMaxLength Then
                                Continue For
                            Else
                                If RukuDirvers.CSRukuDrivers(i).CSLinkTrain(1).StartStaName = ChukuDrivers.CSRukuDrivers(j).CSLinkTrain(UBound(ChukuDrivers.CSRukuDrivers(j).CSLinkTrain)).EndStaName Then
                                    Dim selectMDri As CSDriver = RukuDirvers.CSRukuDrivers(i)
                                    Dim selectNDri As CSDriver = ChukuDrivers.CSRukuDrivers(j)
                                    Dim tempcorplan As New CorCSPlan(selectNDri, selectMDri)
                                    tempcsplan.Add(tempcorplan)
                                    RukuDirvers.CSRukuDrivers.Remove(selectNDri)
                                    ChukuDrivers.CSRukuDrivers.Remove(selectMDri)
                                    Continue For
                                End If
                            End If
                        End If
                    Next
                Next
            End If
        End If
        If RukuDirvers.CSRukuDrivers.Count > 0 AndAlso ChukuDrivers.CSRukuDrivers.Count > 0 Then
            For i As Integer = RukuDirvers.CSRukuDrivers.Count - 1 To 0 Step -1
                Dim selectNDri As CSDriver = RukuDirvers.CSRukuDrivers(i)
                Dim selectMDri As CSDriver = Nothing
                For j As Integer = ChukuDrivers.CSRukuDrivers.Count - 1 To 0 Step -1
                    If selectNDri.CSLinkTrain(1).StartStaName = ChukuDrivers.CSRukuDrivers(j).CSLinkTrain(UBound(ChukuDrivers.CSRukuDrivers(j).CSLinkTrain)).EndStaName Then
                        selectMDri = ChukuDrivers.CSRukuDrivers(j)
                        Exit For
                    End If
                Next
                If selectMDri IsNot Nothing Then
                    Dim tempcorplan As New CorCSPlan(selectNDri, selectMDri)
                    tempcsplan.Add(tempcorplan)
                    RukuDirvers.CSRukuDrivers.Remove(selectNDri)
                    ChukuDrivers.CSRukuDrivers.Remove(selectMDri)
                End If
            Next
        End If
            'If gonglishu Then
            '    '============先安排违反公里的夜早班
            '    If RukuDirvers.CSRukuDrivers.Count > 0 AndAlso ChukuDrivers.CSRukuDrivers.Count > 0 Then
            '        While True
            '            Dim index As Integer = -1
            '            For i As Integer = 0 To RukuDirvers.CSRukuDrivers.Count - 1
            '                If i <= RukuDirvers.CSRukuDrivers.Count - 1 AndAlso i <= ChukuDrivers.CSRukuDrivers.Count - 1 Then
            '                    If RukuDirvers.CSRukuDrivers(i).DriveDistance + ChukuDrivers.CSRukuDrivers(i).DriveDistance > CS_MorningMaxLength + CS_NightMaxLength Then
            '                        index = i
            '                        Exit For
            '                    End If
            '                End If
            '            Next
            '            If index = -1 Then
            '                Exit While
            '            Else
            '                Dim selectMDri As CSDriver = ChukuDrivers.CSRukuDrivers(index)
            '                Dim selectNDri As CSDriver = Nothing
            '                Dim leastindex As Integer = index - 2
            '                If leastindex < 0 Then
            '                    leastindex = 0
            '                End If
            '                '从远端搜索？
            '                For i As Integer = RukuDirvers.CSRukuDrivers.Count - 1 To leastindex Step -1
            '                    If RukuDirvers.CSRukuDrivers(i).DriveDistance + selectMDri.DriveDistance <= CS_MorningMaxLength + CS_NightMaxLength Then
            '                        selectNDri = RukuDirvers.CSRukuDrivers(i)
            '                        Exit For
            '                    End If
            '                Next
            '                If selectNDri IsNot Nothing Then
            '                    Dim tempcorplan As New CorCSPlan(selectNDri, selectMDri)
            '                    tempcsplan.Add(tempcorplan)
            '                    RukuDirvers.CSRukuDrivers.Remove(selectNDri)
            '                    ChukuDrivers.CSRukuDrivers.Remove(selectMDri)
            '                Else
            '                    Exit While
            '                End If
            '            End If
            '        End While
            '    End If

            '    For i As Integer = RukuDirvers.CSRukuDrivers.Count - 1 To 0 Step -1
            '        Dim selectNDri As CSDriver = RukuDirvers.CSRukuDrivers(i)
            '        Dim selectMDri As CSDriver = Nothing
            '        For j As Integer = ChukuDrivers.CSRukuDrivers.Count - 1 To 0 Step -1
            '            If ChukuDrivers.CSRukuDrivers(j).DriveDistance + selectNDri.DriveDistance <= CS_MorningMaxLength + CS_NightMaxLength Then
            '                selectMDri = ChukuDrivers.CSRukuDrivers(j)
            '                Exit For
            '            End If
            '        Next
            '        If selectMDri IsNot Nothing Then
            '            Dim tempcorplan As New CorCSPlan(selectNDri, selectMDri)
            '            tempcsplan.Add(tempcorplan)
            '            RukuDirvers.CSRukuDrivers.Remove(selectNDri)
            '            ChukuDrivers.CSRukuDrivers.Remove(selectMDri)
            '        End If
            '    Next
            'End If

            'If RukuDirvers.CSRukuDrivers.Count > 0 AndAlso ChukuDrivers.CSRukuDrivers.Count > 0 Then
            '    For i As Integer = RukuDirvers.CSRukuDrivers.Count - 1 To 0 Step -1
            '        Dim selectNDri As CSDriver = RukuDirvers.CSRukuDrivers(i)
            '        Dim selectMDri As CSDriver = Nothing
            '        For j As Integer = ChukuDrivers.CSRukuDrivers.Count - 1 To 0 Step -1
            '            selectMDri = ChukuDrivers.CSRukuDrivers(j)
            '            Exit For
            '        Next
            '        If selectMDri IsNot Nothing Then
            '            Dim tempcorplan As New CorCSPlan(selectNDri, selectMDri)
            '            tempcsplan.Add(tempcorplan)
            '            RukuDirvers.CSRukuDrivers.Remove(selectNDri)
            '            ChukuDrivers.CSRukuDrivers.Remove(selectMDri)
            '        End If
            '    Next
            'End If
            Return tempcsplan
    End Function

    Public Sub LoadSectionLengthInfo()
        SectionDistanceList = New List(Of SectionDistance)
        Dim tab As DataTable = ReadData("select * from cs_sh_trainlength t where t.lineid='" & strCurlineID & "'")
        If tab.Rows.Count > 0 Then
            For Each r As DataRow In tab.Rows
                Dim temp As New SectionDistance(r.Item("STARTSTA").ToString.Trim, r.Item("ENDSTA").ToString.Trim, Val(r.Item("LENGTH").ToString))
                SectionDistanceList.Add(temp)
            Next
        End If
    End Sub

    ''' <summary>
    ''' 根据当前列车获取其联合任务
    ''' </summary>
    ''' <param name="train"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMergedTrain(ByVal train As CSLinkTrain) As MergedCSLinkTrain        '获取绑定的列车
        GetMergedTrain = Nothing
        If UBound(CSTrainsAndDrivers.MergedCSLinkTrains) > 0 Then
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                If UBound(CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains) > 0 Then
                    For j As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains)
                        If CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains(j) Is train Then
                            GetMergedTrain = CSTrainsAndDrivers.MergedCSLinkTrains(i)
                            Exit Function
                        End If
                    Next
                End If
            Next
        End If
    End Function
    ''' <summary>
    ''' 添加一个新司机mergelinkd
    ''' </summary>
    ''' <param name="DutySort"></param>
    ''' <param name="termer"></param>
    ''' <remarks></remarks>
    Public Sub AddANewDriverforMerged(ByVal DutySort As String, ByVal termer As MergedCSLinkTrain, Optional ByVal dutyWork As String = "")
        Dim SelDutySort As String = DutySort
        If CSTrainsAndDrivers.CSDrivers Is Nothing = True OrElse UBound(CSTrainsAndDrivers.CSDrivers) = 0 Then
            ReDim CSTrainsAndDrivers.CSDrivers(0)
            ReDim Preserve CSTrainsAndDrivers.CSDrivers(1)
            CSTrainsAndDrivers.CSDrivers(1) = New CSDriver()
            CSTrainsAndDrivers.CSDrivers(1).CSDriverID = 1
            CSTrainsAndDrivers.CSDrivers(1).dutyWork = dutyWork
            CSTrainsAndDrivers.CSDrivers(1).CSdriverNo = DutySort & "01"
            CSTrainsAndDrivers.CSDrivers(1).DutySort = SelDutySort
            CSTrainsAndDrivers.CSDrivers(1).State = "班中"
            CSTrainsAndDrivers.CSDrivers(1).AddMergedTrain(termer)
        Else
            ReDim Preserve CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers) + 1)
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)) = New CSDriver()
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID = UBound(CSTrainsAndDrivers.CSDrivers)
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).dutyWork = dutyWork
            Dim DriverNO As String = "01"
            While True
                Dim IfExist As Boolean = False
                For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                    If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo = DutySort & DriverNO Then
                        IfExist = True
                        Exit For
                    End If
                Next
                If IfExist Then
                    DriverNO = Format(CInt(DriverNO) + 1, "00")
                Else
                    Exit While
                End If
            End While
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSdriverNo = DutySort & DriverNO
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).DutySort = SelDutySort
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).State = "班中"
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).AddMergedTrain(termer)
        End If
        Select Case SelDutySort
            Case "早班"
                CSTrainsAndDrivers.MorningDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
            Case "白班"
                CSTrainsAndDrivers.DayDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
            Case "日勤班"
                CSTrainsAndDrivers.CDayDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
            Case "夜班"
                CSTrainsAndDrivers.NightDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
            Case Else
                CSTrainsAndDrivers.OtherDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
        End Select
    End Sub

    ''' <summary>
    ''' 添加一个新司机
    ''' </summary>
    ''' <param name="DutySort"></param>
    ''' <param name="TrainID"></param>
    ''' <remarks></remarks>
    Public Sub AddANewDriver(ByVal DutySort As String, ByVal TrainID As Integer, Optional ByVal dutyWork As String = "")
        Dim SelDutySort As String = DutySort
        If CSTrainsAndDrivers.CSDrivers Is Nothing = True OrElse UBound(CSTrainsAndDrivers.CSDrivers) = 0 Then
            ReDim CSTrainsAndDrivers.CSDrivers(0)
            ReDim Preserve CSTrainsAndDrivers.CSDrivers(1)
            CSTrainsAndDrivers.CSDrivers(1) = New CSDriver()
            CSTrainsAndDrivers.CSDrivers(1).CSDriverID = 1
            CSTrainsAndDrivers.CSDrivers(1).dutyWork = dutyWork
            CSTrainsAndDrivers.CSDrivers(1).CSdriverNo = DutySort & "01"
            CSTrainsAndDrivers.CSDrivers(1).DutySort = SelDutySort
            CSTrainsAndDrivers.CSDrivers(1).State = "班中"
            Dim termer As MergedCSLinkTrain = GetMergedTrain(CSTrainsAndDrivers.CSLinkTrains(TrainID))
            CSTrainsAndDrivers.CSDrivers(1).AddMergedTrain(termer)
        Else
            ReDim Preserve CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers) + 1)
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)) = New CSDriver()
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID = UBound(CSTrainsAndDrivers.CSDrivers)
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).dutyWork = dutyWork
            Dim DriverNO As String = "01"
            While True
                Dim IfExist As Boolean = False
                For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                    If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo = DutySort & DriverNO Then
                        IfExist = True
                        Exit For
                    End If
                Next
                If IfExist Then
                    DriverNO = Format(CInt(DriverNO) + 1, "00")
                Else
                    Exit While
                End If
            End While
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSdriverNo = DutySort & DriverNO
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).DutySort = SelDutySort
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).State = "班中"
            Dim termer As MergedCSLinkTrain = GetMergedTrain(CSTrainsAndDrivers.CSLinkTrains(TrainID))
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).AddMergedTrain(termer)
        End If
        Select Case SelDutySort
            Case "早班"
                CSTrainsAndDrivers.MorningDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
            Case "白班"
                CSTrainsAndDrivers.DayDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
            Case "日勤班"
                CSTrainsAndDrivers.CDayDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
            Case "夜班"
                CSTrainsAndDrivers.NightDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
            Case Else
                CSTrainsAndDrivers.OtherDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
        End Select
    End Sub

    ''' <summary>
    ''' 指定起始时间清空计划
    ''' </summary>
    ''' <param name="StartTime"></param>
    ''' <remarks></remarks>
    Public Sub ClearPartPlan(ByVal StartTime As Integer)
        If UBound(CSTrainsAndDrivers.MergedCSLinkTrains) > 0 Then
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                If CSTrainsAndDrivers.MergedCSLinkTrains(i).CulStartTime >= AddLitterTime(StartTime) Then
                    For Each train As CSLinkTrain In CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains
                        If train IsNot Nothing Then
                            train.IsLinked = False
                        End If
                    Next
                End If
            Next
            If UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
                For i As Integer = UBound(CSTrainsAndDrivers.CSDrivers) To 1 Step -1
                    Dim dri As CSDriver = CSTrainsAndDrivers.CSDrivers(i)
                    For j As Integer = 1 To UBound(dri.CSLinkTrain)
                        If dri.CSLinkTrain(j).IsLinked = False Then
                            If j = 1 Then
                                RemoveDriver(dri)
                            Else
                                ReDim Preserve dri.CSLinkTrain(j - 1)
                                dri.RefreshState()
                            End If
                            Exit For
                        End If
                    Next
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' 指定任务清空计划
    ''' </summary>
    ''' <param name="dutysort"></param>
    ''' <remarks></remarks>
    Public Sub ClearDutyPlan(ByVal dutysort As String)
        If UBound(CSTrainsAndDrivers.MergedCSLinkTrains) > 0 Then
            If UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
                For i As Integer = UBound(CSTrainsAndDrivers.CSDrivers) To 1 Step -1
                    Dim dri As CSDriver = CSTrainsAndDrivers.CSDrivers(i)
                    If dri.DutySort = dutysort Then
                        For j As Integer = 1 To UBound(dri.CSLinkTrain)
                            dri.CSLinkTrain(j).IsLinked = False
                        Next
                        RemoveDriver(dri)
                    End If
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' 判断相应班种的任务是否编制结束
    ''' </summary>
    ''' <param name="Dutysort"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsDutyOverByDutySort(ByVal Dutysort) As Boolean
        Select Case Dutysort
            Case "早班"
                Dim ifallOff As Boolean = True
                If CSTrainsAndDrivers.MorningDrivers.Count >= CSAutoPlanPara.MoringDutyNum Then
                    For Each mdri As CSDriver In CSTrainsAndDrivers.MorningDrivers
                        If mdri.State <> "班后" Then
                            ifallOff = False
                        End If
                    Next
                Else
                    ifallOff = False
                End If
                Return ifallOff
            Case "白班"
                Dim ifallOff As Boolean = True
                If CSTrainsAndDrivers.DayDrivers.Count >= CSAutoPlanPara.DayDutyNum Then
                    For Each ddri As CSDriver In CSTrainsAndDrivers.DayDrivers
                        If ddri.State <> "班后" Then
                            ifallOff = False
                        End If
                    Next
                Else
                    ifallOff = False
                End If
                Return ifallOff
            Case "日勤班"
                Dim ifallOff As Boolean = True
                If CSTrainsAndDrivers.CDayDrivers.Count >= CSAutoPlanPara.CDayDutyNum Then
                    For Each cddri As CSDriver In CSTrainsAndDrivers.CDayDrivers
                        If cddri.State <> "班后" Then
                            ifallOff = False
                        End If
                    Next
                Else
                    ifallOff = False
                End If
                Return ifallOff
            Case "夜班"
                Dim ifallOff As Boolean = True
                If CSTrainsAndDrivers.NightDrivers.Count >= CSAutoPlanPara.NightDutyNum Then
                    For Each ndri As CSDriver In CSTrainsAndDrivers.NightDrivers
                        If ndri.State <> "班后" Then
                            ifallOff = False
                        End If
                    Next
                Else
                    ifallOff = False
                End If
                For Each tmer As MergedCSLinkTrain In CSTrainsAndDrivers.MergedCSLinkTrains
                    '==晚高峰出库任务段是否已处理完
                    If tmer IsNot Nothing AndAlso tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).FirstStation.IsYard AndAlso tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).CulEndTime > 12 * 3600 Then
                        If tmer.IsLinked = False Then
                            ifallOff = False
                        End If
                    End If
                Next
                Return ifallOff
            Case Else
                Return False
        End Select
    End Function

    ''' <summary>
    ''' 全部状态刷新
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AllRefreshState(Optional ByVal RefreshDirection As Boolean = True)
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
            For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                If dri IsNot Nothing Then
                    dri.RefreshState(, RefreshDirection)
                End If
            Next
        End If
    End Sub
    Public Sub AllOndutyState(ByVal dutysort As String)
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
            For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                If dri IsNot Nothing AndAlso dri.DutySort = dutysort Then
                    dri.State = "班中"
                End If
            Next
        End If
    End Sub

    '=========获取列车在merTrains中的索引号
    Public Function GetMerTrainID(ByVal train As CSLinkTrain) As Integer
        GetMerTrainID = -1
        If CSTrainsAndDrivers.MergedCSLinkTrains IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.MergedCSLinkTrains) > 0 AndAlso train IsNot Nothing Then
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                For j As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains)
                    If CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains(j) Is train Then
                        GetMerTrainID = i
                        Return GetMerTrainID
                    End If
                Next
            Next
        End If
    End Function

    '============整理合并的任务集合
    Public Sub ZhengliMerTrains()
        If CSTrainsAndDrivers.MergedCSLinkTrains IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.MergedCSLinkTrains) > 0 Then
            Dim ub As Integer = -1
            For i As Integer = UBound(CSTrainsAndDrivers.MergedCSLinkTrains) To 1 Step -1
                If CSTrainsAndDrivers.MergedCSLinkTrains(i) IsNot Nothing Then
                    ub = i
                    Exit For
                End If
            Next
            If ub > 0 Then
                ReDim Preserve CSTrainsAndDrivers.MergedCSLinkTrains(ub)
                For i As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                    If UBound(CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains) > 1 Then
                        For m As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains) - 1
                            For n As Integer = m + 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains)
                                If CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains(n).CulStartTime < CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains(m).CulStartTime Then
                                    Dim tTrain As CSLinkTrain = CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains(n)
                                    CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains(n) = CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains(m)
                                    CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains(m) = tTrain
                                End If
                            Next
                        Next
                    End If
                Next
            End If
        End If
    End Sub
    Public Function MyCeiLing(ByVal _Para As Decimal, ByVal Value As Decimal) As Decimal
        MyCeiLing = IIf(Value Mod _Para = 0, Value, Int(Value / _Para) * _Para + _Para)
    End Function
End Module
