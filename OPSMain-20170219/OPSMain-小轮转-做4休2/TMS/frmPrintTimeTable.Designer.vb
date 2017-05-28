<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmPrintTimeTable
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
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.llbATS = New System.Windows.Forms.LinkLabel
        Me.btnOutFenLei = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkTimeFormate = New System.Windows.Forms.CheckBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.cmbPaiXu = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtCols = New System.Windows.Forms.NumericUpDown
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbQuDuanName = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnSelectAll = New System.Windows.Forms.Button
        Me.lstCheDi = New System.Windows.Forms.CheckedListBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.optEnglish = New System.Windows.Forms.RadioButton
        Me.optChinese = New System.Windows.Forms.RadioButton
        Me.txtCheDiSheetCol = New System.Windows.Forms.NumericUpDown
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnOutCheDiLine2 = New System.Windows.Forms.Button
        Me.btnCreatCheDiLine1 = New System.Windows.Forms.Button
        Me.btnNotSelectAll = New System.Windows.Forms.Button
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.chkTainNumFormat = New System.Windows.Forms.CheckBox
        Me.txtTime = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtTitle = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.lstSta = New System.Windows.Forms.ListBox
        Me.cmdDeleAll = New System.Windows.Forms.Button
        Me.cmdDeleOne = New System.Windows.Forms.Button
        Me.cmdAddAll = New System.Windows.Forms.Button
        Me.cmdAddOne = New System.Windows.Forms.Button
        Me.lstBei = New System.Windows.Forms.ListBox
        Me.btnStaSKB = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.proBar = New System.Windows.Forms.ProgressBar
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.txtCols, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.txtCheDiSheetCol, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(391, 381)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.llbATS)
        Me.TabPage1.Controls.Add(Me.btnOutFenLei)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(383, 356)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "以车次方式输出"
        '
        'llbATS
        '
        Me.llbATS.AutoSize = True
        Me.llbATS.Location = New System.Drawing.Point(81, 331)
        Me.llbATS.Name = "llbATS"
        Me.llbATS.Size = New System.Drawing.Size(143, 12)
        Me.llbATS.TabIndex = 6
        Me.llbATS.TabStop = True
        Me.llbATS.Text = "上海一号线新ATS格式输出"
        '
        'btnOutFenLei
        '
        Me.btnOutFenLei.Location = New System.Drawing.Point(266, 326)
        Me.btnOutFenLei.Name = "btnOutFenLei"
        Me.btnOutFenLei.Size = New System.Drawing.Size(102, 23)
        Me.btnOutFenLei.TabIndex = 4
        Me.btnOutFenLei.Text = "开始输出"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkTimeFormate)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cmbPaiXu)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtCols)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbQuDuanName)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(359, 305)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "参数设置"
        '
        'chkTimeFormate
        '
        Me.chkTimeFormate.AutoSize = True
        Me.chkTimeFormate.Location = New System.Drawing.Point(41, 267)
        Me.chkTimeFormate.Name = "chkTimeFormate"
        Me.chkTimeFormate.Size = New System.Drawing.Size(276, 16)
        Me.chkTimeFormate.TabIndex = 9
        Me.chkTimeFormate.Text = "设置单元格为时间格式(该功能会影响输出速度)"
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(39, 218)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(277, 35)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "如果以车站名的发点排序，请确保所有的列车都通过该车站。"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(183, 184)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 12)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "站的发点进行排序"
        '
        'cmbPaiXu
        '
        Me.cmbPaiXu.FormattingEnabled = True
        Me.cmbPaiXu.Location = New System.Drawing.Point(74, 176)
        Me.cmbPaiXu.Name = "cmbPaiXu"
        Me.cmbPaiXu.Size = New System.Drawing.Size(94, 20)
        Me.cmbPaiXu.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(39, 179)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(17, 12)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "以"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(35, 149)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 12)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "排序方式:"
        '
        'txtCols
        '
        Me.txtCols.Location = New System.Drawing.Point(182, 81)
        Me.txtCols.Maximum = New Decimal(New Integer() {250, 0, 0, 0})
        Me.txtCols.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtCols.Name = "txtCols"
        Me.txtCols.Size = New System.Drawing.Size(131, 21)
        Me.txtCols.TabIndex = 3
        Me.txtCols.Value = New Decimal(New Integer() {200, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(36, 86)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(119, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "每一个工作表的列数:"
        '
        'cmbQuDuanName
        '
        Me.cmbQuDuanName.FormattingEnabled = True
        Me.cmbQuDuanName.Location = New System.Drawing.Point(124, 31)
        Me.cmbQuDuanName.Name = "cmbQuDuanName"
        Me.cmbQuDuanName.Size = New System.Drawing.Size(189, 20)
        Me.cmbQuDuanName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(36, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "选择输出区段:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox3)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(383, 356)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "以车底方式输出"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnSelectAll)
        Me.GroupBox3.Controls.Add(Me.lstCheDi)
        Me.GroupBox3.Controls.Add(Me.GroupBox2)
        Me.GroupBox3.Controls.Add(Me.txtCheDiSheetCol)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.btnOutCheDiLine2)
        Me.GroupBox3.Controls.Add(Me.btnCreatCheDiLine1)
        Me.GroupBox3.Controls.Add(Me.btnNotSelectAll)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 13)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(371, 335)
        Me.GroupBox3.TabIndex = 22
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "输出"
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Location = New System.Drawing.Point(31, 267)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(120, 23)
        Me.btnSelectAll.TabIndex = 13
        Me.btnSelectAll.Text = "全选"
        '
        'lstCheDi
        '
        Me.lstCheDi.FormattingEnabled = True
        Me.lstCheDi.Location = New System.Drawing.Point(31, 40)
        Me.lstCheDi.Name = "lstCheDi"
        Me.lstCheDi.Size = New System.Drawing.Size(120, 212)
        Me.lstCheDi.TabIndex = 18
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.optEnglish)
        Me.GroupBox2.Controls.Add(Me.optChinese)
        Me.GroupBox2.Location = New System.Drawing.Point(199, 186)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(140, 66)
        Me.GroupBox2.TabIndex = 16
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "站名显示"
        '
        'optEnglish
        '
        Me.optEnglish.AutoSize = True
        Me.optEnglish.Location = New System.Drawing.Point(22, 42)
        Me.optEnglish.Name = "optEnglish"
        Me.optEnglish.Size = New System.Drawing.Size(71, 16)
        Me.optEnglish.TabIndex = 16
        Me.optEnglish.Text = "显示英文"
        '
        'optChinese
        '
        Me.optChinese.AutoSize = True
        Me.optChinese.Checked = True
        Me.optChinese.Location = New System.Drawing.Point(22, 20)
        Me.optChinese.Name = "optChinese"
        Me.optChinese.Size = New System.Drawing.Size(71, 16)
        Me.optChinese.TabIndex = 15
        Me.optChinese.TabStop = True
        Me.optChinese.Text = "显示中文"
        '
        'txtCheDiSheetCol
        '
        Me.txtCheDiSheetCol.Location = New System.Drawing.Point(258, 71)
        Me.txtCheDiSheetCol.Maximum = New Decimal(New Integer() {25, 0, 0, 0})
        Me.txtCheDiSheetCol.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtCheDiSheetCol.Name = "txtCheDiSheetCol"
        Me.txtCheDiSheetCol.Size = New System.Drawing.Size(81, 21)
        Me.txtCheDiSheetCol.TabIndex = 12
        Me.txtCheDiSheetCol.Value = New Decimal(New Integer() {20, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.ForeColor = System.Drawing.Color.Red
        Me.Label7.Location = New System.Drawing.Point(29, 11)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(308, 26)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "该方式是以固定的格式输出，列车顺序以列车在车库的发点排序。"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(184, 46)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(155, 12)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "每一个工作表输出的车底数:"
        '
        'btnOutCheDiLine2
        '
        Me.btnOutCheDiLine2.Location = New System.Drawing.Point(208, 267)
        Me.btnOutCheDiLine2.Name = "btnOutCheDiLine2"
        Me.btnOutCheDiLine2.Size = New System.Drawing.Size(140, 23)
        Me.btnOutCheDiLine2.TabIndex = 19
        Me.btnOutCheDiLine2.Text = "格式一(上海原二号线)"
        '
        'btnCreatCheDiLine1
        '
        Me.btnCreatCheDiLine1.Location = New System.Drawing.Point(208, 296)
        Me.btnCreatCheDiLine1.Name = "btnCreatCheDiLine1"
        Me.btnCreatCheDiLine1.Size = New System.Drawing.Size(140, 23)
        Me.btnCreatCheDiLine1.TabIndex = 20
        Me.btnCreatCheDiLine1.Text = "规范格式输出"
        '
        'btnNotSelectAll
        '
        Me.btnNotSelectAll.Location = New System.Drawing.Point(31, 296)
        Me.btnNotSelectAll.Name = "btnNotSelectAll"
        Me.btnNotSelectAll.Size = New System.Drawing.Size(120, 23)
        Me.btnNotSelectAll.TabIndex = 14
        Me.btnNotSelectAll.Text = "取消全选"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.GroupBox4)
        Me.TabPage3.Controls.Add(Me.btnStaSKB)
        Me.TabPage3.Location = New System.Drawing.Point(4, 21)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(383, 356)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "车站时刻表输出"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.chkTainNumFormat)
        Me.GroupBox4.Controls.Add(Me.txtTime)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Controls.Add(Me.txtTitle)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.lstSta)
        Me.GroupBox4.Controls.Add(Me.cmdDeleAll)
        Me.GroupBox4.Controls.Add(Me.cmdDeleOne)
        Me.GroupBox4.Controls.Add(Me.cmdAddAll)
        Me.GroupBox4.Controls.Add(Me.cmdAddOne)
        Me.GroupBox4.Controls.Add(Me.lstBei)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 3)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(365, 320)
        Me.GroupBox4.TabIndex = 25
        Me.GroupBox4.TabStop = False
        '
        'chkTainNumFormat
        '
        Me.chkTainNumFormat.AutoSize = True
        Me.chkTainNumFormat.Location = New System.Drawing.Point(22, 290)
        Me.chkTainNumFormat.Name = "chkTainNumFormat"
        Me.chkTainNumFormat.Size = New System.Drawing.Size(144, 16)
        Me.chkTainNumFormat.TabIndex = 22
        Me.chkTainNumFormat.Text = "车次以三位数格式输出"
        Me.chkTainNumFormat.Visible = False
        '
        'txtTime
        '
        Me.txtTime.Location = New System.Drawing.Point(61, 256)
        Me.txtTime.Name = "txtTime"
        Me.txtTime.Size = New System.Drawing.Size(286, 21)
        Me.txtTime.TabIndex = 21
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(20, 259)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(35, 12)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "日期:"
        '
        'txtTitle
        '
        Me.txtTitle.Location = New System.Drawing.Point(61, 229)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(286, 21)
        Me.txtTitle.TabIndex = 19
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(20, 232)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 12)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "标题:"
        '
        'lstSta
        '
        Me.lstSta.FormattingEnabled = True
        Me.lstSta.HorizontalScrollbar = True
        Me.lstSta.ItemHeight = 12
        Me.lstSta.Location = New System.Drawing.Point(213, 17)
        Me.lstSta.Name = "lstSta"
        Me.lstSta.Size = New System.Drawing.Size(134, 184)
        Me.lstSta.TabIndex = 17
        '
        'cmdDeleAll
        '
        Me.cmdDeleAll.Location = New System.Drawing.Point(161, 152)
        Me.cmdDeleAll.Name = "cmdDeleAll"
        Me.cmdDeleAll.Size = New System.Drawing.Size(39, 23)
        Me.cmdDeleAll.TabIndex = 16
        Me.cmdDeleAll.Text = "<<<"
        '
        'cmdDeleOne
        '
        Me.cmdDeleOne.Location = New System.Drawing.Point(161, 113)
        Me.cmdDeleOne.Name = "cmdDeleOne"
        Me.cmdDeleOne.Size = New System.Drawing.Size(39, 23)
        Me.cmdDeleOne.TabIndex = 15
        Me.cmdDeleOne.Text = "<-"
        '
        'cmdAddAll
        '
        Me.cmdAddAll.Location = New System.Drawing.Point(161, 74)
        Me.cmdAddAll.Name = "cmdAddAll"
        Me.cmdAddAll.Size = New System.Drawing.Size(39, 23)
        Me.cmdAddAll.TabIndex = 14
        Me.cmdAddAll.Text = ">>>"
        '
        'cmdAddOne
        '
        Me.cmdAddOne.Location = New System.Drawing.Point(161, 36)
        Me.cmdAddOne.Name = "cmdAddOne"
        Me.cmdAddOne.Size = New System.Drawing.Size(39, 23)
        Me.cmdAddOne.TabIndex = 13
        Me.cmdAddOne.Text = "->"
        '
        'lstBei
        '
        Me.lstBei.FormattingEnabled = True
        Me.lstBei.HorizontalScrollbar = True
        Me.lstBei.ItemHeight = 12
        Me.lstBei.Location = New System.Drawing.Point(22, 17)
        Me.lstBei.Name = "lstBei"
        Me.lstBei.Size = New System.Drawing.Size(130, 184)
        Me.lstBei.TabIndex = 12
        '
        'btnStaSKB
        '
        Me.btnStaSKB.Location = New System.Drawing.Point(257, 330)
        Me.btnStaSKB.Name = "btnStaSKB"
        Me.btnStaSKB.Size = New System.Drawing.Size(120, 23)
        Me.btnStaSKB.TabIndex = 24
        Me.btnStaSKB.Text = "开始输出"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(312, 412)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(91, 23)
        Me.btnExit.TabIndex = 1
        Me.btnExit.Text = "退出(&E)"
        '
        'proBar
        '
        Me.proBar.Location = New System.Drawing.Point(12, 395)
        Me.proBar.Name = "proBar"
        Me.proBar.Size = New System.Drawing.Size(391, 11)
        Me.proBar.TabIndex = 2
        Me.proBar.Visible = False
        '
        'frmPrintTimeTable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 443)
        Me.Controls.Add(Me.proBar)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintTimeTable"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "输出时刻表"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.txtCols, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.txtCheDiSheetCol, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents proBar As System.Windows.Forms.ProgressBar
    Friend WithEvents cmbQuDuanName As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbPaiXu As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtCols As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnNotSelectAll As System.Windows.Forms.Button
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents txtCheDiSheetCol As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optEnglish As System.Windows.Forms.RadioButton
    Friend WithEvents optChinese As System.Windows.Forms.RadioButton
    Friend WithEvents txtTime As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lstSta As System.Windows.Forms.ListBox
    Friend WithEvents cmdDeleAll As System.Windows.Forms.Button
    Friend WithEvents cmdDeleOne As System.Windows.Forms.Button
    Friend WithEvents cmdAddAll As System.Windows.Forms.Button
    Friend WithEvents cmdAddOne As System.Windows.Forms.Button
    Friend WithEvents lstBei As System.Windows.Forms.ListBox
    Friend WithEvents chkTainNumFormat As System.Windows.Forms.CheckBox
    Friend WithEvents lstCheDi As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnOutCheDiLine2 As System.Windows.Forms.Button
    Friend WithEvents btnCreatCheDiLine1 As System.Windows.Forms.Button
    Friend WithEvents btnStaSKB As System.Windows.Forms.Button
    Friend WithEvents btnOutFenLei As System.Windows.Forms.Button
    Friend WithEvents chkTimeFormate As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents llbATS As System.Windows.Forms.LinkLabel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
End Class
