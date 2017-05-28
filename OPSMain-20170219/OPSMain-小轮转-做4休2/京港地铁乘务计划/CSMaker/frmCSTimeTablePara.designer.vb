<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmCSTimeTablePara
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
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnSetDefault = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmbCheDiLineStyle = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.numCheDiLineHeight = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkShowCheci = New System.Windows.Forms.CheckBox()
        Me.chkShowDriverNo = New System.Windows.Forms.CheckBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.dataGrid = New System.Windows.Forms.DataGridView()
        Me.数值 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.参数名称 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.序号 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.序号2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.参数名称2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.值 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.numCheDiLineHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(167, 390)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "确定(&Y)"
        '
        'btnSetDefault
        '
        Me.btnSetDefault.Location = New System.Drawing.Point(12, 390)
        Me.btnSetDefault.Name = "btnSetDefault"
        Me.btnSetDefault.Size = New System.Drawing.Size(99, 23)
        Me.btnSetDefault.TabIndex = 3
        Me.btnSetDefault.Text = "设为默认(&S)"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(248, 390)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "退出(&E)"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(307, 340)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "显示参数"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Controls.Add(Me.cmbCheDiLineStyle)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.numCheDiLineHeight)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.chkShowCheci)
        Me.GroupBox1.Controls.Add(Me.chkShowDriverNo)
        Me.GroupBox1.Location = New System.Drawing.Point(26, 15)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(248, 175)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "运行线参数"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(32, 89)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(190, 1)
        Me.Panel1.TabIndex = 10
        '
        'cmbCheDiLineStyle
        '
        Me.cmbCheDiLineStyle.FormattingEnabled = True
        Me.cmbCheDiLineStyle.Items.AddRange(New Object() {"三角形", "长方形"})
        Me.cmbCheDiLineStyle.Location = New System.Drawing.Point(151, 134)
        Me.cmbCheDiLineStyle.Name = "cmbCheDiLineStyle"
        Me.cmbCheDiLineStyle.Size = New System.Drawing.Size(71, 20)
        Me.cmbCheDiLineStyle.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(30, 137)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(107, 12)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "乘务员连接线线型:"
        '
        'numCheDiLineHeight
        '
        Me.numCheDiLineHeight.Location = New System.Drawing.Point(151, 105)
        Me.numCheDiLineHeight.Name = "numCheDiLineHeight"
        Me.numCheDiLineHeight.Size = New System.Drawing.Size(71, 21)
        Me.numCheDiLineHeight.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 109)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "乘务员连接线高度:"
        '
        'chkShowCheci
        '
        Me.chkShowCheci.AutoSize = True
        Me.chkShowCheci.Location = New System.Drawing.Point(32, 51)
        Me.chkShowCheci.Name = "chkShowCheci"
        Me.chkShowCheci.Size = New System.Drawing.Size(96, 16)
        Me.chkShowCheci.TabIndex = 1
        Me.chkShowCheci.Text = "显示列车车次"
        Me.chkShowCheci.UseVisualStyleBackColor = True
        '
        'chkShowDriverNo
        '
        Me.chkShowDriverNo.AutoSize = True
        Me.chkShowDriverNo.Location = New System.Drawing.Point(32, 29)
        Me.chkShowDriverNo.Name = "chkShowDriverNo"
        Me.chkShowDriverNo.Size = New System.Drawing.Size(108, 16)
        Me.chkShowDriverNo.TabIndex = 0
        Me.chkShowDriverNo.Text = "显示乘务员编号"
        Me.chkShowDriverNo.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(315, 366)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dataGrid)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(307, 340)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "底图参数"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'dataGrid
        '
        Me.dataGrid.AllowUserToAddRows = False
        Me.dataGrid.AllowUserToDeleteRows = False
        Me.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dataGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.序号2, Me.参数名称2, Me.值})
        Me.dataGrid.Location = New System.Drawing.Point(7, 6)
        Me.dataGrid.Name = "dataGrid"
        Me.dataGrid.RowHeadersWidth = 30
        Me.dataGrid.RowTemplate.Height = 23
        Me.dataGrid.Size = New System.Drawing.Size(295, 328)
        Me.dataGrid.TabIndex = 72
        '
        '数值
        '
        Me.数值.HeaderText = "数值"
        Me.数值.Name = "数值"
        Me.数值.Width = 80
        '
        '参数名称
        '
        Me.参数名称.HeaderText = "参数名称"
        Me.参数名称.Name = "参数名称"
        Me.参数名称.Width = 130
        '
        '序号
        '
        Me.序号.HeaderText = "序号"
        Me.序号.Name = "序号"
        Me.序号.Width = 40
        '
        '序号2
        '
        Me.序号2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.序号2.HeaderText = "序号"
        Me.序号2.Name = "序号2"
        Me.序号2.Width = 54
        '
        '参数名称2
        '
        Me.参数名称2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.参数名称2.HeaderText = "参数名称"
        Me.参数名称2.Name = "参数名称2"
        Me.参数名称2.Width = 78
        '
        '值
        '
        Me.值.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.值.HeaderText = "值"
        Me.值.Name = "值"
        Me.值.Width = 42
        '
        'frmCSTimeTablePara
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(339, 425)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnSetDefault)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCSTimeTablePara"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "乘务表参数设置"
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.numCheDiLineHeight, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnSetDefault As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmbCheDiLineStyle As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents numCheDiLineHeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkShowCheci As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowDriverNo As System.Windows.Forms.CheckBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents dataGrid As System.Windows.Forms.DataGridView
    Friend WithEvents 数值 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 参数名称 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 序号 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 序号2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 参数名称2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 值 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
