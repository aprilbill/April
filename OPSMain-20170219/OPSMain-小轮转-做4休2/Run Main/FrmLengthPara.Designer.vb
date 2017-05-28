<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLengthPara
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLengthPara))
        Me.StartPosLB = New System.Windows.Forms.ListBox()
        Me.EndPosLB = New System.Windows.Forms.ListBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Addbtn = New System.Windows.Forms.Button()
        Me.DeleteBtn = New System.Windows.Forms.Button()
        Me.SaveBtn = New System.Windows.Forms.Button()
        Me.Exitbtn = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Uptrainlist = New System.Windows.Forms.DataGridView()
        Me.交路名称 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.距离 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Downtrainlist = New System.Windows.Forms.DataGridView()
        Me.q = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.sad = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AddOpbtn = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Btn_AddStation = New System.Windows.Forms.Button()
        Me.TxtStation = New System.Windows.Forms.TextBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.Uptrainlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        CType(Me.Downtrainlist, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StartPosLB
        '
        Me.StartPosLB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StartPosLB.FormattingEnabled = True
        Me.StartPosLB.ItemHeight = 12
        Me.StartPosLB.Location = New System.Drawing.Point(3, 17)
        Me.StartPosLB.Name = "StartPosLB"
        Me.StartPosLB.Size = New System.Drawing.Size(159, 149)
        Me.StartPosLB.TabIndex = 0
        '
        'EndPosLB
        '
        Me.EndPosLB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EndPosLB.FormattingEnabled = True
        Me.EndPosLB.ItemHeight = 12
        Me.EndPosLB.Location = New System.Drawing.Point(3, 17)
        Me.EndPosLB.Name = "EndPosLB"
        Me.EndPosLB.Size = New System.Drawing.Size(157, 151)
        Me.EndPosLB.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.StartPosLB)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 47)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(165, 169)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "起始站点"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.EndPosLB)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 222)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(163, 171)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "到达站点"
        '
        'Addbtn
        '
        Me.Addbtn.Image = CType(resources.GetObject("Addbtn.Image"), System.Drawing.Image)
        Me.Addbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Addbtn.Location = New System.Drawing.Point(178, 121)
        Me.Addbtn.Name = "Addbtn"
        Me.Addbtn.Size = New System.Drawing.Size(81, 42)
        Me.Addbtn.TabIndex = 4
        Me.Addbtn.Text = "添加"
        Me.Addbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Addbtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.Addbtn.UseVisualStyleBackColor = True
        '
        'DeleteBtn
        '
        Me.DeleteBtn.Image = CType(resources.GetObject("DeleteBtn.Image"), System.Drawing.Image)
        Me.DeleteBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DeleteBtn.Location = New System.Drawing.Point(178, 265)
        Me.DeleteBtn.Name = "DeleteBtn"
        Me.DeleteBtn.Size = New System.Drawing.Size(81, 42)
        Me.DeleteBtn.TabIndex = 4
        Me.DeleteBtn.Text = "删除"
        Me.DeleteBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeleteBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.DeleteBtn.UseVisualStyleBackColor = True
        '
        'SaveBtn
        '
        Me.SaveBtn.Image = CType(resources.GetObject("SaveBtn.Image"), System.Drawing.Image)
        Me.SaveBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SaveBtn.Location = New System.Drawing.Point(442, 399)
        Me.SaveBtn.Name = "SaveBtn"
        Me.SaveBtn.Size = New System.Drawing.Size(81, 40)
        Me.SaveBtn.TabIndex = 4
        Me.SaveBtn.Text = "保存"
        Me.SaveBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SaveBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.SaveBtn.UseVisualStyleBackColor = True
        '
        'Exitbtn
        '
        Me.Exitbtn.DialogResult = System.Windows.Forms.DialogResult.No
        Me.Exitbtn.Image = CType(resources.GetObject("Exitbtn.Image"), System.Drawing.Image)
        Me.Exitbtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Exitbtn.Location = New System.Drawing.Point(529, 399)
        Me.Exitbtn.Name = "Exitbtn"
        Me.Exitbtn.Size = New System.Drawing.Size(81, 40)
        Me.Exitbtn.TabIndex = 4
        Me.Exitbtn.Text = "退出"
        Me.Exitbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Exitbtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.Exitbtn.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Uptrainlist)
        Me.GroupBox3.Location = New System.Drawing.Point(270, 47)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(346, 169)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "上行区段"
        '
        'Uptrainlist
        '
        Me.Uptrainlist.AllowUserToAddRows = False
        Me.Uptrainlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Uptrainlist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.交路名称, Me.距离})
        Me.Uptrainlist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Uptrainlist.Location = New System.Drawing.Point(3, 17)
        Me.Uptrainlist.Name = "Uptrainlist"
        Me.Uptrainlist.RowTemplate.Height = 23
        Me.Uptrainlist.Size = New System.Drawing.Size(340, 149)
        Me.Uptrainlist.TabIndex = 0
        '
        '交路名称
        '
        Me.交路名称.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.交路名称.HeaderText = "交路名称"
        Me.交路名称.Name = "交路名称"
        Me.交路名称.Width = 78
        '
        '距离
        '
        Me.距离.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.距离.HeaderText = "距离"
        Me.距离.Name = "距离"
        Me.距离.Width = 54
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Downtrainlist)
        Me.GroupBox4.Location = New System.Drawing.Point(270, 222)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(343, 171)
        Me.GroupBox4.TabIndex = 5
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "下行区段"
        '
        'Downtrainlist
        '
        Me.Downtrainlist.AllowUserToAddRows = False
        Me.Downtrainlist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Downtrainlist.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.q, Me.sad})
        Me.Downtrainlist.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Downtrainlist.Location = New System.Drawing.Point(3, 17)
        Me.Downtrainlist.Name = "Downtrainlist"
        Me.Downtrainlist.RowTemplate.Height = 23
        Me.Downtrainlist.Size = New System.Drawing.Size(337, 151)
        Me.Downtrainlist.TabIndex = 0
        '
        'q
        '
        Me.q.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.q.HeaderText = "交路名称"
        Me.q.Name = "q"
        Me.q.Width = 78
        '
        'sad
        '
        Me.sad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.sad.HeaderText = "距离"
        Me.sad.Name = "sad"
        Me.sad.Width = 54
        '
        'AddOpbtn
        '
        Me.AddOpbtn.Image = CType(resources.GetObject("AddOpbtn.Image"), System.Drawing.Image)
        Me.AddOpbtn.Location = New System.Drawing.Point(178, 196)
        Me.AddOpbtn.Name = "AddOpbtn"
        Me.AddOpbtn.Size = New System.Drawing.Size(81, 42)
        Me.AddOpbtn.TabIndex = 4
        Me.AddOpbtn.Text = "反向  添加"
        Me.AddOpbtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AddOpbtn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.AddOpbtn.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(29, 12)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "线路"
        '
        'Btn_AddStation
        '
        Me.Btn_AddStation.Location = New System.Drawing.Point(155, 409)
        Me.Btn_AddStation.Name = "Btn_AddStation"
        Me.Btn_AddStation.Size = New System.Drawing.Size(75, 24)
        Me.Btn_AddStation.TabIndex = 8
        Me.Btn_AddStation.Text = "添加车站"
        Me.Btn_AddStation.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage
        Me.Btn_AddStation.UseVisualStyleBackColor = True
        '
        'TxtStation
        '
        Me.TxtStation.Location = New System.Drawing.Point(10, 410)
        Me.TxtStation.Name = "TxtStation"
        Me.TxtStation.Size = New System.Drawing.Size(136, 21)
        Me.TxtStation.TabIndex = 9
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.Enabled = False
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(44, 11)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(128, 20)
        Me.ComboBox1.TabIndex = 6
        '
        'FrmLengthPara
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(628, 449)
        Me.Controls.Add(Me.TxtStation)
        Me.Controls.Add(Me.Btn_AddStation)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Exitbtn)
        Me.Controls.Add(Me.SaveBtn)
        Me.Controls.Add(Me.DeleteBtn)
        Me.Controls.Add(Me.AddOpbtn)
        Me.Controls.Add(Me.Addbtn)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FrmLengthPara"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "交路距离参数设置"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.Uptrainlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        CType(Me.Downtrainlist, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StartPosLB As System.Windows.Forms.ListBox
    Friend WithEvents EndPosLB As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Addbtn As System.Windows.Forms.Button
    Friend WithEvents DeleteBtn As System.Windows.Forms.Button
    Friend WithEvents SaveBtn As System.Windows.Forms.Button
    Friend WithEvents Exitbtn As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents AddOpbtn As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Uptrainlist As System.Windows.Forms.DataGridView
    Friend WithEvents Downtrainlist As System.Windows.Forms.DataGridView
    Friend WithEvents Btn_AddStation As System.Windows.Forms.Button
    Friend WithEvents TxtStation As System.Windows.Forms.TextBox
    Friend WithEvents 交路名称 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 距离 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents q As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents sad As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
End Class
