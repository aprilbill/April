Public Class FrmSetDinner

    Public SelectDriver As CSDriver = Nothing
    Private Sub FrmSetDinner_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If SelectDriver IsNot Nothing Then
            Call ShowCurrentSatte()
        End If
    End Sub
    Public Sub ShowCurrentSatte()
        Me.Label2.Text = SelectDriver.CSdriverNo
        Me.DataGridView1.Rows.Clear()
        For i As Integer = 1 To UBound(SelectDriver.CSLinkTrain)
            Dim ttra As CSLinkTrain = SelectDriver.CSLinkTrain(i)
            Me.DataGridView1.Rows.Add(i, ttra.sCheDiHao & ttra.OutputCheCi, BeTime(ttra.StartTime), ttra.StartStaName, BeTime(ttra.EndTime), ttra.EndStaName)
            For Each strkey As String In SelectDriver.AllDinnerInfo.Keys
                If AddLitterTime(ttra.EndTime) <= strkey.Split("-")(1) AndAlso AddLitterTime(ttra.EndTime) + SelectDriver.AllDinnerInfo(strkey).DinnerTime <= strkey.Split("-")(2) AndAlso ttra.EndStaName = strkey.Split("-")(0) Then
                    If AddLitterTime(ttra.EndTime) >= SelectDriver.AllDinnerInfo(strkey).dinnerStartTime AndAlso AddLitterTime(ttra.EndTime) <= SelectDriver.AllDinnerInfo(strkey).dinnerEndTime Then
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).DefaultCellStyle.BackColor = Color.LightGreen
                        Exit For
                    End If
                End If
                If i = 1 And AddLitterTime(ttra.StartTime) >= SelectDriver.AllDinnerInfo(strkey).dinnerStartTime AndAlso AddLitterTime(ttra.StartTime) <= SelectDriver.AllDinnerInfo(strkey).dinnerEndTime AndAlso ttra.StartStaName = SelectDriver.AllDinnerInfo(strkey).DinnerStationName Then
                    Button3.Text = "取消班前用餐"
                    Exit For
                End If
            Next
        Next
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Public Sub ShowDinnerInfo(ByVal rowindex As Integer)
        If DataGridView1.Rows.Count > 0 AndAlso DataGridView1.Rows(rowindex).DefaultCellStyle.BackColor = Color.Green Then
            Dim ttra As CSLinkTrain = SelectDriver.CSLinkTrain(rowindex + 1)
            For Each strkey As typeDinnerStation In SelectDriver.AllDinnerInfo.Values
                If AddLitterTime(ttra.EndTime) >= strkey.dinnerStartTime AndAlso AddLitterTime(ttra.EndTime) <= strkey.dinnerEndTime AndAlso ttra.EndStaName = strkey.DinnerStationName Then
                    If rowindex = UBound(SelectDriver.CSLinkTrain) - 1 Then
                        Label4.Text = "退勤后用餐" & vbCrLf & "用餐车次:" & ttra.sCheDiHao & ttra.OutputCheCi & _
                        "/" & "开始时间:" & BeTime(ttra.EndTime) & "用餐地点:" & ttra.EndStaName
                    Else
                        Label4.Text = "班中用餐" & vbCrLf & "用餐车次:" & ttra.sCheDiHao & ttra.OutputCheCi & _
                          "/" & "开始时间:" & BeTime(ttra.EndTime) & _
                          vbCrLf & "接车车次:" & SelectDriver.CSLinkTrain(rowindex + 1 + 1).sCheDiHao & SelectDriver.CSLinkTrain(rowindex + 1 + 1).OutputCheCi & _
                          "/" & "接车时间:" & BeTime(SelectDriver.CSLinkTrain(rowindex + 1 + 1).StartTime) & _
                          vbCrLf & "接车地点:" & ttra.EndStaName
                    End If
                End If
            Next
        Else
            Label4.Text = "未用餐"
        End If
    End Sub
    Dim dintime As typeDinnerStation = Nothing
  
    Private Sub 设为用餐车次ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 设为用餐车次ToolStripMenuItem.Click
        If Me.DataGridView1.SelectedRows.Count = 1 AndAlso Me.DataGridView1.SelectedRows(0).Index < Me.DataGridView1.Rows.Count - 1 Then
            If sysDinnerStation IsNot Nothing AndAlso UBound(sysDinnerStation) > 0 Then
                dintime = Nothing
                Dim ttrain As CSLinkTrain = Nothing
                Dim i As Integer = Me.DataGridView1.SelectedRows(0).Index + 1
                For j As Integer = 1 To UBound(sysDinnerStation)
                    If sysDinnerStation(j).dutySort = SelectDriver.DutySort AndAlso sysDinnerStation(j).DinnerStationName = SelectDriver.CSLinkTrain(i).EndStaName And sysDinnerStation(j).Routing = SelectDriver.CSLinkTrain(i).RoutingName Then
                        If AddLitterTime(sysDinnerStation(j).dinnerStartTime) <= AddLitterTime(SelectDriver.CSLinkTrain(i).EndTime) And AddLitterTime(sysDinnerStation(j).dinnerEndTime) >= AddLitterTime(SelectDriver.CSLinkTrain(i).EndTime) Then
                            If AddLitterTime(SelectDriver.CSLinkTrain(i + 1).StartTime) - AddLitterTime(SelectDriver.CSLinkTrain(i).EndTime) >= sysDinnerStation(j).DinnerTime Then
                                ttrain = SelectDriver.CSLinkTrain(i)
                                dintime = sysDinnerStation(j)
                                Exit For
                            End If
                        End If
                    End If
                Next
                    
                If dintime IsNot Nothing AndAlso ttrain IsNot Nothing Then
                    For Each str As String In SelectDriver.AllDinnerInfo.Keys
                        If dintime.dinnerType = SelectDriver.AllDinnerInfo(str).dinnerType Then
                            SelectDriver.AllDinnerInfo.Remove(str)
                            Exit For
                        End If
                    Next
                    SelectDriver.FlagDinner = True
                    SelectDriver.AllDinnerInfo.Add(ttrain.EndStaName & "-" & AddLitterTime(ttrain.EndTime).ToString & "-" & (AddLitterTime(ttrain.EndTime) + dintime.DinnerTime).ToString, dintime)
                    Call ShowCurrentSatte()
                Else
                    MsgBox("该位置不能完成用餐，用餐地点不对或用餐时间不够！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
                    Exit Sub
                End If

            Else
                MsgBox("没有找到用餐参数，不能完成设置！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
                Exit Sub
            End If
        Else
            MsgBox("选择位置不对！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
    End Sub

    Private Sub DataGridView1_CellMouseDown(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDown
        If IsNothing(DataGridView1.Rows) Then
            Exit Sub
        End If
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            DataGridView1.Rows(i).Selected = False
        Next
        If e.Button = Windows.Forms.MouseButtons.Right And e.ColumnIndex > -1 And e.RowIndex > -1 Then
            DataGridView1.Rows(e.RowIndex).Selected = True
            ContextMenuStrip1.Show(MousePosition.X, MousePosition.Y)
        End If
        If e.Button = Windows.Forms.MouseButtons.Left And e.ColumnIndex > -1 And e.RowIndex > -1 Then
            DataGridView1.Rows(e.RowIndex).Selected = True
            ShowDinnerInfo(e.RowIndex)
        End If
    End Sub

    
    Private Sub 设为出勤后用餐ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 设为出勤后用餐ToolStripMenuItem.Click
        If Me.DataGridView1.SelectedRows.Count = 1 Then
            If sysDinnerStation IsNot Nothing AndAlso UBound(sysDinnerStation) > 0 Then
                dintime = Nothing
                Dim ttrain As CSLinkTrain = Nothing
                Dim i As Integer = UBound(SelectDriver.CSLinkTrain)
                For j As Integer = 1 To UBound(sysDinnerStation)
                    If sysDinnerStation(j).dutySort = SelectDriver.DutySort AndAlso sysDinnerStation(j).DinnerStationName = SelectDriver.CSLinkTrain(i).EndStaName And sysDinnerStation(j).Routing = SelectDriver.CSLinkTrain(i).RoutingName Then
                        If AddLitterTime(sysDinnerStation(j).dinnerStartTime) <= AddLitterTime(SelectDriver.CSLinkTrain(i).EndTime) And AddLitterTime(sysDinnerStation(j).dinnerEndTime) >= AddLitterTime(SelectDriver.CSLinkTrain(i).EndTime) Then
                            ttrain = SelectDriver.CSLinkTrain(i)
                            dintime = sysDinnerStation(j)
                            Exit For
                        End If
                    End If
                Next

                If dintime IsNot Nothing AndAlso ttrain IsNot Nothing Then
                    For Each str As String In SelectDriver.AllDinnerInfo.Keys
                        If dintime.dinnerType = SelectDriver.AllDinnerInfo(str).dinnerType Then
                            SelectDriver.AllDinnerInfo.Remove(str)
                            Exit For
                        End If
                    Next
                    SelectDriver.FlagDinner = True
                    SelectDriver.AllDinnerInfo.Add(ttrain.EndStaName & "-" & AddLitterTime(ttrain.EndTime).ToString & "-" & (AddLitterTime(ttrain.EndTime) + dintime.DinnerTime).ToString, dintime)
                    Call ShowCurrentSatte()
                Else
                    MsgBox("该位置不能完成用餐，用餐地点不对或用餐时间不够！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
                    Exit Sub
                End If

            Else
                MsgBox("没有找到用餐参数，不能完成设置！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
                Exit Sub
            End If
        Else
            MsgBox("选择位置不对！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
    End Sub

    Private Sub 取消用餐ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 取消用餐ToolStripMenuItem.Click
        If Me.DataGridView1.SelectedRows.Count = 1 AndAlso Me.DataGridView1.SelectedRows(0).DefaultCellStyle.BackColor = Color.LightGreen Then
            If sysDinnerStation IsNot Nothing AndAlso UBound(sysDinnerStation) > 0 Then
                dintime = Nothing
                Dim i As Integer = Me.DataGridView1.SelectedRows(0).Index + 1
                For j As Integer = 1 To UBound(sysDinnerStation)
                    If sysDinnerStation(j).dutySort = SelectDriver.DutySort AndAlso sysDinnerStation(j).DinnerStationName = SelectDriver.CSLinkTrain(i).EndStaName And sysDinnerStation(j).Routing = SelectDriver.CSLinkTrain(i).RoutingName Then
                        If AddLitterTime(sysDinnerStation(j).dinnerStartTime) <= AddLitterTime(SelectDriver.CSLinkTrain(i).EndTime) And AddLitterTime(sysDinnerStation(j).dinnerEndTime) >= AddLitterTime(SelectDriver.CSLinkTrain(i).EndTime) Then
                            dintime = sysDinnerStation(j)
                            Exit For
                        End If
                    End If
                Next

                If dintime IsNot Nothing Then
                    For Each str As String In SelectDriver.AllDinnerInfo.Keys
                        If dintime.dinnerType = SelectDriver.AllDinnerInfo(str).dinnerType Then
                            If MsgBox("确认删除选中的用餐？", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                                SelectDriver.AllDinnerInfo.Remove(str)
                                If SelectDriver.AllDinnerInfo.Count = 0 Then
                                    SelectDriver.FlagDinner = False
                                End If
                                Exit For
                            End If
                        End If
                    Next
                    Call ShowCurrentSatte()
                End If
            Else
                MsgBox("没有找到用餐参数，不能完成删除！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
                Exit Sub
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Button3.Text = "班前用餐" Then
            If sysDinnerStation IsNot Nothing AndAlso UBound(sysDinnerStation) > 0 Then
                dintime = Nothing
                For j As Integer = 1 To UBound(sysDinnerStation)
                    If sysDinnerStation(j).dutySort = SelectDriver.DutySort AndAlso sysDinnerStation(j).DinnerStationName = SelectDriver.CSLinkTrain(1).StartStaName Then
                        For i As Integer = 1 To UBound(Jiaolu)
                            If Jiaolu(i).JiaoluName = sysDinnerStation(j).Routing And Jiaolu(i).ReJiaoluName = SelectDriver.CSLinkTrain(1).RoutingName Then
                                If AddLitterTime(sysDinnerStation(j).dinnerStartTime) <= AddLitterTime(SelectDriver.CSLinkTrain(1).StartTime) And AddLitterTime(sysDinnerStation(j).dinnerEndTime) >= AddLitterTime(SelectDriver.CSLinkTrain(1).StartTime) Then
                                    dintime = sysDinnerStation(j)
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                Next
                If dintime IsNot Nothing Then
                    For Each str As String In SelectDriver.AllDinnerInfo.Keys
                        If dintime.dinnerType = SelectDriver.AllDinnerInfo(str).dinnerType Then
                            If MsgBox("存在同一时段吃饭两次情况，继续设置班前用餐将会删除另一次吃饭", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                                SelectDriver.AllDinnerInfo.Remove(str)
                                Exit For
                            Else
                                Exit Sub
                            End If
                        End If
                    Next
                    SelectDriver.FlagDinner = True
                    SelectDriver.AllDinnerInfo.Add(SelectDriver.CSLinkTrain(1).StartStaName & "-" & (AddLitterTime(SelectDriver.CSLinkTrain(1).StartTime) - dintime.DinnerTime).ToString & "-" & AddLitterTime(SelectDriver.CSLinkTrain(1).StartTime).ToString, dintime)
                    Call ShowCurrentSatte()
                Else
                    MsgBox("该位置不能完成用餐，用餐地点不对或用餐时间不够！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
                    Exit Sub
                End If
            Else
                MsgBox("没有找到用餐参数，不能完成设置！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
                Exit Sub
            End If
        Else
            If MsgBox("确认取消班前用餐", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                For j As Integer = 1 To UBound(sysDinnerStation)
                    If sysDinnerStation(j).dutySort = SelectDriver.DutySort AndAlso sysDinnerStation(j).DinnerStationName = SelectDriver.CSLinkTrain(1).StartStaName Then
                        For i As Integer = 1 To UBound(Jiaolu)
                            If Jiaolu(i).JiaoluName = sysDinnerStation(j).Routing And Jiaolu(i).ReJiaoluName = SelectDriver.CSLinkTrain(1).RoutingName Then
                                If AddLitterTime(sysDinnerStation(j).dinnerStartTime) <= AddLitterTime(SelectDriver.CSLinkTrain(1).StartTime) And AddLitterTime(sysDinnerStation(j).dinnerEndTime) >= AddLitterTime(SelectDriver.CSLinkTrain(1).StartTime) Then
                                    dintime = sysDinnerStation(j)
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                Next
                If dintime IsNot Nothing Then
                    For Each str As String In SelectDriver.AllDinnerInfo.Keys
                        If dintime.dinnerType = SelectDriver.AllDinnerInfo(str).dinnerType Then
                            SelectDriver.AllDinnerInfo.Remove(str)
                            Button3.Text = "班前用餐"
                            If SelectDriver.AllDinnerInfo.Count = 0 Then
                                SelectDriver.FlagDinner = False
                            End If
                            Call ShowCurrentSatte()
                            Exit Sub
                        End If
                    Next
                End If
            End If
        End If
        
    End Sub
End Class