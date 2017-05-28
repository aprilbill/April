<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSetDinner
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.编号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.车次 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.接车时间 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.接车地点 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.到达时间 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.到达地点 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.设为用餐车次ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.设为出勤后用餐ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.取消用餐ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Button3 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "任务号:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("黑体", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.Location = New System.Drawing.Point(79, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 19)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Label2"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(311, 394)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "确定(&O)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(392, 393)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "取消(&C)"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.编号, Me.车次, Me.接车时间, Me.接车地点, Me.到达时间, Me.到达地点})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 46)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(459, 249)
        Me.DataGridView1.TabIndex = 3
        '
        '编号
        '
        Me.编号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.编号.HeaderText = "编号"
        Me.编号.Name = "编号"
        Me.编号.ReadOnly = True
        Me.编号.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.编号.Width = 35
        '
        '车次
        '
        Me.车次.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.车次.HeaderText = "车次"
        Me.车次.Name = "车次"
        Me.车次.ReadOnly = True
        Me.车次.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.车次.Width = 35
        '
        '接车时间
        '
        Me.接车时间.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.接车时间.HeaderText = "接车时间"
        Me.接车时间.Name = "接车时间"
        Me.接车时间.ReadOnly = True
        Me.接车时间.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.接车时间.Width = 59
        '
        '接车地点
        '
        Me.接车地点.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.接车地点.HeaderText = "接车地点"
        Me.接车地点.Name = "接车地点"
        Me.接车地点.ReadOnly = True
        Me.接车地点.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.接车地点.Width = 59
        '
        '到达时间
        '
        Me.到达时间.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.到达时间.HeaderText = "到达时间"
        Me.到达时间.Name = "到达时间"
        Me.到达时间.ReadOnly = True
        Me.到达时间.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.到达时间.Width = 59
        '
        '到达地点
        '
        Me.到达地点.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.到达地点.HeaderText = "到达地点"
        Me.到达地点.Name = "到达地点"
        Me.到达地点.ReadOnly = True
        Me.到达地点.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.到达地点.Width = 59
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 302)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(459, 85)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "用餐信息"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(16, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(269, 12)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "班中用餐/用餐车次/开始时间/接车车次/接车时间"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.设为用餐车次ToolStripMenuItem, Me.设为出勤后用餐ToolStripMenuItem, Me.取消用餐ToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(173, 70)
        '
        '设为用餐车次ToolStripMenuItem
        '
        Me.设为用餐车次ToolStripMenuItem.Name = "设为用餐车次ToolStripMenuItem"
        Me.设为用餐车次ToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.设为用餐车次ToolStripMenuItem.Text = "设为班中用餐车次"
        '
        '设为出勤后用餐ToolStripMenuItem
        '
        Me.设为出勤后用餐ToolStripMenuItem.Name = "设为出勤后用餐ToolStripMenuItem"
        Me.设为出勤后用餐ToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.设为出勤后用餐ToolStripMenuItem.Text = "设为退勤后用餐"
        '
        '取消用餐ToolStripMenuItem
        '
        Me.取消用餐ToolStripMenuItem.Name = "取消用餐ToolStripMenuItem"
        Me.取消用餐ToolStripMenuItem.Size = New System.Drawing.Size(172, 22)
        Me.取消用餐ToolStripMenuItem.Text = "取消用餐"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(359, 17)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(112, 23)
        Me.Button3.TabIndex = 7
        Me.Button3.Text = "班前用餐"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'FrmSetDinner
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(484, 425)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSetDinner"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "设置用餐"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 设为用餐车次ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 设为出勤后用餐ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 取消用餐ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents 编号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 车次 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 接车时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 接车地点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 到达时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 到达地点 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
