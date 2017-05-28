Public Class FrmOutputPosition
    Dim openList As New Dictionary(Of Integer, String)
    Dim closeList As New Dictionary(Of Integer, String)
    Dim FrmFlag As Integer = 0
    Public Sub New(ByVal flag As Integer) '1为轮值表，2为位置图

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        FrmFlag = flag
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub FrmOutputPosition_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            If CSTrainsAndDrivers.CSDrivers(i).dutyWork = "备车" Then
                ListBox1.Items.Add(CSTrainsAndDrivers.CSDrivers(i).OutPutCSdriverNo)
                openList.Add(i, CSTrainsAndDrivers.CSDrivers(i).OutPutCSdriverNo)
            End If
        Next
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            RadioButton2.Checked = False
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            RadioButton1.Checked = False
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex <> -1 Then
            TextBox1.Text = ListBox1.Items(ListBox1.SelectedIndex)
            CheckBox1_CheckedChanged(sender, e)
            CheckBox2_CheckedChanged(sender, e)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text.Trim <> "" Then
            If CheckBox2.Checked = False And CheckBox1.Checked = True Then
                If MsgBox("是否设置" & TextBox1.Text.Trim & "开始时间为" & DateTimePicker1.Value.ToShortTimeString, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                    closeList.Add(openList.Keys.ElementAt(ListBox1.SelectedIndex), "1-" & Convert.ToInt32(DateTimePicker1.Value.TimeOfDay.TotalSeconds))
                    openList.Remove(openList.Keys.ElementAt(ListBox1.SelectedIndex))
                    ListBox2.Items.Add(TextBox1.Text.Trim)
                    showList()
                    TextBox1.Text = ""
                End If
            Else
                If MsgBox("是否设置" & TextBox1.Text.Trim & "结束时间为" & DateTimePicker2.Value.ToShortTimeString, MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                    closeList.Add(openList.Keys.ElementAt(ListBox1.SelectedIndex), "2-" & Convert.ToInt32(DateTimePicker2.Value.TimeOfDay.TotalSeconds))
                    openList.Remove(openList.Keys.ElementAt(ListBox1.SelectedIndex))
                    ListBox2.Items.Add(TextBox1.Text.Trim)
                    showList()
                    TextBox1.Text = ""
                End If
            End If
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            DateTimePicker1.Enabled = True
            CheckBox2.Checked = False
            If TextBox1.Text <> "" Then
                Label5.Text = "建议值：小于" + BeTime(CSTrainsAndDrivers.CSDrivers(openList.Keys.ElementAt(ListBox1.SelectedIndex)).CSLinkTrain(1).StartTime)
            End If
        Else
            DateTimePicker1.Enabled = False
            CheckBox2.Checked = True
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            DateTimePicker2.Enabled = True
            CheckBox1.Checked = False
            If TextBox1.Text <> "" Then
                Label5.Text = "建议值：大于" + BeTime(CSTrainsAndDrivers.CSDrivers(openList.Keys.ElementAt(ListBox1.SelectedIndex)).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(openList.Keys.ElementAt(ListBox1.SelectedIndex)).CSLinkTrain)).EndTime)
            End If
        Else
            DateTimePicker2.Enabled = False
            CheckBox1.Checked = True
        End If
    End Sub
    Public Sub showList()
        ListBox1.Items.Clear()
        For Each ss As Integer In openList.Keys
            ListBox1.Items.Add(openList(ss))
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If IsNothing(ListBox2.SelectedItem) = False Then
            openList.Add(closeList.Keys.ElementAt(ListBox2.SelectedIndex), CSTrainsAndDrivers.CSDrivers(closeList.Keys.ElementAt(ListBox2.SelectedIndex)).OutPutCSdriverNo)
            closeList.Remove(closeList.Keys.ElementAt(ListBox2.SelectedIndex))
            ListBox2.Items.RemoveAt(ListBox2.SelectedIndex)
            showList()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If RadioButton1.Checked = True And RadioButton3.Checked = True Then
            If FrmFlag = 2 Then
                OutPutDriverPosition("乘务任务", False, False, closeList)
            Else
                OutPutLunzhi(closeList, False, False)
            End If
        ElseIf RadioButton1.Checked = True And RadioButton3.Checked = False Then
            If FrmFlag = 2 Then
                OutPutDriverPosition("乘务任务", False, True, closeList)
            Else
                OutPutLunzhi(closeList, False, True)
            End If
        ElseIf RadioButton1.Checked = False And RadioButton3.Checked = True Then
            If FrmFlag = 2 Then
                OutPutDriverPosition("乘务任务", True, False, closeList)
            Else
                OutPutLunzhi(closeList, True, False)
            End If
        Else
            If FrmFlag = 2 Then
                OutPutDriverPosition("乘务任务", True, True, closeList)
            Else
                OutPutLunzhi(closeList, True, True)
            End If
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            RadioButton4.Checked = False
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked = True Then
            RadioButton3.Checked = False
        End If
    End Sub
End Class