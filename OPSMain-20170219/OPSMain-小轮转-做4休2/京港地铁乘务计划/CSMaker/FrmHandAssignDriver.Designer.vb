<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmHandAssignDriver
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
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmbDutySort = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DGVProperty = New System.Windows.Forms.DataGridView()
        Me.属性 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.值 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.restTimes = New System.Windows.Forms.TextBox()
        Me.CmbStyle = New System.Windows.Forms.ComboBox()
        Me.BtnBack = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.CmbDestination = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.DGVProperty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "乘务员班种:"
        '
        'CmbDutySort
        '
        Me.CmbDutySort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbDutySort.FormattingEnabled = True
        Me.CmbDutySort.Items.AddRange(New Object() {"早班", "白班", "日勤班", "夜班"})
        Me.CmbDutySort.Location = New System.Drawing.Point(109, 12)
        Me.CmbDutySort.Name = "CmbDutySort"
        Me.CmbDutySort.Size = New System.Drawing.Size(212, 20)
        Me.CmbDutySort.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(267, 305)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "完成(&F)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DGVProperty
        '
        Me.DGVProperty.AllowUserToAddRows = False
        Me.DGVProperty.BackgroundColor = System.Drawing.Color.White
        Me.DGVProperty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DGVProperty.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.属性, Me.值})
        Me.DGVProperty.Enabled = False
        Me.DGVProperty.Location = New System.Drawing.Point(12, 161)
        Me.DGVProperty.Name = "DGVProperty"
        Me.DGVProperty.ReadOnly = True
        Me.DGVProperty.RowHeadersVisible = False
        Me.DGVProperty.RowTemplate.Height = 23
        Me.DGVProperty.Size = New System.Drawing.Size(330, 138)
        Me.DGVProperty.TabIndex = 5
        '
        '属性
        '
        Me.属性.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.属性.HeaderText = "属性"
        Me.属性.Name = "属性"
        Me.属性.ReadOnly = True
        Me.属性.Width = 54
        '
        '值
        '
        Me.值.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.值.HeaderText = "值"
        Me.值.Name = "值"
        Me.值.ReadOnly = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.restTimes)
        Me.GroupBox1.Controls.Add(Me.CmbStyle)
        Me.GroupBox1.Controls.Add(Me.BtnBack)
        Me.GroupBox1.Controls.Add(Me.Button2)
        Me.GroupBox1.Controls.Add(Me.CmbDestination)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 39)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(330, 116)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "操作"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(228, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "格数"
        '
        'restTimes
        '
        Me.restTimes.Location = New System.Drawing.Point(261, 52)
        Me.restTimes.Name = "restTimes"
        Me.restTimes.Size = New System.Drawing.Size(48, 21)
        Me.restTimes.TabIndex = 6
        Me.restTimes.Text = "1"
        '
        'CmbStyle
        '
        Me.CmbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbStyle.FormattingEnabled = True
        Me.CmbStyle.Items.AddRange(New Object() {"直接接车", "休息后接车", "吃饭后接车"})
        Me.CmbStyle.Location = New System.Drawing.Point(97, 53)
        Me.CmbStyle.Name = "CmbStyle"
        Me.CmbStyle.Size = New System.Drawing.Size(127, 20)
        Me.CmbStyle.TabIndex = 3
        '
        'BtnBack
        '
        Me.BtnBack.Location = New System.Drawing.Point(142, 83)
        Me.BtnBack.Name = "BtnBack"
        Me.BtnBack.Size = New System.Drawing.Size(82, 23)
        Me.BtnBack.TabIndex = 2
        Me.BtnBack.Text = "退一步(&B)"
        Me.BtnBack.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(22, 83)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(79, 23)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "勾选任务(&G)"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'CmbDestination
        '
        Me.CmbDestination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbDestination.FormattingEnabled = True
        Me.CmbDestination.Location = New System.Drawing.Point(97, 18)
        Me.CmbDestination.Name = "CmbDestination"
        Me.CmbDestination.Size = New System.Drawing.Size(212, 20)
        Me.CmbDestination.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(32, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 12)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "接车方式:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(20, 22)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "下一目的地:"
        '
        'Timer1
        '
        '
        'FrmHandAssignDriver
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(354, 339)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.DGVProperty)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CmbDutySort)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmHandAssignDriver"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "指向性手工安排任务"
        CType(Me.DGVProperty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CmbDutySort As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DGVProperty As System.Windows.Forms.DataGridView
    Friend WithEvents 属性 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 值 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnBack As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents CmbDestination As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents CmbStyle As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents restTimes As System.Windows.Forms.TextBox
End Class
