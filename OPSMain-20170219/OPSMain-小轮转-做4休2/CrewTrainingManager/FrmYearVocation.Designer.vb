<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmYearVocation
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmYearVocation))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.EndTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.StartTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Btn_DeleteExist = New System.Windows.Forms.Button()
        Me.Btn_Save = New System.Windows.Forms.Button()
        Me.Btn_CheckExist = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BtnSeqSetting = New System.Windows.Forms.Button()
        Me.CmbLine = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.DGVDriversSeq = New System.Windows.Forms.DataGridView()
        Me.顺序 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.组号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.司机姓名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DGVYearVocDetial = New System.Windows.Forms.DataGridView()
        Me.CMDGV_Voc = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.安排年休NToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.安排培训RToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.删除安排DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.DGVDriversSeq, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVYearVocDetial, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CMDGV_Voc.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1099, 758)
        Me.SplitContainer1.SplitterDistance = 139
        Me.SplitContainer1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ProgressBar1)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.Btn_DeleteExist)
        Me.GroupBox1.Controls.Add(Me.Btn_Save)
        Me.GroupBox1.Controls.Add(Me.Btn_CheckExist)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.BtnSeqSetting)
        Me.GroupBox1.Controls.Add(Me.CmbLine)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1099, 139)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "年假安排规则设置"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(363, 90)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(701, 18)
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.TabIndex = 1
        Me.ProgressBar1.Visible = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.EndTimePicker)
        Me.GroupBox2.Controls.Add(Me.StartTimePicker)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(204, 20)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(436, 43)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 12)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "起始时间:"
        '
        'EndTimePicker
        '
        Me.EndTimePicker.Location = New System.Drawing.Point(290, 13)
        Me.EndTimePicker.Name = "EndTimePicker"
        Me.EndTimePicker.Size = New System.Drawing.Size(135, 21)
        Me.EndTimePicker.TabIndex = 5
        '
        'StartTimePicker
        '
        Me.StartTimePicker.Location = New System.Drawing.Point(69, 13)
        Me.StartTimePicker.Name = "StartTimePicker"
        Me.StartTimePicker.Size = New System.Drawing.Size(135, 21)
        Me.StartTimePicker.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(227, 17)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 12)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "结束时间:"
        '
        'Btn_DeleteExist
        '
        Me.Btn_DeleteExist.Image = CType(resources.GetObject("Btn_DeleteExist.Image"), System.Drawing.Image)
        Me.Btn_DeleteExist.Location = New System.Drawing.Point(803, 27)
        Me.Btn_DeleteExist.Name = "Btn_DeleteExist"
        Me.Btn_DeleteExist.Size = New System.Drawing.Size(121, 36)
        Me.Btn_DeleteExist.TabIndex = 3
        Me.Btn_DeleteExist.Text = "删除休假(&C)"
        Me.Btn_DeleteExist.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Btn_DeleteExist.UseVisualStyleBackColor = True
        '
        'Btn_Save
        '
        Me.Btn_Save.Image = CType(resources.GetObject("Btn_Save.Image"), System.Drawing.Image)
        Me.Btn_Save.Location = New System.Drawing.Point(258, 75)
        Me.Btn_Save.Name = "Btn_Save"
        Me.Btn_Save.Size = New System.Drawing.Size(95, 45)
        Me.Btn_Save.TabIndex = 0
        Me.Btn_Save.Text = "保存(&S)"
        Me.Btn_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Btn_Save.UseVisualStyleBackColor = True
        '
        'Btn_CheckExist
        '
        Me.Btn_CheckExist.Image = CType(resources.GetObject("Btn_CheckExist.Image"), System.Drawing.Image)
        Me.Btn_CheckExist.Location = New System.Drawing.Point(662, 27)
        Me.Btn_CheckExist.Name = "Btn_CheckExist"
        Me.Btn_CheckExist.Size = New System.Drawing.Size(135, 36)
        Me.Btn_CheckExist.TabIndex = 3
        Me.Btn_CheckExist.Text = "查看休假计划(&C)"
        Me.Btn_CheckExist.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Btn_CheckExist.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(108, 75)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(114, 45)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "安排年假(&A)"
        Me.Button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BtnSeqSetting
        '
        Me.BtnSeqSetting.Image = CType(resources.GetObject("BtnSeqSetting.Image"), System.Drawing.Image)
        Me.BtnSeqSetting.Location = New System.Drawing.Point(14, 75)
        Me.BtnSeqSetting.Name = "BtnSeqSetting"
        Me.BtnSeqSetting.Size = New System.Drawing.Size(88, 45)
        Me.BtnSeqSetting.TabIndex = 3
        Me.BtnSeqSetting.Text = "设置(&S)"
        Me.BtnSeqSetting.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnSeqSetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.BtnSeqSetting.UseVisualStyleBackColor = True
        '
        'CmbLine
        '
        Me.CmbLine.FormattingEnabled = True
        Me.CmbLine.Location = New System.Drawing.Point(53, 34)
        Me.CmbLine.Name = "CmbLine"
        Me.CmbLine.Size = New System.Drawing.Size(109, 20)
        Me.CmbLine.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 12)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "线路:"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.DGVDriversSeq)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.DGVYearVocDetial)
        Me.SplitContainer2.Size = New System.Drawing.Size(1099, 615)
        Me.SplitContainer2.SplitterDistance = 254
        Me.SplitContainer2.TabIndex = 1
        '
        'DGVDriversSeq
        '
        Me.DGVDriversSeq.AllowUserToAddRows = False
        Me.DGVDriversSeq.AllowUserToDeleteRows = False
        Me.DGVDriversSeq.BackgroundColor = System.Drawing.Color.White
        Me.DGVDriversSeq.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVDriversSeq.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.顺序, Me.组号, Me.司机姓名})
        Me.DGVDriversSeq.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVDriversSeq.Location = New System.Drawing.Point(0, 0)
        Me.DGVDriversSeq.Name = "DGVDriversSeq"
        Me.DGVDriversSeq.ReadOnly = True
        Me.DGVDriversSeq.RowHeadersVisible = False
        Me.DGVDriversSeq.RowTemplate.Height = 23
        Me.DGVDriversSeq.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVDriversSeq.Size = New System.Drawing.Size(254, 615)
        Me.DGVDriversSeq.TabIndex = 0
        '
        '顺序
        '
        Me.顺序.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.顺序.HeaderText = "顺序"
        Me.顺序.Name = "顺序"
        Me.顺序.ReadOnly = True
        Me.顺序.Width = 54
        '
        '组号
        '
        Me.组号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
        Me.组号.HeaderText = "组号"
        Me.组号.Name = "组号"
        Me.组号.ReadOnly = True
        Me.组号.Width = 54
        '
        '司机姓名
        '
        Me.司机姓名.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.司机姓名.HeaderText = "司机姓名"
        Me.司机姓名.Name = "司机姓名"
        Me.司机姓名.ReadOnly = True
        '
        'DGVYearVocDetial
        '
        Me.DGVYearVocDetial.AllowUserToAddRows = False
        Me.DGVYearVocDetial.AllowUserToDeleteRows = False
        Me.DGVYearVocDetial.AllowUserToResizeColumns = False
        Me.DGVYearVocDetial.AllowUserToResizeRows = False
        Me.DGVYearVocDetial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVYearVocDetial.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DGVYearVocDetial.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVYearVocDetial.Location = New System.Drawing.Point(0, 0)
        Me.DGVYearVocDetial.Name = "DGVYearVocDetial"
        Me.DGVYearVocDetial.ReadOnly = True
        Me.DGVYearVocDetial.RowHeadersVisible = False
        Me.DGVYearVocDetial.RowTemplate.Height = 23
        Me.DGVYearVocDetial.Size = New System.Drawing.Size(841, 615)
        Me.DGVYearVocDetial.TabIndex = 0
        '
        'CMDGV_Voc
        '
        Me.CMDGV_Voc.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.安排年休NToolStripMenuItem, Me.安排培训RToolStripMenuItem, Me.删除安排DToolStripMenuItem})
        Me.CMDGV_Voc.Name = "CMDGV_Voc"
        Me.CMDGV_Voc.Size = New System.Drawing.Size(143, 70)
        '
        '安排年休NToolStripMenuItem
        '
        Me.安排年休NToolStripMenuItem.Image = CType(resources.GetObject("安排年休NToolStripMenuItem.Image"), System.Drawing.Image)
        Me.安排年休NToolStripMenuItem.Name = "安排年休NToolStripMenuItem"
        Me.安排年休NToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.安排年休NToolStripMenuItem.Text = "安排年假(&N)"
        '
        '安排培训RToolStripMenuItem
        '
        Me.安排培训RToolStripMenuItem.Name = "安排培训RToolStripMenuItem"
        Me.安排培训RToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.安排培训RToolStripMenuItem.Text = "安排培训(&R)"
        '
        '删除安排DToolStripMenuItem
        '
        Me.删除安排DToolStripMenuItem.Name = "删除安排DToolStripMenuItem"
        Me.删除安排DToolStripMenuItem.Size = New System.Drawing.Size(142, 22)
        Me.删除安排DToolStripMenuItem.Text = "删除安排(&D)"
        '
        'FrmYearVocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1099, 758)
        Me.Controls.Add(Me.SplitContainer1)
        Me.KeyPreview = True
        Me.Name = "FrmYearVocation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "年假安排"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.DGVDriversSeq, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVYearVocDetial, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CMDGV_Voc.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents DGVYearVocDetial As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents BtnSeqSetting As System.Windows.Forms.Button
    Friend WithEvents CmbLine As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents DGVDriversSeq As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents EndTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents StartTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents 顺序 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 组号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 司机姓名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Btn_Save As System.Windows.Forms.Button
    Friend WithEvents CMDGV_Voc As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 安排年休NToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Btn_CheckExist As System.Windows.Forms.Button
    Friend WithEvents Btn_DeleteExist As System.Windows.Forms.Button
    Friend WithEvents 安排培训RToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 删除安排DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
