<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShowStaTimetable
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
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnExcel = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnDiagram = New System.Windows.Forms.Button
        Me.dgv = New System.Windows.Forms.DataGridView
        Me.tab = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.车次 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CurVersion = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.发点 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.交路 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.dgvUp = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Panel1.SuspendLayout()
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tab.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvUp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.Location = New System.Drawing.Point(576, 14)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 83
        Me.btnExit.Text = "退出(&E)"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnExcel
        '
        Me.btnExcel.Location = New System.Drawing.Point(127, 14)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(143, 23)
        Me.btnExcel.TabIndex = 84
        Me.btnExcel.Text = "导出至EXCEL表格"
        Me.btnExcel.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnDiagram)
        Me.Panel1.Controls.Add(Me.btnExcel)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 430)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(663, 49)
        Me.Panel1.TabIndex = 85
        '
        'btnDiagram
        '
        Me.btnDiagram.Location = New System.Drawing.Point(12, 14)
        Me.btnDiagram.Name = "btnDiagram"
        Me.btnDiagram.Size = New System.Drawing.Size(109, 23)
        Me.btnDiagram.TabIndex = 85
        Me.btnDiagram.Text = "查看运行图"
        Me.btnDiagram.UseVisualStyleBackColor = True
        '
        'dgv
        '
        Me.dgv.AllowUserToAddRows = False
        Me.dgv.AllowUserToDeleteRows = False
        Me.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgv.ColumnHeadersHeight = 25
        Me.dgv.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9, Me.车次, Me.CurVersion, Me.发点, Me.交路})
        Me.dgv.Location = New System.Drawing.Point(2, 1)
        Me.dgv.Name = "dgv"
        Me.dgv.ReadOnly = True
        Me.dgv.RowHeadersWidth = 30
        Me.dgv.RowTemplate.Height = 23
        Me.dgv.Size = New System.Drawing.Size(634, 389)
        Me.dgv.TabIndex = 86
        '
        'tab
        '
        Me.tab.Controls.Add(Me.TabPage1)
        Me.tab.Controls.Add(Me.TabPage2)
        Me.tab.Location = New System.Drawing.Point(6, 5)
        Me.tab.Name = "tab"
        Me.tab.SelectedIndex = 0
        Me.tab.Size = New System.Drawing.Size(649, 421)
        Me.tab.TabIndex = 87
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dgv)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(641, 396)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "下行"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.dgvUp)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(641, 396)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "上行"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "序号"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Width = 40
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "线路名称"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        '
        '车次
        '
        Me.车次.HeaderText = "车次"
        Me.车次.Name = "车次"
        Me.车次.ReadOnly = True
        Me.车次.Width = 60
        '
        'CurVersion
        '
        Me.CurVersion.HeaderText = "到点"
        Me.CurVersion.Name = "CurVersion"
        Me.CurVersion.ReadOnly = True
        Me.CurVersion.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        '发点
        '
        Me.发点.HeaderText = "发点"
        Me.发点.Name = "发点"
        Me.发点.ReadOnly = True
        '
        '交路
        '
        Me.交路.HeaderText = "交路　"
        Me.交路.Name = "交路"
        Me.交路.ReadOnly = True
        Me.交路.Width = 200
        '
        'dgvUp
        '
        Me.dgvUp.AllowUserToAddRows = False
        Me.dgvUp.AllowUserToDeleteRows = False
        Me.dgvUp.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dgvUp.ColumnHeadersHeight = 25
        Me.dgvUp.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6})
        Me.dgvUp.Location = New System.Drawing.Point(3, 4)
        Me.dgvUp.Name = "dgvUp"
        Me.dgvUp.ReadOnly = True
        Me.dgvUp.RowHeadersWidth = 30
        Me.dgvUp.RowTemplate.Height = 23
        Me.dgvUp.Size = New System.Drawing.Size(634, 389)
        Me.dgvUp.TabIndex = 87
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "序号"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 40
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "线路名称"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "车次"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 60
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "到点"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "发点"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "交路　"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Width = 200
        '
        'frmShowStaTimetable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(663, 479)
        Me.Controls.Add(Me.tab)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmShowStaTimetable"
        Me.Text = "显示车站时刻表"
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgv, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tab.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgvUp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents dgv As System.Windows.Forms.DataGridView
    Friend WithEvents btnDiagram As System.Windows.Forms.Button
    Friend WithEvents tab As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 车次 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CurVersion As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 发点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 交路 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvUp As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
