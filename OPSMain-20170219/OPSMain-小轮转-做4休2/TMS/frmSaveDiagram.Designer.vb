<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSaveDiagram
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ProgressBar = New System.Windows.Forms.ToolStripProgressBar
        Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel
        Me.btnExit = New System.Windows.Forms.Button
        Me.dtpEndTime = New System.Windows.Forms.DateTimePicker
        Me.Label7 = New System.Windows.Forms.Label
        Me.dtpFirstTime = New System.Windows.Forms.DateTimePicker
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtMakerDep = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtSaveName = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbTrainDiamStyle = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbLineInfo = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgressBar, Me.StatusLabel})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 212)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(514, 22)
        Me.StatusStrip1.TabIndex = 40
        Me.StatusStrip1.Text = "StatusStrip1"
        Me.StatusStrip1.Visible = False
        '
        'ProgressBar
        '
        Me.ProgressBar.ForeColor = System.Drawing.Color.LawnGreen
        Me.ProgressBar.Maximum = 380
        Me.ProgressBar.Name = "ProgressBar"
        Me.ProgressBar.Size = New System.Drawing.Size(340, 16)
        '
        'StatusLabel
        '
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(0, 17)
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(298, 187)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(71, 23)
        Me.btnExit.TabIndex = 39
        Me.btnExit.Text = "退出(&E)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'dtpEndTime
        '
        Me.dtpEndTime.Location = New System.Drawing.Point(118, 147)
        Me.dtpEndTime.Name = "dtpEndTime"
        Me.dtpEndTime.Size = New System.Drawing.Size(251, 21)
        Me.dtpEndTime.TabIndex = 36
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(29, 151)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 12)
        Me.Label7.TabIndex = 35
        Me.Label7.Text = "结束执行时间:"
        '
        'dtpFirstTime
        '
        Me.dtpFirstTime.Location = New System.Drawing.Point(118, 120)
        Me.dtpFirstTime.Name = "dtpFirstTime"
        Me.dtpFirstTime.Size = New System.Drawing.Size(251, 21)
        Me.dtpFirstTime.TabIndex = 34
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(54, 96)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 12)
        Me.Label8.TabIndex = 37
        Me.Label8.Text = "编制部门:"
        '
        'txtMakerDep
        '
        Me.txtMakerDep.Location = New System.Drawing.Point(119, 93)
        Me.txtMakerDep.Name = "txtMakerDep"
        Me.txtMakerDep.Size = New System.Drawing.Size(251, 21)
        Me.txtMakerDep.TabIndex = 38
        Me.txtMakerDep.Text = "TCC"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(30, 124)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 12)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "开始执行时间:"
        '
        'txtSaveName
        '
        Me.txtSaveName.Location = New System.Drawing.Point(119, 66)
        Me.txtSaveName.Name = "txtSaveName"
        Me.txtSaveName.Size = New System.Drawing.Size(251, 21)
        Me.txtSaveName.TabIndex = 32
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(40, 69)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 12)
        Me.Label5.TabIndex = 31
        Me.Label5.Text = "运行图名称:"
        '
        'cmbTrainDiamStyle
        '
        Me.cmbTrainDiamStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTrainDiamStyle.FormattingEnabled = True
        Me.cmbTrainDiamStyle.Location = New System.Drawing.Point(118, 38)
        Me.cmbTrainDiamStyle.Name = "cmbTrainDiamStyle"
        Me.cmbTrainDiamStyle.Size = New System.Drawing.Size(251, 20)
        Me.cmbTrainDiamStyle.TabIndex = 28
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(41, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 12)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "运行图类型:"
        '
        'cmbLineInfo
        '
        Me.cmbLineInfo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLineInfo.FormattingEnabled = True
        Me.cmbLineInfo.Location = New System.Drawing.Point(119, 12)
        Me.cmbLineInfo.Name = "cmbLineInfo"
        Me.cmbLineInfo.Size = New System.Drawing.Size(251, 20)
        Me.cmbLineInfo.TabIndex = 26
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 12)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "运行图所属线路:"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(219, 187)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(73, 23)
        Me.btnSave.TabIndex = 23
        Me.btnSave.Text = "保存(&S)"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'frmSaveDiagram
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(381, 223)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.dtpEndTime)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.dtpFirstTime)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtMakerDep)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtSaveName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbTrainDiamStyle)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbLineInfo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnSave)
        Me.Name = "frmSaveDiagram"
        Me.Text = "保存运行图"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ProgressBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents StatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents dtpEndTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents dtpFirstTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtMakerDep As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtSaveName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbTrainDiamStyle As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbLineInfo As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
End Class
