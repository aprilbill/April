<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdjustEvetime
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.labTimeLeft = New System.Windows.Forms.Label
        Me.LabtimeRight = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.labTimeEver = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtLeftTime = New System.Windows.Forms.TextBox
        Me.txtRightTime = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.labTimeTotle = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnOk = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.调整间隔 = New System.Windows.Forms.GroupBox
        Me.labUporDown = New System.Windows.Forms.Label
        Me.labCurSec = New System.Windows.Forms.Label
        Me.labRightTrain = New System.Windows.Forms.Label
        Me.labCurTrain = New System.Windows.Forms.Label
        Me.labLeftTrain = New System.Windows.Forms.Label
        Me.调整间隔.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Navy
        Me.Panel1.Location = New System.Drawing.Point(130, 37)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(3, 50)
        Me.Panel1.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Red
        Me.Panel2.Location = New System.Drawing.Point(206, 37)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(3, 50)
        Me.Panel2.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Navy
        Me.Panel3.Location = New System.Drawing.Point(274, 37)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(3, 50)
        Me.Panel3.TabIndex = 0
        '
        'labTimeLeft
        '
        Me.labTimeLeft.AutoSize = True
        Me.labTimeLeft.Location = New System.Drawing.Point(147, 54)
        Me.labTimeLeft.Name = "labTimeLeft"
        Me.labTimeLeft.Size = New System.Drawing.Size(53, 12)
        Me.labTimeLeft.TabIndex = 1
        Me.labTimeLeft.Text = "左边间隔"
        '
        'LabtimeRight
        '
        Me.LabtimeRight.AutoSize = True
        Me.LabtimeRight.Location = New System.Drawing.Point(215, 54)
        Me.LabtimeRight.Name = "LabtimeRight"
        Me.LabtimeRight.Size = New System.Drawing.Size(53, 12)
        Me.LabtimeRight.TabIndex = 2
        Me.LabtimeRight.Text = "右边间隔"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(165, 103)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "平均间隔="
        '
        'labTimeEver
        '
        Me.labTimeEver.AutoSize = True
        Me.labTimeEver.Location = New System.Drawing.Point(231, 103)
        Me.labTimeEver.Name = "labTimeEver"
        Me.labTimeEver.Size = New System.Drawing.Size(17, 12)
        Me.labTimeEver.TabIndex = 4
        Me.labTimeEver.Text = "无"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 135)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "左边间隔："
        '
        'txtLeftTime
        '
        Me.txtLeftTime.Location = New System.Drawing.Point(79, 132)
        Me.txtLeftTime.Name = "txtLeftTime"
        Me.txtLeftTime.Size = New System.Drawing.Size(64, 21)
        Me.txtLeftTime.TabIndex = 6
        '
        'txtRightTime
        '
        Me.txtRightTime.Location = New System.Drawing.Point(221, 132)
        Me.txtRightTime.Name = "txtRightTime"
        Me.txtRightTime.Size = New System.Drawing.Size(64, 21)
        Me.txtRightTime.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(159, 135)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "右边间隔："
        '
        'labTimeTotle
        '
        Me.labTimeTotle.AutoSize = True
        Me.labTimeTotle.Location = New System.Drawing.Point(115, 103)
        Me.labTimeTotle.Name = "labTimeTotle"
        Me.labTimeTotle.Size = New System.Drawing.Size(17, 12)
        Me.labTimeTotle.TabIndex = 10
        Me.labTimeTotle.Text = "无"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(62, 103)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 12)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "总间隔="
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(162, 184)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 11
        Me.btnOk.Text = "应用(&A)"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(249, 184)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 12
        Me.btnCancel.Text = "退出(&E)"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        '调整间隔
        '
        Me.调整间隔.Controls.Add(Me.labUporDown)
        Me.调整间隔.Controls.Add(Me.labCurSec)
        Me.调整间隔.Controls.Add(Me.labRightTrain)
        Me.调整间隔.Controls.Add(Me.labCurTrain)
        Me.调整间隔.Controls.Add(Me.labLeftTrain)
        Me.调整间隔.Controls.Add(Me.labTimeTotle)
        Me.调整间隔.Controls.Add(Me.Label5)
        Me.调整间隔.Controls.Add(Me.txtRightTime)
        Me.调整间隔.Controls.Add(Me.Label3)
        Me.调整间隔.Controls.Add(Me.txtLeftTime)
        Me.调整间隔.Controls.Add(Me.Label1)
        Me.调整间隔.Controls.Add(Me.labTimeEver)
        Me.调整间隔.Controls.Add(Me.Label2)
        Me.调整间隔.Controls.Add(Me.LabtimeRight)
        Me.调整间隔.Controls.Add(Me.labTimeLeft)
        Me.调整间隔.Controls.Add(Me.Panel3)
        Me.调整间隔.Controls.Add(Me.Panel2)
        Me.调整间隔.Controls.Add(Me.Panel1)
        Me.调整间隔.Location = New System.Drawing.Point(13, 3)
        Me.调整间隔.Name = "调整间隔"
        Me.调整间隔.Size = New System.Drawing.Size(311, 166)
        Me.调整间隔.TabIndex = 13
        Me.调整间隔.TabStop = False
        Me.调整间隔.Text = "调整间隔"
        '
        'labUporDown
        '
        Me.labUporDown.AutoSize = True
        Me.labUporDown.Location = New System.Drawing.Point(6, 68)
        Me.labUporDown.Name = "labUporDown"
        Me.labUporDown.Size = New System.Drawing.Size(65, 12)
        Me.labUporDown.TabIndex = 15
        Me.labUporDown.Text = "上下行列车"
        '
        'labCurSec
        '
        Me.labCurSec.AutoSize = True
        Me.labCurSec.Location = New System.Drawing.Point(6, 40)
        Me.labCurSec.Name = "labCurSec"
        Me.labCurSec.Size = New System.Drawing.Size(53, 12)
        Me.labCurSec.TabIndex = 14
        Me.labCurSec.Text = "当前区间"
        '
        'labRightTrain
        '
        Me.labRightTrain.AutoSize = True
        Me.labRightTrain.Location = New System.Drawing.Point(253, 22)
        Me.labRightTrain.Name = "labRightTrain"
        Me.labRightTrain.Size = New System.Drawing.Size(53, 12)
        Me.labRightTrain.TabIndex = 13
        Me.labRightTrain.Text = "右边车次"
        '
        'labCurTrain
        '
        Me.labCurTrain.AutoSize = True
        Me.labCurTrain.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.labCurTrain.Location = New System.Drawing.Point(184, 22)
        Me.labCurTrain.Name = "labCurTrain"
        Me.labCurTrain.Size = New System.Drawing.Size(53, 12)
        Me.labCurTrain.TabIndex = 12
        Me.labCurTrain.Text = "当前车次"
        '
        'labLeftTrain
        '
        Me.labLeftTrain.AutoSize = True
        Me.labLeftTrain.Location = New System.Drawing.Point(112, 22)
        Me.labLeftTrain.Name = "labLeftTrain"
        Me.labLeftTrain.Size = New System.Drawing.Size(53, 12)
        Me.labLeftTrain.TabIndex = 11
        Me.labLeftTrain.Text = "左边车次"
        '
        'frmAdjustEvetime
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(340, 217)
        Me.Controls.Add(Me.调整间隔)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAdjustEvetime"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "调匀运行线"
        Me.调整间隔.ResumeLayout(False)
        Me.调整间隔.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents labTimeLeft As System.Windows.Forms.Label
    Friend WithEvents LabtimeRight As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents labTimeEver As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtLeftTime As System.Windows.Forms.TextBox
    Friend WithEvents txtRightTime As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents labTimeTotle As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents 调整间隔 As System.Windows.Forms.GroupBox
    Friend WithEvents labRightTrain As System.Windows.Forms.Label
    Friend WithEvents labCurTrain As System.Windows.Forms.Label
    Friend WithEvents labLeftTrain As System.Windows.Forms.Label
    Friend WithEvents labCurSec As System.Windows.Forms.Label
    Friend WithEvents labUporDown As System.Windows.Forms.Label
End Class
