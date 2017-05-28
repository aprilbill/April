<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmStateDetail
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TaskDataGridView = New System.Windows.Forms.DataGridView()
        Me.任务号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.车次 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.起站 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.发车时间 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.到站 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.到达时间 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.TaskDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(3, 2)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(551, 395)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TaskDataGridView)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(543, 369)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "任务列表"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TaskDataGridView
        '
        Me.TaskDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TaskDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.任务号, Me.车次, Me.起站, Me.发车时间, Me.到站, Me.到达时间})
        Me.TaskDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TaskDataGridView.Location = New System.Drawing.Point(3, 3)
        Me.TaskDataGridView.Name = "TaskDataGridView"
        Me.TaskDataGridView.RowTemplate.Height = 23
        Me.TaskDataGridView.Size = New System.Drawing.Size(537, 363)
        Me.TaskDataGridView.TabIndex = 0
        '
        '任务号
        '
        Me.任务号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.任务号.HeaderText = "任务号"
        Me.任务号.Name = "任务号"
        '
        '车次
        '
        Me.车次.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.车次.HeaderText = "车次"
        Me.车次.Name = "车次"
        '
        '起站
        '
        Me.起站.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.起站.HeaderText = "起站"
        Me.起站.Name = "起站"
        '
        '发车时间
        '
        Me.发车时间.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.发车时间.HeaderText = "发车时间"
        Me.发车时间.Name = "发车时间"
        '
        '到站
        '
        Me.到站.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.到站.HeaderText = "到站"
        Me.到站.Name = "到站"
        '
        '到达时间
        '
        Me.到达时间.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.到达时间.HeaderText = "到达时间"
        Me.到达时间.Name = "到达时间"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(476, 403)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "退出"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'FrmStateDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(563, 438)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "FrmStateDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "状态信息"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.TaskDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TaskDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents 任务号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 车次 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 起站 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 发车时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 到站 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 到达时间 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
