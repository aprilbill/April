<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmDrawTrainDiagram
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnOutExcel = New System.Windows.Forms.Button
        Me.labZheFanTime = New System.Windows.Forms.Label
        Me.cmbPuHuaStyle = New System.Windows.Forms.ComboBox
        Me.btnSaveFangAn = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbTrainJLStyle = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.optXianJie = New System.Windows.Forms.GroupBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.cmdStartJLPU = New System.Windows.Forms.Button
        Me.optGongXian = New System.Windows.Forms.GroupBox
        Me.cmbGongXianUpFirSta = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.cmbGongDownFirSta = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.cmbAnotGongXianJL = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.optCangShu = New System.Windows.Forms.GroupBox
        Me.cmbUpID = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.cmbDownID = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.grdBiLi = New System.Windows.Forms.DataGridView
        Me.时间段2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.开行比 = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.下行顺序 = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.上行顺序 = New System.Windows.Forms.DataGridViewComboBoxColumn
        Me.txtLongDayEndTime = New System.Windows.Forms.TextBox
        Me.txtLongDayBeTime = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbJLNumBi = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.optNotBiLi = New System.Windows.Forms.RadioButton
        Me.optDengBiLi = New System.Windows.Forms.RadioButton
        Me.gBoxDuoJL = New System.Windows.Forms.GroupBox
        Me.chkAutoAddRuKu = New System.Windows.Forms.CheckBox
        Me.chkAutoAddChuKu = New System.Windows.Forms.CheckBox
        Me.rBtnGongXianJL = New System.Windows.Forms.RadioButton
        Me.rBtnXianJieJL = New System.Windows.Forms.RadioButton
        Me.rBtnDaXiaoJL = New System.Windows.Forms.RadioButton
        Me.btnExit = New System.Windows.Forms.Button
        Me.cmdShowFangA = New System.Windows.Forms.Button
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.StatusLabel = New System.Windows.Forms.ToolStripStatusLabel
        Me.ProBar = New System.Windows.Forms.ToolStripProgressBar
        Me.optDaXiao = New System.Windows.Forms.GroupBox
        Me.cmdReDrawJiaoLu = New System.Windows.Forms.Button
        Me.cmbSecJLStyle = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
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
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.optXianJie.SuspendLayout()
        Me.optGongXian.SuspendLayout()
        Me.optCangShu.SuspendLayout()
        CType(Me.grdBiLi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gBoxDuoJL.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.optDaXiao.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(826, 70)
        Me.Panel1.TabIndex = 6
        '
        'btnOutExcel
        '
        Me.btnOutExcel.Location = New System.Drawing.Point(677, 41)
        Me.btnOutExcel.Name = "btnOutExcel"
        Me.btnOutExcel.Size = New System.Drawing.Size(137, 23)
        Me.btnOutExcel.TabIndex = 16
        Me.btnOutExcel.Text = "方案导出至EXCEL(&O)"
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
        Me.btnSaveFangAn.Location = New System.Drawing.Point(531, 41)
        Me.btnSaveFangAn.Name = "btnSaveFangAn"
        Me.btnSaveFangAn.Size = New System.Drawing.Size(140, 23)
        Me.btnSaveFangAn.TabIndex = 15
        Me.btnSaveFangAn.Text = "保存铺画方案"
        Me.btnSaveFangAn.Visible = False
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
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnExit)
        Me.Panel2.Controls.Add(Me.chkAutoAddRuKu)
        Me.Panel2.Controls.Add(Me.optXianJie)
        Me.Panel2.Controls.Add(Me.chkAutoAddChuKu)
        Me.Panel2.Controls.Add(Me.optGongXian)
        Me.Panel2.Controls.Add(Me.optCangShu)
        Me.Panel2.Controls.Add(Me.gBoxDuoJL)
        Me.Panel2.Controls.Add(Me.cmdShowFangA)
        Me.Panel2.Controls.Add(Me.StatusStrip1)
        Me.Panel2.Controls.Add(Me.optDaXiao)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 432)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(826, 94)
        Me.Panel2.TabIndex = 7
        '
        'optXianJie
        '
        Me.optXianJie.Controls.Add(Me.Label10)
        Me.optXianJie.Controls.Add(Me.cmdStartJLPU)
        Me.optXianJie.Location = New System.Drawing.Point(29, 202)
        Me.optXianJie.Name = "optXianJie"
        Me.optXianJie.Size = New System.Drawing.Size(172, 180)
        Me.optXianJie.TabIndex = 20
        Me.optXianJie.TabStop = False
        Me.optXianJie.Text = "衔接交路:"
        Me.optXianJie.Visible = False
        '
        'Label10
        '
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(15, 44)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(138, 34)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "从上面选择交路和发车间隔再进行铺画"
        '
        'cmdStartJLPU
        '
        Me.cmdStartJLPU.Location = New System.Drawing.Point(16, 128)
        Me.cmdStartJLPU.Name = "cmdStartJLPU"
        Me.cmdStartJLPU.Size = New System.Drawing.Size(137, 26)
        Me.cmdStartJLPU.TabIndex = 12
        Me.cmdStartJLPU.Text = "开始铺画衔接交路"
        '
        'optGongXian
        '
        Me.optGongXian.Controls.Add(Me.cmbGongXianUpFirSta)
        Me.optGongXian.Controls.Add(Me.Label9)
        Me.optGongXian.Controls.Add(Me.cmbGongDownFirSta)
        Me.optGongXian.Controls.Add(Me.Label8)
        Me.optGongXian.Controls.Add(Me.Button1)
        Me.optGongXian.Controls.Add(Me.cmbAnotGongXianJL)
        Me.optGongXian.Controls.Add(Me.Label7)
        Me.optGongXian.Location = New System.Drawing.Point(665, 168)
        Me.optGongXian.Name = "optGongXian"
        Me.optGongXian.Size = New System.Drawing.Size(158, 180)
        Me.optGongXian.TabIndex = 21
        Me.optGongXian.TabStop = False
        Me.optGongXian.Text = "共线交路:"
        Me.optGongXian.Visible = False
        '
        'cmbGongXianUpFirSta
        '
        Me.cmbGongXianUpFirSta.FormattingEnabled = True
        Me.cmbGongXianUpFirSta.Location = New System.Drawing.Point(9, 108)
        Me.cmbGongXianUpFirSta.Name = "cmbGongXianUpFirSta"
        Me.cmbGongXianUpFirSta.Size = New System.Drawing.Size(139, 20)
        Me.cmbGongXianUpFirSta.TabIndex = 16
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(7, 93)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(131, 12)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "共线段上行第一车站名:"
        '
        'cmbGongDownFirSta
        '
        Me.cmbGongDownFirSta.FormattingEnabled = True
        Me.cmbGongDownFirSta.Location = New System.Drawing.Point(9, 70)
        Me.cmbGongDownFirSta.Name = "cmbGongDownFirSta"
        Me.cmbGongDownFirSta.Size = New System.Drawing.Size(139, 20)
        Me.cmbGongDownFirSta.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(131, 12)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "共线段下行第一车站名:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(9, 141)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(140, 26)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "铺画共线交路"
        '
        'cmbAnotGongXianJL
        '
        Me.cmbAnotGongXianJL.FormattingEnabled = True
        Me.cmbAnotGongXianJL.Location = New System.Drawing.Point(9, 30)
        Me.cmbAnotGongXianJL.Name = "cmbAnotGongXianJL"
        Me.cmbAnotGongXianJL.Size = New System.Drawing.Size(139, 20)
        Me.cmbAnotGongXianJL.TabIndex = 7
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(113, 12)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "选择另一共线交路::"
        '
        'optCangShu
        '
        Me.optCangShu.Controls.Add(Me.cmbUpID)
        Me.optCangShu.Controls.Add(Me.Label12)
        Me.optCangShu.Controls.Add(Me.cmbDownID)
        Me.optCangShu.Controls.Add(Me.Label11)
        Me.optCangShu.Controls.Add(Me.grdBiLi)
        Me.optCangShu.Controls.Add(Me.txtLongDayEndTime)
        Me.optCangShu.Controls.Add(Me.txtLongDayBeTime)
        Me.optCangShu.Controls.Add(Me.Label5)
        Me.optCangShu.Controls.Add(Me.Label4)
        Me.optCangShu.Controls.Add(Me.cmbJLNumBi)
        Me.optCangShu.Controls.Add(Me.Label3)
        Me.optCangShu.Controls.Add(Me.optNotBiLi)
        Me.optCangShu.Controls.Add(Me.optDengBiLi)
        Me.optCangShu.Location = New System.Drawing.Point(207, 202)
        Me.optCangShu.Name = "optCangShu"
        Me.optCangShu.Size = New System.Drawing.Size(466, 212)
        Me.optCangShu.TabIndex = 18
        Me.optCangShu.TabStop = False
        Me.optCangShu.Text = "参数设置:"
        Me.optCangShu.Visible = False
        '
        'cmbUpID
        '
        Me.cmbUpID.FormattingEnabled = True
        Me.cmbUpID.Items.AddRange(New Object() {"1", "2", "3", "4", "5"})
        Me.cmbUpID.Location = New System.Drawing.Point(79, 152)
        Me.cmbUpID.Name = "cmbUpID"
        Me.cmbUpID.Size = New System.Drawing.Size(64, 20)
        Me.cmbUpID.TabIndex = 18
        Me.cmbUpID.Text = "1"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(15, 155)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(59, 12)
        Me.Label12.TabIndex = 17
        Me.Label12.Text = "上行顺序:"
        '
        'cmbDownID
        '
        Me.cmbDownID.FormattingEnabled = True
        Me.cmbDownID.Items.AddRange(New Object() {"1", "2", "3", "4", "5"})
        Me.cmbDownID.Location = New System.Drawing.Point(79, 126)
        Me.cmbDownID.Name = "cmbDownID"
        Me.cmbDownID.Size = New System.Drawing.Size(64, 20)
        Me.cmbDownID.TabIndex = 16
        Me.cmbDownID.Text = "1"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(15, 129)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(59, 12)
        Me.Label11.TabIndex = 15
        Me.Label11.Text = "下行顺序:"
        '
        'grdBiLi
        '
        Me.grdBiLi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.grdBiLi.ColumnHeadersHeight = 25
        Me.grdBiLi.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.时间段2, Me.开行比, Me.下行顺序, Me.上行顺序})
        Me.grdBiLi.Location = New System.Drawing.Point(156, 45)
        Me.grdBiLi.MultiSelect = False
        Me.grdBiLi.Name = "grdBiLi"
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.FormatProvider = New System.Globalization.CultureInfo("zh-CN")
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdBiLi.RowHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdBiLi.RowHeadersWidth = 30
        Me.grdBiLi.RowTemplate.Height = 23
        Me.grdBiLi.Size = New System.Drawing.Size(299, 161)
        Me.grdBiLi.TabIndex = 14
        Me.grdBiLi.Text = "DataGridView1"
        '
        '时间段2
        '
        Me.时间段2.HeaderText = "时间段"
        Me.时间段2.Name = "时间段2"
        Me.时间段2.Width = 50
        '
        '开行比
        '
        Me.开行比.HeaderText = "开行比"
        Me.开行比.Items.AddRange(New Object() {"1:1", "2:1", "3:1", "4:1", "1:2", "1:3", "1:4", "1:0", "0:1"})
        Me.开行比.Name = "开行比"
        Me.开行比.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.开行比.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.开行比.Width = 60
        '
        '下行顺序
        '
        Me.下行顺序.HeaderText = "下行"
        Me.下行顺序.Items.AddRange(New Object() {"1", "2", "3", "4", "5"})
        Me.下行顺序.Name = "下行顺序"
        Me.下行顺序.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.下行顺序.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.下行顺序.Width = 60
        '
        '上行顺序
        '
        Me.上行顺序.HeaderText = "上行"
        Me.上行顺序.Items.AddRange(New Object() {"1", "2", "3", "4", "5"})
        Me.上行顺序.Name = "上行顺序"
        Me.上行顺序.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.上行顺序.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.上行顺序.Width = 60
        '
        'txtLongDayEndTime
        '
        Me.txtLongDayEndTime.Location = New System.Drawing.Point(79, 100)
        Me.txtLongDayEndTime.Name = "txtLongDayEndTime"
        Me.txtLongDayEndTime.Size = New System.Drawing.Size(64, 21)
        Me.txtLongDayEndTime.TabIndex = 10
        Me.txtLongDayEndTime.Text = "24.00"
        '
        'txtLongDayBeTime
        '
        Me.txtLongDayBeTime.Location = New System.Drawing.Point(79, 73)
        Me.txtLongDayBeTime.Name = "txtLongDayBeTime"
        Me.txtLongDayBeTime.Size = New System.Drawing.Size(64, 21)
        Me.txtLongDayBeTime.TabIndex = 9
        Me.txtLongDayBeTime.Text = "4.00"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 103)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 12)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "结束时间:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 77)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 12)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "开始时间:"
        '
        'cmbJLNumBi
        '
        Me.cmbJLNumBi.FormattingEnabled = True
        Me.cmbJLNumBi.Items.AddRange(New Object() {"1:1", "1:2", "1:3", "1:4", "2:1", "3:1", "4:1"})
        Me.cmbJLNumBi.Location = New System.Drawing.Point(79, 47)
        Me.cmbJLNumBi.Name = "cmbJLNumBi"
        Me.cmbJLNumBi.Size = New System.Drawing.Size(64, 20)
        Me.cmbJLNumBi.TabIndex = 6
        Me.cmbJLNumBi.Text = "1:1"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 12)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "列车比:"
        '
        'optNotBiLi
        '
        Me.optNotBiLi.AutoSize = True
        Me.optNotBiLi.Location = New System.Drawing.Point(167, 20)
        Me.optNotBiLi.Name = "optNotBiLi"
        Me.optNotBiLi.Size = New System.Drawing.Size(95, 16)
        Me.optNotBiLi.TabIndex = 1
        Me.optNotBiLi.Text = "全天不等比例"
        '
        'optDengBiLi
        '
        Me.optDengBiLi.AutoSize = True
        Me.optDengBiLi.Checked = True
        Me.optDengBiLi.Location = New System.Drawing.Point(17, 20)
        Me.optDengBiLi.Name = "optDengBiLi"
        Me.optDengBiLi.Size = New System.Drawing.Size(83, 16)
        Me.optDengBiLi.TabIndex = 0
        Me.optDengBiLi.TabStop = True
        Me.optDengBiLi.Text = "全天等比例"
        '
        'gBoxDuoJL
        '
        Me.gBoxDuoJL.Controls.Add(Me.rBtnGongXianJL)
        Me.gBoxDuoJL.Controls.Add(Me.rBtnXianJieJL)
        Me.gBoxDuoJL.Controls.Add(Me.rBtnDaXiaoJL)
        Me.gBoxDuoJL.Location = New System.Drawing.Point(339, 158)
        Me.gBoxDuoJL.Name = "gBoxDuoJL"
        Me.gBoxDuoJL.Size = New System.Drawing.Size(130, 94)
        Me.gBoxDuoJL.TabIndex = 17
        Me.gBoxDuoJL.TabStop = False
        Me.gBoxDuoJL.Text = "多交路铺画"
        Me.gBoxDuoJL.Visible = False
        '
        'chkAutoAddRuKu
        '
        Me.chkAutoAddRuKu.AutoSize = True
        Me.chkAutoAddRuKu.Location = New System.Drawing.Point(128, 14)
        Me.chkAutoAddRuKu.Name = "chkAutoAddRuKu"
        Me.chkAutoAddRuKu.Size = New System.Drawing.Size(108, 16)
        Me.chkAutoAddRuKu.TabIndex = 20
        Me.chkAutoAddRuKu.Text = "自动铺画入库车"
        '
        'chkAutoAddChuKu
        '
        Me.chkAutoAddChuKu.AutoSize = True
        Me.chkAutoAddChuKu.Location = New System.Drawing.Point(14, 14)
        Me.chkAutoAddChuKu.Name = "chkAutoAddChuKu"
        Me.chkAutoAddChuKu.Size = New System.Drawing.Size(108, 16)
        Me.chkAutoAddChuKu.TabIndex = 19
        Me.chkAutoAddChuKu.Text = "自动铺画出库车"
        '
        'rBtnGongXianJL
        '
        Me.rBtnGongXianJL.AutoSize = True
        Me.rBtnGongXianJL.Location = New System.Drawing.Point(10, 64)
        Me.rBtnGongXianJL.Name = "rBtnGongXianJL"
        Me.rBtnGongXianJL.Size = New System.Drawing.Size(71, 16)
        Me.rBtnGongXianJL.TabIndex = 18
        Me.rBtnGongXianJL.Text = "共线交路"
        '
        'rBtnXianJieJL
        '
        Me.rBtnXianJieJL.AutoSize = True
        Me.rBtnXianJieJL.Location = New System.Drawing.Point(10, 42)
        Me.rBtnXianJieJL.Name = "rBtnXianJieJL"
        Me.rBtnXianJieJL.Size = New System.Drawing.Size(71, 16)
        Me.rBtnXianJieJL.TabIndex = 17
        Me.rBtnXianJieJL.Text = "衔接交路"
        '
        'rBtnDaXiaoJL
        '
        Me.rBtnDaXiaoJL.AutoSize = True
        Me.rBtnDaXiaoJL.Checked = True
        Me.rBtnDaXiaoJL.Location = New System.Drawing.Point(10, 20)
        Me.rBtnDaXiaoJL.Name = "rBtnDaXiaoJL"
        Me.rBtnDaXiaoJL.Size = New System.Drawing.Size(71, 16)
        Me.rBtnDaXiaoJL.TabIndex = 16
        Me.rBtnDaXiaoJL.TabStop = True
        Me.rBtnDaXiaoJL.Text = "大小交路"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(716, 39)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(97, 23)
        Me.btnExit.TabIndex = 12
        Me.btnExit.Text = "退出(&E)"
        '
        'cmdShowFangA
        '
        Me.cmdShowFangA.Location = New System.Drawing.Point(14, 36)
        Me.cmdShowFangA.Name = "cmdShowFangA"
        Me.cmdShowFangA.Size = New System.Drawing.Size(130, 28)
        Me.cmdShowFangA.TabIndex = 11
        Me.cmdShowFangA.Text = "铺画单交路运行图"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StatusLabel, Me.ProBar})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 71)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(826, 23)
        Me.StatusStrip1.TabIndex = 13
        Me.StatusStrip1.Text = "StatusBar1"
        '
        'StatusLabel
        '
        Me.StatusLabel.AutoSize = False
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusLabel.Size = New System.Drawing.Size(150, 18)
        Me.StatusLabel.Text = "铺画运行图"
        '
        'ProBar
        '
        Me.ProBar.Name = "ProBar"
        Me.ProBar.Size = New System.Drawing.Size(450, 17)
        Me.ProBar.Visible = False
        '
        'optDaXiao
        '
        Me.optDaXiao.Controls.Add(Me.cmdReDrawJiaoLu)
        Me.optDaXiao.Controls.Add(Me.cmbSecJLStyle)
        Me.optDaXiao.Controls.Add(Me.Label6)
        Me.optDaXiao.Location = New System.Drawing.Point(148, 158)
        Me.optDaXiao.Name = "optDaXiao"
        Me.optDaXiao.Size = New System.Drawing.Size(159, 164)
        Me.optDaXiao.TabIndex = 19
        Me.optDaXiao.TabStop = False
        Me.optDaXiao.Text = "大小交路:"
        '
        'cmdReDrawJiaoLu
        '
        Me.cmdReDrawJiaoLu.Location = New System.Drawing.Point(12, 91)
        Me.cmdReDrawJiaoLu.Name = "cmdReDrawJiaoLu"
        Me.cmdReDrawJiaoLu.Size = New System.Drawing.Size(137, 26)
        Me.cmdReDrawJiaoLu.TabIndex = 12
        Me.cmdReDrawJiaoLu.Text = "开始铺画大小交路"
        '
        'cmbSecJLStyle
        '
        Me.cmbSecJLStyle.FormattingEnabled = True
        Me.cmbSecJLStyle.Location = New System.Drawing.Point(12, 50)
        Me.cmbSecJLStyle.Name = "cmbSecJLStyle"
        Me.cmbSecJLStyle.Size = New System.Drawing.Size(137, 20)
        Me.cmbSecJLStyle.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(89, 12)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "选择第二交路::"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbEditTime)
        Me.GroupBox1.Controls.Add(Me.grdTime)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 70)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(826, 362)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "间隔安排"
        '
        'cmbEditTime
        '
        Me.cmbEditTime.FormattingEnabled = True
        Me.cmbEditTime.Location = New System.Drawing.Point(427, 81)
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
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.FormatProvider = New System.Globalization.CultureInfo("zh-CN")
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTime.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdTime.RowHeadersWidth = 30
        Me.grdTime.RowTemplate.Height = 23
        Me.grdTime.Size = New System.Drawing.Size(820, 342)
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
        'frmDrawTrainDiagram
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(826, 526)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmDrawTrainDiagram"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "铺画列车运行图"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.optXianJie.ResumeLayout(False)
        Me.optGongXian.ResumeLayout(False)
        Me.optGongXian.PerformLayout()
        Me.optCangShu.ResumeLayout(False)
        Me.optCangShu.PerformLayout()
        CType(Me.grdBiLi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gBoxDuoJL.ResumeLayout(False)
        Me.gBoxDuoJL.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.optDaXiao.ResumeLayout(False)
        Me.optDaXiao.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.grdTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents cmbPuHuaStyle As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbTrainJLStyle As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdShowFangA As System.Windows.Forms.Button
    Friend WithEvents labZheFanTime As System.Windows.Forms.Label
    Friend WithEvents grdTime As System.Windows.Forms.DataGridView
    Friend WithEvents cmbEditTime As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents btnSaveFangAn As System.Windows.Forms.Button
    Friend WithEvents ProBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents StatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents rBtnDaXiaoJL As System.Windows.Forms.RadioButton
    Friend WithEvents gBoxDuoJL As System.Windows.Forms.GroupBox
    Friend WithEvents rBtnXianJieJL As System.Windows.Forms.RadioButton
    Friend WithEvents rBtnGongXianJL As System.Windows.Forms.RadioButton
    Friend WithEvents optCangShu As System.Windows.Forms.GroupBox
    Friend WithEvents optDengBiLi As System.Windows.Forms.RadioButton
    Friend WithEvents optDaXiao As System.Windows.Forms.GroupBox
    Friend WithEvents optNotBiLi As System.Windows.Forms.RadioButton
    Friend WithEvents cmbJLNumBi As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtLongDayEndTime As System.Windows.Forms.TextBox
    Friend WithEvents txtLongDayBeTime As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grdBiLi As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents cmbSecJLStyle As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmdReDrawJiaoLu As System.Windows.Forms.Button
    Friend WithEvents optXianJie As System.Windows.Forms.GroupBox
    Friend WithEvents cmdStartJLPU As System.Windows.Forms.Button
    Friend WithEvents optGongXian As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cmbAnotGongXianJL As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbGongXianUpFirSta As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbGongDownFirSta As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnOutExcel As System.Windows.Forms.Button
    Friend WithEvents chkAutoAddRuKu As System.Windows.Forms.CheckBox
    Friend WithEvents chkAutoAddChuKu As System.Windows.Forms.CheckBox
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
    Friend WithEvents cmbUpID As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbDownID As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents 时间段2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 开行比 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents 下行顺序 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents 上行顺序 As System.Windows.Forms.DataGridViewComboBoxColumn
End Class
