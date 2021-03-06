Module modOpenTimetabale

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

    '根据两车站名确定区间ID
    Public Function FromStaNameToSecID(ByVal sFirName As String, ByVal sSecName As String) As Integer
        Dim i As Integer
        Dim sName1 As String
        Dim sName2 As String
        sName1 = sFirName & "->" & sSecName
        sName2 = sSecName & "->" & sFirName
        FromStaNameToSecID = 0
        For i = 1 To UBound(SectionInf)
            If SectionInf(i).sSecName = sName1 Or SectionInf(i).sSecName = sName2 Then
                FromStaNameToSecID = i
                Exit For
            End If
        Next i
    End Function
    '将分钟转化为秒
    Public Function MinuteToSecond(ByVal MinuteTime As String) As Single
        MinuteTime = Val(MinuteTime)
        MinuteToSecond = Int(MinuteTime) * 60 + (MinuteTime - Int(MinuteTime)) * 100
    End Function


    'Public Sub ReadBaseTrainInf()
    '    Dim i, j, p As Integer
    '    Dim ToNum As Integer
    '    Dim dFileName As dao.Database
    '    Dim myWS As dao.Workspace
    '    Dim KCheCheCi As dao.Recordset
    '    Dim DE As dao.DBEngine = New dao.DBEngine
    '    Dim TKche As dao.Recordset
    '    Dim nNum As Integer

    '    myWS = DE.Workspaces(0)
    '    dFileName = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
    '    KCheCheCi = dFileName.OpenRecordset("select * from 列车信息 order by 交路名") '序号")
    '    ReDim BaseTrainInf(0)
    '    If KCheCheCi.RecordCount > 0 Then
    '        KCheCheCi.MoveLast()
    '        ToNum = KCheCheCi.RecordCount
    '    End If
    '    ReDim BasicTrainInf(ToNum)
    '    Dim nJiNum As Int32
    '    Dim nOhNum As Int32
    '    nJiNum = 1
    '    nOhNum = 2
    '    If ToNum > 0 Then
    '        KCheCheCi.MoveFirst()
    '        For p = 1 To ToNum
    '            With BasicTrainInf(p)
    '                .sJiaoLuName = Trim(KCheCheCi("交路名").Value)
    '                If Trim(KCheCheCi("上下行").Value) = "上行" Then
    '                    .nUporDown = 2
    '                Else
    '                    .nUporDown = 1
    '                End If
    '                .TrainStyle = Trim(KCheCheCi("种类").Value)
    '                .StartStation = Trim(KCheCheCi("始发站").Value)
    '                .EndStation = Trim(KCheCheCi("终到站").Value)
    '                .ComeStation = Trim(KCheCheCi("始发站").Value)
    '                .NextStation = Trim(KCheCheCi("终到站").Value)

    '                ReDim Preserve .TrainReturnStyle(2)
    '                .TrainReturnStyle(1) = Trim(KCheCheCi("始发运用方式").Value)
    '                .TrainReturnStyle(2) = Trim(KCheCheCi("终到运用方式").Value)

    '                .sTrainXingZhi = "正常车" 'Trim(KCheCheCi("列车性质").Value)
    '                .SCheDiLeiXing = Trim(KCheCheCi("车底类型").Value)

    '                .sLineNum = Trim(KCheCheCi("线路编号").Value)
    '                .sMuDiNum = Trim(KCheCheCi("目的符").Value)

    '                Dim IfIn As Integer
    '                IfIn = 0

    '                '初始化设置
    '                ReDim .Arrival(UBound(StationInf))
    '                ReDim .Starting(UBound(StationInf))
    '                For j = 1 To UBound(StationInf)
    '                    .Arrival(j) = -1
    '                    .Starting(j) = -1
    '                Next j

    '                '以下代码读入停站信息
    '                nNum = 0
    '                TKche = dFileName.OpenRecordset("select * from 列车停站标尺信息 where 交路名称= '" & .sJiaoLuName & "' order by 停站种类,序号")
    '                If TKche.RecordCount > 0 Then
    '                    TKche.MoveLast()
    '                    nNum = TKche.RecordCount
    '                End If
    '                Dim sFirName As String
    '                Dim ntmpCurID As Integer
    '                ReDim .StopScale(0)
    '                If nNum > 0 Then
    '                    TKche.MoveFirst()
    '                    sFirName = TKche.Fields("停站种类").Value
    '                    ReDim Preserve .StopScale(1)
    '                    ReDim Preserve .StopScale(1).nStopStation(0)
    '                    ReDim Preserve .StopScale(1).StopTime(0)
    '                    ReDim Preserve .StopScale(1).sScaleName(0)
    '                    ntmpCurID = 1
    '                    .StopScale(ntmpCurID).sName = TKche.Fields("停站种类").Value
    '                    For i = 1 To nNum
    '                        If TKche.Fields("停站种类").Value = sFirName Then
    '                            ReDim Preserve .StopScale(ntmpCurID).nStopStation(UBound(.StopScale(ntmpCurID).nStopStation) + 1)
    '                            ReDim Preserve .StopScale(ntmpCurID).StopTime(UBound(.StopScale(ntmpCurID).StopTime) + 1)
    '                            ReDim Preserve .StopScale(ntmpCurID).sScaleName(UBound(.StopScale(ntmpCurID).sScaleName) + 1)
    '                            .StopScale(ntmpCurID).nStopStation(UBound(.StopScale(ntmpCurID).nStopStation)) = FromStaNameToStaIDByStationinf(TKche.Fields("车站名称").Value)
    '                            .StopScale(ntmpCurID).StopTime(UBound(.StopScale(ntmpCurID).StopTime)) = MinuteToSecond(TKche.Fields("停站时间").Value)
    '                            .StopScale(ntmpCurID).sScaleName(UBound(.StopScale(ntmpCurID).sScaleName)) = TKche.Fields("标尺名称").Value.ToString.Trim
    '                            .StopScale(ntmpCurID).nStopNum = .StopScale(ntmpCurID).nStopNum + 1
    '                        Else
    '                            ReDim Preserve .StopScale(UBound(.StopScale) + 1)
    '                            ntmpCurID = ntmpCurID + 1
    '                            ReDim Preserve .StopScale(ntmpCurID).nStopStation(0)
    '                            ReDim Preserve .StopScale(ntmpCurID).StopTime(0)
    '                            ReDim Preserve .StopScale(ntmpCurID).sScaleName(0)
    '                            .StopScale(ntmpCurID).sName = TKche.Fields("停站种类").Value.ToString.Trim
    '                            sFirName = TKche.Fields("停站种类").Value.ToString.Trim
    '                            ReDim Preserve .StopScale(ntmpCurID).nStopStation(UBound(.StopScale(ntmpCurID).nStopStation) + 1)
    '                            ReDim Preserve .StopScale(ntmpCurID).StopTime(UBound(.StopScale(ntmpCurID).StopTime) + 1)
    '                            ReDim Preserve .StopScale(ntmpCurID).sScaleName(UBound(.StopScale(ntmpCurID).sScaleName) + 1)
    '                            .StopScale(ntmpCurID).nStopStation(UBound(.StopScale(ntmpCurID).nStopStation)) = FromStaNameToStaIDByStationinf(TKche.Fields("车站名称").Value)
    '                            .StopScale(ntmpCurID).StopTime(UBound(.StopScale(ntmpCurID).StopTime)) = MinuteToSecond(TKche.Fields("停站时间").Value)
    '                            .StopScale(ntmpCurID).sScaleName(UBound(.StopScale(ntmpCurID).sScaleName)) = TKche.Fields("标尺名称").Value.ToString.Trim
    '                            .StopScale(ntmpCurID).nStopNum = .StopScale(ntmpCurID).nStopNum + 1
    '                        End If
    '                        TKche.MoveNext()
    '                    Next i
    '                End If


    '                '以下代码读入区间运行时分信息
    '                nNum = 0
    '                TKche = dFileName.OpenRecordset("select * from 列车运行标尺信息 where 交路名称= '" & .sJiaoLuName & "' order by 运行种类,序号")
    '                If TKche.RecordCount > 0 Then
    '                    TKche.MoveLast()
    '                    nNum = TKche.RecordCount
    '                End If
    '                sFirName = ""
    '                ntmpCurID = 0
    '                ReDim .SecScale(0)
    '                If nNum > 0 Then
    '                    TKche.MoveFirst()
    '                    sFirName = TKche.Fields("运行种类").Value
    '                    ReDim Preserve .SecScale(1)
    '                    ReDim Preserve .SecScale(1).nSecID(0)
    '                    ReDim Preserve .SecScale(1).RunTime(0)
    '                    ReDim Preserve .SecScale(1).sScaleName(0)

    '                    ntmpCurID = 1
    '                    .SecScale(ntmpCurID).sName = TKche.Fields("运行种类").Value
    '                    For i = 1 To nNum
    '                        If TKche.Fields("运行种类").Value = sFirName Then
    '                            ReDim Preserve .SecScale(ntmpCurID).nSecID(UBound(.SecScale(ntmpCurID).nSecID) + 1)
    '                            ReDim Preserve .SecScale(ntmpCurID).RunTime(UBound(.SecScale(ntmpCurID).RunTime) + 1)
    '                            ReDim Preserve .SecScale(ntmpCurID).sScaleName(UBound(.SecScale(ntmpCurID).sScaleName) + 1)
    '                            .SecScale(ntmpCurID).nSecID(UBound(.SecScale(ntmpCurID).nSecID)) = GetSectionID(TKche.Fields("区间名称").Value)
    '                            .SecScale(ntmpCurID).RunTime(UBound(.SecScale(ntmpCurID).RunTime)) = MinuteToSecond(TKche.Fields("运行时间").Value)
    '                            .SecScale(ntmpCurID).sScaleName(UBound(.SecScale(ntmpCurID).sScaleName)) = TKche.Fields("标尺名称").Value.ToString.Trim
    '                        Else
    '                            ReDim Preserve .SecScale(UBound(.SecScale) + 1)
    '                            ntmpCurID = ntmpCurID + 1
    '                            ReDim Preserve .SecScale(ntmpCurID).nSecID(0)
    '                            ReDim Preserve .SecScale(ntmpCurID).RunTime(0)
    '                            ReDim Preserve .SecScale(ntmpCurID).sScaleName(0)
    '                            .SecScale(ntmpCurID).sName = TKche.Fields("运行种类").Value
    '                            sFirName = TKche.Fields("运行种类").Value
    '                            ReDim Preserve .SecScale(ntmpCurID).nSecID(UBound(.SecScale(ntmpCurID).nSecID) + 1)
    '                            ReDim Preserve .SecScale(ntmpCurID).RunTime(UBound(.SecScale(ntmpCurID).RunTime) + 1)
    '                            ReDim Preserve .SecScale(ntmpCurID).sScaleName(UBound(.SecScale(ntmpCurID).sScaleName) + 1)
    '                            .SecScale(ntmpCurID).nSecID(UBound(.SecScale(ntmpCurID).nSecID)) = GetSectionID(TKche.Fields("区间名称").Value)
    '                            .SecScale(ntmpCurID).RunTime(UBound(.SecScale(ntmpCurID).RunTime)) = MinuteToSecond(TKche.Fields("运行时间").Value)
    '                            .SecScale(ntmpCurID).sScaleName(UBound(.SecScale(ntmpCurID).sScaleName)) = TKche.Fields("标尺名称").Value.ToString.Trim
    '                        End If
    '                        TKche.MoveNext()
    '                    Next i
    '                End If

    '                '以下代码读入列车径路信息

    '                Dim TrainPath As String
    '                TrainPath = Trim(KCheCheCi("列车径路").Value)
    '                If Right(TrainPath, 1) = "," Or Right(TrainPath, 1) = "，" Then
    '                Else
    '                    TrainPath = TrainPath & ","
    '                End If
    '                Dim TrainPathSta() As String
    '                ReDim TrainPathSta(0)

    '                '                '当列车径路字段的值为空时，出错，返回
    '                '                If Trim(TrainPath) = "" Or Trim(TrainPath) = "无" Then
    '                '                    Call InputErrToTrainErrInf(.Train, "列车的列车径路信息为空!","提示")
    '                '                    Exit Sub
    '                '                End If

    '                .sTrainPath = TrainPath
    '                i = 1
    '                For j = 1 To Len(TrainPath)
    '                    If Mid(TrainPath, j, 1) = "," Or Mid(TrainPath, j, 1) = "，" Then
    '                        If Trim(Mid(TrainPath, i, j - i)) <> "" Then
    '                            ReDim Preserve TrainPathSta(UBound(TrainPathSta) + 1)
    '                            TrainPathSta(UBound(TrainPathSta)) = Mid(TrainPath, i, j - i)
    '                            i = j + 1
    '                        End If
    '                    End If
    '                Next j

    '                If UBound(TrainPathSta) < 2 Then
    '                    ' Call InputErrToTrainErrInf(.Train, "列车径路通过的车站只有一个，不符合要求！")
    '                    Exit Sub
    '                End If

    '                '将列车径过的车站组成n-1个区间
    '                ReDim .nPassSection(UBound(TrainPathSta) - 1)
    '                ReDim .sSectionName(UBound(TrainPathSta) - 1)
    '                ReDim .StrFirstSta(UBound(TrainPathSta) - 1)
    '                ReDim .StrSecondSta(UBound(TrainPathSta) - 1)
    '                ReDim .nFirstID(UBound(TrainPathSta) - 1)
    '                ReDim .nSecondID(UBound(TrainPathSta) - 1)

    '                Dim strQianSta As String
    '                Dim StrHouSta As String
    '                Dim strQuJian1 As String
    '                Dim strQuJian2 As String
    '                Dim KK As Integer
    '                strQianSta = TrainPathSta(1)
    '                For i = 2 To UBound(TrainPathSta)
    '                    KK = 0
    '                    StrHouSta = TrainPathSta(i)
    '                    strQuJian1 = strQianSta & "->" & StrHouSta
    '                    strQuJian2 = StrHouSta & "->" & strQianSta
    '                    For j = 1 To UBound(SectionInf)
    '                        If SectionInf(j).sSecName = strQuJian1 Then
    '                            .nPassSection(i - 1) = j
    '                            .sSectionName(i - 1) = strQuJian1
    '                            .StrFirstSta(i - 1) = strQianSta
    '                            .StrSecondSta(i - 1) = StrHouSta
    '                            .nFirstID(i - 1) = SectionInf(j).nFirStaID
    '                            .nSecondID(i - 1) = SectionInf(j).nSecStaID
    '                            KK = 1
    '                        ElseIf SectionInf(j).sSecName = strQuJian2 Then
    '                            .nPassSection(i - 1) = j
    '                            .sSectionName(i - 1) = strQuJian2
    '                            .StrFirstSta(i - 1) = strQianSta
    '                            .StrSecondSta(i - 1) = StrHouSta
    '                            .nFirstID(i - 1) = SectionInf(j).nSecStaID
    '                            .nSecondID(i - 1) = SectionInf(j).nFirStaID
    '                            KK = 1
    '                        End If

    '                        If KK = 1 Then Exit For
    '                        If KK = 0 And j = UBound(SectionInf) Then
    '                            'Call InputErrToTrainErrInf(.Train, "区间" & strQuJian1 & "不存在，请确认底图打开是否有错！")
    '                            ' Stop
    '                        End If

    '                    Next j
    '                    strQianSta = StrHouSta
    '                Next i

    '                '导入nPathID()
    '                Dim ID1 As Integer
    '                Dim ID2 As Integer
    '                If UBound(.nPassSection) > 0 Then
    '                    ID1 = .nFirstID(1)
    '                    ID2 = .nSecondID(1)

    '                    ReDim .nPathID(2)
    '                    .nPathID(1) = ID1
    '                    .nPathID(2) = ID2

    '                    If UBound(.nPassSection) > 1 Then
    '                        For i = 2 To UBound(.nPassSection)
    '                            If .nFirstID(i) <> .nPathID(UBound(.nPathID)) Then
    '                                ReDim Preserve .nPathID(UBound(.nPathID) + 1)
    '                                .nPathID(UBound(.nPathID)) = .nFirstID(i)
    '                                ReDim Preserve .nPathID(UBound(.nPathID) + 1)
    '                                .nPathID(UBound(.nPathID)) = .nSecondID(i)
    '                            Else
    '                                ReDim Preserve .nPathID(UBound(.nPathID) + 1)
    '                                .nPathID(UBound(.nPathID)) = .nSecondID(i)
    '                            End If
    '                        Next i
    '                    End If

    '                End If

    '                '加入分岔站信息
    '                .NumWay = 0
    '                ReDim .Way1(0)
    '                ReDim .Way2(0)
    '                ReDim .Way3(0)

    '                For i = 1 To UBound(.nPathID)
    '                    If Left(StationInf(.nPathID(i)).sStationProp, 1) = "F" Then
    '                        .NumWay = .NumWay + 1
    '                        ReDim Preserve .Way1(.NumWay)
    '                        ReDim Preserve .Way2(.NumWay)
    '                        ReDim Preserve .Way3(.NumWay)
    '                        .Way1(.NumWay) = StationInf(.nPathID(i)).sStationName
    '                        .Way2(.NumWay) = GetBeforOrAfterStaNameFromCurSta(p, StationInf(.nPathID(i)).sStationName, 2)
    '                        .Way3(.NumWay) = GetBeforOrAfterStaNameFromCurSta(p, StationInf(.nPathID(i)).sStationName, 1)
    '                    End If
    '                Next i

    '                KCheCheCi.MoveNext()
    '            End With
    '        Next p
    '    Else
    '        Exit Sub
    '    End If
    'End Sub

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


    ''读入车底连接信息
    'Public Sub readCheDiLinkInf(ByVal sSKBID As String, ByVal proBar As ProgressBar)
    '    Dim i, j, p As Integer
    '    Dim tempName As String
    '    Dim TempFind As Integer
    '    Dim sname As String
    '    Dim DB As dao.Database
    '    Dim Rs As dao.Recordset
    '    Dim ToNum As Integer
    '    Dim nTrain As Integer
    '    Dim nTempTrain As Integer
    '    Dim rsDb As dao.Recordset
    '    For i = 1 To UBound(ChediInfo)
    '        ReDim ChediInfo(i).nLinkTrain(0)
    '    Next
    '    ReDim TrainInf(0)
    '    Dim myWS As dao.Workspace
    '    Dim DE As dao.DBEngine = New dao.DBEngine

    '    myWS = DE.Workspaces(0)

    '    DB = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)

    '    For i = 0 To DB.TableDefs.Count - 1
    '        tempName = DB.TableDefs(i).Name
    '        If Len(Trim(tempName)) >= 6 Then
    '            If Right(Trim(tempName), 6) = "车底使用方案" And Left(tempName, Len(Trim(tempName)) - 6) = sSKBID Then
    '                Rs = DB.OpenRecordset(tempName)
    '                TempFind = 1
    '            End If
    '        End If
    '    Next i

    '    If TempFind = 1 Then
    '        sname = sSKBID & "车底使用方案"
    '        rsDb = DB.OpenRecordset("select * from  " & sname & " order by 车底顺序,连接顺序 ") '(sname) '
    '        If rsDb.RecordCount > 0 Then
    '            rsDb.MoveLast()
    '            ToNum = rsDb.RecordCount
    '        Else
    '            ToNum = 0
    '        End If

    '        Dim stmpSta As Integer
    '        Dim tmpBeforSta As Integer
    '        Dim tmpID As Integer
    '        Dim tmpXH As Integer
    '        If ToNum > 0 Then
    '            rsDb.MoveFirst()
    '            tmpID = 1
    '            tmpBeforSta = Int(rsDb("车底顺序").Value)
    '            ReDim ChediInfo(tmpID)
    '            ReDim ChediInfo(tmpID).nLinkTrain(0)
    '            Call CopyCheDiInformation(tmpID, rsDb.Fields("车底ID").Value)
    '            If tmpID > 0 Then

    '            Else
    '                MsgBox("车底信息与当前运行图信息不对应，请检查运行图和车底信息!","提示")
    '            End If
    '            tmpXH = 1

    '            For i = 1 To ToNum
    '                stmpSta = Int(rsDb.Fields("车底顺序").Value)
    '                If stmpSta <> tmpBeforSta Then
    '                    tmpBeforSta = stmpSta
    '                    tmpID = tmpID + 1
    '                    ReDim Preserve ChediInfo(tmpID)
    '                    ReDim ChediInfo(tmpID).nLinkTrain(0)
    '                    Call CopyCheDiInformation(tmpID, rsDb.Fields("车底ID").Value)
    '                    tmpXH = 1
    '                End If
    '                If tmpID > 0 Then
    '                    tmpXH = 1
    '                    'If rsDb.Fields("连挂车次").Value = "16008" Then Stop
    '                    nTrain = AddTrainInformation(Trim(rsDb.Fields("交路类型").Value), Trim(rsDb.Fields("标尺类型").Value), Trim(rsDb.Fields("停站标尺").Value), rsDb.Fields("连挂车次").Value)
    '                    TrainInf(nTrain).Train = Trim(rsDb.Fields("连挂车次").Value)
    '                    TrainInf(nTrain).sPrintTrain = Trim(rsDb.Fields("输出车次").Value)
    '                    TrainInf(nTrain).nZfLimit = Trim(rsDb.Fields("是否折返约束").Value)
    '                    TrainInf(nTrain).sTrainXingZhi = Trim(rsDb.Fields("列车性质").Value)

    '                    TrainInf(nTrain).PrintLineStyle = GetLineStyleFromText(rsDb.Fields("运行线线型").Value.ToString.Trim)
    '                    TrainInf(nTrain).PrintLineWidth = rsDb.Fields("运行线线宽").Value
    '                    TrainInf(nTrain).PrintLineColor = System.Drawing.ColorTranslator.FromHtml(rsDb.Fields("运行线颜色").Value.ToString.Trim)
    '                    ' If nTrain = 0 Then Stop
    '                    If nTrain > 0 And TrainInf(nTrain).Train <> "" Then
    '                        ReDim Preserve ChediInfo(tmpID).nLinkTrain(UBound(ChediInfo(tmpID).nLinkTrain) + 1)
    '                        ChediInfo(tmpID).nLinkTrain(UBound(ChediInfo(tmpID).nLinkTrain)) = nTrain
    '                    End If

    '                    ChediInfo(tmpID).PrintCheDiLinkStyle = GetLineStyleFromText(rsDb.Fields("车底线线型").Value.ToString.Trim)
    '                    ChediInfo(tmpID).PrintCheDiLinkWidth = rsDb.Fields("车底线线宽").Value
    '                    ChediInfo(tmpID).PrintCheDiLinkColor = System.Drawing.ColorTranslator.FromHtml(rsDb.Fields("车底线颜色").Value.ToString.Trim)
    '                    If rsDb.Fields("是否折返约束").Value = 1 Then
    '                        ChediInfo(tmpID).bIfGouWang = True
    '                    Else
    '                        ChediInfo(tmpID).bIfGouWang = False
    '                    End If
    '                Else
    '                    MsgBox("车底信息与当前运行图信息不对应，请检查运行图和车底信息!","提示")
    '                End If
    '                tmpXH = tmpXH + 1
    '                rsDb.MoveNext()
    '                proBar.Value = 10 + Int(i * 40 / ToNum)
    '            Next i
    '        End If

    '    Else
    '        MsgBox("没有找到" & sSKBID & "车底使用方案表")
    '    End If


    '    For j = 1 To UBound(ChediInfo)
    '        nTempTrain = 0
    '        For p = 1 To UBound(ChediInfo(j).nLinkTrain)
    '            nTrain = ChediInfo(j).nLinkTrain(p)
    '            TrainInf(nTrain).SCheDiLeiXing = ChediInfo(j).SCheDiLeiXing
    '            If nTrain <> 0 And TrainInf(nTrain).Train <> "" Then
    '                If nTempTrain = 0 Then
    '                    TrainInf(nTrain).TrainReturn(1) = 0
    '                    TrainInf(nTrain).TrainReturn(2) = 0
    '                Else
    '                    TrainInf(nTempTrain).TrainReturn(2) = nTrain
    '                    TrainInf(nTrain).TrainReturn(1) = nTempTrain
    '                    TrainInf(nTrain).TrainReturn(2) = 0
    '                End If
    '                TrainInf(nTrain).nCheDiPuOrNot = 1
    '                nTempTrain = nTrain
    '            End If
    '        Next p
    '    Next j
    'End Sub

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

    '增加列车，由BaseTraininf中复制过来，并返回复制后列车的ID号
    Public Function AddTrainInformation(ByVal sJiaoLuName As String, ByVal sTrainStyle As String, ByVal sStopScale As String, ByVal sCheci As String) As Integer
        Dim i As Integer
        Dim nWeiNum As Integer
        Dim nBaseId As Integer
        nBaseId = GetBasicTrainInfID(sJiaoLuName)
        If nBaseId = 0 Then
            AddTrainInformation = 0
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
                        AddTrainInformation = i
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
                        AddTrainInformation = i
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
                    AddTrainInformation = nWeiNum
                    GoTo sEnd
                Else
                    nWeiNum = UBound(TrainInf) + 1
                    ReDim Preserve TrainInf(nWeiNum)
                    If sCheci = "" Then
                        sCheci = nWeiNum
                    End If
                    Call CopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale)
                    'Call CopyFenTingBaseTrainInf(nBaseId, nWeiNum)
                    AddTrainInformation = nWeiNum
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
                    AddTrainInformation = nWeiNum
                    GoTo sEnd
                Else
                    nWeiNum = UBound(TrainInf) + 2
                    ReDim Preserve TrainInf(nWeiNum)
                    If sCheci = "" Then
                        sCheci = nWeiNum
                    End If
                    Call CopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale)
                    'Call CopyFenTingBaseTrainInf(nBaseId, nWeiNum)
                    AddTrainInformation = nWeiNum
                    GoTo sEnd
                End If
            End If
        End If
sEnd:
        Call SetTrainDefautColor(AddTrainInformation)
        'TrainInf(AddTrainInformation).nTrainTimeKind = sTimeScaleToTimeKind(sKind)
        'TrainInf(AddTrainInformation).sTrainTimeScale = sKind
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
                .trainreturn(i) = 0 ' BasicTrainInf(nTrainNumFrom).TrainReturn(i)
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
        Call SetTrainDefautColor(nTrainNumTo)
    End Sub

    '复制列车分岔站基础信息，BaseTraininf
    '复制列车
    Sub CopyFenTingBaseTrainInf(ByVal nTrainNumFrom As Integer, ByVal nTrainNumTo As Integer)

    End Sub

    '将时刻表读入
    Public Sub InputSKBInfByDAO(ByVal sSKBID As String, ByVal proBar As ProgressBar)
        Dim myWS As dao.Workspace
        Dim DE As dao.DBEngine = New dao.DBEngine
        myWS = DE.Workspaces(0)
        Dim dFile As dao.Database
        Dim sFile As dao.Recordset
        Dim nNum As Integer

        dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        Dim i, j, k, p As Integer
        Dim q As Integer
        Dim sCurName As String
        Dim sTrainNum() As String
        ReDim sTrainNum(0)
        sCurName = sSKBID & "列车时刻信息"
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
                Dim strTable3 As String = "select * from " & sCurName & " where 车次='" & TrainInf(i).Train & "' order by ID"
                sFile = dFile.OpenRecordset(strTable3)
                nNum = 0
                If sFile.RecordCount > 0 Then
                    sFile.MoveLast()
                    nNum = sFile.RecordCount
                End If

                If nNum > 0 Then
                    sFile.MoveFirst()
                    For p = 1 To nNum
                        nifIn = 0
                        If p = nNum - 1 Then
                            TrainInf(j).sStartZFStarting = sFile.Fields("发点").Value 'myTable3.Rows(p - 1).Item("发点") ' HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                            TrainInf(j).sStartZFArrival = sFile.Fields("到点").Value 'myTable3.Rows(p - 1).Item("到点") ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                            TrainInf(j).sStartZFLine = sFile.Fields("停站股道").Value.ToString.Trim  'myTable3.Rows(p - 1).Item("停站股道")
                        ElseIf p = nNum Then
                            TrainInf(j).sEndZFStarting = sFile.Fields("发点").Value 'myTable3.Rows(p - 1).Item("发点") ' HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                            TrainInf(j).sEndZFArrival = sFile.Fields("到点").Value ' myTable3.Rows(p - 1).Item("到点") ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                            TrainInf(j).sEndZFLine = sFile.Fields("停站股道").Value.ToString.Trim  '
                        Else
                            For q = 1 To UBound(StationInf)
                                If StationInf(q).sStationName = sFile.Fields("车站名称").Value.ToString.Trim Then
                                    nStaID = q
                                    TrainInf(j).Starting(nStaID) = sFile.Fields("发点").Value ' myTable3.Rows(p - 1).Item("发点") ' HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                                    TrainInf(j).Arrival(nStaID) = sFile.Fields("到点").Value ' myTable3.Rows(p - 1).Item("到点") ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                                    TrainInf(j).StopLine(nStaID) = sFile.Fields("停站股道").Value.ToString.Trim  'myTable3.Rows(p - 1).Item("停站股道")
                                    nifIn = 1
                                End If
                            Next
                            If nifIn = 0 Then
                                nErrorTrain = nErrorTrain + 1
                            End If
                        End If
                        sFile.MoveNext()
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
            proBar.Value = 50 + Int(i * 50 / UBound(TrainInf))
        Next i
        If nErrorTrain > 0 Then
            MsgBox("列车信息中有" & nErrorTrain & "趟列车没有时刻，请检查当前的底图结构是否选择错误！")
        End If
    End Sub

    '将时刻表读入
    Public Sub InputSKBInfByDAO_Old(ByVal sSKBID As String, ByVal proBar As ProgressBar)
        Dim myWS As dao.Workspace
        Dim DE As dao.DBEngine = New dao.DBEngine
        myWS = DE.Workspaces(0)
        Dim dFile As dao.Database
        Dim sFile As dao.Recordset
        Dim nNum As Integer

        dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        Dim i, j, k, p As Integer
        Dim sCurName As String
        Dim sTrainNum() As String
        ReDim sTrainNum(0)
        sCurName = sSKBID & "列车时刻信息"

        Dim strTable2 As String = "select distinct 车次 from " & sCurName & ""
        sFile = dFile.OpenRecordset(strTable2)
        nNum = 0
        If sFile.RecordCount > 0 Then
            sFile.MoveLast()
            nNum = sFile.RecordCount
        End If
        ReDim sTrainNum(nNum)
        If nNum > 0 Then
            sFile.MoveFirst()
            For i = 1 To nNum
                sTrainNum(i) = sFile.Fields("车次").Value.ToString.Trim
                sFile.MoveNext()
            Next
        End If
        For i = 1 To UBound(sTrainNum)
            Dim strTable3 As String = "select * from " & sCurName & " where 车次='" & sTrainNum(i) & "' order by ID"
            sFile = dFile.OpenRecordset(strTable3)
            nNum = 0
            If sFile.RecordCount > 0 Then
                sFile.MoveLast()
                nNum = sFile.RecordCount
            End If

            If nNum > 0 Then
                sFile.MoveFirst()
                For j = 1 To UBound(TrainInf)
                    If TrainInf(j).Train = sTrainNum(i) Then
                        For k = 1 To UBound(StationInf)
                            TrainInf(j).Starting(k) = -1
                            TrainInf(j).Arrival(k) = -1
                            TrainInf(j).StopLine(k) = ""
                        Next k
                        For p = 1 To nNum
                            For k = 1 To UBound(StationInf)
                                If StationInf(k).sStationName = sFile.Fields("车站名称").Value.ToString.Trim Then ' Trim(myTable3.Rows(p - 1).Item("车站名称")) Then
                                    TrainInf(j).TrainPuorNot = 1
                                    TrainInf(j).Starting(k) = sFile.Fields("发点").Value ' myTable3.Rows(p - 1).Item("发点") ' HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                                    TrainInf(j).Arrival(k) = sFile.Fields("到点").Value ' myTable3.Rows(p - 1).Item("到点") ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                                    TrainInf(j).StopLine(k) = sFile.Fields("停站股道").Value.ToString.Trim  'myTable3.Rows(p - 1).Item("停站股道")
                                End If
                            Next k
                            If sFile.Fields("车站名称").Value.ToString.Trim = "A始发折返A" Then '
                                TrainInf(j).sStartZFStarting = sFile.Fields("发点").Value 'myTable3.Rows(p - 1).Item("发点") ' HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                                TrainInf(j).sStartZFArrival = sFile.Fields("到点").Value 'myTable3.Rows(p - 1).Item("到点") ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                                TrainInf(j).sStartZFLine = sFile.Fields("停站股道").Value.ToString.Trim  'myTable3.Rows(p - 1).Item("停站股道")
                            ElseIf sFile.Fields("车站名称").Value.ToString.Trim = "B终到折返B" Then
                                TrainInf(j).sEndZFStarting = sFile.Fields("发点").Value 'myTable3.Rows(p - 1).Item("发点") ' HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                                TrainInf(j).sEndZFArrival = sFile.Fields("到点").Value ' myTable3.Rows(p - 1).Item("到点") ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                                TrainInf(j).sEndZFLine = sFile.Fields("停站股道").Value.ToString.Trim  '
                            End If
                            sFile.MoveNext()
                        Next p
                        TrainInf(j).lAllStartTime = TrainInf(j).Starting(TrainInf(j).nPathID(1))
                        TrainInf(j).lAllEndTime = TrainInf(j).Arrival(TrainInf(j).nPathID(UBound(TrainInf(j).nPathID)))
                        Exit For
                    End If
                Next j
            End If
            proBar.Value = 50 + Int(i * 50 / UBound(sTrainNum))
        Next i
    End Sub

    '将时刻表读入
    Public Sub InputSKBInf(ByVal proBar As ProgressBar)

        Dim i, j, k, p As Integer
        Dim sCurName As String
        Dim sTrainNum() As String
        ReDim sTrainNum(0)
        sCurName = TimeTablePara.sPubCurSkbName & "列车时刻信息"
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)

        Dim strTable2 As String = "select distinct 车次 from " & sCurName & ""
        Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
        Dim myDataSet2 As Data.DataSet = New Data.DataSet
        Mydc2.Fill(myDataSet2, "列车时刻信息")
        Dim myTable2 As Data.DataTable
        myTable2 = myDataSet2.Tables("列车时刻信息")

        ReDim sTrainNum(myTable2.Rows.Count)
        For i = 1 To myTable2.Rows.Count
            sTrainNum(i) = Trim(myTable2.Rows(i - 1).Item("车次"))
        Next

        For i = 1 To UBound(sTrainNum)
            Dim strTable3 As String = "select * from " & sCurName & " where 车次='" & sTrainNum(i) & "' order by ID"
            Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
            Dim myDataSet3 As Data.DataSet = New Data.DataSet
            Mydc3.Fill(myDataSet3, "列车时刻信息")
            Dim myTable3 As Data.DataTable
            myTable3 = myDataSet3.Tables("列车时刻信息")
            For j = 1 To UBound(TrainInf)
                If TrainInf(j).Train = sTrainNum(i) Then
                    For k = 1 To UBound(StationInf)
                        TrainInf(j).Starting(k) = -1
                        TrainInf(j).Arrival(k) = -1
                        TrainInf(j).StopLine(k) = ""
                    Next k
                    For p = 1 To myTable3.Rows.Count
                        For k = 1 To UBound(StationInf)
                            If StationInf(k).sStationName = Trim(myTable3.Rows(p - 1).Item("车站名称")) Then
                                TrainInf(j).TrainPuorNot = 1
                                TrainInf(j).Starting(k) = myTable3.Rows(p - 1).Item("发点") ' HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                                TrainInf(j).Arrival(k) = myTable3.Rows(p - 1).Item("到点") ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                                TrainInf(j).StopLine(k) = myTable3.Rows(p - 1).Item("停站股道")
                            End If
                        Next k
                        If Trim(myTable3.Rows(p - 1).Item("车站名称")).Trim = "A始发折返A" Then '
                            TrainInf(j).sStartZFStarting = myTable3.Rows(p - 1).Item("发点") ' HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                            TrainInf(j).sStartZFArrival = myTable3.Rows(p - 1).Item("到点") ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                            TrainInf(j).sStartZFLine = myTable3.Rows(p - 1).Item("停站股道")
                        ElseIf Trim(myTable3.Rows(p - 1).Item("车站名称")).Trim = "B终到折返B" Then
                            TrainInf(j).sEndZFStarting = myTable3.Rows(p - 1).Item("发点") ' HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                            TrainInf(j).sEndZFArrival = myTable3.Rows(p - 1).Item("到点") ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                            TrainInf(j).sEndZFLine = myTable3.Rows(p - 1).Item("停站股道")
                        End If
                    Next p
                    Exit For
                End If
            Next j
            proBar.Value = 50 + Int(i * 50 / UBound(sTrainNum))
        Next i
    End Sub

    ''读入列车与时刻表信息
    'Public Sub ReadTrainAndTimeTableInf(ByVal sSKBID As String, ByVal proBar As ProgressBar)
    '    proBar.Value = 0
    '    Call readCheDiLinkInf(sSKBID, proBar)
    '    proBar.Value = 50
    '    ' Call InputSKBInf(proBar)
    '    Call InputSKBInfByDAO(sSKBID, proBar)
    'End Sub

    'Public Sub InputChediZhefanDataAndCheDiScaleInf()
    '    Dim DBChediJiche As DAO.Database
    '    Dim RSChediZhefanInfo As DAO.Recordset
    '    Dim i As Integer
    '    Dim j As Integer
    '    ReDim ChediZhefanBiaozhunInfo(0)

    '    Dim myWS As DAO.Workspace
    '    Dim DE As DAO.DBEngine = New DAO.DBEngine
    '    myWS = DE.Workspaces(0)

    '    DBChediJiche = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
    '    RSChediZhefanInfo = DBChediJiche.OpenRecordset("车底折返时间标准")
    '    If RSChediZhefanInfo.BOF <> True Then
    '        RSChediZhefanInfo.MoveFirst()
    '    End If

    '    Do While RSChediZhefanInfo.EOF <> True
    '        ReDim Preserve ChediZhefanBiaozhunInfo(UBound(ChediZhefanBiaozhunInfo) + 1)
    '        i = UBound(ChediZhefanBiaozhunInfo)
    '        With ChediZhefanBiaozhunInfo(i)
    '            .SCheDiLeiXing = RSChediZhefanInfo.Fields("车底类型").Value
    '            .sZhefanStation = RSChediZhefanInfo.Fields("车站名称").Value
    '            .lLijiZhefanTime = MinuteToSecond(RSChediZhefanInfo.Fields("立即折返时间").Value)
    '            '.lTingliuZhefanTime = 0 ' MinuteToSecond(RSChediZhefanInfo.Fields("停留折返时间"))
    '            '.lZhuanxianZhefanTime = 0  'MinuteToSecond(RSChediZhefanInfo.Fields("转线折返时间"))
    '            .lZhanQianTime = MinuteToSecond(RSChediZhefanInfo.Fields("站前折返时间").Value)
    '            .lZhanHouTime = MinuteToSecond(RSChediZhefanInfo.Fields("站后折返时间").Value)
    '            '.lRukuTime = 0 ' MinuteToSecond(RSChediZhefanInfo.Fields("入库时间"))
    '            '.lChukuTime = 0  'MinuteToSecond(RSChediZhefanInfo.Fields("出库时间"))
    '            '.lZaikuStopTime = 0 ' MinuteToSecond(RSChediZhefanInfo.Fields("库内检修时间"))
    '            .nFirRunTime = MinuteToSecond(RSChediZhefanInfo.Fields("到达股道至折返线时间").Value)
    '            .nSecRunTime = MinuteToSecond(RSChediZhefanInfo.Fields("折返线至出发股道时间").Value)
    '            .nArrFaDaoTime = MinuteToSecond(RSChediZhefanInfo.Fields("到达发到间隔").Value)
    '            .nStartFaDaoTime = MinuteToSecond(RSChediZhefanInfo.Fields("出发发到间隔").Value)
    '        End With
    '        RSChediZhefanInfo.MoveNext()
    '    Loop


    '    '   读入车底标尺信息
    '    Dim ToNum As Integer
    '    ReDim TrainRunScaleInf(0)
    '    RSChediZhefanInfo = DBChediJiche.OpenRecordset("select distinct 标尺名称,标尺编号 from 列车运行标尺 order by 标尺编号")
    '    If RSChediZhefanInfo.RecordCount > 0 Then
    '        RSChediZhefanInfo.MoveLast()
    '        ToNum = RSChediZhefanInfo.RecordCount
    '    Else
    '        ToNum = 0
    '    End If

    '    If ToNum > 0 Then
    '        ReDim TrainRunScaleInf(ToNum)
    '        RSChediZhefanInfo.MoveFirst()
    '        For j = 1 To RSChediZhefanInfo.RecordCount
    '            TrainRunScaleInf(j).nScaleID = Val(RSChediZhefanInfo.Fields("标尺编号").Value)
    '            TrainRunScaleInf(j).sScaleName = RSChediZhefanInfo.Fields("标尺名称").Value.ToString.Trim
    '            RSChediZhefanInfo.MoveNext()
    '        Next j
    '    End If

    '    RSChediZhefanInfo.Close()
    '    DBChediJiche.Close()
    'End Sub

    ''打开车底信息和发车间隔安排
    'Public Sub InputChediAndTrainJianGeData()
    '    Dim myWS As DAO.Workspace
    '    Dim DE As DAO.DBEngine = New DAO.DBEngine
    '    myWS = DE.Workspaces(0)

    '    Dim DBChediJiche As DAO.Database
    '    Dim RSChediInfo As DAO.Recordset
    '    Dim i As Integer
    '    ReDim BaseChediInfo(0)
    '    DBChediJiche = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
    '    RSChediInfo = DBChediJiche.OpenRecordset("select * from 车底信息 order by 车底ID") 'val(出库时间)

    '    If RSChediInfo.BOF <> True Then
    '        RSChediInfo.MoveFirst()
    '    End If

    '    Do While RSChediInfo.EOF <> True
    '        ReDim Preserve BaseChediInfo(UBound(BaseChediInfo) + 1)
    '        i = UBound(BaseChediInfo)
    '        With BaseChediInfo(i)
    '            .SCheDiLeiXing = RSChediInfo.Fields("车底类型").Value
    '            .bIfGouWang = 0
    '            ReDim BaseChediInfo(i).nLinkTrain(0)
    '            Exit Do
    '        End With
    '        RSChediInfo.MoveNext()
    '    Loop

    '    If RSChediInfo.BOF <> True Then
    '        RSChediInfo.MoveFirst()
    '    End If

    '    'Do While RSChediInfo.EOF <> True
    '    '    ReDim Preserve ChediInfo(UBound(ChediInfo) + 1)
    '    '    i = UBound(ChediInfo)
    '    '    With ChediInfo(i)
    '    '        .SCheDiLeiXing = RSChediInfo.Fields("车底类型").Value
    '    '        .sCheDiID = RSChediInfo.Fields("车底ID").Value
    '    '        .sCheCiHao = .sCheDiID
    '    '        '.sStation = RSChediInfo.Fields("车站名称")
    '    '        .sDayBeginStation = RSChediInfo.Fields("日始出发车站").Value
    '    '        .sDayEndStation = RSChediInfo.Fields("日终停留车站").Value
    '    '        '.sRukuZhouqi = RSChediInfo.Fields("入库周期")
    '    '        '.lYunyongTime = RSChediInfo.Fields("运用时间")
    '    '        .nYunxingBiaochi = RSChediInfo.Fields("运行标尺").Value
    '    '        .bIfGouWang = False
    '    '        .sChuKuTime = HourToSecond(RSChediInfo.Fields("出库时间").Value)
    '    '        '.sChuKuFangShi = RSChediInfo.Fields("出库方式")
    '    '        .sRuKuTime = HourToSecond(RSChediInfo.Fields("入库时间").Value)
    '    '        '.sRuKuFangShi = RSChediInfo.Fields("入库方式")
    '    '        '.sChuKuSta = RSChediInfo.Fields("出库车站")
    '    '        '.sRuKuSta = RSChediInfo.Fields("入库车站")
    '    '        .sBeiZhu = RSChediInfo.Fields("备注").Value
    '    '        .sYunYongFangShi = RSChediInfo.Fields("运用方式").Value
    '    '        .nIfFixCheDi = 0
    '    '        'ReDim ChediInfo(i).nDayItem(0)
    '    '        .PrintCheDiLinkStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.CheDiLineStyle)
    '    '        .PrintCheDiLinkWidth = TimeTablePara.DiagramStylePara.CheDiLineWidth
    '    '        .PrintCheDiLinkColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.CheDiLineColor)
    '    '        ReDim ChediInfo(i).nLinkTrain(0)
    '    '        'ReDim ChediInfo(i).bIfEnterGZ(0)
    '    '    End With
    '    '    RSChediInfo.MoveNext()
    '    'Loop
    '    Dim stmpSta As String
    '    Dim tmpBeforSta As String
    '    Dim stmpPH As String
    '    Dim stmpBePH As String

    '    Dim tmpID As Integer
    '    Dim tmpXH As Integer
    '    ReDim GaoFenTimeSet(1)
    '    tmpID = 1
    '    ReDim GaoFenTimeSet(tmpID).nXuHao(0)
    '    ReDim GaoFenTimeSet(tmpID).BeTime(0)
    '    ReDim GaoFenTimeSet(tmpID).EndTime(0)
    '    ReDim GaoFenTimeSet(tmpID).JGtime(0)
    '    ReDim GaoFenTimeSet(tmpID).JGOne(0)
    '    ReDim GaoFenTimeSet(tmpID).JGTwo(0)
    '    ReDim GaoFenTimeSet(tmpID).NumOne(0)
    '    ReDim GaoFenTimeSet(tmpID).NumTwo(0)
    '    ReDim GaoFenTimeSet(tmpID).ChediNum(0)
    '    ReDim GaoFenTimeSet(tmpID).lZhouQiTime(0)
    '    ReDim GaoFenTimeSet(tmpID).sRunScaleName(0)
    '    ReDim GaoFenTimeSet(tmpID).sStopScaleName(0)
    '    ReDim GaoFenTimeSet(tmpID).lFirZheFanTime(0)
    '    ReDim GaoFenTimeSet(tmpID).lEndZheFanTime(0)
    '    ReDim GaoFenTimeSet(tmpID).lDownRunTime(0)
    '    ReDim GaoFenTimeSet(tmpID).lUpRunTime(0)
    '    ReDim GaoFenTimeSet(tmpID).lDownStopTime(0)
    '    ReDim GaoFenTimeSet(tmpID).lUpStopTime(0)

    '    Dim RSdata As DAO.Recordset
    '    Dim Tnum As Integer
    '    Tnum = 0
    '    RSdata = DBChediJiche.OpenRecordset("select * from 发车间隔安排 order by 交路类型,铺画类型,时间段")
    '    If RSdata.RecordCount > 0 Then
    '        RSdata.MoveLast()
    '        Tnum = RSdata.RecordCount
    '    Else
    '        Tnum = 0
    '    End If
    '    If Tnum > 0 Then
    '        RSdata.MoveFirst()
    '        tmpBeforSta = RSdata.Fields("交路类型").Value
    '        stmpBePH = RSdata.Fields("铺画类型").Value
    '        tmpID = 1
    '        ReDim GaoFenTimeSet(tmpID)
    '        GaoFenTimeSet(tmpID).sJLstyle = tmpBeforSta
    '        GaoFenTimeSet(tmpID).sPuHuaStyle = stmpBePH
    '        tmpXH = 1
    '        For i = 1 To Tnum
    '            stmpSta = RSdata.Fields("交路类型").Value
    '            stmpPH = RSdata.Fields("铺画类型").Value
    '            If stmpSta = tmpBeforSta Then
    '                If stmpPH <> stmpBePH Then
    '                    stmpBePH = stmpPH
    '                    tmpID = tmpID + 1
    '                    ReDim Preserve GaoFenTimeSet(tmpID)
    '                    GaoFenTimeSet(tmpID).sJLstyle = tmpBeforSta
    '                    GaoFenTimeSet(tmpID).sPuHuaStyle = stmpBePH

    '                    ReDim GaoFenTimeSet(tmpID).nXuHao(0)
    '                    ReDim GaoFenTimeSet(tmpID).BeTime(0)
    '                    ReDim GaoFenTimeSet(tmpID).EndTime(0)
    '                    ReDim GaoFenTimeSet(tmpID).JGtime(0)
    '                    ReDim GaoFenTimeSet(tmpID).JGOne(0)
    '                    ReDim GaoFenTimeSet(tmpID).JGTwo(0)
    '                    ReDim GaoFenTimeSet(tmpID).NumOne(0)
    '                    ReDim GaoFenTimeSet(tmpID).NumTwo(0)
    '                    ReDim GaoFenTimeSet(tmpID).ChediNum(0)
    '                    ReDim GaoFenTimeSet(tmpID).lZhouQiTime(0)
    '                    ReDim GaoFenTimeSet(tmpID).sRunScaleName(0)
    '                    ReDim GaoFenTimeSet(tmpID).sStopScaleName(0)
    '                    ReDim GaoFenTimeSet(tmpID).lFirZheFanTime(0)
    '                    ReDim GaoFenTimeSet(tmpID).lEndZheFanTime(0)
    '                    ReDim GaoFenTimeSet(tmpID).lDownRunTime(0)
    '                    ReDim GaoFenTimeSet(tmpID).lUpRunTime(0)
    '                    ReDim GaoFenTimeSet(tmpID).lDownStopTime(0)
    '                    ReDim GaoFenTimeSet(tmpID).lUpStopTime(0)

    '                    tmpXH = 1
    '                End If
    '            Else
    '                tmpBeforSta = stmpSta
    '                stmpBePH = stmpPH
    '                tmpID = tmpID + 1
    '                ReDim Preserve GaoFenTimeSet(tmpID)
    '                GaoFenTimeSet(tmpID).sJLstyle = tmpBeforSta
    '                GaoFenTimeSet(tmpID).sPuHuaStyle = stmpBePH

    '                ReDim GaoFenTimeSet(tmpID).nXuHao(0)
    '                ReDim GaoFenTimeSet(tmpID).BeTime(0)
    '                ReDim GaoFenTimeSet(tmpID).EndTime(0)
    '                ReDim GaoFenTimeSet(tmpID).JGtime(0)
    '                ReDim GaoFenTimeSet(tmpID).JGOne(0)
    '                ReDim GaoFenTimeSet(tmpID).JGTwo(0)
    '                ReDim GaoFenTimeSet(tmpID).NumOne(0)
    '                ReDim GaoFenTimeSet(tmpID).NumTwo(0)
    '                ReDim GaoFenTimeSet(tmpID).ChediNum(0)
    '                ReDim GaoFenTimeSet(tmpID).lZhouQiTime(0)
    '                ReDim GaoFenTimeSet(tmpID).sRunScaleName(0)
    '                ReDim GaoFenTimeSet(tmpID).sStopScaleName(0)
    '                ReDim GaoFenTimeSet(tmpID).lFirZheFanTime(0)
    '                ReDim GaoFenTimeSet(tmpID).lEndZheFanTime(0)
    '                ReDim GaoFenTimeSet(tmpID).lDownRunTime(0)
    '                ReDim GaoFenTimeSet(tmpID).lUpRunTime(0)
    '                ReDim GaoFenTimeSet(tmpID).lDownStopTime(0)
    '                ReDim GaoFenTimeSet(tmpID).lUpStopTime(0)
    '                tmpXH = 1
    '            End If

    '            ReDim Preserve GaoFenTimeSet(tmpID).nXuHao(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).BeTime(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).EndTime(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).JGtime(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).JGOne(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).JGTwo(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).NumOne(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).NumTwo(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).ChediNum(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).lZhouQiTime(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).sRunScaleName(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).sStopScaleName(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).lFirZheFanTime(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).lEndZheFanTime(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).lDownRunTime(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).lUpRunTime(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).lDownStopTime(tmpXH)
    '            ReDim Preserve GaoFenTimeSet(tmpID).lUpStopTime(tmpXH)

    '            GaoFenTimeSet(tmpID).nXuHao(tmpXH) = RSdata.Fields("时间段").Value
    '            GaoFenTimeSet(tmpID).BeTime(tmpXH) = AddLitterTime(HourToSecond(RSdata.Fields("起始时间").Value))
    '            GaoFenTimeSet(tmpID).EndTime(tmpXH) = AddLitterTime(HourToSecond(RSdata.Fields("终止时间").Value))
    '            GaoFenTimeSet(tmpID).JGtime(tmpXH) = MinuteToSecond(RSdata.Fields("发车间隔").Value)
    '            GaoFenTimeSet(tmpID).JGOne(tmpXH) = MinuteToSecond(RSdata.Fields("间隔一").Value)
    '            GaoFenTimeSet(tmpID).JGTwo(tmpXH) = MinuteToSecond(RSdata.Fields("间隔二").Value)
    '            GaoFenTimeSet(tmpID).NumOne(tmpXH) = RSdata.Fields("数量一").Value
    '            GaoFenTimeSet(tmpID).NumTwo(tmpXH) = RSdata.Fields("数量二").Value
    '            GaoFenTimeSet(tmpID).ChediNum(tmpXH) = RSdata.Fields("车底数量").Value
    '            GaoFenTimeSet(tmpID).lZhouQiTime(tmpXH) = RSdata.Fields("周期时间").Value
    '            GaoFenTimeSet(tmpID).sRunScaleName(tmpXH) = RSdata.Fields("运行标尺").Value
    '            GaoFenTimeSet(tmpID).sStopScaleName(tmpXH) = RSdata.Fields("停站标尺").Value
    '            GaoFenTimeSet(tmpID).lFirZheFanTime(tmpXH) = RSdata.Fields("始发折返").Value
    '            GaoFenTimeSet(tmpID).lEndZheFanTime(tmpXH) = RSdata.Fields("终到折返").Value
    '            tmpXH = tmpXH + 1

    '            RSdata.MoveNext()
    '        Next i
    '    End If


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

    '    RSChediInfo.Close()
    '    DBChediJiche.Close()
    'End Sub
End Module
