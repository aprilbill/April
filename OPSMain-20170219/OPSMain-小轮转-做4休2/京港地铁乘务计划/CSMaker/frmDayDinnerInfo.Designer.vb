<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDayDinnerInfo
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
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDayNeedDriverNum = New System.Windows.Forms.TextBox()
        Me.txtDayRealDriverNum = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.TextBoxCuntifanDriverNum = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.交路 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.用餐地点 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.需要替饭人数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.实际替饭人数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.新增替饭人数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.是否只替饭 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 218)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "白班需要人数："
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(128, 15)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(100, 21)
        Me.TextBox1.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(113, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "白班上线车底数量："
        '
        'txtDayNeedDriverNum
        '
        Me.txtDayNeedDriverNum.Enabled = False
        Me.txtDayNeedDriverNum.Location = New System.Drawing.Point(121, 213)
        Me.txtDayNeedDriverNum.Name = "txtDayNeedDriverNum"
        Me.txtDayNeedDriverNum.Size = New System.Drawing.Size(100, 21)
        Me.txtDayNeedDriverNum.TabIndex = 1
        '
        'txtDayRealDriverNum
        '
        Me.txtDayRealDriverNum.Location = New System.Drawing.Point(121, 271)
        Me.txtDayRealDriverNum.Name = "txtDayRealDriverNum"
        Me.txtDayRealDriverNum.Size = New System.Drawing.Size(100, 21)
        Me.txtDayRealDriverNum.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(26, 274)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 12)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "白班实际人数："
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.交路, Me.用餐地点, Me.需要替饭人数, Me.实际替饭人数, Me.新增替饭人数, Me.是否只替饭})
        Me.DataGridView1.Location = New System.Drawing.Point(16, 62)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(541, 138)
        Me.DataGridView1.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(458, 271)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "关闭"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(243, 213)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "计算"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 46)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 12)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "用餐地点信息："
        '
        'Button3
        '
        Me.Button3.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Button3.Location = New System.Drawing.Point(368, 271)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 6
        Me.Button3.Text = "确定"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'TextBoxCuntifanDriverNum
        '
        Me.TextBoxCuntifanDriverNum.Enabled = False
        Me.TextBoxCuntifanDriverNum.Location = New System.Drawing.Point(121, 241)
        Me.TextBoxCuntifanDriverNum.Name = "TextBoxCuntifanDriverNum"
        Me.TextBoxCuntifanDriverNum.Size = New System.Drawing.Size(100, 21)
        Me.TextBoxCuntifanDriverNum.TabIndex = 11
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(26, 246)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 12)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "其中替饭人数："
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.Red
        Me.Label7.Location = New System.Drawing.Point(241, 246)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(161, 12)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "勾选""只替饭""，则不安排用餐"
        '
        '交路
        '
        Me.交路.HeaderText = "交路"
        Me.交路.Name = "交路"
        '
        '用餐地点
        '
        Me.用餐地点.HeaderText = "用餐地点"
        Me.用餐地点.Name = "用餐地点"
        '
        '需要替饭人数
        '
        Me.需要替饭人数.HeaderText = "需要替饭人数"
        Me.需要替饭人数.Name = "需要替饭人数"
        '
        '实际替饭人数
        '
        Me.实际替饭人数.HeaderText = "实际替饭人数"
        Me.实际替饭人数.Name = "实际替饭人数"
        '
        '新增替饭人数
        '
        Me.新增替饭人数.HeaderText = "新增替饭人数"
        Me.新增替饭人数.Name = "新增替饭人数"
        '
        '是否只替饭
        '
        Me.是否只替饭.HeaderText = "是否只替饭"
        Me.是否只替饭.Name = "是否只替饭"
        '
        'frmDayDinnerInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 317)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxCuntifanDriverNum)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.txtDayRealDriverNum)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtDayNeedDriverNum)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.Name = "frmDayDinnerInfo"
        Me.Text = "白班人数信息"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDayNeedDriverNum As System.Windows.Forms.TextBox
    Friend WithEvents txtDayRealDriverNum As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents TextBoxCuntifanDriverNum As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents 交路 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 用餐地点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 需要替饭人数 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 实际替饭人数 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 新增替饭人数 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 是否只替饭 As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
