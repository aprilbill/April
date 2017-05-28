<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmCSTimeTableManager
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
        Me.lstCSTT = New System.Windows.Forms.ListBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnDeleteAll = New System.Windows.Forms.Button
        Me.labInfor = New System.Windows.Forms.Label
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.ProBar = New System.Windows.Forms.ToolStripProgressBar
        Me.ComlineInf = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
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
        'lstCSTT
        '
        Me.lstCSTT.FormattingEnabled = True
        Me.lstCSTT.HorizontalScrollbar = True
        Me.lstCSTT.ItemHeight = 12
        Me.lstCSTT.Location = New System.Drawing.Point(11, 20)
        Me.lstCSTT.Name = "lstCSTT"
        Me.lstCSTT.Size = New System.Drawing.Size(252, 160)
        Me.lstCSTT.TabIndex = 13
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnDeleteAll)
        Me.GroupBox1.Controls.Add(Me.labInfor)
        Me.GroupBox1.Controls.Add(Me.lstCSTT)
        Me.GroupBox1.Controls.Add(Me.btnRename)
        Me.GroupBox1.Controls.Add(Me.btnDelete)
        Me.GroupBox1.Controls.Add(Me.btnCopy)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 96)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(379, 253)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "现有乘务计划表管理"
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
        Me.labInfor.Size = New System.Drawing.Size(95, 12)
        Me.labInfor.TabIndex = 14
        Me.labInfor.Text = "乘务计划表信息:"
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
        'ComlineInf
        '
        Me.ComlineInf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComlineInf.FormattingEnabled = True
        Me.ComlineInf.Location = New System.Drawing.Point(64, 44)
        Me.ComlineInf.Name = "ComlineInf"
        Me.ComlineInf.Size = New System.Drawing.Size(224, 20)
        Me.ComlineInf.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(25, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "线路名称："
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.ComlineInf)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 14)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(379, 76)
        Me.GroupBox2.TabIndex = 20
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "线路"
        '
        'frmCSTimeTableManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(403, 404)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCSTimeTableManager"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "乘务计划管理"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnRename As System.Windows.Forms.Button
    Friend WithEvents lstCSTT As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ProBar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents labInfor As System.Windows.Forms.Label
    Friend WithEvents btnDeleteAll As System.Windows.Forms.Button
    Friend WithEvents ComlineInf As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
End Class
