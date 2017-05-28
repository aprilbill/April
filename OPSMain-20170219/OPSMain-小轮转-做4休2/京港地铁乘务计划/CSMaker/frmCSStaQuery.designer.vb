<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCSStaQuery
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCSStaQuery))
        Me.DataGrdStatQuery = New System.Windows.Forms.DataGridView()
        Me.BtnExit = New System.Windows.Forms.Button()
        Me.BtnChaXun = New System.Windows.Forms.Button()
        Me.BtnSave = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtTotalMember = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxtTotalDriveTime = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TxtDutyNum = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.StaSta = New System.Windows.Forms.ComboBox()
        Me.AveTime = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.MaxTime = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CSDriverNum = New System.Windows.Forms.TextBox()
        Me.MinTime = New System.Windows.Forms.TextBox()
        Me.VarTime = New System.Windows.Forms.TextBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.TextBoxpx = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.排序 = New System.Windows.Forms.Button()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.断开ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.优化处理ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.更改班种ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.早班ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.白班ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.夜班ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.日勤班ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.DataGrdStatQuery, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DataGrdStatQuery
        '
        Me.DataGrdStatQuery.AllowUserToAddRows = False
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.DataGrdStatQuery.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGrdStatQuery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGrdStatQuery.Location = New System.Drawing.Point(7, 20)
        Me.DataGrdStatQuery.MultiSelect = False
        Me.DataGrdStatQuery.Name = "DataGrdStatQuery"
        Me.DataGrdStatQuery.ReadOnly = True
        Me.DataGrdStatQuery.RowTemplate.Height = 23
        Me.DataGrdStatQuery.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGrdStatQuery.Size = New System.Drawing.Size(716, 302)
        Me.DataGrdStatQuery.TabIndex = 0
        '
        'BtnExit
        '
        Me.BtnExit.Location = New System.Drawing.Point(533, 20)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(119, 25)
        Me.BtnExit.TabIndex = 4
        Me.BtnExit.Text = "退出(&E)"
        Me.BtnExit.UseVisualStyleBackColor = True
        '
        'BtnChaXun
        '
        Me.BtnChaXun.Location = New System.Drawing.Point(163, 20)
        Me.BtnChaXun.Name = "BtnChaXun"
        Me.BtnChaXun.Size = New System.Drawing.Size(86, 25)
        Me.BtnChaXun.TabIndex = 3
        Me.BtnChaXun.Text = "更新(&Q)"
        Me.BtnChaXun.UseVisualStyleBackColor = True
        '
        'BtnSave
        '
        Me.BtnSave.Location = New System.Drawing.Point(334, 20)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(119, 25)
        Me.BtnSave.TabIndex = 2
        Me.BtnSave.Text = "保存为Excel(&S)"
        Me.BtnSave.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.BtnChaXun)
        Me.GroupBox3.Controls.Add(Me.BtnSave)
        Me.GroupBox3.Controls.Add(Me.BtnExit)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 556)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(733, 54)
        Me.GroupBox3.TabIndex = 16
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "操作"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.GroupBox2)
        Me.GroupBox4.Controls.Add(Me.GroupBox1)
        Me.GroupBox4.Location = New System.Drawing.Point(7, 396)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(733, 154)
        Me.GroupBox4.TabIndex = 17
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "指标"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.TxtTotalMember)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.TxtTotalDriveTime)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.TxtDutyNum)
        Me.GroupBox2.Location = New System.Drawing.Point(37, 10)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(686, 58)
        Me.GroupBox2.TabIndex = 30
        Me.GroupBox2.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(226, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 12)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "全天总任务数量："
        '
        'TxtTotalMember
        '
        Me.TxtTotalMember.Location = New System.Drawing.Point(583, 27)
        Me.TxtTotalMember.Name = "TxtTotalMember"
        Me.TxtTotalMember.ReadOnly = True
        Me.TxtTotalMember.Size = New System.Drawing.Size(73, 21)
        Me.TxtTotalMember.TabIndex = 30
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(476, 31)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(101, 12)
        Me.Label7.TabIndex = 27
        Me.Label7.Text = "乘务员运用总数："
        '
        'TxtTotalDriveTime
        '
        Me.TxtTotalDriveTime.Location = New System.Drawing.Point(122, 22)
        Me.TxtTotalDriveTime.Name = "TxtTotalDriveTime"
        Me.TxtTotalDriveTime.ReadOnly = True
        Me.TxtTotalDriveTime.Size = New System.Drawing.Size(73, 21)
        Me.TxtTotalDriveTime.TabIndex = 26
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(15, 27)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(101, 12)
        Me.Label10.TabIndex = 24
        Me.Label10.Text = "全天总驾驶公里："
        '
        'TxtDutyNum
        '
        Me.TxtDutyNum.Location = New System.Drawing.Point(333, 23)
        Me.TxtDutyNum.Name = "TxtDutyNum"
        Me.TxtDutyNum.ReadOnly = True
        Me.TxtDutyNum.Size = New System.Drawing.Size(73, 21)
        Me.TxtDutyNum.TabIndex = 28
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.StaSta)
        Me.GroupBox1.Controls.Add(Me.AveTime)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.MaxTime)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.CSDriverNum)
        Me.GroupBox1.Controls.Add(Me.MinTime)
        Me.GroupBox1.Controls.Add(Me.VarTime)
        Me.GroupBox1.Location = New System.Drawing.Point(37, 67)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(686, 81)
        Me.GroupBox1.TabIndex = 29
        Me.GroupBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(226, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 12)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "驾驶公里平均值："
        '
        'StaSta
        '
        Me.StaSta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.StaSta.FormattingEnabled = True
        Me.StaSta.Location = New System.Drawing.Point(122, 12)
        Me.StaSta.Name = "StaSta"
        Me.StaSta.Size = New System.Drawing.Size(71, 20)
        Me.StaSta.TabIndex = 25
        '
        'AveTime
        '
        Me.AveTime.Location = New System.Drawing.Point(333, 16)
        Me.AveTime.Name = "AveTime"
        Me.AveTime.ReadOnly = True
        Me.AveTime.Size = New System.Drawing.Size(73, 21)
        Me.AveTime.TabIndex = 21
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(51, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 12)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "统计口径："
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(39, 56)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 12)
        Me.Label11.TabIndex = 20
        Me.Label11.Text = "乘务员数量："
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(226, 56)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 12)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "驾驶公里标准差："
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(476, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(101, 12)
        Me.Label9.TabIndex = 27
        Me.Label9.Text = "驾驶公里最小值："
        '
        'MaxTime
        '
        Me.MaxTime.Location = New System.Drawing.Point(583, 15)
        Me.MaxTime.Name = "MaxTime"
        Me.MaxTime.ReadOnly = True
        Me.MaxTime.Size = New System.Drawing.Size(73, 21)
        Me.MaxTime.TabIndex = 26
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(476, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(101, 12)
        Me.Label8.TabIndex = 24
        Me.Label8.Text = "驾驶公里最大值："
        '
        'CSDriverNum
        '
        Me.CSDriverNum.Location = New System.Drawing.Point(122, 51)
        Me.CSDriverNum.Name = "CSDriverNum"
        Me.CSDriverNum.ReadOnly = True
        Me.CSDriverNum.Size = New System.Drawing.Size(73, 21)
        Me.CSDriverNum.TabIndex = 22
        '
        'MinTime
        '
        Me.MinTime.Location = New System.Drawing.Point(583, 51)
        Me.MinTime.Name = "MinTime"
        Me.MinTime.ReadOnly = True
        Me.MinTime.Size = New System.Drawing.Size(73, 21)
        Me.MinTime.TabIndex = 28
        '
        'VarTime
        '
        Me.VarTime.Location = New System.Drawing.Point(333, 51)
        Me.VarTime.Name = "VarTime"
        Me.VarTime.ReadOnly = True
        Me.VarTime.Size = New System.Drawing.Size(73, 21)
        Me.VarTime.TabIndex = 22
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.TextBoxpx)
        Me.GroupBox5.Controls.Add(Me.Button1)
        Me.GroupBox5.Controls.Add(Me.排序)
        Me.GroupBox5.Controls.Add(Me.DataGrdStatQuery)
        Me.GroupBox5.Location = New System.Drawing.Point(7, 8)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(733, 369)
        Me.GroupBox5.TabIndex = 18
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "统计结果"
        '
        'TextBoxpx
        '
        Me.TextBoxpx.Location = New System.Drawing.Point(20, 336)
        Me.TextBoxpx.Name = "TextBoxpx"
        Me.TextBoxpx.Size = New System.Drawing.Size(368, 21)
        Me.TextBoxpx.TabIndex = 30
        Me.TextBoxpx.Text = "驾驶公里"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(501, 337)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 29
        Me.Button1.Text = "排序"
        Me.Button1.UseVisualStyleBackColor = True
        '
        '排序
        '
        Me.排序.Location = New System.Drawing.Point(394, 336)
        Me.排序.Name = "排序"
        Me.排序.Size = New System.Drawing.Size(101, 23)
        Me.排序.TabIndex = 28
        Me.排序.Text = "选择字段..."
        Me.排序.UseVisualStyleBackColor = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.断开ToolStripMenuItem, Me.优化处理ToolStripMenuItem, Me.更改班种ToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(153, 92)
        '
        '断开ToolStripMenuItem
        '
        Me.断开ToolStripMenuItem.Name = "断开ToolStripMenuItem"
        Me.断开ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.断开ToolStripMenuItem.Text = "从中断开"
        '
        '优化处理ToolStripMenuItem
        '
        Me.优化处理ToolStripMenuItem.Name = "优化处理ToolStripMenuItem"
        Me.优化处理ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.优化处理ToolStripMenuItem.Text = "优化处理"
        '
        '更改班种ToolStripMenuItem
        '
        Me.更改班种ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.早班ToolStripMenuItem, Me.白班ToolStripMenuItem, Me.夜班ToolStripMenuItem, Me.日勤班ToolStripMenuItem})
        Me.更改班种ToolStripMenuItem.Name = "更改班种ToolStripMenuItem"
        Me.更改班种ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.更改班种ToolStripMenuItem.Text = "更改班种"
        '
        '早班ToolStripMenuItem
        '
        Me.早班ToolStripMenuItem.Name = "早班ToolStripMenuItem"
        Me.早班ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.早班ToolStripMenuItem.Text = "早班"
        '
        '白班ToolStripMenuItem
        '
        Me.白班ToolStripMenuItem.Name = "白班ToolStripMenuItem"
        Me.白班ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.白班ToolStripMenuItem.Text = "白班"
        '
        '夜班ToolStripMenuItem
        '
        Me.夜班ToolStripMenuItem.Name = "夜班ToolStripMenuItem"
        Me.夜班ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.夜班ToolStripMenuItem.Text = "夜班"
        '
        '日勤班ToolStripMenuItem
        '
        Me.日勤班ToolStripMenuItem.Name = "日勤班ToolStripMenuItem"
        Me.日勤班ToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.日勤班ToolStripMenuItem.Text = "日勤班"
        '
        'frmCSStaQuery
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(746, 613)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox4)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmCSStaQuery"
        Me.Text = "统计数据"
        CType(Me.DataGrdStatQuery, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGrdStatQuery As System.Windows.Forms.DataGridView
    Friend WithEvents BtnExit As System.Windows.Forms.Button
    Friend WithEvents BtnChaXun As System.Windows.Forms.Button
    Friend WithEvents BtnSave As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents StaSta As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents MinTime As System.Windows.Forms.TextBox
    Friend WithEvents VarTime As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents MaxTime As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents AveTime As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TxtTotalDriveTime As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TxtDutyNum As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtTotalMember As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents CSDriverNum As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents 排序 As System.Windows.Forms.Button
    Friend WithEvents TextBoxpx As System.Windows.Forms.TextBox
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 断开ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 优化处理ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 更改班种ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 早班ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 白班ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 夜班ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 日勤班ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
