<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmDiTuStructure
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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkAllMove = New System.Windows.Forms.CheckBox
        Me.btnRefreshDrawLine = New System.Windows.Forms.Button
        Me.chkDuiQi = New System.Windows.Forms.CheckBox
        Me.cmbFenGeTime = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkSetecAll = New System.Windows.Forms.CheckBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.cmdExit = New System.Windows.Forms.Button
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.lstSta = New System.Windows.Forms.CheckedListBox
        Me.cmdRefresh = New System.Windows.Forms.Button
        Me.CmdDown = New System.Windows.Forms.Button
        Me.cmdUp = New System.Windows.Forms.Button
        Me.cmdDeleAll = New System.Windows.Forms.Button
        Me.cmdDeleOne = New System.Windows.Forms.Button
        Me.cmdAddAll = New System.Windows.Forms.Button
        Me.cmdAddOne = New System.Windows.Forms.Button
        Me.lstBei = New System.Windows.Forms.ListBox
        Me.picSta = New System.Windows.Forms.PictureBox
        Me.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.picSta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkAllMove)
        Me.Panel1.Controls.Add(Me.btnRefreshDrawLine)
        Me.Panel1.Controls.Add(Me.chkDuiQi)
        Me.Panel1.Controls.Add(Me.cmbFenGeTime)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.chkSetecAll)
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.cmdExit)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 464)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(778, 66)
        Me.Panel1.TabIndex = 0
        '
        'chkAllMove
        '
        Me.chkAllMove.AutoSize = True
        Me.chkAllMove.Location = New System.Drawing.Point(514, 7)
        Me.chkAllMove.Name = "chkAllMove"
        Me.chkAllMove.Size = New System.Drawing.Size(108, 16)
        Me.chkAllMove.TabIndex = 17
        Me.chkAllMove.Text = "车站后全部移动"
        '
        'btnRefreshDrawLine
        '
        Me.btnRefreshDrawLine.Location = New System.Drawing.Point(679, 7)
        Me.btnRefreshDrawLine.Name = "btnRefreshDrawLine"
        Me.btnRefreshDrawLine.Size = New System.Drawing.Size(87, 23)
        Me.btnRefreshDrawLine.TabIndex = 16
        Me.btnRefreshDrawLine.Text = "刷新底图(&R)"
        '
        'chkDuiQi
        '
        Me.chkDuiQi.AutoSize = True
        Me.chkDuiQi.Location = New System.Drawing.Point(400, 6)
        Me.chkDuiQi.Name = "chkDuiQi"
        Me.chkDuiQi.Size = New System.Drawing.Size(108, 16)
        Me.chkDuiQi.TabIndex = 15
        Me.chkDuiQi.Text = "自动与车站对齐"
        '
        'cmbFenGeTime
        '
        Me.cmbFenGeTime.FormattingEnabled = True
        Me.cmbFenGeTime.Location = New System.Drawing.Point(94, 18)
        Me.cmbFenGeTime.Name = "cmbFenGeTime"
        Me.cmbFenGeTime.Size = New System.Drawing.Size(79, 20)
        Me.cmbFenGeTime.TabIndex = 14
        Me.cmbFenGeTime.Text = "100"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "分隔时间(秒)"
        '
        'chkSetecAll
        '
        Me.chkSetecAll.AutoSize = True
        Me.chkSetecAll.Location = New System.Drawing.Point(264, 6)
        Me.chkSetecAll.Name = "chkSetecAll"
        Me.chkSetecAll.Size = New System.Drawing.Size(48, 16)
        Me.chkSetecAll.TabIndex = 12
        Me.chkSetecAll.Text = "全选"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(546, 36)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(117, 23)
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "保存底图结构(&S)"
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(679, 36)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(87, 23)
        Me.cmdExit.TabIndex = 11
        Me.cmdExit.Text = "退出(&E)"
        '
        'Panel2
        '
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(778, 16)
        Me.Panel2.TabIndex = 1
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 16)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lstSta)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdRefresh)
        Me.SplitContainer1.Panel1.Controls.Add(Me.CmdDown)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdUp)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdDeleAll)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdDeleOne)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdAddAll)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmdAddOne)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lstBei)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.picSta)
        Me.SplitContainer1.Size = New System.Drawing.Size(778, 448)
        Me.SplitContainer1.SplitterDistance = 394
        Me.SplitContainer1.TabIndex = 2
        Me.SplitContainer1.Text = "SplitContainer1"
        '
        'lstSta
        '
        Me.lstSta.Dock = System.Windows.Forms.DockStyle.Right
        Me.lstSta.FormattingEnabled = True
        Me.lstSta.HorizontalScrollbar = True
        Me.lstSta.Location = New System.Drawing.Point(234, 0)
        Me.lstSta.Name = "lstSta"
        Me.lstSta.Size = New System.Drawing.Size(156, 436)
        Me.lstSta.TabIndex = 11
        '
        'cmdRefresh
        '
        Me.cmdRefresh.Location = New System.Drawing.Point(158, 312)
        Me.cmdRefresh.Name = "cmdRefresh"
        Me.cmdRefresh.Size = New System.Drawing.Size(67, 23)
        Me.cmdRefresh.TabIndex = 10
        Me.cmdRefresh.Text = "刷新"
        '
        'CmdDown
        '
        Me.CmdDown.Location = New System.Drawing.Point(158, 265)
        Me.CmdDown.Name = "CmdDown"
        Me.CmdDown.Size = New System.Drawing.Size(67, 23)
        Me.CmdDown.TabIndex = 7
        Me.CmdDown.Text = "向下移动"
        '
        'cmdUp
        '
        Me.cmdUp.Location = New System.Drawing.Point(158, 227)
        Me.cmdUp.Name = "cmdUp"
        Me.cmdUp.Size = New System.Drawing.Size(67, 23)
        Me.cmdUp.TabIndex = 6
        Me.cmdUp.Text = "向上移动"
        '
        'cmdDeleAll
        '
        Me.cmdDeleAll.Location = New System.Drawing.Point(158, 187)
        Me.cmdDeleAll.Name = "cmdDeleAll"
        Me.cmdDeleAll.Size = New System.Drawing.Size(67, 23)
        Me.cmdDeleAll.TabIndex = 5
        Me.cmdDeleAll.Text = "<<<"
        '
        'cmdDeleOne
        '
        Me.cmdDeleOne.Location = New System.Drawing.Point(158, 148)
        Me.cmdDeleOne.Name = "cmdDeleOne"
        Me.cmdDeleOne.Size = New System.Drawing.Size(67, 23)
        Me.cmdDeleOne.TabIndex = 4
        Me.cmdDeleOne.Text = "<-"
        '
        'cmdAddAll
        '
        Me.cmdAddAll.Location = New System.Drawing.Point(158, 109)
        Me.cmdAddAll.Name = "cmdAddAll"
        Me.cmdAddAll.Size = New System.Drawing.Size(67, 23)
        Me.cmdAddAll.TabIndex = 3
        Me.cmdAddAll.Text = ">>>"
        '
        'cmdAddOne
        '
        Me.cmdAddOne.Location = New System.Drawing.Point(158, 71)
        Me.cmdAddOne.Name = "cmdAddOne"
        Me.cmdAddOne.Size = New System.Drawing.Size(67, 23)
        Me.cmdAddOne.TabIndex = 2
        Me.cmdAddOne.Text = "->"
        '
        'lstBei
        '
        Me.lstBei.Dock = System.Windows.Forms.DockStyle.Left
        Me.lstBei.FormattingEnabled = True
        Me.lstBei.HorizontalScrollbar = True
        Me.lstBei.ItemHeight = 12
        Me.lstBei.Location = New System.Drawing.Point(0, 0)
        Me.lstBei.Name = "lstBei"
        Me.lstBei.Size = New System.Drawing.Size(152, 436)
        Me.lstBei.TabIndex = 0
        '
        'picSta
        '
        Me.picSta.BackColor = System.Drawing.Color.White
        Me.picSta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picSta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.picSta.Location = New System.Drawing.Point(0, 0)
        Me.picSta.Name = "picSta"
        Me.picSta.Size = New System.Drawing.Size(376, 444)
        Me.picSta.TabIndex = 0
        Me.picSta.TabStop = False
        '
        'frmDiTuStructure
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(778, 530)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmDiTuStructure"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "底图结构"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.picSta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnRefreshDrawLine As System.Windows.Forms.Button
    Friend WithEvents chkDuiQi As System.Windows.Forms.CheckBox
    Friend WithEvents cmbFenGeTime As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkSetecAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents lstSta As System.Windows.Forms.CheckedListBox
    Friend WithEvents cmdRefresh As System.Windows.Forms.Button
    Friend WithEvents CmdDown As System.Windows.Forms.Button
    Friend WithEvents cmdUp As System.Windows.Forms.Button
    Friend WithEvents cmdDeleAll As System.Windows.Forms.Button
    Friend WithEvents cmdDeleOne As System.Windows.Forms.Button
    Friend WithEvents cmdAddAll As System.Windows.Forms.Button
    Friend WithEvents cmdAddOne As System.Windows.Forms.Button
    Friend WithEvents lstBei As System.Windows.Forms.ListBox
    Friend WithEvents picSta As System.Windows.Forms.PictureBox
    Friend WithEvents chkAllMove As System.Windows.Forms.CheckBox
End Class
