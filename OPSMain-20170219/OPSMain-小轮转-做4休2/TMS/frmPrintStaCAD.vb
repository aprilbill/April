Public Class frmPrintStaCAD
    Dim intCurPrintPage As Integer
    Dim intCurStaPage As Integer
    Dim intCurStaID As Integer
    Private Sub frmPrintStaCAD_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.lstBei.Items.Clear()
        Dim i As Integer
        For i = 1 To UBound(CADStaInf)
            Me.lstBei.Items.Add(CADStaInf(i).sStaName)
        Next i
        intCurPrintPage = 0
        intCurStaPage = 0
        intCurStaID = 0
        Me.txtSmallTitle.Text = SystemPara.sUserCompanyName
        Me.txtLineSmalltitle.Text = SystemPara.sUserCompanyName
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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Dim dialog As New PrintDialog()
        dialog.Document = printDocDiagram

        If dialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            printDocDiagram.Print()
        End If
    End Sub

    Private Sub printDocDiagram_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles printDocDiagram.PrintPage
        intCurPrintPage = intCurPrintPage + 1
        intCurStaPage = intCurStaPage + 1
        Dim newWidth As Single
        Dim newHeight As Single
        Dim IfPrintGuDaoNum As Boolean
        Dim IfPrintTrackNum As Boolean
        Dim IfPrintCorssNum As Boolean
        Dim ifPrintShotLine As Boolean
        Dim nLeftBlank As Single
        Dim nTopBlank As Single
        Dim nTolPageNum As Integer
        Dim PrintBigTitle As String
        Dim sCurSta As String
        Dim tmpFont As Font
        Dim tmpBrush As Brush
        Dim txtWidth As Single
        Dim ntopY As Single
        If Me.lstSta.Items.Count > 0 Then
            nTolPageNum = Me.lstSta.Items.Count
            IfPrintGuDaoNum = Me.chkGudao.Checked
            IfPrintTrackNum = Me.chkTackNum.Checked
            IfPrintCorssNum = Me.chkCrossing.Checked
            ifPrintShotLine = Me.chkShortLine.Checked
            If Me.printDocDiagram.DefaultPageSettings.Landscape = True Then
                newWidth = Me.printDocDiagram.DefaultPageSettings.PaperSize.Height
                newHeight = Me.printDocDiagram.DefaultPageSettings.PaperSize.Width
            Else
                newWidth = Me.printDocDiagram.DefaultPageSettings.PaperSize.Width
                newHeight = Me.printDocDiagram.DefaultPageSettings.PaperSize.Height
            End If
            nLeftBlank = 120 ' Me.printDocDiagram.DefaultPageSettings.Margins.Left
            nTopBlank = 120 ' Me.printDocDiagram.DefaultPageSettings.Margins.Top
            ntopY = 120
            sCurSta = Me.lstSta.Items(intCurStaID).ToString.Trim
            PrintBigTitle = sCurSta & " 车站平面图"
            tmpBrush = New SolidBrush(Me.labbackColor.BackColor)
            e.Graphics.FillRectangle(tmpBrush, 0, 0, newWidth, newHeight)
            tmpFont = New Font("黑体", 20)
            tmpBrush = Brushes.SlateGray
            txtWidth = e.Graphics.MeasureString(PrintBigTitle, tmpFont).Width
            e.Graphics.DrawString(PrintBigTitle, tmpFont, tmpBrush, (newWidth - txtWidth) / 2, nTopBlank)
            tmpFont = New Font("宋体", 10)
            tmpBrush = Brushes.Blue
            txtWidth = e.Graphics.MeasureString(Me.txtSmallTitle.Text, tmpFont).Width
            e.Graphics.DrawString(Me.txtSmallTitle.Text, tmpFont, tmpBrush, newWidth - txtWidth - nLeftBlank, nTopBlank)
            Call PrintStaCADStation(sCurSta, e.Graphics, 0, nLeftBlank, ntopY, nTopBlank, newWidth, newHeight, IfPrintGuDaoNum, IfPrintTrackNum, IfPrintCorssNum, ifPrintShotLine)
            If intCurPrintPage >= nTolPageNum Then
                e.HasMorePages = False
                intCurPrintPage = 0
                intCurStaPage = 0
                intCurStaID = 0
            Else
                e.HasMorePages = True
                intCurStaID = intCurStaID + 1
            End If
            'Next
        Else
            MsgBox("请先选择车站名！")
            Exit Sub
        End If

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

    Private Sub btnLinePrintSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLinePrintSet.Click
        Dim psd As New PageSetupDialog()
        With psd
            .Document = PrintDocDiagramLine
            .PageSettings = PrintDocDiagramLine.DefaultPageSettings
        End With

        If psd.ShowDialog = Windows.Forms.DialogResult.OK Then
            PrintDocDiagramLine.DefaultPageSettings = psd.PageSettings
        End If
    End Sub

    Private Sub btnLinePrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLinePrint.Click
        Dim dialog As New PrintDialog()
        dialog.Document = PrintDocDiagramLine

        If dialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            PrintDocDiagramLine.Print()
        End If
    End Sub

    Private Sub btnLinePrintPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLinePrintPreview.Click
        Dim ppd As New PrintPreviewDialog()

        Try
            ppd.Document = PrintDocDiagramLine
            ppd.ShowDialog()
        Catch exp As Exception
            MessageBox.Show("An error occurred while trying to load the " & _
                "document for Print Preview. Make sure you currently have " & _
                "access to a printer. A printer must be connected and " & _
                "accessible for Print Preview to work.", Me.Text, _
                 MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub printDocDiagramLine_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocDiagramLine.PrintPage
        intCurPrintPage = intCurPrintPage + 1
        intCurStaPage = intCurStaPage + 1
        Dim newWidth As Single
        Dim newHeight As Single
        Dim IfPrintGuDaoNum As Boolean
        Dim IfPrintTrackNum As Boolean
        Dim IfPrintCorssNum As Boolean
        Dim ifPrintShotLine As Boolean
        Dim nLeftBlank As Single
        Dim nTopBlank As Single
        Dim nTolPageNum As Integer
        Dim PrintBigTitle As String
        Dim nLineWidth As Single
        Dim tmpFont As Font
        Dim tmpBrush As Brush
        Dim txtWidth As Single
        Dim ntopY As Single
        nTolPageNum = 1
        IfPrintGuDaoNum = Me.chkLineGuDao.Checked
        IfPrintTrackNum = Me.chkLineTrack.Checked
        IfPrintCorssNum = Me.chkLineCross.Checked
        ifPrintShotLine = Me.chkLineShortLine.Checked
        If Me.PrintDocDiagramLine.DefaultPageSettings.Landscape = True Then
            newWidth = Me.PrintDocDiagramLine.DefaultPageSettings.PaperSize.Height
            newHeight = Me.PrintDocDiagramLine.DefaultPageSettings.PaperSize.Width
        Else
            newWidth = Me.PrintDocDiagramLine.DefaultPageSettings.PaperSize.Width
            newHeight = Me.PrintDocDiagramLine.DefaultPageSettings.PaperSize.Height
        End If
        nLineWidth = Me.NumLineWidth.Value
        nLeftBlank = 100 ' Me.printDocDiagram.DefaultPageSettings.Margins.Left
        nTopBlank = 100 ' Me.printDocDiagram.DefaultPageSettings.Margins.Top
        ntopY = 120
        PrintBigTitle = " 线路平面图"
        tmpBrush = New SolidBrush(Me.labLineBackColor.BackColor)
        e.Graphics.FillRectangle(tmpBrush, 0, 0, newWidth, newHeight)
        tmpFont = New Font("黑体", 20)
        tmpBrush = Brushes.SlateGray
        txtWidth = e.Graphics.MeasureString(PrintBigTitle, tmpFont).Width
        e.Graphics.DrawString(PrintBigTitle, tmpFont, tmpBrush, (newWidth - txtWidth) / 2, nTopBlank)
        tmpFont = New Font("宋体", 10)
        tmpBrush = Brushes.Blue
        txtWidth = e.Graphics.MeasureString(Me.txtLineSmalltitle.Text, tmpFont).Width
        e.Graphics.DrawString(Me.txtLineSmalltitle.Text, tmpFont, tmpBrush, newWidth - txtWidth - nLeftBlank, nTopBlank)
        Call DrawAllStationCAD(e.Graphics, newWidth, newHeight, nLeftBlank, nTopBlank, ntopY, nLineWidth, Color.DarkGray, ifPrintShotLine, IfPrintGuDaoNum, IfPrintTrackNum, IfPrintCorssNum)
        If intCurPrintPage >= nTolPageNum Then
            e.HasMorePages = False
            intCurPrintPage = 0
            intCurStaPage = 0
            intCurStaID = 0
        Else
            e.HasMorePages = True
            intCurStaID = intCurStaID + 1
        End If
    End Sub

    '   画全线图
    Public Sub DrawAllStationCAD(ByVal rBmpGraphics1 As Graphics, ByVal sngWidth As Single, ByVal sngHeight As Single, ByVal priLeftBlank As Single, ByVal priTopYblank As Single, _
        ByVal ntopY As Single, ByVal nLineWidth As Single, ByVal cForColor As Color, _
         ByVal IfShowOtherInf As Boolean, ByVal IfPrintGuDaoNum As Boolean, ByVal IfPrintTrackNum As Boolean, ByVal IfPrintCorssNum As Boolean)
        '画全线图
        'Dim tmpPen As Pen
        'tmpPen = New Pen(Color.White, nLineWidth)
        Dim nLineScale As Single
        Dim nWidthScale As Single
        Dim nheightScale As Single
        Dim minX As Single
        Dim maxX As Single
        Dim minY As Single
        Dim maxY As Single
        Dim LineLeftX As Single
        Dim LineTopY As Single
        sngWidth = sngWidth - priLeftBlank * 2
        sngHeight = sngHeight - priTopYblank * 2 - ntopY

        minX = 1000000
        maxX = -10000000
        Dim i As Integer
        For i = 1 To UBound(TrackInf)
            minX = Minimal(minX, TrackInf(i).X1)
            minX = Minimal(minX, TrackInf(i).X2)
            maxX = Maximal(maxX, TrackInf(i).X1)
            maxX = Maximal(maxX, TrackInf(i).X2)
            minY = Minimal(minY, TrackInf(i).Y1)
            minY = Minimal(minY, TrackInf(i).Y2)
            maxY = Maximal(maxY, TrackInf(i).Y1)
            maxY = Maximal(maxY, TrackInf(i).Y2)
        Next
        If (maxX - minX) > 0 Then
            nWidthScale = (sngWidth / (maxX - minX))
        End If
        If (maxY - minY) > 0 Then
            nheightScale = (sngHeight / (maxY - minY))
        End If
        nLineScale = Minimal(nWidthScale, nheightScale)
        LineLeftX = (sngWidth - (maxX - minX) * nLineScale) / 2 - minX * nLineScale + priLeftBlank
        LineTopY = (sngHeight - (maxY - minY) * nLineScale) / 2 - minY * nLineScale + priTopYblank + ntopY
        Call PrintTrackInLinePic(rBmpGraphics1, nLineWidth, nLineScale, -LineLeftX, -LineTopY, cForColor, IfShowOtherInf, IfPrintGuDaoNum, IfPrintTrackNum, IfPrintCorssNum)
        'If IFDrawReangle = True Then
        '    rBmpGraphics1.DrawRectangle(New Pen(Color.Yellow, 1), 2, 2, picLine.Width - 4, picLine.Height - 4)
        'End If
        'picLine.Image = rBmp1
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub btnTimeLineColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTimeLineColor.Click
        Dim dColor As New ColorDialog
        dColor.Color = Me.labbackColor.BackColor
        If dColor.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.labbackColor.BackColor = dColor.Color
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLineBackColor.Click
        Dim dColor As New ColorDialog
        dColor.Color = Me.labLineBackColor.BackColor
        If dColor.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.labLineBackColor.BackColor = dColor.Color
        End If
    End Sub
End Class