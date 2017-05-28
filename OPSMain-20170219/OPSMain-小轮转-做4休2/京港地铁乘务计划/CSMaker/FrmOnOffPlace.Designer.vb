<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOnOffPlace
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.DGVMain = New System.Windows.Forms.DataGridView()
        Me.序号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.任务号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.输出编号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.班种 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.出勤地点 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.退勤地点 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.工作时间 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.驾驶公里 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.出退勤一致 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.TXTNote = New System.Windows.Forms.TextBox()
        Me.Btn_Exit = New System.Windows.Forms.Button()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DGVMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DGVMain)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(950, 601)
        Me.SplitContainer1.SplitterDistance = 438
        Me.SplitContainer1.TabIndex = 0
        '
        'DGVMain
        '
        Me.DGVMain.AllowUserToAddRows = False
        Me.DGVMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVMain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.序号, Me.任务号, Me.输出编号, Me.班种, Me.出勤地点, Me.退勤地点, Me.工作时间, Me.驾驶公里, Me.出退勤一致})
        Me.DGVMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVMain.Location = New System.Drawing.Point(0, 0)
        Me.DGVMain.Name = "DGVMain"
        Me.DGVMain.ReadOnly = True
        Me.DGVMain.RowTemplate.Height = 23
        Me.DGVMain.Size = New System.Drawing.Size(950, 438)
        Me.DGVMain.TabIndex = 0
        '
        '序号
        '
        Me.序号.HeaderText = "序号"
        Me.序号.Name = "序号"
        Me.序号.ReadOnly = True
        '
        '任务号
        '
        Me.任务号.HeaderText = "任务号"
        Me.任务号.Name = "任务号"
        Me.任务号.ReadOnly = True
        '
        '输出编号
        '
        Me.输出编号.HeaderText = "输出编号"
        Me.输出编号.Name = "输出编号"
        Me.输出编号.ReadOnly = True
        '
        '班种
        '
        Me.班种.HeaderText = "班种"
        Me.班种.Name = "班种"
        Me.班种.ReadOnly = True
        '
        '出勤地点
        '
        Me.出勤地点.HeaderText = "出勤地点"
        Me.出勤地点.Name = "出勤地点"
        Me.出勤地点.ReadOnly = True
        '
        '退勤地点
        '
        Me.退勤地点.HeaderText = "退勤地点"
        Me.退勤地点.Name = "退勤地点"
        Me.退勤地点.ReadOnly = True
        '
        '工作时间
        '
        Me.工作时间.HeaderText = "工作时间"
        Me.工作时间.Name = "工作时间"
        Me.工作时间.ReadOnly = True
        '
        '驾驶公里
        '
        Me.驾驶公里.HeaderText = "驾驶公里"
        Me.驾驶公里.Name = "驾驶公里"
        Me.驾驶公里.ReadOnly = True
        '
        '出退勤一致
        '
        Me.出退勤一致.HeaderText = "出退勤一致"
        Me.出退勤一致.Name = "出退勤一致"
        Me.出退勤一致.ReadOnly = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.TXTNote)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Btn_Exit)
        Me.SplitContainer2.Size = New System.Drawing.Size(950, 159)
        Me.SplitContainer2.SplitterDistance = 109
        Me.SplitContainer2.TabIndex = 0
        '
        'TXTNote
        '
        Me.TXTNote.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.TXTNote.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TXTNote.Location = New System.Drawing.Point(0, 0)
        Me.TXTNote.Multiline = True
        Me.TXTNote.Name = "TXTNote"
        Me.TXTNote.ReadOnly = True
        Me.TXTNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TXTNote.Size = New System.Drawing.Size(950, 109)
        Me.TXTNote.TabIndex = 0
        '
        'Btn_Exit
        '
        Me.Btn_Exit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_Exit.Location = New System.Drawing.Point(863, 11)
        Me.Btn_Exit.Name = "Btn_Exit"
        Me.Btn_Exit.Size = New System.Drawing.Size(75, 23)
        Me.Btn_Exit.TabIndex = 0
        Me.Btn_Exit.Text = "退出(&E)"
        Me.Btn_Exit.UseVisualStyleBackColor = True
        '
        'FrmOnOffPlace
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(950, 601)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "FrmOnOffPlace"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "出退勤地点统计"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DGVMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents DGVMain As System.Windows.Forms.DataGridView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Btn_Exit As System.Windows.Forms.Button
    Friend WithEvents TXTNote As System.Windows.Forms.TextBox
    Friend WithEvents 序号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 任务号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 输出编号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 班种 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 出勤地点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 退勤地点 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 工作时间 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 驾驶公里 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 出退勤一致 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
