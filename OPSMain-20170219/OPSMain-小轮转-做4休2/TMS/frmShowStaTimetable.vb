Public Class frmShowStaTimetable
    Public sCurStaName As String
    'Public sCurLineName As String
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmShowStaTimetable_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.dgv.MultiSelect = False
        Me.dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Me.dgvUp.MultiSelect = False
        Me.dgvUp.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Dim i, j As Integer
        Dim nCurRow, nCurRow2 As Integer
        nCurRow = 0
        nCurRow2 = 0
        Dim sCurVersion As String
        Dim sCurLineName As String
        Dim sCurSta As String
        sCurSta = GetDiaStaNameFromPrintStaName(sCurStaName)
        For i = 1 To UBound(DiaVer)
            sCurVersion = DiaVer(i).sCurDiaVersion
            sCurLineName = DiaVer(i).sLineName
            If sCurVersion <> "" Or sCurVersion <> "��" Then
                TMSlocalDataSet.TMS_TIMETABLEINFO.Clear()
                TMSlocalDataSet.TMS_STOCKUSINGINFO.Clear()
                TMSlocalDataSet.Fill("TMS_TIMETABLEINFO", "TRAINDIAGRAMID='" & sCurVersion & "' and STATIONNAME='" & sCurSta & "' order by ARRITIME")
                TMSlocalDataSet.Fill("TMS_STOCKUSINGINFO", "TRAINDIAGRAMID='" & sCurVersion & "'")
                For Each Row As DataAccessTier.OdsDataSet.TMS_TIMETABLEINFORow In TMSlocalDataSet.TMS_TIMETABLEINFO
                    For j = 1 To TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows.Count
                        If TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(j - 1).Item("LINKTRAINNUM") = Row.TRAINNUM Then
                            If Row.TRAINNUM Mod 2 = 0 Then
                                Me.dgvUp.Rows.Add()
                                nCurRow2 = nCurRow2 + 1
                                Me.dgvUp.Rows(nCurRow2 - 1).Cells(0).Value = nCurRow
                                Me.dgvUp.Rows(nCurRow2 - 1).Cells(1).Value = sCurLineName
                                Me.dgvUp.Rows(nCurRow2 - 1).Cells(2).Value = TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(j - 1).Item("PRINTNUM")
                                Me.dgvUp.Rows(nCurRow2 - 1).Cells(3).Value = SecondToHour(Row.ARRITIME, 0)
                                Me.dgvUp.Rows(nCurRow2 - 1).Cells(4).Value = SecondToHour(Row.DEPARTTIME, 0)
                                Me.dgvUp.Rows(nCurRow2 - 1).Cells(5).Value = TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(j - 1).Item("ROUTINGSTYLE")
                            Else
                                Me.dgv.Rows.Add()
                                nCurRow = nCurRow + 1
                                Me.dgv.Rows(nCurRow - 1).Cells(0).Value = nCurRow
                                Me.dgv.Rows(nCurRow - 1).Cells(1).Value = sCurLineName
                                Me.dgv.Rows(nCurRow - 1).Cells(2).Value = TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(j - 1).Item("PRINTNUM")
                                Me.dgv.Rows(nCurRow - 1).Cells(3).Value = SecondToHour(Row.ARRITIME, 0)
                                Me.dgv.Rows(nCurRow - 1).Cells(4).Value = SecondToHour(Row.DEPARTTIME, 0)
                                Me.dgv.Rows(nCurRow - 1).Cells(5).Value = TMSlocalDataSet.TMS_STOCKUSINGINFO.Rows(j - 1).Item("ROUTINGSTYLE")
                            End If
                            Exit For
                        End If
                    Next
                Next
            End If
        Next
        Me.Text = sCurStaName & "վ ��վʱ�̱�"
        If Me.dgv.Rows.Count = 0 And Me.dgvUp.Rows.Count = 0 Then
            MsgBox("�Բ���û���ҵ���վ��ʱ�̱���ȷ�ϵ��������ͼ�ļ����վ���Ƿ���ȷ��", , "��ʾ")
            Me.Close()
        End If
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Call OutPutToEXCELFileFormDataGrid(sCurStaName & "��վʱ�̱�-����", Me.dgv, Me)
        Call OutPutToEXCELFileFormDataGrid(sCurStaName & "��վʱ�̱�-����", Me.dgvUp, Me)
    End Sub

    Private Sub btnDiagram_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDiagram.Click
        Call ShowInterChangerstaDiagram(sCurStaName)
    End Sub

End Class