Imports autocad
Imports AutoCAD.AcadApplicationClass
Public Class frmPrintCheDiJiaoLu

    Private Sub frmPrintCheDiJiaoLu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim CheDiSeq() As Integer
        Dim i As Integer
        ReDim CheDiSeq(0)
        Me.lstCheDi.Items.Clear()
        Me.chkLstCheDiNum.Items.Clear()
        Me.lstPrintSta.Items.Clear()
        Me.lstSta.Items.Clear()
        For i = 1 To UBound(ChediInfo)
            If ChediInfo(i).sCheCiHao.Trim <> "" And UBound(ChediInfo(i).nLinkTrain) > 0 Then
                ReDim Preserve CheDiSeq(UBound(CheDiSeq) + 1)
                CheDiSeq(UBound(CheDiSeq)) = i
            End If
        Next

        Dim j, k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long
        Dim nTrain As Integer
        Dim nTrain2 As Integer
        '按到达时间排序
        Flag = 1
        k = UBound(CheDiSeq)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                nTrain = ChediInfo(CheDiSeq(j)).nLinkTrain(1)
                nTrain2 = ChediInfo(CheDiSeq(j + 1)).nLinkTrain(1)

                TempTime1 = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
                Temptime2 = TrainInf(nTrain2).Starting(TrainInf(nTrain2).nPathID(1))
                If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                    temp = CheDiSeq(j)
                    CheDiSeq(j) = CheDiSeq(j + 1)
                    CheDiSeq(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop

        For i = 1 To UBound(CheDiSeq)
            Me.lstCheDi.Items.Add(ChediInfo(CheDiSeq(i)).sCheCiHao)
        Next

        Me.cmbLineWidth.Items.Add("10")
        Me.cmbLineWidth.Items.Add("15")
        Me.cmbLineWidth.Items.Add("20")
        Me.cmbLineWidth.Items.Add("25")
        Me.cmbLineWidth.Items.Add("30")
        Me.cmbLineWidth.Items.Add("35")
        Me.cmbLineWidth.Items.Add("40")

        Me.CombJLWidth.Items.Add("100")
        Me.CombJLWidth.Items.Add("150")
        Me.CombJLWidth.Items.Add("250")
        Me.CombJLWidth.Items.Add("300")
        Me.CombJLWidth.Items.Add("400")

        For i = 1 To 20
            Me.cmbFontHeight.Items.Add(i)
        Next i


        '对车底按发点排序

        Call SetCheDiSeqExcel(CheDiSeq)
        For i = 1 To UBound(CheDiSeq)
            Me.chkLstCheDiNum.Items.Add(ChediInfo(CheDiSeq(i)).sCheCiHao)
            Me.chkLstCheDiNum.SetItemChecked(i - 1, True)
        Next
        Call chkLstCheDiNum_SelectedIndexChanged(Nothing, Nothing)

        '将可能输出的车站简化名称
        Dim sFirSta As String
        Dim sEndSta As String
        Dim nState As Integer
        nState = 0
        For i = 1 To UBound(ChediInfo)
            For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                nState = 0
                nTrain = ChediInfo(i).nLinkTrain(j)
                sFirSta = TrainInf(nTrain).ComeStation
                sEndSta = TrainInf(nTrain).NextStation
                For k = 1 To Me.lstSta.Items.Count
                    If Me.lstSta.Items(k - 1) = sFirSta Then
                        nState = 1
                        Exit For
                    End If
                Next
                If nState = 0 Then
                    Me.lstSta.Items.Add(sFirSta)
                End If

                nState = 0
                For k = 1 To Me.lstSta.Items.Count
                    If Me.lstSta.Items(k - 1) = sEndSta Then
                        nState = 1
                        Exit For
                    End If
                Next
                If nState = 0 Then
                    Me.lstSta.Items.Add(sEndSta)
                End If

            Next
        Next

        For i = 1 To Me.lstSta.Items.Count
            Me.lstPrintSta.Items.Add(Me.lstSta.Items(i - 1))
        Next
    End Sub

    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        Dim i As Integer
        For i = 1 To Me.lstCheDi.Items.Count
            Me.lstCheDi.SetItemChecked(i - 1, True)
        Next
    End Sub

    Private Sub btnNotSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNotSelectAll.Click
        Dim i As Integer
        For i = 1 To Me.lstCheDi.Items.Count
            Me.lstCheDi.SetItemChecked(i - 1, False)
        Next
    End Sub

    Private Sub btnCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Try
            AcadApp = CreateObject("AutoCAD.Application")
            Me.Cursor = Cursors.WaitCursor
        Catch ex As Exception
            MsgBox("不能运行AutoCAD 2004，请检查是否安装了 AutoCAD 2004")
            Me.Cursor = Cursors.Default
            Exit Sub
        End Try

        Dim Point(2) As Double
        Dim StartPoint(2) As Double
        Dim EndPoint(2) As Double

        '文本属性
        Dim StyObj1 As AcadTextStyle
        Dim typeFace As String
        Dim Bold As Boolean
        Dim Italic As Boolean
        Dim CharSet As Long
        Dim PicChandFamily As Long
        Dim RotateAngle As Double

        'Dim TextObj As AcadText
        Dim textString As String
        Dim InsertPoint(2) As Double
        Dim Height As Double

        Dim ToHeight As Double
        ToHeight = 1000
        RotateAngle = 0
        Dim SepWidth As Double
        Dim sepJLWidth As Double
        Dim fontHeight As Single
        SepWidth = Val(Me.cmbLineWidth.Text)
        If SepWidth <= 0 Then
            MsgBox("交路线宽不能为零或小于零，请重新选择！")
            Exit Sub
        End If

        sepJLWidth = Val(Me.CombJLWidth.Text)
        If sepJLWidth <= 0 Then
            MsgBox("交路与占空间宽度不能为零或小于零，请重新选择！")
            Exit Sub
        End If

        fontHeight = Val(Me.cmbFontHeight.Text)
        If fontHeight <= 0 Then
            MsgBox("文字大小不能为零或小于零，请重新选择！")
            Exit Sub
        End If

        testLayer = AcadApp.ActiveDocument.Layers.Add("车底交路线")
        AcadApp.ActiveDocument.ActiveLayer = testLayer

        StyObj1 = AcadApp.ActiveDocument.TextStyles.Add("SKY定义")
        typeFace = "Arial"
        Italic = False
        Bold = False
        CharSet = 1
        PicChandFamily = 1 Or 16
        StyObj1.SetFont(typeFace, Bold, Italic, CharSet, PicChandFamily)

        Dim nTrain As Integer
        Dim tmpPoint(2) As Double
        Dim nXuID As Integer
        If Me.lstCheDi.CheckedItems.Count > 0 Then
            Me.proBar.Visible = True
            Me.proBar.Value = 0
            Me.proBar.Maximum = Me.lstCheDi.CheckedItems.Count
            For k = 1 To Me.lstCheDi.CheckedItems.Count
                For i = 1 To UBound(ChediInfo)
                    If ChediInfo(i).sCheCiHao = Me.lstCheDi.CheckedItems(k - 1) Then
                        nXuID = nXuID + 1
                        tmpPoint(0) = nXuID * sepJLWidth - SepWidth / 2
                        tmpPoint(1) = ToHeight

                        textString = ChediInfo(i).sCheCiHao
                        Height = 8
                        InsertPoint(0) = nXuID * sepJLWidth - 8
                        InsertPoint(1) = ToHeight + 15
                        InsertPoint(2) = 0
                        Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                        For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                            nTrain = ChediInfo(i).nLinkTrain(j)
                            If j Mod 2 <> 0 Then
                                If j = 1 Then
                                    StartPoint(0) = tmpPoint(0) + SepWidth / 2
                                    StartPoint(1) = tmpPoint(1)
                                    StartPoint(2) = 0
                                    EndPoint(0) = nXuID * sepJLWidth + SepWidth
                                    EndPoint(1) = ToHeight - j * 10
                                    EndPoint(2) = 0
                                    Call AddCADLine(StartPoint, EndPoint, 0, ACAD_LWEIGHT.acLnWt020)

                                    textString = TrainInf(nTrain).ComeStation & "(" & dTime(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(1)), 0) & ")"
                                    Height = fontHeight
                                    InsertPoint(0) = StartPoint(0) - Len(textString) * 4 - 2
                                    InsertPoint(1) = StartPoint(1)
                                    InsertPoint(2) = 0
                                    Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                                    textString = TrainInf(nTrain).NextStation & "(" & dTime(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))), 0) & ")"
                                    Height = fontHeight
                                    InsertPoint(0) = EndPoint(0) + 2
                                    InsertPoint(1) = EndPoint(1) + 3
                                    InsertPoint(2) = 0
                                    Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                                ElseIf j = UBound(ChediInfo(i).nLinkTrain) Then

                                    StartPoint(0) = tmpPoint(0)
                                    StartPoint(1) = tmpPoint(1)
                                    StartPoint(2) = 0
                                    EndPoint(0) = nXuID * sepJLWidth
                                    EndPoint(1) = ToHeight - j * 10
                                    EndPoint(2) = 0
                                    Call AddCADLine(StartPoint, EndPoint, 0, ACAD_LWEIGHT.acLnWt020)

                                    textString = "(" & dTime(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(1)), 0) & ")"
                                    Height = fontHeight
                                    InsertPoint(0) = StartPoint(0) - Len(textString) * 4 - 2
                                    InsertPoint(1) = StartPoint(1) - 5
                                    InsertPoint(2) = 0
                                    Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                                    textString = TrainInf(nTrain).NextStation & "(" & dTime(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))), 0) & ")"
                                    Height = fontHeight
                                    InsertPoint(0) = EndPoint(0) + 2
                                    InsertPoint(1) = EndPoint(1)
                                    InsertPoint(2) = 0
                                    Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)
                                Else
                                    StartPoint(0) = tmpPoint(0)
                                    StartPoint(1) = tmpPoint(1)
                                    StartPoint(2) = 0
                                    EndPoint(0) = nXuID * sepJLWidth + SepWidth
                                    EndPoint(1) = ToHeight - j * 10
                                    EndPoint(2) = 0
                                    Call AddCADLine(StartPoint, EndPoint, 0, ACAD_LWEIGHT.acLnWt020)

                                    textString = "(" & dTime(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(1)), 0) & ")"
                                    Height = fontHeight
                                    InsertPoint(0) = StartPoint(0) - Len(textString) * 4 - 2
                                    InsertPoint(1) = StartPoint(1) - 5
                                    InsertPoint(2) = 0
                                    Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                                    textString = TrainInf(nTrain).NextStation & "(" & dTime(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))), 0) & ")"
                                    Height = fontHeight
                                    InsertPoint(0) = EndPoint(0) + 2
                                    InsertPoint(1) = EndPoint(1) + 3
                                    InsertPoint(2) = 0
                                    Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)
                                End If
                            Else
                                If j = UBound(ChediInfo(i).nLinkTrain) Then
                                    StartPoint(0) = tmpPoint(0)
                                    StartPoint(1) = tmpPoint(1)
                                    StartPoint(2) = 0
                                    EndPoint(0) = nXuID * sepJLWidth
                                    EndPoint(1) = ToHeight - j * 10
                                    EndPoint(2) = 0
                                    Call AddCADLine(StartPoint, EndPoint, 0, ACAD_LWEIGHT.acLnWt020)

                                    textString = "(" & dTime(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(1)), 0) & ")"
                                    Height = fontHeight
                                    InsertPoint(0) = StartPoint(0) + 2
                                    InsertPoint(1) = StartPoint(1) - 5
                                    InsertPoint(2) = 0
                                    Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                                    textString = TrainInf(nTrain).NextStation & "(" & dTime(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))), 0) & ")"
                                    Height = fontHeight
                                    InsertPoint(0) = EndPoint(0) - Len(textString) * 4 - 2
                                    InsertPoint(1) = EndPoint(1)
                                    InsertPoint(2) = 0
                                    Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)
                                Else
                                    StartPoint(0) = tmpPoint(0)
                                    StartPoint(1) = tmpPoint(1)
                                    StartPoint(2) = 0
                                    EndPoint(0) = nXuID * sepJLWidth - SepWidth
                                    EndPoint(1) = ToHeight - j * 10
                                    EndPoint(2) = 0
                                    Call AddCADLine(StartPoint, EndPoint, 0, ACAD_LWEIGHT.acLnWt020)

                                    textString = "(" & dTime(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(1)), 0) & ")"
                                    Height = fontHeight
                                    InsertPoint(0) = StartPoint(0) + 2
                                    InsertPoint(1) = StartPoint(1) - 5
                                    InsertPoint(2) = 0
                                    Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)

                                    textString = TrainInf(nTrain).NextStation & "(" & dTime(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))), 0) & ")"
                                    Height = fontHeight
                                    InsertPoint(0) = EndPoint(0) - Len(textString) * 4 - 2
                                    InsertPoint(1) = EndPoint(1) + 3
                                    InsertPoint(2) = 0
                                    Call addCADText(StyObj1, textString, InsertPoint, Height, RotateAngle, 0)
                                End If
                            End If

                            tmpPoint(0) = EndPoint(0)
                            tmpPoint(1) = EndPoint(1)
                        Next j
                        Exit For
                    End If
                Next i
                Me.proBar.Value = k
            Next k
            Me.proBar.Visible = False
        End If

        Me.Cursor = Cursors.Default
        AcadApp.Visible = True
        AcadApp.ZoomExtents()
        'AcadApp.AppActivate(AcadApp.Caption)

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim i As Integer
        For i = 1 To Me.chkLstCheDiNum.Items.Count
            Me.chkLstCheDiNum.SetItemChecked(i - 1, True)
        Next i
        Call chkLstCheDiNum_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim i As Integer
        For i = 1 To Me.chkLstCheDiNum.Items.Count
            Me.chkLstCheDiNum.SetItemChecked(i - 1, False)
        Next i
        Call chkLstCheDiNum_SelectedIndexChanged(Nothing, Nothing)
    End Sub

    Private Sub chkLstCheDiNum_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLstCheDiNum.SelectedIndexChanged
        Me.numRowNum.Maximum = Me.chkLstCheDiNum.CheckedItems.Count
        Me.numRowNum.Minimum = 1
    End Sub

    Private Sub lstPrintSta_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstPrintSta.DoubleClick
        Dim sSta As String
        sSta = Me.lstPrintSta.Items(Me.lstPrintSta.SelectedIndex)
        Dim sNewSta As String
        sNewSta = InputBox("请输入新的名称", "输入", sSta)
        If sNewSta <> "" Then
            Me.lstPrintSta.Items(Me.lstPrintSta.SelectedIndex) = sNewSta
        End If
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim i, j, p, q As Integer
        Dim nRowNum As Integer
        nRowNum = Me.numRowNum.Value
        Me.Cursor = Cursors.WaitCursor
        Dim datav As New Data.DataView
        Dim OutPutNum() As String
        ReDim OutPutNum(0)
        If Me.chkLstCheDiNum.CheckedItems.Count <= 0 Then
            MsgBox("你选择输出的车底编号为空，请输入！")
            Exit Sub
        End If
        For i = 1 To Me.chkLstCheDiNum.CheckedItems.Count
            ReDim Preserve OutPutNum(UBound(OutPutNum) + 1)
            OutPutNum(UBound(OutPutNum)) = Me.chkLstCheDiNum.CheckedItems(i - 1)
        Next

        Dim Rows As Integer
        Dim Cols As Integer
        Try
            Dim myExcel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application
            Dim myBook As Microsoft.Office.Interop.Excel.Workbook
            Dim mySheet As Microsoft.Office.Interop.Excel.Worksheet
            myBook = myExcel.Workbooks.Add     '添加一个新的BOOK
            mySheet = myBook.Worksheets.Add     '添加一个新的SHEET
            'myExcel.Caption = ExcelTitle
            mySheet.Name = "车底交路图"
            'mySheet.Cells.Select()
            mySheet.Cells.Font.Size = 9
            mySheet.Cells.ColumnWidth = 6


            'mySheet.Cells.RowHeight = 9
            Dim DataArray(0, 0) As String
            Dim nTrainID As Integer
            Dim nTrain As Integer
            Dim nWidthMove As Integer
            Dim nHeightMove As Integer
            nWidthMove = 0
            nHeightMove = 0
            Dim nMaxHeight As Integer
            Dim nTolHeight As Integer
            Dim nForHeight As Integer '记录前一图的高度
            Dim nNowHeight As Integer '当前图的高度
            Dim nCurHeightMove As Integer '计算高度
            Dim nColsMove As Integer '模向打印了多少行
            nMaxHeight = 0
            nTolHeight = 0
            nForHeight = 0
            nNowHeight = 0
            nColsMove = 0
            Me.proBar.Visible = True
            Me.proBar.Maximum = 100
            Me.proBar.Value = 0
            For i = 1 To UBound(OutPutNum)
                For j = 1 To UBound(ChediInfo)
                    If ChediInfo(j).sCheCiHao = OutPutNum(i) Then
                        Rows = (UBound(ChediInfo(j).nLinkTrain) + 1) * 2
                        If Rows > nMaxHeight Then
                            nMaxHeight = Rows
                        End If
                        Cols = 4
                        ReDim DataArray(Rows - 1, Cols - 1)

                        nNowHeight = Rows
                        If i > 1 Then
                            If nForHeight + nNowHeight + 2 > nMaxHeight Then '判断在一列中能否放两个图
                                nForHeight = nNowHeight
                                nHeightMove = nCurHeightMove
                                If nColsMove > 0 Then
                                    If nColsMove Mod nRowNum = 0 Then
                                        nTolHeight = nTolHeight + nMaxHeight + 3
                                        nCurHeightMove = nTolHeight
                                        nWidthMove = 0
                                        'nMaxHeight = 0
                                        nColsMove = 1
                                        'nCurHeightMove = nCurHeightMove + nMaxHeight
                                        nHeightMove = nCurHeightMove
                                    Else
                                        nColsMove = nColsMove + 1
                                    End If
                                Else
                                    nColsMove = nColsMove + 1
                                End If
                            Else
                                If nWidthMove >= 5 Then
                                    nWidthMove = nWidthMove - 5
                                    nHeightMove = nCurHeightMove + nForHeight + 2
                                    nForHeight = nForHeight + nNowHeight + 2
                                End If
                            End If

                        Else
                            nForHeight = nNowHeight
                            nHeightMove = nCurHeightMove
                            nColsMove = nColsMove + 1
                        End If

                        If Rows = 4 Then
                            DataArray(0, 1) = ChediInfo(j).sCheCiHao
                            nTrainID = 1
                            nTrain = ChediInfo(j).nLinkTrain(nTrainID)
                            DataArray(1, 1) = GetPrintStaName(TrainInf(nTrain).ComeStation) & SecondToHour(GetTrainStartTime(nTrain, TrainInf(nTrain).ComeStation), 0)

                            nTrainID = UBound(ChediInfo(j).nLinkTrain)
                            nTrain = ChediInfo(j).nLinkTrain(nTrainID)
                            DataArray(3, 3) = GetPrintStaName(TrainInf(nTrain).NextStation) & SecondToHour(GetTrainArriTime(nTrain, TrainInf(nTrain).NextStation), 0)
                            mySheet.Cells(3 + nHeightMove, 3 + nWidthMove).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown).LineStyle = 1
                        Else
                            For p = 0 To Cols - 1
                                For q = 0 To Rows - 1
                                    If p = 0 Then
                                        If q > 1 And q < Rows - 2 Then
                                            nTrainID = Int((q - 1) / 2) + 1
                                            nTrain = ChediInfo(j).nLinkTrain(nTrainID)

                                            If q Mod 4 = 0 Then
                                                DataArray(q, p) = GetPrintStaName(TrainInf(nTrain).NextStation) & SecondToHour(GetTrainArriTime(nTrain, TrainInf(nTrain).NextStation), 0)
                                            ElseIf (q - 1) Mod 4 = 0 Then
                                                DataArray(q, p) = GetPrintStaName(TrainInf(nTrain).ComeStation) & SecondToHour(GetTrainStartTime(nTrain, TrainInf(nTrain).ComeStation), 0)
                                            End If
                                        End If
                                    ElseIf p = 1 Then
                                        If q = 0 Then
                                            DataArray(q, p) = ChediInfo(j).sCheCiHao
                                        End If
                                        If q = 1 Then
                                            nTrainID = 1
                                            nTrain = ChediInfo(j).nLinkTrain(nTrainID)
                                            DataArray(q, p) = GetPrintStaName(TrainInf(nTrain).ComeStation) & SecondToHour(GetTrainStartTime(nTrain, TrainInf(nTrain).ComeStation), 0)
                                        End If

                                        If q = Rows - 2 Then
                                            nTrainID = UBound(ChediInfo(j).nLinkTrain)
                                            nTrain = ChediInfo(j).nLinkTrain(nTrainID)
                                            DataArray(q, p) = GetPrintStaName(TrainInf(nTrain).NextStation) & SecondToHour(GetTrainArriTime(nTrain, TrainInf(nTrain).NextStation), 0)
                                        End If
                                        '
                                        If q > 1 And q < Rows - 2 Then
                                            If q Mod 4 = 0 Then
                                                mySheet.Cells(q + 1 + nHeightMove, p + 1 + nWidthMove).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalUp).LineStyle = 1
                                            ElseIf (q - 1) Mod 4 = 0 Then
                                                mySheet.Cells(q + 1 + nHeightMove, p + 1 + nWidthMove).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown).LineStyle = 1
                                            End If
                                        End If

                                    ElseIf p = 2 Then
                                        If q > 1 And q < Rows - 2 Then
                                            nTrainID = Int((q - 1) / 2) + 1
                                            nTrain = ChediInfo(j).nLinkTrain(nTrainID)
                                            If (q - 2) Mod 4 = 0 Then
                                                mySheet.Cells(q + 1 + nHeightMove, p + 1 + nWidthMove).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown).LineStyle = 1
                                            ElseIf (q - 3) Mod 4 = 0 Then
                                                mySheet.Cells(q + 1 + nHeightMove, p + 1 + nWidthMove).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalUp).LineStyle = 1
                                            End If
                                        End If


                                    ElseIf p = 3 Then
                                        If q > 1 And q < Rows - 2 Then
                                            nTrainID = Int((q - 1) / 2) + 1
                                            nTrain = ChediInfo(j).nLinkTrain(nTrainID)
                                            If (q - 2) Mod 4 = 0 Then
                                                DataArray(q, p) = GetPrintStaName(TrainInf(nTrain).NextStation) & SecondToHour(GetTrainArriTime(nTrain, TrainInf(nTrain).NextStation), 0)
                                            ElseIf (q - 3) Mod 4 = 0 Then
                                                DataArray(q, p) = GetPrintStaName(TrainInf(nTrain).ComeStation) & SecondToHour(GetTrainStartTime(nTrain, TrainInf(nTrain).ComeStation), 0)
                                            End If
                                        End If
                                    End If
                                Next q
                            Next p
                        End If
                    End If
                Next j


                mySheet.Cells(nHeightMove + 1, nWidthMove + 1).Resize(Rows, Cols).Value = DataArray
                mySheet.Columns(nWidthMove + 1).EntireColumn.AutoFit()
                mySheet.Columns(nWidthMove + 4).EntireColumn.AutoFit()

                nWidthMove = nWidthMove + 5

                Me.proBar.Value = Int(100 * i / UBound(OutPutNum))
            Next i
            Me.proBar.Value = 0
            Me.proBar.Visible = False
            'For p = 1 To rows
            '    For q = 1 To rows
            '        If Val(DataArray(p, q)) > 0 Then
            '            myExcel.Cells(p + 1, q + 1).Interior.ColorIndex = Val(DataArray(p, q)) + 1
            '        End If
            '    Next
            'Next
            'For p = 1 To Cols
            '    mySheet.Columns(p).EntireColumn.AutoFit()
            'Next p
            'For p = 2 To cols
            '    myExcel.Columns(p).ColumnWidth = 3
            'Next p
            myExcel.Visible = True
            GC.Collect()
        Catch exp As Exception
            MessageBox.Show("数据导出失败!请查看是否已经安装了Microsoft Excel 2003 及以上版本", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    '得到打印的车站名
    Private Function GetPrintStaName(ByVal sSta As String) As String
        GetPrintStaName = ""
        Dim i As Integer
        For i = 1 To Me.lstSta.Items.Count
            If Me.lstSta.Items(i - 1) = sSta Then
                GetPrintStaName = Me.lstPrintSta.Items(i - 1)
                Exit For
            End If
        Next
    End Function

    '车底按发点排序
    Private Sub SetCheDiSeqExcel(ByVal CheDiSeq() As Integer)
        Dim j, k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long

        '按到达时间排序
        Flag = 1
        k = UBound(CheDiSeq)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                TempTime1 = TrainInf(ChediInfo(CheDiSeq(j)).nLinkTrain(1)).nStartStaTime
                Temptime2 = TrainInf(ChediInfo(CheDiSeq(j + 1)).nLinkTrain(1)).nStartStaTime
                If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                    temp = CheDiSeq(j)
                    CheDiSeq(j) = CheDiSeq(j + 1)
                    CheDiSeq(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop

    End Sub
End Class