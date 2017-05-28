<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmStartTime
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
        Me.txtTime = New System.Windows.Forms.TextBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnOK = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.optRight = New System.Windows.Forms.RadioButton
        Me.numTime = New System.Windows.Forms.NumericUpDown
        Me.optLeft = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtFirstTime = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.chkMoveArriTime = New System.Windows.Forms.CheckBox
        Me.GroupBox1.SuspendLayout()
        CType(Me.numTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "请输入新的发点:"
        '
        'txtTime
        '
        Me.txtTime.Location = New System.Drawing.Point(107, 50)
        Me.txtTime.Name = "txtTime"
        Me.txtTime.Size = New System.Drawing.Size(119, 21)
        Me.txtTime.TabIndex = 1
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(246, 157)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 27)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "取消(&E)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(232, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 12)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "小时.分.秒"
        '
        'btnOK
        '
        Me.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOK.Location = New System.Drawing.Point(157, 157)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(73, 27)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "确定(&Y)"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.optRight)
        Me.GroupBox1.Controls.Add(Me.numTime)
        Me.GroupBox1.Controls.Add(Me.optLeft)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 97)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(306, 54)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "平移时间"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(233, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(16, 12)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "秒"
        '
        'optRight
        '
        Me.optRight.AutoSize = True
        Me.optRight.Location = New System.Drawing.Point(61, 22)
        Me.optRight.Name = "optRight"
        Me.optRight.Size = New System.Drawing.Size(46, 16)
        Me.optRight.TabIndex = 1
        Me.optRight.TabStop = False
        Me.optRight.Text = "右移"
        '
        'numTime
        '
        Me.numTime.Location = New System.Drawing.Point(125, 20)
        Me.numTime.Maximum = New Decimal(New Integer() {86401, 0, 0, 0})
        Me.numTime.Minimum = New Decimal(New Integer() {36000, 0, 0, -2147483648})
        Me.numTime.Name = "numTime"
        Me.numTime.Size = New System.Drawing.Size(102, 21)
        Me.numTime.TabIndex = 6
        '
        'optLeft
        '
        Me.optLeft.AutoSize = True
        Me.optLeft.Checked = True
        Me.optLeft.Location = New System.Drawing.Point(9, 22)
        Me.optLeft.Name = "optLeft"
        Me.optLeft.Size = New System.Drawing.Size(46, 16)
        Me.optLeft.TabIndex = 0
        Me.optLeft.Text = "左移"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtFirstTime)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtTime)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 7)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(306, 84)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "输入发点"
        '
        'txtFirstTime
        '
        Me.txtFirstTime.Enabled = False
        Me.txtFirstTime.Location = New System.Drawing.Point(107, 20)
        Me.txtFirstTime.Name = "txtFirstTime"
        Me.txtFirstTime.Size = New System.Drawing.Size(119, 21)
        Me.txtFirstTime.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(232, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 12)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "小时.分.秒"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 12)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "原始发点:"
        '
        'chkMoveArriTime
        '
        Me.chkMoveArriTime.AutoSize = True
        Me.chkMoveArriTime.Location = New System.Drawing.Point(12, 163)
        Me.chkMoveArriTime.Name = "chkMoveArriTime"
        Me.chkMoveArriTime.Size = New System.Drawing.Size(119, 16)
        Me.chkMoveArriTime.TabIndex = 8
        Me.chkMoveArriTime.Text = "同时移动该站到点"
        '
        'frmStartTime
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(329, 196)
        Me.Controls.Add(Me.chkMoveArriTime)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmStartTime"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "调整发点"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.numTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTime As System.Windows.Forms.TextBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents optLeft As System.Windows.Forms.RadioButton
    Friend WithEvents optRight As System.Windows.Forms.RadioButton
    Friend WithEvents numTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFirstTime As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkMoveArriTime As System.Windows.Forms.CheckBox
End Class
