Imports System
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Module ModCSTimeTableMain
    '刷新运行图
    Public Sub CSRefreshDiagram(Optional ByVal RPara As Integer = 1)
        GC.Collect()
        Dim rbmpGraphics As Graphics
        Dim tmprbmP As Bitmap
        Dim rbmpStaGraphics As Graphics
        Dim tmprbmPSta As Bitmap
        Dim rbmpStaGraphics2 As Graphics
        Dim tmprbmPSta2 As Bitmap
        If CSTimeTablePara.picPubDiagram Is Nothing Then
            Exit Sub
        End If
        CSTimeTablePara.sCurDiagramState = DiagramState.运行图 '列车运行图

        CSTimeTablePara.picPubDiagram.Width = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
        CSTimeTablePara.picPubDiagram.Height = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        CSTimeTablePara.picPubStation.Width = 80 ' CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
        CSTimeTablePara.picPubStation.Height = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        CSTimeTablePara.picPubStation2.Width = 80 'CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
        CSTimeTablePara.picPubStation2.Height = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        Try
            tmprbmP = New Bitmap(CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight)
            rbmpGraphics = Graphics.FromImage(tmprbmP)
            tmprbmPSta = New Bitmap(CSTimeTablePara.TimeTableDiagramPara.sngPicStationWidth, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight)
            rbmpStaGraphics = Graphics.FromImage(tmprbmPSta)
            tmprbmPSta2 = New Bitmap(CSTimeTablePara.TimeTableDiagramPara.sngPicStationWidth, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight)
            rbmpStaGraphics2 = Graphics.FromImage(tmprbmPSta2)
            rbmpGraphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            rbmpStaGraphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            rbmpStaGraphics2.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        Catch ex As Exception
            MsgBox("对不起，系统内存不足，请重新启动计算机再试一次！")
            Exit Sub
        End Try
        Select Case RPara
            Case 0
                '底图
                Call CSDrawTimeLine(rbmpGraphics, rbmpStaGraphics, rbmpStaGraphics2, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngtopBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank, 0, 0, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.strTimeFormat, "全部", 0, False, False, True)
                rbmpGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias
                Dim state = rbmpGraphics.Save()
                '画运行线
                Call CSDrawDiagramLine(rbmpGraphics, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, 0, CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi, CSTimeTablePara.TimeTableDiagramPara.sCheDiLineStyle, True, CSTimeTablePara.TimeTableDiagramPara.nCheDiLineHeight)
                CSTimeTablePara.pubTimeBmpPic = tmprbmP.Clone
                rbmpGraphics.Restore(state)
                Call CSDrawDiagramDriverLine(rbmpGraphics, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, 0, CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi, CSTimeTablePara.TimeTableDiagramPara.sCheDiLineStyle, True, CSTimeTablePara.TimeTableDiagramPara.nCheDiLineHeight)
                CSTimeTablePara.picPubDiagram.Image = tmprbmP
                CSTimeTablePara.picPubStation.Image = tmprbmPSta
                CSTimeTablePara.picPubStation2.Image = tmprbmPSta2
            Case 1
                tmprbmP = CSTimeTablePara.pubTimeBmpPic.Clone
                rbmpGraphics = Graphics.FromImage(tmprbmP)
                rbmpGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias
                rbmpGraphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
                '画任务段和乘务员交路线
                Call CSDrawDiagramDriverLine(rbmpGraphics, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, 0, CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi, CSTimeTablePara.TimeTableDiagramPara.sCheDiLineStyle, True, CSTimeTablePara.TimeTableDiagramPara.nCheDiLineHeight)
                CSTimeTablePara.picPubDiagram.Image = tmprbmP
        End Select

        tmprbmP = Nothing
        rbmpGraphics.Dispose()
        GC.Collect()
    End Sub
    '画运行线
    Public Sub CSDrawDiagramLine(ByVal rBmpGraphics As Graphics, ByVal intToWidth As Integer, ByVal intFirTime As Integer, _
                                              ByVal nTimeWidth As Integer, ByVal intLeftBlank As Integer, ByVal intStaBlank As Integer, _
                                              ByVal intLeftX As Integer, ByVal nifPrintCheCi As Boolean, ByVal nIfPrintXieCheCi As Boolean, ByVal sShowJLLineStyle As String, _
                                              ByVal bIFShowFixChediLogo As Boolean, ByVal nChediLineHeight As Integer)
        ' nShowJLLineStyle = 0 '三角形   =1直角形
        ' nShowJLLineStyle = 0 '三角形   =1直角形

        Dim i As Integer
        Dim tmpPen As Pen
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing Then
        Else
            For i = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
                If CSTrainsAndDrivers.CSLinkTrains(i).OutputCheCi <> "" Then
                    tmpPen = New Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.UnAssignTrainLineColor), 1.6)
                    Call CSDrawLineInPicture(i, rBmpGraphics, tmpPen, intToWidth, intFirTime, nTimeWidth, intLeftBlank, intStaBlank, intLeftX, nifPrintCheCi, nIfPrintXieCheCi)
                End If
            Next
        End If
    End Sub

    '画运行线
    Public Sub CSDrawDiagramDriverLine(ByVal rBmpGraphics As Graphics, ByVal intToWidth As Integer, ByVal intFirTime As Integer, _
                                              ByVal nTimeWidth As Integer, ByVal intLeftBlank As Integer, ByVal intStaBlank As Integer, _
                                              ByVal intLeftX As Integer, ByVal nifPrintCheCi As Boolean, ByVal nIfPrintXieCheCi As Boolean, ByVal sShowJLLineStyle As String, _
                                              ByVal bIFShowFixChediLogo As Boolean, ByVal nChediLineHeight As Integer)
        Dim i As Integer
        Dim tmpPen As Pen
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
            For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                If CSTrainsAndDrivers.CSDrivers(i) Is CSTrainsAndDrivers.CurEditDriver Then
                    tmpPen = New Pen(Brushes.DeepSkyBlue, 2)
                Else
                    tmpPen = New Pen(CSTrainsAndDrivers.CSDrivers(i).ShowColor, 1.6)
                End If
                For j As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)
                    If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).IsDeadHeading = False Then
                        Call CSDrawLineInPicture(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).CSTrainID, rBmpGraphics, tmpPen, intToWidth, intFirTime, nTimeWidth, intLeftBlank, intStaBlank, intLeftX, nifPrintCheCi, False)
                    End If
                Next
            Next
        End If

        If CSTrainsAndDrivers.CSDrivers Is Nothing Then
        Else
            Call CSDrawCheDiJiaoLuLine(rBmpGraphics, intToWidth, intFirTime, nTimeWidth, intLeftBlank, intStaBlank, intLeftX, nifPrintCheCi, nIfPrintXieCheCi, sShowJLLineStyle, bIFShowFixChediLogo, nChediLineHeight)
        End If
    End Sub

    '画车底交路图
    Public Sub CSDrawCheDiJiaoLuLine(ByVal rBmpGraphics As Graphics, ByVal intToWidth As Integer, ByVal intFirTime As Integer, _
                                              ByVal nTimeWidth As Integer, ByVal intLeftBlank As Integer, ByVal intStaBlank As Integer, _
                                              ByVal intLeftX As Integer, ByVal nifPrintCheCi As Boolean, ByVal nIfPrintXieCheCi As Boolean, ByVal sShowJLLineStyle As String, _
                                              ByVal bIFShowFixChediLogo As Boolean, ByVal nChediLineHeight As Integer)
        Dim i As Integer
        Dim j As Integer
        Dim X1, X2 As Single
        Dim Y1, Y2 As Single
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
        ReDim CSDriverLinkinf(0)
        Dim MidX As Single
        tmpPen = New Pen(Color.Red, 1)
        Dim f As Font
        Dim tmpBrush As Brush
        Dim sShowStyle As String
        sShowStyle = sShowJLLineStyle
        Dim ntmpHeight As Single
        Dim tmpTextWidth As Single
        Dim nNum As Integer
        Dim nLinkHeight As Single
        nLinkHeight = 10
        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            If UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain) > 0 Then
                If sShowStyle = "三角形" Then
                    ntmpHeight = nChediLineHeight
                Else
                    ntmpHeight = 5 + nNum * 1.5
                End If


                tmpPen = New Pen(Color.Black, 1) '(CSDrivers(i).PrintCheDiLinkColor, CSDrivers(i).PrintCheDiLinkWidth)
                tmpPen.DashStyle = CSTrainsAndDrivers.CSDrivers(i).PrintCheDiLinkStyle
                'tmpPen = New Pen(Color.Red, 2)

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
                tmpBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyNoFontColor))
                Dim tmpEndLogo As Pen
                tmpEndLogo = New Pen(Color.Blue, 1)

                If UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain) = 0 Then

                ElseIf UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain) = 1 Then

                    Y1 = CSLinkStationInf(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).StartStaID).YPicValue
                    Y2 = CSLinkStationInf(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).EndStaID).YPicValue
                    X1 = FormTimeToXCord(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).StartTime, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                    X2 = FormTimeToXCord(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).EndTime, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                    '第一列车
                    If X1 >= sngLeftX And X1 <= sngRightX Then

                        If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).UpOrDown = 0 Then '上行
                            ReDim Preserve CSDriverLinkinf(UBound(CSDriverLinkinf) + 1)
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nFirTrain = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).nTrainID
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nSecTrain = 0
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nCheDiId = i
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).LineHeight = 0
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString = CSTrainsAndDrivers.CSDrivers(i).CSdriverNo
                            tmpTextWidth = rBmpGraphics.MeasureString(CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString.Trim, f).Width
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).X1 = X1 - tmpTextWidth
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).Y1 = Y1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).X2 = X1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).Y2 = Y1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).StringX = X1 - tmpTextWidth / 2
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).StringY = Y1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nIFPrint = False
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpPen = tmpPen
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpFont = f
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpBrush = tmpBrush
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).upOrDown = 1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).sStyle = 2

                        Else '下行

                            ReDim Preserve CSDriverLinkinf(UBound(CSDriverLinkinf) + 1)
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nFirTrain = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).nTrainID
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nSecTrain = 0
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nCheDiId = i
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).LineHeight = 0
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString = CSTrainsAndDrivers.CSDrivers(i).CSdriverNo
                            tmpTextWidth = rBmpGraphics.MeasureString(CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString.Trim, f).Width
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).X1 = X1 - tmpTextWidth
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).Y1 = Y1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).X2 = X1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).Y2 = Y1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).StringX = X1 - tmpTextWidth / 2
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).StringY = Y1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nIFPrint = False
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpPen = tmpPen
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpFont = f
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpBrush = tmpBrush
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).upOrDown = -1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).sStyle = 1
                        End If



                        If X2 >= sngLeftX And X2 <= sngRightX Then '到达

                            If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).UpOrDown = 0 Then '上行
                                ReDim Preserve CSDriverLinkinf(UBound(CSDriverLinkinf) + 1)
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).nFirTrain = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).nTrainID
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).nCheDiId = i
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).nSecTrain = 0
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).LineHeight = 0
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString = CSTrainsAndDrivers.CSDrivers(i).CSdriverNo 'CSDrivers(i).sPrintCheCiHao
                                tmpTextWidth = rBmpGraphics.MeasureString(CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString.Trim, f).Width
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).X1 = X2
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).Y1 = Y2
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).X2 = X2 + tmpTextWidth
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).Y2 = Y2
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).StringX = X2 + tmpTextWidth / 2
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).StringY = Y2
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).nIFPrint = False
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpPen = tmpPen
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpFont = f
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpBrush = tmpBrush
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).upOrDown = -1
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).sStyle = 4

                            Else '下行

                                ReDim Preserve CSDriverLinkinf(UBound(CSDriverLinkinf) + 1)
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).nFirTrain = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).nTrainID
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).nSecTrain = 0
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).nCheDiId = i
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).LineHeight = 0
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString = CSTrainsAndDrivers.CSDrivers(i).CSdriverNo 'CSDrivers(i).sPrintCheCiHao
                                tmpTextWidth = rBmpGraphics.MeasureString(CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString.Trim, f).Width
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).X1 = X2
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).Y1 = Y2
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).X2 = X2 + tmpTextWidth
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).Y2 = Y2
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).StringX = X2 + tmpTextWidth / 2
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).StringY = Y2
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).nIFPrint = False
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpPen = tmpPen
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpFont = f
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpBrush = tmpBrush
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).upOrDown = 1
                                CSDriverLinkinf(UBound(CSDriverLinkinf)).sStyle = 3

                            End If
                        End If
                    End If

                    '判断车底是否已经画完
                    If bIFShowFixChediLogo = True Then
                        If CSTrainsAndDrivers.CSDrivers(i).bIfGouWang = True Then
                            rBmpGraphics.DrawRectangle(tmpEndLogo, X1 - 4, Y1 - 4, 8, 8)
                            rBmpGraphics.DrawRectangle(tmpEndLogo, X2 - 4, Y2 - 4, 8, 8)
                        End If
                    End If

                Else '连接不止一列车

                    Y1 = CSLinkStationInf(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).StartStaID).YPicValue
                    X1 = FormTimeToXCord(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).StartTime, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)

                    If X1 >= sngLeftX And X1 <= sngRightX Then

                        If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).UpOrDown = 0 Then '上行
                            ReDim Preserve CSDriverLinkinf(UBound(CSDriverLinkinf) + 1)
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nFirTrain = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).nTrainID
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nSecTrain = 0
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nCheDiId = i
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).LineHeight = 0
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString = CSTrainsAndDrivers.CSDrivers(i).CSdriverNo
                            tmpTextWidth = rBmpGraphics.MeasureString(CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString.Trim, f).Width
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).X1 = X1 - tmpTextWidth
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).Y1 = Y1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).X2 = X1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).Y2 = Y1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).StringX = X1 - tmpTextWidth / 2
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).StringY = Y1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nIFPrint = False
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpPen = tmpPen
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpFont = f
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpBrush = tmpBrush
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).upOrDown = 1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).sStyle = 2
                        Else '下行
                            ReDim Preserve CSDriverLinkinf(UBound(CSDriverLinkinf) + 1)
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nFirTrain = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).nTrainID
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nCheDiId = i
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nSecTrain = 0
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).LineHeight = 0
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString = CSTrainsAndDrivers.CSDrivers(i).CSdriverNo
                            tmpTextWidth = rBmpGraphics.MeasureString(CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString.Trim, f).Width
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).X1 = X1 - tmpTextWidth
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).Y1 = Y1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).X2 = X1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).Y2 = Y1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).StringX = X1 - tmpTextWidth / 2
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).StringY = Y1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nIFPrint = False
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpPen = tmpPen
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpFont = f
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpBrush = tmpBrush
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).upOrDown = -1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).sStyle = 1
                        End If
                    End If

                '判断车底是否已经画完
                If bIFShowFixChediLogo = True Then
                    If CSTrainsAndDrivers.CSDrivers(i).bIfGouWang = True Then
                        rBmpGraphics.DrawRectangle(tmpEndLogo, X1 - 4, Y1 - 4, 8, 8)
                    End If
                End If

                For j = 2 To UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)
                    If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).OutputCheCi = "用餐" Then
                        Continue For
                    ElseIf CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 1).OutputCheCi = "用餐" Then
                        Y1 = CSLinkStationInf(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 2).EndStaID).YPicValue
                        Y2 = CSLinkStationInf(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).StartStaID).YPicValue
                        X1 = FormTimeToXCord(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 2).EndTime, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                        X2 = FormTimeToXCord(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).StartTime, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)

                        ReDim Preserve CSDriverLinkinf(UBound(CSDriverLinkinf) + 1)
                        CSDriverLinkinf(UBound(CSDriverLinkinf)).nFirTrain = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 1).nTrainID
                        CSDriverLinkinf(UBound(CSDriverLinkinf)).nCheDiId = i
                        CSDriverLinkinf(UBound(CSDriverLinkinf)).nSecTrain = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).nTrainID
                        CSDriverLinkinf(UBound(CSDriverLinkinf)).LineHeight = 0
                        CSDriverLinkinf(UBound(CSDriverLinkinf)).X1 = X1
                        CSDriverLinkinf(UBound(CSDriverLinkinf)).Y1 = Y1
                        CSDriverLinkinf(UBound(CSDriverLinkinf)).X2 = X2
                        CSDriverLinkinf(UBound(CSDriverLinkinf)).Y2 = Y2
                        CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 1).nCheDiID & "/" & CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).nCheDiID  ' CSDrivers(i).sPrintCheCiHao
                        CSDriverLinkinf(UBound(CSDriverLinkinf)).StringX = MidX
                        CSDriverLinkinf(UBound(CSDriverLinkinf)).StringY = Y2
                        CSDriverLinkinf(UBound(CSDriverLinkinf)).nIFPrint = False
                        CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpPen = tmpPen
                        CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpFont = f
                        CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpBrush = tmpBrush
                        If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 2).UpOrDown = 1 Then
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).upOrDown = 1
                        Else
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).upOrDown = -1
                        End If

                        CSDriverLinkinf(UBound(CSDriverLinkinf)).sStyle = 6
                        Continue For
                    Else
                        Y1 = CSLinkStationInf(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 1).EndStaID).YPicValue
                        Y2 = CSLinkStationInf(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).StartStaID).YPicValue
                        X1 = FormTimeToXCord(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 1).EndTime, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                        X2 = FormTimeToXCord(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).StartTime, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)

                    End If

                    If Y1 <> Y2 Then
                        If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 1).UpOrDown = 0 Then '上行
                            rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y1 - nLinkHeight)
                            If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True Then
                                rBmpGraphics.DrawString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f, tmpBrush, X1 + 10, Y1 + nLinkHeight)
                            End If
                        End If
                        If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 1).UpOrDown = 1 Then '下行
                            rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y1 + nLinkHeight)
                            If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True Then
                                rBmpGraphics.DrawString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f, tmpBrush, X1 + 10, Y1 + nLinkHeight)
                            End If
                        End If

                        If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).UpOrDown = 0 Then '上行
                            rBmpGraphics.DrawLine(tmpPen, X2, Y2, X2 - 20, Y2 + nLinkHeight)
                            If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True Then
                                rBmpGraphics.DrawString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f, tmpBrush, X1 + 10, Y1 + nLinkHeight)
                            End If
                        End If

                        If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).UpOrDown = 1 Then '下行
                            rBmpGraphics.DrawLine(tmpPen, X2, Y2, X2 - 20, Y2 - nLinkHeight)
                            If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True Then
                                rBmpGraphics.DrawString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f, tmpBrush, X1 + 10, Y1 + nLinkHeight)
                            End If
                        End If
                        Continue For
                    End If

                    '========================
                    If X1 >= 0 And Y1 >= 0 And X2 >= 0 And Y2 >= 0 Then
                        MidX = X1 + (X2 - X1) / 2
                        If (CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 1).UpOrDown + CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).UpOrDown) Mod 2 = 0 Then '同向
                            If X1 <= X2 Then
                                If X1 <= sngRightX Then
                                    If X2 <= sngRightX Then '
                                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                                        If nifPrintCheCi = True Then
                                            'tmpTextWidth = rBmpGraphics.MeasureString(CSDrivers(i).CSdriverNo, f).Width
                                            'ntmpHeight = rBmpGraphics.MeasureString(CSDrivers(i).CSdriverNo, f).Height
                                            rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y1)
                                            'If CSDrivers(i).CSLinkTrain(j - 1).UpOrDown = 0 Then
                                            '    rBmpGraphics.DrawString(CSDrivers(i).CSdriverNo, f, tmpBrush, MidX - tmpTextWidth / 2, Y1)
                                            'Else
                                            '    rBmpGraphics.DrawString(CSDrivers(i).CSdriverNo, f, tmpBrush, MidX - tmpTextWidth / 2, Y1 - ntmpHeight)
                                            'End If
                                        End If

                                        Else '右边过界
                                            rBmpGraphics.DrawLine(tmpPen, X1, Y1, sngRightX, Y2)
                                        End If
                                End If
                            Else '左边接边界‘修改*****

                                    If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 1).UpOrDown = 1 Then
                                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y1 + nLinkHeight)
                                        'If nifPrintCheCi = True Then
                                        '    rBmpGraphics.DrawString(CSDrivers(i).CSdriverNo, f, tmpBrush, X1 + 20, Y1 + nLinkHeight)
                                        'End If
                                        rBmpGraphics.DrawLine(tmpPen, X2 - 20, Y2 - nLinkHeight, X2, Y2)
                                        'If nifPrintCheCi = True Then
                                        '    rBmpGraphics.DrawString(CSDrivers(i).CSdriverNo, f, tmpBrush, X2 - 20, Y2 - nLinkHeight)
                                        'End If

                                    Else
                                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y1 - nLinkHeight)
                                        'If nifPrintCheCi = True Then
                                        '    rBmpGraphics.DrawString(CSDrivers(i).CSdriverNo, f, tmpBrush, X1 + 20, Y1 - nLinkHeight)
                                        'End If
                                        rBmpGraphics.DrawLine(tmpPen, X2 - 20, Y2 + nLinkHeight, X2, Y2)
                                        'If nifPrintCheCi = True Then
                                        '    rBmpGraphics.DrawString(CSDrivers(i).CSdriverNo, f, tmpBrush, X2 - 20, Y2 + nLinkHeight)
                                        'End If
                                    End If
                                End If
                            '  End If
                        Else '不同向
                            If X1 <= X2 Then
                                If X1 <= sngRightX Then
                                    If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 1).UpOrDown = 0 Then
                                        If X2 <= sngRightX Then

                                            If X2 - X1 >= 600 Then '对于交路线过长的情况
                                                    rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y1 - nLinkHeight)
                                                    If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True Then
                                                        rBmpGraphics.DrawString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f, tmpBrush, X1 + 10, Y1 - nLinkHeight - 12)
                                                    End If
                                                    rBmpGraphics.DrawLine(tmpPen, X2, Y2, X2 - 20, Y2 - nLinkHeight)
                                                    If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True Then
                                                        rBmpGraphics.DrawString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f, tmpBrush, X2 - 30, Y2 - nLinkHeight - 12)
                                                    End If

                                                Else
                                                    If sShowStyle = "三角形" Then
                                                        tmpTextWidth = rBmpGraphics.MeasureString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f).Width
                                                        ntmpHeight = rBmpGraphics.MeasureString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f).Height
                                                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, MidX, Y1 - ntmpHeight)
                                                        rBmpGraphics.DrawLine(tmpPen, MidX, Y1 - ntmpHeight, X2, Y2)
                                                        If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True Then
                                                            rBmpGraphics.DrawString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f, Brushes.Blue, MidX - tmpTextWidth / 2, Y1 - ntmpHeight - 12)
                                                        End If

                                                    Else    '画长方形
                                                        '================================================================================================================{
                                                        ReDim Preserve CSDriverLinkinf(UBound(CSDriverLinkinf) + 1)
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).nFirTrain = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 1).nTrainID
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).nCheDiId = i
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).nSecTrain = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).nTrainID
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).LineHeight = 0
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).X1 = X1
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).Y1 = Y1
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).X2 = X2
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).Y2 = Y2
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 1).nCheDiID & "/" & CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).nCheDiID  ' CSDrivers(i).sPrintCheCiHao
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).StringX = MidX
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).StringY = Y2
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).nIFPrint = False
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpPen = tmpPen
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpFont = f
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpBrush = tmpBrush
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).upOrDown = -1
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).sStyle = 0
                                                    End If
                                                End If
                                            Else '右边过界
                                                rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y1 - nLinkHeight)
                                                If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True Then
                                                    rBmpGraphics.DrawString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f, tmpBrush, X1 + 10, Y1 - nLinkHeight - 12)
                                                End If

                                            End If
                                        Else
                                            If X2 <= sngRightX Then
                                                If X2 - X1 >= 600 Then '对于交路线过长的情况

                                                    rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y1 + 10)
                                                    If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True Then
                                                        rBmpGraphics.DrawString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f, tmpBrush, X1 + 10, Y1 + nLinkHeight)
                                                    End If
                                                    rBmpGraphics.DrawLine(tmpPen, X2, Y2, X2 - 20, Y2 + nLinkHeight)
                                                    If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True Then
                                                        rBmpGraphics.DrawString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f, tmpBrush, X2 - 30, Y2 + nLinkHeight)
                                                    End If

                                                Else
                                                    If sShowStyle = "三角形" Then
                                                        tmpTextWidth = rBmpGraphics.MeasureString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f).Width
                                                        ntmpHeight = rBmpGraphics.MeasureString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f).Height

                                                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, MidX, Y1 + ntmpHeight)
                                                        rBmpGraphics.DrawLine(tmpPen, MidX, Y1 + ntmpHeight, X2, Y2)
                                                        If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True Then
                                                            rBmpGraphics.DrawString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f, Brushes.Blue, MidX - tmpTextWidth / 2, Y1 + ntmpHeight)
                                                        End If

                                                    Else    '长方形
                                                        ReDim Preserve CSDriverLinkinf(UBound(CSDriverLinkinf) + 1)
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).nFirTrain = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 1).nTrainID
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).nCheDiId = i
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).nSecTrain = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).nTrainID
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).LineHeight = 0
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).X1 = X1
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).Y1 = Y1
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).X2 = X2
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).Y2 = Y2
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 1).nCheDiID & "/" & CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).nCheDiID 'CSDrivers(i).sPrintCheCiHao
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).StringX = MidX
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).StringY = Y2
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).nIFPrint = False
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpPen = tmpPen
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpFont = f
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpBrush = tmpBrush
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).upOrDown = 1
                                                        CSDriverLinkinf(UBound(CSDriverLinkinf)).sStyle = 0

                                                    End If
                                                    'If nifPrintCheCi = True Then
                                                    '    rBmpGraphics.DrawString(CSDrivers(i).sPrintCheCiHao, f, tmpBrush, MidX - 12, Y2 + ntmpHeight - 2)
                                                    'End If
                                                End If
                                            Else

                                                rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y1 + nLinkHeight)
                                                If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True Then
                                                    rBmpGraphics.DrawString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f, tmpBrush, X1 + 10, Y1 + nLinkHeight - 2)
                                                End If

                                            End If
                                        End If
                                    End If
                                Else '左边过界

                                    '修改
                                    If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j - 1).UpOrDown = 0 Then
                                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y1 - nLinkHeight)
                                        If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True Then
                                            rBmpGraphics.DrawString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f, tmpBrush, X1, Y1)
                                        End If
                                        rBmpGraphics.DrawLine(tmpPen, X2 - 20, Y2 - nLinkHeight, X2, Y2)
                                        If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True Then
                                            rBmpGraphics.DrawString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f, tmpBrush, X2 - 20, Y2 - nLinkHeight)
                                        End If
                                    Else
                                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1 + 20, Y1 + nLinkHeight)
                                        If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True Then
                                            rBmpGraphics.DrawString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f, tmpBrush, X1 + 20, Y1 + nLinkHeight)
                                        End If
                                        rBmpGraphics.DrawLine(tmpPen, X2 - 20, Y2 + nLinkHeight, X2, Y2)
                                        If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True Then
                                            rBmpGraphics.DrawString(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, f, tmpBrush, X2 - 20, Y2 + nLinkHeight)
                                        End If
                                    End If
                                End If
                            End If
                    End If
                Next


                Y2 = CSLinkStationInf(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)).EndStaID).YPicValue
                X2 = FormTimeToXCord(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)).EndTime, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                If bIFShowFixChediLogo = True Then
                    If CSTrainsAndDrivers.CSDrivers(i).bIfGouWang = True Then
                        rBmpGraphics.DrawRectangle(tmpEndLogo, X2 - 4, Y2 - 4, 8, 8)
                    End If
                End If

                If X2 >= sngLeftX And X2 <= sngRightX Then

                        If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)).UpOrDown = 0 Then '上行  '终到
                            ReDim Preserve CSDriverLinkinf(UBound(CSDriverLinkinf) + 1)
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nFirTrain = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)).nTrainID
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nSecTrain = 0
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nCheDiId = i
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).LineHeight = 0
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString = CSTrainsAndDrivers.CSDrivers(i).CSdriverNo
                            tmpTextWidth = rBmpGraphics.MeasureString(CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString.Trim, f).Width
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).X1 = X2
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).Y1 = Y2
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).X2 = X2 + tmpTextWidth
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).Y2 = Y2
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).StringX = X2 + tmpTextWidth / 2
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).StringY = Y2
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nIFPrint = False
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpPen = tmpPen
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpFont = f
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpBrush = tmpBrush
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).upOrDown = -1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).sStyle = 4
                        Else '下行
                            ReDim Preserve CSDriverLinkinf(UBound(CSDriverLinkinf) + 1)
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nFirTrain = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)).nTrainID
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nCheDiId = i
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nSecTrain = 0
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).LineHeight = 0
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString = CSTrainsAndDrivers.CSDrivers(i).CSdriverNo
                            tmpTextWidth = rBmpGraphics.MeasureString(CSDriverLinkinf(UBound(CSDriverLinkinf)).DrawString.Trim, f).Width
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).X1 = X2
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).Y1 = Y2
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).X2 = X2 + tmpTextWidth
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).Y2 = Y2
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).StringX = X2 + tmpTextWidth / 2
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).StringY = Y2
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).nIFPrint = False
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpPen = tmpPen
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpFont = f
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).tmpBrush = tmpBrush
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).upOrDown = 1
                            CSDriverLinkinf(UBound(CSDriverLinkinf)).sStyle = 3
                        End If
                    End If
                End If
                nNum = nNum + 1
            End If
        Next

        Dim nPaiXu() As Integer
        ReDim nPaiXu(UBound(CSDriverLinkinf))
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
                TempTime1 = CSDriverLinkinf(nPaiXu(j)).X1
                Temptime2 = CSDriverLinkinf(nPaiXu(j + 1)).X1
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
            'If CSDriverLinkinf(nPaiXu(i)).nFirTrain = 23 Then Stop
            nHeight = GetCurDriverLineHeight(nPaiXu(i))
            sngHeight = (tmpHeight + nHeight * tmpStep) * CSDriverLinkinf(nPaiXu(i)).upOrDown
            CSDriverLinkinf(nPaiXu(i)).LineHeight = nHeight
            X1 = CSDriverLinkinf(nPaiXu(i)).X1
            Y1 = CSDriverLinkinf(nPaiXu(i)).Y1
            X2 = CSDriverLinkinf(nPaiXu(i)).X2
            Y2 = CSDriverLinkinf(nPaiXu(i)).Y2
            Select Case CSDriverLinkinf(nPaiXu(i)).sStyle
                Case 0 '矩形
                    rBmpGraphics.DrawLine(CSDriverLinkinf(nPaiXu(i)).tmpPen, X1, Y1, X1, Y1 + sngHeight)
                    rBmpGraphics.DrawLine(CSDriverLinkinf(nPaiXu(i)).tmpPen, X2, Y2, X2, Y2 + sngHeight)
                    rBmpGraphics.DrawLine(CSDriverLinkinf(nPaiXu(i)).tmpPen, X1, Y1 + sngHeight, X2, Y2 + sngHeight)
                Case 1 '左上
                    rBmpGraphics.DrawLine(CSDriverLinkinf(nPaiXu(i)).tmpPen, X2, Y2, X2, Y2 + sngHeight)
                    rBmpGraphics.DrawLine(CSDriverLinkinf(nPaiXu(i)).tmpPen, X1, Y1 + sngHeight, X1 - 5, Y1 + sngHeight - 5)
                    rBmpGraphics.DrawLine(CSDriverLinkinf(nPaiXu(i)).tmpPen, X2, Y2 + sngHeight, X1, Y1 + sngHeight)

                Case 2 '右下
                    rBmpGraphics.DrawLine(CSDriverLinkinf(nPaiXu(i)).tmpPen, X2, Y2, X2, Y2 + sngHeight)
                    rBmpGraphics.DrawLine(CSDriverLinkinf(nPaiXu(i)).tmpPen, X1, Y1 + sngHeight, X1 - 5, Y1 + sngHeight + 5)
                    rBmpGraphics.DrawLine(CSDriverLinkinf(nPaiXu(i)).tmpPen, X2, Y2 + sngHeight, X1, Y1 + sngHeight)

                Case 3 '左下
                    rBmpGraphics.DrawLine(CSDriverLinkinf(nPaiXu(i)).tmpPen, X1, Y1, X1, Y1 + sngHeight)
                    rBmpGraphics.DrawLine(CSDriverLinkinf(nPaiXu(i)).tmpPen, X2, Y2 + sngHeight, X2 + 5, Y2 + sngHeight + 5)
                    rBmpGraphics.DrawLine(CSDriverLinkinf(nPaiXu(i)).tmpPen, X1, Y1 + sngHeight, X2, Y2 + sngHeight)

                Case 4 '右上
                    rBmpGraphics.DrawLine(CSDriverLinkinf(nPaiXu(i)).tmpPen, X1, Y1, X1, Y1 + sngHeight)
                    rBmpGraphics.DrawLine(CSDriverLinkinf(nPaiXu(i)).tmpPen, X2, Y2 + sngHeight, X2 + 5, Y2 + sngHeight - 5)
                    rBmpGraphics.DrawLine(CSDriverLinkinf(nPaiXu(i)).tmpPen, X1, Y1 + sngHeight, X2, Y2 + sngHeight)
                Case 6 '用餐
                    Dim points(2) As Point
                    points(0) = New Point(X1, Y1)
                    points(1) = New Point(0.5 * (X1 + X2), 0.5 * (Y1 + Y2) + sngHeight)
                    points(2) = New Point(X2, Y2)
                    rBmpGraphics.DrawCurve(CSDriverLinkinf(nPaiXu(i)).tmpPen, points)
                    PrintText = CSTrainsAndDrivers.CSDrivers(CSDriverLinkinf(nPaiXu(i)).nCheDiId).CSdriverNo
                    txtWidth = rBmpGraphics.MeasureString(PrintText, CSDriverLinkinf(nPaiXu(i)).tmpFont).Width / 2
                    If CSDriverLinkinf(nPaiXu(i)).upOrDown = -1 Then
                        txtHeight = rBmpGraphics.MeasureString(PrintText, CSDriverLinkinf(nPaiXu(i)).tmpFont).Height * CSDriverLinkinf(nPaiXu(i)).upOrDown + 2
                    Else
                        txtHeight = 0
                    End If
                    rBmpGraphics.DrawString(PrintText, CSDriverLinkinf(nPaiXu(i)).tmpFont, CSDriverLinkinf(nPaiXu(i)).tmpBrush, 0.5 * (X1 + X2) - txtWidth, 0.5 * (Y1 + Y2) + sngHeight + txtHeight)
                Case 5 '同向，正弦曲线连接
                    Dim X3, Y3 As Integer
                    X3 = 0.5 * (X1 + X2)
                    Y3 = 0.5 * (Y1 + Y2)
                    Dim points(2) As Point
                    points(0) = New Point(X1, Y1)
                    points(1) = New Point(0.5 * (X1 + X3), 0.5 * (Y1 + Y3) + sngHeight)
                    points(2) = New Point(X3, Y3)
                    rBmpGraphics.DrawCurve(Pens.DarkSlateBlue, points)

                    points(0) = New Point(X3, Y3)
                    points(1) = New Point(0.5 * (X2 + X3), 0.5 * (Y2 + Y3) - sngHeight)
                    points(2) = New Point(X2, Y2)
                    rBmpGraphics.DrawCurve(Pens.DarkSlateBlue, points)

            End Select

            If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True And CSDriverLinkinf(nPaiXu(i)).sStyle <> 6 And CSDriverLinkinf(nPaiXu(i)).sStyle <> 5 Then

                PrintText = CSTrainsAndDrivers.CSDrivers(CSDriverLinkinf(nPaiXu(i)).nCheDiId).CSdriverNo
                txtWidth = rBmpGraphics.MeasureString(PrintText, CSDriverLinkinf(nPaiXu(i)).tmpFont).Width / 2
                If CSDriverLinkinf(nPaiXu(i)).upOrDown < 0 Then
                    txtHeight = rBmpGraphics.MeasureString(PrintText, CSDriverLinkinf(nPaiXu(i)).tmpFont).Height * CSDriverLinkinf(nPaiXu(i)).upOrDown + 2
                Else
                    txtHeight = 0
                End If
                rBmpGraphics.DrawString(PrintText, CSDriverLinkinf(nPaiXu(i)).tmpFont, CSDriverLinkinf(nPaiXu(i)).tmpBrush, CSDriverLinkinf(nPaiXu(i)).StringX - txtWidth, CSDriverLinkinf(nPaiXu(i)).StringY + sngHeight + txtHeight)
            End If

        Next
    End Sub
    '在底图上画线，考虑过渡时的情况
    Public Sub CSDrawLineInPicture(ByVal nTrain As Integer, _
                                                ByVal rBmpGraphics As Graphics, _
                                                ByVal tmpPen As Pen, _
                                                ByVal intToWidth As Integer, _
                                                ByVal intFirTime As Integer, _
                                                ByVal nTimeWidth As Integer, _
                                                ByVal intLeftBlank As Integer, _
                                                ByVal intStaBlank As Integer, _
                                                ByVal intLeftX As Integer, _
                                                ByVal nifPrintCheCi As Boolean, ByVal nIfPrintXieCheCi As Boolean) ', ByVal sngLeftX As Single, ByVal sngRightX As Single)


        Dim X1, X2, X3, X4 As Single
        Dim Y1, Y2, Y3, Y4 As Single
        Dim intTime1 As Integer
        Dim intTime2 As Integer
        Dim sngToWidth As Single


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
        tmpBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.XieCheCiFontColor))
        '========================================================
        If nTrain = 0 Then
            Return
        End If
        intTime1 = CSTrainsAndDrivers.CSLinkTrains(nTrain).StartTime
        X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        Y1 = CSLinkStationInf(CSTrainsAndDrivers.CSLinkTrains(nTrain).StartStaID).YPicValue
        intTime2 = CSTrainsAndDrivers.CSLinkTrains(nTrain).EndTime
        X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        Y2 = CSLinkStationInf(CSTrainsAndDrivers.CSLinkTrains(nTrain).EndStaID).YPicValue

        If X1 >= 0 And Y1 >= 0 And X2 >= 0 And Y2 >= 0 Then
            If X2 >= X1 Then
                If X1 <= sngRightX Then
                    If X2 <= sngRightX Then
                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                    Else
                        X3 = sngRightX
                        Y3 = ((X3 - X1) * (Y2 - Y1) / (X2 - X1)) + Y1
                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                    End If
                End If
            Else
                X4 = X1 - sngToWidth
                Y4 = Y1
                X3 = intLeftBlank + intStaBlank + intLeftX
                Y3 = ((X3 - X4) * (Y2 - Y4) / (X2 - X4)) + Y4
                rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3 + sngToWidth, Y3)
                rBmpGraphics.DrawLine(tmpPen, X3, Y3, X2, Y2)
            End If
        End If

        If X2 >= X1 Then
            If CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi Then
                Call WriteStrInLine(rBmpGraphics, CSTrainsAndDrivers.CSLinkTrains(nTrain).OutputCheCi, f, tmpBrush, X1, Y1, X2, Y2, 9)
            End If
        Else
            If CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi Then
                Call WriteStrInLine(rBmpGraphics, CSTrainsAndDrivers.CSLinkTrains(nTrain).OutputCheCi, f, tmpBrush, X1, Y1, X3 + sngToWidth, Y3, 9)
            End If
        End If

    End Sub

    '在底图上画线，考虑过渡时的情况
    Public Sub CSDrawLineInPicture(ByVal Train As CSLinkTrain, _
                                                ByVal rBmpGraphics As Graphics, _
                                                ByVal tmpPen As Pen, _
                                                ByVal intToWidth As Integer, _
                                                ByVal intFirTime As Integer, _
                                                ByVal nTimeWidth As Integer, _
                                                ByVal intLeftBlank As Integer, _
                                                ByVal intStaBlank As Integer, _
                                                ByVal intLeftX As Integer, _
                                                ByVal nifPrintCheCi As Boolean, ByVal nIfPrintXieCheCi As Boolean) ', ByVal sngLeftX As Single, ByVal sngRightX As Single)


        Dim X1, X2, X3, X4 As Single
        Dim Y1, Y2, Y3, Y4 As Single
        Dim intTime1 As Integer
        Dim intTime2 As Integer
        Dim sngToWidth As Single

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
        tmpBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.XieCheCiFontColor))
        '========================================================

        intTime1 = Train.StartTime
        X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        Y1 = CSLinkStationInf(Train.StartStaID).YPicValue
        intTime2 = Train.EndTime
        X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        Y2 = CSLinkStationInf(Train.EndStaID).YPicValue

        If X1 >= 0 And Y1 >= 0 And X2 >= 0 And Y2 >= 0 Then
            If X2 >= X1 Then
                If X1 <= sngRightX Then
                    If X2 <= sngRightX Then
                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                    Else
                        X3 = sngRightX
                        Y3 = ((X3 - X1) * (Y2 - Y1) / (X2 - X1)) + Y1
                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                    End If
                End If
            Else
                X4 = X1 - sngToWidth
                Y4 = Y1
                X3 = intLeftBlank + intStaBlank + intLeftX
                Y3 = ((X3 - X4) * (Y2 - Y4) / (X2 - X4)) + Y4
                rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3 + sngToWidth, Y3)
                rBmpGraphics.DrawLine(tmpPen, X3, Y3, X2, Y2)
            End If
        End If

        If X2 >= X1 Then
            Call WriteStrInLine(rBmpGraphics, Train.OutputCheCi, f, tmpBrush, X1, Y1, X2, Y2, 9)
        Else
            Call WriteStrInLine(rBmpGraphics, Train.OutputCheCi, f, tmpBrush, X1, Y1, X3 + sngToWidth, Y3, 9)
        End If

    End Sub

    '打印车站和时间线
    Public Sub CSDrawTimeLine(ByVal rBmpGraphics As Graphics, ByVal rbmpStaGraphics As Graphics, ByVal rbmpStaGraphics2 As Graphics, ByVal PicWidth As Single, ByVal PicHeight As Single, _
                                        ByVal LeftBlank As Single, ByVal topBlank As Single, ByVal StaBlank As Single, _
                                        ByVal sngTimeBlank As Single, ByVal sngLeftX As Single, ByVal sngTopY As Single, _
                                        ByVal nFirTime As Integer, ByVal nTimeWidth As Integer, ByVal DiagramTimeFormate As String, ByVal DiagramTimeLineFont As String, ByVal nPrintSta As Integer, ByVal bIfPrintEverySta As Boolean, ByVal bifPrintLanTu As Boolean, ByVal IfPrintHead As Boolean)


        Dim i, j, k As Integer
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
        If UBound(CSLinkStationInf) > 0 Then
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

            tmpY1 = CSLinkStationInf(1).Ycord
            maxY = 0
            For i = 1 To UBound(CSLinkStationInf)
                If CSLinkStationInf(i).Ycord > maxY Then
                    maxY = CSLinkStationInf(i).Ycord
                End If
            Next
            If maxY - tmpY1 > 0 Then
                YBix = sngHeight / (maxY - tmpY1)

                '第一个车站的横线
                StaLinePen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.StaLineColor), CSTimeTablePara.DiagramStylePara.StaLineWidth)
                StaLinePen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.StaLineStyle)
                rBmpGraphics.DrawLine(StaLinePen, LeftBlank + StaBlank * IfPrintStation + sngLeftX, topBlank + sngTopY, LeftBlank + sngToWidth + StaBlank * IfPrintStation + sngLeftX, topBlank + sngTopY)
                f = New Font(CSTimeTablePara.DiagramStylePara.StaNameFontName, CSTimeTablePara.DiagramStylePara.StaNameFontSize)
                fSta = New Font(CSTimeTablePara.DiagramStylePara.StaNameFontName, CSTimeTablePara.DiagramStylePara.StaNameFontSize)
                If CSTimeTablePara.DiagramStylePara.StaNameFontBold = True Then
                    If CSTimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                        f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        fSta = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                    Else
                        f = New Font(f, FontStyle.Bold)
                        fSta = New Font(f, FontStyle.Bold)
                    End If
                Else
                    If CSTimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                        f = New Font(f, FontStyle.Italic)
                        fSta = New Font(f, FontStyle.Italic)
                    End If
                End If
                nB = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.StaNameFontColor))
                txtWidth = rBmpGraphics.MeasureString(CSLinkStationInf(1).sStationName, f).Width
                If nPrintSta = 1 Then
                    rBmpGraphics.DrawString(CSLinkStationInf(1).sStationName, f, nB, LeftBlank + sngLeftX + StaBlank - txtWidth, topBlank - 6 + sngTopY)
                End If
                If rbmpStaGraphics Is Nothing Then
                Else
                    rbmpStaGraphics.DrawString(CSLinkStationInf(1).sStationName, f, nB, 2, topBlank - 6 + sngTopY)
                End If
                If rbmpStaGraphics2 Is Nothing Then
                Else
                    rbmpStaGraphics2.DrawString(CSLinkStationInf(1).sStationName, f, nB, CSTimeTablePara.picPubStation2.Width - txtWidth + 2, topBlank - 6 + sngTopY)
                End If
                CSLinkStationInf(1).YPicValue = topBlank + sngTopY

                For i = 1 To UBound(CSLinkStationInf)
                    Dim IsDraw As Boolean = False
                    '    If i = 1 Or i = UBound(CSLinkStationInf) Then
                    '        IsDraw = True
                    'Else
                    '    For k = 1 To UBound(UsefulSta)
                    '        If CSLinkStationInf(i).sStationName = UsefulSta(k) Then
                    IsDraw = True
                    'Exit For
                    'End If
                    '    Next
                    'End If


                    tmpY2 = CSLinkStationInf(i).Ycord
                    tmpY = (tmpY2 - tmpY1) * YBix + topBlank + sngTopY

                    StaLinePen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.StaLineColor), CSTimeTablePara.DiagramStylePara.StaLineWidth)
                    StaLinePen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.StaLineStyle)
                    If CSLinkStationInf(i).sStaStyle = "车场" Or CSLinkStationInf(i).sStaStyle = "出入车场信号机" Or CSLinkStationInf(i).sStaStyle = "非作业站" Then
                        StaLinePen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.CheChangStaLineColor), CSTimeTablePara.DiagramStylePara.CheChangStaLineWidth)
                        StaLinePen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.CheChangStaLineStyle)
                    End If

                    If CSLinkStationInf(i).sStaStyle = "大站" Then
                        StaLinePen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.FenStaLineColor), CSTimeTablePara.DiagramStylePara.FenStaLineWidth)
                        StaLinePen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.FenStaLineStyle)
                    End If

                    If IsDraw = True Then
                        rBmpGraphics.DrawLine(StaLinePen, LeftBlank + StaBlank * IfPrintStation + sngLeftX, tmpY, LeftBlank + StaBlank * IfPrintStation + sngLeftX + sngToWidth, tmpY)
                        txtWidth = rBmpGraphics.MeasureString(CSLinkStationInf(i).sStationName, f).Width
                        brsStation = Brushes.Green
                        If nPrintSta = 1 Then
                            rBmpGraphics.DrawString(CSLinkStationInf(i).sStationName, f, nB, LeftBlank + sngLeftX + StaBlank - txtWidth, tmpY - 6)
                        End If
                        If rbmpStaGraphics Is Nothing Then
                        Else
                            rbmpStaGraphics.DrawString(CSLinkStationInf(i).sStationName, f, nB, 2, tmpY - 6)
                        End If

                        If rbmpStaGraphics2 Is Nothing Then
                        Else
                            rbmpStaGraphics2.DrawString(CSLinkStationInf(i).sStationName, f, nB, CSTimeTablePara.picPubStation2.Width - txtWidth + 2, tmpY - 6)
                        End If
                    End If
                    'txtWidth = rBmpGraphics.MeasureString(CSLinkStationInf(i).sStationName, f).Width
                    'brsStation = Brushes.Green
                    'If nPrintSta = 1 Then
                    '    rBmpGraphics.DrawString(CSLinkStationInf(i).sStationName, f, nB, LeftBlank + sngLeftX + StaBlank - txtWidth, tmpY - 6)
                    'End If
                    'If rbmpStaGraphics Is Nothing Then
                    'Else
                    '    rbmpStaGraphics.DrawString(CSLinkStationInf(i).sStationName, f, nB, 2, tmpY - 6)
                    'End If

                    'If rbmpStaGraphics2 Is Nothing Then
                    'Else
                    '    rbmpStaGraphics2.DrawString(CSLinkStationInf(i).sStationName, f, nB, CSTimeTablePara.picPubStation2.Width - txtWidth + 2, tmpY - 6)
                    'End If
                    CSLinkStationInf(i).YPicValue = tmpY
                Next i

                Dim strTimePrint As String
                Dim strPrintString As String
                'Dim tmpPen As Pen
                ' Dim k As Integer
                Dim sngY1, sngY2 As Single
                Dim f1 As Font
                f = New Font(CSTimeTablePara.DiagramStylePara.TimeFontName, CSTimeTablePara.DiagramStylePara.TimeFontSize)
                f1 = New Font(CSTimeTablePara.DiagramStylePara.TimeFontName, CSTimeTablePara.DiagramStylePara.TimeFontSize * 1.5)
                If CSTimeTablePara.DiagramStylePara.TimeFontBold = True Then
                    If CSTimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                        f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        f1 = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                    Else
                        f = New Font(f, FontStyle.Bold)
                        f1 = New Font(f, FontStyle.Bold)
                    End If
                Else
                    If CSTimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                        f = New Font(f, FontStyle.Italic)
                        f1 = New Font(f, FontStyle.Italic)
                    End If
                End If
                nB = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TimeFontColor))

                For k = 1 To UBound(CSLinkSectionInf)
                    sngY1 = CSLinkStationInf(CSLinkSectionInf(k).nFirStaID).YPicValue
                    sngY2 = CSLinkStationInf(CSLinkSectionInf(k).nSecStaID).YPicValue
                    Select Case DiagramTimeFormate
                        Case "小时格"
                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.HourTime60LineColor), CSTimeTablePara.DiagramStylePara.HourTime60LineWidth)
                            curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.HourTime60LineStyle)
                            intTimeLine = nTimeWidth  '10 * 6 * 24
                            sngMinuWidth = sngToWidth / (intTimeLine)
                            For i = 1 To intTimeLine + 1
                                rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                strTimePrint = Trim(CStr((intFirstTime + i - 1) Mod 24) & ":00")
                                '打印时间文字,第一行
                                txtWidth = rBmpGraphics.MeasureString(strTimePrint, f1).Width / 2
                                rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                '最后一行
                                rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                If bIfPrintEverySta = True Then '每小时打印车站
                                    If i > 1 And i < intTimeLine + 1 Then
                                        For j = 1 To UBound(CSLinkStationInf)
                                            txtWidth = rBmpGraphics.MeasureString(CSLinkStationInf(j).sStationName, fSta).Width / 2
                                            txtHeight = rBmpGraphics.MeasureString(CSLinkStationInf(j).sStationName, fSta).Height / 2
                                            rBmpGraphics.DrawString(CSLinkStationInf(j).sStationName, fSta, Brushes.DarkGray, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, CSLinkStationInf(j).YPicValue - txtHeight)
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
                                    curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TenTime60LineColor), CSTimeTablePara.DiagramStylePara.TenTime60LineWidth)
                                    curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.TenTime60LineStyle)
                                    rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                    strTimePrint = Trim(CStr((intFirstTime + (Int((i - 1) / 6))) Mod 24) & ":00")
                                    '打印时间文字,第一行
                                    txtWidth = rBmpGraphics.MeasureString(strTimePrint, f1).Width / 2
                                    txtHeight = rBmpGraphics.MeasureString(strTimePrint, f1).Height

                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - txtHeight + 8)
                                    '最后一行
                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 2)
                                    If bIfPrintEverySta = True Then '每小时打印车站
                                        If i > 1 And i < intTimeLine + 1 Then
                                            For j = 1 To UBound(CSLinkStationInf)
                                                txtWidth = rBmpGraphics.MeasureString(CSLinkStationInf(j).sStationName, fSta).Width / 2
                                                txtHeight = rBmpGraphics.MeasureString(CSLinkStationInf(j).sStationName, fSta).Height / 2
                                                rBmpGraphics.DrawString(CSLinkStationInf(j).sStationName, fSta, Brushes.DarkGray, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, CSLinkStationInf(j).YPicValue - txtHeight)
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
                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TenTime30LineColor), CSTimeTablePara.DiagramStylePara.TenTime30LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.TenTime30LineStyle)
                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)

                                        'If strTimePrint = "8:50" Then Stop
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)

                                        If DiagramTimeLineFont = "全部" Or DiagramTimeLineFont = "标注半小时格" Then
                                            strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2

                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If

                                    Else
                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TenTime10LineColor), CSTimeTablePara.DiagramStylePara.TenTime10LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.TenTime10LineStyle)
                                        rBmpGraphics.DrawLine(curPen, LeftBlank + StaBlank * IfPrintStation + sngLeftX + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        '打印时间文字,第一行
                                        'If strTimePrint = "8:50" Then Stop
                                        If DiagramTimeLineFont = "全部" Then
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If
                                    End If
                                End If
                            Next
                        Case "二分格"
                            intTimeLine = 6 * nTimeWidth
                            sngMinuWidth = sngToWidth / (intTimeLine)
                            For i = 1 To intTimeLine + 1
                                If (i - 1) Mod 6 = 0 Then
                                    curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TwoTime60LineColor), CSTimeTablePara.DiagramStylePara.TwoTime60LineWidth)
                                    curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.TwoTime60LineStyle)
                                    rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                    strTimePrint = Trim(CStr((intFirstTime + (Int((i - 1) / 6))) Mod 24) & ":00")
                                    '打印时间文字,第一行
                                    txtWidth = rBmpGraphics.MeasureString(strTimePrint, f1).Width / 2
                                    txtHeight = rBmpGraphics.MeasureString(strTimePrint, f1).Height

                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - txtHeight + 8)
                                    '最后一行
                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 2)
                                    If bIfPrintEverySta = True Then '每小时打印车站
                                        If i > 1 And i < intTimeLine + 1 Then
                                            For j = 1 To UBound(CSLinkStationInf)
                                                txtWidth = rBmpGraphics.MeasureString(CSLinkStationInf(j).sStationName, fSta).Width / 2
                                                txtHeight = rBmpGraphics.MeasureString(CSLinkStationInf(j).sStationName, fSta).Height / 2
                                                rBmpGraphics.DrawString(CSLinkStationInf(j).sStationName, fSta, Brushes.DarkGray, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, CSLinkStationInf(j).YPicValue - txtHeight)
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

                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TwoTime30LineColor), CSTimeTablePara.DiagramStylePara.TwoTime30LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.TwoTime30LineStyle)
                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)

                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        If DiagramTimeLineFont = "全部" Or DiagramTimeLineFont = "标注半小时格" Then
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)

                                        End If

                                    Else
                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TwoTime10LineColor), CSTimeTablePara.DiagramStylePara.TwoTime10LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.TwoTime10LineStyle)
                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        If DiagramTimeLineFont = "全部" Then
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If
                                    End If
                                End If
                                If i <= intTimeLine Then
                                    For j = 1 To 4
                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TwoTime2LineColor), CSTimeTablePara.DiagramStylePara.TwoTime2LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.TwoTime2LineStyle)
                                        rBmpGraphics.DrawLine(curPen, LeftBlank + StaBlank * IfPrintStation + sngLeftX + (i - 1) * sngMinuWidth + j * sngMinuWidth / 5, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / 5, sngY2)
                                    Next j
                                End If
                            Next i
                        Case "一分格"
                            intTimeLine = 6 * nTimeWidth
                            sngMinuWidth = sngToWidth / (intTimeLine)
                            For i = 1 To intTimeLine + 1
                                If (i - 1) Mod 6 = 0 Then
                                    curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime60LineColor), CSTimeTablePara.DiagramStylePara.OneTime60LineWidth)
                                    curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.OneTime60LineStyle)
                                    rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                    strTimePrint = Trim(CStr((intFirstTime + (Int((i - 1) / 6))) Mod 24) & ":00")
                                    '打印时间文字,第一行
                                    txtWidth = rBmpGraphics.MeasureString(strTimePrint, f1).Width / 2
                                    txtHeight = rBmpGraphics.MeasureString(strTimePrint, f1).Height

                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - txtHeight + 8)
                                    '最后一行
                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 2)
                                    If bIfPrintEverySta = True Then '每小时打印车站
                                        If i > 1 And i < intTimeLine + 1 Then
                                            For j = 1 To UBound(CSLinkStationInf)
                                                txtWidth = rBmpGraphics.MeasureString(CSLinkStationInf(j).sStationName, fSta).Width / 2
                                                txtHeight = rBmpGraphics.MeasureString(CSLinkStationInf(j).sStationName, fSta).Height / 2
                                                rBmpGraphics.DrawString(CSLinkStationInf(j).sStationName, fSta, Brushes.DarkGray, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, CSLinkStationInf(j).YPicValue - txtHeight)
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

                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime30LineColor), CSTimeTablePara.DiagramStylePara.OneTime30LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.OneTime30LineStyle)

                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)

                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        If DiagramTimeLineFont = "全部" Or DiagramTimeLineFont = "标注半小时格" Then
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If

                                    Else

                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime10LineColor), CSTimeTablePara.DiagramStylePara.OneTime10LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.OneTime10LineStyle)
                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        If DiagramTimeLineFont = "全部" Then
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If
                                    End If
                                End If
                                If i <= intTimeLine Then
                                    For j = 1 To 9
                                        If j = 5 Then
                                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime5LineColor), CSTimeTablePara.DiagramStylePara.OneTime5LineWidth)
                                            curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.OneTime5LineStyle)
                                            rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / 10, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / 10, sngY2)
                                        Else
                                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime1LineColor), CSTimeTablePara.DiagramStylePara.OneTime1LineWidth)
                                            curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.OneTime1LineStyle)
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
                                    curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime60LineColor), CSTimeTablePara.DiagramStylePara.OneTime60LineWidth)
                                    curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.OneTime60LineStyle)
                                    rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                    strTimePrint = Trim(CStr((intFirstTime + (Int((i - 1) / 6))) Mod 24) & ":00")
                                    '打印时间文字,第一行
                                    txtWidth = rBmpGraphics.MeasureString(strTimePrint, f1).Width / 2
                                    txtHeight = rBmpGraphics.MeasureString(strTimePrint, f1).Height

                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - txtHeight + 8)
                                    '最后一行
                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 2)
                                    If bIfPrintEverySta = True Then '每小时打印车站
                                        If i > 1 And i < intTimeLine + 1 Then
                                            For j = 1 To UBound(CSLinkStationInf)
                                                txtWidth = rBmpGraphics.MeasureString(CSLinkStationInf(j).sStationName, fSta).Width / 2
                                                txtHeight = rBmpGraphics.MeasureString(CSLinkStationInf(j).sStationName, fSta).Height / 2
                                                rBmpGraphics.DrawString(CSLinkStationInf(j).sStationName, fSta, Brushes.DarkGray, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, CSLinkStationInf(j).YPicValue - txtHeight)
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

                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime30LineColor), CSTimeTablePara.DiagramStylePara.OneTime30LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.OneTime30LineStyle)

                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)

                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        If DiagramTimeLineFont = "全部" Or DiagramTimeLineFont = "标注半小时格" Then
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If

                                    Else

                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime10LineColor), CSTimeTablePara.DiagramStylePara.OneTime10LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.OneTime10LineStyle)
                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        If DiagramTimeLineFont = "全部" Then
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If
                                    End If
                                End If
                                If i <= intTimeLine Then
                                    Dim tmpNum As Integer
                                    tmpNum = 20
                                    For j = 1 To tmpNum - 1
                                        If j Mod 2 = 0 Then
                                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime5LineColor), CSTimeTablePara.DiagramStylePara.OneTime5LineWidth)
                                            curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.OneTime5LineStyle)
                                            rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / tmpNum, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / tmpNum, sngY2)
                                        Else
                                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime1LineColor), CSTimeTablePara.DiagramStylePara.OneTime1LineWidth)
                                            curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.OneTime1LineStyle)
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
                                    curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime60LineColor), CSTimeTablePara.DiagramStylePara.OneTime60LineWidth)
                                    curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.OneTime60LineStyle)
                                    rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                    strTimePrint = Trim(CStr((intFirstTime + (Int((i - 1) / 6))) Mod 24) & ":00")
                                    '打印时间文字,第一行
                                    txtWidth = rBmpGraphics.MeasureString(strTimePrint, f1).Width / 2
                                    txtHeight = rBmpGraphics.MeasureString(strTimePrint, f1).Height

                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - txtHeight + 8)
                                    '最后一行
                                    rBmpGraphics.DrawString(strTimePrint, f1, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 2)
                                    If bIfPrintEverySta = True Then '每小时打印车站
                                        If i > 1 And i < intTimeLine + 1 Then
                                            For j = 1 To UBound(CSLinkStationInf)
                                                txtWidth = rBmpGraphics.MeasureString(CSLinkStationInf(j).sStationName, fSta).Width / 2
                                                txtHeight = rBmpGraphics.MeasureString(CSLinkStationInf(j).sStationName, fSta).Height / 2
                                                rBmpGraphics.DrawString(CSLinkStationInf(j).sStationName, fSta, Brushes.DarkGray, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, CSLinkStationInf(j).YPicValue - txtHeight)
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

                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime30LineColor), CSTimeTablePara.DiagramStylePara.OneTime30LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.OneTime30LineStyle)

                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)

                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        If DiagramTimeLineFont = "全部" Or DiagramTimeLineFont = "标注半小时格" Then
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If

                                    Else

                                        curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime10LineColor), CSTimeTablePara.DiagramStylePara.OneTime10LineWidth)
                                        curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.OneTime10LineStyle)
                                        rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth, sngY2)
                                        strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                                        'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                                        If DiagramTimeLineFont = "全部" Then
                                            '打印时间文字,第一行
                                            txtWidth = rBmpGraphics.MeasureString(strTimePrint, f).Width / 2
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, topBlank - CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                                            '最后一行
                                            rBmpGraphics.DrawString(strTimePrint, f, nB, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth - txtWidth, PicHeight - topBlank + CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                                        End If
                                    End If
                                End If
                                If i <= intTimeLine Then
                                    Dim tmpNum As Integer
                                    tmpNum = 40
                                    For j = 1 To tmpNum - 1
                                        If j Mod 4 = 0 Then
                                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime5LineColor), CSTimeTablePara.DiagramStylePara.OneTime5LineWidth)
                                            curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.OneTime5LineStyle)
                                            rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / tmpNum, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / tmpNum, sngY2)
                                        Else
                                            curPen = New System.Drawing.Pen(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime1LineColor), CSTimeTablePara.DiagramStylePara.OneTime1LineWidth)
                                            curPen.DashStyle = GetLineStyleFromText(CSTimeTablePara.DiagramStylePara.OneTime1LineStyle)
                                            rBmpGraphics.DrawLine(curPen, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / tmpNum, sngY1, LeftBlank + sngLeftX + StaBlank * IfPrintStation + (i - 1) * sngMinuWidth + j * sngMinuWidth / tmpNum, sngY2)
                                        End If
                                    Next j
                                End If
                            Next i
                    End Select
                Next
            End If
        End If
        'If IfPrintHead = True Then
        '    Call PrintTimetableIndexHead(rBmpGraphics)
        'End If
    End Sub

    Public Sub CSreadStationAndSectionDataFromOracle()

        ReDim UsefulSta(0)
        Dim i, j As Integer
        Dim flag As Boolean = True
        Dim sqlstr As String = ""


        sqlstr = "SELECT * FROM CS_DINNERTIME WHERE LINEID='" & CStr(strCurlineID) & "' and timetableid='" & CStr(DiagramCurID) & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        For i = 0 To tempTable.Rows.Count - 1
            flag = True
            For j = 1 To UBound(UsefulSta)
                If UsefulSta(j) = tempTable.Rows(i).Item("StationName").ToString Then
                    flag = False
                End If
            Next
            If flag = True Then
                ReDim Preserve UsefulSta(UBound(UsefulSta) + 1)
                UsefulSta(UBound(UsefulSta)) = tempTable.Rows(i).Item("StationName").ToString
            End If
        Next

        sqlstr = "SELECT * FROM CS_CHANGEPLACEINF WHERE LINEID='" & CStr(strCurlineID) & "' and timetableid='" & CStr(DiagramCurID) & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        For i = 0 To tempTable.Rows.Count - 1
            flag = True
            For j = 1 To UBound(UsefulSta)
                If UsefulSta(j) = tempTable.Rows(i).Item("ChangePlace").ToString Then
                    flag = False
                End If
            Next
            If flag = True Then
                ReDim Preserve UsefulSta(UBound(UsefulSta) + 1)
                UsefulSta(UBound(UsefulSta)) = tempTable.Rows(i).Item("ChangePlace").ToString
            End If
        Next

        sqlstr = "SELECT * FROM CS_SHIFTPLACEINF WHERE LINEID='" & CStr(strCurlineID) & "' and timetableid='" & CStr(DiagramCurID) & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        For i = 0 To tempTable.Rows.Count - 1
            flag = True
            For j = 1 To UBound(UsefulSta)
                If UsefulSta(j) = tempTable.Rows(i).Item("ShiftPlace") Then
                    flag = False
                End If
            Next
            If flag = True Then
                ReDim Preserve UsefulSta(UBound(UsefulSta) + 1)
                UsefulSta(UBound(UsefulSta)) = tempTable.Rows(i).Item("ShiftPlace").ToString
            End If

        Next

        sqlstr = "SELECT * FROM TMS_TRAININFO WHERE TRAINDIAGRAMID='" & CStr(DiagramCurID) & "' "
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        For i = 0 To tempTable.Rows.Count - 1
            flag = True
            For j = 1 To UBound(UsefulSta)
                If UsefulSta(j) = tempTable.Rows(i).Item("OSTATIONNAME").ToString Then
                    flag = False
                End If
            Next

            If flag = True Then
                ReDim Preserve UsefulSta(UBound(UsefulSta) + 1)
                UsefulSta(UBound(UsefulSta)) = tempTable.Rows(i).Item("OSTATIONNAME").ToString
            End If

            flag = True
            For j = 1 To UBound(UsefulSta)
                If UsefulSta(j) = tempTable.Rows(i).Item("DSTATIONNAME").ToString Then
                    flag = False
                End If
            Next

            If flag = True Then
                ReDim Preserve UsefulSta(UBound(UsefulSta) + 1)
                UsefulSta(UBound(UsefulSta)) = tempTable.Rows(i).Item("DSTATIONNAME").ToString
            End If

        Next

        sqlstr = "SELECT * FROM TMS_DIASTRUCTINFO WHERE IfFixed=1 and TRAINDIAGRAMID='" & DiagramCurID & "' order by StationSeq"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        ReDim CSLinkStationInf(0)
        If tempTable.Rows.Count > 0 Then
            For i = 1 To tempTable.Rows.Count
                For j = 1 To UBound(UsefulSta)
                    If UsefulSta(j) = Trim(tempTable.Rows(i - 1).Item("StationName")) Then
                        ReDim Preserve CSLinkStationInf(CSLinkStationInf.GetUpperBound(0) + 1)
                        'CSLinkStationInf(CSLinkStationInf.GetUpperBound(0)).sStationID = StaIDtoFrontID(tempTranSta(j), "下行")                    '下行方向换乘站的前一站
                        CSLinkStationInf(CSLinkStationInf.GetUpperBound(0)).nID = CSLinkStationInf.GetUpperBound(0)
                        CSLinkStationInf(CSLinkStationInf.GetUpperBound(0)).sStationName = Trim(tempTable.Rows(i - 1).Item("StationName"))
                        CSLinkStationInf(CSLinkStationInf.GetUpperBound(0)).sAtLineName = Trim(tempTable.Rows(i - 1).Item("Linename"))
                        CSLinkStationInf(CSLinkStationInf.GetUpperBound(0)).Ycord = Single.Parse(Trim(tempTable.Rows(i - 1).Item("YCoord").ToString))
                        CSLinkStationInf(CSLinkStationInf.GetUpperBound(0)).sStationSeq = Trim(tempTable.Rows(i - 1).Item("StationSEQ"))
                        Exit For
                    End If
                Next

            Next i
        Else
            ''CSLinkStationInf(CSLinkStationInf.GetUpperBound(0)).YPicValue()
            ''MsgBox("底图结构没有设置或者没有设置默认的底图结构!" & vbCrLf _
            ''                           & "或者运行图版本已更改！", "提示")
            'MsgBox("底图结构没有设置，或者地点设置已更改！", MsgBoxStyle.OkOnly, "提示")
            'CSTimeTablePara.sInputDataError = "底图结构设置错误码"
            Exit Sub
        End If

        ReDim NotSameStationInf(0)
        Dim nMark As Integer
        nMark = 0
        For i = 1 To UBound(CSLinkStationInf)
            nMark = 0
            For j = 1 To UBound(NotSameStationInf)
                If NotSameStationInf(j) = CSLinkStationInf(i).sStationName Then
                    nMark = 1
                    Exit For
                End If
            Next j
            If nMark = 0 Then
                ReDim Preserve NotSameStationInf(UBound(NotSameStationInf) + 1)
                NotSameStationInf(UBound(NotSameStationInf)) = CSLinkStationInf(i).sStationName
            End If
        Next

        'LocalDataSet.TMS_SECTIONINFO.Clear()
        'LocalDataSet.Fill("TMS_SECTIONINFO", "TraindiagramID='" & DiagramCurID & "'")
        Dim sFirSta As String
        Dim sSecSta As String
        Dim sSecName As String
        ReDim CSLinkSectionInf(UBound(CSLinkStationInf) - 1)
        For i = 1 To UBound(CSLinkStationInf) - 1
            sFirSta = CSLinkStationInf(i).sStationName
            sSecSta = CSLinkStationInf(i + 1).sStationName
            sSecName = sFirSta & "->" & sSecSta
            ' ReDim CSLinkSectionInf(j).lDistance(2)
            CSLinkSectionInf(i).nSecNumber = i
            CSLinkSectionInf(i).sSecName = sSecName
            'CSLinkSectionInf(i).sLineName = LocalDataSet.TMS_SECTIONINFO.Rows(j - 1).Item("LineName")
            'CSLinkSectionInf(i).nSection = Val(LocalDataSet.TMS_SECTIONINFO.Rows(j - 1).Item("LineNumber").ToString)
            'CSLinkSectionInf(i).sBlock = LocalDataSet.TMS_SECTIONINFO.Rows(j - 1).Item("Blocktype").ToString
            CSLinkSectionInf(i).sSecFirName = sFirSta
            CSLinkSectionInf(i).sSecSecName = sSecSta
            CSLinkSectionInf(i).nFirStaID = i
            CSLinkSectionInf(i).nSecStaID = i + 1
            CSLinkSectionInf(i).nHStation = i
            CSLinkSectionInf(i).nQStation = i + 1
        Next


    End Sub
    '自定义底图变量
    Public Sub CSIniteDiagramPicViraient()

        '从数据库中读入
        'LocalDataSet.CS_CSTIMETABLESYSTEMPARA.Clear()
        'LocalDataSet.Fill("CS_CSTIMETABLESYSTEMPARA", "LINEID='" & strCurlineID & "'")
        Dim sqlstr As String = ""
        sqlstr = "SELECT * FROM CS_CSTIMETABLESYSTEMPARA WHERE LINEID='" & strCurlineID & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        Dim i As Integer
        For i = 1 To tempTable.Rows.Count
            Select Case Trim(tempTable.Rows(i - 1).Item("PARANAME"))
                Case "底图起始时间"
                    CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime = 4 ' tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "底图显示时间宽"
                    CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime = 24 'tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "时间比较起始时间"
                    CSTimeTablePara.TimeTableDiagramPara.intCompareFirstTime = HourToSecond(Val(tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim))
                Case "底图宽"
                    CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "底图高"
                    CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "底图时分格式"
                    CSTimeTablePara.TimeTableDiagramPara.strTimeFormat = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "底图上下空白高度"
                    CSTimeTablePara.TimeTableDiagramPara.sngtopBlank = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "底图时间空白高度"
                    CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "底图左右空白高度"
                    CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "底图车站空白宽度"
                    CSTimeTablePara.TimeTableDiagramPara.sngStaBlank = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "左边缩进宽度"
                    CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "上面缩进高度"
                    CSTimeTablePara.TimeTableDiagramPara.sngPubTopY = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一分格图1分格线型"
                    CSTimeTablePara.DiagramStylePara.OneTime1LineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一分格图1分格线宽"
                    CSTimeTablePara.DiagramStylePara.OneTime1LineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一分格图1分格线颜色"
                    CSTimeTablePara.DiagramStylePara.OneTime1LineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一分格图5分格线型"
                    CSTimeTablePara.DiagramStylePara.OneTime5LineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一分格图5分格线宽"
                    CSTimeTablePara.DiagramStylePara.OneTime5LineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一分格图5分格线颜色"
                    CSTimeTablePara.DiagramStylePara.OneTime5LineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一分格图10分格线型"
                    CSTimeTablePara.DiagramStylePara.OneTime10LineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一分格图10分格线宽"
                    CSTimeTablePara.DiagramStylePara.OneTime10LineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一分格图10分格线颜色"
                    CSTimeTablePara.DiagramStylePara.OneTime10LineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一分格图30分格线型"
                    CSTimeTablePara.DiagramStylePara.OneTime30LineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一分格图30分格线宽"
                    CSTimeTablePara.DiagramStylePara.OneTime30LineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一分格图30分格线颜色"
                    CSTimeTablePara.DiagramStylePara.OneTime30LineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一分格图60分格线型"
                    CSTimeTablePara.DiagramStylePara.OneTime60LineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一分格图60分格线宽"
                    CSTimeTablePara.DiagramStylePara.OneTime60LineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一分格图60分格线颜色"
                    CSTimeTablePara.DiagramStylePara.OneTime60LineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "二分格图2分格线型"
                    CSTimeTablePara.DiagramStylePara.TwoTime2LineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "二分格图2分格线宽"
                    CSTimeTablePara.DiagramStylePara.TwoTime2LineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "二分格图2分格线颜色"
                    CSTimeTablePara.DiagramStylePara.TwoTime2LineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "二分格图10分格线型"
                    CSTimeTablePara.DiagramStylePara.TwoTime10LineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "二分格图10分格线宽"
                    CSTimeTablePara.DiagramStylePara.TwoTime10LineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "二分格图10分格线颜色"
                    CSTimeTablePara.DiagramStylePara.TwoTime10LineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "二分格图30分格线型"
                    CSTimeTablePara.DiagramStylePara.TwoTime30LineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "二分格图30分格线宽"
                    CSTimeTablePara.DiagramStylePara.TwoTime30LineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "二分格图30分格线颜色"
                    CSTimeTablePara.DiagramStylePara.TwoTime30LineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "二分格图60分格线型"
                    CSTimeTablePara.DiagramStylePara.TwoTime60LineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "二分格图60分格线宽"
                    CSTimeTablePara.DiagramStylePara.TwoTime60LineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "二分格图60分格线颜色"
                    CSTimeTablePara.DiagramStylePara.TwoTime60LineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "十分格图10分格线型"
                    CSTimeTablePara.DiagramStylePara.TenTime10LineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "十分格图10分格线宽"
                    CSTimeTablePara.DiagramStylePara.TenTime10LineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "十分格图10分格线颜色"
                    CSTimeTablePara.DiagramStylePara.TenTime10LineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "十分格图30分格线型"
                    CSTimeTablePara.DiagramStylePara.TenTime30LineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "十分格图30分格线宽"
                    CSTimeTablePara.DiagramStylePara.TenTime30LineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "十分格图30分格线颜色"
                    CSTimeTablePara.DiagramStylePara.TenTime30LineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "十分格图60分格线型"
                    CSTimeTablePara.DiagramStylePara.TenTime60LineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "十分格图60分格线宽"
                    CSTimeTablePara.DiagramStylePara.TenTime60LineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "十分格图60分格线颜色"
                    CSTimeTablePara.DiagramStylePara.TenTime60LineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "小时格图60分格线型"
                    CSTimeTablePara.DiagramStylePara.HourTime60LineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "小时格图60分格线宽"
                    CSTimeTablePara.DiagramStylePara.HourTime60LineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "小时格图60分格线颜色"
                    CSTimeTablePara.DiagramStylePara.HourTime60LineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车站名称字体名称"
                    CSTimeTablePara.DiagramStylePara.StaNameFontName = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车站名称字体大小"
                    CSTimeTablePara.DiagramStylePara.StaNameFontSize = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车站名称字体粗体"
                    CSTimeTablePara.DiagramStylePara.StaNameFontBold = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车站名称字体斜体"
                    CSTimeTablePara.DiagramStylePara.StaNameFontItalic = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车站名称字体颜色"
                    CSTimeTablePara.DiagramStylePara.StaNameFontColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "时间标注字体名称"
                    CSTimeTablePara.DiagramStylePara.TimeFontName = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "时间标注字体大小"
                    CSTimeTablePara.DiagramStylePara.TimeFontSize = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "时间标注字体粗体"
                    CSTimeTablePara.DiagramStylePara.TimeFontBold = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "时间标注字体斜体"
                    CSTimeTablePara.DiagramStylePara.TimeFontItalic = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "时间标注字体颜色"
                    CSTimeTablePara.DiagramStylePara.TimeFontColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一般车站中心线类型"
                    CSTimeTablePara.DiagramStylePara.StaLineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一般车站中心线宽度"
                    CSTimeTablePara.DiagramStylePara.StaLineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "一般车站中心线颜色"
                    CSTimeTablePara.DiagramStylePara.StaLineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "分岔站中心线类型"
                    CSTimeTablePara.DiagramStylePara.FenStaLineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "分岔站中心线宽度"
                    CSTimeTablePara.DiagramStylePara.FenStaLineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "分岔站中心线颜色"
                    CSTimeTablePara.DiagramStylePara.FenStaLineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车场中心线类型"
                    CSTimeTablePara.DiagramStylePara.CheChangStaLineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车场中心线宽度"
                    CSTimeTablePara.DiagramStylePara.CheChangStaLineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车场中心线颜色"
                    CSTimeTablePara.DiagramStylePara.CheChangStaLineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "所有运行线线型"
                    CSTimeTablePara.DiagramStylePara.TrainLineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "所有运行线线宽"
                    CSTimeTablePara.DiagramStylePara.TrainLineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "所有运行线颜色"
                    CSTimeTablePara.DiagramStylePara.TrainLineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "所有车底连接线线型"
                    CSTimeTablePara.DiagramStylePara.CheDiLineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "所有车底连接线线宽"
                    CSTimeTablePara.DiagramStylePara.CheDiLineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "所有车底连接线颜色"
                    CSTimeTablePara.DiagramStylePara.CheDiLineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车次标号字体名称"
                    CSTimeTablePara.DiagramStylePara.CheCiFontName = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车次标号字体大小"
                    CSTimeTablePara.DiagramStylePara.CheCiFontSize = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车次标号字体粗体"
                    CSTimeTablePara.DiagramStylePara.CheCiFontBold = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车次标号字体斜体"
                    CSTimeTablePara.DiagramStylePara.CheCiFontItalic = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车次标号字体颜色"
                    CSTimeTablePara.DiagramStylePara.CheCiFontColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "斜向车次字体名称"
                    CSTimeTablePara.DiagramStylePara.XieCheCiFontName = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "斜向车次字体大小"
                    CSTimeTablePara.DiagramStylePara.XieCheCiFontSize = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "斜向车次字体粗体"
                    CSTimeTablePara.DiagramStylePara.XieCheCiFontBold = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "斜向车次字体斜体"
                    CSTimeTablePara.DiagramStylePara.XieCheCiFontItalic = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "斜向车次字体颜色"
                    CSTimeTablePara.DiagramStylePara.XieCheCiFontColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "是否显示车次"
                    CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi = True  'tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "是否显示司机编号"
                    CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车底交路线高度"
                    CSTimeTablePara.TimeTableDiagramPara.nCheDiLineHeight = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车底交路线类型"
                    CSTimeTablePara.TimeTableDiagramPara.sCheDiLineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车站股道线间距"
                    CSTimeTablePara.StaDiagramePara.nStaLineHeight = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车站站名图高"
                    CSTimeTablePara.TimeTableDiagramPara.sngPicStationHeight = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "车站站名图宽"
                    CSTimeTablePara.TimeTableDiagramPara.sngPicStationWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "运行线可调整时间段"
                    ' TdrawLinePara.sMaxMoveTime = 2 'myTable3.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "运行线移动时间"
                    '  TdrawLinePara.sMoveStepTime = 60 'myTable3.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "未勾选任务段线型"
                    CSTimeTablePara.DiagramStylePara.UnAssignTrainLineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "未勾选任务段线宽"
                    CSTimeTablePara.DiagramStylePara.UnAssignTrainLineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "未勾选任务段颜色"
                    CSTimeTablePara.DiagramStylePara.UnAssignTrainLineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "班中任务线型"
                    CSTimeTablePara.DiagramStylePara.DutyOnLineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "班中任务线宽"
                    CSTimeTablePara.DiagramStylePara.DutyOnLineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "班中任务颜色"
                    CSTimeTablePara.DiagramStylePara.DutyOnLineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "小休任务线型"
                    CSTimeTablePara.DiagramStylePara.DutyRestLineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "小休任务线宽"
                    CSTimeTablePara.DiagramStylePara.DutyRestLineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "小休任务颜色"
                    CSTimeTablePara.DiagramStylePara.DutyRestLineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "用餐任务线型"
                    CSTimeTablePara.DiagramStylePara.DutyDinnerLineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "用餐任务线宽"
                    CSTimeTablePara.DiagramStylePara.DutyDinnerLineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "用餐任务颜色"
                    CSTimeTablePara.DiagramStylePara.DutyDinnerLineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "班后任务线型"
                    CSTimeTablePara.DiagramStylePara.DutyOffLineStyle = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "班后任务线宽"
                    CSTimeTablePara.DiagramStylePara.DutyOffLineWidth = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "班后任务颜色"
                    CSTimeTablePara.DiagramStylePara.DutyOffLineColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "任务标号字体名称"
                    CSTimeTablePara.DiagramStylePara.DutyNoFontName = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "任务标号字体大小"
                    CSTimeTablePara.DiagramStylePara.DutyNoFontSize = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "任务标号字体粗体"
                    CSTimeTablePara.DiagramStylePara.DutyNoFontBold = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "任务标号字体斜体"
                    CSTimeTablePara.DiagramStylePara.DutyNoFontItalic = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
                Case "任务标号字体颜色"
                    CSTimeTablePara.DiagramStylePara.DutyNoFontColor = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
            End Select
        Next

    End Sub

    Public Sub CSInitfrmCSTimetable()
        'ReDim CSDrivers(0)
        'ReDim CSLinkTrain(0)
        CSTrainsAndDrivers.CSDrivers = Nothing
        CSTrainsAndDrivers.CSLinkTrains = Nothing
        'ReDim CopyTrainInf(0)

        'Dim i As Integer
        'CSTimeTablePara.nMaxUndoID = 10
        'ReDim UndoInf(CSTimeTablePara.nMaxUndoID)
        'For i = 1 To UBound(UndoInf)
        '    UndoInf(i).nXuHao = i
        '    ReDim UndoInf(i).Traininf(0)
        '    ReDim UndoInf(i).CheDiInf(0)
        'Next
        'UndoSeq.nCurUndoID = 1
        CSTimeTablePara.nPubTrain = 0
        CSTimeTablePara.nPubCheDi = 0
        CSTimeTablePara.sCurDiagramState = DiagramState.运行图
        CSTimeTablePara.sPubTrainStrainDraw = TrainStrainDraw.无约束
        CSTimeTablePara.sPubCurSkbName = "未命名时刻表"
        CSTimeTablePara.nStaJiShuTuJieSeletedState = 0
        CSTimeTablePara.BifAutoBianCheCi = True
    End Sub

    Public Sub CSInputStationGudaoAndJinLuInfByDAO()

        Dim i, j As Integer
        Dim Str As String
        Dim sqlstr As String = ""
        For i = 1 To UBound(CSLinkStationInf)
            sqlstr = "SELECT * FROM TMS_STATIONINFO WHERE stationname='" & CSLinkStationInf(i).sStationName & "' and TraindiagramID='" & ODSPubpara.DiagramSelect & "'"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)

            If tempTable.Rows.Count > 0 Then
                CSLinkStationInf(i).sStaStyle = tempTable.Rows(0).Item("StationType").ToString.Trim
                CSLinkStationInf(i).sAtLineName = tempTable.Rows(0).Item("LINENAME").ToString.Trim
                CSLinkStationInf(i).sPrintStaName = tempTable.Rows(0).Item("OutputName").ToString.Trim
                CSLinkStationInf(i).sStaProperity = tempTable.Rows(0).Item("StationCharacter").ToString.Trim
                CSLinkStationInf(i).sStationProp = ChaStProp(tempTable.Rows(0).Item("StationCharacter").ToString.Trim, tempTable.Rows(0).Item("StationType").ToString.Trim)
                CSLinkStationInf(i).sEnglishName = tempTable.Rows(0).Item("EnglishShortName").ToString.Trim
            End If

            sqlstr = "SELECT * FROM TMS_LINEDRAW WHERE name='" & CSLinkStationInf(i).sStationName & "'and TraindiagramID='" & ODSPubpara.DiagramSelect & "'"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)

            CSLinkStationInf(i).nStLineNum = 0
            ReDim CSLinkStationInf(i).sStLineNo(0)
            ReDim CSLinkStationInf(i).nStLineUse(0)
            ReDim CSLinkStationInf(i).sLineUse(0)
            ReDim CSLinkStationInf(i).sUpOrDownUse(0)
            ReDim CSLinkStationInf(i).sGuDaoUseSeq(0)
            ReDim CSLinkStationInf(i).sGuDaoName(0)
            If tempTable.Rows.Count > 0 Then
                For j = 1 To tempTable.Rows.Count
                    Str = tempTable.Rows(j - 1).Item("PARTLINETYPE").ToString.Trim
                    If Str.Length >= 3 Then
                        If Str.Substring(Str.Length - 3) = "股道线" Then
                            CSLinkStationInf(i).nStLineNum = CSLinkStationInf(i).nStLineNum + 1
                            ReDim Preserve CSLinkStationInf(i).sStLineNo(UBound(CSLinkStationInf(i).sStLineNo) + 1)
                            ReDim Preserve CSLinkStationInf(i).nStLineUse(UBound(CSLinkStationInf(i).sStLineNo) + 1)
                            ReDim Preserve CSLinkStationInf(i).sLineUse(UBound(CSLinkStationInf(i).sStLineNo) + 1)
                            ReDim Preserve CSLinkStationInf(i).sUpOrDownUse(UBound(CSLinkStationInf(i).sStLineNo) + 1)
                            ReDim Preserve CSLinkStationInf(i).sGuDaoUseSeq(UBound(CSLinkStationInf(i).sStLineNo) + 1)
                            ReDim Preserve CSLinkStationInf(i).sGuDaoName(UBound(CSLinkStationInf(i).sStLineNo) + 1)
                            CSLinkStationInf(i).sStLineNo(UBound(CSLinkStationInf(i).sStLineNo)) = tempTable.Rows(j - 1).Item("DAOCHA_ID").ToString.Trim()
                            CSLinkStationInf(i).sLineUse(UBound(CSLinkStationInf(i).sStLineNo)) = tempTable.Rows(j - 1).Item("TRACKTYPE").ToString.Trim
                            CSLinkStationInf(i).sUpOrDownUse(UBound(CSLinkStationInf(i).sStLineNo)) = tempTable.Rows(j - 1).Item("TRACKUSAGE").ToString.Trim
                            CSLinkStationInf(i).sGuDaoUseSeq(UBound(CSLinkStationInf(i).sStLineNo)) = tempTable.Rows(j - 1).Item("TRACKUSINGNUM").ToString.Trim
                            CSLinkStationInf(i).nStLineUse(UBound(CSLinkStationInf(i).sStLineNo)) = ChaLineUse(tempTable.Rows(j - 1).Item("TRACKTYPE").ToString.Trim, tempTable.Rows(j - 1).Item("TRACKUSAGE").ToString.Trim)
                            CSLinkStationInf(i).sGuDaoName(UBound(CSLinkStationInf(i).sStLineNo)) = tempTable.Rows(j - 1).Item("CONTROLMOD").ToString.Trim
                        End If
                    End If
                Next
            End If



            ''导入车站进路和分岔站股道使用
            'ReDim CSLinkStationInf(i).sTrackUse(0)

            With CSLinkStationInf(i)
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
            End With


            With CSLinkStationInf(i)
                ReDim Preserve .IKK(17)
                ReDim Preserve .IKH(17)
                ReDim Preserve .IHH(17)
                ReDim Preserve .IHK(17)
            End With
            'ReDim CSLinkStationInf(i).sCrossUse(0)
        Next i
    End Sub
    '根据车站名确定车站ID
    Public Function CSFromStaNameToStaIDByStationinf(ByVal StaName As String, ByVal StaSeq As String) As Integer
        Dim i As Integer
        CSFromStaNameToStaIDByStationinf = 0
        For i = 1 To UBound(CSLinkStationInf)
            If CSLinkStationInf(i).sStationName = StaName Then   'And CSLinkStationInf(i).sStationSeq = StaSeq
                CSFromStaNameToStaIDByStationinf = i
                Exit For
            End If
        Next i
    End Function

    '得到当前车底线的高度
    Public Function GetCurDriverLineHeight(ByVal nCurId As Integer) As Integer
        Dim i As Integer
        Dim CurX1 As Single
        CurX1 = CSDriverLinkinf(nCurId).X1
        Dim nHeight() As Integer
        ReDim nHeight(0)
        For i = 1 To UBound(CSDriverLinkinf)
            If i <> nCurId Then
                If CSDriverLinkinf(i).nIFPrint = True And CSDriverLinkinf(i).Y1 = CSDriverLinkinf(nCurId).Y1 And CSDriverLinkinf(i).upOrDown = CSDriverLinkinf(nCurId).upOrDown Then
                    If CurX1 >= CSDriverLinkinf(i).X1 And CurX1 <= CSDriverLinkinf(i).X2 Then
                        ReDim Preserve nHeight(UBound(nHeight) + 1)
                        nHeight(UBound(nHeight)) = CSDriverLinkinf(i).LineHeight
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
                GetCurDriverLineHeight = N
                Exit Do
            End If
            N = N + 1
        Loop
        CSDriverLinkinf(nCurId).nIFPrint = True
        CSDriverLinkinf(nCurId).LineHeight = GetCurDriverLineHeight
    End Function
End Module
