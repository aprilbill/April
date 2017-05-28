'Imports Microsoft.Office.Interop
Imports System
Imports System.Data
Imports System.IO
Public Module ModCrewSchedulingMainDim
    '======================================================================
    Public ConDriveTime As Integer '连续驾车时间
    Public ConZaoDriveTime As Integer '连续驾车时间
    'Public RestTime As Integer '休息时间
    Public ForceDriveLength As Integer = 1 '是否满足驾驶公里约束,0为是，1为否
    Public CS_MorningMaxLength As Decimal      '早班最长驾驶公里
    Public CS_DayMaxLength As Decimal          '白班最长驾驶公里
    Public CS_CDayMaxLength As Decimal         '日勤班最长驾驶公里
    Public CS_NightMaxLength As Decimal        '夜班最长驾驶公里
    Public CS_MorningMinLength As Decimal      '早班最短驾驶公里
    Public CS_DayMinLength As Decimal          '白班最短驾驶公里
    Public CS_CDayMinLength As Decimal         '日勤班最短驾驶公里
    Public CS_NightMinLength As Decimal        '夜班最短驾驶公里

    Public MornWorkTime As Integer '早班工作时间的总约束
    Public NoonWorkTime As Integer '白班工作时间的总约束
    Public NightWorkTime As Integer '夜班工作时间的总约束

    Public tempTable As New Data.DataTable
    Public ForceDutyTime As Integer = 0 '是否强制满足上班时间，0为否，1为是
    Public MStartTime As Integer '早班开始时间
    Public NStartTime As Integer '日班开始时间
    Public AStartTime As Integer '夜班开始时间
    Public PrepareTrainTime As Integer
    Public PrepareDutyTime As Integer '出勤备班时间
    Public PrepareDutyOffTime As Integer '退勤备班时间

    Public minRuDis As Integer = 2 '入库合并最小的距离
    Public minChuDis As Integer = 2 '出库合并最小的距离

    '查询
    Public strQCurCSPlanName As String '当前乘务计划名称
    Public strQCurCSPlanID As String '当前乘务计划ID
    Public strCurlineID As String  '当前线路编号
    Public strDiagram As String '当前运行图ID
    Public HouBeiDinner As Integer

    Public DiagramCurID As String '当前选择的列车运行图的DiagramID
    Public UsefulSta() As String
    Public sState As String '="乘务计划编制"与“乘务计划查询与调整”
    Public blnCanChangePlace As Boolean = False

    Public ErrorInfoList As List(Of String)         '记录未能找到距离信息的区间

    Structure typeRestInterval
        Dim BEGINTIME As Integer '开始时间
        Dim ENDTIME As Integer '终止时间
        Dim INTERVAL As Integer  '间隔
    End Structure
    Public RestInterval() As typeRestInterval '各个时段的休息间隔
    <Serializable()> _
    Public Class typeDinnerStation
        Public DinnerStationName As String
        Public DinnerStationID As Integer
        Public Routing As String
        Public Direction As Integer  '上行0，下行1，双向2
        Public RiDirection As Integer  '上行0，下行1，双向2
        Public NeedDinnerDriverNum As Integer
        Public RealDinnerDriverNum As Integer
        Public AddNewDinnerDriverNum As Integer
        Public IfOnlyDinner As Boolean = False '是否只替饭
        Public dinnerMergedCSLinkTrainList As List(Of MergedCSLinkTrain)
        Public dinnerStartTime As Integer
        Public dinnerEndTime As Integer
        Public DinnerTime As Integer
        Public dutySort As String
        Public dinnerType As String
        Public Sub New()

        End Sub
        Public Sub New(ByVal _dutysort As String, ByVal _routing As String, ByVal _staname As String, ByVal _starttime As Integer, ByVal _endtime As Integer, ByVal _dinnertime As Integer)
            dutySort = _dutysort
            Routing = _routing
            dinnerStartTime = _starttime
            dinnerEndTime = _endtime
            DinnerStationName = _staname
            DinnerTime = _dinnertime
        End Sub
    End Class
    Public sysDinnerStation() As typeDinnerStation '用餐的车站

    Structure typeShiftStation
        Dim ShiftStationStationName As String
        Dim ShiftStationStationID As Integer
        Dim Direction As Integer  '上行0，下行1，双向2
        Dim DayDutyStartTime As Integer     '开始时间
        Dim DayDutyEndTime As Integer     '结束时间
        Dim Routing As String '交路
        Dim AvaliableOffPlaces() As String        '可以退勤的 交路:地点
    End Structure
    Public ShiftPlace() As typeShiftStation '上班点
    Public CDayShiftPlace() As typeShiftStation '日勤版上班点

    Public Class ChangeStation '轮换点，非上下班
        Public JiaoLuName As String '交路名称
        Public Name As String
        Public Direction As Integer '上行0，下行1，双向2
        Public UpTrainDirection As Integer '上行0，下行1，双向2
        Public TimeSpanList As clsTimeSpan
        Public RestTime As Integer '休息时间
        Public IfMustChange As Boolean = False
        Public FollowNo As Integer '轮换的人数 0为按时间来
        Public Sub New(ByVal _JiaoLuName As String, ByVal _name As String, ByVal _direction As Integer, ByVal _upTraindirection As Integer, ByVal _restTime As Integer)
            Me.Direction = _direction
            Me.UpTrainDirection = _upTraindirection
            Me.JiaoLuName = _JiaoLuName
            Me.Name = _name
            Me.RestTime = _restTime
        End Sub
        Public Sub New()

        End Sub
    End Class
    Public Class clsTimeSpan
        Public StartTime As Integer
        Public EndTime As Integer
        Public Sub New(ByVal st As DateTime, ByVal et As DateTime)
            Me.StartTime = st.TimeOfDay.TotalSeconds
            Me.EndTime = et.TimeOfDay.TotalSeconds
            If Me.EndTime < Me.StartTime Then                   '相等的话认为跨越24小时
                Me.EndTime = Me.EndTime + 86400
            End If
        End Sub
        Public Sub New()

        End Sub
    End Class
    Public ChangeStationList As List(Of ChangeStation) '轮换的车站

    Structure typeJiaolu
        Dim JiaoluName As String
        Dim CrewType As Integer '单独0，混合1
        Dim StartStationName As String '起始站名
        Dim EndStationName As String '结束站名
        Dim ReJiaoluName As String '逆向交路名称
    End Structure
    Public Jiaolu() As typeJiaolu '交路匹配

    '======================================================================

    Public CSCrewNumber As Integer '每班的基本乘务员数量，主要是最少人数排班时用
    Public CSGivenTotalNumber As Integer '给定排班的总人数
    Public CSGivenNumber As Integer '给定排班的人数
    Public EachCrewNum As Integer '每班的基本乘务员数量，最少人数排班时为CSCrewNumber，给定人数时为给定人数CSGivenNumber
    Public chedi As Integer '取大的车底

    Public Fanhui As Boolean = False
    Public CSBasicCrewTotalNumber As Integer '所有基本乘务员总量
    Public DateNumber As Integer '排班的天数
    Public IFIntelliConnect As Boolean = False    '是否智能连接
    Structure typeDriver
        Dim DriverID As Integer  '司机编号
        Dim DriverNo As String  '司机编号
        Dim DriverDayWorkTime As Integer '记录每天司机的驾车总时间
        'Dim DutyDate() As typeDutyDate '每天的实际作业情况
        Dim TotalZFTime As Integer '记录每天司机的折返总时间
        Dim DinnerStartTime As Integer
        Dim DinnerEndTime As Integer
        Dim CheciArray As ArrayList
        Dim DutyName As String '班种，白班or夜班、休息
        Dim StartTime As Integer '班的开始时间
        Dim EndTime As Integer '班的结束时间
    End Structure
    Public Driver() As typeDriver '司机集合
    Public HouBeiDriver() As typeDriver
    Public HouBeiDinnerDriver() As typeDriver '以时间计数
    Public CSWETTChedi() As typeCSCheDi
    Public SectionDistanceList As List(Of SectionDistance)

    <Serializable()> _
    Public Class CSTrainAndDrivers
        Public CSChedi() As typeCSCheDi
        Public CSDrivers() As CSDriver
        Public CSLinkTrains() As CSLinkTrain
        Public MergedCSLinkTrains() As MergedCSLinkTrain
        ''' <summary>
        ''' key车站名称到达或出发,value：List(Of MergedCSLinkTrain)
        ''' </summary>
        ''' <remarks></remarks>
        Public dicStationTrain As Dictionary(Of String, List(Of MergedCSLinkTrain))
        'Public MergedCSLinkTrains As List(Of MergedCSLinkTrain)
        Public DayDrivers As List(Of CSDriver)          '白班司机
        Public CDayDrivers As List(Of CSDriver)          '日勤班司机
        Public NightDrivers As List(Of CSDriver)          '夜班司机
        Public MorningDrivers As List(Of CSDriver)         '早班司机
        Public OtherDrivers As List(Of CSDriver)           '其他司机
        Public CorCSDrivers() As CSDriver
        Public CorCSLinkTrains() As CSLinkTrain
        Public CorDayDrivers As List(Of CSDriver)          '白班司机
        Public CorCDayDrivers As List(Of CSDriver)          '日勤班司机
        Public CorNightDrivers As List(Of CSDriver)          '夜班司机
        Public CorMorningDrivers As List(Of CSDriver)         '早班司机
        Public CorOtherDrivers As List(Of CSDriver)           '其他司机
        Public CorPreParedTrainDrivers As List(Of CSDriver)        '备车任务
        Public CorPreParedDutyDrivers As List(Of CSDriver)        '备班任务
        Public CorCSTimetableID As String = ""                     '衔接图的ID
        Public PostiveCorCSPlans As List(Of CorCSPlan)         '平日衔接双休
        Public NegtiveCorCSPlans As List(Of CorCSPlan)         '双休衔接平日
        Public ScheduleState As CrewScheduleState          '编制状态
        Public IfCorSchedule As Boolean = False
        Public CorCSPlanName As String = ""
        Public CurEditDriver As CSDriver           '当前手工编制的任务
        Public AreaInfoS As List(Of AreaInfo)

        Public PreParedTrainDrivers As List(Of CSDriver)        '备车任务
        Public PreParedDutyDrivers As List(Of CSDriver)        '备班任务

        Public Sub New()
            ReDim CSChedi(0)
            ReDim CSDrivers(0)
            ReDim CSLinkTrains(0)
            ReDim MergedCSLinkTrains(0)
            ScheduleState = CrewScheduleState.未初始化
            DayDrivers = New List(Of CSDriver)          '白班司机
            CDayDrivers = New List(Of CSDriver)          '日勤班司机
            NightDrivers = New List(Of CSDriver)          '夜班司机
            MorningDrivers = New List(Of CSDriver)
            OtherDrivers = New List(Of CSDriver)
            PreParedTrainDrivers = New List(Of CSDriver)
            PreParedDutyDrivers = New List(Of CSDriver)
            PostiveCorCSPlans = New List(Of CorCSPlan)         '平日衔接双休
            NegtiveCorCSPlans = New List(Of CorCSPlan)         '双休衔接平日
            ReDim CorCSDrivers(0)
            ReDim CorCSLinkTrains(0)
            CorDayDrivers = New List(Of CSDriver)          '白班司机
            CorCDayDrivers = New List(Of CSDriver)          '日勤班司机
            CorNightDrivers = New List(Of CSDriver)          '夜班司机
            CorMorningDrivers = New List(Of CSDriver)
            CorOtherDrivers = New List(Of CSDriver)
            CorPreParedTrainDrivers = New List(Of CSDriver)
            CorPreParedDutyDrivers = New List(Of CSDriver)

        End Sub
    End Class

    Public Enum CrewScheduleState
        未初始化 = 1
        初始化 = 2
        早班 = 3
        白班 = 4
        夜班 = 5
        日勤班 = 6
        完成 = 7
    End Enum

    Public CSTrainsAndDrivers As New CSTrainAndDrivers            '记录车底信息、列车信息、司机信息，便于序列化存储

    Public Enum DutySort
        白班
        早班
        夜班
    End Enum

    '====================以下遗传算法代码
    Public Class Schedule
        Public Binarycoding As String
        Public Value As Double             '计划值
        Public CrewNum As Integer          '乘务员数量
        Public Feasiable As Boolean        '可行性

        Public Sub New(ByVal drivers As List(Of CSDriver), ByVal ran As Random)
            Binarycoding = ""
            For i As Integer = 0 To drivers.Count - 1
                Dim Bin As Integer = ran.Next(0, 2)
                Binarycoding &= Bin.ToString
            Next
        End Sub

        Public Sub New()
            Binarycoding = ""
            Value = 0
        End Sub

        Public Sub GetValue(ByVal drivers As List(Of CSDriver), ByVal trains As List(Of CSLinkTrain))
            Dim NonZeroCount As Integer = 0

            '=======================检查所有的列是否被覆盖且仅覆盖一次
            Dim SelectDrivers As New List(Of CSDriver)
            For i As Integer = 0 To Binarycoding.Length - 1
                Dim str As String = Binarycoding.Substring(i, 1)
                If str = "1" Then
                    NonZeroCount += 1
                    SelectDrivers.Add(drivers(i))
                End If
            Next

            Dim FitTrainNum As Integer = 0
            Dim UnfitTrainNum As Integer = 0
            For Each train As CSLinkTrain In trains
                Dim CoverCount As Integer = 0
                For Each dri As CSDriver In SelectDrivers
                    For i As Integer = 1 To UBound(dri.CSLinkTrain)
                        If dri.CSLinkTrain(i) Is train Then
                            CoverCount += 1
                        End If
                    Next
                Next
                If CoverCount = 1 Then
                    FitTrainNum += 1
                Else
                    UnfitTrainNum += 1
                End If
            Next
            If UnfitTrainNum = 0 Then
                Feasiable = True
            End If
            CrewNum = NonZeroCount
            Value = NonZeroCount + UnfitTrainNum * 20
        End Sub

        Public Sub UpdateSchedule(ByVal drivers As List(Of CSDriver), ByVal trains As List(Of CSLinkTrain))
            Dim AllDrivers As New List(Of CSDriver)
            For Each dri As CSDriver In drivers       '复制搜有的Drivers
                AllDrivers.Add(dri)
            Next
            Dim SelectDrivers As New List(Of CSDriver)
            For i As Integer = 0 To Binarycoding.Length - 1
                Dim str As String = Binarycoding.Substring(i, 1)
                If str = "1" Then
                    SelectDrivers.Add(AllDrivers(i))
                End If
            Next
            '================剔除具有重复列车的Driver
            Call RemoveRepeatColumn(SelectDrivers)

            If SelectDrivers.Count > 0 Then
                For i As Integer = 0 To SelectDrivers.Count - 1
                    Call RemoveRepeatColumn(SelectDrivers(i), AllDrivers)
                Next
            End If
            '=======================覆盖未能覆盖的列
            While AllDrivers.Count > 0
                Dim ranindex As Integer = (New Random).Next(0, AllDrivers.Count)
                Dim BaseDri As CSDriver = AllDrivers(ranindex)
                SelectDrivers.Add(BaseDri)
                Binarycoding = Binarycoding.Remove(BaseDri.CSDriverID, 1)
                Binarycoding = Binarycoding.Insert(BaseDri.CSDriverID, "1")
                AllDrivers.Remove(BaseDri)
                Call RemoveRepeatColumn(BaseDri, AllDrivers)
            End While
            Call GetValue(drivers, trains)
        End Sub

        Public Shared Operator =(ByVal schedule1 As Schedule, ByVal schedule2 As Schedule) As Boolean
            Dim ifequel As Boolean
            If schedule1.Binarycoding = schedule2.Binarycoding Then
                ifequel = True
            Else
                ifequel = False
            End If
            Return ifequel
        End Operator

        Public Shared Operator <>(ByVal schedule1 As Schedule, ByVal schedule2 As Schedule) As Boolean
            Dim ifequel As Boolean
            If schedule1.Binarycoding = schedule2.Binarycoding Then
                ifequel = True
            Else
                ifequel = False
            End If
            Return (Not ifequel)
        End Operator

        Public Sub RemoveRepeatColumn(ByVal BaseDri As CSDriver, ByVal AllDrivers As List(Of CSDriver))        '移除集合中与基列有重复列车的Driver
            If AllDrivers.Count > 0 Then
                For j As Integer = AllDrivers.Count - 1 To 0 Step -1
                    Dim IfRemove As Boolean = False
                    For k As Integer = 1 To BaseDri.CSLinkTrain.Length - 1
                        For m As Integer = 1 To AllDrivers(j).CSLinkTrain.Length - 1
                            If AllDrivers(j).CSLinkTrain(m) Is BaseDri.CSLinkTrain(k) Then
                                IfRemove = True
                                GoTo Remove2
                            End If
                        Next
                    Next
Remove2:
                    If IfRemove Then
                        AllDrivers.RemoveAt(j)
                    End If
                Next
            End If
        End Sub

        Public Sub RemoveRepeatColumn(ByVal AllDrivers As List(Of CSDriver))        '移除集合中重复包含同一列车的Driver
            If AllDrivers.Count >= 2 Then
                Dim i As Integer = 0
                While i <= AllDrivers.Count - 2
                    Dim BaseDri As CSDriver = AllDrivers(i)
                    For j As Integer = AllDrivers.Count - 1 To i + 1 Step -1
                        Dim IfRemove As Boolean = False
                        For k As Integer = 1 To BaseDri.CSLinkTrain.Length - 1
                            For m As Integer = 1 To AllDrivers(j).CSLinkTrain.Length - 1
                                If AllDrivers(j).CSLinkTrain(m) Is BaseDri.CSLinkTrain(k) Then
                                    IfRemove = True
                                    GoTo Remove
                                End If
                            Next
                        Next
Remove:
                        If IfRemove Then
                            Binarycoding = Binarycoding.Remove(AllDrivers(j).CSDriverID, 1)
                            Binarycoding = Binarycoding.Insert(AllDrivers(j).CSDriverID, "1")
                            AllDrivers.RemoveAt(j)
                        End If
                    Next
                    i += 1
                End While
            End If
        End Sub

        Public Shared Function GetDriverValue(ByVal Driver As CSDriver) As Double
            Select Case Driver.DutySort
                Case "白班"

                Case "夜班"
            End Select
        End Function

    End Class

    <Serializable()> _
    Public Class AreaInfo              '分区轮转类
        Public LineName As String
        Public AreaName As String
        Public OnDutyPlaces() As String
        Public ForDutySorts() As String
        Public YunZhuanPara As String
        Public Sub New(ByVal _Linename As String, ByVal _AreaName As String)
            LineName = _Linename
            AreaName = _AreaName
            ReDim OnDutyPlaces(0)
            ReDim ForDutySorts(0)
            YunZhuanPara = ""
        End Sub
        Public Function IsAreaPlace(ByVal _StaName As String) As Boolean
            IsAreaPlace = False
            For Each place As String In OnDutyPlaces
                If place = _StaName Then
                    IsAreaPlace = True
                    Exit For
                End If
            Next
        End Function
    End Class

    ''' <summary>
    ''' 移除指定序号的司机
    ''' </summary>
    ''' <param name="index"></param>
    ''' <remarks></remarks>
    Public Sub RemoveDriver(ByVal index As Integer)
        If index > 0 AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
            Call RemoveCSDriverFromList(CSTrainsAndDrivers.CSDrivers(index))
            Dim newDrivers(0) As CSDriver
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                If i <> index Then
                    ReDim Preserve newDrivers(UBound(newDrivers) + 1)
                    newDrivers(UBound(newDrivers)) = CSTrainsAndDrivers.CSDrivers(i)
                    newDrivers(UBound(newDrivers)).CSDriverID = UBound(newDrivers)
                End If
            Next
            CSTrainsAndDrivers.CSDrivers = newDrivers
        End If
    End Sub

    ''' <summary>
    ''' 移除指定的司机
    ''' </summary>
    ''' <param name="dri"></param>
    ''' <remarks></remarks>
    Public Sub RemoveDriver(ByVal dri As CSDriver)
        If dri IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
            For i As Integer = 1 To UBound(dri.CSLinkTrain)
                dri.CSLinkTrain(i).OverDutySort = ""
            Next
            Call RemoveCSDriverFromList(dri)
            Dim newDrivers(0) As CSDriver
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                If CSTrainsAndDrivers.CSDrivers(i) IsNot dri Then
                    ReDim Preserve newDrivers(UBound(newDrivers) + 1)
                    newDrivers(UBound(newDrivers)) = CSTrainsAndDrivers.CSDrivers(i)
                    newDrivers(UBound(newDrivers)).CSDriverID = UBound(newDrivers)
                End If
            Next
            CSTrainsAndDrivers.CSDrivers = newDrivers
            GC.Collect()
        End If
    End Sub



    Public Class AutoPara
        Public MoringDutyNum As Integer
        Public DayDutyNum As Integer
        Public NightDutyNum As Integer
        Public CDayDutyNum As Integer

        Public Sub New(ByVal MDutyNum As Integer, ByVal DDutyNum As Integer, ByVal CDDutyNum As Integer, ByVal NDutyNum As Integer)
            MoringDutyNum = MDutyNum
            DayDutyNum = DDutyNum
            CDayDutyNum = CDDutyNum
            NightDutyNum = NDutyNum
        End Sub

        Public Sub New()

        End Sub
    End Class

    Public CSAutoPlanPara As AutoPara

End Module
