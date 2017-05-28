Imports System.ComponentModel

Public Class SpanDriveLength : Inherits Report

    Dim _reportStartDate As Date
    Dim _reportEndDate As Date
    Dim _people As String
    Public TeamsDrivers As List(Of List(Of RDriver))
    Public DriverNum As Integer

    Public Sub New(ByVal DriversTeams As List(Of List(Of RDriver)))
        Me.Name = "司机阶段行驶公里统计表"
        Me.StartReportDate = Now.Date
        Me.EndReportDate = Now.AddDays(6).Date
        Me.TeamsDrivers = DriversTeams

        DriverNum = 0
        For Each team As List(Of RDriver) In TeamsDrivers
            For Each rdr As RDriver In team
                DriverNum += 1
            Next
        Next
    End Sub

    <DisplayName("开始日期"), Category("日期"), Description("设定报表日期")> _
    Public Property StartReportDate() As Date
        Get
            Return _reportStartDate
        End Get
        Set(ByVal value As Date)
            If value <> _reportStartDate Then
                _reportStartDate = value.Date
            End If
        End Set
    End Property

    <DisplayName("终到日期"), Category("日期"), Description("设定报表日期")> _
    Public Property EndReportDate() As Date
        Get
            Return _reportEndDate
        End Get
        Set(ByVal value As Date)
            If value <> _reportEndDate Then
                _reportEndDate = value.Date
            End If
        End Set
    End Property

    <Browsable(False)> _
    Public ReadOnly Property DateSpan() As TimeSpan
        Get
            Return Me.EndReportDate.AddDays(1) - Me.StartReportDate
        End Get
    End Property

    Public Overrides Sub LoadData()
        MyBase.LoadData()

        Dim nf As New FrmProgress(DriverNum, "正在加载数据....")
        For Each team As List(Of RDriver) In TeamsDrivers
            For Each rdr As RDriver In team
                rdr.CSDriverList.Clear()
                rdr.ToTalDriveDistance = 0
                rdr.ToTalDriveTime = 0
                rdr.TotalWorkTime = 0
                For i As Integer = 0 To Me.DateSpan.Days - 1
                    rdr.LoadCSDriver(Me.StartReportDate.AddDays(i).ToString("yyyy/MM/dd"), rdr.RDriverNo)
                    'If rdr.CSDriverList.Count = 0 Then
                    '    nf.Close()
                    '    Exit Sub
                    'End If
                Next
                nf.Performstep()
            Next
        Next
        nf.Close()

    End Sub

    Public Overrides Sub DrawFrame()
        MyBase.DrawFrame()

        Me.DataGrid = New CellGrid(4, 1, DriverNum + 2, Me.DateSpan.Days + 5)
        Me.DataGrid.Grid(0, 0).celltext = "线路编号"
        Me.DataGrid.Grid(0, 1).celltext = "司机编号"
        Me.DataGrid.Grid(0, 2).celltext = "司机姓名"
        Me.DataGrid.Grid(0, 3).celltext = "日驾驶里程"
        Me.DataGrid.Grid(0, Me.DataGrid.columns - 2).celltext = "总驾驶里程"
        Me.DataGrid.Grid(0, Me.DataGrid.columns - 1).celltext = "公里补贴"
        For i As Integer = 3 To Me.DataGrid.columns - 3
            Me.DataGrid.Grid(1, i).celltext = Me.StartReportDate.AddDays(i - 3).ToString("yyyy/MM/dd")
        Next
        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(0, 3), Me.DataGrid.Grid(0, Me.DataGrid.columns - 3)))
        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(0, 0), Me.DataGrid.Grid(1, 0)))
        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(0, 1), Me.DataGrid.Grid(1, 1)))
        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(0, 2), Me.DataGrid.Grid(1, 2)))
        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(0, Me.DataGrid.columns - 1), Me.DataGrid.Grid(1, Me.DataGrid.columns - 1)))
        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(0, Me.DataGrid.columns - 2), Me.DataGrid.Grid(1, Me.DataGrid.columns - 2)))
        Me.DataGrid.MergeList.Add(New CellRange(New ExcelCell(3, 1), New ExcelCell(3, Me.DataGrid.columns)))

        Me.DataGrid.HeadText = Me.Name
        Me.DataGrid.HeadRang.Add(New CellRange(New ExcelCell(2, 1), New ExcelCell(2, Me.DataGrid.columns)))
        Me.DataGrid.StrTypeList.Add(New CellRange(Me.DataGrid.Grid(0, 1), Me.DataGrid.Grid(Me.DataGrid.rows - 1, 1)))
        Me.DataGrid.NoteText = "阶段日期:" & Me.StartReportDate.ToString("yyyy/MM/dd") & "-" & Me.EndReportDate.ToString("yyyy/MM/dd")

        Me.DataGrid.FrameRang.Add(New CellRange(Me.DataGrid.Grid(0, 0), Me.DataGrid.Grid(1, Me.DataGrid.columns - 1)))

    End Sub

    Public Overrides Sub DrawData()
        MyBase.DrawData()

        Dim point As Integer = 0

        For Each team As List(Of RDriver) In TeamsDrivers
            For Each rdr As RDriver In team
                If rdr.CSDriverList.Count > 0 Then
                    Me.DataGrid.Grid(point + 2, 0).celltext = rdr.LineId
                    Me.DataGrid.Grid(point + 2, 1).celltext = rdr.RDriverNo
                    Me.DataGrid.Grid(point + 2, 2).celltext = rdr.DriverName
                    For i As Integer = 0 To rdr.CSDriverList.Count - 1
                        Me.DataGrid.Grid(point + 2, 3 + i).celltext = rdr.CSDriverList(i).TotalDriverDistance
                    Next
                    Me.DataGrid.Grid(point + 2, Me.DataGrid.columns - 2).celltext = rdr.ToTalDriveDistance
                    Me.DataGrid.Grid(point + 2, Me.DataGrid.columns - 1).celltext = rdr.ToTalDriveDistance / 10
                    point += 1
                Else
                    Exit Sub
                End If
            Next
        Next

    End Sub

End Class
