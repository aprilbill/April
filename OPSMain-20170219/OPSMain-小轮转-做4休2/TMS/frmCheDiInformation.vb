Public Class frmCheDiInformation

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
        Call RefreshDiagram(1)
    End Sub

    Private Sub frmCheDiInformation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.GroupBox1.Visible = False
        Dim i As Integer
        Dim nToStopTime As Long
        Dim nToSecTime As Long
        nToStopTime = 0
        nToSecTime = 0
        Dim sngToLength As Single
        sngToLength = 0
        Dim nFirTrain As Integer
        Dim nEndTrain As Integer
        Dim lngTime1 As Long
        Dim lngTime2 As Long

        '基本信息
        Me.dataGrid.RowCount = 5
        Me.dataGrid.Rows(0).Height = 20
        Me.dataGrid.Rows(0).Cells(0).Value = "车底ID"
        Me.dataGrid.Rows(0).Cells(1).Value = TimeTablePara.nPubCheDi
        Me.dataGrid.Rows(1).Cells(0).Value = "车底号"
        Me.dataGrid.Rows(1).Cells(1).Value = ChediInfo(TimeTablePara.nPubCheDi).sCheCiHao
        Me.dataGrid.Rows(2).Cells(0).Value = "车底类型"
        Me.dataGrid.Rows(2).Cells(1).Value = ChediInfo(TimeTablePara.nPubCheDi).SCheDiLeiXing
        Me.dataGrid.Rows(3).Cells(0).Value = "接续列车数"
        Me.dataGrid.Rows(3).Cells(1).Value = UBound(ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain)
        Me.dataGrid.Rows(4).Cells(0).Value = "总周转时间"
        If UBound(ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain) > 0 Then
            nFirTrain = ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain(1)
            nEndTrain = ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain(UBound(ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain))
            lngTime1 = GetTrainArriOrStartTime(nFirTrain, 0, 1)
            lngTime2 = GetTrainArriOrStartTime(nFirTrain, -1, 0)
            Me.dataGrid.Rows(4).Cells(1).Value = SecondToHour(TimeMinus(lngTime2, lngTime1), 0)
        Else
            Me.dataGrid.Rows(4).Cells(1).Value = "0"
        End If
        'Me.dataGrid.Rows(4).Cells(0).Value = "车次名称"
        'Me.dataGrid.Rows(4).Cells(1).Value = TrainInf(TimeTablePara.npubtrain).sLineNum
        'Me.dataGrid.Rows(6).Cells(0).Value = "车底类型"
        'Me.dataGrid.Rows(6).Cells(1).Value = TrainInf(TimeTablePara.npubtrain).SCheDiLeiXing
        'Me.dataGrid.Rows(7).Cells(0).Value = "运行标尺"
        'Me.dataGrid.Rows(7).Cells(1).Value = TrainInf(TimeTablePara.npubtrain).sRunScaleName
        'Me.dataGrid.Rows(8).Cells(0).Value = "停站标尺"
        'Me.dataGrid.Rows(8).Cells(1).Value = TrainInf(TimeTablePara.npubtrain).sStopSclaeName
        'Me.dataGrid.Rows(9).Cells(0).Value = "列车类型"
        'Me.dataGrid.Rows(9).Cells(1).Value = TrainInf(TimeTablePara.npubtrain).TrainStyle
        'Me.dataGrid.Rows(10).Cells(0).Value = "始发折返方式"
        'Me.dataGrid.Rows(10).Cells(1).Value = TrainInf(TimeTablePara.npubtrain).TrainReturnStyle(1)
        'Me.dataGrid.Rows(11).Cells(0).Value = "终到折返方式"
        'Me.dataGrid.Rows(11).Cells(1).Value = TrainInf(TimeTablePara.npubtrain).TrainReturnStyle(2)

        '接续列车信息
        Me.DataGridTrain.Rows.Clear()
        Dim nBeTime As Long
        nBeTime = -1
        For i = 1 To UBound(ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain)
            nFirTrain = ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain(i)
            Me.DataGridTrain.Rows.Add()
            Me.DataGridTrain.Rows(Me.DataGridTrain.Rows.Count - 1).Cells(0).Value = Me.DataGridTrain.Rows.Count
            Me.DataGridTrain.Rows(Me.DataGridTrain.Rows.Count - 1).Cells(1).Value = TrainInf(nFirTrain).sPrintTrain & "(" & nFirTrain & ")"
            Me.DataGridTrain.Rows(Me.DataGridTrain.Rows.Count - 1).Cells(2).Value = SecondToHour(GetTrainArriOrStartTime(nFirTrain, 0, 1), 0)
            Me.DataGridTrain.Rows(Me.DataGridTrain.Rows.Count - 1).Cells(3).Value = SecondToHour(GetTrainArriOrStartTime(nFirTrain, -1, 0), 0)
            If nBeTime >= 0 Then
                Me.DataGridTrain.Rows(Me.DataGridTrain.Rows.Count - 1).Cells(4).Value = SecondToMinute(TimeMinus(GetTrainArriOrStartTime(nFirTrain, 0, 1), nBeTime))
            Else
                Me.DataGridTrain.Rows(Me.DataGridTrain.Rows.Count - 1).Cells(4).Value = "0"
            End If
            nBeTime = GetTrainArriOrStartTime(nFirTrain, -1, 0)
        Next i

        Me.cmbLineStyle.Text = GetLineStyleNameFromText(GetLineStyleTextFromStyleName(ChediInfo(TimeTablePara.nPubCheDi).PrintCheDiLinkStyle))
        Me.numLineWidth.Value = ChediInfo(TimeTablePara.nPubCheDi).PrintCheDiLinkWidth
        Me.labTimeLineColor.BackColor = ChediInfo(TimeTablePara.nPubCheDi).PrintCheDiLinkColor

        If ChediInfo(TimeTablePara.nPubCheDi).bIfGouWang = True Then
            Me.chkAutoResetBianHao.Checked = True
        Else
            Me.chkAutoResetBianHao.Checked = False
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
        Dim nCheDiID As Integer
        Dim nTrain As Integer
        Dim i As Integer
        Dim j As Integer
        For i = 1 To UBound(TimeTablePara.nPubTrains)
            nCheDiID = CheCiToCheDiID(TimeTablePara.nPubTrains(i))
            If nCheDiID > 0 Then
                ChediInfo(nCheDiID).PrintCheDiLinkStyle = GetLineTextStyle(Me.cmbLineStyle.Text.Trim)
                ChediInfo(nCheDiID).PrintCheDiLinkWidth = Me.numLineWidth.Value
                ChediInfo(nCheDiID).PrintCheDiLinkColor = Me.labTimeLineColor.BackColor
                If Me.chkAllTrains.Checked = True Then
                    For j = 1 To UBound(ChediInfo(nCheDiID).nLinkTrain)
                        nTrain = ChediInfo(nCheDiID).nLinkTrain(j)
                        If nTrain > 0 Then
                            TrainInf(nTrain).PrintLineStyle = GetLineTextStyle(Me.cmbLineStyle.Text.Trim)
                            TrainInf(nTrain).PrintLineWidth = Me.numLineWidth.Value
                            TrainInf(nTrain).PrintLineColor = Me.labTimeLineColor.BackColor
                        End If
                    Next
                End If
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

    Private Sub chkAutoResetBianHao_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutoResetBianHao.CheckedChanged
        If Me.chkAutoResetBianHao.Checked = True Then
            ChediInfo(TimeTablePara.nPubCheDi).bIfGouWang = True
        Else
            ChediInfo(TimeTablePara.nPubCheDi).bIfGouWang = False
        End If

    End Sub

    Private Sub btnApplyAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApplyAll.Click
        Dim i As Integer
        If Me.chkAutoResetBianHao.Checked = True Then
            For i = 1 To UBound(ChediInfo)
                ChediInfo(i).bIfGouWang = True
            Next
        Else
            For i = 1 To UBound(ChediInfo)
                ChediInfo(i).bIfGouWang = False
            Next
        End If
    End Sub
End Class