
Imports Microsoft.Office.Interop
Imports System
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Module modOpenTimetabale
    '读入车底连接信息
    Public Sub CSreadCheDiLinkInf(ByVal sSKBID As String, ByVal proBar As ProgressBar)
        Dim i, j, p As Integer
        Dim sqlstr As String = ""
        Dim nTrain As Integer
        Dim nTempTrain As Integer
        For i = 1 To UBound(CSchediInfo)
            ReDim CSchediInfo(i).nLinkTrain(0)
        Next
        ReDim CSTrainInf(0)
        sqlstr = "SELECT * FROM TMS_STOCKUSINGINFO WHERE TrainDiagramID='" & sSKBID & "' order by StockSeq,LineSeq"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        Dim stmpSta As Integer
        Dim tmpBeforSta As Integer
        Dim tmpID As Integer
        Dim tmpXH As Integer

        If tempTable.Rows.Count > 0 Then
            tmpBeforSta = Int(tempTable.Rows(0).Item("StockSeq"))
            tmpID = 1
            ReDim CSchediInfo(tmpID)
            ReDim CSchediInfo(tmpID).nLinkTrain(0)
            Call CSCopyCheDiInformation(tmpID, tempTable.Rows(0).Item("StockID"), CSchediInfo)
            If tmpID > 0 Then
            Else
                MsgBox("车底信息与当前运行图信息不对应，请检查运行图和车底信息!", , "提示")
            End If
            tmpXH = 1

            For i = 1 To tempTable.Rows.Count
                stmpSta = Int(tempTable.Rows(i - 1).Item("StockSeq"))
                If stmpSta <> tmpBeforSta Then
                    tmpBeforSta = stmpSta
                    tmpID = tmpID + 1
                    ReDim Preserve CSchediInfo(tmpID)
                    ReDim CSchediInfo(tmpID).nLinkTrain(0)
                    Call CSCopyCheDiInformation(tmpID, tempTable.Rows(i - 1).Item("Stockid"), CSchediInfo)
                    tmpXH = 1
                End If
                If tmpID > 0 Then
                    tmpXH = 1
                    nTrain = CSAddTrainInformation(Trim(tempTable.Rows(i - 1).Item("RoutingStyle")), Trim(tempTable.Rows(i - 1).Item("RunScaleStyle")), tempTable.Rows(i - 1).Item("StopScaleStyle"), tempTable.Rows(i - 1).Item("LinkTrainNum"))
                    CSTrainInf(nTrain).Train = Trim(tempTable.Rows(i - 1).Item("LinkTrainNum"))
                    CSTrainInf(nTrain).sPrintTrain = tempTable.Rows(i - 1).Item("PrintNum").ToString.Trim
                    CSTrainInf(nTrain).nZfLimit = Trim(tempTable.Rows(i - 1).Item("IfTurnFixed"))
                    CSTrainInf(nTrain).PrintLineStyle = GetLineStyleFromText(tempTable.Rows(i - 1).Item("LineShowStyle").ToString.Trim)
                    CSTrainInf(nTrain).PrintLineWidth = tempTable.Rows(i - 1).Item("LineShowWidth")
                    CSTrainInf(nTrain).PrintLineColor = System.Drawing.ColorTranslator.FromHtml(tempTable.Rows(i - 1).Item("LineShowColor"))
                    If nTrain > 0 And CSTrainInf(nTrain).Train <> "" Then
                        ReDim Preserve CSchediInfo(tmpID).nLinkTrain(UBound(CSchediInfo(tmpID).nLinkTrain) + 1)
                        CSchediInfo(tmpID).nLinkTrain(UBound(CSchediInfo(tmpID).nLinkTrain)) = nTrain
                        CSTrainInf(nTrain).nTrain = nTrain
                    End If
                    CSchediInfo(tmpID).sCheCiHao = tempTable.Rows(i - 1).Item("StockID") 'Left(rsDb.Fields("输出车次").Value, 3)
                    CSchediInfo(tmpID).PrintCheDiLinkStyle = GetLineStyleFromText(tempTable.Rows(i - 1).Item("StockShowStyle").ToString.Trim)
                    CSchediInfo(tmpID).PrintCheDiLinkWidth = tempTable.Rows(i - 1).Item("StockShowWidth")
                    CSchediInfo(tmpID).PrintCheDiLinkColor = System.Drawing.ColorTranslator.FromHtml(tempTable.Rows(i - 1).Item("StockShowColor").ToString.Trim)
                    If tempTable.Rows(i - 1).Item("IfTurnFixed") = 1 Then
                        CSchediInfo(tmpID).bIfGouWang = True
                    Else
                        CSchediInfo(tmpID).bIfGouWang = False
                    End If
                Else
                    MsgBox("车底信息与当前运行图信息不对应，请检查运行图和车底信息!", , "提示")
                End If
                tmpXH = tmpXH + 1
                proBar.Value = 10 + Int(i * 40 / tempTable.Rows.Count)
            Next i
        End If

        For j = 1 To UBound(CSchediInfo)
            nTempTrain = 0
            For p = 1 To UBound(CSchediInfo(j).nLinkTrain)
                nTrain = CSchediInfo(j).nLinkTrain(p)
                CSTrainInf(nTrain).SCheDiLeiXing = CSchediInfo(j).SCheDiLeiXing
                If nTrain <> 0 And CSTrainInf(nTrain).Train <> "" Then
                    If nTempTrain = 0 Then
                        CSTrainInf(nTrain).TrainReturn(1) = 0
                        CSTrainInf(nTrain).TrainReturn(2) = 0
                    Else
                        CSTrainInf(nTempTrain).TrainReturn(2) = nTrain
                        CSTrainInf(nTrain).TrainReturn(1) = nTempTrain
                        CSTrainInf(nTrain).TrainReturn(2) = 0
                    End If
                    CSTrainInf(nTrain).nCheDiPuOrNot = 1
                    nTempTrain = nTrain
                End If
            Next p
        Next j
    End Sub
    '增加列车，由BaseTraininf中复制过来，并返回复制后列车的ID号
    Public Function CSAddTrainInformation(ByVal sJiaoLuName As String, ByVal sTrainStyle As String, ByVal sStopScale As String, ByVal sCheci As String) As Integer
        Dim i As Integer
        Dim nWeiNum As Integer
        Dim nBaseId As Integer
        nBaseId = GetBasicTrainInfID(sJiaoLuName)
        If nBaseId = 0 Then
            CSAddTrainInformation = 0
            Exit Function
        End If

        Dim nTrain As Integer
        nTrain = BasicTrainInf(nBaseId).nUporDown
        '先检查有无事先定义好的维数
        If nTrain Mod 2 = 0 Then
            For i = 1 To UBound(CSTrainInf)
                If i Mod 2 = 0 Then
                    If CSTrainInf(i).Train = "" Then
                        nWeiNum = i
                        If sCheci = "" Then
                            sCheci = nWeiNum
                        End If
                        Call CSCopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale, CSTrainInf)
                        'Call CopyFenTingBaseCSTrainInf(nBaseId, nWeiNum)
                        CSAddTrainInformation = i
                        GoTo sEnd
                    End If
                End If
            Next i
        Else
            For i = 1 To UBound(CSTrainInf)
                If i Mod 2 <> 0 Then
                    If CSTrainInf(i).Train = "" Then
                        nWeiNum = i
                        If sCheci = "" Then
                            sCheci = nWeiNum
                        End If
                        Call CSCopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale, CSTrainInf)
                        'Call CopyFenTingBaseCSTrainInf(nBaseId, nWeiNum)
                        CSAddTrainInformation = i
                        GoTo sEnd
                    End If
                End If
            Next i
        End If

        '如果没有事先定义好的维数，增加列车维数
        If nTrain > 0 Then
            If nTrain Mod 2 = 0 Then
                If UBound(CSTrainInf) Mod 2 = 0 Then
                    nWeiNum = UBound(CSTrainInf) + 2
                    ReDim Preserve CSTrainInf(nWeiNum)
                    If sCheci = "" Then
                        sCheci = nWeiNum
                    End If
                    Call CSCopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale, CSTrainInf)
                    'Call CopyFenTingBaseCSTrainInf(nBaseId, nWeiNum)
                    CSAddTrainInformation = nWeiNum
                    GoTo sEnd
                Else
                    nWeiNum = UBound(CSTrainInf) + 1
                    ReDim Preserve CSTrainInf(nWeiNum)
                    If sCheci = "" Then
                        sCheci = nWeiNum
                    End If
                    Call CSCopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale, CSTrainInf)
                    'Call CopyFenTingBaseCSTrainInf(nBaseId, nWeiNum)
                    CSAddTrainInformation = nWeiNum
                    GoTo sEnd
                End If
            Else
                If UBound(CSTrainInf) Mod 2 = 0 Then
                    nWeiNum = UBound(CSTrainInf) + 1
                    ReDim Preserve CSTrainInf(nWeiNum)
                    If sCheci = "" Then
                        sCheci = nWeiNum
                    End If
                    Call CSCopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale, CSTrainInf)
                    'Call CopyFenTingBaseCSTrainInf(nBaseId, nWeiNum)
                    CSAddTrainInformation = nWeiNum
                    GoTo sEnd
                Else
                    nWeiNum = UBound(CSTrainInf) + 2
                    ReDim Preserve CSTrainInf(nWeiNum)
                    If sCheci = "" Then
                        sCheci = nWeiNum
                    End If
                    Call CSCopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale, CSTrainInf)
                    'Call CopyFenTingBaseCSTrainInf(nBaseId, nWeiNum)
                    CSAddTrainInformation = nWeiNum
                    GoTo sEnd
                End If
            End If
        End If
sEnd:
        'CSTrainInf(AddCSTrainInformation).nTrainTimeKind = sTimeScaleToTimeKind(sKind)
        'CSTrainInf(AddCSTrainInformation).sTrainTimeScale = sKind
    End Function
    ''复制列车基础信息，BaseTraininf
    Sub CSCopyMainBaseTrainInf(ByVal nTrainNumFrom As Integer, ByVal nTrainNumTo As Integer, ByVal sTrain As String, ByVal sTrainStyle As String, ByVal sStopScale As String, ByRef CSTrainInf() As typeTrainInformation)
        Dim i As Integer
        With CSTrainInf(nTrainNumTo)
            .Train = sTrain 'BaseTrainInf(nTrainNumFrom).Train
            .nLeftState = 0
            .nRightState = 0
            .nLinkLeft = 0
            .nIfCanMove = 0
            .sJiaoLuName = BasicTrainInf(nTrainNumFrom).sJiaoLuName
            .sRunScaleName = sTrainStyle
            .sStopSclaeName = sStopScale

            .TrainClass = 1 ' BaseTrainInf(nTrainNumFrom).TrainClass
            .TrainClassCal = 1 ' BaseTrainInf(nTrainNumFrom).TrainClassCal
            .nTrainTimeKind = 1 ' BaseTrainInf(nTrainNumFrom).nTrainTimeKind
            .sTrainTimeScale = sTrainStyle 'BaseTrainInf(nTrainNumFrom).sTrainTimeScale
            .TrainKind = "客车" ' BasicTrainInf(nTrainNumFrom).TrainKind
            .StartStation = BasicTrainInf(nTrainNumFrom).StartStation
            .EndStation = BasicTrainInf(nTrainNumFrom).EndStation
            .ComeStation = BasicTrainInf(nTrainNumFrom).ComeStation
            .ComeLine = "" 'BasicTrainInf(nTrainNumFrom).ComeLine
            .NextStation = BasicTrainInf(nTrainNumFrom).NextStation
            .NextLine = "" 'BasicTrainInf(nTrainNumFrom).NextLine
            .sTrainUsageZD = "" ' BasicTrainInf(nTrainNumFrom).sTrainUsageZD
            .sTrainBeizhuZD = "" 'BasicTrainInf(nTrainNumFrom).sTrainBeizhuZD
            .sTrainUsageSF = "" 'BasicTrainInf(nTrainNumFrom).sTrainUsageSF
            .sTrainBeizhuSF = "" 'BasicTrainInf(nTrainNumFrom).sTrainBeizhuSF
            .NumStop = 0 'BasicTrainInf(nTrainNumFrom).NumStop
            .NumWay = 0 ' BasicTrainInf(nTrainNumFrom).NumWay
            .TrainStyle = BasicTrainInf(nTrainNumFrom).TrainStyle
            .TrainPuorNot = 0 'BasicTrainInf(nTrainNumFrom).TrainPuorNot
            .nChaRunDirection = 0 'BasicTrainInf(nTrainNumFrom).nChaRunDirection
            .nLinkTrainNum = 0 'BasicTrainInf(nTrainNumFrom).nLinkTrainNum
            .nIfEnterGZSta = 0 'BasicTrainInf(nTrainNumFrom).nIfEnterGZSta
            .nIfFixedCheDi = 0
            .SCheDiLeiXing = BasicTrainInf(nTrainNumFrom).SCheDiLeiXing
            .sTrainXingZhi = BasicTrainInf(nTrainNumFrom).sTrainXingZhi
            .sPrintTrain = sTrain
            .sLineNum = BasicTrainInf(nTrainNumFrom).sLineNum
            .sMuDiNum = BasicTrainInf(nTrainNumFrom).sMuDiNum

            ReDim .TrainReturn(2)
            ReDim .TrainReturnStyle(2)
            For i = 1 To 2
                .TrainReturn(i) = 0 ' BasicTrainInf(nTrainNumFrom).TrainReturn(i)
                .TrainReturnStyle(i) = BasicTrainInf(nTrainNumFrom).TrainReturnStyle(i)
            Next i


            .sTrainPath = BasicTrainInf(nTrainNumFrom).sTrainPath        '列车径路,用"，"分开
            ReDim .nPassSection(UBound(BasicTrainInf(nTrainNumFrom).nPassSection))
            ReDim .SectionRunTime(UBound(BasicTrainInf(nTrainNumFrom).nPassSection))
            ReDim .sSectionName(UBound(BasicTrainInf(nTrainNumFrom).sSectionName))
            'ReDim .StrFirstSta(UBound(BasicTrainInf(nTrainNumFrom).StrFirstSta))
            'ReDim .StrSecondSta(UBound(BasicTrainInf(nTrainNumFrom).StrSecondSta))
            ReDim .nFirstID(UBound(BasicTrainInf(nTrainNumFrom).nFirstID))
            ReDim .nSecondID(UBound(BasicTrainInf(nTrainNumFrom).nSecondID))
            ReDim .nPathID(UBound(BasicTrainInf(nTrainNumFrom).nPathID))
            ReDim .sPathSta(UBound(BasicTrainInf(nTrainNumFrom).nPathID))

            ReDim .sngFirXcoord(UBound(BasicTrainInf(nTrainNumFrom).nPassSection))
            ReDim .sngFirYcoord(UBound(BasicTrainInf(nTrainNumFrom).nPassSection))
            ReDim .sngSecXcoord(UBound(BasicTrainInf(nTrainNumFrom).nPassSection))
            ReDim .sngSecYcoord(UBound(BasicTrainInf(nTrainNumFrom).nPassSection))

            ReDim .lChaRunTime(0)
            ReDim .StopLine(UBound(StationInf))
            ReDim .StopLineTime(UBound(StationInf))
            ReDim .lChaRunTime(UBound(StationInf))
            ReDim .Starting(UBound(BasicTrainInf(nTrainNumFrom).Starting))
            ReDim .Arrival(UBound(BasicTrainInf(nTrainNumFrom).Arrival))
            ReDim .StopStation(UBound(BasicTrainInf(nTrainNumFrom).nPathID))
            ReDim .nstopSta(UBound(BasicTrainInf(nTrainNumFrom).nPathID))
            ReDim .stopTime(UBound(BasicTrainInf(nTrainNumFrom).Starting))

            .NumStop = UBound(BasicTrainInf(nTrainNumFrom).nPathID)

            For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).nPassSection)
                .nPassSection(i) = BasicTrainInf(nTrainNumFrom).nPassSection(i)
            Next i

            For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).sSectionName)
                .sSectionName(i) = BasicTrainInf(nTrainNumFrom).sSectionName(i)
            Next i

            'For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).StrFirstSta)
            '    .StrFirstSta(i) = BasicTrainInf(nTrainNumFrom).StrFirstSta(i)
            'Next i

            'For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).StrSecondSta)
            '    .StrSecondSta(i) = BasicTrainInf(nTrainNumFrom).StrSecondSta(i)
            'Next i

            For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).nPassSection)
                .nFirstID(i) = BasicTrainInf(nTrainNumFrom).nFirstID(i)
            Next i

            For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).nSecondID)
                .nSecondID(i) = BasicTrainInf(nTrainNumFrom).nSecondID(i)
            Next i

            For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).nPathID)
                .nPathID(i) = BasicTrainInf(nTrainNumFrom).nPathID(i)
                .StopStation(i) = StationInf(.nPathID(i)).sStationName
                .nstopSta(i) = .nPathID(i)

            Next i

            For i = 1 To UBound(.lChaRunTime)
                .lChaRunTime(i) = 0 ' BaseTrainInf(nTrainNumFrom).StopLine(i)
            Next i

            For i = 1 To UBound(.StopLine)
                .StopLine(i) = "" ' BaseTrainInf(nTrainNumFrom).StopLine(i)
            Next i

            For i = 1 To UBound(.StopLineTime)
                .StopLineTime(i) = 0 'BaseTrainInf(nTrainNumFrom).StopLineTime(i)
            Next i

            For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).Starting)
                .Starting(i) = -1 'BaseTrainInf(nTrainNumFrom).Starting(i)
                .stopTime(i) = 0
            Next i

            For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).Arrival)
                .Arrival(i) = -1 'BaseTrainInf(nTrainNumFrom).Arrival(i)
            Next i

            '加入分岔站信息
            .NumWay = BasicTrainInf(nTrainNumFrom).NumWay
            ReDim .Way1(BasicTrainInf(nTrainNumFrom).NumWay)
            ReDim .Way2(BasicTrainInf(nTrainNumFrom).NumWay)
            ReDim .Way3(BasicTrainInf(nTrainNumFrom).NumWay)

            For i = 1 To .NumWay
                .Way1(i) = BasicTrainInf(nTrainNumFrom).Way1(i)
                .Way2(i) = BasicTrainInf(nTrainNumFrom).Way2(i)
                .Way3(i) = BasicTrainInf(nTrainNumFrom).Way3(i)
            Next i

            .PrintLineStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.TrainLineStyle)
            .PrintLineWidth = TimeTablePara.DiagramStylePara.TrainLineWidth
            .PrintLineColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TrainLineColor)

            'Dim j As Integer
            ''停站信息
            'For j = 1 To UBound(BasicTrainInf(nTrainNumFrom).StopScale)
            '    If BasicTrainInf(nTrainNumFrom).StopScale(j).sname = sStopScale Then
            '        ReDim .StopStation(UBound(BasicTrainInf(nTrainNumFrom).StopScale(j).nStopStation))
            '        ReDim .stopTime(UBound(BasicTrainInf(nTrainNumFrom).StopScale(j).nStopStation))
            '        .NumStop = UBound(BasicTrainInf(nTrainNumFrom).StopScale(j).nStopStation)

            '        For i = 1 To .NumStop
            '            .StopStation(i) = StationInf(BasicTrainInf(nTrainNumFrom).StopScale(j).nStopStation(i)).sStationName
            '            .stopTime(i) = BasicTrainInf(nTrainNumFrom).StopScale(j).StopTime(i)
            '        Next i

            '        Exit For
            '    End If
            'Next j

            ''区间运行时分信息
            'For j = 1 To UBound(.nPassSection)
            '    .SectionRunTime(j) = GetSecRunTimeFromJiaoLuName(nTrainNumFrom, sTrainStyle, .nPassSection(j))
            'Next j

        End With
        'Call SetTrainDefautColor(nTrainNumTo)
    End Sub

    '将时刻表读入
    Public Sub CSReadTimetableInf(ByVal sSKBID As String, ByVal proBar As ProgressBar)
        Dim i, j, k, p, n As Integer
        Dim q As Integer
        Dim sqlstr As String = ""
        Dim sTrainNum() As String
        ReDim sTrainNum(0)
        Dim nErrorTrain As Integer
        nErrorTrain = 0
        Dim nStaID As Integer
        Dim nifIn As Integer

        sqlstr = "SELECT * FROM CS_PREPAREDTRAINLIST WHERE TRAINDIAGRAMID='" & sSKBID & "' and lineid='" & CurLineName & "'"
        Dim preparedTrain As New DataTable
        preparedTrain = Globle.Method.ReadDataForAccess(sqlstr)
        'april Edit
        sqlstr = "SELECT * FROM TMS_TIMETABLEINFO WHERE TrainDiagramID='" & sSKBID & "' order by TrainNum,Seq"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        For j = 1 To UBound(CSTrainInf)
            If CSTrainInf(j).Train <> "" Then

                CSTrainInf(j).TrainPuorNot = 1
                '*************************************************************************************************************************
                ReDim Preserve CSTrainInf(j).Starting(UBound(StationInf))
                ReDim Preserve CSTrainInf(j).Arrival(UBound(StationInf))
                ReDim Preserve CSTrainInf(j).StopLine(UBound(StationInf))
                '*************************************************************************************************************************
                For k = 1 To UBound(StationInf)
                    CSTrainInf(j).Starting(k) = -1
                    CSTrainInf(j).Arrival(k) = -1
                    CSTrainInf(j).StopLine(k) = ""
                Next k

                If tempTable.Rows.Count > 0 Then
                    For p = 1 To tempTable.Rows.Count
                        If tempTable.Rows(p - 1).Item("TrainNum").ToString.Trim = CSTrainInf(j).Train Then
                            nifIn = 0
                            While p < tempTable.Rows.Count - 1 AndAlso tempTable.Rows(p + 1).Item("TrainNum").ToString.Trim = CSTrainInf(j).Train
                                For q = 1 To UBound(StationInf)
                                    If StationInf(q).sStationName = tempTable.Rows(p - 1).Item("stationname").ToString.Trim Then
                                        nStaID = q
                                        CSTrainInf(j).Starting(nStaID) = tempTable.Rows(p - 1).Item("DepartTime")
                                        CSTrainInf(j).Arrival(nStaID) = tempTable.Rows(p - 1).Item("ArriTime")
                                        CSTrainInf(j).StopLine(nStaID) = tempTable.Rows(p - 1).Item("StopTrack")
                                        nifIn = 1
                                    End If
                                Next
                                If nifIn = 0 Then
                                    nErrorTrain = nErrorTrain + 1
                                End If
                                p += 1
                            End While
                            If p < tempTable.Rows.Count AndAlso tempTable.Rows(p).Item("TrainNum").ToString.Trim = CSTrainInf(j).Train Then
                                CSTrainInf(j).sStartZFStarting = tempTable.Rows(p - 1).Item("DepartTime") ' HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                                CSTrainInf(j).sStartZFArrival = tempTable.Rows(p - 1).Item("ArriTime") ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                                CSTrainInf(j).sStartZFLine = tempTable.Rows(p - 1).Item("StopTrack")
                            ElseIf p < tempTable.Rows.Count AndAlso tempTable.Rows(p).Item("TrainNum").ToString.Trim <> CSTrainInf(j).Train Then
                                CSTrainInf(j).sEndZFStarting = tempTable.Rows(p - 1).Item("DepartTime") 'HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                                CSTrainInf(j).sEndZFArrival = tempTable.Rows(p - 1).Item("ArriTime")  ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                                CSTrainInf(j).sEndZFLine = tempTable.Rows(p - 1).Item("StopTrack")
                            End If
                            If p = tempTable.Rows.Count Then
                                CSTrainInf(j).sEndZFStarting = tempTable.Rows(p - 1).Item("DepartTime") 'HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                                CSTrainInf(j).sEndZFArrival = tempTable.Rows(p - 1).Item("ArriTime")  ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                                CSTrainInf(j).sEndZFLine = tempTable.Rows(p - 1).Item("StopTrack")
                            End If
                        End If
                    Next p
                    CSTrainInf(j).lAllStartTime = CSTrainInf(j).Starting(CSTrainInf(j).nPathID(1))
                    CSTrainInf(j).lAllEndTime = CSTrainInf(j).Arrival(CSTrainInf(j).nPathID(UBound(CSTrainInf(j).nPathID)))
                    CSTrainInf(j).BeiCheState = ""
                End If
                If preparedTrain.Rows.Count > 0 Then
                    For p = 0 To preparedTrain.Rows.Count - 1
                        If CSTrainInf(j).sPrintTrain = preparedTrain.Rows(p).Item("checi").ToString.Trim AndAlso CSTrainInf(j).Starting(CSTrainInf(j).nPathID(1)).ToString = preparedTrain.Rows(p).Item("starttime").ToString.Trim AndAlso CSTrainInf(j).Arrival(CSTrainInf(j).nPathID(UBound(CSTrainInf(j).nPathID))) = preparedTrain.Rows(p).Item("endtime").ToString.Trim Then
                            CSTrainInf(j).sIfBeiChe = 1
                            CSTrainInf(j).BeiCheState = preparedTrain.Rows(p).Item("state").ToString.Trim
                        End If
                    Next
                End If
            End If
            proBar.Value = 50 + Int(j * 50 / UBound(CSTrainInf))
        Next j
        If nErrorTrain > 0 Then
            MsgBox("列车信息中有" & nErrorTrain & "趟列车没有时刻，请检查当前的底图结构是否选择错误！", , "提示")
        End If
    End Sub

    '打开车底信息和发车间隔安排
    Public Sub CSInputChediAndTrainJianGeData(ByVal sState As String, ByVal nDiagramID As String)
        Dim i As Integer
        Dim sqlstr As String = ""

        sqlstr = "SELECT * FROM TMS_TRAINUSINGINFO WHERE TraindiagramID='" & nDiagramID & "' order by StockStyleID"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        ReDim BaseChediInfo(tempTable.Rows.Count)
        If tempTable.Rows.Count > 0 Then
            For i = 1 To tempTable.Rows.Count
                With BaseChediInfo(i)
                    .SCheDiLeiXing = tempTable.Rows(i - 1).Item("StockStyleName")
                    .bIfGouWang = 0
                    ReDim BaseChediInfo(i).nLinkTrain(0)
                End With
            Next
        End If
    End Sub

    '读入底图结构与线网信息
    Public Sub CSreadNetStaAndSecData(ByVal nDiagramID As String)
        Dim sqlstr As String
        sqlstr = "SELECT * FROM TMS_DIASTRUCTINFO WHERE IfFixed=1 and TRAINDIAGRAMID='" & nDiagramID & "' order by StationSeq"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        ReDim StationInf(tempTable.Rows.Count)
        Dim i, j, n As Integer
        If tempTable.Rows.Count > 0 Then
            For i = 1 To tempTable.Rows.Count
                StationInf(i).sStationName = Trim(tempTable.Rows(i - 1).Item("StationName"))
                StationInf(i).sAtLineName = Trim(tempTable.Rows(i - 1).Item("Linename"))
                StationInf(i).Ycord = Trim(tempTable.Rows(i - 1).Item("YCoord"))
            Next i
        Else
            MsgBox("底图结构没有设置或者没有设置默认的底图结构!", , "提示")
            Exit Sub
        End If


        ''新增 读取车站属性
        sqlstr = "SELECT * FROM TMS_STATIONINFO WHERE  TraindiagramID='" & nDiagramID & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        For i = 1 To UBound(StationInf)
            If tempTable.Rows.Count > 0 Then
                For n = 0 To tempTable.Rows.Count - 1
                    If tempTable.Rows(n).Item("stationname").ToString.Trim = StationInf(i).sStationName Then
                        StationInf(i).sStaStyle = tempTable.Rows(n).Item("StationType").ToString.Trim
                        'StationInf(i).sAtLineName = tempTable.Rows(n).Item("LINENAME").ToString.Trim
                        StationInf(i).sPrintStaName = tempTable.Rows(n).Item("OutputName").ToString.Trim
                        StationInf(i).sStaProperity = tempTable.Rows(n).Item("StationCharacter").ToString.Trim
                        StationInf(i).sStationProp = ChaStProp(tempTable.Rows(n).Item("StationCharacter").ToString.Trim, tempTable.Rows(n).Item("StationType").ToString.Trim)
                        StationInf(i).sEnglishName = tempTable.Rows(n).Item("EnglishShortName").ToString.Trim
                        Exit For
                    End If
                Next
            End If
        Next i
        ''新增 读取车站属性

        ReDim NotSameStationInf(0)
        Dim nMark As Integer
        nMark = 0
        For i = 1 To UBound(StationInf)
            nMark = 0
            For j = 1 To UBound(NotSameStationInf)
                If NotSameStationInf(j) = StationInf(i).sStationName Then
                    nMark = 1
                    Exit For
                End If
            Next j
            If nMark = 0 Then
                ReDim Preserve NotSameStationInf(UBound(NotSameStationInf) + 1)
                NotSameStationInf(UBound(NotSameStationInf)) = StationInf(i).sStationName
            End If
        Next

        sqlstr = "SELECT * FROM TMS_SECTIONINFO WHERE TraindiagramID='" & nDiagramID & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        Dim sFirSta As String
        Dim sSecSta As String
        Dim sSecName As String
        ReDim SectionInf(tempTable.Rows.Count)
        For i = 1 To UBound(StationInf) - 1
            sFirSta = StationInf(i).sStationName
            sSecSta = StationInf(i + 1).sStationName
            sSecName = sFirSta & "->" & sSecSta
            If tempTable.Rows.Count > 0 Then
                For j = 1 To tempTable.Rows.Count
                    If tempTable.Rows(j - 1).Item("SectionName").ToString.Trim = sSecName Then
                        ReDim SectionInf(j).lDistance(2)
                        SectionInf(j).nSecNumber = tempTable.Rows(j - 1).Item("SECTIONSEQ")
                        SectionInf(j).sSecName = sSecName
                        SectionInf(j).sLineName = tempTable.Rows(j - 1).Item("LineName")
                        SectionInf(j).nSection = Val(tempTable.Rows(j - 1).Item("LineNumber").ToString)
                        SectionInf(j).sBlock = tempTable.Rows(j - 1).Item("Blocktype").ToString
                        SectionInf(j).sSecFirName = sFirSta
                        SectionInf(j).sSecSecName = sSecSta
                        SectionInf(j).nFirStaID = i
                        SectionInf(j).nSecStaID = i + 1
                        SectionInf(j).nHStation = i
                        SectionInf(j).nQStation = i + 1
                        SectionInf(j).lDistance(1) = tempTable.Rows(j - 1).Item("DownSectionDistance")
                        SectionInf(j).lDistance(2) = tempTable.Rows(j - 1).Item("UPSectionDistance")
                        Exit For
                    End If
                Next
            End If
        Next
    End Sub

    '读入列车交路
    Public Sub CSReadBaseTrainInf(ByVal ndiagramID As String)
        Dim i, j, p As Integer
        Dim sqlstr As String = ""
        sqlstr = "SELECT * FROM TMS_TRAININFO WHERE TraindiagramID='" & ndiagramID & "' order by RoutingName"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        ReDim BasicTrainInf(tempTable.Rows.Count)
        Dim nJiNum As Int32
        Dim nOhNum As Int32
        nJiNum = 1
        nOhNum = 2
        If tempTable.Rows.Count > 0 Then
            For p = 1 To tempTable.Rows.Count
                With BasicTrainInf(p)
                    .sJiaoLuName = Trim(tempTable.Rows(p - 1).Item("RoutingName").ToString)
                    If Trim(tempTable.Rows(p - 1).Item("UporDown").ToString) = "上行" Then
                        .nUporDown = 2
                    Else
                        .nUporDown = 1
                    End If
                    .TrainStyle = Trim(tempTable.Rows(p - 1).Item("Type").ToString)
                    .StartStation = Trim(tempTable.Rows(p - 1).Item("OStationNAME").ToString)
                    .EndStation = Trim(tempTable.Rows(p - 1).Item("DStationNAME").ToString)
                    .ComeStation = Trim(tempTable.Rows(p - 1).Item("OStationNAME").ToString)
                    .NextStation = Trim(tempTable.Rows(p - 1).Item("DStationNAME").ToString)

                    ReDim Preserve .TrainReturnStyle(2)
                    .TrainReturnStyle(1) = Trim(tempTable.Rows(p - 1).Item("StartType").ToString)
                    .TrainReturnStyle(2) = Trim(tempTable.Rows(p - 1).Item("EndUseType").ToString)

                    .sTrainXingZhi = Trim(tempTable.Rows(p - 1).Item("TrainType").ToString)
                    .SCheDiLeiXing = Trim(tempTable.Rows(p - 1).Item("StockStyleID").ToString)

                    .sLineNum = Trim(tempTable.Rows(p - 1).Item("LINETRAINCODE").ToString)
                    .sMuDiNum = Trim(tempTable.Rows(p - 1).Item("ENDSIGN").ToString)

                    Dim IfIn As Integer
                    IfIn = 0

                    '初始化设置
                    ReDim .Arrival(UBound(StationInf))
                    ReDim .Starting(UBound(StationInf))
                    For j = 1 To UBound(StationInf)
                        .Arrival(j) = -1
                        .Starting(j) = -1
                    Next j


                    '以下代码读入列车径路信息
                    Dim TrainPath As String
                    TrainPath = Trim(tempTable.Rows(p - 1).Item("PASSSTAID"))
                    If Right(TrainPath, 1) = "," Or Right(TrainPath, 1) = "，" Then
                    Else
                        TrainPath = TrainPath & ","
                    End If
                    Dim TrainPathSta() As String
                    ReDim TrainPathSta(0)

                    .sTrainPath = TrainPath
                    i = 1
                    For j = 1 To Len(TrainPath)
                        If Mid(TrainPath, j, 1) = "," Or Mid(TrainPath, j, 1) = "，" Then
                            If Trim(Mid(TrainPath, i, j - i)) <> "" Then
                                ReDim Preserve TrainPathSta(UBound(TrainPathSta) + 1)
                                TrainPathSta(UBound(TrainPathSta)) = Mid(TrainPath, i, j - i)
                                i = j + 1
                            End If
                        End If
                    Next j

                    If UBound(TrainPathSta) < 2 Then
                        Exit Sub
                    End If

                    '将列车径过的车站组成n-1个区间
                    ReDim .nPassSection(UBound(TrainPathSta) - 1)
                    ReDim .sSectionName(UBound(TrainPathSta) - 1)
                    ReDim .StrFirstSta(UBound(TrainPathSta) - 1)
                    ReDim .StrSecondSta(UBound(TrainPathSta) - 1)
                    ReDim .nFirstID(UBound(TrainPathSta) - 1)
                    ReDim .nSecondID(UBound(TrainPathSta) - 1)

                    Dim strQianSta As String
                    Dim StrHouSta As String
                    Dim strQuJian1 As String
                    Dim strQuJian2 As String
                    Dim KK As Integer
                    strQianSta = TrainPathSta(1)
                    For i = 2 To UBound(TrainPathSta)
                        KK = 0
                        StrHouSta = TrainPathSta(i)
                        strQuJian1 = strQianSta & "->" & StrHouSta
                        strQuJian2 = StrHouSta & "->" & strQianSta
                        For j = 1 To UBound(SectionInf)
                            If SectionInf(j).sSecName = strQuJian1 Then
                                .nPassSection(i - 1) = j
                                .sSectionName(i - 1) = strQuJian1
                                .StrFirstSta(i - 1) = strQianSta
                                .StrSecondSta(i - 1) = StrHouSta
                                .nFirstID(i - 1) = SectionInf(j).nFirStaID
                                .nSecondID(i - 1) = SectionInf(j).nSecStaID
                                KK = 1
                            ElseIf SectionInf(j).sSecName = strQuJian2 Then
                                .nPassSection(i - 1) = j
                                .sSectionName(i - 1) = strQuJian2
                                .StrFirstSta(i - 1) = strQianSta
                                .StrSecondSta(i - 1) = StrHouSta
                                .nFirstID(i - 1) = SectionInf(j).nSecStaID
                                .nSecondID(i - 1) = SectionInf(j).nFirStaID
                                KK = 1
                            End If
                            If KK = 1 Then Exit For
                        Next j
                        strQianSta = StrHouSta
                    Next i

                    '导入nPathID()
                    Dim ID1 As Integer
                    Dim ID2 As Integer
                    If UBound(.nPassSection) > 0 Then
                        ID1 = .nFirstID(1)
                        ID2 = .nSecondID(1)

                        ReDim .nPathID(2)
                        .nPathID(1) = ID1
                        .nPathID(2) = ID2

                        If UBound(.nPassSection) > 1 Then
                            For i = 2 To UBound(.nPassSection)
                                If .nFirstID(i) <> .nPathID(UBound(.nPathID)) Then
                                    ReDim Preserve .nPathID(UBound(.nPathID) + 1)
                                    .nPathID(UBound(.nPathID)) = .nFirstID(i)
                                    ReDim Preserve .nPathID(UBound(.nPathID) + 1)
                                    .nPathID(UBound(.nPathID)) = .nSecondID(i)
                                Else
                                    ReDim Preserve .nPathID(UBound(.nPathID) + 1)
                                    .nPathID(UBound(.nPathID)) = .nSecondID(i)
                                End If
                            Next i
                        End If

                    End If


                End With
            Next p
        Else
            Exit Sub
        End If
    End Sub

    '复制车底信息
    Public Sub CSCopyCheDiInformation(ByVal nNewCDid As Integer, ByVal sCheDiID As String, ByRef CSchediInfo() As typePublicChediInformation)
        Dim i As Integer
        i = 1

        With CSchediInfo(nNewCDid)
            .SCheDiLeiXing = BaseChediInfo(i).SCheDiLeiXing
            .sCheDiID = nNewCDid 'BaseChediInfo(i).sCheDiID
            .bIfAutoResetCheCi = True
            If sCheDiID = "NULL" Then
                sCheDiID = ""
            End If
            .sCheCiHao = sCheDiID

            ReDim .nLinkTrain(0)
            .PrintCheDiLinkStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.CheDiLineStyle)
            .PrintCheDiLinkWidth = TimeTablePara.DiagramStylePara.CheDiLineWidth
            .PrintCheDiLinkColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.CheDiLineColor)

        End With

    End Sub

    '由车站名称得到车站ID
    Public Function GetStationID(ByVal strStaName As String) As Integer
        Dim i As Integer
        For i = 1 To UBound(StationInf)
            If StationInf(i).sStationName = strStaName Then
                GetStationID = i
                Exit For
            End If
        Next

    End Function

    '将小时转化为秒
    Public Function HourToSecond(ByVal HourTime As String) As Integer
        If HourTime = "-1" Then
            HourToSecond = -1
            Exit Function
        End If
        Dim i As Integer
        Dim sSecond As Single
        For i = 1 To Len(HourTime)
            If Mid(HourTime, i, 1) = "." Or Mid(HourTime, i, 1) = ":" Then
                sSecond = Val(Left(HourTime, i)) * 3600
                HourTime = Right(HourTime, Len(HourTime) - i)
                Exit For
            End If
            If i = Len(HourTime) Then
                sSecond = Val(HourTime) * 3600
                HourToSecond = sSecond
                Exit Function
            End If
        Next i

        For i = 1 To Len(HourTime)
            If Mid(HourTime, i, 1) = "." Or Mid(HourTime, i, 1) = ":" Then
                sSecond = sSecond + Val(Left(HourTime, i)) * 60
                HourTime = Right(HourTime, Len(HourTime) - i)
                Exit For
            End If
            If i = Len(HourTime) Then
                sSecond = sSecond + Val(HourTime) * 60
                HourToSecond = sSecond
                Exit Function
            End If
        Next i

        For i = 1 To Len(HourTime)
            If Mid(HourTime, i, 1) = "." Or Mid(HourTime, i, 1) = ":" Then
                sSecond = sSecond + Val(Left(HourTime, i))
                HourTime = Right(HourTime, Len(HourTime) - i)
                Exit For
            End If
            If i = Len(HourTime) Then
                sSecond = sSecond + Val(HourTime)
                HourToSecond = sSecond
                Exit Function
            End If
        Next i
        HourToSecond = sSecond
        Exit Function

    End Function

    '根据车站名确定车站ID
    Public Function FromStaNameToStaIDByStationinf(ByVal StaName As String) As Integer
        Dim i As Integer
        FromStaNameToStaIDByStationinf = 0
        For i = 1 To UBound(StationInf)
            If StationInf(i).sStationName = StaName Then
                FromStaNameToStaIDByStationinf = i
                Exit For
            End If
        Next i
    End Function

    '由区间名称得到区间ID
    Public Function GetSectionID(ByVal strSecName As String) As Integer
        Dim i As Integer
        For i = 1 To UBound(SectionInf)
            If SectionInf(i).sSecName = strSecName Then
                GetSectionID = i
                Exit For
            End If
        Next
    End Function
    '将分钟转化为秒
    Public Function MinuteToSecond(ByVal MinuteTime As String) As Single
        MinuteTime = Val(MinuteTime)
        MinuteToSecond = Int(MinuteTime) * 60 + (MinuteTime - Int(MinuteTime)) * 100
    End Function
    '根据当前车站查找前一车站或后一车站的名称，不重复
    Public Function GetBeforOrAfterStaNameFromCurSta(ByVal nTrain As Integer, ByVal sStaName As String, ByVal nState As String) As String
        Dim i As Integer
        Dim sBeforName As String
        Dim sNowStaName As String
        GetBeforOrAfterStaNameFromCurSta = ""
        If nState = 1 Then '找前一车站
            For i = 2 To UBound(BasicTrainInf(nTrain).nPathID)
                sBeforName = StationInf(BasicTrainInf(nTrain).nPathID(i - 1)).sStationName
                sNowStaName = StationInf(BasicTrainInf(nTrain).nPathID(i)).sStationName
                If i = 2 Then
                    If sBeforName = sStaName Then
                        GetBeforOrAfterStaNameFromCurSta = sStaName
                        Exit For
                    Else
                        If sNowStaName = sStaName Then
                            GetBeforOrAfterStaNameFromCurSta = sBeforName
                            Exit For
                        End If
                    End If
                Else
                    If sBeforName <> sNowStaName Then
                        If sNowStaName = sStaName Then
                            GetBeforOrAfterStaNameFromCurSta = sBeforName
                            Exit For
                        End If
                        sBeforName = sNowStaName
                    End If
                End If
            Next i

        Else '找后一车站

            For i = UBound(BasicTrainInf(nTrain).nPathID) To 2 Step -1
                sBeforName = StationInf(BasicTrainInf(nTrain).nPathID(i - 1)).sStationName
                sNowStaName = StationInf(BasicTrainInf(nTrain).nPathID(i)).sStationName
                If i = 2 Then
                    If sNowStaName = sStaName Then
                        GetBeforOrAfterStaNameFromCurSta = sStaName
                        Exit For
                    Else
                        If sBeforName = sStaName Then
                            GetBeforOrAfterStaNameFromCurSta = sNowStaName
                            Exit For
                        End If
                    End If
                Else
                    If sBeforName <> sNowStaName Then
                        If sBeforName = sStaName Then
                            GetBeforOrAfterStaNameFromCurSta = sNowStaName
                            Exit For
                        End If
                        sNowStaName = sBeforName
                    End If
                End If
            Next i
        End If
        If GetBeforOrAfterStaNameFromCurSta = "" Then
            GetBeforOrAfterStaNameFromCurSta = sStaName
        End If
    End Function
    '得到BasicTrainInf的ID值
    Public Function GetBasicTrainInfID(ByVal sJiaoLuName As String) As Integer
        GetBasicTrainInfID = 0
        Dim i As Integer
        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName = sJiaoLuName Then
                GetBasicTrainInfID = i
                Exit For
            End If
        Next i
    End Function
    '增加列车ID，并返回复制后列车的ID号
    Public Function AddNewTrainID(ByVal nUpOrDown As Integer) As Integer
        Dim i As Integer
        Dim nWeiNum As Integer
        Dim nTrain As Integer
        nTrain = nUpOrDown
        '先检查有无事先定义好的维数
        If nTrain Mod 2 = 0 Then
            For i = 1 To UBound(TrainInf)
                If i Mod 2 = 0 Then
                    If TrainInf(i).Train = "" Then
                        nWeiNum = i
                        AddNewTrainID = nWeiNum
                        GoTo sEnd
                    End If
                End If
            Next i
        Else
            For i = 1 To UBound(TrainInf)
                If i Mod 2 <> 0 Then
                    If TrainInf(i).Train = "" Then
                        nWeiNum = i
                        AddNewTrainID = nWeiNum
                        GoTo sEnd
                    End If
                End If
            Next i
        End If

        '如果没有事先定义好的维数，增加列车维数
        If nTrain > 0 Then
            If nTrain Mod 2 = 0 Then
                If UBound(TrainInf) Mod 2 = 0 Then
                    nWeiNum = UBound(TrainInf) + 2
                    ReDim Preserve TrainInf(nWeiNum)
                    AddNewTrainID = nWeiNum
                    GoTo sEnd
                Else
                    nWeiNum = UBound(TrainInf) + 1
                    ReDim Preserve TrainInf(nWeiNum)
                    AddNewTrainID = nWeiNum
                    GoTo sEnd
                End If
            Else
                If UBound(TrainInf) Mod 2 = 0 Then
                    nWeiNum = UBound(TrainInf) + 1
                    ReDim Preserve TrainInf(nWeiNum)
                    AddNewTrainID = nWeiNum
                    GoTo sEnd
                Else
                    nWeiNum = UBound(TrainInf) + 2
                    ReDim Preserve TrainInf(nWeiNum)
                    AddNewTrainID = nWeiNum
                    GoTo sEnd
                End If
            End If
        End If
sEnd:
        'TrainInf(AddTrainInformation).nTrainTimeKind = sTimeScaleToTimeKind(sKind)
        'TrainInf(AddTrainInformation).sTrainTimeScale = sKind
    End Function



    ''复制列车基础信息，BaseTraininf
    Sub CopyMainBaseTrainInf(ByVal nTrainNumFrom As Integer, ByVal nTrainNumTo As Integer, ByVal sTrain As String, ByVal sTrainStyle As String, ByVal sStopScale As String)
        Dim i As Integer
        With TrainInf(nTrainNumTo)
            .Train = sTrain 'BaseTrainInf(nTrainNumFrom).Train
            .nLeftState = 0
            .nRightState = 0
            .nLinkLeft = 0
            .nIfCanMove = 0
            .sJiaoLuName = BasicTrainInf(nTrainNumFrom).sJiaoLuName
            .sRunScaleName = sTrainStyle
            .sStopSclaeName = sStopScale

            .TrainClass = 1 ' BaseTrainInf(nTrainNumFrom).TrainClass
            .TrainClassCal = 1 ' BaseTrainInf(nTrainNumFrom).TrainClassCal
            .nTrainTimeKind = 1 ' BaseTrainInf(nTrainNumFrom).nTrainTimeKind
            .sTrainTimeScale = sTrainStyle 'BaseTrainInf(nTrainNumFrom).sTrainTimeScale
            .TrainKind = "客车" ' BasicTrainInf(nTrainNumFrom).TrainKind
            .StartStation = BasicTrainInf(nTrainNumFrom).StartStation
            .EndStation = BasicTrainInf(nTrainNumFrom).EndStation
            .ComeStation = BasicTrainInf(nTrainNumFrom).ComeStation
            .ComeLine = "" 'BasicTrainInf(nTrainNumFrom).ComeLine
            .NextStation = BasicTrainInf(nTrainNumFrom).NextStation
            .NextLine = "" 'BasicTrainInf(nTrainNumFrom).NextLine
            .sTrainUsageZD = "" ' BasicTrainInf(nTrainNumFrom).sTrainUsageZD
            .sTrainBeizhuZD = "" 'BasicTrainInf(nTrainNumFrom).sTrainBeizhuZD
            .sTrainUsageSF = "" 'BasicTrainInf(nTrainNumFrom).sTrainUsageSF
            .sTrainBeizhuSF = "" 'BasicTrainInf(nTrainNumFrom).sTrainBeizhuSF
            .NumStop = 0 'BasicTrainInf(nTrainNumFrom).NumStop
            .NumWay = 0 ' BasicTrainInf(nTrainNumFrom).NumWay
            .TrainStyle = BasicTrainInf(nTrainNumFrom).TrainStyle
            .TrainPuorNot = 0 'BasicTrainInf(nTrainNumFrom).TrainPuorNot
            .nChaRunDirection = 0 'BasicTrainInf(nTrainNumFrom).nChaRunDirection
            .nLinkTrainNum = 0 'BasicTrainInf(nTrainNumFrom).nLinkTrainNum
            .nIfEnterGZSta = 0 'BasicTrainInf(nTrainNumFrom).nIfEnterGZSta
            .nIfFixedCheDi = 0
            .SCheDiLeiXing = BasicTrainInf(nTrainNumFrom).SCheDiLeiXing
            .sTrainXingZhi = BasicTrainInf(nTrainNumFrom).sTrainXingZhi
            .sPrintTrain = sTrain
            .sLineNum = BasicTrainInf(nTrainNumFrom).sLineNum
            .sMuDiNum = BasicTrainInf(nTrainNumFrom).sMuDiNum

            ReDim .TrainReturn(2)
            ReDim .TrainReturnStyle(2)
            For i = 1 To 2
                .TrainReturn(i) = 0 ' BasicTrainInf(nTrainNumFrom).TrainReturn(i)
                .TrainReturnStyle(i) = BasicTrainInf(nTrainNumFrom).TrainReturnStyle(i)
            Next i


            .sTrainPath = BasicTrainInf(nTrainNumFrom).sTrainPath        '列车径路,用"，"分开
            ReDim .nPassSection(UBound(BasicTrainInf(nTrainNumFrom).nPassSection))
            ReDim .SectionRunTime(UBound(BasicTrainInf(nTrainNumFrom).nPassSection))
            ReDim .sSectionName(UBound(BasicTrainInf(nTrainNumFrom).sSectionName))
            'ReDim .StrFirstSta(UBound(BasicTrainInf(nTrainNumFrom).StrFirstSta))
            'ReDim .StrSecondSta(UBound(BasicTrainInf(nTrainNumFrom).StrSecondSta))
            ReDim .nFirstID(UBound(BasicTrainInf(nTrainNumFrom).nFirstID))
            ReDim .nSecondID(UBound(BasicTrainInf(nTrainNumFrom).nSecondID))
            ReDim .nPathID(UBound(BasicTrainInf(nTrainNumFrom).nPathID))
            ReDim .sPathSta(UBound(BasicTrainInf(nTrainNumFrom).nPathID))

            ReDim .sngFirXcoord(UBound(BasicTrainInf(nTrainNumFrom).nPassSection))
            ReDim .sngFirYcoord(UBound(BasicTrainInf(nTrainNumFrom).nPassSection))
            ReDim .sngSecXcoord(UBound(BasicTrainInf(nTrainNumFrom).nPassSection))
            ReDim .sngSecYcoord(UBound(BasicTrainInf(nTrainNumFrom).nPassSection))

            ReDim .lChaRunTime(0)
            ReDim .StopLine(UBound(StationInf))
            ReDim .StopLineTime(UBound(StationInf))
            ReDim .lChaRunTime(UBound(StationInf))
            ReDim .Starting(UBound(BasicTrainInf(nTrainNumFrom).Starting))
            ReDim .Arrival(UBound(BasicTrainInf(nTrainNumFrom).Arrival))
            ReDim .StopStation(UBound(BasicTrainInf(nTrainNumFrom).nPathID))
            ReDim .nstopSta(UBound(BasicTrainInf(nTrainNumFrom).nPathID))
            ReDim .stopTime(UBound(BasicTrainInf(nTrainNumFrom).Starting))

            .NumStop = UBound(BasicTrainInf(nTrainNumFrom).nPathID)

            For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).nPassSection)
                .nPassSection(i) = BasicTrainInf(nTrainNumFrom).nPassSection(i)
            Next i

            For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).sSectionName)
                .sSectionName(i) = BasicTrainInf(nTrainNumFrom).sSectionName(i)
            Next i

            'For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).StrFirstSta)
            '    .StrFirstSta(i) = BasicTrainInf(nTrainNumFrom).StrFirstSta(i)
            'Next i

            'For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).StrSecondSta)
            '    .StrSecondSta(i) = BasicTrainInf(nTrainNumFrom).StrSecondSta(i)
            'Next i

            For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).nPassSection)
                .nFirstID(i) = BasicTrainInf(nTrainNumFrom).nFirstID(i)
            Next i

            For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).nSecondID)
                .nSecondID(i) = BasicTrainInf(nTrainNumFrom).nSecondID(i)
            Next i

            For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).nPathID)
                .nPathID(i) = BasicTrainInf(nTrainNumFrom).nPathID(i)
                .StopStation(i) = StationInf(.nPathID(i)).sStationName
                .nstopSta(i) = .nPathID(i)

            Next i

            For i = 1 To UBound(.lChaRunTime)
                .lChaRunTime(i) = 0 ' BaseTrainInf(nTrainNumFrom).StopLine(i)
            Next i

            For i = 1 To UBound(.StopLine)
                .StopLine(i) = "" ' BaseTrainInf(nTrainNumFrom).StopLine(i)
            Next i

            For i = 1 To UBound(.StopLineTime)
                .StopLineTime(i) = 0 'BaseTrainInf(nTrainNumFrom).StopLineTime(i)
            Next i

            For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).Starting)
                .Starting(i) = -1 'BaseTrainInf(nTrainNumFrom).Starting(i)
                .stopTime(i) = 0
            Next i

            For i = 1 To UBound(BasicTrainInf(nTrainNumFrom).Arrival)
                .Arrival(i) = -1 'BaseTrainInf(nTrainNumFrom).Arrival(i)
            Next i

            '加入分岔站信息
            .NumWay = BasicTrainInf(nTrainNumFrom).NumWay
            ReDim .Way1(BasicTrainInf(nTrainNumFrom).NumWay)
            ReDim .Way2(BasicTrainInf(nTrainNumFrom).NumWay)
            ReDim .Way3(BasicTrainInf(nTrainNumFrom).NumWay)

            For i = 1 To .NumWay
                .Way1(i) = BasicTrainInf(nTrainNumFrom).Way1(i)
                .Way2(i) = BasicTrainInf(nTrainNumFrom).Way2(i)
                .Way3(i) = BasicTrainInf(nTrainNumFrom).Way3(i)
            Next i

            .PrintLineStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.TrainLineStyle)
            .PrintLineWidth = TimeTablePara.DiagramStylePara.TrainLineWidth
            .PrintLineColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TrainLineColor)

        End With
    End Sub

    Public Sub InputStationYValue()
        Dim i As Integer
        For i = 1 To UBound(StationInf)
            StationInf(i).YPicValue = i
        Next
    End Sub
    '读入列车与时刻表信息
    Public Sub CSReadTrainAndTimeTableInf(ByVal sSKBID As String, ByVal ProBar As ProgressBar)
        ProBar.Visible = True
        ProBar.Maximum = 100
        ProBar.Value = 0
        Call CSreadCheDiLinkInf(sSKBID, ProBar)
        ProBar.Value = 50
        Call CSReadTimetableInf(sSKBID, ProBar)
        ProBar.Value = 100
        ProBar.Visible = False
    End Sub


    Public Sub readOutputStationName(_DiagramCurID As String)
        Dim sqlstr As String = ""
        sqlstr = "select * from tms_stationinfo t where t.traindiagramid='" & _DiagramCurID & "'"
        Dim tab As New DataTable
        tab = Globle.Method.ReadDataForAccess(sqlstr)
        If tab.Rows.Count > 0 Then
            OutputStationName = New Dictionary(Of String, String)
            For Each r As DataRow In tab.Rows
                If OutputStationName.Keys.Contains(r.Item("stationname").ToString()) Then
                Else
                    OutputStationName.Add(r.Item("stationname").ToString(), r.Item("outputname").ToString())
                End If
            Next
        End If
    End Sub

End Module
