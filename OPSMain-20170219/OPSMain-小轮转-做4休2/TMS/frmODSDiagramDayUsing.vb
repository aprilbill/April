Public Class frmODSDiagramDayUsing

    Private Sub frmDiagramDayUsing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i, j As Integer
        TMSlocalDataSet.PD_LINEINFO.Clear()
        TMSlocalDataSet.TMS_TRAINDIAGRAMSTYLE.Clear()
        TMSlocalDataSet.Fill("PD_LINEINFO", "LINEOPERATIONSTATE = '��Ӫ'")
        TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMSTYLE", "1=1 order by TRAINDIASTYLENAME asc")
        Me.dgv.Rows.Clear()
        Dim nCurRow As Integer
        nCurRow = 0

        Me.cmbLineName.Items.Clear()
        Me.cmbStyle.Items.Clear()
        For j = 1 To TMSlocalDataSet.TMS_TRAINDIAGRAMSTYLE.Rows.Count
            Me.cmbStyle.Items.Add(TMSlocalDataSet.TMS_TRAINDIAGRAMSTYLE.Rows(j - 1).Item("DATENAME"))
        Next
        If Me.cmbStyle.Items.Count > 0 Then
            Me.cmbStyle.Text = Me.cmbStyle.Items(0)
        End If

        For i = 1 To TMSlocalDataSet.PD_LINEINFO.Rows.Count
            Me.cmbLineName.Items.Add(TMSlocalDataSet.PD_LINEINFO.Rows(i - 1).Item("LINENAME"))
            Me.dgv.Rows.Add()
            Me.dgv.Rows(nCurRow).Cells(0).Value = nCurRow + 1
            Me.dgv.Rows(nCurRow).Cells(1).Value = TMSlocalDataSet.PD_LINEINFO.Rows(i - 1).Item("LINENAME")
            nCurRow = nCurRow + 1
        Next
        If Me.cmbLineName.Items.Count > 0 Then
            Me.cmbLineName.Text = Me.cmbLineName.Items(0)
        End If
        Call ShowCurDate(Me.Datetime.Value)
    End Sub

    '  ������·��������ͼ������ʾ��Ӧ����ͼ
    Private Sub ShowListName(ByVal sLineName As String, ByVal sDiaStyle As String)
        TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Clear()
        TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMINFO")
        Dim nCurRow As Integer
        nCurRow = 0
        Dim k As Integer
        Me.cmbName.Items.Clear()
        For k = 1 To TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Rows.Count
            If TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Rows(k - 1).Item("LINENAME") = sLineName And TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Rows(k - 1).Item("TRAINDIASTYLENAME") = sDiaStyle Then
                Me.cmbName.Items.Add(TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Rows(k - 1).Item("TIMETABLENAME"))
            End If
        Next
        Me.cmbName.Items.Add("��")
        Me.cmbName.Text = Me.cmbName.Items(0)

    End Sub
    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click
        Dim i As Integer
        For i = 1 To Me.dgv.Rows.Count
            If Me.dgv.Rows(i - 1).Cells(1).Value.ToString.Trim = Me.cmbLineName.Text.Trim Then
                Me.dgv.Rows(i - 1).Cells(2).Value = Me.cmbStyle.Text.Trim
                Me.dgv.Rows(i - 1).Cells(3).Value = Me.cmbName.Text.Trim
                Exit Sub
            End If
        Next
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim i As Integer
        Dim dCurDate As String
        Dim tmpDate As String
        Dim sCurDiaID As String
        dCurDate = DateFormat(Me.Datetime.Value)
        For i = 1 To TMSlocalDataSet.TMS_TRAINDIAGRAMUSING.Rows.Count
            tmpDate = TMSlocalDataSet.TMS_TRAINDIAGRAMUSING.Rows(i - 1).Item("INPUTDATE")
            If tmpDate = dCurDate Then
                If MsgBox("��ǰʱ���Ѿ����ڣ��Ƿ񸲸�ԭ�еļ�¼��", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "ȷ�ϲ���") = MsgBoxResult.Ok Then
                    For Each Row As DataAccessTier.OdsDataSet.TMS_TRAINDIAGRAMUSINGRow In TMSlocalDataSet.TMS_TRAINDIAGRAMUSING
                        If Row.Item("INPUTDATE") = dCurDate Then
                            Row.Delete()
                        End If
                    Next
                End If
                Exit For
            End If
        Next
        For i = 1 To Me.dgv.Rows.Count
            sCurDiaID = GetDiagramVersionFromName(Me.dgv.Rows(i - 1).Cells(3).Value.ToString.Trim)
            TMSlocalDataSet.TMS_TRAINDIAGRAMUSING.AddTMS_TRAINDIAGRAMUSINGRow(dCurDate, Me.dgv.Rows(i - 1).Cells(1).Value.ToString.Trim, sCurDiaID)
        Next
        Try
            TMSlocalDataSet.Update("TMS_TRAINDIAGRAMUSING")
            MsgBox("�Ѿ��ɹ�����", , "��ʾ")
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "������̷�������!")
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        'Call LoadCurVersion()
        Me.Close()
    End Sub

    Private Sub Datetime_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Datetime.ValueChanged
        Call ShowCurDate(Me.Datetime.Value)
    End Sub

    Private Sub ShowCurDate(ByVal dCurDate As Date)
        TMSlocalDataSet.TMS_TRAINDIAGRAMUSING.Clear()
        TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMUSING")
        Dim i, j As Integer
        Dim tmpCurVer As String
        For i = 1 To Me.dgv.Rows.Count
            Me.dgv.Rows(i - 1).Cells(2).Value = "��"
            Me.dgv.Rows(i - 1).Cells(3).Value = "��"
        Next
        For i = 1 To TMSlocalDataSet.TMS_TRAINDIAGRAMUSING.Rows.Count
            If TMSlocalDataSet.TMS_TRAINDIAGRAMUSING.Rows(i - 1).Item("INPUTDATE") = DateFormat(dCurDate.Date) Then
                For j = 1 To Me.dgv.Rows.Count
                    If Me.dgv.Rows(j - 1).Cells(1).Value = TMSlocalDataSet.TMS_TRAINDIAGRAMUSING.Rows(i - 1).Item("LINENAME") Then
                        tmpCurVer = TMSlocalDataSet.TMS_TRAINDIAGRAMUSING.Rows(i - 1).Item("TRAINDIAGRAMID")
                        Me.dgv.Rows(j - 1).Cells(3).Value = GetDiagramNameFromVersion(tmpCurVer)
                        Me.dgv.Rows(j - 1).Cells(2).Value = GetDiagramStyleFromVersion(tmpCurVer)
                    End If
                Next
            End If
        Next
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMouth.Click
        Dim i, k As Integer
        Dim dCurDate As String
        Dim tmpDate As String
        Dim sCurDiaID As String

        TMSlocalDataSet.TMS_TRAINDIAGRAMUSING.Clear()
        TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMUSING")

        If MsgBox("�ò������Ḳ��ԭ�еļ�¼��", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "ȷ�ϲ���") = MsgBoxResult.Ok Then
            Progress.ProgressForm.StartProgress(30, "���ڱ��棬���Ժ�...")

            For k = 0 To 30
                dCurDate = DateFormat(Me.Datetime.Value.AddDays(k))
                For Each Row As DataAccessTier.OdsDataSet.TMS_TRAINDIAGRAMUSINGRow In TMSlocalDataSet.TMS_TRAINDIAGRAMUSING
                    tmpDate = Row.Item("INPUTDATE")
                    If tmpDate = dCurDate Then
                        Row.Delete()
                    End If
                Next
                For i = 1 To Me.dgv.Rows.Count
                    sCurDiaID = GetDiagramVersionFromName(Me.dgv.Rows(i - 1).Cells(3).Value.ToString.Trim)
                    TMSlocalDataSet.TMS_TRAINDIAGRAMUSING.AddTMS_TRAINDIAGRAMUSINGRow(dCurDate, Me.dgv.Rows(i - 1).Cells(1).Value.ToString.Trim, sCurDiaID)
                Next
                TMSlocalDataSet.Update("TMS_TRAINDIAGRAMUSING")
                Progress.ProgressForm.PerformStep()
            Next
            Progress.ProgressForm.EndProgress()
            MsgBox("�Ѿ��ɹ�����", , "��ʾ")
        End If
    End Sub

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        Dim sCurSelectID As String
        If Me.dgv.Rows.Count > 0 Then
            If Me.dgv.CurrentCell.RowIndex >= 0 Then
                sCurSelectID = GetDiagramVersionFromName(Me.dgv.CurrentRow.Cells(3).Value.ToString.Trim)
                If sCurSelectID = "��" Or sCurSelectID = "" Then
                    MsgBox("������ͼ�����ڣ��޷���!", , "��ʾ")
                Else
                    ODSPubpara.DiagramSelect = sCurSelectID
                    Call LoadDiagramData("������ͼ")
                    ODSPubpara.sCurShowListState = "��ʾ����ȫͼ"
                    Dim nf As New frmODSTimeTableMain
                    nf.Show()
                End If
            End If
        End If
    End Sub

    Private Sub dgv_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dgv.CellMouseDown
        If e.RowIndex >= 0 Then
            Me.cmbLineName.Text = Me.dgv.Rows(e.RowIndex).Cells(1).Value.ToString.Trim
            Me.cmbStyle.Text = Me.dgv.Rows(e.RowIndex).Cells(2).Value.ToString.Trim
            Me.cmbName.Text = Me.dgv.Rows(e.RowIndex).Cells(3).Value.ToString.Trim
        End If
    End Sub

    Private Sub cmbStyle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStyle.SelectedIndexChanged
        Call ShowListName(Me.cmbLineName.Text.Trim, Me.cmbStyle.Text.Trim)
    End Sub

    Private Sub cmbLineName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbLineName.SelectedIndexChanged
        Call ShowListName(Me.cmbLineName.Text.Trim, Me.cmbStyle.Text.Trim)
    End Sub
End Class