Public Class CSTimeTable

    Private _ID As String
    Public Property ID() As String
        Get
            Return _ID
        End Get
        Set(ByVal value As String)
            _ID = value
        End Set
    End Property
    Private _LineID As String
    Public Property LineID() As String
        Get
            Return _LineID
        End Get
        Set(ByVal value As String)
            _LineID = value
        End Set
    End Property
    Private _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property

    Private _CreateDate As String
    Public Property CreateDate() As String
        Get
            Return _CreateDate
        End Get
        Set(ByVal value As String)
            _CreateDate = value
        End Set
    End Property
    Private _EditDate As String
    Public Property EditDate() As String
        Get
            Return _EditDate
        End Get
        Set(ByVal value As String)
            _EditDate = value
        End Set
    End Property

    Private _DiagramID As String
    Public Property DiagramID() As String
        Get
            Return _DiagramID
        End Get
        Set(ByVal value As String)
            _DiagramID = value
        End Set
    End Property
    Public CSDrivers As List(Of CSDriver)
    Public MCSDrivers As CSDriversGroup
    Public NCSDrivers As CSDriversGroup
    Public ACSDrivers As CSDriversGroup
    Public PreparedTrainCSdrivers As CSDriversGroup

    Public Sub New()

    End Sub

    Public Sub LoadCSDrivers()
        Dim DaTab As New DataTable
        Dim DaAdapter As New OleDb.OleDbDataAdapter
        Dim i, j, flag As Integer
        Dim Str As String
        'If CrewSchedulingLinker.State = ConnectionState.Closed Then
        '    CrewSchedulingLinker.Open()
        'End If
        If strQCurCSPlanName = "" Then
            'MsgBox("请先选择乘务计划！", MsgBoxStyle.OkOnly)
            Exit Sub
        End If
        strQCurCSPlanID = GetCSPlanIDFromCSPlanName(strQCurCSPlanName)
        Str = "SELECT t.*,d.*  FROM  CS_WORKLOAD t,cs_dinnerinf d  WHERE t.LINEID='" & CStr(Me.LineID) & "' AND t.CSTIMETABLEID='" & CStr(Me.ID) & "' and t.lineid=d.lineid and t.cstimetableid=d.cstimetableid and t.driverno=d.driverno order by  t.DriverNo"
        'COUNT(distinct DriverNo) AS nDriverNum 
        DaTab = New DataTable
        DaTab = Globle.Method.ReadDataForAccess(Str)

        'nDriverNum = CInt(DaTab.Rows(0).Item("nDriverNum").ToString.Trim)

        'nDriverNum = DaTab.Rows.Count
        If DaTab.Rows.Count = 0 Then
            MsgBox("没有数据")
            Exit Sub
        Else
            Me.CSDrivers = New List(Of CSDriver)
            For i = 1 To DaTab.Rows.Count
                CSDrivers(i) = New CSDriver()
                CSDrivers(i).CSDriverID = i
                CSDrivers(i).CSdriverNo = DaTab.Rows(i - 1).Item("DriverNo").ToString
                CSDrivers(i).DutySort = DaTab.Rows(i - 1).Item("DUTYSORT").ToString
                'CSDrivers(i).ZFTime = DaTab.Rows(i - 1).Item("totalzftime").ToString
                CSDrivers(i).TotalDriveTime = DaTab.Rows(i - 1).Item("worktime").ToString
                'CSDrivers(i).TotalDayDriveTime = DaTab.Rows(i - 1).Item("drivetime").ToString
                'CSDrivers(i).DriveDistance = DaTab.Rows(i - 1).Item("drivedistance").ToString
                CSDrivers(i).PreDutyTime = Val(DaTab.Rows(i - 1).Item("PREPAREDUTYTIME").ToString)
                'CSDrivers(i).BeginWorkTime = Val(DaTab.Rows(i - 1).Item("BEGINDUTYTIME").ToString)
                'CSDrivers(i).EndEorkTime = Val(DaTab.Rows(i - 1).Item("endDUTYTIME").ToString)
                CSDrivers(i).DinnerStartTime = Val(DaTab.Rows(i - 1).Item("DINNERBEGINTIME").ToString)
                CSDrivers(i).DinnerEndTime = Val(DaTab.Rows(i - 1).Item("DINNERENDTIME").ToString)
                CSDrivers(i).DinnerStation = DaTab.Rows(i - 1).Item("DINNERPLACE").ToString

                ReDim CSDrivers(i).CSLinkTrain(0)
            Next


            Str = "SELECT * FROM  CS_CREWSCHEDULE WHERE LINEID='" & CStr(Me.LineID) & "' AND CSTIMETABLEID='" & CStr(Me.ID) & "'  order by ID "
            DaTab = New DataTable
            DaTab = Globle.Method.ReadDataForAccess(Str)

            ReDim CSTrainsAndDrivers.CSLinkTrains(0)
            For i = 1 To DaTab.Rows.Count
                Dim TempCSLinkTrain As New CSLinkTrain
                TempCSLinkTrain.CSTrainID = i
                TempCSLinkTrain.OutputCheCi = DaTab.Rows(i - 1).Item("TrainNo").ToString.Trim
                TempCSLinkTrain.StartStaName = DaTab.Rows(i - 1).Item("StartStaName").ToString.Trim
                TempCSLinkTrain.StartTime = CInt(DaTab.Rows(i - 1).Item("StartTime").ToString.Trim)
                TempCSLinkTrain.StartStaID = CSFromStaNameToStaIDByStationinf(TempCSLinkTrain.StartStaName, DaTab.Rows(i - 1).Item("STASEQ1").ToString.Trim)
                TempCSLinkTrain.STASEQ1 = DaTab.Rows(i - 1).Item("STASEQ1").ToString.Trim
                TempCSLinkTrain.STASEQ2 = DaTab.Rows(i - 1).Item("STASEQ2").ToString.Trim
                TempCSLinkTrain.EndStaName = DaTab.Rows(i - 1).Item("EndStaName").ToString.Trim
                TempCSLinkTrain.EndTime = CInt(DaTab.Rows(i - 1).Item("EndTime").ToString.Trim)
                TempCSLinkTrain.EndStaID = CSFromStaNameToStaIDByStationinf(TempCSLinkTrain.EndStaName, DaTab.Rows(i - 1).Item("STASEQ2").ToString.Trim)
                TempCSLinkTrain.nCheDiID = CInt(DaTab.Rows(i - 1).Item("CheDiID").ToString.Trim)
                TempCSLinkTrain.nPathID1 = CInt(DaTab.Rows(i - 1).Item("Path1").ToString.Trim)
                TempCSLinkTrain.nPathID2 = CInt(DaTab.Rows(i - 1).Item("Path2").ToString.Trim)
                TempCSLinkTrain.nTrainID = CInt(DaTab.Rows(i - 1).Item("TrainID").ToString.Trim)
                TempCSLinkTrain.UpOrDown = CInt(DaTab.Rows(i - 1).Item("UpOrDown").ToString.Trim)
                TempCSLinkTrain.ZFTime = CInt(DaTab.Rows(i - 1).Item("ZFTime").ToString.Trim)
                TempCSLinkTrain.CulStartTime = AddLitterTime(TempCSLinkTrain.StartTime)
                TempCSLinkTrain.CulEndTime = AddLitterTime(TempCSLinkTrain.EndTime)
                TempCSLinkTrain.distance = CDbl(DaTab.Rows(i - 1).Item("distance").ToString.Trim)
                'TempCSLinkTrain.RunTime = TempCSLinkTrain.CulEndTime - TempCSLinkTrain.CulStartTime
                TempCSLinkTrain.sCheDiHao = DaTab.Rows(i - 1).Item("VEHICLENO").ToString.Trim
                If DaTab.Rows(i - 1).Item("DriverNo").ToString = "##" Then
                    TempCSLinkTrain.IsLinked = False
                Else
                    TempCSLinkTrain.IsLinked = True
                End If
                ReDim Preserve CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains) + 1)
                CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains)) = TempCSLinkTrain


                '赋给驾驶员
                For j = 0 To CSDrivers.Count - 1
                    flag = 0
                    If CSDrivers(j).CSdriverNo = DaTab.Rows(i - 1).Item("DriverNo").ToString Then
                        flag = j
                        Exit For
                    End If
                Next

                'CSLinkTrain(UBound(CSLinkTrain)).EndTime = CInt(DaTab.Rows(i - 1).Item("EndTime").ToString.Trim)

                If flag > 0 Then
                    ReDim Preserve CSDrivers(flag).CSLinkTrain(UBound(CSDrivers(flag).CSLinkTrain) + 1)
                    CSDrivers(flag).CSLinkTrain(UBound(CSDrivers(flag).CSLinkTrain)) = TempCSLinkTrain
                End If
            Next

            'For Each driver As CSDriver In CSDrivers
            '    If driver Is Nothing = False Then
            '        driver.ReCulDriverWorkLoad()
            '    End If
            'Next
        End If
    End Sub



End Class




Public Class CSDriversGroup
    Inherits List(Of CSDriver)
    Private _Name As String
    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
        End Set
    End Property


    Private _AveTime As Integer
    Public Property AveTime() As Integer
        Get
            _AveTime = 0
            If Me.Count > 0 Then
                Dim TotalDriveTimeList As List(Of Integer) = New List(Of Integer)

                For Each driver As CSDriver In Me
                    TotalDriveTimeList.Add(driver.TotalDriveTime)
                Next

                _AveTime = TotalDriveTimeList.Average
            End If
            Return _AveTime
        End Get
        Set(ByVal value As Integer)
            _AveTime = value
        End Set
    End Property
    Private _MaxTime As Integer
    Public Property MaxTime() As Integer
        Get
            _MaxTime = 0
            If Me.Count > 0 Then
                Dim TotalDriveTimeList As List(Of Integer) = New List(Of Integer)

                For Each driver As CSDriver In Me
                    TotalDriveTimeList.Add(driver.TotalDriveTime)
                Next

                _MaxTime = TotalDriveTimeList.Max
            End If
            Return _MaxTime
        End Get
        Set(ByVal value As Integer)
            _MaxTime = value
        End Set
    End Property
    Private _MinTime As Integer
    Public Property MinTime() As Integer
        Get
            _MinTime = 0
            If Me.Count > 0 Then
                Dim TotalDriveTimeList As List(Of Integer) = New List(Of Integer)

                For Each driver As CSDriver In Me
                    TotalDriveTimeList.Add(driver.TotalDriveTime)
                Next

                _MinTime = TotalDriveTimeList.Min
            End If
            Return _MinTime
        End Get
        Set(ByVal value As Integer)
            _MinTime = value
        End Set
    End Property
    Private _VarTime As Integer
    Public Property VarTime() As Integer
        Get
            _VarTime = 0
            If Me.Count > 0 Then
                Dim TotalDriveTimeList As List(Of Integer) = New List(Of Integer)
                Dim tempAveTime As Integer = Me.AveTime

                For Each s As Integer In TotalDriveTimeList
                    _VarTime = _VarTime + (Math.Abs(CInt(s) - AveTime)) ^ 2
                Next

                _VarTime = Math.Sqrt(_VarTime / Me.Count)
            End If
            Return _VarTime
        End Get
        Set(ByVal value As Integer)
            _VarTime = value
        End Set
    End Property
    Public Sub New()

    End Sub

    Public Sub New(ByVal tempName As String)
        Me.Name = tempName
    End Sub
End Class
