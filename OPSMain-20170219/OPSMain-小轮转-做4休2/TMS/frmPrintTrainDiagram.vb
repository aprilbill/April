Imports AutoCAD
Imports AutoCAD.AcadLineTypeClass
'Imports AutoCAD.acadc

Public Class frmPrintTrainDiagram
    Dim intCurPrintPage As Integer
    Dim bigFont As Font
    Dim bigFontColor As Color
    Dim smallFont As Font
    Dim SmallFontColor As Color
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Call RefreshDiagram(0)
        Me.Close()
    End Sub

    Private Sub btnBeginCAD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBeginCAD.Click
        Dim nBetime As Integer
        Dim nMinutWidth As Integer
        Dim i As Integer
        Dim j As Integer
        Me.labProInfor.Visible = True
        Me.labProInfor.Text = "正在输出运行线至CAD系统中..."

        Try
            AcadApp = CreateObject("AutoCAD.Application")
            Me.Cursor = Cursors.WaitCursor
        Catch ex As Exception
            MsgBox(ex.Message.ToString & " 不能运行AutoCAD 2004，请检查是否安装了 AutoCAD 2004")
            Me.Cursor = Cursors.Default
            Exit Sub
        End Try

        Dim Point(2) As Double
        Dim StartPoint(2) As Double
        Dim StartPoint1(2) As Double
        Dim StartPoint2(2) As Double
        Dim EndPoint(2) As Double
        Dim EndPoint1(2) As Double
        Dim EndPoint2(2) As Double
        Dim RotateAngle As Double

        '文本属性
        Dim StyObj1 As AcadTextStyle
        Dim typeFace As String
        Dim Bold As Boolean
        Dim Italic As Boolean
        Dim CharSet As Long
        Dim PicChandFamily As Long
        RotateAngle = 0

        'Dim TextObj As AcadText
        Dim textString As String
        Dim InsertPoint(2) As Double
        Dim Height As Double
        Dim cColor As ACAD_COLOR
        Dim cWidth As ACAD_LWEIGHT


        Dim ToHeight As Double
        Dim tmpMinHeight As Double
        Dim tmpMaxHeight As Double
        tmpMinHeight = 100000
        tmpMaxHeight = 0
        ToHeight = 10000 'MainForm.TU.Height 
        Dim SepHeight As Double
        SepHeight = 80

        nBetime = Val(Me.cmbCADStartTime.Text)
        nMinutWidth = Val(Me.cmbCADMinuteWidth.Text)
        If nMinutWidth = 0 Then
            MsgBox("分钟宽度刻度不能为零!", , "提示")
            Exit Sub
        End If

        testLayer = AcadApp.ActiveDocument.Layers.Add("车站中心线")
        AcadApp.ActiveDocument.ActiveLayer = testLayer
        StyObj1 = AcadApp.ActiveDocument.TextStyles.Add("SKY定义")
        typeFace = "Arial"
        Italic = False
        Bold = False
        CharSet = 1
        PicChandFamily = 1 Or 16
        StyObj1.SetFont(typeFace, Bold, Italic, CharSet, PicChandFamily)
        Me.proWhole.Visible = True
        Me.ProStep.Visible = True

        Me.proWhole.Maximum = 1000
        Me.ProStep.Value = 0
        Me.labProInfor.Text = "正在输出车站中心线..."
        Me.ProStep.Maximum = UBound(StationInf)
        Dim nGeWidth As Integer
        Dim nStaSize As Integer
        nStaSize = Me.NumStaSize.Value

        If Me.optTime30S.Checked = True Then '30s格
            nGeWidth = 120
        Else
            nGeWidth = 60
        End If
        For i = 1 To UBound(StationInf)
            If StationInf(i).YPicValue > tmpMaxHeight Then
                tmpMaxHeight = StationInf(i).YPicValue
            End If

            If StationInf(i).YPicValue < tmpMinHeight Then
                tmpMinHeight = StationInf(i).YPicValue
            End If

            StartPoint(0) = 0
            StartPoint(1) = ToHeight - StationInf(i).YPicValue
            StartPoint(2) = 0
            EndPoint(0) = 24.0# * nGeWidth * nMinutWidth
            EndPoint(1) = StartPoint(1)
            EndPoint(2) = 0
            Call AddCADLine(StartPoint, EndPoint, 0, ACAD_LWEIGHT.acLnWt020)
            textString = StationInf(i).sPrintStaName

            InsertPoint(0) = -Len(textString) * 15 - 10
            InsertPoint(1) = StartPoint(1) - 5
            InsertPoint(2) = 0
            Call addCADText(StyObj1, textString, InsertPoint, nStaSize, RotateAngle, 0)
            If Me.chkIfPrintStaName.Checked = True Then
                For j = 2 To 24
                    InsertPoint(0) = (j - 1) * nGeWidth * nMinutWidth - Len(textString) * 15 / 2
                    InsertPoint(1) = StartPoint(1) - 5
                    InsertPoint(2) = 0
                    Call addCADText(StyObj1, textString, InsertPoint, nStaSize, RotateAngle, ACAD_COLOR.acCyan)
                Next
            End If
            Me.ProStep.Value = i
        Next i


        StartPoint(0) = 0
        StartPoint(1) = ToHeight - tmpMaxHeight - SepHeight
        StartPoint(2) = 0
        EndPoint(0) = 24.0# * nGeWidth * nMinutWidth
        EndPoint(1) = StartPoint(1)
        EndPoint(2) = 0
        Call AddCADLine(StartPoint, EndPoint, 0, ACAD_LWEIGHT.acLnWt020)

        StartPoint(0) = 0
        StartPoint(1) = ToHeight - tmpMinHeight + SepHeight
        StartPoint(2) = 0
        EndPoint(0) = 24.0# * nGeWidth * nMinutWidth
        EndPoint(1) = StartPoint(1)
        EndPoint(2) = 0
        Call AddCADLine(StartPoint, EndPoint, 0, ACAD_LWEIGHT.acLnWt020)

        Dim sPath As String
        testLayer = AcadApp.ActiveDocument.Layers.Add("时间线")

        sPath = GetAppPath() & "pic\acadiso.lin"
        AcadApp.ActiveDocument.Linetypes.Load("dashed", sPath)
        AcadApp.ActiveDocument.Linetypes.Load("dashdot", sPath)
        testLayer.color = AcColor.acBlue
        AcadApp.ActiveDocument.ActiveLayer = testLayer


        Me.ProStep.Maximum = 24 * nGeWidth + 1
        Me.ProStep.Value = 0
        Me.proWhole.Value = 10
        Me.labProInfor.Text = "正在输出时间刻度线..."

        If Me.optTime30S.Checked = True Then '30s格
            For i = 1 To 24 * nGeWidth + 1
                StartPoint(0) = (i - 1) * nMinutWidth
                StartPoint(1) = ToHeight - tmpMaxHeight
                StartPoint(2) = 0
                EndPoint(0) = StartPoint(0)
                EndPoint(1) = ToHeight - tmpMinHeight
                EndPoint(2) = 0

                If (i - 1) Mod 120 = 0 Then
                    StartPoint(1) = StartPoint(1) - SepHeight
                    EndPoint(1) = EndPoint(1) + SepHeight
                End If
                If (i - 1) Mod 2 = 0 Then
                    If (i - 1) Mod 20 = 0 Then
                        If (i - 1) Mod 120 = 0 Then
                            AcadApp.ActiveDocument.ActiveLinetype = AcadApp.ActiveDocument.Linetypes.Item(0)
                            textString = CADOutPrintTimeFormat(Val((i - 1) / 2), 1)
                            Height = 15
                            InsertPoint(0) = StartPoint(0) - 57
                            InsertPoint(1) = EndPoint(1) - 20
                            InsertPoint(2) = 0
                            Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                            InsertPoint(0) = StartPoint(0) + 5
                            InsertPoint(1) = EndPoint(1) - 20
                            InsertPoint(2) = 0
                            Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                            InsertPoint(0) = StartPoint(0) - 57
                            InsertPoint(1) = StartPoint(1) + 5
                            InsertPoint(2) = 0
                            Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                            InsertPoint(0) = StartPoint(0) + 5
                            InsertPoint(1) = StartPoint(1) + 5
                            InsertPoint(2) = 0
                            Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)
                        Else
                            AcadApp.ActiveDocument.ActiveLinetype = AcadApp.ActiveDocument.Linetypes.Item("dashdot")
                            textString = CADOutPrintTimeFormat(Val((i - 1) / 2), 2)
                            Height = 10
                            InsertPoint(0) = StartPoint(0) - 10
                            InsertPoint(1) = EndPoint(1) + SepHeight - 15 '+ 5
                            InsertPoint(2) = 0
                            Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                            InsertPoint(0) = StartPoint(0) - 10
                            InsertPoint(1) = StartPoint(1) - SepHeight + 5 ' - 20
                            InsertPoint(2) = 0
                            Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)
                        End If

                    Else
                        AcadApp.ActiveDocument.ActiveLinetype = AcadApp.ActiveDocument.Linetypes.Item(0)
                    End If
                Else
                    AcadApp.ActiveDocument.ActiveLinetype = AcadApp.ActiveDocument.Linetypes.Item("dashed")
                End If

                If Me.optQuJian.Checked = True Then
                    '按区间画，很慢
                    If (i - 1) Mod 120 = 0 Then
                        Call AddCADLine(StartPoint, EndPoint, ACAD_COLOR.acBlue, ACAD_LWEIGHT.acLnWt020)
                    Else
                        For j = 1 To UBound(SectionInf)
                            StartPoint(0) = (i - 1) * nMinutWidth
                            StartPoint(1) = ToHeight - StationInf(SectionInf(j).nFirStaID).YPicValue
                            StartPoint(2) = 0
                            EndPoint(0) = StartPoint(0)
                            EndPoint(1) = ToHeight - StationInf(SectionInf(j).nSecStaID).YPicValue
                            EndPoint(2) = 0
                            If (i - 1) Mod 10 = 0 Then
                                cColor = ACAD_COLOR.acBlue
                            Else
                                cColor = 0
                            End If
                            Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)
                        Next j
                    End If
                Else
                    If (i - 1) Mod 10 = 0 Then
                        cColor = ACAD_COLOR.acBlue
                    Else
                        cColor = 0
                    End If
                    '不按区间画
                    Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)
                End If

                Me.ProStep.Value = i
            Next i

        ElseIf Me.optCAD1.Checked = True Then '一分格

            For i = 1 To 24 * 60 + 1
                StartPoint(0) = (i - 1) * nMinutWidth
                StartPoint(1) = ToHeight - tmpMaxHeight
                StartPoint(2) = 0
                EndPoint(0) = StartPoint(0)
                EndPoint(1) = ToHeight - tmpMinHeight
                EndPoint(2) = 0

                If (i - 1) Mod 60 = 0 Then
                    StartPoint(1) = StartPoint(1) - SepHeight
                    EndPoint(1) = EndPoint(1) + SepHeight
                End If
                If (i - 1) Mod 5 = 0 Then
                    If (i - 1) Mod 10 = 0 Then
                        If (i - 1) Mod 60 = 0 Then
                            AcadApp.ActiveDocument.ActiveLinetype = AcadApp.ActiveDocument.Linetypes.Item(0)
                            textString = CADOutPrintTimeFormat(Val(i - 1), 1)
                            Height = 15
                            InsertPoint(0) = StartPoint(0) - 57
                            InsertPoint(1) = EndPoint(1) - 20
                            InsertPoint(2) = 0
                            Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                            InsertPoint(0) = StartPoint(0) + 5
                            InsertPoint(1) = EndPoint(1) - 20
                            InsertPoint(2) = 0
                            Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                            InsertPoint(0) = StartPoint(0) - 57
                            InsertPoint(1) = StartPoint(1) + 5
                            InsertPoint(2) = 0
                            Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                            InsertPoint(0) = StartPoint(0) + 5
                            InsertPoint(1) = StartPoint(1) + 5
                            InsertPoint(2) = 0
                            Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)
                        Else
                            AcadApp.ActiveDocument.ActiveLinetype = AcadApp.ActiveDocument.Linetypes.Item("dashdot")
                            textString = CADOutPrintTimeFormat(Val(i - 1), 2)
                            Height = 10
                            InsertPoint(0) = StartPoint(0) - 10
                            InsertPoint(1) = EndPoint(1) + SepHeight - 15 '+ 5
                            InsertPoint(2) = 0
                            Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                            InsertPoint(0) = StartPoint(0) - 10
                            InsertPoint(1) = StartPoint(1) - SepHeight + 5 ' - 20
                            InsertPoint(2) = 0
                            Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)
                        End If

                    Else
                        AcadApp.ActiveDocument.ActiveLinetype = AcadApp.ActiveDocument.Linetypes.Item("dashed")
                    End If
                Else
                    AcadApp.ActiveDocument.ActiveLinetype = AcadApp.ActiveDocument.Linetypes.Item(0)
                End If

                If Me.optQuJian.Checked = True Then
                    '按区间画，很慢
                    If (i - 1) Mod 60 = 0 Then
                        Call AddCADLine(StartPoint, EndPoint, ACAD_COLOR.acBlue, ACAD_LWEIGHT.acLnWt020)
                    Else
                        For j = 1 To UBound(SectionInf)
                            StartPoint(0) = (i - 1) * nMinutWidth
                            StartPoint(1) = ToHeight - StationInf(SectionInf(j).nFirStaID).YPicValue
                            StartPoint(2) = 0
                            EndPoint(0) = StartPoint(0)
                            EndPoint(1) = ToHeight - StationInf(SectionInf(j).nSecStaID).YPicValue
                            EndPoint(2) = 0
                            If (i - 1) Mod 10 = 0 Then
                                cColor = ACAD_COLOR.acBlue
                            Else
                                cColor = 0
                            End If
                            Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)
                        Next j
                    End If
                Else
                    If (i - 1) Mod 10 = 0 Then
                        cColor = ACAD_COLOR.acBlue
                    Else
                        cColor = 0
                    End If
                    '不按区间画
                    Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)
                End If

                'Next j
                Me.ProStep.Value = i
            Next i
        ElseIf Me.optCAD60.Checked = True Then '小时格
            For i = 1 To 24 * 1 + 1
                'For j = 1 To UBound(SectionInf)
                StartPoint(0) = (i - 1) * nMinutWidth * 60
                StartPoint(1) = ToHeight - tmpMaxHeight
                StartPoint(2) = 0
                EndPoint(0) = StartPoint(0)
                EndPoint(1) = ToHeight - tmpMinHeight
                EndPoint(2) = 0

                StartPoint(1) = StartPoint(1) - SepHeight
                EndPoint(1) = EndPoint(1) + SepHeight
                AcadApp.ActiveDocument.ActiveLinetype = AcadApp.ActiveDocument.Linetypes.Item(0)
                textString = CADOutPrintTimeFormat(Val((i - 1) * 60), 1)
                Height = 15
                InsertPoint(0) = StartPoint(0) - 57
                InsertPoint(1) = EndPoint(1) - 20
                InsertPoint(2) = 0
                Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                InsertPoint(0) = StartPoint(0) + 5
                InsertPoint(1) = EndPoint(1) - 20
                InsertPoint(2) = 0
                Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                InsertPoint(0) = StartPoint(0) - 57
                InsertPoint(1) = StartPoint(1) + 5 '- 20
                InsertPoint(2) = 0
                Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                InsertPoint(0) = StartPoint(0) + 5
                InsertPoint(1) = StartPoint(1) + 5 '- 20
                InsertPoint(2) = 0
                Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                Call AddCADLine(StartPoint, EndPoint, ACAD_COLOR.acBlue, ACAD_LWEIGHT.acLnWt020)

                'Next j
                Me.ProStep.Value = i
            Next i
        End If


        testLayer = AcadApp.ActiveDocument.Layers.Add("运行线")
        'testLayer.color = AcColor.acRed
        AcadApp.ActiveDocument.ActiveLayer = testLayer
        AcadApp.ActiveDocument.ActiveLinetype = AcadApp.ActiveDocument.Linetypes.Item(0)

        '下面开始输出列车
        Dim Alf As Single
        Dim PrintText As String
        Dim nPrintCheCiNum As Integer
        nPrintCheCiNum = Me.NumCheCi.Value
        If Me.cmbLineWidth.Text = "细线" Then
            cWidth = ACAD_LWEIGHT.acLnWt020
        Else
            cWidth = ACAD_LWEIGHT.acLnWt030
        End If
        If UBound(TrainInf) > 0 Then
            Me.ProStep.Maximum = UBound(TrainInf)
            Me.ProStep.Value = 0
            Me.proWhole.Value = 300
            Me.labProInfor.Text = "正在输出运行线..."

            For i = 1 To UBound(TrainInf)
                If TrainInf(i).Train <> "" Then
                    If TrainInf(i).TrainStyle = "运行车" Then
                        If i Mod 2 = 0 Then '上行
                            cColor = ACAD_COLOR.acRed
                        Else
                            cColor = ACAD_COLOR.acGreen
                        End If
                    Else
                        cColor = 0
                    End If
                    For j = 1 To UBound(TrainInf(i).nPassSection)
                        StartPoint(0) = FromTimeToCADX(TrainInf(i).Starting(TrainInf(i).nFirstID(j)), nMinutWidth)
                        StartPoint(1) = ToHeight - StationInf(TrainInf(i).nFirstID(j)).YPicValue
                        StartPoint(2) = 0
                        EndPoint(0) = FromTimeToCADX(TrainInf(i).Arrival(TrainInf(i).nSecondID(j)), nMinutWidth)
                        EndPoint(1) = ToHeight - StationInf(TrainInf(i).nSecondID(j)).YPicValue
                        EndPoint(2) = 0
                        Call AddCADLine(StartPoint, EndPoint, cColor, cWidth)
                        If j <> 1 Then
                            StartPoint1(0) = FromTimeToCADX(TrainInf(i).Arrival(TrainInf(i).nFirstID(j)), nMinutWidth)
                            StartPoint1(1) = StartPoint(1)
                            StartPoint1(2) = 0
                            EndPoint1(0) = StartPoint(0)
                            EndPoint1(1) = StartPoint(1)
                            EndPoint1(2) = 0
                            Call AddCADLine(StartPoint1, EndPoint1, cColor, cWidth)
                        End If

                        If j <> UBound(TrainInf(i).nPassSection) Then
                            StartPoint2(0) = EndPoint(0)
                            StartPoint2(1) = EndPoint(1)
                            StartPoint2(2) = 0
                            EndPoint2(0) = FromTimeToCADX(TrainInf(i).Starting(TrainInf(i).nSecondID(j)), nMinutWidth)
                            EndPoint2(1) = EndPoint(1)
                            EndPoint2(2) = 0
                            Call AddCADLine(StartPoint2, EndPoint2, cColor, cWidth)
                        End If
                        If j = 1 Then
                            Alf = ((Math.Atan((EndPoint(1) - StartPoint(1)) / (EndPoint(0) - StartPoint(0)))))
                            If i Mod 2 = 0 Then '上行
                                StartPoint(0) = StartPoint(0) - 4
                            Else
                                StartPoint(0) = StartPoint(0) + 4
                            End If
                            PrintText = TrainInf(i).sPrintTrain
                            Call addCADText(StyObj1, PrintText, StartPoint, nPrintCheCiNum, Alf, 0)
                        End If
                    Next j
                End If
                Me.ProStep.Value = i
            Next i
        End If
        '下面开始输出车底交路

        Dim nFtrain As Integer
        Dim nNtrain As Integer
        Dim sX1 As Single
        Dim sX2 As Single
        Dim sY1 As Single
        Dim sY2 As Single
        Dim sX3 As Single
        'Dim sY3 As Single
        Dim sX4 As Single
        Dim sY4 As Single
        Dim sXmid As Long
        Dim TempHei As Single
        Dim nBaseHeight As Single
        nBaseHeight = Me.NumCheDiLine.Value
        TempHei = 40
        cColor = 0

        If Me.cmbChediLine.Text = "长方形" Then '长方形线
            For i = 1 To UBound(CheDiLinkinf)
                nFtrain = CheDiLinkinf(i).nFirTrain
                nNtrain = CheDiLinkinf(i).nSecTrain
                TempHei = -(nBaseHeight + CheDiLinkinf(i).LineHeight * nBaseHeight) * CheDiLinkinf(i).upOrDown
                If nFtrain > 0 Then
                    sX2 = FromTimeToCADX(TrainInf(nFtrain).Arrival(TrainInf(nFtrain).nSecondID(UBound(TrainInf(nFtrain).nPassSection))), nMinutWidth)
                    sY2 = ToHeight - StationInf(TrainInf(nFtrain).nSecondID(UBound(TrainInf(nFtrain).nPassSection))).YPicValue
                Else
                    sX2 = -1
                    sY2 = -1
                End If
                If nNtrain > 0 Then
                    sX3 = FromTimeToCADX(TrainInf(nNtrain).Starting(TrainInf(nNtrain).nFirstID(1)), nMinutWidth)
                Else
                    sX3 = -1
                End If
                'sX4 = FromTimeToCADX(TrainInf(nNtrain).Starting(TrainInf(nNtrain).nSecondID(UBound(TrainInf(nNtrain).nPassSection))), nMinutWidth)
                'sY4 = ToHeight - StationInf(TrainInf(nNtrain).nSecondID(UBound(TrainInf(nNtrain).nPassSection))).YPicValue

                StartPoint(0) = sX2
                StartPoint(1) = sY2
                StartPoint(2) = 0

                StartPoint2(0) = sX2
                StartPoint2(1) = sY2 + TempHei
                StartPoint2(2) = 0

                EndPoint(0) = sX3
                EndPoint(1) = sY2
                EndPoint(2) = 0

                EndPoint2(0) = sX3
                EndPoint2(1) = sY2 + TempHei
                EndPoint2(2) = 0

                Select Case CheDiLinkinf(i).sStyle
                    Case 0 '矩形
                        Call AddCADLine(StartPoint, StartPoint2, cColor, cWidth)
                        Call AddCADLine(StartPoint2, EndPoint2, cColor, cWidth)
                        Call AddCADLine(EndPoint, EndPoint2, cColor, cWidth)
                        '    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X1, Y1, X1, Y1 + sngHeight)
                        '    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X2, Y2, X2, Y2 + sngHeight)
                        '    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X1, Y1 + sngHeight, X2, Y2 + sngHeight)
                    Case 1 '左上
                        'rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X2, Y2, X2, Y2 + sngHeight)
                        'rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X1, Y1 + sngHeight, X1 - 5, Y1 + sngHeight - 5)
                        'rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X2, Y2 + sngHeight, X1, Y1 + sngHeight)

                    Case 2 '右下
                        '    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X2, Y2, X2, Y2 + sngHeight)
                        '    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X1, Y1 + sngHeight, X1 - 5, Y1 + sngHeight + 5)
                        '    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X2, Y2 + sngHeight, X1, Y1 + sngHeight)

                    Case 3 '左下
                        '    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X1, Y1, X1, Y1 + sngHeight)
                        '    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X2, Y2 + sngHeight, X2 + 5, Y2 + sngHeight + 5)
                        '    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X1, Y1 + sngHeight, X2, Y2 + sngHeight)

                    Case 4 '右上
                        '    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X1, Y1, X1, Y1 + sngHeight)
                        '    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X2, Y2 + sngHeight, X2 + 5, Y2 + sngHeight - 5)
                        '    rBmpGraphics.DrawLine(CheDiLinkinf(nPaiXu(i)).tmpPen, X1, Y1 + sngHeight, X2, Y2 + sngHeight)

                End Select
            Next i

        Else '三角形线


            If UBound(ChediInfo) > 0 Then
                Me.ProStep.Maximum = UBound(ChediInfo)
                Me.ProStep.Value = 0
                Me.proWhole.Value = 700
                Me.labProInfor.Text = "正在输出车底交路线..."
                For i = 1 To UBound(ChediInfo)

                    If UBound(ChediInfo(i).nLinkTrain) = 1 Then
                        nFtrain = ChediInfo(i).nLinkTrain(1)
                        sX1 = FromTimeToCADX(TrainInf(nFtrain).Arrival(TrainInf(nFtrain).nFirstID(1)), nMinutWidth)
                        sY1 = ToHeight - StationInf(TrainInf(nFtrain).nFirstID(1)).YPicValue
                        sX2 = FromTimeToCADX(TrainInf(nFtrain).Starting(TrainInf(nFtrain).nSecondID(UBound(TrainInf(nFtrain).nPassSection))), nMinutWidth)
                        sY2 = ToHeight - StationInf(TrainInf(nFtrain).nSecondID(UBound(TrainInf(nFtrain).nPassSection))).YPicValue

                        If nFtrain Mod 2 = 0 Then

                            StartPoint(0) = sX1
                            StartPoint(1) = sY1
                            StartPoint(2) = 0
                            EndPoint(0) = sX1
                            EndPoint(1) = sY1 - TempHei
                            EndPoint(2) = 0
                            Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                            StartPoint(0) = sX1
                            StartPoint(1) = sY1 - TempHei
                            StartPoint(2) = 0
                            EndPoint(0) = sX1 - 30
                            EndPoint(1) = sY1 - TempHei
                            EndPoint(2) = 0
                            Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                            textString = Trim(ChediInfo(i).sCheCiHao)
                            Height = 10
                            InsertPoint(0) = sX1 - Len(textString) * 8
                            InsertPoint(1) = sY1 - TempHei + 3
                            InsertPoint(2) = 0
                            Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)


                            StartPoint(0) = sX2
                            StartPoint(1) = sY2
                            StartPoint(2) = 0
                            EndPoint(0) = sX2
                            EndPoint(1) = sY2 + TempHei
                            EndPoint(2) = 0
                            Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                            StartPoint(0) = sX2
                            StartPoint(1) = sY2 + TempHei
                            StartPoint(2) = 0
                            EndPoint(0) = sX2 + 30
                            EndPoint(1) = sY2 + TempHei
                            EndPoint(2) = 0
                            Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                            textString = Trim(ChediInfo(i).sCheCiHao)
                            Height = 10
                            InsertPoint(0) = sX2
                            InsertPoint(1) = sY2 + TempHei + 3
                            InsertPoint(2) = 0
                            Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                        Else

                            StartPoint(0) = sX1
                            StartPoint(1) = sY1
                            StartPoint(2) = 0
                            EndPoint(0) = sX1
                            EndPoint(1) = sY1 + TempHei
                            EndPoint(2) = 0
                            Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                            StartPoint(0) = sX1
                            StartPoint(1) = sY1 + TempHei
                            StartPoint(2) = 0
                            EndPoint(0) = sX1 - 30
                            EndPoint(1) = sY1 + TempHei
                            EndPoint(2) = 0
                            Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                            textString = Trim(ChediInfo(i).sCheCiHao)
                            Height = 10
                            InsertPoint(0) = sX1 - Len(textString) * 8
                            InsertPoint(1) = sY1 + TempHei + 3
                            InsertPoint(2) = 0
                            Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)


                            StartPoint(0) = sX2
                            StartPoint(1) = sY2
                            StartPoint(2) = 0
                            EndPoint(0) = sX2
                            EndPoint(1) = sY2 - TempHei
                            EndPoint(2) = 0
                            Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                            StartPoint(0) = sX2
                            StartPoint(1) = sY2 - TempHei
                            StartPoint(2) = 0
                            EndPoint(0) = sX2 + 30
                            EndPoint(1) = sY2 - TempHei
                            EndPoint(2) = 0
                            Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                            textString = Trim(ChediInfo(i).sCheCiHao)
                            Height = 10
                            InsertPoint(0) = sX2 + 3
                            InsertPoint(1) = sY2 - TempHei + 3
                            InsertPoint(2) = 0
                            Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                        End If

                    Else

                        For j = 1 To UBound(ChediInfo(i).nLinkTrain) - 1


                            nFtrain = ChediInfo(i).nLinkTrain(j)
                            nNtrain = ChediInfo(i).nLinkTrain(j + 1)

                            sX1 = FromTimeToCADX(TrainInf(nFtrain).Arrival(TrainInf(nFtrain).nFirstID(1)), nMinutWidth)
                            sY1 = ToHeight - StationInf(TrainInf(nFtrain).nFirstID(1)).YPicValue
                            sX2 = FromTimeToCADX(TrainInf(nFtrain).Arrival(TrainInf(nFtrain).nSecondID(UBound(TrainInf(nFtrain).nPassSection))), nMinutWidth)
                            sY2 = ToHeight - StationInf(TrainInf(nFtrain).nSecondID(UBound(TrainInf(nFtrain).nPassSection))).YPicValue

                            sX3 = FromTimeToCADX(TrainInf(nNtrain).Starting(TrainInf(nNtrain).nFirstID(1)), nMinutWidth)
                            sX4 = FromTimeToCADX(TrainInf(nNtrain).Starting(TrainInf(nNtrain).nSecondID(UBound(TrainInf(nNtrain).nPassSection))), nMinutWidth)
                            sY4 = ToHeight - StationInf(TrainInf(nNtrain).nSecondID(UBound(TrainInf(nNtrain).nPassSection))).YPicValue
                            sXmid = sX2 + (sX3 - sX2) / 2


                            If j = 1 Then
                                TempHei = 20
                                If nFtrain Mod 2 = 0 Then

                                    StartPoint(0) = sX1
                                    StartPoint(1) = sY1
                                    StartPoint(2) = 0
                                    EndPoint(0) = sX1
                                    EndPoint(1) = sY1 - TempHei
                                    EndPoint(2) = 0
                                    Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                                    StartPoint(0) = sX1
                                    StartPoint(1) = sY1 - TempHei
                                    StartPoint(2) = 0
                                    EndPoint(0) = sX1 - 30
                                    EndPoint(1) = sY1 - TempHei
                                    EndPoint(2) = 0
                                    Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                                    textString = Trim(ChediInfo(i).sCheCiHao)
                                    Height = 10
                                    InsertPoint(0) = sX1 - Len(textString) * 8
                                    InsertPoint(1) = sY1 - TempHei + 3
                                    InsertPoint(2) = 0
                                    Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                                Else

                                    StartPoint(0) = sX1
                                    StartPoint(1) = sY1
                                    StartPoint(2) = 0
                                    EndPoint(0) = sX1
                                    EndPoint(1) = sY1 + TempHei
                                    EndPoint(2) = 0
                                    Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                                    StartPoint(0) = sX1
                                    StartPoint(1) = sY1 + TempHei
                                    StartPoint(2) = 0
                                    EndPoint(0) = sX1 - 30
                                    EndPoint(1) = sY1 + TempHei
                                    EndPoint(2) = 0
                                    Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                                    textString = Trim(ChediInfo(i).sCheCiHao)
                                    Height = 10
                                    InsertPoint(0) = sX1 - Len(textString) * 8
                                    InsertPoint(1) = sY1 + TempHei + 3
                                    InsertPoint(2) = 0
                                    Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)
                                End If
                            End If

                            If (nFtrain + nNtrain) Mod 2 <> 0 Then
                                TempHei = 20
                            Else
                                TempHei = 0
                            End If

                            If nFtrain Mod 2 = 0 Then '上行
                                If sX2 <= sX3 Then
                                    If sX3 - sX2 > 400 Then '折返时间过长时
                                        '                                If sX2 >= CurX3 And sX2 <= CurX4 Then
                                        '                                    Printer.Line (sX2, sY2)-(sX2 + 5, sY2 - TempHei), cColor
                                        '                                    Call PrintTextInObject(Printer, sX2, sY2 - TempHei, sX2 + 5, sY2 - TempHei, Trim(ChediInfo(i).sCheCiHao), "center", "bottom", vbBlue, 9)
                                        '                                End If
                                        '                                If sX3 >= CurX3 And sX3 <= CurX4 Then
                                        '                                    Printer.Line (sX3 - 5, sY2 - TempHei)-(sX3, sY2), cColor
                                        '                                    Call PrintTextInObject(Printer, sX3 - 5, sY2 - TempHei - 2, sX3, sY2 - TempHei, Trim(ChediInfo(i).sCheCiHao), "center", "bottom", vbBlue, 9)
                                        '                                End If
                                    Else
                                        StartPoint(0) = sX2
                                        StartPoint(1) = sY2
                                        StartPoint(2) = 0
                                        EndPoint(0) = sXmid
                                        EndPoint(1) = sY2 + TempHei
                                        EndPoint(2) = 0
                                        Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                                        StartPoint(0) = sXmid
                                        StartPoint(1) = sY2 + TempHei
                                        StartPoint(2) = 0
                                        EndPoint(0) = sX3
                                        EndPoint(1) = sY2
                                        EndPoint(2) = 0
                                        Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)


                                        textString = Trim(ChediInfo(i).sCheCiHao)
                                        Height = 10
                                        InsertPoint(0) = sXmid - Len(textString) * 9 / 2
                                        InsertPoint(1) = sY2 + TempHei + 3
                                        InsertPoint(2) = 0
                                        Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                                    End If
                                End If

                            Else '下行
                                If sX2 <= sX3 Then
                                    If sX3 - sX2 > 400 Then '折返时间过长时
                                        '                                If sX2 >= CurX3 And sX2 <= CurX4 Then
                                        '                                    Printer.Line (sX2, sY2)-(sX2 + 5, sY2 + TempHei), cColor
                                        '                                    Call PrintTextInObject(Printer, sX2, sY2 + TempHei, sX2 + 10, sY2 + TempHei, Trim(ChediInfo(i).sCheCiHao), "center", "top", vbBlue, 9)
                                        '                                End If
                                        '                                If sX3 >= CurX3 And sX3 <= CurX4 Then
                                        '                                    Printer.Line (sX3 - 5, sY2 + TempHei)-(sX3, sY2), cColor
                                        '                                    Call PrintTextInObject(Printer, sX3 - 5, sY2 + TempHei, sX3, sY2 + TempHei, Trim(ChediInfo(i).sCheCiHao), "center", "top", vbBlue, 9)
                                        '                                End If
                                    Else
                                        StartPoint(0) = sX2
                                        StartPoint(1) = sY2
                                        StartPoint(2) = 0
                                        EndPoint(0) = sXmid
                                        EndPoint(1) = sY2 - TempHei
                                        EndPoint(2) = 0
                                        Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                                        StartPoint(0) = sXmid
                                        StartPoint(1) = sY2 - TempHei
                                        StartPoint(2) = 0
                                        EndPoint(0) = sX3
                                        EndPoint(1) = sY2
                                        EndPoint(2) = 0
                                        Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                                        textString = Trim(ChediInfo(i).sCheCiHao)
                                        Height = 10
                                        InsertPoint(0) = sXmid - Len(textString) * 9 / 2
                                        InsertPoint(1) = sY2 - TempHei - 10
                                        InsertPoint(2) = 0
                                        Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)
                                    End If
                                End If
                            End If


                            If j = UBound(ChediInfo(i).nLinkTrain) - 1 Then
                                TempHei = 20
                                If nNtrain Mod 2 = 0 Then

                                    StartPoint(0) = sX4
                                    StartPoint(1) = sY4
                                    StartPoint(2) = 0
                                    EndPoint(0) = sX4
                                    EndPoint(1) = sY4 + TempHei
                                    EndPoint(2) = 0
                                    Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                                    StartPoint(0) = sX4
                                    StartPoint(1) = sY4 + TempHei
                                    StartPoint(2) = 0
                                    EndPoint(0) = sX4 + 30
                                    EndPoint(1) = sY4 + TempHei
                                    EndPoint(2) = 0
                                    Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                                    textString = Trim(ChediInfo(i).sCheCiHao)
                                    Height = 10
                                    InsertPoint(0) = sX4
                                    InsertPoint(1) = sY4 + TempHei + 3
                                    InsertPoint(2) = 0
                                    Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                                Else

                                    StartPoint(0) = sX4
                                    StartPoint(1) = sY4
                                    StartPoint(2) = 0
                                    EndPoint(0) = sX4
                                    EndPoint(1) = sY4 - TempHei
                                    EndPoint(2) = 0
                                    Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                                    StartPoint(0) = sX4
                                    StartPoint(1) = sY4 - TempHei
                                    StartPoint(2) = 0
                                    EndPoint(0) = sX4 + 30
                                    EndPoint(1) = sY4 - TempHei
                                    EndPoint(2) = 0
                                    Call AddCADLine(StartPoint, EndPoint, cColor, ACAD_LWEIGHT.acLnWt020)

                                    textString = Trim(ChediInfo(i).sCheCiHao)
                                    Height = 10
                                    InsertPoint(0) = sX4 + 3
                                    InsertPoint(1) = sY4 - TempHei + 3
                                    InsertPoint(2) = 0
                                    Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)
                                End If
                            End If
                        Next j
                    End If
                    Me.ProStep.Value = i
                Next i
            End If
        End If

        Me.proWhole.Value = 1000
        Me.labProInfor.Text = "运行结束"
        Me.ProStep.Visible = False
        Me.proWhole.Visible = False
        Me.labProInfor.Visible = False
        Me.Cursor = Cursors.Default
        AcadApp.Visible = True
        AcadApp.ZoomExtents()
        AppActivate(AcadApp.Caption)
    End Sub
    ''' <summary>
    ''' 显示时间格式
    ''' </summary>
    ''' <param name="nMinute">分钟值</param>
    ''' <param name="nMark">1为小时格式,2为分钟格式,3为30S格式</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CADOutPrintTimeFormat(ByVal nMinute As Long, ByVal nMark As Integer) As String
        CADOutPrintTimeFormat = "00:00"
        Dim nTime As Long
        nTime = Val(Me.cmbCADStartTime.Text) * 60 + nMinute
        If nTime >= 24 * 60 Then
            nTime = nTime - 24 * 60
        End If
        Select Case nMark
            Case 1 '小时格式
                CADOutPrintTimeFormat = Microsoft.VisualBasic.Left(dTime(nTime * 60, 0), 5)

            Case 2 '一分格
                CADOutPrintTimeFormat = Microsoft.VisualBasic.Left(Microsoft.VisualBasic.Right(MinuteToHour(Val(nTime)), 5), 2)

        End Select


    End Function

    Private Function FromTimeToCADX(ByVal nTime As Long, ByVal nMinuteWidth As Integer) As Double
        If nTime = -1 Then
            FromTimeToCADX = -100000
            Exit Function
        End If

        Dim nBetime As Long
        nBetime = Val(Me.cmbCADStartTime.Text) * 3600
        If nTime < nBetime Then
            nTime = nTime + 24 * 3600.0#
        End If
        FromTimeToCADX = (nTime - nBetime) * nMinuteWidth / 60
    End Function

    Private Sub printDocDiagram_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles printDocDiagram.PrintPage
        intCurPrintPage = intCurPrintPage + 1
        Dim newWidth As Single
        Dim newHeight As Single
        Dim nLeftBlank As Single
        Dim nTopBlank As Single
        Dim nTopTitleHeight As Single
        Dim nTimeBlank As Single
        Dim nStaBlank As Single
        Dim nLeftX As Single
        Dim nTolPageNum As Integer
        Dim intBeTime As Integer
        Dim intEndTime As Integer
        Dim intPageTime As Integer
        Dim strDiagramTimeFormat As String
        Dim strDiagramTimeLineFont As String
        Dim nifPrintCheCi As Boolean
        Dim nifPrintXieCheCi As Boolean
        Dim nCheDiHeight As Integer
        nCheDiHeight = Me.NumCheDiLineHeight.Value

        If Me.chkPrintCheCi.Checked = True Then
            nifPrintCheCi = True
        Else
            nifPrintCheCi = False
        End If

        If Me.chkPrintXieCheCi.Checked = True Then
            nifPrintXieCheCi = True
        Else
            nifPrintXieCheCi = False
        End If

        intBeTime = Me.numBeTime.Value
        intEndTime = Me.numEndTime.Value
        strDiagramTimeFormat = Me.cmbTimeFormat.Text.Trim
        strDiagramTimeLineFont = Me.cmbTimeLineFont.Text.Trim
        If intEndTime <= intBeTime Then
            intEndTime = intEndTime + 24
        End If
        intPageTime = Me.numPageTime.Value
        If (intEndTime - intBeTime) / intPageTime = Int((intEndTime - intBeTime) / intPageTime) Then
            nTolPageNum = (intEndTime - intBeTime) / intPageTime
        Else
            nTolPageNum = Int((intEndTime - intBeTime) / intPageTime) + 1
        End If
        nStaBlank = TimeTablePara.TimeTableDiagramPara.sngStaBlank '50
        nTopTitleHeight = Me.numBigTitleHeight.Value
        nTimeBlank = TimeTablePara.TimeTableDiagramPara.sngTimeBlank  ' 60
        nLeftX = -30
        If Me.printDocDiagram.DefaultPageSettings.Landscape = True Then
            newWidth = Me.printDocDiagram.DefaultPageSettings.PaperSize.Height
            newHeight = Me.printDocDiagram.DefaultPageSettings.PaperSize.Width
        Else
            newWidth = Me.printDocDiagram.DefaultPageSettings.PaperSize.Width
            newHeight = Me.printDocDiagram.DefaultPageSettings.PaperSize.Height
        End If
        nLeftBlank = TimeTablePara.TimeTableDiagramPara.sngLeftBlank + 60 '100
        nTopBlank = TimeTablePara.TimeTableDiagramPara.sngtopBlank ' 50
        newWidth = newWidth '- nLeftBlank ' * 2
        newHeight = newHeight - nTopBlank * 2 - nTopTitleHeight + nTopBlank / 2
        Dim nFirTime As Integer
        nFirTime = (intBeTime + (intCurPrintPage - 1) * intPageTime) Mod 24
        Dim nActFirTime As Integer
        Dim nActPageTime As Integer
        Dim txtWidth As Single
        If Me.optStandard.Checked = True Then
            nActFirTime = nFirTime
            nActPageTime = intPageTime
            txtWidth = e.Graphics.MeasureString(Me.txtBigTitle.Text.Trim, bigFont).Width
            e.Graphics.DrawString(Me.txtBigTitle.Text, bigFont, New SolidBrush(bigFontColor), newWidth / 2 - txtWidth / 2, Me.numBigTitleMarge.Value)
            e.Graphics.DrawString(DateTime.Now.ToString, smallFont, New SolidBrush(SmallFontColor), 100, Me.numSmallTitleMerge.Value)
            txtWidth = e.Graphics.MeasureString(Me.txtSmallTitle.Text.Trim, smallFont).Width
            e.Graphics.DrawString(Me.txtSmallTitle.Text, smallFont, New SolidBrush(SmallFontColor), newWidth - txtWidth - nLeftBlank, Me.numSmallTitleMerge.Value)
            Call DrawTimeLine(e.Graphics, Nothing, Nothing, newWidth, newHeight, nLeftBlank, nTopBlank, nStaBlank, nTimeBlank, nLeftX, nTopTitleHeight, nActFirTime, nActPageTime, strDiagramTimeFormat, strDiagramTimeLineFont, 1, Me.chkEveryPrintSta.Checked, False)
            Call DrawDiagramLine(e.Graphics, newWidth, nActFirTime, nActPageTime, nLeftBlank, nStaBlank, nLeftX, nifPrintCheCi, nifPrintXieCheCi, Me.cmbCheDiLineStyle.Text, False, nCheDiHeight)
        ElseIf Me.optBanTu.Checked = True Then
            nActFirTime = nFirTime - 1
            nActPageTime = intPageTime + 1
            txtWidth = e.Graphics.MeasureString(Me.txtBigTitle.Text.Trim, bigFont).Width
            e.Graphics.DrawString(Me.txtBigTitle.Text, bigFont, New SolidBrush(bigFontColor), newWidth / 2 - txtWidth / 2, Me.numBigTitleMarge.Value)
            'e.Graphics.DrawString(DateTime.Now.ToString, smallFont, New SolidBrush(SmallFontColor), 100, Me.numSmallTitleMerge.Value)
            txtWidth = e.Graphics.MeasureString(Me.txtSmallTitle.Text.Trim, smallFont).Width
            e.Graphics.DrawString(Me.txtSmallTitle.Text, smallFont, New SolidBrush(SmallFontColor), newWidth - txtWidth - nLeftBlank, Me.numSmallTitleMerge.Value)
            Call DrawTimeLine(e.Graphics, Nothing, Nothing, newWidth, newHeight, nLeftBlank, nTopBlank, nStaBlank, nTimeBlank, nLeftX, nTopTitleHeight, nActFirTime, nActPageTime, strDiagramTimeFormat, strDiagramTimeLineFont, 1, Me.chkEveryPrintSta.Checked, False)
            Call DrawDiagramLine(e.Graphics, newWidth, nActFirTime, nActPageTime, nLeftBlank, nStaBlank, nLeftX, nifPrintCheCi, nifPrintXieCheCi, Me.cmbCheDiLineStyle.Text, False, nCheDiHeight)
        ElseIf Me.optLantu.Checked = True Then
            nActFirTime = nFirTime
            nActPageTime = intPageTime
            txtWidth = e.Graphics.MeasureString(Me.txtBigTitle.Text.Trim, bigFont).Width
            'e.Graphics.DrawString("         年       月        日", bigFont, New SolidBrush(bigFontColor), 100, Me.numBigTitleMarge.Value)
            'e.Graphics.DrawString(DateTime.Now.ToString, smallFont, New SolidBrush(SmallFontColor), 100, Me.numSmallTitleMerge.Value)
            'txtWidth = e.Graphics.MeasureString(Me.txtSmallTitle.Text.Trim, smallFont).Width
            'e.Graphics.DrawString(Me.txtSmallTitle.Text, smallFont, New SolidBrush(SmallFontColor), newWidth - txtWidth - nLeftBlank, Me.numSmallTitleMerge.Value)
            Call DrawTimeLine(e.Graphics, Nothing, Nothing, newWidth, newHeight, nLeftBlank, nTopBlank, nStaBlank, nTimeBlank, nLeftX, nTopTitleHeight, nActFirTime, nActPageTime, strDiagramTimeFormat, strDiagramTimeLineFont, 1, Me.chkEveryPrintSta.Checked, True)
            Call DrawDiagramLine(e.Graphics, newWidth, nActFirTime, nActPageTime, nLeftBlank, nStaBlank, nLeftX, nifPrintCheCi, nifPrintXieCheCi, Me.cmbCheDiLineStyle.Text, False, nCheDiHeight)
        End If


        If intCurPrintPage < nTolPageNum Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
            intCurPrintPage = 0
        End If
    End Sub

    Private Sub btnPreView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreView.Click
        Dim ppd As New PrintPreviewDialog()
        Try
            ppd.Document = printDocDiagram

            ppd.ShowDialog()
        Catch exp As Exception
            MessageBox.Show("An error occurred while trying to load the " & _
                "document for Print Preview. Make sure you currently have " & _
                "access to a printer. A printer must be connected and " & _
                "accessible for Print Preview to work.", Me.Text, _
                 MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPageSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageSet.Click
        Dim psd As New PageSetupDialog()
        With psd
            .Document = printDocDiagram
            .PageSettings = printDocDiagram.DefaultPageSettings
        End With

        If psd.ShowDialog = Windows.Forms.DialogResult.OK Then
            printDocDiagram.DefaultPageSettings = psd.PageSettings
        End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim dialog As New PrintDialog()
        dialog.Document = printDocDiagram

        If dialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            printDocDiagram.Print()
        End If
    End Sub

    Private Sub frmPrintTrainDiagram_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Call RefreshDiagram(0)
    End Sub

    Private Sub frmPrintTrainDiagram_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        intCurPrintPage = 0
        bigFont = New Font("黑体", 24)
        smallFont = New Font("宋体", 12)
        bigFontColor = Color.Blue
        SmallFontColor = Color.Green
        Me.txtBigTitle.Text = TimeTablePara.sPubCurSkbName '& "列车运行图"
        Me.txtSmallTitle.Text = SystemPara.sUserCompanyName '"请在这输入小标题名称"
        Select Case GetUserName()
            Case "北京地铁"
                Me.chkPrintCheCi.Checked = False
                Me.chkPrintXieCheCi.Checked = True
                Me.chkEveryPrintSta.Checked = True
                Me.cmbCheDiLineStyle.Text = "直角形"
            Case Else
                Me.chkPrintCheCi.Checked = True
                Me.chkPrintXieCheCi.Checked = False
                Me.chkEveryPrintSta.Checked = False
                Me.cmbCheDiLineStyle.Text = "三角形"

        End Select

    End Sub

    Private Sub btnBigTitleFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBigTitleFont.Click
        Dim nd As New FontDialog
        nd.ShowColor = True
        nd.Font = bigFont
        nd.Color = bigFontColor
        If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
            bigFont = nd.Font
            bigFontColor = nd.Color
        End If
    End Sub

    Private Sub btnSmallTitleFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmallTitleFont.Click
        Dim nd As New FontDialog
        nd.ShowColor = True
        nd.Font = smallFont
        nd.Color = SmallFontColor
        If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
            smallFont = nd.Font
            SmallFontColor = nd.Color
        End If
    End Sub

End Class