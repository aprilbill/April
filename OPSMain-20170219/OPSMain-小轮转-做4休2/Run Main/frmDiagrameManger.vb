Public Class frmDiagrameManger
    Dim TMS_TRAINDIAGRAMINFO As New DataTable
    Private Sub frmDiagrameManger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer = 0
        Me.cmbLine.Items.Clear()
        Me.cmbStyle.Items.Clear()
        Me.cmbStyle.Items.Add("全部")
        Dim sqlstr As String = "select * from TMS_TRAINDIAGRAMSTYLE order by TRAINDIASTYLENAME asc"
        Dim TMS_TRAINDIAGRAMSTYLE As New DataTable
        TMS_TRAINDIAGRAMSTYLE = Globle.Method.ReadDataForAccess(sqlstr)
        Me.cmbLine.Items.Add(CurLineName)
        Me.cmbLine.Text = CurLineName
        Me.cmbStyle.DataSource = TMS_TRAINDIAGRAMSTYLE
        Me.cmbStyle.DisplayMember = "DATENAME"
        Call ListDia(Me.cmbLine.Text.Trim, Me.cmbStyle.Text.Trim)

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim sCurSelectID As String
        If Me.Dgv.Rows.Count > 0 Then
            If Me.Dgv.CurrentCell.RowIndex >= 0 Then
                sCurSelectID = Me.Dgv.CurrentRow.Cells(0).Value
                If sCurSelectID <> "" Then
                    If MsgBox("确定删除 [" & sCurSelectID & "] 运行图码?", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "确定操作") = MsgBoxResult.Yes Then
                        Call DelteDiagrame(sCurSelectID)
                    End If
                End If
            End If
            Call ListDia(Me.cmbLine.Text.Trim, Me.cmbStyle.Text.Trim)
        End If
    End Sub

    '删除图
    Private Sub DelteDiagrame(ByVal sCurDiaID As String)
        Dim sqlstr As String = ""
        sqlstr = "delete from TMS_TRAINDIAGRAMINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_DIASTRUCTINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_LINEINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_RUNSCALE where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_RUNSCALEINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_SECTIONINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_SECTIONTIME where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_STATIONINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_STATIONTIME where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_STOCKUSINGINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_STOPSCALE where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_STOPSCALEINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_TIMETABLEINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_TIMETABLEPARAMETER where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_TIMETABLESTASEQ where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_TRAININFO where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_TRAININTERVALINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_TRAINRETURNTIME where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_TRAINUSINGINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_ROUTINGINDEXVALUE where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_STOCKUSINGINDEXVALUE where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_PERIODINDEXVALUE where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_TIMETABLE_WHOLE_INDEX where TRAINDIAGRAMEID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "delete from TMS_FIRLAST_TRAIN_TIMETABLE where TRAINDIAGRAMID = '" & sCurDiaID & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
    End Sub

    

    Private Sub btnEditName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditName.Click
        Dim sqlstr As String = "select * from TMS_TRAINDIAGRAMINFO"
        Dim tmpDT As New DataTable
        tmpDT = Globle.Method.ReadDataForAccess(sqlstr)
        Dim i As Integer
        If Me.Dgv.Rows.Count > 0 Then
            If Me.Dgv.CurrentCell.RowIndex >= 0 Then
                Dim sCurSelectID As String = Me.Dgv.CurrentRow.Cells(0).Value.ToString.Trim
                If sCurSelectID <> "" Then
                    For i = 1 To tmpDT.Rows.Count
                        If tmpDT.Rows(i - 1).Item("TRAINDIAGRAMID") = sCurSelectID And tmpDT.Rows(i - 1).Item("linename") = CurLineName Then
                            Dim frmNew As New frmInputBox
                            frmNew.Text = "输入运行图名称"
                            frmNew.labTitle.Text = "输入运行图图名称:"
                            frmNew.cmbText.Visible = False
                            frmNew.txtText.Visible = True
                            frmNew.txtText.Text = tmpDT.Rows(i - 1).Item("TIMETABLENAME")
                            frmNew.cmbText.Items.Clear()
                            frmNew.ShowDialog()
                            If frmNew.StrInputBoxText <> "" And frmNew.bCancelInput = 0 Then
                                tmpDT.Rows(i - 1).Item("TIMETABLENAME") = frmNew.StrInputBoxText
                            End If
                        End If
                    Next
                End If
            End If
            Globle.Method.UpdateDataForAccess(sqlstr, tmpDT)
            Call ListDia(Me.cmbLine.Text.Trim, Me.cmbStyle.Text.Trim)
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

   

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Call ListDia(Me.cmbLine.Text.Trim, Me.cmbStyle.Text.Trim)
    End Sub
    Private Sub ListDia(ByVal sCurLine As String, ByVal sCurStyle As String)
        Dim sqlstr As String = ""

        If sCurStyle = "全部" Then
            sqlstr = "select * from TMS_TRAINDIAGRAMINFO where LINENAME='" & CurLineName & "'"
            TMS_TRAINDIAGRAMINFO = Globle.Method.ReadDataForAccess(sqlstr)
        Else
            sqlstr = "select * from TMS_TRAINDIAGRAMINFO where TRAINDIASTYLENAME='" & sCurStyle & "' and LINENAME='" & CurLineName & "'"
            TMS_TRAINDIAGRAMINFO = Globle.Method.ReadDataForAccess(sqlstr)
        End If
        Me.Dgv.DataSource = TMS_TRAINDIAGRAMINFO
        Me.Dgv.ReadOnly = True
        Me.Dgv.MultiSelect = False
        Me.Dgv.Columns(0).HeaderText = "版本号"
        Me.Dgv.Columns(1).HeaderText = "线路名"
        Me.Dgv.Columns(2).HeaderText = "运行图名称"
        Me.Dgv.Columns(2).Width = 150
        Me.Dgv.Columns(3).HeaderText = "运行图类型"
        Me.Dgv.Columns(4).HeaderText = "导入时间"
        Me.Dgv.Columns(5).HeaderText = "开始执行时间"
        Me.Dgv.Columns(6).HeaderText = "结束执行时间"
        Me.Dgv.Columns(7).HeaderText = "编制部门"
        Me.Dgv.Columns(7).Width = 150
    End Sub

End Class