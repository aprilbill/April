Public Class FrmOptiSchedule
    Dim selectDri As CSDriver
    Dim preCSLinkTrain As CSLinkTrain
    Dim afterCSLinkTrain As CSLinkTrain
    Public Sub New(ByVal driverid As String)

        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
            For Each Driver As CSDriver In CSTrainsAndDrivers.CSDrivers
                If Driver IsNot Nothing AndAlso Driver.CSDriverID = driverid Then
                    selectDri = Driver
                    TextBox1.Text = Driver.CSdriverNo
                    Label4.Text = "总驾驶距离：" & Driver.DriveDistance & "公里，任务开始时间" & Driver.CSLinkTrain(1).StartStaName & BeTime(Driver.CSLinkTrain(1).StartTime) & "，任务结束时间" & Driver.CSLinkTrain(UBound(Driver.CSLinkTrain)).EndStaName & BeTime(Driver.CSLinkTrain(UBound(Driver.CSLinkTrain)).EndTime)
                    afterCSLinkTrain = CheckInterval()
                    preCSLinkTrain = CheckInterval(True)
                    initalAfter()
                    initalPre()
                End If
            Next
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Checked = False
        Else
            CheckBox2.Checked = True
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox1.Checked = False
        Else
            CheckBox1.Checked = True
        End If
    End Sub

    Dim preDutyDetail As New Dictionary(Of String, List(Of Integer)) '每个任务在第几列(前序)
    Dim afterDutyDetail As New Dictionary(Of String, List(Of Integer)) '每个任务在第几列(后续)
    Public Sub initalAfter() '初始化后续接车列表
        afterDutyDetail.Clear()
        For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
            If IsNothing(afterCSLinkTrain) = False AndAlso AddLitterTime(CSTrainsAndDrivers.CSLinkTrains(i).StartTime) < AddLitterTime(afterCSLinkTrain.StartTime) Then
                Continue For
            Else
                If IsNothing(afterCSLinkTrain) AndAlso AddLitterTime(CSTrainsAndDrivers.CSLinkTrains(i).StartTime) < AddLitterTime(selectDri.CSLinkTrain(UBound(selectDri.CSLinkTrain)).EndTime) Then
                    Continue For
                End If
            End If
            If AddLitterTime(CSTrainsAndDrivers.CSLinkTrains(i).StartTime) > AddLitterTime(selectDri.CSLinkTrain(UBound(selectDri.CSLinkTrain)).EndTime) + 3600 Then
                Continue For
            End If
            If CheckBox3.Checked = False Then
                If CSTrainsAndDrivers.CSLinkTrains(i).StartStaName <> selectDri.CSLinkTrain(UBound(selectDri.CSLinkTrain)).EndStaName Then
                    Continue For
                End If
            End If
            If CSTrainsAndDrivers.CSLinkTrains(i).IsLinked = True Then
                Dim ifgotit As Boolean = False
                For Each tmpDriver As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If tmpDriver IsNot Nothing Then
                        For z = 1 To UBound(tmpDriver.CSLinkTrain)
                            If tmpDriver.CSLinkTrain(z).StartTime = CSTrainsAndDrivers.CSLinkTrains(i).StartTime And CSTrainsAndDrivers.CSLinkTrains(i).EndTime = tmpDriver.CSLinkTrain(z).EndTime Then
                                If afterDutyDetail.Keys.Contains(tmpDriver.CSdriverNo) = False Then
                                    afterDutyDetail.Add(tmpDriver.CSdriverNo, New List(Of Integer))
                                End If
                                afterDutyDetail(tmpDriver.CSdriverNo).Add(z)
                                ifgotit = True
                                Exit For
                            End If
                        Next
                        If ifgotit = True Then
                            Exit For
                        End If
                    End If
                Next
            End If
        Next
        Dim tmpData As New DataTable
        If afterDutyDetail.Count > 0 Then
            Dim maxcoloum As Integer = 0
            For Each sduty As String In afterDutyDetail.Keys
                If maxcoloum < afterDutyDetail(sduty).Count Then
                    maxcoloum = afterDutyDetail(sduty).Count
                End If
            Next
            tmpData.Columns.Add("涉及任务", GetType(String))
            tmpData.Columns.Add("任务总驾驶里程", GetType(Integer))
            For i As Integer = 1 To maxcoloum
                tmpData.Columns.Add("推荐子任务" & i.ToString, GetType(String))
            Next
            For Each sduty As String In afterDutyDetail.Keys
                tmpData.Rows.Add()
                tmpData.Rows(tmpData.Rows.Count - 1)(0) = sduty
                For Each tmpDriver As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If tmpDriver IsNot Nothing AndAlso tmpDriver.CSdriverNo = sduty Then
                        tmpData.Rows(tmpData.Rows.Count - 1)(1) = tmpDriver.DriveDistance
                        For i As Integer = 0 To afterDutyDetail(sduty).Count - 1
                            tmpData.Rows(tmpData.Rows.Count - 1)(i + 2) = BeTime(tmpDriver.CSLinkTrain(afterDutyDetail(sduty)(i)).StartTime) & tmpDriver.CSLinkTrain(afterDutyDetail(sduty)(i)).StartStaName & "->" & BeTime(tmpDriver.CSLinkTrain(afterDutyDetail(sduty)(i)).EndTime) & tmpDriver.CSLinkTrain(afterDutyDetail(sduty)(i)).EndStaName
                        Next
                        Exit For
                    End If
                Next
            Next
        End If
        DataGridView2.DataSource = tmpData
    End Sub
    Public Sub initalPre() '初始化前序接车列表
        preDutyDetail.Clear()
        For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
            If IsNothing(preCSLinkTrain) = False AndAlso AddLitterTime(CSTrainsAndDrivers.CSLinkTrains(i).EndTime) > AddLitterTime(preCSLinkTrain.EndTime) Then
                Continue For
            Else
                If IsNothing(preCSLinkTrain) AndAlso AddLitterTime(CSTrainsAndDrivers.CSLinkTrains(i).EndTime) > AddLitterTime(selectDri.CSLinkTrain(1).StartTime) Then
                    Continue For
                End If
            End If
            If AddLitterTime(CSTrainsAndDrivers.CSLinkTrains(i).EndTime) < AddLitterTime(selectDri.CSLinkTrain(1).StartTime) - 3600 Then
                Continue For
            End If
            If CheckBox3.Checked = False Then
                If CSTrainsAndDrivers.CSLinkTrains(i).EndStaName <> selectDri.CSLinkTrain(1).StartStaName Then
                    Continue For
                End If
            End If
            If CSTrainsAndDrivers.CSLinkTrains(i).IsLinked = True Then
                Dim ifgotit As Boolean = False
                For Each tmpDriver As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If tmpDriver IsNot Nothing Then
                        For z = 1 To UBound(tmpDriver.CSLinkTrain)
                            If tmpDriver.CSLinkTrain(z).StartTime = CSTrainsAndDrivers.CSLinkTrains(i).StartTime And CSTrainsAndDrivers.CSLinkTrains(i).EndTime = tmpDriver.CSLinkTrain(z).EndTime Then
                                If preDutyDetail.Keys.Contains(tmpDriver.CSdriverNo) = False Then
                                    preDutyDetail.Add(tmpDriver.CSdriverNo, New List(Of Integer))
                                End If
                                preDutyDetail(tmpDriver.CSdriverNo).Add(z)
                                ifgotit = True
                                Exit For
                            End If
                        Next
                        If ifgotit = True Then
                            Exit For
                        End If
                    End If
                Next
            End If
        Next
        Dim tmpData As New DataTable
        If preDutyDetail.Count > 0 Then
            Dim maxcoloum As Integer = 0
            For Each sduty As String In preDutyDetail.Keys
                If maxcoloum < preDutyDetail(sduty).Count Then
                    maxcoloum = preDutyDetail(sduty).Count
                End If
            Next
            tmpData.Columns.Add("涉及任务", GetType(String))
            tmpData.Columns.Add("任务总驾驶里程", GetType(Integer))
            For i As Integer = 1 To maxcoloum
                tmpData.Columns.Add("推荐子任务" & i.ToString, GetType(String))
            Next
            For Each sduty As String In preDutyDetail.Keys
                tmpData.Rows.Add()
                tmpData.Rows(tmpData.Rows.Count - 1)(0) = sduty
                For Each tmpDriver As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If tmpDriver IsNot Nothing AndAlso tmpDriver.CSdriverNo = sduty Then
                        tmpData.Rows(tmpData.Rows.Count - 1)(1) = tmpDriver.DriveDistance
                        For i As Integer = 0 To preDutyDetail(sduty).Count - 1
                            tmpData.Rows(tmpData.Rows.Count - 1)(i + 2) = BeTime(tmpDriver.CSLinkTrain(preDutyDetail(sduty)(i)).StartTime) & tmpDriver.CSLinkTrain(preDutyDetail(sduty)(i)).StartStaName & "->" & BeTime(tmpDriver.CSLinkTrain(preDutyDetail(sduty)(i)).EndTime) & tmpDriver.CSLinkTrain(preDutyDetail(sduty)(i)).EndStaName
                        Next
                        Exit For
                    End If
                Next
            Next
        End If
        DataGridView1.DataSource = tmpData
    End Sub
    Public Function CheckInterval(Optional Direct As Boolean = False) As CSLinkTrain  '判断是否满足间隔,默认正向
        Dim interval As Integer = 0
        If Direct = False Then
            For i = 0 To ChangeStationList.Count - 1
                If ChangeStationList(i).Name = selectDri.CSLinkTrain(UBound(selectDri.CSLinkTrain)).EndStaName And ChangeStationList(i).JiaoLuName = selectDri.CSLinkTrain(UBound(selectDri.CSLinkTrain)).RoutingName Then
                    If (ChangeStationList(i).Direction = selectDri.CSLinkTrain(UBound(selectDri.CSLinkTrain)).UpOrDown Or ChangeStationList(i).Direction = 2) Then
                        If ChangeStationList(i).TimeSpanList.EndTime = ChangeStationList(i).TimeSpanList.StartTime Then '没有时间限制
                            interval = ChangeStationList(i).FollowNo
                            Exit For
                        End If
                        If AddLitterTime(ChangeStationList(i).TimeSpanList.StartTime) <= AddLitterTime(selectDri.CSLinkTrain(UBound(selectDri.CSLinkTrain)).EndTime) And AddLitterTime(selectDri.CSLinkTrain(UBound(selectDri.CSLinkTrain)).EndTime) <= AddLitterTime(ChangeStationList(i).TimeSpanList.EndTime) Then
                            interval = ChangeStationList(i).FollowNo
                            Exit For
                        End If
                    End If
                End If
            Next
            If interval <> 0 Then '没有间隔按时间则默认为可以接车
                Dim TrainList As New List(Of Integer)
                Dim minTime As Integer = 1000000000
                Dim minIndex As Integer = -1
                For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
                    If CSTrainsAndDrivers.CSLinkTrains(i).StartStaName = selectDri.CSLinkTrain(UBound(selectDri.CSLinkTrain)).EndStaName Then
                        If AddLitterTime(CSTrainsAndDrivers.CSLinkTrains(i).StartTime) > AddLitterTime(selectDri.CSLinkTrain(UBound(selectDri.CSLinkTrain)).EndTime) Then
                            TrainList.Add(i)
                            If CSTrainsAndDrivers.CSLinkTrains(i).nCheDiID = selectDri.CSLinkTrain(UBound(selectDri.CSLinkTrain)).nCheDiID And minTime > AddLitterTime(CSTrainsAndDrivers.CSLinkTrains(i).StartTime) - AddLitterTime(selectDri.CSLinkTrain(UBound(selectDri.CSLinkTrain)).EndTime) Then
                                minTime = AddLitterTime(CSTrainsAndDrivers.CSLinkTrains(i).StartTime) - AddLitterTime(selectDri.CSLinkTrain(UBound(selectDri.CSLinkTrain)).EndTime)
                                minIndex = i
                            End If
                        End If

                    End If
                Next
                If TrainList.Count >= interval And minIndex > -1 Then
                    For i As Integer = 0 To TrainList.Count - 2
                        For j As Integer = i + 1 To TrainList.Count - 1
                            If AddLitterTime(CSTrainsAndDrivers.CSLinkTrains(TrainList(i)).StartTime) > AddLitterTime(CSTrainsAndDrivers.CSLinkTrains(TrainList(j)).StartTime) Then
                                Dim tmp As Integer = TrainList(i)
                                TrainList(i) = TrainList(j)
                                TrainList(j) = tmp
                            End If
                        Next
                    Next
                    For i As Integer = 0 To TrainList.Count - 1
                        If TrainList(i) = minIndex Then
                            If i + interval <= TrainList.Count - 1 Then
                                Dim skipBeiche As Integer = 0
                                For j As Integer = 1 To interval
                                    If CSTrainsAndDrivers.CSLinkTrains(TrainList(i + j)).isBeiChe = 2 Then
                                        skipBeiche += 1
                                    End If
                                Next
                                If i + interval + skipBeiche <= TrainList.Count - 1 Then
                                    Return CSTrainsAndDrivers.CSLinkTrains(TrainList(i + interval + skipBeiche))
                                End If
                            End If
                            Exit For
                        End If
                    Next

                End If
            Else
                Return Nothing
            End If
        Else
            For i = 0 To ChangeStationList.Count - 1
                If ChangeStationList(i).Name = selectDri.CSLinkTrain(1).StartStaName Then
                    For z As Integer = 1 To UBound(Jiaolu)
                        If Jiaolu(z).JiaoluName = ChangeStationList(i).JiaoLuName And Jiaolu(z).ReJiaoluName = selectDri.CSLinkTrain(1).RoutingName Then
                            If ChangeStationList(i).TimeSpanList.EndTime = ChangeStationList(i).TimeSpanList.StartTime Then '没有时间限制
                                interval = ChangeStationList(i).FollowNo
                                Exit For
                            End If
                            If AddLitterTime(ChangeStationList(i).TimeSpanList.StartTime) <= AddLitterTime(selectDri.CSLinkTrain(1).StartTime) And AddLitterTime(selectDri.CSLinkTrain(1).StartTime) <= AddLitterTime(ChangeStationList(i).TimeSpanList.EndTime) Then
                                interval = ChangeStationList(i).FollowNo
                                Exit For
                            End If
                        End If
                    Next
                End If
            Next
            If interval <> 0 Then '没有间隔按时间则默认为可以接车
                Dim TrainList As New List(Of Integer)
                Dim minTime As Integer = 1000000000
                Dim minIndex As Integer = -1
                For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
                    If CSTrainsAndDrivers.CSLinkTrains(i).EndStaName = selectDri.CSLinkTrain(1).StartStaName Then
                        If AddLitterTime(CSTrainsAndDrivers.CSLinkTrains(i).EndTime) < AddLitterTime(selectDri.CSLinkTrain(1).StartTime) Then
                            TrainList.Add(i)
                            If CSTrainsAndDrivers.CSLinkTrains(i).nCheDiID = selectDri.CSLinkTrain(1).nCheDiID And minTime > AddLitterTime(selectDri.CSLinkTrain(1).StartTime) - AddLitterTime(CSTrainsAndDrivers.CSLinkTrains(i).EndTime) Then
                                minTime = AddLitterTime(selectDri.CSLinkTrain(1).StartTime) - AddLitterTime(CSTrainsAndDrivers.CSLinkTrains(i).EndTime)
                                minIndex = i
                            End If
                        End If

                    End If
                Next
                If TrainList.Count >= interval And minIndex > -1 Then
                    For i As Integer = 0 To TrainList.Count - 2
                        For j As Integer = i + 1 To TrainList.Count - 1
                            If AddLitterTime(CSTrainsAndDrivers.CSLinkTrains(TrainList(i)).EndTime) < AddLitterTime(CSTrainsAndDrivers.CSLinkTrains(TrainList(j)).EndTime) Then
                                Dim tmp As Integer = TrainList(i)
                                TrainList(i) = TrainList(j)
                                TrainList(j) = tmp
                            End If
                        Next
                    Next
                    For i As Integer = 0 To TrainList.Count - 1
                        If TrainList(i) = minIndex Then
                            If i + interval <= TrainList.Count - 1 Then
                                Dim skipBeiche As Integer = 0
                                For j As Integer = 1 To interval
                                    If CSTrainsAndDrivers.CSLinkTrains(TrainList(i + j)).isBeiChe = 1 Then
                                        skipBeiche += 1
                                    End If
                                Next
                                If i + interval + skipBeiche <= TrainList.Count - 1 Then
                                    Return CSTrainsAndDrivers.CSLinkTrains(TrainList(i + interval + skipBeiche))
                                End If
                            End If
                            Exit For
                        End If
                    Next

                End If
            Else
                Return Nothing
            End If
        End If
        Return Nothing
    End Function

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        initalAfter()
        initalPre()
    End Sub

    Dim selectrow1 As Integer = -1
    Dim selectcolunm1 As Integer = -1
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        selectrow1 = -1
        selectcolunm1 = -1
        If e.RowIndex >= 0 And e.ColumnIndex > 1 Then
            If DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value IsNot Nothing AndAlso DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString <> "" Then
                selectcolunm1 = e.ColumnIndex
                selectrow1 = e.RowIndex
                Dim intervaltime As String = ""
                Dim afterdis As Double = 0.0
                Dim predis As Double = 0.0
                Dim dicState As Boolean = True
                Dim OdicState As Boolean = True
                For Each tmpDriver As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If tmpDriver IsNot Nothing AndAlso tmpDriver.CSdriverNo = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString Then
                        intervaltime = BeTime(AddLitterTime(selectDri.CSLinkTrain(1).StartTime) - AddLitterTime(tmpDriver.CSLinkTrain(preDutyDetail(DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString)(e.ColumnIndex - 2)).EndTime))
                        For i As Integer = 1 To UBound(tmpDriver.CSLinkTrain)
                            If i > preDutyDetail(DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString)(e.ColumnIndex - 2) Then
                                afterdis += tmpDriver.CSLinkTrain(i).distance
                            Else
                                predis += tmpDriver.CSLinkTrain(i).distance
                            End If
                        Next
                        Select Case tmpDriver.DutySort
                            Case "早班"
                                If afterdis <> 0 And afterdis < CS_MorningMinLength Then
                                    dicState = False
                                End If
                            Case "白班"
                                If afterdis <> 0 And afterdis < CS_DayMinLength Then
                                    dicState = False
                                End If
                            Case "夜班"
                                If afterdis <> 0 And afterdis < CS_NightMinLength Then
                                    dicState = False
                                End If
                        End Select
                        Select Case selectDri.DutySort
                            Case "早班"
                                If selectDri.DriveDistance + predis > CS_MorningMaxLength Then
                                    OdicState = False
                                End If
                            Case "白班"
                                If selectDri.DriveDistance + predis > CS_DayMaxLength Then
                                    OdicState = False
                                End If
                            Case "夜班"
                                If selectDri.DriveDistance + predis > CS_NightMaxLength Then
                                    OdicState = False
                                End If
                        End Select
                    End If
                Next
                If afterdis = 0 Then
                    TextBox2.Text = "与目标任务开始时间间隔：" & intervaltime & "，优化后" & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "可节省；目标任务驾驶里程：" & (selectDri.DriveDistance + predis).ToString
                Else
                    TextBox2.Text = "与目标任务开始时间间隔：" & intervaltime & "，优化后" & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "驾驶里程为" & afterdis.ToString & "；目标任务驾驶里程：" & (selectDri.DriveDistance + predis).ToString
                End If
                If dicState = False Then
                    TextBox2.Text &= vbCrLf + "优化后" & DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString & "低于最低公里数，不推荐！"
                End If
                If OdicState = False Then
                    TextBox2.Text &= vbCrLf + "优化后目标任务" & selectDri.CSdriverNo & "超过最高公里数，不推荐！"
                End If
            End If
        End If
    End Sub

    Dim selectrow2 As Integer = -1
    Dim selectcolunm2 As Integer = -1
    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        selectcolunm2 = -1
        selectrow2 = -1
        If e.RowIndex >= 0 And e.ColumnIndex > 1 Then
            If DataGridView2.Rows(e.RowIndex).Cells(e.ColumnIndex).Value IsNot Nothing AndAlso DataGridView2.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString <> "" Then
                selectcolunm2 = e.ColumnIndex
                selectrow2 = e.RowIndex
                Dim intervaltime As String = ""
                Dim afterdis As Double = 0.0
                Dim predis As Double = 0.0
                Dim dicState As Boolean = True
                Dim OdicState As Boolean = True
                For Each tmpDriver As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If tmpDriver IsNot Nothing AndAlso tmpDriver.CSdriverNo = DataGridView2.Rows(e.RowIndex).Cells(0).Value.ToString Then
                        intervaltime = BeTime(AddLitterTime(tmpDriver.CSLinkTrain(afterDutyDetail(DataGridView2.Rows(e.RowIndex).Cells(0).Value.ToString)(e.ColumnIndex - 2)).StartTime) - AddLitterTime(selectDri.CSLinkTrain(UBound(selectDri.CSLinkTrain)).EndTime))
                        For i As Integer = 1 To UBound(tmpDriver.CSLinkTrain)
                            If i >= afterDutyDetail(DataGridView2.Rows(e.RowIndex).Cells(0).Value.ToString)(e.ColumnIndex - 2) Then
                                afterdis += tmpDriver.CSLinkTrain(i).distance
                            Else
                                predis += tmpDriver.CSLinkTrain(i).distance
                            End If
                        Next
                        Select Case tmpDriver.DutySort
                            Case "早班"
                                If predis <> 0 And predis < CS_MorningMinLength Then
                                    dicState = False
                                End If
                            Case "白班"
                                If predis <> 0 And predis < CS_DayMinLength Then
                                    dicState = False
                                End If
                            Case "夜班"
                                If predis <> 0 And predis < CS_NightMinLength Then
                                    dicState = False
                                End If
                        End Select
                        Select Case selectDri.DutySort
                            Case "早班"
                                If selectDri.DriveDistance + afterdis > CS_MorningMaxLength Then
                                    OdicState = False
                                End If
                            Case "白班"
                                If selectDri.DriveDistance + afterdis > CS_DayMaxLength Then
                                    OdicState = False
                                End If
                            Case "夜班"
                                If selectDri.DriveDistance + afterdis > CS_NightMaxLength Then
                                    OdicState = False
                                End If
                        End Select
                    End If
                Next
                If predis = 0 Then
                    TextBox3.Text = "与目标任务结束时间间隔：" & intervaltime & "，优化后" & DataGridView2.Rows(e.RowIndex).Cells(0).Value.ToString & "可节省；目标任务驾驶里程：" & (selectDri.DriveDistance + afterdis).ToString
                Else
                    TextBox3.Text = "与目标任务结束时间间隔：" & intervaltime & "，优化后" & DataGridView2.Rows(e.RowIndex).Cells(0).Value.ToString & "驾驶里程为" & predis.ToString & "；目标任务驾驶里程：" & (selectDri.DriveDistance + afterdis).ToString
                End If
                If dicState = False Then
                    TextBox3.Text &= vbCrLf + "优化后" & DataGridView2.Rows(e.RowIndex).Cells(0).Value.ToString & "低于最低公里数，不推荐！"
                End If
                If OdicState = False Then
                    TextBox3.Text &= vbCrLf + "优化后目标任务" & selectDri.CSdriverNo & "超过最高公里数，不推荐！"
                End If
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.Checked = True Then
            If selectcolunm1 <> -1 And selectrow1 <> -1 Then
                If DataGridView1.Rows(selectrow1).Cells(selectcolunm1).Value IsNot Nothing AndAlso DataGridView1.Rows(selectrow1).Cells(selectcolunm1).Value.ToString <> "" Then
                    If MsgBox("确认是否处理" & DataGridView1.Rows(selectrow1).Cells(0).Value.ToString & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                        Exit Sub
                    End If
                    For Each tmpDriver As CSDriver In CSTrainsAndDrivers.CSDrivers
                        If tmpDriver IsNot Nothing AndAlso tmpDriver.CSdriverNo = DataGridView1.Rows(selectrow1).Cells(0).Value.ToString Then
                            For i As Integer = UBound(tmpDriver.CSLinkTrain) To 1 Step -1
                                If i <= preDutyDetail(DataGridView1.Rows(selectrow1).Cells(0).Value.ToString)(selectcolunm1 - 2) Then
                                    selectDri.ReAddTrain(tmpDriver.CSLinkTrain(i))
                                End If
                            Next
                            If preDutyDetail(DataGridView1.Rows(selectrow1).Cells(0).Value.ToString)(selectcolunm1 - 2) = UBound(tmpDriver.CSLinkTrain) Then
                                RemoveDriver(tmpDriver)
                            Else
                                tmpDriver.ReRemoveTrain(preDutyDetail(DataGridView1.Rows(selectrow1).Cells(0).Value.ToString)(selectcolunm1 - 2))
                                tmpDriver.RefreshState()
                            End If
                        End If
                    Next
                    MsgBox("处理完毕！")
                    selectDri.RefreshState()
                    Call CSRefreshDiagram()
                    Me.Close()
                End If
            End If
        Else
            If selectcolunm2 <> -1 And selectrow2 <> -1 Then
                If DataGridView2.Rows(selectrow2).Cells(selectcolunm2).Value IsNot Nothing AndAlso DataGridView2.Rows(selectrow2).Cells(selectcolunm2).Value.ToString <> "" Then
                    If MsgBox("确认是否处理" & DataGridView2.Rows(selectrow2).Cells(0).Value.ToString & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                        Exit Sub
                    End If
                    For Each tmpDriver As CSDriver In CSTrainsAndDrivers.CSDrivers
                        If tmpDriver IsNot Nothing AndAlso tmpDriver.CSdriverNo = DataGridView2.Rows(selectrow2).Cells(0).Value.ToString Then
                            For i As Integer = 1 To UBound(tmpDriver.CSLinkTrain)
                                If i >= afterDutyDetail(DataGridView2.Rows(selectrow2).Cells(0).Value.ToString)(selectcolunm2 - 2) Then
                                    selectDri.AddTrain(tmpDriver.CSLinkTrain(i))
                                End If
                            Next
                            If afterDutyDetail(DataGridView2.Rows(selectrow2).Cells(0).Value.ToString)(selectcolunm2 - 2) = 1 Then
                                RemoveDriver(tmpDriver)
                            Else
                                tmpDriver.RemoveTrain(afterDutyDetail(DataGridView2.Rows(selectrow2).Cells(0).Value.ToString)(selectcolunm2 - 2) - 1)
                                tmpDriver.RefreshState()
                            End If
                        End If
                    Next
                    MsgBox("处理完毕！")
                    selectDri.RefreshState()
                    Call CSRefreshDiagram()
                    Me.Close()
                End If
            End If
        End If
    End Sub
End Class