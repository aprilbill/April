Public Class frmSetTrainsStopSame
    Public nCurSta As Integer
    Public nCurTrains As New System.Collections.Generic.List(Of Integer)
    Private Sub frmSetTrainsStopSame_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Dim listText As String
        Dim nCurTrain As Integer
        listText = ""
        For i = 1 To nCurTrains.Count
            listText = listText & "/" & nCurTrains.Item(i - 1)
        Next
        Me.labCurTrain.Text = "当前车次：" & listText
        nCurTrain = nCurTrains.Item(0)
        Me.labCurTrip.Text = "列车交路：" & TrainInf(nCurTrain).sJiaoLuName
        Me.CmbStartSta.Items.Clear()
        Me.cmbEndSta.Items.Clear()
        For i = 1 To UBound(TrainInf(nCurTrain).nPathID)
            Me.CmbStartSta.Items.Add(StationInf(TrainInf(nCurTrain).nPathID(i)).sStationName)
            Me.cmbEndSta.Items.Add(StationInf(TrainInf(nCurTrain).nPathID(i)).sStationName)
        Next
        If Me.optStart.Checked = True Then '始发调匀
            Me.CmbStartSta.Text = TrainInf(nCurTrain).ComeStation
            Me.cmbEndSta.Text = StationInf(nCurSta).sPrintStaName
        Else
            Me.CmbStartSta.Text = StationInf(nCurSta).sPrintStaName
            Me.cmbEndSta.Text = TrainInf(nCurTrain).NextStation
        End If
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim nCurTrain As Integer
        Dim nNextTrain As Integer
        Dim sZheFantime As Integer
        Dim nZFTime As Integer
        Dim DeltaTime As Integer
        Dim nSta1 As Integer
        Dim nSta2 As Integer
        Dim nStaNum As Integer
        Dim nEveTime As Integer
        Dim nEveTime2 As Integer
        Dim nTolTime As Integer
        Dim bIfAdd As Boolean
        Dim sPathSta As New System.Collections.Generic.List(Of String)
        Dim nStopTime As New System.Collections.Generic.List(Of Integer)

        Dim sTSta As String
        Dim nCurSta As Integer
        Dim nEndtemp As Integer
        Dim nTmpStopTime As Integer
        Dim lTime1, lTime2 As Integer
        Dim sTmpSta As String '刷选重复车站
        Dim ifExist As Boolean
        Dim sSta() As String
        If Me.optStart.Checked = True Then '始发调匀
            For i = 1 To nCurTrains.Count
                nCurTrain = nCurTrains.Item(i - 1)
                'If TrainInf(nCurTrain).TrainStyle = "环形车" Then
                nNextTrain = TrainInf(nCurTrain).TrainReturn(1)
                If nNextTrain <> 0 Then
                    nZFTime = TimeMinus(TrainInf(nCurTrain).Starting(TrainInf(nCurTrain).nPathID(1)), TrainInf(nNextTrain).Arrival(TrainInf(nNextTrain).nPathID(UBound(TrainInf(nNextTrain).nPathID))))
                    sZheFantime = GetZheFanTime(TrainInf(nNextTrain).SCheDiLeiXing, StationInf(TrainInf(nNextTrain).nPathID(UBound(TrainInf(nNextTrain).nPathID))).sStationName, TrainInf(nNextTrain).TrainReturnStyle(2))
                    DeltaTime = nZFTime - sZheFantime
                End If
                ' End If
                nStaNum = 0
                bIfAdd = False
                nTolTime = DeltaTime

                For j = 1 To UBound(TrainInf(nCurTrain).nPathID)
                    If StationInf(TrainInf(nCurTrain).nPathID(j)).sStationName = Me.CmbStartSta.Text Then
                        nSta1 = j
                        bIfAdd = True
                    End If
                    If bIfAdd = True Then
                        nStaNum = nStaNum + 1
                    End If
                    If StationInf(TrainInf(nCurTrain).nPathID(j)).sStationName = Me.cmbEndSta.Text Then
                        nSta2 = j
                        bIfAdd = False
                        Exit For
                    End If
                Next

                ReDim sSta(0)
                For j = nSta1 To nSta2
                    ifExist = False
                    sTmpSta = StationInf(TrainInf(nCurTrain).nPathID(j)).sStationName
                    For k = 1 To UBound(sSta)
                        If sSta(k) = sTmpSta Then
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

                sPathSta.Clear()
                nStopTime.Clear()

                For j = 1 To UBound(sSta)
                    sPathSta.Add(sSta(j))
                    If j = UBound(sSta) Then
                        nStopTime.Add(nEveTime2)
                    Else
                        nStopTime.Add(nEveTime)
                    End If
                Next

                For j = 1 To sPathSta.Count
                    sTSta = sPathSta.Item(j - 1)
                    nCurSta = GetStationID(sTSta)
                    nTmpStopTime = nStopTime.Item(j - 1)
                    Call TxFxDiffDim(UBound(TrainInf), UBound(StationInf))
                    lTime1 = TrainInf(nCurTrain).Arrival(nCurSta)
                    lTime2 = TimeAdd(TrainInf(nCurTrain).Starting(nCurSta), nTmpStopTime)
                    nEndtemp = TrainInf(nCurTrain).nPathID(UBound(TrainInf(nCurTrain).nPathID))
                    Call TDrawLineNoStrainInMetro(lTime1, lTime2, 0, nCurTrain, nCurSta, nEndtemp, 1)
                Next
                '整体向左平移
                MoveTrain(nCurTrain, -DeltaTime)
            Next

        Else '终到调匀
            For i = 1 To nCurTrains.Count
                nCurTrain = nCurTrains.Item(i - 1)
                ' If TrainInf(nCurTrain).TrainStyle = "环形车" Then
                nNextTrain = TrainInf(nCurTrain).TrainReturn(2)
                If nNextTrain <> 0 Then
                    nZFTime = TimeMinus(TrainInf(nNextTrain).Starting(TrainInf(nNextTrain).nPathID(1)), TrainInf(nCurTrain).Arrival(TrainInf(nCurTrain).nPathID(UBound(TrainInf(nCurTrain).nPathID))))
                    sZheFantime = GetZheFanTime(TrainInf(nCurTrain).SCheDiLeiXing, StationInf(TrainInf(nCurTrain).nPathID(UBound(TrainInf(nCurTrain).nPathID))).sStationName, TrainInf(nCurTrain).TrainReturnStyle(2))
                    DeltaTime = nZFTime - sZheFantime
                End If
                ' End If
                nStaNum = 0
                bIfAdd = False
                nTolTime = DeltaTime

                For j = 1 To UBound(TrainInf(nCurTrain).nPathID)
                    If StationInf(TrainInf(nCurTrain).nPathID(j)).sStationName = Me.CmbStartSta.Text Then
                        nSta1 = j
                        bIfAdd = True
                    End If
                    If bIfAdd = True Then
                        nStaNum = nStaNum + 1
                    End If
                    If StationInf(TrainInf(nCurTrain).nPathID(j)).sStationName = Me.cmbEndSta.Text Then
                        nSta2 = j
                        bIfAdd = False
                        Exit For
                    End If
                Next

                ReDim sSta(0)
                For j = nSta1 To nSta2
                    ifExist = False
                    sTmpSta = StationInf(TrainInf(nCurTrain).nPathID(j)).sStationName
                    For k = 1 To UBound(sSta)
                        If sSta(k) = sTmpSta Then
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

                sPathSta.Clear()
                nStopTime.Clear()

                For j = 1 To UBound(sSta)
                    sPathSta.Add(sSta(j))
                    If j = UBound(sSta) Then
                        nStopTime.Add(nEveTime2)
                    Else
                        nStopTime.Add(nEveTime)
                    End If
                Next

                For j = 1 To sPathSta.Count
                    sTSta = sPathSta.Item(j - 1)
                    nCurSta = GetStationID(sTSta)
                    nTmpStopTime = nStopTime.Item(j - 1)
                    Call TxFxDiffDim(UBound(TrainInf), UBound(StationInf))
                    lTime1 = TrainInf(nCurTrain).Arrival(nCurSta)
                    lTime2 = TimeAdd(TrainInf(nCurTrain).Starting(nCurSta), nTmpStopTime)
                    nEndtemp = TrainInf(nCurTrain).nPathID(UBound(TrainInf(nCurTrain).nPathID))
                    Call TDrawLineNoStrainInMetro(lTime1, lTime2, 0, nCurTrain, nCurSta, nEndtemp, 1)
                Next
            Next

        End If
        Call addOneUndoInf()
        Call RefreshDiagram(1)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class