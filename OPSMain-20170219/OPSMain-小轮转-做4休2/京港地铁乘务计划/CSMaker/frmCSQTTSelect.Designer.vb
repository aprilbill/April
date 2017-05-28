<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCSQTTSelect
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
        Me.cmbCancel = New System.Windows.Forms.Button()
        Me.cmdOk = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstName = New System.Windows.Forms.ListBox()
        Me.LabInfor = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComlineInf = New System.Windows.Forms.ComboBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'cmbCancel
        '
        Me.cmbCancel.Location = New System.Drawing.Point(345, 387)
        Me.cmbCancel.Name = "cmbCancel"
        Me.cmbCancel.Size = New System.Drawing.Size(71, 24)
        Me.cmbCancel.TabIndex = 10
        Me.cmbCancel.Text = "取消(&C)"
        '
        'cmdOk
        '
        Me.cmdOk.Location = New System.Drawing.Point(267, 387)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(71, 24)
        Me.cmdOk.TabIndex = 9
        Me.cmdOk.Text = "确定(&O)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 81)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(155, 12)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "请从下列乘务计划表中选择:"
        '
        'lstName
        '
        Me.lstName.FormattingEnabled = True
        Me.lstName.HorizontalScrollbar = True
        Me.lstName.ItemHeight = 12
        Me.lstName.Location = New System.Drawing.Point(22, 105)
        Me.lstName.Name = "lstName"
        Me.lstName.Size = New System.Drawing.Size(394, 208)
        Me.lstName.TabIndex = 7
        '
        'LabInfor
        '
        Me.LabInfor.AutoSize = True
        Me.LabInfor.Location = New System.Drawing.Point(22, 322)
        Me.LabInfor.Name = "LabInfor"
        Me.LabInfor.Size = New System.Drawing.Size(83, 12)
        Me.LabInfor.TabIndex = 8
        Me.LabInfor.Text = "乘务计划信息:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 12)
        Me.Label2.TabIndex = 11
        Me.Label2.Text = "线路名称:"
        '
        'ComlineInf
        '
        Me.ComlineInf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComlineInf.FormattingEnabled = True
        Me.ComlineInf.Location = New System.Drawing.Point(24, 42)
        Me.ComlineInf.Name = "ComlineInf"
        Me.ComlineInf.Size = New System.Drawing.Size(224, 20)
        Me.ComlineInf.TabIndex = 12
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(22, 367)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(394, 14)
        Me.ProgressBar1.TabIndex = 14
        Me.ProgressBar1.Visible = False
        '
        'frmCSQTTSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 442)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.ComlineInf)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.LabInfor)
        Me.Controls.Add(Me.lstName)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCSQTTSelect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "选择乘务计划表"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbCancel As System.Windows.Forms.Button
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lstName As System.Windows.Forms.ListBox
    Friend WithEvents LabInfor As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComlineInf As System.Windows.Forms.ComboBox
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
End Class
