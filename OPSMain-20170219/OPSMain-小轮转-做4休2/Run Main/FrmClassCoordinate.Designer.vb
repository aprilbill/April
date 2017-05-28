<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmClassCoordinate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmClassCoordinate))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.TSBOpen = New System.Windows.Forms.ToolStripButton()
        Me.TSB_Save = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSBSetting = New System.Windows.Forms.ToolStripButton()
        Me.TSB_AssignFirstDayDuty = New System.Windows.Forms.ToolStripButton()
        Me.TSBAssignDuty = New System.Windows.Forms.ToolStripButton()
        Me.TSBRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSBOutPutExcel = New System.Windows.Forms.ToolStripButton()
        Me.TSB_DutyDetailOutput = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSBShowErrorInfo = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TBEndTime = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TBStartTime = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TBPreTrain = New System.Windows.Forms.TextBox()
        Me.TBLine = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TBYunzhuan = New System.Windows.Forms.TextBox()
        Me.TBDriverNum = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.TreeDrivers = New System.Windows.Forms.TreeView()
        Me.Tabpages = New System.Windows.Forms.TabControl()
        Me.乘务轮值匹配 = New System.Windows.Forms.TabPage()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.TabControlMain = New System.Windows.Forms.TabControl()
        Me.GBErrorInfo = New System.Windows.Forms.GroupBox()
        Me.DGVErrorInfo = New System.Windows.Forms.DataGridView()
        Me.编号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.日期 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.未分配任务 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.表号任务查询 = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.DGVTimetable = New System.Windows.Forms.DataGridView()
        Me.任务号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.班种 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.车次 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.开始时间 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.开始站名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.结束时间 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.结束站名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.TTXTDutyNo = New System.Windows.Forms.ToolStripTextBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.TimetablePicker = New System.Windows.Forms.DateTimePicker()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.BtnQuery = New System.Windows.Forms.Button()
        Me.交换任务EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.任务详情DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CMUChangeDuty = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.分配空闲任务FToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.分配其它任务OToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.删除任务DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.StatusStripMain = New System.Windows.Forms.StatusStrip()
        Me.LBEdit = New System.Windows.Forms.ToolStripStatusLabel()
        Me.LBModify = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ToolStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.Tabpages.SuspendLayout()
        Me.乘务轮值匹配.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GBErrorInfo.SuspendLayout()
        CType(Me.DGVErrorInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.表号任务查询.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.DGVTimetable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.CMUChangeDuty.SuspendLayout()
        Me.StatusStripMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TSBOpen, Me.TSB_Save, Me.ToolStripSeparator2, Me.TSBSetting, Me.TSB_AssignFirstDayDuty, Me.TSBAssignDuty, Me.TSBRefresh, Me.ToolStripSeparator3, Me.TSBOutPutExcel, Me.TSB_DutyDetailOutput, Me.ToolStripSeparator4, Me.TSBShowErrorInfo})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1055, 25)
        Me.ToolStrip1.TabIndex = 12
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'TSBOpen
        '
        Me.TSBOpen.Image = CType(resources.GetObject("TSBOpen.Image"), System.Drawing.Image)
        Me.TSBOpen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBOpen.Name = "TSBOpen"
        Me.TSBOpen.Size = New System.Drawing.Size(70, 22)
        Me.TSBOpen.Text = "打开(&O)"
        Me.TSBOpen.ToolTipText = "管理既有计划"
        '
        'TSB_Save
        '
        Me.TSB_Save.Image = CType(resources.GetObject("TSB_Save.Image"), System.Drawing.Image)
        Me.TSB_Save.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSB_Save.Name = "TSB_Save"
        Me.TSB_Save.Size = New System.Drawing.Size(67, 22)
        Me.TSB_Save.Text = "保存(&S)"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'TSBSetting
        '
        Me.TSBSetting.Image = CType(resources.GetObject("TSBSetting.Image"), System.Drawing.Image)
        Me.TSBSetting.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBSetting.Name = "TSBSetting"
        Me.TSBSetting.Size = New System.Drawing.Size(52, 22)
        Me.TSBSetting.Text = "设置"
        Me.TSBSetting.ToolTipText = "参数设置"
        '
        'TSB_AssignFirstDayDuty
        '
        Me.TSB_AssignFirstDayDuty.Image = CType(resources.GetObject("TSB_AssignFirstDayDuty.Image"), System.Drawing.Image)
        Me.TSB_AssignFirstDayDuty.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSB_AssignFirstDayDuty.Name = "TSB_AssignFirstDayDuty"
        Me.TSB_AssignFirstDayDuty.Size = New System.Drawing.Size(90, 22)
        Me.TSB_AssignFirstDayDuty.Text = "首日任务(&F)"
        Me.TSB_AssignFirstDayDuty.ToolTipText = "指定首日班种及任务"
        '
        'TSBAssignDuty
        '
        Me.TSBAssignDuty.Image = CType(resources.GetObject("TSBAssignDuty.Image"), System.Drawing.Image)
        Me.TSBAssignDuty.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBAssignDuty.Name = "TSBAssignDuty"
        Me.TSBAssignDuty.Size = New System.Drawing.Size(92, 22)
        Me.TSBAssignDuty.Text = "任务分配(&A)"
        '
        'TSBRefresh
        '
        Me.TSBRefresh.Image = CType(resources.GetObject("TSBRefresh.Image"), System.Drawing.Image)
        Me.TSBRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBRefresh.Name = "TSBRefresh"
        Me.TSBRefresh.Size = New System.Drawing.Size(68, 22)
        Me.TSBRefresh.Text = "刷新(&R)"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'TSBOutPutExcel
        '
        Me.TSBOutPutExcel.Image = CType(resources.GetObject("TSBOutPutExcel.Image"), System.Drawing.Image)
        Me.TSBOutPutExcel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBOutPutExcel.Name = "TSBOutPutExcel"
        Me.TSBOutPutExcel.Size = New System.Drawing.Size(88, 22)
        Me.TSBOutPutExcel.Text = "排班表输出"
        '
        'TSB_DutyDetailOutput
        '
        Me.TSB_DutyDetailOutput.Image = CType(resources.GetObject("TSB_DutyDetailOutput.Image"), System.Drawing.Image)
        Me.TSB_DutyDetailOutput.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSB_DutyDetailOutput.Name = "TSB_DutyDetailOutput"
        Me.TSB_DutyDetailOutput.Size = New System.Drawing.Size(88, 22)
        Me.TSB_DutyDetailOutput.Text = "签到表输出"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'TSBShowErrorInfo
        '
        Me.TSBShowErrorInfo.CheckOnClick = True
        Me.TSBShowErrorInfo.Image = CType(resources.GetObject("TSBShowErrorInfo.Image"), System.Drawing.Image)
        Me.TSBShowErrorInfo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBShowErrorInfo.Name = "TSBShowErrorInfo"
        Me.TSBShowErrorInfo.Size = New System.Drawing.Size(67, 22)
        Me.TSBShowErrorInfo.Text = "提示(&T)"
        Me.TSBShowErrorInfo.ToolTipText = "显示提示窗体"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Tabpages)
        Me.SplitContainer1.Size = New System.Drawing.Size(1055, 686)
        Me.SplitContainer1.SplitterDistance = 200
        Me.SplitContainer1.TabIndex = 10
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.SplitContainer3)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(200, 686)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "基本参数"
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(3, 17)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.GroupBox3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.TBYunzhuan)
        Me.SplitContainer3.Panel1.Controls.Add(Me.TBDriverNum)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label5)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.GroupBox5)
        Me.SplitContainer3.Size = New System.Drawing.Size(194, 666)
        Me.SplitContainer3.SplitterDistance = 259
        Me.SplitContainer3.TabIndex = 10
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.TBEndTime)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.TBStartTime)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(184, 84)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "时间参数"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 12)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "起始时间:"
        '
        'TBEndTime
        '
        Me.TBEndTime.Location = New System.Drawing.Point(71, 50)
        Me.TBEndTime.Name = "TBEndTime"
        Me.TBEndTime.Size = New System.Drawing.Size(100, 21)
        Me.TBEndTime.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 53)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 12)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "结束时间:"
        '
        'TBStartTime
        '
        Me.TBStartTime.Location = New System.Drawing.Point(71, 18)
        Me.TBStartTime.Name = "TBStartTime"
        Me.TBStartTime.Size = New System.Drawing.Size(100, 21)
        Me.TBStartTime.TabIndex = 7
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.TBPreTrain)
        Me.GroupBox3.Controls.Add(Me.TBLine)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Location = New System.Drawing.Point(3, 99)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(185, 87)
        Me.GroupBox3.TabIndex = 9
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "线路参数"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "线路:"
        '
        'TBPreTrain
        '
        Me.TBPreTrain.Location = New System.Drawing.Point(73, 52)
        Me.TBPreTrain.Name = "TBPreTrain"
        Me.TBPreTrain.Size = New System.Drawing.Size(100, 21)
        Me.TBPreTrain.TabIndex = 7
        '
        'TBLine
        '
        Me.TBLine.Location = New System.Drawing.Point(73, 18)
        Me.TBLine.Name = "TBLine"
        Me.TBLine.Size = New System.Drawing.Size(100, 21)
        Me.TBLine.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "备车数量:"
        '
        'TBYunzhuan
        '
        Me.TBYunzhuan.Location = New System.Drawing.Point(88, 195)
        Me.TBYunzhuan.Name = "TBYunzhuan"
        Me.TBYunzhuan.Size = New System.Drawing.Size(100, 21)
        Me.TBYunzhuan.TabIndex = 7
        '
        'TBDriverNum
        '
        Me.TBDriverNum.Location = New System.Drawing.Point(88, 226)
        Me.TBDriverNum.Name = "TBDriverNum"
        Me.TBDriverNum.Size = New System.Drawing.Size(100, 21)
        Me.TBDriverNum.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(26, 198)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 12)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "运转制度:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(2, 229)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 12)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "当前司机组数:"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.TreeDrivers)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox5.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(194, 403)
        Me.GroupBox5.TabIndex = 9
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "班组情况"
        '
        'TreeDrivers
        '
        Me.TreeDrivers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeDrivers.Location = New System.Drawing.Point(3, 17)
        Me.TreeDrivers.Name = "TreeDrivers"
        Me.TreeDrivers.Size = New System.Drawing.Size(188, 383)
        Me.TreeDrivers.TabIndex = 8
        '
        'Tabpages
        '
        Me.Tabpages.Controls.Add(Me.乘务轮值匹配)
        Me.Tabpages.Controls.Add(Me.表号任务查询)
        Me.Tabpages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabpages.Location = New System.Drawing.Point(0, 0)
        Me.Tabpages.Name = "Tabpages"
        Me.Tabpages.SelectedIndex = 0
        Me.Tabpages.Size = New System.Drawing.Size(851, 686)
        Me.Tabpages.TabIndex = 1
        '
        '乘务轮值匹配
        '
        Me.乘务轮值匹配.Controls.Add(Me.GroupBox6)
        Me.乘务轮值匹配.Controls.Add(Me.GBErrorInfo)
        Me.乘务轮值匹配.Location = New System.Drawing.Point(4, 22)
        Me.乘务轮值匹配.Name = "乘务轮值匹配"
        Me.乘务轮值匹配.Padding = New System.Windows.Forms.Padding(3)
        Me.乘务轮值匹配.Size = New System.Drawing.Size(843, 660)
        Me.乘务轮值匹配.TabIndex = 1
        Me.乘务轮值匹配.Text = "乘务轮值匹配"
        Me.乘务轮值匹配.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.TabControlMain)
        Me.GroupBox6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox6.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(837, 514)
        Me.GroupBox6.TabIndex = 3
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "任务匹配结果"
        '
        'TabControlMain
        '
        Me.TabControlMain.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.TabControlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlMain.Location = New System.Drawing.Point(3, 17)
        Me.TabControlMain.Name = "TabControlMain"
        Me.TabControlMain.SelectedIndex = 0
        Me.TabControlMain.Size = New System.Drawing.Size(831, 494)
        Me.TabControlMain.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight
        Me.TabControlMain.TabIndex = 1
        '
        'GBErrorInfo
        '
        Me.GBErrorInfo.Controls.Add(Me.DGVErrorInfo)
        Me.GBErrorInfo.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GBErrorInfo.Location = New System.Drawing.Point(3, 517)
        Me.GBErrorInfo.Name = "GBErrorInfo"
        Me.GBErrorInfo.Size = New System.Drawing.Size(837, 140)
        Me.GBErrorInfo.TabIndex = 2
        Me.GBErrorInfo.TabStop = False
        Me.GBErrorInfo.Text = "信息提示栏"
        Me.GBErrorInfo.Visible = False
        '
        'DGVErrorInfo
        '
        Me.DGVErrorInfo.AllowUserToAddRows = False
        Me.DGVErrorInfo.AllowUserToDeleteRows = False
        Me.DGVErrorInfo.BackgroundColor = System.Drawing.Color.White
        Me.DGVErrorInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVErrorInfo.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.编号, Me.日期, Me.未分配任务})
        Me.DGVErrorInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVErrorInfo.Location = New System.Drawing.Point(3, 17)
        Me.DGVErrorInfo.Name = "DGVErrorInfo"
        Me.DGVErrorInfo.ReadOnly = True
        Me.DGVErrorInfo.RowHeadersVisible = False
        Me.DGVErrorInfo.RowTemplate.Height = 23
        Me.DGVErrorInfo.Size = New System.Drawing.Size(831, 120)
        Me.DGVErrorInfo.TabIndex = 0
        '
        '编号
        '
        Me.编号.HeaderText = "编号"
        Me.编号.Name = "编号"
        Me.编号.ReadOnly = True
        Me.编号.Width = 55
        '
        '日期
        '
        Me.日期.HeaderText = "日期"
        Me.日期.Name = "日期"
        Me.日期.ReadOnly = True
        '
        '未分配任务
        '
        Me.未分配任务.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.未分配任务.HeaderText = "未分配任务"
        Me.未分配任务.Name = "未分配任务"
        Me.未分配任务.ReadOnly = True
        '
        '表号任务查询
        '
        Me.表号任务查询.Controls.Add(Me.Panel1)
        Me.表号任务查询.Controls.Add(Me.GroupBox4)
        Me.表号任务查询.Location = New System.Drawing.Point(4, 22)
        Me.表号任务查询.Name = "表号任务查询"
        Me.表号任务查询.Padding = New System.Windows.Forms.Padding(3)
        Me.表号任务查询.Size = New System.Drawing.Size(843, 660)
        Me.表号任务查询.TabIndex = 2
        Me.表号任务查询.Text = "表号任务查询"
        Me.表号任务查询.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.DGVTimetable)
        Me.Panel1.Controls.Add(Me.ToolStrip2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 61)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(837, 596)
        Me.Panel1.TabIndex = 8
        '
        'DGVTimetable
        '
        Me.DGVTimetable.AllowUserToAddRows = False
        Me.DGVTimetable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVTimetable.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.任务号, Me.班种, Me.车次, Me.开始时间, Me.开始站名, Me.结束时间, Me.结束站名})
        Me.DGVTimetable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVTimetable.Location = New System.Drawing.Point(0, 25)
        Me.DGVTimetable.Name = "DGVTimetable"
        Me.DGVTimetable.RowTemplate.Height = 23
        Me.DGVTimetable.Size = New System.Drawing.Size(837, 571)
        Me.DGVTimetable.TabIndex = 6
        '
        '任务号
        '
        Me.任务号.HeaderText = "任务号"
        Me.任务号.Name = "任务号"
        '
        '班种
        '
        Me.班种.HeaderText = "班种"
        Me.班种.Name = "班种"
        '
        '车次
        '
        Me.车次.HeaderText = "车次"
        Me.车次.Name = "车次"
        '
        '开始时间
        '
        Me.开始时间.HeaderText = "开始时间"
        Me.开始时间.Name = "开始时间"
        '
        '开始站名
        '
        Me.开始站名.HeaderText = "开始站名"
        Me.开始站名.Name = "开始站名"
        '
        '结束时间
        '
        Me.结束时间.HeaderText = "结束时间"
        Me.结束时间.Name = "结束时间"
        '
        '结束站名
        '
        Me.结束站名.HeaderText = "结束站名"
        Me.结束站名.Name = "结束站名"
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.TTXTDutyNo})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(837, 25)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(47, 22)
        Me.ToolStripLabel1.Text = "任务号:"
        '
        'TTXTDutyNo
        '
        Me.TTXTDutyNo.Name = "TTXTDutyNo"
        Me.TTXTDutyNo.Size = New System.Drawing.Size(100, 25)
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.TimetablePicker)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.BtnQuery)
        Me.GroupBox4.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox4.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(837, 58)
        Me.GroupBox4.TabIndex = 7
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "查询条件"
        '
        'TimetablePicker
        '
        Me.TimetablePicker.Location = New System.Drawing.Point(77, 23)
        Me.TimetablePicker.Name = "TimetablePicker"
        Me.TimetablePicker.Size = New System.Drawing.Size(165, 21)
        Me.TimetablePicker.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(28, 27)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 12)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "日期:"
        '
        'BtnQuery
        '
        Me.BtnQuery.Location = New System.Drawing.Point(268, 21)
        Me.BtnQuery.Name = "BtnQuery"
        Me.BtnQuery.Size = New System.Drawing.Size(75, 23)
        Me.BtnQuery.TabIndex = 3
        Me.BtnQuery.Text = "查询"
        Me.BtnQuery.UseVisualStyleBackColor = True
        '
        '交换任务EToolStripMenuItem
        '
        Me.交换任务EToolStripMenuItem.Name = "交换任务EToolStripMenuItem"
        Me.交换任务EToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.交换任务EToolStripMenuItem.Text = "交换任务(&E)"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(163, 6)
        '
        '任务详情DToolStripMenuItem
        '
        Me.任务详情DToolStripMenuItem.Name = "任务详情DToolStripMenuItem"
        Me.任务详情DToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.任务详情DToolStripMenuItem.Text = "任务详情(&M)"
        '
        'CMUChangeDuty
        '
        Me.CMUChangeDuty.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.交换任务EToolStripMenuItem, Me.ToolStripSeparator1, Me.分配空闲任务FToolStripMenuItem, Me.分配其它任务OToolStripMenuItem, Me.删除任务DToolStripMenuItem, Me.ToolStripSeparator5, Me.任务详情DToolStripMenuItem})
        Me.CMUChangeDuty.Name = "CMUChangeDuty"
        Me.CMUChangeDuty.Size = New System.Drawing.Size(167, 126)
        '
        '分配空闲任务FToolStripMenuItem
        '
        Me.分配空闲任务FToolStripMenuItem.Name = "分配空闲任务FToolStripMenuItem"
        Me.分配空闲任务FToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.分配空闲任务FToolStripMenuItem.Text = "分配空闲任务(&F)"
        '
        '分配其它任务OToolStripMenuItem
        '
        Me.分配其它任务OToolStripMenuItem.Name = "分配其它任务OToolStripMenuItem"
        Me.分配其它任务OToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.分配其它任务OToolStripMenuItem.Text = "分配其它任务(&O)"
        '
        '删除任务DToolStripMenuItem
        '
        Me.删除任务DToolStripMenuItem.Name = "删除任务DToolStripMenuItem"
        Me.删除任务DToolStripMenuItem.Size = New System.Drawing.Size(166, 22)
        Me.删除任务DToolStripMenuItem.Text = "删除任务(&D)"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(163, 6)
        '
        'StatusStripMain
        '
        Me.StatusStripMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LBEdit, Me.LBModify})
        Me.StatusStripMain.Location = New System.Drawing.Point(0, 711)
        Me.StatusStripMain.Name = "StatusStripMain"
        Me.StatusStripMain.Size = New System.Drawing.Size(1055, 22)
        Me.StatusStripMain.TabIndex = 13
        Me.StatusStripMain.Text = "StatusStrip1"
        '
        'LBEdit
        '
        Me.LBEdit.Name = "LBEdit"
        Me.LBEdit.Size = New System.Drawing.Size(56, 17)
        Me.LBEdit.Text = "编制状态"
        '
        'LBModify
        '
        Me.LBModify.Name = "LBModify"
        Me.LBModify.Size = New System.Drawing.Size(56, 17)
        Me.LBModify.Text = "调整状态"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 500
        '
        'FrmClassCoordinate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1055, 733)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStripMain)
        Me.Name = "FrmClassCoordinate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "乘务轮转计划编制"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.Tabpages.ResumeLayout(False)
        Me.乘务轮值匹配.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GBErrorInfo.ResumeLayout(False)
        CType(Me.DGVErrorInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.表号任务查询.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DGVTimetable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.CMUChangeDuty.ResumeLayout(False)
        Me.StatusStripMain.ResumeLayout(False)
        Me.StatusStripMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents TSBSetting As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TBPreTrain As System.Windows.Forms.TextBox
    Friend WithEvents TBLine As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TreeDrivers As System.Windows.Forms.TreeView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TBEndTime As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TBStartTime As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TBDriverNum As System.Windows.Forms.TextBox
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents TBYunzhuan As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TSBAssignDuty As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSB_AssignFirstDayDuty As System.Windows.Forms.ToolStripButton
    Friend WithEvents Tabpages As System.Windows.Forms.TabControl
    Friend WithEvents 乘务轮值匹配 As System.Windows.Forms.TabPage
    Friend WithEvents 交换任务EToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 任务详情DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CMUChangeDuty As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents GBErrorInfo As System.Windows.Forms.GroupBox
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSBShowErrorInfo As System.Windows.Forms.ToolStripButton
    Friend WithEvents DGVErrorInfo As System.Windows.Forms.DataGridView
    Friend WithEvents 编号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 日期 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 未分配任务 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 分配空闲任务FToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSBRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSBOpen As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSBOutPutExcel As System.Windows.Forms.ToolStripButton
    Friend WithEvents 表号任务查询 As System.Windows.Forms.TabPage
    Friend WithEvents DGVTimetable As System.Windows.Forms.DataGridView
    Friend WithEvents 任务号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 班种 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 车次 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 开始时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 开始站名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 结束时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 结束站名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TimetablePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents BtnQuery As System.Windows.Forms.Button
    Friend WithEvents TSB_DutyDetailOutput As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TSB_Save As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents TTXTDutyNo As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents TabControlMain As System.Windows.Forms.TabControl
    Friend WithEvents StatusStripMain As System.Windows.Forms.StatusStrip
    Friend WithEvents LBModify As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents 删除任务DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 分配其它任务OToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LBEdit As System.Windows.Forms.ToolStripStatusLabel
End Class
