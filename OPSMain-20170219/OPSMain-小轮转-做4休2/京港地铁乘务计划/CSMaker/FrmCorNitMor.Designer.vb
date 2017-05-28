<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCorNitMor
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
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.TabControlMain = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DGVMain = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.夜班任务 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.输出编号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.出勤 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.退勤 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.公里 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.工时 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.夜班出勤 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.退勤地点 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.早班任务 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.输出编号2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.出勤2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.退勤2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.公里2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.工时2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.退勤地点2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.早班退勤 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.总工时 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.总公里 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DGV_UnAssignInfo = New System.Windows.Forms.DataGridView()
        Me.序号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.编号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.输出编号3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.班种 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.出勤3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.出勤地点 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.退勤3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.退勤地点3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.公里3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.工时3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BtnAssign = New System.Windows.Forms.Button()
        Me.Btn_Cancel = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TabControlMain.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DGVMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DGV_UnAssignInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button5)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnAssign)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Btn_Cancel)
        Me.SplitContainer1.Size = New System.Drawing.Size(815, 621)
        Me.SplitContainer1.SplitterDistance = 571
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.TabControlMain)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer2.Size = New System.Drawing.Size(815, 571)
        Me.SplitContainer2.SplitterDistance = 420
        Me.SplitContainer2.TabIndex = 0
        '
        'TabControlMain
        '
        Me.TabControlMain.Controls.Add(Me.TabPage1)
        Me.TabControlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlMain.Location = New System.Drawing.Point(0, 0)
        Me.TabControlMain.Name = "TabControlMain"
        Me.TabControlMain.SelectedIndex = 0
        Me.TabControlMain.Size = New System.Drawing.Size(815, 420)
        Me.TabControlMain.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(807, 394)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "夜早班衔接"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.DGVMain)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(801, 388)
        Me.Panel1.TabIndex = 2
        '
        'DGVMain
        '
        Me.DGVMain.AllowUserToAddRows = False
        Me.DGVMain.AllowUserToDeleteRows = False
        Me.DGVMain.BackgroundColor = System.Drawing.Color.Silver
        Me.DGVMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.夜班任务, Me.输出编号, Me.出勤, Me.退勤, Me.公里, Me.工时, Me.夜班出勤, Me.退勤地点, Me.早班任务, Me.输出编号2, Me.出勤2, Me.退勤2, Me.公里2, Me.工时2, Me.退勤地点2, Me.早班退勤, Me.总工时, Me.总公里})
        Me.DGVMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVMain.Location = New System.Drawing.Point(0, 0)
        Me.DGVMain.Name = "DGVMain"
        Me.DGVMain.ReadOnly = True
        Me.DGVMain.RowTemplate.Height = 23
        Me.DGVMain.Size = New System.Drawing.Size(801, 388)
        Me.DGVMain.TabIndex = 1
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewTextBoxColumn1.HeaderText = "序号"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 54
        '
        '夜班任务
        '
        Me.夜班任务.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.夜班任务.HeaderText = "夜班任务"
        Me.夜班任务.Name = "夜班任务"
        Me.夜班任务.ReadOnly = True
        Me.夜班任务.Width = 78
        '
        '输出编号
        '
        Me.输出编号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.输出编号.DefaultCellStyle = DataGridViewCellStyle7
        Me.输出编号.HeaderText = "输出编号"
        Me.输出编号.Name = "输出编号"
        Me.输出编号.ReadOnly = True
        Me.输出编号.Width = 78
        '
        '出勤
        '
        Me.出勤.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.出勤.HeaderText = "出勤"
        Me.出勤.Name = "出勤"
        Me.出勤.ReadOnly = True
        Me.出勤.Width = 54
        '
        '退勤
        '
        Me.退勤.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.退勤.HeaderText = "退勤"
        Me.退勤.Name = "退勤"
        Me.退勤.ReadOnly = True
        Me.退勤.Width = 54
        '
        '公里
        '
        Me.公里.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.公里.HeaderText = "公里"
        Me.公里.Name = "公里"
        Me.公里.ReadOnly = True
        Me.公里.Width = 54
        '
        '工时
        '
        Me.工时.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.工时.HeaderText = "工时"
        Me.工时.Name = "工时"
        Me.工时.ReadOnly = True
        Me.工时.Width = 54
        '
        '夜班出勤
        '
        Me.夜班出勤.HeaderText = "出勤地点"
        Me.夜班出勤.Name = "夜班出勤"
        Me.夜班出勤.ReadOnly = True
        '
        '退勤地点
        '
        Me.退勤地点.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.退勤地点.HeaderText = "退勤地点"
        Me.退勤地点.Name = "退勤地点"
        Me.退勤地点.ReadOnly = True
        Me.退勤地点.Width = 78
        '
        '早班任务
        '
        Me.早班任务.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.早班任务.DefaultCellStyle = DataGridViewCellStyle8
        Me.早班任务.HeaderText = "早班任务"
        Me.早班任务.Name = "早班任务"
        Me.早班任务.ReadOnly = True
        Me.早班任务.Width = 78
        '
        '输出编号2
        '
        Me.输出编号2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.输出编号2.DefaultCellStyle = DataGridViewCellStyle9
        Me.输出编号2.HeaderText = "输出编号"
        Me.输出编号2.Name = "输出编号2"
        Me.输出编号2.ReadOnly = True
        Me.输出编号2.Width = 78
        '
        '出勤2
        '
        Me.出勤2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.出勤2.HeaderText = "出勤"
        Me.出勤2.Name = "出勤2"
        Me.出勤2.ReadOnly = True
        Me.出勤2.Width = 54
        '
        '退勤2
        '
        Me.退勤2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.退勤2.HeaderText = "退勤"
        Me.退勤2.Name = "退勤2"
        Me.退勤2.ReadOnly = True
        Me.退勤2.Width = 54
        '
        '公里2
        '
        Me.公里2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.公里2.HeaderText = "公里"
        Me.公里2.Name = "公里2"
        Me.公里2.ReadOnly = True
        Me.公里2.Width = 54
        '
        '工时2
        '
        Me.工时2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.工时2.HeaderText = "工时"
        Me.工时2.Name = "工时2"
        Me.工时2.ReadOnly = True
        Me.工时2.Width = 54
        '
        '退勤地点2
        '
        Me.退勤地点2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.退勤地点2.HeaderText = "出勤地点"
        Me.退勤地点2.Name = "退勤地点2"
        Me.退勤地点2.ReadOnly = True
        Me.退勤地点2.Width = 78
        '
        '早班退勤
        '
        Me.早班退勤.HeaderText = "退勤地点"
        Me.早班退勤.Name = "早班退勤"
        Me.早班退勤.ReadOnly = True
        '
        '总工时
        '
        Me.总工时.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.总工时.HeaderText = "总工时"
        Me.总工时.Name = "总工时"
        Me.总工时.ReadOnly = True
        Me.总工时.Width = 66
        '
        '总公里
        '
        Me.总公里.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.总公里.HeaderText = "总公里"
        Me.总公里.Name = "总公里"
        Me.总公里.ReadOnly = True
        Me.总公里.Width = 66
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DGV_UnAssignInfo)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(815, 147)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "未连接的夜早班任务"
        '
        'DGV_UnAssignInfo
        '
        Me.DGV_UnAssignInfo.AllowUserToAddRows = False
        Me.DGV_UnAssignInfo.AllowUserToDeleteRows = False
        Me.DGV_UnAssignInfo.BackgroundColor = System.Drawing.Color.White
        Me.DGV_UnAssignInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_UnAssignInfo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.序号, Me.编号, Me.输出编号3, Me.班种, Me.出勤3, Me.出勤地点, Me.退勤3, Me.退勤地点3, Me.公里3, Me.工时3})
        Me.DGV_UnAssignInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGV_UnAssignInfo.Location = New System.Drawing.Point(3, 17)
        Me.DGV_UnAssignInfo.Name = "DGV_UnAssignInfo"
        Me.DGV_UnAssignInfo.ReadOnly = True
        Me.DGV_UnAssignInfo.RowHeadersVisible = False
        Me.DGV_UnAssignInfo.RowTemplate.Height = 23
        Me.DGV_UnAssignInfo.Size = New System.Drawing.Size(809, 127)
        Me.DGV_UnAssignInfo.TabIndex = 0
        '
        '序号
        '
        Me.序号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.序号.HeaderText = "序号"
        Me.序号.Name = "序号"
        Me.序号.ReadOnly = True
        Me.序号.Width = 54
        '
        '编号
        '
        Me.编号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.编号.HeaderText = "编号"
        Me.编号.Name = "编号"
        Me.编号.ReadOnly = True
        Me.编号.Width = 54
        '
        '输出编号3
        '
        Me.输出编号3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.输出编号3.HeaderText = "输出编号"
        Me.输出编号3.Name = "输出编号3"
        Me.输出编号3.ReadOnly = True
        Me.输出编号3.Width = 78
        '
        '班种
        '
        Me.班种.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.班种.HeaderText = "班种"
        Me.班种.Name = "班种"
        Me.班种.ReadOnly = True
        Me.班种.Width = 54
        '
        '出勤3
        '
        Me.出勤3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.出勤3.HeaderText = "出勤"
        Me.出勤3.Name = "出勤3"
        Me.出勤3.ReadOnly = True
        Me.出勤3.Width = 54
        '
        '出勤地点
        '
        Me.出勤地点.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.出勤地点.HeaderText = "出勤地点"
        Me.出勤地点.Name = "出勤地点"
        Me.出勤地点.ReadOnly = True
        Me.出勤地点.Width = 78
        '
        '退勤3
        '
        Me.退勤3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.退勤3.HeaderText = "退勤"
        Me.退勤3.Name = "退勤3"
        Me.退勤3.ReadOnly = True
        Me.退勤3.Width = 54
        '
        '退勤地点3
        '
        Me.退勤地点3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.退勤地点3.HeaderText = "退勤地点"
        Me.退勤地点3.Name = "退勤地点3"
        Me.退勤地点3.ReadOnly = True
        Me.退勤地点3.Width = 78
        '
        '公里3
        '
        Me.公里3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.公里3.HeaderText = "公里"
        Me.公里3.Name = "公里3"
        Me.公里3.ReadOnly = True
        Me.公里3.Width = 54
        '
        '工时3
        '
        Me.工时3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.工时3.HeaderText = "工时"
        Me.工时3.Name = "工时3"
        Me.工时3.ReadOnly = True
        Me.工时3.Width = 54
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(623, 11)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(91, 23)
        Me.Button4.TabIndex = 3
        Me.Button4.Text = "保存"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(505, 11)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(108, 23)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "导入到Excel"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(387, 11)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(108, 23)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "导出到Excel"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(262, 11)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(115, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "清空(&C)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BtnAssign
        '
        Me.BtnAssign.Location = New System.Drawing.Point(137, 11)
        Me.BtnAssign.Name = "BtnAssign"
        Me.BtnAssign.Size = New System.Drawing.Size(115, 23)
        Me.BtnAssign.TabIndex = 1
        Me.BtnAssign.Text = "自动衔接(&A)"
        Me.BtnAssign.UseVisualStyleBackColor = True
        '
        'Btn_Cancel
        '
        Me.Btn_Cancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_Cancel.Location = New System.Drawing.Point(724, 11)
        Me.Btn_Cancel.Name = "Btn_Cancel"
        Me.Btn_Cancel.Size = New System.Drawing.Size(75, 23)
        Me.Btn_Cancel.TabIndex = 0
        Me.Btn_Cancel.Text = "退出(&E)"
        Me.Btn_Cancel.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(12, 11)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(115, 23)
        Me.Button5.TabIndex = 4
        Me.Button5.Text = "异站衔接设置"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'FrmCorNitMor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(815, 621)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmCorNitMor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "夜早班衔接"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.TabControlMain.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.DGVMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.DGV_UnAssignInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents DGVMain As System.Windows.Forms.DataGridView
    Friend WithEvents DGV_UnAssignInfo As System.Windows.Forms.DataGridView
    Friend WithEvents Btn_Cancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents 序号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 编号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 输出编号3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 班种 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 出勤3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 出勤地点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 退勤3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 退勤地点3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 公里3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 工时3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabControlMain As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents BtnAssign As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 夜班任务 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 输出编号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 出勤 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 退勤 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 公里 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 工时 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 夜班出勤 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 退勤地点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 早班任务 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 输出编号2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 出勤2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 退勤2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 公里2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 工时2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 退勤地点2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 早班退勤 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 总工时 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 总公里 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
End Class
