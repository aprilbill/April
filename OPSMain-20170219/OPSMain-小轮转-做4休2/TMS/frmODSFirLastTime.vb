Public Class frmODSFirLastTime
    Public sCurListLineName As String
    Private Sub frmODSFirLastTime_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        Dim sCurVersion As String
        Me.dgv.Rows.Clear()
        Me.dgv.ReadOnly = True
        Me.dgv.MultiSelect = False

        'Me.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.AllCells
        Me.dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        For i = 1 To Me.dgv.Columns.Count
            Me.dgv.Columns(i - 1).SortMode = DataGridViewColumnSortMode.NotSortable
            If i > 1 Then
                Me.dgv.Columns(i - 1).Width = 100
            Else
                Me.dgv.Columns(i - 1).AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            End If
        Next
        If sCurListLineName = "所有线路" Then
            For i = 1 To ODSStainf.Count
                sCurVersion = GetCurDiaVersionFromLineName(ODSStainf(i - 1).sLineName)
                TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE.Clear()
                TMSlocalDataSet.Fill("TMS_FIRLAST_TRAIN_TIMETABLE", "TRAINDIAGRAMID='" & sCurVersion & "' and STANAME='" & ODSStainf(i - 1).sStaName & "'")
                Me.dgv.Rows.Add()
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(0).Value = Me.dgv.Rows.Count
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(1).Value = ODSStainf(i - 1).sLineName
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(2).Value = ODSStainf(i - 1).sStaName
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(3).Value = "无"
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(4).Value = "无"
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(5).Value = "无"
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(6).Value = "无"
                If TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE.Count > 0 Then
                    Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(3).Value = TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE(0).Item("DOWNFIRTIME")
                    Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(4).Value = TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE(0).Item("DOWNLASTTIME")
                    Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(5).Value = TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE(0).Item("UPFIRTIME")
                    Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(6).Value = TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE(0).Item("UPLASTTIME")
                End If
            Next
        Else

            For i = 1 To ODSStainf.Count
                sCurVersion = GetCurDiaVersionFromLineName(ODSStainf(i - 1).sLineName)
                If ODSStainf(i - 1).sLineName = sCurListLineName Then
                    TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE.Clear()
                    TMSlocalDataSet.Fill("TMS_FIRLAST_TRAIN_TIMETABLE", "TRAINDIAGRAMID='" & sCurVersion & "' and STANAME='" & ODSStainf(i - 1).sStaName & "'")
                    Me.dgv.Rows.Add()
                    Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(0).Value = Me.dgv.Rows.Count
                    Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(1).Value = ODSStainf(i - 1).sLineName
                    Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(2).Value = ODSStainf(i - 1).sStaName
                    Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(3).Value = "无"
                    Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(4).Value = "无"
                    Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(5).Value = "无"
                    Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(6).Value = "无"
                    If TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE.Count > 0 Then
                        Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(3).Value = TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE(0).Item("DOWNFIRTIME")
                        Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(4).Value = TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE(0).Item("DOWNLASTTIME")
                        Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(5).Value = TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE(0).Item("UPFIRTIME")
                        Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(6).Value = TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE(0).Item("UPLASTTIME")
                    End If
                End If
            Next
            'For i = 1 To UBound(DiaVer)
            '    sCurVersion = DiaVer(i).sCurDiaVersion
            '    If DiaVer(i).sLineName = sCurListLineName Then
            '        TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE.Clear()
            '        TMSlocalDataSet.Fill("TMS_FIRLAST_TRAIN_TIMETABLE", "TRAINDIAGRAMID='" & sCurVersion & "'")
            '        If TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE.Count > 0 Then
            '            For j = 1 To TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE.Count
            '                Me.dgv.Rows.Add()
            '                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(0).Value = Me.dgv.Rows.Count
            '                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(1).Value = TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE(j - 1).Item("LINENAME")
            '                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(2).Value = TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE(j - 1).Item("STANAME")
            '                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(3).Value = TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE(j - 1).Item("DOWNFIRTIME")
            '                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(4).Value = TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE(j - 1).Item("DOWNLASTTIME")
            '                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(5).Value = TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE(j - 1).Item("UPFIRTIME")
            '                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(6).Value = TMSlocalDataSet.TMS_FIRLAST_TRAIN_TIMETABLE(j - 1).Item("UPLASTTIME")
            '            Next
            '        Else
            '            MsgBox(sCurListLineName & "首末班车时刻表还没有计算并保存，请至运行图浏览界面进行指标计算并保存！")
            '        End If
            '    End If
            'Next
        End If
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Call OutPutToEXCELFileFormDataGrid("路网首末班车时刻", Me.dgv, Me)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class