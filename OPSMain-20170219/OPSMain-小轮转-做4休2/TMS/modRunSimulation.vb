Module modRunSimulation
    Public nTrackPath() As Integer
    Public sBasicTrackPath() As String
    Public sCurControlSecNum As String
    Public nCurSeekDirection As Integer  '搜索方向,1为下行方向，2为上行方向
    Structure typeSimuPara
        Dim sModelChangeTime As Long '模块切换时间
        Dim sStopTime As Long '停站时间
        Dim sCurRunCircle As String '运行曲线
        Dim ifShowLineCheCi As Boolean '是否显示线路模拟列车车次
        Dim ifShowStaCheCi As Boolean '是否显示车站模拟列车车次
    End Structure
    Public SimuPara As typeSimuPara

    Structure TrainRunTrackInformation
        Dim nTrain As Integer
        Dim nTrackID() As TrackIDInformation
    End Structure

    Structure TrackIDInformation
        Dim nStaID As Integer '车站ID
        Dim nTrackID As Integer 'TrackID
        Dim intArriTime As Integer '到达时间
        Dim intStarTime As Integer '出发时间
        Dim intRunTime As Integer '运行时间
        Dim nTrackGroup As Integer '道岔组
        Dim sControlModle As String ' 控制模块
        Dim nOcupyState As Integer
        Dim sSecName As String
        Dim sngSecFirLength As Single
        Dim sngSecEndLength As Single
        Dim nTrain As Integer
        Dim sRunState As String
    End Structure
    Public TrainRunTrack() As TrainRunTrackInformation

    '道岔信息
    Structure TypeCrossData
        Dim CrossStaNum As Integer '车站编号
        Dim sCrossStaName As String '车站名
        Dim sCrossStaStyle As String '车站类型
        Dim CenterAtLine As String '道岔中心点所在的线路编号
        Dim DER() As Integer   '下行方向直行对应的下一位道岔号码，如无用0表示
        Dim IfVisited As Integer  '是否已经访问过
        Dim CrossMemo As String '备注
    End Structure

    Structure typeLinkRoad
        Dim sRoad() As Integer
        Dim nRoadLength As Single
        Dim IfConFind As Integer
        Dim sRunRoad As String
    End Structure
    Public LinkRoad() As typeLinkRoad
    Public RunRoad() As typeLinkRoad
    Structure typeRoad
        Dim sSecName As String
        Dim sFirSta As String
        Dim sSecSta As String
        Dim nRoadLength As Single
        Dim sRunRoad As String
    End Structure
    Public CurRoad As typeRoad

    Structure TypeLinkcross '定义道岔的联接关系类型
        Dim A1 As Integer
        Dim A2 As Integer
        Dim A3 As Integer
        Dim A4 As Integer
    End Structure

    Structure TypeNodeLink '定义道岔树的结点的结构
        Dim Num As Integer '结点顺序值
        Dim Value As String '结点表示道岔号或联结点
        Dim NextNum1 As Integer
        Dim NextNum2 As Integer
        Dim NextNum3 As Integer
        Dim NextNum4 As Integer
        Dim IfLink1 As Integer '是否已经接上
        Dim IfLink2 As Integer
        Dim IfLink3 As Integer
        Dim IfLink4 As Integer
        Dim IfVisited As Integer
    End Structure

    Structure typeBigSecInf
        Dim sSecName As String '大区间名称
        Dim sAtLineName As String '所在线路名
        Dim sSecFirName As String '
        Dim sSecSecName As String
    End Structure

    Structure typeSimuRunInf
        Dim nTrain As Integer
        Dim sRunState As String
        Dim sStaName As String
        Dim sGuiDaoNum As String
        Dim sStopTime As String
        Dim sControlModle As String
    End Structure
    Public SimuRunInf() As typeSimuRunInf

    Public CrossData() As TypeCrossData '将道岔的信息存放在该数组中

    Dim nDER() As Integer


    Public Sub GetDerNum(ByVal nCurLinkRoadID As Integer, ByVal nID As Integer, ByVal nDerec As Integer)
        Dim i As Integer
        Dim j As Integer
        Dim nDnum As Integer
        nDnum = 0
        ReDim nDER(0)
        If nDerec = 2 Then '向右搜索
            If CrossData(nID).DER(2) > 0 Then
                nDnum = nDnum + 1
                ReDim Preserve nDER(UBound(nDER) + 1)
                nDER(UBound(nDER)) = CrossData(nID).DER(2)
            End If

            If CrossData(nID).DER(4) > 0 Then
                nDnum = nDnum + 1
                ReDim Preserve nDER(UBound(nDER) + 1)
                nDER(UBound(nDER)) = CrossData(nID).DER(4)
            End If

            If CrossData(nID).DER(6) > 0 Then
                nDnum = nDnum + 1
                ReDim Preserve nDER(UBound(nDER) + 1)
                nDER(UBound(nDER)) = CrossData(nID).DER(6)
            End If

        Else

            If CrossData(nID).DER(1) > 0 Then
                nDnum = nDnum + 1
                ReDim Preserve nDER(UBound(nDER) + 1)
                nDER(UBound(nDER)) = CrossData(nID).DER(1)
            End If

            If CrossData(nID).DER(3) > 0 Then
                nDnum = nDnum + 1
                ReDim Preserve nDER(UBound(nDER) + 1)
                nDER(UBound(nDER)) = CrossData(nID).DER(3)
            End If

            If CrossData(nID).DER(5) > 0 Then
                nDnum = nDnum + 1
                ReDim Preserve nDER(UBound(nDER) + 1)
                nDER(UBound(nDER)) = CrossData(nID).DER(5)
            End If
        End If

        If nDnum = 0 Then
            LinkRoad(nCurLinkRoadID).IfConFind = -1
        Else
            Dim nNu As Integer

            For i = 1 To nDnum - 1

                nNu = UBound(LinkRoad) + 1
                ReDim Preserve LinkRoad(nNu)
                ReDim Preserve LinkRoad(nNu).sRoad(UBound(LinkRoad(nCurLinkRoadID).sRoad) + 1)
                For j = 1 To UBound(LinkRoad(nCurLinkRoadID).sRoad)
                    LinkRoad(nNu).sRoad(j) = LinkRoad(nCurLinkRoadID).sRoad(j)
                Next
                For j = 1 To UBound(LinkRoad(nNu).sRoad)
                    If LinkRoad(nNu).sRoad(j) = nDER(i + 1) Then
                        LinkRoad(nNu).IfConFind = -1
                        Exit For
                    End If
                Next
                If LinkRoad(nNu).IfConFind <> -1 Then
                    LinkRoad(nNu).sRoad(UBound(LinkRoad(nNu).sRoad)) = nDER(i + 1)
                End If
            Next

            For j = 1 To UBound(LinkRoad(nCurLinkRoadID).sRoad)
                If LinkRoad(nCurLinkRoadID).sRoad(j) = nDER(1) Then
                    LinkRoad(nCurLinkRoadID).IfConFind = -1
                    Exit For
                End If
            Next
            If LinkRoad(nCurLinkRoadID).IfConFind <> -1 Then
                ReDim Preserve LinkRoad(nCurLinkRoadID).sRoad(UBound(LinkRoad(nCurLinkRoadID).sRoad) + 1)
                LinkRoad(nCurLinkRoadID).sRoad(UBound(LinkRoad(nCurLinkRoadID).sRoad)) = nDER(1)
            End If
        End If
        Dim nCurID As Integer
        nCurID = nID

    End Sub

    
    '是否找到一个
    Public Sub IFFindSecID(ByVal nSecID As Integer)
        Dim i As Integer
        For i = 1 To UBound(LinkRoad)
            If LinkRoad(i).sRoad(UBound(LinkRoad(i).sRoad)) = nSecID Then
                LinkRoad(i).IfConFind = -1
            End If
        Next i
    End Sub
    '是否结束
    Public Function IfSeekEnd() As Boolean
        Dim i As Integer
        IfSeekEnd = True
        For i = 1 To UBound(LinkRoad)
            If LinkRoad(i).IfConFind <> -1 Then
                IfSeekEnd = False
                Exit For
            End If
        Next
    End Function

    '找径路lic
    Public Sub SeekRoadFromID(ByVal nUporDown As Integer, ByVal nStaID As Integer, ByVal nFirID As Integer, ByVal nSecID As Integer)
        ReDim nTrackPath(0)

        If nFirID = nSecID Then
            ReDim nTrackPath(1)
            nTrackPath(1) = nFirID
            Exit Sub
        End If
        Dim i As Integer
        Dim j As Integer
        Dim numDer As Integer
        Dim nSeekID As Integer
        Dim sSeekDerctor As String
        sSeekDerctor = 0
        If nUporDown = 1 Then '下行 then
            sSeekDerctor = 1 '向左搜索
        Else '上行
            sSeekDerctor = 2 '向右搜索
        End If
        'If nSeekLeftOrRight = "右" Then

        'ElseIf nSeekLeftOrRight = "左" Then

        'Else
        '    'Stop
        'End If
        nSeekID = nFirID
        ReDim nDER(1)
        nDER(1) = nFirID
        Dim nCurLinkRoadID As Integer
        ReDim LinkRoad(0)
        ReDim LinkRoad(1)
        ReDim LinkRoad(1).sRoad(1)
        LinkRoad(1).sRoad(1) = nFirID
        nCurLinkRoadID = 1
        numDer = 1
        Do
            For i = 1 To numDer
                nCurLinkRoadID = i
                nSeekID = LinkRoad(i).sRoad(UBound(LinkRoad(i).sRoad))
                If LinkRoad(nCurLinkRoadID).IfConFind <> -1 Then
                    Call GetDerNum(nCurLinkRoadID, nSeekID, sSeekDerctor)
                End If
                Call IFFindSecID(nSecID)
                If IfSeekEnd() = True Then
                    Exit Do
                End If
                numDer = UBound(LinkRoad)
            Next
        Loop

        Dim sM As String
        ReDim RunRoad(0)
        Dim nMinNum As Integer
        Dim nMinID As Integer
        nMinID = 0
        nMinNum = 100000
        For i = 1 To UBound(LinkRoad)
            sM = ""
            If CrossData(LinkRoad(i).sRoad(UBound(LinkRoad(i).sRoad))).sCrossStaName = CrossData(nSecID).sCrossStaName Then
                ReDim Preserve RunRoad(UBound(RunRoad) + 1)
                ReDim RunRoad(UBound(RunRoad)).sRoad(UBound(LinkRoad(i).sRoad))
                For j = 1 To UBound(LinkRoad(i).sRoad)
                    RunRoad(UBound(RunRoad)).sRoad(j) = LinkRoad(i).sRoad(j)
                    ' MsgBox(LinkRoad(i).sRoad(UBound(LinkRoad(i).sRoad)))
                    sM = sM & "-" & CrossData(LinkRoad(i).sRoad(j)).sCrossStaName
                Next j
                RunRoad(UBound(RunRoad)).sRunRoad = sM
                If UBound(LinkRoad(i).sRoad) < nMinNum Then
                    nMinNum = UBound(LinkRoad(i).sRoad)
                    nMinID = i
                End If
                'MsgBox(sM)
            End If
        Next
        sM = ""
        If nMinID > 0 Then
            ReDim nTrackPath(UBound(LinkRoad(nMinID).sRoad))
            For j = 1 To UBound(LinkRoad(nMinID).sRoad)
                sM = sM & "-" & CrossData(LinkRoad(nMinID).sRoad(j)).sCrossStaName
                nTrackPath(j) = LinkRoad(nMinID).sRoad(j)
            Next
            'MsgBox(sM)
        Else
            'MsgBox("没有找到，请检查连接信息是否有错或者搜索条件有误！")
            'Stop
        End If
    End Sub

    '找区间径路lic
    Public Sub SeekRoadFromIDInSection(ByVal nStaID As Integer, ByVal nFirID As Integer, ByVal nSecID As Integer, ByVal nFirSta As Integer, ByVal nSecSta As Integer)
        ReDim nTrackPath(0)

        If nFirID = nSecID Then
            ReDim nTrackPath(1)
            nTrackPath(1) = nFirID
            Exit Sub
        End If
        Dim i As Integer
        Dim j As Integer
        Dim numDer As Integer
        Dim nSeekID As Integer

        nSeekID = nFirID
        ReDim nDER(1)
        nDER(1) = nFirID
        Dim nCurLinkRoadID As Integer
        ReDim LinkRoad(0)
        ReDim LinkRoad(1)
        ReDim LinkRoad(1).sRoad(1)
        LinkRoad(1).sRoad(1) = nFirID
        nCurLinkRoadID = 1
        numDer = 1
        Dim nNextID As Integer
        Dim nForeID As Integer
        Dim sCurTrackNum As String
        Dim nCurID As Integer
        nCurID = nFirID
        nForeID = nFirID
        Do
            sCurTrackNum = CADStaInf(nStaID).Track(nCurID).sTrackCircuitNum
            nNextID = 0
            For i = 1 To UBound(CADStaInf(nStaID).Track)
                If i <> nForeID And i <> nCurID Then
                    If CADStaInf(nStaID).Track(i).sRightLink1 = sCurTrackNum Or CADStaInf(nStaID).Track(i).sRightLink2 = sCurTrackNum Or CADStaInf(nStaID).Track(i).sRightLink3 = sCurTrackNum _
                                    Or CADStaInf(nStaID).Track(i).sLeftLink1 = sCurTrackNum Or CADStaInf(nStaID).Track(i).sLeftLink2 = sCurTrackNum Or CADStaInf(nStaID).Track(i).sLeftLink3 = sCurTrackNum Then
                        nNextID = i
                        Exit For
                    End If
                End If
            Next

            If nNextID > 0 Then
                ReDim Preserve LinkRoad(1).sRoad(UBound(LinkRoad(1).sRoad) + 1)
                LinkRoad(1).sRoad(UBound(LinkRoad(1).sRoad)) = nNextID
            Else
                Exit Do
            End If
            nForeID = nCurID
            nCurID = nNextID
            If nNextID = nSecID Then
                Exit Do
            End If
        Loop

        Dim sM As String
        ReDim RunRoad(0)
        Dim nMinNum As Integer
        Dim nMinID As Integer
        nMinID = 0
        nMinNum = 100000
        For i = 1 To UBound(LinkRoad)
            sM = ""
            ReDim Preserve RunRoad(UBound(RunRoad) + 1)
            ReDim RunRoad(UBound(RunRoad)).sRoad(UBound(LinkRoad(i).sRoad))
            For j = 1 To UBound(LinkRoad(i).sRoad)
                RunRoad(UBound(RunRoad)).sRoad(j) = LinkRoad(i).sRoad(j)
                ' MsgBox(LinkRoad(i).sRoad(UBound(LinkRoad(i).sRoad)))
                ' sM = sM & "-" & CrossData(LinkRoad(i).sRoad(j)).sCrossStaName
            Next j
            RunRoad(UBound(RunRoad)).sRunRoad = sM
            If UBound(LinkRoad(i).sRoad) < nMinNum Then
                nMinNum = UBound(LinkRoad(i).sRoad)
                nMinID = i
            End If
            'MsgBox(sM)

        Next
        sM = ""
        If nMinID > 0 Then
            ReDim nTrackPath(UBound(LinkRoad(nMinID).sRoad))
            For j = 1 To UBound(LinkRoad(nMinID).sRoad)
                'sM = sM & "-" & CrossData(LinkRoad(nMinID).sRoad(j)).sCrossStaName
                nTrackPath(j) = LinkRoad(nMinID).sRoad(j)
            Next
            'MsgBox(sM)
        Else
            'MsgBox("没有找到，请检查连接信息是否有错或者搜索条件有误！")
            'Stop
        End If
    End Sub

   
    '将路网结点赋入
    Public Sub InputCrossData(ByVal nStaId As Integer)
        ReDim CrossData(UBound(CADStaInf(nStaId).Track))
        Dim i As Integer
        For i = 1 To UBound(CrossData)
            ReDim CrossData(i).DER(6)
        Next

        For i = 1 To UBound(CADStaInf(nStaId).Track)
            CrossData(i).DER(1) = GetTrackIDFromLinkInfor(CADStaInf(nStaId).Track(i).sLeftLink1, nStaId)
            CrossData(i).DER(3) = GetTrackIDFromLinkInfor(CADStaInf(nStaId).Track(i).sLeftLink2, nStaId)
            CrossData(i).DER(5) = GetTrackIDFromLinkInfor(CADStaInf(nStaId).Track(i).sLeftLink3, nStaId)
            CrossData(i).DER(2) = GetTrackIDFromLinkInfor(CADStaInf(nStaId).Track(i).sRightLink1, nStaId)
            CrossData(i).DER(4) = GetTrackIDFromLinkInfor(CADStaInf(nStaId).Track(i).sRightLink2, nStaId)
            CrossData(i).DER(6) = GetTrackIDFromLinkInfor(CADStaInf(nStaId).Track(i).sRightLink3, nStaId)
            CrossData(i).sCrossStaName = CADStaInf(nStaId).Track(i).sTrackCircuitNum
        Next i
    End Sub
    Public Function GetTrackIDFromLinkInfor(ByVal sNum As String, ByVal nStaID As Integer) As Integer
        Dim i As Integer
        GetTrackIDFromLinkInfor = 0
        If sNum = "" Or sNum = "NULL" Then
            GetTrackIDFromLinkInfor = 0
            Exit Function
        End If
        For i = 1 To UBound(CADStaInf(nStaID).Track)
            If CADStaInf(nStaID).Track(i).sTrackCircuitNum = sNum Then
                GetTrackIDFromLinkInfor = i
                Exit For
            End If
        Next
    End Function

    '车站折返股道电路搜索
    Public Sub SeekStationReturnTrackID(ByVal ID As Integer, ByVal nSta As Integer, _
                ByVal sFirGudaoNum As String, ByVal sSecGuDaoNum As String, _
                ByVal intFirTime As Integer, ByVal intSecTime As Integer, ByVal intFirStopTime As Integer, ByVal intSecStopTime As Integer)
        Dim i As Integer
        Dim nID1 As Integer
        Dim nID2 As Integer
        Dim Str As String
        Dim nTID As Integer
        Dim nTID2 As Integer
        nTID = UBound(TrainRunTrack(ID).nTrackID)
        nID1 = GetTrackNumFromGuDaoNum(nSta, sFirGudaoNum)
        nID2 = GetTrackNumFromGuDaoNum(nSta, sSecGuDaoNum)
        Dim nUporDown As Integer
        nUporDown = nDirection(ID)
        If nID1 > 0 And nID2 > 0 And nID1 <> nID2 Then
            ReDim nTrackPath(0)
            Call InputCrossData(nSta)
            Call SeekRoadFromID(nUporDown, nSta, nID1, nID2)
            Str = ""
            For i = 1 To UBound(nTrackPath)
                ReDim Preserve TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID) + 1)
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nStaID = nSta
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nTrain = ID
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nTrackID = nTrackPath(i)
                Str = Str & "-" & CADStaInf(nSta).Track(nTrackPath(i)).sTrackCircuitNum
            Next
        End If

        '分配时间，时间均分
        Dim sngEveryTime As Long
        Dim tmpK As Integer
        tmpK = 1
        nTID2 = UBound(TrainRunTrack(ID).nTrackID)
        If nTID2 - nTID > 2 Then
            sngEveryTime = TimeMinus(intSecTime, intFirTime) / (nTID2 - nTID - 2)
            For i = nTID + 1 To nTID2
                'nStaID = TrainRunTrack(ID).nTrackID(i).nStaID
                'nTrackID = TrainRunTrack(ID).nTrackID(i).nTrackID
                If i = nTID + 1 Then
                    TrainRunTrack(ID).nTrackID(i).intStarTime = intFirTime
                    TrainRunTrack(ID).nTrackID(i).intArriTime = TimeMinus(intFirTime, intFirStopTime)
                ElseIf i = nTID2 Then
                    TrainRunTrack(ID).nTrackID(i).intStarTime = TimeAdd(intSecTime, intSecStopTime)
                    TrainRunTrack(ID).nTrackID(i).intArriTime = intSecTime
                Else
                    TrainRunTrack(ID).nTrackID(i).intStarTime = intFirTime + tmpK * sngEveryTime
                    TrainRunTrack(ID).nTrackID(i).intArriTime = intFirTime + (tmpK - 1) * sngEveryTime
                    tmpK = tmpK + 1
                End If
            Next
        ElseIf nTID2 - nTID = 1 Then
            TrainRunTrack(ID).nTrackID(nTID + 1).intStarTime = TimeAdd(intSecTime, intSecStopTime)
            TrainRunTrack(ID).nTrackID(nTID + 1).intArriTime = TimeMinus(intFirTime, intFirStopTime)
        ElseIf nTID2 - nTID = 2 Then
            TrainRunTrack(ID).nTrackID(nTID + 1).intStarTime = intSecTime
            TrainRunTrack(ID).nTrackID(nTID + 1).intArriTime = TimeMinus(intFirTime, intFirStopTime)
            TrainRunTrack(ID).nTrackID(nTID + 2).intStarTime = TimeAdd(intFirTime, intSecStopTime)
            TrainRunTrack(ID).nTrackID(nTID + 2).intArriTime = intSecTime
        End If

        'MsgBox(Str)
    End Sub

    '区间轨道电路搜索
    Public Sub SeekSectionTrackID(ByVal nUporDown As Integer, ByVal ID As Integer, ByVal nFirSta As Integer, _
                ByVal nSecSta As Integer, ByVal nSecID As Integer, ByVal sFirTrackNum As String, ByVal nSecTrackNum As String, _
                ByVal intFirTime As Integer, ByVal intSecTime As Integer, ByVal nCurSecID As Integer, ByVal intFirStopTime As Integer, ByVal intSecstopTime As Integer, ByVal sSecName As String)
        Dim i As Integer
        'Dim j As Integer
        Dim sFirA As String
        Dim sSecA As String
        Dim sSecAB As String
        Dim nID1 As Integer
        Dim nID2 As Integer
        Dim Str As String
        Dim nTID As Integer
        Dim nTID2 As Integer
        nTID = UBound(TrainRunTrack(ID).nTrackID)
        sFirA = CADStaInf(nFirSta).sStaCode
        sSecA = CADStaInf(nSecSta).sStaCode
        sSecAB = CADStaInf(nSecID).sStaCode
        nID1 = GetTrackNumFromGuDaoNum(nFirSta, sFirTrackNum)
        nID2 = GetTrackNumFromStaCode(nUporDown, nFirSta, sSecAB, "出站")
        Dim sngLength As Single
        sngLength = 0

        If nID1 > 0 And nID2 > 0 Then
            ReDim nTrackPath(0)
            Call InputCrossData(nFirSta)
            Call SeekRoadFromID(nUporDown, nFirSta, nID1, nID2)
            Str = ""
            If nCurSecID = 1 And UBound(TrainRunTrack(ID).nTrackID) = 0 Then
                For i = 1 To UBound(nTrackPath)
                    ReDim Preserve TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID) + 1)
                    TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nStaID = nFirSta
                    TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).sSecName = sSecName
                    TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nTrackID = nTrackPath(i)
                    TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nTrain = ID
                    TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).sngSecFirLength = sngLength
                    sngLength = sngLength + CADStaInf(nFirSta).Track(nTrackPath(i)).sngLength
                    TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).sngSecEndLength = sngLength
                    Str = Str & "-" & CADStaInf(nFirSta).Track(nTrackPath(i)).sTrackCircuitNum
                Next
            Else
                For i = 2 To UBound(nTrackPath)
                    ReDim Preserve TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID) + 1)
                    TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nStaID = nFirSta
                    TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).sSecName = sSecName
                    TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nTrackID = nTrackPath(i)
                    TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nTrain = ID
                    TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).sngSecFirLength = sngLength
                    sngLength = sngLength + CADStaInf(nFirSta).Track(nTrackPath(i)).sngLength
                    TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).sngSecEndLength = sngLength
                    Str = Str & "-" & CADStaInf(nFirSta).Track(nTrackPath(i)).sTrackCircuitNum
                Next

        End If
        'MsgBox(Str)

        End If

        nID1 = GetTrackNumFromSecFirStaCode(nSecID, sFirA, nUporDown)
        nID2 = GetTrackNumFromSecSecStaCode(nSecID, sSecA, nUporDown)

        If nID1 > 0 And nID2 > 0 Then
            ReDim nTrackPath(0)
            Call InputCrossData(nSecID)
            Call SeekRoadFromID(nUporDown, nSecID, nID1, nID2)
            Str = ""
            For i = 1 To UBound(nTrackPath)
                ReDim Preserve TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID) + 1)
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nStaID = nSecID
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).sSecName = sSecName
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nTrackID = nTrackPath(i)
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nTrain = ID
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).sngSecFirLength = sngLength
                sngLength = sngLength + CADStaInf(nSecID).Track(nTrackPath(i)).sngLength
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).sngSecEndLength = sngLength
                Str = Str & "-" & CADStaInf(nSecID).Track(nTrackPath(i)).sTrackCircuitNum
            Next
            'MsgBox(Str)
        End If


        nID1 = GetTrackNumFromStaCode(nUporDown, nSecSta, sSecAB, "进站")
        nID2 = GetTrackNumFromGuDaoNum(nSecSta, nSecTrackNum)
        If nID1 > 0 And nID2 > 0 Then
            ReDim nTrackPath(0)
            Call InputCrossData(nSecSta)
            Call SeekRoadFromID(nUporDown, nSecSta, nID1, nID2)
            Str = ""
            For i = 1 To UBound(nTrackPath)
                ReDim Preserve TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID) + 1)
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nStaID = nSecSta
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).sSecName = sSecName
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nTrain = ID
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nTrackID = nTrackPath(i)
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).sngSecFirLength = sngLength
                sngLength = sngLength + CADStaInf(nSecSta).Track(nTrackPath(i)).sngLength
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).sngSecEndLength = sngLength
                Str = Str & "-" & CADStaInf(nSecSta).Track(nTrackPath(i)).sTrackCircuitNum
            Next
            'MsgBox(Str)
        End If

        Str = ""
        For i = 1 To UBound(TrainRunTrack(ID).nTrackID)
            Str = Str & "," & CADStaInf(TrainRunTrack(ID).nTrackID(i).nStaID).Track(TrainRunTrack(ID).nTrackID(i).nTrackID).sTrackCircuitNum
        Next
        If nCurSecID = UBound(TrainInf(ID).nPassSection) Then
            'MsgBox(Str)
            'Stop
        End If

        '分配时间，根据牵引计算分
        '  Dim sngEveryTime As Long
        Dim tmpK As Integer
        Dim tmpTime As Single
        sngLength = 0
        tmpK = 1
        tmpTime = 0
        nTID2 = UBound(TrainRunTrack(ID).nTrackID)
        If nTID2 - nTID > 2 Then
            For i = nTID + 1 To nTID2
                If i = nTID + 1 Then
                    sngLength = CADStaInf(TrainRunTrack(ID).nTrackID(i).nStaID).Track(TrainRunTrack(ID).nTrackID(i).nTrackID).sngLength / 2
                    TrainRunTrack(ID).nTrackID(i).intArriTime = intFirTime - intFirStopTime
                    tmpTime = intFirTime + GetTrackRunTimeFormSecRunCircle(sngLength, CADStaInf(nSecID).sStaName, nUporDown)
                    TrainRunTrack(ID).nTrackID(i).intStarTime = tmpTime
                ElseIf i = nTID2 Then
                    TrainRunTrack(ID).nTrackID(i).intArriTime = tmpTime
                    TrainRunTrack(ID).nTrackID(i).intStarTime = intSecTime + intSecstopTime + 5
                Else
                    sngLength = sngLength + CADStaInf(TrainRunTrack(ID).nTrackID(i).nStaID).Track(TrainRunTrack(ID).nTrackID(i).nTrackID).sngLength
                    TrainRunTrack(ID).nTrackID(i).intArriTime = tmpTime
                    tmpTime = intFirTime + GetTrackRunTimeFormSecRunCircle(sngLength, CADStaInf(nSecID).sStaName, nUporDown)
                    TrainRunTrack(ID).nTrackID(i).intStarTime = tmpTime
                End If
                tmpK = tmpK + 1
            Next
        End If


        ''分配时间，时间均分
        'Dim sngEveryTime As Long
        'Dim tmpK As Integer
        'tmpK = 1
        'nTID2 = UBound(TrainRunTrack(ID).nTrackID)
        'If nTID2 - nTID > 2 Then
        '    sngEveryTime = TimeMinus(intSecTime, intFirTime) / (nTID2 - nTID - 2)
        '    For i = nTID + 1 To nTID2
        '        If i = nTID + 1 Then
        '            TrainRunTrack(ID).nTrackID(i).intStarTime = intFirTime + 5
        '            TrainRunTrack(ID).nTrackID(i).intArriTime = intFirTime - intFirStopTime - 5
        '        ElseIf i = nTID2 Then
        '            TrainRunTrack(ID).nTrackID(i).intStarTime = intSecTime + intSecstopTime + 5
        '            TrainRunTrack(ID).nTrackID(i).intArriTime = intSecTime - 5
        '        Else
        '            TrainRunTrack(ID).nTrackID(i).intStarTime = intFirTime + (tmpK - 1) * sngEveryTime + 5
        '            TrainRunTrack(ID).nTrackID(i).intArriTime = intFirTime + (tmpK - 2) * sngEveryTime - 5
        '        End If
        '        tmpK = tmpK + 1
        '    Next
        'End If

    End Sub

    '计算轨道的运行时间
    Public Function GetTrackRunTimeFormLength(ByVal sngFirLength As Single, ByVal sngSecLength As Single, ByVal sSecName As String, ByVal nUporDown As Single) As Long
        'Dim tmpTime1 As Long
        'Dim tmpTime2 As Long
        'tmpTime1 = GetTrackRunTimeFormSecRunCircle(sngFirLength, sSecName, nUporDown)
        'tmpTime2 = GetTrackRunTimeFormSecRunCircle(sngSecLength, sSecName, nUporDown)
        GetTrackRunTimeFormLength = (sngSecLength - sngFirLength) * 3600 / 130 'tmpTime2 - tmpTime1
    End Function

    '根据运行曲线得到运行时分
    Public Function GetTrackRunTimeFormSecRunCircle(ByVal sngCurLength As Single, ByVal sSecName As String, ByVal nUpOrDown As Integer) As Long
        Dim i, j As Integer
        Dim sngLength As Single
        Dim sngLength2 As Single
        sngLength = 0
        sngLength2 = 0
        Dim sngTime As Single
        sngTime = 0
        If nUporDown = 1 Then '"下行"
            For i = 1 To UBound(SectionInf)
                If SectionInf(i).sSecName = sSecName Then
                    For j = 1 To UBound(SectionInf(i).SecTimeSpace.DownID)
                        sngLength2 = sngLength
                        sngLength = sngLength + SectionInf(i).SecTimeSpace.DownLength(j)
                        If sngCurLength >= sngLength2 And sngCurLength <= sngLength Then
                            GetTrackRunTimeFormSecRunCircle = sngTime + (sngCurLength - sngLength2) * SectionInf(i).SecTimeSpace.DownRunTime(j) / SectionInf(i).SecTimeSpace.DownLength(j)
                            Exit For
                        End If
                        sngTime = sngTime + SectionInf(i).SecTimeSpace.DownRunTime(j)
                    Next
                    If GetTrackRunTimeFormSecRunCircle = 0 Then
                        GetTrackRunTimeFormSecRunCircle = sngTime
                    End If
                    Exit For
                End If
            Next
        Else '上行
            For i = 1 To UBound(SectionInf)
                If SectionInf(i).sSecName = sSecName Then
                    For j = 1 To UBound(SectionInf(i).SecTimeSpace.UpID)
                        sngLength2 = sngLength
                        sngLength = sngLength + SectionInf(i).SecTimeSpace.UpLength(j)
                        If sngCurLength >= sngLength2 And sngCurLength <= sngLength Then
                            GetTrackRunTimeFormSecRunCircle = sngTime + (sngCurLength - sngLength2) * SectionInf(i).SecTimeSpace.UpRunTime(j) / SectionInf(i).SecTimeSpace.UpLength(j)
                            Exit For
                        End If
                        sngTime = sngTime + SectionInf(i).SecTimeSpace.UpRunTime(j)
                    Next
                    If GetTrackRunTimeFormSecRunCircle = 0 Then
                        GetTrackRunTimeFormSecRunCircle = sngTime
                    End If
                    Exit For
                End If
            Next

        End If
    End Function



    '由区间终到站编号得到进站的TRACKID
    Public Function GetTrackNumFromSecSecStaCode(ByVal nStaID As Integer, ByVal sStaCode As String, ByVal nUpOrDown As Integer) As Integer
        GetTrackNumFromSecSecStaCode = 0
        Dim i As Integer
        Dim sStyle As String
        For i = 1 To UBound(CADStaInf(nStaID).Track)
            sStyle = CADStaInf(nStaID).Track(i).sGuDaoYongTu
            If nUpOrDown = 2 Then
                If sStyle = "只能上行" Or sStyle = "双方向" Or sStyle = "主要方向为上行" Then
                    If GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink1) = sStaCode Or _
                                        GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink2) = sStaCode Or _
                                        GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink3) = sStaCode Then
                        GetTrackNumFromSecSecStaCode = i
                        Exit For
                    ElseIf GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink1) = sStaCode Or _
                                    GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink2) = sStaCode Or _
                                    GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink3) = sStaCode Then
                        GetTrackNumFromSecSecStaCode = i
                        Exit For
                    End If
                End If

            Else
                If sStyle = "只能下行" Or sStyle = "双方向" Or sStyle = "主要方向为下行" Then
                    If GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink1) = sStaCode Or _
                                                 GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink2) = sStaCode Or _
                                                 GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink3) = sStaCode Then
                        GetTrackNumFromSecSecStaCode = i
                        Exit For
                    ElseIf GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink1) = sStaCode Or _
                                             GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink2) = sStaCode Or _
                                             GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink3) = sStaCode Then
                        GetTrackNumFromSecSecStaCode = i
                        Exit For
                    End If
                End If
            End If
        Next
    End Function

    '由区间始发站编号得到进站的TRACKID
    Public Function GetTrackNumFromSecFirStaCode(ByVal nStaID As Integer, ByVal sStaCode As String, ByVal nUpOrDown As Integer) As Integer
        GetTrackNumFromSecFirStaCode = 0
        Dim i As Integer
        Dim sStyle As String
        For i = 1 To UBound(CADStaInf(nStaID).Track)
            sStyle = CADStaInf(nStaID).Track(i).sGuDaoYongTu
            If nUpOrDown = 2 Then
                If sStyle = "只能上行" Or sStyle = "双方向" Or sStyle = "主要方向为上行" Then
                    If GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink1) = sStaCode Or _
                         GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink2) = sStaCode Or _
                            GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink3) = sStaCode Then
                        GetTrackNumFromSecFirStaCode = i
                        Exit For
                    ElseIf GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink1) = sStaCode Or _
                                GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink2) = sStaCode Or _
                                GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink3) = sStaCode Then
                        GetTrackNumFromSecFirStaCode = i
                        Exit For
                    End If
                End If
            Else
                If sStyle = "只能下行" Or sStyle = "双方向" Or sStyle = "主要方向为下行" Then
                    If GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink1) = sStaCode Or _
                    GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink2) = sStaCode Or _
                    GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink3) = sStaCode Then
                        GetTrackNumFromSecFirStaCode = i
                        Exit For
                    ElseIf GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink1) = sStaCode Or _
                    GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink2) = sStaCode Or _
                                GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink3) = sStaCode Then
                        GetTrackNumFromSecFirStaCode = i
                        Exit For
                    End If
                End If
            End If
        Next

    End Function

    '由车站股道编号得到出站的TRACKID
    Public Function GetTrackNumFromGuDaoNum(ByVal nStaID As Integer, ByVal sNum As String) As Integer
        Dim i As Integer
        GetTrackNumFromGuDaoNum = 0
        Dim sStyle As String

        For i = 1 To UBound(CADStaInf(nStaID).Track)
            sStyle = CADStaInf(nStaID).Track(i).sStyle
            If sStyle.Length >= 2 Then
                If sStyle = "股道线" Then
                    If CADStaInf(nStaID).Track(i).sTrackNum = sNum Then
                        GetTrackNumFromGuDaoNum = i
                        Exit For
                    End If
                End If
            End If
        Next
    End Function

    '由车站代码得到出站的TRACKID
    Public Function GetTrackNumFromStaCode(ByVal nUpOrDown As Integer, ByVal nStaID As Integer, ByVal sCode As String, ByVal EnterOrLeave As String) As Integer
        Dim i As Integer
        GetTrackNumFromStaCode = 0
        For i = 1 To UBound(CADStaInf(nStaID).Track)
            '    If EnterOrLeave = "出站" Then
            If nUpOrDown = 2 Then
                If CADStaInf(nStaID).Track(i).sGuDaoYongTu = "只能上行" Or CADStaInf(nStaID).Track(i).sGuDaoYongTu = "双方向" Or CADStaInf(nStaID).Track(i).sGuDaoYongTu = "主要方向为上行" Then
                    If GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink1) = sCode Or GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink2) = sCode Or GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink3) = sCode Then
                        GetTrackNumFromStaCode = i
                        If EnterOrLeave = "进站" Then
                            nCurSeekDirection = 1
                        Else
                            nCurSeekDirection = 2
                        End If
                        Exit For
                    ElseIf GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink1) = sCode Or GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink2) = sCode Or GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink3) = sCode Then
                        GetTrackNumFromStaCode = i
                        If EnterOrLeave = "进站" Then
                            nCurSeekDirection = 2
                        Else
                            nCurSeekDirection = 1
                        End If
                        Exit For
                    End If
                End If
            Else
                If CADStaInf(nStaID).Track(i).sGuDaoYongTu = "只能下行" Or CADStaInf(nStaID).Track(i).sGuDaoYongTu = "双方向" Or CADStaInf(nStaID).Track(i).sGuDaoYongTu = "主要方向为下行" Then
                    If GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink1) = sCode Or GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink2) = sCode Or GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink3) = sCode Then
                        GetTrackNumFromStaCode = i
                        If EnterOrLeave = "进站" Then
                            nCurSeekDirection = 2
                        Else
                            nCurSeekDirection = 1
                        End If
                        Exit For
                    ElseIf GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink1) = sCode Or GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink2) = sCode Or GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink3) = sCode Then
                        GetTrackNumFromStaCode = i
                        If EnterOrLeave = "进站" Then
                            nCurSeekDirection = 1
                        Else
                            nCurSeekDirection = 2
                        End If

                        Exit For
                    End If
                End If
            End If
            'Else
            '' If CADStaInf(nStaID).Track(i).sStyle.Length >= 2 Then
            'If nUpOrDown = 2 Then
            '    If CADStaInf(nStaID).Track(i).sGuDaoYongTu = "只能上行" Or CADStaInf(nStaID).Track(i).sGuDaoYongTu = "双方向" Or CADStaInf(nStaID).Track(i).sGuDaoYongTu = "主要方向为上行" Then
            '        If GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink1) = sCode Or GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink2) = sCode Or GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink3) = sCode Then
            '            GetTrackNumFromStaCode = i
            '            Exit For
            '        End If
            '    End If
            'Else
            '    If CADStaInf(nStaID).Track(i).sGuDaoYongTu = "只能下行" Or CADStaInf(nStaID).Track(i).sGuDaoYongTu = "双方向" Or CADStaInf(nStaID).Track(i).sGuDaoYongTu = "主要方向为下行" Then
            '        If GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink1) = sCode Or GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink2) = sCode Or GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink3) = sCode Then
            '            GetTrackNumFromStaCode = i
            '            Exit For
            '        End If
            '    End If
            'End If
            'End If
            ' End If

        Next
    End Function

    '折返时，根据上下行列车得到右边进站轨道电路ID和出站轨道电路的ID
    Public Function GetGuiDaoTrackIDFromUpOrDown(ByVal nStaID As Integer, ByVal nUpOrDown As Integer) As Integer
        GetGuiDaoTrackIDFromUpOrDown = 0
        Dim i As Integer
        Dim sStyle As String
        If nUpOrDown = 1 Then '下行
            For i = 1 To UBound(CADStaInf(nStaID).Track)
                sStyle = GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink1)
                If sStyle <> CADStaInf(nStaID).sStaCode Then
                    If CADStaInf(nStaID).Track(i).sGuDaoYongTu = "只能下行" Then
                        GetGuiDaoTrackIDFromUpOrDown = i
                        Exit For
                    End If
                End If

            Next
        Else
            For i = 1 To UBound(CADStaInf(nStaID).Track)
                sStyle = GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink1)
                If sStyle <> CADStaInf(nStaID).sStaCode Then
                    If CADStaInf(nStaID).Track(i).sGuDaoYongTu = "只能上行" Then
                        GetGuiDaoTrackIDFromUpOrDown = i
                        Exit For
                    End If
                End If
            Next
        End If

    End Function

    '折返时，根据上下行列车得到左边进站轨道电路ID和出站轨道电路的ID
    Public Function GetLeftGuiDaoTrackIDFromUpOrDown(ByVal nStaID As Integer, ByVal nUpOrDown As Integer) As Integer
        GetLeftGuiDaoTrackIDFromUpOrDown = 0
        Dim i As Integer
        Dim sStyle As String
        Dim sStyle1 As String
        If nUpOrDown = 1 Then '下行
            For i = 1 To UBound(CADStaInf(nStaID).Track)
                sStyle = GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink1)
                sStyle1 = GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink1)
                If sStyle <> CADStaInf(nStaID).sStaCode Or sStyle1 <> CADStaInf(nStaID).sStaCode Then
                    If CADStaInf(nStaID).Track(i).sGuDaoYongTu = "只能下行" Then
                        GetLeftGuiDaoTrackIDFromUpOrDown = i
                        Exit For
                    End If
                End If

            Next
        Else
            For i = 1 To UBound(CADStaInf(nStaID).Track)
                sStyle = GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink1)
                sStyle1 = GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink1)
                If sStyle <> CADStaInf(nStaID).sStaCode Or sStyle1 <> CADStaInf(nStaID).sStaCode Then
                    If CADStaInf(nStaID).Track(i).sGuDaoYongTu = "只能上行" Then
                        GetLeftGuiDaoTrackIDFromUpOrDown = i
                        Exit For
                    End If
                End If
            Next
        End If

    End Function



    '根据上下行列车得到进站股道ID和出站股道ID
    Public Function GetTrackIDFromUpOrDown(ByVal nStaID As Integer, ByVal nUpOrDown As Integer) As Integer
        GetTrackIDFromUpOrDown = 0
        Dim i As Integer
        Dim sStyle As String
        If nUpOrDown = 1 Then '下行
            For i = 1 To UBound(CADStaInf(nStaID).Track)
                sStyle = CADStaInf(nStaID).Track(i).sGuDaoYongTu
                If sStyle = "主要方向为上行" Or sStyle = "只能上行" Or sStyle = "双方向" Then
                    GetTrackIDFromUpOrDown = i
                    Exit For
                End If

            Next
        Else
            For i = 1 To UBound(CADStaInf(nStaID).Track)
                sStyle = CADStaInf(nStaID).Track(i).sGuDaoYongTu
                If sStyle = "主要方向为下行" Or sStyle = "只能下行" Or sStyle = "双方向" Then
                    GetTrackIDFromUpOrDown = i
                    Exit For
                End If
            Next
        End If

    End Function

    '根据股道编号得到车站的TRACKID
    Public Function GetTrackIDFromGuDaoNum(ByVal nStaID As Integer, ByVal sNum As String) As Integer
        GetTrackIDFromGuDaoNum = 0
        Dim i As Integer
        Dim sStyle As String
        For i = 1 To UBound(CADStaInf(nStaID).Track)
            sStyle = CADStaInf(nStaID).Track(i).sStyle
            If sStyle = "股道线" Then
                If CADStaInf(nStaID).Track(i).sTrackNum = sNum Then
                    GetTrackIDFromGuDaoNum = i
                    Exit For
                End If
            End If
        Next
    End Function

    '根据TRACKCURNUM得到车站代码
    Public Function GetTrackCodeFromTrackCurNum(ByVal sNum As String) As String
        Dim i As Integer
        GetTrackCodeFromTrackCurNum = "NULL"
        If sNum = "" Or sNum = "NuLL" Then
            Exit Function
        End If
        For i = sNum.Length To 1 Step -1
            If IsNumeric(sNum.Substring(i - 1, 1)) = False Then
                GetTrackCodeFromTrackCurNum = sNum.Substring(0, i - 1)
                Exit For
            End If
        Next
    End Function
    '由区间编号得到相邻车站的名称
    Public Function GetStaNameFromSecId(ByVal sSecID As String) As String
        Dim i As Integer
        GetStaNameFromSecId = ""
        For i = 1 To UBound(CADStaInf)
            If CADStaInf(i).sStaCode = sSecID Then
                GetStaNameFromSecId = CADStaInf(i).sStaName
                Exit For
            End If
        Next
    End Function

    Public Sub TrainRunFirstSet(ByVal proBar As ToolStripProgressBar)
        Dim i As Integer
        ReDim TrainRunTrack(UBound(TrainInf))
        proBar.Visible = True
        proBar.Maximum = UBound(TrainInf)
        proBar.Value = 0
        For i = 1 To UBound(TrainInf)
            ReDim TrainRunTrack(i).nTrackID(0)
            'If i = 4 Then Stop
            If TrainInf(i).Train > "" Then
                'If TrainInf(i).sJiaoLuName = "上海南站-->石龙路" Then Stop
                Call SeekTrackCurcirtIDFromTrainSKBRun(i)
            End If
            proBar.Value = i
        Next
        proBar.Visible = False
    End Sub

    Public Sub SeekTrackCurcirtIDFromTrain(ByVal nTrain As Integer)
        Dim i As Integer
        Dim nFirSta As Integer
        Dim nSecSta As Integer
        Dim nSectionID As Integer
        Dim sFirTrackNum As String
        Dim sSecTrackNum As String
        Dim intFirTime As Integer
        Dim intSecTime As Integer
        Dim intFirStopTime As Integer
        Dim intSecStopTime As Integer
        Dim nUpOrDown As Integer
        Dim intZFRunTime As Integer
        intZFRunTime = 60
        ReDim TrainRunTrack(nTrain).nTrackID(0)
        If TrainInf(nTrain).Train <> "" Then
            '始发折返
            If TrainInf(nTrain).TrainReturn(1) > 0 Then
                nFirSta = GetCADStaIDFromStaName(TrainInf(nTrain).ComeStation)
                intFirTime = TimeMinus(TrainInf(nTrain).sStartZFStarting, intZFRunTime) 'TimeAdd(TrainInf(nTrain).sStartZFArrival, intZFRunTime)
                intSecTime = TrainInf(nTrain).sStartZFStarting
                intFirStopTime = TimeMinus(TimeMinus(TrainInf(nTrain).sStartZFStarting, TrainInf(nTrain).sStartZFArrival), intZFRunTime * 2)
                If intFirStopTime < 0 Then
                    intFirStopTime = 0
                End If
                intSecStopTime = TimeMinus(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)), TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(1)))
                sFirTrackNum = TrainInf(nTrain).sStartZFLine
                sSecTrackNum = TrainInf(nTrain).StopLine(TrainInf(nTrain).nPathID(1))
                TrainRunTrack(nTrain).nTrain = nTrain
                Call SeekStationReturnTrackID(nTrain, nFirSta, sFirTrackNum, sSecTrackNum, intFirTime, intSecTime, intFirStopTime, intSecStopTime)
            End If

            '区间运行
            For i = 1 To UBound(TrainInf(nTrain).nPassSection)
                nFirSta = GetCADStaIDFromStaName(StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                'If nFirSta = 5 Then Stop
                nSecSta = GetCADStaIDFromStaName(StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                nSectionID = GetCADStaIDFromSecName(TrainInf(nTrain).sSectionName(i))
                intFirTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nFirstID(i))
                intSecTime = TrainInf(nTrain).Arrival(TrainInf(nTrain).nSecondID(i))
                sFirTrackNum = TrainInf(nTrain).StopLine(TrainInf(nTrain).nFirstID(i))
                sSecTrackNum = TrainInf(nTrain).StopLine(TrainInf(nTrain).nSecondID(i))
                TrainRunTrack(nTrain).nTrain = nTrain

                intFirStopTime = TimeMinus(TrainInf(nTrain).Starting(TrainInf(nTrain).nFirstID(i)), TrainInf(nTrain).Arrival(TrainInf(nTrain).nFirstID(i)))
                intSecStopTime = TimeMinus(TrainInf(nTrain).Starting(TrainInf(nTrain).nSecondID(i)), TrainInf(nTrain).Arrival(TrainInf(nTrain).nSecondID(i)))

                If nTrain Mod 2 = 0 Then
                    nUpOrDown = 2
                Else
                    nUpOrDown = 1
                End If
                Call SeekSectionTrackID(nUpOrDown, nTrain, nFirSta, nSecSta, nSectionID, sFirTrackNum, sSecTrackNum, intFirTime, intSecTime, i, intFirStopTime, intSecStopTime, SectionInf(TrainInf(nTrain).nPassSection(i)).sSecName)
            Next

            '终到折返
            If TrainInf(nTrain).TrainReturn(2) > 0 Then
                nFirSta = GetCADStaIDFromStaName(TrainInf(nTrain).NextStation)
                intFirTime = TrainInf(nTrain).sEndZFArrival
                intSecTime = TimeAdd(intFirTime, intZFRunTime)
                intFirStopTime = TimeMinus(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))), TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))))
                intSecStopTime = TimeMinus(TimeMinus(TrainInf(nTrain).sEndZFStarting, TrainInf(nTrain).sEndZFArrival), intZFRunTime * 2)
                sFirTrackNum = TrainInf(nTrain).StopLine(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID)))
                sSecTrackNum = TrainInf(nTrain).sEndZFLine
                TrainRunTrack(nTrain).nTrain = nTrain
                Call SeekStationReturnTrackID(nTrain, nFirSta, sFirTrackNum, sSecTrackNum, intFirTime, intSecTime, intFirStopTime, intSecStopTime)
            End If
        End If
    End Sub

    Public Sub SeekTrackCurcirtIDFromTrainSKBRun(ByVal nTrain As Integer)
        Dim i As Integer
        Dim nFirSta As Integer
        Dim nSecSta As Integer
        Dim nSectionID As Integer
        Dim sFirTrackNum As String
        Dim sSecTrackNum As String
        Dim intFirTime As Integer
        Dim intSecTime As Integer
        Dim intFirStopTime As Integer
        Dim intSecStopTime As Integer
        Dim nUpOrDown As Integer
        Dim intZFRunTime As Integer
        intZFRunTime = 60
        ReDim TrainRunTrack(nTrain).nTrackID(0)
        If TrainInf(nTrain).Train <> "" Then
            '始发折返
            If TrainInf(nTrain).TrainReturn(1) > 0 Then
                nFirSta = GetCADStaIDFromStaName(TrainInf(nTrain).ComeStation)
                intFirTime = TrainInf(nTrain).sStartZFStarting  'TimeAdd(TrainInf(nTrain).sStartZFArrival, intZFRunTime)
                intSecTime = TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(1))
                intFirStopTime = TimeMinus(TrainInf(nTrain).sStartZFStarting, TrainInf(nTrain).sStartZFArrival)
                If intFirStopTime < 0 Then
                    intFirStopTime = 0
                End If
                intSecStopTime = TimeMinus(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)), TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(1)))
                sFirTrackNum = TrainInf(nTrain).sStartZFLine
                sSecTrackNum = TrainInf(nTrain).StopLine(TrainInf(nTrain).nPathID(1))
                TrainRunTrack(nTrain).nTrain = nTrain
                Call SeekStationReturnTrackIDSKBRun(nTrain, nFirSta, sFirTrackNum, sSecTrackNum, intFirTime, intSecTime, intFirStopTime, intSecStopTime)
            End If

            '区间运行
            For i = 1 To UBound(TrainInf(nTrain).nPassSection)
                nFirSta = GetCADStaIDFromStaName(StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                'If nFirSta = 5 Then Stop
                nSecSta = GetCADStaIDFromStaName(StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                nSectionID = GetCADStaIDFromSecName(TrainInf(nTrain).sSectionName(i))
                intFirTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nFirstID(i))
                intSecTime = TrainInf(nTrain).Arrival(TrainInf(nTrain).nSecondID(i))
                sFirTrackNum = TrainInf(nTrain).StopLine(TrainInf(nTrain).nFirstID(i))
                sSecTrackNum = TrainInf(nTrain).StopLine(TrainInf(nTrain).nSecondID(i))
                TrainRunTrack(nTrain).nTrain = nTrain

                intFirStopTime = TimeMinus(TrainInf(nTrain).Starting(TrainInf(nTrain).nFirstID(i)), TrainInf(nTrain).Arrival(TrainInf(nTrain).nFirstID(i)))
                intSecStopTime = TimeMinus(TrainInf(nTrain).Starting(TrainInf(nTrain).nSecondID(i)), TrainInf(nTrain).Arrival(TrainInf(nTrain).nSecondID(i)))

                If nTrain Mod 2 = 0 Then
                    nUpOrDown = 2
                Else
                    nUpOrDown = 1
                End If
                Call SeekSectionTrackIDSKBRun(nUpOrDown, nTrain, nFirSta, nSecSta, nSectionID, sFirTrackNum, sSecTrackNum, intFirTime, intSecTime, i, intFirStopTime, intSecStopTime)
            Next

            '终到折返
            If TrainInf(nTrain).TrainReturn(2) > 0 Then
                nFirSta = GetCADStaIDFromStaName(TrainInf(nTrain).NextStation)
                intFirTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID)))
                intSecTime = TrainInf(nTrain).sEndZFArrival
                intFirStopTime = TimeMinus(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))), TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))))
                intSecStopTime = TimeMinus(TrainInf(nTrain).sEndZFStarting, TrainInf(nTrain).sEndZFArrival)
                sFirTrackNum = TrainInf(nTrain).StopLine(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID)))
                sSecTrackNum = TrainInf(nTrain).sEndZFLine
                TrainRunTrack(nTrain).nTrain = nTrain
                Call SeekStationReturnTrackIDSKBRun(nTrain, nFirSta, sFirTrackNum, sSecTrackNum, intFirTime, intSecTime, intFirStopTime, intSecStopTime)
            End If
        End If
    End Sub

    '区间轨道电路搜索
    Public Sub SeekSectionTrackIDSKBRun(ByVal nUporDown As Integer, ByVal ID As Integer, ByVal nFirSta As Integer, _
                ByVal nSecSta As Integer, ByVal nSecID As Integer, ByVal sFirTrackNum As String, ByVal nSecTrackNum As String, _
                ByVal intFirTime As Integer, ByVal intSecTime As Integer, ByVal nCurSecID As Integer, ByVal intFirStopTime As Integer, ByVal intSecstopTime As Integer)
        Dim i As Integer
        'Dim j As Integer
        Dim sFirA As String
        Dim sSecA As String
        Dim sSecAB As String
        Dim nID1 As Integer
        Dim nID2 As Integer
        Dim Str As String
        Dim nTID As Integer
        Dim nTID2 As Integer
        nTID = UBound(TrainRunTrack(ID).nTrackID)
        sFirA = CADStaInf(nFirSta).sStaCode
        sSecA = CADStaInf(nSecSta).sStaCode
        sSecAB = CADStaInf(nSecID).sStaCode
        nID1 = GetTrackNumFromGuDaoNum(nFirSta, sFirTrackNum)
        nID2 = GetTrackNumFromStaCode(nUporDown, nFirSta, sSecAB, "出站")
        ' If CADStaInf(nFirSta).sStaName = "虹桥站" Then Stop
        If nID1 > 0 And nID2 > 0 Then
            ReDim nTrackPath(0)
            Call InputCrossData(nFirSta)
            Call SeekRoadFromID(nCurSeekDirection, nFirSta, nID1, nID2)

            Str = ""
            'If nCurSecID = 1 Then
            For i = 1 To UBound(nTrackPath)
                ReDim Preserve TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID) + 1)
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nStaID = nFirSta
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nTrackID = nTrackPath(i)
                Str = Str & "-" & CADStaInf(nFirSta).Track(nTrackPath(i)).sTrackCircuitNum
            Next
            'Else
            '    For i = 2 To UBound(nTrackPath)
            '        ReDim Preserve TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID) + 1)
            '        TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nStaID = nFirSta
            '        TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nTrackID = nTrackPath(i)
            '        Str = Str & "-" & CADStaInf(nFirSta).Track(nTrackPath(i)).sTrackCircuitNum
            '    Next

            'End If
            ' MsgBox(Str)

        End If

        nID1 = GetTrackNumFromSecFirStaCode(nSecID, sFirA, nUporDown)
        nID2 = GetTrackNumFromSecSecStaCode(nSecID, sSecA, nUporDown)

        If nID1 > 0 And nID2 > 0 Then
            ReDim nTrackPath(0)
            'Call InputCrossData(nSecID)
            'Call SeekRoadFromID(nSecID, nID1, nID2)
            Str = ""
            Call SeekRoadFromIDInSection(nSecID, nID1, nID2, nFirSta, nSecSta)
            For i = 1 To UBound(nTrackPath)
                ReDim Preserve TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID) + 1)
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nStaID = nSecID
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nTrackID = nTrackPath(i)
                Str = Str & "-" & CADStaInf(nSecID).Track(nTrackPath(i)).sTrackCircuitNum
            Next
            'MsgBox(Str)
        End If


        nID1 = GetTrackNumFromStaCode(nUporDown, nSecSta, sSecAB, "进站")
        nID2 = GetTrackNumFromGuDaoNum(nSecSta, nSecTrackNum)
        If nID1 > 0 And nID2 > 0 Then
            ReDim nTrackPath(0)
            Call InputCrossData(nSecSta)
            Call SeekRoadFromID(nCurSeekDirection, nSecSta, nID1, nID2)
            Str = ""
            For i = 1 To UBound(nTrackPath)
                ReDim Preserve TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID) + 1)
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nStaID = nSecSta
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nTrackID = nTrackPath(i)
                Str = Str & "-" & CADStaInf(nSecSta).Track(nTrackPath(i)).sTrackCircuitNum
            Next
            'MsgBox(Str)
        End If

        Str = ""
        For i = 1 To UBound(TrainRunTrack(ID).nTrackID)
            Str = Str & "," & CADStaInf(TrainRunTrack(ID).nTrackID(i).nStaID).Track(TrainRunTrack(ID).nTrackID(i).nTrackID).sTrackCircuitNum
        Next
        If nCurSecID = UBound(TrainInf(ID).nPassSection) Then
            'MsgBox(Str)
            'Stop
        End If


        '分配时间，时间均分
        Dim sngEveryTime As Long
        Dim tmpK As Integer
        tmpK = 1
        nTID2 = UBound(TrainRunTrack(ID).nTrackID)
        If nTID2 - nTID > 2 Then
            sngEveryTime = TimeMinus(intSecTime, intFirTime) / (nTID2 - nTID - 2)
            For i = nTID + 1 To nTID2
                TrainRunTrack(ID).nTrackID(i).sRunState = "运行" '，在车站: " & CADStaInf(TrainRunTrack(ID).nTrackID(i).nStaID).sStaName & " 在轨道: " & CADStaInf(TrainRunTrack(ID).nTrackID(i).nStaID).Track(TrainRunTrack(ID).nTrackID(i).nTrackID).sTrackCircuitNum & "  上"
                If i = nTID + 1 Then
                    TrainRunTrack(ID).nTrackID(i).intStarTime = intFirTime + 5
                    TrainRunTrack(ID).nTrackID(i).intArriTime = intFirTime - intFirStopTime - 5
                ElseIf i = nTID2 Then
                    TrainRunTrack(ID).nTrackID(i).intStarTime = intSecTime + intSecstopTime + 5
                    TrainRunTrack(ID).nTrackID(i).intArriTime = intSecTime - 5
                Else
                    TrainRunTrack(ID).nTrackID(i).intStarTime = intFirTime + (tmpK - 1) * sngEveryTime + 5
                    TrainRunTrack(ID).nTrackID(i).intArriTime = intFirTime + (tmpK - 2) * sngEveryTime - 5
                End If
                tmpK = tmpK + 1
            Next
        End If

    End Sub


    '车站折返股道电路搜索
    Public Sub SeekStationReturnTrackIDSKBRun(ByVal ID As Integer, ByVal nSta As Integer, _
                ByVal sFirGudaoNum As String, ByVal sSecGuDaoNum As String, _
                ByVal intFirTime As Integer, ByVal intSecTime As Integer, ByVal intFirStopTime As Integer, ByVal intSecStopTime As Integer)
        Dim i As Integer
        Dim nID1 As Integer
        Dim nID2 As Integer
        Dim Str As String
        Dim nTID As Integer
        Dim nTID2 As Integer
        nTID = UBound(TrainRunTrack(ID).nTrackID)
        nID1 = GetTrackNumFromGuDaoNum(nSta, sFirGudaoNum)
        nID2 = GetTrackNumFromGuDaoNum(nSta, sSecGuDaoNum)
        Dim nUporDown As Integer
        '   nUporDown = nDirection(ID)
        If nID1 > 0 And nID2 > 0 Then
            ReDim nTrackPath(0)
            Call InputCrossData(nSta)
            nUporDown = 1
            Call SeekRoadFromID(nUporDown, nSta, nID1, nID2)
            If UBound(nTrackPath) = 0 Then
                nUporDown = 2
                Call SeekRoadFromID(nUporDown, nSta, nID1, nID2)
            End If
            Str = ""
            For i = 1 To UBound(nTrackPath)
                ReDim Preserve TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID) + 1)
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nStaID = nSta
                TrainRunTrack(ID).nTrackID(UBound(TrainRunTrack(ID).nTrackID)).nTrackID = nTrackPath(i)
                Str = Str & "-" & CADStaInf(nSta).Track(nTrackPath(i)).sTrackCircuitNum
            Next
        End If

        '分配时间，时间均分
        Dim sngEveryTime As Long
        Dim tmpK As Integer
        tmpK = 1
        nTID2 = UBound(TrainRunTrack(ID).nTrackID)
        If nTID2 - nTID > 2 Then
            sngEveryTime = TimeMinus(intSecTime, intFirTime) / (nTID2 - nTID - 2)
            For i = nTID + 1 To nTID2
                TrainRunTrack(ID).nTrackID(i).sRunState = "折返"
                'nStaID = TrainRunTrack(ID).nTrackID(i).nStaID
                'nTrackID = TrainRunTrack(ID).nTrackID(i).nTrackID
                If i = nTID + 1 Then
                    TrainRunTrack(ID).nTrackID(i).intStarTime = intFirTime
                    TrainRunTrack(ID).nTrackID(i).intArriTime = TimeMinus(intFirTime, intFirStopTime)
                ElseIf i = nTID2 Then
                    TrainRunTrack(ID).nTrackID(i).intStarTime = TimeAdd(intSecTime, intSecStopTime)
                    TrainRunTrack(ID).nTrackID(i).intArriTime = intSecTime
                Else
                    TrainRunTrack(ID).nTrackID(i).intStarTime = intFirTime + tmpK * sngEveryTime
                    TrainRunTrack(ID).nTrackID(i).intArriTime = intFirTime + (tmpK - 1) * sngEveryTime
                    tmpK = tmpK + 1
                End If
            Next
        ElseIf nTID2 - nTID = 1 Then
            TrainRunTrack(ID).nTrackID(nTID + 1).intStarTime = TimeAdd(intSecTime, intSecStopTime)
            TrainRunTrack(ID).nTrackID(nTID + 1).intArriTime = TimeMinus(intFirTime, intFirStopTime)
        ElseIf nTID2 - nTID = 2 Then
            TrainRunTrack(ID).nTrackID(nTID + 1).intStarTime = intSecTime
            TrainRunTrack(ID).nTrackID(nTID + 1).intArriTime = TimeMinus(intFirTime, intFirStopTime)
            TrainRunTrack(ID).nTrackID(nTID + 2).intStarTime = TimeAdd(intFirTime, intSecStopTime)
            TrainRunTrack(ID).nTrackID(nTID + 2).intArriTime = intSecTime
        End If

        'MsgBox(Str)

    End Sub

    Public Function GetCADStaIDFromStaName(ByVal sSta As String) As Integer
        GetCADStaIDFromStaName = 0
        Dim i As Integer
        For i = 1 To UBound(CADStaInf)
            If CADStaInf(i).sStaName = sSta Then
                GetCADStaIDFromStaName = i
                Exit For
            End If
        Next
    End Function
    Public Function GetCADStaIDFromSecName(ByVal sSec As String) As Integer
        GetCADStaIDFromSecName = 0
        Dim i As Integer
        For i = 1 To UBound(CADStaInf)
            If CADStaInf(i).sStaName = sSec Then
                GetCADStaIDFromSecName = i
                Exit For
            End If
        Next
    End Function

    '运行仿真
    Public Sub TrainRunSimulation(ByVal intCurTime As Integer, ByVal PicLine As PictureBox, ByVal PicSta As PictureBox, ByVal sngScale As Single, ByVal sngScale2 As Single, ByVal lngLeftX As Single, ByVal lngRightX As Single, ByVal ifPrintLineCheCi As Boolean, ByVal ifPrintStaCheCi As Boolean)
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim p As Integer
        Dim g As System.Drawing.Graphics
        g = PicLine.CreateGraphics()

        Dim g1 As System.Drawing.Graphics
        g1 = PicSta.CreateGraphics()
        Dim intTime1 As Integer
        Dim intTime2 As Integer
        Dim nStaID As Integer
        Dim nTrackID As Integer
        Dim X1, X2, Y1, Y2 As Single
        Dim X11, X21, Y11, Y21 As Single
        Dim tmpPen As Pen
        Dim tmpPen1 As Pen
        Dim sCheci As String
        lngLeftX = lngLeftX * sngScale2
        lngRightX = lngRightX * sngScale2
        PicLine.Refresh()
        PicSta.Refresh()
        Dim tmpFont As Font
        Dim tmpBrush As Brush
        Dim nIfIN As Integer

        For i = 1 To UBound(TrainRunTrack)
            If UBound(TrainRunTrack(i).nTrackID) > 0 Then
                For j = 1 To UBound(TrainRunTrack(i).nTrackID)
                    intTime1 = TrainRunTrack(i).nTrackID(j).intArriTime
                    intTime2 = TrainRunTrack(i).nTrackID(j).intStarTime
                    If intCurTime >= intTime1 And intCurTime <= intTime2 Then
                        nIfIN = 0
                        For p = 1 To UBound(SimuRunInf)
                            If SimuRunInf(p).sGuiDaoNum = CADStaInf(TrainRunTrack(i).nTrackID(j).nStaID).Track(TrainRunTrack(i).nTrackID(j).nTrackID).sTrackCircuitNum Then
                                nIfIN = 1
                                Exit For
                            End If
                        Next

                        If nIfIN = 0 Then
                            ReDim Preserve SimuRunInf(UBound(SimuRunInf) + 1)
                            SimuRunInf(UBound(SimuRunInf)).nTrain = TrainRunTrack(i).nTrain
                            SimuRunInf(UBound(SimuRunInf)).sRunState = TrainRunTrack(i).nTrackID(j).sRunState
                            SimuRunInf(UBound(SimuRunInf)).sStaName = CADStaInf(TrainRunTrack(i).nTrackID(j).nStaID).sStaName
                            SimuRunInf(UBound(SimuRunInf)).sGuiDaoNum = CADStaInf(TrainRunTrack(i).nTrackID(j).nStaID).Track(TrainRunTrack(i).nTrackID(j).nTrackID).sTrackCircuitNum
                            SimuRunInf(UBound(SimuRunInf)).sStopTime = SecondToMinute(intTime2 - intTime1)
                            SimuRunInf(UBound(SimuRunInf)).sControlModle = CADStaInf(TrainRunTrack(i).nTrackID(j).nStaID).Track(TrainRunTrack(i).nTrackID(j).nTrackID).sControlNum
                        End If

                        nStaID = TrainRunTrack(i).nTrackID(j).nStaID
                        nTrackID = TrainRunTrack(i).nTrackID(j).nTrackID
                        If TrainRunTrack(i).nTrain Mod 2 = 0 Then
                            tmpPen = New Pen(Color.YellowGreen, 2)
                        Else
                            tmpPen = New Pen(Color.SteelBlue, 2)
                        End If

                        If TrainRunTrack(i).nTrain Mod 2 = 0 Then
                            tmpPen1 = New Pen(Color.YellowGreen, 4)
                        Else
                            tmpPen1 = New Pen(Color.SteelBlue, 4)
                        End If

                        If i = 1 Then
                            tmpPen = New Pen(Color.YellowGreen, 2)
                            tmpPen1 = New Pen(Color.YellowGreen, 4)
                        ElseIf i = 3 Then
                            tmpPen = New Pen(Color.SteelBlue, 2)
                            tmpPen1 = New Pen(Color.SteelBlue, 4)
                        End If

                        If i = 2 Then
                            tmpPen = New Pen(Color.YellowGreen, 2)
                            tmpPen1 = New Pen(Color.YellowGreen, 4)
                        ElseIf i = 4 Then
                            tmpPen = New Pen(Color.SteelBlue, 2)
                            tmpPen1 = New Pen(Color.SteelBlue, 4)
                        End If

                        ''显示控制分区
                        'For p = 1 To UBound(CADStaInf)
                        '    For k = 1 To UBound(CADStaInf(p).Track)
                        '        If CADStaInf(p).Track(k).sControlNum = CADStaInf(nStaID).Track(nTrackID).sControlNum Then
                        '            'If sCurControlSecNum <> CADStaInf(nStaID).Track(nTrackID).sControlNum Then
                        '            X1 = CADStaInf(p).Track(k).X1 * sngScale
                        '            Y1 = CADStaInf(p).Track(k).Y1 * sngScale
                        '            X2 = CADStaInf(p).Track(k).X2 * sngScale
                        '            Y2 = CADStaInf(p).Track(k).Y2 * sngScale
                        '            g.DrawLine(tmpPen, X1, Y1, X2, Y2)

                        '            X1 = CADStaInf(p).Track(k).X1 * sngScale2
                        '            Y1 = CADStaInf(p).Track(k).Y1 * sngScale2
                        '            X2 = CADStaInf(p).Track(k).X2 * sngScale2
                        '            Y2 = CADStaInf(p).Track(k).Y2 * sngScale2
                        '            If X1 >= lngLeftX And X2 <= lngRightX Then
                        '                g1.DrawLine(tmpPen1, X1 - lngLeftX, Y1, X2 - lngLeftX, Y2)
                        '            End If
                        '            'End If
                        '        End If
                        '    Next
                        'Next
                        'sCurControlSecNum = CADStaInf(nStaID).Track(nTrackID).sControlNum

                        For k = 1 To UBound(tmpRunTrackInf)
                            If tmpRunTrackInf(k).nStaID = nStaID And tmpRunTrackInf(k).nTrackID = nTrackID Then
                                X1 = tmpRunTrackInf(k).X1
                                Y1 = tmpRunTrackInf(k).Y1
                                X2 = tmpRunTrackInf(k).X2
                                Y2 = tmpRunTrackInf(k).Y2
                                g.DrawLine(New Pen(TrainInf(TrainRunTrack(i).nTrain).PrintLineColor, 2), X1, Y1, X2, Y2)
                                If ifPrintLineCheCi = True Then
                                    sCheci = TrainInf(TrainRunTrack(i).nTrain).sPrintTrain
                                    If sCheci.Length >= 3 Then
                                        sCheci = sCheci.Substring(0, 3)
                                    End If
                                    tmpFont = New Font("Arial", 8)
                                    tmpBrush = New SolidBrush(TrainInf(TrainRunTrack(i).nTrain).PrintLineColor)
                                    Call WriteStrInLine(g, sCheci, tmpFont, tmpBrush, X1, Y1, X2, Y2, 2)
                                End If
                                Exit For
                            End If
                        Next
                        Dim sCurTime As String
                        sCurTime = dTime(intCurTime, 0)
                        g1.DrawString(sCurTime, New Font("黑体", 20), Brushes.Yellow, 10, 10)

                        For k = 1 To UBound(tmpRunStaTrackInf)
                            If tmpRunStaTrackInf(k).nStaID = nStaID And tmpRunStaTrackInf(k).nTrackID = nTrackID Then
                                X11 = tmpRunStaTrackInf(k).X1
                                Y11 = tmpRunStaTrackInf(k).Y1
                                X21 = tmpRunStaTrackInf(k).X2
                                Y21 = tmpRunStaTrackInf(k).Y2
                                g1.DrawLine(New Pen(TrainInf(TrainRunTrack(i).nTrain).PrintLineColor, 3), X11, Y11, X21, Y21)
                                If ifPrintStaCheCi = True Then
                                    sCheci = TrainInf(TrainRunTrack(i).nTrain).sPrintTrain
                                    If sCheci.Length >= 3 Then
                                        sCheci = sCheci.Substring(0, 3)
                                    End If
                                    tmpFont = New Font("Arial", 8)
                                    tmpBrush = New SolidBrush(TrainInf(TrainRunTrack(i).nTrain).PrintLineColor)
                                    Call WriteStrInLine(g1, sCheci, tmpFont, tmpBrush, X11, Y11, X21, Y21, 2)
                                End If
                                Exit For
                            End If
                        Next
                        Exit For
                    End If
                Next
            End If
        Next
    End Sub

    '得到当前PicLine框中对应的X坐标 
    Public Function GetCurLinePicX(ByVal nStaID As Integer, ByVal nTrackID As Integer, ByVal X As Single) As Single
        Dim i As Integer
        For i = 1 To UBound(tmpRunTrackInf)
            If tmpRunTrackInf(i).nStaID = nStaID And tmpRunTrackInf(i).nTrackID = nTrackID Then
                GetCurLinePicX = tmpRunTrackInf(i).X1
            End If
        Next
    End Function

    '得到当前PicLine框中对应的Y坐标 
    Public Function GetCurLinePicY(ByVal Y As Single) As Single

    End Function

    '打印车站平面图
    Public Sub PrintStationCADPicture(ByVal rBmpGraphics As Graphics, ByVal StaName As String, ByVal sngScale As Single, _
                                                ByVal sngLeft As Single, ByVal sngRight As Single, ByVal sngCompareX As Single, _
                                                ByVal sngLeftX As Single, ByVal sngTopY As Single, ByVal tmpPen As Pen)
        Dim X1 As Long
        Dim Y1 As Long
        Dim ForX As Long
        Dim ForY As Long
        Dim i As Integer
        Dim k As Integer
        Dim lngStaX As Long
        Dim lngStaY As Long

        lngStaX = 0
        lngStaY = 0
        ForX = 0
        ForY = 0
        Dim curPen As Pen
        Dim curColor As Color
        For k = 1 To UBound(CADStaInf)
            If CADStaInf(k).sStaName = StaName Then
                For i = 1 To UBound(CADStaInf(k).Track)
                    ForX = CADStaInf(k).Track(i).X1 * sngScale - sngCompareX + sngLeftX
                    ForY = CADStaInf(k).Track(i).Y1 * sngScale + sngTopY
                    X1 = CADStaInf(k).Track(i).X2 * sngScale - sngCompareX + sngLeftX
                    Y1 = CADStaInf(k).Track(i).Y2 * sngScale + sngTopY

                    If ForX >= 0 And ForY > 0 Then
                        If ForX >= sngLeft And ForX <= sngRight And X1 <= sngRight Then
                            ForX = ForX - sngLeft
                            X1 = X1 - sngLeft
                            curColor = GetControModelColor(CADStaInf(k).Track(i).sControlNum)
                            curPen = New Pen(curColor, 3)
                            rBmpGraphics.DrawLine(curPen, ForX, ForY, X1, Y1)

                            'If Val(Asc(CADStaInf(k).Track(i).sControlNum)) Mod 2 = 0 Then
                            '    rBmpGraphics.DrawLine(tmpPen, ForX, ForY, X1, Y1)
                            'Else
                            '    rBmpGraphics.DrawLine(New Pen(Color.Yellow, 4), ForX, ForY, X1, Y1)
                            'End If


                            'If ForX = X1 Then
                            '    rBmpGraphics.DrawLine(New Pen(Color.White, 2), ForX - nShortLineWidth, ForY, ForX + nShortLineWidth, ForY)
                            '    rBmpGraphics.DrawLine(New Pen(Color.White, 2), X1 - nShortLineWidth, Y1, X1 + nShortLineWidth, Y1)
                            'Else
                            '    rBmpGraphics.DrawLine(New Pen(Color.White, 2), ForX, ForY - nShortLineWidth, ForX, ForY + nShortLineWidth)
                            '    rBmpGraphics.DrawLine(New Pen(Color.White, 2), X1, Y1 - nShortLineWidth, X1, Y1 + nShortLineWidth)
                            'End If
                        End If
                    End If

                Next i

                '画信号机
                For i = 1 To UBound(CADStaInf(k).Signal)
                    X1 = CADStaInf(k).Signal(i).X * sngScale - sngCompareX + sngLeftX
                    Y1 = CADStaInf(k).Signal(i).Y * sngScale + sngTopY
                    If X1 >= sngLeft And X1 <= sngRight Then
                        X1 = X1 - sngLeft

                        If X1 >= 0 And Y1 >= 0 Then
                            Call PrintSignalByStyle(CADStaInf(k).Signal(i).sStyle, X1, Y1, rBmpGraphics, Color.White, sngScale)
                        End If
                    End If
                Next i

                '画站台
                For i = 1 To UBound(CADStaInf(k).PlatForm)
                    ForX = CADStaInf(k).PlatForm(i).X1 * sngScale - sngCompareX + sngLeftX
                    ForY = CADStaInf(k).PlatForm(i).Y1 * sngScale + sngTopY
                    X1 = CADStaInf(k).PlatForm(i).X2 * sngScale - sngCompareX + sngLeftX
                    Y1 = CADStaInf(k).PlatForm(i).Y2 * sngScale + sngTopY
                    If ForX >= sngLeft And ForX <= sngRight And X1 <= sngRight Then
                        If ForX >= 0 And ForY >= 0 Then
                            ForX = ForX - sngLeft
                            X1 = X1 - sngLeft
                            rBmpGraphics.DrawRectangle(tmpPen, ForX, ForY, X1 - ForX, Y1 - ForY)
                        End If
                    End If
                Next i

                '打印文字
                For i = 1 To UBound(CADStaInf(k).FontText)
                    X1 = CADStaInf(k).FontText(i).X * sngScale - sngCompareX + sngLeftX
                    Y1 = CADStaInf(k).FontText(i).Y * sngScale + sngTopY - 5
                    If X1 >= sngLeft And X1 <= sngRight Then
                        X1 = X1 - sngLeft
                        Dim sText As String
                        Dim FontName As String
                        Dim FontSize As Single
                        Dim FontColor As Color
                        sText = CADStaInf(k).FontText(i).sText
                        FontSize = CADStaInf(k).FontText(i).FontSize * sngScale * 1.5
                        FontName = CADStaInf(k).FontText(i).FontName
                        FontColor = CADStaInf(k).FontText(i).FontColor
                        Dim s As New FontStyle
                        If CADStaInf(k).FontText(i).Italic = True Then
                            s = CADStaInf(k).FontText(i).Italic
                        End If
                        If CADStaInf(k).FontText(i).Bold = True Then
                            s = CADStaInf(k).FontText(i).Bold
                        End If
                        Dim b As New SolidBrush(FontColor)
                        Dim f As New Font(FontName, FontSize, s)
                        If X1 >= 0 And Y1 >= 0 Then
                            rBmpGraphics.DrawString(sText, f, b, X1, Y1)
                        End If
                    End If
                Next i
                Exit For
            End If
        Next
        '画线段

    End Sub
End Module
