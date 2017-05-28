Public Class FrmCorTTManage
    Dim corPlan As New DataTable
    Private Sub FrmCorTTManage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If strQCurCSPlanID = "" Then
            MsgBox("请先保存正在编制的乘务计划")
            Exit Sub
        End If
        Dim str As String = "select distinct adrivertimetableid,mdrivertimetableid from cs_amdrivercorrespond where (adrivertimetableid='" & strQCurCSPlanID & "' or mdrivertimetableid='" & strQCurCSPlanID & "') and lineid='" & CurLineName & "'"
        corPlan = Globle.Method.ReadDataForAccess(str)
        If IsNothing(corPlan) = False AndAlso corPlan.Rows.Count > 0 Then
            Button1_Click(sender, e)
        Else
            MsgBox("暂无衔接计划！")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DataGridView1.Rows.Clear()
        If IsNothing(corPlan) = False AndAlso corPlan.Rows.Count > 0 Then
            For i As Integer = 0 To corPlan.Rows.Count - 1
                If ComboBox1.Text.Contains("早班") Then
                    If corPlan.Rows(i).Item("mdrivertimetableid").ToString.Trim = strQCurCSPlanID Then
                        DataGridView1.Rows.Add(corPlan.Rows(i).Item("adrivertimetableid").ToString.Trim, corPlan.Rows(i).Item("mdrivertimetableid").ToString.Trim)
                    End If
                End If
                If ComboBox1.Text.Contains("夜班") Then
                    If corPlan.Rows(i).Item("adrivertimetableid").ToString.Trim = strQCurCSPlanID Then
                        DataGridView1.Rows.Add(corPlan.Rows(i).Item("adrivertimetableid").ToString.Trim, corPlan.Rows(i).Item("mdrivertimetableid").ToString.Trim)
                    End If
                End If
                If ComboBox1.Text.Contains("全部") Then
                    DataGridView1.Rows.Add(corPlan.Rows(i).Item("adrivertimetableid").ToString.Trim, corPlan.Rows(i).Item("mdrivertimetableid").ToString.Trim)
                End If
            Next
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If IsNothing(DataGridView1.SelectedRows) = False AndAlso DataGridView1.SelectedRows.Count > 0 Then
            If MsgBox("确认删除此衔接计划？", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                Dim str As String = "delete from cs_amdrivercorrespond where lineid='" & CurLineName & "' and adrivertimetableid='" & DataGridView1.SelectedRows(0).Cells("夜班计划").Value.ToString.Trim & "' and mdrivertimetableid='" & DataGridView1.SelectedRows(0).Cells("早班计划").Value.ToString.Trim & "'"
                Globle.Method.UpdateDataForAccess(str)
                str = "select distinct adrivertimetableid,mdrivertimetableid from cs_amdrivercorrespond where (adrivertimetableid='" & strQCurCSPlanID & "' or mdrivertimetableid='" & strQCurCSPlanID & "') and lineid='" & CurLineName & "'"
                corPlan = Globle.Method.ReadDataForAccess(str)
                System.Threading.Thread.Sleep(50)

                If DataGridView1.SelectedRows(0).Cells("夜班计划").Value.ToString.Trim = CSTrainsAndDrivers.CorCSTimetableID Or DataGridView1.SelectedRows(0).Cells("早班计划").Value.ToString.Trim = CSTrainsAndDrivers.CorCSTimetableID Then
                    ReDim CSTrainsAndDrivers.CorCSDrivers(0)
                    CSTrainsAndDrivers.IfCorSchedule = False
                    CSTrainsAndDrivers.CorCSTimetableID = ""
                    CSTrainsAndDrivers.CorCSPlanName = ""
                    CSTrainsAndDrivers.CorPreParedDutyDrivers.Clear()
                    CSTrainsAndDrivers.CorPreParedTrainDrivers.Clear()
                    CSTrainsAndDrivers.CorMorningDrivers = New List(Of CSDriver)
                    CSTrainsAndDrivers.CorDayDrivers = New List(Of CSDriver)
                    CSTrainsAndDrivers.CorCDayDrivers = New List(Of CSDriver)
                    CSTrainsAndDrivers.CorNightDrivers = New List(Of CSDriver)
                    CSTrainsAndDrivers.CorOtherDrivers = New List(Of CSDriver)
                    CSTrainsAndDrivers.PostiveCorCSPlans = New List(Of CorCSPlan)
                    CSTrainsAndDrivers.NegtiveCorCSPlans = New List(Of CorCSPlan)
                End If
                Button1_Click(sender, e)
                MsgBox("删除成功！")
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If IsNothing(DataGridView1.SelectedRows) = False AndAlso DataGridView1.SelectedRows.Count > 0 Then
            If DataGridView1.SelectedRows(0).Cells("夜班计划").Value.ToString.Trim = strQCurCSPlanID And DataGridView1.SelectedRows(0).Cells("早班计划").Value.ToString.Trim <> strQCurCSPlanID Then
                CSTrainsAndDrivers.CorCSTimetableID = DataGridView1.SelectedRows(0).Cells("早班计划").Value.ToString.Trim
                CSTrainsAndDrivers.CorCSPlanName = GetCSPlanNameFromCSPlanID(CSTrainsAndDrivers.CorCSTimetableID)
                Call ReadCorDriversAndTrains(CSTrainsAndDrivers.CorCSPlanName)
                Call ReadCorPrepareDrivers(CSTrainsAndDrivers.CorCSPlanName)
                Call ReadCorCSPlan()
                Dim nf As New FrmCorNitMor
                nf.tCorStyle = FrmCorNitMor.CorStyle.自身夜班被动衔接
                nf.ShowDialog()
            End If
            If DataGridView1.SelectedRows(0).Cells("早班计划").Value.ToString.Trim = strQCurCSPlanID And DataGridView1.SelectedRows(0).Cells("夜班计划").Value.ToString.Trim <> strQCurCSPlanID Then
                CSTrainsAndDrivers.CorCSTimetableID = DataGridView1.SelectedRows(0).Cells("夜班计划").Value.ToString.Trim
                CSTrainsAndDrivers.CorCSPlanName = GetCSPlanNameFromCSPlanID(CSTrainsAndDrivers.CorCSTimetableID)
                Call ReadCorDriversAndTrains(CSTrainsAndDrivers.CorCSPlanName)
                Call ReadCorPrepareDrivers(CSTrainsAndDrivers.CorCSPlanName)
                Call ReadCorCSPlan()
                Dim nf As New FrmCorNitMor
                nf.tCorStyle = FrmCorNitMor.CorStyle.自身早班主动衔接
                nf.ShowDialog()
            End If
            If DataGridView1.SelectedRows(0).Cells("早班计划").Value.ToString.Trim = strQCurCSPlanID And DataGridView1.SelectedRows(0).Cells("夜班计划").Value.ToString.Trim = strQCurCSPlanID Then
                CSTrainsAndDrivers.CorCSTimetableID = DataGridView1.SelectedRows(0).Cells("夜班计划").Value.ToString.Trim
                CSTrainsAndDrivers.CorCSPlanName = GetCSPlanNameFromCSPlanID(CSTrainsAndDrivers.CorCSTimetableID)
                Call ReadCorDriversAndTrains(CSTrainsAndDrivers.CorCSPlanName)
                Call ReadCorPrepareDrivers(CSTrainsAndDrivers.CorCSPlanName)
                Call ReadCorCSPlan()
                Dim nf As New FrmCorNitMor
                nf.tCorStyle = FrmCorNitMor.CorStyle.自身夜早班衔接
                nf.ShowDialog()
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
End Class