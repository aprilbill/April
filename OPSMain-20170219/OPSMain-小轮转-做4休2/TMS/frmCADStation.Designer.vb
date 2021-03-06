Partial Public Class frmCADStation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCADStation))
        Me.MenuMain = New System.Windows.Forms.MenuStrip
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.保存为图片UToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.将车站平图存为图片SToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.将线路平面图存为图片LToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.打印PToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.车站平面图SToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.线路平面图LToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.trvLine = New System.Windows.Forms.TreeView
        Me.txtInfor = New System.Windows.Forms.TextBox
        Me.SplitRight = New System.Windows.Forms.SplitContainer
        Me.tolStaEdit = New System.Windows.Forms.ToolStrip
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.tolBtShowGuDaoNum = New System.Windows.Forms.ToolStripButton
        Me.tolbtShowTrackNum = New System.Windows.Forms.ToolStripButton
        Me.tolBtShowCrossingNum = New System.Windows.Forms.ToolStripButton
        Me.tolbtShowElseInfor = New System.Windows.Forms.ToolStripButton
        Me.toolStaEquip = New System.Windows.Forms.ToolStrip
        Me.tolbtCursor = New System.Windows.Forms.ToolStripButton
        Me.TolbtMultiLine = New System.Windows.Forms.ToolStripButton
        Me.tolbtHandMove = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator
        Me.tolbtZoom = New System.Windows.Forms.ToolStripButton
        Me.TolbtReduce = New System.Windows.Forms.ToolStripButton
        Me.ToolStripSeparator21 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel
        Me.tolStripCmbShowScale = New System.Windows.Forms.ToolStripComboBox
        Me.ToolStripSeparator27 = New System.Windows.Forms.ToolStripSeparator
        Me.tolbtRefresh = New System.Windows.Forms.ToolStripButton
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer
        Me.picLine = New System.Windows.Forms.PictureBox
        Me.picSta = New System.Windows.Forms.PictureBox
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.proBar = New System.Windows.Forms.ToolStripProgressBar
        Me.imgStaEquip = New System.Windows.Forms.ImageList(Me.components)
        Me.TimerMovePic = New System.Windows.Forms.Timer(Me.components)
        Me.显示网格 = New System.Windows.Forms.ToolStripButton
        Me.MenuMain.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitRight.Panel1.SuspendLayout()
        Me.SplitRight.Panel2.SuspendLayout()
        Me.SplitRight.SuspendLayout()
        Me.tolStaEdit.SuspendLayout()
        Me.toolStaEquip.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.picLine, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picSta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuMain
        '
        Me.MenuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.MenuMain.Location = New System.Drawing.Point(0, 0)
        Me.MenuMain.Name = "MenuMain"
        Me.MenuMain.Size = New System.Drawing.Size(842, 24)
        Me.MenuMain.TabIndex = 1
        Me.MenuMain.Text = "MenuStrip1"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.保存为图片UToolStripMenuItem, Me.打印PToolStripMenuItem, Me.ToolStripSeparator9, Me.ToolStripMenuItem4})
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(57, 20)
        Me.ToolStripMenuItem1.Text = "文件(&F)"
        '
        '保存为图片UToolStripMenuItem
        '
        Me.保存为图片UToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.将车站平图存为图片SToolStripMenuItem1, Me.将线路平面图存为图片LToolStripMenuItem})
        Me.保存为图片UToolStripMenuItem.Name = "保存为图片UToolStripMenuItem"
        Me.保存为图片UToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.保存为图片UToolStripMenuItem.Text = "保存为图片(&U)"
        '
        '将车站平图存为图片SToolStripMenuItem1
        '
        Me.将车站平图存为图片SToolStripMenuItem1.Name = "将车站平图存为图片SToolStripMenuItem1"
        Me.将车站平图存为图片SToolStripMenuItem1.Size = New System.Drawing.Size(152, 22)
        Me.将车站平图存为图片SToolStripMenuItem1.Text = "车站平面图(&S)"
        '
        '将线路平面图存为图片LToolStripMenuItem
        '
        Me.将线路平面图存为图片LToolStripMenuItem.Name = "将线路平面图存为图片LToolStripMenuItem"
        Me.将线路平面图存为图片LToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.将线路平面图存为图片LToolStripMenuItem.Text = "线路平面图(&L)"
        '
        '打印PToolStripMenuItem
        '
        Me.打印PToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.车站平面图SToolStripMenuItem, Me.线路平面图LToolStripMenuItem})
        Me.打印PToolStripMenuItem.Image = CType(resources.GetObject("打印PToolStripMenuItem.Image"), System.Drawing.Image)
        Me.打印PToolStripMenuItem.Name = "打印PToolStripMenuItem"
        Me.打印PToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.打印PToolStripMenuItem.Text = "打印(&P)"
        '
        '车站平面图SToolStripMenuItem
        '
        Me.车站平面图SToolStripMenuItem.Name = "车站平面图SToolStripMenuItem"
        Me.车站平面图SToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.车站平面图SToolStripMenuItem.Text = "车站平面图(&S)"
        '
        '线路平面图LToolStripMenuItem
        '
        Me.线路平面图LToolStripMenuItem.Name = "线路平面图LToolStripMenuItem"
        Me.线路平面图LToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.线路平面图LToolStripMenuItem.Text = "线路平面图(&L)"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(149, 6)
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(152, 22)
        Me.ToolStripMenuItem4.Text = "退出(&Q)"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 24)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitRight)
        Me.SplitContainer1.Size = New System.Drawing.Size(842, 489)
        Me.SplitContainer1.SplitterDistance = 202
        Me.SplitContainer1.TabIndex = 3
        Me.SplitContainer1.Text = "SplitContainer1"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.trvLine)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.txtInfor)
        Me.SplitContainer2.Size = New System.Drawing.Size(202, 489)
        Me.SplitContainer2.SplitterDistance = 330
        Me.SplitContainer2.TabIndex = 0
        Me.SplitContainer2.Text = "SplitContainer2"
        '
        'trvLine
        '
        Me.trvLine.Dock = System.Windows.Forms.DockStyle.Fill
        Me.trvLine.Location = New System.Drawing.Point(0, 0)
        Me.trvLine.Name = "trvLine"
        Me.trvLine.Size = New System.Drawing.Size(198, 326)
        Me.trvLine.TabIndex = 1
        '
        'txtInfor
        '
        Me.txtInfor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtInfor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtInfor.Location = New System.Drawing.Point(0, 0)
        Me.txtInfor.Multiline = True
        Me.txtInfor.Name = "txtInfor"
        Me.txtInfor.Size = New System.Drawing.Size(198, 151)
        Me.txtInfor.TabIndex = 1
        '
        'SplitRight
        '
        Me.SplitRight.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitRight.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitRight.Location = New System.Drawing.Point(0, 0)
        Me.SplitRight.Name = "SplitRight"
        Me.SplitRight.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitRight.Panel1
        '
        Me.SplitRight.Panel1.Controls.Add(Me.tolStaEdit)
        Me.SplitRight.Panel1.Controls.Add(Me.toolStaEquip)
        '
        'SplitRight.Panel2
        '
        Me.SplitRight.Panel2.Controls.Add(Me.SplitContainer4)
        Me.SplitRight.Size = New System.Drawing.Size(636, 489)
        Me.SplitRight.SplitterDistance = 52
        Me.SplitRight.TabIndex = 0
        Me.SplitRight.Text = "SplitContainer3"
        '
        'tolStaEdit
        '
        Me.tolStaEdit.Dock = System.Windows.Forms.DockStyle.None
        Me.tolStaEdit.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.显示网格, Me.ToolStripSeparator5, Me.tolBtShowGuDaoNum, Me.tolbtShowTrackNum, Me.tolBtShowCrossingNum, Me.tolbtShowElseInfor})
        Me.tolStaEdit.Location = New System.Drawing.Point(0, 1)
        Me.tolStaEdit.Name = "tolStaEdit"
        Me.tolStaEdit.Size = New System.Drawing.Size(131, 25)
        Me.tolStaEdit.TabIndex = 3
        Me.tolStaEdit.Text = "ToolStrip2"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'tolBtShowGuDaoNum
        '
        Me.tolBtShowGuDaoNum.Checked = True
        Me.tolBtShowGuDaoNum.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tolBtShowGuDaoNum.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tolBtShowGuDaoNum.Image = CType(resources.GetObject("tolBtShowGuDaoNum.Image"), System.Drawing.Image)
        Me.tolBtShowGuDaoNum.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tolBtShowGuDaoNum.Name = "tolBtShowGuDaoNum"
        Me.tolBtShowGuDaoNum.Size = New System.Drawing.Size(23, 22)
        Me.tolBtShowGuDaoNum.Text = "显示股道号"
        '
        'tolbtShowTrackNum
        '
        Me.tolbtShowTrackNum.Checked = True
        Me.tolbtShowTrackNum.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tolbtShowTrackNum.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tolbtShowTrackNum.Image = CType(resources.GetObject("tolbtShowTrackNum.Image"), System.Drawing.Image)
        Me.tolbtShowTrackNum.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tolbtShowTrackNum.Name = "tolbtShowTrackNum"
        Me.tolbtShowTrackNum.Size = New System.Drawing.Size(23, 22)
        Me.tolbtShowTrackNum.Text = "显示线段号"
        '
        'tolBtShowCrossingNum
        '
        Me.tolBtShowCrossingNum.Checked = True
        Me.tolBtShowCrossingNum.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tolBtShowCrossingNum.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tolBtShowCrossingNum.Image = CType(resources.GetObject("tolBtShowCrossingNum.Image"), System.Drawing.Image)
        Me.tolBtShowCrossingNum.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tolBtShowCrossingNum.Name = "tolBtShowCrossingNum"
        Me.tolBtShowCrossingNum.Size = New System.Drawing.Size(23, 22)
        Me.tolBtShowCrossingNum.Text = "显示道岔号"
        '
        'tolbtShowElseInfor
        '
        Me.tolbtShowElseInfor.Checked = True
        Me.tolbtShowElseInfor.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tolbtShowElseInfor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tolbtShowElseInfor.Image = CType(resources.GetObject("tolbtShowElseInfor.Image"), System.Drawing.Image)
        Me.tolbtShowElseInfor.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tolbtShowElseInfor.Name = "tolbtShowElseInfor"
        Me.tolbtShowElseInfor.Size = New System.Drawing.Size(23, 22)
        Me.tolbtShowElseInfor.Text = "显示其他信息"
        '
        'toolStaEquip
        '
        Me.toolStaEquip.Dock = System.Windows.Forms.DockStyle.None
        Me.toolStaEquip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tolbtCursor, Me.TolbtMultiLine, Me.tolbtHandMove, Me.ToolStripSeparator15, Me.tolbtZoom, Me.TolbtReduce, Me.ToolStripSeparator21, Me.ToolStripLabel1, Me.tolStripCmbShowScale, Me.ToolStripSeparator27, Me.tolbtRefresh})
        Me.toolStaEquip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.toolStaEquip.Location = New System.Drawing.Point(0, 26)
        Me.toolStaEquip.Name = "toolStaEquip"
        Me.toolStaEquip.Size = New System.Drawing.Size(350, 25)
        Me.toolStaEquip.TabIndex = 2
        Me.toolStaEquip.Text = "ToolStrip2"
        '
        'tolbtCursor
        '
        Me.tolbtCursor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tolbtCursor.Image = CType(resources.GetObject("tolbtCursor.Image"), System.Drawing.Image)
        Me.tolbtCursor.Name = "tolbtCursor"
        Me.tolbtCursor.Size = New System.Drawing.Size(23, 22)
        Me.tolbtCursor.Text = "默认指针"
        '
        'TolbtMultiLine
        '
        Me.TolbtMultiLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TolbtMultiLine.Image = CType(resources.GetObject("TolbtMultiLine.Image"), System.Drawing.Image)
        Me.TolbtMultiLine.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TolbtMultiLine.Name = "TolbtMultiLine"
        Me.TolbtMultiLine.Size = New System.Drawing.Size(23, 22)
        Me.TolbtMultiLine.Text = "线段多选"
        '
        'tolbtHandMove
        '
        Me.tolbtHandMove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tolbtHandMove.Image = CType(resources.GetObject("tolbtHandMove.Image"), System.Drawing.Image)
        Me.tolbtHandMove.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tolbtHandMove.Name = "tolbtHandMove"
        Me.tolbtHandMove.Size = New System.Drawing.Size(23, 22)
        Me.tolbtHandMove.Text = "移动底图"
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(6, 25)
        '
        'tolbtZoom
        '
        Me.tolbtZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tolbtZoom.Image = CType(resources.GetObject("tolbtZoom.Image"), System.Drawing.Image)
        Me.tolbtZoom.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tolbtZoom.Name = "tolbtZoom"
        Me.tolbtZoom.Size = New System.Drawing.Size(23, 22)
        Me.tolbtZoom.Text = "放大底图"
        '
        'TolbtReduce
        '
        Me.TolbtReduce.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.TolbtReduce.Image = CType(resources.GetObject("TolbtReduce.Image"), System.Drawing.Image)
        Me.TolbtReduce.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.TolbtReduce.Name = "TolbtReduce"
        Me.TolbtReduce.Size = New System.Drawing.Size(23, 22)
        Me.TolbtReduce.Text = "缩小底图"
        '
        'ToolStripSeparator21
        '
        Me.ToolStripSeparator21.Name = "ToolStripSeparator21"
        Me.ToolStripSeparator21.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(55, 22)
        Me.ToolStripLabel1.Text = "缩放大小"
        '
        'tolStripCmbShowScale
        '
        Me.tolStripCmbShowScale.Items.AddRange(New Object() {"200%", "150%", "100%", "75%", "50%", "25%", "适应页宽", "适应页高", "适应页面"})
        Me.tolStripCmbShowScale.Name = "tolStripCmbShowScale"
        Me.tolStripCmbShowScale.Size = New System.Drawing.Size(75, 25)
        '
        'ToolStripSeparator27
        '
        Me.ToolStripSeparator27.Name = "ToolStripSeparator27"
        Me.ToolStripSeparator27.Size = New System.Drawing.Size(6, 25)
        '
        'tolbtRefresh
        '
        Me.tolbtRefresh.Image = CType(resources.GetObject("tolbtRefresh.Image"), System.Drawing.Image)
        Me.tolbtRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tolbtRefresh.Name = "tolbtRefresh"
        Me.tolbtRefresh.Size = New System.Drawing.Size(75, 22)
        Me.tolbtRefresh.Text = "刷新底图"
        '
        'SplitContainer4
        '
        Me.SplitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.AutoScroll = True
        Me.SplitContainer4.Panel1.Controls.Add(Me.picLine)
        Me.SplitContainer4.Panel1.Controls.Add(Me.picSta)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.AutoScroll = True
        Me.SplitContainer4.Panel2MinSize = 0
        Me.SplitContainer4.Size = New System.Drawing.Size(636, 433)
        Me.SplitContainer4.SplitterDistance = 400
        Me.SplitContainer4.TabIndex = 0
        Me.SplitContainer4.Text = "SplitContainer4"
        '
        'picLine
        '
        Me.picLine.BackColor = System.Drawing.Color.Black
        Me.picLine.Location = New System.Drawing.Point(463, 14)
        Me.picLine.Name = "picLine"
        Me.picLine.Size = New System.Drawing.Size(159, 156)
        Me.picLine.TabIndex = 1
        Me.picLine.TabStop = False
        '
        'picSta
        '
        Me.picSta.BackColor = System.Drawing.Color.Black
        Me.picSta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picSta.Location = New System.Drawing.Point(0, 0)
        Me.picSta.Name = "picSta"
        Me.picSta.Size = New System.Drawing.Size(632, 396)
        Me.picSta.TabIndex = 1
        Me.picSta.TabStop = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel1, Me.proBar})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 513)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(842, 22)
        Me.StatusStrip1.TabIndex = 4
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(67, 17)
        Me.ToolStripStatusLabel1.Text = "车站平面图"
        '
        'proBar
        '
        Me.proBar.Name = "proBar"
        Me.proBar.Size = New System.Drawing.Size(200, 16)
        Me.proBar.Visible = False
        '
        'imgStaEquip
        '
        Me.imgStaEquip.ImageStream = CType(resources.GetObject("imgStaEquip.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgStaEquip.TransparentColor = System.Drawing.Color.Transparent
        Me.imgStaEquip.Images.SetKeyName(0, "Cursor1.ico")
        Me.imgStaEquip.Images.SetKeyName(1, "StaEquipCross1.ico")
        Me.imgStaEquip.Images.SetKeyName(2, "StaEquipCross2.ico")
        Me.imgStaEquip.Images.SetKeyName(3, "StaEquipCross3.ico")
        Me.imgStaEquip.Images.SetKeyName(4, "StaEquipCross4.ico")
        Me.imgStaEquip.Images.SetKeyName(5, "StaEquipCross5.ico")
        Me.imgStaEquip.Images.SetKeyName(6, "StaEquipCross6.ico")
        Me.imgStaEquip.Images.SetKeyName(7, "StaEquipCross7.ico")
        Me.imgStaEquip.Images.SetKeyName(8, "StaEquipCross8.ico")
        Me.imgStaEquip.Images.SetKeyName(9, "StaEquipCross9.ico")
        Me.imgStaEquip.Images.SetKeyName(10, "StaEquipCross10.ico")
        Me.imgStaEquip.Images.SetKeyName(11, "StaEquipCross11.ico")
        Me.imgStaEquip.Images.SetKeyName(12, "StaEquipLine.ico")
        Me.imgStaEquip.Images.SetKeyName(13, "StaEquipSignal1.ico")
        Me.imgStaEquip.Images.SetKeyName(14, "StaEquipSignal2.ico")
        Me.imgStaEquip.Images.SetKeyName(15, "StaEquipSignal3.ico")
        Me.imgStaEquip.Images.SetKeyName(16, "StaEquipSignal4.ico")
        Me.imgStaEquip.Images.SetKeyName(17, "StaEquipSignal5.ico")
        Me.imgStaEquip.Images.SetKeyName(18, "StaEquipSignal6.ico")
        Me.imgStaEquip.Images.SetKeyName(19, "PlatForm.ico")
        Me.imgStaEquip.Images.SetKeyName(20, "PlatForm1.ico")
        Me.imgStaEquip.Images.SetKeyName(21, "Text.ico")
        Me.imgStaEquip.Images.SetKeyName(22, "Grid.ico")
        '
        'TimerMovePic
        '
        '
        '显示网格
        '
        Me.显示网格.CheckOnClick = True
        Me.显示网格.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.显示网格.Image = CType(resources.GetObject("显示网格.Image"), System.Drawing.Image)
        Me.显示网格.Name = "显示网格"
        Me.显示网格.Size = New System.Drawing.Size(23, 22)
        Me.显示网格.Text = "显示网格"
        '
        'frmCADStation
        '
        Me.ClientSize = New System.Drawing.Size(842, 535)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuMain)
        Me.Name = "frmCADStation"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuMain.ResumeLayout(False)
        Me.MenuMain.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitRight.Panel1.ResumeLayout(False)
        Me.SplitRight.Panel1.PerformLayout()
        Me.SplitRight.Panel2.ResumeLayout(False)
        Me.SplitRight.ResumeLayout(False)
        Me.tolStaEdit.ResumeLayout(False)
        Me.tolStaEdit.PerformLayout()
        Me.toolStaEquip.ResumeLayout(False)
        Me.toolStaEquip.PerformLayout()
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.picLine, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picSta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitRight As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents tolStaEdit As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents toolStaEquip As System.Windows.Forms.ToolStrip
    Friend WithEvents tolbtCursor As System.Windows.Forms.ToolStripButton
    Friend WithEvents trvLine As System.Windows.Forms.TreeView
    Friend WithEvents txtInfor As System.Windows.Forms.TextBox
    Friend WithEvents picSta As System.Windows.Forms.PictureBox
    Friend WithEvents picLine As System.Windows.Forms.PictureBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents imgStaEquip As System.Windows.Forms.ImageList
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents proBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TolbtMultiLine As System.Windows.Forms.ToolStripButton
    Friend WithEvents TimerMovePic As System.Windows.Forms.Timer
    Friend WithEvents tolBtShowGuDaoNum As System.Windows.Forms.ToolStripButton
    Friend WithEvents tolbtZoom As System.Windows.Forms.ToolStripButton
    Friend WithEvents TolbtReduce As System.Windows.Forms.ToolStripButton
    Friend WithEvents tolbtShowTrackNum As System.Windows.Forms.ToolStripButton
    Friend WithEvents tolBtShowCrossingNum As System.Windows.Forms.ToolStripButton
    Friend WithEvents tolbtShowElseInfor As System.Windows.Forms.ToolStripButton
    Friend WithEvents tolStripCmbShowScale As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator27 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tolbtRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator15 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tolbtHandMove As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator21 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents 打印PToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 车站平面图SToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 线路平面图LToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 保存为图片UToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 将车站平图存为图片SToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 将线路平面图存为图片LToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 显示网格 As System.Windows.Forms.ToolStripButton

End Class
