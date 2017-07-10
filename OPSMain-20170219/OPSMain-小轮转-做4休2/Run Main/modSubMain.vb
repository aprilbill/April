Imports System.IO


Module modSubMain
    Public IsFirOrLas As Integer
    Public CurrentUserName As String
    Public CurrentUserRole As String
    Public StrLink As String = ""
    Public CurLineName As String = ""

    Public Stalist As New Dictionary(Of String, List(Of String))
    '===================================================================================车站类型信息
    Public DepotPlaces As List(Of String)         '车场
    Public SheftPlaces As List(Of String)          '上班点
    Public ChangePlaces As List(Of String)         '轮班点
    Public BasicPara As BasicParameter
    Public Function GetTimetableFromDate(ByVal _date As Date, ByVal Lineid As String) As Coordination2.CSTimeTable
        GetTimetableFromDate = Nothing
        Dim str As String = "select * from cs_datetimetable t where datediff('d',t.dateno,Format('" & _
                             _date.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))=0 and t.lineid='" & Lineid & "'"
        Dim temtab As DataTable = Globle.Method.ReadDataForAccess(str)
        If temtab IsNot Nothing AndAlso temtab.Rows.Count = 1 Then
            Dim timetableName As String = temtab.Rows(0).Item("cstimetableid").ToString
            If timetableName <> "" Then
                str = "SELECT * FROM CS_CSTIMETABLEINF WHERE LINEID='" + Lineid + "' and CSTIMETABLEID='" + timetableName + "' "
                Dim tempTable As DataTable = Globle.Method.ReadDataForAccess(str)
                If IsNothing(tempTable) = False AndAlso tempTable.Rows.Count > 0 Then
                    GetTimetableFromDate = New Coordination2.CSTimeTable(timetableName, Lineid)
                End If
            End If
        End If
    End Function

    Public Function GetTimetableIDFromDate(ByVal _date As Date, ByVal Lineid As String) As String
        GetTimetableIDFromDate = ""
        Dim str As String = "select * from cs_datetimetable t where datediff('d',t.dateno,Format('" & _
                             _date.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))=0"
        Dim temtab As DataTable = Globle.Method.ReadDataForAccess(str)
        If temtab IsNot Nothing AndAlso temtab.Rows.Count = 1 Then
            GetTimetableIDFromDate = temtab.Rows(0).Item("cstimetableid").ToString
        End If
    End Function

    Public Function ReadCSTableDataFromOracle(ByVal CSTimetableID As String, ByVal Curlinename As String) As CS_CSMaker.CSLinkTrain()         '读取CSlinktrains
        ReadCSTableDataFromOracle = Nothing
        Dim CSLinkTrains() As CS_CSMaker.CSLinkTrain
        Dim DaTab As New DataTable
        Dim ShiftTab As New DataTable
        Dim ChangeTab As New DataTable
        Dim StopPlaces As New List(Of String)              '获取所有可能司机停留的车站
        Dim Str As String
        Str = "SELECT t.*,d.dinnerbegintime,d.dinnerendtime,d.dinnerplace,d.havedinner  FROM  CS_WORKLOAD t,cs_dinnerinf d  WHERE t.LINEID='" & Curlinename & "' AND t.CSTIMETABLEID='" & CSTimetableID & "' and t.lineid=d.lineid and t.cstimetableid=d.cstimetableid and t.driverno=d.driverno order by  t.DriverNo"
        DaTab = Globle.Method.ReadDataForAccess(Str)
        Str = "select * from cs_result_shiftplaceinf t where t.cstimetableid='" & CSTimetableID & "'"
        ShiftTab = Globle.Method.ReadDataForAccess(Str)
        Str = "select * from cs_result_changeplaceinf t where t.cstimetableid='" & CSTimetableID & "'"
        ChangeTab = Globle.Method.ReadDataForAccess(Str)
        For Each row As DataRow In ShiftTab.Rows
            Dim iFExist As Boolean = False
            For Each place As String In StopPlaces
                If place = row.Item("shiftplace").ToString.Trim Then
                    iFExist = True
                    Exit For
                End If
            Next
            If iFExist = False Then
                StopPlaces.Add(row.Item("shiftplace").ToString.Trim)
            End If
        Next
        For Each row As DataRow In ChangeTab.Rows
            Dim iFExist As Boolean = False
            For Each place As String In StopPlaces
                If place = row.Item("changeplace").ToString.Trim Then
                    iFExist = True
                    Exit For
                End If
            Next
            If iFExist = False Then
                StopPlaces.Add(row.Item("changeplace").ToString.Trim)
            End If
        Next
        ShiftTab.Dispose()
        ChangeTab.Dispose()
        If DaTab.Rows.Count = 0 Then
            MsgBox("没有数据")
            Exit Function
        Else
            Str = "SELECT * FROM  CS_CREWSCHEDULE WHERE LINEID='" & Curlinename & "' AND CSTIMETABLEID='" & CSTimetableID & "'  order by ID "
            DaTab = Globle.Method.ReadDataForAccess(Str)
            ReDim CSLinkTrains(0)
            For i As Integer = 1 To DaTab.Rows.Count
                Dim TempCSLinkTrain As New CS_CSMaker.CSLinkTrain
                TempCSLinkTrain.CSTrainID = i
                TempCSLinkTrain.OutputCheCi = DaTab.Rows(i - 1).Item("TrainNo").ToString.Trim
                TempCSLinkTrain.StartStaName = DaTab.Rows(i - 1).Item("StartStaName").ToString.Trim
                TempCSLinkTrain.StartTime = CInt(DaTab.Rows(i - 1).Item("StartTime").ToString.Trim)
                TempCSLinkTrain.STASEQ1 = DaTab.Rows(i - 1).Item("STASEQ1").ToString.Trim
                TempCSLinkTrain.STASEQ2 = DaTab.Rows(i - 1).Item("STASEQ2").ToString.Trim
                TempCSLinkTrain.EndStaName = DaTab.Rows(i - 1).Item("EndStaName").ToString.Trim
                TempCSLinkTrain.EndTime = CInt(DaTab.Rows(i - 1).Item("EndTime").ToString.Trim)
                TempCSLinkTrain.nCheDiID = CInt(DaTab.Rows(i - 1).Item("CheDiID").ToString.Trim)
                TempCSLinkTrain.nPathID1 = CInt(DaTab.Rows(i - 1).Item("Path1").ToString.Trim)
                TempCSLinkTrain.nPathID2 = CInt(DaTab.Rows(i - 1).Item("Path2").ToString.Trim)
                TempCSLinkTrain.nTrainID = CInt(DaTab.Rows(i - 1).Item("TrainID").ToString.Trim)
                TempCSLinkTrain.UpOrDown = CInt(DaTab.Rows(i - 1).Item("UpOrDown").ToString.Trim)
                TempCSLinkTrain.ZFTime = CInt(DaTab.Rows(i - 1).Item("ZFTime").ToString.Trim)
                TempCSLinkTrain.CulStartTime = IIf(TempCSLinkTrain.StartTime < 10800, TempCSLinkTrain.StartTime + 86400, TempCSLinkTrain.StartTime)
                TempCSLinkTrain.CulEndTime = IIf(TempCSLinkTrain.EndTime < 10800, TempCSLinkTrain.EndTime + 86400, TempCSLinkTrain.EndTime)
                TempCSLinkTrain.distance = Convert.ToDouble(DaTab.Rows(i - 1).Item("DISTANCE").ToString.Trim)
                TempCSLinkTrain.sCheDiHao = DaTab.Rows(i - 1).Item("VEHICLENO").ToString.Trim
                '=============将CSLinkTrain尽量拆解到最小
                Str = "select t.* from (select t.*,m.printnum from tms_timetableinfo t,tms_stockusinginfo m where t.traindiagramid=m.traindiagramid and t.trainnum=m.linktrainnum) t,cs_cstimetableinf m " & _
                    "where t.traindiagramid=m.traindiagramid and m.cstimetableid='" & CSTimetableID & "' and t.printnum='" & TempCSLinkTrain.OutputCheCi & "'  order by t.seq"
                Dim TrainTab As DataTable = Globle.Method.ReadDataForAccess(Str)
                Dim StartMark As Boolean = False
                Dim TempTrains As New List(Of CS_CSMaker.CSLinkTrain)
                TempTrains.Add(TempCSLinkTrain)
                For Each row As DataRow In TrainTab.Rows
                    If row.Item("stationname").ToString.Trim = TempCSLinkTrain.StartStaName Then
                        StartMark = True
                        Continue For
                    ElseIf row.Item("stationname").ToString.Trim = TempCSLinkTrain.EndStaName Then
                        Exit For
                    End If
                    If StartMark Then
                        Dim iFExist As Boolean = False
                        For Each place As String In StopPlaces
                            If place = row.Item("stationname").ToString.Trim Then
                                iFExist = True
                                Exit For
                            End If
                        Next
                        If iFExist Then
                            Dim tempTrain As CS_CSMaker.CSLinkTrain = TempTrains(TempTrains.Count - 1)
                            TempTrains.RemoveAt(TempTrains.Count - 1)
                            Dim FirTrain As CS_CSMaker.CSLinkTrain = DeepCloneCSlinkTrain(tempTrain)
                            Dim SecTrain As CS_CSMaker.CSLinkTrain = DeepCloneCSlinkTrain(tempTrain)
                            FirTrain.EndStaName = row.Item("stationname").ToString.Trim
                            FirTrain.EndTime = row.Item("arritime")
                            FirTrain.STASEQ2 = row.Item("seq")
                            FirTrain.CulEndTime = IIf(FirTrain.EndTime < 10800, FirTrain.EndTime + 86400, FirTrain.EndTime)
                            FirTrain.distance = GetSecDistance(FirTrain.StartStaName, FirTrain.EndStaName, Curlinename)
                            SecTrain.StartStaName = row.Item("stationname").ToString.Trim
                            SecTrain.StartTime = row.Item("departtime")
                            SecTrain.STASEQ2 = row.Item("seq")
                            SecTrain.distance = GetSecDistance(SecTrain.StartStaName, SecTrain.EndStaName, Curlinename)
                            SecTrain.CulStartTime = IIf(SecTrain.StartTime < 10800, SecTrain.StartTime + 86400, SecTrain.StartTime)
                            TempTrains.Add(FirTrain)
                            TempTrains.Add(SecTrain)
                        End If
                    End If
                Next
                For Each train As CS_CSMaker.CSLinkTrain In TempTrains
                    ReDim Preserve CSLinkTrains(UBound(CSLinkTrains) + 1)
                    CSLinkTrains(UBound(CSLinkTrains)) = train
                Next
            Next
            For i As Integer = 1 To UBound(CSLinkTrains)
                CSLinkTrains(i).CSTrainID = i
            Next
            ReadCSTableDataFromOracle = CSLinkTrains
            DaTab.Dispose()
        End If
    End Function
    Public Sub GetSta()
        Dim sqlstr As String = "select * from TMS_STATIONINFO where TraindiagramID=(SELECT max(TraindiagramID) FROM TMS_STATIONINFO)"
        Dim tempTable As DataTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        Stalist.Clear()
        For n As Integer = 0 To tempTable.Rows.Count - 1
            If Stalist.Keys.Contains(tempTable.Rows(n).Item("linename").ToString) = False Then
                Stalist.Add(tempTable.Rows(n).Item("linename").ToString, New List(Of String))
            End If
            If Stalist(tempTable.Rows(n).Item("linename").ToString).Contains(tempTable.Rows(n).Item("stationname").ToString) = False Then
                Stalist(tempTable.Rows(n).Item("linename").ToString).Add(tempTable.Rows(n).Item("stationname").ToString)
            End If
        Next

    End Sub

    Public Function GetSecDistance(ByVal StartSta As String, ByVal EndSta As String, ByVal LineID As String) As Decimal
        GetSecDistance = 0
        Dim Str As String = "select * from cs_sh_trainlength t where t.lineid='" & LineID & "' and t.startsta='" & StartSta & "' and t.endsta='" & EndSta & "'"
        Dim tab2 As DataTable = Globle.Method.ReadDataForAccess(Str)
        If tab2 IsNot Nothing AndAlso tab2.Rows.Count = 1 Then
            GetSecDistance = CDec(tab2.Rows(0).Item("length"))
        End If
    End Function

    Public Function DeepCloneCSlinkTrain(ByVal TempCSLinkTrain As CS_CSMaker.CSLinkTrain) As CS_CSMaker.CSLinkTrain
        Dim NewCSlinktrain As New CS_CSMaker.CSLinkTrain
        NewCSlinktrain.CSTrainID = TempCSLinkTrain.CSTrainID
        NewCSlinktrain.OutputCheCi = TempCSLinkTrain.OutputCheCi
        NewCSlinktrain.StartStaName = TempCSLinkTrain.StartStaName
        NewCSlinktrain.StartTime = TempCSLinkTrain.StartTime
        NewCSlinktrain.STASEQ1 = TempCSLinkTrain.STASEQ1
        NewCSlinktrain.STASEQ2 = TempCSLinkTrain.STASEQ2
        NewCSlinktrain.EndStaName = TempCSLinkTrain.EndStaName
        NewCSlinktrain.EndTime = TempCSLinkTrain.EndTime
        NewCSlinktrain.nCheDiID = TempCSLinkTrain.nCheDiID
        NewCSlinktrain.nPathID1 = TempCSLinkTrain.nPathID1
        NewCSlinktrain.nPathID2 = TempCSLinkTrain.nPathID2
        NewCSlinktrain.nTrainID = TempCSLinkTrain.nTrainID
        NewCSlinktrain.UpOrDown = TempCSLinkTrain.UpOrDown
        NewCSlinktrain.ZFTime = TempCSLinkTrain.ZFTime
        NewCSlinktrain.CulStartTime = TempCSLinkTrain.CulStartTime
        NewCSlinktrain.CulEndTime = TempCSLinkTrain.CulEndTime
        NewCSlinktrain.distance = TempCSLinkTrain.distance
        NewCSlinktrain.sCheDiHao = TempCSLinkTrain.sCheDiHao
        Return NewCSlinktrain
    End Function

    Public Function FormCSChediFromCSlinkTrains(ByVal CSlinktrains() As CS_CSMaker.CSLinkTrain) As CS_CSMaker.typeCSCheDi()         '根据cslinktrains形成cschedi
        FormCSChediFromCSlinkTrains = Nothing
        Dim cschedi(0) As CS_CSMaker.typeCSCheDi
        Call SortCSLinkTrain(CSlinktrains)
        If CSlinktrains.Length > 1 Then
            For i As Integer = 1 To UBound(CSlinktrains)
                If i = 1 Then
                    ReDim Preserve cschedi(UBound(cschedi) + 1)
                    cschedi(UBound(cschedi)) = New CS_CSMaker.typeCSCheDi(UBound(cschedi), CSlinktrains(i).sCheDiHao)
                    CSlinktrains(i).nCheDiID = UBound(cschedi)
                    cschedi(UBound(cschedi)).CSLinkTrains.Add(CSlinktrains(i))
                Else
                    Dim IfChediExist As Boolean = False
                    For j As Integer = 1 To UBound(cschedi)
                        If cschedi(j).sCheDiHao = CSlinktrains(i).sCheDiHao Then
                            cschedi(j).CSLinkTrains.Add(CSlinktrains(i))
                            CSlinktrains(i).nCheDiID = j
                            IfChediExist = True
                            Exit For
                        End If
                    Next
                    If IfChediExist = False Then
                        ReDim Preserve cschedi(UBound(cschedi) + 1)
                        cschedi(UBound(cschedi)) = New CS_CSMaker.typeCSCheDi(UBound(cschedi), CSlinktrains(i).sCheDiHao)
                        CSlinktrains(i).nCheDiID = UBound(cschedi)
                        cschedi(UBound(cschedi)).CSLinkTrains.Add(CSlinktrains(i))
                    End If
                End If
            Next
            For i As Integer = 1 To UBound(cschedi)
                cschedi(i).StartTime = cschedi(i).CSLinkTrains(0).StartTime
                cschedi(i).EndTime = cschedi(i).CSLinkTrains(cschedi(i).CSLinkTrains.Count - 1).EndTime
            Next
            FormCSChediFromCSlinkTrains = cschedi
        End If
    End Function

    Public Sub SortCSLinkTrain(ByVal CSLinkTrains() As CS_CSMaker.CSLinkTrain)        '按时间排序
        Dim i, j, flag As Integer
        For i = 1 To UBound(CSLinkTrains)
            For j = i + 1 To UBound(CSLinkTrains)
                If CSLinkTrains(i).CulStartTime > CSLinkTrains(j).CulStartTime Then
                    Dim tempCSlinkTrain As CS_CSMaker.CSLinkTrain
                    tempCSlinkTrain = CSLinkTrains(i)
                    CSLinkTrains(i) = CSLinkTrains(j)
                    CSLinkTrains(j) = tempCSlinkTrain
                End If
            Next
        Next

        flag = 1
        For i = 1 To UBound(CSLinkTrains)
            If CSLinkTrains(i).UpOrDown = 0 Then
                CSLinkTrains(i).UpOrDownNum = flag
                flag = flag + 1
            End If
        Next

        flag = 1
        For i = 1 To UBound(CSLinkTrains)
            If CSLinkTrains(i).UpOrDown = 1 Then
                CSLinkTrains(i).UpOrDownNum = flag
                flag = flag + 1
            End If
        Next
        For i = 1 To UBound(CSLinkTrains)
            CSLinkTrains(i).CSTrainID = i
        Next
    End Sub

    Public Sub SortByInDepotTime(ByVal drivers As List(Of Coordination2.CSDriver))       '按照入库时间倒排序
        If drivers.Count > 1 Then
            For i As Integer = 0 To drivers.Count - 2
                For j As Integer = i + 1 To drivers.Count - 1
                    Dim tempDri As Coordination2.CSDriver = Nothing
                    If IIf(drivers(j).endtime < 10800, drivers(j).endtime + 86400, drivers(j).endtime) > IIf(drivers(i).endtime < 10800, drivers(i).endtime + 86400, drivers(i).endtime) Then
                        tempDri = drivers(i)
                        drivers(i) = drivers(j)
                        drivers(j) = tempDri
                    End If
                Next
            Next
        End If
    End Sub

    Public Sub SortByOutDepotTime(ByVal drivers As List(Of Coordination2.CSDriver))           '按照出库时间正排序
        If drivers.Count > 1 Then
            For i As Integer = 0 To drivers.Count - 2
                For j As Integer = i + 1 To drivers.Count - 1
                    Dim tempDri As Coordination2.CSDriver = Nothing
                    If drivers(j).startdutytime < drivers(i).startdutytime Then
                        tempDri = drivers(i)
                        drivers(i) = drivers(j)
                        drivers(j) = tempDri
                    End If
                Next
            Next
        End If
    End Sub

    Public Sub SortByWorkTime(ByVal drivers As List(Of Coordination2.CSDriver), Optional ByVal IFDESC As Boolean = False)           '按照工作时间排序
        If drivers.Count > 1 Then
            For i As Integer = 0 To drivers.Count - 2
                For j As Integer = i + 1 To drivers.Count - 1
                    Dim tempDri As Coordination2.CSDriver = Nothing
                    If IFDESC = False Then
                        If drivers(j).DriveDistance < drivers(i).DriveDistance Then
                            tempDri = drivers(i)
                            drivers(i) = drivers(j)
                            drivers(j) = tempDri
                        End If
                    Else
                        If drivers(j).DriveDistance > drivers(i).DriveDistance Then
                            tempDri = drivers(i)
                            drivers(i) = drivers(j)
                            drivers(j) = tempDri
                        End If
                    End If
                Next
            Next
        End If
    End Sub
    Public Sub SortByWorkTimebalance(ByVal drivers As List(Of Coordination2.CSDriver), Optional ByVal IFDESC As Boolean = False)           '按照工作时间排序===均衡
        If drivers.Count > 1 Then
            For i As Integer = 0 To drivers.Count - 2
                For j As Integer = i + 1 To drivers.Count - 1
                    Dim tempDri As Coordination2.CSDriver = Nothing
                        If drivers(j).DriveDistance < drivers(i).DriveDistance Then
                        tempDri = drivers(i)
                        drivers(i) = drivers(j)
                        drivers(j) = tempDri
                    ElseIf drivers(j).DriveDistance = drivers(i).DriveDistance Then
                        If drivers(j).OutPutCSDriverNo < drivers(i).OutPutCSDriverNo Then
                            tempDri = drivers(i)
                            drivers(i) = drivers(j)
                            drivers(j) = tempDri
                        End If
                    End If
                Next
            Next
            Dim averse As New List(Of Coordination2.CSDriver)
            Dim endnum = drivers.Count - 1
            For i As Integer = 0 To drivers.Count / 2
                If i <> endnum And i < endnum Then
                    averse.Add(drivers(i))
                    averse.Add(drivers(endnum))
                    endnum -= 1
                ElseIf i = endnum Then
                    averse.Add(drivers(i))
                End If
            Next
            For i As Integer = 0 To drivers.Count - 1
                drivers(i) = averse(i)
            Next
        End If
    End Sub

    Public Sub SortByWorkTime(ByVal drivers As List(Of AMDriver), Optional ByVal IFDESC As Boolean = False)           '按照工作时间排序
        If drivers.Count > 1 Then
            For i As Integer = 0 To drivers.Count - 2
                For j As Integer = i + 1 To drivers.Count - 1
                    Dim tempDri As AMDriver = Nothing
                    If IFDESC = False Then
                        If drivers(j).DriveLength < drivers(i).DriveLength Then
                            tempDri = drivers(i)
                            drivers(i) = drivers(j)
                            drivers(j) = tempDri
                        End If
                    Else
                        If drivers(j).DriveLength > drivers(i).DriveLength Then
                            tempDri = drivers(i)
                            drivers(i) = drivers(j)
                            drivers(j) = tempDri
                        End If
                    End If
                Next
            Next
        End If
    End Sub

    Public Sub SortByWorkTime(ByVal drivers As List(Of Coordination2.Driver), Optional ByVal IFDESC As Boolean = False)           '按照工作时间排序
        If drivers.Count > 1 Then
            For i As Integer = 0 To drivers.Count - 2
                For j As Integer = i + 1 To drivers.Count - 1
                    Dim tempDri As Coordination2.Driver = Nothing
                    If IFDESC = False Then
                        If drivers(j).DriveTime < drivers(i).DriveTime Then
                            tempDri = drivers(i)
                            drivers(i) = drivers(j)
                            drivers(j) = tempDri
                        End If
                    Else
                        If drivers(j).DriveTime > drivers(i).DriveTime Then
                            tempDri = drivers(i)
                            drivers(i) = drivers(j)
                            drivers(j) = tempDri
                        End If
                    End If
                Next
            Next
        End If
    End Sub

    Public Sub SortByWorkTime(ByVal teams As List(Of CrewTrainingManager.DriverTeam), Optional ByVal IFDESC As Boolean = False)           '按照工作时间排序
        If teams.Count > 1 Then
            For i As Integer = 0 To teams.Count - 2
                For j As Integer = i + 1 To teams.Count - 1
                    Dim tempTeam As CrewTrainingManager.DriverTeam = Nothing
                    If IFDESC = False Then
                        If teams(j).XiuxiFengbanNums - teams(i).XiuxiFengbanNums >= 1 Then
                            tempTeam = teams(i)
                            teams(i) = teams(j)
                            teams(j) = tempTeam
                        ElseIf teams(j).XiuxiFengbanNums = teams(i).XiuxiFengbanNums Then
                            If teams(j).DriveDistance + teams(j).PreDriveDistance < teams(i).DriveDistance + teams(i).PreDriveDistance Then
                                tempTeam = teams(i)
                                teams(i) = teams(j)
                                teams(j) = tempTeam
                            End If

                        End If
                        'If teams(j).DriveDistance + teams(j).PreDriveDistance < teams(i).DriveDistance + teams(i).PreDriveDistance  Then
                        '    tempTeam = teams(i)
                        '    teams(i) = teams(j)
                        '    teams(j) = tempTeam
                        'End If
                    Else
                        If teams(j).DriveDistance + teams(j).PreDriveDistance > teams(i).DriveDistance + teams(i).PreDriveDistance Then
                            tempTeam = teams(i)
                            teams(i) = teams(j)
                            teams(j) = tempTeam
                        End If
                    End If
                Next
            Next
        End If
    End Sub

    Public Sub SortBytotalxiuxi(ByVal teams As List(Of CrewTrainingManager.DriverTeam))           '按照工作时间排序
        If teams.Count > 1 Then
            For i As Integer = 0 To teams.Count - 2
                For j As Integer = i + 1 To teams.Count - 1
                    Dim tempTeam As CrewTrainingManager.DriverTeam = Nothing
                        Dim x As Integer = teams(i).CoDrivers(0).TotalXiuxiDayNum
                        Dim y As Integer = teams(j).CoDrivers(0).TotalXiuxiDayNum
                        If y < x Then
                            tempTeam = teams(i)
                            teams(i) = teams(j)
                            teams(j) = tempTeam
                    End If
                Next
            Next
        End If
    End Sub

    Public Sub SortByChubanNum(ByVal teams As List(Of CrewTrainingManager.DriverTeam), Optional ByVal IFDESC As Boolean = False)           '按照出班次数排序
        If teams.Count > 1 Then
            For i As Integer = 0 To teams.Count - 2
                For j As Integer = i + 1 To teams.Count - 1
                    Dim tempTeam As CrewTrainingManager.DriverTeam = Nothing
                    If IFDESC = False Then
                        If teams(j).ChuBanCount < teams(i).ChuBanCount Then
                            tempTeam = teams(i)
                            teams(i) = teams(j)
                            teams(j) = tempTeam
                        End If
                    Else
                        If teams(j).ChuBanCount > teams(i).ChuBanCount Then
                            tempTeam = teams(i)
                            teams(i) = teams(j)
                            teams(j) = tempTeam
                        End If
                    End If
                Next
            Next
        End If
    End Sub

    Public Sub SortByOutDepotNum(ByVal teams As List(Of CrewTrainingManager.DriverTeam), Optional ByVal IFDESC As Boolean = False)           '按照夜班段场次数排序
        If teams.Count > 1 Then
            For i As Integer = 0 To teams.Count - 2
                For j As Integer = i + 1 To teams.Count - 1
                    Dim tempTeam As CrewTrainingManager.DriverTeam = Nothing
                    If IFDESC = False Then
                        If teams(j).nightOutDepotCount < teams(i).nightOutDepotCount Then
                            tempTeam = teams(i)
                            teams(i) = teams(j)
                            teams(j) = tempTeam
                        End If
                    Else
                        If teams(j).nightOutDepotCount > teams(i).nightOutDepotCount Then
                            tempTeam = teams(i)
                            teams(i) = teams(j)
                            teams(j) = tempTeam
                        End If
                    End If
                Next
            Next
        End If
    End Sub

    Public Sub SortByWorkTimes(ByVal teams As List(Of CrewTrainingManager.DriverTeam), Optional ByVal IFDESC As Boolean = False)           '按照工作次数排序
        If teams.Count > 1 Then
            For i As Integer = 0 To teams.Count - 2
                For j As Integer = i + 1 To teams.Count - 1
                    Dim tempTeam As CrewTrainingManager.DriverTeam = Nothing
                    If IFDESC = False Then
                        If teams(j).WorkTimes < teams(i).WorkTimes Then
                            tempTeam = teams(i)
                            teams(i) = teams(j)
                            teams(j) = tempTeam
                        End If
                    Else
                        If teams(j).WorkTimes > teams(i).WorkTimes Then
                            tempTeam = teams(i)
                            teams(i) = teams(j)
                            teams(j) = tempTeam
                        End If
                    End If
                Next
            Next
        End If
    End Sub

    Public Sub SortByID(ByVal drivers As List(Of Coordination2.Driver), Optional ByVal IFDESC As Boolean = False)           '按照工作时间排序
        If drivers.Count > 1 Then
            For i As Integer = 0 To drivers.Count - 2
                For j As Integer = i + 1 To drivers.Count - 1
                    Dim tempDri As Coordination2.Driver = Nothing
                    If drivers(j).ID < drivers(i).ID Then
                        tempDri = drivers(i)
                        drivers(i) = drivers(j)
                        drivers(j) = tempDri
                    End If
                Next
            Next
        End If
    End Sub

    Public Sub SortByID(ByVal teams As List(Of CrewTrainingManager.DriverTeam), Optional ByVal IFDESC As Boolean = False)           '按照工作时间排序
        If teams.Count > 1 Then
            For i As Integer = 0 To teams.Count - 2
                For j As Integer = i + 1 To teams.Count - 1
                    Dim tempDri As CrewTrainingManager.DriverTeam = Nothing
                    If teams(j).TeamNo < teams(i).TeamNo Then
                        tempDri = teams(i)
                        teams(i) = teams(j)
                        teams(j) = tempDri
                    End If
                Next
            Next
        End If
    End Sub

    Public Function CanTaketheDuty(ByVal driver As Coordination2.Driver, ByVal duty As Coordination2.CSDriver, ByVal DutyDate As Date) As Boolean          '判断司机当前是否能够担任该白班任务
        CanTaketheDuty = False
        Dim dayJob As Coordination2.DriverDayJob = driver.DriverDayJobs.Find(Function(value As Coordination2.DriverDayJob)
                                                                                 Return value.Date.Date = DutyDate.Date
                                                                             End Function)
        If dayJob.DutySort = "年休" OrElse dayJob.DutySort = "培训" Then
            CanTaketheDuty = False
            Exit Function
        End If
        If dayJob.CSDriverNo = "" OrElse dayJob.CSDriverNo = "无任务" Then
            CanTaketheDuty = True
        End If
    End Function

    Public Function CanTaketheCDuty(ByVal driver As Coordination2.Driver, ByVal duty As Coordination2.CSDriver, ByVal DutyDate As Date) As Boolean          '判断司机当前是否能够担任该白班任务
        CanTaketheCDuty = False
        Dim dayJob As Coordination2.DriverDayJob = driver.DriverDayJobs.Find(Function(value As Coordination2.DriverDayJob)
                                                                                 Return value.Date.Date = DutyDate.Date
                                                                             End Function)
        If dayJob.DutySort = "年休" OrElse dayJob.DutySort = "培训" Then
            CanTaketheCDuty = False
            Exit Function
        End If
        If dayJob.CSDriverNo = "" OrElse dayJob.CSDriverNo = "无任务" Then
            CanTaketheCDuty = True
        End If
    End Function

    Public Function CanTaketheDuty(ByVal driver As Coordination2.Driver, ByVal duty As AMDriver, ByVal DutyDate As Date) As Boolean          '判断司机当前是否能够担任该夜早班任务,所给日期为夜班日期
        CanTaketheDuty = False
        Dim dutytime As Date = CDate(Coordination2.Global.BeTime(duty.StartTime))
        Dim CulDutyDate As Date
        If duty.ADriver IsNot Nothing Then
            CulDutyDate = New Date(DutyDate.Year, DutyDate.Month, DutyDate.Day, dutytime.Hour, dutytime.Minute, dutytime.Second)
        Else
            CulDutyDate = New Date(DutyDate.AddDays(1).Year, DutyDate.AddDays(1).Month, DutyDate.AddDays(1).Day, dutytime.Hour, dutytime.Minute, dutytime.Second)
        End If
        If driver.CulTime < DutyDate Then     '时间上满足
            CanTaketheDuty = True
        End If
    End Function

    Public Sub AddCSDriver(ByVal driver As Coordination2.Driver, ByVal duty As AMDriver, ByVal DutyDate As Date)
        If duty Is Nothing Then
            Exit Sub
        End If
        '添加夜早班联合任务
        If duty IsNot Nothing AndAlso duty.ADriver IsNot Nothing AndAlso duty.MDriver Is Nothing Then
            Dim dayindex As Integer = driver.DriverDayJobs.FindIndex(Function(value As Coordination2.DriverDayJob)
                                                                         Return value.Date.Date = DutyDate.Date
                                                                     End Function)
            driver.AddCSDriver(duty.ADriver, dayindex, "", False)
            duty.ADriver.FlagDinner = True
        ElseIf duty IsNot Nothing AndAlso duty.ADriver Is Nothing AndAlso duty.MDriver IsNot Nothing Then
            Dim dayindex As Integer = driver.DriverDayJobs.FindIndex(Function(value As Coordination2.DriverDayJob)
                                                                         Return value.Date.Date = DutyDate.AddDays(1).Date
                                                                     End Function)
            driver.AddCSDriver(duty.MDriver, dayindex, "", False)
            duty.MDriver.FlagDinner = True
        Else
            Dim dayindex As Integer = driver.DriverDayJobs.FindIndex(Function(value As Coordination2.DriverDayJob)
                                                                         Return value.Date.Date = DutyDate.Date
                                                                     End Function)
            driver.AddCSDriver(duty.ADriver, dayindex, "", False)
            dayindex = driver.DriverDayJobs.FindIndex(Function(value As Coordination2.DriverDayJob)
                                                          Return value.Date.Date = DutyDate.AddDays(1).Date
                                                      End Function)
            driver.AddCSDriver(duty.MDriver, dayindex, "", False)
            duty.ADriver.FlagDinner = True
            duty.MDriver.FlagDinner = True
        End If
    End Sub

    Public Sub AddCSDriver(ByVal driver As Coordination2.Driver, ByVal duty As Coordination2.CSDriver, ByVal DutyDate As Date)          '添加夜早班联合任务
        Dim dayindex As Integer = driver.DriverDayJobs.FindIndex(Function(value As Coordination2.DriverDayJob)
                                                                     Return value.Date.Date = DutyDate.Date
                                                                 End Function)
        driver.AddCSDriver(duty, dayindex, "", False)
        'If duty.DutySort = "休息" Then
        '    driver.XiuxiFengBanNum += 1
        'End If
        duty.FlagDinner = True
    End Sub

    Public Function LoadDuty(ByVal LineID As String, ByVal CSTimeTableID As String, ByVal CrewNo As String, ByVal DutySort As String) As List(Of CS_CSMaker.CSLinkTrain)
        Dim tempLinktrains As New List(Of CS_CSMaker.CSLinkTrain)
        Dim str As String
        Dim temptab As DataTable
        str = "select * from cs_result_prepareddutyinf t where t.lineid='" & LineID & "' and t.cstimetableid='" & CSTimeTableID & "' and t.dutysort='" & DutySort & "' and t.outputcsdriverno='" & CrewNo & "'"
        temptab = Globle.Method.ReadDataForAccess(str)
        If temptab IsNot Nothing AndAlso temptab.Rows.Count > 0 Then
            For Each row As DataRow In temptab.Rows
                Dim tempsegment As New CS_CSMaker.CSLinkTrain
                tempsegment.DutySort = row.Item("dutysort").ToString
                tempsegment.OutputCheCi = "无"
                tempsegment.StartTime = row.Item("starttime")
                tempsegment.StartStaName = row.Item("place").ToString
                tempsegment.EndTime = row.Item("endtime")
                tempsegment.EndStaName = row.Item("place").ToString
                tempsegment.distance = 0
                tempLinktrains.Add(tempsegment)
            Next
        Else
            str = "select * from cs_result_preparedtraininf t where t.lineid='" & LineID & "' and t.cstimetableid='" & CSTimeTableID & "' and t.dutysort='" & DutySort & "' and t.outputcsdriverno='" & CrewNo & "'"
            temptab = Globle.Method.ReadDataForAccess(str)
            If temptab IsNot Nothing AndAlso temptab.Rows.Count > 0 Then
                For Each row As DataRow In temptab.Rows
                    Dim tempsegment As New CS_CSMaker.CSLinkTrain
                    tempsegment.DutySort = row.Item("dutysort").ToString
                    tempsegment.OutputCheCi = "无"
                    tempsegment.StartTime = row.Item("starttime")
                    tempsegment.StartStaName = row.Item("place").ToString
                    tempsegment.EndTime = row.Item("endtime")
                    tempsegment.EndStaName = row.Item("place").ToString
                    tempsegment.distance = 0
                    tempLinktrains.Add(tempsegment)
                Next
            Else
                str = "select t.*,m.outputcsdriverno from cs_crewschedule t,cs_workload m where t.cstimetableid=m.cstimetableid and t.driverno=m.driverno and t.lineid='" & LineID & _
                    "' and t.cstimetableid='" & CSTimeTableID & "' and t.dutysort='" & DutySort & "' and m.outputcsdriverno='" & CrewNo & "' order by t.id"
                temptab = Globle.Method.ReadDataForAccess(str)
                If temptab.Rows.Count > 0 Then
                    For i As Integer = 0 To temptab.Rows.Count - 1
                        Dim tempsegment As New CS_CSMaker.CSLinkTrain
                        tempsegment.DutySort = temptab.Rows(i).Item("dutysort").ToString
                        tempsegment.OutputCheCi = temptab.Rows(i).Item("trainno").ToString
                        tempsegment.StartTime = temptab.Rows(i).Item("starttime")
                        tempsegment.StartStaName = temptab.Rows(i).Item("startstaname").ToString
                        tempsegment.EndTime = temptab.Rows(i).Item("endtime")
                        tempsegment.EndStaName = temptab.Rows(i).Item("endstaname").ToString
                        tempsegment.UpOrDown = temptab.Rows(i).Item("upordown")
                        tempsegment.distance = temptab.Rows(i).Item("distance")
                        tempsegment.sCheDiHao = temptab.Rows(i).Item("vehicleno")
                        tempsegment.STASEQ1 = temptab.Rows(i).Item("staseq1")
                        tempsegment.STASEQ2 = temptab.Rows(i).Item("staseq2")
                        If temptab.Rows(i).Item("dateno") = -1 Then
                            tempsegment.IsDeadHeading = True
                        Else
                            tempsegment.IsDeadHeading = False
                        End If
                        tempLinktrains.Add(tempsegment)
                    Next
                    MergeDuty(tempLinktrains)        '合并某些任务
                End If
            End If
        End If
        LoadDuty = tempLinktrains
    End Function

    Public Sub MergeDuty(ByVal linktrains As List(Of CS_CSMaker.CSLinkTrain))
        If linktrains.Count > 0 Then
            For i As Integer = linktrains.Count - 1 To 1 Step -1
                If linktrains(i).CSTrainID = linktrains(i - 1).CSTrainID AndAlso linktrains(i).UpOrDown = linktrains(i - 1).UpOrDown Then
                    linktrains(i - 1).EndStaName = linktrains(i).EndStaName
                    linktrains(i - 1).EndTime = linktrains(i).EndTime
                    linktrains(i - 1).STASEQ2 = linktrains(i).STASEQ2
                    linktrains(i - 1).distance += linktrains(i).distance
                    linktrains.RemoveAt(i)
                End If
            Next
        End If
    End Sub

    Public Function GetCSDriverFromTimetable(ByVal CSDriverno As String, ByVal DutySort As String, ByVal CSTimetable As Coordination2.CSTimeTable) As Coordination2.CSDriver
        GetCSDriverFromTimetable = Nothing
        For Each dri As Coordination2.CSDriver In CSTimetable.MCSDrivers
            If dri.CSdriverNo = CSDriverno AndAlso dri.DutySort = DutySort Then
                GetCSDriverFromTimetable = dri
                Exit For
            End If
        Next
        For Each dri As Coordination2.CSDriver In CSTimetable.NCSDrivers
            If dri.CSdriverNo = CSDriverno AndAlso dri.DutySort = DutySort Then
                GetCSDriverFromTimetable = dri
                Exit For
            End If
        Next
        For Each dri As Coordination2.CSDriver In CSTimetable.ACSDrivers
            If dri.CSdriverNo = CSDriverno AndAlso dri.DutySort = DutySort Then
                GetCSDriverFromTimetable = dri
                Exit For
            End If
        Next
        For Each dri As Coordination2.CSDriver In CSTimetable.CCSDrivers
            If dri.CSdriverNo = CSDriverno AndAlso dri.DutySort = DutySort Then
                GetCSDriverFromTimetable = dri
                Exit For
            End If
        Next
    End Function

    Public Function GetCSDriverFromTimetableByOutPutNo(ByVal CSDriverno As String, ByVal DutySort As String, ByVal CSTimetable As Coordination2.CSTimeTable) As Coordination2.CSDriver
        GetCSDriverFromTimetableByOutPutNo = Nothing
        For Each dri As Coordination2.CSDriver In CSTimetable.MCSDrivers
            If dri.OutPutCSDriverNo = CSDriverno AndAlso dri.DutySort = DutySort Then
                GetCSDriverFromTimetableByOutPutNo = dri
                Exit For
            End If
        Next
        For Each dri As Coordination2.CSDriver In CSTimetable.NCSDrivers
            If dri.OutPutCSDriverNo = CSDriverno AndAlso dri.DutySort = DutySort Then
                GetCSDriverFromTimetableByOutPutNo = dri
                Exit For
            End If
        Next
        For Each dri As Coordination2.CSDriver In CSTimetable.ACSDrivers
            If dri.OutPutCSDriverNo = CSDriverno AndAlso dri.DutySort = DutySort Then
                GetCSDriverFromTimetableByOutPutNo = dri
                Exit For
            End If
        Next
        For Each dri As Coordination2.CSDriver In CSTimetable.CCSDrivers
            If dri.OutPutCSDriverNo = CSDriverno AndAlso dri.DutySort = DutySort Then
                GetCSDriverFromTimetableByOutPutNo = dri
                Exit For
            End If
        Next
    End Function

End Module
