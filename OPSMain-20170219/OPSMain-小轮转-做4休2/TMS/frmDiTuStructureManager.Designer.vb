<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmDiTuStructureManager
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnRename = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.listViewName = New System.Windows.Forms.ListView
        Me.µ×Í¼Ãû³Æ = New System.Windows.Forms.ColumnHeader
        Me.×´Ì¬ = New System.Windows.Forms.ColumnHeader
        Me.btnSetDefault = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ÏÖÓÐµ×Í¼½á¹¹Ãû³Æ"
        '
        'btnRename
        '
        Me.btnRename.Location = New System.Drawing.Point(8, 209)
        Me.btnRename.Name = "btnRename"
        Me.btnRename.Size = New System.Drawing.Size(102, 23)
        Me.btnRename.TabIndex = 2
        Me.btnRename.Text = "ÐÞ¸ÄÃû³Æ(&R)"
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(116, 209)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(102, 23)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "É¾³ý(&D)"
        '
        'listViewName
        '
        Me.listViewName.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.listViewName.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.µ×Í¼Ãû³Æ, Me.×´Ì¬})
        Me.listViewName.GridLines = True
        Me.listViewName.HoverSelection = True
        Me.listViewName.Location = New System.Drawing.Point(12, 33)
        Me.listViewName.MultiSelect = False
        Me.listViewName.Name = "listViewName"
        Me.listViewName.Size = New System.Drawing.Size(306, 159)
        Me.listViewName.TabIndex = 4
        Me.listViewName.View = System.Windows.Forms.View.Details
        '
        'µ×Í¼Ãû³Æ
        '
        Me.µ×Í¼Ãû³Æ.Text = "µ×Í¼Ãû³Æ"
        Me.µ×Í¼Ãû³Æ.Width = 217
        '
        '×´Ì¬
        '
        Me.×´Ì¬.Text = "×´Ì¬"
        Me.×´Ì¬.Width = 57
        '
        'btnSetDefault
        '
        Me.btnSetDefault.Location = New System.Drawing.Point(224, 209)
        Me.btnSetDefault.Name = "btnSetDefault"
        Me.btnSetDefault.Size = New System.Drawing.Size(102, 23)
        Me.btnSetDefault.TabIndex = 5
        Me.btnSetDefault.Text = "ÉèÎªÄ¬ÈÏ(&S)"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(224, 246)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(102, 23)
        Me.btnExit.TabIndex = 6
        Me.btnExit.Text = "ÍË³ö(&E)"
        '
        'frmDiTuStructureManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(330, 279)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSetDefault)
        Me.Controls.Add(Me.listViewName)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnRename)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDiTuStructureManager"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "µ×Í¼½á¹¹¹ÜÀí"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnRename As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents listViewName As System.Windows.Forms.ListView
    Friend WithEvents btnSetDefault As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents µ×Í¼Ãû³Æ As System.Windows.Forms.ColumnHeader
    Friend WithEvents ×´Ì¬ As System.Windows.Forms.ColumnHeader
End Class
