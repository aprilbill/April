<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class newTestUpdate
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
        Me.UpdateButton = New System.Windows.Forms.Button()
        Me.CloButton = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.CBEZONE = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CmbApprentice = New System.Windows.Forms.ComboBox()
        Me.TxtMArelation = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.CmbStarLevel = New System.Windows.Forms.ComboBox()
        Me.TxtKM = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.CmbAvailableOrNot = New System.Windows.Forms.ComboBox()
        Me.TxtPhone = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.CmbTechGrade = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.CmbReasonforAvail = New System.Windows.Forms.ComboBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.CmbPost = New System.Windows.Forms.ComboBox()
        Me.CmbClass = New System.Windows.Forms.ComboBox()
        Me.TxtBeline = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtTeamno = New System.Windows.Forms.TextBox()
        Me.DriverNameTextBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DriverIDTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox8.SuspendLayout()
        Me.SuspendLayout()
        '
        'UpdateButton
        '
        Me.UpdateButton.Location = New System.Drawing.Point(290, 321)
        Me.UpdateButton.Name = "UpdateButton"
        Me.UpdateButton.Size = New System.Drawing.Size(64, 20)
        Me.UpdateButton.TabIndex = 11
        Me.UpdateButton.Text = "修改"
        Me.UpdateButton.UseVisualStyleBackColor = True
        '
        'CloButton
        '
        Me.CloButton.Location = New System.Drawing.Point(403, 318)
        Me.CloButton.Name = "CloButton"
        Me.CloButton.Size = New System.Drawing.Size(67, 20)
        Me.CloButton.TabIndex = 11
        Me.CloButton.Text = "关闭"
        Me.CloButton.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.ForeColor = System.Drawing.Color.Red
        Me.Label18.Location = New System.Drawing.Point(10, 329)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(101, 12)
        Me.Label18.TabIndex = 157
        Me.Label18.Text = "注:(*)为必填部分"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox8.Controls.Add(Me.Label26)
        Me.GroupBox8.Controls.Add(Me.Label27)
        Me.GroupBox8.Controls.Add(Me.Label25)
        Me.GroupBox8.Controls.Add(Me.CBEZONE)
        Me.GroupBox8.Controls.Add(Me.Label15)
        Me.GroupBox8.Controls.Add(Me.Label16)
        Me.GroupBox8.Controls.Add(Me.Label13)
        Me.GroupBox8.Controls.Add(Me.Label11)
        Me.GroupBox8.Controls.Add(Me.Label10)
        Me.GroupBox8.Controls.Add(Me.Label9)
        Me.GroupBox8.Controls.Add(Me.Label17)
        Me.GroupBox8.Controls.Add(Me.Label8)
        Me.GroupBox8.Controls.Add(Me.CmbApprentice)
        Me.GroupBox8.Controls.Add(Me.TxtMArelation)
        Me.GroupBox8.Controls.Add(Me.Label7)
        Me.GroupBox8.Controls.Add(Me.Label4)
        Me.GroupBox8.Controls.Add(Me.CmbStarLevel)
        Me.GroupBox8.Controls.Add(Me.TxtKM)
        Me.GroupBox8.Controls.Add(Me.Label20)
        Me.GroupBox8.Controls.Add(Me.Label19)
        Me.GroupBox8.Controls.Add(Me.CmbAvailableOrNot)
        Me.GroupBox8.Controls.Add(Me.TxtPhone)
        Me.GroupBox8.Controls.Add(Me.Label21)
        Me.GroupBox8.Controls.Add(Me.Label22)
        Me.GroupBox8.Controls.Add(Me.CmbTechGrade)
        Me.GroupBox8.Controls.Add(Me.Label23)
        Me.GroupBox8.Controls.Add(Me.CmbReasonforAvail)
        Me.GroupBox8.Controls.Add(Me.Label24)
        Me.GroupBox8.Controls.Add(Me.CmbPost)
        Me.GroupBox8.Controls.Add(Me.CmbClass)
        Me.GroupBox8.Controls.Add(Me.TxtBeline)
        Me.GroupBox8.Controls.Add(Me.Label14)
        Me.GroupBox8.Controls.Add(Me.Label5)
        Me.GroupBox8.Controls.Add(Me.TxtTeamno)
        Me.GroupBox8.Controls.Add(Me.DriverNameTextBox)
        Me.GroupBox8.Controls.Add(Me.Label2)
        Me.GroupBox8.Controls.Add(Me.DriverIDTextBox)
        Me.GroupBox8.Controls.Add(Me.Label1)
        Me.GroupBox8.Controls.Add(Me.Label12)
        Me.GroupBox8.Controls.Add(Me.Label3)
        Me.GroupBox8.Controls.Add(Me.Label6)
        Me.GroupBox8.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(458, 297)
        Me.GroupBox8.TabIndex = 158
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "人事档案"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(293, 221)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(118, 21)
        Me.DateTimePicker1.TabIndex = 235
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.ForeColor = System.Drawing.Color.Red
        Me.Label26.Location = New System.Drawing.Point(419, 222)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(23, 12)
        Me.Label26.TabIndex = 234
        Me.Label26.Text = "(*)"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label27.Location = New System.Drawing.Point(235, 225)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(53, 12)
        Me.Label27.TabIndex = 232
        Me.Label27.Text = "开始统计"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.ForeColor = System.Drawing.Color.Red
        Me.Label25.Location = New System.Drawing.Point(204, 157)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(23, 12)
        Me.Label25.TabIndex = 231
        Me.Label25.Text = "(*)"
        '
        'CBEZONE
        '
        Me.CBEZONE.AutoCompleteCustomSource.AddRange(New String() {"是", "否"})
        Me.CBEZONE.FormattingEnabled = True
        Me.CBEZONE.Location = New System.Drawing.Point(77, 152)
        Me.CBEZONE.Name = "CBEZONE"
        Me.CBEZONE.Size = New System.Drawing.Size(121, 20)
        Me.CBEZONE.TabIndex = 230
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label15.Location = New System.Drawing.Point(32, 157)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(29, 12)
        Me.Label15.TabIndex = 229
        Me.Label15.Text = "区域"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label16.Location = New System.Drawing.Point(250, 90)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(29, 12)
        Me.Label16.TabIndex = 228
        Me.Label16.Text = "组号"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.Color.Red
        Me.Label13.Location = New System.Drawing.Point(203, 59)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(23, 12)
        Me.Label13.TabIndex = 188
        Me.Label13.Text = "(*)"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.Red
        Me.Label11.Location = New System.Drawing.Point(202, 94)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(23, 12)
        Me.Label11.TabIndex = 187
        Me.Label11.Text = "(*)"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(204, 221)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(23, 12)
        Me.Label10.TabIndex = 186
        Me.Label10.Text = "(*)"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Red
        Me.Label9.Location = New System.Drawing.Point(203, 265)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(23, 12)
        Me.Label9.TabIndex = 185
        Me.Label9.Text = "(*)"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.ForeColor = System.Drawing.Color.Red
        Me.Label17.Location = New System.Drawing.Point(412, 92)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(23, 12)
        Me.Label17.TabIndex = 184
        Me.Label17.Text = "(*)"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(412, 64)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(23, 12)
        Me.Label8.TabIndex = 184
        Me.Label8.Text = "(*)"
        '
        'CmbApprentice
        '
        Me.CmbApprentice.AutoCompleteCustomSource.AddRange(New String() {"是", "否"})
        Me.CmbApprentice.FormattingEnabled = True
        Me.CmbApprentice.Items.AddRange(New Object() {"是", "否"})
        Me.CmbApprentice.Location = New System.Drawing.Point(77, 262)
        Me.CmbApprentice.Name = "CmbApprentice"
        Me.CmbApprentice.Size = New System.Drawing.Size(121, 20)
        Me.CmbApprentice.TabIndex = 183
        '
        'TxtMArelation
        '
        Me.TxtMArelation.Location = New System.Drawing.Point(292, 262)
        Me.TxtMArelation.Name = "TxtMArelation"
        Me.TxtMArelation.Size = New System.Drawing.Size(121, 21)
        Me.TxtMArelation.TabIndex = 181
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label7.Location = New System.Drawing.Point(32, 270)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 12)
        Me.Label7.TabIndex = 180
        Me.Label7.Text = "学徒"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label4.Location = New System.Drawing.Point(239, 258)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 36)
        Me.Label4.TabIndex = 179
        Me.Label4.Text = "师徒备注" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "（工号）"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CmbStarLevel
        '
        Me.CmbStarLevel.AutoCompleteCustomSource.AddRange(New String() {"一星级", "二星级", "三星级", "无"})
        Me.CmbStarLevel.FormattingEnabled = True
        Me.CmbStarLevel.Items.AddRange(New Object() {"一星", "二星", "三星", "无"})
        Me.CmbStarLevel.Location = New System.Drawing.Point(292, 152)
        Me.CmbStarLevel.Name = "CmbStarLevel"
        Me.CmbStarLevel.Size = New System.Drawing.Size(121, 20)
        Me.CmbStarLevel.TabIndex = 178
        '
        'TxtKM
        '
        Me.TxtKM.Location = New System.Drawing.Point(77, 221)
        Me.TxtKM.Name = "TxtKM"
        Me.TxtKM.Size = New System.Drawing.Size(121, 21)
        Me.TxtKM.TabIndex = 177
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label20.Location = New System.Drawing.Point(20, 224)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(41, 12)
        Me.Label20.TabIndex = 176
        Me.Label20.Text = "公里数"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label19.Location = New System.Drawing.Point(250, 156)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(29, 12)
        Me.Label19.TabIndex = 175
        Me.Label19.Text = "星级"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CmbAvailableOrNot
        '
        Me.CmbAvailableOrNot.AutoCompleteCustomSource.AddRange(New String() {"是", "否"})
        Me.CmbAvailableOrNot.FormattingEnabled = True
        Me.CmbAvailableOrNot.Items.AddRange(New Object() {"可用", "不可用"})
        Me.CmbAvailableOrNot.Location = New System.Drawing.Point(77, 120)
        Me.CmbAvailableOrNot.Name = "CmbAvailableOrNot"
        Me.CmbAvailableOrNot.Size = New System.Drawing.Size(121, 20)
        Me.CmbAvailableOrNot.TabIndex = 174
        '
        'TxtPhone
        '
        Me.TxtPhone.Location = New System.Drawing.Point(77, 185)
        Me.TxtPhone.Name = "TxtPhone"
        Me.TxtPhone.Size = New System.Drawing.Size(121, 21)
        Me.TxtPhone.TabIndex = 171
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label21.Location = New System.Drawing.Point(31, 191)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(29, 12)
        Me.Label21.TabIndex = 169
        Me.Label21.Text = "电话"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label22.Location = New System.Drawing.Point(226, 188)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(53, 12)
        Me.Label22.TabIndex = 170
        Me.Label22.Text = "技能等级"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CmbTechGrade
        '
        Me.CmbTechGrade.AutoCompleteCustomSource.AddRange(New String() {"高级工", "中级工", "初级工", "无"})
        Me.CmbTechGrade.FormattingEnabled = True
        Me.CmbTechGrade.Items.AddRange(New Object() {"高级工", "中级工", "初级工", "无"})
        Me.CmbTechGrade.Location = New System.Drawing.Point(292, 185)
        Me.CmbTechGrade.Name = "CmbTechGrade"
        Me.CmbTechGrade.Size = New System.Drawing.Size(120, 20)
        Me.CmbTechGrade.TabIndex = 173
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label23.Location = New System.Drawing.Point(250, 123)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(29, 12)
        Me.Label23.TabIndex = 168
        Me.Label23.Text = "原因"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CmbReasonforAvail
        '
        Me.CmbReasonforAvail.AutoCompleteCustomSource.AddRange(New String() {"班组长", "二阶段跟车", "高峰班组", "配检司机", "送饭", "未考证", "线路需要", "一阶段跟车", "暂借运营", "驻勤司机"})
        Me.CmbReasonforAvail.Enabled = False
        Me.CmbReasonforAvail.FormattingEnabled = True
        Me.CmbReasonforAvail.Items.AddRange(New Object() {"班组长", "二阶段跟车", "一阶段跟车", "高峰班组", "配检司机", "送饭", "未考证", "线路需要", "暂借运管", "驻勤司机"})
        Me.CmbReasonforAvail.Location = New System.Drawing.Point(292, 120)
        Me.CmbReasonforAvail.Name = "CmbReasonforAvail"
        Me.CmbReasonforAvail.Size = New System.Drawing.Size(121, 20)
        Me.CmbReasonforAvail.TabIndex = 172
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label24.Location = New System.Drawing.Point(14, 125)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(53, 12)
        Me.Label24.TabIndex = 167
        Me.Label24.Text = "是否可用"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CmbPost
        '
        Me.CmbPost.FormattingEnabled = True
        Me.CmbPost.Items.AddRange(New Object() {"电客司机", "乘务组长", "乘务副组长", "检查员", "安全员"})
        Me.CmbPost.Location = New System.Drawing.Point(77, 55)
        Me.CmbPost.Name = "CmbPost"
        Me.CmbPost.Size = New System.Drawing.Size(120, 20)
        Me.CmbPost.TabIndex = 159
        '
        'CmbClass
        '
        Me.CmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbClass.FormattingEnabled = True
        Me.CmbClass.Items.AddRange(New Object() {"常日班", "乘务1组", "乘务2组", "乘务3组", "乘务4组", "日勤班"})
        Me.CmbClass.Location = New System.Drawing.Point(292, 55)
        Me.CmbClass.Name = "CmbClass"
        Me.CmbClass.Size = New System.Drawing.Size(120, 20)
        Me.CmbClass.TabIndex = 157
        '
        'TxtBeline
        '
        Me.TxtBeline.Location = New System.Drawing.Point(77, 87)
        Me.TxtBeline.Name = "TxtBeline"
        Me.TxtBeline.ReadOnly = True
        Me.TxtBeline.Size = New System.Drawing.Size(119, 21)
        Me.TxtBeline.TabIndex = 156
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.Color.Red
        Me.Label14.Location = New System.Drawing.Point(412, 26)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(23, 12)
        Me.Label14.TabIndex = 155
        Me.Label14.Text = "(*)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(202, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(23, 12)
        Me.Label5.TabIndex = 155
        Me.Label5.Text = "(*)"
        '
        'TxtTeamno
        '
        Me.TxtTeamno.Location = New System.Drawing.Point(292, 87)
        Me.TxtTeamno.Name = "TxtTeamno"
        Me.TxtTeamno.Size = New System.Drawing.Size(119, 21)
        Me.TxtTeamno.TabIndex = 140
        '
        'DriverNameTextBox
        '
        Me.DriverNameTextBox.Location = New System.Drawing.Point(292, 22)
        Me.DriverNameTextBox.Name = "DriverNameTextBox"
        Me.DriverNameTextBox.Size = New System.Drawing.Size(119, 21)
        Me.DriverNameTextBox.TabIndex = 140
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.Location = New System.Drawing.Point(250, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 134
        Me.Label2.Text = "姓名"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DriverIDTextBox
        '
        Me.DriverIDTextBox.Location = New System.Drawing.Point(77, 22)
        Me.DriverIDTextBox.Name = "DriverIDTextBox"
        Me.DriverIDTextBox.ReadOnly = True
        Me.DriverIDTextBox.Size = New System.Drawing.Size(119, 21)
        Me.DriverIDTextBox.TabIndex = 141
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 92)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 136
        Me.Label1.Text = "所属线路"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label12.Location = New System.Drawing.Point(26, 59)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(29, 12)
        Me.Label12.TabIndex = 139
        Me.Label12.Text = "岗位"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.Location = New System.Drawing.Point(26, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 128
        Me.Label3.Text = "工号"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label6.Location = New System.Drawing.Point(250, 57)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 131
        Me.Label6.Text = "班组"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'newTestUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(487, 350)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.CloButton)
        Me.Controls.Add(Me.UpdateButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "newTestUpdate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "司机信息修改"
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents UpdateButton As System.Windows.Forms.Button
    Friend WithEvents CloButton As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtBeline As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DriverNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DriverIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CmbClass As System.Windows.Forms.ComboBox
    Friend WithEvents CmbPost As System.Windows.Forms.ComboBox
    Friend WithEvents CmbStarLevel As System.Windows.Forms.ComboBox
    Friend WithEvents TxtKM As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents CmbAvailableOrNot As System.Windows.Forms.ComboBox
    Friend WithEvents TxtPhone As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents CmbTechGrade As System.Windows.Forms.ComboBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents CmbReasonforAvail As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtMArelation As System.Windows.Forms.TextBox
    Friend WithEvents CmbApprentice As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TxtTeamno As System.Windows.Forms.TextBox
    Friend WithEvents CBEZONE As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
End Class
