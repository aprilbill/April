<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmScaleInforEdit
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
        Me.proBar = New System.Windows.Forms.ProgressBar
        Me.btnExcel = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.dataView = New System.Windows.Forms.DataGridView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.labInfor = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnDeleteScale = New System.Windows.Forms.Button
        Me.btnAddScale = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        CType(Me.dataView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'proBar
        '
        Me.proBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.proBar.Location = New System.Drawing.Point(0, 54)
        Me.proBar.Name = "proBar"
        Me.proBar.Size = New System.Drawing.Size(323, 17)
        Me.proBar.TabIndex = 4
        Me.proBar.Visible = False
        '
        'btnExcel
        '
        Me.btnExcel.Location = New System.Drawing.Point(117, 40)
        Me.btnExcel.Name = "btnExcel"
        Me.btnExcel.Size = New System.Drawing.Size(133, 23)
        Me.btnExcel.TabIndex = 2
        Me.btnExcel.Text = "导出到EXCEL文件(&O)"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(266, 40)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(75, 23)
        Me.btnExit.TabIndex = 0
        Me.btnExit.Text = "退出(&E)"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(266, 11)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "保存(&S)"
        '
        'dataView
        '
        Me.dataView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dataView.Location = New System.Drawing.Point(0, 0)
        Me.dataView.Name = "dataView"
        Me.dataView.RowTemplate.Height = 23
        Me.dataView.Size = New System.Drawing.Size(676, 381)
        Me.dataView.TabIndex = 0
        Me.dataView.Text = "DataGridView1"
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
        Me.SplitContainer1.Size = New System.Drawing.Size(676, 456)
        Me.SplitContainer1.SplitterDistance = 381
        Me.SplitContainer1.TabIndex = 1
        Me.SplitContainer1.Text = "SplitContainer1"
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
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.btnDeleteScale)
        Me.Panel1.Controls.Add(Me.btnAddScale)
        Me.Panel1.Controls.Add(Me.btnExcel)
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(323, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(353, 71)
        Me.Panel1.TabIndex = 2
        '
        'btnDeleteScale
        '
        Me.btnDeleteScale.Location = New System.Drawing.Point(13, 40)
        Me.btnDeleteScale.Name = "btnDeleteScale"
        Me.btnDeleteScale.Size = New System.Drawing.Size(87, 23)
        Me.btnDeleteScale.TabIndex = 4
        Me.btnDeleteScale.Text = "删除标尺"
        '
        'btnAddScale
        '
        Me.btnAddScale.Location = New System.Drawing.Point(13, 11)
        Me.btnAddScale.Name = "btnAddScale"
        Me.btnAddScale.Size = New System.Drawing.Size(87, 23)
        Me.btnAddScale.TabIndex = 3
        Me.btnAddScale.Text = "添加新标尺"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(117, 11)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(133, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "导入EXCEL文件(&I)"
        '
        'frmScaleInforEdit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(676, 456)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmScaleInforEdit"
        Me.Text = "frmScaleInforEdit"
        CType(Me.dataView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents proBar As System.Windows.Forms.ProgressBar
    Friend WithEvents btnExcel As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents dataView As System.Windows.Forms.DataGridView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents labInfor As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnDeleteScale As System.Windows.Forms.Button
    Friend WithEvents btnAddScale As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
