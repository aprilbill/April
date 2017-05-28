Public Class FrmHandAssignDriver

    Public ParentWindow As frmCSTimeTableMain
    Public OldEndSta As String = ""
    
    Private Sub FrmHandAssignDriver_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DGVProperty.Rows.Clear()
        Me.DGVProperty.Rows.Add("任务编号")
        Me.DGVProperty.Rows.Add("班种")
        Me.DGVProperty.Rows.Add("驾驶公里")
        Me.DGVProperty.Rows.Add("工作时间")
        Me.DGVProperty.Rows.Add("驾驶时间")
        Call UpdateDestinationCmb()
        Dim index As Integer = Me.CmbDutySort.Items.IndexOf(CSTrainsAndDrivers.CurEditDriver.DutySort)
        If index <> -1 Then
            Me.CmbDutySort.SelectedIndex = index
        Else
            Me.CmbDutySort.SelectedIndex = 0
        End If
        Me.CmbStyle.SelectedIndex = 0
        Me.Timer1.Enabled = True
        Call SortCSLinkTrain()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CSTrainsAndDrivers.CurEditDriver = Nothing
        Call Me.ParentWindow.ListAllViewInfo()
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
                If train.StartStaName = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).EndStaName AndAlso _
                    train.CulStartTime >= CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).CulEndTime Then
                    Dim endsta As String = train.EndStaName
                    Dim index As Integer = desList.FindIndex(Function(value As String)
                                                                 Return value = endsta
                                                             End Function)
                    If index < 0 Then
                        desList.Add(endsta)
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
            Dim restDirection As Integer = 2
            If Style = "休息后接车" Then
                For i As Integer = 0 To ChangeStationList.Count - 1
                    If ChangeStationList(i).Name = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).EndStaName Then
                        restDirection = ChangeStationList(i).UpTrainDirection
                    End If
                Next
            End If
            Dim dinnerendtime As Integer = -1
            Dim selectDinnerItem As typeDinnerStation = Nothing
            Dim dinneritemFound As Boolean = False
            If Style = "吃饭后接车" Then
                For i As Integer = 1 To UBound(sysDinnerStation)
                    If CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).EndStaName = sysDinnerStation(i).DinnerStationName And (sysDinnerStation(i).Direction = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).UpOrDown Or sysDinnerStation(i).Direction = 2) And CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).RoutingName = sysDinnerStation(i).Routing Then
                        If CSTrainsAndDrivers.CurEditDriver.DutySort = sysDinnerStation(i).dutySort AndAlso AddLitterTime(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).EndTime) >= sysDinnerStation(i).dinnerStartTime And AddLitterTime(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).EndTime) <= sysDinnerStation(i).dinnerEndTime Then
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
                            dinnerendtime = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).CulEndTime + sysDinnerStation(i).DinnerTime
                            selectDinnerItem = sysDinnerStation(i)
                            dinneritemFound = True
                            Exit For
                        End If
                    End If
                Next
                If dinneritemFound = False Then
                    MsgBox("没有找到符合吃饭参数的任务！", vbQuestion + vbOKOnly, "提示")
                    Exit Sub
                End If
            End If
          
            For Each train As MergedCSLinkTrain In CSTrainsAndDrivers.MergedCSLinkTrains
                Select Case Style
                    Case "直接接车"
                        If train IsNot Nothing AndAlso train.IsLinked = False AndAlso train.StartStaName = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).EndStaName AndAlso train.EndStaName = desName AndAlso _
                                            AddLitterTime(train.StartTime) >= AddLitterTime(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).EndTime) Then
                            selecttrain = train
                            Exit For
                        End If
                    Case "休息后接车"
                        If train IsNot Nothing AndAlso train.IsLinked = False AndAlso train.StartStaName = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).EndStaName AndAlso train.EndStaName = desName AndAlso _
                             AddLitterTime(train.StartTime) > AddLitterTime(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).EndTime) Then
                            If CSTrainsAndDrivers.CurEditDriver.CheckIfInterval(restNo, restDirection, train.CSLinkTrains(1)) Then
                                selecttrain = train
                                Exit For
                            End If
                        End If
                    Case "吃饭后接车"
                        If train IsNot Nothing AndAlso train.IsLinked = False AndAlso train.StartStaName = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).EndStaName AndAlso train.EndStaName = desName AndAlso _
                                           train.nCheDiID <> CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).nCheDiID And AddLitterTime(train.CSLinkTrains(1).StartTime) > dinnerendtime Then
                            selecttrain = train
                            CSTrainsAndDrivers.CurEditDriver.FlagDinner = True
                            CSTrainsAndDrivers.CurEditDriver.DinnerStation = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).EndStaName
                            CSTrainsAndDrivers.CurEditDriver.DinnerStartTime = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).CulEndTime
                            CSTrainsAndDrivers.CurEditDriver.DinnerEndTime = dinnerendtime
                            CSTrainsAndDrivers.CurEditDriver.AllDinnerInfo.Add(CSTrainsAndDrivers.CurEditDriver.DinnerStation & "-" & CSTrainsAndDrivers.CurEditDriver.DinnerStartTime.ToString & "-" & CSTrainsAndDrivers.CurEditDriver.DinnerEndTime.ToString, selectDinnerItem)
                            Exit For
                        End If
                End Select
            Next
            If selecttrain IsNot Nothing Then
                Call AddUnReDoInfo(True)
                CSTrainsAndDrivers.CurEditDriver.AddMergedTrain(selecttrain)
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
        If OldEndSta <> CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).EndStaName Then
            Call UpdateDestinationCmb()
            OldEndSta = CSTrainsAndDrivers.CurEditDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CurEditDriver.CSLinkTrain)).EndStaName
        End If
        Call UpdateDGV()

    End Sub

    Private Sub BtnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBack.Click
        Me.ParentWindow.撤销tolStripUndo_Click(Nothing, Nothing)
    End Sub

   
End Class