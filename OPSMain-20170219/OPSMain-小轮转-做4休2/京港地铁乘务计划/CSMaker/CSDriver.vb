Imports System.ComponentModel

<Serializable()> _
Public Class CSDriver
    Public CSDriverID As Integer 'ID

    Private _CSDriverno As String
    <Category("工作信息"), DisplayNameAttribute("编号"), Description("任务编号")> _
    Public Property CSdriverNo As String '司机编号内部
        Get
            Return _CSDriverno
        End Get
        Set(ByVal value As String)
            _CSDriverno = value
        End Set
    End Property
    Private _OutPutCSDriverno As String = ""
    <Category("工作信息"), DisplayNameAttribute("编号"), Description("输出编号")> _
    Public Property OutPutCSdriverNo As String '司机编号内部
        Get
            If _OutPutCSDriverno.Trim = "" Then
                _OutPutCSDriverno = CSdriverNo
            End If
            Return _OutPutCSDriverno
        End Get
        Set(ByVal value As String)
            _OutPutCSDriverno = value
        End Set
    End Property

    Public CSdriverName As String
    Public CSLinkTrain() As CSLinkTrain

    Public ListSuichengCSLinkTrain As List(Of CSLinkTrain)
    Public ListSuichengMergedCSLinkTrain As List(Of MergedCSLinkTrain)
    Public CSLinkTrainID() As Integer
    Private _ModifiedCSLinkTrain() As CSLinkTrain

    Public dutyWork As String = "" 'april临时变量，判断他正在做啥
    Public ifAutoDone As Boolean = True 'april是否是自动安排
    Public LinkedMorDriver As CSDriver        '衔接的早班
    Public LinkedNightDriver As CSDriver      '衔接的夜班
    Public IsPrepareDri As Boolean = False     '是否是预备任务，包括备班和备车
    Private _ShowColor As Color = Color.Red

    Public ReadOnly Property ShowColor As Color
        Get
            If Me.DefaultColor <> Nothing Then
                _ShowColor = Me.DefaultColor
                Return _ShowColor
            Else
                Select Case Me.State
                    Case "班中"
                        _ShowColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyOnLineColor)
                    Case "小休"
                        _ShowColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyRestLineColor)
                    Case "用餐"
                        _ShowColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyDinnerLineColor)
                    Case "班后"
                        _ShowColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyOffLineColor)
                    Case Else
                        _ShowColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyOnLineColor)
                End Select
                Return _ShowColor
            End If
        End Get
    End Property

    Public DefaultColor As Color = Nothing

    ''' <summary>
    ''' 判断任务是否合理
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsReasonable As Boolean
        Get
            Dim isrea As Boolean = True
            Select Case DutySort
                Case "早班"
                    If Me.DriveDistance < CS_MorningMinLength Then
                        isrea = False
                    End If
                Case "白班"
                    If Me.DriveDistance < CS_DayMinLength Then
                        isrea = False
                    End If
                    If Me.CSLinkTrain(UBound(Me.CSLinkTrain)).CulEndTime >= 20 * 3600 Then
                        isrea = False
                    End If
                Case "日勤班"
                    If Me.DriveDistance < CS_CDayMinLength Then
                        isrea = False
                    End If
                    If Me.CSLinkTrain(UBound(Me.CSLinkTrain)).CulEndTime >= 20 * 3600 Then
                        isrea = False
                    End If
                Case "夜班"
                    If Me.DriveDistance < CS_NightMinLength Then
                        isrea = False
                    End If
                Case Else
                    Return False
            End Select
            
            Return isrea
        End Get
    End Property

    ''' <summary>
    ''' 修正后的任务数量
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Category("任务信息"), DisplayNameAttribute("任务数量"), Description("任务数量")> _
    Public ReadOnly Property ModifiedDutyNumber() As Integer
        Get
            Return UBound(ModifiedCSLinkTrain)
        End Get
    End Property
    <Category("任务信息"), DisplayNameAttribute("最紧密接续时间"), Description("最紧密接续时间")> _
    Public ReadOnly Property MinInterval() As String
        Get
            Dim MinTime, trainStartTime, trainEndTime As Integer
            MinTime = 0 : trainStartTime = 0 : trainEndTime = 0
            Dim flagJieXu As Boolean = False
            Dim NextStartTime, PriEndTime As Integer
            NextStartTime = 0
            PriEndTime = 0

            For k = 1 To UBound(Me.ModifiedCSLinkTrain) - 1
                If Me.ModifiedCSLinkTrain(k).sCheDiHao <> Me.ModifiedCSLinkTrain(k + 1).sCheDiHao And Me.ModifiedCSLinkTrain(k + 1).OutputCheCi <> "用餐" Then
                    flagJieXu = True
                    NextStartTime = Me.ModifiedCSLinkTrain(k + 1).StartTime
                    PriEndTime = Me.ModifiedCSLinkTrain(k).EndTime
                    If NextStartTime > PriEndTime Then
                        MinTime = NextStartTime - PriEndTime
                    Else
                        MinTime = NextStartTime + 86400 - PriEndTime
                    End If
                    Exit For
                End If
            Next k

            If flagJieXu = False Then
                MinTime = -1
            Else

                For k = 1 To UBound(Me.ModifiedCSLinkTrain) - 1
                    If Me.ModifiedCSLinkTrain(k).sCheDiHao <> Me.ModifiedCSLinkTrain(k + 1).sCheDiHao And Me.ModifiedCSLinkTrain(k + 1).OutputCheCi <> "用餐" Then
                        Dim tempMinTime As Integer
                        NextStartTime = Me.ModifiedCSLinkTrain(k + 1).StartTime
                        PriEndTime = Me.ModifiedCSLinkTrain(k).EndTime
                        If NextStartTime > PriEndTime Then
                            tempMinTime = NextStartTime - PriEndTime
                        Else
                            tempMinTime = NextStartTime + 86400 - PriEndTime
                        End If
                        If tempMinTime < MinTime Then
                            MinTime = tempMinTime
                        End If
                    End If
                Next k
            End If
            Return BeTime(MinTime)
        End Get
    End Property
    <Category("任务信息"), DisplayNameAttribute("最紧密接续地点"), Description("最紧密接续地点")> _
    Public ReadOnly Property MinIntervalStation() As String
        Get
            Dim MinSpot As String = ""
            If Me.MinInterval = "-1" Then
                MinSpot = "无"
            Else

                Dim MinTime As Integer = 0
                Dim flagJieXu As Boolean = False
                Dim NextStartTime, PriEndTime As Integer
                NextStartTime = 0
                PriEndTime = 0

                For k = 1 To UBound(Me.ModifiedCSLinkTrain) - 1
                    If Me.ModifiedCSLinkTrain(k).sCheDiHao <> Me.ModifiedCSLinkTrain(k + 1).sCheDiHao And Me.ModifiedCSLinkTrain(k + 1).OutputCheCi <> "用餐" Then
                        NextStartTime = Me.ModifiedCSLinkTrain(k + 1).StartTime
                        PriEndTime = Me.ModifiedCSLinkTrain(k).EndTime
                        If NextStartTime > PriEndTime Then
                            MinTime = NextStartTime - PriEndTime
                        Else
                            MinTime = NextStartTime + 86400 - PriEndTime
                        End If
                        If BeTime(MinTime) = Me.MinInterval Then
                            MinSpot = Me.ModifiedCSLinkTrain(k).EndStaName
                            Exit For
                        End If
                    End If
                Next k
            End If
            Return MinSpot
        End Get
    End Property
    <Category("任务信息"), DisplayNameAttribute("最宽松接续时间"), Description("最宽松接续时间")> _
    Public ReadOnly Property MaxInterval() As String
        Get
            Dim MaxTime, trainStartTime, trainEndTime As Integer
            MaxTime = 0 : trainStartTime = 0 : trainEndTime = 0

            Dim flagJieXu As Boolean = False
            Dim NextStartTime, PriEndTime As Integer
            NextStartTime = 0
            PriEndTime = 0

            For k = 1 To UBound(Me.ModifiedCSLinkTrain) - 1
                If Me.ModifiedCSLinkTrain(k).sCheDiHao <> Me.ModifiedCSLinkTrain(k + 1).sCheDiHao And Me.ModifiedCSLinkTrain(k + 1).OutputCheCi <> "用餐" Then
                    flagJieXu = True
                    NextStartTime = Me.ModifiedCSLinkTrain(k + 1).StartTime
                    PriEndTime = Me.ModifiedCSLinkTrain(k).EndTime
                    If NextStartTime > PriEndTime Then
                        MaxTime = NextStartTime - PriEndTime
                    Else
                        MaxTime = NextStartTime + 86400 - PriEndTime
                    End If
                    Exit For
                End If
            Next k

            If flagJieXu = False Then
                MaxTime = -1
            Else

                For k = 1 To UBound(Me.ModifiedCSLinkTrain) - 1
                    If Me.ModifiedCSLinkTrain(k).sCheDiHao <> Me.ModifiedCSLinkTrain(k + 1).sCheDiHao And Me.ModifiedCSLinkTrain(k + 1).OutputCheCi <> "用餐" Then
                        Dim tempMaxTime As Integer
                        NextStartTime = Me.ModifiedCSLinkTrain(k + 1).StartTime
                        PriEndTime = Me.ModifiedCSLinkTrain(k).EndTime
                        If NextStartTime > PriEndTime Then
                            tempMaxTime = NextStartTime - PriEndTime
                        Else
                            tempMaxTime = NextStartTime + 86400 - PriEndTime
                        End If
                        If tempMaxTime > MaxTime Then
                            MaxTime = tempMaxTime
                        End If

                    End If
                Next k
            End If
            Return BeTime(MaxTime)
        End Get
    End Property
    <Category("任务信息"), DisplayNameAttribute("最宽松接续地点"), Description("最宽松接续地点")> _
    Public ReadOnly Property MaxIntervalStation() As String
        Get
            Dim MaxSpot As String = ""
            If Me.MaxInterval = "-1" Then
                MaxSpot = "无"
            Else

                Dim MaxTime, trainStartTime, trainEndTime As Integer

                MaxTime = 0 : trainStartTime = 0 : trainEndTime = 0
                Dim flagJieXu As Boolean = False
                Dim NextStartTime, PriEndTime As Integer
                NextStartTime = 0
                PriEndTime = 0

                For k = 1 To UBound(Me.ModifiedCSLinkTrain) - 1
                    If Me.ModifiedCSLinkTrain(k).sCheDiHao <> Me.ModifiedCSLinkTrain(k + 1).sCheDiHao And Me.ModifiedCSLinkTrain(k + 1).OutputCheCi <> "用餐" Then
                        NextStartTime = Me.ModifiedCSLinkTrain(k + 1).StartTime
                        PriEndTime = Me.ModifiedCSLinkTrain(k).EndTime
                        If NextStartTime > PriEndTime Then
                            MaxTime = NextStartTime - PriEndTime
                        Else
                            MaxTime = NextStartTime + 86400 - PriEndTime
                        End If
                        If BeTime(MaxTime) = Me.MaxInterval Then
                            MaxSpot = Me.ModifiedCSLinkTrain(k).EndStaName
                            Exit For
                        End If

                    End If
                Next k

            End If


            Return MaxSpot
        End Get

    End Property
    Public ReadOnly Property ModifiedCSLinkTrain() As CSLinkTrain()
        Get
            ReDim _ModifiedCSLinkTrain(0)
            Dim i As Integer
            For i = 1 To UBound(Me.CSLinkTrain)
                If Me.CSLinkTrain(i).IsDeadHeading = False Then
                    If UBound(_ModifiedCSLinkTrain) = 0 Then
                        ReDim Preserve _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain) + 1)
                        _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)) = Me.DeepCloneCSLinkTrain(Me.CSLinkTrain(i))
                    Else
                        If _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)).nCheDiID = Me.CSLinkTrain(i).nCheDiID AndAlso _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)).IsDeadHeading = False AndAlso _
                            _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)).OutputCheCi = Me.CSLinkTrain(i).OutputCheCi Then
                            _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)).EndStaName = Me.CSLinkTrain(i).EndStaName '结束站名
                            _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)).EndStaID = Me.CSLinkTrain(i).EndStaID '结束站编号
                            _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)).EndTime = Me.CSLinkTrain(i).EndTime '结束时间
                            _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)).CulEndTime = Me.CSLinkTrain(i).CulEndTime  '结束时间
                            _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)).sCheDiHao = Me.CSLinkTrain(i).sCheDiHao
                            _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)).OffCheCi = Me.CSLinkTrain(i).OffCheCi
                            _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)).nPathID2 = Me.CSLinkTrain(i).nPathID2
                            _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)).STASEQ2 = Me.CSLinkTrain(i).STASEQ2 '重点站下行站序
                            _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)).distance = _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)).distance + Me.CSLinkTrain(i).distance   '运行距离
                            _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)).SecondStation = Me.CSLinkTrain(i).SecondStation
                            _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)).OffUpOrDown = Me.CSLinkTrain(i).OffUpOrDown
                        Else
                            ReDim Preserve _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain) + 1)
                            _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)) = Me.DeepCloneCSLinkTrain(Me.CSLinkTrain(i))
                        End If
                    End If
                Else
                    ReDim Preserve _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain) + 1)
                    _ModifiedCSLinkTrain(UBound(_ModifiedCSLinkTrain)) = Me.DeepCloneCSLinkTrain(Me.CSLinkTrain(i))
                End If
            Next
            Return _ModifiedCSLinkTrain
        End Get
    End Property

    Private Function DeepCloneCSLinkTrain(ByVal tempCSLinkTrain As CSLinkTrain) As CSLinkTrain
        Dim temp As New CSLinkTrain
        temp.CSTrainID = tempCSLinkTrain.CSTrainID '在乘务计划编制中的ID
        'Dim TrainNum As String '对应的运行图编制中的TrainID
        temp.OutputCheCi = tempCSLinkTrain.OutputCheCi '对应的运行图编制中的TrainID
        temp.OffCheCi = tempCSLinkTrain.OffCheCi
        temp.StartStaName = tempCSLinkTrain.StartStaName '起始站名
        temp.EndStaName = tempCSLinkTrain.EndStaName '结束站名

        temp.StartStaID = tempCSLinkTrain.StartStaID '起始站编号
        temp.EndStaID = tempCSLinkTrain.EndStaID '结束站编号

        temp.StartTime = tempCSLinkTrain.StartTime '起始时间
        temp.EndTime = tempCSLinkTrain.EndTime '结束时间

        temp.CulStartTime = tempCSLinkTrain.CulStartTime '起始时间
        temp.CulEndTime = tempCSLinkTrain.CulEndTime  '结束时间

        temp.nCheDiID = tempCSLinkTrain.nCheDiID '车底括号中的id，顺序
        temp.sCheDiHao = tempCSLinkTrain.sCheDiHao
        temp.nTrainID = tempCSLinkTrain.nTrainID  '内部车次,顺序
        temp.nPathID1 = tempCSLinkTrain.nPathID1
        temp.nPathID2 = tempCSLinkTrain.nPathID2
        temp.UpOrDown = tempCSLinkTrain.UpOrDown '0上1下
        temp.OffUpOrDown = tempCSLinkTrain.OffUpOrDown
        temp.UpOrDownNum = tempCSLinkTrain.UpOrDownNum '记录该次列车是上行或下行的第几趟列车

        temp.ZFTime = tempCSLinkTrain.ZFTime '折返时间
        temp.STASEQ1 = tempCSLinkTrain.STASEQ1 '起始站下行站序
        temp.STASEQ2 = tempCSLinkTrain.STASEQ2 '重点站下行站序

        temp.RoutingName = tempCSLinkTrain.RoutingName '交路名称
        temp.RoutingCha = tempCSLinkTrain.RoutingCha '交路性质
        'temp.RunTime = tempCSLinkTrain.RunTime '运行时间
        temp.distance = tempCSLinkTrain.distance   '运行距离
        temp.IsLinked = tempCSLinkTrain.IsLinked
        temp.FirstStation = tempCSLinkTrain.FirstStation
        temp.SecondStation = tempCSLinkTrain.SecondStation
        temp.NeedPrepareTrain = tempCSLinkTrain.NeedPrepareTrain
        temp.IsDeadHeading = tempCSLinkTrain.IsDeadHeading
        Return temp
    End Function
    Private _DutySort As String '早班、白班、夜班
    <Category("工作信息"), DisplayNameAttribute("班种"), Description("班种")> _
    Public Property DutySort As String
        Get
            Return _DutySort
        End Get
        Set(ByVal value As String)
            _DutySort = value
        End Set
    End Property

    Public PrintCheDiLinkColor As Color   '车底联接线的颜色
    Public PrintCheDiLinkWidth As Integer  '车底联接线的线宽
    Public PrintCheDiLinkStyle As String '车底联接线的型状
    Public bIfGouWang As Boolean  '是否已经勾完，0表示没有，1表示勾完
    Public sDiverNo As String '司机号
    Public TargetDutyTime As Integer '目标的工作数量（预留）
    Private _FirstDutyTime As Integer '第一个任务开始的时间
    <Browsable(False)> _
    Public ReadOnly Property FirstDutyTime() As Integer
        Get
            Me._FirstDutyTime = 0
            If UBound(Me.CSLinkTrain) > 0 Then
                For i As Integer = 1 To UBound(Me.CSLinkTrain)
                    If Me.CSLinkTrain(i).IsDeadHeading = False Then
                        Me._FirstDutyTime = Me.CSLinkTrain(i).CulStartTime
                        Exit For
                    End If
                Next
            End If
            Return Me._FirstDutyTime
        End Get
    End Property
    Private _BeginWorkTime As Integer '上班时间
    <Browsable(False)> _
    Public ReadOnly Property BeginWorkTime() As Integer
        Get
            If IsPrepareDri = False Then
                If UBound(Me.ModifiedCSLinkTrain) > 0 Then
                    Me._BeginWorkTime = Me.ModifiedCSLinkTrain(1).CulStartTime - Me.PreDutyTime - Me.PreTrainTime
                End If
                Return Me._BeginWorkTime
            Else
                Return Me.CSLinkTrain(1).StartTime
            End If
        End Get
    End Property
    <Category("工作信息"), DisplayNameAttribute("开始工作时间"), Description("开始工作时间")> _
    Public ReadOnly Property BeginWorkTimeString() As String
        Get
            Return BeTime(Me._BeginWorkTime)
        End Get
    End Property
    Private _EndEorkTime As Integer '下班时间
    <Browsable(False)> _
    Public ReadOnly Property EndEorkTime() As Integer
        Get
            If IsPrepareDri = False Then
                If UBound(Me.CSLinkTrain) > 0 Then
                    If Me.FlagDinner = True AndAlso Me.DinnerEndTime > Me.CSLinkTrain(UBound(Me.CSLinkTrain)).CulEndTime Then
                        Me._EndEorkTime = Me.DinnerEndTime
                    Else
                        Me._EndEorkTime = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).CulEndTime
                        Me._EndEorkTime = Me._EndEorkTime + Me.PreDutyOffTime
                    End If
                End If
                Return Me._EndEorkTime
            Else
                Return Me.CSLinkTrain(1).EndTime
            End If
        End Get
    End Property
    <Category("工作信息"), DisplayNameAttribute("结束工作时间"), Description("结束工作时间")> _
    Public ReadOnly Property EndEorkTimeString() As String
        Get
            Return BeTime(Me._EndEorkTime)
        End Get
    End Property
    <Browsable(False)> _
    Public ReadOnly Property LastTrainEndTime() As Integer
        Get
            If UBound(Me.CSLinkTrain) > 0 Then
                Return Me.CSLinkTrain(UBound(Me.CSLinkTrain)).CulEndTime
            Else
                Return -1
            End If
        End Get
    End Property

    <Browsable(False)> _
    Public ReadOnly Property DriverCost As Decimal            '任务的费用
        Get
            Return DriveDistance + 500 + IIf(Me.IsReasonable, 0, 1) * 1000
        End Get
    End Property

    Public CurCheDiID As Integer
    Public CurDriveTime As Integer '已经驾驶的时间
    Public CurStationName As String  '当前所在车站
    Public CurStationID As Integer  '当前所在车站编号
    Public CurStationTime As Integer '记录司机所在站的时间
    Public CurDirection As Integer '当前上下行方向 0上行，1下行
    Public Isdinnering As Boolean '是否正在吃饭
    Public IsHaofeiDinnerNum As Boolean = True '是否占用同时吃饭的人数名额
    Private _belongArea As String = ""

    <Category("工作信息"), DisplayNameAttribute("所属区域"), Description("所属区域")> _
    Public Property BelongArea As String
        Get
            If _belongArea = "" Then
                Dim tempArea As String = ""
                For Each area As AreaInfo In CSTrainsAndDrivers.AreaInfoS
                    Dim Index As Integer = Array.FindIndex(area.ForDutySorts, Function(value As String)
                                                                                  Return value = DutySort
                                                                              End Function)
                    If Index <> -1 Then
                        Select Case DutySort
                            Case "早班"
                                If UBound(Me.CSLinkTrain) > 0 AndAlso area.IsAreaPlace(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName) Then
                                    tempArea = area.AreaName
                                    Exit For
                                End If
                            Case "白班"
                                If UBound(Me.CSLinkTrain) > 0 AndAlso area.IsAreaPlace(Me.CSLinkTrain(1).StartStaName) Then
                                    tempArea = area.AreaName
                                    Exit For
                                End If
                            Case "日勤班"
                                If UBound(Me.CSLinkTrain) > 0 AndAlso (area.IsAreaPlace(Me.CSLinkTrain(1).StartStaName) OrElse area.IsAreaPlace(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName)) Then
                                    tempArea = area.AreaName
                                    Exit For
                                End If
                            Case "夜班"
                                If UBound(Me.CSLinkTrain) > 0 AndAlso area.IsAreaPlace(Me.CSLinkTrain(1).StartStaName) Then
                                    tempArea = area.AreaName
                                    Exit For
                                End If
                        End Select
                    End If
                Next
                If tempArea = "" Then
                    tempArea = "主区域"
                End If
                Return tempArea
            Else
                Return _belongArea
            End If
        End Get
        Set(ByVal value As String)
            If value <> _belongArea Then
                _belongArea = value
            End If
        End Set
    End Property

    Private _TotalDriveTime As Integer '记录司机的一天工作时间，包括驾驶时间和折返时间
    <Browsable(False)> _
    Public Property TotalDriveTime() As Integer
        Get
            If UBound(Me.CSLinkTrain) > 0 Then
                Me._TotalDriveTime = 0
                For j = 1 To UBound(Me.CSLinkTrain)
                    If Me.CSLinkTrain(j).IsDeadHeading = False Then
                        Me._TotalDriveTime = Me._TotalDriveTime + Me.CSLinkTrain(j).RunTime + Me.CSLinkTrain(j).ZFTime
                    End If
                Next
            End If
            Return Me._TotalDriveTime

        End Get
        Set(ByVal value As Integer)
            Me._TotalDriveTime = value
        End Set
    End Property
    <Category("工作信息"), DisplayNameAttribute("总驾驶时间"), Description("总驾驶时间")> _
    Public ReadOnly Property ShowTotalDriveTime() As String
        Get
            Return BeTime(Me.TotalDriveTime)
        End Get
    End Property
    <Browsable(False)> _
    Public ReadOnly Property WorkTime() As Integer
        Get
            Dim _worktime As Integer = 0
            If IsPrepareDri = False Then
                If Me.EndEorkTime >= Me.BeginWorkTime Then
                    _worktime = Me.EndEorkTime - Me.BeginWorkTime - (Me.DinnerEndTime - Me.DinnerStartTime) '除去吃饭时间
                Else
                    _worktime = Me.EndEorkTime - Me.BeginWorkTime + 86400 - (Me.DinnerEndTime - Me.DinnerStartTime)
                End If
            Else
                _worktime = Me.CSLinkTrain(1).EndTime - Me.CSLinkTrain(1).StartTime
            End If
            Return _worktime
        End Get
    End Property
    <Category("工作信息"), DisplayNameAttribute("工作时间"), Description("工作时间")> _
    Public ReadOnly Property ShowWorkTime() As String
        Get
            Return BeTime(Me.WorkTime)
        End Get

    End Property
    Private _DriveTime As Integer '记录司机的一天驾驶时间
    <Browsable(False)> _
    Public ReadOnly Property DriveTime() As Integer
        Get
            If UBound(Me.CSLinkTrain) > 0 Then
                Me._DriveTime = 0
                For j = 1 To UBound(Me.CSLinkTrain)
                    If Me.CSLinkTrain(j).IsDeadHeading = False Then
                        Me._DriveTime = Me._DriveTime + Me.CSLinkTrain(j).RunTime
                    End If
                Next
            End If
            Return Me._DriveTime
        End Get
    End Property
    Private _DriveDistance As Double  '记录司机的一天驾驶时间
    <Category("工作信息"), DisplayNameAttribute("驾驶里程"), Description("驾驶里程")> _
    Public ReadOnly Property DriveDistance() As Double
        Get
            If IsPrepareDri = False Then
                If UBound(Me.CSLinkTrain) > 0 Then
                    Me._DriveDistance = 0
                    For j = 1 To UBound(Me.CSLinkTrain)
                        If Me.CSLinkTrain(j).IsDeadHeading = False Then
                            Me._DriveDistance = Me._DriveDistance + Me.CSLinkTrain(j).distance
                        End If
                    Next
                End If
                Return Format(Me._DriveDistance, "0.00")
            Else
                Return Format(0, "0.00")
            End If
        End Get
       
    End Property
    <Browsable(False)> _
    Public ReadOnly Property ProductionEfficiency() As Double
        Get
            Dim pe As Double = 0
            If Me.WorkTime > 0 Then
                pe = Me.DriveTime / Me.WorkTime
            End If
            Return pe
        End Get
    End Property
    <Category("工作信息"), DisplayNameAttribute("驾驶时间"), Description("驾驶时间")> _
    Public ReadOnly Property ShowDriveTime() As String
        Get
            Return BeTime(Me.DriveTime)
        End Get
    End Property

    Private _ZFTime As Integer '记录司机的一天折返时间
    <Browsable(False)> _
    Public ReadOnly Property ZFTime() As Integer
        Get
            If UBound(Me.CSLinkTrain) > 0 Then
                Me._ZFTime = 0
                For j = 1 To UBound(Me.CSLinkTrain)
                    If Me.CSLinkTrain(j).IsDeadHeading = False Then
                        Me._ZFTime = Me._ZFTime + Me.CSLinkTrain(j).ZFTime
                    End If
                Next
            End If
            Return Me._ZFTime
        End Get
    End Property
    <Category("工作信息"), DisplayNameAttribute("折返时间"), Description("折返时间")> _
    Public ReadOnly Property ShowZFTime() As String
        Get
            Return BeTime(Me.ZFTime)
        End Get
    End Property
    Public AllDinnerInfo As New Dictionary(Of String, typeDinnerStation)
    Private _DinnerStartTime As Integer
    <Browsable(False)> _
    Public Property DinnerStartTime() As Integer
        Get
            Return _DinnerStartTime
        End Get
        Set(ByVal value As Integer)
            _DinnerStartTime = value
        End Set
    End Property
    <Category("用餐信息"), DisplayNameAttribute("开始时间"), Description("用餐开始时间")> _
    Public ReadOnly Property DinnerSTime() As String
        Get
            Return BeTime(_DinnerStartTime)
        End Get
        'Set(ByVal value As String)
        '    _DinnerStartTime = value
        'End Set
    End Property
    Private _DinnerStation As String
    <Category("用餐信息"), DisplayNameAttribute("用餐车站"), Description("用餐车站")> _
    Public Property DinnerStation() As String
        Get
            Return _DinnerStation
        End Get
        Set(ByVal value As String)
            _DinnerStation = value
        End Set
    End Property
    Private _DinnerEndTime As Integer
    <Browsable(False)> _
    Public Property DinnerEndTime() As Integer
        Get
            Return _DinnerEndTime
        End Get
        Set(ByVal value As Integer)
            _DinnerEndTime = value
        End Set
    End Property
    <Category("用餐信息"), DisplayNameAttribute("终止时间"), Description("用餐结束时间")> _
    Public ReadOnly Property DinnerETime() As String
        Get
            Return BeTime(_DinnerEndTime)
        End Get
    End Property

    Public FlagDinner As Boolean = False   '记录司机用餐的状态，0表示未吃，1表示吃过饭

    Public ReadOnly Property DinnerType As String
        Get
            If FlagDinner Then
                If Me.DinnerEndTime <= Me.CSLinkTrain(1).StartTime Then
                    DinnerType = "接班前用餐"
                ElseIf Me.DinnerStartTime >= Me.CSLinkTrain(UBound(Me.CSLinkTrain)).CulEndTime Then
                    DinnerType = "退勤后用餐"
                Else
                    DinnerType = "班中用餐"
                End If
            Else
                DinnerType = "未用餐"
            End If
        End Get
    End Property

    Public FlagRoutingName As String  '正向最后/逆向最先交路的名称(无用)
    Public FlagRoutingCha As Integer '正向最后/逆向最先交路的性质，0单独，1混合,2默认其它(无用)
    Public State As String '班前，班中，班后，
    ''' <summary>
    ''' 备车时间
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property PreTrainTime As Integer
        Get
            Dim tempTime As Integer = 0
            If UBound(Me.CSLinkTrain) > 0 Then
                If Me.CSLinkTrain(1).FirstStation IsNot Nothing AndAlso Me.CSLinkTrain(1).FirstStation.IsYard = True Then '随乘的和不随乘的同时起床
                    tempTime = PrepareTrainTime
                End If
            End If
            Return tempTime
        End Get
    End Property
    ''' <summary>
    ''' 出勤时间
    ''' </summary>
    ''' <remarks></remarks>
    Public PreDutyTime As Integer
    Public PreDutyOffTime As Integer

    Public Function IsDriveTheVecile(ByRef Train As CSLinkTrain) As Boolean
        IsDriveTheVecile = False
        If Me.CurCheDiID = Train.nCheDiID Then
            IsDriveTheVecile = True
        End If
    End Function
    Public Function IsDriveTheVecile(ByRef Train As MergedCSLinkTrain) As Boolean
        IsDriveTheVecile = False
        If Me.CurCheDiID = Train.nCheDiID Then
            IsDriveTheVecile = True
        End If
    End Function
    Public Function CanDriveTheTrain1(ByRef Train As MergedCSLinkTrain, Optional tempFangkuanxianzhi As Boolean = False) As Boolean '逆向找车下,找出可行解(目前用于夜班、白班后段)
        CanDriveTheTrain1 = False
        Dim FirTime As Integer = Me.CSLinkTrain(1).CulStartTime
        Dim SecTimeDepart As Integer = AddLitterTime(Train.CSLinkTrains(UBound(Train.CSLinkTrains)).SecondStation.ArriveTime)

        ''放宽限制
        If tempFangkuanxianzhi = True Then
            If FirTime >= SecTimeDepart Then
                Return True
            End If
        End If
        If Me.CSLinkTrain(1).StartStaName = Train.CSLinkTrains(UBound(Train.CSLinkTrains)).EndStaName AndAlso FirTime >= SecTimeDepart AndAlso CheckNextJiaolu(Train, True) = True Then                 '一旦司机借非同一车底，该司机下车后必须有休息时间
            CanDriveTheTrain1 = True
            If Train.CSLinkTrains(1).FirstStation.IsYard = True Then
                If Me.DutySort = "夜班" And Train.CSLinkTrains(1).CulEndTime > 12 * 3600 Then
                    CanDriveTheTrain1 = True
                End If
            End If

            Select Case Me.State
                Case "用餐" '未考虑向前吃饭
                    If Me.DinnerStartTime < SecTimeDepart Then
                        CanDriveTheTrain1 = False
                    Else
                        Exit Function
                    End If
            End Select

            If CheckIfInterval(Train, True) = False Then
                CanDriveTheTrain1 = False
            End If
            Dim restTime As Integer = ChangePlaceRestTime(Train.CSLinkTrains(UBound(Train.CSLinkTrains)).EndStaName, Train.CSLinkTrains(UBound(Train.CSLinkTrains)).RoutingName, Train.CSLinkTrains(UBound(Train.CSLinkTrains)).UpOrDown, Train.CSLinkTrains(UBound(Train.CSLinkTrains)).EndTime)
            '车底号相同及不够休息时间则不能接车，则轮换点下车休息
            If restTime <> 0 AndAlso ((CanDriveTheTrain1 = False AndAlso Me.CSLinkTrain(1).CulStartTime - Train.CSLinkTrains(UBound(Train.CSLinkTrains)).CulEndTime > 45 * 60) OrElse Train.CSLinkTrains(UBound(Train.CSLinkTrains)).nCheDiID = Me.CSLinkTrain(1).nCheDiID Or FirTime - SecTimeDepart < restTime) Then
                CanDriveTheTrain1 = False
            End If
            If restTime = 0 Then
                If Train.CSLinkTrains(UBound(Train.CSLinkTrains)).SecondStation.IsLast = False And Train.CSLinkTrains(UBound(Train.CSLinkTrains)).SecondStation.IsYard = False Then
                    If Train.CSLinkTrains(UBound(Train.CSLinkTrains)).UpOrDown <> Me.CSLinkTrain(1).UpOrDown Then
                        CanDriveTheTrain1 = False
                    End If
                Else
                    If Train.CSLinkTrains(UBound(Train.CSLinkTrains)).UpOrDown = Me.CSLinkTrain(1).UpOrDown Then
                        CanDriveTheTrain1 = False
                    End If
                End If
            End If
            If CanDriveTheTrain1 = True Then
                '考虑公里
                Select Case Me.DutySort
                    Case "白班"
                        If Me.DriveDistance + Train.distance > CS_DayMaxLength And ForceDriveLength = 1 Then
                            CanDriveTheTrain1 = False
                        End If
                        If Me.DriveDistance + Train.distance < CS_DayMinLength Then
                            CanDriveTheTrain1 = True
                        End If
                    Case "夜班"
                        If Me.DriveDistance + Train.distance > CS_NightMaxLength And ForceDriveLength = 1 Then
                            CanDriveTheTrain1 = False
                        End If
                        If Me.DriveDistance + Train.distance < CS_NightMinLength Then
                            CanDriveTheTrain1 = True
                        End If
                        If Train.CulEndTime <= AStartTime Then '当前车辆超过15点半不能接
                            CanDriveTheTrain1 = False
                        End If
                End Select
                '白班最多6个交路，夜班最多5个交路，早班最多3个交路,如果最后/最早一个交路是回库，可以延长一个
                Select Case Me.DutySort
                    Case "早班"
                        If (UBound(Me.CSLinkTrain)) >= 3 Then
                            CanDriveTheTrain1 = False
                        End If
                    Case "白班"
                        If (UBound(Me.CSLinkTrain)) >= 6 Then
                            CanDriveTheTrain1 = False
                        End If
                    Case "夜班"
                        If (UBound(Me.CSLinkTrain)) >= 5 Then
                            CanDriveTheTrain1 = False
                        End If
                End Select
                '考虑出勤点
                Dim preRestTime As Integer = ChangePlaceRestTime(Train.CSLinkTrains(1).StartStaName, Train.CSLinkTrains(1).RoutingName, Train.CSLinkTrains(1).UpOrDown, Train.CSLinkTrains(1).CulStartTime, False)
                Select Case Me.DutySort
                    Case "白班"
                        If IsDayDutyOffPlace(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName, Me.CSLinkTrain(1).StartStaName, Me.CSLinkTrain(1).RoutingName, Me.CSLinkTrain(1).StartTime) = False Then
                            CanDriveTheTrain1 = True '如果不在下班地点
                        End If
                        If IsDayDutyOffPlace(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName, Me.CSLinkTrain(1).StartStaName, Me.CSLinkTrain(1).RoutingName, Me.CSLinkTrain(1).StartTime) = False Then
                            If Train.CSLinkTrains(1).FirstStation.IsYard = False AndAlso Train.CSLinkTrains(1).FirstStation.IsShiftSta = False AndAlso Me.DriveDistance + 2 * Train.distance > CS_DayMinLength AndAlso ForceDriveLength = 1 Then
                                CanDriveTheTrain1 = False
                            End If
                        End If
                    Case "夜班"
                        If IsNightDutyon(Me.CSLinkTrain(1).StartStaName, Me.CSLinkTrain(1).RoutingName, Me.CSLinkTrain(1).StartTime) = False Then
                            CanDriveTheTrain1 = True '如果不在夜班上班地点
                        End If
                        If IsNightDutyon(Me.CSLinkTrain(1).StartStaName, Me.CSLinkTrain(1).RoutingName, Me.CSLinkTrain(1).StartTime) Then
                            If Train.CSLinkTrains(1).CulStartTime - Train.RunTime - preRestTime < AStartTime And ForceDutyTime = 1 Then
                                CanDriveTheTrain1 = False
                            End If
                            If Train.CSLinkTrains(1).FirstStation.IsYard = False AndAlso Train.CSLinkTrains(1).FirstStation.IsShiftSta = False AndAlso Me.DriveDistance + 2 * Train.distance > CS_NightMaxLength AndAlso ForceDriveLength = 1 Then
                                CanDriveTheTrain1 = False
                            End If
                        End If
                End Select
            End If

        End If
    End Function

    Public Function CanDriveTheTrain(ByRef Train As MergedCSLinkTrain, Optional tempFangkuanxianzhi As Boolean = False) As Boolean '正向找车（目前用于早班、白班、日勤班）
        CanDriveTheTrain = False
        If UBound(Me.CSLinkTrain) = 0 Then '不存在该司机
            Return False
        End If
        Dim FirTime As Integer = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).CulEndTime
        Dim SecTime As Integer = AddLitterTime(Train.CSLinkTrains(1).FirstStation.DepartTime)

        ''放宽限制
        If tempFangkuanxianzhi = True Then
            If FirTime <= SecTime Then
                Return True
            End If
        End If

        '是否是随乘过来的，若是，满足时间要求即可驾驶
        If Me.ListSuichengCSLinkTrain.Count > 0 AndAlso Me.ListSuichengCSLinkTrain(Me.ListSuichengCSLinkTrain.Count - 1) Is Me.CSLinkTrain(UBound(Me.CSLinkTrain)) Then
            If Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName = Train.CSLinkTrains(1).StartStaName AndAlso FirTime <= SecTime Then
                Return True
            End If
        End If

        If Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName = Train.CSLinkTrains(1).StartStaName AndAlso FirTime <= SecTime Then                '一旦司机接非同一车底，该司机下车后必须有休息时间
            If Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName <> Train.CSLinkTrains(1).RoutingName And CheckNextJiaolu(Train) = False Then
                Exit Function
            End If
            CanDriveTheTrain = True
            '先满足公里数
            Select Case Me.DutySort
                Case "早班"
                    If Me.DriveDistance + Train.distance > CS_MorningMaxLength And ForceDriveLength = 1 Then
                        CanDriveTheTrain = False
                    End If
                    If Me.DriveDistance + Train.distance < CS_MorningMinLength Then
                        CanDriveTheTrain = True
                    End If
                    If Train.CulStartTime > NStartTime And ForceDutyTime = 1 Then
                        CanDriveTheTrain = False
                    End If
                Case "白班"
                    If Me.DriveDistance + Train.distance > CS_DayMaxLength And ForceDriveLength = 1 Then
                        CanDriveTheTrain = False
                    End If
                    If Me.DriveDistance + Train.distance < CS_DayMinLength Then
                        CanDriveTheTrain = True
                    End If
                Case "日勤班"
                    If Me.DriveDistance + Train.distance > CS_CDayMaxLength And ForceDriveLength = 1 Then
                        CanDriveTheTrain = False
                    End If
                Case "夜班"
                    If Me.DriveDistance + Train.distance > CS_NightMaxLength And ForceDriveLength = 1 Then
                        CanDriveTheTrain = False
                    End If
                    If Me.DriveDistance + Train.distance < CS_NightMinLength Then
                        CanDriveTheTrain = True
                    End If
            End Select
        
            Select Case Me.DutySort
                Case "早班"
                    If IsDayDutyOnPlace(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).UpOrDown, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndTime) = False Then
                        CanDriveTheTrain = True '如果不在白班上班地点，必须接车
                    End If
                    If IsDayDutyOnPlace(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).UpOrDown, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndTime) Then
                        If Me.DriveDistance + 2 * Train.distance > CS_MorningMaxLength And ForceDriveLength = 1 Then '如果在下班地点，看是否要继续开
                            CanDriveTheTrain = False
                        End If
                    End If
                Case "白班"
                    If IsDayDutyOffPlace(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName, Me.CSLinkTrain(1).StartStaName, Me.CSLinkTrain(1).RoutingName, Me.CSLinkTrain(1).StartTime) = False Then
                        CanDriveTheTrain = True '如果不在下班地点，必须接车
                    End If
                    If IsDayDutyOffPlace(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName, Me.CSLinkTrain(1).StartStaName, Me.CSLinkTrain(1).RoutingName, Me.CSLinkTrain(1).StartTime) Then
                        If Train.CulStartTime > AStartTime And ForceDutyTime = 1 Then
                            CanDriveTheTrain = False
                        End If
                    End If
                Case "日勤班"
                    If Me.DriveDistance + 2 * Train.distance > CS_CDayMaxLength Then
                        CanDriveTheTrain = False
                    End If
            End Select
            '早班回库车必须早班接
            If Train.CSLinkTrains(UBound(Train.CSLinkTrains)).SecondStation.IsYard = True Then
                If Me.DutySort = "早班" And Train.CSLinkTrains(UBound(Train.CSLinkTrains)).CulEndTime < 12 * 3600 Then
                    CanDriveTheTrain = True
                End If
                If Me.DutySort = "白班" And Train.CSLinkTrains(UBound(Train.CSLinkTrains)).CulEndTime > 12 * 3600 Then
                    CanDriveTheTrain = True
                End If
            End If
            '白班最多6个交路，夜班最多5个交路，早班最多3个交路,如果最后/最早一个交路是回库，可以延长一个
            Select Me.DutySort
                Case "早班"
                    If Me.CSLinkTrain(1).StartStaName = "马泉营车辆段" AndAlso Me.CSLinkTrain(1).EndStaName = "马泉营" Then
                        If (UBound(Me.CSLinkTrain)) >= 4 Then
                            CanDriveTheTrain = False
                        End If
                    Else
                        If (UBound(Me.CSLinkTrain)) >= 3 Then
                            CanDriveTheTrain = False
                        End If
                    End If
                Case "白班"
                    If (UBound(Me.CSLinkTrain)) >= 6 Then
                        CanDriveTheTrain = False
                    End If
                Case "夜班"
                    If (UBound(Me.CSLinkTrain)) >= 5 Then
                        CanDriveTheTrain = False
                    End If
            End Select

            '状态优先
            Select Case Me.State
                Case "用餐"
                    If AddLitterTime(Me.DinnerEndTime) > SecTime Then
                        CanDriveTheTrain = False
                    Else
                        CanDriveTheTrain = True
                        Exit Function
                        'End If
                    End If
            End Select

            '间隔判断
            If CheckIfInterval(Train) = False Then
                CanDriveTheTrain = False
            End If
            '判断休息时间
            Dim restTime As Integer = ChangePlaceRestTime(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).UpOrDown, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndTime)
            If restTime <> 0 AndAlso ((CanDriveTheTrain = False AndAlso SecTime - FirTime > 40 * 60) Or SecTime - FirTime < restTime Or Train.CSLinkTrains(1).nCheDiID = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).nCheDiID) Then
                CanDriveTheTrain = False
            End If
            '不是轮换点上下行一样的接车
            If restTime = 0 Then
                If Me.CSLinkTrain(UBound(Me.CSLinkTrain)).SecondStation.IsYard = False And Me.CSLinkTrain(UBound(Me.CSLinkTrain)).SecondStation.IsLast = False Then
                    If Train.CSLinkTrains(1).UpOrDown <> Me.CSLinkTrain(UBound(Me.CSLinkTrain)).UpOrDown Then
                        CanDriveTheTrain = False
                    End If
                Else
                    If Train.CSLinkTrains(1).UpOrDown = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).UpOrDown Then
                        CanDriveTheTrain = False
                    End If
                End If
            End If
        End If
    End Function

    Public Function DriveDinnnerTrain(ByRef Train As MergedCSLinkTrain, Optional tempFangkuanxianzhi As Boolean = False) As Boolean '正向找车（目前用于早班、白班、日勤班）
        DriveDinnnerTrain = False
        If UBound(Me.CSLinkTrain) = 0 Then '不存在该司机
            Return False
        End If
        Dim FirTime As Integer = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).CulEndTime
        Dim SecTime As Integer = AddLitterTime(Train.CSLinkTrains(1).FirstStation.DepartTime)
        '是否是随乘过来的，若是，满足时间要求即可驾驶
        If Me.ListSuichengCSLinkTrain.Count > 0 AndAlso Me.ListSuichengCSLinkTrain(Me.ListSuichengCSLinkTrain.Count - 1) Is Me.CSLinkTrain(UBound(Me.CSLinkTrain)) Then
            If Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName = Train.CSLinkTrains(1).StartStaName AndAlso FirTime <= SecTime Then
                Return True
            End If
        End If
        If Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName = Train.CSLinkTrains(1).StartStaName AndAlso FirTime <= SecTime Then                '一旦司机接非同一车底，该司机下车后必须有休息时间
            If Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName <> Train.CSLinkTrains(1).RoutingName And CheckNextJiaolu(Train) = False Then
                Exit Function
            End If
            DriveDinnnerTrain = True
            '先满足公里数
            Select Case Me.DutySort
                Case "白班"
                    If Me.DriveDistance + Train.distance > CS_DayMaxLength And ForceDriveLength = 1 Then
                        DriveDinnnerTrain = False
                    End If
                    If Me.DriveDistance + Train.distance < CS_DayMinLength Then
                        DriveDinnnerTrain = True
                    End If
                Case "日勤班"
                    If Me.DriveDistance + Train.distance > CS_CDayMaxLength And ForceDriveLength = 1 Then
                        DriveDinnnerTrain = False
                    End If
            End Select
            Select Case Me.DutySort
                Case "白班"
                    If IsDayDutyOffPlace(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName, Me.CSLinkTrain(1).StartStaName, Me.CSLinkTrain(1).RoutingName, Me.CSLinkTrain(1).StartTime) = False Then
                        DriveDinnnerTrain = True '如果不在下班地点，必须接车
                    End If
                    If IsDayDutyOffPlace(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName, Me.CSLinkTrain(1).StartStaName, Me.CSLinkTrain(1).RoutingName, Me.CSLinkTrain(1).StartTime) Then
                        If Train.CulStartTime > AStartTime And ForceDutyTime = 1 Then
                            DriveDinnnerTrain = False
                        End If
                    End If
                Case "日勤班"
                    If Me.DriveDistance + 2 * Train.distance > CS_CDayMaxLength Then
                        DriveDinnnerTrain = False
                    End If
            End Select
            '早班回库车必须早班接
            If Train.CSLinkTrains(UBound(Train.CSLinkTrains)).SecondStation.IsYard = True Then
                If Me.DutySort = "早班" And Train.CSLinkTrains(UBound(Train.CSLinkTrains)).CulEndTime < 12 * 3600 Then
                    DriveDinnnerTrain = True
                End If
                If Me.DutySort = "白班" And Train.CSLinkTrains(UBound(Train.CSLinkTrains)).CulEndTime > 12 * 3600 Then
                    DriveDinnnerTrain = True
                End If
            End If
            '间隔判断
            If CheckIfInterval(Train) = False Then
                DriveDinnnerTrain = False
            End If
            '判断休息时间
            Dim restTime As Integer = ChangePlaceRestTime(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).UpOrDown, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndTime)
            If restTime <> 0 AndAlso (SecTime - FirTime > 30 * 60 Or SecTime - FirTime < restTime Or Train.CSLinkTrains(1).nCheDiID = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).nCheDiID) Then
                DriveDinnnerTrain = False
            End If
            '不是轮换点上下行一样的接车
            If restTime = 0 Then
                If Me.CSLinkTrain(UBound(Me.CSLinkTrain)).SecondStation.IsYard = False And Me.CSLinkTrain(UBound(Me.CSLinkTrain)).SecondStation.IsLast = False Then
                    If Train.CSLinkTrains(1).UpOrDown <> Me.CSLinkTrain(UBound(Me.CSLinkTrain)).UpOrDown Then
                        DriveDinnnerTrain = False
                    End If
                Else
                    If Train.CSLinkTrains(1).UpOrDown = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).UpOrDown Then
                        DriveDinnnerTrain = False
                    End If
                End If
            End If

            Select Case Me.State
                Case "用餐"
                    If DriveDinnnerTrain = True AndAlso IsShiftPlace(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName, Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndTime) = False Then
                        For Each s As String In Me.AllDinnerInfo.Keys
                            If Me.AllDinnerInfo(s).dinnerType = "晚餐" Then
                                Dim tmpdinnerstart As Integer = AddLitterTime(Train.CSLinkTrains(1).StartTime) - Me.AllDinnerInfo(s).DinnerTime
                                Dim selectdri As CSDriver = Nothing
                                Dim mintime As Integer = 10000000
                                For Each cdriver As CSDriver In CSTrainsAndDrivers.CSDrivers
                                    If cdriver Is Nothing OrElse cdriver.DutySort = "早班" Then
                                        Continue For
                                    End If
                                    If AddLitterTime(cdriver.CSLinkTrain(UBound(cdriver.CSLinkTrain)).EndTime) < tmpdinnerstart And cdriver.CSLinkTrain(UBound(cdriver.CSLinkTrain)).EndStaName = Train.CSLinkTrains(1).StartStaName Then
                                        If mintime > tmpdinnerstart - AddLitterTime(cdriver.CSLinkTrain(UBound(cdriver.CSLinkTrain)).EndTime) Then
                                            mintime = tmpdinnerstart - AddLitterTime(cdriver.CSLinkTrain(UBound(cdriver.CSLinkTrain)).EndTime)
                                            selectdri = cdriver
                                        End If
                                    End If
                                Next
                                If selectdri IsNot Nothing AndAlso selectdri.CSdriverNo <> Me.CSdriverNo Then
                                    If selectdri.State = "用餐" Then
                                        DriveDinnnerTrain = False
                                    Else
                                        If mintime < 15 * 60 Then
                                            DriveDinnnerTrain = False
                                        Else
                                            Me.AllDinnerInfo.Remove(s)
                                            Me.FlagDinner = False
                                            Me.State = "班中"
                                        End If
                                    End If
                                Else
                                    Me.AllDinnerInfo.Remove(s)
                                    Me.FlagDinner = False
                                    Me.State = "班中"
                                End If
                                Exit For
                            End If
                        Next
                    Else
                        If AddLitterTime(Me.DinnerEndTime) > SecTime Then
                            DriveDinnnerTrain = False
                        Else
                            DriveDinnnerTrain = True
                            Exit Function
                        End If
                    End If
            End Select
        End If
    End Function
    Public Function CheckIfInterval(ByVal Train As MergedCSLinkTrain, Optional Direct As Boolean = False) As Boolean '判断是否满足间隔,默认正向
        Dim arrival As Boolean = False
        Dim interval As Integer = 0
        Dim direction As Integer = 0
        If Direct = False Then
            For i = 0 To ChangeStationList.Count - 1
                If ChangeStationList(i).Name = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName And ChangeStationList(i).JiaoLuName = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName Then
                    If (ChangeStationList(i).Direction = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).UpOrDown Or ChangeStationList(i).Direction = 2) Then
                        If ChangeStationList(i).TimeSpanList.EndTime = ChangeStationList(i).TimeSpanList.StartTime Then '没有时间限制
                            interval = ChangeStationList(i).FollowNo
                            direction = ChangeStationList(i).UpTrainDirection
                            Exit For
                        End If
                        If AddLitterTime(ChangeStationList(i).TimeSpanList.StartTime) <= AddLitterTime(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndTime) And AddLitterTime(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndTime) <= AddLitterTime(ChangeStationList(i).TimeSpanList.EndTime) Then
                            interval = ChangeStationList(i).FollowNo
                            direction = ChangeStationList(i).UpTrainDirection
                            Exit For
                        End If
                    End If
                End If
            Next
            If interval <> 0 Then '没有间隔按时间则默认为可以接车
                Dim TrainList As New List(Of Integer)
                Dim minTime As Integer = 1000000000
                Dim minIndex As Integer = -1
                Dim searchTime As Integer = 0
                For i As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                    If searchTime > 15 Then
                        Exit For
                    End If
                    If CSTrainsAndDrivers.MergedCSLinkTrains(i).StartStaName = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName And (CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains(1).UpOrDown = direction Or direction = 2) Then
                        If AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(i).StartTime) > AddLitterTime(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndTime) Then
                            Dim Rejiaolu As Boolean = False
                            For j As Integer = 1 To UBound(Jiaolu)
                                If Jiaolu(j).JiaoluName = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName And Jiaolu(j).ReJiaoluName = CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains(1).RoutingName Then
                                    Rejiaolu = True
                                    Exit For
                                End If
                            Next
                            If Rejiaolu = False Then
                                Continue For
                            End If
                            If CSTrainsAndDrivers.MergedCSLinkTrains(i).nCheDiID = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).nCheDiID And minTime > AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(i).StartTime) - AddLitterTime(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndTime) Then
                                minTime = AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(i).StartTime) - AddLitterTime(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndTime)
                                minIndex = i
                            End If
                            TrainList.Add(i)
                            searchTime += 1
                        End If
                    End If
                Next
                If TrainList.Count >= interval And minIndex > -1 Then
                    For i As Integer = 0 To TrainList.Count - 2
                        For j As Integer = i + 1 To TrainList.Count - 1
                            If AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(TrainList(i)).StartTime) > AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(TrainList(j)).StartTime) Then
                                Dim tmp As Integer = TrainList(i)
                                TrainList(i) = TrainList(j)
                                TrainList(j) = tmp
                            End If
                        Next
                    Next
                    For i As Integer = 0 To TrainList.Count - 1
                        If TrainList(i) = minIndex Then
                            If i + interval <= TrainList.Count - 1 Then
                                Dim skipBeiche As Integer = 0
                                For j As Integer = 1 To interval
                                    If CSTrainsAndDrivers.MergedCSLinkTrains(TrainList(i + j)).beiche = 2 Then
                                        skipBeiche += 1
                                    End If
                                Next
                                If i + interval + skipBeiche <= TrainList.Count - 1 AndAlso AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(TrainList(i + interval + skipBeiche)).StartTime) = AddLitterTime(Train.CSLinkTrains(1).StartTime) Then
                                    arrival = True
                                    Return arrival
                                End If
                            End If
                            Exit For
                        End If
                    Next
                End If
            Else
                arrival = True
            End If
        Else
            For i = 0 To ChangeStationList.Count - 1
                If ChangeStationList(i).Name = Train.CSLinkTrains(UBound(Train.CSLinkTrains)).EndStaName And ChangeStationList(i).JiaoLuName = Train.CSLinkTrains(UBound(Train.CSLinkTrains)).RoutingName Then
                    If (ChangeStationList(i).Direction = Train.CSLinkTrains(UBound(Train.CSLinkTrains)).UpOrDown Or ChangeStationList(i).Direction = 2) Then
                        If ChangeStationList(i).TimeSpanList.EndTime = ChangeStationList(i).TimeSpanList.StartTime Then '没有时间限制
                            interval = ChangeStationList(i).FollowNo
                            direction = ChangeStationList(i).UpTrainDirection
                            Exit For
                        End If
                        If AddLitterTime(ChangeStationList(i).TimeSpanList.StartTime) <= AddLitterTime(Train.CSLinkTrains(UBound(Train.CSLinkTrains)).EndTime) And AddLitterTime(Train.CSLinkTrains(UBound(Train.CSLinkTrains)).EndTime) <= AddLitterTime(ChangeStationList(i).TimeSpanList.EndTime) Then
                            interval = ChangeStationList(i).FollowNo
                            direction = ChangeStationList(i).UpTrainDirection
                            Exit For
                        End If
                    End If
                End If
            Next
            If interval <> 0 Then '没有间隔按时间则默认为可以接车
                Dim TrainList As New List(Of Integer)
                Dim minTime As Integer = 1000000000
                Dim minIndex As Integer = -1

                For i As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                   
                    If CSTrainsAndDrivers.MergedCSLinkTrains(i).StartStaName = Train.CSLinkTrains(UBound(Train.CSLinkTrains)).EndStaName And (CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains(1).UpOrDown = direction Or direction = 2) Then
                        If AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(i).StartTime) > AddLitterTime(Train.CSLinkTrains(UBound(Train.CSLinkTrains)).EndTime) Then
                            Dim Rejiaolu As Boolean = False
                            For j As Integer = 1 To UBound(Jiaolu)
                                If Jiaolu(j).JiaoluName = Train.CSLinkTrains(UBound(Train.CSLinkTrains)).RoutingName And Jiaolu(j).ReJiaoluName = CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains(1).RoutingName Then
                                    Rejiaolu = True
                                    Exit For
                                End If
                            Next
                            If Rejiaolu = False Then
                                Continue For
                            End If
                            If CSTrainsAndDrivers.MergedCSLinkTrains(i).nCheDiID = Train.CSLinkTrains(UBound(Train.CSLinkTrains)).nCheDiID And minTime > AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(i).StartTime) - AddLitterTime(Train.CSLinkTrains(UBound(Train.CSLinkTrains)).EndTime) Then
                                minTime = AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(i).StartTime) - AddLitterTime(Train.CSLinkTrains(UBound(Train.CSLinkTrains)).EndTime)
                                minIndex = i
                            End If
                            TrainList.Add(i)
                        End If

                    End If
                Next
                If TrainList.Count >= interval And minIndex > -1 Then
                    For i As Integer = 0 To TrainList.Count - 2
                        For j As Integer = i + 1 To TrainList.Count - 1
                            If AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(TrainList(i)).StartTime) > AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(TrainList(j)).StartTime) Then
                                Dim tmp As Integer = TrainList(i)
                                TrainList(i) = TrainList(j)
                                TrainList(j) = tmp
                            End If
                        Next
                    Next
                    For i As Integer = 0 To TrainList.Count - 1
                        If TrainList(i) = minIndex Then
                            If i + interval <= TrainList.Count - 1 Then
                                Dim skipBeiche As Integer = 0
                                For j As Integer = 1 To interval
                                    If CSTrainsAndDrivers.MergedCSLinkTrains(TrainList(i + j)).beiche = 2 Then
                                        skipBeiche += 1
                                    End If
                                Next
                                If i + interval + skipBeiche <= TrainList.Count - 1 AndAlso AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(TrainList(i + interval + skipBeiche)).StartTime) = AddLitterTime(Me.CSLinkTrain(1).StartTime) Then
                                    arrival = True
                                    Return arrival
                                End If
                            End If
                            Exit For
                        End If
                    Next

                End If
            Else
                arrival = True
            End If
        End If
        Return arrival
    End Function
    Public Function CheckIfInterval(ByVal interval As Integer, ByVal trainDirection As Integer, ByVal Train As CSLinkTrain, Optional Direct As Boolean = False) '判断是否满足间隔,默认正向
        Dim arrival As Boolean = False
        If Direct = False Then
            If interval <> 0 Then '没有间隔按时间则默认为可以接车
                Dim TrainList As New List(Of Integer)
                Dim minTime As Integer = 1000000000
                Dim minIndex As Integer = -1
                For i As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                    If CSTrainsAndDrivers.MergedCSLinkTrains(i).StartStaName = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName And (CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains(1).UpOrDown = trainDirection Or trainDirection = 2) Then
                        If AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(i).StartTime) > AddLitterTime(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndTime) Then
                            TrainList.Add(i)
                            If CSTrainsAndDrivers.MergedCSLinkTrains(i).nCheDiID = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).nCheDiID And minTime > AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(i).StartTime) - AddLitterTime(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndTime) Then
                                minTime = AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(i).StartTime) - AddLitterTime(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndTime)
                                minIndex = i
                            End If
                        End If
                    End If
                Next
                If TrainList.Count >= interval And minIndex > -1 Then
                    For i As Integer = 0 To TrainList.Count - 2
                        For j As Integer = i + 1 To TrainList.Count - 1
                            If AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(TrainList(i)).StartTime) > AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(TrainList(j)).StartTime) Then
                                Dim tmp As Integer = TrainList(i)
                                TrainList(i) = TrainList(j)
                                TrainList(j) = tmp
                            End If
                        Next
                    Next
                    For i As Integer = 0 To TrainList.Count - 1
                        If TrainList(i) = minIndex Then
                            If i + interval <= TrainList.Count - 1 Then
                                Dim skipBeiche As Integer = 0
                                For j As Integer = 1 To interval
                                    If CSTrainsAndDrivers.MergedCSLinkTrains(TrainList(i + j)).beiche = 2 Then
                                        skipBeiche += 1
                                    End If
                                Next
                                If i + interval + skipBeiche <= TrainList.Count - 1 AndAlso AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(TrainList(i + interval + skipBeiche)).StartTime) = AddLitterTime(Train.StartTime) Then
                                    arrival = True
                                    Return arrival
                                End If
                            End If
                            Exit For
                        End If
                    Next
                End If
            Else
                arrival = True
            End If
        Else
            If interval <> 0 Then '没有间隔按时间则默认为可以接车
                Dim TrainList As New List(Of Integer)
                Dim minTime As Integer = 1000000000
                Dim minIndex As Integer = -1
                For i As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                    If CSTrainsAndDrivers.MergedCSLinkTrains(i).StartStaName = Train.EndStaName And (CSTrainsAndDrivers.MergedCSLinkTrains(i).CSLinkTrains(1).UpOrDown = trainDirection Or trainDirection = 2) Then
                        If AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(i).StartTime) > AddLitterTime(Train.EndTime) Then
                            TrainList.Add(i)
                            If CSTrainsAndDrivers.MergedCSLinkTrains(i).nCheDiID = Train.nCheDiID And minTime > AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(i).StartTime) - AddLitterTime(Train.EndTime) Then
                                minTime = AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(i).StartTime) - AddLitterTime(Train.EndTime)
                                minIndex = i
                            End If
                        End If

                    End If
                Next
                If TrainList.Count >= interval And minIndex > -1 Then
                    For i As Integer = 0 To TrainList.Count - 2
                        For j As Integer = i + 1 To TrainList.Count - 1
                            If AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(TrainList(i)).StartTime) > AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(TrainList(j)).StartTime) Then
                                Dim tmp As Integer = TrainList(i)
                                TrainList(i) = TrainList(j)
                                TrainList(j) = tmp
                            End If
                        Next
                    Next
                    For i As Integer = 0 To TrainList.Count - 1
                        If TrainList(i) = minIndex Then
                            If i + interval <= TrainList.Count - 1 Then
                                Dim skipBeiche As Integer = 0
                                For j As Integer = 1 To interval
                                    If CSTrainsAndDrivers.MergedCSLinkTrains(TrainList(i + j)).beiche = 2 Then
                                        skipBeiche += 1
                                    End If
                                Next
                                If i + interval + skipBeiche <= TrainList.Count - 1 AndAlso AddLitterTime(CSTrainsAndDrivers.MergedCSLinkTrains(TrainList(i + interval + skipBeiche)).StartTime) = AddLitterTime(Me.CSLinkTrain(1).StartTime) Then
                                    arrival = True
                                    Return arrival
                                End If
                            End If
                            Exit For
                        End If
                    Next

                End If
            Else
                arrival = True
            End If
        End If

        Return arrival
    End Function
    Public Function CheckNextJiaolu(ByVal Train As MergedCSLinkTrain, Optional Direct As Boolean = False) '判断存在接续交路,默认正向
        Dim ifNextJiaolu As Boolean = False
        If Direct = True Then
            For i As Integer = 1 To UBound(Jiaolu)
                If Jiaolu(i).ReJiaoluName = Me.CSLinkTrain(1).RoutingName And Jiaolu(i).JiaoluName = Train.CSLinkTrains(UBound(Train.CSLinkTrains)).RoutingName Then
                    ifNextJiaolu = True
                    Exit For
                End If
            Next
        Else
            For i As Integer = 1 To UBound(Jiaolu)
                If Jiaolu(i).JiaoluName = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName And Jiaolu(i).ReJiaoluName = Train.CSLinkTrains(1).RoutingName Then
                    ifNextJiaolu = True
                    Exit For
                End If
            Next
        End If
        Return ifNextJiaolu
    End Function

    Private NeedShifting As Boolean = False '指示是否需要下班
    Private Needdinnering As Boolean = False '指示是否需要吃饭
    Private NeedRest As Boolean = False '指示是否需要休息

    ''' <summary>
    ''' 连续驾驶事件，用于判断是否需要休息
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DriConDriveTime As Integer
        Get
            If UBound(Me.CSLinkTrain) > 0 Then
                Dim index As Integer = 1
                For i As Integer = UBound(Me.CSLinkTrain) To 1 Step -1
                    If i > 1 Then
                        If Me.CSLinkTrain(i).nCheDiID <> Me.CSLinkTrain(i - 1).nCheDiID Then
                            index = i
                            Exit For
                        ElseIf Me.CSLinkTrain(i).nCheDiID = Me.CSLinkTrain(i - 1).nCheDiID AndAlso Me.CSLinkTrain(i).CulStartTime - 1200 > Me.CSLinkTrain(i - 1).CulEndTime Then
                            index = i
                            Exit For
                        End If
                    End If
                Next
                Return Me.CSLinkTrain(UBound(Me.CSLinkTrain)).CulEndTime - Me.CSLinkTrain(index).CulStartTime
            Else
                Return 0
            End If
        End Get
    End Property

    Public Sub RefreshState(Optional ByVal IFZIDONG As Boolean = False, Optional ByVal RefreshDirection As Boolean = True) 'RefreshDirection true为正向，false为逆向
        Select Case Me.DutySort
            Case "早班"
                If Me.State = "" Then
                    Me.State = "班中"
                End If
            Case "日勤班"
                Me.State = "班中"
            Case "白班"
                Me.State = "班中"
            Case "夜班"
                Me.State = "班中"
        End Select
        '==============用餐判断
        Dim dinnerTimeitem As typeDinnerStation = Nothing
        Dim banqian As Boolean = False
        For i As Integer = 1 To sysDinnerStation.Count - 1
            If sysDinnerStation(i).dutySort = Me.DutySort Then
                If RefreshDirection = True Then
                    If UBound(Me.CSLinkTrain) = 1 Then
                        For j As Integer = 1 To UBound(Jiaolu)
                            If Jiaolu(j).JiaoluName = sysDinnerStation(i).Routing And Jiaolu(j).ReJiaoluName = Me.CSLinkTrain(1).RoutingName Then
                                If Me.CSLinkTrain(1).StartStaName = sysDinnerStation(i).DinnerStationName And AddLitterTime(Me.CSLinkTrain(1).StartTime) >= AddLitterTime(sysDinnerStation(i).dinnerStartTime) And AddLitterTime(Me.CSLinkTrain(1).StartTime) <= AddLitterTime(sysDinnerStation(i).dinnerEndTime) Then '防止白班出现2次吃饭，也只有白班可能出现
                                    dinnerTimeitem = sysDinnerStation(i)
                                    banqian = True
                                    Exit For
                                End If
                            End If
                        Next
                    End If
                    If Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName = sysDinnerStation(i).DinnerStationName AndAlso Me.CSLinkTrain(UBound(Me.CSLinkTrain)).RoutingName = sysDinnerStation(i).Routing And AddLitterTime(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndTime) >= AddLitterTime(sysDinnerStation(i).dinnerStartTime) And AddLitterTime(Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndTime) <= AddLitterTime(sysDinnerStation(i).dinnerEndTime) Then '防止白班出现2次吃饭，也只有白班可能出现
                        dinnerTimeitem = sysDinnerStation(i)
                    End If
                Else
                    For j As Integer = 1 To UBound(Jiaolu)
                        If Jiaolu(j).JiaoluName = sysDinnerStation(i).Routing And Jiaolu(j).ReJiaoluName = Me.CSLinkTrain(1).RoutingName Then
                            If Me.CSLinkTrain(1).StartStaName = sysDinnerStation(i).DinnerStationName And AddLitterTime(Me.CSLinkTrain(1).StartTime) >= AddLitterTime(sysDinnerStation(i).dinnerStartTime) + sysDinnerStation(i).DinnerTime And AddLitterTime(Me.CSLinkTrain(1).StartTime) <= AddLitterTime(sysDinnerStation(i).dinnerEndTime) + sysDinnerStation(i).DinnerTime Then '防止白班出现2次吃饭，也只有白班可能出现
                                dinnerTimeitem = sysDinnerStation(i)
                                Exit For
                            End If
                        End If
                    Next
                End If
            End If
        Next
        If dinnerTimeitem IsNot Nothing Then
            If RefreshDirection = True Then
                If Me.FlagDinner = False Then
                    If banqian = True Then
                        Me.FlagDinner = True
                        Me.DinnerStation = Me.CSLinkTrain(1).StartStaName
                        Me.DinnerStartTime = Me.CSLinkTrain(1).CulStartTime - dinnerTimeitem.DinnerTime
                        Me.DinnerEndTime = Me.CSLinkTrain(1).CulStartTime
                        AllDinnerInfo.Add(Me.DinnerStation & "-" & Me.DinnerStartTime.ToString & "-" & Me.DinnerEndTime.ToString, dinnerTimeitem)
                    Else
                        Me.State = "用餐"
                        Me.FlagDinner = True
                        Me.DinnerStation = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName
                        Me.DinnerStartTime = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).CulEndTime
                        Me.DinnerEndTime = Me.DinnerStartTime + dinnerTimeitem.DinnerTime
                        AllDinnerInfo.Add(Me.DinnerStation & "-" & Me.DinnerStartTime.ToString & "-" & Me.DinnerEndTime.ToString, dinnerTimeitem)
                    End If
                Else
                    If AllDinnerInfo.Count > 0 Then
                        Dim ifhaveSameDinnerItem As Boolean = False
                        For Each dinneritem As typeDinnerStation In AllDinnerInfo.Values
                            If dinnerTimeitem.dinnerType = dinneritem.dinnerType Then
                                ifhaveSameDinnerItem = True
                                Exit For
                            End If
                        Next
                        If ifhaveSameDinnerItem = False Then
                            Me.State = "用餐"
                            Me.FlagDinner = True
                            Me.DinnerStation = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).EndStaName
                            Me.DinnerStartTime = Me.CSLinkTrain(UBound(Me.CSLinkTrain)).CulEndTime
                            Me.DinnerEndTime = Me.DinnerStartTime + dinnerTimeitem.DinnerTime
                            AllDinnerInfo.Add(Me.DinnerStation & "-" & Me.DinnerStartTime.ToString & "-" & Me.DinnerEndTime.ToString, dinnerTimeitem)
                        End If
                    End If
                End If
            Else
                If Me.FlagDinner = False Then
                    Me.State = "用餐"
                    Me.FlagDinner = True
                    Me.DinnerStation = Me.CSLinkTrain(1).StartStaName
                    Me.DinnerEndTime = Me.CSLinkTrain(1).CulStartTime
                    Me.DinnerStartTime = Me.DinnerEndTime - dinnerTimeitem.DinnerTime
                    AllDinnerInfo.Add(Me.DinnerStation & "-" & Me.DinnerStartTime.ToString & "-" & Me.DinnerEndTime.ToString, dinnerTimeitem)
                Else
                    If AllDinnerInfo.Count > 0 Then
                        Dim ifhaveSameDinnerItem As Boolean = False
                        For Each dinneritem As typeDinnerStation In AllDinnerInfo.Values
                            If dinnerTimeitem.dinnerType = dinneritem.dinnerType Then
                                ifhaveSameDinnerItem = True
                                Exit For
                            End If
                        Next
                        If ifhaveSameDinnerItem = False Then
                            Me.State = "用餐"
                            Me.FlagDinner = True
                            Me.DinnerStation = Me.CSLinkTrain(1).StartStaName
                            Me.DinnerEndTime = Me.CSLinkTrain(1).CulStartTime
                            Me.DinnerStartTime = Me.DinnerEndTime - dinnerTimeitem.DinnerTime
                            AllDinnerInfo.Add(Me.DinnerStation & "-" & Me.DinnerStartTime.ToString & "-" & Me.DinnerEndTime.ToString, dinnerTimeitem)
                        End If
                    End If
                End If
            End If
        End If

        '==============班后判断
        Dim isavatrains As Boolean = False
        If RefreshDirection = True Then
            If UBound(CSTrainsAndDrivers.MergedCSLinkTrains) > 0 Then
                For i As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                    If IFZIDONG = False Then
                        If CSTrainsAndDrivers.MergedCSLinkTrains(i).IsLinked = False AndAlso Me.CanDriveTheTrain(CSTrainsAndDrivers.MergedCSLinkTrains(i)) Then
                            isavatrains = True
                            Exit For
                        End If
                    Else
                        If Me.CanDriveTheTrain(CSTrainsAndDrivers.MergedCSLinkTrains(i)) Then
                            isavatrains = True
                            Exit For
                        End If
                    End If
                Next
            End If
            If isavatrains = False Then
                Me.State = "班后"
            End If
        Else
            If UBound(CSTrainsAndDrivers.MergedCSLinkTrains) > 0 Then
                For i As Integer = UBound(CSTrainsAndDrivers.MergedCSLinkTrains) To 1 Step -1
                    If IFZIDONG = False Then
                        If CSTrainsAndDrivers.MergedCSLinkTrains(i).IsLinked = False AndAlso Me.CanDriveTheTrain1(CSTrainsAndDrivers.MergedCSLinkTrains(i)) Then
                            isavatrains = True
                            Exit For
                        End If
                    Else
                        If Me.CanDriveTheTrain1(CSTrainsAndDrivers.MergedCSLinkTrains(i)) Then
                            isavatrains = True
                            Exit For
                        End If
                    End If
                Next
            End If
            If isavatrains = False Then
                Me.State = "班后"
            End If
        End If
    End Sub
    Public Sub AddTrain(ByVal Train As CSLinkTrain, Optional ByVal IsDeadheading As Boolean = False)
        ReDim Preserve Me.CSLinkTrain(UBound(Me.CSLinkTrain) + 1)
        Me.CSLinkTrain(UBound(Me.CSLinkTrain)) = Train
        If IsDeadheading = False Then
            Me.CurCheDiID = Train.nCheDiID
            If UBound(Me.CSLinkTrain) <= 1 Then
                Me.CurDriveTime = Me.CurDriveTime + Train.RunTime
            Else
                If Train.CulStartTime >= Me.CurStationTime + ConDriveTime AndAlso Train.nCheDiID <> Me.CSLinkTrain(UBound(Me.CSLinkTrain) - 1).nCheDiID Then
                    Me.CurDriveTime = Train.RunTime
                Else
                    Me.CurDriveTime = Me.CurDriveTime + Train.RunTime
                End If
            End If
            Me.CurStationID = Train.EndStaID
            Me.CurStationName = Train.EndStaName
            If IsTransitMustChange(Train.EndStaName, Train.RoutingName, Train.UpOrDown, Train.EndTime) Then
                Me.CurStationTime = Train.CulEndTime + ChangePlaceRestTime(Train.EndStaName, Train.RoutingName, Train.UpOrDown, Train.EndTime)
                Me.CurDriveTime = 0
            Else
                Me.CurStationTime = Train.CulEndTime
            End If
            Me.CurDirection = Train.UpOrDown
            Me.FlagRoutingCha = Train.RoutingCha
            Me.FlagRoutingName = Train.RoutingName
            Train.IsLinked = True
            Train.OverDutySort = DutySort
        End If
    End Sub

    Public Sub RemoveTrain(ByVal index As Integer)
        If index <> 0 AndAlso index < UBound(Me.CSLinkTrain) Then
            For i As Integer = index + 1 To UBound(Me.CSLinkTrain)
                Me.CSLinkTrain(i).IsLinked = False
                Me.CSLinkTrain(i).OverDutySort = ""
            Next
            ReDim Preserve Me.CSLinkTrain(index)
            Me.CurCheDiID = Me.CSLinkTrain(index).nCheDiID
            Me.CurDriveTime = Me.CSLinkTrain(index).RunTime
            Me.CurStationID = Me.CSLinkTrain(index).EndStaID
            Me.CurStationTime = Me.CSLinkTrain(index).CulEndTime
            Me.CurStationName = Me.CSLinkTrain(index).EndStaName
            Me.CurDirection = Me.CSLinkTrain(index).UpOrDown
            Me.FlagRoutingCha = Me.CSLinkTrain(index).RoutingCha
            Me.FlagRoutingName = Me.CSLinkTrain(index).RoutingName
        End If
    End Sub

    Public Sub ReRemoveTrain(ByVal index As Integer)
        If index <> 0 AndAlso index < UBound(Me.CSLinkTrain) Then
            For i As Integer = 1 To index
                Me.CSLinkTrain(i).IsLinked = False
                Me.CSLinkTrain(i).OverDutySort = ""
            Next
            For i As Integer = index + 1 To UBound(Me.CSLinkTrain)
                Me.CSLinkTrain(i - index) = Me.CSLinkTrain(i)
            Next
            ReDim Preserve Me.CSLinkTrain(UBound(Me.CSLinkTrain) - index)
            Me.CurCheDiID = Me.CSLinkTrain(1).nCheDiID
            Me.CurDriveTime = Me.CSLinkTrain(1).RunTime
            Me.CurStationID = Me.CSLinkTrain(1).StartStaID
            Me.CurStationTime = Me.CSLinkTrain(1).CulStartTime
            Me.CurStationName = Me.CSLinkTrain(1).StartStaName
            Me.CurDirection = Me.CSLinkTrain(1).UpOrDown
            Me.FlagRoutingCha = Me.CSLinkTrain(1).RoutingCha
            Me.FlagRoutingName = Me.CSLinkTrain(1).RoutingName
        End If
    End Sub

   

    Public Sub ReAddTrain(ByVal Train As CSLinkTrain, Optional ByVal IsDeadHeading As Boolean = False)
        ReDim Preserve Me.CSLinkTrain(UBound(Me.CSLinkTrain) + 1)
        If UBound(Me.CSLinkTrain) > 1 Then
            For i As Integer = UBound(Me.CSLinkTrain) To 2 Step -1
                Me.CSLinkTrain(i) = Me.CSLinkTrain(i - 1)
            Next
        End If
        Me.CSLinkTrain(1) = Train
        If IsDeadHeading = False Then
            Me.CurCheDiID = Train.nCheDiID
            Me.CurStationID = Train.StartStaID
            Me.CurStationName = Train.StartStaName
            If IsTransitMustChange(Train.StartStaName, Train.RoutingName, Train.UpOrDown, Train.CulStartTime) Then
                Me.CurStationTime = Train.CulStartTime - ChangePlaceRestTime(Train.StartStaName, Train.RoutingName, Train.UpOrDown, Train.CulStartTime, False)
                Me.CurDriveTime = 0
            Else
                Me.CurStationTime = Train.CulStartTime
            End If
            Me.CurDirection = Train.UpOrDown
            Me.FlagRoutingCha = Train.RoutingCha
            Me.FlagRoutingName = Train.RoutingName
            Train.IsLinked = True
            Train.OverDutySort = DutySort
        End If
    End Sub

    ''' <summary>
    ''' Add MergedCSLinkTrain
    ''' </summary>
    ''' <param name="MergedTrain"></param>
    ''' <remarks></remarks>
    Public Sub AddMergedTrain(ByVal MergedTrain As MergedCSLinkTrain, Optional ByVal IsDeadHeading As Boolean = False) 'ByRef
        Dim i As Integer
        For i = 1 To UBound(MergedTrain.CSLinkTrains)
            Me.AddTrain(MergedTrain.CSLinkTrains(i), IsDeadHeading)
        Next
    End Sub

    Public Sub AddReMergedTrain(ByVal MergedTrain As MergedCSLinkTrain, Optional ByVal IsDeadHeading As Boolean = False) 'ByRef
        Dim i As Integer
        For i = UBound(MergedTrain.CSLinkTrains) To 1 Step -1
            Me.ReAddTrain(MergedTrain.CSLinkTrains(i), IsDeadHeading)
        Next
    End Sub

    Public Shared Operator =(ByVal dri1 As CSDriver, ByVal dri2 As CSDriver)           '判断司机是否相等
        Dim equal As Boolean = True
        If UBound(dri1.CSLinkTrain) > 0 AndAlso UBound(dri2.CSLinkTrain) > 0 AndAlso UBound(dri1.CSLinkTrain) = UBound(dri2.CSLinkTrain) Then
            For i As Integer = 0 To UBound(dri1.CSLinkTrain)
                If dri1.CSLinkTrain(i) IsNot dri2.CSLinkTrain(i) Then
                    equal = False
                End If
            Next
        Else
            equal = False
        End If
        Return equal
    End Operator

    Public Shared Operator <>(ByVal dri1 As CSDriver, ByVal dri2 As CSDriver)           '判断司机是否相等
        Return (Not (dri1 = dri2))
    End Operator

    Public Sub RemoveTrain(ByVal Train As CSLinkTrain)

        Dim i, j As Integer
        If UBound(Me.CSLinkTrain) > 0 Then
            For i = 1 To UBound(Me.CSLinkTrain)
                If Me.CSLinkTrain(i).CSTrainID = Train.CSTrainID Then
                    ''减去备车时间
                    Train.IsLinked = False
                    Train.OverDutySort = ""
                    For j = i To UBound(Me.CSLinkTrain) - 1
                        Me.CSLinkTrain(j) = Me.CSLinkTrain(j + 1)
                    Next
                    ReDim Preserve Me.CSLinkTrain(UBound(Me.CSLinkTrain) - 1)
                    Exit For
                End If
            Next
        End If
    End Sub
  
    Public Sub New(ByVal driverNO As Integer)
        Me.TargetDutyTime = 0
        Me.CSDriverID = driverNO
        Me.CSdriverNo = CStr(Format(CInt(driverNO), "000")) 'i
        Me._DutySort = ""
        Me.CurDriveTime = 0
        Me.CurStationID = Nothing
        Me.CurCheDiID = Nothing
        Me.CurStationName = Nothing
        Me.CurStationTime = Nothing
        Me._DriveTime = 0
        Me._TotalDriveTime = 0
        Me._FirstDutyTime = 0
        Me.State = "班前"
        Me.FlagDinner = False
        Me.PreDutyTime = PrepareDutyTime
        Me.PreDutyOffTime = PrepareDutyOffTime
        Me.OutPutCSdriverNo = Me.CSdriverNo
        ReDim Me.CSLinkTrain(0)
        ReDim Me.CSLinkTrainID(0)
        Me.ListSuichengCSLinkTrain = New List(Of CSLinkTrain)
        Me.ListSuichengMergedCSLinkTrain = New List(Of MergedCSLinkTrain)
    End Sub
    Public Sub New()
        Me.TargetDutyTime = 0
        'Me.CSDriverID = ""
        Me.CSdriverNo = ""
        Me._DutySort = ""
        Me.CurDriveTime = 0
        Me.CurStationID = Nothing
        Me.CurCheDiID = Nothing
        Me.CurStationName = Nothing
        Me.CurStationTime = Nothing
        Me._DriveTime = 0
        Me._TotalDriveTime = 0
        Me._FirstDutyTime = 0
        Me.State = "班前"
        Me.FlagDinner = False
        Me.PreDutyTime = PrepareDutyTime
        Me.PreDutyOffTime = PrepareDutyOffTime
        Me.OutPutCSdriverNo = Me.CSdriverNo
        ReDim Me.CSLinkTrain(0)
        ReDim Me.CSLinkTrainID(0)
        Me.ListSuichengCSLinkTrain = New List(Of CSLinkTrain)
        Me.ListSuichengMergedCSLinkTrain = New List(Of MergedCSLinkTrain)
    End Sub

    Public Function DeepCloneDriver() As CSDriver           '深度复制本身
        DeepCloneDriver = New CSDriver
        DeepCloneDriver.bIfGouWang = Me.bIfGouWang
        DeepCloneDriver.CSDriverID = Me.CSDriverID
        DeepCloneDriver.CSdriverName = Me.CSdriverName
        ReDim DeepCloneDriver.CSLinkTrain(UBound(Me.CSLinkTrain))
        For i As Integer = 1 To UBound(Me.CSLinkTrain)
            DeepCloneDriver.CSLinkTrain(i) = Me.CSLinkTrain(i)
        Next
        ReDim DeepCloneDriver.CSLinkTrainID(UBound(Me.CSLinkTrainID))
        For i As Integer = 1 To UBound(Me.CSLinkTrainID)
            DeepCloneDriver.CSLinkTrainID(i) = Me.CSLinkTrainID(i)
        Next
        DeepCloneDriver.CurCheDiID = Me.CurCheDiID
        DeepCloneDriver.CurDirection = Me.CurDirection
        DeepCloneDriver.CurDriveTime = Me.CurDriveTime
        DeepCloneDriver.CurStationID = Me.CurStationID
        DeepCloneDriver.CurStationName = Me.CurStationName
        DeepCloneDriver.CurStationTime = Me.CurStationTime
        DeepCloneDriver.FlagDinner = Me.FlagDinner
        DeepCloneDriver.FlagRoutingCha = Me.FlagRoutingCha
        DeepCloneDriver.FlagRoutingName = Me.FlagRoutingName
        DeepCloneDriver.Isdinnering = Me.Isdinnering
        DeepCloneDriver.IsHaofeiDinnerNum = Me.IsHaofeiDinnerNum
        DeepCloneDriver.IsPrepareDri = Me.IsPrepareDri
        DeepCloneDriver.LinkedMorDriver = Me.LinkedMorDriver
        DeepCloneDriver.LinkedNightDriver = Me.LinkedNightDriver
        DeepCloneDriver.Needdinnering = Me.Needdinnering
        DeepCloneDriver.NeedRest = Me.NeedRest
        DeepCloneDriver.NeedShifting = Me.NeedShifting
        DeepCloneDriver.PreDutyOffTime = Me.PreDutyOffTime
        DeepCloneDriver.PreDutyTime = Me.PreDutyTime
        DeepCloneDriver.sDiverNo = Me.sDiverNo
        DeepCloneDriver.State = Me.State
        DeepCloneDriver._belongArea = Me._belongArea
        DeepCloneDriver._BeginWorkTime = Me._BeginWorkTime
        DeepCloneDriver._CSDriverno = Me._CSDriverno
        DeepCloneDriver._DinnerEndTime = Me._DinnerEndTime
        DeepCloneDriver._DinnerStartTime = Me._DinnerStartTime
        DeepCloneDriver._DinnerStation = Me._DinnerStation
        DeepCloneDriver._DriveDistance = Me._DriveDistance
        DeepCloneDriver._DriveTime = Me._DriveTime
        DeepCloneDriver._DutySort = Me._DutySort
        DeepCloneDriver._EndEorkTime = Me._EndEorkTime
        DeepCloneDriver._FirstDutyTime = Me._FirstDutyTime
        DeepCloneDriver._OutPutCSDriverno = Me._OutPutCSDriverno
        DeepCloneDriver._TotalDriveTime = Me._TotalDriveTime
        DeepCloneDriver._ZFTime = Me._ZFTime
    End Function



   

End Class
