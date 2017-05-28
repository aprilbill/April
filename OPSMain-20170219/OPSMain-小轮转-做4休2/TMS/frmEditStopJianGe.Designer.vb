<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditStopJianGe
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.labCurTrip = New System.Windows.Forms.Label
        Me.labCurTrain = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.调匀停站 = New System.Windows.Forms.GroupBox
        Me.btnCal = New System.Windows.Forms.Button
        Me.cmbEndSta = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.CmbStartSta = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.numTime = New System.Windows.Forms.NumericUpDown
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOk = New System.Windows.Forms.Button
        Me.grdTime = New System.Windows.Forms.DataGridView
        Me.序号 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.车站名称 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.增加时间 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.optEnd = New System.Windows.Forms.RadioButton
        Me.optStart = New System.Windows.Forms.RadioButton
        Me.调匀停站.SuspendLayout()
        CType(Me.numTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
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
        'labCurTrain
        '
        Me.labCurTrain.AutoSize = True
        Me.labCurTrain.Location = New System.Drawing.Point(10, 22)
        Me.labCurTrain.Name = "labCurTrain"
        Me.labCurTrain.Size = New System.Drawing.Size(65, 12)
        Me.labCurTrain.TabIndex = 14
        Me.labCurTrain.Text = "当前车次："
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "需调匀时间："
        '
        '调匀停站
        '
        Me.调匀停站.Controls.Add(Me.btnCal)
        Me.调匀停站.Controls.Add(Me.cmbEndSta)
        Me.调匀停站.Controls.Add(Me.Label4)
        Me.调匀停站.Controls.Add(Me.CmbStartSta)
        Me.调匀停站.Controls.Add(Me.Label3)
        Me.调匀停站.Controls.Add(Me.Label2)
        Me.调匀停站.Controls.Add(Me.labCurTrip)
        Me.调匀停站.Controls.Add(Me.numTime)
        Me.调匀停站.Controls.Add(Me.labCurTrain)
        Me.调匀停站.Controls.Add(Me.Label1)
        Me.调匀停站.Location = New System.Drawing.Point(11, 59)
        Me.调匀停站.Name = "调匀停站"
        Me.调匀停站.Size = New System.Drawing.Size(240, 200)
        Me.调匀停站.TabIndex = 17
        Me.调匀停站.TabStop = False
        Me.调匀停站.Text = "调匀停站"
        '
        'btnCal
        '
        Me.btnCal.Location = New System.Drawing.Point(135, 166)
        Me.btnCal.Name = "btnCal"
        Me.btnCal.Size = New System.Drawing.Size(76, 23)
        Me.btnCal.TabIndex = 26
        Me.btnCal.Text = "计算(&C)"
        Me.btnCal.UseVisualStyleBackColor = True
        '
        'cmbEndSta
        '
        Me.cmbEndSta.FormattingEnabled = True
        Me.cmbEndSta.Location = New System.Drawing.Point(90, 138)
        Me.cmbEndSta.Name = "cmbEndSta"
        Me.cmbEndSta.Size = New System.Drawing.Size(121, 20)
        Me.cmbEndSta.TabIndex = 24
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 141)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "结束车站："
        '
        'CmbStartSta
        '
        Me.CmbStartSta.FormattingEnabled = True
        Me.CmbStartSta.Location = New System.Drawing.Point(90, 107)
        Me.CmbStartSta.Name = "CmbStartSta"
        Me.CmbStartSta.Size = New System.Drawing.Size(121, 20)
        Me.CmbStartSta.TabIndex = 22
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "起始车站："
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(217, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(17, 12)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "秒"
        '
        'numTime
        '
        Me.numTime.Location = New System.Drawing.Point(90, 76)
        Me.numTime.Maximum = New Decimal(New Integer() {86401, 0, 0, 0})
        Me.numTime.Minimum = New Decimal(New Integer() {36000, 0, 0, -2147483648})
        Me.numTime.Name = "numTime"
        Me.numTime.Size = New System.Drawing.Size(121, 21)
        Me.numTime.TabIndex = 19
        Me.numTime.Value = New Decimal(New Integer() {60, 0, 0, 0})
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(385, 272)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 16
        Me.btnCancel.Text = "退出(&E)"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(293, 272)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 15
        Me.btnOk.Text = "应用(&A)"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'grdTime
        '
        Me.grdTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdTime.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.序号, Me.车站名称, Me.增加时间})
        Me.grdTime.Location = New System.Drawing.Point(257, 12)
        Me.grdTime.MultiSelect = False
        Me.grdTime.Name = "grdTime"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.FormatProvider = New System.Globalization.CultureInfo("zh-CN")
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTime.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdTime.RowHeadersWidth = 30
        Me.grdTime.RowTemplate.Height = 23
        Me.grdTime.Size = New System.Drawing.Size(203, 247)
        Me.grdTime.TabIndex = 19
        Me.grdTime.Text = "DataGridView1"
        '
        '序号
        '
        Me.序号.HeaderText = "序号"
        Me.序号.Name = "序号"
        Me.序号.Width = 40
        '
        '车站名称
        '
        Me.车站名称.HeaderText = "车站名称"
        Me.车站名称.Name = "车站名称"
        Me.车站名称.Width = 60
        '
        '增加时间
        '
        Me.增加时间.HeaderText = "增加时间"
        Me.增加时间.Name = "增加时间"
        Me.增加时间.Width = 60
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optEnd)
        Me.GroupBox1.Controls.Add(Me.optStart)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(240, 48)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "调匀方式"
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
        'optStart
        '
        Me.optStart.AutoSize = True
        Me.optStart.Location = New System.Drawing.Point(139, 20)
        Me.optStart.Name = "optStart"
        Me.optStart.Size = New System.Drawing.Size(71, 16)
        Me.optStart.TabIndex = 0
        Me.optStart.Text = "始发调匀"
        Me.optStart.UseVisualStyleBackColor = True
        '
        'frmEditStopJianGe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(466, 303)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grdTime)
        Me.Controls.Add(Me.调匀停站)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEditStopJianGe"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "调整停站时间"
        Me.调匀停站.ResumeLayout(False)
        Me.调匀停站.PerformLayout()
        CType(Me.numTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents labCurTrip As System.Windows.Forms.Label
    Friend WithEvents labCurTrain As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents 调匀停站 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents cmbEndSta As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CmbStartSta As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents numTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnCal As System.Windows.Forms.Button
    Friend WithEvents grdTime As System.Windows.Forms.DataGridView
    Friend WithEvents 序号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 车站名称 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 增加时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optEnd As System.Windows.Forms.RadioButton
    Friend WithEvents optStart As System.Windows.Forms.RadioButton
End Class
