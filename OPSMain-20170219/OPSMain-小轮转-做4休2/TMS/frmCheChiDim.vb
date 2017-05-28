Public Class frmCheChiDim

    Private Sub frmCheChiDim_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Me.cmbDownSta.Items.Clear()
        Me.cmbUpSta.Items.Clear()
        If UBound(NotSameStationInf) > 0 Then
            Me.cmbDownSta.Items.Add("始发站")
            Me.cmbUpSta.Items.Add("始发站")
            For i = 1 To UBound(NotSameStationInf)
                Me.cmbDownSta.Items.Add(NotSameStationInf(i))
                Me.cmbUpSta.Items.Add(NotSameStationInf(i))
            Next
            Me.cmbDownSta.Text = Me.cmbDownSta.Items(0)
            Me.cmbUpSta.Text = Me.cmbUpSta.Items(0)
        End If

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        '按列车点排序
        If Me.chkChediNum.Checked = True Then
            Call ResetPrintTrainString()
        End If

        Dim ntmpTrain() As Integer
        Dim i, j As Integer
        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long
        ReDim ntmpTrain(0)
        For i = 1 To UBound(TrainInf)
            If i Mod 2 <> 0 Then
                If TrainInf(i).Train <> "" Then
                    If GetStartTimeFromStaName(Me.cmbDownSta.Text, i) <> -1 Then
                        ReDim Preserve ntmpTrain(UBound(ntmpTrain) + 1)
                        ntmpTrain(UBound(ntmpTrain)) = i
                    Else
                        TrainInf(i).sPrintTrain = "0001"
                    End If
                End If
            End If
        Next i
        '按到达时间排序
        Flag = 1
        k = UBound(ntmpTrain)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                If Me.cmbDownSta.Text = "始发站" Then
                    TempTime1 = AddLitterTime(TrainInf(ntmpTrain(j)).Starting(TrainInf(ntmpTrain(j)).nPathID(1)))
                    Temptime2 = AddLitterTime(TrainInf(ntmpTrain(j + 1)).Starting(TrainInf(ntmpTrain(j + 1)).nPathID(1)))
                Else
                    TempTime1 = AddLitterTime(GetStartTimeFromStaName(Me.cmbDownSta.Text, ntmpTrain(j)))
                    Temptime2 = AddLitterTime(GetStartTimeFromStaName(Me.cmbDownSta.Text, ntmpTrain(j + 1)))
                End If
                If TempTime1 > Temptime2 Then '
                    temp = ntmpTrain(j)
                    ntmpTrain(j) = ntmpTrain(j + 1)
                    ntmpTrain(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop

        ' Dim nTrain As Integer
        Dim tmpK1 As Integer
        tmpK1 = 0
        For i = 1 To UBound(ntmpTrain)
            tmpK1 = tmpK1 + 1
            TrainInf(ntmpTrain(i)).sPrintTrain = "1" & FormatPrintTrainHou(tmpK1, 3)
        Next


        '按到达时间排序
        ReDim ntmpTrain(0)
        For i = 1 To UBound(TrainInf)
            If i Mod 2 = 0 Then
                If TrainInf(i).Train <> "" Then
                    If GetStartTimeFromStaName(Me.cmbDownSta.Text, i) <> -1 Then
                        ReDim Preserve ntmpTrain(UBound(ntmpTrain) + 1)
                        ntmpTrain(UBound(ntmpTrain)) = i
                    Else
                        TrainInf(i).sPrintTrain = "0002"
                    End If
                End If
            End If
        Next i

        Flag = 1
        k = UBound(ntmpTrain)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                If Me.cmbUpSta.Text = "始发站" Then
                    TempTime1 = AddLitterTime(TrainInf(ntmpTrain(j)).Starting(TrainInf(ntmpTrain(j)).nPathID(1)))
                    Temptime2 = AddLitterTime(TrainInf(ntmpTrain(j + 1)).Starting(TrainInf(ntmpTrain(j + 1)).nPathID(1)))
                Else
                    TempTime1 = AddLitterTime(GetStartTimeFromStaName(Me.cmbUpSta.Text, ntmpTrain(j)))
                    Temptime2 = AddLitterTime(GetStartTimeFromStaName(Me.cmbUpSta.Text, ntmpTrain(j + 1)))
                End If
                If TempTime1 > Temptime2 Then '
                    temp = ntmpTrain(j)
                    ntmpTrain(j) = ntmpTrain(j + 1)
                    ntmpTrain(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop

        tmpK1 = 0
        For i = 1 To UBound(ntmpTrain)
            tmpK1 = tmpK1 + 1
            TrainInf(ntmpTrain(i)).sPrintTrain = "2" & FormatPrintTrainHou(tmpK1, 3)
        Next

        Call addOneUndoInf()
        Call RefreshDiagram(1)
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class