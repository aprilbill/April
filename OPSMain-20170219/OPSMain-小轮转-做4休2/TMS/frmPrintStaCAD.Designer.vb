<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintStaCAD
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
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.txtSmallTitle = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.chkShortLine = New System.Windows.Forms.CheckBox
        Me.chkCrossing = New System.Windows.Forms.CheckBox
        Me.chkGudao = New System.Windows.Forms.CheckBox
        Me.chkTackNum = New System.Windows.Forms.CheckBox
        Me.btnPageSet = New System.Windows.Forms.Button
        Me.prgPrint = New System.Windows.Forms.ProgressBar
        Me.Button1 = New System.Windows.Forms.Button
        Me.printDocDiagram = New System.Drawing.Printing.PrintDocument
        Me.btnPrint = New System.Windows.Forms.Button
        Me.btnPreView = New System.Windows.Forms.Button
        Me.lstSta = New System.Windows.Forms.ListBox
        Me.cmdDeleAll = New System.Windows.Forms.Button
        Me.cmdAddAll = New System.Windows.Forms.Button
        Me.cmdAddOne = New System.Windows.Forms.Button
        Me.cmdDeleOne = New System.Windows.Forms.Button
        Me.lstBei = New System.Windows.Forms.ListBox
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtLineSmalltitle = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.NumLineWidth = New System.Windows.Forms.NumericUpDown
        Me.Label16 = New System.Windows.Forms.Label
        Me.chkLineShortLine = New System.Windows.Forms.CheckBox
        Me.chkLineCross = New System.Windows.Forms.CheckBox
        Me.chkLineGuDao = New System.Windows.Forms.CheckBox
        Me.chkLineTrack = New System.Windows.Forms.CheckBox
        Me.btnLinePrint = New System.Windows.Forms.Button
        Me.btnLinePrintPreview = New System.Windows.Forms.Button
        Me.btnLinePrintSet = New System.Windows.Forms.Button
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.CheckBox2 = New System.Windows.Forms.CheckBox
        Me.CheckBox3 = New System.Windows.Forms.CheckBox
        Me.CheckBox4 = New System.Windows.Forms.CheckBox
        Me.PrintDocDiagramLine = New System.Drawing.Printing.PrintDocument
        Me.Label3 = New System.Windows.Forms.Label
        Me.labbackColor = New System.Windows.Forms.Label
        Me.btnTimeLineColor = New System.Windows.Forms.Button
        Me.labLineBackColor = New System.Windows.Forms.Label
        Me.btnLineBackColor = New System.Windows.Forms.Button
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.NumLineWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtSmallTitle)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Location = New System.Drawing.Point(15, 300)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(325, 74)
        Me.GroupBox5.TabIndex = 50
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "其它参数:"
        '
        'txtSmallTitle
        '
        Me.txtSmallTitle.Location = New System.Drawing.Point(69, 20)
        Me.txtSmallTitle.Name = "txtSmallTitle"
        Me.txtSmallTitle.Size = New System.Drawing.Size(246, 21)
        Me.txtSmallTitle.TabIndex = 9
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(16, 23)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(47, 12)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "小标题:"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.labbackColor)
        Me.GroupBox4.Controls.Add(Me.btnTimeLineColor)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.chkShortLine)
        Me.GroupBox4.Controls.Add(Me.chkCrossing)
        Me.GroupBox4.Controls.Add(Me.chkGudao)
        Me.GroupBox4.Controls.Add(Me.chkTackNum)
        Me.GroupBox4.Location = New System.Drawing.Point(15, 172)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(325, 122)
        Me.GroupBox4.TabIndex = 49
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "打印设置:"
        '
        'chkShortLine
        '
        Me.chkShortLine.AutoSize = True
        Me.chkShortLine.Location = New System.Drawing.Point(191, 39)
        Me.chkShortLine.Name = "chkShortLine"
        Me.chkShortLine.Size = New System.Drawing.Size(84, 16)
        Me.chkShortLine.TabIndex = 16
        Me.chkShortLine.Text = "显示分段线"
        Me.chkShortLine.UseVisualStyleBackColor = True
        '
        'chkCrossing
        '
        Me.chkCrossing.AutoSize = True
        Me.chkCrossing.Location = New System.Drawing.Point(12, 38)
        Me.chkCrossing.Name = "chkCrossing"
        Me.chkCrossing.Size = New System.Drawing.Size(96, 16)
        Me.chkCrossing.TabIndex = 15
        Me.chkCrossing.Text = "显示道岔编号"
        Me.chkCrossing.UseVisualStyleBackColor = True
        '
        'chkGudao
        '
        Me.chkGudao.AutoSize = True
        Me.chkGudao.Checked = True
        Me.chkGudao.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkGudao.Location = New System.Drawing.Point(12, 17)
        Me.chkGudao.Name = "chkGudao"
        Me.chkGudao.Size = New System.Drawing.Size(96, 16)
        Me.chkGudao.TabIndex = 14
        Me.chkGudao.Text = "显示股道编号"
        Me.chkGudao.UseVisualStyleBackColor = True
        '
        'chkTackNum
        '
        Me.chkTackNum.AutoSize = True
        Me.chkTackNum.Location = New System.Drawing.Point(191, 17)
        Me.chkTackNum.Name = "chkTackNum"
        Me.chkTackNum.Size = New System.Drawing.Size(96, 16)
        Me.chkTackNum.TabIndex = 13
        Me.chkTackNum.Text = "显示轨道编号"
        Me.chkTackNum.UseVisualStyleBackColor = True
        '
        'btnPageSet
        '
        Me.btnPageSet.Location = New System.Drawing.Point(15, 385)
        Me.btnPageSet.Name = "btnPageSet"
        Me.btnPageSet.Size = New System.Drawing.Size(88, 23)
        Me.btnPageSet.TabIndex = 51
        Me.btnPageSet.Text = "打印设置"
        '
        'prgPrint
        '
        Me.prgPrint.Location = New System.Drawing.Point(12, 457)
        Me.prgPrint.Name = "prgPrint"
        Me.prgPrint.Size = New System.Drawing.Size(360, 12)
        Me.prgPrint.TabIndex = 55
        Me.prgPrint.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(284, 475)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(88, 23)
        Me.Button1.TabIndex = 54
        Me.Button1.Text = "退出(&E)"
        '
        'printDocDiagram
        '
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(252, 385)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(88, 23)
        Me.btnPrint.TabIndex = 53
        Me.btnPrint.Text = "打印"
        '
        'btnPreView
        '
        Me.btnPreView.Location = New System.Drawing.Point(127, 385)
        Me.btnPreView.Name = "btnPreView"
        Me.btnPreView.Size = New System.Drawing.Size(88, 23)
        Me.btnPreView.TabIndex = 52
        Me.btnPreView.Text = "打印预览"
        '
        'lstSta
        '
        Me.lstSta.FormattingEnabled = True
        Me.lstSta.HorizontalScrollbar = True
        Me.lstSta.ItemHeight = 12
        Me.lstSta.Location = New System.Drawing.Point(206, 18)
        Me.lstSta.Name = "lstSta"
        Me.lstSta.Size = New System.Drawing.Size(134, 148)
        Me.lstSta.TabIndex = 48
        '
        'cmdDeleAll
        '
        Me.cmdDeleAll.Location = New System.Drawing.Point(151, 134)
        Me.cmdDeleAll.Name = "cmdDeleAll"
        Me.cmdDeleAll.Size = New System.Drawing.Size(39, 23)
        Me.cmdDeleAll.TabIndex = 47
        Me.cmdDeleAll.Text = "<<<"
        '
        'cmdAddAll
        '
        Me.cmdAddAll.Location = New System.Drawing.Point(151, 56)
        Me.cmdAddAll.Name = "cmdAddAll"
        Me.cmdAddAll.Size = New System.Drawing.Size(39, 23)
        Me.cmdAddAll.TabIndex = 45
        Me.cmdAddAll.Text = ">>>"
        '
        'cmdAddOne
        '
        Me.cmdAddOne.Location = New System.Drawing.Point(151, 18)
        Me.cmdAddOne.Name = "cmdAddOne"
        Me.cmdAddOne.Size = New System.Drawing.Size(39, 23)
        Me.cmdAddOne.TabIndex = 44
        Me.cmdAddOne.Text = "->"
        '
        'cmdDeleOne
        '
        Me.cmdDeleOne.Location = New System.Drawing.Point(151, 95)
        Me.cmdDeleOne.Name = "cmdDeleOne"
        Me.cmdDeleOne.Size = New System.Drawing.Size(39, 23)
        Me.cmdDeleOne.TabIndex = 46
        Me.cmdDeleOne.Text = "<-"
        '
        'lstBei
        '
        Me.lstBei.FormattingEnabled = True
        Me.lstBei.HorizontalScrollbar = True
        Me.lstBei.ItemHeight = 12
        Me.lstBei.Location = New System.Drawing.Point(15, 18)
        Me.lstBei.Name = "lstBei"
        Me.lstBei.Size = New System.Drawing.Size(130, 148)
        Me.lstBei.TabIndex = 43
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(360, 439)
        Me.TabControl1.TabIndex = 56
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lstBei)
        Me.TabPage1.Controls.Add(Me.GroupBox5)
        Me.TabPage1.Controls.Add(Me.btnPrint)
        Me.TabPage1.Controls.Add(Me.btnPreView)
        Me.TabPage1.Controls.Add(Me.cmdDeleOne)
        Me.TabPage1.Controls.Add(Me.btnPageSet)
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Controls.Add(Me.cmdAddOne)
        Me.TabPage1.Controls.Add(Me.cmdAddAll)
        Me.TabPage1.Controls.Add(Me.cmdDeleAll)
        Me.TabPage1.Controls.Add(Me.lstSta)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(352, 414)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "车站平面图"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.btnLinePrint)
        Me.TabPage2.Controls.Add(Me.btnLinePrintPreview)
        Me.TabPage2.Controls.Add(Me.btnLinePrintSet)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(352, 414)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "线路平面图"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtLineSmalltitle)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 279)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(325, 74)
        Me.GroupBox1.TabIndex = 58
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "其它参数:"
        '
        'txtLineSmalltitle
        '
        Me.txtLineSmalltitle.Location = New System.Drawing.Point(69, 20)
        Me.txtLineSmalltitle.Name = "txtLineSmalltitle"
        Me.txtLineSmalltitle.Size = New System.Drawing.Size(246, 21)
        Me.txtLineSmalltitle.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 12)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "小标题:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.labLineBackColor)
        Me.GroupBox2.Controls.Add(Me.btnLineBackColor)
        Me.GroupBox2.Controls.Add(Me.NumLineWidth)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.chkLineShortLine)
        Me.GroupBox2.Controls.Add(Me.chkLineCross)
        Me.GroupBox2.Controls.Add(Me.chkLineGuDao)
        Me.GroupBox2.Controls.Add(Me.chkLineTrack)
        Me.GroupBox2.Location = New System.Drawing.Point(14, 26)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(325, 176)
        Me.GroupBox2.TabIndex = 57
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "打印设置:"
        '
        'NumLineWidth
        '
        Me.NumLineWidth.Location = New System.Drawing.Point(52, 95)
        Me.NumLineWidth.Maximum = New Decimal(New Integer() {500, 0, 0, 0})
        Me.NumLineWidth.Name = "NumLineWidth"
        Me.NumLineWidth.Size = New System.Drawing.Size(56, 21)
        Me.NumLineWidth.TabIndex = 58
        Me.NumLineWidth.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(10, 99)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(35, 12)
        Me.Label16.TabIndex = 57
        Me.Label16.Text = "线宽:"
        '
        'chkLineShortLine
        '
        Me.chkLineShortLine.AutoSize = True
        Me.chkLineShortLine.Location = New System.Drawing.Point(191, 39)
        Me.chkLineShortLine.Name = "chkLineShortLine"
        Me.chkLineShortLine.Size = New System.Drawing.Size(84, 16)
        Me.chkLineShortLine.TabIndex = 16
        Me.chkLineShortLine.Text = "显示分段线"
        Me.chkLineShortLine.UseVisualStyleBackColor = True
        '
        'chkLineCross
        '
        Me.chkLineCross.AutoSize = True
        Me.chkLineCross.Location = New System.Drawing.Point(12, 38)
        Me.chkLineCross.Name = "chkLineCross"
        Me.chkLineCross.Size = New System.Drawing.Size(96, 16)
        Me.chkLineCross.TabIndex = 15
        Me.chkLineCross.Text = "显示道岔编号"
        Me.chkLineCross.UseVisualStyleBackColor = True
        '
        'chkLineGuDao
        '
        Me.chkLineGuDao.AutoSize = True
        Me.chkLineGuDao.Checked = True
        Me.chkLineGuDao.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLineGuDao.Location = New System.Drawing.Point(12, 17)
        Me.chkLineGuDao.Name = "chkLineGuDao"
        Me.chkLineGuDao.Size = New System.Drawing.Size(96, 16)
        Me.chkLineGuDao.TabIndex = 14
        Me.chkLineGuDao.Text = "显示股道编号"
        Me.chkLineGuDao.UseVisualStyleBackColor = True
        '
        'chkLineTrack
        '
        Me.chkLineTrack.AutoSize = True
        Me.chkLineTrack.Location = New System.Drawing.Point(191, 17)
        Me.chkLineTrack.Name = "chkLineTrack"
        Me.chkLineTrack.Size = New System.Drawing.Size(96, 16)
        Me.chkLineTrack.TabIndex = 13
        Me.chkLineTrack.Text = "显示轨道编号"
        Me.chkLineTrack.UseVisualStyleBackColor = True
        '
        'btnLinePrint
        '
        Me.btnLinePrint.Location = New System.Drawing.Point(249, 385)
        Me.btnLinePrint.Name = "btnLinePrint"
        Me.btnLinePrint.Size = New System.Drawing.Size(88, 23)
        Me.btnLinePrint.TabIndex = 56
        Me.btnLinePrint.Text = "打印"
        '
        'btnLinePrintPreview
        '
        Me.btnLinePrintPreview.Location = New System.Drawing.Point(129, 385)
        Me.btnLinePrintPreview.Name = "btnLinePrintPreview"
        Me.btnLinePrintPreview.Size = New System.Drawing.Size(88, 23)
        Me.btnLinePrintPreview.TabIndex = 55
        Me.btnLinePrintPreview.Text = "打印预览"
        '
        'btnLinePrintSet
        '
        Me.btnLinePrintSet.Location = New System.Drawing.Point(12, 385)
        Me.btnLinePrintSet.Name = "btnLinePrintSet"
        Me.btnLinePrintSet.Size = New System.Drawing.Size(88, 23)
        Me.btnLinePrintSet.TabIndex = 54
        Me.btnLinePrintSet.Text = "打印设置"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(69, 20)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(246, 21)
        Me.TextBox1.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(47, 12)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "小标题:"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(191, 39)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(84, 16)
        Me.CheckBox1.TabIndex = 16
        Me.CheckBox1.Text = "显示分段线"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(12, 38)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(96, 16)
        Me.CheckBox2.TabIndex = 15
        Me.CheckBox2.Text = "显示道岔编号"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Checked = True
        Me.CheckBox3.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox3.Location = New System.Drawing.Point(12, 17)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(96, 16)
        Me.CheckBox3.TabIndex = 14
        Me.CheckBox3.Text = "显示股道编号"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(191, 17)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(96, 16)
        Me.CheckBox4.TabIndex = 13
        Me.CheckBox4.Text = "显示轨道编号"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'PrintDocDiagramLine
        '
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 98)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 12)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "背景颜色:"
        '
        'labbackColor
        '
        Me.labbackColor.BackColor = System.Drawing.Color.White
        Me.labbackColor.ForeColor = System.Drawing.Color.White
        Me.labbackColor.Location = New System.Drawing.Point(75, 93)
        Me.labbackColor.Name = "labbackColor"
        Me.labbackColor.Size = New System.Drawing.Size(195, 22)
        Me.labbackColor.TabIndex = 20
        '
        'btnTimeLineColor
        '
        Me.btnTimeLineColor.Location = New System.Drawing.Point(276, 93)
        Me.btnTimeLineColor.Name = "btnTimeLineColor"
        Me.btnTimeLineColor.Size = New System.Drawing.Size(39, 23)
        Me.btnTimeLineColor.TabIndex = 19
        Me.btnTimeLineColor.Text = "..."
        '
        'labLineBackColor
        '
        Me.labLineBackColor.BackColor = System.Drawing.Color.White
        Me.labLineBackColor.ForeColor = System.Drawing.Color.White
        Me.labLineBackColor.Location = New System.Drawing.Point(80, 128)
        Me.labLineBackColor.Name = "labLineBackColor"
        Me.labLineBackColor.Size = New System.Drawing.Size(195, 22)
        Me.labLineBackColor.TabIndex = 59
        '
        'btnLineBackColor
        '
        Me.btnLineBackColor.Location = New System.Drawing.Point(281, 128)
        Me.btnLineBackColor.Name = "btnLineBackColor"
        Me.btnLineBackColor.Size = New System.Drawing.Size(39, 23)
        Me.btnLineBackColor.TabIndex = 58
        Me.btnLineBackColor.Text = "..."
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 133)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 12)
        Me.Label5.TabIndex = 57
        Me.Label5.Text = "背景颜色:"
        '
        'frmPrintStaCAD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(386, 507)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.prgPrint)
        Me.Controls.Add(Me.Button1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintStaCAD"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "打印车站平面图"
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.NumLineWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSmallTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnPageSet As System.Windows.Forms.Button
    Friend WithEvents prgPrint As System.Windows.Forms.ProgressBar
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents printDocDiagram As System.Drawing.Printing.PrintDocument
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnPreView As System.Windows.Forms.Button
    Friend WithEvents lstSta As System.Windows.Forms.ListBox
    Friend WithEvents cmdDeleAll As System.Windows.Forms.Button
    Friend WithEvents cmdAddAll As System.Windows.Forms.Button
    Friend WithEvents cmdAddOne As System.Windows.Forms.Button
    Friend WithEvents cmdDeleOne As System.Windows.Forms.Button
    Friend WithEvents lstBei As System.Windows.Forms.ListBox
    Friend WithEvents chkShortLine As System.Windows.Forms.CheckBox
    Friend WithEvents chkCrossing As System.Windows.Forms.CheckBox
    Friend WithEvents chkGudao As System.Windows.Forms.CheckBox
    Friend WithEvents chkTackNum As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtLineSmalltitle As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkLineShortLine As System.Windows.Forms.CheckBox
    Friend WithEvents chkLineCross As System.Windows.Forms.CheckBox
    Friend WithEvents chkLineGuDao As System.Windows.Forms.CheckBox
    Friend WithEvents chkLineTrack As System.Windows.Forms.CheckBox
    Friend WithEvents btnLinePrint As System.Windows.Forms.Button
    Friend WithEvents btnLinePrintPreview As System.Windows.Forms.Button
    Friend WithEvents btnLinePrintSet As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents PrintDocDiagramLine As System.Drawing.Printing.PrintDocument
    Friend WithEvents NumLineWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents labbackColor As System.Windows.Forms.Label
    Friend WithEvents btnTimeLineColor As System.Windows.Forms.Button
    Friend WithEvents labLineBackColor As System.Windows.Forms.Label
    Friend WithEvents btnLineBackColor As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
