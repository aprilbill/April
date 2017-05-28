<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmTrainInfor
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
        Me.TabInf = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.dataGrid = New System.Windows.Forms.DataGridView
        Me.项目名 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.属性值 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.DataGridTime = New System.Windows.Forms.DataGridView
        Me.序号 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.车站名 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.到点 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.发点 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.实际停时 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.停站股道 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.标尺停站 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.停站标尺 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.DataGridSectime = New System.Windows.Forms.DataGridView
        Me.序号2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.区间名 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.实际时分 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.纯运行时分 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.计划运行时分 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.DataGridIndex = New System.Windows.Forms.DataGridView
        Me.项目名2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.属性值2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.picTimeLineShow = New System.Windows.Forms.PictureBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.labTimeLineColor = New System.Windows.Forms.Label
        Me.btnTimeLineColor = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.numLineWidth = New System.Windows.Forms.NumericUpDown
        Me.cmbLineStyle = New System.Windows.Forms.ComboBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.chkZheFanYueShu = New System.Windows.Forms.CheckBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.项目名3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.属性值3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TabInf.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.DataGridTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        CType(Me.DataGridSectime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage4.SuspendLayout()
        CType(Me.DataGridIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.picTimeLineShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numLineWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabInf
        '
        Me.TabInf.Controls.Add(Me.TabPage1)
        Me.TabInf.Controls.Add(Me.TabPage2)
        Me.TabInf.Controls.Add(Me.TabPage3)
        Me.TabInf.Controls.Add(Me.TabPage4)
        Me.TabInf.Controls.Add(Me.TabPage5)
        Me.TabInf.Location = New System.Drawing.Point(12, 12)
        Me.TabInf.Name = "TabInf"
        Me.TabInf.SelectedIndex = 0
        Me.TabInf.Size = New System.Drawing.Size(380, 432)
        Me.TabInf.TabIndex = 71
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dataGrid)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(372, 407)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "基本信息"
        '
        'dataGrid
        '
        Me.dataGrid.AllowUserToAddRows = False
        Me.dataGrid.AllowUserToDeleteRows = False
        Me.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dataGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.项目名, Me.属性值})
        Me.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dataGrid.Location = New System.Drawing.Point(3, 3)
        Me.dataGrid.Name = "dataGrid"
        Me.dataGrid.RowHeadersWidth = 30
        Me.dataGrid.RowTemplate.Height = 23
        Me.dataGrid.Size = New System.Drawing.Size(366, 401)
        Me.dataGrid.TabIndex = 71
        '
        '项目名
        '
        Me.项目名.HeaderText = "项目名"
        Me.项目名.Name = "项目名"
        Me.项目名.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.项目名.Width = 120
        '
        '属性值
        '
        Me.属性值.HeaderText = "属性值"
        Me.属性值.Name = "属性值"
        Me.属性值.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.属性值.Width = 220
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.DataGridTime)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(372, 407)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "时刻表"
        '
        'DataGridTime
        '
        Me.DataGridTime.AllowUserToAddRows = False
        Me.DataGridTime.AllowUserToDeleteRows = False
        Me.DataGridTime.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridTime.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.序号, Me.车站名, Me.到点, Me.发点, Me.实际停时, Me.停站股道, Me.标尺停站, Me.停站标尺})
        Me.DataGridTime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridTime.Location = New System.Drawing.Point(3, 3)
        Me.DataGridTime.Name = "DataGridTime"
        Me.DataGridTime.RowHeadersWidth = 20
        Me.DataGridTime.RowTemplate.Height = 23
        Me.DataGridTime.Size = New System.Drawing.Size(366, 401)
        Me.DataGridTime.TabIndex = 72
        '
        '序号
        '
        Me.序号.HeaderText = "序号"
        Me.序号.Name = "序号"
        Me.序号.Width = 40
        '
        '车站名
        '
        Me.车站名.HeaderText = "车站名"
        Me.车站名.Name = "车站名"
        '
        '到点
        '
        Me.到点.HeaderText = "到点"
        Me.到点.Name = "到点"
        Me.到点.Width = 75
        '
        '发点
        '
        Me.发点.HeaderText = "发点"
        Me.发点.Name = "发点"
        Me.发点.Width = 75
        '
        '实际停时
        '
        Me.实际停时.HeaderText = "实际停时(S)"
        Me.实际停时.Name = "实际停时"
        Me.实际停时.Width = 60
        '
        '停站股道
        '
        Me.停站股道.HeaderText = "停站股道"
        Me.停站股道.Name = "停站股道"
        Me.停站股道.Width = 50
        '
        '标尺停站
        '
        Me.标尺停站.HeaderText = "标尺停时(S)"
        Me.标尺停站.Name = "标尺停站"
        Me.标尺停站.Width = 50
        '
        '停站标尺
        '
        Me.停站标尺.HeaderText = "停站标尺"
        Me.停站标尺.Name = "停站标尺"
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.DataGridSectime)
        Me.TabPage3.Location = New System.Drawing.Point(4, 21)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(372, 407)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "区间时分"
        '
        'DataGridSectime
        '
        Me.DataGridSectime.AllowUserToAddRows = False
        Me.DataGridSectime.AllowUserToDeleteRows = False
        Me.DataGridSectime.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridSectime.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.序号2, Me.区间名, Me.实际时分, Me.纯运行时分, Me.计划运行时分})
        Me.DataGridSectime.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridSectime.Location = New System.Drawing.Point(0, 0)
        Me.DataGridSectime.Name = "DataGridSectime"
        Me.DataGridSectime.RowHeadersWidth = 20
        Me.DataGridSectime.RowTemplate.Height = 23
        Me.DataGridSectime.Size = New System.Drawing.Size(372, 407)
        Me.DataGridSectime.TabIndex = 73
        '
        '序号2
        '
        Me.序号2.HeaderText = "序号"
        Me.序号2.Name = "序号2"
        Me.序号2.Width = 40
        '
        '区间名
        '
        Me.区间名.HeaderText = "区间名"
        Me.区间名.Name = "区间名"
        Me.区间名.Width = 170
        '
        '实际时分
        '
        Me.实际时分.HeaderText = "实际时分"
        Me.实际时分.Name = "实际时分"
        Me.实际时分.Width = 75
        '
        '纯运行时分
        '
        Me.纯运行时分.HeaderText = "纯运行时分"
        Me.纯运行时分.Name = "纯运行时分"
        '
        '计划运行时分
        '
        Me.计划运行时分.HeaderText = "运行标尺"
        Me.计划运行时分.Name = "计划运行时分"
        Me.计划运行时分.Width = 75
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.DataGridIndex)
        Me.TabPage4.Location = New System.Drawing.Point(4, 21)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(372, 407)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "列车指标"
        '
        'DataGridIndex
        '
        Me.DataGridIndex.AllowUserToAddRows = False
        Me.DataGridIndex.AllowUserToDeleteRows = False
        Me.DataGridIndex.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridIndex.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.项目名2, Me.属性值2})
        Me.DataGridIndex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridIndex.Location = New System.Drawing.Point(0, 0)
        Me.DataGridIndex.Name = "DataGridIndex"
        Me.DataGridIndex.RowHeadersWidth = 30
        Me.DataGridIndex.RowTemplate.Height = 23
        Me.DataGridIndex.Size = New System.Drawing.Size(372, 407)
        Me.DataGridIndex.TabIndex = 72
        '
        '项目名2
        '
        Me.项目名2.HeaderText = "项目名"
        Me.项目名2.Name = "项目名2"
        Me.项目名2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.项目名2.Width = 120
        '
        '属性值2
        '
        Me.属性值2.HeaderText = "属性值"
        Me.属性值2.Name = "属性值2"
        Me.属性值2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.属性值2.Width = 220
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.GroupBox2)
        Me.TabPage5.Controls.Add(Me.GroupBox1)
        Me.TabPage5.Location = New System.Drawing.Point(4, 21)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Size = New System.Drawing.Size(372, 407)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "列车属性设置"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.picTimeLineShow)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.labTimeLineColor)
        Me.GroupBox2.Controls.Add(Me.btnTimeLineColor)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.numLineWidth)
        Me.GroupBox2.Controls.Add(Me.cmbLineStyle)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Location = New System.Drawing.Point(23, 120)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(320, 247)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "线型与颜色"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 223)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(197, 12)
        Me.Label1.TabIndex = 77
        Me.Label1.Text = "需要应用当前设置，请按""应用""按纽"
        '
        'picTimeLineShow
        '
        Me.picTimeLineShow.Location = New System.Drawing.Point(89, 133)
        Me.picTimeLineShow.Name = "picTimeLineShow"
        Me.picTimeLineShow.Size = New System.Drawing.Size(159, 26)
        Me.picTimeLineShow.TabIndex = 82
        Me.picTimeLineShow.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(35, 145)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 12)
        Me.Label9.TabIndex = 81
        Me.Label9.Text = "预览:"
        '
        'labTimeLineColor
        '
        Me.labTimeLineColor.BackColor = System.Drawing.Color.Green
        Me.labTimeLineColor.ForeColor = System.Drawing.Color.Green
        Me.labTimeLineColor.Location = New System.Drawing.Point(87, 101)
        Me.labTimeLineColor.Name = "labTimeLineColor"
        Me.labTimeLineColor.Size = New System.Drawing.Size(121, 22)
        Me.labTimeLineColor.TabIndex = 80
        '
        'btnTimeLineColor
        '
        Me.btnTimeLineColor.Location = New System.Drawing.Point(209, 101)
        Me.btnTimeLineColor.Name = "btnTimeLineColor"
        Me.btnTimeLineColor.Size = New System.Drawing.Size(39, 22)
        Me.btnTimeLineColor.TabIndex = 79
        Me.btnTimeLineColor.Text = "..."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(35, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 12)
        Me.Label3.TabIndex = 78
        Me.Label3.Text = "颜色:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(35, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 12)
        Me.Label5.TabIndex = 77
        Me.Label5.Text = "线粗:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(35, 35)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 12)
        Me.Label6.TabIndex = 76
        Me.Label6.Text = "线型:"
        '
        'numLineWidth
        '
        Me.numLineWidth.DecimalPlaces = 1
        Me.numLineWidth.Increment = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.numLineWidth.Location = New System.Drawing.Point(87, 70)
        Me.numLineWidth.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numLineWidth.Name = "numLineWidth"
        Me.numLineWidth.Size = New System.Drawing.Size(161, 21)
        Me.numLineWidth.TabIndex = 75
        Me.numLineWidth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cmbLineStyle
        '
        Me.cmbLineStyle.FormattingEnabled = True
        Me.cmbLineStyle.Location = New System.Drawing.Point(87, 32)
        Me.cmbLineStyle.Name = "cmbLineStyle"
        Me.cmbLineStyle.Size = New System.Drawing.Size(161, 20)
        Me.cmbLineStyle.TabIndex = 74
        Me.cmbLineStyle.Text = "实线 ────────"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(150, 178)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(98, 23)
        Me.Button1.TabIndex = 73
        Me.Button1.Text = "应用(A)"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkZheFanYueShu)
        Me.GroupBox1.Location = New System.Drawing.Point(23, 23)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(320, 71)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "列车属性"
        '
        'chkZheFanYueShu
        '
        Me.chkZheFanYueShu.AutoSize = True
        Me.chkZheFanYueShu.Location = New System.Drawing.Point(25, 34)
        Me.chkZheFanYueShu.Name = "chkZheFanYueShu"
        Me.chkZheFanYueShu.Size = New System.Drawing.Size(120, 16)
        Me.chkZheFanYueShu.TabIndex = 0
        Me.chkZheFanYueShu.Text = "折返条件不受约束"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(317, 464)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 72
        Me.btnOK.Text = "确定(&Y)"
        '
        '项目名3
        '
        Me.项目名3.HeaderText = "项目名"
        Me.项目名3.Name = "项目名3"
        Me.项目名3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        '属性值3
        '
        Me.属性值3.HeaderText = "属性值"
        Me.属性值3.Name = "属性值3"
        Me.属性值3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.属性值3.Width = 150
        '
        'frmTrainInfor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(404, 499)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.TabInf)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTrainInfor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "列车信息"
        Me.TabInf.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.DataGridTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        CType(Me.DataGridSectime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage4.ResumeLayout(False)
        CType(Me.DataGridIndex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.picTimeLineShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numLineWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabInf As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents dataGrid As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents DataGridTime As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridSectime As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridIndex As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents chkZheFanYueShu As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents picTimeLineShow As System.Windows.Forms.PictureBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents labTimeLineColor As System.Windows.Forms.Label
    Friend WithEvents btnTimeLineColor As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents numLineWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmbLineStyle As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 项目名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 属性值 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 序号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 车站名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 到点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 发点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 实际停时 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 停站股道 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 标尺停站 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 停站标尺 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 序号2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 区间名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 实际时分 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 纯运行时分 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 计划运行时分 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 项目名2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 属性值2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 项目名3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 属性值3 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
