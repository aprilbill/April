<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDrawCircleTrainDiagram
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.ProBar = New System.Windows.Forms.ToolStripProgressBar
        Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel
        Me.cmbEditTime = New System.Windows.Forms.ComboBox
        Me.grdTime = New System.Windows.Forms.DataGridView
        Me.时间段1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.起始时间 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.终止时间 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.运行周期 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.运行标尺 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.停站标尺 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.始发折返 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.终到折返 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.下行运行 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.下行停站 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.上行运行 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.上行停站 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.发车间隔 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.车底数量 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.间隔一 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.数量一 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.间隔二 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.数量二 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkAutoAddChuKu = New System.Windows.Forms.CheckBox
        Me.chkAutoAddRuKu = New System.Windows.Forms.CheckBox
        Me.cmdShowFangA = New System.Windows.Forms.Button
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.btnExit = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnConDraw = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbTrainJLStyle = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnOutExcel = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.labZheFanTime = New System.Windows.Forms.Label
        Me.cmbPuHuaStyle = New System.Windows.Forms.ComboBox
        Me.btnSaveFangAn = New System.Windows.Forms.Button
        CType(Me.grdTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ProBar
        '
        Me.ProBar.Name = "ProBar"
        Me.ProBar.Size = New System.Drawing.Size(450, 17)
        Me.ProBar.Visible = False
        '
        'StatusLabel
        '
        Me.StatusLabel.AutoSize = False
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(150, 18)
        Me.StatusLabel.Text = "铺画运行图"
        '
        'cmbEditTime
        '
        Me.cmbEditTime.FormattingEnabled = True
        Me.cmbEditTime.Location = New System.Drawing.Point(452, 132)
        Me.cmbEditTime.Name = "cmbEditTime"
        Me.cmbEditTime.Size = New System.Drawing.Size(88, 20)
        Me.cmbEditTime.TabIndex = 14
        Me.cmbEditTime.Visible = False
        '
        'grdTime
        '
        Me.grdTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdTime.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.时间段1, Me.起始时间, Me.终止时间, Me.运行周期, Me.运行标尺, Me.停站标尺, Me.始发折返, Me.终到折返, Me.下行运行, Me.下行停站, Me.上行运行, Me.上行停站, Me.发车间隔, Me.车底数量, Me.间隔一, Me.数量一, Me.间隔二, Me.数量二})
        Me.grdTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdTime.Location = New System.Drawing.Point(3, 17)
        Me.grdTime.MultiSelect = False
        Me.grdTime.Name = "grdTime"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.FormatProvider = New System.Globalization.CultureInfo("zh-CN")
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTime.RowHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdTime.RowHeadersWidth = 30
        Me.grdTime.RowTemplate.Height = 23
        Me.grdTime.Size = New System.Drawing.Size(804, 274)
        Me.grdTime.TabIndex = 13
        Me.grdTime.Text = "DataGridView1"
        '
        '时间段1
        '
        Me.时间段1.HeaderText = "时间段"
        Me.时间段1.Name = "时间段1"
        Me.时间段1.Width = 50
        '
        '起始时间
        '
        Me.起始时间.HeaderText = "起始时间"
        Me.起始时间.Name = "起始时间"
        Me.起始时间.Width = 60
        '
        '终止时间
        '
        Me.终止时间.HeaderText = "终止时间"
        Me.终止时间.Name = "终止时间"
        Me.终止时间.Width = 60
        '
        '运行周期
        '
        Me.运行周期.HeaderText = "运行周期"
        Me.运行周期.Name = "运行周期"
        Me.运行周期.Width = 60
        '
        '运行标尺
        '
        Me.运行标尺.HeaderText = "运行标尺"
        Me.运行标尺.Name = "运行标尺"
        Me.运行标尺.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.运行标尺.Width = 80
        '
        '停站标尺
        '
        Me.停站标尺.HeaderText = "停站标尺"
        Me.停站标尺.Name = "停站标尺"
        Me.停站标尺.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.停站标尺.Width = 80
        '
        '始发折返
        '
        Me.始发折返.HeaderText = "始发折返"
        Me.始发折返.Name = "始发折返"
        Me.始发折返.Width = 60
        '
        '终到折返
        '
        Me.终到折返.HeaderText = "终到折返"
        Me.终到折返.Name = "终到折返"
        Me.终到折返.Width = 60
        '
        '下行运行
        '
        Me.下行运行.HeaderText = "下行运行"
        Me.下行运行.Name = "下行运行"
        Me.下行运行.Width = 60
        '
        '下行停站
        '
        Me.下行停站.HeaderText = "下行停站"
        Me.下行停站.Name = "下行停站"
        Me.下行停站.Width = 60
        '
        '上行运行
        '
        Me.上行运行.HeaderText = "上行运行"
        Me.上行运行.Name = "上行运行"
        Me.上行运行.Width = 60
        '
        '上行停站
        '
        Me.上行停站.HeaderText = "上行停站"
        Me.上行停站.Name = "上行停站"
        Me.上行停站.Width = 60
        '
        '发车间隔
        '
        Me.发车间隔.HeaderText = "发车间隔"
        Me.发车间隔.Name = "发车间隔"
        Me.发车间隔.Width = 60
        '
        '车底数量
        '
        Me.车底数量.HeaderText = "车底数量"
        Me.车底数量.Name = "车底数量"
        Me.车底数量.Width = 60
        '
        '间隔一
        '
        Me.间隔一.HeaderText = "间隔一"
        Me.间隔一.Name = "间隔一"
        Me.间隔一.Width = 60
        '
        '数量一
        '
        Me.数量一.HeaderText = "数量一"
        Me.数量一.Name = "数量一"
        Me.数量一.Width = 60
        '
        '间隔二
        '
        Me.间隔二.HeaderText = "间隔二"
        Me.间隔二.Name = "间隔二"
        Me.间隔二.Width = 60
        '
        '数量二
        '
        Me.数量二.HeaderText = "数量二"
        Me.数量二.Name = "数量二"
        Me.数量二.Width = 60
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbEditTime)
        Me.GroupBox1.Controls.Add(Me.grdTime)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 70)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(810, 294)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "间隔安排"
        '
        'chkAutoAddChuKu
        '
        Me.chkAutoAddChuKu.AutoSize = True
        Me.chkAutoAddChuKu.Location = New System.Drawing.Point(12, 17)
        Me.chkAutoAddChuKu.Name = "chkAutoAddChuKu"
        Me.chkAutoAddChuKu.Size = New System.Drawing.Size(108, 16)
        Me.chkAutoAddChuKu.TabIndex = 19
        Me.chkAutoAddChuKu.Text = "自动铺画出库车"
        Me.chkAutoAddChuKu.Visible = False
        '
        'chkAutoAddRuKu
        '
        Me.chkAutoAddRuKu.AutoSize = True
        Me.chkAutoAddRuKu.Location = New System.Drawing.Point(12, 51)
        Me.chkAutoAddRuKu.Name = "chkAutoAddRuKu"
        Me.chkAutoAddRuKu.Size = New System.Drawing.Size(108, 16)
        Me.chkAutoAddRuKu.TabIndex = 20
        Me.chkAutoAddRuKu.Text = "自动铺画入库车"
        Me.chkAutoAddRuKu.Visible = False
        '
        'cmdShowFangA
        '
        Me.cmdShowFangA.Location = New System.Drawing.Point(12, 92)
        Me.cmdShowFangA.Name = "cmdShowFangA"
        Me.cmdShowFangA.Size = New System.Drawing.Size(130, 28)
        Me.cmdShowFangA.TabIndex = 11
        Me.cmdShowFangA.Text = "铺画单交路运行图"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLabel, Me.ProBar})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 132)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(810, 23)
        Me.StatusStrip1.TabIndex = 13
        Me.StatusStrip1.Text = "StatusBar1"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(701, 92)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(97, 23)
        Me.btnExit.TabIndex = 12
        Me.btnExit.Text = "退出(&E)"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkAutoAddRuKu)
        Me.Panel2.Controls.Add(Me.chkAutoAddChuKu)
        Me.Panel2.Controls.Add(Me.btnExit)
        Me.Panel2.Controls.Add(Me.btnConDraw)
        Me.Panel2.Controls.Add(Me.cmdShowFangA)
        Me.Panel2.Controls.Add(Me.StatusStrip1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 364)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(810, 155)
        Me.Panel2.TabIndex = 10
        '
        'btnConDraw
        '
        Me.btnConDraw.Location = New System.Drawing.Point(163, 92)
        Me.btnConDraw.Name = "btnConDraw"
        Me.btnConDraw.Size = New System.Drawing.Size(175, 28)
        Me.btnConDraw.TabIndex = 11
        Me.btnConDraw.Text = "在前一基础上铺画运行图"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "选择铺画类型:"
        '
        'cmbTrainJLStyle
        '
        Me.cmbTrainJLStyle.FormattingEnabled = True
        Me.cmbTrainJLStyle.Location = New System.Drawing.Point(95, 12)
        Me.cmbTrainJLStyle.Name = "cmbTrainJLStyle"
        Me.cmbTrainJLStyle.Size = New System.Drawing.Size(150, 20)
        Me.cmbTrainJLStyle.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "选择交路:"
        '
        'btnOutExcel
        '
        Me.btnOutExcel.Location = New System.Drawing.Point(661, 37)
        Me.btnOutExcel.Name = "btnOutExcel"
        Me.btnOutExcel.Size = New System.Drawing.Size(137, 23)
        Me.btnOutExcel.TabIndex = 16
        Me.btnOutExcel.Text = "方案导出至EXCEL(&O)"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnOutExcel)
        Me.Panel1.Controls.Add(Me.labZheFanTime)
        Me.Panel1.Controls.Add(Me.cmbPuHuaStyle)
        Me.Panel1.Controls.Add(Me.btnSaveFangAn)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cmbTrainJLStyle)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(810, 70)
        Me.Panel1.TabIndex = 9
        '
        'labZheFanTime
        '
        Me.labZheFanTime.AutoSize = True
        Me.labZheFanTime.Location = New System.Drawing.Point(261, 15)
        Me.labZheFanTime.Name = "labZheFanTime"
        Me.labZheFanTime.Size = New System.Drawing.Size(77, 12)
        Me.labZheFanTime.TabIndex = 4
        Me.labZheFanTime.Text = "最小折返时间"
        '
        'cmbPuHuaStyle
        '
        Me.cmbPuHuaStyle.FormattingEnabled = True
        Me.cmbPuHuaStyle.Location = New System.Drawing.Point(95, 39)
        Me.cmbPuHuaStyle.Name = "cmbPuHuaStyle"
        Me.cmbPuHuaStyle.Size = New System.Drawing.Size(150, 20)
        Me.cmbPuHuaStyle.TabIndex = 3
        '
        'btnSaveFangAn
        '
        Me.btnSaveFangAn.Location = New System.Drawing.Point(515, 37)
        Me.btnSaveFangAn.Name = "btnSaveFangAn"
        Me.btnSaveFangAn.Size = New System.Drawing.Size(140, 23)
        Me.btnSaveFangAn.TabIndex = 15
        Me.btnSaveFangAn.Text = "保存铺画方案"
        '
        'frmDrawCircleTrainDiagram
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(810, 519)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmDrawCircleTrainDiagram"
        Me.Text = "环形交路运行图铺画"
        CType(Me.grdTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ProBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents StatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cmbEditTime As System.Windows.Forms.ComboBox
    Friend WithEvents grdTime As System.Windows.Forms.DataGridView
    Friend WithEvents 时间段1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 起始时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 终止时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 运行周期 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 运行标尺 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 停站标尺 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 始发折返 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 终到折返 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 下行运行 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 下行停站 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 上行运行 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 上行停站 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 发车间隔 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 车底数量 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 间隔一 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 数量一 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 间隔二 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 数量二 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkAutoAddChuKu As System.Windows.Forms.CheckBox
    Friend WithEvents chkAutoAddRuKu As System.Windows.Forms.CheckBox
    Friend WithEvents cmdShowFangA As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbTrainJLStyle As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOutExcel As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents labZheFanTime As System.Windows.Forms.Label
    Friend WithEvents cmbPuHuaStyle As System.Windows.Forms.ComboBox
    Friend WithEvents btnSaveFangAn As System.Windows.Forms.Button
    Friend WithEvents btnConDraw As System.Windows.Forms.Button
End Class
