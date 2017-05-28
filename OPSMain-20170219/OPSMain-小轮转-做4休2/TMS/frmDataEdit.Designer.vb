<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmDataEdit
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.dataView = New System.Windows.Forms.DataGridView
        Me.proBar = New System.Windows.Forms.ProgressBar
        Me.labInfor = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnAutoCreate = New System.Windows.Forms.Button
        Me.BtnInputExcel = New System.Windows.Forms.Button
        Me.btnExcel = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dataView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.dataView)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.proBar)
        Me.SplitContainer1.Panel2.Controls.Add(Me.labInfor)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(831, 412)
        Me.SplitContainer1.SplitterDistance = 358
        Me.SplitContainer1.TabIndex = 0
        Me.SplitContainer1.Text = "SplitContainer1"
        '
        'dataView
        '
        Me.dataView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dataView.Location = New System.Drawing.Point(0, 0)
        Me.dataView.Name = "dataView"
        Me.dataView.RowTemplate.Height = 23
        Me.dataView.Size = New System.Drawing.Size(831, 358)
        Me.dataView.TabIndex = 0
        Me.dataView.Text = "DataGridView1"
        '
        'proBar
        '
        Me.proBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.proBar.Location = New System.Drawing.Point(0, 33)
        Me.proBar.Name = "proBar"
        Me.proBar.Size = New System.Drawing.Size(288, 17)
        Me.proBar.TabIndex = 4
        Me.proBar.Visible = False
        '
        'labInfor
        '
        Me.labInfor.AutoSize = True
        Me.labInfor.Location = New System.Drawing.Point(8, 7)
        Me.labInfor.Name = "labInfor"
        Me.labInfor.Size = New System.Drawing.Size(185, 12)
        Me.labInfor.TabIndex = 3
        Me.labInfor.Text = "修改后请按保存按纽，然后退出。"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnAutoCreate)
        Me.Panel1.Controls.Add(Me.BtnInputExcel)
        Me.Panel1.Controls.Add(Me.btnExcel)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(288, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(543, 50)
        Me.Panel1.TabIndex = 2
        '
        'btnAutoCreate
        '
        Me.btnAutoCreate.Location = New System.Drawing.Point(6, 15)
        Me.btnAutoCreate.Name = "btnAutoCreate"
        Me.btnAutoCreate.Size = New System.Drawing.Size(85, 23)
        Me.btnAutoCreate.TabIndex = 4
        Me.btnAutoCreate.Text = "自动生成(&C)"
        '
        'BtnInputExcel
        '
        Me.BtnInputExcel.Location = New System.Drawing.Point(97, 15)
        Me.BtnInputExcel.Name = "BtnInputExcel"
        Me.BtnInputExcel.Size = New System.Drawing.Size(133, 23)
        Me.BtnInputExcel.TabIndex = 3
        Me.BtnInputExcel.Text = "从EXCEL文件导入(&I)"
        '
        'btnExcel
        '
        Me.btnExcel.Location = New System.Drawing.Point(236, 15)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(133, 23)
        Me.btnExcel.TabIndex = 2
        Me.btnExcel.Text = "导出到EXCEL文件(&O)"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(456, 15)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "退出(&E)"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(375, 15)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "保存(&S)"
        '
        'frmDataEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(831, 412)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmDataEdit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "数据修改"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dataView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents dataView As System.Windows.Forms.DataGridView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents proBar As System.Windows.Forms.ProgressBar
    Friend WithEvents labInfor As System.Windows.Forms.Label
    Friend WithEvents BtnInputExcel As System.Windows.Forms.Button
    Friend WithEvents btnAutoCreate As System.Windows.Forms.Button
End Class
