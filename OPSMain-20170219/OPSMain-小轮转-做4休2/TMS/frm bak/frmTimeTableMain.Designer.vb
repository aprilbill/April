Partial Public Class frmTimeTableMain
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTimeTableMain))
        Me.MenuMain = New System.Windows.Forms.MenuStrip
        Me.文件7FToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.打开运行图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.新建运行图NToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.分隔一ToolStripMenuItem = New System.Windows.Forms.ToolStripSeparator
        Me.保存运行图SToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.运行图管理MToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.运行图另存为图片ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.输出OToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.运行图DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.车底交路图CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator
        Me.列车时刻表TToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.车站股道技术图解JToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator24 = New System.Windows.Forms.ToolStripSeparator
        Me.运行图数据接口KToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.编辑时刻表车站顺序EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.退出EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.编辑EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.撤销UToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.重复RToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator
        Me.剪切CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.复制CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.粘贴VToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.删除DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator19 = New System.Windows.Forms.ToolStripSeparator
        Me.全选AToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.查找FToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.底图DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.一分格ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.二分格ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.分隔线二ToolStripMenuItem = New System.Windows.Forms.ToolStripSeparator
        Me.十分格ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.小时格ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.向左翻页ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.向右翻页ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator40 = New System.Windows.Forms.ToolStripSeparator
        Me.放大底图宽度ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.缩小底图宽度ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.纵向放大底图ZToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.纵向缩小底图XToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator21 = New System.Windows.Forms.ToolStripSeparator
        Me.底图线型与颜色CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.运行图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.刷新运行图ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.查看列车时刻表TToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.增加列车AToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
        Me.运行图编制ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.单一或大小及共线交路ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.环形交路ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.车底组铺画DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.初始车底组铺画ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.继续ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.一次性铺画方案OToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator33 = New System.Windows.Forms.ToolStripSeparator
        Me.铺画间隔设置ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator
        Me.列车重编车次BToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.整理车站股道ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.整理列车ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.整理到发时刻DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.整理折返时刻ZToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.检查列车运行图KToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator20 = New System.Windows.Forms.ToolStripSeparator
        Me.计算指标XToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.线型与颜色LToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator16 = New System.Windows.Forms.ToolStripSeparator
        Me.全部列车重新铺画AToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.批处理组操作UToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.能力计算CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.错误提示ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.全部显示ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.只显示前20条ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.技术图解JToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.车站股道使用ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.仿真SToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.基于运行图仿真ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.线路通过能力仿真TToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator31 = New System.Windows.Forms.ToolStripSeparator
        Me.折返仿真RToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.牵引曲线QToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.参数CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.约束铺画YToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.铺画方式ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.不越行ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.可以越行ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator36 = New System.Windows.Forms.ToolStripSeparator
        Me.调整时移动提示ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator32 = New System.Windows.Forms.ToolStripSeparator
        Me.自动检查错误EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.自动重编车次ZToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator28 = New System.Windows.Forms.ToolStripSeparator
        Me.系统设置SToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.打开ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.新建ToolStripButton5 = New System.Windows.Forms.ToolStripButton
        Me.保存ToolStripButton7 = New System.Windows.Forms.ToolStripButton
        Me.打印运行图ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator29 = New System.Windows.Forms.ToolStripSeparator
        Me.运行图管理ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator26 = New System.Windows.Forms.ToolStripSeparator
        Me.剪切ToolStripButton10 = New System.Windows.Forms.ToolStripButton
        Me.粘贴ToolStripButton13 = New System.Windows.Forms.ToolStripButton
        Me.复制ToolStripButton9 = New System.Windows.Forms.ToolStripButton
        Me.删除ToolStripButton11 = New System.Windows.Forms.ToolStripButton
        Me.查找ToolStripButton12 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator27 = New System.Windows.Forms.ToolStripSeparator
        Me.撤销tolStripUndo = New System.Windows.Forms.ToolStripButton
        Me.重复tolStripRedo = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator17 = New System.Windows.Forms.ToolStripSeparator
        Me.时间格式ToolStripButton = New System.Windows.Forms.ToolStripDropDownButton
        Me.一分格ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.二分格ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.十分格ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.小时格ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripLeft = New System.Windows.Forms.ToolStripButton
        Me.ToolStripRight = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator41 = New System.Windows.Forms.ToolStripSeparator
        Me.底图放大ToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.底图缩小ToolStripButton = New System.Windows.Forms.ToolStripButton
        Me.纵向放大ToolStripButton6 = New System.Windows.Forms.ToolStripButton
        Me.纵向缩小ToolStripButton8 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.线型与颜色ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.检查错误ToolStripButton4 = New System.Windows.Forms.ToolStripButton
        Me.测量时间ToolStripButton9 = New System.Windows.Forms.ToolStripButton
        Me.多选ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator18 = New System.Windows.Forms.ToolStripSeparator
        Me.运行图ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.股道图解ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.StatusStrip = New System.Windows.Forms.StatusStrip
        Me.staBar = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripLabelTrainNumberInfor = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolLabError = New System.Windows.Forms.ToolStripStatusLabel
        Me.ProBar = New System.Windows.Forms.ToolStripProgressBar
        Me.ToolStripLabelDate = New System.Windows.Forms.ToolStripStatusLabel
        Me.timerCurDate = New System.Timers.Timer
        Me.cmuTrainLine = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.调整发点RToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.平移列车MToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.指定平移时间FToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.鼠标平移MToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.调匀运行ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.间隔调匀ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator39 = New System.Windows.Forms.ToolStripSeparator
        Me.终到停站调匀SToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.始发停站调匀SToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.调整至最小折返时间MToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator
        Me.修改交路JToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.修改标尺EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.修改车次IToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.修改车次编号EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator37 = New System.Windows.Forms.ToolStripSeparator
        Me.车次号加一AToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.车次号减一MToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.修改列车类型LToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator
        Me.修改停站信息OToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.修改停站股道GToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.断开列车交路DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.修改车次连接EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator
        Me.编辑EToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.剪切列车XToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.复制列车CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator22 = New System.Windows.Forms.ToolStripSeparator
        Me.粘贴列车ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.删除列车DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator25 = New System.Windows.Forms.ToolStripSeparator
        Me.车底后加一车ToolStripMenuItem7 = New System.Windows.Forms.ToolStripMenuItem
        Me.车底前加一车ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.车底操作HToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.修改车底所有列车车次NToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.修改车底编号EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.合并车底HToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator23 = New System.Windows.Forms.ToolStripSeparator
        Me.删除车底所有列车DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.列车信息ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.车底信息CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.显示车底所有列车SToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SplitMainContainer = New System.Windows.Forms.SplitContainer
        Me.cmbJiShuTuJieSta = New System.Windows.Forms.ComboBox
        Me.labInfor = New System.Windows.Forms.Label
        Me.labTime = New System.Windows.Forms.Label
        Me.SplitError = New System.Windows.Forms.SplitContainer
        Me.SplitConLeftRight = New System.Windows.Forms.SplitContainer
        Me.SplitDiagram = New System.Windows.Forms.SplitContainer
        Me.PicStation2 = New System.Windows.Forms.PictureBox
        Me.PicDiagram = New System.Windows.Forms.PictureBox
        Me.picStation = New System.Windows.Forms.PictureBox
        Me.listViewTrain = New System.Windows.Forms.ListView
        Me.序号 = New System.Windows.Forms.ColumnHeader
        Me.车次 = New System.Windows.Forms.ColumnHeader
        Me.列车ID = New System.Windows.Forms.ColumnHeader
        Me.时刻 = New System.Windows.Forms.ColumnHeader
        Me.车站 = New System.Windows.Forms.ColumnHeader
        Me.出错信息 = New System.Windows.Forms.ColumnHeader
        Me.cmuDrawSingleLine = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.查找列车FToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.cmuGuDaoLine = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.修改停站股道 = New System.Windows.Forms.ToolStripMenuItem
        Me.修改停站时间TToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator42 = New System.Windows.Forms.ToolStripSeparator
        Me.整理股道EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.cmnuTrains = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.平移列车MToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator30 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem10 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator38 = New System.Windows.Forms.ToolStripSeparator
        Me.修改列车性质ZToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.调匀停站时间TToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.终到调匀EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.始发调匀SToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator35 = New System.Windows.Forms.ToolStripSeparator
        Me.剪切CToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.复制列车CToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.粘贴ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator34 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem15 = New System.Windows.Forms.ToolStripMenuItem
        Me.列车信息TToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.车底信息DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuMain.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        CType(Me.timerCurDate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmuTrainLine.SuspendLayout()
        Me.SplitMainContainer.Panel1.SuspendLayout()
        Me.SplitMainContainer.Panel2.SuspendLayout()
        Me.SplitMainContainer.SuspendLayout()
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
        Me.cmuDrawSingleLine.SuspendLayout()
        Me.cmuGuDaoLine.SuspendLayout()
        Me.cmnuTrains.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuMain
        '
        Me.MenuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.文件7FToolStripMenuItem, Me.编辑EToolStripMenuItem, Me.底图DToolStripMenuItem, Me.运行图ToolStripMenuItem, Me.技术图解JToolStripMenuItem, Me.仿真SToolStripMenuItem, Me.参数CToolStripMenuItem})
        Me.MenuMain.Location = New System.Drawing.Point(0, 0)
        Me.MenuMain.Name = "MenuMain"
        Me.MenuMain.Size = New System.Drawing.Size(886, 25)
        Me.MenuMain.TabIndex = 1
        Me.MenuMain.Text = "mnuMain"
        '
        '文件7FToolStripMenuItem
        '
        Me.文件7FToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.打开运行图ToolStripMenuItem, Me.新建运行图NToolStripMenuItem, Me.分隔一ToolStripMenuItem, Me.保存运行图SToolStripMenuItem, Me.运行图管理MToolStripMenuItem1, Me.运行图另存为图片ToolStripMenuItem, Me.ToolStripSeparator3, Me.输出OToolStripMenuItem, Me.ToolStripSeparator6, Me.编辑时刻表车站顺序EToolStripMenuItem, Me.退出EToolStripMenuItem})
        Me.文件7FToolStripMenuItem.Name = "文件7FToolStripMenuItem"
        Me.文件7FToolStripMenuItem.Size = New System.Drawing.Size(58, 21)
        Me.文件7FToolStripMenuItem.Text = "文件(&F)"
        '
        '打开运行图ToolStripMenuItem
        '
        Me.打开运行图ToolStripMenuItem.Image = CType(resources.GetObject("打开运行图ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.打开运行图ToolStripMenuItem.Name = "打开运行图ToolStripMenuItem"
        Me.打开运行图ToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.打开运行图ToolStripMenuItem.Text = "打开运行图(&O)"
        '
        '新建运行图NToolStripMenuItem
        '
        Me.新建运行图NToolStripMenuItem.Image = CType(resources.GetObject("新建运行图NToolStripMenuItem.Image"), System.Drawing.Image)
        Me.新建运行图NToolStripMenuItem.Name = "新建运行图NToolStripMenuItem"
        Me.新建运行图NToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.新建运行图NToolStripMenuItem.Text = "新建运行图(N)"
        '
        '分隔一ToolStripMenuItem
        '
        Me.分隔一ToolStripMenuItem.Name = "分隔一ToolStripMenuItem"
        Me.分隔一ToolStripMenuItem.Size = New System.Drawing.Size(196, 6)
        '
        '保存运行图SToolStripMenuItem
        '
        Me.保存运行图SToolStripMenuItem.Image = CType(resources.GetObject("保存运行图SToolStripMenuItem.Image"), System.Drawing.Image)
        Me.保存运行图SToolStripMenuItem.Name = "保存运行图SToolStripMenuItem"
        Me.保存运行图SToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.保存运行图SToolStripMenuItem.Text = "保存运行图(&S)"
        '
        '运行图管理MToolStripMenuItem1
        '
        Me.运行图管理MToolStripMenuItem1.Image = CType(resources.GetObject("运行图管理MToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.运行图管理MToolStripMenuItem1.Name = "运行图管理MToolStripMenuItem1"
        Me.运行图管理MToolStripMenuItem1.Size = New System.Drawing.Size(199, 22)
        Me.运行图管理MToolStripMenuItem1.Text = "运行图管理(&M)"
        '
        '运行图另存为图片ToolStripMenuItem
        '
        Me.运行图另存为图片ToolStripMenuItem.Name = "运行图另存为图片ToolStripMenuItem"
        Me.运行图另存为图片ToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.运行图另存为图片ToolStripMenuItem.Text = "运行图另存为图片(&I)"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(196, 6)
        '
        '输出OToolStripMenuItem
        '
        Me.输出OToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.运行图DToolStripMenuItem, Me.车底交路图CToolStripMenuItem, Me.ToolStripSeparator13, Me.列车时刻表TToolStripMenuItem, Me.车站股道技术图解JToolStripMenuItem, Me.ToolStripSeparator24, Me.运行图数据接口KToolStripMenuItem})
        Me.输出OToolStripMenuItem.Image = CType(resources.GetObject("输出OToolStripMenuItem.Image"), System.Drawing.Image)
        Me.输出OToolStripMenuItem.Name = "输出OToolStripMenuItem"
        Me.输出OToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.输出OToolStripMenuItem.Text = "输出打印(&O)"
        '
        '运行图DToolStripMenuItem
        '
        Me.运行图DToolStripMenuItem.Image = CType(resources.GetObject("运行图DToolStripMenuItem.Image"), System.Drawing.Image)
        Me.运行图DToolStripMenuItem.Name = "运行图DToolStripMenuItem"
        Me.运行图DToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
        Me.运行图DToolStripMenuItem.Text = "列车运行图(&D)"
        '
        '车底交路图CToolStripMenuItem
        '
        Me.车底交路图CToolStripMenuItem.Image = CType(resources.GetObject("车底交路图CToolStripMenuItem.Image"), System.Drawing.Image)
        Me.车底交路图CToolStripMenuItem.Name = "车底交路图CToolStripMenuItem"
        Me.车底交路图CToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
        Me.车底交路图CToolStripMenuItem.Text = "车底交路图(&C)"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(223, 6)
        '
        '列车时刻表TToolStripMenuItem
        '
        Me.列车时刻表TToolStripMenuItem.Image = CType(resources.GetObject("列车时刻表TToolStripMenuItem.Image"), System.Drawing.Image)
        Me.列车时刻表TToolStripMenuItem.Name = "列车时刻表TToolStripMenuItem"
        Me.列车时刻表TToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
        Me.列车时刻表TToolStripMenuItem.Text = "列车时刻表(&T)"
        '
        '车站股道技术图解JToolStripMenuItem
        '
        Me.车站股道技术图解JToolStripMenuItem.Image = CType(resources.GetObject("车站股道技术图解JToolStripMenuItem.Image"), System.Drawing.Image)
        Me.车站股道技术图解JToolStripMenuItem.Name = "车站股道技术图解JToolStripMenuItem"
        Me.车站股道技术图解JToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
        Me.车站股道技术图解JToolStripMenuItem.Text = "车站股道技术图解(&J)"
        '
        'ToolStripSeparator24
        '
        Me.ToolStripSeparator24.Name = "ToolStripSeparator24"
        Me.ToolStripSeparator24.Size = New System.Drawing.Size(223, 6)
        '
        '运行图数据接口KToolStripMenuItem
        '
        Me.运行图数据接口KToolStripMenuItem.Image = CType(resources.GetObject("运行图数据接口KToolStripMenuItem.Image"), System.Drawing.Image)
        Me.运行图数据接口KToolStripMenuItem.Name = "运行图数据接口KToolStripMenuItem"
        Me.运行图数据接口KToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
        Me.运行图数据接口KToolStripMenuItem.Text = "生成运行图数据XML文件(&K)"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(196, 6)
        '
        '编辑时刻表车站顺序EToolStripMenuItem
        '
        Me.编辑时刻表车站顺序EToolStripMenuItem.Image = CType(resources.GetObject("编辑时刻表车站顺序EToolStripMenuItem.Image"), System.Drawing.Image)
        Me.编辑时刻表车站顺序EToolStripMenuItem.Name = "编辑时刻表车站顺序EToolStripMenuItem"
        Me.编辑时刻表车站顺序EToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.编辑时刻表车站顺序EToolStripMenuItem.Text = "编辑时刻表车站顺序(&E)"
        '
        '退出EToolStripMenuItem
        '
        Me.退出EToolStripMenuItem.Name = "退出EToolStripMenuItem"
        Me.退出EToolStripMenuItem.Size = New System.Drawing.Size(199, 22)
        Me.退出EToolStripMenuItem.Text = "退出(&E)"
        '
        '编辑EToolStripMenuItem
        '
        Me.编辑EToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.撤销UToolStripMenuItem, Me.重复RToolStripMenuItem, Me.ToolStripSeparator14, Me.剪切CToolStripMenuItem, Me.复制CToolStripMenuItem, Me.粘贴VToolStripMenuItem, Me.删除DToolStripMenuItem, Me.ToolStripSeparator19, Me.全选AToolStripMenuItem, Me.查找FToolStripMenuItem})
        Me.编辑EToolStripMenuItem.Name = "编辑EToolStripMenuItem"
        Me.编辑EToolStripMenuItem.Size = New System.Drawing.Size(59, 21)
        Me.编辑EToolStripMenuItem.Text = "编辑(&E)"
        '
        '撤销UToolStripMenuItem
        '
        Me.撤销UToolStripMenuItem.Image = CType(resources.GetObject("撤销UToolStripMenuItem.Image"), System.Drawing.Image)
        Me.撤销UToolStripMenuItem.Name = "撤销UToolStripMenuItem"
        Me.撤销UToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.撤销UToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.撤销UToolStripMenuItem.Text = "撤销(&U)"
        '
        '重复RToolStripMenuItem
        '
        Me.重复RToolStripMenuItem.Image = CType(resources.GetObject("重复RToolStripMenuItem.Image"), System.Drawing.Image)
        Me.重复RToolStripMenuItem.Name = "重复RToolStripMenuItem"
        Me.重复RToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.R), System.Windows.Forms.Keys)
        Me.重复RToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.重复RToolStripMenuItem.Text = "重复(&R)"
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(159, 6)
        '
        '剪切CToolStripMenuItem
        '
        Me.剪切CToolStripMenuItem.Image = CType(resources.GetObject("剪切CToolStripMenuItem.Image"), System.Drawing.Image)
        Me.剪切CToolStripMenuItem.Name = "剪切CToolStripMenuItem"
        Me.剪切CToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.剪切CToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.剪切CToolStripMenuItem.Text = "剪切(&X)"
        '
        '复制CToolStripMenuItem
        '
        Me.复制CToolStripMenuItem.Image = CType(resources.GetObject("复制CToolStripMenuItem.Image"), System.Drawing.Image)
        Me.复制CToolStripMenuItem.Name = "复制CToolStripMenuItem"
        Me.复制CToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.复制CToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.复制CToolStripMenuItem.Text = "复制(&C)"
        '
        '粘贴VToolStripMenuItem
        '
        Me.粘贴VToolStripMenuItem.Image = CType(resources.GetObject("粘贴VToolStripMenuItem.Image"), System.Drawing.Image)
        Me.粘贴VToolStripMenuItem.Name = "粘贴VToolStripMenuItem"
        Me.粘贴VToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.粘贴VToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.粘贴VToolStripMenuItem.Text = "粘贴(V)"
        '
        '删除DToolStripMenuItem
        '
        Me.删除DToolStripMenuItem.Image = CType(resources.GetObject("删除DToolStripMenuItem.Image"), System.Drawing.Image)
        Me.删除DToolStripMenuItem.Name = "删除DToolStripMenuItem"
        Me.删除DToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.删除DToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.删除DToolStripMenuItem.Text = "删除(&D)"
        '
        'ToolStripSeparator19
        '
        Me.ToolStripSeparator19.Name = "ToolStripSeparator19"
        Me.ToolStripSeparator19.Size = New System.Drawing.Size(159, 6)
        '
        '全选AToolStripMenuItem
        '
        Me.全选AToolStripMenuItem.Name = "全选AToolStripMenuItem"
        Me.全选AToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.全选AToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.全选AToolStripMenuItem.Text = "全选(&A)"
        '
        '查找FToolStripMenuItem
        '
        Me.查找FToolStripMenuItem.Image = CType(resources.GetObject("查找FToolStripMenuItem.Image"), System.Drawing.Image)
        Me.查找FToolStripMenuItem.Name = "查找FToolStripMenuItem"
        Me.查找FToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.F), System.Windows.Forms.Keys)
        Me.查找FToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.查找FToolStripMenuItem.Text = "查找(&F)"
        '
        '底图DToolStripMenuItem
        '
        Me.底图DToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.一分格ToolStripMenuItem, Me.二分格ToolStripMenuItem, Me.分隔线二ToolStripMenuItem, Me.十分格ToolStripMenuItem, Me.小时格ToolStripMenuItem, Me.ToolStripSeparator5, Me.向左翻页ToolStripMenuItem, Me.向右翻页ToolStripMenuItem, Me.ToolStripSeparator40, Me.放大底图宽度ToolStripMenuItem, Me.缩小底图宽度ToolStripMenuItem, Me.纵向放大底图ZToolStripMenuItem, Me.纵向缩小底图XToolStripMenuItem, Me.ToolStripSeparator21, Me.底图线型与颜色CToolStripMenuItem})
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
        '分隔线二ToolStripMenuItem
        '
        Me.分隔线二ToolStripMenuItem.Name = "分隔线二ToolStripMenuItem"
        Me.分隔线二ToolStripMenuItem.Size = New System.Drawing.Size(205, 6)
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
        '向左翻页ToolStripMenuItem
        '
        Me.向左翻页ToolStripMenuItem.Name = "向左翻页ToolStripMenuItem"
        Me.向左翻页ToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Left), System.Windows.Forms.Keys)
        Me.向左翻页ToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.向左翻页ToolStripMenuItem.Text = "向左翻页(&L)"
        '
        '向右翻页ToolStripMenuItem
        '
        Me.向右翻页ToolStripMenuItem.Name = "向右翻页ToolStripMenuItem"
        Me.向右翻页ToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Right), System.Windows.Forms.Keys)
        Me.向右翻页ToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.向右翻页ToolStripMenuItem.Text = "向右翻页(&R)"
        '
        'ToolStripSeparator40
        '
        Me.ToolStripSeparator40.Name = "ToolStripSeparator40"
        Me.ToolStripSeparator40.Size = New System.Drawing.Size(205, 6)
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
        '
        '底图线型与颜色CToolStripMenuItem
        '
        Me.底图线型与颜色CToolStripMenuItem.Image = CType(resources.GetObject("底图线型与颜色CToolStripMenuItem.Image"), System.Drawing.Image)
        Me.底图线型与颜色CToolStripMenuItem.Name = "底图线型与颜色CToolStripMenuItem"
        Me.底图线型与颜色CToolStripMenuItem.Size = New System.Drawing.Size(208, 22)
        Me.底图线型与颜色CToolStripMenuItem.Text = "颜色与字体(&C)"
        '
        '运行图ToolStripMenuItem
        '
        Me.运行图ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.刷新运行图ToolStripMenuItem, Me.ToolStripSeparator2, Me.查看列车时刻表TToolStripMenuItem, Me.增加列车AToolStripMenuItem, Me.ToolStripSeparator9, Me.运行图编制ToolStripMenuItem, Me.车底组铺画DToolStripMenuItem, Me.ToolStripSeparator15, Me.列车重编车次BToolStripMenuItem, Me.整理车站股道ToolStripMenuItem, Me.整理列车ToolStripMenuItem, Me.检查列车运行图KToolStripMenuItem, Me.ToolStripSeparator20, Me.计算指标XToolStripMenuItem, Me.线型与颜色LToolStripMenuItem, Me.ToolStripSeparator16, Me.全部列车重新铺画AToolStripMenuItem, Me.批处理组操作UToolStripMenuItem, Me.能力计算CToolStripMenuItem, Me.错误提示ToolStripMenuItem})
        Me.运行图ToolStripMenuItem.Name = "运行图ToolStripMenuItem"
        Me.运行图ToolStripMenuItem.Size = New System.Drawing.Size(71, 21)
        Me.运行图ToolStripMenuItem.Text = "运行图(&T)"
        '
        '刷新运行图ToolStripMenuItem
        '
        Me.刷新运行图ToolStripMenuItem.Image = CType(resources.GetObject("刷新运行图ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.刷新运行图ToolStripMenuItem.Name = "刷新运行图ToolStripMenuItem"
        Me.刷新运行图ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.刷新运行图ToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.刷新运行图ToolStripMenuItem.Text = "刷新运行图(&R)"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(185, 6)
        '
        '查看列车时刻表TToolStripMenuItem
        '
        Me.查看列车时刻表TToolStripMenuItem.Image = CType(resources.GetObject("查看列车时刻表TToolStripMenuItem.Image"), System.Drawing.Image)
        Me.查看列车时刻表TToolStripMenuItem.Name = "查看列车时刻表TToolStripMenuItem"
        Me.查看列车时刻表TToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.查看列车时刻表TToolStripMenuItem.Text = "查看列车时刻表(&T)"
        '
        '增加列车AToolStripMenuItem
        '
        Me.增加列车AToolStripMenuItem.Image = CType(resources.GetObject("增加列车AToolStripMenuItem.Image"), System.Drawing.Image)
        Me.增加列车AToolStripMenuItem.Name = "增加列车AToolStripMenuItem"
        Me.增加列车AToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.增加列车AToolStripMenuItem.Text = "增加列车(&A)"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(185, 6)
        '
        '运行图编制ToolStripMenuItem
        '
        Me.运行图编制ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.单一或大小及共线交路ToolStripMenuItem, Me.环形交路ToolStripMenuItem})
        Me.运行图编制ToolStripMenuItem.Name = "运行图编制ToolStripMenuItem"
        Me.运行图编制ToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.运行图编制ToolStripMenuItem.Text = "运行图编制(&E)"
        '
        '单一或大小及共线交路ToolStripMenuItem
        '
        Me.单一或大小及共线交路ToolStripMenuItem.Name = "单一或大小及共线交路ToolStripMenuItem"
        Me.单一或大小及共线交路ToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.单一或大小及共线交路ToolStripMenuItem.Text = "单一／大小／共线交路"
        '
        '环形交路ToolStripMenuItem
        '
        Me.环形交路ToolStripMenuItem.Name = "环形交路ToolStripMenuItem"
        Me.环形交路ToolStripMenuItem.Size = New System.Drawing.Size(196, 22)
        Me.环形交路ToolStripMenuItem.Text = "环形交路"
        '
        '车底组铺画DToolStripMenuItem
        '
        Me.车底组铺画DToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.初始车底组铺画ToolStripMenuItem, Me.继续ToolStripMenuItem, Me.一次性铺画方案OToolStripMenuItem, Me.ToolStripSeparator33, Me.铺画间隔设置ToolStripMenuItem})
        Me.车底组铺画DToolStripMenuItem.Name = "车底组铺画DToolStripMenuItem"
        Me.车底组铺画DToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.车底组铺画DToolStripMenuItem.Text = "车底组铺画(&D)"
        Me.车底组铺画DToolStripMenuItem.Visible = False
        '
        '初始车底组铺画ToolStripMenuItem
        '
        Me.初始车底组铺画ToolStripMenuItem.Name = "初始车底组铺画ToolStripMenuItem"
        Me.初始车底组铺画ToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.初始车底组铺画ToolStripMenuItem.Text = "初始车底组铺画(&F)"
        '
        '继续ToolStripMenuItem
        '
        Me.继续ToolStripMenuItem.Name = "继续ToolStripMenuItem"
        Me.继续ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9
        Me.继续ToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.继续ToolStripMenuItem.Text = "接续铺画"
        '
        '一次性铺画方案OToolStripMenuItem
        '
        Me.一次性铺画方案OToolStripMenuItem.Name = "一次性铺画方案OToolStripMenuItem"
        Me.一次性铺画方案OToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.一次性铺画方案OToolStripMenuItem.Text = "一次性铺画方案(&O)"
        Me.一次性铺画方案OToolStripMenuItem.Visible = False
        '
        'ToolStripSeparator33
        '
        Me.ToolStripSeparator33.Name = "ToolStripSeparator33"
        Me.ToolStripSeparator33.Size = New System.Drawing.Size(175, 6)
        '
        '铺画间隔设置ToolStripMenuItem
        '
        Me.铺画间隔设置ToolStripMenuItem.Name = "铺画间隔设置ToolStripMenuItem"
        Me.铺画间隔设置ToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.铺画间隔设置ToolStripMenuItem.Text = "铺画间隔设置(&I)"
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(185, 6)
        '
        '列车重编车次BToolStripMenuItem
        '
        Me.列车重编车次BToolStripMenuItem.Image = CType(resources.GetObject("列车重编车次BToolStripMenuItem.Image"), System.Drawing.Image)
        Me.列车重编车次BToolStripMenuItem.Name = "列车重编车次BToolStripMenuItem"
        Me.列车重编车次BToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.列车重编车次BToolStripMenuItem.Text = "列车重编车次(&B)"
        '
        '整理车站股道ToolStripMenuItem
        '
        Me.整理车站股道ToolStripMenuItem.Name = "整理车站股道ToolStripMenuItem"
        Me.整理车站股道ToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.整理车站股道ToolStripMenuItem.Text = "整理车站股道(&Z)"
        '
        '整理列车ToolStripMenuItem
        '
        Me.整理列车ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.整理到发时刻DToolStripMenuItem, Me.整理折返时刻ZToolStripMenuItem})
        Me.整理列车ToolStripMenuItem.Image = CType(resources.GetObject("整理列车ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.整理列车ToolStripMenuItem.Name = "整理列车ToolStripMenuItem"
        Me.整理列车ToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.整理列车ToolStripMenuItem.Text = "整理时刻(&L)"
        '
        '整理到发时刻DToolStripMenuItem
        '
        Me.整理到发时刻DToolStripMenuItem.Name = "整理到发时刻DToolStripMenuItem"
        Me.整理到发时刻DToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.整理到发时刻DToolStripMenuItem.Text = "整理到发时刻(&D)"
        '
        '整理折返时刻ZToolStripMenuItem
        '
        Me.整理折返时刻ZToolStripMenuItem.Name = "整理折返时刻ZToolStripMenuItem"
        Me.整理折返时刻ZToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.整理折返时刻ZToolStripMenuItem.Text = "整理折返时刻(&Z)"
        '
        '检查列车运行图KToolStripMenuItem
        '
        Me.检查列车运行图KToolStripMenuItem.Image = CType(resources.GetObject("检查列车运行图KToolStripMenuItem.Image"), System.Drawing.Image)
        Me.检查列车运行图KToolStripMenuItem.Name = "检查列车运行图KToolStripMenuItem"
        Me.检查列车运行图KToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.检查列车运行图KToolStripMenuItem.Text = "检查列车运行图(&K)"
        '
        'ToolStripSeparator20
        '
        Me.ToolStripSeparator20.Name = "ToolStripSeparator20"
        Me.ToolStripSeparator20.Size = New System.Drawing.Size(185, 6)
        '
        '计算指标XToolStripMenuItem
        '
        Me.计算指标XToolStripMenuItem.Image = CType(resources.GetObject("计算指标XToolStripMenuItem.Image"), System.Drawing.Image)
        Me.计算指标XToolStripMenuItem.Name = "计算指标XToolStripMenuItem"
        Me.计算指标XToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.计算指标XToolStripMenuItem.Text = "计算指标(&X)"
        '
        '线型与颜色LToolStripMenuItem
        '
        Me.线型与颜色LToolStripMenuItem.Image = CType(resources.GetObject("线型与颜色LToolStripMenuItem.Image"), System.Drawing.Image)
        Me.线型与颜色LToolStripMenuItem.Name = "线型与颜色LToolStripMenuItem"
        Me.线型与颜色LToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.线型与颜色LToolStripMenuItem.Text = "颜色与字体(&L)"
        '
        'ToolStripSeparator16
        '
        Me.ToolStripSeparator16.Name = "ToolStripSeparator16"
        Me.ToolStripSeparator16.Size = New System.Drawing.Size(185, 6)
        '
        '全部列车重新铺画AToolStripMenuItem
        '
        Me.全部列车重新铺画AToolStripMenuItem.Name = "全部列车重新铺画AToolStripMenuItem"
        Me.全部列车重新铺画AToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.全部列车重新铺画AToolStripMenuItem.Text = "全部列车重新铺画(&A)"
        '
        '批处理组操作UToolStripMenuItem
        '
        Me.批处理组操作UToolStripMenuItem.Image = CType(resources.GetObject("批处理组操作UToolStripMenuItem.Image"), System.Drawing.Image)
        Me.批处理组操作UToolStripMenuItem.Name = "批处理组操作UToolStripMenuItem"
        Me.批处理组操作UToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.批处理组操作UToolStripMenuItem.Text = "批处理组操作(&U)"
        '
        '能力计算CToolStripMenuItem
        '
        Me.能力计算CToolStripMenuItem.Name = "能力计算CToolStripMenuItem"
        Me.能力计算CToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.能力计算CToolStripMenuItem.Text = "能力计算(&C)"
        Me.能力计算CToolStripMenuItem.Visible = False
        '
        '错误提示ToolStripMenuItem
        '
        Me.错误提示ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.全部显示ToolStripMenuItem, Me.只显示前20条ToolStripMenuItem})
        Me.错误提示ToolStripMenuItem.Name = "错误提示ToolStripMenuItem"
        Me.错误提示ToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.错误提示ToolStripMenuItem.Text = "错误提示"
        '
        '全部显示ToolStripMenuItem
        '
        Me.全部显示ToolStripMenuItem.Checked = True
        Me.全部显示ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.全部显示ToolStripMenuItem.Name = "全部显示ToolStripMenuItem"
        Me.全部显示ToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.全部显示ToolStripMenuItem.Text = "全部显示"
        '
        '只显示前20条ToolStripMenuItem
        '
        Me.只显示前20条ToolStripMenuItem.Name = "只显示前20条ToolStripMenuItem"
        Me.只显示前20条ToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.只显示前20条ToolStripMenuItem.Text = "只显示前20条"
        '
        '技术图解JToolStripMenuItem
        '
        Me.技术图解JToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.车站股道使用ToolStripMenuItem})
        Me.技术图解JToolStripMenuItem.Name = "技术图解JToolStripMenuItem"
        Me.技术图解JToolStripMenuItem.Size = New System.Drawing.Size(81, 21)
        Me.技术图解JToolStripMenuItem.Text = "技术图解(&J)"
        '
        '车站股道使用ToolStripMenuItem
        '
        Me.车站股道使用ToolStripMenuItem.Image = CType(resources.GetObject("车站股道使用ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.车站股道使用ToolStripMenuItem.Name = "车站股道使用ToolStripMenuItem"
        Me.车站股道使用ToolStripMenuItem.Size = New System.Drawing.Size(189, 22)
        Me.车站股道使用ToolStripMenuItem.Text = "车站股道使用图解(&G)"
        '
        '仿真SToolStripMenuItem
        '
        Me.仿真SToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.基于运行图仿真ToolStripMenuItem, Me.线路通过能力仿真TToolStripMenuItem, Me.ToolStripSeparator31, Me.折返仿真RToolStripMenuItem, Me.牵引曲线QToolStripMenuItem})
        Me.仿真SToolStripMenuItem.Name = "仿真SToolStripMenuItem"
        Me.仿真SToolStripMenuItem.Size = New System.Drawing.Size(59, 21)
        Me.仿真SToolStripMenuItem.Text = "仿真(&S)"
        Me.仿真SToolStripMenuItem.Visible = False
        '
        '基于运行图仿真ToolStripMenuItem
        '
        Me.基于运行图仿真ToolStripMenuItem.Name = "基于运行图仿真ToolStripMenuItem"
        Me.基于运行图仿真ToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.基于运行图仿真ToolStripMenuItem.Text = "基于运行图仿真(&D)"
        '
        '线路通过能力仿真TToolStripMenuItem
        '
        Me.线路通过能力仿真TToolStripMenuItem.Name = "线路通过能力仿真TToolStripMenuItem"
        Me.线路通过能力仿真TToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.线路通过能力仿真TToolStripMenuItem.Text = "线路通过能力仿真(&T)"
        '
        'ToolStripSeparator31
        '
        Me.ToolStripSeparator31.Name = "ToolStripSeparator31"
        Me.ToolStripSeparator31.Size = New System.Drawing.Size(185, 6)
        '
        '折返仿真RToolStripMenuItem
        '
        Me.折返仿真RToolStripMenuItem.Name = "折返仿真RToolStripMenuItem"
        Me.折返仿真RToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.折返仿真RToolStripMenuItem.Text = "车站折返能力仿真(&R)"
        '
        '牵引曲线QToolStripMenuItem
        '
        Me.牵引曲线QToolStripMenuItem.Name = "牵引曲线QToolStripMenuItem"
        Me.牵引曲线QToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.牵引曲线QToolStripMenuItem.Text = "牵引曲线(&Q)"
        '
        '参数CToolStripMenuItem
        '
        Me.参数CToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.约束铺画YToolStripMenuItem, Me.铺画方式ToolStripMenuItem, Me.ToolStripSeparator32, Me.自动检查错误EToolStripMenuItem, Me.自动重编车次ZToolStripMenuItem, Me.ToolStripSeparator28, Me.系统设置SToolStripMenuItem})
        Me.参数CToolStripMenuItem.Name = "参数CToolStripMenuItem"
        Me.参数CToolStripMenuItem.Size = New System.Drawing.Size(60, 21)
        Me.参数CToolStripMenuItem.Text = "参数(&C)"
        '
        '约束铺画YToolStripMenuItem
        '
        Me.约束铺画YToolStripMenuItem.CheckOnClick = True
        Me.约束铺画YToolStripMenuItem.Name = "约束铺画YToolStripMenuItem"
        Me.约束铺画YToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.约束铺画YToolStripMenuItem.Text = "约束铺画(Y)"
        '
        '铺画方式ToolStripMenuItem
        '
        Me.铺画方式ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.不越行ToolStripMenuItem, Me.可以越行ToolStripMenuItem, Me.ToolStripSeparator36, Me.调整时移动提示ToolStripMenuItem})
        Me.铺画方式ToolStripMenuItem.Name = "铺画方式ToolStripMenuItem"
        Me.铺画方式ToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.铺画方式ToolStripMenuItem.Text = "铺画参数"
        '
        '不越行ToolStripMenuItem
        '
        Me.不越行ToolStripMenuItem.Checked = True
        Me.不越行ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.不越行ToolStripMenuItem.Name = "不越行ToolStripMenuItem"
        Me.不越行ToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.不越行ToolStripMenuItem.Text = "不考虑越行"
        '
        '可以越行ToolStripMenuItem
        '
        Me.可以越行ToolStripMenuItem.Name = "可以越行ToolStripMenuItem"
        Me.可以越行ToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.可以越行ToolStripMenuItem.Text = "可以越行"
        '
        'ToolStripSeparator36
        '
        Me.ToolStripSeparator36.Name = "ToolStripSeparator36"
        Me.ToolStripSeparator36.Size = New System.Drawing.Size(157, 6)
        '
        '调整时移动提示ToolStripMenuItem
        '
        Me.调整时移动提示ToolStripMenuItem.Checked = True
        Me.调整时移动提示ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.调整时移动提示ToolStripMenuItem.Name = "调整时移动提示ToolStripMenuItem"
        Me.调整时移动提示ToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.调整时移动提示ToolStripMenuItem.Text = "调整时移动提示"
        '
        'ToolStripSeparator32
        '
        Me.ToolStripSeparator32.Name = "ToolStripSeparator32"
        Me.ToolStripSeparator32.Size = New System.Drawing.Size(160, 6)
        Me.ToolStripSeparator32.Visible = False
        '
        '自动检查错误EToolStripMenuItem
        '
        Me.自动检查错误EToolStripMenuItem.Checked = True
        Me.自动检查错误EToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.自动检查错误EToolStripMenuItem.Name = "自动检查错误EToolStripMenuItem"
        Me.自动检查错误EToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.自动检查错误EToolStripMenuItem.Text = "自动检查错误(&E)"
        '
        '自动重编车次ZToolStripMenuItem
        '
        Me.自动重编车次ZToolStripMenuItem.CheckOnClick = True
        Me.自动重编车次ZToolStripMenuItem.Name = "自动重编车次ZToolStripMenuItem"
        Me.自动重编车次ZToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.自动重编车次ZToolStripMenuItem.Text = "自动重编车次(&Z)"
        Me.自动重编车次ZToolStripMenuItem.Visible = False
        '
        'ToolStripSeparator28
        '
        Me.ToolStripSeparator28.Name = "ToolStripSeparator28"
        Me.ToolStripSeparator28.Size = New System.Drawing.Size(160, 6)
        Me.ToolStripSeparator28.Visible = False
        '
        '系统设置SToolStripMenuItem
        '
        Me.系统设置SToolStripMenuItem.Image = CType(resources.GetObject("系统设置SToolStripMenuItem.Image"), System.Drawing.Image)
        Me.系统设置SToolStripMenuItem.Name = "系统设置SToolStripMenuItem"
        Me.系统设置SToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.系统设置SToolStripMenuItem.Text = "系统设置(&S)"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.打开ToolStripButton1, Me.新建ToolStripButton5, Me.保存ToolStripButton7, Me.打印运行图ToolStripButton1, Me.ToolStripSeparator29, Me.运行图管理ToolStripButton1, Me.ToolStripSeparator26, Me.剪切ToolStripButton10, Me.粘贴ToolStripButton13, Me.复制ToolStripButton9, Me.删除ToolStripButton11, Me.查找ToolStripButton12, Me.ToolStripSeparator27, Me.撤销tolStripUndo, Me.重复tolStripRedo, Me.ToolStripSeparator17, Me.时间格式ToolStripButton, Me.ToolStripLeft, Me.ToolStripRight, Me.ToolStripSeparator41, Me.底图放大ToolStripButton, Me.底图缩小ToolStripButton, Me.纵向放大ToolStripButton6, Me.纵向缩小ToolStripButton8, Me.ToolStripSeparator4, Me.线型与颜色ToolStripButton1, Me.检查错误ToolStripButton4, Me.测量时间ToolStripButton9, Me.多选ToolStripButton1, Me.ToolStripSeparator18, Me.运行图ToolStripButton3, Me.股道图解ToolStripButton2})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 25)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(886, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        '打开ToolStripButton1
        '
        Me.打开ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.打开ToolStripButton1.Image = CType(resources.GetObject("打开ToolStripButton1.Image"), System.Drawing.Image)
        Me.打开ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.打开ToolStripButton1.Name = "打开ToolStripButton1"
        Me.打开ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.打开ToolStripButton1.Text = "打开"
        '
        '新建ToolStripButton5
        '
        Me.新建ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.新建ToolStripButton5.Image = CType(resources.GetObject("新建ToolStripButton5.Image"), System.Drawing.Image)
        Me.新建ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.新建ToolStripButton5.Name = "新建ToolStripButton5"
        Me.新建ToolStripButton5.Size = New System.Drawing.Size(23, 22)
        Me.新建ToolStripButton5.Text = "新建"
        '
        '保存ToolStripButton7
        '
        Me.保存ToolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.保存ToolStripButton7.Image = CType(resources.GetObject("保存ToolStripButton7.Image"), System.Drawing.Image)
        Me.保存ToolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.保存ToolStripButton7.Name = "保存ToolStripButton7"
        Me.保存ToolStripButton7.Size = New System.Drawing.Size(23, 22)
        Me.保存ToolStripButton7.Text = "保存"
        '
        '打印运行图ToolStripButton1
        '
        Me.打印运行图ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.打印运行图ToolStripButton1.Image = CType(resources.GetObject("打印运行图ToolStripButton1.Image"), System.Drawing.Image)
        Me.打印运行图ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.打印运行图ToolStripButton1.Name = "打印运行图ToolStripButton1"
        Me.打印运行图ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.打印运行图ToolStripButton1.Text = "打印运行图"
        '
        'ToolStripSeparator29
        '
        Me.ToolStripSeparator29.Name = "ToolStripSeparator29"
        Me.ToolStripSeparator29.Size = New System.Drawing.Size(6, 25)
        '
        '运行图管理ToolStripButton1
        '
        Me.运行图管理ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.运行图管理ToolStripButton1.Image = CType(resources.GetObject("运行图管理ToolStripButton1.Image"), System.Drawing.Image)
        Me.运行图管理ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.运行图管理ToolStripButton1.Name = "运行图管理ToolStripButton1"
        Me.运行图管理ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.运行图管理ToolStripButton1.Text = "运行图管理"
        Me.运行图管理ToolStripButton1.ToolTipText = "运行图管理"
        '
        'ToolStripSeparator26
        '
        Me.ToolStripSeparator26.Name = "ToolStripSeparator26"
        Me.ToolStripSeparator26.Size = New System.Drawing.Size(6, 25)
        '
        '剪切ToolStripButton10
        '
        Me.剪切ToolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.剪切ToolStripButton10.Image = CType(resources.GetObject("剪切ToolStripButton10.Image"), System.Drawing.Image)
        Me.剪切ToolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.剪切ToolStripButton10.Name = "剪切ToolStripButton10"
        Me.剪切ToolStripButton10.Size = New System.Drawing.Size(23, 22)
        Me.剪切ToolStripButton10.Text = "剪切"
        Me.剪切ToolStripButton10.ToolTipText = "剪切"
        '
        '粘贴ToolStripButton13
        '
        Me.粘贴ToolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.粘贴ToolStripButton13.Image = CType(resources.GetObject("粘贴ToolStripButton13.Image"), System.Drawing.Image)
        Me.粘贴ToolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.粘贴ToolStripButton13.Name = "粘贴ToolStripButton13"
        Me.粘贴ToolStripButton13.Size = New System.Drawing.Size(23, 22)
        Me.粘贴ToolStripButton13.Text = "粘贴"
        '
        '复制ToolStripButton9
        '
        Me.复制ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.复制ToolStripButton9.Image = CType(resources.GetObject("复制ToolStripButton9.Image"), System.Drawing.Image)
        Me.复制ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.复制ToolStripButton9.Name = "复制ToolStripButton9"
        Me.复制ToolStripButton9.Size = New System.Drawing.Size(23, 22)
        Me.复制ToolStripButton9.Text = "复制"
        Me.复制ToolStripButton9.ToolTipText = "复制"
        '
        '删除ToolStripButton11
        '
        Me.删除ToolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.删除ToolStripButton11.Image = CType(resources.GetObject("删除ToolStripButton11.Image"), System.Drawing.Image)
        Me.删除ToolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.删除ToolStripButton11.Name = "删除ToolStripButton11"
        Me.删除ToolStripButton11.Size = New System.Drawing.Size(23, 22)
        Me.删除ToolStripButton11.Text = "删除"
        Me.删除ToolStripButton11.ToolTipText = "删除"
        '
        '查找ToolStripButton12
        '
        Me.查找ToolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.查找ToolStripButton12.Image = CType(resources.GetObject("查找ToolStripButton12.Image"), System.Drawing.Image)
        Me.查找ToolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.查找ToolStripButton12.Name = "查找ToolStripButton12"
        Me.查找ToolStripButton12.Size = New System.Drawing.Size(23, 22)
        Me.查找ToolStripButton12.Text = "查找"
        '
        'ToolStripSeparator27
        '
        Me.ToolStripSeparator27.Name = "ToolStripSeparator27"
        Me.ToolStripSeparator27.Size = New System.Drawing.Size(6, 25)
        '
        '撤销tolStripUndo
        '
        Me.撤销tolStripUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.撤销tolStripUndo.Image = CType(resources.GetObject("撤销tolStripUndo.Image"), System.Drawing.Image)
        Me.撤销tolStripUndo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.撤销tolStripUndo.Name = "撤销tolStripUndo"
        Me.撤销tolStripUndo.Size = New System.Drawing.Size(23, 22)
        Me.撤销tolStripUndo.Text = "撤销"
        '
        '重复tolStripRedo
        '
        Me.重复tolStripRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.重复tolStripRedo.Image = CType(resources.GetObject("重复tolStripRedo.Image"), System.Drawing.Image)
        Me.重复tolStripRedo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.重复tolStripRedo.Name = "重复tolStripRedo"
        Me.重复tolStripRedo.Size = New System.Drawing.Size(23, 22)
        Me.重复tolStripRedo.Text = "重复"
        '
        'ToolStripSeparator17
        '
        Me.ToolStripSeparator17.Name = "ToolStripSeparator17"
        Me.ToolStripSeparator17.Size = New System.Drawing.Size(6, 25)
        '
        '时间格式ToolStripButton
        '
        Me.时间格式ToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.时间格式ToolStripButton.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.一分格ToolStripMenuItem1, Me.二分格ToolStripMenuItem1, Me.ToolStripSeparator1, Me.十分格ToolStripMenuItem1, Me.小时格ToolStripMenuItem1})
        Me.时间格式ToolStripButton.Image = CType(resources.GetObject("时间格式ToolStripButton.Image"), System.Drawing.Image)
        Me.时间格式ToolStripButton.Name = "时间格式ToolStripButton"
        Me.时间格式ToolStripButton.Size = New System.Drawing.Size(29, 22)
        Me.时间格式ToolStripButton.Text = "时分格"
        '
        '一分格ToolStripMenuItem1
        '
        Me.一分格ToolStripMenuItem1.Image = CType(resources.GetObject("一分格ToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.一分格ToolStripMenuItem1.Name = "一分格ToolStripMenuItem1"
        Me.一分格ToolStripMenuItem1.Size = New System.Drawing.Size(112, 22)
        Me.一分格ToolStripMenuItem1.Text = "一分格"
        '
        '二分格ToolStripMenuItem1
        '
        Me.二分格ToolStripMenuItem1.Image = CType(resources.GetObject("二分格ToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.二分格ToolStripMenuItem1.Name = "二分格ToolStripMenuItem1"
        Me.二分格ToolStripMenuItem1.Size = New System.Drawing.Size(112, 22)
        Me.二分格ToolStripMenuItem1.Text = "二分格"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(109, 6)
        '
        '十分格ToolStripMenuItem1
        '
        Me.十分格ToolStripMenuItem1.Image = CType(resources.GetObject("十分格ToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.十分格ToolStripMenuItem1.Name = "十分格ToolStripMenuItem1"
        Me.十分格ToolStripMenuItem1.Size = New System.Drawing.Size(112, 22)
        Me.十分格ToolStripMenuItem1.Text = "十分格"
        '
        '小时格ToolStripMenuItem1
        '
        Me.小时格ToolStripMenuItem1.Image = CType(resources.GetObject("小时格ToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.小时格ToolStripMenuItem1.Name = "小时格ToolStripMenuItem1"
        Me.小时格ToolStripMenuItem1.Size = New System.Drawing.Size(112, 22)
        Me.小时格ToolStripMenuItem1.Text = "小时格"
        '
        'ToolStripLeft
        '
        Me.ToolStripLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripLeft.Image = CType(resources.GetObject("ToolStripLeft.Image"), System.Drawing.Image)
        Me.ToolStripLeft.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripLeft.Name = "ToolStripLeft"
        Me.ToolStripLeft.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripLeft.Text = "向左翻页"
        '
        'ToolStripRight
        '
        Me.ToolStripRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripRight.Image = CType(resources.GetObject("ToolStripRight.Image"), System.Drawing.Image)
        Me.ToolStripRight.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripRight.Name = "ToolStripRight"
        Me.ToolStripRight.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripRight.Text = "向右翻页"
        '
        'ToolStripSeparator41
        '
        Me.ToolStripSeparator41.Name = "ToolStripSeparator41"
        Me.ToolStripSeparator41.Size = New System.Drawing.Size(6, 25)
        '
        '底图放大ToolStripButton
        '
        Me.底图放大ToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.底图放大ToolStripButton.Image = CType(resources.GetObject("底图放大ToolStripButton.Image"), System.Drawing.Image)
        Me.底图放大ToolStripButton.Name = "底图放大ToolStripButton"
        Me.底图放大ToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.底图放大ToolStripButton.Text = "横向放大"
        '
        '底图缩小ToolStripButton
        '
        Me.底图缩小ToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.底图缩小ToolStripButton.Image = CType(resources.GetObject("底图缩小ToolStripButton.Image"), System.Drawing.Image)
        Me.底图缩小ToolStripButton.Name = "底图缩小ToolStripButton"
        Me.底图缩小ToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.底图缩小ToolStripButton.Text = "横向缩小"
        '
        '纵向放大ToolStripButton6
        '
        Me.纵向放大ToolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.纵向放大ToolStripButton6.Image = CType(resources.GetObject("纵向放大ToolStripButton6.Image"), System.Drawing.Image)
        Me.纵向放大ToolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.纵向放大ToolStripButton6.Name = "纵向放大ToolStripButton6"
        Me.纵向放大ToolStripButton6.Size = New System.Drawing.Size(23, 22)
        Me.纵向放大ToolStripButton6.Text = "纵向放大"
        '
        '纵向缩小ToolStripButton8
        '
        Me.纵向缩小ToolStripButton8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.纵向缩小ToolStripButton8.Image = CType(resources.GetObject("纵向缩小ToolStripButton8.Image"), System.Drawing.Image)
        Me.纵向缩小ToolStripButton8.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.纵向缩小ToolStripButton8.Name = "纵向缩小ToolStripButton8"
        Me.纵向缩小ToolStripButton8.Size = New System.Drawing.Size(23, 22)
        Me.纵向缩小ToolStripButton8.Text = "纵向缩小"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        '线型与颜色ToolStripButton1
        '
        Me.线型与颜色ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.线型与颜色ToolStripButton1.Image = CType(resources.GetObject("线型与颜色ToolStripButton1.Image"), System.Drawing.Image)
        Me.线型与颜色ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.线型与颜色ToolStripButton1.Name = "线型与颜色ToolStripButton1"
        Me.线型与颜色ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.线型与颜色ToolStripButton1.Text = "颜色与字体"
        '
        '检查错误ToolStripButton4
        '
        Me.检查错误ToolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.检查错误ToolStripButton4.Image = CType(resources.GetObject("检查错误ToolStripButton4.Image"), System.Drawing.Image)
        Me.检查错误ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.检查错误ToolStripButton4.Name = "检查错误ToolStripButton4"
        Me.检查错误ToolStripButton4.Size = New System.Drawing.Size(23, 22)
        Me.检查错误ToolStripButton4.Text = "检查错误"
        '
        '测量时间ToolStripButton9
        '
        Me.测量时间ToolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.测量时间ToolStripButton9.Image = CType(resources.GetObject("测量时间ToolStripButton9.Image"), System.Drawing.Image)
        Me.测量时间ToolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.测量时间ToolStripButton9.Name = "测量时间ToolStripButton9"
        Me.测量时间ToolStripButton9.Size = New System.Drawing.Size(23, 22)
        Me.测量时间ToolStripButton9.Text = "测量时间"
        '
        '多选ToolStripButton1
        '
        Me.多选ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.多选ToolStripButton1.Image = CType(resources.GetObject("多选ToolStripButton1.Image"), System.Drawing.Image)
        Me.多选ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.多选ToolStripButton1.Name = "多选ToolStripButton1"
        Me.多选ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.多选ToolStripButton1.Text = "多选"
        '
        'ToolStripSeparator18
        '
        Me.ToolStripSeparator18.Name = "ToolStripSeparator18"
        Me.ToolStripSeparator18.Size = New System.Drawing.Size(6, 25)
        '
        '运行图ToolStripButton3
        '
        Me.运行图ToolStripButton3.Image = CType(resources.GetObject("运行图ToolStripButton3.Image"), System.Drawing.Image)
        Me.运行图ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.运行图ToolStripButton3.Name = "运行图ToolStripButton3"
        Me.运行图ToolStripButton3.Size = New System.Drawing.Size(64, 22)
        Me.运行图ToolStripButton3.Text = "运行图"
        '
        '股道图解ToolStripButton2
        '
        Me.股道图解ToolStripButton2.Image = CType(resources.GetObject("股道图解ToolStripButton2.Image"), System.Drawing.Image)
        Me.股道图解ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.股道图解ToolStripButton2.Name = "股道图解ToolStripButton2"
        Me.股道图解ToolStripButton2.Size = New System.Drawing.Size(76, 22)
        Me.股道图解ToolStripButton2.Text = "股道图解"
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.staBar, Me.ToolStripLabelTrainNumberInfor, Me.ToolLabError, Me.ProBar, Me.ToolStripLabelDate})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 569)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(886, 23)
        Me.StatusStrip.TabIndex = 3
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'staBar
        '
        Me.staBar.AutoSize = False
        Me.staBar.Name = "staBar"
        Me.staBar.Size = New System.Drawing.Size(100, 18)
        Me.staBar.Text = "运行图编制"
        '
        'ToolStripLabelTrainNumberInfor
        '
        Me.ToolStripLabelTrainNumberInfor.AutoSize = False
        Me.ToolStripLabelTrainNumberInfor.Name = "ToolStripLabelTrainNumberInfor"
        Me.ToolStripLabelTrainNumberInfor.Size = New System.Drawing.Size(150, 18)
        Me.ToolStripLabelTrainNumberInfor.Text = "列车总数:"
        '
        'ToolLabError
        '
        Me.ToolLabError.Name = "ToolLabError"
        Me.ToolLabError.Size = New System.Drawing.Size(116, 18)
        Me.ToolLabError.Text = "当前运行图没有错误"
        '
        'ProBar
        '
        Me.ProBar.Name = "ProBar"
        Me.ProBar.Size = New System.Drawing.Size(100, 17)
        '
        'ToolStripLabelDate
        '
        Me.ToolStripLabelDate.AutoSize = False
        Me.ToolStripLabelDate.Name = "ToolStripLabelDate"
        Me.ToolStripLabelDate.Size = New System.Drawing.Size(150, 18)
        Me.ToolStripLabelDate.Text = "当前时间"
        '
        'timerCurDate
        '
        Me.timerCurDate.Interval = 2000
        Me.timerCurDate.SynchronizingObject = Me
        '
        'cmuTrainLine
        '
        Me.cmuTrainLine.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.调整发点RToolStripMenuItem, Me.平移列车MToolStripMenuItem, Me.调匀运行ToolStripMenuItem, Me.调整至最小折返时间MToolStripMenuItem, Me.ToolStripSeparator10, Me.修改交路JToolStripMenuItem, Me.修改标尺EToolStripMenuItem, Me.修改车次IToolStripMenuItem, Me.修改列车类型LToolStripMenuItem, Me.ToolStripSeparator11, Me.修改停站信息OToolStripMenuItem, Me.修改停站股道GToolStripMenuItem, Me.ToolStripSeparator8, Me.断开列车交路DToolStripMenuItem, Me.修改车次连接EToolStripMenuItem, Me.ToolStripSeparator12, Me.编辑EToolStripMenuItem1, Me.删除列车DToolStripMenuItem, Me.ToolStripSeparator25, Me.车底后加一车ToolStripMenuItem7, Me.车底前加一车ToolStripMenuItem4, Me.车底操作HToolStripMenuItem, Me.ToolStripSeparator7, Me.列车信息ToolStripMenuItem, Me.车底信息CToolStripMenuItem, Me.显示车底所有列车SToolStripMenuItem})
        Me.cmuTrainLine.Name = "cmuTrainLine"
        Me.cmuTrainLine.Size = New System.Drawing.Size(205, 480)
        '
        '调整发点RToolStripMenuItem
        '
        Me.调整发点RToolStripMenuItem.Image = CType(resources.GetObject("调整发点RToolStripMenuItem.Image"), System.Drawing.Image)
        Me.调整发点RToolStripMenuItem.Name = "调整发点RToolStripMenuItem"
        Me.调整发点RToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.调整发点RToolStripMenuItem.Text = "调整发点(&R)"
        '
        '平移列车MToolStripMenuItem
        '
        Me.平移列车MToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.指定平移时间FToolStripMenuItem, Me.鼠标平移MToolStripMenuItem})
        Me.平移列车MToolStripMenuItem.Image = CType(resources.GetObject("平移列车MToolStripMenuItem.Image"), System.Drawing.Image)
        Me.平移列车MToolStripMenuItem.Name = "平移列车MToolStripMenuItem"
        Me.平移列车MToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.平移列车MToolStripMenuItem.Text = "平移列车(&M)"
        '
        '指定平移时间FToolStripMenuItem
        '
        Me.指定平移时间FToolStripMenuItem.Name = "指定平移时间FToolStripMenuItem"
        Me.指定平移时间FToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.指定平移时间FToolStripMenuItem.Text = "指定平移时间(&F)"
        '
        '鼠标平移MToolStripMenuItem
        '
        Me.鼠标平移MToolStripMenuItem.Name = "鼠标平移MToolStripMenuItem"
        Me.鼠标平移MToolStripMenuItem.Size = New System.Drawing.Size(162, 22)
        Me.鼠标平移MToolStripMenuItem.Text = "鼠标平移(&M)"
        '
        '调匀运行ToolStripMenuItem
        '
        Me.调匀运行ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.间隔调匀ToolStripMenuItem, Me.ToolStripSeparator39, Me.终到停站调匀SToolStripMenuItem, Me.始发停站调匀SToolStripMenuItem})
        Me.调匀运行ToolStripMenuItem.Name = "调匀运行ToolStripMenuItem"
        Me.调匀运行ToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.调匀运行ToolStripMenuItem.Text = "调匀运行线(&Y)"
        '
        '间隔调匀ToolStripMenuItem
        '
        Me.间隔调匀ToolStripMenuItem.Name = "间隔调匀ToolStripMenuItem"
        Me.间隔调匀ToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.间隔调匀ToolStripMenuItem.Text = "间隔调匀(&J)"
        '
        'ToolStripSeparator39
        '
        Me.ToolStripSeparator39.Name = "ToolStripSeparator39"
        Me.ToolStripSeparator39.Size = New System.Drawing.Size(160, 6)
        '
        '终到停站调匀SToolStripMenuItem
        '
        Me.终到停站调匀SToolStripMenuItem.Name = "终到停站调匀SToolStripMenuItem"
        Me.终到停站调匀SToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.终到停站调匀SToolStripMenuItem.Text = "终到停站调匀(&E)"
        '
        '始发停站调匀SToolStripMenuItem
        '
        Me.始发停站调匀SToolStripMenuItem.Name = "始发停站调匀SToolStripMenuItem"
        Me.始发停站调匀SToolStripMenuItem.Size = New System.Drawing.Size(163, 22)
        Me.始发停站调匀SToolStripMenuItem.Text = "始发停站调匀(&S)"
        '
        '调整至最小折返时间MToolStripMenuItem
        '
        Me.调整至最小折返时间MToolStripMenuItem.Image = CType(resources.GetObject("调整至最小折返时间MToolStripMenuItem.Image"), System.Drawing.Image)
        Me.调整至最小折返时间MToolStripMenuItem.Name = "调整至最小折返时间MToolStripMenuItem"
        Me.调整至最小折返时间MToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.调整至最小折返时间MToolStripMenuItem.Text = "调整至最小折返时间(&M)"
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(201, 6)
        '
        '修改交路JToolStripMenuItem
        '
        Me.修改交路JToolStripMenuItem.Image = CType(resources.GetObject("修改交路JToolStripMenuItem.Image"), System.Drawing.Image)
        Me.修改交路JToolStripMenuItem.Name = "修改交路JToolStripMenuItem"
        Me.修改交路JToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.修改交路JToolStripMenuItem.Text = "修改交路(&J)"
        '
        '修改标尺EToolStripMenuItem
        '
        Me.修改标尺EToolStripMenuItem.Image = CType(resources.GetObject("修改标尺EToolStripMenuItem.Image"), System.Drawing.Image)
        Me.修改标尺EToolStripMenuItem.Name = "修改标尺EToolStripMenuItem"
        Me.修改标尺EToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.修改标尺EToolStripMenuItem.Text = "修改标尺(&E)"
        '
        '修改车次IToolStripMenuItem
        '
        Me.修改车次IToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.修改车次编号EToolStripMenuItem, Me.ToolStripSeparator37, Me.车次号加一AToolStripMenuItem, Me.车次号减一MToolStripMenuItem})
        Me.修改车次IToolStripMenuItem.Image = CType(resources.GetObject("修改车次IToolStripMenuItem.Image"), System.Drawing.Image)
        Me.修改车次IToolStripMenuItem.Name = "修改车次IToolStripMenuItem"
        Me.修改车次IToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.修改车次IToolStripMenuItem.Text = "修改车次(&I)"
        '
        '修改车次编号EToolStripMenuItem
        '
        Me.修改车次编号EToolStripMenuItem.Name = "修改车次编号EToolStripMenuItem"
        Me.修改车次编号EToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.修改车次编号EToolStripMenuItem.Text = "修改车次编号(&E)"
        '
        'ToolStripSeparator37
        '
        Me.ToolStripSeparator37.Name = "ToolStripSeparator37"
        Me.ToolStripSeparator37.Size = New System.Drawing.Size(177, 6)
        '
        '车次号加一AToolStripMenuItem
        '
        Me.车次号加一AToolStripMenuItem.Name = "车次号加一AToolStripMenuItem"
        Me.车次号加一AToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.车次号加一AToolStripMenuItem.Text = "后续车次号加一(&A)"
        '
        '车次号减一MToolStripMenuItem
        '
        Me.车次号减一MToolStripMenuItem.Name = "车次号减一MToolStripMenuItem"
        Me.车次号减一MToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.车次号减一MToolStripMenuItem.Text = "后续车次号减一(&M)"
        '
        '修改列车类型LToolStripMenuItem
        '
        Me.修改列车类型LToolStripMenuItem.Name = "修改列车类型LToolStripMenuItem"
        Me.修改列车类型LToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.修改列车类型LToolStripMenuItem.Text = "修改列车性质(&L)"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(201, 6)
        '
        '修改停站信息OToolStripMenuItem
        '
        Me.修改停站信息OToolStripMenuItem.Image = CType(resources.GetObject("修改停站信息OToolStripMenuItem.Image"), System.Drawing.Image)
        Me.修改停站信息OToolStripMenuItem.Name = "修改停站信息OToolStripMenuItem"
        Me.修改停站信息OToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.修改停站信息OToolStripMenuItem.Text = "修改停站与运行时分(&O)"
        '
        '修改停站股道GToolStripMenuItem
        '
        Me.修改停站股道GToolStripMenuItem.Image = CType(resources.GetObject("修改停站股道GToolStripMenuItem.Image"), System.Drawing.Image)
        Me.修改停站股道GToolStripMenuItem.Name = "修改停站股道GToolStripMenuItem"
        Me.修改停站股道GToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.修改停站股道GToolStripMenuItem.Text = "修改停站股道(&G)"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(201, 6)
        '
        '断开列车交路DToolStripMenuItem
        '
        Me.断开列车交路DToolStripMenuItem.Image = CType(resources.GetObject("断开列车交路DToolStripMenuItem.Image"), System.Drawing.Image)
        Me.断开列车交路DToolStripMenuItem.Name = "断开列车交路DToolStripMenuItem"
        Me.断开列车交路DToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.断开列车交路DToolStripMenuItem.Text = "断开列车交路(&D)"
        '
        '修改车次连接EToolStripMenuItem
        '
        Me.修改车次连接EToolStripMenuItem.Image = CType(resources.GetObject("修改车次连接EToolStripMenuItem.Image"), System.Drawing.Image)
        Me.修改车次连接EToolStripMenuItem.Name = "修改车次连接EToolStripMenuItem"
        Me.修改车次连接EToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.修改车次连接EToolStripMenuItem.Text = "修改交路连接方式(&F)"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(201, 6)
        '
        '编辑EToolStripMenuItem1
        '
        Me.编辑EToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.剪切列车XToolStripMenuItem, Me.复制列车CToolStripMenuItem, Me.ToolStripSeparator22, Me.粘贴列车ToolStripMenuItem2})
        Me.编辑EToolStripMenuItem1.Name = "编辑EToolStripMenuItem1"
        Me.编辑EToolStripMenuItem1.Size = New System.Drawing.Size(204, 22)
        Me.编辑EToolStripMenuItem1.Text = "编辑(E)"
        '
        '剪切列车XToolStripMenuItem
        '
        Me.剪切列车XToolStripMenuItem.Image = CType(resources.GetObject("剪切列车XToolStripMenuItem.Image"), System.Drawing.Image)
        Me.剪切列车XToolStripMenuItem.Name = "剪切列车XToolStripMenuItem"
        Me.剪切列车XToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.剪切列车XToolStripMenuItem.Text = "剪切列车(&X)"
        '
        '复制列车CToolStripMenuItem
        '
        Me.复制列车CToolStripMenuItem.Image = CType(resources.GetObject("复制列车CToolStripMenuItem.Image"), System.Drawing.Image)
        Me.复制列车CToolStripMenuItem.Name = "复制列车CToolStripMenuItem"
        Me.复制列车CToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.复制列车CToolStripMenuItem.Text = "复制列车(&C)"
        '
        'ToolStripSeparator22
        '
        Me.ToolStripSeparator22.Name = "ToolStripSeparator22"
        Me.ToolStripSeparator22.Size = New System.Drawing.Size(137, 6)
        '
        '粘贴列车ToolStripMenuItem2
        '
        Me.粘贴列车ToolStripMenuItem2.Image = CType(resources.GetObject("粘贴列车ToolStripMenuItem2.Image"), System.Drawing.Image)
        Me.粘贴列车ToolStripMenuItem2.Name = "粘贴列车ToolStripMenuItem2"
        Me.粘贴列车ToolStripMenuItem2.Size = New System.Drawing.Size(140, 22)
        Me.粘贴列车ToolStripMenuItem2.Text = "粘贴列车(&V)"
        '
        '删除列车DToolStripMenuItem
        '
        Me.删除列车DToolStripMenuItem.Image = CType(resources.GetObject("删除列车DToolStripMenuItem.Image"), System.Drawing.Image)
        Me.删除列车DToolStripMenuItem.Name = "删除列车DToolStripMenuItem"
        Me.删除列车DToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.删除列车DToolStripMenuItem.Text = "删除列车(D)"
        '
        'ToolStripSeparator25
        '
        Me.ToolStripSeparator25.Name = "ToolStripSeparator25"
        Me.ToolStripSeparator25.Size = New System.Drawing.Size(201, 6)
        '
        '车底后加一车ToolStripMenuItem7
        '
        Me.车底后加一车ToolStripMenuItem7.Image = CType(resources.GetObject("车底后加一车ToolStripMenuItem7.Image"), System.Drawing.Image)
        Me.车底后加一车ToolStripMenuItem7.Name = "车底后加一车ToolStripMenuItem7"
        Me.车底后加一车ToolStripMenuItem7.Size = New System.Drawing.Size(204, 22)
        Me.车底后加一车ToolStripMenuItem7.Text = "车底后加一列车(&A)"
        '
        '车底前加一车ToolStripMenuItem4
        '
        Me.车底前加一车ToolStripMenuItem4.Image = CType(resources.GetObject("车底前加一车ToolStripMenuItem4.Image"), System.Drawing.Image)
        Me.车底前加一车ToolStripMenuItem4.Name = "车底前加一车ToolStripMenuItem4"
        Me.车底前加一车ToolStripMenuItem4.Size = New System.Drawing.Size(204, 22)
        Me.车底前加一车ToolStripMenuItem4.Text = "车底前加一列车(&B)"
        '
        '车底操作HToolStripMenuItem
        '
        Me.车底操作HToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.修改车底所有列车车次NToolStripMenuItem, Me.修改车底编号EToolStripMenuItem, Me.合并车底HToolStripMenuItem, Me.ToolStripSeparator23, Me.删除车底所有列车DToolStripMenuItem})
        Me.车底操作HToolStripMenuItem.Name = "车底操作HToolStripMenuItem"
        Me.车底操作HToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.车底操作HToolStripMenuItem.Text = "车底操作(&H)"
        '
        '修改车底所有列车车次NToolStripMenuItem
        '
        Me.修改车底所有列车车次NToolStripMenuItem.Name = "修改车底所有列车车次NToolStripMenuItem"
        Me.修改车底所有列车车次NToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.修改车底所有列车车次NToolStripMenuItem.Text = "修改车底所有列车车次(&N)"
        Me.修改车底所有列车车次NToolStripMenuItem.Visible = False
        '
        '修改车底编号EToolStripMenuItem
        '
        Me.修改车底编号EToolStripMenuItem.Image = CType(resources.GetObject("修改车底编号EToolStripMenuItem.Image"), System.Drawing.Image)
        Me.修改车底编号EToolStripMenuItem.Name = "修改车底编号EToolStripMenuItem"
        Me.修改车底编号EToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.修改车底编号EToolStripMenuItem.Text = "修改输出车底编号(&E)"
        '
        '合并车底HToolStripMenuItem
        '
        Me.合并车底HToolStripMenuItem.Image = CType(resources.GetObject("合并车底HToolStripMenuItem.Image"), System.Drawing.Image)
        Me.合并车底HToolStripMenuItem.Name = "合并车底HToolStripMenuItem"
        Me.合并车底HToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.合并车底HToolStripMenuItem.Text = "合并车底(&H)"
        '
        'ToolStripSeparator23
        '
        Me.ToolStripSeparator23.Name = "ToolStripSeparator23"
        Me.ToolStripSeparator23.Size = New System.Drawing.Size(211, 6)
        '
        '删除车底所有列车DToolStripMenuItem
        '
        Me.删除车底所有列车DToolStripMenuItem.Image = CType(resources.GetObject("删除车底所有列车DToolStripMenuItem.Image"), System.Drawing.Image)
        Me.删除车底所有列车DToolStripMenuItem.Name = "删除车底所有列车DToolStripMenuItem"
        Me.删除车底所有列车DToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.删除车底所有列车DToolStripMenuItem.Text = "删除车底所有列车(&D)"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(201, 6)
        '
        '列车信息ToolStripMenuItem
        '
        Me.列车信息ToolStripMenuItem.Image = CType(resources.GetObject("列车信息ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.列车信息ToolStripMenuItem.Name = "列车信息ToolStripMenuItem"
        Me.列车信息ToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.列车信息ToolStripMenuItem.Text = "列车信息与属性(I)"
        '
        '车底信息CToolStripMenuItem
        '
        Me.车底信息CToolStripMenuItem.Image = CType(resources.GetObject("车底信息CToolStripMenuItem.Image"), System.Drawing.Image)
        Me.车底信息CToolStripMenuItem.Name = "车底信息CToolStripMenuItem"
        Me.车底信息CToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.车底信息CToolStripMenuItem.Text = "车底信息与属性(&C)"
        '
        '显示车底所有列车SToolStripMenuItem
        '
        Me.显示车底所有列车SToolStripMenuItem.Image = CType(resources.GetObject("显示车底所有列车SToolStripMenuItem.Image"), System.Drawing.Image)
        Me.显示车底所有列车SToolStripMenuItem.Name = "显示车底所有列车SToolStripMenuItem"
        Me.显示车底所有列车SToolStripMenuItem.Size = New System.Drawing.Size(204, 22)
        Me.显示车底所有列车SToolStripMenuItem.Text = "显示车底所有列车(&S)"
        '
        'SplitMainContainer
        '
        Me.SplitMainContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitMainContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitMainContainer.Location = New System.Drawing.Point(0, 50)
        Me.SplitMainContainer.Name = "SplitMainContainer"
        Me.SplitMainContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitMainContainer.Panel1
        '
        Me.SplitMainContainer.Panel1.Controls.Add(Me.cmbJiShuTuJieSta)
        Me.SplitMainContainer.Panel1.Controls.Add(Me.labInfor)
        Me.SplitMainContainer.Panel1.Controls.Add(Me.labTime)
        Me.SplitMainContainer.Panel1MinSize = 20
        '
        'SplitMainContainer.Panel2
        '
        Me.SplitMainContainer.Panel2.Controls.Add(Me.SplitError)
        Me.SplitMainContainer.Size = New System.Drawing.Size(886, 519)
        Me.SplitMainContainer.SplitterDistance = 20
        Me.SplitMainContainer.TabIndex = 4
        Me.SplitMainContainer.Text = "SplitContainer1"
        '
        'cmbJiShuTuJieSta
        '
        Me.cmbJiShuTuJieSta.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmbJiShuTuJieSta.FormattingEnabled = True
        Me.cmbJiShuTuJieSta.Location = New System.Drawing.Point(758, 0)
        Me.cmbJiShuTuJieSta.Name = "cmbJiShuTuJieSta"
        Me.cmbJiShuTuJieSta.Size = New System.Drawing.Size(128, 20)
        Me.cmbJiShuTuJieSta.TabIndex = 5
        Me.cmbJiShuTuJieSta.Visible = False
        '
        'labInfor
        '
        Me.labInfor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.labInfor.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labInfor.ForeColor = System.Drawing.Color.Green
        Me.labInfor.Location = New System.Drawing.Point(119, 0)
        Me.labInfor.Name = "labInfor"
        Me.labInfor.Size = New System.Drawing.Size(767, 20)
        Me.labInfor.TabIndex = 0
        Me.labInfor.Text = "在此显示相关信息"
        '
        'labTime
        '
        Me.labTime.Dock = System.Windows.Forms.DockStyle.Left
        Me.labTime.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.labTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.labTime.Location = New System.Drawing.Point(0, 0)
        Me.labTime.Name = "labTime"
        Me.labTime.Size = New System.Drawing.Size(119, 20)
        Me.labTime.TabIndex = 1
        Me.labTime.Text = "当前时间: 01:01:01"
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
        Me.SplitError.Panel2.Controls.Add(Me.listViewTrain)
        Me.SplitError.Panel2MinSize = 0
        Me.SplitError.Size = New System.Drawing.Size(886, 495)
        Me.SplitError.SplitterDistance = 491
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
        Me.SplitConLeftRight.Size = New System.Drawing.Size(886, 491)
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
        Me.SplitDiagram.Size = New System.Drawing.Size(797, 491)
        Me.SplitDiagram.SplitterDistance = 91
        Me.SplitDiagram.TabIndex = 0
        '
        'PicStation2
        '
        Me.PicStation2.BackColor = System.Drawing.Color.White
        Me.PicStation2.Location = New System.Drawing.Point(12, 66)
        Me.PicStation2.Name = "PicStation2"
        Me.PicStation2.Size = New System.Drawing.Size(62, 232)
        Me.PicStation2.TabIndex = 6
        Me.PicStation2.TabStop = False
        '
        'PicDiagram
        '
        Me.PicDiagram.BackColor = System.Drawing.Color.White
        Me.PicDiagram.Location = New System.Drawing.Point(59, 66)
        Me.PicDiagram.Name = "PicDiagram"
        Me.PicDiagram.Size = New System.Drawing.Size(589, 266)
        Me.PicDiagram.TabIndex = 4
        Me.PicDiagram.TabStop = False
        '
        'picStation
        '
        Me.picStation.BackColor = System.Drawing.Color.White
        Me.picStation.Location = New System.Drawing.Point(11, 66)
        Me.picStation.Name = "picStation"
        Me.picStation.Size = New System.Drawing.Size(62, 232)
        Me.picStation.TabIndex = 5
        Me.picStation.TabStop = False
        '
        'listViewTrain
        '
        Me.listViewTrain.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.listViewTrain.AllowColumnReorder = True
        Me.listViewTrain.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.序号, Me.车次, Me.列车ID, Me.时刻, Me.车站, Me.出错信息})
        Me.listViewTrain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listViewTrain.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.listViewTrain.FullRowSelect = True
        Me.listViewTrain.GridLines = True
        Me.listViewTrain.HoverSelection = True
        Me.listViewTrain.Location = New System.Drawing.Point(0, 0)
        Me.listViewTrain.MultiSelect = False
        Me.listViewTrain.Name = "listViewTrain"
        Me.listViewTrain.Size = New System.Drawing.Size(886, 0)
        Me.listViewTrain.TabIndex = 6
        Me.listViewTrain.UseCompatibleStateImageBehavior = False
        Me.listViewTrain.View = System.Windows.Forms.View.Details
        '
        '序号
        '
        Me.序号.Text = "序号"
        Me.序号.Width = 40
        '
        '车次
        '
        Me.车次.Text = "车次"
        Me.车次.Width = 57
        '
        '列车ID
        '
        Me.列车ID.Text = "列车ID"
        '
        '时刻
        '
        Me.时刻.Text = "时刻"
        '
        '车站
        '
        Me.车站.Text = "车站"
        Me.车站.Width = 100
        '
        '出错信息
        '
        Me.出错信息.Text = "出错信息"
        Me.出错信息.Width = 600
        '
        'cmuDrawSingleLine
        '
        Me.cmuDrawSingleLine.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.查找列车FToolStripMenuItem})
        Me.cmuDrawSingleLine.Name = "cmuTrainLine"
        Me.cmuDrawSingleLine.Size = New System.Drawing.Size(141, 48)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Image = CType(resources.GetObject("ToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(140, 22)
        Me.ToolStripMenuItem1.Text = "增加列车(&A)"
        '
        '查找列车FToolStripMenuItem
        '
        Me.查找列车FToolStripMenuItem.Image = CType(resources.GetObject("查找列车FToolStripMenuItem.Image"), System.Drawing.Image)
        Me.查找列车FToolStripMenuItem.Name = "查找列车FToolStripMenuItem"
        Me.查找列车FToolStripMenuItem.Size = New System.Drawing.Size(140, 22)
        Me.查找列车FToolStripMenuItem.Text = "查找列车(&F)"
        '
        'cmuGuDaoLine
        '
        Me.cmuGuDaoLine.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.修改停站股道, Me.修改停站时间TToolStripMenuItem, Me.ToolStripSeparator42, Me.整理股道EToolStripMenuItem})
        Me.cmuGuDaoLine.Name = "cmuGuDaoLine"
        Me.cmuGuDaoLine.Size = New System.Drawing.Size(188, 76)
        '
        '修改停站股道
        '
        Me.修改停站股道.Image = CType(resources.GetObject("修改停站股道.Image"), System.Drawing.Image)
        Me.修改停站股道.Name = "修改停站股道"
        Me.修改停站股道.Size = New System.Drawing.Size(187, 22)
        Me.修改停站股道.Text = "修改停站股道(&E)"
        '
        '修改停站时间TToolStripMenuItem
        '
        Me.修改停站时间TToolStripMenuItem.Name = "修改停站时间TToolStripMenuItem"
        Me.修改停站时间TToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.修改停站时间TToolStripMenuItem.Text = "修改停站时间(&T)"
        '
        'ToolStripSeparator42
        '
        Me.ToolStripSeparator42.Name = "ToolStripSeparator42"
        Me.ToolStripSeparator42.Size = New System.Drawing.Size(184, 6)
        '
        '整理股道EToolStripMenuItem
        '
        Me.整理股道EToolStripMenuItem.Name = "整理股道EToolStripMenuItem"
        Me.整理股道EToolStripMenuItem.Size = New System.Drawing.Size(187, 22)
        Me.整理股道EToolStripMenuItem.Text = "整理股道占用时间(&Z)"
        '
        'cmnuTrains
        '
        Me.cmnuTrains.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.平移列车MToolStripMenuItem1, Me.ToolStripSeparator30, Me.ToolStripMenuItem5, Me.ToolStripMenuItem6, Me.ToolStripMenuItem10, Me.ToolStripSeparator38, Me.修改列车性质ZToolStripMenuItem, Me.调匀停站时间TToolStripMenuItem, Me.ToolStripSeparator35, Me.剪切CToolStripMenuItem1, Me.复制列车CToolStripMenuItem1, Me.粘贴ToolStripMenuItem, Me.ToolStripSeparator34, Me.ToolStripMenuItem15, Me.列车信息TToolStripMenuItem, Me.车底信息DToolStripMenuItem})
        Me.cmnuTrains.Name = "cmuTrainLine"
        Me.cmnuTrains.Size = New System.Drawing.Size(166, 292)
        '
        '平移列车MToolStripMenuItem1
        '
        Me.平移列车MToolStripMenuItem1.Image = CType(resources.GetObject("平移列车MToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.平移列车MToolStripMenuItem1.Name = "平移列车MToolStripMenuItem1"
        Me.平移列车MToolStripMenuItem1.Size = New System.Drawing.Size(165, 22)
        Me.平移列车MToolStripMenuItem1.Text = "平移列车(&M)"
        '
        'ToolStripSeparator30
        '
        Me.ToolStripSeparator30.Name = "ToolStripSeparator30"
        Me.ToolStripSeparator30.Size = New System.Drawing.Size(162, 6)
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Image = CType(resources.GetObject("ToolStripMenuItem5.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(165, 22)
        Me.ToolStripMenuItem5.Text = "修改交路(&J)"
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Image = CType(resources.GetObject("ToolStripMenuItem6.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(165, 22)
        Me.ToolStripMenuItem6.Text = "修改标尺(&E)"
        '
        'ToolStripMenuItem10
        '
        Me.ToolStripMenuItem10.Image = CType(resources.GetObject("ToolStripMenuItem10.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem10.Name = "ToolStripMenuItem10"
        Me.ToolStripMenuItem10.Size = New System.Drawing.Size(165, 22)
        Me.ToolStripMenuItem10.Text = "断开列车交路(&D)"
        '
        'ToolStripSeparator38
        '
        Me.ToolStripSeparator38.Name = "ToolStripSeparator38"
        Me.ToolStripSeparator38.Size = New System.Drawing.Size(162, 6)
        '
        '修改列车性质ZToolStripMenuItem
        '
        Me.修改列车性质ZToolStripMenuItem.Name = "修改列车性质ZToolStripMenuItem"
        Me.修改列车性质ZToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.修改列车性质ZToolStripMenuItem.Text = "修改列车性质(&Z)"
        '
        '调匀停站时间TToolStripMenuItem
        '
        Me.调匀停站时间TToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.终到调匀EToolStripMenuItem, Me.始发调匀SToolStripMenuItem})
        Me.调匀停站时间TToolStripMenuItem.Name = "调匀停站时间TToolStripMenuItem"
        Me.调匀停站时间TToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.调匀停站时间TToolStripMenuItem.Text = "调匀停站时间(&Y)"
        '
        '终到调匀EToolStripMenuItem
        '
        Me.终到调匀EToolStripMenuItem.Name = "终到调匀EToolStripMenuItem"
        Me.终到调匀EToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.终到调匀EToolStripMenuItem.Text = "终到调匀(&E)"
        '
        '始发调匀SToolStripMenuItem
        '
        Me.始发调匀SToolStripMenuItem.Name = "始发调匀SToolStripMenuItem"
        Me.始发调匀SToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.始发调匀SToolStripMenuItem.Text = "始发调匀(&S)"
        '
        'ToolStripSeparator35
        '
        Me.ToolStripSeparator35.Name = "ToolStripSeparator35"
        Me.ToolStripSeparator35.Size = New System.Drawing.Size(162, 6)
        '
        '剪切CToolStripMenuItem1
        '
        Me.剪切CToolStripMenuItem1.Image = CType(resources.GetObject("剪切CToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.剪切CToolStripMenuItem1.Name = "剪切CToolStripMenuItem1"
        Me.剪切CToolStripMenuItem1.Size = New System.Drawing.Size(165, 22)
        Me.剪切CToolStripMenuItem1.Text = "剪切(&X)"
        '
        '复制列车CToolStripMenuItem1
        '
        Me.复制列车CToolStripMenuItem1.Image = CType(resources.GetObject("复制列车CToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.复制列车CToolStripMenuItem1.Name = "复制列车CToolStripMenuItem1"
        Me.复制列车CToolStripMenuItem1.Size = New System.Drawing.Size(165, 22)
        Me.复制列车CToolStripMenuItem1.Text = "复制(&C)"
        '
        '粘贴ToolStripMenuItem
        '
        Me.粘贴ToolStripMenuItem.Image = CType(resources.GetObject("粘贴ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem"
        Me.粘贴ToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.粘贴ToolStripMenuItem.Text = "粘贴(&V)"
        '
        'ToolStripSeparator34
        '
        Me.ToolStripSeparator34.Name = "ToolStripSeparator34"
        Me.ToolStripSeparator34.Size = New System.Drawing.Size(162, 6)
        '
        'ToolStripMenuItem15
        '
        Me.ToolStripMenuItem15.Image = CType(resources.GetObject("ToolStripMenuItem15.Image"), System.Drawing.Image)
        Me.ToolStripMenuItem15.Name = "ToolStripMenuItem15"
        Me.ToolStripMenuItem15.Size = New System.Drawing.Size(165, 22)
        Me.ToolStripMenuItem15.Text = "删除(D)"
        '
        '列车信息TToolStripMenuItem
        '
        Me.列车信息TToolStripMenuItem.Name = "列车信息TToolStripMenuItem"
        Me.列车信息TToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.列车信息TToolStripMenuItem.Text = "列车信息(&T)"
        '
        '车底信息DToolStripMenuItem
        '
        Me.车底信息DToolStripMenuItem.Name = "车底信息DToolStripMenuItem"
        Me.车底信息DToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.车底信息DToolStripMenuItem.Text = "车底信息(&I)"
        '
        'frmTimeTableMain
        '
        Me.ClientSize = New System.Drawing.Size(886, 592)
        Me.Controls.Add(Me.SplitMainContainer)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.MenuMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmTimeTableMain"
        Me.Text = "列车运行图"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuMain.ResumeLayout(False)
        Me.MenuMain.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        CType(Me.timerCurDate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmuTrainLine.ResumeLayout(False)
        Me.SplitMainContainer.Panel1.ResumeLayout(False)
        Me.SplitMainContainer.Panel2.ResumeLayout(False)
        Me.SplitMainContainer.ResumeLayout(False)
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
        Me.cmuDrawSingleLine.ResumeLayout(False)
        Me.cmuGuDaoLine.ResumeLayout(False)
        Me.cmnuTrains.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitConMain As System.Windows.Forms.SplitContainer
    Friend WithEvents MenuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents 文件7FToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 打开运行图ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 分隔一ToolStripMenuItem As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 运行图另存为图片ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 退出EToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 底图DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 小时格ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 十分格ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 二分格ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 一分格ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 分隔线二ToolStripMenuItem As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 放大底图宽度ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 缩小底图宽度ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 运行图ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 刷新运行图ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 查看列车时刻表TToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents 时间格式ToolStripButton As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents 小时格ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 十分格ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 二分格ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 一分格ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 底图放大ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents 底图缩小ToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents timerCurDate As System.Timers.Timer
    Friend WithEvents cmuTrainLine As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 列车信息ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitMainContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents labInfor As System.Windows.Forms.Label
    Friend WithEvents 增加列车AToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmuDrawSingleLine As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents labTime As System.Windows.Forms.Label
    Friend WithEvents 删除列车DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 调整发点RToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 修改标尺EToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 修改交路JToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 调整至最小折返时间MToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents staBar As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 断开列车交路DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 修改车次连接EToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 显示车底所有列车SToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 车底信息CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 输出OToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 运行图DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 列车时刻表TToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 车底交路图CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 编辑时刻表车站顺序EToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 修改停站信息OToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 技术图解JToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 车站股道使用ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmbJiShuTuJieSta As System.Windows.Forms.ComboBox
    Friend WithEvents 整理列车ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 列车重编车次BToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator15 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 保存运行图SToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents 检查列车运行图KToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator16 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 修改停站股道GToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 打开ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator17 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 股道图解ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 运行图ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 检查错误ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator18 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 保存ToolStripButton7 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 参数CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 编辑EToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 约束铺画YToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 自动重编车次ZToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 撤销UToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 重复RToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 剪切CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 复制CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 粘贴VToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator14 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 删除DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 查找FToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator19 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 撤销tolStripUndo As System.Windows.Forms.ToolStripButton
    Friend WithEvents 重复tolStripRedo As System.Windows.Forms.ToolStripButton
    Friend WithEvents 新建运行图NToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 新建ToolStripButton5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 车站股道技术图解JToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmuGuDaoLine As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents 修改停站股道 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 查找列车FToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 计算指标XToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator20 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 系统设置SToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 纵向放大底图ZToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 纵向缩小底图XToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 纵向放大ToolStripButton6 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 纵向缩小ToolStripButton8 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator21 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 底图线型与颜色CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 线型与颜色LToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 测量时间ToolStripButton9 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 修改车次IToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 车底操作HToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 删除车底所有列车DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 修改车底编号EToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 合并车底HToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator23 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 运行图管理MToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 复制ToolStripButton9 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 平移列车MToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator25 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator26 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 剪切ToolStripButton10 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 粘贴ToolStripButton13 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 删除ToolStripButton11 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 查找ToolStripButton12 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator27 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 运行图管理ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 运行图数据接口KToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator24 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 批处理组操作UToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 全部列车重新铺画AToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 能力计算CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripLabelTrainNumberInfor As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripLabelDate As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripSeparator28 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 打印运行图ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator29 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 线型与颜色ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitError As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitConLeftRight As System.Windows.Forms.SplitContainer
    Friend WithEvents listViewTrain As System.Windows.Forms.ListView
    Friend WithEvents 序号 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 车次 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 列车ID As System.Windows.Forms.ColumnHeader
    Friend WithEvents 时刻 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 出错信息 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 车站 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ToolStripSeparator32 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 自动检查错误EToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents picStation As System.Windows.Forms.PictureBox
    Friend WithEvents PicDiagram As System.Windows.Forms.PictureBox
    Friend WithEvents SplitDiagram As System.Windows.Forms.SplitContainer
    Friend WithEvents PicStation2 As System.Windows.Forms.PictureBox
    Friend WithEvents 车底组铺画DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 铺画间隔设置ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 初始车底组铺画ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 继续ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator33 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 一次性铺画方案OToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 修改车底所有列车车次NToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    Private Sub SplitDiagram_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles SplitDiagram.Scroll
        MsgBox("aa")
    End Sub
    Friend WithEvents 仿真SToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 基于运行图仿真ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 铺画方式ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 不越行ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 可以越行ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmnuTrains As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripSeparator30 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator35 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripMenuItem15 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 编辑EToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 复制列车CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 剪切列车XToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 粘贴列车ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 车底前加一车ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 车底后加一车ToolStripMenuItem7 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator22 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 修改停站时间TToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 折返仿真RToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 线路通过能力仿真TToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator31 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 牵引曲线QToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 剪切CToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 复制列车CToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 粘贴ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator34 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 多选ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents 指定平移时间FToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 鼠标平移MToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 平移列车MToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 调整时移动提示ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator36 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 列车信息TToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 车底信息DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 整理车站股道ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 整理到发时刻DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 整理折返时刻ZToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolLabError As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents 运行图编制ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 单一或大小及共线交路ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 环形交路ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 错误提示ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 只显示前20条ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 全部显示ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 调匀运行ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 间隔调匀ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 终到停站调匀SToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 修改列车类型LToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 修改列车性质ZToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 全选AToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 修改车次编号EToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator37 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 车次号加一AToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 车次号减一MToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 调匀停站时间TToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator38 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 终到调匀EToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 始发调匀SToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator39 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 始发停站调匀SToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripLeft As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripRight As System.Windows.Forms.ToolStripButton
    Friend WithEvents 向左翻页ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 向右翻页ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator40 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator41 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator42 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 整理股道EToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
