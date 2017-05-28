<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmDrawSingleTrain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbJiaoLuName = New System.Windows.Forms.ComboBox
        Me.cmbStopBiaoChi = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbRunBiaoChi = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtStartTime = New System.Windows.Forms.TextBox
        Me.btnOk = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "请选择列车交路名称:"
        '
        'cmbJiaoLuName
        '
        Me.cmbJiaoLuName.FormattingEnabled = True
        Me.cmbJiaoLuName.Location = New System.Drawing.Point(139, 24)
        Me.cmbJiaoLuName.Name = "cmbJiaoLuName"
        Me.cmbJiaoLuName.Size = New System.Drawing.Size(194, 20)
        Me.cmbJiaoLuName.TabIndex = 1
        '
        'cmbStopBiaoChi
        '
        Me.cmbStopBiaoChi.FormattingEnabled = True
        Me.cmbStopBiaoChi.Location = New System.Drawing.Point(139, 76)
        Me.cmbStopBiaoChi.Name = "cmbStopBiaoChi"
        Me.cmbStopBiaoChi.Size = New System.Drawing.Size(194, 20)
        Me.cmbStopBiaoChi.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 79)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(119, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "请选择列车停站标尺:"
        '
        'cmbRunBiaoChi
        '
        Me.cmbRunBiaoChi.FormattingEnabled = True
        Me.cmbRunBiaoChi.Location = New System.Drawing.Point(139, 50)
        Me.cmbRunBiaoChi.Name = "cmbRunBiaoChi"
        Me.cmbRunBiaoChi.Size = New System.Drawing.Size(194, 20)
        Me.cmbRunBiaoChi.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(119, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "请选择列车运行标尺:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 105)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 12)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "请输入发车时间:"
        '
        'txtStartTime
        '
        Me.txtStartTime.Location = New System.Drawing.Point(139, 102)
        Me.txtStartTime.Name = "txtStartTime"
        Me.txtStartTime.Size = New System.Drawing.Size(194, 21)
        Me.txtStartTime.TabIndex = 7
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(139, 139)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(89, 24)
        Me.btnOk.TabIndex = 8
        Me.btnOk.Text = "确定(&O)"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(244, 139)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(89, 24)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "取消(&C)"
        '
        'frmDrawSingleTrain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(349, 175)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.txtStartTime)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbRunBiaoChi)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbStopBiaoChi)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbJiaoLuName)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDrawSingleTrain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "添加列车"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbJiaoLuName As System.Windows.Forms.ComboBox
    Friend WithEvents cmbStopBiaoChi As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbRunBiaoChi As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtStartTime As System.Windows.Forms.TextBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
