<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOtherLineCrew
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.线路 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.班组 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.岗位 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.工作证号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.工号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.姓名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.前安全公里 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.开始统计时间 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "选择线路"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(72, 20)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(107, 20)
        Me.ComboBox1.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(207, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "班组"
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(242, 20)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(105, 20)
        Me.ComboBox2.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(495, 18)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(89, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "调入本线"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.线路, Me.班组, Me.岗位, Me.工作证号, Me.工号, Me.姓名, Me.前安全公里, Me.开始统计时间})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 50)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(678, 334)
        Me.DataGridView1.TabIndex = 5
        '
        '线路
        '
        Me.线路.HeaderText = "线路"
        Me.线路.Name = "线路"
        '
        '班组
        '
        Me.班组.HeaderText = "班组"
        Me.班组.Name = "班组"
        '
        '岗位
        '
        Me.岗位.HeaderText = "岗位"
        Me.岗位.Name = "岗位"
        '
        '工作证号
        '
        Me.工作证号.HeaderText = "工作证号"
        Me.工作证号.Name = "工作证号"
        '
        '工号
        '
        Me.工号.HeaderText = "工号"
        Me.工号.Name = "工号"
        '
        '姓名
        '
        Me.姓名.HeaderText = "姓名"
        Me.姓名.Name = "姓名"
        '
        '前安全公里
        '
        Me.前安全公里.HeaderText = "前安全公里"
        Me.前安全公里.Name = "前安全公里"
        '
        '开始统计时间
        '
        Me.开始统计时间.HeaderText = "开始统计时间"
        Me.开始统计时间.Name = "开始统计时间"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Red
        Me.Label3.Location = New System.Drawing.Point(13, 395)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(275, 12)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "注意：本功能需要在连接内网下使用,多选请按Ctrl"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(374, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 12)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "已选择数：0"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(601, 18)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(89, 23)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "退出"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(325, 390)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(365, 23)
        Me.ProgressBar1.TabIndex = 9
        Me.ProgressBar1.Visible = False
        '
        'FrmOtherLineCrew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(710, 419)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.Name = "FrmOtherLineCrew"
        Me.Text = "获取其他线路乘务员"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents 线路 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 班组 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 岗位 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 工作证号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 工号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 姓名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 前安全公里 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 开始统计时间 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
