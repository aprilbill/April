<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOpenPlan
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
        Me.DGVManager = New System.Windows.Forms.DataGridView()
        Me.日期 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.计划 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.乘务员数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.DTPEnd = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DTPStart = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Btn_Open = New System.Windows.Forms.Button()
        Me.Btn_Delete = New System.Windows.Forms.Button()
        Me.Btn_Cancle = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmbLine = New System.Windows.Forms.ComboBox()
        CType(Me.DGVManager, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'DGVManager
        '
        Me.DGVManager.AllowUserToAddRows = False
        Me.DGVManager.AllowUserToDeleteRows = False
        Me.DGVManager.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVManager.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.日期, Me.计划, Me.乘务员数量})
        Me.DGVManager.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVManager.Location = New System.Drawing.Point(3, 17)
        Me.DGVManager.Name = "DGVManager"
        Me.DGVManager.ReadOnly = True
        Me.DGVManager.RowHeadersVisible = False
        Me.DGVManager.RowTemplate.Height = 23
        Me.DGVManager.Size = New System.Drawing.Size(524, 251)
        Me.DGVManager.TabIndex = 0
        '
        '日期
        '
        Me.日期.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.日期.HeaderText = "日期"
        Me.日期.Name = "日期"
        Me.日期.ReadOnly = True
        Me.日期.Width = 54
        '
        '计划
        '
        Me.计划.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.计划.HeaderText = "计划"
        Me.计划.Name = "计划"
        Me.计划.ReadOnly = True
        Me.计划.Width = 54
        '
        '乘务员数量
        '
        Me.乘务员数量.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.乘务员数量.HeaderText = "乘务员组数"
        Me.乘务员数量.Name = "乘务员数量"
        Me.乘务员数量.ReadOnly = True
        Me.乘务员数量.Width = 90
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DGVManager)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(530, 271)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "已有计划信息"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.DTPEnd)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.DTPStart)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 297)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(524, 47)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'DTPEnd
        '
        Me.DTPEnd.Location = New System.Drawing.Point(292, 16)
        Me.DTPEnd.Name = "DTPEnd"
        Me.DTPEnd.Size = New System.Drawing.Size(132, 21)
        Me.DTPEnd.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(227, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "结束日期:"
        '
        'DTPStart
        '
        Me.DTPStart.Location = New System.Drawing.Point(77, 16)
        Me.DTPStart.Name = "DTPStart"
        Me.DTPStart.Size = New System.Drawing.Size(132, 21)
        Me.DTPStart.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "起始日期:"
        '
        'Btn_Open
        '
        Me.Btn_Open.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Btn_Open.Location = New System.Drawing.Point(362, 350)
        Me.Btn_Open.Name = "Btn_Open"
        Me.Btn_Open.Size = New System.Drawing.Size(75, 23)
        Me.Btn_Open.TabIndex = 2
        Me.Btn_Open.Text = "打开(&O)"
        Me.Btn_Open.UseVisualStyleBackColor = True
        '
        'Btn_Delete
        '
        Me.Btn_Delete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Btn_Delete.Location = New System.Drawing.Point(3, 350)
        Me.Btn_Delete.Name = "Btn_Delete"
        Me.Btn_Delete.Size = New System.Drawing.Size(75, 23)
        Me.Btn_Delete.TabIndex = 2
        Me.Btn_Delete.Text = "删除(&D)"
        Me.Btn_Delete.UseVisualStyleBackColor = True
        '
        'Btn_Cancle
        '
        Me.Btn_Cancle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Btn_Cancle.Location = New System.Drawing.Point(443, 350)
        Me.Btn_Cancle.Name = "Btn_Cancle"
        Me.Btn_Cancle.Size = New System.Drawing.Size(75, 23)
        Me.Btn_Cancle.TabIndex = 2
        Me.Btn_Cancle.Text = "退出(&E)"
        Me.Btn_Cancle.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 280)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 12)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "选择线路:"
        '
        'CmbLine
        '
        Me.CmbLine.FormattingEnabled = True
        Me.CmbLine.Location = New System.Drawing.Point(76, 275)
        Me.CmbLine.Name = "CmbLine"
        Me.CmbLine.Size = New System.Drawing.Size(121, 20)
        Me.CmbLine.TabIndex = 4
        '
        'FrmOpenPlan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(530, 385)
        Me.Controls.Add(Me.CmbLine)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Btn_Delete)
        Me.Controls.Add(Me.Btn_Cancle)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Btn_Open)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmOpenPlan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "乘务计划管理"
        CType(Me.DGVManager, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DGVManager As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents DTPEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DTPStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Btn_Open As System.Windows.Forms.Button
    Friend WithEvents Btn_Delete As System.Windows.Forms.Button
    Friend WithEvents Btn_Cancle As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents CmbLine As System.Windows.Forms.ComboBox
    Friend WithEvents 日期 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 计划 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 乘务员数量 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
