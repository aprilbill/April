<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmCheDiInformation
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
        Me.dataGrid = New System.Windows.Forms.DataGridView
        Me.项目名1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.属性值 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.DataGridTrain = New System.Windows.Forms.DataGridView
        Me.序号 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.车次 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.发点 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.到点 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.d = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridIndex = New System.Windows.Forms.DataGridView
        Me.车底属性设置 = New System.Windows.Forms.TabPage
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnApplyAll = New System.Windows.Forms.Button
        Me.chkAutoResetBianHao = New System.Windows.Forms.CheckBox
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
        Me.属性值3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.项目名2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.计划运行时分 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.实际时分 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.区间名 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.序号2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.停站 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.c = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.到点1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.车站名 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.序号1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.chkAllTrains = New System.Windows.Forms.CheckBox
        Me.TabPage1.SuspendLayout()
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        CType(Me.DataGridTrain, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridIndex, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.车底属性设置.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.picTimeLineShow, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numLineWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(302, 493)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 73
        Me.btnOK.Text = "确定(&Y)"
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dataGrid)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(357, 440)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "基本信息"
        '
        'dataGrid
        '
        Me.dataGrid.AllowUserToAddRows = False
        Me.dataGrid.AllowUserToDeleteRows = False
        Me.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dataGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.项目名1, Me.属性值})
        Me.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dataGrid.Location = New System.Drawing.Point(3, 3)
        Me.dataGrid.Name = "dataGrid"
        Me.dataGrid.RowHeadersWidth = 30
        Me.dataGrid.RowTemplate.Height = 23
        Me.dataGrid.Size = New System.Drawing.Size(351, 434)
        Me.dataGrid.TabIndex = 71
        '
        '项目名1
        '
        Me.项目名1.HeaderText = "项目名"
        Me.项目名1.Name = "项目名1"
        Me.项目名1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.项目名1.Width = 120
        '
        '属性值
        '
        Me.属性值.HeaderText = "属性值"
        Me.属性值.Name = "属性值"
        Me.属性值.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.属性值.Width = 220
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.车底属性设置)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(365, 465)
        Me.TabControl1.TabIndex = 72
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.DataGridTrain)
        Me.TabPage4.Controls.Add(Me.DataGridIndex)
        Me.TabPage4.Location = New System.Drawing.Point(4, 21)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(357, 440)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "接续车次"
        '
        'DataGridTrain
        '
        Me.DataGridTrain.AllowUserToAddRows = False
        Me.DataGridTrain.AllowUserToDeleteRows = False
        Me.DataGridTrain.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridTrain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.序号, Me.车次, Me.发点, Me.到点, Me.d})
        Me.DataGridTrain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridTrain.Location = New System.Drawing.Point(0, 0)
        Me.DataGridTrain.Name = "DataGridTrain"
        Me.DataGridTrain.RowHeadersWidth = 20
        Me.DataGridTrain.RowTemplate.Height = 23
        Me.DataGridTrain.Size = New System.Drawing.Size(357, 440)
        Me.DataGridTrain.TabIndex = 73
        '
        '序号
        '
        Me.序号.HeaderText = "序号"
        Me.序号.Name = "序号"
        Me.序号.Width = 40
        '
        '车次
        '
        Me.车次.HeaderText = "车次"
        Me.车次.Name = "车次"
        Me.车次.Width = 60
        '
        '发点
        '
        Me.发点.HeaderText = "发点"
        Me.发点.Name = "发点"
        Me.发点.Width = 60
        '
        '到点
        '
        Me.到点.HeaderText = "到点"
        Me.到点.Name = "到点"
        Me.到点.Width = 60
        '
        'd
        '
        Me.d.HeaderText = "折返时间(S)"
        Me.d.Name = "d"
        Me.d.Width = 80
        '
        'DataGridIndex
        '
        Me.DataGridIndex.AllowUserToAddRows = False
        Me.DataGridIndex.AllowUserToDeleteRows = False
        Me.DataGridIndex.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataGridIndex.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridIndex.Location = New System.Drawing.Point(0, 0)
        Me.DataGridIndex.Name = "DataGridIndex"
        Me.DataGridIndex.RowHeadersWidth = 30
        Me.DataGridIndex.RowTemplate.Height = 23
        Me.DataGridIndex.Size = New System.Drawing.Size(357, 440)
        Me.DataGridIndex.TabIndex = 72
        '
        '车底属性设置
        '
        Me.车底属性设置.Controls.Add(Me.GroupBox1)
        Me.车底属性设置.Controls.Add(Me.GroupBox2)
        Me.车底属性设置.Location = New System.Drawing.Point(4, 21)
        Me.车底属性设置.Name = "车底属性设置"
        Me.车底属性设置.Size = New System.Drawing.Size(357, 440)
        Me.车底属性设置.TabIndex = 4
        Me.车底属性设置.Text = "车底属性设置"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnApplyAll)
        Me.GroupBox1.Controls.Add(Me.chkAutoResetBianHao)
        Me.GroupBox1.Location = New System.Drawing.Point(19, 19)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(317, 78)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "车底属性"
        '
        'btnApplyAll
        '
        Me.btnApplyAll.Location = New System.Drawing.Point(194, 33)
        Me.btnApplyAll.Name = "btnApplyAll"
        Me.btnApplyAll.Size = New System.Drawing.Size(117, 23)
        Me.btnApplyAll.TabIndex = 74
        Me.btnApplyAll.Text = "应用到所有车底"
        '
        'chkAutoResetBianHao
        '
        Me.chkAutoResetBianHao.AutoSize = True
        Me.chkAutoResetBianHao.Location = New System.Drawing.Point(18, 33)
        Me.chkAutoResetBianHao.Name = "chkAutoResetBianHao"
        Me.chkAutoResetBianHao.Size = New System.Drawing.Size(132, 16)
        Me.chkAutoResetBianHao.TabIndex = 1
        Me.chkAutoResetBianHao.Text = "车底不参与自动铺画"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chkAllTrains)
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
        Me.GroupBox2.Location = New System.Drawing.Point(19, 122)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(317, 279)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "线型与颜色"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(45, 245)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(197, 12)
        Me.Label1.TabIndex = 83
        Me.Label1.Text = "需要应用当前设置，请按""应用""按纽"
        '
        'picTimeLineShow
        '
        Me.picTimeLineShow.Location = New System.Drawing.Point(88, 133)
        Me.picTimeLineShow.Name = "picTimeLineShow"
        Me.picTimeLineShow.Size = New System.Drawing.Size(159, 26)
        Me.picTimeLineShow.TabIndex = 82
        Me.picTimeLineShow.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(45, 145)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(35, 12)
        Me.Label9.TabIndex = 81
        Me.Label9.Text = "预览:"
        '
        'labTimeLineColor
        '
        Me.labTimeLineColor.BackColor = System.Drawing.Color.Green
        Me.labTimeLineColor.ForeColor = System.Drawing.Color.Green
        Me.labTimeLineColor.Location = New System.Drawing.Point(86, 100)
        Me.labTimeLineColor.Name = "labTimeLineColor"
        Me.labTimeLineColor.Size = New System.Drawing.Size(121, 22)
        Me.labTimeLineColor.TabIndex = 80
        '
        'btnTimeLineColor
        '
        Me.btnTimeLineColor.Location = New System.Drawing.Point(208, 101)
        Me.btnTimeLineColor.Name = "btnTimeLineColor"
        Me.btnTimeLineColor.Size = New System.Drawing.Size(39, 22)
        Me.btnTimeLineColor.TabIndex = 79
        Me.btnTimeLineColor.Text = "..."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(45, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 12)
        Me.Label3.TabIndex = 78
        Me.Label3.Text = "颜色:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(45, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 12)
        Me.Label5.TabIndex = 77
        Me.Label5.Text = "线粗:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(45, 35)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(35, 12)
        Me.Label6.TabIndex = 76
        Me.Label6.Text = "线型:"
        '
        'numLineWidth
        '
        Me.numLineWidth.DecimalPlaces = 1
        Me.numLineWidth.Increment = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.numLineWidth.Location = New System.Drawing.Point(86, 70)
        Me.numLineWidth.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.numLineWidth.Name = "numLineWidth"
        Me.numLineWidth.Size = New System.Drawing.Size(161, 21)
        Me.numLineWidth.TabIndex = 75
        Me.numLineWidth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cmbLineStyle
        '
        Me.cmbLineStyle.FormattingEnabled = True
        Me.cmbLineStyle.Location = New System.Drawing.Point(86, 32)
        Me.cmbLineStyle.Name = "cmbLineStyle"
        Me.cmbLineStyle.Size = New System.Drawing.Size(161, 20)
        Me.cmbLineStyle.TabIndex = 74
        Me.cmbLineStyle.Text = "实线 ────────"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(139, 206)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(108, 23)
        Me.Button1.TabIndex = 73
        Me.Button1.Text = "应用"
        '
        '属性值3
        '
        Me.属性值3.HeaderText = "属性值"
        Me.属性值3.Name = "属性值3"
        Me.属性值3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.属性值3.Width = 220
        '
        '项目名2
        '
        Me.项目名2.HeaderText = "项目名"
        Me.项目名2.Name = "项目名2"
        Me.项目名2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.项目名2.Width = 120
        '
        '计划运行时分
        '
        Me.计划运行时分.HeaderText = "计划时分"
        Me.计划运行时分.Name = "计划运行时分"
        Me.计划运行时分.Width = 75
        '
        '实际时分
        '
        Me.实际时分.HeaderText = "实际时分"
        Me.实际时分.Name = "实际时分"
        Me.实际时分.Width = 75
        '
        '区间名
        '
        Me.区间名.HeaderText = "区间名"
        Me.区间名.Name = "区间名"
        Me.区间名.Width = 170
        '
        '序号2
        '
        Me.序号2.HeaderText = "序号"
        Me.序号2.Name = "序号2"
        Me.序号2.Width = 40
        '
        '停站
        '
        Me.停站.HeaderText = "停站(S)"
        Me.停站.Name = "停站"
        Me.停站.Width = 60
        '
        'c
        '
        Me.c.HeaderText = "发点"
        Me.c.Name = "c"
        Me.c.Width = 75
        '
        '到点1
        '
        Me.到点1.HeaderText = "到点"
        Me.到点1.Name = "到点1"
        Me.到点1.Width = 75
        '
        '车站名
        '
        Me.车站名.HeaderText = "车站名"
        Me.车站名.Name = "车站名"
        '
        '序号1
        '
        Me.序号1.HeaderText = "序号"
        Me.序号1.Name = "序号1"
        Me.序号1.Width = 40
        '
        'chkAllTrains
        '
        Me.chkAllTrains.AutoSize = True
        Me.chkAllTrains.Location = New System.Drawing.Point(47, 172)
        Me.chkAllTrains.Name = "chkAllTrains"
        Me.chkAllTrains.Size = New System.Drawing.Size(168, 16)
        Me.chkAllTrains.TabIndex = 84
        Me.chkAllTrains.Text = "同样应用于该车底所有列车"
        Me.chkAllTrains.UseVisualStyleBackColor = True
        '
        'frmCheDiInformation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(390, 528)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "frmCheDiInformation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "车底信息"
        Me.TabPage1.ResumeLayout(False)
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        CType(Me.DataGridTrain, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridIndex, System.ComponentModel.ISupportInitialize).EndInit()
        Me.车底属性设置.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.picTimeLineShow, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numLineWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents dataGrid As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridIndex As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridTrain As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 车底属性设置 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents picTimeLineShow As System.Windows.Forms.PictureBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents labTimeLineColor As System.Windows.Forms.Label
    Friend WithEvents btnTimeLineColor As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents numLineWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents cmbLineStyle As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkAutoResetBianHao As System.Windows.Forms.CheckBox
    Friend WithEvents 项目名1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 属性值 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 序号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 车次 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 发点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 到点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents d As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 属性值3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 项目名2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 计划运行时分 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 实际时分 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 区间名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 序号2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 停站 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents c As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 到点1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 车站名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 序号1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnApplyAll As System.Windows.Forms.Button
    Friend WithEvents chkAllTrains As System.Windows.Forms.CheckBox
End Class
