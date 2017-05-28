<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class newLeaveRequest
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(newLeaveRequest))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.LineTextBox = New System.Windows.Forms.TextBox()
        Me.DriverIDTextBox = New System.Windows.Forms.TextBox()
        Me.DriverNameTextBox = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.LeaveTreeView = New System.Windows.Forms.TreeView()
        Me.操作 = New System.Windows.Forms.GroupBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.AgendaRightMenue = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel2 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel3 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel4 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel5 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStripStatusLabel6 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ConfirmButton = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.DetailTextBox = New System.Windows.Forms.TextBox()
        Me.VacEnd = New System.Windows.Forms.DateTimePicker()
        Me.VacStart = New System.Windows.Forms.DateTimePicker()
        Me.ISVAC = New System.Windows.Forms.ComboBox()
        Me.VacTypeComboBox = New System.Windows.Forms.ComboBox()
        Me.ParaTextBox = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.月 = New System.Windows.Forms.TabPage()
        Me.NewMonthCalendar = New Pabo.Calendar.MonthCalendar()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.操作.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.月.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel1)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 16)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(158, 109)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "司机信息"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.LineTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DriverIDTextBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.DriverNameTextBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel3, 0, 2)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 17)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.9434!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.0566!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(150, 84)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'LineTextBox
        '
        Me.LineTextBox.Location = New System.Drawing.Point(78, 3)
        Me.LineTextBox.Name = "LineTextBox"
        Me.LineTextBox.Size = New System.Drawing.Size(69, 21)
        Me.LineTextBox.TabIndex = 0
        '
        'DriverIDTextBox
        '
        Me.DriverIDTextBox.Location = New System.Drawing.Point(78, 30)
        Me.DriverIDTextBox.Name = "DriverIDTextBox"
        Me.DriverIDTextBox.Size = New System.Drawing.Size(69, 21)
        Me.DriverIDTextBox.TabIndex = 1
        '
        'DriverNameTextBox
        '
        Me.DriverNameTextBox.Location = New System.Drawing.Point(78, 56)
        Me.DriverNameTextBox.Name = "DriverNameTextBox"
        Me.DriverNameTextBox.Size = New System.Drawing.Size(69, 21)
        Me.DriverNameTextBox.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(64, 21)
        Me.Panel1.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "线路号"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Location = New System.Drawing.Point(3, 30)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(64, 19)
        Me.Panel2.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 3)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "工号"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Location = New System.Drawing.Point(3, 56)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(64, 20)
        Me.Panel3.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "姓名"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.LeaveTreeView)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 145)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(154, 230)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "请假培训列表"
        '
        'LeaveTreeView
        '
        Me.LeaveTreeView.Location = New System.Drawing.Point(6, 20)
        Me.LeaveTreeView.Name = "LeaveTreeView"
        Me.LeaveTreeView.Size = New System.Drawing.Size(140, 204)
        Me.LeaveTreeView.TabIndex = 0
        '
        '操作
        '
        Me.操作.Controls.Add(Me.ToolStrip1)
        Me.操作.Location = New System.Drawing.Point(7, 415)
        Me.操作.Name = "操作"
        Me.操作.Size = New System.Drawing.Size(154, 73)
        Me.操作.TabIndex = 3
        Me.操作.TabStop = False
        Me.操作.Text = "操作"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripSeparator1, Me.ToolStripButton2, Me.ToolStripSeparator2, Me.ToolStripButton3, Me.ToolStripSeparator3})
        Me.ToolStrip1.Location = New System.Drawing.Point(3, 17)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(148, 56)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(36, 53)
        Me.ToolStripButton1.Text = "新增"
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 56)
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(36, 53)
        Me.ToolStripButton2.Text = "修改"
        Me.ToolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 56)
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(36, 53)
        Me.ToolStripButton3.Text = "删除"
        Me.ToolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 56)
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(16, 75)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(53, 12)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "结束时间"
        Me.Label7.UseWaitCursor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 46)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "开始时间"
        Me.Label6.UseWaitCursor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(217, 46)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 12)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "事由详情"
        Me.Label8.UseWaitCursor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(217, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "事由"
        Me.Label4.UseWaitCursor = True
        '
        'AgendaRightMenue
        '
        Me.AgendaRightMenue.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.AgendaRightMenue.Name = "AgendaRightMenue"
        Me.AgendaRightMenue.Size = New System.Drawing.Size(61, 4)
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.ToolStripStatusLabel2, Me.ToolStripStatusLabel3, Me.ToolStripStatusLabel4, Me.ToolStripStatusLabel5, Me.ToolStripStatusLabel6})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 491)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(687, 22)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(64, 17)
        Me.ToolStripStatusLabel1.Text = "              "
        '
        'ToolStripStatusLabel2
        '
        Me.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2"
        Me.ToolStripStatusLabel2.Size = New System.Drawing.Size(56, 17)
        Me.ToolStripStatusLabel2.Text = "            "
        '
        'ToolStripStatusLabel3
        '
        Me.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3"
        Me.ToolStripStatusLabel3.Size = New System.Drawing.Size(80, 17)
        Me.ToolStripStatusLabel3.Text = "当前批复状态"
        '
        'ToolStripStatusLabel4
        '
        Me.ToolStripStatusLabel4.Name = "ToolStripStatusLabel4"
        Me.ToolStripStatusLabel4.Size = New System.Drawing.Size(84, 17)
        Me.ToolStripStatusLabel4.Text = "                   "
        '
        'ToolStripStatusLabel5
        '
        Me.ToolStripStatusLabel5.Name = "ToolStripStatusLabel5"
        Me.ToolStripStatusLabel5.Size = New System.Drawing.Size(80, 17)
        Me.ToolStripStatusLabel5.Text = "当前操作为："
        '
        'ToolStripStatusLabel6
        '
        Me.ToolStripStatusLabel6.Name = "ToolStripStatusLabel6"
        Me.ToolStripStatusLabel6.Size = New System.Drawing.Size(80, 17)
        Me.ToolStripStatusLabel6.Text = "                  "
        '
        'ConfirmButton
        '
        Me.ConfirmButton.Location = New System.Drawing.Point(291, 84)
        Me.ConfirmButton.Name = "ConfirmButton"
        Me.ConfirmButton.Size = New System.Drawing.Size(75, 23)
        Me.ConfirmButton.TabIndex = 8
        Me.ConfirmButton.Text = "确认操作"
        Me.ConfirmButton.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.DetailTextBox)
        Me.GroupBox3.Controls.Add(Me.VacEnd)
        Me.GroupBox3.Controls.Add(Me.VacStart)
        Me.GroupBox3.Controls.Add(Me.ISVAC)
        Me.GroupBox3.Controls.Add(Me.VacTypeComboBox)
        Me.GroupBox3.Controls.Add(Me.ParaTextBox)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Cancel_Button)
        Me.GroupBox3.Controls.Add(Me.ConfirmButton)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Location = New System.Drawing.Point(179, 381)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(497, 111)
        Me.GroupBox3.TabIndex = 9
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "操作明细"
        '
        'DetailTextBox
        '
        Me.DetailTextBox.Location = New System.Drawing.Point(276, 46)
        Me.DetailTextBox.Multiline = True
        Me.DetailTextBox.Name = "DetailTextBox"
        Me.DetailTextBox.Size = New System.Drawing.Size(192, 32)
        Me.DetailTextBox.TabIndex = 12
        '
        'VacEnd
        '
        Me.VacEnd.Location = New System.Drawing.Point(75, 73)
        Me.VacEnd.Name = "VacEnd"
        Me.VacEnd.Size = New System.Drawing.Size(112, 21)
        Me.VacEnd.TabIndex = 11
        '
        'VacStart
        '
        Me.VacStart.Location = New System.Drawing.Point(75, 46)
        Me.VacStart.Name = "VacStart"
        Me.VacStart.Size = New System.Drawing.Size(112, 21)
        Me.VacStart.TabIndex = 11
        '
        'ISVAC
        '
        Me.ISVAC.FormattingEnabled = True
        Me.ISVAC.Items.AddRange(New Object() {"请假", "培训"})
        Me.ISVAC.Location = New System.Drawing.Point(75, 14)
        Me.ISVAC.Name = "ISVAC"
        Me.ISVAC.Size = New System.Drawing.Size(112, 20)
        Me.ISVAC.TabIndex = 10
        '
        'VacTypeComboBox
        '
        Me.VacTypeComboBox.FormattingEnabled = True
        Me.VacTypeComboBox.Location = New System.Drawing.Point(276, 14)
        Me.VacTypeComboBox.Name = "VacTypeComboBox"
        Me.VacTypeComboBox.Size = New System.Drawing.Size(112, 20)
        Me.VacTypeComboBox.TabIndex = 10
        '
        'ParaTextBox
        '
        Me.ParaTextBox.Location = New System.Drawing.Point(212, 84)
        Me.ParaTextBox.Name = "ParaTextBox"
        Me.ParaTextBox.Size = New System.Drawing.Size(27, 21)
        Me.ParaTextBox.TabIndex = 9
        Me.ParaTextBox.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "类型"
        Me.Label5.UseWaitCursor = True
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Location = New System.Drawing.Point(387, 84)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(75, 23)
        Me.Cancel_Button.TabIndex = 8
        Me.Cancel_Button.Text = "取消操作"
        Me.Cancel_Button.UseVisualStyleBackColor = True
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.月)
        Me.TabControl2.Location = New System.Drawing.Point(179, 16)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(508, 359)
        Me.TabControl2.TabIndex = 10
        '
        '月
        '
        Me.月.Controls.Add(Me.NewMonthCalendar)
        Me.月.Location = New System.Drawing.Point(4, 22)
        Me.月.Name = "月"
        Me.月.Padding = New System.Windows.Forms.Padding(3, 3, 3, 3)
        Me.月.Size = New System.Drawing.Size(500, 333)
        Me.月.TabIndex = 0
        Me.月.Text = "月"
        Me.月.UseVisualStyleBackColor = True
        '
        'NewMonthCalendar
        '
        Me.NewMonthCalendar.ActiveMonth.Month = 8
        Me.NewMonthCalendar.ActiveMonth.Year = 2011
        Me.NewMonthCalendar.Culture = New System.Globalization.CultureInfo("zh-CN")
        Me.NewMonthCalendar.Footer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.NewMonthCalendar.Header.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.NewMonthCalendar.Header.TextColor = System.Drawing.Color.White
        Me.NewMonthCalendar.ImageList = Nothing
        Me.NewMonthCalendar.Location = New System.Drawing.Point(0, 2)
        Me.NewMonthCalendar.MaxDate = New Date(2021, 8, 3, 20, 26, 13, 46)
        Me.NewMonthCalendar.MinDate = New Date(2001, 8, 3, 20, 26, 13, 46)
        Me.NewMonthCalendar.Month.BackgroundImage = Nothing
        Me.NewMonthCalendar.Month.DateFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.NewMonthCalendar.Month.TextFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.NewMonthCalendar.Name = "NewMonthCalendar"
        Me.NewMonthCalendar.Size = New System.Drawing.Size(497, 332)
        Me.NewMonthCalendar.TabIndex = 0
        Me.NewMonthCalendar.Weekdays.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.NewMonthCalendar.Weeknumbers.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        '
        'newLeaveRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(687, 513)
        Me.Controls.Add(Me.TabControl2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.操作)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "newLeaveRequest"
        Me.Text = "司机请假培训管理"
        Me.GroupBox1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.操作.ResumeLayout(False)
        Me.操作.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.TabControl2.ResumeLayout(False)
        Me.月.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents LineTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DriverIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DriverNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents LeaveTreeView As System.Windows.Forms.TreeView
    Friend WithEvents 操作 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AgendaRightMenue As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel2 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ToolStripStatusLabel3 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ConfirmButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents ToolStripStatusLabel4 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel5 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripStatusLabel6 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ParaTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents 月 As System.Windows.Forms.TabPage
    Friend WithEvents NewMonthCalendar As Pabo.Calendar.MonthCalendar
    Friend WithEvents VacTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents DetailTextBox As System.Windows.Forms.TextBox
    Friend WithEvents VacEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents VacStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents ISVAC As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
