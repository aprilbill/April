<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAddOffDutyPlace
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
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ListBoxshiftPlace = New System.Windows.Forms.ListBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TxtStation = New System.Windows.Forms.TextBox()
        Me.Btn_AddStation = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("微软雅黑", 10.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label5.Location = New System.Drawing.Point(12, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(107, 20)
        Me.Label5.TabIndex = 28
        Me.Label5.Text = "出勤地点选择"
        '
        'ListBoxshiftPlace
        '
        Me.ListBoxshiftPlace.FormattingEnabled = True
        Me.ListBoxshiftPlace.ItemHeight = 12
        Me.ListBoxshiftPlace.Location = New System.Drawing.Point(12, 34)
        Me.ListBoxshiftPlace.Name = "ListBoxshiftPlace"
        Me.ListBoxshiftPlace.Size = New System.Drawing.Size(180, 316)
        Me.ListBoxshiftPlace.TabIndex = 27
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 12
        Me.ListBox1.Location = New System.Drawing.Point(279, 34)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(192, 316)
        Me.ListBox1.TabIndex = 27
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(315, 368)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 29
        Me.Button1.Text = "确定(&Y)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(396, 368)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 29
        Me.Button2.Text = "取消(&C)"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(198, 154)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 30
        Me.Button3.Text = "-->"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(198, 183)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 30
        Me.Button4.Text = "<--"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'TxtStation
        '
        Me.TxtStation.Location = New System.Drawing.Point(13, 368)
        Me.TxtStation.Name = "TxtStation"
        Me.TxtStation.Size = New System.Drawing.Size(134, 21)
        Me.TxtStation.TabIndex = 32
        '
        'Btn_AddStation
        '
        Me.Btn_AddStation.Location = New System.Drawing.Point(153, 367)
        Me.Btn_AddStation.Name = "Btn_AddStation"
        Me.Btn_AddStation.Size = New System.Drawing.Size(75, 24)
        Me.Btn_AddStation.TabIndex = 31
        Me.Btn_AddStation.Text = "添加车站"
        Me.Btn_AddStation.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.Btn_AddStation.UseVisualStyleBackColor = True
        '
        'FrmAddOffDutyPlace
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(483, 403)
        Me.Controls.Add(Me.TxtStation)
        Me.Controls.Add(Me.Btn_AddStation)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.ListBoxshiftPlace)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAddOffDutyPlace"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents ListBoxshiftPlace As System.Windows.Forms.ListBox
    Public WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents TxtStation As System.Windows.Forms.TextBox
    Public WithEvents Btn_AddStation As System.Windows.Forms.Button
End Class
