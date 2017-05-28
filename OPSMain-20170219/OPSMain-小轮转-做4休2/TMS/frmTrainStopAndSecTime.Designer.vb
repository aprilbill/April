<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmTrainStopAndSecTime
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cmbEditTime1 = New System.Windows.Forms.ComboBox
        Me.btnSaveSecScale = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnDeleteSecScale = New System.Windows.Forms.Button
        Me.btnAddSecScale = New System.Windows.Forms.Button
        Me.cmbBiaoChi = New System.Windows.Forms.ComboBox
        Me.grdQuJian = New System.Windows.Forms.DataGridView
        Me.停站标尺 = New System.Windows.Forms.GroupBox
        Me.cmbEditTime = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbStopSta = New System.Windows.Forms.ComboBox
        Me.btnSaveStopScale = New System.Windows.Forms.Button
        Me.btnDeleteStopScale = New System.Windows.Forms.Button
        Me.btnAddStopScale = New System.Windows.Forms.Button
        Me.grdStopSta = New System.Windows.Forms.DataGridView
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnAutoCreate = New System.Windows.Forms.Button
        Me.序号2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.车站名 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.标尺名称2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.停站时间 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.序号 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.区间名 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.标尺名称 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.运行时间 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdQuJian, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.停站标尺.SuspendLayout()
        CType(Me.grdStopSta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmbEditTime1)
        Me.GroupBox2.Controls.Add(Me.btnSaveSecScale)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.btnDeleteSecScale)
        Me.GroupBox2.Controls.Add(Me.btnAddSecScale)
        Me.GroupBox2.Controls.Add(Me.cmbBiaoChi)
        Me.GroupBox2.Controls.Add(Me.grdQuJian)
        Me.GroupBox2.Location = New System.Drawing.Point(344, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(365, 447)
        Me.GroupBox2.TabIndex = 30
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "运行种类:"
        '
        'cmbEditTime1
        '
        Me.cmbEditTime1.FormattingEnabled = True
        Me.cmbEditTime1.Location = New System.Drawing.Point(253, 375)
        Me.cmbEditTime1.Name = "cmbEditTime1"
        Me.cmbEditTime1.Size = New System.Drawing.Size(88, 20)
        Me.cmbEditTime1.TabIndex = 80
        Me.cmbEditTime1.Visible = False
        '
        'btnSaveSecScale
        '
        Me.btnSaveSecScale.Location = New System.Drawing.Point(270, 415)
        Me.btnSaveSecScale.Name = "btnSaveSecScale"
        Me.btnSaveSecScale.Size = New System.Drawing.Size(83, 25)
        Me.btnSaveSecScale.TabIndex = 79
        Me.btnSaveSecScale.Text = "保存"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 12)
        Me.Label2.TabIndex = 78
        Me.Label2.Text = "种类名称:"
        '
        'btnDeleteSecScale
        '
        Me.btnDeleteSecScale.Location = New System.Drawing.Point(146, 415)
        Me.btnDeleteSecScale.Name = "btnDeleteSecScale"
        Me.btnDeleteSecScale.Size = New System.Drawing.Size(83, 25)
        Me.btnDeleteSecScale.TabIndex = 76
        Me.btnDeleteSecScale.Text = "删除种类"
        '
        'btnAddSecScale
        '
        Me.btnAddSecScale.Location = New System.Drawing.Point(11, 415)
        Me.btnAddSecScale.Name = "btnAddSecScale"
        Me.btnAddSecScale.Size = New System.Drawing.Size(83, 25)
        Me.btnAddSecScale.TabIndex = 75
        Me.btnAddSecScale.Text = "添加种类"
        '
        'cmbBiaoChi
        '
        Me.cmbBiaoChi.FormattingEnabled = True
        Me.cmbBiaoChi.Location = New System.Drawing.Point(91, 15)
        Me.cmbBiaoChi.Name = "cmbBiaoChi"
        Me.cmbBiaoChi.Size = New System.Drawing.Size(262, 20)
        Me.cmbBiaoChi.TabIndex = 74
        '
        'grdQuJian
        '
        Me.grdQuJian.AllowUserToAddRows = False
        Me.grdQuJian.AllowUserToDeleteRows = False
        Me.grdQuJian.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdQuJian.ColumnHeadersHeight = 20
        Me.grdQuJian.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.序号, Me.区间名, Me.标尺名称, Me.运行时间})
        Me.grdQuJian.Location = New System.Drawing.Point(11, 46)
        Me.grdQuJian.Name = "grdQuJian"
        Me.grdQuJian.RowHeadersWidth = 20
        Me.grdQuJian.RowTemplate.Height = 23
        Me.grdQuJian.Size = New System.Drawing.Size(342, 363)
        Me.grdQuJian.TabIndex = 70
        '
        '停站标尺
        '
        Me.停站标尺.Controls.Add(Me.cmbEditTime)
        Me.停站标尺.Controls.Add(Me.Label1)
        Me.停站标尺.Controls.Add(Me.cmbStopSta)
        Me.停站标尺.Controls.Add(Me.btnSaveStopScale)
        Me.停站标尺.Controls.Add(Me.btnDeleteStopScale)
        Me.停站标尺.Controls.Add(Me.btnAddStopScale)
        Me.停站标尺.Controls.Add(Me.grdStopSta)
        Me.停站标尺.Location = New System.Drawing.Point(12, 12)
        Me.停站标尺.Name = "停站标尺"
        Me.停站标尺.Size = New System.Drawing.Size(326, 447)
        Me.停站标尺.TabIndex = 29
        Me.停站标尺.TabStop = False
        Me.停站标尺.Text = "停站类型:"
        '
        'cmbEditTime
        '
        Me.cmbEditTime.FormattingEnabled = True
        Me.cmbEditTime.Location = New System.Drawing.Point(206, 366)
        Me.cmbEditTime.Name = "cmbEditTime"
        Me.cmbEditTime.Size = New System.Drawing.Size(88, 20)
        Me.cmbEditTime.TabIndex = 76
        Me.cmbEditTime.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 12)
        Me.Label1.TabIndex = 75
        Me.Label1.Text = "种类名称:"
        '
        'cmbStopSta
        '
        Me.cmbStopSta.FormattingEnabled = True
        Me.cmbStopSta.Location = New System.Drawing.Point(91, 20)
        Me.cmbStopSta.Name = "cmbStopSta"
        Me.cmbStopSta.Size = New System.Drawing.Size(222, 20)
        Me.cmbStopSta.TabIndex = 74
        '
        'btnSaveStopScale
        '
        Me.btnSaveStopScale.Location = New System.Drawing.Point(231, 417)
        Me.btnSaveStopScale.Name = "btnSaveStopScale"
        Me.btnSaveStopScale.Size = New System.Drawing.Size(84, 24)
        Me.btnSaveStopScale.TabIndex = 73
        Me.btnSaveStopScale.Text = "保存"
        '
        'btnDeleteStopScale
        '
        Me.btnDeleteStopScale.Location = New System.Drawing.Point(123, 417)
        Me.btnDeleteStopScale.Name = "btnDeleteStopScale"
        Me.btnDeleteStopScale.Size = New System.Drawing.Size(84, 24)
        Me.btnDeleteStopScale.TabIndex = 72
        Me.btnDeleteStopScale.Text = "删除种类"
        '
        'btnAddStopScale
        '
        Me.btnAddStopScale.Location = New System.Drawing.Point(17, 417)
        Me.btnAddStopScale.Name = "btnAddStopScale"
        Me.btnAddStopScale.Size = New System.Drawing.Size(84, 24)
        Me.btnAddStopScale.TabIndex = 71
        Me.btnAddStopScale.Text = "添加种类"
        '
        'grdStopSta
        '
        Me.grdStopSta.AllowUserToAddRows = False
        Me.grdStopSta.AllowUserToDeleteRows = False
        Me.grdStopSta.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grdStopSta.ColumnHeadersHeight = 20
        Me.grdStopSta.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.序号2, Me.车站名, Me.标尺名称2, Me.停站时间})
        Me.grdStopSta.Location = New System.Drawing.Point(16, 48)
        Me.grdStopSta.Name = "grdStopSta"
        Me.grdStopSta.RowHeadersWidth = 20
        Me.grdStopSta.RowTemplate.Height = 23
        Me.grdStopSta.Size = New System.Drawing.Size(298, 362)
        Me.grdStopSta.TabIndex = 70
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(614, 465)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(83, 23)
        Me.btnExit.TabIndex = 32
        Me.btnExit.Text = "退出(&E)"
        '
        'btnAutoCreate
        '
        Me.btnAutoCreate.Location = New System.Drawing.Point(12, 465)
        Me.btnAutoCreate.Name = "btnAutoCreate"
        Me.btnAutoCreate.Size = New System.Drawing.Size(117, 23)
        Me.btnAutoCreate.TabIndex = 33
        Me.btnAutoCreate.Text = "自动生成(&C)"
        '
        '序号2
        '
        Me.序号2.HeaderText = "序号"
        Me.序号2.Name = "序号2"
        Me.序号2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.序号2.Width = 35
        '
        '车站名
        '
        Me.车站名.HeaderText = "车站名"
        Me.车站名.Name = "车站名"
        Me.车站名.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.车站名.Width = 80
        '
        '标尺名称2
        '
        Me.标尺名称2.HeaderText = "标尺名称"
        Me.标尺名称2.Name = "标尺名称2"
        Me.标尺名称2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        '停站时间
        '
        Me.停站时间.HeaderText = "停站时间"
        Me.停站时间.Name = "停站时间"
        Me.停站时间.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.停站时间.Width = 60
        '
        '序号
        '
        Me.序号.HeaderText = "序号"
        Me.序号.Name = "序号"
        Me.序号.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.序号.Width = 35
        '
        '区间名
        '
        Me.区间名.HeaderText = "区间名"
        Me.区间名.Name = "区间名"
        Me.区间名.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.区间名.Width = 130
        '
        '标尺名称
        '
        Me.标尺名称.HeaderText = "标尺名称"
        Me.标尺名称.Name = "标尺名称"
        Me.标尺名称.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        '运行时间
        '
        Me.运行时间.HeaderText = "运行时间"
        Me.运行时间.Name = "运行时间"
        Me.运行时间.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.运行时间.Width = 60
        '
        'frmTrainStopAndSecTime
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(719, 499)
        Me.Controls.Add(Me.btnAutoCreate)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.停站标尺)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTrainStopAndSecTime"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "停站和运行标尺"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grdQuJian, System.ComponentModel.ISupportInitialize).EndInit()
        Me.停站标尺.ResumeLayout(False)
        Me.停站标尺.PerformLayout()
        CType(Me.grdStopSta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbBiaoChi As System.Windows.Forms.ComboBox
    Friend WithEvents grdQuJian As System.Windows.Forms.DataGridView
    Friend WithEvents 停站标尺 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbStopSta As System.Windows.Forms.ComboBox
    Friend WithEvents btnSaveStopScale As System.Windows.Forms.Button
    Friend WithEvents btnDeleteStopScale As System.Windows.Forms.Button
    Friend WithEvents btnAddStopScale As System.Windows.Forms.Button
    Friend WithEvents grdStopSta As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnDeleteSecScale As System.Windows.Forms.Button
    Friend WithEvents btnAddSecScale As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSaveSecScale As System.Windows.Forms.Button
    Friend WithEvents btnAutoCreate As System.Windows.Forms.Button
    Friend WithEvents cmbEditTime As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cmbEditTime1 As System.Windows.Forms.ComboBox
    Friend WithEvents 序号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 区间名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 标尺名称 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 运行时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 序号2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 车站名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 标尺名称2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 停站时间 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
