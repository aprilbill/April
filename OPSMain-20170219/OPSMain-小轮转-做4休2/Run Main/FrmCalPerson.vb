Public Class FrmCalPerson
    Dim TMS_TRAINDIAGRAMINFO As New DataTable
    Private Sub FrmCalPerson_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim sqlstr As String = "select * from TMS_TRAINDIAGRAMINFO where LINENAME='" & CurLineName & "'"
        TMS_TRAINDIAGRAMINFO = Globle.Method.ReadDataForAccess(sqlstr)
        If IsNothing(TMS_TRAINDIAGRAMINFO) = False AndAlso TMS_TRAINDIAGRAMINFO.Rows.Count > 0 Then
            For i As Integer = 0 To TMS_TRAINDIAGRAMINFO.Rows.Count - 1
                ListBox1.Items.Add(TMS_TRAINDIAGRAMINFO.Rows(i).Item("traindiagramid").ToString.Trim & " | " & TMS_TRAINDIAGRAMINFO.Rows(i).Item("timetablename").ToString.Trim)
            Next
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If IsNothing(ListBox1.SelectedItem) = False Then
            Dim DiagramCurID As String = ListBox1.SelectedItem.ToString.Split("|")(0).Trim
            Call CS_CSMaker.CSInitSystemVariant()
            Call CS_CSMaker.CSreadNetStaAndSecData(DiagramCurID)
            Call CS_CSMaker.CSInputChediAndTrainJianGeData("", DiagramCurID)
            Call CS_CSMaker.CSReadBaseTrainInf(DiagramCurID)
            Call CS_CSMaker.CSReadTrainAndTimeTableInf(DiagramCurID, Me.ProgressBar1)
            Call CS_CSMaker.GetEachDutyChediNum()
            DataGridView1.Rows.Add(ListBox1.SelectedItem.ToString.Split("|")(1).Trim, 0, CS_CSMaker.MChediNum, 0, CS_CSMaker.NChediNum, 0, CS_CSMaker.AChediNum, 0)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If IsNothing(DataGridView1.SelectedRows) = False AndAlso DataGridView1.SelectedRows.Count > 0 Then
            DataGridView1.Rows.Remove(DataGridView1.SelectedRows(0))
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If DataGridView1.Rows.Count > 0 And ComboBox1.Text.Trim <> "" And TextBox1.Text.Trim <> "" Then
            If ComboBox1.Text.Trim = "四班两转" Then
                Dim tmpmax As Integer = 0
                For i As Integer = 0 To DataGridView1.Rows.Count - 1
                    Dim zao As Integer = CInt(DataGridView1.Rows(i).Cells("早高峰").Value.ToString.Trim) + CInt(DataGridView1.Rows(i).Cells("早高峰轮换").Value.ToString.Trim)
                    Dim zhong As Integer = CInt(DataGridView1.Rows(i).Cells("平峰").Value.ToString.Trim) + CInt(DataGridView1.Rows(i).Cells("平峰轮换").Value.ToString.Trim)
                    Dim wan As Integer = CInt(DataGridView1.Rows(i).Cells("晚高峰").Value.ToString.Trim) + CInt(DataGridView1.Rows(i).Cells("晚高峰轮换").Value.ToString.Trim)
                    Dim max As Integer = 0
                    If zao > max Then
                        max = zao
                    End If
                    If zhong > max Then
                        max = zhong
                    End If
                    If wan > max Then
                        max = wan
                    End If
                    If max > tmpmax Then
                        tmpmax = max
                    End If
                Next
                TextBox2.Text = tmpmax
            Else
                Dim tanshu As Double = 0
                Dim tmpsum As Double = 0
                For i As Integer = 0 To DataGridView1.Rows.Count - 1
                    tanshu += CDbl(DataGridView1.Rows(i).Cells("天数").Value.ToString.Trim)
                    tmpsum += CDbl(DataGridView1.Rows(i).Cells("天数").Value.ToString.Trim) * (CDbl(DataGridView1.Rows(i).Cells("早高峰").Value.ToString.Trim) + CDbl(DataGridView1.Rows(i).Cells("早高峰轮换").Value.ToString.Trim) + CDbl(DataGridView1.Rows(i).Cells("平峰").Value.ToString.Trim) + CDbl(DataGridView1.Rows(i).Cells("平峰轮换").Value.ToString.Trim) + CDbl(DataGridView1.Rows(i).Cells("晚高峰").Value.ToString.Trim) + CDbl(DataGridView1.Rows(i).Cells("晚高峰轮换").Value.ToString.Trim))
                Next
                TextBox2.Text = -CInt(-(tmpsum / (4 * tanshu - CInt(TextBox1.Text.Trim))))
            End If
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text = "四班两转" Then
            TextBox1.Enabled = False
            TextBox1.Text = "-"
        Else
            TextBox1.Enabled = True
            TextBox1.Text = "0"
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
End Class