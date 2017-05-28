<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSetAreainfo
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DGVMain = New System.Windows.Forms.DataGridView()
        Me.编号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.任务编号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.输出编号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.班种 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.出勤时间 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.出勤地点 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.退勤时间 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.退勤地点 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.工作时间 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.驾驶公里 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.所属区域 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnInput = New System.Windows.Forms.Button()
        Me.BtnOutPut = New System.Windows.Forms.Button()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DGVMain, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.DGVMain)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnInput)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnOutPut)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnExit)
        Me.SplitContainer1.Size = New System.Drawing.Size(932, 500)
        Me.SplitContainer1.SplitterDistance = 456
        Me.SplitContainer1.TabIndex = 1
        '
        'DGVMain
        '
        Me.DGVMain.AllowUserToAddRows = False
        Me.DGVMain.AllowUserToDeleteRows = False
        Me.DGVMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.编号, Me.任务编号, Me.输出编号, Me.班种, Me.出勤时间, Me.出勤地点, Me.退勤时间, Me.退勤地点, Me.工作时间, Me.驾驶公里, Me.所属区域})
        Me.DGVMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVMain.Location = New System.Drawing.Point(0, 0)
        Me.DGVMain.Name = "DGVMain"
        Me.DGVMain.RowTemplate.Height = 23
        Me.DGVMain.Size = New System.Drawing.Size(932, 456)
        Me.DGVMain.TabIndex = 0
        '
        '编号
        '
        Me.编号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.编号.HeaderText = "编号"
        Me.编号.Name = "编号"
        Me.编号.ReadOnly = True
        Me.编号.Width = 54
        '
        '任务编号
        '
        Me.任务编号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.任务编号.HeaderText = "任务编号"
        Me.任务编号.Name = "任务编号"
        Me.任务编号.ReadOnly = True
        Me.任务编号.Width = 78
        '
        '输出编号
        '
        Me.输出编号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.输出编号.DefaultCellStyle = DataGridViewCellStyle1
        Me.输出编号.HeaderText = "输出编号"
        Me.输出编号.Name = "输出编号"
        Me.输出编号.Width = 78
        '
        '班种
        '
        Me.班种.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.班种.HeaderText = "班种"
        Me.班种.Name = "班种"
        Me.班种.ReadOnly = True
        Me.班种.Width = 54
        '
        '出勤时间
        '
        Me.出勤时间.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.出勤时间.HeaderText = "出勤时间"
        Me.出勤时间.Name = "出勤时间"
        Me.出勤时间.ReadOnly = True
        Me.出勤时间.Width = 78
        '
        '出勤地点
        '
        Me.出勤地点.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.出勤地点.HeaderText = "出勤地点"
        Me.出勤地点.Name = "出勤地点"
        Me.出勤地点.ReadOnly = True
        Me.出勤地点.Width = 78
        '
        '退勤时间
        '
        Me.退勤时间.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.退勤时间.HeaderText = "退勤时间"
        Me.退勤时间.Name = "退勤时间"
        Me.退勤时间.ReadOnly = True
        Me.退勤时间.Width = 78
        '
        '退勤地点
        '
        Me.退勤地点.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.退勤地点.HeaderText = "退勤地点"
        Me.退勤地点.Name = "退勤地点"
        Me.退勤地点.ReadOnly = True
        Me.退勤地点.Width = 78
        '
        '工作时间
        '
        Me.工作时间.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.工作时间.HeaderText = "工作时间"
        Me.工作时间.Name = "工作时间"
        Me.工作时间.ReadOnly = True
        Me.工作时间.Width = 78
        '
        '驾驶公里
        '
        Me.驾驶公里.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.驾驶公里.HeaderText = "驾驶公里"
        Me.驾驶公里.Name = "驾驶公里"
        Me.驾驶公里.ReadOnly = True
        Me.驾驶公里.Width = 78
        '
        '所属区域
        '
        Me.所属区域.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.所属区域.DefaultCellStyle = DataGridViewCellStyle2
        Me.所属区域.HeaderText = "所属区域"
        Me.所属区域.Name = "所属区域"
        Me.所属区域.ReadOnly = True
        Me.所属区域.Width = 78
        '
        'BtnInput
        '
        Me.BtnInput.Location = New System.Drawing.Point(113, 8)
        Me.BtnInput.Name = "BtnInput"
        Me.BtnInput.Size = New System.Drawing.Size(174, 23)
        Me.BtnInput.TabIndex = 1
        Me.BtnInput.Text = "输出名称批量导入修改(&O)"
        Me.BtnInput.UseVisualStyleBackColor = True
        '
        'BtnOutPut
        '
        Me.BtnOutPut.Location = New System.Drawing.Point(12, 8)
        Me.BtnOutPut.Name = "BtnOutPut"
        Me.BtnOutPut.Size = New System.Drawing.Size(95, 23)
        Me.BtnOutPut.TabIndex = 1
        Me.BtnOutPut.Text = "信息导出(&O)"
        Me.BtnOutPut.UseVisualStyleBackColor = True
        '
        'BtnExit
        '
        Me.BtnExit.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnExit.Location = New System.Drawing.Point(845, 8)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(75, 23)
        Me.BtnExit.TabIndex = 0
        Me.BtnExit.Text = "退出(&E)"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'frmSetAreainfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(932, 500)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmSetAreainfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "概要信息设置"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DGVMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DGVMain As System.Windows.Forms.DataGridView
    Friend WithEvents BtnExit As System.Windows.Forms.Button
    Friend WithEvents 编号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 任务编号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 输出编号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 班种 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 出勤时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 出勤地点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 退勤时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 退勤地点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 工作时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 驾驶公里 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 所属区域 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BtnInput As System.Windows.Forms.Button
    Friend WithEvents BtnOutPut As System.Windows.Forms.Button
End Class
