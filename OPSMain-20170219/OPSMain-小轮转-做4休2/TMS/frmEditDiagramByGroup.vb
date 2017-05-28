Public Class frmEditDiagramByGroup

    Private Sub frmEditDiagramByGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.lstChaoZuo.Items.Clear()
        Me.lstChaoZuo.Items.Add("ɾ���г�")
        Me.lstChaoZuo.Items.Add("��ѡ�г�")
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
        Me.cmbTrainJiaoLu.Items.Add("���н�·")
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
            MsgBox("ʱ�䷶Χ������Ҫ��")
            Me.txtFirTime.Select()
            Exit Sub
        End If
        If Me.cmbTrainJiaoLu.Text.Trim = "" Then
            MsgBox("��ѡ��·���ƣ�")
            Exit Sub
        End If
        'Dim Cdid2 As Integer
        'Dim nAfterTrain As Integer  
        Dim sCurValue As String
        If Me.lstChaoZuo.SelectedIndex >= 0 Then
            sCurValue = Me.lstChaoZuo.Items(Me.lstChaoZuo.SelectedIndex)
            Select Case sCurValue
                Case "ɾ���г�"
                    If MsgBox("��Ҫɾ������Ҫ��������г�����", vbQuestion + vbYesNo + vbDefaultButton2, "ȷ��ɾ��") = vbYes Then
                        If Me.cmbTrainJiaoLu.Text.Trim = "���н�·" Then
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
                        MsgBox("ɾ����ϣ�")
                    End If
                Case "��ѡ�г�"
                    ReDim TimeTablePara.nPubTrains(0)
                    If Me.cmbTrainJiaoLu.Text.Trim = "���н�·" Then
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
            MsgBox("����ѡ�����!", , "��ʾ")
        End If

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class