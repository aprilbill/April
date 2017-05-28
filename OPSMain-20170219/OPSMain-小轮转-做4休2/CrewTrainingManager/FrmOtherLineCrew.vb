Public Class FrmOtherLineCrew
    Public ifRefresh As Boolean = False
    Public line2beclass As New Dictionary(Of String, List(Of String))

    Private Sub FrmOtherLineCrew_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim str As String = "select distinct lineid,beclass from cs_driverinf where lineid in (select linename from pd_lineinfo t where t.linemanagerid = (select linemanagerid from pd_lineinfo s where s.linename='" & CurLineName & "')) order by lineid"
        Dim tmpDt As New DataTable
        tmpDt = Globle.Method.ReadDataForOracle(str)
        For i As Integer = 0 To tmpDt.Rows.Count - 1
            If tmpDt.Rows(i)("lineid").ToString <> CurLineName Then
                If line2beclass.Keys.Contains(tmpDt.Rows(i)("lineid").ToString) = False Then
                    line2beclass.Add(tmpDt.Rows(i)("lineid").ToString, New List(Of String))
                    ComboBox1.Items.Add(tmpDt.Rows(i)("lineid").ToString)
                End If
                line2beclass(tmpDt.Rows(i)("lineid").ToString).Add(tmpDt.Rows(i)("beclass").ToString)
            End If
        Next
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.Text.Trim <> "" Then
            ComboBox2.Items.Clear()
            For i As Integer = 0 To line2beclass(ComboBox1.Text.Trim).Count - 1
                ComboBox2.Items.Add(line2beclass(ComboBox1.Text.Trim)(i).ToString)
            Next
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox1.Text.Trim <> "" AndAlso ComboBox2.Text.Trim <> "" Then
            Dim str As String = "select * from cs_driverinf t where t.lineid = '" & ComboBox1.Text.Trim & "' and beclass='" & ComboBox2.Text.Trim & "'"
            Dim selectDriDt As New DataTable
            selectDriDt = Globle.Method.ReadDataForOracle(str)
            DataGridView1.Rows.Clear()
            For i As Integer = 0 To selectDriDt.Rows.Count - 1
                DataGridView1.Rows.Add(ComboBox1.Text.Trim, ComboBox2.Text.Trim, selectDriDt.Rows(i)("post").ToString.Trim, selectDriDt.Rows(i)("unicode").ToString.Trim, selectDriDt.Rows(i)("rdriverno").ToString.Trim, selectDriDt.Rows(i)("drivername").ToString.Trim, selectDriDt.Rows(i)("KM").ToString.Trim, selectDriDt.Rows(i)("enrolltime").ToString.Trim)
            Next
            Label4.Text = "已选择数：0"
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        Label4.Text = "已选择数：" & DataGridView1.SelectedRows.Count
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            If MsgBox("确认将选中人员调入" & CurLineName & "?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                ifRefresh = True
                ProgressBar1.Visible = True
                Dim str As String = ""
                ProgressBar1.Value = 0
                ProgressBar1.Maximum = DataGridView1.SelectedRows.Count
                For i As Integer = 0 To DataGridView1.SelectedRows.Count - 1
                    ProgressBar1.Value = i + 1
                    str = "INSERT INTO CS_DRIVERINF " _
                                  & "(lineid,beclass,rdriverno,drivername,bezone,enrolltime,post,idleornot,reasonforunavailable,KM,unicode)" _
                                  & "VALUES(" _
                                  & "'" & CurLineName _
                                  & "','" & DataGridView1.SelectedRows(i).Cells("班组").Value.ToString.Trim _
                                  & "','" & DataGridView1.SelectedRows(i).Cells("工号").Value.ToString.Trim _
                                  & "','" & DataGridView1.SelectedRows(i).Cells("姓名").Value.ToString.Trim _
                                  & "','" & "主区域" _
                                  & "','" & DataGridView1.SelectedRows(i).Cells("开始统计时间").Value.ToString.Trim _
                                   & "','" & DataGridView1.SelectedRows(i).Cells("岗位").Value.ToString.Trim _
                                  & "','" & "不可用" _
                                  & "','" & DataGridView1.SelectedRows(i).Cells("线路").Value.ToString.Trim _
                                  & "','" & DataGridView1.SelectedRows(i).Cells("前安全公里").Value.ToString.Trim _
                                  & "','" & DataGridView1.SelectedRows(i).Cells("工作证号").Value.ToString.Trim _
                                  & "')"
                    Try
                        UpdateData(str)
                    Catch ex As Exception
                        MsgBox("网络连接失败！" & vbCrLf & ex.ToString)
                        Exit Sub
                    End Try
                Next
                MsgBox("本地调用成功！")
                ProgressBar1.Visible = False
            End If
        End If

    End Sub
End Class