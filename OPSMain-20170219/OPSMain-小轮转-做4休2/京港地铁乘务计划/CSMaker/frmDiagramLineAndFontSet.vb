Public Class frmDiagramLineAndFontSet
    Private Structure TrainLineStyle
        Dim sJiaoLuName As String
        Dim TrainLineStyle As String
        Dim TrainLineWidth As Single
        Dim TrainLineColor As String
        Dim CheDiLineStyle As String
        Dim CheDiLineWidth As Single
        Dim CheDiLineColor As String
    End Structure
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmDiagramLineAndFontSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.cmbLineStyle.Items.Clear()
        Me.cmbDigramStyle.Items.Add("一分格")
        Me.cmbDigramStyle.Items.Add("二分格")
        Me.cmbDigramStyle.Items.Add("十分格")
        Me.cmbDigramStyle.Items.Add("小时格")
        Me.cmbDigramStyle.Text = CSTimeTablePara.TimeTableDiagramPara.strTimeFormat

        Call listTimeLineStyle()
        Me.cmbLineStyle.Items.Clear()
        Me.cmbLineStyle.Items.Add("实线 ─────────")
        Me.cmbLineStyle.Items.Add("长虚线— — — — — —")
        Me.cmbLineStyle.Items.Add("点虚线-----------------")
        Me.cmbLineStyle.Items.Add("点划线— - — - — - —")
        Me.cmbLineStyle.Items.Add("双点划线— -- — -- — ")
        Me.cmbLineStyle.Text = "实线 ─────────"

        Me.cmbTrainLineStyle.Items.Clear()
        Me.cmbTrainLineStyle.Items.Add("实线 ─────────")
        Me.cmbTrainLineStyle.Items.Add("长虚线— — — — — —")
        Me.cmbTrainLineStyle.Items.Add("点虚线-----------------")
        Me.cmbTrainLineStyle.Items.Add("点划线— - — - — - —")
        Me.cmbTrainLineStyle.Items.Add("双点划线— -- — -- — ")
        Me.cmbTrainLineStyle.Text = "实线 ─────────"

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


    Private Sub btnTimeLineColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTimeLineColor.Click
        Dim dColor As New ColorDialog
        dColor.Color = Me.labTimeLineColor.BackColor
        If dColor.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.labTimeLineColor.BackColor = dColor.Color
            Call ShowTimeLinePrint()
            Call InputLineStyleInfor()
        End If
    End Sub

    Private Sub lstTimeLine_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTimeLine.SelectedValueChanged
        Select Case Me.cmbDigramStyle.Text
            Case "一分格"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "一分格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.OneTime1LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.OneTime1LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime1LineColor)
                        Case "五分格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.OneTime5LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.OneTime5LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime5LineColor)
                        Case "十分格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.OneTime10LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.OneTime10LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime10LineColor)
                        Case "半小时格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.OneTime30LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.OneTime30LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime30LineColor)
                        Case "小时格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.OneTime60LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.OneTime60LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime60LineColor)
                    End Select
                End If
            Case "二分格"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "二分格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.TwoTime2LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.TwoTime2LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TwoTime2LineColor)
                        Case "十分格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.TwoTime10LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.TwoTime10LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TwoTime10LineColor)
                        Case "半小时格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.TwoTime30LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.TwoTime30LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TwoTime30LineColor)
                        Case "小时格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.TwoTime60LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.TwoTime60LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TwoTime60LineColor)
                    End Select
                End If
            Case "十分格"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "十分格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.TenTime10LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.TenTime10LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TenTime10LineColor)
                        Case "半小时格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.TenTime30LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.TenTime30LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TenTime30LineColor)
                        Case "小时格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.TenTime60LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.TenTime60LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TenTime60LineColor)
                    End Select
                End If
            Case "小时格"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "小时格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.HourTime60LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.HourTime60LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.HourTime60LineColor)
                    End Select
                End If
        End Select
        Call ShowTimeLinePrint()
    End Sub

    '显示列表，根据运行图类型显示可能的线名
    Private Sub listTimeLineStyle()
        Me.lstTimeLine.Items.Clear()
        Select Case Me.cmbDigramStyle.Text
            Case "一分格"
                Me.lstTimeLine.Items.Add("一分格线")
                Me.lstTimeLine.Items.Add("五分格线")
                Me.lstTimeLine.Items.Add("十分格线")
                Me.lstTimeLine.Items.Add("半小时格线")
                Me.lstTimeLine.Items.Add("小时格线")
            Case "二分格"
                Me.lstTimeLine.Items.Add("二分格线")
                Me.lstTimeLine.Items.Add("五分格线")
                Me.lstTimeLine.Items.Add("十分格线")
                Me.lstTimeLine.Items.Add("半小时格线")
                Me.lstTimeLine.Items.Add("小时格线")
            Case "十分格"
                Me.lstTimeLine.Items.Add("十分格线")
                Me.lstTimeLine.Items.Add("半小时格线")
                Me.lstTimeLine.Items.Add("小时格线")
            Case "小时格"
                Me.lstTimeLine.Items.Add("小时格线")
        End Select
    End Sub


    Private Sub btnSetLineStyleDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLineStyleDefault.Click
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String
        Select Case Me.cmbDigramStyle.Text
            Case "一分格"
                cellName = "一分格图1分格线型"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime1LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "一分格图1分格线宽"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime1LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "一分格图1分格线颜色"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime1LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "一分格图5分格线型"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime5LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "一分格图5分格线宽"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime5LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "一分格图5分格线颜色"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime5LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "一分格图10分格线型"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime10LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "一分格图10分格线宽"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime10LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "一分格图10分格线颜色"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime10LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "一分格图30分格线型"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime30LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "一分格图30分格线宽"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime30LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "一分格图30分格线颜色"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime30LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "一分格图60分格线型"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime60LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "一分格图60分格线宽"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime60LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "一分格图60分格线颜色"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime60LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

            Case "二分格"

                cellName = "二分格图2分格线型"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime2LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "二分格图2分格线宽"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime2LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "二分格图2分格线颜色"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime2LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "二分格图10分格线型"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime10LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "二分格图10分格线宽"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime10LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "二分格图10分格线颜色"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime10LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "二分格图30分格线型"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime30LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "二分格图30分格线宽"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime30LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "二分格图30分格线颜色"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime30LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "二分格图60分格线型"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime60LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "二分格图60分格线宽"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime60LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "二分格图60分格线颜色"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime60LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

            Case "十分格"

                cellName = "十分格图10分格线型"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime10LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "十分格图10分格线宽"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime10LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "十分格图10分格线颜色"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime10LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "十分格图30分格线型"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime30LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "十分格图30分格线宽"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime30LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "十分格图30分格线颜色"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime30LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "十分格图60分格线型"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime60LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "十分格图60分格线宽"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime60LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "十分格图60分格线颜色"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime60LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

            Case "小时格"

                cellName = "小时格图60分格线型"
                CellValue = CSTimeTablePara.DiagramStylePara.HourTime60LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "小时格图60分格线宽"
                CellValue = CSTimeTablePara.DiagramStylePara.HourTime60LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "小时格图60分格线颜色"
                CellValue = CSTimeTablePara.DiagramStylePara.HourTime60LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

        End Select


        cellName = "车站名称字体名称"
        CellValue = CSTimeTablePara.DiagramStylePara.StaNameFontName
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "车站名称字体大小"
        CellValue = CSTimeTablePara.DiagramStylePara.StaNameFontSize
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "车站名称字体粗体"
        CellValue = CSTimeTablePara.DiagramStylePara.StaNameFontBold
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "车站名称字体斜体"
        CellValue = CSTimeTablePara.DiagramStylePara.StaNameFontItalic
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "车站名称字体颜色"
        CellValue = CSTimeTablePara.DiagramStylePara.StaNameFontColor
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "时间标注字体名称"
        CellValue = CSTimeTablePara.DiagramStylePara.TimeFontName
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "时间标注字体大小"
        CellValue = CSTimeTablePara.DiagramStylePara.TimeFontSize
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "时间标注字体粗体"
        CellValue = CSTimeTablePara.DiagramStylePara.TimeFontBold
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "时间标注字体斜体"
        CellValue = CSTimeTablePara.DiagramStylePara.TimeFontItalic
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "时间标注字体颜色"
        CellValue = CSTimeTablePara.DiagramStylePara.TimeFontColor
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "一般车站中心线类型"
        CellValue = CSTimeTablePara.DiagramStylePara.StaLineStyle
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "一般车站中心线宽度"
        CellValue = CSTimeTablePara.DiagramStylePara.StaLineWidth
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "一般车站中心线颜色"
        CellValue = CSTimeTablePara.DiagramStylePara.StaLineColor
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "分岔站中心线类型"
        CellValue = CSTimeTablePara.DiagramStylePara.FenStaLineStyle
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "分岔站中心线宽度"
        CellValue = CSTimeTablePara.DiagramStylePara.FenStaLineWidth
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "分岔站中心线颜色"
        CellValue = CSTimeTablePara.DiagramStylePara.FenStaLineColor
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)
        cellName = "车场中心线类型"
        CellValue = CSTimeTablePara.DiagramStylePara.CheChangStaLineStyle
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "车场中心线宽度"
        CellValue = CSTimeTablePara.DiagramStylePara.CheChangStaLineWidth
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)


        cellName = "车场中心线颜色"
        CellValue = CSTimeTablePara.DiagramStylePara.CheChangStaLineColor
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        Call InputLineStyleInfor()

        MsgBox("已经成功设置!")
    End Sub

    Private Sub InputLineStyleInfor()
        Select Case Me.cmbDigramStyle.Text
            Case "一分格"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "一分格线"
                            CSTimeTablePara.DiagramStylePara.OneTime1LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.OneTime1LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.OneTime1LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "五分格线"
                            CSTimeTablePara.DiagramStylePara.OneTime5LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.OneTime5LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.OneTime5LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "十分格线"
                            CSTimeTablePara.DiagramStylePara.OneTime10LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.OneTime10LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.OneTime10LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "半小时格线"
                            CSTimeTablePara.DiagramStylePara.OneTime30LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.OneTime30LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.OneTime30LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "小时格线"
                            CSTimeTablePara.DiagramStylePara.OneTime60LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.OneTime60LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.OneTime60LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                    End Select
                End If
            Case "二分格"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "二分格线"
                            CSTimeTablePara.DiagramStylePara.TwoTime2LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.TwoTime2LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.TwoTime2LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "十分格线"
                            CSTimeTablePara.DiagramStylePara.TwoTime10LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.TwoTime10LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.TwoTime10LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "半小时格线"
                            CSTimeTablePara.DiagramStylePara.TwoTime30LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.TwoTime30LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.TwoTime30LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "小时格线"
                            CSTimeTablePara.DiagramStylePara.TwoTime60LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.TwoTime60LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.TwoTime60LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                    End Select
                End If
            Case "十分格"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "十分格线"
                            CSTimeTablePara.DiagramStylePara.TenTime10LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.TenTime10LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.TenTime10LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "半小时格线"
                            CSTimeTablePara.DiagramStylePara.TenTime30LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.TenTime30LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.TenTime30LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "小时格线"
                            CSTimeTablePara.DiagramStylePara.TenTime60LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.TenTime60LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.TenTime60LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                    End Select
                End If
            Case "小时格"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "小时格线"
                            CSTimeTablePara.DiagramStylePara.HourTime60LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.HourTime60LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.HourTime60LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                    End Select
                End If
        End Select

        If Me.lstStaLine.SelectedItem <> Nothing Then
            Select Case Me.lstStaLine.SelectedItem.ToString
                Case "一般车站"
                    CSTimeTablePara.DiagramStylePara.StaLineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                    CSTimeTablePara.DiagramStylePara.StaLineWidth = Me.numLineWidth.Value
                    CSTimeTablePara.DiagramStylePara.StaLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                Case "大站"
                    CSTimeTablePara.DiagramStylePara.FenStaLineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                    CSTimeTablePara.DiagramStylePara.FenStaLineWidth = Me.numLineWidth.Value
                    CSTimeTablePara.DiagramStylePara.FenStaLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                Case "车场"
                    CSTimeTablePara.DiagramStylePara.CheChangStaLineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                    CSTimeTablePara.DiagramStylePara.CheChangStaLineWidth = Me.numLineWidth.Value
                    CSTimeTablePara.DiagramStylePara.CheChangStaLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
            End Select
        End If

    End Sub

    Private Sub cmbDigramStyle_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDigramStyle.SelectedValueChanged
        Call listTimeLineStyle()
    End Sub

    Private Sub numLineWidth_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles numLineWidth.MouseDown
        Call ShowTimeLinePrint()
        Call InputLineStyleInfor()
    End Sub

    Private Sub cmbLineStyle_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLineStyle.DropDownClosed
        Me.cmbLineStyle.Text = Me.cmbLineStyle.SelectedItem
        Call ShowTimeLinePrint()
        Call InputLineStyleInfor()
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Call CSRefreshDiagram(0)
        Me.Close()
    End Sub

    Private Sub btnSelectFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectFont.Click
        Dim nd As New FontDialog
        Dim f As Font
        If Me.lstFont.SelectedItem <> Nothing Then
            Select Case Me.lstFont.SelectedItem.ToString
                Case "车站名称"
                    f = New Font(CSTimeTablePara.DiagramStylePara.StaNameFontName, CSTimeTablePara.DiagramStylePara.StaNameFontSize)
                    If CSTimeTablePara.DiagramStylePara.StaNameFontBold = True Then
                        If CSTimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If CSTimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If
                    nd.Font = f
                    If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        CSTimeTablePara.DiagramStylePara.StaNameFontName = nd.Font.Name
                        CSTimeTablePara.DiagramStylePara.StaNameFontSize = nd.Font.Size
                        CSTimeTablePara.DiagramStylePara.StaNameFontBold = nd.Font.Bold
                        CSTimeTablePara.DiagramStylePara.StaNameFontItalic = nd.Font.Italic
                    End If

                Case "时间标注"

                    f = New Font(CSTimeTablePara.DiagramStylePara.TimeFontName, CSTimeTablePara.DiagramStylePara.TimeFontSize)
                    If CSTimeTablePara.DiagramStylePara.TimeFontBold = True Then
                        If CSTimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If CSTimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If

                    nd.Font = f
                    If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        CSTimeTablePara.DiagramStylePara.TimeFontName = nd.Font.Name
                        CSTimeTablePara.DiagramStylePara.TimeFontSize = nd.Font.Size
                        CSTimeTablePara.DiagramStylePara.TimeFontBold = nd.Font.Bold
                        CSTimeTablePara.DiagramStylePara.TimeFontItalic = nd.Font.Italic
                    End If
            End Select
            Call ShowFontPreView()
        End If
    End Sub

    Private Sub btnFontColorSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFontColorSet.Click
        Dim dColor As New ColorDialog
        dColor.Color = Me.labTimeLineColor.BackColor
        If dColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.labFontColor.BackColor = dColor.Color
            If Me.lstFont.SelectedItem <> Nothing Then
                Select Case Me.lstFont.SelectedItem.ToString
                    Case "车站名称"
                        CSTimeTablePara.DiagramStylePara.StaNameFontColor = System.Drawing.ColorTranslator.ToHtml(Me.labFontColor.BackColor)
                    Case "时间标注"
                        CSTimeTablePara.DiagramStylePara.TimeFontColor = System.Drawing.ColorTranslator.ToHtml(Me.labFontColor.BackColor)
                End Select
            End If
            Call ShowFontPreView()
        End If
    End Sub

    '字体预览
    Private Sub ShowFontPreView()
        'CorTimeTablePara.DiagramStylePara.StaNameFontStyle = New Font("宋体", 20)
        'CorTimeTablePara.DiagramStylePara.TimeFontStyle = New Font("宋体", 20)
        Me.picFontShow.Refresh()
        Dim g As System.Drawing.Graphics
        g = Me.picFontShow.CreateGraphics
        Dim newBrush As Brush
        Dim f As Font
        If Me.lstFont.SelectedItem <> Nothing Then
            Select Case Me.lstFont.SelectedItem.ToString
                Case "车站名称"
                    f = New Font(CSTimeTablePara.DiagramStylePara.StaNameFontName, CSTimeTablePara.DiagramStylePara.StaNameFontSize)
                    If CSTimeTablePara.DiagramStylePara.StaNameFontBold = True Then
                        If CSTimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If CSTimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If

                    newBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.StaNameFontColor))
                    g.DrawString("这是预览效果012345aAbBcC", f, newBrush, 10, 10)

                    Me.labFontColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.StaNameFontColor)
                Case "时间标注"
                    f = New Font(CSTimeTablePara.DiagramStylePara.TimeFontName, CSTimeTablePara.DiagramStylePara.TimeFontSize)
                    If CSTimeTablePara.DiagramStylePara.TimeFontBold = True Then
                        If CSTimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If CSTimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If
                    newBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TimeFontColor))
                    g.DrawString("这是预览效果012345aAbBcC", f, newBrush, 10, 10)
                    Me.labFontColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TimeFontColor)
            End Select
        End If

    End Sub

    Private Sub lstFont_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFont.SelectedIndexChanged
        Call ShowFontPreView()
    End Sub

    Private Sub lstStaLine_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstStaLine.SelectedValueChanged
        If Me.lstStaLine.SelectedItem <> Nothing Then
            Select Case Me.lstStaLine.SelectedItem.ToString
                Case "一般车站"
                    Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.StaLineStyle)
                    Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.StaLineWidth
                    Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.StaLineColor)
                Case "大站"
                    Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.FenStaLineStyle)
                    Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.FenStaLineWidth
                    Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.FenStaLineColor)
                Case "车场"
                    Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.CheChangStaLineStyle)
                    Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.CheChangStaLineWidth
                    Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.CheChangStaLineColor)
            End Select
        End If
        Call ShowTimeLinePrint()
    End Sub

    Private Sub numTrainLineWidth_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles numTrainLineWidth.MouseDown
        Call ListTrainLineView()
        Call SetTrainLineStyle()
    End Sub

    Private Sub ListTrainLineView()
        Me.picTrainLineView.Refresh()
        Dim g As System.Drawing.Graphics
        g = Me.picTrainLineView.CreateGraphics
        Dim tmpPen As Pen
        tmpPen = New Pen(Me.labTrainLineColor.BackColor, Me.numTrainLineWidth.Value)
        tmpPen.DashStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
        g.DrawLine(tmpPen, 10, 13, Me.picTimeLineShow.Width - 20, 13)
    End Sub

    Private Sub SetTrainLineStyle()
        If Me.lstTrainStyle.SelectedItem = Nothing Then
            Exit Sub
        End If
        Select Case Me.lstTrainStyle.SelectedItem.ToString
            Case "未勾选任务段"
                CSTimeTablePara.DiagramStylePara.UnAssignTrainLineStyle = GetLineTextNameFromStyle(Me.cmbTrainLineStyle.Text)
                CSTimeTablePara.DiagramStylePara.UnAssignTrainLineWidth = Me.numTrainLineWidth.Value
                CSTimeTablePara.DiagramStylePara.UnAssignTrainLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTrainLineColor.BackColor)
            Case "班中任务"
                CSTimeTablePara.DiagramStylePara.DutyOnLineStyle = GetLineTextNameFromStyle(Me.cmbTrainLineStyle.Text)
                CSTimeTablePara.DiagramStylePara.DutyOnLineWidth = Me.numTrainLineWidth.Value
                CSTimeTablePara.DiagramStylePara.DutyOnLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTrainLineColor.BackColor)
            Case "小休任务"
                CSTimeTablePara.DiagramStylePara.DutyRestLineStyle = GetLineTextNameFromStyle(Me.cmbTrainLineStyle.Text)
                CSTimeTablePara.DiagramStylePara.DutyRestLineWidth = Me.numTrainLineWidth.Value
                CSTimeTablePara.DiagramStylePara.DutyRestLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTrainLineColor.BackColor)
            Case "用餐任务"
                CSTimeTablePara.DiagramStylePara.DutyDinnerLineStyle = GetLineTextNameFromStyle(Me.cmbTrainLineStyle.Text)
                CSTimeTablePara.DiagramStylePara.DutyDinnerLineWidth = Me.numTrainLineWidth.Value
                CSTimeTablePara.DiagramStylePara.DutyDinnerLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTrainLineColor.BackColor)
            Case "班后任务"
                CSTimeTablePara.DiagramStylePara.DutyOffLineStyle = GetLineTextNameFromStyle(Me.cmbTrainLineStyle.Text)
                CSTimeTablePara.DiagramStylePara.DutyOffLineWidth = Me.numTrainLineWidth.Value
                CSTimeTablePara.DiagramStylePara.DutyOffLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTrainLineColor.BackColor)
        End Select
    End Sub

    Private Sub btnSetTrainDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrainDefault.Click
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String
        Dim tab As DataTable

        cellName = "未勾选任务段线型"
        CellValue = CSTimeTablePara.DiagramStylePara.UnAssignTrainLineStyle
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','96','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "未勾选任务段线宽"
        CellValue = CSTimeTablePara.DiagramStylePara.UnAssignTrainLineWidth
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','97','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "未勾选任务段颜色"
        CellValue = CSTimeTablePara.DiagramStylePara.UnAssignTrainLineColor
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','98','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "班中任务线型"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyOnLineStyle
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','99','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "班中任务线宽"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyOnLineWidth
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','100','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "班中任务颜色"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyOnLineColor
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','101','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "小休任务线型"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyRestLineStyle
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','99','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "小休任务线宽"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyRestLineWidth
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','100','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "小休任务颜色"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyRestLineColor
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','101','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "用餐任务线型"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyDinnerLineStyle
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','102','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "用餐任务线宽"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyDinnerLineWidth
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','103','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "用餐任务颜色"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyDinnerLineColor
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','104','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "班后任务线型"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyOffLineStyle
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','105','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "班后任务线宽"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyOffLineWidth
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','106','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "班后任务颜色"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyOffLineColor
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','107','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "任务标号字体名称"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyNoFontName
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','108','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If
        
        cellName = "任务标号字体大小"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyNoFontSize
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','109','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "任务标号字体粗体"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyNoFontBold
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','110','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "任务标号字体斜体"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyNoFontItalic
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','111','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "任务标号字体颜色"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyNoFontColor
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','112','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "斜向车次字体名称"
        CellValue = CSTimeTablePara.DiagramStylePara.XieCheCiFontName
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "斜向车次字体大小"
        CellValue = CSTimeTablePara.DiagramStylePara.XieCheCiFontSize
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "斜向车次字体粗体"
        CellValue = CSTimeTablePara.DiagramStylePara.XieCheCiFontBold
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "斜向车次字体斜体"
        CellValue = CSTimeTablePara.DiagramStylePara.XieCheCiFontItalic
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "斜向车次字体颜色"
        CellValue = CSTimeTablePara.DiagramStylePara.XieCheCiFontColor
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)
        MsgBox("已经成功设置!")

    End Sub

    Private Sub cmbTrainLineStyle_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTrainLineStyle.DropDownClosed
        Me.cmbTrainLineStyle.Text = Me.cmbTrainLineStyle.SelectedItem
        Call ListTrainLineView()
        Call SetTrainLineStyle()
    End Sub

    Private Sub btnTrainLineColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrainLineColor.Click
        Dim dColor As New ColorDialog
        dColor.Color = Me.labTrainLineColor.BackColor
        If dColor.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.labTrainLineColor.BackColor = dColor.Color
            Call ListTrainLineView()
            Call SetTrainLineStyle()
        End If
    End Sub

    Private Sub btnCheCiFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheCiFont.Click
        Dim nd As New FontDialog
        Dim f As Font
        If Me.lstCheCiFont.SelectedItem <> Nothing Then
            Select Case Me.lstCheCiFont.SelectedItem.ToString
                Case "运行线车次"
                    f = New Font(CSTimeTablePara.DiagramStylePara.XieCheCiFontName, CSTimeTablePara.DiagramStylePara.XieCheCiFontSize)
                    If CSTimeTablePara.DiagramStylePara.XieCheCiFontBold = True Then
                        If CSTimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If CSTimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If
                    nd.Font = f
                    If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        CSTimeTablePara.DiagramStylePara.XieCheCiFontName = nd.Font.Name
                        CSTimeTablePara.DiagramStylePara.XieCheCiFontSize = nd.Font.Size
                        CSTimeTablePara.DiagramStylePara.XieCheCiFontBold = nd.Font.Bold
                        CSTimeTablePara.DiagramStylePara.XieCheCiFontItalic = nd.Font.Italic
                    End If

                Case "任务标号"
                    f = New Font(CSTimeTablePara.DiagramStylePara.DutyNoFontName, CSTimeTablePara.DiagramStylePara.DutyNoFontSize)
                    If CSTimeTablePara.DiagramStylePara.DutyNoFontBold = True Then
                        If CSTimeTablePara.DiagramStylePara.DutyNoFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If CSTimeTablePara.DiagramStylePara.DutyNoFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If
                    nd.Font = f
                    If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        CSTimeTablePara.DiagramStylePara.DutyNoFontName = nd.Font.Name
                        CSTimeTablePara.DiagramStylePara.DutyNoFontSize = nd.Font.Size
                        CSTimeTablePara.DiagramStylePara.DutyNoFontBold = nd.Font.Bold
                        CSTimeTablePara.DiagramStylePara.DutyNoFontItalic = nd.Font.Italic
                    End If

            End Select
            Call ShowCheCiFontPreView()

        End If
    End Sub

    '显示车次
    Private Sub ShowCheCiFontPreView()
        Me.picCheCiFont.Refresh()
        Dim g As System.Drawing.Graphics
        g = Me.picCheCiFont.CreateGraphics
        Dim newBrush As Brush
        Dim f As Font
        If Me.lstCheCiFont.SelectedItem <> Nothing Then
            Select Case Me.lstCheCiFont.SelectedItem.ToString
                Case "运行线车次"
                    f = New Font(CSTimeTablePara.DiagramStylePara.XieCheCiFontName, CSTimeTablePara.DiagramStylePara.XieCheCiFontSize)
                    If CSTimeTablePara.DiagramStylePara.XieCheCiFontBold = True Then
                        If CSTimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If CSTimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If

                    newBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.XieCheCiFontColor))
                    g.DrawString("这是预览效果012345aAbBcC", f, newBrush, 10, 10)
                    Me.labCheCiColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.XieCheCiFontColor)
                Case "任务标号"
                    f = New Font(CSTimeTablePara.DiagramStylePara.DutyNoFontName, CSTimeTablePara.DiagramStylePara.DutyNoFontSize)
                    If CSTimeTablePara.DiagramStylePara.DutyNoFontBold = True Then
                        If CSTimeTablePara.DiagramStylePara.DutyNoFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If CSTimeTablePara.DiagramStylePara.DutyNoFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If

                    newBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyNoFontColor))
                    g.DrawString("这是预览效果012345aAbBcC", f, newBrush, 10, 10)
                    Me.labCheCiColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyNoFontColor)
            End Select
        End If
    End Sub

    Private Sub lstCheCiFont_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCheCiFont.SelectedIndexChanged
        Call ShowCheCiFontPreView()
    End Sub

    Private Sub btnCheCiColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheCiColor.Click
        Dim dColor As New ColorDialog
        dColor.Color = Me.labCheCiColor.BackColor
        If dColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.labCheCiColor.BackColor = dColor.Color
            If Me.lstCheCiFont.SelectedItem <> Nothing Then
                Select Case Me.lstCheCiFont.SelectedItem.ToString
                    Case "运行线车次"
                        CSTimeTablePara.DiagramStylePara.XieCheCiFontColor = System.Drawing.ColorTranslator.ToHtml(Me.labCheCiColor.BackColor)
                    Case "任务标号"
                        CSTimeTablePara.DiagramStylePara.DutyNoFontColor = System.Drawing.ColorTranslator.ToHtml(Me.labCheCiColor.BackColor)
                End Select
            End If
            Call ShowCheCiFontPreView()
        End If
    End Sub

    Private Sub btnSetTrainLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrainLine.Click
        Call SetTrainLineStyle()
        Call CSRefreshDiagram(0)
    End Sub

    Private Sub lstTrainStyle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTrainStyle.SelectedIndexChanged
        Select Case Me.lstTrainStyle.SelectedItem.ToString
            Case "未勾选任务段"
                Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.UnAssignTrainLineStyle)
                Me.numTrainLineWidth.Value = CSTimeTablePara.DiagramStylePara.UnAssignTrainLineWidth
                Me.labTrainLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.UnAssignTrainLineColor)
            Case "班中任务"
                Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.DutyOnLineStyle)
                Me.numTrainLineWidth.Value = CSTimeTablePara.DiagramStylePara.DutyOnLineWidth
                Me.labTrainLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyOnLineColor)
            Case "小休任务"
                Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.DutyRestLineStyle)
                Me.numTrainLineWidth.Value = CSTimeTablePara.DiagramStylePara.DutyRestLineWidth
                Me.labTrainLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyRestLineColor)
            Case "用餐任务"
                Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.DutyDinnerLineStyle)
                Me.numTrainLineWidth.Value = CSTimeTablePara.DiagramStylePara.DutyDinnerLineWidth
                Me.labTrainLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyDinnerLineColor)
            Case "班后任务"
                Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.DutyOffLineStyle)
                Me.numTrainLineWidth.Value = CSTimeTablePara.DiagramStylePara.DutyOffLineWidth
                Me.labTrainLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyOffLineColor)
        End Select
        Call ListTrainLineView()
    End Sub

End Class