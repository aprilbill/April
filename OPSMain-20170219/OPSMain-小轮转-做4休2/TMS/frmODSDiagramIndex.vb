Public Class frmODSDiagramIndex
    Dim nStockNum As Integer
    Dim sTravleSpeed As Single
    Dim sTechSpeed As Single
    Dim nTolPassLength As Single

    Private Sub frmDiagramIndex_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        For i = 1 To Me.DgdWholeIndex.Columns.Count
            Me.DgdWholeIndex.Columns(i - 1).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        For i = 1 To Me.dgrvPeriod.Columns.Count
            Me.dgrvPeriod.Columns(i - 1).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        For i = 1 To Me.dataGridrouting.Columns.Count
            Me.dataGridrouting.Columns(i - 1).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        For i = 1 To Me.dataGridChedi.Columns.Count
            Me.dataGridChedi.Columns(i - 1).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        For i = 1 To Me.dataFirLast.Columns.Count
            Me.dataFirLast.Columns(i - 1).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        Call CalDiaIndex()
    End Sub
    Private Sub CalDiaIndex()
        Call CalIndex() '分交路指标
        Call CalChediIndex() '车底运用指标
        Call CalSectionIndex() '区间能力指标
        Call CalWholeIndex() '总体指标 
        Call CalFirLastStaTimetalbe() '首末班车指标
    End Sub
    Private Sub btnSaveToExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveToExcel.Click
        Call OutPutToEXCELFileFormDataGrid("运行图运量指标", Me.dataGridrouting, Me)
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    '计算区间指标
    Private Sub CalSectionIndex()
        nStockNum = 0
        Dim i As Integer
        Dim j, k, p As Integer
        Dim nStartTime As Integer
        TimeIndex.Clear()
        Dim tmpTime As Integer
        For p = 1 To 48 '半小时
            'If p = 8 Then Stop
            Dim tmpTimeIndex As New typeTimeIndex
            tmpTimeIndex.nFirTime = (p - 1) * 1800
            tmpTimeIndex.nSecTime = p * 1800
            Dim tmpList As New Generic.List(Of typeTimetableIndex)
            For i = 1 To UBound(SectionInf)
                If SectionInf(i).sSecName <> "" Then
                    Dim tmpIndex As New typeTimetableIndex
                    tmpIndex.sSecName = SectionInf(i).sSecName
                    For j = 1 To UBound(TrainInf)
                        If TrainInf(j).Train <> "" Then
                            For k = 1 To UBound(TrainInf(j).nPassSection)
                                If SectionInf(TrainInf(j).nPassSection(k)).sSecName = SectionInf(i).sSecName Then
                                    nStartTime = TrainInf(j).Starting(TrainInf(j).nFirstID(k))
                                    tmpTime = TimeMinus(nStartTime, tmpTimeIndex.nFirTime)
                                    If tmpTime < 1800 And tmpTime >= 0 Then
                                        If j Mod 2 = 0 Then
                                            tmpIndex.nUpTrans = tmpIndex.nUpTrans + 1
                                        Else
                                            tmpIndex.nDownTrains = tmpIndex.nDownTrains + 1
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    Next
                    If tmpIndex.nUpTrans > 0 Then
                        tmpIndex.nUpInterval = 1800 / tmpIndex.nUpTrans
                    Else
                        tmpIndex.nUpInterval = -1
                    End If
                    If tmpIndex.nDownTrains > 0 Then
                        tmpIndex.nDownInterval = 1800 / tmpIndex.nDownTrains
                    Else
                        tmpIndex.nDownInterval = -1
                    End If

                    tmpIndex.nStockNumber = GetUsedStockNumber(TimeAdd(tmpTimeIndex.nFirTime, 900))
                    tmpList.Add(tmpIndex)

                End If
            Next
            tmpTimeIndex.TimetableIndex = tmpList
            TimeIndex.Add(tmpTimeIndex)
        Next p

        '在界面上显示
        Dim nCurRow As Integer
        nCurRow = 0
        Me.dgrvPeriod.Rows.Clear()
        Dim tmpSec As String
        Dim ifIn As Boolean
        For i = 1 To TimeIndex.Count
            For j = 1 To TimeIndex.Item(i - 1).TimetableIndex.Count
                ifIn = False
                tmpSec = GetPrintSecNameFromSecName(TimeIndex.Item(i - 1).TimetableIndex(j - 1).sSecName)
                For k = 1 To Me.dgrvPeriod.Rows.Count
                    If Me.dgrvPeriod.Rows(k - 1).Cells(1).Value.ToString.Trim = tmpSec And Me.dgrvPeriod.Rows(k - 1).Cells(2).Value = i Then
                        ifIn = True
                        Exit For
                    End If
                Next
                If ifIn = False Then
                    Me.dgrvPeriod.Rows.Add()
                    Me.dgrvPeriod.Rows(nCurRow).Cells(0).Value = nCurRow + 1
                    Me.dgrvPeriod.Rows(nCurRow).Cells(1).Value = tmpSec
                    Me.dgrvPeriod.Rows(nCurRow).Cells(2).Value = i
                    Me.dgrvPeriod.Rows(nCurRow).Cells(3).Value = dTime(TimeIndex.Item(i - 1).nFirTime, 0)
                    Me.dgrvPeriod.Rows(nCurRow).Cells(4).Value = dTime(TimeIndex.Item(i - 1).nSecTime, 0)
                    Me.dgrvPeriod.Rows(nCurRow).Cells(5).Value = TimeIndex.Item(i - 1).TimetableIndex(j - 1).nDownTrains
                    Me.dgrvPeriod.Rows(nCurRow).Cells(6).Value = TimeIndex.Item(i - 1).TimetableIndex(j - 1).nUpTrans
                    Me.dgrvPeriod.Rows(nCurRow).Cells(7).Value = SecondToMinute(TimeIndex.Item(i - 1).TimetableIndex(j - 1).nDownInterval)
                    Me.dgrvPeriod.Rows(nCurRow).Cells(8).Value = SecondToMinute(TimeIndex.Item(i - 1).TimetableIndex(j - 1).nUpInterval)
                    Me.dgrvPeriod.Rows(nCurRow).Cells(9).Value = TimeIndex.Item(i - 1).TimetableIndex(j - 1).nStockNumber
                    nStockNum = Math.Max(nStockNum, TimeIndex.Item(i - 1).TimetableIndex(j - 1).nStockNumber)
                    nCurRow = nCurRow + 1
                End If
            Next
        Next
    End Sub

    '得到某时刻车底数
    Private Function GetUsedStockNumber(ByVal nTime As Integer) As Integer
        Dim i As Integer
        Dim nNum As Integer
        Dim nTime1 As Integer
        Dim nTime2 As Integer
        Dim nFirTrain As Integer
        nNum = 0
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                nFirTrain = TrainInf(i).TrainReturn(1)
                If nFirTrain > 0 Then
                    nTime1 = TrainInf(nFirTrain).Arrival(TrainInf(nFirTrain).nPathID(UBound(TrainInf(nFirTrain).nPathID)))
                Else
                    nTime1 = TrainInf(i).Starting(TrainInf(i).nPathID(1))
                End If
                nTime2 = TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID)))
                If TimeMinus(nTime1, nTime) > TimeMinus(nTime2, nTime) Then
                    nNum = nNum + 1
                End If
            End If
        Next
        Return nNum
    End Function

    '分交路指标
    Private Sub CalIndex()
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim p As Integer
        Dim q As Integer
        Dim nNum As Integer
        Dim nCurRow As Integer
        Dim nCurTrain As Integer
        Dim CurLength As Single
        Dim nToSecTime As Integer
        Dim sngToLength As Single
        Dim nToStopTime As Integer
        Dim sSpeed As Single
        Dim sPassNum As Single
        nNum = 0
        Dim r, t As Integer
        Dim curRunTime As Long
        Me.dataGridrouting.Rows.Clear()
        Dim ifIn As Integer
        Dim curStopTime As Long
        Dim stmpSta() As String
        Dim nTolTravleSpeed As Single
        nTolTravleSpeed = 0
        Dim nTolTechSpeed As Single
        nTolTechSpeed = 0
        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName <> "" Then
                'If BasicTrainInf(i).sJiaoLuName = "洞口-->苹果园" Then Stop
                For k = 1 To UBound(BasicTrainInf(i).SecScale)
                    For p = 1 To UBound(BasicTrainInf(i).StopScale)
                        nNum = 0
                        nToStopTime = 0
                        sngToLength = 0
                        nToSecTime = 0
                        For j = 1 To UBound(TrainInf)
                            If TrainInf(j).Train <> "" And TrainInf(j).sJiaoLuName = BasicTrainInf(i).sJiaoLuName Then
                                If TrainInf(j).sRunScaleName = BasicTrainInf(i).SecScale(k).sName And TrainInf(j).sStopSclaeName = BasicTrainInf(i).StopScale(p).sName Then
                                    nNum = nNum + 1
                                    nCurTrain = j
                                End If
                            End If
                        Next j
                        If nNum > 0 Then
                            Me.dataGridrouting.Rows.Add()
                            nCurRow = Me.dataGridrouting.Rows.Count - 1
                            Me.dataGridrouting.Rows(nCurRow).Cells(0).Value = nCurRow + 1
                            Me.dataGridrouting.Rows(nCurRow).Cells(1).Value = BasicTrainInf(i).sJiaoLuName
                            Me.dataGridrouting.Rows(nCurRow).Cells(2).Value = BasicTrainInf(i).SecScale(k).sName
                            Me.dataGridrouting.Rows(nCurRow).Cells(3).Value = BasicTrainInf(i).StopScale(p).sName
                            Me.dataGridrouting.Rows(nCurRow).Cells(4).Value = nNum
                            ReDim stmpSta(0)
                            For r = 1 To UBound(TrainInf(nCurTrain).nPathID)
                                ifIn = 0
                                For j = 1 To UBound(stmpSta)
                                    If stmpSta(j) = StationInf(TrainInf(nCurTrain).nPathID(r)).sStationName Then
                                        ifIn = 1
                                        Exit For
                                    End If
                                Next
                                If ifIn = 0 Then '不同名
                                    curStopTime = TimeMunusTime(TrainInf(nCurTrain).Starting(TrainInf(nCurTrain).nPathID(r)), TrainInf(nCurTrain).Arrival(TrainInf(nCurTrain).nPathID(r)))
                                    If r > 1 And r < UBound(TrainInf(nCurTrain).nPathID) Then
                                        nToStopTime = nToStopTime + curStopTime
                                    End If
                                    ReDim Preserve stmpSta(UBound(stmpSta) + 1)
                                    stmpSta(UBound(stmpSta)) = StationInf(TrainInf(nCurTrain).nPathID(r)).sStationName
                                End If
                            Next


                            For t = 1 To UBound(TrainInf(nCurTrain).nPassSection)
                                curRunTime = TimeMunusTime(TrainInf(nCurTrain).Arrival(TrainInf(nCurTrain).nSecondID(t)), TrainInf(nCurTrain).Starting(TrainInf(nCurTrain).nFirstID(t)))
                                nToSecTime = nToSecTime + curRunTime
                            Next

                            For q = 1 To UBound(BasicTrainInf(i).nPassSection)
                                If i Mod 2 = 0 Then
                                    CurLength = SectionInf(BasicTrainInf(i).nPassSection(q)).lDistance(2)
                                Else
                                    CurLength = SectionInf(BasicTrainInf(i).nPassSection(q)).lDistance(1)
                                End If
                                sngToLength = sngToLength + CurLength
                            Next

                            If Me.cmbTimeFormate.Text = "分" Then
                                Me.dataGridrouting.Rows(nCurRow).Cells(5).Value = Format(nToSecTime / 60, "#0.00")
                                Me.dataGridrouting.Rows(nCurRow).Cells(6).Value = Format(nToStopTime / 60, "#0.00") ' SecondToMinute(nToStopTime)
                                Me.dataGridrouting.Rows(nCurRow).Cells(7).Value = Format((nToSecTime + nToStopTime) / 60, "#0.00") ' SecondToMinute(nToSecTime + nToStopTime)
                            Else
                                Me.dataGridrouting.Rows(nCurRow).Cells(5).Value = SecondToMinute(nToSecTime)
                                Me.dataGridrouting.Rows(nCurRow).Cells(6).Value = SecondToMinute(nToStopTime)
                                Me.dataGridrouting.Rows(nCurRow).Cells(7).Value = SecondToMinute(nToSecTime + nToStopTime)
                            End If

                            sSpeed = (sngToLength / ((nToSecTime + nToStopTime) / 3600))
                            Me.dataGridrouting.Rows(nCurRow).Cells(8).Value = Format(sSpeed, "#0.00") '& "  (公里/小时)"
                            nTolTravleSpeed = nTolTravleSpeed + sSpeed * nNum
                            sSpeed = sngToLength / (nToSecTime / 3600)
                            Me.dataGridrouting.Rows(nCurRow).Cells(9).Value = Format(sSpeed, "#0.00") '& "  (公里/小时)"
                            nTolTechSpeed = nTolTechSpeed + sSpeed * nNum
                            sPassNum = nNum * sngToLength
                            Me.dataGridrouting.Rows(nCurRow).Cells(10).Value = Format(sPassNum, "#0.00") '走行公里
                        End If
                    Next p
                Next k
            End If
        Next i
        '作合计
        Me.dataGridrouting.Rows.Add()
        nCurRow = Me.dataGridrouting.Rows.Count - 1
        For i = 1 To Me.dataGridrouting.Rows(nCurRow).Cells.Count
            Me.dataGridrouting.Rows(nCurRow).Cells(i - 1).Style.BackColor = Color.DarkGray
        Next
        Me.dataGridrouting.Rows(nCurRow).Cells(1).Value = "合计"
        Dim nToTrainNum As Single
        Dim nToPassNum As Single
        Dim nToPassNum1 As Single
        Dim nToPassNumKM As Single
        Dim nToPassNumKM1 As Single
        Dim nToPassNumKM2 As Single
        nToTrainNum = 0
        nToPassNum = 0
        nToPassNum1 = 0
        nToPassNumKM = 0
        nToPassNumKM1 = 0
        nToPassNumKM2 = 0
        For i = 1 To Me.dataGridrouting.Rows.Count
            nToTrainNum = nToTrainNum + Me.dataGridrouting.Rows(i - 1).Cells(4).Value
            nToPassNum = nToPassNum + Me.dataGridrouting.Rows(i - 1).Cells(10).Value
            'nToPassNum1 = nToPassNum1 + Me.dataGrid.Rows(i - 1).Cells(11).Value
            'nToPassNumKM = nToPassNumKM + Me.dataGrid.Rows(i - 1).Cells(12).Value
            'nToPassNumKM1 = nToPassNumKM1 + Me.dataGrid.Rows(i - 1).Cells(13).Value
            'nToPassNumKM2 = nToPassNumKM2 + Me.dataGrid.Rows(i - 1).Cells(14).Value
        Next
        Me.dataGridrouting.Rows(nCurRow).Cells(4).Value = nToTrainNum
        nTolPassLength = nToPassNum
        Me.dataGridrouting.Rows(nCurRow).Cells(10).Value = Format(nToPassNum, "#0.00")
        If nToTrainNum > 0 Then
            sTravleSpeed = Format(nTolTravleSpeed / nToTrainNum, "#0.00")
            Me.dataGridrouting.Rows(nCurRow).Cells(8).Value = sTravleSpeed
            sTechSpeed = Format(nTolTechSpeed / nToTrainNum, "#0.00")
            Me.dataGridrouting.Rows(nCurRow).Cells(9).Value = sTechSpeed
        End If
        'Me.dataGrid.Rows(nCurRow).Cells(13).Value = nToPassNumKM1
        'Me.dataGrid.Rows(nCurRow).Cells(14).Value = nToPassNumKM2
    End Sub

    Private Sub CalChediIndex()
        Me.dataGridChedi.Rows.Clear()
        Dim i, j As Integer
        Dim nCurRow As Integer
        nCurRow = 0
        Dim sLinkTrain As String
        For i = 1 To UBound(ChediInfo)
            If UBound(ChediInfo(i).nLinkTrain) > 0 Then
                sLinkTrain = ""
                For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                    sLinkTrain = sLinkTrain & "\" & TrainInf(ChediInfo(i).nLinkTrain(j)).sPrintTrain
                Next
                Me.dataGridChedi.Rows.Add()
                Me.dataGridChedi.Rows(nCurRow).Cells(0).Value = nCurRow + 1
                Me.dataGridChedi.Rows(nCurRow).Cells(1).Value = ChediInfo(i).sCheCiHao
                Me.dataGridChedi.Rows(nCurRow).Cells(2).Value = ChediInfo(i).SCheDiLeiXing
                Me.dataGridChedi.Rows(nCurRow).Cells(3).Value = UBound(ChediInfo(i).nLinkTrain)
                Me.dataGridChedi.Rows(nCurRow).Cells(4).Value = sLinkTrain
                nCurRow = nCurRow + 1
            End If
        Next
    End Sub

    Private Sub CalWholeIndex()
        Dim i, j, r, t, q As Integer

        Dim nDownTrains As Integer
        Dim nUpTrains As Integer
        Dim nTryTrains As Integer
        Dim nFRunTrains As Integer
        Dim sTrainPro As String
        Dim sIfAllowPass As String
        Dim sAllowPassTrains As Integer
        Dim sNotAllowPassTrains As Integer
        Dim nTolTravalSpeed As Single = 0
        Dim nTolTechSpeed As Single = 0
        Dim curRunTime As Integer = 0
        Dim nToSecTime As Integer = 0
        Dim CurLength As Single = 0
        Dim sngToLength As Single = 0
        Dim stmpSta() As String
        Dim ifIn As Integer = 0
        Dim curStopTime As Integer = 0
        Dim nToStopTime As Integer = 0
        Dim ntmpSpeed As Single
        ReDim stmpSta(0)
        nDownTrains = 0
        nUpTrains = 0
        nTryTrains = 0
        nFRunTrains = 0
        sAllowPassTrains = 0
        sNotAllowPassTrains = 0
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                If i Mod 2 = 0 Then
                    nUpTrains = nUpTrains + 1
                Else
                    nDownTrains = nDownTrains + 1
                End If
                sTrainPro = GetTrainPro(TrainInf(i).sTrainXingZhi, 2)
                If sTrainPro = "轧道车" Then
                    nFRunTrains = nFRunTrains + 1
                ElseIf sTrainPro = "调试车" Then
                    nTryTrains = nTryTrains + 1
                End If
                sIfAllowPass = GetTrainPro(TrainInf(i).sTrainXingZhi, 1)
                If sIfAllowPass = "能载客" Or sIfAllowPass = "" Then
                    sAllowPassTrains = sAllowPassTrains + 1
                    nToStopTime = 0
                    ReDim stmpSta(0)
                    For r = 1 To UBound(TrainInf(i).nPathID)
                        ifIn = 0
                        For j = 1 To UBound(stmpSta)
                            If stmpSta(j) = StationInf(TrainInf(i).nPathID(r)).sStationName Then
                                ifIn = 1
                                Exit For
                            End If
                        Next
                        If ifIn = 0 Then '不同名
                            curStopTime = TimeMunusTime(TrainInf(i).Starting(TrainInf(i).nPathID(r)), TrainInf(i).Arrival(TrainInf(i).nPathID(r)))
                            If r > 1 And r < UBound(TrainInf(i).nPathID) Then
                                nToStopTime = nToStopTime + curStopTime
                            End If
                            ReDim Preserve stmpSta(UBound(stmpSta) + 1)
                            stmpSta(UBound(stmpSta)) = StationInf(TrainInf(i).nPathID(r)).sStationName
                        End If
                    Next
                    nToSecTime = 0
                    For t = 1 To UBound(TrainInf(i).nPassSection)
                        curRunTime = TimeMunusTime(TrainInf(i).Arrival(TrainInf(i).nSecondID(t)), TrainInf(i).Starting(TrainInf(i).nFirstID(t)))
                        nToSecTime = nToSecTime + curRunTime
                    Next
                    sngToLength = 0
                    For q = 1 To UBound(TrainInf(i).nPassSection)
                        If i Mod 2 = 0 Then
                            CurLength = SectionInf(TrainInf(i).nPassSection(q)).lDistance(2)
                        Else
                            CurLength = SectionInf(TrainInf(i).nPassSection(q)).lDistance(1)
                        End If
                        sngToLength = sngToLength + CurLength
                    Next
                    ntmpSpeed = (sngToLength / ((nToSecTime + nToStopTime) / 3600))
                    nTolTravalSpeed = nTolTravalSpeed + ntmpSpeed
                    ntmpSpeed = sngToLength / (nToSecTime / 3600)
                    nTolTechSpeed = nTolTechSpeed + ntmpSpeed
                ElseIf sIfAllowPass = "不载客" Then
                    sNotAllowPassTrains = sNotAllowPassTrains + 1
                End If
            End If
        Next
        '基本信息
        Me.DgdWholeIndex.RowCount = 11
        Me.DgdWholeIndex.Rows(0).Height = 20
        Me.DgdWholeIndex.Rows(0).Cells(0).Value = "下行开行列数(列次)"
        Me.DgdWholeIndex.Rows(0).Cells(1).Value = nDownTrains
        Me.DgdWholeIndex.Rows(1).Cells(0).Value = "上行开行列数(列次)"
        Me.DgdWholeIndex.Rows(1).Cells(1).Value = nUpTrains
        Me.DgdWholeIndex.Rows(2).Cells(0).Value = "总开行列数(列次)"
        Me.DgdWholeIndex.Rows(2).Cells(1).Value = nDownTrains + nUpTrains
        Me.DgdWholeIndex.Rows(3).Cells(0).Value = "轧道列车数(列次)"
        Me.DgdWholeIndex.Rows(3).Cells(1).Value = nFRunTrains
        Me.DgdWholeIndex.Rows(4).Cells(0).Value = "调试列车数(列次)"
        Me.DgdWholeIndex.Rows(4).Cells(1).Value = nTryTrains
        Me.DgdWholeIndex.Rows(5).Cells(0).Value = "载客列车数(列次)"
        Me.DgdWholeIndex.Rows(5).Cells(1).Value = sAllowPassTrains
        Me.DgdWholeIndex.Rows(6).Cells(0).Value = "空驶列车数(列次)"
        Me.DgdWholeIndex.Rows(6).Cells(1).Value = sNotAllowPassTrains
        Me.DgdWholeIndex.Rows(7).Cells(0).Value = "平均旅行速度(km/h)"
        Me.DgdWholeIndex.Rows(7).Cells(1).Value = Format(nTolTravalSpeed / sAllowPassTrains, "#0.00")
        Me.DgdWholeIndex.Rows(8).Cells(0).Value = "平均技术速度(km/h)"
        Me.DgdWholeIndex.Rows(8).Cells(1).Value = Format(nTolTechSpeed / sAllowPassTrains, "#0.00")
        Me.DgdWholeIndex.Rows(9).Cells(0).Value = "列车走行公里(列车公里)"
        Me.DgdWholeIndex.Rows(9).Cells(1).Value = nTolPassLength
        Me.DgdWholeIndex.Rows(10).Cells(0).Value = "上线车底数(列)"
        Me.DgdWholeIndex.Rows(10).Cells(1).Value = nStockNum

    End Sub
    Private Sub btnCal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCal.Click
        Call CalIndex()
    End Sub

    Private Sub btOutCheDitoExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btOutCheDitoExcel.Click
        Call OutPutToEXCELFileFormDataGrid("运行图底运用指标", Me.dataGridChedi, Me)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call OutPutToEXCELFileFormDataGrid("运行图综合指标", Me.DgdWholeIndex, Me)
    End Sub

    Private Sub btnAllOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllOutput.Click
        Call OutPutToEXCELFileFormDataGrid("运行图运量指标", Me.dataGridrouting, Me)
        Call OutPutToEXCELFileFormDataGrid("运行图分时段运量指标", Me.dgrvPeriod, Me)
        Call OutPutToEXCELFileFormDataGrid("运行图底运用指标", Me.dataGridChedi, Me)
        Call OutPutToEXCELFileFormDataGrid("首末班车时刻表", Me.dataFirLast, Me)
        Call OutPutToEXCELFileFormDataGrid("运行图综合指标", Me.DgdWholeIndex, Me)

    End Sub

    Private Sub btnOutExcel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutExcel2.Click
        Call OutPutToEXCELFileFormDataGrid("运行图分时段运量指标", Me.dgrvPeriod, Me)
    End Sub
    Private Sub SaveIndextoDatabase()
        Dim i As Integer
        TMSlocalDataSet.TMS_ROUTINGINDEXVALUE.Clear()
        TMSlocalDataSet.Fill("TMS_ROUTINGINDEXVALUE")

        '先删除以前存好的记录
        For Each Row As DataAccessTier.OdsDataSet.TMS_ROUTINGINDEXVALUERow In TMSlocalDataSet.TMS_ROUTINGINDEXVALUE
            If Row.Item("TRAINDIAGRAMID") = ODSPubpara.DiagramSelect Then
                Row.Delete()
            End If
        Next
        TMSlocalDataSet.Update("TMS_ROUTINGINDEXVALUE")
        For i = 1 To Me.dataGridrouting.Rows.Count
            If dataGridrouting.Rows(i - 1).Cells(1).Value <> "合计" Then
                TMSlocalDataSet.TMS_ROUTINGINDEXVALUE.AddTMS_ROUTINGINDEXVALUERow(ODSPubpara.DiagramSelect, dataGridrouting.Rows(i - 1).Cells(1).Value, dataGridrouting.Rows(i - 1).Cells(2).Value, dataGridrouting.Rows(i - 1).Cells(3).Value, dataGridrouting.Rows(i - 1).Cells(4).Value, dataGridrouting.Rows(i - 1).Cells(5).Value, dataGridrouting.Rows(i - 1).Cells(6).Value, dataGridrouting.Rows(i - 1).Cells(7).Value, dataGridrouting.Rows(i - 1).Cells(8).Value, dataGridrouting.Rows(i - 1).Cells(9).Value, dataGridrouting.Rows(i - 1).Cells(10).Value)
            End If
        Next
        TMSlocalDataSet.Update("TMS_ROUTINGINDEXVALUE")

        TMSlocalDataSet.TMS_PERIODINDEXVALUE.Clear()
        TMSlocalDataSet.Fill("TMS_PERIODINDEXVALUE")
        '先删除以前存好的记录
        For Each Row As DataAccessTier.OdsDataSet.TMS_PERIODINDEXVALUERow In TMSlocalDataSet.TMS_PERIODINDEXVALUE
            If Row.Item("TRAINDIAGRAMID") = ODSPubpara.DiagramSelect Then
                Row.Delete()
            End If
        Next
        TMSlocalDataSet.Update("TMS_PERIODINDEXVALUE")

        For i = 1 To Me.dgrvPeriod.Rows.Count
            If dgrvPeriod.Rows(i - 1).Cells(1).Value <> "合计" Then
                TMSlocalDataSet.TMS_PERIODINDEXVALUE.AddTMS_PERIODINDEXVALUERow(ODSPubpara.DiagramSelect, dgrvPeriod.Rows(i - 1).Cells(1).Value, Val(dgrvPeriod.Rows(i - 1).Cells(2).Value), dgrvPeriod.Rows(i - 1).Cells(3).Value, dgrvPeriod.Rows(i - 1).Cells(4).Value, Val(dgrvPeriod.Rows(i - 1).Cells(5).Value), Val(dgrvPeriod.Rows(i - 1).Cells(6).Value), dgrvPeriod.Rows(i - 1).Cells(7).Value, dgrvPeriod.Rows(i - 1).Cells(8).Value, Val(dgrvPeriod.Rows(i - 1).Cells(9).Value))
            End If
        Next
        TMSlocalDataSet.Update("TMS_PERIODINDEXVALUE")

        TMSlocalDataSet.TMS_STOCKUSINGINDEXVALUE.Clear()
        TMSlocalDataSet.Fill("TMS_STOCKUSINGINDEXVALUE")
        '先删除以前存好的记录
        For Each Row As DataAccessTier.OdsDataSet.TMS_STOCKUSINGINDEXVALUERow In TMSlocalDataSet.TMS_STOCKUSINGINDEXVALUE
            If Row.Item("TRAINDIAGRAMID") = ODSPubpara.DiagramSelect Then
                Row.Delete()
            End If
        Next
        TMSlocalDataSet.Update("TMS_STOCKUSINGINDEXVALUE")

        For i = 1 To Me.dataGridChedi.Rows.Count
            If Me.dataGridChedi.Rows(i - 1).Cells(1).Value <> "合计" Then
                TMSlocalDataSet.TMS_STOCKUSINGINDEXVALUE.AddTMS_STOCKUSINGINDEXVALUERow(ODSPubpara.DiagramSelect, Me.dataGridChedi.Rows(i - 1).Cells(1).Value, Me.dataGridChedi.Rows(i - 1).Cells(2).Value, Val(Me.dataGridChedi.Rows(i - 1).Cells(3).Value), Me.dataGridChedi.Rows(i - 1).Cells(4).Value, Me.dataGridChedi.Rows(i - 1).Cells(0).Value)
            End If
        Next
        TMSlocalDataSet.Update("TMS_STOCKUSINGINDEXVALUE")

        '首末班车保存
        TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE.Clear()
        TMSlocalDataSet.Fill("TMS_FIRLAST_TRAIN_TIMETABLE")

        '先删除以前存好的记录
        For Each Row As DataAccessTier.OdsDataSet.TMS_FIRLAST_TRAIN_TIMETABLERow In TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE
            If Row.Item("TRAINDIAGRAMID") = ODSPubpara.DiagramSelect Then
                Row.Delete()
            End If
        Next
        TMSlocalDataSet.Update("TMS_FIRLAST_TRAIN_TIMETABLE")
        Dim sLineName As String
        sLineName = GetDiagramLineNameFromVersion(ODSPubpara.DiagramSelect)
        For i = 1 To Me.dataFirLast.Rows.Count
            If dataFirLast.Rows(i - 1).Cells(1).Value <> "" Then
                TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE.AddTMS_FIRLAST_TRAIN_TIMETABLERow(ODSPubpara.DiagramSelect, sLineName, dataFirLast.Rows(i - 1).Cells(1).Value, dataFirLast.Rows(i - 1).Cells(2).Value, dataFirLast.Rows(i - 1).Cells(3).Value, dataFirLast.Rows(i - 1).Cells(4).Value, dataFirLast.Rows(i - 1).Cells(5).Value)
            End If
        Next
        TMSlocalDataSet.Update("TMS_FIRLAST_TRAIN_TIMETABLE")

        '整体指标保存
        TMSlocalDataSet.TMS_TIMETABLE_WHOLE_INDEX.Clear()
        TMSlocalDataSet.Fill("TMS_TIMETABLE_WHOLE_INDEX")

        '先删除以前存好的记录
        For Each Row As DataAccessTier.OdsDataSet.TMS_TIMETABLE_WHOLE_INDEXRow In TMSlocalDataSet.TMS_TIMETABLE_WHOLE_INDEX
            If Row.Item("TRAINDIAGRAMEID") = ODSPubpara.DiagramSelect Then
                Row.Delete()
            End If
        Next
        TMSlocalDataSet.Update("TMS_TIMETABLE_WHOLE_INDEX")
        Dim sValue As New Generic.List(Of String)
        For i = 1 To Me.DgdWholeIndex.Rows.Count
            sValue.Add(Me.DgdWholeIndex.Rows(i - 1).Cells(1).Value)
        Next
        TMSlocalDataSet.TMS_TIMETABLE_WHOLE_INDEX.AddTMS_TIMETABLE_WHOLE_INDEXRow(ODSPubpara.DiagramSelect, sValue(0), sValue(1), sValue(2), sValue(3), sValue(4), sValue(5), sValue(6), sValue(7), sValue(8), sValue(9), sValue(10))
        TMSlocalDataSet.Update("TMS_TIMETABLE_WHOLE_INDEX")
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If ODSPubpara.DiagramSelect = "" Then
            MsgBox("由于运行图版本号为空，该运行图指标无法保存!", , "提示")
            Exit Sub
        Else
            Me.Cursor = Cursors.WaitCursor
            Call SaveIndextoDatabase()
            Me.Cursor = Cursors.Default
            MsgBox("已经成功保存!", , "提示")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutExcelFisrtLast.Click
        Call OutPutToEXCELFileFormDataGrid("首末班车时刻表", Me.dataFirLast, Me)
    End Sub

    '计算首末班车时刻表
    Private Sub CalFirLastStaTimetalbe()
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim sFirTime As Long
        Dim sEndTime As Long
        Dim sUpFirTime As Long
        Dim sUpEndTime As Long

        Dim sArriTime As Long
        Dim sStarTime As Long
        Dim sTrainPro As String
        Dim tmpSta As String
        Dim ifIn As Boolean
        Me.dataFirLast.Rows.Clear()
        For i = 1 To UBound(NotSameStationInf)
            'If StationInf(i).sAtLineName <> "出入库线" Then
            sFirTime = 48 * 3600.0#
            sEndTime = 0

            sUpFirTime = 48 * 3600.0#
            sUpEndTime = 0
            For j = 1 To UBound(TrainInf)
                sTrainPro = GetTrainPro(TrainInf(j).sTrainXingZhi, 1)
                If j Mod 2 <> 0 Then '下行
                    If sTrainPro <> "不载客" Then
                        If TrainInf(j).Train <> "" Then
                            For k = 1 To UBound(TrainInf(j).nPathID)
                                If StationInf(TrainInf(j).nPathID(k)).sStationName = NotSameStationInf(i) Then
                                    sArriTime = AddLitterTime(TrainInf(j).Arrival(TrainInf(j).nPathID(k)))
                                    sStarTime = AddLitterTime(TrainInf(j).Starting(TrainInf(j).nPathID(k)))
                                    'If dTime(sStarTime, 0) = "04:44:00" Then Stop
                                    If k < UBound(TrainInf(j).nPathID) Then
                                        If k = 1 Then
                                            If sArriTime <= sStarTime Then '停站
                                                If sStarTime < sFirTime Then
                                                    sFirTime = sStarTime
                                                End If

                                                If sStarTime > sEndTime Then
                                                    sEndTime = sStarTime
                                                End If
                                            End If

                                        Else
                                            If sArriTime < sStarTime Then '停站
                                                If sStarTime < sFirTime Then
                                                    sFirTime = sStarTime
                                                End If

                                                If sStarTime > sEndTime Then
                                                    sEndTime = sStarTime
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            Next k
                        End If
                    End If
                Else
                    If TrainInf(j).Train <> "" Then
                        If sTrainPro <> "不载客" Then
                            For k = 1 To UBound(TrainInf(j).nPathID)
                                If StationInf(TrainInf(j).nPathID(k)).sStationName = NotSameStationInf(i) Then
                                    sArriTime = AddLitterTime(TrainInf(j).Arrival(TrainInf(j).nPathID(k)))
                                    sStarTime = AddLitterTime(TrainInf(j).Starting(TrainInf(j).nPathID(k)))
                                    If k < UBound(TrainInf(j).nPathID) Then
                                        If k = 1 Then
                                            If sArriTime <= sStarTime Then '停站
                                                If sStarTime < sUpFirTime Then
                                                    sUpFirTime = sStarTime
                                                End If

                                                If sStarTime > sUpEndTime Then
                                                    sUpEndTime = sStarTime
                                                End If
                                            End If
                                        Else
                                            If sArriTime < sStarTime Then '停站
                                                If sStarTime < sUpFirTime Then
                                                    sUpFirTime = sStarTime
                                                End If

                                                If sStarTime > sUpEndTime Then
                                                    sUpEndTime = sStarTime
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            Next k
                        End If
                    End If
                End If
            Next j

            sFirTime = DeleteLitterTime(sFirTime)
            sEndTime = DeleteLitterTime(sEndTime)

            sUpFirTime = DeleteLitterTime(sUpFirTime)
            sUpEndTime = DeleteLitterTime(sUpEndTime)

            Dim Str1, Str2, Str3, Str4 As String
            If sFirTime = 48 * 3600.0# Then
                Str1 = "无"
            Else
                Str1 = dTime(sFirTime, 0)
            End If

            If sUpFirTime = 48 * 3600.0# Then
                Str2 = "无"
            Else
                Str2 = dTime(sUpFirTime, 0)
            End If

            If sEndTime = 0 Then
                Str3 = "无"
            Else
                Str3 = dTime(sEndTime, 0)
            End If

            If sUpEndTime = 0 Then
                Str4 = "无"
            Else
                Str4 = dTime(sUpEndTime, 0)
            End If
            tmpSta = GetPrintStaNameFromStaName(NotSameStationInf(i))
            ifIn = False
            For j = 1 To Me.dataFirLast.Rows.Count
                If Me.dataFirLast.Rows(j - 1).Cells(1).Value = tmpSta Then
                    If Str2 <> "无" Then
                        Me.dataFirLast.Rows(j - 1).Cells(4).Value = Str2
                    End If
                    If Str4 <> "无" Then
                        Me.dataFirLast.Rows(j - 1).Cells(5).Value = Str4
                    End If
                    ifIn = True
                    Exit For
                End If
            Next
            If ifIn = False Then
                Me.dataFirLast.Rows.Add()
                Me.dataFirLast.Rows(Me.dataFirLast.Rows.Count - 1).Cells(0).Value = Me.dataFirLast.Rows.Count
                Me.dataFirLast.Rows(Me.dataFirLast.Rows.Count - 1).Cells(1).Value = GetPrintStaNameFromStaName(NotSameStationInf(i))
                Me.dataFirLast.Rows(Me.dataFirLast.Rows.Count - 1).Cells(2).Value = Str1
                Me.dataFirLast.Rows(Me.dataFirLast.Rows.Count - 1).Cells(3).Value = Str3
                Me.dataFirLast.Rows(Me.dataFirLast.Rows.Count - 1).Cells(4).Value = Str2
                Me.dataFirLast.Rows(Me.dataFirLast.Rows.Count - 1).Cells(5).Value = Str4
            End If
        Next i

    End Sub

    Private Sub btnReCal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReCal.Click
        Call CalDiaIndex()
    End Sub
End Class