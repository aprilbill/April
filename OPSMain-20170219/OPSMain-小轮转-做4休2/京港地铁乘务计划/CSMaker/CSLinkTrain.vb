Imports System.ComponentModel

<Serializable()> _
Public Class CSLinkTrain
    Public CSTrainID As Integer '在乘务计划编制中的ID
    'Dim TrainNum As String '对应的运行图编制中的TrainID
    Public OutputCheCi As String '对应的运行图编制中的车次
    Public OffCheCi As String = ""    '下车车次
    Public StartStaName As String '起始站名
    Public EndStaName As String '结束站名

    Public StartStaID As Integer '起始站编号
    Public EndStaID As Integer '结束站编号

    Public StartTime As Integer '起始时间 不加86400
    Public EndTime As Integer '结束时间 不加86400

    Public CulStartTime As Integer '起始时间
    Public CulEndTime As Integer '结束时间

    Public nCheDiID As Integer '车底括号中的id，顺序
    Public sCheDiHao As String
    Public nTrainID As Integer '内部车次,顺序
    Public nPathID1 As Integer
    Public nPathID2 As Integer
    Public UpOrDown As Integer '0上1下
    Public OffUpOrDown As Integer = 0    '下车时的上下行
    Public UpOrDownNum As Integer '记录该次列车是上行或下行的第几趟列车

    Public ZFTime As Integer '折返时间
    Public STASEQ1 As String '起始站下行站序
    Public STASEQ2 As String '终点站下行站序

    Public RoutingName As String '交路名称
    Public RoutingCha As String '交路性质

    Public OverDutySort As String = "" '安排班种
    Public DutySort As String = "" 'CSMain有用

    Public IsDeadHeading As Boolean = False    '是否为随乘车

    'Public RunTime As Integer '运行时间
    <Category("列车信息"), DisplayNameAttribute("运行时间"), Description("运行时间")> _
    Public ReadOnly Property RunTime() As Integer
        Get
            Return Me.CulEndTime - Me.CulStartTime
        End Get
    End Property
    Public distance As Double  '运行距离
    Public IsLinked As Boolean = False
    Public FirstStation As typeCrewStation
    Public SecondStation As typeCrewStation
    Public ForDutySort As String ''必须由哪班司机完成，出入库车
    Public isBeiChe As Integer = 0
    ''' <summary>
    ''' 是否需要备车
    ''' </summary>
    ''' <remarks></remarks>
    Public NeedPrepareTrain As Boolean

    Public Sub New(ByVal FirCrewSta As typeCrewStation, ByVal SecCrewSta As typeCrewStation, ByVal tempDutySort As String, ByVal CSCheDi As typeCSCheDi)
        Me.FirstStation = FirCrewSta
        Me.SecondStation = SecCrewSta

        Me.sCheDiHao = CSCheDi.sCheDiHao

        Dim i, kJiaoLu, FindPoint As Integer

        Me.StartStaName = FirCrewSta.CrewStaName
        Me.StartTime = FirCrewSta.DepartTime     'FirCrewSta.ArriveTime
        Me.nTrainID = FirCrewSta.CSTrainID
        Me.EndStaName = SecCrewSta.CrewStaName
        Me.EndTime = SecCrewSta.ArriveTime
        Me.OutputCheCi = FirCrewSta.CheCi
        Me.OffCheCi = SecCrewSta.CheCi

        Me.nCheDiID = CSCheDi.CSCheDiId '' FirCrewSta.CSCheDiID
        Me.nPathID1 = FirCrewSta.PathID
        Me.nPathID2 = SecCrewSta.PathID
        Me.STASEQ1 = FirCrewSta.nStationSeq
        Me.STASEQ2 = SecCrewSta.nStationSeq

        Me.isBeiChe = FirCrewSta.IsBeiche

        Me.StartStaID = CSFromStaNameToStaIDByStationinf(Me.StartStaName, Me.STASEQ1)
        Me.EndStaID = CSFromStaNameToStaIDByStationinf(Me.EndStaName, Me.STASEQ2)
        Me.ForDutySort = Me.FirstStation.ForDutySort


        If SecCrewSta.JiaoLuName Is Nothing = False Then
            Me.RoutingName = FirCrewSta.JiaoLuName
        Else
            If FirCrewSta.JiaoLuName Is Nothing = False Then
                Me.RoutingName = SecCrewSta.JiaoLuName
            End If
        End If

        If SecCrewSta.JiaoLuName <> FirCrewSta.JiaoLuName Then
            Me.RoutingName = SecCrewSta.JiaoLuName
        End If
        ' 标识交路性质
        For kJiaoLu = 1 To UBound(Jiaolu)
            If Me.RoutingName = Jiaolu(kJiaoLu).JiaoluName Then
                Me.RoutingCha = Jiaolu(kJiaoLu).CrewType
                Exit For
            End If
        Next

        Me.UpOrDown = FirCrewSta.FlagUpDown
        Me.OffUpOrDown = SecCrewSta.FlagUpDown

        ''出库车的判断
        If CSCheDi.CrewSta(1) Is FirCrewSta Then
            Me.NeedPrepareTrain = True
        End If

        If SecCrewSta.IsLast = True Then
            For i = 1 To UBound(CSCheDi.CrewSta)
                If CSCheDi.CrewSta(i) Is SecCrewSta Then
                    FindPoint = i
                    Exit For
                End If
            Next
            If FindPoint + 1 <= UBound(CSCheDi.CrewSta) Then
                Me.ZFTime = CSCheDi.CrewSta(FindPoint + 1).ArriveTime - SecCrewSta.ArriveTime
            End If
        End If

        If Me Is Nothing = False Then
            Me.CulStartTime = AddLitterTime(Me.StartTime)
            Me.CulEndTime = AddLitterTime(Me.EndTime)

            '添加距离
            If Me.nPathID1 <> 0 And Me.nPathID2 <> 0 Then
                Me.distance = 0
                Dim tempSectionDistance As SectionDistance = SectionDistanceList.Find(Function(value As SectionDistance)
                                                                                          Return value.StartName = Me.StartStaName And value.EndName = Me.EndStaName
                                                                                      End Function)
                If tempSectionDistance IsNot Nothing Then
                    Me.distance = tempSectionDistance.Distance
                Else
                    If ErrorInfoList Is Nothing Then
                        ErrorInfoList = New List(Of String)
                    End If
                    Dim index As Integer = ErrorInfoList.FindIndex(Function(value As String)
                                                                       Return value = Me.StartStaName & "-" & Me.EndStaName
                                                                   End Function)
                    If index < 0 Then
                        If Me.StartStaName <> Me.EndStaName Then
                            ErrorInfoList.Add(Me.StartStaName & "-" & Me.EndStaName)
                        End If
                    End If

                End If
            End If

        End If
    End Sub
    Public Sub New()

    End Sub
    Public Sub ReCalPara()
        Me.StartStaName = Me.FirstStation.CrewStaName
        Me.StartTime = Me.FirstStation.ArriveTime
        Me.nTrainID = Me.FirstStation.CSTrainID
        Me.EndStaName = Me.SecondStation.CrewStaName
        Me.EndTime = Me.SecondStation.ArriveTime
        Me.OutputCheCi = Me.FirstStation.CheCi
        Me.OffCheCi = Me.SecondStation.CheCi

        Me.nPathID1 = Me.FirstStation.PathID
        Me.nPathID2 = Me.SecondStation.PathID
        Me.STASEQ1 = Me.FirstStation.nStationSeq
        Me.STASEQ2 = Me.SecondStation.nStationSeq

        Me.StartStaID = CSFromStaNameToStaIDByStationinf(Me.StartStaName, Me.STASEQ1)
        Me.EndStaID = CSFromStaNameToStaIDByStationinf(Me.EndStaName, Me.STASEQ2)
        Me.ForDutySort = Me.FirstStation.ForDutySort

        If Me.SecondStation.JiaoLuName Is Nothing = False Then
            Me.RoutingName = Me.SecondStation.JiaoLuName
        Else
            If Me.FirstStation Is Nothing = False Then
                Me.RoutingName = Me.FirstStation Is Nothing
            End If
        End If
        ' 标识交路性质
        For kJiaoLu = 1 To UBound(Jiaolu)
            If Me.RoutingName = Jiaolu(kJiaoLu).JiaoluName Then
                Me.RoutingCha = Jiaolu(kJiaoLu).CrewType
                Exit For
            End If
        Next

        Me.UpOrDown = Me.FirstStation.FlagUpDown
        Me.OffUpOrDown = Me.SecondStation.FlagUpDown

        ''出库车的判断

        If Me Is Nothing = False Then
            Me.CulStartTime = AddLitterTime(Me.StartTime)
            Me.CulEndTime = AddLitterTime(Me.EndTime)

            '添加距离
            If Me.nPathID1 <> 0 And Me.nPathID2 <> 0 Then
                Me.distance = 0
                Dim tempSectionDistance As SectionDistance = SectionDistanceList.Find(Function(value As SectionDistance)
                                                                                          Return value.StartName = Me.StartStaName And value.EndName = Me.EndStaName
                                                                                      End Function)
                If tempSectionDistance IsNot Nothing Then
                    Me.distance = tempSectionDistance.Distance
                Else
                    Dim index As Integer = ErrorInfoList.FindIndex(Function(value As String)
                                                                       Return value = Me.StartStaName & "-" & Me.EndStaName
                                                                   End Function)
                    If index < 0 Then
                        ErrorInfoList.Add(Me.StartStaName & "-" & Me.EndStaName)
                    End If

                End If
            End If

        End If
    End Sub
End Class

''' <summary>
''' 由于端点站可能不是轮换点，需要将几列列车捆绑共同考虑
''' </summary>
''' <remarks></remarks>
<Serializable()> _
Public Class MergedCSLinkTrain
    Public CSLinkTrains() As CSLinkTrain

    Public CGTrainID As Integer

    Public nCheDiID As Integer '车底括号中的id，顺序
    Public StartStaName As String '起始站名
    Public EndStaName As String '结束站名

    Public StartStaID As Integer '起始站编号
    Public EndStaID As Integer '结束站编号

    Public StartTime As Integer '起始时间
    Public EndTime As Integer '结束时间

    Public CulStartTime As Integer '起始时间
    Public CulEndTime As Integer '结束时间

    Public UpOrDown As Integer '0上1下
    'Public UpOrDownNum As Integer '记录该次列车是上行或下行的第几趟列车

    Public ZFTime As Integer '折返时间
    Public STASEQ1 As String '起始站下行站序
    Public STASEQ2 As String '重点站下行站序

    Public RoutingName As String '交路名称
    Public RoutingCha As String '交路性质
    Public RunTime As Integer '运行时间
    Public _distance As Double  '运行距离

    Public beiche As Integer = 0 '备车信息，1为入备车线，2为出备车线
    Public dutywork As String = "" '特殊说明用（吃饭）
    Public ifAutoDone As Boolean = True '配合dri


    Public ReadOnly Property IsLinked As Boolean
        Get
            If UBound(Me.CSLinkTrains) > 0 Then
                Return Me.CSLinkTrains(1).IsLinked
            Else
                Return False
            End If
        End Get
    End Property
    Public ForDutySort As String ''必须由哪班司机完成，出入库车
    Public ReadOnly Property DriveTime As Integer
        Get
            Dim _drivetime As Integer = 0
            If UBound(CSLinkTrains) > 0 Then
                For i As Integer = 1 To UBound(CSLinkTrains)
                    _drivetime += CSLinkTrains(i).CulEndTime - CSLinkTrains(i).CulStartTime
                Next
            End If
        End Get
    End Property

    Public ReadOnly Property distance As Double
        Get
            Dim tempdis As Double = 0.0
            If UBound(Me.CSLinkTrains) > 0 Then
                For j = 1 To UBound(Me.CSLinkTrains)
                    tempdis += Me.CSLinkTrains(j).distance
                Next
            End If
            Return tempdis
        End Get
    End Property

    Public Sub New()
        ReDim Me.CSLinkTrains(0)
    End Sub
    
    Public Sub ReCalWorkLoad()
        If UBound(Me.CSLinkTrains) > 0 Then
            Me.RunTime = 0
            Me._distance = 0
            Me.ZFTime = 0
            Dim j As Integer
            For j = 1 To UBound(Me.CSLinkTrains)

                Me._distance = Me.distance + Me.CSLinkTrains(j).distance
                Me.ZFTime = Me.ZFTime + Me.CSLinkTrains(j).ZFTime
                Me.RunTime = Me.RunTime + Me.CSLinkTrains(j).RunTime
            Next
        End If
    End Sub
    Public Sub AddCSLinkTrain(ByVal Train As CSLinkTrain)
        ReDim Preserve Me.CSLinkTrains(UBound(Me.CSLinkTrains) + 1)
        Me.CSLinkTrains(UBound(Me.CSLinkTrains)) = Train
        If UBound(Me.CSLinkTrains) = 1 Then
            Me.RoutingName = Train.RoutingName
            Me.RoutingCha = Train.RoutingCha

            Me.StartStaName = Train.StartStaName
            Me.StartStaID = Train.StartStaID
            Me.StartTime = Train.StartTime
            Me.CulStartTime = Train.CulStartTime
            Me.nCheDiID = Train.nCheDiID
            Me.ForDutySort = Train.ForDutySort
            Me.UpOrDown = Train.UpOrDown
        End If
        Me.EndStaName = Train.EndStaName
        Me.EndStaID = Train.EndStaID
        Me.EndTime = Train.EndTime
        Me.CulEndTime = Train.CulEndTime
        Me.ReCalWorkLoad()
    End Sub
    Public Sub RemoveCSLinkTrain()
        If UBound(Me.CSLinkTrains) > 0 Then
            ReDim Preserve Me.CSLinkTrains(UBound(Me.CSLinkTrains) - 1)
        End If
        If UBound(Me.CSLinkTrains) > 0 Then
            Me.EndStaName = Me.CSLinkTrains(UBound(Me.CSLinkTrains)).EndStaName
            Me.EndStaID = Me.CSLinkTrains(UBound(Me.CSLinkTrains)).EndStaID
            Me.EndTime = Me.CSLinkTrains(UBound(Me.CSLinkTrains)).EndTime
            Me.CulEndTime = Me.CSLinkTrains(UBound(Me.CSLinkTrains)).CulEndTime
        End If
        Me.ReCalWorkLoad()
    End Sub
End Class

<Serializable()> _
Public Class typeCrewStation
    'Public CSCheDiID As Integer
    Public CrewStaName As String
    Public ArriveTime As Integer
    Public DepartTime As Integer
    Public CheCi As String '
    'Public sCheDiHao As String
    Public FlagUpDown As Integer '0上行，1下行
    Public JiaoLuName As String '记录后行列车的交路名称
    Public nStationSeq As String '车站下行站序
    Public CSTrainID As Integer
    Public PathID As Integer
    Public IsLast As Boolean = False   '指示是否为一列车的最后一个站点
    Public IsFirst As Boolean = False  '指示是否为一列车的第一个站点
    Public IsYard As Boolean = False
    Public IsDinnnerSta As Boolean = False
    Public IsTransitSta As Boolean = False
    Public IsShiftSta As Boolean = False
    Public IsBeiche As Integer = 0
    Public ForDutySort As String ''必须由哪班司机完成，出入库车
    'Dim PathID2 As Integer
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="tempCSTrain"></param>
    ''' <param name="tempPathID">CSTrainInf(TrainId).nPathID(j),j</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tempCSTrain As typeTrainInformation, ByVal TrainID As Integer, ByVal tempPathID As Integer, ByVal tempForDuty As String)
        Me.ArriveTime = tempCSTrain.Arrival(tempCSTrain.nPathID(tempPathID))
        Me.DepartTime = tempCSTrain.Starting(tempCSTrain.nPathID(tempPathID))
        Me.CheCi = tempCSTrain.sPrintTrain
        Me.CrewStaName = StationInf(tempCSTrain.nPathID(tempPathID)).sStationName

        If TrainID Mod 2 = 0 Then
            Me.FlagUpDown = 0 '上行
        Else
            Me.FlagUpDown = 1 '下行
        End If

        If tempPathID = UBound(tempCSTrain.nPathID) Then
            Me.IsLast = True  '是
        Else
            Me.IsLast = False '否
        End If

        If tempPathID = 1 Then
            Me.IsFirst = True  '是
        Else
            Me.IsFirst = False '否
        End If

        If StationInf(tempCSTrain.nPathID(tempPathID)).sStaStyle = "车场" Then
            Me.IsYard = True  '是
        Else
            Me.IsYard = False '否
        End If
        Me.ForDutySort = tempForDuty

        Me.CSTrainID = TrainID
        Me.PathID = tempPathID

        Me.JiaoLuName = tempCSTrain.sJiaoLuName
        Me.nStationSeq = tempCSTrain.nPathID(tempPathID)


        If IsTransitStationPlace(Me.CrewStaName, Me.JiaoLuName, FlagUpDown, Me.ArriveTime) = True Then '’车场-->轮换点交路，此判断无效
            Me.IsTransitSta = True
        End If

        If IsDinnerPlace(Me.CrewStaName, Me.FlagUpDown, Me.JiaoLuName) = True Then '’用餐点
            Me.IsDinnnerSta = True
        End If
        If IsShiftPlace(Me.CrewStaName, Me.JiaoLuName, Me.ArriveTime) = True Then '’上班点
            Me.IsShiftSta = True
        End If
    End Sub
    Public Sub New(ByVal tempCSTrain As typeTrainInformation, ByVal TrainID As Integer, ByVal tempPathID As Integer)
        Me.ArriveTime = tempCSTrain.Arrival(tempCSTrain.nPathID(tempPathID))
        Me.DepartTime = tempCSTrain.Starting(tempCSTrain.nPathID(tempPathID))
        Me.CheCi = tempCSTrain.sPrintTrain
        Me.CrewStaName = StationInf(tempCSTrain.nPathID(tempPathID)).sStationName
        If TrainID Mod 2 = 0 Then
            Me.FlagUpDown = 0 '上行
        Else
            Me.FlagUpDown = 1 '下行
        End If

        If tempPathID = UBound(tempCSTrain.nPathID) Then
            Me.IsLast = True  '是
        Else
            Me.IsLast = False '否
        End If

        If tempPathID = 1 Then
            Me.IsFirst = True  '是
        Else
            Me.IsFirst = False '否
        End If

        If StationInf(tempCSTrain.nPathID(tempPathID)).sStaStyle = "车场" Then
            Me.IsYard = True  '是
        Else
            Me.IsYard = False '否
        End If
        'Me.CSCheDiID = UBound(CSChedi) ''要不要？？？？？？
        Me.CSTrainID = TrainID
        Me.PathID = tempPathID
        'Me.CrewStaStartTime = tempCSTrain.Starting(tempCSTrain.nPathID(tempPathID))
        Me.JiaoLuName = tempCSTrain.sJiaoLuName
        Me.nStationSeq = tempCSTrain.nPathID(tempPathID)


        If IsTransitStationPlace(Me.CrewStaName, Me.JiaoLuName, FlagUpDown, Me.ArriveTime) = True Then '’车场-->轮换点交路，此判断无效
            Me.IsTransitSta = True
        End If

        If IsDinnerPlace(Me.CrewStaName, Me.FlagUpDown, Me.JiaoLuName) = True Then '’用餐点
            Me.IsDinnnerSta = True
        End If
        If IsShiftPlace(Me.CrewStaName, Me.JiaoLuName, Me.ArriveTime) = True Then '’上班点
            Me.IsShiftSta = True
        End If

    End Sub
    Public Sub New()

    End Sub
    Public Sub New(ByVal StationName As String, ByVal CheHao As String, ByVal ifEarly As Boolean)
        Dim j, n, m
        For j = 1 To UBound(CSchediInfo)
            For n = 1 To UBound(CSchediInfo(j).nLinkTrain)
                If CSTrainInf(CSchediInfo(j).nLinkTrain(n)).sPrintTrain = CheHao Then
                    For m = 1 To UBound(CSTrainInf(CSchediInfo(j).nLinkTrain(n)).nPathID)
                        If StationName = StationInf(CSTrainInf(CSchediInfo(j).nLinkTrain(n)).nPathID(m)).sStationName And ((ifEarly = True And AddLitterTime(CSTrainInf(CSchediInfo(j).nLinkTrain(n)).Starting(CSTrainInf(CSchediInfo(j).nLinkTrain(n)).nPathID(m))) < 9 * 3600) Or (ifEarly = False And AddLitterTime(CSTrainInf(CSchediInfo(j).nLinkTrain(n)).Starting(CSTrainInf(CSchediInfo(j).nLinkTrain(n)).nPathID(m))) > 22 * 3600)) Then
                            Me.ArriveTime = CSTrainInf(CSchediInfo(j).nLinkTrain(n)).Arrival(CSTrainInf(CSchediInfo(j).nLinkTrain(n)).nPathID(m))
                            Me.DepartTime = CSTrainInf(CSchediInfo(j).nLinkTrain(n)).Starting(CSTrainInf(CSchediInfo(j).nLinkTrain(n)).nPathID(m))
                            Me.CheCi = CheHao
                            Me.CrewStaName = StationName
                            If CSchediInfo(j).nLinkTrain(n) Mod 2 = 0 Then
                                Me.FlagUpDown = 0 '上行
                            Else
                                Me.FlagUpDown = 1 '下行
                            End If

                            If m = UBound(CSTrainInf(CSchediInfo(j).nLinkTrain(n)).nPathID) Then
                                Me.IsLast = True  '是
                            Else
                                Me.IsLast = False '否
                            End If

                            If m = 1 Then
                                Me.IsFirst = True  '是
                            Else
                                Me.IsFirst = False '否
                            End If

                            If StationInf(CSTrainInf(CSchediInfo(j).nLinkTrain(n)).nPathID(m)).sStaStyle = "车场" Then
                                Me.IsYard = True  '是
                            Else
                                Me.IsYard = False '否
                            End If

                            Me.CSTrainID = CSchediInfo(j).nLinkTrain(n)
                            Me.PathID = m
                            'Me.CrewStaStartTime = tempCSTrain.Starting(tempCSTrain.nPathID(tempPathID))
                            Me.JiaoLuName = CSTrainInf(CSchediInfo(j).nLinkTrain(n)).sJiaoLuName
                            Me.nStationSeq = CSTrainInf(CSchediInfo(j).nLinkTrain(n)).nPathID(m)


                            If IsTransitStationPlace(Me.CrewStaName, Me.JiaoLuName, FlagUpDown, Me.ArriveTime) = True Then '’车场-->轮换点交路，此判断无效
                                Me.IsTransitSta = True
                            End If

                           If IsDinnerPlace(Me.CrewStaName, Me.FlagUpDown, Me.JiaoLuName) = True Then '’用餐点
                                Me.IsDinnnerSta = True
                            End If
                            If IsShiftPlace(Me.CrewStaName, Me.JiaoLuName, Me.ArriveTime) = True Then '’上班点
                                Me.IsShiftSta = True
                            End If
                            Exit Sub
                        End If
                    Next
                End If

            Next
        Next

    End Sub
    Public Function isEqual(ByVal tmpSta As typeCrewStation) As Boolean
        If Me.ArriveTime = tmpSta.ArriveTime And Me.DepartTime = tmpSta.DepartTime And Me.CrewStaName = tmpSta.CrewStaName And Me.CheCi = tmpSta.CheCi Then
            Return True
        End If
        Return False
    End Function
End Class

<Serializable()> _
Public Class typeCSCheDi
    Public CSCheDiId As Integer
    Public sCheDiHao As String
    'Dim JiaoLuName As String '交路名称
    Public CrewType As Integer '单独0，混合1
    Public CrewSta() As typeCrewStation '轮换点，从1开始,包括用餐点
    Public IsBeiChe As Integer '是否为备车 0否1是

    Public DinnerStaName As String
    Public DinnerStaTime As Integer
    Public StartStaName As String '车底开始的站名
    Public StartTime As Integer '车底开始运行的时间
    Public EndStaName As String '车底结束的站名
    Public EndTime As Integer '车底结束运行的时间

    Public CSLinkTrains As List(Of CSLinkTrain)

    Public ForDutySort As String ''必须由哪班司机完成，出入库车,早高峰出车

    Public Sub New(ByVal _CSCheDiId As Integer, ByVal _sCheDiHao As String)
        Me.CSCheDiId = _CSCheDiId
        Me.sCheDiHao = _sCheDiHao
        Me.CSLinkTrains = New List(Of CSLinkTrain)
        ReDim Me.CrewSta(0)
    End Sub
    Public Sub New()
        Me.CSLinkTrains = New List(Of CSLinkTrain)
        ReDim Me.CrewSta(0)
    End Sub
    Public Sub AddCrewSta(ByVal _CrewSta As typeCrewStation)
        ReDim Preserve Me.CrewSta(UBound(Me.CrewSta) + 1)
        Me.CrewSta(UBound(Me.CrewSta)) = _CrewSta
    End Sub
End Class

Public Class SectionDistance
    Public _StartName As String
    Public _Distance As Decimal
    Public _EndName As String '

    Public Property StartName
        Get
            Return _StartName
        End Get

        Set(ByVal value)
            _StartName = value
        End Set
    End Property
    Public Property EndName
        Get
            Return _EndName
        End Get

        Set(ByVal value)
            _EndName = value
        End Set
    End Property
    Public Property Distance
        Get
            Return _Distance
        End Get

        Set(ByVal value)
            _Distance = value
        End Set
    End Property

    Public Sub New()

    End Sub
    Public Sub New(ByVal _tempStartName As String, ByVal _tempEndName As String, ByVal _tempDistance As Decimal)
        _StartName = _tempStartName
        _EndName = _tempEndName
        _Distance = _tempDistance
    End Sub
End Class


