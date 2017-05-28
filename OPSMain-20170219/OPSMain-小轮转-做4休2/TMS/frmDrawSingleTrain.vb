Public Class frmDrawSingleTrain
    Public IfNotDrawLine As Integer
    Public sTtempUpOrDown As String
    Public sCurFormState As String
    Public nBeforTrainNum As Integer
    Public nBeforTrainNums() As Integer

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click

        Dim sJiaoLuName As String
        Dim sRunScaleName As String
        Dim sStopScaleName As String
        Dim tmpTime As Long

        Dim nTrain As Integer
        Dim sSta As String
        Dim Cdid As Integer
        Dim tmpZFTime As Long
        Dim tmpZFTime2 As Long
        Dim sFirTrain As Integer
        Dim tmpRunTime As Long
        Dim nCdid As Integer
        Dim nNewRunTime As Long

        Dim nRunTime As Long
        Dim nBefore As Integer
        Dim nAfter As Integer
        Dim nBeTime As Integer
        Dim nAfTime As Integer
        Dim nZfTime1 As Integer
        Dim nZfTime2 As Integer
        Dim i As Integer
        Dim k As Integer
        Dim nCurTrain As Integer
        sJiaoLuName = Trim(Me.cmbJiaoLuName.Text)
        If sJiaoLuName = "" Then
            MsgBox("交路名称不能为空，请选择！")
            Exit Sub
        End If
        sRunScaleName = Trim(Me.cmbRunBiaoChi.Text)
        If sRunScaleName = "" Then
            MsgBox("运行标尺名称不能为空，请选择！")
            Exit Sub
        End If

        sStopScaleName = Trim(Me.cmbStopBiaoChi.Text)
        If sStopScaleName = "" Then
            MsgBox("停站标尺名称不能为空，请选择！")
            Exit Sub
        End If
        Dim nNewTrain As Integer
        ' Dim nChediID As Integer
        Select Case sCurFormState
            Case "新增列车"
                tmpTime = HourToSecond(Trim(Me.txtStartTime.Text))
                If tmpTime > 0 And tmpTime <= 24 * 3600.0# Then
                    nNewTrain = FaAddNewTrainInf(sJiaoLuName, sRunScaleName, sStopScaleName, "", tmpTime)
                    Call AddTrainToNewCheDiInfo(nNewTrain)
                    If TimeTablePara.BifAutoBianCheCi = True Then
                        Call ResetPrintTrainString()
                    End If
                Else
                    MsgBox("时间输入错误，请重输！")
                End If

            Case "在车底后加一列车"

                Cdid = CheCiToCheDiID(nBeforTrainNum)
                If Cdid > 0 Then
                    sFirTrain = ChediInfo(Cdid).nLinkTrain(UBound(ChediInfo(Cdid).nLinkTrain))
                Else
                    Exit Sub
                End If

                sSta = TrainInf(sFirTrain).NextStation
                tmpZFTime = GetZheFanTime(TrainInf(sFirTrain).SCheDiLeiXing, sSta, TrainInf(sFirTrain).TrainReturnStyle(2))
                tmpTime = TimeAdd(TrainInf(sFirTrain).Arrival(TrainInf(sFirTrain).nPathID(UBound(TrainInf(sFirTrain).nPathID))), tmpZFTime)

                nTrain = AddTrainInformation(sJiaoLuName, sRunScaleName, sStopScaleName, "")

                If nTrain > 0 Then
                    If (nTrain + nBeforTrainNum) Mod 2 = 0 Then
                        tmpZFTime2 = GetZheFanTime(TrainInf(nBeforTrainNum).SCheDiLeiXing, sSta, "立即折返")
                    Else
                        tmpZFTime2 = GetZheFanTime(TrainInf(nBeforTrainNum).SCheDiLeiXing, sSta, TrainInf(nBeforTrainNum).TrainReturnStyle(2))
                    End If
                    tmpTime = TimeMinus(tmpTime, tmpZFTime - tmpZFTime2)

                    Call DrawSingleTrain(nTrain, tmpTime, 0)
                    Call AddTrainInCheDiLink(Cdid, nTrain, 2)

                    If TimeTablePara.BifAutoBianCheCi = True Then
                        Call ResetPrintTrainString()
                    End If

                Else
                    MsgBox("该列车交路不存在！")
                End If


            Case "在车底前加一列车"

                Cdid = CheCiToCheDiID(nBeforTrainNum)
                If Cdid > 0 Then
                    sFirTrain = ChediInfo(Cdid).nLinkTrain(1)
                Else
                    Exit Sub
                End If

                sSta = TrainInf(sFirTrain).ComeStation
                tmpZFTime = GetZheFanTime(TrainInf(sFirTrain).SCheDiLeiXing, sSta, TrainInf(sFirTrain).TrainReturnStyle(1))
                Cdid = CheCiToCheDiID(sFirTrain)
                tmpTime = TimeMinus(TrainInf(sFirTrain).Starting(TrainInf(sFirTrain).nPathID(1)), tmpZFTime)


                nTrain = AddTrainInformation(sJiaoLuName, sRunScaleName, sStopScaleName, "")
                If nTrain > 0 Then
                    If (nTrain + nBeforTrainNum) Mod 2 = 0 Then
                        tmpZFTime2 = GetZheFanTime(TrainInf(nBeforTrainNum).SCheDiLeiXing, sSta, "立即折返")
                        tmpTime = TimeAdd(tmpTime, tmpZFTime - tmpZFTime2)
                    Else
                        'tmpZFTime2 = GetZheFanTime(TrainInf(nBeforTrainNum).SCheDiLeiXing, sSta, TrainInf(nBeforTrainNum).TrainReturnStyle(1))
                    End If
                    tmpRunTime = CalTrainRunTimeNotStopFromTrain(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).sStopSclaeName, 0)
                    tmpTime = TimeMinus(tmpTime, tmpRunTime)


                    Call DrawSingleTrain(nTrain, tmpTime, 0)

                    Call AddTrainInCheDiLink(Cdid, nTrain, 1)
                    If TimeTablePara.BifAutoBianCheCi = True Then
                        Call ResetPrintTrainString()
                    End If

                Else
                    MsgBox("该列车交路不存在！")
                End If

            Case "修改标尺"

                For k = 1 To UBound(nBeforTrainNums)
                    nCurTrain = nBeforTrainNums(k)
                    nCdid = CheCiToCheDiID(nCurTrain)
                    For i = 1 To UBound(ChediInfo(nCdid).nLinkTrain)
                        If ChediInfo(nCdid).nLinkTrain(i) = nCurTrain Then
                            If i = 1 And i = UBound(ChediInfo(nCdid).nLinkTrain) Then
                                nBefore = 0
                                nAfter = 0
                            ElseIf i = 1 And i < UBound(ChediInfo(nCdid).nLinkTrain) Then
                                nBefore = 0
                                nAfter = ChediInfo(nCdid).nLinkTrain(i + 1)
                            ElseIf i = UBound(ChediInfo(nCdid).nLinkTrain) And i > 1 Then
                                nAfter = 0
                                nBefore = ChediInfo(nCdid).nLinkTrain(i - 1)
                            Else
                                nBefore = ChediInfo(nCdid).nLinkTrain(i - 1)
                                nAfter = ChediInfo(nCdid).nLinkTrain(i + 1)
                            End If
                        End If
                    Next i

                    nRunTime = CalTrainRunTimeNotStopFromTrain(TrainInf(nCurTrain).sJiaoLuName, TrainInf(nCurTrain).sRunScaleName, TrainInf(nCurTrain).sStopSclaeName, 0)
                    nNewRunTime = CalTrainRunTimeNotStopFromTrain(sJiaoLuName, sRunScaleName, sStopScaleName, 0)

                    If nNewRunTime <= nRunTime Then
                        TrainInf(nCurTrain).sRunScaleName = sRunScaleName
                        TrainInf(nCurTrain).sStopSclaeName = sStopScaleName
                        Call ResetTrainStartOrEndTime(nCurTrain)

                    Else
                        If nBefore = 0 And nAfter = 0 Then '修改到点
                            TrainInf(nCurTrain).sRunScaleName = sRunScaleName
                            TrainInf(nCurTrain).sStopSclaeName = sStopScaleName
                            Call ResetTrainStartOrEndTime(nCurTrain)
                        ElseIf nBefore = 0 And nAfter <> 0 Then '修改发点
                            TrainInf(nCurTrain).sRunScaleName = sRunScaleName
                            TrainInf(nCurTrain).sStopSclaeName = sStopScaleName
                            TrainInf(nCurTrain).Starting(TrainInf(nCurTrain).nPathID(1)) = TimeMinus(TrainInf(nCurTrain).lAllEndTime, nNewRunTime)
                            Call ResetTrainStartOrEndTime(nCurTrain)
                        ElseIf nBefore <> 0 And nAfter = 0 Then '修改到点
                            TrainInf(nCurTrain).sTrainTimeScale = StrInputBoxCombText
                            TrainInf(nCurTrain).sRunScaleName = sRunScaleName
                            TrainInf(nCurTrain).sStopSclaeName = sStopScaleName
                            TrainInf(nCurTrain).lAllEndTime = TimeAdd(TrainInf(nCurTrain).lAllStartTime, nNewRunTime)
                            Call ResetTrainStartOrEndTime(nCurTrain)
                        Else
                            nBeTime = AddLitterTime(TrainInf(nCurTrain).lAllStartTime) - AddLitterTime(TrainInf(nBefore).lAllEndTime)
                            nAfTime = AddLitterTime(TrainInf(nAfter).lAllStartTime) - AddLitterTime(TrainInf(nCurTrain).lAllEndTime)
                            nZfTime1 = GetZheFanTime(TrainInf(nCurTrain).SCheDiLeiXing, StationInf(TrainInf(nBeforTrainNum).nPathID(1)).sStationName, TrainInf(nCurTrain).TrainReturnStyle(1))
                            nZfTime2 = GetZheFanTime(TrainInf(nCurTrain).SCheDiLeiXing, StationInf(TrainInf(nAfter).nPathID(1)).sStationName, TrainInf(nCurTrain).TrainReturnStyle(2))
                            If nBeTime + nAfTime - nZfTime2 - nZfTime2 >= nNewRunTime - nRunTime Then
                                If nAfTime - nZfTime2 >= nNewRunTime - nRunTime Then '修改到点
                                    TrainInf(nCurTrain).sTrainTimeScale = StrInputBoxCombText
                                    TrainInf(nCurTrain).sRunScaleName = sRunScaleName
                                    TrainInf(nCurTrain).sStopSclaeName = sStopScaleName
                                    TrainInf(nCurTrain).lAllEndTime = TimeAdd(TrainInf(nCurTrain).lAllStartTime, nNewRunTime)
                                    Call ResetTrainStartOrEndTime(nBeforTrainNum)
                                Else '修改到发点，先满足到点
                                    TrainInf(nCurTrain).sTrainTimeScale = StrInputBoxCombText
                                    TrainInf(nCurTrain).sRunScaleName = sRunScaleName
                                    TrainInf(nCurTrain).sStopSclaeName = sStopScaleName
                                    TrainInf(nCurTrain).lAllEndTime = TimeMinus(TrainInf(nAfter).lAllStartTime, nZfTime2)
                                    TrainInf(nCurTrain).lAllStartTime = TimeMinus(TrainInf(nCurTrain).lAllEndTime, nNewRunTime)
                                    Call ResetTrainStartOrEndTime(nCurTrain)
                                End If
                            Else
                                MsgBox("该列车的前后折返时间太紧凑，无法修改至更慢的时分标尺，请先调整前后列车！")
                                Exit Sub
                            End If
                        End If
                    End If
                Next
        End Select

        IfNotDrawLine = 1
        Me.Close()
    End Sub
    

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    Private Sub frmDrawSingleTrain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If sCurFormState <> "新增列车" Then
            nBeforTrainNum = nBeforTrainNums(1)
        End If
        Dim i, p As Integer
        Dim sForeJLName As String
        Dim sForeEndSta As String
        Dim sFanJiaoLu As String
        Dim nStaLen As Integer
        Me.cmbJiaoLuName.Items.Clear()
        Select Case sCurFormState
            Case "新增列车"
                Me.txtStartTime.Text = SecondToHour(TimeTablePara.lngCurMouseDownTime, 0)
                If UBound(BasicTrainInf) > 0 Then
                    For i = 1 To UBound(BasicTrainInf)
                        For p = 1 To UBound(StationInf)
                            If BasicTrainInf(i).ComeStation = StationInf(p).sStationName Then
                                If BasicTrainInf(i).sJiaoLuName <> "" Then
                                    Me.cmbJiaoLuName.Items.Add(BasicTrainInf(i).sJiaoLuName)
                                End If
                                Exit For
                            End If
                        Next p
                    Next i
                    cmbJiaoLuName.Text = Me.cmbJiaoLuName.Items(0)
                Else
                    MsgBox("列车信息中没有交路信息，请检查列车信息！")
                    Exit Sub
                End If
                Call ListElseInf()

            Case "在车底后加一列车"

                sForeJLName = TrainInf(nBeforTrainNum).sJiaoLuName
                sForeEndSta = GetSystemStaName(TrainInf(nBeforTrainNum).NextStation)
                nStaLen = Len(sForeEndSta)
                If UBound(BasicTrainInf) > 0 Then
                    'If TrainInf(nBeforTrainNum).TrainStyle = "环形车" Then
                    '    For i = 1 To UBound(BasicTrainInf)
                    '        If BasicTrainInf(i).TrainStyle = "环形车" Then
                    '            If BasicTrainInf(i).NextStation = TrainInf(nBeforTrainNum).NextStation Then
                    '                Me.cmbJiaoLuName.Items.Add(BasicTrainInf(i).sJiaoLuName)
                    '            End If
                    '        End If
                    '    Next i
                    'End If

                    For i = 1 To UBound(BasicTrainInf)
                        For p = 1 To UBound(StationInf)
                            If BasicTrainInf(i).ComeStation = StationInf(p).sStationName Then
                                'If BasicTrainInf(i).ComeStation = "宜山路四" Then Stop
                                If BasicTrainInf(i).sJiaoLuName <> "" And GetSystemStaName(BasicTrainInf(i).ComeStation) = sForeEndSta Then
                                    Me.cmbJiaoLuName.Items.Add(BasicTrainInf(i).sJiaoLuName)
                                End If
                                Exit For
                            End If
                        Next p
                    Next i
                    If TrainInf(nBeforTrainNum).TrainStyle = "环形车" Then
                        Me.cmbJiaoLuName.Text = sForeJLName
                    Else
                        sFanJiaoLu = GetReturnJiaoLuName(sForeJLName)
                        For i = 1 To Me.cmbJiaoLuName.Items.Count
                            If Me.cmbJiaoLuName.Items(i - 1) = sFanJiaoLu Then
                                Me.cmbJiaoLuName.Text = sFanJiaoLu
                            End If
                        Next
                    End If
                Else
                    MsgBox("列车信息中没有交路信息，请检查列车信息！")
                    Exit Sub
                End If
                Call ListElseInf()
                Dim Cdid As Integer
                Dim sFirTrain As Integer
                Cdid = CheCiToCheDiID(nBeforTrainNum)
                If Cdid > 0 Then
                    sFirTrain = ChediInfo(Cdid).nLinkTrain(UBound(ChediInfo(Cdid).nLinkTrain))
                    Me.cmbRunBiaoChi.Text = TrainInf(sFirTrain).sRunScaleName
                    Me.cmbStopBiaoChi.Text = TrainInf(sFirTrain).sStopSclaeName
                End If
                Me.txtStartTime.Visible = False
                Me.Label4.Visible = False


            Case "在车底前加一列车"
                sForeJLName = TrainInf(nBeforTrainNum).sJiaoLuName
                sForeEndSta = GetSystemStaName(TrainInf(nBeforTrainNum).ComeStation)
                nStaLen = Len(sForeEndSta)
                If UBound(BasicTrainInf) > 0 Then
                    'If TrainInf(nBeforTrainNum).TrainStyle = "环形车" Then
                    '    For i = 1 To UBound(BasicTrainInf)
                    '        If BasicTrainInf(i).TrainStyle = "环形车" Then
                    '            If BasicTrainInf(i).NextStation = TrainInf(nBeforTrainNum).NextStation Then
                    '                Me.cmbJiaoLuName.Items.Add(BasicTrainInf(i).sJiaoLuName)
                    '            End If
                    '        End If
                    '    Next i
                    'End If

                    For i = 1 To UBound(BasicTrainInf)
                        For p = 1 To UBound(StationInf)
                            If BasicTrainInf(i).ComeStation = StationInf(p).sStationName Then
                                If BasicTrainInf(i).sJiaoLuName <> "" And GetSystemStaName(BasicTrainInf(i).NextStation) = sForeEndSta Then
                                    Me.cmbJiaoLuName.Items.Add(BasicTrainInf(i).sJiaoLuName)
                                End If
                                Exit For
                            End If
                        Next p

                    Next i
                    If TrainInf(nBeforTrainNum).TrainStyle = "环形车" Then
                        Me.cmbJiaoLuName.Text = sForeJLName
                    Else
                        sFanJiaoLu = GetReturnJiaoLuName(sForeJLName)
                        For i = 1 To Me.cmbJiaoLuName.Items.Count
                            If Me.cmbJiaoLuName.Items(i - 1) = sFanJiaoLu Then
                                Me.cmbJiaoLuName.Text = sFanJiaoLu
                            End If
                        Next

                    End If
                    'If Me.cmbJiaoLuName.Items.Count > 0 Then
                    '    If Me.cmbJiaoLuName.Text <> "" Then
                    '        Me.cmbJiaoLuName.Text = Me.cmbJiaoLuName.Items(0)
                    '    End If
                    'End If

                Else
                    MsgBox("列车信息中没有交路信息，请检查列车信息！")
                    Exit Sub
                End If
                Call ListElseInf()

                Me.txtStartTime.Visible = False
                Me.Label4.Visible = False


            Case "修改标尺"

                cmbJiaoLuName.Text = TrainInf(nBeforTrainNum).sJiaoLuName
                Call ListElseInf()

                Me.cmbRunBiaoChi.Text = TrainInf(nBeforTrainNum).sRunScaleName
                Me.cmbStopBiaoChi.Text = TrainInf(nBeforTrainNum).sStopSclaeName

                Me.txtStartTime.Visible = False
                Me.Label4.Visible = False

        End Select
        IfNotDrawLine = 0
    End Sub
    Private Sub ListElseInf()
        Me.cmbRunBiaoChi.Items.Clear()
        Me.cmbStopBiaoChi.Items.Clear()
        Dim sTtempUpOrDown As Integer
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim ifIn As Integer
        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName = Me.cmbJiaoLuName.Text Then
                sTtempUpOrDown = BasicTrainInf(i).nUporDown
                If UBound(BasicTrainInf(i).SecScale) > 0 Then
                    For j = 1 To UBound(BasicTrainInf(i).SecScale)
                        ifIn = 0
                        For k = 1 To Me.cmbRunBiaoChi.Items.Count
                            If Me.cmbRunBiaoChi.Items(k - 1) = BasicTrainInf(i).SecScale(j).sName Then
                                ifIn = 1
                                Exit For
                            End If
                        Next
                        If ifIn = 0 Then
                            Me.cmbRunBiaoChi.Items.Add(BasicTrainInf(i).SecScale(j).sName)
                        End If
                    Next j
                Else
                    MsgBox("该列车交路信息中没有定义运行标尺信息，请检查列车信息！")
                    Exit Sub
                End If
                If Me.cmbRunBiaoChi.Items.Count > 0 Then
                    Me.cmbRunBiaoChi.Text = Me.cmbRunBiaoChi.Items(0)
                End If

                If UBound(BasicTrainInf(i).StopScale) > 0 Then

                    For j = 1 To UBound(BasicTrainInf(i).StopScale)
                        ifIn = 0
                        For k = 1 To Me.cmbStopBiaoChi.Items.Count
                            If Me.cmbStopBiaoChi.Items(k - 1) = BasicTrainInf(i).StopScale(j).sName Then
                                ifIn = 1
                                Exit For
                            End If
                        Next
                        If ifIn = 0 Then
                            Me.cmbStopBiaoChi.Items.Add(BasicTrainInf(i).StopScale(j).sName)
                        End If
                    Next j
                Else
                    MsgBox("该列车交路信息中没有定义停站标尺信息，请检查列车信息！")
                    Exit Sub
                End If

                If Me.cmbStopBiaoChi.Items.Count > 0 Then
                    Me.cmbStopBiaoChi.Text = Me.cmbStopBiaoChi.Items(0)
                End If

                Exit For
            End If
        Next i
    End Sub

    Private Sub cmbJiaoLuName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbJiaoLuName.SelectedIndexChanged
        Call ListElseInf()
    End Sub
End Class