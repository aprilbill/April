<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmODSMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmODSMain))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.文件FToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.用户管理UToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.更换用户CToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.退出EToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.乘务信息管理DToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.基本参数设定ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.交路距离参数ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.区域参数ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.运行图管理ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.运行图导入ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ts运行图管理 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Ts运行图浏览 = New System.Windows.Forms.ToolStripMenuItem()
        Me.乘务员信息管理ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.乘务计划ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.人数测算ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.乘务计划ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.乘务员匹配ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.乘务输出ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.乘务绩效管理ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.乘务员绩效管理ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.年度星级成绩录入XToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.检查更新ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel8 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel9 = New System.Windows.Forms.LinkLabel()
        Me.pnlMap = New System.Windows.Forms.Panel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.文件FToolStripMenuItem, Me.乘务信息管理DToolStripMenuItem, Me.乘务计划ToolStripMenuItem, Me.乘务输出ToolStripMenuItem, Me.乘务绩效管理ToolStripMenuItem, Me.检查更新ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(762, 25)
        Me.MenuStrip1.TabIndex = 4
        Me.MenuStrip1.Text = "检查更新"
        '
        '文件FToolStripMenuItem
        '
        Me.文件FToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.用户管理UToolStripMenuItem, Me.更换用户CToolStripMenuItem, Me.退出EToolStripMenuItem})
        Me.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem"
        Me.文件FToolStripMenuItem.Size = New System.Drawing.Size(82, 21)
        Me.文件FToolStripMenuItem.Text = "用户管理(&F)"
        '
        '用户管理UToolStripMenuItem
        '
        Me.用户管理UToolStripMenuItem.Name = "用户管理UToolStripMenuItem"
        Me.用户管理UToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.用户管理UToolStripMenuItem.Text = "用户管理(&U)"
        '
        '更换用户CToolStripMenuItem
        '
        Me.更换用户CToolStripMenuItem.Name = "更换用户CToolStripMenuItem"
        Me.更换用户CToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.更换用户CToolStripMenuItem.Text = "更换用户(&C)"
        '
        '退出EToolStripMenuItem
        '
        Me.退出EToolStripMenuItem.Name = "退出EToolStripMenuItem"
        Me.退出EToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.退出EToolStripMenuItem.Text = "退出(&E)"
        '
        '乘务信息管理DToolStripMenuItem
        '
        Me.乘务信息管理DToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.基本参数设定ToolStripMenuItem, Me.运行图管理ToolStripMenuItem, Me.乘务员信息管理ToolStripMenuItem1})
        Me.乘务信息管理DToolStripMenuItem.Name = "乘务信息管理DToolStripMenuItem"
        Me.乘务信息管理DToolStripMenuItem.Size = New System.Drawing.Size(109, 21)
        Me.乘务信息管理DToolStripMenuItem.Text = "乘务信息管理(&D)"
        '
        '基本参数设定ToolStripMenuItem
        '
        Me.基本参数设定ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.交路距离参数ToolStripMenuItem, Me.区域参数ToolStripMenuItem})
        Me.基本参数设定ToolStripMenuItem.Name = "基本参数设定ToolStripMenuItem"
        Me.基本参数设定ToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.基本参数设定ToolStripMenuItem.Text = "系统参数设定(&P)"
        '
        '交路距离参数ToolStripMenuItem
        '
        Me.交路距离参数ToolStripMenuItem.Name = "交路距离参数ToolStripMenuItem"
        Me.交路距离参数ToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.交路距离参数ToolStripMenuItem.Text = "交路距离参数(&R)"
        '
        '区域参数ToolStripMenuItem
        '
        Me.区域参数ToolStripMenuItem.Name = "区域参数ToolStripMenuItem"
        Me.区域参数ToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.区域参数ToolStripMenuItem.Text = "区域参数(&A)"
        '
        '运行图管理ToolStripMenuItem
        '
        Me.运行图管理ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.运行图导入ToolStripMenuItem, Me.ts运行图管理, Me.Ts运行图浏览})
        Me.运行图管理ToolStripMenuItem.Name = "运行图管理ToolStripMenuItem"
        Me.运行图管理ToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.运行图管理ToolStripMenuItem.Text = "运行图管理(&G)"
        '
        '运行图导入ToolStripMenuItem
        '
        Me.运行图导入ToolStripMenuItem.Name = "运行图导入ToolStripMenuItem"
        Me.运行图导入ToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.运行图导入ToolStripMenuItem.Text = "运行图导入(&I)"
        '
        'ts运行图管理
        '
        Me.ts运行图管理.Name = "ts运行图管理"
        Me.ts运行图管理.Size = New System.Drawing.Size(161, 22)
        Me.ts运行图管理.Text = "版本管理(&M)"
        '
        'Ts运行图浏览
        '
        Me.Ts运行图浏览.Image = CType(resources.GetObject("Ts运行图浏览.Image"), System.Drawing.Image)
        Me.Ts运行图浏览.Name = "Ts运行图浏览"
        Me.Ts运行图浏览.Size = New System.Drawing.Size(161, 22)
        Me.Ts运行图浏览.Text = "运行图浏览(&V)..."
        '
        '乘务员信息管理ToolStripMenuItem1
        '
        Me.乘务员信息管理ToolStripMenuItem1.Name = "乘务员信息管理ToolStripMenuItem1"
        Me.乘务员信息管理ToolStripMenuItem1.Size = New System.Drawing.Size(177, 22)
        Me.乘务员信息管理ToolStripMenuItem1.Text = "乘务员信息管理(&D)"
        '
        '乘务计划ToolStripMenuItem
        '
        Me.乘务计划ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.人数测算ToolStripMenuItem, Me.乘务计划ToolStripMenuItem1, Me.乘务员匹配ToolStripMenuItem})
        Me.乘务计划ToolStripMenuItem.Name = "乘务计划ToolStripMenuItem"
        Me.乘务计划ToolStripMenuItem.Size = New System.Drawing.Size(108, 21)
        Me.乘务计划ToolStripMenuItem.Text = "乘务计划管理(&C)"
        '
        '人数测算ToolStripMenuItem
        '
        Me.人数测算ToolStripMenuItem.Name = "人数测算ToolStripMenuItem"
        Me.人数测算ToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.人数测算ToolStripMenuItem.Text = "人数测算"
        '
        '乘务计划ToolStripMenuItem1
        '
        Me.乘务计划ToolStripMenuItem1.Image = CType(resources.GetObject("乘务计划ToolStripMenuItem1.Image"), System.Drawing.Image)
        Me.乘务计划ToolStripMenuItem1.Name = "乘务计划ToolStripMenuItem1"
        Me.乘务计划ToolStripMenuItem1.Size = New System.Drawing.Size(165, 22)
        Me.乘务计划ToolStripMenuItem1.Text = "乘务计划编制(&P)"
        '
        '乘务员匹配ToolStripMenuItem
        '
        Me.乘务员匹配ToolStripMenuItem.Image = CType(resources.GetObject("乘务员匹配ToolStripMenuItem.Image"), System.Drawing.Image)
        Me.乘务员匹配ToolStripMenuItem.Name = "乘务员匹配ToolStripMenuItem"
        Me.乘务员匹配ToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.乘务员匹配ToolStripMenuItem.Text = "轮转计划编制(&D)"
        '
        '乘务输出ToolStripMenuItem
        '
        Me.乘务输出ToolStripMenuItem.Name = "乘务输出ToolStripMenuItem"
        Me.乘务输出ToolStripMenuItem.Size = New System.Drawing.Size(110, 21)
        Me.乘务输出ToolStripMenuItem.Text = "乘务计划发布(&O)"
        '
        '乘务绩效管理ToolStripMenuItem
        '
        Me.乘务绩效管理ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.乘务员绩效管理ToolStripMenuItem, Me.年度星级成绩录入XToolStripMenuItem})
        Me.乘务绩效管理ToolStripMenuItem.Enabled = False
        Me.乘务绩效管理ToolStripMenuItem.Name = "乘务绩效管理ToolStripMenuItem"
        Me.乘务绩效管理ToolStripMenuItem.Size = New System.Drawing.Size(105, 21)
        Me.乘务绩效管理ToolStripMenuItem.Text = "乘务绩效管理(&J)"
        '
        '乘务员绩效管理ToolStripMenuItem
        '
        Me.乘务员绩效管理ToolStripMenuItem.Name = "乘务员绩效管理ToolStripMenuItem"
        Me.乘务员绩效管理ToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.乘务员绩效管理ToolStripMenuItem.Text = "乘务员绩效(&D)"
        '
        '年度星级成绩录入XToolStripMenuItem
        '
        Me.年度星级成绩录入XToolStripMenuItem.Name = "年度星级成绩录入XToolStripMenuItem"
        Me.年度星级成绩录入XToolStripMenuItem.Size = New System.Drawing.Size(164, 22)
        Me.年度星级成绩录入XToolStripMenuItem.Text = "年度星级评定(&X)"
        '
        '检查更新ToolStripMenuItem
        '
        Me.检查更新ToolStripMenuItem.Name = "检查更新ToolStripMenuItem"
        Me.检查更新ToolStripMenuItem.Size = New System.Drawing.Size(68, 21)
        Me.检查更新ToolStripMenuItem.Text = "检查更新"
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(55, 17)
        Me.ToolStripStatusLabel1.Text = "当前状态"
        '
        'LinkLabel3
        '
        Me.LinkLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel3.Location = New System.Drawing.Point(6, 17)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(75, 21)
        Me.LinkLabel3.TabIndex = 2
        Me.LinkLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LinkLabel8
        '
        Me.LinkLabel8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel8.Location = New System.Drawing.Point(6, 48)
        Me.LinkLabel8.Name = "LinkLabel8"
        Me.LinkLabel8.Size = New System.Drawing.Size(75, 21)
        Me.LinkLabel8.TabIndex = 4
        Me.LinkLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LinkLabel9
        '
        Me.LinkLabel9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LinkLabel9.Location = New System.Drawing.Point(6, 86)
        Me.LinkLabel9.Name = "LinkLabel9"
        Me.LinkLabel9.Size = New System.Drawing.Size(75, 21)
        Me.LinkLabel9.TabIndex = 5
        Me.LinkLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlMap
        '
        Me.pnlMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pnlMap.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMap.Location = New System.Drawing.Point(0, 0)
        Me.pnlMap.Name = "pnlMap"
        Me.pnlMap.Size = New System.Drawing.Size(556, 475)
        Me.pnlMap.TabIndex = 8
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TreeView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.AutoScroll = True
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.Silver
        Me.SplitContainer1.Panel2.Controls.Add(Me.pnlMap)
        Me.SplitContainer1.Size = New System.Drawing.Size(762, 477)
        Me.SplitContainer1.SplitterDistance = 200
        Me.SplitContainer1.TabIndex = 8
        '
        'TreeView1
        '
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        Me.TreeView1.Size = New System.Drawing.Size(198, 475)
        Me.TreeView1.TabIndex = 1
        '
        'frmODSMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(762, 502)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmODSMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CS 主界面-上海地铁 V2.0"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout

End Sub
    Friend WithEvents 处置方案ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 内容CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 索引IToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents LinkLabel3 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel8 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel9 As System.Windows.Forms.LinkLabel
    Friend WithEvents 乘务计划ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 乘务计划ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 乘务员匹配ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 文件FToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 退出EToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 乘务输出ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 乘务信息管理DToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 运行图管理ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Ts运行图浏览 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ts运行图管理 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 运行图导入ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 乘务员信息管理ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 乘务绩效管理ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 乘务员绩效管理ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 基本参数设定ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 交路距离参数ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 年度星级成绩录入XToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 用户管理UToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 区域参数ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 更换用户CToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 人数测算ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents 检查更新ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlMap As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView

End Class
