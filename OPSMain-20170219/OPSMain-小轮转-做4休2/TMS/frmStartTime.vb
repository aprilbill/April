Public Class frmStartTime
    Public nPriCurTrain As Integer
    Public lngPriStartTime As Long
    Public IfNotEdit As Integer
    Public intCurMoveTimeSta As Integer
    Public nAfterMove As Boolean
    Public nBeforeMove As Boolean

    Private Sub frmStartTime_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Enter Then
            Call btnOK_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmStartTime_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim NowTime As Long
        Dim nMoveTime As Long
        NowTime = TrainInf(nPriCurTrain).Starting(intCurMoveTimeSta)
        Me.txtTime.Text = SecondToHour(lngPriStartTime, 1)
        Me.txtFirstTime.Text = SecondToHour(NowTime, 1)
        nMoveTime = AddLitterTime(lngPriStartTime) - AddLitterTime(NowTime)
        If nMoveTime > 0 Then
            Me.optRight.Checked = True
            Me.numTime.Value = nMoveTime
        Else
            Me.optLeft.Checked = True
            Me.numTime.Value = -nMoveTime
        End If
        IfNotEdit = 0
        If StationInf(intCurMoveTimeSta).sStationName = TrainInf(nPriCurTrain).ComeStation Then
            Me.chkMoveArriTime.Visible = False
        Else
            Me.chkMoveArriTime.Visible = True
        End If
        Me.KeyPreview = True
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim lngStartTime As Long
        Dim lMoveTimeTo As Long
        Dim strInfor As String
        strInfor = CheckTimeFormat(Me.txtTime.Text)
        Dim AnoTrain As Integer
        Dim AnoTime As Long
        Dim NowTime As Long
        Dim NowTrain As Integer
        Dim NowSta As Integer
        'Dim UpOrDown As Integer
        Dim sArriTime As Long
        Dim NextSta As Integer
        Dim sZFtime As Long

        ' Dim nBeTrain As Integer
        'Dim nAfTrain As Integer
        'Dim ntmpTrain As Integer
        'Dim tmpTime As Long
        'Dim tmpTime1 As Long
        'Dim tmpTime2 As Long
        'Dim tmpTime3 As Long
        'Dim sFaDaoTime As Long
        nBeforeMove = False
        nAfterMove = False
        Dim nNowTrain As Integer
        nNowTrain = nPriCurTrain
        Dim nNowSta As Integer
        nNowSta = intCurMoveTimeSta
        'NowTime = AddLitterTime(NowTime)
        If strInfor = "�ϸ�" Then
            lngStartTime = HourToSecond(Me.txtTime.Text)
            lMoveTimeTo = lngStartTime

            NowTrain = nNowTrain
            NowTime = TrainInf(NowTrain).Starting(nNowSta)
            NowSta = nNowSta
            If TimeMinus(NowTime, lMoveTimeTo) >= TimeMinus(lMoveTimeTo, NowTime) Then '����

                '�����г����Ӱ��
                If NowTrain Mod 2 <> 0 Then '����
                    AnoTrain = TrainInf(NowTrain).TrainReturn(2) 'BEFORE(NowTrian, lMoveTimeTo, MainForm.nNowStation, UpOrDown, 1, 1, "", "", "")
                    If AnoTrain <> 0 And TrainInf(AnoTrain).nZfLimit = 0 Then
                        AnoTime = TrainInf(AnoTrain).Starting(TrainInf(AnoTrain).nPathID(1))
                        NextSta = TrainInf(NowTrain).nPathID(UBound(TrainInf(NowTrain).nPathID))
                        If (AnoTrain + NowTrain) Mod 2 = 0 Then
                            sZFtime = GetZheFanTime(TrainInf(NowTrain).SCheDiLeiXing, StationInf(NextSta).sStationName, "�����۷�")
                        Else
                            sZFtime = GetZheFanTime(TrainInf(NowTrain).SCheDiLeiXing, StationInf(NextSta).sStationName, TrainInf(NowTrain).TrainReturnStyle(2))
                        End If
                        sArriTime = TrainInf(NowTrain).Starting(NowSta)
                        AnoTime = TimeMinus(AnoTime, TimeMinus(TrainInf(NowTrain).Arrival(NextSta), sArriTime))
                        AnoTime = TimeMinus(AnoTime, sZFtime)
                        If TimeMinus(AnoTime, lMoveTimeTo) > TimeMinus(lMoveTimeTo, AnoTime) Then
                            MsgBox("�����Ƶ�" & TrainInf(AnoTrain).Train & "�г�֮��!!����ֻ���Ƶ�" & dTime(DeleteLitterTime(AnoTime), 0) & "ʱ��")  'dTime(sTimeToZhenShuDown(AnoTime, sMoveJGTime), 0) & "ʱ��"
                            Me.txtTime.Text = dTime(DeleteLitterTime(AnoTime), 0)  'dTime(sTimeToZhenShuDown(AnoTime, sMoveJGTime), 0)
                            Me.txtTime.Select()
                            Exit Sub
                        End If
                    End If
                Else '����

                    AnoTrain = TrainInf(NowTrain).TrainReturn(2) 'BEFORE(NowTrian, lMoveTimeTo, MainForm.nNowStation, UpOrDown, 1, 1, "", "", "")
                    If AnoTrain <> 0 And TrainInf(AnoTrain).nZfLimit = 0 Then
                        AnoTime = TrainInf(AnoTrain).Starting(TrainInf(AnoTrain).nPathID(1))
                        NextSta = TrainInf(NowTrain).nPathID(UBound(TrainInf(NowTrain).nPathID))

                        If (AnoTrain + NowTrain) Mod 2 = 0 Then
                            sZFtime = GetZheFanTime(TrainInf(NowTrain).SCheDiLeiXing, StationInf(NextSta).sStationName, "�����۷�")
                        Else
                            sZFtime = GetZheFanTime(TrainInf(NowTrain).SCheDiLeiXing, StationInf(NextSta).sStationName, TrainInf(NowTrain).TrainReturnStyle(2))
                        End If

                        sArriTime = TrainInf(NowTrain).Starting(NowSta)
                        AnoTime = TimeMinus(AnoTime, TimeMinus(TrainInf(NowTrain).Arrival(NextSta), sArriTime))
                        AnoTime = TimeMinus(AnoTime, sZFtime)

                        If TimeMinus(AnoTime, lMoveTimeTo) > TimeMinus(lMoveTimeTo, AnoTime) Then
                            MsgBox("�����Ƶ�" & TrainInf(AnoTrain).Train & "�г�֮��!!����ֻ���Ƶ�" & dTime(DeleteLitterTime(AnoTime), 0) & "ʱ��")  'dTime(sTimeToZhenShuDown(AnoTime, sMoveJGTime), 0) & "ʱ��"
                            Me.txtTime.Text = dTime(DeleteLitterTime(AnoTime), 0)  ' dTime(sTimeToZhenShuDown(AnoTime, sMoveJGTime), 0)
                            Me.txtTime.Select()
                            Exit Sub
                        End If
                    End If
            End If

            Else '����

                If NowTrain Mod 2 <> 0 Then '����
                    AnoTrain = TrainInf(NowTrain).TrainReturn(1) 'BEFORE(NowTrian, lMoveTimeTo, MainForm.nNowStation, UpOrDown, 1, 1, "", "", "")
                    If AnoTrain <> 0 And TrainInf(NowTrain).nZfLimit = 0 Then
                        If (AnoTrain + NowTrain) Mod 2 = 0 Then
                            sZFtime = GetZheFanTime(TrainInf(NowTrain).SCheDiLeiXing, StationInf(NowSta).sStationName, "�����۷�")
                        Else
                            sZFtime = GetZheFanTime(TrainInf(NowTrain).SCheDiLeiXing, StationInf(NowSta).sStationName, TrainInf(NowTrain).TrainReturnStyle(1))
                        End If
                        AnoTime = TimeAdd(TrainInf(AnoTrain).Arrival(NowSta), sZFtime)

                        If TimeMinus(AnoTime, lMoveTimeTo) < TimeMinus(lMoveTimeTo, AnoTime) Then
                            MsgBox("�����Ƶ�" & TrainInf(AnoTrain).Train & "�г�֮ǰ!!����ֻ���Ƶ�" & dTime(AnoTime, 0) & "ʱ��") 'dTime(sTimeToZhenShu(AnoTime, sMoveJGTime), 0) & "ʱ��"
                            Me.txtTime.Text = dTime(AnoTime, 0) ' dTime(sTimeToZhenShu(AnoTime, sMoveJGTime), 0)
                            Me.txtTime.Select()
                            Exit Sub
                        End If
                    End If

                Else '����

                    AnoTrain = TrainInf(NowTrain).TrainReturn(1) 'BEFORE(NowTrian, lMoveTimeTo, MainForm.nNowStation, UpOrDown, 1, 1, "", "", "")
                    If AnoTrain <> 0 And TrainInf(NowTrain).nZfLimit = 0 Then
                        If (AnoTrain + NowTrain) Mod 2 = 0 Then
                            sZFtime = GetZheFanTime(TrainInf(NowTrain).SCheDiLeiXing, StationInf(NowSta).sStationName, "�����۷�")
                        Else
                            sZFtime = GetZheFanTime(TrainInf(NowTrain).SCheDiLeiXing, StationInf(NowSta).sStationName, TrainInf(NowTrain).TrainReturnStyle(1))
                        End If
                        AnoTime = TimeAdd(TrainInf(AnoTrain).Arrival(NowSta), sZFtime)

                        If TimeMinus(AnoTime, lMoveTimeTo) < TimeMinus(lMoveTimeTo, AnoTime) Then
                            MsgBox("�����Ƶ�" & TrainInf(AnoTrain).Train & "�г�֮ǰ!!����ֻ���Ƶ�" & dTime(AnoTime, 0) & "ʱ��") 'dTime(sTimeToZhenShu(AnoTime, sMoveJGTime), 0) & "ʱ��"
                            Me.txtTime.Text = dTime(AnoTime, 0)
                            Me.txtTime.Select()
                            Exit Sub
                        End If
                    End If
                End If
            End If


            If Me.chkMoveArriTime.Checked = True Then
                Dim nArriTime As Long
                Dim nStartTime As Long
                Dim nMoveTime As Long
                nMoveTime = TimeMinus(HourToSecond(Me.txtTime.Text), HourToSecond(Me.txtFirstTime.Text))
                nArriTime = TimeAdd(TrainInf(nPriCurTrain).Arrival(intCurMoveTimeSta), nMoveTime)
                nStartTime = TrainInf(nPriCurTrain).Starting(intCurMoveTimeSta)
                RecordStaTime(nPriCurTrain, intCurMoveTimeSta, nStartTime, nArriTime)
            End If
            Call DrawSingleTrain(nPriCurTrain, lngStartTime, intCurMoveTimeSta)
            IfNotEdit = 1
            Me.Close()
        Else
            MsgBox(strInfor)
            Me.txtTime.Select()
        End If


    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Function CheckTimeFormat(ByVal HourTime As String) As String
        CheckTimeFormat = "�ϸ�"
        HourTime = HourTime.Trim
        If HourTime.Length > 8 Then
            CheckTimeFormat = "ʱ�䳤�Ȳ�����Ҫ������������!"
        Else

            Dim i As Integer
            Dim sSecond As Single
            For i = 1 To Len(HourTime)
                If Mid(HourTime, i, 1) = "." Or Mid(HourTime, i, 1) = ":" Then
                    sSecond = Val(Microsoft.VisualBasic.Left(HourTime, i)) * 3600
                    HourTime = Microsoft.VisualBasic.Right(HourTime, Len(HourTime) - i)
                    Exit For
                End If

                If Val(HourTime) < 0 Or Val(HourTime) > 24 Then
                    CheckTimeFormat = "Сʱʱ���ʽ������Ҫ������������!"
                    Exit Function
                End If

                If i = Len(HourTime) Then
                    Exit Function
                End If
            Next i

            For i = 1 To Len(HourTime)
                If Mid(HourTime, i, 1) = "." Or Mid(HourTime, i, 1) = ":" Then
                    sSecond = sSecond + Val(Microsoft.VisualBasic.Left(HourTime, i)) * 60
                    HourTime = Microsoft.VisualBasic.Right(HourTime, Len(HourTime) - i)
                    Exit For
                End If

                If Val(HourTime) < 0 Or Val(HourTime) > 60 Then
                    CheckTimeFormat = "����ʱ���ʽ������Ҫ������������!"
                    Exit Function
                End If

                If i = Len(HourTime) Then
                    Exit Function
                End If
            Next i

            For i = 1 To Len(HourTime)
                If Mid(HourTime, i, 1) = "." Or Mid(HourTime, i, 1) = ":" Then
                    sSecond = sSecond + Val(Microsoft.VisualBasic.Left(HourTime, i))
                    HourTime = Microsoft.VisualBasic.Right(HourTime, Len(HourTime) - i)
                    Exit For
                End If

                If Val(HourTime) < 0 Or Val(HourTime) > 60 Then
                    CheckTimeFormat = "����ʱ���ʽ������Ҫ������������!"
                    Exit Function
                End If
                If i = Len(HourTime) Then
                    Exit Function
                End If
            Next i
            Exit Function
        End If

    End Function

    Private Sub txtTime_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTime.KeyPress
        Dim intAsc As Int16
        intAsc = Asc(e.KeyChar)
        'MsgBox(intAsc)
        'ֻ����������
        If intAsc = 13 Then '�س�
            Call btnOK_Click(Nothing, Nothing)
        Else
            If intAsc = 46 Or intAsc = 58 Or intAsc = 8 Then
                e.Handled = False
            ElseIf intAsc > Asc("9") Or intAsc < Asc("0") Then
                e.Handled = True
            Else
                e.Handled = False
            End If
        End If
    End Sub

    Private Sub numTime_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles numTime.KeyUp
        'MsgBox(Me.numTime.Value)
        Call TimeMove()
    End Sub

    Private Sub numTime_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles numTime.MouseDown
        Call TimeMove()
    End Sub

    Private Sub TimeMove()
        If Me.optRight.Checked = True Then
            Me.txtTime.Text = SecondToHour(TimeAdd(TrainInf(nPriCurTrain).Starting(intCurMoveTimeSta), Me.numTime.Value), 1)
        ElseIf Me.optLeft.Checked = True Then
            Me.txtTime.Text = SecondToHour(TimeMinus(TrainInf(nPriCurTrain).Starting(intCurMoveTimeSta), Val(Me.numTime.Value)), 1)
        End If
    End Sub

    Private Sub optRight_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles optRight.MouseClick
        Call TimeMove()
    End Sub

    Private Sub optLeft_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles optLeft.MouseClick
        Call TimeMove()
    End Sub
End Class