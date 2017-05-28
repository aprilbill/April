<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCSMakeTTSelect
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
        Me.WorkDayTimeTableSelect = New System.Windows.Forms.Label()
        Me.ComWDTT = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Comline = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'WorkDayTimeTableSelect
        '
        Me.WorkDayTimeTableSelect.AutoSize = True
        Me.WorkDayTimeTableSelect.Location = New System.Drawing.Point(21, 45)
        Me.WorkDayTimeTableSelect.Name = "WorkDayTimeTableSelect"
        Me.WorkDayTimeTableSelect.Size = New System.Drawing.Size(71, 12)
        Me.WorkDayTimeTableSelect.TabIndex = 0
        Me.WorkDayTimeTableSelect.Text = "运行图名称:"
        '
        'ComWDTT
        '
        Me.ComWDTT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComWDTT.FormattingEnabled = True
        Me.ComWDTT.Location = New System.Drawing.Point(98, 42)
        Me.ComWDTT.Name = "ComWDTT"
        Me.ComWDTT.Size = New System.Drawing.Size(256, 20)
        Me.ComWDTT.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(185, 96)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "确定(&Y)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(279, 96)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "退出(&E)"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Comline
        '
        Me.Comline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Comline.FormattingEnabled = True
        Me.Comline.Location = New System.Drawing.Point(98, 15)
        Me.Comline.Name = "Comline"
        Me.Comline.Size = New System.Drawing.Size(256, 20)
        Me.Comline.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 12)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "线路名称:"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(18, 74)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(336, 16)
        Me.ProgressBar1.TabIndex = 14
        Me.ProgressBar1.Visible = False
        '
        'frmCSMakeTTSelect
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(377, 131)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Comline)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ComWDTT)
        Me.Controls.Add(Me.WorkDayTimeTableSelect)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCSMakeTTSelect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "乘务编制运行图选择"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents WorkDayTimeTableSelect As System.Windows.Forms.Label
    Friend WithEvents ComWDTT As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Comline As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
End Class
