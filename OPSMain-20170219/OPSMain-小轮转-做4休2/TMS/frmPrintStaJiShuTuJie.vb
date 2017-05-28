Public Class frmPrintStaJiShuTuJie
    Dim intCurPrintPage As Integer
    Dim intCurStaPage As Integer
    Dim intCurStaID As Integer
    Private Sub frmPrintStaJiShuTuJie_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.lstBei.Items.Clear()
        Dim i As Integer
        For i = 1 To UBound(NotSameStationInf)
            Me.lstBei.Items.Add(NotSameStationInf(i))
        Next i
        intCurPrintPage = 0
        intCurStaPage = 0
        intCurStaID = 0
        Me.txtBigTitle.Text = TimeTablePara.sPubCurSkbName
        Me.txtSmallTitle.Text = SystemPara.sUserCompanyName
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
        Dim intEveryPageNum As Integer
        Dim PrintBigTitle As String
        Dim ntopY As Single
        Dim strTimeFormate As String
        ' Dim i As Integer
        If Me.lstSta.Items.Count > 0 Then
            ' For i = 1 To Me.lstSta.Items.Count
            intBeTime = Me.numBeTime.Value
            intEndTime = Me.numEndTime.Value
            strTimeFormate = Me.cmbTimeFormat.Text.Trim
            If intEndTime <= intBeTime Then
                intEndTime = intEndTime + 24
            End If
            intPageTime = Me.numPageTime.Value
            If (intEndTime - intBeTime) / intPageTime = Int((intEndTime - intBeTime) / intPageTime) Then
                intEveryPageNum = (intEndTime - intBeTime) / intPageTime
            Else
                intEveryPageNum = Int((intEndTime - intBeTime) / intPageTime) + 1
            End If
            If intCurStaPage > intEveryPageNum Then
                intCurStaPage = 1
                intCurStaID = intCurStaID + 1
            End If
            nTolPageNum = intEveryPageNum * Me.lstSta.Items.Count
            nStaBlank = TimeTablePara.TimeTableDiagramPara.sngStaBlank
            nTopTitleHeight = 20
            nTimeBlank = 10
            nLeftX = 0
            ntopY = 20

            If Me.printDocDiagram.DefaultPageSettings.Landscape = True Then
                newWidth = Me.printDocDiagram.DefaultPageSettings.PaperSize.Height
                newHeight = Me.printDocDiagram.DefaultPageSettings.PaperSize.Width
            Else
                newWidth = Me.printDocDiagram.DefaultPageSettings.PaperSize.Width
                newHeight = Me.printDocDiagram.DefaultPageSettings.PaperSize.Height
            End If
            nLeftBlank = 100 ' Me.printDocDiagram.DefaultPageSettings.Margins.Left
            nTopBlank = 200 ' Me.printDocDiagram.DefaultPageSettings.Margins.Top
            newWidth = newWidth '- nLeftBlank ' * 2
            newHeight = newHeight - nTopBlank * 2 - nTopTitleHeight
            Dim bigFont As Font
            bigFont = New Font("黑体", 20)
            PrintBigTitle = Me.txtBigTitle.Text.Trim & " " & Me.lstSta.Items(intCurStaID).ToString.Trim & " 股道使用图解"
            Dim txtWidth As Single
            txtWidth = e.Graphics.MeasureString(PrintBigTitle, bigFont).Width
            e.Graphics.DrawString(PrintBigTitle, bigFont, Brushes.Blue, newWidth / 2 - txtWidth / 2, 60 + ntopY)
            e.Graphics.DrawString(DateTime.Now.ToString, New Font("宋体", 10), Brushes.Blue, 100, 60 + ntopY)
            e.Graphics.DrawString(Me.txtSmallTitle.Text, New Font("宋体", 10), Brushes.Blue, newWidth - Me.txtSmallTitle.Text.Length * 11 - nLeftBlank, 60 + ntopY)
            Dim nFirTime As Integer
            nFirTime = (intBeTime + (intCurStaPage - 1) * intPageTime) Mod 24
            Dim nSta As Integer
            Dim nGuDaoHeight As Single
            nGuDaoHeight = TimeTablePara.StaDiagramePara.nStaLineHeight
            nSta = StaNameToStaInfID(Me.lstSta.Items(intCurStaID).ToString.Trim)
            Call InputGuDaoYData(nSta, nGuDaoHeight, nTopBlank, ntopY)
            Call DrawGuDaoJiShuTuJie(nSta, e.Graphics, Nothing, Nothing, newWidth, newHeight, nLeftBlank, nTopBlank, nStaBlank, nTimeBlank, nLeftX, nTopTitleHeight, nFirTime, intPageTime, nGuDaoHeight, "打印", strTimeFormate)
            ' Call DrawJiShuTuJieDiagramLine(e.Graphics, newWidth, nFirTime, intPageTime, nLeftBlank, nStaBlank, nLeftX)
            If intCurPrintPage = nTolPageNum Then
                e.HasMorePages = False
                intCurPrintPage = 0
                intCurStaPage = 0
                intCurStaID = 0
            Else
                e.HasMorePages = True
            End If
            'Next
        Else
            MsgBox("请先选择车站名！")
            Exit Sub
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class