<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUserManager
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BtnCloseRegister = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.CmbLineName = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.CmbNewDepart = New System.Windows.Forms.ComboBox()
        Me.TXTNewConfrimPwd = New System.Windows.Forms.TextBox()
        Me.TXTNewPwd = New System.Windows.Forms.TextBox()
        Me.TXTNewName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.EditUser = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.修改用户信息MToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.删除用户信息DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.TTLineName = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CmbDepart = New System.Windows.Forms.ComboBox()
        Me.TTComfirmPwd = New System.Windows.Forms.TextBox()
        Me.TTPwd = New System.Windows.Forms.TextBox()
        Me.TTName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DGVUser = New System.Windows.Forms.DataGridView()
        Me.线路名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.用户名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.密码 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.用户角色 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.BtnRegister = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.EditUser.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DGVUser, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.BtnCloseRegister)
        Me.Panel1.Controls.Add(Me.Button2)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(681, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(229, 358)
        Me.Panel1.TabIndex = 3
        '
        'BtnCloseRegister
        '
        Me.BtnCloseRegister.Location = New System.Drawing.Point(7, 265)
        Me.BtnCloseRegister.Name = "BtnCloseRegister"
        Me.BtnCloseRegister.Size = New System.Drawing.Size(103, 23)
        Me.BtnCloseRegister.TabIndex = 0
        Me.BtnCloseRegister.Text = "<< 关闭注册(&C)"
        Me.BtnCloseRegister.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(132, 265)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(90, 23)
        Me.Button2.TabIndex = 0
        Me.Button2.Text = "提交注册(&P)"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.CmbLineName)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.CmbNewDepart)
        Me.GroupBox2.Controls.Add(Me.TXTNewConfrimPwd)
        Me.GroupBox2.Controls.Add(Me.TXTNewPwd)
        Me.GroupBox2.Controls.Add(Me.TXTNewName)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(215, 244)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "新用户信息"
        '
        'CmbLineName
        '
        Me.CmbLineName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbLineName.FormattingEnabled = True
        Me.CmbLineName.Location = New System.Drawing.Point(85, 31)
        Me.CmbLineName.Name = "CmbLineName"
        Me.CmbLineName.Size = New System.Drawing.Size(109, 20)
        Me.CmbLineName.TabIndex = 6
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(28, 34)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(41, 12)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "线路名"
        '
        'CmbNewDepart
        '
        Me.CmbNewDepart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbNewDepart.FormattingEnabled = True
        Me.CmbNewDepart.Items.AddRange(New Object() {"高级管理员", "中心管理员", "车间运用管理", "车间管理员"})
        Me.CmbNewDepart.Location = New System.Drawing.Point(85, 187)
        Me.CmbNewDepart.Name = "CmbNewDepart"
        Me.CmbNewDepart.Size = New System.Drawing.Size(109, 20)
        Me.CmbNewDepart.TabIndex = 3
        '
        'TXTNewConfrimPwd
        '
        Me.TXTNewConfrimPwd.Location = New System.Drawing.Point(85, 147)
        Me.TXTNewConfrimPwd.Name = "TXTNewConfrimPwd"
        Me.TXTNewConfrimPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TXTNewConfrimPwd.Size = New System.Drawing.Size(109, 21)
        Me.TXTNewConfrimPwd.TabIndex = 1
        '
        'TXTNewPwd
        '
        Me.TXTNewPwd.Location = New System.Drawing.Point(85, 109)
        Me.TXTNewPwd.Name = "TXTNewPwd"
        Me.TXTNewPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TXTNewPwd.Size = New System.Drawing.Size(109, 21)
        Me.TXTNewPwd.TabIndex = 1
        '
        'TXTNewName
        '
        Me.TXTNewName.Location = New System.Drawing.Point(85, 70)
        Me.TXTNewName.Name = "TXTNewName"
        Me.TXTNewName.Size = New System.Drawing.Size(109, 21)
        Me.TXTNewName.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 192)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "用户角色"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 150)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "确认密码"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 114)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "登录密码"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "用户名"
        '
        'EditUser
        '
        Me.EditUser.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.修改用户信息MToolStripMenuItem, Me.删除用户信息DToolStripMenuItem})
        Me.EditUser.Name = "EditUser"
        Me.EditUser.Size = New System.Drawing.Size(169, 48)
        '
        '修改用户信息MToolStripMenuItem
        '
        Me.修改用户信息MToolStripMenuItem.Name = "修改用户信息MToolStripMenuItem"
        Me.修改用户信息MToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.修改用户信息MToolStripMenuItem.Text = "修改用户信息(&M)"
        '
        '删除用户信息DToolStripMenuItem
        '
        Me.删除用户信息DToolStripMenuItem.Name = "删除用户信息DToolStripMenuItem"
        Me.删除用户信息DToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.删除用户信息DToolStripMenuItem.Text = "删除用户信息(&D)"
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.Button4)
        Me.Panel2.Controls.Add(Me.Button3)
        Me.Panel2.Controls.Add(Me.GroupBox3)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(449, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(232, 358)
        Me.Panel2.TabIndex = 7
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(5, 265)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(106, 23)
        Me.Button4.TabIndex = 0
        Me.Button4.Text = "<< 关闭修改(&C)"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(134, 265)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(90, 23)
        Me.Button3.TabIndex = 0
        Me.Button3.Text = "提交修改(&P)"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.TTLineName)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.CmbDepart)
        Me.GroupBox3.Controls.Add(Me.TTComfirmPwd)
        Me.GroupBox3.Controls.Add(Me.TTPwd)
        Me.GroupBox3.Controls.Add(Me.TTName)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Location = New System.Drawing.Point(5, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(219, 244)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "用户信息"
        '
        'TTLineName
        '
        Me.TTLineName.Location = New System.Drawing.Point(83, 31)
        Me.TTLineName.Name = "TTLineName"
        Me.TTLineName.ReadOnly = True
        Me.TTLineName.Size = New System.Drawing.Size(109, 21)
        Me.TTLineName.TabIndex = 4
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(28, 34)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(41, 12)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "线路名"
        '
        'CmbDepart
        '
        Me.CmbDepart.Enabled = False
        Me.CmbDepart.FormattingEnabled = True
        Me.CmbDepart.Items.AddRange(New Object() {"高级管理员", "中心管理员", "车间运营管理", "车间管理员"})
        Me.CmbDepart.Location = New System.Drawing.Point(83, 187)
        Me.CmbDepart.Name = "CmbDepart"
        Me.CmbDepart.Size = New System.Drawing.Size(109, 20)
        Me.CmbDepart.TabIndex = 2
        '
        'TTComfirmPwd
        '
        Me.TTComfirmPwd.Location = New System.Drawing.Point(83, 147)
        Me.TTComfirmPwd.Name = "TTComfirmPwd"
        Me.TTComfirmPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TTComfirmPwd.Size = New System.Drawing.Size(109, 21)
        Me.TTComfirmPwd.TabIndex = 1
        '
        'TTPwd
        '
        Me.TTPwd.Location = New System.Drawing.Point(83, 109)
        Me.TTPwd.Name = "TTPwd"
        Me.TTPwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TTPwd.Size = New System.Drawing.Size(109, 21)
        Me.TTPwd.TabIndex = 1
        '
        'TTName
        '
        Me.TTName.Location = New System.Drawing.Point(83, 70)
        Me.TTName.Name = "TTName"
        Me.TTName.ReadOnly = True
        Me.TTName.Size = New System.Drawing.Size(109, 21)
        Me.TTName.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 191)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "用户角色"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 154)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "确认密码"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(28, 114)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 12)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "新密码"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(28, 73)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 12)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "用户名"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.BtnRegister)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer1.Size = New System.Drawing.Size(449, 358)
        Me.SplitContainer1.SplitterDistance = 316
        Me.SplitContainer1.TabIndex = 8
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DGVUser)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(449, 316)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "用户列表"
        '
        'DGVUser
        '
        Me.DGVUser.AllowUserToAddRows = False
        Me.DGVUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVUser.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.线路名, Me.用户名, Me.密码, Me.用户角色})
        Me.DGVUser.ContextMenuStrip = Me.EditUser
        Me.DGVUser.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVUser.Location = New System.Drawing.Point(3, 17)
        Me.DGVUser.Name = "DGVUser"
        Me.DGVUser.ReadOnly = True
        Me.DGVUser.RowHeadersVisible = False
        Me.DGVUser.RowTemplate.Height = 23
        Me.DGVUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGVUser.Size = New System.Drawing.Size(443, 296)
        Me.DGVUser.TabIndex = 1
        '
        '线路名
        '
        Me.线路名.HeaderText = "线路名"
        Me.线路名.Name = "线路名"
        Me.线路名.ReadOnly = True
        '
        '用户名
        '
        Me.用户名.HeaderText = "用户名"
        Me.用户名.Name = "用户名"
        Me.用户名.ReadOnly = True
        '
        '密码
        '
        Me.密码.HeaderText = "密码"
        Me.密码.Name = "密码"
        Me.密码.ReadOnly = True
        '
        '用户角色
        '
        Me.用户角色.HeaderText = "用户角色"
        Me.用户角色.Name = "用户角色"
        Me.用户角色.ReadOnly = True
        '
        'BtnRegister
        '
        Me.BtnRegister.Location = New System.Drawing.Point(301, 6)
        Me.BtnRegister.Name = "BtnRegister"
        Me.BtnRegister.Size = New System.Drawing.Size(118, 23)
        Me.BtnRegister.TabIndex = 0
        Me.BtnRegister.Text = "注册新用户(&R) >>"
        Me.BtnRegister.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(34, 6)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(84, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "退出(&E)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'FrmUserManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(910, 358)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmUserManager"
        Me.Text = "系统用户管理"
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.EditUser.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.DGVUser, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TXTNewConfrimPwd As System.Windows.Forms.TextBox
    Friend WithEvents TXTNewPwd As System.Windows.Forms.TextBox
    Friend WithEvents TXTNewName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents EditUser As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 修改用户信息MToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 删除用户信息DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BtnCloseRegister As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents TTComfirmPwd As System.Windows.Forms.TextBox
    Friend WithEvents TTPwd As System.Windows.Forms.TextBox
    Friend WithEvents TTName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents DGVUser As System.Windows.Forms.DataGridView
    Friend WithEvents BtnRegister As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents CmbNewDepart As System.Windows.Forms.ComboBox
    Friend WithEvents CmbDepart As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TTLineName As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CmbLineName As System.Windows.Forms.ComboBox
    Friend WithEvents 线路名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 用户名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 密码 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 用户角色 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
