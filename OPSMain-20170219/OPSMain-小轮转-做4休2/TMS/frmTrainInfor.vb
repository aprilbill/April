Public Class frmTrainInfor

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Private Sub frmTrainInfor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i, j As Integer
        Dim nToStopTime As Long
        Dim nToSecTime As Long
        nToStopTime = 0
        nToSecTime = 0
        Dim curStopTime As Long
        Dim curRunTime As Long
        Dim sngToLength As Single
        Dim CurLength As Single
        Dim sSpeed As Single
        sngToLength = 0

        '基本信息
        Me.dataGrid.RowCount = 13
        Me.dataGrid.Rows(0).Height = 20
        Me.dataGrid.Rows(0).Cells(0).Value = "列车ID"
        Me.dataGrid.Rows(0).Cells(1).Value = TimeTablePara.nPubTrain
        Me.dataGrid.Rows(1).Cells(0).Value = "列车车次"
        Me.dataGrid.Rows(1).Cells(1).Value = TrainInf(TimeTablePara.nPubTrain).Train
        Me.dataGrid.Rows(2).Cells(0).Value = "输出车次"
        Me.dataGrid.Rows(2).Cells(1).Value = TrainInf(TimeTablePara.nPubTrain).sPrintTrain
        Me.dataGrid.Rows(3).Cells(0).Value = "列车交路名"
        Me.dataGrid.Rows(3).Cells(1).Value = TrainInf(TimeTablePara.nPubTrain).sJiaoLuName
        Me.dataGrid.Rows(4).Cells(0).Value = "线路编号"
        Me.dataGrid.Rows(4).Cells(1).Value = TrainInf(TimeTablePara.nPubTrain).sLineNum
        Me.dataGrid.Rows(5).Cells(0).Value = "交路编号(目的符)"
        Me.dataGrid.Rows(5).Cells(1).Value = TrainInf(TimeTablePara.nPubTrain).sMuDiNum
        Me.dataGrid.Rows(6).Cells(0).Value = "车底类型"
        Me.dataGrid.Rows(6).Cells(1).Value = TrainInf(TimeTablePara.nPubTrain).SCheDiLeiXing
        Me.dataGrid.Rows(7).Cells(0).Value = "运行标尺"
        Me.dataGrid.Rows(7).Cells(1).Value = TrainInf(TimeTablePara.nPubTrain).sRunScaleName
        Me.dataGrid.Rows(8).Cells(0).Value = "停站标尺"
        Me.dataGrid.Rows(8).Cells(1).Value = TrainInf(TimeTablePara.nPubTrain).sStopSclaeName
        Me.dataGrid.Rows(9).Cells(0).Value = "列车类型"
        Me.dataGrid.Rows(9).Cells(1).Value = TrainInf(TimeTablePara.nPubTrain).TrainStyle
        Me.dataGrid.Rows(10).Cells(0).Value = "列车性质"
        Me.dataGrid.Rows(10).Cells(1).Value = TrainInf(TimeTablePara.nPubTrain).sTrainXingZhi
        Me.dataGrid.Rows(11).Cells(0).Value = "始发折返方式"
        Me.dataGrid.Rows(11).Cells(1).Value = TrainInf(TimeTablePara.nPubTrain).TrainReturnStyle(1)
        Me.dataGrid.Rows(12).Cells(0).Value = "终到折返方式"
        Me.dataGrid.Rows(12).Cells(1).Value = TrainInf(TimeTablePara.nPubTrain).TrainReturnStyle(2)

        '时刻表信息
        Dim stmpSta() As String
        ReDim stmpSta(0)
        Dim ifIn As Integer

        'Me.DataGridTime.RowCount = UBound(TrainInf(TimeTablePara.npubtrain).nPathID)
        For i = 1 To UBound(TrainInf(TimeTablePara.nPubTrain).nPathID)
            ifIn = 0
            For j = 1 To UBound(stmpSta)
                If stmpSta(j) = StationInf(TrainInf(TimeTablePara.nPubTrain).nPathID(i)).sStationName Then
                    ifIn = 1
                    Exit For
                End If
            Next
            If ifIn = 0 Then '不同名
                Me.DataGridTime.Rows.Add()
                Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(0).Value = Me.DataGridTime.Rows.Count
                Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(1).Value = StationInf(TrainInf(TimeTablePara.nPubTrain).nPathID(i)).sStationName
                Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(2).Value = SecondToHour(TrainInf(TimeTablePara.nPubTrain).Arrival(TrainInf(TimeTablePara.nPubTrain).nPathID(i)), 0)
                Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(3).Value = SecondToHour(TrainInf(TimeTablePara.nPubTrain).Starting(TrainInf(TimeTablePara.nPubTrain).nPathID(i)), 0)
                curStopTime = TimeMunusTime(TrainInf(TimeTablePara.nPubTrain).Starting(TrainInf(TimeTablePara.nPubTrain).nPathID(i)), TrainInf(TimeTablePara.nPubTrain).Arrival(TrainInf(TimeTablePara.nPubTrain).nPathID(i)))
                Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(4).Value = SecondToMinute(curStopTime)
                Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(5).Value = TrainInf(TimeTablePara.nPubTrain).StopLine(TrainInf(TimeTablePara.nPubTrain).nPathID(i))
                Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(6).Value = SecondToMinute(GetCurTrainStopTimeAtStation(TrainInf(TimeTablePara.nPubTrain).sJiaoLuName, TrainInf(TimeTablePara.nPubTrain).sStopSclaeName, StationInf(TrainInf(TimeTablePara.nPubTrain).nPathID(i)).sStationName))
                Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(7).Value = GetCurTrainStopScaleAtStation(TrainInf(TimeTablePara.nPubTrain).sJiaoLuName, TrainInf(TimeTablePara.nPubTrain).sStopSclaeName, StationInf(TrainInf(TimeTablePara.nPubTrain).nPathID(i)).sStationName)
                If Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(6).Value.ToString.Trim <> Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(4).Value.ToString.Trim Then
                    Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(6).Style.ForeColor = Color.Red
                    Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(4).Style.ForeColor = Color.Red
                End If
                If i > 1 And i < UBound(TrainInf(TimeTablePara.nPubTrain).nPathID) Then
                    nToStopTime = nToStopTime + curStopTime
                End If
                ReDim Preserve stmpSta(UBound(stmpSta) + 1)
                stmpSta(UBound(stmpSta)) = StationInf(TrainInf(TimeTablePara.nPubTrain).nPathID(i)).sStationName
            End If
        Next

        '始发折返信息
        Me.DataGridTime.Rows.Add()
        Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(0).Value = Me.DataGridTime.Rows.Count
        Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(1).Value = "始发折返"
        Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(2).Value = SecondToHour(TrainInf(TimeTablePara.nPubTrain).sStartZFArrival, 0)
        Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(3).Value = SecondToHour(TrainInf(TimeTablePara.nPubTrain).sStartZFStarting, 0)
        curStopTime = TimeMunusTime(TrainInf(TimeTablePara.nPubTrain).sStartZFStarting, TrainInf(TimeTablePara.nPubTrain).sStartZFArrival)
        Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(4).Value = SecondToMinute(curStopTime)
        Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(5).Value = TrainInf(TimeTablePara.nPubTrain).sStartZFLine

        '终到折返信息
        Me.DataGridTime.Rows.Add()
        Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(0).Value = Me.DataGridTime.Rows.Count
        Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(1).Value = "终到折返"
        Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(2).Value = SecondToHour(TrainInf(TimeTablePara.nPubTrain).sEndZFArrival, 0)
        Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(3).Value = SecondToHour(TrainInf(TimeTablePara.nPubTrain).sEndZFStarting, 0)
        curStopTime = TimeMunusTime(TrainInf(TimeTablePara.nPubTrain).sEndZFStarting, TrainInf(TimeTablePara.nPubTrain).sEndZFArrival)
        Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(4).Value = SecondToMinute(curStopTime)
        Me.DataGridTime.Rows(Me.DataGridTime.Rows.Count - 1).Cells(5).Value = TrainInf(TimeTablePara.nPubTrain).sEndZFLine


        '区间时分
        Dim sTime As String
        Me.DataGridSectime.RowCount = UBound(TrainInf(TimeTablePara.nPubTrain).nPassSection)
        For i = 1 To UBound(TrainInf(TimeTablePara.nPubTrain).nPassSection)
            Me.DataGridSectime.Rows(i - 1).Cells(0).Value = i
            Me.DataGridSectime.Rows(i - 1).Cells(1).Value = StationInf(TrainInf(TimeTablePara.nPubTrain).nFirstID(i)).sStationName & "—>" & StationInf(TrainInf(TimeTablePara.nPubTrain).nSecondID(i)).sStationName
            curRunTime = TimeMunusTime(TrainInf(TimeTablePara.nPubTrain).Arrival(TrainInf(TimeTablePara.nPubTrain).nSecondID(i)), TrainInf(TimeTablePara.nPubTrain).Starting(TrainInf(TimeTablePara.nPubTrain).nFirstID(i)))
            If TimeTablePara.nPubTrain Mod 2 = 0 Then
                If TrainInf(TimeTablePara.nPubTrain).nPassSection(i) > 0 Then
                    CurLength = SectionInf(TrainInf(TimeTablePara.nPubTrain).nPassSection(i)).lDistance(2)
                End If
                '  Me.DataGridSectime.Rows(i - 1).Cells(3).Value = SecondToMinute(SectionInf(i).SecScale(TrainInf(TimeTablePara.npubtrain).nTrainTimeKind).sng)
            Else
                If TrainInf(TimeTablePara.nPubTrain).nPassSection(i) > 0 Then
                    CurLength = SectionInf(TrainInf(TimeTablePara.nPubTrain).nPassSection(i)).lDistance(1)
                End If
            End If
            Me.DataGridSectime.Rows(i - 1).Cells(2).Value = SecondToMinute(curRunTime)

            sTime = SecondToMinute(TimeRunByBiaoChiScale(TrainInf(TimeTablePara.nPubTrain).sJiaoLuName, TrainInf(TimeTablePara.nPubTrain).sRunScaleName, SectionInf(TrainInf(TimeTablePara.nPubTrain).nPassSection(i)).sSecName)).Trim
            Me.DataGridSectime.Rows(i - 1).Cells(3).Value = sTime
            'If sTime <> Me.DataGridSectime.Rows(i - 1).Cells(2).Value.ToString.Trim Then
            '    Me.DataGridSectime.Rows(i - 1).Cells(2).Style.ForeColor = Color.Red
            '    Me.DataGridSectime.Rows(i - 1).Cells(3).Style.ForeColor = Color.Red
            'End If
            Me.DataGridSectime.Rows(i - 1).Cells(4).Value = TimeRunScaleNameByBiaoChiScale(TrainInf(TimeTablePara.nPubTrain).sJiaoLuName, TrainInf(TimeTablePara.nPubTrain).sRunScaleName, SectionInf(TrainInf(TimeTablePara.nPubTrain).nPassSection(i)).sSecName)
            nToSecTime = nToSecTime + curRunTime
            sngToLength = sngToLength + CurLength
        Next

        '指标
        Me.DataGridIndex.RowCount = 10
        Me.DataGridIndex.Rows(0).Cells(0).Value = "纯运行时分"
        Me.DataGridIndex.Rows(0).Cells(1).Value = SecondToMinute(nToSecTime) & "  (分.秒)"
        Me.DataGridIndex.Rows(1).Cells(0).Value = "停站时分"
        Me.DataGridIndex.Rows(1).Cells(1).Value = SecondToMinute(nToStopTime) & "  (分.秒)"
        Me.DataGridIndex.Rows(2).Cells(0).Value = "总运行时分"
        Me.DataGridIndex.Rows(2).Cells(1).Value = SecondToMinute(nToSecTime + nToStopTime) & "  (分.秒)"
        Me.DataGridIndex.Rows(3).Cells(0).Value = "运行距离"
        Me.DataGridIndex.Rows(3).Cells(1).Value = sngToLength & "  (公里)"
        Me.DataGridIndex.Rows(4).Cells(0).Value = "旅行速度"
        sSpeed = (sngToLength / ((nToSecTime + nToStopTime) / 3600))
        Me.DataGridIndex.Rows(4).Cells(1).Value = Format(sSpeed, "#0.00") & "  (公里/小时)"
        Me.DataGridIndex.Rows(5).Cells(0).Value = "技术速度"
        sSpeed = sngToLength / (nToSecTime / 3600)
        Me.DataGridIndex.Rows(5).Cells(1).Value = Format(sSpeed, "#0.00") & "  (公里/小时)"

        Me.DataGridIndex.Rows(6).Cells(0).Value = "前面接续列车"
        If TrainInf(TimeTablePara.nPubTrain).TrainReturn(1) = 0 Then
            Me.DataGridIndex.Rows(6).Cells(1).Value = "无"
        Else
            Me.DataGridIndex.Rows(6).Cells(1).Value = TrainInf(TimeTablePara.nPubTrain).TrainReturn(1) & "(" & TrainInf(TrainInf(TimeTablePara.nPubTrain).TrainReturn(1)).Train & ")"
        End If
        Me.DataGridIndex.Rows(7).Cells(0).Value = "后面接续列车"
        If TrainInf(TimeTablePara.nPubTrain).TrainReturn(2) = 0 Then
            Me.DataGridIndex.Rows(7).Cells(1).Value = "无"
        Else
            Me.DataGridIndex.Rows(7).Cells(1).Value = TrainInf(TimeTablePara.nPubTrain).TrainReturn(2) & "(" & TrainInf(TrainInf(TimeTablePara.nPubTrain).TrainReturn(2)).Train & ")"
        End If

        Me.DataGridIndex.Rows(8).Cells(0).Value = "前面折返时间"
        If TrainInf(TimeTablePara.nPubTrain).TrainReturn(1) = 0 Then
            Me.DataGridIndex.Rows(8).Cells(1).Value = "无"
        Else
            Me.DataGridIndex.Rows(8).Cells(1).Value = SecondToMinute(TimeMunusTime(TrainInf(TimeTablePara.nPubTrain).Starting(TrainInf(TimeTablePara.nPubTrain).nPathID(1)), TrainInf(TrainInf(TimeTablePara.nPubTrain).TrainReturn(1)).Arrival(TrainInf(TrainInf(TimeTablePara.nPubTrain).TrainReturn(1)).nPathID(UBound(TrainInf(TrainInf(TimeTablePara.nPubTrain).TrainReturn(1)).nPathID))))) & "  (分.秒)"
        End If
        Me.DataGridIndex.Rows(9).Cells(0).Value = "后面折返时间"
        If TrainInf(TimeTablePara.nPubTrain).TrainReturn(2) = 0 Then
            Me.DataGridIndex.Rows(9).Cells(1).Value = "无"
        Else
            Me.DataGridIndex.Rows(9).Cells(1).Value = SecondToMinute(TimeMunusTime(TrainInf(TrainInf(TimeTablePara.nPubTrain).TrainReturn(2)).Starting(TrainInf(TrainInf(TimeTablePara.nPubTrain).TrainReturn(2)).nPathID(1)), TrainInf(TimeTablePara.nPubTrain).Arrival(TrainInf(TimeTablePara.nPubTrain).nPathID(UBound(TrainInf(TimeTablePara.nPubTrain).nPathID))))) & "  (分.秒)"
        End If

        Me.cmbLineStyle.Items.Clear()
        Me.cmbLineStyle.Items.Add("实线 ─────────")
        Me.cmbLineStyle.Items.Add("长虚线— — — — — —")
        Me.cmbLineStyle.Items.Add("点虚线-----------------")
        Me.cmbLineStyle.Items.Add("点划线— - — - — - —")
        Me.cmbLineStyle.Items.Add("双点划线— -- — -- — ")
        Me.cmbLineStyle.Text = "实线 ─────────"

        If TrainInf(TimeTablePara.nPubTrain).nZfLimit = 0 Then
            Me.chkZheFanYueShu.Checked = False
        Else
            Me.chkZheFanYueShu.Checked = True
        End If

        Me.cmbLineStyle.Text = GetLineStyleNameFromText(GetLineStyleTextFromStyleName(TrainInf(TimeTablePara.nPubTrain).PrintLineStyle))
        Me.numLineWidth.Value = TrainInf(TimeTablePara.nPubTrain).PrintLineWidth
        Me.labTimeLineColor.BackColor = TrainInf(TimeTablePara.nPubTrain).PrintLineColor
    End Sub

    Private Sub chkZheFanYueShu_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkZheFanYueShu.CheckedChanged
        If TimeTablePara.nPubTrain > 0 Then
            If Me.chkZheFanYueShu.Checked = True Then
                TrainInf(TimeTablePara.nPubTrain).nZfLimit = 1
            Else
                TrainInf(TimeTablePara.nPubTrain).nZfLimit = 0
            End If
        End If
    End Sub

    Private Sub btnTimeLineColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTimeLineColor.Click
        Dim dColor As New ColorDialog
        dColor.Color = Me.labTimeLineColor.BackColor
        If dColor.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.labTimeLineColor.BackColor = dColor.Color
            Call ShowTimeLinePrint()
            Call InputLineStyleInfor()
        End If
    End Sub

    Private Sub ShowTimeLinePrint()
        'Call InputLineStyleInfor()
        Me.picTimeLineShow.Refresh()
        Dim g As System.Drawing.Graphics
        g = Me.picTimeLineShow.CreateGraphics
        Dim tmpPen As Pen
        tmpPen = New Pen(Me.labTimeLineColor.BackColor, Me.numLineWidth.Value)
        If Me.cmbLineStyle.Text.Trim.Length >= 2 Then
            Select Case Me.cmbLineStyle.Text.Trim.Substring(0, 2)
                Case "实线"
                    tmpPen.DashStyle = Drawing2D.DashStyle.Solid
                Case "长虚"
                    tmpPen.DashStyle = Drawing2D.DashStyle.Dash
                Case "点虚"
                    tmpPen.DashStyle = Drawing2D.DashStyle.Dot
                Case "点划"
                    tmpPen.DashStyle = Drawing2D.DashStyle.DashDot
                Case "双点"
                    tmpPen.DashStyle = Drawing2D.DashStyle.DashDotDot
            End Select
        End If
        g.DrawLine(tmpPen, 10, 13, Me.picTimeLineShow.Width - 20, 13)
    End Sub

    Private Sub cmbLineStyle_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLineStyle.DropDownClosed
        Me.cmbLineStyle.Text = Me.cmbLineStyle.SelectedItem
        Call ShowTimeLinePrint()
        Call InputLineStyleInfor()
    End Sub

    Private Sub InputLineStyleInfor()
        Dim i As Integer
        Dim nTrain As Integer
        For i = 1 To UBound(TimeTablePara.nPubTrains)
            nTrain = TimeTablePara.nPubTrains(i)
            If nTrain > 0 Then
                TrainInf(nTrain).PrintLineStyle = GetLineTextStyle(Me.cmbLineStyle.Text.Trim)
                TrainInf(nTrain).PrintLineWidth = Me.numLineWidth.Value
                TrainInf(nTrain).PrintLineColor = Me.labTimeLineColor.BackColor
            End If
        Next
    End Sub

    Private Sub numLineWidth_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles numLineWidth.MouseDown
        Call ShowTimeLinePrint()
        Call InputLineStyleInfor()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call addOneUndoInf()
        Call RefreshDiagram(1)
    End Sub
End Class