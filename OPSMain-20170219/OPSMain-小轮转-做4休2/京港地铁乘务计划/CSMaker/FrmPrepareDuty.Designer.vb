<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPrepareDuty
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
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
        CType(Me.DGVBeiBan, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGVBeiche, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(403, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "备班参数："
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "备车参数："
        '
        'Button13
        '
        Me.Button13.Location = New System.Drawing.Point(590, 359)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(75, 23)
        Me.Button13.TabIndex = 15
        Me.Button13.Text = "删除"
        Me.Button13.UseVisualStyleBackColor = True
        '
        'Button14
        '
        Me.Button14.Location = New System.Drawing.Point(499, 359)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(75, 23)
        Me.Button14.TabIndex = 14
        Me.Button14.Text = "添加"
        Me.Button14.UseVisualStyleBackColor = True
        '
        'DGVBeiBan
        '
        Me.DGVBeiBan.AllowUserToAddRows = False
        Me.DGVBeiBan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVBeiBan.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.班种2, Me.备班名称, Me.备班地点, Me.开始时间2, Me.结束时间2, Me.所属区域2})
        Me.DGVBeiBan.Location = New System.Drawing.Point(405, 34)
        Me.DGVBeiBan.Name = "DGVBeiBan"
        Me.DGVBeiBan.RowHeadersVisible = False
        Me.DGVBeiBan.RowTemplate.Height = 23
        Me.DGVBeiBan.Size = New System.Drawing.Size(366, 319)
        Me.DGVBeiBan.TabIndex = 13
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
        Me.Button12.Location = New System.Drawing.Point(193, 359)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(75, 23)
        Me.Button12.TabIndex = 12
        Me.Button12.Text = "删除"
        Me.Button12.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(100, 359)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "添加"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DGVBeiche
        '
        Me.DGVBeiche.AllowUserToAddRows = False
        Me.DGVBeiche.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVBeiche.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.班种, Me.备车名称, Me.备车地点, Me.开始时间, Me.结束时间, Me.所属区域})
        Me.DGVBeiche.Location = New System.Drawing.Point(12, 34)
        Me.DGVBeiche.Name = "DGVBeiche"
        Me.DGVBeiche.RowHeadersVisible = False
        Me.DGVBeiche.RowTemplate.Height = 23
        Me.DGVBeiche.Size = New System.Drawing.Size(362, 319)
        Me.DGVBeiche.TabIndex = 10
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
        'FrmPrepareDuty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(786, 397)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Button13)
        Me.Controls.Add(Me.Button14)
        Me.Controls.Add(Me.DGVBeiBan)
        Me.Controls.Add(Me.Button12)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DGVBeiche)
        Me.Name = "FrmPrepareDuty"
        Me.Text = "备车备班设置"
        CType(Me.DGVBeiBan, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGVBeiche, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents DGVBeiBan As System.Windows.Forms.DataGridView
    Friend WithEvents 班种2 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents 备班名称 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 备班地点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend 开始时间2 As CS_CSMaker.frmCSMakeBasicSet.CalendarColumn
    Friend 结束时间2 As CS_CSMaker.frmCSMakeBasicSet.CalendarColumn
    Friend WithEvents 所属区域2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DGVBeiche As System.Windows.Forms.DataGridView
    Friend WithEvents 班种 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents 备车名称 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 备车地点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend 开始时间 As CS_CSMaker.frmCSMakeBasicSet.CalendarColumn
    Friend 结束时间 As CS_CSMaker.frmCSMakeBasicSet.CalendarColumn
    Friend WithEvents 所属区域 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
