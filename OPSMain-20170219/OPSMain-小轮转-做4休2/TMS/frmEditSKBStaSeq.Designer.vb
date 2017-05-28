<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmEditSKBStaSeq
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
        Me.CmdDown = New System.Windows.Forms.Button
        Me.cmdUp = New System.Windows.Forms.Button
        Me.cmdDeleAll = New System.Windows.Forms.Button
        Me.cmdDeleOne = New System.Windows.Forms.Button
        Me.cmdAddAll = New System.Windows.Forms.Button
        Me.cmdAddOne = New System.Windows.Forms.Button
        Me.lstBei = New System.Windows.Forms.ListBox
        Me.lstSta = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cmbSecName = New System.Windows.Forms.ComboBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnDeleteQuDuan = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'CmdDown
        '
        Me.CmdDown.Location = New System.Drawing.Point(149, 261)
        Me.CmdDown.Name = "CmdDown"
        Me.CmdDown.Size = New System.Drawing.Size(67, 23)
        Me.CmdDown.TabIndex = 18
        Me.CmdDown.Text = "向下移动"
        '
        'cmdUp
        '
        Me.cmdUp.Location = New System.Drawing.Point(149, 223)
        Me.cmdUp.Name = "cmdUp"
        Me.cmdUp.Size = New System.Drawing.Size(67, 23)
        Me.cmdUp.TabIndex = 17
        Me.cmdUp.Text = "向上移动"
        '
        'cmdDeleAll
        '
        Me.cmdDeleAll.Location = New System.Drawing.Point(149, 183)
        Me.cmdDeleAll.Name = "cmdDeleAll"
        Me.cmdDeleAll.Size = New System.Drawing.Size(67, 23)
        Me.cmdDeleAll.TabIndex = 16
        Me.cmdDeleAll.Text = "<<<"
        '
        'cmdDeleOne
        '
        Me.cmdDeleOne.Location = New System.Drawing.Point(149, 144)
        Me.cmdDeleOne.Name = "cmdDeleOne"
        Me.cmdDeleOne.Size = New System.Drawing.Size(67, 23)
        Me.cmdDeleOne.TabIndex = 15
        Me.cmdDeleOne.Text = "<-"
        '
        'cmdAddAll
        '
        Me.cmdAddAll.Location = New System.Drawing.Point(149, 105)
        Me.cmdAddAll.Name = "cmdAddAll"
        Me.cmdAddAll.Size = New System.Drawing.Size(67, 23)
        Me.cmdAddAll.TabIndex = 14
        Me.cmdAddAll.Text = ">>>"
        '
        'cmdAddOne
        '
        Me.cmdAddOne.Location = New System.Drawing.Point(149, 67)
        Me.cmdAddOne.Name = "cmdAddOne"
        Me.cmdAddOne.Size = New System.Drawing.Size(67, 23)
        Me.cmdAddOne.TabIndex = 13
        Me.cmdAddOne.Text = "->"
        '
        'lstBei
        '
        Me.lstBei.FormattingEnabled = True
        Me.lstBei.HorizontalScrollbar = True
        Me.lstBei.ItemHeight = 12
        Me.lstBei.Location = New System.Drawing.Point(13, 58)
        Me.lstBei.Name = "lstBei"
        Me.lstBei.Size = New System.Drawing.Size(128, 244)
        Me.lstBei.TabIndex = 12
        '
        'lstSta
        '
        Me.lstSta.FormattingEnabled = True
        Me.lstSta.HorizontalScrollbar = True
        Me.lstSta.ItemHeight = 12
        Me.lstSta.Location = New System.Drawing.Point(230, 58)
        Me.lstSta.Name = "lstSta"
        Me.lstSta.Size = New System.Drawing.Size(130, 244)
        Me.lstSta.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 12)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "区段名称:"
        '
        'cmbSecName
        '
        Me.cmbSecName.FormattingEnabled = True
        Me.cmbSecName.Location = New System.Drawing.Point(76, 18)
        Me.cmbSecName.Name = "cmbSecName"
        Me.cmbSecName.Size = New System.Drawing.Size(284, 20)
        Me.cmbSecName.TabIndex = 22
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(261, 319)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(99, 23)
        Me.btnSave.TabIndex = 23
        Me.btnSave.Text = "保存修改(&S)"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(261, 349)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(99, 23)
        Me.btnExit.TabIndex = 24
        Me.btnExit.Text = "退出(&E)"
        '
        'btnDeleteQuDuan
        '
        Me.btnDeleteQuDuan.Location = New System.Drawing.Point(156, 319)
        Me.btnDeleteQuDuan.Name = "btnDeleteQuDuan"
        Me.btnDeleteQuDuan.Size = New System.Drawing.Size(99, 23)
        Me.btnDeleteQuDuan.TabIndex = 25
        Me.btnDeleteQuDuan.Text = "删除区段(&D)"
        '
        'frmEditSKBStaSeq
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(372, 381)
        Me.Controls.Add(Me.btnDeleteQuDuan)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.cmbSecName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstSta)
        Me.Controls.Add(Me.CmdDown)
        Me.Controls.Add(Me.cmdUp)
        Me.Controls.Add(Me.cmdDeleAll)
        Me.Controls.Add(Me.cmdDeleOne)
        Me.Controls.Add(Me.cmdAddAll)
        Me.Controls.Add(Me.cmdAddOne)
        Me.Controls.Add(Me.lstBei)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEditSKBStaSeq"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "编辑时刻表车站顺序"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CmdDown As System.Windows.Forms.Button
    Friend WithEvents cmdUp As System.Windows.Forms.Button
    Friend WithEvents cmdDeleAll As System.Windows.Forms.Button
    Friend WithEvents cmdDeleOne As System.Windows.Forms.Button
    Friend WithEvents cmdAddAll As System.Windows.Forms.Button
    Friend WithEvents cmdAddOne As System.Windows.Forms.Button
    Friend WithEvents lstBei As System.Windows.Forms.ListBox
    Friend WithEvents lstSta As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbSecName As System.Windows.Forms.ComboBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnDeleteQuDuan As System.Windows.Forms.Button
End Class
