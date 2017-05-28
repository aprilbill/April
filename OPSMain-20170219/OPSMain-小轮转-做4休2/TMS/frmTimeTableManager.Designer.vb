<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmTimeTableManager
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
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnCopy = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnRename = New System.Windows.Forms.Button
        Me.lstSKB = New System.Windows.Forms.ListBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnDeleteAll = New System.Windows.Forms.Button
        Me.labInfor = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.txtCurSKB = New System.Windows.Forms.TextBox
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ProBar = New System.Windows.Forms.ToolStripProgressBar
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(293, 355)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(87, 23)
        Me.btnExit.TabIndex = 12
        Me.btnExit.Text = "退出(&E)"
        '
        'btnCopy
        '
        Me.btnCopy.Location = New System.Drawing.Point(282, 60)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(86, 23)
        Me.btnCopy.TabIndex = 11
        Me.btnCopy.Text = "复制(&C)"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(283, 98)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(86, 23)
        Me.btnDelete.TabIndex = 9
        Me.btnDelete.Text = "删除(&D)"
        '
        'btnRename
        '
        Me.btnRename.Location = New System.Drawing.Point(282, 20)
        Me.btnRename.Name = "btnRename"
        Me.btnRename.Size = New System.Drawing.Size(86, 23)
        Me.btnRename.TabIndex = 8
        Me.btnRename.Text = "修改名称(&R)"
        '
        'lstSKB
        '
        Me.lstSKB.FormattingEnabled = True
        Me.lstSKB.HorizontalScrollbar = True
        Me.lstSKB.ItemHeight = 12
        Me.lstSKB.Location = New System.Drawing.Point(11, 20)
        Me.lstSKB.Name = "lstSKB"
        Me.lstSKB.Size = New System.Drawing.Size(252, 160)
        Me.lstSKB.TabIndex = 13
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnDeleteAll)
        Me.GroupBox1.Controls.Add(Me.labInfor)
        Me.GroupBox1.Controls.Add(Me.lstSKB)
        Me.GroupBox1.Controls.Add(Me.btnRename)
        Me.GroupBox1.Controls.Add(Me.btnDelete)
        Me.GroupBox1.Controls.Add(Me.btnCopy)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 96)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(379, 253)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "现有时刻表管理"
        '
        'btnDeleteAll
        '
        Me.btnDeleteAll.Location = New System.Drawing.Point(282, 137)
        Me.btnDeleteAll.Name = "btnDeleteAll"
        Me.btnDeleteAll.Size = New System.Drawing.Size(86, 23)
        Me.btnDeleteAll.TabIndex = 15
        Me.btnDeleteAll.Text = "删除所有"
        '
        'labInfor
        '
        Me.labInfor.AutoSize = True
        Me.labInfor.Location = New System.Drawing.Point(13, 191)
        Me.labInfor.Name = "labInfor"
        Me.labInfor.Size = New System.Drawing.Size(71, 12)
        Me.labInfor.TabIndex = 14
        Me.labInfor.Text = "时刻表信息:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Controls.Add(Me.btnSave)
        Me.GroupBox2.Controls.Add(Me.txtCurSKB)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(379, 75)
        Me.GroupBox2.TabIndex = 15
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "当前时刻表管理"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(282, 45)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(86, 23)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "另存为(&A)"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(283, 16)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(86, 23)
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "保存(&S)"
        '
        'txtCurSKB
        '
        Me.txtCurSKB.Location = New System.Drawing.Point(11, 20)
        Me.txtCurSKB.Name = "txtCurSKB"
        Me.txtCurSKB.Size = New System.Drawing.Size(252, 21)
        Me.txtCurSKB.TabIndex = 0
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProBar})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 382)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(403, 22)
        Me.StatusStrip1.TabIndex = 17
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ProBar
        '
        Me.ProBar.Name = "ProBar"
        Me.ProBar.Size = New System.Drawing.Size(350, 16)
        Me.ProBar.Visible = False
        '
        'frmTimeTableManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(403, 404)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTimeTableManager"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "运行图管理"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnRename As System.Windows.Forms.Button
    Friend WithEvents lstSKB As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtCurSKB As System.Windows.Forms.TextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ProBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents labInfor As System.Windows.Forms.Label
    Friend WithEvents btnDeleteAll As System.Windows.Forms.Button
End Class
