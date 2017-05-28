<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CrewInfoIndex
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CrewInfoIndex))
        Me.AddContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.新增NToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.新增ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.请假ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.师徒ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HeadToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.TSBModify = New System.Windows.Forms.ToolStripButton()
        Me.TSBDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSBRefresh = New System.Windows.Forms.ToolStripButton()
        Me.TSBRelation = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton8 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSBInput = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.TSBSearch = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton7 = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TVDrivers = New System.Windows.Forms.TreeView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TotalDataGridView = New System.Windows.Forms.DataGridView()
        Me.线路 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.班组 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.组号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.工号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.姓名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.岗位 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.区域 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.工作证编号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.是否可用 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.原因 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.技能等级 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.公里数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.开始统计 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.星级 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.联系电话 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.学徒 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.师徒备注 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SubToolStrip = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.TxtQuery = New System.Windows.Forms.ToolStripTextBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.AddContextMenu.SuspendLayout()
        Me.HeadToolStrip.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.TotalDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SubToolStrip.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'AddContextMenu
        '
        Me.AddContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.新增NToolStripMenuItem, Me.新增ToolStripMenuItem, Me.请假ToolStripMenuItem, Me.师徒ToolStripMenuItem})
        Me.AddContextMenu.Name = "AddContextMenu"
        Me.AddContextMenu.Size = New System.Drawing.Size(130, 92)
        '
        '新增NToolStripMenuItem
        '
        Me.新增NToolStripMenuItem.Image = CType(resources.GetObject("新增NToolStripMenuItem.Image"), System.Drawing.Image)
        Me.新增NToolStripMenuItem.Name = "新增NToolStripMenuItem"
        Me.新增NToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.新增NToolStripMenuItem.Text = "新增(&N)..."
        '
        '新增ToolStripMenuItem
        '
        Me.新增ToolStripMenuItem.Image = CType(resources.GetObject("新增ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.新增ToolStripMenuItem.Name = "新增ToolStripMenuItem"
        Me.新增ToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.新增ToolStripMenuItem.Text = "修改(&M)..."
        '
        '请假ToolStripMenuItem
        '
        Me.请假ToolStripMenuItem.Image = CType(resources.GetObject("请假ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.请假ToolStripMenuItem.Name = "请假ToolStripMenuItem"
        Me.请假ToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.请假ToolStripMenuItem.Text = "删除(&D)"
        '
        '师徒ToolStripMenuItem
        '
        Me.师徒ToolStripMenuItem.Name = "师徒ToolStripMenuItem"
        Me.师徒ToolStripMenuItem.Size = New System.Drawing.Size(129, 22)
        Me.师徒ToolStripMenuItem.Text = "师徒(&A)..."
        '
        'HeadToolStrip
        '
        Me.HeadToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.TSBModify, Me.TSBDelete, Me.ToolStripSeparator1, Me.TSBRefresh, Me.TSBRelation, Me.ToolStripButton8, Me.ToolStripButton6, Me.ToolStripSeparator2, Me.TSBInput, Me.ToolStripButton3, Me.ToolStripSeparator3, Me.TSBSearch, Me.ToolStripSeparator4, Me.ToolStripButton7, Me.ToolStripButton4, Me.ToolStripButton5, Me.ToolStripButton2})
        Me.HeadToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.HeadToolStrip.Name = "HeadToolStrip"
        Me.HeadToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional
        Me.HeadToolStrip.Size = New System.Drawing.Size(1027, 56)
        Me.HeadToolStrip.TabIndex = 0
        Me.HeadToolStrip.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(36, 53)
        Me.ToolStripButton1.Text = "新增"
        Me.ToolStripButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'TSBModify
        '
        Me.TSBModify.Image = CType(resources.GetObject("TSBModify.Image"), System.Drawing.Image)
        Me.TSBModify.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TSBModify.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBModify.Name = "TSBModify"
        Me.TSBModify.Size = New System.Drawing.Size(36, 53)
        Me.TSBModify.Text = "修改"
        Me.TSBModify.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'TSBDelete
        '
        Me.TSBDelete.Image = CType(resources.GetObject("TSBDelete.Image"), System.Drawing.Image)
        Me.TSBDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TSBDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBDelete.Name = "TSBDelete"
        Me.TSBDelete.Size = New System.Drawing.Size(36, 53)
        Me.TSBDelete.Text = "删除"
        Me.TSBDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 56)
        '
        'TSBRefresh
        '
        Me.TSBRefresh.Image = CType(resources.GetObject("TSBRefresh.Image"), System.Drawing.Image)
        Me.TSBRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TSBRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBRefresh.Name = "TSBRefresh"
        Me.TSBRefresh.Size = New System.Drawing.Size(36, 53)
        Me.TSBRefresh.Text = "刷新"
        Me.TSBRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.TSBRefresh.ToolTipText = "刷新"
        '
        'TSBRelation
        '
        Me.TSBRelation.Image = CType(resources.GetObject("TSBRelation.Image"), System.Drawing.Image)
        Me.TSBRelation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TSBRelation.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBRelation.Name = "TSBRelation"
        Me.TSBRelation.Size = New System.Drawing.Size(36, 53)
        Me.TSBRelation.Text = "师徒"
        Me.TSBRelation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton8
        '
        Me.ToolStripButton8.Image = CType(resources.GetObject("ToolStripButton8.Image"), System.Drawing.Image)
        Me.ToolStripButton8.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton8.Name = "ToolStripButton8"
        Me.ToolStripButton8.Size = New System.Drawing.Size(72, 53)
        Me.ToolStripButton8.Text = "请休假安排"
        Me.ToolStripButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton6
        '
        Me.ToolStripButton6.Image = CType(resources.GetObject("ToolStripButton6.Image"), System.Drawing.Image)
        Me.ToolStripButton6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton6.Name = "ToolStripButton6"
        Me.ToolStripButton6.Size = New System.Drawing.Size(60, 53)
        Me.ToolStripButton6.Text = "批量设置"
        Me.ToolStripButton6.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.ToolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 56)
        '
        'TSBInput
        '
        Me.TSBInput.Image = CType(resources.GetObject("TSBInput.Image"), System.Drawing.Image)
        Me.TSBInput.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TSBInput.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBInput.Name = "TSBInput"
        Me.TSBInput.Size = New System.Drawing.Size(36, 53)
        Me.TSBInput.Text = "导入"
        Me.TSBInput.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(36, 53)
        Me.ToolStripButton3.Text = "导出"
        Me.ToolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 56)
        '
        'TSBSearch
        '
        Me.TSBSearch.Image = CType(resources.GetObject("TSBSearch.Image"), System.Drawing.Image)
        Me.TSBSearch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.TSBSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TSBSearch.Name = "TSBSearch"
        Me.TSBSearch.Size = New System.Drawing.Size(60, 53)
        Me.TSBSearch.Text = "高级查询"
        Me.TSBSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 56)
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(60, 53)
        Me.ToolStripButton4.Text = "上传云端"
        Me.ToolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(60, 53)
        Me.ToolStripButton5.Text = "下载本地"
        Me.ToolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(36, 53)
        Me.ToolStripButton2.Text = "关闭"
        Me.ToolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ToolStripButton7
        '
        Me.ToolStripButton7.Image = CType(resources.GetObject("ToolStripButton7.Image"), System.Drawing.Image)
        Me.ToolStripButton7.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton7.Name = "ToolStripButton7"
        Me.ToolStripButton7.Size = New System.Drawing.Size(60, 53)
        Me.ToolStripButton7.Text = "他线共享"
        Me.ToolStripButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 59)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TVDrivers)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.TotalDataGridView)
        Me.SplitContainer1.Panel2.Controls.Add(Me.SubToolStrip)
        Me.SplitContainer1.Size = New System.Drawing.Size(1027, 661)
        Me.SplitContainer1.SplitterDistance = 169
        Me.SplitContainer1.TabIndex = 3
        '
        'TVDrivers
        '
        Me.TVDrivers.ContextMenuStrip = Me.AddContextMenu
        Me.TVDrivers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TVDrivers.HideSelection = False
        Me.TVDrivers.Location = New System.Drawing.Point(0, 0)
        Me.TVDrivers.Name = "TVDrivers"
        Me.TVDrivers.Size = New System.Drawing.Size(169, 661)
        Me.TVDrivers.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(232, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "统计信息："
        '
        'TotalDataGridView
        '
        Me.TotalDataGridView.AllowUserToAddRows = False
        Me.TotalDataGridView.AllowUserToDeleteRows = False
        Me.TotalDataGridView.BackgroundColor = System.Drawing.Color.White
        Me.TotalDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.线路, Me.班组, Me.组号, Me.工号, Me.姓名, Me.岗位, Me.区域, Me.工作证编号, Me.是否可用, Me.原因, Me.技能等级, Me.公里数, Me.开始统计, Me.星级, Me.联系电话, Me.学徒, Me.师徒备注})
        Me.TotalDataGridView.ContextMenuStrip = Me.AddContextMenu
        Me.TotalDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalDataGridView.GridColor = System.Drawing.Color.LawnGreen
        Me.TotalDataGridView.Location = New System.Drawing.Point(0, 25)
        Me.TotalDataGridView.Name = "TotalDataGridView"
        Me.TotalDataGridView.ReadOnly = True
        Me.TotalDataGridView.RowTemplate.Height = 23
        Me.TotalDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.TotalDataGridView.Size = New System.Drawing.Size(854, 636)
        Me.TotalDataGridView.TabIndex = 2
        '
        '线路
        '
        Me.线路.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.线路.HeaderText = "线路"
        Me.线路.Name = "线路"
        Me.线路.ReadOnly = True
        Me.线路.Width = 54
        '
        '班组
        '
        Me.班组.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.班组.HeaderText = "班组"
        Me.班组.Name = "班组"
        Me.班组.ReadOnly = True
        Me.班组.Width = 54
        '
        '组号
        '
        Me.组号.HeaderText = "组号"
        Me.组号.Name = "组号"
        Me.组号.ReadOnly = True
        Me.组号.Width = 50
        '
        '工号
        '
        Me.工号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.工号.HeaderText = "工号"
        Me.工号.Name = "工号"
        Me.工号.ReadOnly = True
        Me.工号.Width = 54
        '
        '姓名
        '
        Me.姓名.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.姓名.HeaderText = "姓名"
        Me.姓名.Name = "姓名"
        Me.姓名.ReadOnly = True
        Me.姓名.Width = 54
        '
        '岗位
        '
        Me.岗位.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.岗位.HeaderText = "岗位"
        Me.岗位.Name = "岗位"
        Me.岗位.ReadOnly = True
        Me.岗位.Width = 54
        '
        '区域
        '
        Me.区域.HeaderText = "区域"
        Me.区域.Name = "区域"
        Me.区域.ReadOnly = True
        '
        '工作证编号
        '
        Me.工作证编号.HeaderText = "工作证编号"
        Me.工作证编号.Name = "工作证编号"
        Me.工作证编号.ReadOnly = True
        '
        '是否可用
        '
        Me.是否可用.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.是否可用.HeaderText = "是否可用"
        Me.是否可用.Name = "是否可用"
        Me.是否可用.ReadOnly = True
        Me.是否可用.Width = 78
        '
        '原因
        '
        Me.原因.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.原因.HeaderText = "原因"
        Me.原因.Name = "原因"
        Me.原因.ReadOnly = True
        Me.原因.Width = 54
        '
        '技能等级
        '
        Me.技能等级.HeaderText = "技能等级"
        Me.技能等级.Name = "技能等级"
        Me.技能等级.ReadOnly = True
        Me.技能等级.Width = 78
        '
        '公里数
        '
        Me.公里数.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.公里数.HeaderText = "公里数"
        Me.公里数.Name = "公里数"
        Me.公里数.ReadOnly = True
        Me.公里数.Width = 66
        '
        '开始统计
        '
        Me.开始统计.HeaderText = "开始统计"
        Me.开始统计.Name = "开始统计"
        Me.开始统计.ReadOnly = True
        '
        '星级
        '
        Me.星级.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.星级.HeaderText = "星级"
        Me.星级.Name = "星级"
        Me.星级.ReadOnly = True
        Me.星级.Width = 54
        '
        '联系电话
        '
        Me.联系电话.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.联系电话.HeaderText = "联系电话"
        Me.联系电话.Name = "联系电话"
        Me.联系电话.ReadOnly = True
        Me.联系电话.Width = 78
        '
        '学徒
        '
        Me.学徒.HeaderText = "学徒"
        Me.学徒.Name = "学徒"
        Me.学徒.ReadOnly = True
        Me.学徒.Width = 54
        '
        '师徒备注
        '
        Me.师徒备注.HeaderText = "师徒备注"
        Me.师徒备注.Name = "师徒备注"
        Me.师徒备注.ReadOnly = True
        Me.师徒备注.Width = 78
        '
        'SubToolStrip
        '
        Me.SubToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.TxtQuery})
        Me.SubToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.SubToolStrip.Name = "SubToolStrip"
        Me.SubToolStrip.Size = New System.Drawing.Size(854, 25)
        Me.SubToolStrip.TabIndex = 0
        Me.SubToolStrip.Text = "ToolStrip2"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(80, 22)
        Me.ToolStripLabel1.Text = "快速查找工号"
        '
        'TxtQuery
        '
        Me.TxtQuery.Name = "TxtQuery"
        Me.TxtQuery.Size = New System.Drawing.Size(100, 25)
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 720)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(1027, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(56, 17)
        Me.ToolStripStatusLabel1.Text = "司机管理"
        '
        'CrewInfoIndex
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1027, 742)
        Me.Controls.Add(Me.HeadToolStrip)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Name = "CrewInfoIndex"
        Me.Text = "乘务信息列表"
        Me.AddContextMenu.ResumeLayout(False)
        Me.HeadToolStrip.ResumeLayout(False)
        Me.HeadToolStrip.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.TotalDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SubToolStrip.ResumeLayout(False)
        Me.SubToolStrip.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents HeadToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSBModify As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSBDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton8 As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSBInput As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSBSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents AddContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 新增ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 请假ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TVDrivers As System.Windows.Forms.TreeView
    Friend WithEvents TotalDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents SubToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TxtQuery As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents TSBRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents 新增NToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents TSBRelation As System.Windows.Forms.ToolStripButton
    Friend WithEvents 师徒ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 线路 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 班组 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 组号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 工号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 姓名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 岗位 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 区域 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 工作证编号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 是否可用 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 原因 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 技能等级 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 公里数 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 开始统计 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 星级 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 联系电话 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 学徒 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 师徒备注 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStripButton5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolStripButton6 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton7 As System.Windows.Forms.ToolStripButton
End Class
