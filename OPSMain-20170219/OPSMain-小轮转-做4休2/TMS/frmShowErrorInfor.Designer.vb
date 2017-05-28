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
        Me.序号 = New System.Windows.Forms.ColumnHeader
        Me.车次 = New System.Windows.Forms.ColumnHeader
        Me.列车ID = New System.Windows.Forms.ColumnHeader
        Me.时刻 = New System.Windows.Forms.ColumnHeader
        Me.出错信息 = New System.Windows.Forms.ColumnHeader
        Me.SuspendLayout()
        '
        'listViewTrain
        '
        Me.listViewTrain.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.listViewTrain.AllowColumnReorder = True
        Me.listViewTrain.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.序号, Me.车次, Me.列车ID, Me.时刻, Me.出错信息})
        Me.listViewTrain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listViewTrain.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
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
        '序号
        '
        Me.序号.Text = "序号"
        Me.序号.Width = 40
        '
        '车次
        '
        Me.车次.Text = "车次"
        Me.车次.Width = 57
        '
        '列车ID
        '
        Me.列车ID.Text = "列车ID"
        '
        '时刻
        '
        Me.时刻.Text = "时刻"
        '
        '出错信息
        '
        Me.出错信息.Text = "出错信息"
        Me.出错信息.Width = 300
        '
        'frmShowErrorInfor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(525, 168)
        Me.Controls.Add(Me.listViewTrain)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "frmShowErrorInfor"
        Me.Text = "出错信息"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents listViewTrain As System.Windows.Forms.ListView
    Friend WithEvents 序号 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 车次 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 列车ID As System.Windows.Forms.ColumnHeader
    Friend WithEvents 时刻 As System.Windows.Forms.ColumnHeader
    Friend WithEvents 出错信息 As System.Windows.Forms.ColumnHeader
End Class
