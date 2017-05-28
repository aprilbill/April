<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmrenshucesuan
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

    '注意:  以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.时段 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.车底数 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.折返司机数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.配检司机数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.机动司机数量 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.合计 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtCesuan = New System.Windows.Forms.TextBox()
        Me.TxtZaofeng = New System.Windows.Forms.TextBox()
        Me.TxtWanfeng = New System.Windows.Forms.TextBox()
        Me.TxtPingfeng = New System.Windows.Forms.TextBox()
        Me.Txtchazhi = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtZongshu = New System.Windows.Forms.TextBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.时段, Me.车底数, Me.折返司机数量, Me.配检司机数量, Me.机动司机数量, Me.合计})
        Me.DataGridView1.Location = New System.Drawing.Point(4, 12)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(638, 143)
        Me.DataGridView1.TabIndex = 0
        '
        '时段
        '
        Me.时段.HeaderText = "时段"
        Me.时段.Name = "时段"
        '
        '车底数
        '
        Me.车底数.HeaderText = "车底数"
        Me.车底数.Name = "车底数"
        '
        '折返司机数量
        '
        Me.折返司机数量.HeaderText = "折返司机数量"
        Me.折返司机数量.Name = "折返司机数量"
        '
        '配检司机数量
        '
        Me.配检司机数量.HeaderText = "配检司机数量"
        Me.配检司机数量.Name = "配检司机数量"
        '
        '机动司机数量
        '
        Me.机动司机数量.HeaderText = "机动司机数量"
        Me.机动司机数量.Name = "机动司机数量"
        '
        '合计
        '
        Me.合计.HeaderText = "合计"
        Me.合计.Name = "合计"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(68, 177)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "测算人数："
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(275, 177)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "早峰人数："
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(275, 254)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "晚峰人数："
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(275, 213)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 12)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "平峰人数："
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(44, 215)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(89, 12)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "峰班差值人数："
        '
        'TxtCesuan
        '
        Me.TxtCesuan.Location = New System.Drawing.Point(139, 174)
        Me.TxtCesuan.Name = "TxtCesuan"
        Me.TxtCesuan.Size = New System.Drawing.Size(100, 21)
        Me.TxtCesuan.TabIndex = 6
        '
        'TxtZaofeng
        '
        Me.TxtZaofeng.Location = New System.Drawing.Point(346, 174)
        Me.TxtZaofeng.Name = "TxtZaofeng"
        Me.TxtZaofeng.Size = New System.Drawing.Size(100, 21)
        Me.TxtZaofeng.TabIndex = 7
        '
        'TxtWanfeng
        '
        Me.TxtWanfeng.Location = New System.Drawing.Point(346, 251)
        Me.TxtWanfeng.Name = "TxtWanfeng"
        Me.TxtWanfeng.Size = New System.Drawing.Size(100, 21)
        Me.TxtWanfeng.TabIndex = 8
        '
        'TxtPingfeng
        '
        Me.TxtPingfeng.Location = New System.Drawing.Point(346, 210)
        Me.TxtPingfeng.Name = "TxtPingfeng"
        Me.TxtPingfeng.Size = New System.Drawing.Size(100, 21)
        Me.TxtPingfeng.TabIndex = 9
        '
        'Txtchazhi
        '
        Me.Txtchazhi.Location = New System.Drawing.Point(139, 210)
        Me.Txtchazhi.Name = "Txtchazhi"
        Me.Txtchazhi.Size = New System.Drawing.Size(100, 21)
        Me.Txtchazhi.TabIndex = 10
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(528, 249)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "计算"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(80, 251)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 12
        Me.Label6.Text = "总人数："
        '
        'TxtZongshu
        '
        Me.TxtZongshu.Location = New System.Drawing.Point(139, 248)
        Me.TxtZongshu.Name = "TxtZongshu"
        Me.TxtZongshu.Size = New System.Drawing.Size(100, 21)
        Me.TxtZongshu.TabIndex = 13
        '
        'Frmrenshucesuan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(650, 296)
        Me.Controls.Add(Me.TxtZongshu)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Txtchazhi)
        Me.Controls.Add(Me.TxtPingfeng)
        Me.Controls.Add(Me.TxtWanfeng)
        Me.Controls.Add(Me.TxtZaofeng)
        Me.Controls.Add(Me.TxtCesuan)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.DataGridView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "Frmrenshucesuan"
        Me.Text = "人数测算"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents 时段 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 车底数 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 折返司机数量 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 配检司机数量 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 机动司机数量 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents 合计 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TxtCesuan As System.Windows.Forms.TextBox
    Friend WithEvents TxtZaofeng As System.Windows.Forms.TextBox
    Friend WithEvents TxtWanfeng As System.Windows.Forms.TextBox
    Friend WithEvents TxtPingfeng As System.Windows.Forms.TextBox
    Friend WithEvents Txtchazhi As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TxtZongshu As System.Windows.Forms.TextBox
End Class
