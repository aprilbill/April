Public Class FrmAddDinnerTime
    Dim curJiaolu As New List(Of String)
    Public Sub New(ByVal station As List(Of String), ByVal jiaolu As List(Of String))

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        curJiaolu = jiaolu
        For i As Integer = 0 To jiaolu.Count - 1
            ComboBox1.Items.Add(jiaolu(i))
        Next
        For i As Integer = 0 To station.Count - 1
            ComboBox2.Items.Add(station(i))
        Next
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnok.Click
        If ComboBox1.Text.Trim = "" Or ComboBox2.Text.Trim = "" Or ComboBox3.Text.Trim = "" Or ComboBox4.Text.Trim = "" Or ComboBox5.Text.Trim = "" Then
            MsgBox("请填写完整参数！")
            Exit Sub
        End If
        Dim flagRight As Integer = 0
        For i = 1 To UBound(CSTrainInf)
            If CSTrainInf(i).sJiaoLuName = ComboBox1.Text.Trim Then
                For j = 0 To UBound(CSTrainInf(i).nPathID)
                    If StationInf(CSTrainInf(i).nPathID(j)).sStationName = ComboBox2.Text.Trim Then
                        If CSTrainInf(i).Arrival(CSTrainInf(i).nPathID(j)) >= dtStartTime.Value.TimeOfDay.TotalSeconds And CSTrainInf(i).Arrival(CSTrainInf(i).nPathID(j)) <= DTEndtime.Value.TimeOfDay.TotalSeconds Then
                            flagRight = 1
                            Exit For
                        End If
                    End If
                Next
            End If
        Next
        If flagRight = 0 Then
            MsgBox("当前时间段该交路无到 " & ComboBox2.Text.Trim & " 的列车，请重新选择")
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub FrmAddDinnerTime_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.cmbDutysort.Text.Trim = "" Then
            Me.cmbDutysort.SelectedIndex = 0
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ComboBox1.Items.Clear()
        If Button2.Text = "筛选交路" Then
            For i = 1 To UBound(CSTrainInf)
                If IsNothing(CSTrainInf(i).Arrival) = False Then
                    For j = 0 To UBound(CSTrainInf(i).Arrival)
                        If CSTrainInf(i).Arrival(j) <= DTEndtime.Value.TimeOfDay.TotalSeconds And CSTrainInf(i).Arrival(j) >= dtStartTime.Value.TimeOfDay.TotalSeconds Then
                            If ComboBox1.Items.Contains(CSTrainInf(i).sJiaoLuName) = False Then
                                ComboBox1.Items.Add(CSTrainInf(i).sJiaoLuName)
                            End If
                        End If
                    Next
                End If
            Next
            Button2.Text = "全部列举"
        Else
            For i As Integer = 0 To curJiaolu.Count - 1
                ComboBox1.Items.Add(curJiaolu(i))
            Next
            Button2.Text = "筛选交路"
        End If

    End Sub
End Class