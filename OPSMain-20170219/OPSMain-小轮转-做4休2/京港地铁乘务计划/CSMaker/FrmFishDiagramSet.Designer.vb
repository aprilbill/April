<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFishDiagramSet
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DGVUpFormat = New System.Windows.Forms.DataGridView()
        Me.编号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.输出类型 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.适用车站 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.输出表头 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnDown1 = New System.Windows.Forms.Button()
        Me.BtnUp1 = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.DGVDownFormat = New System.Windows.Forms.DataGridView()
        Me.编号2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.输出类型2 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.适用车站2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.输出表头2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Btn_Cancel = New System.Windows.Forms.Button()
        Me.Btn_Save = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.BtnInput = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DGVUpFormat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.DGVDownFormat, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(536, 341)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(528, 315)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "上行输出"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DGVUpFormat)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnDown1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnUp1)
        Me.SplitContainer1.Size = New System.Drawing.Size(522, 309)
        Me.SplitContainer1.SplitterDistance = 269
        Me.SplitContainer1.TabIndex = 1
        '
        'DGVUpFormat
        '
        Me.DGVUpFormat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVUpFormat.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.编号, Me.输出类型, Me.适用车站, Me.输出表头})
        Me.DGVUpFormat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVUpFormat.Location = New System.Drawing.Point(0, 0)
        Me.DGVUpFormat.Name = "DGVUpFormat"
        Me.DGVUpFormat.RowTemplate.Height = 23
        Me.DGVUpFormat.Size = New System.Drawing.Size(522, 269)
        Me.DGVUpFormat.TabIndex = 0
        '
        '编号
        '
        Me.编号.HeaderText = "编号"
        Me.编号.Name = "编号"
        '
        '输出类型
        '
        Me.输出类型.HeaderText = "输出类型"
        Me.输出类型.Items.AddRange(New Object() {"时刻", "接车表号", "下车表号", "车次", "折返车次", "车底号"})
        Me.输出类型.Name = "输出类型"
        Me.输出类型.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.输出类型.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        '适用车站
        '
        Me.适用车站.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.适用车站.DefaultCellStyle = DataGridViewCellStyle7
        Me.适用车站.HeaderText = "适用车站"
        Me.适用车站.Name = "适用车站"
        Me.适用车站.ReadOnly = True
        Me.适用车站.Width = 78
        '
        '输出表头
        '
        Me.输出表头.HeaderText = "输出表头"
        Me.输出表头.Name = "输出表头"
        '
        'BtnDown1
        '
        Me.BtnDown1.Location = New System.Drawing.Point(90, 7)
        Me.BtnDown1.Name = "BtnDown1"
        Me.BtnDown1.Size = New System.Drawing.Size(75, 23)
        Me.BtnDown1.TabIndex = 0
        Me.BtnDown1.Text = "下移(&D)"
        Me.BtnDown1.UseVisualStyleBackColor = True
        '
        'BtnUp1
        '
        Me.BtnUp1.Location = New System.Drawing.Point(9, 7)
        Me.BtnUp1.Name = "BtnUp1"
        Me.BtnUp1.Size = New System.Drawing.Size(75, 23)
        Me.BtnUp1.TabIndex = 0
        Me.BtnUp1.Text = "上移(&U)"
        Me.BtnUp1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.SplitContainer2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(528, 315)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "下行输出"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.IsSplitterFixed = True
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.DGVDownFormat)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Button2)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Button3)
        Me.SplitContainer2.Size = New System.Drawing.Size(522, 309)
        Me.SplitContainer2.SplitterDistance = 268
        Me.SplitContainer2.TabIndex = 2
        '
        'DGVDownFormat
        '
        Me.DGVDownFormat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVDownFormat.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.编号2, Me.输出类型2, Me.适用车站2, Me.输出表头2})
        Me.DGVDownFormat.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVDownFormat.Location = New System.Drawing.Point(0, 0)
        Me.DGVDownFormat.Name = "DGVDownFormat"
        Me.DGVDownFormat.RowTemplate.Height = 23
        Me.DGVDownFormat.Size = New System.Drawing.Size(522, 268)
        Me.DGVDownFormat.TabIndex = 1
        '
        '编号2
        '
        Me.编号2.HeaderText = "编号"
        Me.编号2.Name = "编号2"
        '
        '输出类型2
        '
        Me.输出类型2.HeaderText = "输出类型"
        Me.输出类型2.Items.AddRange(New Object() {"时刻", "接车表号", "下车表号", "车次", "折返车次", "车底号"})
        Me.输出类型2.Name = "输出类型2"
        Me.输出类型2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.输出类型2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        '适用车站2
        '
        Me.适用车站2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.适用车站2.DefaultCellStyle = DataGridViewCellStyle8
        Me.适用车站2.HeaderText = "适用车站"
        Me.适用车站2.Name = "适用车站2"
        Me.适用车站2.ReadOnly = True
        Me.适用车站2.Width = 78
        '
        '输出表头2
        '
        Me.输出表头2.HeaderText = "输出表头"
        Me.输出表头2.Name = "输出表头2"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(89, 8)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "下移(&D)"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(8, 8)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 1
        Me.Button3.Text = "上移(&U)"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Btn_Cancel
        '
        Me.Btn_Cancel.Location = New System.Drawing.Point(473, 359)
        Me.Btn_Cancel.Name = "Btn_Cancel"
        Me.Btn_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Btn_Cancel.TabIndex = 1
        Me.Btn_Cancel.Text = "退出(&C)"
        Me.Btn_Cancel.UseVisualStyleBackColor = True
        '
        'Btn_Save
        '
        Me.Btn_Save.Location = New System.Drawing.Point(12, 359)
        Me.Btn_Save.Name = "Btn_Save"
        Me.Btn_Save.Size = New System.Drawing.Size(102, 23)
        Me.Btn_Save.TabIndex = 1
        Me.Btn_Save.Text = "设为默认值(&M)"
        Me.Btn_Save.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(365, 359)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(102, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "开始输出(&O)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(124, 361)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(233, 18)
        Me.ProgressBar1.TabIndex = 2
        Me.ProgressBar1.Visible = False
        '
        'BtnInput
        '
        Me.BtnInput.Location = New System.Drawing.Point(365, 401)
        Me.BtnInput.Name = "BtnInput"
        Me.BtnInput.Size = New System.Drawing.Size(102, 23)
        Me.BtnInput.TabIndex = 3
        Me.BtnInput.Text = "导入钓鱼图(&I)"
        Me.BtnInput.UseVisualStyleBackColor = True
        '
        'FrmFishDiagramSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(560, 394)
        Me.Controls.Add(Me.BtnInput)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Btn_Save)
        Me.Controls.Add(Me.Btn_Cancel)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "FrmFishDiagramSet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "钓鱼图输出设置"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DGVUpFormat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.DGVDownFormat, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents DGVUpFormat As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DGVDownFormat As System.Windows.Forms.DataGridView
    Friend WithEvents Btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents Btn_Save As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents 编号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 输出类型 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents 适用车站 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 输出表头 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 编号2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 输出类型2 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents 适用车站2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 输出表头2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents BtnDown1 As System.Windows.Forms.Button
    Friend WithEvents BtnUp1 As System.Windows.Forms.Button
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents BtnInput As System.Windows.Forms.Button
End Class
