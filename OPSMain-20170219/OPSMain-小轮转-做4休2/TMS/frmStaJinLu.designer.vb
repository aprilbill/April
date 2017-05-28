<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStaJinLu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
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
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer
        Me.SplitContainerSta = New System.Windows.Forms.SplitContainer
        Me.btnData = New System.Windows.Forms.Button
        Me.btnSaveSta = New System.Windows.Forms.Button
        Me.btnAllSta = New System.Windows.Forms.Button
        Me.cmbControlStyle = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnStart = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbStaName = New System.Windows.Forms.ComboBox
        Me.cmbReturnLine = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.picSta = New System.Windows.Forms.PictureBox
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.dataGrid = New System.Windows.Forms.DataGridView
        Me.Label4 = New System.Windows.Forms.Label
        Me.DataDiDui = New System.Windows.Forms.DataGridView
        Me.Label3 = New System.Windows.Forms.Label
        Me.ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.停站标尺 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.连接车站名 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.运行标尺 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.通过的轨道 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.开行数量 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.通过的控制模块 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        Me.SplitContainerSta.Panel1.SuspendLayout()
        Me.SplitContainerSta.Panel2.SuspendLayout()
        Me.SplitContainerSta.SuspendLayout()
        CType(Me.picSta, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataDiDui, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer4
        '
        Me.SplitContainer4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.SplitContainerSta)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.AutoScroll = True
        Me.SplitContainer4.Panel2.Controls.Add(Me.SplitContainer1)
        Me.SplitContainer4.Size = New System.Drawing.Size(761, 620)
        Me.SplitContainer4.SplitterDistance = 294
        Me.SplitContainer4.TabIndex = 9
        Me.SplitContainer4.Text = "SplitContainer4"
        '
        'SplitContainerSta
        '
        Me.SplitContainerSta.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainerSta.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerSta.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainerSta.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerSta.Name = "SplitContainerSta"
        '
        'SplitContainerSta.Panel1
        '
        Me.SplitContainerSta.Panel1.Controls.Add(Me.btnData)
        Me.SplitContainerSta.Panel1.Controls.Add(Me.btnSaveSta)
        Me.SplitContainerSta.Panel1.Controls.Add(Me.btnAllSta)
        Me.SplitContainerSta.Panel1.Controls.Add(Me.cmbControlStyle)
        Me.SplitContainerSta.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainerSta.Panel1.Controls.Add(Me.btnCancel)
        Me.SplitContainerSta.Panel1.Controls.Add(Me.btnStart)
        Me.SplitContainerSta.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainerSta.Panel1.Controls.Add(Me.cmbStaName)
        Me.SplitContainerSta.Panel1.Controls.Add(Me.cmbReturnLine)
        Me.SplitContainerSta.Panel1.Controls.Add(Me.Label1)
        '
        'SplitContainerSta.Panel2
        '
        Me.SplitContainerSta.Panel2.BackColor = System.Drawing.Color.Black
        Me.SplitContainerSta.Panel2.Controls.Add(Me.picSta)
        Me.SplitContainerSta.Size = New System.Drawing.Size(761, 294)
        Me.SplitContainerSta.SplitterDistance = 239
        Me.SplitContainerSta.TabIndex = 0
        Me.SplitContainerSta.Text = "SplitContainer5"
        '
        'btnData
        '
        Me.btnData.Location = New System.Drawing.Point(10, 226)
        Me.btnData.Name = "btnData"
        Me.btnData.Size = New System.Drawing.Size(211, 23)
        Me.btnData.TabIndex = 10
        Me.btnData.Text = "打开已存车站进路信息(&O)"
        '
        'btnSaveSta
        '
        Me.btnSaveSta.Location = New System.Drawing.Point(10, 255)
        Me.btnSaveSta.Name = "btnSaveSta"
        Me.btnSaveSta.Size = New System.Drawing.Size(112, 23)
        Me.btnSaveSta.TabIndex = 9
        Me.btnSaveSta.Text = "保存进路信息(&A)"
        '
        'btnAllSta
        '
        Me.btnAllSta.Location = New System.Drawing.Point(18, 96)
        Me.btnAllSta.Name = "btnAllSta"
        Me.btnAllSta.Size = New System.Drawing.Size(207, 23)
        Me.btnAllSta.TabIndex = 8
        Me.btnAllSta.Text = "搜索所有车站所有进路(&A)"
        '
        'cmbControlStyle
        '
        Me.cmbControlStyle.FormattingEnabled = True
        Me.cmbControlStyle.Location = New System.Drawing.Point(109, 136)
        Me.cmbControlStyle.Name = "cmbControlStyle"
        Me.cmbControlStyle.Size = New System.Drawing.Size(112, 20)
        Me.cmbControlStyle.TabIndex = 7
        Me.cmbControlStyle.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 139)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(95, 12)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "请选择控制方式:"
        Me.Label5.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(138, 255)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(83, 23)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "退出(&E)"
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(18, 54)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(207, 23)
        Me.btnStart.TabIndex = 4
        Me.btnStart.Text = "搜索车站所有进路(&S)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(83, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "请选择车站名:"
        '
        'cmbStaName
        '
        Me.cmbStaName.FormattingEnabled = True
        Me.cmbStaName.Location = New System.Drawing.Point(110, 11)
        Me.cmbStaName.Name = "cmbStaName"
        Me.cmbStaName.Size = New System.Drawing.Size(111, 20)
        Me.cmbStaName.TabIndex = 2
        '
        'cmbReturnLine
        '
        Me.cmbReturnLine.FormattingEnabled = True
        Me.cmbReturnLine.Location = New System.Drawing.Point(109, 165)
        Me.cmbReturnLine.Name = "cmbReturnLine"
        Me.cmbReturnLine.Size = New System.Drawing.Size(112, 20)
        Me.cmbReturnLine.TabIndex = 3
        Me.cmbReturnLine.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 165)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "请选择折返线:"
        Me.Label1.Visible = False
        '
        'picSta
        '
        Me.picSta.BackColor = System.Drawing.Color.Black
        Me.picSta.Location = New System.Drawing.Point(88, 30)
        Me.picSta.Name = "picSta"
        Me.picSta.Size = New System.Drawing.Size(330, 226)
        Me.picSta.TabIndex = 9
        Me.picSta.TabStop = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.dataGrid)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label4)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataDiDui)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer1.Size = New System.Drawing.Size(757, 318)
        Me.SplitContainer1.SplitterDistance = 403
        Me.SplitContainer1.TabIndex = 0
        '
        'dataGrid
        '
        Me.dataGrid.AllowUserToAddRows = False
        Me.dataGrid.AllowUserToDeleteRows = False
        Me.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dataGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ID, Me.DataGridViewTextBoxColumn1, Me.停站标尺, Me.连接车站名, Me.运行标尺, Me.通过的轨道, Me.开行数量, Me.通过的控制模块})
        Me.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dataGrid.Location = New System.Drawing.Point(0, 19)
        Me.dataGrid.Name = "dataGrid"
        Me.dataGrid.RowHeadersWidth = 20
        Me.dataGrid.RowTemplate.Height = 23
        Me.dataGrid.Size = New System.Drawing.Size(403, 299)
        Me.dataGrid.TabIndex = 78
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.DimGray
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label4.Location = New System.Drawing.Point(0, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(403, 19)
        Me.Label4.TabIndex = 77
        Me.Label4.Text = "所有进路"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DataDiDui
        '
        Me.DataDiDui.AllowUserToAddRows = False
        Me.DataDiDui.AllowUserToDeleteRows = False
        Me.DataDiDui.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.DataDiDui.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9})
        Me.DataDiDui.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataDiDui.Location = New System.Drawing.Point(0, 19)
        Me.DataDiDui.Name = "DataDiDui"
        Me.DataDiDui.RowHeadersWidth = 20
        Me.DataDiDui.RowTemplate.Height = 23
        Me.DataDiDui.Size = New System.Drawing.Size(350, 299)
        Me.DataDiDui.TabIndex = 79
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.DimGray
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label3.Location = New System.Drawing.Point(0, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(350, 19)
        Me.Label3.TabIndex = 76
        Me.Label3.Text = "敌对进路"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'ID
        '
        Me.ID.HeaderText = "ID"
        Me.ID.Name = "ID"
        Me.ID.Width = 40
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "车站"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 60
        '
        '停站标尺
        '
        Me.停站标尺.HeaderText = "进站方式"
        Me.停站标尺.Name = "停站标尺"
        Me.停站标尺.Width = 60
        '
        '连接车站名
        '
        Me.连接车站名.HeaderText = "连接车站名"
        Me.连接车站名.Name = "连接车站名"
        '
        '运行标尺
        '
        Me.运行标尺.HeaderText = "股道"
        Me.运行标尺.Name = "运行标尺"
        Me.运行标尺.Width = 40
        '
        '通过的轨道
        '
        Me.通过的轨道.HeaderText = "通过的轨道"
        Me.通过的轨道.Name = "通过的轨道"
        '
        '开行数量
        '
        Me.开行数量.HeaderText = "通过的道岔"
        Me.开行数量.Name = "开行数量"
        '
        '通过的控制模块
        '
        Me.通过的控制模块.HeaderText = "通过的控制模块"
        Me.通过的控制模块.Name = "通过的控制模块"
        Me.通过的控制模块.Width = 5
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Width = 40
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.HeaderText = "车站"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 60
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.HeaderText = "进站方式"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 60
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.HeaderText = "连接车站名"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.HeaderText = "股道"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Width = 40
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.HeaderText = "通过的轨道"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.HeaderText = "通过的道岔"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.Width = 200
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.HeaderText = "通过的控制模块"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.Width = 5
        '
        'frmStaJinLu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(761, 620)
        Me.Controls.Add(Me.SplitContainer4)
        Me.Name = "frmStaJinLu"
        Me.Text = "车站进路搜索"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        Me.SplitContainerSta.Panel1.ResumeLayout(False)
        Me.SplitContainerSta.Panel1.PerformLayout()
        Me.SplitContainerSta.Panel2.ResumeLayout(False)
        Me.SplitContainerSta.ResumeLayout(False)
        CType(Me.picSta, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataDiDui, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainerSta As System.Windows.Forms.SplitContainer
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbStaName As System.Windows.Forms.ComboBox
    Friend WithEvents cmbReturnLine As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents picSta As System.Windows.Forms.PictureBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dataGrid As System.Windows.Forms.DataGridView
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DataDiDui As System.Windows.Forms.DataGridView
    Friend WithEvents cmbControlStyle As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnAllSta As System.Windows.Forms.Button
    Friend WithEvents btnSaveSta As System.Windows.Forms.Button
    Friend WithEvents btnData As System.Windows.Forms.Button
    Friend WithEvents ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 停站标尺 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 连接车站名 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 运行标尺 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 通过的轨道 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 开行数量 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 通过的控制模块 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
