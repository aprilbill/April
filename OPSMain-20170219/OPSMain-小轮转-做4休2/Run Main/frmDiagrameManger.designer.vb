<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDiagrameManger
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
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.btnEditName = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.cmbStyle = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbLine = New System.Windows.Forms.ComboBox()
        Me.线路 = New System.Windows.Forms.Label()
        Me.Dgv = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.Dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(712, 22)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(110, 23)
        Me.btnDelete.TabIndex = 79
        Me.btnDelete.Text = "删除该运行图(&D)"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Location = New System.Drawing.Point(842, 22)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 81
        Me.btnExit.Text = "退出(&E)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnEditName
        '
        Me.btnEditName.Location = New System.Drawing.Point(568, 22)
        Me.btnEditName.Name = "btnEditName"
        Me.btnEditName.Size = New System.Drawing.Size(125, 23)
        Me.btnEditName.TabIndex = 82
        Me.btnEditName.Text = "修改运行图名称(&E)"
        Me.btnEditName.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.btnDelete)
        Me.Panel1.Controls.Add(Me.btnEditName)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 563)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(929, 61)
        Me.Panel1.TabIndex = 84
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnApply)
        Me.GroupBox1.Controls.Add(Me.cmbStyle)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbLine)
        Me.GroupBox1.Controls.Add(Me.线路)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(443, 49)
        Me.GroupBox1.TabIndex = 86
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "筛选"
        '
        'btnApply
        '
        Me.btnApply.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnApply.Location = New System.Drawing.Point(362, 18)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(75, 23)
        Me.btnApply.TabIndex = 88
        Me.btnApply.Text = "应用"
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'cmbStyle
        '
        Me.cmbStyle.FormattingEnabled = True
        Me.cmbStyle.Items.AddRange(New Object() {"全部"})
        Me.cmbStyle.Location = New System.Drawing.Point(221, 20)
        Me.cmbStyle.Name = "cmbStyle"
        Me.cmbStyle.Size = New System.Drawing.Size(121, 20)
        Me.cmbStyle.TabIndex = 86
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(180, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 12)
        Me.Label1.TabIndex = 87
        Me.Label1.Text = "类型:"
        '
        'cmbLine
        '
        Me.cmbLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLine.FormattingEnabled = True
        Me.cmbLine.Location = New System.Drawing.Point(48, 19)
        Me.cmbLine.Name = "cmbLine"
        Me.cmbLine.Size = New System.Drawing.Size(121, 20)
        Me.cmbLine.TabIndex = 84
        '
        '线路
        '
        Me.线路.AutoSize = True
        Me.线路.Location = New System.Drawing.Point(7, 22)
        Me.线路.Name = "线路"
        Me.线路.Size = New System.Drawing.Size(35, 12)
        Me.线路.TabIndex = 85
        Me.线路.Text = "线路:"
        '
        'Dgv
        '
        Me.Dgv.AllowUserToAddRows = False
        Me.Dgv.AllowUserToDeleteRows = False
        Me.Dgv.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Dgv.ColumnHeadersHeight = 25
        Me.Dgv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Dgv.Location = New System.Drawing.Point(0, 0)
        Me.Dgv.Name = "Dgv"
        Me.Dgv.RowHeadersWidth = 30
        Me.Dgv.RowTemplate.Height = 23
        Me.Dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Dgv.Size = New System.Drawing.Size(929, 563)
        Me.Dgv.TabIndex = 85
        '
        'frmDiagrameManger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(929, 624)
        Me.Controls.Add(Me.Dgv)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmDiagrameManger"
        Me.Text = "运行图管理"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.Dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnEditName As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Dgv As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbLine As System.Windows.Forms.ComboBox
    Friend WithEvents 线路 As System.Windows.Forms.Label
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents cmbStyle As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
