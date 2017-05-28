<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmPrintStaJiShuTuJie
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
        Me.lstSta = New System.Windows.Forms.ListBox
        Me.cmdDeleAll = New System.Windows.Forms.Button
        Me.cmdDeleOne = New System.Windows.Forms.Button
        Me.cmdAddAll = New System.Windows.Forms.Button
        Me.cmdAddOne = New System.Windows.Forms.Button
        Me.lstBei = New System.Windows.Forms.ListBox
        Me.btnPrint = New System.Windows.Forms.Button
        Me.btnPreView = New System.Windows.Forms.Button
        Me.btnPageSet = New System.Windows.Forms.Button
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.txtSmallTitle = New System.Windows.Forms.TextBox
        Me.txtBigTitle = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.numPageTime = New System.Windows.Forms.NumericUpDown
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.numEndTime = New System.Windows.Forms.NumericUpDown
        Me.numBeTime = New System.Windows.Forms.NumericUpDown
        Me.Button1 = New System.Windows.Forms.Button
        Me.prgPrint = New System.Windows.Forms.ProgressBar
        Me.printDocDiagram = New System.Drawing.Printing.PrintDocument
        Me.cmbTimeFormat = New System.Windows.Forms.ComboBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.numPageTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numEndTime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numBeTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lstSta
        '
        Me.lstSta.FormattingEnabled = True
        Me.lstSta.HorizontalScrollbar = True
        Me.lstSta.ItemHeight = 12
        Me.lstSta.Location = New System.Drawing.Point(203, 12)
        Me.lstSta.Name = "lstSta"
        Me.lstSta.Size = New System.Drawing.Size(134, 148)
        Me.lstSta.TabIndex = 29
        '
        'cmdDeleAll
        '
        Me.cmdDeleAll.Location = New System.Drawing.Point(148, 128)
        Me.cmdDeleAll.Name = "cmdDeleAll"
        Me.cmdDeleAll.Size = New System.Drawing.Size(39, 23)
        Me.cmdDeleAll.TabIndex = 28
        Me.cmdDeleAll.Text = "<<<"
        '
        'cmdDeleOne
        '
        Me.cmdDeleOne.Location = New System.Drawing.Point(148, 89)
        Me.cmdDeleOne.Name = "cmdDeleOne"
        Me.cmdDeleOne.Size = New System.Drawing.Size(39, 23)
        Me.cmdDeleOne.TabIndex = 27
        Me.cmdDeleOne.Text = "<-"
        '
        'cmdAddAll
        '
        Me.cmdAddAll.Location = New System.Drawing.Point(148, 50)
        Me.cmdAddAll.Name = "cmdAddAll"
        Me.cmdAddAll.Size = New System.Drawing.Size(39, 23)
        Me.cmdAddAll.TabIndex = 26
        Me.cmdAddAll.Text = ">>>"
        '
        'cmdAddOne
        '
        Me.cmdAddOne.Location = New System.Drawing.Point(148, 12)
        Me.cmdAddOne.Name = "cmdAddOne"
        Me.cmdAddOne.Size = New System.Drawing.Size(39, 23)
        Me.cmdAddOne.TabIndex = 25
        Me.cmdAddOne.Text = "->"
        '
        'lstBei
        '
        Me.lstBei.FormattingEnabled = True
        Me.lstBei.HorizontalScrollbar = True
        Me.lstBei.ItemHeight = 12
        Me.lstBei.Location = New System.Drawing.Point(12, 12)
        Me.lstBei.Name = "lstBei"
        Me.lstBei.Size = New System.Drawing.Size(130, 148)
        Me.lstBei.TabIndex = 24
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(251, 374)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(88, 23)
        Me.btnPrint.TabIndex = 40
        Me.btnPrint.Text = "打印"
        '
        'btnPreView
        '
        Me.btnPreView.Location = New System.Drawing.Point(130, 374)
        Me.btnPreView.Name = "btnPreView"
        Me.btnPreView.Size = New System.Drawing.Size(88, 23)
        Me.btnPreView.TabIndex = 39
        Me.btnPreView.Text = "打印预览"
        '
        'btnPageSet
        '
        Me.btnPageSet.Location = New System.Drawing.Point(12, 374)
        Me.btnPageSet.Name = "btnPageSet"
        Me.btnPageSet.Size = New System.Drawing.Size(88, 23)
        Me.btnPageSet.TabIndex = 38
        Me.btnPageSet.Text = "打印设置"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtSmallTitle)
        Me.GroupBox5.Controls.Add(Me.txtBigTitle)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Controls.Add(Me.Label15)
        Me.GroupBox5.Location = New System.Drawing.Point(12, 260)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(325, 108)
        Me.GroupBox5.TabIndex = 37
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "其它参数:"
        '
        'txtSmallTitle
        '
        Me.txtSmallTitle.Location = New System.Drawing.Point(12, 71)
        Me.txtSmallTitle.Name = "txtSmallTitle"
        Me.txtSmallTitle.Size = New System.Drawing.Size(301, 21)
        Me.txtSmallTitle.TabIndex = 9
        '
        'txtBigTitle
        '
        Me.txtBigTitle.Location = New System.Drawing.Point(12, 32)
        Me.txtBigTitle.Name = "txtBigTitle"
        Me.txtBigTitle.Size = New System.Drawing.Size(301, 21)
        Me.txtBigTitle.TabIndex = 8
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(10, 56)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(47, 12)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "小标题:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(10, 17)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(47, 12)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "大标题:"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.cmbTimeFormat)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.numPageTime)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.numEndTime)
        Me.GroupBox4.Controls.Add(Me.numBeTime)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 166)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(325, 88)
        Me.GroupBox4.TabIndex = 36
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "时间设置:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(155, 58)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(17, 12)
        Me.Label1.TabIndex = 9
        Me.Label1.Text = "时"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 60)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(83, 12)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "每页打印时间:"
        '
        'numPageTime
        '
        Me.numPageTime.Location = New System.Drawing.Point(92, 51)
        Me.numPageTime.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.numPageTime.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numPageTime.Name = "numPageTime"
        Me.numPageTime.Size = New System.Drawing.Size(60, 21)
        Me.numPageTime.TabIndex = 8
        Me.numPageTime.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(297, 26)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(17, 12)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "时"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(155, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(17, 12)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "时"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(179, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(59, 12)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "结束时间:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 12)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "起始时间:"
        '
        'numEndTime
        '
        Me.numEndTime.Location = New System.Drawing.Point(239, 20)
        Me.numEndTime.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.numEndTime.Name = "numEndTime"
        Me.numEndTime.Size = New System.Drawing.Size(56, 21)
        Me.numEndTime.TabIndex = 3
        Me.numEndTime.Value = New Decimal(New Integer() {24, 0, 0, 0})
        '
        'numBeTime
        '
        Me.numBeTime.Location = New System.Drawing.Point(92, 20)
        Me.numBeTime.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.numBeTime.Name = "numBeTime"
        Me.numBeTime.Size = New System.Drawing.Size(60, 21)
        Me.numBeTime.TabIndex = 1
        Me.numBeTime.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(251, 422)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(88, 23)
        Me.Button1.TabIndex = 41
        Me.Button1.Text = "退出(&E)"
        '
        'prgPrint
        '
        Me.prgPrint.Location = New System.Drawing.Point(12, 403)
        Me.prgPrint.Name = "prgPrint"
        Me.prgPrint.Size = New System.Drawing.Size(327, 12)
        Me.prgPrint.TabIndex = 42
        Me.prgPrint.Visible = False
        '
        'printDocDiagram
        '
        '
        'cmbTimeFormat
        '
        Me.cmbTimeFormat.FormattingEnabled = True
        Me.cmbTimeFormat.Items.AddRange(New Object() {"一分格", "二分格", "十分格", "小时格"})
        Me.cmbTimeFormat.Location = New System.Drawing.Point(240, 51)
        Me.cmbTimeFormat.Name = "cmbTimeFormat"
        Me.cmbTimeFormat.Size = New System.Drawing.Size(73, 20)
        Me.cmbTimeFormat.TabIndex = 12
        Me.cmbTimeFormat.Text = "一分格"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(176, 56)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(59, 12)
        Me.Label17.TabIndex = 11
        Me.Label17.Text = "底图格式:"
        '
        'frmPrintStaJiShuTuJie
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(353, 451)
        Me.Controls.Add(Me.prgPrint)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.btnPreView)
        Me.Controls.Add(Me.btnPageSet)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.lstSta)
        Me.Controls.Add(Me.cmdDeleAll)
        Me.Controls.Add(Me.cmdDeleOne)
        Me.Controls.Add(Me.cmdAddAll)
        Me.Controls.Add(Me.cmdAddOne)
        Me.Controls.Add(Me.lstBei)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintStaJiShuTuJie"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "打印车站股道使用技术图解"
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.numPageTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numEndTime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numBeTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstSta As System.Windows.Forms.ListBox
    Friend WithEvents cmdDeleAll As System.Windows.Forms.Button
    Friend WithEvents cmdDeleOne As System.Windows.Forms.Button
    Friend WithEvents cmdAddAll As System.Windows.Forms.Button
    Friend WithEvents cmdAddOne As System.Windows.Forms.Button
    Friend WithEvents lstBei As System.Windows.Forms.ListBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnPreView As System.Windows.Forms.Button
    Friend WithEvents btnPageSet As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSmallTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtBigTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents numPageTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents numEndTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents numBeTime As System.Windows.Forms.NumericUpDown
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents prgPrint As System.Windows.Forms.ProgressBar
    Friend WithEvents printDocDiagram As System.Drawing.Printing.PrintDocument
    Friend WithEvents cmbTimeFormat As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
End Class
