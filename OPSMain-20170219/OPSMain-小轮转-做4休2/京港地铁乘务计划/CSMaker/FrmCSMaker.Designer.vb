<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCSMaker
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
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TXTMorningNum = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Btn_OK = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TXTNightNum = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TXTDayNum = New System.Windows.Forms.TextBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TXTCDayNum = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(36, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(59, 12)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "早班人数:"
        '
        'TXTMorningNum
        '
        Me.TXTMorningNum.Location = New System.Drawing.Point(105, 17)
        Me.TXTMorningNum.Name = "TXTMorningNum"
        Me.TXTMorningNum.Size = New System.Drawing.Size(96, 21)
        Me.TXTMorningNum.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(213, 21)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(17, 12)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "人"
        '
        'Btn_OK
        '
        Me.Btn_OK.Location = New System.Drawing.Point(91, 137)
        Me.Btn_OK.Name = "Btn_OK"
        Me.Btn_OK.Size = New System.Drawing.Size(75, 23)
        Me.Btn_OK.TabIndex = 3
        Me.Btn_OK.Text = "确定(&Y)"
        Me.Btn_OK.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(213, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(17, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "人"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(36, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "夜班人数:"
        '
        'TXTNightNum
        '
        Me.TXTNightNum.Location = New System.Drawing.Point(105, 44)
        Me.TXTNightNum.Name = "TXTNightNum"
        Me.TXTNightNum.Size = New System.Drawing.Size(96, 21)
        Me.TXTNightNum.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(213, 105)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(17, 12)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "人"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(36, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 12)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "白班人数:"
        '
        'TXTDayNum
        '
        Me.TXTDayNum.Location = New System.Drawing.Point(105, 72)
        Me.TXTDayNum.Name = "TXTDayNum"
        Me.TXTDayNum.Size = New System.Drawing.Size(96, 21)
        Me.TXTDayNum.TabIndex = 1
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 500
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(24, 104)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 12)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "日勤班人数:"
        '
        'TXTCDayNum
        '
        Me.TXTCDayNum.Location = New System.Drawing.Point(105, 100)
        Me.TXTCDayNum.Name = "TXTCDayNum"
        Me.TXTCDayNum.Size = New System.Drawing.Size(96, 21)
        Me.TXTCDayNum.TabIndex = 1
        Me.TXTCDayNum.Text = "0"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(213, 74)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(17, 12)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "人"
        '
        'BtnExit
        '
        Me.BtnExit.Location = New System.Drawing.Point(172, 137)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(75, 23)
        Me.BtnExit.TabIndex = 13
        Me.BtnExit.Text = "退出(&E)"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'FrmCSMaker
        '
        Me.AcceptButton = Me.Btn_OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(269, 178)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.Btn_OK)
        Me.Controls.Add(Me.TXTDayNum)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TXTCDayNum)
        Me.Controls.Add(Me.TXTNightNum)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TXTMorningNum)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmCSMaker"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "计划编制人数"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TXTMorningNum As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Btn_OK As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TXTNightNum As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TXTDayNum As System.Windows.Forms.TextBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TXTCDayNum As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents BtnExit As System.Windows.Forms.Button
End Class
