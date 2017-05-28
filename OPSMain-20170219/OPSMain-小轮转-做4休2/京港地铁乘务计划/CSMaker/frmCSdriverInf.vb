Public Class frmCSdriverInf

    Public ParentWindow As frmCSTimeTableMain
    Private Property CurMousex As Integer
    Private Property CurMousey As Integer
    Private Property CurCellx As Integer
    Private Property CurCelly As Integer

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        If CSTimeTablePara.nPubTrain > 0 And CSTimeTablePara.nPubCheDi > 0 Then
            Dim ForCSdriverNo, LaterCSdriverNo As String
            ForCSdriverNo = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSdriverNo

            Dim i, j As Integer
            For i = 0 To Me.dataGrid.Rows.Count - 1
                Select Case Me.dataGrid.Rows(i).Cells(1).Value.ToString.Trim
                    Case "乘务员编号"
                        LaterCSdriverNo = Me.dataGrid.Rows(i).Cells(2).Value
                        If LaterCSdriverNo = "" Then
                            MsgBox("乘务员编号空缺！")
                            Exit Sub
                        End If
                        For j = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                            If CSTrainsAndDrivers.CSDrivers(j).CSdriverNo = LaterCSdriverNo And CSTrainsAndDrivers.CSDrivers(j).CSDriverID <> CSTimeTablePara.nPubCheDi Then
                                MsgBox("编号重复", MsgBoxStyle.OkOnly, "错误信息")
                                Exit Sub
                            Else
                                CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSdriverNo = LaterCSdriverNo
                            End If
                        Next
                    Case "乘务员班种"
                        CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).DutySort = Me.dataGrid.Rows(i).Cells(2).Value
                        Dim temdri As CSDriver = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi)
                        Dim index As Integer = -1
                        Select Case temdri.DutySort
                            Case "早班"
                                index = CSTrainsAndDrivers.MorningDrivers.FindIndex(Function(value As CSDriver)
                                                                                        Return value = temdri
                                                                                    End Function)
                            Case "白班"
                                index = CSTrainsAndDrivers.DayDrivers.FindIndex(Function(value As CSDriver)
                                                                                    Return value = temdri
                                                                                End Function)
                            Case "日勤班"
                                index = CSTrainsAndDrivers.CDayDrivers.FindIndex(Function(value As CSDriver)
                                                                                     Return value = temdri
                                                                                 End Function)
                            Case "夜班"
                                index = CSTrainsAndDrivers.NightDrivers.FindIndex(Function(value As CSDriver)
                                                                                      Return value = temdri
                                                                                  End Function)
                            Case Else
                                index = CSTrainsAndDrivers.OtherDrivers.FindIndex(Function(value As CSDriver)
                                                                                      Return value = temdri
                                                                                  End Function)
                        End Select
                        If index = -1 Then
                            For Each dri As CSDriver In CSTrainsAndDrivers.MorningDrivers
                                If dri Is temdri Then
                                    CSTrainsAndDrivers.MorningDrivers.Remove(temdri)
                                    GoTo L
                                End If
                            Next
                            For Each dri As CSDriver In CSTrainsAndDrivers.DayDrivers
                                If dri Is temdri Then
                                    CSTrainsAndDrivers.DayDrivers.Remove(temdri)
                                    GoTo L
                                End If
                            Next
                            For Each dri As CSDriver In CSTrainsAndDrivers.CDayDrivers
                                If dri Is temdri Then
                                    CSTrainsAndDrivers.CDayDrivers.Remove(temdri)
                                    GoTo L
                                End If
                            Next
                            For Each dri As CSDriver In CSTrainsAndDrivers.NightDrivers
                                If dri Is temdri Then
                                    CSTrainsAndDrivers.NightDrivers.Remove(temdri)
                                    GoTo L
                                End If
                            Next
                            For Each dri As CSDriver In CSTrainsAndDrivers.OtherDrivers
                                If dri Is temdri Then
                                    CSTrainsAndDrivers.OtherDrivers.Remove(temdri)
                                    GoTo L
                                End If
                            Next
L:
                            Select Case temdri.DutySort
                                Case "早班"
                                    CSTrainsAndDrivers.MorningDrivers.Add(temdri)
                                Case "白班"
                                    CSTrainsAndDrivers.DayDrivers.Add(temdri)
                                Case "日勤班"
                                    CSTrainsAndDrivers.CDayDrivers.Add(temdri)
                                Case "夜班"
                                    CSTrainsAndDrivers.NightDrivers.Add(temdri)
                                Case Else
                                    CSTrainsAndDrivers.OtherDrivers.Add(temdri)
                            End Select
                        End If
                    Case "显示颜色"
                        If Me.dataGrid.Rows(i).Cells(2).Style.BackColor <> CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).ShowColor Then
                            CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).DefaultColor = Me.dataGrid.Rows(i).Cells(2).Style.BackColor
                        End If
                    Case "输出编号"
                        Dim OutPutNo As String = Me.dataGrid.Rows(i).Cells(2).Value.ToString.Trim
                        If OutPutNo = "" Then
                            MsgBox("输出编号空缺！")
                            Exit Sub
                        Else
                            CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).OutPutCSdriverNo = OutPutNo
                        End If
                    Case "所属区域"
                        Dim BelongArea As String = Me.dataGrid.Rows(i).Cells(2).Value.ToString.Trim
                        If BelongArea = "" Then
                            MsgBox("所属区域空缺！")
                            Exit Sub
                        Else
                            CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).BelongArea = BelongArea
                        End If
                End Select
            Next
            Me.ParentWindow.ListAllViewInfo()
            Call CSRefreshDiagram()
        End If
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub dataGrid_CellMouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dataGrid.CellMouseDown
        Cmb.Visible = False
        CurCellx = e.X
        CurCelly = e.Y
    End Sub

    Private Sub dataGrid_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dataGrid.MouseDown
        CurMousex = e.X
        CurMousey = e.Y
    End Sub

    Private Sub dataGrid_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dataGrid.CellClick
        If Me.dataGrid.CurrentCell IsNot Nothing AndAlso Me.dataGrid.Rows(Me.dataGrid.CurrentCell.RowIndex).Cells(1).Value = "乘务员班种" AndAlso _
            Me.dataGrid.Columns(Me.dataGrid.CurrentCell.ColumnIndex).Name = "信息值" Then
            Me.dataGrid.Controls.Add(Cmb)
            Cmb.Items.Clear()
            Cmb.Items.Add("早班")
            Cmb.Items.Add("白班")
            Cmb.Items.Add("日勤班")
            Cmb.Items.Add("夜班")
            Cmb.Size = Me.dataGrid.CurrentCell.Size
            Cmb.Location = New Point(CurMousex - CurCellx, CurMousey - CurCelly)
            If Me.dataGrid.CurrentCell.Value Is Nothing Then
                Cmb.Text = ""
            Else
                Cmb.Text = Me.dataGrid.CurrentCell.Value.ToString
            End If
            Cmb.Visible = True
        ElseIf Me.dataGrid.CurrentCell IsNot Nothing AndAlso Me.dataGrid.Rows(Me.dataGrid.CurrentCell.RowIndex).Cells(1).Value = "所属区域" AndAlso _
                Me.dataGrid.Columns(Me.dataGrid.CurrentCell.ColumnIndex).Name = "信息值" Then
            Me.dataGrid.Controls.Add(Cmb)
            Cmb.Items.Clear()
            If CSTrainsAndDrivers.AreaInfoS IsNot Nothing AndAlso CSTrainsAndDrivers.AreaInfoS.Count > 0 Then
                For Each area As AreaInfo In CSTrainsAndDrivers.AreaInfoS
                    Cmb.Items.Add(area.AreaName)
                Next
                Cmb.Size = Me.dataGrid.CurrentCell.Size
                Cmb.Location = New Point(CurMousex - CurCellx, CurMousey - CurCelly)
                If Me.dataGrid.CurrentCell.Value Is Nothing Then
                    Cmb.Text = ""
                Else
                    Cmb.Text = Me.dataGrid.CurrentCell.Value.ToString
                End If
                Cmb.Visible = True
            End If
        ElseIf Me.dataGrid.CurrentCell IsNot Nothing AndAlso Me.dataGrid.Rows(Me.dataGrid.CurrentCell.RowIndex).Cells(1).Value = "显示颜色" AndAlso _
                Me.dataGrid.Columns(Me.dataGrid.CurrentCell.ColumnIndex).Name = "信息值" Then
            Dim frmColor As New ColorDialog
            If frmColor.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.dataGrid.CurrentCell.Style.BackColor = frmColor.Color
            End If
        End If
    End Sub

    Private Sub Cmb_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmb.SelectedValueChanged
        Me.dataGrid.CurrentCell.Value = Cmb.Text.Trim
        Me.Cmb.Visible = False
    End Sub
End Class