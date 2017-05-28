Public Class frmDiagrameManger
    Public UserInfo As String = "集团/所有线路"

   
    Private Sub frmDiagrameManger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        TMSlocalDataSet.PD_LINEINFO.Clear()
        TMSlocalDataSet.TMS_TRAINDIAGRAMSTYLE.Clear()
        Dim tmpUserInfo() As String = UserInfo.Split("/")
        If tmpUserInfo(0) = "空" Then

            TMSlocalDataSet.Fill("PD_LINEINFO", "linename='" & tmpUserInfo(1) & "' order by lineid asc")
        End If
        If tmpUserInfo(0) <> "空" And tmpUserInfo(0).Contains("集团") Then

            TMSlocalDataSet.Fill("PD_LINEINFO", "1=1 order by lineid asc")
        End If
        If tmpUserInfo(0) <> "空" And tmpUserInfo(0).Contains("集团") = False Then

            TMSlocalDataSet.Fill("PD_LINEINFO", "linemanagerid='" & tmpUserInfo(0) & "' order by lineid asc")
        End If

        Dim i As Integer
        Me.cmbLine.Items.Clear()
        Me.cmbStyle.Items.Clear()
        Me.cmbStyle.Items.Add("全部")

        If CurLineName = "" Then        '20160306修改，功能：本线管理员只能管理本线运行图
            'TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMSTYLE", "1=1 order by TRAINDIASTYLENAME asc")

            'For j = 1 To TMSlocalDataSet.TMS_TRAINDIAGRAMSTYLE.Rows.Count
            '    Me.cmbStyle.Items.Add(TMSlocalDataSet.TMS_TRAINDIAGRAMSTYLE.Rows(j - 1).Item("DATENAME"))
            'Next
            'If Me.cmbStyle.Items.Count > 0 Then
            '    Me.cmbStyle.Text = Me.cmbStyle.Items(0)
            'End If

            Me.cmbLine.Items.Add("全部")
            For i = 1 To TMSlocalDataSet.PD_LINEINFO.Rows.Count
                Me.cmbLine.Items.Add(TMSlocalDataSet.PD_LINEINFO.Rows(i - 1).Item("LINENAME"))
            Next
            If Me.cmbLine.Items.Count > 0 Then
                Me.cmbLine.Text = Me.cmbLine.Items(0)
            End If

        Else
            TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMSTYLE", "1=1 order by TRAINDIASTYLENAME asc") '’lineid='" & CurLineName & "'
            Me.cmbLine.Items.Add(CurLineName)
            Me.cmbLine.Text = CurLineName
            Me.cmbStyle.DataSource = TMSlocalDataSet.TMS_TRAINDIAGRAMSTYLE
            Me.cmbStyle.DisplayMember = "DATENAME"

            'For j = 1 To TMSlocalDataSet.TMS_TRAINDIAGRAMSTYLE.Rows.Count
            '    Me.cmbStyle.Items.Add(TMSlocalDataSet.TMS_TRAINDIAGRAMSTYLE.Rows(j - 1).Item("DATENAME"))
            'Next
            'If Me.cmbStyle.Items.Count > 0 Then
            'Me.cmbStyle.Text = Me.cmbStyle.Items(0) = "全部"
            'End If

        End If

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


    Private Sub btnDeleteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim sCurSelectID As String
        If Me.Dgv.Rows.Count > 0 Then
            If Me.Dgv.CurrentCell.RowIndex >= 0 Then
                sCurSelectID = Me.Dgv.CurrentRow.Cells(0).Value.ToString.Trim
                If sCurSelectID <> "" Then
                    If MsgBox("确定删除所有运行图码?", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "确定操作") = MsgBoxResult.Yes Then
                        Call DelteAllDiagrame()
                    End If
                End If
            End If
            TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Clear()
            TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMINFO")
            Me.Dgv.DataSource = TMSlocalDataSet.TMS_TRAINDIAGRAMINFO
        End If
    End Sub

    '删除图
    Private Sub DelteDiagrame(ByVal sCurDiaID As String)
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TRAINDIAGRAMINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_DIASTRUCTINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_LINEINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_RUNSCALE where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_RUNSCALEINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_SECTIONINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_SECTIONTIME where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_STATIONINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_STATIONTIME where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_STOCKUSINGINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_STOPSCALE where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_STOPSCALEINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TIMETABLEINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TIMETABLEPARAMETER where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TIMETABLESTASEQ where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TRAININFO where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TRAININTERVALINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TRAINRETURNTIME where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TRAINUSINGINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_ROUTINGINDEXVALUE where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_STOCKUSINGINDEXVALUE where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_PERIODINDEXVALUE where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TIMETABLE_WHOLE_INDEX where TRAINDIAGRAMEID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_FIRLAST_TRAIN_TIMETABLE where TRAINDIAGRAMID = '" & sCurDiaID & "'")

    End Sub

    '删除图
    Private Sub DelteAllDiagrame()
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TRAINDIAGRAMINFO")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_DIASTRUCTINFO")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_LINEINFO")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_RUNSCALE")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_RUNSCALEINFO")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_SECTIONINFO")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_SECTIONTIME")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_STATIONINFO")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_STATIONTIME")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_STOCKUSINGINFO")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_STOPSCALE")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_STOPSCALEINFO")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TIMETABLEINFO")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TIMETABLEPARAMETER")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TIMETABLESTASEQ")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TRAININFO")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TRAININTERVALINFO")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TRAINRETURNTIME")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TRAINUSINGINFO")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_ROUTINGINDEXVALUE")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_STOCKUSINGINDEXVALUE")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_PERIODINDEXVALUE")
    End Sub

    Private Sub btnEditName_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditName.Click
        Dim sCurSelectID As String
        Dim i As Integer
        If Me.Dgv.Rows.Count > 0 Then
            If Me.Dgv.CurrentCell.RowIndex >= 0 Then
                sCurSelectID = Me.Dgv.CurrentRow.Cells(0).Value.ToString.Trim
                If sCurSelectID <> "" Then
                    For i = 1 To TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Rows.Count
                        If TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Rows(i - 1).Item("TRAINDIAGRAMID") = sCurSelectID Then
                            Dim frmNew As New frmInputBox
                            frmNew.Text = "输入运行图名称"
                            frmNew.labTitle.Text = "输入运行图图名称:"
                            frmNew.cmbText.Visible = False
                            frmNew.txtText.Visible = True
                            frmNew.txtText.Text = TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Rows(i - 1).Item("TIMETABLENAME")
                            frmNew.cmbText.Items.Clear()
                            frmNew.ShowDialog()
                            If StrInputBoxText <> "" And bCancelInput = 0 Then
                                TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Rows(i - 1).Item("TIMETABLENAME") = StrInputBoxText
                            End If
                        End If
                    Next
                End If
            End If
            TMSlocalDataSet.Update("TMS_TRAINDIAGRAMINFO")
            Call ListDia(Me.cmbLine.Text.Trim, Me.cmbStyle.Text.Trim)
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        Dim sCurSelectID As String
        If Me.Dgv.Rows.Count > 0 Then
            If Me.Dgv.CurrentCell.RowIndex >= 0 Then
                sCurSelectID = Me.Dgv.CurrentRow.Cells(0).Value.ToString.Trim
                ODSPubpara.DiagramSelect = sCurSelectID
                Call LoadDiagramData("打开运行图")
                ODSPubpara.sCurShowListState = "显示单线全图"
                Dim nf As New frmODSTimeTableMain
                nf.Show()
            End If
        End If
    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Call ListDia(Me.cmbLine.Text.Trim, Me.cmbStyle.Text.Trim)
    End Sub
    Private Sub ListDia(ByVal sCurLine As String, ByVal sCurStyle As String)
        TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Clear()
        If sCurLine = "全部" Then
            Dim tmpUserInfo() As String = UserInfo.Split("/")
            If sCurStyle = "全部" Then
                If tmpUserInfo(0) = "空" Then
                    TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMINFO", "linename='" & tmpUserInfo(1) & "'")
                End If
                If tmpUserInfo(0) <> "空" And tmpUserInfo(0).Contains("集团") Then
                    TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMINFO")
                End If
                If tmpUserInfo(0) <> "空" And tmpUserInfo(0).Contains("集团") = False Then
                    Dim Lines As String = ""
                    For i As Integer = 1 To Me.cmbLine.Items.Count - 1
                        If i = Me.cmbLine.Items.Count - 1 Then
                            Lines += "linename='" + Me.cmbLine.Items(i).ToString + "'"
                        Else
                            Lines += "linename='" + Me.cmbLine.Items(i).ToString + "' or "
                        End If
                    Next
                    TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMINFO", Lines & " order by lineid asc")
                End If
            Else
                If tmpUserInfo(0) = "空" Then
                    TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMINFO", "linename='" & tmpUserInfo(1) & "' and " & "TRAINDIASTYLENAME='" & sCurStyle & "'")
                End If
                If tmpUserInfo(0) <> "空" And tmpUserInfo(0).Contains("集团") Then
                    TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMINFO", "TRAINDIASTYLENAME='" & sCurStyle & "'")
                End If
                If tmpUserInfo(0) <> "空" And tmpUserInfo(0).Contains("集团") = False Then
                    Dim Lines As String = ""
                    For i As Integer = 1 To Me.cmbLine.Items.Count - 1
                        If i = Me.cmbLine.Items.Count - 1 Then
                            Lines += "linename='" + Me.cmbLine.Items(i).ToString + "'"
                        Else
                            Lines += "linename='" + Me.cmbLine.Items(i).ToString + "' or "
                        End If
                    Next
                    TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMINFO", Lines & " and TRAINDIASTYLENAME='" & sCurStyle & "'" & " order by lineid asc")
                End If
            End If
        Else
            If sCurStyle = "全部" Then
                TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMINFO", "LINENAME='" & sCurLine & "'")
            Else
                TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMINFO", "TRAINDIASTYLENAME='" & sCurStyle & "' and LINENAME='" & sCurLine & "'")
            End If
        End If
        Me.Dgv.DataSource = TMSlocalDataSet.TMS_TRAINDIAGRAMINFO
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

   
    Private Sub cmbLine_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLine.SelectedIndexChanged
        If Me.cmbLine.Text = "全部" Then
            Me.cmbStyle.Text = "全部"
        Else
            TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMSTYLE", "1=1 order by TRAINDIASTYLENAME asc") 'lineid='" & cmbLine.Text & "'
            Me.cmbStyle.DataSource = TMSlocalDataSet.TMS_TRAINDIAGRAMSTYLE
            Me.cmbStyle.DisplayMember = "DATENAME"
        End If
    End Sub
End Class