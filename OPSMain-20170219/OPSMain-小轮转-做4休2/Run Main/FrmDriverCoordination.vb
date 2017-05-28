Imports System.ComponentModel
Imports Pabo.Calendar
Imports System.Xml
Imports System.Linq

Public Class FrmDriverCoordination

    Private net As Coordination2.Net
    Public CurLine As Coordination2.Line                '当前线路
    Public cstimetableNameList() As String              '列车时刻表名称集
    Public TotalDriverNum As Integer                    '最小驾驶员编号
    Public ActualDriverNum As Integer                   '实际乘务员编号
    Public Yunzhuanpara As String                       '运转制度参数
    Public MaxTiao As Integer                           '最大调班人数

    <Browsable(False)> _
    Public Property Spandate() As TimeSpan              '选择的时间集
        Get
            Return Me.EndDate.Date.AddDays(1) - Me.StartDate.Date
        End Get
        Set(ByVal value As TimeSpan)
        End Set
    End Property

    <Browsable(False)> _
    Public Property StartDate() As DateTime               '开始时间
        Get
            Return Me.FromDate.Value
        End Get
        Set(ByVal value As DateTime)
        End Set
    End Property

    <Browsable(False)> _
    Public Property EndDate() As DateTime               '结束时间
        Get
            Return Me.ToDate.Value
        End Get
        Set(ByVal value As DateTime)
        End Set
    End Property

    Public Sub New(ByVal netpa As Coordination2.Net)

        ' 此调用是 Windows 窗体设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        net = netpa
        MaxTiao = 0
    End Sub

    Private Sub FrmDriverCoordination_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If CurLineName = "" Then
            For Each lin As Coordination2.Line In net.Lines
                Me.CmbLine.Items.Add(lin.Name)
            Next
            If Me.CmbLine.Items.Count > 0 And Me.CmbLine.Text = "" Then
                Me.CmbLine.SelectedIndex = 0
            ElseIf Me.CmbLine.Items.Count > 0 And Me.CmbLine.Text <> "" Then
                Me.CmbLine.SelectedIndex = Me.CmbLine.Items.IndexOf(Me.CmbLine.Text.Trim)
            End If
        Else
            Me.CmbLine.Items.Add(CurLineName)
            Me.CmbLine.Text = CurLineName
        End If

        Me.InitializeCalendar()
        Me.MonthCalendar.SelectButton = Windows.Forms.MouseButtons.Left

        If Me.CmbYunzhuan.Text = "" Then
            Me.CmbYunzhuan.SelectedIndex = 0
        Else
            Me.CmbYunzhuan.SelectedIndex = Me.CmbYunzhuan.Items.IndexOf(Me.CmbYunzhuan.Text.Trim)
        End If

    End Sub

    Private Sub CmbLine_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbLine.SelectedIndexChanged
        If CurLineName = "" Then
            CurLine = net.Lines(CmbLine.SelectedIndex)
        Else
            For Each lin As Coordination2.Line In net.Lines
                If lin.Name = CurLineName Then
                    CurLine = lin
                End If
            Next
        End If

        Me.LBExistPlan.Items.Clear()
        Me.CanderContextMenu.Items.Clear()
        If CurLine.CSTimeTableDic.Count > 0 Then
            For Each cstt As Coordination2.CSTimeTable In CurLine.CSTimeTableDic.Values
                Me.LBExistPlan.Items.Add(cstt.Name)
                Me.CanderContextMenu.Items.Add(cstt.Name)
            Next
            Me.CanderContextMenu.Items.Add(New ToolStripSeparator)
            Me.CanderContextMenu.Items.Add("清除")
            Me.CanderContextMenu.Items.Add("全部清除")
        End If

        For i As Integer = 0 To Me.CanderContextMenu.Items.Count - 1
            AddHandler Me.CanderContextMenu.Items(i).Click, AddressOf CanderContextMenu_Click
        Next

        If Me.LBExistPlan.Items.Count > 0 Then
            Me.LBExistPlan.SelectedIndex = 0
        End If

        Call UpdateOtherDuty()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        For i As Integer = 0 To Me.MonthCalendar.Dates.Count - 1
            If Me.MonthCalendar.Dates(i).Text = "" Then
                MsgBox("乘务计划未设置完整！")
                Exit Sub
            End If
        Next
        'Call SaveTimeTableDate()
        'Call SaveOtherDuty()
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Public Sub SaveTimeTableDate()
        Dim str As String = String.Empty
        If cstimetableNameList.Length > 0 Then
            str = "delete from cs_datetimetable where lineid='" & CurLine.Name & "' and " & _
                  "datediff(dateno,Format('" & Me.StartDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 " & _
                  "and datediff(dateno,Format('" & Me.EndDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>=0"
            Call Globle.Method.UpdateDataForAccess(str)

            For i As Integer = 0 To cstimetableNameList.Length - 1
                str = "insert into cs_datetimetable (lineid,dateno,cstimetableid) " & _
                      "values('" & CurLine.Name & "',Format('" & Me.StartDate.AddDays(i).ToString("yyyy/MM/dd") & _
                      "','yyyy/MM/dd'),'" & Me.CurLine.GetCSTimeTableFromName(cstimetableNameList(i)).ID & "')"
                Call Globle.Method.UpdateDataForAccess(str)
            Next
        End If
    End Sub

    Public Sub UpdateOtherDuty()
        Me.DGVOtherDuty.Rows.Clear()
        Dim str As String = "select * from cs_otherDutyInfo where lineid='" & Me.CurLine.Name & "' order by ID"
        Dim tab As Data.DataTable = Globle.Method.ReadDataForAccess(str)
        If tab IsNot Nothing Then
            For Each row As DataRow In tab.Rows
                Me.DGVOtherDuty.Rows.Add(row.Item("ID").ToString, row.Item("dutysort").ToString, row.Item("driverno").ToString, _
                                         Coordination2.Global.BeTime(row.Item("starttime").ToString), Coordination2.Global.BeTime(row.Item("endtime").ToString), row.Item("drivedistance").ToString)
            Next
        End If
    End Sub

    Public Sub SaveOtherDuty()
        If Me.DGVOtherDuty.Rows.Count > 1 Then
            Dim str As String = "delete from cs_otherDutyInfo where lineid='" & Me.CurLine.Name & "'"
            Call Globle.Method.UpdateDataForAccess(str)
            For i As Integer = 0 To Me.DGVOtherDuty.Rows.Count - 2
                str = "Insert into cs_otherDutyInfo (lineid,id,dutysort,driverno,starttime,endtime,drivedistance) " & _
                    "values('" & Me.CurLine.Name & "'," & Me.DGVOtherDuty.Rows(i).Cells("编号").Value.ToString.Trim & _
                    ",'" & Me.DGVOtherDuty.Rows(i).Cells("班种").Value.ToString.Trim & _
                    "','" & Me.DGVOtherDuty.Rows(i).Cells("任务").Value.ToString.Trim & _
                    "','" & CDate(Me.DGVOtherDuty.Rows(i).Cells("起始时间").Value.ToString.Trim).TimeOfDay.TotalSeconds & _
                    "','" & CDate(Me.DGVOtherDuty.Rows(i).Cells("结束时间").Value.ToString.Trim).TimeOfDay.TotalSeconds & _
                    "'," & Me.DGVOtherDuty.Rows(i).Cells("驾驶公里").Value.ToString.Trim & ")"
                Try
                    Globle.Method.UpdateDataForAccess(str)
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "错误")
                    Exit Sub
                End Try
            Next
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub LBExistPlan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LBExistPlan.SelectedIndexChanged
        PropertyGrid1.SelectedObject = Me.CurLine.CSTimeTableDic.Values(Me.LBExistPlan.SelectedIndex)
    End Sub

    Private Sub ToDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToDate.ValueChanged
        Me.RefreshCalendar()
    End Sub

    Private Sub FromDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FromDate.ValueChanged
        Me.RefreshCalendar()
    End Sub

    Private Sub RefreshCalendar()
        Me.MonthCalendar.Dates.Clear()
        If Me.Spandate.Days > 0 Then
            For i As Integer = 0 To Me.Spandate.Days - 1
                Dim tempdateitem As New Pabo.Calendar.DateItem()
                tempdateitem.Date = Me.StartDate.AddDays(i).Date
                tempdateitem.BackColor1 = Color.Salmon
                Me.MonthCalendar.Dates.Add(tempdateitem)
            Next
            Me.MonthCalendar.Refresh()
            Me.MonthCalendar.ActiveMonth.Year = Me.StartDate.Year
            Me.MonthCalendar.ActiveMonth.Month = Me.StartDate.Month
            ReDim cstimetableNameList(Me.Spandate.Days - 1)
        Else
            MsgBox("日期选择错误！")
            Exit Sub
        End If
    End Sub

    Private Sub InitializeCalendar()
        Me.MonthCalendar.Dates.Clear()
        If Me.Spandate.Days > 0 Then
            If cstimetableNameList Is Nothing Then
                For i As Integer = 0 To Me.Spandate.Days - 1
                    Dim tempdateitem As New Pabo.Calendar.DateItem()
                    tempdateitem.Date = Me.StartDate.Date.AddDays(i).Date
                    tempdateitem.BackColor1 = Color.Salmon
                    Me.MonthCalendar.Dates.Add(tempdateitem)
                Next
                Me.MonthCalendar.Refresh()
                Me.MonthCalendar.ActiveMonth.Year = Me.StartDate.Year
                Me.MonthCalendar.ActiveMonth.Month = Me.StartDate.Month
                ReDim cstimetableNameList(Me.Spandate.Days - 1)
            Else
                For i As Integer = 0 To Me.cstimetableNameList.Length - 1
                    Dim tempdateitem As New Pabo.Calendar.DateItem()
                    tempdateitem.Date = Me.StartDate.Date.AddDays(i).Date
                    tempdateitem.BackColor1 = Color.Salmon
                    tempdateitem.Text = Me.cstimetableNameList(i)
                    Me.MonthCalendar.Dates.Add(tempdateitem)
                Next
                'Me.MonthCalendar.Refresh()
                Me.MonthCalendar.ActiveMonth.Year = Me.StartDate.Year
                Me.MonthCalendar.ActiveMonth.Month = Me.StartDate.Month
            End If
        Else
            MsgBox("日期选择错误！")
            Exit Sub
        End If
    End Sub

    Private Sub CanderContextMenu_Click(ByVal sender As ToolStripMenuItem, ByVal e As System.EventArgs)
        If sender.Text <> "清除" AndAlso sender.Text <> "全部清除" Then
            If Me.MonthCalendar.SelectedDates.Count = 0 Then
                MsgBox("没有选中日期！")
            Else
                For i As Integer = 0 To Me.MonthCalendar.SelectedDates.Count - 1
                    If Me.MonthCalendar.SelectedDates(i).Date < Me.StartDate.Date OrElse Me.MonthCalendar.SelectedDates(i).Date > Me.EndDate.Date Then
                        MsgBox("选择日期不在范围以内！")
                        Exit Sub
                    End If
                Next

                If Me.MonthCalendar.SelectedDates.Count = 1 Then

                    If Me.WorkDayCheck.Checked = True Then
                        If GlobalFunc.IsWeekend(Me.MonthCalendar.SelectedDates(0).Date) = False Then
                            For i As Integer = 0 To Me.Spandate.Days - 1
                                If GlobalFunc.IsWeekend(Me.StartDate.AddDays(i).Date) = False Then
                                    Me.MonthCalendar.Dates(i).Text = sender.Text
                                    Me.cstimetableNameList(i) = sender.Text
                                End If
                            Next
                        End If
                    End If

                    If Me.WeekDayCheck.Checked = True Then
                        If GlobalFunc.IsWeekend(Me.MonthCalendar.SelectedDates(0).Date) = True Then
                            For i As Integer = 0 To Me.Spandate.Days - 1
                                If GlobalFunc.IsWeekend(Me.StartDate.AddDays(i).Date) = True Then
                                    Me.MonthCalendar.Dates(i).Text = sender.Text
                                    Me.cstimetableNameList(i) = sender.Text
                                End If
                            Next
                        End If
                    End If

                    If Me.OneWeekCheck.Checked = True Then
                        For i As Integer = 0 To Me.Spandate.Days - 1
                            If Me.MonthCalendar.SelectedDates(0).Date.AddDays(i * 7).Date <= Me.EndDate.Date Then
                                Me.MonthCalendar.Dates((Me.MonthCalendar.SelectedDates(0).Date.AddDays(i * 7).Date - Me.StartDate.Date).Days).Text = sender.Text
                                Me.cstimetableNameList((Me.MonthCalendar.SelectedDates(0).Date.AddDays(i * 7).Date - Me.StartDate.Date).Days) = sender.Text
                            End If
                        Next

                        For i As Integer = 0 To Me.Spandate.Days - 1
                            If Me.MonthCalendar.SelectedDates(0).Date.AddDays(-i * 7).Date >= Me.StartDate.Date Then
                                Me.MonthCalendar.Dates((Me.MonthCalendar.SelectedDates(0).Date.AddDays(-i * 7).Date - Me.StartDate.Date).Days).Text = sender.Text
                                Me.cstimetableNameList((Me.MonthCalendar.SelectedDates(0).Date.AddDays(-i * 7).Date - Me.StartDate.Date).Days) = sender.Text
                            End If
                        Next
                    End If
                    Me.MonthCalendar.Dates((Me.MonthCalendar.SelectedDates(0).Date - Me.StartDate.Date).Days).Text = sender.Text
                    Me.cstimetableNameList((Me.MonthCalendar.SelectedDates(0).Date - Me.StartDate.Date).Days) = sender.Text
                Else
                    If Me.WeekDayCheck.Checked = True OrElse Me.WorkDayCheck.Checked = True OrElse Me.OneWeekCheck.Checked = True Then
                        MsgBox("快捷功能下，不接受多选！")
                        Exit Sub
                    End If

                    For i As Integer = 0 To Me.MonthCalendar.SelectedDates.Count - 1
                        Me.MonthCalendar.Dates((Me.MonthCalendar.SelectedDates(i).Date - Me.StartDate.Date).Days).Text = sender.Text
                        Me.cstimetableNameList((Me.MonthCalendar.SelectedDates(i).Date - Me.StartDate.Date).Days) = sender.Text
                    Next
                End If
            End If
        Else
            If sender.Text = "清除" Then
                If Me.MonthCalendar.SelectedDates.Count = 0 Then
                    MsgBox("没有选中日期！")
                Else
                    For i As Integer = 0 To Me.MonthCalendar.SelectedDates.Count - 1
                        If Me.MonthCalendar.SelectedDates(i).Date < Me.StartDate.Date OrElse Me.MonthCalendar.SelectedDates(i).Date > Me.EndDate.Date Then
                            MsgBox("选择日期不在范围以内！")
                            Exit Sub
                        End If
                    Next

                    If Me.MonthCalendar.SelectedDates.Count = 1 Then
                        Me.MonthCalendar.Dates((Me.MonthCalendar.SelectedDates(0).Date - Me.StartDate.Date).Days).Text = ""
                        Me.cstimetableNameList((Me.MonthCalendar.SelectedDates(0).Date - Me.StartDate.Date).Days) = Nothing
                    Else
                        For i As Integer = 0 To Me.MonthCalendar.SelectedDates.Count - 1
                            Me.MonthCalendar.Dates((Me.MonthCalendar.SelectedDates(i).Date - Me.StartDate.Date).Days).Text = ""
                            Me.cstimetableNameList((Me.MonthCalendar.SelectedDates(i).Date - Me.StartDate.Date).Days) = Nothing
                        Next
                    End If
                End If
            End If
            If sender.Text = "全部清除" Then
                For i As Integer = 0 To Me.Spandate.Days - 1
                    Me.MonthCalendar.Dates(i).Text = ""
                    Me.cstimetableNameList(i) = Nothing
                Next
            End If
        End If
        For Each item As String In Me.LBExistPlan.Items
            If sender.Text = item.ToString Then
                Me.LBExistPlan.SelectedItem = item
                Exit For
            End If
        Next
        Me.MonthCalendar.Refresh()
    End Sub

    Private Sub CmbYunzhuan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbYunzhuan.SelectedIndexChanged
        If Me.CmbYunzhuan.Text <> "" Then
            Me.Yunzhuanpara = Me.CmbYunzhuan.Text.Trim
            Me.RefreshCalendar()
        End If
    End Sub

    Public Function GetMaxDriverNum() As Integer
        GetMaxDriverNum = 0
        Dim Str As String = "select count(*) as MaxNum from cs_driverinf t where t.lineid = '" & Me.CurLine.Name & "'"

        Dim tab As New DataTable
        tab = Globle.Method.ReadDataForAccess(Str)
        If tab.Rows.Count > 0 Then
            GetMaxDriverNum = CInt(tab.Rows(0).Item("MaxNum"))
        End If
    End Function

    Class CalendarEditingControl
        Inherits DateTimePicker
        Implements IDataGridViewEditingControl

        Private dataGridView As DataGridView
        Private Shadows ValueChanged As Boolean = False
        Private rowIndexNum As Integer

        Public Sub New()
            Me.Format = DateTimePickerFormat.Time
            Me.ShowUpDown = True

        End Sub

        Protected Overrides Sub OnValueChanged(ByVal eventargs As System.EventArgs)
            MyBase.OnValueChanged(eventargs)
            Call NotifyDataGridViewOfValueChange()
        End Sub

        Private Sub NotifyDataGridViewOfValueChange()
            valueChanged = True
            DataGridView.NotifyCurrentCellDirty(True)
        End Sub

        Public Property EditingControlFormattedValue() As Object Implements IDataGridViewEditingControl.EditingControlFormattedValue
            Get
                Return Me.Text
            End Get
            Set(ByVal value As Object)
                Me.Text = value.ToString
                Call NotifyDataGridViewOfValueChange()
            End Set
        End Property

        Public Function GetEditingControlFormattedValue(ByVal context As DataGridViewDataErrorContexts) As Object Implements IDataGridViewEditingControl.GetEditingControlFormattedValue
            Return Me.Text
        End Function

        Public Sub ApplyCellStyleToEditingControl(ByVal dataGridViewCellStyle As DataGridViewCellStyle) Implements IDataGridViewEditingControl.ApplyCellStyleToEditingControl
            Me.Font = dataGridViewCellStyle.Font
            Me.CalendarForeColor = dataGridViewCellStyle.ForeColor
            Me.CalendarMonthBackground = dataGridViewCellStyle.BackColor
        End Sub

        Public Property EditingControlRowIndex() As Integer Implements IDataGridViewEditingControl.EditingControlRowIndex
            Get
                Return rowIndexNum
            End Get
            Set(ByVal value As Integer)
                rowIndexNum = value
            End Set
        End Property

        Public Function EditingControlWantsInputKey(ByVal key As Keys, ByVal dataGridViewWantsInputKey As Boolean) As Boolean Implements IDataGridViewEditingControl.EditingControlWantsInputKey
            ' Let the DateTimePicker handle the keys listed.
            Select Case key And Keys.KeyCode
                Case Keys.Left, Keys.Up, Keys.Down, Keys.Right, Keys.Home, Keys.End, Keys.PageDown, Keys.PageUp
                    Return True
                Case Else
                    Return False
            End Select
        End Function

        Public Sub PrepareEditingControlForEdit(ByVal selectAll As Boolean) Implements IDataGridViewEditingControl.PrepareEditingControlForEdit
            ' No preparation needs to be done.
        End Sub

        Public ReadOnly Property RepositionEditingControlOnValueChange() As Boolean Implements IDataGridViewEditingControl.RepositionEditingControlOnValueChange
            Get
                Return False
            End Get
        End Property

        Public Property EditingControlDataGridView() As DataGridView Implements IDataGridViewEditingControl.EditingControlDataGridView
            Get
                Return dataGridView
            End Get
            Set(ByVal value As DataGridView)
                dataGridView = value
            End Set
        End Property

        Public Property EditingControlValueChanged() As Boolean Implements IDataGridViewEditingControl.EditingControlValueChanged
            Get
                Return valueChanged
            End Get
            Set(ByVal value As Boolean)
                valueChanged = value
            End Set
        End Property

        Public ReadOnly Property EditingControlCursor() As Cursor Implements IDataGridViewEditingControl.EditingPanelCursor
            Get
                Return MyBase.Cursor
            End Get
        End Property


    End Class
End Class