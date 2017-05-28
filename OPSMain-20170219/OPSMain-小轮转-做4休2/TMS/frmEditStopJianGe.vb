Public Class frmEditStopJianGe
    Public nCurSta As Integer
    Public nCurTrain As Integer

    Private Sub frmEditStopJianGe_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.labCurTrain.Text = "当前车次：" & TrainInf(nCurTrain).sPrintTrain
        Me.labCurTrip.Text = "列车交路：" & TrainInf(nCurTrain).sJiaoLuName
        Dim i As Integer
        Me.CmbStartSta.Items.Clear()
        Me.cmbEndSta.Items.Clear()
        For i = 1 To UBound(TrainInf(nCurTrain).nPathID)
            Me.CmbStartSta.Items.Add(StationInf(TrainInf(nCurTrain).nPathID(i)).sStationName)
            Me.cmbEndSta.Items.Add(StationInf(TrainInf(nCurTrain).nPathID(i)).sStationName)
        Next

        If Me.optStart.Checked = True Then '始发调匀
            Me.CmbStartSta.Text = TrainInf(nCurTrain).ComeStation
            Me.cmbEndSta.Text = StationInf(nCurSta).sPrintStaName

            '   If TrainInf(nCurTrain).TrainStyle = "环形车" Then
            Dim nNextTrain As Integer
            Dim sZheFantime As Integer
            Dim nZFTime As Integer
            Dim DeltaTime As Integer
            nNextTrain = TrainInf(nCurTrain).TrainReturn(1)
            If nNextTrain <> 0 Then
                nZFTime = TimeMinus(TrainInf(nCurTrain).Starting(TrainInf(nCurTrain).nPathID(1)), TrainInf(nNextTrain).Arrival(TrainInf(nNextTrain).nPathID(UBound(TrainInf(nNextTrain).nPathID))))
                sZheFantime = GetZheFanTime(TrainInf(nNextTrain).SCheDiLeiXing, StationInf(TrainInf(nNextTrain).nPathID(UBound(TrainInf(nNextTrain).nPathID))).sStationName, TrainInf(nNextTrain).TrainReturnStyle(2))
                DeltaTime = nZFTime - sZheFantime
                Me.numTime.Value = DeltaTime
            End If
            'End If

        Else
            Me.CmbStartSta.Text = StationInf(nCurSta).sPrintStaName
            Me.cmbEndSta.Text = TrainInf(nCurTrain).NextStation

            ' If TrainInf(nCurTrain).TrainStyle = "环形车" Then
            Dim nNextTrain As Integer
            Dim sZheFantime As Integer
            Dim nZFTime As Integer
            Dim DeltaTime As Integer
            nNextTrain = TrainInf(nCurTrain).TrainReturn(2)
            If nNextTrain <> 0 Then
                nZFTime = TimeMinus(TrainInf(nNextTrain).Starting(TrainInf(nNextTrain).nPathID(1)), TrainInf(nCurTrain).Arrival(TrainInf(nCurTrain).nPathID(UBound(TrainInf(nCurTrain).nPathID))))
                sZheFantime = GetZheFanTime(TrainInf(nCurTrain).SCheDiLeiXing, StationInf(TrainInf(nCurTrain).nPathID(UBound(TrainInf(nCurTrain).nPathID))).sStationName, TrainInf(nCurTrain).TrainReturnStyle(2))
                DeltaTime = nZFTime - sZheFantime
                Me.numTime.Value = DeltaTime
            End If
            'End If

        End If
        Me.grdTime.Rows.Clear()
    End Sub

    Private Sub btnCal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCal.Click
        Dim nSta1 As Integer
        Dim nSta2 As Integer
        Dim nStaNum As Integer
        Dim nEveTime As Integer
        Dim nEveTime2 As Integer
        Dim nTolTime As Integer
        Dim i As Integer
        nStaNum = 0
        Dim bIfAdd As Boolean
        bIfAdd = False
        nTolTime = Me.numTime.Value
        Dim sSta() As String
        ReDim sSta(0)
        For i = 1 To UBound(TrainInf(nCurTrain).nPathID)
            If StationInf(TrainInf(nCurTrain).nPathID(i)).sStationName = Me.CmbStartSta.Text Then
                nSta1 = i
                bIfAdd = True
            End If
            If bIfAdd = True Then
                nStaNum = nStaNum + 1
            End If
            If StationInf(TrainInf(nCurTrain).nPathID(i)).sStationName = Me.cmbEndSta.Text Then
                nSta2 = i
                bIfAdd = False
                Exit For
            End If
        Next

        Dim sTmpSta As String '刷选重复车站
        Dim j As Integer
        Dim ifExist As Boolean
        For i = nSta1 To nSta2
            ifExist = False
            sTmpSta = StationInf(TrainInf(nCurTrain).nPathID(i)).sStationName
            For j = 1 To UBound(sSta)
                If sSta(j) = sTmpSta Then
                    ifExist = True
                End If
            Next
            If ifExist = False Then
                ReDim Preserve sSta(UBound(sSta) + 1)
                sSta(UBound(sSta)) = sTmpSta
            End If
        Next

        If UBound(sSta) > 0 Then
            nEveTime = nTolTime / UBound(sSta)
            nEveTime2 = nTolTime - nEveTime * (UBound(sSta) - 1)
        End If

        Dim nCurRow As Integer
        nCurRow = 0
        Me.grdTime.Rows.Clear()
        For i = 1 To UBound(sSta)
            Me.grdTime.Rows.Add()
            Me.grdTime.Rows(nCurRow).Cells(0).Value = i
            Me.grdTime.Rows(nCurRow).Cells(1).Value = sSta(i)
            Me.grdTime.Rows(nCurRow).Cells(2).Value = nEveTime
            If i = UBound(sSta) Then
                Me.grdTime.Rows(nCurRow).Cells(2).Value = nEveTime2
            End If
            nCurRow = nCurRow + 1
        Next

    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim i As Integer
        Dim sSta As String
        Dim nAddStopTime As Integer
        Dim nStopTime As Integer
        nAddStopTime = 0
        Dim nCurSta As Integer
        Dim nEndtemp As Integer
        Dim lTime1, lTime2 As Integer
        Dim nTopStopTime As Integer
        nTopStopTime = 0
        For i = 1 To Me.grdTime.Rows.Count - 1
            sSta = Me.grdTime.Rows(i - 1).Cells(1).Value
            nCurSta = GetStationID(sSta)
            nStopTime = Me.grdTime.Rows(i - 1).Cells(2).Value
            Call TxFxDiffDim(UBound(TrainInf), UBound(StationInf))
            lTime1 = TrainInf(nCurTrain).Arrival(nCurSta)
            lTime2 = TimeAdd(TrainInf(nCurTrain).Starting(nCurSta), nStopTime)
            nEndtemp = TrainInf(nCurTrain).nPathID(UBound(TrainInf(nCurTrain).nPathID))
            Call TDrawLineNoStrainInMetro(lTime1, lTime2, 0, nCurTrain, nCurSta, nEndtemp, 1)
            nTopStopTime = nTopStopTime + nStopTime
        Next
        If Me.optStart.Checked = True Then '始发调匀
            '整体向左平移
            MoveTrain(nCurTrain, -nTopStopTime)
        End If

        Call addOneUndoInf()
        Call RefreshDiagram(1)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

End Class