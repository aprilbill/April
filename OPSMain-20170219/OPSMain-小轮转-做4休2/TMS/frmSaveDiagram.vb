Public Class frmSaveDiagram

    Private Sub frmSaveDiagram_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.cmbLineInfo.DataSource = TMSlocalDataSet.PD_LINEINFO
        Me.cmbLineInfo.DisplayMember = "LINENAME"
        Me.cmbTrainDiamStyle.DataSource = TMSlocalDataSet.TMS_TRAINDIAGRAMSTYLE
        Me.cmbTrainDiamStyle.DisplayMember = "DATENAME"
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'If Me.txtSaveName.Text <> "" And Me.txtMakerDep.Text <> "" Then
        '    If IfSameDiagramName(Me.txtSaveName.Text.ToString.Trim) = True Then
        '        MsgBox("�洢������ͼ�����Ѿ����ڣ����޸�Ϊ�������ƺ��ٵ��룡")
        '        Exit Sub
        '    End If
        '    Dim sTrainDiagramID As String
        '    Dim sInputTime As String
        '    sTrainDiagramID = Now.Year & "_" & Now.Month & "_" & Now.Day & "_" & Now.Hour & "_" & Now.Minute & "_" & Now.Second
        '    sInputTime = Date.Now.ToString '��������ͼ��ʱ��
        '    TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.AddTMS_TRAINDIAGRAMINFORow( _
        '            sTrainDiagramID, Me.cmbLineInfo.Text.Trim, Me.txtSaveName.Text.ToString.Trim, Me.cmbTrainDiamStyle.Text, sInputTime, Me.dtpFirstTime.Text, Me.dtpEndTime.Text, Me.txtMakerDep.Text)
        '    Try
        '        TMSlocalDataSet.Update("TMS_TRAINDIAGRAMINFO")
        '    Catch ex As Exception
        '        MessageBox.Show(ex.ToString, "������̷�������!","��ʾ")
        '    End Try
        '    If (Me.cmbTrainDiaName.Text.Trim <> "") Then
        '        Me.StatusStrip1.Visible = True
        '        Call InputACCESSTimeTable(sTrainDiagramID, Me.cmbTrainDiaName.Text)
        '        Me.StatusStrip1.Visible = False
        '    End If
        '    MessageBox.Show(Me.cmbTrainDiaName.Text & "���ݵ���ɹ���", "�ɹ�����", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Me.Dispose()
        'End If
    End Sub
End Class