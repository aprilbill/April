Imports Microsoft.Office.Interop.Excel

Public Class CellGrid
    Public Titlefont As System.Drawing.Font
    Public Notefont As System.Drawing.Font
    Public Datafont As System.Drawing.Font

    Public Grid(,) As ExcelCell
    Public MergeList As List(Of CellRange)
    Public StrTypeList As List(Of CellRange)
    Public HeadRang As List(Of CellRange)
    Public FrameRang As List(Of CellRange)
    Public rows As Integer
    Public columns As Integer
    Public startrow As Integer
    Public startcolumn As Integer
    Public HeadText As String
    Public NoteText As String
    Public FillZeros As Boolean

    Public Sub New(ByVal _startrow As Integer, ByVal _startcolumn As Integer, ByVal _rows As Integer, ByVal _columns As Integer)
        MergeList = New List(Of CellRange)()
        HeadRang = New List(Of CellRange)()
        FrameRang = New List(Of CellRange)()
        StrTypeList = New List(Of CellRange)()
        HeadText = ""
        NoteText = ""
        Titlefont = New System.Drawing.Font("黑体", 16)
        Notefont = New System.Drawing.Font("宋体", 9)
        Datafont = New System.Drawing.Font("宋体", 10)
        FillZeros = True

        rows = _rows
        columns = _columns
        startrow = _startrow
        startcolumn = _startcolumn
        ReDim Grid(rows - 1, columns - 1)
        For i As Integer = 1 To Me.rows
            For j As Integer = 1 To columns
                Grid(i - 1, j - 1) = New ExcelCell(i + startrow - 1, j + startcolumn - 1)
            Next
        Next

    End Sub

    Public Sub DrawTotaltoExcel(ByVal sheet As Worksheet)

        For Each ra As CellRange In Me.StrTypeList
            sheet.Range(sheet.Cells(ra.startCell.row, ra.startCell.column), sheet.Cells(ra.endCell.row, ra.endCell.column)).NumberFormat = "@"
        Next

        sheet.Cells(2, 1) = Me.HeadText
        sheet.Cells(3, 1) = Me.NoteText
        sheet.Range(sheet.Cells(Me.HeadRang(0).startCell.row, Me.HeadRang(0).startCell.column), sheet.Cells(Me.HeadRang(0).endCell.row, Me.HeadRang(0).endCell.column)).Merge()
        sheet.Range(sheet.Cells(Me.HeadRang(0).startCell.row, Me.HeadRang(0).startCell.column), sheet.Cells(Me.HeadRang(0).endCell.row, Me.HeadRang(0).endCell.column)).Font.Size = Titlefont.Size
        sheet.Range(sheet.Cells(Me.HeadRang(0).startCell.row, Me.HeadRang(0).startCell.column), sheet.Cells(Me.HeadRang(0).endCell.row, Me.HeadRang(0).endCell.column)).Font.Name = Titlefont.Name
        sheet.Range(sheet.Cells(Me.HeadRang(0).startCell.row, Me.HeadRang(0).startCell.column), sheet.Cells(Me.HeadRang(0).endCell.row, Me.HeadRang(0).endCell.column)).HorizontalAlignment = XlHAlign.xlHAlignCenter
        sheet.Range(sheet.Cells(Me.HeadRang(0).startCell.row, Me.HeadRang(0).startCell.column), sheet.Cells(Me.HeadRang(0).endCell.row, Me.HeadRang(0).endCell.column)).VerticalAlignment = XlHAlign.xlHAlignCenter
        sheet.Range(sheet.Cells(3, 1), sheet.Cells(3, Me.columns)).Font.Size = Notefont.Size
        sheet.Range(sheet.Cells(3, 1), sheet.Cells(3, Me.columns)).HorizontalAlignment = XlHAlign.xlHAlignLeft
        sheet.Range(sheet.Cells(3, 1), sheet.Cells(3, Me.columns)).VerticalAlignment = XlHAlign.xlHAlignCenter

        For Each ra As CellRange In Me.MergeList
            sheet.Range(sheet.Cells(ra.startCell.row, ra.startCell.column), sheet.Cells(ra.endCell.row, ra.endCell.column)).Merge()
        Next

        For Each ra As CellRange In Me.FrameRang
            sheet.Range(sheet.Cells(ra.startCell.row, ra.startCell.column), sheet.Cells(ra.endCell.row, ra.endCell.column)).Interior.ColorIndex = 20
        Next

        Dim nf As FrmProgress = New FrmProgress(Me.columns * Me.rows, "正在导出到Excel...")
        For i As Integer = 0 To Me.rows - 1
            For j As Integer = 0 To Me.columns - 1
                If Me.Grid(i, j).celltext <> "" Then
                    sheet.Cells(Me.Grid(i, j).row, Me.Grid(i, j).column) = Me.Grid(i, j).celltext.ToString
                Else
                    If Me.FillZeros = True Then
                        sheet.Cells(Me.Grid(i, j).row, Me.Grid(i, j).column) = "0"
                    End If
                End If
                nf.Performstep()
            Next
        Next
        nf.EndProgress()

        sheet.Range(sheet.Cells(Me.Grid(0, 0).row, Me.Grid(0, 0).column), sheet.Cells(Me.Grid(Me.rows - 1, Me.columns - 1).row, Me.Grid(Me.rows - 1, Me.columns - 1).column)).Borders.LineStyle = 1
        sheet.Range(sheet.Cells(Me.Grid(0, 0).row, Me.Grid(0, 0).column), sheet.Cells(Me.Grid(Me.rows - 1, Me.columns - 1).row, Me.Grid(Me.rows - 1, Me.columns - 1).column)).Font.Size = Datafont.Size
        sheet.Range(sheet.Cells(Me.Grid(0, 0).row, Me.Grid(0, 0).column), sheet.Cells(Me.Grid(Me.rows - 1, Me.columns - 1).row, Me.Grid(Me.rows - 1, Me.columns - 1).column)).Font.Name = Datafont.Name
        sheet.Range(sheet.Cells(Me.Grid(0, 0).row, Me.Grid(0, 0).column), sheet.Cells(Me.Grid(Me.rows - 1, Me.columns - 1).row, Me.Grid(Me.rows - 1, Me.columns - 1).column)).HorizontalAlignment = XlHAlign.xlHAlignCenter
        sheet.Range(sheet.Cells(Me.Grid(0, 0).row, Me.Grid(0, 0).column), sheet.Cells(Me.Grid(Me.rows - 1, Me.columns - 1).row, Me.Grid(Me.rows - 1, Me.columns - 1).column)).VerticalAlignment = XlHAlign.xlHAlignCenter
    End Sub

End Class

Public Class RDriver             '真实的乘务员
    Public LineId As String
    Public Beclass As String
    Public Beteam As String
    Public RDriverNo As String
    Public DriverName As String
    Public TotalWorkTime As Integer              '工作指标
    Public ToTalDriveTime As Integer
    Public ToTalDriveDistance As Integer
    Public CSDriverList As List(Of CSDriver)
    Public Sub New(ByVal LN As String, ByVal BC As String, ByVal BT As String, ByVal RDN As String, ByVal DNAME As String)
        CSDriverList = New List(Of CSDriver)
        Beclass = BC
        Beteam = BT
        RDriverNo = RDN
        DriverName = DNAME
        LineId = LN
        ToTalDriveDistance = 0
        ToTalDriveTime = 0
        TotalWorkTime = 0
    End Sub

    Public Sub LoadCSDriver(ByVal datestr As String, ByVal rdriverno As String)           '加载任务
        Dim tempcsdriver As New CSDriver
        Dim driverno As String
        Dim durysort As String

        Dim str As String
        str = "select * from cs_corresponding t where datediff('d',t.dateno,Format('" & datestr & "','yyyy/MM/dd'))=0 and t.rdriverno='" & rdriverno & "'"
        Dim tab As Data.DataTable = Globle.Method.ReadDataForAccess(str)

        If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
            driverno = tab.Rows(0).Item("driverno").ToString
            durysort = tab.Rows(0).Item("dutysort").ToString
            If durysort = "休息" OrElse durysort = "休假" OrElse durysort = "培训" Then
                tempcsdriver.DutySort = durysort
                tempcsdriver.DriverNo = durysort
            Else
                tempcsdriver.DriverNo = driverno
                tempcsdriver.DutySort = durysort
                If driverno.Contains("备") Then
                    str = "select * from (select a.*,b.dateno as dutydate from cs_crewschedule a,cs_datetimetable b " & _
                                        "where b.cstimetableid=a.cstimetableid) t " & _
                                        "where  datediff('d',t.dutydate,Format('" & datestr & "','yyyy/MM/dd'))=0 and t.driverno like '" & driverno & "%' and t.dutysort='" & durysort & "' " & _
                                        "order by iif(val(t.starttime)<10800,val(t.starttime)+86400,val(t.starttime))"
                Else
                    str = "select * from (select a.*,b.dateno as dutydate from cs_crewschedule a,cs_datetimetable b " & _
                                        "where b.cstimetableid=a.cstimetableid) t " & _
                                        "where datediff('d',t.dutydate,Format('" & datestr & "','yyyy/MM/dd'))=0 and t.driverno='" & driverno & "' and t.dutysort='" & durysort & "' " & _
                                        "order by iif(val(t.starttime)<10800,val(t.starttime)+86400,val(t.starttime))"
                End If
                tab = New Data.DataTable
                tab = Globle.Method.ReadDataForAccess(str)
                tempcsdriver.LinkDutyList = New List(Of LinkDuty)
                tempcsdriver.DHDutyList = New List(Of LinkDuty)
                tempcsdriver.DHToDutyList = New List(Of LinkDuty)
                If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
                    tempcsdriver.CSTimeTableID = tab.Rows(0).Item("CSTimeTableID").ToString
                    For i As Integer = 0 To tab.Rows.Count - 1
                        Dim templink As New LinkDuty(tab.Rows(i).Item("driverno").ToString.Trim, tab.Rows(i).Item("dutysort").ToString.Trim, tab.Rows(i).Item("trainno").ToString.Trim, _
                                                 IIf(Convert.ToInt32(tab.Rows(i).Item("starttime")) < 10800, Convert.ToInt32(tab.Rows(i).Item("starttime")) + 86400, Convert.ToInt32(tab.Rows(i).Item("starttime"))), _
                                                 tab.Rows(i).Item("startstaname").ToString.Trim, IIf(Convert.ToInt32(tab.Rows(i).Item("endtime")) < 10800, Convert.ToInt32(tab.Rows(i).Item("endtime")) + 86400, Convert.ToInt32(tab.Rows(i).Item("endtime"))), _
                                                 tab.Rows(i).Item("endstaname").ToString.Trim, tab.Rows(i).Item("vehicleno").ToString.Trim, Convert.ToDecimal(tab.Rows(i).Item("distance")), Convert.ToInt32(tab.Rows(i).Item("upordown")), Convert.ToInt32(tab.Rows(i).Item("staseq1")), Convert.ToInt32(tab.Rows(i).Item("staseq2")))
                        tempcsdriver.LinkDutyList.Add(templink)
                    Next
                End If

                str = "select * from cs_deadheading t where t.driverno='" & driverno & _
                    "' and datediff('d',t.dateno ,Format('" & datestr & _
                    "','yyyy/MM/dd'))=0 order by t.driverno,iif(val(t.starttime)<10800,val(t.starttime)+86400,val(t.starttime))"
                tab = New Data.DataTable
                tab = Globle.Method.ReadDataForAccess(str)

                If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
                    For j As Integer = 0 To tab.Rows.Count - 1
                        If tab.Rows(j).Item("headingproperty").ToString = "出库" OrElse tab.Rows(j).Item("headingproperty").ToString = "出班" Then
                            Dim templink As New LinkDuty(tab.Rows(j).Item("driverno").ToString.Trim, tab.Rows(j).Item("dutysort").ToString.Trim, tab.Rows(j).Item("trainno").ToString.Trim, _
                                             IIf(Convert.ToInt32(tab.Rows(j).Item("starttime")) < 10800, Convert.ToInt32(tab.Rows(j).Item("starttime")) + 86400, Convert.ToInt32(tab.Rows(j).Item("starttime"))), _
                                             tab.Rows(j).Item("startstaname").ToString.Trim, IIf(Convert.ToInt32(tab.Rows(j).Item("endtime")) < 10800, Convert.ToInt32(tab.Rows(j).Item("endtime")) + 86400, Convert.ToInt32(tab.Rows(j).Item("endtime"))), _
                                             tab.Rows(j).Item("endstaname").ToString.Trim, tab.Rows(j).Item("vehicleno").ToString.Trim, Convert.ToDecimal(tab.Rows(j).Item("distance")), Convert.ToInt32(tab.Rows(j).Item("upordown")), Convert.ToInt32(tab.Rows(j).Item("startstaseq")), Convert.ToInt32(tab.Rows(j).Item("endstaseq")))
                            tempcsdriver.DHDutyList.Add(templink)
                        Else
                            Dim templink As New LinkDuty(tab.Rows(j).Item("driverno").ToString.Trim, tab.Rows(j).Item("dutysort").ToString.Trim, tab.Rows(j).Item("trainno").ToString.Trim, _
                                             IIf(Convert.ToInt32(tab.Rows(j).Item("starttime")) < 10800, Convert.ToInt32(tab.Rows(j).Item("starttime")) + 86400, Convert.ToInt32(tab.Rows(j).Item("starttime"))), _
                                             tab.Rows(j).Item("startstaname").ToString.Trim, IIf(Convert.ToInt32(tab.Rows(j).Item("endtime")) < 10800, Convert.ToInt32(tab.Rows(j).Item("endtime")) + 86400, Convert.ToInt32(tab.Rows(j).Item("endtime"))), _
                                             tab.Rows(j).Item("endstaname").ToString.Trim, tab.Rows(j).Item("vehicleno").ToString.Trim, Convert.ToDecimal(tab.Rows(j).Item("distance")), Convert.ToInt32(tab.Rows(j).Item("upordown")), Convert.ToInt32(tab.Rows(j).Item("startstaseq")), Convert.ToInt32(tab.Rows(j).Item("endstaseq")))
                            tempcsdriver.DHToDutyList.Add(templink)
                        End If
                    Next
                End If
            End If
        Else
            Exit Sub
        End If
        If tempcsdriver.DutySort <> "休息" AndAlso tempcsdriver.DriverNo <> "" Then
            tempcsdriver.GetPreparedTime()
            tempcsdriver.GetWorkLoad()
            tempcsdriver.SimplifyCSDriver()
        End If
        Me.CSDriverList.Add(tempcsdriver)
        If tempcsdriver.DriverNo.Contains("备班") Then
            str = "select t.*,m.dateno from cs_result_prepareddutyinf t,cs_datetimetable m where t.lineid=m.lineid " & _
                  "and t.cstimetableid=m.cstimetableid and datediff('d',m.dateno,Format('" & datestr & "','yyyy/MM/dd'))=0 and t.dutysort='" & tempcsdriver.DutySort & "' and t.name='" & tempcsdriver.DriverNo & "'"
            tab = Globle.Method.ReadDataForAccess(str)
            If tab IsNot Nothing AndAlso tab.Rows.Count = 1 Then
                tempcsdriver.TotalDriveTime = CInt(tab.Rows(0).Item("endtime")) - CInt(tab.Rows(0).Item("starttime"))
                tempcsdriver.TotalWorkTime = CInt(tab.Rows(0).Item("endtime")) - CInt(tab.Rows(0).Item("starttime"))
            End If
        ElseIf tempcsdriver.DriverNo.Contains("备车") Then
            str = "select t.*,m.dateno from cs_result_preparedtraininf t,cs_datetimetable m where t.lineid=m.lineid " & _
                  "and t.cstimetableid=m.cstimetableid and datediff('d',m.dateno,Format('" & datestr & "','yyyy/MM/dd'))=0 and t.dutysort='" & tempcsdriver.DutySort & "' and t.name='" & tempcsdriver.DriverNo & "'"
            tab = Globle.Method.ReadDataForAccess(str)
            If tab IsNot Nothing AndAlso tab.Rows.Count = 1 Then
                tempcsdriver.TotalDriveTime = CInt(tab.Rows(0).Item("endtime")) - CInt(tab.Rows(0).Item("starttime"))
                tempcsdriver.TotalWorkTime = CInt(tab.Rows(0).Item("endtime")) - CInt(tab.Rows(0).Item("starttime"))
            End If
        Else
            str = "select t.*,m.dateno from cs_workload t,cs_datetimetable m where t.lineid=m.lineid and t.cstimetableid=m.cstimetableid and datediff('d',m.dateno,Format('" & _
                        datestr & "','yyyy/MM/dd'))=0 and t.dutysort='" & tempcsdriver.DutySort & "' and t.driverno='" & tempcsdriver.DriverNo & "'"
            tab = Globle.Method.ReadDataForAccess(str)
            If tab IsNot Nothing AndAlso tab.Rows.Count = 1 Then
                tempcsdriver.TotalDriveTime = tab.Rows(0).Item("drivetime")
                tempcsdriver.TotalWorkTime = tab.Rows(0).Item("worktime")
            End If
        End If

        Me.ToTalDriveDistance += tempcsdriver.TotalDriverDistance
        Me.ToTalDriveTime += tempcsdriver.TotalDriveTime
        Me.TotalWorkTime += tempcsdriver.TotalWorkTime
        tab.Dispose()
    End Sub
End Class

Public Class CSDriver
    Public LineID As String
    Public CSTimeTableID As String
    Public DriverNo As String
    Public DutySort As String
    Public PreTrainTime As Integer
    Public PreDutyTime As Integer
    Public TotalDriveTime As Integer
    Public TotalWorkTime As Integer
    Public TotalDriverDistance As Decimal
    Public LinkDutyList As List(Of LinkDuty)
    Public DHDutyList As List(Of LinkDuty)
    Public DHToDutyList As List(Of LinkDuty)
    Public MealDuty As List(Of LinkDuty)

    Public Sub New(ByVal linid As String, ByVal cstbid As String, ByVal drino As String, ByVal dusort As String)
        LineID = linid
        CSTimeTableID = cstbid
        DriverNo = drino
        DutySort = dusort
        LinkDutyList = New List(Of LinkDuty)()
        DHDutyList = New List(Of LinkDuty)()
        DHToDutyList = New List(Of LinkDuty)()
        MealDuty = New List(Of LinkDuty)()
    End Sub

    Public Sub New()
        LinkDutyList = New List(Of LinkDuty)()
        DHDutyList = New List(Of LinkDuty)()
        DHToDutyList = New List(Of LinkDuty)()
        MealDuty = New List(Of LinkDuty)()
    End Sub

    Public Sub GetWorkLoad()
        Dim str As String = "select * from cs_workload t where t.cstimetableid='" & Me.CSTimeTableID & "' and t.driverno='" & Me.DriverNo & "'"
        Dim temTab As Data.DataTable = Globle.Method.ReadDataForAccess(str)
        If temTab IsNot Nothing AndAlso temTab.Rows.Count = 1 Then
            Me.TotalDriverDistance = CDec(temTab.Rows(0).Item("drivedistance"))
            Me.TotalDriveTime = CInt(temTab.Rows(0).Item("drivetime"))
            Me.TotalWorkTime = CInt(temTab.Rows(0).Item("worktime"))
        End If
    End Sub

    Public Sub GetPreparedTime()
        Me.PreDutyTime = BasicPara.PrepareDutytime

        If Me.DHDutyList IsNot Nothing AndAlso Me.DHDutyList.Count > 0 Then
            If Coordination2.Global.GetStationType(Me.DHDutyList(0).startstaname, DepotPlaces, SheftPlaces, ChangePlaces) = Coordination2.StationTypes.车场或车辆段 Then
                Me.PreTrainTime = BasicPara.PrepareTraintime
            Else
                Me.PreTrainTime = 0
            End If
        Else
            If Me.LinkDutyList.Count > 0 AndAlso Coordination2.Global.GetStationType(Me.LinkDutyList(0).startstaname, DepotPlaces, SheftPlaces, ChangePlaces) = Coordination2.StationTypes.车场或车辆段 Then
                Me.PreTrainTime = BasicPara.PrepareTraintime
            Else
                Me.PreTrainTime = 0
            End If
        End If
    End Sub

    Public Sub SimplifyCSDriver()
        If Me.DHDutyList IsNot Nothing AndAlso Me.DHDutyList.Count > 0 Then
            For i As Integer = Me.DHDutyList.Count - 1 To 1 Step -1
                If Me.DHDutyList(i).trainno = Me.DHDutyList(i - 1).trainno AndAlso Me.DHDutyList(i).UpOrDown = Me.DHDutyList(i - 1).UpOrDown Then
                    Me.DHDutyList(i - 1).endstaname = Me.DHDutyList(i).endstaname
                    Me.DHDutyList(i - 1).endtime = Me.DHDutyList(i).endtime
                    Me.DHDutyList(i - 1).EndStaSeq = Me.DHDutyList(i).EndStaSeq
                    Me.DHDutyList(i - 1).Drivetime += Me.DHDutyList(i).Drivetime
                    Me.DHDutyList(i - 1).DriveDistance += Me.DHDutyList(i).DriveDistance
                    Me.DHDutyList.RemoveAt(i)
                Else
                    If Me.DHDutyList(i - 1).EndStaSeq <> Me.DHDutyList(i).StartStaSeq Then              '车站序号的修正处理
                        If Coordination2.Global.GetStationType(Me.DHDutyList(i - 1).startstaname, DepotPlaces, SheftPlaces, ChangePlaces) _
                        = Coordination2.StationTypes.车场或车辆段 Then
                            Dim chazhi As Integer = Me.DHDutyList(i - 1).EndStaSeq - Me.DHDutyList(i - 1).StartStaSeq
                            Me.DHDutyList(i - 1).EndStaSeq = Me.DHDutyList(i).StartStaSeq
                            Me.DHDutyList(i - 1).StartStaSeq = Me.DHDutyList(i - 1).EndStaSeq - chazhi
                        End If

                        If Coordination2.Global.GetStationType(Me.DHDutyList(i).endstaname, DepotPlaces, SheftPlaces, ChangePlaces) _
                        = Coordination2.StationTypes.车场或车辆段 Then
                            Dim chazhi As Integer = Me.DHDutyList(i).EndStaSeq - Me.DHDutyList(i).StartStaSeq
                            Me.DHDutyList(i).StartStaSeq = Me.DHDutyList(i - 1).EndStaSeq
                            Me.DHDutyList(i).EndStaSeq = Me.DHDutyList(i).StartStaSeq + chazhi
                        End If
                    End If
                End If
            Next
        End If

        If Me.LinkDutyList IsNot Nothing AndAlso Me.LinkDutyList.Count > 0 Then
            For i As Integer = Me.LinkDutyList.Count - 1 To 1 Step -1
                If Me.LinkDutyList(i).trainno = Me.LinkDutyList(i - 1).trainno AndAlso Me.LinkDutyList(i).UpOrDown = Me.LinkDutyList(i - 1).UpOrDown Then
                    Me.LinkDutyList(i - 1).endstaname = Me.LinkDutyList(i).endstaname
                    Me.LinkDutyList(i - 1).endtime = Me.LinkDutyList(i).endtime
                    Me.LinkDutyList(i - 1).EndStaSeq = Me.LinkDutyList(i).EndStaSeq
                    Me.LinkDutyList(i - 1).Drivetime += Me.LinkDutyList(i).Drivetime
                    Me.LinkDutyList(i - 1).DriveDistance += Me.LinkDutyList(i).DriveDistance
                    Me.LinkDutyList.RemoveAt(i)
                Else
                    If Me.LinkDutyList(i - 1).EndStaSeq <> Me.LinkDutyList(i).StartStaSeq Then              '车站序号的修正处理
                        If Coordination2.Global.GetStationType(Me.LinkDutyList(i - 1).startstaname, DepotPlaces, SheftPlaces, ChangePlaces) _
                        = Coordination2.StationTypes.车场或车辆段 Then
                            Dim chazhi As Integer = Me.LinkDutyList(i - 1).EndStaSeq - Me.LinkDutyList(i - 1).StartStaSeq
                            Me.LinkDutyList(i - 1).EndStaSeq = Me.LinkDutyList(i).StartStaSeq
                            Me.LinkDutyList(i - 1).StartStaSeq = Me.LinkDutyList(i - 1).EndStaSeq - chazhi
                        End If

                        If Coordination2.Global.GetStationType(Me.LinkDutyList(i).endstaname, DepotPlaces, SheftPlaces, ChangePlaces) _
                        = Coordination2.StationTypes.车场或车辆段 Then
                            Dim chazhi As Integer = Me.LinkDutyList(i).EndStaSeq - Me.LinkDutyList(i).StartStaSeq
                            Me.LinkDutyList(i).StartStaSeq = Me.LinkDutyList(i - 1).EndStaSeq
                            Me.LinkDutyList(i).EndStaSeq = Me.LinkDutyList(i).StartStaSeq + chazhi
                        End If
                    End If
                End If
            Next
        End If

        If Me.DHToDutyList IsNot Nothing AndAlso Me.DHToDutyList.Count > 0 Then
            For i As Integer = Me.DHToDutyList.Count - 1 To 1 Step -1
                If Me.DHToDutyList(i).trainno = Me.DHToDutyList(i - 1).trainno AndAlso Me.DHToDutyList(i).UpOrDown = Me.DHToDutyList(i - 1).UpOrDown Then
                    Me.DHToDutyList(i - 1).endstaname = Me.DHToDutyList(i).endstaname
                    Me.DHToDutyList(i - 1).endtime = Me.DHToDutyList(i).endtime
                    Me.DHToDutyList(i - 1).EndStaSeq = Me.DHToDutyList(i).EndStaSeq
                    Me.DHToDutyList(i - 1).Drivetime += Me.DHToDutyList(i).Drivetime
                    Me.DHToDutyList(i - 1).DriveDistance += Me.DHToDutyList(i).DriveDistance
                    Me.DHToDutyList.RemoveAt(i)
                Else
                    If Me.DHToDutyList(i - 1).EndStaSeq <> Me.DHToDutyList(i).StartStaSeq Then                  '车站序号的修正处理
                        If Coordination2.Global.GetStationType(Me.DHToDutyList(i - 1).startstaname, DepotPlaces, SheftPlaces, ChangePlaces) _
                        = Coordination2.StationTypes.车场或车辆段 Then
                            Dim chazhi As Integer = Me.DHToDutyList(i - 1).EndStaSeq - Me.DHToDutyList(i - 1).StartStaSeq
                            Me.DHToDutyList(i - 1).EndStaSeq = Me.DHToDutyList(i).StartStaSeq
                            Me.DHToDutyList(i - 1).StartStaSeq = Me.DHToDutyList(i - 1).EndStaSeq - chazhi
                        End If

                        If Coordination2.Global.GetStationType(Me.DHToDutyList(i).endstaname, DepotPlaces, SheftPlaces, ChangePlaces) _
                        = Coordination2.StationTypes.车场或车辆段 Then
                            Dim chazhi As Integer = Me.DHToDutyList(i).EndStaSeq - Me.DHToDutyList(i).StartStaSeq
                            Me.DHToDutyList(i).StartStaSeq = Me.DHToDutyList(i - 1).EndStaSeq
                            Me.DHToDutyList(i).EndStaSeq = Me.DHToDutyList(i).StartStaSeq + chazhi
                        End If
                    End If
                End If
            Next
        End If
    End Sub

    Public Sub InsertMealDutyIntoDutylist()
        If MealDuty.Count > 0 Then
            For Each meal As LinkDuty In MealDuty
                If Me.LinkDutyList.Count > 0 Then
                    For i As Integer = 0 To Me.LinkDutyList.Count - 1
                        If meal.starttime < Me.LinkDutyList(i).starttime Then
                            Me.LinkDutyList.Insert(i, meal)
                            Exit For
                        End If
                    Next
                End If
            Next
        End If
    End Sub
End Class

Public Class LinkDuty
    Public driverno As String
    Public dutysort As String
    Public trainno As String
    Public starttime As Integer
    Public startstaname As String
    Public endtime As Integer
    Public endstaname As String
    Public vehicleno As String
    Public Drivetime As Integer
    Public DriveDistance As Decimal
    Public UpOrDown As Integer
    Public StartStaSeq As Integer
    Public EndStaSeq As Integer
    Public Sub New(ByVal drivno As String, ByVal DutSort As String, ByVal TraNo As String, _
                   ByVal StaTime As Integer, ByVal StaStaname As String, ByVal EnTime As Integer, ByVal EnStaname As String, ByVal VehiNo As String, ByVal Dridistance As Decimal, ByVal updown As Integer, ByVal startstacode As Integer, ByVal endstacode As Integer)
        driverno = drivno
        dutysort = DutSort
        trainno = TraNo
        starttime = StaTime
        startstaname = StaStaname
        endtime = EnTime
        endstaname = EnStaname
        vehicleno = VehiNo
        Drivetime = EnTime - StaTime
        DriveDistance = Dridistance
        UpOrDown = updown
        StartStaSeq = startstacode
        EndStaSeq = endstacode
    End Sub
End Class
