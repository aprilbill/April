<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmShowErrorInfor
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
        Me.listViewTrain = New System.Windows.Forms.ListView
        Me.��� = New System.Windows.Forms.ColumnHeader
        Me.���� = New System.Windows.Forms.ColumnHeader
        Me.�г�ID = New System.Windows.Forms.ColumnHeader
        Me.ʱ�� = New System.Windows.Forms.ColumnHeader
        Me.������Ϣ = New System.Windows.Forms.ColumnHeader
        Me.SuspendLayout()
        '
        'listViewTrain
        '
        Me.listViewTrain.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.listViewTrain.AllowColumnReorder = True
        Me.listViewTrain.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.���, Me.����, Me.�г�ID, Me.ʱ��, Me.������Ϣ})
        Me.listViewTrain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listViewTrain.Font = New System.Drawing.Font("����", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.listViewTrain.FullRowSelect = True
        Me.listViewTrain.GridLines = True
        Me.listViewTrain.HoverSelection = True
        Me.listViewTrain.Location = New System.Drawing.Point(0, 0)
        Me.listViewTrain.MultiSelect = False
        Me.listViewTrain.Name = "listViewTrain"
        Me.listViewTrain.Size = New System.Drawing.Size(525, 168)
        Me.listViewTrain.TabIndex = 5
        Me.listViewTrain.View = System.Windows.Forms.View.Details
        '
        '���
        '
        Me.���.Text = "���"
        Me.���.Width = 40
        '
        '����
        '
        Me.����.Text = "����"
        Me.����.Width = 57
        '
        '�г�ID
        '
        Me.�г�ID.Text = "�г�ID"
        '
        'ʱ��
        '
        Me.ʱ��.Text = "ʱ��"
        '
        '������Ϣ
        '
        Me.������Ϣ.Text = "������Ϣ"
        Me.������Ϣ.Width = 300
        '
        'frmShowErrorInfor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(525, 168)
        Me.Controls.Add(Me.listViewTrain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "frmShowErrorInfor"
        Me.Text = "������Ϣ"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents listViewTrain As System.Windows.Forms.ListView
    Friend WithEvents ��� As System.Windows.Forms.ColumnHeader
    Friend WithEvents ���� As System.Windows.Forms.ColumnHeader
    Friend WithEvents �г�ID As System.Windows.Forms.ColumnHeader
    Friend WithEvents ʱ�� As System.Windows.Forms.ColumnHeader
    Friend WithEvents ������Ϣ As System.Windows.Forms.ColumnHeader
End Class
