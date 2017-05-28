<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCheChiDim
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbDownSta = New System.Windows.Forms.ComboBox
        Me.cmbUpSta = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnOK = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.chkChediNum = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "下行基准站："
        '
        'cmbDownSta
        '
        Me.cmbDownSta.FormattingEnabled = True
        Me.cmbDownSta.Location = New System.Drawing.Point(101, 12)
        Me.cmbDownSta.Name = "cmbDownSta"
        Me.cmbDownSta.Size = New System.Drawing.Size(121, 20)
        Me.cmbDownSta.TabIndex = 1
        '
        'cmbUpSta
        '
        Me.cmbUpSta.FormattingEnabled = True
        Me.cmbUpSta.Location = New System.Drawing.Point(101, 47)
        Me.cmbUpSta.Name = "cmbUpSta"
        Me.cmbUpSta.Size = New System.Drawing.Size(121, 20)
        Me.cmbUpSta.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "上行基准站："
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(66, 118)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 5
        Me.btnOK.Text = "确定(&Y)"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(147, 118)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "取消(&C)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'chkChediNum
        '
        Me.chkChediNum.AutoSize = True
        Me.chkChediNum.Location = New System.Drawing.Point(101, 87)
        Me.chkChediNum.Name = "chkChediNum"
        Me.chkChediNum.Size = New System.Drawing.Size(108, 16)
        Me.chkChediNum.TabIndex = 7
        Me.chkChediNum.Text = "自动编车底编号"
        Me.chkChediNum.UseVisualStyleBackColor = True
        '
        'frmCheChiDim
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(247, 153)
        Me.Controls.Add(Me.chkChediNum)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.cmbUpSta)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbDownSta)
        Me.Controls.Add(Me.Label1)
        Me.Name = "frmCheChiDim"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "车次定义"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbDownSta As System.Windows.Forms.ComboBox
    Friend WithEvents cmbUpSta As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents chkChediNum As System.Windows.Forms.CheckBox
End Class
