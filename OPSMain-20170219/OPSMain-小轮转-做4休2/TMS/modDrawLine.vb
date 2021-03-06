
Module modDrawLine

    Structure typeCheDiLinkLine
        Dim nFirTrain As Integer
        Dim nSecTrain As Integer
        Dim nCheDiId As Integer
        Dim LineHeight As Integer
        Dim X1 As Single
        Dim Y1 As Single
        Dim X2 As Single
        Dim Y2 As Single
        Dim DrawString As String
        Dim StringX As Single
        Dim StringY As Single
        Dim nIFPrint As Boolean
        Dim tmpPen As Pen
        Dim tmpFont As Font
        Dim tmpBrush As Brush
        Dim upOrDown As Integer
        Dim sStyle As Integer '类型,0表示矩形，1表示左上，2表示右下，3表示左下，4表示右上
    End Structure
    Public CheDiLinkinf() As typeCheDiLinkLine


    Structure typeGuDaoJiShuTuJie
        Dim nSta As Integer
        Dim sGuDao() As String
        Dim nUpSta() As Integer
        Dim UpStaYcoord() As Single
        Dim DownStaYcoord() As Single
        Dim GuDaoYcoord() As Single
        Dim nDownSta() As Integer
        Dim nLine As Integer
        Dim sCurSeleteState As String '当前选中的状态，中间停站/始发折返
        Dim CurSelectedLineX1 As Single '修改股道时的X，Y坐标
        Dim CurSelectedLineY1 As Single
        Dim CurSelectedLineX2 As Single
        Dim CurSelectedLineY2 As Single

        'gujianghe begin
        Dim nUpSwitches() As String '上行道岔号码
        Dim nUpSwitchesYCoord() As Single
        Dim nDownSwitches() As String '下行道岔号码
        Dim nDownSwitchesYCoord() As Single
        Dim nTotleY As Long '最高Y值
        'gujianghe end

    End Structure
    Public GuDaoJishutujie As typeGuDaoJiShuTuJie

    Structure typeCDJLPrintPlace
        Dim sngX1 As Single
        Dim sngY1 As Single
        Dim sngX2 As Single
        Dim sngY2 As Single
    End Structure
    Public CDJLPrintPlace() As typeCDJLPrintPlace


    '文字居中对齐
    Public Function FindStartPoint(ByVal g As Graphics, ByVal str As String, ByVal point1 As Point, ByVal y1 As Single, ByVal y2 As Single) As Point
        Dim StrtWidth, StrHeight As Single
        StrtWidth = g.MeasureString(str, New Font("宋体", 10)).Width
        StrHeight = g.MeasureString(str, New Font("宋体", 10)).Height
        Dim StartPoint As New Point
        StartPoint.X = point1.X + 0.5 * (90 - StrtWidth)
        StartPoint.Y = point1.Y + 0.5 * (y2 - y1 - StrHeight)
        Return StartPoint
    End Function

    Public Function FindWH(ByVal g As Graphics, ByRef str As String, ByRef Fn As Font) As Generic.List(Of Single)
        Dim WH As New Generic.List(Of Single)
        WH.Add(g.MeasureString(str, Fn).Width)
        WH.Add(g.MeasureString(str, Fn).Height)
        Return WH
    End Function

    '打印车站和时间线
    Public Sub DrawTimeLine(ByVal rBmpGraphics As Graphics, ByVal rbmpStaGraphics As Graphics, ByVal rbmpStaGraphics2 As Graphics, ByVal PicWidth As Single, ByVal PicHeight As Single, _
                                        ByVal LeftBlank As Single, ByVal topBlank As Single, ByVal StaBlank As Single, _
                                        ByVal sngTimeBlank As Single, ByVal sngLeftX As Single, ByVal sngTopY As Single, _
                                        ByVal nFirTime As Integer, ByVal nTimeWidth As Integer, ByVal DiagramTimeFormate As String, ByVal DiagramTimeLineFont As String, ByVal nPrintSta As Integer, ByVal bIfPrintEverySta As Boolean, ByVal bifPrintLanTu As Boolean)


        Dim i, j As Integer
        Dim intTimeLine As Single
        Dim sngMinuWidth As Single
        Dim sngToWidth As Single
        Dim sngHeight As Single
        Dim intFirstTime As Integer
        Dim curPen As Pen
        intFirstTime = nFirTime
        Dim IfPrintStation As Integer
        IfPrintStation = 1
        sngToWidth = PicWidth - LeftBlank * 2 - StaBlank * IfPrintStation - sngLeftX
        sngHeight = PicHeight - 2 * topBlank
        Dim f As Font
        Dim nB As Brush
        Dim StaLinePen As Pen
        Dim txtWidth As Single
        Dim txtHeight As Single
        Dim fSta As Font

        '#####################'车站名称picStation上#######################
        If UBound(StationInf) > 0 Then
            Dim maxY As Single
            Dim tmpY1 As Single
            Dim tmpY2 As Single
            Dim tmpY As Single
            Dim YBix As Single
            Dim brsStation As Brush
            ' Dim clrStation As Color

            If DiagramTimeLineFont = "全部" Then
                If PicWidth <= 3000 And nTimeWidth >= 6 Then
                    DiagramTimeLineFont = ""
                Else
                    DiagramTimeLineFont = "全部"
                End If
            End If

            tmpY1 = StationInf(1).Ycord
            maxY = 0
            For i = 1 To UBound(StationInf)
                If StationInf(i).Ycord > maxY Then
                    maxY = StationInf(i).Ycord
                End If
            Next
            If maxY - tmpY1 > 0 Then
                YBix = sngHeight / (maxY - tmpY1)

                '第一个车站的横线
                StaLinePen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.StaLineColor), TimeTablePara.DiagramStylePara.StaLineWidth)
                StaLinePen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.StaLineStyle)
                rBmpGraphics.DrawLine(StaLinePen, LeftBlank + StaBlank * IfPrintStation + sngLeftX, topBlank + sngTopY, LeftBlank + sngToWidth + StaBlank * IfPrintStation + sngLeftX, topBlank + sngTopY)
                f = New Font(TimeTablePara.DiagramStylePara.StaNameFontName, TimeTablePara.DiagramStylePara.StaNameFontSize)
                fSta = New Font(TimeTablePara.DiagramStylePara.StaNameFontName, TimeTablePara.DiagramStylePara.StaNameFontSize)
                If TimeTablePara.DiagramStylePara.StaNameFontBold = True Then
                    If TimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                        f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        fSta = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                    Else
                        f = New Font(f, FontStyle.Bold)
                        fSta = New Font(f, FontStyle.Bold)
                    End If
                Else
                    If TimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                        f = New Font(f, FontStyle.Italic)
                        fSta = New Font(f, FontStyle.Italic)
                    End If
                End If
                nB = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.StaNameFontColor))
                txtWidth = rBmpGraphics.MeasureString(StationInf(1).sPrintStaName, f).Width
                If nPrintSta = 1 Then
                    rBmpGraphics.DrawString(StationInf(1).sPrintStaName, f, nB, LeftBlank + sngLeftX + StaBlank - txtWidth, topBlank - 6 + sngTopY)
                End If
                If rbmpStaGraphics Is Nothing Then
                Else
                    rbmpStaGraphics.DrawString(StationInf(1).sPrintStaName, f, nB, 2, topBlank - 6 + sngTopY)
                End If
                If rbmpStaGraphics2 Is Nothing Then
                Else
                    rbmpStaGraphics2.DrawString(StationInf(1).sPrintStaName, f, nB, TimeTablePara.picPubStation2.Width - txtWidth + 2, topBlank - 6 + sngTopY)
                End If
                StationInf(1).YPicValue = topBlank + sngTopY
                For i = 1 To UBound(StationInf)
                    tmpY2 = StationInf(i).Ycord
                    tmpY = (tmpY2 - tmpY1) * YBix + topBlank + sngTopY

                    StaLinePen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.StaLineColor), TimeTablePara.DiagramStylePara.StaLineWidth)
                    StaLinePen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.StaLineStyle)
                    If StationInf(i).sStaStyle = "车场" Or StationInf(i).sStaStyle = "出入车场信号机" Or StationInf(i).sStaStyle = "非作业站" Then
                        StaLinePen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.CheChangStaLineColor), TimeTablePara.DiagramStylePara.CheChangStaLineWidth)
                        StaLinePen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.CheChangStaLineStyle)
                    End If

                    If StationInf(i).sStaStyle = "大站" Then
                        StaLinePen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.FenStaLineColor), TimeTablePara.DiagramStylePara.FenStaLineWidth)
                        StaLinePen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.FenStaLineStyle)
                    End If


                    rBmpGraphics.DrawLine(StaLinePen, LeftBlank + StaBlank * IfPrintStation + sngLeftX, tmpY, LeftBlank + StaBlank * IfPrintStation + sngLeftX + sngToWidth, tmpY)
                    txtWidth = rBmpGraphics.MeasureString(StationInf(i).sPrintStaName, f).Width
                    brsStation = Brushes.Green
                    If nPrintSta = 1 Then
                        rBmpGraphics.DrawString(StationInf(i).sPrintStaName, f, nB, LeftBlank + sngLeftX + StaBlank - txtWidth, tmpY - 6)
                    End If
                    If rbmpStaGraphics Is Nothing Then
                    Else
                        rbmpStaGraphics.DrawString(StationInf(i).sPrintStaName, f, nB, 2, tmpY - 6)
                    End If

                    If rbmpStaGraphics2 Is Nothing Then
                    Else
                        rbmpStaGraphics2.DrawString(StationInf(i).sPrintStaName, f, nB, TimeTablePara.picPubStation2.Width - txtWidth + 2, tmpY - 6)
                    End If

                    StationInf(i).YPicValue = tmpY
                Next i

                Dim strTimePrint As String
                Dim strPrintString As String
                'Dim tmpPen As Pen
                Dim k As Integer
                Dim sngY1, sngY2 As Single
                Dim f1 As Font
                f = New Font(TimeTablePara.DiagramStylePara.TimeFontName, TimeTablePara.DiagramStylePara.TimeFontSize)
                f1 = New Font(TimeTablePara.DiagramStylePara.TimeFontName, TimeTablePara.DiagramStylePara.TimeFontSize * 1.5)
                If TimeTablePara.DiagramStylePara.TimeFontBold = True Then
                    If TimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                        f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        f1 = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                    Else
                        f = New Font(f, FontStyle.Bold)
                        f1 = New Font(f, FontStyle.Bold)
                    End If
                Else
                    If TimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                        f = New Font(f, FontStyle.Italic)
                        f1 = New Font(f, FontStyle.Italic)
                    End If
                End If
                nB = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TimeFontColor))

                For k = 1 To UBound(SectionInf)
                    sngY1 = StationInf(SectionInf(k).nFirStaID).YPicValue
                    sngY2 = StationInf(SectionInf(k).nSecStaID).YPicValue
                    Select Case DiagramTimeFormate
                        Case "小时格"
                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.HourTime60LineColor), TimeTablePara.DiagramStylePara.HourTime60LineWidth)
                            curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.HourTime60LineStyle)
                            intTimeLine = nTimeWidth  '10 * 6 * 24
                            sngMinuWidth = sngToWidth / (intTimeLine)
                            For i = 1 To intTimeLine + 1
                                rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                strTimePrint = Trim(Str((intFirstTime + i - 1) Mod 24) & ":00")
                                '打印时间文字,第一行
                                txtWidth = rBmpGraphics.MeasureString(strTimePrint, f1).Width / 2
                                rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                '最后一行
                                rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                If bIfPrintEverySta = True Then '每小时打印车站
                                    If i > 1 And i < intTimeLine + 1 Then
                                        For j = 1 To UBound(StationInf)
                                            txtWidth = rBmpGraphics.MeasureString(StationInf(j).sPrintStaName, fSta).Width / 2
                                            txtHeight = rBmpGraphics.MeasureString(StationInf(j).sPrintStaName, fSta).Height / 2
                                            rBmpGraphics.DrawString(StationInf(j).sPrintStaName, fSta, Brushes.DarkGray, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, StationInf(j).YPicValue - txtHeight)
                                        Next
                                    End If
                                End If
                                strPrintString = ""
                                If bifPrintLanTu = True Then
                                    If strTimePrint = "5:00" Then
                                        strPrintString = "____________年_______月_______日   班次________ 姓名_________________________"
                                    ElseIf strTimePrint = "8:00" Or strTimePrint = "17:00" Then
                                        strPrintString = "班次________ 姓名_________________________"
                                    End If
                                    rBmpGraphics.DrawString(strPrintString, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, 40)
                                End If
                            Next

                        Case "十分格"
                            intTimeLine = 6 * nTimeWidth
                            sngMinuWidth = sngToWidth / (intTimeLine)
                            For i = 1 To intTimeLine + 1
                                If (i - 1) Mod 6 = 0 Then
                                    curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TenTime60LineColor), TimeTablePara.DiagramStylePara.TenTime60LineWidth)
                                    curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.TenTime60LineStyle)
                                    rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                    strTimePrint = Trim(Str((intFirstTime + (Int((i - 1) / 6))) Mod 24) & ":00")
                                    '打印时间文字,第一行
                                    txtWidth = rBmpGraphics.MeasureString(strTimePrint, f1).Width / 2
                                    txtHeight = rBmpGraphics.MeasureString(strTimePrint, f1).Height

                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - txtHeight + 8)
                                    '最后一行
                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 2)
                                    If bIfPrintEverySta = True Then '每小时打印车站
                                        If i > 1 And i < intTimeLine + 1 Then
                                            For j = 1 To UBound(StationInf)
                                                txtWidth = rBmpGraphics.MeasureString(StationInf(j).sPrintStaName, fSta).Width / 2
                                                txtHeight = rBmpGraphics.MeasureString(StationInf(j).sPrintStaName, fSta).Height / 2
                                                rBmpGraphics.DrawString(StationInf(j).sPrintStaName, fSta, Brushes.DarkGray, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, StationInf(j).YPicValue - txtHeight)
                                            Next
                                        End If
                                    End If
                                    strPrintString = ""
                                    If bifPrintLanTu = True Then
                                        If strTimePrint = "5:00" Then
                                            strPrintString = "____________年_______月_______日   班次________ 姓名_________________________"
                                        ElseIf strTimePrint = "8:00" Or strTimePrint = "17:00" Then
                                            strPrintString = "班次________ 姓名_________________________"
                                        End If
                                        rBmpGraphics.DrawString(strPrintString, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, 40)
                                    End If
                                Else
                                    If (i - 1) Mod 3 = 0 Then
                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TenTime30LineColor), TimeTablePara.DiagramStylePara.TenTime30LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.TenTime30LineStyle)
                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)

                                        'If strTimePrint = "8:50" Then Stop
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)

                                        If DiagramTimeLineFont = "全部" Or DiagramTimeLineFont = "标注半小时格" Then
                                            strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2

                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If

                                    Else
                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TenTime10LineColor), TimeTablePara.DiagramStylePara.TenTime10LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.TenTime10LineStyle)
                                        rBmpGraphics.DrawLine(curPen, LeftBlank + StaBlank * IfPrintStation + sngLeftX + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        '打印时间文字,第一行
                                        'If strTimePrint = "8:50" Then Stop
                                        If DiagramTimeLineFont = "全部" Then
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If
                                    End If
                                End If
                            Next
                        Case "二分格"
                            intTimeLine = 6 * nTimeWidth
                            sngMinuWidth = sngToWidth / (intTimeLine)
                            For i = 1 To intTimeLine + 1
                                If (i - 1) Mod 6 = 0 Then
                                    curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime60LineColor), TimeTablePara.DiagramStylePara.TwoTime60LineWidth)
                                    curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.TwoTime60LineStyle)
                                    rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                    strTimePrint = Trim(Str((intFirstTime + (Int((i - 1) / 6))) Mod 24) & ":00")
                                    '打印时间文字,第一行
                                    txtWidth = rBmpGraphics.MeasureString(strTimePrint, f1).Width / 2
                                    txtHeight = rBmpGraphics.MeasureString(strTimePrint, f1).Height

                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - txtHeight + 8)
                                    '最后一行
                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 2)
                                    If bIfPrintEverySta = True Then '每小时打印车站
                                        If i > 1 And i < intTimeLine + 1 Then
                                            For j = 1 To UBound(StationInf)
                                                txtWidth = rBmpGraphics.MeasureString(StationInf(j).sPrintStaName, fSta).Width / 2
                                                txtHeight = rBmpGraphics.MeasureString(StationInf(j).sPrintStaName, fSta).Height / 2
                                                rBmpGraphics.DrawString(StationInf(j).sPrintStaName, fSta, Brushes.DarkGray, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, StationInf(j).YPicValue - txtHeight)
                                            Next
                                        End If
                                    End If
                                    strPrintString = ""
                                    If bifPrintLanTu = True Then
                                        If strTimePrint = "5:00" Then
                                            strPrintString = "____________年_______月_______日   班次________ 姓名_________________________"
                                        ElseIf strTimePrint = "8:00" Or strTimePrint = "17:00" Then
                                            strPrintString = "班次________ 姓名_________________________"
                                        End If
                                        rBmpGraphics.DrawString(strPrintString, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, 40)
                                    End If
                                Else
                                    If (i - 1) Mod 3 = 0 Then

                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime30LineColor), TimeTablePara.DiagramStylePara.TwoTime30LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.TwoTime30LineStyle)
                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)

                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        If DiagramTimeLineFont = "全部" Or DiagramTimeLineFont = "标注半小时格" Then
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)

                                        End If

                                    Else
                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime10LineColor), TimeTablePara.DiagramStylePara.TwoTime10LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.TwoTime10LineStyle)
                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        If DiagramTimeLineFont = "全部" Then
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If
                                    End If
                                End If
                                If i <= intTimeLine Then
                                    For j = 1 To 4
                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime2LineColor), TimeTablePara.DiagramStylePara.TwoTime2LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.TwoTime2LineStyle)
                                        rBmpGraphics.DrawLine(curPen, LeftBlank + StaBlank * IfPrintStation + sngLeftX + (i - 1) * sngMinuWidth + j * sngMinuWidth / 5, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / 5, sngY2)
                                    Next j
                                End If
                            Next i
                        Case "一分格"
                            intTimeLine = 6 * nTimeWidth
                            sngMinuWidth = sngToWidth / (intTimeLine)
                            For i = 1 To intTimeLine + 1
                                If (i - 1) Mod 6 = 0 Then
                                    curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime60LineColor), TimeTablePara.DiagramStylePara.OneTime60LineWidth)
                                    curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime60LineStyle)
                                    rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                    strTimePrint = Trim(Str((intFirstTime + (Int((i - 1) / 6))) Mod 24) & ":00")
                                    '打印时间文字,第一行
                                    txtWidth = rBmpGraphics.MeasureString(strTimePrint, f1).Width / 2
                                    txtHeight = rBmpGraphics.MeasureString(strTimePrint, f1).Height

                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - txtHeight + 8)
                                    '最后一行
                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 2)
                                    If bIfPrintEverySta = True Then '每小时打印车站
                                        If i > 1 And i < intTimeLine + 1 Then
                                            For j = 1 To UBound(StationInf)
                                                txtWidth = rBmpGraphics.MeasureString(StationInf(j).sPrintStaName, fSta).Width / 2
                                                txtHeight = rBmpGraphics.MeasureString(StationInf(j).sPrintStaName, fSta).Height / 2
                                                rBmpGraphics.DrawString(StationInf(j).sPrintStaName, fSta, Brushes.DarkGray, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, StationInf(j).YPicValue - txtHeight)
                                            Next
                                        End If
                                    End If
                                    strPrintString = ""
                                    If bifPrintLanTu = True Then
                                        If strTimePrint = "5:00" Then
                                            strPrintString = "____________年_______月_______日   班次________ 姓名_________________________"
                                        ElseIf strTimePrint = "8:00" Or strTimePrint = "17:00" Then
                                            strPrintString = "班次________ 姓名_________________________"
                                        End If
                                        rBmpGraphics.DrawString(strPrintString, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, 40)
                                    End If
                                Else
                                    If (i - 1) Mod 3 = 0 Then

                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime30LineColor), TimeTablePara.DiagramStylePara.OneTime30LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime30LineStyle)

                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)

                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        If DiagramTimeLineFont = "全部" Or DiagramTimeLineFont = "标注半小时格" Then
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If

                                    Else

                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime10LineColor), TimeTablePara.DiagramStylePara.OneTime10LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime10LineStyle)
                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        If DiagramTimeLineFont = "全部" Then
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If
                                    End If
                                End If
                                If i <= intTimeLine Then
                                    For j = 1 To 9
                                        If j = 5 Then
                                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime5LineColor), TimeTablePara.DiagramStylePara.OneTime5LineWidth)
                                            curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime5LineStyle)
                                            rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / 10, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / 10, sngY2)
                                        Else
                                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime1LineColor), TimeTablePara.DiagramStylePara.OneTime1LineWidth)
                                            curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime1LineStyle)
                                            rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / 10, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / 10, sngY2)
                                        End If
                                    Next j
                                End If
                            Next i
                        Case "30秒格"
                            intTimeLine = 6 * nTimeWidth
                            sngMinuWidth = sngToWidth / (intTimeLine)
                            For i = 1 To intTimeLine + 1
                                If (i - 1) Mod 6 = 0 Then
                                    curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime60LineColor), TimeTablePara.DiagramStylePara.OneTime60LineWidth)
                                    curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime60LineStyle)
                                    rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                    strTimePrint = Trim(Str((intFirstTime + (Int((i - 1) / 6))) Mod 24) & ":00")
                                    '打印时间文字,第一行
                                    txtWidth = rBmpGraphics.MeasureString(strTimePrint, f1).Width / 2
                                    txtHeight = rBmpGraphics.MeasureString(strTimePrint, f1).Height

                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - txtHeight + 8)
                                    '最后一行
                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 2)
                                    If bIfPrintEverySta = True Then '每小时打印车站
                                        If i > 1 And i < intTimeLine + 1 Then
                                            For j = 1 To UBound(StationInf)
                                                txtWidth = rBmpGraphics.MeasureString(StationInf(j).sPrintStaName, fSta).Width / 2
                                                txtHeight = rBmpGraphics.MeasureString(StationInf(j).sPrintStaName, fSta).Height / 2
                                                rBmpGraphics.DrawString(StationInf(j).sPrintStaName, fSta, Brushes.DarkGray, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, StationInf(j).YPicValue - txtHeight)
                                            Next
                                        End If
                                    End If
                                    strPrintString = ""
                                    If bifPrintLanTu = True Then
                                        If strTimePrint = "5:00" Then
                                            strPrintString = "____________年_______月_______日   班次________ 姓名_________________________"
                                        ElseIf strTimePrint = "8:00" Or strTimePrint = "17:00" Then
                                            strPrintString = "班次________ 姓名_________________________"
                                        End If
                                        rBmpGraphics.DrawString(strPrintString, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, 40)
                                    End If
                                Else
                                    If (i - 1) Mod 3 = 0 Then

                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime30LineColor), TimeTablePara.DiagramStylePara.OneTime30LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime30LineStyle)

                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)

                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        If DiagramTimeLineFont = "全部" Or DiagramTimeLineFont = "标注半小时格" Then
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If

                                    Else

                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime10LineColor), TimeTablePara.DiagramStylePara.OneTime10LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime10LineStyle)
                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        If DiagramTimeLineFont = "全部" Then
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If
                                    End If
                                End If
                                If i <= intTimeLine Then
                                    Dim tmpNum As Integer
                                    tmpNum = 20
                                    For j = 1 To tmpNum - 1
                                        If j Mod 2 = 0 Then
                                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime5LineColor), TimeTablePara.DiagramStylePara.OneTime5LineWidth)
                                            curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime5LineStyle)
                                            rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / tmpNum, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / tmpNum, sngY2)
                                        Else
                                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime1LineColor), TimeTablePara.DiagramStylePara.OneTime1LineWidth)
                                            curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime1LineStyle)
                                            rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / tmpNum, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / tmpNum, sngY2)
                                        End If
                                    Next j
                                End If
                            Next i
                        Case "15秒格"
                            intTimeLine = 6 * nTimeWidth
                            sngMinuWidth = sngToWidth / (intTimeLine)
                            For i = 1 To intTimeLine + 1
                                If (i - 1) Mod 6 = 0 Then
                                    curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime60LineColor), TimeTablePara.DiagramStylePara.OneTime60LineWidth)
                                    curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime60LineStyle)
                                    rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                    strTimePrint = Trim(Str((intFirstTime + (Int((i - 1) / 6))) Mod 24) & ":00")
                                    '打印时间文字,第一行
                                    txtWidth = rBmpGraphics.MeasureString(strTimePrint, f1).Width / 2
                                    txtHeight = rBmpGraphics.MeasureString(strTimePrint, f1).Height

                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - txtHeight + 8)
                                    '最后一行
                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 2)
                                    If bIfPrintEverySta = True Then '每小时打印车站
                                        If i > 1 And i < intTimeLine + 1 Then
                                            For j = 1 To UBound(StationInf)
                                                txtWidth = rBmpGraphics.MeasureString(StationInf(j).sPrintStaName, fSta).Width / 2
                                                txtHeight = rBmpGraphics.MeasureString(StationInf(j).sPrintStaName, fSta).Height / 2
                                                rBmpGraphics.DrawString(StationInf(j).sPrintStaName, fSta, Brushes.DarkGray, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, StationInf(j).YPicValue - txtHeight)
                                            Next
                                        End If
                                    End If
                                    strPrintString = ""
                                    If bifPrintLanTu = True Then
                                        If strTimePrint = "5:00" Then
                                            strPrintString = "____________年_______月_______日   班次________ 姓名_________________________"
                                        ElseIf strTimePrint = "8:00" Or strTimePrint = "17:00" Then
                                            strPrintString = "班次________ 姓名_________________________"
                                        End If
                                        rBmpGraphics.DrawString(strPrintString, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, 40)
                                    End If
                                Else
                                    If (i - 1) Mod 3 = 0 Then

                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime30LineColor), TimeTablePara.DiagramStylePara.OneTime30LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime30LineStyle)

                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)

                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        If DiagramTimeLineFont = "全部" Or DiagramTimeLineFont = "标注半小时格" Then
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If

                                    Else

                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime10LineColor), TimeTablePara.DiagramStylePara.OneTime10LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime10LineStyle)
                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        If DiagramTimeLineFont = "全部" Then
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If
                                    End If
                                End If
                                If i <= intTimeLine Then
                                    Dim tmpNum As Integer
                                    tmpNum = 40
                                    For j = 1 To tmpNum - 1
                                        If j Mod 4 = 0 Then
                                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime5LineColor), TimeTablePara.DiagramStylePara.OneTime5LineWidth)
                                            curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime5LineStyle)
                                            rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / tmpNum, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / tmpNum, sngY2)
                                        Else
                                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime1LineColor), TimeTablePara.DiagramStylePara.OneTime1LineWidth)
                                            curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime1LineStyle)
                                            rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / tmpNum, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / tmpNum, sngY2)
                                        End If
                                    Next j
                                End If
                            Next i
                    End Select
                Next
            End If
        End If
    End Sub

    '画运行线
    Public Sub DrawDiagramLine(ByVal rBmpGraphics As Graphics, ByVal intToWidth As Integer, ByVal intFirTime As Integer, _
                                              ByVal nTimeWidth As Integer, ByVal intLeftBlank As Integer, ByVal intStaBlank As Integer, _
                                              ByVal intLeftX As Integer, ByVal nifPrintCheCi As Boolean, ByVal nIfPrintXieCheCi As Boolean, ByVal sShowJLLineStyle As String, _
                                              ByVal bIFShowFixChediLogo As Boolean, ByVal nChediLineHeight As Integer)
        ' nShowJLLineStyle = 0 '三角形   =1直角形
        ' nShowJLLineStyle = 0 '三角形   =1直角形

        Dim i As Integer

        Dim tmpPen As Pen
        '  tmpPen = New Pen(Color.Red, 1)
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                tmpPen = New Pen(TrainInf(i).PrintLineColor, TrainInf(i).PrintLineWidth)
                tmpPen.DashStyle = TrainInf(i).PrintLineStyle
                Call TMSDrawLineInPicture(i, rBmpGraphics, tmpPen, intToWidth, intFirTime, nTimeWidth, intLeftBlank, intStaBlank, intLeftX, nifPrintCheCi, nIfPrintXieCheCi)
            End If
        Next

        Dim sngLeftX As Single
        Dim sngRightX As Single
        sngLeftX = FormTimeToXCord(intFirTime * 3600, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        sngRightX = FormTimeToXCord(TimeAdd(intFirTime * 3600, nTimeWidth * 3600), intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        If sngRightX = sngLeftX Then
            sngRightX = sngLeftX + intToWidth - 2 * intLeftBlank - intStaBlank - intLeftX
        End If

        'If ODSPubpara.sCurShowListState <> "显示换乘站图" Then
        Call DrawCheDiJiaoLuLine(rBmpGraphics, intToWidth, intFirTime, nTimeWidth, intLeftBlank, intStaBlank, intLeftX, nifPrintCheCi, nIfPrintXieCheCi, sShowJLLineStyle, bIFShowFixChediLogo, nChediLineHeight)
        'End If

    End Sub
    '画车底交路图
    Public Sub DrawCheDiJiaoLuLine(ByVal rBmpGraphics As Graphics, ByVal intToWidth As Integer, ByVal intFirTime As Integer, _
                                              ByVal nTimeWidth As Integer, ByVal intLeftBlank As Integer, ByVal intStaBlank As Integer, _
                                              ByVal intLeftX As Integer, ByVal nifPrintCheCi As Boolean, ByVal nIfPrintXieCheCi As Boolean, ByVal sShowJLLineStyle As String, _
                                              ByVal bIFShowFixChediLogo As Boolean, ByVal nChediLineHeight As Integer)

        Dim i As Integer
        Dim j As Integer
        'Dim k As Integer
        Dim X1, X2 As Single
        Dim Y1, Y2 As Single
        Dim intTime1 As Integer
        Dim intTime2 As Integer
        Dim tmpPen As Pen
        Dim StepHeight As Single
        StepHeight = 8

        Dim sngLeftX As Single
        Dim sngRightX As Single
        sngLeftX = FormTimeToXCord(intFirTime * 3600, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        sngRightX = FormTimeToXCord(TimeAdd(intFirTime * 3600, nTimeWidth * 3600), intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        If sngRightX = sngLeftX Then
            sngRightX = sngLeftX + intToWidth - 2 * intLeftBlank - intStaBlank - intLeftX
        End If

        '画车底交路图
        ReDim CheDiLinkinf(0)
        Dim nTrain1 As Integer
        Dim nTrain2 As Integer
        Dim sSta As String
        Dim MidX As Single
        tmpPen = New Pen(Color.Red, 1)
        Dim f As Font
        Dim tmpBrush As Brush
        Dim nFirSta As Integer
        Dim nEndSta As Integer
        Dim sShowStyle As String
        sShowStyle = sShowJLLineStyle
        Dim ntmpHeight As Single
        Dim tmpTextWidth As Single
        Dim nNum As Integer
        Dim nLinkHeight As Single
        nLinkHeight = 10
        For i = 1 To UBound(ChediInfo)
            If UBound(ChediInfo(i).nLinkTrain) > 0 Then
                If sShowStyle = "三角形" Then
                    ntmpHeight = nChediLineHeight
                Else
                    ntmpHeight = 5 + nNum * 1.5
                End If

                tmpPen = New Pen(ChediInfo(i).PrintCheDiLinkColor, ChediInfo(i).PrintCheDiLinkWidth)
                tmpPen.DashStyle = ChediInfo(i).PrintCheDiLinkStyle
                'tmpPen = New Pen(Color.Red, 2)

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
                tmpBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.CheCiFontColor))
                Dim tmpEndLogo As Pen
                tmpEndLogo = New Pen(Color.Blue, 1)

                If UBound(ChediInfo(i).nLinkTrain) = 0 Then

                ElseIf UBound(ChediInfo(i).nLinkTrain) = 1 Then
                    nTrain1 = ChediInfo(i).nLinkTrain(1)
                    'If nTrain1 = 31 Then Stop
                    ' sSta = TrainInf(nTrain1).ComeStation
                    nFirSta = TrainInf(nTrain1).nFirstID(1)
                    Y1 = StationInf(nFirSta).YPicValue
                    intTime1 = GetTrainArriOrStartTime(nTrain1, 0, 1)
                    nEndSta = TrainInf(nTrain1).nSecondID(UBound(TrainInf(nTrain1).nPassSection))
                    Y2 = StationInf(nEndSta).YPicValue
                    intTime2 = GetTrainArriOrStartTime(nTrain1, -1, 0)
                    X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                    X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)

                    '第一列车
                    If X1 >= sngLeftX And X1 <= sngRightX Then
                        If GetUserName() <> "北京地铁" Then '北京图不画第一出库线
                            If nTrain1 Mod 2 = 0 Then '上行
                                ReDim Preserve CheDiLinkinf(UBound(CheDiLinkinf) + 1)
                                CheDiLinkinf(UBound(CheDiLinkinf)).nFirTrain = nTrain1
                                CheDiLinkinf(UBound(CheDiLinkinf)).nSecTrain = 0
                                CheDiLinkinf(UBound(CheDiLinkinf)).nCheDiId = i
                                CheDiLinkinf(UBound(CheDiLinkinf)).LineHeight = 0
                                CheDiLinkinf(UBound(CheDiLinkinf)).DrawString = ChediInfo(i).sCheCiHao
                                tmpTextWidth = rBmpGraphics.MeasureString(CheDiLinkinf(UBound(CheDiLinkinf)).DrawString.Trim, f).Width
                                CheDiLinkinf(UBound(CheDiLinkinf)).X1 = X1 - tmpTextWidth
                                CheDiLinkinf(UBound(CheDiLinkinf)).Y1 = Y1
                                CheDiLinkinf(UBound(CheDiLinkinf)).X2 = X1
                                CheDiLinkinf(UBound(CheDiLinkinf)).Y2 = Y1
                                CheDiLinkinf(UBound(CheDiLinkinf)).StringX = X1 - tmpTextWidth / 2
                                CheDiLinkinf(UBound(CheDiLinkinf)).StringY = Y1
                                CheDiLinkinf(UBound(CheDiLinkinf)).nIFPrint = False
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpPen = tmpPen
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpFont = f
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpBrush = tmpBrush
                                CheDiLinkinf(UBound(CheDiLinkinf)).upOrDown = 1
                                CheDiLinkinf(UBound(CheDiLinkinf)).sStyle = 2

                            Else '下行

                                ReDim Preserve CheDiLinkinf(UBound(CheDiLinkinf) + 1)
                                CheDiLinkinf(UBound(CheDiLinkinf)).nFirTrain = nTrain1
                                CheDiLinkinf(UBound(CheDiLinkinf)).nSecTrain = 0
                                CheDiLinkinf(UBound(CheDiLinkinf)).nCheDiId = i
                                CheDiLinkinf(UBound(CheDiLinkinf)).LineHeight = 0
                                CheDiLinkinf(UBound(CheDiLinkinf)).DrawString = ChediInfo(i).sCheCiHao
                                tmpTextWidth = rBmpGraphics.MeasureString(CheDiLinkinf(UBound(CheDiLinkinf)).DrawString.Trim, f).Width
                                CheDiLinkinf(UBound(CheDiLinkinf)).X1 = X1 - tmpTextWidth
                                CheDiLinkinf(UBound(CheDiLinkinf)).Y1 = Y1
                                CheDiLinkinf(UBound(CheDiLinkinf)).X2 = X1
                                CheDiLinkinf(UBound(CheDiLinkinf)).Y2 = Y1
                                CheDiLinkinf(UBound(CheDiLinkinf)).StringX = X1 - tmpTextWidth / 2
                                CheDiLinkinf(UBound(CheDiLinkinf)).StringY = Y1
                                CheDiLinkinf(UBound(CheDiLinkinf)).nIFPrint = False
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpPen = tmpPen
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpFont = f
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpBrush = tmpBrush
                                CheDiLinkinf(UBound(CheDiLinkinf)).upOrDown = -1
                                CheDiLinkinf(UBound(CheDiLinkinf)).sStyle = 1
                            End If
                        End If


                        If X2 >= sngLeftX And X2 <= sngRightX Then '到达
                            If GetUserName() <> "北京地铁" Then '北京图不画第一出库线
                                If nTrain1 Mod 2 = 0 Then '上行
                                    ReDim Preserve CheDiLinkinf(UBound(CheDiLinkinf) + 1)
                                    CheDiLinkinf(UBound(CheDiLinkinf)).nFirTrain = nTrain1
                                    CheDiLinkinf(UBound(CheDiLinkinf)).nCheDiId = i
                                    CheDiLinkinf(UBound(CheDiLinkinf)).nSecTrain = 0
                                    CheDiLinkinf(UBound(CheDiLinkinf)).LineHeight = 0
                                    CheDiLinkinf(UBound(CheDiLinkinf)).DrawString = TrainInf(nTrain1).sPrintTrain 'ChediInfo(i).sPrintCheCiHao
                                    tmpTextWidth = rBmpGraphics.MeasureString(CheDiLinkinf(UBound(CheDiLinkinf)).DrawString.Trim, f).Width
                                    CheDiLinkinf(UBound(CheDiLinkinf)).X1 = X2
                                    CheDiLinkinf(UBound(CheDiLinkinf)).Y1 = Y2
                                    CheDiLinkinf(UBound(CheDiLinkinf)).X2 = X2 + tmpTextWidth
                                    CheDiLinkinf(UBound(CheDiLinkinf)).Y2 = Y2
                                    CheDiLinkinf(UBound(CheDiLinkinf)).StringX = X2 + tmpTextWidth / 2
                                    CheDiLinkinf(UBound(CheDiLinkinf)).StringY = Y2
                                    CheDiLinkinf(UBound(CheDiLinkinf)).nIFPrint = False
                                    CheDiLinkinf(UBound(CheDiLinkinf)).tmpPen = tmpPen
                                    CheDiLinkinf(UBound(CheDiLinkinf)).tmpFont = f
                                    CheDiLinkinf(UBound(CheDiLinkinf)).tmpBrush = tmpBrush
                                    CheDiLinkinf(UBound(CheDiLinkinf)).upOrDown = -1
                                    CheDiLinkinf(UBound(CheDiLinkinf)).sStyle = 4

                                Else '下行

                                    ReDim Preserve CheDiLinkinf(UBound(CheDiLinkinf) + 1)
                                    CheDiLinkinf(UBound(CheDiLinkinf)).nFirTrain = nTrain1
                                    CheDiLinkinf(UBound(CheDiLinkinf)).nSecTrain = 0
                                    CheDiLinkinf(UBound(CheDiLinkinf)).nCheDiId = i
                                    CheDiLinkinf(UBound(CheDiLinkinf)).LineHeight = 0
                                    CheDiLinkinf(UBound(CheDiLinkinf)).DrawString = TrainInf(nTrain1).sPrintTrain 'ChediInfo(i).sPrintCheCiHao
                                    tmpTextWidth = rBmpGraphics.MeasureString(CheDiLinkinf(UBound(CheDiLinkinf)).DrawString.Trim, f).Width
                                    CheDiLinkinf(UBound(CheDiLinkinf)).X1 = X2
                                    CheDiLinkinf(UBound(CheDiLinkinf)).Y1 = Y2
                                    CheDiLinkinf(UBound(CheDiLinkinf)).X2 = X2 + tmpTextWidth
                                    CheDiLinkinf(UBound(CheDiLinkinf)).Y2 = Y2
                                    CheDiLinkinf(UBound(CheDiLinkinf)).StringX = X2 + tmpTextWidth / 2
                                    CheDiLinkinf(UBound(CheDiLinkinf)).StringY = Y2
                                    CheDiLinkinf(UBound(CheDiLinkinf)).nIFPrint = False
                                    CheDiLinkinf(UBound(CheDiLinkinf)).tmpPen = tmpPen
                                    CheDiLinkinf(UBound(CheDiLinkinf)).tmpFont = f
                                    CheDiLinkinf(UBound(CheDiLinkinf)).tmpBrush = tmpBrush
                                    CheDiLinkinf(UBound(CheDiLinkinf)).upOrDown = 1
                                    CheDiLinkinf(UBound(CheDiLinkinf)).sStyle = 3

                                End If

                            End If
                        End If
                    End If

                    '判断车底是否已经画完
                    If bIFShowFixChediLogo = True Then
                        If ChediInfo(i).bIfGouWang = True Then
                            rBmpGraphics.DrawRectangle(tmpEndLogo, X1 - 4, Y1 - 4, 8, 8)
                            rBmpGraphics.DrawRectangle(tmpEndLogo, X2 - 4, Y2 - 4, 8, 8)
                        End If
                    End If

                Else
                    nTrain1 = ChediInfo(i).nLinkTrain(1)
                    'If nTrain1 = 70 Then Stop
                    nFirSta = TrainInf(nTrain1).nFirstID(1)
                    Y1 = StationInf(nFirSta).YPicValue

                    intTime1 = GetTrainArriOrStartTime(nTrain1, 0, 1)
                    X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)

                    If X1 >= sngLeftX And X1 <= sngRightX Then
                        If GetUserName() <> "北京地铁" Then '北京图不画第一出库线
                            If nTrain1 Mod 2 = 0 Then '上行
                                ReDim Preserve CheDiLinkinf(UBound(CheDiLinkinf) + 1)
                                CheDiLinkinf(UBound(CheDiLinkinf)).nFirTrain = nTrain1
                                CheDiLinkinf(UBound(CheDiLinkinf)).nSecTrain = 0
                                CheDiLinkinf(UBound(CheDiLinkinf)).nCheDiId = i
                                CheDiLinkinf(UBound(CheDiLinkinf)).LineHeight = 0
                                CheDiLinkinf(UBound(CheDiLinkinf)).DrawString = ChediInfo(i).sCheCiHao
                                tmpTextWidth = rBmpGraphics.MeasureString(CheDiLinkinf(UBound(CheDiLinkinf)).DrawString.Trim, f).Width
                                CheDiLinkinf(UBound(CheDiLinkinf)).X1 = X1 - tmpTextWidth
                                CheDiLinkinf(UBound(CheDiLinkinf)).Y1 = Y1
                                CheDiLinkinf(UBound(CheDiLinkinf)).X2 = X1
                                CheDiLinkinf(UBound(CheDiLinkinf)).Y2 = Y1
                                CheDiLinkinf(UBound(CheDiLinkinf)).StringX = X1 - tmpTextWidth / 2
                                CheDiLinkinf(UBound(CheDiLinkinf)).StringY = Y1
                                CheDiLinkinf(UBound(CheDiLinkinf)).nIFPrint = False
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpPen = tmpPen
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpFont = f
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpBrush = tmpBrush
                                CheDiLinkinf(UBound(CheDiLinkinf)).upOrDown = 1
                                CheDiLinkinf(UBound(CheDiLinkinf)).sStyle = 2
                            Else '下行
                                ReDim Preserve CheDiLinkinf(UBound(CheDiLinkinf) + 1)
                                CheDiLinkinf(UBound(CheDiLinkinf)).nFirTrain = nTrain1
                                CheDiLinkinf(UBound(CheDiLinkinf)).nCheDiId = i
                                CheDiLinkinf(UBound(CheDiLinkinf)).nSecTrain = 0
                                CheDiLinkinf(UBound(CheDiLinkinf)).LineHeight = 0
                                CheDiLinkinf(UBound(CheDiLinkinf)).DrawString = ChediInfo(i).sCheCiHao
                                tmpTextWidth = rBmpGraphics.MeasureString(CheDiLinkinf(UBound(CheDiLinkinf)).DrawString.Trim, f).Width
                                CheDiLinkinf(UBound(CheDiLinkinf)).X1 = X1 - tmpTextWidth
                                CheDiLinkinf(UBound(CheDiLinkinf)).Y1 = Y1
                                CheDiLinkinf(UBound(CheDiLinkinf)).X2 = X1
                                CheDiLinkinf(UBound(CheDiLinkinf)).Y2 = Y1
                                CheDiLinkinf(UBound(CheDiLinkinf)).StringX = X1 - tmpTextWidth / 2
                                CheDiLinkinf(UBound(CheDiLinkinf)).StringY = Y1
                                CheDiLinkinf(UBound(CheDiLinkinf)).nIFPrint = False
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpPen = tmpPen
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpFont = f
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpBrush = tmpBrush
                                CheDiLinkinf(UBound(CheDiLinkinf)).upOrDown = -1
                                CheDiLinkinf(UBound(CheDiLinkinf)).sStyle = 1
                            End If
                        End If
                    End If

                    '判断车底是否已经画完
                    If bIFShowFixChediLogo = True Then
                        If ChediInfo(i).bIfGouWang = True Then
                            rBmpGraphics.DrawRectangle(tmpEndLogo, X1 - 4, Y1 - 4, 8, 8)
                        End If
                    End If

                    For j = 2 To UBound(ChediInfo(i).nLinkTrain)
                        nTrain2 = ChediInfo(i).nLinkTrain(j)
                        'If TrainInf(nTrain1).sPrintTrain = "1049" Then Stop
                        If GetSystemStaName(TrainInf(nTrain2).ComeStation) = GetSystemStaName(TrainInf(nTrain1).NextStation) Then
                            sSta = TrainInf(nTrain2).ComeStation
                            nFirSta = TrainInf(nTrain2).nFirstID(1)
                            Y1 = StationInf(nFirSta).YPicValue
                            Y2 = Y1
                            intTime1 = GetTrainArriTime(nTrain1, TrainInf(nTrain1).NextStation)
                            intTime2 = GetTrainStartTime(nTrain2, sSta)
                            X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                            X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)

                            If X1 >= 0 And Y1 >= 0 And X2 >= 0 And Y2 >= 0 Then
                                MidX = X1 + (X2 - X1) / 2
                                If (nTrain1 + nTrain2) Mod 2 = 0 Then
                                    '  If ChediInfo(i).sPrintCheCiHao = "401" Then Stop
                                    If X1 <= X2 Then
                                        If X1 <= sngRightX Then
                                            If X2 <= sngRightX Then '
                                                rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                                                If nifPrintCheCi = True Then
                                                    tmpTextWidth = rBmpGraphics.MeasureString(ChediInfo(i).sCheCiHao, f).Width
                                                    ntmpHeight = rBmpGraphics.MeasureString(ChediInfo(i).sCheCiHao, f).Height
                                                    rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y1)
                                                    If nTrain1 Mod 2 = 0 Then
                                                        rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, tmpBrush, MidX - tmpTextWidth / 2, Y1)
                                                    Else
                                                        rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, tmpBrush, MidX - tmpTextWidth / 2, Y1 - ntmpHeight)
                                                    End If
                                                End If
                                            Else '右边过界
                                                If GetUserName() <> "北京地铁" Then
                                                    rBmpGraphics.DrawLine(tmpPen, X1, Y1, sngRightX, Y2)
                                                    If nifPrintCheCi = True Then
                                                        rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, tmpBrush, X1 + 2, Y1 - nLinkHeight - 5)
                                                    End If
                                                End If
                                            End If
                                        End If
                                    Else '左边接边界
                                        If GetUserName() <> "北京地铁" Then
                                            rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y2)
                                            If nifPrintCheCi = True Then
                                                rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, tmpBrush, X1, Y1 - nLinkHeight - 5)
                                            End If
                                            rBmpGraphics.DrawLine(tmpPen, X2 - 20, Y1, X2, Y2)
                                            If nifPrintCheCi = True Then
                                                rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, tmpBrush, X2 - 20, Y1 - nLinkHeight - 5)
                                            End If
                                        End If

                                    End If
                                Else
                                    If X1 <= X2 Then
                                        If X1 <= sngRightX Then
                                            If nTrain1 Mod 2 = 0 Then
                                                If X2 <= sngRightX Then

                                                    If X2 - X1 >= 600 Then '对于交路线过长的情况
                                                        If GetUserName() <> "北京地铁" Then
                                                            rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y1 - nLinkHeight)
                                                            If nifPrintCheCi = True Then
                                                                rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, tmpBrush, X1 + 10, Y1 - nLinkHeight - 12)
                                                            End If
                                                            rBmpGraphics.DrawLine(tmpPen, X2, Y2, X2 - 20, Y2 - nLinkHeight)
                                                            If nifPrintCheCi = True Then
                                                                rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, tmpBrush, X2 - 30, Y2 - nLinkHeight - 12)
                                                            End If
                                                        End If

                                                    Else
                                                        If sShowStyle = "三角形" Then
                                                            tmpTextWidth = rBmpGraphics.MeasureString(ChediInfo(i).sCheCiHao, f).Width
                                                            ntmpHeight = rBmpGraphics.MeasureString(ChediInfo(i).sCheCiHao, f).Height
                                                            rBmpGraphics.DrawLine(tmpPen, X1, Y1, MidX, Y1 - ntmpHeight)
                                                            rBmpGraphics.DrawLine(tmpPen, MidX, Y1 - ntmpHeight, X2, Y2)
                                                            If nifPrintCheCi = True Then
                                                                rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, Brushes.Blue, MidX - tmpTextWidth / 2, Y1 - ntmpHeight - 12)
                                                            End If

                                                        Else    '画长方形
                                                            '================================================================================================================{
                                                            ReDim Preserve CheDiLinkinf(UBound(CheDiLinkinf) + 1)
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).nFirTrain = nTrain1
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).nCheDiId = i
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).nSecTrain = nTrain2
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).LineHeight = 0
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).X1 = X1
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).Y1 = Y1
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).X2 = X2
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).Y2 = Y2
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).DrawString = TrainInf(nTrain1).sPrintTrain & "/" & TrainInf(nTrain2).sPrintTrain ' ChediInfo(i).sPrintCheCiHao
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).StringX = MidX
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).StringY = Y2
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).nIFPrint = False
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).tmpPen = tmpPen
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).tmpFont = f
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).tmpBrush = tmpBrush
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).upOrDown = -1
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).sStyle = 0
                                                        End If
                                                    End If
                                                Else '右边过界
                                                    If GetUserName() <> "北京地铁" Then
                                                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y1 - nLinkHeight)
                                                        If nifPrintCheCi = True Then
                                                            rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, tmpBrush, X1 + 10, Y1 - nLinkHeight - 12)
                                                        End If
                                                    End If
                                                End If
                                            Else
                                                If X2 <= sngRightX Then
                                                    If X2 - X1 >= 600 Then '对于交路线过长的情况
                                                        If GetUserName() <> "北京地铁" Then
                                                            rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y1 + 10)
                                                            If nifPrintCheCi = True Then
                                                                rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, tmpBrush, X1 + 10, Y1 + nLinkHeight)
                                                            End If
                                                            rBmpGraphics.DrawLine(tmpPen, X2, Y2, X2 - 20, Y2 + nLinkHeight)
                                                            If nifPrintCheCi = True Then
                                                                rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, tmpBrush, X2 - 30, Y2 + nLinkHeight)
                                                            End If
                                                        End If
                                                    Else
                                                        If sShowStyle = "三角形" Then
                                                            tmpTextWidth = rBmpGraphics.MeasureString(ChediInfo(i).sCheCiHao, f).Width
                                                            ntmpHeight = rBmpGraphics.MeasureString(ChediInfo(i).sCheCiHao, f).Height

                                                            rBmpGraphics.DrawLine(tmpPen, X1, Y1, MidX, Y1 + ntmpHeight)
                                                            rBmpGraphics.DrawLine(tmpPen, MidX, Y1 + ntmpHeight, X2, Y2)
                                                            If nifPrintCheCi = True Then
                                                                rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, Brushes.Blue, MidX - tmpTextWidth / 2, Y1 + ntmpHeight)
                                                            End If

                                                        Else    '长方形
                                                            ReDim Preserve CheDiLinkinf(UBound(CheDiLinkinf) + 1)
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).nFirTrain = nTrain1
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).nCheDiId = i
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).nSecTrain = nTrain2
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).LineHeight = 0
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).X1 = X1
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).Y1 = Y1
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).X2 = X2
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).Y2 = Y2
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).DrawString = TrainInf(nTrain1).sPrintTrain & "/" & TrainInf(nTrain2).sPrintTrain 'ChediInfo(i).sPrintCheCiHao
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).StringX = MidX
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).StringY = Y2
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).nIFPrint = False
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).tmpPen = tmpPen
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).tmpFont = f
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).tmpBrush = tmpBrush
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).upOrDown = 1
                                                            CheDiLinkinf(UBound(CheDiLinkinf)).sStyle = 0

                                                        End If
                                                        'If nifPrintCheCi = True Then
                                                        '    rBmpGraphics.DrawString(ChediInfo(i).sPrintCheCiHao, f, tmpBrush, MidX - 12, Y2 + ntmpHeight - 2)
                                                        'End If
                                                    End If
                                                Else
                                                    If GetUserName() <> "北京地铁" Then
                                                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y1 + nLinkHeight)
                                                        If nifPrintCheCi = True Then
                                                            rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, tmpBrush, X1 + 10, Y1 + nLinkHeight - 2)
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    Else '左边过界
                                        If GetUserName() <> "北京地铁" Then
                                            If nTrain1 Mod 2 = 0 Then
                                                rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y1 - nLinkHeight)
                                                If nifPrintCheCi = True Then
                                                    rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, tmpBrush, X1 + 10, Y1 - nLinkHeight - 12)
                                                End If
                                                rBmpGraphics.DrawLine(tmpPen, X2 - 20, Y1 - nLinkHeight, X2, Y2)
                                                If nifPrintCheCi = True Then
                                                    rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, tmpBrush, X2 - 30, Y1 - nLinkHeight - 12)
                                                End If
                                            Else
                                                rBmpGraphics.DrawLine(tmpPen, X2 - 20, Y1 + nLinkHeight, X2, Y2)
                                                If nifPrintCheCi = True Then
                                                    rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, tmpBrush, X2 - 30, Y1 + nLinkHeight)
                                                End If
                                                rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y1 + nLinkHeight)
                                                If nifPrintCheCi = True Then
                                                    rBmpGraphics.DrawString(ChediInfo(i).sCheCiHao, f, tmpBrush, X1 + 10, Y1 + nLinkHeight - 2)
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        nTrain1 = nTrain2
                    Next

                    nTrain1 = ChediInfo(i).nLinkTrain(UBound(ChediInfo(i).nLinkTrain))
                    nEndSta = TrainInf(nTrain1).nSecondID(UBound(TrainInf(nTrain1).nPassSection))
                    Y2 = StationInf(nEndSta).YPicValue
                    intTime2 = GetTrainArriOrStartTime(nTrain1, -1, 0)
                    X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)

                    If bIFShowFixChediLogo = True Then
                        If ChediInfo(i).bIfGouWang = True Then
                            rBmpGraphics.DrawRectangle(tmpEndLogo, X2 - 4, Y2 - 4, 8, 8)
                        End If
                    End If

                    If X2 >= sngLeftX And X2 <= sngRightX Then
                        If GetUserName() <> "北京地铁" Then '北京图不画第一出库线
                            If nTrain1 Mod 2 = 0 Then '上行  '终到
                                ReDim Preserve CheDiLinkinf(UBound(CheDiLinkinf) + 1)
                                CheDiLinkinf(UBound(CheDiLinkinf)).nFirTrain = nTrain1
                                CheDiLinkinf(UBound(CheDiLinkinf)).nSecTrain = 0
                                CheDiLinkinf(UBound(CheDiLinkinf)).nCheDiId = i
                                CheDiLinkinf(UBound(CheDiLinkinf)).LineHeight = 0
                                CheDiLinkinf(UBound(CheDiLinkinf)).DrawString = ChediInfo(i).sCheCiHao
                                tmpTextWidth = rBmpGraphics.MeasureString(CheDiLinkinf(UBound(CheDiLinkinf)).DrawString.Trim, f).Width
                                CheDiLinkinf(UBound(CheDiLinkinf)).X1 = X2
                                CheDiLinkinf(UBound(CheDiLinkinf)).Y1 = Y2
                                CheDiLinkinf(UBound(CheDiLinkinf)).X2 = X2 + tmpTextWidth
                                CheDiLinkinf(UBound(CheDiLinkinf)).Y2 = Y2
                                CheDiLinkinf(UBound(CheDiLinkinf)).StringX = X2 + tmpTextWidth / 2
                                CheDiLinkinf(UBound(CheDiLinkinf)).StringY = Y2
                                CheDiLinkinf(UBound(CheDiLinkinf)).nIFPrint = False
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpPen = tmpPen
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpFont = f
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpBrush = tmpBrush
                                CheDiLinkinf(UBound(CheDiLinkinf)).upOrDown = -1
                                CheDiLinkinf(UBound(CheDiLinkinf)).sStyle = 4
                            Else '下行
                                ReDim Preserve CheDiLinkinf(UBound(CheDiLinkinf) + 1)
                                CheDiLinkinf(UBound(CheDiLinkinf)).nFirTrain = nTrain1
                                CheDiLinkinf(UBound(CheDiLinkinf)).nCheDiId = i
                                CheDiLinkinf(UBound(CheDiLinkinf)).nSecTrain = 0
                                CheDiLinkinf(UBound(CheDiLinkinf)).LineHeight = 0
                                CheDiLinkinf(UBound(CheDiLinkinf)).DrawString = ChediInfo(i).sCheCiHao
                                tmpTextWidth = rBmpGraphics.MeasureString(CheDiLinkinf(UBound(CheDiLinkinf)).DrawString.Trim, f).Width
                                CheDiLinkinf(UBound(CheDiLinkinf)).X1 = X2
                                CheDiLinkinf(UBound(CheDiLinkinf)).Y1 = Y2
                                CheDiLinkinf(UBound(CheDiLinkinf)).X2 = X2 + tmpTextWidth
                                CheDiLinkinf(UBound(CheDiLinkinf)).Y2 = Y2
                                CheDiLinkinf(UBound(CheDiLinkinf)).StringX = X2 + tmpTextWidth / 2
                                CheDiLinkinf(UBound(CheDiLinkinf)).StringY = Y2
                                CheDiLinkinf(UBound(CheDiLinkinf)).nIFPrint = False
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpPen = tmpPen
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpFont = f
                                CheDiLinkinf(UBound(CheDiLinkinf)).tmpBrush = tmpBrush
                                CheDiLinkinf(UBound(CheDiLinkinf)).upOrDown = 1
                                CheDiLinkinf(UBound(CheDiLinkinf)).sStyle = 3
                            End If
                        End If
                    End If

                End If
                nNum = nNum + 1
            End If
        Next
        Dim nPaiXu() As Integer
        ReDim nPaiXu(UBound(CheDiLinkinf))
        For i = 1 To UBound(nPaiXu)
            nPaiXu(i) = i
        Next

        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long

        '按时间排序
        Flag = 1
        k = UBound(nPaiXu)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                TempTime1 = CheDiLinkinf(nPaiXu(j)).X1
                Temptime2 = CheDiLinkinf(nPaiXu(j + 1)).X1
                If TempTime1 > Temptime2 Then
                    temp = nPaiXu(j)
                    nPaiXu(j) = nPaiXu(j + 1)
                    nPaiXu(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop

        Dim tmpHeight As Single
        Dim tmpStep As Single
        Dim sngHeight As Integer
        Dim nHeight As Integer
        Dim txtWidth As Single
        Dim txtHeight As Single
        Dim PrintText As String
        tmpHeight = nChediLineHeight
        tmpStep = nChediLineHeight
        sngHeight = 0
        nHeight = 0
        For i = 1 To UBound(nPaiXu)
            'If CheDiLinkinf(nPaiXu(i)).nFirTrain = 23 Then Stop
            nHeight = GetCurLineHeight(nPaiXu(i))
            sngHeight = (tmpHeight + nHeight * tmpStep) * CheDiLinkinf(nPaiXu(i)).upOrDown
            CheDiLinkinf(nPaiXu(i)).LineHeight = nHeight
            X1 = CheDiLinkinf(nPaiXu(i)).X1
            Y1 = CheDiLinkinf(nPaiXu(i)).Y1
            X2 = CheDiLinkinf(nPaiXu(i)).X2
            Y2 = CheDiLinkinf(nPaiXu(i)).Y2
            Select Case CheDiLinkinf(nPaiXu(i)).sStyle
                Case 0 '矩形
                    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X1, Y1, X1, Y1 + sngHeight)
                    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X2, Y2, X2, Y2 + sngHeight)
                    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X1, Y1 + sngHeight, X2, Y2 + sngHeight)
                Case 1 '左上
                    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X2, Y2, X2, Y2 + sngHeight)
                    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X1, Y1 + sngHeight, X1 - 5, Y1 + sngHeight - 5)
                    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X2, Y2 + sngHeight, X1, Y1 + sngHeight)

                Case 2 '右下
                    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X2, Y2, X2, Y2 + sngHeight)
                    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X1, Y1 + sngHeight, X1 - 5, Y1 + sngHeight + 5)
                    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X2, Y2 + sngHeight, X1, Y1 + sngHeight)

                Case 3 '左下
                    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X1, Y1, X1, Y1 + sngHeight)
                    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X2, Y2 + sngHeight, X2 + 5, Y2 + sngHeight + 5)
                    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X1, Y1 + sngHeight, X2, Y2 + sngHeight)

                Case 4 '右上
                    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X1, Y1, X1, Y1 + sngHeight)
                    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X2, Y2 + sngHeight, X2 + 5, Y2 + sngHeight - 5)
                    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X1, Y1 + sngHeight, X2, Y2 + sngHeight)

            End Select

            If TimeTablePara.TimeTableDiagramPara.nifPrintCheCi = True Then
                PrintText = ChediInfo(CheDiLinkinf(nPaiXu(i)).nCheDiId).sCheCiHao
                txtWidth = rBmpGraphics.MeasureString(PrintText, CheDiLinkinf(nPaiXu(i)).tmpFont).Width / 2
                If CheDiLinkinf(nPaiXu(i)).upOrDown < 0 Then
                    txtHeight = rBmpGraphics.MeasureString(PrintText, CheDiLinkinf(nPaiXu(i)).tmpFont).Height * CheDiLinkinf(nPaiXu(i)).upOrDown + 2
                Else
                    txtHeight = 0
                End If
                rBmpGraphics.DrawString(PrintText, CheDiLinkinf(nPaiXu(i)).tmpFont, CheDiLinkinf(nPaiXu(i)).tmpBrush, CheDiLinkinf(nPaiXu(i)).StringX - txtWidth, CheDiLinkinf(nPaiXu(i)).StringY + sngHeight + txtHeight)
            End If

        Next
    End Sub

    '得到当前车底线的高度
    Public Function GetCurLineHeight(ByVal nCurId As Integer) As Integer
        Dim i As Integer
        Dim CurX1 As Single
        CurX1 = CheDiLinkinf(nCurId).X1
        Dim nHeight() As Integer
        ReDim nHeight(0)
        For i = 1 To UBound(CheDiLinkinf)
            If i <> nCurId Then
                If CheDiLinkinf(i).nIFPrint = True And CheDiLinkinf(i).Y1 = CheDiLinkinf(nCurId).Y1 And CheDiLinkinf(i).upOrDown = CheDiLinkinf(nCurId).upOrDown Then
                    If CurX1 >= CheDiLinkinf(i).X1 And CurX1 <= CheDiLinkinf(i).X2 Then
                        ReDim Preserve nHeight(UBound(nHeight) + 1)
                        nHeight(UBound(nHeight)) = CheDiLinkinf(i).LineHeight
                    End If
                End If
            End If
        Next
        Dim N As Integer
        N = 0
        Dim ifIn As Integer
        Do
            ifIn = 1
            For i = 1 To UBound(nHeight)
                If nHeight(i) = N Then
                    ifIn = 0
                    Exit For
                End If
            Next
            If ifIn = 1 Then
                GetCurLineHeight = N
                Exit Do
            End If
            N = N + 1
        Loop
        CheDiLinkinf(nCurId).nIFPrint = True
        CheDiLinkinf(nCurId).LineHeight = GetCurLineHeight
    End Function
    '得以股道的类型
    Public Function GetCurGuDaoStyle(ByVal sGuDaoNum As String, ByVal nStaId As Integer) As String
        Dim i As Integer
        GetCurGuDaoStyle = ""
        For i = 1 To UBound(StationInf(nStaId).sStLineNo)
            If StationInf(nStaId).sStLineNo(i) = sGuDaoNum Then
                GetCurGuDaoStyle = StationInf(nStaId).sLineUse(i).Substring(0, 3)
                Exit For
            End If
        Next
    End Function

    '画技术图解
    Public Sub DrawGuDaoJiShuTuJie(ByVal nSta As Integer, ByVal rBmpGraphics As Graphics, ByVal rbmpStaGraphics As Graphics, ByVal rbmpStaGraphics2 As Graphics, ByVal PicWidth As Single, ByVal PicHeight As Single, _
                                        ByVal LeftBlank As Single, ByVal topBlank As Single, ByVal StaBlank As Single, _
                                        ByVal sngTimeBlank As Single, ByVal sngLeftX As Single, ByVal sngTopY As Single, _
                                        ByVal nFirTime As Integer, ByVal nTimeWidth As Integer, ByVal nGudaoHeight As Single, ByVal sPrintStyle As String, ByVal StrTimeFormate As String)
        'rbmpStaGraphics2.Clear(Color.Black)
        Call DrawGuDaoJiShuTuJieTimeLine(rBmpGraphics, rbmpStaGraphics, rbmpStaGraphics2, PicWidth, PicHeight, LeftBlank, topBlank, StaBlank, TimeTablePara.TimeTableDiagramPara.sngTimeBlank, sngLeftX, sngTopY, nFirTime, nTimeWidth, nGudaoHeight, sPrintStyle, StrTimeFormate)
        If TimeTablePara.sPubCurSkbName <> "" Then
            Call DrawJiShuTuJieDiagramLine(rBmpGraphics, PicWidth, nFirTime, nTimeWidth, LeftBlank, StaBlank, sngLeftX, nGudaoHeight)
        End If

    End Sub

    Public Sub InputGuDaoYData(ByVal nSta As Integer, ByVal nGuDaoHeight As Single, ByVal sngTopBlank As Single, ByVal sngTopY As Single)
        ' Dim picHeight As Integer
        Dim stsPicHeight As Integer
        Dim i As Integer
        GuDaoJishutujie.nSta = nSta
        JishutujieVerInterval = nGuDaoHeight * 2
        Dim nCurY As Single
        ReDim GuDaoJishutujie.sGuDao(0)
        ReDim GuDaoJishutujie.GuDaoYcoord(0)
        ReDim GuDaoJishutujie.nUpSta(0)
        ReDim GuDaoJishutujie.UpStaYcoord(0)
        ReDim GuDaoJishutujie.nDownSta(0)
        ReDim GuDaoJishutujie.DownStaYcoord(0)
        For i = 1 To UBound(SectionInf)
            If StationInf(SectionInf(i).nSecStaID).sStationName = StationInf(nSta).sStationName Then
                ReDim Preserve GuDaoJishutujie.nUpSta(UBound(GuDaoJishutujie.nUpSta) + 1)
                GuDaoJishutujie.nUpSta(UBound(GuDaoJishutujie.nUpSta)) = SectionInf(i).nFirStaID
                ReDim Preserve GuDaoJishutujie.UpStaYcoord(UBound(GuDaoJishutujie.UpStaYcoord) + 1)
            End If
        Next

        '对股道编号排序
        Dim tmpDownNum() As String
        Dim tmpUPNum() As String
        ReDim tmpDownNum(0)
        ReDim tmpUPNum(0)
        Dim nNum As Integer
        For i = 1 To UBound(StationInf(nSta).sStLineNo)
            If Val(StationInf(nSta).sStLineNo(i)) Mod 2 <> 0 Then
                ReDim Preserve tmpDownNum(UBound(tmpDownNum) + 1)
                tmpDownNum(UBound(tmpDownNum)) = StationInf(nSta).sStLineNo(i)
            Else
                ReDim Preserve tmpUPNum(UBound(tmpUPNum) + 1)
                tmpUPNum(UBound(tmpUPNum)) = StationInf(nSta).sStLineNo(i)
            End If
        Next
        Call SetPaiXu(tmpDownNum, 2)
        Call SetPaiXu(tmpUPNum, 1)
        nNum = 1
        For i = 1 To UBound(tmpDownNum)
            ReDim Preserve GuDaoJishutujie.sGuDao(nNum)
            ReDim Preserve GuDaoJishutujie.GuDaoYcoord(nNum)
            GuDaoJishutujie.sGuDao(nNum) = tmpDownNum(i)
            nNum = nNum + 1
        Next

        For i = 1 To UBound(tmpUPNum)
            ReDim Preserve GuDaoJishutujie.sGuDao(nNum)
            ReDim Preserve GuDaoJishutujie.GuDaoYcoord(nNum)
            GuDaoJishutujie.sGuDao(nNum) = tmpUPNum(i)
            nNum = nNum + 1
        Next

        For i = 1 To UBound(SectionInf)
            If StationInf(SectionInf(i).nFirStaID).sStationName = StationInf(nSta).sStationName Then
                ReDim Preserve GuDaoJishutujie.nDownSta(UBound(GuDaoJishutujie.nDownSta) + 1)
                GuDaoJishutujie.nDownSta(UBound(GuDaoJishutujie.nDownSta)) = SectionInf(i).nSecStaID
                ReDim Preserve GuDaoJishutujie.DownStaYcoord(UBound(GuDaoJishutujie.DownStaYcoord) + 1)
            End If
        Next

        Dim tmpCurY As Single
        nCurY = sngTopBlank + sngTopY
        tmpCurY = nCurY
        For i = 1 To UBound(GuDaoJishutujie.nUpSta)
            nCurY = tmpCurY + (i - 1) * nGuDaoHeight
            GuDaoJishutujie.UpStaYcoord(i) = nCurY
        Next

        nCurY = nCurY + nGuDaoHeight
        tmpCurY = nCurY
        For i = 1 To UBound(GuDaoJishutujie.sGuDao)
            nCurY = tmpCurY + (i - 1) * nGuDaoHeight
            GuDaoJishutujie.GuDaoYcoord(i) = nCurY
        Next

        nCurY = nCurY + nGuDaoHeight * 2
        tmpCurY = nCurY
        For i = 1 To UBound(GuDaoJishutujie.nDownSta)
            nCurY = tmpCurY + (i - 1) * nGuDaoHeight
            GuDaoJishutujie.DownStaYcoord(i) = nCurY
        Next

        stsPicHeight = 1000
        GuDaoJishutujie.nTotleY = nCurY + nGuDaoHeight + 40
    End Sub


    '画股道技术图解股道线
    Public Sub DrawGuDaoJiShuTuJieTimeLine(ByVal rBmpGraphics As Graphics, ByVal rbmpStaGraphics As Graphics, ByVal rbmpStaGraphics2 As Graphics, ByVal PicWidth As Single, ByVal PicHeight As Single, _
                                        ByVal LeftBlank As Single, ByVal topBlank As Single, ByVal StaBlank As Single, _
                                        ByVal sngTimeBlank As Single, ByVal sngLeftX As Single, ByVal sngTopY As Single, _
                                        ByVal nFirTime As Integer, ByVal nTimeWidth As Integer, ByVal nGudaoHeight As Integer, _
                                        ByVal sPrintStyle As String, ByVal strTimeFormat As String)

        Dim i, j As Integer
        Dim intTimeLine As Single
        Dim sngMinuWidth As Single
        Dim sngToWidth As Single
        Dim sngHeight As Single
        Dim intFirstTime As Integer
        intFirstTime = nFirTime
        sngToWidth = PicWidth - LeftBlank * 2 - StaBlank - sngLeftX
        sngHeight = PicHeight - 2 * topBlank
        Dim sGuDaoStyle As String
        Dim sDrawString As String
        Dim GuDaoUpLineY As Single
        Dim GuDaoDownLineY As Single
        Dim DiagramTimeLineFont As String
        DiagramTimeLineFont = "全部"
        If DiagramTimeLineFont = "全部" Then
            If PicWidth <= 5000 And nTimeWidth >= 6 Then
                DiagramTimeLineFont = ""
            Else
                DiagramTimeLineFont = "全部"
            End If
        End If

        '#####################'车站名称#######################
        Dim maxY As Single
        Dim tmpY1 As Single
        Dim tmpY2 As Single
        Dim tmpY As Single
        Dim YBix As Single
        Dim brsStation As Brush
        Dim clrStation As Color
        Dim txtWidth As Single
        Dim curPen As Pen
        tmpY1 = 0 ' StationInf(1).Ycord
        maxY = 0
        For i = 1 To UBound(StationInf)
            If StationInf(i).Ycord > maxY Then
                maxY = StationInf(i).Ycord
            End If
        Next
        YBix = 1

        '上面车站横线
        For i = 1 To UBound(GuDaoJishutujie.nUpSta)
            tmpY = GuDaoJishutujie.UpStaYcoord(i)
            If StationInf(i).sPrintStaName.Substring(0, 2) = "车场" Or StationInf(i).sPrintStaName.Substring(StationInf(i).sPrintStaName.Length - 2) = "车场" Then
                clrStation = Color.Orange
            Else
                clrStation = Color.Green
            End If
            rBmpGraphics.DrawLine(New System.Drawing.Pen(clrStation, 2), LeftBlank + StaBlank + sngLeftX, tmpY, LeftBlank + StaBlank + sngLeftX + sngToWidth, tmpY)
            brsStation = Brushes.Green
            txtWidth = rBmpGraphics.MeasureString(StationInf(GuDaoJishutujie.nUpSta(i)).sPrintStaName, New Font("黑体", 11)).Width
            rBmpGraphics.DrawString(StationInf(GuDaoJishutujie.nUpSta(i)).sPrintStaName, New Font("黑体", 11), brsStation, LeftBlank + sngLeftX + StaBlank - txtWidth - 10, tmpY - 6)
            If sPrintStyle = "显示" Then
                rbmpStaGraphics2.DrawString(StationInf(GuDaoJishutujie.nUpSta(i)).sPrintStaName, New Font("黑体", 11), brsStation, StaBlank - txtWidth - 5, tmpY - 6)
                rbmpStaGraphics.DrawString(StationInf(GuDaoJishutujie.nUpSta(i)).sPrintStaName, New Font("黑体", 11), brsStation, 5, tmpY - 6)
            End If
        Next i

        '股道横线
        For i = 1 To UBound(GuDaoJishutujie.sGuDao)
            tmpY = GuDaoJishutujie.GuDaoYcoord(i)
            clrStation = Color.Green
            rBmpGraphics.DrawLine(New System.Drawing.Pen(clrStation, 1), LeftBlank + StaBlank + sngLeftX, tmpY, LeftBlank + StaBlank + sngLeftX + sngToWidth, tmpY)
            brsStation = Brushes.Green
            sGuDaoStyle = GetCurGuDaoStyle(GuDaoJishutujie.sGuDao(i), GuDaoJishutujie.nSta)
            If sGuDaoStyle = "折返线" Then
                sDrawString = "折" & GuDaoJishutujie.sGuDao(i)
            Else
                sDrawString = GuDaoJishutujie.sGuDao(i) & "道"
            End If

            rBmpGraphics.DrawString(sDrawString, New Font("黑体", 11), brsStation, LeftBlank + sngLeftX + StaBlank - 40, tmpY + 10)
            txtWidth = rBmpGraphics.MeasureString(sDrawString, New Font("黑体", 11)).Width
            If sPrintStyle = "显示" Then
                rbmpStaGraphics2.DrawString(sDrawString, New Font("黑体", 11), brsStation, StaBlank - txtWidth - 5, tmpY + 10)
                rbmpStaGraphics.DrawString(sDrawString, New Font("黑体", 11), brsStation, 5, tmpY + 10)
            End If

            If i = 1 Then
                GuDaoUpLineY = tmpY
            End If
            If i = UBound(GuDaoJishutujie.sGuDao) Then
                GuDaoDownLineY = tmpY
            End If
        Next i
        tmpY2 = GuDaoDownLineY
        tmpY = tmpY2 + nGudaoHeight '(tmpY2 - tmpY1) * YBix + topBlank + sngTopY
        rBmpGraphics.DrawLine(New System.Drawing.Pen(clrStation, 1), LeftBlank + StaBlank + sngLeftX, tmpY, LeftBlank + StaBlank + sngLeftX + sngToWidth, tmpY)
        brsStation = Brushes.Green
        'rBmpGraphics.DrawString(GuDaoJishutujie.sGuDao(i), New Font("黑体", 11), brsStation, LeftBlank + sngLeftX, tmpY - 6)
        GuDaoDownLineY = tmpY


        '下面车站横线
        For i = 1 To UBound(GuDaoJishutujie.nDownSta)
            tmpY = GuDaoJishutujie.DownStaYcoord(i)
            If StationInf(i).sPrintStaName.Substring(0, 2) = "车场" Or StationInf(i).sStationName.Substring(StationInf(i).sPrintStaName.Length - 2) = "车场" Then
                clrStation = Color.Orange
            Else
                clrStation = Color.Green
            End If
            rBmpGraphics.DrawLine(New System.Drawing.Pen(clrStation, 2), LeftBlank + StaBlank + sngLeftX, tmpY, LeftBlank + StaBlank + sngLeftX + sngToWidth, tmpY)
            brsStation = Brushes.Green
            txtWidth = rBmpGraphics.MeasureString(StationInf(GuDaoJishutujie.nDownSta(i)).sPrintStaName, New Font("黑体", 11)).Width
            rBmpGraphics.DrawString(StationInf(GuDaoJishutujie.nDownSta(i)).sPrintStaName, New Font("黑体", 11), brsStation, LeftBlank + sngLeftX + StaBlank - txtWidth - 10, tmpY - 6)
            If sPrintStyle = "显示" Then
                rbmpStaGraphics.DrawString(StationInf(GuDaoJishutujie.nDownSta(i)).sPrintStaName, New Font("黑体", 11), brsStation, 5, tmpY - 6)
                rbmpStaGraphics2.DrawString(StationInf(GuDaoJishutujie.nDownSta(i)).sPrintStaName, New Font("黑体", 11), brsStation, StaBlank - txtWidth - 5, tmpY - 6)
            End If
        Next i

        Dim strTimePrint As String
        Dim sngY1, sngY2 As Single
        sngY1 = GuDaoUpLineY
        sngY2 = GuDaoDownLineY
        Dim LastLineY As Single
        LastLineY = tmpY + 40
        Select Case strTimeFormat 'TimeTablePara.TimeTableDiagramPara.strTimeFormat
            Case "小时格"
                intTimeLine = nTimeWidth  '10 * 6 * 24
                sngMinuWidth = sngToWidth / (intTimeLine)
                For i = 1 To intTimeLine + 1
                    curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.HourTime60LineColor), TimeTablePara.DiagramStylePara.HourTime60LineWidth)
                    curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.HourTime60LineStyle)
                    rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)
                    strTimePrint = Trim(Str((intFirstTime + i - 1) Mod 24) & ":00")
                    '打印时间文字,第一行
                    rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - sngTimeBlank + sngTopY - 12)
                    '最后一行
                    rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, LastLineY) ' PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                Next
            Case "十分格"
                intTimeLine = 6 * nTimeWidth
                sngMinuWidth = sngToWidth / (intTimeLine)
                For i = 1 To intTimeLine + 1
                    If (i - 1) Mod 6 = 0 Then
                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TenTime60LineColor), TimeTablePara.DiagramStylePara.TenTime60LineWidth)
                        curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.TenTime60LineStyle)
                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)
                        strTimePrint = Trim(Str((intFirstTime + (Int((i - 1) / 6))) Mod 24) & ":00")
                        '打印时间文字,第一行
                        rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - sngTimeBlank + sngTopY - 12)
                        '最后一行
                        rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, LastLineY)
                    Else

                        If (i - 1) Mod 3 = 0 Then
                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime30LineColor), TimeTablePara.DiagramStylePara.TwoTime30LineWidth)
                            curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.TwoTime30LineStyle)
                            rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)
                            strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                            'If strTimePrint = "8:50" Then Stop
                            'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                            '打印时间文字,第一行
                            If DiagramTimeLineFont = "全部" Then
                                rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - sngTimeBlank + sngTopY - 12)
                                rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, LastLineY)
                            End If
                        Else
                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TenTime10LineColor), TimeTablePara.DiagramStylePara.TenTime10LineWidth)
                            curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.TenTime10LineStyle)
                            rBmpGraphics.DrawLine(curPen, LeftBlank + StaBlank + sngLeftX + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)
                            strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                            'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                            '打印时间文字,第一行
                            'If strTimePrint = "8:50" Then Stop
                            If DiagramTimeLineFont = "全部" Then
                                rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - sngTimeBlank + sngTopY - 12)
                                '最后一行
                                rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, LastLineY)
                            End If
                        End If
                    End If
                Next
            Case "二分格"
                intTimeLine = 6 * nTimeWidth
                sngMinuWidth = sngToWidth / (intTimeLine)
                For i = 1 To intTimeLine + 1
                    If (i - 1) Mod 6 = 0 Then
                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime60LineColor), TimeTablePara.DiagramStylePara.TwoTime60LineWidth)
                        curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.TwoTime60LineStyle)
                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)
                        strTimePrint = Trim(Str((intFirstTime + (Int((i - 1) / 6))) Mod 24) & ":00")
                        '打印时间文字,第一行
                        rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - sngTimeBlank + sngTopY - 12)
                        '最后一行
                        rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, LastLineY)
                    Else

                        If (i - 1) Mod 3 = 0 Then
                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime30LineColor), TimeTablePara.DiagramStylePara.TwoTime30LineWidth)
                            curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.TwoTime30LineStyle)
                            rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)

                            strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                            'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                            '打印时间文字,第一行
                            If DiagramTimeLineFont = "全部" Then
                                rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - sngTimeBlank + sngTopY - 12)
                                '最后一行
                                rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + StaBlank + (i - 1) * sngMinuWidth - 12, LastLineY)
                            End If
                        Else
                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime10LineColor), TimeTablePara.DiagramStylePara.TwoTime10LineWidth)
                            curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.TwoTime10LineStyle)
                            rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)
                            strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                            'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                            '打印时间文字,第一行
                            If DiagramTimeLineFont = "全部" Then
                                rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + sngLeftX + (i - 1) * sngMinuWidth - 12, topBlank - sngTimeBlank + sngTopY - 12)
                                '最后一行
                                rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + sngLeftX + (i - 1) * sngMinuWidth - 12, LastLineY)
                            End If
                        End If
                    End If
                    If i <= intTimeLine Then
                        For j = 1 To 4
                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime2LineColor), TimeTablePara.DiagramStylePara.TwoTime2LineWidth)
                            curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.TwoTime2LineStyle)
                            rBmpGraphics.DrawLine(curPen, LeftBlank + StaBlank + sngLeftX + (i - 1) * sngMinuWidth + j * sngMinuWidth / 5, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth + j * sngMinuWidth / 5, sngY2)
                        Next j
                    End If
                Next i
            Case "一分格"
                intTimeLine = 6 * nTimeWidth
                sngMinuWidth = sngToWidth / (intTimeLine)
                For i = 1 To intTimeLine + 1
                    If (i - 1) Mod 6 = 0 Then
                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime60LineColor), TimeTablePara.DiagramStylePara.OneTime60LineWidth)
                        curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime60LineStyle)
                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)
                        strTimePrint = Trim(Str((intFirstTime + (Int((i - 1) / 6))) Mod 24) & ":00")
                        '打印时间文字,第一行
                        rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - sngTimeBlank + sngTopY - 12)
                        '最后一行
                        rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, LastLineY)
                    Else
                        If (i - 1) Mod 3 = 0 Then
                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime30LineColor), TimeTablePara.DiagramStylePara.OneTime30LineWidth)
                            curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime30LineStyle)
                            rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)

                            strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                            'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                            '打印时间文字,第一行
                            If DiagramTimeLineFont = "全部" Then
                                rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - sngTimeBlank + sngTopY - 12)
                                '最后一行
                                rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, LastLineY)
                            End If
                        Else
                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime10LineColor), TimeTablePara.DiagramStylePara.OneTime10LineWidth)
                            curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime10LineStyle)
                            rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)
                            strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                            'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                            '打印时间文字,第一行
                            If DiagramTimeLineFont = "全部" Then
                                rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - sngTimeBlank + sngTopY - 12)
                                '最后一行
                                rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, LastLineY)
                            End If
                        End If
                    End If
                    If i <= intTimeLine Then
                        For j = 1 To 9
                            If j = 5 Then
                                curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime5LineColor), TimeTablePara.DiagramStylePara.OneTime5LineWidth)
                                curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime5LineStyle)
                                rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth + j * sngMinuWidth / 10, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth + j * sngMinuWidth / 10, sngY2)
                            Else
                                curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime1LineColor), TimeTablePara.DiagramStylePara.OneTime1LineWidth)
                                curPen.DashStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.OneTime1LineStyle)
                                rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth + j * sngMinuWidth / 10, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth + j * sngMinuWidth / 10, sngY2)
                            End If
                        Next j
                    End If
                Next i
        End Select
    End Sub

    '画技术图解运行线
    Public Sub DrawJiShuTuJieDiagramLine(ByVal rBmpGraphics As Graphics, ByVal intToWidth As Integer, ByVal intFirTime As Integer, ByVal nTimeWidth As Integer, ByVal intLeftBlank As Integer, ByVal intStaBlank As Integer, ByVal intLeftX As Integer, ByVal nGuDaoHeight As Integer)

        If UBound(GuDaoJishutujie.GuDaoYcoord) > 0 Then
            Dim i As Integer
            Dim j As Integer
            'Dim k As Integer
            Dim X1, X2 As Single
            Dim Y1, Y2 As Single
            Dim X3, Y3, X4, Y4 As Single
            Dim UpX, UpY As Single
            Dim zX, zY As Single
            Dim zX1, zY1 As Single
            Dim UpStaX, UpStaY As Single
            Dim DownX, DownY As Single
            Dim DownStaX, DownStaY As Single
            Dim intTime1 As Integer
            Dim intTime2 As Integer
            Dim tmpPen As Pen
            Dim tmpPenUp As Pen
            Dim tmpPenDown As Pen
            Dim tmpPenDownLine As Pen
            Dim tmpPenUpLine As Pen
            Dim tmpPenReturn As Pen
            Dim tmpReturnUp As Pen
            Dim tmpReturnDown As Pen
            Dim sngToWidth As Single
            Dim tmpDownFont As Font
            Dim tmpUpFont As Font
            Dim tmpDownBrush As Brush
            Dim tmpUpBrush As Brush
            Dim tmpStopTime As String
            sngToWidth = (intToWidth - intLeftBlank * 2 - intStaBlank - intLeftX) * 24 / nTimeWidth
            Dim sngLeftX As Single
            Dim sngRightX As Single
            sngLeftX = FormTimeToXCord(intFirTime * 3600, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
            sngRightX = FormTimeToXCord(TimeAdd(intFirTime * 3600, nTimeWidth * 3600), intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
            If sngRightX = sngLeftX Then
                sngRightX = sngLeftX + intToWidth - 2 * intLeftBlank - intStaBlank - intLeftX
            End If
            Dim tmpHeight As Integer
            tmpHeight = 40
            tmpPen = New Pen(Color.Red, 1)
            tmpPenUp = New Pen(Color.Blue, 10)
            tmpPenDown = New Pen(Color.Red, 10)
            tmpPenReturn = New Pen(Color.Green, 10)
            tmpPenDownLine = New Pen(Color.Red, 1)
            tmpPenUpLine = New Pen(Color.Blue, 1)
            tmpReturnUp = New Pen(Color.Blue, 2)
            tmpReturnDown = New Pen(Color.Red, 2)
            tmpDownFont = New Font("黑体", 10)
            tmpUpFont = tmpDownFont
            tmpDownBrush = Brushes.Red
            tmpUpBrush = Brushes.Blue
            For j = 1 To UBound(TrainInf)
                If TrainInf(j).Train <> "" Then
                    'If j = 21 Then Stop
                    If UBound(TrainInf(j).nPassSection) > 0 Then
                        If j Mod 2 <> 0 Then '下行
                            If StationInf(SectionInf(TrainInf(j).nPassSection(1)).nFirStaID).sStationName = StationInf(GuDaoJishutujie.nSta).sStationName Then
                                intTime1 = TrainInf(j).Arrival(GuDaoJishutujie.nSta) 'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                                X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                Y1 = GetCurGuDaoYCood(TrainInf(j).StopLine(GuDaoJishutujie.nSta)) '+ tmpHeight / 2
                                intTime2 = TrainInf(j).Starting(GuDaoJishutujie.nSta) 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                                X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                Y2 = Y1
                                UpStaX = X2 + 20
                                UpStaY = GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(1)).nSecStaID).sStationName, 2)
                                UpX = X2
                                UpY = GuDaoJishutujie.GuDaoYcoord(UBound(GuDaoJishutujie.GuDaoYcoord)) + nGuDaoHeight
                                tmpStopTime = SecondToMinute(TimeMinus(intTime2, intTime1))

                                If X1 > 0 And Y1 > 0 And X2 > 0 And Y2 > 0 Then
                                    If X2 >= X1 Then
                                        If X1 <= sngRightX Then
                                            If X2 <= sngRightX Then
                                                rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                                rBmpGraphics.DrawLine(tmpPenDown, X1, Y1, X2, Y2)
                                                Call WriteStrInLine(rBmpGraphics, tmpStopTime, tmpDownFont, tmpDownBrush, X1, Y1, X2, Y2, 2)
                                                Call WriteStrInLine(rBmpGraphics, TrainInf(j).sPrintTrain, tmpDownFont, tmpDownBrush, UpX - 2, UpY + 2, UpStaX - 2, UpStaY + 2, 3)
                                                'rBmpGraphics.DrawString(TrainInf(j).sPrintTrain, tmpDownFont, tmpDownBrush, UpStaX - 12, UpStaY - 18)
                                            Else
                                                X3 = sngRightX
                                                Y3 = Y1
                                                rBmpGraphics.DrawLine(tmpPenDown, X1, Y1, X3, Y3)
                                            End If
                                        End If
                                    Else
                                        X4 = X1 - sngToWidth
                                        Y4 = Y1
                                        X3 = intLeftBlank + intStaBlank + intLeftX
                                        Y3 = Y1
                                        If UpX <= sngRightX Then
                                            rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                            rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                            Call WriteStrInLine(rBmpGraphics, TrainInf(j).sPrintTrain, tmpDownFont, tmpDownBrush, UpX - 2, UpY + 2, UpStaX - 2, UpStaY + 2, 3)
                                        End If
                                        rBmpGraphics.DrawLine(tmpPenDown, X3, Y3, X2, Y2)
                                    End If
                                End If

                                '始发折返
                                'If j = 35 Then Stop

                                If TrainInf(j).TrainReturn(1) > 0 Then
                                    intTime1 = TrainInf(j).sStartZFArrival  'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                                    X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                    Y1 = GetCurGuDaoYCood(TrainInf(j).sStartZFLine)
                                    intTime2 = TrainInf(j).sStartZFStarting 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                                    X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                    Y2 = Y1
                                    zY = GetCurGuDaoYCood(TrainInf(j).StopLine(GuDaoJishutujie.nSta))
                                    zX = FormTimeToXCord(TrainInf(j).Arrival(GuDaoJishutujie.nSta), intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                    zY1 = GetCurGuDaoYCood(TrainInf(TrainInf(j).TrainReturn(1)).StopLine(GuDaoJishutujie.nSta))
                                    zX1 = FormTimeToXCord(TrainInf(TrainInf(j).TrainReturn(1)).Starting(GuDaoJishutujie.nSta), intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                    tmpStopTime = SecondToMinute(TimeMinus(intTime2, intTime1))
                                    If X1 > 0 And Y1 > 0 And X2 > 0 And Y2 > 0 Then
                                        If X2 >= X1 Then
                                            If X1 <= sngRightX Then
                                                If X2 <= sngRightX Then
                                                    rBmpGraphics.DrawLine(tmpPenReturn, X1, Y1, X2, Y2)
                                                    rBmpGraphics.DrawLine(tmpReturnDown, X2, Y2, zX, zY)
                                                    If zX1 <= sngRightX Then
                                                        rBmpGraphics.DrawLine(tmpReturnUp, X1, Y1, zX1, zY1)
                                                    End If
                                                    Call WriteStrInLine(rBmpGraphics, tmpStopTime, tmpDownFont, tmpDownBrush, X1, Y1, X2, Y2, 2)
                                                Else
                                                    X3 = sngRightX
                                                    Y3 = Y1
                                                    rBmpGraphics.DrawLine(tmpReturnUp, X1, Y1, zX1, zY1)
                                                    rBmpGraphics.DrawLine(tmpPenReturn, X1, Y1, X3, Y3)
                                                End If
                                            End If
                                        Else
                                            If X1 - X2 >= sngToWidth / 2 Then
                                                X4 = X1 - sngToWidth
                                                Y4 = Y1
                                                X3 = intLeftBlank + intStaBlank + intLeftX
                                                Y3 = Y1
                                                rBmpGraphics.DrawLine(tmpPenReturn, X1, Y1, X3 + sngToWidth, Y3)
                                                rBmpGraphics.DrawLine(tmpPenReturn, X3, Y3, X2, Y2)
                                                rBmpGraphics.DrawLine(tmpReturnDown, X2, Y2, zX, zY)
                                                rBmpGraphics.DrawLine(tmpReturnUp, X1, Y1, zX1, zY1)

                                            End If
                                        End If
                                    End If
                                End If
                            Else
                                For i = 1 To UBound(TrainInf(j).nPassSection)
                                    If StationInf(SectionInf(TrainInf(j).nPassSection(i)).nSecStaID).sStationName = StationInf(GuDaoJishutujie.nSta).sStationName Then
                                        intTime1 = TrainInf(j).Arrival(GuDaoJishutujie.nSta) 'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                                        X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                        Y1 = GetCurGuDaoYCood(TrainInf(j).StopLine(GuDaoJishutujie.nSta))
                                        intTime2 = TrainInf(j).Starting(GuDaoJishutujie.nSta) 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                                        X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                        Y2 = Y1
                                        UpStaX = X1 - 20
                                        UpStaY = GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(i)).nFirStaID).sStationName, 1)
                                        UpX = X1
                                        UpY = GuDaoJishutujie.GuDaoYcoord(1)
                                        tmpStopTime = SecondToMinute(TimeMinus(intTime2, intTime1))

                                        If StationInf(GuDaoJishutujie.nSta).sStationName = TrainInf(j).NextStation Then '终到站
                                        Else
                                            If X2 >= sngLeftX And X2 <= sngRightX Then
                                                DownX = X2
                                                DownY = GuDaoJishutujie.GuDaoYcoord(UBound(GuDaoJishutujie.GuDaoYcoord)) + nGuDaoHeight
                                                DownStaX = X2 + 20
                                                DownStaY = GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(i + 1)).nSecStaID).sStationName, 2)
                                                rBmpGraphics.DrawLine(tmpPen, X2, Y2, DownX, DownY)
                                                rBmpGraphics.DrawLine(tmpPen, DownX, DownY, DownStaX, DownStaY)
                                            End If
                                        End If

                                        If X1 > 0 And Y1 > 0 And X2 > 0 And Y2 > 0 Then
                                            If X1 = X2 Then '不停
                                                rBmpGraphics.DrawEllipse(tmpPenDownLine, X1 - 4, Y1 - 4, 8, 8)
                                            End If

                                            If X2 >= X1 Then
                                                If X1 <= sngRightX Then
                                                    If X2 <= sngRightX Then
                                                        rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                        rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X1, Y1)
                                                        rBmpGraphics.DrawLine(tmpPenDown, X1, Y1, X2, Y2)
                                                        If intTime2 <> intTime1 Then
                                                            Call WriteStrInLine(rBmpGraphics, tmpStopTime, tmpDownFont, tmpDownBrush, X1, Y1, X2, Y2, 2)
                                                        End If
                                                        Call WriteStrInLine(rBmpGraphics, TrainInf(j).sPrintTrain, tmpDownFont, tmpDownBrush, UpX, UpY, UpStaX, UpStaY + 5, 1)
                                                        ' rBmpGraphics.DrawString(TrainInf(j).sPrintTrain, tmpDownFont, tmpDownBrush, UpStaX - 16, UpStaY - 14)
                                                    Else
                                                        X3 = sngRightX
                                                        Y3 = Y1
                                                        rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                        rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X1, Y1)
                                                        'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                        rBmpGraphics.DrawLine(tmpPenDown, X1, Y1, X3, Y3)
                                                        If intTime2 <> intTime1 Then
                                                            Call WriteStrInLine(rBmpGraphics, tmpStopTime, tmpDownFont, tmpDownBrush, X1, Y1, X2, Y2, 2)
                                                        End If
                                                        Call WriteStrInLine(rBmpGraphics, TrainInf(j).sPrintTrain, tmpDownFont, tmpDownBrush, UpX, UpY, UpStaX, UpStaY + 5, 1)
                                                        'rBmpGraphics.DrawString(TrainInf(j).sPrintTrain, tmpDownFont, tmpDownBrush, UpStaX - 16, UpStaY - 14)
                                                    End If
                                                End If
                                            Else
                                                X4 = X1 - sngToWidth
                                                Y4 = Y1
                                                X3 = intLeftBlank + intStaBlank + intLeftX
                                                Y3 = Y1
                                                rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X1, Y1)
                                                'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                rBmpGraphics.DrawLine(tmpPenDown, X1, Y1, X3 + sngToWidth, Y3)
                                                rBmpGraphics.DrawLine(tmpPenDown, X3, Y3, X2, Y2)
                                            End If
                                        End If
                                    End If
                                Next
                            End If

                        Else '上行

                            If StationInf(SectionInf(TrainInf(j).nPassSection(1)).nSecStaID).sStationName = StationInf(GuDaoJishutujie.nSta).sStationName Then
                                intTime1 = TrainInf(j).Arrival(GuDaoJishutujie.nSta) 'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                                X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                Y1 = GetCurGuDaoYCood(TrainInf(j).StopLine(GuDaoJishutujie.nSta))
                                intTime2 = TrainInf(j).Starting(GuDaoJishutujie.nSta) 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                                X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                Y2 = Y1
                                UpStaX = X2 + 20
                                UpStaY = GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(1)).nFirStaID).sStationName, 1)
                                UpX = X2
                                UpY = GuDaoJishutujie.GuDaoYcoord(1)
                                tmpStopTime = SecondToMinute(TimeMinus(intTime2, intTime1))
                                If X1 > 0 And Y1 > 0 And X2 > 0 And Y2 > 0 Then
                                    If X2 >= X1 Then
                                        If X1 <= sngRightX Then
                                            If X2 <= sngRightX Then
                                                rBmpGraphics.DrawLine(tmpPenUpLine, UpStaX, UpStaY, UpX, UpY)
                                                rBmpGraphics.DrawLine(tmpPenUpLine, UpX, UpY, X2, Y2)
                                                rBmpGraphics.DrawLine(tmpPenUp, X1, Y1, X2, Y2)
                                                Call WriteStrInLine(rBmpGraphics, tmpStopTime, tmpUpFont, tmpUpBrush, X1, Y1, X2, Y2, 2)
                                                Call WriteStrInLine(rBmpGraphics, TrainInf(j).sPrintTrain, tmpUpFont, tmpUpBrush, UpX + 2, UpY, UpStaX + 2, UpStaY + 5, 3)
                                                'rBmpGraphics.DrawString(TrainInf(j).sPrintTrain, tmpUpFont, tmpUpBrush, X1 - 12, Y1 + 5)
                                            Else
                                                X3 = sngRightX
                                                Y3 = Y1
                                                If UpX <= sngRightX Then
                                                    rBmpGraphics.DrawLine(tmpPenUpLine, UpStaX, UpStaY, UpX, UpY)
                                                    rBmpGraphics.DrawLine(tmpPenUpLine, UpX, UpY, X2, Y2)
                                                    Call WriteStrInLine(rBmpGraphics, TrainInf(j).sPrintTrain, tmpUpFont, tmpUpBrush, UpX + 2, UpY, UpStaX + 2, UpStaY + 5, 3)
                                                End If
                                                rBmpGraphics.DrawLine(tmpPenUp, X1, Y1, X3, Y3)
                                            End If
                                        End If
                                    Else

                                        X4 = X1 - sngToWidth
                                        Y4 = Y1
                                        X3 = intLeftBlank + intStaBlank + intLeftX
                                        Y3 = Y1
                                        rBmpGraphics.DrawLine(tmpPenUpLine, X1, Y1, X3 + sngToWidth, Y3)
                                        rBmpGraphics.DrawLine(tmpPenUp, X3, Y3, X2, Y2)
                                        rBmpGraphics.DrawLine(tmpPenUpLine, UpStaX, UpStaY, UpX, UpY)
                                        rBmpGraphics.DrawLine(tmpPenUpLine, UpX, UpY, X2, Y2)
                                    End If
                                End If

                                '始发折返
                                'If j = 2 Then Stop
                                If TrainInf(j).TrainReturn(1) > 0 Then
                                    intTime1 = TrainInf(j).sStartZFArrival  'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                                    X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                    Y1 = GetCurGuDaoYCood(TrainInf(j).sStartZFLine)
                                    intTime2 = TrainInf(j).sStartZFStarting 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                                    X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                    Y2 = Y1
                                    zY = GetCurGuDaoYCood(TrainInf(j).StopLine(GuDaoJishutujie.nSta))
                                    zX = FormTimeToXCord(TrainInf(j).Arrival(GuDaoJishutujie.nSta), intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                    zY1 = GetCurGuDaoYCood(TrainInf(TrainInf(j).TrainReturn(1)).StopLine(GuDaoJishutujie.nSta))
                                    zX1 = FormTimeToXCord(TrainInf(TrainInf(j).TrainReturn(1)).Starting(GuDaoJishutujie.nSta), intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                    tmpStopTime = SecondToMinute(TimeMinus(intTime2, intTime1))
                                    If X1 > 0 And Y1 > 0 And X2 > 0 And Y2 > 0 Then
                                        If X2 >= X1 Then
                                            If X1 <= sngRightX Then
                                                If X2 <= sngRightX Then
                                                    rBmpGraphics.DrawLine(tmpPenReturn, X1, Y1, X2, Y2)
                                                    rBmpGraphics.DrawLine(tmpReturnUp, X2, Y2, zX, zY)
                                                    If zX1 <= sngRightX Then
                                                        rBmpGraphics.DrawLine(tmpReturnDown, X1, Y1, zX1, zY1)
                                                    End If
                                                    Call WriteStrInLine(rBmpGraphics, tmpStopTime, tmpUpFont, tmpUpBrush, X1, Y1, X2, Y2, 2)
                                                Else
                                                    X3 = sngRightX
                                                    Y3 = Y1
                                                    rBmpGraphics.DrawLine(tmpReturnDown, X1, Y1, zX1, zY1)
                                                    rBmpGraphics.DrawLine(tmpPenReturn, X1, Y1, X3, Y3)
                                                End If
                                            End If
                                        Else
                                            X4 = X1 - sngToWidth
                                            Y4 = Y1
                                            X3 = intLeftBlank + intStaBlank + intLeftX
                                            Y3 = Y1
                                            rBmpGraphics.DrawLine(tmpPenReturn, X1, Y1, X3 + sngToWidth, Y3)
                                            rBmpGraphics.DrawLine(tmpPenReturn, X3, Y3, X2, Y2)
                                            rBmpGraphics.DrawLine(tmpReturnUp, X2, Y2, zX, zY)
                                            rBmpGraphics.DrawLine(tmpReturnDown, X1, Y1, zX1, zY1)
                                        End If
                                    End If
                                End If


                            Else
                                For i = 1 To UBound(TrainInf(j).nPassSection)
                                    If StationInf(SectionInf(TrainInf(j).nPassSection(i)).nFirStaID).sStationName = StationInf(GuDaoJishutujie.nSta).sStationName Then
                                        intTime1 = TrainInf(j).Arrival(GuDaoJishutujie.nSta) 'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                                        X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                        Y1 = GetCurGuDaoYCood(TrainInf(j).StopLine(GuDaoJishutujie.nSta))
                                        intTime2 = TrainInf(j).Starting(GuDaoJishutujie.nSta) 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                                        X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                        Y2 = Y1
                                        UpStaX = X1 - 20
                                        UpStaY = GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(i)).nSecStaID).sStationName, 2)
                                        UpX = X1
                                        UpY = GuDaoJishutujie.GuDaoYcoord(UBound(GuDaoJishutujie.GuDaoYcoord)) + nGuDaoHeight
                                        tmpStopTime = SecondToMinute(TimeMinus(intTime2, intTime1))
                                        If StationInf(GuDaoJishutujie.nSta).sStationName = TrainInf(j).NextStation Then '终到站

                                        Else
                                            If X2 >= sngLeftX And X2 <= sngRightX Then
                                                DownX = X2
                                                DownY = GuDaoJishutujie.GuDaoYcoord(1)
                                                DownStaX = X2 + 20
                                                DownStaY = GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(i + 1)).nFirStaID).sStationName, 1)
                                                rBmpGraphics.DrawLine(tmpPenUpLine, X2, Y2, DownX, DownY)
                                                rBmpGraphics.DrawLine(tmpPenUpLine, DownX, DownY, DownStaX, DownStaY)
                                                'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                            End If
                                        End If

                                        If X1 > 0 And Y1 > 0 And X2 > 0 And Y2 > 0 Then
                                            If X1 = X2 Then '不停
                                                rBmpGraphics.DrawEllipse(tmpPenUpLine, X1 - 4, Y1 - 4, 8, 8)
                                            End If
                                            If X2 >= X1 Then
                                                If X1 <= sngRightX Then
                                                    If X2 <= sngRightX Then
                                                        rBmpGraphics.DrawLine(tmpPenUpLine, UpStaX, UpStaY, UpX, UpY)
                                                        rBmpGraphics.DrawLine(tmpPenUpLine, UpX, UpY, X1, Y1)
                                                        rBmpGraphics.DrawLine(tmpPenUp, X1, Y1, X2, Y2)
                                                        If intTime1 <> intTime2 Then
                                                            Call WriteStrInLine(rBmpGraphics, tmpStopTime, tmpUpFont, tmpUpBrush, X1, Y1, X2, Y2, 2)
                                                        End If
                                                        Call WriteStrInLine(rBmpGraphics, TrainInf(j).sPrintTrain, tmpUpFont, tmpUpBrush, UpX + 3, UpY, UpStaX + 3, UpStaY, 1)
                                                        ' rBmpGraphics.DrawString(TrainInf(j).sPrintTrain, tmpUpFont, tmpUpBrush, UpStaX - 16, UpStaY)
                                                    Else
                                                        X3 = sngRightX
                                                        Y3 = Y1
                                                        rBmpGraphics.DrawLine(tmpPenUpLine, UpStaX, UpStaY, UpX, UpY)
                                                        rBmpGraphics.DrawLine(tmpPenUpLine, UpX, UpY, X1, Y1)
                                                        'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                        rBmpGraphics.DrawLine(tmpPenUp, X1, Y1, X3, Y3)
                                                        If intTime1 <> intTime2 Then
                                                            Call WriteStrInLine(rBmpGraphics, tmpStopTime, tmpUpFont, tmpUpBrush, X1, Y1, X2, Y2, 2)
                                                        End If
                                                        Call WriteStrInLine(rBmpGraphics, TrainInf(j).sPrintTrain, tmpUpFont, tmpUpBrush, UpX + 3, UpY, UpStaX + 3, UpStaY, 1)
                                                        'rBmpGraphics.DrawString(TrainInf(j).sPrintTrain, tmpUpFont, tmpUpBrush, UpStaX - 16, UpStaY)
                                                    End If
                                                End If
                                            Else
                                                X4 = X1 - sngToWidth
                                                Y4 = Y1
                                                X3 = intLeftBlank + intStaBlank + intLeftX
                                                Y3 = Y1
                                                rBmpGraphics.DrawLine(tmpPenUpLine, X1, Y1, X3 + sngToWidth, Y3)
                                                'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                rBmpGraphics.DrawLine(tmpPenUp, X3, Y3, X2, Y2)
                                            End If
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    '查找当前股道的Y坐标
    'gujianghe begin
    Public Function GetCurGuDaoYCood(ByVal sGuDaoNum As String) As Single
        Dim i As Integer
        GetCurGuDaoYCood = 0
        For i = 1 To UBound(GuDaoJishutujie.sGuDao)
            If GuDaoJishutujie.sGuDao(i) = sGuDaoNum Then
                GetCurGuDaoYCood = GuDaoJishutujie.GuDaoYcoord(i) + JishutujieVerInterval / 4
                Exit For
            End If
        Next
    End Function 'gujianghe end 


    '根据Y坐标查找股道编号
    Public Function GetCurGuDaoBianHaoFromYCoord(ByVal Y As Single) As Integer
        Dim i As Integer
        GetCurGuDaoBianHaoFromYCoord = 0
        For i = 1 To UBound(GuDaoJishutujie.sGuDao)
            If Y > GuDaoJishutujie.GuDaoYcoord(i) And Y < GuDaoJishutujie.GuDaoYcoord(i) + JishutujieVerInterval / 2 Then
                GetCurGuDaoBianHaoFromYCoord = i 'GuDaoJishutujie.sGuDao(i)
                Exit For
            End If
        Next
    End Function
    '查找当前车站的Y坐标
    Public Function GetCurJiShuTuJieStaYCood(ByVal sStaName As String, ByVal nUporDown As Integer) As Single
        Dim i As Integer
        GetCurJiShuTuJieStaYCood = 0
        If nUporDown = 1 Then '上面
            For i = 1 To UBound(GuDaoJishutujie.nUpSta)
                If StationInf(GuDaoJishutujie.nUpSta(i)).sStationName = sStaName Then
                    GetCurJiShuTuJieStaYCood = GuDaoJishutujie.UpStaYcoord(i)
                    Exit For
                End If
            Next
        Else
            For i = 1 To UBound(GuDaoJishutujie.nDownSta)
                If StationInf(GuDaoJishutujie.nDownSta(i)).sStationName = sStaName Then
                    GetCurJiShuTuJieStaYCood = GuDaoJishutujie.DownStaYcoord(i)
                    Exit For
                End If
            Next
        End If
    End Function

    '在标头上显示列车信息
    Public Sub ShowLabInfor(ByVal nTrain As Integer, ByVal labName As Label)
        If nTrain > 0 Then
            Dim nCdid As Integer
            Dim sStime As String
            Dim sEtime As String

            nCdid = CheCiToCheDiID(nTrain)

            sStime = SecondToHour(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)), 0)
            sEtime = SecondToHour(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))), 0)

            Dim nBeTrain As Integer
            Dim nAfTrain As Integer
            Dim sBeJGtime As String
            Dim sAfJGtime As String

            sBeJGtime = "无列车"
            sAfJGtime = "无列车"

            '查找当前列车前面的列车

            nAfTrain = nFindAfterTrain(nTrain, TrainInf(nTrain).nPathID(1), SectionInf(TrainInf(nTrain).nPassSection(1)).sSecName)
            If nAfTrain <> 0 Then
                sAfJGtime = SecondToMinute(AddLitterTime(TrainInf(nAfTrain).Starting(TrainInf(nAfTrain).nPathID(1))) - AddLitterTime(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))))
            End If

            nBeTrain = nFindBeforeTrain(nTrain, TrainInf(nTrain).nPathID(1), SectionInf(TrainInf(nTrain).nPassSection(1)).sSecName)
            If nBeTrain <> 0 Then
                sBeJGtime = SecondToMinute(AddLitterTime(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))) - AddLitterTime(TrainInf(nBeTrain).Starting(TrainInf(nBeTrain).nPathID(1))))
            End If
            labName.Text = " ID:" & nTrain & " 车次:" & TrainInf(nTrain).Train & " 输出车次：" & TrainInf(nTrain).sPrintTrain & _
          " |运行标尺:" & TrainInf(nTrain).sRunScaleName & " |停站标尺:" & TrainInf(nTrain).sStopSclaeName & _
         " |始发时间:" & sStime & " |终到时间:" & sEtime & " |前行间隔:" & sBeJGtime & " |后行间隔:" & sAfJGtime '" |列车类型:" & TrainInf(nTrainNumberYXT).SCheDiLeiXing & ChediInfo(nCdid).sCheDiID & ",  列车径路：" & TrainInf(nTrainNumberYXT).sTrainPath
        Else
            labName.Text = "在此显示相关信息"
        End If
    End Sub

    '在底图上画股道选择线，考虑过渡时的情况
    Public Sub DrawJiShuTuJieGuDaoLineInPicture(ByVal nTrain As Integer, ByVal rBmpGraphics As Graphics, ByVal tmpPen As Pen, ByVal intToWidth As Integer, ByVal intFirTime As Integer, ByVal nTimeWidth As Integer, ByVal intLeftBlank As Integer, ByVal intStaBlank As Integer, ByVal intLeftX As Integer, ByVal nSta As Integer, ByVal nGuDaoID As String) ', ByVal sngLeftX As Single, ByVal sngRightX As Single)
        Dim X1, X2 As Single
        Dim Y1, Y2 As Single
        Dim intTime1 As Integer
        Dim intTime2 As Integer
        Dim sngToWidth As Single
        sngToWidth = (intToWidth - intLeftBlank * 2 - intStaBlank - intLeftX) * 24 / nTimeWidth
        Dim sngLeftX As Single
        Dim sngRightX As Single
        sngLeftX = FormTimeToXCord(intFirTime * 3600, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        sngRightX = FormTimeToXCord(TimeAdd(intFirTime * 3600, nTimeWidth * 3600), intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        If sngRightX = sngLeftX Then
            sngRightX = sngLeftX + intToWidth - 2 * intLeftBlank - intStaBlank - intLeftX
        End If

        If GuDaoJishutujie.sCurSeleteState = "车站股道" Then
            intTime1 = TrainInf(nTrain).Arrival(nSta) 'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
            X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
            Y1 = GuDaoJishutujie.GuDaoYcoord(nGuDaoID) + JishutujieVerInterval / 4
            intTime2 = TrainInf(nTrain).Starting(nSta) 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
            X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
            Y2 = Y1
        ElseIf GuDaoJishutujie.sCurSeleteState = "始发折返" Then
            intTime1 = TrainInf(nTrain).sStartZFArrival
            X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
            Y1 = GuDaoJishutujie.GuDaoYcoord(nGuDaoID) + JishutujieVerInterval / 4
            intTime2 = TrainInf(nTrain).sStartZFStarting
            X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
            Y2 = Y1
        End If

        If X2 < X1 Then
            X2 = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth - TimeTablePara.TimeTableDiagramPara.sngLeftBlank
        End If
        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
        rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
        rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
        GuDaoJishutujie.CurSelectedLineX1 = X1
        GuDaoJishutujie.CurSelectedLineY1 = Y1
        GuDaoJishutujie.CurSelectedLineX2 = X2
        GuDaoJishutujie.CurSelectedLineY2 = Y2
    End Sub


    '在底图上画线，考虑过渡时的情况
    Public Sub TMSDrawLineInPicture(ByVal nTrain As Integer, _
                                                ByVal rBmpGraphics As Graphics, _
                                                ByVal tmpPen As Pen, _
                                                ByVal intToWidth As Integer, _
                                                ByVal intFirTime As Integer, _
                                                ByVal nTimeWidth As Integer, _
                                                ByVal intLeftBlank As Integer, _
                                                ByVal intStaBlank As Integer, _
                                                ByVal intLeftX As Integer, _
                                                ByVal nifPrintCheCi As Boolean, ByVal nIfPrintXieCheCi As Boolean) ', ByVal sngLeftX As Single, ByVal sngRightX As Single)

        If nTrain = 0 Then Exit Sub
        Dim i As Integer
        Dim X1, X2 As Single
        Dim Y1, Y2 As Single
        Dim X3, Y3 As Single
        Dim X4, Y4 As Single
        Dim intTime1 As Integer
        Dim intTime2 As Integer
        Dim sngToWidth As Single
        'Dim sInfor As New Generic.List(Of String)
        Dim beY As Single
        Dim printText As String
        printText = ""
        sngToWidth = (intToWidth - intLeftBlank * 2 - intStaBlank - intLeftX) * 24 / nTimeWidth
        Dim sngLeftX As Single
        Dim sngRightX As Single
        sngLeftX = FormTimeToXCord(intFirTime * 3600, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        sngRightX = FormTimeToXCord(TimeAdd(intFirTime * 3600, nTimeWidth * 3600), intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        If sngRightX = sngLeftX Then
            sngRightX = sngLeftX + intToWidth - 2 * intLeftBlank - intStaBlank - intLeftX
        End If

        Dim f As Font
        Dim tmpBrush As Brush
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
        tmpBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.XieCheCiFontColor))

        Dim ForX1, ForY1 As Single
        Dim ForX2, ForY2 As Single
        Dim tmpArriTime As Long
        Dim tmpX As Single
        Dim nPrintID As Integer
        If UBound(TrainInf(nTrain).nPassSection) > 0 Then
            If UBound(TrainInf(nTrain).nPassSection) >= 4 Then
                nPrintID = 2
            Else
                nPrintID = 1
            End If
            For i = 1 To UBound(TrainInf(nTrain).nPassSection)
                intTime1 = TrainInf(nTrain).Starting(TrainInf(nTrain).nFirstID(i)) 'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                Y1 = StationInf(TrainInf(nTrain).nFirstID(i)).YPicValue
                intTime2 = TrainInf(nTrain).Arrival(TrainInf(nTrain).nSecondID(i)) 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                Y2 = StationInf(TrainInf(nTrain).nSecondID(i)).YPicValue
                If i = 1 Then
                    If StationInf(TrainInf(nTrain).nFirstID(i)).sStaProperity = "环形终端站" Then
                        If TrainInf(nTrain).TrainReturn(1) <> 0 Then
                            If (nTrain + TrainInf(nTrain).TrainReturn(1)) Mod 2 = 0 Then
                                If TrainInf(nTrain).ComeStation <> TrainInf(TrainInf(nTrain).TrainReturn(1)).NextStation Then
                                    If GetSystemStaName(TrainInf(nTrain).ComeStation) = GetSystemStaName(TrainInf(TrainInf(nTrain).TrainReturn(1)).NextStation) Then
                                        If StationInf(TrainInf(TrainInf(nTrain).TrainReturn(1)).nSecondID(UBound(TrainInf(TrainInf(nTrain).TrainReturn(1)).nSecondID))).sStaProperity = "环形终端站" Then
                                            tmpArriTime = GetTrainArriTime(TrainInf(nTrain).TrainReturn(1), TrainInf(TrainInf(nTrain).TrainReturn(1)).NextStation)
                                            tmpX = FormTimeToXCord(tmpArriTime, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                            If tmpX <> -1 And tmpX <= sngRightX Then
                                                If nTrain Mod 2 = 0 Then
                                                    rBmpGraphics.DrawLine(tmpPen, tmpX - 12, Y1 + 15, tmpX, Y1)
                                                Else
                                                    rBmpGraphics.DrawLine(tmpPen, tmpX - 12, Y1 - 15, tmpX, Y1)
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                If i = UBound(TrainInf(nTrain).nPassSection) Then
                    '  If nTrain = 2 Then Stop
                    If StationInf(TrainInf(nTrain).nSecondID(i)).sStaProperity = "环形终端站" Then
                        If TrainInf(nTrain).TrainReturn(2) <> 0 Then
                            If (nTrain + TrainInf(nTrain).TrainReturn(2)) Mod 2 = 0 Then
                                If TrainInf(nTrain).NextStation <> TrainInf(TrainInf(nTrain).TrainReturn(2)).ComeStation Then
                                    If GetSystemStaName(TrainInf(nTrain).NextStation) = GetSystemStaName(TrainInf(TrainInf(nTrain).TrainReturn(2)).ComeStation) Then
                                        If StationInf(TrainInf(TrainInf(nTrain).TrainReturn(2)).nFirstID(1)).sStaProperity = "环形终端站" Then
                                            tmpArriTime = GetTrainStartTime(TrainInf(nTrain).TrainReturn(2), TrainInf(TrainInf(nTrain).TrainReturn(2)).ComeStation)
                                            tmpX = FormTimeToXCord(tmpArriTime, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                            If tmpX <> -1 And tmpX <= sngRightX Then
                                                If nTrain Mod 2 = 0 Then
                                                    rBmpGraphics.DrawLine(tmpPen, tmpX + 12, Y2 - 15, tmpX, Y2)
                                                Else
                                                    rBmpGraphics.DrawLine(tmpPen, tmpX + 12, Y2 + 15, tmpX, Y2)
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                If X1 >= 0 And Y1 >= 0 And X2 >= 0 And Y2 >= 0 Then
                    If X2 >= X1 Then
                        If X1 <= sngRightX Then
                            If i = nPrintID Then
                                If nIfPrintXieCheCi = True Then '打印车次
                                    If TrainInf(nTrain).sPrintTrain.Trim <> "" Then
                                        If TimeTablePara.TimeTableDiagramPara.sCheCiShowStyle = 1 Then
                                            printText = ChediInfo(CheCiToCheDiID(nTrain)).sCheCiHao & TrainInf(nTrain).sPrintTrain  '.Substring(0, 3)
                                        Else
                                            printText = TrainInf(nTrain).sPrintTrain  '.Substring(0, 3)
                                        End If
                                        Call WriteStrInLine(rBmpGraphics, printText, f, tmpBrush, X1, Y1, X2, Y2, 4)
                                    End If
                                End If
                            End If

                            If X2 <= sngRightX Then
                                rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                                If Y1 <> beY And i > 1 Then
                                    If nIfPrintXieCheCi = True Then
                                        If TrainInf(nTrain).sPrintTrain.Trim <> "" Then
                                            If TimeTablePara.TimeTableDiagramPara.sCheCiShowStyle = 1 Then
                                                printText = ChediInfo(CheCiToCheDiID(nTrain)).sCheCiHao & TrainInf(nTrain).sPrintTrain  '.Substring(0, 3)
                                            Else
                                                printText = TrainInf(nTrain).sPrintTrain  '.Substring(0, 3)
                                            End If
                                            Call WriteStrInLine(rBmpGraphics, printText, f, tmpBrush, X1, Y1, X2, Y2, 4)
                                        End If
                                    End If
                                End If

                                TrainInf(nTrain).sngFirXcoord(i) = X1
                                TrainInf(nTrain).sngFirYcoord(i) = Y1
                                TrainInf(nTrain).sngSecXcoord(i) = X2
                                TrainInf(nTrain).sngSecYcoord(i) = Y2
                                TrainInf(nTrain).bIfPassFenLine = 0
                                beY = Y2
                            Else
                                X3 = sngRightX
                                Y3 = ((X3 - X1) * (Y2 - Y1) / (X2 - X1)) + Y1
                                rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                                ' rBmpGraphics.DrawLine(tmpPen, X3, Y3, X2, Y2)
                                TrainInf(nTrain).sngFirXcoord(i) = X1
                                TrainInf(nTrain).sngFirYcoord(i) = Y1
                                TrainInf(nTrain).sngSecXcoord(i) = X3
                                TrainInf(nTrain).sngSecYcoord(i) = Y3
                            End If
                        End If
                    Else
                        X4 = X1 - sngToWidth
                        Y4 = Y1
                        X3 = intLeftBlank + intStaBlank + intLeftX
                        Y3 = ((X3 - X4) * (Y2 - Y4) / (X2 - X4)) + Y4
                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3 + sngToWidth, Y3)
                        rBmpGraphics.DrawLine(tmpPen, X3, Y3, X2, Y2)
                        TrainInf(nTrain).sngFirXcoord(i) = X1
                        TrainInf(nTrain).sngFirYcoord(i) = Y1
                        TrainInf(nTrain).sngSecXcoord(i) = X2
                        TrainInf(nTrain).sngSecYcoord(i) = Y2
                        TrainInf(nTrain).bIfPassFenLine = 1
                        TrainInf(nTrain).sngPassLineX1Coord = X3 + sngToWidth
                        TrainInf(nTrain).sngPassLineY1Coord = Y3
                        TrainInf(nTrain).sngPassLineX2Coord = X3
                        TrainInf(nTrain).sngPassLineX2Coord = Y3
                        'If TimeTablePara.TimeTableDiagramPara.sCheCiLength = 6 Then
                        '    printText = ChediInfo(CheCiToCheDiID(nTrain)).sPrintCheCiHao & TrainInf(nTrain).sPrintTrain  '.Substring(0, 3)
                        'Else
                        '    printText = TrainInf(nTrain).sPrintTrain  '.Substring(0, 3)
                        'End If
                        'Call WriteStrInLine(rBmpGraphics, printText, f, tmpBrush, X3, Y3, X4, Y4, 4)

                    End If
                End If

                '车站连接横线
                ForX2 = X1
                ForY2 = Y1
                ForY1 = Y1
                tmpArriTime = TrainInf(nTrain).Arrival(TrainInf(nTrain).nFirstID(i))
                ForX1 = FormTimeToXCord(tmpArriTime, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                'sInfor.Add(ForX1 & "/" & ForX2)
                If ForX1 >= 0 And ForY1 >= 0 And ForX2 >= 0 And ForY2 >= 0 Then
                    If ForX2 >= ForX1 Then
                        If ForX1 <= sngRightX Then
                            If ForX2 <= sngRightX Then
                                If i > 1 Then
                                    rBmpGraphics.DrawLine(tmpPen, ForX1, ForY1, ForX2, ForY2)
                                End If
                                'Else
                                '    X3 = sngRightX
                                '    Y3 = ((X3 - ForX1) * (ForY2 - ForY1) / (ForX2 - ForX1)) + ForY1
                                '    rBmpGraphics.DrawLine(tmpPen, ForX1, ForY1, X3, Y3)
                            End If
                        End If
                        'Else
                        '    X4 = ForX1 - sngToWidth
                        '    Y4 = ForY1
                        '    X3 = intLeftBlank + intStaBlank + intLeftX
                        '    Y3 = ((X3 - X4) * (ForY2 - Y4) / (ForX2 - X4)) + Y4
                        '    rBmpGraphics.DrawLine(tmpPen, ForX1, ForY1, X3 + sngToWidth, Y3)
                        '    rBmpGraphics.DrawLine(tmpPen, X3, Y3, ForX2, ForY2)
                    End If
                End If

                ForX1 = X2
                ForY1 = Y2
                ForY2 = Y2
                tmpArriTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nSecondID(i))
                ForX2 = FormTimeToXCord(tmpArriTime, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                If ForX1 >= 0 And ForY1 >= 0 And ForX2 >= 0 And ForY2 >= 0 Then
                    If ForX2 >= ForX1 Then
                        If ForX1 <= sngRightX Then
                            If ForX2 <= sngRightX Then
                                If i < UBound(TrainInf(nTrain).nPassSection) Then
                                    rBmpGraphics.DrawLine(tmpPen, ForX1, ForY1, ForX2, ForY2)
                                End If
                                'Else
                                '    X3 = sngRightX
                                '    Y3 = ((X3 - ForX1) * (ForY2 - ForY1) / (ForX2 - ForX1)) + ForY1
                                '    rBmpGraphics.DrawLine(tmpPen, ForX1, ForY1, X3, Y3)
                            End If
                        End If
                        'Else
                        '    X4 = ForX1 - sngToWidth
                        '    Y4 = ForY1
                        '    X3 = intLeftBlank + intStaBlank + intLeftX
                        '    Y3 = ((X3 - X4) * (ForY2 - Y4) / (ForX2 - X4)) + Y4
                        '    rBmpGraphics.DrawLine(tmpPen, ForX1, ForY1, X3 + sngToWidth, Y3)
                        '    rBmpGraphics.DrawLine(tmpPen, X3, Y3, ForX2, ForY2)
                    End If
                End If
            Next
        End If
        'Dim txt As String
        'For i = 1 To sInfor.Count
        '    txt = txt & "--" & sInfor.Item(i - 1)
        'Next
        'MsgBox(txt)
    End Sub

    '环形交路时，选中后显示接续的列车
    Public Sub DrawLinkCircleTrain(ByVal nTrain As Integer, _
                                        ByVal rBmpGraphics As Graphics, _
                                        ByVal tmpPen As Pen, _
                                        ByVal intToWidth As Integer, _
                                        ByVal intFirTime As Integer, _
                                        ByVal nTimeWidth As Integer, _
                                        ByVal intLeftBlank As Integer, _
                                        ByVal intStaBlank As Integer, _
                                        ByVal intLeftX As Integer, _
                                        ByVal nifPrintCheCi As Boolean, ByVal nIfPrintXieCheCi As Boolean) ', ByVal sngLeftX As Single, ByVal sngRightX As Single)

        If nTrain = 0 Then Exit Sub

        'Dim i As Integer
        'Dim X1, X2 As Single
        'Dim Y1, Y2 As Single
        'Dim intTime1 As Integer
        'Dim intTime2 As Integer
        'Dim sngToWidth As Single

        'Dim printText As String
        'printText = ""
        'sngToWidth = (intToWidth - intLeftBlank * 2 - intStaBlank - intLeftX) * 24 / nTimeWidth
        'Dim sngLeftX As Single
        'Dim sngRightX As Single
        'sngLeftX = FormTimeToXCord(intFirTime * 3600, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        'sngRightX = FormTimeToXCord(TimeAdd(intFirTime * 3600, nTimeWidth * 3600), intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        'If sngRightX = sngLeftX Then
        '    sngRightX = sngLeftX + intToWidth - 2 * intLeftBlank - intStaBlank - intLeftX
        'End If

        Dim nNextTrain As Integer
        nNextTrain = GetCheDiNextTrain(nTrain)
        If nNextTrain > 0 Then
            Call TMSDrawLineInPicture(nNextTrain, rBmpGraphics, tmpPen, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX, TimeTablePara.TimeTableDiagramPara.nifPrintCheCi, TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)

            'If UBound(TrainInf(nNextTrain).nPassSection) > 0 Then
            '    For i = 1 To UBound(TrainInf(nNextTrain).nPassSection)
            '        intTime1 = TrainInf(nNextTrain).Starting(TrainInf(nNextTrain).nFirstID(i)) 'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
            '        X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
            '        Y1 = StationInf(TrainInf(nNextTrain).nFirstID(i)).YPicValue
            '        intTime2 = TrainInf(nNextTrain).Arrival(TrainInf(nNextTrain).nSecondID(i)) 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
            '        X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
            '        Y2 = StationInf(TrainInf(nNextTrain).nSecondID(i)).YPicValue
            '        If X1 >= 0 And Y1 >= 0 And X2 >= 0 And Y2 >= 0 Then
            '            rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
            '        End If
            '    Next
            'End If
        End If

        nNextTrain = GetCheDiUpTrain(nTrain)
        If nNextTrain > 0 Then
            Call TMSDrawLineInPicture(nNextTrain, rBmpGraphics, tmpPen, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX, TimeTablePara.TimeTableDiagramPara.nifPrintCheCi, TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)

            'If UBound(TrainInf(nNextTrain).nPassSection) > 0 Then
            '    For i = 1 To UBound(TrainInf(nNextTrain).nPassSection)
            '        intTime1 = TrainInf(nNextTrain).Starting(TrainInf(nNextTrain).nFirstID(i)) 'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
            '        X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
            '        Y1 = StationInf(TrainInf(nNextTrain).nFirstID(i)).YPicValue
            '        intTime2 = TrainInf(nNextTrain).Arrival(TrainInf(nNextTrain).nSecondID(i)) 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
            '        X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
            '        Y2 = StationInf(TrainInf(nNextTrain).nSecondID(i)).YPicValue
            '        If X1 >= 0 And Y1 >= 0 And X2 >= 0 And Y2 >= 0 Then
            '            rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
            '        End If
            '    Next
            'End If
        End If

    End Sub

    '得到该列车在该车站的到点
    Public Function GetTrainArriTime(ByVal nTrain As Int16, ByVal sStaName As String) As Integer
        Dim i As Int16
        For i = 1 To UBound(TrainInf(nTrain).nPathID)
            If StationInf(TrainInf(nTrain).nPathID(i)).sStationName = sStaName Then
                GetTrainArriTime = TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(i))
                Exit For
            End If
        Next
    End Function

    '得到该列车在该车站的发点
    Public Function GetTrainStartTime(ByVal nTrain As Int16, ByVal sStaName As String) As Integer
        Dim i As Int16
        For i = 1 To UBound(TrainInf(nTrain).nPathID)
            If StationInf(TrainInf(nTrain).nPathID(i)).sStationName = sStaName Then
                GetTrainStartTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(i))
                Exit For
            End If
        Next
    End Function

    '由时间得到X坐标
    Public Function FormTimeToXCord(ByVal intTime As Integer, ByVal intFirTime As Integer, ByVal intTimeWidth As Integer, ByVal intToWidh As Integer, ByVal intLeftBlank As Integer, ByVal intStaBlank As Integer, ByVal intLeftX As Single) As Single
        If intTime = -1 Then
            FormTimeToXCord = -1
        Else
            Dim intWidth As Integer
            intWidth = intTime - intFirTime * 3600
            If intWidth < 0 Then
                intWidth = intWidth + 24 * 3600
            End If
            Dim sngToWidth As Single
            sngToWidth = intToWidh - intLeftBlank * 2 - intStaBlank - intLeftX
            FormTimeToXCord = intLeftX + intLeftBlank + intStaBlank + intWidth * sngToWidth / (intTimeWidth * 3600)
        End If
    End Function

    '由站名得到车站ID
    Public Function StaNameToStaInfID(ByVal sStaName As String) As Integer
        Dim i As Integer
        For i = 1 To UBound(StationInf)
            If StationInf(i).sStationName = sStaName Then
                StaNameToStaInfID = i
                Exit For
            End If
        Next
    End Function

    '由坐标得到列车车次号
    Public Function SeekTrainNumberByXYCoordBack(ByVal X As Single, ByVal Y As Single, ByVal Mark1 As Single, ByVal Mark2 As Single) As Integer
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim sngX1 As Single
        Dim sngY1 As Single
        Dim sngX2 As Single
        Dim sngY2 As Single

        Dim sJGWidth As Single
        sJGWidth = 1000000

        Dim TgAlpha As Single
        Dim StandardX As Single
        Dim AllowWidth As Single
        SeekTrainNumberByXYCoordBack = 0

        Dim nSecNum As Integer
        Dim nFirSta As Integer
        Dim nSecSta As Integer
        For k = 1 To UBound(SectionInf)
            If Y >= StationInf(SectionInf(k).nFirStaID).YPicValue And Y <= StationInf(SectionInf(k).nSecStaID).YPicValue Then
                nFirSta = SectionInf(k).nFirStaID
                nSecSta = SectionInf(k).nSecStaID
                nSecNum = k
                Exit For
            End If
        Next k

        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                For j = 1 To UBound(TrainInf(i).nPassSection)
                    If TrainInf(i).nPassSection(j) = nSecNum Then
                        If TrainInf(i).bIfPassFenLine = 0 Then
                            If X >= TrainInf(i).sngFirXcoord(j) - 20 And X <= TrainInf(i).sngSecXcoord(j) + 20 Then
                                sngX1 = TrainInf(i).sngFirXcoord(j)
                                sngY1 = TrainInf(i).sngFirYcoord(j)
                                sngX2 = TrainInf(i).sngSecXcoord(j)
                                sngY2 = TrainInf(i).sngSecYcoord(j)
                            End If
                        Else
                            If X >= TrainInf(i).sngFirXcoord(j) And X <= TrainInf(i).sngPassLineX1Coord Then
                                sngX1 = TrainInf(i).sngFirXcoord(j)
                                sngY1 = TrainInf(i).sngFirYcoord(j)
                                sngX2 = TrainInf(i).sngPassLineX1Coord
                                sngY2 = TrainInf(i).sngPassLineY1Coord
                            ElseIf X >= TrainInf(i).sngPassLineX2Coord And X <= TrainInf(i).sngSecXcoord(j) Then
                                sngX1 = TrainInf(i).sngPassLineX2Coord
                                sngY1 = TrainInf(i).sngPassLineY2Coord
                                sngX2 = TrainInf(i).sngSecXcoord(j)
                                sngY2 = TrainInf(i).sngSecYcoord(j)
                            End If
                        End If

                        If sngX1 > 0 And sngX2 > 0 And sngY1 > 0 And sngY2 > 0 Then
                            If k Mod 2 <> 0 Then '下行
                                If X >= sngX1 And X <= sngX2 Then
                                    If Y >= sngY1 And Y <= sngY2 Then
                                        TgAlpha = Math.Abs((sngY2 - sngY1) / (sngX2 - sngX1))
                                        If TgAlpha <> 0 Then
                                            StandardX = (Y - sngY1) / TgAlpha + sngX1
                                            AllowWidth = Math.Abs(StandardX - X)
                                        End If
                                    End If
                                End If
                            Else
                                If X >= sngX1 And X <= sngX2 Then
                                    If Y >= sngY2 And Y <= sngY1 Then
                                        TgAlpha = (sngY1 - sngY2) / (sngX2 - sngX1)
                                        If TgAlpha <> 0 Then
                                            StandardX = (sngY1 - Y) / TgAlpha + sngX1
                                            AllowWidth = Math.Abs(StandardX - X)
                                        End If
                                    End If
                                End If
                            End If

                            If AllowWidth >= 0 Then
                                If AllowWidth < sJGWidth Then
                                    sJGWidth = AllowWidth
                                    SeekTrainNumberByXYCoordBack = i
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        Next i

    End Function

    Public Function SeekJiShuTuJieTrainNumberByXYCoord(ByVal X As Single, ByVal Y As Single, ByVal sStaName As String) As Integer
        Dim X1, Y1, X2, Y2 As Single
        Dim sngCurY As Single
        Dim i As Integer
        Dim nSta As Integer
        ' Dim nGuDao As Integer
        'nGuDao = GetCurGuDaoBianHaoFromYCoord(Y)
        'Y1 = GuDaoJishutujie.GuDaoYcoord(nGuDao)
        'Y2 = GuDaoJishutujie.GuDaoYcoord(nGuDao) + 50
        nSta = StaNameToStaInfID(sStaName)
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                X1 = FormTimeToXCord(TrainInf(i).Arrival(nSta), TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                X2 = FormTimeToXCord(TrainInf(i).Starting(nSta), TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                sngCurY = GetCurGuDaoYCood(TrainInf(i).StopLine(nSta))
                Y1 = sngCurY - JishutujieVerInterval / 4
                Y2 = sngCurY + JishutujieVerInterval / 4
                'If X2 < X1 Then
                '    X2 = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth - TimeTablePara.TimeTableDiagramPara.sngLeftBlank
                'End If
                If X >= X1 And X <= X2 Then
                    If Y <= Y2 And Y >= Y1 Then
                        GuDaoJishutujie.sCurSeleteState = "车站股道"
                        SeekJiShuTuJieTrainNumberByXYCoord = i
                        Exit For
                    End If
                End If

                If SeekJiShuTuJieTrainNumberByXYCoord = 0 Then
                    If TrainInf(i).ComeStation = sStaName Then '查始发折返
                        X1 = FormTimeToXCord(TrainInf(i).sStartZFArrival, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                        X2 = FormTimeToXCord(TrainInf(i).sStartZFStarting, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                        sngCurY = GetCurGuDaoYCood(TrainInf(i).sStartZFLine)
                        Y1 = sngCurY - JishutujieVerInterval / 4
                        Y2 = sngCurY + JishutujieVerInterval / 4
                        'If X2 < X1 Then
                        '    X2 = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth - TimeTablePara.TimeTableDiagramPara.sngLeftBlank
                        'End If

                        If X >= X1 And X <= X2 Then
                            If Y <= Y2 And Y >= Y1 Then
                                GuDaoJishutujie.sCurSeleteState = "始发折返"
                                SeekJiShuTuJieTrainNumberByXYCoord = i
                                Exit For
                            End If
                        End If
                    End If
                End If
            End If
        Next
    End Function


    '找列车
    Public Function SeekTrainNumberByXYCoord(ByVal X As Single, ByVal Y As Single, ByVal Mark1 As Single, ByVal Mark2 As Single) As Integer
        Dim i As Integer
        Dim k As Integer
        Dim ArriveTime As Long
        Dim StartTime As Long

        Dim sngX1 As Single
        Dim sngY1 As Single
        Dim sngX2 As Single
        Dim sngY2 As Single

        Dim sJGWidth As Single
        sJGWidth = 1000000

        Dim TgAlpha As Single
        Dim StandardX As Single
        Dim AllowWidth As Single
        SeekTrainNumberByXYCoord = 0

        For k = 1 To UBound(TrainInf)
            If TrainInf(k).Train <> "" Then

                AllowWidth = -1
                ReDim CDJLPrintPlace(0)

                For i = 1 To UBound(TrainInf(k).nPathID)
                    ArriveTime = TrainInf(k).Arrival(TrainInf(k).nPathID(i))
                    StartTime = TrainInf(k).Starting(TrainInf(k).nPathID(i))
                    ReDim Preserve CDJLPrintPlace(UBound(CDJLPrintPlace) + 1)
                    CDJLPrintPlace(UBound(CDJLPrintPlace)).sngX1 = FormTimeToXCord(ArriveTime, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                    CDJLPrintPlace(UBound(CDJLPrintPlace)).sngY1 = StationInf(TrainInf(k).nPathID(i)).YPicValue
                    CDJLPrintPlace(UBound(CDJLPrintPlace)).sngX2 = FormTimeToXCord(StartTime, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                    CDJLPrintPlace(UBound(CDJLPrintPlace)).sngY2 = StationInf(TrainInf(k).nPathID(i)).YPicValue
                Next i

                For i = 1 To UBound(CDJLPrintPlace) - 1
                    sngX1 = CDJLPrintPlace(i).sngX2
                    sngY1 = CDJLPrintPlace(i).sngY2
                    sngX2 = CDJLPrintPlace(i + 1).sngX1
                    sngY2 = CDJLPrintPlace(i + 1).sngY1


                    If k Mod 2 <> 0 Then '下行
                        'If X >= sngX1 And X <= sngX2 Then
                        If Y >= sngY1 And Y <= sngY2 Then
                            TgAlpha = Math.Abs((sngY2 - sngY1) / (sngX2 - sngX1))
                            If TgAlpha <> 0 Then
                                StandardX = (Y - sngY1) / TgAlpha + sngX1
                                AllowWidth = Math.Abs(StandardX - X)
                            End If
                        End If
                        'End If
                    Else
                        ' If X >= sngX1 And X <= sngX2 Then
                        If Y >= sngY2 And Y <= sngY1 Then
                            TgAlpha = (sngY1 - sngY2) / (sngX2 - sngX1)
                            If TgAlpha <> 0 Then
                                StandardX = (sngY1 - Y) / TgAlpha + sngX1
                                AllowWidth = Math.Abs(StandardX - X)
                            End If
                            ' End If
                        End If
                    End If

                    If AllowWidth >= 0 Then
                        If AllowWidth < sJGWidth Then
                            sJGWidth = AllowWidth
                            SeekTrainNumberByXYCoord = k
                        End If
                    End If
                Next i
            End If
        Next k


        '*******************原代码
        'SeekTrainNumberByXYCoord = 0
        'Dim i As Integer
        'Dim MarkStation1 As Integer, MarkStation2 As Integer
        'Dim temp As Single
        'Dim Gap As Single
        'Dim MinGap As Single
        'Dim PointX As Single
        'Dim FindTrain As Integer
        'FindTrain = 0


        'For i = 1 To UBound(StationInf)
        '    If StationInf(i).YPicValue < Y And StationInf(i).YPicValue >= Mark1 Then
        '        Mark1 = StationInf(i).YPicValue
        '        MarkStation1 = i
        '    End If
        '    If StationInf(i).YPicValue > Y And StationInf(i).YPicValue <= Mark2 Then
        '        Mark2 = StationInf(i).YPicValue
        '        MarkStation2 = i
        '    End If
        'Next i
        'temp = 900000
        'Gap = 1000000
        'MinGap = 10000
        'If (Y - Mark1) <= (Mark2 - Y) Then
        '    For i = 1 To UBound(TrainInf)
        '        With TrainInf(i)
        '            If .Train <> "" Then
        '                If Int(i / 2) <> i / 2 Then
        '                    PointX = X - (Y - Mark1) '* (UnitageX / UnitageY)
        '                    If .Starting(MarkStation1) <> -1 Then
        '                        Gap = Math.Abs(PointX - FormTimeToXCord(.Starting(MarkStation1), TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX))
        '                    End If
        '                ElseIf Int(i / 2) = i / 2 Then
        '                    PointX = X + (Y - Mark1)
        '                    If .Arrival(MarkStation1) <> -1 Then
        '                        Gap = Math.Abs(PointX - FormTimeToXCord(.Arrival(MarkStation1), TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX))
        '                    End If
        '                End If
        '                If temp > Gap Then
        '                    temp = Gap
        '                    If temp < 20 Then FindTrain = i
        '                End If
        '            End If
        '        End With
        '    Next i
        'Else
        '    For i = 1 To UBound(TrainInf)
        '        With TrainInf(i)
        '            If .Train <> "" Then
        '                If Int(i / 2) <> i / 2 Then
        '                    PointX = X + (Mark2 - Y)
        '                    If .Arrival(MarkStation2) <> -1 Then
        '                        Gap = Math.Abs(PointX - FormTimeToXCord(.Arrival(MarkStation2), TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX))
        '                    End If
        '                ElseIf Int(i / 2) = i / 2 Then
        '                    PointX = X - (Mark2 - Y)
        '                    If .Starting(MarkStation2) <> -1 Then
        '                        Gap = Math.Abs(PointX - FormTimeToXCord(.Starting(MarkStation2), TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX))
        '                    End If
        '                End If
        '                If temp > Gap Then
        '                    temp = Gap
        '                    If temp < 20 Then FindTrain = i
        '                End If
        '            End If
        '        End With
        '    Next i
        'End If
        'SeekTrainNumberByXYCoord = FindTrain
    End Function

    '修改车站股道
    Public Sub EditStaGuDao(ByVal nTrain As Integer, ByVal sGudaoNum As String, ByVal sStaName As String)
        Dim i As Integer
        For i = 1 To UBound(StationInf)
            If StationInf(i).sStationName = sStaName Then
                TrainInf(nTrain).StopLine(i) = sGudaoNum
            End If
        Next

    End Sub


    '修改当前占用的股道
    Public Sub EditCurTrainOcupyStaGuDao(ByVal nTrain As Integer, ByVal sGuDaoNum As String, ByVal nSta As Integer, ByVal sStyle As String)

        Dim i As Integer
        Dim sStaName As String
        sStaName = StationInf(nSta).sStationName
        If sStaName = TrainInf(nTrain).ComeStation Then '始发站
            If sStyle = "车站股道" Then
                For i = 1 To UBound(StationInf)
                    If StationInf(i).sStationName = sStaName Then
                        TrainInf(nTrain).StopLine(i) = sGuDaoNum
                    End If
                Next
            End If
            If TrainInf(nTrain).TrainReturn(1) > 0 Then
                If TrainInf(nTrain).TrainReturnStyle(1) = "站前折返" Or TrainInf(nTrain).TrainReturnStyle(1) = "立即折返" Then
                    If TrainInf(nTrain).TrainReturn(1) > 0 Then
                        For i = 1 To UBound(StationInf)
                            If StationInf(i).sStationName = TrainInf(TrainInf(nTrain).TrainReturn(1)).NextStation Then
                                TrainInf(TrainInf(nTrain).TrainReturn(1)).StopLine(i) = sGuDaoNum
                            End If
                        Next
                    End If
                    TrainInf(nTrain).sStartZFLine = sGuDaoNum
                    TrainInf(TrainInf(nTrain).TrainReturn(1)).sEndZFLine = sGuDaoNum
                    TrainInf(TrainInf(nTrain).TrainReturn(1)).sEndZFStarting = TrainInf(nTrain).Arrival(nSta)
                    TrainInf(TrainInf(nTrain).TrainReturn(1)).sEndZFArrival = TrainInf(TrainInf(nTrain).TrainReturn(1)).Starting(nSta)
                    TrainInf(nTrain).sStartZFStarting = TrainInf(nTrain).Arrival(nSta)
                    TrainInf(nTrain).sStartZFArrival = TrainInf(TrainInf(nTrain).TrainReturn(1)).Starting(nSta)
                Else
                    If sStyle = "折返股道" Then
                        TrainInf(nTrain).sStartZFLine = sGuDaoNum
                        TrainInf(TrainInf(nTrain).TrainReturn(1)).sEndZFLine = sGuDaoNum
                    End If

                End If
            End If
        ElseIf sStaName = TrainInf(nTrain).NextStation Then '终到站
            If sStyle = "车站股道" Then
                For i = 1 To UBound(StationInf)
                    If StationInf(i).sStationName = sStaName Then
                        TrainInf(nTrain).StopLine(i) = sGuDaoNum
                        Exit For
                    End If
                Next
            End If

            If TrainInf(nTrain).TrainReturn(2) > 0 Then
                If TrainInf(nTrain).TrainReturnStyle(2) = "站前折返" Or TrainInf(nTrain).TrainReturnStyle(2) = "立即折返" Then
                    If TrainInf(nTrain).TrainReturn(2) > 0 Then
                        For i = 1 To UBound(StationInf)
                            If StationInf(i).sStationName = TrainInf(TrainInf(nTrain).TrainReturn(2)).ComeStation Then
                                TrainInf(TrainInf(nTrain).TrainReturn(2)).StopLine(i) = sGuDaoNum
                                TrainInf(nTrain).StopLine(i) = sGuDaoNum
                            End If
                        Next
                    End If
                    TrainInf(nTrain).sEndZFLine = sGuDaoNum
                    TrainInf(TrainInf(nTrain).TrainReturn(2)).sStartZFLine = sGuDaoNum
                    TrainInf(TrainInf(nTrain).TrainReturn(2)).sStartZFArrival = TrainInf(nTrain).Starting(nSta)
                    TrainInf(TrainInf(nTrain).TrainReturn(2)).sStartZFStarting = TrainInf(TrainInf(nTrain).TrainReturn(2)).Arrival(nSta)
                    TrainInf(nTrain).sEndZFArrival = TrainInf(nTrain).Starting(nSta)
                    TrainInf(nTrain).sEndZFStarting = TrainInf(TrainInf(nTrain).TrainReturn(2)).Arrival(nSta)
                Else
                    If sStyle = "折返股道" Then
                        TrainInf(nTrain).sEndZFLine = sGuDaoNum
                        TrainInf(TrainInf(nTrain).TrainReturn(2)).sStartZFLine = sGuDaoNum
                    End If
                End If
            End If
        Else '中间站
            If sStyle = "车站股道" Then
                For i = 1 To UBound(StationInf)
                    If StationInf(i).sStationName = sStaName Then
                        TrainInf(nTrain).StopLine(i) = sGuDaoNum
                    End If
                Next
            End If
        End If
    End Sub

End Module
