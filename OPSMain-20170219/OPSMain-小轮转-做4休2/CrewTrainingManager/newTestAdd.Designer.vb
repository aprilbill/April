<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class newTestAdd
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
        Me.components = New System.ComponentModel.Container()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.AddButton = New System.Windows.Forms.Button()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.TxtUnicode = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.bezone = New System.Windows.Forms.ComboBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.CmbLine = New System.Windows.Forms.ComboBox()
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
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtTeamNo = New System.Windows.Forms.TextBox()
        Me.DriverNameTextBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DriverIDTextBox = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.GroupBox8.SuspendLayout()
        Me.SuspendLayout()
        '
        'CloseButton
        '
        Me.CloseButton.Location = New System.Drawing.Point(414, 373)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(66, 21)
        Me.CloseButton.TabIndex = 7
        Me.CloseButton.Text = "关闭"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'AddButton
        '
        Me.AddButton.Location = New System.Drawing.Point(323, 373)
        Me.AddButton.Name = "AddButton"
        Me.AddButton.Size = New System.Drawing.Size(64, 21)
        Me.AddButton.TabIndex = 7
        Me.AddButton.Text = "添加"
        Me.AddButton.UseVisualStyleBackColor = True
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.Label28)
        Me.GroupBox8.Controls.Add(Me.Label27)
        Me.GroupBox8.Controls.Add(Me.TxtUnicode)
        Me.GroupBox8.Controls.Add(Me.Label26)
        Me.GroupBox8.Controls.Add(Me.bezone)
        Me.GroupBox8.Controls.Add(Me.Label25)
        Me.GroupBox8.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox8.Controls.Add(Me.Label15)
        Me.GroupBox8.Controls.Add(Me.Label13)
        Me.GroupBox8.Controls.Add(Me.CmbLine)
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
        Me.GroupBox8.Controls.Add(Me.Label14)
        Me.GroupBox8.Controls.Add(Me.Label5)
        Me.GroupBox8.Controls.Add(Me.TxtTeamNo)
        Me.GroupBox8.Controls.Add(Me.DriverNameTextBox)
        Me.GroupBox8.Controls.Add(Me.Label2)
        Me.GroupBox8.Controls.Add(Me.DriverIDTextBox)
        Me.GroupBox8.Controls.Add(Me.Label1)
        Me.GroupBox8.Controls.Add(Me.Label12)
        Me.GroupBox8.Controls.Add(Me.Label3)
        Me.GroupBox8.Controls.Add(Me.Label16)
        Me.GroupBox8.Controls.Add(Me.Label6)
        Me.GroupBox8.Location = New System.Drawing.Point(18, 13)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(462, 346)
        Me.GroupBox8.TabIndex = 9
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "人事档案"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.ForeColor = System.Drawing.Color.Red
        Me.Label28.Location = New System.Drawing.Point(420, 199)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(23, 12)
        Me.Label28.TabIndex = 229
        Me.Label28.Text = "(*)"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.ForeColor = System.Drawing.Color.Red
        Me.Label27.Location = New System.Drawing.Point(207, 310)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(23, 12)
        Me.Label27.TabIndex = 228
        Me.Label27.Text = "(*)"
        '
        'TxtUnicode
        '
        Me.TxtUnicode.Location = New System.Drawing.Point(80, 305)
        Me.TxtUnicode.Name = "TxtUnicode"
        Me.TxtUnicode.Size = New System.Drawing.Size(121, 21)
        Me.TxtUnicode.TabIndex = 227
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label26.Location = New System.Drawing.Point(5, 310)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(65, 12)
        Me.Label26.TabIndex = 226
        Me.Label26.Text = "工作证编号"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'bezone
        '
        Me.bezone.AutoCompleteCustomSource.AddRange(New String() {"一星级", "二星级", "三星级", "无"})
        Me.bezone.FormattingEnabled = True
        Me.bezone.Items.AddRange(New Object() {"主区域"})
        Me.bezone.Location = New System.Drawing.Point(293, 195)
        Me.bezone.Name = "bezone"
        Me.bezone.Size = New System.Drawing.Size(121, 20)
        Me.bezone.TabIndex = 225
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label25.Location = New System.Drawing.Point(252, 202)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(29, 12)
        Me.Label25.TabIndex = 224
        Me.Label25.Text = "区域"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(294, 229)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(121, 21)
        Me.DateTimePicker1.TabIndex = 223
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label15.Location = New System.Drawing.Point(236, 237)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(53, 12)
        Me.Label15.TabIndex = 222
        Me.Label15.Text = "开始统计"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.ForeColor = System.Drawing.Color.Red
        Me.Label13.Location = New System.Drawing.Point(206, 75)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(23, 12)
        Me.Label13.TabIndex = 221
        Me.Label13.Text = "(*)"
        '
        'CmbLine
        '
        Me.CmbLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbLine.FormattingEnabled = True
        Me.CmbLine.Location = New System.Drawing.Point(80, 99)
        Me.CmbLine.Name = "CmbLine"
        Me.CmbLine.Size = New System.Drawing.Size(120, 20)
        Me.CmbLine.TabIndex = 220
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.Red
        Me.Label11.Location = New System.Drawing.Point(205, 106)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(23, 12)
        Me.Label11.TabIndex = 219
        Me.Label11.Text = "(*)"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.Red
        Me.Label10.Location = New System.Drawing.Point(205, 232)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(23, 12)
        Me.Label10.TabIndex = 218
        Me.Label10.Text = "(*)"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Red
        Me.Label9.Location = New System.Drawing.Point(205, 271)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(23, 12)
        Me.Label9.TabIndex = 217
        Me.Label9.Text = "(*)"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.ForeColor = System.Drawing.Color.Red
        Me.Label17.Location = New System.Drawing.Point(415, 106)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(23, 12)
        Me.Label17.TabIndex = 216
        Me.Label17.Text = "(*)"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(415, 76)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(23, 12)
        Me.Label8.TabIndex = 216
        Me.Label8.Text = "(*)"
        '
        'CmbApprentice
        '
        Me.CmbApprentice.AutoCompleteCustomSource.AddRange(New String() {"是", "否"})
        Me.CmbApprentice.FormattingEnabled = True
        Me.CmbApprentice.Items.AddRange(New Object() {"是", "否"})
        Me.CmbApprentice.Location = New System.Drawing.Point(80, 268)
        Me.CmbApprentice.Name = "CmbApprentice"
        Me.CmbApprentice.Size = New System.Drawing.Size(121, 20)
        Me.CmbApprentice.TabIndex = 215
        '
        'TxtMArelation
        '
        Me.TxtMArelation.Location = New System.Drawing.Point(293, 266)
        Me.TxtMArelation.Name = "TxtMArelation"
        Me.TxtMArelation.Size = New System.Drawing.Size(121, 21)
        Me.TxtMArelation.TabIndex = 214
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label7.Location = New System.Drawing.Point(23, 272)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 12)
        Me.Label7.TabIndex = 213
        Me.Label7.Text = "新司机"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label4.Location = New System.Drawing.Point(236, 266)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 36)
        Me.Label4.TabIndex = 212
        Me.Label4.Text = "师徒备注" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "（工号）"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CmbStarLevel
        '
        Me.CmbStarLevel.AutoCompleteCustomSource.AddRange(New String() {"一星级", "二星级", "三星级", "无"})
        Me.CmbStarLevel.FormattingEnabled = True
        Me.CmbStarLevel.Items.AddRange(New Object() {"一星", "二星", "三星", "无"})
        Me.CmbStarLevel.Location = New System.Drawing.Point(294, 164)
        Me.CmbStarLevel.Name = "CmbStarLevel"
        Me.CmbStarLevel.Size = New System.Drawing.Size(121, 20)
        Me.CmbStarLevel.TabIndex = 211
        '
        'TxtKM
        '
        Me.TxtKM.Location = New System.Drawing.Point(80, 229)
        Me.TxtKM.Name = "TxtKM"
        Me.TxtKM.Size = New System.Drawing.Size(121, 21)
        Me.TxtKM.TabIndex = 210
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label20.Location = New System.Drawing.Point(23, 237)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(41, 12)
        Me.Label20.TabIndex = 209
        Me.Label20.Text = "公里数"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label19.Location = New System.Drawing.Point(252, 166)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(29, 12)
        Me.Label19.TabIndex = 208
        Me.Label19.Text = "星级"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CmbAvailableOrNot
        '
        Me.CmbAvailableOrNot.AutoCompleteCustomSource.AddRange(New String() {"是", "否"})
        Me.CmbAvailableOrNot.FormattingEnabled = True
        Me.CmbAvailableOrNot.Items.AddRange(New Object() {"可用", "不可用"})
        Me.CmbAvailableOrNot.Location = New System.Drawing.Point(80, 131)
        Me.CmbAvailableOrNot.Name = "CmbAvailableOrNot"
        Me.CmbAvailableOrNot.Size = New System.Drawing.Size(121, 20)
        Me.CmbAvailableOrNot.TabIndex = 207
        '
        'TxtPhone
        '
        Me.TxtPhone.Location = New System.Drawing.Point(80, 163)
        Me.TxtPhone.Name = "TxtPhone"
        Me.TxtPhone.Size = New System.Drawing.Size(121, 21)
        Me.TxtPhone.TabIndex = 204
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label21.Location = New System.Drawing.Point(29, 170)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(29, 12)
        Me.Label21.TabIndex = 202
        Me.Label21.Text = "电话"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label22.Location = New System.Drawing.Point(17, 203)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(53, 12)
        Me.Label22.TabIndex = 203
        Me.Label22.Text = "技能等级"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CmbTechGrade
        '
        Me.CmbTechGrade.AutoCompleteCustomSource.AddRange(New String() {"高级工", "中级工", "初级工", "无"})
        Me.CmbTechGrade.FormattingEnabled = True
        Me.CmbTechGrade.Items.AddRange(New Object() {"高级工", "中级工", "初级工", "无"})
        Me.CmbTechGrade.Location = New System.Drawing.Point(80, 196)
        Me.CmbTechGrade.Name = "CmbTechGrade"
        Me.CmbTechGrade.Size = New System.Drawing.Size(120, 20)
        Me.CmbTechGrade.TabIndex = 206
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label23.Location = New System.Drawing.Point(252, 134)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(29, 12)
        Me.Label23.TabIndex = 201
        Me.Label23.Text = "原因"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CmbReasonforAvail
        '
        Me.CmbReasonforAvail.AutoCompleteCustomSource.AddRange(New String() {"班组长", "二阶段跟车", "高峰班组", "配检司机", "送饭", "未考证", "线路需要", "一阶段跟车", "暂借运营", "驻勤司机"})
        Me.CmbReasonforAvail.Enabled = False
        Me.CmbReasonforAvail.FormattingEnabled = True
        Me.CmbReasonforAvail.Items.AddRange(New Object() {"班组长", "二阶段跟车", "一阶段跟车", "高峰班组", "配检司机", "送饭", "未考证", "线路需要", "暂借运管", "驻勤司机", "无"})
        Me.CmbReasonforAvail.Location = New System.Drawing.Point(294, 132)
        Me.CmbReasonforAvail.Name = "CmbReasonforAvail"
        Me.CmbReasonforAvail.Size = New System.Drawing.Size(121, 20)
        Me.CmbReasonforAvail.TabIndex = 205
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label24.Location = New System.Drawing.Point(17, 137)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(53, 12)
        Me.Label24.TabIndex = 200
        Me.Label24.Text = "是否可用"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'CmbPost
        '
        Me.CmbPost.FormattingEnabled = True
        Me.CmbPost.Items.AddRange(New Object() {"电客司机", "乘务组长", "乘务副组长", "检查员", "安全员"})
        Me.CmbPost.Location = New System.Drawing.Point(80, 67)
        Me.CmbPost.Name = "CmbPost"
        Me.CmbPost.Size = New System.Drawing.Size(120, 20)
        Me.CmbPost.TabIndex = 199
        '
        'CmbClass
        '
        Me.CmbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbClass.FormattingEnabled = True
        Me.CmbClass.Items.AddRange(New Object() {"常日班", "乘务1组", "乘务2组", "乘务3组", "乘务4组", "日勤班"})
        Me.CmbClass.Location = New System.Drawing.Point(294, 68)
        Me.CmbClass.Name = "CmbClass"
        Me.CmbClass.Size = New System.Drawing.Size(120, 20)
        Me.CmbClass.TabIndex = 198
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.ForeColor = System.Drawing.Color.Red
        Me.Label14.Location = New System.Drawing.Point(415, 38)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(23, 12)
        Me.Label14.TabIndex = 195
        Me.Label14.Text = "(*)"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(205, 37)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(23, 12)
        Me.Label5.TabIndex = 196
        Me.Label5.Text = "(*)"
        '
        'TxtTeamNo
        '
        Me.TxtTeamNo.Location = New System.Drawing.Point(294, 99)
        Me.TxtTeamNo.Name = "TxtTeamNo"
        Me.TxtTeamNo.Size = New System.Drawing.Size(121, 21)
        Me.TxtTeamNo.TabIndex = 193
        '
        'DriverNameTextBox
        '
        Me.DriverNameTextBox.Location = New System.Drawing.Point(294, 35)
        Me.DriverNameTextBox.Name = "DriverNameTextBox"
        Me.DriverNameTextBox.Size = New System.Drawing.Size(119, 21)
        Me.DriverNameTextBox.TabIndex = 193
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label2.Location = New System.Drawing.Point(252, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 12)
        Me.Label2.TabIndex = 190
        Me.Label2.Text = "姓名"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'DriverIDTextBox
        '
        Me.DriverIDTextBox.Location = New System.Drawing.Point(80, 34)
        Me.DriverIDTextBox.Name = "DriverIDTextBox"
        Me.DriverIDTextBox.Size = New System.Drawing.Size(119, 21)
        Me.DriverIDTextBox.TabIndex = 194
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label1.Location = New System.Drawing.Point(17, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 12)
        Me.Label1.TabIndex = 191
        Me.Label1.Text = "所属线路"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label12.Location = New System.Drawing.Point(29, 71)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(29, 12)
        Me.Label12.TabIndex = 192
        Me.Label12.Text = "岗位"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label3.Location = New System.Drawing.Point(29, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 12)
        Me.Label3.TabIndex = 188
        Me.Label3.Text = "工号"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label16.Location = New System.Drawing.Point(252, 102)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(29, 12)
        Me.Label16.TabIndex = 189
        Me.Label16.Text = "组号"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label6.Location = New System.Drawing.Point(252, 70)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(29, 12)
        Me.Label6.TabIndex = 189
        Me.Label6.Text = "班组"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.ForeColor = System.Drawing.Color.Red
        Me.Label18.Location = New System.Drawing.Point(16, 378)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(101, 12)
        Me.Label18.TabIndex = 155
        Me.Label18.Text = "注:(*)为必填部分"
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'newTestAdd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(497, 404)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.GroupBox8)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.AddButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "newTestAdd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "乘务员信息添加"
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents AddButton As System.Windows.Forms.Button
    Friend WithEvents CloseButton As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents CmbApprentice As System.Windows.Forms.ComboBox
    Friend WithEvents TxtMArelation As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
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
    Friend WithEvents CmbPost As System.Windows.Forms.ComboBox
    Friend WithEvents CmbClass As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DriverNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DriverIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CmbLine As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents TxtTeamNo As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents bezone As System.Windows.Forms.ComboBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents TxtUnicode As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
End Class
