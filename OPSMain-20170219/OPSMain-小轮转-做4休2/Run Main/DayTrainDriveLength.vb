Imports System.ComponentModel
Imports Microsoft.Office.Interop.Excel
Public Class DayTrainDriveLength : Inherits Report
    Dim _reportDate As Date
    Dim _people As String
    Public table As Data.DataTable
    Private _reporttype As ReportStyle

    Public Sub New()
        Me.Name = "车底日走行公里统计表"
        Me.ReportDate = Now.Date
        Me.ReportType = ReportStyle.载客
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

    Public Overrides Sub LoadData()
        MyBase.LoadData()
        Dim str As String

        If Me.ReportType = ReportStyle.载客 Then
            str = "select t.vehicleno as traincode,t.distance as drivelength from cs_crewschedule t,cs_corresponding m,cs_datetimetable x " & _
                    "where datediff('d',m.dateno,x.dateno)=0 and m.driverno=t.driverno and x.cstimetableid=t.cstimetableid and " & _
                    "datediff('d',m.dateno,Format('" & ReportDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))=0 order by t.vehicleno,t.endtime"
        Else
            Me.table = New Data.DataTable
            Me.table.Rows.Clear()
            Exit Sub
        End If

        Dim tab As New System.Data.DataTable
        tab = Globle.Method.ReadDataForAccess(str)

        tab.Columns("traincode").ColumnName = "列车号"
        tab.Columns("drivelength").ColumnName = "运营公里"
        Me.table = tab

        tab.Dispose()
    End Sub

    Public Overrides Sub DrawFrame()
        MyBase.DrawFrame()

        Dim str As String = "select t.traincode from (select t.vehicleno as traincode,t.distance as drivelength from cs_crewschedule t,cs_corresponding m,cs_datetimetable x " & _
                        "where datediff('d',m.dateno,x.dateno)=0 and m.driverno=t.driverno and x.cstimetableid=t.cstimetableid and " & _
                        "datediff('d',m.dateno,Format('" & ReportDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))=0 order by t.vehicleno,t.endtime) t group by t.traincode order by t.traincode"
        Dim tab As New System.Data.DataTable
        tab = Globle.Method.ReadDataForAccess(str)

        Dim tempstr As String = "select max(count(t.traincode)) as count from (select t.vehicleno as traincode,t.distance as drivelength from cs_crewschedule t,cs_corresponding m,cs_datetimetable x " & _
                        "where datediff('d',m.dateno,x.dateno)=0 and m.driverno=t.driverno and x.cstimetableid=t.cstimetableid and " & _
                        "datediff('d',m.dateno,Format('" & ReportDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))=0 order by t.vehicleno,t.endtime) t group by t.traincode order by t.traincode"


        Dim temptab As New System.Data.DataTable
        temptab = Globle.Method.ReadDataForAccess(tempstr)
        Dim Maxdutynum As Integer = temptab.Rows(0).Item(0)
        temptab.Dispose()

        Me.DataGrid = New CellGrid(4, 1, tab.Rows.Count * 2 + 2, Maxdutynum + 4)
        Me.DataGrid.Grid(0, 0).celltext = "列车号"
        Me.DataGrid.Grid(0, 1).celltext = "类型"
        Me.DataGrid.Grid(0, 2).celltext = "任务号"
        Me.DataGrid.Grid(0, Me.DataGrid.columns - 2).celltext = "日运营公里"
        Me.DataGrid.Grid(0, Me.DataGrid.columns - 1).celltext = "日走行公里"
        For i As Integer = 2 To Me.DataGrid.columns - 3
            Me.DataGrid.Grid(1, i).celltext = "任务" & (i - 1).ToString
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
        Me.DataGrid.NoteText = "日期:" & Me.ReportDate.ToString("yyyy/MM/dd")

        Me.DataGrid.FrameRang.Add(New CellRange(Me.DataGrid.Grid(0, 0), Me.DataGrid.Grid(1, Me.DataGrid.columns - 1)))
        tab.Dispose()
    End Sub

    Public Overrides Sub DrawData()
        MyBase.DrawData()
        Me.table.Dispose()
        Dim str As String = "select t.vehicleno as traincode,t.distance as drivelength,'' as isempty from cs_crewschedule t,cs_corresponding m,cs_datetimetable x " & _
                        "where datediff('d',m.dateno,x.dateno)=0 and m.driverno=t.driverno and x.cstimetableid=t.cstimetableid and " & _
                        "datediff('d',m.dateno,Format('" & ReportDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))=0 order by t.vehicleno,t.endtime"

        Dim tab As New System.Data.DataTable
        tab = Globle.Method.ReadDataForAccess(str)
        tab.Columns("traincode").ColumnName = "列车号"
        tab.Columns("drivelength").ColumnName = "运营公里"
        tab.Columns("isempty").ColumnName = "是否空载"

        Dim sumlist As New List(Of Decimal)
        Dim sumlist2 As New List(Of Decimal)
        Dim tempsum As Decimal = 0
        Dim tempsum2 As Decimal = 0
        Dim point As Integer = 0  'excel行坐标
        Dim count As Integer = 0  'excel载客列坐标
        Dim count2 As Integer = 0  'excel空车列坐标
        For i As Integer = 0 To tab.Rows.Count - 1
            If i = 0 Then
                If tab.Rows(i).Item("是否空载").ToString = "是" Then
                    count2 += 1
                    Me.DataGrid.Grid(point * 2 + 3, count2 + 1).celltext = tab.Rows(i).Item("运营公里").ToString
                    tempsum2 += CDec(tab.Rows(i).Item("运营公里"))
                Else
                    count += 1
                    Me.DataGrid.Grid(point * 2 + 2, count + 1).celltext = tab.Rows(i).Item("运营公里").ToString
                    tempsum += CDec(tab.Rows(i).Item("运营公里"))
                End If
            Else
                If tab.Rows(i).Item("列车号") = tab.Rows(i - 1).Item("列车号") Then
                    If tab.Rows(i).Item("是否空载").ToString = "是" Then
                        count2 += 1
                        Me.DataGrid.Grid(point * 2 + 3, count2 + 1).celltext = tab.Rows(i).Item("运营公里")
                        tempsum2 += CDec(tab.Rows(i).Item("运营公里"))
                    Else
                        count += 1
                        Me.DataGrid.Grid(point * 2 + 2, count + 1).celltext = tab.Rows(i).Item("运营公里")
                        tempsum += CDec(tab.Rows(i).Item("运营公里"))
                    End If
                Else
                    Me.DataGrid.Grid(point * 2 + 2, Me.DataGrid.columns - 2).celltext = tempsum.ToString
                    Me.DataGrid.Grid(point * 2 + 3, Me.DataGrid.columns - 2).celltext = tempsum2.ToString
                    Me.DataGrid.Grid(point * 2 + 2, Me.DataGrid.columns - 1).celltext = (tempsum + tempsum2).ToString
                    point += 1
                    count = 0
                    count2 = 0
                    tempsum = 0
                    tempsum2 = 0
                    If tab.Rows(i).Item("是否空载").ToString = "是" Then
                        count2 += 1
                        Me.DataGrid.Grid(point * 2 + 3, count2 + 1).celltext = tab.Rows(i).Item("运营公里").ToString
                        tempsum2 += CDec(tab.Rows(i).Item("运营公里"))
                    Else
                        count += 1
                        Me.DataGrid.Grid(point * 2 + 2, count + 1).celltext = tab.Rows(i).Item("运营公里").ToString
                        tempsum += CDec(tab.Rows(i).Item("运营公里"))
                    End If
                End If
            End If
        Next
        Me.DataGrid.Grid(point * 2 + 2, Me.DataGrid.columns - 2).celltext = tempsum.ToString
        Me.DataGrid.Grid(point * 2 + 3, Me.DataGrid.columns - 2).celltext = tempsum2.ToString
        Me.DataGrid.Grid(point * 2 + 2, Me.DataGrid.columns - 1).celltext = (tempsum + tempsum2).ToString
        Me.DataGrid.FillZeros = False
        tab.Dispose()

    End Sub
End Class
