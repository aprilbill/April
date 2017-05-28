
Public Class frmPrintCheDiJiaoLu

    Private Sub frmPrintCheDiJiaoLu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim CheDiSeq() As Integer
        Dim i As Integer
        ReDim CheDiSeq(0)
        Me.chkLstCheDiNum.Items.Clear()
        Me.lstPrintSta.Items.Clear()
        Me.lstSta.Items.Clear()
        For i = 1 To UBound(CSchediInfo)
            If CSchediInfo(i).sCheCiHao.Trim <> "" And UBound(CSchediInfo(i).nLinkTrain) > 0 Then
                ReDim Preserve CheDiSeq(UBound(CheDiSeq) + 1)
                CheDiSeq(UBound(CheDiSeq)) = i
            End If
        Next

        Dim j, k, temp, Flag As Integer
        Dim ChediHao1, ChediHao2 As Long
        Dim nTrain As Integer
        '按车底编号排序
        Flag = 1
        k = UBound(CheDiSeq)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                ChediHao1 = CSchediInfo(CheDiSeq(j)).sCheCiHao
                ChediHao2 = CSchediInfo(CheDiSeq(j + 1)).sCheCiHao
                If ChediHao1 > ChediHao2 Then
                    temp = CheDiSeq(j)
                    CheDiSeq(j) = CheDiSeq(j + 1)
                    CheDiSeq(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop

        '对车底按发点排序

        Call SetCheDiSeqExcel(CheDiSeq)
        For i = 1 To UBound(CheDiSeq)
            Me.chkLstCheDiNum.Items.Add(CSchediInfo(CheDiSeq(i)).sCheCiHao)
            Me.chkLstCheDiNum.SetItemChecked(i - 1, True)
        Next
        Call chkLstCheDiNum_SelectedIndexChanged(Nothing, Nothing)

        '将可能输出的车站简化名称
        Dim sFirSta As String
        Dim sEndSta As String
        Dim nState As Integer
        nState = 0
        For i = 1 To UBound(CSchediInfo)
            For j = 1 To UBound(CSchediInfo(i).nLinkTrain)
                nState = 0
                nTrain = CSchediInfo(i).nLinkTrain(j)
                sFirSta = GetPrintStaNameFromStaName(CSTrainInf(nTrain).StartStation)
                sEndSta = GetPrintStaNameFromStaName(CSTrainInf(nTrain).EndStation)
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
            mySheet.Name = "车底交路图"
            mySheet.Cells.Font.Size = 9
            mySheet.Cells.ColumnWidth = 6

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
                For j = 1 To UBound(CSchediInfo)
                    If CSchediInfo(j).sCheCiHao = OutPutNum(i) Then
                        Rows = (UBound(CSchediInfo(j).nLinkTrain) + 1) * 2
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
                                        nColsMove = 1
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
                            DataArray(0, 1) = CSchediInfo(j).sCheCiHao
                            nTrainID = 1
                            nTrain = CSchediInfo(j).nLinkTrain(nTrainID)
                            DataArray(1, 1) = GetPrintStaName(CSTrainInf(nTrain).StartStation) & SecondToHour(GetCSTrainStartTime(nTrain, CSTrainInf(nTrain).StartStation), 0)

                            nTrainID = UBound(CSchediInfo(j).nLinkTrain)
                            nTrain = CSchediInfo(j).nLinkTrain(nTrainID)
                            DataArray(3, 3) = GetPrintStaName(CSTrainInf(nTrain).EndStation) & SecondToHour(GetCSTrainArriTime(nTrain, CSTrainInf(nTrain).EndStation), 0)
                            mySheet.Cells(3 + nHeightMove, 3 + nWidthMove).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown).LineStyle = 1
                        Else
                            For p = 0 To Cols - 1
                                For q = 0 To Rows - 1
                                    If p = 0 Then
                                        If q > 1 And q < Rows - 2 Then
                                            nTrainID = Int((q - 1) / 2) + 1
                                            nTrain = CSchediInfo(j).nLinkTrain(nTrainID)

                                            If q Mod 4 = 0 Then
                                                DataArray(q, p) = GetPrintStaName(CSTrainInf(nTrain).EndStation) & SecondToHour(GetCSTrainArriTime(nTrain, CSTrainInf(nTrain).EndStation), 0)
                                                DataArray(q - 1, p + 2) = CSTrainInf(nTrain).sPrintTrain
                                            ElseIf (q - 1) Mod 4 = 0 Then
                                                DataArray(q, p) = GetPrintStaName(CSTrainInf(nTrain).StartStation) & SecondToHour(GetCSTrainStartTime(nTrain, CSTrainInf(nTrain).StartStation), 0)
                                                DataArray(q, p + 1) = CSTrainInf(nTrain).sPrintTrain
                                            End If
                                        End If
                                    ElseIf p = 1 Then
                                        If q = 0 Then
                                            DataArray(q, p) = CSchediInfo(j).sCheCiHao
                                        End If
                                        If q = 1 Then
                                            nTrainID = 1
                                            nTrain = CSchediInfo(j).nLinkTrain(nTrainID)
                                            DataArray(q, p) = GetPrintStaName(CSTrainInf(nTrain).StartStation) & SecondToHour(GetCSTrainStartTime(nTrain, CSTrainInf(nTrain).StartStation), 0)
                                            DataArray(q + 1, p + 1) = CSTrainInf(nTrain).sPrintTrain
                                        End If

                                        If q = Rows - 2 Then
                                            nTrainID = UBound(CSchediInfo(j).nLinkTrain)
                                            nTrain = CSchediInfo(j).nLinkTrain(nTrainID)
                                            DataArray(q, p) = GetPrintStaName(CSTrainInf(nTrain).EndStation) & SecondToHour(GetCSTrainArriTime(nTrain, CSTrainInf(nTrain).EndStation), 0)
                                            DataArray(q - 1, p + 1) = CSTrainInf(nTrain).sPrintTrain
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
                                            nTrain = CSchediInfo(j).nLinkTrain(nTrainID)
                                            If (q - 2) Mod 4 = 0 Then
                                                mySheet.Cells(q + 1 + nHeightMove, p + 1 + nWidthMove).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalDown).LineStyle = 1
                                            ElseIf (q - 3) Mod 4 = 0 Then
                                                mySheet.Cells(q + 1 + nHeightMove, p + 1 + nWidthMove).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlDiagonalUp).LineStyle = 1
                                            End If
                                        End If


                                    ElseIf p = 3 Then
                                        If q > 1 And q < Rows - 2 Then
                                            nTrainID = Int((q - 1) / 2) + 1
                                            nTrain = CSchediInfo(j).nLinkTrain(nTrainID)
                                            If (q - 2) Mod 4 = 0 Then
                                                DataArray(q, p) = GetPrintStaName(CSTrainInf(nTrain).EndStation) & SecondToHour(GetCSTrainArriTime(nTrain, CSTrainInf(nTrain).EndStation), 0)
                                            ElseIf (q - 3) Mod 4 = 0 Then
                                                DataArray(q, p) = GetPrintStaName(CSTrainInf(nTrain).StartStation) & SecondToHour(GetCSTrainStartTime(nTrain, CSTrainInf(nTrain).StartStation), 0)
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
            If Me.lstSta.Items(i - 1) = GetPrintStaNameFromStaName(sSta) Then
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
                TempTime1 = CSTrainInf(CSchediInfo(CheDiSeq(j)).nLinkTrain(1)).nStartStaTime
                Temptime2 = CSTrainInf(CSchediInfo(CheDiSeq(j + 1)).nLinkTrain(1)).nStartStaTime
                If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                    temp = CheDiSeq(j)
                    CheDiSeq(j) = CheDiSeq(j + 1)
                    CheDiSeq(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop

    End Sub

    Public Function GetPrintStaNameFromStaName(ByVal sSta As String) As String
        GetPrintStaNameFromStaName = sSta
        Dim i As Integer
        For i = 1 To UBound(StationInf)
            If StationInf(i).sStationName = sSta Then
                GetPrintStaNameFromStaName = StationInf(i).sPrintStaName
                Exit For
            End If
        Next
    End Function

    Public Function GetCSTrainArriTime(ByVal nTrain As Int16, ByVal sStaName As String) As Integer
        Dim i As Int16
        For i = 1 To UBound(CSTrainInf(nTrain).nPathID)
            If StationInf(CSTrainInf(nTrain).nPathID(i)).sStationName = sStaName Then
                GetCSTrainArriTime = CSTrainInf(nTrain).Arrival(CSTrainInf(nTrain).nPathID(i))
                Exit For
            End If
        Next
    End Function

    Public Function GetCSTrainStartTime(ByVal nTrain As Int16, ByVal sStaName As String) As Integer
        Dim i As Int16
        For i = 1 To UBound(CSTrainInf(nTrain).nPathID)
            If StationInf(CSTrainInf(nTrain).nPathID(i)).sStationName = sStaName Then
                GetCSTrainStartTime = CSTrainInf(nTrain).Starting(CSTrainInf(nTrain).nPathID(i))
                Exit For
            End If
        Next
    End Function

End Class