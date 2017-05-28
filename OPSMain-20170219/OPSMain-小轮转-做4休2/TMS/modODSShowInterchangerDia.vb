Public Module modODSShowInterchangerDia
    Public Sub ShowInterChangerstaDiagram(ByVal sCurStaName As String)
        '显示换乘站运行图

        ODSPubpara.DiagramSelect = DiaVer(1).sCurDiaVersion
        Call InitSystemVariant(0)
        Call IniteDiagramPicViraient("换乘站运行图")

        Dim i, j As Integer
        Dim N, M, N1, Num As Integer
        N = 0
        M = 0
        N1 = 0
        ReDim StationInf(0)
        ReDim SectionInf(0)
        ReDim BasicTrainInf(0)
        Call GetTransferLinkSta(sCurStaName)

        For i = 1 To TransferStaInf.sSecInf.Count
            Num = 0
            If TransferStaInf.sSecInf(i - 1).sUpSecName <> "" Then
                N = N + 1
                Num = Num + 1
                ReDim Preserve StationInf(N)
                StationInf(N).sStationName = TransferStaInf.sSecInf(i - 1).sUpStaName
                StationInf(N).sPrintStaName = TransferStaInf.sSecInf(i - 1).sUpStaName
                StationInf(N).sAtLineName = TransferStaInf.sSecInf(i - 1).sLineName
                StationInf(N).Ycord = 100 + N * 100
                M = M + 1
                ReDim Preserve SectionInf(M)
                ReDim Preserve SectionInf(M).lDistance(2)
                SectionInf(M).nSecNumber = M
                SectionInf(M).sSecName = TransferStaInf.sSecInf(i - 1).sUpSecName
                SectionInf(M).sLineName = TransferStaInf.sSecInf(i - 1).sLineName
                SectionInf(M).nSection = 2
                SectionInf(M).sBlock = "自动"
                SectionInf(M).sSecFirName = TransferStaInf.sStaName
                SectionInf(M).sSecSecName = TransferStaInf.sSecInf(i - 1).sUpStaName
                SectionInf(M).nFirStaID = N
                SectionInf(M).nSecStaID = N + 1
                SectionInf(M).nHStation = N
                SectionInf(M).nQStation = N + 1
                SectionInf(M).lDistance(1) = 2
                SectionInf(M).lDistance(2) = 2

            End If

            N = N + 1
            Num = Num + 1
            ReDim Preserve StationInf(N)
            StationInf(N).sStationName = TransferStaInf.sStaName
            StationInf(N).sPrintStaName = TransferStaInf.sStaName
            StationInf(N).sAtLineName = TransferStaInf.sSecInf(i - 1).sLineName
            StationInf(N).Ycord = 100 + N * 100

            If TransferStaInf.sSecInf(i - 1).sDownSecName <> "" Then
                N = N + 1
                Num = Num + 1
                ReDim Preserve StationInf(N)
                StationInf(N).sStationName = TransferStaInf.sSecInf(i - 1).sDownStaName
                StationInf(N).sPrintStaName = TransferStaInf.sSecInf(i - 1).sDownStaName
                StationInf(N).sAtLineName = TransferStaInf.sSecInf(i - 1).sLineName
                StationInf(N).Ycord = 100 + N * 100

                M = M + 1
                ReDim Preserve SectionInf(M)
                ReDim Preserve SectionInf(M).lDistance(2)
                SectionInf(M).nSecNumber = M
                SectionInf(M).sSecName = TransferStaInf.sSecInf(i - 1).sDownSecName
                SectionInf(M).sLineName = TransferStaInf.sSecInf(i - 1).sLineName
                SectionInf(M).nSection = 2
                SectionInf(M).sBlock = "自动"
                SectionInf(M).sSecFirName = TransferStaInf.sStaName
                SectionInf(M).sSecSecName = TransferStaInf.sSecInf(i - 1).sDownStaName
                SectionInf(M).nFirStaID = N - 1
                SectionInf(M).nSecStaID = N
                SectionInf(M).nHStation = N - 1
                SectionInf(M).nQStation = N
                SectionInf(M).lDistance(1) = 2
                SectionInf(M).lDistance(2) = 2

            End If
            N1 = i

            ReDim Preserve BasicTrainInf(N1 * 2)
            '下行车
            BasicTrainInf(N1 * 2 - 1).ComeStation = TransferStaInf.sSecInf(i - 1).sUpStaName
            BasicTrainInf(N1 * 2 - 1).NextStation = TransferStaInf.sSecInf(i - 1).sDownStaName
            BasicTrainInf(N1 * 2 - 1).StartStation = TransferStaInf.sSecInf(i - 1).sUpStaName
            BasicTrainInf(N1 * 2 - 1).EndStation = TransferStaInf.sSecInf(i - 1).sDownStaName
            BasicTrainInf(N1 * 2 - 1).nUporDown = 1
            ReDim Preserve BasicTrainInf(N1 * 2 - 1).TrainReturnStyle(2)
            ReDim Preserve BasicTrainInf(N1 * 2 - 1).StopScale(0)
            ReDim Preserve BasicTrainInf(N1 * 2 - 1).SecScale(0)
            ReDim Preserve BasicTrainInf(N1 * 2 - 1).nPathID(Num)
            ReDim Preserve BasicTrainInf(N1 * 2 - 1).Starting(Num)
            ReDim Preserve BasicTrainInf(N1 * 2 - 1).Arrival(Num)
            ReDim Preserve BasicTrainInf(N1 * 2 - 1).nPassSection(Num - 1)
            ReDim Preserve BasicTrainInf(N1 * 2 - 1).nFirstID(Num - 1)
            ReDim Preserve BasicTrainInf(N1 * 2 - 1).nSecondID(Num - 1)
            ReDim Preserve BasicTrainInf(N1 * 2 - 1).sSectionName(Num - 1)
            For j = 1 To Num
                BasicTrainInf(N1 * 2 - 1).nPathID(j) = N - Num + j
            Next
            For j = 1 To Num - 1
                BasicTrainInf(N1 * 2 - 1).nPassSection(j) = M - (Num - 1) + j
                BasicTrainInf(N1 * 2 - 1).nFirstID(j) = SectionInf(M - (Num - 1) + j).nFirStaID
                BasicTrainInf(N1 * 2 - 1).nSecondID(j) = SectionInf(M - (Num - 1) + j).nSecStaID
                BasicTrainInf(N1 * 2 - 1).sSectionName(j) = SectionInf(M - (Num - 1) + j).sSecName
            Next

            '上行车
            BasicTrainInf(N1 * 2).ComeStation = TransferStaInf.sSecInf(i - 1).sDownStaName
            BasicTrainInf(N1 * 2).NextStation = TransferStaInf.sSecInf(i - 1).sUpStaName
            BasicTrainInf(N1 * 2).StartStation = TransferStaInf.sSecInf(i - 1).sDownStaName
            BasicTrainInf(N1 * 2).EndStation = TransferStaInf.sSecInf(i - 1).sUpStaName
            BasicTrainInf(N1 * 2).nUporDown = 2
            ReDim Preserve BasicTrainInf(N1 * 2).TrainReturnStyle(2)
            ReDim Preserve BasicTrainInf(N1 * 2).StopScale(0)
            ReDim Preserve BasicTrainInf(N1 * 2).SecScale(0)
            ReDim Preserve BasicTrainInf(N1 * 2).nPathID(Num)
            ReDim Preserve BasicTrainInf(N1 * 2).Starting(Num)
            ReDim Preserve BasicTrainInf(N1 * 2).Arrival(Num)
            ReDim Preserve BasicTrainInf(N1 * 2).nPassSection(Num - 1)
            ReDim Preserve BasicTrainInf(N1 * 2).nFirstID(Num - 1)
            ReDim Preserve BasicTrainInf(N1 * 2).nSecondID(Num - 1)
            ReDim Preserve BasicTrainInf(N1 * 2).sSectionName(Num - 1)

            For j = Num To 1 Step -1
                BasicTrainInf(N1 * 2).nPathID(Num - j + 1) = N - Num + j
            Next
            For j = Num - 1 To 1 Step -1
                BasicTrainInf(N1 * 2).nPassSection(Num - j) = M - (Num - 1) + j
                BasicTrainInf(N1 * 2).nFirstID(Num - j) = SectionInf(M - (Num - 1) + j).nSecStaID
                BasicTrainInf(N1 * 2).nSecondID(Num - j) = SectionInf(M - (Num - 1) + j).nFirStaID
                BasicTrainInf(N1 * 2).sSectionName(Num - j) = SectionInf(M - (Num - 1) + j).sSecName
            Next
        Next

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

        ReDim BaseChediInfo(1)
        BaseChediInfo(1).SCheDiLeiXing = "车底"
        BaseChediInfo(1).bIfGouWang = 0
        ReDim BaseChediInfo(1).nLinkTrain(0)

        ReDim TrainInf(0)
        Call GetInterChangerAllTraininf()
        TimeTablePara.sPubCurSkbName = sCurStaName & " 车站运行图"
        ODSPubpara.sCurShowListState = "显示换乘站图"
        If UBound(TrainInf) = 0 Then
            MsgBox("对不起，没有找到该站的时刻表，请确认导入的运行图文件输出站名是否正确！", , "提示")
            Exit Sub
        End If
        Dim nf As New frmODSTimeTableMain
        nf.Show()
    End Sub
    '导入经过该换乘站的所有列车
    Private Sub GetInterChangerAllTraininf()

        Dim i, j, k, p, q, r As Integer
        Dim nCurRow As Integer
        nCurRow = 0
        Dim sCurVersion As String
        Dim sCurLineName As String
        ReDim TrainInf(0)
        Dim tmpID As Integer
        tmpID = 0
        Dim nTrain As Integer
        'Dim stmpSta As Integer
        Dim tmpBeforSta As Integer
        Dim tmpXH As Integer
        Dim nifIn As Integer
        Dim nStaID As Integer
        Progress.ProgressForm.StartProgress(TransferStaInf.sSecInf.Count + 1, "正在加载时刻表相关数据，请稍候...")
        Progress.ProgressForm.PerformStep()
        For r = 1 To UBound(DiaVer)
            sCurVersion = DiaVer(r).sCurDiaVersion
            sCurLineName = DiaVer(r).sLineName
            If sCurVersion <> "空" Then
                TMSlocalDataSet.TMS_STOCKUSINGINFO.Clear()
                TMSlocalDataSet.Fill("TMS_STOCKUSINGINFO", "TrainDiagramID='" & sCurVersion & "' order by StockSeq,LineSeq")
                If TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows.Count > 0 Then
                    tmpBeforSta = Int(TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(0).Item("StockSeq"))
                    'tmpID = tmpID + 1
                    'ReDim Preserve ChediInfo(tmpID)
                    'ReDim Preserve ChediInfo(tmpID).nLinkTrain(0)
                    'Call CopyCheDiInformation(tmpID, TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(0).Item("StockID"))
                    tmpXH = 1

                    For i = 1 To TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows.Count
                        'stmpSta = Int(TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("StockSeq"))
                        'If stmpSta <> tmpBeforSta Then
                        '    tmpBeforSta = stmpSta
                        '    tmpID = tmpID + 1
                        '    ReDim Preserve ChediInfo(tmpID)
                        '    ReDim Preserve ChediInfo(tmpID).nLinkTrain(0)
                        '    Call CopyCheDiInformation(tmpID, TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("Stockid"))
                        '    tmpXH = 1
                        'End If
                        'If tmpID > 0 Then
                        tmpXH = 1
                        nTrain = AddInterChangerTrainInformation(sCurLineName, Trim(TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("RunScaleStyle")), TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("StopScaleStyle"), TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("LinkTrainNum"))
                        If nTrain > 0 And TrainInf(nTrain).Train <> "" Then

                            j = nTrain
                            TrainInf(j).TrainPuorNot = 1
                            '*************************************************************************************************************************
                            ReDim Preserve TrainInf(j).Starting(UBound(StationInf))
                            ReDim Preserve TrainInf(j).Arrival(UBound(StationInf))
                            ReDim Preserve TrainInf(j).StopLine(UBound(StationInf))
                            '*************************************************************************************************************************
                            For k = 1 To UBound(StationInf)
                                TrainInf(j).Starting(k) = -1
                                TrainInf(j).Arrival(k) = -1
                                TrainInf(j).StopLine(k) = ""
                            Next k
                            TMSlocalDataSet.TMS_TIMETABLEINFO.Rows.Clear()
                            TMSlocalDataSet.Fill("TMS_TIMETABLEINFO", "TrainDiagramID='" & sCurVersion & "'and TrainNum='" & Trim(TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("LinkTrainNum")) & "' order by Seq")
                            If TMSlocalDataSet.TMS_TIMETABLEINFO.Rows.Count > 0 Then
                                nifIn = 0
                                For p = 1 To TMSlocalDataSet.TMS_TIMETABLEINFO.Rows.Count
                                    If p = TMSlocalDataSet.TMS_TIMETABLEINFO.Rows.Count - 1 Then
                                        TrainInf(j).sStartZFStarting = TMSlocalDataSet.TMS_TIMETABLEINFO.Rows(p - 1).Item("DepartTime") ' HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                                        TrainInf(j).sStartZFArrival = TMSlocalDataSet.TMS_TIMETABLEINFO.Rows(p - 1).Item("ArriTime") ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                                        TrainInf(j).sStartZFLine = TMSlocalDataSet.TMS_TIMETABLEINFO.Rows(p - 1).Item("StopTrack")
                                    ElseIf p = TMSlocalDataSet.TMS_TIMETABLEINFO.Rows.Count Then
                                        TrainInf(j).sEndZFStarting = TMSlocalDataSet.TMS_TIMETABLEINFO.Rows(p - 1).Item("DepartTime") 'HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                                        TrainInf(j).sEndZFArrival = TMSlocalDataSet.TMS_TIMETABLEINFO.Rows(p - 1).Item("ArriTime")  ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                                        TrainInf(j).sEndZFLine = TMSlocalDataSet.TMS_TIMETABLEINFO.Rows(p - 1).Item("StopTrack")
                                    Else
                                        For q = 1 To UBound(StationInf)
                                            If StationInf(q).sStationName = TMSlocalDataSet.TMS_TIMETABLEINFO.Rows(p - 1).Item("stationname").ToString.Trim Then
                                                nStaID = q
                                                TrainInf(j).Starting(nStaID) = TMSlocalDataSet.TMS_TIMETABLEINFO.Rows(p - 1).Item("DepartTime")
                                                TrainInf(j).Arrival(nStaID) = TMSlocalDataSet.TMS_TIMETABLEINFO.Rows(p - 1).Item("ArriTime")
                                                TrainInf(j).StopLine(nStaID) = TMSlocalDataSet.TMS_TIMETABLEINFO.Rows(p - 1).Item("StopTrack")
                                                nifIn = 1
                                            End If
                                        Next
                                    End If
                                Next p
                                TrainInf(j).lAllStartTime = TrainInf(j).Starting(TrainInf(j).nPathID(1))
                                TrainInf(j).lAllEndTime = TrainInf(j).Arrival(TrainInf(j).nPathID(UBound(TrainInf(j).nPathID)))
                            End If

                            If nifIn = 1 Then
                                TrainInf(nTrain).Train = Trim(TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("LinkTrainNum"))
                                TrainInf(nTrain).sPrintTrain = TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("PrintNum").ToString.Trim
                                TrainInf(nTrain).nZfLimit = Trim(TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("IfTurnFixed"))
                                TrainInf(nTrain).PrintLineStyle = GetLineStyleFromText(TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("LineShowStyle").ToString.Trim)
                                TrainInf(nTrain).PrintLineWidth = TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("LineShowWidth")
                                TrainInf(nTrain).PrintLineColor = System.Drawing.ColorTranslator.FromHtml(TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("LineShowColor"))
                                'ReDim Preserve ChediInfo(tmpID).nLinkTrain(UBound(ChediInfo(tmpID).nLinkTrain) + 1)
                                'ChediInfo(tmpID).nLinkTrain(UBound(ChediInfo(tmpID).nLinkTrain)) = nTrain
                            End If
                        End If
                        'ChediInfo(tmpID).sCheCiHao = TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("StockID") 'Left(rsDb.Fields("输出车次").Value, 3)
                        'ChediInfo(tmpID).PrintCheDiLinkStyle = GetLineStyleFromText(TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("StockShowStyle").ToString.Trim)
                        'ChediInfo(tmpID).PrintCheDiLinkWidth = TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("StockShowWidth")
                        'ChediInfo(tmpID).PrintCheDiLinkColor = System.Drawing.ColorTranslator.FromHtml(TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("StockShowColor").ToString.Trim)
                        'If TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(i - 1).Item("IfTurnFixed") = 1 Then
                        '    ChediInfo(tmpID).bIfGouWang = True
                        'Else
                        '    ChediInfo(tmpID).bIfGouWang = False
                        'End If
                        'Else
                        'MsgBox("车底信息与当前运行图信息不对应，请检查运行图和车底信息!","提示")
                        'End If
                        tmpXH = tmpXH + 1
                    Next i
                End If
            End If
            Progress.ProgressForm.PerformStep()
        Next
        ReDim ChediInfo(0)
        Progress.ProgressForm.EndProgress()
    End Sub


    '得到该站所有连接的车站名称
    Private Sub GetTransferLinkSta(ByVal sStaName As String)
        Dim i As Integer
        Dim sCurVersion As String
        Dim sCurLineName As String
        Dim sSecinf As New Generic.List(Of typeTransferSta)
        Dim sCurSta As String
        sCurSta = GetDiaStaNameFromPrintStaName(sStaName)
        TransferStaInf.sStaName = sCurSta
        For i = 1 To UBound(DiaVer)
            Dim tmpSecinf As New typeTransferSta
            sCurVersion = DiaVer(i).sCurDiaVersion
            sCurLineName = DiaVer(i).sLineName
            If sCurVersion <> "" Or sCurVersion <> "空" Then
                TMSlocalDataSet.TMS_SECTIONINFO.Clear()
                TMSlocalDataSet.Fill("TMS_SECTIONINFO", "TRAINDIAGRAMID='" & sCurVersion & "' order by SECTIONSEQ")
                For Each Row As DataAccessTier.OdsDataSet.TMS_SECTIONINFORow In TMSlocalDataSet.TMS_SECTIONINFO
                    If Row.STARTSTATIONNAME = sCurSta Then
                        tmpSecinf.sDownSecName = Row.SECTIONNAME
                        tmpSecinf.sLineName = sCurLineName
                        tmpSecinf.sDownStaName = Row.TERMINALSTATIONNAME
                    End If
                    If Row.TERMINALSTATIONNAME = sCurSta Then
                        tmpSecinf.sUpSecName = Row.SECTIONNAME
                        tmpSecinf.sLineName = sCurLineName
                        tmpSecinf.sUpStaName = Row.STARTSTATIONNAME
                    End If
                Next
            End If
            If tmpSecinf.sLineName <> "" Then
                sSecinf.Add(tmpSecinf)
            End If
        Next
        TransferStaInf.sSecInf = sSecinf
    End Sub

    Private Function AddInterChangerTrainInformation(ByVal sLineName As String, ByVal sTrainStyle As String, ByVal sStopScale As String, ByVal sCheci As String) As Integer
        Dim i, j As Integer
        Dim nWeiNum As Integer
        Dim nBaseId As Integer
        For j = 1 To TransferStaInf.sSecInf.Count
            If TransferStaInf.sSecInf(j - 1).sLineName = sLineName Then
                If Int(sCheci) Mod 2 = 0 Then
                    nBaseId = j * 2
                Else
                    nBaseId = j * 2 - 1
                End If
            End If
        Next
        If nBaseId = 0 Then
            AddInterChangerTrainInformation = 0
            Exit Function
        End If

        Dim nTrain As Integer
        nTrain = BasicTrainInf(nBaseId).nUporDown
        '先检查有无事先定义好的维数
        If nTrain Mod 2 = 0 Then
            For i = 1 To UBound(TrainInf)
                If i Mod 2 = 0 Then
                    If TrainInf(i).Train = "" Then
                        nWeiNum = i
                        If sCheci = "" Then
                            sCheci = nWeiNum
                        End If
                        Call CopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale)
                        'Call CopyFenTingBaseTrainInf(nBaseId, nWeiNum)
                        AddInterChangerTrainInformation = i
                        GoTo sEnd
                    End If
                End If
            Next i
        Else
            For i = 1 To UBound(TrainInf)
                If i Mod 2 <> 0 Then
                    If TrainInf(i).Train = "" Then
                        nWeiNum = i
                        If sCheci = "" Then
                            sCheci = nWeiNum
                        End If
                        Call CopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale)
                        'Call CopyFenTingBaseTrainInf(nBaseId, nWeiNum)
                        AddInterChangerTrainInformation = i
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
                    If sCheci = "" Then
                        sCheci = nWeiNum
                    End If
                    Call CopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale)
                    'Call CopyFenTingBaseTrainInf(nBaseId, nWeiNum)
                    AddInterChangerTrainInformation = nWeiNum
                    GoTo sEnd
                Else
                    nWeiNum = UBound(TrainInf) + 1
                    ReDim Preserve TrainInf(nWeiNum)
                    If sCheci = "" Then
                        sCheci = nWeiNum
                    End If
                    Call CopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale)
                    'Call CopyFenTingBaseTrainInf(nBaseId, nWeiNum)
                    AddInterChangerTrainInformation = nWeiNum
                    GoTo sEnd
                End If
            Else
                If UBound(TrainInf) Mod 2 = 0 Then
                    nWeiNum = UBound(TrainInf) + 1
                    ReDim Preserve TrainInf(nWeiNum)
                    If sCheci = "" Then
                        sCheci = nWeiNum
                    End If
                    Call CopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale)
                    'Call CopyFenTingBaseTrainInf(nBaseId, nWeiNum)
                    AddInterChangerTrainInformation = nWeiNum
                    GoTo sEnd
                Else
                    nWeiNum = UBound(TrainInf) + 2
                    ReDim Preserve TrainInf(nWeiNum)
                    If sCheci = "" Then
                        sCheci = nWeiNum
                    End If
                    Call CopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale)
                    'Call CopyFenTingBaseTrainInf(nBaseId, nWeiNum)
                    AddInterChangerTrainInformation = nWeiNum
                    GoTo sEnd
                End If
            End If
        End If
sEnd:
        Call SetTrainDefautColor(AddInterChangerTrainInformation)
    End Function
End Module
