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
        Me.cmbDigramStyle.Text = TimeTablePara.TimeTableDiagramPara.strTimeFormat

        Call listTimeLineStyle()
        Me.cmbLineStyle.Items.Clear()
        Me.cmbLineStyle.Items.Add("实线 ─────────")
        Me.cmbLineStyle.Items.Add("长虚线— — — — — —")
        Me.cmbLineStyle.Items.Add("点虚线-----------------")
        Me.cmbLineStyle.Items.Add("点划线— - — - — - —")
        Me.cmbLineStyle.Items.Add("双点划线— -- — -- — ")
        Me.cmbLineStyle.Text = "实线 ─────────"

        Me.cmbTrainStyle.Items.Clear()
        Me.cmbTrainStyle.Items.Add("所有运行线")
        Me.cmbTrainStyle.Items.Add("所有车底连接线")
        Me.cmbTrainStyle.Items.Add("运行线按交路分类")
        Me.cmbTrainStyle.Items.Add("运行线按运行标尺")
        Me.cmbTrainStyle.Items.Add("运行线按线路编号")
        Me.cmbTrainStyle.Items.Add("运行线按车底编号分类")
        Me.cmbTrainStyle.Items.Add("车底线按车底编号分类")
        Me.cmbTrainStyle.Items.Add("所有运行线分上下行")
        Me.cmbTrainStyle.Text = "所有运行线"

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
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.OneTime1LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.OneTime1LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime1LineColor)
                        Case "五分格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.OneTime5LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.OneTime5LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime5LineColor)
                        Case "十分格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.OneTime10LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.OneTime10LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime10LineColor)
                        Case "半小时格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.OneTime30LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.OneTime30LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime30LineColor)
                        Case "小时格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.OneTime60LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.OneTime60LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime60LineColor)
                    End Select
                End If
            Case "二分格"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "二分格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.TwoTime2LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.TwoTime2LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime2LineColor)
                        Case "十分格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.TwoTime10LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.TwoTime10LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime10LineColor)
                        Case "半小时格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.TwoTime30LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.TwoTime30LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime30LineColor)
                        Case "小时格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.TwoTime60LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.TwoTime60LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime60LineColor)
                    End Select
                End If
            Case "十分格"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "十分格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.TenTime10LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.TenTime10LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TenTime10LineColor)
                        Case "半小时格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.TenTime30LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.TenTime30LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TenTime30LineColor)
                        Case "小时格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.TenTime60LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.TenTime60LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TenTime60LineColor)
                    End Select
                End If
            Case "小时格"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "小时格线"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.HourTime60LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.HourTime60LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.HourTime60LineColor)
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
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String
        Select Case Me.cmbDigramStyle.Text
            Case "一分格"
                cellName = "一分格图1分格线型"
                CellValue = TimeTablePara.DiagramStylePara.OneTime1LineStyle
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom.ExecuteNonQuery()

                cellName = "一分格图1分格线宽"
                CellValue = TimeTablePara.DiagramStylePara.OneTime1LineWidth
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom1.ExecuteNonQuery()

                cellName = "一分格图1分格线颜色"
                CellValue = TimeTablePara.DiagramStylePara.OneTime1LineColor
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom2.ExecuteNonQuery()

                cellName = "一分格图5分格线型"
                CellValue = TimeTablePara.DiagramStylePara.OneTime5LineStyle
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom3 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom3.ExecuteNonQuery()

                cellName = "一分格图5分格线宽"
                CellValue = TimeTablePara.DiagramStylePara.OneTime5LineWidth
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom4 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom4.ExecuteNonQuery()

                cellName = "一分格图5分格线颜色"
                CellValue = TimeTablePara.DiagramStylePara.OneTime5LineColor
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom5 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom5.ExecuteNonQuery()

                cellName = "一分格图10分格线型"
                CellValue = TimeTablePara.DiagramStylePara.OneTime10LineStyle
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom6 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom6.ExecuteNonQuery()

                cellName = "一分格图10分格线宽"
                CellValue = TimeTablePara.DiagramStylePara.OneTime10LineWidth
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom7 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom7.ExecuteNonQuery()

                cellName = "一分格图10分格线颜色"
                CellValue = TimeTablePara.DiagramStylePara.OneTime10LineColor
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom8 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom8.ExecuteNonQuery()

                cellName = "一分格图30分格线型"
                CellValue = TimeTablePara.DiagramStylePara.OneTime30LineStyle
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom9 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom9.ExecuteNonQuery()

                cellName = "一分格图30分格线宽"
                CellValue = TimeTablePara.DiagramStylePara.OneTime30LineWidth
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom10 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom10.ExecuteNonQuery()

                cellName = "一分格图30分格线颜色"
                CellValue = TimeTablePara.DiagramStylePara.OneTime30LineColor
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom11 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom11.ExecuteNonQuery()

                cellName = "一分格图60分格线型"
                CellValue = TimeTablePara.DiagramStylePara.OneTime60LineStyle
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom12 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom12.ExecuteNonQuery()

                cellName = "一分格图60分格线宽"
                CellValue = TimeTablePara.DiagramStylePara.OneTime60LineWidth
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom13 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom13.ExecuteNonQuery()

                cellName = "一分格图60分格线颜色"
                CellValue = TimeTablePara.DiagramStylePara.OneTime60LineColor
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom14 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom14.ExecuteNonQuery()

            Case "二分格"

                cellName = "二分格图2分格线型"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime2LineStyle
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom.ExecuteNonQuery()

                cellName = "二分格图2分格线宽"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime2LineWidth
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom1.ExecuteNonQuery()

                cellName = "二分格图2分格线颜色"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime2LineColor
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom2.ExecuteNonQuery()

                cellName = "二分格图10分格线型"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime10LineStyle
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom3 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom3.ExecuteNonQuery()

                cellName = "二分格图10分格线宽"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime10LineWidth
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom4 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom4.ExecuteNonQuery()

                cellName = "二分格图10分格线颜色"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime10LineColor
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom5 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom5.ExecuteNonQuery()

                cellName = "二分格图30分格线型"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime30LineStyle
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom6 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom6.ExecuteNonQuery()

                cellName = "二分格图30分格线宽"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime30LineWidth
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom7 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom7.ExecuteNonQuery()

                cellName = "二分格图30分格线颜色"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime30LineColor
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom8 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom8.ExecuteNonQuery()

                cellName = "二分格图60分格线型"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime60LineStyle
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom9 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom9.ExecuteNonQuery()

                cellName = "二分格图60分格线宽"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime60LineWidth
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom10 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom10.ExecuteNonQuery()

                cellName = "二分格图60分格线颜色"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime60LineColor
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom11 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom11.ExecuteNonQuery()

            Case "十分格"

                cellName = "十分格图10分格线型"
                CellValue = TimeTablePara.DiagramStylePara.TenTime10LineStyle
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom.ExecuteNonQuery()

                cellName = "十分格图10分格线宽"
                CellValue = TimeTablePara.DiagramStylePara.TenTime10LineWidth
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom1.ExecuteNonQuery()

                cellName = "十分格图10分格线颜色"
                CellValue = TimeTablePara.DiagramStylePara.TenTime10LineColor
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom2.ExecuteNonQuery()


                cellName = "十分格图30分格线型"
                CellValue = TimeTablePara.DiagramStylePara.TenTime30LineStyle
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom3 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom3.ExecuteNonQuery()

                cellName = "十分格图30分格线宽"
                CellValue = TimeTablePara.DiagramStylePara.TenTime30LineWidth
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom4 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom4.ExecuteNonQuery()

                cellName = "十分格图30分格线颜色"
                CellValue = TimeTablePara.DiagramStylePara.TenTime30LineColor
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom6 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom6.ExecuteNonQuery()


                cellName = "十分格图60分格线型"
                CellValue = TimeTablePara.DiagramStylePara.TenTime60LineStyle
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom7 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom7.ExecuteNonQuery()

                cellName = "十分格图60分格线宽"
                CellValue = TimeTablePara.DiagramStylePara.TenTime60LineWidth
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom8 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom8.ExecuteNonQuery()

                cellName = "十分格图60分格线颜色"
                CellValue = TimeTablePara.DiagramStylePara.TenTime60LineColor
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom9 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom9.ExecuteNonQuery()

            Case "小时格"

                cellName = "小时格图60分格线型"
                CellValue = TimeTablePara.DiagramStylePara.HourTime60LineStyle
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom.ExecuteNonQuery()

                cellName = "小时格图60分格线宽"
                CellValue = TimeTablePara.DiagramStylePara.HourTime60LineWidth
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom1.ExecuteNonQuery()

                cellName = "小时格图60分格线颜色"
                CellValue = TimeTablePara.DiagramStylePara.HourTime60LineColor
                Str = "update 运行图系统参数表 set " & _
                        "数值 ='" & CellValue & "'" & _
                        "where 参数名 = '" & cellName & "'"
                Dim MyCom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom2.ExecuteNonQuery()

        End Select


        cellName = "车站名称字体名称"
        CellValue = TimeTablePara.DiagramStylePara.StaNameFontName
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom02 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom02.ExecuteNonQuery()

        cellName = "车站名称字体大小"
        CellValue = TimeTablePara.DiagramStylePara.StaNameFontSize
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom03 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom03.ExecuteNonQuery()

        cellName = "车站名称字体粗体"
        CellValue = TimeTablePara.DiagramStylePara.StaNameFontBold
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom04 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom04.ExecuteNonQuery()

        cellName = "车站名称字体斜体"
        CellValue = TimeTablePara.DiagramStylePara.StaNameFontItalic
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom05 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom05.ExecuteNonQuery()

        cellName = "车站名称字体颜色"
        CellValue = TimeTablePara.DiagramStylePara.StaNameFontColor
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom06 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom06.ExecuteNonQuery()

        cellName = "时间标注字体名称"
        CellValue = TimeTablePara.DiagramStylePara.TimeFontName
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom07 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom07.ExecuteNonQuery()

        cellName = "时间标注字体大小"
        CellValue = TimeTablePara.DiagramStylePara.TimeFontSize
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom08 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom08.ExecuteNonQuery()

        cellName = "时间标注字体粗体"
        CellValue = TimeTablePara.DiagramStylePara.TimeFontBold
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom09 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom09.ExecuteNonQuery()

        cellName = "时间标注字体斜体"
        CellValue = TimeTablePara.DiagramStylePara.TimeFontItalic
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom010 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom010.ExecuteNonQuery()

        cellName = "时间标注字体颜色"
        CellValue = TimeTablePara.DiagramStylePara.TimeFontColor
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom011 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom011.ExecuteNonQuery()

        cellName = "一般车站中心线类型"
        CellValue = TimeTablePara.DiagramStylePara.StaLineStyle
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom012 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom012.ExecuteNonQuery()

        cellName = "一般车站中心线宽度"
        CellValue = TimeTablePara.DiagramStylePara.StaLineWidth
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom013 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom013.ExecuteNonQuery()

        cellName = "一般车站中心线颜色"
        CellValue = TimeTablePara.DiagramStylePara.StaLineColor
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom014 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom014.ExecuteNonQuery()

        cellName = "分岔站中心线类型"
        CellValue = TimeTablePara.DiagramStylePara.FenStaLineStyle
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom015 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom015.ExecuteNonQuery()

        cellName = "分岔站中心线宽度"
        CellValue = TimeTablePara.DiagramStylePara.FenStaLineWidth
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom016 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom016.ExecuteNonQuery()

        cellName = "分岔站中心线颜色"
        CellValue = TimeTablePara.DiagramStylePara.FenStaLineColor
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom017 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom017.ExecuteNonQuery()

        cellName = "车场中心线类型"
        CellValue = TimeTablePara.DiagramStylePara.CheChangStaLineStyle
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom018 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom018.ExecuteNonQuery()

        cellName = "车场中心线宽度"
        CellValue = TimeTablePara.DiagramStylePara.CheChangStaLineWidth
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom019 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom019.ExecuteNonQuery()


        cellName = "车场中心线颜色"
        CellValue = TimeTablePara.DiagramStylePara.CheChangStaLineColor
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom020 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom020.ExecuteNonQuery()

        MyConn.Close()
        Call InputLineStyleInfor()

        MsgBox("已经成功设置!", , "提示")
    End Sub

    Private Sub InputLineStyleInfor()
        Select Case Me.cmbDigramStyle.Text
            Case "一分格"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "一分格线"
                            TimeTablePara.DiagramStylePara.OneTime1LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.OneTime1LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.OneTime1LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "五分格线"
                            TimeTablePara.DiagramStylePara.OneTime5LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.OneTime5LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.OneTime5LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "十分格线"
                            TimeTablePara.DiagramStylePara.OneTime10LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.OneTime10LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.OneTime10LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "半小时格线"
                            TimeTablePara.DiagramStylePara.OneTime30LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.OneTime30LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.OneTime30LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "小时格线"
                            TimeTablePara.DiagramStylePara.OneTime60LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.OneTime60LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.OneTime60LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                    End Select
                End If
            Case "二分格"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "二分格线"
                            TimeTablePara.DiagramStylePara.TwoTime2LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.TwoTime2LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.TwoTime2LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "十分格线"
                            TimeTablePara.DiagramStylePara.TwoTime10LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.TwoTime10LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.TwoTime10LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "半小时格线"
                            TimeTablePara.DiagramStylePara.TwoTime30LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.TwoTime30LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.TwoTime30LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "小时格线"
                            TimeTablePara.DiagramStylePara.TwoTime60LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.TwoTime60LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.TwoTime60LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                    End Select
                End If
            Case "十分格"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "十分格线"
                            TimeTablePara.DiagramStylePara.TenTime10LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.TenTime10LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.TenTime10LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "半小时格线"
                            TimeTablePara.DiagramStylePara.TenTime30LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.TenTime30LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.TenTime30LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "小时格线"
                            TimeTablePara.DiagramStylePara.TenTime60LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.TenTime60LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.TenTime60LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                    End Select
                End If
            Case "小时格"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "小时格线"
                            TimeTablePara.DiagramStylePara.HourTime60LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.HourTime60LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.HourTime60LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                    End Select
                End If
        End Select

        If Me.lstStaLine.SelectedItem <> Nothing Then
            Select Case Me.lstStaLine.SelectedItem.ToString
                Case "一般车站"
                    TimeTablePara.DiagramStylePara.StaLineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                    TimeTablePara.DiagramStylePara.StaLineWidth = Me.numLineWidth.Value
                    TimeTablePara.DiagramStylePara.StaLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                Case "大站"
                    TimeTablePara.DiagramStylePara.FenStaLineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                    TimeTablePara.DiagramStylePara.FenStaLineWidth = Me.numLineWidth.Value
                    TimeTablePara.DiagramStylePara.FenStaLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                Case "车场"
                    TimeTablePara.DiagramStylePara.CheChangStaLineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                    TimeTablePara.DiagramStylePara.CheChangStaLineWidth = Me.numLineWidth.Value
                    TimeTablePara.DiagramStylePara.CheChangStaLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
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
        Call addOneUndoInf()
        Call RefreshDiagram(1)
        Me.Close()
    End Sub

    Private Sub btnSelectFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectFont.Click
        Dim nd As New FontDialog
        Dim f As Font
        If Me.lstFont.SelectedItem <> Nothing Then
            Select Case Me.lstFont.SelectedItem.ToString
                Case "车站名称"
                    f = New Font(TimeTablePara.DiagramStylePara.StaNameFontName, TimeTablePara.DiagramStylePara.StaNameFontSize)
                    If TimeTablePara.DiagramStylePara.StaNameFontBold = True Then
                        If TimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If TimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If
                    nd.Font = f
                    If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TimeTablePara.DiagramStylePara.StaNameFontName = nd.Font.Name
                        TimeTablePara.DiagramStylePara.StaNameFontSize = nd.Font.Size
                        TimeTablePara.DiagramStylePara.StaNameFontBold = nd.Font.Bold
                        TimeTablePara.DiagramStylePara.StaNameFontItalic = nd.Font.Italic
                    End If

                Case "时间标注"

                    f = New Font(TimeTablePara.DiagramStylePara.TimeFontName, TimeTablePara.DiagramStylePara.TimeFontSize)
                    If TimeTablePara.DiagramStylePara.TimeFontBold = True Then
                        If TimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If TimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If

                    nd.Font = f
                    If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TimeTablePara.DiagramStylePara.TimeFontName = nd.Font.Name
                        TimeTablePara.DiagramStylePara.TimeFontSize = nd.Font.Size
                        TimeTablePara.DiagramStylePara.TimeFontBold = nd.Font.Bold
                        TimeTablePara.DiagramStylePara.TimeFontItalic = nd.Font.Italic
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
                        TimeTablePara.DiagramStylePara.StaNameFontColor = System.Drawing.ColorTranslator.ToHtml(Me.labFontColor.BackColor)
                    Case "时间标注"
                        TimeTablePara.DiagramStylePara.TimeFontColor = System.Drawing.ColorTranslator.ToHtml(Me.labFontColor.BackColor)
                End Select
            End If
            Call ShowFontPreView()
        End If
    End Sub

    '字体预览
    Private Sub ShowFontPreView()
        'TimeTablePara.DiagramStylePara.StaNameFontStyle = New Font("宋体", 20)
        'TimeTablePara.DiagramStylePara.TimeFontStyle = New Font("宋体", 20)
        Me.picFontShow.Refresh()
        Dim g As System.Drawing.Graphics
        g = Me.picFontShow.CreateGraphics
        Dim newBrush As Brush
        Dim f As Font
        If Me.lstFont.SelectedItem <> Nothing Then
            Select Case Me.lstFont.SelectedItem.ToString
                Case "车站名称"
                    f = New Font(TimeTablePara.DiagramStylePara.StaNameFontName, TimeTablePara.DiagramStylePara.StaNameFontSize)
                    If TimeTablePara.DiagramStylePara.StaNameFontBold = True Then
                        If TimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If TimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If

                    newBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.StaNameFontColor))
                    g.DrawString("这是预览效果012345aAbBcC", f, newBrush, 10, 10)

                    Me.labFontColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.StaNameFontColor)
                Case "时间标注"
                    f = New Font(TimeTablePara.DiagramStylePara.TimeFontName, TimeTablePara.DiagramStylePara.TimeFontSize)
                    If TimeTablePara.DiagramStylePara.TimeFontBold = True Then
                        If TimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If TimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If
                    newBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TimeFontColor))
                    g.DrawString("这是预览效果012345aAbBcC", f, newBrush, 10, 10)
                    Me.labFontColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TimeFontColor)
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
                    Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.StaLineStyle)
                    Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.StaLineWidth
                    Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.StaLineColor)
                Case "大站"
                    Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.FenStaLineStyle)
                    Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.FenStaLineWidth
                    Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.FenStaLineColor)
                Case "车场"
                    Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.CheChangStaLineStyle)
                    Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.CheChangStaLineWidth
                    Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.CheChangStaLineColor)
            End Select
        End If
        Call ShowTimeLinePrint()
    End Sub

    Private Sub cmbTrainStyle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTrainStyle.SelectedIndexChanged
        Dim i As Integer
        Select Case Me.cmbTrainStyle.SelectedItem.ToString
            Case "所有运行线"
                Me.lstTrainStyle.Items.Clear()
                Me.lstTrainStyle.Items.Add("所有运行线")
            Case "所有车底连接线"
                Me.lstTrainStyle.Items.Clear()
                Me.lstTrainStyle.Items.Add("所有车底连接线")
            Case "运行线按交路分类"
                Me.lstTrainStyle.Items.Clear()
                For i = 1 To UBound(BasicTrainInf)
                    If BasicTrainInf(i).sJiaoLuName <> "" Then
                        Me.lstTrainStyle.Items.Add(BasicTrainInf(i).sJiaoLuName)
                    End If
                Next i
            Case "运行线按运行标尺"
                Dim ifIn As Integer
                Dim j As Integer
                Me.lstTrainStyle.Items.Clear()
                For i = 1 To UBound(TrainInf)
                    ifIn = 0
                    If TrainInf(i).sRunScaleName <> "" Then
                        For j = 1 To Me.lstTrainStyle.Items.Count
                            If Me.lstTrainStyle.Items(j - 1) = TrainInf(i).sRunScaleName Then
                                ifIn = 1
                            End If
                        Next
                        If ifIn = 0 Then
                            Me.lstTrainStyle.Items.Add(TrainInf(i).sRunScaleName)
                        End If
                    End If
                Next i

            Case "运行线按线路编号"
                Dim ifIn As Integer
                Dim j As Integer
                Me.lstTrainStyle.Items.Clear()
                For i = 1 To UBound(TrainInf)
                    ifIn = 0
                    If TrainInf(i).sLineNum <> "" Then
                        For j = 1 To Me.lstTrainStyle.Items.Count
                            If Me.lstTrainStyle.Items(j - 1) = TrainInf(i).sLineNum Then
                                ifIn = 1
                            End If
                        Next
                        If ifIn = 0 Then
                            Me.lstTrainStyle.Items.Add(TrainInf(i).sLineNum)
                        End If
                    End If
                Next i

            Case "运行线按车底编号分类"
                Me.lstTrainStyle.Items.Clear()
                For i = 1 To UBound(ChediInfo)
                    If UBound(ChediInfo(i).nLinkTrain) > 0 Then
                        Me.lstTrainStyle.Items.Add(ChediInfo(i).sCheCiHao)
                    End If
                Next i
            Case "车底线按车底编号分类"
                Me.lstTrainStyle.Items.Clear()
                For i = 1 To UBound(ChediInfo)
                    If UBound(ChediInfo(i).nLinkTrain) > 0 Then
                        Me.lstTrainStyle.Items.Add(ChediInfo(i).sCheCiHao)
                    End If
                Next i
            Case "所有运行线分上下行"
                Me.lstTrainStyle.Items.Clear()
                Me.lstTrainStyle.Items.Add("下行线")
                Me.lstTrainStyle.Items.Add("上行线")
        End Select
    End Sub

    Private Sub lstTrainStyle_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTrainStyle.SelectedValueChanged
        Dim i, j As Integer
        Dim nState As Integer
        nState = 0
        Select Case Me.cmbTrainStyle.Text
            Case "所有运行线"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    Select Case Me.cmbTrainStyle.SelectedItem.ToString
                        Case "所有运行线"
                            Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.TrainLineStyle)
                            Me.numTrainLineWidth.Value = TimeTablePara.DiagramStylePara.TrainLineWidth
                            Me.labTrainLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TrainLineColor)
                    End Select
                End If
            Case "所有车底连接线"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    Select Case Me.cmbTrainStyle.SelectedItem.ToString
                        Case "所有车底连接线"
                            Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.CheDiLineStyle)
                            Me.numTrainLineWidth.Value = TimeTablePara.DiagramStylePara.CheDiLineWidth
                            Me.labTrainLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.CheDiLineColor)
                    End Select
                End If

            Case "运行线按交路分类"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If TrainInf(i).sJiaoLuName = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                                Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(GetLineStyleTextFromStyleName(TrainInf(i).PrintLineStyle))
                                Me.numTrainLineWidth.Value = TrainInf(i).PrintLineWidth
                                Me.labTrainLineColor.BackColor = TrainInf(i).PrintLineColor
                                Exit For
                            End If
                        End If
                    Next
                End If
            Case "运行线按运行标尺"

                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If TrainInf(i).sRunScaleName = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                                Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(GetLineStyleTextFromStyleName(TrainInf(i).PrintLineStyle))
                                Me.numTrainLineWidth.Value = TrainInf(i).PrintLineWidth
                                Me.labTrainLineColor.BackColor = TrainInf(i).PrintLineColor
                                Exit For
                            End If
                        End If
                    Next
                End If

            Case "运行线按线路编号"

                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If TrainInf(i).sLineNum = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                                Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(GetLineStyleTextFromStyleName(TrainInf(i).PrintLineStyle))
                                Me.numTrainLineWidth.Value = TrainInf(i).PrintLineWidth
                                Me.labTrainLineColor.BackColor = TrainInf(i).PrintLineColor
                                Exit For
                            End If
                        End If
                    Next
                End If

            Case "运行线按车底编号分类"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    nState = 0
                    For i = 1 To UBound(ChediInfo)
                        If ChediInfo(i).sCheCiHao = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                            For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                                If ChediInfo(i).nLinkTrain(j) > 0 Then
                                    Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(GetLineStyleTextFromStyleName(TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineStyle))
                                    Me.numTrainLineWidth.Value = TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineWidth
                                    Me.labTrainLineColor.BackColor = TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineColor
                                    nState = 1
                                    Exit For
                                End If
                            Next
                        End If
                        If nState = 1 Then Exit For
                    Next
                End If
            Case "车底线按车底编号分类"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(ChediInfo)
                        If ChediInfo(i).sCheCiHao = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                            Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(GetLineStyleTextFromStyleName(ChediInfo(i).PrintCheDiLinkStyle))
                            Me.numTrainLineWidth.Value = ChediInfo(i).PrintCheDiLinkWidth
                            Me.labTrainLineColor.BackColor = ChediInfo(i).PrintCheDiLinkColor
                            Exit For
                        End If
                    Next
                End If
            Case "所有运行线分上下行"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If Me.lstTrainStyle.SelectedItem.ToString.Trim = "上行线" Then
                                If i Mod 2 = 0 Then '上行车
                                    Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(GetLineStyleTextFromStyleName(TrainInf(i).PrintLineStyle))
                                    Me.numTrainLineWidth.Value = TrainInf(i).PrintLineWidth
                                    Me.labTrainLineColor.BackColor = TrainInf(i).PrintLineColor
                                    Exit For
                                End If
                            Else
                                If i Mod 2 <> 0 Then '下行车
                                    Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(GetLineStyleTextFromStyleName(TrainInf(i).PrintLineStyle))
                                    Me.numTrainLineWidth.Value = TrainInf(i).PrintLineWidth
                                    Me.labTrainLineColor.BackColor = TrainInf(i).PrintLineColor
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                End If
        End Select
        Call ListTrainLineView()
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
        Dim i, j As Integer
        Select Case Me.cmbTrainStyle.Text
            Case "所有运行线"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    Select Case Me.cmbTrainStyle.SelectedItem.ToString
                        Case "所有运行线"
                            TimeTablePara.DiagramStylePara.TrainLineStyle = GetLineTextNameFromStyle(Me.cmbTrainLineStyle.Text)
                            TimeTablePara.DiagramStylePara.TrainLineWidth = Me.numTrainLineWidth.Value
                            TimeTablePara.DiagramStylePara.TrainLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTrainLineColor.BackColor)
                            For i = 1 To UBound(TrainInf)
                                If TrainInf(i).Train <> "" Then
                                    TrainInf(i).PrintLineStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                                    TrainInf(i).PrintLineWidth = Me.numTrainLineWidth.Value
                                    TrainInf(i).PrintLineColor = Me.labTrainLineColor.BackColor
                                End If
                            Next
                    End Select
                End If
            Case "所有车底连接线"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    Select Case Me.cmbTrainStyle.SelectedItem.ToString
                        Case "所有车底连接线"
                            TimeTablePara.DiagramStylePara.CheDiLineStyle = GetLineTextNameFromStyle(Me.cmbTrainLineStyle.Text)
                            TimeTablePara.DiagramStylePara.CheDiLineWidth = Me.numTrainLineWidth.Value
                            TimeTablePara.DiagramStylePara.CheDiLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTrainLineColor.BackColor)
                            For i = 1 To UBound(ChediInfo)
                                ChediInfo(i).PrintCheDiLinkStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                                ChediInfo(i).PrintCheDiLinkWidth = Me.numTrainLineWidth.Value
                                ChediInfo(i).PrintCheDiLinkColor = Me.labTrainLineColor.BackColor
                            Next
                    End Select
                End If
            Case "运行线按交路分类"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If TrainInf(i).sJiaoLuName = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                                TrainInf(i).PrintLineStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                                TrainInf(i).PrintLineWidth = Me.numTrainLineWidth.Value
                                TrainInf(i).PrintLineColor = Me.labTrainLineColor.BackColor
                            End If
                        End If
                    Next
                End If
            Case "运行线按运行标尺"

                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If TrainInf(i).sRunScaleName = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                                TrainInf(i).PrintLineStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                                TrainInf(i).PrintLineWidth = Me.numTrainLineWidth.Value
                                TrainInf(i).PrintLineColor = Me.labTrainLineColor.BackColor
                            End If
                        End If
                    Next
                End If

            Case "运行线按线路编号"

                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If TrainInf(i).sLineNum = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                                TrainInf(i).PrintLineStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                                TrainInf(i).PrintLineWidth = Me.numTrainLineWidth.Value
                                TrainInf(i).PrintLineColor = Me.labTrainLineColor.BackColor
                            End If
                        End If
                    Next
                End If

            Case "运行线按车底编号分类"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(ChediInfo)
                        If ChediInfo(i).sCheCiHao = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                            For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                                If ChediInfo(i).nLinkTrain(j) > 0 Then
                                    TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                                    TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineWidth = Me.numTrainLineWidth.Value
                                    TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineColor = Me.labTrainLineColor.BackColor
                                End If
                            Next
                        End If
                    Next
                End If

            Case "车底线按车底编号分类"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(ChediInfo)
                        If ChediInfo(i).sCheCiHao = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                            ChediInfo(i).PrintCheDiLinkStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                            ChediInfo(i).PrintCheDiLinkWidth = Me.numTrainLineWidth.Value
                            ChediInfo(i).PrintCheDiLinkColor = Me.labTrainLineColor.BackColor
                        End If
                    Next
                End If
            Case "所有运行线分上下行"

                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If Me.lstTrainStyle.SelectedItem.ToString.Trim = "上行线" Then
                                If i Mod 2 = 0 Then '上行车
                                    TrainInf(i).PrintLineStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                                    TrainInf(i).PrintLineWidth = Me.numTrainLineWidth.Value
                                    TrainInf(i).PrintLineColor = Me.labTrainLineColor.BackColor
                                End If
                            Else
                                If i Mod 2 <> 0 Then '下行车
                                    TrainInf(i).PrintLineStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                                    TrainInf(i).PrintLineWidth = Me.numTrainLineWidth.Value
                                    TrainInf(i).PrintLineColor = Me.labTrainLineColor.BackColor
                                End If
                            End If
                        End If
                    Next
                End If

        End Select
    End Sub

    Private Sub btnSetTrainDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrainDefault.Click
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String

        cellName = "所有运行线线型"
        CellValue = TimeTablePara.DiagramStylePara.TrainLineStyle
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom.ExecuteNonQuery()

        cellName = "所有运行线线宽"
        CellValue = TimeTablePara.DiagramStylePara.TrainLineWidth
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom1.ExecuteNonQuery()

        cellName = "所有运行线颜色"
        CellValue = TimeTablePara.DiagramStylePara.TrainLineColor
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom2.ExecuteNonQuery()

        cellName = "所有车底连接线线型"
        CellValue = TimeTablePara.DiagramStylePara.CheDiLineStyle
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom3 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom3.ExecuteNonQuery()

        cellName = "所有车底连接线线宽"
        CellValue = TimeTablePara.DiagramStylePara.CheDiLineWidth
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom4 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom4.ExecuteNonQuery()

        cellName = "所有车底连接线颜色"
        CellValue = TimeTablePara.DiagramStylePara.CheDiLineColor
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom5 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom5.ExecuteNonQuery()

        cellName = "车次标号字体名称"
        CellValue = TimeTablePara.DiagramStylePara.CheCiFontName
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom02 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom02.ExecuteNonQuery()

        cellName = "车次标号字体大小"
        CellValue = TimeTablePara.DiagramStylePara.CheCiFontSize
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom03 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom03.ExecuteNonQuery()

        cellName = "车次标号字体粗体"
        CellValue = TimeTablePara.DiagramStylePara.CheCiFontBold
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom04 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom04.ExecuteNonQuery()

        cellName = "车次标号字体斜体"
        CellValue = TimeTablePara.DiagramStylePara.CheCiFontItalic
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom05 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom05.ExecuteNonQuery()

        cellName = "车次标号字体颜色"
        CellValue = TimeTablePara.DiagramStylePara.CheCiFontColor
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom06 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom06.ExecuteNonQuery()

        cellName = "斜向车次字体名称"
        CellValue = TimeTablePara.DiagramStylePara.XieCheCiFontName
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom07 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom07.ExecuteNonQuery()

        cellName = "斜向车次字体大小"
        CellValue = TimeTablePara.DiagramStylePara.XieCheCiFontSize
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom08 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom08.ExecuteNonQuery()

        cellName = "斜向车次字体粗体"
        CellValue = TimeTablePara.DiagramStylePara.XieCheCiFontBold
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom09 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom09.ExecuteNonQuery()

        cellName = "斜向车次字体斜体"
        CellValue = TimeTablePara.DiagramStylePara.XieCheCiFontItalic
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom010 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom010.ExecuteNonQuery()

        cellName = "斜向车次字体颜色"
        CellValue = TimeTablePara.DiagramStylePara.XieCheCiFontColor
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom011 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom011.ExecuteNonQuery()

        MyConn.Close()
        MsgBox("已经成功设置!", , "提示")

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
                Case "车次标号"
                    f = New Font(TimeTablePara.DiagramStylePara.CheCiFontName, TimeTablePara.DiagramStylePara.CheCiFontSize)
                    If TimeTablePara.DiagramStylePara.CheCiFontBold = True Then
                        If TimeTablePara.DiagramStylePara.CheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If TimeTablePara.DiagramStylePara.CheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If
                    nd.Font = f
                    If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TimeTablePara.DiagramStylePara.CheCiFontName = nd.Font.Name
                        TimeTablePara.DiagramStylePara.CheCiFontSize = nd.Font.Size
                        TimeTablePara.DiagramStylePara.CheCiFontBold = nd.Font.Bold
                        TimeTablePara.DiagramStylePara.CheCiFontItalic = nd.Font.Italic
                    End If

                Case "斜向车次"
                    f = New Font(TimeTablePara.DiagramStylePara.XieCheCiFontName, TimeTablePara.DiagramStylePara.XieCheCiFontSize)
                    If TimeTablePara.DiagramStylePara.XieCheCiFontBold = True Then
                        If TimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If TimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If
                    nd.Font = f
                    If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TimeTablePara.DiagramStylePara.XieCheCiFontName = nd.Font.Name
                        TimeTablePara.DiagramStylePara.XieCheCiFontSize = nd.Font.Size
                        TimeTablePara.DiagramStylePara.XieCheCiFontBold = nd.Font.Bold
                        TimeTablePara.DiagramStylePara.XieCheCiFontItalic = nd.Font.Italic
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
                Case "车次标号"
                    f = New Font(TimeTablePara.DiagramStylePara.CheCiFontName, TimeTablePara.DiagramStylePara.CheCiFontSize)
                    If TimeTablePara.DiagramStylePara.CheCiFontBold = True Then
                        If TimeTablePara.DiagramStylePara.CheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If TimeTablePara.DiagramStylePara.CheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If

                    newBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.CheCiFontColor))
                    g.DrawString("这是预览效果012345aAbBcC", f, newBrush, 10, 10)
                    Me.labCheCiColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.CheCiFontColor)

                Case "斜向车次"
                    f = New Font(TimeTablePara.DiagramStylePara.XieCheCiFontName, TimeTablePara.DiagramStylePara.XieCheCiFontSize)
                    If TimeTablePara.DiagramStylePara.XieCheCiFontBold = True Then
                        If TimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If TimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If

                    newBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.XieCheCiFontColor))
                    g.DrawString("这是预览效果012345aAbBcC", f, newBrush, 10, 10)
                    Me.labCheCiColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.XieCheCiFontColor)
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
                    Case "车次标号"
                        TimeTablePara.DiagramStylePara.CheCiFontColor = System.Drawing.ColorTranslator.ToHtml(Me.labCheCiColor.BackColor)
                    Case "斜向车次"
                        TimeTablePara.DiagramStylePara.XieCheCiFontColor = System.Drawing.ColorTranslator.ToHtml(Me.labCheCiColor.BackColor)
                End Select
            End If
            Call ShowCheCiFontPreView()
        End If
    End Sub

    Private Sub btnSetTrainLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrainLine.Click
        Call SetTrainLineStyle()
        Call RefreshDiagram(1)
    End Sub
End Class