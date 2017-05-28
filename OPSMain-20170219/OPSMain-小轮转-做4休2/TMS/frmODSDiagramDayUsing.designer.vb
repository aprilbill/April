<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmODSDiagramDayUsing
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
        Me.Datetime = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.dgv = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CurStyle = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmbVersion = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.cmbStyle = New System.Windows.Forms.ComboBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbName = New System.Windows.Forms.ComboBox
        Me.btnMouth = New System.Windows.Forms.Button
        Me.btnOpen = New System.Windows.Forms.Button
        Me.btnApply = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.cmbLineName = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Datetime
        '
        Me.Datetime.Location = New System.Drawing.Point(84, 12)
        Me.Datetime.Name = "Datetime"
        Me.Datetime.Size = New System.Drawing.Size(134, 21)
        Me.Datetime.TabIndex = 80
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 81
        Me.Label1.Text = "选择时间："
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(428, 383)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 82
        Me.btnExit.Text = "退出(&E)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgv.ColumnHeadersHeight = 25
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9, Me.CurStyle, Me.cmbVersion})
        Me.dgv.Location = New System.Drawing.Point(12, 16)
        Me.dgv.MultiSelect = False
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersWidth = 30
        Me.dgv.RowTemplate.Height = 23
        Me.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgv.Size = New System.Drawing.Size(468, 257)
        Me.dgv.TabIndex = 79
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "序号"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn8.Width = 40
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "线路名称"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'CurStyle
        '
        Me.CurStyle.HeaderText = "运行图类型"
        Me.CurStyle.Name = "CurStyle"
        Me.CurStyle.ReadOnly = True
        Me.CurStyle.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.CurStyle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.CurStyle.Width = 120
        '
        'cmbVersion
        '
        Me.cmbVersion.HeaderText = "当前版本"
        Me.cmbVersion.Name = "cmbVersion"
        Me.cmbVersion.ReadOnly = True
        Me.cmbVersion.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.cmbVersion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.cmbVersion.Width = 170
        '
        'cmbStyle
        '
        Me.cmbStyle.FormattingEnabled = True
        Me.cmbStyle.Location = New System.Drawing.Point(355, 279)
        Me.cmbStyle.Name = "cmbStyle"
        Me.cmbStyle.Size = New System.Drawing.Size(125, 20)
        Me.cmbStyle.TabIndex = 84
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(12, 383)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(99, 23)
        Me.btnSave.TabIndex = 83
        Me.btnSave.Text = "保存一天(&S)"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(272, 284)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 12)
        Me.Label2.TabIndex = 85
        Me.Label2.Text = "运行图类型："
        '
        'cmbName
        '
        Me.cmbName.FormattingEnabled = True
        Me.cmbName.Location = New System.Drawing.Point(90, 310)
        Me.cmbName.Name = "cmbName"
        Me.cmbName.Size = New System.Drawing.Size(255, 20)
        Me.cmbName.TabIndex = 89
        '
        'btnMouth
        '
        Me.btnMouth.Location = New System.Drawing.Point(119, 383)
        Me.btnMouth.Name = "btnMouth"
        Me.btnMouth.Size = New System.Drawing.Size(164, 23)
        Me.btnMouth.TabIndex = 87
        Me.btnMouth.Text = "保存从该天起的一个月(&T)"
        Me.btnMouth.UseVisualStyleBackColor = True
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(289, 383)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(125, 23)
        Me.btnOpen.TabIndex = 88
        Me.btnOpen.Text = "打开该运行图(&O)"
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(355, 308)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(125, 23)
        Me.btnApply.TabIndex = 86
        Me.btnApply.Text = "应用"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 313)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 12)
        Me.Label3.TabIndex = 90
        Me.Label3.Text = "运行图名称："
        '
        'cmbLineName
        '
        Me.cmbLineName.FormattingEnabled = True
        Me.cmbLineName.Location = New System.Drawing.Point(90, 279)
        Me.cmbLineName.Name = "cmbLineName"
        Me.cmbLineName.Size = New System.Drawing.Size(140, 20)
        Me.cmbLineName.TabIndex = 91
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 284)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 92
        Me.Label4.Text = "线路名称："
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbLineName)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.btnApply)
        Me.GroupBox1.Controls.Add(Me.cmbName)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbStyle)
        Me.GroupBox1.Controls.Add(Me.dgv)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 32)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(491, 345)
        Me.GroupBox1.TabIndex = 91
        Me.GroupBox1.TabStop = False
        '
        'frmODSDiagramDayUsing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(511, 412)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Datetime)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.btnMouth)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmODSDiagramDayUsing"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "运行图日期版本管理"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Datetime As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents cmbStyle As System.Windows.Forms.ComboBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbName As System.Windows.Forms.ComboBox
    Friend WithEvents btnMouth As System.Windows.Forms.Button
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbLineName As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CurStyle As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmbVersion As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
