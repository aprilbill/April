Public Class frmShowErrorInfor

    'Private Sub frmShowErrorInfor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    Dim i As Integer
    '    Dim lvItem As ListViewItem
    '    lvItem = Nothing
    '    Dim liFile(4) As String
    '    For i = 1 To UBound(TrainErrInf)
    '        liFile(0) = i
    '        liFile(1) = TrainInf(TrainErrInf(i).nTrain).Train
    '        liFile(2) = TrainErrInf(i).nTrain
    '        liFile(3) = TrainErrInf(i).Scurtime
    '        liFile(4) = TrainErrInf(i).sErrorMessage
    '        lvItem = New ListViewItem(liFile)
    '        Me.listViewTrain.Items.Add(lvItem)
    '    Next
    'End Sub

    'Private Sub listViewTrain_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles listViewTrain.DoubleClick
    '    TimeTablePara.nPubTrain = Me.listViewTrain.SelectedItems(0).SubItems(2).Text
    '    If TimeTablePara.nPubTrain > 0 Then
    '        Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
    '        TimeTablePara.picPubDiagram.Refresh()
    '        rBmpGraphics = TimeTablePara.picPubDiagram.CreateGraphics()
    '        Dim tmpPen As Pen
    '        tmpPen = New Pen(Color.SpringGreen, 2)
    '        Call DrawLineInPicture(TimeTablePara.nPubTrain, rBmpGraphics, tmpPen, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX, TimeTablePara.TimeTableDiagramPara.nifPrintCheCi)
    '        Call ShowLabInfor(TimeTablePara.nPubTrain, frmTimeTableMain.labInfor)
    '        frmTimeTableMain.nIFShowAllCheDiTrain = 0
    '        Call listTrainInMiddlePic(TimeTablePara.nPubTrain)
    '    Else
    '        TimeTablePara.picPubDiagram.Refresh()
    '    End If
    'End Sub
End Class