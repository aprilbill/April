Public Class frmShowLinePic
    Public sCurStaName As String
    Public sCurLineName As String
    Public nShowState As Integer
    Private Sub frmTest_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case nShowState
            Case 0
                Me.Pic.Width = 600
                Me.Pic.Height = 400
            Case 1
                Me.Pic.Width = 2000
                Me.Pic.Height = 500
        End Select
        Call ShowLineAndSta()
    End Sub

    '显示车站与线路图
    Private Sub ShowLineAndSta()
        'nState=0表示显示车站，1表示显示线路
        Dim rBmp As Bitmap '画图临时保存的图像
        Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
        rBmp = New Bitmap(Me.Pic.Width, Me.Pic.Height)
        rBmpGraphics = Graphics.FromImage(rBmp)
        rBmpGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias
        Select Case nShowState
            Case 0
                Call PrintStaCADStation(sCurStaName, rBmpGraphics, 50, 10, 10, 10, Me.Pic.Width, Me.Pic.Height, True, True, True, False)
            Case 1
                Call DrawLinePicture(rBmpGraphics, Me.Pic.Width, Me.Pic.Height)
        End Select
        Me.Pic.Image = rBmp
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBig.Click
        Me.Pic.Width = Me.Pic.Width + 200
        Me.Pic.Height = Me.Pic.Height + 200
        If Me.Pic.Width > 5000 Then
            Me.Pic.Width = 5000
        End If
        If Me.Pic.Height > 5000 Then
            Me.Pic.Height = 5000
        End If
        Call ShowLineAndSta()
    End Sub

    Private Sub btnSmall_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSmall.Click
        Me.Pic.Width = Me.Pic.Width - 200
        Me.Pic.Height = Me.Pic.Height - 200
        If Me.Pic.Width < 200 Then
            Me.Pic.Width = 200
        End If
        If Me.Pic.Height < 200 Then
            Me.Pic.Height = 200
        End If
        Call ShowLineAndSta()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSavePic.Click
        Dim strPath As String
        Dim New0penFile As New SaveFileDialog
        New0penFile.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|jpeg files (*.jpeg)|*.jpeg|All files (*.*)|*.*"
        New0penFile.FilterIndex = 1
        New0penFile.RestoreDirectory = True
        strPath = ""
        If New0penFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strPath = New0penFile.FileName
            Me.Pic.Image.Save(strPath)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim dialog As New PrintDialog()
        dialog.Document = printDocDiagram

        If dialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            printDocDiagram.Print()
        End If
    End Sub

    Private Sub printDocDiagram_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles printDocDiagram.PrintPage
        Dim rBmpGraphics As Graphics
        rBmpGraphics = e.Graphics
        rBmpGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias
        Dim newWidth, newHeight As Integer
        If Me.printDocDiagram.DefaultPageSettings.Landscape = True Then
            newWidth = Me.printDocDiagram.DefaultPageSettings.PaperSize.Height
            newHeight = Me.printDocDiagram.DefaultPageSettings.PaperSize.Width
        Else
            newWidth = Me.printDocDiagram.DefaultPageSettings.PaperSize.Width
            newHeight = Me.printDocDiagram.DefaultPageSettings.PaperSize.Height
        End If

        Select Case nShowState
            Case 0
                Call PrintStaCADStation(sCurStaName, rBmpGraphics, 10, 10, 10, 10, newWidth, newHeight, True, True, True, False)
            Case 1
                Call DrawLinePicture(rBmpGraphics, newWidth, newHeight)
        End Select
    End Sub

    Private Sub btnPrintView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintView.Click
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

    Private Sub btnPrintSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintSet.Click
        Dim psd As New PageSetupDialog()
        With psd
            .Document = printDocDiagram
            .PageSettings = printDocDiagram.DefaultPageSettings
        End With

        If psd.ShowDialog = Windows.Forms.DialogResult.OK Then
            printDocDiagram.DefaultPageSettings = psd.PageSettings
        End If
    End Sub
End Class