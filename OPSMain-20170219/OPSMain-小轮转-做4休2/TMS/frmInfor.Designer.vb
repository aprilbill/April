<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmInfor
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
        Me.txtInfor = New System.Windows.Forms.TextBox
        Me.btnEXit = New System.Windows.Forms.Button
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtInfor
        '
        Me.txtInfor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtInfor.Location = New System.Drawing.Point(0, 0)
        Me.txtInfor.Multiline = True
        Me.txtInfor.Name = "txtInfor"
        Me.txtInfor.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtInfor.Size = New System.Drawing.Size(334, 405)
        Me.txtInfor.TabIndex = 0
        '
        'btnEXit
        '
        Me.btnEXit.Location = New System.Drawing.Point(247, 8)
        Me.btnEXit.Name = "btnEXit"
        Me.btnEXit.Size = New System.Drawing.Size(75, 23)
        Me.btnEXit.TabIndex = 1
        Me.btnEXit.Text = "ÍË³ö(&E)"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.txtInfor)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnEXit)
        Me.SplitContainer1.Size = New System.Drawing.Size(334, 452)
        Me.SplitContainer1.SplitterDistance = 405
        Me.SplitContainer1.TabIndex = 2
        Me.SplitContainer1.Text = "SplitContainer1"
        '
        'frmInfor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 452)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmInfor"
        Me.Text = "frmInfor"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtInfor As System.Windows.Forms.TextBox
    Friend WithEvents btnEXit As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
End Class
