<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmTimeTablePara
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
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.rbtFour = New System.Windows.Forms.RadioButton
        Me.rbtSix = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.numGuDaoLineHeight = New System.Windows.Forms.NumericUpDown
        Me.Label4 = New System.Windows.Forms.Label
        Me.cmbCheDiLineStyle = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.numCheDiLineHeight = New System.Windows.Forms.NumericUpDown
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkXieCheci = New System.Windows.Forms.CheckBox
        Me.chkShowCheCi = New System.Windows.Forms.CheckBox
        Me.NumLineMoveStep = New System.Windows.Forms.NumericUpDown
        Me.Label5 = New System.Windows.Forms.Label
        Me.numLineAdjustWidth = New System.Windows.Forms.NumericUpDown
        Me.Label3 = New System.Windows.Forms.Label
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.dataGrid = New System.Windows.Forms.DataGridView
        Me.��� = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.�������� = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.��ֵ = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnSetDefault = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.TabControl1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.numGuDaoLineHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numCheDiLineHeight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumLineMoveStep, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numLineAdjustWidth, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage1.SuspendLayout()
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(315, 366)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox2)
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Controls.Add(Me.NumLineMoveStep)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.numLineAdjustWidth)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Location = New System.Drawing.Point(4, 21)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(307, 341)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "��ʾ����"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbtFour)
        Me.GroupBox2.Controls.Add(Me.rbtSix)
        Me.GroupBox2.Location = New System.Drawing.Point(27, 217)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(248, 53)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "������ʾ��ʽ��"
        '
        'rbtFour
        '
        Me.rbtFour.AutoSize = True
        Me.rbtFour.Location = New System.Drawing.Point(128, 20)
        Me.rbtFour.Name = "rbtFour"
        Me.rbtFour.Size = New System.Drawing.Size(59, 16)
        Me.rbtFour.TabIndex = 1
        Me.rbtFour.Text = "������"
        Me.rbtFour.UseVisualStyleBackColor = True
        '
        'rbtSix
        '
        Me.rbtSix.AutoSize = True
        Me.rbtSix.Checked = True
        Me.rbtSix.Location = New System.Drawing.Point(17, 20)
        Me.rbtSix.Name = "rbtSix"
        Me.rbtSix.Size = New System.Drawing.Size(89, 16)
        Me.rbtSix.TabIndex = 0
        Me.rbtSix.TabStop = True
        Me.rbtSix.Text = "���׺�+����"
        Me.rbtSix.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Panel1)
        Me.GroupBox1.Controls.Add(Me.numGuDaoLineHeight)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbCheDiLineStyle)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.numCheDiLineHeight)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.chkXieCheci)
        Me.GroupBox1.Controls.Add(Me.chkShowCheCi)
        Me.GroupBox1.Location = New System.Drawing.Point(27, 15)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(248, 186)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "�����߲���"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Black
        Me.Panel1.Location = New System.Drawing.Point(33, 82)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(190, 1)
        Me.Panel1.TabIndex = 10
        '
        'numGuDaoLineHeight
        '
        Me.numGuDaoLineHeight.Location = New System.Drawing.Point(152, 152)
        Me.numGuDaoLineHeight.Name = "numGuDaoLineHeight"
        Me.numGuDaoLineHeight.Size = New System.Drawing.Size(71, 21)
        Me.numGuDaoLineHeight.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(31, 152)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 12)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "��վ�ɵ��߼��:"
        '
        'cmbCheDiLineStyle
        '
        Me.cmbCheDiLineStyle.FormattingEnabled = True
        Me.cmbCheDiLineStyle.Items.AddRange(New Object() {"������", "������"})
        Me.cmbCheDiLineStyle.Location = New System.Drawing.Point(152, 126)
        Me.cmbCheDiLineStyle.Name = "cmbCheDiLineStyle"
        Me.cmbCheDiLineStyle.Size = New System.Drawing.Size(71, 20)
        Me.cmbCheDiLineStyle.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(31, 126)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 12)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "����������:"
        '
        'numCheDiLineHeight
        '
        Me.numCheDiLineHeight.Location = New System.Drawing.Point(152, 98)
        Me.numCheDiLineHeight.Name = "numCheDiLineHeight"
        Me.numCheDiLineHeight.Size = New System.Drawing.Size(71, 21)
        Me.numCheDiLineHeight.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(31, 98)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "�����߸߶�:"
        '
        'chkXieCheci
        '
        Me.chkXieCheci.AutoSize = True
        Me.chkXieCheci.Location = New System.Drawing.Point(32, 51)
        Me.chkXieCheci.Name = "chkXieCheci"
        Me.chkXieCheci.Size = New System.Drawing.Size(96, 16)
        Me.chkXieCheci.TabIndex = 1
        Me.chkXieCheci.Text = "��ʾб�򳵴�"
        Me.chkXieCheci.UseVisualStyleBackColor = True
        '
        'chkShowCheCi
        '
        Me.chkShowCheCi.AutoSize = True
        Me.chkShowCheCi.Location = New System.Drawing.Point(32, 29)
        Me.chkShowCheCi.Name = "chkShowCheCi"
        Me.chkShowCheCi.Size = New System.Drawing.Size(96, 16)
        Me.chkShowCheCi.TabIndex = 0
        Me.chkShowCheCi.Text = "��ʾ���ױ��"
        Me.chkShowCheCi.UseVisualStyleBackColor = True
        '
        'NumLineMoveStep
        '
        Me.NumLineMoveStep.Location = New System.Drawing.Point(179, 313)
        Me.NumLineMoveStep.Maximum = New Decimal(New Integer() {3600, 0, 0, 0})
        Me.NumLineMoveStep.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumLineMoveStep.Name = "NumLineMoveStep"
        Me.NumLineMoveStep.Size = New System.Drawing.Size(71, 21)
        Me.NumLineMoveStep.TabIndex = 11
        Me.NumLineMoveStep.Value = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumLineMoveStep.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(57, 315)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 12)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "�������ƶ�����:"
        Me.Label5.Visible = False
        '
        'numLineAdjustWidth
        '
        Me.numLineAdjustWidth.Increment = New Decimal(New Integer() {60, 0, 0, 0})
        Me.numLineAdjustWidth.Location = New System.Drawing.Point(179, 286)
        Me.numLineAdjustWidth.Maximum = New Decimal(New Integer() {86400, 0, 0, 0})
        Me.numLineAdjustWidth.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numLineAdjustWidth.Name = "numLineAdjustWidth"
        Me.numLineAdjustWidth.Size = New System.Drawing.Size(71, 21)
        Me.numLineAdjustWidth.TabIndex = 7
        Me.numLineAdjustWidth.Value = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numLineAdjustWidth.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(57, 288)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(119, 12)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "�����߿ɵ���ʱ���:"
        Me.Label3.Visible = False
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.dataGrid)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(307, 341)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "ʱ�����"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'dataGrid
        '
        Me.dataGrid.AllowUserToAddRows = False
        Me.dataGrid.AllowUserToDeleteRows = False
        Me.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dataGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.���, Me.��������, Me.��ֵ})
        Me.dataGrid.Location = New System.Drawing.Point(6, 7)
        Me.dataGrid.Name = "dataGrid"
        Me.dataGrid.RowHeadersWidth = 30
        Me.dataGrid.RowTemplate.Height = 23
        Me.dataGrid.Size = New System.Drawing.Size(295, 328)
        Me.dataGrid.TabIndex = 72
        '
        '���
        '
        Me.���.HeaderText = "���"
        Me.���.Name = "���"
        Me.���.Width = 40
        '
        '��������
        '
        Me.��������.HeaderText = "��������"
        Me.��������.Name = "��������"
        Me.��������.Width = 130
        '
        '��ֵ
        '
        Me.��ֵ.HeaderText = "��ֵ"
        Me.��ֵ.Name = "��ֵ"
        Me.��ֵ.Width = 80
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(155, 390)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "ȷ��(&Y)"
        '
        'btnSetDefault
        '
        Me.btnSetDefault.Location = New System.Drawing.Point(12, 390)
        Me.btnSetDefault.Name = "btnSetDefault"
        Me.btnSetDefault.Size = New System.Drawing.Size(99, 23)
        Me.btnSetDefault.TabIndex = 3
        Me.btnSetDefault.Text = "��ΪĬ��(&S)"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(252, 390)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "�˳�(&E)"
        '
        'frmTimeTablePara
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(339, 425)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnSetDefault)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTimeTablePara"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "����ͼ��������"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.numGuDaoLineHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numCheDiLineHeight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumLineMoveStep, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numLineAdjustWidth, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage1.ResumeLayout(False)
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnSetDefault As System.Windows.Forms.Button
    Friend WithEvents dataGrid As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ��� As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents �������� As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ��ֵ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents chkXieCheci As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowCheCi As System.Windows.Forms.CheckBox
    Friend WithEvents numCheDiLineHeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbCheDiLineStyle As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents numLineAdjustWidth As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents numGuDaoLineHeight As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents NumLineMoveStep As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtFour As System.Windows.Forms.RadioButton
    Friend WithEvents rbtSix As System.Windows.Forms.RadioButton
End Class
