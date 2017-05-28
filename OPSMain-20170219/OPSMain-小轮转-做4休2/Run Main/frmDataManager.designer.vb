<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDataManager
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
        Me.BtnRefresh = New System.Windows.Forms.Button
        Me.TextboxSource = New System.Windows.Forms.TextBox
        Me.LabelSource = New System.Windows.Forms.Label
        Me.Cancel = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.TextBoxPassword = New System.Windows.Forms.TextBox
        Me.TextBoxUsername = New System.Windows.Forms.TextBox
        Me.PasswordLabel = New System.Windows.Forms.Label
        Me.UsernameLabel = New System.Windows.Forms.Label
        Me.ChkdListBox = New System.Windows.Forms.CheckedListBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.RadioBtn2 = New System.Windows.Forms.RadioButton
        Me.RadioBtn1 = New System.Windows.Forms.RadioButton
        Me.LabelList = New System.Windows.Forms.Label
        Me.BtnChkAll = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkSelect = New System.Windows.Forms.CheckedListBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnApply = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnRefresh
        '
        Me.BtnRefresh.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnRefresh.Location = New System.Drawing.Point(284, 288)
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(80, 23)
        Me.BtnRefresh.TabIndex = 25
        Me.BtnRefresh.Text = "刷新表名"
        '
        'TextboxSource
        '
        Me.TextboxSource.Location = New System.Drawing.Point(96, 84)
        Me.TextboxSource.Name = "TextboxSource"
        Me.TextboxSource.Size = New System.Drawing.Size(166, 21)
        Me.TextboxSource.TabIndex = 3
        Me.TextboxSource.Text = "uat"
        '
        'LabelSource
        '
        Me.LabelSource.Location = New System.Drawing.Point(4, 84)
        Me.LabelSource.Name = "LabelSource"
        Me.LabelSource.Size = New System.Drawing.Size(85, 23)
        Me.LabelSource.TabIndex = 19
        Me.LabelSource.Text = "连接字符串(&D)"
        Me.LabelSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Cancel
        '
        Me.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel.Location = New System.Drawing.Point(416, 326)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(80, 23)
        Me.Cancel.TabIndex = 18
        Me.Cancel.Text = "取消(&C)"
        '
        'OK
        '
        Me.OK.Location = New System.Drawing.Point(296, 326)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(80, 23)
        Me.OK.TabIndex = 17
        Me.OK.Text = "确定(&Y)"
        '
        'TextBoxPassword
        '
        Me.TextBoxPassword.Location = New System.Drawing.Point(96, 57)
        Me.TextBoxPassword.Name = "TextBoxPassword"
        Me.TextBoxPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TextBoxPassword.Size = New System.Drawing.Size(166, 21)
        Me.TextBoxPassword.TabIndex = 2
        Me.TextBoxPassword.Text = "uat"
        '
        'TextBoxUsername
        '
        Me.TextBoxUsername.Location = New System.Drawing.Point(96, 30)
        Me.TextBoxUsername.Name = "TextBoxUsername"
        Me.TextBoxUsername.Size = New System.Drawing.Size(166, 21)
        Me.TextBoxUsername.TabIndex = 1
        Me.TextBoxUsername.Text = "uat"
        '
        'PasswordLabel
        '
        Me.PasswordLabel.Location = New System.Drawing.Point(42, 57)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(60, 23)
        Me.PasswordLabel.TabIndex = 15
        Me.PasswordLabel.Text = "口令(&P)"
        Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UsernameLabel
        '
        Me.UsernameLabel.Location = New System.Drawing.Point(19, 31)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(79, 23)
        Me.UsernameLabel.TabIndex = 13
        Me.UsernameLabel.Text = "用户名称(&U)"
        Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ChkdListBox
        '
        Me.ChkdListBox.FormattingEnabled = True
        Me.ChkdListBox.Location = New System.Drawing.Point(280, 33)
        Me.ChkdListBox.Name = "ChkdListBox"
        Me.ChkdListBox.Size = New System.Drawing.Size(204, 244)
        Me.ChkdListBox.TabIndex = 26
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioBtn2)
        Me.GroupBox1.Controls.Add(Me.RadioBtn1)
        Me.GroupBox1.Location = New System.Drawing.Point(13, 269)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(249, 42)
        Me.GroupBox1.TabIndex = 27
        Me.GroupBox1.TabStop = False
        '
        'RadioBtn2
        '
        Me.RadioBtn2.AutoSize = True
        Me.RadioBtn2.Location = New System.Drawing.Point(111, 16)
        Me.RadioBtn2.Name = "RadioBtn2"
        Me.RadioBtn2.Size = New System.Drawing.Size(83, 16)
        Me.RadioBtn2.TabIndex = 2
        Me.RadioBtn2.Text = "数据库恢复"
        Me.RadioBtn2.UseVisualStyleBackColor = True
        '
        'RadioBtn1
        '
        Me.RadioBtn1.AutoSize = True
        Me.RadioBtn1.Checked = True
        Me.RadioBtn1.Location = New System.Drawing.Point(22, 16)
        Me.RadioBtn1.Name = "RadioBtn1"
        Me.RadioBtn1.Size = New System.Drawing.Size(83, 16)
        Me.RadioBtn1.TabIndex = 1
        Me.RadioBtn1.TabStop = True
        Me.RadioBtn1.Text = "数据库备份"
        Me.RadioBtn1.UseVisualStyleBackColor = True
        '
        'LabelList
        '
        Me.LabelList.AutoSize = True
        Me.LabelList.Location = New System.Drawing.Point(278, 15)
        Me.LabelList.Name = "LabelList"
        Me.LabelList.Size = New System.Drawing.Size(77, 12)
        Me.LabelList.TabIndex = 28
        Me.LabelList.Text = "数据表列表："
        '
        'BtnChkAll
        '
        Me.BtnChkAll.Location = New System.Drawing.Point(404, 288)
        Me.BtnChkAll.Name = "BtnChkAll"
        Me.BtnChkAll.Size = New System.Drawing.Size(80, 23)
        Me.BtnChkAll.TabIndex = 29
        Me.BtnChkAll.Text = "全选"
        Me.BtnChkAll.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 125)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "刷选："
        '
        'chkSelect
        '
        Me.chkSelect.FormattingEnabled = True
        Me.chkSelect.Items.AddRange(New Object() {"路网基础数据表", "客流相关数据表", "运行图相关数据表", "大客流处置相关数据表", "首末班车与换乘衔接数据表"})
        Me.chkSelect.Location = New System.Drawing.Point(13, 142)
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.Size = New System.Drawing.Size(249, 100)
        Me.chkSelect.TabIndex = 31
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnApply)
        Me.GroupBox2.Controls.Add(Me.chkSelect)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.BtnChkAll)
        Me.GroupBox2.Controls.Add(Me.LabelList)
        Me.GroupBox2.Controls.Add(Me.GroupBox1)
        Me.GroupBox2.Controls.Add(Me.ChkdListBox)
        Me.GroupBox2.Controls.Add(Me.BtnRefresh)
        Me.GroupBox2.Controls.Add(Me.TextboxSource)
        Me.GroupBox2.Controls.Add(Me.LabelSource)
        Me.GroupBox2.Controls.Add(Me.TextBoxPassword)
        Me.GroupBox2.Controls.Add(Me.TextBoxUsername)
        Me.GroupBox2.Controls.Add(Me.PasswordLabel)
        Me.GroupBox2.Controls.Add(Me.UsernameLabel)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(492, 318)
        Me.GroupBox2.TabIndex = 32
        Me.GroupBox2.TabStop = False
        '
        'btnApply
        '
        Me.btnApply.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnApply.Location = New System.Drawing.Point(190, 248)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(72, 23)
        Me.btnApply.TabIndex = 32
        Me.btnApply.Text = "应用"
        '
        'frmDataManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(516, 356)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDataManager"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "数据库管理"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BtnRefresh As System.Windows.Forms.Button
    Friend WithEvents TextboxSource As System.Windows.Forms.TextBox
    Friend WithEvents LabelSource As System.Windows.Forms.Label
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents TextBoxPassword As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxUsername As System.Windows.Forms.TextBox
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents UsernameLabel As System.Windows.Forms.Label
    Friend WithEvents ChkdListBox As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioBtn2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioBtn1 As System.Windows.Forms.RadioButton
    Friend WithEvents LabelList As System.Windows.Forms.Label
    Friend WithEvents BtnChkAll As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkSelect As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnApply As System.Windows.Forms.Button

End Class
