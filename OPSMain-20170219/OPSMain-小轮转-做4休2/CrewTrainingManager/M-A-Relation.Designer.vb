<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class M_A_Relation
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(M_A_Relation))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TxtAPreM = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtATel = New System.Windows.Forms.TextBox()
        Me.TxtANO = New System.Windows.Forms.TextBox()
        Me.TxtAName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TxtPreA = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.TxtMTel = New System.Windows.Forms.TextBox()
        Me.TxtMNO = New System.Windows.Forms.TextBox()
        Me.TxtMName = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.BtnOK = New System.Windows.Forms.Button()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.TxtAPreM)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.TxtATel)
        Me.Panel1.Controls.Add(Me.TxtANO)
        Me.Panel1.Controls.Add(Me.TxtAName)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(-3, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(197, 301)
        Me.Panel1.TabIndex = 0
        '
        'TxtAPreM
        '
        Me.TxtAPreM.Location = New System.Drawing.Point(78, 216)
        Me.TxtAPreM.Name = "TxtAPreM"
        Me.TxtAPreM.Size = New System.Drawing.Size(100, 21)
        Me.TxtAPreM.TabIndex = 8
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(18, 219)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(53, 24)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "当前师傅" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(工号)"
        '
        'TxtATel
        '
        Me.TxtATel.Location = New System.Drawing.Point(78, 164)
        Me.TxtATel.Name = "TxtATel"
        Me.TxtATel.Size = New System.Drawing.Size(100, 21)
        Me.TxtATel.TabIndex = 6
        '
        'TxtANO
        '
        Me.TxtANO.Location = New System.Drawing.Point(78, 101)
        Me.TxtANO.Name = "TxtANO"
        Me.TxtANO.Size = New System.Drawing.Size(100, 21)
        Me.TxtANO.TabIndex = 5
        '
        'TxtAName
        '
        Me.TxtAName.Location = New System.Drawing.Point(78, 46)
        Me.TxtAName.Name = "TxtAName"
        Me.TxtAName.Size = New System.Drawing.Size(100, 21)
        Me.TxtAName.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 164)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "联系方式"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(29, 12)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "工号"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 49)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "姓名"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(89, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "徒弟信息"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.TxtPreA)
        Me.Panel2.Controls.Add(Me.Label10)
        Me.Panel2.Controls.Add(Me.TxtMTel)
        Me.Panel2.Controls.Add(Me.TxtMNO)
        Me.Panel2.Controls.Add(Me.TxtMName)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Location = New System.Drawing.Point(281, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(204, 301)
        Me.Panel2.TabIndex = 1
        '
        'TxtPreA
        '
        Me.TxtPreA.Location = New System.Drawing.Point(82, 216)
        Me.TxtPreA.Name = "TxtPreA"
        Me.TxtPreA.Size = New System.Drawing.Size(100, 21)
        Me.TxtPreA.TabIndex = 11
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(23, 219)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 24)
        Me.Label10.TabIndex = 10
        Me.Label10.Text = "当前徒弟" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "（工号）"
        '
        'TxtMTel
        '
        Me.TxtMTel.Location = New System.Drawing.Point(82, 161)
        Me.TxtMTel.Name = "TxtMTel"
        Me.TxtMTel.Size = New System.Drawing.Size(100, 21)
        Me.TxtMTel.TabIndex = 9
        '
        'TxtMNO
        '
        Me.TxtMNO.Location = New System.Drawing.Point(82, 101)
        Me.TxtMNO.Name = "TxtMNO"
        Me.TxtMNO.Size = New System.Drawing.Size(100, 21)
        Me.TxtMNO.TabIndex = 8
        '
        'TxtMName
        '
        Me.TxtMName.Location = New System.Drawing.Point(82, 52)
        Me.TxtMName.Name = "TxtMName"
        Me.TxtMName.Size = New System.Drawing.Size(100, 21)
        Me.TxtMName.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(23, 164)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(53, 12)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "联系方式"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(23, 110)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 12)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "工号"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(23, 55)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "姓名"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(80, 13)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "师傅信息"
        '
        'BtnOK
        '
        Me.BtnOK.Image = CType(resources.GetObject("BtnOK.Image"), System.Drawing.Image)
        Me.BtnOK.Location = New System.Drawing.Point(200, 93)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New System.Drawing.Size(75, 34)
        Me.BtnOK.TabIndex = 2
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.Image = CType(resources.GetObject("BtnCancel.Image"), System.Drawing.Image)
        Me.BtnCancel.Location = New System.Drawing.Point(200, 208)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(75, 34)
        Me.BtnCancel.TabIndex = 3
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'M_A_Relation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(486, 302)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "M_A_Relation"
        Me.Text = "师徒关系表"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents BtnOK As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents TxtATel As System.Windows.Forms.TextBox
    Friend WithEvents TxtANO As System.Windows.Forms.TextBox
    Friend WithEvents TxtAName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtMTel As System.Windows.Forms.TextBox
    Friend WithEvents TxtMNO As System.Windows.Forms.TextBox
    Friend WithEvents TxtMName As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtAPreM As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TxtPreA As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
