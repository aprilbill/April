Imports System.Windows.Forms.DataVisualization.Charting

Public Class DriverPerformance
    Dim daydrivelength As DayDriveLength
    Dim daydrivetime As DayDriveTime
    Dim spandrivelength As SpanDriveLength
    Dim spandrivetime As SpanDriveTime
    Dim daytrainlength As DayTrainDriveLength
    Dim daytraintime As DayTrainDriveTime
    Dim spantrainlength As SpanTrainDriveLength
    Dim spantraintime As SpanTrainDriveTime
    Dim DriverTeams As List(Of List(Of RDriver))

    Public Sub New()

        ' 此调用是 Windows 窗体设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        DriverTeams = New List(Of List(Of RDriver))()
    End Sub

    Private Sub DriverPerformance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.TreeView1.ExpandAll()
        Me.Load_Drivers()
    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        Me.TreeView1.SelectedNode = e.Node
        If e.Node.Text = "司机日行驶公里统计" Then
            daydrivelength = New DayDriveLength(DriverTeams)
            Me.PropertyGrid1.SelectedObject = daydrivelength
            Me.DataGridView1.ContextMenuStrip = Nothing

        ElseIf e.Node.Text = "司机日行驶工时统计" Then
            daydrivetime = New DayDriveTime(DriverTeams)
            Me.PropertyGrid1.SelectedObject = daydrivetime
            Me.DataGridView1.ContextMenuStrip = Nothing

        ElseIf e.Node.Text = "司机阶段行驶公里统计" Then
            spandrivelength = New SpanDriveLength(DriverTeams)
            Me.PropertyGrid1.SelectedObject = spandrivelength

        ElseIf e.Node.Text = "司机阶段行驶工时统计" Then
            spandrivetime = New SpanDriveTime(DriverTeams)
            Me.PropertyGrid1.SelectedObject = spandrivetime

        ElseIf e.Node.Text = "车底日走行公里统计" Then
            daytrainlength = New DayTrainDriveLength()
            Me.PropertyGrid1.SelectedObject = daytrainlength

        ElseIf e.Node.Text = "车底日走行时间统计" Then
            daytraintime = New DayTrainDriveTime()
            Me.PropertyGrid1.SelectedObject = daytraintime

        ElseIf e.Node.Text = "车底阶段走行公里统计" Then
            spantrainlength = New SpanTrainDriveLength()
            Me.PropertyGrid1.SelectedObject = spantrainlength

        ElseIf e.Node.Text = "车底阶段走行时间统计" Then
            spantraintime = New SpanTrainDriveTime()
            Me.PropertyGrid1.SelectedObject = spantraintime
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click

        If Me.TreeView1.SelectedNode.Text = "司机日行驶公里统计" Then

            If daydrivelength.ReportDate = Nothing Then
                MessageBox.Show("请选择统计日期！")
                Return
            End If

            daydrivelength.LoadData()
            If Me.DriverTeams(0)(0).CSDriverList.Count = 0 Then
                MsgBox("没有数据!")
                Exit Sub
            End If

            Me.DataGridView1.Columns.Clear()
            Me.DataGridView1.Rows.Clear()
            Me.DataGridView1.Columns.Add("线路编号", "线路编号")
            Me.DataGridView1.Columns.Add("司机编号", "司机编号")
            Me.DataGridView1.Columns.Add("司机姓名", "司机姓名")
            For i As Integer = 0 To daydrivelength.MaxDutyNum - 1
                Me.DataGridView1.Columns.Add("任务" & (i + 1), "任务" & (i + 1))
            Next
            Me.DataGridView1.Columns.Add("日总公里", "日总公里")
            Dim count As Integer = 0
            For Each team As List(Of RDriver) In daydrivelength.TeamsDrivers
                For Each rdr As RDriver In team
                    Me.DataGridView1.Rows.Add(rdr.LineId, rdr.RDriverNo, rdr.DriverName)
                    If rdr.CSDriverList(0).LinkDutyList.Count > 0 Then
                        For i As Integer = 0 To rdr.CSDriverList(0).LinkDutyList.Count - 1
                            Me.DataGridView1.Rows(count).Cells(3 + i).Value = rdr.CSDriverList(0).LinkDutyList(i).DriveDistance
                        Next
                    Else
                        If rdr.CSDriverList(0).DutySort = "休息" Then
                            Me.DataGridView1.Rows(count).Cells(3).Value = rdr.CSDriverList(0).DutySort
                        ElseIf rdr.CSDriverList(0).DriverNo.Contains("备班") Then
                            Me.DataGridView1.Rows(count).Cells(3).Value = "备班"
                        ElseIf rdr.CSDriverList(0).DriverNo.Contains("备车") Then
                            Me.DataGridView1.Rows(count).Cells(3).Value = "备车"
                        Else
                            Me.DataGridView1.Rows(count).Cells(3).Value = "无任务"
                        End If
                    End If
                    Me.DataGridView1.Rows(count).Cells(Me.DataGridView1.ColumnCount - 1).Value = rdr.CSDriverList(0).TotalDriverDistance
                    count += 1
                Next
            Next
            

        ElseIf Me.TreeView1.SelectedNode.Text = "司机日行驶工时统计" Then

            If daydrivetime.ReportDate = Nothing Then
                MessageBox.Show("请选择统计日期！")
                Return
            End If

            daydrivetime.LoadData()
            If Me.DriverTeams(0)(0).CSDriverList.Count = 0 Then
                MsgBox("没有数据!")
                Exit Sub
            End If

            Me.DataGridView1.Columns.Clear()
            Me.DataGridView1.Rows.Clear()
            Me.DataGridView1.Columns.Add("线路编号", "线路编号")
            Me.DataGridView1.Columns.Add("司机编号", "司机编号")
            Me.DataGridView1.Columns.Add("司机姓名", "司机姓名")
            For i As Integer = 0 To daydrivetime.MaxDutyNum - 1
                Me.DataGridView1.Columns.Add("任务" & (i + 1), "任务" & (i + 1))
            Next
            Me.DataGridView1.Columns.Add("日总工时", "日总工时")
            Dim count As Integer = 0
            For Each team As List(Of RDriver) In daydrivetime.TeamsDrivers
                For Each rdr As RDriver In team
                    Me.DataGridView1.Rows.Add(rdr.LineId, rdr.RDriverNo, rdr.DriverName)
                    If rdr.CSDriverList(0).LinkDutyList.Count > 0 Then
                        For i As Integer = 0 To rdr.CSDriverList(0).LinkDutyList.Count - 1
                            Me.DataGridView1.Rows(count).Cells(3 + i).Value = Coordination2.Global.BeTime2(rdr.CSDriverList(0).LinkDutyList(i).Drivetime)
                        Next
                    Else
                        If rdr.CSDriverList(0).DutySort = "休息" Then
                            Me.DataGridView1.Rows(count).Cells(3).Value = rdr.CSDriverList(0).DutySort
                        ElseIf rdr.CSDriverList(0).DriverNo.Contains("备班") Then
                            Me.DataGridView1.Rows(count).Cells(3).Value = "备班"
                        ElseIf rdr.CSDriverList(0).DriverNo.Contains("备车") Then
                            Me.DataGridView1.Rows(count).Cells(3).Value = "备车"
                        Else
                            Me.DataGridView1.Rows(count).Cells(3).Value = "无任务"
                        End If
                    End If
                    Me.DataGridView1.Rows(count).Cells(Me.DataGridView1.ColumnCount - 1).Value = Coordination2.Global.BeTime2(rdr.CSDriverList(0).TotalWorkTime)
                    count += 1
                Next
            Next

        ElseIf Me.TreeView1.SelectedNode.Text = "司机阶段行驶公里统计" Then

            If spandrivelength.StartReportDate = Nothing Or spandrivelength.EndReportDate = Nothing Then
                MessageBox.Show("请选择统计日期！")
                Return
            End If
            If spandrivelength.StartReportDate > spandrivelength.EndReportDate Then
                MessageBox.Show("日期选择有误！")
                Return
            End If
            spandrivelength.LoadData()
            If Me.DriverTeams(0)(0).CSDriverList.Count = 0 Then
                MsgBox("没有数据!")
                Exit Sub
            End If

            Me.DataGridView1.Columns.Clear()
            Me.DataGridView1.Rows.Clear()
            Me.DataGridView1.Columns.Add("线路编号", "线路编号")
            Me.DataGridView1.Columns.Add("司机编号", "司机编号")
            Me.DataGridView1.Columns.Add("司机姓名", "司机姓名")
            For i As Integer = 0 To spandrivelength.DateSpan.Days - 1
                Me.DataGridView1.Columns.Add(spandrivelength.StartReportDate.AddDays(i).ToString("yyyy/MM/dd"), spandrivelength.StartReportDate.AddDays(i).ToString("yyyy/MM/dd"))
            Next
            Me.DataGridView1.Columns.Add("阶段总公里", "阶段总公里")
            Me.DataGridView1.Columns.Add("公里补贴", "公里补贴")
            Dim count As Integer = 0
            For Each team As List(Of RDriver) In spandrivelength.TeamsDrivers
                For Each rdr As RDriver In team
                    Me.DataGridView1.Rows.Add(rdr.LineId, rdr.RDriverNo, rdr.DriverName)
                    For i As Integer = 0 To rdr.CSDriverList.Count - 1
                        Me.DataGridView1.Rows(count).Cells(3 + i).Value = rdr.CSDriverList(i).TotalDriverDistance
                    Next

                    Me.DataGridView1.Rows(count).Cells(Me.DataGridView1.ColumnCount - 2).Value = rdr.ToTalDriveDistance
                    Me.DataGridView1.Rows(count).Cells(Me.DataGridView1.ColumnCount - 1).Value = rdr.ToTalDriveDistance / 10
                    count += 1
                Next
            Next

        ElseIf Me.TreeView1.SelectedNode.Text = "司机阶段行驶工时统计" Then

            If spandrivetime.StartReportDate = Nothing Or spandrivetime.EndReportDate = Nothing Then
                MessageBox.Show("请选择统计日期！")
                Return
            End If

            If spandrivetime.StartReportDate > spandrivetime.EndReportDate Then
                MessageBox.Show("日期选择有误！")
                Return
            End If

            spandrivetime.LoadData()
            If Me.DriverTeams(0)(0).CSDriverList.Count = 0 Then
                MsgBox("没有数据!")
                Exit Sub
            End If

            Me.DataGridView1.Columns.Clear()
            Me.DataGridView1.Rows.Clear()
            Me.DataGridView1.Columns.Add("线路编号", "线路编号")
            Me.DataGridView1.Columns.Add("司机编号", "司机编号")
            Me.DataGridView1.Columns.Add("司机姓名", "司机姓名")
            For i As Integer = 0 To spandrivetime.DateSpan.Days - 1
                Me.DataGridView1.Columns.Add(spandrivetime.StartReportDate.AddDays(i).ToString("yyyy/MM/dd"), spandrivetime.StartReportDate.AddDays(i).ToString("yyyy/MM/dd"))
            Next
            Me.DataGridView1.Columns.Add("阶段总工时", "阶段总工时")
            Dim count As Integer = 0
            For Each team As List(Of RDriver) In spandrivetime.TeamsDrivers
                For Each rdr As RDriver In team
                    Me.DataGridView1.Rows.Add(rdr.LineId, rdr.RDriverNo, rdr.DriverName)
                    For i As Integer = 0 To rdr.CSDriverList.Count - 1
                        Me.DataGridView1.Rows(count).Cells(3 + i).Value = Coordination2.Global.BeTime2(rdr.CSDriverList(i).TotalWorkTime)
                    Next

                    Me.DataGridView1.Rows(count).Cells(Me.DataGridView1.ColumnCount - 1).Value = Coordination2.Global.BeTime2(rdr.TotalWorkTime)
                    count += 1
                Next
            Next

        ElseIf Me.TreeView1.SelectedNode.Text = "车底日走行公里统计" Then

            If daytrainlength.ReportDate = Nothing Then
                MessageBox.Show("请选择统计日期！")
                Return
            End If

            daytrainlength.LoadData()
            If daytrainlength.table Is Nothing OrElse daytrainlength.table.Rows.Count = 0 Then
                MsgBox("没有数据!")
                Exit Sub
            End If

            Me.DataGridView1.Columns.Clear()
            Me.DataGridView1.Rows.Clear()

            Me.DataGridView1.Columns.Add(daytrainlength.table.Columns(0).ColumnName, daytrainlength.table.Columns(0).ColumnName)

            Dim count As Integer = 0
            Dim maxs As Integer = 0
            Dim sumlist As New List(Of Decimal)
            Dim tempsum As Decimal = 0
            For i As Integer = 0 To daytrainlength.table.Rows.Count - 1
                If i = 0 Then
                    count += 1
                    Dim tempcolumnname As String = "任务" & count.ToString
                    Me.DataGridView1.Columns.Add(tempcolumnname, tempcolumnname)
                    Me.DataGridView1.Rows.Add(daytrainlength.table.Rows(i).Item("列车号"), daytrainlength.table.Rows(i).Item("运营公里"))
                    tempsum += CDec(daytrainlength.table.Rows(i).Item("运营公里"))
                Else
                    If daytrainlength.table.Rows(i).Item("列车号").ToString = daytrainlength.table.Rows(i - 1).Item("列车号").ToString Then
                        If count >= maxs Then
                            count += 1
                            Dim tempcolumnname As String = "任务" & count.ToString
                            Me.DataGridView1.Columns.Add(tempcolumnname, tempcolumnname)
                            Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 2).Cells(tempcolumnname).Value = CDec(daytrainlength.table.Rows(i).Item("运营公里"))
                            tempsum += CDec(daytrainlength.table.Rows(i).Item("运营公里"))
                        Else
                            count += 1
                            Dim tempcolumnname As String = "任务" & count.ToString
                            Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 2).Cells(tempcolumnname).Value = CDec(daytrainlength.table.Rows(i).Item("运营公里"))
                            tempsum += CDec(daytrainlength.table.Rows(i).Item("运营公里"))
                        End If
                    Else
                        If count > maxs Then
                            maxs = count
                        End If
                        count = 1
                        sumlist.Add(tempsum)
                        tempsum = 0
                        Me.DataGridView1.Rows.Add(daytrainlength.table.Rows(i).Item("列车号"), daytrainlength.table.Rows(i).Item("运营公里"))
                        tempsum += CDec(daytrainlength.table.Rows(i).Item("运营公里"))
                    End If
                End If
            Next
            sumlist.Add(tempsum)
            Me.DataGridView1.Columns.Add("日运营公里", "日运营公里")
            For i As Integer = 0 To sumlist.Count - 1
                Me.DataGridView1.Rows(i).Cells("日运营公里").Value = sumlist(i).ToString
            Next
            daytrainlength.table.Dispose()

        ElseIf Me.TreeView1.SelectedNode.Text = "车底日走行时间统计" Then

            If daytraintime.ReportDate = Nothing Then
                MessageBox.Show("请选择统计日期！")
                Return
            End If

            daytraintime.LoadData()
            If daytraintime.table Is Nothing OrElse daytraintime.table.Rows.Count = 0 Then
                MsgBox("没有数据!")
                Exit Sub
            End If

            Me.DataGridView1.Columns.Clear()
            Me.DataGridView1.Rows.Clear()

            Me.DataGridView1.Columns.Add(daytraintime.table.Columns(0).ColumnName, daytraintime.table.Columns(0).ColumnName)

            Dim count As Integer = 0
            Dim maxs As Integer = 0
            Dim sumlist As New List(Of Decimal)
            Dim tempsum As Decimal = 0
            For i As Integer = 0 To daytraintime.table.Rows.Count - 1
                If i = 0 Then
                    count += 1
                    Dim tempcolumnname As String = "任务" & count.ToString
                    Me.DataGridView1.Columns.Add(tempcolumnname, tempcolumnname)
                    Me.DataGridView1.Rows.Add(daytraintime.table.Rows(i).Item("列车号"), Coordination2.Global.BeTime2(CInt(daytraintime.table.Rows(i).Item("运营时间"))))
                    tempsum += CInt(daytraintime.table.Rows(i).Item("运营时间"))
                Else
                    If daytraintime.table.Rows(i).Item("列车号").ToString = daytraintime.table.Rows(i - 1).Item("列车号").ToString Then
                        If count >= maxs Then
                            count += 1
                            Dim tempcolumnname As String = "任务" & count.ToString
                            Me.DataGridView1.Columns.Add(tempcolumnname, tempcolumnname)
                            Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 2).Cells(tempcolumnname).Value = Coordination2.Global.BeTime2(CInt(daytraintime.table.Rows(i).Item("运营时间")))
                            tempsum += CInt(daytraintime.table.Rows(i).Item("运营时间"))
                        Else
                            count += 1
                            Dim tempcolumnname As String = "任务" & count.ToString
                            Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 2).Cells(tempcolumnname).Value = Coordination2.Global.BeTime2(CInt(daytraintime.table.Rows(i).Item("运营时间")))
                            tempsum += CInt(daytraintime.table.Rows(i).Item("运营时间"))
                        End If
                    Else
                        If count > maxs Then
                            maxs = count
                        End If
                        count = 1
                        sumlist.Add(tempsum)
                        tempsum = 0
                        Me.DataGridView1.Rows.Add(daytraintime.table.Rows(i).Item("列车号"), Coordination2.Global.BeTime2(CInt(daytraintime.table.Rows(i).Item("运营时间"))))
                        tempsum += CInt(daytraintime.table.Rows(i).Item("运营时间"))
                    End If
                End If
            Next
            sumlist.Add(tempsum)
            Me.DataGridView1.Columns.Add("日运营时间", "日运营时间")
            For i As Integer = 0 To sumlist.Count - 1
                Me.DataGridView1.Rows(i).Cells("日运营时间").Value = Coordination2.Global.BeTime2(sumlist(i))
            Next
            daytraintime.table.Dispose()

        ElseIf Me.TreeView1.SelectedNode.Text = "车底阶段走行公里统计" Then

            If spantrainlength.StartReportDate = Nothing Or spantrainlength.EndReportDate = Nothing Then
                MessageBox.Show("请选择统计日期！")
                Return
            End If

            If spantrainlength.StartReportDate > spantrainlength.EndReportDate Then
                MessageBox.Show("日期选择有误！")
                Return
            End If

            spantrainlength.LoadData()
            If spantrainlength.table Is Nothing OrElse spantrainlength.table.Rows.Count = 0 Then
                MsgBox("没有数据!")
                Exit Sub
            End If

            Me.DataGridView1.Columns.Clear()
            Me.DataGridView1.Rows.Clear()

            Me.DataGridView1.Columns.Add(spantrainlength.table.Columns(0).ColumnName, spantrainlength.table.Columns(0).ColumnName)
            For i As Integer = 0 To spantrainlength.DateSpan.Days - 1
                Me.DataGridView1.Columns.Add(spantrainlength.StartReportDate.AddDays(i).ToString("yyyy/MM/dd"), spantrainlength.StartReportDate.AddDays(i).ToString("yyyy/MM/dd"))
            Next
            Me.DataGridView1.Columns.Add("总运营公里", "总运营公里")

            Dim tempsum As Decimal = 0
            For i As Integer = 0 To spantrainlength.table.Rows.Count - 1
                If i = 0 Then
                    Me.DataGridView1.Rows.Add(spantrainlength.table.Rows(i).Item("列车号").ToString)
                    Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 2).Cells(CDate(spantrainlength.table.Rows(i).Item("日期")).ToString("yyyy/MM/dd")).Value = spantrainlength.table.Rows(i).Item("运营公里").ToString
                    tempsum += CDec(spantrainlength.table.Rows(i).Item("运营公里"))
                Else
                    If spantrainlength.table.Rows(i).Item("列车号").ToString = spantrainlength.table.Rows(i - 1).Item("列车号").ToString Then
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 2).Cells(CDate(spantrainlength.table.Rows(i).Item("日期")).ToString("yyyy/MM/dd")).Value = spantrainlength.table.Rows(i).Item("运营公里").ToString
                        tempsum += CDec(spantrainlength.table.Rows(i).Item("运营公里"))
                    Else
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 2).Cells("总运营公里").Value = tempsum.ToString
                        tempsum = 0
                        Me.DataGridView1.Rows.Add(spantrainlength.table.Rows(i).Item("列车号").ToString)
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 2).Cells(CDate(spantrainlength.table.Rows(i).Item("日期")).ToString("yyyy/MM/dd")).Value = spantrainlength.table.Rows(i).Item("运营公里").ToString
                        tempsum += CDec(spantrainlength.table.Rows(i).Item("运营公里"))
                    End If
                End If
            Next
            Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 2).Cells("总运营公里").Value = tempsum.ToString

            For i As Integer = 0 To Me.DataGridView1.Rows.Count - 2
                For j As Integer = 0 To Me.DataGridView1.Columns.Count - 1
                    If Me.DataGridView1.Rows(i).Cells(j).Value = "" Then
                        Me.DataGridView1.Rows(i).Cells(j).Value = (0).ToString
                    End If
                Next
            Next

            spantrainlength.table.Dispose()

        ElseIf Me.TreeView1.SelectedNode.Text = "车底阶段走行时间统计" Then

            If spantraintime.StartReportDate = Nothing Or spantraintime.EndReportDate = Nothing Then
                MessageBox.Show("请选择统计日期！")
                Return
            End If

            If spantraintime.StartReportDate > spantraintime.EndReportDate Then
                MessageBox.Show("日期选择有误！")
                Return
            End If

            spantraintime.LoadData()
            If spantraintime.table Is Nothing OrElse spantraintime.table.Rows.Count = 0 Then
                MsgBox("没有数据!")
                Exit Sub
            End If

            Me.DataGridView1.Columns.Clear()
            Me.DataGridView1.Rows.Clear()

            Me.DataGridView1.Columns.Add(spantraintime.table.Columns(0).ColumnName, spantraintime.table.Columns(0).ColumnName)
            For i As Integer = 0 To spantraintime.DateSpan.Days - 1
                Me.DataGridView1.Columns.Add(spantraintime.StartReportDate.AddDays(i).ToString("yyyy/MM/dd"), spantraintime.StartReportDate.AddDays(i).ToString("yyyy/MM/dd"))
            Next
            Me.DataGridView1.Columns.Add("总运营时间", "总运营时间")

            Dim tempsum As Decimal = 0
            For i As Integer = 0 To spantraintime.table.Rows.Count - 1
                If i = 0 Then
                    Me.DataGridView1.Rows.Add(spantraintime.table.Rows(i).Item("列车号").ToString)
                    Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 2).Cells(CDate(spantraintime.table.Rows(i).Item("日期")).ToString("yyyy/MM/dd")).Value = Coordination2.Global.BeTime2(CInt(spantraintime.table.Rows(i).Item("运营时间")))
                    tempsum += CDec(spantraintime.table.Rows(i).Item("运营时间"))
                Else
                    If spantraintime.table.Rows(i).Item("列车号").ToString = spantraintime.table.Rows(i - 1).Item("列车号").ToString Then
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 2).Cells(CDate(spantraintime.table.Rows(i).Item("日期")).ToString("yyyy/MM/dd")).Value = Coordination2.Global.BeTime2(CInt(spantraintime.table.Rows(i).Item("运营时间")))
                        tempsum += CDec(spantraintime.table.Rows(i).Item("运营时间"))
                    Else
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 2).Cells("总运营时间").Value = Coordination2.Global.BeTime2(tempsum)
                        tempsum = 0
                        Me.DataGridView1.Rows.Add(spantraintime.table.Rows(i).Item("列车号").ToString)
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 2).Cells(CDate(spantraintime.table.Rows(i).Item("日期")).ToString("yyyy/MM/dd")).Value = Coordination2.Global.BeTime2(CInt(spantraintime.table.Rows(i).Item("运营时间")))
                        tempsum += CDec(spantraintime.table.Rows(i).Item("运营时间"))
                    End If
                End If
            Next
            Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 2).Cells("总运营时间").Value = Coordination2.Global.BeTime2(tempsum)

            For i As Integer = 0 To Me.DataGridView1.Rows.Count - 2
                For j As Integer = 0 To Me.DataGridView1.Columns.Count - 1
                    If Me.DataGridView1.Rows(i).Cells(j).Value = "" Then
                        Me.DataGridView1.Rows(i).Cells(j).Value = (0).ToString
                    End If
                Next
            Next

            spantraintime.table.Dispose()
        End If
        Me.DataGridView1.AutoResizeColumns()
    End Sub

    Private Sub 执行ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 执行ToolStripMenuItem.Click
        Call ToolStripButton1_Click(Nothing, Nothing)
    End Sub

    Private Sub 输出ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 输出ToolStripMenuItem.Click

        Dim exe As Microsoft.Office.Interop.Excel.Application
        Dim workbooks As Microsoft.Office.Interop.Excel.Workbooks
        Dim workbook As Microsoft.Office.Interop.Excel.Workbook
        Dim worksheet As Microsoft.Office.Interop.Excel.Worksheet
        Try
            exe = New Microsoft.Office.Interop.Excel.Application()
            If exe Is Nothing Then
                MessageBox.Show("创建excel文件失败！")
            End If
            workbooks = exe.Workbooks
            workbook = workbooks.Add
            worksheet = workbook.Sheets(1)
        Catch ex As Exception
            MessageBox.Show("启动EXCEL服务失败！")
            For Each thisproc As System.Diagnostics.Process In System.Diagnostics.Process.GetProcesses()
                If thisproc.ProcessName = "EXCEL" Then
                    thisproc.Kill()
                End If
            Next
            Return
        End Try

        If Me.TreeView1.SelectedNode.Text = "司机日行驶公里统计" Then

            If daydrivelength.ReportDate = Nothing Then
                MessageBox.Show("请选择统计日期！")
                Return
            End If

            daydrivelength.LoadData()
            If Me.DriverTeams(0)(0).CSDriverList.Count = 0 Then
                MsgBox("没有数据!")
                Exit Sub
            End If

            daydrivelength.DrawFrame()
            daydrivelength.DrawData()
            daydrivelength.DataGrid.DrawTotaltoExcel(worksheet)

            exe.Visible = True
            worksheet.Columns.AutoFit()

        ElseIf Me.TreeView1.SelectedNode.Text = "司机日行驶工时统计" Then

            If daydrivetime.ReportDate = Nothing Then
                MessageBox.Show("请选择统计日期！")
                Return
            End If

            daydrivetime.LoadData()
            If Me.DriverTeams(0)(0).CSDriverList.Count = 0 Then
                MsgBox("没有数据!")
                Exit Sub
            End If

            daydrivetime.DrawFrame()
            daydrivetime.DrawData()
            daydrivetime.DataGrid.DrawTotaltoExcel(worksheet)

            exe.Visible = True
            worksheet.Columns.AutoFit()

        ElseIf Me.TreeView1.SelectedNode.Text = "司机阶段行驶公里统计" Then

            If spandrivelength.StartReportDate = Nothing Or spandrivelength.EndReportDate = Nothing Then
                MessageBox.Show("请选择统计日期！")
                Return
            End If

            If spandrivelength.StartReportDate > spandrivelength.EndReportDate Then
                MessageBox.Show("日期选择有误！")
                Return
            End If

            spandrivelength.LoadData()
            If Me.DriverTeams(0)(0).CSDriverList.Count = 0 Then
                MsgBox("没有数据!")
                Exit Sub
            End If

            spandrivelength.DrawFrame()
            spandrivelength.DrawData()
            spandrivelength.DataGrid.DrawTotaltoExcel(worksheet)

            exe.Visible = True
            worksheet.Columns.AutoFit()

        ElseIf Me.TreeView1.SelectedNode.Text = "司机阶段行驶工时统计" Then

            If spandrivetime.StartReportDate = Nothing Or spandrivetime.EndReportDate = Nothing Then
                MessageBox.Show("请选择统计日期！")
                Return
            End If

            If spandrivetime.StartReportDate > spandrivetime.EndReportDate Then
                MessageBox.Show("日期选择有误！")
                Return
            End If

            spandrivetime.LoadData()
            If Me.DriverTeams(0)(0).CSDriverList.Count = 0 Then
                MsgBox("没有数据!")
                Exit Sub
            End If
            spandrivetime.DrawFrame()
            spandrivetime.DrawData()
            spandrivetime.DataGrid.DrawTotaltoExcel(worksheet)

            exe.Visible = True
            worksheet.Columns.AutoFit()

        ElseIf Me.TreeView1.SelectedNode.Text = "车底日走行公里统计" Then

            If daytrainlength.ReportDate = Nothing Then
                MessageBox.Show("请选择统计日期！")
                Return
            End If

            daytrainlength.LoadData()
            If daytrainlength.table.Rows.Count = 0 Then
                MsgBox("没有数据!")
                Exit Sub
            End If

            daytrainlength.DrawFrame()
            daytrainlength.DrawData()
            daytrainlength.DataGrid.DrawTotaltoExcel(worksheet)

            exe.Visible = True
            worksheet.Columns.AutoFit()

        ElseIf Me.TreeView1.SelectedNode.Text = "车底日走行时间统计" Then

            If daytraintime.ReportDate = Nothing Then
                MessageBox.Show("请选择统计日期！")
                Return
            End If

            daytraintime.LoadData()
            If daytraintime.table.Rows.Count = 0 Then
                MsgBox("没有数据!")
                Exit Sub
            End If

            daytraintime.DrawFrame()
            daytraintime.DrawData()
            daytraintime.DataGrid.DrawTotaltoExcel(worksheet)

            exe.Visible = True
            worksheet.Columns.AutoFit()

        ElseIf Me.TreeView1.SelectedNode.Text = "车底阶段走行公里统计" Then

            If spantrainlength.StartReportDate = Nothing Or spantrainlength.EndReportDate = Nothing Then
                MessageBox.Show("请选择统计日期！")
                Return
            End If

            If spantrainlength.StartReportDate > spantrainlength.EndReportDate Then
                MessageBox.Show("日期选择有误！")
                Return
            End If

            spantrainlength.LoadData()
            If spantrainlength.table.Rows.Count = 0 Then
                MsgBox("没有数据!")
                Exit Sub
            End If

            spantrainlength.DrawFrame()
            spantrainlength.DrawData()
            spantrainlength.DataGrid.DrawTotaltoExcel(worksheet)

            exe.Visible = True
            worksheet.Columns.AutoFit()

        ElseIf Me.TreeView1.SelectedNode.Text = "车底阶段走行时间统计" Then

            If spantraintime.StartReportDate = Nothing Or spantraintime.EndReportDate = Nothing Then
                MessageBox.Show("请选择统计日期！")
                Return
            End If

            If spantraintime.StartReportDate > spantraintime.EndReportDate Then
                MessageBox.Show("日期选择有误！")
                Return
            End If

            spantraintime.LoadData()
            If spantraintime.table.Rows.Count = 0 Then
                MsgBox("没有数据!")
                Exit Sub
            End If

            spantraintime.DrawFrame()
            spantraintime.DrawData()
            spantraintime.DataGrid.DrawTotaltoExcel(worksheet)

            exe.Visible = True
            worksheet.Columns.AutoFit()

        End If

    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Call 输出ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Public Sub Load_Drivers()     '加载司机信息
        Dim Str As String
        If CurLineName = "" Then
            Str = "Select lineid,Beclass,Beteam,RDriverNo,DriverName from cs_driverinf where beteam is not null order by Beteam,RDriverNo"
        Else
            Str = "Select lineid, Beclass,Beteam,RDriverNo,DriverName from cs_driverinf where lineid='" & CurLineName & "'and beteam is not null order by Beteam,RDriverNo"
        End If
        Dim tab As DataTable = Globle.Method.ReadDataForAccess(Str)

        Me.TVDrivers.Nodes.Clear()
        Me.DriverTeams.Clear()
        Dim tempTeam As New List(Of RDriver)
        Me.TVDrivers.Nodes.Add("个人指标分析", "个人指标分析")
        For i As Integer = 0 To tab.Rows.Count - 1
            If tab.Rows(i).Item("Beteam").ToString = "其它" Then
                Exit For
            Else
                If i = 0 Then
                    Me.TVDrivers.Nodes("个人指标分析").Nodes.Add(tab.Rows(i).Item("lineid").ToString, tab.Rows(i).Item("lineid").ToString)
                    Me.TVDrivers.Nodes("个人指标分析").Nodes(tab.Rows(i).Item("lineid").ToString).Nodes.Add(tab.Rows(i).Item("Beclass").ToString, tab.Rows(i).Item("Beclass").ToString)
                    Me.TVDrivers.Nodes("个人指标分析").Nodes(tab.Rows(i).Item("lineid").ToString).Nodes(tab.Rows(i).Item("Beclass").ToString).Nodes.Add(tab.Rows(i).Item("Beteam").ToString, tab.Rows(i).Item("Beteam").ToString)
                    Me.TVDrivers.Nodes("个人指标分析").Nodes(tab.Rows(i).Item("lineid").ToString).Nodes(tab.Rows(i).Item("Beclass").ToString).Nodes(tab.Rows(i).Item("Beteam").ToString).Nodes.Add(tab.Rows(i).Item("DriverName").ToString, tab.Rows(i).Item("DriverName").ToString)
                    tempTeam.Add(New RDriver(tab.Rows(i).Item("lineid").ToString, tab.Rows(i).Item("Beclass").ToString, tab.Rows(i).Item("Beteam").ToString, tab.Rows(i).Item("RdriverNo").ToString, tab.Rows(i).Item("DriverName").ToString))
                Else
                    If tab.Rows(i).Item("lineid").ToString = tab.Rows(i - 1).Item("lineid").ToString Then
                        If tab.Rows(i).Item("beclass").ToString = tab.Rows(i - 1).Item("beclass").ToString Then
                            If tab.Rows(i).Item("beteam").ToString = tab.Rows(i - 1).Item("beteam").ToString Then
                                Me.TVDrivers.Nodes("个人指标分析").Nodes(tab.Rows(i).Item("lineid").ToString).Nodes(tab.Rows(i).Item("Beclass").ToString).Nodes(tab.Rows(i).Item("Beteam").ToString).Nodes.Add(tab.Rows(i).Item("DriverName").ToString, tab.Rows(i).Item("DriverName").ToString)
                            Else
                                Me.TVDrivers.Nodes("个人指标分析").Nodes(tab.Rows(i).Item("lineid").ToString).Nodes(tab.Rows(i).Item("Beclass").ToString).Nodes.Add(tab.Rows(i).Item("Beteam").ToString, tab.Rows(i).Item("Beteam").ToString)
                                Me.TVDrivers.Nodes("个人指标分析").Nodes(tab.Rows(i).Item("lineid").ToString).Nodes(tab.Rows(i).Item("Beclass").ToString).Nodes(tab.Rows(i).Item("Beteam").ToString).Nodes.Add(tab.Rows(i).Item("DriverName").ToString, tab.Rows(i).Item("DriverName").ToString)
                                tempTeam.Add(New RDriver(tab.Rows(i).Item("lineid").ToString, tab.Rows(i).Item("Beclass").ToString, tab.Rows(i).Item("Beteam").ToString, tab.Rows(i).Item("RdriverNo").ToString, tab.Rows(i).Item("DriverName").ToString))
                            End If
                        Else
                            Me.TVDrivers.Nodes("个人指标分析").Nodes(tab.Rows(i).Item("lineid").ToString).Nodes.Add(tab.Rows(i).Item("Beclass").ToString, tab.Rows(i).Item("Beclass").ToString)
                            Me.TVDrivers.Nodes("个人指标分析").Nodes(tab.Rows(i).Item("lineid").ToString).Nodes(tab.Rows(i).Item("Beclass").ToString).Nodes.Add(tab.Rows(i).Item("Beteam").ToString, tab.Rows(i).Item("Beteam").ToString)
                            Me.TVDrivers.Nodes("个人指标分析").Nodes(tab.Rows(i).Item("lineid").ToString).Nodes(tab.Rows(i).Item("Beclass").ToString).Nodes(tab.Rows(i).Item("Beteam").ToString).Nodes.Add(tab.Rows(i).Item("DriverName").ToString, tab.Rows(i).Item("DriverName").ToString)
                            tempTeam.Add(New RDriver(tab.Rows(i).Item("lineid").ToString, tab.Rows(i).Item("Beclass").ToString, tab.Rows(i).Item("Beteam").ToString, tab.Rows(i).Item("RdriverNo").ToString, tab.Rows(i).Item("DriverName").ToString))
                        End If
                    Else
                        Me.DriverTeams.Add(tempTeam)
                        tempTeam = New List(Of RDriver)
                        Me.TVDrivers.Nodes("个人指标分析").Nodes.Add(tab.Rows(i).Item("lineid").ToString, tab.Rows(i).Item("lineid").ToString)
                        Me.TVDrivers.Nodes("个人指标分析").Nodes(tab.Rows(i).Item("lineid").ToString).Nodes.Add(tab.Rows(i).Item("Beclass").ToString, tab.Rows(i).Item("Beclass").ToString)
                        Me.TVDrivers.Nodes("个人指标分析").Nodes(tab.Rows(i).Item("lineid").ToString).Nodes(tab.Rows(i).Item("Beclass").ToString).Nodes.Add(tab.Rows(i).Item("Beteam").ToString, tab.Rows(i).Item("Beteam").ToString)
                        Me.TVDrivers.Nodes("个人指标分析").Nodes(tab.Rows(i).Item("lineid").ToString).Nodes(tab.Rows(i).Item("Beclass").ToString).Nodes(tab.Rows(i).Item("Beteam").ToString).Nodes.Add(tab.Rows(i).Item("DriverName").ToString, tab.Rows(i).Item("DriverName").ToString)
                        tempTeam.Add(New RDriver(tab.Rows(i).Item("lineid").ToString, tab.Rows(i).Item("Beclass").ToString, tab.Rows(i).Item("Beteam").ToString, tab.Rows(i).Item("RdriverNo").ToString, tab.Rows(i).Item("DriverName").ToString))
                    End If
                End If
            End If
        Next
        Me.DriverTeams.Add(tempTeam)

        Me.TVDrivers.Nodes.Add("总体指标分析", "总体指标分析")
        For i As Integer = 0 To tab.Rows.Count - 1
            If i = 0 Then
                Me.TVDrivers.Nodes("总体指标分析").Nodes.Add(tab.Rows(i).Item("lineid").ToString, tab.Rows(i).Item("lineid").ToString)
                Me.TVDrivers.Nodes("总体指标分析").Nodes(tab.Rows(i).Item("lineid").ToString).Nodes.Add(tab.Rows(i).Item("Beclass").ToString, tab.Rows(i).Item("Beclass").ToString)
            Else
                If tab.Rows(i).Item("lineid").ToString = tab.Rows(i - 1).Item("lineid").ToString Then
                    If tab.Rows(i).Item("beclass").ToString <> tab.Rows(i - 1).Item("beclass").ToString Then
                        Me.TVDrivers.Nodes("总体指标分析").Nodes(tab.Rows(i).Item("lineid").ToString).Nodes.Add(tab.Rows(i).Item("Beclass").ToString, tab.Rows(i).Item("Beclass").ToString)
                    End If
                Else
                    Me.TVDrivers.Nodes("总体指标分析").Nodes.Add(tab.Rows(i).Item("lineid").ToString, tab.Rows(i).Item("lineid").ToString)
                    Me.TVDrivers.Nodes("总体指标分析").Nodes(tab.Rows(i).Item("lineid").ToString).Nodes.Add(tab.Rows(i).Item("Beclass").ToString, tab.Rows(i).Item("Beclass").ToString)
                End If
            End If
        Next
        Me.TVDrivers.Nodes("总体指标分析").Nodes.Add("总体", "总体")
        tab.Dispose()
    End Sub

    Private Sub TVDrivers_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TVDrivers.AfterSelect

        If Me.TreeView1.SelectedNode.Text = "司机日行驶公里统计" Then
            If e.Node.Level = 2 AndAlso e.Node.Parent.Parent.Name = "个人指标分析" Then
                Dim selectRdriver As RDriver
                selectRdriver = DriverTeams(e.Node.Parent.Index)(e.Node.Index)

                Dim series As New Series("日行驶公里统计")
                series.ChartType = SeriesChartType.Column
                series.BorderWidth = 3
                series.ShadowOffset = 2
                If selectRdriver.CSDriverList.Count > 0 Then
                    If selectRdriver.CSDriverList(0).DutySort = "休息" Then
                        Me.DataChart.Series.Clear()
                        Me.DataChart.Titles.Clear()
                        Me.DataChart.ChartAreas.Clear()
                        Me.DataChart.Legends.Clear()
                        Me.DataChart.Titles.Add(selectRdriver.DriverName & "该天休息")
                        Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                        Me.Fuzhi(0, 0, 0, 0, 0, 0, 1)
                    Else
                        If selectRdriver.CSDriverList(0).LinkDutyList.Count = 0 Then
                            If selectRdriver.CSDriverList(0).DriverNo.Contains("备") Then
                                Me.DataChart.Series.Clear()
                                Me.DataChart.Titles.Clear()
                                Me.DataChart.ChartAreas.Clear()
                                Me.DataChart.Legends.Clear()
                                Me.DataChart.Titles.Add(selectRdriver.DriverName & "该天任务为:" & selectRdriver.CSDriverList(0).DriverNo)
                                Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                                Me.Fuzhi(0, 0, 0, 0, 0, 0, 1)
                            Else
                                Me.DataChart.Series.Clear()
                                Me.DataChart.Titles.Clear()
                                Me.DataChart.ChartAreas.Clear()
                                Me.DataChart.Legends.Clear()
                                Me.DataChart.Titles.Add(selectRdriver.DriverName & "该天无任务")
                                Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                                Me.Fuzhi(0, 0, 0, 0, 0, 0, 1)
                            End If
                        Else
                            For Each link As LinkDuty In selectRdriver.CSDriverList(0).LinkDutyList
                                series.Points.AddXY(link.startstaname & "-" & link.endstaname, link.DriveDistance)
                            Next
                            Me.DataChart.Series.Clear()
                            Me.DataChart.Titles.Clear()
                            Me.DataChart.ChartAreas.Clear()
                            Me.DataChart.Series.Add(series)
                            Me.DataChart.Titles.Add(selectRdriver.DriverName & "日行驶公里统计")
                            Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                            Me.DataChart.ChartAreas.Add("公里")
                            Me.DataChart.ChartAreas(0).AxisY.Title = "公里(km)"
                            Me.DataChart.ChartAreas(0).AxisY.TitleFont = New Font("Times New Roman", 15, FontStyle.Regular)
                            Me.DataChart.ChartAreas("公里").AxisX.Interval = 1

                            Dim temp As New List(Of Decimal)
                            For Each link As LinkDuty In selectRdriver.CSDriverList(0).LinkDutyList
                                temp.Add(link.DriveDistance)
                            Next
                            Me.Fuzhi(GlobalFunc.GetMaxValue(temp), GlobalFunc.GetMinValue(temp), GlobalFunc.GetAveValue(temp), GlobalFunc.GetVarValue(temp), _
                                     selectRdriver.ToTalDriveDistance, selectRdriver.CSDriverList(0).LinkDutyList.Count, 1)
                        End If
                    End If
                Else
                    Me.DataChart.Series.Clear()
                    Me.DataChart.Titles.Clear()
                    Me.DataChart.ChartAreas.Clear()
                    Me.DataChart.Legends.Clear()
                    MsgBox("未加载或没有数据!")
                    Exit Sub
                End If
            ElseIf e.Node.Level = 1 AndAlso e.Node.Parent.Text = "总体指标分析" Then
                Dim series As New Series("日行驶公里统计")
                If e.Node.Text = "总体" Then
                    For Each team As List(Of RDriver) In DriverTeams
                        For Each rdr As RDriver In team
                            series.Points.AddXY(rdr.DriverName, rdr.ToTalDriveDistance)
                        Next
                    Next
                    Me.DataChart.Series.Clear()
                    Me.DataChart.Titles.Clear()
                    Me.DataChart.ChartAreas.Clear()
                    Me.DataChart.Series.Add(series)
                    Me.DataChart.Titles.Add(e.Node.Text & "日行驶公里统计")
                    Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                    Me.DataChart.ChartAreas.Add("公里")
                    Me.DataChart.ChartAreas(0).AxisY.Title = "公里(km)"
                    Me.DataChart.ChartAreas(0).AxisY.TitleFont = New Font("Times New Roman", 15, FontStyle.Regular)
                    Me.DataChart.ChartAreas("公里").AxisX.Interval = 1

                    Dim temp As New List(Of Decimal)
                    Dim DriverNum As Integer = 0
                    Dim DutyNum As Integer = 0
                    For Each team As List(Of RDriver) In DriverTeams
                        For Each rdr As RDriver In team
                            temp.Add(rdr.ToTalDriveDistance)
                            DriverNum += 1
                            DutyNum += rdr.CSDriverList(0).LinkDutyList.Count
                        Next
                    Next
                    Me.Fuzhi(GlobalFunc.GetMaxValue(temp), GlobalFunc.GetMinValue(temp), GlobalFunc.GetAveValue(temp), GlobalFunc.GetVarValue(temp), _
                             GlobalFunc.GetSumValue(temp), DutyNum, DriverNum)
                Else
                    Dim selectteam As List(Of RDriver)
                    selectteam = DriverTeams(e.Node.Index)
                    For Each rdr As RDriver In selectteam       '给图赋值
                        series.Points.AddXY(rdr.DriverName, rdr.ToTalDriveDistance)
                    Next
                    Me.DataChart.Series.Clear()
                    Me.DataChart.Titles.Clear()
                    Me.DataChart.ChartAreas.Clear()
                    Me.DataChart.Series.Add(series)
                    Me.DataChart.Titles.Add(e.Node.Text & "日行驶公里统计")
                    Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                    Me.DataChart.ChartAreas.Add("公里")
                    Me.DataChart.ChartAreas(0).AxisY.Title = "公里(km)"
                    Me.DataChart.ChartAreas(0).AxisY.TitleFont = New Font("Times New Roman", 15, FontStyle.Regular)
                    Me.DataChart.ChartAreas("公里").AxisX.Interval = 1

                    Dim temp As New List(Of Decimal)
                    Dim DriverNum As Integer = 0
                    Dim DutyNum As Integer = 0
                    For Each rdr As RDriver In selectteam
                        temp.Add(rdr.ToTalDriveDistance)
                        DriverNum += 1
                        DutyNum += rdr.CSDriverList(0).LinkDutyList.Count
                    Next
                    Me.Fuzhi(GlobalFunc.GetMaxValue(temp), GlobalFunc.GetMinValue(temp), GlobalFunc.GetAveValue(temp), GlobalFunc.GetVarValue(temp), _
                             GlobalFunc.GetSumValue(temp), DutyNum, DriverNum)
                End If
            End If
        ElseIf Me.TreeView1.SelectedNode.Text = "司机日行驶工时统计" Then
            If e.Node.Level = 2 AndAlso e.Node.Parent.Parent.Name = "个人指标分析" Then
                Dim selectRdriver As RDriver
                selectRdriver = DriverTeams(e.Node.Parent.Index)(e.Node.Index)

                Dim series As New Series("日行驶工时统计")
                series.ChartType = SeriesChartType.Column
                series.BorderWidth = 3
                series.ShadowOffset = 2
                If selectRdriver.CSDriverList.Count > 0 Then
                    If selectRdriver.CSDriverList(0).DutySort = "休息" Then
                        Me.DataChart.Series.Clear()
                        Me.DataChart.Titles.Clear()
                        Me.DataChart.ChartAreas.Clear()
                        Me.DataChart.Legends.Clear()
                        Me.DataChart.Titles.Add(selectRdriver.DriverName & "该天休息")
                        Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                        Me.Fuzhi(0, 0, 0, 0, 0, 0, 1)
                    Else
                        If selectRdriver.CSDriverList(0).LinkDutyList.Count = 0 Then
                            If selectRdriver.CSDriverList(0).DriverNo.Contains("备") Then
                                Me.DataChart.Series.Clear()
                                Me.DataChart.Titles.Clear()
                                Me.DataChart.ChartAreas.Clear()
                                Me.DataChart.Legends.Clear()
                                Me.DataChart.Titles.Add(selectRdriver.DriverName & "该天任务为:" & selectRdriver.CSDriverList(0).DriverNo)
                                Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                                Me.Fuzhi(0, 0, 0, 0, 0, 0, 1)
                            Else
                                Me.DataChart.Series.Clear()
                                Me.DataChart.Titles.Clear()
                                Me.DataChart.ChartAreas.Clear()
                                Me.DataChart.Legends.Clear()
                                Me.DataChart.Titles.Add(selectRdriver.DriverName & "该天无任务")
                                Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                                Me.Fuzhi(0, 0, 0, 0, 0, 0, 1)
                            End If
                        Else
                            For Each link As LinkDuty In selectRdriver.CSDriverList(0).LinkDutyList
                                series.Points.AddXY(link.startstaname & "-" & link.endstaname, Format(link.Drivetime / 3600, "0.00"))
                            Next
                            Me.DataChart.Series.Clear()
                            Me.DataChart.Titles.Clear()
                            Me.DataChart.ChartAreas.Clear()
                            Me.DataChart.Series.Add(series)
                            Me.DataChart.Titles.Add(selectRdriver.DriverName & "日行驶工时统计")
                            Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                            Me.DataChart.ChartAreas.Add("工时")
                            Me.DataChart.ChartAreas(0).AxisY.Title = "工时(h)"
                            Me.DataChart.ChartAreas(0).AxisY.TitleFont = New Font("Times New Roman", 15, FontStyle.Regular)
                            Me.DataChart.ChartAreas("工时").AxisX.Interval = 1
                            Dim temp As New List(Of Decimal)
                            For Each link As LinkDuty In selectRdriver.CSDriverList(0).LinkDutyList
                                temp.Add(Format(link.Drivetime / 3600, "0.00"))
                            Next
                            Me.Fuzhi(GlobalFunc.GetMaxValue(temp), GlobalFunc.GetMinValue(temp), GlobalFunc.GetAveValue(temp), GlobalFunc.GetVarValue(temp), _
                                     GlobalFunc.GetSumValue(temp), selectRdriver.CSDriverList(0).LinkDutyList.Count, 1)
                        End If
                    End If
                Else
                    Me.DataChart.Series.Clear()
                    Me.DataChart.Titles.Clear()
                    Me.DataChart.ChartAreas.Clear()
                    Me.DataChart.Legends.Clear()
                    MsgBox("没有数据!")
                    Exit Sub
                End If
            ElseIf e.Node.Level = 1 AndAlso e.Node.Parent.Text = "总体指标分析" Then
                Dim series As New Series("日行驶工时统计")
                If e.Node.Text = "总体" Then
                    For Each team As List(Of RDriver) In DriverTeams
                        For Each rdr As RDriver In team
                            series.Points.AddXY(rdr.DriverName, Format(rdr.TotalWorkTime / 3600, "0.00"))
                        Next
                    Next
                    Me.DataChart.Series.Clear()
                    Me.DataChart.Titles.Clear()
                    Me.DataChart.ChartAreas.Clear()
                    Me.DataChart.Series.Add(series)
                    Me.DataChart.Titles.Add(e.Node.Text & "日行驶工时统计")
                    Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                    Me.DataChart.ChartAreas.Add("工时")
                    Me.DataChart.ChartAreas(0).AxisY.Title = "工时(h)"
                    Me.DataChart.ChartAreas(0).AxisY.TitleFont = New Font("Times New Roman", 15, FontStyle.Regular)
                    Me.DataChart.ChartAreas("工时").AxisX.Interval = 1
                    Dim temp As New List(Of Decimal)
                    Dim DriverNum As Integer = 0
                    Dim DutyNum As Integer = 0
                    For Each team As List(Of RDriver) In DriverTeams
                        For Each rdr As RDriver In team
                            temp.Add(Format(rdr.TotalWorkTime / 3600, "0.00"))
                            DriverNum += 1
                            DutyNum += rdr.CSDriverList(0).LinkDutyList.Count
                        Next
                    Next
                    Me.Fuzhi(GlobalFunc.GetMaxValue(temp), GlobalFunc.GetMinValue(temp), GlobalFunc.GetAveValue(temp), GlobalFunc.GetVarValue(temp), _
                             GlobalFunc.GetSumValue(temp), DutyNum, DriverNum)
                Else
                    Dim selectteam As List(Of RDriver)
                    selectteam = DriverTeams(e.Node.Index)

                    For Each rdr As RDriver In selectteam       '给图赋值
                        series.Points.AddXY(rdr.DriverName, Format(rdr.TotalWorkTime / 3600, "0.00"))
                    Next
                    Me.DataChart.Series.Clear()
                    Me.DataChart.Titles.Clear()
                    Me.DataChart.ChartAreas.Clear()
                    Me.DataChart.Series.Add(series)
                    Me.DataChart.Titles.Add(e.Node.Text & "日行驶工时统计")
                    Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                    Me.DataChart.ChartAreas.Add("工时")
                    Me.DataChart.ChartAreas(0).AxisY.Title = "工时(km)"
                    Me.DataChart.ChartAreas(0).AxisY.TitleFont = New Font("Times New Roman", 15, FontStyle.Regular)
                    Me.DataChart.ChartAreas("工时").AxisX.Interval = 1
                    Dim temp As New List(Of Decimal)
                    Dim DriverNum As Integer = 0
                    Dim DutyNum As Integer = 0
                    For Each rdr As RDriver In selectteam
                        temp.Add(Format(rdr.TotalWorkTime / 3600, "0.00"))
                        DriverNum += 1
                        DutyNum += rdr.CSDriverList(0).LinkDutyList.Count
                    Next
                    Me.Fuzhi(GlobalFunc.GetMaxValue(temp), GlobalFunc.GetMinValue(temp), GlobalFunc.GetAveValue(temp), GlobalFunc.GetVarValue(temp), _
                             GlobalFunc.GetSumValue(temp), DutyNum, DriverNum)
                End If
            End If
        ElseIf Me.TreeView1.SelectedNode.Text = "司机阶段行驶公里统计" Then
            If e.Node.Level = 2 AndAlso e.Node.Parent.Parent.Name = "个人指标分析" Then
                Dim selectRdriver As RDriver
                selectRdriver = DriverTeams(e.Node.Parent.Index)(e.Node.Index)

                Dim series As New Series("阶段行驶公里统计")
                series.ChartType = SeriesChartType.Column
                series.BorderWidth = 3
                series.ShadowOffset = 2
                If selectRdriver.CSDriverList.Count > 0 Then
                    For i As Integer = 0 To selectRdriver.CSDriverList.Count - 1
                        series.Points.AddXY(Me.spandrivelength.StartReportDate.AddDays(i).ToString("yyyy/MM/dd"), selectRdriver.CSDriverList(i).TotalDriverDistance)
                    Next
                    Me.DataChart.Series.Clear()
                    Me.DataChart.Titles.Clear()
                    Me.DataChart.ChartAreas.Clear()
                    Me.DataChart.Series.Add(series)
                    Me.DataChart.Titles.Add(e.Node.Text & "阶段行驶公里统计")
                    Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                    Me.DataChart.ChartAreas.Add("公里")
                    Me.DataChart.ChartAreas(0).AxisY.Title = "公里(km)"
                    Me.DataChart.ChartAreas(0).AxisY.TitleFont = New Font("Times New Roman", 15, FontStyle.Regular)
                    Me.DataChart.ChartAreas("公里").AxisX.Interval = 1
                    Dim temp As New List(Of Decimal)
                    For Each csd As CSDriver In selectRdriver.CSDriverList
                        temp.Add(csd.TotalDriverDistance)
                    Next

                    Me.Fuzhi(GlobalFunc.GetMaxValue(temp), GlobalFunc.GetMinValue(temp), GlobalFunc.GetAveValue(temp), GlobalFunc.GetVarValue(temp), _
                             selectRdriver.ToTalDriveDistance, spandrivelength.DateSpan.Days, 1)
                Else
                    Me.DataChart.Series.Clear()
                    Me.DataChart.Titles.Clear()
                    Me.DataChart.ChartAreas.Clear()
                    Me.DataChart.Legends.Clear()
                    MsgBox("没有数据!")
                    Exit Sub
                End If
            ElseIf e.Node.Level = 1 AndAlso e.Node.Parent.Text = "总体指标分析" Then
                Dim series As New Series("阶段行驶公里统计")
                If e.Node.Text = "总体" Then
                    For Each team As List(Of RDriver) In DriverTeams
                        For Each rdr As RDriver In team
                            series.Points.AddXY(rdr.DriverName, rdr.ToTalDriveDistance)
                        Next
                    Next
                    Me.DataChart.Series.Clear()
                    Me.DataChart.Titles.Clear()
                    Me.DataChart.ChartAreas.Clear()
                    Me.DataChart.Series.Add(series)
                    Me.DataChart.Titles.Add(e.Node.Text & "阶段行驶公里统计")
                    Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                    Me.DataChart.ChartAreas.Add("公里")
                    Me.DataChart.ChartAreas(0).AxisY.Title = "公里(km)"
                    Me.DataChart.ChartAreas(0).AxisY.TitleFont = New Font("Times New Roman", 15, FontStyle.Regular)
                    Me.DataChart.ChartAreas("公里").AxisX.Interval = 1
                    Dim temp As New List(Of Decimal)
                    Dim DriverNum As Integer = 0
                    For Each team As List(Of RDriver) In DriverTeams
                        For Each rdr As RDriver In team
                            temp.Add(rdr.ToTalDriveDistance)
                            DriverNum += 1
                        Next
                    Next
                    Me.Fuzhi(GlobalFunc.GetMaxValue(temp), GlobalFunc.GetMinValue(temp), GlobalFunc.GetAveValue(temp), GlobalFunc.GetVarValue(temp), _
                             GlobalFunc.GetSumValue(temp), spandrivelength.DateSpan.Days, DriverNum)
                Else
                    Dim selectteam As List(Of RDriver)
                    selectteam = DriverTeams(e.Node.Index)

                    For Each rdr As RDriver In selectteam       '给图赋值
                        series.Points.AddXY(rdr.DriverName, rdr.ToTalDriveDistance)
                    Next
                    Me.DataChart.Series.Clear()
                    Me.DataChart.Titles.Clear()
                    Me.DataChart.ChartAreas.Clear()
                    Me.DataChart.Series.Add(series)
                    Me.DataChart.Titles.Add(e.Node.Text & "阶段行驶公里统计")
                    Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                    Me.DataChart.ChartAreas.Add("公里")
                    Me.DataChart.ChartAreas(0).AxisY.Title = "公里(km)"
                    Me.DataChart.ChartAreas(0).AxisY.TitleFont = New Font("Times New Roman", 15, FontStyle.Regular)
                    Me.DataChart.ChartAreas("公里").AxisX.Interval = 1
                    Dim temp As New List(Of Decimal)
                    Dim DriverNum As Integer = 0
                    For Each rdr As RDriver In selectteam
                        temp.Add(rdr.ToTalDriveDistance)
                        DriverNum += 1
                    Next
                    Me.Fuzhi(GlobalFunc.GetMaxValue(temp), GlobalFunc.GetMinValue(temp), GlobalFunc.GetAveValue(temp), GlobalFunc.GetVarValue(temp), _
                             GlobalFunc.GetSumValue(temp), spandrivelength.DateSpan.Days, DriverNum)
                End If
            End If
        ElseIf Me.TreeView1.SelectedNode.Text = "司机阶段行驶工时统计" Then
            If e.Node.Level = 2 AndAlso e.Node.Parent.Parent.Name = "个人指标分析" Then
                Dim selectRdriver As RDriver
                selectRdriver = DriverTeams(e.Node.Parent.Index)(e.Node.Index)

                Dim series As New Series("阶段行驶工时统计")
                series.ChartType = SeriesChartType.Column
                series.BorderWidth = 3
                series.ShadowOffset = 2
                If selectRdriver.CSDriverList.Count > 0 Then
                    For i As Integer = 0 To selectRdriver.CSDriverList.Count - 1
                        series.Points.AddXY(Me.spandrivetime.StartReportDate.AddDays(i).ToString("yyyy/MM/dd"), Format(selectRdriver.CSDriverList(i).TotalWorkTime / 3600, "0.00"))
                    Next
                    Me.DataChart.Series.Clear()
                    Me.DataChart.Titles.Clear()
                    Me.DataChart.ChartAreas.Clear()
                    Me.DataChart.Series.Add(series)
                    Me.DataChart.Titles.Add(e.Node.Text & "阶段行驶工时统计")
                    Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                    Me.DataChart.ChartAreas.Add("工时")
                    Me.DataChart.ChartAreas(0).AxisY.Title = "工时(h)"
                    Me.DataChart.ChartAreas(0).AxisY.TitleFont = New Font("Times New Roman", 15, FontStyle.Regular)
                    Me.DataChart.ChartAreas("工时").AxisX.Interval = 1
                    Dim temp As New List(Of Decimal)
                    For Each csd As CSDriver In selectRdriver.CSDriverList
                        temp.Add(Format(csd.TotalWorkTime / 3600, "0.00"))
                    Next

                    Me.Fuzhi(GlobalFunc.GetMaxValue(temp), GlobalFunc.GetMinValue(temp), GlobalFunc.GetAveValue(temp), GlobalFunc.GetVarValue(temp), _
                             GlobalFunc.GetSumValue(temp), spandrivetime.DateSpan.Days, 1)
                Else
                    Me.DataChart.Series.Clear()
                    Me.DataChart.Titles.Clear()
                    Me.DataChart.ChartAreas.Clear()
                    Me.DataChart.Legends.Clear()
                    MsgBox("没有数据!")
                    Exit Sub
                End If
            ElseIf e.Node.Level = 1 AndAlso e.Node.Parent.Text = "总体指标分析" Then
                Dim series As New Series("阶段行驶工时统计")
                If e.Node.Text = "总体" Then
                    For Each team As List(Of RDriver) In DriverTeams
                        For Each rdr As RDriver In team
                            series.Points.AddXY(rdr.DriverName, Format(rdr.TotalWorkTime / 3600, "0.00"))
                        Next
                    Next
                    Me.DataChart.Series.Clear()
                    Me.DataChart.Titles.Clear()
                    Me.DataChart.ChartAreas.Clear()
                    Me.DataChart.Series.Add(series)
                    Me.DataChart.Titles.Add(e.Node.Text & "阶段行驶工时统计")
                    Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                    Me.DataChart.ChartAreas.Add("工时")
                    Me.DataChart.ChartAreas(0).AxisY.Title = "工时(h)"
                    Me.DataChart.ChartAreas(0).AxisY.TitleFont = New Font("Times New Roman", 15, FontStyle.Regular)
                    Me.DataChart.ChartAreas("工时").AxisX.Interval = 1
                    Dim temp As New List(Of Decimal)
                    Dim DriverNum As Integer = 0
                    For Each team As List(Of RDriver) In DriverTeams
                        For Each rdr As RDriver In team
                            temp.Add(Format(rdr.TotalWorkTime / 3600, "0.00"))
                            DriverNum += 1
                        Next
                    Next
                    Me.Fuzhi(GlobalFunc.GetMaxValue(temp), GlobalFunc.GetMinValue(temp), GlobalFunc.GetAveValue(temp), GlobalFunc.GetVarValue(temp), _
                             GlobalFunc.GetSumValue(temp), spandrivetime.DateSpan.Days, DriverNum)
                Else
                    Dim selectteam As List(Of RDriver)
                    selectteam = DriverTeams(e.Node.Index)

                    For Each rdr As RDriver In selectteam       '给图赋值
                        series.Points.AddXY(rdr.DriverName, Format(rdr.TotalWorkTime / 3600, "0.00"))
                    Next
                    Me.DataChart.Series.Clear()
                    Me.DataChart.Titles.Clear()
                    Me.DataChart.ChartAreas.Clear()
                    Me.DataChart.Series.Add(series)
                    Me.DataChart.Titles.Add(e.Node.Text & "阶段行驶工时统计")
                    Me.DataChart.Titles(0).Font = New Font("黑体", 20, FontStyle.Regular)
                    Me.DataChart.ChartAreas.Add("工时")
                    Me.DataChart.ChartAreas(0).AxisY.Title = "工时(h)"
                    Me.DataChart.ChartAreas(0).AxisY.TitleFont = New Font("Times New Roman", 15, FontStyle.Regular)
                    Me.DataChart.ChartAreas("工时").AxisX.Interval = 1
                    Dim temp As New List(Of Decimal)
                    Dim DriverNum As Integer = 0
                    For Each rdr As RDriver In selectteam
                        temp.Add(Format(rdr.TotalWorkTime / 3600, "0.00"))
                        DriverNum += 1
                    Next
                    Me.Fuzhi(GlobalFunc.GetMaxValue(temp), GlobalFunc.GetMinValue(temp), GlobalFunc.GetAveValue(temp), GlobalFunc.GetVarValue(temp), _
                             GlobalFunc.GetSumValue(temp), spandrivetime.DateSpan.Days, DriverNum)
                End If
            End If
        Else
            Me.DataChart.Series.Clear()
            Me.DataChart.Titles.Clear()
            Me.DataChart.ChartAreas.Clear()
            MsgBox("该项报表不做指标分析！")
        End If
    End Sub

    Public Sub Fuzhi(ByVal maxvalue As Decimal, ByVal minvalue As Decimal, ByVal avevalue As Decimal, ByVal varvalue As Decimal, ByVal totalvalue As Decimal, ByVal dutynum As Decimal, ByVal drivernum As Decimal)
        'Me.TxTMaxValue.Text = Coordination2.Global.BeTime2(maxvalue * 3600)
        'Me.TxTMinValue.Text = Coordination2.Global.BeTime2(minvalue * 3600)
        'Me.TxtAveValue.Text = Coordination2.Global.BeTime2(avevalue * 3600)
        'Me.TxtVarValue.Text = Coordination2.Global.BeTime2(varvalue * 3600)
        'Me.TxTTotalValue.Text = Coordination2.Global.BeTime2(totalvalue * 3600)
        'Me.TxTDutyNum.Text = dutynum
        'Me.TxTDriverNum.Text = drivernum

        Me.TxTMaxValue.Text = maxvalue
        Me.TxTMinValue.Text = minvalue
        Me.TxtAveValue.Text = avevalue.ToString("0.00")
        Me.TxtVarValue.Text = varvalue.ToString("0.00")
        Me.TxTTotalValue.Text = totalvalue
        Me.TxTDutyNum.Text = dutynum
        Me.TxTDriverNum.Text = drivernum
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton5.Click
        If Me.DataChart.Dock <> DockStyle.Fill Then
            Me.DataChart.Dock = DockStyle.Fill
        End If
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        If Me.DataChart.Dock = DockStyle.Fill Then
            Me.DataChart.Dock = DockStyle.None
            Me.DataChart.Size = ChartPanel.Size
        End If
        Me.DataChart.Size = New Size(Me.DataChart.Width * 1.25, Me.DataChart.Height)
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton4.Click
        If Me.DataChart.Dock = DockStyle.Fill Then
            Me.DataChart.Dock = DockStyle.None
            Me.DataChart.Size = ChartPanel.Size
        End If
        Me.DataChart.Size = New Size(Me.DataChart.Width * 0.8, Me.DataChart.Height)
    End Sub
End Class