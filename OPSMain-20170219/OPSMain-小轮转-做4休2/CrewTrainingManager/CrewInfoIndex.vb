
Imports Pabo.Calendar
Imports System.IO
Imports Microsoft.Office.Interop.Excel

Public Class CrewInfoIndex

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim frm As New newTestAdd
        If frm.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            RefreshData()
            MsgBox("添加成功！", MsgBoxStyle.OkOnly, "提醒")
        End If
    End Sub

    Private Sub CrewInfoIndex_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandler Me.TotalDataGridView.KeyDown, AddressOf DGVKeyPress
        Call RefreshData()
    End Sub

    Private Sub DGVKeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Delete Then
            Call TSBDelete_Click(Nothing, Nothing)
        End If
    End Sub

    Public Sub RefreshData()

        Me.TotalDataGridView.Rows.Clear()
        Me.TVDrivers.Nodes.Clear()

        Dim str As String = "select * from cs_driverinf where lineid='" & CurLineName & "' order by beclass"
        Dim tab As New System.Data.DataTable
        tab = ReadData(str)
        Dim npro As New FrmProgress(tab.Rows.Count, "正在更新人员信息...")
        If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then

            For i = 0 To tab.Rows.Count - 1
                Me.TotalDataGridView.Rows.Add()
                Me.TotalDataGridView.Rows(i).Cells("线路").Value = tab.Rows(i).Item("lineid").ToString
                Me.TotalDataGridView.Rows(i).Cells("班组").Value = tab.Rows(i).Item("beclass").ToString
                Me.TotalDataGridView.Rows(i).Cells("组号").Value = tab.Rows(i).Item("beteam").ToString
                Me.TotalDataGridView.Rows(i).Cells("工号").Value = tab.Rows(i).Item("rdriverno").ToString
                Me.TotalDataGridView.Rows(i).Cells("姓名").Value = tab.Rows(i).Item("drivername").ToString
                Me.TotalDataGridView.Rows(i).Cells("岗位").Value = tab.Rows(i).Item("post").ToString
                Me.TotalDataGridView.Rows(i).Cells("区域").Value = tab.Rows(i).Item("bezone").ToString
                Me.TotalDataGridView.Rows(i).Cells("工作证编号").Value = tab.Rows(i).Item("unicode").ToString
                Me.TotalDataGridView.Rows(i).Cells("是否可用").Value = tab.Rows(i).Item("idleornot").ToString
                Me.TotalDataGridView.Rows(i).Cells("原因").Value = tab.Rows(i).Item("reasonforunavailable").ToString
                Me.TotalDataGridView.Rows(i).Cells("技能等级").Value = tab.Rows(i).Item("techgrade").ToString
                Me.TotalDataGridView.Rows(i).Cells("公里数").Value = tab.Rows(i).Item("KM").ToString
                Me.TotalDataGridView.Rows(i).Cells("开始统计").Value = tab.Rows(i).Item("enrolltime").ToString
                Me.TotalDataGridView.Rows(i).Cells("星级").Value = tab.Rows(i).Item("starlevel").ToString
                Me.TotalDataGridView.Rows(i).Cells("联系电话").Value = tab.Rows(i).Item("phone").ToString
                Me.TotalDataGridView.Rows(i).Cells("学徒").Value = tab.Rows(i).Item("apprentice").ToString
                Me.TotalDataGridView.Rows(i).Cells("师徒备注").Value = tab.Rows(i).Item("marelation").ToString

                If i = 0 Then
                    TVDrivers.Nodes.Add(tab.Rows(i).Item("lineid").ToString, tab.Rows(i).Item("lineid").ToString)
                    TVDrivers.Nodes(tab.Rows(i).Item("lineid").ToString).Nodes.Add(tab.Rows(i).Item("beclass").ToString, tab.Rows(i).Item("beclass").ToString)
                    TVDrivers.Nodes(tab.Rows(i).Item("lineid").ToString).Nodes(tab.Rows(i).Item("beclass").ToString).Nodes.Add(tab.Rows(i).Item("drivername").ToString, tab.Rows(i).Item("drivername").ToString)
                Else
                    If tab.Rows(i).Item("lineid").ToString = tab.Rows(i - 1).Item("lineid").ToString Then
                        If tab.Rows(i).Item("beclass").ToString = tab.Rows(i - 1).Item("beclass").ToString Then
                            TVDrivers.Nodes(tab.Rows(i).Item("lineid").ToString).Nodes(tab.Rows(i).Item("beclass").ToString).Nodes.Add(tab.Rows(i).Item("drivername").ToString, tab.Rows(i).Item("drivername").ToString)
                        Else
                            TVDrivers.Nodes(tab.Rows(i).Item("lineid").ToString).Nodes.Add(tab.Rows(i).Item("beclass").ToString, tab.Rows(i).Item("beclass").ToString)
                            TVDrivers.Nodes(tab.Rows(i).Item("lineid").ToString).Nodes(tab.Rows(i).Item("beclass").ToString).Nodes.Add(tab.Rows(i).Item("drivername").ToString, tab.Rows(i).Item("drivername").ToString)
                        End If
                    Else
                        TVDrivers.Nodes.Add(tab.Rows(i).Item("lineid").ToString, tab.Rows(i).Item("lineid").ToString)
                        TVDrivers.Nodes(tab.Rows(i).Item("lineid").ToString).Nodes.Add(tab.Rows(i).Item("beclass").ToString, tab.Rows(i).Item("beclass").ToString)
                        TVDrivers.Nodes(tab.Rows(i).Item("lineid").ToString).Nodes(tab.Rows(i).Item("beclass").ToString).Nodes.Add(tab.Rows(i).Item("drivername").ToString, tab.Rows(i).Item("drivername").ToString)
                    End If
                End If
                npro.Performstep()
            Next
            Me.TVDrivers.Nodes(0).Expand()
        End If
        npro.Close()
        tab.Dispose()
    End Sub



    Private Sub TSBModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBModify.Click
        Dim frm As New newTestUpdate
        If Me.TotalDataGridView.SelectedRows.Count = 1 Then
            Dim selectrow As DataGridViewRow = Me.TotalDataGridView.SelectedRows(0)
            frm.TxtBeline.Text = selectrow.Cells("线路").Value.ToString
            frm.CmbClass.Text = selectrow.Cells("班组").Value.ToString
            frm.TxtTeamno.Text = selectrow.Cells("组号").Value.ToString
            frm.DriverIDTextBox.Text = selectrow.Cells("工号").Value.ToString
            frm.DriverNameTextBox.Text = selectrow.Cells("姓名").Value.ToString
            frm.CmbPost.Text = selectrow.Cells("岗位").Value.ToString
            frm.CmbAvailableOrNot.Text = selectrow.Cells("是否可用").Value.ToString
            frm.CmbReasonforAvail.Text = selectrow.Cells("原因").Value.ToString
            frm.CmbTechGrade.Text = selectrow.Cells("技能等级").Value.ToString
            frm.TxtPhone.Text = selectrow.Cells("联系电话").Value.ToString
            frm.TxtKM.Text = selectrow.Cells("公里数").Value.ToString
            frm.CmbStarLevel.Text = selectrow.Cells("星级").Value.ToString
            frm.CmbApprentice.Text = selectrow.Cells("学徒").Value.ToString
            frm.TxtMArelation.Text = selectrow.Cells("师徒备注").Value.ToString
            frm.CBEZONE.Text = selectrow.Cells("区域").Value.ToString
            If selectrow.Cells("开始统计").Value.ToString.Trim <> "" Then
                frm.DateTimePicker1.Value = CDate(selectrow.Cells("开始统计").Value.ToString)
            Else
                frm.DateTimePicker1.Value = Now
            End If
            If frm.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                RefreshData()
            End If
        End If
    End Sub

    Private Sub TSBDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBDelete.Click
        If Me.TotalDataGridView.SelectedRows.Count > 0 Then
            If MsgBox("确实删除这些选中的记录?", MsgBoxStyle.OkCancel, "提醒") = MsgBoxResult.Ok Then
                For i As Integer = Me.TotalDataGridView.SelectedRows.Count - 1 To 0 Step -1
                    Dim lineid As String = Me.TotalDataGridView.SelectedRows(i).Cells("线路").Value.ToString
                    Dim driverno As String = Me.TotalDataGridView.SelectedRows(i).Cells("工号").Value.ToString
                    Dim beclass As String = Me.TotalDataGridView.SelectedRows(i).Cells("班组").Value.ToString
                    Dim driverName As String = Me.TotalDataGridView.SelectedRows(i).Cells("姓名").Value.ToString
                    Dim str As String = "delete from cs_driverinf where lineid='" & lineid & "' and rdriverno='" & driverno & "'"
                    UpdateData(str)
                    System.Threading.Thread.Sleep(50)
                Next
                RefreshData()
            End If
        End If
    End Sub

    Private Sub TSBExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub TSBInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBInput.Click
        Dim frm As New DriverInfoLoad
        If frm.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Call RefreshData()
        End If
    End Sub

    Private Sub TSBRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBRefresh.Click
        Call RefreshData()
    End Sub

    Private Sub TSBOutPut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        If TotalDataGridView.Rows.Count > 0 Then
            OutPutToEXCELFileFormDataGrid("人员信息", TotalDataGridView, Me)
        End If
    End Sub

    Private Sub TotalDataGridView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles TotalDataGridView.CellClick
        If Me.TotalDataGridView.SelectedRows.Count = 1 Then
            Dim selectrow As DataGridViewRow = Me.TotalDataGridView.SelectedRows(0)
            Dim lineid As String = selectrow.Cells("线路").Value.ToString
            Dim beclass As String = selectrow.Cells("班组").Value.ToString
            Dim driverName As String = selectrow.Cells("姓名").Value.ToString
            Me.TVDrivers.Nodes(0).Expand()
            Me.TVDrivers.SelectedNode = Me.TVDrivers.Nodes(lineid).Nodes(beclass).Nodes(driverName)
        End If
    End Sub

    Private Sub 新增ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新增ToolStripMenuItem.Click
        Call TSBModify_Click(Nothing, Nothing)
    End Sub

    Private Sub 请假ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 请假ToolStripMenuItem.Click
        Call TSBDelete_Click(Nothing, Nothing)
    End Sub

    Private Sub 新增NToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 新增NToolStripMenuItem.Click
        Call ToolStripButton1_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton8.Click
        Dim frm As New FrmYearVocation
        frm.ShowDialog()
    End Sub

    Private Sub TxtQuery_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtQuery.TextChanged
        Dim driverno As String = Me.TxtQuery.Text.Trim
        Dim sqlStr As String = ""
        If driverno <> "" Then
            sqlStr = "Select * from cs_driverinf where lineid='" & CurLineName & "' and rdriverno='" & driverno & "'"
        Else
            sqlStr = "Select * from cs_driverinf order by lineid,beclass,beteam,rdriverno"
        End If
        Dim tab As System.Data.DataTable = ReadData(sqlStr)
        If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
            Me.TotalDataGridView.Rows.Clear()
            For i As Integer = 0 To tab.Rows.Count - 1
                Me.TotalDataGridView.Rows.Add()
                Me.TotalDataGridView.Rows(i).Cells("线路").Value = tab.Rows(i).Item("lineid").ToString
                Me.TotalDataGridView.Rows(i).Cells("班组").Value = tab.Rows(i).Item("beclass").ToString
                Me.TotalDataGridView.Rows(i).Cells("组号").Value = tab.Rows(i).Item("beteam").ToString
                Me.TotalDataGridView.Rows(i).Cells("工号").Value = tab.Rows(i).Item("rdriverno").ToString
                Me.TotalDataGridView.Rows(i).Cells("姓名").Value = tab.Rows(i).Item("drivername").ToString
                Me.TotalDataGridView.Rows(i).Cells("岗位").Value = tab.Rows(i).Item("post").ToString
                Me.TotalDataGridView.Rows(i).Cells("区域").Value = tab.Rows(i).Item("bezone").ToString
                Me.TotalDataGridView.Rows(i).Cells("工作证编号").Value = tab.Rows(i).Item("unicode").ToString
                Me.TotalDataGridView.Rows(i).Cells("是否可用").Value = tab.Rows(i).Item("idleornot").ToString
                Me.TotalDataGridView.Rows(i).Cells("原因").Value = tab.Rows(i).Item("reasonforunavailable").ToString
                Me.TotalDataGridView.Rows(i).Cells("技能等级").Value = tab.Rows(i).Item("techgrade").ToString
                Me.TotalDataGridView.Rows(i).Cells("公里数").Value = tab.Rows(i).Item("KM").ToString
                Me.TotalDataGridView.Rows(i).Cells("开始统计").Value = tab.Rows(i).Item("enrolltime").ToString
                Me.TotalDataGridView.Rows(i).Cells("星级").Value = tab.Rows(i).Item("starlevel").ToString
                Me.TotalDataGridView.Rows(i).Cells("联系电话").Value = tab.Rows(i).Item("phone").ToString
                Me.TotalDataGridView.Rows(i).Cells("学徒").Value = tab.Rows(i).Item("apprentice").ToString
                Me.TotalDataGridView.Rows(i).Cells("师徒备注").Value = tab.Rows(i).Item("marelation").ToString
            Next
        End If
        tab.Dispose()
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Me.Close()
    End Sub

    Private Sub TSBRelation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBRelation.Click
        Dim frm As New M_A_Relation
        If Me.TotalDataGridView.SelectedRows.Count = 1 Then
            Dim SelectedRow As DataGridViewRow = Me.TotalDataGridView.SelectedRows(0)
            Dim Name As String = SelectedRow.Cells("姓名").Value.ToString.Trim
            Dim NO As String = SelectedRow.Cells("工号").Value.ToString.Trim
            Dim Tel As String = SelectedRow.Cells("联系电话").Value.ToString.Trim
            Dim LineName As String = SelectedRow.Cells("线路").Value.ToString.Trim
            Dim tmstr As String = "select * from cs_driverinf where drivername='" & Name & "' and rdriverno='" & NO & "' and lineid='" & LineName & "'"
            Dim tab As System.Data.DataTable = ReadData(tmstr)
            If tab IsNot Nothing AndAlso tab.Rows.Count = 1 Then
                Dim row As DataRow = tab.Rows(0)
                If row.Item("APPRENTICE").ToString = "是" AndAlso row.Item("marelation").ToString = "" Then
                    frm.TxtAName.Text = row.Item("drivername").ToString
                    frm.TxtANO.Text = row.Item("rdriverno").ToString
                    frm.TxtATel.Text = row.Item("phone").ToString
                    frm.TxtAPreM.Text = row.Item("marelation").ToString
                ElseIf row.Item("APPRENTICe").ToString = "是" AndAlso row.Item("marelation").ToString <> "" Then
                    frm.TxtAName.Text = row.Item("drivername").ToString
                    frm.TxtANO.Text = row.Item("rdriverno").ToString
                    frm.TxtATel.Text = row.Item("phone").ToString
                    frm.TxtAPreM.Text = row.Item("marelation").ToString
                    frm.TxtMNO.Text = row.Item("marelation").ToString

                ElseIf row.Item("APPRENTICe").ToString = "否" AndAlso row.Item("marelation").ToString = "" Then
                    frm.TxtMName.Text = row.Item("drivername").ToString
                    frm.TxtMNO.Text = row.Item("rdriverno").ToString
                    frm.TxtMTel.Text = row.Item("phone").ToString
                    frm.TxtPreA.Text = row.Item("marelation").ToString
                Else
                    frm.TxtMName.Text = row.Item("drivername").ToString
                    frm.TxtMNO.Text = row.Item("rdriverno").ToString
                    frm.TxtMTel.Text = row.Item("phone").ToString
                    frm.TxtPreA.Text = row.Item("marelation").ToString
                    frm.TxtANO.Text = row.Item("marelation").ToString
                End If
            End If
            If frm.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim str1 As String = "select * from cs_driverinf where rdriverno='" & frm.ANO & "' and lineid='" & LineName & "'"
                Dim tab1 As System.Data.DataTable = ReadData(str1)
                If tab1 IsNot Nothing AndAlso tab1.Rows.Count = 1 Then
                    Dim row1 As DataRow = tab1.Rows(0)
                    Dim i As Integer
                    For i = 0 To TotalDataGridView.Rows.Count - 1
                        If Me.TotalDataGridView.Rows(i).Cells("工号").Value = row1.Item("rdriverno").ToString Then
                            Me.TotalDataGridView.Rows(i).Cells("师徒备注").Value = row1.Item("marelation").ToString
                        End If
                    Next
                End If
                Dim str2 As String = "select * from cs_driverinf where rdriverno='" & frm.MNO & "' and lineid='" & LineName & "'"
                Dim tab2 As System.Data.DataTable = ReadData(str2)
                If tab2 IsNot Nothing AndAlso tab2.Rows.Count = 1 Then
                    Dim row2 As DataRow = tab2.Rows(0)
                    Dim j As Integer
                    For j = 0 To TotalDataGridView.Rows.Count - 1
                        If Me.TotalDataGridView.Rows(j).Cells("工号").Value = row2.Item("rdriverno").ToString Then
                            Me.TotalDataGridView.Rows(j).Cells("师徒备注").Value = row2.Item("marelation").ToString
                        End If
                    Next
                End If
                Dim str3 As String = "select * from cs_driverinf where rdriverno='" & frm.APreM & "' and lineid='" & LineName & "'"
                Dim tab3 As System.Data.DataTable = ReadData(str3)
                If tab3 IsNot Nothing AndAlso tab3.Rows.Count = 1 Then
                    Dim row3 As DataRow = tab3.Rows(0)
                    Dim k As Integer
                    For k = 0 To TotalDataGridView.Rows.Count - 1
                        If Me.TotalDataGridView.Rows(k).Cells("工号").Value = row3.Item("rdriverno").ToString Then
                            Me.TotalDataGridView.Rows(k).Cells("师徒备注").Value = row3.Item("marelation").ToString
                        End If
                    Next
                End If
                Dim str4 As String = "select * from cs_driverinf where rdriverno='" & frm.PreA & "' and lineid='" & LineName & "'"
                Dim tab4 As System.Data.DataTable = ReadData(str4)
                If tab4 IsNot Nothing AndAlso tab4.Rows.Count = 1 Then
                    Dim row4 As DataRow = tab4.Rows(0)
                    Dim l As Integer
                    For l = 0 To TotalDataGridView.Rows.Count - 1
                        If Me.TotalDataGridView.Rows(l).Cells("工号").Value = row4.Item("rdriverno").ToString Then
                            Me.TotalDataGridView.Rows(l).Cells("师徒备注").Value = row4.Item("marelation").ToString
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub 师徒ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 师徒ToolStripMenuItem.Click
        Call TSBRelation_Click(Nothing, Nothing)
    End Sub

    Public Sub New(ByVal linename As String)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        CurLineName = linename
    End Sub

    Private Sub TSBSearch_Click(sender As Object, e As EventArgs) Handles TSBSearch.Click
        Dim nf As New FrmComplexSeq
        If nf.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            Dim sqlStr As String = nf.SeqStr
            Dim tab As System.Data.DataTable = ReadData(sqlStr)
            If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
                Me.TotalDataGridView.Rows.Clear()
                For i As Integer = 0 To tab.Rows.Count - 1
                    Me.TotalDataGridView.Rows.Add()
                    Me.TotalDataGridView.Rows(i).Cells("线路").Value = tab.Rows(i).Item("lineid").ToString
                    Me.TotalDataGridView.Rows(i).Cells("班组").Value = tab.Rows(i).Item("beclass").ToString
                    Me.TotalDataGridView.Rows(i).Cells("组号").Value = tab.Rows(i).Item("beteam").ToString
                    Me.TotalDataGridView.Rows(i).Cells("工号").Value = tab.Rows(i).Item("rdriverno").ToString
                    Me.TotalDataGridView.Rows(i).Cells("姓名").Value = tab.Rows(i).Item("drivername").ToString
                    Me.TotalDataGridView.Rows(i).Cells("岗位").Value = tab.Rows(i).Item("post").ToString
                    Me.TotalDataGridView.Rows(i).Cells("区域").Value = tab.Rows(i).Item("bezone").ToString
                    Me.TotalDataGridView.Rows(i).Cells("工作证编号").Value = tab.Rows(i).Item("unicode").ToString
                    Me.TotalDataGridView.Rows(i).Cells("是否可用").Value = tab.Rows(i).Item("idleornot").ToString
                    Me.TotalDataGridView.Rows(i).Cells("原因").Value = tab.Rows(i).Item("reasonforunavailable").ToString
                    Me.TotalDataGridView.Rows(i).Cells("技能等级").Value = tab.Rows(i).Item("techgrade").ToString
                    Me.TotalDataGridView.Rows(i).Cells("公里数").Value = tab.Rows(i).Item("KM").ToString
                    Me.TotalDataGridView.Rows(i).Cells("开始统计").Value = tab.Rows(i).Item("enrolltime").ToString
                    Me.TotalDataGridView.Rows(i).Cells("星级").Value = tab.Rows(i).Item("starlevel").ToString
                    Me.TotalDataGridView.Rows(i).Cells("联系电话").Value = tab.Rows(i).Item("phone").ToString
                    Me.TotalDataGridView.Rows(i).Cells("学徒").Value = tab.Rows(i).Item("apprentice").ToString
                    Me.TotalDataGridView.Rows(i).Cells("师徒备注").Value = tab.Rows(i).Item("marelation").ToString
                Next
            Else
                MsgBox("没有找到记录！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
            End If
            tab.Dispose()
        End If
    End Sub

    Private Sub ToolStripButton4_Click(sender As Object, e As EventArgs) Handles ToolStripButton4.Click
        If MsgBox("确认更新云端人员信息？", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Dim str As String = "select * from cs_driverinf where lineid='" & CurLineName & "'"
            Dim tmpDT As New System.Data.DataTable
            tmpDT = ReadData(str)
            If IsNothing(tmpDT) = False AndAlso tmpDT.Rows.Count > 0 Then
                str = "delete from cs_driverinf where lineid='" & CurLineName & "'"
                Globle.Method.UpdateDataForOracle(str)
                Dim npro As New FrmProgress(tmpDT.Rows.Count, "正在上传人员信息...")
                For i As Integer = 0 To tmpDT.Rows.Count - 1
                    str = "INSERT INTO CS_DRIVERINF " _
                                  & "(lineid,beclass,beteam,rdriverno,drivername,bezone,enrolltime,techgrade,post,phone,idleornot,reasonforunavailable,KM,starlevel,apprentice,marelation,unicode)" _
                                  & "VALUES(" _
                                  & "'" & tmpDT.Rows(i).Item("lineid").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("beclass").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("beteam").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("rdriverno").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("drivername").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("bezone").ToString.Trim _
                                   & "','" & tmpDT.Rows(i).Item("enrolltime").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("techgrade").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("post").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("phone").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("idleornot").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("reasonforunavailable").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("KM").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("starlevel").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("apprentice").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("marelation").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("unicode").ToString.Trim _
                                  & "')"
                    Try
                        Globle.Method.UpdateDataForOracle(str)
                    Catch ex As Exception
                        npro.Close()
                        MsgBox("网络连接失败！")
                        Exit Sub
                    End Try
                    System.Threading.Thread.Sleep(30)
                    npro.Performstep()
                Next
                npro.Close()
                MsgBox("上传完毕！")
            End If
        End If
      
    End Sub

    Private Sub ToolStripButton5_Click(sender As Object, e As EventArgs) Handles ToolStripButton5.Click
        If MsgBox("确认下载云端人员信息？将覆盖本地", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Dim str As String = "select * from cs_driverinf where lineid='" & CurLineName & "'"
            Dim tmpDT As New System.Data.DataTable
            tmpDT = Globle.Method.ReadDataForOracle(str)
            If IsNothing(tmpDT) = False AndAlso tmpDT.Rows.Count > 0 Then
                str = "delete from cs_driverinf where lineid='" & CurLineName & "'"
                UpdateData(str)
                Dim npro As New FrmProgress(tmpDT.Rows.Count, "正在下载人员信息...")
                For i As Integer = 0 To tmpDT.Rows.Count - 1
                    Dim beteamNo As Integer = 0
                    If tmpDT.Rows(i).Item("beteam").ToString.Trim() <> "" Then
                        beteamNo = CInt(tmpDT.Rows(i).Item("beteam").ToString.Trim())
                    End If
                    str = "INSERT INTO CS_DRIVERINF " _
                                  & "(lineid,beclass,beteam,rdriverno,drivername,bezone,enrolltime,techgrade,post,phone,idleornot,reasonforunavailable,KM,starlevel,apprentice,marelation,unicode)" _
                                  & "VALUES(" _
                                  & "'" & tmpDT.Rows(i).Item("lineid").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("beclass").ToString.Trim _
                                  & "','" & beteamNo _
                                  & "','" & tmpDT.Rows(i).Item("rdriverno").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("drivername").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("bezone").ToString.Trim _
                                   & "','" & tmpDT.Rows(i).Item("enrolltime").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("techgrade").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("post").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("phone").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("idleornot").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("reasonforunavailable").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("KM").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("starlevel").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("apprentice").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("marelation").ToString.Trim _
                                  & "','" & tmpDT.Rows(i).Item("unicode").ToString.Trim _
                                  & "')"
                    Try
                        UpdateData(str)
                    Catch ex As Exception
                        npro.Close()
                        MsgBox("网络连接失败！" & vbCrLf & ex.ToString)
                        Exit Sub
                    End Try
                    System.Threading.Thread.Sleep(30)
                    npro.Performstep()
                Next
                npro.Close()
                MsgBox("下载完毕！")
            End If
        End If
    End Sub

    Private Sub TVDrivers_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TVDrivers.NodeMouseClick
        For i As Integer = 0 To Me.TotalDataGridView.Rows.Count - 1
            Me.TotalDataGridView.Rows(i).Selected = False
        Next
        If IsNothing(e.Node.Parent) = False Then
            If e.Node.Nodes.Count = 0 Then
                For i As Integer = 0 To Me.TotalDataGridView.Rows.Count - 1
                    If Me.TotalDataGridView.Rows(i).Cells("班组").Value.ToString = e.Node.Parent.Text And Me.TotalDataGridView.Rows(i).Cells("姓名").Value.ToString = e.Node.Text Then
                        Me.TotalDataGridView.Rows(i).Selected = True
                        Me.TotalDataGridView.FirstDisplayedScrollingRowIndex = i
                        Label1.Text = "暂无统计信息，请点击班组或线路"
                        Exit For
                    End If
                Next
            Else
                Dim lunzhuan As Integer = 0
                Dim beclassSum As Integer = 0
                For i As Integer = 0 To Me.TotalDataGridView.Rows.Count - 1
                    If Me.TotalDataGridView.Rows(i).Cells("班组").Value.ToString = e.Node.Text Then
                        beclassSum += 1
                        Me.TotalDataGridView.Rows(i).Selected = True
                        Me.TotalDataGridView.FirstDisplayedScrollingRowIndex = i
                        If Me.TotalDataGridView.Rows(i).Cells("组号").Value.ToString = "0" Or Me.TotalDataGridView.Rows(i).Cells("组号").Value.ToString = "" Or Me.TotalDataGridView.Rows(i).Cells("是否可用").Value.ToString = "不可用" Then
                            lunzhuan += 1
                        End If
                    End If
                Next
                Label1.Text = "统计信息：" & e.Node.Text & "总人数 " & beclassSum.ToString & ",可轮换人数 " & (beclassSum - lunzhuan).ToString
            End If

        Else
            Dim lunzhuan As Integer = 0
            For i As Integer = 0 To Me.TotalDataGridView.Rows.Count - 1
                If Me.TotalDataGridView.Rows(i).Cells("组号").Value.ToString = "0" Or Me.TotalDataGridView.Rows(i).Cells("组号").Value.ToString = "" Or Me.TotalDataGridView.Rows(i).Cells("是否可用").Value.ToString = "不可用" Then
                    lunzhuan += 1
                End If
            Next
            Label1.Text = "统计信息：总人数 " & Me.TotalDataGridView.RowCount.ToString & ",可轮换人数 " & (Me.TotalDataGridView.RowCount - lunzhuan).ToString
        End If
    End Sub

    Private Sub ToolStripButton6_Click(sender As Object, e As EventArgs) Handles ToolStripButton6.Click
        If IsNothing(TotalDataGridView.SelectedRows) = False AndAlso TotalDataGridView.SelectedRows.Count > 0 Then
            Dim DriverList As New Dictionary(Of String, String)
            For i As Integer = 0 To TotalDataGridView.SelectedRows.Count - 1
                DriverList.Add(TotalDataGridView.SelectedRows(i).Cells("工号").Value, TotalDataGridView.SelectedRows(i).Cells("姓名").Value.ToString)
            Next
            Dim frm1 As New FrmParaSet(DriverList)
            frm1.ShowDialog()
            If frm1.ifRefresh Then
                RefreshData()
            End If

        End If
    End Sub

    Private Sub ToolStripButton7_Click(sender As Object, e As EventArgs) Handles ToolStripButton7.Click
        Dim frm As New FrmOtherLineCrew
        frm.ShowDialog()
        If frm.ifRefresh Then
            RefreshData()
        End If
    End Sub
End Class