<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmVocSeqSetting
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

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DGV_DutySeq = New System.Windows.Forms.DataGridView()
        Me.Btn_OK = New System.Windows.Forms.Button()
        Me.Btn_Cancle = New System.Windows.Forms.Button()
        Me.Btn_AddOne = New System.Windows.Forms.Button()
        Me.Btn_AddAll = New System.Windows.Forms.Button()
        Me.Btn_DelOne = New System.Windows.Forms.Button()
        Me.Btn_DelAll = New System.Windows.Forms.Button()
        Me.DGV_Drivers = New System.Windows.Forms.DataGridView()
        Me.组号2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.司机姓名2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.组号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.司机姓名 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.天数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.休假顺序 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.类型 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        CType(Me.DGV_DutySeq, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DGV_Drivers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DGV_DutySeq
        '
        Me.DGV_DutySeq.AllowUserToAddRows = False
        Me.DGV_DutySeq.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DGV_DutySeq.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_DutySeq.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.组号, Me.司机姓名, Me.天数, Me.休假顺序, Me.类型})
        Me.DGV_DutySeq.Location = New System.Drawing.Point(247, 12)
        Me.DGV_DutySeq.Name = "DGV_DutySeq"
        Me.DGV_DutySeq.RowHeadersVisible = False
        Me.DGV_DutySeq.RowTemplate.Height = 23
        Me.DGV_DutySeq.Size = New System.Drawing.Size(408, 572)
        Me.DGV_DutySeq.TabIndex = 7
        '
        'Btn_OK
        '
        Me.Btn_OK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_OK.Location = New System.Drawing.Point(452, 594)
        Me.Btn_OK.Name = "Btn_OK"
        Me.Btn_OK.Size = New System.Drawing.Size(75, 23)
        Me.Btn_OK.TabIndex = 8
        Me.Btn_OK.Text = "确定(&Q)"
        Me.Btn_OK.UseVisualStyleBackColor = True
        '
        'Btn_Cancle
        '
        Me.Btn_Cancle.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_Cancle.Location = New System.Drawing.Point(580, 594)
        Me.Btn_Cancle.Name = "Btn_Cancle"
        Me.Btn_Cancle.Size = New System.Drawing.Size(75, 23)
        Me.Btn_Cancle.TabIndex = 8
        Me.Btn_Cancle.Text = "取消(&C)"
        Me.Btn_Cancle.UseVisualStyleBackColor = True
        '
        'Btn_AddOne
        '
        Me.Btn_AddOne.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Btn_AddOne.Location = New System.Drawing.Point(199, 203)
        Me.Btn_AddOne.Name = "Btn_AddOne"
        Me.Btn_AddOne.Size = New System.Drawing.Size(42, 27)
        Me.Btn_AddOne.TabIndex = 10
        Me.Btn_AddOne.Text = ">"
        Me.Btn_AddOne.UseVisualStyleBackColor = True
        '
        'Btn_AddAll
        '
        Me.Btn_AddAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Btn_AddAll.Location = New System.Drawing.Point(199, 236)
        Me.Btn_AddAll.Name = "Btn_AddAll"
        Me.Btn_AddAll.Size = New System.Drawing.Size(42, 27)
        Me.Btn_AddAll.TabIndex = 10
        Me.Btn_AddAll.Text = ">>"
        Me.Btn_AddAll.UseVisualStyleBackColor = True
        '
        'Btn_DelOne
        '
        Me.Btn_DelOne.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Btn_DelOne.Location = New System.Drawing.Point(199, 305)
        Me.Btn_DelOne.Name = "Btn_DelOne"
        Me.Btn_DelOne.Size = New System.Drawing.Size(42, 27)
        Me.Btn_DelOne.TabIndex = 10
        Me.Btn_DelOne.Text = "<"
        Me.Btn_DelOne.UseVisualStyleBackColor = True
        '
        'Btn_DelAll
        '
        Me.Btn_DelAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Btn_DelAll.Location = New System.Drawing.Point(199, 272)
        Me.Btn_DelAll.Name = "Btn_DelAll"
        Me.Btn_DelAll.Size = New System.Drawing.Size(42, 27)
        Me.Btn_DelAll.TabIndex = 10
        Me.Btn_DelAll.Text = "<<"
        Me.Btn_DelAll.UseVisualStyleBackColor = True
        '
        'DGV_Drivers
        '
        Me.DGV_Drivers.AllowUserToAddRows = False
        Me.DGV_Drivers.AllowUserToDeleteRows = False
        Me.DGV_Drivers.AllowUserToResizeColumns = False
        Me.DGV_Drivers.AllowUserToResizeRows = False
        Me.DGV_Drivers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGV_Drivers.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.组号2, Me.司机姓名2})
        Me.DGV_Drivers.Location = New System.Drawing.Point(12, 12)
        Me.DGV_Drivers.MultiSelect = False
        Me.DGV_Drivers.Name = "DGV_Drivers"
        Me.DGV_Drivers.ReadOnly = True
        Me.DGV_Drivers.RowHeadersVisible = False
        Me.DGV_Drivers.RowTemplate.Height = 23
        Me.DGV_Drivers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DGV_Drivers.Size = New System.Drawing.Size(181, 572)
        Me.DGV_Drivers.TabIndex = 11
        '
        '组号2
        '
        Me.组号2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.组号2.HeaderText = "组号"
        Me.组号2.Name = "组号2"
        Me.组号2.ReadOnly = True
        Me.组号2.Width = 54
        '
        '司机姓名2
        '
        Me.司机姓名2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.司机姓名2.HeaderText = "司机姓名"
        Me.司机姓名2.Name = "司机姓名2"
        Me.司机姓名2.ReadOnly = True
        '
        '组号
        '
        Me.组号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.组号.HeaderText = "组号"
        Me.组号.Name = "组号"
        Me.组号.Width = 54
        '
        '司机姓名
        '
        Me.司机姓名.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.司机姓名.HeaderText = "司机姓名"
        Me.司机姓名.Name = "司机姓名"
        Me.司机姓名.Width = 78
        '
        '天数
        '
        Me.天数.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.天数.HeaderText = "天数"
        Me.天数.Name = "天数"
        Me.天数.Width = 54
        '
        '休假顺序
        '
        Me.休假顺序.HeaderText = "休假顺序"
        Me.休假顺序.Name = "休假顺序"
        '
        '类型
        '
        Me.类型.HeaderText = "类型"
        Me.类型.Items.AddRange(New Object() {"年假", "培训"})
        Me.类型.Name = "类型"
        Me.类型.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.类型.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'FrmVocSeqSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(667, 629)
        Me.Controls.Add(Me.DGV_Drivers)
        Me.Controls.Add(Me.Btn_DelAll)
        Me.Controls.Add(Me.Btn_DelOne)
        Me.Controls.Add(Me.Btn_AddAll)
        Me.Controls.Add(Me.Btn_AddOne)
        Me.Controls.Add(Me.Btn_Cancle)
        Me.Controls.Add(Me.Btn_OK)
        Me.Controls.Add(Me.DGV_DutySeq)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmVocSeqSetting"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "设置年假休假顺序"
        CType(Me.DGV_DutySeq, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DGV_Drivers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DGV_DutySeq As System.Windows.Forms.DataGridView
    Friend WithEvents Btn_OK As System.Windows.Forms.Button
    Friend WithEvents Btn_Cancle As System.Windows.Forms.Button
    Friend WithEvents Btn_AddOne As System.Windows.Forms.Button
    Friend WithEvents Btn_AddAll As System.Windows.Forms.Button
    Friend WithEvents Btn_DelOne As System.Windows.Forms.Button
    Friend WithEvents Btn_DelAll As System.Windows.Forms.Button
    Friend WithEvents DGV_Drivers As System.Windows.Forms.DataGridView
    Friend WithEvents 组号2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 司机姓名2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 组号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 司机姓名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 天数 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 休假顺序 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 类型 As System.Windows.Forms.DataGridViewComboBoxColumn
End Class
