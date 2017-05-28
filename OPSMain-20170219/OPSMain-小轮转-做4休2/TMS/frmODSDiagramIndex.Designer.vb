<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmODSDiagramIndex
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
        Me.btnOK = New System.Windows.Forms.Button
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmbTimeFormate = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.btnCal = New System.Windows.Forms.Button
        Me.btnSaveToExcel = New System.Windows.Forms.Button
        Me.dataGridrouting = New System.Windows.Forms.DataGridView
        Me.序号 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.交路名称 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.运行标尺 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.停站标尺 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.开行数量 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.纯运行时分 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.总停站时分 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.总运行时分 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.旅行速度 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.技术速度 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.走行公里数 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.btnOutExcel2 = New System.Windows.Forms.Button
        Me.dgrvPeriod = New System.Windows.Forms.DataGridView
        Me.Column1序号 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.区间名称 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.结束时间 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.下行开行列数 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.上行开行列数 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.下行间隔 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.上行间隔 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.车底数量 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.btOutCheDitoExcel = New System.Windows.Forms.Button
        Me.dataGridChedi = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.车底名称 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.连挂列车数 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.连挂车次 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.btnOutExcelFisrtLast = New System.Windows.Forms.Button
        Me.dataFirLast = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.上行末班车 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.Button1 = New System.Windows.Forms.Button
        Me.DgdWholeIndex = New System.Windows.Forms.DataGridView
        Me.指标名 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.指标值 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnAllOutput = New System.Windows.Forms.Button
        Me.btnReCal = New System.Windows.Forms.Button
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dataGridrouting, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.dgrvPeriod, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.dataGridChedi, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        CType(Me.dataFirLast, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.DgdWholeIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(606, 454)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 73
        Me.btnOK.Text = "退出(&E)"
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.btnSaveToExcel)
        Me.TabPage1.Controls.Add(Me.dataGridrouting)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(661, 411)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "分交路指标统计"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbTimeFormate)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btnCal)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(649, 55)
        Me.GroupBox1.TabIndex = 75
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "参数设置"
        '
        'cmbTimeFormate
        '
        Me.cmbTimeFormate.FormattingEnabled = True
        Me.cmbTimeFormate.Items.AddRange(New Object() {"分", "分.秒"})
        Me.cmbTimeFormate.Location = New System.Drawing.Point(71, 22)
        Me.cmbTimeFormate.Name = "cmbTimeFormate"
        Me.cmbTimeFormate.Size = New System.Drawing.Size(65, 20)
        Me.cmbTimeFormate.TabIndex = 83
        Me.cmbTimeFormate.Text = "分.秒"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 12)
        Me.Label4.TabIndex = 82
        Me.Label4.Text = "时间格式:"
        '
        'btnCal
        '
        Me.btnCal.Location = New System.Drawing.Point(142, 20)
        Me.btnCal.Name = "btnCal"
        Me.btnCal.Size = New System.Drawing.Size(95, 23)
        Me.btnCal.TabIndex = 81
        Me.btnCal.Text = "重新计算(&C)"
        '
        'btnSaveToExcel
        '
        Me.btnSaveToExcel.Location = New System.Drawing.Point(532, 382)
        Me.btnSaveToExcel.Name = "btnSaveToExcel"
        Me.btnSaveToExcel.Size = New System.Drawing.Size(123, 23)
        Me.btnSaveToExcel.TabIndex = 72
        Me.btnSaveToExcel.Text = "导出至EXCEL"
        '
        'dataGridrouting
        '
        Me.dataGridrouting.AllowUserToAddRows = False
        Me.dataGridrouting.AllowUserToDeleteRows = False
        Me.dataGridrouting.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dataGridrouting.ColumnHeadersHeight = 40
        Me.dataGridrouting.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.序号, Me.交路名称, Me.运行标尺, Me.停站标尺, Me.开行数量, Me.纯运行时分, Me.总停站时分, Me.总运行时分, Me.旅行速度, Me.技术速度, Me.走行公里数})
        Me.dataGridrouting.Location = New System.Drawing.Point(6, 70)
        Me.dataGridrouting.Name = "dataGridrouting"
        Me.dataGridrouting.ReadOnly = True
        Me.dataGridrouting.RowHeadersWidth = 30
        Me.dataGridrouting.RowTemplate.Height = 23
        Me.dataGridrouting.Size = New System.Drawing.Size(649, 306)
        Me.dataGridrouting.TabIndex = 71
        '
        '序号
        '
        Me.序号.HeaderText = "序号"
        Me.序号.Name = "序号"
        Me.序号.ReadOnly = True
        Me.序号.Width = 40
        '
        '交路名称
        '
        Me.交路名称.HeaderText = "交路名称"
        Me.交路名称.Name = "交路名称"
        Me.交路名称.ReadOnly = True
        Me.交路名称.Width = 120
        '
        '运行标尺
        '
        Me.运行标尺.HeaderText = "运行标尺"
        Me.运行标尺.Name = "运行标尺"
        Me.运行标尺.ReadOnly = True
        Me.运行标尺.Width = 80
        '
        '停站标尺
        '
        Me.停站标尺.HeaderText = "停站标尺"
        Me.停站标尺.Name = "停站标尺"
        Me.停站标尺.ReadOnly = True
        Me.停站标尺.Width = 80
        '
        '开行数量
        '
        Me.开行数量.HeaderText = "开行数量(列)"
        Me.开行数量.Name = "开行数量"
        Me.开行数量.ReadOnly = True
        Me.开行数量.Width = 60
        '
        '纯运行时分
        '
        Me.纯运行时分.HeaderText = "纯运行时分"
        Me.纯运行时分.Name = "纯运行时分"
        Me.纯运行时分.ReadOnly = True
        Me.纯运行时分.Width = 50
        '
        '总停站时分
        '
        Me.总停站时分.HeaderText = "总停站时分"
        Me.总停站时分.Name = "总停站时分"
        Me.总停站时分.ReadOnly = True
        Me.总停站时分.Width = 50
        '
        '总运行时分
        '
        Me.总运行时分.HeaderText = "总旅行时间"
        Me.总运行时分.Name = "总运行时分"
        Me.总运行时分.ReadOnly = True
        Me.总运行时分.Width = 50
        '
        '旅行速度
        '
        Me.旅行速度.HeaderText = "旅行速度(km/h)"
        Me.旅行速度.Name = "旅行速度"
        Me.旅行速度.ReadOnly = True
        Me.旅行速度.Width = 60
        '
        '技术速度
        '
        Me.技术速度.HeaderText = "技术速度(km/h)"
        Me.技术速度.Name = "技术速度"
        Me.技术速度.ReadOnly = True
        Me.技术速度.Width = 60
        '
        '走行公里数
        '
        Me.走行公里数.HeaderText = "走行公里(列车公里)"
        Me.走行公里数.Name = "走行公里数"
        Me.走行公里数.ReadOnly = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(669, 436)
        Me.TabControl1.TabIndex = 72
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.btnOutExcel2)
        Me.TabPage3.Controls.Add(Me.dgrvPeriod)
        Me.TabPage3.Location = New System.Drawing.Point(4, 21)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(661, 411)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "分时段指标统计"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'btnOutExcel2
        '
        Me.btnOutExcel2.Location = New System.Drawing.Point(530, 375)
        Me.btnOutExcel2.Name = "btnOutExcel2"
        Me.btnOutExcel2.Size = New System.Drawing.Size(123, 23)
        Me.btnOutExcel2.TabIndex = 74
        Me.btnOutExcel2.Text = "导出至EXCEL"
        '
        'dgrvPeriod
        '
        Me.dgrvPeriod.AllowUserToAddRows = False
        Me.dgrvPeriod.AllowUserToDeleteRows = False
        Me.dgrvPeriod.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgrvPeriod.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1序号, Me.区间名称, Me.DataGridViewTextBoxColumn10, Me.DataGridViewTextBoxColumn11, Me.结束时间, Me.下行开行列数, Me.上行开行列数, Me.下行间隔, Me.上行间隔, Me.车底数量})
        Me.dgrvPeriod.Location = New System.Drawing.Point(8, 25)
        Me.dgrvPeriod.Name = "dgrvPeriod"
        Me.dgrvPeriod.ReadOnly = True
        Me.dgrvPeriod.RowHeadersWidth = 30
        Me.dgrvPeriod.RowTemplate.Height = 23
        Me.dgrvPeriod.Size = New System.Drawing.Size(645, 344)
        Me.dgrvPeriod.TabIndex = 73
        '
        'Column1序号
        '
        Me.Column1序号.HeaderText = "序号"
        Me.Column1序号.Name = "Column1序号"
        Me.Column1序号.ReadOnly = True
        Me.Column1序号.Width = 40
        '
        '区间名称
        '
        Me.区间名称.HeaderText = "区间名称"
        Me.区间名称.Name = "区间名称"
        Me.区间名称.ReadOnly = True
        Me.区间名称.Width = 120
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.HeaderText = "时段编号"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn10.Width = 80
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.HeaderText = "起始时间"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn11.Width = 80
        '
        '结束时间
        '
        Me.结束时间.HeaderText = "结束时间"
        Me.结束时间.Name = "结束时间"
        Me.结束时间.ReadOnly = True
        Me.结束时间.Width = 80
        '
        '下行开行列数
        '
        Me.下行开行列数.HeaderText = "下行开行列数"
        Me.下行开行列数.Name = "下行开行列数"
        Me.下行开行列数.ReadOnly = True
        '
        '上行开行列数
        '
        Me.上行开行列数.HeaderText = "上行开行列数"
        Me.上行开行列数.Name = "上行开行列数"
        Me.上行开行列数.ReadOnly = True
        '
        '下行间隔
        '
        Me.下行间隔.HeaderText = "下行间隔"
        Me.下行间隔.Name = "下行间隔"
        Me.下行间隔.ReadOnly = True
        '
        '上行间隔
        '
        Me.上行间隔.HeaderText = "上行间隔"
        Me.上行间隔.Name = "上行间隔"
        Me.上行间隔.ReadOnly = True
        '
        '车底数量
        '
        Me.车底数量.HeaderText = "车底数量"
        Me.车底数量.Name = "车底数量"
        Me.车底数量.ReadOnly = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btOutCheDitoExcel)
        Me.TabPage2.Controls.Add(Me.dataGridChedi)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(661, 411)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "车底运用指标"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btOutCheDitoExcel
        '
        Me.btOutCheDitoExcel.Location = New System.Drawing.Point(527, 379)
        Me.btOutCheDitoExcel.Name = "btOutCheDitoExcel"
        Me.btOutCheDitoExcel.Size = New System.Drawing.Size(123, 23)
        Me.btOutCheDitoExcel.TabIndex = 73
        Me.btOutCheDitoExcel.Text = "导出至EXCEL"
        '
        'dataGridChedi
        '
        Me.dataGridChedi.AllowUserToAddRows = False
        Me.dataGridChedi.AllowUserToDeleteRows = False
        Me.dataGridChedi.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dataGridChedi.ColumnHeadersHeight = 25
        Me.dataGridChedi.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9, Me.车底名称, Me.连挂列车数, Me.连挂车次})
        Me.dataGridChedi.Location = New System.Drawing.Point(3, 3)
        Me.dataGridChedi.Name = "dataGridChedi"
        Me.dataGridChedi.RowHeadersWidth = 30
        Me.dataGridChedi.RowTemplate.Height = 23
        Me.dataGridChedi.Size = New System.Drawing.Size(649, 360)
        Me.dataGridChedi.TabIndex = 72
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "序号"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.Width = 40
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "车底ID"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.Width = 50
        '
        '车底名称
        '
        Me.车底名称.HeaderText = "车底名称"
        Me.车底名称.Name = "车底名称"
        Me.车底名称.Width = 120
        '
        '连挂列车数
        '
        Me.连挂列车数.HeaderText = "连挂车数"
        Me.连挂列车数.Name = "连挂列车数"
        Me.连挂列车数.Width = 80
        '
        '连挂车次
        '
        Me.连挂车次.HeaderText = "连挂车次"
        Me.连挂车次.Name = "连挂车次"
        Me.连挂车次.Width = 350
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.btnOutExcelFisrtLast)
        Me.TabPage5.Controls.Add(Me.dataFirLast)
        Me.TabPage5.Location = New System.Drawing.Point(4, 21)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(661, 411)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "首末班车"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'btnOutExcelFisrtLast
        '
        Me.btnOutExcelFisrtLast.Location = New System.Drawing.Point(524, 379)
        Me.btnOutExcelFisrtLast.Name = "btnOutExcelFisrtLast"
        Me.btnOutExcelFisrtLast.Size = New System.Drawing.Size(123, 23)
        Me.btnOutExcelFisrtLast.TabIndex = 74
        Me.btnOutExcelFisrtLast.Text = "导出至EXCEL"
        '
        'dataFirLast
        '
        Me.dataFirLast.AllowUserToAddRows = False
        Me.dataFirLast.AllowUserToDeleteRows = False
        Me.dataFirLast.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dataFirLast.ColumnHeadersHeight = 25
        Me.dataFirLast.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn12, Me.DataGridViewTextBoxColumn13, Me.DataGridViewTextBoxColumn14, Me.DataGridViewTextBoxColumn16, Me.DataGridViewTextBoxColumn15, Me.上行末班车})
        Me.dataFirLast.Location = New System.Drawing.Point(8, 13)
        Me.dataFirLast.Name = "dataFirLast"
        Me.dataFirLast.RowHeadersWidth = 30
        Me.dataFirLast.RowTemplate.Height = 23
        Me.dataFirLast.Size = New System.Drawing.Size(649, 360)
        Me.dataFirLast.TabIndex = 73
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.HeaderText = "序号"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.Width = 40
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.HeaderText = "车站名称"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.Width = 120
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.HeaderText = "下行首班车"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.Width = 120
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.HeaderText = "下行末班车"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.Width = 120
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.HeaderText = "上行首班车"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.Width = 120
        '
        '上行末班车
        '
        Me.上行末班车.HeaderText = "上行末班车"
        Me.上行末班车.Name = "上行末班车"
        Me.上行末班车.Width = 120
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Button1)
        Me.TabPage4.Controls.Add(Me.DgdWholeIndex)
        Me.TabPage4.Location = New System.Drawing.Point(4, 21)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(661, 411)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "综合指标"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(525, 381)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(123, 23)
        Me.Button1.TabIndex = 74
        Me.Button1.Text = "导出至EXCEL"
        '
        'DgdWholeIndex
        '
        Me.DgdWholeIndex.AllowUserToAddRows = False
        Me.DgdWholeIndex.AllowUserToDeleteRows = False
        Me.DgdWholeIndex.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DgdWholeIndex.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.指标名, Me.指标值})
        Me.DgdWholeIndex.Location = New System.Drawing.Point(3, 7)
        Me.DgdWholeIndex.Name = "DgdWholeIndex"
        Me.DgdWholeIndex.ReadOnly = True
        Me.DgdWholeIndex.RowHeadersWidth = 30
        Me.DgdWholeIndex.RowTemplate.Height = 23
        Me.DgdWholeIndex.Size = New System.Drawing.Size(645, 361)
        Me.DgdWholeIndex.TabIndex = 72
        '
        '指标名
        '
        Me.指标名.HeaderText = "指标名"
        Me.指标名.Name = "指标名"
        Me.指标名.ReadOnly = True
        Me.指标名.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.指标名.Width = 200
        '
        '指标值
        '
        Me.指标值.HeaderText = "指标值"
        Me.指标值.Name = "指标值"
        Me.指标值.ReadOnly = True
        Me.指标值.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.指标值.Width = 220
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(491, 454)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(99, 23)
        Me.btnSave.TabIndex = 74
        Me.btnSave.Text = "保存指标(&S)"
        '
        'btnAllOutput
        '
        Me.btnAllOutput.Location = New System.Drawing.Point(351, 454)
        Me.btnAllOutput.Name = "btnAllOutput"
        Me.btnAllOutput.Size = New System.Drawing.Size(123, 23)
        Me.btnAllOutput.TabIndex = 75
        Me.btnAllOutput.Text = "全部导出至EXCEL"
        '
        'btnReCal
        '
        Me.btnReCal.Location = New System.Drawing.Point(12, 454)
        Me.btnReCal.Name = "btnReCal"
        Me.btnReCal.Size = New System.Drawing.Size(116, 23)
        Me.btnReCal.TabIndex = 82
        Me.btnReCal.Text = "全部重新计算"
        '
        'frmODSDiagramIndex
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(693, 492)
        Me.Controls.Add(Me.btnReCal)
        Me.Controls.Add(Me.btnAllOutput)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmODSDiagramIndex"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "运行图指标"
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dataGridrouting, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        CType(Me.dgrvPeriod, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dataGridChedi, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        CType(Me.dataFirLast, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.DgdWholeIndex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents btnSaveToExcel As System.Windows.Forms.Button
    Friend WithEvents dataGridrouting As System.Windows.Forms.DataGridView
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCal As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents dataGridChedi As System.Windows.Forms.DataGridView
    Friend WithEvents btOutCheDitoExcel As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 车底名称 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 连挂列车数 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 连挂车次 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmbTimeFormate As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DgdWholeIndex As System.Windows.Forms.DataGridView
    Friend WithEvents 指标名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 指标值 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgrvPeriod As System.Windows.Forms.DataGridView
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnAllOutput As System.Windows.Forms.Button
    Friend WithEvents btnOutExcel2 As System.Windows.Forms.Button
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents dataFirLast As System.Windows.Forms.DataGridView
    Friend WithEvents btnOutExcelFisrtLast As System.Windows.Forms.Button
    Friend WithEvents btnReCal As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 上行末班车 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 序号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 交路名称 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 运行标尺 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 停站标尺 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 开行数量 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 纯运行时分 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 总停站时分 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 总运行时分 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 旅行速度 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 技术速度 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 走行公里数 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1序号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 区间名称 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 结束时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 下行开行列数 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 上行开行列数 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 下行间隔 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 上行间隔 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 车底数量 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
