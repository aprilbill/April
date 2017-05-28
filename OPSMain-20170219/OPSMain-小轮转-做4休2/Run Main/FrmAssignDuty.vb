Public Class FrmAssignDuty
    Public UnAssignDuties As List(Of Coordination2.CSDriver)
    Public dutytime As Date
    Public _AreaYunZhuanS As New List(Of AreaYunZhuan)
    Public _TeamList As New List(Of CrewTrainingManager.DriverTeam)
    Public starttime As Date
    Public endtime As Date
    Dim haveDuty As New Dictionary(Of String, String) '有任务的人
    Dim driverinf As New Dictionary(Of String, String) '所有人的工号配姓名
    Dim tmpDriver As New DataTable
    Dim maxtiban As Integer = 0
    Dim selectone As Boolean = False


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndex <> -1 AndAlso selectone Then
            If MsgBox(ListBox1.SelectedItem.ToString & "是否分配给" & TextBox1.Text.Trim & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                For Each rdriverno As String In driverinf.Keys
                    If driverinf(rdriverno) = TextBox1.Text.Trim Then
                        ListBox2.Items.Add(ListBox1.SelectedItem.ToString & "-" & rdriverno & "|" & TextBox1.Text.Trim)
                        ListBox1.Items.Remove(ListBox1.SelectedItem)
                        If haveDuty.Keys.Contains(rdriverno) = False Then
                            haveDuty.Add(rdriverno, TextBox1.Text.Trim)
                        End If
                        TextBox1.Text = ""
                        Exit For
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub FrmAssignDuty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If UnAssignDuties IsNot Nothing Then
            For Each csdri As Coordination2.CSDriver In UnAssignDuties
                Me.ListBox1.Items.Add(csdri.DutySort & "/" & csdri.OutPutCSDriverNo & "||" & (csdri.TotalDayWorkTime / 3600).ToString("0.0") & "||" & csdri.DriveDistance.ToString("0.0"))
            Next
        End If

        Label1.Text = "日期：" & dutytime.ToString("yyyy/MM/dd")

        For Each area As AreaYunZhuan In _AreaYunZhuanS
            For Each team As CrewTrainingManager.DriverTeam In area.AvaDrivers
                For i As Integer = 0 To team.CoDrivers.Count - 1
                    Dim ToJob As Coordination2.DriverDayJob = team.CoDrivers(i).DriverDayJobs.Find(Function(value As Coordination2.DriverDayJob)
                                                                                                       Return value.Date.Date = dutytime.Date
                                                                                                   End Function)
                    If ToJob IsNot Nothing AndAlso ToJob.DutySort.Contains("休息") = False Then
                        If haveDuty.Keys.Contains(team.CoDrivers(i).ID) = False Then
                            haveDuty.Add(team.CoDrivers(i).ID, team.CoDrivers(i).name)
                        End If
                    End If
                Next

            Next
        Next

        Dim str As String = "select * from cs_driverinf where lineid='" & CurLineName & "'"
        tmpDriver = Globle.Method.ReadDataForAccess(str)
        For i As Integer = 0 To tmpDriver.Rows.Count - 1
            If driverinf.Keys.Contains(tmpDriver.Rows(i).Item("rdriverno").ToString.Trim) = False Then
                driverinf.Add(tmpDriver.Rows(i).Item("rdriverno").ToString.Trim, tmpDriver.Rows(i).Item("drivername").ToString.Trim)
            End If
        Next
    End Sub
   
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        selectone = False
        If TextBox1.Text.Trim.Length >= 2 Then
            If haveDuty.Values.Contains(TextBox1.Text.Trim) = False And driverinf.Values.Contains(TextBox1.Text.Trim) Then
                Label4.Text = "人员状态：" & "可用"
                selectone = True
            Else
                If driverinf.Values.Contains(TextBox1.Text.Trim) = False Then
                    Label4.Text = "人员状态：" & "姓名错误！"
                Else
                    Label4.Text = "人员状态：" & "不可用"
                End If
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If MsgBox("请确认已经保存！", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Me.Close()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ListBox2.SelectedIndex <> -1 Then
            If MsgBox("是否要撤销安排" & ListBox2.SelectedItem.ToString & "？", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                Dim driverinfo As String = ListBox2.SelectedItem.ToString.Split("-")(1)
                haveDuty.Remove(driverinfo.Split("|")(0))
                ListBox1.Items.Add(ListBox2.SelectedItem.ToString.Split("-")(0))
                ListBox2.Items.Remove(ListBox2.SelectedItem)
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        For i As Integer = 0 To ListBox2.Items.Count - 1
            Dim driverinfo As String = ListBox2.Items(i).ToString.Split("-")(1)
            Dim dutyinfo As String = ListBox2.Items(i).ToString.Split("-")(0)
            Dim selectduty As Coordination2.CSDriver = Nothing
            If UnAssignDuties IsNot Nothing Then
                For Each csdri As Coordination2.CSDriver In UnAssignDuties
                    Dim tmpstr As String = csdri.DutySort & "/" & csdri.OutPutCSDriverNo & "||" & (csdri.TotalDayWorkTime / 3600).ToString("0.0") & "||" & csdri.DriveDistance.ToString("0.0")
                    If tmpstr = dutyinfo Then
                        selectduty = csdri
                    End If
                Next
            End If
            Dim haveone As Boolean = False
            For Each area As AreaYunZhuan In _AreaYunZhuanS
                For Each team As CrewTrainingManager.DriverTeam In area.AvaDrivers
                    For Each dri As Coordination2.Driver In team.CoDrivers
                        If dri.ID = driverinfo.Split("|")(0) Then
                            AddCSDriver(dri, selectduty, dutytime.Date)      '分配任务
                            dri.GetWorkLoad()
                            haveone = True
                            Exit For
                        End If
                    Next
                    If haveone Then
                        Exit For
                    End If
                Next
                If haveone Then
                    Exit For
                End If
            Next
            If haveone = False Then
                For j As Integer = 0 To tmpDriver.Rows.Count - 1
                    If tmpDriver.Rows(j).Item("rdriverno").ToString.Trim = driverinfo.Split("|")(0) Then
                        Dim teamno As String = tmpDriver.Rows(j).Item("beteam").ToString.Trim
                        Dim rdriverno As String = tmpDriver.Rows(j).Item("rdriverno").ToString.Trim
                        Dim Driteam As CrewTrainingManager.DriverTeam = _TeamList.Find(Function(value As CrewTrainingManager.DriverTeam)
                                                                                           Return value.TeamNo = teamno
                                                                                       End Function)

                        If Driteam Is Nothing Then
                            Driteam = New CrewTrainingManager.DriverTeam(CurLineName, tmpDriver.Rows(j).Item("beclass").ToString.Trim, teamno)
                            _TeamList.Add(Driteam)
                        End If
                        Dim Dri As Coordination2.Driver = Driteam.CoDrivers.Find(Function(value As Coordination2.Driver)
                                                                                     Return value.ID = rdriverno
                                                                                 End Function)
                        If Dri Is Nothing Then
                            Dri = New Coordination2.Driver(tmpDriver.Rows(j).Item("beclass").ToString.Trim, teamno, rdriverno, tmpDriver.Rows(j).Item("drivername").ToString.Trim, CurLineName, tmpDriver.Rows(j).Item("bezone").ToString.Trim, starttime.Date, endtime.Date)
                            Driteam.CoDrivers.Add(Dri)
                        End If
                        AddCSDriver(Dri, selectduty, dutytime)
                        Dri.GetWorkLoad()
                        Dim ifhaveArea As Boolean = False
                        For Each area As AreaYunZhuan In _AreaYunZhuanS
                            If area.AreaName = "替班" Then
                                ifhaveArea = True
                                Exit For
                            End If
                        Next
                        If ifhaveArea = False Then
                            Dim tempArea As New AreaYunZhuan(CurLineName, "替班")
                            tempArea.AvaDrivers.Add(Driteam)
                            _AreaYunZhuanS.Add(tempArea)
                        End If
                    End If
                Next
            End If
        Next
        ListBox2.Items.Clear()
        MsgBox("保存完毕！")
    End Sub
End Class