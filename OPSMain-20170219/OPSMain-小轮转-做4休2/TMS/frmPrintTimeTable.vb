Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop.Excel.XlBordersIndex
Imports Microsoft.Office.Interop.Excel.xlColumnDataType
Imports Microsoft.Office.Interop.Excel.XlBorderWeight
Imports Microsoft.Office.Interop.Excel.XlUnderlineStyle
Imports Microsoft.Office.Interop.Excel.XlCommentDisplayMode
Imports Microsoft.Office.Interop.Excel.XlLineStyle
Imports Microsoft.Office.Interop.Excel.Constants
Imports Microsoft.Office.Interop.Excel.XlPaperSize

Public Class frmPrintTimeTable
    Structure typeOutPrintTrainByStation
        Dim nTrain As Integer
        Dim sArriTime As Long
        Dim sStartTime As Long
    End Structure
    Dim OutPrintTrainBySta() As typeOutPrintTrainByStation '按车站输出的列车序列，已按好序

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmPrintTimeTable_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim i As Integer
        Dim j As Integer
        Me.cmbPaiXu.Items.Clear()
        Me.cmbPaiXu.Items.Add("始发站")
        Me.lstBei.Items.Clear()
        Me.lstSta.Items.Clear()
        For i = 1 To UBound(NotSameStationInf)
            Me.cmbPaiXu.Items.Add(NotSameStationInf(i))
            Me.lstBei.Items.Add(NotSameStationInf(i))
        Next i
        Me.txtTitle.Text = NetInf.sNetName & TimeTablePara.sPubCurSkbName
        Me.txtTime.Text = DateTime.Now.ToString   'Year(Now) & "年" & Month(Now) & "月" & Day(Now) & "日"

        Dim CheDiSeq() As Integer
        'ReDim CheDiSeq(UBound(ChediInfo))
        ReDim CheDiSeq(0)
        For i = 1 To UBound(ChediInfo)
            If UBound(ChediInfo(i).nLinkTrain) > 0 And ChediInfo(i).sCheCiHao.Trim <> "" Then
                ReDim Preserve CheDiSeq(UBound(CheDiSeq) + 1)
                CheDiSeq(UBound(CheDiSeq)) = i
            End If
        Next

        Dim k, temp, Flag As Integer
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
        Me.lstCheDi.Items.Clear()
        For i = 1 To UBound(CheDiSeq)
            Me.lstCheDi.Items.Add(ChediInfo(CheDiSeq(i)).sCheCiHao)
        Next
        For i = 1 To Me.lstCheDi.Items.Count
            Me.lstCheDi.SetItemChecked(i - 1, True)
        Next i

        Me.cmbQuDuanName.Items.Clear()
        For i = 1 To UBound(SkbStnSeq)
            Me.cmbQuDuanName.Items.Add(SkbStnSeq(i).sQDName)
        Next i

        If Me.cmbQuDuanName.Items.Count > 0 Then
            Me.cmbQuDuanName.Text = Me.cmbQuDuanName.Items(0)
        Else
            MsgBox("时刻表车站顺序信息没有添加，请先添加：[文件]->[编辑时刻表车站顺序]")
        End If
        Me.cmbPaiXu.Text = "始发站"
        'Me.checkListsta.Items.Clear()
        'For i = 1 To UBound(NotSameStationInf)
        '    Me.checkListsta.Items.Add(NotSameStationInf(i))
        'Next

        If GetUserName() = "北京地铁" Then
            Me.llbATS.Visible = False
            'Me.grpLine2.Visible = False
            Me.btnOutCheDiLine2.Visible = False
        End If
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

    Private Sub cmdAddOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddOne.Click
        Dim nSelectID As Integer
        nSelectID = Me.lstSta.SelectedIndex
        If Me.lstBei.SelectedIndex >= 0 Then
            Me.lstSta.Items.Insert(Me.lstSta.SelectedIndex + 1, Me.lstBei.SelectedItem)
            Me.lstSta.SelectedIndex = nSelectID + 1
            'Call AddItems(Me.lstBei.SelectedItem)
            If Me.lstBei.SelectedIndex <= Me.lstBei.Items.Count - 2 Then
                Me.lstBei.SelectedItem = Me.lstBei.Items(Me.lstBei.SelectedIndex + 1)
            End If
        End If
    End Sub

    Private Sub cmdAddAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddAll.Click
        Dim i As Integer
        If Me.lstBei.Items.Count > 0 Then
            For i = 1 To Me.lstBei.Items.Count
                Me.lstSta.Items.Add(Me.lstBei.Items(i - 1))
            Next
        End If
    End Sub

    Private Sub cmdDeleOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleOne.Click
        Dim nCurID As Integer
        nCurID = Me.lstSta.SelectedIndex
        If nCurID >= 0 Then
            Me.lstSta.Items.RemoveAt(Me.lstSta.SelectedIndex)
            If Me.lstSta.Items.Count > 0 Then
                If nCurID <= Me.lstSta.Items.Count - 1 Then
                    Me.lstSta.SelectedIndex = nCurID
                Else
                    Me.lstSta.SelectedIndex = nCurID - 1
                End If
            End If
        End If
    End Sub

    Private Sub cmdDeleAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleAll.Click
        Me.lstSta.Items.Clear()
    End Sub

    Private Sub lstBei_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBei.DoubleClick
        Call cmdAddOne_Click(Nothing, Nothing)
    End Sub

    Private Sub lstSta_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSta.DoubleClick
        Call cmdDeleOne_Click(Nothing, Nothing)
    End Sub

    Private Sub btnOutPutCheCiSKB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim nPageNum As Integer
        nPageNum = Val(Me.txtCols.Text)

        If nPageNum >= 1 And nPageNum <= 250 Then

        Else
            MsgBox("每个工作表输出列数设置错误，请重输！")
            Exit Sub
        End If

        'Dim intIdx As Integer
        Dim i As Integer, j As Integer
        Dim iRows As Integer
        'Dim iColumns As Integer
        Dim nTrain As Integer
        Dim objExcel As Microsoft.Office.Interop.Excel.Application
        Dim myWorkbook As Microsoft.Office.Interop.Excel.Workbook, mySheets As Microsoft.Office.Interop.Excel.Sheets
        Dim nCurSTnSeqID As Integer
        Dim iColUp As Integer
        Dim iColDown As Integer
        objExcel = Nothing

        For i = 1 To UBound(SkbStnSeq)
            If SkbStnSeq(i).sQDName = Me.cmbQuDuanName.Text Then
                nCurSTnSeqID = 1
                Exit For
            End If
        Next i


        iColUp = 2 '两列表头
        iColDown = 2
        Me.proBar.Maximum = 1000
        proBar.Value = 0
        proBar.Visible = True

        Try
            objExcel = GetObject("", "Excel.Application")
        Catch ex As Exception
            MsgBox("不能运行Excel，请检查是否安装了 Microsoft Excel 2003!", , "提示")
            Exit Sub
        End Try

        Me.Cursor = Cursors.WaitCursor
        ' objExcel.WindowState = Excel.xlMaximized '调试用最大化
        myWorkbook = objExcel.Workbooks.Add
        mySheets = myWorkbook.Sheets

        iRows = UBound(SkbStnSeq(nCurSTnSeqID).nStnSeq) '车站数


        '按列车到站点排序
        Dim ntmpTrain() As Integer

        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long
        ReDim ntmpTrain(0)
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                ReDim Preserve ntmpTrain(UBound(ntmpTrain) + 1)
                ntmpTrain(UBound(ntmpTrain)) = i
            End If
        Next i
        '按到达时间排序
        Flag = 1
        k = UBound(ntmpTrain)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                If Me.cmbPaiXu.Text = "始发站" Then
                    TempTime1 = AddLitterTime(TrainInf(ntmpTrain(j)).Starting(TrainInf(ntmpTrain(j)).nPathID(1)))
                    Temptime2 = AddLitterTime(TrainInf(ntmpTrain(j + 1)).Starting(TrainInf(ntmpTrain(j + 1)).nPathID(1)))
                Else
                    TempTime1 = AddLitterTime(GetStartTimeFromStaName(Me.cmbPaiXu.Text, ntmpTrain(j)))
                    Temptime2 = AddLitterTime(GetStartTimeFromStaName(Me.cmbPaiXu.Text, ntmpTrain(j + 1)))
                End If
                If TempTime1 > Temptime2 Then '
                    temp = ntmpTrain(j)
                    ntmpTrain(j) = ntmpTrain(j + 1)
                    ntmpTrain(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop


        '表格格式设置
        '出库、入库
        mySheets("Sheet1").Name = "出库"
        mySheets("出库").Activate()
        mySheets("出库").Cells(1, 1).Value = "站名\车次"
        mySheets("出库").Columns("A:A").ColumnWidth = 10
        mySheets("出库").Columns("B:B").ColumnWidth = 6

        mySheets("Sheet2").Name = "入库"
        mySheets("入库").Activate()
        mySheets("入库").Cells(1, 1).Value = "站名\车次"
        mySheets("入库").Columns("A:A").ColumnWidth = 10
        mySheets("入库").Columns("B:B").ColumnWidth = 6

        With mySheets("出库").Cells
            .Font.size = 11
            .HorizontalAlignment = xlCenter
            .VerticalAlignment = xlCenter
            .NumberFormatLocal = "@"
            For i = 1 To iRows
                mySheets("出库").Cells(2 * i, 1).Value = StationInf(SkbStnSeq(nCurSTnSeqID).nStnSeq(i)).sStationName
                mySheets("出库").Range("A" & Trim(Str(2 * i)) & ":" & "A" & Trim(Str(2 * i + 1))).Merge()
                mySheets("出库").Range("B" & Trim(Str(2 * i)) & ":" & "B" & Trim(Str(2 * i + 1))).Merge()
                mySheets("出库").Cells(2 * i, 2).Value = "到\发"
            Next i
        End With

        With mySheets("入库").Cells
            .Font.size = 11
            .HorizontalAlignment = xlCenter
            .VerticalAlignment = xlCenter
            .NumberFormatLocal = "@"
            For i = 1 To iRows
                mySheets("入库").Cells(2 * i, 1).Value = StationInf(SkbStnSeq(nCurSTnSeqID).nStnSeq(i)).sStationName
                mySheets("入库").Range("A" & Trim(Str(2 * i)) & ":" & "A" & Trim(Str(2 * i + 1))).Merge()
                mySheets("入库").Range("B" & Trim(Str(2 * i)) & ":" & "B" & Trim(Str(2 * i + 1))).Merge()
                mySheets("入库").Cells(2 * i, 2).Value = "到\发"
            Next i
        End With

        Dim nDownNum As Integer
        Dim nUpNum As Integer
        Dim nDownPage As Integer
        Dim nUpPage As Integer

        nUpNum = 0
        nDownNum = 0
        Dim sCurDownName As String
        Dim sCurUpName As String
        Dim tmpK As Integer

        For i = 1 To UBound(TrainInf)
            If TrainInf(i).TrainStyle = "运行车" And TrainInf(i).Train <> "" And (i Mod 2 = 0) Then
                nUpNum = nUpNum + 1
            End If
        Next i

        For i = 1 To UBound(TrainInf)
            If TrainInf(i).TrainStyle = "运行车" And TrainInf(i).Train <> "" And (i Mod 2 <> 0) Then
                nDownNum = nDownNum + 1
            End If
        Next i


        nUpPage = GetMaxZhenShu(nUpNum / nPageNum)
        nDownPage = GetMaxZhenShu(nDownNum / nPageNum)

        For i = 1 To nDownPage
            mySheets.Add()
            sCurDownName = "下行" & i
            mySheets("Sheet" & i + 3).Name = sCurDownName
            mySheets(sCurDownName).Activate()
            mySheets(sCurDownName).Cells(1, 1).Value = "下行"
            mySheets(sCurDownName).Columns("A:A").ColumnWidth = 10
            mySheets(sCurDownName).Columns("B:B").ColumnWidth = 3
            With mySheets(sCurDownName).Cells
                .Font.size = 11
                .HorizontalAlignment = xlCenter
                .VerticalAlignment = xlCenter
                .NumberFormatLocal = "@"
                For j = 1 To iRows
                    mySheets(sCurDownName).Cells(2 * j, 1).Value = StationInf(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)).sStationName
                    mySheets(sCurDownName).Range("A" & Trim(Str(2 * j)) & ":" & "A" & Trim(Str(2 * j + 1))).Merge()
                    mySheets(sCurDownName).Cells(2 * j, 2).Value = "到"
                    mySheets(sCurDownName).Cells(2 * j + 1, 2).Value = "发"
                Next j
            End With
        Next i

        For i = 1 To nUpPage
            mySheets.Add()
            sCurUpName = "上行" & i
            mySheets("Sheet" & i + 3 + nDownPage).Name = sCurUpName
            mySheets(sCurUpName).Activate()
            mySheets(sCurUpName).Cells(1, 1).Value = "上行"
            mySheets(sCurUpName).Columns("A:A").ColumnWidth = 10
            mySheets(sCurUpName).Columns("B:B").ColumnWidth = 3
            With mySheets(sCurUpName).Cells
                .Font.size = 11
                .HorizontalAlignment = xlCenter
                .VerticalAlignment = xlCenter
                .NumberFormatLocal = "@"
                For j = 1 To iRows
                    mySheets(sCurUpName).Cells(2 * j, 1).Value = StationInf(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)).sStationName
                    mySheets(sCurUpName).Range("A" & Trim(Str(2 * j)) & ":" & "A" & Trim(Str(2 * j + 1))).Merge()
                    mySheets(sCurUpName).Cells(2 * j, 2).Value = "发"
                    mySheets(sCurUpName).Cells(2 * j + 1, 2).Value = "到"
                Next j
            End With
        Next i



        Me.proBar.Value = 100
        Dim nCurCol As Integer
        nCurCol = 2
        For i = 1 To UBound(ntmpTrain) '先输出出库车
            nTrain = ntmpTrain(i)
            If TrainInf(nTrain).TrainStyle = "出库车" Then
                nCurCol = nCurCol + 1
                mySheets("出库").Cells(1, nCurCol).Value = TrainInf(nTrain).sPrintTrain
                For j = 1 To iRows
                    mySheets("出库").Cells(j * 2, nCurCol).Value = dTime(TrainInf(nTrain).Arrival(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                    mySheets("出库").Cells(j * 2 + 1, nCurCol).Value = dTime(TrainInf(nTrain).Starting(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                    mySheets("出库").Cells(j * 2, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                    mySheets("出库").Cells(j * 2 + 1, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                Next j
            End If
        Next i

        Me.proBar.Value = 150
        nCurCol = 2
        For i = 1 To UBound(ntmpTrain)  '输出入库车
            nTrain = ntmpTrain(i)
            If TrainInf(nTrain).TrainStyle = "入库车" Then
                nCurCol = nCurCol + 1
                mySheets("入库").Cells(1, nCurCol).Value = TrainInf(nTrain).sPrintTrain
                For j = 1 To iRows
                    mySheets("入库").Cells(j * 2, nCurCol).Value = dTime(TrainInf(nTrain).Arrival(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                    mySheets("入库").Cells(j * 2 + 1, nCurCol).Value = dTime(TrainInf(nTrain).Starting(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                    mySheets("入库").Cells(j * 2, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                    mySheets("入库").Cells(j * 2 + 1, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                Next j
            End If
        Next i

        nCurCol = 2
        tmpK = 0

        Me.proBar.Value = 200
        For i = 1 To UBound(ntmpTrain)  '输出下行车
            nTrain = ntmpTrain(i)
            If TrainInf(nTrain).TrainStyle = "运行车" And nTrain Mod 2 <> 0 Then
                tmpK = tmpK + 1

                nCurCol = 3 + ((tmpK - 1) Mod nPageNum)

                sCurDownName = "下行" & GetMaxZhenShu(tmpK / nPageNum)
                mySheets(sCurDownName).Cells(1, nCurCol).Value = TrainInf(nTrain).sPrintTrain
                For j = 1 To iRows
                    mySheets(sCurDownName).Cells(j * 2, nCurCol).Value = dTime(TrainInf(nTrain).Arrival(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                    mySheets(sCurDownName).Cells(j * 2 + 1, nCurCol).Value = dTime(TrainInf(nTrain).Starting(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                    mySheets(sCurDownName).Cells(j * 2 + 1, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                    mySheets(sCurDownName).Cells(j * 2, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                Next j
                Me.proBar.Value = 200 + Int(400.0# * tmpK / nDownNum)
            End If

        Next i

        nCurCol = 2
        tmpK = 0
        For i = 1 To UBound(ntmpTrain)  '输出上行车
            nTrain = ntmpTrain(i)
            If TrainInf(nTrain).TrainStyle = "运行车" And nTrain Mod 2 = 0 Then
                tmpK = tmpK + 1

                nCurCol = 3 + ((tmpK - 1) Mod nPageNum)

                sCurUpName = "上行" & GetMaxZhenShu(tmpK / nPageNum)
                mySheets(sCurUpName).Cells(1, nCurCol).Value = TrainInf(nTrain).sPrintTrain
                For j = 1 To iRows
                    mySheets(sCurUpName).Cells(j * 2, nCurCol).Value = dTime(TrainInf(nTrain).Starting(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                    mySheets(sCurUpName).Cells(j * 2 + 1, nCurCol).Value = dTime(TrainInf(nTrain).Arrival(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                    mySheets(sCurUpName).Cells(j * 2, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                    mySheets(sCurUpName).Cells(j * 2 + 1, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                Next j
                Me.proBar.Value = 600 + Int(400.0# * tmpK / nUpNum)
            End If
        Next i


        '页面设置
        'For i = 1 To mySheets.Count
        '    mySheets(1).Activate()
        '    With mySheets(i).PageSetup
        '        .PrintTitleRows = ""
        '        .PrintTitleColumns = "A:B"
        '        .PrintArea = ""
        '        .LeftHeader = ""
        '        .CenterHeader = NetInf.sNetName & "运行图时刻表"
        '        .RightHeader = ""
        '        .LeftFooter = ""
        '        .CenterFooter = "第&P页 共&N页"
        '        .RightFooter = SystemPara.sUserCompanyName
        '        .PrintHeadings = False
        '        .PrintGridlines = True
        '        '.Orientation = xlLandscape
        '        .Draft = False
        '        .PaperSize = xlPaperA4
        '        .FirstPageNumber = xlAutomatic
        '        .Zoom = 80
        '   End With
        'Next i
        Me.proBar.Visible = False
        objExcel.Visible = True
        objExcel = Nothing
        myWorkbook = Nothing
        mySheets = Nothing
        Me.Cursor = Cursors.Default
    End Sub

    '车站时刻表输出，排序
    Private Sub StaOutPrintSKB(ByVal sStaName As String)
        ReDim OutPrintTrainBySta(0)
        Dim i As Integer
        Dim j As Integer
        Dim ntmpTrain() As Integer
        ReDim ntmpTrain(0)
        Dim nSta As Integer
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                ' If i = 495 Then Stop
                For j = 1 To UBound(TrainInf(i).nPathID)
                    nSta = TrainInf(i).nPathID(j)
                    If StationInf(nSta).sStationName = sStaName Then
                        If TrainInf(i).Arrival(nSta) <> -1 And TrainInf(i).Starting(nSta) <> -1 Then
                            ReDim Preserve OutPrintTrainBySta(UBound(OutPrintTrainBySta) + 1)
                            OutPrintTrainBySta(UBound(OutPrintTrainBySta)).nTrain = i
                            OutPrintTrainBySta(UBound(OutPrintTrainBySta)).sArriTime = AddLitterTime(TrainInf(i).Arrival(nSta))
                            OutPrintTrainBySta(UBound(OutPrintTrainBySta)).sStartTime = AddLitterTime(TrainInf(i).Starting(nSta))
                            Exit For
                        End If
                    End If
                Next j
            End If
        Next i

        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long
        Dim sTime1 As Long
        Dim sTime2 As Long

        '按到达时间排序
        Flag = 1
        k = UBound(OutPrintTrainBySta)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                TempTime1 = OutPrintTrainBySta(j).sArriTime
                Temptime2 = OutPrintTrainBySta(j + 1).sArriTime
                If TempTime1 > Temptime2 Then
                    temp = OutPrintTrainBySta(j).nTrain
                    sTime1 = OutPrintTrainBySta(j).sArriTime
                    sTime2 = OutPrintTrainBySta(j).sStartTime
                    OutPrintTrainBySta(j).nTrain = OutPrintTrainBySta(j + 1).nTrain
                    OutPrintTrainBySta(j).sArriTime = OutPrintTrainBySta(j + 1).sArriTime
                    OutPrintTrainBySta(j).sStartTime = OutPrintTrainBySta(j + 1).sStartTime
                    OutPrintTrainBySta(j + 1).nTrain = temp
                    OutPrintTrainBySta(j + 1).sArriTime = sTime1
                    OutPrintTrainBySta(j + 1).sStartTime = sTime2
                    Flag = 1
                End If
            Next j
        Loop

    End Sub

    Private Sub btnOutCheDiLine2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutCheDiLine2.Click
        Try
            Dim i, j, p, k, q As Integer
            Dim rows As Integer
            Dim cols As Integer
            Dim nOutNum As Integer
            nOutNum = Val(Me.txtCheDiSheetCol.Text)
            If nOutNum >= 1 And nOutNum <= 25 Then
            Else
                MsgBox("每个SHEET表可输出的个数不能超过25！，也不能为零！")
                Exit Sub
            End If

            Dim myExcel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application
            'myExcel.Caption = ExcelTitle
            Dim myBook As Microsoft.Office.Interop.Excel.Workbook
            Dim mySheet As Microsoft.Office.Interop.Excel.Worksheet
            Me.Cursor = Cursors.WaitCursor
            myBook = myExcel.Workbooks.Add     '添加一个新的BOOK



            Dim nCheDiHao() As String
            ReDim nCheDiHao(0)
            '加上车底排序
            For i = 1 To Me.lstCheDi.CheckedItems.Count
                ReDim Preserve nCheDiHao(UBound(nCheDiHao) + 1)
                nCheDiHao(UBound(nCheDiHao)) = FromPrintCheDiNameToCheDiID(Me.lstCheDi.CheckedItems(i - 1))
            Next i
            Dim nSheets As Integer
            nSheets = GetMaxZhenShu(UBound(nCheDiHao) / nOutNum)

            Dim ChediID As Integer
            Dim tmpK As Integer
            Dim tmpP As Integer
            Dim curSheetName As String
            Dim curKstate As Integer
            Dim nCheDi As Integer
            Dim sCheDi As String
            Dim iCurRow As Integer
            Dim nStation As Integer
            Dim lArrive As String
            Dim lRtime As String
            Dim lDwell As String
            Dim lDepart As String
            Dim K1 As Integer
            Dim K2 As Integer
            Dim nTolCheDiNum As Integer
            nTolCheDiNum = UBound(nCheDiHao)
            Dim nMaxTrain As Integer
            Dim nMaxTrainPathSta As Integer
            nMaxTrain = 0
            nMaxTrainPathSta = 0
            Me.proBar.Visible = True
            Me.proBar.Value = 0
            Me.proBar.Maximum = UBound(nCheDiHao)
            If nSheets > 0 Then
                For i = 1 To nTolCheDiNum
                    If UBound(ChediInfo(nCheDiHao(i)).nLinkTrain) > nMaxTrain Then
                        nMaxTrain = UBound(ChediInfo(nCheDiHao(i)).nLinkTrain)
                    End If
                    For j = 1 To UBound(ChediInfo(nCheDiHao(i)).nLinkTrain)
                        If ChediInfo(nCheDiHao(i)).nLinkTrain(j) > 0 Then
                            If UBound(TrainInf(ChediInfo(nCheDiHao(i)).nLinkTrain(j)).nPathID) > nMaxTrainPathSta Then
                                nMaxTrainPathSta = UBound(TrainInf(ChediInfo(nCheDiHao(i)).nLinkTrain(j)).nPathID)
                            End If
                        End If
                    Next
                Next i
                rows = nMaxTrainPathSta * nMaxTrain + 2
                cols = nOutNum * 10

                For q = 1 To nSheets
                    mySheet = myBook.Worksheets.Add     '添加一个新的SHEET
                    mySheet.Name = "车底" & q

                    K1 = (q - 1) * nOutNum + 1
                    If q * nOutNum <= nTolCheDiNum Then
                        K2 = q * nOutNum
                    Else
                        K2 = (q - 1) * nOutNum + nTolCheDiNum - (q - 1) * nOutNum
                    End If
                    Dim DataArray(rows, cols) As String
                    For i = 0 To rows
                        For j = 0 To cols
                            DataArray(i, j) = ""
                        Next
                    Next
                    tmpK = 0
                    tmpP = 0
                    For k = K1 To K2
                        curKstate = 0
                        ChediID = nCheDiHao(k)
                        iCurRow = 1
                        If UBound(ChediInfo(ChediID).nLinkTrain) > 0 Then
                            tmpP = tmpP + 1
                            curSheetName = "车底" & GetMaxZhenShu(tmpP / nOutNum)
                            tmpK = tmpK + 1
                            If tmpK Mod nOutNum = 0 Then
                                tmpK = nOutNum
                            Else
                                tmpK = tmpK Mod nOutNum
                            End If
                        End If
                        If Me.optEnglish.Checked = True Then
                            DataArray(0, (tmpK - 1) * 9 + 0) = "Train ID"
                            DataArray(0, (tmpK - 1) * 9 + 1) = "Trip"
                            DataArray(0, (tmpK - 1) * 9 + 2) = "DID"
                            DataArray(0, (tmpK - 1) * 9 + 3) = "LOC"
                            DataArray(0, (tmpK - 1) * 9 + 4) = "Arrive"
                            DataArray(0, (tmpK - 1) * 9 + 5) = "Depart"
                            DataArray(0, (tmpK - 1) * 9 + 6) = "Rtime"
                            DataArray(0, (tmpK - 1) * 9 + 7) = "Dwell"
                            DataArray(0, (tmpK - 1) * 9 + 8) = ""
                        Else
                            DataArray(0, (tmpK - 1) * 9 + 0) = "车次号"
                            DataArray(0, (tmpK - 1) * 9 + 1) = "列次号"
                            DataArray(0, (tmpK - 1) * 9 + 2) = "目的符"
                            DataArray(0, (tmpK - 1) * 9 + 3) = "车站名"
                            DataArray(0, (tmpK - 1) * 9 + 4) = "到点"
                            DataArray(0, (tmpK - 1) * 9 + 5) = "发点"
                            DataArray(0, (tmpK - 1) * 9 + 6) = "运行时分"
                            DataArray(0, (tmpK - 1) * 9 + 7) = "停站时分"
                            DataArray(0, (tmpK - 1) * 9 + 8) = ""
                        End If

                        nCheDi = ChediID '车底序号
                        sCheDi = ChediInfo(nCheDi).sCheCiHao  '车底名称TrainInf(ChediInfo(nCheDi).nLinkTrain(1)).sPrintTrain
                        iCurRow = 2
                        Dim inSame As Integer
                        inSame = 0
                        For i = 1 To UBound(ChediInfo(nCheDi).nLinkTrain)
                            For j = 1 To UBound(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID)
                                inSame = 0
                                For p = 1 To j - 1
                                    If StationInf(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j)).sStationName = StationInf(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(p)).sStationName Then
                                        inSame = 1
                                    End If
                                Next p
                                If inSame = 0 Then

                                    nStation = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j)
                                    DataArray(iCurRow - 1, (tmpK - 1) * 9) = sCheDi
                                    If j = 1 Then
                                        If curKstate = 0 Then
                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 1) = i & "FT"
                                            curKstate = 1
                                        Else
                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 1) = i
                                        End If
                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 2) = Microsoft.VisualBasic.Right(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sPrintTrain, 2)
                                        lArrive = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Arrival(nStation)
                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 4) = dTime(lArrive, 0)
                                        lRtime = lArrive - TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j - 1))
                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 6) = ""
                                    Else
                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 1) = ""
                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 2) = ""
                                        lArrive = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Arrival(nStation)
                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 4) = dTime(lArrive, 0)
                                        lRtime = lArrive - TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j - 1))
                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 6) = dTime(lRtime, 0)
                                    End If
                                    If Me.optEnglish.Checked = True Then
                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = Trim(StationInf(nStation).sEnglishName)
                                    Else
                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = Trim(StationInf(nStation).sPrintStaName)
                                    End If
                                    lDepart = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(nStation)
                                    DataArray(iCurRow - 1, (tmpK - 1) * 9 + 5) = dTime(lDepart, 0)
                                    lDwell = lDepart - lArrive
                                    DataArray(iCurRow - 1, (tmpK - 1) * 9 + 7) = dTime(lDwell, 0)
                                    DataArray(iCurRow - 1, (tmpK - 1) * 9 + 8) = ""
                                    iCurRow = iCurRow + 1
                                End If
                            Next j
                        Next i
                        Me.proBar.Value = k
                    Next k

                    mySheet.Range("A1").Resize(rows, cols).Value = DataArray
                    For p = 1 To cols
                        mySheet.Columns(p).EntireColumn.AutoFit()
                    Next p
                Next q

                '页面设置
                'For i = 1 To myBook.Sheets.Count
                '    myBook.Sheets(i).Activate()
                '    With myBook.Sheets(i).PageSetup
                '        .PrintTitleRows = ""
                '        .PrintArea = ""
                '        .LeftHeader = ""
                '        .CenterHeader = NetInf.sNetName & TimeTablePara.sPubCurSkbName
                '        .RightHeader = "(车底)"
                '        .LeftFooter = ""
                '        .CenterFooter = "第&P页 共&N页"
                '        .RightFooter = SystemPara.sUserCompanyName
                '        .PrintHeadings = False
                '        .PrintGridlines = True
                '        '.Orientation = xlLandscape
                '        .Draft = False
                '        .PaperSize = xlPaperA4
                '        .FirstPageNumber = xlAutomatic
                '        .Zoom = 75
                '    End With
                'Next i
                myExcel.Visible = True
                Me.proBar.Visible = False
                myBook = Nothing
                mySheet = Nothing
                myExcel = Nothing
                Me.Cursor = Cursors.Default
                GC.Collect()
            Else
                MessageBox.Show("没有数据!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch exp As Exception
            MessageBox.Show("数据导出失败!请查看是否已经安装了Excel", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Cursor = Cursors.Default
        Finally

        End Try

    End Sub

    Private Sub btnCreatCheDiLine1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreatCheDiLine1.Click
        Try
            Dim i, j, p, k, q As Integer
            Dim rows As Integer
            Dim cols As Integer
            Dim nOutNum As Integer
            nOutNum = Val(Me.txtCheDiSheetCol.Text)
            If nOutNum >= 1 And nOutNum <= 20 Then
            Else
                MsgBox("该条件下每个SHEET表可输出的个数不能超过20！，也不能为零！")
                Exit Sub
            End If
            Dim myExcel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application
            myExcel.Caption = "车底方式输出"
            Dim myBook As Microsoft.Office.Interop.Excel.Workbook
            Dim mySheet As Microsoft.Office.Interop.Excel.Worksheet
            Me.Cursor = Cursors.WaitCursor
            myBook = myExcel.Workbooks.Add     '添加一个新的BOOK


            Dim nCheDiHao() As String
            ReDim nCheDiHao(0)
            '加上车底排序
            For i = 1 To Me.lstCheDi.CheckedItems.Count
                ReDim Preserve nCheDiHao(UBound(nCheDiHao) + 1)
                nCheDiHao(UBound(nCheDiHao)) = FromPrintCheDiNameToCheDiID(Me.lstCheDi.CheckedItems(i - 1))
            Next i
            Dim nSheets As Integer
            nSheets = GetMaxZhenShu(UBound(nCheDiHao) / nOutNum)

            Dim ChediID As Integer
            Dim tmpK As Integer
            Dim tmpP As Integer
            Dim curSheetName As String
            Dim curKstate As Integer
            Dim nCheDi As Integer
            Dim sCheDi As String
            Dim iCurRow As Integer
            Dim nStation As Integer
            Dim lArrive As String
            Dim lRtime As String
            Dim lDwell As String
            Dim lDepart As String
            Dim K1 As Integer
            Dim K2 As Integer
            Dim nTolCheDiNum As Integer
            nTolCheDiNum = UBound(nCheDiHao)
            Dim nMaxTrain As Integer
            Dim nMaxTrainPathSta As Integer
            nMaxTrain = 0
            nMaxTrainPathSta = 0
            Me.proBar.Visible = True
            Me.proBar.Value = 0
            Me.proBar.Maximum = UBound(nCheDiHao)
            If nSheets > 0 Then
                For i = 1 To nTolCheDiNum
                    If UBound(ChediInfo(nCheDiHao(i)).nLinkTrain) > nMaxTrain Then
                        nMaxTrain = UBound(ChediInfo(nCheDiHao(i)).nLinkTrain)
                    End If
                    For j = 1 To UBound(ChediInfo(nCheDiHao(i)).nLinkTrain)
                        If ChediInfo(nCheDiHao(i)).nLinkTrain(j) > 0 Then
                            If UBound(TrainInf(ChediInfo(nCheDiHao(i)).nLinkTrain(j)).nPathID) > nMaxTrainPathSta Then
                                nMaxTrainPathSta = UBound(TrainInf(ChediInfo(nCheDiHao(i)).nLinkTrain(j)).nPathID)
                            End If
                        End If
                    Next
                Next i
                rows = nMaxTrainPathSta * nMaxTrain + 2
                cols = nOutNum * 12

                For q = 1 To nSheets
                    mySheet = myBook.Worksheets.Add     '添加一个新的SHEET
                    mySheet.Name = "车底" & q

                    K1 = (q - 1) * nOutNum + 1
                    If q * nOutNum <= nTolCheDiNum Then
                        K2 = q * nOutNum
                    Else
                        K2 = (q - 1) * nOutNum + nTolCheDiNum - (q - 1) * nOutNum
                    End If
                    Dim DataArray(rows, cols) As String
                    For i = 0 To rows
                        For j = 0 To cols
                            DataArray(i, j) = ""
                        Next
                    Next
                    tmpK = 0
                    tmpP = 0
                    For k = K1 To K2
                        curKstate = 0
                        ChediID = nCheDiHao(k)
                        iCurRow = 1
                        If UBound(ChediInfo(ChediID).nLinkTrain) > 0 Then
                            tmpP = tmpP + 1
                            curSheetName = "车底" & GetMaxZhenShu(tmpP / nOutNum)
                            tmpK = tmpK + 1
                            If tmpK Mod nOutNum = 0 Then
                                tmpK = nOutNum
                            Else
                                tmpK = tmpK Mod nOutNum
                            End If
                        End If
                        If Me.optEnglish.Checked = True Then
                            DataArray(0, (tmpK - 1) * 11 + 0) = "Train ID"
                            DataArray(0, (tmpK - 1) * 11 + 1) = "Trip"
                            DataArray(0, (tmpK - 1) * 11 + 2) = "DID"
                            DataArray(0, (tmpK - 1) * 11 + 3) = "LOC"
                            DataArray(0, (tmpK - 1) * 11 + 4) = "Arrive"
                            DataArray(0, (tmpK - 1) * 11 + 5) = "Depart"
                            DataArray(0, (tmpK - 1) * 11 + 6) = "Rtime"
                            DataArray(0, (tmpK - 1) * 11 + 7) = "Dwell"
                            DataArray(0, (tmpK - 1) * 11 + 8) = "RunScale"
                            DataArray(0, (tmpK - 1) * 11 + 9) = "StopScale"
                            DataArray(0, (tmpK - 1) * 11 + 10) = ""
                        Else
                            DataArray(0, (tmpK - 1) * 11 + 0) = "车底号"
                            DataArray(0, (tmpK - 1) * 11 + 1) = "列次号"
                            DataArray(0, (tmpK - 1) * 11 + 2) = "车次"
                            DataArray(0, (tmpK - 1) * 11 + 3) = "车站名"
                            DataArray(0, (tmpK - 1) * 11 + 4) = "到点"
                            DataArray(0, (tmpK - 1) * 11 + 5) = "发点"
                            DataArray(0, (tmpK - 1) * 11 + 6) = "运行时分"
                            DataArray(0, (tmpK - 1) * 11 + 7) = "停站时分"
                            DataArray(0, (tmpK - 1) * 11 + 8) = "运行标尺"
                            DataArray(0, (tmpK - 1) * 11 + 9) = "停站标尺"
                            DataArray(0, (tmpK - 1) * 11 + 10) = ""
                        End If

                        nCheDi = ChediID '车底序号
                        sCheDi = ChediInfo(nCheDi).sCheCiHao  '车底名称TrainInf(ChediInfo(nCheDi).nLinkTrain(1)).sPrintTrain
                        iCurRow = 2
                        Dim inSame As Integer
                        Dim sSecName As String
                        Dim sForStaName As String
                        inSame = 0
                        For i = 1 To UBound(ChediInfo(nCheDi).nLinkTrain)
                            sForStaName = ""
                            For j = 1 To UBound(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID)
                                inSame = 0
                                For p = 1 To j - 1
                                    If StationInf(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j)).sStationName = StationInf(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(p)).sStationName Then
                                        inSame = 1
                                    End If
                                Next p
                                If inSame = 0 Then

                                    nStation = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j)
                                    DataArray(iCurRow - 1, (tmpK - 1) * 11) = sCheDi
                                    If j = 1 Then
                                        If curKstate = 0 Then
                                            DataArray(iCurRow - 1, (tmpK - 1) * 11 + 1) = i '& "FT"
                                            curKstate = 1
                                        Else
                                            DataArray(iCurRow - 1, (tmpK - 1) * 11 + 1) = i
                                        End If
                                        DataArray(iCurRow - 1, (tmpK - 1) * 11 + 2) = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sPrintTrain 'Microsoft.VisualBasic.Right(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sPrintTrain, 2)
                                        lArrive = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Arrival(nStation)
                                        DataArray(iCurRow - 1, (tmpK - 1) * 11 + 4) = dTime(lArrive, 0)
                                        lRtime = lArrive - TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j - 1))
                                        DataArray(iCurRow - 1, (tmpK - 1) * 11 + 6) = ""
                                        DataArray(iCurRow - 1, (tmpK - 1) * 11 + 8) = ""
                                    Else
                                        DataArray(iCurRow - 1, (tmpK - 1) * 11 + 1) = ""
                                        DataArray(iCurRow - 1, (tmpK - 1) * 11 + 2) = ""
                                        lArrive = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Arrival(nStation)
                                        DataArray(iCurRow - 1, (tmpK - 1) * 11 + 4) = dTime(lArrive, 0)
                                        lRtime = lArrive - TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j - 1))
                                        DataArray(iCurRow - 1, (tmpK - 1) * 11 + 6) = dTime(lRtime, 0)
                                        If ChediInfo(nCheDi).nLinkTrain(i) Mod 2 = 0 Then
                                            sSecName = StationInf(nStation).sPrintStaName & "->" & sForStaName
                                        Else
                                            sSecName = sForStaName & "->" & StationInf(nStation).sPrintStaName
                                        End If
                                        DataArray(iCurRow - 1, (tmpK - 1) * 11 + 8) = TimeRunScaleNameByBiaoChiScale(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sJiaoLuName, TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sRunScaleName, sSecName)
                                    End If
                                    If Me.optEnglish.Checked = True Then
                                        DataArray(iCurRow - 1, (tmpK - 1) * 11 + 3) = Trim(StationInf(nStation).sEnglishName)
                                    Else
                                        DataArray(iCurRow - 1, (tmpK - 1) * 11 + 3) = Trim(StationInf(nStation).sPrintStaName)
                                    End If
                                    lDepart = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(nStation)
                                    DataArray(iCurRow - 1, (tmpK - 1) * 11 + 5) = dTime(lDepart, 0)
                                    lDwell = lDepart - lArrive
                                    DataArray(iCurRow - 1, (tmpK - 1) * 11 + 7) = dTime(lDwell, 0)
                                    DataArray(iCurRow - 1, (tmpK - 1) * 11 + 9) = GetCurTrainStopScaleAtStation(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sJiaoLuName, TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sStopSclaeName, StationInf(nStation).sStationName)
                                    DataArray(iCurRow - 1, (tmpK - 1) * 11 + 10) = ""
                                    sForStaName = StationInf(nStation).sPrintStaName
                                    iCurRow = iCurRow + 1
                                End If
                            Next j
                        Next i
                        Me.proBar.Value = k
                    Next k

                    mySheet.Range("A1").Resize(rows, cols).Value = DataArray
                    For p = 1 To cols
                        mySheet.Columns(p).EntireColumn.AutoFit()
                    Next p
                Next q

                '页面设置
                'For i = 1 To myBook.Sheets.Count
                '    myBook.Sheets(i).Activate()
                '    With myBook.Sheets(i).PageSetup
                '        .PrintTitleRows = ""
                '        .PrintArea = ""
                '        .LeftHeader = ""
                '        .CenterHeader = NetInf.sNetName & TimeTablePara.sPubCurSkbName
                '        .RightHeader = "(车底)"
                '        .LeftFooter = ""
                '        .CenterFooter = "第&P页 共&N页"
                '        .RightFooter = SystemPara.sUserCompanyName
                '        .PrintHeadings = False
                '        .PrintGridlines = True
                '        '.Orientation = xlLandscape
                '        .Draft = False
                '        .PaperSize = xlPaperA4
                '        .FirstPageNumber = xlAutomatic
                '        .Zoom = 75
                '    End With
                'Next i
                myExcel.Visible = True
                Me.proBar.Visible = False
                myBook = Nothing
                mySheet = Nothing
                myExcel = Nothing
                Me.Cursor = Cursors.Default
                GC.Collect()
            Else
                MessageBox.Show("没有数据!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch exp As Exception
            MessageBox.Show("数据导出失败!请查看是否已经安装了Excel", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Me.Cursor = Cursors.Default
        Finally

        End Try
    End Sub

    Private Sub btnStaSKB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStaSKB.Click
        Dim i As Integer
        Dim objExcel As Object
        Dim myWorkbook As Workbook
        Dim mySheets As Sheets
        Me.Cursor = Cursors.WaitCursor
        objExcel = Nothing
        Try
            objExcel = GetObject("", "Excel.Application")
        Catch ex As Exception
            MsgBox("不能运行Excel，请检查是否安装了 Microsoft Excel 2003!", , "提示")
            Exit Sub
        End Try

        'objExcel.WindowState = xlMaximized '调试用最大化
        myWorkbook = objExcel.Workbooks.Add
        mySheets = myWorkbook.Sheets

        Dim k, p As Integer
        Dim tmpN As Integer
        Dim ToRows As Integer
        Dim sStaName As String
        Me.proBar.Visible = True
        Me.proBar.Value = 0
        Dim rows As Integer
        Dim cols As Integer
        If Me.lstSta.Items.Count > 0 Then
            Me.proBar.Maximum = Me.lstSta.Items.Count
            For k = 1 To Me.lstSta.Items.Count
                If k > 3 Then
                    mySheets.Add()
                End If
                '表格格式设置"Sheet" & k

                sStaName = GetPrintStaNameFromStaName(Me.lstSta.Items(k - 1))
                mySheets("Sheet" & k).Name = sStaName
                mySheets(sStaName).Activate()

                rows = UBound(TrainInf)
                cols = 15
                Dim DataArray(rows, cols) As String
                DataArray(0, 0) = "车站名称"
                DataArray(0, 1) = "下行车次"
                DataArray(0, 2) = "始发站名"
                DataArray(0, 3) = "终到站名"
                DataArray(0, 4) = "到点"
                DataArray(0, 5) = "发点"
                DataArray(0, 6) = "停站时间"

                'With mySheets(sStaName).PageSetup
                '    .LeftHeader = vbCrLf & "内部资料 注意保密"
                '    .CenterHeader = "&""宋体,加粗""&18 " & Me.txtTitle.Text & "──" & sStaName & "站列车时刻表"
                '    .RightHeader = ""
                '    .LeftFooter = Me.txtTime.Text
                '    .CenterFooter = "第&P页，共&N页"
                '    .RightFooter = SystemPara.sUserCompanyName
                '    '.LeftMargin = Application.CentimetersToPoints(1.9)
                '    '.RightMargin = Application.CentimetersToPoints(1.9)
                '    '.TopMargin = Application.CentimetersToPoints(2.4)
                '    '.BottomMargin = Application.CentimetersToPoints(2.5)
                '    '.HeaderMargin = Application.CentimetersToPoints(1)
                '    '.FooterMargin = Application.CentimetersToPoints(1.3)
                '    .PaperSize = xlPaperA4
                '    .CenterHorizontally = True
                '    .CenterVertically = False
                '    '.Orientation = xlLandscape
                'End With

                Call StaOutPrintSKB(sStaName)

                With mySheets(sStaName).Cells
                    .Font.size = 11
                    .HorizontalAlignment = xlCenter
                    .VerticalAlignment = xlCenter
                    .NumberFormatLocal = "@"

                    tmpN = 1

                    For i = 1 To UBound(OutPrintTrainBySta)
                        If OutPrintTrainBySta(i).nTrain Mod 2 <> 0 Then '下行列车
                            tmpN = tmpN + 1
                            DataArray(tmpN - 1, 0) = sStaName
                            If Me.chkTainNumFormat.Checked = False Then
                                DataArray(tmpN - 1, 1) = TrainInf(OutPrintTrainBySta(i).nTrain).sPrintTrain 'TrainInf(OutPrintTrainBySta(i).nTrain).Train '
                            Else
                                DataArray(tmpN - 1, 1) = Microsoft.VisualBasic.Left(TrainInf(OutPrintTrainBySta(i).nTrain).sPrintTrain, 3)
                            End If
                            DataArray(tmpN - 1, 2) = GetPrintStaNameFromStaName(TrainInf(OutPrintTrainBySta(i).nTrain).ComeStation)
                            DataArray(tmpN - 1, 3) = GetPrintStaNameFromStaName(TrainInf(OutPrintTrainBySta(i).nTrain).NextStation)
                            DataArray(tmpN - 1, 4) = dTime(OutPrintTrainBySta(i).sArriTime, 0)
                            ' mySheets(sStaName).Cells(tmpN, 5).NumberFormatLocal = "h:mm:ss;@"
                            DataArray(tmpN - 1, 5) = dTime(OutPrintTrainBySta(i).sStartTime, 0)
                            ' mySheets(sStaName).Cells(tmpN, 6).NumberFormatLocal = "h:mm:ss;@"
                            DataArray(tmpN - 1, 6) = dTime(TimeMinus(OutPrintTrainBySta(i).sStartTime, OutPrintTrainBySta(i).sArriTime), 0)
                            ' mySheets(sStaName).Cells(tmpN, 7).NumberFormatLocal = "h:mm:ss;@"
                        End If
                    Next i
                End With

                ToRows = tmpN

                DataArray(0, 8) = "车站名称"
                DataArray(0, 9) = "上行车次"
                DataArray(0, 10) = "始发站名"
                DataArray(0, 11) = "终到站名"
                DataArray(0, 12) = "到点"
                DataArray(0, 13) = "发点"
                DataArray(0, 14) = "停站时间"

                With mySheets(sStaName).Cells
                    .Font.size = 11
                    .HorizontalAlignment = xlCenter
                    .VerticalAlignment = xlCenter
                    .NumberFormatLocal = "@"
                    tmpN = 1
                    For i = 1 To UBound(OutPrintTrainBySta)
                        If OutPrintTrainBySta(i).nTrain Mod 2 = 0 Then '上行列车
                            tmpN = tmpN + 1
                            DataArray(tmpN - 1, 8) = sStaName
                            If Me.chkTainNumFormat.Checked = False Then
                                DataArray(tmpN - 1, 9) = TrainInf(OutPrintTrainBySta(i).nTrain).sPrintTrain 'TrainInf(OutPrintTrainBySta(i).nTrain).Train ' 
                            Else
                                DataArray(tmpN - 1, 9) = Microsoft.VisualBasic.Left(TrainInf(OutPrintTrainBySta(i).nTrain).sPrintTrain, 3)
                            End If
                            DataArray(tmpN - 1, 10) = GetPrintStaNameFromStaName(TrainInf(OutPrintTrainBySta(i).nTrain).ComeStation)
                            DataArray(tmpN - 1, 11) = GetPrintStaNameFromStaName(TrainInf(OutPrintTrainBySta(i).nTrain).NextStation)
                            DataArray(tmpN - 1, 12) = dTime(OutPrintTrainBySta(i).sArriTime, 0)
                            'mySheets(sStaName).Cells(tmpN, 13).NumberFormatLocal = "h:mm:ss;@"
                            DataArray(tmpN - 1, 13) = dTime(OutPrintTrainBySta(i).sStartTime, 0)
                            'mySheets(sStaName).Cells(tmpN, 14).NumberFormatLocal = "h:mm:ss;@"
                            DataArray(tmpN - 1, 14) = dTime(TimeMinus(OutPrintTrainBySta(i).sStartTime, OutPrintTrainBySta(i).sArriTime), 0)
                            'mySheets(sStaName).Cells(tmpN, 15).NumberFormatLocal = "h:mm:ss;@"
                        End If
                    Next i
                End With

                'For i = 1 To ToRows
                '    mySheets(sStaName).Cells(i, 5).NumberFormatLocal = "h:mm:ss;@"
                '    mySheets(sStaName).Cells(i, 6).NumberFormatLocal = "h:mm:ss;@"
                '    mySheets(sStaName).Cells(i, 7).NumberFormatLocal = "h:mm:ss;@"
                'Next i

                mySheets(sStaName).Columns("A:G").Select()
                mySheets(sStaName).Columns("A:G").Borders(xlDiagonalDown).LineStyle = xlNone
                mySheets(sStaName).Columns("A:G").Borders(xlDiagonalUp).LineStyle = xlNone
                With mySheets(sStaName).Columns("A:G").Borders(xlEdgeLeft)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With
                With mySheets(sStaName).Columns("A:G").Borders(xlEdgeTop)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With
                With mySheets(sStaName).Columns("A:G").Borders(xlEdgeBottom)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With
                With mySheets(sStaName).Columns("A:G").Borders(xlEdgeRight)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With
                With mySheets(sStaName).Columns("A:G").Borders(xlInsideVertical)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With
                With mySheets(sStaName).Columns("A:G").Borders(xlInsideHorizontal)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With

                mySheets(sStaName).Columns("I:O").Select()
                mySheets(sStaName).Columns("I:O").Borders(xlDiagonalDown).LineStyle = xlNone
                mySheets(sStaName).Columns("I:O").Borders(xlDiagonalUp).LineStyle = xlNone
                With mySheets(sStaName).Columns("I:O").Borders(xlEdgeLeft)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With
                With mySheets(sStaName).Columns("I:O").Borders(xlEdgeTop)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With
                With mySheets(sStaName).Columns("I:O").Borders(xlEdgeBottom)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With
                With mySheets(sStaName).Columns("I:O").Borders(xlEdgeRight)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With
                With mySheets(sStaName).Columns("I:O").Borders(xlInsideVertical)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With
                With mySheets(sStaName).Columns("I:O").Borders(xlInsideHorizontal)
                    .LineStyle = xlContinuous
                    .Weight = xlThin
                    .ColorIndex = xlAutomatic
                End With
                mySheets(sStaName).Range("A1").Resize(rows, cols).Value = DataArray
                For p = 1 To cols
                    mySheets(sStaName).Columns(p).EntireColumn.AutoFit()
                Next p

                Me.proBar.Value = k
            Next k
        End If
        Me.proBar.Visible = False
        objExcel.Visible = True
        objExcel = Nothing
        myWorkbook = Nothing
        mySheets = Nothing
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnOutFenLei_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutFenLei.Click

        Dim nPageNum As Integer
        nPageNum = Val(Me.txtCols.Text)
        If nPageNum >= 1 And nPageNum <= 250 Then
        Else
            MsgBox("每个工作表输出列数设置错误，请重输！")
            Exit Sub
        End If

        'Dim intIdx As Integer
        Dim i As Integer, j As Integer
        Dim p As Integer
        Dim Rows As Integer
        Dim Cols As Integer
        Dim iRows As Integer
        'Dim iColumns As Integer
        Dim nTrain As Integer
        Dim objExcel As Microsoft.Office.Interop.Excel.Application
        Dim myWorkbook As Microsoft.Office.Interop.Excel.Workbook, mySheets As Microsoft.Office.Interop.Excel.Sheets
        Dim nCurSTnSeqID As Integer
        Dim iColUp As Integer
        Dim iColDown As Integer
        Dim nIfPrint As Boolean
        objExcel = Nothing

        For i = 1 To UBound(SkbStnSeq)
            If SkbStnSeq(i).sQDName = Me.cmbQuDuanName.Text Then
                nCurSTnSeqID = i
                Exit For
            End If
        Next i


        iColUp = 2 '两列表头
        iColDown = 2
        Me.proBar.Maximum = 1000
        proBar.Value = 0
        proBar.Visible = True

        Try
            objExcel = GetObject("", "Excel.Application")
        Catch ex As Exception
            MsgBox("不能运行Excel，请检查是否安装了 Microsoft Excel 2003!", , "提示")
            Exit Sub
        End Try

        Me.Cursor = Cursors.WaitCursor
        ' objExcel.WindowState = Excel.xlMaximized '调试用最大化
        myWorkbook = objExcel.Workbooks.Add
        mySheets = myWorkbook.Sheets

        iRows = UBound(SkbStnSeq(nCurSTnSeqID).nStnSeq) '车站数


        '按列车到站点排序
        Dim ntmpTrain() As Integer

        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long
        ReDim ntmpTrain(0)
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                ReDim Preserve ntmpTrain(UBound(ntmpTrain) + 1)
                ntmpTrain(UBound(ntmpTrain)) = i
            End If
        Next i
        '按到达时间排序
        Flag = 1
        k = UBound(ntmpTrain)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                If Me.cmbPaiXu.Text = "始发站" Then
                    TempTime1 = AddLitterTime(TrainInf(ntmpTrain(j)).Starting(TrainInf(ntmpTrain(j)).nPathID(1)))
                    Temptime2 = AddLitterTime(TrainInf(ntmpTrain(j + 1)).Starting(TrainInf(ntmpTrain(j + 1)).nPathID(1)))
                Else
                    TempTime1 = AddLitterTime(GetStartTimeFromStaName(Me.cmbPaiXu.Text, ntmpTrain(j)))
                    Temptime2 = AddLitterTime(GetStartTimeFromStaName(Me.cmbPaiXu.Text, ntmpTrain(j + 1)))
                End If
                If TempTime1 > Temptime2 Then '
                    temp = ntmpTrain(j)
                    ntmpTrain(j) = ntmpTrain(j + 1)
                    ntmpTrain(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop

        Rows = iRows * 2 + 2
        Cols = 254
        Dim DataArray(Rows, Cols) As String

        Me.proBar.Value = 50

        '表格格式设置
        '出库、入库
        'mySheets("Sheet1").Name = "出库"
        'mySheets("出库").Activate()
        'DataArray(0, 0) = "站名\车次"
        'mySheets("出库").Columns("A:A").ColumnWidth = 10
        'mySheets("出库").Columns("B:B").ColumnWidth = 6

        'With mySheets("出库").Cells
        '    .Font.size = 11
        '    .HorizontalAlignment = xlCenter
        '    .VerticalAlignment = xlCenter
        '    .NumberFormatLocal = "@"
        '    For i = 1 To iRows
        '        DataArray(2 * i - 1, 0) = StationInf(SkbStnSeq(nCurSTnSeqID).nStnSeq(i)).sStationName
        '        mySheets("出库").Range("A" & Trim(Str(2 * i)) & ":" & "A" & Trim(Str(2 * i + 1))).Merge()
        '        mySheets("出库").Range("B" & Trim(Str(2 * i)) & ":" & "B" & Trim(Str(2 * i + 1))).Merge()
        '        DataArray(2 * i - 1, 1) = "到\发"
        '    Next i
        'End With

        'Me.proBar.Value = 100
        Dim nCurCol As Integer
        'nCurCol = 2
        'For i = 1 To UBound(ntmpTrain) '先输出出库车
        '    nTrain = ntmpTrain(i)
        '    If TrainInf(nTrain).TrainStyle = "出库车" Then
        '        nIfPrint = IFCurTrainNotTimeInCurDitu(nTrain, nCurSTnSeqID)
        '        If nIfPrint = True Then
        '            nCurCol = nCurCol + 1
        '            DataArray(0, nCurCol - 1) = TrainInf(nTrain).sPrintTrain
        '            For j = 1 To iRows
        '                DataArray(j * 2 - 1, nCurCol - 1) = dTime(TrainInf(nTrain).Arrival(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
        '                DataArray(j * 2, nCurCol - 1) = dTime(TrainInf(nTrain).Starting(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
        '                If Me.chkTimeFormate.Checked = True Then
        '                    mySheets("出库").Cells(j * 2, nCurCol).NumberFormatLocal = "h:mm:ss;@"
        '                    mySheets("出库").Cells(j * 2 + 1, nCurCol).NumberFormatLocal = "h:mm:ss;@"
        '                End If
        '            Next j
        '        End If
        '    End If
        'Next i

        'mySheets("出库").Range("A1").Resize(Rows, Cols).Value = DataArray

        'Me.proBar.Value = 150

        'ReDim DataArray(Rows, Cols)
        'mySheets("Sheet2").Name = "入库"
        'mySheets("入库").Activate()
        'DataArray(0, 0) = "站名\车次"
        'mySheets("入库").Columns("A:A").ColumnWidth = 10
        'mySheets("入库").Columns("B:B").ColumnWidth = 6
        'With mySheets("入库").Cells
        '    .Font.size = 11
        '    .HorizontalAlignment = xlCenter
        '    .VerticalAlignment = xlCenter
        '    .NumberFormatLocal = "@"
        '    For i = 1 To iRows
        '        DataArray(2 * i - 1, 0) = StationInf(SkbStnSeq(nCurSTnSeqID).nStnSeq(i)).sStationName
        '        mySheets("入库").Range("A" & Trim(Str(2 * i)) & ":" & "A" & Trim(Str(2 * i + 1))).Merge()
        '        mySheets("入库").Range("B" & Trim(Str(2 * i)) & ":" & "B" & Trim(Str(2 * i + 1))).Merge()
        '        DataArray(2 * i - 1, 1) = "到\发"
        '    Next i
        'End With

        'nCurCol = 2
        'For i = 1 To UBound(ntmpTrain)  '输出入库车
        '    nTrain = ntmpTrain(i)
        '    If TrainInf(nTrain).TrainStyle = "入库车" Then
        '        nIfPrint = IFCurTrainNotTimeInCurDitu(nTrain, nCurSTnSeqID)
        '        If nIfPrint = True Then
        '            nCurCol = nCurCol + 1
        '            DataArray(0, nCurCol - 1) = TrainInf(nTrain).sPrintTrain
        '            For j = 1 To iRows
        '                DataArray(j * 2 - 1, nCurCol - 1) = dTime(TrainInf(nTrain).Arrival(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
        '                DataArray(j * 2, nCurCol - 1) = dTime(TrainInf(nTrain).Starting(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
        '                If Me.chkTimeFormate.Checked = True Then
        '                    mySheets("入库").Cells(j * 2, nCurCol).NumberFormatLocal = "h:mm:ss;@"
        '                    mySheets("入库").Cells(j * 2 + 1, nCurCol).NumberFormatLocal = "h:mm:ss;@"
        '                End If
        '            Next j
        '        End If
        '    End If
        'Next i
        'mySheets("入库").Range("A1").Resize(Rows, Cols).Value = DataArray


        Dim nDownNum As Integer
        Dim nUpNum As Integer
        Dim nDownPage As Integer
        Dim nUpPage As Integer

        nUpNum = 0
        nDownNum = 0
        Dim sCurDownName As String
        Dim sCurUpName As String
        Dim tmpK As Integer
        Dim NTolNum As Integer
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" And (i Mod 2 = 0) Then
                nUpNum = nUpNum + 1
            End If
        Next i

        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" And (i Mod 2 <> 0) Then
                nDownNum = nDownNum + 1
            End If
        Next i

        Me.proBar.Value = 200
        nUpPage = GetMaxZhenShu(nUpNum / nPageNum)
        nDownPage = GetMaxZhenShu(nDownNum / nPageNum)
        NTolNum = 0
        For p = 1 To nDownPage
            mySheets.Add()
            ReDim DataArray(Rows, Cols)
            sCurDownName = "下行" & p
            mySheets("Sheet" & p + 3).Name = sCurDownName
            mySheets(sCurDownName).Activate()
            DataArray(0, 0) = "下行"
            mySheets(sCurDownName).Columns("A:A").ColumnWidth = 10
            mySheets(sCurDownName).Columns("B:B").ColumnWidth = 3

            With mySheets(sCurDownName).Cells
                .Font.size = 11
                .HorizontalAlignment = xlCenter
                .VerticalAlignment = xlCenter
                .NumberFormatLocal = "@"
                For j = 1 To iRows
                    DataArray(2 * j - 1, 0) = StationInf(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)).sPrintStaName
                    mySheets(sCurDownName).Range("A" & Trim(Str(2 * j)) & ":" & "A" & Trim(Str(2 * j + 1))).Merge()
                    DataArray(2 * j - 1, 1) = "到"
                    DataArray(2 * j, 1) = "发"
                Next j
            End With

            nCurCol = 2
            tmpK = 0
            For i = 1 To UBound(ntmpTrain)  '输出下行车
                nTrain = ntmpTrain(i)
                If nTrain Mod 2 <> 0 Then
                    nIfPrint = IFCurTrainNotTimeInCurDitu(nTrain, nCurSTnSeqID)
                    If nIfPrint = True Then
                        tmpK = tmpK + 1
                        nCurCol = 3 + ((tmpK - 1) Mod nPageNum)
                        If GetMaxZhenShu(tmpK / nPageNum) = p Then
                            sCurDownName = "下行" & GetMaxZhenShu(tmpK / nPageNum)
                            DataArray(0, nCurCol - 1) = TrainInf(nTrain).sPrintTrain
                            For j = 1 To iRows
                                DataArray(j * 2 - 1, nCurCol - 1) = dTime(TrainInf(nTrain).Arrival(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                                DataArray(j * 2, nCurCol - 1) = dTime(TrainInf(nTrain).Starting(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                                If Me.chkTimeFormate.Checked = True Then
                                    mySheets(sCurDownName).Cells(j * 2 + 1, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                                    mySheets(sCurDownName).Cells(j * 2, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                                End If
                            Next j
                            Me.proBar.Value = 200 + Int(400.0# * NTolNum / nDownNum)
                            NTolNum = NTolNum + 1
                        End If
                    End If
                End If
            Next i
            mySheets(sCurDownName).Range("A1").Resize(Rows, Cols).Value = DataArray
        Next p

        NTolNum = 0
        For p = 1 To nUpPage
            mySheets.Add()
            ReDim DataArray(Rows, Cols)
            sCurUpName = "上行" & p
            mySheets("Sheet" & p + 3 + nDownPage).Name = sCurUpName
            mySheets(sCurUpName).Activate()
            DataArray(0, 0) = "上行"
            mySheets(sCurUpName).Columns("A:A").ColumnWidth = 10
            mySheets(sCurUpName).Columns("B:B").ColumnWidth = 3
            With mySheets(sCurUpName).Cells
                .Font.size = 11
                .HorizontalAlignment = xlCenter
                .VerticalAlignment = xlCenter
                .NumberFormatLocal = "@"
                For j = 1 To iRows
                    DataArray(2 * j - 1, 0) = StationInf(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)).sPrintStaName
                    mySheets(sCurUpName).Range("A" & Trim(Str(2 * j)) & ":" & "A" & Trim(Str(2 * j + 1))).Merge()
                    DataArray(2 * j - 1, 1) = "发"
                    DataArray(2 * j, 1) = "到"
                Next j
            End With

            nCurCol = 2
            tmpK = 0
            For i = 1 To UBound(ntmpTrain)  '输出上行车
                nTrain = ntmpTrain(i)
                If nTrain Mod 2 = 0 Then
                    nIfPrint = IFCurTrainNotTimeInCurDitu(nTrain, nCurSTnSeqID)
                    If nIfPrint = True Then
                        tmpK = tmpK + 1
                        nCurCol = 3 + ((tmpK - 1) Mod nPageNum)
                        If GetMaxZhenShu(tmpK / nPageNum) = p Then
                            sCurUpName = "上行" & GetMaxZhenShu(tmpK / nPageNum)
                            DataArray(0, nCurCol - 1) = TrainInf(nTrain).sPrintTrain
                            For j = 1 To iRows
                                DataArray(j * 2 - 1, nCurCol - 1) = dTime(TrainInf(nTrain).Starting(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                                DataArray(j * 2, nCurCol - 1) = dTime(TrainInf(nTrain).Arrival(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                                If Me.chkTimeFormate.Checked = True Then
                                    mySheets(sCurUpName).Cells(j * 2, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                                    mySheets(sCurUpName).Cells(j * 2 + 1, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                                End If
                            Next j
                            Me.proBar.Value = 600 + Int(400.0# * NTolNum / nUpNum)
                            NTolNum = NTolNum + 1
                        End If
                    End If
                End If
            Next i
            mySheets(sCurUpName).Range("A1").Resize(Rows, Cols).Value = DataArray
        Next p


        ''页面设置
        'For i = 1 To mySheets.Count
        '    mySheets(1).Activate()
        '    With mySheets(i).PageSetup
        '        .PrintTitleRows = ""
        '        .PrintTitleColumns = "A:B"
        '        .PrintArea = ""
        '        .LeftHeader = ""
        '        .CenterHeader = NetInf.sNetName & "运行图时刻表"
        '        .RightHeader = ""
        '        .LeftFooter = ""
        '        .CenterFooter = "第&P页 共&N页"
        '        .RightFooter = SystemPara.sUserCompanyName
        '        .PrintHeadings = False
        '        .PrintGridlines = True
        '        '.Orientation = xlLandscape
        '        .Draft = False
        '        .PaperSize = xlPaperA4
        '        .FirstPageNumber = xlAutomatic
        '        .Zoom = 80
        '    End With
        'Next i
        Me.proBar.Visible = False
        objExcel.Visible = True
        objExcel = Nothing
        myWorkbook = Nothing
        mySheets = Nothing
        Me.Cursor = Cursors.Default
    End Sub

    '判断该列车是否在输出车站中上有没有点
    Private Function IFCurTrainNotTimeInCurDitu(ByVal nTrain As Integer, ByVal nCurSTnSeqID As Integer) As Boolean
        IFCurTrainNotTimeInCurDitu = False
        Dim i As Integer
        For i = 1 To UBound(SkbStnSeq(nCurSTnSeqID).nStnSeq)
            If TrainInf(nTrain).Starting(SkbStnSeq(nCurSTnSeqID).nStnSeq(i)) <> -1 Or TrainInf(nTrain).Arrival(SkbStnSeq(nCurSTnSeqID).nStnSeq(i)) <> -1 Then
                IFCurTrainNotTimeInCurDitu = True
                Exit For
            End If
        Next
    End Function

    'Private Sub btnLine2New_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim i, j, p, k, q As Integer
    '        Dim rows As Integer
    '        Dim cols As Integer
    '        Dim nOutNum As Integer
    '        nOutNum = Val(Me.txtCheDiSheetCol.Text)
    '        If nOutNum >= 1 And nOutNum <= 25 Then
    '        Else
    '            MsgBox("每个SHEET表可输出的个数不能超过25！，也不能为零！")
    '            Exit Sub
    '        End If

    '        Dim myExcel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application
    '        'myExcel.Caption = ExcelTitle
    '        Dim myBook As Microsoft.Office.Interop.Excel.Workbook
    '        Dim mySheet As Microsoft.Office.Interop.Excel.Worksheet
    '        Me.Cursor = Cursors.WaitCursor
    '        myBook = myExcel.Workbooks.Add     '添加一个新的BOOK



    '        Dim nCheDiHao() As String
    '        ReDim nCheDiHao(0)
    '        '加上车底排序
    '        For i = 1 To Me.lstCheDi.CheckedItems.Count
    '            ReDim Preserve nCheDiHao(UBound(nCheDiHao) + 1)
    '            nCheDiHao(UBound(nCheDiHao)) = FromPrintCheDiNameToCheDiID(Me.lstCheDi.CheckedItems(i - 1))
    '        Next i
    '        Dim nSheets As Integer
    '        nSheets = GetMaxZhenShu(UBound(nCheDiHao) / nOutNum)

    '        Dim ChediID As Integer
    '        Dim tmpK As Integer
    '        Dim tmpP As Integer
    '        Dim curSheetName As String
    '        Dim curKstate As Integer
    '        Dim nCheDi As Integer
    '        Dim sCheDi As String
    '        Dim iCurRow As Integer
    '        Dim nStation As Integer
    '        Dim lArrive As String
    '        Dim lRtime As String
    '        Dim lDwell As String
    '        Dim lDepart As String
    '        Dim K1 As Integer
    '        Dim K2 As Integer
    '        Dim K3 As Integer
    '        Dim nTolCheDiNum As Integer
    '        Dim nIFPrint As Integer
    '        nTolCheDiNum = UBound(nCheDiHao)
    '        Dim nMaxTrain As Integer
    '        Dim nMaxTrainPathSta As Integer
    '        Dim nCircleNum As Integer
    '        Dim nLastEndTime As Integer
    '        nMaxTrain = 0
    '        nMaxTrainPathSta = 0
    '        Me.proBar.Visible = True
    '        Me.proBar.Value = 0
    '        Me.proBar.Maximum = UBound(nCheDiHao)
    '        lArrive = 0
    '        Dim tmpGudaoName As String
    '        If nSheets > 0 Then
    '            For i = 1 To nTolCheDiNum
    '                If UBound(ChediInfo(nCheDiHao(i)).nLinkTrain) > nMaxTrain Then
    '                    nMaxTrain = UBound(ChediInfo(nCheDiHao(i)).nLinkTrain)
    '                End If
    '                For j = 1 To UBound(ChediInfo(nCheDiHao(i)).nLinkTrain)
    '                    If ChediInfo(nCheDiHao(i)).nLinkTrain(j) > 0 Then
    '                        If UBound(TrainInf(ChediInfo(nCheDiHao(i)).nLinkTrain(j)).nPathID) > nMaxTrainPathSta Then
    '                            nMaxTrainPathSta = UBound(TrainInf(ChediInfo(nCheDiHao(i)).nLinkTrain(j)).nPathID)
    '                        End If
    '                    End If
    '                Next
    '            Next i
    '            rows = nMaxTrainPathSta * nMaxTrain * 2 + 6
    '            cols = nOutNum * 10

    '            For q = 1 To nSheets
    '                mySheet = myBook.Worksheets.Add     '添加一个新的SHEET
    '                mySheet.Name = "车底" & q

    '                K1 = (q - 1) * nOutNum + 1
    '                If q * nOutNum <= nTolCheDiNum Then
    '                    K2 = q * nOutNum
    '                Else
    '                    K2 = (q - 1) * nOutNum + nTolCheDiNum - (q - 1) * nOutNum
    '                End If
    '                Dim DataArray(rows, cols) As String
    '                For i = 0 To rows
    '                    For j = 0 To cols
    '                        DataArray(i, j) = ""
    '                    Next
    '                Next
    '                tmpK = 0
    '                tmpP = 0
    '                For k = K1 To K2
    '                    curKstate = 0
    '                    ChediID = nCheDiHao(k)
    '                    iCurRow = 1
    '                    If UBound(ChediInfo(ChediID).nLinkTrain) > 0 Then
    '                        tmpP = tmpP + 1
    '                        curSheetName = "车底" & GetMaxZhenShu(tmpP / nOutNum)
    '                        tmpK = tmpK + 1
    '                        If tmpK Mod nOutNum = 0 Then
    '                            tmpK = nOutNum
    '                        Else
    '                            tmpK = tmpK Mod nOutNum
    '                        End If
    '                    End If
    '                    If Me.optEnglish.Checked = True Then
    '                        DataArray(0, (tmpK - 1) * 9 + 0) = "列车ID"
    '                        DataArray(0, (tmpK - 1) * 9 + 1) = "圈次"
    '                        DataArray(0, (tmpK - 1) * 9 + 2) = "目的地ID"
    '                        DataArray(0, (tmpK - 1) * 9 + 3) = "车站"
    '                        DataArray(0, (tmpK - 1) * 9 + 4) = "到达"
    '                        DataArray(0, (tmpK - 1) * 9 + 5) = "发车"
    '                        DataArray(0, (tmpK - 1) * 9 + 6) = "运行时间"
    '                        DataArray(0, (tmpK - 1) * 9 + 7) = "停站时间"
    '                        DataArray(0, (tmpK - 1) * 9 + 8) = ""
    '                    Else
    '                        DataArray(0, (tmpK - 1) * 9 + 0) = "车次号"
    '                        DataArray(0, (tmpK - 1) * 9 + 1) = "列次号"
    '                        DataArray(0, (tmpK - 1) * 9 + 2) = "目的符"
    '                        DataArray(0, (tmpK - 1) * 9 + 3) = "车站名"
    '                        DataArray(0, (tmpK - 1) * 9 + 4) = "到点"
    '                        DataArray(0, (tmpK - 1) * 9 + 5) = "发点"
    '                        DataArray(0, (tmpK - 1) * 9 + 6) = "运行时分"
    '                        DataArray(0, (tmpK - 1) * 9 + 7) = "停站时分"
    '                        DataArray(0, (tmpK - 1) * 9 + 8) = ""
    '                    End If

    '                    nCheDi = ChediID '车底序号
    '                    sCheDi = ChediInfo(nCheDi).sCheCiHao  '车底名称TrainInf(ChediInfo(nCheDi).nLinkTrain(1)).sPrintTrain
    '                    iCurRow = 2
    '                    Dim inSame As Integer
    '                    inSame = 0
    '                    nLastEndTime = 0
    '                    nCircleNum = 0
    '                    For i = 1 To UBound(ChediInfo(nCheDi).nLinkTrain)
    '                        For j = 1 To UBound(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID)
    '                            inSame = 0
    '                            For p = 1 To j - 1
    '                                If StationInf(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j)).sStationName = StationInf(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(p)).sStationName Then
    '                                    inSame = 1
    '                                End If
    '                            Next p
    '                            If inSame = 0 Then

    '                                nStation = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j)
    '                                If StationInf(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j)).sStationName = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).ComeStation Then
    '                                    If TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).TrainReturnStyle(1) = "站后折返" Then
    '                                        If i > 1 Then
    '                                            tmpGudaoName = FromGudaoNumToGuDaoName(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sStartZFLine, TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j))
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9) = sCheDi
    '                                            nCircleNum = nCircleNum + 1
    '                                            DataArray(iCurRow - 2, (tmpK - 1) * 9 + 1) = nCircleNum
    '                                            DataArray(iCurRow - 2, (tmpK - 1) * 9 + 2) = Microsoft.VisualBasic.Right(TrainInf(ChediInfo(nCheDi).nLinkTrain(i - 1)).sPrintTrain, 2)
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 1) = ""
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 2) = ""
    '                                            If Me.optEnglish.Checked = True Then
    '                                                DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = tmpGudaoName
    '                                            Else
    '                                                DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = "折返线" & tmpGudaoName
    '                                            End If
    '                                            lArrive = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sStartZFArrival
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 4) = dTime(lArrive, 0)
    '                                            lRtime = TimeMinus(lArrive, nLastEndTime) ' TimeMinus(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sStartZFStarting, lArrive)
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 6) = dTime(lRtime, 0)
    '                                            lDepart = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(nStation)
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 5) = "" 'dTime(lDepart, 0)
    '                                            lDwell = TimeMinus(lDepart, lArrive)
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 7) = "" ' dTime(lDwell, 0)
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 8) = ""
    '                                            iCurRow = iCurRow + 1

    '                                            nCircleNum = nCircleNum + 1
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9) = sCheDi
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 1) = nCircleNum
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 2) = Microsoft.VisualBasic.Right(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sPrintTrain, 2)
    '                                            If Me.optEnglish.Checked = True Then
    '                                                DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = tmpGudaoName
    '                                            Else
    '                                                DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = "折返线" & tmpGudaoName
    '                                            End If
    '                                            lArrive = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sStartZFArrival
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 4) = "" ' dTime(lArrive, 0)
    '                                            lRtime = TimeMinus(lArrive, TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j - 1)))
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 6) = ""
    '                                            lDepart = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sStartZFStarting
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 5) = dTime(lDepart, 0)
    '                                            lDwell = TimeMinus(lDepart, lArrive)
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 7) = dTime(lDwell, 0)
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 8) = ""
    '                                            iCurRow = iCurRow + 1
    '                                        End If

    '                                        tmpGudaoName = FromGudaoNumToGuDaoName(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).StopLine(nStation), nStation)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9) = sCheDi
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 1) = ""
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 2) = "" 'Microsoft.VisualBasic.Right(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sPrintTrain, 2)
    '                                        If Me.optEnglish.Checked = True Then
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = tmpGudaoName
    '                                        Else
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = Trim(StationInf(nStation).sStationName)
    '                                        End If
    '                                        lArrive = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Arrival(nStation)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 4) = dTime(lArrive, 0)
    '                                        lRtime = TimeMinus(lArrive, TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sStartZFStarting)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 6) = dTime(lRtime, 0)
    '                                        lDepart = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sStartZFStarting
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 5) = "" 'dTime(lDepart, 0)
    '                                        lDwell = TimeMinus(lDepart, lArrive)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 7) = "" 'dTime(lDwell, 0)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 8) = ""
    '                                        iCurRow = iCurRow + 1

    '                                        nCircleNum = nCircleNum + 1
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9) = sCheDi
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 1) = nCircleNum
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 2) = Microsoft.VisualBasic.Right(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sPrintTrain, 2)
    '                                        If Me.optEnglish.Checked = True Then
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = tmpGudaoName
    '                                        Else
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = Trim(StationInf(nStation).sStationName)
    '                                        End If
    '                                        lArrive = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Arrival(nStation)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 4) = "" ' dTime(lArrive, 0)
    '                                        lRtime = 0 'TimeMinus(lArrive, TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j - 1)))
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 6) = ""
    '                                        lDepart = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(nStation)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 5) = dTime(lDepart, 0)
    '                                        lDwell = TimeMinus(lDepart, lArrive)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 7) = dTime(lDwell, 0)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 8) = ""
    '                                        iCurRow = iCurRow + 1

    '                                    Else '站前
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9) = sCheDi
    '                                        nCircleNum = nCircleNum + 1
    '                                        If i = 1 Then
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 1) = nCircleNum & "FT"
    '                                        Else
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 1) = nCircleNum
    '                                        End If
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 2) = Microsoft.VisualBasic.Right(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sPrintTrain, 2)
    '                                        If i = 1 Then
    '                                            lArrive = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(nStation)
    '                                        Else
    '                                            lArrive = TrainInf(ChediInfo(nCheDi).nLinkTrain(i - 1)).Arrival(nStation)
    '                                        End If
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 4) = "" 'dTime(lArrive, 0)
    '                                        lRtime = TimeMinus(lArrive, TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j - 1)))
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 6) = "" ' dTime(lRtime, 0)
    '                                        If Me.optEnglish.Checked = True Then
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = Trim(StationInf(nStation).sEnglishName)
    '                                        Else
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = Trim(StationInf(nStation).sStationName)
    '                                        End If
    '                                        lDepart = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(nStation)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 5) = dTime(lDepart, 0)
    '                                        lDwell = TimeMinus(lDepart, lArrive)
    '                                        If i = 1 Then
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 7) = ""
    '                                        Else
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 7) = dTime(lDwell, 0)
    '                                        End If
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 8) = ""
    '                                        iCurRow = iCurRow + 1
    '                                    End If

    '                                ElseIf StationInf(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j)).sStationName = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).NextStation Then
    '                                    If TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).TrainReturnStyle(2) = "站后折返" Then
    '                                        tmpGudaoName = FromGudaoNumToGuDaoName(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).StopLine(nStation), nStation)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9) = sCheDi
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 1) = ""
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 2) = ""
    '                                        lArrive = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Arrival(nStation)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 4) = dTime(lArrive, 0)
    '                                        lRtime = TimeMinus(lArrive, TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j - 1)))
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 6) = dTime(lRtime, 0)
    '                                        If Me.optEnglish.Checked = True Then
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = tmpGudaoName
    '                                        Else
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = Trim(StationInf(nStation).sStationName)
    '                                        End If
    '                                        lDepart = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(nStation)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 5) = "" 'dTime(lDepart, 0)
    '                                        lDwell = TimeMinus(lDepart, lArrive)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 7) = "" 'dTime(lDwell, 0)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 8) = ""
    '                                        iCurRow = iCurRow + 1

    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9) = sCheDi
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 1) = ""
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 2) = ""
    '                                        lArrive = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Arrival(nStation)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 4) = "" 'dTime(lArrive, 0)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 6) = ""
    '                                        If Me.optEnglish.Checked = True Then
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = tmpGudaoName
    '                                        Else
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = Trim(StationInf(nStation).sStationName)
    '                                        End If
    '                                        lDepart = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(nStation)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 5) = dTime(lDepart, 0)
    '                                        lDwell = TimeMinus(lDepart, lArrive)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 7) = dTime(lDwell, 0)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 8) = ""
    '                                        iCurRow = iCurRow + 1
    '                                        nLastEndTime = lDepart
    '                                    Else '站前
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9) = sCheDi
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 1) = ""
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 2) = ""
    '                                        lArrive = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Arrival(nStation)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 4) = dTime(lArrive, 0)
    '                                        lRtime = TimeMinus(lArrive, TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j - 1)))
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 6) = dTime(lRtime, 0)
    '                                        If Me.optEnglish.Checked = True Then
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = Trim(StationInf(nStation).sEnglishName)
    '                                        Else
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = Trim(StationInf(nStation).sStationName)
    '                                        End If
    '                                        lDepart = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(nStation)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 5) = "" ' dTime(lDepart, 0)
    '                                        lDwell = TimeMinus(lDepart, lArrive)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 7) = "" ' dTime(lDwell, 0)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 8) = ""
    '                                        iCurRow = iCurRow + 1
    '                                    End If
    '                                Else
    '                                    nIFPrint = 0
    '                                    For K3 = 1 To Me.checkListsta.CheckedItems.Count
    '                                        If Me.checkListsta.CheckedItems(K3 - 1) = StationInf(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j)).sStationName Then '需要断开车站
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9) = sCheDi
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 1) = ""
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 2) = ""
    '                                            lArrive = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Arrival(nStation)
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 4) = dTime(lArrive, 0)
    '                                            lRtime = TimeMinus(lArrive, TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j - 1)))
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 6) = dTime(lRtime, 0)
    '                                            If Me.optEnglish.Checked = True Then
    '                                                DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = Trim(StationInf(nStation).sEnglishName)
    '                                            Else
    '                                                DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = Trim(StationInf(nStation).sStationName)
    '                                            End If
    '                                            lDepart = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(nStation)
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 5) = "" ' dTime(lDepart, 0)
    '                                            lDwell = TimeMinus(lDepart, lArrive)
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 7) = "" 'dTime(lDwell, 0)
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 8) = ""
    '                                            iCurRow = iCurRow + 1

    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9) = sCheDi
    '                                            nCircleNum = nCircleNum + 1
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 1) = nCircleNum
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 2) = Microsoft.VisualBasic.Right(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).sPrintTrain, 2)
    '                                            lArrive = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Arrival(nStation)
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 4) = "" 'dTime(lArrive, 0)
    '                                            lRtime = TimeMinus(lArrive, TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j - 1)))
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 6) = "" 'dTime(lRtime, 0)
    '                                            If Me.optEnglish.Checked = True Then
    '                                                DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = Trim(StationInf(nStation).sEnglishName)
    '                                            Else
    '                                                DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = Trim(StationInf(nStation).sStationName)
    '                                            End If
    '                                            lDepart = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(nStation)
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 5) = dTime(lDepart, 0)
    '                                            lDwell = TimeMinus(lDepart, lArrive)
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 7) = dTime(lDwell, 0)
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 8) = ""
    '                                            iCurRow = iCurRow + 1
    '                                            nIFPrint = 1
    '                                            Exit For
    '                                        End If
    '                                    Next
    '                                    If nIFPrint = 0 Then
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9) = sCheDi
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 1) = ""
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 2) = ""
    '                                        lArrive = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Arrival(nStation)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 4) = dTime(lArrive, 0)
    '                                        lRtime = TimeMinus(lArrive, TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).nPathID(j - 1)))
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 6) = dTime(lRtime, 0)
    '                                        If Me.optEnglish.Checked = True Then
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = Trim(StationInf(nStation).sEnglishName)
    '                                        Else
    '                                            DataArray(iCurRow - 1, (tmpK - 1) * 9 + 3) = Trim(StationInf(nStation).sStationName)
    '                                        End If
    '                                        lDepart = TrainInf(ChediInfo(nCheDi).nLinkTrain(i)).Starting(nStation)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 5) = dTime(lDepart, 0)
    '                                        lDwell = TimeMinus(lDepart, lArrive)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 7) = dTime(lDwell, 0)
    '                                        DataArray(iCurRow - 1, (tmpK - 1) * 9 + 8) = ""
    '                                        iCurRow = iCurRow + 1
    '                                    End If
    '                                End If
    '                            End If
    '                        Next j
    '                        'nCircleNum = nCircleNum + 1
    '                    Next i
    '                    Me.proBar.Value = k
    '                Next k

    '                mySheet.Range("A1").Resize(rows, cols).Value = DataArray
    '                For p = 1 To cols
    '                    mySheet.Columns(p).EntireColumn.AutoFit()
    '                Next p
    '            Next q

    '            '页面设置
    '            'For i = 1 To myBook.Sheets.Count
    '            '    myBook.Sheets(i).Activate()
    '            '    With myBook.Sheets(i).PageSetup
    '            '        .PrintTitleRows = ""
    '            '        .PrintArea = ""
    '            '        .LeftHeader = ""
    '            '        .CenterHeader = NetInf.sNetName & TimeTablePara.sPubCurSkbName
    '            '        .RightHeader = "(车底)"
    '            '        .LeftFooter = ""
    '            '        .CenterFooter = "第&P页 共&N页"
    '            '        .RightFooter = SystemPara.sUserCompanyName
    '            '        .PrintHeadings = False
    '            '        .PrintGridlines = True
    '            '        '.Orientation = xlLandscape
    '            '        .Draft = False
    '            '        .PaperSize = xlPaperA4
    '            '        .FirstPageNumber = xlAutomatic
    '            '        .Zoom = 75
    '            '    End With
    '            'Next i
    '            myExcel.Visible = True
    '            Me.proBar.Visible = False
    '            myBook = Nothing
    '            mySheet = Nothing
    '            myExcel = Nothing
    '            Me.Cursor = Cursors.Default
    '            GC.Collect()
    '        Else
    '            MessageBox.Show("没有数据!", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        End If

    '    Catch exp As Exception
    '        MessageBox.Show("数据导出失败!请查看是否已经安装了Excel", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        Me.Cursor = Cursors.Default
    '    Finally

    '    End Try

    'End Sub

    'Private Sub btSeAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim i As Integer
    '    For i = 1 To Me.checkListsta.Items.Count
    '        Me.checkListsta.SetItemChecked(i - 1, True)
    '    Next
    'End Sub

    'Private Sub btNotSeAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim i As Integer
    '    For i = 1 To Me.checkListsta.Items.Count
    '        Me.checkListsta.SetItemChecked(i - 1, False)
    '    Next
    'End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles llbATS.LinkClicked
        Dim nPageNum As Integer
        nPageNum = Val(Me.txtCols.Text)
        If nPageNum >= 1 And nPageNum <= 250 Then
        Else
            MsgBox("每个工作表输出列数设置错误，请重输！")
            Exit Sub
        End If

        'Dim intIdx As Integer
        Dim i As Integer, j As Integer
        Dim p As Integer
        Dim Rows As Integer
        Dim Cols As Integer
        Dim iRows As Integer
        'Dim iColumns As Integer
        Dim nTrain As Integer
        Dim objExcel As Microsoft.Office.Interop.Excel.Application
        Dim myWorkbook As Microsoft.Office.Interop.Excel.Workbook, mySheets As Microsoft.Office.Interop.Excel.Sheets
        Dim nCurSTnSeqID As Integer
        Dim iColUp As Integer
        Dim iColDown As Integer
        Dim nIfPrint As Boolean
        objExcel = Nothing

        For i = 1 To UBound(SkbStnSeq)
            If SkbStnSeq(i).sQDName = Me.cmbQuDuanName.Text Then
                nCurSTnSeqID = i
                Exit For
            End If
        Next i


        iColUp = 2 '两列表头
        iColDown = 2
        Me.proBar.Maximum = 1000
        proBar.Value = 0
        proBar.Visible = True

        Try
            objExcel = GetObject("", "Excel.Application")
        Catch ex As Exception
            MsgBox("不能运行Excel，请检查是否安装了 Microsoft Excel 2003!", , "提示")
            Exit Sub
        End Try

        Me.Cursor = Cursors.WaitCursor
        ' objExcel.WindowState = Excel.xlMaximized '调试用最大化
        myWorkbook = objExcel.Workbooks.Add
        mySheets = myWorkbook.Sheets

        iRows = UBound(SkbStnSeq(nCurSTnSeqID).nStnSeq) '车站数


        '按列车到站点排序
        Dim ntmpTrain() As Integer

        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long
        ReDim ntmpTrain(0)
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                '   If TrainInf(i).sLineNum = "3" Then
                ReDim Preserve ntmpTrain(UBound(ntmpTrain) + 1)
                ntmpTrain(UBound(ntmpTrain)) = i
                'End If
            End If
        Next i
        '按到达时间排序
        Flag = 1
        k = UBound(ntmpTrain)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                If Me.cmbPaiXu.Text = "始发站" Then
                    TempTime1 = AddLitterTime(TrainInf(ntmpTrain(j)).Starting(TrainInf(ntmpTrain(j)).nPathID(1)))
                    Temptime2 = AddLitterTime(TrainInf(ntmpTrain(j + 1)).Starting(TrainInf(ntmpTrain(j + 1)).nPathID(1)))
                Else
                    TempTime1 = AddLitterTime(GetStartTimeFromStaName(Me.cmbPaiXu.Text, ntmpTrain(j)))
                    Temptime2 = AddLitterTime(GetStartTimeFromStaName(Me.cmbPaiXu.Text, ntmpTrain(j + 1)))
                End If
                If TempTime1 > Temptime2 Then '
                    temp = ntmpTrain(j)
                    ntmpTrain(j) = ntmpTrain(j + 1)
                    ntmpTrain(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop

        Rows = iRows * 2 + 2
        Cols = 254
        Dim DataArray(Rows, Cols) As String

        Me.proBar.Value = 50

        '表格格式设置()
        ' 出库、入库
        mySheets("Sheet1").Name = "出库"
        mySheets("出库").Activate()
        DataArray(0, 0) = "站名/车次"
        mySheets("出库").Columns("A:A").ColumnWidth = 10
        mySheets("出库").Columns("B:B").ColumnWidth = 6

        With mySheets("出库").Cells
            .Font.size = 11
            .HorizontalAlignment = xlCenter
            .VerticalAlignment = xlCenter
            .NumberFormatLocal = "@"
            For i = 1 To iRows
                DataArray(2 * i - 1, 0) = StationInf(SkbStnSeq(nCurSTnSeqID).nStnSeq(i)).sPrintStaName
                DataArray(2 * i, 0) = StationInf(SkbStnSeq(nCurSTnSeqID).nStnSeq(i)).sPrintStaName
                'mySheets("出库").Range("A" & Trim(Str(2 * i)) & ":" & "A" & Trim(Str(2 * i + 1))).Merge()
                'mySheets("出库").Range("B" & Trim(Str(2 * i)) & ":" & "B" & Trim(Str(2 * i + 1))).Merge()
                DataArray(2 * i - 1, 1) = "到"
                DataArray(2 * i, 1) = "发"
            Next i
        End With

        Me.proBar.Value = 100
        Dim nCurCol As Integer
        nCurCol = 2
        For i = 1 To UBound(ntmpTrain) '先输出出库车
            nTrain = ntmpTrain(i)
            If TrainInf(nTrain).TrainStyle = "出库车" Then
                nIfPrint = IFCurTrainNotTimeInCurDitu(nTrain, nCurSTnSeqID)
                If nIfPrint = True Then
                    nCurCol = nCurCol + 1
                    DataArray(0, nCurCol - 1) = TrainInf(nTrain).sPrintTrain
                    For j = 1 To iRows
                        DataArray(j * 2 - 1, nCurCol - 1) = dTime(TrainInf(nTrain).Arrival(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                        DataArray(j * 2, nCurCol - 1) = dTime(TrainInf(nTrain).Starting(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                        If Me.chkTimeFormate.Checked = True Then
                            mySheets("出库").Cells(j * 2, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                            mySheets("出库").Cells(j * 2 + 1, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                        End If
                    Next j
                End If
            End If
        Next i

        mySheets("出库").Range("A1").Resize(Rows, Cols).Value = DataArray

        Me.proBar.Value = 150

        ReDim DataArray(Rows, Cols)
        mySheets("Sheet2").Name = "入库"
        mySheets("入库").Activate()
        DataArray(0, 0) = "站名/车次"
        mySheets("入库").Columns("A:A").ColumnWidth = 10
        mySheets("入库").Columns("B:B").ColumnWidth = 6
        With mySheets("入库").Cells
            .Font.size = 11
            .HorizontalAlignment = xlCenter
            .VerticalAlignment = xlCenter
            .NumberFormatLocal = "@"
            For i = 1 To iRows
                DataArray(2 * i - 1, 0) = StationInf(SkbStnSeq(nCurSTnSeqID).nStnSeq(i)).sPrintStaName
                'mySheets("入库").Range("A" & Trim(Str(2 * i)) & ":" & "A" & Trim(Str(2 * i + 1))).Merge()
                ' mySheets("入库").Range("B" & Trim(Str(2 * i)) & ":" & "B" & Trim(Str(2 * i + 1))).Merge()
                DataArray(2 * i - 1, 1) = "到"
                DataArray(2 * i, 1) = "发"
            Next i
        End With

        nCurCol = 2
        For i = 1 To UBound(ntmpTrain)  '输出入库车
            nTrain = ntmpTrain(i)
            If TrainInf(nTrain).TrainStyle = "入库车" Then
                nIfPrint = IFCurTrainNotTimeInCurDitu(nTrain, nCurSTnSeqID)
                If nIfPrint = True Then
                    nCurCol = nCurCol + 1
                    DataArray(0, nCurCol - 1) = TrainInf(nTrain).sPrintTrain
                    For j = 1 To iRows
                        DataArray(j * 2 - 1, nCurCol - 1) = dTime(TrainInf(nTrain).Arrival(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                        DataArray(j * 2, nCurCol - 1) = dTime(TrainInf(nTrain).Starting(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                        If Me.chkTimeFormate.Checked = True Then
                            mySheets("入库").Cells(j * 2, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                            mySheets("入库").Cells(j * 2 + 1, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                        End If
                    Next j
                End If
            End If
        Next i
        mySheets("入库").Range("A1").Resize(Rows, Cols).Value = DataArray


        Dim nDownNum As Integer
        Dim nUpNum As Integer
        Dim nDownPage As Integer
        Dim nUpPage As Integer

        nUpNum = 0
        nDownNum = 0
        Dim sCurDownName As String
        Dim sCurUpName As String
        Dim tmpK As Integer
        Dim NTolNum As Integer
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" And (i Mod 2 = 0) Then
                nUpNum = nUpNum + 1
            End If
        Next i

        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" And (i Mod 2 <> 0) Then
                nDownNum = nDownNum + 1
            End If
        Next i

        Me.proBar.Value = 200
        nUpPage = GetMaxZhenShu(nUpNum / nPageNum)
        nDownPage = GetMaxZhenShu(nDownNum / nPageNum)
        NTolNum = 0
        For p = 1 To nDownPage
            mySheets.Add()
            ReDim DataArray(Rows, Cols)
            sCurDownName = "下行" & p
            mySheets("Sheet" & p + 3).Name = sCurDownName
            mySheets(sCurDownName).Activate()
            DataArray(0, 0) = "站名/车次"
            mySheets(sCurDownName).Columns("A:A").ColumnWidth = 10
            mySheets(sCurDownName).Columns("B:B").ColumnWidth = 3

            With mySheets(sCurDownName).Cells
                .Font.size = 11
                .HorizontalAlignment = xlCenter
                .VerticalAlignment = xlCenter
                .NumberFormatLocal = "@"
                For j = 1 To iRows
                    DataArray(2 * j - 1, 0) = StationInf(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)).sPrintStaName
                    DataArray(2 * j, 0) = StationInf(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)).sPrintStaName
                    'mySheets(sCurDownName).Range("A" & Trim(Str(2 * j)) & ":" & "A" & Trim(Str(2 * j + 1))).Merge()
                    DataArray(2 * j - 1, 1) = "到"
                    DataArray(2 * j, 1) = "发"
                Next j
            End With

            nCurCol = 2
            tmpK = 0
            For i = 1 To UBound(ntmpTrain)  '输出下行车
                nTrain = ntmpTrain(i)
                If TrainInf(nTrain).TrainStyle = "运行车" Or TrainInf(nTrain).TrainStyle = "环形车" Then
                    If nTrain Mod 2 <> 0 Then
                        nIfPrint = IFCurTrainNotTimeInCurDitu(nTrain, nCurSTnSeqID)
                        If nIfPrint = True Then
                            tmpK = tmpK + 1
                            nCurCol = 3 + ((tmpK - 1) Mod nPageNum)
                            If GetMaxZhenShu(tmpK / nPageNum) = p Then
                                sCurDownName = "下行" & GetMaxZhenShu(tmpK / nPageNum)
                                DataArray(0, nCurCol - 1) = TrainInf(nTrain).sPrintTrain
                                For j = 1 To iRows
                                    DataArray(j * 2 - 1, nCurCol - 1) = dTime(TrainInf(nTrain).Arrival(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                                    DataArray(j * 2, nCurCol - 1) = dTime(TrainInf(nTrain).Starting(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                                    If Me.chkTimeFormate.Checked = True Then
                                        mySheets(sCurDownName).Cells(j * 2 + 1, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                                        mySheets(sCurDownName).Cells(j * 2, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                                    End If
                                Next j
                                Me.proBar.Value = 200 + Int(400.0# * NTolNum / nDownNum)
                                NTolNum = NTolNum + 1
                            End If
                        End If
                    End If
                End If
            Next i
            mySheets(sCurDownName).Range("A1").Resize(Rows, Cols).Value = DataArray
        Next p

        NTolNum = 0
        For p = 1 To nUpPage
            mySheets.Add()
            ReDim DataArray(Rows, Cols)
            sCurUpName = "上行" & p
            mySheets("Sheet" & p + 3 + nDownPage).Name = sCurUpName
            mySheets(sCurUpName).Activate()
            DataArray(0, 0) = "站名/车次"
            mySheets(sCurUpName).Columns("A:A").ColumnWidth = 10
            mySheets(sCurUpName).Columns("B:B").ColumnWidth = 3
            With mySheets(sCurUpName).Cells
                .Font.size = 11
                .HorizontalAlignment = xlCenter
                .VerticalAlignment = xlCenter
                .NumberFormatLocal = "@"
                For j = 1 To iRows
                    DataArray(2 * j - 1, 0) = StationInf(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)).sPrintStaName
                    DataArray(2 * j, 0) = StationInf(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)).sPrintStaName
                    'mySheets(sCurUpName).Range("A" & Trim(Str(2 * j)) & ":" & "A" & Trim(Str(2 * j + 1))).Merge()
                    DataArray(2 * j - 1, 1) = "到"
                    DataArray(2 * j, 1) = "发"
                Next j
            End With

            nCurCol = 2
            tmpK = 0
            For i = 1 To UBound(ntmpTrain)  '输出上行车
                nTrain = ntmpTrain(i)
                If TrainInf(nTrain).TrainStyle = "运行车" Or TrainInf(nTrain).TrainStyle = "环形车" Then
                    If nTrain Mod 2 = 0 Then
                        nIfPrint = IFCurTrainNotTimeInCurDitu(nTrain, nCurSTnSeqID)
                        If nIfPrint = True Then
                            tmpK = tmpK + 1
                            nCurCol = 3 + ((tmpK - 1) Mod nPageNum)
                            If GetMaxZhenShu(tmpK / nPageNum) = p Then
                                sCurUpName = "上行" & GetMaxZhenShu(tmpK / nPageNum)
                                DataArray(0, nCurCol - 1) = TrainInf(nTrain).sPrintTrain
                                For j = 1 To iRows
                                    DataArray(j * 2 - 1, nCurCol - 1) = dTime(TrainInf(nTrain).Starting(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                                    DataArray(j * 2, nCurCol - 1) = dTime(TrainInf(nTrain).Arrival(SkbStnSeq(nCurSTnSeqID).nStnSeq(j)), 0)
                                    If Me.chkTimeFormate.Checked = True Then
                                        mySheets(sCurUpName).Cells(j * 2, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                                        mySheets(sCurUpName).Cells(j * 2 + 1, nCurCol).NumberFormatLocal = "h:mm:ss;@"
                                    End If
                                Next j
                                Me.proBar.Value = 600 + Int(400.0# * NTolNum / nUpNum)
                                NTolNum = NTolNum + 1
                            End If
                        End If
                    End If
                End If
            Next i
            mySheets(sCurUpName).Range("A1").Resize(Rows, Cols).Value = DataArray
        Next p


        '页面设置
        'For i = 1 To mySheets.Count
        '    mySheets(1).Activate()
        '    With mySheets(i).PageSetup
        '        .PrintTitleRows = ""
        '        .PrintTitleColumns = "A:B"
        '        .PrintArea = ""
        '        .LeftHeader = ""
        '        .CenterHeader = NetInf.sNetName & "运行图时刻表"
        '        .RightHeader = ""
        '        .LeftFooter = ""
        '        .CenterFooter = "第&P页 共&N页"
        '        .RightFooter = SystemPara.sUserCompanyName
        '        .PrintHeadings = False
        '        .PrintGridlines = True
        '        '.Orientation = xlLandscape
        '        .Draft = False
        '        .PaperSize = xlPaperA4
        '        .FirstPageNumber = xlAutomatic
        '        .Zoom = 80
        '    End With
        'Next i
        Me.proBar.Visible = False
        objExcel.Visible = True
        objExcel = Nothing
        myWorkbook = Nothing
        mySheets = Nothing
        Me.Cursor = Cursors.Default
    End Sub
End Class