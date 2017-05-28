<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmODSNetDiaIndex
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
        Me.btnExcel = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.dgv = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.车次 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.交路 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.上下行 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CurVersion = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.发点 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.载客列次 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.空驶列次 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.平均旅速 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.平均技速 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.总走行公里 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.运用车底数 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Panel1 = New System.Windows.Forms.Panel
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExcel
        '
        Me.btnExcel.Location = New System.Drawing.Point(12, 14)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(137, 23)
        Me.btnExcel.TabIndex = 84
        Me.btnExcel.Text = "导出至EXCEL表格"
        Me.btnExcel.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.Location = New System.Drawing.Point(580, 14)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 83
        Me.btnExit.Text = "退出(&E)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgv.ColumnHeadersHeight = 25
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9, Me.车次, Me.交路, Me.上下行, Me.CurVersion, Me.发点, Me.载客列次, Me.空驶列次, Me.平均旅速, Me.平均技速, Me.总走行公里, Me.运用车底数})
        Me.dgv.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv.Location = New System.Drawing.Point(0, 0)
        Me.dgv.Name = "dgv"
        Me.dgv.RowHeadersWidth = 30
        Me.dgv.RowTemplate.Height = 23
        Me.dgv.Size = New System.Drawing.Size(667, 372)
        Me.dgv.TabIndex = 88
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "序号"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.Width = 40
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "线路名称"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        '
        '车次
        '
        Me.车次.HeaderText = "下行列次"
        Me.车次.Name = "车次"
        '
        '交路
        '
        Me.交路.HeaderText = "上行列次"
        Me.交路.Name = "交路"
        '
        '上下行
        '
        Me.上下行.HeaderText = "总列次"
        Me.上下行.Name = "上下行"
        '
        'CurVersion
        '
        Me.CurVersion.HeaderText = "轧道车数"
        Me.CurVersion.Name = "CurVersion"
        Me.CurVersion.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        '发点
        '
        Me.发点.HeaderText = "调试车数"
        Me.发点.Name = "发点"
        '
        '载客列次
        '
        Me.载客列次.HeaderText = "载客列次"
        Me.载客列次.Name = "载客列次"
        '
        '空驶列次
        '
        Me.空驶列次.HeaderText = "空驶列次"
        Me.空驶列次.Name = "空驶列次"
        '
        '平均旅速
        '
        Me.平均旅速.HeaderText = "平均旅速(km/h)"
        Me.平均旅速.Name = "平均旅速"
        '
        '平均技速
        '
        Me.平均技速.HeaderText = "平均技速(km/h)"
        Me.平均技速.Name = "平均技速"
        '
        '总走行公里
        '
        Me.总走行公里.HeaderText = "总走行公里(车公里)"
        Me.总走行公里.Name = "总走行公里"
        '
        '运用车底数
        '
        Me.运用车底数.HeaderText = "运用车底数"
        Me.运用车底数.Name = "运用车底数"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnExcel)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 372)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(667, 49)
        Me.Panel1.TabIndex = 87
        '
        'frmODSNetDiaIndex
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(667, 421)
        Me.Controls.Add(Me.dgv)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmODSNetDiaIndex"
        Me.Text = "路网运行图指标"
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 车次 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 交路 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 上下行 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CurVersion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 发点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 载客列次 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 空驶列次 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 平均旅速 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 平均技速 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 总走行公里 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 运用车底数 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
