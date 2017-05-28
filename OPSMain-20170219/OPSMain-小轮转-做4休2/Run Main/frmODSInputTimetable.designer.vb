<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmODSInputTimetable
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
        Me.btnBegin = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbLineInfo = New System.Windows.Forms.ComboBox()
        Me.cmbTrainDiamStyle = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbTrainDiaName = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpFirstTime = New System.Windows.Forms.DateTimePicker()
        Me.dtpEndTime = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtMakerDep = New System.Windows.Forms.TextBox()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.grpBox = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnOpen = New System.Windows.Forms.Button()
        Me.txtPath = New System.Windows.Forms.TextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.grpBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnBegin
        '
        Me.btnBegin.Location = New System.Drawing.Point(191, 222)
        Me.btnBegin.Name = "btnBegin"
        Me.btnBegin.Size = New System.Drawing.Size(102, 23)
        Me.btnBegin.TabIndex = 0
        Me.btnBegin.Text = "开始导入(&I)"
        Me.btnBegin.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 12)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "运行图所属线路:"
        '
        'cmbLineInfo
        '
        Me.cmbLineInfo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLineInfo.FormattingEnabled = True
        Me.cmbLineInfo.Location = New System.Drawing.Point(109, 15)
        Me.cmbLineInfo.Name = "cmbLineInfo"
        Me.cmbLineInfo.Size = New System.Drawing.Size(131, 20)
        Me.cmbLineInfo.TabIndex = 5
        '
        'cmbTrainDiamStyle
        '
        Me.cmbTrainDiamStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTrainDiamStyle.FormattingEnabled = True
        Me.cmbTrainDiamStyle.Items.AddRange(New Object() {"工作日", "节假日", "双休日"})
        Me.cmbTrainDiamStyle.Location = New System.Drawing.Point(332, 15)
        Me.cmbTrainDiamStyle.Name = "cmbTrainDiamStyle"
        Me.cmbTrainDiamStyle.Size = New System.Drawing.Size(151, 20)
        Me.cmbTrainDiamStyle.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(255, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 12)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "运行图类型:"
        '
        'cmbTrainDiaName
        '
        Me.cmbTrainDiaName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTrainDiaName.FormattingEnabled = True
        Me.cmbTrainDiaName.Location = New System.Drawing.Point(109, 45)
        Me.cmbTrainDiaName.Name = "cmbTrainDiaName"
        Me.cmbTrainDiaName.Size = New System.Drawing.Size(374, 20)
        Me.cmbTrainDiaName.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 48)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 12)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "可导入的运行图:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(20, 110)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 12)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "开始执行时间:"
        '
        'dtpFirstTime
        '
        Me.dtpFirstTime.Enabled = False
        Me.dtpFirstTime.Location = New System.Drawing.Point(109, 106)
        Me.dtpFirstTime.Name = "dtpFirstTime"
        Me.dtpFirstTime.Size = New System.Drawing.Size(141, 21)
        Me.dtpFirstTime.TabIndex = 13
        '
        'dtpEndTime
        '
        Me.dtpEndTime.Enabled = False
        Me.dtpEndTime.Location = New System.Drawing.Point(356, 106)
        Me.dtpEndTime.Name = "dtpEndTime"
        Me.dtpEndTime.Size = New System.Drawing.Size(127, 21)
        Me.dtpEndTime.TabIndex = 15
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(267, 110)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 12)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "结束执行时间:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(44, 77)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 12)
        Me.Label8.TabIndex = 16
        Me.Label8.Text = "编制部门:"
        '
        'txtMakerDep
        '
        Me.txtMakerDep.Location = New System.Drawing.Point(109, 77)
        Me.txtMakerDep.Name = "txtMakerDep"
        Me.txtMakerDep.ReadOnly = True
        Me.txtMakerDep.Size = New System.Drawing.Size(374, 21)
        Me.txtMakerDep.TabIndex = 17
        Me.txtMakerDep.Text = "运营公司"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(407, 221)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(89, 23)
        Me.btnExit.TabIndex = 18
        Me.btnExit.Text = "退出(&E)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'grpBox
        '
        Me.grpBox.Controls.Add(Me.txtMakerDep)
        Me.grpBox.Controls.Add(Me.Label8)
        Me.grpBox.Controls.Add(Me.dtpEndTime)
        Me.grpBox.Controls.Add(Me.Label7)
        Me.grpBox.Controls.Add(Me.dtpFirstTime)
        Me.grpBox.Controls.Add(Me.Label6)
        Me.grpBox.Controls.Add(Me.cmbTrainDiaName)
        Me.grpBox.Controls.Add(Me.Label4)
        Me.grpBox.Controls.Add(Me.cmbTrainDiamStyle)
        Me.grpBox.Controls.Add(Me.Label3)
        Me.grpBox.Controls.Add(Me.cmbLineInfo)
        Me.grpBox.Controls.Add(Me.Label2)
        Me.grpBox.Enabled = False
        Me.grpBox.Location = New System.Drawing.Point(4, 67)
        Me.grpBox.Name = "grpBox"
        Me.grpBox.Size = New System.Drawing.Size(492, 144)
        Me.grpBox.TabIndex = 23
        Me.grpBox.TabStop = False
        '
        'Label9
        '
        Me.Label9.ForeColor = System.Drawing.Color.Red
        Me.Label9.Location = New System.Drawing.Point(12, 221)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(163, 27)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "注：导入过程大概会花几分钟的时间，请耐心等待。"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(308, 221)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(83, 23)
        Me.Button1.TabIndex = 24
        Me.Button1.Text = "整理"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(443, 12)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(42, 23)
        Me.btnOpen.TabIndex = 27
        Me.btnOpen.Text = "..."
        Me.btnOpen.UseVisualStyleBackColor = True
        '
        'txtPath
        '
        Me.txtPath.Location = New System.Drawing.Point(102, 14)
        Me.txtPath.Name = "txtPath"
        Me.txtPath.Size = New System.Drawing.Size(329, 21)
        Me.txtPath.TabIndex = 25
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Location = New System.Drawing.Point(29, 17)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(66, 16)
        Me.CheckBox1.TabIndex = 28
        Me.CheckBox1.Text = "TPM文件"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(29, 47)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(84, 16)
        Me.CheckBox2.TabIndex = 29
        Me.CheckBox2.Text = "远端数据库"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.Location = New System.Drawing.Point(113, 43)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(80, 23)
        Me.Button2.TabIndex = 30
        Me.Button2.Text = "连接"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(203, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 31
        Me.Label1.Text = "状态："
        '
        'frmODSInputTimetable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(501, 259)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.CheckBox2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.txtPath)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.grpBox)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnBegin)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmODSInputTimetable"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "导入运行图"
        Me.grpBox.ResumeLayout(False)
        Me.grpBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBegin As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbLineInfo As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTrainDiamStyle As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbTrainDiaName As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpFirstTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpEndTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtMakerDep As System.Windows.Forms.TextBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents grpBox As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents txtPath As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
