Imports System.ComponentModel

Public Enum ReportStyle
    空车 = 1
    载客 = 2
End Enum

Public Class DayDriveTime : Inherits Report
    Dim _reportDate As Date
    Dim _people As String
    Public TeamsDrivers As List(Of List(Of RDriver))
    Public DriverNum As Integer
    Public MaxDutyNum As Integer

    Public Sub New(ByVal DriversTeams As List(Of List(Of RDriver)))
        Me.Name = "司机日驾驶时间统计表"
        Me.ReportDate = Now.Date
        Me.TeamsDrivers = DriversTeams

        DriverNum = 0
        For Each team As List(Of RDriver) In TeamsDrivers
            For Each rdr As RDriver In team
                DriverNum += 1
            Next
        Next
    End Sub

    <DisplayName("报表日期"), Category("日期"), Description("设定报表日期")> _
    Public Property ReportDate() As Date
        Get
            Return _reportDate
        End Get
        Set(ByVal value As Date)
            If value <> _reportDate Then
                _reportDate = value.Date
            End If
        End Set
    End Property

    Public Overrides Sub LoadData()
        MyBase.LoadData()
        Dim nf As New FrmProgress(DriverNum, "正在加载数据....")
        MaxDutyNum = 0
        For Each team As List(Of RDriver) In TeamsDrivers
            For Each rdr As RDriver In team
                rdr.CSDriverList.Clear()
                rdr.ToTalDriveDistance = 0
                rdr.ToTalDriveTime = 0
                rdr.TotalWorkTime = 0
                rdr.LoadCSDriver(Me.ReportDate.ToString("yyyy/MM/dd"), rdr.RDriverNo)
                If rdr.CSDriverList.Count > 0 Then
                    If rdr.CSDriverList(0).LinkDutyList.Count > MaxDutyNum Then
                        MaxDutyNum = rdr.CSDriverList(0).LinkDutyList.Count
                    End If
                Else
                    nf.Close()
                    Exit Sub
                End If
                nf.Performstep()
            Next
        Next
        nf.Close()
    End Sub

    Public Overrides Sub DrawFrame()
        MyBase.DrawFrame()

        Me.DataGrid = New CellGrid(4, 1, DriverNum + 2, MaxDutyNum + 4)
        Me.DataGrid.Grid(0, 0).celltext = "线路编号"
        Me.DataGrid.Grid(0, 1).celltext = "司机编号"
        Me.DataGrid.Grid(0, 2).celltext = "司机姓名"
        Me.DataGrid.Grid(0, 3).celltext = "任务号"
        Me.DataGrid.Grid(0, Me.DataGrid.columns - 1).celltext = "日驾驶工时"
        For i As Integer = 3 To Me.DataGrid.columns - 2
            Me.DataGrid.Grid(1, i).celltext = "任务" & (i - 2).ToString
        Next
        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(0, 3), Me.DataGrid.Grid(0, Me.DataGrid.columns - 2)))
        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(0, 0), Me.DataGrid.Grid(1, 0)))
        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(0, 1), Me.DataGrid.Grid(1, 1)))
        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(0, 2), Me.DataGrid.Grid(1, 2)))
        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(0, Me.DataGrid.columns - 1), Me.DataGrid.Grid(1, Me.DataGrid.columns - 1)))
        Me.DataGrid.MergeList.Add(New CellRange(New ExcelCell(3, 1), New ExcelCell(3, Me.DataGrid.columns)))

        Me.DataGrid.HeadText = Me.Name
        Me.DataGrid.HeadRang.Add(New CellRange(New ExcelCell(2, 1), New ExcelCell(2, Me.DataGrid.columns)))
        Me.DataGrid.StrTypeList.Add(New CellRange(Me.DataGrid.Grid(0, 1), Me.DataGrid.Grid(Me.DataGrid.rows - 1, 1)))
        Me.DataGrid.NoteText = "日期:" & Me.ReportDate.ToString("yyyy/MM/dd")

        Me.DataGrid.FrameRang.Add(New CellRange(Me.DataGrid.Grid(0, 0), Me.DataGrid.Grid(1, Me.DataGrid.columns - 1)))
    End Sub

    Public Overrides Sub DrawData()
        MyBase.DrawData()
        Dim point As Integer = 0  'excel行坐标

        For Each team As List(Of RDriver) In TeamsDrivers
            For Each rdr As RDriver In team
                If rdr.CSDriverList.Count > 0 Then
                    Me.DataGrid.Grid(point + 2, 0).celltext = rdr.LineId
                    Me.DataGrid.Grid(point + 2, 1).celltext = rdr.RDriverNo
                    Me.DataGrid.Grid(point + 2, 2).celltext = rdr.DriverName
                    If rdr.CSDriverList(0).LinkDutyList.Count > 0 Then
                        For i As Integer = 0 To rdr.CSDriverList(0).LinkDutyList.Count - 1
                            Me.DataGrid.Grid(point + 2, 3 + i).celltext = Coordination2.Global.BeTime(rdr.CSDriverList(0).LinkDutyList(i).Drivetime)
                        Next
                    Else
                        If rdr.CSDriverList(0).DutySort = "休息" OrElse rdr.CSDriverList(0).DutySort = "休假" OrElse rdr.CSDriverList(0).DutySort = "培训" Then
                            Me.DataGrid.Grid(point + 2, 3).celltext = rdr.CSDriverList(0).DutySort
                        ElseIf rdr.CSDriverList(0).DriverNo.Contains("备班") Then
                            Me.DataGrid.Grid(point + 2, 3).celltext = "备班"
                        ElseIf rdr.CSDriverList(0).DriverNo.Contains("备车") Then
                            Me.DataGrid.Grid(point + 2, 3).celltext = "备车"
                        Else
                            Me.DataGrid.Grid(point + 2, 3).celltext = "无任务"
                        End If
                        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(point + 2, 3), Me.DataGrid.Grid(point + 2, Me.DataGrid.columns - 2)))
                    End If
                    Me.DataGrid.Grid(point + 2, Me.DataGrid.columns - 1).celltext = Coordination2.Global.BeTime(rdr.CSDriverList(0).TotalWorkTime)
                    point += 1
                Else
                    Exit Sub
                End If
            Next
        Next

        Me.DataGrid.FillZeros = False
    End Sub
End Class
