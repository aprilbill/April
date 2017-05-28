<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmEditTriainStopTime
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
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbSta = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtShiJiTime = New System.Windows.Forms.TextBox
        Me.txtStopTime = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.grpSta = New System.Windows.Forms.GroupBox
        Me.grpSec = New System.Windows.Forms.GroupBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtSecTime = New System.Windows.Forms.TextBox
        Me.optMinus = New System.Windows.Forms.RadioButton
        Me.optAdd = New System.Windows.Forms.RadioButton
        Me.Label11 = New System.Windows.Forms.Label
        Me.cmbBiaoChi = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.cmbSecName = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtShiJiTime2 = New System.Windows.Forms.TextBox
        Me.txtGuDingTime = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.optStaStop = New System.Windows.Forms.RadioButton
        Me.optSecTime = New System.Windows.Forms.RadioButton
        Me.选择 = New System.Windows.Forms.GroupBox
        Me.grpSta.SuspendLayout()
        Me.grpSec.SuspendLayout()
        Me.选择.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(139, 244)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 72
        Me.btnOK.Text = "确定(&Y)"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(230, 244)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 73
        Me.btnExit.Text = "取消(&C)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 12)
        Me.Label1.TabIndex = 74
        Me.Label1.Text = "请选择车站名:"
        '
        'cmbSta
        '
        Me.cmbSta.FormattingEnabled = True
        Me.cmbSta.Location = New System.Drawing.Point(111, 23)
        Me.cmbSta.Name = "cmbSta"
        Me.cmbSta.Size = New System.Drawing.Size(170, 20)
        Me.cmbSta.TabIndex = 75
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 12)
        Me.Label2.TabIndex = 76
        Me.Label2.Text = "实际停站时间:"
        '
        'txtShiJiTime
        '
        Me.txtShiJiTime.Enabled = False
        Me.txtShiJiTime.Location = New System.Drawing.Point(111, 49)
        Me.txtShiJiTime.Name = "txtShiJiTime"
        Me.txtShiJiTime.Size = New System.Drawing.Size(127, 21)
        Me.txtShiJiTime.TabIndex = 77
        '
        'txtStopTime
        '
        Me.txtStopTime.Location = New System.Drawing.Point(111, 76)
        Me.txtStopTime.Name = "txtStopTime"
        Me.txtStopTime.Size = New System.Drawing.Size(127, 21)
        Me.txtStopTime.TabIndex = 79
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 12)
        Me.Label3.TabIndex = 78
        Me.Label3.Text = "实际停站时间:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(246, 56)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 12)
        Me.Label4.TabIndex = 80
        Me.Label4.Text = "分.秒"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(246, 83)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 12)
        Me.Label5.TabIndex = 81
        Me.Label5.Text = "分.秒"
        '
        'grpSta
        '
        Me.grpSta.Controls.Add(Me.Label1)
        Me.grpSta.Controls.Add(Me.Label5)
        Me.grpSta.Controls.Add(Me.cmbSta)
        Me.grpSta.Controls.Add(Me.Label4)
        Me.grpSta.Controls.Add(Me.Label2)
        Me.grpSta.Controls.Add(Me.txtStopTime)
        Me.grpSta.Controls.Add(Me.txtShiJiTime)
        Me.grpSta.Controls.Add(Me.Label3)
        Me.grpSta.Location = New System.Drawing.Point(13, 59)
        Me.grpSta.Name = "grpSta"
        Me.grpSta.Size = New System.Drawing.Size(292, 116)
        Me.grpSta.TabIndex = 82
        Me.grpSta.TabStop = False
        Me.grpSta.Text = "停站时分"
        '
        'grpSec
        '
        Me.grpSec.Controls.Add(Me.Label12)
        Me.grpSec.Controls.Add(Me.txtSecTime)
        Me.grpSec.Controls.Add(Me.optMinus)
        Me.grpSec.Controls.Add(Me.optAdd)
        Me.grpSec.Controls.Add(Me.Label11)
        Me.grpSec.Controls.Add(Me.cmbBiaoChi)
        Me.grpSec.Controls.Add(Me.Label6)
        Me.grpSec.Controls.Add(Me.Label7)
        Me.grpSec.Controls.Add(Me.cmbSecName)
        Me.grpSec.Controls.Add(Me.Label8)
        Me.grpSec.Controls.Add(Me.Label9)
        Me.grpSec.Controls.Add(Me.txtShiJiTime2)
        Me.grpSec.Controls.Add(Me.txtGuDingTime)
        Me.grpSec.Controls.Add(Me.Label10)
        Me.grpSec.Location = New System.Drawing.Point(7, 59)
        Me.grpSec.Name = "grpSec"
        Me.grpSec.Size = New System.Drawing.Size(298, 175)
        Me.grpSec.TabIndex = 83
        Me.grpSec.TabStop = False
        Me.grpSec.Text = "区间时分"
        Me.grpSec.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(246, 146)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(35, 12)
        Me.Label12.TabIndex = 87
        Me.Label12.Text = "分.秒"
        '
        'txtSecTime
        '
        Me.txtSecTime.Location = New System.Drawing.Point(171, 139)
        Me.txtSecTime.Name = "txtSecTime"
        Me.txtSecTime.Size = New System.Drawing.Size(67, 21)
        Me.txtSecTime.TabIndex = 86
        '
        'optMinus
        '
        Me.optMinus.AutoSize = True
        Me.optMinus.Location = New System.Drawing.Point(94, 144)
        Me.optMinus.Name = "optMinus"
        Me.optMinus.Size = New System.Drawing.Size(71, 16)
        Me.optMinus.TabIndex = 85
        Me.optMinus.Text = "减少时间"
        Me.optMinus.UseVisualStyleBackColor = True
        '
        'optAdd
        '
        Me.optAdd.AutoSize = True
        Me.optAdd.Checked = True
        Me.optAdd.Location = New System.Drawing.Point(17, 144)
        Me.optAdd.Name = "optAdd"
        Me.optAdd.Size = New System.Drawing.Size(71, 16)
        Me.optAdd.TabIndex = 84
        Me.optAdd.TabStop = True
        Me.optAdd.Text = "增加时间"
        Me.optAdd.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(15, 107)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(83, 12)
        Me.Label11.TabIndex = 83
        Me.Label11.Text = "选择区间标尺:"
        '
        'cmbBiaoChi
        '
        Me.cmbBiaoChi.FormattingEnabled = True
        Me.cmbBiaoChi.Location = New System.Drawing.Point(111, 104)
        Me.cmbBiaoChi.Name = "cmbBiaoChi"
        Me.cmbBiaoChi.Size = New System.Drawing.Size(170, 20)
        Me.cmbBiaoChi.TabIndex = 82
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 12)
        Me.Label6.TabIndex = 74
        Me.Label6.Text = "请选择区间名:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(246, 83)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 12)
        Me.Label7.TabIndex = 81
        Me.Label7.Text = "分.秒"
        '
        'cmbSecName
        '
        Me.cmbSecName.FormattingEnabled = True
        Me.cmbSecName.Location = New System.Drawing.Point(111, 23)
        Me.cmbSecName.Name = "cmbSecName"
        Me.cmbSecName.Size = New System.Drawing.Size(170, 20)
        Me.cmbSecName.TabIndex = 75
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(246, 56)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(35, 12)
        Me.Label8.TabIndex = 80
        Me.Label8.Text = "分.秒"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(16, 56)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(83, 12)
        Me.Label9.TabIndex = 76
        Me.Label9.Text = "固定区间时间:"
        '
        'txtShiJiTime2
        '
        Me.txtShiJiTime2.Enabled = False
        Me.txtShiJiTime2.Location = New System.Drawing.Point(111, 76)
        Me.txtShiJiTime2.Name = "txtShiJiTime2"
        Me.txtShiJiTime2.Size = New System.Drawing.Size(127, 21)
        Me.txtShiJiTime2.TabIndex = 79
        '
        'txtGuDingTime
        '
        Me.txtGuDingTime.Enabled = False
        Me.txtGuDingTime.Location = New System.Drawing.Point(111, 49)
        Me.txtGuDingTime.Name = "txtGuDingTime"
        Me.txtGuDingTime.Size = New System.Drawing.Size(127, 21)
        Me.txtGuDingTime.TabIndex = 77
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(16, 83)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(83, 12)
        Me.Label10.TabIndex = 78
        Me.Label10.Text = "实际区间时间:"
        '
        'optStaStop
        '
        Me.optStaStop.AutoSize = True
        Me.optStaStop.Checked = True
        Me.optStaStop.Location = New System.Drawing.Point(7, 20)
        Me.optStaStop.Name = "optStaStop"
        Me.optStaStop.Size = New System.Drawing.Size(95, 16)
        Me.optStaStop.TabIndex = 85
        Me.optStaStop.TabStop = True
        Me.optStaStop.Text = "修改车站时分"
        Me.optStaStop.UseVisualStyleBackColor = True
        '
        'optSecTime
        '
        Me.optSecTime.AutoSize = True
        Me.optSecTime.Location = New System.Drawing.Point(168, 20)
        Me.optSecTime.Name = "optSecTime"
        Me.optSecTime.Size = New System.Drawing.Size(95, 16)
        Me.optSecTime.TabIndex = 86
        Me.optSecTime.Text = "修改区间时分"
        Me.optSecTime.UseVisualStyleBackColor = True
        '
        '选择
        '
        Me.选择.Controls.Add(Me.optSecTime)
        Me.选择.Controls.Add(Me.optStaStop)
        Me.选择.Location = New System.Drawing.Point(7, 3)
        Me.选择.Name = "选择"
        Me.选择.Size = New System.Drawing.Size(296, 50)
        Me.选择.TabIndex = 87
        Me.选择.TabStop = False
        Me.选择.Text = "选择"
        '
        'frmEditTriainStopTime
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(313, 275)
        Me.Controls.Add(Me.选择)
        Me.Controls.Add(Me.grpSec)
        Me.Controls.Add(Me.grpSta)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEditTriainStopTime"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "修改时分"
        Me.grpSta.ResumeLayout(False)
        Me.grpSta.PerformLayout()
        Me.grpSec.ResumeLayout(False)
        Me.grpSec.PerformLayout()
        Me.选择.ResumeLayout(False)
        Me.选择.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbSta As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtShiJiTime As System.Windows.Forms.TextBox
    Friend WithEvents txtStopTime As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents grpSta As System.Windows.Forms.GroupBox
    Friend WithEvents grpSec As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbBiaoChi As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbSecName As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtShiJiTime2 As System.Windows.Forms.TextBox
    Friend WithEvents txtGuDingTime As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtSecTime As System.Windows.Forms.TextBox
    Friend WithEvents optMinus As System.Windows.Forms.RadioButton
    Friend WithEvents optAdd As System.Windows.Forms.RadioButton
    Friend WithEvents optStaStop As System.Windows.Forms.RadioButton
    Friend WithEvents optSecTime As System.Windows.Forms.RadioButton
    Friend WithEvents 选择 As System.Windows.Forms.GroupBox
End Class
