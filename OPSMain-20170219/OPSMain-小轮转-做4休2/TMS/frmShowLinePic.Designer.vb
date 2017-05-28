<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShowLinePic
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
        Me.Pic = New System.Windows.Forms.PictureBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnSavePic = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.btnPrintSet = New System.Windows.Forms.Button
        Me.btnPrintView = New System.Windows.Forms.Button
        Me.btnSmall = New System.Windows.Forms.Button
        Me.btnBig = New System.Windows.Forms.Button
        Me.printDocDiagram = New System.Drawing.Printing.PrintDocument
        CType(Me.Pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pic
        '
        Me.Pic.BackColor = System.Drawing.Color.White
        Me.Pic.Location = New System.Drawing.Point(61, 33)
        Me.Pic.Name = "Pic"
        Me.Pic.Size = New System.Drawing.Size(549, 294)
        Me.Pic.TabIndex = 0
        Me.Pic.TabStop = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(371, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 21)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "打印(&P)"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnSavePic
        '
        Me.btnSavePic.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSavePic.Location = New System.Drawing.Point(460, 12)
        Me.btnSavePic.Name = "btnSavePic"
        Me.btnSavePic.Size = New System.Drawing.Size(92, 21)
        Me.btnSavePic.TabIndex = 2
        Me.btnSavePic.Text = "保存图片(&S)"
        Me.btnSavePic.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.Location = New System.Drawing.Point(577, 12)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 21)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "退出(&E)"
        Me.Button3.UseVisualStyleBackColor = True
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
        Me.SplitContainer1.Panel1.AutoScroll = True
        Me.SplitContainer1.Panel1.BackColor = System.Drawing.Color.White
        Me.SplitContainer1.Panel1.Controls.Add(Me.Pic)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrintSet)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnPrintView)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSmall)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnBig)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSavePic)
        Me.SplitContainer1.Size = New System.Drawing.Size(661, 413)
        Me.SplitContainer1.SplitterDistance = 363
        Me.SplitContainer1.TabIndex = 4
        '
        'btnPrintSet
        '
        Me.btnPrintSet.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrintSet.Location = New System.Drawing.Point(173, 12)
        Me.btnPrintSet.Name = "btnPrintSet"
        Me.btnPrintSet.Size = New System.Drawing.Size(93, 21)
        Me.btnPrintSet.TabIndex = 7
        Me.btnPrintSet.Text = "打印设置(&T)"
        Me.btnPrintSet.UseVisualStyleBackColor = True
        '
        'btnPrintView
        '
        Me.btnPrintView.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrintView.Location = New System.Drawing.Point(272, 12)
        Me.btnPrintView.Name = "btnPrintView"
        Me.btnPrintView.Size = New System.Drawing.Size(93, 21)
        Me.btnPrintView.TabIndex = 6
        Me.btnPrintView.Text = "打印预览(&V)"
        Me.btnPrintView.UseVisualStyleBackColor = True
        '
        'btnSmall
        '
        Me.btnSmall.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSmall.Location = New System.Drawing.Point(88, 12)
        Me.btnSmall.Name = "btnSmall"
        Me.btnSmall.Size = New System.Drawing.Size(75, 21)
        Me.btnSmall.TabIndex = 5
        Me.btnSmall.Text = "缩小(&S)"
        Me.btnSmall.UseVisualStyleBackColor = True
        '
        'btnBig
        '
        Me.btnBig.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBig.Location = New System.Drawing.Point(7, 12)
        Me.btnBig.Name = "btnBig"
        Me.btnBig.Size = New System.Drawing.Size(75, 21)
        Me.btnBig.TabIndex = 4
        Me.btnBig.Text = "放大(&B)"
        Me.btnBig.UseVisualStyleBackColor = True
        '
        'printDocDiagram
        '
        '
        'frmShowLinePic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(661, 413)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "frmShowLinePic"
        Me.Text = "显示车站"
        CType(Me.Pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Pic As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnSavePic As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSmall As System.Windows.Forms.Button
    Friend WithEvents btnBig As System.Windows.Forms.Button
    Friend WithEvents printDocDiagram As System.Drawing.Printing.PrintDocument
    Friend WithEvents btnPrintView As System.Windows.Forms.Button
    Friend WithEvents btnPrintSet As System.Windows.Forms.Button
End Class
