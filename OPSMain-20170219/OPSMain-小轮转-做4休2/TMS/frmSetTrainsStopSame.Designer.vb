<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetTrainsStopSame
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
        Me.Label4 = New System.Windows.Forms.Label
        Me.CmbStartSta = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbEndSta = New System.Windows.Forms.ComboBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.labCurTrip = New System.Windows.Forms.Label
        Me.调匀停站 = New System.Windows.Forms.GroupBox
        Me.labCurTrain = New System.Windows.Forms.Label
        Me.btnOk = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.optStart = New System.Windows.Forms.RadioButton
        Me.optEnd = New System.Windows.Forms.RadioButton
        Me.调匀停站.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(11, 109)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "结束车站："
        '
        'CmbStartSta
        '
        Me.CmbStartSta.FormattingEnabled = True
        Me.CmbStartSta.Location = New System.Drawing.Point(72, 75)
        Me.CmbStartSta.Name = "CmbStartSta"
        Me.CmbStartSta.Size = New System.Drawing.Size(140, 20)
        Me.CmbStartSta.TabIndex = 22
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "起始车站："
        '
        'cmbEndSta
        '
        Me.cmbEndSta.FormattingEnabled = True
        Me.cmbEndSta.Location = New System.Drawing.Point(72, 106)
        Me.cmbEndSta.Name = "cmbEndSta"
        Me.cmbEndSta.Size = New System.Drawing.Size(140, 20)
        Me.cmbEndSta.TabIndex = 24
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(177, 207)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 21
        Me.btnCancel.Text = "退出(&E)"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'labCurTrip
        '
        Me.labCurTrip.AutoSize = True
        Me.labCurTrip.Location = New System.Drawing.Point(10, 49)
        Me.labCurTrip.Name = "labCurTrip"
        Me.labCurTrip.Size = New System.Drawing.Size(65, 12)
        Me.labCurTrip.TabIndex = 15
        Me.labCurTrip.Text = "开行交路："
        '
        '调匀停站
        '
        Me.调匀停站.Controls.Add(Me.cmbEndSta)
        Me.调匀停站.Controls.Add(Me.Label4)
        Me.调匀停站.Controls.Add(Me.CmbStartSta)
        Me.调匀停站.Controls.Add(Me.Label3)
        Me.调匀停站.Controls.Add(Me.labCurTrip)
        Me.调匀停站.Controls.Add(Me.labCurTrain)
        Me.调匀停站.Location = New System.Drawing.Point(12, 58)
        Me.调匀停站.Name = "调匀停站"
        Me.调匀停站.Size = New System.Drawing.Size(240, 143)
        Me.调匀停站.TabIndex = 22
        Me.调匀停站.TabStop = False
        Me.调匀停站.Text = "调匀停站"
        '
        'labCurTrain
        '
        Me.labCurTrain.AutoSize = True
        Me.labCurTrain.Location = New System.Drawing.Point(10, 22)
        Me.labCurTrain.Name = "labCurTrain"
        Me.labCurTrain.Size = New System.Drawing.Size(65, 12)
        Me.labCurTrain.TabIndex = 14
        Me.labCurTrain.Text = "当前车次："
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(96, 207)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 20
        Me.btnOk.Text = "应用(&A)"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optEnd)
        Me.GroupBox1.Controls.Add(Me.optStart)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(240, 48)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "调匀方式"
        '
        'optStart
        '
        Me.optStart.AutoSize = True
        Me.optStart.Location = New System.Drawing.Point(139, 20)
        Me.optStart.Name = "optStart"
        Me.optStart.Size = New System.Drawing.Size(71, 16)
        Me.optStart.TabIndex = 0
        Me.optStart.TabStop = True
        Me.optStart.Text = "始发调匀"
        Me.optStart.UseVisualStyleBackColor = True
        '
        'optEnd
        '
        Me.optEnd.AutoSize = True
        Me.optEnd.Checked = True
        Me.optEnd.Location = New System.Drawing.Point(13, 20)
        Me.optEnd.Name = "optEnd"
        Me.optEnd.Size = New System.Drawing.Size(71, 16)
        Me.optEnd.TabIndex = 1
        Me.optEnd.TabStop = True
        Me.optEnd.Text = "终到调匀"
        Me.optEnd.UseVisualStyleBackColor = True
        '
        'frmSetTrainsStopSame
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(264, 238)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.调匀停站)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSetTrainsStopSame"
        Me.Text = "调匀停站"
        Me.调匀停站.ResumeLayout(False)
        Me.调匀停站.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CmbStartSta As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbEndSta As System.Windows.Forms.ComboBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents labCurTrip As System.Windows.Forms.Label
    Friend WithEvents 调匀停站 As System.Windows.Forms.GroupBox
    Friend WithEvents labCurTrain As System.Windows.Forms.Label
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optEnd As System.Windows.Forms.RadioButton
    Friend WithEvents optStart As System.Windows.Forms.RadioButton
End Class
