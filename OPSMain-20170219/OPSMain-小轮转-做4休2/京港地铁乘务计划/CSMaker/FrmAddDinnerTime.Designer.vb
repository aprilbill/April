<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAddDinnerTime
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtStartTime = New System.Windows.Forms.DateTimePicker()
        Me.DTEndtime = New System.Windows.Forms.DateTimePicker()
        Me.DTDinnerTime = New System.Windows.Forms.DateTimePicker()
        Me.cmbDutysort = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnok = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.ComboBox3 = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ComboBox4 = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ComboBox5 = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(19, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "用餐班种"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "起始时间"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(222, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "终止时间"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(222, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 12)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "用餐时间"
        '
        'dtStartTime
        '
        Me.dtStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtStartTime.Location = New System.Drawing.Point(83, 54)
        Me.dtStartTime.Name = "dtStartTime"
        Me.dtStartTime.ShowUpDown = True
        Me.dtStartTime.Size = New System.Drawing.Size(122, 21)
        Me.dtStartTime.TabIndex = 4
        '
        'DTEndtime
        '
        Me.DTEndtime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DTEndtime.Location = New System.Drawing.Point(286, 54)
        Me.DTEndtime.Name = "DTEndtime"
        Me.DTEndtime.ShowUpDown = True
        Me.DTEndtime.Size = New System.Drawing.Size(122, 21)
        Me.DTEndtime.TabIndex = 5
        '
        'DTDinnerTime
        '
        Me.DTDinnerTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.DTDinnerTime.Location = New System.Drawing.Point(286, 19)
        Me.DTDinnerTime.Name = "DTDinnerTime"
        Me.DTDinnerTime.ShowUpDown = True
        Me.DTDinnerTime.Size = New System.Drawing.Size(122, 21)
        Me.DTDinnerTime.TabIndex = 6
        '
        'cmbDutysort
        '
        Me.cmbDutysort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDutysort.FormattingEnabled = True
        Me.cmbDutysort.Items.AddRange(New Object() {"早班", "白班", "日勤班", "夜班"})
        Me.cmbDutysort.Location = New System.Drawing.Point(83, 22)
        Me.cmbDutysort.Name = "cmbDutysort"
        Me.cmbDutysort.Size = New System.Drawing.Size(122, 20)
        Me.cmbDutysort.TabIndex = 7
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(333, 204)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "取消"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnok
        '
        Me.btnok.Location = New System.Drawing.Point(235, 204)
        Me.btnok.Name = "btnok"
        Me.btnok.Size = New System.Drawing.Size(75, 23)
        Me.btnok.TabIndex = 8
        Me.btnok.Text = "确定"
        Me.btnok.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(42, 96)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 12)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "交路"
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(83, 92)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(203, 20)
        Me.ComboBox1.TabIndex = 10
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(209, 128)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "用餐车站"
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(268, 124)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(121, 20)
        Me.ComboBox2.TabIndex = 12
        '
        'ComboBox3
        '
        Me.ComboBox3.FormattingEnabled = True
        Me.ComboBox3.Items.AddRange(New Object() {"上行", "下行", "双向"})
        Me.ComboBox3.Location = New System.Drawing.Point(95, 161)
        Me.ComboBox3.Name = "ComboBox3"
        Me.ComboBox3.Size = New System.Drawing.Size(103, 20)
        Me.ComboBox3.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 165)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 12)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "下车用餐方向"
        '
        'ComboBox4
        '
        Me.ComboBox4.FormattingEnabled = True
        Me.ComboBox4.Items.AddRange(New Object() {"上行", "下行", "双向"})
        Me.ComboBox4.Location = New System.Drawing.Point(286, 162)
        Me.ComboBox4.Name = "ComboBox4"
        Me.ComboBox4.Size = New System.Drawing.Size(103, 20)
        Me.ComboBox4.TabIndex = 16
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(209, 166)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 12)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "餐后接车方向"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(292, 91)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 17
        Me.Button2.Text = "筛选交路"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'ComboBox5
        '
        Me.ComboBox5.FormattingEnabled = True
        Me.ComboBox5.Items.AddRange(New Object() {"午餐", "晚餐"})
        Me.ComboBox5.Location = New System.Drawing.Point(83, 124)
        Me.ComboBox5.Name = "ComboBox5"
        Me.ComboBox5.Size = New System.Drawing.Size(103, 20)
        Me.ComboBox5.TabIndex = 19
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(43, 128)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(29, 12)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "餐种"
        '
        'FrmAddDinnerTime
        '
        Me.AcceptButton = Me.btnok
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(428, 239)
        Me.Controls.Add(Me.ComboBox5)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ComboBox4)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.ComboBox3)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.ComboBox2)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnok)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmbDutysort)
        Me.Controls.Add(Me.DTDinnerTime)
        Me.Controls.Add(Me.DTEndtime)
        Me.Controls.Add(Me.dtStartTime)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAddDinnerTime"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "添加用餐参数"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtStartTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTEndtime As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTDinnerTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbDutysort As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnok As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ComboBox5 As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
