<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class frmEditDiagramByGroup
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
        Me.lstChaoZuo = New System.Windows.Forms.ListBox
        Me.grpDeleteTrain = New System.Windows.Forms.GroupBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnBeDelete = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cmbTrainJiaoLu = New System.Windows.Forms.ComboBox
        Me.交路名称 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtEndTime = New System.Windows.Forms.TextBox
        Me.txtFirTime = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.grpDeleteTrain.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "请选择操作："
        '
        'lstChaoZuo
        '
        Me.lstChaoZuo.FormattingEnabled = True
        Me.lstChaoZuo.ItemHeight = 12
        Me.lstChaoZuo.Location = New System.Drawing.Point(13, 34)
        Me.lstChaoZuo.Name = "lstChaoZuo"
        Me.lstChaoZuo.Size = New System.Drawing.Size(144, 268)
        Me.lstChaoZuo.TabIndex = 1
        '
        'grpDeleteTrain
        '
        Me.grpDeleteTrain.Controls.Add(Me.Label6)
        Me.grpDeleteTrain.Controls.Add(Me.btnBeDelete)
        Me.grpDeleteTrain.Controls.Add(Me.GroupBox1)
        Me.grpDeleteTrain.Location = New System.Drawing.Point(172, 34)
        Me.grpDeleteTrain.Name = "grpDeleteTrain"
        Me.grpDeleteTrain.Size = New System.Drawing.Size(339, 268)
        Me.grpDeleteTrain.TabIndex = 2
        Me.grpDeleteTrain.TabStop = False
        Me.grpDeleteTrain.Text = "条件设置"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(21, 224)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 12)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "注:以列车发点为准"
        '
        'btnBeDelete
        '
        Me.btnBeDelete.Location = New System.Drawing.Point(224, 180)
        Me.btnBeDelete.Name = "btnBeDelete"
        Me.btnBeDelete.Size = New System.Drawing.Size(90, 23)
        Me.btnBeDelete.TabIndex = 3
        Me.btnBeDelete.Text = "开始应用(&Y)"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbTrainJiaoLu)
        Me.GroupBox1.Controls.Add(Me.交路名称)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtEndTime)
        Me.GroupBox1.Controls.Add(Me.txtFirTime)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(23, 30)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(291, 132)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "时间段设置"
        '
        'cmbTrainJiaoLu
        '
        Me.cmbTrainJiaoLu.FormattingEnabled = True
        Me.cmbTrainJiaoLu.Location = New System.Drawing.Point(108, 93)
        Me.cmbTrainJiaoLu.Name = "cmbTrainJiaoLu"
        Me.cmbTrainJiaoLu.Size = New System.Drawing.Size(158, 20)
        Me.cmbTrainJiaoLu.TabIndex = 9
        '
        '交路名称
        '
        Me.交路名称.AutoSize = True
        Me.交路名称.Location = New System.Drawing.Point(12, 96)
        Me.交路名称.Name = "交路名称"
        Me.交路名称.Size = New System.Drawing.Size(59, 12)
        Me.交路名称.TabIndex = 8
        Me.交路名称.Text = "交路名称:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(213, 60)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 12)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "时.分.秒"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(213, 28)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "时.分.秒"
        '
        'txtEndTime
        '
        Me.txtEndTime.Location = New System.Drawing.Point(108, 55)
        Me.txtEndTime.Name = "txtEndTime"
        Me.txtEndTime.Size = New System.Drawing.Size(89, 21)
        Me.txtEndTime.TabIndex = 3
        Me.txtEndTime.Text = "23.00.00"
        '
        'txtFirTime
        '
        Me.txtFirTime.Location = New System.Drawing.Point(108, 20)
        Me.txtFirTime.Name = "txtFirTime"
        Me.txtFirTime.Size = New System.Drawing.Size(89, 21)
        Me.txtFirTime.TabIndex = 2
        Me.txtFirTime.Text = "5.00.00"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 58)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 12)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "终止时间:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 12)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "起始时间:"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(421, 311)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(90, 23)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "退出(&E)"
        '
        'frmEditDiagramByGroup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(526, 346)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.grpDeleteTrain)
        Me.Controls.Add(Me.lstChaoZuo)
        Me.Controls.Add(Me.Label1)
        Me.MaximizeBox = False
        Me.Name = "frmEditDiagramByGroup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "批处理组操作"
        Me.grpDeleteTrain.ResumeLayout(False)
        Me.grpDeleteTrain.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lstChaoZuo As System.Windows.Forms.ListBox
    Friend WithEvents grpDeleteTrain As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtFirTime As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtEndTime As System.Windows.Forms.TextBox
    Friend WithEvents btnBeDelete As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbTrainJiaoLu As System.Windows.Forms.ComboBox
    Friend WithEvents 交路名称 As System.Windows.Forms.Label
End Class
