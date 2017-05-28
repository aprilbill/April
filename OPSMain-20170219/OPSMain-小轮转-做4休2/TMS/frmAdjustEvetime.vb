Public Class frmAdjustEvetime
    Public nCurTrain As Integer
    Public nCurSec As Integer
    Dim nLeftTrain As Integer
    Dim nRightTrain As Integer
    Dim nBeforeInteval As Integer
    Dim nAfterInteval As Integer

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmAdjustEvetime_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim i As Integer
        Dim AfterTrain, BeforeTrainF As Integer
        Dim lTimeStart As Integer
        Dim nArriTime As Integer
        Dim nStationNumber As Integer
        Dim nSdirection As Integer
        nSdirection = nDirection(nCurTrain)
        '出发间隔判断
        For i = 1 To UBound(TrainInf(nCurTrain).nPassSection)
            If TrainInf(nCurTrain).nPassSection(i) = nCurSec Then
                lTimeStart = TrainInf(nCurTrain).Starting(TrainInf(nCurTrain).nFirstID(i))
                nArriTime = TrainInf(nCurTrain).Arrival(TrainInf(nCurTrain).nSecondID(i))
                nStationNumber = TrainInf(nCurTrain).nFirstID(i)
                Exit For
            End If
        Next
        BeforeTrainF = SeekLeftTrainSameDirection(nCurTrain, SectionInf(nCurSec).sSecName, lTimeStart, nArriTime)
        If BeforeTrainF <> nCurTrain Then
            nBeforeInteval = TimeMinus(lTimeStart, TrainInf(BeforeTrainF).Starting(nStationNumber))
        End If

        AfterTrain = SeekRightTrainSameDirection(nCurTrain, SectionInf(nCurSec).sSecName, lTimeStart, nArriTime)
        If AfterTrain = BeforeTrainF Then
            MsgBox("不满足调整要求，无法调整!", , "提示")
            Exit Sub
        Else
            If AfterTrain <> nCurTrain Then
                nAfterInteval = TimeMinus(TrainInf(AfterTrain).Starting(nStationNumber), lTimeStart)
            End If
            Me.labCurSec.Text = SectionInf(nCurSec).sSecName
            Me.labCurTrain.Text = TrainInf(nCurTrain).sPrintTrain
            Me.labLeftTrain.Text = TrainInf(BeforeTrainF).sPrintTrain
            Me.labRightTrain.Text = TrainInf(AfterTrain).sPrintTrain
            If nCurTrain Mod 2 = 0 Then
                Me.labUporDown.Text = "上行列车"
            Else
                Me.labUporDown.Text = "下行列车"
            End If
            Me.labTimeLeft.Text = SecondToMinute(nBeforeInteval)
            Me.LabtimeRight.Text = SecondToMinute(nAfterInteval)
            Me.labTimeEver.Text = SecondToMinute((nBeforeInteval + nAfterInteval) / 2)
            Me.labTimeTotle.Text = SecondToMinute(nBeforeInteval + nAfterInteval)
            Me.txtLeftTime.Text = SecondToMinute((nBeforeInteval + nAfterInteval) / 2)
            Me.txtRightTime.Text = SecondToMinute(nBeforeInteval + nAfterInteval - Int((nBeforeInteval + nAfterInteval) / 2))
            Me.KeyPreview = True

        End If
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim nMoveTime As Integer
        nMoveTime = MinuteToSecond(Me.txtLeftTime.Text) - nBeforeInteval
        Dim nTrain As Integer
        nTrain = nCurTrain
        Dim i As Integer
        If nMoveTime <> 0 Then
            For i = 1 To UBound(TrainInf(nTrain).Arrival)
                TrainInf(nTrain).Arrival(i) = TimeAdd(TrainInf(nTrain).Arrival(i), nMoveTime)
                TrainInf(nTrain).Starting(i) = TimeAdd(TrainInf(nTrain).Starting(i), nMoveTime)
            Next
            TrainInf(nTrain).lAllStartTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
            TrainInf(nTrain).lAllEndTime = TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID)))
            TrainInf(nTrain).sStartZFArrival = -1
            TrainInf(nTrain).sStartZFStarting = -1
            TrainInf(nTrain).sEndZFArrival = -1
            TrainInf(nTrain).sEndZFStarting = -1
            Call addOneUndoInf()
            Call RefreshDiagram(1)
        End If

    End Sub

    Private Sub txtLeftTime_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtLeftTime.KeyUp
        Me.txtRightTime.Text = SecondToMinute(nBeforeInteval + nAfterInteval - MinuteToSecond(Me.txtLeftTime.Text))
    End Sub
End Class