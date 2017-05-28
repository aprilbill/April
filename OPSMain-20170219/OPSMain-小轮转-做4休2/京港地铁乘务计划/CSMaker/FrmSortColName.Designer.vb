<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSortColName
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
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.ListBoxColName = New System.Windows.Forms.ListBox()
        Me.ListBoxColName2 = New System.Windows.Forms.ListBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(182, 125)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(59, 23)
        Me.Button5.TabIndex = 27
        Me.Button5.Text = "<--"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(182, 84)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(59, 23)
        Me.Button4.TabIndex = 26
        Me.Button4.Text = "-->"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'ListBoxColName
        '
        Me.ListBoxColName.FormattingEnabled = True
        Me.ListBoxColName.ItemHeight = 12
        Me.ListBoxColName.Items.AddRange(New Object() {"出勤时间", "退勤时间", "工作时间", "驾驶公里"})
        Me.ListBoxColName.Location = New System.Drawing.Point(12, 41)
        Me.ListBoxColName.Name = "ListBoxColName"
        Me.ListBoxColName.Size = New System.Drawing.Size(164, 148)
        Me.ListBoxColName.TabIndex = 25
        '
        'ListBoxColName2
        '
        Me.ListBoxColName2.FormattingEnabled = True
        Me.ListBoxColName2.ItemHeight = 12
        Me.ListBoxColName2.Location = New System.Drawing.Point(247, 41)
        Me.ListBoxColName2.Name = "ListBoxColName2"
        Me.ListBoxColName2.Size = New System.Drawing.Size(164, 148)
        Me.ListBoxColName2.TabIndex = 28
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(247, 204)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 29
        Me.Button1.Text = "确定"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(336, 204)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 30
        Me.Button2.Text = "关闭"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "选择字段"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(245, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 32
        Me.Label2.Text = "排序字段"
        '
        'FrmSortColName
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(428, 240)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ListBoxColName2)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.ListBoxColName)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSortColName"
        Me.Text = "选择排序字段"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents ListBoxColName As System.Windows.Forms.ListBox
    Friend WithEvents ListBoxColName2 As System.Windows.Forms.ListBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
