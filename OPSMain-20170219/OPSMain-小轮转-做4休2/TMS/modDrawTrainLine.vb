Module modDrawTrainLine
    Public TxDiffTime1(,) As Long          '同向实际时间间隔与I的差值(前行车)
    Public TxIntervalKind1(,) As Integer   '同向时间间隔种类(前行车)
    Public TxDiffTime2(,) As Long          '同向实际时间间隔与I的差值(后行车)
    Public TxIntervalKind2(,) As Integer   '同向时间间隔种类(后行车)
    Public FxDiffTime1(,) As Long          '反向实际时间间隔与I的差值(前行车)
    Public FxIntervalKind1(,) As Integer   '反向时间间隔种类(前行车)
    Public FxDiffTime2(,) As Long          '反向实际时间间隔与I的差值(后行车)
    Public FxIntervalKind2(,) As Integer   '反向时间间隔种类(后行车)
    Public TxDiffTrain1(,) As Integer      '同向前行车
    Public TxDiffTrain2(,) As Integer      '同向后行车
    Public FxDiffTrain1(,) As Integer      '反向前行车
    Public FxDiffTrain2(,) As Integer      '反向后行车

    Public EffTrain() As Integer       '受影响车的次序
    Public InfTrnNum As Integer           '受影响车的数量

    '画单个列车
    Public Sub DrawSingleTrain(ByVal nTrain As Integer, ByVal lStemp As Long, ByVal nCurSta As Integer)

        '画单个列车
        Dim lStime As Long, lstemp1 As Long, lstemp2 As Long
        Dim nEndTrn As Integer, nBeginTrn As Integer
        If nCurSta = 0 Then
            nBeginTrn = nFindStaNum(nTrain, 1)
            lstemp1 = lStemp
            lstemp2 = lStemp ' TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(nBeginTrn))
        Else
            If StationInf(nCurSta).sStationName = TrainInf(nTrain).ComeStation Then
                nBeginTrn = nFindStaNum(nTrain, 1)
                lstemp1 = lStemp
                lstemp2 = lStemp ' TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(nBeginTrn))
            Else
                nBeginTrn = nCurSta
                lstemp1 = TrainInf(nTrain).Arrival(nBeginTrn)
                lstemp2 = lStemp ' TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(nBeginTrn))
            End If
        End If
        nEndTrn = nFindStaNum(nTrain, 2)

        Call TxFxDiffDim(UBound(TrainInf), UBound(StationInf))
        'TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)) = lStemp
        'TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(1)) = lStemp
        TrainInf(nTrain).TrainPuorNot = 0
        If lstemp1 = -1 Then lstemp1 = lstemp2
        If lstemp2 = -1 Then lstemp2 = lstemp1
        If lstemp1 <> -1 And lstemp2 <> -1 Then
            Select Case TimeTablePara.sPubTrainStrainDraw
                Case TrainStrainDraw.有约束
                    ' TDrawLineMaglev(lstemp1, lstemp2, lStime, nTrain, nBeginTrn, nEndTrn, 0)
                    Call TDrawLineOld(lstemp1, lstemp2, lStime, nTrain, nBeginTrn, nEndTrn, 0)
                Case TrainStrainDraw.无约束
                    TDrawLineNoStrainInMetro(lstemp1, lstemp2, lStime, nTrain, nBeginTrn, nEndTrn, 0)
                    'Case 3
                    '    Call DrawSingleTrainLine(nTrain, lStemp)
            End Select
            TrainInf(nTrain).TrainPuorNot = 1
        End If
    End Sub


    '列车通过第一站或最后一站的车站ID号
    Function nFindStaNum(ByVal nTrnNum As Integer, ByVal nStarArri As Integer) As Integer
        'nStatemp当前车站名
        '确定列车所经过车站的编号(包括始发车站编号）
        If nStarArri = 1 Then
            '出发
            If TrainInf(nTrnNum).Train <> "" Then
                nFindStaNum = TrainInf(nTrnNum).nPathID(1)
            End If
        ElseIf nStarArri = 2 Then
            '到达
            If TrainInf(nTrnNum).Train <> "" Then
                nFindStaNum = TrainInf(nTrnNum).nPathID(UBound(TrainInf(nTrnNum).nPathID))
            End If
        End If
    End Function

    ''地铁图,无约束铺画
    'Sub TDrawLineNoStrainInMetro(ByVal lArrivalTime As Long, ByVal lStartTime As Long, ByVal lSDtime As Long, ByVal nTrnNum As Integer, _
    'ByVal nBeginSta As Integer, ByVal nEndSta As Integer, ByVal nTiaoorPu As Integer)
    '    'nTiaoorPu调整（1），铺画（0）

    '    Dim nUporDown As Integer, nTrnState As Integer
    '    Dim nStation As Integer, nQStation As Integer
    '    Dim nTempSt As Integer, nPhFx As Integer
    '    Dim lStoporNot As Integer

    '    nBstation = nBeginSta
    '    nEstation = nEndSta
    '    nPhFx = 1
    '    '中间变量初试化
    '    nStation = nBstation
    '    nUporDown = nDirection(nTrnNum)
    '    Initial(nTrnNum, nBstation, nEstation)
    '    nTrnState = nBeginDeal(lArrivalTime, lStartTime, nStation, nTrnNum)
    '    Do
    '        With TrainInf(nTrnNum)
    '            nQStation = nFindQstaNum(nTrnNum, nStation, nUporDown)
    '            nStation = nQStation - nSteplen(nTrnNum)
    '            Select Case nTrnState
    '                Case 1
    '                    '出发处理（从nStation站发往nQStation站）
    '                    Select Case nStartstation(nTrnNum, nStation, nPhFx)
    '                        Case 1
    '                            '始发站处理
    '                            'lArrivalTime = DealTrainStart(nTrnNum, nStation, lStartTime)
    '                            RecordNoStrainInMetro(nTrnNum, nStation, lStartTime, lStartTime)
    '                            'DealIfStartStationInMetro nTrnNum, nStation, lStartTime, lArrivalTime, 1
    '                            lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))

    '                        Case 2
    '                            '接入站处理
    '                            RecordNoStrainInMetro(nTrnNum, nStation, lStartTime, lArrivalTime)
    '                            If lArrivalTime <> lStartTime Then
    '                                lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
    '                            End If
    '                        Case 3
    '                            '平移站处理
    '                            RecordNoStrainInMetro(nTrnNum, nStation, lStartTime, lArrivalTime)
    '                            If lStartTime <> lArrivalTime Then
    '                                lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
    '                            End If
    '                        Case Else
    '                            RecordNoStrainInMetro(nTrnNum, nStation, lStartTime, lArrivalTime)
    '                            lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
    '                    End Select
    '                    If nEndStation(nTrnNum, nStation, nPhFx) <> 0 Then
    '                        '本条运行线的终点（终到站、交出站、平移站）
    '                        Exit Do
    '                    End If
    '                    nTrnState = 2
    '                Case 2
    '                    '通过处理（从nStation站通过nQStation站）
    '                    lArrivalTime = TimeAdd(lStartTime, TimeRunByBiaoChiTrain(nTrnNum, nStation, nQStation))
    '                    lStartTime = lArrivalTime
    '                    'lStoporNot = StoporPass(nTrnNum, nQStation, lSDtime, lArrivalTime, nUporDown, 1)
    '                    lStoporNot = GetCurTrainStopTimeAtStation(nTrnNum, StationInf(nQStation).sStationName) '
    '                    If lStoporNot = 0 Then
    '                        '不需停车
    '                        RecordNoStrainInMetro(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
    '                        nStation = nQStation
    '                        nTrnState = 2
    '                        Select Case nEndStation(nTrnNum, nStation, nPhFx)
    '                            Case 1, 2
    '                                Exit Do
    '                            Case 0, 3
    '                        End Select
    '                    Else
    '                        '需要停车
    '                        nTempSt = nFindHstaNum(nTrnNum, nQStation, nUporDown)
    '                        nTempSt = nBeginStation(nTrnNum, nTempSt)
    '                        lArrivalTime = TimeAdd(lArrivalTime, TimeT(nTrnNum, nTempSt, nQStation))
    '                        lStartTime = TimeAdd(lArrivalTime, lStoporNot)
    '                        nStation = nTempSt
    '                        nTrnState = 3
    '                    End If
    '                Case 3
    '                    '到达处理（从nStation站到达nQStation站）
    '                    nStation = nQStation
    '                    nTrnState = 1
    '                    RecordNoStrainInMetro(nTrnNum, nQStation, lArrivalTime, lArrivalTime)

    '            End Select
    '        End With
    '    Loop Until nStation = nEstation

    'End Sub

    '确定上下行
    Function nDirection(ByVal nTrnNum As Integer) As Integer
        'ntrnnum本次列车编号
        If nTrnNum Mod 2 <> 0 Then
            nDirection = 1
        Else
            nDirection = 2
        End If
    End Function

    '初始化
    Sub Initial(ByVal nTrnNum As Integer, ByVal nBsta As Integer, ByVal nEsta As Integer)
        Dim i As Integer, nUpDowntemp As Integer
        Dim nTrn As Integer
        nTrn = nTrnNum

        For i = 1 To UBound(TrainInf(nTrnNum).StopLine)
            TrainInf(nTrnNum).StopLine(i) = ""
        Next

        With TrainInf(nTrn)
            If StationInf(nBsta).sStationName <> .ComeStation Then
                Record(nTrn, nBsta, -1, .Arrival(nBsta))
            Else
                Record(nTrn, nBsta, -1, -1)
            End If
            TxDiffTime1(nTrn, nBsta) = 0
            TxDiffTime2(nTrn, nBsta) = 0
            FxDiffTime1(nTrn, nBsta) = 0
            FxDiffTime2(nTrn, nBsta) = 0
            i = nBsta
            nUpDowntemp = nDirection(nTrn)
            Do While i <> nEsta
                i = nFindQstaNum(nTrn, i, nUpDowntemp)
                If i = nEsta Then
                    If .TrainReturn(2) <> 0 Then
                        If TrainInf(.TrainReturn(2)).TrainPuorNot = 0 Then
                            .StopLine(i) = ""
                        End If
                    End If
                End If
                .StopLineTime(i) = 0
                .StopLine(i) = ""
                TxDiffTime1(nTrn, i) = 0
                TxDiffTime2(nTrn, i) = 0
                FxDiffTime1(nTrn, i) = 0
                FxDiffTime2(nTrn, i) = 0
                If StationInf(nEsta).sStationName <> .NextStation And i = nEsta Then '平移站
                    Record(nTrn, nEsta, .Starting(nEsta), -1)
                Else
                    Record(nTrn, i, -1, -1)
                End If
                If StationInf(i).sStationName = .NextStation Then Exit Do
            Loop
        End With
    End Sub

    '修改时间
    Sub Record(ByVal nTrnNum As Integer, ByVal nStation As Integer, ByVal lStartTime As Long, ByVal lArrivalTime As Long)
        Dim i As Integer
        ' Dim nAnotherTrnNum As Integer
        Dim nBegintemp As Integer, nEndtemp As Integer

        'nBegintemp = nBeginDataStation(nNowDataReadLineNum)
        'nEndtemp = nEndDataStation(nNowDataReadLineNum)
        'If StationInf(nStation).sStationName = "广州东" Then Stop
        nBegintemp = 1
        nEndtemp = UBound(StationInf)
        For i = nBegintemp To nEndtemp
            If StationInf(i).sStationName = StationInf(nStation).sStationName Then
                With TrainInf(nTrnNum)
                    .Arrival(i) = lArrivalTime
                    .Starting(i) = lStartTime
                    TxDiffTime1(nTrnNum, i) = TxDiffTime1(nTrnNum, nStation)
                    TxDiffTime2(nTrnNum, i) = TxDiffTime2(nTrnNum, nStation)
                    FxDiffTime1(nTrnNum, i) = FxDiffTime1(nTrnNum, nStation)
                    FxDiffTime2(nTrnNum, i) = FxDiffTime2(nTrnNum, nStation)
                    .StopLine(i) = .StopLine(nStation)
                End With
                'If nAnotherTrnNum <> 0 Then
                '    With TrainInf(nAnotherTrnNum)
                '        .Arrival(i) = TrainInf(nTrnNum).Arrival(i)
                '        .Starting(i) = TrainInf(nTrnNum).Starting(i)
                '        TxDiffTime1(nAnotherTrnNum, i) = TxDiffTime1(nTrnNum, i)
                '        TxDiffTime2(nAnotherTrnNum, i) = TxDiffTime2(nTrnNum, i)
                '        FxDiffTime1(nAnotherTrnNum, i) = FxDiffTime1(nTrnNum, i)
                '        FxDiffTime2(nAnotherTrnNum, i) = FxDiffTime2(nTrnNum, i)
                '        .StopLine(i) = TrainInf(nTrnNum).StopLine(i)
                '    End With
                'End If
            End If
        Next i
    End Sub

    '修改时间
    Sub RecordStaTime(ByVal nTrnNum As Integer, ByVal nStation As Integer, ByVal lStartTime As Long, ByVal lArrivalTime As Long)
        Dim i As Integer
        ' If nTrnNum = 102 Then Stop
        For i = 1 To UBound(StationInf)
            If StationInf(i).sStationName = StationInf(nStation).sStationName Then
                With TrainInf(nTrnNum)
                    .Arrival(i) = lArrivalTime
                    .Starting(i) = lStartTime
                    .StopLine(i) = .StopLine(nStation)
                End With
            End If
        Next i
    End Sub

    '确定前方站编号    
    Function nFindQstaNum(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal nUporDown As Integer) As Integer 'new
        If nStatemp = 0 Then
            nFindQstaNum = TrainInf(nTrnNum).nSecondID(UBound(TrainInf(nTrnNum).nPassSection))
        Else
            Dim i As Integer
            For i = 1 To UBound(TrainInf(nTrnNum).nPassSection)
                If StationInf(TrainInf(nTrnNum).nFirstID(i)).sStationName = StationInf(nStatemp).sStationName Then
                    nFindQstaNum = TrainInf(nTrnNum).nSecondID(i)
                    Exit For
                End If
            Next
        End If
        If nFindQstaNum = 0 Then nFindQstaNum = nStatemp


        'Dim i As Integer
        'Dim nTemp As Integer
        'For i = 1 To UBound(TrainInf(nTrnNum).nPathID)
        '    If StationInf(nStatemp).sStationName = StationInf(TrainInf(nTrnNum).nPathID(i)).sStationName Then
        '        If nStatemp = TrainInf(nTrnNum).nPathID(UBound(TrainInf(nTrnNum).nPathID)) Then
        '            nFindQstaNum = nStatemp
        '            Exit Function
        '        Else
        '            nTemp = i
        '            Exit For
        '        End If
        '    End If
        'Next i
        'For i = nTemp + 1 To UBound(TrainInf(nTrnNum).nPathID)
        '    If StationInf(TrainInf(nTrnNum).nPathID(nTemp)).sStationName <> StationInf(TrainInf(nTrnNum).nPathID(i)).sStationName Then
        '        nFindQstaNum = TrainInf(nTrnNum).nPathID(i)
        '        Exit For
        '    End If
        'Next i
    End Function

    '计算开始
    Function nBeginDeal(ByVal lATime As Long, ByVal lStime As Long, ByVal nStatemp As Integer, ByVal nTrainNum As Integer) As Integer
        With TrainInf(nTrainNum)
            If StationInf(nStatemp).sStationName = .StartStation Then
                nBeginDeal = 1
            Else
                If StationInf(nStatemp).sStationName = .ComeStation Then
                    If lATime = lStime Then
                        nBeginDeal = 2
                        Record(nTrainNum, nStatemp, lStime, lATime)
                    Else
                        nBeginDeal = 1
                    End If
                Else
                    If lATime = lStime Then
                        nBeginDeal = 2
                        Record(nTrainNum, nStatemp, lStime, lATime)
                    Else
                        nBeginDeal = 1
                    End If
                End If
            End If
        End With
    End Function

    '确定循环步长
    Function nSteplen(ByVal nTrnNum As Integer) As Integer
        'ntrnnum本次列车编号
        If nTrnNum Mod 2 <> 0 Then
            nSteplen = 1
        Else
            nSteplen = -1
        End If
    End Function

    Function nStartstation(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal nPhFx As Integer) As Integer
        '始发站处理
        Select Case nPhFx
            Case 1
                nStartstation = 0
                If StationInf(nStatemp).sStationName = StationInf(nBstation).sStationName Then
                    If TrainInf(nTrnNum).ComeStation = StationInf(nStatemp).sStationName Then
                        If TrainInf(nTrnNum).ComeStation = TrainInf(nTrnNum).StartStation Then
                            'nStatemp是始发站
                            nStartstation = 1
                        ElseIf TrainInf(nTrnNum).ComeStation <> TrainInf(nTrnNum).StartStation Then
                            'nStatemp不是始发站，但是接入站
                            nStartstation = 2
                        End If
                    Else
                        'nStatemp不是始发站，也不是接入站,是平移运行线而得
                        nStartstation = 3
                    End If
                End If
            Case 2
                nStartstation = 0
                With TrainInf(nTrnNum)
                    If StationInf(nStatemp).sStationName = StationInf(nEstation).sStationName Then
                        If .ComeStation = StationInf(nStatemp).sStationName Then
                            If .ComeStation = .StartStation Then
                                'nStatemp是始发站
                                nStartstation = 1
                            ElseIf .ComeStation <> .StartStation Then
                                'nStatemp不是始发站，但是接入站
                                nStartstation = 2
                            End If
                        Else
                            'nStatemp不是始发站，也不是接入站,是平移运行线而得
                            nStartstation = 3
                        End If
                    End If
                End With
        End Select
    End Function

    Function TimeT(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal nQstatemp As Integer) As Long
        '    TimeT = 0 '起始附加时分设为0,如果需要加上，请修改！
        Dim nTemp As Integer
        Dim nUpDowntemp As Integer
        Dim k As Integer
        Dim nScaleID As Integer
        nUpDowntemp = nDirection(nTrnNum)
        nTemp = nFindSecNum(nStatemp, nQstatemp, nUpDowntemp)

        Dim sRunScaleName As String
        sRunScaleName = TrainInf(nTrnNum).sRunScaleName
        If nTemp = 0 Then
            TimeT = 0
            Exit Function
        End If
        For k = 1 To UBound(SectionInf(nTemp).SecScale)
            If SectionInf(nTemp).SecScale(k).sScaleName = sRunScaleName Then
                nScaleID = k
                Exit For
            End If
        Next k
        With TrainInf(nTrnNum)
            If nTemp <> 0 Then
                If nUpDowntemp = 1 Then
                    TimeT = SectionInf(nTemp).SecScale(nScaleID).sngDownAppendStopTime
                Else
                    TimeT = SectionInf(nTemp).SecScale(nScaleID).sngUpAppendStopTime
                End If
            End If
        End With
    End Function

    Function TimeQ(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal nQstatemp As Integer) As Long
        '    TimeQ = 0 '起始附加时分设为0,如果需要加上，请修改！
        Dim nScaleID As Integer
        Dim k As Integer
        Dim nTemp As Integer
        Dim nUpDowntemp As Integer

        nUpDowntemp = nDirection(nTrnNum)
        nTemp = nFindSecNum(nStatemp, nQstatemp, nUpDowntemp)
        Dim sRunScaleName As String
        sRunScaleName = TrainInf(nTrnNum).sRunScaleName

        If nTemp = 0 Then
            TimeQ = 0
            Exit Function
        End If
        For k = 1 To UBound(SectionInf(nTemp).SecScale)
            If SectionInf(nTemp).SecScale(k).sScaleName = sRunScaleName Then
                nScaleID = k
                Exit For
            End If
        Next k

        With TrainInf(nTrnNum)
            If nTemp <> 0 Then
                If nUpDowntemp = 1 Then
                    TimeQ = SectionInf(nTemp).SecScale(nScaleID).sngDownAppendStartTime
                Else
                    TimeQ = SectionInf(nTemp).SecScale(nScaleID).sngUpAppendStartTime
                End If
            End If
        End With
    End Function

    Function nEndStation(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal nPhFx As Integer) As Integer
        '终到站处理
        Select Case nPhFx
            Case 1
                nEndStation = 0
                With TrainInf(nTrnNum)
                    If StationInf(nStatemp).sStationName = StationInf(nEstation).sStationName And nStatemp = TrainInf(nTrnNum).nPathID(UBound(TrainInf(nTrnNum).nPathID)) Then
                        If .NextStation = StationInf(nStatemp).sStationName Then
                            If .NextStation = .EndStation Then
                                'nStatemp是终到站
                                nEndStation = 1
                            ElseIf .NextStation <> .EndStation Then
                                'nStatemp不是终到站，但是交出站
                                nEndStation = 2
                            End If
                        Else
                            'nStatemp不是终到站，也不是交出站,是平移运行线而得
                            nEndStation = 3
                        End If
                    End If
                End With
            Case 2
                nEndStation = 0
                With TrainInf(nTrnNum)
                    If StationInf(nStatemp).sStationName = StationInf(nBstation).sStationName Then
                        If .NextStation = StationInf(nStatemp).sStationName Then
                            If .NextStation = .EndStation Then
                                'nStatemp是终到站
                                nEndStation = 1
                            ElseIf .NextStation <> .EndStation Then
                                'nStatemp不是终到站，但是交出站
                                nEndStation = 2
                            End If
                        Else
                            'nStatemp不是终到站，也不是交出站,是平移运行线而得
                            nEndStation = 3
                        End If
                    End If
                End With
        End Select
    End Function

  
    '修改时间,地铁图编制，取消所有的约束条件,指定股道用途及股道号
    Sub RecordNoStrainInMetro(ByVal nTrnNum As Integer, ByVal nStation As Integer, ByVal lStartTime As Long, ByVal lArrivalTime As Long, ByVal sGuDaoNumtmp As String)
        Dim i As Integer, nAnotherTrnNum As Integer
        Dim nBegintemp As Integer, nEndtemp As Integer

        'nBegintemp = nBeginDataStation(nNowDataReadLineNum)
        'nEndtemp = nEndDataStation(nNowDataReadLineNum)
        nBegintemp = 1
        nEndtemp = UBound(StationInf)
        Dim nGudaoNum As Integer
        If sGuDaoNumtmp = "" Or sGuDaoNumtmp = "无" Then
            ReDim KeYongGD(0)
            Call SeekKeYongGD(nTrnNum, nStation) '找满足条件的股道
            If UBound(KeYongGD) > 0 Then
                nGudaoNum = StationInf(nStation).sStLineNo(KeYongGD(1))
            Else
                ' MsgBox("找不到股道")
                If nTrnNum Mod 2 = 0 Then
                    nGudaoNum = 2
                Else
                    nGudaoNum = 1
                End If
                'Stop
            End If
        Else
            nGudaoNum = sGuDaoNumtmp
        End If
            Dim sSta As String
            sSta = StationInf(nStation).sStationName
            For i = nBegintemp To nEndtemp
                If StationInf(i).sStationName = StationInf(nStation).sStationName Then
                    With TrainInf(nTrnNum)
                        .Arrival(i) = lArrivalTime
                        .Starting(i) = lStartTime
                        TxDiffTime1(nTrnNum, i) = TxDiffTime1(nTrnNum, nStation)
                        TxDiffTime2(nTrnNum, i) = TxDiffTime2(nTrnNum, nStation)
                        FxDiffTime1(nTrnNum, i) = FxDiffTime1(nTrnNum, nStation)
                        FxDiffTime2(nTrnNum, i) = FxDiffTime2(nTrnNum, nStation)
                        .StopLine(i) = nGudaoNum ' .StopLine(nStation)
                    End With
                    If nAnotherTrnNum <> 0 Then
                        With TrainInf(nAnotherTrnNum)
                            .Arrival(i) = TrainInf(nTrnNum).Arrival(i)
                            .Starting(i) = TrainInf(nTrnNum).Starting(i)
                            TxDiffTime1(nAnotherTrnNum, i) = TxDiffTime1(nTrnNum, i)
                            TxDiffTime2(nAnotherTrnNum, i) = TxDiffTime2(nTrnNum, i)
                            FxDiffTime1(nAnotherTrnNum, i) = FxDiffTime1(nTrnNum, i)
                            FxDiffTime2(nAnotherTrnNum, i) = FxDiffTime2(nTrnNum, i)
                            .StopLine(i) = nGudaoNum 'TrainInf(nTrnNum).StopLine(i)
                        End With
                    End If
                End If
            Next i
    End Sub

    Function TimeRunByBiaoChiTrain(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal nQstatemp As Integer) As Long
        Dim nTemp As Integer
        Dim nUpDowntemp As Integer
        nUpDowntemp = nDirection(nTrnNum)
        nTemp = nFindSecNum(nStatemp, nQstatemp, nUpDowntemp)

        If nUpDowntemp = 1 Then
            TimeRunByBiaoChiTrain = SectionInf(nTemp).SecScale(TrainInf(nTrnNum).nTrainTimeKind).sngDownTime
        Else
            TimeRunByBiaoChiTrain = SectionInf(nTemp).SecScale(TrainInf(nTrnNum).nTrainTimeKind).sngUpTime
        End If
    End Function

    '根据运行种类得到运行时分
    Function TimeRunByBiaoChiScale(ByVal sJiaoLuName As String, ByVal sRunScaleName As String, ByVal sSecName As String) As Long
        Dim i, j, k As Integer

        TimeRunByBiaoChiScale = 0
        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName = sJiaoLuName Then ' StationInf(nStatemp).sStationName
                For j = 1 To UBound(BasicTrainInf(i).SecScale)
                    If BasicTrainInf(i).SecScale(j).sName = sRunScaleName Then
                        For k = 1 To UBound(BasicTrainInf(i).SecScale(j).nSecID)
                            If SectionInf(BasicTrainInf(i).SecScale(j).nSecID(k)).sSecName = sSecName Then
                                TimeRunByBiaoChiScale = BasicTrainInf(i).SecScale(j).RunTime(k)
                                Exit For
                            End If
                        Next k
                        Exit For
                    End If
                Next j
                Exit For
            End If
        Next i
    End Function

    '根据运行种类得到运行标尺名称
    Function TimeRunScaleNameByBiaoChiScale(ByVal sJiaoLuName As String, ByVal sRunScaleName As String, ByVal sSecName As String) As String
        Dim i, j, k As Integer
        TimeRunScaleNameByBiaoChiScale = ""
        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName = sJiaoLuName Then ' StationInf(nStatemp).sStationName
                For j = 1 To UBound(BasicTrainInf(i).SecScale)
                    If BasicTrainInf(i).SecScale(j).sName = sRunScaleName Then
                        For k = 1 To UBound(BasicTrainInf(i).SecScale(j).nSecID)
                            If SectionInf(BasicTrainInf(i).SecScale(j).nSecID(k)).sSecName = sSecName Then
                                TimeRunScaleNameByBiaoChiScale = BasicTrainInf(i).SecScale(j).sScaleName(k)
                                Exit For
                            End If
                        Next k
                        Exit For
                    End If
                Next j
                Exit For
            End If
        Next i
    End Function


    Function nFindSecNum(ByVal nSectionBeginStation As Integer, ByVal nSectionEndStation As Integer, _
    ByVal nUpDowntemp As Integer) As Integer
        Dim i As Integer

        nFindSecNum = 0
        Dim sSec1, sSec2 As String
        sSec1 = StationInf(nSectionBeginStation).sStationName & "->" & StationInf(nSectionEndStation).sStationName
        sSec2 = StationInf(nSectionEndStation).sStationName & "->" & StationInf(nSectionBeginStation).sStationName

        For i = 1 To UBound(SectionInf)
            If SectionInf(i).sSecName = sSec1 Or SectionInf(i).sSecName = sSec2 Then
                nFindSecNum = i
                Exit For
            End If
        Next i

        If nFindSecNum = 0 Then
            For i = 1 To UBound(SectionInf)
                If nUpDowntemp = 1 Then
                    If SectionInf(i).nQStation = nSectionEndStation Then
                        nFindSecNum = i
                        Exit For
                    End If
                Else
                    If SectionInf(i).nHStation = nSectionBeginStation Then
                        nFindSecNum = i
                        Exit For
                    End If
                End If
            Next i
        End If
    End Function


    Function nFindHstaNum(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal nUporDown As Integer) As Integer
        Dim i As Integer
        Dim nTemp As Integer
        For i = 1 To UBound(TrainInf(nTrnNum).nPathID)
            If StationInf(nStatemp).sStationName = StationInf(TrainInf(nTrnNum).nPathID(i)).sStationName Then
                If nStatemp = TrainInf(nTrnNum).nPathID(1) Then
                    nFindHstaNum = nStatemp
                    Exit Function
                Else
                    nTemp = i
                    Exit For
                End If
            End If
        Next i
        For i = nTemp - 1 To 1 Step -1
            If StationInf(TrainInf(nTrnNum).nPathID(nTemp)).sStationName <> StationInf(TrainInf(nTrnNum).nPathID(i)).sStationName Then
                nFindHstaNum = TrainInf(nTrnNum).nPathID(i)
                Exit For
            End If
        Next i
    End Function

    Function nBeginStation(ByVal nTrainNumber As Integer, ByVal nStationNumber As Integer) As Integer
        nBeginStation = nStationNumber
        If StationInf(nStationNumber).sStationName = TrainInf(nTrainNumber).ComeStation Then
            nBeginStation = nFindStaNum(nTrainNumber, 1) 'nBstation
        End If
    End Function

    ''新增列车
    Public Function FaAddNewTrainInf(ByVal sJiaoLuName As String, ByVal sTrainStyle As String, ByVal sStopScale As String, ByVal sCheci As String, ByVal sTime As Long) As Integer

        Dim nNewTrain As Integer
        'Dim sTrain As String
        nNewTrain = AddTrainInformation(sJiaoLuName, sTrainStyle, sStopScale, sCheci)

        If nNewTrain > 0 Then
            FaAddNewTrainInf = nNewTrain
            'Dim nKind As String
            'Dim tmpRunTime As Long
            Dim sTmpTime As Long
            sTmpTime = sTime

            '    TrainInf(nNewTrain) = nKind
            '    TrainInf(nNewTrain).sTrainTimeScale = TimeKindToTimeScale(nKind)
            '    tmpRunTime = CalTrainRunTimeFromTrain(nNewTrain, nKind)
            '
            '    TrainInf(nNewTrain).lAllStartTime = DeleteLitterTime(sTime)
            '    TrainInf(nNewTrain).lAllEndTime = TimeAdd(TrainInf(nNewTrain).lAllStartTime, tmpRunTime)
            '    TrainInf(nNewTrain).Starting(TrainInf(nNewTrain).nPathID(1)) = TrainInf(nNewTrain).lAllStartTime
            '    TrainInf(nNewTrain).Arrival(TrainInf(nNewTrain).nPathID(UBound(TrainInf(nNewTrain).nPathID))) = TrainInf(nNewTrain).lAllEndTime

            Call DrawSingleTrain(nNewTrain, sTmpTime, 0)
            'Call AddTrainToCheDiInfo(nNewTrain)
        Else
            FaAddNewTrainInf = 0
        End If
    End Function

    '将新增列车添加至车底信息中
    Public Sub AddTrainToNewCheDiInfo(ByVal nTrain As Integer)
        Dim nChediID As Integer
        nChediID = AddNewChediInfor()
        If nChediID > 0 Then
            Call AddLianGuaCheCi(nChediID, nTrain)
        Else
            If nTrain > 0 Then
                Call AddTrainToCheDiInfo(nTrain)
            End If
        End If

    End Sub
    Public Sub AddTrainToCheDiInfo(ByVal nTrain As Integer)
        Dim i As Integer
        Dim Cdid As Integer
        For i = 1 To UBound(ChediInfo)
            If UBound(ChediInfo(i).nLinkTrain) = 0 And ChediInfo(i).SCheDiLeiXing = "临时车底" Then
                Cdid = i
                Exit For
            End If
        Next i
        If Cdid = 0 Then
            ReDim Preserve ChediInfo(UBound(ChediInfo) + 1)
            Cdid = UBound(ChediInfo)
        End If
        Call CopyCheDiInformation(Cdid, Str(Cdid))
        ChediInfo(Cdid).sCheCiHao = Cdid
        TrainInf(nTrain).SCheDiLeiXing = ChediInfo(Cdid).SCheDiLeiXing
        ReDim ChediInfo(Cdid).nLinkTrain(0)
        Call AddLianGuaCheCi(Cdid, nTrain)

    End Sub

    '新增一个临时车底
    Public Function AddNewChediInfor() As Integer
        Dim i As Integer
        Dim nCheDINum As Integer
        For i = 1 To UBound(ChediInfo)
            If UBound(ChediInfo(i).nLinkTrain) = 0 Then
                AddNewChediInfor = i
                Exit For
            End If
        Next
        If AddNewChediInfor = 0 Then
            nCheDINum = UBound(ChediInfo) + 1
            ReDim Preserve ChediInfo(nCheDINum)
            Call CopyCheDiInformation(nCheDINum, nCheDINum)
            AddNewChediInfor = nCheDINum
        End If
    End Function

    '复制车底信息
    Public Sub CopyCheDiInformation(ByVal nNewCDid As Integer, ByVal sCheDiID As String)
        Dim i As Integer
        'Dim tmpChe1 As String
        'Dim tmpChe2 As String
        i = 1

        'Dim sCheCi As String
        'sCheCi = nNewCDid.ToString.Trim
        'If sCheCi.Length = 1 Then
        '    sCheCi = "00" & sCheCi
        'ElseIf sCheCi.Length = 2 Then
        '    sCheCi = "0" & sCheCi
        'End If
        With ChediInfo(nNewCDid)
            .SCheDiLeiXing = BaseChediInfo(i).SCheDiLeiXing
            .sCheDiID = nNewCDid 'BaseChediInfo(i).sCheDiID
            .bIfAutoResetCheCi = True
            If sCheDiID = "NULL" Then
                sCheDiID = ""
            End If
            .sCheCiHao = sCheDiID
            'tmpChe1 = GetTrainPro(sCheDiID, 1)
            'tmpChe2 = GetTrainPro(sCheDiID, 2)
            'If tmpChe1 = "" Then
            '    .sCheCiHao = sCheDiID ' rsDb.Fields("车底ID")
            'Else
            '    .sCheCiHao = tmpChe1 ' rsDb.Fields("车底ID")
            '.sPrintCheCiHao = tmpChe2 ' rsDb.Fields("车底ID")
            'End If

            '.sDayBeginStation = BaseChediInfo(i).sDayBeginStation
            '.sDayEndStation = BaseChediInfo(i).sDayEndStation
            '.nYunxingBiaochi = BaseChediInfo(i).nYunxingBiaochi
            '.bIfGouWang = BaseChediInfo(i).bIfGouWang
            '.sChuKuTime = BaseChediInfo(i).sChuKuTime
            '.sRuKuTime = BaseChediInfo(i).sRuKuTime
            '.sBeiZhu = BaseChediInfo(i).sBeiZhu
            '.sYunYongFangShi = BaseChediInfo(i).sYunYongFangShi
            '.nIfFixCheDi = BaseChediInfo(i).nIfFixCheDi
            ReDim .nLinkTrain(0)
            .PrintCheDiLinkStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.CheDiLineStyle)
            .PrintCheDiLinkWidth = TimeTablePara.DiagramStylePara.CheDiLineWidth
            .PrintCheDiLinkColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.CheDiLineColor)

        End With
        '    End If
        'Next i
    End Sub

    Public Sub AddLianGuaCheCi(ByVal nCheDi As Integer, ByVal nTrain As Integer)
        'If nCheDi = 16 Then Stop
        'If nTrain = 438 Then Stop
        Dim i, j As Integer
        Dim TempNtrain As Integer
        Dim TempNtrain1 As Integer
        Dim sTime As Single
        Dim sTime1 As Single
        Dim sTime2 As Single
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
            'ReDim Preserve ChediInfo(nCheDi).nDayItem(UBound(ChediInfo(nCheDi).nDayItem) + 1)
            'ReDim Preserve ChediInfo(nCheDi).bIfEnterGZ(UBound(ChediInfo(nCheDi).bIfEnterGZ) + 1)
            sTime = AddTimeOver24(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
            sTime1 = AddTimeOver24(TrainInf(TempNtrain).Starting(TrainInf(TempNtrain).nPathID(1)))
            If sTime <= sTime1 Then
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
            sTime = AddTimeOver24(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
            sTime1 = AddTimeOver24(TrainInf(TempNtrain).Starting(TrainInf(TempNtrain).nPathID(1)))
            If sTime <= sTime1 Then
                ReDim Preserve ChediInfo(nCheDi).nLinkTrain(UBound(ChediInfo(nCheDi).nLinkTrain) + 1)
                For j = UBound(ChediInfo(nCheDi).nLinkTrain) To 2 Step -1
                    ChediInfo(nCheDi).nLinkTrain(j) = ChediInfo(nCheDi).nLinkTrain(j - 1)
                Next j
                ChediInfo(nCheDi).nLinkTrain(1) = nTrain

                TrainInf(nTrain).TrainReturn(1) = 0
                TrainInf(nTrain).TrainReturn(2) = TempNtrain
                TrainInf(TempNtrain).TrainReturn(1) = nTrain

                Exit Sub
            End If

            sTime = AddTimeOver24(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
            sTime1 = AddTimeOver24(TrainInf(TempNtrain1).Starting(TrainInf(TempNtrain1).nPathID(1)))
            If sTime >= sTime1 Then
                ReDim Preserve ChediInfo(nCheDi).nLinkTrain(UBound(ChediInfo(nCheDi).nLinkTrain) + 1)
                ChediInfo(nCheDi).nLinkTrain(UBound(ChediInfo(nCheDi).nLinkTrain)) = nTrain

                TrainInf(TempNtrain1).TrainReturn(2) = nTrain
                TrainInf(nTrain).TrainReturn(1) = TempNtrain1
                TrainInf(nTrain).TrainReturn(2) = 0

                Exit Sub
            End If


            For i = 1 To UBound(ChediInfo(nCheDi).nLinkTrain) - 1
                TempNtrain = ChediInfo(nCheDi).nLinkTrain(i)
                TempNtrain1 = ChediInfo(nCheDi).nLinkTrain(i + 1)
                sTime = AddTimeOver24(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                sTime1 = AddTimeOver24(TrainInf(TempNtrain).Starting(TrainInf(TempNtrain).nPathID(1)))
                sTime2 = AddTimeOver24(TrainInf(TempNtrain1).Starting(TrainInf(TempNtrain1).nPathID(1)))
                If sTime >= sTime1 And sTime <= sTime2 Then
                    ReDim Preserve ChediInfo(nCheDi).nLinkTrain(UBound(ChediInfo(nCheDi).nLinkTrain) + 1)
                    For j = UBound(ChediInfo(nCheDi).nLinkTrain) To i + 1 Step -1
                        ChediInfo(nCheDi).nLinkTrain(j) = ChediInfo(nCheDi).nLinkTrain(j - 1)
                    Next j
                    ChediInfo(nCheDi).nLinkTrain(i + 1) = nTrain

                    TrainInf(nTrain).TrainReturn(1) = TempNtrain
                    TrainInf(nTrain).TrainReturn(2) = TempNtrain1
                    TrainInf(TempNtrain).TrainReturn(2) = nTrain
                    TrainInf(TempNtrain1).TrainReturn(1) = nTrain

                    Exit For
                End If
            Next i
        End If
    End Sub

    Public Function AddTimeOver24(ByVal sTime As Long) As Single
        If sTime >= 0 And sTime < TimeTablePara.TimeTableDiagramPara.intCompareFirstTime Then
            AddTimeOver24 = 24 * 3600.0# + sTime
        Else
            AddTimeOver24 = sTime
        End If
    End Function

End Module
