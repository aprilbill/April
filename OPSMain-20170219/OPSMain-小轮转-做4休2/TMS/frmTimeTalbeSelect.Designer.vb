Public Partial Class frmTimeTalbeSelect
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

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
        Me.Label1 = New System.Windows.Forms.Label
        Me.lstName = New System.Windows.Forms.ListBox
        Me.cmdOk = New System.Windows.Forms.Button
        Me.cmbCancel = New System.Windows.Forms.Button
        Me.proBar = New System.Windows.Forms.ProgressBar
        Me.LabInfor = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "请从下列时刻表中选择"
        '
        'lstName
        '
        Me.lstName.FormattingEnabled = True
        Me.lstName.HorizontalScrollbar = True
        Me.lstName.ItemHeight = 12
        Me.lstName.Location = New System.Drawing.Point(13, 36)
        Me.lstName.Name = "lstName"
        Me.lstName.Size = New System.Drawing.Size(394, 256)
        Me.lstName.TabIndex = 1
        '
        'cmdOk
        '
        Me.cmdOk.Location = New System.Drawing.Point(264, 375)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(71, 24)
        Me.cmdOk.TabIndex = 2
        Me.cmdOk.Text = "确定(&O)"
        '
        'cmbCancel
        '
        Me.cmbCancel.Location = New System.Drawing.Point(342, 375)
        Me.cmbCancel.Name = "cmbCancel"
        Me.cmbCancel.Size = New System.Drawing.Size(71, 24)
        Me.cmbCancel.TabIndex = 3
        Me.cmbCancel.Text = "取消(&C)"
        '
        'proBar
        '
        Me.proBar.Location = New System.Drawing.Point(11, 352)
        Me.proBar.Name = "proBar"
        Me.proBar.Size = New System.Drawing.Size(402, 17)
        Me.proBar.TabIndex = 4
        Me.proBar.Visible = False
        '
        'LabInfor
        '
        Me.LabInfor.AutoSize = True
        Me.LabInfor.Location = New System.Drawing.Point(13, 301)
        Me.LabInfor.Name = "LabInfor"
        Me.LabInfor.Size = New System.Drawing.Size(71, 12)
        Me.LabInfor.TabIndex = 5
        Me.LabInfor.Text = "时刻表信息:"
        '
        'frmTimeTalbeSelect
        '
        Me.ClientSize = New System.Drawing.Size(419, 411)
        Me.Controls.Add(Me.LabInfor)
        Me.Controls.Add(Me.proBar)
        Me.Controls.Add(Me.cmbCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Controls.Add(Me.lstName)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTimeTalbeSelect"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "选择时刻表"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lstName As System.Windows.Forms.ListBox
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents cmbCancel As System.Windows.Forms.Button
    Friend WithEvents proBar As System.Windows.Forms.ProgressBar
    Friend WithEvents LabInfor As System.Windows.Forms.Label
End Class
