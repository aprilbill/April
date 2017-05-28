<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmNewDriverInputBox
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
        Me.cmdOK = New System.Windows.Forms.Button()
        Me.txtText = New System.Windows.Forms.TextBox()
        Me.cmdExit = New System.Windows.Forms.Button()
        Me.cmbText = New System.Windows.Forms.ComboBox()
        Me.labTitle = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cmdOK
        '
        Me.cmdOK.Location = New System.Drawing.Point(317, 12)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 23)
        Me.cmdOK.TabIndex = 0
        Me.cmdOK.Text = "确定(&Y)"
        '
        'txtText
        '
        Me.txtText.Location = New System.Drawing.Point(89, 77)
        Me.txtText.Name = "txtText"
        Me.txtText.Size = New System.Drawing.Size(303, 21)
        Me.txtText.TabIndex = 3
        '
        'cmdExit
        '
        Me.cmdExit.Location = New System.Drawing.Point(317, 44)
        Me.cmdExit.Name = "cmdExit"
        Me.cmdExit.Size = New System.Drawing.Size(75, 23)
        Me.cmdExit.TabIndex = 4
        Me.cmdExit.Text = "取消(&C)"
        '
        'cmbText
        '
        Me.cmbText.FormattingEnabled = True
        Me.cmbText.Location = New System.Drawing.Point(89, 104)
        Me.cmbText.Name = "cmbText"
        Me.cmbText.Size = New System.Drawing.Size(303, 20)
        Me.cmbText.TabIndex = 2
        '
        'labTitle
        '
        Me.labTitle.Location = New System.Drawing.Point(12, 12)
        Me.labTitle.Name = "labTitle"
        Me.labTitle.Size = New System.Drawing.Size(133, 23)
        Me.labTitle.TabIndex = 1
        Me.labTitle.Text = "输入新乘务员信息"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(12, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 18)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "乘务员编号"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(12, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 18)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "乘务员班种"
        '
        'frmNewDriverInputBox
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(401, 141)
        Me.Controls.Add(Me.cmdExit)
        Me.Controls.Add(Me.cmbText)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.labTitle)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.txtText)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmNewDriverInputBox"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "输入窗体"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmdOK As System.Windows.Forms.Button
    Friend WithEvents cmdExit As System.Windows.Forms.Button
    Public WithEvents txtText As System.Windows.Forms.TextBox
    Public WithEvents cmbText As System.Windows.Forms.ComboBox
    Public WithEvents labTitle As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
End Class
