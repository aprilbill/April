<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmChangeDury
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.PreEnd = New System.Windows.Forms.TextBox()
        Me.PreStart = New System.Windows.Forms.TextBox()
        Me.PreDutySort = New System.Windows.Forms.TextBox()
        Me.PreDis = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PreNo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.CmbDutySort = New System.Windows.Forms.ComboBox()
        Me.CurNo = New System.Windows.Forms.ComboBox()
        Me.CurEnd = New System.Windows.Forms.TextBox()
        Me.CurStart = New System.Windows.Forms.TextBox()
        Me.CurDis = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.PreEnd)
        Me.GroupBox1.Controls.Add(Me.PreStart)
        Me.GroupBox1.Controls.Add(Me.PreDutySort)
        Me.GroupBox1.Controls.Add(Me.PreDis)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.PreNo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(360, 149)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "当前任务"
        '
        'PreEnd
        '
        Me.PreEnd.Location = New System.Drawing.Point(114, 105)
        Me.PreEnd.Name = "PreEnd"
        Me.PreEnd.ReadOnly = True
        Me.PreEnd.Size = New System.Drawing.Size(229, 21)
        Me.PreEnd.TabIndex = 1
        '
        'PreStart
        '
        Me.PreStart.Location = New System.Drawing.Point(114, 78)
        Me.PreStart.Name = "PreStart"
        Me.PreStart.ReadOnly = True
        Me.PreStart.Size = New System.Drawing.Size(229, 21)
        Me.PreStart.TabIndex = 1
        '
        'PreDutySort
        '
        Me.PreDutySort.Location = New System.Drawing.Point(60, 49)
        Me.PreDutySort.Name = "PreDutySort"
        Me.PreDutySort.ReadOnly = True
        Me.PreDutySort.Size = New System.Drawing.Size(110, 21)
        Me.PreDutySort.TabIndex = 1
        '
        'PreDis
        '
        Me.PreDis.Location = New System.Drawing.Point(241, 49)
        Me.PreDis.Name = "PreDis"
        Me.PreDis.ReadOnly = True
        Me.PreDis.Size = New System.Drawing.Size(102, 21)
        Me.PreDis.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(19, 110)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 12)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "退勤时间/地点:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 12)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "出勤时间/地点:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(19, 54)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 12)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "班种:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(176, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "驾驶公里:"
        '
        'PreNo
        '
        Me.PreNo.Location = New System.Drawing.Point(84, 22)
        Me.PreNo.Name = "PreNo"
        Me.PreNo.ReadOnly = True
        Me.PreNo.Size = New System.Drawing.Size(259, 21)
        Me.PreNo.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "任务编号:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.CmbDutySort)
        Me.GroupBox2.Controls.Add(Me.CurNo)
        Me.GroupBox2.Controls.Add(Me.CurEnd)
        Me.GroupBox2.Controls.Add(Me.CurStart)
        Me.GroupBox2.Controls.Add(Me.CurDis)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 167)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(360, 143)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "交换任务"
        '
        'CmbDutySort
        '
        Me.CmbDutySort.FormattingEnabled = True
        Me.CmbDutySort.Items.AddRange(New Object() {"早班", "白班", "日勤班", "夜班"})
        Me.CmbDutySort.Location = New System.Drawing.Point(60, 48)
        Me.CmbDutySort.Name = "CmbDutySort"
        Me.CmbDutySort.Size = New System.Drawing.Size(110, 20)
        Me.CmbDutySort.TabIndex = 5
        '
        'CurNo
        '
        Me.CurNo.FormattingEnabled = True
        Me.CurNo.Location = New System.Drawing.Point(83, 22)
        Me.CurNo.Name = "CurNo"
        Me.CurNo.Size = New System.Drawing.Size(260, 20)
        Me.CurNo.TabIndex = 4
        '
        'CurEnd
        '
        Me.CurEnd.Location = New System.Drawing.Point(114, 104)
        Me.CurEnd.Name = "CurEnd"
        Me.CurEnd.ReadOnly = True
        Me.CurEnd.Size = New System.Drawing.Size(229, 21)
        Me.CurEnd.TabIndex = 1
        '
        'CurStart
        '
        Me.CurStart.Location = New System.Drawing.Point(114, 77)
        Me.CurStart.Name = "CurStart"
        Me.CurStart.ReadOnly = True
        Me.CurStart.Size = New System.Drawing.Size(229, 21)
        Me.CurStart.TabIndex = 1
        '
        'CurDis
        '
        Me.CurDis.Location = New System.Drawing.Point(241, 48)
        Me.CurDis.Name = "CurDis"
        Me.CurDis.ReadOnly = True
        Me.CurDis.Size = New System.Drawing.Size(102, 21)
        Me.CurDis.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 109)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 12)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "退勤时间/地点:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(19, 83)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 12)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "出勤时间/地点:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(19, 54)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 12)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "班种:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(176, 54)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(59, 12)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "驾驶公里:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(19, 27)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 12)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "任务编号:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(217, 316)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "确定(&S)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(298, 316)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "取消(&C)"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'FrmChangeDury
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(385, 351)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmChangeDury"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "交换任务"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents PreEnd As System.Windows.Forms.TextBox
    Friend WithEvents PreStart As System.Windows.Forms.TextBox
    Friend WithEvents PreDis As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents PreNo As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents CurEnd As System.Windows.Forms.TextBox
    Friend WithEvents CurStart As System.Windows.Forms.TextBox
    Friend WithEvents CurDis As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents CurNo As System.Windows.Forms.ComboBox
    Friend WithEvents PreDutySort As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents CmbDutySort As System.Windows.Forms.ComboBox
End Class
