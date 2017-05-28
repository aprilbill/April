Public Class Frmdicon
    Dim tabDriver As New DataTable
    Dim selectDri As CSDriver
    Public Sub New(ByVal driverid As String)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
            For Each Driver As CSDriver In CSTrainsAndDrivers.CSDrivers
                If Driver IsNot Nothing AndAlso Driver.CSDriverID = driverid Then
                    selectDri = Driver
                    TextBox1.Text = Driver.CSdriverNo
                    Label3.Text = "总驾驶距离：" & Driver.DriveDistance & "公里，总驾驶时间：" & BeTime(Driver.DriveTime)
                    tabDriver = New DataTable
                    For i As Integer = 1 To UBound(Driver.CSLinkTrain)
                        tabDriver.Columns.Add("子任务" & i.ToString, GetType(String))
                    Next
                    tabDriver.Rows.Add()
                    For i As Integer = 1 To UBound(Driver.CSLinkTrain)
                        tabDriver.Rows(0).Item("子任务" & i.ToString) = BeTime(Driver.CSLinkTrain(i).StartTime) & Driver.CSLinkTrain(i).StartStaName & "->" & BeTime(Driver.CSLinkTrain(i).EndTime) & Driver.CSLinkTrain(i).EndStaName
                    Next
                    DataGridView1.DataSource = tabDriver
                End If
            Next
        End If
    End Sub
    Public selMission As Integer = -1
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            selMission = e.ColumnIndex + 2
            Dim predis As Double = 0
            For i As Integer = 1 To UBound(selectDri.CSLinkTrain)
                If i > e.ColumnIndex + 1 Then
                    Exit For
                End If
                predis += selectDri.CSLinkTrain(i).distance
            Next
            If e.ColumnIndex + 1 = UBound(selectDri.CSLinkTrain) Then
                Label2.Text = "无法从该位置之后断开，当前为最后一个子任务"
            Else
                Label2.Text = "从选中位置之后断开后：前部分" & predis.ToString() & "公里，后部分" & (selectDri.DriveDistance - predis).ToString & "公里"
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If selMission > UBound(selectDri.CSLinkTrain) Then
            MsgBox("无法从选中位置断开！")
            Exit Sub
        End If
        If selMission = -1 Then
            MsgBox("没有选中断开位置！")
            Exit Sub
        End If
        If MsgBox("确认从该位置断开？", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Call AddUnReDoInfo(True)
            Call DeleteDriverLink(selectDri.CSLinkTrain(selMission).CSTrainID, selectDri.CSDriverID)
            selectDri.RefreshState()
            Call CSRefreshDiagram()
            Me.Close()
        End If
    End Sub
End Class