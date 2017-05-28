<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAssignDinner
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.序号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.任务号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.是否用餐 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.用餐类型 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.用餐车次 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.开始时间 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.接车车次 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.接车时间 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.用餐地点 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer1.Size = New System.Drawing.Size(889, 475)
        Me.SplitContainer1.SplitterDistance = 431
        Me.SplitContainer1.TabIndex = 0
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.序号, Me.任务号, Me.是否用餐, Me.用餐类型, Me.用餐车次, Me.开始时间, Me.接车车次, Me.接车时间, Me.用餐地点})
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(889, 431)
        Me.DataGridView1.TabIndex = 0
        '
        '序号
        '
        Me.序号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.序号.HeaderText = "序号"
        Me.序号.Name = "序号"
        Me.序号.ReadOnly = True
        Me.序号.Width = 54
        '
        '任务号
        '
        Me.任务号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.任务号.HeaderText = "任务号"
        Me.任务号.Name = "任务号"
        Me.任务号.ReadOnly = True
        Me.任务号.Width = 66
        '
        '是否用餐
        '
        Me.是否用餐.HeaderText = "是否用餐"
        Me.是否用餐.Name = "是否用餐"
        Me.是否用餐.ReadOnly = True
        '
        '用餐类型
        '
        Me.用餐类型.HeaderText = "用餐类型"
        Me.用餐类型.Name = "用餐类型"
        Me.用餐类型.ReadOnly = True
        '
        '用餐车次
        '
        Me.用餐车次.HeaderText = "用餐车次"
        Me.用餐车次.Name = "用餐车次"
        Me.用餐车次.ReadOnly = True
        '
        '开始时间
        '
        Me.开始时间.HeaderText = "开始时间"
        Me.开始时间.Name = "开始时间"
        Me.开始时间.ReadOnly = True
        '
        '接车车次
        '
        Me.接车车次.HeaderText = "接车车次"
        Me.接车车次.Name = "接车车次"
        Me.接车车次.ReadOnly = True
        '
        '接车时间
        '
        Me.接车时间.HeaderText = "接车时间"
        Me.接车时间.Name = "接车时间"
        Me.接车时间.ReadOnly = True
        '
        '用餐地点
        '
        Me.用餐地点.HeaderText = "用餐地点"
        Me.用餐地点.Name = "用餐地点"
        Me.用餐地点.ReadOnly = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(12, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(137, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "注：双击可改变用餐设置"
        '
        'Button1
        '
        Me.Button1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(802, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "退出(&E)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'FrmAssignDinner
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(889, 475)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmAssignDinner"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "用餐设置"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents 序号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 任务号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 是否用餐 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 用餐类型 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 用餐车次 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 开始时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 接车车次 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 接车时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 用餐地点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
