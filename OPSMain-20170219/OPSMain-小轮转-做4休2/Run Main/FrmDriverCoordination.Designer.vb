<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDriverCoordination
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.CmbYunzhuan = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.CmbLine = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToDate = New System.Windows.Forms.DateTimePicker()
        Me.FromDate = New System.Windows.Forms.DateTimePicker()
        Me.LBExistPlan = New System.Windows.Forms.ListBox()
        Me.MonthCalendar = New Pabo.Calendar.MonthCalendar()
        Me.CanderContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.WorkDayCheck = New System.Windows.Forms.CheckBox()
        Me.WeekDayCheck = New System.Windows.Forms.CheckBox()
        Me.OneWeekCheck = New System.Windows.Forms.CheckBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.PropertyGrid1 = New System.Windows.Forms.PropertyGrid()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.DGVOtherDuty = New System.Windows.Forms.DataGridView()
        Me.编号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.班种 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.任务 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.起始时间 = New CS_CSMaker.frmCSMakeBasicSet.CalendarColumn()
        Me.结束时间 = New CS_CSMaker.frmCSMakeBasicSet.CalendarColumn()
        Me.驾驶公里 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DriverContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer5 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer6 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.DGVOtherDuty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.DriverContextMenu.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        Me.SplitContainer5.Panel1.SuspendLayout()
        Me.SplitContainer5.Panel2.SuspendLayout()
        Me.SplitContainer5.SuspendLayout()
        Me.SplitContainer6.Panel2.SuspendLayout()
        Me.SplitContainer6.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(973, 106)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "基本设置"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.CmbYunzhuan)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Location = New System.Drawing.Point(641, 28)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox4.Size = New System.Drawing.Size(316, 98)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "制度"
        '
        'CmbYunzhuan
        '
        Me.CmbYunzhuan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmbYunzhuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbYunzhuan.FormattingEnabled = True
        Me.CmbYunzhuan.Items.AddRange(New Object() {"四班两转", "五班三转", "六班三转"})
        Me.CmbYunzhuan.Location = New System.Drawing.Point(124, 41)
        Me.CmbYunzhuan.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CmbYunzhuan.Name = "CmbYunzhuan"
        Me.CmbYunzhuan.Size = New System.Drawing.Size(156, 23)
        Me.CmbYunzhuan.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(33, 48)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 15)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "运转制度:"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.CmbLine)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Location = New System.Drawing.Point(332, 28)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(301, 98)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "线路"
        '
        'CmbLine
        '
        Me.CmbLine.FormattingEnabled = True
        Me.CmbLine.Location = New System.Drawing.Point(75, 41)
        Me.CmbLine.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CmbLine.Name = "CmbLine"
        Me.CmbLine.Size = New System.Drawing.Size(217, 23)
        Me.CmbLine.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 46)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 15)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "线路:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.ToDate)
        Me.GroupBox2.Controls.Add(Me.FromDate)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 28)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(316, 98)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "日期"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 66)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "结束日期:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 32)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "起始日期:"
        '
        'ToDate
        '
        Me.ToDate.Location = New System.Drawing.Point(87, 59)
        Me.ToDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.Size = New System.Drawing.Size(207, 25)
        Me.ToDate.TabIndex = 0
        '
        'FromDate
        '
        Me.FromDate.Location = New System.Drawing.Point(87, 25)
        Me.FromDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.FromDate.Name = "FromDate"
        Me.FromDate.Size = New System.Drawing.Size(207, 25)
        Me.FromDate.TabIndex = 0
        '
        'LBExistPlan
        '
        Me.LBExistPlan.BackColor = System.Drawing.Color.SeaShell
        Me.LBExistPlan.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LBExistPlan.FormattingEnabled = True
        Me.LBExistPlan.ItemHeight = 15
        Me.LBExistPlan.Location = New System.Drawing.Point(4, 22)
        Me.LBExistPlan.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.LBExistPlan.Name = "LBExistPlan"
        Me.LBExistPlan.Size = New System.Drawing.Size(218, 286)
        Me.LBExistPlan.TabIndex = 1
        '
        'MonthCalendar
        '
        Me.MonthCalendar.ActiveMonth.Month = 9
        Me.MonthCalendar.ActiveMonth.Year = 2011
        Me.MonthCalendar.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Outset
        Me.MonthCalendar.ContextMenuStrip = Me.CanderContextMenu
        Me.MonthCalendar.Culture = New System.Globalization.CultureInfo("zh-CN")
        Me.MonthCalendar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.MonthCalendar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MonthCalendar.Footer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.MonthCalendar.Header.BackColor1 = System.Drawing.Color.Maroon
        Me.MonthCalendar.Header.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MonthCalendar.Header.TextColor = System.Drawing.Color.White
        Me.MonthCalendar.ImageList = Nothing
        Me.MonthCalendar.Location = New System.Drawing.Point(0, 0)
        Me.MonthCalendar.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MonthCalendar.MaxDate = New Date(2021, 8, 3, 20, 26, 13, 46)
        Me.MonthCalendar.MinDate = New Date(2001, 8, 3, 20, 26, 13, 46)
        Me.MonthCalendar.Month.BackgroundImage = Nothing
        Me.MonthCalendar.Month.BorderStyles.Focus = System.Windows.Forms.ButtonBorderStyle.Dotted
        Me.MonthCalendar.Month.BorderStyles.Normal = System.Windows.Forms.ButtonBorderStyle.Solid
        Me.MonthCalendar.Month.Colors.BackColor1 = System.Drawing.Color.DimGray
        Me.MonthCalendar.Month.Colors.BackColor2 = System.Drawing.Color.Gray
        Me.MonthCalendar.Month.Colors.Days.BackColor1 = System.Drawing.Color.WhiteSmoke
        Me.MonthCalendar.Month.Colors.Days.BackColor2 = System.Drawing.Color.Black
        Me.MonthCalendar.Month.Colors.Days.Text = System.Drawing.Color.DarkGreen
        Me.MonthCalendar.Month.Colors.Focus.BackColor = System.Drawing.Color.Red
        Me.MonthCalendar.Month.Colors.Focus.Border = System.Drawing.Color.Black
        Me.MonthCalendar.Month.Colors.Selected.BackColor = System.Drawing.Color.Red
        Me.MonthCalendar.Month.DateFont = New System.Drawing.Font("Times New Roman", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MonthCalendar.Month.TextAlign = Pabo.Calendar.mcItemAlign.BottomCenter
        Me.MonthCalendar.Month.TextFont = New System.Drawing.Font("华文新魏", 10.5!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.MonthCalendar.Name = "MonthCalendar"
        Me.MonthCalendar.Size = New System.Drawing.Size(726, 540)
        Me.MonthCalendar.TabIndex = 5
        Me.MonthCalendar.TodayColor = System.Drawing.Color.Maroon
        Me.MonthCalendar.Weekdays.BackColor1 = System.Drawing.Color.SeaShell
        Me.MonthCalendar.Weekdays.Font = New System.Drawing.Font("幼圆", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.MonthCalendar.Weekdays.TextColor = System.Drawing.Color.Maroon
        Me.MonthCalendar.Weeknumbers.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        '
        'CanderContextMenu
        '
        Me.CanderContextMenu.Name = "CanderContextMenu"
        Me.CanderContextMenu.Size = New System.Drawing.Size(61, 4)
        '
        'WorkDayCheck
        '
        Me.WorkDayCheck.AutoSize = True
        Me.WorkDayCheck.Location = New System.Drawing.Point(13, 18)
        Me.WorkDayCheck.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.WorkDayCheck.Name = "WorkDayCheck"
        Me.WorkDayCheck.Size = New System.Drawing.Size(104, 19)
        Me.WorkDayCheck.TabIndex = 6
        Me.WorkDayCheck.Text = "工作日一致"
        Me.WorkDayCheck.UseVisualStyleBackColor = True
        '
        'WeekDayCheck
        '
        Me.WeekDayCheck.AutoSize = True
        Me.WeekDayCheck.Location = New System.Drawing.Point(143, 18)
        Me.WeekDayCheck.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.WeekDayCheck.Name = "WeekDayCheck"
        Me.WeekDayCheck.Size = New System.Drawing.Size(89, 19)
        Me.WeekDayCheck.TabIndex = 6
        Me.WeekDayCheck.Text = "双休一致"
        Me.WeekDayCheck.UseVisualStyleBackColor = True
        '
        'OneWeekCheck
        '
        Me.OneWeekCheck.AutoSize = True
        Me.OneWeekCheck.Location = New System.Drawing.Point(247, 18)
        Me.OneWeekCheck.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.OneWeekCheck.Name = "OneWeekCheck"
        Me.OneWeekCheck.Size = New System.Drawing.Size(89, 19)
        Me.OneWeekCheck.TabIndex = 6
        Me.OneWeekCheck.Text = "每周一致"
        Me.OneWeekCheck.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(973, 617)
        Me.TabControl1.TabIndex = 7
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage1.Size = New System.Drawing.Size(965, 588)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "乘务计划设置"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(4, 4)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer1.Size = New System.Drawing.Size(957, 580)
        Me.SplitContainer1.SplitterDistance = 226
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 6
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.GroupBox5)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.PropertyGrid1)
        Me.SplitContainer2.Size = New System.Drawing.Size(226, 580)
        Me.SplitContainer2.SplitterDistance = 312
        Me.SplitContainer2.SplitterWidth = 5
        Me.SplitContainer2.TabIndex = 0
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.LBExistPlan)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox5.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox5.Size = New System.Drawing.Size(226, 312)
        Me.GroupBox5.TabIndex = 8
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "已有乘务计划"
        '
        'PropertyGrid1
        '
        Me.PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PropertyGrid1.Location = New System.Drawing.Point(0, 0)
        Me.PropertyGrid1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PropertyGrid1.Name = "PropertyGrid1"
        Me.PropertyGrid1.Size = New System.Drawing.Size(226, 263)
        Me.PropertyGrid1.TabIndex = 0
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.MonthCalendar)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.OneWeekCheck)
        Me.SplitContainer3.Panel2.Controls.Add(Me.WorkDayCheck)
        Me.SplitContainer3.Panel2.Controls.Add(Me.WeekDayCheck)
        Me.SplitContainer3.Size = New System.Drawing.Size(726, 580)
        Me.SplitContainer3.SplitterDistance = 540
        Me.SplitContainer3.SplitterWidth = 5
        Me.SplitContainer3.TabIndex = 6
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DGVOtherDuty)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TabPage2.Size = New System.Drawing.Size(965, 553)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "其它任务设置"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DGVOtherDuty
        '
        Me.DGVOtherDuty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVOtherDuty.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.编号, Me.班种, Me.任务, Me.起始时间, Me.结束时间, Me.驾驶公里})
        Me.DGVOtherDuty.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVOtherDuty.Location = New System.Drawing.Point(4, 4)
        Me.DGVOtherDuty.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DGVOtherDuty.Name = "DGVOtherDuty"
        Me.DGVOtherDuty.RowTemplate.Height = 23
        Me.DGVOtherDuty.Size = New System.Drawing.Size(957, 545)
        Me.DGVOtherDuty.TabIndex = 0
        '
        '编号
        '
        Me.编号.HeaderText = "编号"
        Me.编号.Name = "编号"
        '
        '班种
        '
        Me.班种.HeaderText = "班种"
        Me.班种.Name = "班种"
        '
        '任务
        '
        Me.任务.HeaderText = "任务"
        Me.任务.Name = "任务"
        '
        '起始时间
        '
        Me.起始时间.HeaderText = "起始时间"
        Me.起始时间.Name = "起始时间"
        Me.起始时间.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.起始时间.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        '结束时间
        '
        Me.结束时间.HeaderText = "结束时间"
        Me.结束时间.Name = "结束时间"
        Me.结束时间.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.结束时间.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        '驾驶公里
        '
        Me.驾驶公里.HeaderText = "驾驶公里"
        Me.驾驶公里.Name = "驾驶公里"
        '
        'DriverContextMenu
        '
        Me.DriverContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.ToolStripComboBox1})
        Me.DriverContextMenu.Name = "DriverContextMenu"
        Me.DriverContextMenu.ShowImageMargin = False
        Me.DriverContextMenu.Size = New System.Drawing.Size(157, 42)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(153, 6)
        '
        'ToolStripComboBox1
        '
        Me.ToolStripComboBox1.Name = "ToolStripComboBox1"
        Me.ToolStripComboBox1.Size = New System.Drawing.Size(121, 28)
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(5, 4)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(100, 29)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "保存"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Location = New System.Drawing.Point(108, 4)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(100, 29)
        Me.Button2.TabIndex = 10
        Me.Button2.Text = "取消"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.GroupBox1)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.SplitContainer5)
        Me.SplitContainer4.Size = New System.Drawing.Size(973, 765)
        Me.SplitContainer4.SplitterDistance = 106
        Me.SplitContainer4.SplitterWidth = 5
        Me.SplitContainer4.TabIndex = 11
        '
        'SplitContainer5
        '
        Me.SplitContainer5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer5.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer5.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer5.Name = "SplitContainer5"
        Me.SplitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer5.Panel1
        '
        Me.SplitContainer5.Panel1.Controls.Add(Me.TabControl1)
        '
        'SplitContainer5.Panel2
        '
        Me.SplitContainer5.Panel2.Controls.Add(Me.SplitContainer6)
        Me.SplitContainer5.Size = New System.Drawing.Size(973, 654)
        Me.SplitContainer5.SplitterDistance = 617
        Me.SplitContainer5.SplitterWidth = 5
        Me.SplitContainer5.TabIndex = 8
        '
        'SplitContainer6
        '
        Me.SplitContainer6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer6.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer6.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer6.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer6.Name = "SplitContainer6"
        '
        'SplitContainer6.Panel2
        '
        Me.SplitContainer6.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer6.Panel2.Controls.Add(Me.Button2)
        Me.SplitContainer6.Size = New System.Drawing.Size(973, 32)
        Me.SplitContainer6.SplitterDistance = 808
        Me.SplitContainer6.SplitterWidth = 5
        Me.SplitContainer6.TabIndex = 0
        '
        'FrmDriverCoordination
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(973, 765)
        Me.Controls.Add(Me.SplitContainer4)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmDriverCoordination"
        Me.Text = "参数设置"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        Me.SplitContainer3.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DGVOtherDuty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.DriverContextMenu.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        Me.SplitContainer5.Panel1.ResumeLayout(False)
        Me.SplitContainer5.Panel2.ResumeLayout(False)
        Me.SplitContainer5.ResumeLayout(False)
        Me.SplitContainer6.Panel2.ResumeLayout(False)
        Me.SplitContainer6.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents FromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CmbLine As System.Windows.Forms.ComboBox
    Friend WithEvents LBExistPlan As System.Windows.Forms.ListBox
    Friend WithEvents MonthCalendar As Pabo.Calendar.MonthCalendar
    Friend WithEvents WorkDayCheck As System.Windows.Forms.CheckBox
    Friend WithEvents WeekDayCheck As System.Windows.Forms.CheckBox
    Friend WithEvents OneWeekCheck As System.Windows.Forms.CheckBox
    Friend WithEvents CanderContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents PropertyGrid1 As System.Windows.Forms.PropertyGrid
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents DriverContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripComboBox1 As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer5 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer6 As System.Windows.Forms.SplitContainer
    Friend WithEvents CmbYunzhuan As System.Windows.Forms.ComboBox
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DGVOtherDuty As System.Windows.Forms.DataGridView
    Friend WithEvents 编号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 班种 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 任务 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend 起始时间 As CS_CSMaker.frmCSMakeBasicSet.CalendarColumn
    Friend 结束时间 As CS_CSMaker.frmCSMakeBasicSet.CalendarColumn
    Friend WithEvents 驾驶公里 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
