<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmDiagramLineAndFontSet
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
        Me.btnSetLineStyleDefault = New System.Windows.Forms.Button
        Me.cmbDigramStyle = New System.Windows.Forms.ComboBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.picFontShow = New System.Windows.Forms.PictureBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnSelectFont = New System.Windows.Forms.Button
        Me.lstFont = New System.Windows.Forms.ListBox
        Me.labFontColor = New System.Windows.Forms.Label
        Me.btnFontColorSet = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lstStaLine = New System.Windows.Forms.ListBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.picTimeLineShow = New System.Windows.Forms.PictureBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.labTimeLineColor = New System.Windows.Forms.Label
        Me.btnTimeLineColor = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lstTimeLine = New System.Windows.Forms.ListBox
        Me.numLineWidth = New System.Windows.Forms.NumericUpDown
        Me.cmbLineStyle = New System.Windows.Forms.ComboBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.picCheCiFont = New System.Windows.Forms.PictureBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.btnCheCiFont = New System.Windows.Forms.Button
        Me.lstCheCiFont = New System.Windows.Forms.ListBox
        Me.labCheCiColor = New System.Windows.Forms.Label
        Me.btnCheCiColor = New System.Windows.Forms.Button
        Me.Label14 = New System.Windows.Forms.Label
        Me.btnSetTrainDefault = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnSetTrainLine = New System.Windows.Forms.Button
        Me.cmbTrainStyle = New System.Windows.Forms.ComboBox
        Me.picTrainLineView = New System.Windows.Forms.PictureBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.labTrainLineColor = New System.Windows.Forms.Label
        Me.btnTrainLineColor = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.numTrainLineWidth = New System.Windows.Forms.NumericUpDown
        Me.cmbTrainLineStyle = New System.Windows.Forms.ComboBox
        Me.lstTrainStyle = New System.Windows.Forms.ListBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.picFontShow, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.picTimeLineShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numLineWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.picCheCiFont, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.picTrainLineView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numTrainLineWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(366, 393)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btnSetLineStyleDefault)
        Me.TabPage1.Controls.Add(Me.cmbDigramStyle)
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.Label16)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(358, 368)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "底图"
        '
        'btnSetLineStyleDefault
        '
        Me.btnSetLineStyleDefault.Location = New System.Drawing.Point(15, 339)
        Me.btnSetLineStyleDefault.Name = "btnSetLineStyleDefault"
        Me.btnSetLineStyleDefault.Size = New System.Drawing.Size(123, 23)
        Me.btnSetLineStyleDefault.TabIndex = 15
        Me.btnSetLineStyleDefault.Text = "保存为默认值(&D)"
        '
        'cmbDigramStyle
        '
        Me.cmbDigramStyle.FormattingEnabled = True
        Me.cmbDigramStyle.Location = New System.Drawing.Point(89, 12)
        Me.cmbDigramStyle.Name = "cmbDigramStyle"
        Me.cmbDigramStyle.Size = New System.Drawing.Size(114, 20)
        Me.cmbDigramStyle.TabIndex = 14
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.picFontShow)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.btnSelectFont)
        Me.GroupBox3.Controls.Add(Me.lstFont)
        Me.GroupBox3.Controls.Add(Me.labFontColor)
        Me.GroupBox3.Controls.Add(Me.btnFontColorSet)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Location = New System.Drawing.Point(15, 206)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(329, 121)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "字体"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(114, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 12)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "字体:"
        '
        'picFontShow
        '
        Me.picFontShow.Location = New System.Drawing.Point(54, 79)
        Me.picFontShow.Name = "picFontShow"
        Me.picFontShow.Size = New System.Drawing.Size(255, 36)
        Me.picFontShow.TabIndex = 14
        Me.picFontShow.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 89)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 12)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "预览:"
        '
        'btnSelectFont
        '
        Me.btnSelectFont.Location = New System.Drawing.Point(180, 15)
        Me.btnSelectFont.Name = "btnSelectFont"
        Me.btnSelectFont.Size = New System.Drawing.Size(131, 22)
        Me.btnSelectFont.TabIndex = 9
        Me.btnSelectFont.Text = "选择字体"
        '
        'lstFont
        '
        Me.lstFont.FormattingEnabled = True
        Me.lstFont.ItemHeight = 12
        Me.lstFont.Items.AddRange(New Object() {"车站名称", "时间标注"})
        Me.lstFont.Location = New System.Drawing.Point(10, 20)
        Me.lstFont.Name = "lstFont"
        Me.lstFont.Size = New System.Drawing.Size(91, 52)
        Me.lstFont.TabIndex = 8
        '
        'labFontColor
        '
        Me.labFontColor.BackColor = System.Drawing.Color.Green
        Me.labFontColor.ForeColor = System.Drawing.Color.Green
        Me.labFontColor.Location = New System.Drawing.Point(178, 44)
        Me.labFontColor.Name = "labFontColor"
        Me.labFontColor.Size = New System.Drawing.Size(91, 22)
        Me.labFontColor.TabIndex = 7
        '
        'btnFontColorSet
        '
        Me.btnFontColorSet.Location = New System.Drawing.Point(270, 44)
        Me.btnFontColorSet.Name = "btnFontColorSet"
        Me.btnFontColorSet.Size = New System.Drawing.Size(39, 22)
        Me.btnFontColorSet.TabIndex = 6
        Me.btnFontColorSet.Text = "..."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(114, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 12)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "字体颜色:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lstStaLine)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.picTimeLineShow)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.labTimeLineColor)
        Me.GroupBox1.Controls.Add(Me.btnTimeLineColor)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lstTimeLine)
        Me.GroupBox1.Controls.Add(Me.numLineWidth)
        Me.GroupBox1.Controls.Add(Me.cmbLineStyle)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 38)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(329, 162)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "线"
        '
        'lstStaLine
        '
        Me.lstStaLine.FormattingEnabled = True
        Me.lstStaLine.ItemHeight = 12
        Me.lstStaLine.Items.AddRange(New Object() {"一般车站", "大站", "车场"})
        Me.lstStaLine.Location = New System.Drawing.Point(10, 114)
        Me.lstStaLine.Name = "lstStaLine"
        Me.lstStaLine.Size = New System.Drawing.Size(91, 40)
        Me.lstStaLine.TabIndex = 15
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(8, 15)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(47, 12)
        Me.Label18.TabIndex = 14
        Me.Label18.Text = "时间线:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(8, 99)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(71, 12)
        Me.Label17.TabIndex = 13
        Me.Label17.Text = "车站中心线:"
        '
        'picTimeLineShow
        '
        Me.picTimeLineShow.Location = New System.Drawing.Point(150, 121)
        Me.picTimeLineShow.Name = "picTimeLineShow"
        Me.picTimeLineShow.Size = New System.Drawing.Size(159, 26)
        Me.picTimeLineShow.TabIndex = 12
        Me.picTimeLineShow.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(107, 133)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 12)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "预览:"
        '
        'labTimeLineColor
        '
        Me.labTimeLineColor.BackColor = System.Drawing.Color.Green
        Me.labTimeLineColor.ForeColor = System.Drawing.Color.Green
        Me.labTimeLineColor.Location = New System.Drawing.Point(148, 89)
        Me.labTimeLineColor.Name = "labTimeLineColor"
        Me.labTimeLineColor.Size = New System.Drawing.Size(121, 22)
        Me.labTimeLineColor.TabIndex = 10
        '
        'btnTimeLineColor
        '
        Me.btnTimeLineColor.Location = New System.Drawing.Point(270, 89)
        Me.btnTimeLineColor.Name = "btnTimeLineColor"
        Me.btnTimeLineColor.Size = New System.Drawing.Size(39, 22)
        Me.btnTimeLineColor.TabIndex = 9
        Me.btnTimeLineColor.Text = "..."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(107, 89)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 12)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "颜色:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(107, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 12)
        Me.Label5.TabIndex = 7
        Me.Label5.Text = "线粗:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(107, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 12)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "线型:"
        '
        'lstTimeLine
        '
        Me.lstTimeLine.FormattingEnabled = True
        Me.lstTimeLine.ItemHeight = 12
        Me.lstTimeLine.Items.AddRange(New Object() {"一分格线", "二分格线", "十分格线", "半小时格线", "小时格线"})
        Me.lstTimeLine.Location = New System.Drawing.Point(10, 31)
        Me.lstTimeLine.Name = "lstTimeLine"
        Me.lstTimeLine.Size = New System.Drawing.Size(91, 64)
        Me.lstTimeLine.TabIndex = 5
        '
        'numLineWidth
        '
        Me.numLineWidth.DecimalPlaces = 1
        Me.numLineWidth.Increment = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.numLineWidth.Location = New System.Drawing.Point(148, 58)
        Me.numLineWidth.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numLineWidth.Name = "numLineWidth"
        Me.numLineWidth.Size = New System.Drawing.Size(161, 21)
        Me.numLineWidth.TabIndex = 3
        Me.numLineWidth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cmbLineStyle
        '
        Me.cmbLineStyle.FormattingEnabled = True
        Me.cmbLineStyle.Location = New System.Drawing.Point(148, 20)
        Me.cmbLineStyle.Name = "cmbLineStyle"
        Me.cmbLineStyle.Size = New System.Drawing.Size(161, 20)
        Me.cmbLineStyle.TabIndex = 1
        Me.cmbLineStyle.Text = "实线 ────────"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(13, 15)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(71, 12)
        Me.Label16.TabIndex = 13
        Me.Label16.Text = "运行图类型:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Controls.Add(Me.btnSetTrainDefault)
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(358, 368)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "运行线"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.picCheCiFont)
        Me.GroupBox4.Controls.Add(Me.Label12)
        Me.GroupBox4.Controls.Add(Me.btnCheCiFont)
        Me.GroupBox4.Controls.Add(Me.lstCheCiFont)
        Me.GroupBox4.Controls.Add(Me.labCheCiColor)
        Me.GroupBox4.Controls.Add(Me.btnCheCiColor)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Location = New System.Drawing.Point(7, 212)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(345, 121)
        Me.GroupBox4.TabIndex = 17
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "字体"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(132, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(35, 12)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "字体:"
        '
        'picCheCiFont
        '
        Me.picCheCiFont.Location = New System.Drawing.Point(53, 78)
        Me.picCheCiFont.Name = "picCheCiFont"
        Me.picCheCiFont.Size = New System.Drawing.Size(283, 36)
        Me.picCheCiFont.TabIndex = 14
        Me.picCheCiFont.TabStop = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(13, 89)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(35, 12)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "预览:"
        '
        'btnCheCiFont
        '
        Me.btnCheCiFont.Location = New System.Drawing.Point(198, 15)
        Me.btnCheCiFont.Name = "btnCheCiFont"
        Me.btnCheCiFont.Size = New System.Drawing.Size(138, 22)
        Me.btnCheCiFont.TabIndex = 9
        Me.btnCheCiFont.Text = "选择字体"
        '
        'lstCheCiFont
        '
        Me.lstCheCiFont.FormattingEnabled = True
        Me.lstCheCiFont.ItemHeight = 12
        Me.lstCheCiFont.Items.AddRange(New Object() {"车次标号", "斜向车次"})
        Me.lstCheCiFont.Location = New System.Drawing.Point(10, 20)
        Me.lstCheCiFont.Name = "lstCheCiFont"
        Me.lstCheCiFont.Size = New System.Drawing.Size(118, 52)
        Me.lstCheCiFont.TabIndex = 8
        '
        'labCheCiColor
        '
        Me.labCheCiColor.BackColor = System.Drawing.Color.Green
        Me.labCheCiColor.ForeColor = System.Drawing.Color.Green
        Me.labCheCiColor.Location = New System.Drawing.Point(196, 44)
        Me.labCheCiColor.Name = "labCheCiColor"
        Me.labCheCiColor.Size = New System.Drawing.Size(100, 22)
        Me.labCheCiColor.TabIndex = 7
        '
        'btnCheCiColor
        '
        Me.btnCheCiColor.Location = New System.Drawing.Point(297, 43)
        Me.btnCheCiColor.Name = "btnCheCiColor"
        Me.btnCheCiColor.Size = New System.Drawing.Size(39, 22)
        Me.btnCheCiColor.TabIndex = 6
        Me.btnCheCiColor.Text = "..."
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(132, 54)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(59, 12)
        Me.Label14.TabIndex = 5
        Me.Label14.Text = "字体颜色:"
        '
        'btnSetTrainDefault
        '
        Me.btnSetTrainDefault.Location = New System.Drawing.Point(7, 339)
        Me.btnSetTrainDefault.Name = "btnSetTrainDefault"
        Me.btnSetTrainDefault.Size = New System.Drawing.Size(123, 23)
        Me.btnSetTrainDefault.TabIndex = 16
        Me.btnSetTrainDefault.Text = "保存为默认值(&D)"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnSetTrainLine)
        Me.GroupBox2.Controls.Add(Me.cmbTrainStyle)
        Me.GroupBox2.Controls.Add(Me.picTrainLineView)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.labTrainLineColor)
        Me.GroupBox2.Controls.Add(Me.btnTrainLineColor)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.numTrainLineWidth)
        Me.GroupBox2.Controls.Add(Me.cmbTrainLineStyle)
        Me.GroupBox2.Controls.Add(Me.lstTrainStyle)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 17)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(345, 189)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "线"
        '
        'btnSetTrainLine
        '
        Me.btnSetTrainLine.Location = New System.Drawing.Point(268, 160)
        Me.btnSetTrainLine.Name = "btnSetTrainLine"
        Me.btnSetTrainLine.Size = New System.Drawing.Size(68, 23)
        Me.btnSetTrainLine.TabIndex = 23
        Me.btnSetTrainLine.Text = "应用"
        '
        'cmbTrainStyle
        '
        Me.cmbTrainStyle.FormattingEnabled = True
        Me.cmbTrainStyle.Location = New System.Drawing.Point(6, 20)
        Me.cmbTrainStyle.Name = "cmbTrainStyle"
        Me.cmbTrainStyle.Size = New System.Drawing.Size(122, 20)
        Me.cmbTrainStyle.TabIndex = 22
        '
        'picTrainLineView
        '
        Me.picTrainLineView.Location = New System.Drawing.Point(177, 121)
        Me.picTrainLineView.Name = "picTrainLineView"
        Me.picTrainLineView.Size = New System.Drawing.Size(159, 26)
        Me.picTrainLineView.TabIndex = 21
        Me.picTrainLineView.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(134, 133)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(35, 12)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "预览:"
        '
        'labTrainLineColor
        '
        Me.labTrainLineColor.BackColor = System.Drawing.Color.Green
        Me.labTrainLineColor.ForeColor = System.Drawing.Color.Green
        Me.labTrainLineColor.Location = New System.Drawing.Point(175, 89)
        Me.labTrainLineColor.Name = "labTrainLineColor"
        Me.labTrainLineColor.Size = New System.Drawing.Size(121, 22)
        Me.labTrainLineColor.TabIndex = 19
        '
        'btnTrainLineColor
        '
        Me.btnTrainLineColor.Location = New System.Drawing.Point(293, 89)
        Me.btnTrainLineColor.Name = "btnTrainLineColor"
        Me.btnTrainLineColor.Size = New System.Drawing.Size(43, 22)
        Me.btnTrainLineColor.TabIndex = 18
        Me.btnTrainLineColor.Text = "..."
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(134, 89)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(35, 12)
        Me.Label10.TabIndex = 17
        Me.Label10.Text = "颜色:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(134, 60)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(35, 12)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "线粗:"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(134, 23)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(35, 12)
        Me.Label19.TabIndex = 15
        Me.Label19.Text = "线型:"
        '
        'numTrainLineWidth
        '
        Me.numTrainLineWidth.DecimalPlaces = 1
        Me.numTrainLineWidth.Increment = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.numTrainLineWidth.Location = New System.Drawing.Point(175, 58)
        Me.numTrainLineWidth.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numTrainLineWidth.Name = "numTrainLineWidth"
        Me.numTrainLineWidth.Size = New System.Drawing.Size(161, 21)
        Me.numTrainLineWidth.TabIndex = 14
        Me.numTrainLineWidth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cmbTrainLineStyle
        '
        Me.cmbTrainLineStyle.FormattingEnabled = True
        Me.cmbTrainLineStyle.Location = New System.Drawing.Point(175, 20)
        Me.cmbTrainLineStyle.Name = "cmbTrainLineStyle"
        Me.cmbTrainLineStyle.Size = New System.Drawing.Size(161, 20)
        Me.cmbTrainLineStyle.TabIndex = 13
        Me.cmbTrainLineStyle.Text = "实线 ────────"
        '
        'lstTrainStyle
        '
        Me.lstTrainStyle.FormattingEnabled = True
        Me.lstTrainStyle.HorizontalScrollbar = True
        Me.lstTrainStyle.ItemHeight = 12
        Me.lstTrainStyle.Location = New System.Drawing.Point(6, 59)
        Me.lstTrainStyle.Name = "lstTrainStyle"
        Me.lstTrainStyle.Size = New System.Drawing.Size(122, 112)
        Me.lstTrainStyle.TabIndex = 5
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(209, 411)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "确定(&Y)"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(303, 411)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "退出(&E)"
        '
        'frmDiagramLineAndFontSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(388, 442)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnOK)
        Me.Name = "frmDiagramLineAndFontSet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "颜色与字体设置"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.picFontShow, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.picTimeLineShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numLineWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.picCheCiFont, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.picTrainLineView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numTrainLineWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents numLineWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmbLineStyle As System.Windows.Forms.ComboBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents lstTimeLine As System.Windows.Forms.ListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents labFontColor As System.Windows.Forms.Label
    Friend WithEvents btnFontColorSet As System.Windows.Forms.Button
    Friend WithEvents labTimeLineColor As System.Windows.Forms.Label
    Friend WithEvents btnTimeLineColor As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lstTrainStyle As System.Windows.Forms.ListBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents picTimeLineShow As System.Windows.Forms.PictureBox
    Friend WithEvents cmbDigramStyle As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnSetLineStyleDefault As System.Windows.Forms.Button
    Friend WithEvents lstFont As System.Windows.Forms.ListBox
    Friend WithEvents picFontShow As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnSelectFont As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lstStaLine As System.Windows.Forms.ListBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents picTrainLineView As System.Windows.Forms.PictureBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents labTrainLineColor As System.Windows.Forms.Label
    Friend WithEvents btnTrainLineColor As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents numTrainLineWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmbTrainLineStyle As System.Windows.Forms.ComboBox
    Friend WithEvents cmbTrainStyle As System.Windows.Forms.ComboBox
    Friend WithEvents btnSetTrainDefault As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents picCheCiFont As System.Windows.Forms.PictureBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents btnCheCiFont As System.Windows.Forms.Button
    Friend WithEvents lstCheCiFont As System.Windows.Forms.ListBox
    Friend WithEvents labCheCiColor As System.Windows.Forms.Label
    Friend WithEvents btnCheCiColor As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnSetTrainLine As System.Windows.Forms.Button
End Class
