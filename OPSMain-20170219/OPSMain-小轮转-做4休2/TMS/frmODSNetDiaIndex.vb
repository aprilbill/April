Public Class frmODSNetDiaIndex

    Private Sub frmODSNetDiaIndex_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        Dim sCurVersion As String
        Me.dgv.Rows.Clear()
        Me.dgv.ReadOnly = True
        Me.dgv.MultiSelect = False
        Me.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnMode.AllCells
        Me.dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        For i = 1 To Me.dgv.Columns.Count
            Me.dgv.Columns(i - 1).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        Dim nValue(11) As Single
        For i = 1 To UBound(DiaVer)
            sCurVersion = DiaVer(i).sCurDiaVersion
            TMSlocalDataSet.TMS_TIMETABLE_WHOLE_INDEX.Clear()
            TMSlocalDataSet.Fill("TMS_TIMETABLE_WHOLE_INDEX", "TRAINDIAGRAMEID='" & sCurVersion & "'")
            If TMSlocalDataSet.TMS_TIMETABLE_WHOLE_INDEX.Count > 0 Then
                Me.dgv.Rows.Add()
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(0).Value = i
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(1).Value = DiaVer(i).sLineName
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(2).Value = TMSlocalDataSet.TMS_TIMETABLE_WHOLE_INDEX(0).Item("DOWNTRAINNUM")
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(3).Value = TMSlocalDataSet.TMS_TIMETABLE_WHOLE_INDEX(0).Item("UPTRAINNUM")
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(4).Value = TMSlocalDataSet.TMS_TIMETABLE_WHOLE_INDEX(0).Item("TOLTRAINNUM")
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(5).Value = TMSlocalDataSet.TMS_TIMETABLE_WHOLE_INDEX(0).Item("YADAOTRAINNUM")
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(6).Value = TMSlocalDataSet.TMS_TIMETABLE_WHOLE_INDEX(0).Item("TIAOTRAINNUM")
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(7).Value = TMSlocalDataSet.TMS_TIMETABLE_WHOLE_INDEX(0).Item("ZAIKETRAINNUM")
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(8).Value = TMSlocalDataSet.TMS_TIMETABLE_WHOLE_INDEX(0).Item("KONGTRAINNUM")
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(9).Value = TMSlocalDataSet.TMS_TIMETABLE_WHOLE_INDEX(0).Item("EVETRAINSPEED")
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(10).Value = TMSlocalDataSet.TMS_TIMETABLE_WHOLE_INDEX(0).Item("TECTRAINSPEED")
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(11).Value = TMSlocalDataSet.TMS_TIMETABLE_WHOLE_INDEX(0).Item("TRAINTOLRUN")
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(12).Value = TMSlocalDataSet.TMS_TIMETABLE_WHOLE_INDEX(0).Item("TOLRUNSTOCKNUM")
                nValue(1) = nValue(1) + Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(2).Value
                nValue(2) = nValue(2) + Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(3).Value
                nValue(3) = nValue(3) + Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(4).Value
                nValue(4) = nValue(4) + Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(5).Value
                nValue(5) = nValue(5) + Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(6).Value
                nValue(6) = nValue(6) + Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(7).Value
                nValue(7) = nValue(7) + Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(8).Value
                nValue(8) = nValue(8) + Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(9).Value * Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(4).Value
                nValue(9) = nValue(9) + Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(10).Value * Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(4).Value
                nValue(10) = nValue(10) + Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(11).Value
                nValue(11) = nValue(11) + Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(12).Value
            End If
        Next

        If Me.dgv.Rows.Count > 0 Then
            Me.dgv.Rows.Add()
            Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(0).Value = ""
            Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(1).Value = "合计"
            Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(2).Value = nValue(1)
            Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(3).Value = nValue(2)
            Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(4).Value = nValue(3)
            Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(5).Value = nValue(4)
            Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(6).Value = nValue(5)
            Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(7).Value = nValue(6)
            Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(8).Value = nValue(7)
            Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(9).Value = Format(nValue(8) / nValue(3), "#.##")
            Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(10).Value = Format(nValue(9) / nValue(3), "#.##")
            Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(11).Value = nValue(10)
            Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(12).Value = nValue(11)
            For i = 1 To Me.dgv.Columns.Count
                Me.dgv.Rows(Me.dgv.Rows.Count - 1).Cells(i - 1).Style.BackColor = Color.LightGray
            Next
        End If
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Call OutPutToEXCELFileFormDataGrid("线网运行图指标", Me.dgv, Me)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class