<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCalPerson
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

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.运行图名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.天数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.早高峰 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.早高峰轮换 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.平峰 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.平峰轮换 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.晚高峰 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.晚高峰轮换 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Button4 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 12
        Me.ListBox1.Location = New System.Drawing.Point(6, 32)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(182, 172)
        Me.ListBox1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "当前运行图"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 265)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "班种"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.运行图名, Me.天数, Me.早高峰, Me.早高峰轮换, Me.平峰, Me.平峰轮换, Me.晚高峰, Me.晚高峰轮换})
        Me.DataGridView1.Location = New System.Drawing.Point(275, 32)
        Me.DataGridView1.MultiSelect = False
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(592, 201)
        Me.DataGridView1.TabIndex = 3
        '
        '运行图名
        '
        Me.运行图名.HeaderText = "运行图名"
        Me.运行图名.Name = "运行图名"
        '
        '天数
        '
        Me.天数.HeaderText = "天数"
        Me.天数.Name = "天数"
        Me.天数.Width = 60
        '
        '早高峰
        '
        Me.早高峰.HeaderText = "早高峰"
        Me.早高峰.Name = "早高峰"
        Me.早高峰.Width = 70
        '
        '早高峰轮换
        '
        Me.早高峰轮换.HeaderText = "轮换（早）"
        Me.早高峰轮换.Name = "早高峰轮换"
        Me.早高峰轮换.Width = 70
        '
        '平峰
        '
        Me.平峰.HeaderText = "平峰"
        Me.平峰.Name = "平峰"
        Me.平峰.Width = 60
        '
        '平峰轮换
        '
        Me.平峰轮换.HeaderText = "轮换（平）"
        Me.平峰轮换.Name = "平峰轮换"
        Me.平峰轮换.Width = 70
        '
        '晚高峰
        '
        Me.晚高峰.HeaderText = "晚高峰"
        Me.晚高峰.Name = "晚高峰"
        Me.晚高峰.Width = 70
        '
        '晚高峰轮换
        '
        Me.晚高峰轮换.HeaderText = "轮换（晚）"
        Me.晚高峰轮换.Name = "晚高峰轮换"
        Me.晚高峰轮换.Width = 70
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(273, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "涉及运行图"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(194, 76)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "->"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(194, 141)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "<-"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(201, 265)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "休息天数"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(265, 261)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 21)
        Me.TextBox1.TabIndex = 9
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(395, 260)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 10
        Me.Button3.Text = "计算"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(497, 265)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 12)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "建议每班人数"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(580, 260)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.ReadOnly = True
        Me.TextBox2.Size = New System.Drawing.Size(100, 21)
        Me.TextBox2.TabIndex = 12
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ProgressBar1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.ListBox1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.DataGridView1)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(873, 243)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(6, 210)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(182, 23)
        Me.ProgressBar1.TabIndex = 7
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"四班两转", "五班三转"})
        Me.ComboBox1.Location = New System.Drawing.Point(50, 261)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(121, 20)
        Me.ComboBox1.TabIndex = 7
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(703, 258)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 14
        Me.Button4.Text = "退出"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'FrmCalPerson
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(897, 294)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label2)
        Me.MaximizeBox = False
        Me.Name = "FrmCalPerson"
        Me.Text = "初步人数测算"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents 运行图名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 天数 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 早高峰 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 早高峰轮换 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 平峰 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 平峰轮换 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 晚高峰 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 晚高峰轮换 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button4 As System.Windows.Forms.Button
End Class
