<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAssignUnDuty
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Cmb_Duties = New System.Windows.Forms.ComboBox()
        Me.Btn_Cancle = New System.Windows.Forms.Button()
        Me.Btn_Ok = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(25, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "选择任务:"
        '
        'Cmb_Duties
        '
        Me.Cmb_Duties.FormattingEnabled = True
        Me.Cmb_Duties.Location = New System.Drawing.Point(99, 25)
        Me.Cmb_Duties.Name = "Cmb_Duties"
        Me.Cmb_Duties.Size = New System.Drawing.Size(206, 20)
        Me.Cmb_Duties.TabIndex = 1
        '
        'Btn_Cancle
        '
        Me.Btn_Cancle.Location = New System.Drawing.Point(230, 68)
        Me.Btn_Cancle.Name = "Btn_Cancle"
        Me.Btn_Cancle.Size = New System.Drawing.Size(75, 23)
        Me.Btn_Cancle.TabIndex = 2
        Me.Btn_Cancle.Text = "取消(&C)"
        Me.Btn_Cancle.UseVisualStyleBackColor = True
        '
        'Btn_Ok
        '
        Me.Btn_Ok.Location = New System.Drawing.Point(149, 68)
        Me.Btn_Ok.Name = "Btn_Ok"
        Me.Btn_Ok.Size = New System.Drawing.Size(75, 23)
        Me.Btn_Ok.TabIndex = 2
        Me.Btn_Ok.Text = "确定(&Q)"
        Me.Btn_Ok.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 68)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(94, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "详细信息↓"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PictureBox1.Location = New System.Drawing.Point(-21, 103)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(359, 2)
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PropertyGrid1)
        Me.Panel1.Location = New System.Drawing.Point(12, 111)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(310, 202)
        Me.Panel1.TabIndex = 5
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGrid1.HelpVisible = False
        Me.PropertyGrid1.Location = New System.Drawing.Point(0, 0)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(308, 200)
        Me.PropertyGrid1.TabIndex = 0
        '
        'FrmAssignUnDuty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 325)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Btn_Ok)
        Me.Controls.Add(Me.Btn_Cancle)
        Me.Controls.Add(Me.Cmb_Duties)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAssignUnDuty"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "安排空闲任务"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Cmb_Duties As System.Windows.Forms.ComboBox
    Friend WithEvents Btn_Cancle As System.Windows.Forms.Button
    Friend WithEvents Btn_Ok As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
End Class
