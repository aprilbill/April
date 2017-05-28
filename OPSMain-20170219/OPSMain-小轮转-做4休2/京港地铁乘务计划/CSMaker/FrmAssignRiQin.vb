Public Class FrmAssignRiQin


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.Checked = False Then
            Me.Close()
        Else
            formRiQin()
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            GroupBox1.Enabled = True
        Else
            GroupBox1.Enabled = False
        End If
    End Sub
    Private Sub formRiQin()
        Call SortMergedCSLinkTrain(True)
        Dim AvaMerTrains As New List(Of MergedCSLinkTrain)
        For Each tmertrain As MergedCSLinkTrain In CSTrainsAndDrivers.MergedCSLinkTrains
            If tmertrain IsNot Nothing AndAlso tmertrain.IsLinked = False Then
                AvaMerTrains.Add(tmertrain)
            End If
        Next
        Me.ProgressBar1.Maximum = AvaMerTrains.Count
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.Visible = True
        For Each tmer As MergedCSLinkTrain In AvaMerTrains

            Dim maxWaitTime As Integer = -1
            Dim selectDri As CSDriver = Nothing
            Dim AvaDris As New List(Of CSDriver)
            Dim AttOffPlace As Boolean = False

            If tmer.beiche <> 0 Then
                If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then           '找出可以接车的所有司机
                    For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                        If dri IsNot Nothing AndAlso dri.DutySort = "暂编" AndAlso dri.dutyWork <> "替饭" Then
                            Dim upTainDirection As Integer = TransitStationPlaceforUptrain(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime)
                            If (upTainDirection <> -1 And upTainDirection <> 2) AndAlso upTainDirection <> tmer.UpOrDown Then '判断是否是可以接的方向
                                Continue For
                            End If
                            If dri.CanDriveTheTrain(tmer) Then
                                If tmer.dutywork = "吃饭" And dri.dutyWork = "已吃饭" Then
                                    Continue For
                                End If
                                If upTainDirection = -1 Then
                                    AttOffPlace = True
                                End If
                                AvaDris.Add(dri)
                            Else
                                If dri.dutyWork = "备车" Then
                                    If upTainDirection = -1 Then
                                        AttOffPlace = True
                                    End If
                                    AvaDris.Add(dri)
                                End If
                            End If
                        End If
                    Next
                End If
                For Each dri As CSDriver In AvaDris
                    If dri IsNot Nothing AndAlso dri.DutySort = "暂编" AndAlso dri.dutyWork = "备车" Then
                        If dri.CSLinkTrain(UBound(dri.CSLinkTrain)).nCheDiID = tmer.nCheDiID AndAlso dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName = tmer.CSLinkTrains(1).StartStaName Then
                            selectDri = dri
                            Exit For
                        End If
                    End If
                Next
                If selectDri Is Nothing Then
                    If AttOffPlace = False Then
                        '最大休息时间人接车 
                        maxWaitTime = -1
                        For Each dri As CSDriver In AvaDris
                            If dri.dutyWork = "备车" Then
                                Continue For
                            End If
                            Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                            If waittime > maxWaitTime And waittime >= ChangePlaceRestTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime) Then
                                maxWaitTime = waittime
                                selectDri = dri
                            End If
                        Next
                    Else
                        maxWaitTime = 24 * 3600 '最小
                        For Each dri As CSDriver In AvaDris
                            If dri.dutyWork = "备车" Then
                                Continue For
                            End If
                            Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                            If waittime < maxWaitTime Then
                                maxWaitTime = waittime
                                selectDri = dri
                            End If
                        Next
                    End If
                End If

                If selectDri IsNot Nothing Then
                    If tmer.beiche = 1 Then
                        selectDri.dutyWork = "备车"
                    Else
                        selectDri.dutyWork = ""
                    End If
                    selectDri.AddMergedTrain(tmer)
                    selectDri.RefreshState()
                Else
                    Call AddANewDriverforMerged("暂编", tmer)
                    If tmer.beiche = 1 Then
                        CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).dutyWork = "备车"
                    End If
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).RefreshState()
                End If
                Continue For
            End If

            '====普通任务段处理
            selectDri = Nothing
            AvaDris = New List(Of CSDriver)
            Dim ifDirSame As Boolean = False
            If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then         '找出可以接车的所有司机
                For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If dri IsNot Nothing AndAlso dri.DutySort = "暂编" AndAlso dri.dutyWork <> "替饭" Then
                        Dim upTainDirection As Integer = TransitStationPlaceforUptrain(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime)
                        If (upTainDirection <> -1 And upTainDirection <> 2) AndAlso upTainDirection <> tmer.UpOrDown Then '判断是否是可以接的方向
                            Continue For
                        End If
                        If dri.CanDriveTheTrain(tmer) Then
                            If dri.dutyWork = "备车" Then
                                Continue For
                            End If
                            If tmer.dutywork = "吃饭" And dri.dutyWork = "已吃饭" Then
                                Continue For
                            End If
                            If upTainDirection = -1 Then
                                AttOffPlace = True
                            End If
                            AvaDris.Add(dri)
                        End If
                    End If
                Next
            End If
            If AttOffPlace = False Then
                '最大休息时间人接车 
                maxWaitTime = -1
                For Each dri As CSDriver In AvaDris

                    Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                    If waittime > maxWaitTime And waittime >= ChangePlaceRestTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime) Then
                        maxWaitTime = waittime
                        selectDri = dri
                    End If
                Next
            Else
                maxWaitTime = 24 * 3600 '最小
                For Each dri As CSDriver In AvaDris
                    Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                    If waittime < maxWaitTime Then
                        maxWaitTime = waittime
                        selectDri = dri
                    End If
                Next
            End If
            If selectDri IsNot Nothing Then
                If tmer.dutywork = "吃饭" Then
                    selectDri.dutyWork = "已吃饭"
                End If
                selectDri.AddMergedTrain(tmer)
                selectDri.RefreshState()
            Else
                Call AddANewDriverforMerged("暂编", tmer)
                If tmer.dutywork = "吃饭" Then
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).dutyWork = "已吃饭"
                End If
                CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).RefreshState()
            End If
            Me.ProgressBar1.PerformStep()
            System.Windows.Forms.Application.DoEvents()
          
        Next

        Me.ProgressBar1.Maximum = CSTrainsAndDrivers.CSDrivers.Count
        Me.ProgressBar1.Step = 1
        Dim cout As Integer = 1
        Dim ruku As New List(Of CSDriver)
        Dim chuku As New List(Of CSDriver)
        Dim chifan As New List(Of CSDriver)
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then
            For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                Me.ProgressBar1.PerformStep()
                If dri IsNot Nothing AndAlso dri.dutyWork = "替饭" Then
                    chifan.Add(dri)
                End If
                If dri IsNot Nothing AndAlso dri.DutySort = "暂编" Then
                    If dri.CSLinkTrain(UBound(dri.CSLinkTrain)).SecondStation.IsYard = True Then
                        dri.DutySort = "日勤班"
                        dri.OutPutCSdriverNo = "日勤班" + cout.ToString("00")
                        dri.CSdriverNo = "日勤班" + cout.ToString("00")
                        cout += 1
                        ruku.Add(dri)
                        Call RemoveCSDriverFromList(dri)
                        CSTrainsAndDrivers.CDayDrivers.Add(dri)
                    Else
                        If dri.CSLinkTrain(1).FirstStation.IsYard = True Then
                            dri.DutySort = "日勤班"
                            dri.OutPutCSdriverNo = "日勤班" + cout.ToString("00")
                            dri.CSdriverNo = "日勤班" + cout.ToString("00")
                            cout += 1
                            chuku.Add(dri)
                            Call RemoveCSDriverFromList(dri)
                            CSTrainsAndDrivers.CDayDrivers.Add(dri)
                        End If
                    End If
                    If dri.CSLinkTrain(UBound(dri.CSLinkTrain)).SecondStation.IsYard = False And dri.CSLinkTrain(1).FirstStation.IsYard = False Then
                        For i As Integer = 1 To UBound(dri.CSLinkTrain)
                            dri.CSLinkTrain(i).IsLinked = False
                        Next
                        RemoveDriver(dri)
                    End If
                End If
            Next
        End If
        If CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then
            Exit Sub
        End If
        For i As Integer = 0 To ruku.Count - 2
            For j As Integer = i + 1 To ruku.Count - 1
                If ruku(i).CSLinkTrain(UBound(ruku(i).CSLinkTrain)).CulEndTime > ruku(j).CSLinkTrain(UBound(ruku(j).CSLinkTrain)).CulEndTime Then
                    Dim tmpDri As CSDriver = ruku(i)
                    ruku(i) = ruku(j)
                    ruku(j) = tmpDri
                End If
            Next
        Next
        Dim closedDinner As New List(Of Integer)
        Dim dicRuku As New Dictionary(Of Integer, CSDriver)
        Dim closedDinner1 As New List(Of Integer)
        Dim dicChuku As New Dictionary(Of Integer, CSDriver)
        If CheckBox2.Checked = True Then
            Dim chazhi As Integer = ruku.Count - chifan.Count
            For i As Integer = ruku.Count - 1 To 0 Step -1
                Dim j As Integer = i - chazhi
                If j > -1 Then
                    For z As Integer = 1 To chifan(j).CSLinkTrain.Count - 1
                        ruku(i).AddTrain(chifan(j).CSLinkTrain(z))
                    Next
                    dicRuku.Add(j, ruku(i))
                    If closedDinner.Contains(j) = False Then
                        closedDinner.Add(j)
                    End If
                    ruku.RemoveAt(i)
                End If
            Next
        End If
        If CheckBox4.Checked = True Then
            For i As Integer = 0 To chuku.Count - 1
                Dim chazhi As Integer = chuku.Count - chifan.Count
                Dim j As Integer = i - chazhi
                If j < chifan.Count Then
                    Dim MergedCSTrain As New MergedCSLinkTrain
                    For z As Integer = 1 To chifan(j).CSLinkTrain.Count - 1
                        MergedCSTrain.AddCSLinkTrain(chifan(j).CSLinkTrain(z))
                    Next
                    If closedDinner1.Contains(j) = False Then
                        closedDinner1.Add(j)
                    End If
                    chuku(i).AddReMergedTrain(MergedCSTrain)
                    dicChuku.Add(j, chuku(i))
                    chuku.RemoveAt(i)
                    i -= 1
                End If
            Next
        End If
        If CheckBox3.Checked = True Then
            Dim chazhi As Integer = ruku.Count - chuku.Count
            For i As Integer = ruku.Count - 1 To 0 Step -1
                Dim j As Integer = -1
                If chazhi >= 0 Then
                    j = i - chazhi
                Else
                    j = i
                End If
                If j > -1 Then
                    Dim MergedCSTrain As New MergedCSLinkTrain
                    For z As Integer = 1 To chuku(j).CSLinkTrain.Count - 1
                        MergedCSTrain.AddCSLinkTrain(chuku(j).CSLinkTrain(z))
                    Next
                    chuku(i).AddMergedTrain(MergedCSTrain)
                    RemoveDriver(ruku(i))
                End If
            Next
        End If
      
        If CheckBox2.Checked = True And CheckBox4.Checked = True Then
            For i As Integer = 0 To closedDinner.Count - 1
                If closedDinner1.Contains(closedDinner(i)) Then
                    Dim MergedCSTrain As New MergedCSLinkTrain
                    For z As Integer = 1 To dicChuku(closedDinner(i)).CSLinkTrain.Count - 1
                        If dicChuku(closedDinner(i)).CSLinkTrain(z).CulStartTime > dicRuku(closedDinner(i)).CSLinkTrain(UBound(dicRuku(closedDinner(i)).CSLinkTrain)).CulEndTime Then
                            MergedCSTrain.AddCSLinkTrain(dicChuku(closedDinner(i)).CSLinkTrain(z))
                        End If
                    Next
                    dicRuku(closedDinner(i)).AddMergedTrain(MergedCSTrain)
                    RemoveDriver(dicChuku(closedDinner(i)))
                End If
            Next
        End If
        For i As Integer = 0 To closedDinner1.Count - 1
            If closedDinner.Contains(closedDinner1(i)) = False Then
                closedDinner.Add(closedDinner1(i))
            End If
        Next
        For i As Integer = 0 To closedDinner.Count - 1
            RemoveDriver(chifan(closedDinner(i)))
        Next
        Me.ProgressBar1.Visible = False
        MsgBox("处理完毕！")
        Me.Close()
    End Sub
End Class