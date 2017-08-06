Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing

Public Class frmCSMakeBasicSet
    Public flag As Integer = 0 '用于指示是经过“退出”0退出的，还是“确定退出的”1，若为后者需要刷新底图
    Public ListStation As New List(Of String)
    Public ListJiaolu As New List(Of String)
    Dim minRestList As New List(Of Integer())
#Region "DateTimePick"
    Public Class CalendarColumn
        Inherits DataGridViewColumn

        Public Sub New()
            MyBase.New(New CalendarCell())
        End Sub

        Public Overrides Property CellTemplate() As DataGridViewCell
            Get
                Return MyBase.CellTemplate
            End Get
            Set(ByVal value As DataGridViewCell)
                If Not (value Is Nothing) AndAlso Not value.GetType().IsAssignableFrom(GetType(CalendarCell)) Then
                    Throw New InvalidCastException("Must be a CalendarCell")
                End If
                MyBase.CellTemplate = value
            End Set
        End Property

    End Class

    Public Class CalendarCell
        Inherits DataGridViewTextBoxCell

        Public Sub New()
            Me.Style.Format = "T" '"d"
        End Sub

        Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, ByVal initialFormattedValue As Object, ByVal dataGridViewCellStyle As DataGridViewCellStyle)
            MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle)
            Dim ctl As CalendarEditingControl = CType(DataGridView.EditingControl, CalendarEditingControl)
        End Sub

        Public Overrides ReadOnly Property EditType() As Type
            Get
                Return GetType(CalendarEditingControl)
            End Get
        End Property

        Public Overrides ReadOnly Property ValueType() As Type
            Get
                Return GetType(DateTime)
            End Get
        End Property

        Public Overrides ReadOnly Property DefaultNewRowValue() As Object
            Get
                Return DateTime.Now
            End Get
        End Property

    End Class
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
            ValueChanged = True
            dataGridView.NotifyCurrentCellDirty(True)
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
                Return ValueChanged
            End Get
            Set(ByVal value As Boolean)
                ValueChanged = value
            End Set
        End Property

        Public ReadOnly Property EditingControlCursor() As Cursor Implements IDataGridViewEditingControl.EditingPanelCursor
            Get
                Return MyBase.Cursor
            End Get
        End Property


    End Class
#End Region
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If sState <> "乘务计划编制" Then
            Exit Sub '当前状态肯定不是编制乘务计划，因为没有选择运行图
        End If

        Dim i, j As Integer
        Dim sqlstr As String = ""
        ConDriveTime = Me.dtConTime.Value.TimeOfDay.TotalSeconds  '连续驾车时间

        MornWorkTime = Me.dtMorWTime.Value.TimeOfDay.TotalSeconds
        NoonWorkTime = Me.dtNoonWTime.Value.TimeOfDay.TotalSeconds
        NightWorkTime = Me.dtNigWTime.Value.TimeOfDay.TotalSeconds

        If Me.TxTMorningLength.Text.Trim <> "" AndAlso Me.TxTDayLength.Text.Trim <> "" AndAlso Me.TXTCDayLength.Text.Trim <> "" AndAlso Me.TxTNightLength.Text.Trim <> "" _
             AndAlso Me.TxTMinMorningLength.Text.Trim <> "" AndAlso Me.TxTMinDayLength.Text.Trim <> "" AndAlso Me.TXTMinCDayLength.Text.Trim <> "" AndAlso Me.TxTMinNightLength.Text.Trim <> "" Then
            CS_MorningMaxLength = CDec(Me.TxTMorningLength.Text.Trim)
            CS_DayMaxLength = CDec(Me.TxTDayLength.Text.Trim)
            CS_CDayMaxLength = CDec(Me.TXTCDayLength.Text.Trim)
            CS_NightMaxLength = CDec(Me.TxTNightLength.Text.Trim)
            CS_MorningMinLength = CDec(Me.TxTMinMorningLength.Text.Trim)
            CS_DayMinLength = CDec(Me.TxTMinDayLength.Text.Trim)
            CS_CDayMinLength = CDec(Me.TXTMinCDayLength.Text.Trim)
            CS_NightMinLength = CDec(Me.TxTMinNightLength.Text.Trim)
        Else
            MsgBox("参数不能为空")
            Exit Sub
        End If
        If CheckBox1.Checked = True Then '强制公里数
            ForceDriveLength = 1
        Else
            ForceDriveLength = 0
        End If
        If CheckBox2.Checked = True Then '强制工作时间
            ForceDutyTime = 1
        Else
            ForceDutyTime = 0
        End If
        Try
            minChuDis = CDec(Me.TextBox2.Text.Trim)
            minRuDis = CDec(Me.TextBox1.Text.Trim)
        Catch ex As Exception

        End Try
        Try
            '将基本设置信息存到数据库中 OK
            sqlstr = "DELETE FROM CS_CREWBASICINF WHERE LINEID='" & CStr(strCurlineID) & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "SELECT * FROM CS_CREWBASICINF "
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            tempTable.Rows.Add(CStr(strCurlineID) _
                               , ForceDutyTime.ToString & "|" & ForceDriveLength.ToString _
                               , Me.dtMorBTime.Value _
                               , Me.dtNoonBTime.Value _
                               , Me.dtNigBTime.Value _
                               , Me.dtConTime.Value _
                               , Me.dtPrepareTrainTime.Value _
                               , Me.dtPrepareDutyTime.Value _
                               , minRuDis.ToString & ";" & minChuDis.ToString _
                               , CS_MorningMaxLength _
                               , CS_DayMaxLength _
                               , CS_NightMaxLength _
                               , CS_CDayMaxLength _
                               , CS_MorningMinLength _
                               , CS_DayMinLength _
                               , CS_NightMinLength _
                               , CS_CDayMinLength _
                               , Me.dtMorWTime.Value _
                               , Me.dtNoonWTime.Value _
                               , Me.dtNigWTime.Value _
                               , Me.dtPrepareDutyoffTime.Value _
                               )
            Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        Catch ex As Exception
            MsgBox("作业参数保存失败！")
        End Try


        '将轮换地点信息存到数据库中 ok
        Dim id As Integer
        Dim RoutingName As String
        Dim ChangePlace As String
        Dim DirectionMatch As Integer
        Dim DirectionMatch1 As Integer
        Try
            sqlstr = "DELETE FROM CS_CHANGEPLACEINF WHERE LINEID='" & CStr(strCurlineID) & "' and timetableid='" & DiagramCurID & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "SELECT * FROM CS_CHANGEPLACEINF "
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            For i = 0 To DataGridView2.Rows.Count - 1
                id = CInt(DataGridView2.Rows(i).Cells(0).Value)
                RoutingName = DataGridView2.Rows(i).Cells(1).Value.ToString
                ChangePlace = DataGridView2.Rows(i).Cells(2).Value.ToString
                Dim startTime As DateTime = DataGridView2.Rows(i).Cells(3).Value
                Dim endTime As DateTime = DataGridView2.Rows(i).Cells(4).Value
                Dim restTime As DateTime = DataGridView2.Rows(i).Cells(8).Value
                Select Case DataGridView2.Rows(i).Cells(5).Value.ToString
                    Case "上行"
                        DirectionMatch = 0
                    Case "下行"
                        DirectionMatch = 1
                    Case "双向"
                        DirectionMatch = 2
                    Case ""
                        DirectionMatch = 2
                End Select
                Select Case DataGridView2.Rows(i).Cells(6).Value.ToString
                    Case "上行"
                        DirectionMatch1 = 0
                    Case "下行"
                        DirectionMatch1 = 1
                    Case "双向"
                        DirectionMatch1 = 2
                    Case ""
                        DirectionMatch1 = 2
                End Select
                tempTable.Rows.Add(CStr(strCurlineID), CStr(id), CStr(RoutingName), CStr(ChangePlace), CStr(DirectionMatch), startTime, endTime, DataGridView2.Rows(i).Cells(9).Value, DataGridView2.Rows(i).Cells(7).Value, CStr(DirectionMatch1), restTime, CStr(DiagramCurID))
            Next
            Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        Catch ex As Exception
            MsgBox("换班地点保存失败！")
        End Try


        Try
            '将上班地点信息存到数据库中 ok
            sqlstr = "DELETE FROM CS_SHIFTPLACEINF WHERE LINEID='" & CStr(strCurlineID) & "' and timetableid='" & DiagramCurID & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "SELECT * FROM CS_SHIFTPLACEINF "
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)

            For i = 0 To DataGridView4.Rows.Count - 1
                id = CInt(DataGridView4.Rows(i).Cells(0).Value)
                ChangePlace = DataGridView4.Rows(i).Cells(2).Value.ToString
                Select Case DataGridView4.Rows(i).Cells(3).Value.ToString
                    Case "上行"
                        DirectionMatch = 0
                    Case "下行"
                        DirectionMatch = 1
                    Case "双向"
                        DirectionMatch = 2
                End Select
                Dim DayDutyStartTime As DateTime = DataGridView4.Rows(i).Cells(4).Value
                Dim NightDutyStartTime As DateTime = DataGridView4.Rows(i).Cells(5).Value
                tempTable.Rows.Add(CStr(strCurlineID), CStr(id), CStr(ChangePlace), CStr(DirectionMatch), DayDutyStartTime, NightDutyStartTime, DataGridView4.Rows(i).Cells(6).Value.ToString, DataGridView4.Rows(i).Cells(1).Value.ToString, CStr(DiagramCurID))
            Next
            Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        Catch ex As Exception
            MsgBox("上班地点保存失败！")
        End Try



        Try
            '将用餐地点信息存到数据库中 ok
            sqlstr = "DELETE FROM CS_DINNERTIME WHERE LINEID='" & CStr(strCurlineID) & "' and timetableid='" & DiagramCurID & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "SELECT * FROM CS_DINNERTIME "
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            For i = 0 To DGVDinnertime.Rows.Count - 1
                id = CInt(DGVDinnertime.Rows(i).Cells(0).Value)
                Dim DinnerDuty As String = DGVDinnertime.Rows(i).Cells("用餐班种").Value.ToString
                Dim DinnerStart As String = CDate(DGVDinnertime.Rows(i).Cells("起始时间").Value.ToString).TimeOfDay.TotalSeconds
                Dim DinnerEnd As String = CDate(DGVDinnertime.Rows(i).Cells("终止时间").Value.ToString).TimeOfDay.TotalSeconds
                Dim DinnerTime As String = CDate(DGVDinnertime.Rows(i).Cells("用餐时间").Value.ToString).TimeOfDay.TotalSeconds
                Dim DinnerPlace As String = DGVDinnertime.Rows(i).Cells("用餐地点").Value.ToString
                Dim DinnerRoute As String = DGVDinnertime.Rows(i).Cells("用餐交路").Value.ToString
                Dim Dinnertype As String = DGVDinnertime.Rows(i).Cells("餐种").Value.ToString
                Select Case DGVDinnertime.Rows(i).Cells("饭前方向").Value.ToString
                    Case "上行"
                        DirectionMatch = 0
                    Case "下行"
                        DirectionMatch = 1
                    Case "双向"
                        DirectionMatch = 2
                End Select
                Select Case DGVDinnertime.Rows(i).Cells("饭后方向").Value.ToString
                    Case "上行"
                        DirectionMatch1 = 0
                    Case "下行"
                        DirectionMatch1 = 1
                    Case "双向"
                        DirectionMatch1 = 2
                End Select
                tempTable.Rows.Add(CStr(strCurlineID), CStr(DinnerDuty), CStr(DinnerStart), CStr(DinnerEnd), CStr(DinnerTime), CStr(DinnerRoute), CStr(DinnerPlace), CStr(DirectionMatch), CStr(DirectionMatch1), CStr(id), CStr(DiagramCurID), CStr(Dinnertype))
            Next
            Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        Catch ex As Exception
            MsgBox("用餐参数保存失败！")
        End Try

        Try
            '将交路信息存到数据库中 ok
            sqlstr = "DELETE FROM CS_ROUTINGINF WHERE LINEID='" & CStr(strCurlineID) & "' and TIMETABLEID='" & CStr(DiagramCurID) & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "SELECT * FROM CS_ROUTINGINF "
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            For i = 0 To DataGridView3.Rows.Count - 1
                Dim StartStationName, EndStationName As String
                StartStationName = ""
                EndStationName = ""
                id = CInt(DataGridView3.Rows(i).Cells(0).Value)
                ChangePlace = DataGridView3.Rows(i).Cells(1).Value.ToString
                DirectionMatch = DataGridView3.Rows(i).Cells(2).Value.ToString '交路编号
                For j = 1 To UBound(BasicTrainInf)
                    If BasicTrainInf(j).sJiaoLuName = ChangePlace Then
                        StartStationName = BasicTrainInf(j).StartStation
                        EndStationName = BasicTrainInf(j).EndStation
                    End If
                Next
                tempTable.Rows.Add(CStr(strCurlineID), CStr(id), CStr(ChangePlace), CStr(DirectionMatch), CStr(StartStationName), CStr(EndStationName), CStr(DataGridView3.Rows(i).Cells(3).Value.ToString), CStr(DiagramCurID))
            Next
            Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        Catch ex As Exception
            MsgBox("交路参数保存失败！")
        End Try
        flag = 1
        MsgBox("保存完毕！")
    End Sub

    Public Function PiPei1(ByVal a As Integer) As String
        Select Case a
            Case 0
                Return "上行"
            Case 1
                Return "下行"
            Case 2
                Return "双向"
            Case Else
                Return ""
        End Select

    End Function
   
    Private Sub frmCSBasicSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sqlstr As String = ""
        For i As Integer = 1 To UBound(StationInf)
            If StationInf(i).sStationName <> "" Then
                If ListStation.Contains(StationInf(i).sStationName) = False Then
                    ListStation.Add(StationInf(i).sStationName)
                    ListBoxSta.Items.Add(StationInf(i).sStationName)
                End If

            End If
        Next

        For i As Integer = 1 To UBound(CSTrainInf)
            If CSTrainInf(i).Train <> "" AndAlso ListBox1.Items.Contains(CSTrainInf(i).sJiaoLuName) = False Then
                ListBox1.Items.Add(CSTrainInf(i).sJiaoLuName)
                ListJiaolu.Add(CSTrainInf(i).sJiaoLuName)
                接续交路.Items.Add(CSTrainInf(i).sJiaoLuName)
            End If
        Next

        sqlstr = "SELECT * FROM CS_CREWBASICINF WHERE LINEID='" & CStr(strCurlineID) & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        If tempTable.Rows.Count > 0 Then
            'ok
            Me.dtMorBTime.Value = CDate(tempTable.Rows(0).Item("Time1").ToString)
            Me.dtNoonBTime.Value = CDate(tempTable.Rows(0).Item("Time2").ToString)
            Me.dtNigBTime.Value = CDate(tempTable.Rows(0).Item("Time3").ToString)
            Me.dtConTime.Value = CDate(tempTable.Rows(0).Item("ConDriveTime").ToString)
            Me.dtPrepareTrainTime.Value = CDate(tempTable.Rows(0).Item("PrepareTrainTime").ToString)
            Me.dtPrepareDutyTime.Value = CDate(tempTable.Rows(0).Item("PREPAREDUTYTIME").ToString)
            Me.dtPrepareDutyoffTime.Value = CDate(tempTable.Rows(0).Item("PREDUTYOFFTIME").ToString)
            Me.TxTMorningLength.Text = tempTable.Rows(0).Item("MorningDistance").ToString
            Me.TxTDayLength.Text = tempTable.Rows(0).Item("DayDistance").ToString
            Me.TXTCDayLength.Text = tempTable.Rows(0).Item("CDayDistance").ToString
            Me.TxTNightLength.Text = tempTable.Rows(0).Item("NightDistance").ToString
            Me.TxTMinMorningLength.Text = tempTable.Rows(0).Item("MorningminDistance").ToString
            Me.TxTMinDayLength.Text = tempTable.Rows(0).Item("DayminDistance").ToString
            Me.TXTMinCDayLength.Text = tempTable.Rows(0).Item("CDayminDistance").ToString
            Me.TxTMinNightLength.Text = tempTable.Rows(0).Item("NightminDistance").ToString
            Me.dtMorWTime.Value = CDate(tempTable.Rows(0).Item("DAYWTIME").ToString)
            Me.dtNoonWTime.Value = CDate(tempTable.Rows(0).Item("NOONWTIME").ToString)
            Me.dtNigWTime.Value = CDate(tempTable.Rows(0).Item("NIGHTWTIME").ToString)
            Dim checkpara As String = tempTable.Rows(0).Item("uniform").ToString.Trim
            Try
                Me.TextBox1.Text = tempTable.Rows(0).Item("SPECIALCHURU").ToString.Trim.Split(";")(0) '入库
                Me.TextBox2.Text = tempTable.Rows(0).Item("SPECIALCHURU").ToString.Trim.Split(";")(1) '出库
            Catch ex As Exception
                Me.TextBox1.Text = "2"
                Me.TextBox2.Text = "2"
            End Try
            Try
                If checkpara.Split("|")(0).Trim = "1" Then
                    CheckBox2.Checked = True
                Else
                    CheckBox2.Checked = False
                End If
                If checkpara.Split("|")(1).Trim = "1" Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If
            Catch ex As Exception

            End Try
        End If
        Dim CurDiagramcurID As String = DiagramCurID
        'ok
        sqlstr = "SELECT * FROM CS_ROUTINGINF WHERE LINEID='" & CStr(strCurlineID) & "' and  timetableid='" & CStr(DiagramCurID) & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        DataGridView3.RowCount = tempTable.Rows.Count
        If tempTable.Rows.Count > 0 Then
            For i = 0 To tempTable.Rows.Count - 1
                DataGridView3.Rows(i).Cells(0).Value = tempTable.Rows(i).Item("ID").ToString
                DataGridView3.Rows(i).Cells(1).Value = tempTable.Rows(i).Item("RoutingName").ToString
                DataGridView3.Rows(i).Cells(2).Value = CInt(tempTable.Rows(i).Item("CrewType"))
                If Me.ListBoxJiaoLuSta.Items.Contains(tempTable.Rows(i).Item("RoutingName").ToString) = False Then
                    Me.ListBoxJiaoLuSta.Items.Add(tempTable.Rows(i).Item("RoutingName").ToString)
                End If
                DataGridView3.Rows(i).Cells(3).Value = tempTable.Rows(i).Item("ReRoutingName").ToString
            Next
        Else
            sqlstr = "select * from tms_traindiagraminfo where traindiagramid in (SELECT distinct(timetableid) from cs_routinginf where lineid='" & CStr(strCurlineID) & "') and linename='" & CStr(strCurlineID) & "'"
            Dim tmpDT As New DataTable
            tmpDT = Globle.Method.ReadDataForAccess(sqlstr)
            If tmpDT.Rows.Count > 0 Then
                If MsgBox("是否要引用其他位置图所设置的参数", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                    Dim frm1 As New frmInputBox
                    frm1.labTitle.Text = "请选择需要借用参数的运行图"
                    frm1.txtText.Visible = False
                    For s As Integer = 0 To tmpDT.Rows.Count - 1
                        frm1.cmbText.Items.Add(tmpDT.Rows(s).Item("timetablename").ToString)
                    Next
                    If frm1.ShowDialog = Windows.Forms.DialogResult.OK Then
                        CurDiagramcurID = tmpDT.Rows(frm1.selectindex).Item("traindiagramid").ToString
                        sqlstr = "SELECT * FROM CS_ROUTINGINF WHERE LINEID='" & CStr(strCurlineID) & "' and  timetableid='" & CStr(CurDiagramcurID) & "'"
                        tempTable = New Data.DataTable
                        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
                        DataGridView3.RowCount = tempTable.Rows.Count
                        If tempTable.Rows.Count > 0 Then
                            For i = 0 To tempTable.Rows.Count - 1
                                DataGridView3.Rows(i).Cells(0).Value = tempTable.Rows(i).Item("ID").ToString
                                DataGridView3.Rows(i).Cells(1).Value = tempTable.Rows(i).Item("RoutingName").ToString
                                DataGridView3.Rows(i).Cells(2).Value = CInt(tempTable.Rows(i).Item("CrewType"))
                                If Me.ListBoxJiaoLuSta.Items.Contains(tempTable.Rows(i).Item("RoutingName").ToString) = False Then
                                    Me.ListBoxJiaoLuSta.Items.Add(tempTable.Rows(i).Item("RoutingName").ToString)
                                End If
                                DataGridView3.Rows(i).Cells(3).Value = tempTable.Rows(i).Item("ReRoutingName").ToString
                            Next
                        End If
                    End If
                End If
            End If
            End If


            'ok
            sqlstr = "SELECT * FROM cs_dinnertime WHERE LINEID='" & CStr(strCurlineID) & "' and timetableid='" & CStr(CurDiagramcurID) & "'"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            Me.DGVDinnertime.Rows.Clear()
            For Each row As DataRow In tempTable.Rows
                Dim id As String = row.Item("ID").ToString.Trim
                Dim DutySort As String = row.Item("dutysort").ToString.Trim
                Dim StartTime As String = BeTime(row.Item("starttime").ToString.Trim)
                Dim EndTime As String = BeTime(row.Item("endtime").ToString.Trim)
                Dim DinnerTime As String = BeTime(row.Item("dinnertime").ToString.Trim)
                Dim DinnerRoute As String = row.Item("routing").ToString.Trim
                Dim DinnerSta As String = row.Item("stationname").ToString.Trim
                Dim DinnerDir As String = PiPei1(CInt(row.Item("DirectionMatch").ToString.Trim))
                Dim afterDinnerDir As String = PiPei1(CInt(row.Item("DirectionMatch1").ToString.Trim))
                Dim Dinnertype As String = row.Item("DINNERTYPE").ToString.Trim
                Me.DGVDinnertime.Rows.Add(id, DutySort, Dinnertype, StartTime, EndTime, DinnerTime, DinnerRoute, DinnerSta, DinnerDir, afterDinnerDir)
            Next

            'ok
            sqlstr = "SELECT * FROM CS_CHANGEPLACEINF WHERE LINEID='" & CStr(strCurlineID) & "' and timetableid='" & CStr(CurDiagramcurID) & "'"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            DataGridView2.RowCount = tempTable.Rows.Count
            minRestList.Clear()
            For i = 0 To tempTable.Rows.Count - 1
                Dim minRest() As Integer = SearchLeastRest(tempTable.Rows(i).Item("RoutingName").ToString, tempTable.Rows(i).Item("ChangePlace").ToString)
                minRestList.Add(minRest)
                DataGridView2.Rows(i).Cells(0).Value = tempTable.Rows(i).Item("ID").ToString
                DataGridView2.Rows(i).Cells(1).Value = tempTable.Rows(i).Item("RoutingName").ToString
                DataGridView2.Rows(i).Cells(2).Value = tempTable.Rows(i).Item("ChangePlace").ToString
                DataGridView2.Rows(i).Cells(3).Value = CDate(tempTable.Rows(i).Item("BETIME").ToString)
                DataGridView2.Rows(i).Cells(4).Value = CDate(tempTable.Rows(i).Item("ENDTIME").ToString)
                DataGridView2.Rows(i).Cells(5).Value = PiPei1(CInt(tempTable.Rows(i).Item("DirectionMatch")))
                DataGridView2.Rows(i).Cells(6).Value = PiPei1(CInt(tempTable.Rows(i).Item("DirectionMatch1")))
                DataGridView2.Rows(i).Cells(7).Value = tempTable.Rows(i).Item("followdriver").ToString
                DataGridView2.Rows(i).Cells(8).Value = CDate(tempTable.Rows(i).Item("resttime").ToString).ToString("HH:mm:ss")
                DataGridView2.Rows(i).Cells(9).Value = tempTable.Rows(i).Item("IfMustChange")

            Next

            sqlstr = "SELECT * FROM CS_SHIFTPLACEINF WHERE LINEID='" & CStr(strCurlineID) & "'  and timetableid='" & CStr(CurDiagramcurID) & "'"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            DataGridView4.RowCount = tempTable.Rows.Count
            For i = 0 To tempTable.Rows.Count - 1
                DataGridView4.Rows(i).Cells(0).Value = tempTable.Rows(i).Item("ID").ToString
                DataGridView4.Rows(i).Cells(1).Value = tempTable.Rows(i).Item("routing").ToString
                DataGridView4.Rows(i).Cells(2).Value = tempTable.Rows(i).Item("ShiftPlace").ToString
                DataGridView4.Rows(i).Cells(3).Value = PiPei1(CInt(tempTable.Rows(i).Item("DirectionMatch")))
                DataGridView4.Rows(i).Cells(4).Value = CDate(tempTable.Rows(i).Item("DayDutyStartTime").ToString).ToString("HH:mm:ss")
                DataGridView4.Rows(i).Cells(5).Value = CDate(tempTable.Rows(i).Item("NightDutyStartTime").ToString).ToString("HH:mm:ss")
                DataGridView4.Rows(i).Cells(6).Value = tempTable.Rows(i).Item("AvaDutyOffPlace").ToString
            Next

    End Sub

  
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        ListBoxSta_MouseDoubleClick(Nothing, Nothing)
    End Sub

    Private Sub ListBoxSta_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListBoxSta.MouseDoubleClick

        If ListBoxSta.SelectedItem Is Nothing Or ListBoxJiaoLuSta.SelectedItem Is Nothing Then
            MsgBox("请选择项目", MsgBoxStyle.OkOnly)
            Exit Sub
        Else
            Dim i As Integer
            If DataGridView2.Rows.Count > 0 Then
                For i = 0 To DataGridView2.Rows.Count - 1
                    If ListBoxSta.SelectedItem.ToString = DataGridView2.Rows(i).Cells(2).Value.ToString _
                    And ListBoxJiaoLuSta.SelectedItem.ToString = DataGridView2.Rows(i).Cells(1).Value.ToString Then
                        MsgBox("数据已添加", MsgBoxStyle.OkOnly, "错误提示")
                        Exit Sub
                    End If
                Next
            End If
            Dim minRest() As Integer = SearchLeastRest(ListBoxJiaoLuSta.SelectedItem.ToString, ListBoxSta.SelectedItem.ToString)
            minRestList.Add(minRest)
            DataGridView2.Rows.Add(1)
            DataGridView2.Rows(DataGridView2.Rows.Count - 1).Cells(0).Value = DataGridView2.Rows.Count
            DataGridView2.Rows(DataGridView2.Rows.Count - 1).Cells(1).Value = ListBoxJiaoLuSta.SelectedItem.ToString
            DataGridView2.Rows(DataGridView2.Rows.Count - 1).Cells(2).Value = ListBoxSta.SelectedItem.ToString
            DataGridView2.Rows(DataGridView2.Rows.Count - 1).Cells(3).Value = CDate("02:00:00").ToLongTimeString.ToString
            DataGridView2.Rows(DataGridView2.Rows.Count - 1).Cells(4).Value = CDate("02:00:00").ToLongTimeString.ToString
            DataGridView2.Rows(DataGridView2.Rows.Count - 1).Cells(5).Value = "双向"
            DataGridView2.Rows(DataGridView2.Rows.Count - 1).Cells(6).Value = "双向"
            DataGridView2.Rows(DataGridView2.Rows.Count - 1).Cells(7).Value = "1"
            DataGridView2.Rows(DataGridView2.Rows.Count - 1).Cells(8).Value = BeTime(minRest(0) + minRest(1))
            DataGridView2.Rows(DataGridView2.Rows.Count - 1).Cells(9).Value = False
        End If

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        ListBox1_MouseDoubleClick(Nothing, Nothing)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If DataGridView3.Rows.Count > 0 Then
            If DataGridView3.SelectedRows.Count > 0 Then
                Dim i, k As Integer
                k = DataGridView3.SelectedRows.Count
                For i = 1 To k
                    DataGridView3.Rows.Remove(DataGridView3.SelectedRows(0))
                Next
            Else
                DataGridView3.Rows.RemoveAt(DataGridView3.CurrentRow.Index)
            End If
        Else
            Exit Sub
        End If


    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If DataGridView2.Rows.Count > 0 Then
            If DataGridView2.SelectedRows.Count > 0 Then
                minRestList.RemoveAt(DataGridView2.SelectedRows(0).Index)
                DataGridView2.Rows.Remove(DataGridView2.SelectedRows(0))

            End If
        Else
            Exit Sub
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Me.Close()
    End Sub
    Private Sub TabPage4_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabPage4.Leave
        ListBoxJiaoLuSta.Items.Clear()
        Dim i As Integer
        If DataGridView3.Rows.Count > 0 Then
            For i = 0 To DataGridView3.Rows.Count - 1
                If ListBoxJiaoLuSta.Items.Contains(DataGridView3.Rows(i).Cells(1).Value.ToString) = False Then
                    ListBoxJiaoLuSta.Items.Add(DataGridView3.Rows(i).Cells(1).Value.ToString)
                End If
            Next
        End If
    End Sub

    Private Sub DataGridView3_RowsRemoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView3.RowsRemoved
        Dim i As Integer
        For i = 0 To DataGridView3.Rows.Count - 1
            DataGridView3.Rows(i).Cells(0).Value = i + 1
        Next
    End Sub

    Private Sub DataGridView2_RowsRemoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView2.RowsRemoved
        Dim i As Integer
        For i = 0 To DataGridView2.Rows.Count - 1
            DataGridView2.Rows(i).Cells(0).Value = i + 1
        Next
    End Sub

    Private Sub DataGridView4_RowsRemoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles DataGridView4.RowsRemoved
        Dim i As Integer
        For i = 0 To DataGridView4.Rows.Count - 1
            DataGridView4.Rows(i).Cells(0).Value = i + 1
        Next
    End Sub


    Function dTime(ByVal temp As Long, ByVal Mark As Integer) As String
        dTime = ""
        If temp = -1 Then
            dTime = ""
            Exit Function
        End If
        '将时间以小时算
        Dim HStr As String
        Dim MStr As String
        Dim sStr As String
        Dim sSpace As String
        sSpace = " "
        HStr = Trim$(Str$(Int(temp / 3600)))
        MStr = Trim$(Str$(Int((temp - Val(HStr) * 3600) / 60)))
        sStr = Trim$(Str$(temp - Val(HStr) * 3600 - Val(MStr) * 60))
        If Val(HStr) > 24 Then
            HStr = HStr - 24
        End If

        If Val(HStr) < 10 And Val(HStr) > 0 Then
            HStr = sSpace & HStr
        ElseIf Val(HStr) = 24 Or Val(HStr) = 0 Then
            HStr = sSpace & "0"
        End If

        If Val(MStr) < 10 And Val(MStr) > 0 Then
            MStr = "0" & MStr
        ElseIf Val(MStr) = 0 Then
            MStr = "00"
        End If

        If Val(sStr) < 10 And Val(sStr) > 0 Then
            sStr = "0" & sStr
        ElseIf Val(sStr) = 0 Then
            sStr = "00"
        End If
        If HStr.Trim.Length = 1 Then
            HStr = "0" & HStr.Trim
        End If
        Select Case Mark
            Case 0
                dTime = HStr & ":" & MStr & ":" & sStr
            Case 1
                'dTime = sSpace & sSpace & "." & MStr & "." & sStr
                dTime = HStr & ":" & MStr & ":" & sStr
            Case 2
                dTime = "" 'HStr & "." & MStr
            Case 3
                dTime = ""
            Case 4
                dTime = "—"
            Case 5
                'dTime = HStr & "." & MStr
                dTime = HStr & ":" & MStr
        End Select

    End Function
    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        Dim frm As New FrmAddDinnerTime(ListStation, ListJiaolu)
        frm.Text = "添加用餐参数"
        If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim DutySort As String = frm.cmbDutysort.Text.Trim
            Dim StartTime As String = frm.dtStartTime.Value.ToString("HH:mm:ss")
            Dim EndTime As String = frm.DTEndtime.Value.ToString("HH:mm:ss")
            Dim DinnerTime As String = frm.DTDinnerTime.Value.ToString("HH:mm:ss")
            Dim DinnerJiaolu As String = frm.ComboBox1.Text.Trim
            Dim DinnerSta As String = frm.ComboBox2.Text.Trim
            Dim DinnerDir As String = frm.ComboBox3.Text.Trim
            Dim afterDinnerDir As String = frm.ComboBox4.Text.Trim
            Dim Dinnertype As String = frm.ComboBox5.Text.Trim
            Me.DGVDinnertime.Rows.Add((Me.DGVDinnertime.Rows.Count + 1).ToString, DutySort, Dinnertype, StartTime, EndTime, DinnerTime, DinnerJiaolu, DinnerSta, DinnerDir, afterDinnerDir)
        End If
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        If Me.DGVDinnertime.SelectedRows.Count = 1 Then
            Dim frm As New FrmAddDinnerTime(ListStation, ListJiaolu)
            frm.Text = "修改用餐参数"
            Dim selectrow As DataGridViewRow = Me.DGVDinnertime.SelectedRows(0)
            frm.cmbDutysort.Text = selectrow.Cells("用餐班种").Value.ToString.Trim
            frm.dtStartTime.Value = CDate(Now.ToString("yyyy/MM/dd") & " " & selectrow.Cells("起始时间").Value.ToString.Trim)
            frm.DTEndtime.Value = CDate(Now.ToString("yyyy/MM/dd") & " " & selectrow.Cells("终止时间").Value.ToString.Trim)
            frm.DTDinnerTime.Value = CDate(Now.ToString("yyyy/MM/dd") & " " & selectrow.Cells("用餐时间").Value.ToString.Trim)
            frm.ComboBox1.Text = selectrow.Cells("用餐交路").Value.ToString.Trim
            frm.ComboBox2.Text = selectrow.Cells("用餐地点").Value.ToString.Trim
            frm.ComboBox3.Text = selectrow.Cells("饭前方向").Value.ToString.Trim
            frm.ComboBox4.Text = selectrow.Cells("饭后方向").Value.ToString.Trim
            frm.ComboBox5.Text = selectrow.Cells("餐种").Value.ToString.Trim

            If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim DutySort As String = frm.cmbDutysort.Text.Trim
                Dim StartTime As String = frm.dtStartTime.Value.ToString("HH:mm:ss")
                Dim EndTime As String = frm.DTEndtime.Value.ToString("HH:mm:ss")
                Dim DinnerTime As String = frm.DTDinnerTime.Value.ToString("HH:mm:ss")
                Dim DinnerJiaolu As String = frm.ComboBox1.Text.Trim
                Dim DinnerSta As String = frm.ComboBox2.Text.Trim
                Dim DinnerDir As String = frm.ComboBox3.Text.Trim
                Dim afterDinnerDir As String = frm.ComboBox4.Text.Trim
                Dim Dinnertype As String = frm.ComboBox5.Text.Trim
                selectrow.Cells("用餐班种").Value = DutySort
                selectrow.Cells("餐种").Value = Dinnertype
                selectrow.Cells("起始时间").Value = StartTime
                selectrow.Cells("终止时间").Value = EndTime
                selectrow.Cells("用餐时间").Value = DinnerTime
                selectrow.Cells("用餐交路").Value = DinnerJiaolu
                selectrow.Cells("用餐地点").Value = DinnerSta
                selectrow.Cells("饭前方向").Value = DinnerDir
                selectrow.Cells("饭后方向").Value = afterDinnerDir
            End If
        Else
            MsgBox("请选中需要修改的单项！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        If Me.DGVDinnertime.SelectedRows.Count = 1 Then
            Dim selectrow As DataGridViewRow = Me.DGVDinnertime.SelectedRows(0)
            Me.DGVDinnertime.Rows.Remove(selectrow)
        Else
            MsgBox("请选中需要修改的单项！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
    End Sub


    Public Sub New()
        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.DataGridView4.SelectedRows.Count = 1 Then
            Dim selectrow As DataGridViewRow = Me.DataGridView4.SelectedRows(0)
            Me.DataGridView4.Rows.Remove(selectrow)
        Else
            MsgBox("请选中需要修改的单项！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim frm As New FrmAddShitPlace(ListStation, ListJiaolu)
        frm.Text = "添加上下班参数"
        If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
            Dim DutyonJiaolu As String = frm.ComboBox1.Text.Trim
            Dim StartTime As String = frm.DateTimePicker1.Value.ToString("HH:mm:ss")
            Dim EndTime As String = frm.DateTimePicker2.Value.ToString("HH:mm:ss")
            Dim DutyonStation As String = frm.ComboBox2.Text.Trim
            Dim DutyonDirec As String = frm.ComboBox3.Text.Trim
            Dim Dutyoff As String = ""
            For i As Integer = 0 To frm.ListBox1.Items.Count - 1
                If i = frm.ListBox1.Items.Count - 1 Then
                    Dutyoff &= frm.ListBox1.Items(i)
                Else
                    Dutyoff &= frm.ListBox1.Items(i) & "|"
                End If
            Next
            Me.DataGridView4.Rows.Add((Me.DGVDinnertime.Rows.Count + 1).ToString, DutyonJiaolu, DutyonStation, DutyonDirec, StartTime, EndTime, Dutyoff)
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If Me.DataGridView4.SelectedRows.Count = 1 Then
            Dim frm As New FrmAddShitPlace(ListStation, ListJiaolu)
            frm.Text = "修改上下班参数"
            Dim selectrow As DataGridViewRow = Me.DataGridView4.SelectedRows(0)
            frm.ComboBox1.Text = selectrow.Cells("上班交路").Value.ToString.Trim
            frm.ComboBox2.Text = selectrow.Cells("上班地点").Value.ToString.Trim
            frm.DateTimePicker1.Value = CDate(Now.ToString("yyyy/MM/dd") & " " & selectrow.Cells("白班开始时间").Value.ToString.Trim)
            frm.DateTimePicker2.Value = CDate(Now.ToString("yyyy/MM/dd") & " " & selectrow.Cells("白班结束时间").Value.ToString.Trim)
            frm.ComboBox3.Text = selectrow.Cells("方向匹配").Value.ToString.Trim
            Dim dutyoff() As String = selectrow.Cells("可退勤地点").Value.ToString.Trim.Split("|")
            For i As Integer = 0 To dutyoff.Count - 1
                frm.ListBox1.Items.Add(dutyoff(i))
            Next

            If frm.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim DutyonJiaolu As String = frm.ComboBox1.Text.Trim
                Dim StartTime As String = frm.DateTimePicker1.Value.ToString("HH:mm:ss")
                Dim EndTime As String = frm.DateTimePicker2.Value.ToString("HH:mm:ss")
                Dim DutyonStation As String = frm.ComboBox2.Text.Trim
                Dim DutyonDirec As String = frm.ComboBox3.Text.Trim
                Dim DutyoffPosi As String = ""
                For i As Integer = 0 To frm.ListBox1.Items.Count - 1
                    If i = frm.ListBox1.Items.Count - 1 Then
                        DutyoffPosi &= frm.ListBox1.Items(i)
                    Else
                        DutyoffPosi &= frm.ListBox1.Items(i) & "|"
                    End If
                Next
                selectrow.Cells("上班交路").Value = DutyonJiaolu
                selectrow.Cells("白班开始时间").Value = StartTime
                selectrow.Cells("白班结束时间").Value = EndTime
                selectrow.Cells("上班地点").Value = DutyonStation
                selectrow.Cells("方向匹配").Value = DutyonDirec
                selectrow.Cells("可退勤地点").Value = DutyoffPosi
            End If
        Else
            MsgBox("请选中需要修改的单项！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
    End Sub
    Public Function SearchLeastRest(ByVal jiaolu As String, ByVal staName As String) As Integer()
        Dim listTrain As New List(Of typeTrainInformation)
        For i As Integer = 0 To CSTrainInf.Count - 1
            If CSTrainInf(i).sJiaoLuName = jiaolu Then
                listTrain.Add(CSTrainInf(i))
            End If
        Next
        For i As Integer = 0 To listTrain.Count - 2
            For j As Integer = i + 1 To listTrain.Count - 1
                If listTrain(i).Starting(listTrain(i).nPathID(1)) > listTrain(j).Starting(listTrain(j).nPathID(1)) Then
                    Dim sTmp As typeTrainInformation = listTrain(j)
                    listTrain(j) = listTrain(i)
                    listTrain(i) = sTmp
                End If
            Next
        Next
        Dim trainInterval As Integer = 100000
        Dim zhefanTime As Integer = 0
        Dim tmpNext As typeTrainInformation = Nothing
        For i As Integer = 0 To listTrain.Count - 2
            If trainInterval > listTrain(i + 1).Starting(listTrain(i + 1).nPathID(1)) - listTrain(i).Starting(listTrain(i).nPathID(1)) Then
                trainInterval = listTrain(i + 1).Starting(listTrain(i + 1).nPathID(1)) - listTrain(i).Starting(listTrain(i).nPathID(1))
                tmpNext = listTrain(i + 1)
            End If
        Next
        If IsNothing(tmpNext.Train) = False Then
            If StationInf(tmpNext.nPathID(UBound(tmpNext.nPathID))).sStationName = staName Then
                Dim getzhefantrain As Boolean = False
                For i As Integer = 1 To UBound(CSchediInfo)
                    For j As Integer = 1 To UBound(CSchediInfo(i).nLinkTrain)
                        If CSchediInfo(i).nLinkTrain(j) = tmpNext.nTrain And j < UBound(CSchediInfo(i).nLinkTrain) Then
                            zhefanTime = CSTrainInf(CSchediInfo(i).nLinkTrain(j + 1)).Arrival(CSTrainInf(CSchediInfo(i).nLinkTrain(j + 1)).nPathID(1)) - tmpNext.Starting(tmpNext.nPathID(UBound(tmpNext.nPathID))) - 1
                            getzhefantrain = True
                            Exit For
                        End If
                    Next
                    If getzhefantrain = True Then
                        Exit For
                    End If
                Next
            End If
        End If
        Dim result(1) As Integer
        result(0) = trainInterval
        result(1) = zhefanTime
        Return result
    End Function

    Private Sub DataGridView2_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellValueChanged
        If e.ColumnIndex = 7 And e.RowIndex > -1 Then
            Try
                If CInt(DataGridView2.Rows(e.RowIndex).Cells("轮换人数").Value) > 0 Then
                    DataGridView2.Rows(e.RowIndex).Cells("休息时间").Value = BeTime(CInt(DataGridView2.Rows(e.RowIndex).Cells("轮换人数").Value) * minRestList(e.RowIndex)(0) + minRestList(e.RowIndex)(1))
                End If
            Catch ex As Exception
                MsgBox("请在 轮换人数 单元格中填写数字！")
            End Try
        End If
    End Sub

    Private Sub ListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseDoubleClick
        If ListBox1.SelectedItem Is Nothing Then
            Exit Sub
        Else
            Dim id As Integer = 1
            If DataGridView3.Rows.Count > 0 Then
                For i As Integer = 0 To DataGridView3.Rows.Count - 1
                    If ListBox1.SelectedItem.ToString = DataGridView3.Rows(i).Cells(1).Value.ToString Then
                        If CInt(DataGridView3.Rows(i).Cells(2).Value.ToString) > id Then
                            id = CInt(DataGridView3.Rows(i).Cells(2).Value.ToString)
                        End If
                    End If
                Next
            End If
            DataGridView3.Rows.Add(1)
            DataGridView3.Rows(DataGridView3.Rows.Count - 1).Cells(0).Value = DataGridView3.Rows.Count
            DataGridView3.Rows(DataGridView3.Rows.Count - 1).Cells(1).Value = ListBox1.SelectedItem.ToString
            DataGridView3.Rows(DataGridView3.Rows.Count - 1).Cells(2).Value = (id + 1).ToString
            DataGridView3.Rows(DataGridView3.Rows.Count - 1).Cells(3).Value = ""
            Dim StartStationName = "", EndStationName = ""
            For j As Integer = 1 To UBound(BasicTrainInf)
                If BasicTrainInf(j).sJiaoLuName = ListBox1.SelectedItem.ToString Then
                    StartStationName = BasicTrainInf(j).StartStation
                    EndStationName = BasicTrainInf(j).EndStation
                    Exit For
                End If
            Next
            For j As Integer = 0 To ListBox1.Items.Count - 1
                If ListBox1.Items(j) = EndStationName + "-->" + StartStationName Then
                    DataGridView3.Rows(DataGridView3.Rows.Count - 1).Cells(3).Value = ListBox1.Items(j)
                    Exit For
                End If
            Next
        End If

    End Sub
End Class