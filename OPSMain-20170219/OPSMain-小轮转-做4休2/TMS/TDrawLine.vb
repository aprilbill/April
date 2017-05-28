Module TDrawLine
    Public nBstation As Integer
    Public nEstation As Integer
    Public nCanRightMove() As Integer, nCanLeftMove() As Integer
    Public m_StartorArrival As String, m_StartorArrivaltime As Long
    Public sChangeTrainReturn As String
    Public lPaoDian As Long                '铺画时抛点时分
    Public nPuHuaFangShi As Integer        '铺画方式

    Public nNowDataReadLineNum As Integer       '当前铺画线路(分局)总数
    'Public NumberStation As Integer        '车站数量 **
    Public KeYongGD() As Integer
    Public nTongGuDaotemp() As Integer
    Public nGuDaoTrain() As Integer
    Public nGuDaotemp() As Integer
    Public FenChaZhanGuDaoUse() As String '存入分岔站股道使用的信息

    Public nMoveStepTimeInTdrawline As Integer  '不够时移动多少时间
    Public nMoveStepTime As Integer   '不够时移动多少时间

    Structure typeTrainAdjustJianGeStyleAndTime '编图时的调整
        Dim nStyle As Integer '调整的类型
        Dim nMoveTime As Long '调整的时间
    End Structure
    Public trainAdjustStyle As typeTrainAdjustJianGeStyleAndTime

    '调图时的参数
    Structure typeTdrawlLinePara
        Dim sMoveStepTime As Integer '每一步移动的时间
        Dim sMaxMoveTime As Integer '最在可移动的时间
    End Structure
    Public TdrawLinePara As typeTdrawlLinePara

    Structure TrainErrorInformation
        Dim nTrain As String             '车次
        Dim nErrorSta As Integer
        Dim Scurtime As String
        Dim sErrorMessage As String
    End Structure
    Public TrainErrInf() As TrainErrorInformation

    Structure DataReadInformation
        Dim NumStation As Integer              '车站数量
        Dim nTotalLineNum As Integer           '线路总数量/
        Dim sTotalLineNum() As String          '线路总数量名称
        Dim sLuJuName As String                '当前铺画路局名称
        Dim sFenJuname As String               '当前铺画分局名称
        Dim sNowLineName As String             '当前铺画线路名称
        Dim nWholeLineNum As Integer           '所有线路(正线、岔线、联络线等)数量(不同于线路总数量)
        Dim PathName As String                 '当前数据存放路径
        Dim Filename As String                 '当前数据文件名
        Dim sSKBNameKe As String               '当前客车时刻表文件名
        Dim sSKBNameHuo As String              '当前货车时刻表文件名
        Dim NowStationBegin As Integer          '当前铺画线路起点站名称
        Dim NowStationEnd As Integer            '当前铺画线路终点站名称
        Dim NowSectionBegin As Integer          '当前铺画线路起点区段名称
        Dim NowSectionEnd As Integer            '当前铺画线路终点区段名称
        Dim NowTrainBegin As Integer            '当前铺画线路起始车次
        Dim NowTrainEnd As Integer              '当前铺画线路终止车次
        Dim NowFenChaStationBegin As Integer    '当前铺画线路起点站名称
        Dim NowFenChaStationEnd As Integer      '当前铺画线路终点站名称
        Dim NowShiKeBiaoBegin As Integer        '当前铺画线路时刻表起点站名称
        Dim NowShiKeBiaoEnd As Integer          '当前铺画线路时刻表终点站名称
        Dim NowIndexBegin As Integer            '当前铺画线路指标计算起点站名称
        Dim NowIndexEnd As Integer              '当前铺画线路指标计算终点站名称
        Dim SDataPlace As Integer
        Dim XDataPlace As Integer
        Dim SKeDataPlace As Integer
        Dim XKeDataPlace As Integer
        Dim KeCheNum As Integer
        Dim HuoCheNum As Integer
End Structure
    Public DataReadInf() As DataReadInformation

    Structure TianChuangInformation
        Dim sShiGongSectionName As String '施工天窗区间名称
        Dim lDownEmptyTime() As Long     '下行施工天窗范围
        Dim lUpEmptyTime() As Long       '上行施工天窗范围
        Dim sTianChuangKind As String     '施工天窗种类
    End Structure
    Public TianChuangInf() As TianChuangInformation


    Public Sub TDrawLineOld_Skip_stop(ByVal lArrivalTime As Long, ByVal lStartTime As Long, ByVal lSDtime As Long, ByVal nTrnNum As Integer, _
    ByVal nBeginSta As Integer, ByVal nEndSta As Integer, ByVal nTiaoorPu As Integer)
        'nTiaoorPu调整（1），铺画（0）
        ' If nTrnNum = 121 Then Stop
        Dim nUporDown As Integer, nTrnState As Integer
        Dim nStation As Integer, nQStation As Integer
        Dim nLeaveSection As Integer, nEnterSection As Integer
        Dim lStoporNot As Long, lMoveTimetemp As Long
        Dim nTempSt As Integer, nStopSt As Integer, nPhFx As Integer
        Dim nTrainReturn As Integer
        Dim nStartTimeChange As Integer, nStartStop As Integer
        Dim i As Integer
        Call setTempTraininfTime(nTrnNum)
        nPhFx = 1
        nStartTimeChange = 0
        nStartStop = 0
        ReDim nCanRightMove(UBound(StationInf))
        ReDim nCanLeftMove(UBound(StationInf))
        nPhFx = 1
        nBstation = nBeginSta
        nEstation = nEndSta
        '中间变量初试化
        nStation = nBstation
        nUporDown = nDirection(nTrnNum)
        ChangeStationNum(nTrnNum, nStation, nUporDown, 0)
        Initial(nTrnNum, nBstation, nEstation)
        nTrnState = nBeginDeal(lArrivalTime, lStartTime, nStation, nTrnNum)
        sChangeTrainReturn = "无"
        Dim stmptmpSTaName As String
        Dim curStartTime As String
        Dim nDoNums As Integer
        Dim nMoveTime As Integer '移动的秒数
        nMoveTime = 1
        nMoveStepTimeInTdrawline = nMoveTime
        nDoNums = 0
        Dim nFirTime As Long
        Dim nHaveMoveTime As Long
        nFirTime = lStartTime

        Do
            nMoveStepTime = 180
            If lStartTime > 86400 Then
                For i = 1 To UBound(StationInf)
                    TrainInf(nTrnNum).Arrival(i) = -1
                    TrainInf(nTrnNum).Starting(i) = -1
                Next i
                Exit Sub
            End If
            nQStation = nFindQstaNum(nTrnNum, nStation, nUporDown)
            If nStation <> nQStation Then
                nStation = nQStation - nSteplen(nTrnNum)
            End If
            stmptmpSTaName = StationInf(nStation).sStationName
            curStartTime = dTime(lStartTime, 0)
            If nStation = nBeginSta Then
                nHaveMoveTime = TimeMinus(lStartTime, nFirTime)
                If nHaveMoveTime > TdrawLinePara.sMaxMoveTime Then
                    If MsgBox("当前列车已经向右移动了一个小时，请确认是否让该列车继续移动", MsgBoxStyle.OkCancel, "确认操作") = MsgBoxResult.Cancel Then
                        '  将原始时间赋回来
                        Call CopyTempTraininfTime(nTrnNum)
                        Exit Do
                    Else
                        nFirTime = lStartTime
                    End If
                End If
            End If

            Select Case nTrnState
                Case 1
                    '出发处理（从nStation站发往nQStation站）

                    nEnterSection = nFindSecNum(nStation, nQStation, nUporDown)
                    nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                    nLeaveSection = nFindSecNum(nTempSt, nStation, nUporDown)
                    If nLeaveSection = 0 Then nLeaveSection = nEnterSection
                    Dim tmpStart As Integer
                    tmpStart = nStartstation(nTrnNum, nStation, nPhFx)
                    Select Case tmpStart
                        Case 1
                            '始发站处理
                            If sChangeTrainReturn = "无" Then
                                lArrivalTime = DealTrainStart(nTrnNum, nStation, lStartTime)
                            End If
                        Case 2
                            '接入站处理
                        Case 3
                            '其他站处理
                    End Select
                    '股道和出发判别

                    Dim tmpGuDaoStart As Integer
                    tmpGuDaoStart = nGuDaoStart(lArrivalTime, lStartTime, nTrnNum, nLeaveSection, nStation, nEnterSection, nPhFx)
                    Select Case tmpGuDaoStart
                        Case 0
                            Record(nTrnNum, nStation, lStartTime, lArrivalTime)

                            If nEndStation(nTrnNum, nStation, nPhFx) <> 0 Then
                                '本条运行线的终点（终到站、交出站、平移站）
                                Exit Do
                            End If
                            Dim tmpStartStation As Integer
                            tmpStartStation = nStartstation(nTrnNum, nStation, nPhFx)
                            Select Case tmpStartStation
                                Case 0
                                    lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                Case 1
                                    DealIfStartStation(nTrnNum, nStation, lStartTime, lArrivalTime, 1)
                                    lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                Case 2
                                    If lArrivalTime <> lStartTime Then
                                        lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                    End If
                                    nTrainReturn = nFindLinkTrain(nTrnNum)
                                    If nTrainReturn <> 0 Then
                                        If StationInf(nStation).sStationName = TrainInf(nTrainReturn).NextStation Then
                                            TrainInf(nTrainReturn).StopLine(nStation) = TrainInf(nTrnNum).StopLine(nStation)
                                            Record(nTrainReturn, nStation, TrainInf(nTrnNum).Starting(nStation), TrainInf(nTrainReturn).Arrival(nStation))
                                        End If
                                    End If
                                Case 3
                                    If lStartTime <> lArrivalTime Then
                                        lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                    End If
                            End Select
                            nTrnState = 2
                            nLeftRightMove("不移", nStation, "出发")
                        Case 1
                            '与前后行均不够，移动发车时间至后行车后
                            lMoveTimetemp = FindStartTime(1, nTrnNum, lStartTime, nUporDown, _
                                nLeaveSection, nEnterSection, nPhFx)
                            nTrnState = 1
                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), lMoveTimetemp)
                            Initial(nTrnNum, nBstation, nEstation)
                            nStation = TrainInf(nTrnNum).nPathID(1)
                        Case 2
                            '与前行不够，比较与前行车间的富余时间决定移动的时间
                            lMoveTimetemp = FindStartTime(2, nTrnNum, lStartTime, nUporDown, _
                                nLeaveSection, nEnterSection, nPhFx)
                            nTrnState = 1
                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), lMoveTimetemp)
                            Initial(nTrnNum, nBstation, nEstation)
                            nStation = TrainInf(nTrnNum).nPathID(1)
                        Case 3
                            '与后行不够，移动发车时间至前行车前或后行车后
                            lMoveTimetemp = FindStartTime(3, nTrnNum, lStartTime, nUporDown, _
                                nLeaveSection, nEnterSection, nPhFx)
                            If lMoveTimetemp < 0 Then
                                '与后行不够，移动发车时间至前行车前
                                nTrnState = 1
                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), nMoveTime)
                                Initial(nTrnNum, nBstation, nEstation)
                                nStation = TrainInf(nTrnNum).nPathID(1)
                            Else
                                '与后行不够，移动发车时间至后行车后
                                nTrnState = 1
                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), lMoveTimetemp)
                                Initial(nTrnNum, nBstation, nEstation)
                                nStation = TrainInf(nTrnNum).nPathID(1)
                            End If
                        Case 4
                            '本站股道数量不够，移至后一站
                            lMoveTimetemp = FindStartTime(4, nTrnNum, lArrivalTime, nUporDown, _
                                nLeaveSection, nEnterSection, nPhFx)
                            nTrnState = 1
                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), lMoveTimetemp)
                            Initial(nTrnNum, nBstation, nEstation)
                            nStation = TrainInf(nTrnNum).nPathID(1)

                        Case 5
                            '由于发车检查发现为反向到达
                            MsgBox("有反向到达车！")
                        Case 6
                            If TrainInf(nTrnNum).TrainClassCal = 12 Then
                                MsgBox(TrainInf(nTrnNum).Train & "在车底停留站" & StationInf(nStation).sStationName & _
                                    "股道数量不足，需修改车底折返方式", 0 + 16)
                                Exit Sub
                            ElseIf TrainInf(nTrnNum).TrainClassCal <> 12 Then
                                MsgBox(TrainInf(nTrnNum).Train & "在车底停留站" & StationInf(nStation).sStationName & _
                                    "股道数量不足，需修改车底折返方式", 0 + 16)
                                sChangeTrainReturn = "修改车底折返方式"
                                lArrivalTime = TimeMinus(lStartTime, nStartTchTime(nTrnNum))
                                'Exit Sub
                            End If
                    End Select
                Case 2
                    '通过处理（从nStation站通过nQStation站）
                    nLeaveSection = nFindSecNum(nStation, nQStation, nUporDown)
                    lArrivalTime = TimeAdd(lStartTime, TimeRun(nTrnNum, nStation, nQStation))
                    lStartTime = lArrivalTime
                    lStoporNot = StoporPass(nTrnNum, nQStation, lSDtime, lArrivalTime, nUporDown, 1)
                    ChangeStationNum(nTrnNum, nQStation, nUporDown, 0)
                    nTempSt = nFindQstaNum(nTrnNum, nQStation, nUporDown)
                    nEnterSection = nFindSecNum(nQStation, nTempSt, nUporDown)

                    '江编2003.07.20
                    If nLeaveSection = nEnterSection Then
                        nEnterSection = EnterSecCheck(nLeaveSection, nEnterSection, nUporDown)
                    End If

                    If lStoporNot = 0 Then
                        '不需停车
                        Dim tmpnGuDaoPass As Integer
                        tmpnGuDaoPass = nGuDaoPass(lArrivalTime, nTrnNum, nLeaveSection, nQStation, nEnterSection)
                        Select Case tmpnGuDaoPass
                            Case 0
                                '能够通过，记录到发时刻
                                Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                nStation = nQStation
                                nTrnState = 2
                            Case 1
                                '与前后行均不够，不能通过，检查能否停车
                                nTrnState = 1
                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), nMoveTime)
                                Initial(nTrnNum, nBstation, nEstation)
                                nStation = TrainInf(nTrnNum).nPathID(1)
                            Case 2
                                '与前行车通过间隔不够时，决定能否右移运行线

                                lMoveTimetemp = FindPassTime(2, nTrnNum, lArrivalTime, nBstation, _
                                                nUporDown, nLeaveSection, nEnterSection, nPhFx)
                                If lMoveTimetemp > 0 Then
                                    nTrnState = 1
                                    lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), lMoveTimetemp)
                                    Initial(nTrnNum, nBstation, nEstation)
                                    nStation = TrainInf(nTrnNum).nPathID(1)

                                ElseIf lMoveTimetemp = 0 Then
                                    '不能右移
                                    nTrnState = 1
                                    lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), nMoveTime)
                                    Initial(nTrnNum, nBstation, nEstation)
                                    nStation = TrainInf(nTrnNum).nPathID(1)

                                ElseIf lMoveTimetemp < 0 Then
                                    '能够左移
                                    nTrnState = 1
                                    lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), nMoveTime)
                                    Initial(nTrnNum, nBstation, nEstation)
                                    nStation = TrainInf(nTrnNum).nPathID(1)
                                End If
                            Case 3
                                '与后行车通过间隔不够时，决定能否左移运行线
                                nTrnState = 1
                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), nMoveTime)
                                Initial(nTrnNum, nBstation, nEstation)
                                nStation = TrainInf(nTrnNum).nPathID(1)
                            Case 4
                                '正线被占用，检查该站能否停车
                                nTrnState = 1
                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), nMoveTime)
                                Initial(nTrnNum, nBstation, nEstation)
                                nStation = TrainInf(nTrnNum).nPathID(1)
                        End Select
                        Select Case nEndStation(nTrnNum, nStation, nPhFx)
                            Case 1, 2
                                lArrivalTime = TimeAdd(lArrivalTime, TimeT(nTrnNum, nStation, nQStation))
                                lStartTime = lArrivalTime
                                DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 1)
                                Exit Do
                            Case 0, 3
                        End Select
                    Else
                        '需要停车
                        ChangeStationNum(nTrnNum, nQStation, nUporDown, 1)
                        nTempSt = nFindHstaNum(nTrnNum, nQStation, nUporDown)
                        nTempSt = nBeginStation(nTrnNum, nTempSt)
                        lArrivalTime = TimeAdd(lArrivalTime, TimeT(nTrnNum, nTempSt, nQStation))
                        lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                        nStation = nTempSt
                        nTrnState = 3
                    End If
                Case 3
                    '到达处理（从nStation站到达nQStation站）
                    If sChangeTrainReturn = "无" Then
                        nLeaveSection = nFindSecNum(nStation, nQStation, nUporDown)
                        nTempSt = nQStation
                        nStopSt = nTempSt
                        nTempSt = nFindQstaNum(nTrnNum, nTempSt, nUporDown)
                        nEnterSection = nFindSecNum(nQStation, nTempSt, nUporDown)
                        If nEnterSection = 0 Then nEnterSection = nLeaveSection
                        lArrivalTime = lArrivalTimeReSet(nTrnNum, lArrivalTime, nQStation)
                        lStartTime = lStartTimeReSet(nTrnNum, lStartTime, nQStation)
                    End If
                    Select Case nEndStation(nTrnNum, nQStation, nPhFx)
                        Case 1
                            '终到站
                            If sChangeTrainReturn = "无" Then
                                lStartTime = DealKeCheArrival(nTrnNum, nQStation, lArrivalTime)
                            End If
                        Case Else
                    End Select

                    '江编2003.07.20
                    If nLeaveSection = nEnterSection Then
                        nEnterSection = EnterSecCheck(nLeaveSection, nEnterSection, nUporDown)
                    End If

                    Dim tmpnGuDaoStop As Integer
                    trainAdjustStyle.nStyle = 0
                    tmpnGuDaoStop = nGuDaoStop(lArrivalTime, lStartTime, nTrnNum, nLeaveSection, nQStation, nEnterSection, nPhFx)
                    Dim nArriMoveState As String
                    nArriMoveState = 0
                    Select Case tmpnGuDaoStop
                        Case 0
                            '下站能够到达，记录到发时刻，形成发车点
                            ' nStation = nQStation
                            nTrnState = 1
                            Select Case nEndStation(nTrnNum, nQStation, nPhFx)
                                Case 1  '终到站
                                    lArrivalTime = TimeAdd(lArrivalTime, TimeT(nTrnNum, nStation, nQStation))
                                    lStartTime = lArrivalTime
                                    DealIfEndStation(nTrnNum, nQStation, lArrivalTime, lStartTime, 1)
                                    Exit Do
                                Case 2
                                    DealIfEndStation(nTrnNum, nQStation, lArrivalTime, lStartTime, 2)
                                    Exit Do
                                Case 3
                                    DealIfEndStation(nTrnNum, nQStation, lArrivalTime, lStartTime, 3)
                                    Exit Do
                                Case Else
                                    If sChangeTrainReturn = "修改车底折返方式" Then
                                        sChangeTrainReturn = "无"
                                    End If
                                    Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                            End Select
                            nStation = nQStation
                        Case 1
                            '前方站前后行均不够，不能停车,移到后一站
                            nTrnState = 1
                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), nMoveTime)
                            Initial(nTrnNum, nBstation, nEstation)
                            nStation = TrainInf(nTrnNum).nPathID(1)

                        Case 2

                            '与前行车到达间隔不够，决定能否右移运行线

                            lMoveTimetemp = FindStopTime(2, nTrnNum, lArrivalTime, nBstation, _
                                            nUporDown, nLeaveSection, nEnterSection, nPhFx)

                            If lMoveTimetemp < 86400 And lMoveTimetemp > 0 Then
                                '能够右移

                                nTrnState = 1
                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), lMoveTimetemp)
                                Initial(nTrnNum, nBstation, nEstation)
                                nStation = TrainInf(nTrnNum).nPathID(1)

                            ElseIf lMoveTimetemp >= 86400 Then
                                nTrnState = 1
                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), lMoveTimetemp)
                                Initial(nTrnNum, nBstation, nEstation)
                                nStation = TrainInf(nTrnNum).nPathID(1)
                            ElseIf lMoveTimetemp < 0 Then
                                nTrnState = 1
                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), nMoveTime)
                                Initial(nTrnNum, nBstation, nEstation)
                                nStation = TrainInf(nTrnNum).nPathID(1)
                            End If
                        Case 3
                            '与后行车到达间隔不够，决定能否左移运行线
                            nTrnState = 1
                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), nMoveTime)
                            Initial(nTrnNum, nBstation, nEstation)
                            nStation = TrainInf(nTrnNum).nPathID(1)
                        Case 4
                            '前方站股道数量不够，移至后一站
                            lMoveTimetemp = FindStopTime(4, nTrnNum, lArrivalTime, nBstation, _
                                            nUporDown, nLeaveSection, nEnterSection, nPhFx)
                            nTrnState = 1
                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), lMoveTimetemp)
                            Initial(nTrnNum, nBstation, nEstation)
                            nStation = TrainInf(nTrnNum).nPathID(1)

                        Case 5
                            If TrainInf(nTrnNum).TrainClassCal = 12 Then
                                MsgBox(TrainInf(nTrnNum).Train & "在车底停留站" & StationInf(nQStation).sStationName & _
                                    "股道数量不足，需修改车底折返方式", 0 + 16)
                                Exit Sub
                            ElseIf TrainInf(nTrnNum).TrainClassCal <> 12 Then
                                MsgBox(TrainInf(nTrnNum).Train & "在车底停留站" & StationInf(nQStation).sStationName & _
                                    "股道数量不足，需修改车底折返方式", 0 + 16)
                                sChangeTrainReturn = "修改车底折返方式"
                                lStartTime = TimeAdd(lArrivalTime, nStartTchTime(nTrnNum))
                                'Exit Sub
                            End If
                    End Select
            End Select
        Loop Until nStation = nEstation And TrainInf(nTrnNum).Starting(nStation) <> -1
        DealTrainStart(nTrnNum, TrainInf(nTrnNum).nPathID(1), TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)))
        'DealTrainStart(nTrnNum, TrainInf(nTrnNum).nPathID(UBound(TrainInf(nTrnNum).nPathID)), TrainInf(nTrnNum).Arrival(TrainInf(nTrnNum).nPathID(UBound(TrainInf(nTrnNum).nPathID))))
    End Sub

    Public Sub TDrawLineOld(ByVal lArrivalTime As Long, ByVal lStartTime As Long, ByVal lSDtime As Long, ByVal nTrnNum As Integer, _
        ByVal nBeginSta As Integer, ByVal nEndSta As Integer, ByVal nTiaoorPu As Integer)
        Dim nTiaoState As Integer

        If TimeTablePara.sTiaoLineState = "不提示" Then
            nTiaoState = 0
        Else
            nTiaoState = 1
        End If

        If TimeTablePara.sDrawLineStyle = "不能越行" Then
            TDrawLineOld_Metro(lArrivalTime, lStartTime, lSDtime, nTrnNum, nBeginSta, nEndSta, nTiaoorPu, nTiaoState)
            'TDrawLineNoStrainInMetro(lArrivalTime, lStartTime, lSDtime, nTrnNum, nBeginSta, nEndSta, nTiaoorPu)

        Else
            TDrawLineOld_GaungShenLine(lArrivalTime, lStartTime, lSDtime, nTrnNum, nBeginSta, nEndSta, nTiaoorPu, nTiaoState)
        End If
    End Sub

    Public Sub TDrawLineOld_Metro(ByVal lArrivalTime As Long, ByVal lStartTime As Long, ByVal lSDtime As Long, ByVal nTrnNum As Integer, _
        ByVal nBeginSta As Integer, ByVal nEndSta As Integer, ByVal nTiaoorPu As Integer, ByVal nTiaoState As Integer)
        Dim nUporDown As Integer, nTrnState As Integer
        Dim nStation As Integer, nQStation As Integer
        Dim nLeaveSection As Integer, nEnterSection As Integer
        Dim lStoporNot As Long
        Dim nTempSt As Integer, nStopSt As Integer, nPhFx As Integer
        Dim nTrainReturn As Integer
        Dim nStartTimeChange As Integer, nStartStop As Integer
        Dim i As Integer
        Call setTempTraininfTime(nTrnNum)
        nPhFx = 1
        nStartTimeChange = 0
        nStartStop = 0
        ReDim nCanRightMove(UBound(StationInf))
        ReDim nCanLeftMove(UBound(StationInf))
        nPhFx = 1
        nBstation = nBeginSta
        nEstation = nEndSta
        '中间变量初试化
        nStation = nBstation
        nUporDown = nDirection(nTrnNum)
        ChangeStationNum(nTrnNum, nStation, nUporDown, 0)
        Initial(nTrnNum, nBstation, nEstation)
        nTrnState = nBeginDeal(lArrivalTime, lStartTime, nStation, nTrnNum)
        sChangeTrainReturn = "无"
        Dim stmptmpSTaName As String
        Dim curStartTime As String
        Dim nDoNums As Integer
        Dim nMoveTime As Integer '移动的秒数
        nMoveTime = 1
        nDoNums = 0
        nMoveStepTimeInTdrawline = 1
        nMoveStepTime = 1
        Dim nFirTime As Long
        Dim nHaveMoveTime As Long
        nFirTime = lStartTime

        Do
            If lStartTime > 86400 Then '- 2 * 3600 Then
                For i = 1 To UBound(StationInf)
                    TrainInf(nTrnNum).Arrival(i) = -1
                    TrainInf(nTrnNum).Starting(i) = -1
                Next i
                Exit Sub
            End If
            If sChangeTrainReturn = "无" Then
                nQStation = nFindQstaNum(nTrnNum, nStation, nUporDown)
                If nStation <> nQStation Then
                    nStation = nQStation - nSteplen(nTrnNum)
                End If
            End If
            stmptmpSTaName = StationInf(nStation).sStationName
            curStartTime = dTime(lStartTime, 0)
            If nTiaoState = 1 Then '需要询问操作
                If nStation = nBeginSta Then
                    nHaveMoveTime = TimeMinus(lStartTime, nFirTime)
                    If nHaveMoveTime > TdrawLinePara.sMaxMoveTime Then
                        If MsgBox("当前列车已经向右移动了一个小时，请确认是否让该列车继续移动", MsgBoxStyle.OkCancel, "确认操作") = MsgBoxResult.Cancel Then
                            '  将原始时间赋回来
                            'Call CopyTempTraininfTime(nTrnNum)
                            Call TDrawLine.TDrawLineNoStrainInMetro(nFirTime, nFirTime, lSDtime, nTrnNum, nBeginSta, nEndSta, nTiaoorPu)
                            Exit Do
                        Else
                            nFirTime = lStartTime
                        End If
                    End If
                End If
            End If

            Select Case nTrnState
                Case 1
                    '出发处理（从nStation站发往nQStation站）
                    If sChangeTrainReturn = "无" Then
                        nEnterSection = nFindSecNum(nStation, nQStation, nUporDown)
                        ChangeStationNum(nTrnNum, nStation, nUporDown, 1)
                        nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                        nLeaveSection = nFindSecNum(nTempSt, nStation, nUporDown)
                        If nLeaveSection = 0 Then nLeaveSection = nEnterSection
                        ChangeStationNum(nTrnNum, nStation, nUporDown, 0)
                    End If
                    Dim tmpStart As Integer
                    tmpStart = nStartstation(nTrnNum, nStation, nPhFx)
                    Select Case tmpStart
                        Case 1
                            '始发站处理
                            If sChangeTrainReturn = "无" Then
                                lArrivalTime = DealTrainStart(nTrnNum, nStation, lStartTime)
                            End If
                        Case 2
                            '接入站处理
                        Case 3
                            '其他站处理
                    End Select
                    '股道和出发判别

                    Dim tmpGuDaoStart As Integer
                    tmpGuDaoStart = nGuDaoStart(lArrivalTime, lStartTime, nTrnNum, nLeaveSection, nStation, nEnterSection, nPhFx)
                    Select Case tmpGuDaoStart
                        Case 0
                            Record(nTrnNum, nStation, lStartTime, lArrivalTime)
                            If nEndStation(nTrnNum, nStation, nPhFx) <> 0 Then
                                '本条运行线的终点（终到站、交出站、平移站）
                                Exit Do
                            End If
                            Dim tmpStartStation As Integer
                            tmpStartStation = nStartstation(nTrnNum, nStation, nPhFx)
                            Select Case tmpStartStation
                                Case 0
                                    lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                    If sChangeTrainReturn = "修改车底折返方式" Then
                                        sChangeTrainReturn = "无"
                                    End If
                                Case 1
                                    DealIfStartStation(nTrnNum, nStation, lStartTime, lArrivalTime, 1)
                                    lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                Case 2
                                    If lArrivalTime <> lStartTime Then
                                        lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                    End If
                                    nTrainReturn = nFindLinkTrain(nTrnNum)
                                    If nTrainReturn <> 0 Then
                                        If StationInf(nStation).sStationName = TrainInf(nTrainReturn).NextStation Then
                                            TrainInf(nTrainReturn).StopLine(nStation) = TrainInf(nTrnNum).StopLine(nStation)
                                            Record(nTrainReturn, nStation, TrainInf(nTrnNum).Starting(nStation), TrainInf(nTrainReturn).Arrival(nStation))
                                        End If
                                    End If
                                Case 3
                                    If lStartTime <> lArrivalTime Then
                                        lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                    End If
                            End Select
                            nTrnState = 2
                            nLeftRightMove("不移", nStation, "出发")
                        Case Else
                            nTrnState = 1
                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), nMoveTime)
                            Initial(nTrnNum, nBstation, nEstation)
                            nStation = TrainInf(nTrnNum).nPathID(1)
                    End Select
                Case 2
                    '通过处理（从nStation站通过nQStation站）
                    nLeaveSection = nFindSecNum(nStation, nQStation, nUporDown)
                    lArrivalTime = TimeAdd(lStartTime, TimeRun(nTrnNum, nStation, nQStation))
                    lStartTime = lArrivalTime
                    lStoporNot = StoporPass(nTrnNum, nQStation, lSDtime, lArrivalTime, nUporDown, 1)
                    ChangeStationNum(nTrnNum, nQStation, nUporDown, 0)
                    nTempSt = nFindQstaNum(nTrnNum, nQStation, nUporDown)
                    nEnterSection = nFindSecNum(nQStation, nTempSt, nUporDown)

                    '江编2003.07.20
                    If nLeaveSection = nEnterSection Then
                        nEnterSection = EnterSecCheck(nLeaveSection, nEnterSection, nUporDown)
                    End If

                    If lStoporNot = 0 Then
                        '不需停车
                        Dim tmpnGuDaoPass As Integer
                        tmpnGuDaoPass = nGuDaoPass(lArrivalTime, nTrnNum, nLeaveSection, nQStation, nEnterSection)
                        Select Case tmpnGuDaoPass
                            Case 0
                                '能够通过，记录到发时刻
                                Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                nStation = nQStation
                                nTrnState = 2
                            Case Else
                                nTrnState = 1
                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), nMoveTime)
                                Initial(nTrnNum, nBstation, nEstation)
                                nStation = TrainInf(nTrnNum).nPathID(1)
                        End Select
                        Select Case nEndStation(nTrnNum, nStation, nPhFx)
                            Case 1, 2
                                Exit Do
                            Case 0, 3
                        End Select
                    Else
                        '需要停车
                        ChangeStationNum(nTrnNum, nQStation, nUporDown, 1)
                        nTempSt = nFindHstaNum(nTrnNum, nQStation, nUporDown)
                        nTempSt = nBeginStation(nTrnNum, nTempSt)
                        lArrivalTime = TimeAdd(lArrivalTime, TimeT(nTrnNum, nTempSt, nQStation))
                        lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                        nStation = nTempSt
                        nTrnState = 3
                    End If
                Case 3
                    '到达处理（从nStation站到达nQStation站）
                    If sChangeTrainReturn = "无" Then
                        nLeaveSection = nFindSecNum(nStation, nQStation, nUporDown)
                        nTempSt = nQStation
                        ChangeStationNum(nTrnNum, nTempSt, nUporDown, 0)
                        nStopSt = nTempSt
                        nTempSt = nFindQstaNum(nTrnNum, nTempSt, nUporDown)
                        nEnterSection = nFindSecNum(nQStation, nTempSt, nUporDown)
                        If nEnterSection = 0 Then nEnterSection = nLeaveSection
                        ChangeStationNum(nTrnNum, nStopSt, nUporDown, 1)

                        lArrivalTime = lArrivalTimeReSet(nTrnNum, lArrivalTime, nQStation)
                        lStartTime = lStartTimeReSet(nTrnNum, lStartTime, nQStation)
                    End If
                    Select Case nEndStation(nTrnNum, nQStation, nPhFx)
                        Case 1
                            '终到站
                            If sChangeTrainReturn = "无" Then
                                lStartTime = DealKeCheArrival(nTrnNum, nQStation, lArrivalTime)
                            End If
                        Case Else
                    End Select

                    '江编2003.07.20
                    If nLeaveSection = nEnterSection Then
                        nEnterSection = EnterSecCheck(nLeaveSection, nEnterSection, nUporDown)
                    End If

                    Dim tmpnGuDaoStop As Integer
                    trainAdjustStyle.nStyle = 0
                    tmpnGuDaoStop = nGuDaoStop(lArrivalTime, lStartTime, nTrnNum, nLeaveSection, nQStation, nEnterSection, nPhFx)
                    Dim nArriMoveState As String
                    nArriMoveState = 0
                    Select Case tmpnGuDaoStop
                        Case 0
                            '下站能够到达，记录到发时刻，形成发车点
                            ChangeStationNum(nTrnNum, nQStation, nUporDown, 0)
                            nStation = nQStation
                            nTrnState = 1
                            Select Case nEndStation(nTrnNum, nStation, nPhFx)
                                Case 1  '终到站
                                    DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 1)
                                    Exit Do
                                Case 2
                                    DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 2)
                                    Exit Do
                                Case 3
                                    DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 3)
                                    Exit Do
                                Case Else
                                    If sChangeTrainReturn = "修改车底折返方式" Then
                                        sChangeTrainReturn = "无"
                                    End If
                                    Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                            End Select
                        Case Else
                            nTrnState = 1
                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)), nMoveTime)
                            Initial(nTrnNum, nBstation, nEstation)
                            nStation = TrainInf(nTrnNum).nPathID(1)
                    End Select
            End Select
        Loop Until nStation = nEstation And TrainInf(nTrnNum).Starting(nStation) <> -1
        DealTrainStart(nTrnNum, TrainInf(nTrnNum).nPathID(1), TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)))
        'DealTrainStart(nTrnNum, TrainInf(nTrnNum).nPathID(UBound(TrainInf(nTrnNum).nPathID)), TrainInf(nTrnNum).Arrival(TrainInf(nTrnNum).nPathID(UBound(TrainInf(nTrnNum).nPathID))))
    End Sub

    Public Sub TDrawLineOld_GaungShenLine(ByVal lArrivalTime As Long, ByVal lStartTime As Long, ByVal lSDtime As Long, ByVal nTrnNum As Integer, _
    ByVal nBeginSta As Integer, ByVal nEndSta As Integer, ByVal nTiaoorPu As Integer, ByVal nTiaoState As Integer)
        'nTiaoorPu调整（1），铺画（0）
        ' If nTrnNum = 121 Then Stop
        Dim nUporDown As Integer, nTrnState As Integer
        Dim nStation As Integer, nQStation As Integer
        Dim nLeaveSection As Integer, nEnterSection As Integer
        Dim lStoporNot As Long, lMoveTimetemp As Long
        Dim nTempSt As Integer, nStopSt As Integer, nPhFx As Integer
        Dim nTrainReturn As Integer
        Dim nStartTimeChange As Integer, nStartStop As Integer
        Dim nTmpTime As Integer
        Dim i As Integer
        Call setTempTraininfTime(nTrnNum)
        nPhFx = 1
        nStartTimeChange = 0
        nStartStop = 0
        ReDim nCanRightMove(UBound(StationInf))
        ReDim nCanLeftMove(UBound(StationInf))
        nPhFx = 1
        nBstation = nBeginSta
        nEstation = nEndSta
        '中间变量初试化
        nStation = nBstation
        nUporDown = nDirection(nTrnNum)
        ChangeStationNum(nTrnNum, nStation, nUporDown, 0)
        Initial(nTrnNum, nBstation, nEstation)
        nTrnState = nBeginDeal(lArrivalTime, lStartTime, nStation, nTrnNum)
        sChangeTrainReturn = "无"
        Dim stmptmpSTaName As String
        Dim curStartTime As String
        Dim nDoNums As Integer
        nDoNums = 0
        nMoveStepTimeInTdrawline = 15
        'If nTrnNum = 27 Then Stop
        nMoveStepTime = 15

        Dim nFirTime As Long
        Dim nHaveMoveTime As Long
        nFirTime = lStartTime
        Do
            'nDoNums = nDoNums + 1
            'If nDoNums > 10000 Then Exit Do
            If lStartTime > 86400 Then '- 2 * 3600 Then
                For i = 1 To UBound(StationInf)
                    TrainInf(nTrnNum).Arrival(i) = -1
                    TrainInf(nTrnNum).Starting(i) = -1
                Next i
                Exit Sub
            End If
            If sChangeTrainReturn = "无" Then
                nQStation = nFindQstaNum(nTrnNum, nStation, nUporDown)
                If nStation <> nQStation Then
                    nStation = nQStation - nSteplen(nTrnNum)
                End If
            End If
            stmptmpSTaName = StationInf(nStation).sStationName
            curStartTime = dTime(lStartTime, 0)
            If nTiaoState = 1 Then '需要询问操作
                If nStation = nBeginSta Then
                    nHaveMoveTime = TimeMinus(lStartTime, nFirTime)
                    If nHaveMoveTime > TdrawLinePara.sMaxMoveTime Then
                        If nHaveMoveTime >= 3600 Then
                            If MsgBox("当前列车已经向右移动了一个小时，请确认是否让该列车继续移动", MsgBoxStyle.OkCancel, "确认操作") = MsgBoxResult.Cancel Then
                                '  将原始时间赋回来
                                Call CopyTempTraininfTime(nTrnNum)
                                Exit Do
                            Else
                                nFirTime = lStartTime
                            End If
                        End If
                    End If
                End If
            End If
            Select Case nTrnState
                Case 1
                    '出发处理（从nStation站发往nQStation站）
                    If sChangeTrainReturn = "无" Then
                        nEnterSection = nFindSecNum(nStation, nQStation, nUporDown)
                        ChangeStationNum(nTrnNum, nStation, nUporDown, 1)
                        nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                        nLeaveSection = nFindSecNum(nTempSt, nStation, nUporDown)
                        If nLeaveSection = 0 Then nLeaveSection = nEnterSection
                        ChangeStationNum(nTrnNum, nStation, nUporDown, 0)
                    End If
                    Dim tmpStart As Integer
                    tmpStart = nStartstation(nTrnNum, nStation, nPhFx)
                    Select Case tmpStart
                        Case 1
                            '始发站处理
                            If sChangeTrainReturn = "无" Then
                                lArrivalTime = DealTrainStart(nTrnNum, nStation, lStartTime)
                            End If
                        Case 2
                            '接入站处理
                        Case 3
                            '其他站处理
                    End Select
                    '股道和出发判别

                    Dim tmpGuDaoStart As Integer
                    tmpGuDaoStart = nGuDaoStart(lArrivalTime, lStartTime, nTrnNum, nLeaveSection, nStation, nEnterSection, nPhFx)
                    ' If tmpGuDaoStart > 0 Then Stop
                    Select Case tmpGuDaoStart
                        Case 0
                            Record(nTrnNum, nStation, lStartTime, lArrivalTime)

                            If nEndStation(nTrnNum, nStation, nPhFx) <> 0 Then
                                '本条运行线的终点（终到站、交出站、平移站）
                                Exit Do
                            End If
                            Dim tmpStartStation As Integer
                            tmpStartStation = nStartstation(nTrnNum, nStation, nPhFx)
                            Select Case tmpStartStation
                                Case 0
                                    lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                    If sChangeTrainReturn = "修改车底折返方式" Then
                                        sChangeTrainReturn = "无"
                                    End If
                                Case 1
                                    DealIfStartStation(nTrnNum, nStation, lStartTime, lArrivalTime, 1)
                                    lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                Case 2
                                    If lArrivalTime <> lStartTime Then
                                        lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                    End If
                                    nTrainReturn = nFindLinkTrain(nTrnNum)
                                    If nTrainReturn <> 0 Then
                                        If StationInf(nStation).sStationName = TrainInf(nTrainReturn).NextStation Then
                                            TrainInf(nTrainReturn).StopLine(nStation) = TrainInf(nTrnNum).StopLine(nStation)
                                            Record(nTrainReturn, nStation, TrainInf(nTrnNum).Starting(nStation), TrainInf(nTrainReturn).Arrival(nStation))
                                        End If
                                    End If
                                Case 3
                                    If lStartTime <> lArrivalTime Then
                                        lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                    End If
                            End Select
                            nTrnState = 2
                            nLeftRightMove("不移", nStation, "出发")
                        Case 1
                            '与前后行均不够，移动发车时间至后行车后
                            lMoveTimetemp = FindStartTime(1, nTrnNum, lStartTime, nUporDown, _
                                nLeaveSection, nEnterSection, nPhFx)
                            lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                            nTrnState = 1
                            nLeftRightMove("右移", nStation, "出发")
                        Case 2
                            '与前行不够，比较与前行车间的富余时间决定移动的时间
                            lMoveTimetemp = FindStartTime(2, nTrnNum, lStartTime, nUporDown, _
                                nLeaveSection, nEnterSection, nPhFx)
                            lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                            nTrnState = 1
                            nLeftRightMove("右移", nStation, "出发")
                        Case 3
                            '与后行不够，移动发车时间至前行车前或后行车后
                            lMoveTimetemp = FindStartTime(3, nTrnNum, lStartTime, nUporDown, _
                                nLeaveSection, nEnterSection, nPhFx)
                            If lMoveTimetemp < 0 Then
                                '与后行不够，移动发车时间至前行车前
                                lStartTime = TimeMinus(lStartTime, -lMoveTimetemp)
                                If StationInf(nStation).sStationName = TrainInf(nTrnNum).ComeStation Then
                                    lArrivalTime = TimeMinus(lArrivalTime, -lMoveTimetemp)
                                End If
                                nLeftRightMove("左移", nStation, "出发")
                            Else
                                '与后行不够，移动发车时间至后行车后
                                lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                                nLeftRightMove("右移", nStation, "出发")
                            End If
                            nTrnState = 1
                        Case 4
                            '本站股道数量不够，移至后一站
                            lMoveTimetemp = FindStartTime(4, nTrnNum, lArrivalTime, nUporDown, _
                                nLeaveSection, nEnterSection, nPhFx)
                            ChangeStationNum(nTrnNum, nStation, nUporDown, 1)
                            nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                            nTempSt = nBeginStation(nTrnNum, nTempSt)
                            Do While Right(StationInf(nTempSt).sStationProp, 3) = "XLS"
                                ChangeStationNum(nTrnNum, nTempSt, nUporDown, 1)
                                nTempSt = nFindHstaNum(nTrnNum, nTempSt, nUporDown)
                                nTempSt = nBeginStation(nTrnNum, nTempSt)
                            Loop
                            StationLineNoOccupy(nTrnNum, nStation)
                            Dim ntmpnStartStation As Integer
                            ntmpnStartStation = nStartstation(nTrnNum, nStation, nPhFx)
                            Select Case ntmpnStartStation
                                Case 1
                                    '始发站

                                    If nStartTimeChange = 0 Then
                                        'MsgBox TrainInf(nTrnNum).Train + "在始发站" + StationInf(nStation).sStationName + _
                                        '    "股道数量不够，是否改变始发时刻？", 0 + 16, "铺画运行线"
                                        nStartTimeChange = 1
                                    End If
                                    lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                                    lArrivalTime = TimeAdd(lArrivalTime, lMoveTimetemp)
                                    nTrnState = 1
                                    nStation = nBstation

                                    nLeftRightMove("右移", nStation, "出发")

                                Case 2

                                    '接入站(要)
                                    'MsgBox TrainInf(nTrnNum).Train + "在接入站" + StationInf(nStation).sStationName + _
                                    '  "股道数量不够，是否改变到达时刻？", 0 + 16, "铺画运行线"
                                    lArrivalTime = TimeAdd(lArrivalTime, lMoveTimetemp)
                                    lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                                    nTrnState = 1
                                    nStation = nBstation

                                    nLeftRightMove("右移", nStation, "出发")

                                    '加对话框重新输入到达时刻
                                Case 3
                                    '平移站
                                    'MsgBox TrainInf(nTrnNum).Train + "在平移站" + StationInf(nStation).sStationName + _
                                    '  "股道数量不够", 0 + 16, "铺画运行线"
                                    If nTempSt = nBstation Then
                                        'MsgBox TrainInf(nTrnNum).Train + "由于在平移站" + StationInf(nStation).sStationName + _
                                        ' "股道数量不够，需改变" + StationInf(nTempSt).sStationName + "站的出发时刻", 0 + 16, "铺画运行线"
                                        lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                                        lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nTempSt), lMoveTimetemp)
                                        nTrnState = 1
                                        nStation = nBstation

                                        nLeftRightMove("右移", nStation, "出发")

                                    Else
                                        If TrainInf(nTrnNum).Arrival(nTempSt) = TrainInf(nTrnNum).Starting(nTempSt) Then

                                            ChangeStationNum(nTrnNum, nTempSt, nUporDown, 1)
                                            nStation = nFindHstaNum(nTrnNum, nTempSt, nUporDown)
                                            nStation = nBeginStation(nTrnNum, nStation)

                                            lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nTempSt), TimeT(nTrnNum, nStation, nTempSt))
                                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nTempSt), lMoveTimetemp)
                                            nTrnState = 3

                                            nLeftRightMove("右移", nTempSt, "出发")

                                        Else
                                            lArrivalTime = TrainInf(nTrnNum).Arrival(nTempSt)
                                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nTempSt), lMoveTimetemp)
                                            nStation = nTempSt
                                            nTrnState = 1

                                            nLeftRightMove("右移", nTempSt, "出发")

                                        End If
                                    End If
                                Case Else
                                    '其它站
                                    'MsgBox TrainInf(nTrnNum).Train + "在" + StationInf(nStation).sStationName + _
                                    ' "站股道数量不够", 0 + 16, "铺画运行线"
                                    If nTempSt = nBstation Then
                                        'MsgBox TrainInf(nTrnNum).Train + "由于在" + StationInf(nStation).sStationName + _
                                        ' "股道数量不够，需改变" + StationInf(nTempSt).sStationName + "站的出发时刻", 0 + 16, "铺画运行线"
                                        Dim tmpnStartSta As Integer
                                        tmpnStartSta = nStartstation(nTrnNum, nTempSt, nPhFx)
                                        Select Case tmpnStartSta
                                            Case 3 '平移站
                                                lArrivalTime = TrainInf(nTrnNum).Arrival(nTempSt)
                                                lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                                                nStation = nBstation

                                                nLeftRightMove("右移", nTempSt, "出发")

                                                nTrnState = 1
                                            Case 1 '始发站
                                                lStartTime = TimeAdd(lStartTime, lMoveTimetemp) - TimeRun(nTrnNum, nTempSt, nStation)
                                                lArrivalTime = lStartTime
                                                nStation = nBstation
                                                nTrnState = 1
                                                nLeftRightMove("右移", nTempSt, "出发")

                                            Case 2 '接入站
                                                If TrainInf(nTrnNum).Arrival(nTempSt) = TrainInf(nTrnNum).Starting(nTempSt) Then
                                                    lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                                                    lArrivalTime = lStartTime
                                                    nStation = nBstation
                                                    nTrnState = nBeginDeal(lArrivalTime, lStartTime, nStation, nTrnNum)
                                                Else
                                                    lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                                                    lArrivalTime = TimeMinus(lStartTime, TimeMinus(TrainInf(nTrnNum).Starting(nTempSt), TrainInf(nTrnNum).Arrival(nTempSt)))
                                                    nStation = nBstation
                                                    nTrnState = 1

                                                    nLeftRightMove("右移", nTempSt, "出发")

                                                End If
                                        End Select
                                    Else
                                        If TrainInf(nTrnNum).Arrival(nTempSt) = TrainInf(nTrnNum).Starting(nTempSt) Then

                                            ChangeStationNum(nTrnNum, nTempSt, nUporDown, 1)
                                            nStation = nFindHstaNum(nTrnNum, nTempSt, nUporDown)
                                            nStation = nBeginStation(nTrnNum, nStation)

                                            lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nTempSt), TimeT(nTrnNum, nStation, nTempSt))
                                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nTempSt), lMoveTimetemp)
                                            'lStartTime = TimeAdd(lStartTime, lMoveTimetemp) - TimeRun(nTrnNum, nStation, nTempSt)
                                            nTrnState = 3

                                            nLeftRightMove("右移", nTempSt, "出发")

                                        Else
                                            lArrivalTime = TrainInf(nTrnNum).Arrival(nTempSt)
                                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nTempSt), lMoveTimetemp)
                                            ' lStartTime = TimeMinus(TrainInf(nTrnNum).Starting(nTempSt), 360)
                                            nStation = nTempSt
                                            nTrnState = 1

                                            nLeftRightMove("右移", nTempSt, "出发")

                                        End If
                                    End If
                            End Select
                        Case 5
                            '由于发车检查发现为反向到达
                            Select Case nStartstation(nTrnNum, nStation, nPhFx)
                                Case 0
                                    '途中站
                                    ChangeStationNum(nTrnNum, nStation, nUporDown, 1)
                                    nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                                    nTempSt = nBeginStation(nTrnNum, nTempSt)
                                    'MsgBox TrainInf(nTrnNum).Train + "在" + StationInf(nStation).sStationName + _
                                    '     "站反向出发", 0 + 16, "铺画运行线"
                                    nStation = nTempSt
                                    nTrnState = 3
                                Case 1
                                    '始发站
                                    'MsgBox TrainInf(nTrnNum).Train + "在始发站" + StationInf(nStation).sStationName + _
                                    '    "反向出发", 0 + 16, "铺画运行线"
                                    nTrnState = 1
                                Case 2
                                    '接入站(要)
                                    'MsgBox TrainInf(nTrnNum).Train + "在接入站" + StationInf(nStation).sStationName + _
                                    '  "反向出发", 0 + 16, "铺画运行线"
                                    nTrnState = 3
                                Case 3
                                    '平移站
                                    ChangeStationNum(nTrnNum, nStation, nUporDown, 1)
                                    nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                                    nTempSt = nBeginStation(nTrnNum, nTempSt)
                                    MsgBox(TrainInf(nTrnNum).Train + "在平移站" + StationInf(nStation).sStationName + _
                                            "反向出发", 0 + 16, "铺画运行线")
                                    nStation = nTempSt
                                    nTrnState = 3
                            End Select
                        Case 6
                            If TrainInf(nTrnNum).TrainClassCal = 12 Then
                                MsgBox(TrainInf(nTrnNum).Train & "在车底停留站" & StationInf(nStation).sStationName & _
                                    "股道数量不足，需修改车底折返方式", 0 + 16)
                                Exit Sub
                            ElseIf TrainInf(nTrnNum).TrainClassCal <> 12 Then
                                MsgBox(TrainInf(nTrnNum).Train & "在车底停留站" & StationInf(nStation).sStationName & _
                                    "股道数量不足，需修改车底折返方式", 0 + 16)
                                sChangeTrainReturn = "修改车底折返方式"
                                lArrivalTime = TimeMinus(lStartTime, nStartTchTime(nTrnNum))
                                'Exit Sub
                            End If
                    End Select
                Case 2
                    '通过处理（从nStation站通过nQStation站）
                    nLeaveSection = nFindSecNum(nStation, nQStation, nUporDown)
                    lArrivalTime = TimeAdd(lStartTime, TimeRun(nTrnNum, nStation, nQStation))
                    lStartTime = lArrivalTime
                    lStoporNot = StoporPass(nTrnNum, nQStation, lSDtime, lArrivalTime, nUporDown, 1)
                    ChangeStationNum(nTrnNum, nQStation, nUporDown, 0)
                    nTempSt = nFindQstaNum(nTrnNum, nQStation, nUporDown)
                    nEnterSection = nFindSecNum(nQStation, nTempSt, nUporDown)

                    '江编2003.07.20
                    If nLeaveSection = nEnterSection Then
                        nEnterSection = EnterSecCheck(nLeaveSection, nEnterSection, nUporDown)
                    End If

                    If lStoporNot = 0 Then
                        '不需停车
                        Dim tmpnGuDaoPass As Integer
                        tmpnGuDaoPass = nGuDaoPass(lArrivalTime, nTrnNum, nLeaveSection, nQStation, nEnterSection)
                        If lStoporNot > 0 Then Stop
                        Select Case tmpnGuDaoPass
                            Case 0
                                '能够通过，记录到发时刻
                                Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                nStation = nQStation
                                nTrnState = 2
                            Case 1
                                '与前后行均不够，不能通过，检查能否停车
                                ChangeStationNum(nTrnNum, nStation, nUporDown, 1)
                                nTempSt = nFindHstaNum(nTrnNum, nQStation, nUporDown)
                                nTempSt = nBeginStation(nTrnNum, nTempSt)
                                lStartTime = TimeAdd(lArrivalTime, 60)
                                If StationInf(nTempSt).sStationName = TrainInf(nTrnNum).ComeStation Then
                                    nTmpTime = TimeAdd(TrainInf(nTrnNum).Starting(nTempSt), TimeAdd(TimeRun(nTrnNum, nTempSt, nQStation), TimeQ(nTrnNum, nTempSt, nQStation)))
                                    nTmpTime = TimeAdd(nTmpTime, TimeT(nTrnNum, nTempSt, nStation))
                                Else
                                    If TrainInf(nTrnNum).Starting(nTempSt) = TrainInf(nTrnNum).Arrival(nTempSt) Then
                                        nTmpTime = TimeAdd(TrainInf(nTrnNum).Starting(nTempSt), TimeRun(nTrnNum, nTempSt, nQStation))
                                        nTmpTime = TimeAdd(nTmpTime, TimeT(nTrnNum, nTempSt, nStation))
                                    Else
                                        nTmpTime = TimeAdd(TrainInf(nTrnNum).Starting(nTempSt), TimeAdd(TimeRun(nTrnNum, nTempSt, nQStation), TimeQ(nTrnNum, nTempSt, nQStation)))
                                        nTmpTime = TimeAdd(nTmpTime, TimeT(nTrnNum, nTempSt, nStation))
                                    End If
                                End If
                                lArrivalTime = nTmpTime
                                If AddLitterTime(lArrivalTime) - AddLitterTime(lStartTime) > 0 Then
                                    lStartTime = TimeAdd(lArrivalTime, 60)
                                End If
                                nStation = nTempSt
                                nTrnState = 3
                                StationLineNoOccupy(nTrnNum, nQStation)
                            Case 2
                                '与前行车通过间隔不够时，决定能否右移运行线

                                lMoveTimetemp = FindPassTime(2, nTrnNum, lArrivalTime, nBstation, _
                                                nUporDown, nLeaveSection, nEnterSection, nPhFx)
                                If lMoveTimetemp > 0 Then
                                    'MoveTimetemp = 30
                                    '能够右移
                                    Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                    nStopSt = FindSstaNum(nTrnNum, nQStation, nBstation, nUporDown, 1)

                                    '****************移到前一站
                                    HMoveLine(nTrnNum, nQStation, lMoveTimetemp, nStopSt, nUporDown, 1)
                                    lStartTime = TrainInf(nTrnNum).Starting(nQStation)
                                    lArrivalTime = TrainInf(nTrnNum).Arrival(nQStation)
                                    nStation = nQStation
                                    nTrnState = 2
                                    '*****************

                                    '***************移到后一站
                                    'ChangeStationNum(nTrnNum, nQStation, nUporDown, 1)
                                    'nTempSt = nFindHstaNum(nTrnNum, nQStation, nUporDown)
                                    'nTempSt = nBeginStation(nTrnNum, nTempSt)
                                    'lArrivalTime = TimeAdd(lArrivalTime, TimeT(nTrnNum, nTempSt, nQStation))
                                    'lStartTime = TimeAdd(lArrivalTime, 60)
                                    'nStation = nTempSt
                                    'nTrnState = 3
                                    'StationLineNoOccupy(nTrnNum, nQStation)
                                    '**********************


                                    '当前站直接右移
                                    'lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                                    'lArrivalTime = TimeAdd(lArrivalTime, lMoveTimetemp)
                                    'If StationInf(nStation).sStationName = TrainInf(nTrnNum).ComeStation Then
                                    '    nTrnState = 1
                                    'Else
                                    '    nTrnState = 2
                                    'End If
                                ElseIf lMoveTimetemp = 0 Then
                                    '不能右移
                                    If lCanPaoDian(nTrnNum, nQStation) <> 0 Then
                                        lArrivalTime = lArrivalTime + lCanPaoDian(nTrnNum, nQStation)
                                        lStartTime = lArrivalTime
                                        Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                        nStation = nQStation
                                        nTrnState = 2
                                    Else
                                        ChangeStationNum(nTrnNum, nQStation, nUporDown, 1)
                                        nTempSt = nFindHstaNum(nTrnNum, nQStation, nUporDown)
                                        nTempSt = nBeginStation(nTrnNum, nTempSt)
                                        lArrivalTime = TimeAdd(lArrivalTime, TimeT(nTrnNum, nTempSt, nQStation))
                                        lStartTime = TimeAdd(lArrivalTime, 60)
                                        nStation = nTempSt
                                        nTrnState = 3
                                        StationLineNoOccupy(nTrnNum, nQStation)
                                    End If
                                ElseIf lMoveTimetemp < 0 Then
                                    '能够左移
                                    Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                    nStopSt = FindSstaNum(nTrnNum, nQStation, nBstation, nUporDown, 1)
                                    QMoveLine(nTrnNum, nQStation, lMoveTimetemp, nStopSt, nUporDown, 1)
                                    lStartTime = TrainInf(nTrnNum).Starting(nQStation)
                                    lArrivalTime = TrainInf(nTrnNum).Arrival(nQStation)
                                    nStation = nQStation
                                    nTrnState = 2
                                End If
                            Case 3
                                '与后行车通过间隔不够时，决定能否左移运行线
                                lMoveTimetemp = FindPassTime(3, nTrnNum, lArrivalTime, nBstation, _
                                                nUporDown, nLeaveSection, nEnterSection, nPhFx)
                                If lMoveTimetemp >= 0 Then
                                    lMoveTimetemp = -nMoveStepTime
                                End If

                                If lMoveTimetemp > 0 Then
                                    '能够左移
                                    Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                    nStopSt = FindSstaNum(nTrnNum, nQStation, nBstation, nUporDown, 1)
                                    QMoveLine(nTrnNum, nQStation, lMoveTimetemp, nStopSt, nUporDown, 1)
                                    lStartTime = TrainInf(nTrnNum).Starting(nQStation)
                                    lArrivalTime = TrainInf(nTrnNum).Arrival(nQStation)
                                    nStation = nQStation
                                    nTrnState = 2
                                Else
                                    '不能左移
                                    'ChangeStationNum(nTrnNum, nStation, nUporDown, 1)
                                    'nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                                    'nTempSt = nBeginStation(nTrnNum, nTempSt)
                                    'lStartTime = TimeAdd(lArrivalTime, 60)
                                    ''lArrivalTime = TrainInf(nTrnNum).Starting(nStation)
                                    'lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nStation, nQStation)) '
                                    'nStation = nTempSt
                                    'nTrnState = 3
                                    'StationLineNoOccupy(nTrnNum, nQStation)

                                    ChangeStationNum(nTrnNum, nQStation, nUporDown, 1)
                                    nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                                    nTempSt = nBeginStation(nTrnNum, nTempSt)
                                    lStartTime = TimeAdd(lArrivalTime, -lMoveTimetemp)
                                    ' nTmpTime = MoveJiangGeLineTime(nTrnNum, nTempSt, nStation, nUporDown)
                                    nTmpTime = TimeAdd(lArrivalTime, TimeT(nTrnNum, nTempSt, nQStation))
                                    lArrivalTime = nTmpTime
                                    If AddLitterTime(lArrivalTime) - AddLitterTime(lStartTime) > 0 Then
                                        lStartTime = TimeAdd(lArrivalTime, 60)
                                    End If

                                    'If TimeMinus(lStartTime, lArrivalTime) >= TimeMinus(lArrivalTime, lStartTime) Then
                                    'End If
                                    'nStation = nTempSt
                                    nTrnState = 3
                                    StationLineNoOccupy(nTrnNum, nStation)
                                End If
                            Case 4
                                '正线被占用，检查该站能否停车
                                ChangeStationNum(nTrnNum, nQStation, nUporDown, 1)
                                nTempSt = nFindHstaNum(nTrnNum, nQStation, nUporDown)
                                nTempSt = nBeginStation(nTrnNum, nTempSt)
                                lArrivalTime = TimeAdd(lArrivalTime, TimeT(nTrnNum, nTempSt, nQStation))
                                lStartTime = TimeAdd(lArrivalTime, 60)
                                nStation = nTempSt
                                nTrnState = 3
                                StationLineNoOccupy(nTrnNum, nQStation)
                        End Select
                        Select Case nEndStation(nTrnNum, nStation, nPhFx)
                            Case 1, 2
                                Exit Do
                            Case 0, 3
                        End Select
                    Else
                        '需要停车
                        ChangeStationNum(nTrnNum, nQStation, nUporDown, 1)
                        nTempSt = nFindHstaNum(nTrnNum, nQStation, nUporDown)
                        nTempSt = nBeginStation(nTrnNum, nTempSt)
                        lArrivalTime = TimeAdd(lArrivalTime, TimeT(nTrnNum, nTempSt, nQStation))
                        lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                        nStation = nTempSt
                        nTrnState = 3
                    End If
                Case 3
                    '到达处理（从nStation站到达nQStation站）
                    If sChangeTrainReturn = "无" Then
                        nLeaveSection = nFindSecNum(nStation, nQStation, nUporDown)
                        nTempSt = nQStation
                        ChangeStationNum(nTrnNum, nTempSt, nUporDown, 0)
                        nStopSt = nTempSt
                        nTempSt = nFindQstaNum(nTrnNum, nTempSt, nUporDown)
                        nEnterSection = nFindSecNum(nQStation, nTempSt, nUporDown)
                        If nEnterSection = 0 Then nEnterSection = nLeaveSection
                        ChangeStationNum(nTrnNum, nStopSt, nUporDown, 1)

                        lArrivalTime = lArrivalTimeReSet(nTrnNum, lArrivalTime, nQStation)
                        lStartTime = lStartTimeReSet(nTrnNum, lStartTime, nQStation)
                    End If
                    Select Case nEndStation(nTrnNum, nQStation, nPhFx)
                        Case 1
                            '终到站
                            If sChangeTrainReturn = "无" Then
                                lStartTime = DealKeCheArrival(nTrnNum, nQStation, lArrivalTime)
                            End If
                        Case Else
                    End Select

                    '江编2003.07.20
                    If nLeaveSection = nEnterSection Then
                        nEnterSection = EnterSecCheck(nLeaveSection, nEnterSection, nUporDown)
                    End If

                    Dim tmpnGuDaoStop As Integer
                    trainAdjustStyle.nStyle = 0
                    tmpnGuDaoStop = nGuDaoStop(lArrivalTime, lStartTime, nTrnNum, nLeaveSection, nQStation, nEnterSection, nPhFx)
                    Dim nArriMoveState As String
                    nArriMoveState = 0
                    ' If tmpnGuDaoStop > 0 Then Stop
                    Select Case tmpnGuDaoStop
                        Case 0
                            '下站能够到达，记录到发时刻，形成发车点
                            ChangeStationNum(nTrnNum, nQStation, nUporDown, 0)
                            nStation = nQStation
                            nTrnState = 1
                            Select Case nEndStation(nTrnNum, nStation, nPhFx)
                                Case 1  '终到站
                                    DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 1)
                                    Exit Do
                                Case 2
                                    DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 2)
                                    Exit Do
                                Case 3
                                    DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 3)
                                    Exit Do
                                Case Else
                                    If sChangeTrainReturn = "修改车底折返方式" Then
                                        sChangeTrainReturn = "无"
                                    End If
                                    Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                            End Select
                        Case 1
                            '前方站前后行均不够，不能停车,移到后一站
                            'nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                            'lArrivalTime = GetCurTrainArriStaTime(nTrnNum, nTempSt, nStation, nUporDown)
                            'lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                            'If AddLitterTime(lArrivalTime) - AddLitterTime(lStartTime) >0 Then
                            '    lStartTime = TimeAdd(lArrivalTime, 60)
                            'End If
                            'Record(nTrnNum, nStation, lStartTime, lArrivalTime)
                            'nTrnState = 1

                            lMoveTimetemp = FindStopTime(1, nTrnNum, lArrivalTime, nBstation, _
                                         nUporDown, nLeaveSection, nEnterSection, nPhFx)
                            If TrainInf(nTrnNum).Arrival(nStation) = TrainInf(nTrnNum).Starting(nStation) Then
                                ChangeStationNum(nTrnNum, nStation, nUporDown, 1)
                                Select Case nStartstation(nTrnNum, nStation, nPhFx)
                                    Case 0, 3 '平移站、途中站
                                        nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                                        nTempSt = nBeginStation(nTrnNum, nTempSt)
                                        lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nTempSt, nStation))
                                        lStartTime = TimeAdd(lMoveTimetemp, 60)

                                        nLeftRightMove("右移", nStation, "出发")

                                        StationLineNoOccupy(nTrnNum, nStation)
                                        nStation = nTempSt
                                        nTrnState = 3
                                    Case 1 '始发站
                                        lStartTime = TimeAdd(lMoveTimetemp, 60)
                                        lArrivalTime = lStartTime
                                        nStation = nBstation
                                        nTrnState = 1

                                        nLeftRightMove("右移", nStation, "出发")

                                    Case 2 '接入站
                                        If nStartStop = 0 Then
                                            If MsgBox(TrainInf(nTrnNum).Train + "由于在" + StationInf(nQStation).sStationName + _
                                            "停车间隔时间不够，是否在接入站" + StationInf(nStation).sStationName + _
                                            "停车", 4 + 16, "铺画运行线") = vbNo Then
                                                nStartStop = 1
                                            End If
                                        End If
                                        If nStartStop = 1 Then
                                            lStartTime = TimeAdd(lMoveTimetemp, 60)
                                            lArrivalTime = lStartTime
                                            nStation = nBstation
                                            nTrnState = nBeginDeal(lArrivalTime, lStartTime, nStation, nTrnNum)
                                        Else
                                            lStartTime = TimeAdd(lMoveTimetemp, 60)
                                            lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nStation, nQStation))
                                            If AddLitterTime(lArrivalTime) - AddLitterTime(lStartTime) > 0 Then
                                                lStartTime = TimeAdd(lArrivalTime, 60)
                                            End If
                                            'If TimeMinus(lStartTime, lArrivalTime) > TimeMinus(lArrivalTime, lStartTime) Then
                                            'End If
                                            nStation = nBstation
                                            nTrnState = 1

                                            nLeftRightMove("右移", nStation, "出发")

                                        End If
                                End Select
                            Else
                                lArrivalTime = TrainInf(nTrnNum).Arrival(nStation)
                                lStartTime = TimeAdd(lMoveTimetemp, 60)
                                nTrnState = 1
                                nLeftRightMove("右移", nStation, "出发")
                            End If
                            StationLineNoOccupy(nTrnNum, nQStation)
                        Case 2

                            '与前行车到达间隔不够，决定能否右移运行线

                            lMoveTimetemp = FindStopTime(2, nTrnNum, lArrivalTime, nBstation, _
                                            nUporDown, nLeaveSection, nEnterSection, nPhFx)

                            If lMoveTimetemp < 86400 And lMoveTimetemp > 0 Then
                                '能够右移

                                ''待修改，2005.10.20
                                'nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                                ''lArrivalTime = GetCurTrainArriStaTime(nTrnNum, nTempSt, nStation, nUporDown)
                                'lStartTime = TimeAdd(lStartTime, lMoveTimetemp) 'TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                ''If AddLitterTime(lArrivalTime) - AddLitterTime(lStartTime) > 0 Then
                                ''    lStartTime = TimeAdd(lArrivalTime, 60)
                                ''End If
                                'Record(nTrnNum, nStation, lStartTime, lArrivalTime)
                                'nTrnState = 1

                                Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                nStopSt = FindSstaNum(nTrnNum, nQStation, nBstation, nUporDown, 1)
                                HMoveLine(nTrnNum, nQStation, lMoveTimetemp, nStopSt, nUporDown, 1)
                                lArrivalTime = TrainInf(nTrnNum).Arrival(nQStation)
                                lStoporNot = lNeedStop(nTrnNum, nQStation, 4)
                                If lStoporNot = 0 Then lStoporNot = 60
                                If TimeMinus(lStartTime, lArrivalTime) > TimeMinus(lArrivalTime, lStartTime) Then
                                    lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                                Else
                                    If TimeMinus(lStartTime, lArrivalTime) < lStoporNot Then
                                        lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                                    End If
                                End If
                                ChangeStationNum(nTrnNum, nQStation, nUporDown, 0)
                                'nStation = nQStation
                                lArrivalTime = TrainInf(nTrnNum).Arrival(nStation)
                                lStartTime = TrainInf(nTrnNum).Starting(nStation)
                                nTrnState = 1
                                lStoporNot = 0
                                Select Case nEndStation(nTrnNum, nStation, nPhFx)
                                    Case 1  '终到站
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 1)
                                        Exit Do
                                    Case 2
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 2)
                                        Exit Do
                                    Case 3
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 3)
                                        Exit Do
                                    Case Else
                                        If sChangeTrainReturn = "修改车底折返方式" Then
                                            sChangeTrainReturn = "无"
                                        End If
                                        Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                End Select
                            ElseIf lMoveTimetemp >= 86400 Then
                                '不能右移,移至后一站
                                If lCanPaoDian(nTrnNum, nQStation) <> 0 Then
                                    lArrivalTime = lArrivalTime + lCanPaoDian(nTrnNum, nQStation)
                                    Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)

                                    lStoporNot = lNeedStop(nTrnNum, nQStation, 4)
                                    If lStoporNot = 0 Then lStoporNot = 60

                                    'If TimeMinus(lStartTime, lArrivalTime) > TimeMinus(lArrivalTime, lStartTime) Then
                                    If AddLitterTime(lArrivalTime) - AddLitterTime(lStartTime) > 0 Then
                                        lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                                    Else
                                        If TimeMinus(lStartTime, lArrivalTime) < lStoporNot Then
                                            lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                                        End If
                                    End If
                                    ChangeStationNum(nTrnNum, nQStation, nUporDown, 0)
                                    nStation = nQStation
                                    nTrnState = 1
                                    lStoporNot = 0
                                    Select Case nEndStation(nTrnNum, nStation, nPhFx)
                                        Case 1  '终到站
                                            DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 1)
                                            Exit Do
                                        Case 2
                                            DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 2)
                                            Exit Do
                                        Case 3
                                            DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 3)
                                            Exit Do
                                        Case Else
                                            If sChangeTrainReturn = "修改车底折返方式" Then
                                                sChangeTrainReturn = "无"
                                            End If
                                            Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                    End Select
                                Else
                                    lMoveTimetemp = lMoveTimetemp - 86400
                                    If TrainInf(nTrnNum).Arrival(nStation) = TrainInf(nTrnNum).Starting(nStation) Then
                                        ChangeStationNum(nTrnNum, nStation, nUporDown, 1)
                                        Select Case nStartstation(nTrnNum, nStation, nPhFx)
                                            Case 0, 3 '平移站、途中站
                                                nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                                                nTempSt = nBeginStation(nTrnNum, nTempSt)
                                                lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nTempSt, nStation))
                                                lStartTime = TimeAdd(lArrivalTime, 60)

                                                ''''''''''''''''''''
                                                'lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                                'If TimeMinus(lStartTime, lArrivalTime) >= TimeMinus(lArrivalTime, lStartTime) Then
                                                '    lStartTime = TimeAdd(lArrivalTime, 60)
                                                'End If

                                                nLeftRightMove("右移", nStation, "出发")

                                                StationLineNoOccupy(nTrnNum, nStation)
                                                nStation = nTempSt
                                                nTrnState = 3
                                            Case 1 '始发站
                                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                                lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), lMoveTimetemp)
                                                nStation = nBstation
                                                nTrnState = 1

                                                nLeftRightMove("右移", nStation, "出发")

                                            Case 2 '接入站
                                                If MsgBox(TrainInf(nTrnNum).Train + "由于在" + StationInf(nQStation).sStationName + _
                                                    "停车间隔时间不够，是否在接入站" + StationInf(nStation).sStationName + _
                                                    "停车", 4 + 16, "铺画运行线") = vbNo Then
                                                    lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                                    lArrivalTime = lStartTime
                                                    nStation = nBstation
                                                    nTrnState = nBeginDeal(lArrivalTime, lStartTime, nStation, nTrnNum)
                                                Else
                                                    lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nStation, nQStation))
                                                    lStartTime = TimeAdd(lArrivalTime, 60)

                                                    ''''''''''''''''''''''''
                                                    'lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                                    'If TimeMinus(lStartTime, lArrivalTime) > TimeMinus(lArrivalTime, lStartTime) Then
                                                    '    lStartTime = TimeAdd(lArrivalTime, 60)
                                                    'End If
                                                    nStation = nBstation
                                                    nTrnState = 1
                                                    nLeftRightMove("右移", nStation, "出发")
                                                End If
                                        End Select
                                    Else
                                        lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                        lArrivalTime = TrainInf(nTrnNum).Arrival(nStation)
                                        nTrnState = 1

                                        nLeftRightMove("右移", nStation, "出发")

                                    End If
                                    StationLineNoOccupy(nTrnNum, nQStation)
                                End If
                            ElseIf lMoveTimetemp < 0 Then
                                '能够左移
                                Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                nStopSt = FindSstaNum(nTrnNum, nQStation, nBstation, nUporDown, 1)
                                QMoveLine(nTrnNum, nQStation, lMoveTimetemp, nStopSt, nUporDown, 1)
                                lArrivalTime = TrainInf(nTrnNum).Arrival(nQStation)
                                lStartTime = TimeAdd(lStartTime, -lMoveTimetemp)
                                lStoporNot = lNeedStop(nTrnNum, nQStation, 4)
                                If lStoporNot = 0 Then lStoporNot = 60
                                If TimeMinus(lStartTime, lArrivalTime) < lStoporNot Then
                                    lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                                End If
                                ChangeStationNum(nTrnNum, nQStation, nUporDown, 0)
                                nStation = nQStation
                                nTrnState = 1
                                lStoporNot = 0
                                Select Case nEndStation(nTrnNum, nStation, nPhFx)
                                    Case 1  '终到站
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 1)
                                        Exit Do
                                    Case 2
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 2)
                                        Exit Do
                                    Case 3
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 3)
                                        Exit Do
                                    Case Else
                                        If sChangeTrainReturn = "修改车底折返方式" Then
                                            sChangeTrainReturn = "无"
                                        End If
                                        Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                End Select
                            End If
                        Case 3
                            '与后行车到达间隔不够，决定能否左移运行线
                            lMoveTimetemp = FindStopTime(3, nTrnNum, lArrivalTime, nBstation, _
                                            nUporDown, nLeaveSection, nEnterSection, nPhFx)
                            If lMoveTimetemp > 0 Then
                                lMoveTimetemp = -nMoveStepTime
                            End If
                            If lMoveTimetemp > 0 Then
                                '能够左移
                                Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                nStopSt = FindSstaNum(nTrnNum, nQStation, nBstation, nUporDown, 1)
                                QMoveLine(nTrnNum, nQStation, lMoveTimetemp, nStopSt, nUporDown, 1)
                                lArrivalTime = TrainInf(nTrnNum).Arrival(nQStation)
                                lStoporNot = lNeedStop(nTrnNum, nQStation, 4)
                                If lStoporNot = 0 Then lStoporNot = 60
                                If TimeMinus(lStartTime, lArrivalTime) < lStoporNot Then
                                    lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                                End If
                                ChangeStationNum(nTrnNum, nQStation, nUporDown, 0)
                                nStation = nQStation
                                nTrnState = 1
                                lStoporNot = 0
                                Select Case nEndStation(nTrnNum, nStation, nPhFx)
                                    Case 1  '终到站
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 1)
                                        Exit Do
                                    Case 2
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 2)
                                        Exit Do
                                    Case 3
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 3)
                                        Exit Do
                                    Case Else
                                        If sChangeTrainReturn = "修改车底折返方式" Then
                                            sChangeTrainReturn = "无"
                                        End If
                                        Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                End Select
                            Else
                                '不能左移,移至后一站
                                If TrainInf(nTrnNum).Arrival(nStation) = TrainInf(nTrnNum).Starting(nStation) Then
                                    ChangeStationNum(nTrnNum, nStation, nUporDown, 1)
                                    Dim tmpnStartStaSta As Integer
                                    tmpnStartStaSta = nStartstation(nTrnNum, nStation, nPhFx)
                                    Select Case tmpnStartStaSta
                                        Case 0, 3 '平移站、途中站
                                            nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                                            nTempSt = nBeginStation(nTrnNum, nTempSt)
                                            lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nTempSt, nStation))
                                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), -lMoveTimetemp)
                                            If AddLitterTime(lArrivalTime) - AddLitterTime(lStartTime) > 0 Then
                                                lStartTime = TimeAdd(lArrivalTime, 60)
                                            End If
                                            'If TimeMinus(lStartTime, lArrivalTime) >= TimeMinus(lArrivalTime, lStartTime) Then
                                            'End If

                                            nLeftRightMove("右移", nStation, "出发")

                                            StationLineNoOccupy(nTrnNum, nStation)
                                            nStation = nTempSt
                                            nTrnState = 3

                                        Case 1 '始发站
                                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), -lMoveTimetemp)
                                            lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), -lMoveTimetemp)
                                            nStation = nBstation
                                            nTrnState = 1

                                            nLeftRightMove("右移", nStation, "出发")

                                        Case 2 '接入站
                                            If MsgBox(TrainInf(nTrnNum).Train + "由于在" + StationInf(nQStation).sStationName + _
                                                "停车间隔时间不够，是否在接入站" + StationInf(nStation).sStationName + _
                                                "停车", 4 + 16, "铺画运行线") = vbNo Then
                                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), -lMoveTimetemp)
                                                lArrivalTime = lStartTime
                                                nStation = nBstation
                                                nTrnState = nBeginDeal(lArrivalTime, lStartTime, nStation, nTrnNum)
                                            Else
                                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), -lMoveTimetemp)
                                                lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nStation, nQStation))
                                                If AddLitterTime(lArrivalTime) - AddLitterTime(lStartTime) > 0 Then
                                                    lStartTime = TimeAdd(lArrivalTime, 60)
                                                End If
                                                'If TimeMinus(lStartTime, lArrivalTime) > TimeMinus(lArrivalTime, lStartTime) Then
                                                'End If
                                                nStation = nBstation
                                                nTrnState = 1

                                                nLeftRightMove("右移", nStation, "出发")

                                            End If
                                    End Select
                                Else

                                    lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), -lMoveTimetemp)
                                    lArrivalTime = TrainInf(nTrnNum).Arrival(nStation)
                                    nTrnState = 1
                                    nLeftRightMove("右移", nStation, "出发")

                                End If
                                StationLineNoOccupy(nTrnNum, nQStation)
                            End If
                        Case 4
                            '前方站股道数量不够，移至后一站
                            lMoveTimetemp = FindStopTime(4, nTrnNum, lArrivalTime, nBstation, _
                                            nUporDown, nLeaveSection, nEnterSection, nPhFx)
                            If lPaoDian > lMoveTimetemp And Right(StationInf(nQStation).sStationProp, 3) <> "XLS" Then
                                lArrivalTime = lArrivalTime + lMoveTimetemp
                                Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)

                                lStoporNot = lNeedStop(nTrnNum, nQStation, 4)
                                If lStoporNot = 0 Then lStoporNot = 60
                                ' If TimeMinus(lStartTime, lArrivalTime) > TimeMinus(lArrivalTime, lStartTime) Then
                                If AddLitterTime(lArrivalTime) - AddLitterTime(lStartTime) > 0 Then
                                    lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                                Else
                                    If TimeMinus(lStartTime, lArrivalTime) < lStoporNot Then
                                        lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                                    End If
                                End If
                                ChangeStationNum(nTrnNum, nQStation, nUporDown, 0)
                                nStation = nQStation
                                nTrnState = 1
                                lStoporNot = 0
                                Select Case nEndStation(nTrnNum, nStation, nPhFx)
                                    Case 1  '终到站
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 1)
                                        Exit Do
                                    Case 2
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 2)
                                        Exit Do
                                    Case 3
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 3)
                                        Exit Do
                                    Case Else
                                        If sChangeTrainReturn = "修改车底折返方式" Then
                                            sChangeTrainReturn = "无"
                                        End If
                                        Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                End Select

                            Else

                                If sChangeTrainReturn = "修改车底折返方式" Then
                                    sChangeTrainReturn = "无"
                                End If
                                If TrainInf(nTrnNum).Arrival(nStation) = TrainInf(nTrnNum).Starting(nStation) Then
                                    Select Case nStartstation(nTrnNum, nStation, nPhFx)
                                        Case 0, 3 '平移站、途中站
                                            ChangeStationNum(nTrnNum, nStation, nUporDown, 1)
                                            nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                                            nTempSt = nBeginStation(nTrnNum, nTempSt)
                                            lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nTempSt, nStation))
                                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                            If AddLitterTime(lArrivalTime) - AddLitterTime(lStartTime) > 0 Then
                                                lStartTime = TimeAdd(lArrivalTime, 60)
                                            End If
                                            'If TimeMinus(lStartTime, lArrivalTime) >= TimeMinus(lArrivalTime, lStartTime) Then
                                            'End If

                                            nLeftRightMove("右移", nStation, "出发")

                                            StationLineNoOccupy(nTrnNum, nStation)
                                            nStation = nTempSt
                                            nTrnState = 3
                                        Case 1 '始发站
                                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                            lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), lMoveTimetemp)
                                            nStation = nBstation
                                            nTrnState = 1

                                            nLeftRightMove("右移", nStation, "出发")

                                        Case 2 '接入站
                                            If MsgBox(TrainInf(nTrnNum).Train + "由于在" + StationInf(nQStation).sStationName + _
                                                "股道数量不够，是否在接入站" + StationInf(nStation).sStationName + _
                                                "停车", 4 + 16, "铺画运行线") = vbNo Then
                                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                                lArrivalTime = lStartTime
                                                nStation = nBstation
                                                nTrnState = nBeginDeal(lArrivalTime, lStartTime, nStation, nTrnNum)
                                            Else
                                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                                lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nStation, nQStation))
                                                If AddLitterTime(lArrivalTime) - AddLitterTime(lStartTime) > 0 Then
                                                    lStartTime = TimeAdd(lArrivalTime, 60)
                                                End If
                                                'If TimeMinus(lStartTime, lArrivalTime) > TimeMinus(lArrivalTime, lStartTime) Then
                                                'End If
                                                nStation = nBstation
                                                nTrnState = 1

                                                nLeftRightMove("右移", nStation, "出发")

                                            End If
                                    End Select
                                Else
                                    lArrivalTime = TrainInf(nTrnNum).Arrival(nStation)
                                    lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                    nTrnState = 1

                                    nLeftRightMove("右移", nStation, "出发")
                                End If
                            End If
                        Case 5
                            If TrainInf(nTrnNum).TrainClassCal = 12 Then
                                MsgBox(TrainInf(nTrnNum).Train & "在车底停留站" & StationInf(nQStation).sStationName & _
                                    "股道数量不足，需修改车底折返方式", 0 + 16)
                                Exit Sub
                            ElseIf TrainInf(nTrnNum).TrainClassCal <> 12 Then
                                MsgBox(TrainInf(nTrnNum).Train & "在车底停留站" & StationInf(nQStation).sStationName & _
                                    "股道数量不足，需修改车底折返方式", 0 + 16)
                                sChangeTrainReturn = "修改车底折返方式"
                                lStartTime = TimeAdd(lArrivalTime, nStartTchTime(nTrnNum))
                                'Exit Sub
                            End If
                    End Select
            End Select
        Loop Until nStation = nEstation And TrainInf(nTrnNum).Starting(nStation) <> -1
        DealTrainStart(nTrnNum, TrainInf(nTrnNum).nPathID(1), TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1)))
        'DealTrainStart(nTrnNum, TrainInf(nTrnNum).nPathID(UBound(TrainInf(nTrnNum).nPathID)), TrainInf(nTrnNum).Arrival(TrainInf(nTrnNum).nPathID(UBound(TrainInf(nTrnNum).nPathID))))
    End Sub

    Public Function GetCurTrainArriStaTime(ByVal ntrnNum As Integer, ByVal nTempSt As Integer, ByVal nstation As Integer, ByVal nUporDown As Integer) As Integer
        Dim nTmpTime As Integer
        If StationInf(nTempSt).sStationName = TrainInf(ntrnNum).ComeStation Then '第一站
            nTmpTime = TimeAdd(TrainInf(ntrnNum).Starting(nTempSt), TimeAdd(TimeRun(ntrnNum, nTempSt, nstation), TimeQ(ntrnNum, nTempSt, nstation)))
            nTmpTime = TimeAdd(nTmpTime, TimeT(ntrnNum, nTempSt, nstation))
        Else
            If TrainInf(ntrnNum).Starting(nTempSt) = TrainInf(ntrnNum).Arrival(nTempSt) Then
                nTmpTime = TimeAdd(TrainInf(ntrnNum).Starting(nTempSt), TimeRun(ntrnNum, nTempSt, nstation))
                nTmpTime = TimeAdd(nTmpTime, TimeT(ntrnNum, nTempSt, nstation))
            Else
                nTmpTime = TimeAdd(TrainInf(ntrnNum).Starting(nTempSt), TimeAdd(TimeRun(ntrnNum, nTempSt, nstation), TimeQ(ntrnNum, nTempSt, nstation)))
                nTmpTime = TimeAdd(nTmpTime, TimeT(ntrnNum, nTempSt, nstation))
            End If
        End If
        GetCurTrainArriStaTime = nTmpTime
    End Function


    Sub TDrawLineMaglev(ByVal lArrivalTime As Long, ByVal lStartTime As Long, ByVal lSDtime As Long, ByVal nTrnNum As Integer, _
    ByVal nBeginSta As Integer, ByVal nEndSta As Integer, ByVal nTiaoorPu As Integer)
        Dim nUporDown As Integer, nTrnState As Integer
        Dim nStation As Integer, nQStation As Integer
        Dim nLeaveSection As Integer, nEnterSection As Integer
        Dim lStoporNot As Long, lMoveTimetemp As Long
        Dim nTempSt As Integer, nStopSt As Integer, nPhFx As Integer
        Dim nStartTimeChange As Integer, nStartStop As Integer
        Dim i As Integer
        nPhFx = 1
        nStartTimeChange = 0
        nStartStop = 0
        ReDim nCanRightMove(UBound(StationInf))
        ReDim nCanLeftMove(UBound(StationInf))
        nPhFx = 1
        nBstation = nBeginSta
        nEstation = nEndSta
        '中间变量初试化
        nStation = nBstation
        nUporDown = nDirection(nTrnNum)
        ' ChangeStationNum(nTrnNum, nStation, nUporDown, 0)
        Initial(nTrnNum, nBstation, nEstation)
        nTrnState = nBeginDeal(lArrivalTime, lStartTime, nStation, nTrnNum)
        sChangeTrainReturn = "无"
        Dim stmptmpSTaName As String
        Dim curStartTime As String
        nMoveStepTimeInTdrawline = 30
        Do
            If lStartTime > 86400 Then '- 2 * 3600 Then
                For i = 1 To UBound(StationInf)
                    TrainInf(nTrnNum).Arrival(i) = -1
                    TrainInf(nTrnNum).Starting(i) = -1
                Next i
                Exit Sub
            End If
            If sChangeTrainReturn = "无" Then
                nQStation = nFindQstaNum(nTrnNum, nStation, nUporDown)
                If nStation <> nQStation Then
                    nStation = nQStation - nSteplen(nTrnNum)
                End If
            End If
            stmptmpSTaName = StationInf(nStation).sStationName
            curStartTime = dTime(lStartTime, 0)
            Select Case nTrnState
                Case 1
                    '出发处理（从nStation站发往nQStation站）
                    If sChangeTrainReturn = "无" Then
                        nEnterSection = nFindSecNum(nStation, nQStation, nUporDown)
                        ' ChangeStationNum(nTrnNum, nStation, nUporDown, 1)
                        nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                        nLeaveSection = nFindSecNum(nTempSt, nStation, nUporDown)

                        If nLeaveSection = 0 Then nLeaveSection = nEnterSection

                        ' ChangeStationNum(nTrnNum, nStation, nUporDown, 0)
                    End If
                    Select Case nStartstation(nTrnNum, nStation, nPhFx)
                        Case 1
                            '始发站处理
                            If sChangeTrainReturn = "无" Then
                                lArrivalTime = DealTrainStart(nTrnNum, nStation, lStartTime)
                            End If
                        Case 2
                            '接入站处理
                        Case 3
                            '其他站处理
                    End Select
                    '股道和出发判别
                    Dim tmpnGuDaoStart As Integer
                    tmpnGuDaoStart = nGuDaoStart(lArrivalTime, lStartTime, nTrnNum, nLeaveSection, nStation, nEnterSection, nPhFx)
                    Select Case tmpnGuDaoStart
                        Case 0
                            Record(nTrnNum, nStation, lStartTime, lArrivalTime)

                            If nEndStation(nTrnNum, nStation, nPhFx) <> 0 Then
                                '本条运行线的终点（终到站、交出站、平移站）
                                Exit Do
                            End If
                            Select Case nStartstation(nTrnNum, nStation, nPhFx)
                                Case 0
                                    lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                    If sChangeTrainReturn = "修改车底折返方式" Then
                                        sChangeTrainReturn = "无"
                                    End If
                                Case 1
                                    DealIfStartStation(nTrnNum, nStation, lStartTime, lArrivalTime, 1)
                                    lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                Case 2
                                    If lArrivalTime <> lStartTime Then
                                        lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                    End If
                                Case 3
                                    If lStartTime <> lArrivalTime Then
                                        lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                    End If
                            End Select
                            nTrnState = 2
                            nLeftRightMove("不移", nStation, "出发")
                        Case 1
                            '与前后行均不够，移动发车时间至后行车后
                            lMoveTimetemp = FindStartTime(1, nTrnNum, lStartTime, nUporDown, _
                                nLeaveSection, nEnterSection, nPhFx)
                            lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                            nTrnState = 1
                            nLeftRightMove("右移", nStation, "出发")
                        Case 2
                            '与前行不够，比较与前行车间的富余时间决定移动的时间
                            lMoveTimetemp = FindStartTime(2, nTrnNum, lStartTime, nUporDown, _
                                nLeaveSection, nEnterSection, nPhFx)
                            lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                            nTrnState = 1
                            nLeftRightMove("右移", nStation, "出发")
                        Case 3
                            '与后行不够，移动发车时间至前行车前或后行车后
                            lMoveTimetemp = FindStartTime(3, nTrnNum, lStartTime, nUporDown, _
                                nLeaveSection, nEnterSection, nPhFx)
                            If lMoveTimetemp < 0 Then
                                '与后行不够，移动发车时间至前行车前
                                lStartTime = TimeMinus(lStartTime, -lMoveTimetemp)
                                If StationInf(nStation).sStationName = TrainInf(nTrnNum).ComeStation Then
                                    lArrivalTime = TimeMinus(lArrivalTime, -lMoveTimetemp)
                                End If
                                nLeftRightMove("左移", nStation, "出发")
                            Else
                                '与后行不够，移动发车时间至后行车后
                                lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                                nLeftRightMove("右移", nStation, "出发")
                            End If
                            nTrnState = 1
                        Case 4
                            '本站股道数量不够，移至后一站
                            lMoveTimetemp = FindStartTime(4, nTrnNum, lArrivalTime, nUporDown, _
                                nLeaveSection, nEnterSection, nPhFx)
                            nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                            nTempSt = nBeginStation(nTrnNum, nTempSt)
                            Do While Right(StationInf(nTempSt).sStationProp, 3) = "XLS"
                                nTempSt = nFindHstaNum(nTrnNum, nTempSt, nUporDown)
                                nTempSt = nBeginStation(nTrnNum, nTempSt)
                            Loop
                            StationLineNoOccupy(nTrnNum, nStation)
                            'nStation = nTempSt
                            Dim tmpnStartstation As Integer
                            tmpnStartstation = nStartstation(nTrnNum, nStation, nPhFx)
                            Select Case tmpnStartstation
                                Case 1
                                    '始发站

                                    If nStartTimeChange = 0 Then
                                        'MsgBox TrainInf(nTrnNum).Train + "在始发站" + StationInf(nStation).sStationName + _
                                        '     "股道数量不够，是否改变始发时刻？", 0 + 16, "铺画运行线"
                                        nStartTimeChange = 1
                                    End If
                                    lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                                    lArrivalTime = TimeAdd(lArrivalTime, lMoveTimetemp)
                                    nTrnState = 1
                                    nStation = nBstation

                                    nLeftRightMove("右移", nStation, "出发")

                                Case 2
                                    '接入站(要)
                                    'MsgBox TrainInf(nTrnNum).Train + "在接入站" + StationInf(nStation).sStationName + _
                                    '  "股道数量不够，是否改变到达时刻？", 0 + 16, "铺画运行线"
                                    lArrivalTime = TimeAdd(lArrivalTime, lMoveTimetemp)
                                    lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                                    nTrnState = 1
                                    nStation = nBstation

                                    nLeftRightMove("右移", nStation, "出发")

                                    '加对话框重新输入到达时刻
                                Case 3
                                    '平移站
                                    'MsgBox TrainInf(nTrnNum).Train + "在平移站" + StationInf(nStation).sStationName + _
                                    '   "股道数量不够", 0 + 16, "铺画运行线"
                                    If nTempSt = nBstation Then
                                        'MsgBox TrainInf(nTrnNum).Train + "由于在平移站" + StationInf(nStation).sStationName + _
                                        '   "股道数量不够，需改变" + StationInf(nTempSt).sStationName + "站的出发时刻", 0 + 16, "铺画运行线"
                                        lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                                        lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nTempSt), lMoveTimetemp)
                                        nTrnState = 1
                                        nStation = nBstation

                                        nLeftRightMove("右移", nStation, "出发")

                                    Else
                                        If TrainInf(nTrnNum).Arrival(nTempSt) = TrainInf(nTrnNum).Starting(nTempSt) Then
                                            nStation = nFindHstaNum(nTrnNum, nTempSt, nUporDown)
                                            nStation = nBeginStation(nTrnNum, nStation)
                                            lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nTempSt), TimeT(nTrnNum, nStation, nTempSt))
                                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nTempSt), lMoveTimetemp)
                                            nTrnState = 3
                                            nLeftRightMove("右移", nTempSt, "出发")
                                        Else
                                            lArrivalTime = TrainInf(nTrnNum).Arrival(nTempSt)
                                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nTempSt), lMoveTimetemp)
                                            nStation = nTempSt
                                            nTrnState = 1

                                            nLeftRightMove("右移", nTempSt, "出发")

                                        End If
                                    End If
                                Case Else
                                    '其它站
                                    'MsgBox TrainInf(nTrnNum).Train + "在" + StationInf(nStation).sStationName + _
                                    '    "站股道数量不够", 0 + 16, "铺画运行线"
                                    If nTempSt = nBstation Then
                                        'MsgBox TrainInf(nTrnNum).Train + "由于在" + StationInf(nStation).sStationName + _
                                        '  "股道数量不够，需改变" + StationInf(nTempSt).sStationName + "站的出发时刻", 0 + 16, "铺画运行线"
                                        Select Case nStartstation(nTrnNum, nTempSt, nPhFx)
                                            Case 3 '平移站
                                                lArrivalTime = TrainInf(nTrnNum).Arrival(nTempSt)
                                                lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                                                nStation = nBstation
                                                nLeftRightMove("右移", nTempSt, "出发")
                                                nTrnState = 1
                                            Case 1 '始发站
                                                lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                                                lArrivalTime = lStartTime
                                                nStation = nBstation
                                                nTrnState = 1

                                                nLeftRightMove("右移", nTempSt, "出发")


                                            Case 2 '接入站
                                                If TrainInf(nTrnNum).Arrival(nTempSt) = TrainInf(nTrnNum).Starting(nTempSt) Then
                                                    lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                                                    lArrivalTime = lStartTime
                                                    nStation = nBstation
                                                    nTrnState = nBeginDeal(lArrivalTime, lStartTime, nStation, nTrnNum)
                                                Else
                                                    lStartTime = TimeAdd(lStartTime, lMoveTimetemp)
                                                    lArrivalTime = TimeMinus(lStartTime, TimeMinus(TrainInf(nTrnNum).Starting(nTempSt), TrainInf(nTrnNum).Arrival(nTempSt)))
                                                    nStation = nBstation
                                                    nTrnState = 1

                                                    nLeftRightMove("右移", nTempSt, "出发")

                                                End If
                                        End Select
                                    Else
                                        If TrainInf(nTrnNum).Arrival(nTempSt) = TrainInf(nTrnNum).Starting(nTempSt) Then

                                            nStation = nFindHstaNum(nTrnNum, nTempSt, nUporDown)
                                            nStation = nBeginStation(nTrnNum, nStation)

                                            lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nTempSt), TimeT(nTrnNum, nStation, nTempSt))
                                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nTempSt), lMoveTimetemp)
                                            nTrnState = 3

                                            nLeftRightMove("右移", nTempSt, "出发")

                                        Else
                                            lArrivalTime = TrainInf(nTrnNum).Arrival(nTempSt)
                                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nTempSt), lMoveTimetemp)
                                            nStation = nTempSt
                                            nTrnState = 1

                                            nLeftRightMove("右移", nTempSt, "出发")

                                        End If
                                    End If
                            End Select
                        Case 5
                            '由于发车检查发现为反向到达
                            Dim tmpnStartstation As Integer
                            tmpnStartstation = nStartstation(nTrnNum, nStation, nPhFx)
                            Select Case tmpnStartstation
                                Case 0
                                    '途中站
                                    nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                                    nTempSt = nBeginStation(nTrnNum, nTempSt)
                                    'MsgBox TrainInf(nTrnNum).Train + "在" + StationInf(nStation).sStationName + _
                                    '   "站反向出发", 0 + 16, "铺画运行线"
                                    nStation = nTempSt
                                    nTrnState = 3
                                Case 1
                                    '始发站
                                    'MsgBox TrainInf(nTrnNum).Train + "在始发站" + StationInf(nStation).sStationName + _
                                    '  "反向出发", 0 + 16, "铺画运行线"
                                    nTrnState = 1
                                Case 2
                                    '接入站(要)
                                    'MsgBox TrainInf(nTrnNum).Train + "在接入站" + StationInf(nStation).sStationName + _
                                    '  "反向出发", 0 + 16, "铺画运行线"
                                    nTrnState = 3
                                Case 3
                                    '平移站
                                    nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                                    nTempSt = nBeginStation(nTrnNum, nTempSt)
                                    'MsgBox(TrainInf(nTrnNum).Train + "在平移站" + StationInf(nStation).sStationName + _
                                    '    "反向出发", 0 + 16, "铺画运行线")
                                    nStation = nTempSt
                                    nTrnState = 3
                            End Select
                        Case 6
                            If TrainInf(nTrnNum).TrainClassCal = 12 Then
                                MsgBox(TrainInf(nTrnNum).Train & "在车底停留站" & StationInf(nStation).sStationName & _
                                    "股道数量不足，需修改车底折返方式", 0 + 16)
                                Exit Sub
                            ElseIf TrainInf(nTrnNum).TrainClassCal <> 12 Then
                                MsgBox(TrainInf(nTrnNum).Train & "在车底停留站" & StationInf(nStation).sStationName & _
                                    "股道数量不足，需修改车底折返方式", 0 + 16)
                                sChangeTrainReturn = "修改车底折返方式"
                                lArrivalTime = TimeMinus(lStartTime, nStartTchTime(nTrnNum))
                                'Exit Sub
                            End If
                    End Select
                Case 2
                    '通过处理（从nStation站通过nQStation站）
                    nLeaveSection = nFindSecNum(nStation, nQStation, nUporDown)
                    lArrivalTime = TimeAdd(lStartTime, TimeRun(nTrnNum, nStation, nQStation))
                    lStartTime = lArrivalTime
                    lStoporNot = StoporPass(nTrnNum, nQStation, lSDtime, lArrivalTime, nUporDown, 1)
                    nTempSt = nFindQstaNum(nTrnNum, nQStation, nUporDown)
                    nEnterSection = nFindSecNum(nQStation, nTempSt, nUporDown)
                    If nEnterSection = 0 Then
                        nEnterSection = nLeaveSection
                    End If
                    '江编2003.07.20
                    If nLeaveSection = nEnterSection Then
                        nEnterSection = EnterSecCheck(nLeaveSection, nEnterSection, nUporDown)
                    End If

                    If lStoporNot = 0 Then
                        '不需停车
                        Dim tmpnGuDaoPass As Integer
                        tmpnGuDaoPass = nGuDaoPass(lArrivalTime, nTrnNum, nLeaveSection, nQStation, nEnterSection)
                        Select Case tmpnGuDaoPass
                            Case 0
                                '能够通过，记录到发时刻
                                Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                nStation = nQStation
                                nTrnState = 2
                            Case 1
                                '与前后行均不够，不能通过，检查能否停车
                                nTempSt = nFindHstaNum(nTrnNum, nQStation, nUporDown)
                                nTempSt = nBeginStation(nTrnNum, nTempSt)
                                lArrivalTime = TimeAdd(lArrivalTime, TimeT(nTrnNum, nTempSt, nQStation))
                                lStartTime = TimeAdd(lArrivalTime, 60)
                                nStation = nTempSt
                                nTrnState = 3
                                StationLineNoOccupy(nTrnNum, nQStation)
                            Case 2
                                '与前行车通过间隔不够时，决定能否右移运行线
                                lMoveTimetemp = FindPassTime(2, nTrnNum, lArrivalTime, nBstation, _
                                                nUporDown, nLeaveSection, nEnterSection, nPhFx)
                                If lMoveTimetemp > 0 Then
                                    '能够右移
                                    Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                    nStopSt = FindSstaNum(nTrnNum, nQStation, nBstation, nUporDown, 1)
                                    HMoveLine(nTrnNum, nQStation, lMoveTimetemp, nStopSt, nUporDown, 1)
                                    lStartTime = TrainInf(nTrnNum).Starting(nQStation)
                                    lArrivalTime = TrainInf(nTrnNum).Arrival(nQStation)
                                    nStation = nQStation
                                    nTrnState = 2
                                ElseIf lMoveTimetemp = 0 Then
                                    '不能右移
                                    If lCanPaoDian(nTrnNum, nQStation) <> 0 Then
                                        lArrivalTime = lArrivalTime + lCanPaoDian(nTrnNum, nQStation)
                                        lStartTime = lArrivalTime
                                        Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                        nStation = nQStation
                                        nTrnState = 2
                                    Else
                                        nTempSt = nFindHstaNum(nTrnNum, nQStation, nUporDown)
                                        nTempSt = nBeginStation(nTrnNum, nTempSt)
                                        lArrivalTime = TimeAdd(lArrivalTime, TimeT(nTrnNum, nTempSt, nQStation))
                                        lStartTime = TimeAdd(lArrivalTime, 60)
                                        nStation = nTempSt
                                        nTrnState = 3
                                        StationLineNoOccupy(nTrnNum, nQStation)
                                    End If
                                ElseIf lMoveTimetemp < 0 Then
                                    '能够左移
                                    Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                    nStopSt = FindSstaNum(nTrnNum, nQStation, nBstation, nUporDown, 1)
                                    QMoveLine(nTrnNum, nQStation, lMoveTimetemp, nStopSt, nUporDown, 1)
                                    lStartTime = TrainInf(nTrnNum).Starting(nQStation)
                                    lArrivalTime = TrainInf(nTrnNum).Arrival(nQStation)
                                    nStation = nQStation
                                    nTrnState = 2
                                End If
                            Case 3
                                '与后行车通过间隔不够时，决定能否左移运行线
                                lMoveTimetemp = FindPassTime(3, nTrnNum, lArrivalTime, nBstation, _
                                                nUporDown, nLeaveSection, nEnterSection, nPhFx)
                                If lMoveTimetemp > 0 Then
                                    '能够左移
                                    Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                    nStopSt = FindSstaNum(nTrnNum, nQStation, nBstation, nUporDown, 1)
                                    QMoveLine(nTrnNum, nQStation, lMoveTimetemp, nStopSt, nUporDown, 1)
                                    lStartTime = TrainInf(nTrnNum).Starting(nQStation)
                                    lArrivalTime = TrainInf(nTrnNum).Arrival(nQStation)
                                    nStation = nQStation
                                    nTrnState = 2
                                Else
                                    '不能左移
                                    nTempSt = nFindHstaNum(nTrnNum, nQStation, nUporDown)
                                    nTempSt = nBeginStation(nTrnNum, nTempSt)
                                    lArrivalTime = TimeAdd(lArrivalTime, TimeT(nTrnNum, nTempSt, nQStation))
                                    lStartTime = TimeAdd(lArrivalTime, 60)
                                    nStation = nTempSt
                                    nTrnState = 3
                                    StationLineNoOccupy(nTrnNum, nQStation)
                                End If
                            Case 4
                                '正线被占用，检查该站能否停车
                                nTempSt = nFindHstaNum(nTrnNum, nQStation, nUporDown)
                                nTempSt = nBeginStation(nTrnNum, nTempSt)
                                lArrivalTime = TimeAdd(lArrivalTime, TimeT(nTrnNum, nTempSt, nQStation))
                                lStartTime = TimeAdd(lArrivalTime, 60)
                                nStation = nTempSt
                                nTrnState = 3
                                StationLineNoOccupy(nTrnNum, nQStation)
                        End Select
                        Select Case nEndStation(nTrnNum, nStation, nPhFx)
                            Case 1, 2
                                Exit Do
                            Case 0, 3
                        End Select
                    Else
                        '需要停车
                        nTempSt = nFindHstaNum(nTrnNum, nQStation, nUporDown)
                        nTempSt = nBeginStation(nTrnNum, nTempSt)
                        lArrivalTime = TimeAdd(lArrivalTime, TimeT(nTrnNum, nTempSt, nQStation))
                        lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                        nStation = nTempSt
                        nTrnState = 3
                    End If
                Case 3
                    '到达处理（从nStation站到达nQStation站）
                    If sChangeTrainReturn = "无" Then
                        nLeaveSection = nFindSecNum(nStation, nQStation, nUporDown)
                        nTempSt = nQStation
                        nStopSt = nTempSt
                        nTempSt = nFindQstaNum(nTrnNum, nTempSt, nUporDown)
                        nEnterSection = nFindSecNum(nQStation, nTempSt, nUporDown)
                        If nEnterSection = 0 Then nEnterSection = nLeaveSection

                        lArrivalTime = lArrivalTimeReSet(nTrnNum, lArrivalTime, nQStation)
                        lStartTime = lStartTimeReSet(nTrnNum, lStartTime, nQStation)
                    End If
                    Select Case nEndStation(nTrnNum, nQStation, nPhFx)
                        Case 1
                            '终到站
                            If sChangeTrainReturn = "无" Then
                                lStartTime = DealKeCheArrival(nTrnNum, nQStation, lArrivalTime)
                            End If
                        Case Else
                    End Select


                    '江编2003.07.20
                    If nLeaveSection = nEnterSection Then
                        nEnterSection = EnterSecCheck(nLeaveSection, nEnterSection, nUporDown)
                    End If


                    Dim tmpnGuDaoStop As Integer
                    tmpnGuDaoStop = nGuDaoStop(lArrivalTime, lStartTime, nTrnNum, nLeaveSection, nQStation, nEnterSection, nPhFx)
                    Select Case tmpnGuDaoStop
                        Case 0
                            '下站能够到达，记录到发时刻，形成发车点
                            nStation = nQStation
                            nTrnState = 1
                            Select Case nEndStation(nTrnNum, nStation, nPhFx)
                                Case 1  '终到站
                                    DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 1)
                                    Exit Do
                                Case 2
                                    DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 2)
                                    Exit Do
                                Case 3
                                    DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 3)
                                    Exit Do
                                Case Else
                                    If sChangeTrainReturn = "修改车底折返方式" Then
                                        sChangeTrainReturn = "无"
                                    End If
                                    Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                            End Select
                        Case 1
                            '前方站前后行均不够，不能停车,移到后一站
                            lMoveTimetemp = FindStopTime(1, nTrnNum, lArrivalTime, nBstation, _
                                            nUporDown, nLeaveSection, nEnterSection, nPhFx)
                            If TrainInf(nTrnNum).Arrival(nStation) = TrainInf(nTrnNum).Starting(nStation) Then
                                Dim tmpnStartstation As Integer
                                tmpnStartstation = nStartstation(nTrnNum, nStation, nPhFx)
                                Select Case tmpnStartstation
                                    Case 0, 3 '平移站、途中站
                                        nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                                        nTempSt = nBeginStation(nTrnNum, nTempSt)
                                        lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nTempSt, nStation))
                                        lStartTime = TimeAdd(lMoveTimetemp, 60)

                                        nLeftRightMove("右移", nStation, "出发")

                                        StationLineNoOccupy(nTrnNum, nStation)
                                        nStation = nTempSt
                                        nTrnState = 3
                                    Case 1 '始发站
                                        lStartTime = TimeAdd(lMoveTimetemp, 60)
                                        lArrivalTime = lStartTime
                                        nStation = nBstation
                                        nTrnState = 1

                                        nLeftRightMove("右移", nStation, "出发")

                                    Case 2 '接入站
                                        If nStartStop = 0 Then
                                            If MsgBox(TrainInf(nTrnNum).Train + "由于在" + StationInf(nQStation).sStationName + _
                                            "停车间隔时间不够，是否在接入站" + StationInf(nStation).sStationName + _
                                            "停车", 4 + 16, "铺画运行线") = vbNo Then
                                                nStartStop = 1
                                            End If
                                        End If
                                        If nStartStop = 1 Then
                                            lStartTime = TimeAdd(lMoveTimetemp, 60)
                                            lArrivalTime = lStartTime
                                            nStation = nBstation
                                            nTrnState = nBeginDeal(lArrivalTime, lStartTime, nStation, nTrnNum)
                                        Else
                                            lStartTime = TimeAdd(lMoveTimetemp, 60)
                                            lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nStation, nQStation))
                                            If TimeMinus(lStartTime, lArrivalTime) > TimeMinus(lArrivalTime, lStartTime) Then
                                                lStartTime = TimeAdd(lArrivalTime, 60)
                                            End If
                                            nStation = nBstation
                                            nTrnState = 1

                                            nLeftRightMove("右移", nStation, "出发")

                                        End If
                                End Select
                            Else
                                lArrivalTime = TrainInf(nTrnNum).Arrival(nStation)
                                lStartTime = TimeAdd(lMoveTimetemp, 60)
                                nTrnState = 1
                                nLeftRightMove("右移", nStation, "出发")
                            End If
                            StationLineNoOccupy(nTrnNum, nQStation)
                        Case 2
                            '与前行车到达间隔不够，决定能否右移运行线
                            lMoveTimetemp = FindStopTime(2, nTrnNum, lArrivalTime, nBstation, _
                                            nUporDown, nLeaveSection, nEnterSection, nPhFx)
                            If lMoveTimetemp < 86400 And lMoveTimetemp > 0 Then
                                '能够右移
                                Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                nStopSt = FindSstaNum(nTrnNum, nQStation, nBstation, nUporDown, 1)
                                HMoveLine(nTrnNum, nQStation, lMoveTimetemp, nStopSt, nUporDown, 1)
                                lArrivalTime = TrainInf(nTrnNum).Arrival(nQStation)

                                lStoporNot = lNeedStop(nTrnNum, nQStation, 4)
                                If lStoporNot = 0 Then lStoporNot = 60
                                If TimeMinus(lStartTime, lArrivalTime) > TimeMinus(lArrivalTime, lStartTime) Then
                                    lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                                Else
                                    If TimeMinus(lStartTime, lArrivalTime) < lStoporNot Then
                                        lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                                    End If
                                End If
                                nStation = nQStation
                                nTrnState = 1
                                lStoporNot = 0
                                Select Case nEndStation(nTrnNum, nStation, nPhFx)
                                    Case 1  '终到站
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 1)
                                        Exit Do
                                    Case 2
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 2)
                                        Exit Do
                                    Case 3
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 3)
                                        Exit Do
                                    Case Else
                                        If sChangeTrainReturn = "修改车底折返方式" Then
                                            sChangeTrainReturn = "无"
                                        End If
                                        Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                End Select
                            ElseIf lMoveTimetemp >= 86400 Then
                                '不能右移,移至后一站
                                If lCanPaoDian(nTrnNum, nQStation) <> 0 Then
                                    lArrivalTime = lArrivalTime + lCanPaoDian(nTrnNum, nQStation)
                                    Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)

                                    lStoporNot = lNeedStop(nTrnNum, nQStation, 4)
                                    If lStoporNot = 0 Then lStoporNot = 60
                                    If TimeMinus(lStartTime, lArrivalTime) > TimeMinus(lArrivalTime, lStartTime) Then
                                        lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                                    Else
                                        If TimeMinus(lStartTime, lArrivalTime) < lStoporNot Then
                                            lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                                        End If
                                    End If
                                    nStation = nQStation
                                    nTrnState = 1
                                    lStoporNot = 0
                                    Select Case nEndStation(nTrnNum, nStation, nPhFx)
                                        Case 1  '终到站
                                            DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 1)
                                            Exit Do
                                        Case 2
                                            DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 2)
                                            Exit Do
                                        Case 3
                                            DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 3)
                                            Exit Do
                                        Case Else
                                            If sChangeTrainReturn = "修改车底折返方式" Then
                                                sChangeTrainReturn = "无"
                                            End If
                                            Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                    End Select
                                Else
                                    lMoveTimetemp = lMoveTimetemp - 86400
                                    If TrainInf(nTrnNum).Arrival(nStation) = TrainInf(nTrnNum).Starting(nStation) Then
                                        Select Case nStartstation(nTrnNum, nStation, nPhFx)
                                            Case 0, 3 '平移站、途中站
                                                nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                                                nTempSt = nBeginStation(nTrnNum, nTempSt)
                                                lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nTempSt, nStation))
                                                lStartTime = TimeAdd(lArrivalTime, 60)

                                                ''''''''''''''''''''
                                                'lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                                'If TimeMinus(lStartTime, lArrivalTime) >= TimeMinus(lArrivalTime, lStartTime) Then
                                                '    lStartTime = TimeAdd(lArrivalTime, 60)
                                                'End If

                                                nLeftRightMove("右移", nStation, "出发")

                                                StationLineNoOccupy(nTrnNum, nStation)
                                                nStation = nTempSt
                                                nTrnState = 3
                                            Case 1 '始发站
                                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                                lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), lMoveTimetemp)
                                                nStation = nBstation
                                                nTrnState = 1

                                                nLeftRightMove("右移", nStation, "出发")

                                            Case 2 '接入站
                                                If MsgBox(TrainInf(nTrnNum).Train + "由于在" + StationInf(nQStation).sStationName + _
                                                    "停车间隔时间不够，是否在接入站" + StationInf(nStation).sStationName + _
                                                    "停车", 4 + 16, "铺画运行线") = vbNo Then
                                                    lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                                    lArrivalTime = lStartTime
                                                    nStation = nBstation
                                                    nTrnState = nBeginDeal(lArrivalTime, lStartTime, nStation, nTrnNum)
                                                Else
                                                    lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nStation, nQStation))
                                                    lStartTime = TimeAdd(lArrivalTime, 60)

                                                    ''''''''''''''''''''''''
                                                    'lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                                    'If TimeMinus(lStartTime, lArrivalTime) > TimeMinus(lArrivalTime, lStartTime) Then
                                                    '    lStartTime = TimeAdd(lArrivalTime, 60)
                                                    'End If

                                                    nStation = nBstation
                                                    nTrnState = 1
                                                    nLeftRightMove("右移", nStation, "出发")

                                                End If
                                        End Select
                                    Else
                                        lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                        lArrivalTime = TrainInf(nTrnNum).Arrival(nStation)
                                        nTrnState = 1

                                        nLeftRightMove("右移", nStation, "出发")

                                    End If
                                    StationLineNoOccupy(nTrnNum, nQStation)
                                End If
                            ElseIf lMoveTimetemp < 0 Then
                                '能够左移
                                Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                nStopSt = FindSstaNum(nTrnNum, nQStation, nBstation, nUporDown, 1)
                                QMoveLine(nTrnNum, nQStation, lMoveTimetemp, nStopSt, nUporDown, 1)
                                lArrivalTime = TrainInf(nTrnNum).Arrival(nQStation)
                                lStartTime = TimeAdd(lStartTime, -lMoveTimetemp)
                                lStoporNot = lNeedStop(nTrnNum, nQStation, 4)
                                If lStoporNot = 0 Then lStoporNot = 60
                                If TimeMinus(lStartTime, lArrivalTime) < lStoporNot Then
                                    lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                                End If
                                nStation = nQStation
                                nTrnState = 1
                                lStoporNot = 0
                                Select Case nEndStation(nTrnNum, nStation, nPhFx)
                                    Case 1  '终到站
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 1)
                                        Exit Do
                                    Case 2
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 2)
                                        Exit Do
                                    Case 3
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 3)
                                        Exit Do
                                    Case Else
                                        If sChangeTrainReturn = "修改车底折返方式" Then
                                            sChangeTrainReturn = "无"
                                        End If
                                        Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                End Select
                            End If
                        Case 3
                            '与后行车到达间隔不够，决定能否左移运行线
                            lMoveTimetemp = FindStopTime(3, nTrnNum, lArrivalTime, nBstation, _
                                            nUporDown, nLeaveSection, nEnterSection, nPhFx)
                            If lMoveTimetemp >= 0 Then
                                lMoveTimetemp = -lMoveTimetemp
                            End If
                            If lMoveTimetemp > 0 Then
                                '能够左移
                                Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                nStopSt = FindSstaNum(nTrnNum, nQStation, nBstation, nUporDown, 1)
                                QMoveLine(nTrnNum, nQStation, lMoveTimetemp, nStopSt, nUporDown, 1)
                                lArrivalTime = TrainInf(nTrnNum).Arrival(nQStation)
                                lStoporNot = lNeedStop(nTrnNum, nQStation, 4)
                                If lStoporNot = 0 Then lStoporNot = 60
                                If TimeMinus(lStartTime, lArrivalTime) < lStoporNot Then
                                    lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                                End If
                                nStation = nQStation
                                nTrnState = 1
                                lStoporNot = 0
                                Select Case nEndStation(nTrnNum, nStation, nPhFx)
                                    Case 1  '终到站
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 1)
                                        Exit Do
                                    Case 2
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 2)
                                        Exit Do
                                    Case 3
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 3)
                                        Exit Do
                                    Case Else
                                        If sChangeTrainReturn = "修改车底折返方式" Then
                                            sChangeTrainReturn = "无"
                                        End If
                                        Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                End Select
                            Else
                                '不能左移,移至后一站
                                If TrainInf(nTrnNum).Arrival(nStation) = TrainInf(nTrnNum).Starting(nStation) Then
                                    Select Case nStartstation(nTrnNum, nStation, nPhFx)
                                        Case 0, 3 '平移站、途中站
                                            nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                                            nTempSt = nBeginStation(nTrnNum, nTempSt)
                                            lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nTempSt, nStation))
                                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), -lMoveTimetemp)
                                            If TimeMinus(lStartTime, lArrivalTime) >= TimeMinus(lArrivalTime, lStartTime) Then
                                                lStartTime = TimeAdd(lArrivalTime, 60)
                                            End If

                                            nLeftRightMove("右移", nStation, "出发")

                                            StationLineNoOccupy(nTrnNum, nStation)
                                            nStation = nTempSt
                                            nTrnState = 3
                                        Case 1 '始发站
                                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), -lMoveTimetemp)
                                            lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), -lMoveTimetemp)
                                            nStation = nBstation
                                            nTrnState = 1

                                            nLeftRightMove("右移", nStation, "出发")

                                        Case 2 '接入站
                                            If MsgBox(TrainInf(nTrnNum).Train + "由于在" + StationInf(nQStation).sStationName + _
                                                "停车间隔时间不够，是否在接入站" + StationInf(nStation).sStationName + _
                                                "停车", 4 + 16, "铺画运行线") = vbNo Then
                                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), -lMoveTimetemp)
                                                lArrivalTime = lStartTime
                                                nStation = nBstation
                                                nTrnState = nBeginDeal(lArrivalTime, lStartTime, nStation, nTrnNum)
                                            Else
                                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), -lMoveTimetemp)
                                                lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nStation, nQStation))
                                                If TimeMinus(lStartTime, lArrivalTime) > TimeMinus(lArrivalTime, lStartTime) Then
                                                    lStartTime = TimeAdd(lArrivalTime, 60)
                                                End If
                                                nStation = nBstation
                                                nTrnState = 1

                                                nLeftRightMove("右移", nStation, "出发")

                                            End If
                                    End Select
                                Else
                                    lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), -lMoveTimetemp)
                                    lArrivalTime = TrainInf(nTrnNum).Arrival(nStation)
                                    nTrnState = 1

                                    nLeftRightMove("右移", nStation, "出发")

                                End If
                                StationLineNoOccupy(nTrnNum, nQStation)
                            End If
                        Case 4
                            '前方站股道数量不够，移至后一站
                            lMoveTimetemp = FindStopTime(4, nTrnNum, lArrivalTime, nBstation, _
                                            nUporDown, nLeaveSection, nEnterSection, nPhFx)

                            '待修改，20050622
                            '                    If lMoveTimetemp = 0 Then lMoveTimetemp = 30
                            '                      If lMoveTimetemp >= 80000 Then lMoveTimetemp = 30


                            If lPaoDian > lMoveTimetemp And Right(StationInf(nQStation).sStationProp, 3) <> "XLS" Then
                                lArrivalTime = lArrivalTime + lMoveTimetemp
                                Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)

                                lStoporNot = lNeedStop(nTrnNum, nQStation, 4)
                                If lStoporNot = 0 Then lStoporNot = 60
                                If TimeMinus(lStartTime, lArrivalTime) > TimeMinus(lArrivalTime, lStartTime) Then
                                    lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                                Else
                                    If TimeMinus(lStartTime, lArrivalTime) < lStoporNot Then
                                        lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                                    End If
                                End If
                                nStation = nQStation
                                nTrnState = 1
                                lStoporNot = 0
                                Select Case nEndStation(nTrnNum, nStation, nPhFx)
                                    Case 1  '终到站
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 1)
                                        Exit Do
                                    Case 2
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 2)
                                        Exit Do
                                    Case 3
                                        DealIfEndStation(nTrnNum, nStation, lArrivalTime, lStartTime, 3)
                                        Exit Do
                                    Case Else
                                        If sChangeTrainReturn = "修改车底折返方式" Then
                                            sChangeTrainReturn = "无"
                                        End If
                                        Record(nTrnNum, nQStation, lArrivalTime, lArrivalTime)
                                End Select

                            Else


                                If sChangeTrainReturn = "修改车底折返方式" Then
                                    sChangeTrainReturn = "无"
                                End If
                                If TrainInf(nTrnNum).Arrival(nStation) = TrainInf(nTrnNum).Starting(nStation) Then
                                    Select Case nStartstation(nTrnNum, nStation, nPhFx)
                                        Case 0, 3 '平移站、途中站
                                            nTempSt = nFindHstaNum(nTrnNum, nStation, nUporDown)
                                            nTempSt = nBeginStation(nTrnNum, nTempSt)
                                            lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nTempSt, nStation))
                                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                            If TimeMinus(lStartTime, lArrivalTime) >= TimeMinus(lArrivalTime, lStartTime) Then
                                                lStartTime = TimeAdd(lArrivalTime, 60)
                                            End If

                                            nLeftRightMove("右移", nStation, "出发")

                                            StationLineNoOccupy(nTrnNum, nStation)
                                            nStation = nTempSt
                                            nTrnState = 3
                                        Case 1 '始发站
                                            lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                            lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), lMoveTimetemp)
                                            nStation = nBstation
                                            nTrnState = 1

                                            nLeftRightMove("右移", nStation, "出发")

                                        Case 2 '接入站
                                            If MsgBox(TrainInf(nTrnNum).Train + "由于在" + StationInf(nQStation).sStationName + _
                                                "股道数量不够，是否在接入站" + StationInf(nStation).sStationName + _
                                                "停车", 4 + 16, "铺画运行线") = vbNo Then
                                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                                lArrivalTime = lStartTime
                                                nStation = nBstation
                                                nTrnState = nBeginDeal(lArrivalTime, lStartTime, nStation, nTrnNum)
                                            Else
                                                lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                                lArrivalTime = TimeAdd(TrainInf(nTrnNum).Arrival(nStation), TimeT(nTrnNum, nStation, nQStation))
                                                If TimeMinus(lStartTime, lArrivalTime) > TimeMinus(lArrivalTime, lStartTime) Then
                                                    lStartTime = TimeAdd(lArrivalTime, 60)
                                                End If
                                                nStation = nBstation
                                                nTrnState = 1

                                                nLeftRightMove("右移", nStation, "出发")

                                            End If
                                    End Select
                                Else
                                    lArrivalTime = TrainInf(nTrnNum).Arrival(nStation)
                                    lStartTime = TimeAdd(TrainInf(nTrnNum).Starting(nStation), lMoveTimetemp)
                                    nTrnState = 1
                                    nLeftRightMove("右移", nStation, "出发")
                                End If

                            End If
                        Case 5
                            If TrainInf(nTrnNum).TrainClassCal = 12 Then
                                MsgBox(TrainInf(nTrnNum).Train & "在车底停留站" & StationInf(nQStation).sStationName & _
                                    "股道数量不足，需修改车底折返方式", 0 + 16)
                                Exit Sub
                            ElseIf TrainInf(nTrnNum).TrainClassCal <> 12 Then
                                MsgBox(TrainInf(nTrnNum).Train & "在车底停留站" & StationInf(nQStation).sStationName & _
                                    "股道数量不足，需修改车底折返方式", 0 + 16)
                                sChangeTrainReturn = "修改车底折返方式"
                                lStartTime = TimeAdd(lArrivalTime, nStartTchTime(nTrnNum))
                                'Exit Sub
                            End If
                    End Select
            End Select
        Loop Until nStation = nEstation And TrainInf(nTrnNum).Starting(nStation) <> -1

        TrainInf(nTrnNum).lAllStartTime = TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1))
        TrainInf(nTrnNum).lAllEndTime = TrainInf(nTrnNum).Arrival(TrainInf(nTrnNum).nPathID(UBound(TrainInf(nTrnNum).nPathID)))
    End Sub

    '地铁图,无约束铺画
    Sub TDrawLineNoStrainInMetro(ByVal lArrivalTime As Long, ByVal lStartTime As Long, ByVal lSDtime As Long, ByVal nTrnNum As Integer, _
    ByVal nBeginSta As Integer, ByVal nEndSta As Integer, ByVal nTiaoorPu As Integer)
        'nTiaoorPu调整（1），铺画（0）

        Dim nUporDown As Integer, nTrnState As Integer
        Dim nStation As Integer, nQStation As Integer
        Dim lStoporNot As Long
        Dim nTempSt As Integer, nPhFx As Integer

        nBstation = nBeginSta
        nEstation = nEndSta
        nPhFx = 1
        '中间变量初试化
        nStation = nBstation
        nUporDown = nDirection(nTrnNum)
        Initial(nTrnNum, nBstation, nEstation)
        nTrnState = nBeginDeal(lArrivalTime, lStartTime, nStation, nTrnNum)
        Dim sGuDaoNum As String
        sGuDaoNum = ""
        Do
            With TrainInf(nTrnNum)
                nQStation = nFindQstaNum(nTrnNum, nStation, nUporDown)
                'If nStation <> nQStation Then
                '    nStation = nQStation - nSteplen(nTrnNum)
                'End If
                If nStation = 0 Then nStation = nQStation 'Exit Do
                Select Case nTrnState
                    Case 1
                        '出发处理（从nStation站发往nQStation站）
                        Select Case nStartstation(nTrnNum, nStation, nPhFx)
                            Case 1
                                '始发站处理
                                sGuDaoNum = ""
                                If TrainInf(nTrnNum).TrainReturnStyle(1) = "站前折返" Or TrainInf(nTrnNum).TrainReturnStyle(1) = "立即折返" Then
                                    If TrainInf(nTrnNum).TrainReturn(1) <> 0 Then
                                        sGuDaoNum = TrainInf(TrainInf(nTrnNum).TrainReturn(1)).StopLine(TrainInf(TrainInf(nTrnNum).TrainReturn(1)).nPathID(UBound(TrainInf(TrainInf(nTrnNum).TrainReturn(1)).nPathID)))
                                    End If
                                End If

                                lArrivalTime = DealTrainStart(nTrnNum, nStation, lStartTime)
                                RecordNoStrainInMetro(nTrnNum, nStation, lStartTime, lArrivalTime, sGuDaoNum)
                                'DealIfStartStationInMetro nTrnNum, nStation, lStartTime, lArrivalTime, 1
                                lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))

                            Case 2
                                '接入站处理
                                RecordNoStrainInMetro(nTrnNum, nStation, lStartTime, lArrivalTime, "")
                                If lArrivalTime <> lStartTime Then
                                    lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                End If
                            Case 3
                                '平移站处理
                                RecordNoStrainInMetro(nTrnNum, nStation, lStartTime, lArrivalTime, "")
                                If lStartTime <> lArrivalTime Then
                                    lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                                End If
                            Case Else
                                RecordNoStrainInMetro(nTrnNum, nStation, lStartTime, lArrivalTime, "")
                                lStartTime = TimeAdd(lStartTime, TimeQ(nTrnNum, nStation, nQStation))
                        End Select
                        If nEndStation(nTrnNum, nStation, nPhFx) <> 0 Then
                            '本条运行线的终点（终到站、交出站、平移站）
                            Exit Do
                        End If
                        nTrnState = 2
                    Case 2
                        '通过处理（从nStation站通过nQStation站）
                        lArrivalTime = TimeAdd(lStartTime, TimeRun(nTrnNum, nStation, nQStation))
                        lStartTime = lArrivalTime
                        'lStoporNot = StoporPass(nTrnNum, nQStation, lSDtime, lArrivalTime, nUporDown, 1)
                        lStoporNot = GetCurTrainStopTimeAtStation(TrainInf(nTrnNum).sJiaoLuName, TrainInf(nTrnNum).sStopSclaeName, StationInf(nQStation).sStationName) '
                        If lStoporNot = 0 Then
                            '不需停车
                            RecordNoStrainInMetro(nTrnNum, nQStation, lArrivalTime, lArrivalTime, "")
                            nStation = nQStation
                            nTrnState = 2
                            Select Case nEndStation(nTrnNum, nStation, nPhFx)
                                Case 1, 2
                                    Exit Do
                                Case 0, 3
                            End Select
                        Else
                            '需要停车
                            nTempSt = nFindHstaNum(nTrnNum, nQStation, nUporDown)
                            nTempSt = nBeginStation(nTrnNum, nTempSt)
                            lArrivalTime = TimeAdd(lArrivalTime, TimeT(nTrnNum, nTempSt, nQStation))
                            lStartTime = TimeAdd(lArrivalTime, lStoporNot)
                            nStation = nTempSt
                            nTrnState = 3
                        End If
                    Case 3
                        '到达处理（从nStation站到达nQStation站）
                        nStation = nQStation
                        nTrnState = 1
                        sGuDaoNum = ""
                        If TrainInf(nTrnNum).TrainReturnStyle(2) = "站前折返" Or TrainInf(nTrnNum).TrainReturnStyle(2) = "立即折返" Then
                            If TrainInf(nTrnNum).TrainReturn(2) <> 0 Then
                                sGuDaoNum = TrainInf(TrainInf(nTrnNum).TrainReturn(2)).StopLine(TrainInf(TrainInf(nTrnNum).TrainReturn(2)).nPathID(1))
                            End If
                        End If

                        If StationInf(nStation).sStationName = TrainInf(nTrnNum).NextStation Then '终到站
                            RecordNoStrainInMetro(nTrnNum, nQStation, TimeAdd(lArrivalTime, lEndTchTime(nTrnNum)), lArrivalTime, sGuDaoNum)
                        Else
                            RecordNoStrainInMetro(nTrnNum, nQStation, lArrivalTime, lArrivalTime, "")
                        End If

                End Select
            End With
        Loop Until nStation = nEstation

        TrainInf(nTrnNum).lAllStartTime = TrainInf(nTrnNum).Starting(TrainInf(nTrnNum).nPathID(1))
        TrainInf(nTrnNum).lAllEndTime = TrainInf(nTrnNum).Arrival(TrainInf(nTrnNum).nPathID(UBound(TrainInf(nTrnNum).nPathID)))

    End Sub



    Function DealTrainStart(ByVal nTrainNumbertemp As Integer, ByVal nStatemp As Integer, ByVal lTimetemp As Long) _
        As Long
        Dim nTempTrainNumber As Integer

        If TrainInf(nTrainNumbertemp).nChaRunDirection = 9999 Then
            nTempTrainNumber = nChaTrnNum(nTrainNumbertemp)
        Else
            nTempTrainNumber = nTrainNumbertemp
        End If

        Dim Zftime As Single
        '根据对应车次是否铺画，赋值（区分站前、站后）
        Zftime = nStartTchTime(nTempTrainNumber)
        With TrainInf(nTrainNumbertemp)
            '        If .TrainReturnStyle(1) = "站后折返" Then
            '                DealTrainStart = TimeMinus(lTimetemp, Zftime)
            '                .StopLine(nBstation) = "" 'TrainInf(.TrainReturn(1)).StopLine(nStatemp)
            '                Record nStatemp, nStatemp, lTimetemp, DealTrainStart
            '        ElseIf .TrainReturnStyle(1) = "站前折返" Then
            '            If TrainInf(.TrainContinue(1)).TrainPuorNot <> 0 Then
            '                DealTrainStart = TrainInf(.trainreturn(1)).Arrival(nStatemp)
            '                .StopLine(nBstation) = TrainInf(.trainreturn(1)).StopLine(nBstation)
            '                Record .trainreturn(1), nStatemp, lTimetemp, DealTrainStart
            '            Else
            '                DealTrainStart = TimeMinus(lTimetemp, Zftime)
            '                .StopLine(nBstation) = ""
            '            End If
            '        Else
            DealTrainStart = TimeMinus(lTimetemp, Zftime)
            If .TrainReturnStyle(1) = "站前折返" Or .TrainReturnStyle(1) = "立即折返" Then
                If .TrainReturn(1) > 0 Then
                    .StopLine(nBstation) = TrainInf(.TrainReturn(1)).StopLine(nBstation)
                End If
            End If
            Record(nTempTrainNumber, nStatemp, lTimetemp, DealTrainStart)
            '        End If
        End With

    End Function

    Function DealTrainEnd(ByVal nTrainNumbertemp As Integer, ByVal nStatemp As Integer, ByVal lTimetemp As Long) _
         As Long
        Dim nTempTrainNumber As Integer
        Dim Zftime As Single
        '根据对应车次是否铺画，赋值（区分站前、站后）
        Zftime = lEndTchTime(nTempTrainNumber)
        With TrainInf(nTrainNumbertemp)
            DealTrainEnd = TimeAdd(lTimetemp, Zftime)
            'If .TrainReturnStyle(2) = "站前折返" Or .TrainReturnStyle(2) = "立即折返" Then
            '    If .TrainReturn(2) > 0 Then
            '        .StopLine(nBstation) = TrainInf(.TrainReturn(2)).StopLine(nBstation)
            '    End If
            'End If
            Record(nTempTrainNumber, nStatemp, DealTrainEnd, lTimetemp)
        End With

    End Function
    Function nGuDaoStart(ByVal lATime As Long, ByVal lStime As Long, ByVal nTrnNum As Integer, _
    ByVal nLeaveSection As Integer, ByVal ntmpStation As Integer, ByVal nEnterSection As Integer, ByVal nPhFx As Integer) As Integer

        Dim sGdtemp As String
        Dim nUpDown As Integer, nGuDaoCheck As Integer
        Dim sGuDaotemp(1) As String ', nTemp As Integer
        Dim nStatemp As Integer

        nUpDown = nDirection(nTrnNum)
        nStatemp = ntmpStation
        sGdtemp = ""
        Dim tmpNstartStation As Integer
        tmpNstartStation = nStartstation(nTrnNum, nStatemp, nPhFx)
        Select Case tmpNstartStation
            Case 1
                '如果是已经铺画完了，复制；检查
                If TrainInf(nTrnNum).StopLine(nStatemp) = "" Then
                    ''''                If TrainInf(nTrnNum).TrainReturnStyle(1) = "站后折返" Then
                    ''''                    ReturnStartLineNum nTrnNum, ntmpStation, lATime, lStime
                    ''''                    'DealIfEndReturnStation nTrnNum, nStatemp, lATime, lStime, TrainInf(nTrnNum).StopLine(nStatemp)
                    ''''                    lATime = TrainInf(nTrnNum).Arrival(ntmpStation)
                    ''''                    lStime = TrainInf(nTrnNum).Starting(ntmpStation)
                    nGuDaoCheck = CheckStopLine(nTrnNum, lATime, lStime, nStatemp)
                    ''''                Else
                    ''''                    nGuDaoCheck = 1
                    ''''
                    ''''                End If
                    ''''                 'CheckStopLine(nTrnNum, lATime, lStime, nStatemp)
                    ''''            Else '没铺画，根据折返要求找股道
                    ''''                If TrainInf(nTrnNum).TrainReturnStyle(1) = "站后折返" Then
                    ''''''                    ReturnStartLineNum nTrnNum, ntmpStation, lATime, lStime
                    ''''''                    'DealIfEndReturnStation nTrnNum, nStatemp, lATime, lStime, TrainInf(nTrnNum).StopLine(nStatemp)
                    ''''''                    lATime = TrainInf(nTrnNum).Arrival(ntmpStation)
                    ''''''                    lStime = TrainInf(nTrnNum).Starting(ntmpStation)
                    ''''''                    nGuDaoCheck = CheckStopLine(nTrnNum, lATime, lStime, nStatemp)
                    ''''
                    ''''                Else
                    ''''                    sGdtemp = ""
                    ''''                    nGuDaoCheck = CheckStopLine(nTrnNum, lATime, lStime, nStatemp)
                    ''''                End If
                End If


            Case Else
                sGdtemp = TrainInf(nTrnNum).StopLine(nStatemp)
                If sGdtemp = "" Or sGdtemp = "9999" Then
                    nGuDaoCheck = StopLineNum(nTrnNum, nStatemp, lATime, lStime)
                Else
                    'nTemp = nLineIfCanStop(nTrnNum, nStatemp, sGdtemp, lATime, lStime)

                    nGuDaoCheck = StopLineNum(nTrnNum, nStatemp, lATime, lStime)
                    'If nTemp <> 0 Then
                    '    nGuDaoCheck = StopLineNum(nTrnNum, nStatemp, lATime, lStime)
                    'Else
                    '    nGuDaoCheck = nPanDuanTxFxZx(nFindLineNum(TrainInf(nTrnNum).StopLine(nStatemp), nStatemp), nTrnNum, nStatemp)
                    'End If
                End If
        End Select
        'nGuDaoCheck = 0
        Select Case nGuDaoCheck
            Case 0
                '正线出发
                nGuDaoStart = CheckStart(nTrnNum, lStime, nUpDown, nLeaveSection, nStatemp, nEnterSection)
            Case 1
                '同向股道出发
                nGuDaoStart = CheckStart(nTrnNum, lStime, nUpDown, nLeaveSection, nStatemp, nEnterSection)
            Case 2
                '反向股道出发
                If sGdtemp <> TrainInf(nTrnNum).StopLine(nStatemp) Then
                    '反向到达未检查过
                    If StationInf(nStatemp).sStationName = TrainInf(nTrnNum).ComeStation Then
                        nGuDaoStart = CheckStart(nTrnNum, lStime, nUpDown, nLeaveSection, nStatemp, nEnterSection)
                    Else
                        nGuDaoStart = 5
                    End If
                Else
                    '反向到达已检查过
                    nGuDaoStart = CheckStart(nTrnNum, lStime, nUpDown, nLeaveSection, nStatemp, nEnterSection)
                End If
            Case 3
                '股道数量不符合要求
                nGuDaoStart = 4
                '  nGuDaoStart = CheckStart(nTrnNum, lStime, nUpDown, nLeaveSection, nStatemp, nEnterSection)

            Case 4
                '正线出发
                nGuDaoStart = CheckStart(nTrnNum, lStime, nUpDown, nLeaveSection, nStatemp, nEnterSection)
            Case 5
                '停留折返股道数量不符合要求
                nGuDaoStart = 6
        End Select
    End Function

    Sub DealIfStartStation(ByVal ntmpTrainNumber As Integer, ByVal ntmpStation As Integer, _
    ByVal ltmpStartTime As Long, ByVal ltmpArrivalTime As Long, ByVal ntmpStartStationKind As Integer)
        Select Case ntmpStartStationKind
            Case 1
                With TrainInf(ntmpTrainNumber)

                    '                If .TrainReturnStyle(1) = "站前折返" Then
                    '                    If TrainInf(.trainreturn(1)).TrainPuorNot <> 0 Then
                    '                        Record ntmpTrainNumber, ntmpStation, ltmpStartTime, TrainInf(.trainreturn(1)).Arrival(ntmpStation)
                    '                        Record .trainreturn(1), ntmpStation, ltmpStartTime, TrainInf(.trainreturn(1)).Arrival(ntmpStation)
                    '                    End If
                    '                ElseIf .TrainReturnStyle(1) = "站后折返" Then
                    '                    If TrainInf(.trainreturn(1)).TrainPuorNot <> 0 Then
                    '                        sZytime = nStartTchTime(ntmpTrainNumber)
                    '                        sTime = TimeMinus(ltmpStartTime, sZytime)
                    '
                    '                        Record ntmpTrainNumber, ntmpStation, ltmpStartTime, sTime 'TrainInf(.TrainReturn(1)).sEndZFStarting
                    '                        'RecordReturnEnd .TrainReturn(1), TrainInf(.TrainReturn(1)).sEndZFStarting, TrainInf(.TrainReturn(1)).sEndZFArrival, TrainInf(.TrainReturn(1)).sEndZFLine
                    '                        'RecordReturnStart ntmpTrainNumber, TrainInf(.TrainReturn(1)).sEndZFStarting, TrainInf(.TrainReturn(1)).sEndZFArrival, TrainInf(.TrainReturn(1)).sEndZFLine
                    '                        'RecordReturnEnd .TrainReturn(1), TrainInf(.TrainReturn(1)).sEndZFStarting, TrainInf(.TrainReturn(1)).sEndZFArrival, TrainInf(.TrainReturn(1)).sEndZFLine
                    '                    Else
                    '                        Record ntmpTrainNumber, ntmpStation, ltmpStartTime, TrainInf(ntmpTrainNumber).Starting(ntmpStation)
                    '                    End If
                    '                Else
                    Dim Zftime As Integer
                    Zftime = nStartTchTime(ntmpTrainNumber)
                    ltmpArrivalTime = TimeMinus(ltmpStartTime, Zftime)
                    Record(ntmpTrainNumber, ntmpStation, ltmpStartTime, ltmpArrivalTime)
                    '                End If


                End With
                ''        Case 2
                ''            Record ntmpTrainNumber, ntmpStation, ltmpStartTime, ltmpArrivalTime
                ''        Case 3
                ''            Record ntmpTrainNumber, ntmpStation, ltmpStartTime, TrainInf(ntmpTrainNumber).Starting(ntmpStation)
        End Select

    End Sub

    Sub nLeftRightMove(ByVal nLeftRight As String, ByVal nStationNumber As Integer, ByVal nRunKind As String)
        Dim i As Integer
        Dim nBegintemp As Integer, nEndtemp As Integer

        nBegintemp = nBeginDataStation(nNowDataReadLineNum)
        nEndtemp = nEndDataStation(nNowDataReadLineNum)
        Select Case nLeftRight
            Case "不移"
                Select Case nRunKind
                    Case "出发"

                    Case "通过"
                        nCanLeftMove(nStationNumber) = 0
                        nCanRightMove(nStationNumber) = 0
                    Case "到达"
                        nCanLeftMove(nStationNumber) = 0
                        nCanRightMove(nStationNumber) = 0
                End Select

            Case "左移"
                Select Case nRunKind
                    Case "出发"
                        For i = nBegintemp To nEndtemp
                            If StationInf(i).sStationName = StationInf(nStationNumber).sStationName Then
                                nCanRightMove(i) = 1
                                nCanLeftMove(i) = 0
                            End If
                        Next i
                    Case "通过"
                        nCanLeftMove(nStationNumber) = 0
                        nCanRightMove(nStationNumber) = 0
                    Case "到达"
                        nCanLeftMove(nStationNumber) = 0
                        nCanRightMove(nStationNumber) = 0
                End Select
            Case "右移"
                Select Case nRunKind
                    Case "出发"
                        For i = nBegintemp To nEndtemp
                            If StationInf(i).sStationName = StationInf(nStationNumber).sStationName Then
                                nCanLeftMove(i) = 1
                                nCanRightMove(i) = 0
                            End If
                        Next i
                    Case "通过"
                        nCanLeftMove(nStationNumber) = 0
                        nCanRightMove(nStationNumber) = 0
                    Case "到达"
                        nCanLeftMove(nStationNumber) = 0
                        nCanRightMove(nStationNumber) = 0
                End Select
        End Select
    End Sub
    Function FindStartTime(ByVal nConflict As Integer, ByVal nTrnNum As Integer, ByVal lTime As Long, _
    ByVal nUporDown As Integer, ByVal nLeaveSection As Integer, ByVal nEnterSection As Integer, _
    ByVal nPhFx As Integer) As Long
        'nTrnNum 本次列车编号，nStatemp当前车站，nUporDown上下行
        'nConflict交叉的种类，nPhFx铺画方式
        '寻找可移动的出发时间
        Dim nUporDowntemp As Integer, nIntertemp As Integer, nNumtemp As Integer, nNumtemp1 As Integer
        Dim nTxNumtemp As Integer, nFxNumtemp As Integer
        Dim ii As Integer
        Dim lDifftemp As Long, lTimetemp As Long
        Dim lTxMovetemp As Long, lFxMovetemp As Long
        Dim nStatemp As Integer

        Select Case nPhFx
            Case 1
                If nUporDown = 1 Then
                    nUporDowntemp = 2
                    nStatemp = SectionInf(nEnterSection).nHStation
                Else
                    nUporDowntemp = 1
                    nStatemp = SectionInf(nEnterSection).nQStation
                End If
                Select Case nConflict
                    Case 1
                        '前后都不够，只考虑右移至后行车后
                        If TxIntervalKind1(nTrnNum, nStatemp) >= 300 Then
                            FindStartTime = TxDiffTime2(nTrnNum, nStatemp)
                        Else
                            If TxDiffTime2(nTrnNum, nStatemp) < FxDiffTime2(nTrnNum, nStatemp) Then
                                lDifftemp = FxDiffTime2(nTrnNum, nStatemp)
                                nIntertemp = FxIntervalKind2(nTrnNum, nStatemp)
                                nNumtemp = FxDiffTrain2(nTrnNum, nStatemp)
                            Else
                                lDifftemp = TxDiffTime2(nTrnNum, nStatemp)
                                nIntertemp = TxIntervalKind2(nTrnNum, nStatemp)
                                nNumtemp = TxDiffTrain2(nTrnNum, nStatemp)
                            End If
                            FindStartTime = lMoveStartTime(nTrnNum, nNumtemp, nIntertemp, _
                                lDifftemp, nConflict, nUporDown, nLeaveSection, nEnterSection)
                        End If
                    Case 2
                        '前不够，既考虑右移一段时间，又考虑右移至后行车后
                        If TxDiffTime1(nTrnNum, nStatemp) < FxDiffTime1(nTrnNum, nStatemp) Then
                            lDifftemp = FxDiffTime1(nTrnNum, nStatemp)
                            nIntertemp = FxIntervalKind1(nTrnNum, nStatemp)
                            nNumtemp = FxDiffTrain1(nTrnNum, nStatemp)
                        Else
                            lDifftemp = TxDiffTime1(nTrnNum, nStatemp)
                            nIntertemp = TxIntervalKind1(nTrnNum, nStatemp)
                            nNumtemp = TxDiffTrain1(nTrnNum, nStatemp)
                        End If
                        '考虑右移一段时间
                        FindStartTime = lCanRightMoveStart(lDifftemp, nTrnNum, nStatemp, 0, lTime, nPhFx)
                        If FindStartTime = 0 Then
                            If TxDiffTime2(nTrnNum, nStatemp) < FxDiffTime2(nTrnNum, nStatemp) Then
                                lDifftemp = FxDiffTime2(nTrnNum, nStatemp)
                                nIntertemp = FxIntervalKind2(nTrnNum, nStatemp)
                                nNumtemp = FxDiffTrain2(nTrnNum, nStatemp)
                            Else
                                lDifftemp = TxDiffTime2(nTrnNum, nStatemp)
                                nIntertemp = TxIntervalKind2(nTrnNum, nStatemp)
                                nNumtemp = TxDiffTrain2(nTrnNum, nStatemp)
                            End If
                            FindStartTime = lMoveStartTime(nTrnNum, nNumtemp, nIntertemp, _
                                lDifftemp, nConflict, nUporDown, nLeaveSection, nEnterSection)
                        End If
                    Case 3
                        '后不够，既考虑左移一段时间，又考虑右移至后行车后
                        If TxDiffTime2(nTrnNum, nStatemp) < FxDiffTime2(nTrnNum, nStatemp) Then
                            lDifftemp = FxDiffTime2(nTrnNum, nStatemp)
                            nIntertemp = FxIntervalKind2(nTrnNum, nStatemp)
                            nNumtemp = FxDiffTrain2(nTrnNum, nStatemp)
                        Else
                            lDifftemp = TxDiffTime2(nTrnNum, nStatemp)
                            nIntertemp = TxIntervalKind2(nTrnNum, nStatemp)
                            nNumtemp = TxDiffTrain2(nTrnNum, nStatemp)
                            nUporDowntemp = nUporDown
                        End If
                        If FindStartTime = 0 Then
                            FindStartTime = lMoveStartTime(nTrnNum, nNumtemp, nIntertemp, _
                                lDifftemp, nConflict, nUporDown, nLeaveSection, nEnterSection)
                        End If
                    Case 4
                        nNumtemp = 0
                        nNumtemp1 = 0
                        nFxNumtemp = nTrnNum
                        nTxNumtemp = nTrnNum
                        lTxMovetemp = lTime
                        lFxMovetemp = lTime
                        lTimetemp = lTime

                        Do While nNumtemp = 0 Or nNumtemp1 = 0
                            If nNumtemp = 0 Then
                                nTxNumtemp = AFTER(nTxNumtemp, lTxMovetemp, nStatemp, nUporDown, 1, 1, _
                                            "", "", "")
                                If nTxNumtemp <> nTrnNum Then

                                    If TrainInf(nTxNumtemp).Arrival(nStatemp) = TrainInf(nTxNumtemp).Starting(nStatemp) Then
                                        lTxMovetemp = TrainInf(nTxNumtemp).Arrival(nStatemp)
                                        If TimeMinus(lTxMovetemp, lTimetemp) >= TimeMinus(lFxMovetemp, lTimetemp) And nNumtemp1 <> 0 Then
                                            nNumtemp = nNumtemp1
                                            lTxMovetemp = lFxMovetemp
                                        Else
                                            nNumtemp = 0
                                        End If
                                    Else
                                        lTxMovetemp = TrainInf(nTxNumtemp).Starting(nStatemp)
                                        nNumtemp = nTxNumtemp
                                    End If

                                Else
                                    nNumtemp = nTrnNum

                                End If
                            End If


                            nNumtemp1 = 1


                            If nNumtemp1 = 0 Then
                                nFxNumtemp = AFTER(nFxNumtemp, lFxMovetemp, nStatemp, nUporDowntemp, 1, 1, _
                                            "", "", "")

                                If nFxNumtemp <> nTrnNum Then

                                    If TrainInf(nFxNumtemp).Arrival(nStatemp) = TrainInf(nFxNumtemp).Starting(nStatemp) Then
                                        lFxMovetemp = TrainInf(nFxNumtemp).Arrival(nStatemp)
                                        If TimeMinus(lFxMovetemp, lTimetemp) >= TimeMinus(lTxMovetemp, lTimetemp) And nNumtemp <> 0 Then
                                            nNumtemp1 = nNumtemp
                                            lFxMovetemp = lTxMovetemp
                                        Else
                                            nNumtemp1 = 0
                                        End If
                                    Else
                                        lFxMovetemp = TrainInf(nFxNumtemp).Starting(nStatemp)
                                        nNumtemp1 = nFxNumtemp
                                    End If

                                Else
                                    nNumtemp1 = nTrnNum
                                End If

                            End If
                        Loop
                        ''                    If TimeMinus(lTxMovetemp, lTimetemp) >= TimeMinus(lFxMovetemp, lTimetemp) Then
                        FindStartTime = TimeMinus(lFxMovetemp, lTime) + nMoveStepTime
                        ''                    Else
                        'FindStartTime = TimeMinus(lTxMovetemp, lTime) + nMoveStepTime
                        ''                    End If
                        ''                    FindStartTime = 30
                End Select
            Case 2
                If nUporDown = 1 Then
                    nUporDowntemp = 2
                    If nLeaveSection = nEnterSection Then
                        If StationInf(SectionInf(nEnterSection).nQStation).sStationName = TrainInf(nTrnNum).NextStation Then
                            nStatemp = SectionInf(nEnterSection).nQStation
                        Else
                            nStatemp = SectionInf(nEnterSection).nHStation
                        End If
                    Else
                        nStatemp = SectionInf(nEnterSection).nHStation
                    End If
                Else
                    nUporDowntemp = 1
                    If nLeaveSection = nEnterSection Then
                        If StationInf(SectionInf(nEnterSection).nHStation).sStationName = TrainInf(nTrnNum).NextStation Then
                            nStatemp = SectionInf(nEnterSection).nHStation
                        Else
                            nStatemp = SectionInf(nEnterSection).nQStation
                        End If
                    Else
                        nStatemp = SectionInf(nEnterSection).nQStation
                    End If
                End If
                Select Case nConflict
                    Case 1
                        '前后都不够，只考虑右移至后行车后
                        If TxIntervalKind1(nTrnNum, nStatemp) >= 300 Then
                            FindStartTime = TxDiffTime2(nTrnNum, nStatemp)
                        Else
                            FindStartTime = TimeMinus(lQConflictFa(nTrnNum, nStatemp), 30)
                        End If
                    Case 2
                        '前不够，既考虑右移一段时间，又考虑右移至后行车后
                        If TxDiffTime1(nTrnNum, nStatemp) < FxDiffTime1(nTrnNum, nStatemp) Then
                            lDifftemp = FxDiffTime1(nTrnNum, nStatemp)
                            nIntertemp = FxIntervalKind1(nTrnNum, nStatemp)
                            nNumtemp = FxDiffTrain1(nTrnNum, nStatemp)
                        Else
                            lDifftemp = TxDiffTime1(nTrnNum, nStatemp)
                            nIntertemp = TxIntervalKind1(nTrnNum, nStatemp)
                            nNumtemp = TxDiffTrain1(nTrnNum, nStatemp)
                        End If
                        '考虑右移一段时间
                        'nTempSt = FindSstaNum(nTrnNum, nStatemp, nBstation, nUporDown, nPhFx)
                        'FindStartTime = lCanRightMoveStart(lDifftemp, nTrnNum, nStatemp, nTempSt, lTime, nPhFx)

                        If FindStartTime = 0 Then
                            FindStartTime = TimeMinus(lQConflictFa(nTrnNum, nStatemp), 30)
                            FindStartTime = TimeMinus(lTime, FindStartTime)
                        Else
                            FindStartTime = -FindStartTime
                        End If
                    Case 3
                        '后不够，既考虑左移一段时间，又考虑右移至后行车后
                        If TxDiffTime2(nTrnNum, nStatemp) < FxDiffTime2(nTrnNum, nStatemp) Then
                            lDifftemp = FxDiffTime2(nTrnNum, nStatemp)
                            nIntertemp = FxIntervalKind2(nTrnNum, nStatemp)
                            nNumtemp = FxDiffTrain2(nTrnNum, nStatemp)
                        Else
                            lDifftemp = TxDiffTime2(nTrnNum, nStatemp)
                            nIntertemp = TxIntervalKind2(nTrnNum, nStatemp)
                            nNumtemp = TxDiffTrain2(nTrnNum, nStatemp)
                            nUporDowntemp = nUporDown
                        End If
                        '考虑左移一段时间
                        If nIntertemp = 203 Or nIntertemp = 204 Or nIntertemp = 205 Or nIntertemp = 206 Then
                            lDifftemp = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, 2, _
                                        nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                            ii = 206
                            lDifftemp = lIntervalTime(nNumtemp, nTrnNum, ii, 2, _
                                        nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                        End If
                        'nTempSt = FindSstaNum(nTrnNum, nStatemp, nBstation, nUporDown, nPhFx)
                        'FindStartTime = lCanLeftMoveStart(lDifftemp, nTrnNum, nStatemp, nTempSt, lTime, nPhFx)
                        '当左移一段时间不够时 , 考虑左移至前行车前
                        If FindStartTime = 0 Then
                            FindStartTime = TimeMinus(lQConflictFa(nTrnNum, nStatemp), 30)
                            FindStartTime = TimeMinus(lTime, FindStartTime)
                        Else
                            FindStartTime = 86400 + FindStartTime
                        End If
                    Case 4
                        nNumtemp = 0
                        nNumtemp1 = 0
                        nFxNumtemp = nTrnNum
                        nTxNumtemp = nTrnNum
                        lTxMovetemp = lTime
                        lFxMovetemp = lTime
                        lTimetemp = lTime

                        Do While nNumtemp = 0 Or nNumtemp1 = 0
                            If nNumtemp = 0 Then
                                nTxNumtemp = BEFORE(nTxNumtemp, lTxMovetemp, nStatemp, nUporDown, 0, 1, _
                                            "", "", "")
                                If nTxNumtemp <> nTrnNum Then
                                    If TrainInf(nTxNumtemp).Arrival(nStatemp) = TrainInf(nTxNumtemp).Starting(nStatemp) Then
                                        lTxMovetemp = TrainInf(nTxNumtemp).Arrival(nStatemp)
                                        If TimeMinus(lTimetemp, lTxMovetemp) >= TimeMinus(lTimetemp, lFxMovetemp) And nNumtemp1 <> 0 Then
                                            nNumtemp = nNumtemp1
                                            lTxMovetemp = lFxMovetemp
                                        Else
                                            nNumtemp = 0
                                        End If
                                    Else
                                        lTxMovetemp = TrainInf(nTxNumtemp).Arrival(nStatemp)
                                        nNumtemp = nTxNumtemp
                                    End If
                                Else
                                    nNumtemp = nTxNumtemp
                                End If
                            End If
                            If nNumtemp1 = 0 Then
                                nFxNumtemp = BEFORE(nFxNumtemp, lFxMovetemp, nStatemp, nUporDowntemp, 0, 1, _
                                            "", "", "")
                                If nFxNumtemp <> nTrnNum Then
                                    If TrainInf(nFxNumtemp).Arrival(nStatemp) = TrainInf(nFxNumtemp).Starting(nStatemp) Then
                                        lFxMovetemp = TrainInf(nFxNumtemp).Arrival(nStatemp)
                                        If TimeMinus(lTimetemp, lFxMovetemp) >= TimeMinus(lTimetemp, lTxMovetemp) And nNumtemp <> 0 Then
                                            nNumtemp1 = nNumtemp
                                            lFxMovetemp = lTxMovetemp
                                        Else
                                            nNumtemp1 = 0
                                        End If
                                    Else
                                        lFxMovetemp = TrainInf(nFxNumtemp).Arrival(nStatemp)
                                        nNumtemp1 = nFxNumtemp
                                    End If
                                Else
                                    nNumtemp1 = nFxNumtemp
                                End If
                            End If
                        Loop
                        If lTxMovetemp <> lTimetemp And lFxMovetemp <> lTimetemp Then
                            If TimeMinus(lTimetemp, lTxMovetemp) >= TimeMinus(lTimetemp, lFxMovetemp) Then
                                FindStartTime = TimeMinus(lTime, lFxMovetemp) + nMoveStepTime
                            Else
                                FindStartTime = TimeMinus(lTime, lTxMovetemp) + nMoveStepTime
                            End If
                        ElseIf lTxMovetemp = lTimetemp And lFxMovetemp <> lTimetemp Then
                            FindStartTime = TimeMinus(lTime, lFxMovetemp) + nMoveStepTime
                        ElseIf lTxMovetemp <> lTimetemp And lFxMovetemp = lTimetemp Then
                            FindStartTime = TimeMinus(lTime, lTxMovetemp) + nMoveStepTime
                        End If
                End Select
        End Select
    End Function

    Sub StationLineNoOccupy(ByVal nTrainNumber As Integer, ByVal nStationNumber As Integer)
        Dim i As Integer
        For i = 1 To UBound(StationInf)
            If StationInf(i).sStationName = StationInf(nStationNumber).sStationName Then
                TrainInf(nTrainNumber).StopLine(i) = ""
            End If
        Next i
    End Sub

    '始发站作业时间
    Function nStartTchTime(ByVal nTrnNum As Integer) As Long
        nStartTchTime = GetCurTrainStopTimeAtStation(TrainInf(nTrnNum).sJiaoLuName, TrainInf(nTrnNum).sStopSclaeName, TrainInf(nTrnNum).ComeStation)
    End Function

    '终到站作业时间
    Function lEndTchTime(ByVal nTrnNum As Integer) As Long
        lEndTchTime = GetCurTrainStopTimeAtStation(TrainInf(nTrnNum).sJiaoLuName, TrainInf(nTrnNum).sStopSclaeName, TrainInf(nTrnNum).NextStation)
    End Function

    Function TimeActualRun(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal nQstatemp As Integer) As Long
        If TrainInf(nTrnNum).Arrival(nQstatemp) = -1 Then
            TimeActualRun = 0
        Else
            TimeActualRun = TimeMinus(TrainInf(nTrnNum).Arrival(nQstatemp), TrainInf(nTrnNum).Starting(nStatemp))
        End If
    End Function

    Function TimeRun(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal nQstatemp As Integer) As Long
        TimeRun = 0
        'TimeRun = TimeRunByBiaoChiTrain(nTrnNum, nStatemp, nQstatemp)

        Dim nTemp As Integer
        Dim nUpDowntemp As Integer
        nUpDowntemp = nDirection(nTrnNum)
        nTemp = nFindSecNum(nStatemp, nQstatemp, nUpDowntemp)
        If nTemp = 0 Then
            TimeRun = 0
            Exit Function
        End If
        Dim sJiaoLuName As String
        sJiaoLuName = TrainInf(nTrnNum).sJiaoLuName
        Dim sRunScaleName As String
        sRunScaleName = TrainInf(nTrnNum).sRunScaleName
        Dim sSecName As String
        sSecName = SectionInf(nTemp).sSecName

        TimeRun = TimeRunByBiaoChiScale(sJiaoLuName, sRunScaleName, sSecName)

        'Dim nTemp As Integer
        'Dim nUpDowntemp As Integer

        'nUpDowntemp = nDirection(nTrnNum)
        'nTemp = nFindSecNum(nStatemp, nQstatemp, nUpDowntemp)
        'With TrainInf(nTrnNum)
        '    If nTemp <> 0 Then
        '        If nUpDowntemp = 1 Then
        '            If .TrainKind = "客车" Then
        '                TimeRun = SectionInf(nTemp).KeDownRunTime(.nTrainTimeKind)
        '            Else
        '                If .TrainClass = 38 Then
        '                    TimeRun = SectionInf(nTemp).KeDownRunTime(.nTrainTimeKind)
        '                Else
        '                    TimeRun = SectionInf(nTemp).HuoDownRunTime(.nTrainTimeKind)
        '                End If
        '            End If
        '        Else
        '            If .TrainKind = "客车" Then
        '                TimeRun = SectionInf(nTemp).KeUpRunTime(.nTrainTimeKind)
        '            Else
        '                If .TrainClass = 38 Then
        '                    TimeRun = SectionInf(nTemp).KeUpRunTime(.nTrainTimeKind)
        '                Else
        '                    TimeRun = SectionInf(nTemp).HuoUpRunTime(.nTrainTimeKind)
        '                End If
        '            End If
        '        End If
        '        TimeRun = TimeRun + .lChaRunTime(nTemp)
        '    End If
        'End With
    End Function

    Function StoporPass(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal lSDtime As Long, _
    ByVal lTimetemp As Long, ByVal nUporDown As Integer, ByVal nPhFx As Integer) As Long
        'nStatemp当前车站，nEstation铺画的终点站，lTimetemp当前时间，lSDtime需撒点时间
        '确定车站是否需要停车

        Dim nTemp As Integer
        Dim lStime As Long

        StoporPass = 0
        Select Case nPhFx
            Case 1
                Select Case nEndStation(nTrnNum, nStatemp, nPhFx)
                    Case 1 '本站是终到站
                        StoporPass = lNeedStop(nTrnNum, nStatemp, 1)
                    Case 2 '本站是交出站
                        StoporPass = lNeedStop(nTrnNum, nStatemp, 2)
                    Case 3 '本站是平移站
                        StoporPass = lNeedStop(nTrnNum, nStatemp, 4)
                    Case Else
                        StoporPass = lNeedStop(nTrnNum, nStatemp, 4)
                        nTemp = nFindSectionNumber(nStatemp, nEstation, nUporDown)
                        If nTemp <> 0 Then
                            If lSDtime > 0 Then
                                '需要撒点
                                If TrainInf(nTrnNum).NextStation = StationInf(nEstation).sStationName Then
                                    '该列车终到、交出站不是平移运行线的终到站，且在本区段终到
                                    If TrainInf(nTrnNum).TrainClassCal >= 21 Then
                                        '本列车是货车
                                        lStime = lCalculStopTime(nTrnNum, nBstation, nStatemp)
                                        If StoporPass < (lSDtime - lStime) Then
                                            StoporPass = lSDtime - lStime
                                        End If
                                    End If
                                End If
                            End If
                        End If
                End Select
            Case 2
                Select Case nFanStartStation(nTrnNum, nStatemp)
                    Case 1 '本站是终到站
                        StoporPass = lNeedStop(nTrnNum, nStatemp, 1)
                    Case 2 '本站是交出站
                        StoporPass = lNeedStop(nTrnNum, nStatemp, 2)
                    Case 3 '本站是平移站
                        StoporPass = lNeedStop(nTrnNum, nStatemp, 4)
                    Case Else
                        StoporPass = lNeedStop(nTrnNum, nStatemp, 4)
                        nTemp = nFindSectionNumber(nStatemp, nEstation, nUporDown)
                        If nTemp <> 0 Then
                            If lSDtime > 0 Then
                                '需要撒点
                                If TrainInf(nTrnNum).NextStation = StationInf(nEstation).sStationName Then
                                    '该列车终到、交出站不是平移运行线的终到站，且在本区段终到
                                    If TrainInf(nTrnNum).TrainClassCal >= 21 Then
                                        '本列车是货车
                                        lStime = lCalculStopTime(nTrnNum, nBstation, nStatemp)
                                        If StoporPass < (lSDtime - lStime) Then
                                            StoporPass = lSDtime - lStime
                                        End If
                                    End If
                                End If
                            End If
                        End If
                End Select
        End Select
    End Function


    Public Function EnterSecCheck(ByVal nLevSec As Integer, ByVal nEntSec As Integer, ByVal nUpDown As Integer) As Integer
        Dim nSecNum As Integer
        If nLevSec = nEntSec Then
            If nUpDown = 2 Then
                If Left(StationInf(SectionInf(nEntSec).nHStation).sStationProp, 1) = "F" Then
                    EnterSecCheck = nEntSec
                    Exit Function
                End If
                nSecNum = SeekSecNum(SectionInf(nEntSec).nHStation, nUpDown)
                If nSecNum > 0 Then
                    EnterSecCheck = nSecNum
                Else
                    EnterSecCheck = nEntSec
                End If
            Else
                If Left(StationInf(SectionInf(nEntSec).nQStation).sStationProp, 1) = "F" Then
                    EnterSecCheck = nEntSec
                    Exit Function
                End If
                nSecNum = SeekSecNum(SectionInf(nEntSec).nQStation, nUpDown)
                If nSecNum > 0 Then
                    EnterSecCheck = nSecNum
                Else
                    EnterSecCheck = nEntSec
                End If
            End If
        Else
            EnterSecCheck = nEntSec
        End If
    End Function

    Function nGuDaoPass(ByVal lPtime As Long, ByVal nTrnNum As Integer, _
    ByVal nLeaveSection As Integer, ByVal ntmpStation As Integer, ByVal nEnterSection As Integer) As Integer
        '检查列车通过时正线是否空闲
        Dim nUpDown As Integer, nGuDaoCheck As Integer
        Dim nStatemp As Integer
        nStatemp = ntmpStation
        nUpDown = nDirection(nTrnNum)
        nGuDaoCheck = PassLineNum(nTrnNum, nStatemp, lPtime)
        Select Case nGuDaoCheck
            Case 0
                '正线通过
                nGuDaoPass = CheckPass(nTrnNum, lPtime, nUpDown, nLeaveSection, nStatemp, nEnterSection)
            Case 1
                '正线被占用
                nGuDaoPass = 4
        End Select
    End Function

    Function FindPassTime(ByVal nConflict As Integer, ByVal nTrnNum As Integer, ByVal lTime As Long, _
    ByVal nBstation As Integer, ByVal nUporDown As Integer, ByVal nLeaveSection As Integer, ByVal nEnterSection As Integer, _
    ByVal nPhFx As Integer) As Long

        'nTrnNum 本次列车编号，nStatemp当前车站，nUporDown上下行
        'nConflict交叉的种类，nPhFx铺画方式
        '寻找可移动的出发时间
        Dim nUporDowntemp As Integer, nIntertemp As Integer, nNumtemp As Integer
        Dim ii As Integer, nTempSt As Integer
        Dim lDifftemp As Long
        Dim nStatemp As Integer

        If nUporDown = 1 Then
            nUporDowntemp = 2
            If StationInf(SectionInf(nEnterSection).nQStation).sStationName = _
                TrainInf(nTrnNum).NextStation And nLeaveSection = nEnterSection Then
                nStatemp = SectionInf(nEnterSection).nQStation
            Else
                nStatemp = SectionInf(nEnterSection).nHStation
            End If
        Else
            nUporDowntemp = 1
            If StationInf(SectionInf(nEnterSection).nHStation).sStationName = _
                TrainInf(nTrnNum).NextStation And nLeaveSection = nEnterSection Then
                nStatemp = SectionInf(nEnterSection).nHStation
            Else
                nStatemp = SectionInf(nEnterSection).nQStation
            End If
        End If
        FindPassTime = 0
        Select Case nPhFx
            Case 1
                Select Case nConflict
                    Case 1
                        '前后都不够，只考虑在后方站停车，并右移发车时刻
                        If TxIntervalKind1(nTrnNum, nStatemp) >= 300 Then
                            FindPassTime = TimeAdd(lTime, TxDiffTime2(nTrnNum, nStatemp))
                        Else
                            If TxDiffTime2(nTrnNum, nStatemp) < FxDiffTime2(nTrnNum, nStatemp) Then
                                If TimeMinus(lTime, TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).Arrival(nStatemp)) <= TimeMinus(TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).Arrival(nStatemp), lTime) Then
                                    lDifftemp = TimeMinus(TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).Starting(nStatemp), lTime)
                                Else
                                    lDifftemp = TimeMinus(TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).Arrival(nStatemp), lTime)
                                End If
                                nTempSt = nFindHstaNum(nTrnNum, nStatemp, nUporDown)
                                lDifftemp = TimeAdd(TrainInf(nTrnNum).Starting(nTempSt), lDifftemp)
                            Else
                                If TimeMinus(lTime, TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).Arrival(nStatemp)) <= TimeMinus(TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).Arrival(nStatemp), lTime) Then
                                    lDifftemp = TimeMinus(TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).Starting(nStatemp), lTime)
                                Else
                                    lDifftemp = TimeMinus(TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).Arrival(nStatemp), lTime)
                                End If
                                nTempSt = nFindHstaNum(nTrnNum, nStatemp, nUporDown)
                                lDifftemp = TimeAdd(TrainInf(nTrnNum).Starting(nTempSt), lDifftemp)
                            End If
                            FindPassTime = TimeAdd(lDifftemp, 120)
                        End If
                    Case 2
                        '前不够，考虑右移（对211、212、213考虑左移，左移不够时再考虑右移）；
                        '上述左右移都不够时，在该站停车，并右移发车时刻至后行车后
                        If TxDiffTime1(nTrnNum, nStatemp) < FxDiffTime1(nTrnNum, nStatemp) Then
                            lDifftemp = FxDiffTime1(nTrnNum, nStatemp)
                            nIntertemp = FxIntervalKind1(nTrnNum, nStatemp)
                            nNumtemp = FxDiffTrain1(nTrnNum, nStatemp)
                        Else
                            lDifftemp = TxDiffTime1(nTrnNum, nStatemp)
                            nIntertemp = TxIntervalKind1(nTrnNum, nStatemp)
                            nNumtemp = TxDiffTrain1(nTrnNum, nStatemp)
                        End If


                        'nTempSt = FindSstaNum(nTrnNum, nStatemp, nBstation, nUporDown, nPhFx)
                        nTempSt = FindSstaNum(nTrnNum, nStatemp, nBstation, nUporDown, nPhFx)

                        If nIntertemp = 211 Or nIntertemp = 212 Or nIntertemp = 213 Then
                            lDifftemp = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                        0, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                            ii = 209
                            lDifftemp = lDifftemp + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                        0, nLeaveSection, nStatemp, nEnterSection)
                        End If

                        'FindPassTime = lCanRightMovePass(lDifftemp, nTrnNum, nStatemp, nTempSt, nIntertemp, lTime, nPhFx)
                        FindPassTime = lCanRightMovePass(lDifftemp, nTrnNum, nStatemp, nTempSt, nIntertemp, lTime, nPhFx)


                        If FindPassTime <> 0 Then
                            If nIntertemp = 211 Or nIntertemp = 212 Or nIntertemp = 213 Then
                                FindPassTime = -FindPassTime
                            End If
                        End If
                    Case 3
                        '后不够，考虑左移；
                        '上述左移不够时，在该站停车，并右移发车时刻至后行车后
                        If TxDiffTime2(nTrnNum, nStatemp) < FxDiffTime2(nTrnNum, nStatemp) Then
                            lDifftemp = FxDiffTime2(nTrnNum, nStatemp)
                            nIntertemp = FxIntervalKind2(nTrnNum, nStatemp)
                            nNumtemp = FxDiffTrain2(nTrnNum, nStatemp)
                        Else
                            lDifftemp = TxDiffTime2(nTrnNum, nStatemp)
                            nIntertemp = TxIntervalKind2(nTrnNum, nStatemp)
                            nNumtemp = TxDiffTrain2(nTrnNum, nStatemp)
                            nUporDowntemp = nUporDown
                        End If
                        '左移
                        'If nIntertemp = 203 Or nIntertemp = 204 Or nIntertemp = 205 Then
                        '    lDifftemp = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                        '                0, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                        '    ii = 206
                        '    lDifftemp = lIntervalTime(nNumtemp, nTrnNum, ii, _
                        '                0, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                        'End If
                        'nTempSt = FindSstaNum(nTrnNum, nStatemp, nBstation, nUporDown, nPhFx)
                        'FindPassTime = lCanLeftMovePass(lDifftemp, nTrnNum, nStatemp, nTempSt, nIntertemp, lTime, nPhFx)
                End Select
            Case 2
                Select Case nConflict
                    Case 1
                        '前后都不够，只考虑在后方站停车，并右移发车时刻
                        If TxIntervalKind1(nTrnNum, nStatemp) >= 300 Then
                            FindPassTime = TimeAdd(lTime, TxDiffTime2(nTrnNum, nStatemp))
                        Else
                            FindPassTime = TimeMinus(lQConflictTong(nTrnNum, nStatemp), 30)
                        End If
                    Case 2
                        '前不够，考虑右移（对211、212、213考虑左移，左移不够时再考虑右移）；
                        '上述左右移都不够时，在该站停车，并右移发车时刻至后行车后
                        If TxDiffTime1(nTrnNum, nStatemp) < FxDiffTime1(nTrnNum, nStatemp) Then
                            lDifftemp = FxDiffTime1(nTrnNum, nStatemp)
                            nIntertemp = FxIntervalKind1(nTrnNum, nStatemp)
                            nNumtemp = FxDiffTrain1(nTrnNum, nStatemp)
                        Else
                            lDifftemp = TxDiffTime1(nTrnNum, nStatemp)
                            nIntertemp = TxIntervalKind1(nTrnNum, nStatemp)
                            nNumtemp = TxDiffTrain1(nTrnNum, nStatemp)
                        End If
                        'nTempSt = FindSstaNum(nTrnNum, nStatemp, nBstation, nUporDown, nPhFx)

                        If nIntertemp = 211 Or nIntertemp = 212 Or nIntertemp = 213 Then
                            lDifftemp = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                        0, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                            ii = 209
                            lDifftemp = lDifftemp + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                        0, nLeaveSection, nStatemp, nEnterSection)
                        End If
                        'FindPassTime = lCanRightMovePass(lDifftemp, nTrnNum, nStatemp, nTempSt, nIntertemp, lTime, nPhFx)

                        If FindPassTime <> 0 Then
                            If nIntertemp = 211 Or nIntertemp = 212 Or nIntertemp = 213 Then
                                FindPassTime = -FindPassTime
                            End If
                        End If
                    Case 3
                        '后不够，考虑左移；
                        '上述左移不够时，在该站停车，并右移发车时刻至后行车后
                        If TxDiffTime2(nTrnNum, nStatemp) < FxDiffTime2(nTrnNum, nStatemp) Then
                            lDifftemp = FxDiffTime2(nTrnNum, nStatemp)
                            nIntertemp = FxIntervalKind2(nTrnNum, nStatemp)
                            nNumtemp = FxDiffTrain2(nTrnNum, nStatemp)
                        Else
                            lDifftemp = TxDiffTime2(nTrnNum, nStatemp)
                            nIntertemp = TxIntervalKind2(nTrnNum, nStatemp)
                            nNumtemp = TxDiffTrain2(nTrnNum, nStatemp)
                            nUporDowntemp = nUporDown
                        End If
                        '左移
                        If nIntertemp = 203 Or nIntertemp = 204 Or nIntertemp = 205 Then
                            lDifftemp = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                        0, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                            ii = 206
                            lDifftemp = lIntervalTime(nNumtemp, nTrnNum, ii, _
                                        0, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                        End If
                        'nTempSt = FindSstaNum(nTrnNum, nStatemp, nBstation, nUporDown, nPhFx)
                        'FindPassTime = lCanLeftMovePass(lDifftemp, nTrnNum, nStatemp, nTempSt, nIntertemp, lTime, nPhFx)
                End Select
        End Select
    End Function

    Function FindSstaNum(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal nBStatemp As Integer, _
    ByVal nUporDown As Integer, ByVal PhFx As Integer) As Integer
        'num本次列车编号，statemp当前车站，bstatemp起始车站，KH客或货车，upordown上下行
        '寻找已停的车站
        Dim nTemp As Integer
        Dim nAnoTrnNum As Integer, nUpDowntemp As Integer
        'nstep步长
        Select Case PhFx
            Case 1
                FindSstaNum = nBStatemp
                nTemp = nStatemp
                nAnoTrnNum = nTrnNum
                nUpDowntemp = nUporDown
                Do While nTemp <> nBStatemp
                    If nChaDirectionSt(nAnoTrnNum, nTemp, 1) <> 0 Then
                        nTemp = nChaDirectionSt(nAnoTrnNum, nTemp, 1)
                        nAnoTrnNum = nChaTrnNum(nAnoTrnNum)
                        nUpDowntemp = nDirection(nAnoTrnNum)
                    End If
                    nTemp = nFindHstaNum(nAnoTrnNum, nTemp, nUpDowntemp)

                    If StationInf(nTemp).sStationName = TrainInf(nTrnNum).ComeStation Then
                        nTemp = nBStatemp
                    End If
                    With TrainInf(nAnoTrnNum)
                        If .Arrival(nTemp) <> -1 And .Starting(nTemp) <> -1 Then
                            If .Arrival(nTemp) <> .Starting(nTemp) Then
                                FindSstaNum = nTemp
                                Exit Do
                            End If
                        Else
                            'Stop
                        End If
                    End With
                Loop
            Case 2
                FindSstaNum = nBStatemp
                nTemp = nStatemp
                nAnoTrnNum = nTrnNum
                nUpDowntemp = nUporDown
                Do While nTemp <> nBStatemp
                    If nChaDirectionSt(nAnoTrnNum, nTemp, 0) <> 0 Then
                        nTemp = nChaDirectionSt(nAnoTrnNum, nTemp, 0)
                        nAnoTrnNum = nChaTrnNum(nAnoTrnNum)
                        nUpDowntemp = nDirection(nAnoTrnNum)
                    End If
                    nTemp = nFindQstaNum(nAnoTrnNum, nTemp, nUpDowntemp)

                    If StationInf(nTemp).sStationName = TrainInf(nTrnNum).NextStation Then
                        nTemp = nBStatemp
                    End If
                    With TrainInf(nAnoTrnNum)
                        If .Arrival(nTemp) <> -1 And .Starting(nTemp) <> -1 Then
                            If .Arrival(nTemp) <> .Starting(nTemp) Then
                                FindSstaNum = nTemp
                                Exit Do
                            End If
                        Else
                            'Stop
                        End If
                    End With
                Loop
        End Select
    End Function

    Sub HMoveLine(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal lMoveTime As Long, ByVal nSStatemp As Integer, _
    ByVal nUporDown As Integer, ByVal nPhFx As Integer)
        'num本次列车编号，statemp当前车站，sstatemp前一停车站
        'movetime移动的时间，upordown上下行
        '右移运行线
        Dim i As Integer, j As Integer, nTempTrnNum As Integer
        Dim nAnoTrnNum As Integer, nUpDowntemp As Integer, nTempSt As Integer
        Select Case nPhFx
            Case 1
                i = nStatemp
                nAnoTrnNum = nTrnNum
                nUpDowntemp = nUporDown
                Do
                    nTempTrnNum = nChaTrnNum(nAnoTrnNum)
                    With TrainInf(nAnoTrnNum)
                        If .Arrival(i) <> -1 Then
                            .Arrival(i) = TimeAdd(.Arrival(i), lMoveTime)
                        End If
                        If .Starting(i) <> -1 Then
                            .Starting(i) = TimeAdd(.Starting(i), lMoveTime)
                        End If
                        TxDiffTime1(nAnoTrnNum, i) = TxDiffTime1(nAnoTrnNum, i) - lMoveTime
                        TxDiffTime2(nAnoTrnNum, i) = TxDiffTime2(nAnoTrnNum, i) + lMoveTime
                        FxDiffTime1(nAnoTrnNum, i) = FxDiffTime1(nAnoTrnNum, i) - lMoveTime
                        FxDiffTime2(nAnoTrnNum, i) = FxDiffTime2(nAnoTrnNum, i) + lMoveTime
                        If nTempTrnNum <> 0 Then
                            TrainInf(nTempTrnNum).Arrival(i) = .Arrival(i)
                            TrainInf(nTempTrnNum).Starting(i) = .Starting(i)
                            TxDiffTime1(nTempTrnNum, i) = TxDiffTime1(nAnoTrnNum, i)
                            TxDiffTime2(nTempTrnNum, i) = TxDiffTime2(nAnoTrnNum, i)
                            FxDiffTime1(nTempTrnNum, i) = FxDiffTime1(nAnoTrnNum, i)
                            FxDiffTime2(nTempTrnNum, i) = FxDiffTime2(nAnoTrnNum, i)
                        End If
                        For j = 1 To UBound(StationInf)
                            If StationInf(j).sStationName = StationInf(i).sStationName And j <> i And j <> nSStatemp Then
                                .Arrival(j) = .Arrival(i)
                                .Starting(j) = .Starting(i)
                                TxDiffTime1(nAnoTrnNum, j) = TxDiffTime1(nAnoTrnNum, i)
                                TxDiffTime2(nAnoTrnNum, j) = TxDiffTime2(nAnoTrnNum, i)
                                FxDiffTime1(nAnoTrnNum, j) = FxDiffTime1(nAnoTrnNum, i)
                                FxDiffTime2(nAnoTrnNum, j) = FxDiffTime2(nAnoTrnNum, i)
                                If nTempTrnNum <> 0 Then
                                    TrainInf(nTempTrnNum).Arrival(j) = .Arrival(j)
                                    TrainInf(nTempTrnNum).Starting(j) = .Starting(j)
                                    TxDiffTime1(nTempTrnNum, j) = TxDiffTime1(nAnoTrnNum, j)
                                    TxDiffTime2(nTempTrnNum, j) = TxDiffTime2(nAnoTrnNum, j)
                                    FxDiffTime1(nTempTrnNum, j) = FxDiffTime1(nAnoTrnNum, j)
                                    FxDiffTime2(nTempTrnNum, j) = FxDiffTime2(nAnoTrnNum, j)
                                End If
                            End If
                        Next j
                        nTempSt = nChaDirectionSt(nAnoTrnNum, i, 1)
                        If nTempSt <> 0 Then
                            i = nTempSt
                            nAnoTrnNum = nChaTrnNum(nAnoTrnNum)
                            nUpDowntemp = nDirection(nAnoTrnNum)
                        End If
                        i = nFindHstaNum(nAnoTrnNum, i, nUpDowntemp)
                        If StationInf(i).sStationName = .ComeStation Then
                            i = nBstation
                        End If
                    End With
                Loop Until i = nSStatemp
                With TrainInf(nAnoTrnNum)
                    If .Starting(nSStatemp) <> -1 Then
                        .Starting(nSStatemp) = TimeAdd(.Starting(nSStatemp), lMoveTime)
                    End If
                    TxDiffTime1(nAnoTrnNum, nSStatemp) = TxDiffTime1(nAnoTrnNum, nSStatemp) - lMoveTime
                    TxDiffTime2(nAnoTrnNum, nSStatemp) = TxDiffTime2(nAnoTrnNum, nSStatemp) + lMoveTime
                    FxDiffTime1(nAnoTrnNum, nSStatemp) = FxDiffTime1(nAnoTrnNum, nSStatemp) - lMoveTime
                    FxDiffTime2(nAnoTrnNum, nSStatemp) = FxDiffTime2(nAnoTrnNum, nSStatemp) + lMoveTime
                    nTempTrnNum = nChaTrnNum(nAnoTrnNum)
                    If nTempTrnNum <> 0 Then
                        TrainInf(nTempTrnNum).Arrival(nSStatemp) = .Arrival(nSStatemp)
                        TrainInf(nTempTrnNum).Starting(nSStatemp) = .Starting(nSStatemp)
                        TxDiffTime1(nTempTrnNum, nSStatemp) = TxDiffTime1(nAnoTrnNum, nSStatemp)
                        TxDiffTime2(nTempTrnNum, nSStatemp) = TxDiffTime2(nAnoTrnNum, nSStatemp)
                        FxDiffTime1(nTempTrnNum, nSStatemp) = FxDiffTime1(nAnoTrnNum, nSStatemp)
                        FxDiffTime2(nTempTrnNum, nSStatemp) = FxDiffTime2(nAnoTrnNum, nSStatemp)
                    End If
                    For j = 1 To UBound(StationInf)
                        If StationInf(j).sStationName = StationInf(nSStatemp).sStationName And j <> nSStatemp Then
                            .Arrival(j) = .Arrival(nSStatemp)
                            .Starting(j) = .Starting(nSStatemp)
                            TxDiffTime1(nAnoTrnNum, j) = TxDiffTime1(nAnoTrnNum, nSStatemp)
                            TxDiffTime2(nAnoTrnNum, j) = TxDiffTime2(nAnoTrnNum, nSStatemp)
                            FxDiffTime1(nAnoTrnNum, j) = FxDiffTime1(nAnoTrnNum, nSStatemp)
                            FxDiffTime2(nAnoTrnNum, j) = FxDiffTime2(nAnoTrnNum, nSStatemp)
                            If nTempTrnNum <> 0 Then
                                TrainInf(nTempTrnNum).Arrival(j) = .Arrival(j)
                                TrainInf(nTempTrnNum).Starting(j) = .Starting(j)
                                TxDiffTime1(nTempTrnNum, j) = TxDiffTime1(nAnoTrnNum, j)
                                TxDiffTime2(nTempTrnNum, j) = TxDiffTime2(nAnoTrnNum, j)
                                FxDiffTime1(nTempTrnNum, j) = FxDiffTime1(nAnoTrnNum, j)
                                FxDiffTime2(nTempTrnNum, j) = FxDiffTime2(nAnoTrnNum, j)
                            End If
                        End If
                    Next j
                    TrainInf(nTrnNum).StopLineTime(nSStatemp) = TrainInf(nTrnNum).StopLineTime(nSStatemp) + lMoveTime
                End With
            Case 2
                i = nStatemp
                nAnoTrnNum = nTrnNum
                nUpDowntemp = nUporDown
                Do
                    nTempTrnNum = nChaTrnNum(nAnoTrnNum)
                    With TrainInf(nAnoTrnNum)
                        If .Arrival(i) <> -1 Then
                            .Arrival(i) = TimeAdd(.Arrival(i), lMoveTime)
                        End If
                        If .Starting(i) <> -1 Then
                            .Starting(i) = TimeAdd(.Starting(i), lMoveTime)
                        End If
                        TxDiffTime1(nAnoTrnNum, i) = TxDiffTime1(nAnoTrnNum, i) - lMoveTime
                        TxDiffTime2(nAnoTrnNum, i) = TxDiffTime2(nAnoTrnNum, i) + lMoveTime
                        FxDiffTime1(nAnoTrnNum, i) = FxDiffTime1(nAnoTrnNum, i) - lMoveTime
                        FxDiffTime2(nAnoTrnNum, i) = FxDiffTime2(nAnoTrnNum, i) + lMoveTime
                        If nTempTrnNum <> 0 Then
                            TrainInf(nTempTrnNum).Arrival(i) = .Arrival(i)
                            TrainInf(nTempTrnNum).Starting(i) = .Starting(i)
                            TxDiffTime1(nTempTrnNum, i) = TxDiffTime1(nAnoTrnNum, i)
                            TxDiffTime2(nTempTrnNum, i) = TxDiffTime2(nAnoTrnNum, i)
                            FxDiffTime1(nTempTrnNum, i) = FxDiffTime1(nAnoTrnNum, i)
                            FxDiffTime2(nTempTrnNum, i) = FxDiffTime2(nAnoTrnNum, i)
                        End If
                        For j = 1 To UBound(StationInf)
                            If StationInf(j).sStationName = StationInf(i).sStationName And j <> i And j <> nSStatemp Then
                                .Arrival(j) = .Arrival(i)
                                .Starting(j) = .Starting(i)
                                TxDiffTime1(nAnoTrnNum, j) = TxDiffTime1(nAnoTrnNum, i)
                                TxDiffTime2(nAnoTrnNum, j) = TxDiffTime2(nAnoTrnNum, i)
                                FxDiffTime1(nAnoTrnNum, j) = FxDiffTime1(nAnoTrnNum, i)
                                FxDiffTime2(nAnoTrnNum, j) = FxDiffTime2(nAnoTrnNum, i)
                                If nTempTrnNum <> 0 Then
                                    TrainInf(nTempTrnNum).Arrival(j) = .Arrival(j)
                                    TrainInf(nTempTrnNum).Starting(j) = .Starting(j)
                                    TxDiffTime1(nTempTrnNum, j) = TxDiffTime1(nAnoTrnNum, j)
                                    TxDiffTime2(nTempTrnNum, j) = TxDiffTime2(nAnoTrnNum, j)
                                    FxDiffTime1(nTempTrnNum, j) = FxDiffTime1(nAnoTrnNum, j)
                                    FxDiffTime2(nTempTrnNum, j) = FxDiffTime2(nAnoTrnNum, j)
                                End If
                            End If
                        Next j
                        nTempSt = nChaDirectionSt(nAnoTrnNum, i, 0)
                        If nTempSt <> 0 Then
                            i = nTempSt
                            nAnoTrnNum = nChaTrnNum(nAnoTrnNum)
                            nUpDowntemp = nDirection(nAnoTrnNum)
                        End If
                        i = nFindQstaNum(nAnoTrnNum, i, nUpDowntemp)
                        If StationInf(i).sStationName = .NextStation Then
                            i = nBstation
                        End If
                    End With
                Loop Until i = nSStatemp
                If StationInf(nStatemp).sStationName <> StationInf(nSStatemp).sStationName Then
                    With TrainInf(nAnoTrnNum)
                        If .Arrival(nSStatemp) <> -1 Then
                            .Arrival(nSStatemp) = TimeAdd(.Arrival(nSStatemp), lMoveTime)
                        End If
                        TxDiffTime1(nAnoTrnNum, nSStatemp) = TxDiffTime1(nAnoTrnNum, nSStatemp) - lMoveTime
                        TxDiffTime2(nAnoTrnNum, nSStatemp) = TxDiffTime2(nAnoTrnNum, nSStatemp) + lMoveTime
                        FxDiffTime1(nAnoTrnNum, nSStatemp) = FxDiffTime1(nAnoTrnNum, nSStatemp) - lMoveTime
                        FxDiffTime2(nAnoTrnNum, nSStatemp) = FxDiffTime2(nAnoTrnNum, nSStatemp) + lMoveTime
                        nTempTrnNum = nChaTrnNum(nAnoTrnNum)
                        If nTempTrnNum <> 0 Then
                            TrainInf(nTempTrnNum).Arrival(nSStatemp) = .Arrival(nSStatemp)
                            TrainInf(nTempTrnNum).Starting(nSStatemp) = .Starting(nSStatemp)
                            TxDiffTime1(nTempTrnNum, nSStatemp) = TxDiffTime1(nAnoTrnNum, nSStatemp)
                            TxDiffTime2(nTempTrnNum, nSStatemp) = TxDiffTime2(nAnoTrnNum, nSStatemp)
                            FxDiffTime1(nTempTrnNum, nSStatemp) = FxDiffTime1(nAnoTrnNum, nSStatemp)
                            FxDiffTime2(nTempTrnNum, nSStatemp) = FxDiffTime2(nAnoTrnNum, nSStatemp)
                        End If
                        For j = 1 To UBound(StationInf)
                            If StationInf(j).sStationName = StationInf(nSStatemp).sStationName And j <> nSStatemp Then
                                .Arrival(j) = .Arrival(nSStatemp)
                                .Starting(j) = .Starting(nSStatemp)
                                TxDiffTime1(nAnoTrnNum, j) = TxDiffTime1(nAnoTrnNum, nSStatemp)
                                TxDiffTime2(nAnoTrnNum, j) = TxDiffTime2(nAnoTrnNum, nSStatemp)
                                FxDiffTime1(nAnoTrnNum, j) = FxDiffTime1(nAnoTrnNum, nSStatemp)
                                FxDiffTime2(nAnoTrnNum, j) = FxDiffTime2(nAnoTrnNum, nSStatemp)
                                If nTempTrnNum <> 0 Then
                                    TrainInf(nTempTrnNum).Arrival(j) = .Arrival(j)
                                    TrainInf(nTempTrnNum).Starting(j) = .Starting(j)
                                    TxDiffTime1(nTempTrnNum, j) = TxDiffTime1(nAnoTrnNum, j)
                                    TxDiffTime2(nTempTrnNum, j) = TxDiffTime2(nAnoTrnNum, j)
                                    FxDiffTime1(nTempTrnNum, j) = FxDiffTime1(nAnoTrnNum, j)
                                    FxDiffTime2(nTempTrnNum, j) = FxDiffTime2(nAnoTrnNum, j)
                                End If
                            End If
                        Next j
                        TrainInf(nTrnNum).StopLineTime(nSStatemp) = TrainInf(nTrnNum).StopLineTime(nSStatemp) + lMoveTime
                    End With
                End If
        End Select
    End Sub

    Function lCanPaoDian(ByVal ntmpTrainNumber As Integer, ByVal nStatemp As Integer) As Long
        Dim lDifftemp1 As Long, lDifftemp2 As Long

        If TxDiffTime1(ntmpTrainNumber, nStatemp) < FxDiffTime1(ntmpTrainNumber, nStatemp) Then
            lDifftemp1 = FxDiffTime1(ntmpTrainNumber, nStatemp)
        Else
            lDifftemp1 = TxDiffTime1(ntmpTrainNumber, nStatemp)
        End If
        If TxDiffTime2(ntmpTrainNumber, nStatemp) < FxDiffTime2(ntmpTrainNumber, nStatemp) Then
            lDifftemp2 = FxDiffTime2(ntmpTrainNumber, nStatemp)
        Else
            lDifftemp2 = TxDiffTime2(ntmpTrainNumber, nStatemp)
        End If
        If lDifftemp1 <= lPaoDian And lDifftemp1 <= Math.Abs(lDifftemp2) Then
            lCanPaoDian = lDifftemp1
        Else
            lCanPaoDian = 0
        End If
    End Function

    Sub QMoveLine(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal lMoveTime As Long, ByVal nSStatemp As Integer, _
    ByVal nUporDown As Integer, ByVal nPhFx As Integer)
        'num本次列车编号，statemp当前车站，sstatemp前一停车站
        'movetime移动的时间，upordown上下行
        '左移运行线
        Dim i As Integer, j As Integer, nTempTrnNum As Integer
        Dim nAnoTrnNum As Integer, nUpDowntemp As Integer, nTempSt As Integer
        Dim nBegintemp As Integer, nEndtemp As Integer

        nBegintemp = nBeginDataStation(nNowDataReadLineNum)
        nEndtemp = nEndDataStation(nNowDataReadLineNum)
        Select Case nPhFx
            Case 1
                i = nStatemp
                nAnoTrnNum = nTrnNum
                nUpDowntemp = nUporDown
                Do
                    With TrainInf(nAnoTrnNum)
                        nTempTrnNum = nChaTrnNum(nAnoTrnNum)
                        If .Arrival(i) <> -1 And nStartstation(nTrnNum, i, nPhFx) <> 3 Then
                            .Arrival(i) = TimeMinus(.Arrival(i), lMoveTime)
                        End If
                        If .Starting(i) <> -1 Then
                            .Starting(i) = TimeMinus(.Starting(i), lMoveTime)
                        End If
                        TxDiffTime1(nAnoTrnNum, i) = TxDiffTime1(nAnoTrnNum, i) + lMoveTime
                        TxDiffTime2(nAnoTrnNum, i) = TxDiffTime2(nAnoTrnNum, i) - lMoveTime
                        FxDiffTime1(nAnoTrnNum, i) = FxDiffTime1(nAnoTrnNum, i) + lMoveTime
                        FxDiffTime2(nAnoTrnNum, i) = FxDiffTime2(nAnoTrnNum, i) - lMoveTime
                        If nTempTrnNum <> 0 Then
                            TrainInf(nTempTrnNum).Arrival(i) = .Arrival(i)
                            TrainInf(nTempTrnNum).Starting(i) = .Starting(i)
                            TxDiffTime1(nTempTrnNum, i) = TxDiffTime1(nAnoTrnNum, i)
                            TxDiffTime2(nTempTrnNum, i) = TxDiffTime2(nAnoTrnNum, i)
                            FxDiffTime1(nTempTrnNum, i) = FxDiffTime1(nAnoTrnNum, i)
                            FxDiffTime2(nTempTrnNum, i) = FxDiffTime2(nAnoTrnNum, i)
                        End If
                        For j = nBegintemp To nEndtemp
                            If StationInf(j).sStationName = StationInf(i).sStationName And j <> i And j <> nSStatemp Then
                                .Arrival(j) = .Arrival(i)
                                .Starting(j) = .Starting(i)
                                TxDiffTime1(nAnoTrnNum, j) = TxDiffTime1(nAnoTrnNum, i)
                                TxDiffTime2(nAnoTrnNum, j) = TxDiffTime2(nAnoTrnNum, i)
                                FxDiffTime1(nAnoTrnNum, j) = FxDiffTime1(nAnoTrnNum, i)
                                FxDiffTime2(nAnoTrnNum, j) = FxDiffTime2(nAnoTrnNum, i)
                                If nTempTrnNum <> 0 Then
                                    TrainInf(nTempTrnNum).Arrival(j) = .Arrival(j)
                                    TrainInf(nTempTrnNum).Starting(j) = .Starting(j)
                                    TxDiffTime1(nTempTrnNum, j) = TxDiffTime1(nAnoTrnNum, j)
                                    TxDiffTime2(nTempTrnNum, j) = TxDiffTime2(nAnoTrnNum, j)
                                    FxDiffTime1(nTempTrnNum, j) = FxDiffTime1(nAnoTrnNum, j)
                                    FxDiffTime2(nTempTrnNum, j) = FxDiffTime2(nAnoTrnNum, j)
                                End If
                            End If
                        Next j
                        nTempSt = nChaDirectionSt(nAnoTrnNum, i, 1)
                        If nTempSt <> 0 Then
                            i = nTempSt
                            nAnoTrnNum = nChaTrnNum(nAnoTrnNum)
                            nUpDowntemp = nDirection(nAnoTrnNum)
                        End If
                        i = nFindHstaNum(nAnoTrnNum, i, nUpDowntemp)
                        If StationInf(i).sStationName = .ComeStation Then
                            i = nBstation
                        End If
                    End With
                Loop Until i = nSStatemp
                With TrainInf(nAnoTrnNum)
                    If .Starting(nSStatemp) <> -1 Then
                        .Starting(nSStatemp) = TimeMinus(.Starting(nSStatemp), lMoveTime)
                    End If
                    TxDiffTime1(nAnoTrnNum, nSStatemp) = TxDiffTime1(nAnoTrnNum, nSStatemp) + lMoveTime
                    TxDiffTime2(nAnoTrnNum, nSStatemp) = TxDiffTime2(nAnoTrnNum, nSStatemp) - lMoveTime
                    FxDiffTime1(nAnoTrnNum, nSStatemp) = FxDiffTime1(nAnoTrnNum, nSStatemp) + lMoveTime
                    FxDiffTime2(nAnoTrnNum, nSStatemp) = FxDiffTime2(nAnoTrnNum, nSStatemp) - lMoveTime
                    nTempTrnNum = nChaTrnNum(nAnoTrnNum)
                    If nTempTrnNum <> 0 Then
                        TrainInf(nTempTrnNum).Arrival(nSStatemp) = .Arrival(nSStatemp)
                        TrainInf(nTempTrnNum).Starting(nSStatemp) = .Starting(nSStatemp)
                        TxDiffTime1(nTempTrnNum, nSStatemp) = TxDiffTime1(nAnoTrnNum, nSStatemp)
                        TxDiffTime2(nTempTrnNum, nSStatemp) = TxDiffTime2(nAnoTrnNum, nSStatemp)
                        FxDiffTime1(nTempTrnNum, nSStatemp) = FxDiffTime1(nAnoTrnNum, nSStatemp)
                        FxDiffTime2(nTempTrnNum, nSStatemp) = FxDiffTime2(nAnoTrnNum, nSStatemp)
                    End If
                    For j = nBegintemp To nEndtemp
                        If StationInf(j).sStationName = StationInf(nSStatemp).sStationName And j <> nSStatemp Then
                            .Arrival(j) = .Arrival(nSStatemp)
                            .Starting(j) = .Starting(nSStatemp)
                            TxDiffTime1(nAnoTrnNum, j) = TxDiffTime1(nAnoTrnNum, nSStatemp)
                            TxDiffTime2(nAnoTrnNum, j) = TxDiffTime2(nAnoTrnNum, nSStatemp)
                            FxDiffTime1(nAnoTrnNum, j) = FxDiffTime1(nAnoTrnNum, nSStatemp)
                            FxDiffTime2(nAnoTrnNum, j) = FxDiffTime2(nAnoTrnNum, nSStatemp)
                            If nTempTrnNum <> 0 Then
                                TrainInf(nTempTrnNum).Arrival(j) = .Arrival(nSStatemp)
                                TrainInf(nTempTrnNum).Starting(j) = .Starting(nSStatemp)
                                TxDiffTime1(nTempTrnNum, j) = TxDiffTime1(nAnoTrnNum, nSStatemp)
                                TxDiffTime2(nTempTrnNum, j) = TxDiffTime2(nAnoTrnNum, nSStatemp)
                                FxDiffTime1(nTempTrnNum, j) = FxDiffTime1(nAnoTrnNum, nSStatemp)
                                FxDiffTime2(nTempTrnNum, j) = FxDiffTime2(nAnoTrnNum, nSStatemp)
                            End If
                        End If
                    Next j
                    TrainInf(nTrnNum).StopLineTime(nSStatemp) = TrainInf(nTrnNum).StopLineTime(nSStatemp) - lMoveTime
                End With
            Case 2
                i = nStatemp
                nAnoTrnNum = nTrnNum
                nUpDowntemp = nUporDown
                Do
                    With TrainInf(nAnoTrnNum)
                        nTempTrnNum = nChaTrnNum(nAnoTrnNum)
                        If .Arrival(i) <> -1 Then
                            .Arrival(i) = TimeMinus(.Arrival(i), lMoveTime)
                        End If
                        If .Starting(i) <> -1 And nEndStation(nTrnNum, i, nPhFx) <> 3 Then
                            .Starting(i) = TimeMinus(.Starting(i), lMoveTime)
                        End If
                        TxDiffTime1(nAnoTrnNum, i) = TxDiffTime1(nAnoTrnNum, i) + lMoveTime
                        TxDiffTime2(nAnoTrnNum, i) = TxDiffTime2(nAnoTrnNum, i) - lMoveTime
                        FxDiffTime1(nAnoTrnNum, i) = FxDiffTime1(nAnoTrnNum, i) + lMoveTime
                        FxDiffTime2(nAnoTrnNum, i) = FxDiffTime2(nAnoTrnNum, i) - lMoveTime
                        If nTempTrnNum <> 0 Then
                            TrainInf(nTempTrnNum).Arrival(i) = .Arrival(i)
                            TrainInf(nTempTrnNum).Starting(i) = .Starting(i)
                            TxDiffTime1(nTempTrnNum, i) = TxDiffTime1(nAnoTrnNum, i)
                            TxDiffTime2(nTempTrnNum, i) = TxDiffTime2(nAnoTrnNum, i)
                            FxDiffTime1(nTempTrnNum, i) = FxDiffTime1(nAnoTrnNum, i)
                            FxDiffTime2(nTempTrnNum, i) = FxDiffTime2(nAnoTrnNum, i)
                        End If
                        For j = nBegintemp To nEndtemp
                            If StationInf(j).sStationName = StationInf(i).sStationName And j <> i And j <> nSStatemp Then
                                .Arrival(j) = .Arrival(i)
                                .Starting(j) = .Starting(i)
                                TxDiffTime1(nAnoTrnNum, j) = TxDiffTime1(nAnoTrnNum, i)
                                TxDiffTime2(nAnoTrnNum, j) = TxDiffTime2(nAnoTrnNum, i)
                                FxDiffTime1(nAnoTrnNum, j) = FxDiffTime1(nAnoTrnNum, i)
                                FxDiffTime2(nAnoTrnNum, j) = FxDiffTime2(nAnoTrnNum, i)
                                If nTempTrnNum <> 0 Then
                                    TrainInf(nTempTrnNum).Arrival(j) = .Arrival(j)
                                    TrainInf(nTempTrnNum).Starting(j) = .Starting(j)
                                    TxDiffTime1(nTempTrnNum, j) = TxDiffTime1(nAnoTrnNum, j)
                                    TxDiffTime2(nTempTrnNum, j) = TxDiffTime2(nAnoTrnNum, j)
                                    FxDiffTime1(nTempTrnNum, j) = FxDiffTime1(nAnoTrnNum, j)
                                    FxDiffTime2(nTempTrnNum, j) = FxDiffTime2(nAnoTrnNum, j)
                                End If
                            End If
                        Next j
                        nTempSt = nChaDirectionSt(nAnoTrnNum, i, 0)
                        If nTempSt <> 0 Then
                            i = nTempSt
                            nAnoTrnNum = nChaTrnNum(nAnoTrnNum)
                            nUpDowntemp = nDirection(nAnoTrnNum)
                        End If
                        i = nFindQstaNum(nAnoTrnNum, i, nUpDowntemp)
                        If StationInf(i).sStationName = .NextStation Then
                            i = nBstation
                        End If
                    End With
                Loop Until i = nSStatemp
                If StationInf(nStatemp).sStationName <> StationInf(nSStatemp).sStationName Then
                    With TrainInf(nAnoTrnNum)
                        If .Arrival(nSStatemp) <> -1 Then
                            .Arrival(nSStatemp) = TimeMinus(.Arrival(nSStatemp), lMoveTime)
                        End If
                        TxDiffTime1(nAnoTrnNum, nSStatemp) = TxDiffTime1(nAnoTrnNum, nSStatemp) + lMoveTime
                        TxDiffTime2(nAnoTrnNum, nSStatemp) = TxDiffTime2(nAnoTrnNum, nSStatemp) - lMoveTime
                        FxDiffTime1(nAnoTrnNum, nSStatemp) = FxDiffTime1(nAnoTrnNum, nSStatemp) + lMoveTime
                        FxDiffTime2(nAnoTrnNum, nSStatemp) = FxDiffTime2(nAnoTrnNum, nSStatemp) - lMoveTime
                        nTempTrnNum = nChaTrnNum(nAnoTrnNum)
                        If nTempTrnNum <> 0 Then
                            TrainInf(nTempTrnNum).Arrival(nSStatemp) = .Arrival(nSStatemp)
                            TrainInf(nTempTrnNum).Starting(nSStatemp) = .Starting(nSStatemp)
                            TxDiffTime1(nTempTrnNum, nSStatemp) = TxDiffTime1(nAnoTrnNum, nSStatemp)
                            TxDiffTime2(nTempTrnNum, nSStatemp) = TxDiffTime2(nAnoTrnNum, nSStatemp)
                            FxDiffTime1(nTempTrnNum, nSStatemp) = FxDiffTime1(nAnoTrnNum, nSStatemp)
                            FxDiffTime2(nTempTrnNum, nSStatemp) = FxDiffTime2(nAnoTrnNum, nSStatemp)
                        End If
                        For j = nBegintemp To nEndtemp
                            If StationInf(j).sStationName = StationInf(nSStatemp).sStationName And j <> nSStatemp Then
                                .Arrival(j) = .Arrival(nSStatemp)
                                .Starting(j) = .Starting(nSStatemp)
                                TxDiffTime1(nAnoTrnNum, j) = TxDiffTime1(nAnoTrnNum, nSStatemp)
                                TxDiffTime2(nAnoTrnNum, j) = TxDiffTime2(nAnoTrnNum, nSStatemp)
                                FxDiffTime1(nAnoTrnNum, j) = FxDiffTime1(nAnoTrnNum, nSStatemp)
                                FxDiffTime2(nAnoTrnNum, j) = FxDiffTime2(nAnoTrnNum, nSStatemp)
                                If nTempTrnNum <> 0 Then
                                    TrainInf(nTempTrnNum).Arrival(j) = .Arrival(nSStatemp)
                                    TrainInf(nTempTrnNum).Starting(j) = .Starting(nSStatemp)
                                    TxDiffTime1(nTempTrnNum, j) = TxDiffTime1(nAnoTrnNum, nSStatemp)
                                    TxDiffTime2(nTempTrnNum, j) = TxDiffTime2(nAnoTrnNum, nSStatemp)
                                    FxDiffTime1(nTempTrnNum, j) = FxDiffTime1(nAnoTrnNum, nSStatemp)
                                    FxDiffTime2(nTempTrnNum, j) = FxDiffTime2(nAnoTrnNum, nSStatemp)
                                End If
                            End If
                        Next j
                        TrainInf(nTrnNum).StopLineTime(nSStatemp) = TrainInf(nTrnNum).StopLineTime(nSStatemp) - lMoveTime
                    End With
                End If
        End Select
    End Sub

    Function nFindLinkTrain(ByVal nTrnNum As Integer) As Integer
        '查找该车是否还存在另一车次
        '0表示不存在另一车次,其他表示另一车次的编号
        Dim i As Integer
        nFindLinkTrain = 0
        With TrainInf(nTrnNum)
            If .nLinkTrainNum <> 0 Then
                If .nLinkTrainNum = 9999 Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).nLinkTrainNum = nTrnNum Then
                            nFindLinkTrain = i
                            Exit For
                        End If
                    Next i
                Else
                    nFindLinkTrain = .nLinkTrainNum
                End If
            End If
        End With
    End Function

    Sub ChangeStationNum(ByVal nTrainNumber As Integer, ByVal nStationNumber As Integer, _
      ByVal nUporDowntemp As Integer, ByVal nQianHou As Integer)

        Dim nTemp As Integer, nTemp1 As Integer
        nTemp = nChaDirectionSt(nTrainNumber, nStationNumber, nQianHou)
        nTemp1 = nTrainNumber
        If nTemp <> 0 Then
            nStationNumber = nTemp
            nTrainNumber = nChaTrnNum(nTrainNumber)
            nUporDowntemp = nDirection(nTrainNumber)
        End If
    End Sub

    Function lArrivalTimeReSet(ByVal ntmpTrainNumber As Integer, ByVal lTimetemp As Long, ByVal nStatemp As Integer) As Long
        '    If lTimetemp / 60 <> Int(lTimetemp / 60) Then
        '        Select Case StationInf(nStatemp).sStationName
        '            Case "杭州"
        '                If TrainInf(ntmpTrainNumber).TrainKind = "客车" Then
        '                    lArrivalTimeReSet = Int(lTimetemp / 30) * 30 + 30
        '                ElseIf TrainInf(ntmpTrainNumber).TrainKind = "货车" Then
        '                    lArrivalTimeReSet = lTimetemp
        '                End If
        lArrivalTimeReSet = lTimetemp
        '    End If
    End Function


    Function lStartTimeReSet(ByVal ntmpTrainNumber As Integer, ByVal lTimetemp As Long, ByVal nStatemp As Integer) As Long
        '    If lTimetemp / 60 <> Int(lTimetemp / 60) Then
        '        Select Case StationInf(nStatemp).sStationName
        '            Case "杭州"
        '                If TrainInf(ntmpTrainNumber).TrainKind = "客车" Then
        '                    lStartTimeReSet = Int(lTimetemp / 60) * 60 + 60
        '                ElseIf TrainInf(ntmpTrainNumber).TrainKind = "货车" Then
        '                    lStartTimeReSet = lTimetemp

        lStartTimeReSet = lTimetemp
        '    End If
    End Function



    Function DealKeCheArrival(ByVal nTrainNumbertemp As Integer, ByVal nStatemp As Integer, ByVal lTimetemp As Long) _
    As Long

        With TrainInf(nTrainNumbertemp)
 
            'If .TrainReturnStyle(2) = "站前折返" Or .TrainReturnStyle(2) = "立即折返" Then
            '    If .TrainReturn(2) > 0 Then
            '        .StopLine(nEstation) = TrainInf(.TrainReturn(2)).StopLine(nEstation)
            '    End If
            'End If
            DealKeCheArrival = TimeAdd(lTimetemp, lEndTchTime(nTrainNumbertemp))
        End With


        'Dim nTempTrainNumber As Integer
        'Dim sTemp1 As String, sTemp3 As String
        'Dim StartorArrival As String
        'Dim StartorArrivaltime As Long
        'nTempTrainNumber = nTrainNumbertemp
        'sTemp1 = ""
        'sTemp3 = ""
        'If TrainInf(nTempTrainNumber).TrainClassCal = 38 Then
        '    DealKeCheArrival = TimeAdd(lTimetemp, 15)
        'Else
        '    If TrainInf(nTempTrainNumber).TrainKind = "客车" Then
        '        If TrainInf(nTempTrainNumber).TrainClassCal <> 12 Then
        '            StartorArrival = "到达"
        '            StartorArrivaltime = lTimetemp
        '            If nPuHuaFangShi <> 0 Then

        '                Select Case sTemp1
        '                    Case "停留折返"
        '                        With TrainInf(nTempTrainNumber)
        '                            If TrainInf(.TrainReturn(2)).TrainPuorNot <> 0 Then
        '                                DealKeCheArrival = TrainInf(.TrainReturn(2)).Starting(nEstation)
        '                                .StopLine(nEstation) = sTemp3
        '                                TrainInf(.TrainReturn(2)).StopLine(nEstation) = sTemp3
        '                            Else
        '                                DealKeCheArrival = TimeAdd(lTimetemp, nStartTchTime(nTempTrainNumber))
        '                                .StopLine(nEstation) = sTemp3
        '                            End If
        '                        End With
        '                    Case "车底入库"
        '                        With TrainInf(nTempTrainNumber)
        '                            If TrainInf(.TrainContinue(2)).TrainPuorNot <> 0 Then
        '                                DealKeCheArrival = TrainInf(.TrainContinue(2)).Starting(nEstation)
        '                                .StopLine(nEstation) = sTemp3
        '                                TrainInf(.TrainContinue(2)).StopLine(nBstation) = sTemp3
        '                            Else
        '                                .StopLine(nEstation) = sTemp3
        '                                DealKeCheArrival = TimeAdd(lTimetemp, nStartTchTime(nTempTrainNumber))
        '                            End If
        '                        End With
        '                    Case Else
        '                End Select
        '            Else
        '                With TrainInf(nTempTrainNumber)
        '                    '                        If .TrainReturnStyle(2) = "停留折返" Then
        '                    '                            If TrainInf(.trainreturn(2)).TrainPuorNot <> 0 Then
        '                    '                                DealKeCheArrival = TrainInf(.trainreturn(2)).Starting(nEstation)
        '                    '                                .StopLine(nEstation) = _
        '                    '                                    TrainInf(.trainreturn(2)).StopLine(nEstation)
        '                    '                            Else
        '                    '                                DealKeCheArrival = TimeAdd(lTimetemp, lEndTchTime(nTempTrainNumber))
        '                    '                                '.StopLine(nEstation) = ""
        '                    '                            End If
        '                    '                        ElseIf .TrainReturnStyle(2) = "车底入库" Then
        '                    '                            If TrainInf(.TrainContinue(2)).TrainPuorNot <> 0 Then
        '                    '                                .StopLine(nEstation) = _
        '                    '                                    TrainInf(.TrainContinue(2)).StopLine(nEstation)
        '                    '                                DealKeCheArrival = TrainInf(.TrainContinue(2)).Starting(nEstation)
        '                    '                            Else
        '                    '                                '.StopLine(nEstation) = ""
        '                    '                                DealKeCheArrival = TimeAdd(lTimetemp, lEndTchTime(nTempTrainNumber))
        '                    '                            End If
        '                    If .TrainReturnStyle(2) = "站前折返" Then

        '                        DealKeCheArrival = TimeAdd(lTimetemp, GetZheFanTime2(nTempTrainNumber, StationInf(nStatemp).sStationName, "立即折返", ""))

        '                    ElseIf .TrainReturnStyle(2) = "站后折返" Then

        '                        DealKeCheArrival = TimeAdd(lTimetemp, GetZheFanTime2(nTempTrainNumber, StationInf(nStatemp).sStationName, "立即折返", ""))

        '                    Else
        '                        .StopLine(nEstation) = ""
        '                        DealKeCheArrival = TimeAdd(lTimetemp, lEndTchTime(nTempTrainNumber))
        '                        '                        End If
        '                End With
        '            End If
        '        ElseIf TrainInf(nTempTrainNumber).TrainClassCal = 12 Then
        '            With TrainInf(nTempTrainNumber)
        '                If .TrainContinue(2) <> 0 Then
        '                    If TrainInf(.TrainContinue(2)).TrainPuorNot <> 0 Then
        '                        .StopLine(nEstation) = _
        '                            TrainInf(.TrainContinue(2)).StopLine(nEstation)
        '                        DealKeCheArrival = TrainInf(.TrainContinue(2)).Starting(nEstation)
        '                    Else
        '                        '.StopLine(nEstation) = ""
        '                        DealKeCheArrival = TimeAdd(lTimetemp, lEndTchTime(nTempTrainNumber))
        '                    End If
        '                Else
        '                    '.StopLine(nEstation) = ""
        '                    DealKeCheArrival = TimeAdd(lTimetemp, lEndTchTime(nTempTrainNumber))
        '                End If
        '            End With
        '        End If
        '    Else
        '        DealKeCheArrival = TimeAdd(lTimetemp, lEndTchTime(nTempTrainNumber))
        '    End If
        'End If
        'If TrainInf(nTrainNumbertemp).nChaRunDirection = 9999 Then
        '    TrainInf(nTrainNumbertemp).StopLine(nEstation) = TrainInf(nTempTrainNumber).StopLine(nEstation)
        'End If
    End Function

    Function nGuDaoStop(ByVal lATime As Long, ByVal lStime As Long, ByVal nTrnNum As Integer, _
    ByVal nLeaveSection As Integer, ByVal ntmpStation As Integer, ByVal nEnterSection As Integer, ByVal nPhFx As Integer) As Integer
        Dim nGdtemp As Integer
        Dim nUpDown As Integer, nTemp As Integer
        Dim nStatemp As Integer
        Dim sGuDaotemp(1) As String

        nUpDown = nDirection(nTrnNum)
        nStatemp = ntmpStation

        If TrainInf(nTrnNum).StopLine(nStatemp) = "" Or TrainInf(nTrnNum).StopLine(nStatemp) = "9999" Then
            nGdtemp = CheckStopLine(nTrnNum, lATime, lStime, nStatemp)
        Else
            nTemp = nLineIfCanStop(nTrnNum, nStatemp, TrainInf(nTrnNum).StopLine(nStatemp), lATime, lStime)
            If nTemp <> 0 Then
                nGdtemp = StopLineNum(nTrnNum, nStatemp, lATime, lStime)
            Else
                '不用反向股道
                nGdtemp = 1
                '当反向股道可用时，用下面代码
                'nGdtemp = nPanDuanTxFxZx(nFindLineNum(TrainInf(nTrnNum).StopLine(nStatemp), nStatemp), nTrnNum, nStatemp)
            End If
            '   End Select
        End If

        Select Case nGdtemp
            Case 0
                '停入正线
                nGuDaoStop = 4
                '检查时间间隔
            Case 1
                '停入同向股道
                nGuDaoStop = CheckStop(nTrnNum, lATime, nUpDown, nLeaveSection, nStatemp, nEnterSection)
            Case 2
                '停入反向股道
                nGuDaoStop = CheckStop(nTrnNum, lATime, nUpDown, nLeaveSection, nStatemp, nEnterSection)
        End Select
    End Function

    Sub DealIfEndStation(ByVal ntmpTrainNumber As Integer, ByVal ntmpStation As Integer, _
    ByVal ltmpArrivalTime As Long, ByVal ltmpStartTime As Long, ByVal ntmpEndStationKind As Integer)

        Select Case ntmpEndStationKind
            Case 1  '终到站
                With TrainInf(ntmpTrainNumber)
                    '               ' 站前
                    '                If .TrainReturnStyle(2) = "站前折返" Then
                    '                    If TrainInf(.trainreturn(2)).TrainPuorNot <> 0 Then
                    '                        sStime = TrainInf(.trainreturn(2)).Starting(ntmpStation)
                    '                        If TimeMinus(sStime, ltmpArrivalTime) < TimeMinus(ltmpArrivalTime, sStime) Then
                    '                            Record .trainreturn(2), ntmpStation, sStime, ltmpArrivalTime
                    '                            Record ntmpTrainNumber, ntmpStation, sStime, ltmpArrivalTime
                    '                        Else
                    '                            sTimes = nStartTchTime(ntmpTrainNumber)
                    '                            sStime = TimeAdd(ltmpStartTime, sTimes)
                    '                            Record ntmpTrainNumber, ntmpStation, sStime, ltmpArrivalTime
                    '                            sStime = TrainInf(.trainreturn(2)).Starting(ntmpStation)
                    '                            sStarTime = TimeMinus(sStime, sTimes)
                    '                            Record .trainreturn(2), ntmpStation, sStime, sStarTime
                    '                        End If
                    '                    Else
                    '                        sTimes = lEndTchTime(ntmpTrainNumber)
                    '                        sStime = TimeAdd(ltmpArrivalTime, sTimes)
                    '                        Record ntmpTrainNumber, ntmpStation, sStime, ltmpArrivalTime
                    '                    End If
                    '                Else
                    Record(ntmpTrainNumber, ntmpStation, ltmpStartTime, ltmpArrivalTime)
                    '                End If
                End With

            Case 3
                Record(ntmpTrainNumber, ntmpStation, TrainInf(ntmpTrainNumber).Starting(ntmpStation), ltmpArrivalTime)
            Case 2
                Record(ntmpTrainNumber, ntmpStation, ltmpStartTime, ltmpArrivalTime)
        End Select

    End Sub
    Function FindStopTime(ByVal nConflict As Integer, ByVal nTrnNum As Integer, ByVal lTime As Long, _
    ByVal nBstation As Integer, ByVal nUporDown As Integer, _
    ByVal nLeaveSection As Integer, ByVal nEnterSection As Integer, ByVal nPhFx As Integer) As Long
        'nTrnNum 本次列车编号，nStatemp当前车站，nUporDown上下行
        'nConflict交叉的种类，nPhFx铺画方式
        '寻找可移动的出发时间
        Dim nUporDowntemp As Integer, nIntertemp As Integer, nNumtemp As Integer, nNumtemp1 As Integer
        Dim nTxNumtemp As Integer
        Dim nFxNumtemp As Integer
        Dim ii As Integer, nTempSt As Integer
        Dim lDifftemp As Long, lTimetemp As Long
        Dim lTxMovetemp As Long, lFxMovetemp As Long
        Dim nStatemp As Integer
        If nUporDown = 1 Then
            nUporDowntemp = 2
            nStatemp = SectionInf(nLeaveSection).nQStation
        Else
            nUporDowntemp = 1
            nStatemp = SectionInf(nLeaveSection).nHStation
        End If
        FindStopTime = 0
        Select Case nPhFx
            Case 1
                Select Case nConflict
                    Case 1
                        '前后都不够，只考虑在后方站停车，并右移发车时刻至后行车后
                        If TxIntervalKind1(nTrnNum, nStatemp) >= 300 Then
                            FindStopTime = TimeAdd(lTime, TxDiffTime2(nTrnNum, nStatemp))
                        Else
                            If TxDiffTime2(nTrnNum, nStatemp) < FxDiffTime2(nTrnNum, nStatemp) Then
                                If TimeMinus(lTime, TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).Arrival(nStatemp)) <= TimeMinus(TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).Arrival(nStatemp), lTime) Then
                                    lDifftemp = TimeMinus(TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).Starting(nStatemp), lTime)
                                Else
                                    lDifftemp = TimeMinus(TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).Arrival(nStatemp), lTime)
                                End If
                                nTempSt = nFindHstaNum(nTrnNum, nStatemp, nUporDown)
                                lDifftemp = TimeAdd(TrainInf(nTrnNum).Starting(nTempSt), lDifftemp)
                            Else
                                If TimeMinus(lTime, TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).Arrival(nStatemp)) <= TimeMinus(TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).Arrival(nStatemp), lTime) Then
                                    lDifftemp = TimeMinus(TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).Starting(nStatemp), lTime)
                                Else
                                    lDifftemp = TimeMinus(TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).Arrival(nStatemp), lTime)
                                End If
                                nTempSt = nFindHstaNum(nTrnNum, nStatemp, nUporDown)
                                lDifftemp = lDifftemp + lRunTimeDifference(nTrnNum, TxDiffTrain2(nTrnNum, nStatemp), nTempSt, nStatemp)
                                lDifftemp = TimeAdd(TrainInf(nTrnNum).Starting(nTempSt), lDifftemp)
                            End If
                            FindStopTime = TimeAdd(lDifftemp, TimeQ(nTrnNum, nTempSt, nStatemp))
                        End If
                    Case 2
                        '前不够，考虑右移（对211、212、213考虑左移，左移不够时再考虑右移）；
                        '上述左右移都不够时，在该站停车，并右移发车时刻至后行车后
                        If TxDiffTime1(nTrnNum, nStatemp) < FxDiffTime1(nTrnNum, nStatemp) Then
                            lDifftemp = FxDiffTime1(nTrnNum, nStatemp)
                            nIntertemp = FxIntervalKind1(nTrnNum, nStatemp)
                            nNumtemp = FxDiffTrain1(nTrnNum, nStatemp)
                        Else
                            lDifftemp = TxDiffTime1(nTrnNum, nStatemp)
                            nIntertemp = TxIntervalKind1(nTrnNum, nStatemp)
                            nNumtemp = TxDiffTrain1(nTrnNum, nStatemp)
                        End If

                        FindStopTime = lDifftemp



                        ''                    nTempSt = FindSstaNum(nTrnNum, nStatemp, nBstation, nUporDown, nPhFx)
                        ''                    FindStopTime = lCanRightMoveStop(lDifftemp, nTrnNum, nStatemp, nTempSt, nIntertemp, lTime, nPhFx)
                        ''
                        ''                    If FindStopTime = 0 Then
                        ''                        FindStopTime = 86400 + lDifftemp
                        ''                    End If


                        'FindStopTime = 86400 + lDifftemp
                    Case 3
                        '后不够，左移
                        If TxDiffTime2(nTrnNum, nStatemp) < FxDiffTime2(nTrnNum, nStatemp) Then
                            lDifftemp = FxDiffTime2(nTrnNum, nStatemp)
                            nIntertemp = FxIntervalKind2(nTrnNum, nStatemp)
                            nNumtemp = FxDiffTrain2(nTrnNum, nStatemp)
                        Else
                            lDifftemp = TxDiffTime2(nTrnNum, nStatemp)
                            nIntertemp = TxIntervalKind2(nTrnNum, nStatemp)
                            nNumtemp = TxDiffTrain2(nTrnNum, nStatemp)
                            nUporDowntemp = nUporDown
                        End If
                        FindStopTime = lDifftemp + 1

                        If FindStopTime = 0 Then
                            Select Case nIntertemp
                                Case 7   '到通
                                    nTempSt = nFindHstaNum(nTrnNum, nStatemp, nUporDown)
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                    1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + TimeT(nTrnNum, nTempSt, nStatemp)
                                    FindStopTime = FindStopTime + TimeRun(nTrnNum, nTempSt, nStatemp)
                                    FindStopTime = FindStopTime - TimeRun(nNumtemp, nTempSt, nStatemp)
                                    FindStopTime = FindStopTime + nMoveStepTime
                                Case 6      '到到
                                    nTempSt = nFindHstaNum(nTrnNum, nStatemp, nUporDown)
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                    1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + TimeT(nTrnNum, nTempSt, nStatemp)
                                    FindStopTime = FindStopTime + TimeRun(nTrnNum, nTempSt, nStatemp)
                                    FindStopTime = FindStopTime - TimeRun(nNumtemp, nTempSt, nStatemp)
                                    FindStopTime = FindStopTime + nMoveStepTime
                                Case 2      '到发
                                    ii = 4   '发到
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                                        1, nLeaveSection, nStatemp, nEnterSection)
                                    If FindStopTime = 0 Then
                                        FindStopTime = lDifftemp
                                    End If
                                Case 9      '同到，对反到
                                    ii = 9  '对反到, 同到
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                                        1, nLeaveSection, nStatemp, nEnterSection)
                                Case 11      '同到，对反发
                                    ii = 10  '对反发, 同到
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                                        1, nLeaveSection, nStatemp, nEnterSection)
                                Case 13      '反到同通
                                    ii = 15  '同通反到
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                                        1, nLeaveSection, nStatemp, nEnterSection)
                                Case 13      '同到，对反向通过
                                    ii = 13  '同到，对反向通过
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                                        1, nLeaveSection, nStatemp, nEnterSection)

                                Case 110 '本次列车到达与后行列车到达本站
                                    ii = 102 '前行列车从本站出发与本次列车出发
                                    'FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    ' 1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    'FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                    '   2, nLeaveSection, nStatemp, nEnterSection)

                                    nTempSt = nFindHstaNum(nTrnNum, nStatemp, nUporDown)
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                    1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + TimeT(nTrnNum, nTempSt, nStatemp)
                                    FindStopTime = FindStopTime + TimeRun(nTrnNum, nTempSt, nStatemp)
                                    FindStopTime = FindStopTime - TimeRun(nNumtemp, nTempSt, nStatemp)
                                    FindStopTime = FindStopTime + nMoveStepTime

                                Case 111 '本次列车到达与后行列车本站出发
                                    ii = 108 '前行列车本站出发与本次列车到达本站
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                                        1, nLeaveSection, nStatemp, nEnterSection)
                                Case 112 '本次列车到达与后行列车通过本站
                                    ii = 103 '前行列车通过本站与本次列车出发
                                    'FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    '              1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    'FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                    '               2, nLeaveSection, nStatemp, nEnterSection)

                                    nTempSt = nFindHstaNum(nTrnNum, nStatemp, nUporDown)
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                    1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + TimeT(nTrnNum, nTempSt, nStatemp)
                                    FindStopTime = FindStopTime + TimeRun(nTrnNum, nTempSt, nStatemp)
                                    FindStopTime = FindStopTime - TimeRun(nNumtemp, nTempSt, nStatemp)
                                    FindStopTime = FindStopTime + nMoveStepTime
                                Case 207
                                    ii = 207
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                                        1, nLeaveSection, nStatemp, nEnterSection)
                                Case 209
                                    ii = 201              'ii = 214  03.7.19
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                                        1, nLeaveSection, nStatemp, nEnterSection)
                                Case 216
                                    ii = 215
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                                        1, nLeaveSection, nStatemp, nEnterSection)
                                Case 208
                                    ii = 201              'ii = 214  03.7.19
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                                        1, nLeaveSection, nStatemp, nEnterSection)
                                Case 300
                                    FindStopTime = TxDiffTime2(nTrnNum, nStatemp)
                                Case 230
                                    ii = 230
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                                        1, nLeaveSection, nStatemp, nEnterSection)
                                Case 231
                                    ii = 231
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                                        1, nLeaveSection, nStatemp, nEnterSection)
                                Case 240
                                    ii = 240
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + nMoveStepTime 'lIntervalTime(nNumtemp, nTrnNum, ii, _
                                    '    1, nLeaveSection, nEnterSection)
                                Case 241
                                    ii = 241
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                                        1, nLeaveSection, nStatemp, nEnterSection)
                                Case 242
                                    ii = 232
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                                        1, nLeaveSection, nStatemp, nEnterSection)
                                Case 243
                                    ii = 233
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                                        1, nLeaveSection, nStatemp, nEnterSection)
                                Case 201
                                    ii = 201
                                    FindStopTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                                    FindStopTime = FindStopTime + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                                        1, nLeaveSection, nStatemp, nEnterSection)
                                Case Else
                                    FindStopTime = lDifftemp + nMoveStepTime
                                    'Stop
                            End Select

                            FindStopTime = -FindStopTime
                        End If
                    Case 4 '股道不够
                        nNumtemp = 0
                        nNumtemp1 = 0
                        nFxNumtemp = nTrnNum
                        nTxNumtemp = nTrnNum
                        lTxMovetemp = lTime
                        lFxMovetemp = lTime
                        lTimetemp = lTime
                        nTempSt = nFindHstaNum(nTrnNum, nStatemp, nUporDown)
                        If Right(StationInf(nStatemp).sStationProp, 3) = "XLS" Then
                            FindStopTime = 30
                        Else
                            Do While nNumtemp = 0 Or nNumtemp1 = 0
                                If nNumtemp = 0 Then
                                    nTxNumtemp = AFTER(nTxNumtemp, lTxMovetemp, nStatemp, nUporDown, 1, 0, _
                                                "", "", "")
                                    If nTxNumtemp <> nTrnNum Then
                                        If TrainInf(nTxNumtemp).Arrival(nStatemp) = TrainInf(nTxNumtemp).Starting(nStatemp) Then
                                            lTxMovetemp = TrainInf(nTxNumtemp).Arrival(nStatemp)
                                            If TimeMinus(lTxMovetemp, lTimetemp) >= TimeMinus(lFxMovetemp, lTimetemp) And nNumtemp1 <> 0 Then
                                                nNumtemp = nNumtemp1
                                                lTxMovetemp = lFxMovetemp
                                            Else
                                                nNumtemp = 0
                                            End If
                                        Else
                                            lTxMovetemp = TrainInf(nTxNumtemp).Starting(nStatemp)
                                            nNumtemp = nTxNumtemp
                                        End If
                                    Else
                                        nNumtemp = nTxNumtemp
                                    End If
                                End If

                                nNumtemp1 = 1


                                If nNumtemp1 = 0 Then
                                    nFxNumtemp = AFTER(nFxNumtemp, lFxMovetemp, nStatemp, nUporDowntemp, 1, 0, _
                                                "", "", "")
                                    If nFxNumtemp <> nTrnNum Then
                                        If TrainInf(nFxNumtemp).Arrival(nStatemp) = TrainInf(nFxNumtemp).Starting(nStatemp) Then
                                            lFxMovetemp = TrainInf(nFxNumtemp).Arrival(nStatemp)
                                            If TimeMinus(lFxMovetemp, lTimetemp) >= TimeMinus(lTxMovetemp, lTimetemp) And nNumtemp <> 0 Then
                                                nNumtemp1 = nNumtemp
                                                lFxMovetemp = lTxMovetemp
                                            Else
                                                nNumtemp1 = 0
                                            End If
                                        Else
                                            lFxMovetemp = TrainInf(nFxNumtemp).Starting(nStatemp)
                                            nNumtemp1 = nFxNumtemp
                                        End If
                                    Else
                                        nNumtemp1 = nFxNumtemp
                                    End If
                                End If
                            Loop
                            ''If TimeMinus(lTxMovetemp, lTimetemp) >= TimeMinus(lFxMovetemp, lTimetemp) Then
                            ''    FindStopTime = TimeMinus(lFxMovetemp, lTime)
                            ''Else
                            FindStopTime = TimeMinus(lTxMovetemp, lTime)
                            ''End If
                            FindStopTime = FindStopTime + TimeT(nTrnNum, nTempSt, nStatemp)
                        End If
                End Select
            Case 2
                Select Case nConflict
                    Case 1
                        '前后都不够，只考虑在前方站停车，并左移到达时刻至前行车前
                        If TxIntervalKind1(nTrnNum, nStatemp) >= 300 Then
                            FindStopTime = TimeMinus(lTime, TxDiffTime2(nTrnNum, nStatemp))
                        Else
                            FindStopTime = TimeMinus(lQConflictDao(nTrnNum, nStatemp), 30)
                        End If
                    Case 2
                        '前不够，考虑右移（对211、212、213考虑右移，右移不够时再考虑左移）；
                        '上述右移都不够时，左移到达时刻至前行车前

                        If TxDiffTime1(nTrnNum, nStatemp) < FxDiffTime1(nTrnNum, nStatemp) Then
                            lDifftemp = FxDiffTime1(nTrnNum, nStatemp)
                            nIntertemp = FxIntervalKind1(nTrnNum, nStatemp)
                            nNumtemp = FxDiffTrain1(nTrnNum, nStatemp)
                        Else
                            lDifftemp = TxDiffTime1(nTrnNum, nStatemp)
                            nIntertemp = TxIntervalKind1(nTrnNum, nStatemp)
                            nNumtemp = TxDiffTrain1(nTrnNum, nStatemp)
                        End If
                        If nIntertemp = 211 Or nIntertemp = 212 Or nIntertemp = 213 Then
                            ii = 209
                            lDifftemp = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                        1, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                            lDifftemp = lDifftemp + lIntervalTime(nNumtemp, nTrnNum, ii, _
                                        1, nLeaveSection, nStatemp, nEnterSection)
                        End If
                        'FindStopTime = lCanRightMoveStop(lDifftemp, nTrnNum, nStatemp, 0, nIntertemp, lTime, nPhFx)
                        If FindStopTime = 0 Then
                            FindStopTime = TimeMinus(lQConflictDao(nTrnNum, nStatemp), 30)
                            FindStopTime = TimeMinus(lTime, FindStopTime)
                        End If
                    Case 3
                        '后不够，左移
                        If TxDiffTime2(nTrnNum, nStatemp) < FxDiffTime2(nTrnNum, nStatemp) Then
                            lDifftemp = FxDiffTime2(nTrnNum, nStatemp)
                            nIntertemp = FxIntervalKind2(nTrnNum, nStatemp)
                            nNumtemp = FxDiffTrain2(nTrnNum, nStatemp)
                        Else
                            lDifftemp = TxDiffTime2(nTrnNum, nStatemp)
                            nIntertemp = TxIntervalKind2(nTrnNum, nStatemp)
                            nNumtemp = TxDiffTrain2(nTrnNum, nStatemp)
                            nUporDowntemp = nUporDown
                        End If
                        FindStopTime = lCanLeftMoveStop(lDifftemp, nTrnNum, nStatemp, 0, nIntertemp, lTime, nPhFx)
                        If FindStopTime = 0 Then
                            FindStopTime = TimeMinus(lQConflictDao(nTrnNum, nStatemp), 30)
                            FindStopTime = TimeMinus(lTime, FindStopTime)
                        End If
                    Case 4
                        nNumtemp = 0
                        nNumtemp1 = 0
                        nFxNumtemp = nTrnNum
                        nTxNumtemp = nTrnNum
                        lTxMovetemp = lTime
                        lFxMovetemp = lTime
                        lTimetemp = TrainInf(nTrnNum).Starting(nStatemp)
                        nTempSt = nFindQstaNum(nTrnNum, nStatemp, nUporDown)
                        Do While nNumtemp = 0 Or nNumtemp1 = 0
                            If nNumtemp = 0 Then
                                nTxNumtemp = BEFORE(nTxNumtemp, lTxMovetemp, nStatemp, nUporDown, 0, 0, _
                                            "", "", "")
                                If nTxNumtemp <> nTrnNum Then
                                    lTxMovetemp = TrainInf(nTxNumtemp).Arrival(nStatemp)
                                    If TrainInf(nTxNumtemp).Arrival(nStatemp) = TrainInf(nTxNumtemp).Starting(nStatemp) Then
                                        If TimeMinus(lTimetemp, lTxMovetemp) >= TimeMinus(lTimetemp, lFxMovetemp) And nNumtemp1 <> 0 Then
                                            nNumtemp = nNumtemp1
                                            lTxMovetemp = lFxMovetemp
                                        Else
                                            nNumtemp = 0
                                        End If
                                    Else
                                        nNumtemp = nTxNumtemp
                                    End If
                                Else
                                    nNumtemp = nTxNumtemp
                                End If
                            End If
                            If nNumtemp1 = 0 Then
                                nFxNumtemp = BEFORE(nFxNumtemp, lFxMovetemp, nStatemp, nUporDowntemp, 0, 0, _
                                            "", "", "")
                                If nFxNumtemp <> nTrnNum Then
                                    lFxMovetemp = TrainInf(nFxNumtemp).Arrival(nStatemp)
                                    If TrainInf(nFxNumtemp).Arrival(nStatemp) = TrainInf(nFxNumtemp).Starting(nStatemp) Then
                                        If TimeMinus(lTimetemp, lFxMovetemp) >= TimeMinus(lTimetemp, lTxMovetemp) And nNumtemp <> 0 Then
                                            nNumtemp1 = nNumtemp
                                            lFxMovetemp = lTxMovetemp
                                        Else
                                            nNumtemp1 = 0
                                        End If
                                    Else
                                        nNumtemp1 = nFxNumtemp
                                    End If
                                Else
                                    nNumtemp1 = nFxNumtemp
                                End If
                            End If
                        Loop
                        If lTxMovetemp <> lTimetemp And lFxMovetemp <> lTimetemp Then
                            If TimeMinus(lTimetemp, lTxMovetemp) >= TimeMinus(lTimetemp, lFxMovetemp) Then
                                FindStopTime = TimeMinus(lTime, lFxMovetemp)
                            Else
                                FindStopTime = TimeMinus(lTime, lTxMovetemp)
                            End If
                        ElseIf lTxMovetemp = lTimetemp And lFxMovetemp <> lTimetemp Then
                            FindStopTime = TimeMinus(lTime, lFxMovetemp)
                        ElseIf lTxMovetemp <> lTimetemp And lFxMovetemp = lTimetemp Then
                            FindStopTime = TimeMinus(lTime, lTxMovetemp)
                        End If
                        FindStopTime = FindStopTime + nMoveStepTime
                End Select
        End Select
    End Function
    Function lNeedStop(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal nStationKind As Integer) As Long
        Dim i As Integer
        lNeedStop = 0
        Select Case nStationKind
            Case 0 '本站是始发站
                If TrainInf(nTrnNum).TrainKind = "客车" Then
                    lNeedStop = GetCurTrainStopTimeAtStation(TrainInf(nTrnNum).sJiaoLuName, TrainInf(nTrnNum).sStopSclaeName, TrainInf(nTrnNum).ComeStation)
                End If
            Case 1 '本站是终到站
                If TrainInf(nTrnNum).TrainKind = "客车" Then
                    lNeedStop = GetCurTrainStopTimeAtStation(TrainInf(nTrnNum).sJiaoLuName, TrainInf(nTrnNum).sStopSclaeName, TrainInf(nTrnNum).NextStation)
                End If
            Case 2 '本站是交出站
                If TrainInf(nTrnNum).TrainKind = "客车" Then
                    If Right(StationInf(nStatemp).sStationProp, 3) = "KJZ" Then
                        lNeedStop = 8 * 60
                    Else
                        For i = 1 To TrainInf(nTrnNum).NumStop
                            If StationInf(nStatemp).sStationName = TrainInf(nTrnNum).StopStation(i) Then
                                lNeedStop = GetCurTrainStopTimeAtStation(TrainInf(nTrnNum).sJiaoLuName, TrainInf(nTrnNum).sStopSclaeName, StationInf(nStatemp).sStationName) 'TrainInf(nTrnNum).StopTime(i)
                                Exit For
                            End If
                        Next i
                    End If
                Else
                    If Right(StationInf(nStatemp).sStationProp, 3) = "QDZ" _
                        Or Right(StationInf(nStatemp).sStationProp, 3) = "BZZ" Then
                        If TrainInf(nTrnNum).TrainClassCal = 30 Or TrainInf(nTrnNum).TrainClassCal = 31 Then
                            '小运转
                            lNeedStop = 15 * 60
                        Else
                            lNeedStop = 25 * 60
                        End If
                    Else
                        For i = 1 To TrainInf(nTrnNum).NumStop
                            If StationInf(nStatemp).sStationName = TrainInf(nTrnNum).StopStation(i) Then
                                lNeedStop = GetCurTrainStopTimeAtStation(TrainInf(nTrnNum).sJiaoLuName, TrainInf(nTrnNum).sStopSclaeName, StationInf(nStatemp).sStationName) 'TrainInf(nTrnNum).StopTime(i)
                                Exit For
                            End If
                        Next i
                    End If
                End If
            Case 3
                lNeedStop = 30
            Case Else
                For i = 1 To TrainInf(nTrnNum).NumStop
                    If StationInf(nStatemp).sStationName = TrainInf(nTrnNum).StopStation(i) Then
                        lNeedStop = GetCurTrainStopTimeAtStation(TrainInf(nTrnNum).sJiaoLuName, TrainInf(nTrnNum).sStopSclaeName, StationInf(nStatemp).sStationName) 'TrainInf(nTrnNum).StopTime(i)
                        Exit For
                    End If
                Next i
        End Select
    End Function

    Function nChaTrnNum(ByVal nTrnNum As Integer) As Integer
        '查找该车是否在该站反向运行
        '0 表示没有反向运行,其他值表示反向运行的列车编号
        Dim i As Integer, nBeg As Integer, nEnd As Integer

        With TrainInf(nTrnNum)
            nChaTrnNum = 0
            If .nChaRunDirection <> 0 Then
                'If .nChaRunDirection <> 0 And nFindAnotherTrain(nTrnNum) = 0 Then
                nChaTrnNum = .nChaRunDirection
                If nChaTrnNum = 9999 Then
                    If nTrnNum Mod 2 <> 0 Then
                        nBeg = 2
                        nEnd = UBound(TrainInf)
                    Else
                        nBeg = 1
                        nEnd = UBound(TrainInf)
                    End If
                    For i = nBeg To nEnd Step 2
                        If TrainInf(i).Train <> "" Then
                            If TrainInf(i).nChaRunDirection = nTrnNum Then
                                nChaTrnNum = i
                                Exit For
                            End If
                        End If
                    Next i
                End If
            End If
        End With
    End Function

    Function CheckStopLine(ByVal nTrnNum As Integer, ByVal lATime As Long, ByVal lStime As Long, ByVal nStatemp As Integer) As Integer
        'num本次列车编号，stime当前时间，qstatemp前一车站
        '检查车站股道条件
        ' If StationInf(nStatemp).sStationName = "龙阳路" Then Stop
        Select Case StopLineNum(nTrnNum, nStatemp, lATime, lStime)
            Case 1
                CheckStopLine = 1 '停入同向股道
            Case 2
                CheckStopLine = 2 '停入反向股道
            Case 3
                CheckStopLine = 3 '不能停车
        End Select
    End Function
    Function StopLineNum(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal lAtimetemp As Long, ByVal lStimetemp As Long) As Integer
        '确定停车股道
        'ntrnnum列车编号，nstatemp当前车站，latimetemp到达时间,lstimetemp出发时间,nchektemp检查标志
        Dim i As Integer
        Dim nGdtemp() As Integer
        Dim nChecktemp1 As Integer

        '  If nTrnNum = 27 Then Stop
        '对于地铁，可指定股道使用方式，下行为1道，上行为2道，不加以检查判断

        'If TrainInf(nTrnNum).StopLine(nStatemp) = "" Or TrainInf(nTrnNum).StopLine(nStatemp) = "0" Then
        '        If nTrnNum Mod 2 = 0 Then
        '            TrainInf(nTrnNum).StopLine(nStatemp) = 2
        '        Else
        '            TrainInf(nTrnNum).StopLine(nStatemp) = 1
        '        End If
        '        StopLineNum = 1
        'End If

        ReDim nGuDaoTrain(0)
        ReDim nGdtemp(0)
        GuDaoOccupiedbyTrain(nTrnNum, nStatemp, lAtimetemp, lStimetemp)
        StopLineNum = 0
        ReDim KeYongGD(0)
        Call SeekKeYongGD(nTrnNum, nStatemp) '找满足条件的股道
        Dim tmpNGuDaoOcccupy As Integer
        If UBound(KeYongGD) > 0 Then '找到满足条件的,下面判断股道是否被占用
            For i = 1 To UBound(KeYongGD)
                tmpNGuDaoOcccupy = nGudaoOccupy(nTrnNum, nStatemp, KeYongGD(i))
                If tmpNGuDaoOcccupy = 0 Then
                    nChecktemp1 = nGuDaoFaDaoOccupy(nTrnNum, nStatemp, KeYongGD(i), lAtimetemp, lStimetemp)
                    If nChecktemp1 = 0 Then
                        '该股道空闲，将列车停入该股道
                        TrainInf(nTrnNum).StopLine(nStatemp) = StationInf(nStatemp).sStLineNo(KeYongGD(i))
                        StopLineNum = 1
                        '                    If TrainInf(nTrnNum).NextStation = StationInf(nStatemp).sStationName Then
                        '                        If TrainInf(nTrnNum).TrainReturnStyle(2) = "站前折返" Then
                        '                            StopLineNum = 2
                        '                        End If
                        '                    End If
                        Exit For
                    End If
                End If
            Next i
        Else
            If SystemPara.SystemStyle = "地铁" Or SystemPara.SystemStyle = "磁浮" Then
                If nTrnNum Mod 2 = 0 Then
                    TrainInf(nTrnNum).StopLine(nStatemp) = 2 'StationInf(nStatemp).sStLineNo(KeYongGD(i))
                Else
                    TrainInf(nTrnNum).StopLine(nStatemp) = 1 'StationInf(nStatemp).sStLineNo(KeYongGD(i))
                End If
                StopLineNum = 1
            Else
                'Stop
            End If
            '        If nTrnNum Mod 2 = 0 Then
            '           
            '        Else
            '            StopLineNum = 2
            '        End If
            '        Stop
        End If
        If StopLineNum = 0 Then
            StopLineNum = 3
        End If

    End Function

    Function nLineIfCanStop(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal sLinetemp As String, _
        ByVal lAtimetemp As Long, ByVal lStimetemp As Long) As Integer
        '确定停车股道
        'ntrnnum列车编号，nstatemp当前车站，latimetemp到达时间,lstimetemp出发时间,nchektemp检查标志
        Dim i As Integer
        Dim nTemp As Integer
        nLineIfCanStop = 0
        For i = 1 To StationInf(nStatemp).nStLineNum
            If StationInf(nStatemp).sStLineNo(i) = sLinetemp Then
                nTemp = i
                Exit For
            End If
        Next i
        If StationInf(nStatemp).nStLineUse(nTemp) < 6000 Then
            If nPassOccupy(nTrnNum, nStatemp, sLinetemp, lAtimetemp, lStimetemp) = 0 Then '该正线未被占用,在停站时间内无列车通过
                nLineIfCanStop = 0
            Else
                nLineIfCanStop = 1
            End If
        Else
            For i = 1 To UBound(TrainInf)
                '检查停站列车，确定当前时间是否落入停站列车的停车时间范围内
                If TrainInf(i).Train <> "" Then
                    If TrainInf(i).Train <> TrainInf(nTrnNum).Train Then
                        ' If TrainInf(i).Train <> TrainInf(nTrnNum).Train And TrainInf(i).TrainContinue(1) <> nTrnNum _
                        '  And TrainInf(i).TrainContinue(2) <> nTrnNum And TrainInf(i).TrainReturn(1) <> nTrnNum _
                        '  And TrainInf(i).TrainReturn(2) <> nTrnNum Then
                        If TrainInf(i).Arrival(nStatemp) <> TrainInf(i).Starting(nStatemp) Then
                            Select Case TimeCheck(lAtimetemp, lStimetemp, nStatemp, i)
                                Case 1
                                    '当前列车到达时刻落入已有列车停车范围
                                    If TrainInf(i).StopLine(nStatemp) = sLinetemp Then
                                        nLineIfCanStop = 1
                                        Exit For
                                    End If
                                Case 2
                                    '当前列车停车时间包含已有列车停车时间
                                    If TrainInf(i).StopLine(nStatemp) = sLinetemp Then
                                        nLineIfCanStop = 1
                                        Exit For
                                    End If
                            End Select
                        End If
                    End If
                    End If
            Next i
        End If
    End Function
    Function nPanDuanTxFxZx(ByVal nGuDaoNumbertemp As Integer, ByVal nTrnNum As Integer, _
        ByVal nStatemp As Integer) As Integer
        'Dim j As Integer
        'Dim nTemp1 As Integer, nTemp2 As Integer
        'Dim bTemp As Boolean

        nPanDuanTxFxZx = 0
        'With StationInf(nStatemp)

        '    If .nStLineUse(nGuDaoNumbertemp) >= 6000 Then
        '        If Left(.sStationProp, 1) = "F" Then
        '            nTemp1 = nFindTrainFenChaNum(nTrnNum, nStatemp)
        '            nTemp2 = nFindFenChaZhan(nStatemp)

        '            '江编的代码，判断分岔站可停的股道号
        '            Dim Q As Integer
        '            For j = 1 To UBound(.sGDFromSta)
        '                bTemp = False
        '                If .sGDFromSta(j) = TrainInf(nTrnNum).Way3(nTemp1) And .sGDToSta(j) = TrainInf(nTrnNum).Way2(nTemp1) Then
        '                    Call GetGuDaoYongTu(.sGDDaoFaBasicJinLu(j))
        '                    If UBound(FenChaZhanGuDaoUse) > 0 Then
        '                        For Q = 1 To UBound(FenChaZhanGuDaoUse)
        '                            If FenChaZhanGuDaoUse(Q) = .sStLineNo(nGuDaoNumbertemp) Then
        '                                nPanDuanTxFxZx = 1
        '                                bTemp = True
        '                                Exit For
        '                            End If
        '                        Next Q
        '                        If bTemp = True Then Exit For
        '                    End If

        '                    Call GetGuDaoYongTu(.sGDDaoFaKeXuanJinLu(j))
        '                    If UBound(FenChaZhanGuDaoUse) > 0 Then
        '                        For Q = 1 To UBound(FenChaZhanGuDaoUse)
        '                            If FenChaZhanGuDaoUse(Q) = .sStLineNo(nGuDaoNumbertemp) Then
        '                                nPanDuanTxFxZx = 2
        '                                bTemp = True
        '                                Exit For
        '                            End If
        '                        Next Q
        '                        If bTemp = True Then Exit For
        '                    End If
        '                End If
        '            Next j

        '            If nPanDuanTxFxZx = 0 Then
        '                If ((nTrnNum Mod 2) = (Left(CStr(.nStLineUse(nGuDaoNumbertemp)), 1) Mod 2)) Then
        '                    nPanDuanTxFxZx = 1
        '                Else
        '                    nPanDuanTxFxZx = 2
        '                End If
        '            End If
        '        Else
        '            If ((nTrnNum Mod 2) = (Left(CStr(.nStLineUse(nGuDaoNumbertemp)), 1) Mod 2)) Then
        '                nPanDuanTxFxZx = 1
        '            Else
        '                nPanDuanTxFxZx = 2
        '            End If
        '        End If
        '    Else
        '        nPanDuanTxFxZx = 4
        '    End If
        'End With
    End Function

    Function nFindLineNum(ByVal sLinetemp As String, ByVal nStatemp As Integer) As Integer
        Dim i As Integer

        For i = 1 To StationInf(nStatemp).nStLineNum
            If sLinetemp = StationInf(nStatemp).sStLineNo(i) Then
                nFindLineNum = i
                Exit For
            End If
        Next i
    End Function

    Function CheckStart(ByVal nTrnNum As Integer, ByVal lStime As Long, ByVal nUporDown As Integer, _
    ByVal nLeaveSection As Integer, ByVal ntmpStation As Integer, ByVal nEnterSection As Integer) As Integer
        'num本次列车编号，stime当前时间，statemp当前车站
        '检查发车条件，最后给出列车在该站的发车时间

        Dim nStatemp As Integer

        nStatemp = ntmpStation
        TXFZ(nTrnNum, lStime, nUporDown, nLeaveSection, nStatemp, nEnterSection)
        CheckStart = nCheckJianGe(nTrnNum, nStatemp)
    End Function

    Function nBeginDataStation(ByVal nDataField As Integer) As Integer
        If nDataField = 1 Then
            nBeginDataStation = 1
        ElseIf nDataField >= UBound(DataReadInf) Then
            nBeginDataStation = 1
        Else
            nBeginDataStation = DataReadInf(nDataField).NowStationBegin
        End If
    End Function

    Function nEndDataStation(ByVal nDataField As Integer) As Integer
        If nDataField = 1 Then
            nEndDataStation = DataReadInf(nDataField).NowStationEnd
        ElseIf nDataField >= UBound(DataReadInf) Then
            nEndDataStation = UBound(StationInf)
        Else
            nEndDataStation = DataReadInf(nDataField).NowStationEnd
        End If
    End Function

    Function lMoveStartTime(ByVal nTrnNum As Integer, ByVal nNumtemp As Integer, ByVal nIntertemp As Integer, _
    ByVal lDifftemp As Long, ByVal nConflict As Integer, ByVal nUpDown As Integer, _
    ByVal nLeaveSection As Integer, ByVal nEnterSection As Integer) As Long

        Dim nTemp As Integer, nUporDowntemp As Integer
        Dim nStatemp As Integer

        If nUpDown = 1 Then
            nUporDowntemp = 2
            nStatemp = SectionInf(nEnterSection).nHStation
        Else
            nUporDowntemp = 1
            nStatemp = SectionInf(nEnterSection).nQStation
        End If
        Select Case nIntertemp
            Case 1      '发发
                nTemp = 1   '发发
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 3      '发通
                nTemp = 0   '通发
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 4      '发到
                nTemp = 2   '到发
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 10      '同发，对反向到达；反发，对同向到达
                nTemp = 11  '同到，对反向出发；反到，对同向出发
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 12      '同发，对反向发车；反发，对同向发车
                nTemp = 12  '同发，对反向出发；反发，对同向出发
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 14      '同发，对反向通过；反发，对同向通过
                nTemp = 16  '对反向通过，同发；对同向通过，反发
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 104
                nTemp = 101
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 105
                nTemp = 102
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 106
                nTemp = 103
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 201
                nTemp = 208
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 203
                If nConflict = 3 Then
                    lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                Else
                    lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                End If
            Case 204
                If nConflict = 3 Then
                    lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                Else
                    lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                End If
            Case 207
                nTemp = 207
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 210
                nTemp = 210
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 250
                nTemp = 250
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 251
                nTemp = 251
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 260
                nTemp = 260
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + nMoveStepTime 'lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                ' 2, nLeaveSection, nEnterSection)
            Case 261
                nTemp = 251
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 262
                nTemp = 252
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 263
                nTemp = 253
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case 500
                nTemp = 500
                lMoveStartTime = lIntervalTime(nNumtemp, nTrnNum, nIntertemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection) - lDifftemp
                lMoveStartTime = lMoveStartTime + lIntervalTime(nNumtemp, nTrnNum, nTemp, _
                                    2, nLeaveSection, nStatemp, nEnterSection)
            Case Else
                lMoveStartTime = nMoveStepTimeInTdrawline
                'Stop
        End Select
        If lMoveStartTime = 0 Then
            lMoveStartTime = lDifftemp
        End If
    End Function

    Function lCanRightMoveStart(ByVal lNeedMoveTime As Long, ByVal nTrainNumber As Integer, ByVal nStationtemp As Integer, _
    ByVal nStopStation As Integer, ByVal lNowTime As Long, ByVal nPhFx As Integer) As Long
        Dim lTxMovetemp As Long, lFxMovetemp As Long
        Dim nTxNumtemp As Integer, nFxNumtemp As Integer
        Dim nStationHtemp As Integer, nTrainNumbertemp As Integer, nTemp As Integer, nUpDowntemp As Integer
        Dim bTemp As Boolean
        Dim lTemp As Long

        Select Case nPhFx
            Case 1
                lCanRightMoveStart = 0
                'If StationInf(nStationTemp).sStationName <> TrainInf(nTrainNumber).ComeStation Then
                If nCanRightMove(nStationtemp) = 0 Then
                    lFxMovetemp = FxDiffTime2(nTrainNumber, nStationtemp)
                    nFxNumtemp = FxDiffTrain2(nTrainNumber, nStationtemp)
                    lTxMovetemp = TxDiffTime2(nTrainNumber, nStationtemp)
                    nTxNumtemp = TxDiffTrain2(nTrainNumber, nStationtemp)
                    If lNeedMoveTime <= -lTxMovetemp And lNeedMoveTime <= -lFxMovetemp Then
                        lCanRightMoveStart = lNeedMoveTime
                    ElseIf lNeedMoveTime <= -lTxMovetemp And lNeedMoveTime > -lFxMovetemp Then
                        If TrainInf(nFxNumtemp).TrainClassCal > TrainInf(nTrainNumber).TrainClassCal Then
                            lCanRightMoveStart = lNeedMoveTime
                            FinInfTrn(nFxNumtemp)
                        End If
                    ElseIf lNeedMoveTime > -lTxMovetemp And lNeedMoveTime <= -lFxMovetemp Then
                        If TrainInf(nTxNumtemp).TrainClassCal > TrainInf(nTrainNumber).TrainClassCal Then
                            lCanRightMoveStart = lNeedMoveTime
                            FinInfTrn(nTxNumtemp)
                        End If
                    ElseIf lNeedMoveTime > -lTxMovetemp And lNeedMoveTime > -lFxMovetemp Then
                        If TrainInf(nTxNumtemp).TrainClassCal > TrainInf(nTrainNumber).TrainClassCal _
                            And TrainInf(nFxNumtemp).TrainClassCal > TrainInf(nTrainNumber).TrainClassCal Then
                            lCanRightMoveStart = lNeedMoveTime
                            FinInfTrn(nTxNumtemp)
                            FinInfTrn(nFxNumtemp)
                        End If
                    End If
                End If
                'End If
            Case 2
                lCanRightMoveStart = 0
                nStationHtemp = nStationtemp
                bTemp = True
                nTrainNumbertemp = nTrainNumber
                If nCanLeftMove(nStopStation) = 0 Then
                    If StationInf(nStopStation).sStationName = TrainInf(nTrainNumber).EndStation Then
                        lTemp = lNeedStop(nTrainNumber, nStopStation, 1)
                    Else
                        lTemp = lNeedStop(nTrainNumber, nStopStation, 4)
                    End If
                    If (TimeMinus(TrainInf(nTrainNumber).Starting(nStopStation), TrainInf(nTrainNumber).Arrival(nStopStation)) _
                        - lTemp) > lNeedMoveTime Then
                        '前方停车站停站时间允许左移
                        Do While bTemp = True
                            If StationInf(nStationHtemp).sStationName = StationInf(nStopStation).sStationName Then
                                bTemp = False
                            End If
                            lFxMovetemp = FxDiffTime2(nTrainNumbertemp, nStationHtemp)
                            nFxNumtemp = FxDiffTrain2(nTrainNumbertemp, nStationHtemp)
                            lTxMovetemp = TxDiffTime2(nTrainNumbertemp, nStationHtemp)
                            nTxNumtemp = TxDiffTrain2(nTrainNumbertemp, nStationHtemp)
                            If lNeedMoveTime <= -lTxMovetemp And lNeedMoveTime <= -lFxMovetemp Then
                                lCanRightMoveStart = lNeedMoveTime
                            ElseIf lNeedMoveTime <= -lTxMovetemp And lNeedMoveTime > -lFxMovetemp Then
                                If TrainInf(nFxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal Then
                                    lCanRightMoveStart = lNeedMoveTime
                                    FinInfTrn(nFxNumtemp)
                                Else
                                    lCanRightMoveStart = 0
                                    Exit Do
                                End If
                            ElseIf lNeedMoveTime > -lTxMovetemp And lNeedMoveTime <= -lFxMovetemp Then
                                If TrainInf(nTxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal Then
                                    lCanRightMoveStart = lNeedMoveTime
                                    FinInfTrn(nTxNumtemp)
                                Else
                                    lCanRightMoveStart = 0
                                    Exit Do
                                End If
                            ElseIf lNeedMoveTime > -lTxMovetemp And lNeedMoveTime > -lFxMovetemp Then
                                If TrainInf(nTxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal _
                                    And TrainInf(nFxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal Then
                                    lCanRightMoveStart = lNeedMoveTime
                                    FinInfTrn(nTxNumtemp)
                                    FinInfTrn(nFxNumtemp)
                                Else
                                    lCanRightMoveStart = 0
                                    Exit Do
                                End If
                            End If
                            nTemp = nChaDirectionSt(nTrainNumber, nStationHtemp, 0)
                            If nTemp <> 0 Then
                                nStationHtemp = nTemp
                                nTrainNumbertemp = nChaTrnNum(nTrainNumbertemp)
                            Else
                                nTrainNumbertemp = nTrainNumber
                            End If
                            nUpDowntemp = nDirection(nTrainNumbertemp)
                            nStationHtemp = nFindQstaNum(nTrainNumbertemp, nStationHtemp, nUpDowntemp)
                            If StationInf(nStationHtemp).sStationName = TrainInf(nTrainNumbertemp).NextStation Then
                                nStationHtemp = nBstation
                            End If
                        Loop
                    End If
                End If
        End Select
    End Function

    Function AFTER(ByVal nTrnNum As Integer, ByVal lTimetemp As Long, ByVal nStaterecord As Integer, _
    ByVal nSdirection As Integer, ByVal nArriStar As Integer, ByVal nArrivalStart As Integer, _
    ByVal sFromGo As String, ByVal sSameorNot As String, ByVal sFGstation As String) As Integer
        'nTrnNum本次列车编号，timtemp当前时间，StateRecord当前车站
        'nSdirection方向，ArriStar到达或出发条件
        '找后行车

        'If StationInf(nStaterecord).sStationName = "" Then Stop

        Dim lRighttemp As Long, lLefttemp As Long, lTraintime As Long, lAfterTime As Long

        Dim nTrainNum As Integer, nAfterTrain As Integer
        Dim jj As Integer, i As Integer
        Dim nBegintemp As Integer, nEndtemp As Integer

        nBegintemp = nSdirection
        nEndtemp = UBound(TrainInf)
        nAfterTrain = 0
        AFTER = nTrnNum
        lRighttemp = lTimetemp
        jj = 0
        Do While nAfterTrain = 0 And jj <= 8 '8当无车的时候保证循环24小时
            If lRighttemp = 86400 Then '21
                lRighttemp = 0
            End If
            lLefttemp = lRighttemp
            lRighttemp = lLefttemp + 10800
            If lRighttemp > 86400 Then
                lRighttemp = 86400
            End If
            lAfterTime = lRighttemp
            If nArriStar = 0 Then '到达条件0
                For i = nBegintemp To nEndtemp Step 2
                    If TrainInf(i).Train <> "" Then
                        'If TrainInf(i).StartStation <> StationInf(nStaterecord).sStationName Then
                        nTrainNum = nTrainCompare(nTrnNum, i, nSdirection, _
                                    nArrivalStart, nStaterecord, sFromGo, sSameorNot, sFGstation)
                        If nTrainNum <> 0 Then
                            With TrainInf(nTrainNum)
                                If StationInf(nStaterecord).sStationName = .StartStation Then
                                    lTraintime = .Starting(nStaterecord)
                                Else
                                    lTraintime = .Arrival(nStaterecord)
                                End If
                                If lRighttemp >= lTraintime And lTraintime > lLefttemp Then
                                    If lTraintime <= lAfterTime Then
                                        If Trim(TrainInf(nTrnNum).Train) <> Trim(.Train) Then
                                            lAfterTime = lTraintime
                                            nAfterTrain = nTrainNum
                                        End If
                                    End If
                                End If
                            End With
                        End If
                        'End If
                    End If
                Next i
            ElseIf nArriStar = 1 Then '出发条件1
                For i = nBegintemp To nEndtemp Step 2
                    If TrainInf(i).Train <> "" Then
                        'If TrainInf(i).EndStation <> StationInf(nStaterecord).sStationName Then
                        nTrainNum = nTrainCompare(nTrnNum, i, nSdirection, _
                                     nArrivalStart, nStaterecord, sFromGo, sSameorNot, sFGstation)
                        If nTrainNum <> 0 Then
                            With TrainInf(nTrainNum)
                                'If StationInf(nStaterecord).sStationName = .EndStation Then
                                '    lTraintime = .Arrival(nStaterecord)
                                'Else
                                lTraintime = .Starting(nStaterecord)
                                'End If
                                If lRighttemp >= lTraintime And lTraintime > lLefttemp Then
                                    If lTraintime <= lAfterTime Then
                                        If Trim(TrainInf(nTrnNum).Train) <> Trim(.Train) Then
                                            lAfterTime = lTraintime
                                            nAfterTrain = nTrainNum
                                        End If
                                    End If
                                End If
                            End With
                        End If
                        'End If
                    End If
                Next i
            End If
            jj = jj + 1
        Loop
        If nAfterTrain <> 0 Then
            AFTER = nAfterTrain
        End If
    End Function

    Function BEFORE(ByVal nTrnNum As Integer, ByVal lTimetemp As Long, ByVal nStaterecord As Integer, _
        ByVal nSdirection As Integer, ByVal nArriStar As Integer, ByVal nArrivalStart As Integer, _
        ByVal sFromGo As String, ByVal sSameorNot As String, ByVal sFGstation As String) As Integer
        'nTrnNum本次列车编号，timtemp当前时间，StateRecord当前车站
        'nSdirection方向，ArriStar到达或出发条件,nArrivalStart本次车是到、发
        'nFromGo来或往, sFGStation来或往的车站
        '找前行车

        'If StationInf(nStaterecord).sStationName = "" Then Stop

        Dim lRighttemp As Long, lLefttemp As Long, lTraintime As Long, lBeforeTime As Long
        Dim nTrainNum As Integer, nBeforeTrain As Integer
        Dim i As Integer, jj As Integer
        Dim nBegintemp As Integer, nEndtemp As Integer

        nBegintemp = nSdirection
        nEndtemp = UBound(TrainInf)
        nBeforeTrain = 0
        BEFORE = nTrnNum
        lLefttemp = lTimetemp
        jj = 0

        Do While nBeforeTrain = 0 And jj <= 8 '8当无车的时候保证循环24小时
            If lLefttemp = 0 Then
                lLefttemp = 86400
            End If
            lRighttemp = lLefttemp
            lLefttemp = lRighttemp - 10800 '3 hours
            If lLefttemp < 0 Then
                lLefttemp = 0
            End If
            lBeforeTime = lLefttemp
            If nArriStar = 0 Then '到达条件0
                For i = nBegintemp To nEndtemp Step 2
                    If TrainInf(i).Train <> "" Then
                        nTrainNum = nTrainCompare(nTrnNum, i, nSdirection, _
                                    nArrivalStart, nStaterecord, sFromGo, sSameorNot, sFGstation)
                        If nTrainNum <> 0 Then
                            With TrainInf(nTrainNum)
                                If StationInf(nStaterecord).sStationName = .StartStation Then
                                    lTraintime = .Starting(nStaterecord)
                                Else
                                    lTraintime = .Arrival(nStaterecord)
                                End If
                                If lTraintime >= lLefttemp And lTraintime <= lRighttemp Then
                                    If lTraintime >= lBeforeTime Then
                                        If Trim(TrainInf(nTrnNum).Train) <> Trim(.Train) Then
                                            lBeforeTime = lTraintime
                                            nBeforeTrain = nTrainNum
                                        End If
                                    End If
                                End If
                            End With
                        End If
                    End If
                Next i
            ElseIf nArriStar = 1 Then '出发条件1
                For i = nBegintemp To nEndtemp Step 2
                    If TrainInf(i).Train <> "" Then
                        nTrainNum = nTrainCompare(nTrnNum, i, nSdirection, _
                                    nArrivalStart, nStaterecord, sFromGo, sSameorNot, sFGstation)
                        If nTrainNum <> 0 Then
                            With TrainInf(nTrainNum)
                                If StationInf(nStaterecord).sStationName = .EndStation Then
                                    lTraintime = .Arrival(nStaterecord)
                                Else
                                    lTraintime = .Starting(nStaterecord)
                                End If
                                If lTraintime >= lLefttemp And lTraintime <= lRighttemp Then
                                    If lTraintime >= lBeforeTime Then
                                        If Trim(TrainInf(nTrnNum).Train) <> Trim(.Train) Then
                                            lBeforeTime = lTraintime
                                            nBeforeTrain = nTrainNum
                                        End If
                                    End If
                                End If
                            End With
                        End If
                    End If
                Next i
            End If
            jj = jj + 1
        Loop
        If nBeforeTrain <> 0 Then
            BEFORE = nBeforeTrain
        End If
    End Function

    Function lQConflictFa(ByVal ntmpTrnNum As Integer, ByVal ntmpStatemp As Integer) As Long
        Dim ntmpInterval As Integer, ntmpTrain As Integer

        If TxDiffTime1(ntmpTrnNum, ntmpStatemp) < FxDiffTime1(ntmpTrnNum, ntmpStatemp) Then
            ntmpInterval = FxIntervalKind1(ntmpTrnNum, ntmpStatemp)
            ntmpTrain = FxDiffTrain1(ntmpTrnNum, ntmpStatemp)
        Else
            ntmpInterval = TxIntervalKind1(ntmpTrnNum, ntmpStatemp)
            ntmpTrain = TxDiffTrain1(ntmpTrnNum, ntmpStatemp)
        End If
        Select Case ntmpInterval
            Case 0, 1, 103, 102, 12, 16, 208, 210, 252, 253 '19
                lQConflictFa = TrainInf(ntmpTrain).Starting(ntmpStatemp)
            Case 2, 101, 11, 17, 208, 251
                lQConflictFa = TrainInf(ntmpTrain).Arrival(ntmpStatemp)
        End Select
    End Function

    Function lIntervalTime(ByVal TrainDF As Integer, ByVal nTrainNumber As Integer, _
    ByVal nInterKind As Integer, ByVal nDaoFa As Integer, _
    ByVal nLeaveSection As Integer, ByVal ntmpStation As Integer, ByVal nEnterSection As Integer) As Long

        If TrainInf(TrainDF).TrainClass = 38 Then
            lIntervalTime = 0
        Else
            If nInterKind < 100 Then
                lIntervalTime = DiffTime(TrainDF, nTrainNumber, nInterKind, nLeaveSection, ntmpStation, nEnterSection)
            Else
                lIntervalTime = DanDiffTime(TrainDF, nTrainNumber, nInterKind, nDaoFa, nLeaveSection, ntmpStation, nEnterSection)
            End If
        End If
    End Function

    ''获得列车在该车站的停站时间
    'Public Function GetCurTrainStopTimeAtStation(ByVal nTrain As Integer, ByVal sStaName As String) As Long
    '    Dim i, j, k As Integer
    '    GetCurTrainStopTimeAtStation = 0
    '    For i = 1 To UBound(BasicTrainInf)
    '        If BasicTrainInf(i).sJiaoLuName = TrainInf(nTrain).sJiaoLuName Then ' StationInf(nStatemp).sStationName
    '            For j = 1 To UBound(BasicTrainInf(i).StopScale)
    '                If BasicTrainInf(i).StopScale(j).sName = TrainInf(nTrain).sStopSclaeName Then
    '                    For k = 1 To UBound(BasicTrainInf(i).StopScale(j).nStopStation)
    '                        If StationInf(BasicTrainInf(i).StopScale(j).nStopStation(k)).sStationName = sStaName Then
    '                            GetCurTrainStopTimeAtStation = BasicTrainInf(i).StopScale(j).StopTime(k)
    '                            Exit For
    '                        End If
    '                    Next k
    '                    Exit For
    '                End If
    '            Next j
    '            Exit For
    '        End If
    '    Next i
    'End Function

    '获得列车在该车站的停站时间
    Public Function GetCurTrainStopTimeAtStation(ByVal sJiaoLuName As String, ByVal sStopScaleName As String, ByVal sStaName As String) As Long
        Dim i, j, k As Integer
        GetCurTrainStopTimeAtStation = 0
        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName = sJiaoLuName Then ' StationInf(nStatemp).sStationName
                For j = 1 To UBound(BasicTrainInf(i).StopScale)
                    If BasicTrainInf(i).StopScale(j).sName = sStopScaleName Then
                        For k = 1 To UBound(BasicTrainInf(i).StopScale(j).nStopStation)
                            If StationInf(BasicTrainInf(i).StopScale(j).nStopStation(k)).sStationName = sStaName Then
                                GetCurTrainStopTimeAtStation = BasicTrainInf(i).StopScale(j).StopTime(k)
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

    '获得列车在该车站的停站标尺名称 
    Public Function GetCurTrainStopScaleAtStation(ByVal sJiaoLuName As String, ByVal sStopScaleName As String, ByVal sStaName As String) As String
        Dim i, j, k As Integer
        GetCurTrainStopScaleAtStation = 0
        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName = sJiaoLuName Then ' StationInf(nStatemp).sStationName
                For j = 1 To UBound(BasicTrainInf(i).StopScale)
                    If BasicTrainInf(i).StopScale(j).sName = sStopScaleName Then
                        For k = 1 To UBound(BasicTrainInf(i).StopScale(j).nStopStation)
                            If StationInf(BasicTrainInf(i).StopScale(j).nStopStation(k)).sStationName = sStaName Then
                                GetCurTrainStopScaleAtStation = BasicTrainInf(i).StopScale(j).sScaleName(k)
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

    Function nFindSectionNumber(ByVal nStationNumber1 As Integer, ByVal nStationNumber2 As Integer, _
    ByVal nUpDowntemp As Integer) As Integer
        Dim i As Integer
        Dim nBegintemp As Integer, nEndtemp As Integer

        nBegintemp = nBeginDataSection(nNowDataReadLineNum)
        nEndtemp = nEndDataSection(nNowDataReadLineNum)

        nFindSectionNumber = 0
        For i = nBegintemp To nEndtemp
            With SectionInf(i)
                If nUpDowntemp = 1 Then
                    If StationInf(.nHStation).sStationName = StationInf(nStationNumber1).sStationName _
                        And StationInf(.nQStation).sStationName = StationInf(nStationNumber2).sStationName Then
                        nFindSectionNumber = i
                        Exit For
                    End If
                Else
                    If StationInf(.nHStation).sStationName = StationInf(nStationNumber2).sStationName _
                        And StationInf(.nQStation).sStationName = StationInf(nStationNumber1).sStationName Then
                        nFindSectionNumber = i
                        Exit For
                    End If
                End If
            End With
        Next i
    End Function

    Function lCalculStopTime(ByVal nTrainNumber As Integer, _
    ByVal nBStationtemp As Integer, ByVal nEStationtemp As Integer) As Long 'new
        '计算沿途停站时间
        Dim nTrainNumbertemp As Integer, nStationtemp As Integer, nUpDowntemp As Integer
        Dim nTemp As Integer

        lCalculStopTime = 0
        nStationtemp = nBStationtemp
        nTrainNumbertemp = nTrainNumber

        Do While nStationtemp <> nEStationtemp
            nTemp = nChaDirectionSt(nTrainNumbertemp, nStationtemp, 1)
            If nTemp <> 0 Then
                nStationtemp = nTemp
                nTrainNumbertemp = nChaTrnNum(nTrainNumbertemp)
            Else
                nTrainNumbertemp = nTrainNumber
            End If
            nUpDowntemp = nDirection(nTrainNumbertemp)
            nStationtemp = nFindQstaNum(nTrainNumbertemp, nStationtemp, nUpDowntemp)
            If TrainInf(nTrainNumber).Arrival(nStationtemp) <> TrainInf(nTrainNumber).Starting(nStationtemp) _
                And nStationtemp <> nEStationtemp Then
                lCalculStopTime = lCalculStopTime + TimeMinus(TrainInf(nTrainNumber).Starting(nStationtemp), TrainInf(nTrainNumber).Arrival(nStationtemp))
            End If
        Loop
    End Function

    Function nFanStartStation(ByVal nTrnNum As Integer, ByVal nStatemp As Integer) As Integer
        '始发站处理
        nFanStartStation = 0
        With TrainInf(nTrnNum)
            If StationInf(nStatemp).sStationName = StationInf(nEstation).sStationName Then
                If .ComeStation = StationInf(nStatemp).sStationName Then
                    If .ComeStation = .StartStation Then
                        'nStatemp是始发站
                        nFanStartStation = 1
                    ElseIf .ComeStation <> .StartStation Then
                        'nStatemp不是始发站，但是接入站
                        nFanStartStation = 2
                    End If
                Else
                    'nStatemp不是始发站，也不是接入站,是平移运行线而得
                    nFanStartStation = 3
                End If
            End If
        End With
    End Function

    Public Function SeekSecNum(ByVal nStaNum As Integer, ByVal nUpDown As Integer) As Integer
        Dim i As Integer
        If nUpDown = 2 Then
            For i = 1 To UBound(SectionInf)
                If StationInf(SectionInf(i).nQStation).sStationName = StationInf(nStaNum).sStationName Then
                    SeekSecNum = i
                    Exit Function
                End If
                If i = UBound(SectionInf) Then
                    SeekSecNum = 0
                    Exit Function
                End If
            Next i
        Else
            For i = 1 To UBound(SectionInf)
                If StationInf(SectionInf(i).nHStation).sStationName = StationInf(nStaNum).sStationName Then
                    SeekSecNum = i
                    Exit Function
                End If
                If i = UBound(SectionInf) Then
                    SeekSecNum = 0
                    Exit Function
                End If
            Next i
        End If
    End Function

    Function PassLineNum(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal lPtimetemp As Long) As Integer
        '确定停车股道
        'ntrnnum列车编号，nstatemp当前车站，latimetemp到达时间,lstimetemp出发时间,nchektemp检查标志


        Dim i As Integer, j As Integer
        Dim nBegin As Integer, nEnd As Integer
        Dim nUpDowntemp As Integer, nTemp As Integer
        Dim bTemp As Boolean
        '  Dim nGuDaotemp() As Integer

        nUpDowntemp = nDirection(nTrnNum)

        If nUpDowntemp = 1 Then
            nBegin = 1
            nEnd = UBound(TrainInf)
        Else
            nBegin = 2
            nEnd = UBound(TrainInf)
        End If
        ReDim nTongGuDaotemp(0)
        ZhenXianPassGuDaoNum(nTrnNum, nStatemp)
        Dim tmpPassTimeCheck As Integer

        For i = 1 To UBound(nTongGuDaotemp)
            bTemp = True
            For j = nBegin To nEnd Step 2
                With TrainInf(j)
                    If .Train <> "" Then
                        'If .Arrival(nStatemp) <> .Starting(nStatemp) Then
                        nTemp = nFindGuDaoNum(j, nStatemp)
                        If nTemp = nTongGuDaotemp(i) Then
                            tmpPassTimeCheck = PassTimeCheck(lPtimetemp, nStatemp, j)
                            If tmpPassTimeCheck <> 0 Then
                                If TrainInf(nTrnNum).TrainClassCal < .TrainClassCal Then
                                    FinInfTrn(j)
                                    PassLineNum = 0
                                    Exit For
                                Else
                                    bTemp = False
                                    PassLineNum = 1
                                End If
                                Exit For
                            End If
                        End If
                    End If
                    ' End If
                End With
            Next j
            If bTemp = True Then
                TrainInf(nTrnNum).StopLine(nStatemp) = StationInf(nStatemp).sStLineNo(nTongGuDaotemp(i))
                For j = 1 To UBound(StationInf)
                    If StationInf(j).sStationName = StationInf(nStatemp).sStationName Then
                        TrainInf(nTrnNum).StopLine(j) = TrainInf(nTrnNum).StopLine(nStatemp)
                        'If TrainInf(nTrnNum).nChaRunDirection <> 0 Then
                        'TrainInf(nChaTrnNum(nTrnNum)).StopLine(j) = TrainInf(nTrnNum).StopLine(nStatemp)
                        'End If
                    End If
                Next j
                Exit For
            End If
        Next i
    End Function

    Function CheckPass(ByVal nTrnNum As Integer, ByVal lPtime As Long, ByVal nUporDown As Integer, _
    ByVal nLeaveSection As Integer, ByVal ntmpStation As Integer, ByVal nEnterSection As Integer) As Integer
        'ntrnnum本次列车编号，lstime当前时间，nstatemp前一车站
        '检查通过条件
        Dim nStatemp As Integer
        nStatemp = ntmpStation
        TXTG(nTrnNum, lPtime, nUporDown, nLeaveSection, nStatemp, nEnterSection)
        CheckPass = nCheckJianGe(nTrnNum, nStatemp)
    End Function

    Function lCanRightMovePass(ByVal lNeedMoveTime As Long, ByVal nTrainNumber As Integer, ByVal nStationtemp As Integer, _
    ByVal nStopStation As Integer, ByVal nConflictKind As Integer, ByVal lNowTime As Long, ByVal nPhFx As Integer) As Long
        Dim nTrainNumbertemp As Integer, nStationHtemp As Integer, nUpDowntemp As Integer
        Dim lTxMovetemp As Long, lFxMovetemp As Long
        Dim nTxNumtemp As Integer, nFxNumtemp As Integer
        Dim nTemp As Integer
        Dim bTemp As Boolean
        Dim lTemp As Long
        Select Case nPhFx
            Case 1
                lCanRightMovePass = 0
                nStationHtemp = nStationtemp
                bTemp = True
                nTrainNumbertemp = nTrainNumber
                'If StationInf(nStopStation).sStationName <> TrainInf(nTrainNumber).ComeStation Then
                If nCanRightMove(nStopStation) = 0 Then
                    Do While bTemp = True

                        If StationInf(nStationHtemp).sStationName = StationInf(nStopStation).sStationName Then
                            bTemp = False
                        End If
                        If nConflictKind = 211 Or nConflictKind = 212 Or nConflictKind = 213 Then
                            lFxMovetemp = FxDiffTime1(nTrainNumbertemp, nStationHtemp)
                            nFxNumtemp = FxDiffTrain1(nTrainNumbertemp, nStationHtemp)
                            lTxMovetemp = TxDiffTime1(nTrainNumbertemp, nStationHtemp)
                            nTxNumtemp = TxDiffTrain1(nTrainNumbertemp, nStationHtemp)
                        Else
                            lFxMovetemp = FxDiffTime2(nTrainNumbertemp, nStationHtemp)
                            nFxNumtemp = FxDiffTrain2(nTrainNumbertemp, nStationHtemp)
                            lTxMovetemp = TxDiffTime2(nTrainNumbertemp, nStationHtemp)
                            nTxNumtemp = TxDiffTrain2(nTrainNumbertemp, nStationHtemp)
                        End If

                        If lNeedMoveTime <= -lTxMovetemp And lNeedMoveTime <= -lFxMovetemp Then
                            lCanRightMovePass = lNeedMoveTime
                        ElseIf lNeedMoveTime <= -lTxMovetemp And lNeedMoveTime > -lFxMovetemp Then
                            If TrainInf(nFxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal Then
                                lCanRightMovePass = lNeedMoveTime
                                FinInfTrn(nFxNumtemp)
                            Else
                                lCanRightMovePass = 0
                                Exit Do
                            End If
                        ElseIf lNeedMoveTime > -lTxMovetemp And lNeedMoveTime <= -lFxMovetemp Then
                            If TrainInf(nTxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal Then
                                lCanRightMovePass = lNeedMoveTime
                                FinInfTrn(nTxNumtemp)
                            Else
                                lCanRightMovePass = 0
                                Exit Do
                            End If
                        ElseIf lNeedMoveTime > -lTxMovetemp And lNeedMoveTime > -lFxMovetemp Then
                            ' If TrainInf(nTxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal _
                            'And TrainInf(nFxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal Then
                            lCanRightMovePass = lNeedMoveTime
                            FinInfTrn(nTxNumtemp)
                            FinInfTrn(nFxNumtemp)
                            'Else
                            ' lCanRightMovePass = 0
                            ' Exit Do
                            'End If
                        End If
                        nTemp = nChaDirectionSt(nTrainNumber, nStationHtemp, 1)
                        If nTemp <> 0 Then
                            nStationHtemp = nTemp
                            nTrainNumbertemp = nChaTrnNum(nTrainNumbertemp)
                        Else
                            nTrainNumbertemp = nTrainNumber
                        End If
                        nUpDowntemp = nDirection(nTrainNumbertemp)
                        nStationHtemp = nFindHstaNum(nTrainNumbertemp, nStationHtemp, nUpDowntemp)
                        If StationInf(nStationHtemp).sStationName = TrainInf(nTrainNumbertemp).ComeStation Then
                            nStationHtemp = nBstation
                        End If
                    Loop
                End If
                'End If
            Case 2
                lCanRightMovePass = 0
                nStationHtemp = nStationtemp
                bTemp = True
                nTrainNumbertemp = nTrainNumber
                If nCanLeftMove(nStopStation) = 0 Then
                    'If StationInf(nStationTemp).sStationName = TrainInf(nTrainNumber).NextStation _
                    '    And TrainInf(nTrainNumber).Starting(nStationTemp) = TrainInf(nTrainNumber).Arrival(nStationTemp) Then
                    '    lFxMovetemp = FxDiffTime2(nTrainNumbertemp, nStationHtemp)
                    '    nFxNumtemp = FxDiffTrain2(nTrainNumbertemp, nStationHtemp)
                    '    lTxMovetemp = TxDiffTime2(nTrainNumbertemp, nStationHtemp)
                    '    nTxNumtemp = TxDiffTrain2(nTrainNumbertemp, nStationHtemp)
                    '    If lNeedMoveTime <= -lTxMovetemp And lNeedMoveTime <= -lFxMovetemp Then
                    '        lCanRightMovePass = lNeedMoveTime
                    '    ElseIf lNeedMoveTime <= -lTxMovetemp And lNeedMoveTime > -lFxMovetemp Then
                    '        If TrainInf(nFxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal Then
                    '            lCanRightMovePass = lNeedMoveTime
                    '            FinInfTrn nFxNumtemp
                    '        Else
                    '            lCanRightMovePass = 0
                    '        End If
                    '    ElseIf lNeedMoveTime > -lTxMovetemp And lNeedMoveTime <= -lFxMovetemp Then
                    '        If TrainInf(nTxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal Then
                    '            lCanRightMovePass = lNeedMoveTime
                    '            FinInfTrn nTxNumtemp
                    '        Else
                    '            lCanRightMovePass = 0
                    '       End If
                    '   ElseIf lNeedMoveTime > -lTxMovetemp And lNeedMoveTime > -lFxMovetemp Then
                    '       If TrainInf(nTxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal _
                    '           And TrainInf(nFxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal Then
                    '           lCanRightMovePass = lNeedMoveTime
                    '          FinInfTrn nTxNumtemp
                    '          FinInfTrn nFxNumtemp
                    '      Else
                    '          lCanRightMovePass = 0
                    '      End If
                    '  End If
                    'Else
                    If StationInf(nStopStation).sStationName = TrainInf(nTrainNumber).EndStation Then
                        lTemp = lNeedStop(nTrainNumber, nStopStation, 1)
                    Else
                        lTemp = lNeedStop(nTrainNumber, nStopStation, 4)
                    End If
                    If (TimeMinus(TrainInf(nTrainNumber).Starting(nStopStation), TrainInf(nTrainNumber).Arrival(nStopStation)) _
                        - lTemp) > lNeedMoveTime Then
                        '前方停车站停站时间允许左移
                        Do While bTemp = True
                            If StationInf(nStationHtemp).sStationName = StationInf(nStopStation).sStationName Then
                                bTemp = False
                            End If
                            lFxMovetemp = FxDiffTime2(nTrainNumbertemp, nStationHtemp)
                            nFxNumtemp = FxDiffTrain2(nTrainNumbertemp, nStationHtemp)
                            lTxMovetemp = TxDiffTime2(nTrainNumbertemp, nStationHtemp)
                            nTxNumtemp = TxDiffTrain2(nTrainNumbertemp, nStationHtemp)
                            If lNeedMoveTime <= -lTxMovetemp And lNeedMoveTime <= -lFxMovetemp Then
                                lCanRightMovePass = lNeedMoveTime
                            ElseIf lNeedMoveTime <= -lTxMovetemp And lNeedMoveTime > -lFxMovetemp Then
                                If TrainInf(nFxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal Then
                                    lCanRightMovePass = lNeedMoveTime
                                    FinInfTrn(nFxNumtemp)
                                Else
                                    lCanRightMovePass = 0
                                    Exit Do
                                End If
                            ElseIf lNeedMoveTime > -lTxMovetemp And lNeedMoveTime <= -lFxMovetemp Then
                                If TrainInf(nTxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal Then
                                    lCanRightMovePass = lNeedMoveTime
                                    FinInfTrn(nTxNumtemp)
                                Else
                                    lCanRightMovePass = 0
                                    Exit Do
                                End If
                            ElseIf lNeedMoveTime > -lTxMovetemp And lNeedMoveTime > -lFxMovetemp Then
                                If TrainInf(nTxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal _
                                    And TrainInf(nFxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal Then
                                    lCanRightMovePass = lNeedMoveTime
                                    FinInfTrn(nTxNumtemp)
                                    FinInfTrn(nFxNumtemp)
                                Else
                                    lCanRightMovePass = 0
                                    Exit Do
                                End If
                            End If
                            nTemp = nChaDirectionSt(nTrainNumber, nStationHtemp, 0)
                            If nTemp <> 0 Then
                                nStationHtemp = nTemp
                                nTrainNumbertemp = nChaTrnNum(nTrainNumbertemp)
                            Else
                                nTrainNumbertemp = nTrainNumber
                            End If
                            nUpDowntemp = nDirection(nTrainNumbertemp)
                            nStationHtemp = nFindQstaNum(nTrainNumbertemp, nStationHtemp, nUpDowntemp)
                            If StationInf(nStationHtemp).sStationName = TrainInf(nTrainNumbertemp).NextStation Then
                                nStationHtemp = nBstation
                            End If
                        Loop
                    End If
                    'End If
                End If
        End Select
    End Function

    Function lQConflictTong(ByVal ntmpTrnNum As Integer, ByVal ntmpStatemp As Integer) As Long
        Dim ntmpInterval As Integer, ntmpTrain As Integer

        If TxDiffTime1(ntmpTrnNum, ntmpStatemp) < FxDiffTime1(ntmpTrnNum, ntmpStatemp) Then
            ntmpInterval = FxIntervalKind1(ntmpTrnNum, ntmpStatemp)
            ntmpTrain = FxDiffTrain1(ntmpTrnNum, ntmpStatemp)
        Else
            ntmpInterval = TxIntervalKind1(ntmpTrnNum, ntmpStatemp)
            ntmpTrain = TxDiffTrain1(ntmpTrnNum, ntmpStatemp)
        End If
        Select Case ntmpInterval
            Case 3, 8, 115, 114, 14, 201, 211, 212, 213, 272, 273
                lQConflictTong = TrainInf(ntmpTrain).Starting(ntmpStatemp)
            Case 7, 113, 13, 17, 202, 271
                lQConflictTong = TrainInf(ntmpTrain).Arrival(ntmpStatemp)
        End Select
    End Function

    Function nChaDirectionSt(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal nQianHou As Integer) As Integer '
        '查找在该车站列车是否改变运行方向
        '0 表示没有反向运行,其他值表示反向运行的车站编号(在该站上行变下行或下行变上行)
        'nQianHou表示向前或向后查找，0表示向前，1表示向后

        Dim i As Integer
        Dim nTemp As Integer
        nChaDirectionSt = 0
        With TrainInf(nTrnNum)
            If .nChaRunDirection <> 0 Then
                For i = 1 To .NumWay
                    If StationInf(nStatemp).sStationName = .Way1(i) _
                        And .Way4(i) <> "无" Then
                        If nQianHou = 0 Then
                            If nTrnNum Mod 2 <> 0 Then
                                If .Way4(i) = "上行" Then
                                    nTemp = nFindSecNumbyString(.Way1(i), .Way2(i), 2, nNowDataReadLineNum)
                                    If nTemp <> 0 Then
                                        nChaDirectionSt = SectionInf(nTemp).nQStation
                                    Else
                                        nTemp = nFindSecNumbyString(.Way3(i), .Way1(i), 1, nNowDataReadLineNum)
                                        nChaDirectionSt = SectionInf(nTemp).nQStation
                                    End If
                                    Exit For
                                End If
                            Else
                                If .Way4(i) = "下行" Then
                                    nTemp = nFindSecNumbyString(.Way1(i), .Way2(i), 1, nNowDataReadLineNum)
                                    If nTemp <> 0 Then
                                        nChaDirectionSt = SectionInf(nTemp).nHStation
                                    Else
                                        nTemp = nFindSecNumbyString(.Way3(i), .Way1(i), 2, nNowDataReadLineNum)
                                        nChaDirectionSt = SectionInf(nTemp).nHStation
                                    End If
                                    Exit For
                                End If
                            End If
                        Else
                            If nTrnNum Mod 2 <> 0 Then
                                If .Way4(i) = "下行" Then
                                    nTemp = nFindSecNumbyString(.Way3(i), .Way1(i), 2, nNowDataReadLineNum)
                                    If nTemp <> 0 Then
                                        nChaDirectionSt = SectionInf(nTemp).nHStation
                                    Else
                                        nChaDirectionSt = nStatemp
                                    End If
                                    Exit For
                                End If
                            Else
                                If .Way4(i) = "上行" Then
                                    nTemp = nFindSecNumbyString(.Way3(i), .Way1(i), 1, nNowDataReadLineNum)
                                    If nTemp <> 0 Then
                                        nChaDirectionSt = SectionInf(nTemp).nQStation
                                    Else
                                        nChaDirectionSt = nStatemp
                                    End If
                                    Exit For
                                End If
                            End If
                        End If
                    End If
                Next i
            End If
        End With
    End Function

    Sub GuDaoOccupiedbyTrain(ByVal nTrnNum As Integer, _
    ByVal nStatemp As Integer, ByVal lAtimetemp As Long, ByVal lStimetemp As Long)
        Dim i As Integer
        Dim nBegintemp As Integer, nEndtemp As Integer
        'ReDim nGuDaotemp(1)

        nBegintemp = 1
        nEndtemp = UBound(TrainInf)

        For i = nBegintemp To nEndtemp
            '检查停站列车，确定当前时间是否落入停站列车的停车时间范围内
            If TrainInf(i).Train <> "" Then
                If i <> nTrnNum Then
                    Select Case TimeCheck(lAtimetemp, lStimetemp, nStatemp, i)
                        Case 1
                            '当前列车到达时刻落入已有列车停车范围
                            ReDim Preserve nGuDaoTrain(UBound(nGuDaoTrain) + 1)
                            nGuDaoTrain(UBound(nGuDaoTrain)) = i
                        Case 2
                            '当前列车停车时间包含已有列车停车时间
                            ReDim Preserve nGuDaoTrain(UBound(nGuDaoTrain) + 1)
                            nGuDaoTrain(UBound(nGuDaoTrain)) = i
                    End Select
                End If
            End If
        Next i
    End Sub

    '以下代码为新编写 2004年4月3日
    Public Sub SeekKeYongGD(ByVal nTrain As Integer, ByVal nStaNum As Integer)
        Dim i As Integer
        Dim j As Integer
        ' If Left(StationInf(nStaNum).sStationProp, 1) <> "F" Then
        '以下代码为广深线使用
        '    If StationInf(nStaNum).sStationName = TrainInf(nTrain).ComeStation Or StationInf(nStaNum).sStationName = TrainInf(nTrain).NextStation Then '始发或终到站
        '        For j = 1 To UBound(staTrackUseInf)
        '            If staTrackUseInf(j).sJiaoLuName = TrainInf(nTrain).sJiaoLuName And staTrackUseInf(j).sStaName = StationInf(nStaNum).sStationName Then
        '                For k = 1 To UBound(staTrackUseInf(j).sUseSeq)
        '                    ReDim Preserve KeYongGD(UBound(KeYongGD) + 1)
        '                    KeYongGD(UBound(KeYongGD)) = FromSGudaoNumtoGuDaoID(staTrackUseInf(j).sUseSeq(k), nStaNum)
        '                Next
        '            End If
        '        Next
        '    ElseIf StationInf(nStaNum).sStationName = CDZDrawPara.sDownStartSta Then '通过广州东且停站的列车
        '        If TrainInf(nTrain).ComeStation <> CDZDrawPara.sDownStartSta And TrainInf(nTrain).NextStation <> CDZDrawPara.sDownStartSta Then
        '            For j = 1 To UBound(staTrackUseInf)
        '                If staTrackUseInf(j).sJiaoLuName = TrainInf(nTrain).sJiaoLuName And staTrackUseInf(j).sStaName = StationInf(nStaNum).sStationName Then
        '                    For k = 1 To UBound(staTrackUseInf(j).sUseSeq)
        '                        ReDim Preserve KeYongGD(UBound(KeYongGD) + 1)
        '                        KeYongGD(UBound(KeYongGD)) = FromSGudaoNumtoGuDaoID(staTrackUseInf(j).sUseSeq(k), nStaNum)
        '                    Next
        '                End If
        '            Next
        '        End If
        '    End If
        '    If UBound(KeYongGD) > 0 Then
        '        Exit Sub
        '    End If
        'If StationInf(nStaNum).sStationName = "浦东机场站" Then Stop
        If nTrain Mod 2 = 0 Then
            For i = 1 To UBound(StationInf(nStaNum).sTrackUse)
                If StationInf(nStaNum).sTrackUse(i).sJiaoLuName = TrainInf(nTrain).sJiaoLuName Then
                    If UBound(StationInf(nStaNum).sTrackUse(i).sUpStopUse) > 0 Then
                        ReDim KeYongGD(UBound(StationInf(nStaNum).sTrackUse(i).sUpStopUse))
                        For j = 1 To UBound(KeYongGD)
                            KeYongGD(j) = FromSGudaoNumtoGuDaoID(StationInf(nStaNum).sTrackUse(i).sUpStopUse(j), nStaNum)
                        Next
                    End If
                    Exit For
                End If
            Next
        Else
            For i = 1 To UBound(StationInf(nStaNum).sTrackUse)
                If StationInf(nStaNum).sTrackUse(i).sJiaoLuName = TrainInf(nTrain).sJiaoLuName Then
                    If UBound(StationInf(nStaNum).sTrackUse(i).sDownStopUse) > 0 Then
                        ReDim KeYongGD(UBound(StationInf(nStaNum).sTrackUse(i).sDownStopUse))
                        For j = 1 To UBound(KeYongGD)
                            KeYongGD(j) = FromSGudaoNumtoGuDaoID(StationInf(nStaNum).sTrackUse(i).sDownStopUse(j), nStaNum)
                        Next
                    End If
                    Exit For
                End If
            Next
        End If

        If UBound(KeYongGD) > 0 Then
            Exit Sub
        End If
        '再找到发线
        For i = 1 To UBound(StationInf(nStaNum).sStLineNo)
            If StationInf(nStaNum).sLineUse(i).Length >= 3 Then
                If nTrain Mod 2 <> 0 Then
                    If StationInf(nStaNum).sUpOrDownUse(i) = "只能下行" Or StationInf(nStaNum).sUpOrDownUse(i) = "主要方向为下行" Or StationInf(nStaNum).sUpOrDownUse(i) = "双方向" Then
                        If StationInf(nStaNum).sLineUse(i).Substring(0, 3) = "到发线" Then
                            ReDim Preserve KeYongGD(UBound(KeYongGD) + 1)
                            KeYongGD(UBound(KeYongGD)) = i
                        End If
                    End If
                Else
                    If StationInf(nStaNum).sUpOrDownUse(i) = "只能上行" Or StationInf(nStaNum).sUpOrDownUse(i) = "主要方向为上行" Or StationInf(nStaNum).sUpOrDownUse(i) = "双方向" Then
                        If StationInf(nStaNum).sLineUse(i).Substring(0, 3) = "到发线" Then
                            ReDim Preserve KeYongGD(UBound(KeYongGD) + 1)
                            KeYongGD(UBound(KeYongGD)) = i
                        End If
                    End If
                End If
            End If
        Next i

        ''再找折返线
        'For i = 1 To UBound(StationInf(nStaNum).sStLineNo)
        '    If nTrain Mod 2 <> 0 Then
        '        If StationInf(nStaNum).sUpOrDownUse(i) = "只能下行" Or StationInf(nStaNum).sUpOrDownUse(i) = "主要方向为下行" Or StationInf(nStaNum).sUpOrDownUse(i) = "双方向" Then
        '            If StationInf(nStaNum).sLineUse(i).Substring(0, 3) = "折返线" Then
        '                ReDim Preserve KeYongGD(UBound(KeYongGD) + 1)
        '                KeYongGD(UBound(KeYongGD)) = i
        '            End If
        '        End If
        '    Else
        '        If StationInf(nStaNum).sUpOrDownUse(i) = "只能上行" Or StationInf(nStaNum).sUpOrDownUse(i) = "主要方向为上行" Or StationInf(nStaNum).sUpOrDownUse(i) = "双方向" Then
        '            If StationInf(nStaNum).sLineUse(i).Substring(0, 3) = "折返线" Then
        '                ReDim Preserve KeYongGD(UBound(KeYongGD) + 1)
        '                KeYongGD(UBound(KeYongGD)) = i
        '            End If
        '        End If
        '    End If
        'Next i

        ''再找存车线
        'For i = 1 To UBound(StationInf(nStaNum).sStLineNo)
        '    If nTrain Mod 2 <> 0 Then
        '        If StationInf(nStaNum).sUpOrDownUse(i) = "只能下行" Or StationInf(nStaNum).sUpOrDownUse(i) = "主要方向为下行" Or StationInf(nStaNum).sUpOrDownUse(i) = "双方向" Then
        '            If StationInf(nStaNum).sLineUse(i).Substring(0, 3) = "存车线" Then
        '                ReDim Preserve KeYongGD(UBound(KeYongGD) + 1)
        '                KeYongGD(UBound(KeYongGD)) = i
        '            End If
        '        End If
        '    Else
        '        If StationInf(nStaNum).sUpOrDownUse(i) = "只能上行" Or StationInf(nStaNum).sUpOrDownUse(i) = "主要方向为上行" Or StationInf(nStaNum).sUpOrDownUse(i) = "双方向" Then
        '            If StationInf(nStaNum).sLineUse(i).Substring(0, 3) = "存车线" Then
        '                ReDim Preserve KeYongGD(UBound(KeYongGD) + 1)
        '                KeYongGD(UBound(KeYongGD)) = i
        '            End If
        '        End If
        '    End If
        'Next i

        If UBound(KeYongGD) = 0 Then '如果找不到到发线，使用正线停车
            '再找存车线
            For i = 1 To UBound(StationInf(nStaNum).sStLineNo)
                If StationInf(nStaNum).sLineUse(i).Length >= 3 Then
                    If nTrain Mod 2 <> 0 Then
                        If StationInf(nStaNum).sUpOrDownUse(i) = "只能下行" Or StationInf(nStaNum).sUpOrDownUse(i) = "主要方向为下行" Or StationInf(nStaNum).sUpOrDownUse(i) = "双方向" Then
                            If StationInf(nStaNum).sLineUse(i).Substring(0, 3) = "正线线" Then
                                ReDim Preserve KeYongGD(UBound(KeYongGD) + 1)
                                KeYongGD(UBound(KeYongGD)) = i
                            End If
                        End If
                    Else
                        If StationInf(nStaNum).sUpOrDownUse(i) = "只能上行" Or StationInf(nStaNum).sUpOrDownUse(i) = "主要方向为上行" Or StationInf(nStaNum).sUpOrDownUse(i) = "双方向" Then
                            If StationInf(nStaNum).sLineUse(i).Substring(0, 3) = "正线线" Then
                                ReDim Preserve KeYongGD(UBound(KeYongGD) + 1)
                                KeYongGD(UBound(KeYongGD)) = i
                            End If
                        End If
                    End If
                End If

            Next i
        End If
        '    End If

        '  Else '**********************************************************************为分岔站

        'Dim nTemp1 As Integer

        'With StationInf(nStaNum)
        '    nTemp1 = nFindTrainFenChaNum(nTrain, nStaNum)
        '    For i = 1 To .nStLineNum
        '        Dim Q As Integer
        '        For j = 1 To UBound(.sGDFromSta)
        '            If .sGDFromSta(j) = TrainInf(nTrain).Way3(nTemp1) And .sGDToSta(j) = TrainInf(nTrain).Way2(nTemp1) Then
        '                If .sGDDaoFaBasicJinLu(j) = "" Or .sGDDaoFaBasicJinLu(j) = "无" Then
        '                    MsgBox("从(" & TrainInf(nTrain).Way3(nTemp1) & ") 至 (" & TrainInf(nTrain).Way2(nTemp1) & ") 的车站股道使用信息定义为空!","提示")
        '                    Stop
        '                End If
        '                Call GetGuDaoYongTu(.sGDDaoFaBasicJinLu(j))
        '                If UBound(FenChaZhanGuDaoUse) > 0 Then
        '                    For Q = 1 To UBound(FenChaZhanGuDaoUse)
        '                        If FenChaZhanGuDaoUse(Q) = .sStLineNo(i) Then
        '                            ReDim Preserve KeYongGD(UBound(KeYongGD) + 1)
        '                            KeYongGD(UBound(KeYongGD)) = i
        '                        End If
        '                    Next Q
        '                End If
        '                Exit For
        '            End If
        '        Next j
        '    Next i
        'End With
        '  End If

        If UBound(KeYongGD) = 0 Then
            'MsgBox("从(" & TrainInf(nTrain).Way3(nTemp1) & ") 至 (" & TrainInf(nTrain).Way2(nTemp1) & ") 的车站股道使用信息没有定义!","提示")
            'Stop
        End If
    End Sub

    Sub ZhenXianPassGuDaoNum(ByVal nTrnNum As Integer, ByVal nStatemp As Integer)
        Dim i As Integer
        ' Dim j As Integer
        Dim nTemp1 As Integer
        'Dim bTemp As Boolean
        ReDim nTongGuDaotemp(0)
        With StationInf(nStatemp)
            For i = 1 To .nStLineNum
                If .sStLineNo(i) <> "" Then '找出正线
                    If Left(.sStationProp, 1) = "F" Then
                        nTemp1 = nFindTrainFenChaNum(nTrnNum, nStatemp)
                        'nTemp2 = nFindFenChaZhan(nStatemp)

                        '江编的代码，判断分岔站可停的股道号
                        'Dim Q As Integer
                        'For j = 1 To UBound(.sGDFromSta)
                        '    bTemp = False
                        '    If .sGDFromSta(j) = TrainInf(nTrnNum).Way3(nTemp1) And .sGDToSta(j) = TrainInf(nTrnNum).Way2(nTemp1) Then
                        '        Call GetGuDaoYongTu(.sGDPassBasicJinLu(j))
                        '        If UBound(FenChaZhanGuDaoUse) > 0 Then
                        '            For Q = 1 To UBound(FenChaZhanGuDaoUse)
                        '                If FenChaZhanGuDaoUse(Q) = .sStLineNo(i) Then
                        '                    ReDim Preserve nTongGuDaotemp(UBound(nTongGuDaotemp) + 1)
                        '                    nTongGuDaotemp(UBound(nTongGuDaotemp)) = i
                        '                    bTemp = True
                        '                    Exit For
                        '                End If
                        '            Next Q
                        '            If bTemp = True Then Exit For
                        '        End If
                        '    End If
                        'Next j
                    Else
                        If nTrnNum Mod 2 = 0 Then
                            If .nStLineUse(i) = 2100 Then '下行正线
                                ReDim Preserve nTongGuDaotemp(UBound(nTongGuDaotemp) + 1)
                                nTongGuDaotemp(UBound(nTongGuDaotemp)) = i
                            End If
                        Else
                            If .nStLineUse(i) = 1100 Then '上行正线
                                ReDim Preserve nTongGuDaotemp(UBound(nTongGuDaotemp) + 1)
                                nTongGuDaotemp(UBound(nTongGuDaotemp)) = i
                            End If
                        End If
                    End If
                End If
            Next i
        End With
    End Sub

    Sub TXFZ(ByVal nTrnNum As Integer, ByVal lTim As Long, ByVal nSdirection As Integer, _
    ByVal nLeaveSection As Integer, ByVal ntmpStation As Integer, ByVal nEnterSection As Integer)
        'nTrnNum本次列车车次，tim当前时间，nStateRecord当前车站编号
        'KH客车或货车，nSdirection检查的方向
        '发站的同向判断，停入同向股道

        Dim nDanShuang1 As Integer, nDanShuang2 As Integer
        Dim lDifftemp1 As Long, lDifftemp2 As Long
        Dim nEnterSec As Integer, nLeaveSec As Integer
        Dim ntmpTianChuangSec As Integer, ntmpFa As Integer
        Dim nStaterecord As Integer

        nStaterecord = ntmpStation
        If StationInf(nStaterecord).sStationName = "" Then Stop

        nEnterSec = nEnterSection
        nLeaveSec = nLeaveSection

        ntmpTianChuangSec = nIfTianChuangSec(nEnterSec)

        If ntmpTianChuangSec <> 0 Then
            Select Case TianChuangInf(ntmpTianChuangSec).sTianChuangKind
                Case "上行"
                    lDifftemp1 = TianChuangInf(ntmpTianChuangSec).lUpEmptyTime(1)
                    lDifftemp2 = TianChuangInf(ntmpTianChuangSec).lUpEmptyTime(2)
                    ntmpFa = EmptyTimeCheck(lDifftemp1, lDifftemp2, lTim)
                    If ntmpFa <> 0 Then
                        nDanShuang1 = SectionInf(nLeaveSec).nSection
                        nDanShuang2 = 1
                    Else
                        nDanShuang1 = SectionInf(nLeaveSec).nSection
                        nDanShuang2 = SectionInf(nEnterSec).nSection
                    End If
                Case "下行"
                    lDifftemp1 = TianChuangInf(ntmpTianChuangSec).lDownEmptyTime(1)
                    lDifftemp2 = TianChuangInf(ntmpTianChuangSec).lDownEmptyTime(2)
                    ntmpFa = EmptyTimeCheck(lDifftemp1, lDifftemp2, lTim)
                    If ntmpFa <> 0 Then
                        nDanShuang1 = SectionInf(nLeaveSec).nSection
                        nDanShuang2 = 1
                    Else
                        nDanShuang1 = SectionInf(nLeaveSec).nSection
                        nDanShuang2 = SectionInf(nEnterSec).nSection
                    End If
                Case "上下行"
                    lDifftemp1 = TianChuangInf(ntmpTianChuangSec).lUpEmptyTime(1)
                    lDifftemp2 = TianChuangInf(ntmpTianChuangSec).lUpEmptyTime(2)
                    ntmpFa = EmptyTimeCheck(lDifftemp1, lDifftemp2, lTim)
                    If ntmpFa <> 0 Then
                        TxIntervalKind1(nTrnNum, nStaterecord) = 301
                        TxDiffTime1(nTrnNum, nStaterecord) = TimeMinus(lTim, lDifftemp1)
                        TxDiffTrain1(nTrnNum, nStaterecord) = 0
                        TxIntervalKind2(nTrnNum, nStaterecord) = 301
                        TxDiffTime2(nTrnNum, nStaterecord) = TimeMinus(lDifftemp2, lTim)
                        TxDiffTrain2(nTrnNum, nStaterecord) = 0
                        FxIntervalKind1(nTrnNum, nStaterecord) = 301
                        FxDiffTime1(nTrnNum, nStaterecord) = TimeMinus(lTim, lDifftemp1)
                        FxDiffTrain1(nTrnNum, nStaterecord) = 0
                        FxIntervalKind2(nTrnNum, nStaterecord) = 301
                        FxDiffTime2(nTrnNum, nStaterecord) = TimeMinus(lDifftemp2, lTim)
                        FxDiffTrain2(nTrnNum, nStaterecord) = 0
                    End If
            End Select
        Else
            nDanShuang1 = SectionInf(nLeaveSec).nSection
            nDanShuang2 = SectionInf(nEnterSec).nSection
        End If
        If nDanShuang1 = 2 And nDanShuang2 = 2 Then '从双进双
            DoubleDoubleStartCheck(nTrnNum, lTim, nStaterecord, nSdirection, nLeaveSec, nEnterSec)
        ElseIf nDanShuang1 = 2 And nDanShuang2 = 1 Then '从双进单
            DoubleSingleStartCheck(nTrnNum, lTim, nStaterecord, nSdirection, nLeaveSec, nEnterSec)
        ElseIf nDanShuang1 = 1 And nDanShuang2 = 1 Then '从单进单
            SingleSingleStartCheck(nTrnNum, lTim, nStaterecord, nSdirection, nLeaveSec, nEnterSec)
        ElseIf nDanShuang1 = 1 And nDanShuang2 = 2 Then '从单进双
            SingleDoubleStartCheck(nTrnNum, lTim, nStaterecord, nSdirection, nLeaveSec, nEnterSec)
        End If
    End Sub

    Sub TXTG(ByVal nTrnNum As Integer, ByVal lTim As Long, ByVal nSdirection As Integer, _
    ByVal nLeaveSection As Integer, ByVal ntmpStation As Integer, ByVal nEnterSection As Integer)
        'nTrnNum本次列车车次，lTim当前时间，nStateRecord当前车站编号
        'KH客车或货车，nSdirection检查的方向
        '通过的同向判断，停入同向股道
        Dim nDanShuang1 As Integer, nDanShuang2 As Integer
        Dim lDifftemp1 As Long, lDifftemp2 As Long
        Dim nEnterSec As Integer, nLeaveSec As Integer
        Dim ntmpTianChuangSec1 As Integer, ntmpTianChuangSec2 As Integer, ntmpFa As Integer, ntmpDao As Integer
        Dim nStaterecord As Integer

        nStaterecord = ntmpStation
        If StationInf(nStaterecord).sStationName = "" Then Stop

        nEnterSec = nEnterSection
        nLeaveSec = nLeaveSection
        nDanShuang1 = SectionInf(nLeaveSec).nSection
        nDanShuang2 = SectionInf(nEnterSec).nSection
        ntmpTianChuangSec1 = nIfTianChuangSec(nLeaveSec)
        ntmpTianChuangSec2 = nIfTianChuangSec(nEnterSec)

        If ntmpTianChuangSec1 <> 0 Then
            Select Case TianChuangInf(ntmpTianChuangSec1).sTianChuangKind
                Case "上行"
                    lDifftemp1 = TianChuangInf(ntmpTianChuangSec1).lUpEmptyTime(1)
                    lDifftemp2 = TianChuangInf(ntmpTianChuangSec1).lUpEmptyTime(2)
                    If nDirection(nTrnNum) = 1 Then
                        ntmpFa = EmptyTimeCheck(lDifftemp1, lDifftemp2, TrainInf(nTrnNum).Starting(SectionInf(nLeaveSec).nHStation))
                    Else
                        ntmpFa = EmptyTimeCheck(lDifftemp1, lDifftemp2, TrainInf(nTrnNum).Starting(SectionInf(nLeaveSec).nQStation))
                    End If
                    ntmpDao = EmptyTimeCheck(lDifftemp1, lDifftemp2, lTim)
                    If ntmpFa = 0 And ntmpDao <> 0 Then
                        nDanShuang1 = 1
                    ElseIf ntmpFa <> 0 And ntmpDao <> 0 Then
                        nDanShuang1 = 1
                    Else
                        nDanShuang1 = SectionInf(nLeaveSec).nSection
                    End If
                Case "下行"
                    lDifftemp1 = TianChuangInf(ntmpTianChuangSec1).lDownEmptyTime(1)
                    lDifftemp2 = TianChuangInf(ntmpTianChuangSec1).lDownEmptyTime(2)
                    ntmpFa = EmptyTimeCheck(lDifftemp1, lDifftemp2, lTim)
                    If nDirection(nTrnNum) = 1 Then
                        ntmpFa = EmptyTimeCheck(lDifftemp1, lDifftemp2, TrainInf(nTrnNum).Starting(SectionInf(nLeaveSec).nHStation))
                    Else
                        ntmpFa = EmptyTimeCheck(lDifftemp1, lDifftemp2, TrainInf(nTrnNum).Starting(SectionInf(nLeaveSec).nQStation))
                    End If
                    ntmpDao = EmptyTimeCheck(lDifftemp1, lDifftemp2, lTim)
                    If ntmpFa = 0 And ntmpDao <> 0 Then
                        nDanShuang1 = 1
                    ElseIf ntmpFa <> 0 And ntmpDao <> 0 Then
                        nDanShuang1 = 1
                    Else
                        nDanShuang1 = SectionInf(nLeaveSec).nSection
                    End If
                Case "上下行"
                    lDifftemp1 = TianChuangInf(ntmpTianChuangSec1).lUpEmptyTime(1)
                    lDifftemp2 = TianChuangInf(ntmpTianChuangSec1).lUpEmptyTime(2)
                    ntmpFa = EmptyTimeCheck(lDifftemp1, lDifftemp2, lTim)
                    If ntmpFa <> 0 Then
                        TxIntervalKind1(nTrnNum, nStaterecord) = 302
                        TxDiffTime1(nTrnNum, nStaterecord) = TimeMinus(lTim, lDifftemp1)
                        TxDiffTrain1(nTrnNum, nStaterecord) = 0
                        TxIntervalKind2(nTrnNum, nStaterecord) = 302
                        TxDiffTime2(nTrnNum, nStaterecord) = TimeMinus(lDifftemp2, lTim)
                        TxDiffTrain2(nTrnNum, nStaterecord) = 0
                        FxIntervalKind1(nTrnNum, nStaterecord) = 302
                        FxDiffTime1(nTrnNum, nStaterecord) = TimeMinus(lTim, lDifftemp1)
                        FxDiffTrain1(nTrnNum, nStaterecord) = 0
                        FxIntervalKind2(nTrnNum, nStaterecord) = 302
                        FxDiffTime2(nTrnNum, nStaterecord) = TimeMinus(lDifftemp2, lTim)
                        FxDiffTrain2(nTrnNum, nStaterecord) = 0
                    End If
            End Select
        End If
        If ntmpTianChuangSec2 <> 0 Then
            Select Case TianChuangInf(ntmpTianChuangSec2).sTianChuangKind
                Case "上行"
                    lDifftemp1 = TianChuangInf(ntmpTianChuangSec2).lUpEmptyTime(1)
                    lDifftemp2 = TianChuangInf(ntmpTianChuangSec2).lUpEmptyTime(2)
                    ntmpFa = EmptyTimeCheck(lDifftemp1, lDifftemp2, lTim)
                    If ntmpFa <> 0 Then
                        nDanShuang2 = 1
                    Else
                        nDanShuang2 = SectionInf(nEnterSec).nSection
                    End If
                Case "下行"
                    lDifftemp1 = TianChuangInf(ntmpTianChuangSec2).lDownEmptyTime(1)
                    lDifftemp2 = TianChuangInf(ntmpTianChuangSec2).lDownEmptyTime(2)
                    ntmpFa = EmptyTimeCheck(lDifftemp1, lDifftemp2, lTim)
                    If ntmpFa <> 0 Then
                        nDanShuang2 = 1
                    Else
                        nDanShuang2 = SectionInf(nEnterSec).nSection
                    End If
                Case "上下行"
                    lDifftemp1 = TianChuangInf(ntmpTianChuangSec2).lUpEmptyTime(1)
                    lDifftemp2 = TianChuangInf(ntmpTianChuangSec2).lUpEmptyTime(2)
                    ntmpFa = EmptyTimeCheck(lDifftemp1, lDifftemp2, lTim)
                    If ntmpFa <> 0 Then
                        TxIntervalKind1(nTrnNum, nStaterecord) = 302
                        TxDiffTime1(nTrnNum, nStaterecord) = TimeMinus(lTim, lDifftemp1)
                        TxDiffTrain1(nTrnNum, nStaterecord) = 0
                        TxIntervalKind2(nTrnNum, nStaterecord) = 302
                        TxDiffTime2(nTrnNum, nStaterecord) = TimeMinus(lDifftemp2, lTim)
                        TxDiffTrain2(nTrnNum, nStaterecord) = 0
                        FxIntervalKind1(nTrnNum, nStaterecord) = 302
                        FxDiffTime1(nTrnNum, nStaterecord) = TimeMinus(lTim, lDifftemp1)
                        FxDiffTrain1(nTrnNum, nStaterecord) = 0
                        FxIntervalKind2(nTrnNum, nStaterecord) = 302
                        FxDiffTime2(nTrnNum, nStaterecord) = TimeMinus(lDifftemp2, lTim)
                        FxDiffTrain2(nTrnNum, nStaterecord) = 0
                    End If
            End Select
        End If
        If nDanShuang1 = 2 And nDanShuang2 = 2 Then '从双进双
            DoubleDoublePassCheck(nTrnNum, lTim, nStaterecord, nSdirection, nLeaveSec, nEnterSec)
        ElseIf nDanShuang1 = 2 And nDanShuang2 = 1 Then '从双进单
            DoubleSinglePassCheck(nTrnNum, lTim, nStaterecord, nSdirection, nLeaveSec, nEnterSec)
        ElseIf nDanShuang1 = 1 And nDanShuang2 = 1 Then '从单进单
            SingleSinglePassCheck(nTrnNum, lTim, nStaterecord, nSdirection, nLeaveSec, nEnterSec)
        ElseIf nDanShuang1 = 1 And nDanShuang2 = 2 Then '从单进双
            SingleDoublePassCheck(nTrnNum, lTim, nStaterecord, nSdirection, nLeaveSec, nEnterSec)
        End If
    End Sub

    Function TimeCheck(ByVal lAtimetemp As Long, ByVal lStimetemp As Long, _
    ByVal nStatemp As Integer, ByVal nTrnNum As Integer) As Integer
        'atimetemp到达当前时间，stimetemp出发当前时间，statemp当前车站,trnnum列车编号
        Dim lTemp1 As Long, lTemp2 As Long
        With TrainInf(nTrnNum)
            If .ComeStation = StationInf(nStatemp).sStationName Then '始发站
                If .Arrival(nStatemp) = -1 Or .Arrival(nStatemp) = .Starting(nStatemp) Then
                    lTemp1 = TimeMinus(.Starting(nStatemp), nStartTchTime(nTrnNum))
                    lTemp2 = .Starting(nStatemp)
                Else
                    lTemp1 = .Arrival(nStatemp)
                    lTemp2 = .Starting(nStatemp)
                End If
            ElseIf .NextStation = StationInf(nStatemp).sStationName Then '终到站
                If .Starting(nStatemp) = -1 Or .Arrival(nStatemp) = .Starting(nStatemp) Then
                    lTemp1 = .Arrival(nStatemp)
                    lTemp2 = TimeAdd(.Arrival(nStatemp), lEndTchTime(nTrnNum))
                Else
                    lTemp1 = .Arrival(nStatemp)
                    lTemp2 = .Starting(nStatemp)
                End If
            Else
                lTemp1 = .Arrival(nStatemp)
                lTemp2 = .Starting(nStatemp)
            End If
        End With
        TimeCheck = 0
        If lTemp1 = 0 Then
            'Stop
            lTemp1 = -1
        End If
        If lTemp1 <> -1 And lTemp2 <> -1 And lTemp1 <> lTemp2 Then
            If TimeMinus(lAtimetemp, lTemp1) < TimeMinus(lTemp2, lTemp1) Then '<=
                If TimeMinus(lTemp2, lAtimetemp) < TimeMinus(lTemp2, lTemp1) Then
                    '当前时间落入已有停站列车的停车时间范围
                    TimeCheck = 1
                End If
            ElseIf TimeMinus(lTemp1, lAtimetemp) < TimeMinus(lStimetemp, lAtimetemp) Then
                '当前时间范围包含已有停站列车的停车时间
                If TimeMinus(lStimetemp, lTemp1) < TimeMinus(lStimetemp, lAtimetemp) Then
                    TimeCheck = 1 '2
                    '包含
                End If
            End If
        End If
    End Function

    Function PassTimeCheck(ByVal lPtimetemp As Long, ByVal nStatemp As Integer, ByVal nTrnNum As Integer) As Integer
        'atimetemp到达当前时间，stimetemp出发当前时间，statemp当前车站,trnnum列车编号
        Dim lTemp1 As Long, lTemp2 As Long
        With TrainInf(nTrnNum)
            If .StartStation = StationInf(nStatemp).sStationName Then
                If .Arrival(nStatemp) = -1 Or .Arrival(nStatemp) = .Starting(nStatemp) Then
                    lTemp1 = TimeMinus(.Starting(nStatemp), nStartTchTime(nTrnNum))
                    lTemp2 = .Starting(nStatemp)
                Else
                    lTemp1 = .Arrival(nStatemp)
                    lTemp2 = .Starting(nStatemp)
                End If
            ElseIf .EndStation = StationInf(nStatemp).sStationName Then
                If .Starting(nStatemp) = -1 Or .Arrival(nStatemp) = .Starting(nStatemp) Then
                    lTemp1 = .Arrival(nStatemp)
                    lTemp2 = TimeAdd(.Arrival(nStatemp), nStartTchTime(nTrnNum))
                Else
                    lTemp1 = .Arrival(nStatemp)
                    lTemp2 = .Starting(nStatemp)
                End If
            Else
                lTemp1 = .Arrival(nStatemp)
                lTemp2 = .Starting(nStatemp)
            End If
        End With
        PassTimeCheck = 0
        If lTemp1 <> -1 And lTemp2 <> -1 And lTemp1 <> lTemp2 Then
            If TimeMinus(lPtimetemp, lTemp1) <= TimeMinus(lTemp1, lPtimetemp) Then
                If TimeMinus(lTemp2, lPtimetemp) <= TimeMinus(lPtimetemp, lTemp2) Then
                    '当前时间落入已有停站列车的停车时间范围
                    PassTimeCheck = 1
                End If
            End If
        End If
    End Function

    Function nTrainCompare(ByVal nTrnNum As Integer, ByVal nTrnNum1 As Integer, _
    ByVal nSdirection As Integer, ByVal nArrivalStart As Integer, _
    ByVal nStaterecord As Integer, ByVal sFromGo As String, ByVal sSameorNot As String, _
    ByVal sFGstation As String) As Integer
        'nTrnNum本次列车编号，timtemp当前时间，StateRecord当前车站
        'nSdirection方向,nArrivalStart本次车是到、发
        'nFromGo来或往, sFGStation来或往的车站
        '找是否参与比较的前行车

        Dim nTemp1 As Integer, nUpDowntemp As Integer
        Dim bTemp As Boolean

        nUpDowntemp = nDirection(nTrnNum)
        nTrainCompare = 0
        bTemp = False
        With TrainInf(nTrnNum1)
            If .TrainPuorNot <> 0 Then

                Select Case sFromGo
                    Case ""
                        bTemp = True
                    Case "来自"
                        If sSameorNot = "相同" Then
                            If sStationComeFrom(nTrnNum1, nStaterecord) = sFGstation Then
                                bTemp = True
                            End If
                        ElseIf sSameorNot = "不同" Then
                            If sStationComeFrom(nTrnNum1, nStaterecord) <> sFGstation Then
                                If sStationComeFrom(nTrnNum1, nStaterecord) <> "K" Then
                                    bTemp = True
                                End If
                            End If
                        End If
                    Case "去往"
                        If sSameorNot = "相同" Then
                            If sStationGoTo(nTrnNum1, nStaterecord) = sFGstation Then
                                bTemp = True
                            End If
                        ElseIf sSameorNot = "不同" Then
                            If sStationGoTo(nTrnNum1, nStaterecord) <> sFGstation Then
                                If sStationGoTo(nTrnNum1, nStaterecord) <> "K" Then
                                    bTemp = True
                                End If
                            End If
                        End If

                End Select
            End If
            If bTemp = True Then
                If .Arrival(nStaterecord) <> -1 Or .Starting(nStaterecord) <> -1 Then
                    If .nChaRunDirection = 9999 Then
                        If nArrivalStart = 0 Then '到达
                            nTemp1 = nFindHstaNum(nTrnNum, nStaterecord, nUpDowntemp)
                            If nUpDowntemp <> nSdirection Then
                                If TimeMinus(.Arrival(nTemp1), .Starting(nStaterecord)) _
                                    < TimeMinus(.Starting(nStaterecord), .Arrival(nTemp1)) Then
                                    nTrainCompare = nTrnNum1
                                End If
                            Else
                                If TimeMinus(.Arrival(nStaterecord), .Starting(nTemp1)) _
                                    < TimeMinus(.Starting(nTemp1), .Arrival(nStaterecord)) Then
                                    nTrainCompare = nTrnNum1
                                End If
                            End If
                        Else '出发
                            nTemp1 = nFindQstaNum(nTrnNum, nStaterecord, nUpDowntemp)
                            If nUpDowntemp <> nSdirection Then
                                If TimeMinus(.Arrival(nStaterecord), .Starting(nTemp1)) _
                                    < TimeMinus(.Starting(nTemp1), .Arrival(nStaterecord)) Then
                                    nTrainCompare = nTrnNum1
                                End If
                            Else
                                If TimeMinus(.Arrival(nTemp1), .Starting(nStaterecord)) _
                                    < TimeMinus(.Starting(nStaterecord), .Arrival(nTemp1)) Then
                                    nTrainCompare = nTrnNum1
                                End If
                            End If
                        End If
                    Else
                        nTrainCompare = nTrnNum1
                    End If
                End If
            End If
        End With
    End Function

    Function nPassOccupy(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal sLinetemp As String, _
    ByVal lAtimetemp As Long, ByVal lStimetemp As Long) As Integer
        'nstatemp当前车站,ntrnnum列车编号,nGudaoNum股道编号
        '判断该股道是否被占用，返回占用的列车编号
        Dim i As Integer
        Dim nTemp1 As Integer, nTemp2 As Integer

        nPassOccupy = 0
        nTemp1 = 1
        nTemp2 = UBound(TrainInf)
        If StationInf(nStatemp).sStationName = TrainInf(nTrnNum).ComeStation _
            Or StationInf(nStatemp).sStationName = TrainInf(nTrnNum).NextStation Then
            '始发终到站不允许正线停车
            'nPassOccupy = 1
        Else
            For i = nTemp1 To nTemp2
                With TrainInf(i)
                    If .Train <> "" Then
                        If TrainInf(i).Train <> TrainInf(nTrnNum).Train Then
                            'If .Arrival(nStatemp) = .Starting(nStatemp) And .Arrival(nStatemp) <> -1 And .Starting(nStatemp) <> -1 Then
                            '该列车通过该站
                            If .StopLine(nStatemp) = sLinetemp Or .StopLine(nStatemp) = "" Then

                                If TimeCheck(lAtimetemp, lStimetemp, nStatemp, i) <> 0 Then
                                    ''                            If TimeMinus(.Arrival(nStatemp), lAtimetemp) < TimeMinus(lAtimetemp, .Arrival(nStatemp)) Then
                                    ''                                If TimeMinus(lStimetemp, .Arrival(nStatemp)) < TimeMinus(.Arrival(nStatemp), lStimetemp) Then
                                    '停站列车通过的时间范围内有列车通过
                                    nPassOccupy = i
                                    Exit For
                                    'End If
                                End If
                            End If
                            'End If
                        End If
                    End If
                End With
            Next i
        End If
    End Function

    Function nGudaoOccupy(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal nGudaoNum As Integer) As Integer
        'nstatemp当前车站,ntrnnum列车编号,nGudaoNum股道编号
        '判断该股道是否被占用，返回占用的列车编号
        Dim i As Integer
        nGudaoOccupy = 0
        With StationInf(nStatemp)
            For i = 1 To UBound(nGuDaoTrain)
                If .sStLineNo(nGudaoNum) = TrainInf(nGuDaoTrain(i)).StopLine(nStatemp) Then
                    '该股道被占用
                    If .sStationName <> TrainInf(nTrnNum).StartStation And .sStationName <> TrainInf(nTrnNum).EndStation Then
                        If nGuDaoTrain(i) <> nTrnNum Then
                            nGudaoOccupy = nGuDaoTrain(i)
                            Exit For
                        End If
                    Else
                        If nGuDaoTrain(i) <> TrainInf(nTrnNum).TrainReturn(1) _
                            Or TrainInf(nGuDaoTrain(i)).TrainClass <> 12 _
                            Or TrainInf(nGuDaoTrain(i)).TrainClass <> 38 Then
                            nGudaoOccupy = nGuDaoTrain(i)
                            Exit For
                        End If
                    End If
                End If
            Next i
        End With
    End Function

    Function nGuDaoFaDaoOccupy(ByVal ntmpGuDaoTrain As Integer, ByVal ntmpStation As Integer, ByVal ntmpLine As Integer, ByVal lAtimetemp As Long, ByVal lStimetemp As Long) As Integer
        Dim i As Integer
        Dim lTemp As Long
        nGuDaoFaDaoOccupy = 0
        lTemp = 1
        For i = 1 To UBound(TrainInf)
            With TrainInf(i)
                If .Train <> "" Then
                    If .Arrival(ntmpStation) <> .Starting(ntmpStation) Then '该列车在该站停车
                        If .Arrival(ntmpStation) <> -1 And .Starting(ntmpStation) <> -1 Then '
                            If .StopLine(ntmpStation) = StationInf(ntmpStation).sStLineNo(ntmpLine) Then
                                If TimeMinus(.Arrival(ntmpStation), lStimetemp) < TimeMinus(lStimetemp, .Arrival(ntmpStation)) Then
                                    If TimeMinus(.Arrival(ntmpStation), lStimetemp) < lTemp Then
                                        nGuDaoFaDaoOccupy = i
                                        Exit For
                                    End If
                                ElseIf TimeMinus(lAtimetemp, .Starting(ntmpStation)) < TimeMinus(.Starting(ntmpStation), lAtimetemp) Then
                                    If TimeMinus(lAtimetemp, .Starting(ntmpStation)) < lTemp Then
                                        nGuDaoFaDaoOccupy = i
                                        Exit For
                                    End If
                                End If
                            End If
                        End If

                    Else '该列车在该站不停车
                        If TrainInf(i).Arrival(ntmpStation) <> -1 Then
                            If .StopLine(ntmpStation) = StationInf(ntmpStation).sStLineNo(ntmpLine) Then
                                If TimeMinus(.Arrival(ntmpStation), lAtimetemp) < TimeMinus(lAtimetemp, .Arrival(ntmpStation)) And _
                                TimeMinus(lStimetemp, .Arrival(ntmpStation)) < TimeMinus(.Arrival(ntmpStation), lStimetemp) Then '股道被占用
                                    nGuDaoFaDaoOccupy = i
                                    Exit For
                                End If
                            End If
                        End If
                    End If
                End If
            End With
        Next i
    End Function

    Function nFindTrainFenChaNum(ByVal nTrainNumber As Integer, ByVal nStationNumber As Integer) As Integer
        Dim i As Integer

        For i = 1 To TrainInf(nTrainNumber).NumWay
            If StationInf(nStationNumber).sStationName = TrainInf(nTrainNumber).Way1(i) Then
                nFindTrainFenChaNum = i
                Exit For
            End If
        Next i
    End Function

    Function nFindSecNumbyString(ByVal sSectionBeginStation As String, ByVal sSectionEndStation As String, _
    ByVal nUpDowntemp As Integer, ByVal nDataField As Integer) As Integer
        Dim i As Integer
        Dim sStationtemp1 As String, sStationtemp2 As String

        Dim nBeginSecNum As Integer, nEndSecNum As Integer

        nBeginSecNum = nBeginDataSection(nDataField)
        nEndSecNum = nEndDataSection(nDataField)

        nFindSecNumbyString = 0
        If nUpDowntemp = 1 Then
            sStationtemp1 = sSectionBeginStation
            sStationtemp2 = sSectionEndStation
        Else
            sStationtemp1 = sSectionEndStation
            sStationtemp2 = sSectionBeginStation
        End If
        For i = nBeginSecNum To nEndSecNum
            If StationInf(SectionInf(i).nHStation).sStationName = sStationtemp1 _
                And StationInf(SectionInf(i).nQStation).sStationName = sStationtemp2 Then
                nFindSecNumbyString = i
                Exit For
            End If
        Next i
    End Function

    Function nFindGuDaoNum(ByVal nTrnNum As Integer, ByVal nStatemp As Integer) As Integer
        Dim i As Integer
        For i = 1 To StationInf(nStatemp).nStLineNum
            If TrainInf(nTrnNum).StopLine(nStatemp) = StationInf(nStatemp).sStLineNo(i) Then
                nFindGuDaoNum = i
                Exit For
            End If
        Next i
    End Function

    Function nFindFenChaZhan(ByVal nStationtemp As Integer) As Integer
        nFindFenChaZhan = nStationtemp
        '    Dim i As Integer
        '    For i = 1 To UBound(FenChaStation) ' -1
        '        '找出分岔站的编号
        '        If StationInf(nStationtemp).sStationName = FenChaStation(i).sFenChaZhanName Then
        '            nFindFenChaZhan = i
        '            Exit For
        '        End If
        '    Next i
    End Function

    Function nEndDataSection(ByVal nDataField As Integer) As Integer
        If nDataField = 1 Then
            nEndDataSection = DataReadInf(nDataField).NowSectionEnd
        ElseIf nDataField > UBound(DataReadInf) Then
            nEndDataSection = UBound(SectionInf)
        Else
            nEndDataSection = DataReadInf(nDataField).NowSectionEnd
        End If

    End Function

    Function nCheckJianGe(ByVal nTrnNum As Integer, ByVal nStatemp As Integer) As Integer
        'ntrnnum本次列车编号，lstime当前时间，nstatemp前一车站
        '检查通过条件
        With TrainInf(nTrnNum)
            If TxDiffTime1(nTrnNum, nStatemp) > 0 Then
                If FxDiffTime1(nTrnNum, nStatemp) > 0 Then
                    '前行同、对向列车间隔均不满足
                    If TrainInf(TxDiffTrain1(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                        And TrainInf(FxDiffTrain1(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                        '前行同、对向列车等级均低于本次列车
                        FinInfTrn(TxDiffTrain1(nTrnNum, nStatemp))
                        FinInfTrn(FxDiffTrain1(nTrnNum, nStatemp))
                        If TxDiffTime2(nTrnNum, nStatemp) > 0 Then
                            If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                                If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同、对向列车等级均低于本次列车
                                    nCheckJianGe = 0 '可以通过
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同向列车等级低于本次列车、对向列车等级高于本次列车
                                    nCheckJianGe = 3 '与后行不够
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同向列车等级高于本次列车、对向列车等级低于本次列车
                                    nCheckJianGe = 3 '与后行不够
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同、对向列车等级均高于本次列车
                                    nCheckJianGe = 3 '与后行不够
                                End If
                            Else
                                If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同向列车等级低于本次列车、对向列车间隔时间满足
                                    nCheckJianGe = 0 '可以通过
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同向列车等级高于本次列车、对向列车间隔时间满足
                                    nCheckJianGe = 3 '与后行不够
                                End If
                            End If
                        Else
                            If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                                If TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行对向列车等级低于本次列车、同向列车间隔时间满足
                                    nCheckJianGe = 0 '可以通过
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行对向列车等级高于本次列车、同向列车间隔时间满足
                                    nCheckJianGe = 3 '与后行不够
                                End If
                            Else
                                nCheckJianGe = 0 '可以通过
                            End If
                        End If
                    ElseIf TrainInf(TxDiffTrain1(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                        And TrainInf(FxDiffTrain1(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                        '前行同向列车等级低于本次列车、对向列车等级高于本次列车
                        FinInfTrn(TxDiffTrain1(nTrnNum, nStatemp))
                        If TxDiffTime2(nTrnNum, nStatemp) > 0 Then
                            If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                                If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同、对向列车等级均低于本次列车
                                    nCheckJianGe = 2 '与前行不够
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同向列车等级低于本次列车、对向列车等级高于本次列车
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同向列车等级高于本次列车、对向列车等级低于本次列车
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同、对向列车等级均高于本次列车
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                End If
                            Else
                                If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同向列车等级低于本次列车、对向列车间隔时间满足
                                    nCheckJianGe = 2 '与前行不够
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同向列车等级高于本次列车、对向列车间隔时间满足
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                End If
                            End If
                        Else
                            If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                                If TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行对向列车等级低于本次列车、同向列车间隔时间满足
                                    nCheckJianGe = 2 '与前行不够
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行对向列车等级高于本次列车、同向列车间隔时间满足
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                End If
                            Else
                                nCheckJianGe = 2 '与前行不够
                            End If
                        End If
                    ElseIf TrainInf(TxDiffTrain1(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                        And TrainInf(FxDiffTrain1(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                        '前行同向列车等级高于本次列车、对向列车等级低于本次列车
                        FinInfTrn(FxDiffTrain1(nTrnNum, nStatemp))
                        If TxDiffTime2(nTrnNum, nStatemp) > 0 Then
                            If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                                If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同、对向列车等级均低于本次列车
                                    nCheckJianGe = 2 '与前行不够
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同向列车等级低于本次列车、对向列车等级高于本次列车
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同向列车等级高于本次列车、对向列车等级低于本次列车
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同、对向列车等级均高于本次列车
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                End If
                            Else
                                If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同向列车等级低于本次列车、对向列车间隔时间满足
                                    nCheckJianGe = 2 '与前行不够
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同向列车等级高于本次列车、对向列车间隔时间满足
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                End If
                            End If
                        Else
                            If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                                If TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行对向列车等级低于本次列车、同向列车间隔时间满足
                                    nCheckJianGe = 2 '与前行不够
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行对向列车等级高于本次列车、同向列车间隔时间满足
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                End If
                            Else
                                '后行对、同向列车间隔时间满足
                                nCheckJianGe = 2 '与前行不够
                            End If
                        End If
                    ElseIf TrainInf(TxDiffTrain1(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                        And TrainInf(FxDiffTrain1(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                        '前行同、对向列车等级均高于本次列车
                        If TxDiffTime2(nTrnNum, nStatemp) > 0 Then
                            If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                                If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同、对向列车等级均低于本次列车
                                    nCheckJianGe = 2 '与前行不够
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同向列车等级低于本次列车、对向列车等级高于本次列车
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同向列车等级高于本次列车、对向列车等级低于本次列车
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同、对向列车等级均高于本次列车
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                End If
                            Else
                                If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同向列车等级低于本次列车、对向列车间隔时间满足
                                    nCheckJianGe = 2 '与前行不够
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同向列车等级高于本次列车、对向列车间隔时间满足
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                End If
                            End If
                        Else
                            If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                                If TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行对向列车等级低于本次列车、同向列车间隔时间满足
                                    nCheckJianGe = 2 '与前行不够
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行对向列车等级高于本次列车、同向列车间隔时间满足
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                End If
                            Else
                                '后行对、同向列车间隔时间满足
                                nCheckJianGe = 2 '与前行不够
                            End If
                        End If
                    End If
                Else
                    '前行同向列车间隔不满足、对向列车间隔满足
                    If TrainInf(TxDiffTrain1(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                        '前行同向列车等级低于本次列车、对向列车间隔满足
                        FinInfTrn(TxDiffTrain1(nTrnNum, nStatemp))
                        If TxDiffTime2(nTrnNum, nStatemp) > 0 Then
                            If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                                '后行同、对向列车间隔均不满足
                                If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同、对向列车等级均低于本次列车
                                    nCheckJianGe = 0 '可以通过
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同向列车等级低于本次列车、对向列车等级高于本次列车
                                    nCheckJianGe = 3 '与后行不够
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同向列车等级高于本次列车、对向列车等级低于本次列车
                                    nCheckJianGe = 3 '与后行不够
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同、对向列车等级均高于本次列车
                                    nCheckJianGe = 3 '与后行不够
                                End If
                            Else
                                '后行同向列车间隔不满足、对向列车间隔满足
                                If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同向列车等级低于本次列车、对向列车间隔时间满足
                                    nCheckJianGe = 0 '可以通过
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同向列车等级高于本次列车、对向列车间隔时间满足
                                    nCheckJianGe = 3 '与后行不够
                                End If
                            End If
                        Else
                            If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                                '后行同向列车间隔满足、对向列车间隔不满足
                                If TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行对向列车等级低于本次列车、同向列车间隔时间满足
                                    nCheckJianGe = 0 '可以通过
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行对向列车等级高于本次列车、同向列车间隔时间满足
                                    nCheckJianGe = 3 '与后行不够
                                End If
                            Else
                                nCheckJianGe = 0 '可以通过
                            End If
                        End If
                    ElseIf TrainInf(TxDiffTrain1(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                        '前行同向列车等级高于本次列车、对向列车间隔满足
                        If TxDiffTime2(nTrnNum, nStatemp) > 0 Then
                            If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                                '后行同、对向列车间隔均不满足
                                If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同、对向列车等级均低于本次列车
                                    nCheckJianGe = 2 '与前行不够
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同向列车等级低于本次列车、对向列车等级高于本次列车
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同向列车等级高于本次列车、对向列车等级低于本次列车
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同、对向列车等级均高于本次列车
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                End If
                            Else
                                '后行同向列车间隔不满足、对向列车间隔满足
                                If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同向列车等级低于本次列车、对向列车间隔时间满足
                                    nCheckJianGe = 2 '与前行不够
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同向列车等级高于本次列车、对向列车间隔时间满足
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                End If
                            End If
                        Else
                            If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                                '后行同向列车间隔满足、对向列车间隔不满足
                                If TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行对向列车等级低于本次列车、同向列车间隔时间满足
                                    nCheckJianGe = 2 '与前行不够
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行对向列车等级高于本次列车、同向列车间隔时间满足
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                End If
                            Else
                                '后行对、同向列车间隔时间满足
                                nCheckJianGe = 2 '与前行不够
                            End If
                        End If
                    End If
                End If
            Else
                If FxDiffTime1(nTrnNum, nStatemp) > 0 Then
                    '前行同向列车间隔满足、对向列车间隔不满足
                    If TrainInf(FxDiffTrain1(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                        '前行同向列车间隔满足、对向列车等级低于本次列车
                        FinInfTrn(FxDiffTrain1(nTrnNum, nStatemp))
                        If TxDiffTime2(nTrnNum, nStatemp) > 0 Then
                            If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                                '后行同、对向列车间隔均不满足
                                If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同、对向列车等级均低于本次列车
                                    nCheckJianGe = 0 '可以通过
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同向列车等级低于本次列车、对向列车等级高于本次列车
                                    nCheckJianGe = 3 '与后行不够
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同向列车等级高于本次列车、对向列车等级低于本次列车
                                    nCheckJianGe = 3 '与后行不够
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同、对向列车等级均高于本次列车
                                    nCheckJianGe = 3 '与后行不够
                                End If
                            Else
                                '后行同向列车间隔不满足、对向列车间隔满足
                                If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同向列车等级低于本次列车、对向列车间隔时间满足
                                    nCheckJianGe = 0 '可以通过
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同向列车等级高于本次列车、对向列车间隔时间满足
                                    nCheckJianGe = 3 '与后行不够
                                End If
                            End If
                        Else
                            If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                                '后行同向列车间隔满足、对向列车间隔不满足
                                If TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行对向列车等级低于本次列车、同向列车间隔时间满足
                                    nCheckJianGe = 0 '可以通过
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行对向列车等级高于本次列车、同向列车间隔时间满足
                                    nCheckJianGe = 3 '与后行不够
                                End If
                            Else
                                nCheckJianGe = 0 '可以通过
                            End If
                        End If
                    ElseIf TrainInf(FxDiffTrain1(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                        '前行同向列车间隔满足、对向列车等级高于本次列车
                        If TxDiffTime2(nTrnNum, nStatemp) > 0 Then
                            If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                                '后行同、对向列车间隔均不满足
                                If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同、对向列车等级均低于本次列车
                                    nCheckJianGe = 2 '与前行不够
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同向列车等级低于本次列车、对向列车等级高于本次列车
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同向列车等级高于本次列车、对向列车等级低于本次列车
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                    And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同、对向列车等级均高于本次列车
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                End If
                            Else
                                '后行同列车间隔不满足、对向列车间隔满足
                                If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行同向列车等级低于本次列车、对向列车间隔时间满足
                                    nCheckJianGe = 2 '与前行不够
                                    FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行同向列车等级高于本次列车、对向列车间隔时间满足
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                End If
                            End If
                        Else
                            If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                                '后行同列车间隔满足、对向列车间隔不满足
                                If TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                    '后行对向列车等级低于本次列车、同向列车间隔时间满足
                                    nCheckJianGe = 2 '与前行不够
                                    FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                                ElseIf TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                    '后行对向列车等级高于本次列车、同向列车间隔时间满足
                                    nCheckJianGe = 1 '与前后行均不够，不能通过
                                End If
                            Else
                                nCheckJianGe = 2 '与前行不够
                            End If
                        End If
                    End If
                Else
                    '前行同向列车间隔满足、对向列车间隔满足
                    If TxDiffTime2(nTrnNum, nStatemp) > 0 Then
                        If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                            '后行同、对向列车间隔均不满足
                            If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                '后行同、对向列车等级均低于本次列车
                                nCheckJianGe = 0 '可以通过
                                FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                                FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                            ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal _
                                And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                '后行同向列车等级低于本次列车、对向列车等级高于本次列车
                                nCheckJianGe = 3 '与后行不够
                                FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                            ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                '后行同向列车等级高于本次列车、对向列车等级低于本次列车
                                nCheckJianGe = 3 '与后行不够
                                FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                            ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal _
                                And TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                '后行同、对向列车等级均高于本次列车
                                nCheckJianGe = 3 '与后行不够
                            End If
                        Else
                            '后行同向列车间隔不满足、对向列车间隔满足
                            If TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                '后行同向列车等级低于本次列车、对向列车间隔时间满足
                                nCheckJianGe = 0 '可以通过
                                FinInfTrn(TxDiffTrain2(nTrnNum, nStatemp))
                            ElseIf TrainInf(TxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                '后行同向列车等级高于本次列车、对向列车间隔时间满足
                                nCheckJianGe = 3 '与后行不够
                            End If
                        End If
                    Else
                        If FxDiffTime2(nTrnNum, nStatemp) > 0 Then
                            '后行同向列车间隔满足、对向列车间隔不满足
                            If TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal > .TrainClassCal Then
                                '后行对向列车等级低于本次列车、同向列车间隔时间满足
                                nCheckJianGe = 0 '可以通过
                                FinInfTrn(FxDiffTrain2(nTrnNum, nStatemp))
                            ElseIf TrainInf(FxDiffTrain2(nTrnNum, nStatemp)).TrainClassCal <= .TrainClassCal Then
                                '后行对向列车等级高于本次列车、同向列车间隔时间满足
                                nCheckJianGe = 3 '与后行不够
                            End If
                        Else
                            nCheckJianGe = 0 '可以通过
                        End If
                    End If
                End If
            End If
        End With
    End Function

    Function nBeginDataSection(ByVal nDataField As Integer) As Integer
        If nDataField = 1 Then
            nBeginDataSection = 1
        ElseIf nDataField > UBound(DataReadInf) Then
            nBeginDataSection = 1
        Else
            nBeginDataSection = DataReadInf(nDataField).NowSectionBegin
        End If
    End Function

    Function lRunTimeDifference(ByVal nTrainNumber1 As Integer, ByVal nTrainNumber2 As Integer, ByVal nStationH As Integer, ByVal nStationQ As Integer) As Long
        lRunTimeDifference = TimeRun(nTrainNumber1, nStationH, nStationQ) - TimeRun(nTrainNumber2, nStationH, nStationQ)
        If lRunTimeDifference < 0 Then
            lRunTimeDifference = 0
        End If
    End Function

    Function lQConflictDao(ByVal ntmpTrnNum As Integer, ByVal ntmpStatemp As Integer) As Long
        Dim ntmpInterval As Integer, ntmpTrain As Integer

        If TxDiffTime1(ntmpTrnNum, ntmpStatemp) < FxDiffTime1(ntmpTrnNum, ntmpStatemp) Then
            ntmpInterval = FxIntervalKind1(ntmpTrnNum, ntmpStatemp)
            ntmpTrain = FxDiffTrain1(ntmpTrnNum, ntmpStatemp)
        Else
            ntmpInterval = TxIntervalKind1(ntmpTrnNum, ntmpStatemp)
            ntmpTrain = TxDiffTrain1(ntmpTrnNum, ntmpStatemp)
        End If
        Select Case ntmpInterval
            Case 4, 5, 109, 108, 10, 15, 201, 211, 212, 213, 232, 233
                lQConflictDao = TrainInf(ntmpTrain).Starting(ntmpStatemp)
            Case 6, 107, 9, 17, 215, 231
                lQConflictDao = TrainInf(ntmpTrain).Arrival(ntmpStatemp)
        End Select
    End Function

    Function lCanLeftMoveStop(ByVal lNeedMoveTime As Long, ByVal nTrainNumber As Integer, ByVal nStationtemp As Integer, _
    ByVal nStopStation As Integer, ByVal nConflictKind As Integer, ByVal lNowTime As Long, ByVal nPhFx As Integer) As Long
        Dim nTrainNumbertemp As Integer, nStationHtemp As Integer, nUpDowntemp As Integer
        Dim lTxMovetemp As Long, lFxMovetemp As Long
        Dim nTxNumtemp As Integer, nFxNumtemp As Integer
        Dim nTemp As Integer
        Dim bTemp As Boolean
        Dim lTemp As Long

        Select Case nPhFx
            Case 1
                lCanLeftMoveStop = 0
                nStationHtemp = nStationtemp
                bTemp = True
                nTrainNumbertemp = nTrainNumber
                If nCanLeftMove(nStopStation) = 0 Then
                    If StationInf(nStopStation).sStationName = TrainInf(nTrainNumber).StartStation Then
                        lTemp = lNeedStop(nTrainNumber, nStopStation, 1)
                    Else
                        lTemp = lNeedStop(nTrainNumber, nStopStation, 4)
                    End If
                    If (TimeMinus(TrainInf(nTrainNumber).Starting(nStopStation), TrainInf(nTrainNumber).Arrival(nStopStation)) _
                        - lTemp) > lNeedMoveTime Then
                        '前方停车站停站时间允许左移
                        Do While bTemp = True
                            If StationInf(nStationHtemp).sStationName = StationInf(nStopStation).sStationName Then
                                bTemp = False
                            End If
                            lFxMovetemp = FxDiffTime1(nTrainNumbertemp, nStationHtemp)
                            nFxNumtemp = FxDiffTrain1(nTrainNumbertemp, nStationHtemp)
                            lTxMovetemp = TxDiffTime1(nTrainNumbertemp, nStationHtemp)
                            nTxNumtemp = TxDiffTrain1(nTrainNumbertemp, nStationHtemp)
                            If lNeedMoveTime <= -lTxMovetemp And lNeedMoveTime <= -lFxMovetemp Then
                                lCanLeftMoveStop = lNeedMoveTime
                            ElseIf lNeedMoveTime <= -lTxMovetemp And lNeedMoveTime > -lFxMovetemp Then
                                If TrainInf(nFxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal Then
                                    lCanLeftMoveStop = lNeedMoveTime
                                    FinInfTrn(nFxNumtemp)
                                Else
                                    lCanLeftMoveStop = 0
                                    Exit Do
                                End If
                            ElseIf lNeedMoveTime > -lTxMovetemp And lNeedMoveTime <= -lFxMovetemp Then
                                If TrainInf(nTxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal Then
                                    lCanLeftMoveStop = lNeedMoveTime
                                    FinInfTrn(nTxNumtemp)
                                Else
                                    lCanLeftMoveStop = 0
                                    Exit Do
                                End If
                            ElseIf lNeedMoveTime > -lTxMovetemp And lNeedMoveTime > -lFxMovetemp Then
                                If TrainInf(nTxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal _
                                    And TrainInf(nFxNumtemp).TrainClassCal > TrainInf(nTrainNumbertemp).TrainClassCal Then
                                    lCanLeftMoveStop = lNeedMoveTime
                                    FinInfTrn(nTxNumtemp)
                                    FinInfTrn(nFxNumtemp)
                                Else
                                    lCanLeftMoveStop = 0
                                    Exit Do
                                End If
                            End If
                            nTemp = nChaDirectionSt(nTrainNumber, nStationHtemp, 1)
                            If nTemp <> 0 Then
                                nStationHtemp = nTemp
                                nTrainNumbertemp = nChaTrnNum(nTrainNumbertemp)
                            Else
                                nTrainNumbertemp = nTrainNumber
                            End If
                            nUpDowntemp = nDirection(nTrainNumbertemp)
                            nStationHtemp = nFindHstaNum(nTrainNumbertemp, nStationHtemp, nUpDowntemp)
                            If StationInf(nStationHtemp).sStationName = TrainInf(nTrainNumbertemp).ComeStation Then
                                nStationHtemp = nBstation
                            End If
                        Loop
                    End If
                End If
            Case 2
                lCanLeftMoveStop = 0
                If nCanLeftMove(nStationtemp) = 0 Then
                    'If StationInf(nStationTemp).sStationName <> TrainInf(nTrainNumber).NextStation Then
                    lFxMovetemp = FxDiffTime1(nTrainNumber, nStationtemp)
                    nFxNumtemp = FxDiffTrain1(nTrainNumber, nStationtemp)
                    lTxMovetemp = TxDiffTime1(nTrainNumber, nStationtemp)
                    nTxNumtemp = TxDiffTrain1(nTrainNumber, nStationtemp)
                    If lNeedMoveTime <= -lTxMovetemp And lNeedMoveTime <= -lFxMovetemp Then
                        lCanLeftMoveStop = lNeedMoveTime
                    ElseIf lNeedMoveTime <= -lTxMovetemp And lNeedMoveTime > -lFxMovetemp Then
                        If TrainInf(nFxNumtemp).TrainClassCal > TrainInf(nTrainNumber).TrainClassCal Then
                            lCanLeftMoveStop = lNeedMoveTime
                            FinInfTrn(nFxNumtemp)
                        End If
                    ElseIf lNeedMoveTime > -lTxMovetemp And lNeedMoveTime <= -lFxMovetemp Then
                        If TrainInf(nTxNumtemp).TrainClassCal > TrainInf(nTrainNumber).TrainClassCal Then
                            lCanLeftMoveStop = lNeedMoveTime
                            FinInfTrn(nTxNumtemp)
                        End If
                    ElseIf lNeedMoveTime > -lTxMovetemp And lNeedMoveTime > -lFxMovetemp Then
                        If TrainInf(nTxNumtemp).TrainClassCal > TrainInf(nTrainNumber).TrainClassCal _
                            And TrainInf(nFxNumtemp).TrainClassCal > TrainInf(nTrainNumber).TrainClassCal Then
                            lCanLeftMoveStop = lNeedMoveTime
                            FinInfTrn(nTxNumtemp)
                            FinInfTrn(nFxNumtemp)
                        End If
                    End If
                End If
                'End If
        End Select
    End Function

    '由分岔站股道使用表得到各径路的股道使用情况
    Public Sub GetGuDaoYongTu(ByVal StrGuDao As String)
        Dim j As Integer
        If StrGuDao = "无" Then
            StrGuDao = ""
        End If
        ReDim FenChaZhanGuDaoUse(0)
        Dim i As Integer
        If Len(StrGuDao) > 0 Then
            If Right(StrGuDao, 1) = "," Or Right(StrGuDao, 1) = "，" Or Right(StrGuDao, 1) = "、" Then
            Else
                StrGuDao = StrGuDao & ","
            End If
            i = 1
            For j = 1 To Len(StrGuDao)
                If Mid(StrGuDao, j, 1) = "," Or Mid(StrGuDao, j, 1) = "，" Or Mid(StrGuDao, j, 1) = "、" Then
                    If Trim(Mid(StrGuDao, i, j - i)) <> "" Then
                        ReDim Preserve FenChaZhanGuDaoUse(UBound(FenChaZhanGuDaoUse) + 1)
                        FenChaZhanGuDaoUse(UBound(FenChaZhanGuDaoUse)) = Mid(StrGuDao, i, j - i)
                        i = j + 1
                    End If
                End If
            Next j
            'Next i
        Else
            Exit Sub
        End If
    End Sub

    Sub FinInfTrn(ByVal nTrn As Integer)
        Dim i As Integer
        Dim nTemp As Integer
        If TrainInf(nTrn).TrainPuorNot <> 0 Then
            nTemp = 0

            For i = 1 To UBound(EffTrain) - 1
                If nTrn = EffTrain(i) Then
                    nTemp = 1
                    Exit For
                End If
            Next i
            If nTemp = 0 Then
                EffTrain(UBound(EffTrain)) = nTrn
                TrainInf(nTrn).TrainPuorNot = 0
                TrainInf(nTrn).TrainClassCal = TrainInf(nTrn).TrainClass
                InfTrnNum = InfTrnNum + 1
                ReDim Preserve EffTrain(UBound(EffTrain) + 1)
            End If
        Else
            If TrainInf(nTrn).TrainClass = 12 Then
                nTemp = 0
                For i = 1 To UBound(EffTrain) - 1
                    If nTrn = EffTrain(i) Then
                        nTemp = 1
                        Exit For
                    End If
                Next i
                If nTemp = 0 Then
                    EffTrain(UBound(EffTrain)) = nTrn
                    TrainInf(nTrn).TrainPuorNot = 0
                    InfTrnNum = InfTrnNum + 1
                    ReDim Preserve EffTrain(UBound(EffTrain) + 1)
                End If
            End If
        End If
    End Sub

    Sub SingleSingleStartCheck(ByVal nTrainNumber As Integer, ByVal lTimeArrival As Long, _
    ByVal nStationNumber As Integer, ByVal nSdirection As Integer, _
    ByVal nSectionLeave As Integer, ByVal nSectionEnter As Integer)

        Dim BeforeTrainD As Integer, BeforeTrainF As Integer
        Dim AfterTrainD As Integer, AfterTrainF As Integer
        Dim lArrivalArrival As Long, lArrivalStart As Long
        Dim ii As Integer, jj As Integer
        Dim BeforeTrainD1 As Integer, BeforeTrainF1 As Integer
        Dim AfterTrainD1 As Integer, AfterTrainF1 As Integer
        Dim lArrivalArrival1 As Long, lArrivalStart1 As Long
        Dim ii1 As Integer, jj1 As Integer
        Dim nTemp As Integer

        If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nSameDirectionStart(BeforeTrainD1, nTrainNumber, lTimeArrival, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nSameDirectionStart(BeforeTrainF1, nTrainNumber, lTimeArrival, nStationNumber, 0, 1, nSectionEnter)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionStart(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionStart(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                jj1 = nSameDirectionStart(AfterTrainD1, nTrainNumber, lTimeArrival, nStationNumber, 1, 1, nSectionLeave)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nSameDirectionStart(AfterTrainF1, nTrainNumber, lTimeArrival, nStationNumber, 1, 1, nSectionEnter)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionStart(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionStart(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nConflictStart(BeforeTrainD1, nStationNumber, 0, 0, nSectionEnter)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nConflictStart(BeforeTrainF1, nStationNumber, 0, 1, nSectionEnter)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionStart(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionStart(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDanConflictH(lArrivalArrival1, AfterTrainD1, nTrainNumber, 2, _
                        nSectionLeave, nStationNumber, nSectionEnter)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nConflictStart(AfterTrainF1, nStationNumber, 1, 1, nSectionEnter)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionStart(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionStart(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

        Else
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nSameDirectionStart(BeforeTrainD, nTrainNumber, lTimeArrival, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nSameDirectionStart(BeforeTrainF, nTrainNumber, lTimeArrival, nStationNumber, 0, 1, nSectionEnter)
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nSameDirectionStart(AfterTrainD, nTrainNumber, lTimeArrival, nStationNumber, 1, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nSameDirectionStart(AfterTrainF, nTrainNumber, lTimeArrival, nStationNumber, 1, 1, nSectionEnter)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If

            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nConflictStart(BeforeTrainD, nStationNumber, 0, 0, nSectionEnter)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nConflictStart(BeforeTrainF, nStationNumber, 0, 1, nSectionEnter)
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nDanConflictH(lArrivalArrival, AfterTrainD, nTrainNumber, 2, _
                        nSectionLeave, nStationNumber, nSectionEnter)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nConflictStart(AfterTrainF, nStationNumber, 1, 1, nSectionEnter)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        End If
    End Sub

    Sub SingleDoubleArrivalCheck(ByVal nTrainNumber As Integer, ByVal lTimeArrival As Long, _
    ByVal nStationNumber As Integer, ByVal nSdirection As Integer, _
    ByVal nSectionLeave As Integer, ByVal nSectionEnter As Integer)

        Dim BeforeTrainD As Integer, BeforeTrainF As Integer
        Dim AfterTrainD As Integer, AfterTrainF As Integer
        Dim lArrivalArrival As Long, lArrivalStart As Long
        Dim ii As Integer, jj As Integer
        Dim BeforeTrainD1 As Integer, BeforeTrainF1 As Integer
        Dim AfterTrainD1 As Integer, AfterTrainF1 As Integer
        Dim lArrivalArrival1 As Long, lArrivalStart1 As Long
        Dim ii1 As Integer, jj1 As Integer
        Dim nTemp As Integer

        If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nSameDirectionArrival(BeforeTrainD1, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nSameDirectionArrival(BeforeTrainF1, nTrainNumber, nStationNumber, 0, 1, nSectionEnter, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nSameDirectionArrival(AfterTrainD1, nTrainNumber, nStationNumber, 1, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nSameDirectionArrival(AfterTrainF1, nTrainNumber, nStationNumber, 1, 1, nSectionEnter, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionArrival(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionArrival(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nConflictArrival(BeforeTrainD1, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDanConflictQ(lArrivalStart1, BeforeTrainF1, nTrainNumber, 1, _
                        nSectionLeave, nStationNumber, nSectionEnter) '到达与对出发
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionArrival(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nConflictArrival(AfterTrainF1, nStationNumber, 1, 1, nSectionLeave)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionArrival(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionArrival(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        Else
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nSameDirectionArrival(BeforeTrainD, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nSameDirectionArrival(BeforeTrainF, nTrainNumber, nStationNumber, 0, 1, nSectionEnter, lTimeArrival)
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "", "", "") '2到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nSameDirectionArrival(AfterTrainD, nTrainNumber, nStationNumber, 1, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nSameDirectionArrival(AfterTrainF, nTrainNumber, nStationNumber, 1, 1, nSectionEnter, lTimeArrival)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)


            '检查与对向列车的敌对情况
            '以下应进行修改
            '按单线车站间隔时间进行检查


            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nConflictArrival(BeforeTrainD, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nDanConflictQ(lArrivalStart, BeforeTrainF, nTrainNumber, 1, _
                        nSectionLeave, nStationNumber, nSectionEnter) '到达与对出发
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "", "", "") '到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nConflictArrival(AfterTrainD, nStationNumber, 1, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nConflictArrival(AfterTrainF, nStationNumber, 1, 1, nSectionLeave)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        End If
    End Sub

    Sub SingleDoubleStartCheck(ByVal nTrainNumber As Integer, ByVal lTimeArrival As Long, _
    ByVal nStationNumber As Integer, ByVal nSdirection As Integer, _
    ByVal nSectionLeave As Integer, ByVal nSectionEnter As Integer)

        Dim BeforeTrainD As Integer, BeforeTrainF As Integer
        Dim AfterTrainD As Integer, AfterTrainF As Integer
        Dim lArrivalArrival As Long, lArrivalStart As Long
        Dim ii As Integer, jj As Integer
        Dim BeforeTrainD1 As Integer, BeforeTrainF1 As Integer
        Dim AfterTrainD1 As Integer, AfterTrainF1 As Integer
        Dim lArrivalArrival1 As Long, lArrivalStart1 As Long
        Dim ii1 As Integer, jj1 As Integer
        Dim nTemp As Integer

        If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nSameDirectionStart(BeforeTrainD1, nTrainNumber, lTimeArrival, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nSameDirectionStart(BeforeTrainF1, nTrainNumber, lTimeArrival, nStationNumber, 0, 1, nSectionEnter)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionStart(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionStart(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nSameDirectionStart(AfterTrainD1, nTrainNumber, lTimeArrival, nStationNumber, 1, 0, nSectionLeave)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nSameDirectionStart(AfterTrainF1, nTrainNumber, lTimeArrival, nStationNumber, 1, 1, nSectionEnter)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionStart(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionStart(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionStart(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nConflictStart(BeforeTrainF1, nStationNumber, 0, 1, nSectionEnter)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionStart(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionStart(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionStart(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionStart(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionStart(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionStart(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

        Else
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nSameDirectionStart(BeforeTrainD, nTrainNumber, lTimeArrival, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nSameDirectionStart(BeforeTrainF, nTrainNumber, lTimeArrival, nStationNumber, 0, 1, nSectionEnter)
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '2到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nSameDirectionStart(AfterTrainD, nTrainNumber, lTimeArrival, nStationNumber, 1, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nSameDirectionStart(AfterTrainF, nTrainNumber, lTimeArrival, nStationNumber, 1, 1, nSectionEnter)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
            '检查与对向列车的敌对情况
            '以下应进行修改！！！！
            '只需按双线进行进路交叉检查

            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nConflictStart(BeforeTrainD, nStationNumber, 0, 0, nSectionEnter)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nConflictStart(BeforeTrainF, nStationNumber, 0, 1, nSectionEnter)
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nConflictStart(AfterTrainD, nStationNumber, 1, 0, nSectionEnter)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nConflictStart(AfterTrainF, nStationNumber, 1, 1, nSectionEnter)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        End If
    End Sub

    Sub DoubleDoubleStartCheck(ByVal nTrainNumber As Integer, ByVal lTimeArrival As Long, _
    ByVal nStationNumber As Integer, ByVal nSdirection As Integer, _
    ByVal nSectionLeave As Integer, ByVal nSectionEnter As Integer)

        Dim BeforeTrainD As Integer, BeforeTrainF As Integer
        Dim AfterTrainD As Integer, AfterTrainF As Integer
        Dim lArrivalArrival As Long, lArrivalStart As Long
        Dim ii As Integer, jj As Integer
        Dim BeforeTrainD1 As Integer, BeforeTrainF1 As Integer
        Dim AfterTrainD1 As Integer, AfterTrainF1 As Integer
        Dim lArrivalArrival1 As Long, lArrivalStart1 As Long
        Dim ii1 As Integer, jj1 As Integer
        Dim nTemp As Integer

        If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nSameDirectionStart(BeforeTrainD1, nTrainNumber, lTimeArrival, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nSameDirectionStart(BeforeTrainF1, nTrainNumber, lTimeArrival, nStationNumber, 0, 1, nSectionEnter)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionStart(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionStart(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nSameDirectionStart(AfterTrainD1, nTrainNumber, lTimeArrival, nStationNumber, 1, 0, nSectionLeave)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nSameDirectionStart(AfterTrainF1, nTrainNumber, lTimeArrival, nStationNumber, 1, 1, nSectionEnter)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionStart(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionStart(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionStart(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionStart(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionStart(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionStart(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionStart(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionStart(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionStart(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionStart(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        Else
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nSameDirectionStart(BeforeTrainD, nTrainNumber, lTimeArrival, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nSameDirectionStart(BeforeTrainF, nTrainNumber, lTimeArrival, nStationNumber, 0, 1, nSectionEnter)
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '2到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nSameDirectionStart(AfterTrainD, nTrainNumber, lTimeArrival, nStationNumber, 1, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nSameDirectionStart(AfterTrainF, nTrainNumber, lTimeArrival, nStationNumber, 1, 1, nSectionEnter)
                If jj = 810 Then
                    lArrivalStart = lArrivalStart + nMoveStepTime
                Else
                    lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                        jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
                End If
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

            '检查与反向的对向列车的敌对情况（只有到达和出发两种情况）

            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If
            If TrainInf(nTrainNumber).TrainClass = 38 _
                And StationInf(nStationNumber).sStationName = TrainInf(nTrainNumber).EndStation Then
                lArrivalArrival = -100000
                lArrivalStart = -100000
                CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
                CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 2)
            ElseIf TrainInf(nTrainNumber).TrainClass = 38 _
                And StationInf(nStationNumber).sStationName = TrainInf(nTrainNumber).StartStation Then
                lArrivalArrival = -100000
                lArrivalStart = -100000
                CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
                CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 2)
            Else
                If nTrainStopFanZheng(nTrainNumber, nStationNumber) = 1 Then
                    lArrivalArrival = -100000
                    lArrivalStart = -100000
                    BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                                    "", "", "") '到达条件
                    If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                        lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                        If TrainInf(BeforeTrainD).Arrival(nStationNumber) = TrainInf(BeforeTrainD).Starting(nStationNumber) Then
                            ii = 16 '对同通过，反发
                        Else
                            ii = 11 '对同到达，反发
                        End If
                        lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                            ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
                    End If
                    BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                                    "", "", "") '出发条件
                    If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                        lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                        If TrainInf(BeforeTrainF).Arrival(nStationNumber) = TrainInf(BeforeTrainF).Starting(nStationNumber) Then
                            jj = 16 '对同通过，反发
                        Else
                            jj = 12 '对同发车，反发
                        End If
                        lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                            jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
                    End If
                    CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

                    lArrivalArrival = -100000
                    lArrivalStart = -100000
                    AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                                    "", "", "") '到达条件
                    If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                        lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                        If TrainInf(AfterTrainD).Arrival(nStationNumber) = TrainInf(AfterTrainD).Starting(nStationNumber) Then
                            ii = 14 '反发，对同通过
                        Else
                            ii = 10 '反发，对同到达
                        End If
                        lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                            ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
                    End If
                    AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                                    "", "", "") '出发条件
                    If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                        lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                        If TrainInf(AfterTrainF).Arrival(nStationNumber) = TrainInf(AfterTrainF).Starting(nStationNumber) Then
                            jj = 14 '反发，对同通过
                        Else
                            jj = 12 '反发，对同出发
                        End If
                        lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                                jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
                    End If
                    CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
                Else
                    If nIfStopInWaiBaoZX(nTrainNumber, nStationNumber) = 0 Then
                        lArrivalArrival = -100000
                        lArrivalStart = -100000
                        BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                                        "", "", "") '到达条件
                        If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                            If TrainInf(BeforeTrainD).Arrival(nStationNumber) <> TrainInf(BeforeTrainD).Starting(nStationNumber) Then
                                '对向列车是否停站
                                If nTrainStopFanZheng(BeforeTrainD, nStationNumber) = 1 Then
                                    lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                                    ii = 11 '对反向到达，同出发
                                    lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                                        ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
                                End If
                            End If
                        End If
                        BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                                            "", "", "") '出发条件
                        If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                            If TrainInf(BeforeTrainF).Arrival(nStationNumber) <> TrainInf(BeforeTrainF).Starting(nStationNumber) Then
                                If nTrainStopFanZheng(BeforeTrainF, nStationNumber) = 1 Then
                                    lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                                    jj = 12 '对反向出发，同出发
                                    lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                                    jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
                                End If
                            End If
                        End If
                        CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
                        lArrivalArrival = -100000
                        lArrivalStart = -100000
                        AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                                        "", "", "") '到达条件
                        If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                            If TrainInf(AfterTrainD).Arrival(nStationNumber) <> TrainInf(AfterTrainD).Starting(nStationNumber) Then
                                If nTrainStopFanZheng(AfterTrainD, nStationNumber) = 1 Then
                                    lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                                    ii = 10 '同出发，对反向到达
                                    lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                                        ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
                                End If
                            End If
                        End If
                        AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                                        "", "", "") '出发条件
                        If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                            If TrainInf(AfterTrainF).Arrival(nStationNumber) <> TrainInf(AfterTrainF).Starting(nStationNumber) Then
                                If nTrainStopFanZheng(AfterTrainF, nStationNumber) = 1 Then
                                    lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                                    jj = 12 '同出发，对反向出发
                                    lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                                    jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
                                End If
                            End If
                        End If
                        CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
                    End If
                End If
            End If
        End If
    End Sub

    Function nIfTianChuangSec(ByVal ntmpSec As Integer) As Integer
        'Dim i As Integer
        nIfTianChuangSec = 0
        'For i = 1 To UBound(TianChuangInf) - 1
        '    If TianChuangInf(i).sShiGongSectionName = SectionInf(ntmpSec).sSecName Then
        '        nIfTianChuangSec = i
        '        Exit For
        '    End If
        'Next i
    End Function

    Function sStationGoTo(ByVal nTrainNumber As Integer, ByVal nStationNumber As Integer) As String
        Dim i As Integer
        sStationGoTo = ""
        With TrainInf(nTrainNumber)
            For i = 1 To .NumWay
                If StationInf(nStationNumber).sStationName = .Way1(i) Then
                    sStationGoTo = .Way2(i)
                    Exit For
                End If
            Next i
        End With
    End Function

    Function sStationComeFrom(ByVal nTrainNumber As Integer, ByVal nStationNumber As Integer) As String
        Dim i As Integer
        sStationComeFrom = ""
        For i = 1 To TrainInf(nTrainNumber).NumWay
            If StationInf(nStationNumber).sStationName = TrainInf(nTrainNumber).Way1(i) Then
                sStationComeFrom = TrainInf(nTrainNumber).Way3(i)
                Exit For
            End If
        Next i
    End Function

    Sub SingleSinglePassCheck(ByVal nTrainNumber As Integer, ByVal lTimeArrival As Long, _
    ByVal nStationNumber As Integer, ByVal nSdirection As Integer, _
    ByVal nSectionLeave As Integer, ByVal nSectionEnter As Integer)

        Dim BeforeTrainD As Integer, BeforeTrainF As Integer
        Dim AfterTrainD As Integer, AfterTrainF As Integer
        Dim lArrivalArrival As Long, lArrivalStart As Long
        Dim ii As Integer, jj As Integer
        Dim BeforeTrainD1 As Integer, BeforeTrainF1 As Integer
        Dim AfterTrainD1 As Integer, AfterTrainF1 As Integer
        Dim lArrivalArrival1 As Long, lArrivalStart1 As Long
        Dim ii1 As Integer, jj1 As Integer
        Dim nTemp As Integer

        If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nSameDirectionPass(BeforeTrainD1, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nSameDirectionPass(BeforeTrainF1, nTrainNumber, nStationNumber, 0, 1, nSectionEnter, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionPass(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionPass(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nSameDirectionPass(AfterTrainD1, nTrainNumber, nStationNumber, 1, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nSameDirectionPass(AfterTrainF1, nTrainNumber, nStationNumber, 1, 1, nSectionEnter, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionPass(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionPass(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nConflictPass(BeforeTrainD1, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDanConflictQ(lArrivalStart1, BeforeTrainF1, nTrainNumber, 0, _
                        nSectionLeave, nStationNumber, nSectionEnter) '对出发与通过
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionPass(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionPass(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDanConflictH(lArrivalArrival1, AfterTrainD1, nTrainNumber, 1, _
                        nSectionLeave, nStationNumber, nSectionEnter)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nConflictPass(AfterTrainF1, nStationNumber, 1, 1, nSectionLeave)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionPass(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionPass(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        Else
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nSameDirectionPass(BeforeTrainD, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nSameDirectionPass(BeforeTrainF, nTrainNumber, nStationNumber, 0, 1, nSectionEnter, lTimeArrival)
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nSameDirectionPass(AfterTrainD, nTrainNumber, nStationNumber, 1, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nSameDirectionPass(AfterTrainF, nTrainNumber, nStationNumber, 1, 1, nSectionEnter, lTimeArrival)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
            '检查对向
            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nConflictPass(BeforeTrainD, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nDanConflictQ(lArrivalStart, BeforeTrainF, nTrainNumber, 0, _
                        nSectionLeave, nStationNumber, nSectionEnter) '对出发与通过
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nDanConflictH(lArrivalArrival, AfterTrainD, nTrainNumber, 1, _
                        nSectionLeave, nStationNumber, nSectionEnter)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nConflictPass(AfterTrainF, nStationNumber, 1, 1, nSectionLeave)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        End If
    End Sub

    Sub SingleDoublePassCheck(ByVal nTrainNumber As Integer, ByVal lTimeArrival As Long, _
    ByVal nStationNumber As Integer, ByVal nSdirection As Integer, _
    ByVal nSectionLeave As Integer, ByVal nSectionEnter As Integer)

        Dim BeforeTrainD As Integer, BeforeTrainF As Integer
        Dim AfterTrainD As Integer, AfterTrainF As Integer
        Dim lArrivalArrival As Long, lArrivalStart As Long
        Dim ii As Integer, jj As Integer
        Dim BeforeTrainD1 As Integer, BeforeTrainF1 As Integer
        Dim AfterTrainD1 As Integer, AfterTrainF1 As Integer
        Dim lArrivalArrival1 As Long, lArrivalStart1 As Long
        Dim ii1 As Integer, jj1 As Integer
        Dim nTemp As Integer

        If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nSameDirectionPass(BeforeTrainD1, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nSameDirectionPass(BeforeTrainF1, nTrainNumber, nStationNumber, 0, 1, nSectionEnter, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionPass(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionPass(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nSameDirectionPass(AfterTrainD1, nTrainNumber, nStationNumber, 1, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nSameDirectionPass(AfterTrainF1, nTrainNumber, nStationNumber, 1, 1, nSectionEnter, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionPass(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionPass(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionPass(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDanConflictQ(lArrivalStart1, BeforeTrainF1, nTrainNumber, 0, _
                        nSectionLeave, nStationNumber, nSectionEnter) '对出发与通过
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionPass(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionPass(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionPass(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nConflictPass(AfterTrainF1, nStationNumber, 1, 1, nSectionLeave)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionPass(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionPass(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        Else
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nSameDirectionPass(BeforeTrainD, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nSameDirectionPass(BeforeTrainF, nTrainNumber, nStationNumber, 0, 1, nSectionEnter, lTimeArrival)
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '2到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nSameDirectionPass(AfterTrainD, nTrainNumber, nStationNumber, 1, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nSameDirectionPass(AfterTrainF, nTrainNumber, nStationNumber, 1, 1, nSectionEnter, lTimeArrival)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
            '检查与对向列车的敌对情况
            '以下应进行修改！！！
            '到达端检查进路交叉
            '出发端检查单线间隔




            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nConflictPass(BeforeTrainD, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nDanConflictQ(lArrivalStart, BeforeTrainF, nTrainNumber, 0, _
                        nSectionLeave, nStationNumber, nSectionEnter) '对出发与通过
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nConflictPass(AfterTrainD, nStationNumber, 1, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nConflictPass(AfterTrainF, nStationNumber, 1, 1, nSectionLeave)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        End If
    End Sub

    Sub DoubleDoublePassCheck(ByVal nTrainNumber As Integer, ByVal lTimeArrival As Long, _
    ByVal nStationNumber As Integer, ByVal nSdirection As Integer, _
    ByVal nSectionLeave As Integer, ByVal nSectionEnter As Integer)

        Dim BeforeTrainD As Integer, BeforeTrainF As Integer
        Dim AfterTrainD As Integer, AfterTrainF As Integer
        Dim lArrivalArrival As Long, lArrivalStart As Long
        Dim ii As Integer, jj As Integer
        Dim BeforeTrainD1 As Integer, BeforeTrainF1 As Integer
        Dim AfterTrainD1 As Integer, AfterTrainF1 As Integer
        Dim lArrivalArrival1 As Long, lArrivalStart1 As Long
        Dim ii1 As Integer, jj1 As Integer
        Dim nTemp As Integer

        If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nSameDirectionPass(BeforeTrainD1, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nSameDirectionPass(BeforeTrainF1, nTrainNumber, nStationNumber, 0, 1, nSectionEnter, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionPass(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionPass(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nSameDirectionPass(AfterTrainD1, nTrainNumber, nStationNumber, 1, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nSameDirectionPass(AfterTrainF1, nTrainNumber, nStationNumber, 1, 1, nSectionEnter, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionPass(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionPass(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionPass(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionPass(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionPass(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionPass(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionPass(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionPass(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionPass(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionPass(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        Else
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nSameDirectionPass(BeforeTrainD, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                If ii = 810 Then '区间交叉，前车快后车慢 then
                    If nStationNumber = SectionInf(nSectionLeave).nHStation Then
                        nTemp = SectionInf(nSectionLeave).nQStation
                    ElseIf nStationNumber = SectionInf(nSectionLeave).nQStation Then
                        nTemp = SectionInf(nSectionLeave).nHStation
                    End If
                    lArrivalArrival = TimeRun(nTrainNumber, nTemp, nStationNumber) - TimeActualRun(BeforeTrainD, nTemp, nStationNumber)
                Else
                    lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                        ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
                End If
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nSameDirectionPass(BeforeTrainF, nTrainNumber, nStationNumber, 0, 1, nSectionEnter, lTimeArrival)
                If jj = 820 Then '前车慢后车快，区间交叉 
                    lArrivalStart = lArrivalStart + nMoveStepTime
                Else
                    lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                        jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
                End If
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '2到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nSameDirectionPass(AfterTrainD, nTrainNumber, nStationNumber, 1, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nSameDirectionPass(AfterTrainF, nTrainNumber, nStationNumber, 1, 1, nSectionEnter, lTimeArrival)
                If jj = 810 Then
                    lArrivalStart = lArrivalStart + StationInf(nStationNumber).IKK(1)
                Else
                    lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                        jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
                End If
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
            '检查与反向的对向列车的敌对情况（只有到达和出发两种情况）

            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If

            Dim tmpStpoFanZheng As Integer
            tmpStpoFanZheng = nTrainStopFanZheng(nTrainNumber, nStationNumber)
            If tmpStpoFanZheng = 1 Then
                lArrivalArrival = -100000
                lArrivalStart = -100000
                BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                                "", "", "") '到达条件
                If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                    lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                    ii = 13 '对同到达，反通
                    lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                        ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
                End If
                BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                                    "", "", "") '出发条件
                If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                    lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                    jj = 14 '对同出发，反通
                    lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
                End If
                CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
                lArrivalArrival = -100000
                lArrivalStart = -100000
                AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                                "", "", "") '到达条件
                If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                    lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                    ii = 15 '反通，对同到达
                    lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                        ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
                End If
                AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                                "", "", "") '出发条件
                If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                    lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                    jj = 16 '反通，对同发车
                    lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
                End If
                CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
            Else
                Dim tmpNIfStopInWaiZbaoZX As Integer
                tmpNIfStopInWaiZbaoZX = nIfStopInWaiBaoZX(nTrainNumber, nStationNumber)
                If tmpNIfStopInWaiZbaoZX = 0 Then
                    lArrivalArrival = -100000
                    lArrivalStart = -100000
                    BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                                    "", "", "") '到达条件
                    If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                        If TrainInf(BeforeTrainD).Arrival(nStationNumber) <> TrainInf(BeforeTrainD).Starting(nStationNumber) Then
                            '对向列车是否停站
                            If nTrainStopFanZheng(BeforeTrainD, nStationNumber) = 1 Then
                                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                                ii = 13 '对反向到达，同通过
                                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                                    ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
                            End If
                        End If
                    End If
                    BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                                    "", "", "") '出发条件
                    If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                        If TrainInf(BeforeTrainF).Arrival(nStationNumber) <> TrainInf(BeforeTrainF).Starting(nStationNumber) Then
                            If nTrainStopFanZheng(BeforeTrainF, nStationNumber) = 1 Then
                                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                                jj = 14 '对反向出发，同通过
                                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                                jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
                            End If
                        End If
                    End If
                    CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

                    lArrivalArrival = -100000
                    lArrivalStart = -100000
                    AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                                    "", "", "") '到达条件
                    If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                        If TrainInf(AfterTrainD).Arrival(nStationNumber) <> TrainInf(AfterTrainD).Starting(nStationNumber) Then
                            If nTrainStopFanZheng(AfterTrainD, nStationNumber) = 1 Then
                                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                                ii = 15 '同通过，对反向到达
                                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                                    ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
                            End If
                        End If
                    End If
                    AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                                    "", "", "") '出发条件
                    If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                        If TrainInf(AfterTrainF).Arrival(nStationNumber) <> TrainInf(AfterTrainF).Starting(nStationNumber) Then
                            If nTrainStopFanZheng(AfterTrainF, nStationNumber) = 1 Then
                                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                                jj = 16 '同通过，对反向出发
                                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                                jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
                            End If
                        End If
                    End If
                    CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
                End If
            End If
        End If
    End Sub

    Function EmptyTimeCheck(ByVal lTimetemp1 As Long, ByVal lTimetemp2 As Long, ByVal lTimetemp As Long) As Integer
        EmptyTimeCheck = 0
        If TimeMinus(lTimetemp2, lTimetemp1) > TimeMinus(lTimetemp, lTimetemp1) Then
            If TimeMinus(lTimetemp2, lTimetemp1) >= TimeMinus(lTimetemp2, lTimetemp) Then
                EmptyTimeCheck = 1
            End If
        End If
    End Function

    Function nSameJiange(ByVal nTrnNum As Integer, ByVal nNum1 As Integer, ByVal lTtemp1 As Long, ByVal nJiange1 As Integer, ByVal lTtemp2 As Long, ByVal nNum2 As Integer, ByVal nJiange2 As Integer) As Integer
        '求nTrnNum列车的前、后间隔种类（lTtemp1、lTtemp2两者取大）
        With TrainInf(nTrnNum)
            If lTtemp1 < lTtemp2 Then
                If lTtemp2 > 0 Then
                    If TrainInf(nNum2).TrainClassCal > .TrainClassCal Then
                        If lTtemp1 > 0 Then
                            If TrainInf(nNum1).TrainClassCal > .TrainClassCal Then
                                nSameJiange = 2
                                FinInfTrn(nNum1)
                            Else
                                nSameJiange = 1
                                FinInfTrn(nNum2)
                            End If
                        Else
                            nSameJiange = 2
                        End If
                    Else
                        nSameJiange = 2
                    End If
                Else
                    nSameJiange = 2
                End If
            Else
                If lTtemp1 > 0 Then
                    If TrainInf(nNum1).TrainClassCal > .TrainClassCal Then
                        If lTtemp2 > 0 Then
                            If TrainInf(nNum2).TrainClassCal > .TrainClassCal Then
                                nSameJiange = 1
                                FinInfTrn(nNum2)
                            Else
                                nSameJiange = 2
                                FinInfTrn(nNum1)
                            End If
                        Else
                            nSameJiange = 1
                        End If
                    Else
                        nSameJiange = 1
                    End If
                Else
                    nSameJiange = 1
                End If
            End If
        End With
    End Function

    Function nSameDirectionStart(ByVal nTrainNumber As Integer, ByVal nTrainNumberBen As Integer, ByVal lTimetemp As Long, _
    ByVal nStationNumber As Integer, ByVal nQianorHou As Integer, ByVal nArrivalStart As Integer, ByVal nSectionNumber As Integer) As Integer
        Dim i As Integer
        Dim nTemp As Integer, nTempBen As Integer
        Dim ltmp1 As Long, ltmp2 As Long
        Dim tmpRunFir As Long
        Dim tmpRunBen As Long
        Dim bIfCheckJianGe As Boolean
        If SectionInf(nSectionNumber).sBlock = "自动闭塞" Then
            '双线自动闭塞
            If TrainInf(nTrainNumber).Arrival(nStationNumber) = TrainInf(nTrainNumber).Starting(nStationNumber) Then
                bIfCheckJianGe = True
                '            If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
                '                For i = 1 To TrainInf(nTrainNumber).NumWay
                '                    If TrainInf(nTrainNumber).Way1(i) = StationInf(nStationNumber).sStationName Then
                '                        nTemp = i
                '                        Exit For
                '                    End If
                '                Next i
                '                For i = 1 To TrainInf(nTrainNumberBen).NumWay
                '                    If TrainInf(nTrainNumberBen).Way1(i) = StationInf(nStationNumber).sStationName Then
                '                        nTempBen = i
                '                        Exit For
                '                    End If
                '                Next i
                '                If TrainInf(nTrainNumber).Way2(nTemp) <> TrainInf(nTrainNumberBen).Way2(nTempBen) _
                '                    Or TrainInf(nTrainNumber).Way2(nTemp) = "K" Then
                '                    bIfCheckJianGe = False
                '                End If
                '            End If
                If bIfCheckJianGe = True Then
                    If nQianorHou = 0 Then '与前比
                        If StationInf(nStationNumber).sStationName = StationInf(SectionInf(nSectionNumber).nHStation).sStationName Then
                            nTemp = SectionInf(nSectionNumber).nQStation
                        Else
                            nTemp = SectionInf(nSectionNumber).nHStation
                        End If
                        ltmp1 = TimeMinus(lTimetemp, TrainInf(nTrainNumber).Starting(nStationNumber))
                        tmpRunFir = TimeActualRun(nTrainNumber, nStationNumber, nTemp)
                        If tmpRunFir = 0 Then
                            nSameDirectionStart = 0 '通发
                        Else
                            tmpRunBen = TimeRun(nTrainNumberBen, nStationNumber, nTemp) + TimeQ(nTrainNumber, nStationNumber, nTemp)
                            If nArrivalStart = 1 Then '出发
                                If tmpRunFir >= tmpRunBen Then '前车慢后车快
                                    If ltmp1 < tmpRunFir - tmpRunBen Then
                                        nSameDirectionStart = 820 '区间交叉'前车慢后车快
                                    Else
                                        nSameDirectionStart = 0 '通发
                                    End If
                                Else
                                    nSameDirectionStart = 0 '通发
                                End If
                            Else
                                nSameDirectionStart = 0 '通发
                            End If

                        End If

                    ElseIf nQianorHou = 1 Then '与后比
                        If StationInf(nStationNumber).sStationName = StationInf(SectionInf(nSectionNumber).nHStation).sStationName Then
                            nTemp = SectionInf(nSectionNumber).nQStation
                        Else
                            nTemp = SectionInf(nSectionNumber).nHStation
                        End If
                        ltmp1 = TimeMinus(TrainInf(nTrainNumber).Starting(nStationNumber), lTimetemp)
                        tmpRunFir = TimeActualRun(nTrainNumber, nStationNumber, nTemp)
                        If tmpRunFir = 0 Then
                            nSameDirectionStart = 3 '发通
                        Else
                            tmpRunBen = TimeRun(nTrainNumberBen, nStationNumber, nTemp) + TimeQ(nTrainNumber, nStationNumber, nTemp)
                            If nArrivalStart = 1 Then '出发
                                If tmpRunFir <= tmpRunBen Then '前车快后车慢
                                    If ltmp1 < tmpRunBen - tmpRunFir Then
                                        nSameDirectionStart = 810 '区间交叉'前车快后车慢
                                    Else
                                        nSameDirectionStart = 3 '发通
                                    End If
                                Else
                                    nSameDirectionStart = 3 '发通
                                End If
                            Else
                                nSameDirectionStart = 3 '发通
                            End If
                        End If
                    End If
                Else
                    nSameDirectionStart = 600
                End If
            Else
                If nQianorHou = 0 Then '与前比
                    If nArrivalStart = 0 Then '到达
                        nSameDirectionStart = 2 '到发
                    ElseIf nArrivalStart = 1 Then '出发

                        'If nStationNumber = SectionInf(nSectionNumber).nHStation Then
                        '    nTemp = SectionInf(nSectionNumber).nQStation
                        'ElseIf nStationNumber = SectionInf(nSectionNumber).nQStation Then
                        '    nTemp = SectionInf(nSectionNumber).nHStation
                        'End If
                        'ltmp1 = TimeMinus(lTimetemp, TrainInf(nTrainNumber).Starting(nStationNumber))
                        'ltmp2 = TimeRun(nTrainNumberBen, nStationNumber, nTemp) - TimeRun(nTrainNumber, nStationNumber, nTemp)
                        'ltmp2 = Math.Abs(ltmp2)
                        'If ltmp1 < ltmp2 Then
                        '    nSameDirectionStart = 500 '区间交叉
                        'Else
                        '    'ltmp1 = TimeMinus(lTimetemp, TrainInf(nTrainNumber).Starting(nStationNumber))
                        '    'ltmp2 = TimeRun(nTrainNumberBen, nStationNumber, nTemp) + lDelayTime(nTrainNumberBen, nSectionNumber)
                        '    'ltmp2 = ltmp2 - (TimeRun(nTrainNumber, nStationNumber, nTemp) + lDelayTime(nTrainNumber, nSectionNumber))
                        '    'ltmp2 = Math.Abs(ltmp2)
                        '    'If ltmp1 < ltmp2 Then
                        '    '    nSameDirectionStart = 700 '区间交叉
                        '    'Else
                        '    nSameDirectionStart = 1 '发发
                        '    'End If
                        'End If
                        nSameDirectionStart = 1 '发发
                    End If
                ElseIf nQianorHou = 1 Then '与后比
                    If nArrivalStart = 0 Then '到达
                        nSameDirectionStart = 4 '发到
                    ElseIf nArrivalStart = 1 Then  '出发

                        If StationInf(nStationNumber).sStationName = StationInf(SectionInf(nSectionNumber).nHStation).sStationName Then
                            nTemp = SectionInf(nSectionNumber).nQStation
                        Else
                            nTemp = SectionInf(nSectionNumber).nHStation
                        End If
                        ltmp1 = TimeMinus(TrainInf(nTrainNumber).Starting(nStationNumber), lTimetemp)
                        tmpRunFir = TimeRun(nTrainNumber, nStationNumber, nTemp)
                        If tmpRunFir = 0 Then
                            nSameDirectionStart = 1 '发发
                        Else
                            tmpRunBen = TimeRun(nTrainNumberBen, nStationNumber, nTemp) + TimeQ(nTrainNumber, nStationNumber, nTemp)
                            If tmpRunFir <= tmpRunBen Then '前车快后车慢
                                If ltmp1 < tmpRunBen - tmpRunFir Then
                                    nSameDirectionStart = 810 '区间交叉'前车快后车慢
                                Else
                                    nSameDirectionStart = 1 '发发
                                End If
                            Else
                                nSameDirectionStart = 1 '发发
                            End If

                        End If
                    End If
                End If
            End If
        ElseIf SectionInf(nSectionNumber).sBlock = "半自动闭塞" Then
            If TrainInf(nTrainNumber).Arrival(nStationNumber) = TrainInf(nTrainNumber).Starting(nStationNumber) Then
                bIfCheckJianGe = True
                If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
                    For i = 1 To TrainInf(nTrainNumber).NumWay
                        If TrainInf(nTrainNumber).Way1(i) = StationInf(nStationNumber).sStationName Then
                            nTemp = i
                            Exit For
                        End If
                    Next i
                    For i = 1 To TrainInf(nTrainNumberBen).NumWay
                        If TrainInf(nTrainNumberBen).Way1(i) = StationInf(nStationNumber).sStationName Then
                            nTempBen = i
                            Exit For
                        End If
                    Next i
                    If TrainInf(nTrainNumber).Way2(nTemp) <> TrainInf(nTrainNumberBen).Way2(nTempBen) Then
                        bIfCheckJianGe = False
                    End If
                End If
                If bIfCheckJianGe = True Then
                    If nQianorHou = 0 Then '与前比
                        If StationInf(nStationNumber).sStationName = StationInf(SectionInf(nSectionNumber).nHStation).sStationName Then
                            nTemp = SectionInf(nSectionNumber).nQStation
                        Else
                            nTemp = SectionInf(nSectionNumber).nHStation
                        End If
                        ltmp1 = TimeMinus(lTimetemp, TrainInf(nTrainNumber).Starting(nStationNumber))
                        ltmp2 = TimeRun(nTrainNumberBen, nStationNumber, nTemp) - TimeRun(nTrainNumber, nStationNumber, nTemp)
                        ltmp2 = Math.Abs(ltmp2)
                        If ltmp1 < ltmp2 Then
                            nSameDirectionStart = 500 '区间交叉
                        Else
                            'ltmp1 = TimeMinus(lTimetemp, TrainInf(nTrainNumber).Starting(nStationNumber))
                            'ltmp2 = TimeRun(nTrainNumberBen, nStationNumber, nTemp) + lDelayTime(nTrainNumberBen, nSectionNumber)
                            'ltmp2 = ltmp2 - (TimeRun(nTrainNumber, nStationNumber, nTemp) + lDelayTime(nTrainNumber, nSectionNumber))
                            'ltmp2 = Math.Abs(ltmp2)
                            'If ltmp1 < ltmp2 Then
                            '    nSameDirectionStart = 700 '区间交叉
                            'Else
                            nSameDirectionStart = 103 '通发
                            'End If
                        End If
                    ElseIf nQianorHou = 1 Then '与后比
                        If StationInf(nStationNumber).sStationName = StationInf(SectionInf(nSectionNumber).nHStation).sStationName Then
                            nTemp = SectionInf(nSectionNumber).nQStation
                        Else
                            nTemp = SectionInf(nSectionNumber).nHStation
                        End If
                        If TimeMinus(TrainInf(nTrainNumber).Starting(nStationNumber), lTimetemp) _
                            < TimeRun(nTrainNumberBen, nStationNumber, nTemp) + TimeQ(nTrainNumberBen, nStationNumber, nTemp) - TimeRun(nTrainNumber, nStationNumber, nTemp) Then
                            nSameDirectionStart = 800 '区间交叉
                        Else
                            'ltmp1 = TimeMinus(TrainInf(nTrainNumber).Starting(nStationNumber), lTimetemp)
                            'ltmp2 = TimeRun(nTrainNumberBen, nStationNumber, nTemp) + lDelayTime(nTrainNumberBen, nSectionNumber)
                            'ltmp2 = ltmp2 - (TimeRun(nTrainNumber, nStationNumber, nTemp) + lDelayTime(nTrainNumber, nSectionNumber))
                            'ltmp2 = Math.Abs(ltmp2)
                            'If ltmp1 < ltmp2 Then
                            '    nSameDirectionStart = 800 '区间交叉
                            'Else
                            nSameDirectionStart = 106 '发通
                            ' End If
                        End If
                    End If
                Else
                    nSameDirectionStart = 600
                End If
            Else
                If nQianorHou = 0 Then '与前比
                    If nArrivalStart = 0 Then '到达
                        nSameDirectionStart = 101 '到发
                    ElseIf nArrivalStart = 1 Then '出发
                        If StationInf(nStationNumber).sStationName = StationInf(SectionInf(nSectionNumber).nHStation).sStationName Then
                            nTemp = SectionInf(nSectionNumber).nQStation
                        Else
                            nTemp = SectionInf(nSectionNumber).nHStation
                        End If
                        ltmp1 = TimeMinus(lTimetemp, TrainInf(nTrainNumber).Starting(nStationNumber))
                        ltmp2 = TimeRun(nTrainNumberBen, nStationNumber, nTemp) - TimeRun(nTrainNumber, nStationNumber, nTemp)
                        ltmp2 = Math.Abs(ltmp2)
                        If ltmp1 < ltmp2 Then
                            nSameDirectionStart = 500 '区间交叉
                        Else
                            'ltmp1 = TimeMinus(lTimetemp, TrainInf(nTrainNumber).Starting(nStationNumber))
                            'ltmp2 = TimeRun(nTrainNumberBen, nStationNumber, nTemp) + lDelayTime(nTrainNumberBen, nSectionNumber)
                            'ltmp2 = ltmp2 - (TimeRun(nTrainNumber, nStationNumber, nTemp) + lDelayTime(nTrainNumber, nSectionNumber))
                            'ltmp2 = Math.Abs(ltmp2)
                            'If ltmp1 < ltmp2 Then
                            '    nSameDirectionStart = 700 '区间交叉
                            'Else
                            nSameDirectionStart = 102 '发发
                            'End If
                        End If
                    End If
                ElseIf nQianorHou = 1 Then '与后比
                    If nArrivalStart = 0 Then '到达
                        nSameDirectionStart = 104 '发到
                    ElseIf nArrivalStart = 1 Then '出发
                        If StationInf(nStationNumber).sStationName = StationInf(SectionInf(nSectionNumber).nHStation).sStationName Then
                            nTemp = SectionInf(nSectionNumber).nQStation
                        Else
                            nTemp = SectionInf(nSectionNumber).nHStation
                        End If
                        If TimeMinus(TrainInf(nTrainNumber).Starting(nStationNumber), lTimetemp) _
                            < TimeRun(nTrainNumberBen, nStationNumber, nTemp) - TimeRun(nTrainNumber, nStationNumber, nTemp) Then
                            nSameDirectionStart = 800 '区间交叉
                        Else
                            'ltmp1 = TimeMinus(TrainInf(nTrainNumber).Starting(nStationNumber), lTimetemp)
                            'ltmp2 = TimeRun(nTrainNumberBen, nStationNumber, nTemp) + lDelayTime(nTrainNumberBen, nSectionNumber)
                            'ltmp2 = ltmp2 - (TimeRun(nTrainNumber, nStationNumber, nTemp) + lDelayTime(nTrainNumber, nSectionNumber))
                            'ltmp2 = Math.Abs(ltmp2)
                            'If ltmp1 < ltmp2 Then
                            '    nSameDirectionStart = 800 '区间交叉
                            'Else
                            nSameDirectionStart = 105 '发发
                            'End If
                        End If
                    End If
                End If
            End If
        End If
    End Function

    Function nDiffDirectionStart(ByVal nTrainNumber1 As Integer, ByVal nTrainNumber2 As Integer, _
    ByVal nStationNumber As Integer, ByVal nQianorHou As Integer, ByVal nArrivalStart2 As Integer) As Integer

        Dim nTemp As Integer
        Dim nArrivalStart1 As Integer

        nArrivalStart1 = 1
        If nArrivalStart2 = 0 Then
            If TrainInf(nTrainNumber2).Starting(nStationNumber) = TrainInf(nTrainNumber2).Arrival(nStationNumber) Then
                nArrivalStart2 = 2
            End If
        ElseIf nArrivalStart2 = 1 Then
            If TrainInf(nTrainNumber2).Starting(nStationNumber) = TrainInf(nTrainNumber2).Arrival(nStationNumber) Then
                nArrivalStart2 = 2
            End If
        End If
        '    If TrainInf(nTrainNumber1).TrainClass = 38 _
        '        And StationInf(nStationNumber).sStationName = TrainInf(nTrainNumber1).StartStation Then
        '        nTemp = 0
        '    Else
        '        nTemp = nPanDuanFenChaJiaoCha(nStationNumber, nTrainNumber1, nArrivalStart1, nTrainNumber2, nArrivalStart2)
        '    End If
        nTemp = 0
        If nTemp = 0 Then
            If nQianorHou = 0 Then
                nDiffDirectionStart = 250
            Else
                nDiffDirectionStart = 260
            End If
        Else
            If nQianorHou = 0 Then
                If nArrivalStart2 = 0 Then
                    '前到后发（11）
                    nDiffDirectionStart = 251
                ElseIf nArrivalStart2 = 1 Then
                    '前发后发（12）
                    nDiffDirectionStart = 252
                ElseIf nArrivalStart2 = 2 Then
                    '前通后发（16）
                    nDiffDirectionStart = 253
                End If
            Else
                If nArrivalStart2 = 0 Then
                    '前发后到（10）
                    nDiffDirectionStart = 261
                ElseIf nArrivalStart2 = 1 Then
                    '前发后发（12）
                    nDiffDirectionStart = 262
                ElseIf nArrivalStart2 = 2 Then
                    '前发后通（14）
                    nDiffDirectionStart = 263
                End If
            End If
        End If
    End Function

    Function nDanConflictH(ByVal lTimetemp As Long, ByVal nNum1 As Integer, _
        ByVal nNum2 As Integer, ByVal nQiTing As Integer, _
        ByVal nLeaveSection As Integer, ByVal ntmpStation As Integer, ByVal nEnterSection As Integer) As Integer

        Dim nDirectionTemp As Integer, nQianStation As Integer
        Dim nStationtemp As Integer
        Dim lTaoZhantime As Long, lYXtime As Long
        'nnum1前行车，nnum2本次车
        '本函数只在出发和通过时调用（双进单、单进单）

        If StationInf(ntmpStation).sStationName = StationInf(SectionInf(nEnterSection).nHStation).sStationName Then
            nStationtemp = SectionInf(nEnterSection).nHStation
            nQianStation = SectionInf(nEnterSection).nQStation
        ElseIf StationInf(ntmpStation).sStationName = StationInf(SectionInf(nEnterSection).nQStation).sStationName Then
            nStationtemp = SectionInf(nEnterSection).nQStation
            nQianStation = SectionInf(nEnterSection).nHStation
        End If
        If nStationtemp > nQianStation Then
            nDirectionTemp = 2
        Else
            nDirectionTemp = 1
        End If

        With TrainInf(nNum1)
            If .Starting(nQianStation) <> -1 Then
                lYXtime = TimeMinus(.Arrival(nStationtemp), .Starting(nQianStation))
                lYXtime = lYXtime + TimeRun(nNum2, nStationtemp, nQianStation)
                If nQiTing = 2 Then
                    lYXtime = lYXtime + TimeQ(nNum2, nStationtemp, nQianStation)
                End If
                If .Arrival(nQianStation) <> .Starting(nQianStation) Then
                    lTaoZhantime = StationInf(ntmpStation).lTaoHui(nDirectionTemp)
                    If lTaoZhantime = 0 Then
                        lTaoZhantime = 2 * 60
                    End If
                Else
                    lTaoZhantime = StationInf(ntmpStation).lTaoBu(nDirectionTemp)
                    If lTaoZhantime = 0 Then
                        lTaoZhantime = 4 * 60
                    End If
                End If
                lYXtime = lYXtime + lTaoZhantime
                If lTimetemp < lYXtime Then
                    nDanConflictH = 203
                Else
                    nDanConflictH = 204
                End If
            Else
                nDanConflictH = 207
            End If
        End With
    End Function

    Function nConflictStart(ByVal nTrainNumber As Integer, ByVal nStationNumber As Integer, _
    ByVal nQianorHou As Integer, ByVal nArrivalStart As Integer, ByVal nSectionNumber As Integer) As Integer
        If TrainInf(nTrainNumber).Arrival(nStationNumber) = TrainInf(nTrainNumber).Starting(nStationNumber) Then
            If nQianorHou = 0 Then '与前比
                nConflictStart = 208 '通发taohui
            ElseIf nQianorHou = 1 Then '与后比
                nConflictStart = 201 '发通taotong
            End If
        Else
            If nQianorHou = 0 Then '与前比
                If nArrivalStart = 0 Then '到达
                    nConflictStart = 208 '到发taohui
                ElseIf nArrivalStart = 1 Then '出发
                    nConflictStart = 210 '发发(不同方向不同时发车)taobutong
                End If
            ElseIf nQianorHou = 1 Then '与后比
                If nArrivalStart = 0 Then '到达
                    nConflictStart = 201 '发到taotong
                ElseIf nArrivalStart = 1 Then '出发
                    nConflictStart = 210 '发发(不同方向不同时发车)taobutong
                End If
            End If
        End If
    End Function
    Function nSameDirectionArrival(ByVal nTrainNumber As Integer, ByVal nTrainNumberBen As Integer, ByVal nStationNumber As Integer, _
    ByVal nQianorHou As Integer, ByVal nArrivalStart As Integer, ByVal nSectionNumber As Integer, ByVal lTimeArrival As Long) As Integer
        Dim i As Integer
        Dim nTemp As Integer, nTempBen As Integer
        Dim bIfCheckJianGe As Boolean
        Dim ltmp1 As Long ', ltmp2 As Long
        Dim tmpRunFir As Long
        Dim tmpRunBen As Long
        If SectionInf(nSectionNumber).sBlock = "自动闭塞" Then
            '自动闭塞
            If TrainInf(nTrainNumber).Arrival(nStationNumber) = TrainInf(nTrainNumber).Starting(nStationNumber) Then
                bIfCheckJianGe = True
                '            If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
                '                For i = 1 To TrainInf(nTrainNumber).NumWay
                '                    If TrainInf(nTrainNumber).Way1(i) = StationInf(nStationNumber).sStationName Then
                '                        nTemp = i
                '                        Exit For
                '                    End If
                '                Next i
                '                For i = 1 To TrainInf(nTrainNumberBen).NumWay
                '                    If TrainInf(nTrainNumberBen).Way1(i) = StationInf(nStationNumber).sStationName Then
                '                        nTempBen = i
                '                        Exit For
                '                    End If
                '                Next i
                '                bIfCheckJianGe = False
                '            End If
                If nQianorHou = 0 Then '与前比
                    If bIfCheckJianGe = False Then
                        If TrainInf(nTrainNumber).Way3(nTemp) = TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                            nSameDirectionArrival = 5 '通到
                        Else
                            nSameDirectionArrival = 600
                        End If
                    Else
                        If StationInf(nStationNumber).sStationName = StationInf(SectionInf(nSectionNumber).nHStation).sStationName Then
                            nTemp = SectionInf(nSectionNumber).nQStation
                        Else
                            nTemp = SectionInf(nSectionNumber).nHStation
                        End If
                        ltmp1 = TimeMinus(lTimeArrival, TrainInf(nTrainNumber).Starting(nStationNumber))
                        tmpRunFir = TimeActualRun(nTrainNumber, nTemp, nStationNumber)
                        If tmpRunFir = 0 Then
                            nSameDirectionArrival = 5 '通到
                        Else
                            tmpRunBen = TimeRun(nTrainNumberBen, nTemp, nStationNumber) + TimeT(nTrainNumberBen, nTemp, nStationNumber)
                            If nArrivalStart = 0 Then '到达
                                If tmpRunFir <= tmpRunBen Then '前车快后车慢
                                    If ltmp1 < tmpRunBen - tmpRunFir Then
                                        nSameDirectionArrival = 810 '区间交叉'前车快后车慢
                                    Else
                                        nSameDirectionArrival = 5 '通到
                                    End If
                                Else
                                    nSameDirectionArrival = 5 '通到
                                End If
                            Else
                                nSameDirectionArrival = 5 '通到
                            End If

                        End If
                        'nSameDirectionArrival = 5 '通到
                    End If
                ElseIf nQianorHou = 1 Then '与后比
                    If bIfCheckJianGe = False Then
                        If TrainInf(nTrainNumber).Way3(nTemp) = TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                            nSameDirectionArrival = 7 '到通
                        Else
                            nSameDirectionArrival = 600
                        End If
                    Else
                        'If nStationNumber = SectionInf(nSectionNumber).nHStation Then
                        '    nTemp = SectionInf(nSectionNumber).nQStation
                        'ElseIf nStationNumber = SectionInf(nSectionNumber).nQStation Then
                        '    nTemp = SectionInf(nSectionNumber).nHStation
                        'End If
                        'ltmp1 = TimeMinus(TrainInf(nTrainNumber).Starting(nStationNumber), lTimeArrival)
                        'tmpRunFir = TimeRun(nTrainNumber, nStationNumber, nTemp)
                        'tmpRunBen = TimeRun(nTrainNumberBen, nStationNumber, nTemp) + TimeQ(nTrainNumber, nStationNumber, nTemp)
                        'If nArrivalStart = 1 Then '出发
                        '    If tmpRunFir <= tmpRunBen Then '前车快后车慢
                        '        If ltmp1 < tmpRunBen - tmpRunFir Then
                        '            nSameDirectionArrival = 810 '区间交叉'前车快后车慢
                        '        Else
                        '            nSameDirectionArrival = 7 '到通
                        '        End If
                        '    Else
                        '        nSameDirectionArrival = 7 '到通
                        '    End If
                        'Else
                        '    nSameDirectionArrival = 7 '到通
                        'End If

                        nSameDirectionArrival = 7 '到通
                    End If
                End If
            Else
                bIfCheckJianGe = True
                '            If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
                '                For i = 1 To TrainInf(nTrainNumber).NumWay
                '                    If TrainInf(nTrainNumber).Way1(i) = StationInf(nStationNumber).sStationName Then
                '                        nTemp = i
                '                        Exit For
                '                    End If
                '                Next i
                '                For i = 1 To TrainInf(nTrainNumberBen).NumWay
                '                    If TrainInf(nTrainNumberBen).Way1(i) = StationInf(nStationNumber).sStationName Then
                '                        nTempBen = i
                '                        Exit For
                '                    End If
                '                Next i
                '                bIfCheckJianGe = False
                '            End If
                If nQianorHou = 0 Then '与前比
                    If nArrivalStart = 0 Then '到达
                        If bIfCheckJianGe = False Then
                            If TrainInf(nTrainNumber).Way3(nTemp) = TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                                nSameDirectionArrival = 6 '到到
                            Else
                                nSameDirectionArrival = 600
                            End If
                        Else
                            nSameDirectionArrival = 6 '到到
                        End If
                    ElseIf nArrivalStart = 1 Then '出发
                        If bIfCheckJianGe = False Then
                            If TrainInf(nTrainNumber).Way2(nTemp) = TrainInf(nTrainNumberBen).Way2(nTempBen) Then
                                nSameDirectionArrival = 4 '发到
                            Else
                                nSameDirectionArrival = 600
                            End If
                        Else
                            nSameDirectionArrival = 4 '发到
                        End If
                    End If
                ElseIf nQianorHou = 1 Then '与后比
                    If nArrivalStart = 0 Then '到达
                        If bIfCheckJianGe = False Then
                            If TrainInf(nTrainNumber).Way3(nTemp) = TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                                nSameDirectionArrival = 6 '到到
                            Else
                                nSameDirectionArrival = 600
                            End If
                        Else
                            nSameDirectionArrival = 6 '到到
                        End If
                    ElseIf nArrivalStart = 1 Then  '出发
                        If bIfCheckJianGe = False Then
                            If TrainInf(nTrainNumber).Way2(nTemp) = TrainInf(nTrainNumberBen).Way2(nTempBen) Then
                                nSameDirectionArrival = 2 '到发
                            Else
                                nSameDirectionArrival = 600
                            End If
                        Else

                            'If nStationNumber = SectionInf(nSectionNumber).nHStation Then
                            '    nTemp = SectionInf(nSectionNumber).nQStation
                            'ElseIf nStationNumber = SectionInf(nSectionNumber).nQStation Then
                            '    nTemp = SectionInf(nSectionNumber).nHStation
                            'End If
                            'ltmp1 = TimeMinus(TrainInf(nTrainNumber).Starting(nStationNumber), lTimeArrival)
                            'tmpRunFir = TimeRun(nTrainNumber, nStationNumber, nTemp)
                            'tmpRunBen = TimeRun(nTrainNumberBen, nStationNumber, nTemp) + TimeQ(nTrainNumber, nStationNumber, nTemp)
                            'If nArrivalStart = 1 Then '出发
                            '    If tmpRunFir <= tmpRunBen Then '前车快后车慢
                            '        If ltmp1 < tmpRunBen - tmpRunFir Then
                            '            nSameDirectionArrival = 810 '区间交叉'前车快后车慢
                            '        Else
                            '            nSameDirectionArrival = 2 '到发
                            '        End If
                            '    Else
                            '        nSameDirectionArrival = 2 '到发
                            '    End If
                            'Else
                            '    nSameDirectionArrival = 2 '到发
                            'End If

                            nSameDirectionArrival = 2 '到发
                        End If
                    End If
                End If
            End If
        ElseIf SectionInf(nSectionNumber).sBlock = "半自动闭塞" Then
            If TrainInf(nTrainNumber).Arrival(nStationNumber) = TrainInf(nTrainNumber).Starting(nStationNumber) Then
                bIfCheckJianGe = True
                If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
                    For i = 1 To TrainInf(nTrainNumber).NumWay
                        If TrainInf(nTrainNumber).Way1(i) = StationInf(nStationNumber).sStationName Then
                            nTemp = i
                            Exit For
                        End If
                    Next i
                    For i = 1 To TrainInf(nTrainNumberBen).NumWay
                        If TrainInf(nTrainNumberBen).Way1(i) = StationInf(nStationNumber).sStationName Then
                            nTempBen = i
                            Exit For
                        End If
                    Next i
                    bIfCheckJianGe = False
                End If
                If nQianorHou = 0 Then '与前比
                    If bIfCheckJianGe = False Then
                        If TrainInf(nTrainNumber).Way3(nTemp) = TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                            nSameDirectionArrival = 109 '通到
                        Else
                            nSameDirectionArrival = 600
                        End If
                    Else
                        nSameDirectionArrival = 109 '通到
                    End If
                ElseIf nQianorHou = 1 Then '与后比
                    If bIfCheckJianGe = False Then
                        If TrainInf(nTrainNumber).Way3(nTemp) = TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                            nSameDirectionArrival = 112 '通到
                        Else
                            nSameDirectionArrival = 600
                        End If
                    Else
                        nSameDirectionArrival = 112 '到通
                    End If
                End If
            Else
                bIfCheckJianGe = True
                If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
                    For i = 1 To TrainInf(nTrainNumber).NumWay
                        If TrainInf(nTrainNumber).Way1(i) = StationInf(nStationNumber).sStationName Then
                            nTemp = i
                            Exit For
                        End If
                    Next i
                    For i = 1 To TrainInf(nTrainNumberBen).NumWay
                        If TrainInf(nTrainNumberBen).Way1(i) = StationInf(nStationNumber).sStationName Then
                            nTempBen = i
                            Exit For
                        End If
                    Next i
                    bIfCheckJianGe = False
                End If
                If nQianorHou = 0 Then '与前比
                    If nArrivalStart = 0 Then '到达
                        If bIfCheckJianGe = False Then
                            If TrainInf(nTrainNumber).Way3(nTemp) = TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                                nSameDirectionArrival = 107 '到到
                            Else
                                nSameDirectionArrival = 600
                            End If
                        Else
                            nSameDirectionArrival = 107 '到到
                        End If
                    ElseIf nArrivalStart = 1 Then '出发
                        If bIfCheckJianGe = False Then
                            If TrainInf(nTrainNumber).Way3(nTemp) = TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                                nSameDirectionArrival = 108 '发到
                            Else
                                nSameDirectionArrival = 600
                            End If
                        Else
                            nSameDirectionArrival = 108 '发到
                        End If
                    End If
                ElseIf nQianorHou = 1 Then '与后比
                    If nArrivalStart = 0 Then '到达
                        If bIfCheckJianGe = False Then
                            If TrainInf(nTrainNumber).Way3(nTemp) = TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                                nSameDirectionArrival = 110 '到到
                            Else
                                nSameDirectionArrival = 600
                            End If
                        Else
                            nSameDirectionArrival = 110 '到到
                        End If
                    ElseIf nArrivalStart = 1 Then '出发
                        If bIfCheckJianGe = False Then
                            If TrainInf(nTrainNumber).Way3(nTemp) = TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                                nSameDirectionArrival = 111 '到发
                            Else
                                nSameDirectionArrival = 600
                            End If
                        Else
                            nSameDirectionArrival = 111 '到发
                        End If
                    End If
                End If
            End If
        End If
    End Function

    Function nDiffDirectionArrival(ByVal nTrainNumber1 As Integer, ByVal nTrainNumber2 As Integer, _
    ByVal nStationNumber As Integer, ByVal nQianorHou As Integer, ByVal nArrivalStart2 As Integer, ByVal lTimeArrival As Long) As Integer

        Dim nTemp As Integer
        Dim nArrivalStart1 As Integer

        nArrivalStart1 = 0
        If nArrivalStart2 = 0 Then
            If TrainInf(nTrainNumber2).Starting(nStationNumber) = TrainInf(nTrainNumber2).Arrival(nStationNumber) Then
                nArrivalStart2 = 2
            End If
        ElseIf nArrivalStart2 = 1 Then
            If TrainInf(nTrainNumber2).Starting(nStationNumber) = TrainInf(nTrainNumber2).Arrival(nStationNumber) Then
                nArrivalStart2 = 2
            End If
        End If
        '    If TrainInf(nTrainNumber1).TrainClass = 38 _
        '        And StationInf(nStationNumber).sStationName = TrainInf(nTrainNumber1).EndStation Then
        '        nTemp = 0
        '    Else
        '        nTemp = nPanDuanFenChaJiaoCha(nStationNumber, nTrainNumber1, nArrivalStart1, nTrainNumber2, nArrivalStart2)
        '    End If
        nTemp = 0
        If nTemp = 0 Then
            If nQianorHou = 0 Then
                nDiffDirectionArrival = 230 '分岔站无交叉时与前行列车的到达间隔
            Else
                nDiffDirectionArrival = 240 '分岔站无交叉时与后行列车的到达间隔
            End If
        Else
            If nQianorHou = 0 Then
                If nArrivalStart2 = 0 Then
                    '前到后到（9）
                    nDiffDirectionArrival = 231
                ElseIf nArrivalStart2 = 1 Then
                    '前发后到（10）
                    nDiffDirectionArrival = 232
                ElseIf nArrivalStart2 = 2 Then
                    '前通后到（15）
                    nDiffDirectionArrival = 233
                End If
            Else
                If nArrivalStart2 = 0 Then
                    '前到后到（9）
                    nDiffDirectionArrival = 241
                ElseIf nArrivalStart2 = 1 Then
                    '前到后发（11）
                    nDiffDirectionArrival = 242
                ElseIf nArrivalStart2 = 2 Then
                    '前到后通（13）
                    nDiffDirectionArrival = 243
                End If
            End If
        End If
    End Function

    Function nDanConflictQ(ByVal lTimetemp As Long, ByVal nNum1 As Integer, _
        ByVal nNum2 As Integer, ByVal nQiTing As Integer, _
        ByVal nLeaveSection As Integer, ByVal ntmpStation As Integer, ByVal nEnterSection As Integer) As Integer

        Dim nDirectionTemp As Integer
        Dim nStationtemp As Integer, nHuoStation As Integer
        Dim lTaoZhantime As Long, lYXtime As Long

        'nnum1前行车，nnum2本次车
        '本函数只在到达和通过时调用（单进单、单进双）

        If nQiTing = 0 Then '通过
            If StationInf(ntmpStation).sStationName = StationInf(SectionInf(nEnterSection).nHStation).sStationName Then
                nStationtemp = SectionInf(nEnterSection).nHStation
            ElseIf StationInf(ntmpStation).sStationName = StationInf(SectionInf(nEnterSection).nQStation).sStationName Then
                nStationtemp = SectionInf(nEnterSection).nQStation
            End If
        End If
        If StationInf(ntmpStation).sStationName = StationInf(SectionInf(nLeaveSection).nHStation).sStationName Then
            nStationtemp = SectionInf(nLeaveSection).nHStation
            nHuoStation = SectionInf(nLeaveSection).nQStation
        ElseIf StationInf(ntmpStation).sStationName = StationInf(SectionInf(nLeaveSection).nQStation).sStationName Then
            nStationtemp = SectionInf(nLeaveSection).nQStation
            nHuoStation = SectionInf(nLeaveSection).nHStation
        End If
        If nStationtemp > nHuoStation Then
            nDirectionTemp = 1
        Else
            nDirectionTemp = 2
        End If

        With TrainInf(nNum1)
            If .Arrival(nHuoStation) <> -1 Then
                lYXtime = TimeMinus(.Arrival(nHuoStation), .Starting(nStationtemp))
                lYXtime = lYXtime + TimeRun(nNum2, nHuoStation, nStationtemp)
                If nQiTing = 1 Then
                    lYXtime = lYXtime + TimeT(nNum2, nHuoStation, nStationtemp)
                End If
                If TrainInf(nNum2).Arrival(nHuoStation) <> TrainInf(nNum2).Starting(nHuoStation) Then
                    lYXtime = lYXtime + TimeQ(nNum2, nHuoStation, nStationtemp)
                    lTaoZhantime = StationInf(ntmpStation).lTaoHui(nDirectionTemp)
                    If lTaoZhantime = 0 Then
                        lTaoZhantime = 2 * 60
                    End If
                Else
                    lTaoZhantime = StationInf(ntmpStation).lTaoBu(nDirectionTemp)
                    If lTaoZhantime = 0 Then
                        lTaoZhantime = 4 * 60
                    End If
                End If
                lYXtime = lYXtime + lTaoZhantime
                If lTimetemp < lYXtime Then
                    nDanConflictQ = 211
                Else
                    nDanConflictQ = 212
                End If
            Else
                nDanConflictQ = 207
            End If
        End With
    End Function

    Function nConflictArrival(ByVal nTrainNumber As Integer, ByVal nStationNumber As Integer, _
    ByVal nQianorHou As Integer, ByVal nArrivalStart As Integer, ByVal nSectionNumber As Integer) As Integer
        If TrainInf(nTrainNumber).Arrival(nStationNumber) = TrainInf(nTrainNumber).Starting(nStationNumber) Then
            If nQianorHou = 0 Then '与前比
                nConflictArrival = 201 '通到taotong
            ElseIf nQianorHou = 1 Then '与后比
                nConflictArrival = 209 '到通taobu
            End If
        Else
            If nQianorHou = 0 Then '与前比
                If nArrivalStart = 0 Then '到达
                    nConflictArrival = 215 '到到taobu
                ElseIf nArrivalStart = 1 Then '出发
                    nConflictArrival = 201 '发到taotong
                End If
            ElseIf nQianorHou = 1 Then '与后比
                If nArrivalStart = 0 Then '到达
                    nConflictArrival = 216 '到到taobu
                ElseIf nArrivalStart = 1 Then '出发
                    nConflictArrival = 208 '到发taohui
                End If
            End If
        End If
    End Function
    Sub CalFxJiange(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal nNum1 As Integer, ByVal lTtemp1 As Long, ByVal nJiange1 As Integer, ByVal lTtemp2 As Long, ByVal nNum2 As Integer, ByVal nJiange2 As Integer, ByVal QianHou As Integer)
        Dim i As Integer

        If lTtemp1 < lTtemp2 Then
            If lTtemp2 > 0 Then
                If TrainInf(nNum2).TrainClassCal > TrainInf(nTrnNum).TrainClassCal Then
                    If lTtemp1 > 0 Then
                        If TrainInf(nNum1).TrainClassCal > TrainInf(nTrnNum).TrainClassCal Then
                            If QianHou = 1 Then
                                FxIntervalKind1(nTrnNum, nStatemp) = nJiange2
                                FxDiffTime1(nTrnNum, nStatemp) = lTtemp2
                                FxDiffTrain1(nTrnNum, nStatemp) = nNum2
                            Else
                                FxIntervalKind2(nTrnNum, nStatemp) = nJiange2
                                FxDiffTime2(nTrnNum, nStatemp) = lTtemp2
                                FxDiffTrain2(nTrnNum, nStatemp) = nNum2
                            End If
                            FinInfTrn(nNum1)
                        Else
                            If QianHou = 1 Then
                                FxIntervalKind1(nTrnNum, nStatemp) = nJiange1
                                FxDiffTime1(nTrnNum, nStatemp) = lTtemp1
                                FxDiffTrain1(nTrnNum, nStatemp) = nNum1
                            Else
                                FxIntervalKind2(nTrnNum, nStatemp) = nJiange1
                                FxDiffTime2(nTrnNum, nStatemp) = lTtemp1
                                FxDiffTrain2(nTrnNum, nStatemp) = nNum1
                            End If
                            FinInfTrn(nNum2)
                        End If
                    Else
                        If QianHou = 1 Then
                            FxIntervalKind1(nTrnNum, nStatemp) = nJiange2
                            FxDiffTime1(nTrnNum, nStatemp) = lTtemp2
                            FxDiffTrain1(nTrnNum, nStatemp) = nNum2
                        Else
                            FxIntervalKind2(nTrnNum, nStatemp) = nJiange2
                            FxDiffTime2(nTrnNum, nStatemp) = lTtemp2
                            FxDiffTrain2(nTrnNum, nStatemp) = nNum2
                        End If
                    End If
                Else
                    If QianHou = 1 Then
                        FxIntervalKind1(nTrnNum, nStatemp) = nJiange2
                        FxDiffTime1(nTrnNum, nStatemp) = lTtemp2
                        FxDiffTrain1(nTrnNum, nStatemp) = nNum2
                    Else
                        FxIntervalKind2(nTrnNum, nStatemp) = nJiange2
                        FxDiffTime2(nTrnNum, nStatemp) = lTtemp2
                        FxDiffTrain2(nTrnNum, nStatemp) = nNum2
                    End If
                End If
            Else
                If QianHou = 1 Then
                    FxIntervalKind1(nTrnNum, nStatemp) = nJiange2
                    FxDiffTime1(nTrnNum, nStatemp) = lTtemp2
                    FxDiffTrain1(nTrnNum, nStatemp) = nNum2
                Else
                    FxIntervalKind2(nTrnNum, nStatemp) = nJiange2
                    FxDiffTime2(nTrnNum, nStatemp) = lTtemp2
                    FxDiffTrain2(nTrnNum, nStatemp) = nNum2
                End If
            End If
        Else
            If lTtemp1 > 0 Then
                If TrainInf(nNum1).TrainClassCal > TrainInf(nTrnNum).TrainClassCal Then
                    If lTtemp2 > 0 Then
                        If TrainInf(nNum2).TrainClassCal > TrainInf(nTrnNum).TrainClassCal Then
                            If QianHou = 1 Then
                                FxIntervalKind1(nTrnNum, nStatemp) = nJiange1
                                FxDiffTime1(nTrnNum, nStatemp) = lTtemp1
                                FxDiffTrain1(nTrnNum, nStatemp) = nNum1
                            Else
                                FxIntervalKind2(nTrnNum, nStatemp) = nJiange1
                                FxDiffTime2(nTrnNum, nStatemp) = lTtemp1
                                FxDiffTrain2(nTrnNum, nStatemp) = nNum1
                            End If
                            FinInfTrn(nNum2)
                        Else
                            If QianHou = 1 Then
                                FxIntervalKind1(nTrnNum, nStatemp) = nJiange2
                                FxDiffTime1(nTrnNum, nStatemp) = lTtemp2
                                FxDiffTrain1(nTrnNum, nStatemp) = nNum2
                            Else
                                FxIntervalKind2(nTrnNum, nStatemp) = nJiange2
                                FxDiffTime2(nTrnNum, nStatemp) = lTtemp2
                                FxDiffTrain2(nTrnNum, nStatemp) = nNum2
                            End If
                            FinInfTrn(nNum1)
                        End If
                    Else
                        If QianHou = 1 Then
                            FxIntervalKind1(nTrnNum, nStatemp) = nJiange1
                            FxDiffTime1(nTrnNum, nStatemp) = lTtemp1
                            FxDiffTrain1(nTrnNum, nStatemp) = nNum1
                        Else
                            FxIntervalKind2(nTrnNum, nStatemp) = nJiange1
                            FxDiffTime2(nTrnNum, nStatemp) = lTtemp1
                            FxDiffTrain2(nTrnNum, nStatemp) = nNum1
                        End If
                    End If
                Else
                    If QianHou = 1 Then
                        FxIntervalKind1(nTrnNum, nStatemp) = nJiange1
                        FxDiffTime1(nTrnNum, nStatemp) = lTtemp1
                        FxDiffTrain1(nTrnNum, nStatemp) = nNum1
                    Else
                        FxIntervalKind2(nTrnNum, nStatemp) = nJiange1
                        FxDiffTime2(nTrnNum, nStatemp) = lTtemp1
                        FxDiffTrain2(nTrnNum, nStatemp) = nNum1
                    End If
                End If
            Else
                If QianHou = 1 Then
                    FxIntervalKind1(nTrnNum, nStatemp) = nJiange1
                    FxDiffTime1(nTrnNum, nStatemp) = lTtemp1
                    FxDiffTrain1(nTrnNum, nStatemp) = nNum1
                Else
                    FxIntervalKind2(nTrnNum, nStatemp) = nJiange1
                    FxDiffTime2(nTrnNum, nStatemp) = lTtemp1
                    FxDiffTrain2(nTrnNum, nStatemp) = nNum1
                End If
            End If
        End If
        For i = 1 To UBound(StationInf)
            If StationInf(nStatemp).sStationName = StationInf(i).sStationName _
                And i <> nStatemp Then
                If QianHou = 1 Then
                    FxIntervalKind1(nTrnNum, i) = FxIntervalKind1(nTrnNum, nStatemp)
                    FxDiffTime1(nTrnNum, i) = FxDiffTime1(nTrnNum, nStatemp)
                    FxDiffTrain1(nTrnNum, i) = FxDiffTrain1(nTrnNum, nStatemp)
                Else
                    FxIntervalKind2(nTrnNum, i) = FxIntervalKind2(nTrnNum, nStatemp)
                    FxDiffTime2(nTrnNum, i) = FxDiffTime2(nTrnNum, nStatemp)
                    FxDiffTrain2(nTrnNum, i) = FxDiffTrain2(nTrnNum, nStatemp)
                End If
            End If
        Next i
    End Sub

    Sub CalTxJiange(ByVal nTrnNum As Integer, ByVal nStatemp As Integer, ByVal nNum1 As Integer, ByVal lTtemp1 As Long, _
    ByVal nJiange1 As Integer, ByVal lTtemp2 As Long, ByVal nNum2 As Integer, ByVal nJiange2 As Integer, ByVal QianHou As Integer)
        '求nTrnNum列车的前后间隔时间（lTtemp1、lTtemp2两者取大）
        Dim i As Integer

        With TrainInf(nTrnNum)
            If lTtemp1 < lTtemp2 Then
                If lTtemp2 > 0 Then
                    If TrainInf(nNum2).TrainClassCal > .TrainClassCal Then
                        If lTtemp1 > 0 Then
                            If TrainInf(nNum1).TrainClassCal > .TrainClassCal Then
                                If QianHou = 1 Then
                                    TxIntervalKind1(nTrnNum, nStatemp) = nJiange2
                                    TxDiffTime1(nTrnNum, nStatemp) = lTtemp2
                                    TxDiffTrain1(nTrnNum, nStatemp) = nNum2
                                Else
                                    TxIntervalKind2(nTrnNum, nStatemp) = nJiange2
                                    TxDiffTime2(nTrnNum, nStatemp) = lTtemp2
                                    TxDiffTrain2(nTrnNum, nStatemp) = nNum2
                                End If
                                FinInfTrn(nNum1)
                            Else
                                If QianHou = 1 Then
                                    TxIntervalKind1(nTrnNum, nStatemp) = nJiange1
                                    TxDiffTime1(nTrnNum, nStatemp) = lTtemp1
                                    TxDiffTrain1(nTrnNum, nStatemp) = nNum1
                                Else
                                    TxIntervalKind2(nTrnNum, nStatemp) = nJiange1
                                    TxDiffTime2(nTrnNum, nStatemp) = lTtemp1
                                    TxDiffTrain2(nTrnNum, nStatemp) = nNum1
                                End If
                                FinInfTrn(nNum2)
                            End If
                        Else
                            If QianHou = 1 Then
                                TxIntervalKind1(nTrnNum, nStatemp) = nJiange2
                                TxDiffTime1(nTrnNum, nStatemp) = lTtemp2
                                TxDiffTrain1(nTrnNum, nStatemp) = nNum2
                            Else
                                TxIntervalKind2(nTrnNum, nStatemp) = nJiange2
                                TxDiffTime2(nTrnNum, nStatemp) = lTtemp2
                                TxDiffTrain2(nTrnNum, nStatemp) = nNum2
                            End If
                        End If
                    Else
                        If QianHou = 1 Then
                            TxIntervalKind1(nTrnNum, nStatemp) = nJiange2
                            TxDiffTime1(nTrnNum, nStatemp) = lTtemp2
                            TxDiffTrain1(nTrnNum, nStatemp) = nNum2
                        Else
                            TxIntervalKind2(nTrnNum, nStatemp) = nJiange2
                            TxDiffTime2(nTrnNum, nStatemp) = lTtemp2
                            TxDiffTrain2(nTrnNum, nStatemp) = nNum2
                        End If
                    End If
                Else
                    If QianHou = 1 Then
                        TxIntervalKind1(nTrnNum, nStatemp) = nJiange2
                        TxDiffTime1(nTrnNum, nStatemp) = lTtemp2
                        TxDiffTrain1(nTrnNum, nStatemp) = nNum2
                    Else
                        TxIntervalKind2(nTrnNum, nStatemp) = nJiange2
                        TxDiffTime2(nTrnNum, nStatemp) = lTtemp2
                        TxDiffTrain2(nTrnNum, nStatemp) = nNum2
                    End If
                End If
            Else
                If lTtemp1 > 0 Then
                    If TrainInf(nNum1).TrainClassCal > .TrainClassCal Then
                        If lTtemp2 > 0 Then
                            If TrainInf(nNum2).TrainClassCal > .TrainClassCal Then
                                If QianHou = 1 Then
                                    TxIntervalKind1(nTrnNum, nStatemp) = nJiange1
                                    TxDiffTime1(nTrnNum, nStatemp) = lTtemp1
                                    TxDiffTrain1(nTrnNum, nStatemp) = nNum1
                                Else
                                    TxIntervalKind2(nTrnNum, nStatemp) = nJiange1
                                    TxDiffTime2(nTrnNum, nStatemp) = lTtemp1
                                    TxDiffTrain2(nTrnNum, nStatemp) = nNum1
                                End If
                                FinInfTrn(nNum2)
                            Else
                                If QianHou = 1 Then
                                    TxIntervalKind1(nTrnNum, nStatemp) = nJiange2
                                    TxDiffTime1(nTrnNum, nStatemp) = lTtemp2
                                    TxDiffTrain1(nTrnNum, nStatemp) = nNum2
                                Else
                                    TxIntervalKind2(nTrnNum, nStatemp) = nJiange2
                                    TxDiffTime2(nTrnNum, nStatemp) = lTtemp2
                                    TxDiffTrain2(nTrnNum, nStatemp) = nNum2
                                End If
                                FinInfTrn(nNum1)
                            End If
                        Else
                            If QianHou = 1 Then
                                TxIntervalKind1(nTrnNum, nStatemp) = nJiange1
                                TxDiffTime1(nTrnNum, nStatemp) = lTtemp1
                                TxDiffTrain1(nTrnNum, nStatemp) = nNum1
                            Else
                                TxIntervalKind2(nTrnNum, nStatemp) = nJiange1
                                TxDiffTime2(nTrnNum, nStatemp) = lTtemp1
                                TxDiffTrain2(nTrnNum, nStatemp) = nNum1
                            End If
                        End If
                    Else
                        If QianHou = 1 Then
                            TxIntervalKind1(nTrnNum, nStatemp) = nJiange1
                            TxDiffTime1(nTrnNum, nStatemp) = lTtemp1
                            TxDiffTrain1(nTrnNum, nStatemp) = nNum1
                        Else
                            TxIntervalKind2(nTrnNum, nStatemp) = nJiange1
                            TxDiffTime2(nTrnNum, nStatemp) = lTtemp1
                            TxDiffTrain2(nTrnNum, nStatemp) = nNum1
                        End If
                    End If
                Else
                    If QianHou = 1 Then
                        TxIntervalKind1(nTrnNum, nStatemp) = nJiange1
                        TxDiffTime1(nTrnNum, nStatemp) = lTtemp1
                        TxDiffTrain1(nTrnNum, nStatemp) = nNum1
                    Else
                        TxIntervalKind2(nTrnNum, nStatemp) = nJiange1
                        TxDiffTime2(nTrnNum, nStatemp) = lTtemp1
                        TxDiffTrain2(nTrnNum, nStatemp) = nNum1
                    End If
                End If
            End If
            For i = 1 To UBound(StationInf)
                If StationInf(nStatemp).sStationName = StationInf(i).sStationName _
                    And i <> nStatemp Then
                    If QianHou = 1 Then
                        TxIntervalKind1(nTrnNum, i) = TxIntervalKind1(nTrnNum, nStatemp)
                        TxDiffTime1(nTrnNum, i) = TxDiffTime1(nTrnNum, nStatemp)
                        TxDiffTrain1(nTrnNum, i) = TxDiffTrain1(nTrnNum, nStatemp)
                    Else
                        TxIntervalKind2(nTrnNum, i) = TxIntervalKind2(nTrnNum, nStatemp)
                        TxDiffTime2(nTrnNum, i) = TxDiffTime2(nTrnNum, nStatemp)
                        TxDiffTrain2(nTrnNum, i) = TxDiffTrain2(nTrnNum, nStatemp)
                    End If
                End If
            Next i
        End With
    End Sub

    '获得在区间的延误时间
    Function lDelayTime(ByVal ntmpTrainNum As Integer, ByVal ntmpSecNum As Integer) As Integer
        lDelayTime = 0
        ''    Dim i As Integer, j As Integer
        ''    For i = 1 To UBound(DelayInf())
        ''        If TrainInf(ntmpTrainNum).Train = DelayInf(i).STrain Then
        ''            For j = 1 To UBound(DelayInf(i).sSecName)
        ''                If SectionInf(ntmpSecNum).sSectionName = DelayInf(i).sSecName(j) Then
        ''                    lDelayTime = DelayInf(i).sSecDelayTime(j)
        ''                    Exit Function
        ''                End If
        ''            Next j
        ''        End If
        ''    Next i
    End Function

    Function CheckStop(ByVal nTrnNum As Integer, ByVal lStime As Long, ByVal nUporDown As Integer, _
    ByVal nLeaveSection As Integer, ByVal ntmpStation As Integer, ByVal nEnterSection As Integer) As Integer
        'num本次列车编号，stime当前时间，qstatemp前一车站
        '检查到达条件
        Dim nStatemp As Integer
        nStatemp = ntmpStation
        TXDZ(nTrnNum, lStime, nUporDown, nLeaveSection, nStatemp, nEnterSection)
        CheckStop = nCheckJianGe(nTrnNum, nStatemp)
    End Function

    Function DanDiffTime(ByVal nNum1 As Integer, ByVal nNum2 As Integer, _
    ByVal ii As Integer, ByVal nQiTingTong As Integer, _
    ByVal nLeaveSection As Integer, ByVal ntmpStation As Integer, ByVal nEnterSection As Integer) As Long
        Dim nStationtempEnt As Integer, nStationtempLea As Integer, nQianStation As Integer, nHouStation As Integer
        Dim lYXtime As Long, lTaoZhan As Long
        Dim nUporDown As Integer
        Dim ntmpTrainNumberEnt As Integer, ntmpTrainNumberLea As Integer
        Dim nDirectionTempEnt As Integer, nDirectionTempLea As Integer
        Dim nTemp As Integer
        'nnum1前行车，nnum2本次车

        nUporDown = nDirection(nNum2)
        Select Case nQiTingTong
            Case 0, 2
                If StationInf(ntmpStation).sStationName = TrainInf(nNum2).NextStation And nLeaveSection = nEnterSection Then
                    If StationInf(ntmpStation).sStationName = StationInf(SectionInf(nEnterSection).nHStation).sStationName Then
                        nStationtempEnt = SectionInf(nEnterSection).nQStation
                        nQianStation = SectionInf(nEnterSection).nHStation
                        ntmpTrainNumberEnt = nNum2
                    ElseIf StationInf(ntmpStation).sStationName = StationInf(SectionInf(nEnterSection).nQStation).sStationName Then
                        nStationtempEnt = SectionInf(nEnterSection).nHStation
                        nQianStation = SectionInf(nEnterSection).nQStation
                        ntmpTrainNumberEnt = nNum2
                    End If
                ElseIf StationInf(ntmpStation).sStationName = TrainInf(nNum2).ComeStation And nLeaveSection = nEnterSection Then
                    If StationInf(ntmpStation).sStationName = StationInf(SectionInf(nEnterSection).nHStation).sStationName Then
                        nStationtempEnt = SectionInf(nEnterSection).nHStation
                        nQianStation = SectionInf(nEnterSection).nQStation
                        ntmpTrainNumberEnt = nNum2
                    ElseIf StationInf(ntmpStation).sStationName = StationInf(SectionInf(nEnterSection).nQStation).sStationName Then
                        nStationtempEnt = SectionInf(nEnterSection).nQStation
                        nQianStation = SectionInf(nEnterSection).nHStation
                        ntmpTrainNumberEnt = nNum2
                    End If
                Else
                    If StationInf(ntmpStation).sStationName = StationInf(SectionInf(nEnterSection).nHStation).sStationName Then
                        nStationtempEnt = SectionInf(nEnterSection).nHStation
                        nQianStation = SectionInf(nEnterSection).nQStation
                        ntmpTrainNumberEnt = nNum2
                    ElseIf StationInf(ntmpStation).sStationName = StationInf(SectionInf(nEnterSection).nQStation).sStationName Then
                        nStationtempEnt = SectionInf(nEnterSection).nQStation
                        nQianStation = SectionInf(nEnterSection).nHStation
                        ntmpTrainNumberEnt = nNum2
                    End If
                End If
                If nStationtempEnt > nQianStation Then
                    nDirectionTempEnt = 2
                Else
                    nDirectionTempEnt = 1
                End If

                If StationInf(ntmpStation).sStationName = TrainInf(nNum2).NextStation And nLeaveSection = nEnterSection Then
                    If StationInf(ntmpStation).sStationName = StationInf(SectionInf(nLeaveSection).nHStation).sStationName Then
                        nStationtempLea = SectionInf(nLeaveSection).nHStation
                        nHouStation = SectionInf(nLeaveSection).nQStation
                        ntmpTrainNumberLea = nNum2
                    ElseIf StationInf(ntmpStation).sStationName = StationInf(SectionInf(nLeaveSection).nQStation).sStationName Then
                        nStationtempLea = SectionInf(nLeaveSection).nQStation
                        nHouStation = SectionInf(nLeaveSection).nHStation
                        ntmpTrainNumberLea = nNum2
                    End If
                ElseIf StationInf(ntmpStation).sStationName = TrainInf(nNum2).ComeStation And nLeaveSection = nEnterSection Then
                    If StationInf(ntmpStation).sStationName = StationInf(SectionInf(nLeaveSection).nHStation).sStationName Then
                        nStationtempLea = SectionInf(nLeaveSection).nHStation
                        nHouStation = SectionInf(nLeaveSection).nQStation
                        ntmpTrainNumberLea = nNum2
                    ElseIf StationInf(ntmpStation).sStationName = StationInf(SectionInf(nLeaveSection).nQStation).sStationName Then
                        nStationtempLea = SectionInf(nLeaveSection).nQStation
                        nHouStation = SectionInf(nLeaveSection).nHStation
                        ntmpTrainNumberLea = nNum2
                    End If
                Else
                    If StationInf(ntmpStation).sStationName = StationInf(SectionInf(nLeaveSection).nHStation).sStationName Then
                        nStationtempLea = SectionInf(nLeaveSection).nHStation
                        nHouStation = SectionInf(nLeaveSection).nQStation
                        ntmpTrainNumberLea = nNum2
                    ElseIf StationInf(ntmpStation).sStationName = StationInf(SectionInf(nLeaveSection).nQStation).sStationName Then
                        nStationtempLea = SectionInf(nLeaveSection).nQStation
                        nHouStation = SectionInf(nLeaveSection).nHStation
                        ntmpTrainNumberLea = nNum2
                    End If
                End If
                If nStationtempLea > nHouStation Then
                    nDirectionTempLea = 2
                Else
                    nDirectionTempLea = 1
                End If
            Case 1

                If StationInf(ntmpStation).sStationName = TrainInf(nNum2).NextStation And nLeaveSection = nEnterSection Then
                    If StationInf(ntmpStation).sStationName = StationInf(SectionInf(nLeaveSection).nHStation).sStationName Then
                        nStationtempLea = SectionInf(nLeaveSection).nHStation
                        nHouStation = SectionInf(nLeaveSection).nQStation
                        ntmpTrainNumberLea = nNum2
                    ElseIf StationInf(ntmpStation).sStationName = StationInf(SectionInf(nLeaveSection).nQStation).sStationName Then
                        nStationtempLea = SectionInf(nLeaveSection).nQStation
                        nHouStation = SectionInf(nLeaveSection).nHStation
                        ntmpTrainNumberLea = nNum2
                    End If
                ElseIf StationInf(ntmpStation).sStationName = TrainInf(nNum2).ComeStation And nLeaveSection = nEnterSection Then
                    If StationInf(ntmpStation).sStationName = StationInf(SectionInf(nLeaveSection).nHStation).sStationName Then
                        nStationtempLea = SectionInf(nLeaveSection).nQStation
                        nHouStation = SectionInf(nLeaveSection).nHStation
                        ntmpTrainNumberLea = nNum2
                    ElseIf StationInf(ntmpStation).sStationName = StationInf(SectionInf(nLeaveSection).nQStation).sStationName Then
                        nStationtempLea = SectionInf(nLeaveSection).nQStation
                        nHouStation = SectionInf(nLeaveSection).nHStation
                        ntmpTrainNumberLea = nNum2
                    End If
                Else
                    If StationInf(ntmpStation).sStationName = StationInf(SectionInf(nLeaveSection).nHStation).sStationName Then
                        nStationtempLea = SectionInf(nLeaveSection).nHStation
                        nHouStation = SectionInf(nLeaveSection).nQStation
                        ntmpTrainNumberLea = nNum2
                    ElseIf StationInf(ntmpStation).sStationName = StationInf(SectionInf(nLeaveSection).nQStation).sStationName Then
                        nStationtempLea = SectionInf(nLeaveSection).nQStation
                        nHouStation = SectionInf(nLeaveSection).nHStation
                        ntmpTrainNumberLea = nNum2
                    End If
                End If
                If nStationtempLea > nHouStation Then
                    nDirectionTempLea = 2
                Else
                    nDirectionTempLea = 1
                End If
                If StationInf(ntmpStation).sStationName = TrainInf(nNum2).NextStation And nLeaveSection = nEnterSection Then
                    If StationInf(ntmpStation).sStationName = StationInf(SectionInf(nEnterSection).nHStation).sStationName Then
                        nStationtempEnt = SectionInf(nEnterSection).nQStation
                        nQianStation = SectionInf(nEnterSection).nHStation
                        ntmpTrainNumberEnt = nNum2
                    ElseIf StationInf(ntmpStation).sStationName = StationInf(SectionInf(nEnterSection).nQStation).sStationName Then
                        nStationtempEnt = SectionInf(nEnterSection).nHStation
                        nQianStation = SectionInf(nEnterSection).nQStation
                        ntmpTrainNumberEnt = nNum2
                    End If
                ElseIf StationInf(ntmpStation).sStationName = TrainInf(nNum2).ComeStation And nLeaveSection = nEnterSection Then
                    If StationInf(ntmpStation).sStationName = StationInf(SectionInf(nEnterSection).nHStation).sStationName Then
                        nStationtempEnt = SectionInf(nEnterSection).nHStation
                        nQianStation = SectionInf(nEnterSection).nQStation
                        ntmpTrainNumberEnt = nNum2
                    ElseIf StationInf(ntmpStation).sStationName = StationInf(SectionInf(nEnterSection).nQStation).sStationName Then
                        nStationtempEnt = SectionInf(nEnterSection).nQStation
                        nQianStation = SectionInf(nEnterSection).nHStation
                        ntmpTrainNumberEnt = nNum2
                    End If
                Else
                    If StationInf(ntmpStation).sStationName = StationInf(SectionInf(nEnterSection).nHStation).sStationName Then
                        nStationtempEnt = SectionInf(nEnterSection).nHStation
                        nQianStation = SectionInf(nEnterSection).nQStation
                        ntmpTrainNumberEnt = nNum2
                    ElseIf StationInf(ntmpStation).sStationName = StationInf(SectionInf(nEnterSection).nQStation).sStationName Then
                        nStationtempEnt = SectionInf(nEnterSection).nQStation
                        nQianStation = SectionInf(nEnterSection).nHStation
                        ntmpTrainNumberEnt = nNum2
                    End If
                    If nStationtempEnt > nQianStation Then
                        nDirectionTempEnt = 2
                    Else
                        nDirectionTempEnt = 1
                    End If
                End If
        End Select
        '//////////////
        'nUporDown = nDirection(nNum2)
        With TrainInf(nNum1)
            Select Case ii
                Case 101
                    '前行列车到达本站与本次列车出发

                    DanDiffTime = StationInf(nStationtempLea).lTaoDaoFa(nDirectionTempLea)

                Case 102
                    '前行列车从本站出发与本次列车出发
                    If .Arrival(nQianStation) <> -1 And .Starting(nStationtempEnt) <> -1 Then
                        DanDiffTime = TimeMinus(.Arrival(nQianStation), .Starting(nStationtempEnt)) _
                        + StationInf(nStationtempEnt).lTaoLian2(nDirectionTempEnt)
                    Else
                        DanDiffTime = StationInf(nStationtempEnt).lTaoFaFa(nDirectionTempEnt) 'tao不同发发、发往不同方向
                    End If

                Case 103
                    '前行列车通过本站与本次列车出发

                    If .Arrival(nQianStation) <> -1 And .Starting(nStationtempEnt) <> -1 Then
                        DanDiffTime = TimeMinus(.Arrival(nQianStation), .Starting(nStationtempEnt)) _
                        + StationInf(nStationtempEnt).lTaoLian2(nDirectionTempEnt)
                    Else
                        DanDiffTime = StationInf(nStationtempEnt).lTaoFaFa(nDirectionTempEnt)  '(2 * 60) 'tao不同发发、发往不同方向
                    End If

                Case 104
                    '本次列车出发与后行列车到达本站

                    If TrainInf(nNum2).StopLine(nStationtempEnt) = .StopLine(nStationtempEnt) Then
                        DanDiffTime = StationInf(nStationtempEnt).lTaoFaDao(nDirectionTempEnt)
                    Else
                        DanDiffTime = StationInf(nStationtempEnt).lTaoFaDao(nDirectionTempEnt)
                    End If

                Case 105
                    '本次列车出发与后行列车本站出发

                    DanDiffTime = StationInf(nStationtempEnt).lTaoFaFa(nDirectionTempEnt) '(2 * 60)

                Case 106
                    '本次列车出发与后行列车通过本站

                    DanDiffTime = StationInf(nStationtempEnt).lTaoFaFa(nDirectionTempEnt) '(2 * 60)

                Case 107
                    '前行列车到达本站与本次列车到达本站

                    'If .Starting(nHouStation) <> -1 Then
                    '    If TrainInf(nNum2).Arrival(nHouStation) = TrainInf(nNum2).Starting(nHouStation) Then
                    '        DanDiffTime = TimeMinus(.Arrival(nStationtempLea), .Starting(nHouStation)) _
                    '        + SectionInf(nLeaveSection).lTaoLian1(nDirectionTempLea)
                    '    Else
                    '        DanDiffTime = TimeMinus(.Arrival(nStationtempLea), .Starting(nHouStation)) _
                    '        + SectionInf(nLeaveSection).lTaoLian2(nDirectionTempLea)
                    '    End If
                    'Else
                    '    DanDiffTime = SectionInf(nLeaveSection).lTaoDaoDao(nDirectionTempLea)
                    'End If
                    If .Starting(nHouStation) <> -1 And TrainInf(nNum2).Starting(nHouStation) <> -1 Then
                        If TimeMinus(TrainInf(nNum2).Starting(nHouStation), .Starting(nHouStation)) < _
                            TimeMinus(.Starting(nHouStation), TrainInf(nNum2).Starting(nHouStation)) Then
                            DanDiffTime = 0
                        Else
                            DanDiffTime = TimeMinus(.Starting(nHouStation), TrainInf(nNum2).Starting(nHouStation)) + 60
                        End If
                    Else
                        DanDiffTime = 0
                    End If
                Case 108
                    '前行列车本站出发与本次列车到达本站
                    If .StopLine(nStationtempLea) = TrainInf(nNum2).StopLine(nStationtempLea) Then
                        DanDiffTime = StationInf(nStationtempLea).lTaoFaDao(nDirectionTempLea)
                    Else
                        DanDiffTime = StationInf(nStationtempLea).lTaoFaDao(nDirectionTempLea)
                    End If

                Case 109
                    '前行列车通过本站与本次列车到达本站

                    If .Starting(nHouStation) <> -1 Then
                        If TrainInf(nNum2).Arrival(nHouStation) = TrainInf(nNum2).Starting(nHouStation) Then
                            DanDiffTime = TimeMinus(.Arrival(nStationtempLea), .Starting(nHouStation)) _
                            + StationInf(nStationtempLea).lTaoLian1(nDirectionTempLea)
                        Else
                            DanDiffTime = TimeMinus(.Arrival(nStationtempLea), .Starting(nHouStation)) _
                            + StationInf(nStationtempLea).lTaoLian2(nDirectionTempLea)
                        End If
                    Else
                        DanDiffTime = StationInf(nStationtempLea).lTaoDaoDao(nDirectionTempLea)
                    End If

                Case 110
                    '本次列车到达与后行列车到达本站

                    If .Starting(nHouStation) <> -1 Then
                        If .Arrival(nHouStation) = .Starting(nHouStation) Then
                            DanDiffTime = TimeMinus(.Arrival(nStationtempLea), .Starting(nHouStation)) _
                            + StationInf(nStationtempLea).lTaoLian1(nDirectionTempLea)
                        Else
                            DanDiffTime = TimeMinus(.Arrival(nStationtempLea), .Starting(nHouStation)) _
                            + StationInf(nStationtempLea).lTaoLian2(nDirectionTempLea)
                        End If
                    Else
                        DanDiffTime = StationInf(nStationtempLea).lTaoDaoDao(nDirectionTempLea)
                    End If

                Case 111
                    '本次列车到达与后行列车本站出发

                    DanDiffTime = StationInf(nStationtempLea).lTaoDaoFa(nDirectionTempLea)

                Case 112
                    '本次列车到达与后行列车通过本站

                    If .Starting(nHouStation) <> -1 Then
                        If .Arrival(nHouStation) = .Starting(nHouStation) Then
                            DanDiffTime = TimeMinus(.Arrival(nStationtempLea), .Starting(nHouStation)) _
                            + StationInf(nStationtempLea).lTaoLian1(nDirectionTempLea)
                        Else
                            DanDiffTime = TimeMinus(.Arrival(nStationtempLea), .Starting(nHouStation)) _
                            + StationInf(nStationtempLea).lTaoLian2(nDirectionTempLea)
                        End If
                    Else
                        DanDiffTime = StationInf(nStationtempLea).lTaoDaoDao(nDirectionTempLea)
                    End If

                Case 113
                    '前行列车到达本站与本次列车通过本站

                    DanDiffTime = StationInf(nStationtempLea).lTaoDaoFa(nDirectionTempLea)

                Case 114
                    '前行列车本站出发与本次列车通过本站
                    If SectionInf(nEnterSection).sBlock = "半自动闭塞" Then
                        nTemp = nFindQstaNum(nNum1, nStationtempEnt, nDirection(nNum1))
                        If nTemp = nQianStation Then
                            If .Arrival(nQianStation) <> -1 And .Starting(nStationtempEnt) <> -1 Then
                                DanDiffTime = TimeMinus(.Arrival(nQianStation), .Starting(nStationtempEnt)) _
                                + StationInf(nStationtempEnt).lTaoLian1(nDirectionTempEnt)
                            Else
                                DanDiffTime = StationInf(nStationtempEnt).lTaoFaFa(nDirectionTempEnt)
                            End If
                        Else
                            DanDiffTime = 0
                        End If
                    Else
                        DanDiffTime = lAutoBlockTime(nNum1, nNum2, nEnterSection, 3)
                    End If
                Case 115
                    '前行列车通过本站与本次列车通过本站

                    If SectionInf(nEnterSection).sBlock = "半自动闭塞" Then
                        nTemp = nFindQstaNum(nNum1, nStationtempEnt, nDirection(nNum1))
                        If nTemp = nQianStation Then
                            If .Arrival(nQianStation) <> -1 And .Starting(nStationtempEnt) <> -1 Then

                                DanDiffTime = TimeMinus(.Arrival(nQianStation), .Starting(nStationtempEnt)) _
                                + StationInf(nStationtempEnt).lTaoLian1(nDirectionTempEnt)
                            Else
                                DanDiffTime = StationInf(nStationtempEnt).lTaoFaFa(nDirectionTempEnt)
                            End If
                        Else
                            DanDiffTime = 0
                        End If
                    Else
                        DanDiffTime = lAutoBlockTime(nNum1, nNum2, nEnterSection, 8)
                    End If
                Case 116
                    '本次列车通过本站与后行列车到达本站

                    If .Starting(nHouStation) <> -1 Then
                        If .Arrival(nHouStation) = .Starting(nHouStation) Then
                            DanDiffTime = TimeMinus(.Arrival(nStationtempLea), .Starting(nHouStation)) _
                            + StationInf(nStationtempLea).lTaoLian1(nDirectionTempLea)
                        Else
                            DanDiffTime = TimeMinus(.Arrival(nStationtempLea), .Starting(nHouStation)) _
                            + StationInf(nStationtempLea).lTaoLian2(nDirectionTempLea)
                        End If
                    Else
                        DanDiffTime = StationInf(nStationtempLea).lTaoDaoDao(nDirectionTempLea)
                    End If

                Case 117
                    '本次列车通过本站与后行列车本站出发

                    DanDiffTime = StationInf(nStationtempLea).lTaoFaFa(nDirectionTempLea)

                Case 118
                    '本次列车通过本站与后行列车通过本站

                    If .Starting(nHouStation) <> -1 Then
                        If .Arrival(nHouStation) = .Starting(nHouStation) Then
                            DanDiffTime = TimeMinus(.Arrival(nStationtempLea), .Starting(nHouStation)) _
                            + StationInf(nStationtempLea).lTaoLian1(nDirectionTempLea)
                        Else
                            DanDiffTime = TimeMinus(.Arrival(nStationtempLea), .Starting(nHouStation)) _
                            + StationInf(nStationtempLea).lTaoLian2(nDirectionTempLea)
                        End If
                    Else
                        DanDiffTime = StationInf(nStationtempLea).lTaoDaoDao(nDirectionTempLea)
                    End If
                Case 201

                    If SectionInf(nLeaveSection).nSection = 1 _
                        And SectionInf(nEnterSection).nSection = 2 Then
                        '单进双时的tao通
                        If .TrainKind = "客车" And TrainInf(nNum2).TrainKind = "客车" Then
                            DanDiffTime = StationInf(nStationtempLea).lTaoTongK(nDirectionTempLea)
                            If DanDiffTime = 0 Then
                                DanDiffTime = 4 * 60
                            End If
                        Else
                            DanDiffTime = StationInf(nStationtempLea).lTaoTongH(nDirectionTempLea)
                            If DanDiffTime = 0 Then
                                DanDiffTime = 5 * 60
                            End If
                        End If
                    ElseIf SectionInf(nLeaveSection).nSection = 2 _
                        And SectionInf(nEnterSection).nSection = 1 Then
                        '双进单时的tao通
                        If .TrainKind = "客车" And TrainInf(nNum2).TrainKind = "客车" Then
                            DanDiffTime = StationInf(nStationtempEnt).lTaoTongK(nDirectionTempEnt)
                            If DanDiffTime = 0 Then
                                DanDiffTime = 4 * 60
                            End If
                        Else
                            DanDiffTime = StationInf(nStationtempEnt).lTaoTongH(nDirectionTempEnt)
                            If DanDiffTime = 0 Then
                                DanDiffTime = 5 * 60
                            End If
                        End If
                    End If
                Case 202
                    'tao不
                    If SectionInf(nEnterSection).nSection = 2 Then
                        DanDiffTime = StationInf(nStationtempLea).lTaoBu(nDirectionTempLea)
                    Else
                        DanDiffTime = StationInf(nStationtempEnt).lTaoBu(nDirectionTempEnt)
                    End If
                    If DanDiffTime = 0 Then
                        DanDiffTime = 4 * 60
                    End If
                Case 203
                    If .Starting(nQianStation) <> -1 Then
                        lYXtime = TimeMinus(.Arrival(nStationtempEnt), .Starting(nQianStation))
                        lYXtime = lYXtime + TimeRun(ntmpTrainNumberEnt, nStationtempEnt, nQianStation)
                        If nQiTingTong = 2 Then
                            lYXtime = lYXtime + TimeQ(ntmpTrainNumberEnt, nStationtempEnt, nQianStation)
                        End If
                        If .Arrival(nQianStation) = .Starting(nQianStation) Then
                            lTaoZhan = StationInf(nStationtempEnt).lTaoBu(nDirectionTempEnt)
                            If lTaoZhan = 0 Then
                                lTaoZhan = 4 * 60
                            End If
                        Else
                            lTaoZhan = StationInf(nStationtempEnt).lTaoHui(nDirectionTempEnt)
                            If lTaoZhan = 0 Then
                                lTaoZhan = 2 * 60
                            End If
                        End If
                        DanDiffTime = lYXtime + lTaoZhan
                    End If
                Case 204
                    If .Starting(nQianStation) <> -1 Then
                        lYXtime = TimeMinus(.Arrival(nStationtempEnt), .Starting(nQianStation))
                        lYXtime = lYXtime + TimeRun(ntmpTrainNumberEnt, nStationtempEnt, nQianStation)
                        If nQiTingTong = 2 Then
                            lYXtime = lYXtime + TimeQ(ntmpTrainNumberEnt, nStationtempEnt, nQianStation)
                        End If
                        If .Arrival(nQianStation) = .Starting(nQianStation) Then
                            lTaoZhan = StationInf(nStationtempEnt).lTaoBu(nDirectionTempEnt)
                            If lTaoZhan = 0 Then
                                lTaoZhan = 4 * 60
                            End If
                        Else
                            lTaoZhan = StationInf(nStationtempEnt).lTaoHui(nDirectionTempEnt)
                            If lTaoZhan = 0 Then
                                lTaoZhan = 2 * 60
                            End If
                        End If
                        DanDiffTime = lYXtime + lTaoZhan
                    End If
                Case 205
                    If .Starting(nQianStation) <> -1 Then
                        lYXtime = TimeMinus(.Arrival(nStationtempEnt), .Starting(nQianStation))
                        lYXtime = lYXtime + TimeRun(ntmpTrainNumberEnt, nStationtempEnt, nQianStation)
                        If nQiTingTong = 2 Then
                            lYXtime = lYXtime + TimeQ(ntmpTrainNumberEnt, nStationtempEnt, nQianStation)
                        End If
                    Else
                        '不同方向的到达
                        lYXtime = 0
                    End If
                    If .Arrival(nQianStation) <> .Starting(nQianStation) Then
                        lTaoZhan = StationInf(nStationtempEnt).lTaoHui(nDirectionTempEnt)
                        If lTaoZhan = 0 Then
                            lTaoZhan = 2 * 60
                        End If
                    Else
                        lTaoZhan = StationInf(nStationtempEnt).lTaoBu(nDirectionTempEnt)
                        If lTaoZhan = 0 Then
                            lTaoZhan = 4 * 60
                        End If
                    End If
                    DanDiffTime = lYXtime + lTaoZhan
                Case 206
                    If .Starting(nQianStation) <> -1 Then
                        lYXtime = TimeMinus(.Arrival(nStationtempEnt), .Starting(nQianStation))
                        lYXtime = lYXtime + TimeRun(ntmpTrainNumberEnt, nStationtempEnt, nQianStation)
                        If nQiTingTong = 2 Then
                            lYXtime = lYXtime + TimeQ(ntmpTrainNumberEnt, nStationtempEnt, nQianStation)
                        End If
                    Else
                        lYXtime = 0
                    End If
                    If .Arrival(nQianStation) <> .Starting(nQianStation) Then
                        lTaoZhan = StationInf(nStationtempEnt).lTaoHui(nDirectionTempEnt)
                        If lTaoZhan = 0 Then
                            lTaoZhan = 2 * 60
                        End If
                    Else
                        lTaoZhan = StationInf(nStationtempEnt).lTaoBu(nDirectionTempEnt)
                        If lTaoZhan = 0 Then
                            lTaoZhan = 4 * 60
                        End If
                    End If
                    DanDiffTime = lYXtime + lTaoZhan
                Case 207
                    If nQiTingTong = 0 Then
                        DanDiffTime = StationInf(nStationtempLea).lTaoDaoDao(nDirectionTempLea)
                    Else
                        DanDiffTime = StationInf(nStationtempEnt).lTaoDaoDao(nDirectionTempEnt)
                    End If
                Case 208
                    DanDiffTime = StationInf(nStationtempEnt).lTaoHui(nDirectionTempEnt)
                    If DanDiffTime = 0 Then
                        DanDiffTime = 2 * 60
                    End If
                Case 209
                    DanDiffTime = StationInf(nStationtempLea).lTaoBu(nDirectionTempLea)
                    If DanDiffTime = 0 Then
                        DanDiffTime = 4 * 60
                    End If
                Case 210
                    DanDiffTime = StationInf(nStationtempEnt).lTaoFaFa(nDirectionTempEnt)
                Case 211
                    If .Arrival(nHouStation) <> -1 Then
                        lYXtime = TimeMinus(.Arrival(nHouStation), .Starting(nStationtempLea))
                        lYXtime = lYXtime + TimeRun(ntmpTrainNumberLea, nHouStation, nStationtempLea)
                        If nQiTingTong = 1 Then
                            lYXtime = lYXtime + TimeT(ntmpTrainNumberLea, nHouStation, nStationtempLea)
                        End If
                        If TrainInf(nNum2).Arrival(nHouStation) <> TrainInf(nNum2).Starting(nHouStation) Then
                            lYXtime = lYXtime + TimeQ(ntmpTrainNumberLea, nHouStation, nStationtempLea)
                        End If
                    End If

                    If StationInf(nStationtempLea).lTaoHui(nDirectionTempLea) = 0 Then
                        lTaoZhan = 2 * 60
                    Else
                        lTaoZhan = StationInf(nStationtempLea).lTaoHui(nDirectionTempLea)
                    End If
                    DanDiffTime = lYXtime + lTaoZhan
                Case 212
                    If .Arrival(nHouStation) <> -1 Then
                        lYXtime = TimeMinus(.Arrival(nHouStation), .Starting(nStationtempLea))
                        lYXtime = lYXtime + TimeRun(ntmpTrainNumberLea, nHouStation, nStationtempLea)
                        If nQiTingTong = 1 Then
                            lYXtime = lYXtime + TimeT(ntmpTrainNumberLea, nHouStation, nStationtempLea)
                        End If
                        If TrainInf(nNum2).Arrival(nHouStation) <> TrainInf(nNum2).Starting(nHouStation) Then
                            lYXtime = lYXtime + TimeQ(ntmpTrainNumberLea, nHouStation, nStationtempLea)
                        End If
                    End If

                    If StationInf(nStationtempLea).lTaoHui(nDirectionTempLea) = 0 Then
                        lTaoZhan = 2 * 60
                    Else
                        lTaoZhan = StationInf(nStationtempLea).lTaoHui(nDirectionTempLea)
                    End If
                    DanDiffTime = lYXtime + lTaoZhan
                Case 213
                    If .Arrival(nHouStation) <> -1 Then
                        lYXtime = TimeMinus(.Arrival(nHouStation), .Starting(nStationtempLea))
                        lYXtime = lYXtime + TimeRun(ntmpTrainNumberLea, nHouStation, nStationtempLea)
                        If nQiTingTong = 1 Then
                            lYXtime = lYXtime + TimeT(ntmpTrainNumberLea, nHouStation, nStationtempLea)
                        End If
                        If TrainInf(nNum2).Arrival(nHouStation) <> TrainInf(nNum2).Starting(nHouStation) Then
                            'lYXtime = lYXtime + TimeQ(ntmpTrainNumberLea, nHouStation, nStationtempLea)
                        End If
                    Else
                        lYXtime = 0
                    End If
                    lTaoZhan = StationInf(nStationtempLea).lTaoBu(nDirectionTempLea)
                    If lTaoZhan = 0 Then
                        lTaoZhan = 4 * 60
                    End If
                    DanDiffTime = lYXtime + lTaoZhan
                Case 214
                    If .Arrival(nHouStation) <> -1 Then
                        lYXtime = TimeMinus(.Arrival(nHouStation), .Starting(nStationtempLea))
                        lYXtime = lYXtime + TimeRun(ntmpTrainNumberLea, nHouStation, nStationtempLea)
                        If nQiTingTong = 1 Then
                            lYXtime = lYXtime + TimeT(ntmpTrainNumberLea, nHouStation, nStationtempLea)
                        End If
                        If TrainInf(nNum2).Arrival(nHouStation) <> TrainInf(nNum2).Starting(nHouStation) Then
                            'lYXtime = lYXtime + TimeQ(ntmpTrainNumberLea, nHouStation, nStationtempLea)
                        End If
                    Else
                        lYXtime = 0
                    End If
                    lTaoZhan = StationInf(nStationtempLea).lTaoBu(nDirectionTempLea)
                    If lTaoZhan = 0 Then
                        lTaoZhan = 4 * 60
                    End If
                    DanDiffTime = lYXtime + lTaoZhan
                Case 215
                    DanDiffTime = StationInf(nStationtempLea).lTaoBu(nDirectionTempLea)
                    If DanDiffTime = 0 Then
                        DanDiffTime = 4 * 60
                    End If
                Case 216
                    DanDiffTime = StationInf(nStationtempLea).lTaoBu(nDirectionTempLea)
                    If DanDiffTime = 0 Then
                        DanDiffTime = 4 * 60
                    End If
                Case 230
                    'If SectionInf(nLeaveSection).nSection = 1 Then
                    '    DanDiffTime = SectionInf(nLeaveSection).lTaoBu(nDirectionTempLea)
                    'Else
                    DanDiffTime = 0
                    'End If
                Case 231
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 9)
                Case 232
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 10)
                Case 233
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 15)
                Case 240
                    DanDiffTime = 0
                Case 241
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 9)
                Case 242
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 11)
                Case 243
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 13)
                Case 250
                    DanDiffTime = 0
                Case 251
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 11)
                Case 252
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 12)
                Case 253
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 16)
                Case 260
                    DanDiffTime = 0
                Case 261
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 10)
                Case 262
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 12)
                Case 263
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 14)
                Case 270
                    DanDiffTime = 0
                Case 271
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 13)
                Case 272
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 14)
                Case 273
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 17)
                Case 280
                    DanDiffTime = 0
                Case 281
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 15)
                Case 282
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 16)
                Case 283
                    DanDiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStation, 17)
                Case 500
                    DanDiffTime = TimeRun(nNum2, nHouStation, nStationtempEnt) - TimeQ(nNum2, nHouStation, nStationtempEnt) - TimeRun(nNum1, nHouStation, nStationtempEnt) - TimeMinus(TrainInf(nNum1).Starting(nHouStation), TrainInf(nNum2).Starting(nHouStation))
                Case 600
                    DanDiffTime = 0
                Case 700
                    DanDiffTime = TimeMinus(TrainInf(nNum1).Arrival(nQianStation), TrainInf(nNum1).Starting(nStationtempEnt)) - TimeRun(nNum2, nStationtempEnt, nQianStation) - lDelayTime(nNum2, nEnterSection) + nMoveStepTime
                Case 800
                    DanDiffTime = TimeRun(nNum2, nStationtempEnt, nHouStation) - TimeQ(nNum2, nStationtempEnt, nHouStation) - TimeRun(nNum1, nStationtempEnt, nHouStation) - TimeMinus(TrainInf(nNum1).Starting(nStationtempEnt), TrainInf(nNum2).Starting(nStationtempEnt)) + nMoveStepTime
                Case 810 '区间交叉'前车快后车慢
                    DanDiffTime = nMoveStepTimeInTdrawline 'TimeRun(nNum2, nStationtempEnt, nQianStation) - TimeQ(nNum2, nStationtempEnt, nQianStation) -TimeActualRun(nNum1, nStationtempEnt, nQianStation) +
                Case 820 '区间交叉'前车慢后车快
                    DanDiffTime = -TimeRun(nNum2, nStationtempEnt, nQianStation) + TimeQ(nNum1, nStationtempEnt, nQianStation) + TimeRun(nNum1, nStationtempEnt, nQianStation) + nMoveStepTime
            End Select
        End With
    End Function

    Function DiffTime(ByVal nNum1 As Integer, ByVal nNum2 As Integer, ByVal ii As Integer, _
    ByVal nLeaveSection As Integer, ByVal ntmpStationNum As Integer, ByVal nEnterSection As Integer) As Long
        'ii=0 通发
        'ii=1 发发
        'ii=2 到发
        'ii=3 发通
        'ii=4 发到
        'ii=5 通到
        'ii=6 到到
        'ii=7 到通
        'ii=8 通通

        'ii=9 反到同到
        'ii=10 反发同到
        'ii=11 同到反发
        'ii=12 反发同发（不同方向）
        'ii=13 反到同通
        'ii=14 反发同通
        'ii=15 同通反到
        'ii=16 同通反发
        'ii=17 同到反通
        'ii=18 反通同到
        'ii=19 反通同发

        Select Case ii
            Case 0 'ii=0 通发
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)
            Case 1 'ii=1 发发
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)
            Case 2 'ii=2 到发
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)
            Case 3 'ii=3 发通
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)
            Case 4 'ii=4 发到
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)
            Case 5 'ii=5 通到
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)
            Case 6 'ii=6 到到
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)
            Case 7 'ii=7 到通
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)
            Case 8 'ii=8 通通
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)

            Case 9 'ii=9 反到同到
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)
            Case 10 'ii=10 反发同到
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)
            Case 11 'ii=11 同到反发
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)
            Case 12 'ii=12 反发同发（不同方向）
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)
            Case 13 'ii=13 反到同通
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)
            Case 14 'ii=14 反发同通
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)
            Case 15 'ii=15 同通反到
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)
            Case 16 'ii=16 同通反发
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)
            Case 17 'ii=17 反通
                DiffTime = lAutoBlockTime(nNum1, nNum2, ntmpStationNum, ii)

            Case 18 'ii=18 反通同到
            Case 19 'ii=19 反通同发
        End Select
    End Function

    Sub DoubleSingleStartCheck(ByVal nTrainNumber As Integer, ByVal lTimeArrival As Long, _
    ByVal nStationNumber As Integer, ByVal nSdirection As Integer, _
    ByVal nSectionLeave As Integer, ByVal nSectionEnter As Integer)

        Dim BeforeTrainD As Integer, BeforeTrainF As Integer
        Dim AfterTrainD As Integer, AfterTrainF As Integer
        Dim lArrivalArrival As Long, lArrivalStart As Long
        Dim ii As Integer, jj As Integer
        Dim BeforeTrainD1 As Integer, BeforeTrainF1 As Integer
        Dim AfterTrainD1 As Integer, AfterTrainF1 As Integer
        Dim lArrivalArrival1 As Long, lArrivalStart1 As Long
        Dim ii1 As Integer, jj1 As Integer
        Dim nTemp As Integer

        If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nSameDirectionStart(BeforeTrainD1, nTrainNumber, lTimeArrival, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nSameDirectionStart(BeforeTrainF1, nTrainNumber, lTimeArrival, nStationNumber, 0, 1, nSectionEnter)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionStart(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionStart(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nSameDirectionStart(AfterTrainD1, nTrainNumber, lTimeArrival, nStationNumber, 1, 0, nSectionLeave)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nSameDirectionStart(AfterTrainF1, nTrainNumber, lTimeArrival, nStationNumber, 1, 1, nSectionEnter)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionStart(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionStart(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nConflictStart(BeforeTrainD1, nStationNumber, 0, 0, nSectionEnter)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nConflictStart(BeforeTrainF1, nStationNumber, 0, 1, nSectionEnter)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionStart(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionStart(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDanConflictH(lArrivalArrival1, AfterTrainD1, nTrainNumber, 2, _
                        nSectionLeave, nStationNumber, nSectionEnter)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nConflictStart(AfterTrainF1, nStationNumber, 1, 1, nSectionEnter)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionStart(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionStart(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        Else
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nSameDirectionStart(BeforeTrainD, nTrainNumber, lTimeArrival, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nSameDirectionStart(BeforeTrainF, nTrainNumber, lTimeArrival, nStationNumber, 0, 1, nSectionEnter)
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '2到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nSameDirectionStart(AfterTrainD, nTrainNumber, lTimeArrival, nStationNumber, 1, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nSameDirectionStart(AfterTrainF, nTrainNumber, lTimeArrival, nStationNumber, 1, 1, nSectionEnter)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
            '检查对向
            '以下应进行修改！！！！
            '只需按单线进行间隔时间检查

            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nConflictStart(BeforeTrainD, nStationNumber, 0, 0, nSectionEnter)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nConflictStart(BeforeTrainF, nStationNumber, 0, 1, nSectionEnter)
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nDanConflictH(lArrivalArrival, AfterTrainD, nTrainNumber, 2, _
                        nSectionLeave, nStationNumber, nSectionEnter)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nConflictStart(AfterTrainF, nStationNumber, 1, 1, nSectionEnter)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 2, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        End If
    End Sub

    Function lAutoBlockTime(ByVal nTrainNum1 As Integer, ByVal nTrainNum2 As Integer, _
    ByVal nStaNumtemp As Integer, ByVal nInterKind As Integer) As Long


        With TrainInf(nTrainNum1)
            Select Case .TrainKind
                Case "客车"
                    Select Case TrainInf(nTrainNum2).TrainKind
                        Case "客车"
                            lAutoBlockTime = StationInf(nStaNumtemp).IKK(nInterKind)
                        Case "货车"
                            lAutoBlockTime = StationInf(nStaNumtemp).IKH(nInterKind)
                    End Select
                Case "货车"
                    Select Case TrainInf(nTrainNum2).TrainKind
                        Case "客车"
                            lAutoBlockTime = StationInf(nStaNumtemp).IHK(nInterKind)
                        Case "货车"
                            lAutoBlockTime = StationInf(nStaNumtemp).IHH(nInterKind)
                    End Select
            End Select
            If nInterKind = 2 Then
                If .StopLine(nStaNumtemp) <> TrainInf(nTrainNum2).StopLine(nStaNumtemp) Then
                    lAutoBlockTime = 0
                End If
            ElseIf nInterKind = 4 Then
                If .StopLine(nStaNumtemp) <> TrainInf(nTrainNum2).StopLine(nStaNumtemp) Then
                    lAutoBlockTime = 0
                End If
            End If
        End With
    End Function


    '获得在车站的延误时间
    Public Function lDelayTimeSta(ByVal ntmpTrainNum As Integer, ByVal nStaNum As Integer) As Long
        lDelayTimeSta = 0
        ''    Dim i As Integer, j As Integer
        ''    For i = 1 To UBound(DelayInf)
        ''        If TrainInf(ntmpTrainNum).Train = DelayInf(i).STrain Then
        ''            For j = 1 To UBound(DelayInf(i).sStaName)
        ''                If StationInf(nStaNum).sStationName = DelayInf(i).sStaName(j) Then
        ''                    lDelayTimeSta = DelayInf(i).sStaDelayTime(j)
        ''                    Exit Function
        ''                End If
        ''            Next j
        ''        End If
        ''    Next i
    End Function

    Function nConflictPass(ByVal nTrainNumber As Integer, ByVal nStationNumber As Integer, _
    ByVal nQianorHou As Integer, ByVal nArrivalStart As Integer, ByVal nSectionNumber As Integer) As Integer
        If TrainInf(nTrainNumber).Arrival(nStationNumber) = TrainInf(nTrainNumber).Starting(nStationNumber) Then
            If nQianorHou = 0 Then '与前比
                nConflictPass = 201 '通通taotong
            ElseIf nQianorHou = 1 Then '与后比
                nConflictPass = 201 '通通taotong
            End If
        Else
            If nQianorHou = 0 Then '与前比
                If nArrivalStart = 0 Then '到达
                    nConflictPass = 202 '到通taobu
                ElseIf nArrivalStart = 1 Then '出发
                    nConflictPass = 201 '发通taotong
                End If
            ElseIf nQianorHou = 1 Then '与后比
                If nArrivalStart = 0 Then '到达
                    nConflictPass = 201 '通到taotong
                ElseIf nArrivalStart = 1 Then '出发
                    nConflictPass = 208 '通发taohui
                End If
            End If
        End If
    End Function

    Function nDiffDirectionPass(ByVal nTrainNumber1 As Integer, ByVal nTrainNumber2 As Integer, _
    ByVal nStationNumber As Integer, ByVal nQianorHou As Integer, ByVal nArrivalStart2 As Integer) As Integer

        Dim nTemp As Integer
        Dim nArrivalStart1 As Integer

        nArrivalStart1 = 2
        If nArrivalStart2 = 0 Then
            If TrainInf(nTrainNumber2).Starting(nStationNumber) = TrainInf(nTrainNumber2).Arrival(nStationNumber) Then
                nArrivalStart2 = 2
            End If
        ElseIf nArrivalStart2 = 1 Then
            If TrainInf(nTrainNumber2).Starting(nStationNumber) = TrainInf(nTrainNumber2).Arrival(nStationNumber) Then
                nArrivalStart2 = 2
            End If
        End If
        nTemp = nPanDuanFenChaJiaoCha(nStationNumber, nTrainNumber1, nArrivalStart1, nTrainNumber2, nArrivalStart2)
        If nTemp = 0 Then
            If nQianorHou = 0 Then
                nDiffDirectionPass = 270
            Else
                nDiffDirectionPass = 280
            End If
        Else
            If nQianorHou = 0 Then
                If nArrivalStart2 = 0 Then
                    '前到后通（13）
                    nDiffDirectionPass = 271
                ElseIf nArrivalStart2 = 1 Then
                    '前发后通（14）
                    nDiffDirectionPass = 272
                ElseIf nArrivalStart2 = 2 Then
                    '前通后通（17）
                    nDiffDirectionPass = 273
                End If
            Else
                If nArrivalStart2 = 0 Then
                    '前通后到（15）
                    nDiffDirectionPass = 281
                ElseIf nArrivalStart2 = 1 Then
                    '前通后发（16）
                    nDiffDirectionPass = 282
                ElseIf nArrivalStart2 = 2 Then
                    '前通后通（17）
                    nDiffDirectionPass = 283
                End If
            End If
        End If
    End Function

    Function nIfStopInWaiBaoZX(ByVal ntmpTrain As Integer, ByVal nStatemp As Integer) As Integer
        Dim i As Integer, nTemp As Integer
        nTemp = 0
        nIfStopInWaiBaoZX = 0
        For i = 1 To StationInf(nStatemp).nStLineNum
            If StationInf(nStatemp).sStLineNo(i) = TrainInf(ntmpTrain).StopLine(nStatemp) Then
                nTemp = i
                Exit For
            End If
        Next i
        If nTemp <> 0 Then
            If Left(StationInf(nStatemp).nStLineUse(nTemp), 1) = 3 Or Left(StationInf(nStatemp).nStLineUse(nTemp), 1) = 4 Then
                nIfStopInWaiBaoZX = 1
            End If
        End If
    End Function

    Function nPanDuanFenChaJiaoCha(ByVal nStationtemp As Integer, _
    ByVal nTrainNum1 As Integer, ByVal nArrivalStart1 As Integer, _
    ByVal nTrainNum2 As Integer, ByVal nArrivalStart2 As Integer) As Integer
        Dim i As Integer
        Dim nTemp1 As Integer, nTemp2 As Integer
        Dim nFenChatemp As Integer, ntmpAandS As Integer
        Dim sPointTemp1 As String, sPointTemp2 As String

        nFenChatemp = nStationtemp 'nFindFenChaZhan(nStationtemp)
        sPointTemp1 = ""
        sPointTemp2 = ""

        For i = 1 To TrainInf(nTrainNum1).NumWay
            If StationInf(nFenChatemp).sStationName = TrainInf(nTrainNum1).Way1(i) Then
                nTemp1 = i
                Exit For
            End If
        Next i
        For i = 1 To TrainInf(nTrainNum2).NumWay
            If StationInf(nFenChatemp).sStationName = TrainInf(nTrainNum2).Way1(i) Then
                nTemp2 = i
                Exit For
            End If
        Next i

        'sPointTemp1 = sFindFenChaZhanPoint(nFenChatemp, nTrainNum1, nArrivalStart1)
        'sPointTemp2 = sFindFenChaZhanPoint(nFenChatemp, nTrainNum2, nArrivalStart2)
        Select Case nArrivalStart2
            Case 0
                Select Case nArrivalStart1
                    Case 0
                        If TrainInf(nTrainNum1).Way3(nTemp1) = TrainInf(nTrainNum2).Way3(nTemp2) Then
                            ntmpAandS = 100
                        Else
                            ntmpAandS = 0
                        End If
                    Case 1
                        ntmpAandS = 1
                    Case 2
                        If TrainInf(nTrainNum1).Way3(nTemp1) = TrainInf(nTrainNum2).Way3(nTemp2) Then
                            ntmpAandS = 100
                        Else
                            ntmpAandS = 2
                        End If
                End Select
                If ntmpAandS <> 100 Then
                    sPointTemp1 = sFindFenChaZhanPoint(nFenChatemp, nTrainNum1, ntmpAandS)
                    sPointTemp2 = sFindFenChaZhanPoint(nFenChatemp, nTrainNum2, nArrivalStart2)
                End If
            Case 1
                Select Case nArrivalStart1
                    Case 0
                        ntmpAandS = 0
                    Case 1
                        If TrainInf(nTrainNum1).Way2(nTemp1) = TrainInf(nTrainNum2).Way2(nTemp2) Then
                            ntmpAandS = 100
                        Else
                            ntmpAandS = 1
                        End If
                    Case 2
                        If TrainInf(nTrainNum1).Way2(nTemp1) = TrainInf(nTrainNum2).Way2(nTemp2) Then
                            ntmpAandS = 100
                        Else
                            ntmpAandS = 2
                        End If
                End Select
                If ntmpAandS <> 100 Then
                    sPointTemp1 = sFindFenChaZhanPoint(nFenChatemp, nTrainNum1, ntmpAandS)
                    sPointTemp2 = sFindFenChaZhanPoint(nFenChatemp, nTrainNum2, nArrivalStart2)
                End If
            Case 2
                Select Case nArrivalStart1
                    Case 0
                        If TrainInf(nTrainNum1).Way3(nTemp1) = TrainInf(nTrainNum2).Way3(nTemp2) Then
                            ntmpAandS = 100
                        Else
                            ntmpAandS = 0
                        End If
                    Case 1
                        If TrainInf(nTrainNum1).Way2(nTemp1) = TrainInf(nTrainNum2).Way2(nTemp2) Then
                            ntmpAandS = 100
                        Else
                            ntmpAandS = 1
                        End If
                    Case 2
                        If TrainInf(nTrainNum1).Way2(nTemp1) = TrainInf(nTrainNum2).Way2(nTemp2) _
                            Or TrainInf(nTrainNum1).Way3(nTemp1) = TrainInf(nTrainNum2).Way3(nTemp2) Then
                            ntmpAandS = 100
                        Else
                            ntmpAandS = 2
                        End If
                End Select
                If ntmpAandS <> 100 Then
                    sPointTemp1 = sFindFenChaZhanPoint(nFenChatemp, nTrainNum1, nArrivalStart1)
                    sPointTemp2 = sFindFenChaZhanPoint(nFenChatemp, nTrainNum2, ntmpAandS)
                End If
        End Select
        If ntmpAandS <> 100 Then
            nPanDuanFenChaJiaoCha = nFindSameDiffPoint(sPointTemp1, sPointTemp2)
        Else
            nPanDuanFenChaJiaoCha = 0
        End If
    End Function

    Function nFindSameDiffPoint(ByVal sTemp1 As String, ByVal sTemp2 As String) As Integer
        Dim i As Integer
        Dim j As Integer
        Dim p As Integer
        Dim sTrainPath As String
        Dim sTemp11 As String, sTemp22 As String
        If sTemp1 = "无" Or sTemp1 = "、" Or sTemp1 = "," Or sTemp1 = "，" Then
            sTemp1 = ""
        End If
        If sTemp2 = "无" Or sTemp2 = "、" Or sTemp2 = "," Or sTemp2 = "，" Then
            sTemp2 = ""
        End If

        If sTemp1 = "" Or sTemp2 = "" Then
            nFindSameDiffPoint = 0
            Exit Function
        End If

        nFindSameDiffPoint = 0
        sTemp11 = ""
        sTemp22 = ""
        Dim TrainPathSta() As String
        Dim StrPath() As String
        Dim strPath1() As String
        ReDim StrPath(0)
        ReDim strPath1(0)
        sTrainPath = sTemp1
        If sTrainPath <> "" Then
            If Right(sTrainPath, 1) = "," Or Right(sTrainPath, 1) = "，" Then
            Else
                sTrainPath = sTrainPath & ","
            End If

            ReDim TrainPathSta(0)
            p = 1
            For j = 1 To Len(sTrainPath)
                If Mid(sTrainPath, j, 1) = "," Or Mid(sTrainPath, j, 1) = "，" Or Mid(sTrainPath, j, 1) = "、" Then
                    If Trim(Mid(sTrainPath, p, j - p)) <> "" Then
                        ReDim Preserve TrainPathSta(UBound(TrainPathSta) + 1)
                        TrainPathSta(UBound(TrainPathSta)) = Mid(sTrainPath, p, j - p)
                        p = j + 1
                    End If
                End If
            Next j

            ReDim StrPath(UBound(TrainPathSta))
            For i = 1 To UBound(TrainPathSta)
                StrPath(i) = TrainPathSta(i)
            Next i
        End If

        sTrainPath = sTemp2
        If sTrainPath <> "" Then
            If Right(sTrainPath, 1) = "," Or Right(sTrainPath, 1) = "，" Then
            Else
                sTrainPath = sTrainPath & ","
            End If

            ReDim TrainPathSta(0)
            p = 1
            For j = 1 To Len(sTrainPath)
                If Mid(sTrainPath, j, 1) = "," Or Mid(sTrainPath, j, 1) = "，" Or Mid(sTrainPath, j, 1) = "、" Then
                    If Trim(Mid(sTrainPath, p, j - p)) <> "" Then
                        ReDim Preserve TrainPathSta(UBound(TrainPathSta) + 1)
                        TrainPathSta(UBound(TrainPathSta)) = Mid(sTrainPath, p, j - p)
                        p = j + 1
                    End If
                End If
            Next j

            ReDim strPath1(UBound(TrainPathSta))
            For i = 1 To UBound(TrainPathSta)
                strPath1(i) = TrainPathSta(i)
            Next i
        End If

        For i = 1 To UBound(StrPath)
            For j = 1 To UBound(strPath1)
                If StrPath(i) = StrPath(1) Then
                    nFindSameDiffPoint = 1
                    Exit For
                End If
            Next j
            If nFindSameDiffPoint = 1 Then
                Exit For
            End If
        Next i

        '
        '    For i = 1 To Len(sTemp1) + 1
        '        If Mid(sTemp1, i, 1) <> "、" And i <> Len(sTemp1) + 1 Then
        '            sTemp11 = sTemp11 + Mid(sTemp1, i, 1)
        '        Else
        '            For j = 1 To Len(sTemp2) + 1
        '                If Mid(sTemp2, j, 1) <> "、" And j <> Len(sTemp2) + 1 Then
        '                    sTemp22 = sTemp22 + Mid(sTemp2, j, 1)
        '                Else
        '                    If sTemp22 = sTemp11 And sTemp11 <> "" Then
        '                        nFindSameDiffPoint = 1
        '                        Exit For
        '                    End If
        '                    sTemp22 = ""
        '                End If
        '            Next j
        '            sTemp11 = ""
        '            If nFindSameDiffPoint = 1 Then Exit For
        '        End If
        '    Next i
    End Function

    Function sFindFenChaZhanPoint(ByVal nStationtemp As Integer, _
    ByVal nTrainNumber As Integer, ByVal nArrivalStart As Integer) As String

        sFindFenChaZhanPoint = ""

        '找出分岔站对应股道的到发进路所经过的道岔
        Dim i As Integer
        Dim nTemp As Integer
        Dim StopLine As String
        Dim CurSta As String
        Dim ComeSta As String
        Dim ToSta As String
        StopLine = TrainInf(nTrainNumber).StopLine(nStationtemp)
        For i = 1 To TrainInf(nTrainNumber).NumWay
            If StationInf(nStationtemp).sStationName = TrainInf(nTrainNumber).Way1(i) Then
                nTemp = i
                Exit For
            End If
        Next i

        CurSta = StationInf(nStationtemp).sStationName
        ComeSta = TrainInf(nTrainNumber).Way3(nTemp)
        ToSta = TrainInf(nTrainNumber).Way2(nTemp)
        Dim tmp1 As String
        Dim tmp2 As String
        Select Case nArrivalStart
            Case 0 '到达
                sFindFenChaZhanPoint = sFindFenChaZhanFromPoint(nStationtemp, nTrainNumber, ComeSta, "K", StopLine)
            Case 1 '出发
                sFindFenChaZhanPoint = sFindFenChaZhanToPoint(nStationtemp, nTrainNumber, "K", ToSta, StopLine)
            Case 2 '通过
                tmp1 = sFindFenChaZhanFromPoint(nStationtemp, nTrainNumber, ComeSta, CurSta, StopLine)
                tmp2 = sFindFenChaZhanToPoint(nStationtemp, nTrainNumber, CurSta, ToSta, StopLine)
                sFindFenChaZhanPoint = tmp1 & "," & tmp2
        End Select

        If sFindFenChaZhanPoint = "," Then
            sFindFenChaZhanPoint = ""
        End If
        If sFindFenChaZhanPoint = "" Then
            'MsgBox TrainInf(nTrainNumber).Train & " 在 " & StationInf(nStationtemp).sStationName & "没有找到进路!!,请检查车站进路信息是否有错!!"
        End If

    End Function

    Function sFindFenChaZhanFromPoint(ByVal nStationtemp As Integer, _
    ByVal nTrainNumber As Integer, ByVal ComeSta As String, ByVal ToSta As String, ByVal StopLine As String) As String
        ''找出分岔站对应股道的到达进路所经过的道岔
        'Dim i As Integer
        sFindFenChaZhanFromPoint = ""
        ''江编的代码，进站进路
        'For i = 1 To UBound(StationInf(nStationtemp).sjComeSta)
        '    If StationInf(nStationtemp).sjComeSta(i) = ComeSta And StationInf(nStationtemp).sjNextSta(i) = ToSta _
        '        And StationInf(nStationtemp).sjGuoDaoNum(i) = StopLine Then
        '        If StationInf(nStationtemp).sjBaseJinLu(i) = "无" Then
        '            sFindFenChaZhanFromPoint = ""
        '        Else
        '            sFindFenChaZhanFromPoint = StationInf(nStationtemp).sjBaseJinLu(i)
        '        End If
        '        Exit Function
        '    End If
        'Next i
    End Function

    Function sFindFenChaZhanToPoint(ByVal nStationtemp As Integer, _
    ByVal nTrainNumber As Integer, ByVal ComeSta As String, ByVal ToSta As String, ByVal StopLine As String) As String
        '找出分岔站对应股道的到达进路所经过的道岔
        'Dim i As Integer
        sFindFenChaZhanToPoint = ""
        'For i = 1 To UBound(StationInf(nStationtemp).sjComeSta)
        '    If StationInf(nStationtemp).sjComeSta(i) = ComeSta And StationInf(nStationtemp).sjNextSta(i) = ToSta _
        '        And StationInf(nStationtemp).sjGuoDaoNum(i) = StopLine Then
        '        sFindFenChaZhanToPoint = StationInf(nStationtemp).sjBaseJinLu(i)
        '        If sFindFenChaZhanToPoint = "无" Then sFindFenChaZhanToPoint = ""
        '        Exit Function
        '    End If
        'Next i
    End Function

    Function nSameDirectionPass(ByVal nTrainNumber As Integer, ByVal nTrainNumberBen As Integer, ByVal nStationNumber As Integer, _
    ByVal nQianorHou As Integer, ByVal nArrivalStart As Integer, ByVal nSectionNumber As Integer, ByVal lTimeArrival As Long) As Integer
        Dim i As Integer
        Dim nTemp As Integer, nTempBen As Integer
        Dim bIfCheckJianGe As Boolean
        Dim ltmp1 As Long ', ltmp2 As Long
        Dim tmpRunFir As Long
        Dim tmpRunBen As Long
        ' Dim bIfCheckJianGe As Boolean

        If SectionInf(nSectionNumber).sBlock = "自动闭塞" Then
            '双线自动闭塞
            If TrainInf(nTrainNumber).Arrival(nStationNumber) = TrainInf(nTrainNumber).Starting(nStationNumber) Then
                bIfCheckJianGe = True
                '            If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
                '                For i = 1 To TrainInf(nTrainNumber).NumWay
                '                    If TrainInf(nTrainNumber).Way1(i) = StationInf(nStationNumber).sStationName Then
                '                        nTemp = i
                '                        Exit For
                '                    End If
                '                Next i
                '                For i = 1 To TrainInf(nTrainNumberBen).NumWay
                '                    If TrainInf(nTrainNumberBen).Way1(i) = StationInf(nStationNumber).sStationName Then
                '                        nTempBen = i
                '                        Exit For
                '                    End If
                '                Next i
                '                bIfCheckJianGe = False
                '            End If
                If nQianorHou = 0 Then '与前比
                    If bIfCheckJianGe = False Then
                        If TrainInf(nTrainNumber).Way2(nTemp) = TrainInf(nTrainNumberBen).Way2(nTempBen) _
                            Or TrainInf(nTrainNumber).Way2(nTemp) = TrainInf(nTrainNumberBen).Way2(nTempBen) Then
                            nSameDirectionPass = 8 '通通
                        ElseIf TrainInf(nTrainNumber).Way3(nTemp) <> TrainInf(nTrainNumberBen).Way3(nTempBen) _
                            Or TrainInf(nTrainNumber).Way2(nTemp) = TrainInf(nTrainNumberBen).Way2(nTempBen) Then
                            nSameDirectionPass = 3 '发通
                        ElseIf TrainInf(nTrainNumber).Way2(nTemp) <> TrainInf(nTrainNumberBen).Way2(nTempBen) _
                            Or TrainInf(nTrainNumber).Way3(nTemp) = TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                            nSameDirectionPass = 7 '到通
                        ElseIf TrainInf(nTrainNumber).Way2(nTemp) <> TrainInf(nTrainNumberBen).Way2(nTempBen) _
                            Or TrainInf(nTrainNumber).Way3(nTemp) <> TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                            nSameDirectionPass = 600 '到通
                        End If
                    Else
                        If nArrivalStart = 1 Then '出发
                            If StationInf(nStationNumber).sStationName = StationInf(SectionInf(nSectionNumber).nHStation).sStationName Then
                                nTemp = SectionInf(nSectionNumber).nQStation
                            Else
                                nTemp = SectionInf(nSectionNumber).nHStation
                            End If
                            ltmp1 = TimeMinus(lTimeArrival, TrainInf(nTrainNumber).Starting(nStationNumber))
                            tmpRunFir = TimeActualRun(nTrainNumber, nStationNumber, nTemp)
                            If tmpRunFir = 0 Then
                                nSameDirectionPass = 8 '通发
                            Else
                                tmpRunBen = TimeRun(nTrainNumberBen, nStationNumber, nTemp) + TimeQ(nTrainNumber, nStationNumber, nTemp)
                                If tmpRunFir >= tmpRunBen Then '前车慢后车快
                                    If ltmp1 < tmpRunFir - tmpRunBen Then
                                        nSameDirectionPass = 820 '区间交叉'前车慢后车快
                                    Else
                                        nSameDirectionPass = 8 '通发
                                    End If
                                Else
                                    nSameDirectionPass = 8 '通发
                                End If
                            End If
                        Else  '到达
                            If StationInf(nStationNumber).sStationName = StationInf(SectionInf(nSectionNumber).nHStation).sStationName Then
                                nTemp = SectionInf(nSectionNumber).nQStation
                            Else
                                nTemp = SectionInf(nSectionNumber).nHStation
                            End If
                            ltmp1 = TimeMinus(lTimeArrival, TrainInf(nTrainNumber).Starting(nStationNumber))
                            tmpRunFir = TimeActualRun(nTrainNumber, nTemp, nStationNumber)
                            If tmpRunFir = 0 Then
                                nSameDirectionPass = 8 '通发
                            Else
                                tmpRunBen = TimeRun(nTrainNumberBen, nTemp, nStationNumber)
                                If tmpRunFir <= tmpRunBen Then '前车快后车慢
                                    If ltmp1 < tmpRunBen - tmpRunFir Then
                                        nSameDirectionPass = 810 '区间交叉前车快后车慢
                                    Else
                                        nSameDirectionPass = 8 '通发
                                    End If
                                Else
                                    nSameDirectionPass = 8 '通发
                                End If
                            End If
                        End If
                    End If
                ElseIf nQianorHou = 1 Then '与后比
                    If bIfCheckJianGe = False Then
                        If TrainInf(nTrainNumber).Way2(nTemp) = TrainInf(nTrainNumberBen).Way2(nTempBen) _
                            Or TrainInf(nTrainNumber).Way2(nTemp) = TrainInf(nTrainNumberBen).Way2(nTempBen) Then
                            nSameDirectionPass = 8 '通通
                        ElseIf TrainInf(nTrainNumber).Way3(nTemp) <> TrainInf(nTrainNumberBen).Way3(nTempBen) _
                            Or TrainInf(nTrainNumber).Way2(nTemp) = TrainInf(nTrainNumberBen).Way2(nTempBen) Then
                            nSameDirectionPass = 0 '通发
                        ElseIf TrainInf(nTrainNumber).Way2(nTemp) <> TrainInf(nTrainNumberBen).Way2(nTempBen) _
                            Or TrainInf(nTrainNumber).Way3(nTemp) = TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                            nSameDirectionPass = 5 '通到
                        ElseIf TrainInf(nTrainNumber).Way2(nTemp) <> TrainInf(nTrainNumberBen).Way2(nTempBen) _
                            Or TrainInf(nTrainNumber).Way3(nTemp) <> TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                            nSameDirectionPass = 600 '通到
                        End If
                    Else
                        If StationInf(nStationNumber).sStationName = StationInf(SectionInf(nSectionNumber).nHStation).sStationName Then
                            nTemp = SectionInf(nSectionNumber).nQStation
                        ElseIf StationInf(nStationNumber).sStationName = StationInf(SectionInf(nSectionNumber).nQStation).sStationName Then
                            nTemp = SectionInf(nSectionNumber).nHStation
                        End If
                        If nTemp > 0 Then
                            ltmp1 = TimeMinus(TrainInf(nTrainNumber).Starting(nStationNumber), lTimeArrival)
                            tmpRunFir = TimeActualRun(nTrainNumber, nStationNumber, nTemp)
                            If tmpRunFir = 0 Then
                                nSameDirectionPass = 8 '通发
                            Else
                                tmpRunBen = TimeRun(nTrainNumberBen, nStationNumber, nTemp) + StationInf(nTemp).IKK(5) '加上通到时间
                                If nArrivalStart = 1 Then '出发判断
                                    If tmpRunFir <= tmpRunBen Then '前车快后车慢
                                        If ltmp1 < tmpRunBen - tmpRunFir Then
                                            nSameDirectionPass = 810 '区间交叉'前车快后车慢
                                        Else
                                            nSameDirectionPass = 8 '通发
                                        End If
                                    Else
                                        nSameDirectionPass = 8 '通发
                                    End If
                                Else '到达判断
                                    nSameDirectionPass = 8 '通发
                                End If
                            End If
                        Else
                            nSameDirectionPass = 8 '通发
                        End If
                    End If
                End If
            Else
                bIfCheckJianGe = True
                '            If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
                '                For i = 1 To TrainInf(nTrainNumber).NumWay
                '                    If TrainInf(nTrainNumber).Way1(i) = StationInf(nStationNumber).sStationName Then
                '                        nTemp = i
                '                        Exit For
                '                    End If
                '                Next i
                '                For i = 1 To TrainInf(nTrainNumberBen).NumWay
                '                    If TrainInf(nTrainNumberBen).Way1(i) = StationInf(nStationNumber).sStationName Then
                '                        nTempBen = i
                '                        Exit For
                '                    End If
                '                Next i
                '                bIfCheckJianGe = False
                '            End If

                If nQianorHou = 0 Then '与前比
                    If nArrivalStart = 0 Then '到达
                        If bIfCheckJianGe = False Then
                            If TrainInf(nTrainNumberBen).Way3(nTempBen) = TrainInf(nTrainNumber).Way3(nTemp) Then
                                nSameDirectionPass = 7 '到通
                            Else
                                nSameDirectionPass = 600
                            End If
                        Else
                            nSameDirectionPass = 7 '到通
                        End If
                    ElseIf nArrivalStart = 1 Then '出发
                        If bIfCheckJianGe = False Then
                            If TrainInf(nTrainNumberBen).Way2(nTempBen) = TrainInf(nTrainNumber).Way2(nTemp) Then
                                nSameDirectionPass = 3 '发通
                            Else
                                nSameDirectionPass = 600
                            End If
                        Else
                            If StationInf(nStationNumber).sStationName = StationInf(SectionInf(nSectionNumber).nHStation).sStationName Then
                                nTemp = SectionInf(nSectionNumber).nQStation
                            Else
                                nTemp = SectionInf(nSectionNumber).nHStation
                            End If
                            ltmp1 = TimeMinus(lTimeArrival, TrainInf(nTrainNumber).Starting(nStationNumber))
                            tmpRunFir = TimeActualRun(nTrainNumber, nStationNumber, nTemp)
                            If tmpRunFir = 0 Then
                                nSameDirectionPass = 3 '发通
                            Else
                                tmpRunBen = TimeRun(nTrainNumberBen, nStationNumber, nTemp)
                                If tmpRunFir >= tmpRunBen Then '前车慢后车快
                                    If ltmp1 < tmpRunFir - tmpRunBen Then
                                        nSameDirectionPass = 820 '区间交叉'前车慢后车快
                                    Else
                                        nSameDirectionPass = 3 '发通
                                    End If
                                Else
                                    nSameDirectionPass = 3 '发通
                                End If
                            End If
                            'nSameDirectionPass = 3 '发通
                        End If
                    End If
                ElseIf nQianorHou = 1 Then '与后比
                    If nArrivalStart = 0 Then '到达
                        If bIfCheckJianGe = False Then
                            If TrainInf(nTrainNumberBen).Way3(nTempBen) = TrainInf(nTrainNumber).Way3(nTemp) Then
                                nSameDirectionPass = 5 '通到
                            Else
                                nSameDirectionPass = 600
                            End If
                        Else
                            nSameDirectionPass = 5 '通到
                        End If
                    ElseIf nArrivalStart = 1 Then  '出发
                        If bIfCheckJianGe = False Then
                            If TrainInf(nTrainNumberBen).Way2(nTempBen) = TrainInf(nTrainNumber).Way2(nTemp) Then
                                nSameDirectionPass = 0 '通发
                            Else
                                nSameDirectionPass = 600
                            End If
                        Else
                            If StationInf(nStationNumber).sStationName = StationInf(SectionInf(nSectionNumber).nHStation).sStationName Then
                                nTemp = SectionInf(nSectionNumber).nQStation
                            Else
                                nTemp = SectionInf(nSectionNumber).nHStation
                            End If
                            ltmp1 = TimeMinus(TrainInf(nTrainNumber).Starting(nStationNumber), lTimeArrival)
                            tmpRunFir = TimeRun(nTrainNumber, nStationNumber, nTemp)
                            If tmpRunFir = 0 Then
                                nSameDirectionPass = 0 '通发
                            Else
                                tmpRunBen = TimeRun(nTrainNumberBen, nStationNumber, nTemp) + TimeQ(nTrainNumber, nStationNumber, nTemp)
                                If tmpRunFir <= tmpRunBen Then '前车快后车慢
                                    If ltmp1 < tmpRunBen - tmpRunFir Then
                                        nSameDirectionPass = 810 '区间交叉'前车快后车慢
                                    Else
                                        nSameDirectionPass = 0 '通发
                                    End If
                                Else
                                    nSameDirectionPass = 0 '通发
                                End If
                            End If
                            ' nSameDirectionPass = 0 '通发
                    End If
                    End If
                End If
            End If
        ElseIf SectionInf(nSectionNumber).sBlock = "半自动闭塞" Then
            If TrainInf(nTrainNumber).Arrival(nStationNumber) = TrainInf(nTrainNumber).Starting(nStationNumber) Then
                If nQianorHou = 0 Then '与前比
                    If bIfCheckJianGe = False Then
                        If TrainInf(nTrainNumber).Way2(nTemp) = TrainInf(nTrainNumberBen).Way2(nTempBen) _
                            Or TrainInf(nTrainNumber).Way2(nTemp) = TrainInf(nTrainNumberBen).Way2(nTempBen) Then
                            nSameDirectionPass = 115 '通通
                        ElseIf TrainInf(nTrainNumber).Way3(nTemp) <> TrainInf(nTrainNumberBen).Way3(nTempBen) _
                            Or TrainInf(nTrainNumber).Way2(nTemp) = TrainInf(nTrainNumberBen).Way2(nTempBen) Then
                            nSameDirectionPass = 114 '发通
                        ElseIf TrainInf(nTrainNumber).Way2(nTemp) <> TrainInf(nTrainNumberBen).Way2(nTempBen) _
                            Or TrainInf(nTrainNumber).Way3(nTemp) = TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                            nSameDirectionPass = 113 '到通
                        ElseIf TrainInf(nTrainNumber).Way2(nTemp) <> TrainInf(nTrainNumberBen).Way2(nTempBen) _
                            Or TrainInf(nTrainNumber).Way3(nTemp) <> TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                            nSameDirectionPass = 600 '到通
                        End If
                    Else
                        nSameDirectionPass = 115 '通通
                    End If
                ElseIf nQianorHou = 1 Then '与后比
                    If bIfCheckJianGe = False Then
                        If TrainInf(nTrainNumber).Way2(nTemp) = TrainInf(nTrainNumberBen).Way2(nTempBen) _
                            Or TrainInf(nTrainNumber).Way2(nTemp) = TrainInf(nTrainNumberBen).Way2(nTempBen) Then
                            nSameDirectionPass = 118  '通通
                        ElseIf TrainInf(nTrainNumber).Way3(nTemp) <> TrainInf(nTrainNumberBen).Way3(nTempBen) _
                            Or TrainInf(nTrainNumber).Way2(nTemp) = TrainInf(nTrainNumberBen).Way2(nTempBen) Then
                            nSameDirectionPass = 117 '通发
                        ElseIf TrainInf(nTrainNumber).Way2(nTemp) <> TrainInf(nTrainNumberBen).Way2(nTempBen) _
                            Or TrainInf(nTrainNumber).Way3(nTemp) = TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                            nSameDirectionPass = 116 '通到
                        ElseIf TrainInf(nTrainNumber).Way2(nTemp) <> TrainInf(nTrainNumberBen).Way2(nTempBen) _
                            Or TrainInf(nTrainNumber).Way3(nTemp) <> TrainInf(nTrainNumberBen).Way3(nTempBen) Then
                            nSameDirectionPass = 600 '通到
                        End If
                    Else
                        nSameDirectionPass = 118 '通通
                    End If
                End If
            Else
                bIfCheckJianGe = True
                If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
                    For i = 1 To TrainInf(nTrainNumber).NumWay
                        If TrainInf(nTrainNumber).Way1(i) = StationInf(nStationNumber).sStationName Then
                            nTemp = i
                            Exit For
                        End If
                    Next i
                    For i = 1 To TrainInf(nTrainNumberBen).NumWay
                        If TrainInf(nTrainNumberBen).Way1(i) = StationInf(nStationNumber).sStationName Then
                            nTempBen = i
                            Exit For
                        End If
                    Next i
                    bIfCheckJianGe = False
                End If
                If nQianorHou = 0 Then '与前比
                    If nArrivalStart = 0 Then '到达
                        If bIfCheckJianGe = False Then
                            If TrainInf(nTrainNumberBen).Way2(nTempBen) = TrainInf(nTrainNumber).Way2(nTemp) Then
                                nSameDirectionPass = 113 '到通
                            Else
                                nSameDirectionPass = 600
                            End If
                        Else
                            nSameDirectionPass = 113 '到通
                        End If
                    ElseIf nArrivalStart = 1 Then '出发
                        If bIfCheckJianGe = False Then
                            If TrainInf(nTrainNumberBen).Way3(nTempBen) = TrainInf(nTrainNumber).Way3(nTemp) Then
                                nSameDirectionPass = 114 '发通
                            Else
                                nSameDirectionPass = 600
                            End If
                        Else
                            nSameDirectionPass = 114 '发通
                        End If
                    End If
                ElseIf nQianorHou = 1 Then '与后比
                    If nArrivalStart = 0 Then '到达
                        If bIfCheckJianGe = False Then
                            If TrainInf(nTrainNumberBen).Way2(nTempBen) = TrainInf(nTrainNumber).Way2(nTemp) Then
                                nSameDirectionPass = 116 '通到
                            Else
                                nSameDirectionPass = 600
                            End If
                        Else
                            nSameDirectionPass = 116 '通到
                        End If
                    ElseIf nArrivalStart = 1 Then '出发
                        If bIfCheckJianGe = False Then
                            If TrainInf(nTrainNumberBen).Way3(nTempBen) = TrainInf(nTrainNumber).Way3(nTemp) Then
                                nSameDirectionPass = 117 '通发
                            Else
                                nSameDirectionPass = 600
                            End If
                        Else
                            nSameDirectionPass = 117 '通发
                        End If
                    End If
                End If
            End If
        End If
    End Function


    Function nTrainStopFanZheng(ByVal nTrainNumber As Integer, ByVal nStationNumber As Integer) As Integer
        Dim i As Integer, nTemp As Integer
        nTemp = 0
        For i = 1 To StationInf(nStationNumber).nStLineNum
            If StationInf(nStationNumber).sStLineNo(i) = TrainInf(nTrainNumber).StopLine(nStationNumber) Then
                nTemp = i
                Exit For
            End If
        Next i
        If nTemp = 0 Then
            If TrainInf(nTrainNumber).TrainClass = 12 Then
                If StationInf(nStationNumber).sStationName = TrainInf(nTrainNumber).StartStation Then
                    If TrainInf(nTrainNumber).sTrainUsageSF = "车底出库" Then
                        nTrainStopFanZheng = nKeJiZhanZhengFanXiang(nTrainNumber, "出发")
                    ElseIf TrainInf(nTrainNumber).sTrainUsageSF = "车底入库" Then
                        nTrainStopFanZheng = 0
                    End If
                ElseIf StationInf(nStationNumber).sStationName = TrainInf(nTrainNumber).EndStation Then
                    If TrainInf(nTrainNumber).sTrainUsageZD = "车底出库" Then
                        nTrainStopFanZheng = 0
                    ElseIf TrainInf(nTrainNumber).sTrainUsageZD = "车底入库" Then
                        nTrainStopFanZheng = nKeJiZhanZhengFanXiang(nTrainNumber, "到达")
                    End If
                End If
            ElseIf TrainInf(nTrainNumber).TrainClass = 38 Then
                If StationInf(nStationNumber).sStationName = TrainInf(nTrainNumber).StartStation Then
                    If TrainInf(nTrainNumber).sTrainUsageSF = "单机出库" Then
                        nTrainStopFanZheng = 0
                    ElseIf TrainInf(nTrainNumber).sTrainUsageSF = "单机入库" Then
                        nTrainStopFanZheng = 0
                    End If
                ElseIf StationInf(nStationNumber).sStationName = TrainInf(nTrainNumber).EndStation Then
                    If TrainInf(nTrainNumber).sTrainUsageZD = "单机出库" Then
                        nTrainStopFanZheng = 0
                    ElseIf TrainInf(nTrainNumber).sTrainUsageZD = "单机入库" Then
                        nTrainStopFanZheng = 0
                    End If
                End If
            End If
        Else
            'If nStationNumber = 21 Then Stop
            Select Case Int(StationInf(nStationNumber).nStLineUse(nTemp) / 1000)

                Case 1 '下行正线
                    nTrainStopFanZheng = 0
                    '        If TrainInf(nTrainNumber).NextStation = StationInf(nStationNumber).sStationName Then
                    '                If TrainInf(nTrainNumber).TrainReturnStyle(2) = "站前折返" Then
                    '                      If StationInf(nStationNumber).sTSuse(nTemp) = "站前折返线" Then
                    '                         nTrainStopFanZheng = 1
                    '                End If
                    '                End If
                    '         End If
                    '             Stop
                Case 2 '上行正线
                    nTrainStopFanZheng = 0
                    '         If TrainInf(nTrainNumber).NextStation = StationInf(nStationNumber).sStationName Then
                    '                 If TrainInf(nTrainNumber).TrainReturnStyle(2) = "站前折返" Then
                    '                    If StationInf(nStationNumber).sTSuse(nTemp) = "站前折返线" Then
                    '                         nTrainStopFanZheng = 1
                    '                     End If
                    '                End If
                    '           End If
                    '               Stop
                Case 3 '外包下行正线
                    nTrainStopFanZheng = 0
                    '               Stop
                Case 4 '外包上行正线
                    nTrainStopFanZheng = 0
                Case 5 '正线
                    nTrainStopFanZheng = 0
                    If TrainInf(nTrainNumber).NextStation = StationInf(nStationNumber).sStationName Then
                        If TrainInf(nTrainNumber).TrainReturnStyle(2) = "站前折返" Then
                            nTrainStopFanZheng = 1
                        End If
                    End If

                    '            Stop
                Case 6 '上行到发线
                    If nDirection(nTrainNumber) = 1 Then
                        nTrainStopFanZheng = 1
                        '                Stop
                    Else
                        nTrainStopFanZheng = 0
                    End If
                Case 6 '下行到发线
                    If nDirection(nTrainNumber) = 2 Then
                        nTrainStopFanZheng = 1
                        '                Stop
                    Else
                        nTrainStopFanZheng = 0
                    End If
                Case 8 '上下行到发线
                    If nDirection(nTrainNumber) = 1 Then
                        nTrainStopFanZheng = 1
                    Else
                        nTrainStopFanZheng = 0
                    End If
                Case 9 '下上行到发线
                    If nDirection(nTrainNumber) = 2 Then
                        nTrainStopFanZheng = 1
                    Else
                        nTrainStopFanZheng = 0
                    End If
            End Select
        End If
    End Function

    Function nKeJiZhanZhengFanXiang(ByVal ntmpTrainNum As Integer, ByVal sDaoorFa As String) As Integer
        Select Case sDaoorFa
            Case "到达"
                If nDirection(ntmpTrainNum) = 1 Then
                    nKeJiZhanZhengFanXiang = 1
                ElseIf nDirection(ntmpTrainNum) = 2 Then
                    nKeJiZhanZhengFanXiang = 0
                End If
            Case "出发"
                If nDirection(ntmpTrainNum) = 1 Then
                    nKeJiZhanZhengFanXiang = 1
                ElseIf nDirection(ntmpTrainNum) = 2 Then
                    nKeJiZhanZhengFanXiang = 0
                End If
        End Select
    End Function

    Sub TXDZ(ByVal nTrnNum As Integer, ByVal lTim As Long, ByVal nSdirection As Integer, _
    ByVal nLeaveSection As Integer, ByVal ntmpStation As Integer, ByVal nEnterSection As Integer)

        'nTrnNum本次列车车次，tim当前时间，nStateRecord当前车站编号
        'KH客车或货车，nSdirection检查的方向
        '到站的同向判断
        Dim nDanShuang1 As Integer, nDanShuang2 As Integer
        Dim lDifftemp1 As Long, lDifftemp2 As Long
        Dim nEnterSec As Integer, nLeaveSec As Integer
        Dim nStaterecord As Integer
        Dim ntmpTianChuangSec As Integer, ntmpFa As Integer, ntmpDao As Integer
        nStaterecord = ntmpStation
        If StationInf(nStaterecord).sStationName = "" Then Stop

        nEnterSec = nEnterSection
        nLeaveSec = nLeaveSection
        ntmpTianChuangSec = nIfTianChuangSec(nLeaveSec)
        If ntmpTianChuangSec <> 0 Then
            Select Case TianChuangInf(ntmpTianChuangSec).sTianChuangKind
                Case "上行"
                    lDifftemp1 = TianChuangInf(ntmpTianChuangSec).lUpEmptyTime(1)
                    lDifftemp2 = TianChuangInf(ntmpTianChuangSec).lUpEmptyTime(2)
                    If nDirection(nTrnNum) = 1 Then
                        ntmpFa = EmptyTimeCheck(lDifftemp1, lDifftemp2, TrainInf(nTrnNum).Starting(SectionInf(nLeaveSec).nHStation))
                    Else
                        ntmpFa = EmptyTimeCheck(lDifftemp1, lDifftemp2, TrainInf(nTrnNum).Starting(SectionInf(nLeaveSec).nQStation))
                    End If
                    ntmpDao = EmptyTimeCheck(lDifftemp1, lDifftemp2, lTim)
                    If ntmpFa = 0 And ntmpDao <> 0 Then
                        nDanShuang1 = 1
                        nDanShuang2 = SectionInf(nEnterSec).nSection
                    ElseIf ntmpFa <> 0 And ntmpDao <> 0 Then
                        nDanShuang1 = 1
                        nDanShuang2 = SectionInf(nEnterSec).nSection
                    Else
                        nDanShuang1 = SectionInf(nLeaveSec).nSection
                        nDanShuang2 = SectionInf(nEnterSec).nSection
                    End If
                Case "下行"
                    lDifftemp1 = TianChuangInf(ntmpTianChuangSec).lDownEmptyTime(1)
                    lDifftemp2 = TianChuangInf(ntmpTianChuangSec).lDownEmptyTime(2)
                    If nDirection(nTrnNum) = 1 Then
                        ntmpFa = EmptyTimeCheck(lDifftemp1, lDifftemp2, TrainInf(nTrnNum).Starting(SectionInf(nLeaveSec).nHStation))
                    Else
                        ntmpFa = EmptyTimeCheck(lDifftemp1, lDifftemp2, TrainInf(nTrnNum).Starting(SectionInf(nLeaveSec).nQStation))
                    End If
                    ntmpDao = EmptyTimeCheck(lDifftemp1, lDifftemp2, lTim)
                    If ntmpFa = 0 And ntmpDao <> 0 Then
                        nDanShuang1 = 1
                        nDanShuang2 = SectionInf(nEnterSec).nSection
                    ElseIf ntmpFa <> 0 And ntmpDao <> 0 Then
                        nDanShuang1 = 1
                        nDanShuang2 = SectionInf(nEnterSec).nSection
                    Else
                        nDanShuang1 = SectionInf(nLeaveSec).nSection
                        nDanShuang2 = SectionInf(nEnterSec).nSection
                    End If
                Case "上下行"
                    lDifftemp1 = TianChuangInf(ntmpTianChuangSec).lUpEmptyTime(1)
                    lDifftemp2 = TianChuangInf(ntmpTianChuangSec).lUpEmptyTime(2)
                    ntmpDao = EmptyTimeCheck(lDifftemp1, lDifftemp2, lTim)
                    If ntmpDao <> 0 Then
                        TxIntervalKind1(nTrnNum, nStaterecord) = 300
                        TxDiffTime1(nTrnNum, nStaterecord) = TimeMinus(lTim, lDifftemp1)
                        TxDiffTrain1(nTrnNum, nStaterecord) = 0
                        TxIntervalKind2(nTrnNum, nStaterecord) = 300
                        TxDiffTime2(nTrnNum, nStaterecord) = TimeMinus(lDifftemp2, lTim)
                        TxDiffTrain2(nTrnNum, nStaterecord) = 0
                        FxIntervalKind1(nTrnNum, nStaterecord) = 300
                        FxDiffTime1(nTrnNum, nStaterecord) = TimeMinus(lTim, lDifftemp1)
                        FxDiffTrain1(nTrnNum, nStaterecord) = 0
                        FxIntervalKind2(nTrnNum, nStaterecord) = 300
                        FxDiffTime2(nTrnNum, nStaterecord) = TimeMinus(lDifftemp2, lTim)
                        FxDiffTrain2(nTrnNum, nStaterecord) = 0
                    End If
            End Select
        Else
            nDanShuang1 = SectionInf(nLeaveSec).nSection
            nDanShuang2 = SectionInf(nEnterSec).nSection
        End If
        If nDanShuang1 = 2 And nDanShuang2 = 2 Then '从双进双
            DoubleDoubleArrivalCheck(nTrnNum, lTim, nStaterecord, nSdirection, nLeaveSec, nEnterSec)
        ElseIf nDanShuang1 = 2 And nDanShuang2 = 1 Then '从双进单
            DoubleSingleArrivalCheck(nTrnNum, lTim, nStaterecord, nSdirection, nLeaveSec, nEnterSec)
        ElseIf nDanShuang1 = 1 And nDanShuang2 = 1 Then '从单进单
            SingleSingleArrivalCheck(nTrnNum, lTim, nStaterecord, nSdirection, nLeaveSec, nEnterSec)
        ElseIf nDanShuang1 = 1 And nDanShuang2 = 2 Then '从单进双
            SingleDoubleArrivalCheck(nTrnNum, lTim, nStaterecord, nSdirection, nLeaveSec, nEnterSec)
        End If
    End Sub
    Sub DoubleDoubleArrivalCheck(ByVal nTrainNumber As Integer, ByVal lTimeArrival As Long, _
    ByVal nStationNumber As Integer, ByVal nSdirection As Integer, _
    ByVal nSectionLeave As Integer, ByVal nSectionEnter As Integer)

        Dim BeforeTrainD As Integer, BeforeTrainF As Integer
        Dim BeforeTrainD1 As Integer, BeforeTrainF1 As Integer
        Dim AfterTrainD As Integer, AfterTrainF As Integer
        Dim AfterTrainD1 As Integer, AfterTrainF1 As Integer
        Dim lArrivalArrival As Long, lArrivalStart As Long
        Dim lArrivalArrival1 As Long, lArrivalStart1 As Long
        Dim ii As Integer, jj As Integer
        Dim ii1 As Integer, jj1 As Integer
        Dim nTemp As Integer
        Dim ltmp1 As Long
        Dim tmpRunFir As Long
        Dim tmpRunBen As Long
        If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000

            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nSameDirectionArrival(BeforeTrainD1, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If

            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nSameDirectionArrival(BeforeTrainF1, nTrainNumber, nStationNumber, 0, 1, nSectionEnter, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If

            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件

            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nSameDirectionArrival(AfterTrainD1, nTrainNumber, nStationNumber, 1, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If

            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nSameDirectionArrival(AfterTrainF1, nTrainNumber, nStationNumber, 1, 1, nSectionEnter, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionArrival(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If

            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionArrival(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000

            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件

            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件

            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件

            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionArrival(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If

            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件

            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionArrival(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionArrival(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionArrival(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

        Else
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nSameDirectionArrival(BeforeTrainD, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                If ii = 810 Then '区间交叉，前车快后车慢
                    If nStationNumber = SectionInf(nSectionLeave).nHStation Then
                        nTemp = SectionInf(nSectionLeave).nQStation
                    ElseIf nStationNumber = SectionInf(nSectionLeave).nQStation Then
                        nTemp = SectionInf(nSectionLeave).nHStation
                    End If
                    ltmp1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Starting(nStationNumber))
                    tmpRunFir = TimeActualRun(BeforeTrainD, nTemp, nStationNumber)
                    tmpRunBen = TimeRun(nTrainNumber, nTemp, nStationNumber) + TimeT(nTrainNumber, nTemp, nStationNumber) + TimeQ(nTrainNumber, nTemp, nStationNumber)
                    lArrivalArrival = tmpRunBen - tmpRunFir - ltmp1 + StationInf(nTemp).IKK(1)
                Else
                    lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                        ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
                End If
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nSameDirectionArrival(BeforeTrainF, nTrainNumber, nStationNumber, 0, 1, nSectionEnter, lTimeArrival)
                If lArrivalArrival = -100001 Then
                    lArrivalArrival = 0
                    lArrivalStart = 0
                Else
                    lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                        jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
                End If
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "", "", "") '2到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nSameDirectionArrival(AfterTrainD, nTrainNumber, nStationNumber, 1, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nSameDirectionArrival(AfterTrainF, nTrainNumber, nStationNumber, 1, 1, nSectionEnter, lTimeArrival)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

            '检查与反向的对向列车的敌对情况（只有到达和出发两种情况）
            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If

            If TrainInf(nTrainNumber).TrainClass = 38 _
                And StationInf(nStationNumber).sStationName = TrainInf(nTrainNumber).EndStation Then
                lArrivalArrival = -100000
                lArrivalStart = -100000
                CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
                CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 2)
            ElseIf TrainInf(nTrainNumber).TrainClass = 38 _
                And StationInf(nStationNumber).sStationName = TrainInf(nTrainNumber).StartStation Then
                lArrivalArrival = -100000
                lArrivalStart = -100000
                CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
                CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 2)
            Else
                If nTrainStopFanZheng(nTrainNumber, nStationNumber) = 1 Then
                    lArrivalArrival = -100000
                    lArrivalStart = -100000
                    BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                                    "", "", "") '到达条件
                    If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                        lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                        If TrainInf(BeforeTrainD).Arrival(nStationNumber) = TrainInf(BeforeTrainD).Starting(nStationNumber) Then
                            ii = 15 '对同通过,反到
                        Else
                            ii = 9 '对同到达，反到
                        End If
                        lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                            ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
                    End If
                    BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                                        "", "", "") '出发条件
                    If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                        lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                        If TrainInf(BeforeTrainF).Arrival(nStationNumber) = TrainInf(BeforeTrainF).Starting(nStationNumber) Then
                            jj = 15 '对同通过,反到
                        Else
                            jj = 10 '对同发车，反到
                        End If
                        lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                            jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
                    End If
                    CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
                    lArrivalArrival = -100000
                    lArrivalStart = -100000
                    AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                                    "", "", "") '到达条件
                    If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                        lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                        If TrainInf(AfterTrainD).Arrival(nStationNumber) = TrainInf(AfterTrainD).Starting(nStationNumber) Then
                            ii = 13 '反到，对同通过
                        Else
                            ii = 9 '反到，对同到达
                        End If
                        lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                            ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
                    End If
                    AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                                        "", "", "") '出发条件
                    If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                        lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                        If TrainInf(AfterTrainF).Arrival(nStationNumber) = TrainInf(AfterTrainF).Starting(nStationNumber) Then
                            jj = 13 '反到，对同通过
                        Else
                            jj = 11 '反到，对同出发
                        End If
                        lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                            jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
                    End If
                    CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
                Else
                    If nIfStopInWaiBaoZX(nTrainNumber, nStationNumber) = 0 Then
                        lArrivalArrival = -100000
                        lArrivalStart = -100000
                        BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                                        "", "", "") '到达条件
                        If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                            If TrainInf(BeforeTrainD).Arrival(nStationNumber) <> TrainInf(BeforeTrainD).Starting(nStationNumber) Then
                                '对向列车是否停站
                                If nTrainStopFanZheng(BeforeTrainD, nStationNumber) = 1 Then
                                    '对向列车是否停入反向股道
                                    lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                                    ii = 9 '对反向到达，同到达
                                    lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                                ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
                                End If
                            End If
                        End If
                        BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                                        "", "", "") '出发条件
                        If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                            If TrainInf(BeforeTrainF).Arrival(nStationNumber) <> TrainInf(BeforeTrainF).Starting(nStationNumber) Then
                                If nTrainStopFanZheng(BeforeTrainF, nStationNumber) = 1 Then
                                    lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                                    jj = 10 '对反向出发，同到达
                                    lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                                        jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
                                End If
                            End If
                        End If
                        CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
                        lArrivalArrival = -100000
                        lArrivalStart = -100000
                        AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                                        "", "", "") '到达条件
                        If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                            If TrainInf(AfterTrainD).Arrival(nStationNumber) <> TrainInf(AfterTrainD).Starting(nStationNumber) Then
                                If nTrainStopFanZheng(AfterTrainD, nStationNumber) = 1 Then
                                    lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                                    ii = 9 '同到达，对反向到达
                                    lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                                ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
                                End If
                            End If
                        End If
                        AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                                        "", "", "") '出发条件
                        If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                            If TrainInf(AfterTrainF).Arrival(nStationNumber) <> TrainInf(AfterTrainF).Starting(nStationNumber) Then
                                If nTrainStopFanZheng(AfterTrainF, nStationNumber) = 1 Then
                                    lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                                    jj = 11 '同到达，对反向出发
                                    lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                                        jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
                                End If
                            End If
                        End If
                        CalFxJiange(nTrainNumber, nStationNumber, AfterTrainF, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
                    End If
                End If
            End If
        End If
    End Sub

    Sub DoubleSingleArrivalCheck(ByVal nTrainNumber As Integer, ByVal lTimeArrival As Long, _
    ByVal nStationNumber As Integer, ByVal nSdirection As Integer, _
    ByVal nSectionLeave As Integer, ByVal nSectionEnter As Integer)

        Dim BeforeTrainD As Integer, BeforeTrainF As Integer
        Dim BeforeTrainD1 As Integer, BeforeTrainF1 As Integer
        Dim AfterTrainD As Integer, AfterTrainF As Integer
        Dim AfterTrainD1 As Integer, AfterTrainF1 As Integer
        Dim lArrivalArrival As Long, lArrivalStart As Long
        Dim lArrivalArrival1 As Long, lArrivalStart1 As Long
        Dim ii As Integer, jj As Integer
        Dim ii1 As Integer, jj1 As Integer
        Dim nTemp As Integer

        If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nSameDirectionArrival(BeforeTrainD1, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nSameDirectionArrival(BeforeTrainF1, nTrainNumber, nStationNumber, 0, 1, nSectionEnter, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nSameDirectionArrival(AfterTrainD1, nTrainNumber, nStationNumber, 1, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nSameDirectionArrival(AfterTrainF1, nTrainNumber, nStationNumber, 1, 1, nSectionEnter, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionArrival(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionArrival(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionArrival(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionArrival(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionArrival(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionArrival(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        Else
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nSameDirectionArrival(BeforeTrainD, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nSameDirectionArrival(BeforeTrainF, nTrainNumber, nStationNumber, 0, 1, nSectionEnter, lTimeArrival)
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "", "", "") '2到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nSameDirectionArrival(AfterTrainD, nTrainNumber, nStationNumber, 1, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nSameDirectionArrival(AfterTrainF, nTrainNumber, nStationNumber, 1, 1, nSectionEnter, lTimeArrival)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

            '检查与对向列车的敌对情况

            '以下应修改！！
            '只需要进行进路交叉（与双线相同）检查


            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nConflictArrival(BeforeTrainD, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nConflictArrival(BeforeTrainF, nStationNumber, 0, 1, nSectionLeave)
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "", "", "") '到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nConflictArrival(AfterTrainD, nStationNumber, 1, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nConflictArrival(AfterTrainF, nStationNumber, 1, 1, nSectionLeave)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        End If
    End Sub

    Sub DoubleSinglePassCheck(ByVal nTrainNumber As Integer, ByVal lTimeArrival As Long, _
    ByVal nStationNumber As Integer, ByVal nSdirection As Integer, _
    ByVal nSectionLeave As Integer, ByVal nSectionEnter As Integer)

        Dim BeforeTrainD As Integer, BeforeTrainF As Integer
        Dim AfterTrainD As Integer, AfterTrainF As Integer
        Dim lArrivalArrival As Long, lArrivalStart As Long
        Dim ii As Integer, jj As Integer
        Dim BeforeTrainD1 As Integer, BeforeTrainF1 As Integer
        Dim AfterTrainD1 As Integer, AfterTrainF1 As Integer
        Dim lArrivalArrival1 As Long, lArrivalStart1 As Long
        Dim ii1 As Integer, jj1 As Integer
        Dim nTemp As Integer

        If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nSameDirectionPass(BeforeTrainD1, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nSameDirectionPass(BeforeTrainF1, nTrainNumber, nStationNumber, 0, 1, nSectionEnter, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionPass(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionPass(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nSameDirectionPass(AfterTrainD1, nTrainNumber, nStationNumber, 1, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nSameDirectionPass(AfterTrainF1, nTrainNumber, nStationNumber, 1, 1, nSectionEnter, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionPass(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionPass(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nConflictPass(BeforeTrainD1, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionPass(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionPass(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionPass(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDanConflictH(lArrivalArrival1, AfterTrainD1, nTrainNumber, 0, _
                        nSectionLeave, nStationNumber, nSectionEnter)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionPass(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionPass(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionPass(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        Else
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nSameDirectionPass(BeforeTrainD, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nSameDirectionPass(BeforeTrainF, nTrainNumber, nStationNumber, 0, 1, nSectionEnter, lTimeArrival)
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nSameDirectionPass(AfterTrainD, nTrainNumber, nStationNumber, 1, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nSameDirectionPass(AfterTrainF, nTrainNumber, nStationNumber, 1, 1, nSectionEnter, lTimeArrival)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)




            '检查与对向列车的敌对情况(以下应做修改！
            '到达端与对向列车（停车时）出发比较有无交叉，
            '出发端与对向列车（通过、停车时）比较单线的间隔时间




            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 1, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nConflictPass(BeforeTrainD, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nConflictPass(BeforeTrainF, nStationNumber, 0, 1, nSectionLeave)
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "", "", "") '到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nDanConflictH(lArrivalArrival, AfterTrainD, nTrainNumber, 0, _
                        nSectionLeave, nStationNumber, nSectionEnter)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                        "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nConflictPass(AfterTrainF, nStationNumber, 1, 1, nSectionLeave)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 0, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        End If
    End Sub

    Sub SingleSingleArrivalCheck(ByVal nTrainNumber As Integer, ByVal lTimeArrival As Long, _
    ByVal nStationNumber As Integer, ByVal nSdirection As Integer, _
    ByVal nSectionLeave As Integer, ByVal nSectionEnter As Integer)

        Dim BeforeTrainD As Integer, BeforeTrainF As Integer
        Dim AfterTrainD As Integer, AfterTrainF As Integer
        Dim lArrivalArrival As Long, lArrivalStart As Long
        Dim ii As Integer, jj As Integer
        Dim BeforeTrainD1 As Integer, BeforeTrainF1 As Integer
        Dim AfterTrainD1 As Integer, AfterTrainF1 As Integer
        Dim lArrivalArrival1 As Long, lArrivalStart1 As Long
        Dim ii1 As Integer, jj1 As Integer
        Dim nTemp As Integer

        If Left(StationInf(nStationNumber).sStationProp, 1) = "F" Then

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nSameDirectionArrival(BeforeTrainD1, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nSameDirectionArrival(BeforeTrainF1, nTrainNumber, nStationNumber, 0, 1, nSectionEnter, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nSameDirectionArrival(AfterTrainD1, nTrainNumber, nStationNumber, 1, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nSameDirectionArrival(AfterTrainF1, nTrainNumber, nStationNumber, 1, 1, nSectionEnter, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionArrival(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionArrival(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)

            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nConflictArrival(BeforeTrainD1, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDanConflictQ(lArrivalStart1, BeforeTrainF1, nTrainNumber, 1, _
                        nSectionLeave, nStationNumber, nSectionEnter) '到达与对出发
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                BeforeTrainD = BeforeTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                BeforeTrainD = BeforeTrainF1
            End If
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            BeforeTrainD1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If BeforeTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD1).Arrival(nStationNumber))
                ii1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainD1, nStationNumber, 0, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(BeforeTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            BeforeTrainF1 = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If BeforeTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF1).Starting(nStationNumber))
                jj1 = nDiffDirectionArrival(nTrainNumber, BeforeTrainF1, nStationNumber, 0, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(BeforeTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, BeforeTrainD1, lArrivalArrival1, ii1, lArrivalStart1, BeforeTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                BeforeTrainF = BeforeTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                BeforeTrainF = BeforeTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)
            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "相同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nConflictArrival(AfterTrainD1, nStationNumber, 1, 0, nSectionLeave)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "相同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nConflictArrival(AfterTrainF1, nStationNumber, 1, 1, nSectionLeave)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                ii = ii1
                lArrivalArrival = lArrivalArrival1
                AfterTrainD = AfterTrainD1
            Else
                ii = jj1
                lArrivalArrival = lArrivalStart1
                AfterTrainD = AfterTrainF1
            End If

            lArrivalArrival1 = -100000
            lArrivalStart1 = -100000
            AfterTrainD1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "来自", "不同", sStationGoTo(nTrainNumber, nStationNumber)) '到达条件
            If AfterTrainD1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD1).StartStation Then
                lArrivalArrival1 = TimeMinus(TrainInf(AfterTrainD1).Arrival(nStationNumber), lTimeArrival)
                ii1 = nDiffDirectionArrival(nTrainNumber, AfterTrainD1, nStationNumber, 1, 0, lTimeArrival)
                lArrivalArrival1 = lIntervalTime(AfterTrainD1, nTrainNumber, _
                                    ii1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival1
            End If
            AfterTrainF1 = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "去往", "不同", sStationComeFrom(nTrainNumber, nStationNumber)) '出发条件
            If AfterTrainF1 <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF1).EndStation Then
                lArrivalStart1 = TimeMinus(TrainInf(AfterTrainF1).Starting(nStationNumber), lTimeArrival)
                jj1 = nDiffDirectionArrival(nTrainNumber, AfterTrainF1, nStationNumber, 1, 1, lTimeArrival)
                lArrivalStart1 = lIntervalTime(AfterTrainF1, nTrainNumber, _
                                    jj1, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart1
            End If
            nTemp = nSameJiange(nTrainNumber, AfterTrainD1, lArrivalArrival1, ii1, lArrivalStart1, AfterTrainF1, jj1)
            If nTemp = 1 Then
                jj = ii1
                lArrivalStart = lArrivalArrival1
                AfterTrainF = AfterTrainD1
            Else
                jj = jj1
                lArrivalStart = lArrivalStart1
                AfterTrainF = AfterTrainF1
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        Else
            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nSameDirectionArrival(BeforeTrainD, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nSameDirectionArrival(BeforeTrainF, nTrainNumber, nStationNumber, 0, 1, nSectionEnter, lTimeArrival)
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "", "", "") '2到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nSameDirectionArrival(AfterTrainD, nTrainNumber, nStationNumber, 1, 0, nSectionLeave, lTimeArrival)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nSameDirectionArrival(AfterTrainF, nTrainNumber, nStationNumber, 1, 1, nSectionEnter, lTimeArrival)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalTxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
            '检查与对向列车的敌对情况
            If nSdirection = 1 Then
                nSdirection = 2
            Else
                nSdirection = 1
            End If

            lArrivalArrival = -100000
            lArrivalStart = -100000
            BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "", "", "") '到达条件
            If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                ii = nConflictArrival(BeforeTrainD, nStationNumber, 0, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                    ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "", "", "") '出发条件
            If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                jj = nDanConflictQ(lArrivalStart, BeforeTrainF, nTrainNumber, 1, _
                        nSectionLeave, nStationNumber, nSectionEnter) '到达与对出发
                lArrivalStart = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                    jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, BeforeTrainD, lArrivalArrival, ii, lArrivalStart, BeforeTrainF, jj, 1)

            lArrivalArrival = -100000
            lArrivalStart = -100000
            AfterTrainD = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, _
                            "", "", "") '到达条件
            If AfterTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainD).StartStation Then
                lArrivalArrival = TimeMinus(TrainInf(AfterTrainD).Arrival(nStationNumber), lTimeArrival)
                ii = nConflictArrival(AfterTrainD, nStationNumber, 1, 0, nSectionLeave)
                lArrivalArrival = lIntervalTime(AfterTrainD, nTrainNumber, _
                                    ii, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalArrival
            End If
            AfterTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 0, _
                            "", "", "") '出发条件
            If AfterTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(AfterTrainF).EndStation Then
                lArrivalStart = TimeMinus(TrainInf(AfterTrainF).Starting(nStationNumber), lTimeArrival)
                jj = nConflictArrival(AfterTrainF, nStationNumber, 1, 1, nSectionLeave)
                lArrivalStart = lIntervalTime(AfterTrainF, nTrainNumber, _
                                    jj, 1, nSectionLeave, nStationNumber, nSectionEnter) - lArrivalStart
            End If
            CalFxJiange(nTrainNumber, nStationNumber, AfterTrainD, lArrivalArrival, ii, lArrivalStart, AfterTrainF, jj, 2)
        End If
    End Sub

    Sub TxFxDiffDim(ByVal nTemp As Integer, ByVal tmpNumstation As Integer)
        ReDim TxDiffTime1(nTemp, tmpNumstation)
        ReDim TxDiffTime2(nTemp, tmpNumstation)
        ReDim FxDiffTime1(nTemp, tmpNumstation)
        ReDim FxDiffTime2(nTemp, tmpNumstation)
        ReDim TxDiffTrain1(nTemp, tmpNumstation)
        ReDim TxDiffTrain2(nTemp, tmpNumstation)
        ReDim FxDiffTrain1(nTemp, tmpNumstation)
        ReDim FxDiffTrain2(nTemp, tmpNumstation)
        ReDim TxIntervalKind1(nTemp, tmpNumstation)
        ReDim TxIntervalKind2(nTemp, tmpNumstation)
        ReDim FxIntervalKind1(nTemp, tmpNumstation)
        ReDim FxIntervalKind2(nTemp, tmpNumstation)
    End Sub

    '整理所有列车时刻
    Public Sub ResertAllTrainStartOrArriTime()
        Dim i As Integer
        'Dim j As Integer
        'Dim nTrain As Integer
        Dim nFtrain As Integer
        'Dim nNtrain As Integer
        Dim nStoptime As Long
        For i = 1 To UBound(TrainInf)
            'If i = 7 Then Stop
            If TrainInf(i).Train <> "" Then
                nFtrain = TrainInf(i).TrainReturn(1)
                If TrainInf(i).TrainReturnStyle(1) = "站前折返" Or TrainInf(i).TrainReturnStyle(1) = "立即折返" Then
                    If nFtrain <> 0 Then
                        TrainInf(i).StopLine(TrainInf(i).nPathID(1)) = TrainInf(nFtrain).StopLine(TrainInf(i).nPathID(1))
                    End If
                End If
                nStoptime = GetCurTrainStopTimeAtStation(TrainInf(i).sJiaoLuName, TrainInf(i).sStopSclaeName, TrainInf(i).ComeStation)
                TrainInf(i).Arrival(TrainInf(i).nPathID(1)) = TimeMinus(TrainInf(i).Starting(TrainInf(i).nPathID(1)), nStoptime)
                Call RecordStaTime(i, TrainInf(i).nPathID(1), TrainInf(i).Starting(TrainInf(i).nPathID(1)), TrainInf(i).Arrival(TrainInf(i).nPathID(1)))
                TrainInf(i).sStartZFArrival = TrainInf(i).Arrival(TrainInf(i).nPathID(1))
                TrainInf(i).sStartZFStarting = TrainInf(i).Arrival(TrainInf(i).nPathID(1))
                TrainInf(i).sStartZFLine = ""
                nStoptime = GetCurTrainStopTimeAtStation(TrainInf(i).sJiaoLuName, TrainInf(i).sStopSclaeName, TrainInf(i).NextStation)
                TrainInf(i).Starting(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))) = TimeAdd(TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))), nStoptime)
                Call RecordStaTime(i, TrainInf(i).nPathID(UBound(TrainInf(i).nPathID)), TrainInf(i).Starting(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))), TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))))
                TrainInf(i).sEndZFStarting = TrainInf(i).Starting(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID)))
                TrainInf(i).sEndZFLine = ""
            End If
        Next i

    End Sub

    '计算折返股道和折返时间,nCalRule为计算规则，
    Public Sub CalZheFanTimeAndZheFanGuDao(ByVal sCalRule As String)
        ReDim TrainErrInf(0)
        Dim leftStartTime As Long
        Dim RightArriTime As Long
        Dim nCurSta As Integer
        Dim i As Integer
        Dim k As Integer
        Dim nFirTrain As Integer
        Dim nSecTrain As Integer
        Dim sCurZheFanLine As String
        sCurZheFanLine = ""
        Dim nMoveTime1 As Integer
        Dim nMovetime2 As Integer
        Dim nMinAtZFLineTime As Integer
        Dim nStoptime As Integer
        Dim nStoptime2 As Integer
        Dim nMinZftime As Integer
        Dim nActZFTime As Integer
        Dim tmpJunYunTime As Integer
        Dim tmpJunYunTime2 As Integer
        Dim tmpJunYunTime3 As Integer
        Dim nOcupy As Integer
        Dim IFaFa As Integer
        Dim IDaoDao As Integer
        Dim nAfTrain As Integer
        Dim sAfJGtime As Integer
        Select Case sCalRule
            Case "富余时间放在折返线上"
                For k = 1 To UBound(ChediInfo)
                    If UBound(ChediInfo(k).nLinkTrain) > 1 Then
                        For i = 2 To UBound(ChediInfo(k).nLinkTrain)
                            nFirTrain = ChediInfo(k).nLinkTrain(i - 1)
                            nSecTrain = ChediInfo(k).nLinkTrain(i)
                            ' If nFirTrain = 253 Then Stop
                            nCurSta = TrainInf(nSecTrain).nPathID(1)
                            nMoveTime1 = GetReturnRunTime(TrainInf(nFirTrain).SCheDiLeiXing, StationInf(nCurSta).sStationName, 1)
                            nMovetime2 = GetReturnRunTime(TrainInf(nFirTrain).SCheDiLeiXing, StationInf(nCurSta).sStationName, 2)
                            IDaoDao = GetReturnRunTime(TrainInf(nFirTrain).SCheDiLeiXing, StationInf(nCurSta).sStationName, 3)
                            IFaFa = GetReturnRunTime(TrainInf(nFirTrain).SCheDiLeiXing, StationInf(nCurSta).sStationName, 4)
                            ' If nFirTrain = 45 Then Stop
                            If TrainInf(nFirTrain).TrainReturnStyle(2) = "站后折返" Then
                                nStoptime = GetCurTrainStopTimeAtStation(TrainInf(nFirTrain).sJiaoLuName, TrainInf(nFirTrain).sStopSclaeName, TrainInf(nFirTrain).NextStation)
                                nStoptime2 = GetCurTrainStopTimeAtStation(TrainInf(nSecTrain).sJiaoLuName, TrainInf(nSecTrain).sStopSclaeName, TrainInf(nSecTrain).ComeStation)
                                nMinZftime = CDZGetZheFanTime(nSecTrain, TrainInf(nSecTrain).SCheDiLeiXing, TrainInf(nSecTrain).ComeStation, TrainInf(nSecTrain).TrainReturnStyle(1))
                                nMinAtZFLineTime = nMinZftime - nStoptime - nStoptime2 - nMoveTime1 - nMovetime2

                                If nMinAtZFLineTime < 0 Then
                                    Call AddTrainErrorInf(nSecTrain, TrainInf(nSecTrain).nPathID(1), TrainInf(nSecTrain).Starting(TrainInf(nSecTrain).nPathID(1)), "列车" & TrainInf(nFirTrain).Train & "与列车" & TrainInf(nSecTrain).Train & "最小折返时间给定错误，在折返股道的停留时间小于零!")
                                    nMinAtZFLineTime = 0
                                End If
                                nActZFTime = TimeMinus(TrainInf(nSecTrain).Starting(nCurSta), TrainInf(nFirTrain).Arrival(nCurSta)) - nStoptime - nStoptime2 - nMoveTime1 - nMovetime2

                                '到达停站
                                'Call RecordStaTime(nFirTrain, nCurSta, TrainInf(nFirTrain).Starting(nCurSta), TrainInf(nFirTrain).Arrival(nCurSta))
                                leftStartTime = TimeAdd(TrainInf(nFirTrain).Starting(nCurSta), nMoveTime1)
                                'TrainInf(nSecTrain).Arrival(nCurSta) = TimeMinus(TrainInf(nSecTrain).Starting(nCurSta), sAfJGtime - IFaFa)
                                RightArriTime = TimeMinus(TrainInf(nSecTrain).Arrival(nCurSta), nMovetime2)

                                If TrainInf(nFirTrain).sEndZFLine = "" Or TrainInf(nFirTrain).sEndZFLine = "无" Then
                                    sCurZheFanLine = SeekZheFanGuDao(nCurSta, TimeMinus(leftStartTime, IDaoDao), TimeAdd(RightArriTime, IFaFa), nFirTrain)
                                Else
                                    If TrainInf(nFirTrain).StopLine(TrainInf(nFirTrain).nPathID(UBound(TrainInf(nFirTrain).nPathID))) = TrainInf(nSecTrain).StopLine(TrainInf(nSecTrain).nPathID(1)) Then
                                        sCurZheFanLine = TrainInf(nSecTrain).StopLine(TrainInf(nSecTrain).nPathID(1))
                                    Else
                                        sCurZheFanLine = TrainInf(nFirTrain).sEndZFLine
                                    End If
                                End If
                            ElseIf TrainInf(nFirTrain).TrainReturnStyle(2) = "站前折返" Or TrainInf(nFirTrain).TrainReturnStyle(2) = "立即折返" Then
                                leftStartTime = TrainInf(nFirTrain).Arrival(nCurSta)
                                RightArriTime = TrainInf(nSecTrain).Starting(nCurSta)
                                If TrainInf(nFirTrain).sEndZFLine = "" Or TrainInf(nFirTrain).sEndZFLine = "无" Then
                                    sCurZheFanLine = SeekZhanQianZheFanGuDao(TrainInf(nFirTrain).sJiaoLuName, nCurSta, leftStartTime, RightArriTime, nFirTrain, nSecTrain)
                                Else
                                    nOcupy = CurGudaoIfOcupy(nCurSta, TrainInf(nFirTrain).sEndZFLine, leftStartTime, RightArriTime, nFirTrain, nSecTrain)
                                    If nOcupy = 1 Then
                                        sCurZheFanLine = TrainInf(nFirTrain).sEndZFLine
                                    Else
                                        sCurZheFanLine = SeekZhanQianZheFanGuDao(TrainInf(nFirTrain).sJiaoLuName, nCurSta, leftStartTime, RightArriTime, nFirTrain, nSecTrain)
                                    End If
                                End If

                            Else
                                If TrainInf(nFirTrain).StopLine(nCurSta) = TrainInf(nSecTrain).StopLine(nCurSta) Then
                                    sCurZheFanLine = TrainInf(nFirTrain).StopLine(nCurSta)
                                Else
                                    Call AddTrainErrorInf(nFirTrain, nCurSta, leftStartTime, "列车" & TrainInf(nFirTrain).Train & "与列车" & TrainInf(nSecTrain).Train & "的折返股道不一致，在该车站为站前折返方式")
                                End If
                            End If
                            If sCurZheFanLine <> "" Then

                                If TrainInf(nFirTrain).TrainReturnStyle(2) = "站后折返" Then
                                    If AddLitterTime(leftStartTime) > AddLitterTime(RightArriTime) Then
                                        Call AddTrainErrorInf(nFirTrain, nCurSta, leftStartTime, "列车" & TrainInf(nFirTrain).Train & "与列车" & TrainInf(nSecTrain).Train & "在该站折返时到达时刻晚于出发时刻，请修改折返方式或修改列车到发时刻!")
                                    Else
                                        TrainInf(nFirTrain).sEndZFStarting = RightArriTime
                                        TrainInf(nFirTrain).sEndZFArrival = leftStartTime
                                        TrainInf(nSecTrain).sStartZFArrival = leftStartTime
                                        TrainInf(nSecTrain).sStartZFStarting = RightArriTime
                                        TrainInf(nFirTrain).sEndZFLine = sCurZheFanLine
                                        TrainInf(nSecTrain).sStartZFLine = sCurZheFanLine

                                    End If
                                ElseIf TrainInf(nFirTrain).TrainReturnStyle(2) = "站前折返" Or TrainInf(nFirTrain).TrainReturnStyle(2) = "立即折返" Then
                                    leftStartTime = TrainInf(nFirTrain).Starting(nCurSta)
                                    RightArriTime = TrainInf(nSecTrain).Arrival(nCurSta)
                                    If AddLitterTime(leftStartTime) > AddLitterTime(RightArriTime) Then
                                        Call AddTrainErrorInf(nFirTrain, nCurSta, leftStartTime, "列车" & TrainInf(nFirTrain).Train & "与列车" & TrainInf(nSecTrain).Train & "在该站折返时到达时刻晚于出发时刻，请修改折返方式或修改列车到发时刻!")
                                    Else
                                        Call EditCurTrainOcupyStaGuDao(nFirTrain, sCurZheFanLine, nCurSta, "折返股道")
                                        TrainInf(nFirTrain).sEndZFStarting = TrainInf(nSecTrain).Arrival(nCurSta)
                                        TrainInf(nFirTrain).sEndZFArrival = TrainInf(nFirTrain).Starting(nCurSta)
                                        TrainInf(nSecTrain).sStartZFArrival = TrainInf(nFirTrain).Starting(nCurSta)
                                        TrainInf(nSecTrain).sStartZFStarting = TrainInf(nSecTrain).Arrival(nCurSta)
                                    End If
                                End If
                            Else
                                Call AddTrainErrorInf(nFirTrain, nCurSta, leftStartTime, "列车" & TrainInf(nFirTrain).Train & "与列车" & TrainInf(nSecTrain).Train & " 折返时折返股道数量不够，请修改折返方式或修改列车到发时刻!")
                            End If
                        Next i
                    End If
                Next k

            Case "富余时间放在到发线上"
                For k = 1 To UBound(ChediInfo)
                    If UBound(ChediInfo(k).nLinkTrain) > 1 Then
                        For i = 2 To UBound(ChediInfo(k).nLinkTrain)
                            nFirTrain = ChediInfo(k).nLinkTrain(i - 1)
                            nSecTrain = ChediInfo(k).nLinkTrain(i)
                            'If nFirTrain = 6 Then Stop
                            nCurSta = TrainInf(nSecTrain).nPathID(1)
                            nMoveTime1 = GetReturnRunTime(TrainInf(nFirTrain).SCheDiLeiXing, StationInf(nCurSta).sStationName, 1)
                            nMovetime2 = GetReturnRunTime(TrainInf(nFirTrain).SCheDiLeiXing, StationInf(nCurSta).sStationName, 2)
                            IDaoDao = GetReturnRunTime(TrainInf(nFirTrain).SCheDiLeiXing, StationInf(nCurSta).sStationName, 3)
                            IFaFa = GetReturnRunTime(TrainInf(nFirTrain).SCheDiLeiXing, StationInf(nCurSta).sStationName, 4)
                            ' If nFirTrain = 45 Then Stop
                            If TrainInf(nFirTrain).TrainReturnStyle(2) = "站后折返" Then

                                nStoptime = GetCurTrainStopTimeAtStation(TrainInf(nFirTrain).sJiaoLuName, TrainInf(nFirTrain).sStopSclaeName, TrainInf(nFirTrain).NextStation)
                                nStoptime2 = GetCurTrainStopTimeAtStation(TrainInf(nSecTrain).sJiaoLuName, TrainInf(nSecTrain).sStopSclaeName, TrainInf(nSecTrain).ComeStation)

                                nMinZftime = CDZGetZheFanTime(nSecTrain, TrainInf(nSecTrain).SCheDiLeiXing, TrainInf(nSecTrain).ComeStation, TrainInf(nSecTrain).TrainReturnStyle(1))
                                nMinAtZFLineTime = nMinZftime - nStoptime - nStoptime2 - nMoveTime1 - nMovetime2

                                If nMinAtZFLineTime < 0 Then
                                    Call AddTrainErrorInf(nSecTrain, TrainInf(nSecTrain).nPathID(1), TrainInf(nSecTrain).Starting(TrainInf(nSecTrain).nPathID(1)), "列车" & TrainInf(nFirTrain).Train & "与列车" & TrainInf(nSecTrain).Train & "最小折返时间给定错误，在折返股道的停留时间小于零!")
                                    nMinAtZFLineTime = 0
                                End If
                                nActZFTime = TimeMinus(TrainInf(nSecTrain).Starting(nCurSta), TrainInf(nFirTrain).Arrival(nCurSta))

                                tmpJunYunTime = (nActZFTime - nMinZftime) / 2
                                tmpJunYunTime2 = nActZFTime - nMinZftime - tmpJunYunTime
                                If tmpJunYunTime < 0 Then
                                    tmpJunYunTime = 0
                                End If
                                If tmpJunYunTime2 < 0 Then
                                    tmpJunYunTime2 = 0
                                End If
                                nAfTrain = nFindArriAfterTrain(nFirTrain, nCurSta, SectionInf(TrainInf(nFirTrain).nPassSection(UBound(TrainInf(nFirTrain).nPassSection))).sSecName)
                                If nAfTrain <> 0 Then
                                    sAfJGtime = TimeMinus(TrainInf(nAfTrain).Arrival(nCurSta), TrainInf(nFirTrain).Arrival(nCurSta))
                                Else
                                    sAfJGtime = 24 * 3600 + 1
                                End If

                                '到达停站
                                If nStoptime + tmpJunYunTime + IDaoDao > sAfJGtime Then
                                    TrainInf(nFirTrain).Starting(nCurSta) = TimeAdd(TrainInf(nFirTrain).Arrival(nCurSta), sAfJGtime - IDaoDao)
                                    Call RecordStaTime(nFirTrain, nCurSta, TrainInf(nFirTrain).Starting(nCurSta), TrainInf(nFirTrain).Arrival(nCurSta))
                                    leftStartTime = TimeAdd(TrainInf(nFirTrain).Starting(nCurSta), nMoveTime1)
                                    nMinAtZFLineTime = nMinAtZFLineTime + (nStoptime + tmpJunYunTime + IDaoDao) - sAfJGtime
                                Else
                                    TrainInf(nFirTrain).Starting(nCurSta) = TimeAdd(TrainInf(nFirTrain).Starting(nCurSta), tmpJunYunTime)
                                    Call RecordStaTime(nFirTrain, nCurSta, TrainInf(nFirTrain).Starting(nCurSta), TrainInf(nFirTrain).Arrival(nCurSta))
                                    leftStartTime = TimeAdd(TrainInf(nFirTrain).Starting(nCurSta), nMoveTime1)
                                End If


                                nAfTrain = nFindBeforeTrain(nSecTrain, nCurSta, SectionInf(TrainInf(nSecTrain).nPassSection(1)).sSecName)

                                If nAfTrain <> 0 Then
                                    sAfJGtime = TimeMinus(TrainInf(nSecTrain).Starting(nCurSta), TrainInf(nAfTrain).Starting(nCurSta))
                                Else
                                    sAfJGtime = 24 * 3600 + 1
                                End If

                                '出发停站
                                If nStoptime2 + tmpJunYunTime2 + IFaFa > sAfJGtime Then
                                    TrainInf(nSecTrain).Arrival(nCurSta) = TimeMinus(TrainInf(nSecTrain).Starting(nCurSta), sAfJGtime - IFaFa)
                                    Call RecordStaTime(nSecTrain, nCurSta, TrainInf(nSecTrain).Starting(nCurSta), TrainInf(nSecTrain).Arrival(nCurSta))
                                    RightArriTime = TimeMinus(TrainInf(nSecTrain).Arrival(nCurSta), nMovetime2)
                                    nMinAtZFLineTime = nMinAtZFLineTime + (nStoptime2 + tmpJunYunTime2 + IFaFa) - sAfJGtime
                                Else
                                    TrainInf(nSecTrain).Arrival(nCurSta) = TimeMinus(TrainInf(nSecTrain).Arrival(nCurSta), tmpJunYunTime2)
                                    Call RecordStaTime(nSecTrain, nCurSta, TrainInf(nSecTrain).Starting(nCurSta), TrainInf(nSecTrain).Arrival(nCurSta))
                                    RightArriTime = TimeMinus(TrainInf(nSecTrain).Arrival(nCurSta), nMovetime2)
                                End If

                                If TrainInf(nFirTrain).sEndZFLine = "" Or TrainInf(nFirTrain).sEndZFLine = "无" Then
                                    sCurZheFanLine = SeekZheFanGuDao(nCurSta, TimeMinus(leftStartTime, IDaoDao), TimeAdd(RightArriTime, IFaFa), nFirTrain)
                                Else
                                    If TrainInf(nFirTrain).StopLine(TrainInf(nFirTrain).nPathID(UBound(TrainInf(nFirTrain).nPathID))) = TrainInf(nSecTrain).StopLine(TrainInf(nSecTrain).nPathID(1)) Then
                                        sCurZheFanLine = TrainInf(nSecTrain).StopLine(TrainInf(nSecTrain).nPathID(1))
                                    Else
                                        sCurZheFanLine = TrainInf(nFirTrain).sEndZFLine
                                    End If
                                End If
                            ElseIf TrainInf(nFirTrain).TrainReturnStyle(2) = "站前折返" Or TrainInf(nFirTrain).TrainReturnStyle(2) = "立即折返" Then
                                leftStartTime = TrainInf(nFirTrain).Arrival(nCurSta)
                                RightArriTime = TrainInf(nSecTrain).Starting(nCurSta)
                                If TrainInf(nFirTrain).sEndZFLine = "" Or TrainInf(nFirTrain).sEndZFLine = "无" Then
                                    sCurZheFanLine = SeekZhanQianZheFanGuDao(TrainInf(nFirTrain).sJiaoLuName, nCurSta, leftStartTime, RightArriTime, nFirTrain, nSecTrain)
                                Else
                                    nOcupy = CurGudaoIfOcupy(nCurSta, TrainInf(nFirTrain).sEndZFLine, leftStartTime, RightArriTime, nFirTrain, nSecTrain)
                                    If nOcupy = 1 Then
                                        sCurZheFanLine = TrainInf(nFirTrain).sEndZFLine
                                    Else
                                        sCurZheFanLine = SeekZhanQianZheFanGuDao(TrainInf(nFirTrain).sJiaoLuName, nCurSta, leftStartTime, RightArriTime, nFirTrain, nSecTrain)
                                    End If
                                End If

                            Else
                                If TrainInf(nFirTrain).StopLine(nCurSta) = TrainInf(nSecTrain).StopLine(nCurSta) Then
                                    sCurZheFanLine = TrainInf(nFirTrain).StopLine(nCurSta)
                                Else
                                    Call AddTrainErrorInf(nFirTrain, nCurSta, leftStartTime, "列车" & TrainInf(nFirTrain).Train & "与列车" & TrainInf(nSecTrain).Train & "的折返股道不一致，在该车站为站前折返方式")
                                End If
                            End If
                            If sCurZheFanLine <> "" Then
                                If AddLitterTime(leftStartTime) > AddLitterTime(RightArriTime) Then
                                    Call AddTrainErrorInf(nFirTrain, nCurSta, leftStartTime, "列车" & TrainInf(nFirTrain).Train & "与列车" & TrainInf(nSecTrain).Train & "在该站折返时到达时刻晚于出发时刻，请修改折返方式或修改列车到发时刻!")
                                End If

                                If TrainInf(nFirTrain).TrainReturnStyle(2) = "站后折返" Then
                                    TrainInf(nFirTrain).sEndZFStarting = RightArriTime
                                    TrainInf(nFirTrain).sEndZFArrival = leftStartTime
                                    TrainInf(nSecTrain).sStartZFArrival = leftStartTime
                                    TrainInf(nSecTrain).sStartZFStarting = RightArriTime
                                    TrainInf(nFirTrain).sEndZFLine = sCurZheFanLine
                                    TrainInf(nSecTrain).sStartZFLine = sCurZheFanLine
                                ElseIf TrainInf(nFirTrain).TrainReturnStyle(2) = "站前折返" Or TrainInf(nFirTrain).TrainReturnStyle(2) = "立即折返" Then
                                    Call EditCurTrainOcupyStaGuDao(nFirTrain, sCurZheFanLine, nCurSta, "折返股道")
                                    TrainInf(nFirTrain).sEndZFStarting = TrainInf(nSecTrain).Arrival(nCurSta)
                                    TrainInf(nFirTrain).sEndZFArrival = TrainInf(nFirTrain).Starting(nCurSta)
                                    TrainInf(nSecTrain).sStartZFArrival = TrainInf(nFirTrain).Starting(nCurSta)
                                    TrainInf(nSecTrain).sStartZFStarting = TrainInf(nSecTrain).Arrival(nCurSta)
                                End If
                            Else
                                Call AddTrainErrorInf(nFirTrain, nCurSta, leftStartTime, "列车" & TrainInf(nFirTrain).Train & "与列车" & TrainInf(nSecTrain).Train & " 折返时折返股道数量不够，请修改折返方式或修改列车到发时刻!")
                            End If
                        Next i
                    End If
                Next k

            Case "富余时间平均分配到在到发线与折返线上"
                For k = 1 To UBound(ChediInfo)
                    If UBound(ChediInfo(k).nLinkTrain) > 1 Then
                        For i = 2 To UBound(ChediInfo(k).nLinkTrain)
                            nFirTrain = ChediInfo(k).nLinkTrain(i - 1)
                            nSecTrain = ChediInfo(k).nLinkTrain(i)
                            'If nFirTrain = 6 Then Stop
                            nCurSta = TrainInf(nSecTrain).nPathID(1)
                            nMoveTime1 = GetReturnRunTime(TrainInf(nFirTrain).SCheDiLeiXing, StationInf(nCurSta).sStationName, 1)
                            nMovetime2 = GetReturnRunTime(TrainInf(nFirTrain).SCheDiLeiXing, StationInf(nCurSta).sStationName, 2)
                            IDaoDao = GetReturnRunTime(TrainInf(nFirTrain).SCheDiLeiXing, StationInf(nCurSta).sStationName, 3)
                            IFaFa = GetReturnRunTime(TrainInf(nFirTrain).SCheDiLeiXing, StationInf(nCurSta).sStationName, 4)
                            ' If nFirTrain = 45 Then Stop
                            If TrainInf(nFirTrain).TrainReturnStyle(2) = "站后折返" Then

                                nStoptime = GetCurTrainStopTimeAtStation(TrainInf(nFirTrain).sJiaoLuName, TrainInf(nFirTrain).sStopSclaeName, TrainInf(nFirTrain).NextStation)
                                nStoptime2 = GetCurTrainStopTimeAtStation(TrainInf(nSecTrain).sJiaoLuName, TrainInf(nSecTrain).sStopSclaeName, TrainInf(nSecTrain).ComeStation)

                                nMinZftime = CDZGetZheFanTime(nSecTrain, TrainInf(nSecTrain).SCheDiLeiXing, TrainInf(nSecTrain).ComeStation, TrainInf(nSecTrain).TrainReturnStyle(1))
                                nMinAtZFLineTime = nMinZftime - nStoptime - nStoptime2 - nMoveTime1 - nMovetime2

                                If nMinAtZFLineTime < 0 Then
                                    Call AddTrainErrorInf(nSecTrain, TrainInf(nSecTrain).nPathID(1), TrainInf(nSecTrain).Starting(TrainInf(nSecTrain).nPathID(1)), "列车" & TrainInf(nFirTrain).Train & "与列车" & TrainInf(nSecTrain).Train & "最小折返时间给定错误，在折返股道的停留时间小于零!")
                                    nMinAtZFLineTime = 0
                                End If
                                nActZFTime = TimeMinus(TrainInf(nSecTrain).Starting(nCurSta), TrainInf(nFirTrain).Arrival(nCurSta))

                                tmpJunYunTime = (nActZFTime - nMinZftime) / 3
                                tmpJunYunTime2 = (nActZFTime - nMinZftime) / 3
                                tmpJunYunTime3 = nActZFTime - nMinZftime - tmpJunYunTime - tmpJunYunTime2
                                If tmpJunYunTime < 0 Then
                                    tmpJunYunTime = 0
                                End If
                                If tmpJunYunTime2 < 0 Then
                                    tmpJunYunTime2 = 0
                                End If
                                If tmpJunYunTime3 < 0 Then
                                    tmpJunYunTime3 = 0
                                End If
                                nAfTrain = nFindArriAfterTrain(nFirTrain, nCurSta, SectionInf(TrainInf(nFirTrain).nPassSection(UBound(TrainInf(nFirTrain).nPassSection))).sSecName)
                                If nAfTrain <> 0 Then
                                    sAfJGtime = TimeMinus(TrainInf(nAfTrain).Arrival(nCurSta), TrainInf(nFirTrain).Arrival(nCurSta))
                                Else
                                    sAfJGtime = 24 * 3600 + 1
                                End If

                                '到达停站
                                If nStoptime + tmpJunYunTime + IDaoDao > sAfJGtime Then
                                    TrainInf(nFirTrain).Starting(nCurSta) = TimeAdd(TrainInf(nFirTrain).Arrival(nCurSta), sAfJGtime - IDaoDao)
                                    Call RecordStaTime(nFirTrain, nCurSta, TrainInf(nFirTrain).Starting(nCurSta), TrainInf(nFirTrain).Arrival(nCurSta))
                                    leftStartTime = TimeAdd(TrainInf(nFirTrain).Starting(nCurSta), nMoveTime1)
                                    nMinAtZFLineTime = nMinAtZFLineTime + (nStoptime + tmpJunYunTime + IDaoDao) - sAfJGtime
                                Else
                                    TrainInf(nFirTrain).Starting(nCurSta) = TimeAdd(TrainInf(nFirTrain).Starting(nCurSta), tmpJunYunTime)
                                    Call RecordStaTime(nFirTrain, nCurSta, TrainInf(nFirTrain).Starting(nCurSta), TrainInf(nFirTrain).Arrival(nCurSta))
                                    leftStartTime = TimeAdd(TrainInf(nFirTrain).Starting(nCurSta), nMoveTime1)
                                End If


                                nAfTrain = nFindBeforeTrain(nSecTrain, nCurSta, SectionInf(TrainInf(nSecTrain).nPassSection(1)).sSecName)

                                If nAfTrain <> 0 Then
                                    sAfJGtime = TimeMinus(TrainInf(nSecTrain).Starting(nCurSta), TrainInf(nAfTrain).Starting(nCurSta))
                                Else
                                    sAfJGtime = 24 * 3600 + 1
                                End If

                                '出发停站
                                If nStoptime2 + tmpJunYunTime2 + IFaFa > sAfJGtime Then
                                    TrainInf(nSecTrain).Arrival(nCurSta) = TimeMinus(TrainInf(nSecTrain).Starting(nCurSta), sAfJGtime - IFaFa)
                                    Call RecordStaTime(nSecTrain, nCurSta, TrainInf(nSecTrain).Starting(nCurSta), TrainInf(nSecTrain).Arrival(nCurSta))
                                    RightArriTime = TimeMinus(TrainInf(nSecTrain).Arrival(nCurSta), nMovetime2)
                                    nMinAtZFLineTime = nMinAtZFLineTime + (nStoptime2 + tmpJunYunTime2 + IFaFa) - sAfJGtime
                                Else
                                    TrainInf(nSecTrain).Arrival(nCurSta) = TimeMinus(TrainInf(nSecTrain).Arrival(nCurSta), tmpJunYunTime2)
                                    Call RecordStaTime(nSecTrain, nCurSta, TrainInf(nSecTrain).Starting(nCurSta), TrainInf(nSecTrain).Arrival(nCurSta))
                                    RightArriTime = TimeMinus(TrainInf(nSecTrain).Arrival(nCurSta), nMovetime2)
                                End If

                                If TrainInf(nFirTrain).sEndZFLine = "" Or TrainInf(nFirTrain).sEndZFLine = "无" Then
                                    sCurZheFanLine = SeekZheFanGuDao(nCurSta, TimeMinus(leftStartTime, IDaoDao), TimeAdd(RightArriTime, IFaFa), nFirTrain)
                                Else
                                    If TrainInf(nFirTrain).StopLine(TrainInf(nFirTrain).nPathID(UBound(TrainInf(nFirTrain).nPathID))) = TrainInf(nSecTrain).StopLine(TrainInf(nSecTrain).nPathID(1)) Then
                                        sCurZheFanLine = TrainInf(nSecTrain).StopLine(TrainInf(nSecTrain).nPathID(1))
                                    Else
                                        sCurZheFanLine = TrainInf(nFirTrain).sEndZFLine
                                    End If
                                End If
                            ElseIf TrainInf(nFirTrain).TrainReturnStyle(2) = "站前折返" Or TrainInf(nFirTrain).TrainReturnStyle(2) = "立即折返" Then
                                leftStartTime = TrainInf(nFirTrain).Arrival(nCurSta)
                                RightArriTime = TrainInf(nSecTrain).Starting(nCurSta)
                                If TrainInf(nFirTrain).sEndZFLine = "" Or TrainInf(nFirTrain).sEndZFLine = "无" Then
                                    sCurZheFanLine = SeekZhanQianZheFanGuDao(TrainInf(nFirTrain).sJiaoLuName, nCurSta, leftStartTime, RightArriTime, nFirTrain, nSecTrain)
                                Else
                                    nOcupy = CurGudaoIfOcupy(nCurSta, TrainInf(nFirTrain).sEndZFLine, leftStartTime, RightArriTime, nFirTrain, nSecTrain)
                                    If nOcupy = 1 Then
                                        sCurZheFanLine = TrainInf(nFirTrain).sEndZFLine
                                    Else
                                        sCurZheFanLine = SeekZhanQianZheFanGuDao(TrainInf(nFirTrain).sJiaoLuName, nCurSta, leftStartTime, RightArriTime, nFirTrain, nSecTrain)
                                    End If
                                End If

                            Else
                                If TrainInf(nFirTrain).StopLine(nCurSta) = TrainInf(nSecTrain).StopLine(nCurSta) Then
                                    sCurZheFanLine = TrainInf(nFirTrain).StopLine(nCurSta)
                                Else
                                    Call AddTrainErrorInf(nFirTrain, nCurSta, leftStartTime, "列车" & TrainInf(nFirTrain).Train & "与列车" & TrainInf(nSecTrain).Train & "的折返股道不一致，在该车站为站前折返方式")
                                End If
                            End If
                            If sCurZheFanLine <> "" Then
                                If AddLitterTime(leftStartTime) > AddLitterTime(RightArriTime) Then
                                    Call AddTrainErrorInf(nFirTrain, nCurSta, leftStartTime, "列车" & TrainInf(nFirTrain).Train & "与列车" & TrainInf(nSecTrain).Train & "在该站折返时到达时刻晚于出发时刻，请修改折返方式或修改列车到发时刻!")
                                End If

                                If TrainInf(nFirTrain).TrainReturnStyle(2) = "站后折返" Then
                                    TrainInf(nFirTrain).sEndZFStarting = RightArriTime
                                    TrainInf(nFirTrain).sEndZFArrival = leftStartTime
                                    TrainInf(nSecTrain).sStartZFArrival = leftStartTime
                                    TrainInf(nSecTrain).sStartZFStarting = RightArriTime
                                    TrainInf(nFirTrain).sEndZFLine = sCurZheFanLine
                                    TrainInf(nSecTrain).sStartZFLine = sCurZheFanLine
                                ElseIf TrainInf(nFirTrain).TrainReturnStyle(2) = "站前折返" Or TrainInf(nFirTrain).TrainReturnStyle(2) = "立即折返" Then
                                    Call EditCurTrainOcupyStaGuDao(nFirTrain, sCurZheFanLine, nCurSta, "折返股道")
                                    TrainInf(nFirTrain).sEndZFStarting = TrainInf(nSecTrain).Arrival(nCurSta)
                                    TrainInf(nFirTrain).sEndZFArrival = TrainInf(nFirTrain).Starting(nCurSta)
                                    TrainInf(nSecTrain).sStartZFArrival = TrainInf(nFirTrain).Starting(nCurSta)
                                    TrainInf(nSecTrain).sStartZFStarting = TrainInf(nSecTrain).Arrival(nCurSta)
                                End If
                            Else
                                Call AddTrainErrorInf(nFirTrain, nCurSta, leftStartTime, "列车" & TrainInf(nFirTrain).Train & "与列车" & TrainInf(nSecTrain).Train & " 折返时折返股道数量不够，请修改折返方式或修改列车到发时刻!")
                            End If
                        Next i
                    End If
                Next k
        End Select
    End Sub

    '编辑车站折返股道信息
    Public Sub ResetCurZFTrainZFtime(ByVal nFirTrain As Integer, ByVal nSecTrain As Integer, ByVal nCurSta As Integer)
        Dim sCurZheFanLine As String
        sCurZheFanLine = ""
        Dim nMoveTime1 As Integer
        Dim nMovetime2 As Integer
        Dim nMinAtZFLineTime As Integer
        Dim nStoptime As Integer
        Dim nStoptime2 As Integer
        Dim nMinZftime As Integer
        Dim nActZFTime As Integer
        Dim nZFArriTime As Integer
        Dim nZFStartTime As Integer

        nMoveTime1 = GetReturnRunTime(TrainInf(nFirTrain).SCheDiLeiXing, StationInf(nCurSta).sStationName, 1)
        nMovetime2 = GetReturnRunTime(TrainInf(nFirTrain).SCheDiLeiXing, StationInf(nCurSta).sStationName, 2)
        If TrainInf(nFirTrain).StopLine(nCurSta) = TrainInf(nFirTrain).sEndZFLine And TrainInf(nSecTrain).StopLine(nCurSta) = TrainInf(nFirTrain).sEndZFLine Then '同股道折返
            nMoveTime1 = 0
            nMovetime2 = 0
        ElseIf TrainInf(nFirTrain).sEndZFLine <> TrainInf(nFirTrain).sStartZFLine Then
            If TrainInf(nFirTrain).sEndZFLine = TrainInf(nFirTrain).StopLine(nCurSta) Then '终到停站股道与折返股道相同
                nMoveTime1 = 0
            ElseIf TrainInf(nSecTrain).sStartZFLine = TrainInf(nSecTrain).StopLine(nCurSta) Then '始发停站股道与折返股道相同
                nMovetime2 = 0
            Else '正常站后折返

            End If

        End If

        nStoptime = GetCurTrainStopTimeAtStation(TrainInf(nFirTrain).sJiaoLuName, TrainInf(nFirTrain).sStopSclaeName, TrainInf(nFirTrain).NextStation)
        nStoptime2 = GetCurTrainStopTimeAtStation(TrainInf(nSecTrain).sJiaoLuName, TrainInf(nSecTrain).sStopSclaeName, TrainInf(nSecTrain).ComeStation)
        nMinZftime = CDZGetZheFanTime(nSecTrain, TrainInf(nSecTrain).SCheDiLeiXing, TrainInf(nSecTrain).ComeStation, TrainInf(nSecTrain).TrainReturnStyle(1))
        nMinAtZFLineTime = nMinZftime - nStoptime - nStoptime2 - nMoveTime1 - nMovetime2

        If nMinAtZFLineTime < 0 Then
            Call AddTrainErrorInf(nSecTrain, TrainInf(nSecTrain).nPathID(1), TrainInf(nSecTrain).Starting(TrainInf(nSecTrain).nPathID(1)), "列车" & TrainInf(nFirTrain).Train & "与列车" & TrainInf(nSecTrain).Train & "最小折返时间给定错误，在折返股道的停留时间小于零!")
            nMinAtZFLineTime = 0
        Else
            nActZFTime = TimeMinus(TrainInf(nSecTrain).Starting(nCurSta), TrainInf(nFirTrain).Arrival(nCurSta))
            If nActZFTime > nStoptime + nStoptime2 + nMoveTime1 + nMovetime2 Then
                nZFArriTime = TimeAdd(TrainInf(nFirTrain).Arrival(nCurSta), nStoptime + nMoveTime1)
                nZFStartTime = TimeMinus(TrainInf(nSecTrain).Starting(nCurSta), (nStoptime2 + nMovetime2))
                TrainInf(nFirTrain).sEndZFArrival = nZFArriTime
                TrainInf(nFirTrain).sEndZFStarting = nZFStartTime
                TrainInf(nSecTrain).sStartZFArrival = nZFArriTime
                TrainInf(nSecTrain).sStartZFStarting = nZFStartTime

                Call RecordStaTime(nFirTrain, nCurSta, TimeAdd(TrainInf(nFirTrain).Arrival(nCurSta), nStoptime), TrainInf(nFirTrain).Arrival(nCurSta))
                Call RecordStaTime(nSecTrain, nCurSta, TrainInf(nSecTrain).Starting(nCurSta), TimeMinus(TrainInf(nSecTrain).Starting(nCurSta), nStoptime2))
            Else
                nZFArriTime = TimeAdd(TrainInf(nFirTrain).Arrival(nCurSta), nStoptime)
                nZFStartTime = TimeMinus(TrainInf(nSecTrain).Starting(nCurSta), nStoptime2)
                TrainInf(nFirTrain).sEndZFArrival = nZFArriTime
                TrainInf(nFirTrain).sEndZFStarting = nZFStartTime
                TrainInf(nSecTrain).sStartZFArrival = nZFArriTime
                TrainInf(nSecTrain).sStartZFStarting = nZFStartTime
            End If
        End If


    End Sub
    '添加一条出错信息
    Public Sub AddTrainErrorInf(ByVal nTrain As Integer, ByVal nSta As Integer, ByVal lCurTime As Long, ByVal sEriorInf As String)
        ReDim Preserve TrainErrInf(UBound(TrainErrInf) + 1)
        TrainErrInf(UBound(TrainErrInf)).nTrain = nTrain
        TrainErrInf(UBound(TrainErrInf)).Scurtime = dTime(lCurTime, 0)
        TrainErrInf(UBound(TrainErrInf)).nErrorSta = nSta
        TrainErrInf(UBound(TrainErrInf)).sErrorMessage = sEriorInf
    End Sub

    '检查当前股道是否被占用
    Public Function CurGudaoIfOcupy(ByVal nSta As Integer, ByVal sGuDaoNum As String, ByVal lTemp1 As Long, ByVal lTemp2 As Long, ByVal nCurTrain As Integer, ByVal nCurTrain1 As Integer) As Integer

        Dim i As Integer
        Dim tmpTrain() As Integer
        ReDim tmpTrain(0)
        Dim TimeCheck As Integer
        Dim Temp1 As Long, Temp2 As Long
        For i = 1 To UBound(TrainInf)
            '检查停站列车，确定当前时间是否落入停站列车的停车时间范围内
            If TrainInf(i).Train <> "" Then
                TimeCheck = 0
                If i <> nCurTrain And i <> nCurTrain1 Then
                    If TrainInf(i).StopLine(nSta) = sGuDaoNum Then
                        Temp1 = TrainInf(i).Arrival(nSta)
                        Temp2 = TrainInf(i).Starting(nSta)
                        If Temp1 <> -1 And Temp2 <> -1 Then
                            If TimeConflictCheck(lTemp1, lTemp2, Temp1, Temp2) = True Then
                                TimeCheck = 1
                            End If
                        End If
                    End If
                    If TrainInf(i).ComeStation = StationInf(nSta).sStationName Then
                        If TrainInf(i).sStartZFLine = sGuDaoNum Then
                            Temp1 = TrainInf(i).sStartZFArrival
                            Temp2 = TrainInf(i).sStartZFStarting
                            If Temp1 <> -1 And Temp2 <> -1 Then
                                If TimeConflictCheck(lTemp1, lTemp2, Temp1, Temp2) = True Then
                                    TimeCheck = 1
                                End If
                            End If
                        End If
                    End If

                    If TrainInf(i).NextStation = StationInf(nSta).sStationName Then
                        If TrainInf(i).sEndZFLine = sGuDaoNum Then
                            Temp1 = TrainInf(i).sEndZFArrival
                            Temp2 = TrainInf(i).sEndZFStarting
                            If Temp1 <> -1 And Temp2 <> -1 Then
                                If TimeConflictCheck(lTemp1, lTemp2, Temp1, Temp2) = True Then
                                    TimeCheck = 1
                                End If
                            End If
                        End If
                    End If

                End If

                Select Case TimeCheck
                    Case 1
                        '当前列车到达时刻落入已有列车停车范围
                        ReDim Preserve tmpTrain(UBound(tmpTrain) + 1)
                        tmpTrain(UBound(tmpTrain)) = i
                End Select
            End If
        Next i

        If UBound(tmpTrain) > 0 Then
            CurGudaoIfOcupy = tmpTrain(1)
        End If

    End Function


    '查找折返股道
    Public Function SeekZheFanGuDao(ByVal nStaNum As Integer, ByVal lTemp1 As Long, ByVal lTemp2 As Long, ByVal nTrain As Integer) As String
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        SeekZheFanGuDao = ""
        Dim KYGuDao() As String
        ReDim KYGuDao(0)

        For i = 1 To UBound(StationInf(nStaNum).sTrackUse)
            If StationInf(nStaNum).sTrackUse(i).sJiaoLuName = TrainInf(nTrain).sJiaoLuName Then
                ReDim Preserve KYGuDao(UBound(StationInf(nStaNum).sTrackUse(i).sReturnUse))
                KYGuDao = StationInf(nStaNum).sTrackUse(i).sReturnUse
            End If
        Next i

        Dim TimeCheck As Integer
        Dim lAtimetemp As Long
        Dim lStimetemp As Long

        TimeCheck = 0
        If UBound(KYGuDao) > 0 Then
            For k = 1 To UBound(KYGuDao)
                TimeCheck = 0
                For j = 1 To UBound(TrainInf)
                    'If j = 263 Then Stop
                    If j Mod 2 <> 0 Then '只找下行列车的占用
                        If TrainInf(j).sStartZFLine = KYGuDao(k) And TrainInf(j).ComeStation = StationInf(nStaNum).sStationName Then
                            lAtimetemp = TrainInf(j).sStartZFArrival
                            lStimetemp = TrainInf(j).sStartZFStarting
                            If lAtimetemp <> -1 And lStimetemp <> -1 Then
                                If TimeConflictCheck(lAtimetemp, lStimetemp, lTemp1, lTemp2) = True Then
                                    TimeCheck = 1
                                End If
                            End If
                        End If
                        If TrainInf(j).sEndZFLine = KYGuDao(k) And TrainInf(j).NextStation = StationInf(nStaNum).sStationName Then
                            lAtimetemp = TrainInf(j).sEndZFArrival
                            lStimetemp = TrainInf(j).sEndZFStarting
                            If lAtimetemp <> -1 And lStimetemp <> -1 Then
                                If TimeConflictCheck(lAtimetemp, lStimetemp, lTemp1, lTemp2) = True Then
                                    TimeCheck = 1
                                End If
                            End If
                        End If
                    End If

                    If TimeCheck = 1 Then
                        Exit For
                    End If
                Next j
                If TimeCheck = 0 Then
                    SeekZheFanGuDao = KYGuDao(k)
                    Exit For
                End If
            Next k
        Else
            Call AddTrainErrorInf(nTrain, nStaNum, lTemp1, "列车交路 " & TrainInf(nTrain).sJiaoLuName & " 在车站 " & StationInf(nStaNum).sStationName & " 的股道使用方案没有添加，请先添加!")
        End If
    End Function

    '查找站前折返时，折返股道
    Public Function SeekZhanQianZheFanGuDao(ByVal sJiaoLuName As String, ByVal nStaNum As Integer, ByVal lTemp1 As Long, ByVal lTemp2 As Long, ByVal nCurTrain As Integer, ByVal nCurTrain1 As Integer) As String
        Dim i As Integer
        'Dim j As Integer
        Dim k As Integer
        SeekZhanQianZheFanGuDao = ""
        Dim KYGuDao() As String
        ReDim KYGuDao(0)


        If UBound(KYGuDao) = 0 Then
            For i = 1 To UBound(StationInf(nStaNum).sStLineNo)
                If StationInf(nStaNum).sLineUse(i) = "折返线" Or StationInf(nStaNum).sLineUse(i) = "到发线" Or StationInf(nStaNum).sLineUse(i) = "正线线" Then
                    ReDim Preserve KYGuDao(UBound(KYGuDao) + 1)
                    KYGuDao(UBound(KYGuDao)) = StationInf(nStaNum).sStLineNo(i)
                End If
            Next i
        End If

        Dim TimeCheck As Integer
        TimeCheck = 0
        If UBound(KYGuDao) > 0 Then
            For k = 1 To UBound(KYGuDao)
                TimeCheck = 0
                TimeCheck = CurGudaoIfOcupy(nStaNum, KYGuDao(k), lTemp1, lTemp2, nCurTrain, nCurTrain1)
                If TimeCheck = 0 Then
                    SeekZhanQianZheFanGuDao = KYGuDao(k)
                    Exit For
                End If
            Next k
        End If
    End Function

    '检查是否满足折返要求     '所有车底交路是否能满足时间间隔要求
    Public Sub EditAllJiaoLuLine()
        Dim i As Integer
        For i = 1 To UBound(ChediInfo)
            Call EditJiaoLuLine(i)
        Next i
    End Sub

    '追踪间隔判断
    Public Sub CheckIntervalStartAndArrival()
        Dim i, j As Integer
        Dim BeforeTrainD, BeforeTrainF As Integer
        ' Dim nAfter As Integer
        Dim lTimeArrival, lArrivalStart As Integer
        Dim nStationNumber As Integer
        Dim nSdirection As Integer
        Dim nTrainNumber As Integer
        Dim lArrivalArrival As Integer
        Dim nSectionLeave As Integer
        Dim nSectionEnter As Integer
        Dim sErrorMessage As String
        Dim nActInterval As Integer
        Dim ii, jj As Integer
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                'If TrainInf(i).Train = "540" Then Stop
                nSdirection = nDirection(i)
                nTrainNumber = i
                For j = 1 To UBound(TrainInf(i).nPassSection)
                    If j = 1 Then
                        nSectionLeave = TrainInf(i).nPassSection(j)
                        nSectionEnter = nSectionLeave
                    Else
                        nSectionLeave = TrainInf(i).nPassSection(j - 1)
                        nSectionEnter = TrainInf(i).nPassSection(j)
                    End If
                    '到达间隔判断
                    nStationNumber = TrainInf(i).nSecondID(j)
                    lTimeArrival = TrainInf(i).Arrival(nStationNumber)
                    BeforeTrainD = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, "", "", "")
                    If BeforeTrainD <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainD).StartStation Then
                        lArrivalArrival = TimeMinus(lTimeArrival, TrainInf(BeforeTrainD).Arrival(nStationNumber))
                        ii = nSameDirectionArrival(BeforeTrainD, nTrainNumber, nStationNumber, 0, 0, nSectionLeave, lTimeArrival)
                        nActInterval = lIntervalTime(BeforeTrainD, nTrainNumber, _
                                                                        ii, 1, nSectionLeave, nStationNumber, nSectionEnter)
                        lArrivalArrival = nActInterval - lArrivalArrival
                        If lArrivalArrival > 0 Then
                            sErrorMessage = "到达间隔不满足，与标准时间相差" & lArrivalArrival & "秒"
                            Call AddTrainErrorInf(nTrainNumber, nStationNumber, lTimeArrival, sErrorMessage)
                        End If
                    End If

                    '出发间隔判断
                    nStationNumber = TrainInf(i).nFirstID(j)
                    lTimeArrival = TrainInf(i).Starting(nStationNumber)
                    BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                                    "", "", "") '出发条件
                    If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                        lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                        jj = nSameDirectionStart(BeforeTrainF, nTrainNumber, lTimeArrival, nStationNumber, 0, 1, nSectionEnter)
                        nActInterval = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                                                   jj, 2, nSectionLeave, nStationNumber, nSectionEnter)
                        lArrivalStart = nActInterval - lArrivalStart
                        If lArrivalStart > 0 Then
                            sErrorMessage = "出发间隔不满足，与标准时间相差" & lArrivalStart & "秒"
                            Call AddTrainErrorInf(nTrainNumber, nStationNumber, lTimeArrival, sErrorMessage)
                        End If
                    End If

                    '发到间隔判断
                    nStationNumber = TrainInf(i).nSecondID(j)
                    lTimeArrival = TrainInf(i).Arrival(nStationNumber)
                    BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                                    "", "", "") '出发条件
                    If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                        If TrainInf(BeforeTrainF).StopLine(nStationNumber) = TrainInf(nTrainNumber).StopLine(nStationNumber) Then
                            lArrivalStart = TimeMinus(lTimeArrival, TrainInf(BeforeTrainF).Starting(nStationNumber))
                            jj = nSameDirectionStart(BeforeTrainF, nTrainNumber, lTimeArrival, nStationNumber, 0, 1, nSectionEnter)
                            nActInterval = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                                                       jj, 2, nSectionLeave, nStationNumber, nSectionEnter)
                            lArrivalStart = nActInterval - lArrivalStart
                            If lArrivalStart > 0 Then
                                sErrorMessage = "前发后到间隔不满足，与标准时间相差" & lArrivalStart & "秒"
                                Call AddTrainErrorInf(nTrainNumber, nStationNumber, lTimeArrival, sErrorMessage)
                            End If
                        End If
                    End If
                Next
            End If
        Next
    End Sub

    '环形交路到达间隔判断,在折返站
    Public Sub CheckCirclePathArrivelAndStart()
        Dim i, j As Integer
        Dim nStationNumber As Integer
        Dim lTimeArrival As Long
        Dim BeforeTrainF As Integer
        Dim nTrainNumber As Integer
        Dim nSdirection As Integer
        Dim nFirTrain As Integer
        Dim nSecTrain As Integer
        Dim lArrivalStart As Integer
        Dim jj As Integer
        Dim nActInterval As Integer
        Dim nSectionLeave As Integer
        Dim nSectionEnter As Integer
        Dim sErrorMessage As String
        For i = 1 To UBound(ChediInfo)
            If UBound(ChediInfo(i).nLinkTrain) > 1 Then
                For j = 2 To UBound(ChediInfo(i).nLinkTrain)
                    nFirTrain = ChediInfo(i).nLinkTrain(j - 1)
                    nSecTrain = ChediInfo(i).nLinkTrain(j)
                    If TrainInf(nFirTrain).TrainStyle = "环形车" And TrainInf(nSecTrain).TrainStyle = "环形车" Then
                        ' If nSecTrain = 3 Then Stop
                        '环形第一站的检查
                        nTrainNumber = nSecTrain
                        nStationNumber = TrainInf(nTrainNumber).nPathID(1)
                        lTimeArrival = TrainInf(nTrainNumber).Starting(nStationNumber)
                        nSdirection = nDirection(nTrainNumber)
                        nSectionLeave = TrainInf(nTrainNumber).nPassSection(1)
                        nSectionEnter = nSectionLeave
                        BeforeTrainF = BEFORE(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 1, 1, _
                                        "", "", "") '出发条件
                        If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).EndStation Then
                            lArrivalStart = TimeMinus(TrainInf(nFirTrain).Arrival(TrainInf(nFirTrain).nPathID(UBound(TrainInf(nFirTrain).nPathID))), TrainInf(BeforeTrainF).Starting(nStationNumber))
                            If lArrivalStart > 3600 * 12 Then
                                lArrivalStart = lArrivalStart - 24 * 3600
                            End If
                            jj = 4 '发到间隔
                            nActInterval = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                                                       jj, 2, nSectionLeave, nStationNumber, nSectionEnter)
                            lArrivalStart = nActInterval - lArrivalStart
                            If lArrivalStart > 0 And lArrivalStart < 3600 Then
                                'If nTrainNumber = 5 Then Stop
                                sErrorMessage = "与列车" & TrainInf(BeforeTrainF).Train & "在始发折返站的前发后到间隔不满足，与标准时间相差" & lArrivalStart & "秒"
                                Call AddTrainErrorInf(nTrainNumber, nStationNumber, lTimeArrival, sErrorMessage)
                            End If
                        End If

                        '环形第二站的检查
                        nTrainNumber = nFirTrain
                        ' If nFirTrain = 113 Then Stop
                        nStationNumber = TrainInf(nFirTrain).nPathID(UBound(TrainInf(nFirTrain).nPathID))
                        lTimeArrival = TrainInf(nTrainNumber).Arrival(nStationNumber)
                        nSdirection = nDirection(nTrainNumber)
                        nSectionLeave = TrainInf(nTrainNumber).nPassSection(UBound(TrainInf(nTrainNumber).nPassSection))
                        nSectionEnter = nSectionLeave
                        BeforeTrainF = AFTER(nTrainNumber, lTimeArrival, nStationNumber, nSdirection, 0, 0, "", "", "")
                        If BeforeTrainF <> nTrainNumber And StationInf(nStationNumber).sStationName <> TrainInf(BeforeTrainF).StartStation Then
                            lArrivalStart = TimeMinus(TrainInf(BeforeTrainF).Arrival(nStationNumber), TrainInf(nSecTrain).Starting(TrainInf(nSecTrain).nPathID(1)))
                            If lArrivalStart > 3600 * 12 Then
                                lArrivalStart = lArrivalStart - 24 * 3600
                            End If
                            jj = 4 '发到间隔
                            nActInterval = lIntervalTime(BeforeTrainF, nTrainNumber, _
                                                                       jj, 2, nSectionLeave, nStationNumber, nSectionEnter)
                            lArrivalStart = nActInterval - lArrivalStart
                            If lArrivalStart > 0 And lArrivalStart < 3600 Then
                                sErrorMessage = "与列车" & TrainInf(BeforeTrainF).Train & "在终到折返站的前发后到间隔不满足，与标准时间相差" & lArrivalStart & "秒"
                                Call AddTrainErrorInf(nTrainNumber, nStationNumber, lTimeArrival, sErrorMessage)
                            End If
                        End If
                    End If
                Next
            End If
        Next

    End Sub

    '车站进路交叉检查
    Public Sub CheckStationPath()
        Dim i As Integer
        Dim nNextTrain As Integer
        Dim PathOneTime1 As Integer
        Dim PathOneTime2 As Integer
        Dim nStaID As Integer
        Dim ConflictTrain As New Generic.List(Of Integer)
        Dim ConflictInfor As New Generic.List(Of String)
        Dim sErrorMessage As String
        '折返进路交叉判断
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                If TrainInf(i).TrainReturn(2) <> 0 Then
                    '终到折返比较
                    nNextTrain = TrainInf(i).TrainReturn(2)
                    nStaID = TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))
                    If TrainInf(i).Starting(nStaID) <> -1 Then
                        PathOneTime1 = TrainInf(i).Starting(nStaID)
                    End If
                    If TrainInf(i).sEndZFArrival <> -1 Then
                        PathOneTime2 = TrainInf(i).sEndZFArrival
                    End If
                    If PathOneTime1 <> -1 Then
                        If PathOneTime2 <> -1 Then
                            ' If i = 10 Then Stop
                            ConflictTrain.Clear()
                            ConflictInfor.Clear()
                            If IfPathConflict(i, ConflictTrain, ConflictInfor, TrainInf(i).NextStation, TrainInf(i).StopLine(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))), TrainInf(i).sEndZFLine, PathOneTime1, PathOneTime2) = True Then
                                sErrorMessage = "该列车由到达股道 " & TrainInf(i).StopLine(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))) & " 至折返股道 " & TrainInf(i).sEndZFLine & ConflictInfor.Item(0)
                                Call AddTrainErrorInf(i, nStaID, PathOneTime1, sErrorMessage)
                                'MsgBox(TrainInf(i).NextStation & TrainInf(i).Train & "-" & TrainInf(ConflictTrain.Item(0)).Train)
                                'Stop
                            End If
                        End If
                    End If

                    '始发折返比较
                    If TrainInf(nNextTrain).sStartZFStarting <> -1 Then
                        PathOneTime1 = TrainInf(nNextTrain).sStartZFStarting
                    End If
                    If TrainInf(nNextTrain).Arrival(nStaID) <> -1 Then
                        PathOneTime2 = TrainInf(nNextTrain).Arrival(nStaID)
                    End If
                    If PathOneTime1 <> -1 Then
                        If PathOneTime2 <> -1 Then
                            ConflictTrain.Clear()
                            If IfPathConflict(i, ConflictTrain, ConflictInfor, TrainInf(i).NextStation, TrainInf(nNextTrain).sStartZFLine, TrainInf(nNextTrain).StopLine(nStaID), PathOneTime1, PathOneTime2) = True Then
                                sErrorMessage = "该列车由折返股道 " & TrainInf(nNextTrain).sStartZFLine & " 至到达股道 " & TrainInf(nNextTrain).StopLine(nStaID) & ConflictInfor.Item(0)
                                Call AddTrainErrorInf(i, nStaID, PathOneTime1, sErrorMessage)
                                'MsgBox(TrainInf(i).NextStation & TrainInf(i).Train & "-" & TrainInf(ConflictTrain.Item(0)).Train)
                            End If
                        End If
                    End If

                End If
            End If
        Next

    End Sub


    '得到进路通过的道岔编号
    Public Function GetPathCrossingNum(ByVal sStaName As String, ByVal sFirTrackNum As String, ByVal sSecTrackNum As String) As System.Collections.Generic.List(Of String)
        Dim a As New System.Collections.Generic.List(Of String)
        a = Nothing
        Dim i, j As Integer
        For i = 1 To UBound(StationInf)
            If StationInf(i).sStationName = sStaName Then
                For j = 0 To StationInf(i).sCrossUse.Length - 1
                    If StationInf(i).sCrossUse(j).LinkTrackNum = sFirTrackNum And StationInf(i).sCrossUse(j).StaTrackNum = sSecTrackNum Then
                        a = StationInf(i).sCrossUse(j).PathCrossNum
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next
        Return a
    End Function

    '判断当前进路是否有交叉
    Public Function IfPathConflict(ByVal nTrain As Integer, ByRef ConflictTrains As Generic.List(Of Integer), ByVal ConflictInfor As Generic.List(Of String), ByVal sStaName As String, ByVal sStopTrack As String, ByVal sReturnTrack As String, ByVal nTime1 As Integer, ByVal nTime2 As Integer) As Boolean
        IfPathConflict = False
        Dim i As Integer
        Dim nNextTrain As Integer
        Dim PathOneTime1 As Integer
        Dim PathOneTime2 As Integer
        Dim sPathOne As Generic.List(Of String)
        Dim sPathTwo As Generic.List(Of String)
        sPathOne = GetPathCrossingNum(sStaName, sStopTrack, sReturnTrack)
        Dim Conftime As Integer
        Conftime = 120
        Dim curStopTrack As String
        Dim CurReturnTrack As String
        Dim nStaID As Integer
        For i = 1 To UBound(TrainInf)
            If i <> nTrain Then
                If TrainInf(i).Train <> "" Then
                    If TrainInf(i).TrainReturn(2) <> 0 Then
                        nNextTrain = TrainInf(i).TrainReturn(2)
                        nStaID = TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))
                        '到达折返判断
                        If StationInf(nStaID).sStationName = sStaName Then
                            If TrainInf(i).Starting(nStaID) <> -1 Then
                                PathOneTime1 = TimeMinus(TrainInf(i).Starting(nStaID), Conftime)
                            End If
                            If TrainInf(i).sEndZFArrival <> -1 Then
                                PathOneTime2 = TimeAdd(TrainInf(i).sEndZFArrival, Conftime)
                            End If
                            If PathOneTime1 <> -1 Then
                                If PathOneTime2 <> -1 Then
                                    If TimeConflictCheck(PathOneTime1, PathOneTime2, nTime1, nTime2) = True Then
                                        curStopTrack = TrainInf(i).StopLine(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID)))
                                        CurReturnTrack = TrainInf(i).sEndZFLine
                                        sPathTwo = GetPathCrossingNum(sStaName, curStopTrack, CurReturnTrack)
                                        If CrossPathConflictCheck(sPathOne, sPathTwo) = True Then
                                            ConflictTrains.Add(i)
                                            ConflictInfor.Add(", 与列车 " & TrainInf(i).sPrintTrain & " 在由到达股道 " & curStopTrack & " 至折返股道 " & CurReturnTrack & " 发生进路交叉")
                                            IfPathConflict = True
                                            Exit For
                                        End If
                                    End If
                                End If
                            End If

                            '始发折返判断
                            If TrainInf(nNextTrain).sStartZFStarting <> -1 Then
                                PathOneTime1 = TimeMinus(TrainInf(nNextTrain).sStartZFStarting, Conftime)
                            End If
                            If TrainInf(nNextTrain).Arrival(nStaID) <> -1 Then
                                PathOneTime2 = TimeAdd(TrainInf(nNextTrain).Arrival(nStaID), Conftime)
                            End If
                            If PathOneTime1 <> -1 Then
                                If PathOneTime2 <> -1 Then
                                    If TimeConflictCheck(PathOneTime1, PathOneTime2, nTime1, nTime2) = True Then
                                        curStopTrack = TrainInf(nNextTrain).StopLine(TrainInf(nNextTrain).nPathID(1))
                                        CurReturnTrack = TrainInf(nNextTrain).sStartZFLine
                                        sPathTwo = GetPathCrossingNum(sStaName, curStopTrack, CurReturnTrack)
                                        If CrossPathConflictCheck(sPathOne, sPathTwo) = True Then
                                            ConflictTrains.Add(i)
                                            ConflictInfor.Add(", 与列车 " & TrainInf(i).sPrintTrain & " 在由折返股道 " & CurReturnTrack & " 至出发股道 " & curStopTrack & " 发生进路交叉")
                                            IfPathConflict = True
                                            Exit For
                                        End If
                                    End If
                                End If
                            End If

                        End If

                    End If
                End If
            End If
        Next i
    End Function

    '判读折返股道安排是否有冲突
    Public Function IfReturnTrackOcupyConflict(ByVal nTrain As Integer, ByRef ConflictTrains As Generic.List(Of Integer), ByVal ConflictInfor As Generic.List(Of String), ByVal sStaName As String, ByVal sReturnTrack As String, ByVal nTime1 As Integer, ByVal nTime2 As Integer) As Boolean
        IfReturnTrackOcupyConflict = False
        Dim i As Integer
        Dim nNextTrain As Integer
        Dim PathOneTime1 As Integer
        Dim PathOneTime2 As Integer
        Dim Conftime As Integer
        Conftime = 0

        '修正冲突时间参数*#*

        Dim nStaID As Integer
        For i = 1 To UBound(TrainInf)
            If i <> nTrain Then
                If TrainInf(i).Train <> "" Then
                    If TrainInf(i).TrainReturn(2) <> 0 Then
                        nNextTrain = TrainInf(i).TrainReturn(2)
                        nStaID = TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))
                        '到达折返判断
                        If StationInf(nStaID).sStationName = sStaName Then
                            If sReturnTrack = TrainInf(i).sEndZFLine Then '占用同一折返股道
                                If TrainInf(i).sEndZFArrival <> -1 Then
                                    PathOneTime1 = TimeAdd(TrainInf(i).sEndZFArrival, Conftime)
                                End If
                                If TrainInf(i).sEndZFStarting <> -1 Then
                                    PathOneTime2 = TimeMinus(TrainInf(i).sEndZFStarting, Conftime)
                                End If
                                If PathOneTime1 <> -1 Then
                                    If PathOneTime2 <> -1 Then
                                        If TimeConflictCheck(PathOneTime1, PathOneTime2, nTime1, nTime2) = True Then
                                            ConflictTrains.Add(i)
                                            ConflictInfor.Add(", 与列车 " & TrainInf(i).sPrintTrain & " 在折返股道 " & sReturnTrack & " 占用时间冲突 ")
                                            IfReturnTrackOcupyConflict = True
                                            Exit For
                                        End If
                                    End If
                                End If
                            End If


                            '始发折返判断
                            If sReturnTrack = TrainInf(i).sStartZFLine Then '占用同一折返股道
                                If TrainInf(nNextTrain).sStartZFArrival <> -1 Then
                                    PathOneTime1 = TimeMinus(TrainInf(nNextTrain).sStartZFArrival, Conftime)
                                End If
                                If TrainInf(nNextTrain).sStartZFStarting <> -1 Then
                                    PathOneTime2 = TimeAdd(TrainInf(nNextTrain).sStartZFStarting, Conftime)
                                End If
                                If PathOneTime1 <> -1 Then
                                    If PathOneTime2 <> -1 Then
                                        If TimeConflictCheck(PathOneTime1, PathOneTime2, nTime1, nTime2) = True Then
                                            ConflictTrains.Add(i)
                                            ConflictInfor.Add(", 与列车 " & TrainInf(i).sPrintTrain & " 在折返股道" & sReturnTrack & " 占用时间冲突")
                                            IfReturnTrackOcupyConflict = True
                                            Exit For
                                        End If
                                    End If
                                End If
                            End If

                        End If

                    End If
                End If
            End If
        Next i
    End Function

    '两段时间冲突比较
    Function TimeConflictCheck(ByVal OneTime1 As Integer, ByVal OneTime2 As Integer, ByVal TwoTime1 As Integer, ByVal TwoTime2 As Integer) As Boolean
        'atimetemp到达当前时间，stimetemp出发当前时间，statemp当前车站,trnnum列车编号
        TimeConflictCheck = False
        If TimeMinus(OneTime1, TwoTime1) <= TimeMinus(OneTime1, TwoTime2) Then
            TimeConflictCheck = True
            Exit Function
        End If
        If TimeMinus(OneTime2, TwoTime1) <= TimeMinus(OneTime2, TwoTime2) Then
            TimeConflictCheck = True
            Exit Function
        End If
        If TimeMinus(TwoTime1, OneTime1) <= TimeMinus(TwoTime1, OneTime2) Then
            TimeConflictCheck = True
            Exit Function
        End If
        If TimeMinus(TwoTime2, OneTime1) <= TimeMinus(TwoTime2, OneTime2) Then
            TimeConflictCheck = True
            Exit Function
        End If

    End Function

    ' 进路交叉冲突比较
    Function CrossPathConflictCheck(ByVal Path1 As Generic.List(Of String), ByVal Path2 As Generic.List(Of String)) As Boolean
        Dim b As Boolean
        b = False
        Dim i As Integer
        Dim j As Integer
        Dim ifEnd As Boolean
        ifEnd = False
        If Path1 Is Nothing Or Path2 Is Nothing Then

        Else
            For i = 0 To Path1.Count - 1
                For j = 0 To Path2.Count - 1
                    If Path2.Item(j) = Path1.Item(i) Then
                        b = True
                        ifEnd = True
                        Exit For
                    End If
                Next
                If ifEnd = True Then
                    Exit For
                End If
            Next

        End If
        Return b
    End Function

    '检查站前折返是否满足 到发间隔判断，站前折返时
    Public Sub ListNotSatisDaoFaTime()
        Dim i As Integer
        Dim sTmpTime As Long
        Dim ntmpTrain As Integer
        Dim sFaDaoTime As Long
        Dim sErrorMessage As String
        For i = 1 To UBound(TrainInf)
            'If i = 438 Then Stop
            If TrainInf(i).Train <> "" Then
                If TrainInf(i).TrainReturnStyle(2) = "站前折返" Then
                    ntmpTrain = SeekArriRightTrainZhanQianZheFan(i)
                    If ntmpTrain > 0 Then
                        sTmpTime = AddLitterTime(TrainInf(ntmpTrain).Arrival(TrainInf(ntmpTrain).nPathID(UBound(TrainInf(ntmpTrain).nPathID)))) - AddLitterTime(TrainInf(i).Starting(TrainInf(i).nPathID(1)))
                        sFaDaoTime = GetFaDaoJianGeTime(TrainInf(i).ComeStation, i)
                        If sTmpTime < sFaDaoTime Then
                            sErrorMessage = "与列车" & TrainInf(ntmpTrain).sPrintTrain & "在 " & TrainInf(i).ComeStation _
                        & " 站站前折返时发到间隔不足，规定发到间隔时间为：" & sFaDaoTime & " 秒，实际发到时间为：" & sTmpTime & " 秒"
                            Call AddTrainErrorInf(i, TrainInf(ntmpTrain).nPathID(1), TrainInf(i).Starting(TrainInf(i).nPathID(1)), sErrorMessage)
                        End If
                    End If
                End If
            End If
        Next i
    End Sub

    '得到发到间隔时间
    Public Function GetFaDaoJianGeTime(ByVal sSta As String, ByVal nTrain As Integer) As Long
        GetFaDaoJianGeTime = 0
        Dim i As Integer
        For i = 1 To UBound(StationInf)
            If StationInf(i).sStationName = sSta Then
                If nTrain Mod 2 <> 0 Then
                    GetFaDaoJianGeTime = StationInf(i).lTaoFaDao(1)
                Else
                    GetFaDaoJianGeTime = StationInf(i).lTaoFaDao(2)
                End If
                Exit For
            End If
        Next i
    End Function

    '查找满足到达列车右边到达最近的列车
    Public Function SeekArriRightTrainZhanQianZheFan(ByVal nTrain As Integer) As Integer
        Dim i As Integer
        Dim nMinTime As Long
        nMinTime = 25 * 3600.0#
        Dim sTmpTime As Long
        Dim ntmpTrain As Integer
        Dim nTrain2 As Integer
        Dim tmpT1 As Long
        Dim tmpT2 As Long
        Dim tmpT3 As Long

        If nTrain Mod 2 = 0 Then
            For i = 1 To UBound(TrainInf)
                'If i = 321 Then Stop
                If TrainInf(i).Train <> "" And (i Mod 2) <> 0 Then
                    If TrainInf(i).TrainReturnStyle(2) = "站前折返" Then
                        If TrainInf(i).NextStation = TrainInf(nTrain).ComeStation Then
                            sTmpTime = AddLitterTime(TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID)))) - AddLitterTime(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                            If sTmpTime >= 0 Then '右边到达的列车
                                If sTmpTime < nMinTime Then
                                    nMinTime = sTmpTime
                                    ntmpTrain = i
                                End If
                            Else '出发线的折返内到达的车
                                If TrainInf(nTrain).TrainReturn(1) > 0 Then
                                    nTrain2 = TrainInf(nTrain).TrainReturn(1)
                                    If i <> nTrain2 Then
                                        tmpT1 = AddLitterTime(TrainInf(nTrain2).Arrival(TrainInf(nTrain2).nPathID(UBound(TrainInf(nTrain2).nPathID))))
                                        tmpT3 = AddLitterTime(TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))))
                                        tmpT2 = AddLitterTime(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                                        If tmpT3 > tmpT1 And tmpT3 <= tmpT2 Then
                                            nMinTime = tmpT1 - tmpT3
                                            ntmpTrain = i
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Next i
        Else
            For i = 1 To UBound(TrainInf)
                'If i = 138 Then Stop
                If TrainInf(i).Train <> "" And (i Mod 2) = 0 Then
                    If TrainInf(i).TrainReturnStyle(2) = "站前折返" Then
                        If TrainInf(i).NextStation = TrainInf(nTrain).ComeStation Then
                            sTmpTime = AddLitterTime(TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID)))) - AddLitterTime(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                            If sTmpTime >= 0 Then '右边到达的列车
                                If sTmpTime < nMinTime Then
                                    nMinTime = sTmpTime
                                    ntmpTrain = i
                                End If
                            Else '出发线的折返内到达的车
                                If TrainInf(nTrain).TrainReturn(1) > 0 Then
                                    nTrain2 = TrainInf(nTrain).TrainReturn(1)
                                    If i <> nTrain2 Then
                                        tmpT1 = AddLitterTime(TrainInf(nTrain2).Arrival(TrainInf(nTrain2).nPathID(UBound(TrainInf(nTrain2).nPathID))))
                                        tmpT3 = AddLitterTime(TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))))
                                        tmpT2 = AddLitterTime(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                                        If tmpT3 > tmpT1 And tmpT3 <= tmpT2 Then
                                            nMinTime = tmpT1 - tmpT3
                                            ntmpTrain = i
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            Next i
        End If
        If ntmpTrain > 0 Then
            SeekArriRightTrainZhanQianZheFan = ntmpTrain
        Else
            SeekArriRightTrainZhanQianZheFan = 0
        End If
    End Function


    '调整交路时判断是否能满足间隔要求
    Public Sub EditJiaoLuLine(ByVal nCheDiNum As Integer)
        Dim i As Integer
        Dim NFir As Integer
        Dim nSec As Integer
        Dim sZheFantime As Single
        Dim sTime As Single
        Dim sTime1 As Long
        Dim sTime2 As Long
        Dim sErrorMessage As String
        If nCheDiNum = 0 Then Exit Sub
        If UBound(ChediInfo(nCheDiNum).nLinkTrain) > 1 Then
            For i = 2 To UBound(ChediInfo(nCheDiNum).nLinkTrain)
                NFir = ChediInfo(nCheDiNum).nLinkTrain(i - 1)
                'If NFir = 280 Then Stop
                nSec = ChediInfo(nCheDiNum).nLinkTrain(i)
                ' If (NFir + nSec) Mod 2 <> 0 Then
                sTime2 = TrainInf(NFir).Arrival(TrainInf(NFir).nPathID(UBound(TrainInf(NFir).nPathID)))
                sTime1 = TrainInf(nSec).Starting(TrainInf(nSec).nPathID(1)) 'AddCompareTime(TrainInf(nSec).Starting(TrainInf(nSec).nPathID(1)), sTime2)
                sTime = TimeMinus(sTime1, sTime2)
                sZheFantime = CDZGetZheFanTime(NFir, ChediInfo(nCheDiNum).SCheDiLeiXing, StationInf(TrainInf(NFir).nPathID(UBound(TrainInf(NFir).nPathID))).sStationName, TrainInf(NFir).TrainReturnStyle(2))
                If sTime - sZheFantime < 0 Then
                    'MsgBox TrainInf(nFir).Train & "与" & TrainInf(nSec).Train & "的折返时间不够"
                    sErrorMessage = "折返时间不满足，规定折返时间为：" & sZheFantime & " 秒，实际折返时间为：" & sTime & "秒"
                    Call AddTrainErrorInf(NFir, TrainInf(NFir).nPathID(UBound(TrainInf(NFir).nPathID)), sTime2, sErrorMessage)
                    'Call DrawLineInCheDiJLTu(NFir, 1, QBColor(11))
                    'Call DrawLineInCheDiJLTu(nSec, 1, QBColor(11))
                End If
            Next i
        End If
    End Sub

    '能力计算铺画完后，将有富余的列车左移
    Public Sub ZhenLiStartTrain(ByVal nInterValTime As Integer, ByVal sNotZhenLiJiaoLu As String)
        Dim i As Integer
        Dim j As Integer
        Dim nCurSta As Integer
        Dim nStopSta As Integer
        Dim nFirSta As Integer
        Dim nStopStaID As Integer
        Dim nBeforeTrain As Integer
        Dim nDeltTime As Integer
        Dim nMinDeltTime As Integer
        Dim nUporDown As Integer
        For i = 1 To UBound(TrainInf)
            nMinDeltTime = 100000
            nStopStaID = 0
            If TrainInf(i).Train <> "" And TrainInf(i).sJiaoLuName <> sNotZhenLiJiaoLu Then
                For j = 1 To UBound(TrainInf(i).nPathID)
                    If TrainInf(i).Arrival(TrainInf(i).nPathID(j)) <> TrainInf(i).Starting(TrainInf(i).nPathID(j)) Then
                        nStopStaID = j
                        Exit For
                    End If
                Next
                If i Mod 2 = 0 Then
                    nUporDown = 2
                Else
                    nUporDown = 1
                End If
                'If nStopStaID = 0 Then
                '    nStopStaID = UBound(TrainInf(i).nPathID)
                'End If
                If nStopStaID > 0 And nStopStaID <> 1 Then 'And nStopStaID <> UBound(TrainInf(i).nPathID) Then
                    nFirSta = TrainInf(i).nPathID(1)
                    nStopSta = TrainInf(i).nPathID(nStopStaID)
                    For j = 1 To nStopStaID - 1
                        nCurSta = TrainInf(i).nPathID(j)
                        nBeforeTrain = BEFORE(i, TrainInf(i).Starting(nCurSta), nCurSta, nUporDown, 1, 1, "", "", "") '出发条件
                        nDeltTime = TimeMinus(TrainInf(i).Starting(nCurSta), TrainInf(nBeforeTrain).Starting(nCurSta)) - nInterValTime 'StationInf(nCurSta).IKK(1)
                        If nDeltTime >= 0 And nDeltTime < 3600 Then
                            If nMinDeltTime > nDeltTime Then
                                nMinDeltTime = nDeltTime
                            End If
                        End If
                        nDeltTime = TimeMinus(TrainInf(i).Arrival(nCurSta), TrainInf(nBeforeTrain).Arrival(nCurSta)) - nInterValTime 'StationInf(nCurSta).IKK(1)
                        If nDeltTime >= 0 And nDeltTime < 3600 Then
                            If nMinDeltTime > nDeltTime Then
                                nMinDeltTime = nDeltTime
                            End If
                        End If
                    Next
                    For j = 2 To nStopStaID
                        nCurSta = TrainInf(i).nPathID(j)
                        nBeforeTrain = BEFORE(i, TrainInf(i).Arrival(nCurSta), nCurSta, nUporDown, 0, 0, "", "", "") '到达条件
                        nDeltTime = TimeMinus(TrainInf(i).Arrival(nCurSta), TrainInf(nBeforeTrain).Arrival(nCurSta)) - nInterValTime 'StationInf(nCurSta).IKK(1)
                        If nDeltTime >= 0 And nDeltTime < 3600 Then
                            If nMinDeltTime > nDeltTime Then
                                nMinDeltTime = nDeltTime
                            End If
                        End If
                    Next
                    If nMinDeltTime > 0 And nMinDeltTime <> 100000 Then
                        For j = 1 To nStopStaID
                            nCurSta = TrainInf(i).nPathID(j)
                            If j <> nStopStaID Then
                                TrainInf(i).Starting(nCurSta) = TimeMinus(TrainInf(i).Starting(nCurSta), nMinDeltTime)
                            End If
                            TrainInf(i).Arrival(nCurSta) = TimeMinus(TrainInf(i).Arrival(nCurSta), nMinDeltTime)
                        Next
                        'QMoveLine(i, nFirSta, nMinDeltTime, nStopSta, nUporDown, 1)
                    End If
                End If
            End If
        Next
    End Sub

    '将列车信息复制到临时列车
    Public Sub setTempTraininfTime(ByVal nTrain As Integer)
        Dim i As Integer
        ReDim tempFirstTrainInf.Starting(UBound(TrainInf(nTrain).Starting))
        ReDim tempFirstTrainInf.Arrival(UBound(TrainInf(nTrain).Arrival))
        ReDim tempFirstTrainInf.StopLine(UBound(TrainInf(nTrain).StopLine))
        For i = 1 To UBound(TrainInf(nTrain).Starting)
            tempFirstTrainInf.Starting(i) = TrainInf(nTrain).Starting(i)
            tempFirstTrainInf.Arrival(i) = TrainInf(nTrain).Arrival(i)
            tempFirstTrainInf.StopLine(i) = TrainInf(nTrain).StopLine(i)
        Next
    End Sub
    '将列车信息复制到临时列车
    Public Sub CopyTempTraininfTime(ByVal nTrain As Integer)
        Dim i As Integer
        For i = 1 To UBound(TrainInf(nTrain).Starting)
            TrainInf(nTrain).Starting(i) = tempFirstTrainInf.Starting(i)
            TrainInf(nTrain).Arrival(i) = tempFirstTrainInf.Arrival(i)
            TrainInf(nTrain).StopLine(i) = tempFirstTrainInf.StopLine(i)
        Next
    End Sub
End Module
