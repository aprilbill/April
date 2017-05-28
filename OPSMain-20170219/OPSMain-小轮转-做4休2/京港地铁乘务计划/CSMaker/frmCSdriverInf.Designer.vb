<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCSdriverInf
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
        Me.dataGrid = New System.Windows.Forms.DataGridView()
        Me.序号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.乘务员信息名称 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.信息值 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Cmb = New System.Windows.Forms.ComboBox()
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dataGrid
        '
        Me.dataGrid.AllowUserToAddRows = False
        Me.dataGrid.AllowUserToDeleteRows = False
        Me.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dataGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.序号, Me.乘务员信息名称, Me.信息值})
        Me.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dataGrid.Location = New System.Drawing.Point(0, 0)
        Me.dataGrid.Name = "dataGrid"
        Me.dataGrid.RowHeadersWidth = 30
        Me.dataGrid.RowTemplate.Height = 23
        Me.dataGrid.Size = New System.Drawing.Size(313, 349)
        Me.dataGrid.TabIndex = 73
        '
        '序号
        '
        Me.序号.HeaderText = "序号"
        Me.序号.Name = "序号"
        Me.序号.Width = 40
        '
        '乘务员信息名称
        '
        Me.乘务员信息名称.HeaderText = "乘务员信息名称"
        Me.乘务员信息名称.Name = "乘务员信息名称"
        Me.乘务员信息名称.Width = 130
        '
        '信息值
        '
        Me.信息值.HeaderText = "信息值"
        Me.信息值.Name = "信息值"
        Me.信息值.Width = 95
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(250, 367)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 75
        Me.Button1.Text = "退出(&E)"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(169, 367)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 74
        Me.btnOK.Text = "确定(&Y)"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Cmb)
        Me.Panel1.Controls.Add(Me.dataGrid)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(313, 349)
        Me.Panel1.TabIndex = 76
        '
        'Cmb
        '
        Me.Cmb.FormattingEnabled = True
        Me.Cmb.Location = New System.Drawing.Point(64, 118)
        Me.Cmb.Name = "Cmb"
        Me.Cmb.Size = New System.Drawing.Size(121, 20)
        Me.Cmb.TabIndex = 74
        Me.Cmb.Visible = False
        '
        'frmCSdriverInf
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(337, 402)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmCSdriverInf"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "修改乘务员信息"
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents dataGrid As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents 序号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 乘务员信息名称 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 信息值 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Cmb As System.Windows.Forms.ComboBox
End Class
