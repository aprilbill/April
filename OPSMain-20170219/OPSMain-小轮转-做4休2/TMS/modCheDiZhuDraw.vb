Module modCheDiZhuDraw

    Structure typeChediZhuDrawPara
        Dim nCurJianGe As Long '铺画间隔时间
        Dim nLastDrawTime As Long '铺画的最后时间
        Dim sDownStartSta As String '下行第一站，比较间隔用
        Dim sUpStartSta As String '上行第一站，比较间隔用
        Dim nFirReturnTime As Long '下行第一站的折返时间
        Dim nSecReturnTime As Long ' 上行第一站的折返时间
    End Structure
    Public CDZDrawPara As typeChediZhuDrawPara

    Structure typeCheDiZhuInf
        Dim sMaxDownTime As Single '下行的最大时间
        Dim sMinDownTime As Single '下行的最小时间
        Dim sMaxUpTime As Single '上行的最大时间
        Dim sMinUpTime As Single '上行的最小时间

        Dim sMaxDownArrTime As Single '下行组的最大发车时间
        Dim sMinDownArrTime As Single '下行组的最小发车时间
        Dim sMaxUpArrTime As Single '上行组的最大发车时间
        Dim sMinUpArrTime As Single '上行组的最小发车时间

        Dim nId() As Integer '车底的ID，与chediinfo对应
        Dim nEnd() As Integer '是否结束, 0表示没有结束，1表示已经结束
        Dim nUporDown() As Integer '1为下行，2为上行
        Dim sArriTime() As Single '到达车站时间
        Dim sStarActTime() As Single '在车站的发车的实际时间
        Dim sStarTime() As Single '可在车站的发车的最小时间
        Dim sStaName() As String '所在车站名_
        Dim nLinkTrain() As Integer '联接车次
    End Structure
    Public CheDiZhuInf() As typeCheDiZhuInf

    Structure TypeCheDiZhuInformation
        Dim nChediID() As Integer
        Dim nCurTrain() As Integer
        Dim nUporDown() As Integer
        Dim nArriTime() As Long
        Dim nMinStartTime() As Long
        Dim nStartTime() As Long
        Dim sStaName() As String
        Dim sNextTrainJiaoLuName() As String
        Dim sNextTrainRunScale() As String
        Dim sNextTrainStopScale() As String
    End Structure
    Public CheDiZhuInfor As TypeCheDiZhuInformation

    ''停站车站停站设置
    'Structure typeStopStaSet
    '    Dim sStaName As String '停站名
    '    Dim nStaId As Integer '车站的ID，与Stationinf()对应
    '    Dim sUpStopFirTime As Single '最后停站的时间
    '    Dim sUpStopTrainNum As Integer '停站的列车数
    '    Dim sDownStopFirTime As Single '最后停站的时间
    '    Dim sDownStopTrainNum As Integer '停站的列车数
    'End Structure
    'Public StopStaSet() As typeStopStaSet

    '广州车站的股道可使用的时间段安排结构
    Structure typeGuDaoUseTime
        Dim sStaName As String '车站名
        Dim sShiJianDuan() As String '时间段
        Dim sBeTime() As Long '开始时间,单位：S
        Dim sEndTime() As Long '结束时间,单位：S
    End Structure
    Public GuDaoUseTime() As typeGuDaoUseTime

    '列车停站设置结构
    Structure typeTrainStop
        Dim sStaName As String '车站名
        Dim sTrainStyle As String '列车类型
        Dim sStopTime As Single '停站时间
        Dim sMinStopInterval As Single '列车停站的最小间隔时间
        Dim sMaxStopInterval As Single '列车停站的最大间隔时间
        Dim nMaxStopTrainNum As Integer '最大的停车数量
        Dim nDownStopTrain() As Integer
        Dim nDownStopTime() As Long
        Dim nUpStopTrain() As Integer
        Dim nUpStopTime() As Long
    End Structure
    Public TrainStop() As typeTrainStop

    Public IntevalTimeDown As Long '下行车底组的铺画间隔时间
    Public IntevalTimeUp As Long ' 上行车底组的铺画间隔时间
    Public SCDTempBeTimeDown As Single '车底组下行的最大时间
    Public SCDTempBeTimeUp As Single '车底组上行的最大时间
    Public Const sMoveJGTime As Integer = 15 '移动间隔时间,时间精度


    ''分车底组铺画
    'Public Sub FenCheDiZhuPuHua(ByVal nZhu As Integer)
    '    Call ResetCheDiZhu(nZhu)
    '    Call CheDiZhuPuHua(nZhu)

    '    Call RefreshDiagram(1)

    'End Sub


    ''重设置车底组
    'Public Sub ResetCheDiZhu(ByVal nZhu As Integer)

    '    Call SetCheDiZhuMaxMinTime(nZhu) '重设车底组


    '    Dim i, j As Integer
    '    ' Dim IFE As Integer
    '    Dim Nn As Integer
    '    Nn = 0


    '    '新增一个车底组
    '    ReDim Preserve CheDiZhuInf(nZhu + 1)
    '    ReDim Preserve CheDiZhuInf(nZhu + 1).nEnd(UBound(ChediInfo))
    '    ReDim Preserve CheDiZhuInf(nZhu + 1).nId(UBound(ChediInfo))
    '    ReDim Preserve CheDiZhuInf(nZhu + 1).nUporDown(UBound(ChediInfo))
    '    ReDim Preserve CheDiZhuInf(nZhu + 1).sArriTime(UBound(ChediInfo))
    '    ReDim Preserve CheDiZhuInf(nZhu + 1).sStaName(UBound(ChediInfo))
    '    ReDim Preserve CheDiZhuInf(nZhu + 1).sStarActTime(UBound(ChediInfo))
    '    ReDim Preserve CheDiZhuInf(nZhu + 1).sStarTime(UBound(ChediInfo))
    '    ReDim Preserve CheDiZhuInf(nZhu + 1).nLinkTrain(UBound(ChediInfo))


    '    ''判断有没有新增的或减少的车底
    '    Dim nChediId As Integer
    '    Dim nTrain As Integer
    '    Dim nTrain1 As Integer
    '    Dim sCheDiReturn As Single
    '    Dim sTime As Single
    '    Dim sTime1 As Single
    '    Dim sZheFantime As Single
    '    '出库车在进广九线前能否再跑个来回
    '    For i = 1 To UBound(CheDiZhuInf(nZhu).nId)
    '        nChediId = CheDiZhuInf(nZhu).nId(i)
    '        If ChediInfo(nChediId).sYunYongFangShi = "广九车底" Then
    '            'If nChediId = 15 Then Stop
    '            If UBound(ChediInfo(nChediId).nLinkTrain) > 0 Then
    '                nTrain = ChediInfo(nChediId).nLinkTrain(1)
    '                If nTrain Mod 2 <> 0 Then
    '                    If StationInf(TrainInf(nTrain).nPathID(1)).sStationName = "广州东" Then '下行
    '                        If CheDiZhuInf(nZhu).nUporDown(i) = 1 Then
    '                            sTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)) - ChediInfo(nChediId).sChuKuTime
    '                            sCheDiReturn = GetCheDiAtGZReturnTime(TrainInf(nTrain).SCheDiLeiXing)
    '                            If sTime - sCheDiReturn >= 0 Then 'CheDiZhuInf(nZhu).sMinDownTime And sTime <= CheDiZhuInf(nZhu).sMaxDownTime Then
    '                                CheDiZhuInf(nZhu).nUporDown(i) = 1
    '                                CheDiZhuInf(nZhu).nEnd(i) = 0
    '                        End If
    '                        End If
    '                    End If

    '                End If
    '            End If
    '        End If
    '    Next i

    '    '有无广九线出来的车
    '    For i = 1 To UBound(CheDiZhuInf(nZhu).nId)

    '        nChediId = CheDiZhuInf(nZhu).nId(i)
    '        If ChediInfo(nChediId).sYunYongFangShi = "广九车底" Then
    '            If UBound(ChediInfo(nChediId).nLinkTrain) > 0 Then

    '                nTrain = ChediInfo(nChediId).nLinkTrain(UBound(ChediInfo(nChediId).nLinkTrain))
    '                'If nTrain = 2 Then Stop
    '                If nTrain Mod 2 = 0 Then
    '                    sTime = TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID)))
    '                    sZheFantime = CDZGetZheFanTime(nTrain, TrainInf(nTrain).SCheDiLeiXing, "深圳", "立即折返")
    '                    sTime = sTime + sZheFantime
    '                    If sTime >= CheDiZhuInf(nZhu).sMaxDownTime And sTime <= CheDiZhuInf(nZhu).sMaxDownArrTime Then
    '                        'If CheDiZhuInf(nZhu).nId(i) = 3 Then Stop
    '                        CheDiZhuInf(nZhu).nUporDown(i) = 1
    '                        CheDiZhuInf(nZhu).nEnd(i) = 0
    '                        CheDiZhuInf(nZhu).sArriTime(i) = sTime
    '                        Exit For
    '                    End If
    '                End If
    '            End If
    '        End If
    '    Next i


    '    '有无中间能再跑一个来回的
    '    For i = 1 To UBound(CheDiZhuInf(nZhu).nId)
    '        nChediId = CheDiZhuInf(nZhu).nId(i)
    '        If ChediInfo(nChediId).sYunYongFangShi = "广九车底" Then
    '            If UBound(ChediInfo(nChediId).nLinkTrain) > 1 Then
    '                For j = 1 To UBound(ChediInfo(nChediId).nLinkTrain) - 1
    '                    nTrain = ChediInfo(nChediId).nLinkTrain(j)
    '                    nTrain1 = ChediInfo(nChediId).nLinkTrain(j + 1)
    '                    'If nTrain = 6 Then Stop
    '                    If (nTrain Mod 2 = 0) And (nTrain1 Mod 2 <> 0) Then
    '                        'If ChediInfo(CheDiZhuInf(nZhu).nId(i)).sChediId = "SS8CD009" And TrainInf(nTrain).Train = "T812" Then Stop
    '                        If StationInf(TrainInf(nTrain1).nPathID(1)).sStationName = "广州东" Then
    '                            'If CheDiZhuInf(nZhu).nUpOrDown(i) = 1 Then
    '                            sTime = TrainInf(nTrain1).Starting(TrainInf(nTrain1).nPathID(1))
    '                            sTime1 = TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID)))
    '                            sTime = sTime - sTime1
    '                            sCheDiReturn = GetCheDiAtGZReturnTime(TrainInf(nTrain).SCheDiLeiXing)
    '                            sZheFantime = CDZGetZheFanTime(nTrain, TrainInf(nTrain).SCheDiLeiXing, "广州东", "立即折返")
    '                            sTime1 = sTime1 + sZheFantime
    '                            If sTime - sCheDiReturn >= 0 Then 'CheDiZhuInf(nZhu).sMinDownTime And sTime <= CheDiZhuInf(nZhu).sMaxDownTime Then
    '                                If sTime1 >= CheDiZhuInf(nZhu).sMaxDownTime And sTime1 <= CheDiZhuInf(nZhu).sMaxDownArrTime Then
    '                                    CheDiZhuInf(nZhu).nUporDown(i) = 1
    '                                    CheDiZhuInf(nZhu).nEnd(i) = 0
    '                                    CheDiZhuInf(nZhu).sArriTime(i) = sTime1
    '                                End If
    '                            Else
    '                                If StationInf(TrainInf(nTrain1).nPathID(UBound(TrainInf(nTrain1).nPathID))).sStationName = "九龙" Then
    '                                    If sTime1 >= CheDiZhuInf(nZhu).sMaxDownTime And sTime1 <= CheDiZhuInf(nZhu).sMaxDownArrTime Then
    '                                        If CheDiZhuInf(nZhu).nEnd(i) = 0 Then CheDiZhuInf(nZhu).nEnd(i) = 1
    '                                    End If
    '                                End If
    '                            End If
    '                            'End If
    '                        End If
    '                    End If
    '                Next j
    '            End If
    '        End If
    '    Next i

    '    Call CheDiZhuPaiXu(nZhu) '车底组排序
    '    Call SetCheDiZhuMaxMinTime(nZhu) '重设车底组

    'End Sub


    ''按车底组铺画
    'Public Sub CheDiZhuPuHua(ByVal nZhu As Integer)
    '    Dim i As Integer
    '    Dim nEndTrn As Integer, nBeginTrn As Integer
    '    Dim lStime As Long, lstemp1 As Long, lstemp2 As Long
    '    Dim sZheFantime As Single
    '    '先将车底组接上车
    '    Dim DownNum As Integer
    '    Dim UpNum As Integer
    '    Dim sZFtime As Single
    '    'Dim nTrain1 As Integer
    '    'Dim sTempT As Single
    '    'Dim TempUpArriTime As Single
    '    'Dim TempDownArriTime As Single
    '    Dim nIfEnter As Integer
    '    Dim Cdid As Integer
    '    Dim tempTime As Single
    '    Dim nFtrain As Integer
    '    DownNum = 1
    '    UpNum = 1
    '    Dim nTrain As Integer
    '    '    IntevalTimeDown = CDZCalJianGeTime(1, SCDDayEndTime - SCDDayBeginTime)
    '    '    IntevalTimeUp = CDZCalJianGeTime(2, SCDDayEndTime - SCDDayBeginTime)
    '    Dim sDownJiaoLu As String
    '    sDownJiaoLu = "广州东->深圳"
    '    '下行车底组
    '    For i = 1 To UBound(CheDiZhuInf(nZhu).nId)
    '        Cdid = CheDiZhuInf(nZhu).nId(i)
    '        If CheDiZhuInf(nZhu).nEnd(i) = 0 Then
    '            If CheDiZhuInf(nZhu).nUporDown(i) = 1 Then
    '                nTrain = GetNotGouTrain(1, Cdid) ' FaAddNewTrainInf((sDownJiaoLu, '
    '                If nTrain <> 0 Then
    '                    If nZhu = 1 Then
    '                        tempTime = CheDiZhuInf(nZhu).sMaxDownTime + (DownNum - 1) * IntevalTimeDown
    '                    Else
    '                        tempTime = CheDiZhuInf(nZhu).sMaxDownTime + DownNum * IntevalTimeDown
    '                    End If
    '                    sZFtime = tempTime - CheDiZhuInf(nZhu).sArriTime(i)
    '                    If sZFtime > 0 Then
    '                        TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)) = CDZSetTime(tempTime)
    '                        TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(1)) = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
    '                    Else
    '                        TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)) = CDZSetTime(CheDiZhuInf(nZhu).sArriTime(i))
    '                        TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(1)) = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
    '                    End If
    '                    nFtrain = CheDiZhuInf(nZhu).nLinkTrain(i)
    '                    If nFtrain > 0 Then
    '                        TrainInf(nFtrain).TrainReturn(2) = nTrain
    '                        TrainInf(nTrain).TrainReturn(1) = nFtrain
    '                    End If

    '                    Call AddLianGuaCheCi(Cdid, nTrain)

    '                    Call SetTrainStopSta(nTrain, ChediInfo(Cdid).SCheDiLeiXing, 1)
    '                    TrainInf(nTrain).SCheDiLeiXing = ChediInfo(Cdid).SCheDiLeiXing
    '                    TrainInf(nTrain).nTrainTimeKind = ChediInfo(Cdid).nYunxingBiaochi
    '                    'traininf(ntrain).TrainReturn(
    '                    nBeginTrn = nFindStaNum(nTrain, 1)  'TrainInf(DrawRank(i)).nPathID(1) '
    '                    nEndTrn = nFindStaNum(nTrain, 2) 'TrainInf(DrawRank(i)).nPathID(UBound(TrainInf(DrawRank(i)).nPathID())) '
    '                    lstemp1 = TrainInf(nTrain).Arrival(nBeginTrn)
    '                    lstemp2 = TrainInf(nTrain).Starting(nBeginTrn)
    '                    If lstemp1 = -1 Then lstemp1 = lstemp2
    '                    If lstemp2 = -1 Then lstemp2 = lstemp1
    '                    'If lsTemp1 <> -1 And lsTemp2 <> -1 And TrainInf(1).TrainPuorNot = 0 Then
    '                    TDrawLineOld(lstemp1, lstemp2, lStime, nTrain, nBeginTrn, nEndTrn, 0)
    '                    'End If
    '                    TrainInf(nTrain).TrainPuorNot = 1


    '                    'ChediInfo(CDId).nlinktrain(UBound(ChediInfo(CDId).nlinktrain())) = nTrain
    '                    ''                    If UBound(ChediInfo(CDId).nlinktrain()) > 1 Then
    '                    ''                        If ChediInfo(CDId).bIfEnterGZ(UBound(ChediInfo(CDId).nlinktrain()) - 1) = 1 Then
    '                    ''                            ChediInfo(CDId).bIfEnterGZ(UBound(ChediInfo(CDId).nlinktrain())) = 1
    '                    ''                        Else
    '                    ''                            ChediInfo(CDId).bIfEnterGZ(UBound(ChediInfo(CDId).nlinktrain())) = 0
    '                    ''                        End If
    '                    ''                    Else
    '                    ''                        ChediInfo(CDId).bIfEnterGZ(UBound(ChediInfo(CDId).nlinktrain())) = 0
    '                    ''                    End If
    '                    TrainInf(nTrain).nCheDiPuOrNot = 1
    '                    TrainInf(nTrain).SCheDiLeiXing = ChediInfo(Cdid).SCheDiLeiXing
    '                    sZheFantime = CDZGetZheFanTime(nTrain, ChediInfo(Cdid).SCheDiLeiXing, StationInf(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))).sStationName, "立即折返")
    '                    CheDiZhuInf(nZhu + 1).nId(i) = CheDiZhuInf(nZhu).nId(i)
    '                    CheDiZhuInf(nZhu + 1).nEnd(i) = 0
    '                    CheDiZhuInf(nZhu + 1).nUporDown(i) = 2
    '                    CheDiZhuInf(nZhu + 1).sArriTime(i) = EditArriTime(nTrain, 1) + sZheFantime
    '                    CheDiZhuInf(nZhu + 1).sStarTime(i) = EditArriTime(nTrain, 2)
    '                    CheDiZhuInf(nZhu + 1).sStarActTime(i) = CheDiZhuInf(nZhu + 1).sArriTime(i)
    '                    CheDiZhuInf(nZhu + 1).sStaName(i) = StationInf(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))).sStationName
    '                    CheDiZhuInf(nZhu + 1).nLinkTrain(i) = nTrain
    '                    DownNum = DownNum + 1
    '                End If
    '            End If
    '        End If
    '    Next i


    '    '上行车底组
    '    nIfEnter = 0
    '    For i = 1 To UBound(CheDiZhuInf(nZhu).nId)
    '        Cdid = CheDiZhuInf(nZhu).nId(i)

    '        If CheDiZhuInf(nZhu).nEnd(i) = 0 Then
    '            If CheDiZhuInf(nZhu).nUporDown(i) = 2 Then
    '                nTrain = GetNotGouTrain(2, Cdid)
    '                If nTrain <> 0 Then
    '                    If nZhu = 1 Then
    '                        tempTime = CheDiZhuInf(nZhu).sMaxUpTime + (UpNum - 1) * IntevalTimeUp
    '                    Else
    '                        tempTime = CheDiZhuInf(nZhu).sMaxUpTime + UpNum * IntevalTimeUp
    '                    End If
    '                    sZFtime = tempTime - CheDiZhuInf(nZhu).sArriTime(i)
    '                    If sZFtime > 0 Then
    '                        TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)) = CDZSetTime(tempTime)
    '                        TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(1)) = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
    '                    Else
    '                        TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)) = CDZSetTime(CheDiZhuInf(nZhu).sArriTime(i))
    '                        TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(1)) = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
    '                    End If

    '                    Call SetTrainStopSta(nTrain, ChediInfo(Cdid).SCheDiLeiXing, 2)

    '                    TrainInf(nTrain).SCheDiLeiXing = ChediInfo(Cdid).SCheDiLeiXing
    '                    TrainInf(nTrain).nTrainTimeKind = ChediInfo(Cdid).nYunxingBiaochi

    '                    nFtrain = CheDiZhuInf(nZhu).nLinkTrain(i)
    '                    If nFtrain > 0 Then
    '                        TrainInf(nFtrain).TrainReturn(2) = nTrain
    '                        TrainInf(nTrain).TrainReturn(1) = nFtrain
    '                    End If

    '                    Call AddLianGuaCheCi(Cdid, nTrain)

    '                    nBeginTrn = nFindStaNum(nTrain, 1)  'TrainInf(DrawRank(i)).nPathID(1) '
    '                    nEndTrn = nFindStaNum(nTrain, 2) 'TrainInf(DrawRank(i)).nPathID(UBound(TrainInf(DrawRank(i)).nPathID())) '
    '                    lstemp1 = TrainInf(nTrain).Arrival(nBeginTrn)
    '                    lstemp2 = TrainInf(nTrain).Starting(nBeginTrn)
    '                    If lstemp1 = -1 Then lstemp1 = lstemp2
    '                    If lstemp2 = -1 Then lstemp2 = lstemp1
    '                    'If lsTemp1 <> -1 And lsTemp2 <> -1 And TrainInf(1).TrainPuorNot = 0 Then
    '                    TDrawLineOld(lstemp1, lstemp2, lStime, nTrain, nBeginTrn, nEndTrn, 0)
    '                    'End If
    '                    TrainInf(nTrain).TrainPuorNot = 1


    '                    TrainInf(nTrain).nCheDiPuOrNot = 1
    '                    TrainInf(nTrain).SCheDiLeiXing = ChediInfo(Cdid).SCheDiLeiXing
    '                    sZheFantime = CDZGetZheFanTime(nTrain, ChediInfo(Cdid).SCheDiLeiXing, StationInf(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))).sStationName, "立即折返")

    '                    Dim sEnterGZZheFanTime As Single
    '                    sEnterGZZheFanTime = EnterGZZheFanTime(nTrain)

    '                    CheDiZhuInf(nZhu + 1).nId(i) = CheDiZhuInf(nZhu).nId(i)
    '                    CheDiZhuInf(nZhu + 1).nEnd(i) = 0
    '                    CheDiZhuInf(nZhu + 1).nUporDown(i) = 1
    '                    If sEnterGZZheFanTime > 0 And nIfEnter < 2 Then
    '                        CheDiZhuInf(nZhu + 1).sArriTime(i) = EditArriTime(nTrain, 1) + sEnterGZZheFanTime
    '                        CheDiZhuInf(nZhu + 1).sStarTime(i) = EditArriTime(nTrain, 2)
    '                        CheDiZhuInf(nZhu + 1).sStarActTime(i) = EditArriTime(nTrain, 1) + sEnterGZZheFanTime
    '                        'ChediInfo(cdID).bIfEnterGZ(UBound(ChediInfo(cdID).bIfEnterGZ())) = 1
    '                        TrainInf(nTrain).nIfEnterGZSta = 1
    '                        nIfEnter = nIfEnter + 1
    '                    Else
    '                        CheDiZhuInf(nZhu + 1).sArriTime(i) = EditArriTime(nTrain, 1) + sZheFantime
    '                        CheDiZhuInf(nZhu + 1).sStarTime(i) = EditArriTime(nTrain, 2)
    '                        CheDiZhuInf(nZhu + 1).sStarActTime(i) = EditArriTime(nTrain, 1) + sZheFantime
    '                        'ChediInfo(cdID).bIfEnterGZ(UBound(ChediInfo(cdID).bIfEnterGZ())) = 0
    '                    End If
    '                    CheDiZhuInf(nZhu + 1).sStaName(i) = StationInf(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))).sStationName
    '                    CheDiZhuInf(nZhu + 1).nLinkTrain(i) = nTrain
    '                    UpNum = UpNum + 1
    '                End If
    '            End If
    '        End If
    '        If i = UBound(CheDiZhuInf(nZhu).nId) And nIfEnter = 0 Then
    '            'MsgBox "该车底组没有进广州的车"
    '        End If
    '    Next i

    '    '没有勾画的车底
    '    For i = 1 To UBound(CheDiZhuInf(nZhu).nId)
    '        If CheDiZhuInf(nZhu).nEnd(i) = 1 Then
    '            CheDiZhuInf(nZhu + 1).nId(i) = CheDiZhuInf(nZhu).nId(i)
    '            CheDiZhuInf(nZhu + 1).nEnd(i) = CheDiZhuInf(nZhu).nEnd(i)
    '            CheDiZhuInf(nZhu + 1).nUporDown(i) = CheDiZhuInf(nZhu).nUporDown(i)
    '            CheDiZhuInf(nZhu + 1).sArriTime(i) = CheDiZhuInf(nZhu).sArriTime(i)
    '            CheDiZhuInf(nZhu + 1).sStarTime(i) = CheDiZhuInf(nZhu).sStarTime(i)
    '            CheDiZhuInf(nZhu + 1).sStarActTime(i) = CheDiZhuInf(nZhu).sStarActTime(i)
    '            CheDiZhuInf(nZhu + 1).sStaName(i) = CheDiZhuInf(nZhu).sStaName(i)
    '            CheDiZhuInf(nZhu + 1).nLinkTrain(i) = 0
    '        End If
    '    Next i
    'End Sub

    ''设置车底组的最大最小时间
    'Public Sub SetCheDiZhuMaxMinTime(ByVal nZhu As Integer)
    '    Dim i As Integer
    '    Dim sT As Single
    '    sT = 0
    '    If UBound(CheDiZhuInf(nZhu).nId) > 0 Then
    '        For i = 1 To UBound(CheDiZhuInf(nZhu).nId)
    '            If CheDiZhuInf(nZhu).nUporDown(i) = 2 And CheDiZhuInf(nZhu).nEnd(i) = 0 And CheDiZhuInf(nZhu).sStarTime(i) > sT Then
    '                sT = CheDiZhuInf(nZhu).sStarTime(i)
    '            End If
    '        Next i
    '        CheDiZhuInf(nZhu).sMaxDownTime = sT
    '    End If

    '    sT = 100000
    '    If UBound(CheDiZhuInf(nZhu).nId) > 0 Then
    '        For i = 1 To UBound(CheDiZhuInf(nZhu).nId)
    '            If CheDiZhuInf(nZhu).nUporDown(i) = 2 And CheDiZhuInf(nZhu).nEnd(i) = 0 And CheDiZhuInf(nZhu).sStarTime(i) < sT Then
    '                sT = CheDiZhuInf(nZhu).sStarTime(i)
    '            End If
    '        Next i
    '        'SCDTempBeTimeDown = sT
    '        CheDiZhuInf(nZhu).sMinDownTime = sT
    '    End If

    '    sT = 0
    '    If UBound(CheDiZhuInf(nZhu).nId) > 0 Then
    '        For i = 1 To UBound(CheDiZhuInf(nZhu).nId)
    '            If CheDiZhuInf(nZhu).nUporDown(i) = 1 And CheDiZhuInf(nZhu).nEnd(i) = 0 And CheDiZhuInf(nZhu).sStarTime(i) > sT Then
    '                sT = CheDiZhuInf(nZhu).sStarTime(i)
    '            End If
    '        Next i
    '        CheDiZhuInf(nZhu).sMaxUpTime = sT
    '    End If

    '    sT = 100000
    '    If UBound(CheDiZhuInf(nZhu).nId) > 0 Then
    '        For i = 1 To UBound(CheDiZhuInf(nZhu).nId)
    '            If CheDiZhuInf(nZhu).nUporDown(i) = 1 And CheDiZhuInf(nZhu).nEnd(i) = 0 And CheDiZhuInf(nZhu).sStarTime(i) < sT Then
    '                sT = CheDiZhuInf(nZhu).sStarTime(i)
    '            End If
    '        Next i
    '        SCDTempBeTimeUp = sT
    '        CheDiZhuInf(nZhu).sMinUpTime = sT
    '    End If

    '    '下行组的最大时间
    '    sT = 0
    '    If UBound(CheDiZhuInf(nZhu).nId) > 0 Then
    '        For i = 1 To UBound(CheDiZhuInf(nZhu).nId)
    '            If CheDiZhuInf(nZhu).nUporDown(i) = 1 And CheDiZhuInf(nZhu).nEnd(i) = 0 And CheDiZhuInf(nZhu).sArriTime(i) > sT Then
    '                sT = CheDiZhuInf(nZhu).sArriTime(i)
    '            End If
    '        Next i
    '        CheDiZhuInf(nZhu).sMaxDownArrTime = sT
    '    End If

    '    sT = 100000
    '    If UBound(CheDiZhuInf(nZhu).nId) > 0 Then
    '        For i = 1 To UBound(CheDiZhuInf(nZhu).nId)
    '            If CheDiZhuInf(nZhu).nUporDown(i) = 1 And CheDiZhuInf(nZhu).nEnd(i) = 0 And CheDiZhuInf(nZhu).sArriTime(i) < sT Then
    '                sT = CheDiZhuInf(nZhu).sArriTime(i)
    '            End If
    '        Next i
    '        'SCDTempBeTimeDown = sT
    '        CheDiZhuInf(nZhu).sMinDownArrTime = sT
    '    End If

    '    sT = 0
    '    If UBound(CheDiZhuInf(nZhu).nId) > 0 Then
    '        For i = 1 To UBound(CheDiZhuInf(nZhu).nId)
    '            If CheDiZhuInf(nZhu).nUporDown(i) = 2 And CheDiZhuInf(nZhu).nEnd(i) = 0 And CheDiZhuInf(nZhu).sArriTime(i) > sT Then
    '                sT = CheDiZhuInf(nZhu).sArriTime(i)
    '            End If
    '        Next i
    '        CheDiZhuInf(nZhu).sMaxUpArrTime = sT
    '    End If

    '    sT = 100000
    '    If UBound(CheDiZhuInf(nZhu).nId) > 0 Then
    '        For i = 1 To UBound(CheDiZhuInf(nZhu).nId)
    '            If CheDiZhuInf(nZhu).nUporDown(i) = 2 And CheDiZhuInf(nZhu).nEnd(i) = 0 And CheDiZhuInf(nZhu).sArriTime(i) < sT Then
    '                sT = CheDiZhuInf(nZhu).sArriTime(i)
    '            End If
    '        Next i
    '        SCDTempBeTimeUp = sT
    '        CheDiZhuInf(nZhu).sMinUpArrTime = sT
    '    End If

    '    If nZhu = 1 Then
    '        CheDiZhuInf(nZhu).sMaxDownTime = CheDiZhuInf(nZhu).sMinUpTime
    '        CheDiZhuInf(nZhu).sMaxUpTime = CheDiZhuInf(nZhu).sMinDownTime
    '        For i = 1 To UBound(CheDiZhuInf(nZhu).nId)
    '            If CheDiZhuInf(nZhu).nUporDown(i) = 1 Then
    '                CheDiZhuInf(nZhu).sArriTime(i) = CheDiZhuInf(nZhu).sMinDownTime
    '                CheDiZhuInf(nZhu).sStarTime(i) = CheDiZhuInf(nZhu).sMinDownTime
    '            Else
    '                CheDiZhuInf(nZhu).sArriTime(i) = CheDiZhuInf(nZhu).sMinUpTime
    '                CheDiZhuInf(nZhu).sStarTime(i) = CheDiZhuInf(nZhu).sMinUpTime
    '            End If
    '        Next i
    '    End If
    'End Sub

    ''一个车底来回一次所花的时间
    'Public Function GetCheDiAtGZReturnTime(ByVal SCheDiLeiXing As String) As Long
    '    GetCheDiAtGZReturnTime = 0
    '    Select Case SCheDiLeiXing
    '        Case "蓝箭"
    '            GetCheDiAtGZReturnTime = 170 * 60.0#
    '        Case "新时速"
    '            GetCheDiAtGZReturnTime = 150 * 60.0#
    '        Case "准高速"
    '            GetCheDiAtGZReturnTime = 160 * 60.0#
    '    End Select
    'End Function


    ''车底组排序
    'Public Sub CheDiZhuPaiXu(ByVal nZhu As Integer)
    '    '车底按时间排序列
    '    Dim K As Integer
    '    Dim j As Integer
    '    Dim TempTime1, Temptime2 As Long
    '    Dim tempId As Integer
    '    Dim TempnEnd As Integer
    '    Dim TempArriTime As Single
    '    Dim TempStartTime As Single
    '    Dim TempStaName As String
    '    Dim TempUporDown As Integer
    '    Dim TempStarActTime As Single
    '    Dim tmpTrain As Integer
    '    Dim Flag As Integer
    '    Flag = 1
    '    K = UBound(CheDiZhuInf(nZhu).nId)
    '    Do While Flag > 0
    '        K = K - 1
    '        Flag = 0
    '        For j = 1 To K
    '            TempTime1 = CheDiZhuInf(nZhu).sArriTime(j)
    '            Temptime2 = CheDiZhuInf(nZhu).sArriTime(j + 1)
    '            If TempTime1 > Temptime2 Then
    '                tempId = CheDiZhuInf(nZhu).nId(j)
    '                TempnEnd = CheDiZhuInf(nZhu).nEnd(j)
    '                TempArriTime = CheDiZhuInf(nZhu).sArriTime(j)
    '                TempStartTime = CheDiZhuInf(nZhu).sStarTime(j)
    '                TempStarActTime = CheDiZhuInf(nZhu).sStarActTime(j)
    '                TempStaName = CheDiZhuInf(nZhu).sStaName(j)
    '                TempUporDown = CheDiZhuInf(nZhu).nUporDown(j)
    '                tmpTrain = CheDiZhuInf(nZhu).nLinkTrain(j)

    '                CheDiZhuInf(nZhu).nId(j) = CheDiZhuInf(nZhu).nId(j + 1)
    '                CheDiZhuInf(nZhu).nEnd(j) = CheDiZhuInf(nZhu).nEnd(j + 1)
    '                CheDiZhuInf(nZhu).sArriTime(j) = CheDiZhuInf(nZhu).sArriTime(j + 1)
    '                CheDiZhuInf(nZhu).sStarTime(j) = CheDiZhuInf(nZhu).sStarTime(j + 1)
    '                CheDiZhuInf(nZhu).sStarActTime(j) = CheDiZhuInf(nZhu).sStarActTime(j + 1)
    '                CheDiZhuInf(nZhu).sStaName(j) = CheDiZhuInf(nZhu).sStaName(j + 1)
    '                CheDiZhuInf(nZhu).nUporDown(j) = CheDiZhuInf(nZhu).nUporDown(j + 1)
    '                CheDiZhuInf(nZhu).nLinkTrain(j) = CheDiZhuInf(nZhu).nLinkTrain(j + 1)

    '                CheDiZhuInf(nZhu).nId(j + 1) = tempId
    '                CheDiZhuInf(nZhu).nEnd(j + 1) = TempnEnd
    '                CheDiZhuInf(nZhu).sArriTime(j + 1) = TempArriTime
    '                CheDiZhuInf(nZhu).sStarTime(j + 1) = TempStartTime
    '                CheDiZhuInf(nZhu).sStarActTime(j + 1) = TempStarActTime
    '                CheDiZhuInf(nZhu).sStaName(j + 1) = TempStaName
    '                CheDiZhuInf(nZhu).nUporDown(j + 1) = TempUporDown
    '                CheDiZhuInf(nZhu).nLinkTrain(j + 1) = tmpTrain

    '                Flag = 1
    '            End If
    '        Next j
    '    Loop
    'End Sub

    ''找一列满足要求的车，没有被勾画上的列车
    'Public Function GetNotGouTrain(ByVal nUporDown As Integer, ByVal Cdid As Integer) As Integer
    '    Dim i As Integer
    '    GetNotGouTrain = 0
    '    If nUporDown Mod 2 <> 0 Then '下行
    '        For i = 1 To UBound(TrainInf)
    '            If i Mod 2 <> 0 Then
    '                If TrainInf(i).nCheDiPuOrNot = 0 And TrainInf(i).SCheDiLeiXing = ChediInfo(Cdid).SCheDiLeiXing And TrainInf(i).sIfFixTime <> "固定时刻" And TrainInf(i).sIfFixTime <> "广九直通" Then
    '                    GetNotGouTrain = i
    '                    Exit Function
    '                End If
    '            End If
    '        Next i
    '    Else
    '        For i = 1 To UBound(TrainInf)
    '            If i Mod 2 = 0 Then
    '                If TrainInf(i).nCheDiPuOrNot = 0 And TrainInf(i).SCheDiLeiXing = ChediInfo(Cdid).SCheDiLeiXing And TrainInf(i).sIfFixTime <> "固定时刻" And TrainInf(i).sIfFixTime <> "广九直通" Then
    '                    GetNotGouTrain = i
    '                    Exit Function
    '                End If
    '            End If
    '        Next i

    '    End If
    'End Function

    '设置时间格式
    Public Function CDZSetTime(ByVal sTime As Single) As Single
        sTime = TimeToZhenShu(Val(sTime), sMoveJGTime)
        If sTime >= (24 * 3600.0#) Then
            CDZSetTime = sTime Mod 24 * 3600.0#
        Else
            CDZSetTime = sTime
        End If
    End Function
    ''设置列车停站
    'Public Sub SetTrainStopSta(ByVal nTrain As Integer, ByVal SCheDiStyle As String, ByVal nUporDown As Integer)
    '    Dim i As Integer
    '    Dim j As Integer
    '    Dim n As Integer
    '    Dim TempTi As Single
    '    Dim TempN As Integer
    '    Dim Ssta As String
    '    Dim SStopT As Single
    '    Dim TK As Integer
    '    TK = 0
    '    ReDim TrainInf(nTrain).stopTime(0)
    '    ReDim TrainInf(nTrain).StopStation(0)

    '    '////////////////////////////////////////////////////////只停一站或不停
    '    TempN = 0
    '    If nUporDown = 1 Then ' 下行
    '        For j = UBound(StopStaSet) To 1 Step -1
    '            For i = 1 To UBound(TrainStop)
    '                If TrainStop(i).sStaName = StopStaSet(j).sStaName Then
    '                    TempTi = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)) - StopStaSet(j).sDownStopFirTime
    '                    If TempTi >= TrainStop(i).sMinStopInterval * 60 Then
    '                        n = j
    '                        TempN = 1
    '                        Exit For
    '                    End If
    '                End If
    '            Next i
    '            If TempN = 1 Then Exit For
    '        Next j
    '    Else
    '        For j = 1 To UBound(StopStaSet)
    '            For i = 1 To UBound(TrainStop)
    '                If TrainStop(i).sStaName = StopStaSet(j).sStaName Then
    '                    TempTi = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)) - StopStaSet(j).sUpStopFirTime
    '                    If TempTi >= TrainStop(i).sMinStopInterval * 60 Then
    '                        n = j
    '                        TempN = 1
    '                        Exit For
    '                    End If
    '                End If
    '            Next i
    '            If TempN = 1 Then Exit For
    '        Next j
    '    End If

    '    If n > 0 Then
    '        SStopT = 0
    '        Ssta = StopStaSet(n).sStaName
    '        For i = 1 To UBound(TrainStop)
    '            If TrainStop(i).sStaName = Ssta And TrainStop(i).sTrainStyle = SCheDiStyle Then
    '                SStopT = TrainStop(i).sStopTime * 60
    '                Exit For
    '            End If
    '        Next i

    '        If SStopT > 0 Then
    '            With TrainInf(nTrain)
    '                .NumStop = .NumStop + 1
    '                ReDim Preserve .StopStation(.NumStop)
    '                ReDim Preserve .stopTime(.NumStop)
    '                .StopStation(.NumStop) = Ssta
    '                .stopTime(.NumStop) = SStopT
    '                If nUporDown = 1 Then
    '                    StopStaSet(n).sDownStopFirTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
    '                    StopStaSet(n).sDownStopTrainNum = StopStaSet(n).sDownStopTrainNum + 1
    '                Else
    '                    StopStaSet(n).sUpStopFirTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
    '                    StopStaSet(n).sUpStopTrainNum = StopStaSet(n).sUpStopTrainNum + 1
    '                End If
    '            End With
    '        End If
    '    End If

    'End Sub

    '判断是否跨过24点,修改到点时间
    Public Function EditArriTime(ByVal nTrain1 As Integer, ByVal DaoOrFa As Integer) As Single
        EditArriTime = 0
        Dim sArrTime As Single
        If DaoOrFa = 1 Then '到点
            sArrTime = TrainInf(nTrain1).Arrival(TrainInf(nTrain1).nPathID(UBound(TrainInf(nTrain1).nPathID)))
            If sArrTime >= 0 And sArrTime <= 4 * 3600.0# Then
                EditArriTime = sArrTime + 24 * 3600.0#
            Else
                EditArriTime = sArrTime
            End If
        Else '发点
            sArrTime = TrainInf(nTrain1).Starting(TrainInf(nTrain1).nPathID(1))
            If sArrTime >= 0 And sArrTime <= 4 * 3600.0# Then
                EditArriTime = sArrTime + 24 * 3600.0#
            Else
                EditArriTime = sArrTime
            End If
        End If
    End Function

    '判断是否能进广州站
    Public Function EnterGZZheFanTime(ByVal nTrain As Integer) As Single
        Dim i As Integer
        Dim sZFtime As Single
        Dim sBeTime As Single
        sBeTime = EditArriTime(nTrain, 1) + GetSectionRunTime("广州", "广州东", nTrain)

        EnterGZZheFanTime = 0
        sZFtime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, "广州", "立即折返") '+ GetSectionRunTime("广州", "广州东", nTrain) * 2
        For i = 1 To UBound(GuDaoUseTime(1).sBeTime)
            If sBeTime >= GuDaoUseTime(1).sBeTime(i) And (sBeTime + sZFtime) <= GuDaoUseTime(1).sEndTime(i) Then
                EnterGZZheFanTime = sZFtime + GetSectionRunTime("广州", "广州东", nTrain) * 2
                Exit Function
            End If
        Next i
    End Function

    '由区间名称得到区间运行时分
    Public Function GetSectionRunTime(ByVal sFirStaName As String, ByVal sSecStaName As String, ByVal nTrain As Integer) As Single
        Dim i As Integer
        Dim RunBiaoChi As Integer
        For i = 1 To UBound(SectionInf)
            If StationInf(SectionInf(i).nQStation).sStationName = sSecStaName And StationInf(SectionInf(i).nHStation).sStationName = sFirStaName Then
                RunBiaoChi = TrainInf(nTrain).nTrainTimeKind
                GetSectionRunTime = SectionInf(i).KeDownRunTime(RunBiaoChi)
                Exit Function
            End If
        Next i
    End Function

    '时间进整
    Public Function TimeToZhenShu(ByVal tmpTime As Single, ByVal tmpZheShu As Single) As Single
        If tmpTime / tmpZheShu <> Int(tmpTime / tmpZheShu) Then
            TimeToZhenShu = Int(tmpTime / 60) * 60 + Int((tmpTime - Int(tmpTime / 60) * 60) / tmpZheShu) * tmpZheShu + tmpZheShu
        Else
            TimeToZhenShu = tmpTime
        End If
    End Function

    '由车底ID得到对应的ID号
    Public Function GetCurCheDiInfoIDIDFromSchediID(ByVal sCheDiID As String) As Integer
        GetCurCheDiInfoIDIDFromSchediID = 0
        Dim i As Integer
        For i = 1 To UBound(ChediInfo)
            If ChediInfo(i).sCheDiID = sCheDiID Then
                GetCurCheDiInfoIDIDFromSchediID = i
                Exit For
            End If
        Next
    End Function

    '得到下一步要铺画交路名称
    Public Function GetNextTrainsJiaoLuName(ByVal sCurJiaoLuFirSta As String, ByVal sCurJiaoLuEndSta As String, ByVal nUporDown As Integer) As String
        'Dim i As Integer
        If nUporDown = 1 Then
            GetNextTrainsJiaoLuName = sCurJiaoLuEndSta & "-->" & sCurJiaoLuFirSta 'CDZDrawPara.sUpStartSta
        Else
            GetNextTrainsJiaoLuName = sCurJiaoLuEndSta & "-->" & sCurJiaoLuFirSta 'CDZDrawPara.sDownStartSta
        End If

    End Function

    '是否需要修改交路
    '得到下一步要铺画交路名称
    Public Function GetNewJiaoLuName(ByVal sJiaoLuName As String, ByVal sFirSta As String, ByVal sStopScaleName As String, ByVal sRunScaleName As String, ByVal nUporDown As Integer, ByVal nStartTime As Long) As String
        'Dim i As Integer
        Dim i As Integer
        Dim tmpRunTime As Long
        Dim tmpArritime As Long
        Dim tmpTime1 As Long
        Dim tmpTime2 As Long
        Dim nTrainNum As Integer
        GetNewJiaoLuName = sJiaoLuName
        If nUporDown = 1 Then

        Else
            tmpRunTime = GetCurTrainRunTime(sJiaoLuName, sRunScaleName, sStopScaleName, "广州", 0) + 5 * 60
            tmpArritime = TimeAdd(nStartTime, tmpRunTime) '预计到广州站的时间
            For i = 1 To UBound(GuDaoUseTime(1).sBeTime)
                tmpTime1 = AddLitterTime(GuDaoUseTime(1).sBeTime(i))
                tmpTime2 = AddLitterTime(GuDaoUseTime(1).sEndTime(i))
                If tmpArritime >= tmpTime1 And tmpArritime <= tmpTime2 Then
                    nTrainNum = GetToTrainEnterInGZ(tmpTime1, tmpTime2)
                    If nTrainNum = 0 Then
                        GetNewJiaoLuName = sFirSta & "-->" & "广州"
                    End If
                End If
            Next
        End If

    End Function

    '得以该时间段内有几个列车已经进入广州站
    Public Function GetToTrainEnterInGZ(ByVal nFirTime As Long, ByVal nEndTime As Long) As Integer
        Dim i As Integer
        Dim tmpTime As Long
        GetToTrainEnterInGZ = 0
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" And i Mod 2 = 0 Then
                If TrainInf(i).TrainStyle = "广深列车" And TrainInf(i).NextStation = "广州" Then
                    tmpTime = AddLitterTime(TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))))
                    If tmpTime >= nFirTime And tmpTime <= nEndTime Then
                        GetToTrainEnterInGZ = GetToTrainEnterInGZ + 1
                    End If
                End If
            End If
        Next
    End Function
    '得到下一步要铺画的停站标尺
    Public Function GetNextStopScaleName(ByVal sJiaoLuName As String, ByVal sRunScale As String, ByVal lCurStartTime As Long, ByVal nUporDown As Integer) As String
        GetNextStopScaleName = ""
        Dim i, j As Integer
        Dim tmpTime As Long
        Dim nMinJGTime As Long
        Dim tmpRunTime As Long
        Dim StopSta() As String
        ReDim StopSta(0)
        Dim tmpStopSta() As String
        ReDim tmpStopSta(0)
        Dim sNotStopScaleName As String
        Call InputStatoinStopTime()
        sNotStopScaleName = GetNotStopScaleName(sJiaoLuName)
        Dim sFirName As String
        Dim sEndName As String
        sEndName = GetJiaoLuStaName(sJiaoLuName, 2)
        sFirName = GetJiaoLuStaName(sJiaoLuName, 1)
        If nUporDown = 1 Then
            If sFirName = "广州" Then
                ReDim Preserve StopSta(UBound(StopSta) + 1)
                StopSta(UBound(StopSta)) = CDZDrawPara.sDownStartSta
            End If
        Else
            If sEndName = "广州" Then
                ReDim Preserve StopSta(UBound(StopSta) + 1)
                StopSta(UBound(StopSta)) = CDZDrawPara.sDownStartSta
            End If
        End If

        Dim MinJG() As Long
        ReDim MinJG(0)
        If nUporDown = 1 Then
            For i = 1 To UBound(TrainStop)
                nMinJGTime = 24 * 3600 + 1
                tmpRunTime = GetCurTrainRunTime(sJiaoLuName, sRunScale, sNotStopScaleName, TrainStop(i).sStaName, 1) + UBound(StopSta) * 8 * 60
                If nUporDown = 1 Then
                    For j = 1 To UBound(TrainStop(i).nDownStopTime)
                        tmpTime = AddLitterTime(TimeAdd(lCurStartTime, tmpRunTime)) - AddLitterTime(TrainStop(i).nDownStopTime(j))
                        If tmpTime > 0 Then
                            If tmpTime < nMinJGTime Then
                                nMinJGTime = tmpTime
                            End If
                        End If
                    Next
                Else
                    For j = 1 To UBound(TrainStop(i).nUpStopTime)
                        tmpTime = AddLitterTime(TimeAdd(lCurStartTime, tmpRunTime)) - AddLitterTime(TrainStop(i).nUpStopTime(j))
                        If tmpTime > 0 Then
                            If tmpTime < nMinJGTime Then
                                nMinJGTime = tmpTime
                            End If
                        End If
                    Next
                End If

                If nMinJGTime >= TrainStop(i).sMinStopInterval Or nMinJGTime = 24 * 3600 + 1 Then
                    ReDim Preserve tmpStopSta(UBound(tmpStopSta) + 1)
                    tmpStopSta(UBound(tmpStopSta)) = TrainStop(i).sStaName
                    ReDim Preserve MinJG(UBound(MinJG) + 1)
                    MinJG(UBound(MinJG)) = nMinJGTime
                End If
            Next
        Else
            For i = UBound(TrainStop) To 1 Step -1
                nMinJGTime = 24 * 3600 + 1
                tmpRunTime = GetCurTrainRunTime(sJiaoLuName, sRunScale, sNotStopScaleName, TrainStop(i).sStaName, 1) + UBound(StopSta) * 6
                If nUporDown = 1 Then
                    For j = 1 To UBound(TrainStop(i).nDownStopTime)
                        tmpTime = AddLitterTime(TimeAdd(lCurStartTime, tmpRunTime)) - AddLitterTime(TrainStop(i).nDownStopTime(j))
                        If tmpTime > 0 Then
                            If tmpTime < nMinJGTime Then
                                nMinJGTime = tmpTime
                            End If
                        End If
                    Next
                Else
                    For j = 1 To UBound(TrainStop(i).nUpStopTime)
                        tmpTime = AddLitterTime(TimeAdd(lCurStartTime, tmpRunTime)) - AddLitterTime(TrainStop(i).nUpStopTime(j))
                        If tmpTime > 0 Then
                            If tmpTime < nMinJGTime Then
                                nMinJGTime = tmpTime
                            End If
                        End If
                    Next
                End If

                If nMinJGTime >= TrainStop(i).sMinStopInterval Or nMinJGTime = 24 * 3600 + 1 Then
                    ReDim Preserve tmpStopSta(UBound(tmpStopSta) + 1)
                    tmpStopSta(UBound(tmpStopSta)) = TrainStop(i).sStaName
                    ReDim Preserve MinJG(UBound(MinJG) + 1)
                    MinJG(UBound(MinJG)) = nMinJGTime
                End If
            Next
        End If

        If UBound(tmpStopSta) > 0 Then
            If UBound(tmpStopSta) > 2 Then
                Dim k, temp, Flag As Integer
                Dim tmpSta As String
                Dim TempTime1, Temptime2 As Single
                Flag = 1
                k = UBound(MinJG)
                Do While Flag > 0
                    k = k - 1
                    Flag = 0
                    For j = 1 To k
                        TempTime1 = Val(MinJG(j))
                        Temptime2 = Val(MinJG(j + 1))
                        If TempTime1 > Temptime2 Then '
                            temp = MinJG(j)
                            MinJG(j) = MinJG(j + 1)
                            MinJG(j + 1) = temp
                            tmpSta = tmpStopSta(j)
                            tmpStopSta(j) = tmpStopSta(j + 1)
                            tmpStopSta(j + 1) = tmpSta
                            Flag = 1
                        End If
                    Next j
                Loop

                For i = 1 To 2
                    ReDim Preserve StopSta(UBound(StopSta) + 1)
                    StopSta(UBound(StopSta)) = tmpStopSta(i)
                Next
            Else
                For i = 1 To UBound(tmpStopSta)
                    ReDim Preserve StopSta(UBound(StopSta) + 1)
                    StopSta(UBound(StopSta)) = tmpStopSta(i)
                Next
            End If
        End If


        If UBound(StopSta) = 0 Then
            GetNextStopScaleName = sNotStopScaleName
        Else
            Dim k, t, q As Integer
            Dim IfSame() As Integer
            Dim IfFind As Integer
            Dim sCurStopSta() As String
            For i = 1 To UBound(BasicTrainInf)
                If BasicTrainInf(i).sJiaoLuName = sJiaoLuName Then ' StationInf(nStatemp).sStationName
                    For j = 1 To UBound(BasicTrainInf(i).StopScale)
                        'If BasicTrainInf(i).StopScale(j).sName = "停所有该停站" Then Stop
                        ReDim sCurStopSta(0)
                        For k = 2 To UBound(BasicTrainInf(i).StopScale(j).nStopStation) - 1
                            'For p = 1 To UBound(TrainStop)
                            If Val(BasicTrainInf(i).StopScale(j).StopTime(k)) > 0 Then
                                ' If StationInf(BasicTrainInf(i).StopScale(j).nStopStation(k)).sStationName = TrainStop(p).sStaName Then
                                ReDim Preserve sCurStopSta(UBound(sCurStopSta) + 1)
                                sCurStopSta(UBound(sCurStopSta)) = StationInf(BasicTrainInf(i).StopScale(j).nStopStation(k)).sStationName
                                ' Exit For
                            End If
                            ' End If
                            '  Next
                        Next k

                        If UBound(StopSta) = UBound(sCurStopSta) Then
                            ReDim IfSame(UBound(StopSta))
                            IfFind = 0
                            For q = 1 To UBound(StopSta)
                                For t = 1 To UBound(sCurStopSta)
                                    If StopSta(q) = sCurStopSta(t) Then
                                        IfSame(q) = 1
                                        Exit For
                                    End If
                                Next
                            Next
                            For q = 1 To UBound(IfSame)
                                If IfSame(q) = 0 Then
                                    IfFind = 1
                                End If
                            Next
                            If IfFind = 0 Then '找到
                                GetNextStopScaleName = BasicTrainInf(i).StopScale(j).sName
                                Exit For
                            End If
                        End If
                    Next j
                    Exit For
                End If
            Next i
        End If
        ' If GetNextStopScaleName = "" Then Stop
    End Function

    '得到一站不停时的标尺名称
    Public Function GetNotStopScaleName(ByVal sJiaoLuName As String) As String
        GetNotStopScaleName = ""
        Dim i, j, k As Integer
        Dim IfAllStop As Integer
        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName = sJiaoLuName Then
                For j = 1 To UBound(BasicTrainInf(i).StopScale)
                    IfAllStop = 0
                    For k = 2 To UBound(BasicTrainInf(i).StopScale(j).nStopStation) - 1
                        If Val(BasicTrainInf(i).StopScale(j).StopTime(k)) > 0 Then
                            IfAllStop = 1
                        End If
                    Next
                    If IfAllStop = 0 Then
                        GetNotStopScaleName = BasicTrainInf(i).StopScale(j).sName
                        Exit For
                    End If
                Next j
                Exit For
            End If
        Next i
    End Function

    '得到当前车站前面停站的时间
    Public Sub InputStatoinStopTime()
        Dim i, j, k As Integer
        '下行
        For k = 1 To UBound(TrainStop)
            ReDim TrainStop(k).nDownStopTime(0)
            ReDim TrainStop(k).nDownStopTrain(0)
            For i = 1 To UBound(TrainInf)
                If TrainInf(i).Train <> "" And i Mod 2 <> 0 Then
                    For j = 1 To UBound(TrainInf(i).nPathID)
                        If StationInf(TrainInf(i).nPathID(j)).sStationName = TrainStop(k).sStaName Then
                            If TrainInf(i).Arrival(TrainInf(i).nPathID(j)) <> TrainInf(i).Starting(TrainInf(i).nPathID(j)) Then '该列车在该车站停车
                                ReDim Preserve TrainStop(k).nDownStopTrain(UBound(TrainStop(k).nDownStopTrain) + 1)
                                ReDim Preserve TrainStop(k).nDownStopTime(UBound(TrainStop(k).nDownStopTime) + 1)
                                TrainStop(k).nDownStopTrain(UBound(TrainStop(k).nDownStopTrain)) = i
                                TrainStop(k).nDownStopTime(UBound(TrainStop(k).nDownStopTime)) = TrainInf(i).Arrival(TrainInf(i).nPathID(j))
                            End If
                        End If
                    Next
                End If
            Next
        Next
        '上行
        For k = 1 To UBound(TrainStop)
            ReDim TrainStop(k).nUpStopTime(0)
            ReDim TrainStop(k).nUpStopTrain(0)
            For i = 1 To UBound(TrainInf)
                If TrainInf(i).Train <> "" And i Mod 2 = 0 Then
                    For j = 1 To UBound(TrainInf(i).nPathID)
                        If StationInf(TrainInf(i).nPathID(j)).sStationName = TrainStop(k).sStaName Then
                            If TrainInf(i).Arrival(TrainInf(i).nPathID(j)) <> TrainInf(i).Starting(TrainInf(i).nPathID(j)) Then '该列车在该车站停车
                                ReDim Preserve TrainStop(k).nUpStopTrain(UBound(TrainStop(k).nUpStopTrain) + 1)
                                ReDim Preserve TrainStop(k).nUpStopTime(UBound(TrainStop(k).nUpStopTime) + 1)
                                TrainStop(k).nUpStopTrain(UBound(TrainStop(k).nUpStopTrain)) = i
                                TrainStop(k).nUpStopTime(UBound(TrainStop(k).nUpStopTime)) = TrainInf(i).Arrival(TrainInf(i).nPathID(j))
                            End If
                        End If
                    Next
                End If
            Next
        Next
    End Sub

    '计算整个列车的运行时分，basicTraininf所得
    Public Function GetCurTrainRunTime(ByVal sJiaoLuName As String, ByVal sRunScale As String, ByVal sStopScale As String, ByVal sNextSta As String, ByVal CalFangShi As Integer) As Long
        'CalFangShi,0表示计停站时间，1表示不计停站时间,2表示只计停站时间
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
                If StationInf(nFirSta).sStationName = sNextSta Then
                    GoTo sEnd
                End If
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
                If StationInf(nFirSta).sStationName = sNextSta Then
                    GoTo sEnd
                End If

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
                lngStopAppendTime = GetCurSecStartAppendTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                sRunTime = sRunTime + lngStopAppendTime

            Else

                nFirSta = BasicTrainInf(nTrain).nFirstID(i)
                nSecSta = BasicTrainInf(nTrain).nSecondID(i)
                If StationInf(nFirSta).sStationName = sNextSta Then
                    GoTo sEnd
                End If

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
                    lngStopAppendTime = GetCurSecStartAppendTime(sJiaoLuName, sRunScale, BasicTrainInf(nTrain).nPassSection(i))
                    sRunTime = sRunTime + lngStopAppendTime
                End If

            End If
        Next
sEnd:
        If CalFangShi = 2 Then '只计停站
            GetCurTrainRunTime = lngToStopTime
        Else
            GetCurTrainRunTime = sRunTime
        End If
    End Function

    Public Sub InputCheDiZhuInfor() '当前铺画的车底组
        Dim i As Integer
        Dim CdID As Integer
        Dim nFirTrain As Integer
        Dim sSta As String
        Dim tmpZFTime As Integer
        Dim tmpZFTime1 As Integer
        Dim tmpTime As Long
        Dim sJiaoLuName As String
        Dim sRunScaleName As String
        Dim sStopScaleName As String
        Dim tmpUpOrDown As Integer
        ReDim CheDiZhuInfor.nChediID(0)
        ReDim CheDiZhuInfor.nCurTrain(0)
        ReDim CheDiZhuInfor.nMinStartTime(0)
        ReDim CheDiZhuInfor.nUporDown(0)
        ReDim CheDiZhuInfor.nStartTime(0)
        ReDim CheDiZhuInfor.nArriTime(0)
        ReDim CheDiZhuInfor.sNextTrainJiaoLuName(0)
        ReDim CheDiZhuInfor.sNextTrainRunScale(0)
        ReDim CheDiZhuInfor.sNextTrainStopScale(0)

        For i = 1 To UBound(ChediInfo)
            If UBound(ChediInfo(i).nLinkTrain) > 0 And ChediInfo(i).bIfGouWang = False Then
                CdID = i
                nFirTrain = ChediInfo(i).nLinkTrain(UBound(ChediInfo(i).nLinkTrain))
                ' If nFirTrain = 6 Then Stop
                ReDim Preserve CheDiZhuInfor.nChediID(UBound(CheDiZhuInfor.nChediID) + 1)
                ReDim Preserve CheDiZhuInfor.nCurTrain(UBound(CheDiZhuInfor.nChediID) + 1)
                ReDim Preserve CheDiZhuInfor.nMinStartTime(UBound(CheDiZhuInfor.nChediID) + 1)
                ReDim Preserve CheDiZhuInfor.nUporDown(UBound(CheDiZhuInfor.nChediID) + 1)
                ReDim Preserve CheDiZhuInfor.nStartTime(UBound(CheDiZhuInfor.nChediID) + 1)
                ReDim Preserve CheDiZhuInfor.nArriTime(UBound(CheDiZhuInfor.nChediID) + 1)
                ReDim Preserve CheDiZhuInfor.sNextTrainJiaoLuName(UBound(CheDiZhuInfor.nChediID) + 1)
                ReDim Preserve CheDiZhuInfor.sNextTrainRunScale(UBound(CheDiZhuInfor.nChediID) + 1)
                ReDim Preserve CheDiZhuInfor.sNextTrainStopScale(UBound(CheDiZhuInfor.nChediID) + 1)

                CheDiZhuInfor.nChediID(UBound(CheDiZhuInfor.nChediID)) = i
                CheDiZhuInfor.nArriTime(UBound(CheDiZhuInfor.nChediID)) = TrainInf(nFirTrain).Arrival(TrainInf(nFirTrain).nPathID(UBound(TrainInf(nFirTrain).nPathID)))
                CheDiZhuInfor.nCurTrain(UBound(CheDiZhuInfor.nChediID)) = nFirTrain

                sSta = TrainInf(nFirTrain).NextStation
                If nFirTrain Mod 2 = 0 Then
                    tmpUpOrDown = 1
                Else
                    tmpUpOrDown = 2
                End If
                tmpZFTime = GetZheFanTime(TrainInf(nFirTrain).SCheDiLeiXing, sSta, TrainInf(nFirTrain).TrainReturnStyle(2))
                If nFirTrain Mod 2 = 0 Then
                    tmpZFTime1 = CDZDrawPara.nFirReturnTime
                Else
                    tmpZFTime1 = CDZDrawPara.nSecReturnTime
                End If
                tmpZFTime = Math.Max(tmpZFTime, tmpZFTime1)
                tmpTime = TimeAdd(TrainInf(nFirTrain).Arrival(TrainInf(nFirTrain).nPathID(UBound(TrainInf(nFirTrain).nPathID))), tmpZFTime)
                sRunScaleName = TrainInf(nFirTrain).sRunScaleName
                sJiaoLuName = GetNextTrainsJiaoLuName(TrainInf(nFirTrain).ComeStation, TrainInf(nFirTrain).NextStation, tmpUpOrDown)
                sStopScaleName = TrainInf(nFirTrain).sStopSclaeName

                CheDiZhuInfor.sNextTrainJiaoLuName(UBound(CheDiZhuInfor.nChediID)) = sJiaoLuName
                CheDiZhuInfor.sNextTrainRunScale(UBound(CheDiZhuInfor.nChediID)) = sRunScaleName
                CheDiZhuInfor.sNextTrainStopScale(UBound(CheDiZhuInfor.nChediID)) = sStopScaleName

                CheDiZhuInfor.nUporDown(UBound(CheDiZhuInfor.nChediID)) = tmpUpOrDown

                CheDiZhuInfor.nMinStartTime(UBound(CheDiZhuInfor.nChediID)) = tmpTime
                CheDiZhuInfor.nStartTime(UBound(CheDiZhuInfor.nChediID)) = tmpTime
            End If
        Next

    End Sub

    '得到列车在广州东或深圳的发点
    Public Function GetTrainTimeInGZDandSZ(ByVal nStarTime As Long, ByVal sJiaoLuName As String, ByVal sRunScaleName As String, ByVal sStopScaleName As String, ByVal nUpOrDown As Integer) As Long
        Dim tmpRunTime As Long
        If nUpOrDown = 1 Then
            tmpRunTime = GetCurTrainRunTime(sJiaoLuName, sRunScaleName, sStopScaleName, CDZDrawPara.sDownStartSta, 1)
            If tmpRunTime > 0 Then
                tmpRunTime = tmpRunTime + 6 * 60
            End If
            GetTrainTimeInGZDandSZ = TimeAdd(nStarTime, tmpRunTime)
        Else
            tmpRunTime = GetCurTrainRunTime(sJiaoLuName, sRunScaleName, sStopScaleName, CDZDrawPara.sUpStartSta, 1)
            GetTrainTimeInGZDandSZ = TimeAdd(nStarTime, tmpRunTime)
        End If
    End Function

    '得到当前左边列车出发时刻
    Public Function GetLeftTrainStartTime(ByVal nCurTime As Long, ByVal nUporDown As Integer, ByVal sCurSta As String) As Long
        Dim i, j As Integer
        Dim nMintime As Long
        Dim tmpTime As Long
        GetLeftTrainStartTime = 0
        nMintime = 24 * 3600 + 1
        'Dim sCurSta As String

        'If nUporDown = 1 Then
        '    sCurSta = CDZDrawPara.sDownStartSta
        'Else
        '    sCurSta = CDZDrawPara.sUpStartSta
        'End If
        For i = 1 To UBound(TrainInf)
            If i Mod 2 = nUporDown Mod 2 Then
                If TrainInf(i).Train <> "" Then
                    For j = 1 To UBound(TrainInf(i).nPathID)
                        If StationInf(TrainInf(i).nPathID(j)).sStationName = sCurSta Then
                            tmpTime = AddLitterTime(nCurTime) - AddLitterTime(TrainInf(i).Starting(TrainInf(i).nPathID(j)))
                            If tmpTime > 0 Then
                                If tmpTime < nMintime Then
                                    nMintime = tmpTime
                                    GetLeftTrainStartTime = TrainInf(i).Starting(TrainInf(i).nPathID(j))
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        Next
    End Function

    '得到左边列车在车站的发车时间
    Public Function GetTrainStartTimeInStation(ByVal nTrain As Integer, ByVal nUporDown As Integer) As Long
        Dim i As Integer
        If nUporDown = 1 Then
            For i = 1 To UBound(TrainInf(nTrain).nPathID)
                If StationInf(TrainInf(nTrain).nPathID(i)).sStationName = CDZDrawPara.sDownStartSta Then
                    GetTrainStartTimeInStation = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(i))
                End If
            Next
        Else
            For i = 1 To UBound(TrainInf(nTrain).nPathID)
                If StationInf(TrainInf(nTrain).nPathID(i)).sStationName = CDZDrawPara.sUpStartSta Then
                    GetTrainStartTimeInStation = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(i))
                End If
            Next
        End If
    End Function

    Public Sub CheDiZhuDraw()
        Dim i As Integer
        Dim CdID As Integer
        Dim nFirTrain As Integer
        Dim sSta As String
        'Dim tmpZFTime As Integer
        'Dim tmpZFTime2 As Integer
        Dim tmpTime As Long
        Dim sJiaoLuName As String
        Dim sRunScaleName As String
        Dim sStopScaleName As String
        Dim nTrain As Integer
        Dim nPuHuaSeq() As Integer
        ReDim nPuHuaSeq(UBound(CheDiZhuInfor.nChediID))
        For i = 1 To UBound(CheDiZhuInfor.nChediID)
            nPuHuaSeq(i) = i
        Next
        Dim nCurDrawJianGe As Integer
        nCurDrawJianGe = CDZDrawPara.nCurJianGe
        Dim nLeftTrainStartTime As Long
        Dim tmpJGtime As Integer
        Dim tmpStartTime As Long

        '车底按时间排序列
        Dim K As Integer
        Dim j As Integer
        Dim TempTime1, Temptime2 As Long
        Dim tempId As Integer
        Dim Flag As Integer
        Flag = 1
        K = UBound(nPuHuaSeq)
        Do While Flag > 0
            K = K - 1
            Flag = 0
            For j = 1 To K
                TempTime1 = AddLitterTime(GetTrainTimeInGZDandSZ(CheDiZhuInfor.nStartTime(nPuHuaSeq(j)), _
                                                             CheDiZhuInfor.sNextTrainJiaoLuName(nPuHuaSeq(j)), CheDiZhuInfor.sNextTrainRunScale(nPuHuaSeq(j)), CheDiZhuInfor.sNextTrainStopScale(nPuHuaSeq(j)), CheDiZhuInfor.nUporDown(nPuHuaSeq(j))))
                Temptime2 = AddLitterTime(GetTrainTimeInGZDandSZ(CheDiZhuInfor.nStartTime(nPuHuaSeq(j + 1)), _
                                                            CheDiZhuInfor.sNextTrainJiaoLuName(nPuHuaSeq(j + 1)), CheDiZhuInfor.sNextTrainRunScale(nPuHuaSeq(j + 1)), CheDiZhuInfor.sNextTrainStopScale(nPuHuaSeq(j + 1)), CheDiZhuInfor.nUporDown(nPuHuaSeq(j + 1))))
                If TempTime1 > Temptime2 Then
                    tempId = nPuHuaSeq(j)
                    nPuHuaSeq(j) = nPuHuaSeq(j + 1)
                    nPuHuaSeq(j + 1) = tempId
                    Flag = 1
                End If
            Next j
        Loop

        Dim nDownLeftTrain As Integer
        Dim nUpLeftTrain As Integer
        Dim tmpUporDown As Integer
        Dim tmpLeftTime As Long
        Dim tmpLeftTime1 As Long
        nDownLeftTrain = 0
        nUpLeftTrain = 0
        Dim tmpTime3 As Long
        For i = 1 To UBound(nPuHuaSeq)
            CdID = CheDiZhuInfor.nChediID(nPuHuaSeq(i))
            nFirTrain = ChediInfo(CdID).nLinkTrain(UBound(ChediInfo(CdID).nLinkTrain))
            sJiaoLuName = CheDiZhuInfor.sNextTrainJiaoLuName(nPuHuaSeq(i))
            sRunScaleName = CheDiZhuInfor.sNextTrainRunScale(nPuHuaSeq(i))
            tmpUporDown = CheDiZhuInfor.nUporDown(nPuHuaSeq(i))

            tmpTime = CheDiZhuInfor.nStartTime(nPuHuaSeq(i))
            tmpLeftTime1 = GetLeftTrainStartTime(tmpTime, CheDiZhuInfor.nUporDown(nPuHuaSeq(i)), TrainInf(nFirTrain).NextStation)
            tmpLeftTime = 0
            If tmpUporDown = 1 Then
                If nDownLeftTrain = 0 Then
                Else
                    tmpLeftTime = GetTrainStartTimeInStation(nDownLeftTrain, tmpUporDown)
                End If
            Else
                If nUpLeftTrain = 0 Then

                Else
                    tmpLeftTime = GetTrainStartTimeInStation(nUpLeftTrain, tmpUporDown)
                End If
            End If
            If tmpLeftTime > 0 Then
                If AddLitterTime(tmpLeftTime) - AddLitterTime(tmpLeftTime1) > 0 Then
                    nLeftTrainStartTime = tmpLeftTime
                Else
                    nLeftTrainStartTime = tmpLeftTime1
                End If
            Else
                nLeftTrainStartTime = tmpLeftTime1
            End If

            tmpStartTime = GetTrainTimeInGZDandSZ(CheDiZhuInfor.nStartTime(nPuHuaSeq(j)), _
                                                             CheDiZhuInfor.sNextTrainJiaoLuName(nPuHuaSeq(j)), CheDiZhuInfor.sNextTrainRunScale(nPuHuaSeq(j)), CheDiZhuInfor.sNextTrainStopScale(nPuHuaSeq(j)), CheDiZhuInfor.nUporDown(nPuHuaSeq(j)))
            If nLeftTrainStartTime > 0 Then
                tmpJGtime = AddLitterTime(tmpTime) - AddLitterTime(nLeftTrainStartTime)
                If tmpJGtime < nCurDrawJianGe Then
                    tmpTime = TimeAdd(tmpTime, nCurDrawJianGe - tmpJGtime)
                End If
            End If
            sSta = TrainInf(nFirTrain).NextStation
            sStopScaleName = CheDiZhuInfor.sNextTrainStopScale(nPuHuaSeq(i))  'GetNextStopScaleName(sJiaoLuName, sRunScaleName, tmpTime, CheDiZhuInfor.nUporDown(nPuHuaSeq(i))) 'CheDiZhuInfor.sNextTrainStopScale(nPuHuaSeq(i))
            'sJiaoLuName = GetNewJiaoLuName(sJiaoLuName, sSta, sStopScaleName, sRunScaleName, tmpUporDown, tmpTime)
            nTrain = AddTrainInformation(sJiaoLuName, sRunScaleName, sStopScaleName, "")

            If nTrain > 0 Then
                Call DrawSingleTrain(nTrain, tmpTime, 0)
                Call AddTrainInCheDiLink(CdID, nTrain, 2)
                tmpTime3 = AddLitterTime(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))))
                If tmpTime3 >= CDZDrawPara.nLastDrawTime Then '该车底铺画结束
                    ChediInfo(CdID).bIfGouWang = True
                End If
                If tmpUporDown = 1 Then
                    nDownLeftTrain = nTrain
                Else
                    nUpLeftTrain = nTrain
                End If

                If TimeTablePara.BifAutoBianCheCi = True Then
                    Call ResetPrintTrainString()
                End If
            End If
        Next
    End Sub

    '得到当前交路的反方向的交路名称
    Public Function GetReturnJiaoLuName(ByVal sCurJiaoLuName As String) As String
        GetReturnJiaoLuName = ""
        Dim i As Integer
        Dim sFirSta As String
        Dim sEndSta As String
        sFirSta = ""
        sEndSta = ""
        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName = sCurJiaoLuName Then
                sFirSta = BasicTrainInf(i).ComeStation
                sEndSta = BasicTrainInf(i).NextStation
                Exit For
            End If
        Next

        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).ComeStation = sEndSta And BasicTrainInf(i).NextStation = sFirSta Then
                GetReturnJiaoLuName = BasicTrainInf(i).sJiaoLuName
                Exit For
            End If
        Next

    End Function
End Module
