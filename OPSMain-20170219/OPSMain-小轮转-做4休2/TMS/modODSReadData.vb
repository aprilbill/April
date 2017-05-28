Module modODSReadData

    '读入列车交路
    Public Sub ReadBaseTrainInf()
        Dim i, j, p As Integer
        Dim TMS_TRAININFO As New DataTable
        Dim sqlstr As String = "select * from TMS_TRAININFO where TraindiagramID='" & ODSPubpara.DiagramSelect & "' order by RoutingName"
        TMS_TRAININFO = Globle.Method.ReadDataForAccess(sqlstr)
       
        ReDim BasicTrainInf(TMS_TRAININFO.Rows.Count)
        Dim nJiNum As Int32
        Dim nOhNum As Int32
        nJiNum = 1
        nOhNum = 2
        If TMS_TRAININFO.Rows.Count > 0 Then
            For p = 1 To TMS_TRAININFO.Rows.Count
                With BasicTrainInf(p)
                    .sJiaoLuName = Trim(TMS_TRAININFO.Rows(p - 1).Item("RoutingName").ToString)
                    If Trim(TMS_TRAININFO.Rows(p - 1).Item("UporDown").ToString) = "上行" Then
                        .nUporDown = 2
                    Else
                        .nUporDown = 1
                    End If
                    .TrainStyle = Trim(TMS_TRAININFO.Rows(p - 1).Item("Type").ToString)
                    .StartStation = Trim(TMS_TRAININFO.Rows(p - 1).Item("OStationNAME").ToString)
                    .EndStation = Trim(TMS_TRAININFO.Rows(p - 1).Item("DStationNAME").ToString)
                    .ComeStation = Trim(TMS_TRAININFO.Rows(p - 1).Item("OStationNAME").ToString)
                    .NextStation = Trim(TMS_TRAININFO.Rows(p - 1).Item("DStationNAME").ToString)

                    ReDim Preserve .TrainReturnStyle(2)
                    .TrainReturnStyle(1) = Trim(TMS_TRAININFO.Rows(p - 1).Item("StartType").ToString)
                    .TrainReturnStyle(2) = Trim(TMS_TRAININFO.Rows(p - 1).Item("EndUseType").ToString)

                    .sTrainXingZhi = Trim(TMS_TRAININFO.Rows(p - 1).Item("TrainType").ToString)
                    .SCheDiLeiXing = Trim(TMS_TRAININFO.Rows(p - 1).Item("StockStyleID").ToString)

                    .sLineNum = Trim(TMS_TRAININFO.Rows(p - 1).Item("LINETRAINCODE").ToString)
                    .sMuDiNum = Trim(TMS_TRAININFO.Rows(p - 1).Item("ENDSIGN").ToString)

                    Dim IfIn As Integer
                    IfIn = 0

                    '初始化设置
                    ReDim .Arrival(UBound(StationInf))
                    ReDim .Starting(UBound(StationInf))
                    For j = 1 To UBound(StationInf)
                        .Arrival(j) = -1
                        .Starting(j) = -1
                    Next j

                    '以下代码读入停站信息
                    Dim TMS_STOPSCALEINFO As New DataTable
                    sqlstr = "select * from TMS_STOPSCALEINFO where TraindiagramID='" & ODSPubpara.DiagramSelect & "' and RoutingName='" & .sJiaoLuName & "' order by Stoptype,Seqnum"
                    TMS_STOPSCALEINFO = Globle.Method.ReadDataForAccess(sqlstr)

                    Dim sFirName As String
                    Dim ntmpCurID As Integer
                    ReDim .StopScale(0)
                    Dim nIfIn As Boolean
                    nIfIn = False
                    If TMS_STOPSCALEINFO.Rows.Count > 0 Then

                        For i = 1 To TMS_STOPSCALEINFO.Rows.Count
                            nIfIn = False
                            For j = 1 To UBound(.StopScale)
                                If TMS_STOPSCALEINFO.Rows(i - 1).Item("Stoptype").ToString.Trim = .StopScale(j).sName.ToString.Trim Then
                                    nIfIn = True
                                    ntmpCurID = j
                                    Exit For
                                End If
                            Next
                            If nIfIn = True Then
                                ReDim Preserve .StopScale(ntmpCurID).nStopStation(UBound(.StopScale(ntmpCurID).nStopStation) + 1)
                                ReDim Preserve .StopScale(ntmpCurID).StopTime(UBound(.StopScale(ntmpCurID).StopTime) + 1)
                                ReDim Preserve .StopScale(ntmpCurID).sScaleName(UBound(.StopScale(ntmpCurID).sScaleName) + 1)
                                .StopScale(ntmpCurID).nStopStation(UBound(.StopScale(ntmpCurID).nStopStation)) = FromStaNameToStaIDByStationinf(TMS_STOPSCALEINFO.Rows(i - 1).Item("Staionname"))
                                .StopScale(ntmpCurID).StopTime(UBound(.StopScale(ntmpCurID).StopTime)) = MinuteToSecond(TMS_STOPSCALEINFO.Rows(i - 1).Item("StopTime"))
                                .StopScale(ntmpCurID).sScaleName(UBound(.StopScale(ntmpCurID).sScaleName)) = TMS_STOPSCALEINFO.Rows(i - 1).Item("StopScaleName").ToString.Trim
                                .StopScale(ntmpCurID).nStopNum = .StopScale(ntmpCurID).nStopNum + 1
                            Else
                                ReDim Preserve .StopScale(UBound(.StopScale) + 1)
                                ntmpCurID = UBound(.StopScale)
                                ReDim Preserve .StopScale(ntmpCurID).nStopStation(0)
                                ReDim Preserve .StopScale(ntmpCurID).StopTime(0)
                                ReDim Preserve .StopScale(ntmpCurID).sScaleName(0)
                                .StopScale(ntmpCurID).sName = TMS_STOPSCALEINFO.Rows(i - 1).Item("Stoptype").ToString.Trim
                                ReDim Preserve .StopScale(ntmpCurID).nStopStation(UBound(.StopScale(ntmpCurID).nStopStation) + 1)
                                ReDim Preserve .StopScale(ntmpCurID).StopTime(UBound(.StopScale(ntmpCurID).StopTime) + 1)
                                ReDim Preserve .StopScale(ntmpCurID).sScaleName(UBound(.StopScale(ntmpCurID).sScaleName) + 1)
                                .StopScale(ntmpCurID).nStopStation(UBound(.StopScale(ntmpCurID).nStopStation)) = FromStaNameToStaIDByStationinf(TMS_STOPSCALEINFO.Rows(i - 1).Item("Staionname"))
                                .StopScale(ntmpCurID).StopTime(UBound(.StopScale(ntmpCurID).StopTime)) = MinuteToSecond(TMS_STOPSCALEINFO.Rows(i - 1).Item("StopTime"))
                                .StopScale(ntmpCurID).sScaleName(UBound(.StopScale(ntmpCurID).sScaleName)) = TMS_STOPSCALEINFO.Rows(i - 1).Item("StopScaleName").ToString.Trim
                                .StopScale(ntmpCurID).nStopNum = .StopScale(ntmpCurID).nStopNum + 1
                            End If
                        Next i
                    End If


                    '以下代码读入区间运行时分信息
                    Dim TMS_RUNSCALEINFO As New DataTable
                    sqlstr = "select * from TMS_RUNSCALEINFO where TraindiagramID='" & ODSPubpara.DiagramSelect & "' and RoutingName='" & .sJiaoLuName & "' order by RUNTYPE,sectionseq"
                    TMS_RUNSCALEINFO = Globle.Method.ReadDataForAccess(sqlstr)

                    sFirName = ""
                    ntmpCurID = 0
                    ReDim .SecScale(0)
                    If TMS_RUNSCALEINFO.Rows.Count > 0 Then

                        ntmpCurID = 1
                        nIfIn = False
                        For i = 1 To TMS_RUNSCALEINFO.Rows.Count
                            nIfIn = False
                            For j = 1 To UBound(.SecScale)
                                If TMS_RUNSCALEINFO.Rows(i - 1).Item("runtype").ToString.Trim = .SecScale(j).sName.ToString.Trim Then
                                    nIfIn = True
                                    ntmpCurID = j
                                    Exit For
                                End If
                            Next

                            If nIfIn = True Then
                                ReDim Preserve .SecScale(ntmpCurID).nSecID(UBound(.SecScale(ntmpCurID).nSecID) + 1)
                                ReDim Preserve .SecScale(ntmpCurID).RunTime(UBound(.SecScale(ntmpCurID).RunTime) + 1)
                                ReDim Preserve .SecScale(ntmpCurID).sScaleName(UBound(.SecScale(ntmpCurID).sScaleName) + 1)
                                .SecScale(ntmpCurID).nSecID(UBound(.SecScale(ntmpCurID).nSecID)) = GetSectionID(TMS_RUNSCALEINFO.Rows(i - 1).Item("SectionName"))
                                .SecScale(ntmpCurID).RunTime(UBound(.SecScale(ntmpCurID).RunTime)) = MinuteToSecond(TMS_RUNSCALEINFO.Rows(i - 1).Item("SecRunTime"))
                                .SecScale(ntmpCurID).sScaleName(UBound(.SecScale(ntmpCurID).sScaleName)) = TMS_RUNSCALEINFO.Rows(i - 1).Item("RunScaleName").ToString.Trim
                            Else
                                ReDim Preserve .SecScale(UBound(.SecScale) + 1)
                                ntmpCurID = UBound(.SecScale)
                                ReDim Preserve .SecScale(ntmpCurID).nSecID(0)
                                ReDim Preserve .SecScale(ntmpCurID).RunTime(0)
                                ReDim Preserve .SecScale(ntmpCurID).sScaleName(0)
                                .SecScale(ntmpCurID).sName = TMS_RUNSCALEINFO.Rows(i - 1).Item("runtype")
                                sFirName = TMS_RUNSCALEINFO.Rows(i - 1).Item("runtype")
                                ReDim Preserve .SecScale(ntmpCurID).nSecID(UBound(.SecScale(ntmpCurID).nSecID) + 1)
                                ReDim Preserve .SecScale(ntmpCurID).RunTime(UBound(.SecScale(ntmpCurID).RunTime) + 1)
                                ReDim Preserve .SecScale(ntmpCurID).sScaleName(UBound(.SecScale(ntmpCurID).sScaleName) + 1)
                                .SecScale(ntmpCurID).nSecID(UBound(.SecScale(ntmpCurID).nSecID)) = GetSectionID(TMS_RUNSCALEINFO.Rows(i - 1).Item("SectionName"))
                                .SecScale(ntmpCurID).RunTime(UBound(.SecScale(ntmpCurID).RunTime)) = MinuteToSecond(TMS_RUNSCALEINFO.Rows(i - 1).Item("SecRunTime"))
                                .SecScale(ntmpCurID).sScaleName(UBound(.SecScale(ntmpCurID).sScaleName)) = TMS_RUNSCALEINFO.Rows(i - 1).Item("RunScaleName").ToString.Trim
                            End If
                        Next i
                    End If

                    '以下代码读入列车径路信息

                    Dim TrainPath As String
                    TrainPath = Trim(TMS_TRAININFO.Rows(p - 1).Item("PASSSTAID"))
                    If Right(TrainPath, 1) = "," Or Right(TrainPath, 1) = "，" Then
                    Else
                        TrainPath = TrainPath & ","
                    End If
                    Dim TrainPathSta() As String
                    ReDim TrainPathSta(0)

                    '                '当列车径路字段的值为空时，出错，返回
                    '                If Trim(TrainPath) = "" Or Trim(TrainPath) = "无" Then
                    '                    Call InputErrToTrainErrInf(.Train, "列车的列车径路信息为空!","提示")
                    '                    Exit Sub
                    '                End If

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
                        ' Call InputErrToTrainErrInf(.Train, "列车径路通过的车站只有一个，不符合要求！")
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
                            If KK = 0 And j = UBound(SectionInf) Then
                                'Call InputErrToTrainErrInf(.Train, "区间" & strQuJian1 & "不存在，请确认底图打开是否有错！")
                                ' Stop
                            End If

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

                    '加入分岔站信息
                    .NumWay = 0
                    ReDim .Way1(0)
                    ReDim .Way2(0)
                    ReDim .Way3(0)

                    For i = 1 To UBound(.nPathID)
                        If Left(StationInf(.nPathID(i)).sStationProp, 1) = "F" Then
                            .NumWay = .NumWay + 1
                            ReDim Preserve .Way1(.NumWay)
                            ReDim Preserve .Way2(.NumWay)
                            ReDim Preserve .Way3(.NumWay)
                            .Way1(.NumWay) = StationInf(.nPathID(i)).sStationName
                            .Way2(.NumWay) = GetBeforOrAfterStaNameFromCurSta(p, StationInf(.nPathID(i)).sStationName, 2)
                            .Way3(.NumWay) = GetBeforOrAfterStaNameFromCurSta(p, StationInf(.nPathID(i)).sStationName, 1)
                        End If
                    Next i
                End With
            Next p
        Else
            Exit Sub
        End If
    End Sub

    '打开车底信息和发车间隔安排
    Public Sub InputChediAndTrainJianGeData(ByVal sState As String)
        Dim i As Integer
        Dim TMS_TRAINUSINGINFO, TMS_TRAININTERVALINFO As New DataTable
        Dim sqlstr As String = "select * from TMS_TRAINUSINGINFO where TraindiagramID='" & ODSPubpara.DiagramSelect & "' order by StockStyleID"
        TMS_TRAINUSINGINFO = Globle.Method.ReadDataForAccess(sqlstr)
        ReDim BaseChediInfo(TMS_TRAINUSINGINFO.Rows.Count)
        If IsNothing(TMS_TRAINUSINGINFO) = False AndAlso TMS_TRAINUSINGINFO.Rows.Count > 0 Then
            For i = 1 To TMS_TRAINUSINGINFO.Rows.Count
                With BaseChediInfo(i)
                    .SCheDiLeiXing = TMS_TRAINUSINGINFO.Rows(i - 1).Item("StockStyleName")
                    .bIfGouWang = 0
                    ReDim BaseChediInfo(i).nLinkTrain(0)
                End With
            Next
        End If


        'Do While RSChediInfo.EOF <> True
        '    ReDim Preserve ChediInfo(UBound(ChediInfo) + 1)
        '    i = UBound(ChediInfo)
        '    With ChediInfo(i)
        '        .SCheDiLeiXing = RSChediInfo.Fields("车底类型").Value
        '        .sCheDiID = RSChediInfo.Fields("车底ID").Value
        '        .sCheCiHao = .sCheDiID
        '        '.sStation = RSChediInfo.Fields("车站名称")
        '        .sDayBeginStation = RSChediInfo.Fields("日始出发车站").Value
        '        .sDayEndStation = RSChediInfo.Fields("日终停留车站").Value
        '        '.sRukuZhouqi = RSChediInfo.Fields("入库周期")
        '        '.lYunyongTime = RSChediInfo.Fields("运用时间")
        '        .nYunxingBiaochi = RSChediInfo.Fields("运行标尺").Value
        '        .bIfGouWang = False
        '        .sChuKuTime = HourToSecond(RSChediInfo.Fields("出库时间").Value)
        '        '.sChuKuFangShi = RSChediInfo.Fields("出库方式")
        '        .sRuKuTime = HourToSecond(RSChediInfo.Fields("入库时间").Value)
        '        '.sRuKuFangShi = RSChediInfo.Fields("入库方式")
        '        '.sChuKuSta = RSChediInfo.Fields("出库车站")
        '        '.sRuKuSta = RSChediInfo.Fields("入库车站")
        '        .sBeiZhu = RSChediInfo.Fields("备注").Value
        '        .sYunYongFangShi = RSChediInfo.Fields("运用方式").Value
        '        .nIfFixCheDi = 0
        '        'ReDim ChediInfo(i).nDayItem(0)
        '        .PrintCheDiLinkStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.CheDiLineStyle)
        '        .PrintCheDiLinkWidth = TimeTablePara.DiagramStylePara.CheDiLineWidth
        '        .PrintCheDiLinkColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.CheDiLineColor)
        '        ReDim ChediInfo(i).nLinkTrain(0)
        '        'ReDim ChediInfo(i).bIfEnterGZ(0)
        '    End With
        '    RSChediInfo.MoveNext()
        'Loop
        Dim stmpSta As String
        Dim tmpBeforSta As String
        Dim stmpPH As String
        Dim stmpBePH As String

        Dim tmpID As Integer
        Dim tmpXH As Integer
        ReDim GaoFenTimeSet(1)
        tmpID = 1
        ReDim GaoFenTimeSet(tmpID).nXuHao(0)
        ReDim GaoFenTimeSet(tmpID).BeTime(0)
            ReDim GaoFenTimeSet(tmpID).EndTime(0)
            ReDim GaoFenTimeSet(tmpID).JGtime(0)
            ReDim GaoFenTimeSet(tmpID).JGOne(0)
            ReDim GaoFenTimeSet(tmpID).JGTwo(0)
            ReDim GaoFenTimeSet(tmpID).NumOne(0)
            ReDim GaoFenTimeSet(tmpID).NumTwo(0)
            ReDim GaoFenTimeSet(tmpID).ChediNum(0)
            ReDim GaoFenTimeSet(tmpID).lZhouQiTime(0)
            ReDim GaoFenTimeSet(tmpID).sRunScaleName(0)
            ReDim GaoFenTimeSet(tmpID).sStopScaleName(0)
            ReDim GaoFenTimeSet(tmpID).lFirZheFanTime(0)
            ReDim GaoFenTimeSet(tmpID).lEndZheFanTime(0)
            ReDim GaoFenTimeSet(tmpID).lDownRunTime(0)
            ReDim GaoFenTimeSet(tmpID).lUpRunTime(0)
        ReDim GaoFenTimeSet(tmpID).lDownStopTime(0)
        ReDim GaoFenTimeSet(tmpID).lUpStopTime(0)
        sqlstr = "select * from TMS_TRAININTERVALINFO where TRAiNDIAGRAMID='" & ODSPubpara.DiagramSelect & "' order by ROUTINGNAME,TrainDiaStyleID,TimeID"
        TMS_TRAININTERVALINFO = Globle.Method.ReadDataForAccess(sqlstr)
        If TMS_TRAININTERVALINFO.Rows.Count > 0 Then
            tmpBeforSta = TMS_TRAININTERVALINFO.Rows(0).Item("ROUTINGNAME")
            stmpBePH = TMS_TRAININTERVALINFO.Rows(0).Item("TrainDiaStyleID")
            tmpID = 1
            ReDim GaoFenTimeSet(tmpID)
            GaoFenTimeSet(tmpID).sJLstyle = tmpBeforSta
            GaoFenTimeSet(tmpID).sPuHuaStyle = stmpBePH
            tmpXH = 1
            For i = 1 To TMS_TRAININTERVALINFO.Rows.Count
                stmpSta = TMS_TRAININTERVALINFO.Rows(i - 1).Item("ROUTINGNAME")
                stmpPH = TMS_TRAININTERVALINFO.Rows(i - 1).Item("TrainDiaStyleID")
                If stmpSta = tmpBeforSta Then
                    If stmpPH <> stmpBePH Then
                        stmpBePH = stmpPH
                        tmpID = tmpID + 1
                        ReDim Preserve GaoFenTimeSet(tmpID)
                        GaoFenTimeSet(tmpID).sJLstyle = tmpBeforSta
                        GaoFenTimeSet(tmpID).sPuHuaStyle = stmpBePH

                        ReDim GaoFenTimeSet(tmpID).nXuHao(0)
                        ReDim GaoFenTimeSet(tmpID).BeTime(0)
                        ReDim GaoFenTimeSet(tmpID).EndTime(0)
                        ReDim GaoFenTimeSet(tmpID).JGtime(0)
                        ReDim GaoFenTimeSet(tmpID).JGOne(0)
                        ReDim GaoFenTimeSet(tmpID).JGTwo(0)
                        ReDim GaoFenTimeSet(tmpID).NumOne(0)
                        ReDim GaoFenTimeSet(tmpID).NumTwo(0)
                        ReDim GaoFenTimeSet(tmpID).ChediNum(0)
                        ReDim GaoFenTimeSet(tmpID).lZhouQiTime(0)
                        ReDim GaoFenTimeSet(tmpID).sRunScaleName(0)
                        ReDim GaoFenTimeSet(tmpID).sStopScaleName(0)
                        ReDim GaoFenTimeSet(tmpID).lFirZheFanTime(0)
                        ReDim GaoFenTimeSet(tmpID).lEndZheFanTime(0)
                        ReDim GaoFenTimeSet(tmpID).lDownRunTime(0)
                        ReDim GaoFenTimeSet(tmpID).lUpRunTime(0)
                        ReDim GaoFenTimeSet(tmpID).lDownStopTime(0)
                        ReDim GaoFenTimeSet(tmpID).lUpStopTime(0)
                        tmpXH = 1
                    End If
                Else
                    tmpBeforSta = stmpSta
                    stmpBePH = stmpPH
                    tmpID = tmpID + 1
                    ReDim Preserve GaoFenTimeSet(tmpID)
                    GaoFenTimeSet(tmpID).sJLstyle = tmpBeforSta
                    GaoFenTimeSet(tmpID).sPuHuaStyle = stmpBePH

                    ReDim GaoFenTimeSet(tmpID).nXuHao(0)
                    ReDim GaoFenTimeSet(tmpID).BeTime(0)
                    ReDim GaoFenTimeSet(tmpID).EndTime(0)
                    ReDim GaoFenTimeSet(tmpID).JGtime(0)
                    ReDim GaoFenTimeSet(tmpID).JGOne(0)
                    ReDim GaoFenTimeSet(tmpID).JGTwo(0)
                    ReDim GaoFenTimeSet(tmpID).NumOne(0)
                    ReDim GaoFenTimeSet(tmpID).NumTwo(0)
                    ReDim GaoFenTimeSet(tmpID).ChediNum(0)
                    ReDim GaoFenTimeSet(tmpID).lZhouQiTime(0)
                    ReDim GaoFenTimeSet(tmpID).sRunScaleName(0)
                    ReDim GaoFenTimeSet(tmpID).sStopScaleName(0)
                    ReDim GaoFenTimeSet(tmpID).lFirZheFanTime(0)
                    ReDim GaoFenTimeSet(tmpID).lEndZheFanTime(0)
                    ReDim GaoFenTimeSet(tmpID).lDownRunTime(0)
                    ReDim GaoFenTimeSet(tmpID).lUpRunTime(0)
                    ReDim GaoFenTimeSet(tmpID).lDownStopTime(0)
                    ReDim GaoFenTimeSet(tmpID).lUpStopTime(0)
                    tmpXH = 1
                End If

                ReDim Preserve GaoFenTimeSet(tmpID).nXuHao(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).BeTime(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).EndTime(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).JGtime(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).JGOne(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).JGTwo(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).NumOne(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).NumTwo(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).ChediNum(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).lZhouQiTime(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).sRunScaleName(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).sStopScaleName(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).lFirZheFanTime(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).lEndZheFanTime(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).lDownRunTime(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).lUpRunTime(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).lDownStopTime(tmpXH)
                ReDim Preserve GaoFenTimeSet(tmpID).lUpStopTime(tmpXH)

                GaoFenTimeSet(tmpID).nXuHao(tmpXH) = TMS_TRAININTERVALINFO.Rows(i - 1).Item("TimeID")
                GaoFenTimeSet(tmpID).BeTime(tmpXH) = AddLitterTime(HourToSecond(TMS_TRAININTERVALINFO.Rows(i - 1).Item("StartTime")))
                GaoFenTimeSet(tmpID).EndTime(tmpXH) = AddLitterTime(HourToSecond(TMS_TRAININTERVALINFO.Rows(i - 1).Item("EndTime")))
                GaoFenTimeSet(tmpID).JGtime(tmpXH) = MinuteToSecond(TMS_TRAININTERVALINFO.Rows(i - 1).Item("Inteval"))
                GaoFenTimeSet(tmpID).JGOne(tmpXH) = MinuteToSecond(TMS_TRAININTERVALINFO.Rows(i - 1).Item("IntevalOne"))
                GaoFenTimeSet(tmpID).JGTwo(tmpXH) = MinuteToSecond(TMS_TRAININTERVALINFO.Rows(i - 1).Item("IntevalTwo"))
                GaoFenTimeSet(tmpID).NumOne(tmpXH) = TMS_TRAININTERVALINFO.Rows(i - 1).Item("NumOne")
                GaoFenTimeSet(tmpID).NumTwo(tmpXH) = TMS_TRAININTERVALINFO.Rows(i - 1).Item("NumTwo")
                GaoFenTimeSet(tmpID).ChediNum(tmpXH) = TMS_TRAININTERVALINFO.Rows(i - 1).Item("StockNum")
                GaoFenTimeSet(tmpID).lZhouQiTime(tmpXH) = TMS_TRAININTERVALINFO.Rows(i - 1).Item("CycleTime")
                GaoFenTimeSet(tmpID).sRunScaleName(tmpXH) = TMS_TRAININTERVALINFO.Rows(i - 1).Item("RunScaleName")
                GaoFenTimeSet(tmpID).sStopScaleName(tmpXH) = TMS_TRAININTERVALINFO.Rows(i - 1).Item("StopScaleName")
                GaoFenTimeSet(tmpID).lFirZheFanTime(tmpXH) = TMS_TRAININTERVALINFO.Rows(i - 1).Item("StartTurnTime")
                GaoFenTimeSet(tmpID).lEndZheFanTime(tmpXH) = TMS_TRAININTERVALINFO.Rows(i - 1).Item("EndTurnTime")
                tmpXH = tmpXH + 1
            Next i

        End If

        '导入停站信息数据
        'ReDim TrainStop(0)
        'RSdata = DBChediJiche.OpenRecordset("select * from 列车停站安排 order by 站序")
        'If RSdata.RecordCount > 0 Then
        '    RSdata.MoveLast()
        '    Tnum = RSdata.RecordCount
        'Else
        '    Tnum = 0
        'End If
        'If Tnum > 0 Then
        '    ReDim TrainStop(Tnum)
        '    RSdata.MoveFirst()
        '    For i = 1 To Tnum
        '        ReDim TrainStop(i).nDownStopTime(0)
        '        ReDim TrainStop(i).nDownStopTrain(0)
        '        ReDim TrainStop(i).nUpStopTime(0)
        '        ReDim TrainStop(i).nUpStopTrain(0)
        '        TrainStop(i).sStaName = Trim(RSdata.Fields("车站名称").Value)
        '        TrainStop(i).sStopTime = Trim(RSdata.Fields("停站时间").Value)
        '        TrainStop(i).sMinStopInterval = MinuteToSecond(Trim(RSdata.Fields("停站最小间隔").Value))
        '        TrainStop(i).sMaxStopInterval = MinuteToSecond(Trim(RSdata.Fields("停站最大间隔").Value))
        '        TrainStop(i).nMaxStopTrainNum = Trim(RSdata.Fields("最大停车数量").Value)
        '        RSdata.MoveNext()
        '    Next i
        'End If


        '车站可使用的时间段安排（广州站）
        'ReDim GuDaoUseTime(0)
        'RSdata = DBChediJiche.OpenRecordset("select * from 广州站股道可占用时间 order by 时间段")
        'If RSdata.RecordCount > 0 Then
        '    RSdata.MoveLast()
        '    Tnum = RSdata.RecordCount
        'Else
        '    Tnum = 0
        'End If
        'If Tnum > 0 Then
        '    ReDim GuDaoUseTime(1)
        '    ReDim GuDaoUseTime(1).sShiJianDuan(Tnum)
        '    ReDim GuDaoUseTime(1).sBeTime(Tnum)
        '    ReDim GuDaoUseTime(1).sEndTime(Tnum)
        '    GuDaoUseTime(1).sStaName = "广州"
        '    RSdata.MoveFirst()
        '    For i = 1 To Tnum
        '        GuDaoUseTime(1).sShiJianDuan(i) = RSdata.Fields("时间段").Value
        '        GuDaoUseTime(1).sBeTime(i) = HourToSecond(RSdata.Fields("起始时间").Value)
        '        GuDaoUseTime(1).sEndTime(i) = HourToSecond(RSdata.Fields("终止时间").Value)
        '        RSdata.MoveNext()
        '    Next i
        'End If

        '读入车站股道使用方案
        'Dim stmpValue() As String
        'Dim j As Integer
        'ReDim stmpValue(0)
        'ReDim staTrackUseInf(0)
        'RSdata = DBChediJiche.OpenRecordset("select * from 车站股道使用安排 order by ID")
        'If RSdata.RecordCount > 0 Then
        '    RSdata.MoveLast()
        '    Tnum = RSdata.RecordCount
        'Else
        '    Tnum = 0
        'End If
        'If Tnum > 0 Then
        '    ReDim staTrackUseInf(Tnum)
        '    RSdata.MoveFirst()
        '    For i = 1 To Tnum
        '        ReDim staTrackUseInf(i).sUseSeq(0)
        '        staTrackUseInf(i).sJiaoLuName = RSdata.Fields("交路类型").Value.ToString.Trim
        '        staTrackUseInf(i).sStaName = RSdata.Fields("车站名称").Value.ToString.Trim
        '        staTrackUseInf(i).sGudaoUse = RSdata.Fields("股道使用顺序").Value.ToString.Trim
        '        Call SplitString(stmpValue, RSdata.Fields("股道使用顺序").Value.ToString.Trim)
        '        For j = 1 To UBound(stmpValue)
        '            ReDim Preserve staTrackUseInf(i).sUseSeq(UBound(staTrackUseInf(i).sUseSeq) + 1)
        '            staTrackUseInf(i).sUseSeq(UBound(staTrackUseInf(i).sUseSeq)) = stmpValue(j)
        '        Next
        '        RSdata.MoveNext()
        '    Next i
        'End If


    End Sub
    '读入折返站
    Public Sub InputChediZhefanDataAndCheDiScaleInf()
        Dim TMS_TRAINRETURNTIME As New DataTable
        Dim sqlstr As String = "select * from TMS_TRAINRETURNTIME where TraindiagramID='" & ODSPubpara.DiagramSelect & "'"
        TMS_TRAINRETURNTIME = Globle.Method.ReadDataForAccess(sqlstr)
        Dim i As Integer
        ReDim ChediZhefanBiaozhunInfo(TMS_TRAINRETURNTIME.Rows.Count)
        For i = 1 To TMS_TRAINRETURNTIME.Rows.Count
            With ChediZhefanBiaozhunInfo(i)
                .SCheDiLeiXing = TMS_TRAINRETURNTIME.Rows(i - 1).Item("StockStyleNAME")
                .sZhefanStation = TMS_TRAINRETURNTIME.Rows(i - 1).Item("StationName")
                .lLijiZhefanTime = MinuteToSecond(TMS_TRAINRETURNTIME.Rows(i - 1).Item("ImmiTurnTime"))
                .lZhanQianTime = MinuteToSecond(TMS_TRAINRETURNTIME.Rows(i - 1).Item("BeforeTurnTime"))
                .lZhanHouTime = MinuteToSecond(TMS_TRAINRETURNTIME.Rows(i - 1).Item("AfterTurnTime"))
                .nFirRunTime = MinuteToSecond(TMS_TRAINRETURNTIME.Rows(i - 1).Item("ArriToTurnTime"))
                .nSecRunTime = MinuteToSecond(TMS_TRAINRETURNTIME.Rows(i - 1).Item("TurnToDepartTime"))
                .nArrFaDaoTime = MinuteToSecond(TMS_TRAINRETURNTIME.Rows(i - 1).Item("ArriStartArriTime"))
                .nStartFaDaoTime = MinuteToSecond(TMS_TRAINRETURNTIME.Rows(i - 1).Item("StartStartArriTime"))
            End With
        Next
        '   读入车底标尺信息,select distinct 信息没有呀
        ReDim TrainRunScaleInf(1)
        TrainRunScaleInf(1).nScaleID = 1
        TrainRunScaleInf(1).sScaleName = "正常运行"
        'TMSlocalDataSet.Fill("TMS_RUNSCALE", " TraindiagramID='" & ODSPubpara.DiagramSelect & "' order by SecScaleNum asc ")
        'If TMSlocalDataSet.TMS_RUNSCALE.Rows.Count > 0 Then
        '    ReDim TrainRunScaleInf(TMSlocalDataSet.TMS_RUNSCALE.Rows.Count)
        '    For j = 1 To TMSlocalDataSet.TMS_RUNSCALE.Rows.Count
        '        TrainRunScaleInf(j).nScaleID = Val(TMSlocalDataSet.TMS_RUNSCALE.Rows(j - 1).Item("SecScaleNum"))
        '        TrainRunScaleInf(j).sScaleName = TMSlocalDataSet.TMS_RUNSCALE.Rows(j - 1).Item("SecScaleName").ToString.Trim
        '    Next j
        'End If
    End Sub

    '读入时刻表车站顺序
    Public Sub ReadSKBStaSeqData()
        Dim i As Integer
        Dim j As Integer
        ReDim SkbStnSeq(0)
        Dim TMS_TIMETABLESTASEQ As New DataTable
        Dim sqlstr As String = "select * from TMS_TIMETABLESTASEQ order by STASEQNAME,Staseq"
        TMS_TIMETABLESTASEQ = Globle.Method.ReadDataForAccess(sqlstr)

        Dim nCurID As Integer
        Dim nCurV As Integer

        nCurV = 1
        For i = 1 To TMS_TIMETABLESTASEQ.Rows.Count
            nCurID = 0
            For j = 1 To UBound(SkbStnSeq)
                If SkbStnSeq(j).sQDName = TMS_TIMETABLESTASEQ.Rows(j - 1).Item("STASEQNAME") Then
                    nCurID = j
                    Exit For
                End If
            Next
            If nCurID > 0 Then
                ReDim Preserve SkbStnSeq(j).nStnSeq(UBound(SkbStnSeq(j).nStnSeq) + 1)
                SkbStnSeq(j).nStnSeq(UBound(SkbStnSeq(j).nStnSeq)) = StaNameToStaInfID(TMS_TIMETABLESTASEQ.Rows(j - 1).Item("STATIONNAME"))
            Else
                ReDim Preserve SkbStnSeq(UBound(SkbStnSeq) + 1)
                ReDim SkbStnSeq(UBound(SkbStnSeq)).nStnSeq(0)
                SkbStnSeq(UBound(SkbStnSeq)).sQDName = TMS_TIMETABLESTASEQ.Rows(j - 1).Item("STASEQNAME")
            End If
        Next
    End Sub

    '读入车站等信息
    Public Sub InputStationGudaoAndJinLuInfByDAO()
        Dim i, j As Integer
        Dim Str As String
        Dim TMS_STATIONINFO As New DataTable
        Dim sqlstr As String = "select * from TMS_STATIONINFO where TraindiagramID='" & ODSPubpara.DiagramSelect & "'"
        TMS_STATIONINFO = Globle.Method.ReadDataForAccess(sqlstr)
        For i = 1 To UBound(StationInf)
            For j = 1 To TMS_STATIONINFO.Rows.Count
                If TMS_STATIONINFO.Rows(j - 1).Item("stationname").ToString = StationInf(i).sStationName Then
                    StationInf(i).sStaStyle = TMS_STATIONINFO.Rows(j - 1).Item("StationType").ToString.Trim
                    StationInf(i).sAtLineName = TMS_STATIONINFO.Rows(j - 1).Item("LINENAME").ToString.Trim
                    StationInf(i).sPrintStaName = TMS_STATIONINFO.Rows(j - 1).Item("OutputName").ToString.Trim
                    StationInf(i).sStaProperity = TMS_STATIONINFO.Rows(j - 1).Item("StationCharacter").ToString.Trim
                    StationInf(i).sStationProp = ChaStProp(TMS_STATIONINFO.Rows(j - 1).Item("StationCharacter").ToString.Trim, TMS_STATIONINFO.Rows(0).Item("StationType").ToString.Trim)
                    StationInf(i).sEnglishName = TMS_STATIONINFO.Rows(j - 1).Item("EnglishShortName").ToString.Trim
                    Exit For
                End If
            Next
            Dim TMS_LINEDRAW As New DataTable
            sqlstr = "select * from TMS_LINEDRAW where name='" & StationInf(i).sStationName & "'and TraindiagramID='" & ODSPubpara.DiagramSelect & "'"
            TMS_LINEDRAW = Globle.Method.ReadDataForAccess(sqlstr)
            StationInf(i).nStLineNum = 0
            ReDim StationInf(i).sStLineNo(0)
            ReDim StationInf(i).nStLineUse(0)
            ReDim StationInf(i).sLineUse(0)
            ReDim StationInf(i).sUpOrDownUse(0)
            ReDim StationInf(i).sGuDaoUseSeq(0)
            ReDim StationInf(i).sGuDaoName(0)
            If IsNothing(TMS_LINEDRAW) = False AndAlso TMS_LINEDRAW.Rows.Count > 0 Then
                For j = 1 To TMS_LINEDRAW.Rows.Count
                    Str = TMS_LINEDRAW.Rows(j - 1).Item("PARTLINETYPE").ToString.Trim
                    If Str.Length >= 3 Then
                        If Str.Substring(Str.Length - 3) = "股道线" Then
                            StationInf(i).nStLineNum = StationInf(i).nStLineNum + 1
                            ReDim Preserve StationInf(i).sStLineNo(UBound(StationInf(i).sStLineNo) + 1)
                            ReDim Preserve StationInf(i).nStLineUse(UBound(StationInf(i).sStLineNo) + 1)
                            ReDim Preserve StationInf(i).sLineUse(UBound(StationInf(i).sStLineNo) + 1)
                            ReDim Preserve StationInf(i).sUpOrDownUse(UBound(StationInf(i).sStLineNo) + 1)
                            ReDim Preserve StationInf(i).sGuDaoUseSeq(UBound(StationInf(i).sStLineNo) + 1)
                            ReDim Preserve StationInf(i).sGuDaoName(UBound(StationInf(i).sStLineNo) + 1)
                            StationInf(i).sStLineNo(UBound(StationInf(i).sStLineNo)) = TMS_LINEDRAW.Rows(j - 1).Item("DAOCHA_ID").ToString.Trim()
                            StationInf(i).sLineUse(UBound(StationInf(i).sStLineNo)) = TMS_LINEDRAW.Rows(j - 1).Item("TRACKTYPE").ToString.Trim
                            StationInf(i).sUpOrDownUse(UBound(StationInf(i).sStLineNo)) = TMS_LINEDRAW.Rows(j - 1).Item("TRACKUSAGE").ToString.Trim
                            StationInf(i).sGuDaoUseSeq(UBound(StationInf(i).sStLineNo)) = TMS_LINEDRAW.Rows(j - 1).Item("TRACKUSINGNUM").ToString.Trim
                            StationInf(i).nStLineUse(UBound(StationInf(i).sStLineNo)) = ChaLineUse(TMS_LINEDRAW.Rows(j - 1).Item("TRACKTYPE").ToString.Trim, TMSlocalDataSet.TMS_LINEDRAW.Rows(j - 1).Item("TRACKUSAGE").ToString.Trim)
                            StationInf(i).sGuDaoName(UBound(StationInf(i).sStLineNo)) = TMS_LINEDRAW.Rows(j - 1).Item("CONTROLMOD").ToString.Trim
                        End If
                    End If
                Next
            End If

            '导入车站进路和分岔站股道使用

            '导入车站进路和分岔站股道使用
            ReDim StationInf(i).sTrackUse(0)

            ''以下代码读入车站进路信息
            'ReDim StationInf(i).PathLinkTrack(0)
            'ReDim StationInf(i).PathControlNum(0)
            'ReDim StationInf(i).PathCrossNum(0)
            'ReDim StationInf(i).PathLinkSta(0)
            'ReDim StationInf(i).PathTrackNum(0)
            'ReDim StationInf(i).PathPathTrackID(0)
            'Dim strTable5 As String = "select * from 车站进路信息 where 车站名称='" & StationInf(i).sStationName & "'"
            'ReDim StationInf(i).PathLinkTrack(nNum)
            'ReDim StationInf(i).PathControlNum(nNum)
            'ReDim StationInf(i).PathCrossNum(nNum)
            'ReDim StationInf(i).PathLinkSta(nNum)
            'ReDim StationInf(i).PathTrackNum(nNum)
            'ReDim StationInf(i).PathPathTrackID(nNum)

            'If nNum > 0 Then
            '    sFile.MoveFirst()
            '    For j = 1 To nNum
            '        StationInf(i).PathLinkTrack(j) = sFile.Fields("进站方式").Value.ToString.Trim ' myTable5.Rows(j - 1).Item("到达方向").ToString.Trim
            '        StationInf(i).PathLinkSta(j) = sFile.Fields("连接车站名").Value.ToString.Trim ' myTable5.Rows(j - 1).Item("出发方向").ToString.Trim
            '        StationInf(i).PathTrackNum(j) = sFile.Fields("股道编号").Value.ToString.Trim ' myTable5.Rows(j - 1).Item("股道号").ToString.Trim
            '        StationInf(i).PathLinkTrack(j) = sFile.Fields("通过的轨道编号").Value.ToString.Trim ' myTable5.Rows(j - 1).Item("基本进路").ToString.Trim
            '        StationInf(i).PathCrossNum(j) = sFile.Fields("通过的道岔编号").Value.ToString.Trim '  myTable5.Rows(j - 1).Item("可选进路").ToString.Trim
            '        StationInf(i).PathControlNum(j) = sFile.Fields("通过的控制模块").Value.ToString.Trim ' myTable5.Rows(j - 1).Item("备注").ToString.Trim
            '        sFile.MoveNext()
            '    Next
            'End If


            '    '以下代码是车站间隔时间
            '    Dim strTable6 As String = "select * from 车站间隔时间 where 车站名称='" & StationInf(i).sStationName & "'"
            '    sFile = dFile.OpenRecordset(strTable6)
            '    nNum = 0
            '    If sFile.RecordCount > 0 Then
            '        sFile.MoveLast()
            '        nNum = sFile.RecordCount
            '    End If

            With StationInf(i)
                ReDim Preserve .lTaoBu(2)
                ReDim Preserve .lTaoHui(2)
                ReDim Preserve .lTaoLian1(2)
                ReDim Preserve .lTaoLian2(2)
                ReDim Preserve .lTaoTongK(2)
                ReDim Preserve .lTaoTongH(2)
                ReDim Preserve .lTaoDaoFa(2)
                ReDim Preserve .lTaoFaDao(2)
                ReDim Preserve .lTaoFaFa(2)
                ReDim Preserve .lTaoDaoDao(2)
                '        If nNum > 0 Then
                '            sFile.MoveFirst()
                '            .lTaoBu(1) = MinuteToSecond(sFile.Fields("τ不上").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ不上").ToString.Trim)
                '            .lTaoBu(2) = MinuteToSecond(sFile.Fields("τ不下").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ不下").ToString.Trim)
                '            .lTaoHui(1) = MinuteToSecond(sFile.Fields("τ会上").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ会上").ToString.Trim)
                '            .lTaoHui(2) = MinuteToSecond(sFile.Fields("τ会下").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ会下").ToString.Trim)
                '            .lTaoLian1(1) = MinuteToSecond(sFile.Fields("τ连1上").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ连1上").ToString.Trim)
                '            .lTaoLian1(2) = MinuteToSecond(sFile.Fields("τ连1下").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ连1下").ToString.Trim)
                '            .lTaoLian2(1) = MinuteToSecond(sFile.Fields("τ连2上").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ连2上").ToString.Trim)
                '            .lTaoLian2(2) = MinuteToSecond(sFile.Fields("τ连2下").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ连2下").ToString.Trim)
                '            .lTaoTongK(1) = MinuteToSecond(sFile.Fields("τ通1上").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ通1上").ToString.Trim)
                '            .lTaoTongK(2) = MinuteToSecond(sFile.Fields("τ通1下").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ通1下").ToString.Trim)
                '            .lTaoTongH(1) = MinuteToSecond(sFile.Fields("τ通2上").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ通2上").ToString.Trim)
                '            .lTaoTongH(2) = MinuteToSecond(sFile.Fields("τ通2下").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ通2下").ToString.Trim)
                '            .lTaoDaoFa(1) = MinuteToSecond(sFile.Fields("τ到发上").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ到发上").ToString.Trim)
                '            .lTaoDaoFa(2) = MinuteToSecond(sFile.Fields("τ到发下").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ到发下").ToString.Trim)
                '            .lTaoFaDao(1) = MinuteToSecond(sFile.Fields("τ发到上").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ发到上").ToString.Trim)
                '            .lTaoFaDao(2) = MinuteToSecond(sFile.Fields("τ发到下").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ发到下").ToString.Trim)
                '            .lTaoFaFa(1) = MinuteToSecond(sFile.Fields("τ发发上").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ发发上").ToString.Trim)
                '            .lTaoFaFa(2) = MinuteToSecond(sFile.Fields("τ发发下").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ发发下").ToString.Trim)
                '            .lTaoDaoDao(1) = MinuteToSecond(sFile.Fields("τ到到上").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ到到上").ToString.Trim)
                '            .lTaoDaoDao(2) = MinuteToSecond(sFile.Fields("τ到到下").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ到到下").ToString.Trim)
                '        End If
            End With


            '    '以下代码是追踪间隔时间
            '    Dim strTable7 As String = "select * from 追踪间隔时间 where 车站名称='" & StationInf(i).sStationName & "'"
            '    sFile = dFile.OpenRecordset(strTable7)
            '    nNum = 0
            '    If sFile.RecordCount > 0 Then
            '        sFile.MoveLast()
            '        nNum = sFile.RecordCount
            '    End If


            With StationInf(i)
                ReDim Preserve .IKK(17)
                ReDim Preserve .IKH(17)
                ReDim Preserve .IHH(17)
                ReDim Preserve .IHK(17)
                '        If nNum > 0 Then
                '            sFile.MoveFirst()
                '            .IKK(0) = MinuteToSecond(sFile.Fields("同向通发").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("同向通发").ToString.Trim)
                '            .IKK(1) = MinuteToSecond(sFile.Fields("同向发发").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("同向发发").ToString.Trim)
                '            .IKK(2) = MinuteToSecond(sFile.Fields("同向到发").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("同向到发").ToString.Trim)
                '            .IKK(3) = MinuteToSecond(sFile.Fields("同向发通").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("同向发通").ToString.Trim)
                '            .IKK(4) = MinuteToSecond(sFile.Fields("同向发到").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("同向发到").ToString.Trim)
                '            .IKK(5) = MinuteToSecond(sFile.Fields("同向通到").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("同向通到").ToString.Trim)
                '            .IKK(6) = MinuteToSecond(sFile.Fields("同向到到").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("同向到到").ToString.Trim)
                '            .IKK(7) = MinuteToSecond(sFile.Fields("同向到通").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("同向到通").ToString.Trim)
                '            .IKK(8) = MinuteToSecond(sFile.Fields("同向通通").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("同向通通").ToString.Trim)
                '            .IKK(9) = MinuteToSecond(sFile.Fields("对向到到").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("对向到到").ToString.Trim)
                '            .IKK(10) = MinuteToSecond(sFile.Fields("对向发到").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("对向发到").ToString.Trim)
                '            .IKK(11) = MinuteToSecond(sFile.Fields("对向到发").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("对向到发").ToString.Trim)
                '            .IKK(12) = MinuteToSecond(sFile.Fields("对向发发").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("对向发发").ToString.Trim)
                '            .IKK(13) = MinuteToSecond(sFile.Fields("对向到通").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("对向到通").ToString.Trim)
                '            .IKK(14) = MinuteToSecond(sFile.Fields("对向发通").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("对向发通").ToString.Trim)
                '            .IKK(15) = MinuteToSecond(sFile.Fields("对向通到").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("对向通到").ToString.Trim)
                '            .IKK(16) = MinuteToSecond(sFile.Fields("对向通发").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("对向通发").ToString.Trim)
                '            .IKK(17) = MinuteToSecond(sFile.Fields("对向通通").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("对向通通").ToString.Trim)
                '        End If
            End With
            ' 以下代码读入车站进路信息()
            ReDim StationInf(i).sCrossUse(0)
        Next i

        'dFile.Close()
    End Sub

    '读入底图结构与线网信息
    Public Sub readNetStaAndSecData()
        Dim TMS_DIASTRUCTINFO, TMS_SECTIONINFO, TMS_RUNSCALE As New DataTable
        Dim sqlstr As String = "select * from TMS_DIASTRUCTINFO where IfFixed=1 and TRAINDIAGRAMID='" & ODSPubpara.DiagramSelect.Trim & "' order by StationSeq"
        TMS_DIASTRUCTINFO = Globle.Method.ReadDataForAccess(sqlstr)
        ReDim StationInf(TMS_DIASTRUCTINFO.Rows.Count)
        Dim i, j As Integer
        If TMS_DIASTRUCTINFO.Rows.Count > 0 Then
            For i = 1 To TMS_DIASTRUCTINFO.Rows.Count
                StationInf(i).sStationName = Trim(TMS_DIASTRUCTINFO.Rows(i - 1).Item("StationName"))
                StationInf(i).sAtLineName = Trim(TMS_DIASTRUCTINFO.Rows(i - 1).Item("Linename"))
                StationInf(i).Ycord = Trim(TMS_DIASTRUCTINFO.Rows(i - 1).Item("YCoord"))
            Next i
        Else
            MsgBox("底图结构没有设置或者没有设置默认的底图结构!", , "提示")
            TimeTablePara.sInputDataError = "底图结构设置错误码"
            Exit Sub
        End If

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

        sqlstr = "select * from TMS_SECTIONINFO where TraindiagramID='" & ODSPubpara.DiagramSelect & "'"
        TMS_SECTIONINFO = Globle.Method.ReadDataForAccess(sqlstr)
       
        Dim sFirSta As String
        Dim sSecSta As String
        Dim sSecName As String
        ReDim SectionInf(TMS_SECTIONINFO.Rows.Count)
        For i = 1 To UBound(StationInf) - 1
            sFirSta = StationInf(i).sStationName
            sSecSta = StationInf(i + 1).sStationName
            sSecName = sFirSta & "->" & sSecSta
            If TMS_SECTIONINFO.Rows.Count > 0 Then
                For j = 1 To TMS_SECTIONINFO.Rows.Count
                    If TMS_SECTIONINFO.Rows(j - 1).Item("SectionName").ToString.Trim = sSecName Then
                        ReDim SectionInf(j).lDistance(2)
                        SectionInf(j).nSecNumber = TMS_SECTIONINFO.Rows(j - 1).Item("SECTIONSEQ")
                        SectionInf(j).sSecName = sSecName
                        SectionInf(j).sLineName = TMS_SECTIONINFO.Rows(j - 1).Item("LineName")
                        SectionInf(j).nSection = Val(TMS_SECTIONINFO.Rows(j - 1).Item("LineNumber").ToString)
                        SectionInf(j).sBlock = TMS_SECTIONINFO.Rows(j - 1).Item("Blocktype").ToString
                        SectionInf(j).sSecFirName = sFirSta
                        SectionInf(j).sSecSecName = sSecSta
                        SectionInf(j).nFirStaID = i
                        SectionInf(j).nSecStaID = i + 1
                        SectionInf(j).nHStation = i
                        SectionInf(j).nQStation = i + 1
                        SectionInf(j).lDistance(1) = TMS_SECTIONINFO.Rows(j - 1).Item("DownSectionDistance")
                        SectionInf(j).lDistance(2) = TMS_SECTIONINFO.Rows(j - 1).Item("UPSectionDistance")
                        Exit For
                    End If
                Next
            End If
        Next
        '区间标尺
        sqlstr = "select * from TMS_RUNSCALE where TraindiagramID='" & ODSPubpara.DiagramSelect & "' order by Sectionname,SecScaleNum"
        TMS_RUNSCALE = Globle.Method.ReadDataForAccess(sqlstr)
        Dim strSecName As String
        For i = 1 To UBound(SectionInf)
            strSecName = SectionInf(i).sSecName
            ReDim SectionInf(i).SecScale(0)
            If TMS_RUNSCALE.Rows.Count > 0 Then
                For j = 1 To TMS_RUNSCALE.Rows.Count
                    If TMS_RUNSCALE.Rows(j - 1).Item("sectionname").ToString = strSecName Then
                        ReDim Preserve SectionInf(i).SecScale(UBound(SectionInf(i).SecScale) + 1)
                        SectionInf(i).SecScale(UBound(SectionInf(i).SecScale)).nID = TMS_RUNSCALE.Rows(j - 1).Item("SecScaleNum")
                        SectionInf(i).SecScale(UBound(SectionInf(i).SecScale)).sScaleName = TMS_RUNSCALE.Rows(j - 1).Item("SecScaleName").ToString.Trim
                        SectionInf(i).SecScale(UBound(SectionInf(i).SecScale)).sngDownTime = MinuteToSecond(TMS_RUNSCALE.Rows(j - 1).Item("DownRunTime").ToString.Trim)
                        SectionInf(i).SecScale(UBound(SectionInf(i).SecScale)).sngUpTime = MinuteToSecond(TMS_RUNSCALE.Rows(j - 1).Item("UpRunTime").ToString.Trim)
                        SectionInf(i).SecScale(UBound(SectionInf(i).SecScale)).sngDownAppendStartTime = MinuteToSecond(TMS_RUNSCALE.Rows(j - 1).Item("DownStartAddTime").ToString.Trim)
                        SectionInf(i).SecScale(UBound(SectionInf(i).SecScale)).sngDownAppendStopTime = MinuteToSecond(TMS_RUNSCALE.Rows(j - 1).Item("DownStopAddTime").ToString.Trim)
                        SectionInf(i).SecScale(UBound(SectionInf(i).SecScale)).sngUpAppendStartTime = MinuteToSecond(TMS_RUNSCALE.Rows(j - 1).Item("UpStartAddTime").ToString.Trim)
                        SectionInf(i).SecScale(UBound(SectionInf(i).SecScale)).sngUpAppendStopTime = MinuteToSecond(TMS_RUNSCALE.Rows(j - 1).Item("UpStopAddTime").ToString.Trim)
                    End If
                Next
            End If

        Next

       
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
        ReDim ChediMultiJLInfo(0)
        ReDim TrainErrInf(0)

        TimeTablePara.nMaxUndoID = 20
        ReDim UndoInf(TimeTablePara.nMaxUndoID)
        For i = 1 To UBound(UndoInf)
            UndoInf(i).nXuHao = i
            ReDim UndoInf(i).Traininf(0)
            ReDim UndoInf(i).CheDiInf(0)
        Next
        UndoSeq.nCurUndoID = 1
        TimeTablePara.nPubTrain = 0
        TimeTablePara.nPubCheDi = 0
        ReDim TimeTablePara.nPubTrains(0)
        nJGFuYu = 30
        TimeTablePara.sCurDiagramState = DiagramState.运行图
        TimeTablePara.sPubTrainStrainDraw = TrainStrainDraw.无约束
        TimeTablePara.sDrawLineStyle = "不能越行"
        TimeTablePara.sPubCurSkbName = ODSPubpara.sDiagramName
        TimeTablePara.nStaJiShuTuJieSeletedState = 0
        TimeTablePara.BifAutoBianCheCi = False
        TimeTablePara.sErrorShowStyle = "显示全部"
        'nMoveStepTime = 30 '运行线调整时移动变量,不够时移动30s
        'TdrawLinePara.sMaxMoveTime = 3600
    End Sub

    '自定义底图变量
    Public Sub IniteDiagramPicViraient(ByVal sDiaStyle As String)
        '从数据库中读入
        Dim TMS_TIMETABLEPARAMETER As New DataTable
        Dim sqlstr As String = "select * from TMS_TIMETABLEPARAMETER where TrainDiagramID='" & ODSPubpara.DiagramSelect & "'"
        TMS_TIMETABLEPARAMETER = Globle.Method.ReadDataForAccess(sqlstr)
        Dim i As Integer
        For i = 1 To TMS_TIMETABLEPARAMETER.Rows.Count
            Select Case Trim(TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaName").ToString)
                Case "底图起始时间"
                    TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime = 4 ' TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "底图显示时间宽"
                    TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime = 4 'TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "时间比较起始时间"
                    TimeTablePara.TimeTableDiagramPara.intCompareFirstTime = HourToSecond(Val(TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim))
                Case "底图宽"
                    TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = 4000 'TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "底图高"
                    If sDiaStyle = "换乘站运行图" Then
                        TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = 400 ' TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                    Else
                        TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = 800 ' TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                    End If
                Case "底图时分格式"
                    TimeTablePara.TimeTableDiagramPara.strTimeFormat = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "底图上下空白高度"
                    TimeTablePara.TimeTableDiagramPara.sngtopBlank = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "底图时间空白高度"
                    TimeTablePara.TimeTableDiagramPara.sngTimeBlank = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "底图左右空白高度"
                    TimeTablePara.TimeTableDiagramPara.sngLeftBlank = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "底图车站空白宽度"
                    TimeTablePara.TimeTableDiagramPara.sngStaBlank = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "左边缩进宽度"
                    TimeTablePara.TimeTableDiagramPara.sngPubLeftX = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "上面缩进高度"
                    TimeTablePara.TimeTableDiagramPara.sngPubTopY = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图1分格线型"
                    TimeTablePara.DiagramStylePara.OneTime1LineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图1分格线宽"
                    TimeTablePara.DiagramStylePara.OneTime1LineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图1分格线颜色"
                    TimeTablePara.DiagramStylePara.OneTime1LineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图5分格线型"
                    TimeTablePara.DiagramStylePara.OneTime5LineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图5分格线宽"
                    TimeTablePara.DiagramStylePara.OneTime5LineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图5分格线颜色"
                    TimeTablePara.DiagramStylePara.OneTime5LineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图10分格线型"
                    TimeTablePara.DiagramStylePara.OneTime10LineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图10分格线宽"
                    TimeTablePara.DiagramStylePara.OneTime10LineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图10分格线颜色"
                    TimeTablePara.DiagramStylePara.OneTime10LineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图30分格线型"
                    TimeTablePara.DiagramStylePara.OneTime30LineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图30分格线宽"
                    TimeTablePara.DiagramStylePara.OneTime30LineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图30分格线颜色"
                    TimeTablePara.DiagramStylePara.OneTime30LineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图60分格线型"
                    TimeTablePara.DiagramStylePara.OneTime60LineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图60分格线宽"
                    TimeTablePara.DiagramStylePara.OneTime60LineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图60分格线颜色"
                    TimeTablePara.DiagramStylePara.OneTime60LineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图2分格线型"
                    TimeTablePara.DiagramStylePara.TwoTime2LineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图2分格线宽"
                    TimeTablePara.DiagramStylePara.TwoTime2LineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图2分格线颜色"
                    TimeTablePara.DiagramStylePara.TwoTime2LineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图10分格线型"
                    TimeTablePara.DiagramStylePara.TwoTime10LineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图10分格线宽"
                    TimeTablePara.DiagramStylePara.TwoTime10LineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图10分格线颜色"
                    TimeTablePara.DiagramStylePara.TwoTime10LineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图30分格线型"
                    TimeTablePara.DiagramStylePara.TwoTime30LineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图30分格线宽"
                    TimeTablePara.DiagramStylePara.TwoTime30LineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图30分格线颜色"
                    TimeTablePara.DiagramStylePara.TwoTime30LineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图60分格线型"
                    TimeTablePara.DiagramStylePara.TwoTime60LineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图60分格线宽"
                    TimeTablePara.DiagramStylePara.TwoTime60LineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图60分格线颜色"
                    TimeTablePara.DiagramStylePara.TwoTime60LineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图10分格线型"
                    TimeTablePara.DiagramStylePara.TenTime10LineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图10分格线宽"
                    TimeTablePara.DiagramStylePara.TenTime10LineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图10分格线颜色"
                    TimeTablePara.DiagramStylePara.TenTime10LineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图30分格线型"
                    TimeTablePara.DiagramStylePara.TenTime30LineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图30分格线宽"
                    TimeTablePara.DiagramStylePara.TenTime30LineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图30分格线颜色"
                    TimeTablePara.DiagramStylePara.TenTime30LineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图60分格线型"
                    TimeTablePara.DiagramStylePara.TenTime60LineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图60分格线宽"
                    TimeTablePara.DiagramStylePara.TenTime60LineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图60分格线颜色"
                    TimeTablePara.DiagramStylePara.TenTime60LineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "小时格图60分格线型"
                    TimeTablePara.DiagramStylePara.HourTime60LineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "小时格图60分格线宽"
                    TimeTablePara.DiagramStylePara.HourTime60LineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "小时格图60分格线颜色"
                    TimeTablePara.DiagramStylePara.HourTime60LineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车站名称字体名称"
                    TimeTablePara.DiagramStylePara.StaNameFontName = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车站名称字体大小"
                    TimeTablePara.DiagramStylePara.StaNameFontSize = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车站名称字体粗体"
                    TimeTablePara.DiagramStylePara.StaNameFontBold = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车站名称字体斜体"
                    TimeTablePara.DiagramStylePara.StaNameFontItalic = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车站名称字体颜色"
                    TimeTablePara.DiagramStylePara.StaNameFontColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "时间标注字体名称"
                    TimeTablePara.DiagramStylePara.TimeFontName = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "时间标注字体大小"
                    TimeTablePara.DiagramStylePara.TimeFontSize = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "时间标注字体粗体"
                    TimeTablePara.DiagramStylePara.TimeFontBold = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "时间标注字体斜体"
                    TimeTablePara.DiagramStylePara.TimeFontItalic = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "时间标注字体颜色"
                    TimeTablePara.DiagramStylePara.TimeFontColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一般车站中心线类型"
                    TimeTablePara.DiagramStylePara.StaLineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一般车站中心线宽度"
                    TimeTablePara.DiagramStylePara.StaLineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一般车站中心线颜色"
                    TimeTablePara.DiagramStylePara.StaLineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "分岔站中心线类型"
                    TimeTablePara.DiagramStylePara.FenStaLineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "分岔站中心线宽度"
                    TimeTablePara.DiagramStylePara.FenStaLineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "分岔站中心线颜色"
                    TimeTablePara.DiagramStylePara.FenStaLineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车场中心线类型"
                    TimeTablePara.DiagramStylePara.CheChangStaLineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车场中心线宽度"
                    TimeTablePara.DiagramStylePara.CheChangStaLineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车场中心线颜色"
                    TimeTablePara.DiagramStylePara.CheChangStaLineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "所有运行线线型"
                    TimeTablePara.DiagramStylePara.TrainLineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "所有运行线线宽"
                    TimeTablePara.DiagramStylePara.TrainLineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "所有运行线颜色"
                    TimeTablePara.DiagramStylePara.TrainLineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "所有车底连接线线型"
                    TimeTablePara.DiagramStylePara.CheDiLineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "所有车底连接线线宽"
                    TimeTablePara.DiagramStylePara.CheDiLineWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "所有车底连接线颜色"
                    TimeTablePara.DiagramStylePara.CheDiLineColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车次标号字体名称"
                    TimeTablePara.DiagramStylePara.CheCiFontName = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车次标号字体大小"
                    TimeTablePara.DiagramStylePara.CheCiFontSize = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车次标号字体粗体"
                    TimeTablePara.DiagramStylePara.CheCiFontBold = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车次标号字体斜体"
                    TimeTablePara.DiagramStylePara.CheCiFontItalic = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车次标号字体颜色"
                    TimeTablePara.DiagramStylePara.CheCiFontColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "斜向车次字体名称"
                    TimeTablePara.DiagramStylePara.XieCheCiFontName = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "斜向车次字体大小"
                    TimeTablePara.DiagramStylePara.XieCheCiFontSize = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "斜向车次字体粗体"
                    TimeTablePara.DiagramStylePara.XieCheCiFontColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "斜向车次字体斜体"
                    TimeTablePara.DiagramStylePara.XieCheCiFontItalic = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "斜向车次字体颜色"
                    TimeTablePara.DiagramStylePara.XieCheCiFontColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "斜向车次字体颜色"
                    TimeTablePara.DiagramStylePara.XieCheCiFontColor = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "是否显示车次"
                    TimeTablePara.TimeTableDiagramPara.nifPrintCheCi = True  'TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "是否显示斜向车次"
                    TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车底交路线高度"
                    TimeTablePara.TimeTableDiagramPara.nCheDiLineHeight = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车底交路线类型"
                    TimeTablePara.TimeTableDiagramPara.sCheDiLineStyle = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车站股道线间距"
                    TimeTablePara.StaDiagramePara.nStaLineHeight = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车站站名图高"
                    TimeTablePara.TimeTableDiagramPara.sngPicStationHeight = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车站站名图宽"
                    TimeTablePara.TimeTableDiagramPara.sngPicStationWidth = TMS_TIMETABLEPARAMETER.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "运行线可调整时间段"
                    ' TdrawLinePara.sMaxMoveTime = 2 'myTable3.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "运行线移动时间"
                    '  TdrawLinePara.sMoveStepTime = 60 'myTable3.Rows(i - 1).Item("ParaValue").ToString.Trim
            End Select
        Next
        TimeTablePara.TimeTableDiagramPara.sCheCiShowStyle = 1
    End Sub

    '读入列车与时刻表信息
    Public Sub ReadTrainAndTimeTableInf(ByVal sSKBID As String)
        Call readCheDiLinkInf(sSKBID)
        Call readtimetableinf(sSKBID)
    End Sub

    '读入车底连接信息
    Public Sub readCheDiLinkInf(ByVal sSKBID As String)
        Dim i, j, p As Integer
        Dim nTrain As Integer
        Dim nTempTrain As Integer
        For i = 1 To UBound(ChediInfo)
            ReDim ChediInfo(i).nLinkTrain(0)
        Next
        ReDim TrainInf(0)
        Dim TMS_STOCKUSINGINFO As New DataTable
        Dim sqlstr As String = "select * from TMS_STOCKUSINGINFO where TrainDiagramID='" & sSKBID & "' order by StockSeq,LineSeq"
        TMS_STOCKUSINGINFO = Globle.Method.ReadDataForAccess(sqlstr)
       
        Dim stmpSta As Integer
        Dim tmpBeforSta As Integer
        Dim tmpID As Integer
        Dim tmpXH As Integer

        If TMS_STOCKUSINGINFO.Rows.Count > 0 Then
            tmpBeforSta = Int(TMS_STOCKUSINGINFO.Rows(0).Item("StockSeq"))
            'tmpID = GetCurCheDiInfoIDIDFromSchediID(rsDb.Fields("车底ID").Value)
            tmpID = 1
            ReDim ChediInfo(tmpID)
            ReDim ChediInfo(tmpID).nLinkTrain(0)
            ''ChediInfo(tmpID).SCheDiLeiXing = "西门子"
            ''ChediInfo(tmpID).sCheDiID = rsDb.Fields("车底顺序").Value
            Call CopyCheDiInformation(tmpID, TMS_STOCKUSINGINFO.Rows(0).Item("StockID"))
            If tmpID > 0 Then
            Else
                MsgBox("车底信息与当前运行图信息不对应，请检查运行图和车底信息!", , "提示")
            End If
            tmpXH = 1

            For i = 1 To TMS_STOCKUSINGINFO.Rows.Count
                stmpSta = Int(TMS_STOCKUSINGINFO.Rows(i - 1).Item("StockSeq"))
                If stmpSta <> tmpBeforSta Then
                    tmpBeforSta = stmpSta
                    tmpID = tmpID + 1
                    ReDim Preserve ChediInfo(tmpID)
                    ReDim ChediInfo(tmpID).nLinkTrain(0)
                    ''ChediInfo(tmpID).SCheDiLeiXing = "西门子"
                    ''ChediInfo(tmpID).sCheDiID = rsDb.Fields("车底顺序").Value
                    Call CopyCheDiInformation(tmpID, TMS_STOCKUSINGINFO.Rows(i - 1).Item("Stockid"))
                    ' tmpID = GetCurCheDiInfoIDIDFromSchediID(rsDb.Fields("车底ID").Value)
                    tmpXH = 1
                End If
                If tmpID > 0 Then
                    tmpXH = 1
                    'If rsDb.Fields("连挂车次").Value = "16008" Then Stop
                    nTrain = AddTrainInformation(Trim(TMS_STOCKUSINGINFO.Rows(i - 1).Item("RoutingStyle")), Trim(TMS_STOCKUSINGINFO.Rows(i - 1).Item("RunScaleStyle")), TMS_STOCKUSINGINFO.Rows(i - 1).Item("StopScaleStyle"), TMS_STOCKUSINGINFO.Rows(i - 1).Item("LinkTrainNum"))
                    TrainInf(nTrain).Train = Trim(TMS_STOCKUSINGINFO.Rows(i - 1).Item("LinkTrainNum"))
                    TrainInf(nTrain).sPrintTrain = TMS_STOCKUSINGINFO.Rows(i - 1).Item("PrintNum").ToString.Trim
                    TrainInf(nTrain).nZfLimit = Trim(TMS_STOCKUSINGINFO.Rows(i - 1).Item("IfTurnFixed"))
                    TrainInf(nTrain).PrintLineStyle = GetLineStyleFromText(TMS_STOCKUSINGINFO.Rows(i - 1).Item("LineShowStyle").ToString.Trim)
                    TrainInf(nTrain).PrintLineWidth = TMS_STOCKUSINGINFO.Rows(i - 1).Item("LineShowWidth")
                    TrainInf(nTrain).PrintLineColor = System.Drawing.ColorTranslator.FromHtml(TMS_STOCKUSINGINFO.Rows(i - 1).Item("LineShowColor"))
                    ' If nTrain = 0 Then Stop
                    If nTrain > 0 And TrainInf(nTrain).Train <> "" Then
                        ReDim Preserve ChediInfo(tmpID).nLinkTrain(UBound(ChediInfo(tmpID).nLinkTrain) + 1)
                        ChediInfo(tmpID).nLinkTrain(UBound(ChediInfo(tmpID).nLinkTrain)) = nTrain
                    End If
                    ChediInfo(tmpID).sCheCiHao = TMS_STOCKUSINGINFO.Rows(i - 1).Item("StockID") 'Left(rsDb.Fields("输出车次").Value, 3)
                    ChediInfo(tmpID).PrintCheDiLinkStyle = GetLineStyleFromText(TMS_STOCKUSINGINFO.Rows(i - 1).Item("StockShowStyle").ToString.Trim)
                    ChediInfo(tmpID).PrintCheDiLinkWidth = TMS_STOCKUSINGINFO.Rows(i - 1).Item("StockShowWidth")
                    ChediInfo(tmpID).PrintCheDiLinkColor = System.Drawing.ColorTranslator.FromHtml(TMS_STOCKUSINGINFO.Rows(i - 1).Item("StockShowColor").ToString.Trim)
                    If TMS_STOCKUSINGINFO.Rows(i - 1).Item("IfTurnFixed") = 1 Then
                        ChediInfo(tmpID).bIfGouWang = True
                    Else
                        ChediInfo(tmpID).bIfGouWang = False
                    End If
                Else
                    MsgBox("车底信息与当前运行图信息不对应，请检查运行图和车底信息!", , "提示")
                End If
                tmpXH = tmpXH + 1
                'proBar.Value = 10 + Int(i * 40 / TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows.Count)
            Next i
        End If

        For j = 1 To UBound(ChediInfo)
            nTempTrain = 0
            For p = 1 To UBound(ChediInfo(j).nLinkTrain)
                nTrain = ChediInfo(j).nLinkTrain(p)
                TrainInf(nTrain).SCheDiLeiXing = ChediInfo(j).SCheDiLeiXing
                If nTrain <> 0 And TrainInf(nTrain).Train <> "" Then
                    If nTempTrain = 0 Then
                        TrainInf(nTrain).TrainReturn(1) = 0
                        TrainInf(nTrain).TrainReturn(2) = 0
                    Else
                        TrainInf(nTempTrain).TrainReturn(2) = nTrain
                        TrainInf(nTrain).TrainReturn(1) = nTempTrain
                        TrainInf(nTrain).TrainReturn(2) = 0
                    End If
                    TrainInf(nTrain).nCheDiPuOrNot = 1
                    nTempTrain = nTrain
                End If
            Next p
        Next j
    End Sub

    '将时刻表读入
    Public Sub ReadTimetableInf(ByVal sSKBID As String)
        Dim i, j, k, p As Integer
        Dim q As Integer
        Dim sTrainNum() As String
        ReDim sTrainNum(0)
        Dim nErrorTrain As Integer
        nErrorTrain = 0
        Dim nStaID As Integer
        Dim nifIn As Integer
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                j = i
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
                Dim TMS_TIMETABLEINFO As New DataTable
                Dim sqlstr As String = "select * from TMS_TIMETABLEINFO where TrainDiagramID='" & sSKBID & "'and TrainNum='" & TrainInf(i).Train & "' order by Seq"
                TMS_TIMETABLEINFO = Globle.Method.ReadDataForAccess(sqlstr)
              
                If TMS_TIMETABLEINFO.Rows.Count > 0 Then
                    For p = 1 To TMS_TIMETABLEINFO.Rows.Count
                        nifIn = 0
                        If p = TMS_TIMETABLEINFO.Rows.Count - 1 Then
                            TrainInf(j).sStartZFStarting = TMS_TIMETABLEINFO.Rows(p - 1).Item("DepartTime") ' HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                            TrainInf(j).sStartZFArrival = TMS_TIMETABLEINFO.Rows(p - 1).Item("ArriTime") ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                            TrainInf(j).sStartZFLine = TMS_TIMETABLEINFO.Rows(p - 1).Item("StopTrack")
                        ElseIf p = TMS_TIMETABLEINFO.Rows.Count Then
                            TrainInf(j).sEndZFStarting = TMS_TIMETABLEINFO.Rows(p - 1).Item("DepartTime") 'HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                            TrainInf(j).sEndZFArrival = TMS_TIMETABLEINFO.Rows(p - 1).Item("ArriTime")  ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                            TrainInf(j).sEndZFLine = TMS_TIMETABLEINFO.Rows(p - 1).Item("StopTrack")
                        Else
                            For q = 1 To UBound(StationInf)
                                If StationInf(q).sStationName = TMS_TIMETABLEINFO.Rows(p - 1).Item("stationname").ToString.Trim Then
                                    nStaID = q
                                    TrainInf(j).Starting(nStaID) = TMS_TIMETABLEINFO.Rows(p - 1).Item("DepartTime")
                                    TrainInf(j).Arrival(nStaID) = TMS_TIMETABLEINFO.Rows(p - 1).Item("ArriTime")
                                    TrainInf(j).StopLine(nStaID) = TMS_TIMETABLEINFO.Rows(p - 1).Item("StopTrack")
                                    nifIn = 1
                                End If
                            Next
                            If nifIn = 0 Then
                                nErrorTrain = nErrorTrain + 1
                            End If
                        End If
                    Next p
                    TrainInf(j).lAllStartTime = TrainInf(j).Starting(TrainInf(j).nPathID(1))
                    TrainInf(j).lAllEndTime = TrainInf(j).Arrival(TrainInf(j).nPathID(UBound(TrainInf(j).nPathID)))
                End If
                'For p = 1 To UBound(TrainInf(i).nPathID)
                '    If TrainInf(i).Arrival(TrainInf(i).nPathID(p)) = -1 Then
                '        nErrorTrain = nErrorTrain + 1  
                '        Exit For
                '    End If
                'Next
            End If
            'proBar.Value = 50 + Int(i * 50 / UBound(TrainInf))
        Next i
        If nErrorTrain > 0 Then
            MsgBox("列车信息中有" & nErrorTrain & "趟列车没有时刻，请检查当前的底图结构是否选择错误！", , "提示")
        End If
    End Sub

    '从数据库中导入线段信息DAO代码
    Public Sub InputDataToCADstaInfTrackByDAO(ByVal nStaID As Integer)
        Dim i, k As Integer
        TMSlocalDataSet.TMS_LINEDRAW.Rows.Clear()
        TMSlocalDataSet.Fill("TMS_LINEDRAW", "TRAINDIAGRAMID='" & ODSPubpara.DiagramSelect & "'and name='" & CADStaInf(nStaID).sStaName & "' order by tracknum")
        For k = 1 To UBound(CADStaInf)
            If CADStaInf(k).sStaName = CADStaInf(nStaID).sStaName Then
                ReDim CADStaInf(k).Track(TMSlocalDataSet.TMS_LINEDRAW.Rows.Count)
                If TMSlocalDataSet.TMS_LINEDRAW.Rows.Count > 0 Then
                    For i = 1 To TMSlocalDataSet.TMS_LINEDRAW.Rows.Count
                        CADStaInf(k).Track(i).sStaName = TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(0).ToString.Trim
                        CADStaInf(k).Track(i).nNum = Val(TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(1))
                        CADStaInf(k).Track(i).sStyle = TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(3).ToString.Trim
                        CADStaInf(k).Track(i).sGuDaoStyle = TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(4).ToString.Trim
                        CADStaInf(k).Track(i).sGuDaoYongTu = TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(5).ToString.Trim
                        CADStaInf(k).Track(i).sGuDaoUseSeq = TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(6).ToString.Trim
                        CADStaInf(k).Track(i).sngLength = TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(7).ToString.Trim
                        CADStaInf(k).Track(i).sTrackNum = TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(8).ToString.Trim
                        CADStaInf(k).Track(i).X1 = Val(TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(10).ToString.Trim)
                        CADStaInf(k).Track(i).X2 = Val(TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(11).ToString.Trim)
                        CADStaInf(k).Track(i).Y1 = Val(TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(12).ToString.Trim)
                        CADStaInf(k).Track(i).Y2 = Val(TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(13).ToString.Trim)
                        CADStaInf(k).Track(i).sLeftLink1 = TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(14).ToString.Trim
                        CADStaInf(k).Track(i).sLeftLink2 = TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(15).ToString.Trim
                        CADStaInf(k).Track(i).sLeftLink3 = TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(16).ToString.Trim
                        CADStaInf(k).Track(i).sRightLink1 = TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(17).ToString.Trim
                        CADStaInf(k).Track(i).sRightLink2 = TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(18).ToString.Trim
                        CADStaInf(k).Track(i).sRightLink3 = TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(19).ToString.Trim
                        CADStaInf(k).Track(i).sTrackCircuitNum = TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(2).ToString.Trim
                        CADStaInf(k).Track(i).sControlNum = TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(9).ToString.Trim
                        CADStaInf(k).Track(i).sMemo = TMSlocalDataSet.TMS_LINEDRAW.Rows(i - 1).ItemArray(20).ToString.Trim
                        CADStaInf(k).Track(i).nDelete = 0
                        ReDim Preserve CADStaInf(k).Track(i).TrackOcupyFirTime(0)
                        ReDim Preserve CADStaInf(k).Track(i).TrackOcuPySecTime(0)
                    Next
                End If
            End If
        Next k
    End Sub
End Module

