<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DriverInfoLoad
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
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.FilePathTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ViewButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.AddFileDataGridView = New System.Windows.Forms.DataGridView()
        Me.ImportDBButton = New System.Windows.Forms.Button()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.BtnCancle = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.AddFileDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog"
        '
        'FilePathTextBox
        '
        Me.FilePathTextBox.Location = New System.Drawing.Point(102, 29)
        Me.FilePathTextBox.Name = "FilePathTextBox"
        Me.FilePathTextBox.Size = New System.Drawing.Size(413, 21)
        Me.FilePathTextBox.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "导入文件地址"
        '
        'ViewButton
        '
        Me.ViewButton.Location = New System.Drawing.Point(547, 29)
        Me.ViewButton.Name = "ViewButton"
        Me.ViewButton.Size = New System.Drawing.Size(75, 23)
        Me.ViewButton.TabIndex = 2
        Me.ViewButton.Text = "导入并预览"
        Me.ViewButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.ViewButton)
        Me.GroupBox1.Controls.Add(Me.FilePathTextBox)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(839, 68)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "文件预览设置"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(12, 86)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(846, 356)
        Me.TabControl1.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.AddFileDataGridView)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(838, 330)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "文件数据"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'AddFileDataGridView
        '
        Me.AddFileDataGridView.AllowUserToAddRows = False
        Me.AddFileDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.AddFileDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AddFileDataGridView.Location = New System.Drawing.Point(3, 3)
        Me.AddFileDataGridView.Name = "AddFileDataGridView"
        Me.AddFileDataGridView.RowTemplate.Height = 23
        Me.AddFileDataGridView.Size = New System.Drawing.Size(832, 324)
        Me.AddFileDataGridView.TabIndex = 0
        '
        'ImportDBButton
        '
        Me.ImportDBButton.Location = New System.Drawing.Point(665, 448)
        Me.ImportDBButton.Name = "ImportDBButton"
        Me.ImportDBButton.Size = New System.Drawing.Size(89, 23)
        Me.ImportDBButton.TabIndex = 2
        Me.ImportDBButton.Text = "导入司机信息"
        Me.ImportDBButton.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(12, 452)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(622, 14)
        Me.ProgressBar1.TabIndex = 5
        Me.ProgressBar1.Visible = False
        '
        'BtnCancle
        '
        Me.BtnCancle.Location = New System.Drawing.Point(778, 448)
        Me.BtnCancle.Name = "BtnCancle"
        Me.BtnCancle.Size = New System.Drawing.Size(76, 23)
        Me.BtnCancle.TabIndex = 2
        Me.BtnCancle.Text = "取消"
        Me.BtnCancle.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(653, 29)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "导出模板"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DriverInfoLoad
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(870, 483)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.BtnCancle)
        Me.Controls.Add(Me.ImportDBButton)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DriverInfoLoad"
        Me.Text = "司机基础信息导入"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.AddFileDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents FilePathTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ViewButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents AddFileDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents ImportDBButton As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents BtnCancle As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
