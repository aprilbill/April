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
        Me.btnExit = New System.Windows.Forms.Button()
        Me.proBar = New System.Windows.Forms.ProgressBar()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.EXCEL格式输出 = New System.Windows.Forms.TabPage()
        Me.numRowNum = New System.Windows.Forms.NumericUpDown()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lstPrintSta = New System.Windows.Forms.ListBox()
        Me.lstSta = New System.Windows.Forms.ListBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.chkLstCheDiNum = New System.Windows.Forms.CheckedListBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.EXCEL格式输出.SuspendLayout()
        CType(Me.numRowNum, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
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
        Me.TabControl1.Controls.Add(Me.EXCEL格式输出)
        Me.TabControl1.Location = New System.Drawing.Point(12, 9)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(421, 338)
        Me.TabControl1.TabIndex = 27
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
        Me.EXCEL格式输出.Location = New System.Drawing.Point(4, 22)
        Me.EXCEL格式输出.Name = "EXCEL格式输出"
        Me.EXCEL格式输出.Padding = New System.Windows.Forms.Padding(3)
        Me.EXCEL格式输出.Size = New System.Drawing.Size(413, 312)
        Me.EXCEL格式输出.TabIndex = 1
        Me.EXCEL格式输出.Text = "EXCEL格式输出"
        Me.EXCEL格式输出.UseVisualStyleBackColor = True
        '
        'numRowNum
        '
        Me.numRowNum.Location = New System.Drawing.Point(222, 248)
        Me.numRowNum.Name = "numRowNum"
        Me.numRowNum.Size = New System.Drawing.Size(78, 21)
        Me.numRowNum.TabIndex = 15
        Me.numRowNum.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(134, 252)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(83, 12)
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
        Me.Label6.Size = New System.Drawing.Size(65, 12)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "(双击修改)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(122, 94)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(17, 12)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "=>"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(146, 20)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 12)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "输出站名"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(7, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(65, 12)
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
        Me.chkLstCheDiNum.Size = New System.Drawing.Size(105, 196)
        Me.chkLstCheDiNum.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 7)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 12)
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
        Me.TabControl1.ResumeLayout(False)
        Me.EXCEL格式输出.ResumeLayout(False)
        Me.EXCEL格式输出.PerformLayout()
        CType(Me.numRowNum, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents proBar As System.Windows.Forms.ProgressBar
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
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
