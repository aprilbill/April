<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmPrintCheDiJiaoLu
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
        Me.lstCheDi = New System.Windows.Forms.CheckedListBox
        Me.btnNotSelectAll = New System.Windows.Forms.Button
        Me.btnSelectAll = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmbFontHeight = New System.Windows.Forms.ComboBox
        Me.CombJLWidth = New System.Windows.Forms.ComboBox
        Me.cmbLineWidth = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnCreate = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.proBar = New System.Windows.Forms.ProgressBar
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.CAD格式输出 = New System.Windows.Forms.TabPage
        Me.EXCEL格式输出 = New System.Windows.Forms.TabPage
        Me.numRowNum = New System.Windows.Forms.NumericUpDown
        Me.Label10 = New System.Windows.Forms.Label
        Me.btnOk = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lstPrintSta = New System.Windows.Forms.ListBox
        Me.lstSta = New System.Windows.Forms.ListBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.chkLstCheDiNum = New System.Windows.Forms.CheckedListBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.CAD格式输出.SuspendLayout()
        Me.EXCEL格式输出.SuspendLayout()
        CType(Me.numRowNum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstCheDi
        '
        Me.lstCheDi.FormattingEnabled = True
        Me.lstCheDi.Location = New System.Drawing.Point(15, 44)
        Me.lstCheDi.Name = "lstCheDi"
        Me.lstCheDi.Size = New System.Drawing.Size(120, 184)
        Me.lstCheDi.TabIndex = 21
        '
        'btnNotSelectAll
        '
        Me.btnNotSelectAll.Location = New System.Drawing.Point(15, 273)
        Me.btnNotSelectAll.Name = "btnNotSelectAll"
        Me.btnNotSelectAll.Size = New System.Drawing.Size(120, 23)
        Me.btnNotSelectAll.TabIndex = 20
        Me.btnNotSelectAll.Text = "取消全选"
        '
        'btnSelectAll
        '
        Me.btnSelectAll.Location = New System.Drawing.Point(15, 244)
        Me.btnSelectAll.Name = "btnSelectAll"
        Me.btnSelectAll.Size = New System.Drawing.Size(120, 23)
        Me.btnSelectAll.TabIndex = 19
        Me.btnSelectAll.Text = "全选"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 12)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "选择需要输出的车底号"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbFontHeight)
        Me.GroupBox1.Controls.Add(Me.CombJLWidth)
        Me.GroupBox1.Controls.Add(Me.cmbLineWidth)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(153, 44)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(243, 184)
        Me.GroupBox1.TabIndex = 23
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "输出参数设置"
        '
        'cmbFontHeight
        '
        Me.cmbFontHeight.FormattingEnabled = True
        Me.cmbFontHeight.Location = New System.Drawing.Point(141, 111)
        Me.cmbFontHeight.Name = "cmbFontHeight"
        Me.cmbFontHeight.Size = New System.Drawing.Size(88, 20)
        Me.cmbFontHeight.TabIndex = 32
        Me.cmbFontHeight.Text = "5"
        '
        'CombJLWidth
        '
        Me.CombJLWidth.FormattingEnabled = True
        Me.CombJLWidth.Location = New System.Drawing.Point(141, 74)
        Me.CombJLWidth.Name = "CombJLWidth"
        Me.CombJLWidth.Size = New System.Drawing.Size(88, 20)
        Me.CombJLWidth.TabIndex = 31
        Me.CombJLWidth.Text = "200"
        '
        'cmbLineWidth
        '
        Me.cmbLineWidth.FormattingEnabled = True
        Me.cmbLineWidth.Location = New System.Drawing.Point(141, 38)
        Me.cmbLineWidth.Name = "cmbLineWidth"
        Me.cmbLineWidth.Size = New System.Drawing.Size(88, 20)
        Me.cmbLineWidth.TabIndex = 30
        Me.cmbLineWidth.Text = "20"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 114)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 12)
        Me.Label5.TabIndex = 29
        Me.Label5.Text = "选择文字大小:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 77)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 12)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "交路线所占空间宽度:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 12)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "选择交路线宽度:"
        '
        'btnCreate
        '
        Me.btnCreate.Location = New System.Drawing.Point(153, 275)
        Me.btnCreate.Name = "btnCreate"
        Me.btnCreate.Size = New System.Drawing.Size(243, 23)
        Me.btnCreate.TabIndex = 24
        Me.btnCreate.Text = "开始生成(CAD格式)"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(350, 353)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(83, 23)
        Me.btnExit.TabIndex = 25
        Me.btnExit.Text = "退出(&E)"
        '
        'proBar
        '
        Me.proBar.Location = New System.Drawing.Point(12, 358)
        Me.proBar.Name = "proBar"
        Me.proBar.Size = New System.Drawing.Size(332, 18)
        Me.proBar.TabIndex = 26
        Me.proBar.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.CAD格式输出)
        Me.TabControl1.Controls.Add(Me.EXCEL格式输出)
        Me.TabControl1.Location = New System.Drawing.Point(12, 9)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(421, 338)
        Me.TabControl1.TabIndex = 27
        '
        'CAD格式输出
        '
        Me.CAD格式输出.Controls.Add(Me.Label1)
        Me.CAD格式输出.Controls.Add(Me.btnSelectAll)
        Me.CAD格式输出.Controls.Add(Me.btnNotSelectAll)
        Me.CAD格式输出.Controls.Add(Me.btnCreate)
        Me.CAD格式输出.Controls.Add(Me.lstCheDi)
        Me.CAD格式输出.Controls.Add(Me.GroupBox1)
        Me.CAD格式输出.Location = New System.Drawing.Point(4, 21)
        Me.CAD格式输出.Name = "CAD格式输出"
        Me.CAD格式输出.Padding = New System.Windows.Forms.Padding(3)
        Me.CAD格式输出.Size = New System.Drawing.Size(413, 313)
        Me.CAD格式输出.TabIndex = 0
        Me.CAD格式输出.Text = "AutoCAD格式输出"
        '
        'EXCEL格式输出
        '
        Me.EXCEL格式输出.Controls.Add(Me.numRowNum)
        Me.EXCEL格式输出.Controls.Add(Me.Label10)
        Me.EXCEL格式输出.Controls.Add(Me.btnOk)
        Me.EXCEL格式输出.Controls.Add(Me.GroupBox2)
        Me.EXCEL格式输出.Controls.Add(Me.Button1)
        Me.EXCEL格式输出.Controls.Add(Me.Button2)
        Me.EXCEL格式输出.Controls.Add(Me.chkLstCheDiNum)
        Me.EXCEL格式输出.Controls.Add(Me.Label4)
        Me.EXCEL格式输出.Location = New System.Drawing.Point(4, 21)
        Me.EXCEL格式输出.Name = "EXCEL格式输出"
        Me.EXCEL格式输出.Padding = New System.Windows.Forms.Padding(3)
        Me.EXCEL格式输出.Size = New System.Drawing.Size(413, 313)
        Me.EXCEL格式输出.TabIndex = 1
        Me.EXCEL格式输出.Text = "EXCEL格式输出"
        '
        'numRowNum
        '
        Me.numRowNum.Location = New System.Drawing.Point(222, 248)
        Me.numRowNum.Name = "numRowNum"
        Me.numRowNum.Size = New System.Drawing.Size(78, 21)
        Me.numRowNum.TabIndex = 15
        Me.numRowNum.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(134, 252)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(82, 12)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "每行输出数量:"
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(136, 281)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(271, 23)
        Me.btnOk.TabIndex = 12
        Me.btnOk.Text = "开始生成(EXCEL格式)(&C)"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.lstPrintSta)
        Me.GroupBox2.Controls.Add(Me.lstSta)
        Me.GroupBox2.Location = New System.Drawing.Point(136, 10)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(271, 225)
        Me.GroupBox2.TabIndex = 10
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "车站名输出"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(199, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 12)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "(双击修改)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(122, 94)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(16, 12)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "=>"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(146, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(52, 12)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "输出站名"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(7, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(64, 12)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "原车站名："
        '
        'lstPrintSta
        '
        Me.lstPrintSta.FormattingEnabled = True
        Me.lstPrintSta.ItemHeight = 12
        Me.lstPrintSta.Location = New System.Drawing.Point(146, 43)
        Me.lstPrintSta.Name = "lstPrintSta"
        Me.lstPrintSta.Size = New System.Drawing.Size(110, 172)
        Me.lstPrintSta.TabIndex = 1
        '
        'lstSta
        '
        Me.lstSta.FormattingEnabled = True
        Me.lstSta.ItemHeight = 12
        Me.lstSta.Location = New System.Drawing.Point(7, 43)
        Me.lstSta.Name = "lstSta"
        Me.lstSta.Size = New System.Drawing.Size(110, 172)
        Me.lstSta.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(15, 278)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(105, 23)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "全不选(&N)"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(15, 249)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(105, 23)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "全选(&A)"
        '
        'chkLstCheDiNum
        '
        Me.chkLstCheDiNum.FormattingEnabled = True
        Me.chkLstCheDiNum.Location = New System.Drawing.Point(15, 30)
        Me.chkLstCheDiNum.Name = "chkLstCheDiNum"
        Me.chkLstCheDiNum.Size = New System.Drawing.Size(105, 202)
        Me.chkLstCheDiNum.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 12)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "选择车底编号"
        '
        'frmPrintCheDiJiaoLu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(443, 386)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.proBar)
        Me.Controls.Add(Me.btnExit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintCheDiJiaoLu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "输出车底交路图"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.CAD格式输出.ResumeLayout(False)
        Me.CAD格式输出.PerformLayout()
        Me.EXCEL格式输出.ResumeLayout(False)
        Me.EXCEL格式输出.PerformLayout()
        CType(Me.numRowNum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lstCheDi As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnNotSelectAll As System.Windows.Forms.Button
    Friend WithEvents btnSelectAll As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbFontHeight As System.Windows.Forms.ComboBox
    Friend WithEvents CombJLWidth As System.Windows.Forms.ComboBox
    Friend WithEvents cmbLineWidth As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnCreate As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents proBar As System.Windows.Forms.ProgressBar
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents CAD格式输出 As System.Windows.Forms.TabPage
    Friend WithEvents EXCEL格式输出 As System.Windows.Forms.TabPage
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents chkLstCheDiNum As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lstPrintSta As System.Windows.Forms.ListBox
    Friend WithEvents lstSta As System.Windows.Forms.ListBox
    Friend WithEvents numRowNum As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents btnOk As System.Windows.Forms.Button
End Class
