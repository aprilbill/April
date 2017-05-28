Public Class FrmHandAssignDriverR

    Public ParentWindow As frmCSTimeTableMain
    Public OldStartSta As String = ""

    Private Sub FrmHandAssignDriver_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DGVProperty.Rows.Clear()
        Me.DGVProperty.Rows.Add("任务编号")
        Me.DGVProperty.Rows.Add("班种")
        Me.DGVProperty.Rows.Add("驾驶公里")
        Me.DGVProperty.Rows.Add("工作时间")
        Me.DGVProperty.Rows.Add("驾驶时间")
        Call UpdateDestinationCmb()
        Dim index As Integer = CmbDutySort.Items.IndexOf(CSTrainsAndDrivers.CurEditDriver.DutySort)
        If index <> -1 Then
            Me.CmbDutySort.SelectedIndex = index
        Else
            Me.CmbDutySort.SelectedIndex = 0
        End If
        Me.Timer1.Enabled = True
        Me.CmbStyle.SelectedIndex = 0
        Call SortCSLinkTrain(True)

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CSTrainsAndDrivers.CurEditDriver = Nothing
        Me.ParentWindow.ListAllViewInfo()
        Call CSRefreshDiagram()
        Me.Close()
    End Sub

    Private Sub CmbDutySort_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbDutySort.SelectedIndexChanged
        CSTrainsAndDrivers.CurEditDriver.DutySort = Me.CmbDutySort.Text
        Call UpdateDGV()
        Call UpdateDestinationCmb()

        Dim index As Integer = -1
        Select Case CSTrainsAndDrivers.CurEditDriver.DutySort
            Case "早班"
                index = CSTrainsAndDrivers.MorningDrivers.FindIndex(Function(value As CSDriver)
                                                                        Return value = CSTrainsAndDrivers.CurEditDriver
                                                                    End Function)
            Case "白班"
                index = CSTrainsAndDrivers.DayDrivers.FindIndex(Function(value As CSDriver)
                                                                    Return value = CSTrainsAndDrivers.CurEditDriver
                                                                End Function)
            Case "日勤班"
                index = CSTrainsAndDrivers.CDayDrivers.FindIndex(Function(value As CSDriver)
                                                                     Return value = CSTrainsAndDrivers.CurEditDriver
                                                                 End Function)
            Case "夜班"
                index = CSTrainsAndDrivers.NightDrivers.FindIndex(Function(value As CSDriver)
                                                                      Return value = CSTrainsAndDrivers.CurEditDriver
                                                                  End Function)
            Case Else
                index = CSTrainsAndDrivers.OtherDrivers.FindIndex(Function(value As CSDriver)
                                                                      Return value = CSTrainsAndDrivers.CurEditDriver
                                                                  End Function)
        End Select
        If index = -1 Then
            For Each dri As CSDriver In CSTrainsAndDrivers.MorningDrivers
                If dri Is CSTrainsAndDrivers.CurEditDriver Then
                    CSTrainsAndDrivers.MorningDrivers.Remove(CSTrainsAndDrivers.CurEditDriver)
                    GoTo L
                End If
            Next
            For Each dri As CSDriver In CSTrainsAndDrivers.DayDrivers
                If dri Is CSTrainsAndDrivers.CurEditDriver Then
                    CSTrainsAndDrivers.DayDrivers.Remove(CSTrainsAndDrivers.CurEditDriver)
                    GoTo L
                End If
            Next
            For Each dri As CSDriver In CSTrainsAndDrivers.CDayDrivers
                If dri Is CSTrainsAndDrivers.CurEditDriver Then
                    CSTrainsAndDrivers.CDayDrivers.Remove(CSTrainsAndDrivers.CurEditDriver)
                    GoTo L
                End If
            Next
            For Each dri As CSDriver In CSTrainsAndDrivers.NightDrivers
                If dri Is CSTrainsAndDrivers.CurEditDriver Then
                    CSTrainsAndDrivers.NightDrivers.Remove(CSTrainsAndDrivers.CurEditDriver)
                    GoTo L
                End If
            Next
            For Each dri As CSDriver In CSTrainsAndDrivers.OtherDrivers
                If dri Is CSTrainsAndDrivers.CurEditDriver Then
                    CSTrainsAndDrivers.OtherDrivers.Remove(CSTrainsAndDrivers.CurEditDriver)
                    GoTo L
                End If
            Next
L:
            Select Case CSTrainsAndDrivers.CurEditDriver.DutySort
                Case "早班"
                    CSTrainsAndDrivers.MorningDrivers.Add(CSTrainsAndDrivers.CurEditDriver)
                Case "白班"
                    CSTrainsAndDrivers.DayDrivers.Add(CSTrainsAndDrivers.CurEditDriver)
                Case "日勤班"
                    CSTrainsAndDrivers.CDayDrivers.Add(CSTrainsAndDrivers.CurEditDriver)
                Case "夜班"
                    CSTrainsAndDrivers.NightDrivers.Add(CSTrainsAndDrivers.CurEditDriver)
                Case Else
                    CSTrainsAndDrivers.OtherDrivers.Add(CSTrainsAndDrivers.CurEditDriver)
            End Select
        End If
        Me.ParentWindow.ListAllViewInfo()
        Call CSRefreshDiagram()
    End Sub

    Public Sub UpdateDGV()
        Me.DGVProperty.Rows(0).Cells("值").Value = CSTrainsAndDrivers.CurEditDriver.CSdriverNo
        Me.DGVProperty.Rows(1).Cells("值").Value = CSTrainsAndDrivers.CurEditDriver.DutySort
        Me.DGVProperty.Rows(2).Cells("值").Value = CSTrainsAndDrivers.CurEditDriver.DriveDistance
        Me.DGVProperty.Rows(3).Cells("值").Value = BeTime(CSTrainsAndDrivers.CurEditDriver.WorkTime)
        Me.DGVProperty.Rows(4).Cells("值").Value = BeTime(CSTrainsAndDrivers.CurEditDriver.DriveTime)
    End Sub

    Public Sub UpdateDestinationCmb()
        Dim desList As New List(Of String)
        Me.CmbDestination.Items.Clear()
        For Each train As CSLinkTrain In CSTrainsAndDrivers.CSLinkTrains
            If train IsNot Nothing AndAlso train.IsLinked = False Then
                If train.EndStaName = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).StartStaName AndAlso _
                    train.CulEndTime <= CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).CulStartTime Then
                    Dim startsta As String = train.StartStaName
                    Dim index As Integer = desList.FindIndex(Function(value As String)
                                                                 Return value = startsta
                                                             End Function)
                    If index < 0 Then
                        desList.Add(startsta)
                    End If
                End If
            End If
        Next
        For Each staname As String In desList
            Me.CmbDestination.Items.Add(staname)
        Next
        If Me.CmbDestination.Items.Count > 0 Then
            Me.CmbDestination.SelectedIndex = 0
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Me.CmbDestination.Text <> "" Then
            Dim selecttrain As MergedCSLinkTrain = Nothing
            Dim desName As String = Me.CmbDestination.Text
            Dim Style As String = Me.CmbStyle.Text
            Dim restNo As Integer = 1
            Try
                restNo = Integer.Parse(restTimes.Text)
            Catch ex As Exception
                restTimes.Text = "1"
            End Try
            Dim restdirection As Integer = 2
            If Style = "休息后接车" Then
                For i As Integer = 0 To ChangeStationList.Count - 1
                    If ChangeStationList(i).Name = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).StartStaName Then
                        restdirection = ChangeStationList(i).UpTrainDirection
                        Exit For
                    End If
                Next
            End If

            Dim dinnerendtime As Integer = -1
            Dim selectDinnerItem As typeDinnerStation = Nothing
            Dim dinneritemFound As Boolean = False
            If Style = "吃饭后接车" Then
                For i As Integer = 1 To UBound(sysDinnerStation)
                    For j As Integer = 1 To UBound(Jiaolu)
                        If Jiaolu(j).JiaoluName = sysDinnerStation(i).Routing And Jiaolu(j).ReJiaoluName = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).RoutingName Then
                            If CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).StartStaName = sysDinnerStation(i).DinnerStationName And AddLitterTime(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).StartTime) >= AddLitterTime(sysDinnerStation(i).dinnerStartTime) + sysDinnerStation(i).DinnerTime And AddLitterTime(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).StartTime) <= AddLitterTime(sysDinnerStation(i).dinnerEndTime) + sysDinnerStation(i).DinnerTime Then '防止白班出现2次吃饭，也只有白班可能出现
                                If CSTrainsAndDrivers.CurEditDriver.FlagDinner = True Then
                                    Dim ifhaveSameDinnerItem As Boolean = False
                                    For Each dinneritem As typeDinnerStation In CSTrainsAndDrivers.CurEditDriver.AllDinnerInfo.Values
                                        If sysDinnerStation(i).dinnerStartTime = dinneritem.dinnerStartTime And sysDinnerStation(i).dinnerEndTime = dinneritem.dinnerEndTime And sysDinnerStation(i).DinnerStationName = dinneritem.DinnerStationName And sysDinnerStation(i).dutySort = dinneritem.dutySort Then
                                            ifhaveSameDinnerItem = True
                                            Exit For
                                        End If
                                    Next
                                    If ifhaveSameDinnerItem = True Then
                                        Continue For
                                    End If
                                End If
                                dinnerendtime = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).CulStartTime - sysDinnerStation(i).DinnerTime
                                selectDinnerItem = sysDinnerStation(i)
                                dinneritemFound = True
                                Exit For
                            End If
                        End If
                    Next
                Next
                If dinneritemFound = False Then
                    MsgBox("没有找到符合吃饭参数的任务！", vbQuestion + vbOKOnly, "提示")
                    Exit Sub
                End If
            End If
            Dim zhijiejieche As Integer = 100000000
            For Each train As MergedCSLinkTrain In CSTrainsAndDrivers.MergedCSLinkTrains
                Select Case Style
                    Case "直接接车"
                        If train IsNot Nothing AndAlso train.IsLinked = False AndAlso train.EndStaName = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).StartStaName AndAlso train.StartStaName = desName AndAlso _
                                            AddLitterTime(train.EndTime) <= AddLitterTime(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).StartTime) Then
                            If zhijiejieche > AddLitterTime(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).StartTime) - AddLitterTime(train.EndTime) Then
                                selecttrain = train
                                zhijiejieche = AddLitterTime(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).StartTime) - AddLitterTime(train.EndTime)
                            End If

                        End If
                    Case "休息后接车"
                        If train IsNot Nothing AndAlso train.IsLinked = False AndAlso train.EndStaName = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).StartStaName AndAlso train.StartStaName = desName AndAlso _
                              AddLitterTime(train.EndTime) < AddLitterTime(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).StartTime) Then
                            If CSTrainsAndDrivers.CurEditDriver.CheckIfInterval(restNo, restdirection, train.CSLinkTrains(UBound(train.CSLinkTrains)), True) Then
                                selecttrain = train
                                Exit For
                            End If
                        End If
                    Case "吃饭后接车"
                        If train IsNot Nothing AndAlso train.IsLinked = False AndAlso train.EndStaName = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).StartStaName AndAlso train.StartStaName = desName AndAlso _
                                            train.CulEndTime <= dinnerendtime AndAlso _
                                            train.nCheDiID <> CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).nCheDiID Then
                            selecttrain = train
                            CSTrainsAndDrivers.CurEditDriver.FlagDinner = True
                            CSTrainsAndDrivers.CurEditDriver.DinnerStation = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).StartStaName
                            CSTrainsAndDrivers.CurEditDriver.DinnerStartTime = dinnerendtime
                            CSTrainsAndDrivers.CurEditDriver.DinnerEndTime = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).CulStartTime
                            CSTrainsAndDrivers.CurEditDriver.AllDinnerInfo.Add(CSTrainsAndDrivers.CurEditDriver.DinnerStation & "-" & CSTrainsAndDrivers.CurEditDriver.DinnerStartTime.ToString & "-" & CSTrainsAndDrivers.CurEditDriver.DinnerEndTime.ToString, selectDinnerItem)
                            Exit For
                        End If
                End Select
            Next

            If selecttrain IsNot Nothing Then
                Call AddUnReDoInfo(True)
                CSTrainsAndDrivers.CurEditDriver.AddReMergedTrain(selecttrain)
                Call CSRefreshDiagram()
            Else
                MsgBox("没有找到可勾选任务！", vbQuestion + vbOKOnly, "提示")
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If CSTrainsAndDrivers.CurEditDriver Is Nothing Then
            Me.Close()
            Exit Sub
        End If
        If UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain) <= 1 Then
            Me.BtnBack.Enabled = False
        Else
            Me.BtnBack.Enabled = True
        End If
        If Me.ParentWindow.撤销tolStripUndo.Enabled = False Then
            Me.BtnBack.Enabled = False
        End If
        If OldStartSta <> CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).StartStaName Then
            Call UpdateDestinationCmb()
            OldStartSta = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(1).StartStaName
        End If
        Call UpdateDGV()

    End Sub

    Private Sub BtnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBack.Click
        Me.ParentWindow.撤销tolStripUndo_Click(Nothing, Nothing)
    End Sub

    Private Sub FrmHandAssignDriverR_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Call SortCSLinkTrain()
    End Sub
    
  
End Class