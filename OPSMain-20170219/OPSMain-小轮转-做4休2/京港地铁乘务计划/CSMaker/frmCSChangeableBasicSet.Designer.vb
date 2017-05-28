<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCSChangeableBasicSet
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
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TxTNightLength = New System.Windows.Forms.TextBox()
        Me.TXTCDayLength = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.TxTDayLength = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.TxTMorningLength = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.TxTMinNightLength = New System.Windows.Forms.TextBox()
        Me.TXTMinCDayLength = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.TxTMinDayLength = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.TxTMinMorningLength = New System.Windows.Forms.TextBox()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker3 = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.DateTimePicker4 = New System.Windows.Forms.DateTimePicker()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Button13 = New System.Windows.Forms.Button()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.DGVBeiBan = New System.Windows.Forms.DataGridView()
        Me.班种2 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.备班名称 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.备班地点 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.开始时间2 = New CS_CSMaker.frmCSMakeBasicSet.CalendarColumn()
        Me.结束时间2 = New CS_CSMaker.frmCSMakeBasicSet.CalendarColumn()
        Me.所属区域2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DGVBeiche = New System.Windows.Forms.DataGridView()
        Me.班种 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.备车名称 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.备车地点 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.开始时间 = New CS_CSMaker.frmCSMakeBasicSet.CalendarColumn()
        Me.结束时间 = New CS_CSMaker.frmCSMakeBasicSet.CalendarColumn()
        Me.所属区域 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        CType(Me.DGVBeiBan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVBeiche, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(682, 479)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 16
        Me.Button2.Text = "确定(&Y)"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Location = New System.Drawing.Point(12, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(833, 470)
        Me.TabControl1.TabIndex = 38
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox4)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(825, 444)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "作业参数"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.GroupBox1)
        Me.GroupBox4.Controls.Add(Me.GroupBox6)
        Me.GroupBox4.Controls.Add(Me.GroupBox5)
        Me.GroupBox4.Location = New System.Drawing.Point(26, 24)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(767, 397)
        Me.GroupBox4.TabIndex = 38
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "劳动参数设置"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label27)
        Me.GroupBox6.Controls.Add(Me.Label10)
        Me.GroupBox6.Controls.Add(Me.Label7)
        Me.GroupBox6.Controls.Add(Me.TxTNightLength)
        Me.GroupBox6.Controls.Add(Me.TXTCDayLength)
        Me.GroupBox6.Controls.Add(Me.Label20)
        Me.GroupBox6.Controls.Add(Me.Label26)
        Me.GroupBox6.Controls.Add(Me.TxTDayLength)
        Me.GroupBox6.Controls.Add(Me.Label21)
        Me.GroupBox6.Controls.Add(Me.TxTMorningLength)
        Me.GroupBox6.Controls.Add(Me.Label24)
        Me.GroupBox6.Controls.Add(Me.Label12)
        Me.GroupBox6.Controls.Add(Me.Label28)
        Me.GroupBox6.Controls.Add(Me.Label29)
        Me.GroupBox6.Controls.Add(Me.Label30)
        Me.GroupBox6.Controls.Add(Me.TxTMinNightLength)
        Me.GroupBox6.Controls.Add(Me.TXTMinCDayLength)
        Me.GroupBox6.Controls.Add(Me.Label31)
        Me.GroupBox6.Controls.Add(Me.Label32)
        Me.GroupBox6.Controls.Add(Me.TxTMinDayLength)
        Me.GroupBox6.Controls.Add(Me.Label33)
        Me.GroupBox6.Controls.Add(Me.TxTMinMorningLength)
        Me.GroupBox6.Controls.Add(Me.Label34)
        Me.GroupBox6.Controls.Add(Me.Label35)
        Me.GroupBox6.Location = New System.Drawing.Point(10, 148)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(692, 155)
        Me.GroupBox6.TabIndex = 58
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "公里参数"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(301, 90)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(113, 12)
        Me.Label27.TabIndex = 72
        Me.Label27.Text = "日勤班最大驾驶公里"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(313, 62)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(101, 12)
        Me.Label10.TabIndex = 73
        Me.Label10.Text = "白班最大驾驶公里"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(312, 35)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(101, 12)
        Me.Label7.TabIndex = 74
        Me.Label7.Text = "早班最大驾驶公里"
        '
        'TxTNightLength
        '
        Me.TxTNightLength.Location = New System.Drawing.Point(435, 114)
        Me.TxTNightLength.Name = "TxTNightLength"
        Me.TxTNightLength.Size = New System.Drawing.Size(67, 21)
        Me.TxTNightLength.TabIndex = 78
        '
        'TXTCDayLength
        '
        Me.TXTCDayLength.Location = New System.Drawing.Point(435, 86)
        Me.TXTCDayLength.Name = "TXTCDayLength"
        Me.TXTCDayLength.Size = New System.Drawing.Size(67, 21)
        Me.TXTCDayLength.TabIndex = 75
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(508, 35)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(29, 12)
        Me.Label20.TabIndex = 68
        Me.Label20.Text = "公里"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(508, 90)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(29, 12)
        Me.Label26.TabIndex = 67
        Me.Label26.Text = "公里"
        '
        'TxTDayLength
        '
        Me.TxTDayLength.Location = New System.Drawing.Point(435, 59)
        Me.TxTDayLength.Name = "TxTDayLength"
        Me.TxTDayLength.Size = New System.Drawing.Size(67, 21)
        Me.TxTDayLength.TabIndex = 77
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(508, 62)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(29, 12)
        Me.Label21.TabIndex = 69
        Me.Label21.Text = "公里"
        '
        'TxTMorningLength
        '
        Me.TxTMorningLength.Location = New System.Drawing.Point(435, 32)
        Me.TxTMorningLength.Name = "TxTMorningLength"
        Me.TxTMorningLength.Size = New System.Drawing.Size(67, 21)
        Me.TxTMorningLength.TabIndex = 76
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(508, 117)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(29, 12)
        Me.Label24.TabIndex = 71
        Me.Label24.Text = "公里"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(313, 117)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(101, 12)
        Me.Label12.TabIndex = 70
        Me.Label12.Text = "夜班最大驾驶公里"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(16, 90)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(113, 12)
        Me.Label28.TabIndex = 60
        Me.Label28.Text = "日勤班最小驾驶公里"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(27, 62)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(101, 12)
        Me.Label29.TabIndex = 61
        Me.Label29.Text = "白班最小驾驶公里"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(26, 35)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(101, 12)
        Me.Label30.TabIndex = 62
        Me.Label30.Text = "早班最小驾驶公里"
        '
        'TxTMinNightLength
        '
        Me.TxTMinNightLength.Location = New System.Drawing.Point(149, 114)
        Me.TxTMinNightLength.Name = "TxTMinNightLength"
        Me.TxTMinNightLength.Size = New System.Drawing.Size(67, 21)
        Me.TxTMinNightLength.TabIndex = 66
        '
        'TXTMinCDayLength
        '
        Me.TXTMinCDayLength.Location = New System.Drawing.Point(149, 87)
        Me.TXTMinCDayLength.Name = "TXTMinCDayLength"
        Me.TXTMinCDayLength.Size = New System.Drawing.Size(67, 21)
        Me.TXTMinCDayLength.TabIndex = 63
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(222, 35)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(29, 12)
        Me.Label31.TabIndex = 56
        Me.Label31.Text = "公里"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(222, 90)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(29, 12)
        Me.Label32.TabIndex = 55
        Me.Label32.Text = "公里"
        '
        'TxTMinDayLength
        '
        Me.TxTMinDayLength.Location = New System.Drawing.Point(149, 59)
        Me.TxTMinDayLength.Name = "TxTMinDayLength"
        Me.TxTMinDayLength.Size = New System.Drawing.Size(67, 21)
        Me.TxTMinDayLength.TabIndex = 65
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(222, 62)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(29, 12)
        Me.Label33.TabIndex = 57
        Me.Label33.Text = "公里"
        '
        'TxTMinMorningLength
        '
        Me.TxTMinMorningLength.Location = New System.Drawing.Point(149, 32)
        Me.TxTMinMorningLength.Name = "TxTMinMorningLength"
        Me.TxTMinMorningLength.Size = New System.Drawing.Size(67, 21)
        Me.TxTMinMorningLength.TabIndex = 64
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(222, 117)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(29, 12)
        Me.Label34.TabIndex = 59
        Me.Label34.Text = "公里"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(27, 117)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(101, 12)
        Me.Label35.TabIndex = 58
        Me.Label35.Text = "夜班最小驾驶公里"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label1)
        Me.GroupBox5.Controls.Add(Me.Label2)
        Me.GroupBox5.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox5.Controls.Add(Me.Label5)
        Me.GroupBox5.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox5.Controls.Add(Me.DateTimePicker3)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Controls.Add(Me.DateTimePicker4)
        Me.GroupBox5.Location = New System.Drawing.Point(10, 34)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(692, 108)
        Me.GroupBox5.TabIndex = 57
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "时间参数"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 54
        Me.Label1.Text = "连续驾车时间"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(30, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 12)
        Me.Label2.TabIndex = 55
        Me.Label2.Text = "库发准备时间"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DateTimePicker1.Location = New System.Drawing.Point(334, 69)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.ShowUpDown = True
        Me.DateTimePicker1.Size = New System.Drawing.Size(95, 21)
        Me.DateTimePicker1.TabIndex = 60
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(225, 28)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 12)
        Me.Label5.TabIndex = 56
        Me.Label5.Text = "正线出勤准备时间"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DateTimePicker2.Location = New System.Drawing.Point(334, 24)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.ShowUpDown = True
        Me.DateTimePicker2.Size = New System.Drawing.Size(95, 21)
        Me.DateTimePicker2.TabIndex = 61
        '
        'DateTimePicker3
        '
        Me.DateTimePicker3.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DateTimePicker3.Location = New System.Drawing.Point(115, 24)
        Me.DateTimePicker3.Name = "DateTimePicker3"
        Me.DateTimePicker3.ShowUpDown = True
        Me.DateTimePicker3.Size = New System.Drawing.Size(95, 21)
        Me.DateTimePicker3.TabIndex = 58
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(249, 73)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(77, 12)
        Me.Label11.TabIndex = 57
        Me.Label11.Text = "退勤准备时间"
        '
        'DateTimePicker4
        '
        Me.DateTimePicker4.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DateTimePicker4.Location = New System.Drawing.Point(116, 71)
        Me.DateTimePicker4.Name = "DateTimePicker4"
        Me.DateTimePicker4.ShowUpDown = True
        Me.DateTimePicker4.Size = New System.Drawing.Size(95, 21)
        Me.DateTimePicker4.TabIndex = 59
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.Label4)
        Me.TabPage6.Controls.Add(Me.Label3)
        Me.TabPage6.Controls.Add(Me.Button13)
        Me.TabPage6.Controls.Add(Me.Button14)
        Me.TabPage6.Controls.Add(Me.DGVBeiBan)
        Me.TabPage6.Controls.Add(Me.Button12)
        Me.TabPage6.Controls.Add(Me.Button1)
        Me.TabPage6.Controls.Add(Me.DGVBeiche)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(825, 444)
        Me.TabPage6.TabIndex = 6
        Me.TabPage6.Text = "备车/班参数"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(410, 33)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "备班参数："
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "备车参数："
        '
        'Button13
        '
        Me.Button13.Location = New System.Drawing.Point(703, 378)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(75, 23)
        Me.Button13.TabIndex = 5
        Me.Button13.Text = "删除"
        Me.Button13.UseVisualStyleBackColor = True
        '
        'Button14
        '
        Me.Button14.Location = New System.Drawing.Point(604, 378)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(75, 23)
        Me.Button14.TabIndex = 4
        Me.Button14.Text = "添加"
        Me.Button14.UseVisualStyleBackColor = True
        '
        'DGVBeiBan
        '
        Me.DGVBeiBan.AllowUserToAddRows = False
        Me.DGVBeiBan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVBeiBan.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.班种2, Me.备班名称, Me.备班地点, Me.开始时间2, Me.结束时间2, Me.所属区域2})
        Me.DGVBeiBan.Location = New System.Drawing.Point(412, 53)
        Me.DGVBeiBan.Name = "DGVBeiBan"
        Me.DGVBeiBan.RowHeadersVisible = False
        Me.DGVBeiBan.RowTemplate.Height = 23
        Me.DGVBeiBan.Size = New System.Drawing.Size(366, 319)
        Me.DGVBeiBan.TabIndex = 3
        '
        '班种2
        '
        Me.班种2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.班种2.Frozen = True
        Me.班种2.HeaderText = "班种"
        Me.班种2.Items.AddRange(New Object() {"早班", "白班", "夜班"})
        Me.班种2.Name = "班种2"
        Me.班种2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.班种2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.班种2.Width = 54
        '
        '备班名称
        '
        Me.备班名称.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.备班名称.Frozen = True
        Me.备班名称.HeaderText = "备班名称"
        Me.备班名称.Name = "备班名称"
        Me.备班名称.Width = 78
        '
        '备班地点
        '
        Me.备班地点.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.备班地点.HeaderText = "备班地点"
        Me.备班地点.Name = "备班地点"
        Me.备班地点.Width = 78
        '
        '开始时间2
        '
        Me.开始时间2.HeaderText = "开始时间"
        Me.开始时间2.Name = "开始时间2"
        Me.开始时间2.Width = 80
        '
        '结束时间2
        '
        Me.结束时间2.HeaderText = "结束时间"
        Me.结束时间2.Name = "结束时间2"
        Me.结束时间2.Width = 80
        '
        '所属区域2
        '
        Me.所属区域2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.所属区域2.DataPropertyName = "说明"
        Me.所属区域2.HeaderText = "所属区域"
        Me.所属区域2.Name = "所属区域2"
        Me.所属区域2.Width = 78
        '
        'Button12
        '
        Me.Button12.Location = New System.Drawing.Point(306, 378)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(75, 23)
        Me.Button12.TabIndex = 2
        Me.Button12.Text = "删除"
        Me.Button12.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(203, 378)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "添加"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DGVBeiche
        '
        Me.DGVBeiche.AllowUserToAddRows = False
        Me.DGVBeiche.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVBeiche.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.班种, Me.备车名称, Me.备车地点, Me.开始时间, Me.结束时间, Me.所属区域})
        Me.DGVBeiche.Location = New System.Drawing.Point(19, 53)
        Me.DGVBeiche.Name = "DGVBeiche"
        Me.DGVBeiche.RowHeadersVisible = False
        Me.DGVBeiche.RowTemplate.Height = 23
        Me.DGVBeiche.Size = New System.Drawing.Size(362, 319)
        Me.DGVBeiche.TabIndex = 0
        '
        '班种
        '
        Me.班种.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.班种.Frozen = True
        Me.班种.HeaderText = "班种"
        Me.班种.Items.AddRange(New Object() {"早班", "白班", "夜班"})
        Me.班种.Name = "班种"
        Me.班种.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.班种.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.班种.Width = 54
        '
        '备车名称
        '
        Me.备车名称.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.备车名称.Frozen = True
        Me.备车名称.HeaderText = "备车名称"
        Me.备车名称.Name = "备车名称"
        Me.备车名称.Width = 78
        '
        '备车地点
        '
        Me.备车地点.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.备车地点.HeaderText = "备车地点"
        Me.备车地点.Name = "备车地点"
        Me.备车地点.Width = 78
        '
        '开始时间
        '
        Me.开始时间.HeaderText = "开始时间"
        Me.开始时间.Name = "开始时间"
        Me.开始时间.Width = 80
        '
        '结束时间
        '
        Me.结束时间.HeaderText = "结束时间"
        Me.结束时间.Name = "结束时间"
        Me.结束时间.Width = 80
        '
        '所属区域
        '
        Me.所属区域.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.所属区域.HeaderText = "所属区域"
        Me.所属区域.Name = "所属区域"
        Me.所属区域.Width = 78
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(763, 479)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(75, 23)
        Me.Button6.TabIndex = 39
        Me.Button6.Text = "退出(&E)"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TextBox4)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.TextBox3)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.TextBox2)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 309)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(692, 74)
        Me.GroupBox1.TabIndex = 59
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "人数参数"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(30, 33)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "早班人数"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(89, 30)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(55, 21)
        Me.TextBox1.TabIndex = 1
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(237, 30)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(55, 21)
        Me.TextBox2.TabIndex = 3
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(178, 33)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 12)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "白班人数"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(391, 30)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(55, 21)
        Me.TextBox3.TabIndex = 5
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(332, 33)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 12)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "夜班人数"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(557, 30)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(55, 21)
        Me.TextBox4.TabIndex = 7
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(490, 33)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(65, 12)
        Me.Label13.TabIndex = 6
        Me.Label13.Text = "日勤班人数"
        '
        'frmCSChangeableBasicSet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(850, 507)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Button2)
        Me.MaximizeBox = False
        Me.Name = "frmCSChangeableBasicSet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "乘务计划基础数据设置"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage6.PerformLayout()
        CType(Me.DGVBeiBan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVBeiche, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents DGVBeiche As System.Windows.Forms.DataGridView
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DGVBeiBan As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents 班种2 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents 备班名称 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 备班地点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend 开始时间2 As CS_CSMaker.frmCSMakeBasicSet.CalendarColumn
    Friend 结束时间2 As CS_CSMaker.frmCSMakeBasicSet.CalendarColumn
    Friend WithEvents 所属区域2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 班种 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents 备车名称 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 备车地点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend 开始时间 As CS_CSMaker.frmCSMakeBasicSet.CalendarColumn
    Friend 结束时间 As CS_CSMaker.frmCSMakeBasicSet.CalendarColumn
    Friend WithEvents 所属区域 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TxTNightLength As System.Windows.Forms.TextBox
    Friend WithEvents TXTCDayLength As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents TxTDayLength As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents TxTMorningLength As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents TxTMinNightLength As System.Windows.Forms.TextBox
    Friend WithEvents TXTMinCDayLength As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents TxTMinDayLength As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents TxTMinMorningLength As System.Windows.Forms.TextBox
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker3 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker4 As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
