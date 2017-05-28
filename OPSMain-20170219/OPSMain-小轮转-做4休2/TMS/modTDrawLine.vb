Module modTDrawLine
    Public Sub DrawSingleTrainLine(ByVal nTrain As Integer, ByVal lngStartTime As Long)
        Dim i As Integer
        Dim lngArriTime1 As Long
        Dim lngStarTime1 As Long
        Dim lngArriTime2 As Long
        Dim lngStarTime2 As Long
        Dim lngStopTime As Long
        Dim lngStopTime2 As Long
        Dim nFirSta As Integer
        Dim nSecSta As Integer
        Dim sCurStopLine As String
        Dim lngCurSecRunTime As Long
        Dim lngStartAppendTime As Long
        Dim lngStopAppendTime As Long

        For i = 1 To UBound(TrainInf(nTrain).nPassSection) '第一个区间
            If i = 1 Then
                nFirSta = TrainInf(nTrain).nFirstID(i)
                nSecSta = TrainInf(nTrain).nSecondID(i)
                lngStarTime1 = lngStartTime
                lngStopTime = GetCurTrainStopTimeAtStation(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sStopSclaeName, StationInf(nFirSta).sStationName)
                lngArriTime1 = TimeMinus(lngStarTime1, lngStopTime)
                sCurStopLine = GetCurTrainStopLine(nTrain, StationInf(nFirSta).sStationName)
                RecordTimeInStation(nTrain, StationInf(nFirSta).sStationName, sCurStopLine, lngArriTime1, lngStarTime1)

                lngCurSecRunTime = GetCurSecRunTime(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).nPassSection(i))
                lngStartAppendTime = GetCurSecStartAppendTime(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).nPassSection(i))

                lngArriTime2 = TimeAdd(lngStarTime1, lngCurSecRunTime + lngStartAppendTime)
                lngStopTime2 = GetCurTrainStopTimeAtStation(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sStopSclaeName, StationInf(nSecSta).sStationName)
                If lngStopTime2 > 0 Then
                    lngStopAppendTime = GetCurSecStartAppendTime(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).nPassSection(i))
                    lngArriTime2 = TimeAdd(lngArriTime2, lngStopAppendTime)
                End If
                If UBound(TrainInf(nTrain).nPassSection) = 1 Then '列车只有一个区间
                    lngStarTime2 = TimeAdd(lngArriTime2, lngStopTime2)
                    sCurStopLine = GetCurTrainStopLine(nTrain, StationInf(nSecSta).sStationName)
                    RecordTimeInStation(nTrain, StationInf(nSecSta).sStationName, sCurStopLine, lngArriTime2, lngStarTime2)
                End If
            ElseIf i = UBound(TrainInf(nTrain).nPassSection) Then '最后一个区间
                nFirSta = TrainInf(nTrain).nFirstID(i)
                nSecSta = TrainInf(nTrain).nSecondID(i)
                lngArriTime1 = lngArriTime2
                sCurStopLine = GetCurTrainStopLine(nTrain, StationInf(nFirSta).sStationName)
                lngStopTime = GetCurTrainStopTimeAtStation(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sStopSclaeName, StationInf(nFirSta).sStationName)
                If lngStopTime = 0 Then
                    lngStarTime1 = lngArriTime1
                Else
                    lngStarTime1 = TimeAdd(lngArriTime1, lngStopTime)
                End If
                RecordTimeInStation(nTrain, StationInf(nFirSta).sStationName, sCurStopLine, lngArriTime1, lngStarTime1)

                lngCurSecRunTime = GetCurSecRunTime(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).nPassSection(i))
                If lngStopTime > 0 Then
                    lngStartAppendTime = GetCurSecStartAppendTime(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).nPassSection(i))
                Else
                    lngStartAppendTime = 0
                End If

                lngArriTime2 = TimeAdd(lngStarTime1, lngCurSecRunTime + lngStartAppendTime)
                lngStopTime2 = GetCurTrainStopTimeAtStation(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sStopSclaeName, StationInf(nSecSta).sStationName)
                ' If lngStopTime2 > 0 Then
                lngStopAppendTime = GetCurSecStartAppendTime(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).nPassSection(i))
                lngArriTime2 = TimeAdd(lngArriTime2, lngStopAppendTime)
                lngStarTime2 = TimeAdd(lngArriTime2, lngStopTime2)
                'End If
                sCurStopLine = GetCurTrainStopLine(nTrain, StationInf(nSecSta).sStationName)
                RecordTimeInStation(nTrain, StationInf(nSecSta).sStationName, sCurStopLine, lngArriTime2, lngStarTime2)

            Else

                nFirSta = TrainInf(nTrain).nFirstID(i)
                nSecSta = TrainInf(nTrain).nSecondID(i)
                lngArriTime1 = lngArriTime2
                sCurStopLine = GetCurTrainStopLine(nTrain, StationInf(nFirSta).sStationName)
                lngStopTime = GetCurTrainStopTimeAtStation(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sStopSclaeName, StationInf(nFirSta).sStationName)
                If lngStopTime = 0 Then
                    lngStarTime1 = lngArriTime1
                Else
                    lngStarTime1 = TimeAdd(lngArriTime1, lngStopTime)
                End If
                RecordTimeInStation(nTrain, StationInf(nFirSta).sStationName, sCurStopLine, lngArriTime1, lngStarTime1)

                lngCurSecRunTime = GetCurSecRunTime(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).nPassSection(i))
                If lngStopTime > 0 Then
                    lngStartAppendTime = GetCurSecStartAppendTime(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).nPassSection(i))
                Else
                    lngStartAppendTime = 0
                End If

                lngArriTime2 = TimeAdd(lngStarTime1, lngCurSecRunTime + lngStartAppendTime)
                lngStopTime2 = GetCurTrainStopTimeAtStation(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sStopSclaeName, StationInf(nSecSta).sStationName)
                If lngStopTime2 > 0 Then
                    lngStopAppendTime = GetCurSecStartAppendTime(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).nPassSection(i))
                    lngArriTime2 = TimeAdd(lngArriTime2, lngStopAppendTime)
                End If
            End If
        Next
        TrainInf(nTrain).nPuOrNot = 1
        TrainInf(nTrain).lAllStartTime = GetTrainArriOrStartTime(nTrain, 0, 1)
        TrainInf(nTrain).lAllEndTime = GetTrainArriOrStartTime(nTrain, -1, 0)

    End Sub

    '得到区间的起车时分
    Public Function GetCurSecStartAppendTime(ByVal sJiaoLuName As String, ByVal sRunScaleName As String, ByVal nSecNum As String) As Long
        If nSecNum > 0 Then
            Dim nUpDowntemp As Integer
            Dim nScaleID As Integer
            nScaleID = GetRunScaleIDFromScaleName(sRunScaleName)
            nUpDowntemp = GetUporDownFromJiaoLuName(sJiaoLuName)
            If nUpDowntemp = 1 Then
                GetCurSecStartAppendTime = SectionInf(nSecNum).SecScale(nScaleID).sngDownAppendStartTime
            Else
                GetCurSecStartAppendTime = SectionInf(nSecNum).SecScale(nScaleID).sngUpAppendStartTime
            End If
        End If
    End Function

    '得到区间的停车时分
    Public Function GetCurSecStopAppendTime(ByVal sJiaoLuName As String, ByVal sRunScaleName As String, ByVal nSecNum As String) As Long
        If nSecNum > 0 Then
            Dim nUpDowntemp As Integer
            Dim nScaleID As Integer
            nScaleID = GetRunScaleIDFromScaleName(sRunScaleName)
            nUpDowntemp = GetUporDownFromJiaoLuName(sJiaoLuName)
            If nUpDowntemp = 1 Then
                GetCurSecStopAppendTime = SectionInf(nSecNum).SecScale(nScaleID).sngDownAppendStopTime
            Else
                GetCurSecStopAppendTime = SectionInf(nSecNum).SecScale(nScaleID).sngUpAppendStopTime
            End If
        End If
    End Function

    '得到区间运行时分
    Public Function GetCurSecRunTime(ByVal sJiaoLuName As String, ByVal sRunScaleName As String, ByVal nSecNum As Integer) As Long
        If nSecNum > 0 Then
            Dim i, j, k As Integer
            Dim nTemp As Integer
            nTemp = nSecNum
            GetCurSecRunTime = 0
            For i = 1 To UBound(BasicTrainInf)
                If BasicTrainInf(i).sJiaoLuName = sJiaoLuName Then ' StationInf(nStatemp).sStationName
                    For j = 1 To UBound(BasicTrainInf(i).SecScale)
                        If BasicTrainInf(i).SecScale(j).sName = sRunScaleName Then
                            For k = 1 To UBound(BasicTrainInf(i).SecScale(j).nSecID)
                                If SectionInf(BasicTrainInf(i).SecScale(j).nSecID(k)).sSecName = SectionInf(nTemp).sSecName Then
                                    GetCurSecRunTime = BasicTrainInf(i).SecScale(j).RunTime(k)
                                    Exit For
                                End If
                            Next k
                            Exit For
                        End If
                    Next j
                    Exit For
                End If
            Next i
        End If

        'Dim nUpDowntemp As Integer
        'Dim nScaleID As Integer
        'nScaleID = GetRunScaleIDFromScaleName(sRunScaleName)
        'nUpDowntemp = GetUporDownFromJiaoLuName(sJiaoLuName)
        'If nUpDowntemp = 1 Then
        '    GetCurSecRunTime = SectionInf(nSecNum).SecScale(nScaleID).sngDownTime
        'Else
        '    GetCurSecRunTime = SectionInf(nSecNum).SecScale(nScaleID).sngUpTime
        'End If
    End Function

    '根据运行标尺名得到标尺ID
    Public Function GetRunScaleIDFromScaleName(ByVal sScaleName As String) As Integer
        Dim i As Integer
        For i = 1 To UBound(TrainRunScaleInf)
            If TrainRunScaleInf(i).sScaleName = sScaleName Then
                GetRunScaleIDFromScaleName = i
                Exit For
            End If
        Next
    End Function

    '根据交路名判断上下行
    Public Function GetUporDownFromJiaoLuName(ByVal sJiaoLuName As String) As Integer
        Dim i As Integer
        GetUporDownFromJiaoLuName = 0
        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName = sJiaoLuName Then ' StationInf(nStatemp).sStationName
                GetUporDownFromJiaoLuName = BasicTrainInf(i).nUporDown
                Exit For
            End If
        Next i
    End Function

    '修改时间和停站股道
    Sub RecordTimeInStation(ByVal nTrnNum As Integer, ByVal sStaName As String, ByVal sStopLine As String, ByVal lArrivalTime As Long, ByVal lStartTime As Long)
        Dim i As Integer
        For i = 1 To UBound(TrainInf(nTrnNum).nPathID)
            If StationInf(TrainInf(nTrnNum).nPathID(i)).sStationName = sStaName Then
                With TrainInf(nTrnNum)
                    .Arrival(TrainInf(nTrnNum).nPathID(i)) = lArrivalTime
                    .Starting(TrainInf(nTrnNum).nPathID(i)) = lStartTime
                    .StopLine(TrainInf(nTrnNum).nPathID(i)) = sStopLine
                End With
            End If
        Next i
    End Sub
    '当前可以停放的股道
    Public Function GetCurTrainStopLine(ByVal nTrain As Integer, ByVal sStaName As String) As String
        Dim nUporDown As Integer
        nUporDown = nDirection(nTrain)
        If nUporDown = 1 Then
            GetCurTrainStopLine = "1"
        Else
            GetCurTrainStopLine = "2"
        End If
    End Function

    '作时间加
    Function TimeAdd(ByVal Time1 As Long, ByVal Time2 As Long) As Long
        Dim temp As Long
        temp = Time1 + Time2
        If temp > 86400 Then
            temp = temp - 86400
        ElseIf temp < 0 Then
            temp = temp + 86400
        End If
        TimeAdd = temp
    End Function

    '作时间差
    Function TimeMinus(ByVal Time1 As Long, ByVal Time2 As Long) As Long
        If Time1 >= Time2 Then
            TimeMinus = Time1 - Time2
        Else
            TimeMinus = 86400 + Time1 - Time2
        End If
    End Function

    Function TimeRunByBiaoChiTrainFromSecNum(ByVal nTrnNum As Integer, ByVal nSecNum As Integer) As Long
        Dim sJiaoLuName As String
        Dim sRunScale As String
        Dim nUpOrDown As Integer
        nUpOrDown = nDirection(nTrnNum)
        sJiaoLuName = TrainInf(nTrnNum).sJiaoLuName
        sRunScale = TrainInf(nTrnNum).sRunScaleName

        If nUpOrDown = 1 Then
            TimeRunByBiaoChiTrainFromSecNum = SectionInf(nSecNum).SecScale(TrainInf(nTrnNum).nTrainTimeKind).sngDownTime
        Else
            TimeRunByBiaoChiTrainFromSecNum = SectionInf(nSecNum).SecScale(TrainInf(nTrnNum).nTrainTimeKind).sngUpTime
        End If
    End Function

    '删除列车
    Public Sub DeleteTrainFromTrainID(ByVal nTrain As Integer, ByVal sIFSure As Boolean)
        If nTrain > 0 Then
            If sIFSure = True Then
                If MsgBox("确定删除该列车吗？", vbQuestion + vbYesNo + vbDefaultButton2, "确认操作") = vbYes Then
                    Dim Cdid As Integer
                    Cdid = CheCiToCheDiID(nTrain)
                    If Cdid <> 0 Then
                        Call DelectTrainInCheDiLink(nTrain, Cdid)
                        TimeTablePara.nPubTrain = 0
                    End If
                End If
            Else
                Dim Cdid As Integer
                Cdid = CheCiToCheDiID(nTrain)
                If Cdid <> 0 Then
                    Call DelectTrainInCheDiLink(nTrain, Cdid)
                    TimeTablePara.nPubTrain = 0
                End If
            End If
        Else
            MsgBox("请先选择要删除的列车!", , "提示")
        End If

    End Sub

    '删除列车运行线
    Public Sub DeleteLinetime(ByVal ntmpTrainNumber As Integer)
        Dim i As Integer
        TrainInf(ntmpTrainNumber).Train = ""
        For i = 1 To UBound(StationInf)
            TrainInf(ntmpTrainNumber).Arrival(i) = -1
            TrainInf(ntmpTrainNumber).Starting(i) = -1
            TrainInf(ntmpTrainNumber).StopLine(i) = ""
        Next i
        TrainInf(ntmpTrainNumber).TrainPuorNot = 0
    End Sub

    ''将列车从车底连接信息中删除
    Public Sub DelectTrainInCheDiLink(ByVal nTrain As Integer, ByVal nCdid As Integer)
        Dim i As Integer
        Dim TempNum() As Integer
        Dim TempNum2() As Integer

        Dim nFtrain As Integer
        Dim nNtrain As Integer
        ReDim TempNum(0)
        ReDim TempNum2(0)
        Dim TN As Integer
        TN = 1
        'If nTrain = 13 Then Stop
        For i = 1 To UBound(ChediInfo(nCdid).nLinkTrain)
            If ChediInfo(nCdid).nLinkTrain(i) <> nTrain Then
                If TN = 1 Then
                    ReDim Preserve TempNum(UBound(TempNum) + 1)
                    TempNum(UBound(TempNum)) = ChediInfo(nCdid).nLinkTrain(i)
                Else
                    ReDim Preserve TempNum2(UBound(TempNum2) + 1)
                    TempNum2(UBound(TempNum2)) = ChediInfo(nCdid).nLinkTrain(i)
                End If
            Else
                TN = 2
            End If
        Next i

        ReDim ChediInfo(nCdid).nLinkTrain(0)
        If UBound(TempNum) > 0 Then
            ReDim ChediInfo(nCdid).nLinkTrain(UBound(TempNum))
            For i = 1 To UBound(TempNum)
                ChediInfo(nCdid).nLinkTrain(i) = TempNum(i)
            Next i
            If UBound(ChediInfo(nCdid).nLinkTrain) > 0 Then
                If UBound(ChediInfo(nCdid).nLinkTrain) = 1 Then
                    TrainInf(ChediInfo(nCdid).nLinkTrain(1)).TrainReturn(1) = 0
                    TrainInf(ChediInfo(nCdid).nLinkTrain(1)).TrainReturn(2) = 0
                Else
                    For i = 1 To UBound(ChediInfo(nCdid).nLinkTrain) - 1
                        nFtrain = ChediInfo(nCdid).nLinkTrain(i)
                        nNtrain = ChediInfo(nCdid).nLinkTrain(i + 1)

                        TrainInf(nFtrain).TrainReturn(1) = 0
                        TrainInf(nFtrain).TrainReturn(2) = nNtrain
                        TrainInf(nNtrain).TrainReturn(1) = nFtrain
                        TrainInf(nNtrain).TrainReturn(2) = 0
                    Next i
                End If
            End If
        End If

        If UBound(TempNum2) > 0 Then

            nCdid = AddNewChediInfor()
            If nCdid > 0 Then
                ReDim ChediInfo(nCdid).nLinkTrain(UBound(TempNum2))
                For i = 1 To UBound(TempNum2)
                    ChediInfo(nCdid).nLinkTrain(i) = TempNum2(i)
                Next i

                If UBound(ChediInfo(nCdid).nLinkTrain) > 0 Then
                    If UBound(ChediInfo(nCdid).nLinkTrain) = 1 Then
                        TrainInf(ChediInfo(nCdid).nLinkTrain(1)).TrainReturn(1) = 0
                        TrainInf(ChediInfo(nCdid).nLinkTrain(1)).TrainReturn(2) = 0
                    Else
                        For i = 1 To UBound(ChediInfo(nCdid).nLinkTrain) - 1
                            nFtrain = ChediInfo(nCdid).nLinkTrain(i)
                            nNtrain = ChediInfo(nCdid).nLinkTrain(i + 1)

                            TrainInf(nFtrain).TrainReturn(1) = 0
                            TrainInf(nFtrain).TrainReturn(2) = nNtrain
                            TrainInf(nNtrain).TrainReturn(1) = nFtrain
                            TrainInf(nNtrain).TrainReturn(2) = 0
                        Next i
                    End If
                End If
                TrainInf(nTrain).nCheDiPuOrNot = 0
                TrainInf(nTrain).nIfFixedCheDi = 0
                DeleteLinetime(nTrain)
            End If
        Else
            TrainInf(nTrain).nCheDiPuOrNot = 0
            TrainInf(nTrain).nIfFixedCheDi = 0
            DeleteLinetime(nTrain)
        End If

    End Sub

    '查找当前可用的ChediInfo()的ID号码
    Public Function GetCurCheDiInfoID() As Integer
        Dim i As Integer
        GetCurCheDiInfoID = 0
        For i = 1 To UBound(ChediInfo)
            If UBound(ChediInfo(i).nLinkTrain) = 0 Then
                GetCurCheDiInfoID = i
                Call CopyCheDiInformation(i, i)
                'ChediInfo(GetCurCheDiInfoID).sCheDiID = GetCurCheDiInfoID
                'ChediInfo(GetCurCheDiInfoID).sCheCiHao = GetCurCheDiInfoID
                Exit For
            End If
        Next i

        If GetCurCheDiInfoID = 0 Then
            GetCurCheDiInfoID = UBound(ChediInfo) + 1
            ReDim Preserve ChediInfo(GetCurCheDiInfoID)
            'ChediInfo(GetCurCheDiInfoID).sCheDiID = GetCurCheDiInfoID
            'ChediInfo(GetCurCheDiInfoID).sCheCiHao = GetCurCheDiInfoID
            Call CopyCheDiInformation(GetCurCheDiInfoID, GetCurCheDiInfoID)
            'ChediInfo(GetCurCheDiInfoID).SCheDiLeiXing = BaseChediInfo(1).SCheDiLeiXing
            'ChediInfo(GetCurCheDiInfoID).PrintCheDiLinkColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.CheDiLineColor)
            'ChediInfo(GetCurCheDiInfoID).PrintCheDiLinkStyle = GetLineTextStyle(TimeTablePara.DiagramStylePara.CheDiLineStyle)
            'ChediInfo(GetCurCheDiInfoID).PrintCheDiLinkWidth = TimeTablePara.DiagramStylePara.CheDiLineWidth

        End If
    End Function

    '查找折返时的运行时间
    Public Function GetReturnRunTime(ByVal SCheDiLeiXing As String, ByVal sStaName As String, ByVal nStep As Integer) As Long
        Dim i As Integer
        GetReturnRunTime = 0
        For i = 1 To UBound(ChediZhefanBiaozhunInfo)
            If ChediZhefanBiaozhunInfo(i).sZhefanStation = sStaName And ChediZhefanBiaozhunInfo(i).SCheDiLeiXing = SCheDiLeiXing Then
                Select Case nStep
                    Case 1 '到达运行至折返线时间
                        GetReturnRunTime = ChediZhefanBiaozhunInfo(i).nFirRunTime
                    Case 2 '折返线运行至出发股道时间
                        GetReturnRunTime = ChediZhefanBiaozhunInfo(i).nSecRunTime
                    Case 3 '到达时发到间隔时间
                        GetReturnRunTime = ChediZhefanBiaozhunInfo(i).nArrFaDaoTime
                    Case 4 '出发时发到间隔时间
                        GetReturnRunTime = ChediZhefanBiaozhunInfo(i).nStartFaDaoTime
                End Select
                Exit For
            End If
        Next i
    End Function
    '查找折返时间
    Public Function GetZheFanTime(ByVal SCheDiLeiXing As String, ByVal sZheFanSta As String, ByVal sZheFanFangShi As String) As Single
        Dim i As Integer
        GetZheFanTime = 0
        For i = 1 To UBound(ChediZhefanBiaozhunInfo)
            If ChediZhefanBiaozhunInfo(i).sZhefanStation = sZheFanSta And ChediZhefanBiaozhunInfo(i).SCheDiLeiXing = SCheDiLeiXing Then
                Select Case sZheFanFangShi
                    Case "立即折返"
                        GetZheFanTime = ChediZhefanBiaozhunInfo(i).lLijiZhefanTime
                        'Case "停留折返"
                        '    GetZheFanTime = ChediZhefanBiaozhunInfo(i).lTingliuZhefanTime
                        'Case "转线折返"
                        '    GetZheFanTime = ChediZhefanBiaozhunInfo(i).lZhuanxianZhefanTime
                    Case "站前折返"
                        GetZheFanTime = ChediZhefanBiaozhunInfo(i).lZhanQianTime
                    Case "站后折返"
                        GetZheFanTime = ChediZhefanBiaozhunInfo(i).lZhanHouTime
                        'Case "入库"
                        '    GetZheFanTime = ChediZhefanBiaozhunInfo(i).lRukuTime
                        'Case "出库"
                        '    GetZheFanTime = ChediZhefanBiaozhunInfo(i).lChukuTime
                        'Case "库内检修"
                        '    GetZheFanTime = ChediZhefanBiaozhunInfo(i).lZaikuStopTime
                        'Case "出入库"
                        '    GetZheFanTime = ChediZhefanBiaozhunInfo(i).lRukuTime + ChediZhefanBiaozhunInfo(i).lChukuTime + ChediZhefanBiaozhunInfo(i).lZaikuStopTime
                End Select
                Exit For
            End If
        Next i
    End Function

    '在原车底上加上一列车
    Public Sub AddTrainInCheDiLink(ByVal nCheDi As Integer, ByVal nTrain As Integer, ByVal sBeForeOrAfter As Integer)
        'sBeForeOrAfter 1表示在前加，2表示在后加
        Dim j As Integer
        Dim TempNtrain As Integer
        Dim TempNtrain1 As Integer
        TrainInf(nTrain).nCheDiPuOrNot = 1
        TrainInf(nTrain).SCheDiLeiXing = ChediInfo(nCheDi).SCheDiLeiXing
        If UBound(ChediInfo(nCheDi).nLinkTrain) = 0 Then
            ReDim Preserve ChediInfo(nCheDi).nLinkTrain(UBound(ChediInfo(nCheDi).nLinkTrain) + 1)
            ChediInfo(nCheDi).nLinkTrain(1) = nTrain
            TrainInf(nTrain).TrainReturn(1) = 0
            TrainInf(nTrain).TrainReturn(2) = 0
            Exit Sub

        ElseIf UBound(ChediInfo(nCheDi).nLinkTrain) = 1 Then
            TempNtrain = ChediInfo(nCheDi).nLinkTrain(1)
            ReDim Preserve ChediInfo(nCheDi).nLinkTrain(UBound(ChediInfo(nCheDi).nLinkTrain) + 1)
            If sBeForeOrAfter = 1 Then
                ChediInfo(nCheDi).nLinkTrain(2) = ChediInfo(nCheDi).nLinkTrain(1)
                ChediInfo(nCheDi).nLinkTrain(1) = nTrain

                TrainInf(TempNtrain).TrainReturn(1) = nTrain
                TrainInf(TempNtrain).TrainReturn(2) = 0
                TrainInf(nTrain).TrainReturn(1) = 0
                TrainInf(nTrain).TrainReturn(2) = TempNtrain
                Exit Sub
            Else
                ChediInfo(nCheDi).nLinkTrain(UBound(ChediInfo(nCheDi).nLinkTrain)) = nTrain
                TrainInf(TempNtrain).TrainReturn(1) = 0
                TrainInf(TempNtrain).TrainReturn(2) = nTrain
                TrainInf(nTrain).TrainReturn(1) = TempNtrain
                TrainInf(nTrain).TrainReturn(2) = 0
                Exit Sub
            End If
        Else
            TempNtrain = ChediInfo(nCheDi).nLinkTrain(1)
            TempNtrain1 = ChediInfo(nCheDi).nLinkTrain(UBound(ChediInfo(nCheDi).nLinkTrain))
            If sBeForeOrAfter = 1 Then
                ReDim Preserve ChediInfo(nCheDi).nLinkTrain(UBound(ChediInfo(nCheDi).nLinkTrain) + 1)
                For j = UBound(ChediInfo(nCheDi).nLinkTrain) To 2 Step -1
                    ChediInfo(nCheDi).nLinkTrain(j) = ChediInfo(nCheDi).nLinkTrain(j - 1)
                Next j
                ChediInfo(nCheDi).nLinkTrain(1) = nTrain

                TrainInf(nTrain).TrainReturn(1) = 0
                TrainInf(nTrain).TrainReturn(2) = TempNtrain
                TrainInf(TempNtrain).TrainReturn(1) = nTrain
                Exit Sub

            Else

                ReDim Preserve ChediInfo(nCheDi).nLinkTrain(UBound(ChediInfo(nCheDi).nLinkTrain) + 1)
                ChediInfo(nCheDi).nLinkTrain(UBound(ChediInfo(nCheDi).nLinkTrain)) = nTrain
                TrainInf(TempNtrain1).TrainReturn(2) = nTrain
                TrainInf(nTrain).TrainReturn(1) = TempNtrain1
                TrainInf(nTrain).TrainReturn(2) = 0
                Exit Sub
            End If

        End If
    End Sub

    '重编车次
    Public Sub ResetPrintTrainString()
        Dim i, j As Integer
        Dim p As Integer
        Dim DrawTemp() As Integer
        ReDim DrawTemp(0)
        Dim nTrain As Integer
        Dim sStr As String
        For i = 1 To UBound(ChediInfo)
            If UBound(ChediInfo(i).nLinkTrain) > 0 And ChediInfo(i).bIfAutoResetCheCi = True Then
                ReDim Preserve DrawTemp(UBound(DrawTemp) + 1)
                DrawTemp(UBound(DrawTemp)) = i
            End If
        Next i

        '按出库时间排序
        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long

        '按到达时间排序
        Flag = 1
        k = UBound(DrawTemp)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                nTrain = ChediInfo(DrawTemp(j)).nLinkTrain(1)
                TempTime1 = AddLitterTime(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                nTrain = ChediInfo(DrawTemp(j + 1)).nLinkTrain(1)
                Temptime2 = AddLitterTime(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                    temp = DrawTemp(j)
                    DrawTemp(j) = DrawTemp(j + 1)
                    DrawTemp(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop

        Dim tmpJ As Integer
        Dim tmpK() As String
        Dim tpK As Integer
        Dim IfIn As Integer
        Dim sFirName As String
        ReDim tmpK(0)
        For k = 1 To UBound(DrawTemp)
            IfIn = 0
            If UBound(ChediInfo(DrawTemp(k)).nLinkTrain) > 0 Then
                sFirName = TrainInf(ChediInfo(DrawTemp(k)).nLinkTrain(1)).sLineNum
                For i = 1 To UBound(tmpK)
                    If tmpK(i) = sFirName Then
                        IfIn = 1
                        Exit For
                    End If
                Next i

                If IfIn = 0 Then
                    ReDim Preserve tmpK(UBound(tmpK) + 1)
                    tmpK(UBound(tmpK)) = sFirName
                End If
            End If
        Next k

        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                TrainInf(i).Train = FormatPrintTrainHou(i, 3)
            End If
        Next

        Select Case GetUserName()
            Case "上海地铁"
                '根据出库时间确定输出车次号
                sStr = ""
                For p = 1 To UBound(tmpK)
                    tpK = 0
                    For k = 1 To UBound(DrawTemp)
                        For i = 1 To UBound(ChediInfo)
                            If i = DrawTemp(k) Then
                                If UBound(ChediInfo(i).nLinkTrain) > 0 Then
                                    sFirName = TrainInf(ChediInfo(DrawTemp(k)).nLinkTrain(1)).sLineNum
                                    If sFirName = tmpK(p) Then
                                        tpK = tpK + 1
                                        For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                                            tmpJ = j
                                            nTrain = ChediInfo(i).nLinkTrain(j)
                                            sStr = FormatPrintTrain(Trim(Str(tpK)), nTrain, sFirName)

                                            TrainInf(nTrain).sPrintTrain = sStr
                                            'TrainInf(nTrain).Train = FormatPrintTrainHou(Left(sStr, 3), nTrain, tmpJ)
                                            'If TrainInf(nTrain).sPrintTrain = "21601" Then Stop
                                        Next j
                                        If SystemPara.SystemStyle = "地铁" Then
                                            ChediInfo(i).sCheCiHao = Left(sStr, 3)
                                        Else
                                            ChediInfo(i).sCheCiHao = FormatPrintTrainHou(tpK, 3)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        Next i
                    Next k
                Next p
            Case "北京地铁"
                sStr = ""
                For p = 1 To UBound(tmpK)
                    tpK = 1
                    For k = 1 To UBound(DrawTemp)
                        For i = 1 To UBound(ChediInfo)
                            If i = DrawTemp(k) Then
                                If UBound(ChediInfo(i).nLinkTrain) > 0 Then
                                    ChediInfo(i).sCheCiHao = FormatPrintTrainHou(tpK, 2)
                                End If
                                tpK = tpK + 1
                                Exit For
                            End If
                        Next i
                    Next k
                Next p

                '下行排序
                '按列车到站点排序
                Dim ntmpTrain() As Integer
                ReDim ntmpTrain(0)
                For i = 1 To UBound(TrainInf)
                    If TrainInf(i).Train <> "" Then
                        ReDim Preserve ntmpTrain(UBound(ntmpTrain) + 1)
                        ntmpTrain(UBound(ntmpTrain)) = i
                    End If
                Next i
                '按到达时间排序
                Flag = 1
                k = UBound(ntmpTrain)
                Do While Flag > 0
                    k = k - 1
                    Flag = 0
                    For j = 1 To k
                        TempTime1 = AddLitterTime(TrainInf(ntmpTrain(j)).Starting(TrainInf(ntmpTrain(j)).nPathID(1)))
                        Temptime2 = AddLitterTime(TrainInf(ntmpTrain(j + 1)).Starting(TrainInf(ntmpTrain(j + 1)).nPathID(1)))
                        If TempTime1 > Temptime2 Then '
                            temp = ntmpTrain(j)
                            ntmpTrain(j) = ntmpTrain(j + 1)
                            ntmpTrain(j + 1) = temp
                            Flag = 1
                        End If
                    Next j
                Loop

                Dim tmpK1, tmpK2 As Integer
                tmpK1 = 0
                tmpK2 = 0
                For i = 1 To UBound(ntmpTrain)
                    If ntmpTrain(i) Mod 2 = 0 Then
                        tmpK1 = tmpK1 + 1
                        TrainInf(ntmpTrain(i)).sPrintTrain = "2" & FormatPrintTrainHou(tmpK1, 3)
                    Else
                        tmpK2 = tmpK2 + 1
                        TrainInf(ntmpTrain(i)).sPrintTrain = "1" & FormatPrintTrainHou(tmpK2, 3)
                    End If
                Next
                'If TimeTablePara.TimeTableDiagramPara.sCheCiLength = 6 Then
                '    For i = 1 To UBound(ChediInfo)
                '        For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                '            If TrainInf(ChediInfo(i).nLinkTrain(j)).Train <> "" Then
                '                TrainInf(ChediInfo(i).nLinkTrain(j)).sPrintTrain = ChediInfo(i).sPrintCheCiHao & TrainInf(ChediInfo(i).nLinkTrain(j)).sPrintTrain
                '            End If
                '        Next
                '    Next
                'End If
            Case Else

                '根据出库时间确定输出车次号
                sStr = ""
                For p = 1 To UBound(tmpK)
                    tpK = 0
                    For k = 1 To UBound(DrawTemp)
                        For i = 1 To UBound(ChediInfo)
                            If i = DrawTemp(k) Then
                                If UBound(ChediInfo(i).nLinkTrain) > 0 Then
                                    sFirName = TrainInf(ChediInfo(DrawTemp(k)).nLinkTrain(1)).sLineNum
                                    If sFirName = tmpK(p) Then
                                        tpK = tpK + 1
                                        For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                                            tmpJ = j
                                            nTrain = ChediInfo(i).nLinkTrain(j)
                                            sStr = FormatPrintTrain(Trim(Str(tpK)), nTrain, sFirName)

                                            TrainInf(nTrain).sPrintTrain = sStr
                                            'TrainInf(nTrain).Train = FormatPrintTrainHou(Left(sStr, 3), nTrain, tmpJ)
                                            'If TrainInf(nTrain).sPrintTrain = "21601" Then Stop
                                        Next j
                                        If SystemPara.SystemStyle = "地铁" Then
                                            ChediInfo(i).sCheCiHao = Left(sStr, 3)
                                        Else
                                            ChediInfo(i).sCheCiHao = FormatPrintTrainHou(tpK, 3)
                                        End If
                                        Exit For
                                    End If
                                End If
                            End If
                        Next i
                    Next k
                Next p
                'For i = 1 To UBound(TrainInf)
                '    If TrainInf(i).Train <> "" Then
                '        TrainInf(i).sPrintTrain = TrainInf(i).Train
                '    End If
                'Next
        End Select


    End Sub
    '车次后两位格式
    Public Function FormatPrintTrainHou(ByVal nTrain As Integer, ByVal nLength As Integer) As String
        Dim sPTrain As String
        sPTrain = nTrain.ToString.Trim
        Select Case nLength
            Case 2
                If sPTrain.Length = 1 Then
                    sPTrain = "0" & sPTrain
                End If
            Case 3
                If sPTrain.Length = 1 Then
                    sPTrain = "00" & sPTrain
                ElseIf sPTrain.Length = 2 Then
                    sPTrain = "0" & sPTrain
                End If
        End Select

        FormatPrintTrainHou = sPTrain 'sStr & sPTrain
    End Function

    '格式输出车次
    Public Function FormatPrintTrain(ByVal sStr As String, ByVal nTrain As Integer, ByVal NFir As String) As String
        If SystemPara.SystemStyle = "地铁" Then
            Dim sMuDiNum As String
            sStr = Trim(sStr)
            If Len(sStr) = 1 Then
                sStr = "0" & sStr
            End If
            sMuDiNum = TrainInf(nTrain).sMuDiNum
            If Len(sMuDiNum) = 1 Then
                sMuDiNum = "0" & sMuDiNum
            End If
            FormatPrintTrain = NFir & sStr & sMuDiNum
            'If SystemPara.sUserCompanyName.Substring(0, 4) = "北京地铁" Then
            '    FormatPrintTrain = NFir & sStr & sMuDiNum
            'End If

        Else
            sStr = Trim(nTrain)
            If Len(sStr) = 1 Then
                sStr = "00" & sStr
            ElseIf Len(sStr) = 2 Then
                sStr = "0" & sStr
            End If
            FormatPrintTrain = sStr
        End If

        '    For i = 1 To UBound(TrainNumSet)
        '        If TrainInf(nTrain).NextStation = TrainNumSet(i).sStaName And TrainInf(nTrain).TrainStyle = TrainNumSet(i).sTrainStyle Then
        '            FormatPrintTrain = FormatPrintTrain & TrainNumSet(i).sNum
        '            sExist = 1
        '            Exit For
        '        End If
        '    Next i
        '    If sExist = 0 Then
        '    ReDim Preserve TrainErrInf(UBound(TrainErrInf) + 1)
        '        TrainErrInf(UBound(TrainErrInf)).nTrain = TrainInf(NFir).Train & " 次  在 " & TrainInf(nTrain).NextStation & " 站的 目的符没有定义"
        '    End If


    End Function

    '计算整个列车的运行时分,basicTraininf所得
    Public Function CalTrainRunTimeFromTrain(ByVal sJiaoLuName As String, ByVal sRunScale As String, ByVal sStopScale As String) As Long
        'Dim i As Integer
        'Dim j As Integer
        'Dim k As Integer
        Dim sRunTime As Long
        Dim sStopTime As Long
        sRunTime = 0
        sStopTime = 0

        sRunTime = CalTrainRunTimeNotStopFromTrain(sJiaoLuName, sRunScale, sStopScale, 0)

        CalTrainRunTimeFromTrain = sRunTime ' + sStopTime

    End Function

    '计算整个列车的运行时分，basicTraininf所得
    Public Function CalTrainRunTimeNotStopFromTrain(ByVal sJiaoLuName As String, ByVal sRunScale As String, ByVal sStopScale As String, ByVal CalFangShi As Integer) As Long
        'CalFangShi,0表示计停站时间，1表示不计停站时间,2表示只计停站时间
        If sJiaoLuName = "" Then Exit Function
        Dim i As Integer
        Dim lngStopTime As Long
        Dim lngStopTime2 As Long
        Dim nFirSta As Integer
        Dim nSecSta As Integer
        Dim lngCurSecRunTime As Long
        Dim lngStartAppendTime As Long
        Dim lngStopAppendTime As Long
        Dim nTrain As Integer

        Dim sRunTime As Long
        Dim lngToStopTime As Long
        lngToStopTime = 0

        sRunTime = 0
        Dim nScale As Integer
        nScale = GetScaleIDFromName(sRunScale)
        If sStopScale = "" Then Exit Function

        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName = sJiaoLuName Then
                nTrain = i
                Exit For
            End If
        Next i

        For i = 1 To UBound(BasicTrainInf(nTrain).nPassSection)
            If i = 1 Then '第一个区间
                nFirSta = BasicTrainInf(nTrain).nFirstID(i)
                nSecSta = BasicTrainInf(nTrain).nSecondID(i)

                lngCurSecRunTime = GetCurSecRunTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                sRunTime = sRunTime + lngCurSecRunTime
                lngStartAppendTime = GetCurSecStartAppendTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                sRunTime = sRunTime + lngStartAppendTime
                lngStopTime2 = GetCurTrainStopTimeAtStation(sJiaoLuName, sStopScale, StationInf(nSecSta).sStationName)
                If lngStopTime2 > 0 Then
                    lngStopAppendTime = GetCurSecStopAppendTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                    sRunTime = sRunTime + lngStopAppendTime
                End If
            ElseIf i = UBound(BasicTrainInf(nTrain).nPassSection) Then '最后一个区间
                nFirSta = BasicTrainInf(nTrain).nFirstID(i)
                nSecSta = BasicTrainInf(nTrain).nSecondID(i)
                lngStopTime = GetCurTrainStopTimeAtStation(sJiaoLuName, sStopScale, StationInf(nFirSta).sStationName)
                If lngStopTime = 0 Then

                Else
                    If CalFangShi = 0 Then
                        sRunTime = sRunTime + lngStopTime
                    End If
                    lngToStopTime = lngToStopTime + lngStopTime
                End If

                lngCurSecRunTime = GetCurSecRunTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                If lngStopTime > 0 Then
                    lngStartAppendTime = GetCurSecStartAppendTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                Else
                    lngStartAppendTime = 0
                End If
                sRunTime = sRunTime + lngCurSecRunTime + lngStartAppendTime
                lngStopTime2 = GetCurTrainStopTimeAtStation(sJiaoLuName, sStopScale, StationInf(nSecSta).sStationName)
                lngStopAppendTime = GetCurSecStopAppendTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                sRunTime = sRunTime + lngStopAppendTime

            Else

                nFirSta = BasicTrainInf(nTrain).nFirstID(i)
                nSecSta = BasicTrainInf(nTrain).nSecondID(i)
                lngStopTime = GetCurTrainStopTimeAtStation(sJiaoLuName, sStopScale, StationInf(nFirSta).sStationName)
                If lngStopTime = 0 Then

                Else
                    If CalFangShi = 0 Then
                        sRunTime = sRunTime + lngStopTime
                    End If
                    lngToStopTime = lngToStopTime + lngStopTime
                End If

                lngCurSecRunTime = GetCurSecRunTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                If lngStopTime > 0 Then
                    lngStartAppendTime = GetCurSecStartAppendTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                Else
                    lngStartAppendTime = 0
                End If

                sRunTime = sRunTime + lngCurSecRunTime + lngStartAppendTime
                lngStopTime2 = GetCurTrainStopTimeAtStation(sJiaoLuName, sStopScale, StationInf(nSecSta).sStationName)
                If lngStopTime2 > 0 Then
                    lngStopAppendTime = GetCurSecStopAppendTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                    sRunTime = sRunTime + lngStopAppendTime
                End If
            End If
        Next

        If CalFangShi = 2 Then '只计停站
            CalTrainRunTimeNotStopFromTrain = lngToStopTime
        Else
            CalTrainRunTimeNotStopFromTrain = sRunTime
        End If

    End Function

    '根据标尺名称得到标尺ID号
    Public Function GetScaleIDFromName(ByVal sScaleName As String) As Integer
        Dim i As Integer
        For i = 1 To UBound(TrainRunScaleInf)
            If TrainRunScaleInf(i).sScaleName = sScaleName Then
                GetScaleIDFromName = TrainRunScaleInf(i).nScaleID
                Exit For
            End If
        Next i
    End Function

    '重画列车
    Public Sub ResetTrainStartOrEndTime(ByVal nTrain As Integer)

        TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)) = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
        TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(1)) = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
        Call DrawSingleTrain(nTrain, TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)), 0)
    End Sub

    '将列车信息修改至长交路
    Public Sub ResetLongTrain(ByVal nTrain As Integer, ByVal nUporDown As Integer, ByVal sTrainStyle As String)
        Dim i As Integer
        Dim nBaseId As Integer
        Dim sBtime As Long
        Dim sRtime As Long
        Dim sEtime As Long
        Dim nDaoFa As Integer
        sBtime = GetTrainArriOrStartTime(nTrain, 0, 1)
        sEtime = GetTrainArriOrStartTime(nTrain, -1, 0)
        nDaoFa = 0

        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName = sTrainStyle Then
                nBaseId = i
                Exit For
            End If
        Next i


        Dim sEndStation As String
        If nBaseId > 0 Then
            If BasicTrainInf(nBaseId).ComeStation = TrainInf(nTrain).ComeStation Then
                nDaoFa = 1
            ElseIf BasicTrainInf(nBaseId).NextStation = TrainInf(nTrain).NextStation Then
                nDaoFa = 2
            Else
                nDaoFa = 0
            End If
            If nDaoFa <> 0 Then
                Call CopyMainBaseTrainInf(nBaseId, nTrain, TrainInf(nTrain).Train, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).sStopSclaeName)
                Call ResetStopScaleName(nTrain)

                If nDaoFa = 1 Then
                    sRtime = CalTrainRunTimeFromTrain(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).sStopSclaeName)
                    sEtime = TimeAdd(sBtime, sRtime)
                Else
                    sRtime = CalTrainRunTimeFromTrain(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).sStopSclaeName)
                    sBtime = TimeMinus(sEtime, sRtime)
                End If
                Call DrawSingleTrain(nTrain, sBtime, 0)
                TrainInf(nTrain).nCDPuOrNot = 0
            Else '既不在始发站，也不在终到站，但经过始发站，以始发站的点为准
                For i = 1 To UBound(BasicTrainInf(nBaseId).nPathID)
                    If StationInf(BasicTrainInf(nBaseId).nPathID(i)).sStationName = TrainInf(nTrain).ComeStation Then
                        sEndStation = TrainInf(nTrain).ComeStation
                        Call CopyMainBaseTrainInf(nBaseId, nTrain, TrainInf(nTrain).Train, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).sStopSclaeName)
                        Call ResetStopScaleName(nTrain)

                        sRtime = CalTrainRunTimeFromTwoStation(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).sStopSclaeName, BasicTrainInf(nBaseId).ComeStation, sEndStation)
                        sBtime = TimeMinus(sBtime, sRtime)
                        Call DrawSingleTrain(nTrain, sBtime, 0)
                        TrainInf(nTrain).nCDPuOrNot = 0
                        Exit Sub
                    End If
                Next i

                For i = 1 To UBound(TrainInf(nTrain).nPathID)
                    If StationInf(TrainInf(nTrain).nPathID(i)).sStationName = BasicTrainInf(nBaseId).ComeStation Then
                        sBtime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(i))
                        Call CopyMainBaseTrainInf(nBaseId, nTrain, TrainInf(nTrain).Train, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).sStopSclaeName)
                        Call ResetStopScaleName(nTrain)
                        'sRtime = CalTrainRunTimeFromTwoStation(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).sStopSclaeName, BasicTrainInf(nBaseId).ComeStation, sEndStation)

                        TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)) = sBtime
                        Call DrawSingleTrain(nTrain, sBtime, 0)
                        TrainInf(nTrain).nCDPuOrNot = 0
                        Exit Sub
                    End If
                Next i

            End If

        Else
            'MsgBox "列车信息中没有这个交路!"
        End If
    End Sub

    '计算整个列车的运行时分,basicTraininf所得
    Public Function CalTrainRunTimeFromTwoStation(ByVal sJiaoLuName As String, ByVal sRunScale As String, ByVal sStopScale As String, ByVal sFirSta As String, ByVal sEndSta As String) As Long


        Dim i As Integer
        Dim lngStopTime As Long
        Dim lngStopTime2 As Long
        Dim nFirSta As Integer
        Dim nSecSta As Integer
        Dim lngCurSecRunTime As Long
        Dim lngStartAppendTime As Long
        Dim lngStopAppendTime As Long
        Dim nTrain As Integer

        Dim sRunTime As Long
        Dim lngToStopTime As Long
        lngToStopTime = 0

        sRunTime = 0
        Dim nScale As Integer
        nScale = GetScaleIDFromName(sRunScale)
        If sStopScale = "" Then Exit Function

        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName = sJiaoLuName Then
                nTrain = i
                Exit For
            End If
        Next i


        Dim nFirSecID As Integer
        Dim nSecSecID As Integer
        For i = 1 To UBound(BasicTrainInf(nTrain).nPassSection)
            If StationInf(BasicTrainInf(nTrain).nFirstID(i)).sStationName = sFirSta Then
                nFirSecID = i
                Exit For
            End If
        Next

        For i = 1 To UBound(BasicTrainInf(nTrain).nPassSection)
            If StationInf(BasicTrainInf(nTrain).nSecondID(i)).sStationName = sEndSta Then
                nSecSecID = i
                Exit For
            End If
        Next


        For i = nFirSecID To nSecSecID
            If i = 1 Then '第一个区间
                nFirSta = BasicTrainInf(nTrain).nFirstID(i)
                nSecSta = BasicTrainInf(nTrain).nSecondID(i)

                lngCurSecRunTime = GetCurSecRunTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                sRunTime = sRunTime + lngCurSecRunTime
                lngStartAppendTime = GetCurSecStartAppendTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))

                lngStopTime2 = GetCurTrainStopTimeAtStation(sJiaoLuName, sStopScale, StationInf(nSecSta).sStationName)
                If lngStopTime2 > 0 Then
                    lngStopAppendTime = GetCurSecStartAppendTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                    sRunTime = sRunTime + lngStopAppendTime
                End If
            ElseIf i = UBound(BasicTrainInf(nTrain).nPassSection) Then '最后一个区间
                nFirSta = BasicTrainInf(nTrain).nFirstID(i)
                nSecSta = BasicTrainInf(nTrain).nSecondID(i)
                lngStopTime = GetCurTrainStopTimeAtStation(sJiaoLuName, sStopScale, StationInf(nFirSta).sStationName)
                lngToStopTime = lngToStopTime + lngStopTime

                lngCurSecRunTime = GetCurSecRunTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                If lngStopTime > 0 Then
                    lngStartAppendTime = GetCurSecStartAppendTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                Else
                    lngStartAppendTime = 0
                End If
                sRunTime = sRunTime + lngCurSecRunTime + lngStartAppendTime
                lngStopTime2 = GetCurTrainStopTimeAtStation(sJiaoLuName, sStopScale, StationInf(nSecSta).sStationName)
                lngStopAppendTime = GetCurSecStartAppendTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                sRunTime = sRunTime + lngStopAppendTime
            Else
                nFirSta = BasicTrainInf(nTrain).nFirstID(i)
                nSecSta = BasicTrainInf(nTrain).nSecondID(i)
                lngStopTime = GetCurTrainStopTimeAtStation(sJiaoLuName, sStopScale, StationInf(nFirSta).sStationName)
                If lngStopTime = 0 Then

                Else
                    sRunTime = sRunTime + lngStopTime
                    lngToStopTime = lngToStopTime + lngStopTime
                End If

                lngCurSecRunTime = GetCurSecRunTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                If lngStopTime > 0 Then
                    lngStartAppendTime = GetCurSecStartAppendTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                Else
                    lngStartAppendTime = 0
                End If

                sRunTime = sRunTime + lngCurSecRunTime + lngStartAppendTime
                lngStopTime2 = GetCurTrainStopTimeAtStation(sJiaoLuName, sStopScale, StationInf(nSecSta).sStationName)
                If lngStopTime2 > 0 Then
                    lngStopAppendTime = GetCurSecStartAppendTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                    sRunTime = sRunTime + lngStopAppendTime
                End If
                If i = nSecSecID Then '加上最后一个站的停站时间
                    nFirSta = BasicTrainInf(nTrain).nFirstID(i)
                    nSecSta = BasicTrainInf(nTrain).nSecondID(i)
                    lngStopTime = GetCurTrainStopTimeAtStation(sJiaoLuName, sStopScale, StationInf(nSecSta).sStationName)
                    lngToStopTime = lngToStopTime + lngStopTime
                    sRunTime = sRunTime + lngStopTime
                End If
            End If
        Next

        CalTrainRunTimeFromTwoStation = sRunTime



        'Dim i As Integer
        'Dim j As Integer
        'Dim k As Integer
        'Dim sRunTime As Long
        'Dim sStopTime As Long
        'sRunTime = 0
        'sStopTime = 0


        'For i = 1 To UBound(BasicTrainInf)
        '    If BasicTrainInf(i).sJiaoLuName = sJiaoLuName Then
        '        For j = 1 To UBound(BasicTrainInf(i).SecScale)
        '            If BasicTrainInf(i).SecScale(j).sName = sRunScale Then
        '                For k = 1 To UBound(BasicTrainInf(i).SecScale(j).RunTime)
        '                    sRunTime = sRunTime + BasicTrainInf(i).SecScale(j).RunTime(k)
        '                    If BasicTrainInf(i).NextStation = sEndSta Then
        '                        Exit For
        '                    End If
        '                Next k
        '                Exit For
        '            End If
        '            '            If j = UBound(BasicTrainInf(i).SecScale) Then
        '            '                MsgBox "交路为 " & sJiaoLuName & " 的列车不存在标尺名为：" & sRunScale & " 运行标尺!"
        '            '            End If
        '        Next j
        '        For j = 1 To UBound(BasicTrainInf(i).StopScale)
        '            If BasicTrainInf(i).StopScale(j).sName = sStopScale Then
        '                For k = 2 To UBound(BasicTrainInf(i).StopScale(j).StopTime) - 1
        '                    sStopTime = sStopTime + BasicTrainInf(i).StopScale(j).StopTime(k)
        '                    If StationInf(BasicTrainInf(i).StopScale(j).nStopStation(k)).sStationName = sEndSta Then
        '                        Exit For
        '                    End If
        '                Next k
        '                Exit For
        '            End If
        '            '            If j = UBound(BasicTrainInf(i).SecScale) Then
        '            '                MsgBox "交路为 " & sJiaoLuName & " 的列车不存在标尺名为：" & sStopScale & " 停站标尺!"
        '            '            End If

        '        Next j

        '        Exit For
        '    End If
        'Next i

        'CalTrainRunTimeFromTwoStation = sRunTime + sStopTime

    End Function

    '修改列车停站标尺名称
    Public Sub ResetStopScaleName(ByVal nTrain As Integer)
        Dim i, k As Integer
        Dim nStopID As Integer
        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName = TrainInf(nTrain).sJiaoLuName Then
                For k = 1 To UBound(BasicTrainInf(i).StopScale)
                    If BasicTrainInf(i).StopScale(k).sName = TrainInf(nTrain).sStopSclaeName Then
                        nStopID = k
                        Exit For
                    End If
                Next k
                If nStopID = 0 Then '不存这个标尺
                    TrainInf(nTrain).sStopSclaeName = BasicTrainInf(i).StopScale(1).sName
                End If
            End If
        Next i

    End Sub

    '最小折返时间的调整
    Public Sub TZYXLineZheFanMin(ByVal nTrain As Integer)
        Dim SselTrNum As Integer
        Dim Reply As Integer
        Dim DeltaTime As Long
        Dim nFirTrain As Integer
        Dim sZheFantime As Single
        Dim sJianGe As Single
        If nTrain = 0 Then Exit Sub
        nFirTrain = TrainInf(nTrain).TrainReturn(1)
        If nFirTrain = 0 Then
            MsgBox("该列车为车底的第一趟车，不能调整! 请选用鼠标移动进行操作!", , "提示")
            Exit Sub
        End If
        If (nTrain + nFirTrain) Mod 2 = 0 Then
            sZheFantime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, StationInf(TrainInf(nTrain).nPathID(1)).sStationName, "立即折返")
        Else
            sZheFantime = GetZheFanTime(TrainInf(nFirTrain).SCheDiLeiXing, StationInf(TrainInf(nFirTrain).nPathID(UBound(TrainInf(nFirTrain).nPathID))).sStationName, TrainInf(nFirTrain).TrainReturnStyle(2))
        End If
        sJianGe = TimeMinus(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)), TrainInf(nFirTrain).Arrival(TrainInf(nFirTrain).nPathID(UBound(TrainInf(nFirTrain).nPathID))))
        If sJianGe - sZheFantime > 0 Then
            DeltaTime = sJianGe - sZheFantime
            'DeltaTime = DeltaX / CDUnitageX
            Dim strTime As String
            strTime = dTime(DeltaTime, 0)
            Reply = MsgBox("你要将该列车的始发时刻移动" & strTime & "吗", vbOKCancel, "提示")

            If Reply = 1 Then
                Call TrainTZDFSK(nTrain, -DeltaTime)
                SselTrNum = nTrain
            Else
            End If
            'DeltaX = TrainInf(nFirTrain).Arrival(TrainInf(nFirTrain).nPathID(UBound(TrainInf(nFirTrain).nPathID))) + sZheFanTime
        ElseIf sJianGe - sZheFantime = 0 Then
            MsgBox("两列车间隔已是最小折返时间，无法调整！")
            Exit Sub
        Else
            If MsgBox("原来两列车的间隔不满足要求，是否重让" & TrainInf(nTrain).Train & "自动调整到满足折返要求", vbQuestion + vbYesNo + vbDefaultButton2, "确认") = vbYes Then
                DeltaTime = sJianGe - sZheFantime
                Call TrainTZDFSK(nTrain, -DeltaTime)
            End If
        End If

        Call DrawSingleTrain(nTrain, TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)), 0)
        Call addOneUndoInf()
        Call RefreshDiagram(1)
    End Sub

    '调整至最小折返
    Sub TrainTZDFSK(ByVal CheciNo As Integer, ByVal DeltaTime As Long)
        'Dim StartTime As Long
        'Dim ArriveTime As Long
        'Dim nAnoTrain As Integer
        Dim lngStartTime As Long
        lngStartTime = GetTrainArriOrStartTime(CheciNo, 0, 1)
        lngStartTime = TimeAdd(lngStartTime, DeltaTime)
        Call DrawSingleTrain(CheciNo, lngStartTime, 0)

        'If frmMoveTime.nAfterMove = True Then
        '    nAnoTrain = TrainInf(CheciNo).trainreturn(2)
        '    If nAnoTrain > 0 Then
        '        TrainInf(nAnoTrain).lAllStartTime = TimeAdd(TrainInf(nAnoTrain).lAllStartTime, DeltaTime)
        '        TrainInf(nAnoTrain).lAllEndTime = TimeAdd(TrainInf(nAnoTrain).lAllEndTime, DeltaTime)
        '        TrainInf(nAnoTrain).Starting(TrainInf(nAnoTrain).nPathID(1)) = DeleteLitterTime(TrainInf(nAnoTrain).lAllStartTime)
        '        TrainInf(nAnoTrain).Arrival(TrainInf(nAnoTrain).nPathID(UBound(TrainInf(nAnoTrain).nPathID))) = DeleteLitterTime(TrainInf(nAnoTrain).lAllEndTime)
        '        Call DrawSingleTrain(nAnoTrain, TrainInf(nAnoTrain).lAllStartTime)

        '    End If
        'End If

        'If frmMoveTime.nBeforeMove = True Then
        '    nAnoTrain = TrainInf(CheciNo).trainreturn(1)
        '    If nAnoTrain > 0 Then
        '        TrainInf(nAnoTrain).lAllStartTime = TimeAdd(TrainInf(nAnoTrain).lAllStartTime, DeltaTime)
        '        TrainInf(nAnoTrain).lAllEndTime = TimeAdd(TrainInf(nAnoTrain).lAllEndTime, DeltaTime)
        '        TrainInf(nAnoTrain).Starting(TrainInf(nAnoTrain).nPathID(1)) = DeleteLitterTime(TrainInf(nAnoTrain).lAllStartTime)
        '        TrainInf(nAnoTrain).Arrival(TrainInf(nAnoTrain).nPathID(UBound(TrainInf(nAnoTrain).nPathID))) = DeleteLitterTime(TrainInf(nAnoTrain).lAllEndTime)
        '        Call DrawSingleTrain(nAnoTrain, TrainInf(nAnoTrain).lAllStartTime)
        '    End If
        'End If


        '    StartTime = TrainInf(CheciNo).Starting(TrainInf(CheciNo).nPathID(1))
        '    ArriveTime = TrainInf(CheciNo).Arrival(TrainInf(CheciNo).nPathID(UBound(TrainInf(CheciNo).nPathID)))
        '    If StartTime + DeltaTime < 0 Then
        '        StartTime = StartTime + 86400# + DeltaTime
        '    ElseIf StartTime + DeltaTime > 86400# Then
        '        StartTime = StartTime + DeltaTime - 86400#
        '    Else
        '        StartTime = StartTime + DeltaTime
        '    End If
        '
        '    Dim nEndTrn As Integer, nBeginTrn As Integer
        '    Dim lStime As Long, lEtime As Long, lstemp1 As Long, lstemp2 As Long
        '    nBeginTrn = TrainInf(CheciNo).nPathID(1)
        '    nEndTrn = TrainInf(CheciNo).nPathID(UBound(TrainInf(CheciNo).nPathID))
        '    lstemp1 = StartTime
        '    lstemp2 = lstemp1
        '
        '    TiaoDrawline.TDrawLineNoStrainInMetro lstemp1, lstemp2, 0, CheciNo, nBeginTrn, nEndTrn, 1


        '    If ArriveTime + DeltaTime < 0 Then
        '        ArriveTime = ArriveTime + 86400# + DeltaTime
        '    ElseIf ArriveTime + DeltaTime > 86400# Then
        '        ArriveTime = ArriveTime + DeltaTime - 86400#
        '    Else
        '        ArriveTime = ArriveTime + DeltaTime
        '    End If
        '    TrainInf(CheciNo).Starting(TrainInf(CheciNo).nPathID(1)) = StartTime
        '    TrainInf(CheciNo).Arrival(TrainInf(CheciNo).nPathID(UBound(TrainInf(CheciNo).nPathID))) = ArriveTime
    End Sub

    ''调整交路时判断是否能满足间隔要求
    'Public Sub EditJiaoLuLine(ByVal nCheDiNum As Integer)
    '    Dim i As Integer
    '    Dim NFir As Integer
    '    Dim nSec As Integer
    '    Dim sZheFantime As Single
    '    Dim sTime As Single
    '    Dim sTime1 As Long
    '    Dim sTime2 As Long
    '    If nCheDiNum = 0 Then Exit Sub
    '    If UBound(ChediInfo(nCheDiNum).nLinkTrain) > 1 Then
    '        For i = 2 To UBound(ChediInfo(nCheDiNum).nLinkTrain)
    '            NFir = ChediInfo(nCheDiNum).nLinkTrain(i - 1)
    '            'If NFir = 280 Then Stop
    '            nSec = ChediInfo(nCheDiNum).nLinkTrain(i)
    '            If (NFir + nSec) Mod 2 <> 0 Then
    '                sTime2 = TrainInf(NFir).Arrival(TrainInf(NFir).nPathID(UBound(TrainInf(NFir).nPathID)))
    '                sTime1 = AddCompareTime(TrainInf(nSec).Starting(TrainInf(nSec).nPathID(1)), sTime2)
    '                sTime = sTime1 'sTime1 - sTime2
    '                sZheFantime = GetZheFanTime(ChediInfo(nCheDiNum).SCheDiLeiXing, StationInf(TrainInf(NFir).nPathID(UBound(TrainInf(NFir).nPathID))).sStationName, TrainInf(NFir).TrainReturnStyle(2))
    '                If sTime - sZheFantime < 0 Then
    '                    'MsgBox TrainInf(nFir).Train & "与" & TrainInf(nSec).Train & "的折返时间不够"
    '                    ReDim Preserve TrainErrInf(UBound(TrainErrInf) + 1)
    '                    TrainErrInf(UBound(TrainErrInf)).nTrain = TrainInf(NFir).Train & " 次 与 " & TrainInf(nSec).Train & " 次"
    '                    TrainErrInf(UBound(TrainErrInf)).sErrorMessage = "两列车在 " & StationInf(TrainInf(NFir).nPathID(UBound(TrainInf(NFir).nPathID))).sStationName _
    '                        & " 站的折返时间不足，规定折返时间为：" & sZheFantime & " 秒，实际折返时间为：" & sTime & "秒"

    '                    Call DrawLineInCheDiJLTu(NFir, 1, QBColor(11))
    '                    Call DrawLineInCheDiJLTu(nSec, 1, QBColor(11))
    '                    'Call ShowNotSatiLine(NFir)
    '                    'Call ShowNotSatiLine(nSec)
    '                End If
    '            End If
    '        Next i
    '    End If
    'End Sub
    '时间相对比较
    Public Function AddCompareTime(ByVal sTime As Long, ByVal sLeftTime As Long) As Long
        If sTime >= sLeftTime Then
            AddCompareTime = sTime - sLeftTime
        Else
            AddCompareTime = sTime + 24 * 3600.0# - sLeftTime
        End If
        If AddCompareTime > 12 * 3600.0# Then
            AddCompareTime = -1
        End If
    End Function

    '修改0-4点间时间段的时间值，减去24小时
    Public Function DeleteLitterTime(ByVal sTime As Long) As Long
        If sTime >= 24 * 3600.0# And sTime <= 24 * 3600.0# + TimeTablePara.TimeTableDiagramPara.intCompareFirstTime Then
            DeleteLitterTime = sTime - 24 * 3600.0#
        Else
            DeleteLitterTime = sTime
        End If
    End Function

    ''在车底交路图上画运行线
    'Public Sub DrawLineInCheDiJLTu(ByVal nTrain As Integer, ByVal nWidth As Integer, ByVal corColor As ColorConstants)
    '    Dim i As Integer
    '    Dim j As Integer
    '    Dim ArriveTime As Long
    '    Dim StartTime As Long
    '    'If nTrain = 142 Then Stop
    '    ReDim CDJLPrintPlace(0)
    '    For i = 1 To UBound(TrainInf(nTrain).nPathID)
    '        For j = 1 To UBound(ZheFanStation)
    '            If ZheFanStation(j) = StationInf(TrainInf(nTrain).nPathID(i)).sStationName Then
    '                ArriveTime = TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(i))
    '                StartTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(i))
    '                ReDim Preserve CDJLPrintPlace(UBound(CDJLPrintPlace) + 1)
    '                CDJLPrintPlace(UBound(CDJLPrintPlace)).sngX1 = AdjustCheDiTimeX2(ArriveTime, 10, CDBeginTime)
    '                CDJLPrintPlace(UBound(CDJLPrintPlace)).sngY1 = CDStationPlace(TrainInf(nTrain).nPathID(i))
    '                CDJLPrintPlace(UBound(CDJLPrintPlace)).sngX2 = AdjustCheDiTimeX2(StartTime, 10, CDBeginTime)
    '                CDJLPrintPlace(UBound(CDJLPrintPlace)).sngY2 = CDJLPrintPlace(UBound(CDJLPrintPlace)).sngY1
    '                Exit For
    '            End If
    '        Next j
    '    Next i

    '    Dim sngX1 As Single
    '    Dim sngY1 As Single
    '    Dim sngX2 As Single
    '    Dim sngY2 As Single

    '    MainFormCheDi.TU.ForeColor = corColor
    '    MainFormCheDi.TU.DrawWidth = nWidth
    '    For i = 1 To UBound(CDJLPrintPlace) - 1
    '        sngX1 = CDJLPrintPlace(i).sngX2
    '        sngY1 = CDJLPrintPlace(i).sngY2
    '        sngX2 = CDJLPrintPlace(i + 1).sngX1
    '        sngY2 = CDJLPrintPlace(i + 1).sngY1
    '        If sngX1 = -1 Or sngX2 = -1 Then

    '        Else
    '            'If sngX1 > 0 And sngX2 > 0 Then
    '            If sngX1 <= sngX2 Then
    '            MainFormCheDi.TU.Line (sngX1, sngY1)-(sngX2, sngY2)
    '            Else
    '                ' MainFormCheDi.TU.Line (sngX1, sngY1)-(sngX2, sngY2)
    '            End If
    '            'End If
    '        End If
    '    Next i
    '    MainFormCheDi.TU.ForeColor = vbBlack
    '    MainFormCheDi.TU.DrawWidth = 1
    'End Sub

End Module
