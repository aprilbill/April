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
        '        MsgBox("存储的运行图名称已经存在，请修改为其他名称后再导入！")
        '        Exit Sub
        '    End If
        '    Dim sTrainDiagramID As String
        '    Dim sInputTime As String
        '    sTrainDiagramID = Now.Year & "_" & Now.Month & "_" & Now.Day & "_" & Now.Hour & "_" & Now.Minute & "_" & Now.Second
        '    sInputTime = Date.Now.ToString '导入运行图的时间
        '    TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.AddTMS_TRAINDIAGRAMINFORow( _
        '            sTrainDiagramID, Me.cmbLineInfo.Text.Trim, Me.txtSaveName.Text.ToString.Trim, Me.cmbTrainDiamStyle.Text, sInputTime, Me.dtpFirstTime.Text, Me.dtpEndTime.Text, Me.txtMakerDep.Text)
        '    Try
        '        TMSlocalDataSet.Update("TMS_TRAINDIAGRAMINFO")
        '    Catch ex As Exception
        '        MessageBox.Show(ex.ToString, "导入过程发生错误!","提示")
        '    End Try
        '    If (Me.cmbTrainDiaName.Text.Trim <> "") Then
        '        Me.StatusStrip1.Visible = True
        '        Call InputACCESSTimeTable(sTrainDiagramID, Me.cmbTrainDiaName.Text)
        '        Me.StatusStrip1.Visible = False
        '    End If
        '    MessageBox.Show(Me.cmbTrainDiaName.Text & "数据导入成功！", "成功操作", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    Me.Dispose()
        'End If
    End Sub
End Class