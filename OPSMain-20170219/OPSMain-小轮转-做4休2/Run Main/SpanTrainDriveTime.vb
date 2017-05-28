Imports System.ComponentModel
Imports Microsoft.Office.Interop.Excel
Public Class SpanTrainDriveTime : Inherits Report
    Dim _reportStartDate As Date
    Dim _reportEndDate As Date
    Dim _reporttype As ReportStyle
    Public table As Data.DataTable

    Public Sub New()
        Me.Name = "车底阶段走行时间统计表"
        Me.StartReportDate = Now.Date
        Me.EndReportDate = Now.AddDays(6).Date
        Me.ReportType = ReportStyle.载客
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

    <DisplayName("报表类型"), Category("种类"), Description("设定报表类型")> _
    Public Property ReportType() As ReportStyle
        Get
            Return _reporttype
        End Get
        Set(ByVal value As ReportStyle)
            If value <> _reporttype Then
                _reporttype = value
            End If
        End Set
    End Property

    <Browsable(False)> _
    Public Property DateSpan() As TimeSpan
        Get
            Return Me.EndReportDate.AddDays(1) - Me.StartReportDate
        End Get
        Set(ByVal value As TimeSpan)
        End Set
    End Property

    Public Overrides Sub LoadData()
        MyBase.LoadData()

        Dim str As String
        If Me.ReportType = ReportStyle.空车 Then
            Me.table = New Data.DataTable
            Me.table.Rows.Clear()
            Exit Sub
        Else
            str = "select t.traincode,sum(t.drivetime) as drivetime,t.dutydate from (select t.vehicleno as traincode,iif((val(t.endtime)-val(t.starttime))<0,(val(t.endtime)-val(t.starttime))+86400,(val(t.endtime)-val(t.starttime))) as drivetime,m.dateno as dutydate from cs_crewschedule t,cs_corresponding m,cs_datetimetable x " & _
                    "where datediff('d',m.dateno,x.dateno)=0 and m.driverno=t.driverno and x.cstimetableid=t.cstimetableid and " & _
                    "datediff('d',m.dateno,Format('" & StartReportDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 and datediff('d',m.dateno,Format('" & EndReportDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>=0 order by t.vehicleno,t.endtime) t group by t.dutydate,t.traincode order by t.traincode,t.dutydate"
        End If
        Dim tab As New System.Data.DataTable
        tab = Globle.Method.ReadDataForAccess(str)
     

        tab.Columns("traincode").ColumnName = "列车号"
        tab.Columns("drivetime").ColumnName = "运营时间"
        tab.Columns("dutydate").ColumnName = "日期"
        table = tab
        tab.Dispose()

    End Sub

    Public Overrides Sub DrawFrame()
        MyBase.DrawFrame()

        Dim str As String = "select t.traincode from (select t.vehicleno as traincode from cs_crewschedule t,cs_corresponding m,cs_datetimetable x " & _
                        "where datediff('d',m.dateno,x.dateno)=0 and m.driverno=t.driverno and x.cstimetableid=t.cstimetableid and " & _
                        "datediff('d',m.dateno,Format('" & StartReportDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 and datediff('d',m.dateno,Format('" & EndReportDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>=0 order by t.vehicleno,t.endtime) t group by t.traincode order by t.traincode"

       
        Dim tab As New System.Data.DataTable
        tab = Globle.Method.ReadDataForAccess(str)
       

        Me.DataGrid = New CellGrid(4, 1, tab.Rows.Count * 2 + 2, Me.DateSpan.Days + 4)
        Me.DataGrid.Grid(0, 0).celltext = "列车号"
        Me.DataGrid.Grid(0, 1).celltext = "类型"
        Me.DataGrid.Grid(0, 2).celltext = "日期"
        Me.DataGrid.Grid(0, Me.DataGrid.columns - 2).celltext = "总运营时间"
        Me.DataGrid.Grid(0, Me.DataGrid.columns - 1).celltext = "总走行时间"
        For i As Integer = 2 To Me.DataGrid.columns - 3
            Me.DataGrid.Grid(1, i).celltext = Me.StartReportDate.AddDays(i - 2).ToString("yyyy/MM/dd")
        Next

        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(0, 2), Me.DataGrid.Grid(0, Me.DataGrid.columns - 3)))
        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(0, 0), Me.DataGrid.Grid(1, 0)))
        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(0, 1), Me.DataGrid.Grid(1, 1)))
        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(0, Me.DataGrid.columns - 2), Me.DataGrid.Grid(1, Me.DataGrid.columns - 2)))
        Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(0, Me.DataGrid.columns - 1), Me.DataGrid.Grid(1, Me.DataGrid.columns - 1)))
        Me.DataGrid.MergeList.Add(New CellRange(New ExcelCell(3, 1), New ExcelCell(3, Me.DataGrid.columns)))

        For i As Integer = 0 To tab.Rows.Count - 1
            Me.DataGrid.Grid(2 * i + 2, 0).celltext = tab.Rows(i).Item(0).ToString()
            Me.DataGrid.Grid(2 * i + 2, 1).celltext = "载客"
            Me.DataGrid.Grid(2 * i + 3, 1).celltext = "空车"
            Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(2 * i + 2, 0), Me.DataGrid.Grid(2 * i + 3, 0)))
            Me.DataGrid.MergeList.Add(New CellRange(Me.DataGrid.Grid(2 * i + 2, Me.DataGrid.columns - 1), Me.DataGrid.Grid(2 * i + 3, Me.DataGrid.columns - 1)))
        Next

        Me.DataGrid.HeadText = Me.Name
        Me.DataGrid.HeadRang.Add(New CellRange(New ExcelCell(2, 1), New ExcelCell(2, Me.DataGrid.columns)))
        Me.DataGrid.NoteText = "阶段日期:" & Me.StartReportDate.ToString("yyyy/MM/dd") & "-" & Me.EndReportDate.ToString("yyyy/MM/dd")

        Me.DataGrid.FrameRang.Add(New CellRange(Me.DataGrid.Grid(0, 0), Me.DataGrid.Grid(1, Me.DataGrid.columns - 1)))
        tab.Dispose()

    End Sub

    Public Overrides Sub DrawData()
        MyBase.DrawData()

        Me.table.Dispose()
        Dim str As String = "select t.traincode,sum(t.drivetime) as peopletime,'' as emptytime,t.dutydate from (select t.vehicleno as traincode,iif((val(t.endtime)-val(t.starttime))<0 ,(val(t.endtime)-val(t.starttime))+86400 ,(val(t.endtime)-val(t.starttime))) as drivetime,m.dateno as dutydate from cs_crewschedule t,cs_corresponding m,cs_datetimetable x " & _
                        "where datediff('d',m.dateno,x.dateno)=0 and m.driverno=t.driverno and x.cstimetableid=t.cstimetableid and " & _
                        "datediff('d',m.dateno,Format('" & StartReportDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 and datediff('d',m.dateno,Format('" & EndReportDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>=0 order by t.vehicleno,t.endtime) t group by t.dutydate,t.traincode order by t.traincode,t.dutydate"

        Dim tab As New System.Data.DataTable
        tab = Globle.Method.ReadDataForAccess(str)
        tab.Columns("traincode").ColumnName = "列车号"
        tab.Columns("peopletime").ColumnName = "载客时间"
        tab.Columns("emptytime").ColumnName = "空车时间"
        tab.Columns("dutydate").ColumnName = "日期"

        Dim point As Integer = 0
        Dim sum1 As Decimal = 0
        Dim sum2 As Decimal = 0
        For i As Integer = 0 To tab.Rows.Count - 1
            If i = 0 Then
                If tab.Rows(i).Item("载客时间").ToString = "" Then
                    Me.DataGrid.Grid(point * 2 + 2, 1 + (CDate(tab.Rows(i).Item("日期")).AddDays(1) - Me.StartReportDate).Days).celltext = tab.Rows(i).Item("载客时间").ToString
                Else
                    Me.DataGrid.Grid(point * 2 + 2, 1 + (CDate(tab.Rows(i).Item("日期")).AddDays(1) - Me.StartReportDate).Days).celltext = Coordination2.Global.BeTime(CInt(tab.Rows(i).Item("载客时间")))
                End If
                If tab.Rows(i).Item("空车时间").ToString = "" Then
                    Me.DataGrid.Grid(point * 2 + 3, 1 + (CDate(tab.Rows(i).Item("日期")).AddDays(1) - Me.StartReportDate).Days).celltext = tab.Rows(i).Item("空车时间").ToString
                Else
                    Me.DataGrid.Grid(point * 2 + 3, 1 + (CDate(tab.Rows(i).Item("日期")).AddDays(1) - Me.StartReportDate).Days).celltext = Coordination2.Global.BeTime(CInt(tab.Rows(i).Item("空车时间")))
                End If
                If tab.Rows(i).Item("载客时间").ToString = "" Then
                    sum1 += 0
                Else
                    sum1 += CInt(tab.Rows(i).Item("载客时间"))
                End If
                If tab.Rows(i).Item("空车时间").ToString = "" Then
                    sum2 += 0
                Else
                    sum2 += CInt(tab.Rows(i).Item("空车时间"))
                End If

            Else
                If tab.Rows(i).Item("列车号") <> tab.Rows(i - 1).Item("列车号") Then
                    Me.DataGrid.Grid(point * 2 + 2, Me.DataGrid.columns - 2).celltext = Coordination2.Global.BeTime(sum1)
                    Me.DataGrid.Grid(point * 2 + 3, Me.DataGrid.columns - 2).celltext = Coordination2.Global.BeTime(sum2)
                    Me.DataGrid.Grid(point * 2 + 2, Me.DataGrid.columns - 1).celltext = Coordination2.Global.BeTime(sum1 + sum2)

                    point += 1
                    sum1 = 0
                    sum2 = 0
                    If tab.Rows(i).Item("载客时间").ToString = "" Then
                        Me.DataGrid.Grid(point * 2 + 2, 1 + (CDate(tab.Rows(i).Item("日期")).AddDays(1) - Me.StartReportDate).Days).celltext = tab.Rows(i).Item("载客时间").ToString
                    Else
                        Me.DataGrid.Grid(point * 2 + 2, 1 + (CDate(tab.Rows(i).Item("日期")).AddDays(1) - Me.StartReportDate).Days).celltext = Coordination2.Global.BeTime(CInt(tab.Rows(i).Item("载客时间")))
                    End If
                    If tab.Rows(i).Item("空车时间").ToString = "" Then
                        Me.DataGrid.Grid(point * 2 + 3, 1 + (CDate(tab.Rows(i).Item("日期")).AddDays(1) - Me.StartReportDate).Days).celltext = tab.Rows(i).Item("空车时间").ToString
                    Else
                        Me.DataGrid.Grid(point * 2 + 3, 1 + (CDate(tab.Rows(i).Item("日期")).AddDays(1) - Me.StartReportDate).Days).celltext = Coordination2.Global.BeTime(CInt(tab.Rows(i).Item("空车时间")))
                    End If
                    If tab.Rows(i).Item("载客时间").ToString = "" Then
                        sum1 += 0
                    Else
                        sum1 += CInt(tab.Rows(i).Item("载客时间"))
                    End If
                    If tab.Rows(i).Item("空车时间").ToString = "" Then
                        sum2 += 0
                    Else
                        sum2 += CInt(tab.Rows(i).Item("空车时间"))
                    End If
                Else
                    If tab.Rows(i).Item("载客时间").ToString = "" Then
                        Me.DataGrid.Grid(point * 2 + 2, 1 + (CDate(tab.Rows(i).Item("日期")).AddDays(1) - Me.StartReportDate).Days).celltext = tab.Rows(i).Item("载客时间").ToString
                    Else
                        Me.DataGrid.Grid(point * 2 + 2, 1 + (CDate(tab.Rows(i).Item("日期")).AddDays(1) - Me.StartReportDate).Days).celltext = Coordination2.Global.BeTime(CInt(tab.Rows(i).Item("载客时间")))
                    End If
                    If tab.Rows(i).Item("空车时间").ToString = "" Then
                        Me.DataGrid.Grid(point * 2 + 3, 1 + (CDate(tab.Rows(i).Item("日期")).AddDays(1) - Me.StartReportDate).Days).celltext = tab.Rows(i).Item("空车时间").ToString
                    Else
                        Me.DataGrid.Grid(point * 2 + 3, 1 + (CDate(tab.Rows(i).Item("日期")).AddDays(1) - Me.StartReportDate).Days).celltext = Coordination2.Global.BeTime(CInt(tab.Rows(i).Item("空车时间")))
                    End If
                    If tab.Rows(i).Item("载客时间").ToString = "" Then
                        sum1 += 0
                    Else
                        sum1 += CInt(tab.Rows(i).Item("载客时间"))
                    End If
                    If tab.Rows(i).Item("空车时间").ToString = "" Then
                        sum2 += 0
                    Else
                        sum2 += CInt(tab.Rows(i).Item("空车时间"))
                    End If
                End If
            End If
        Next
        Me.DataGrid.Grid(point * 2 + 2, Me.DataGrid.columns - 2).celltext = Coordination2.Global.BeTime(sum1)
        Me.DataGrid.Grid(point * 2 + 3, Me.DataGrid.columns - 2).celltext = Coordination2.Global.BeTime(sum2)
        Me.DataGrid.Grid(point * 2 + 2, Me.DataGrid.columns - 1).celltext = Coordination2.Global.BeTime(sum1 + sum2)
    End Sub

End Class
