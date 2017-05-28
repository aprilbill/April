<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmODSTraindiagramSelect
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
        Me.ComboBox = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboDiagram = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Comline = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnYes = New System.Windows.Forms.Button()
        Me.BtnNo = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ComboBox
        '
        Me.ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox.FormattingEnabled = True
        Me.ComboBox.Location = New System.Drawing.Point(106, 74)
        Me.ComboBox.Name = "ComboBox"
        Me.ComboBox.Size = New System.Drawing.Size(218, 20)
        Me.ComboBox.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(25, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "运行图名称："
        '
        'ComboDiagram
        '
        Me.ComboDiagram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboDiagram.FormattingEnabled = True
        Me.ComboDiagram.Location = New System.Drawing.Point(106, 43)
        Me.ComboDiagram.Name = "ComboDiagram"
        Me.ComboDiagram.Size = New System.Drawing.Size(161, 20)
        Me.ComboDiagram.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "运行图类型："
        '
        'Comline
        '
        Me.Comline.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Comline.FormattingEnabled = True
        Me.Comline.Location = New System.Drawing.Point(106, 12)
        Me.Comline.Name = "Comline"
        Me.Comline.Size = New System.Drawing.Size(161, 20)
        Me.Comline.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(35, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "线路名称："
        '
        'BtnYes
        '
        Me.BtnYes.Location = New System.Drawing.Point(125, 112)
        Me.BtnYes.Name = "BtnYes"
        Me.BtnYes.Size = New System.Drawing.Size(85, 23)
        Me.BtnYes.TabIndex = 1
        Me.BtnYes.Text = "确定(&Y)"
        Me.BtnYes.UseVisualStyleBackColor = True
        '
        'BtnNo
        '
        Me.BtnNo.Location = New System.Drawing.Point(240, 112)
        Me.BtnNo.Name = "BtnNo"
        Me.BtnNo.Size = New System.Drawing.Size(84, 23)
        Me.BtnNo.TabIndex = 2
        Me.BtnNo.Text = "退出(&E)"
        Me.BtnNo.UseVisualStyleBackColor = True
        '
        'FrmODSTraindiagramSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(336, 146)
        Me.Controls.Add(Me.ComboBox)
        Me.Controls.Add(Me.BtnNo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.BtnYes)
        Me.Controls.Add(Me.ComboDiagram)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Comline)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmODSTraindiagramSelect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "打开运行图"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ComboDiagram As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Comline As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnYes As System.Windows.Forms.Button
    Friend WithEvents BtnNo As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBox As System.Windows.Forms.ComboBox
End Class
