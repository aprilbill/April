<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPrepareTrain
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
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.车体号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.车次 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.起始车站 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.发车时间 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.终到车站 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.终到时间 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.状态 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Button2 = New System.Windows.Forms.Button()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.车体号, Me.车次, Me.起始车站, Me.发车时间, Me.终到车站, Me.终到时间, Me.状态})
        Me.DataGridView1.Location = New System.Drawing.Point(12, 50)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(765, 399)
        Me.DataGridView1.TabIndex = 1
        '
        '车体号
        '
        Me.车体号.HeaderText = "车体号"
        Me.车体号.Name = "车体号"
        '
        '车次
        '
        Me.车次.HeaderText = "车次"
        Me.车次.Name = "车次"
        '
        '起始车站
        '
        Me.起始车站.HeaderText = "起始车站"
        Me.起始车站.Name = "起始车站"
        '
        '发车时间
        '
        Me.发车时间.HeaderText = "发车时间"
        Me.发车时间.Name = "发车时间"
        '
        '终到车站
        '
        Me.终到车站.HeaderText = "终到车站"
        Me.终到车站.Name = "终到车站"
        '
        '终到时间
        '
        Me.终到时间.HeaderText = "终到时间"
        Me.终到时间.Name = "终到时间"
        '
        '状态
        '
        Me.状态.HeaderText = "状态"
        Me.状态.Items.AddRange(New Object() {"无", "进备车线（备车）", "出备车线（回库或运行）"})
        Me.状态.Name = "状态"
        Me.状态.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.状态.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(71, 17)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(114, 21)
        Me.TextBox1.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "车次筛选"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(702, 20)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "保存"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 455)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(765, 23)
        Me.ProgressBar1.TabIndex = 6
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(603, 20)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "排序还原"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'FrmPrepareTrain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(789, 487)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.DataGridView1)
        Me.MaximizeBox = False
        Me.Name = "FrmPrepareTrain"
        Me.Text = "备车设置"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents 车体号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 车次 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 起始车站 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 发车时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 终到车站 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 终到时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 状态 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Button2 As System.Windows.Forms.Button
End Class
