Partial Public Class frmCSTimeTableMain
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCSTimeTableMain))
        Me.MenuMain = New System.Windows.Forms.MenuStrip()
        Me.文件7FToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.打开运行图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.保存ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.另存为ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.分隔一ToolStripMenuItem = New System.Windows.Forms.ToolStripSeparator()
        Me.管理ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.导入IToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.输出ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.图片IToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.位置图导出ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.输出车底交路CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.钓鱼图DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.钓鱼图反导入ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.输出OToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.退出EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.选择运行图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.人数测算ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.备车设置ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.参数设置ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.可变动参数TToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.安排乘务员人数ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.计算机安排CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.安排完早班ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.安排完夜班ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.安排完吃饭ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.白班中午吃饭ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.安排完白班ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.安排完日勤班ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.自动优化ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.合并短任务ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.重新处理晚餐ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.指定条件清空计划ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.按时间清空ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.清空早班ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.清空夜班ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.清空白班ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.清空日勤班ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.清空当前计划ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.操作OToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.撤销CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.回复RToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.放宽限制ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.任务段衔接ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.整理任务SToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.整理吃饭ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.任务检查ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
        Me.衔接计划设置GToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.衔接计划管理ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.衔接计划设置ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.夜早班搭配ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator19 = New System.Windows.Forms.ToolStripSeparator()
        Me.用餐设置DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.设置区域信息ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.查询ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.乘务员ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.统计数据ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.出退勤地点统计IToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.查找列车FToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.按内在编号查找任务ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.按输出号查找任务ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.底图DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.一分格ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.二分格ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.十分格ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.小时格ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.放大底图宽度ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.缩小底图宽度ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.纵向放大底图ZToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.纵向缩小底图XToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator21 = New System.Windows.Forms.ToolStripSeparator()
        Me.底图线型与颜色CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.参数CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.系统设置SToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.颜色与字体CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.打开ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.保存乘务计划ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.导入司机位置图ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator26 = New System.Windows.Forms.ToolStripSeparator()
        Me.剪切ToolStripButton10 = New System.Windows.Forms.ToolStripButton()
        Me.粘贴ToolStripButton13 = New System.Windows.Forms.ToolStripButton()
        Me.复制ToolStripButton9 = New System.Windows.Forms.ToolStripButton()
        Me.删除ToolStripButton11 = New System.Windows.Forms.ToolStripButton()
        Me.查找ToolStripButton12 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator27 = New System.Windows.Forms.ToolStripSeparator()
        Me.撤销tolStripUndo = New System.Windows.Forms.ToolStripButton()
        Me.重复tolStripRedo = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator17 = New System.Windows.Forms.ToolStripSeparator()
        Me.CmbSize = New System.Windows.Forms.ToolStripComboBox()
        Me.底图放大ToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.底图缩小ToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.纵向放大ToolStripButton6 = New System.Windows.Forms.ToolStripButton()
        Me.纵向缩小ToolStripButton8 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.检查错误ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator18 = New System.Windows.Forms.ToolStripSeparator()
        Me.刷新乘务交路图ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.ToolLabError = New System.Windows.Forms.ToolStripStatusLabel()
        Me.cmuTrainLine = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.安排驾驶员IToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.指派给乘务员eToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.断开驾驶员任务DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.改变乘务员状态KToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.班中ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.用餐ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.班后ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.替饭ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.指向性手工安排乘务员AToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.正向安排ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.逆向安排ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.修改驾驶员连接方式EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.删除该乘务员DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.用餐设置YToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.断开任务段SToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.安排关联任务MToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.安排随乘列车DHToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.安排随乘退勤列车TToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.断开随勤列车BToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.车底信息CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.查询ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitError = New System.Windows.Forms.SplitContainer()
        Me.SplitConLeftRight = New System.Windows.Forms.SplitContainer()
        Me.SplitDiagram = New System.Windows.Forms.SplitContainer()
        Me.PicStation2 = New System.Windows.Forms.PictureBox()
        Me.PicDiagram = New System.Windows.Forms.PictureBox()
        Me.picStation = New System.Windows.Forms.PictureBox()
        Me.TabControlMain = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.listViewTrain = New System.Windows.Forms.ListView()
        Me.序号 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.列车ID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.开始站名 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.开始时间 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.上车车次 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.终到站名 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.终到时间 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.下车车次 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.里程 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.ListViewDuty = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader24 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.是否用餐 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.ListViewCurDuty = New System.Windows.Forms.ListView()
        Me.ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader13 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader14 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader15 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader16 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader17 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader20 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader18 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader19 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader21 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader42 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.ListViewPosition = New System.Windows.Forms.ListView()
        Me.ColumnHeader25 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader26 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader27 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.任务编号 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.ListViewInOutDepot = New System.Windows.Forms.ListView()
        Me.ColumnHeader22 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader23 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader32 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader43 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader33 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader44 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.ListViewDutyNum = New System.Windows.Forms.ListView()
        Me.ColumnHeader28 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader29 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader34 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader30 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader31 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader35 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader36 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader37 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage7 = New System.Windows.Forms.TabPage()
        Me.ListViewMOnOffInfo = New System.Windows.Forms.ListView()
        Me.ColumnHeader46 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader47 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader38 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader39 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader40 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader41 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage8 = New System.Windows.Forms.TabPage()
        Me.ListViewUnDead = New System.Windows.Forms.ListView()
        Me.ColumnHeader45 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader48 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader49 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.labInfor = New System.Windows.Forms.Label()
        Me.SplitMainContainer = New System.Windows.Forms.SplitContainer()
        Me.LabelPro = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.labName = New System.Windows.Forms.Label()
        Me.labChediNum = New System.Windows.Forms.Label()
        Me.MenuMain.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.cmuTrainLine.SuspendLayout()
        Me.SplitError.Panel1.SuspendLayout()
        Me.SplitError.Panel2.SuspendLayout()
        Me.SplitError.SuspendLayout()
        Me.SplitConLeftRight.Panel1.SuspendLayout()
        Me.SplitConLeftRight.Panel2.SuspendLayout()
        Me.SplitConLeftRight.SuspendLayout()
        Me.SplitDiagram.Panel1.SuspendLayout()
        Me.SplitDiagram.Panel2.SuspendLayout()
        Me.SplitDiagram.SuspendLayout()
        CType(Me.PicStation2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PicDiagram, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picStation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControlMain.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.TabPage8.SuspendLayout()
        Me.SplitMainContainer.Panel1.SuspendLayout()
        Me.SplitMainContainer.Panel2.SuspendLayout()
        Me.SplitMainContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuMain
        '
        Me.MenuMain.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.文件7FToolStripMenuItem, Me.ToolStripMenuItem1, Me.操作OToolStripMenuItem, Me.查询ToolStripMenuItem1, Me.底图DToolStripMenuItem, Me.参数CToolStripMenuItem})
        Me.MenuMain.Location = New System.Drawing.Point(0, 0)
        Me.MenuMain.Name = "MenuMain"
        Me.MenuMain.Size = New System.Drawing.Size(886, 25)
        Me.MenuMain.TabIndex = 1
        Me.MenuMain.Text = "mnuMain"
        '
        '文件7FToolStripMenuItem
        '
        Me.文件7FToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.打开运行图ToolStripMenuItem, Me.保存ToolStripMenuItem, Me.另存为ToolStripMenuItem, Me.分隔一ToolStripMenuItem, Me.管理ToolStripMenuItem, Me.ToolStripSeparator3, Me.导入IToolStripMenuItem, Me.输出ToolStripMenuItem, Me.输出OToolStripMenuItem, Me.ToolStripSeparator6, Me.退出EToolStripMenuItem})
        Me.文件7FToolStripMenuItem.Name = "文件7FToolStripMenuItem"
        Me.文件7FToolStripMenuItem.Size = New System.Drawing.Size(58, 21)
        Me.文件7FToolStripMenuItem.Text = "文件(&F)"
        '
        '打开运行图ToolStripMenuItem
        '
        Me.打开运行图ToolStripMenuItem.Image = CType(resources.GetObject("打开运行图ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.打开运行图ToolStripMenuItem.Name = "打开运行图ToolStripMenuItem"
        Me.打开运行图ToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.打开运行图ToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.打开运行图ToolStripMenuItem.Text = "打开(&O)"
        '
        '保存ToolStripMenuItem
        '
        Me.保存ToolStripMenuItem.Image = CType(resources.GetObject("保存ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem"
        Me.保存ToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.保存ToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.保存ToolStripMenuItem.Text = "保存(&S)"
        '
        '另存为ToolStripMenuItem
        '
        Me.另存为ToolStripMenuItem.Image = CType(resources.GetObject("另存为ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem"
        Me.另存为ToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.另存为ToolStripMenuItem.Text = "另存为..."
        '
        '分隔一ToolStripMenuItem
        '
        Me.分隔一ToolStripMenuItem.Name = "分隔一ToolStripMenuItem"
        Me.分隔一ToolStripMenuItem.Size = New System.Drawing.Size(166, 6)
        '
        '管理ToolStripMenuItem
        '
        Me.管理ToolStripMenuItem.Image = CType(resources.GetObject("管理ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.管理ToolStripMenuItem.Name = "管理ToolStripMenuItem"
        Me.管理ToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.M), System.Windows.Forms.Keys)
        Me.管理ToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.管理ToolStripMenuItem.Text = "管理(&M)"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(166, 6)
        '
        '导入IToolStripMenuItem
        '
        Me.导入IToolStripMenuItem.Image = CType(resources.GetObject("导入IToolStripMenuItem.Image"), System.Drawing.Image)
        Me.导入IToolStripMenuItem.Name = "导入IToolStripMenuItem"
        Me.导入IToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.导入IToolStripMenuItem.Text = "导入位置图(&I)"
        '
        '输出ToolStripMenuItem
        '
        Me.输出ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.图片IToolStripMenuItem, Me.ExcelToolStripMenuItem, Me.位置图导出ToolStripMenuItem, Me.输出车底交路CToolStripMenuItem, Me.钓鱼图DToolStripMenuItem, Me.钓鱼图反导入ToolStripMenuItem})
        Me.输出ToolStripMenuItem.Image = CType(resources.GetObject("输出ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.输出ToolStripMenuItem.Name = "输出ToolStripMenuItem"
        Me.输出ToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.输出ToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.输出ToolStripMenuItem.Text = "输出(&O)"
        '
        '图片IToolStripMenuItem
        '
        Me.图片IToolStripMenuItem.Image = CType(resources.GetObject("图片IToolStripMenuItem.Image"), System.Drawing.Image)
        Me.图片IToolStripMenuItem.Name = "图片IToolStripMenuItem"
        Me.图片IToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.图片IToolStripMenuItem.Text = "图片(&I)"
        '
        'ExcelToolStripMenuItem
        '
        Me.ExcelToolStripMenuItem.Image = CType(resources.GetObject("ExcelToolStripMenuItem.Image"), System.Drawing.Image)
        Me.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem"
        Me.ExcelToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.ExcelToolStripMenuItem.Text = "轮值表(&C)"
        '
        '位置图导出ToolStripMenuItem
        '
        Me.位置图导出ToolStripMenuItem.Name = "位置图导出ToolStripMenuItem"
        Me.位置图导出ToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.位置图导出ToolStripMenuItem.Text = "输出位置图"
        '
        '输出车底交路CToolStripMenuItem
        '
        Me.输出车底交路CToolStripMenuItem.Name = "输出车底交路CToolStripMenuItem"
        Me.输出车底交路CToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.输出车底交路CToolStripMenuItem.Text = "输出车底交路(&C)"
        '
        '钓鱼图DToolStripMenuItem
        '
        Me.钓鱼图DToolStripMenuItem.Image = CType(resources.GetObject("钓鱼图DToolStripMenuItem.Image"), System.Drawing.Image)
        Me.钓鱼图DToolStripMenuItem.Name = "钓鱼图DToolStripMenuItem"
        Me.钓鱼图DToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.钓鱼图DToolStripMenuItem.Text = "钓鱼图(&D)"
        Me.钓鱼图DToolStripMenuItem.Visible = False
        '
        '钓鱼图反导入ToolStripMenuItem
        '
        Me.钓鱼图反导入ToolStripMenuItem.Name = "钓鱼图反导入ToolStripMenuItem"
        Me.钓鱼图反导入ToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.钓鱼图反导入ToolStripMenuItem.Text = "钓鱼图反导入(&R)"
        Me.钓鱼图反导入ToolStripMenuItem.Visible = False
        '
        '输出OToolStripMenuItem
        '
        Me.输出OToolStripMenuItem.Image = CType(resources.GetObject("输出OToolStripMenuItem.Image"), System.Drawing.Image)
        Me.输出OToolStripMenuItem.Name = "输出OToolStripMenuItem"
        Me.输出OToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.输出OToolStripMenuItem.Text = "打印(&P)"
        Me.输出OToolStripMenuItem.Visible = False
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(166, 6)
        '
        '退出EToolStripMenuItem
        '
        Me.退出EToolStripMenuItem.Name = "退出EToolStripMenuItem"
        Me.退出EToolStripMenuItem.Size = New System.Drawing.Size(169, 22)
        Me.退出EToolStripMenuItem.Text = "退出(&E)"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.选择运行图ToolStripMenuItem, Me.人数测算ToolStripMenuItem, Me.ToolStripSeparator11, Me.备车设置ToolStripMenuItem, Me.参数设置ToolStripMenuItem, Me.可变动参数TToolStripMenuItem, Me.ToolStripSeparator12, Me.安排乘务员人数ToolStripMenuItem, Me.计算机安排CToolStripMenuItem, Me.自动优化ToolStripMenuItem, Me.ToolStripSeparator14, Me.指定条件清空计划ToolStripMenuItem, Me.清空当前计划ToolStripMenuItem})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(64, 21)
        Me.ToolStripMenuItem1.Text = "编制(&M)"
        '
        '选择运行图ToolStripMenuItem
        '
        Me.选择运行图ToolStripMenuItem.Image = CType(resources.GetObject("选择运行图ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.选择运行图ToolStripMenuItem.Name = "选择运行图ToolStripMenuItem"
        Me.选择运行图ToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.选择运行图ToolStripMenuItem.Text = "选择运行图(&S)"
        '
        '人数测算ToolStripMenuItem
        '
        Me.人数测算ToolStripMenuItem.Name = "人数测算ToolStripMenuItem"
        Me.人数测算ToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.人数测算ToolStripMenuItem.Text = "单图人数测算"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(182, 6)
        '
        '备车设置ToolStripMenuItem
        '
        Me.备车设置ToolStripMenuItem.Image = CType(resources.GetObject("备车设置ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.备车设置ToolStripMenuItem.Name = "备车设置ToolStripMenuItem"
        Me.备车设置ToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.备车设置ToolStripMenuItem.Text = "图中备车设置"
        '
        '参数设置ToolStripMenuItem
        '
        Me.参数设置ToolStripMenuItem.Image = CType(resources.GetObject("参数设置ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem"
        Me.参数设置ToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.参数设置ToolStripMenuItem.Text = "固定参数设置(&P)"
        '
        '可变动参数TToolStripMenuItem
        '
        Me.可变动参数TToolStripMenuItem.Image = CType(resources.GetObject("可变动参数TToolStripMenuItem.Image"), System.Drawing.Image)
        Me.可变动参数TToolStripMenuItem.Name = "可变动参数TToolStripMenuItem"
        Me.可变动参数TToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.可变动参数TToolStripMenuItem.Text = "可变动参数(&E)"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(182, 6)
        '
        '安排乘务员人数ToolStripMenuItem
        '
        Me.安排乘务员人数ToolStripMenuItem.Image = CType(resources.GetObject("安排乘务员人数ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.安排乘务员人数ToolStripMenuItem.Name = "安排乘务员人数ToolStripMenuItem"
        Me.安排乘务员人数ToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.安排乘务员人数ToolStripMenuItem.Text = "安排乘务员人数"
        '
        '计算机安排CToolStripMenuItem
        '
        Me.计算机安排CToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.安排完早班ToolStripMenuItem, Me.安排完夜班ToolStripMenuItem, Me.安排完吃饭ToolStripMenuItem, Me.安排完白班ToolStripMenuItem, Me.安排完日勤班ToolStripMenuItem})
        Me.计算机安排CToolStripMenuItem.Image = CType(resources.GetObject("计算机安排CToolStripMenuItem.Image"), System.Drawing.Image)
        Me.计算机安排CToolStripMenuItem.Name = "计算机安排CToolStripMenuItem"
        Me.计算机安排CToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.计算机安排CToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.计算机安排CToolStripMenuItem.Text = "自动安排(&C)"
        '
        '安排完早班ToolStripMenuItem
        '
        Me.安排完早班ToolStripMenuItem.Name = "安排完早班ToolStripMenuItem"
        Me.安排完早班ToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.安排完早班ToolStripMenuItem.Text = "安排完早班"
        '
        '安排完夜班ToolStripMenuItem
        '
        Me.安排完夜班ToolStripMenuItem.Name = "安排完夜班ToolStripMenuItem"
        Me.安排完夜班ToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.安排完夜班ToolStripMenuItem.Text = "安排完夜班"
        '
        '安排完吃饭ToolStripMenuItem
        '
        Me.安排完吃饭ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.白班中午吃饭ToolStripMenuItem})
        Me.安排完吃饭ToolStripMenuItem.Name = "安排完吃饭ToolStripMenuItem"
        Me.安排完吃饭ToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.安排完吃饭ToolStripMenuItem.Text = "安排完吃饭"
        '
        '白班中午吃饭ToolStripMenuItem
        '
        Me.白班中午吃饭ToolStripMenuItem.Name = "白班中午吃饭ToolStripMenuItem"
        Me.白班中午吃饭ToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.白班中午吃饭ToolStripMenuItem.Text = "白班中午吃饭"
        '
        '安排完白班ToolStripMenuItem
        '
        Me.安排完白班ToolStripMenuItem.Name = "安排完白班ToolStripMenuItem"
        Me.安排完白班ToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.安排完白班ToolStripMenuItem.Text = "安排完白班"
        '
        '安排完日勤班ToolStripMenuItem
        '
        Me.安排完日勤班ToolStripMenuItem.Name = "安排完日勤班ToolStripMenuItem"
        Me.安排完日勤班ToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.安排完日勤班ToolStripMenuItem.Text = "安排完日勤班"
        '
        '自动优化ToolStripMenuItem
        '
        Me.自动优化ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.合并短任务ToolStripMenuItem, Me.重新处理晚餐ToolStripMenuItem})
        Me.自动优化ToolStripMenuItem.Name = "自动优化ToolStripMenuItem"
        Me.自动优化ToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.自动优化ToolStripMenuItem.Text = "自动优化"
        '
        '合并短任务ToolStripMenuItem
        '
        Me.合并短任务ToolStripMenuItem.Name = "合并短任务ToolStripMenuItem"
        Me.合并短任务ToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.合并短任务ToolStripMenuItem.Text = "合并短任务"
        '
        '重新处理晚餐ToolStripMenuItem
        '
        Me.重新处理晚餐ToolStripMenuItem.Name = "重新处理晚餐ToolStripMenuItem"
        Me.重新处理晚餐ToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.重新处理晚餐ToolStripMenuItem.Text = "重新处理晚餐"
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(182, 6)
        '
        '指定条件清空计划ToolStripMenuItem
        '
        Me.指定条件清空计划ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.按时间清空ToolStripMenuItem, Me.清空早班ToolStripMenuItem, Me.清空夜班ToolStripMenuItem, Me.清空白班ToolStripMenuItem, Me.清空日勤班ToolStripMenuItem})
        Me.指定条件清空计划ToolStripMenuItem.Name = "指定条件清空计划ToolStripMenuItem"
        Me.指定条件清空计划ToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.指定条件清空计划ToolStripMenuItem.Text = "清空部分计划"
        '
        '按时间清空ToolStripMenuItem
        '
        Me.按时间清空ToolStripMenuItem.Name = "按时间清空ToolStripMenuItem"
        Me.按时间清空ToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.按时间清空ToolStripMenuItem.Text = "按时间清空"
        '
        '清空早班ToolStripMenuItem
        '
        Me.清空早班ToolStripMenuItem.Name = "清空早班ToolStripMenuItem"
        Me.清空早班ToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.清空早班ToolStripMenuItem.Text = "清空早班"
        '
        '清空夜班ToolStripMenuItem
        '
        Me.清空夜班ToolStripMenuItem.Name = "清空夜班ToolStripMenuItem"
        Me.清空夜班ToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.清空夜班ToolStripMenuItem.Text = "清空夜班"
        '
        '清空白班ToolStripMenuItem
        '
        Me.清空白班ToolStripMenuItem.Name = "清空白班ToolStripMenuItem"
        Me.清空白班ToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.清空白班ToolStripMenuItem.Text = "清空白班"
        '
        '清空日勤班ToolStripMenuItem
        '
        Me.清空日勤班ToolStripMenuItem.Name = "清空日勤班ToolStripMenuItem"
        Me.清空日勤班ToolStripMenuItem.Size = New System.Drawing.Size(136, 22)
        Me.清空日勤班ToolStripMenuItem.Text = "清空日勤班"
        '
        '清空当前计划ToolStripMenuItem
        '
        Me.清空当前计划ToolStripMenuItem.Name = "清空当前计划ToolStripMenuItem"
        Me.清空当前计划ToolStripMenuItem.Size = New System.Drawing.Size(185, 22)
        Me.清空当前计划ToolStripMenuItem.Text = "清空全部计划"
        '
        '操作OToolStripMenuItem
        '
        Me.操作OToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.撤销CToolStripMenuItem, Me.回复RToolStripMenuItem, Me.放宽限制ToolStripMenuItem, Me.ToolStripSeparator10, Me.任务段衔接ToolStripMenuItem, Me.整理任务SToolStripMenuItem, Me.ToolStripMenuItem2, Me.任务检查ToolStripMenuItem, Me.ToolStripSeparator15, Me.衔接计划设置GToolStripMenuItem, Me.ToolStripSeparator19, Me.用餐设置DToolStripMenuItem, Me.设置区域信息ToolStripMenuItem})
        Me.操作OToolStripMenuItem.Name = "操作OToolStripMenuItem"
        Me.操作OToolStripMenuItem.Size = New System.Drawing.Size(86, 21)
        Me.操作OToolStripMenuItem.Text = "手工操作(&O)"
        '
        '撤销CToolStripMenuItem
        '
        Me.撤销CToolStripMenuItem.Image = CType(resources.GetObject("撤销CToolStripMenuItem.Image"), System.Drawing.Image)
        Me.撤销CToolStripMenuItem.Name = "撤销CToolStripMenuItem"
        Me.撤销CToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.撤销CToolStripMenuItem.ShowShortcutKeys = False
        Me.撤销CToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.撤销CToolStripMenuItem.Text = "撤销(&Z)"
        '
        '回复RToolStripMenuItem
        '
        Me.回复RToolStripMenuItem.Image = CType(resources.GetObject("回复RToolStripMenuItem.Image"), System.Drawing.Image)
        Me.回复RToolStripMenuItem.Name = "回复RToolStripMenuItem"
        Me.回复RToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.回复RToolStripMenuItem.ShowShortcutKeys = False
        Me.回复RToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.回复RToolStripMenuItem.Text = "重复(&R)"
        '
        '放宽限制ToolStripMenuItem
        '
        Me.放宽限制ToolStripMenuItem.Name = "放宽限制ToolStripMenuItem"
        Me.放宽限制ToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.放宽限制ToolStripMenuItem.Text = "放宽限制"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(173, 6)
        '
        '任务段衔接ToolStripMenuItem
        '
        Me.任务段衔接ToolStripMenuItem.Name = "任务段衔接ToolStripMenuItem"
        Me.任务段衔接ToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.任务段衔接ToolStripMenuItem.Text = "任务段连接修改(&R)"
        '
        '整理任务SToolStripMenuItem
        '
        Me.整理任务SToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.整理吃饭ToolStripMenuItem})
        Me.整理任务SToolStripMenuItem.Image = CType(resources.GetObject("整理任务SToolStripMenuItem.Image"), System.Drawing.Image)
        Me.整理任务SToolStripMenuItem.Name = "整理任务SToolStripMenuItem"
        Me.整理任务SToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.整理任务SToolStripMenuItem.Text = "整理任务(&S)"
        '
        '整理吃饭ToolStripMenuItem
        '
        Me.整理吃饭ToolStripMenuItem.Name = "整理吃饭ToolStripMenuItem"
        Me.整理吃饭ToolStripMenuItem.Size = New System.Drawing.Size(124, 22)
        Me.整理吃饭ToolStripMenuItem.Text = "整理吃饭"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Image = CType(resources.GetObject("ToolStripMenuItem2.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(176, 22)
        Me.ToolStripMenuItem2.Text = "重新排序(&B)"
        '
        '任务检查ToolStripMenuItem
        '
        Me.任务检查ToolStripMenuItem.Image = CType(resources.GetObject("任务检查ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.任务检查ToolStripMenuItem.Name = "任务检查ToolStripMenuItem"
        Me.任务检查ToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.任务检查ToolStripMenuItem.Text = "任务检查"
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(173, 6)
        '
        '衔接计划设置GToolStripMenuItem
        '
        Me.衔接计划设置GToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.衔接计划管理ToolStripMenuItem, Me.衔接计划设置ToolStripMenuItem, Me.夜早班搭配ToolStripMenuItem})
        Me.衔接计划设置GToolStripMenuItem.Image = CType(resources.GetObject("衔接计划设置GToolStripMenuItem.Image"), System.Drawing.Image)
        Me.衔接计划设置GToolStripMenuItem.Name = "衔接计划设置GToolStripMenuItem"
        Me.衔接计划设置GToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.衔接计划设置GToolStripMenuItem.Text = "衔接计划设置(&G)"
        '
        '衔接计划管理ToolStripMenuItem
        '
        Me.衔接计划管理ToolStripMenuItem.Name = "衔接计划管理ToolStripMenuItem"
        Me.衔接计划管理ToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.衔接计划管理ToolStripMenuItem.Text = "衔接计划管理"
        '
        '衔接计划设置ToolStripMenuItem
        '
        Me.衔接计划设置ToolStripMenuItem.Name = "衔接计划设置ToolStripMenuItem"
        Me.衔接计划设置ToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.衔接计划设置ToolStripMenuItem.Text = "衔接计划设置"
        '
        '夜早班搭配ToolStripMenuItem
        '
        Me.夜早班搭配ToolStripMenuItem.Name = "夜早班搭配ToolStripMenuItem"
        Me.夜早班搭配ToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.夜早班搭配ToolStripMenuItem.Text = "夜早班搭配"
        '
        'ToolStripSeparator19
        '
        Me.ToolStripSeparator19.Name = "ToolStripSeparator19"
        Me.ToolStripSeparator19.Size = New System.Drawing.Size(173, 6)
        '
        '用餐设置DToolStripMenuItem
        '
        Me.用餐设置DToolStripMenuItem.Name = "用餐设置DToolStripMenuItem"
        Me.用餐设置DToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.用餐设置DToolStripMenuItem.Text = "用餐设置(&D)"
        '
        '设置区域信息ToolStripMenuItem
        '
        Me.设置区域信息ToolStripMenuItem.Name = "设置区域信息ToolStripMenuItem"
        Me.设置区域信息ToolStripMenuItem.Size = New System.Drawing.Size(176, 22)
        Me.设置区域信息ToolStripMenuItem.Text = "概要信息设置(&M)"
        '
        '查询ToolStripMenuItem1
        '
        Me.查询ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.乘务员ToolStripMenuItem, Me.统计数据ToolStripMenuItem, Me.出退勤地点统计IToolStripMenuItem, Me.查找列车FToolStripMenuItem})
        Me.查询ToolStripMenuItem1.Name = "查询ToolStripMenuItem1"
        Me.查询ToolStripMenuItem1.Size = New System.Drawing.Size(62, 21)
        Me.查询ToolStripMenuItem1.Text = "查询(&Q)"
        '
        '乘务员ToolStripMenuItem
        '
        Me.乘务员ToolStripMenuItem.Image = CType(resources.GetObject("乘务员ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.乘务员ToolStripMenuItem.Name = "乘务员ToolStripMenuItem"
        Me.乘务员ToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.乘务员ToolStripMenuItem.Text = "详细任务查询(&C)"
        '
        '统计数据ToolStripMenuItem
        '
        Me.统计数据ToolStripMenuItem.Image = CType(resources.GetObject("统计数据ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.统计数据ToolStripMenuItem.Name = "统计数据ToolStripMenuItem"
        Me.统计数据ToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.统计数据ToolStripMenuItem.Text = "统计数据(&S)"
        '
        '出退勤地点统计IToolStripMenuItem
        '
        Me.出退勤地点统计IToolStripMenuItem.Image = CType(resources.GetObject("出退勤地点统计IToolStripMenuItem.Image"), System.Drawing.Image)
        Me.出退勤地点统计IToolStripMenuItem.Name = "出退勤地点统计IToolStripMenuItem"
        Me.出退勤地点统计IToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.出退勤地点统计IToolStripMenuItem.Text = "地点统计(&I)"
        '
        '查找列车FToolStripMenuItem
        '
        Me.查找列车FToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.按内在编号查找任务ToolStripMenuItem, Me.按输出号查找任务ToolStripMenuItem})
        Me.查找列车FToolStripMenuItem.Image = CType(resources.GetObject("查找列车FToolStripMenuItem.Image"), System.Drawing.Image)
        Me.查找列车FToolStripMenuItem.Name = "查找列车FToolStripMenuItem"
        Me.查找列车FToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.查找列车FToolStripMenuItem.Text = "查找任务(&F)"
        '
        '按内在编号查找任务ToolStripMenuItem
        '
        Me.按内在编号查找任务ToolStripMenuItem.Name = "按内在编号查找任务ToolStripMenuItem"
        Me.按内在编号查找任务ToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+F"
        Me.按内在编号查找任务ToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
        Me.按内在编号查找任务ToolStripMenuItem.Text = "按内在编号查找任务"
        '
        '按输出号查找任务ToolStripMenuItem
        '
        Me.按输出号查找任务ToolStripMenuItem.Name = "按输出号查找任务ToolStripMenuItem"
        Me.按输出号查找任务ToolStripMenuItem.Size = New System.Drawing.Size(227, 22)
        Me.按输出号查找任务ToolStripMenuItem.Text = "按输出号查找任务"
        '
        '底图DToolStripMenuItem
        '
        Me.底图DToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.一分格ToolStripMenuItem, Me.二分格ToolStripMenuItem, Me.十分格ToolStripMenuItem, Me.小时格ToolStripMenuItem, Me.ToolStripSeparator5, Me.放大底图宽度ToolStripMenuItem, Me.缩小底图宽度ToolStripMenuItem, Me.纵向放大底图ZToolStripMenuItem, Me.纵向缩小底图XToolStripMenuItem, Me.ToolStripSeparator21, Me.底图线型与颜色CToolStripMenuItem})
        Me.底图DToolStripMenuItem.Name = "底图DToolStripMenuItem"
        Me.底图DToolStripMenuItem.Size = New System.Drawing.Size(61, 21)
        Me.底图DToolStripMenuItem.Text = "底图(&D)"
        '
        '一分格ToolStripMenuItem
        '
        Me.一分格ToolStripMenuItem.Image = CType(resources.GetObject("一分格ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.一分格ToolStripMenuItem.Name = "一分格ToolStripMenuItem"
        Me.一分格ToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.一分格ToolStripMenuItem.Text = "一分格图(&O)"
        '
        '二分格ToolStripMenuItem
        '
        Me.二分格ToolStripMenuItem.Image = CType(resources.GetObject("二分格ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.二分格ToolStripMenuItem.Name = "二分格ToolStripMenuItem"
        Me.二分格ToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.二分格ToolStripMenuItem.Text = "二分格图(&W)"
        '
        '十分格ToolStripMenuItem
        '
        Me.十分格ToolStripMenuItem.Image = CType(resources.GetObject("十分格ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.十分格ToolStripMenuItem.Name = "十分格ToolStripMenuItem"
        Me.十分格ToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.十分格ToolStripMenuItem.Text = "十分格图(&T)"
        '
        '小时格ToolStripMenuItem
        '
        Me.小时格ToolStripMenuItem.Image = CType(resources.GetObject("小时格ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.小时格ToolStripMenuItem.Name = "小时格ToolStripMenuItem"
        Me.小时格ToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.小时格ToolStripMenuItem.Text = "小时格图(&H)"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(205, 6)
        '
        '放大底图宽度ToolStripMenuItem
        '
        Me.放大底图宽度ToolStripMenuItem.Image = CType(resources.GetObject("放大底图宽度ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.放大底图宽度ToolStripMenuItem.Name = "放大底图宽度ToolStripMenuItem"
        Me.放大底图宽度ToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.J), System.Windows.Forms.Keys)
        Me.放大底图宽度ToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.放大底图宽度ToolStripMenuItem.Text = "横向放大底图(&B)"
        '
        '缩小底图宽度ToolStripMenuItem
        '
        Me.缩小底图宽度ToolStripMenuItem.Image = CType(resources.GetObject("缩小底图宽度ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.缩小底图宽度ToolStripMenuItem.Name = "缩小底图宽度ToolStripMenuItem"
        Me.缩小底图宽度ToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.K), System.Windows.Forms.Keys)
        Me.缩小底图宽度ToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.缩小底图宽度ToolStripMenuItem.Text = "横向缩小底图(&S)"
        '
        '纵向放大底图ZToolStripMenuItem
        '
        Me.纵向放大底图ZToolStripMenuItem.Image = CType(resources.GetObject("纵向放大底图ZToolStripMenuItem.Image"), System.Drawing.Image)
        Me.纵向放大底图ZToolStripMenuItem.Name = "纵向放大底图ZToolStripMenuItem"
        Me.纵向放大底图ZToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.纵向放大底图ZToolStripMenuItem.Text = "纵向放大底图(&Z)"
        '
        '纵向缩小底图XToolStripMenuItem
        '
        Me.纵向缩小底图XToolStripMenuItem.Image = CType(resources.GetObject("纵向缩小底图XToolStripMenuItem.Image"), System.Drawing.Image)
        Me.纵向缩小底图XToolStripMenuItem.Name = "纵向缩小底图XToolStripMenuItem"
        Me.纵向缩小底图XToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.纵向缩小底图XToolStripMenuItem.Text = "纵向缩小底图(&X)"
        '
        'ToolStripSeparator21
        '
        Me.ToolStripSeparator21.Name = "ToolStripSeparator21"
        Me.ToolStripSeparator21.Size = New System.Drawing.Size(205, 6)
        Me.ToolStripSeparator21.Visible = False
        '
        '底图线型与颜色CToolStripMenuItem
        '
        Me.底图线型与颜色CToolStripMenuItem.Image = CType(resources.GetObject("底图线型与颜色CToolStripMenuItem.Image"), System.Drawing.Image)
        Me.底图线型与颜色CToolStripMenuItem.Name = "底图线型与颜色CToolStripMenuItem"
        Me.底图线型与颜色CToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.底图线型与颜色CToolStripMenuItem.Text = "颜色与字体(&C)"
        Me.底图线型与颜色CToolStripMenuItem.Visible = False
        '
        '参数CToolStripMenuItem
        '
        Me.参数CToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.系统设置SToolStripMenuItem, Me.颜色与字体CToolStripMenuItem})
        Me.参数CToolStripMenuItem.Name = "参数CToolStripMenuItem"
        Me.参数CToolStripMenuItem.Size = New System.Drawing.Size(84, 21)
        Me.参数CToolStripMenuItem.Text = "系统参数(&C)"
        '
        '系统设置SToolStripMenuItem
        '
        Me.系统设置SToolStripMenuItem.Image = CType(resources.GetObject("系统设置SToolStripMenuItem.Image"), System.Drawing.Image)
        Me.系统设置SToolStripMenuItem.Name = "系统设置SToolStripMenuItem"
        Me.系统设置SToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.系统设置SToolStripMenuItem.Text = "系统设置(&P)"
        '
        '颜色与字体CToolStripMenuItem
        '
        Me.颜色与字体CToolStripMenuItem.Image = CType(resources.GetObject("颜色与字体CToolStripMenuItem.Image"), System.Drawing.Image)
        Me.颜色与字体CToolStripMenuItem.Name = "颜色与字体CToolStripMenuItem"
        Me.颜色与字体CToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.颜色与字体CToolStripMenuItem.Text = "颜色与字体(&C)"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.打开ToolStripButton1, Me.保存乘务计划ToolStripButton1, Me.导入司机位置图ToolStripButton1, Me.ToolStripSeparator26, Me.剪切ToolStripButton10, Me.粘贴ToolStripButton13, Me.复制ToolStripButton9, Me.删除ToolStripButton11, Me.查找ToolStripButton12, Me.ToolStripSeparator27, Me.撤销tolStripUndo, Me.重复tolStripRedo, Me.ToolStripSeparator17, Me.CmbSize, Me.底图放大ToolStripButton, Me.底图缩小ToolStripButton, Me.纵向放大ToolStripButton6, Me.纵向缩小ToolStripButton8, Me.ToolStripSeparator4, Me.检查错误ToolStripButton4, Me.ToolStripSeparator18, Me.刷新乘务交路图ToolStripButton3})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 25)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(886, 27)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        '打开ToolStripButton1
        '
        Me.打开ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.打开ToolStripButton1.Image = CType(resources.GetObject("打开ToolStripButton1.Image"), System.Drawing.Image)
        Me.打开ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.打开ToolStripButton1.Name = "打开ToolStripButton1"
        Me.打开ToolStripButton1.Size = New System.Drawing.Size(24, 24)
        Me.打开ToolStripButton1.Text = "打开"
        '
        '保存乘务计划ToolStripButton1
        '
        Me.保存乘务计划ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.保存乘务计划ToolStripButton1.Image = CType(resources.GetObject("保存乘务计划ToolStripButton1.Image"), System.Drawing.Image)
        Me.保存乘务计划ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.保存乘务计划ToolStripButton1.Name = "保存乘务计划ToolStripButton1"
        Me.保存乘务计划ToolStripButton1.Size = New System.Drawing.Size(24, 24)
        Me.保存乘务计划ToolStripButton1.Text = "保存"
        Me.保存乘务计划ToolStripButton1.ToolTipText = "保存乘务计划"
        '
        '导入司机位置图ToolStripButton1
        '
        Me.导入司机位置图ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.导入司机位置图ToolStripButton1.Image = CType(resources.GetObject("导入司机位置图ToolStripButton1.Image"), System.Drawing.Image)
        Me.导入司机位置图ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.导入司机位置图ToolStripButton1.Name = "导入司机位置图ToolStripButton1"
        Me.导入司机位置图ToolStripButton1.Size = New System.Drawing.Size(24, 24)
        Me.导入司机位置图ToolStripButton1.Text = "导入司机位置图"
        '
        'ToolStripSeparator26
        '
        Me.ToolStripSeparator26.Name = "ToolStripSeparator26"
        Me.ToolStripSeparator26.Size = New System.Drawing.Size(6, 27)
        '
        '剪切ToolStripButton10
        '
        Me.剪切ToolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.剪切ToolStripButton10.Image = CType(resources.GetObject("剪切ToolStripButton10.Image"), System.Drawing.Image)
        Me.剪切ToolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.剪切ToolStripButton10.Name = "剪切ToolStripButton10"
        Me.剪切ToolStripButton10.Size = New System.Drawing.Size(24, 24)
        Me.剪切ToolStripButton10.Text = "剪切"
        Me.剪切ToolStripButton10.ToolTipText = "剪切"
        Me.剪切ToolStripButton10.Visible = False
        '
        '粘贴ToolStripButton13
        '
        Me.粘贴ToolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.粘贴ToolStripButton13.Image = CType(resources.GetObject("粘贴ToolStripButton13.Image"), System.Drawing.Image)
        Me.粘贴ToolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.粘贴ToolStripButton13.Name = "粘贴ToolStripButton13"
        Me.粘贴ToolStripButton13.Size = New System.Drawing.Size(24, 24)
        Me.粘贴ToolStripButton13.Text = "粘贴"
        Me.粘贴ToolStripButton13.Visible = False
        '
        '复制ToolStripButton9
        '
        Me.复制ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.复制ToolStripButton9.Image = CType(resources.GetObject("复制ToolStripButton9.Image"), System.Drawing.Image)
        Me.复制ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.复制ToolStripButton9.Name = "复制ToolStripButton9"
        Me.复制ToolStripButton9.Size = New System.Drawing.Size(24, 24)
        Me.复制ToolStripButton9.Text = "复制"
        Me.复制ToolStripButton9.ToolTipText = "复制"
        Me.复制ToolStripButton9.Visible = False
        '
        '删除ToolStripButton11
        '
        Me.删除ToolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.删除ToolStripButton11.Image = CType(resources.GetObject("删除ToolStripButton11.Image"), System.Drawing.Image)
        Me.删除ToolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.删除ToolStripButton11.Name = "删除ToolStripButton11"
        Me.删除ToolStripButton11.Size = New System.Drawing.Size(24, 24)
        Me.删除ToolStripButton11.Text = "删除"
        Me.删除ToolStripButton11.ToolTipText = "删除"
        Me.删除ToolStripButton11.Visible = False
        '
        '查找ToolStripButton12
        '
        Me.查找ToolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.查找ToolStripButton12.Image = CType(resources.GetObject("查找ToolStripButton12.Image"), System.Drawing.Image)
        Me.查找ToolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.查找ToolStripButton12.Name = "查找ToolStripButton12"
        Me.查找ToolStripButton12.Size = New System.Drawing.Size(24, 24)
        Me.查找ToolStripButton12.Text = "查找"
        '
        'ToolStripSeparator27
        '
        Me.ToolStripSeparator27.Name = "ToolStripSeparator27"
        Me.ToolStripSeparator27.Size = New System.Drawing.Size(6, 27)
        Me.ToolStripSeparator27.Visible = False
        '
        '撤销tolStripUndo
        '
        Me.撤销tolStripUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.撤销tolStripUndo.Image = CType(resources.GetObject("撤销tolStripUndo.Image"), System.Drawing.Image)
        Me.撤销tolStripUndo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.撤销tolStripUndo.Name = "撤销tolStripUndo"
        Me.撤销tolStripUndo.Size = New System.Drawing.Size(24, 24)
        Me.撤销tolStripUndo.Text = "撤销"
        '
        '重复tolStripRedo
        '
        Me.重复tolStripRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.重复tolStripRedo.Image = CType(resources.GetObject("重复tolStripRedo.Image"), System.Drawing.Image)
        Me.重复tolStripRedo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.重复tolStripRedo.Name = "重复tolStripRedo"
        Me.重复tolStripRedo.Size = New System.Drawing.Size(24, 24)
        Me.重复tolStripRedo.Text = "重复"
        '
        'ToolStripSeparator17
        '
        Me.ToolStripSeparator17.Name = "ToolStripSeparator17"
        Me.ToolStripSeparator17.Size = New System.Drawing.Size(6, 27)
        '
        'CmbSize
        '
        Me.CmbSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbSize.Items.AddRange(New Object() {"50%", "75%", "100%", "125%", "150%", "200%"})
        Me.CmbSize.Name = "CmbSize"
        Me.CmbSize.Size = New System.Drawing.Size(121, 27)
        '
        '底图放大ToolStripButton
        '
        Me.底图放大ToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.底图放大ToolStripButton.Image = CType(resources.GetObject("底图放大ToolStripButton.Image"), System.Drawing.Image)
        Me.底图放大ToolStripButton.Name = "底图放大ToolStripButton"
        Me.底图放大ToolStripButton.Size = New System.Drawing.Size(24, 24)
        Me.底图放大ToolStripButton.Text = "横向放大"
        '
        '底图缩小ToolStripButton
        '
        Me.底图缩小ToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.底图缩小ToolStripButton.Image = CType(resources.GetObject("底图缩小ToolStripButton.Image"), System.Drawing.Image)
        Me.底图缩小ToolStripButton.Name = "底图缩小ToolStripButton"
        Me.底图缩小ToolStripButton.Size = New System.Drawing.Size(24, 24)
        Me.底图缩小ToolStripButton.Text = "横向缩小"
        '
        '纵向放大ToolStripButton6
        '
        Me.纵向放大ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.纵向放大ToolStripButton6.Image = CType(resources.GetObject("纵向放大ToolStripButton6.Image"), System.Drawing.Image)
        Me.纵向放大ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.纵向放大ToolStripButton6.Name = "纵向放大ToolStripButton6"
        Me.纵向放大ToolStripButton6.Size = New System.Drawing.Size(24, 24)
        Me.纵向放大ToolStripButton6.Text = "纵向放大"
        '
        '纵向缩小ToolStripButton8
        '
        Me.纵向缩小ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.纵向缩小ToolStripButton8.Image = CType(resources.GetObject("纵向缩小ToolStripButton8.Image"), System.Drawing.Image)
        Me.纵向缩小ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.纵向缩小ToolStripButton8.Name = "纵向缩小ToolStripButton8"
        Me.纵向缩小ToolStripButton8.Size = New System.Drawing.Size(24, 24)
        Me.纵向缩小ToolStripButton8.Text = "纵向缩小"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 27)
        '
        '检查错误ToolStripButton4
        '
        Me.检查错误ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.检查错误ToolStripButton4.Image = CType(resources.GetObject("检查错误ToolStripButton4.Image"), System.Drawing.Image)
        Me.检查错误ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.检查错误ToolStripButton4.Name = "检查错误ToolStripButton4"
        Me.检查错误ToolStripButton4.Size = New System.Drawing.Size(24, 24)
        Me.检查错误ToolStripButton4.Text = "检查错误"
        Me.检查错误ToolStripButton4.Visible = False
        '
        'ToolStripSeparator18
        '
        Me.ToolStripSeparator18.Name = "ToolStripSeparator18"
        Me.ToolStripSeparator18.Size = New System.Drawing.Size(6, 27)
        Me.ToolStripSeparator18.Visible = False
        '
        '刷新乘务交路图ToolStripButton3
        '
        Me.刷新乘务交路图ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.刷新乘务交路图ToolStripButton3.Image = CType(resources.GetObject("刷新乘务交路图ToolStripButton3.Image"), System.Drawing.Image)
        Me.刷新乘务交路图ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.刷新乘务交路图ToolStripButton3.Name = "刷新乘务交路图ToolStripButton3"
        Me.刷新乘务交路图ToolStripButton3.Size = New System.Drawing.Size(24, 24)
        Me.刷新乘务交路图ToolStripButton3.Text = "刷新乘务交路图"
        '
        'StatusStrip
        '
        Me.StatusStrip.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolLabError})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 568)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(886, 22)
        Me.StatusStrip.TabIndex = 3
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'ToolLabError
        '
        Me.ToolLabError.ForeColor = System.Drawing.Color.DarkBlue
        Me.ToolLabError.Name = "ToolLabError"
        Me.ToolLabError.Size = New System.Drawing.Size(140, 17)
        Me.ToolLabError.Text = "当前乘务计划没有错误！"
        '
        'cmuTrainLine
        '
        Me.cmuTrainLine.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.cmuTrainLine.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.安排驾驶员IToolStripMenuItem, Me.指派给乘务员eToolStripMenuItem, Me.断开驾驶员任务DToolStripMenuItem, Me.改变乘务员状态KToolStripMenuItem, Me.指向性手工安排乘务员AToolStripMenuItem, Me.修改驾驶员连接方式EToolStripMenuItem, Me.删除该乘务员DToolStripMenuItem, Me.ToolStripSeparator7, Me.用餐设置YToolStripMenuItem, Me.断开任务段SToolStripMenuItem, Me.安排关联任务MToolStripMenuItem, Me.ToolStripSeparator8, Me.安排随乘列车DHToolStripMenuItem, Me.安排随乘退勤列车TToolStripMenuItem, Me.断开随勤列车BToolStripMenuItem, Me.ToolStripSeparator9, Me.车底信息CToolStripMenuItem})
        Me.cmuTrainLine.Name = "cmuTrainLine"
        Me.cmuTrainLine.Size = New System.Drawing.Size(217, 386)
        '
        '安排驾驶员IToolStripMenuItem
        '
        Me.安排驾驶员IToolStripMenuItem.Image = CType(resources.GetObject("安排驾驶员IToolStripMenuItem.Image"), System.Drawing.Image)
        Me.安排驾驶员IToolStripMenuItem.Name = "安排驾驶员IToolStripMenuItem"
        Me.安排驾驶员IToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.安排驾驶员IToolStripMenuItem.Text = "安排新乘务员(&I)"
        '
        '指派给乘务员eToolStripMenuItem
        '
        Me.指派给乘务员eToolStripMenuItem.Image = CType(resources.GetObject("指派给乘务员eToolStripMenuItem.Image"), System.Drawing.Image)
        Me.指派给乘务员eToolStripMenuItem.Name = "指派给乘务员eToolStripMenuItem"
        Me.指派给乘务员eToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.指派给乘务员eToolStripMenuItem.Text = "指派给乘务员(&E)"
        '
        '断开驾驶员任务DToolStripMenuItem
        '
        Me.断开驾驶员任务DToolStripMenuItem.Image = CType(resources.GetObject("断开驾驶员任务DToolStripMenuItem.Image"), System.Drawing.Image)
        Me.断开驾驶员任务DToolStripMenuItem.Name = "断开驾驶员任务DToolStripMenuItem"
        Me.断开驾驶员任务DToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.断开驾驶员任务DToolStripMenuItem.Text = "断开乘务员任务(&D)"
        '
        '改变乘务员状态KToolStripMenuItem
        '
        Me.改变乘务员状态KToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.班中ToolStripMenuItem, Me.用餐ToolStripMenuItem, Me.班后ToolStripMenuItem, Me.替饭ToolStripMenuItem})
        Me.改变乘务员状态KToolStripMenuItem.Image = CType(resources.GetObject("改变乘务员状态KToolStripMenuItem.Image"), System.Drawing.Image)
        Me.改变乘务员状态KToolStripMenuItem.Name = "改变乘务员状态KToolStripMenuItem"
        Me.改变乘务员状态KToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.改变乘务员状态KToolStripMenuItem.Text = "改变乘务员状态(&K)"
        '
        '班中ToolStripMenuItem
        '
        Me.班中ToolStripMenuItem.Image = CType(resources.GetObject("班中ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.班中ToolStripMenuItem.Name = "班中ToolStripMenuItem"
        Me.班中ToolStripMenuItem.Size = New System.Drawing.Size(100, 22)
        Me.班中ToolStripMenuItem.Text = "班中"
        '
        '用餐ToolStripMenuItem
        '
        Me.用餐ToolStripMenuItem.Image = CType(resources.GetObject("用餐ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.用餐ToolStripMenuItem.Name = "用餐ToolStripMenuItem"
        Me.用餐ToolStripMenuItem.Size = New System.Drawing.Size(100, 22)
        Me.用餐ToolStripMenuItem.Text = "用餐"
        '
        '班后ToolStripMenuItem
        '
        Me.班后ToolStripMenuItem.Image = CType(resources.GetObject("班后ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.班后ToolStripMenuItem.Name = "班后ToolStripMenuItem"
        Me.班后ToolStripMenuItem.Size = New System.Drawing.Size(100, 22)
        Me.班后ToolStripMenuItem.Text = "班后"
        '
        '替饭ToolStripMenuItem
        '
        Me.替饭ToolStripMenuItem.Name = "替饭ToolStripMenuItem"
        Me.替饭ToolStripMenuItem.Size = New System.Drawing.Size(100, 22)
        Me.替饭ToolStripMenuItem.Text = "替饭"
        '
        '指向性手工安排乘务员AToolStripMenuItem
        '
        Me.指向性手工安排乘务员AToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.正向安排ToolStripMenuItem, Me.逆向安排ToolStripMenuItem})
        Me.指向性手工安排乘务员AToolStripMenuItem.Image = CType(resources.GetObject("指向性手工安排乘务员AToolStripMenuItem.Image"), System.Drawing.Image)
        Me.指向性手工安排乘务员AToolStripMenuItem.Name = "指向性手工安排乘务员AToolStripMenuItem"
        Me.指向性手工安排乘务员AToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.指向性手工安排乘务员AToolStripMenuItem.Text = "指向性手工安排乘务员(&A)"
        '
        '正向安排ToolStripMenuItem
        '
        Me.正向安排ToolStripMenuItem.Image = CType(resources.GetObject("正向安排ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.正向安排ToolStripMenuItem.Name = "正向安排ToolStripMenuItem"
        Me.正向安排ToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.正向安排ToolStripMenuItem.Text = "正向安排(&F)"
        '
        '逆向安排ToolStripMenuItem
        '
        Me.逆向安排ToolStripMenuItem.Image = CType(resources.GetObject("逆向安排ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.逆向安排ToolStripMenuItem.Name = "逆向安排ToolStripMenuItem"
        Me.逆向安排ToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.逆向安排ToolStripMenuItem.Text = "逆向安排(&B)"
        '
        '修改驾驶员连接方式EToolStripMenuItem
        '
        Me.修改驾驶员连接方式EToolStripMenuItem.Image = CType(resources.GetObject("修改驾驶员连接方式EToolStripMenuItem.Image"), System.Drawing.Image)
        Me.修改驾驶员连接方式EToolStripMenuItem.Name = "修改驾驶员连接方式EToolStripMenuItem"
        Me.修改驾驶员连接方式EToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.修改驾驶员连接方式EToolStripMenuItem.Text = "修改乘务员连接方式(&F)"
        '
        '删除该乘务员DToolStripMenuItem
        '
        Me.删除该乘务员DToolStripMenuItem.Image = CType(resources.GetObject("删除该乘务员DToolStripMenuItem.Image"), System.Drawing.Image)
        Me.删除该乘务员DToolStripMenuItem.Name = "删除该乘务员DToolStripMenuItem"
        Me.删除该乘务员DToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.删除该乘务员DToolStripMenuItem.Text = "删除该乘务员(&D)"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(213, 6)
        '
        '用餐设置YToolStripMenuItem
        '
        Me.用餐设置YToolStripMenuItem.Name = "用餐设置YToolStripMenuItem"
        Me.用餐设置YToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.用餐设置YToolStripMenuItem.Text = "用餐设置(&Y)"
        '
        '断开任务段SToolStripMenuItem
        '
        Me.断开任务段SToolStripMenuItem.Name = "断开任务段SToolStripMenuItem"
        Me.断开任务段SToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.断开任务段SToolStripMenuItem.Text = "断开任务段(&S)"
        '
        '安排关联任务MToolStripMenuItem
        '
        Me.安排关联任务MToolStripMenuItem.Image = CType(resources.GetObject("安排关联任务MToolStripMenuItem.Image"), System.Drawing.Image)
        Me.安排关联任务MToolStripMenuItem.Name = "安排关联任务MToolStripMenuItem"
        Me.安排关联任务MToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.安排关联任务MToolStripMenuItem.Text = "修改关联任务(&M)"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(213, 6)
        '
        '安排随乘列车DHToolStripMenuItem
        '
        Me.安排随乘列车DHToolStripMenuItem.Image = CType(resources.GetObject("安排随乘列车DHToolStripMenuItem.Image"), System.Drawing.Image)
        Me.安排随乘列车DHToolStripMenuItem.Name = "安排随乘列车DHToolStripMenuItem"
        Me.安排随乘列车DHToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.安排随乘列车DHToolStripMenuItem.Text = "安排随乘出勤列车(&H)"
        '
        '安排随乘退勤列车TToolStripMenuItem
        '
        Me.安排随乘退勤列车TToolStripMenuItem.Image = CType(resources.GetObject("安排随乘退勤列车TToolStripMenuItem.Image"), System.Drawing.Image)
        Me.安排随乘退勤列车TToolStripMenuItem.Name = "安排随乘退勤列车TToolStripMenuItem"
        Me.安排随乘退勤列车TToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.安排随乘退勤列车TToolStripMenuItem.Text = "安排随乘退勤列车(&T)"
        '
        '断开随勤列车BToolStripMenuItem
        '
        Me.断开随勤列车BToolStripMenuItem.Image = CType(resources.GetObject("断开随勤列车BToolStripMenuItem.Image"), System.Drawing.Image)
        Me.断开随勤列车BToolStripMenuItem.Name = "断开随勤列车BToolStripMenuItem"
        Me.断开随勤列车BToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.断开随勤列车BToolStripMenuItem.Text = "断开随乘列车(&B)"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(213, 6)
        '
        '车底信息CToolStripMenuItem
        '
        Me.车底信息CToolStripMenuItem.Image = CType(resources.GetObject("车底信息CToolStripMenuItem.Image"), System.Drawing.Image)
        Me.车底信息CToolStripMenuItem.Name = "车底信息CToolStripMenuItem"
        Me.车底信息CToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.车底信息CToolStripMenuItem.Text = "乘务员信息与属性(&C)"
        '
        '查询ToolStripMenuItem
        '
        Me.查询ToolStripMenuItem.Name = "查询ToolStripMenuItem"
        Me.查询ToolStripMenuItem.Size = New System.Drawing.Size(32, 19)
        Me.查询ToolStripMenuItem.Text = "查询"
        '
        'SplitError
        '
        Me.SplitError.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitError.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitError.Location = New System.Drawing.Point(0, 0)
        Me.SplitError.Name = "SplitError"
        Me.SplitError.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitError.Panel1
        '
        Me.SplitError.Panel1.AutoScroll = True
        Me.SplitError.Panel1.Controls.Add(Me.SplitConLeftRight)
        '
        'SplitError.Panel2
        '
        Me.SplitError.Panel2.Controls.Add(Me.TabControlMain)
        Me.SplitError.Panel2MinSize = 0
        Me.SplitError.Size = New System.Drawing.Size(886, 492)
        Me.SplitError.SplitterDistance = 351
        Me.SplitError.TabIndex = 6
        '
        'SplitConLeftRight
        '
        Me.SplitConLeftRight.BackColor = System.Drawing.SystemColors.Control
        Me.SplitConLeftRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitConLeftRight.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitConLeftRight.Location = New System.Drawing.Point(0, 0)
        Me.SplitConLeftRight.Name = "SplitConLeftRight"
        '
        'SplitConLeftRight.Panel1
        '
        Me.SplitConLeftRight.Panel1.AutoScroll = True
        Me.SplitConLeftRight.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.SplitConLeftRight.Panel1.Controls.Add(Me.SplitDiagram)
        Me.SplitConLeftRight.Panel1MinSize = 0
        '
        'SplitConLeftRight.Panel2
        '
        Me.SplitConLeftRight.Panel2.BackColor = System.Drawing.Color.White
        Me.SplitConLeftRight.Panel2.Controls.Add(Me.picStation)
        Me.SplitConLeftRight.Panel2MinSize = 0
        Me.SplitConLeftRight.Size = New System.Drawing.Size(886, 351)
        Me.SplitConLeftRight.SplitterDistance = 797
        Me.SplitConLeftRight.TabIndex = 5
        '
        'SplitDiagram
        '
        Me.SplitDiagram.BackColor = System.Drawing.SystemColors.Control
        Me.SplitDiagram.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitDiagram.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitDiagram.Location = New System.Drawing.Point(0, 0)
        Me.SplitDiagram.Name = "SplitDiagram"
        '
        'SplitDiagram.Panel1
        '
        Me.SplitDiagram.Panel1.BackColor = System.Drawing.Color.White
        Me.SplitDiagram.Panel1.Controls.Add(Me.PicStation2)
        Me.SplitDiagram.Panel1MinSize = 0
        '
        'SplitDiagram.Panel2
        '
        Me.SplitDiagram.Panel2.AutoScroll = True
        Me.SplitDiagram.Panel2.BackColor = System.Drawing.Color.White
        Me.SplitDiagram.Panel2.Controls.Add(Me.PicDiagram)
        Me.SplitDiagram.Size = New System.Drawing.Size(797, 351)
        Me.SplitDiagram.SplitterDistance = 91
        Me.SplitDiagram.TabIndex = 0
        '
        'PicStation2
        '
        Me.PicStation2.BackColor = System.Drawing.Color.White
        Me.PicStation2.Location = New System.Drawing.Point(12, 45)
        Me.PicStation2.Name = "PicStation2"
        Me.PicStation2.Size = New System.Drawing.Size(62, 232)
        Me.PicStation2.TabIndex = 6
        Me.PicStation2.TabStop = False
        '
        'PicDiagram
        '
        Me.PicDiagram.BackColor = System.Drawing.Color.White
        Me.PicDiagram.Location = New System.Drawing.Point(61, 45)
        Me.PicDiagram.Name = "PicDiagram"
        Me.PicDiagram.Size = New System.Drawing.Size(589, 266)
        Me.PicDiagram.TabIndex = 4
        Me.PicDiagram.TabStop = False
        '
        'picStation
        '
        Me.picStation.BackColor = System.Drawing.Color.White
        Me.picStation.Location = New System.Drawing.Point(11, 45)
        Me.picStation.Name = "picStation"
        Me.picStation.Size = New System.Drawing.Size(62, 232)
        Me.picStation.TabIndex = 5
        Me.picStation.TabStop = False
        '
        'TabControlMain
        '
        Me.TabControlMain.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.TabControlMain.Controls.Add(Me.TabPage1)
        Me.TabControlMain.Controls.Add(Me.TabPage2)
        Me.TabControlMain.Controls.Add(Me.TabPage3)
        Me.TabControlMain.Controls.Add(Me.TabPage5)
        Me.TabControlMain.Controls.Add(Me.TabPage4)
        Me.TabControlMain.Controls.Add(Me.TabPage6)
        Me.TabControlMain.Controls.Add(Me.TabPage7)
        Me.TabControlMain.Controls.Add(Me.TabPage8)
        Me.TabControlMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlMain.Location = New System.Drawing.Point(0, 0)
        Me.TabControlMain.Name = "TabControlMain"
        Me.TabControlMain.SelectedIndex = 0
        Me.TabControlMain.Size = New System.Drawing.Size(886, 137)
        Me.TabControlMain.TabIndex = 8
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.listViewTrain)
        Me.TabPage1.Location = New System.Drawing.Point(4, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(878, 111)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "空闲车次"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'listViewTrain
        '
        Me.listViewTrain.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.listViewTrain.AllowColumnReorder = True
        Me.listViewTrain.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.序号, Me.列车ID, Me.开始站名, Me.开始时间, Me.上车车次, Me.终到站名, Me.终到时间, Me.下车车次, Me.里程})
        Me.listViewTrain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listViewTrain.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.listViewTrain.FullRowSelect = True
        Me.listViewTrain.GridLines = True
        Me.listViewTrain.Location = New System.Drawing.Point(3, 3)
        Me.listViewTrain.MultiSelect = False
        Me.listViewTrain.Name = "listViewTrain"
        Me.listViewTrain.Size = New System.Drawing.Size(872, 105)
        Me.listViewTrain.TabIndex = 8
        Me.listViewTrain.UseCompatibleStateImageBehavior = False
        Me.listViewTrain.View = System.Windows.Forms.View.Details
        '
        '序号
        '
        Me.序号.Text = "序号"
        Me.序号.Width = 40
        '
        '列车ID
        '
        Me.列车ID.Text = "列车ID"
        '
        '开始站名
        '
        Me.开始站名.Text = "开始站名"
        Me.开始站名.Width = 100
        '
        '开始时间
        '
        Me.开始时间.Text = "开始时间"
        Me.开始时间.Width = 100
        '
        '上车车次
        '
        Me.上车车次.Text = "上车车次"
        Me.上车车次.Width = 100
        '
        '终到站名
        '
        Me.终到站名.Text = "终到站名"
        Me.终到站名.Width = 100
        '
        '终到时间
        '
        Me.终到时间.Text = "终到时间"
        Me.终到时间.Width = 100
        '
        '下车车次
        '
        Me.下车车次.Text = "下车车次"
        Me.下车车次.Width = 100
        '
        '里程
        '
        Me.里程.Text = "里程"
        Me.里程.Width = 100
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ListViewDuty)
        Me.TabPage2.Location = New System.Drawing.Point(4, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(878, 111)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "任务信息"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'ListViewDuty
        '
        Me.ListViewDuty.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ListViewDuty.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader3, Me.ColumnHeader2, Me.ColumnHeader11, Me.ColumnHeader24, Me.ColumnHeader4, Me.ColumnHeader5, Me.是否用餐, Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10})
        Me.ListViewDuty.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewDuty.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ListViewDuty.FullRowSelect = True
        Me.ListViewDuty.GridLines = True
        Me.ListViewDuty.Location = New System.Drawing.Point(3, 3)
        Me.ListViewDuty.MultiSelect = False
        Me.ListViewDuty.Name = "ListViewDuty"
        Me.ListViewDuty.Size = New System.Drawing.Size(872, 105)
        Me.ListViewDuty.TabIndex = 9
        Me.ListViewDuty.UseCompatibleStateImageBehavior = False
        Me.ListViewDuty.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "序号"
        Me.ColumnHeader1.Width = 40
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "任务ID"
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "任务号"
        Me.ColumnHeader2.Width = 100
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "输出号"
        Me.ColumnHeader11.Width = 100
        '
        'ColumnHeader24
        '
        Me.ColumnHeader24.Text = "班种"
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "出勤地点"
        Me.ColumnHeader4.Width = 100
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "出勤时间"
        Me.ColumnHeader5.Width = 100
        '
        '是否用餐
        '
        Me.是否用餐.Text = "是否用餐"
        Me.是否用餐.Width = 100
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "退勤地点"
        Me.ColumnHeader6.Width = 100
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "退勤时间"
        Me.ColumnHeader7.Width = 100
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "当前状态"
        Me.ColumnHeader8.Width = 100
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "驾驶公里"
        Me.ColumnHeader9.Width = 100
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "工作时间"
        Me.ColumnHeader10.Width = 100
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.ListViewCurDuty)
        Me.TabPage3.Location = New System.Drawing.Point(4, 4)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(878, 111)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "当前任务"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'ListViewCurDuty
        '
        Me.ListViewCurDuty.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ListViewCurDuty.AllowColumnReorder = True
        Me.ListViewCurDuty.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader12, Me.ColumnHeader13, Me.ColumnHeader14, Me.ColumnHeader15, Me.ColumnHeader16, Me.ColumnHeader17, Me.ColumnHeader20, Me.ColumnHeader18, Me.ColumnHeader19, Me.ColumnHeader21, Me.ColumnHeader42})
        Me.ListViewCurDuty.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewCurDuty.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ListViewCurDuty.FullRowSelect = True
        Me.ListViewCurDuty.GridLines = True
        Me.ListViewCurDuty.Location = New System.Drawing.Point(3, 3)
        Me.ListViewCurDuty.MultiSelect = False
        Me.ListViewCurDuty.Name = "ListViewCurDuty"
        Me.ListViewCurDuty.Size = New System.Drawing.Size(872, 105)
        Me.ListViewCurDuty.TabIndex = 10
        Me.ListViewCurDuty.UseCompatibleStateImageBehavior = False
        Me.ListViewCurDuty.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "序号"
        Me.ColumnHeader12.Width = 40
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "任务ID"
        '
        'ColumnHeader14
        '
        Me.ColumnHeader14.Text = "任务号"
        Me.ColumnHeader14.Width = 100
        '
        'ColumnHeader15
        '
        Me.ColumnHeader15.Text = "输出号"
        Me.ColumnHeader15.Width = 100
        '
        'ColumnHeader16
        '
        Me.ColumnHeader16.Text = "开始站名"
        Me.ColumnHeader16.Width = 100
        '
        'ColumnHeader17
        '
        Me.ColumnHeader17.Text = "开始时间"
        Me.ColumnHeader17.Width = 100
        '
        'ColumnHeader20
        '
        Me.ColumnHeader20.Text = "上车车次"
        Me.ColumnHeader20.Width = 100
        '
        'ColumnHeader18
        '
        Me.ColumnHeader18.Text = "结束站名"
        Me.ColumnHeader18.Width = 100
        '
        'ColumnHeader19
        '
        Me.ColumnHeader19.Text = "结束时间"
        Me.ColumnHeader19.Width = 100
        '
        'ColumnHeader21
        '
        Me.ColumnHeader21.Text = "下车车次"
        Me.ColumnHeader21.Width = 100
        '
        'ColumnHeader42
        '
        Me.ColumnHeader42.Text = "累计公里"
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.ListViewPosition)
        Me.TabPage5.Location = New System.Drawing.Point(4, 4)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(878, 111)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "司机位置"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'ListViewPosition
        '
        Me.ListViewPosition.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ListViewPosition.AllowColumnReorder = True
        Me.ListViewPosition.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader25, Me.ColumnHeader26, Me.ColumnHeader27, Me.任务编号})
        Me.ListViewPosition.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewPosition.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ListViewPosition.FullRowSelect = True
        Me.ListViewPosition.GridLines = True
        Me.ListViewPosition.Location = New System.Drawing.Point(3, 3)
        Me.ListViewPosition.MultiSelect = False
        Me.ListViewPosition.Name = "ListViewPosition"
        Me.ListViewPosition.Size = New System.Drawing.Size(872, 105)
        Me.ListViewPosition.TabIndex = 12
        Me.ListViewPosition.UseCompatibleStateImageBehavior = False
        Me.ListViewPosition.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader25
        '
        Me.ColumnHeader25.Text = "序号"
        Me.ColumnHeader25.Width = 40
        '
        'ColumnHeader26
        '
        Me.ColumnHeader26.Text = "车站"
        Me.ColumnHeader26.Width = 150
        '
        'ColumnHeader27
        '
        Me.ColumnHeader27.Text = "司机数量"
        Me.ColumnHeader27.Width = 100
        '
        '任务编号
        '
        Me.任务编号.Text = "任务编号"
        Me.任务编号.Width = 600
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.ListViewInOutDepot)
        Me.TabPage4.Location = New System.Drawing.Point(4, 4)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(878, 111)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "出入库信息"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'ListViewInOutDepot
        '
        Me.ListViewInOutDepot.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ListViewInOutDepot.AllowColumnReorder = True
        Me.ListViewInOutDepot.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader22, Me.ColumnHeader23, Me.ColumnHeader32, Me.ColumnHeader43, Me.ColumnHeader33, Me.ColumnHeader44})
        Me.ListViewInOutDepot.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewInOutDepot.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ListViewInOutDepot.FullRowSelect = True
        Me.ListViewInOutDepot.GridLines = True
        Me.ListViewInOutDepot.Location = New System.Drawing.Point(3, 3)
        Me.ListViewInOutDepot.MultiSelect = False
        Me.ListViewInOutDepot.Name = "ListViewInOutDepot"
        Me.ListViewInOutDepot.Size = New System.Drawing.Size(872, 105)
        Me.ListViewInOutDepot.TabIndex = 11
        Me.ListViewInOutDepot.UseCompatibleStateImageBehavior = False
        Me.ListViewInOutDepot.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader22
        '
        Me.ColumnHeader22.Text = "序号"
        Me.ColumnHeader22.Width = 40
        '
        'ColumnHeader23
        '
        Me.ColumnHeader23.Text = "车场"
        Me.ColumnHeader23.Width = 150
        '
        'ColumnHeader32
        '
        Me.ColumnHeader32.Text = "早班出库数量"
        Me.ColumnHeader32.Width = 100
        '
        'ColumnHeader43
        '
        Me.ColumnHeader43.Text = "剩余出库车"
        Me.ColumnHeader43.Width = 100
        '
        'ColumnHeader33
        '
        Me.ColumnHeader33.Text = "夜班入库数量"
        Me.ColumnHeader33.Width = 100
        '
        'ColumnHeader44
        '
        Me.ColumnHeader44.Text = "剩余入库车"
        Me.ColumnHeader44.Width = 100
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.ListViewDutyNum)
        Me.TabPage6.Location = New System.Drawing.Point(4, 4)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(878, 111)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "任务数统计"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'ListViewDutyNum
        '
        Me.ListViewDutyNum.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ListViewDutyNum.AllowColumnReorder = True
        Me.ListViewDutyNum.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader28, Me.ColumnHeader29, Me.ColumnHeader34, Me.ColumnHeader30, Me.ColumnHeader31, Me.ColumnHeader35, Me.ColumnHeader36, Me.ColumnHeader37})
        Me.ListViewDutyNum.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewDutyNum.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ListViewDutyNum.FullRowSelect = True
        Me.ListViewDutyNum.GridLines = True
        Me.ListViewDutyNum.Location = New System.Drawing.Point(3, 3)
        Me.ListViewDutyNum.MultiSelect = False
        Me.ListViewDutyNum.Name = "ListViewDutyNum"
        Me.ListViewDutyNum.Size = New System.Drawing.Size(872, 105)
        Me.ListViewDutyNum.TabIndex = 12
        Me.ListViewDutyNum.UseCompatibleStateImageBehavior = False
        Me.ListViewDutyNum.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader28
        '
        Me.ColumnHeader28.Text = "序号"
        Me.ColumnHeader28.Width = 40
        '
        'ColumnHeader29
        '
        Me.ColumnHeader29.Text = "班种"
        '
        'ColumnHeader34
        '
        Me.ColumnHeader34.Text = "当前计划数量"
        Me.ColumnHeader34.Width = 100
        '
        'ColumnHeader30
        '
        Me.ColumnHeader30.Text = "出勤位置统计"
        Me.ColumnHeader30.Width = 250
        '
        'ColumnHeader31
        '
        Me.ColumnHeader31.Text = "退勤位置统计"
        Me.ColumnHeader31.Width = 250
        '
        'ColumnHeader35
        '
        Me.ColumnHeader35.Text = "衔接计划数量"
        Me.ColumnHeader35.Width = 100
        '
        'ColumnHeader36
        '
        Me.ColumnHeader36.Text = "衔接计划出勤位置统计"
        Me.ColumnHeader36.Width = 250
        '
        'ColumnHeader37
        '
        Me.ColumnHeader37.Text = "衔接计划退勤数量统计"
        Me.ColumnHeader37.Width = 250
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.ListViewMOnOffInfo)
        Me.TabPage7.Location = New System.Drawing.Point(4, 4)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage7.Size = New System.Drawing.Size(878, 111)
        Me.TabPage7.TabIndex = 6
        Me.TabPage7.Text = "早班出退勤统计"
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'ListViewMOnOffInfo
        '
        Me.ListViewMOnOffInfo.AllowColumnReorder = True
        Me.ListViewMOnOffInfo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader46, Me.ColumnHeader47, Me.ColumnHeader38, Me.ColumnHeader39, Me.ColumnHeader40, Me.ColumnHeader41})
        Me.ListViewMOnOffInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewMOnOffInfo.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ListViewMOnOffInfo.FullRowSelect = True
        Me.ListViewMOnOffInfo.GridLines = True
        Me.ListViewMOnOffInfo.Location = New System.Drawing.Point(3, 3)
        Me.ListViewMOnOffInfo.MultiSelect = False
        Me.ListViewMOnOffInfo.Name = "ListViewMOnOffInfo"
        Me.ListViewMOnOffInfo.Size = New System.Drawing.Size(872, 105)
        Me.ListViewMOnOffInfo.TabIndex = 13
        Me.ListViewMOnOffInfo.UseCompatibleStateImageBehavior = False
        Me.ListViewMOnOffInfo.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader46
        '
        Me.ColumnHeader46.Text = "序号"
        '
        'ColumnHeader47
        '
        Me.ColumnHeader47.Text = "早班出勤地点"
        Me.ColumnHeader47.Width = 100
        '
        'ColumnHeader38
        '
        Me.ColumnHeader38.Text = "任务数量"
        '
        'ColumnHeader39
        '
        Me.ColumnHeader39.Text = "退勤统计"
        Me.ColumnHeader39.Width = 300
        '
        'ColumnHeader40
        '
        Me.ColumnHeader40.Text = "衔接数量"
        '
        'ColumnHeader41
        '
        Me.ColumnHeader41.Text = "衔接计划退勤统计"
        Me.ColumnHeader41.Width = 300
        '
        'TabPage8
        '
        Me.TabPage8.Controls.Add(Me.ListViewUnDead)
        Me.TabPage8.Location = New System.Drawing.Point(4, 4)
        Me.TabPage8.Name = "TabPage8"
        Me.TabPage8.Size = New System.Drawing.Size(878, 111)
        Me.TabPage8.TabIndex = 7
        Me.TabPage8.Text = "夜早班未安排随乘任务"
        Me.TabPage8.UseVisualStyleBackColor = True
        '
        'ListViewUnDead
        '
        Me.ListViewUnDead.AllowColumnReorder = True
        Me.ListViewUnDead.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader45, Me.ColumnHeader48, Me.ColumnHeader49})
        Me.ListViewUnDead.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListViewUnDead.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.ListViewUnDead.FullRowSelect = True
        Me.ListViewUnDead.GridLines = True
        Me.ListViewUnDead.Location = New System.Drawing.Point(0, 0)
        Me.ListViewUnDead.MultiSelect = False
        Me.ListViewUnDead.Name = "ListViewUnDead"
        Me.ListViewUnDead.Size = New System.Drawing.Size(878, 111)
        Me.ListViewUnDead.TabIndex = 14
        Me.ListViewUnDead.UseCompatibleStateImageBehavior = False
        Me.ListViewUnDead.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader45
        '
        Me.ColumnHeader45.Text = "序号"
        '
        'ColumnHeader48
        '
        Me.ColumnHeader48.Text = "班种"
        Me.ColumnHeader48.Width = 100
        '
        'ColumnHeader49
        '
        Me.ColumnHeader49.Text = "未安排随乘的任务"
        Me.ColumnHeader49.Width = 1000
        '
        'labInfor
        '
        Me.labInfor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.labInfor.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labInfor.ForeColor = System.Drawing.Color.Green
        Me.labInfor.Location = New System.Drawing.Point(234, 0)
        Me.labInfor.Name = "labInfor"
        Me.labInfor.Size = New System.Drawing.Size(652, 20)
        Me.labInfor.TabIndex = 0
        Me.labInfor.Text = "在此显示相关信息"
        '
        'SplitMainContainer
        '
        Me.SplitMainContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitMainContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitMainContainer.IsSplitterFixed = True
        Me.SplitMainContainer.Location = New System.Drawing.Point(0, 52)
        Me.SplitMainContainer.Name = "SplitMainContainer"
        Me.SplitMainContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitMainContainer.Panel1
        '
        Me.SplitMainContainer.Panel1.Controls.Add(Me.LabelPro)
        Me.SplitMainContainer.Panel1.Controls.Add(Me.ProgressBar1)
        Me.SplitMainContainer.Panel1.Controls.Add(Me.labInfor)
        Me.SplitMainContainer.Panel1.Controls.Add(Me.labName)
        Me.SplitMainContainer.Panel1MinSize = 20
        '
        'SplitMainContainer.Panel2
        '
        Me.SplitMainContainer.Panel2.Controls.Add(Me.SplitError)
        Me.SplitMainContainer.Size = New System.Drawing.Size(886, 516)
        Me.SplitMainContainer.SplitterDistance = 20
        Me.SplitMainContainer.TabIndex = 4
        Me.SplitMainContainer.Text = "SplitContainer1"
        '
        'LabelPro
        '
        Me.LabelPro.Dock = System.Windows.Forms.DockStyle.Right
        Me.LabelPro.ForeColor = System.Drawing.Color.Green
        Me.LabelPro.Location = New System.Drawing.Point(521, 0)
        Me.LabelPro.Name = "LabelPro"
        Me.LabelPro.Size = New System.Drawing.Size(76, 20)
        Me.LabelPro.TabIndex = 3
        Me.LabelPro.Text = "正在计算..."
        Me.LabelPro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.LabelPro.Visible = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Dock = System.Windows.Forms.DockStyle.Right
        Me.ProgressBar1.Location = New System.Drawing.Point(597, 0)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(289, 20)
        Me.ProgressBar1.TabIndex = 2
        Me.ProgressBar1.Visible = False
        '
        'labName
        '
        Me.labName.Dock = System.Windows.Forms.DockStyle.Left
        Me.labName.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.labName.Location = New System.Drawing.Point(0, 0)
        Me.labName.Name = "labName"
        Me.labName.Size = New System.Drawing.Size(234, 20)
        Me.labName.TabIndex = 1
        Me.labName.Text = "当前乘务计划: 无"
        '
        'labChediNum
        '
        Me.labChediNum.AutoSize = True
        Me.labChediNum.Location = New System.Drawing.Point(422, 30)
        Me.labChediNum.Name = "labChediNum"
        Me.labChediNum.Size = New System.Drawing.Size(53, 12)
        Me.labChediNum.TabIndex = 5
        Me.labChediNum.Text = "车底信息"
        '
        'frmCSTimeTableMain
        '
        Me.ClientSize = New System.Drawing.Size(886, 590)
        Me.Controls.Add(Me.labChediNum)
        Me.Controls.Add(Me.SplitMainContainer)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.MenuMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmCSTimeTableMain"
        Me.Text = "乘务计划编制"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuMain.ResumeLayout(False)
        Me.MenuMain.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.cmuTrainLine.ResumeLayout(False)
        Me.SplitError.Panel1.ResumeLayout(False)
        Me.SplitError.Panel2.ResumeLayout(False)
        Me.SplitError.ResumeLayout(False)
        Me.SplitConLeftRight.Panel1.ResumeLayout(False)
        Me.SplitConLeftRight.Panel2.ResumeLayout(False)
        Me.SplitConLeftRight.ResumeLayout(False)
        Me.SplitDiagram.Panel1.ResumeLayout(False)
        Me.SplitDiagram.Panel2.ResumeLayout(False)
        Me.SplitDiagram.ResumeLayout(False)
        CType(Me.PicStation2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PicDiagram, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picStation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControlMain.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.TabPage8.ResumeLayout(False)
        Me.SplitMainContainer.Panel1.ResumeLayout(False)
        Me.SplitMainContainer.Panel2.ResumeLayout(False)
        Me.SplitMainContainer.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitConMain As System.Windows.Forms.SplitContainer
    Friend WithEvents MenuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents 文件7FToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 打开运行图ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 分隔一ToolStripMenuItem As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 退出EToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 底图DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 小时格ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 十分格ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 二分格ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 一分格ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 放大底图宽度ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 缩小底图宽度ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents 底图放大ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 底图缩小ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents cmuTrainLine As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 断开驾驶员任务DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 修改驾驶员连接方式EToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 车底信息CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 输出OToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 打开ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator17 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 刷新乘务交路图ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 检查错误ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator18 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 参数CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 撤销tolStripUndo As System.Windows.Forms.ToolStripButton
    Friend WithEvents 重复tolStripRedo As System.Windows.Forms.ToolStripButton
    Friend WithEvents 系统设置SToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 纵向放大底图ZToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 纵向缩小底图XToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 纵向放大ToolStripButton6 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 纵向缩小ToolStripButton8 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator21 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 安排驾驶员IToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 复制ToolStripButton9 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator26 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 剪切ToolStripButton10 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 粘贴ToolStripButton13 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 删除ToolStripButton11 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 查找ToolStripButton12 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator27 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 保存乘务计划ToolStripButton1 As System.Windows.Forms.ToolStripButton

    Private Sub SplitDiagram_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs)
        MsgBox("aa")
    End Sub
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 参数设置ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 保存ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 选择运行图ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 管理ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 查询ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 乘务员ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 统计数据ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 查询ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitMainContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents labInfor As System.Windows.Forms.Label
    Friend WithEvents SplitError As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitConLeftRight As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitDiagram As System.Windows.Forms.SplitContainer
    Friend WithEvents PicStation2 As System.Windows.Forms.PictureBox
    Friend WithEvents PicDiagram As System.Windows.Forms.PictureBox
    Friend WithEvents picStation As System.Windows.Forms.PictureBox
    Friend WithEvents ToolLabError As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents 输出ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 图片IToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 底图线型与颜色CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 操作OToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 撤销CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 回复RToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 删除该乘务员DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 出退勤地点统计IToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 指向性手工安排乘务员AToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 钓鱼图DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 安排随乘列车DHToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 安排随乘退勤列车TToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 整理任务SToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 安排关联任务MToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 正向安排ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 逆向安排ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 断开随勤列车BToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabControlMain As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents listViewTrain As System.Windows.Forms.ListView
    Friend WithEvents 序号 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 上车车次 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 列车ID As System.Windows.Forms.ColumnHeader
    Friend WithEvents 开始站名 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 开始时间 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 终到站名 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 终到时间 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 里程 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents ListViewDuty As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 是否用餐 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents ListViewCurDuty As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader12 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader13 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader14 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader15 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader16 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader17 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader18 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader19 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader20 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader21 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents ListViewInOutDepot As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader22 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader23 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader32 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader33 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader24 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 指派给乘务员eToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 改变乘务员状态KToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 班中ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 用餐ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 班后ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents ListViewPosition As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader25 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader26 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader27 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 计算机安排CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents labName As System.Windows.Forms.Label
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents ListViewDutyNum As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader28 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader29 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader30 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader31 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader34 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 查找列车FToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 可变动参数TToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LabelPro As System.Windows.Forms.Label
    Friend WithEvents 任务编号 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader35 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader36 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader37 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents ListViewMOnOffInfo As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader46 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader47 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader38 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader39 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader40 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader41 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 颜色与字体CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ColumnHeader42 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader43 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader44 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 另存为ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CmbSize As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents 钓鱼图反导入ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 断开任务段SToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 设置区域信息ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabPage8 As System.Windows.Forms.TabPage
    Friend WithEvents ListViewUnDead As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader45 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader48 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader49 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 下车车次 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 替饭ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 安排乘务员人数ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator15 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator14 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 清空当前计划ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 指定条件清空计划ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 安排完早班ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 安排完白班ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 安排完日勤班ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 安排完夜班ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 用餐设置DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 用餐设置YToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 输出车底交路CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator19 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 导入IToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 导入司机位置图ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents labChediNum As System.Windows.Forms.Label
    Friend WithEvents 安排完吃饭ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 备车设置ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 衔接计划设置GToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 衔接计划管理ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 衔接计划设置ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 夜早班搭配ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 人数测算ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 任务检查ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 放宽限制ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 任务段衔接ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 按时间清空ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 清空早班ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 清空夜班ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 清空白班ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 清空日勤班ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 整理吃饭ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 位置图导出ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 白班中午吃饭ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 按内在编号查找任务ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 按输出号查找任务ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 自动优化ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 合并短任务ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 重新处理晚餐ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
