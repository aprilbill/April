Public Class frmEditDiagramByGroup

    Private Sub frmEditDiagramByGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lstChaoZuo.Items.Clear()
        Me.lstChaoZuo.Items.Add("删除列车")
        Me.lstChaoZuo.Items.Add("多选列车")
        Dim i As Integer
        Dim j As Integer
        Dim IfIn As Integer
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                IfIn = 0
                For j = 1 To Me.cmbTrainJiaoLu.Items.Count
                    If Me.cmbTrainJiaoLu.Items(j - 1) = TrainInf(i).sJiaoLuName Then
                        IfIn = 1
                        Exit For
                    End If
                Next
                If IfIn = 0 Then
                    Me.cmbTrainJiaoLu.Items.Add(TrainInf(i).sJiaoLuName)
                End If
            End If
        Next
        Me.cmbTrainJiaoLu.Items.Add("所有交路")
    End Sub

    Private Sub btnBeDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBeDelete.Click
        Dim i As Integer
        Dim sFirTime As Long
        Dim sSecTime As Long
        Dim lngCurTime As Long
        Dim Cdid As Integer
        sFirTime = AddLitterTime(HourToSecond(Me.txtFirTime.Text))
        sSecTime = AddLitterTime(HourToSecond(Me.txtEndTime.Text))
        If sSecTime <= sFirTime Then
            MsgBox("时间范围不符合要求！")
            Me.txtFirTime.Select()
            Exit Sub
        End If
        If Me.cmbTrainJiaoLu.Text.Trim = "" Then
            MsgBox("请选择交路名称！")
            Exit Sub
        End If
        'Dim Cdid2 As Integer
        'Dim nAfterTrain As Integer  
        Dim sCurValue As String
        If Me.lstChaoZuo.SelectedIndex >= 0 Then
            sCurValue = Me.lstChaoZuo.Items(Me.lstChaoZuo.SelectedIndex)
            Select Case sCurValue
                Case "删除列车"
                    If MsgBox("将要删除满足要求的所有列车！！", vbQuestion + vbYesNo + vbDefaultButton2, "确认删除") = vbYes Then
                        If Me.cmbTrainJiaoLu.Text.Trim = "所有交路" Then
                            For i = 1 To UBound(TrainInf)
                                If TrainInf(i).Train <> "" Then
                                    lngCurTime = AddLitterTime(TrainInf(i).Starting(TrainInf(i).nPathID(1)))
                                    If lngCurTime >= sFirTime And lngCurTime <= sSecTime Then
                                        Cdid = CheCiToCheDiID(i)
                                        If Cdid <> 0 Then
                                            Call DelectTrainInCheDiLink(i, Cdid)
                                        End If
                                    End If
                                End If
                            Next i
                        Else
                            For i = 1 To UBound(TrainInf)
                                If TrainInf(i).Train <> "" And TrainInf(i).sJiaoLuName = Me.cmbTrainJiaoLu.Text.Trim Then
                                    lngCurTime = AddLitterTime(TrainInf(i).Starting(TrainInf(i).nPathID(1)))
                                    If lngCurTime >= sFirTime And lngCurTime <= sSecTime Then
                                        Cdid = CheCiToCheDiID(i)
                                        If Cdid <> 0 Then
                                            Call DelectTrainInCheDiLink(i, Cdid)
                                        End If
                                    End If
                                End If
                            Next i
                        End If

                        If TimeTablePara.BifAutoBianCheCi = True Then
                            Call ResetPrintTrainString()
                        End If
                        Call addOneUndoInf()
                        Call ShowChediJiaolu2()
                        MsgBox("删除完毕！")
                    End If
                Case "多选列车"
                    ReDim TimeTablePara.nPubTrains(0)
                    If Me.cmbTrainJiaoLu.Text.Trim = "所有交路" Then
                        For i = 1 To UBound(TrainInf)
                            If TrainInf(i).Train <> "" Then
                                ReDim Preserve TimeTablePara.nPubTrains(UBound(TimeTablePara.nPubTrains) + 1)
                                TimeTablePara.nPubTrains(UBound(TimeTablePara.nPubTrains)) = i
                            End If
                        Next i
                    Else
                        For i = 1 To UBound(TrainInf)
                            If TrainInf(i).Train <> "" And TrainInf(i).sJiaoLuName = Me.cmbTrainJiaoLu.Text.Trim Then
                                lngCurTime = AddLitterTime(TrainInf(i).Starting(TrainInf(i).nPathID(1)))
                                If lngCurTime >= sFirTime And lngCurTime <= sSecTime Then
                                    ReDim Preserve TimeTablePara.nPubTrains(UBound(TimeTablePara.nPubTrains) + 1)
                                    TimeTablePara.nPubTrains(UBound(TimeTablePara.nPubTrains)) = i
                                End If
                            End If
                        Next i
                    End If
                    If UBound(TimeTablePara.nPubTrains) > 0 Then
                        TimeTablePara.nPubTrain = TimeTablePara.nPubTrains(1)
                    Else
                        TimeTablePara.nPubTrain = 0
                    End If
            End Select
        Else
            MsgBox("请先选择操作!", , "提示")
        End If

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class