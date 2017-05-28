Public Class frmEditTriainStopTime
    Dim sStopTime As Long  '停站时间
    Dim sCurStation As String '停站车站
    Dim nCurSta As Integer '车站序号
    Dim nCurTrain As Integer
    Dim nCurState As String

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim lTime1 As Long, lTime2 As Long
        Dim nBegintemp As Integer
        Dim nEndtemp As Integer
        Dim lAddTime As Long
        Dim i As Integer
        If Me.optStaStop.Checked = True Then
            nCurSta = 0
            For i = 1 To UBound(TrainInf(nCurTrain).StopStation)
                If StationInf(TrainInf(nCurTrain).nstopSta(i)).sStationName = Trim(Me.cmbSta.Text) Then
                    nCurSta = TrainInf(nCurTrain).nstopSta(i)
                    Exit For
                End If
            Next i
            sStopTime = MinuteToSecond(Me.txtStopTime.Text)
            If nCurSta > 0 Then
                If sStopTime >= 0 Then
                    If nCurSta = 0 Then
                        TrainInf(nCurTrain).NumStop = TrainInf(nCurTrain).NumStop + 1
                        ReDim Preserve TrainInf(nCurTrain).StopStation(UBound(TrainInf(nCurTrain).StopStation) + 1)
                        ReDim Preserve TrainInf(nCurTrain).stopTime(UBound(TrainInf(nCurTrain).stopTime) + 1)
                    Else
                        TrainInf(nCurTrain).stopTime(nCurSta) = sStopTime
                    End If

                    nBegintemp = TrainInf(nCurTrain).nPathID(1)
                    nEndtemp = TrainInf(nCurTrain).nPathID(UBound(TrainInf(nCurTrain).nPathID))

                    lTime1 = TrainInf(nCurTrain).Arrival(nCurSta)
                    lTime2 = TimeAdd(TrainInf(nCurTrain).Arrival(nCurSta), sStopTime)
                    TrainInf(nCurTrain).Starting(nCurSta) = lTime2
                    If nBegintemp > 0 Then
                        Call TxFxDiffDim(UBound(TrainInf), UBound(StationInf))
                        Select Case TimeTablePara.sPubTrainStrainDraw
                            Case TrainStrainDraw.有约束
                                Call TDrawLineOld(lTime1, lTime2, 0, nCurTrain, nCurSta, nEndtemp, 1)
                            Case TrainStrainDraw.无约束
                                Call TDrawLineNoStrainInMetro(lTime1, lTime2, 0, nCurTrain, nCurSta, nEndtemp, 1)
                        End Select
                        Call addOneUndoInf()
                        Call RefreshDiagram(1)
                    End If
                    Me.Close()
                Else
                    MsgBox("时间格式不对，请重新输入!", , "提示")
                    Me.txtStopTime.Select()
                End If
            Else
                MsgBox("请先选择车站名！")
                Exit Sub
            End If

        Else

            If Me.cmbSecName.Text = "请选择区间名称" Then
                MsgBox("没有选择区间，请选择！")
                Exit Sub
            End If
            lAddTime = MinuteToSecond(Me.txtSecTime.Text)
            If lAddTime > 0 Then
                If Me.optMinus.Checked = True Then
                    If MinuteToSecond(Me.txtShiJiTime2.Text) - lAddTime <= 0 Then
                        MsgBox("减少的时间过大，区间运行时间不能为负，请重新输入！")
                        Me.txtSecTime.Text = "0.00"
                        Exit Sub
                    End If
                    lAddTime = -lAddTime
                Else
                    lAddTime = lAddTime
                End If

                For i = 1 To UBound(TrainInf(nCurTrain).nPassSection)
                    If SectionInf(TrainInf(nCurTrain).nPassSection(i)).sSecName = Me.cmbSecName.Text Then
                        nBegintemp = TrainInf(nCurTrain).nSecondID(i)
                        Exit For
                    End If
                Next i
                If nBegintemp > 0 Then
                    nEndtemp = TrainInf(nCurTrain).nPathID(UBound(TrainInf(nCurTrain).nPathID))
                    If nEndtemp = nBegintemp Then
                        lTime1 = TrainInf(nCurTrain).Arrival(nBegintemp)
                        lTime2 = TrainInf(nCurTrain).Starting(nBegintemp)
                        TrainInf(nCurTrain).Arrival(nBegintemp) = TimeAdd(lTime1, lAddTime)
                        TrainInf(nCurTrain).Starting(nBegintemp) = TimeAdd(lTime2, lAddTime)
                    Else
                        lTime1 = TrainInf(nCurTrain).Arrival(nBegintemp)
                        lTime2 = TrainInf(nCurTrain).Starting(nBegintemp)
                        lTime1 = TimeAdd(lTime1, lAddTime)
                        lTime2 = TimeAdd(lTime2, lAddTime)
                        nCurSta = nBegintemp
                        Call TxFxDiffDim(UBound(TrainInf), UBound(StationInf))
                        Select Case TimeTablePara.sPubTrainStrainDraw
                            Case TrainStrainDraw.有约束
                                Call TDrawLineOld(lTime1, lTime2, 0, nCurTrain, nCurSta, nEndtemp, 1)
                            Case TrainStrainDraw.无约束
                                Call TDrawLineNoStrainInMetro(lTime1, lTime2, 0, nCurTrain, nCurSta, nEndtemp, 1)
                        End Select
                    End If
                    Call addOneUndoInf()
                    Call RefreshDiagram(1)
                    Me.Close()
                End If
            End If
        End If
    End Sub

    Private Sub frmEditTriainStopTime_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        nCurTrain = TimeTablePara.nPubTrain
        Me.cmbSta.Items.Clear()
        If UBound(TrainInf(nCurTrain).nPathID) > 0 Then
            For i = 1 To UBound(TrainInf(nCurTrain).nPathID)
                Me.cmbSta.Items.Add(StationInf(TrainInf(nCurTrain).nPathID(i)).sStationName)
            Next i
            Me.cmbSta.Text = "请选择车站名"
        End If
        Me.txtStopTime.Text = "0.00"
        Me.txtShiJiTime.Text = "0.00"

        '区间标尺
        Me.cmbSecName.Items.Clear()
        For i = 1 To UBound(TrainInf(nCurTrain).nPassSection)
            Me.cmbSecName.Items.Add(SectionInf(TrainInf(nCurTrain).nPassSection(i)).sSecName)
        Next i
        Me.cmbSecName.Text = "请选择区间名称"
        Me.txtSecTime.Text = "0.00"
        Me.txtGuDingTime.Text = "0.00"
        Me.txtShiJiTime2.Text = "0.00"

        Me.cmbBiaoChi.Items.Clear()

        Dim j As Integer
        Dim k As Integer
        Dim ifIn As Integer
        Dim sTtempUpOrDown As Integer
        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName = TrainInf(nCurTrain).sJiaoLuName Then
                sTtempUpOrDown = BasicTrainInf(i).nUporDown
                If UBound(BasicTrainInf(i).SecScale) > 0 Then
                    For j = 1 To UBound(BasicTrainInf(i).SecScale)
                        ifIn = 0
                        For k = 1 To Me.cmbBiaoChi.Items.Count
                            If Me.cmbBiaoChi.Items(k - 1) = BasicTrainInf(i).SecScale(j).sName Then
                                ifIn = 1
                                Exit For
                            End If
                        Next
                        If ifIn = 0 Then
                            Me.cmbBiaoChi.Items.Add(BasicTrainInf(i).SecScale(j).sName)
                        End If
                    Next j
                Else
                    MsgBox("该列车交路信息中没有定义运行标尺信息，请检查列车信息！")
                    Exit Sub
                End If
                'If Me.cmbBiaoChi.Items.Count > 0 Then
                '    Me.cmbBiaoChi.Text = Me.cmbBiaoChi.Items(0)
                'End If
                Exit For
            End If
        Next i
    End Sub

    Private Sub cmbSta_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSta.DropDownClosed
        Dim i As Integer
        nCurSta = 0
        For i = 1 To UBound(TrainInf(nCurTrain).StopStation)
            If StationInf(TrainInf(nCurTrain).nstopSta(i)).sStationName = Trim(Me.cmbSta.SelectedItem) Then
                nCurSta = TrainInf(nCurTrain).nstopSta(i)
                Exit For
            End If
        Next i
        Me.txtStopTime.Text = SecondToMinute(TimeMinus(TrainInf(nCurTrain).Starting(nCurSta), TrainInf(nCurTrain).Arrival(nCurSta)))
        Me.txtShiJiTime.Text = Me.txtStopTime.Text
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub cmbSecName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSecName.SelectedIndexChanged
        Dim nSta1, nSta2 As Integer
        Dim i As Integer
        For i = 1 To UBound(TrainInf(nCurTrain).nPassSection)
            If SectionInf(TrainInf(nCurTrain).nPassSection(i)).sSecName = Me.cmbSecName.Text Then
                Me.txtGuDingTime.Text = SecondToMinute(TimeRunByBiaoChiScale(TrainInf(nCurTrain).sJiaoLuName, TrainInf(nCurTrain).sRunScaleName, Me.cmbSecName.Text.Trim))
                nSta1 = TrainInf(nCurTrain).nSecondID(i)
                nSta2 = TrainInf(nCurTrain).nFirstID(i)
                Me.txtShiJiTime2.Text = SecondToMinute(TimeMinus(TrainInf(nCurTrain).Arrival(nSta1), TrainInf(nCurTrain).Starting(nSta2)))
                Exit For
            End If
        Next i
        Me.cmbBiaoChi.Text = TimeRunScaleNameByBiaoChiScale(TrainInf(nCurTrain).sJiaoLuName, TrainInf(nCurTrain).sRunScaleName, Me.cmbSecName.Text.Trim)
    End Sub

    Private Sub optStaStop_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optStaStop.CheckedChanged
        If Me.optStaStop.Checked = True Then
            Me.grpSec.Visible = False
            Me.grpSta.Visible = True
        Else
            Me.grpSec.Visible = True
            Me.grpSta.Visible = False
        End If
    End Sub

    Private Sub optSecTime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSecTime.CheckedChanged
        If Me.optSecTime.Checked = True Then
            Me.grpSec.Visible = True
            Me.grpSta.Visible = False
        Else
            Me.grpSec.Visible = True
            Me.grpSta.Visible = False
        End If
    End Sub

    Private Sub cmbBiaoChi_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBiaoChi.SelectedValueChanged
        Dim nNowTime As Long
        Dim nForetime As Long
        If Me.cmbSecName.Text.Trim = "请选择区间名称" Or Trim(Me.cmbSecName.Text) = "" Then
            MsgBox("请先选择区间名称!", , "提示")
            Me.cmbBiaoChi.Text = ""
            Exit Sub
        End If
        nNowTime = TimeRunByBiaoChiScale(TrainInf(nCurTrain).sJiaoLuName, Me.cmbBiaoChi.Text.Trim, Me.cmbSecName.Text.Trim)
        nForetime = MinuteToSecond(Me.txtShiJiTime2.Text)
        If nNowTime > nForetime Then
            Me.optAdd.Checked = True
            Me.txtSecTime.Text = SecondToMinute(nNowTime - nForetime)
        Else
            Me.optMinus.Checked = True
            Me.txtSecTime.Text = SecondToMinute(nForetime - nNowTime)
        End If
    End Sub
End Class