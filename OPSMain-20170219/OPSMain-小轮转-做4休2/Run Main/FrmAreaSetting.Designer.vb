<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAreaSetting
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
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DGVArea = New System.Windows.Forms.DataGridView()
        Me.序号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.线路名称 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.区域名称 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.出勤地点 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.适用班种 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.轮转制度 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.DGVArea, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnExit)
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnSave)
        Me.SplitContainer1.Size = New System.Drawing.Size(590, 474)
        Me.SplitContainer1.SplitterDistance = 426
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
        Me.SplitContainer2.Panel1.Controls.Add(Me.ComboBox1)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.DGVArea)
        Me.SplitContainer2.Size = New System.Drawing.Size(590, 426)
        Me.SplitContainer2.SplitterDistance = 37
        Me.SplitContainer2.TabIndex = 1
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(76, 7)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 20)
        Me.ComboBox1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "选择线路:"
        '
        'DGVArea
        '
        Me.DGVArea.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVArea.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.序号, Me.线路名称, Me.区域名称, Me.出勤地点, Me.适用班种, Me.轮转制度})
        Me.DGVArea.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVArea.Location = New System.Drawing.Point(0, 0)
        Me.DGVArea.MultiSelect = False
        Me.DGVArea.Name = "DGVArea"
        Me.DGVArea.RowTemplate.Height = 23
        Me.DGVArea.Size = New System.Drawing.Size(590, 385)
        Me.DGVArea.TabIndex = 0
        '
        '序号
        '
        Me.序号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.序号.HeaderText = "序号"
        Me.序号.Name = "序号"
        Me.序号.ReadOnly = True
        Me.序号.Width = 54
        '
        '线路名称
        '
        Me.线路名称.HeaderText = "线路名称"
        Me.线路名称.Name = "线路名称"
        Me.线路名称.ReadOnly = True
        '
        '区域名称
        '
        Me.区域名称.HeaderText = "区域名称"
        Me.区域名称.Name = "区域名称"
        '
        '出勤地点
        '
        Me.出勤地点.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.出勤地点.DefaultCellStyle = DataGridViewCellStyle1
        Me.出勤地点.HeaderText = "出勤地点"
        Me.出勤地点.Name = "出勤地点"
        Me.出勤地点.ReadOnly = True
        Me.出勤地点.Width = 78
        '
        '适用班种
        '
        Me.适用班种.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.适用班种.DefaultCellStyle = DataGridViewCellStyle2
        Me.适用班种.HeaderText = "适用班种"
        Me.适用班种.Name = "适用班种"
        Me.适用班种.ReadOnly = True
        Me.适用班种.Width = 78
        '
        '轮转制度
        '
        Me.轮转制度.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.轮转制度.HeaderText = "轮转制度"
        Me.轮转制度.Name = "轮转制度"
        Me.轮转制度.Width = 78
        '
        'BtnExit
        '
        Me.BtnExit.Location = New System.Drawing.Point(502, 10)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(75, 23)
        Me.BtnExit.TabIndex = 0
        Me.BtnExit.Text = "退出(&E)"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'BtnSave
        '
        Me.BtnSave.Location = New System.Drawing.Point(421, 10)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(75, 23)
        Me.BtnSave.TabIndex = 0
        Me.BtnSave.Text = "保存(&S)"
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Red
        Me.Label2.Location = New System.Drawing.Point(12, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "注意：必须包含主区域"
        '
        'FrmAreaSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(590, 474)
        Me.Controls.Add(Me.SplitContainer1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAreaSetting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "区域参数设置"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.DGVArea, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DGVArea As System.Windows.Forms.DataGridView
    Friend WithEvents BtnExit As System.Windows.Forms.Button
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents 序号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 线路名称 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 区域名称 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 出勤地点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 适用班种 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 轮转制度 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
