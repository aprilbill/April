Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports System.Threading

Public Class FrmClassCoordinate

    Private net As Coordination2.Net
    Public CurLine As Coordination2.Line                                '当前线路
    Public cstimetableNameList() As String                              '乘务计划ID数组
    Public CSTimeTables() As Coordination2.CSTimeTable                  '存储各乘务计划的任务
    Public PreDayTimeTable As Coordination2.CSTimeTable                 '前一天乘务计划任务
    Public StartDate As DateTime                                        '编制开始时间
    Public EndDate As DateTime                                          '编制结束时间
    Public TotalDriverNum As Integer                                    '最小需使用的乘务员数量
    Public ActualDriverNum As Integer                                   '实际设置的乘务员数量
    Public Yunzhunpara As String                                        '运转制度参数
    Public DriverTeams As List(Of List(Of Coordination2.Driver))        '乘务员数组
    Public AvaDriverTeams As List(Of List(Of Coordination2.Driver))     '挑选出的当天的空闲司机
    Public MaxTiao As Integer                                           '调班人数，针对六班三转
    Public AMDutyCons As New List(Of AMDutyConnect)
    Public TeamList As New List(Of CrewTrainingManager.DriverTeam)      '所有参加轮转的司机
    Public AreaYunZhuanS As New List(Of AreaYunZhuan)
    Public Errstr As String            '错误字符串，存储错误信息
    Public DutySort() As String         '记录轮班顺序
    Public PreCell As DataGridViewCell = Nothing
    Public CurrentCell As DataGridViewCell = Nothing
    Public ModifyMode As Integer = 0                  '修改模式 0为正常 1为交换任务
    Public EditMode As Integer = 0            '编辑状态
    Public DGVDuties As List(Of DataGridView)
    Public lunzhuansets As List(Of Dictionary(Of Integer, String))   '轮转班制集合
    Public lunzhuanset As New Dictionary(Of Integer, String)         '轮转班制
    Public arealunzhuan As New Dictionary(Of String, Dictionary(Of Integer, String))   '区域与轮转班制对应关系

    Public Sub New()

        ' 此调用是 Windows 窗体设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        net = New Coordination2.Net()
        DriverTeams = New List(Of List(Of Coordination2.Driver))
        AvaDriverTeams = New List(Of List(Of Coordination2.Driver))
        
    End Sub
    Public Sub New(tpmCurLineName As String)

        ' 此调用是 Windows 窗体设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        net = New Coordination2.Net()
        For Each l As Coordination2.Line In net.Lines
            If l.Name = tpmCurLineName Then
                CurLine = l
                Exit For
            End If
        Next
        DriverTeams = New List(Of List(Of Coordination2.Driver))
        AvaDriverTeams = New List(Of List(Of Coordination2.Driver))

    End Sub

    Private Sub FrmClassCoordinate_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

       
        Select Case CurrentUserRole
            Case "高级管理员"

            Case "中心管理员"
                TSB_Save.Visible = False
                TSBSetting.Visible = False
                TSB_AssignFirstDayDuty.Visible = False
                TSBAssignDuty.Visible = False
                CMUChangeDuty.Items.Clear()
                CMUChangeDuty.Items.Add(任务详情DToolStripMenuItem)

            Case "车间运用管理"
            Case "车间管理员"
                TSB_Save.Visible = False
                TSBSetting.Visible = False
                TSB_AssignFirstDayDuty.Visible = False
                TSBAssignDuty.Visible = False
                CMUChangeDuty.Items.Clear()
                CMUChangeDuty.Items.Add(任务详情DToolStripMenuItem)

            Case Else
                MsgBox("当前用户为非法用户，请联系系统管理员!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                End
        End Select

        Me.TBStartTime.ReadOnly = True
        Me.TBEndTime.ReadOnly = True
        Me.TBLine.ReadOnly = True
        Me.TBPreTrain.ReadOnly = True
        Me.TBDriverNum.ReadOnly = True
        Me.TBYunzhuan.ReadOnly = True
        'RefreshPara()
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBSetting.Click
        Dim frm As New FrmDriverCoordination(net)

        If frm.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Me.CurLine = frm.CurLine
            Me.StartDate = frm.StartDate
            Me.EndDate = frm.EndDate
            Me.Yunzhunpara = frm.Yunzhuanpara
            Me.TBYunzhuan.Text = Me.Yunzhunpara
            Me.cstimetableNameList = frm.cstimetableNameList
            Me.TotalDriverNum = frm.TotalDriverNum
            Me.ActualDriverNum = frm.ActualDriverNum
            Me.MaxTiao = frm.MaxTiao
            Me.arealunzhuan = frm.arealunzhuan

            For Each Key As String In arealunzhuan.Keys
                ComboBox1.Items.Add(Key)
            Next

            Call Load_Drivers()
            Call LoadVacationfo()
            Call RefreshPara()
            AMDutyCons.Clear()
            Call ADutyAndMdutyConnect()          '加载各天任务，并进行夜早班匹配
            Call GetAreaInfo()
            Call LoadDriversTreeView()
            Me.EditMode = 1
        End If
    End Sub
    ''' <summary>
    ''' 加载区域司机及区域信息
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Load_Drivers()
        Dim Str As String = ""
        Dim tab As Data.DataTable
        '============加载司机信息
        Str = "Select * from cs_driverinf where LINEID='" & Me.CurLine.Name & "' and (beteam is not null and beteam<>0) and idleornot='可用' order by beclass,beteam,RDriverNo"  '加载司机信息
        tab = Globle.Method.ReadDataForAccess(Str)

        TeamList.Clear()
        For i As Integer = 0 To tab.Rows.Count - 1
            If i = 0 OrElse tab.Rows(i).Item("beteam").ToString <> tab.Rows(i - 1).Item("beteam").ToString Then
                Dim tempTeam As New CrewTrainingManager.DriverTeam(CurLine.Name, tab.Rows(i).Item("beclass").ToString, tab.Rows(i).Item("beteam").ToString)
                tempTeam.CoDrivers.Add(New Coordination2.Driver(tab.Rows(i).Item("beclass").ToString, tab.Rows(i).Item("beteam").ToString, tab.Rows(i).Item("RdriverNo").ToString, tab.Rows(i).Item("DriverName").ToString, CurLine.Name, tab.Rows(i).Item("BeZone").ToString, Me.StartDate.Date, Me.EndDate.Date))
                TeamList.Add(tempTeam)
            Else
                Dim tmpNum As Integer = TeamList.Count
                TeamList(tmpNum - 1).CoDrivers.Add(New Coordination2.Driver(tab.Rows(i).Item("beclass").ToString, tab.Rows(i).Item("beteam").ToString, tab.Rows(i).Item("RdriverNo").ToString, tab.Rows(i).Item("DriverName").ToString, CurLine.Name, tab.Rows(i).Item("BeZone").ToString, Me.StartDate.Date, Me.EndDate.Date))
            End If
        Next
        Call LoadPrePara()
    End Sub

    Public Sub LoadPrePara()
        '==============首先加载初始公里数
        Dim str As String = "select t.rdriverno,sum(t.drivedistance) as distance from " & _
                            "(select m.*,t.* from " & _
                            "(select t.cstimetableid,t.driverno,t.drivetime,t.drivedistance,t.startstaname " & _
                            "from (select distinct t.cstimetableid,t.driverno,t.dutysort,t.drivetime,t.drivedistance,s.startstaname,s.endstaname,s.starttime,s.endtime, " & _
                            "ROW_NUMBER() OVER(PARTITION BY t.cstimetableid,t.driverno ORDER BY iif(val(s.starttime)<10800 ,val(s.starttime)+86400 ,val(s.starttime)) as Indexid from CS_WORKLOAD t, CS_CrewSchedule s " & _
                            "where(t.CSTIMETABLEID = s.CSTIMETABLEID And s.DRIVERNO = t.DRIVERNO) " & _
                            "order by t.cstimetableid,t.driverno,t.driverno,Indexid) t where t.Indexid=1) t, " & _
                            "(select t.rdriverno,t.dateno,t.driverno,t.dutysort as chuban,m.cstimetableid from cs_corresponding t,cs_datetimetable m where t.dateno=m.dateno) m " & _
                            "where t.cstimetableid=m.cstimetableid and t.driverno=m.driverno) t " & _
                            "where datediff('d',t.dateno,Format('" & Me.StartDate.Date.AddMonths(-1).ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 and datediff('d',t.dateno,Format('" & Me.StartDate.Date.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>0 group by t.rdriverno order by t.rdriverno"
        Dim tab As Data.DataTable = Globle.Method.ReadDataForAccess(str)
        For Each row As DataRow In tab.Rows
            Dim rdriverno As String = row.Item("rdriverno").ToString
            For Each team As CrewTrainingManager.DriverTeam In TeamList
                For Each dri As Coordination2.Driver In team.CoDrivers
                    If dri.ID = rdriverno Then
                        dri.PreDistance = CDec(row.Item("distance"))
                        GoTo L
                    End If
                Next
            Next
L:
            Continue For
        Next

        '=================加载夜班从段场出的次数
        Dim wstr As String = "("
        If DepotPlaces.Count > 0 Then
            wstr &= "t.startstaname='" & DepotPlaces(0) & "'"
        End If
        If DepotPlaces.Count > 1 Then
            For i As Integer = 1 To DepotPlaces.Count - 1
                wstr &= " or t.startstaname='" & DepotPlaces(i) & "'"
            Next
        End If
        wstr &= ")"
        str = "select t.rdriverno,count(t.rdriverno) as count from " & _
                "(select m.*,t.* from " & _
                "(select t.cstimetableid,t.driverno,t.drivetime,t.drivedistance,t.startstaname " & _
                "from (select distinct t.cstimetableid,t.driverno,t.dutysort,t.drivetime,t.drivedistance,s.startstaname,s.endstaname,s.starttime,s.endtime, " & _
                "ROW_NUMBER() OVER(PARTITION BY t.cstimetableid,t.driverno ORDER BY iif(val(s.starttime)<10800 ,val(s.starttime)+86400 ,val(s.starttime)) as Indexid from CS_WORKLOAD t, CS_CrewSchedule s " & _
                "where(t.CSTIMETABLEID = s.CSTIMETABLEID And s.DRIVERNO = t.DRIVERNO) " & _
                "order by t.cstimetableid,t.driverno,t.driverno,Indexid) t where t.Indexid=1) t, " & _
                "(select t.rdriverno,t.dateno,t.driverno,t.dutysort,m.cstimetableid from cs_corresponding t,cs_datetimetable m where t.dateno=m.dateno) m " & _
                "where t.cstimetableid=m.cstimetableid and t.driverno=m.driverno order by m.rdriverno,m.dateno) t " & _
                "where datediff('d',t.dateno,Format('" & Me.StartDate.Date.AddDays(-7).ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 and datediff(t.dateno,Format('" & _
                Me.StartDate.Date.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>0 and " & wstr & " and t.dutysort='夜班' group by t.rdriverno order by t.rdriverno"
        tab = Globle.Method.ReadDataForAccess(str)
        For Each row As DataRow In tab.Rows
            Dim rdriverno As String = row.Item("rdriverno").ToString
            For Each team As CrewTrainingManager.DriverTeam In TeamList
                For Each dri As Coordination2.Driver In team.CoDrivers
                    If dri.ID = rdriverno Then
                        dri.NightOutFromDepotNum = CInt(row.Item("count"))
                        GoTo M
                    End If
                Next
            Next
M:
            Continue For
        Next

        '=========================加载出班次数
        str = "select t.rdriverno,count(t.rdriverno) as count from " & _
                "(select t.rdriverno,t.dateno,t.driverno,(case when t.dutysort=t.fordutysort then 'nochuban' else 'chuban' end) as chuban,t.cstimetableid from " & _
                "(select t.rdriverno,t.dateno,t.driverno,t.dutysort, " & _
                "(case when INSTR(t.fordutysort,'/')=0 then t.fordutysort else substr(t.fordutysort, 0, INSTR(t.fordutysort,'/')-1) end) as fordutysort, " & _
                "m.cstimetableid from cs_corresponding t,cs_datetimetable m " & _
                "where datediff('d',t.dateno,m.dateno)=0 order by t.rdriverno,t.dateno) t) t " & _
                "where datediff('d',t.dateno,Format('" & Me.StartDate.Date.AddDays(-20).ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 and datediff('d',t.dateno,Format('" & Me.StartDate.Date.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>0 and t.chuban='chuban' group by t.rdriverno order by t.rdriverno"
        tab = Globle.Method.ReadDataForAccess(str)
        For Each row As DataRow In tab.Rows
            Dim rdriverno As String = row.Item("rdriverno").ToString
            For Each team As CrewTrainingManager.DriverTeam In TeamList
                For Each dri As Coordination2.Driver In team.CoDrivers
                    If dri.ID = rdriverno Then
                        dri.DeadheadingNum = CInt(row.Item("count"))
                        GoTo N
                    End If
                Next
            Next
N:
            Continue For
        Next
    End Sub

    Public Sub LoadDriversTreeView()
        Dim tempclass As List(Of String) = New List(Of String)
        Dim tempteam As List(Of String) = New List(Of String)
        Dim tempdri As List(Of String) = New List(Of String)
        Me.TreeDrivers.Nodes.Clear()
        For Each area As AreaYunZhuan In AreaYunZhuanS
            'If area.YunZhuanPara = Me.TBYunzhuan.Text Then
            '    Continue For
            'End If
            tempclass = New Generic.List(Of String)
            tempteam = New Generic.List(Of String)
            tempdri = New Generic.List(Of String)
            Me.TreeDrivers.Nodes.Add(area.AreaName, area.AreaName)
            For Each team As CrewTrainingManager.DriverTeam In area.AvaDrivers
                For Each dri As Coordination2.Driver In team.CoDrivers
                    tempclass.Add(team.ClassName)
                    tempteam.Add(team.TeamNo)
                    tempdri.Add(dri.name)
                Next
            Next

            For i As Integer = 0 To tempdri.Count - 1
                If i = 0 Then
                    Me.TreeDrivers.Nodes(area.AreaName).Nodes.Add(tempclass(0), tempclass(0))
                    Me.TreeDrivers.Nodes(area.AreaName).Nodes(tempclass(0)).Nodes.Add(tempteam(0), tempteam(0))
                Else
                    If tempclass(i) <> tempclass(i - 1) Then
                        Me.TreeDrivers.Nodes(area.AreaName).Nodes.Add(tempclass(i), tempclass(i))
                    End If
                    If tempteam(i) <> tempteam(i - 1) Then
                        Me.TreeDrivers.Nodes(area.AreaName).Nodes(tempclass(i)).Nodes.Add(tempteam(i), tempteam(i))
                    End If
                End If
                Me.TreeDrivers.Nodes(area.AreaName).Nodes(tempclass(i)).Nodes(tempteam(i)).Nodes.Add(tempdri(i), tempdri(i))
            Next
        Next

    End Sub

    Public Sub RefreshPara()
        Me.TBStartTime.Text = Me.StartDate.Date.ToString("yyyy年MM月dd日")
        Me.TBEndTime.Text = Me.EndDate.Date.ToString("yyyy年MM月dd日")
        Me.TBLine.Text = Me.CurLine.Name
        Me.TBPreTrain.Text = Me.CurLine.PreparedTrainNum.ToString
        Me.TBDriverNum.Text = Me.ActualDriverNum
        Me.TBYunzhuan.Text = Me.Yunzhunpara
        Me.TBDriverNum.Text = TeamList.Count
        
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBAssignDuty.Click
        If cstimetableNameList Is Nothing OrElse cstimetableNameList.Count = 0 Then
            MsgBox("参数设置未完整！", MsgBoxStyle.Information, "提醒")
            Exit Sub
        End If

        Call AssignDutySort()
        Call LunZhuan()
        Call DrawDGVData()
        Call RefreshErrorInfo()
        Me.EditMode = 3
    End Sub

    '===================以下是新轮转制度代码

    Public Sub ADutyAndMdutyConnect()
        If Me.cstimetableNameList Is Nothing OrElse Me.cstimetableNameList.Length = 0 Then
            MsgBox("运行图运用计划未设置！", MsgBoxStyle.Information, "提醒")
        Else
            Dim pro As New FrmProgress(Me.cstimetableNameList.Length, "正在导入运行图任务...")
            ReDim CSTimeTables(Me.cstimetableNameList.Length - 1)      '首先加载乘务计划
            For i As Integer = 0 To CSTimeTables.Length - 1
                If Me.cstimetableNameList(i) <> "" OrElse Me.cstimetableNameList(i) IsNot Nothing Then
                    CSTimeTables(i) = New Coordination2.CSTimeTable(Me.CurLine.GetCSTimeTableFromName(Me.cstimetableNameList(i)).ID, Me.CurLine.Name)
                    If CSTimeTables(i).ACSDrivers IsNot Nothing Then
                        For Each csdri As Coordination2.CSDriver In CSTimeTables(i).ACSDrivers
                            If DepotPlaces.Contains(csdri.StartStaName) Then
                                csdri.IFNigthOutDeput = True
                            End If
                        Next
                    End If
                Else
                    MsgBox("乘务计划未设置完整！")
                    Exit Sub
                End If
                pro.Performstep()
            Next
            pro.Close()

            If UBound(CSTimeTables) >= 0 Then
                PreDayTimeTable = GetTimetableFromDate(Me.StartDate.AddDays(-1), CurLine.Name)
                If PreDayTimeTable Is Nothing Then
                    Call ADutyAndMdutyConnect(CSTimeTables(0), 0)         '未找到前一天计划，则不形成联合任务
                Else
                    Call ADutyAndMdutyConnect(PreDayTimeTable, CSTimeTables(0), 0)
                End If
                For i As Integer = 0 To UBound(CSTimeTables) - 1
                    Call ADutyAndMdutyConnect(CSTimeTables(i), CSTimeTables(i + 1), i + 1)
                Next

            End If

        End If
    End Sub

    Public Sub ADutyAndMdutyConnect(ByVal TimeTable1 As Coordination2.CSTimeTable, ByVal TimeTable2 As Coordination2.CSTimeTable, ByVal DayIndex As Integer)             '安排出入库随乘车
        Dim tempConnect As New AMDutyConnect(Me.StartDate.AddDays(DayIndex - 1), Me.StartDate.AddDays(DayIndex), TimeTable1.ID, TimeTable2.ID)
        '==============首先安排早班的出库随乘车
        Dim eindex As Integer = AMDutyCons.FindIndex(Function(value As AMDutyConnect)
                                                         Return value.ACSTimetableID = TimeTable1.ID AndAlso value.MCSTimetableID = TimeTable2.ID
                                                     End Function)
        If eindex >= 0 Then                  '如果已经匹配过，则直接跳出
            tempConnect.ADHDriverList = AMDutyCons(eindex).ADHDriverList
            tempConnect.MDHDriverList = AMDutyCons(eindex).MDHDriverList
            tempConnect.AMDrivers = AMDutyCons(eindex).AMDrivers
            tempConnect.NDrivers = AMDutyCons(eindex).NDrivers
            tempConnect.CDrivers = AMDutyCons(eindex).CDrivers
            AMDutyCons.Add(tempConnect)
            Exit Sub
        End If

        Call FromConnectDuty(TimeTable1, TimeTable2, tempConnect)          '形成夜班与早班连接任务
        AMDutyCons.Add(tempConnect)
    End Sub

    Public Sub ADutyAndMdutyConnect(ByVal TimeTable2 As Coordination2.CSTimeTable, ByVal DayIndex As Integer)             '未找到前一天的计划时，不形成前一天的夜早班联合任务
        Dim tempConnect As New AMDutyConnect(Me.StartDate.AddDays(DayIndex - 1), Me.StartDate.AddDays(DayIndex), "", TimeTable2.ID)

        If TimeTable2.MCSDrivers.Count > 0 Then
            For i As Integer = 0 To TimeTable2.MCSDrivers.Count - 1
                Dim tempMdriver As New AMDriver(TimeTable2.MCSDrivers(i), "早班")
                tempConnect.AMDrivers.Add(tempMdriver)
            Next
        End If
        For Each Ndriver As Coordination2.CSDriver In TimeTable2.NCSDrivers          '=============形成白班任务
            tempConnect.NDrivers.Add(Ndriver)
        Next
        For Each Cdriver As Coordination2.CSDriver In TimeTable2.CCSDrivers           '=============形成常班班任务
            tempConnect.CDrivers.Add(Cdriver)
        Next
        AMDutyCons.Add(tempConnect)
    End Sub

    Public Sub FromConnectDuty(ByVal TimeTable1 As Coordination2.CSTimeTable, ByVal TimeTable2 As Coordination2.CSTimeTable, ByVal AMCon As AMDutyConnect)
        AMCon.AMDrivers.Clear()
        AMCon.NDrivers.Clear()
        AMCon.CDrivers.Clear()

        Dim str As String = "select * from cs_amdrivercorrespond t where t.adrivertimetableid='" & TimeTable1.ID & "' and t.mdrivertimetableid='" & TimeTable2.ID & "'"
        Dim temtab As Data.DataTable = Globle.Method.ReadDataForAccess(str)


        If temtab IsNot Nothing AndAlso temtab.Rows.Count > 0 Then
            For Each row As DataRow In temtab.Rows
                Dim MorDriNo As String = row.Item("MDriverno").ToString
                Dim NigDriNo As String = row.Item("ADriverno").ToString
                Dim MDriver As Coordination2.CSDriver = TimeTable2.MCSDrivers.Find(Function(value As Coordination2.CSDriver)
                                                                                       Return (value.CSdriverNo = MorDriNo AndAlso value.DutySort = "早班")
                                                                                   End Function)
                Dim ADriver As Coordination2.CSDriver = TimeTable1.ACSDrivers.Find(Function(value As Coordination2.CSDriver)
                                                                                       Return (value.CSdriverNo = NigDriNo AndAlso value.DutySort = "夜班")
                                                                                   End Function)
                If MDriver IsNot Nothing AndAlso ADriver IsNot Nothing Then
                    Dim tempAMDriver As New AMDriver(ADriver, MDriver)
                    AMCon.AMDrivers.Add(tempAMDriver)
                ElseIf MDriver IsNot Nothing AndAlso ADriver Is Nothing Then
                    Dim temCSDriver As New Coordination2.CSDriver()
                    temCSDriver.startdutytime = 17 * 3600
                    temCSDriver.endtime = 21 * 3600
                    temCSDriver.TotalDayDriveTime = 4 * 3600
                    temCSDriver.TotalDayWorkTime = 4 * 3600
                    temCSDriver.DutySort = "夜班"
                    temCSDriver.CSdriverNo = "SP"
                    temCSDriver.DriveDistance = 0
                    Dim tempAMDriver As New AMDriver(temCSDriver, MDriver)
                    AMCon.AMDrivers.Add(tempAMDriver)
                ElseIf MDriver Is Nothing AndAlso ADriver IsNot Nothing Then
                    Dim temCSDriver As New Coordination2.CSDriver()
                    temCSDriver.startdutytime = 5 * 3600
                    temCSDriver.endtime = 9 * 3600
                    temCSDriver.TotalDayDriveTime = 4 * 3600
                    temCSDriver.TotalDayWorkTime = 4 * 3600
                    temCSDriver.DutySort = "早班"
                    temCSDriver.CSdriverNo = "SP"
                    temCSDriver.DriveDistance = 0
                    Dim tempAMDriver As New AMDriver(ADriver, temCSDriver)
                    AMCon.AMDrivers.Add(tempAMDriver)
                End If
            Next
        End If

        For Each Ndriver As Coordination2.CSDriver In TimeTable2.NCSDrivers          '=============形成白班任务
            AMCon.NDrivers.Add(Ndriver)
        Next
        For Each Cdriver As Coordination2.CSDriver In TimeTable2.CCSDrivers           '=============形成常班班任务
            AMCon.CDrivers.Add(Cdriver)
        Next

    End Sub

    Public Sub AssignDutySort()            '安排班种'
        Dim decidemode As Integer = 0
        For Each area As AreaYunZhuan In AreaYunZhuanS
            If area.AreaName = "主区域" Or arealunzhuan.Keys.Contains(area.AreaName) = False Then      '没有定制轮转匹配的区域按主区域处理
                decidemode = 1
            Else
                decidemode = 2
            End If
            Select Case decidemode
                Case 1
                    Select Case area.YunZhuanPara
                        Case "四班两转"
                            ReDim DutySort(3)
                            DutySort(0) = "白班"
                            DutySort(1) = "夜班"
                            DutySort(2) = "早班"
                            DutySort(3) = "休息"
                            Call AssignDutySort(DutySort, area.AvaDrivers)
                            'Case "四班两转+日勤班"
                            '    ReDim DutySort(3)
                            '    DutySort(0) = "白班"
                            '    DutySort(1) = "夜班"
                            '    DutySort(2) = "早班"
                            '    DutySort(3) = "休息"
                            '    Call AssignDutySort(DutySort, area.AvaDrivers)

                        Case "五班三转"
                            ReDim DutySort(4)
                            DutySort(0) = "白班"
                            DutySort(1) = "夜班"
                            DutySort(2) = "早班"
                            DutySort(3) = "日勤班"
                            DutySort(4) = "休息"
                            Call AssignDutySort(DutySort, area.AvaDrivers)
                            'Case "日勤班轮转"
                            '    ReDim DutySort(lunzhuanset.Keys.Count - 1)
                            '    Dim z, x As Integer
                            '    Dim y As String  '在序号前加0
                            '    z = 1  '值班次数
                            '    x = 1  '休息次数
                            '    For i As Integer = 0 To lunzhuanset.Keys.Count - 1
                            '        If lunzhuanset((i + 1).ToString) = "值班" Then
                            '            If z < 10 Then
                            '                y = "0" + z.ToString
                            '            Else
                            '                y = z.ToString
                            '            End If
                            '            DutySort(i) = "日勤班" + y
                            '            z += 1
                            '        Else
                            '            DutySort(i) = "休息-" + x.ToString
                            '            x += 1
                            '        End If

                            '    Next
                    End Select
                Case 2
                    If arealunzhuan.Keys.Contains(area.AreaName) Then

                        ReDim DutySort(arealunzhuan(area.AreaName).Count - 1)
                        Dim z, x As Integer
                        Dim y As String  '在序号前加0
                        z = 1  '值班次数
                        x = 1  '休息次数
                        For i As Integer = 0 To arealunzhuan(area.AreaName).Keys.Count - 1
                            DutySort(i) = arealunzhuan(area.AreaName)(i + 1)
                            'If arealunzhuan(area.AreaName)(i + 1).ToString = "值班" Then
                            '    If z < 10 Then
                            '        y = "0" + z.ToString
                            '    Else
                            '        y = z.ToString
                            '    End If
                            '    DutySort(i) = "日勤班" + y
                            '    z += 1
                            'Else
                            '    DutySort(i) = "休息-" + x.ToString
                            '    x += 1
                            'End If
                        Next
                    End If
                    Call AssignDutySort(DutySort, area.AvaDrivers)
            End Select

        Next
    End Sub

    Public Sub AssignDutySort(ByVal DutySort As String(), ByVal Teams As List(Of CrewTrainingManager.DriverTeam))           '==========安排第一天班种
        '===============================安排后面所有班种
        If Me.EditMode = 1 Then
            '==========首先安排首日任务
            Dim Point As Integer = 0
            For Each team As CrewTrainingManager.DriverTeam In Teams
                For Each dri As Coordination2.Driver In team.CoDrivers
                    dri.DriverDayJobs(0).ForDutySort = DutySort(Point)
                    dri.DriverDayJobs(0).DutySort = dri.DriverDayJobs(0).ForDutySort.Split("/")(0)
                    If dri.DriverDayJobs(0).DutySort.Contains("休息") Then '"休息-1""休息-2"
                        dri.DriverDayJobs(0).DutySort = "休息"
                    ElseIf dri.DriverDayJobs(0).DutySort.Contains("值班") Then
                        dri.DriverDayJobs(0).DutySort = "日勤班"
                    End If
                Next
                Point += 1
                Point = Point Mod DutySort.Length
            Next
            For Each team As CrewTrainingManager.DriverTeam In Teams
                For Each dri As Coordination2.Driver In team.CoDrivers
                    Dim PreDutySort As String = dri.DriverDayJobs(0).ForDutySort
                    For i As Integer = 1 To dri.DriverDayJobs.Count - 1
                        Dim temdutysort As String = DutySort(IIf(Array.IndexOf(DutySort, PreDutySort) < UBound(DutySort), Array.IndexOf(DutySort, PreDutySort) + 1, 0))
                        dri.DriverDayJobs(i).ForDutySort = temdutysort
                        dri.DriverDayJobs(i).DutySort = temdutysort.Split("/")(0)
                        If dri.DriverDayJobs(i).DutySort.Contains("休息") Then '"休息-1""休息-2"
                            dri.DriverDayJobs(i).DutySort = "休息"
                        ElseIf dri.DriverDayJobs(i).DutySort.Contains("值班") Then
                            dri.DriverDayJobs(i).DutySort = "日勤班"
                        End If
                        PreDutySort = temdutysort
                    Next
                Next
            Next
        ElseIf Me.EditMode = 2 Then
            For Each team As CrewTrainingManager.DriverTeam In Teams
                For Each dri As Coordination2.Driver In team.CoDrivers
                    Dim PreDutySort As String = dri.DriverDayJobs(0).ForDutySort
                    For i As Integer = 1 To dri.DriverDayJobs.Count - 1
                        Dim temp1 As Integer = Array.IndexOf(DutySort, PreDutySort)
                        Dim temp2 As Integer = UBound(DutySort)
                        Dim temdutysort As String = DutySort(IIf(Array.IndexOf(DutySort, PreDutySort) < UBound(DutySort), Array.IndexOf(DutySort, PreDutySort) + 1, 0))
                        dri.DriverDayJobs(i).ForDutySort = temdutysort
                        dri.DriverDayJobs(i).DutySort = temdutysort.Split("/")(0)
                        If dri.DriverDayJobs(i).DutySort.Contains("休息") Then '"休息-1""休息-2"
                            dri.DriverDayJobs(i).DutySort = "休息"
                        ElseIf dri.DriverDayJobs(i).DutySort.Contains("值班") Then
                            dri.DriverDayJobs(i).DutySort = "日勤班"
                        End If
                        PreDutySort = temdutysort
                    Next
                Next
            Next
        End If

    End Sub

    Public Sub LunZhuan()
        '==========先安排年假
        For Each team As CrewTrainingManager.DriverTeam In TeamList
            If team.IFVacation Then
                For Each _vac As CrewTrainingManager.TypeVac In team.VocDates
                    Dim temdate As Date = _vac.vDate
                    Dim VacCSDri As New Coordination2.CSDriver
                    VacCSDri.startdutytime = 10 * 3600
                    VacCSDri.endtime = 18 * 3600
                    VacCSDri.TotalDayDriveTime = 8 * 3600
                    VacCSDri.TotalDayWorkTime = 8 * 3600
                    VacCSDri.DutySort = _vac.vType
                    VacCSDri.CSdriverNo = "无任务"
                    For Each dri As Coordination2.Driver In team.CoDrivers
                        AddCSDriver(dri, VacCSDri, temdate)
                    Next
                Next
            End If
        Next

        Dim pro As New FrmProgress(AreaYunZhuanS.Count * cstimetableNameList.Count, "正在进行任务轮转...")
        For Each area As AreaYunZhuan In AreaYunZhuanS
            For i As Integer = 0 To area.AmCons.Count - 1                  '一天一天的轮转
                Dim MDrivers As New List(Of Coordination2.CSDriver)       '早班
                Dim NDrivers As New List(Of Coordination2.CSDriver)       '白班
                Dim ADrivers As New List(Of Coordination2.CSDriver)       '
                Dim CDrivers As New List(Of Coordination2.CSDriver)
                For Each AMDri As AMDriver In area.AmCons(i).AMDrivers
                    If AMDri.MDriver IsNot Nothing Then
                        MDrivers.Add(AMDri.MDriver)
                    End If
                Next
                NDrivers = area.AmCons(i).NDrivers
                CDrivers = area.AmCons(i).CDrivers
                If i < area.AmCons.Count - 1 Then
                    For Each AMDri As AMDriver In area.AmCons(i + 1).AMDrivers
                        If AMDri.ADriver IsNot Nothing Then
                            ADrivers.Add(AMDri.ADriver)
                        End If
                    Next
                Else
                    For Each dri As Coordination2.CSDriver In CSTimeTables(CSTimeTables.Count - 1).ACSDrivers
                        'If dri.BelongArea = area.AreaName Then
                        ADrivers.Add(dri)
                        'End If
                    Next
                End If

                AssignDuty(area.AvaDrivers, MDrivers, NDrivers, ADrivers, CDrivers, Me.StartDate.AddDays(i))          '根据往日工作量分配当天任务
                pro.Performstep()
            Next
        Next
        pro.Close()
    End Sub

    Public Sub AssignDuty(ByVal Teams As List(Of CrewTrainingManager.DriverTeam), ByVal Mdrivers As List(Of Coordination2.CSDriver), ByVal Ndrivers As List(Of Coordination2.CSDriver), ByVal Adrivers As List(Of Coordination2.CSDriver), _
                          ByVal Cdrivers As List(Of Coordination2.CSDriver), ByVal _date As Date)
        '===================将任务排序
        Call SortByWorkTime(Mdrivers, True)         '将任务均按照工作量倒排序
        Call SortByWorkTime(Ndrivers, True)
        Call SortByWorkTime(Adrivers, True)
        Call SortByWorkTime(Cdrivers, True)
        Call SortByWorkTime(Teams)                     '将司机按照已完成工作量正排序
        For Each team As CrewTrainingManager.DriverTeam In Teams     '===设置当前时间，便于判断worktimes
            For Each dri As Coordination2.Driver In team.CoDrivers
                dri.CulTime = _date.Date
            Next
        Next

        Call CheckCSDrivers(Mdrivers, _date)             '================检查部分任务是否已被指定(如首日任务，或由于夜早班联合任务)
        Call CheckCSDrivers(Ndrivers, _date)
        Call CheckCSDrivers(Adrivers, _date)
        Call CheckCSDrivers(Cdrivers, _date)

        AssignDuty(Teams, Mdrivers, _date)               '============找优先满足班种的司机先接任务
        AssignDuty(Teams, Ndrivers, _date)

        Dim OutDepotAdris As New List(Of Coordination2.CSDriver)            '===============先安排夜班段场出的任务
        For Each dri As Coordination2.CSDriver In Adrivers
            If dri.IFNigthOutDeput Then
                OutDepotAdris.Add(dri)
            End If
        Next
        Call SortByWorkTime(Teams)
        Call SortByOutDepotNum(Teams)
        AssignDuty(Teams, OutDepotAdris, _date)

        Call SortByWorkTime(Teams)
        AssignDuty(Teams, Adrivers, _date)
        AssignDuty(Teams, Cdrivers, _date)
        'AssignDuty(Teams, Mdrivers, _date, True)         '=========将最后未分配的司机以当前休息的司机进行顶替
        Call SortByWorkTime(Teams)
        Call SortByChubanNum(Teams)
        AssignDuty(Teams, Ndrivers, _date, 2)
        AssignDuty(Teams, Cdrivers, _date, 2)

        Call SortByWorkTime(Teams)
        Call SortByChubanNum(Teams)
        AssignDuty(Teams, Ndrivers, _date, 3)
        AssignDuty(Teams, Cdrivers, _date, 3)

        For Each team As CrewTrainingManager.DriverTeam In Teams            '最后将没有任务的司机设置为备班
            For Each dri As Coordination2.Driver In team.CoDrivers
                Dim temJob As Coordination2.DriverDayJob = dri.DriverDayJobs.Find(Function(value As Coordination2.DriverDayJob)
                                                                                      Return value.Date.Date = _date.Date
                                                                                  End Function)
                If temJob.CSDriverNo = "" AndAlso temJob.DutySort <> "" Then
                    Dim tempDri As New Coordination2.CSDriver()
                    'Select Case temJob.DutySort
                    '    Case "早班"
                    '        tempDri.startdutytime = 5 * 3600
                    '        tempDri.endtime = 9 * 3600
                    '        tempDri.TotalDayDriveTime = 4 * 3600
                    '        tempDri.TotalDayWorkTime = 4 * 3600
                    '        tempDri.DutySort = temJob.DutySort
                    '        tempDri.CSdriverNo = "SP"
                    '        tempDri.OutPutCSDriverNo = "SP"
                    '    Case "白班"
                    '        tempDri.startdutytime = 9 * 3600
                    '        tempDri.endtime = 17 * 3600
                    '        tempDri.TotalDayDriveTime = 8 * 3600
                    '        tempDri.TotalDayWorkTime = 8 * 3600
                    '        tempDri.DutySort = temJob.DutySort
                    '        tempDri.CSdriverNo = "SP"
                    '        tempDri.OutPutCSDriverNo = "SP"
                    '    Case "日勤班"
                    '        tempDri.startdutytime = 9 * 3600
                    '        tempDri.endtime = 17 * 3600
                    '        tempDri.TotalDayDriveTime = 8 * 3600
                    '        tempDri.TotalDayWorkTime = 8 * 3600
                    '        tempDri.DutySort = temJob.DutySort
                    '        tempDri.CSdriverNo = "SP"
                    '        tempDri.OutPutCSDriverNo = "SP"
                    '    Case "夜班"
                    '        tempDri.startdutytime = 17 * 3600
                    '        tempDri.endtime = 21 * 3600
                    '        tempDri.TotalDayDriveTime = 4 * 3600
                    '        tempDri.TotalDayWorkTime = 4 * 3600
                    '        tempDri.DutySort = temJob.DutySort
                    '        tempDri.CSdriverNo = "SP"
                    '        tempDri.OutPutCSDriverNo = "SP"
                    '    Case Else
                    '        tempDri.DutySort = temJob.DutySort
                    '        tempDri.CSdriverNo = "无任务"
                    '        tempDri.OutPutCSDriverNo = "无任务"
                    'End Select
                    Select Case temJob.DutySort
                        Case "早班"
                            tempDri.startdutytime = 5 * 3600
                            tempDri.endtime = 9 * 3600
                            tempDri.TotalDayDriveTime = 4 * 3600
                            tempDri.TotalDayWorkTime = 4 * 3600
                            tempDri.DutySort = temJob.DutySort
                            tempDri.CSdriverNo = "SP"
                            tempDri.OutPutCSDriverNo = "SP"
                        Case "白班"
                            tempDri.startdutytime = 9 * 3600
                            tempDri.endtime = 17 * 3600
                            tempDri.TotalDayDriveTime = 8 * 3600
                            tempDri.TotalDayWorkTime = 8 * 3600
                            tempDri.DutySort = temJob.DutySort
                            tempDri.CSdriverNo = "SP"
                            tempDri.OutPutCSDriverNo = "SP"
                        Case "日勤班"
                            'tempDri.startdutytime = 9 * 3600
                            'tempDri.endtime = 17 * 3600
                            'tempDri.TotalDayDriveTime = 8 * 3600
                            'tempDri.TotalDayWorkTime = 8 * 3600
                            tempDri.DutySort = "休息"
                            tempDri.CSdriverNo = "无任务"
                            tempDri.OutPutCSDriverNo = "无任务"
                        Case "夜班"
                            tempDri.startdutytime = 17 * 3600
                            tempDri.endtime = 21 * 3600
                            tempDri.TotalDayDriveTime = 4 * 3600
                            tempDri.TotalDayWorkTime = 4 * 3600
                            tempDri.DutySort = temJob.DutySort
                            tempDri.CSdriverNo = "SP"
                            tempDri.OutPutCSDriverNo = "SP"
                        Case Else
                            tempDri.DutySort = temJob.DutySort
                            tempDri.CSdriverNo = "无任务"
                            tempDri.OutPutCSDriverNo = "无任务"
                    End Select
                    tempDri.DriveDistance = 0
                    AddCSDriver(dri, tempDri, _date.Date)
                End If
            Next
        Next
    End Sub

    Public Sub AssignDuty(ByVal Teams As List(Of CrewTrainingManager.DriverTeam), ByVal duties As List(Of Coordination2.CSDriver), ByVal _date As Date, Optional freelevel As Integer = 1)
        Dim dutynum As Integer = duties.Count   '任务总数
        Dim drivernum As Integer = Teams.Count
        Dim morexiuxi As Integer
        morexiuxi = drivernum - dutynum   '应该有的休息数
        Dim xx As Integer = 0
        For Each team As CrewTrainingManager.DriverTeam In Teams            '将当天休息最少的需值班司机设置为多余的休息
            For Each dri As Coordination2.Driver In team.CoDrivers
                Dim temJob As Coordination2.DriverDayJob = dri.DriverDayJobs.Find(Function(value As Coordination2.DriverDayJob)
                                                                                      Return value.Date.Date = _date.Date
                                                                                  End Function)
                If temJob.DutySort = "休息" Then
                    xx += 1   '今天有的休息数
                    Exit For
                End If
            Next
        Next
        morexiuxi = morexiuxi - xx
        If morexiuxi > 0 And Teams(0).ClassName = "日勤班" And duties.Count > 0 Then

            Call SortBytotalxiuxi(Teams)
            Dim i As Integer = 0
            For i = 0 To morexiuxi
                For Each team As CrewTrainingManager.DriverTeam In Teams            '将当天休息最少的需值班司机设置为多余的休息
                    If i < morexiuxi Then
                        For Each dri As Coordination2.Driver In team.CoDrivers
                            Dim temJob As Coordination2.DriverDayJob = dri.DriverDayJobs.Find(Function(value As Coordination2.DriverDayJob)
                                                                                                  Return value.Date.Date = _date.Date
                                                                                              End Function)
                            Dim nextemJob As Coordination2.DriverDayJob = dri.DriverDayJobs.Find(Function(value As Coordination2.DriverDayJob)
                                                                                                     Return value.Date.Date = _date.Date.AddDays(1)
                                                                                                 End Function)
                            Dim pretemJob As Coordination2.DriverDayJob = dri.DriverDayJobs.Find(Function(value As Coordination2.DriverDayJob)
                                                                                                     Return value.Date.Date = _date.Date.AddDays(-1)
                                                                                                 End Function)
                            If i < morexiuxi And temJob.CSDriverNo = "" AndAlso temJob.DutySort <> "休息" AndAlso (nextemJob Is Nothing OrElse nextemJob.DutySort <> "休息") _
                                AndAlso (pretemJob Is Nothing OrElse pretemJob.DutySort <> "休息") Then
                                Dim tempDri As New Coordination2.CSDriver()
                                Select Case temJob.DutySort
                                    Case "日勤班"
                                        tempDri.DutySort = "休息"
                                        tempDri.CSdriverNo = "无任务"
                                        tempDri.OutPutCSDriverNo = "无任务"
                                    Case Else
                                        tempDri.DutySort = temJob.DutySort
                                        tempDri.CSdriverNo = "无任务"
                                        tempDri.OutPutCSDriverNo = "无任务"
                                End Select
                                tempDri.DriveDistance = 0
                                AddCSDriver(dri, tempDri, _date.Date)
                                i += 1
                                Exit For
                            End If
                        Next
                    Else
                        Exit For
                    End If

                Next
            Next
        End If
        Call SortByWorkTime(Teams)
        For Each Duty As Coordination2.CSDriver In duties

            If Duty.FlagDinner = False Then
                Dim selectTeam As CrewTrainingManager.DriverTeam = Nothing
                For Each team As CrewTrainingManager.DriverTeam In Teams             '找优先满足班种的司机先接任务
                    Dim ToJob As Coordination2.DriverDayJob = team.CoDrivers(0).DriverDayJobs.Find(Function(value As Coordination2.DriverDayJob)
                                                                                                       Return value.Date.Date = _date.Date
                                                                                                   End Function)
                    Dim restdaynum = team.CoDrivers(0).DriverDayJobs.FindAll(Function(value As Coordination2.DriverDayJob)
                                                                                 Return value.CSDriverNo.Contains("无任务") And DateDiff("d", value.Date, _date.Date) < dutynum
                                                                             End Function).Count    '当天之前的（任务数）天内有几个休息
                    Dim Noreplic As Integer = restdaynum + dutynum   '不重复的真正天数，包含了休息
                    Dim ToJobexist As Coordination2.DriverDayJob = team.CoDrivers(0).DriverDayJobs.Find(Function(value As Coordination2.DriverDayJob)
                                                                                                            Return value.CSDriverNo = Duty.CSdriverNo And DateDiff("d", value.Date, _date.Date) < Noreplic
                                                                                                        End Function) '查看以前任务数天内该司机有没有执行过该任务
                    Select Case freelevel
                        Case 1     '全约束需满足
                            If ToJobexist Is Nothing Then
                                If ToJob.DutySort = Duty.DutySort Then              '找对应班种的司机  Or (ToJob.DutySort.Contains("日勤班") And Duty.DutySort = "日勤班") 
                                    For Each Dri As Coordination2.Driver In team.CoDrivers
                                        If CanTaketheDuty(Dri, Duty, _date) Then
                                            selectTeam = team
                                        End If
                                    Next
                                    If selectTeam IsNot Nothing Then
                                        Exit For
                                    End If
                                End If
                            End If
                        Case 2    '不用满足不重复条件情况下
                            If ToJob.DutySort = Duty.DutySort Then              '找对应班种的司机
                                For Each Dri As Coordination2.Driver In team.CoDrivers
                                    If CanTaketheDuty(Dri, Duty, _date) Then
                                        selectTeam = team
                                    End If
                                Next
                                If selectTeam IsNot Nothing Then
                                    Exit For
                                End If
                            End If
                        Case 3     '不能找到相应班种的司机则找无任务的司机接班情况下
                            If ToJob.DutySort = "休息" Then
                                For Each Dri As Coordination2.Driver In team.CoDrivers
                                    If CanTaketheDuty(Dri, Duty, _date) Then
                                        selectTeam = team
                                    End If
                                Next
                            End If
                            If selectTeam IsNot Nothing Then
                                Exit For
                            End If
                    End Select
                Next
                If selectTeam IsNot Nothing Then
                    For Each Dri As Coordination2.Driver In selectTeam.CoDrivers                  '对于夜班司机，则直接分配夜早联合班
                        If Duty.DutySort = "夜班" Then
                            Dim driverno As String = Duty.CSdriverNo
                            Dim tempAmcon As AMDutyConnect = AMDutyCons.Find(Function(value As AMDutyConnect)
                                                                                 Return value.ADate.Date = _date.Date
                                                                             End Function)
                            If tempAmcon IsNot Nothing Then                       '最后一天无法找到联合任务
                                Dim temAmdri As AMDriver = tempAmcon.AMDrivers.Find(Function(value As AMDriver)
                                                                                        Return value.ADriver.CSdriverNo = driverno
                                                                                    End Function)
                                AddCSDriver(Dri, temAmdri, _date)
                            Else
                                AddCSDriver(Dri, Duty, _date)
                            End If
                            Dri.DeadheadingNum += 1
                            If Duty.IFNigthOutDeput Then
                                Dri.NightOutFromDepotNum += 1
                            End If
                        Else
                            AddCSDriver(Dri, Duty, _date)
                            Dri.DeadheadingNum += 1
                        End If
                    Next
                End If
            End If
        Next
        '判断


    End Sub

    Public Sub AssignCDuty(ByVal Teams As List(Of CrewTrainingManager.DriverTeam), ByVal duties As List(Of Coordination2.CSDriver), ByVal _date As Date)       '========分配常班
        Call SortByWorkTimes(Teams)                    '按司机的工作次数正排序
        For Each Cdri As Coordination2.CSDriver In duties
            If Cdri.FlagDinner = False Then
                Dim selectTeam As CrewTrainingManager.DriverTeam = Nothing
                For Each team As CrewTrainingManager.DriverTeam In Teams
                    For Each dri As Coordination2.Driver In team.CoDrivers
                        If CanTaketheCDuty(dri, Cdri, _date) Then
                            selectTeam = team
                        End If
                    Next
                    If selectTeam IsNot Nothing Then
                        Exit For
                    End If
                Next
                If selectTeam IsNot Nothing Then
                    For Each dri As Coordination2.Driver In selectTeam.CoDrivers
                        AddCSDriver(dri, Cdri, _date)
                    Next
                End If
            End If
        Next
    End Sub

    Public Sub CheckCSDrivers(ByVal CSDrivers As List(Of Coordination2.CSDriver), ByVal _date As Date)           '========检查任务是否已分配
        For Each Cdri As Coordination2.CSDriver In CSDrivers
            Cdri.FlagDinner = False
            For Each team As CrewTrainingManager.DriverTeam In TeamList
                For Each dri As Coordination2.Driver In team.CoDrivers
                    Dim temJob As Coordination2.DriverDayJob = dri.DriverDayJobs.Find(Function(value As Coordination2.DriverDayJob)
                                                                                          Return value.Date.Date = _date.Date
                                                                                      End Function)
                    If temJob.DutySort = Cdri.DutySort AndAlso temJob.CSDriverNo = Cdri.CSdriverNo Then
                        Cdri.FlagDinner = True
                    End If
                Next
            Next
        Next
    End Sub

    Public Function CheckUnAssignCSDrivers(ByVal CSDrivers As List(Of Coordination2.CSDriver), ByVal _date As Date) As String
        CheckUnAssignCSDrivers = ""
        For Each Cdri As Coordination2.CSDriver In CSDrivers
            Cdri.FlagDinner = False
            For Each team As CrewTrainingManager.DriverTeam In TeamList
                For Each dri As Coordination2.Driver In team.CoDrivers
                    Dim temJob As Coordination2.DriverDayJob = dri.DriverDayJobs.Find(Function(value As Coordination2.DriverDayJob)
                                                                                          Return value.Date.Date = _date.Date
                                                                                      End Function)
                    If temJob.DutySort = Cdri.DutySort AndAlso temJob.CSDriverNo = Cdri.CSdriverNo Then
                        Cdri.FlagDinner = True
                    End If
                Next
            Next
            If Cdri.FlagDinner = False Then
                CheckUnAssignCSDrivers &= Cdri.DutySort & "(" & Cdri.OutPutCSDriverNo & "),"
            End If
        Next
    End Function

    Public Sub DrawDGVData()
        Me.TabControlMain.TabPages.Clear()
        Me.DGVDuties = New List(Of DataGridView)
        For Each area As AreaYunZhuan In AreaYunZhuanS
            Call SortByID(area.AvaDrivers)
            Dim ShowDGV As New DataGridView
            Dim TempTab As New TabPage
            TempTab.Controls.Add(ShowDGV)
            Me.DGVDuties.Add(ShowDGV)
            TempTab.Text = area.AreaName
            ShowDGV.Dock = DockStyle.Fill
            ShowDGV.ReadOnly = True
            ShowDGV.AllowUserToAddRows = False
            ShowDGV.AllowUserToDeleteRows = False
            ShowDGV.Cursor = Cursors.Hand
            AddHandler ShowDGV.CellDoubleClick, AddressOf DutyGridView_CellDoubleClick
            AddHandler ShowDGV.CellClick, AddressOf DutyGridView_CellClick
            AddHandler ShowDGV.KeyDown, AddressOf DutyDGV_KeyDown
            ShowDGV.ContextMenuStrip = CMUChangeDuty
            TabControlMain.TabPages.Add(TempTab)
            ShowDGV.Rows.Clear()
            ShowDGV.Columns.Clear()
            ShowDGV.Columns.Add("编号", "编号")
            ShowDGV.Columns.Add("班组", "班组")
            ShowDGV.Columns.Add("组号", "组号")
            ShowDGV.Columns.Add("姓名", "姓名")
            ShowDGV.Columns.Add("总工时", "总工时||公里")
            ShowDGV.Columns.Add("峰班", "总数||早峰||休息")
            ShowDGV.Columns("编号").Frozen = True
            ShowDGV.Columns("编号").DefaultCellStyle.BackColor = Color.LightGreen
            ShowDGV.Columns("编号").Width = 55
            ShowDGV.Columns("班组").Frozen = True
            ShowDGV.Columns("班组").DefaultCellStyle.BackColor = Color.LightGreen
            ShowDGV.Columns("班组").Width = 80
            ShowDGV.Columns("组号").Frozen = True
            ShowDGV.Columns("组号").DefaultCellStyle.BackColor = Color.LightGreen
            ShowDGV.Columns("组号").Width = 55
            ShowDGV.Columns("姓名").Frozen = True
            ShowDGV.Columns("姓名").DefaultCellStyle.BackColor = Color.LightGreen
            ShowDGV.Columns("姓名").Width = 85
            ShowDGV.Columns("总工时").Frozen = True
            ShowDGV.Columns("总工时").DefaultCellStyle.BackColor = Color.LightBlue
            ShowDGV.Columns("总工时").Width = 105
            ShowDGV.Columns("峰班").Frozen = True
            ShowDGV.Columns("峰班").DefaultCellStyle.BackColor = Color.LightBlue
            ShowDGV.Columns("峰班").Width = 120
            For i As Integer = 0 To Me.cstimetableNameList.Length - 1
                ShowDGV.Columns.Add(Me.StartDate.AddDays(i).ToString("yyyy/MM/dd"), Me.StartDate.AddDays(i).ToString("yyyy/MM/dd") & " " & GlobalFunc.GetWeekDayString(Me.StartDate.AddDays(i)))
                If area.YunZhuanPara = "日勤班轮转" Then
                    ShowDGV.Columns(Me.StartDate.AddDays(i).ToString("yyyy/MM/dd")).Width = 200
                Else
                    ShowDGV.Columns(Me.StartDate.AddDays(i).ToString("yyyy/MM/dd")).Width = 170
                End If
            Next

            For Each team As CrewTrainingManager.DriverTeam In area.AvaDrivers
                For Each driver As Coordination2.Driver In team.CoDrivers
                    ShowDGV.Rows.Add(ShowDGV.Rows.Count + 1, team.ClassName, team.TeamNo, driver.name)
                    For j As Integer = 0 To driver.DriverDayJobs.Count - 1
                        If driver.DriverDayJobs(j).DutySort <> "" AndAlso driver.DriverDayJobs(j).CSDriverNo <> "" Then
                            ShowDGV.Rows(ShowDGV.Rows.Count - 1).Cells(driver.DriverDayJobs(j).Date.ToString("yyyy/MM/dd")).Value = _
                                                                                    driver.DriverDayJobs(j).DutySort & "/" & driver.DriverDayJobs(j).OutPutCSDriverNO & "||" & driver.DriverDayJobs(j).WorkTimeHour.ToString("0.00") & "||" & driver.DriverDayJobs(j).DriveDistance.ToString("0.00")
                        End If
                    Next
                    ShowDGV.Rows(ShowDGV.Rows.Count - 1).Cells("总工时").Value = driver.WorkTimeHour.ToString("0.00") & "||" & driver.DriveDistance.ToString("0.00")
                    'ShowDGV.Rows(ShowDGV.Rows.Count - 1).Cells("峰班").Value = driver.FengBanNum & "||" & driver.ZaoFengBanNum & "||" & driver.XiuxiFengBanNum

                    Dim i As Integer = driver.TotalXiuxiDayNum
                    ShowDGV.Rows(ShowDGV.Rows.Count - 1).Cells("峰班").Value = driver.FengBanNum & "||" & driver.ZaoFengBanNum & "||" & driver.TotalXiuxiDayNum
                Next
            Next
            Me.RefreshColor(ShowDGV)

        Next
    End Sub

    Public Sub RefreshErrorInfo()
        Me.DGVErrorInfo.Rows.Clear()
        Dim frmpro As New FrmProgress(AMDutyCons.Count, "正在更新任务分配信息,请稍后...")
        For i As Integer = 0 To AMDutyCons.Count - 1
            Errstr = ""
            Dim MDrivers As New List(Of Coordination2.CSDriver)
            Dim NDrivers As New List(Of Coordination2.CSDriver)
            Dim ADrivers As New List(Of Coordination2.CSDriver)
            Dim CDrivers As New List(Of Coordination2.CSDriver)
            For Each AMDri As AMDriver In AMDutyCons(i).AMDrivers
                If AMDri.MDriver IsNot Nothing Then
                    MDrivers.Add(AMDri.MDriver)
                End If
            Next
            NDrivers = AMDutyCons(i).NDrivers
            CDrivers = AMDutyCons(i).CDrivers
            If i < AMDutyCons.Count - 1 Then
                For Each AMDri As AMDriver In AMDutyCons(i + 1).AMDrivers
                    If AMDri.ADriver IsNot Nothing Then
                        ADrivers.Add(AMDri.ADriver)
                    End If
                Next
            Else
                ADrivers = CSTimeTables(CSTimeTables.Count - 1).ACSDrivers
            End If

            Errstr &= CheckUnAssignCSDrivers(MDrivers, Me.StartDate.AddDays(i))
            Errstr &= CheckUnAssignCSDrivers(NDrivers, Me.StartDate.AddDays(i))
            Errstr &= CheckUnAssignCSDrivers(ADrivers, Me.StartDate.AddDays(i))
            Errstr &= CheckUnAssignCSDrivers(CDrivers, Me.StartDate.AddDays(i))
            If Errstr <> "" Then
                Me.DGVErrorInfo.Rows.Add(Me.DGVErrorInfo.Rows.Count + 1, Me.StartDate.AddDays(i).ToString("yyyy/MM/dd"), Errstr.Trim(","))
            End If
            frmpro.Performstep()
        Next
        frmpro.Close()
    End Sub
    '========================

    Private Sub SaveAllDutys()
        If Me.TeamList Is Nothing OrElse Me.TeamList.Count = 0 OrElse Me.cstimetableNameList Is Nothing OrElse Me.cstimetableNameList.Length = 0 Then
            MsgBox("计划未编制！")
            Exit Sub
        Else
            Dim Str As String
            Try
                '=============================删除原信息
                Str = "delete from cs_datetimetable where lineid='" & CurLine.Name & "' and " & _
                  "datediff('d',dateno ,Format('" & Me.StartDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 " & _
                  "and datediff('d',dateno,Format('" & Me.EndDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>=0"
                Globle.Method.UpdateDataForAccess(Str)

                Str = "delete from CS_CORRESPONDING WHERE LINEID='" & Me.CurLine.Name _
                            & "' and datediff('d',DATENO,Format('" + Me.StartDate.Date.ToString("yyyy-MM-dd") _
                            & "','yyyy-mm-dd'))<=0 and datediff('d',DATENO,Format('" + Me.EndDate.Date.ToString("yyyy-MM-dd") _
                            & "','yyyy-mm-dd'))>=0 "
                Globle.Method.UpdateDataForAccess(Str)

                Str = "delete from cs_amdutycorrespond where lineid='" & Me.CurLine.Name & "' and datediff('d',adutydate,Format('" & _
                      Me.StartDate.Date.AddDays(-1).ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 and datediff('d',adutydate,Format('" & _
                      Me.EndDate.Date.AddDays(-1).ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>=0"
                Globle.Method.UpdateDataForAccess(Str)

                Dim pro As New FrmProgress(Me.cstimetableNameList.Length, "正在保存...")
                For i As Integer = 0 To Me.cstimetableNameList.Length - 1
                    Str = "insert into cs_datetimetable (lineid,dateno,cstimetableid) " & _
                      "values('" & CurLine.Name & "',Format('" & Me.StartDate.AddDays(i).ToString("yyyy/MM/dd") & _
                      "','yyyy/MM/dd'),'" & Me.CurLine.GetCSTimeTableFromName(cstimetableNameList(i)).ID & "')"
                    Globle.Method.UpdateDataForAccess(Str)

                    For Each driteam As CrewTrainingManager.DriverTeam In Me.TeamList
                        For Each teDrive As Coordination2.Driver In driteam.CoDrivers
                            'Str = "select rdriverno from cs_driverinf where beteam='" + teDrive.ID + "'"
                            'Dim tmpdatabase As System.Data.DataTable = Globle.Method.ReadDataForAccess(Str)
                            'If IsNothing(tmpdatabase) = False AndAlso tmpdatabase.Rows.Count > 0 Then
                            'For ssss As Integer = 0 To tmpdatabase.Rows.Count - 1
                            Str = "INSERT INTO CS_CORRESPONDING " _
                                & "(LINEID,rdriverno,Dateno,Driverno,Cstimetableid,Dutysort,ForDutysort) " _
                                & "VALUES( '" & Me.CurLine.Name _
                                & "','" & teDrive.ID _
                                & "',Format('" & Me.StartDate.AddDays(i).Date.ToString("yyyy/MM/dd") _
                                & "','yyyy/MM/dd'),'" & teDrive.DriverDayJobs(i).CSDriverNo _
                                & "','" & teDrive.DriverDayJobs(i).CSTimetableID _
                                & "','" & teDrive.DriverDayJobs(i).DutySort _
                                & "','" & teDrive.DriverDayJobs(i).ForDutySort _
                                                                               & "')"
                            Globle.Method.UpdateDataForAccess(Str)
                        Next
                    Next

                    For Each amdr As AMDriver In AMDutyCons(i).AMDrivers
                        If amdr.ADriver IsNot Nothing AndAlso amdr.MDriver Is Nothing Then
                            Str = "insert into cs_amdutycorrespond (lineid,adutydate,mdutydate,adriverno,mdriverno) " & _
                                  "values('" & CurLine.Name & "',Format('" & Me.StartDate.AddDays(i - 1).ToString("yyyy/MM/dd") & _
                                  "','yyyy/MM/dd'),Format('" & Me.StartDate.AddDays(i).ToString("yyyy/MM/dd") & "','yyyy/MM/dd'),'" & amdr.ADriver.CSdriverNo & "','null')"
                        ElseIf amdr.ADriver Is Nothing AndAlso amdr.MDriver IsNot Nothing Then
                            Str = "insert into cs_amdutycorrespond (lineid,adutydate,mdutydate,adriverno,mdriverno) " & _
                                  "values('" & CurLine.Name & "',Format('" & Me.StartDate.AddDays(i - 1).ToString("yyyy/MM/dd") & _
                                  "','yyyy/MM/dd'),Format('" & Me.StartDate.AddDays(i).ToString("yyyy/MM/dd") & "','yyyy/MM/dd'),'null','" & amdr.MDriver.CSdriverNo & "')"
                        Else
                            Str = "insert into cs_amdutycorrespond (lineid,adutydate,mdutydate,adriverno,mdriverno) " & _
                                  "values('" & CurLine.Name & "',Format('" & Me.StartDate.AddDays(i - 1).ToString("yyyy/MM/dd") & _
                                  "','yyyy/MM/dd'),Format('" & Me.StartDate.AddDays(i).ToString("yyyy/MM/dd") & "','yyyy/MM/dd'),'" _
                                  & amdr.ADriver.CSdriverNo & "','" & amdr.MDriver.CSdriverNo & "')"
                        End If
                        Try
                            Globle.Method.UpdateDataForAccess(Str)
                        Catch ex As Exception
                            MsgBox(ex.Message)
                        End Try

                    Next
                    pro.Performstep()
                Next
                pro.Close()
            Catch ex As Exception
                MsgBox(ex.Message)
                Exit Sub
            End Try
            MsgBox("保存完毕！")
        End If
    End Sub

    Public Sub RefreshColor(ByVal datagridview As DataGridView)
        For Each row As DataGridViewRow In datagridview.Rows
            For Each cell As DataGridViewCell In row.Cells
                If cell.Value IsNot Nothing Then
                    Dim tempStr As String = cell.Value.ToString().Trim().Split("/")(0)
                    If tempStr = "夜班" Then
                        cell.Style.ForeColor = Color.DarkGreen
                    ElseIf tempStr = "白班" Then
                        cell.Style.ForeColor = Color.SeaGreen
                    ElseIf tempStr = "早班" Then
                        cell.Style.ForeColor = Color.MidnightBlue
                    ElseIf tempStr = "休息" Then
                        cell.Style.ForeColor = Color.Red
                    ElseIf tempStr = "日勤班" Then
                        cell.Style.ForeColor = Color.SeaGreen
                    ElseIf tempStr = "年假" Then
                        cell.Style.BackColor = Color.LightGreen
                        cell.Style.ForeColor = Color.Red
                    ElseIf tempStr = "培训" Then
                        cell.Style.BackColor = Color.Purple
                        cell.Style.ForeColor = Color.White
                    End If
                    If datagridview.Columns(cell.ColumnIndex).Name.ToString.Trim <> "编号" AndAlso datagridview.Columns(cell.ColumnIndex).Name.ToString.Trim <> "班组" AndAlso datagridview.Columns(cell.ColumnIndex).Name.ToString.Trim <> "组号" _
                      AndAlso datagridview.Columns(cell.ColumnIndex).Name.ToString.Trim <> "姓名" AndAlso datagridview.Columns(cell.ColumnIndex).Name.ToString.Trim <> "总工时" _
                      AndAlso datagridview.Columns(cell.ColumnIndex).Name.ToString.Trim <> "峰班" Then

                        Dim _date As Date = CDate(datagridview.Columns(cell.ColumnIndex).Name.ToString.Trim)
                        Dim _teamNo As String = datagridview.Rows(cell.RowIndex).Cells("组号").Value.ToString.Trim
                        Dim team As CrewTrainingManager.DriverTeam = TeamList.Find(Function(value As CrewTrainingManager.DriverTeam)
                                                                                       Return value.TeamNo = _teamNo
                                                                                   End Function)
                        Dim Job As Coordination2.DriverDayJob = team.CoDrivers(0).DriverDayJobs.Find(Function(value As Coordination2.DriverDayJob)
                                                                                                         Return value.Date = _date.Date
                                                                                                     End Function)
                        If Job.IsChuBan Then
                            cell.Style.BackColor = Color.LightBlue
                        End If
                    End If
                End If
            Next
        Next
    End Sub

    Public Sub RefreshColor(ByVal cell As DataGridViewCell)
        If cell.Value IsNot Nothing Then
            If cell.Value.ToString.Trim.Length >= 2 Then
                Dim tempStr As String = cell.Value.ToString().Trim().Split("/")(0)
                If tempStr = "夜班" Then
                    cell.Style.ForeColor = Color.DarkGreen
                    cell.Style.BackColor = Color.White
                ElseIf tempStr = "白班" Then
                    cell.Style.ForeColor = Color.SeaGreen
                    cell.Style.BackColor = Color.White
                ElseIf tempStr = "早班" Then
                    cell.Style.ForeColor = Color.MidnightBlue
                    cell.Style.BackColor = Color.White
                ElseIf tempStr = "休息" Then
                    cell.Style.ForeColor = Color.Red
                    cell.Style.BackColor = Color.White
                ElseIf tempStr = "日勤班" Then
                    cell.Style.ForeColor = Color.SeaGreen
                    cell.Style.BackColor = Color.White
                ElseIf tempStr = "年假" Then
                    cell.Style.BackColor = Color.LightGreen
                    cell.Style.ForeColor = Color.Red
                ElseIf tempStr = "培训" Then
                    cell.Style.BackColor = Color.Purple
                    cell.Style.ForeColor = Color.White
                End If
                If cell.DataGridView.Columns(cell.ColumnIndex).Name.ToString.Trim <> "编号" AndAlso cell.DataGridView.Columns(cell.ColumnIndex).Name.ToString.Trim <> "组号" _
                      AndAlso cell.DataGridView.Columns(cell.ColumnIndex).Name.ToString.Trim <> "姓名" AndAlso cell.DataGridView.Columns(cell.ColumnIndex).Name.ToString.Trim <> "总工时" _
                      AndAlso cell.DataGridView.Columns(cell.ColumnIndex).Name.ToString.Trim <> "峰班" Then
                    Dim _date As Date = CDate(cell.DataGridView.Columns(cell.ColumnIndex).Name.ToString.Trim)
                    Dim _teamNo As String = cell.DataGridView.Rows(cell.RowIndex).Cells("组号").Value.ToString.Trim
                    Dim team As CrewTrainingManager.DriverTeam = TeamList.Find(Function(value As CrewTrainingManager.DriverTeam)
                                                                                   Return value.TeamNo = _teamNo
                                                                               End Function)
                    Dim Job As Coordination2.DriverDayJob = team.CoDrivers(0).DriverDayJobs.Find(Function(value As Coordination2.DriverDayJob)
                                                                                                     Return value.Date = _date.Date
                                                                                                 End Function)
                    If Job.IsChuBan Then
                        cell.Style.BackColor = Color.LightBlue
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub DutyGridView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If Me.CurrentCell.Value Is Nothing Then
            Exit Sub
        End If
        Dim ColumnName As String = sender.Columns(sender.CurrentCell.ColumnIndex).Name
        If ColumnName = "编号" OrElse ColumnName = " 班组" OrElse ColumnName = "组号" OrElse ColumnName = "总工时" OrElse ColumnName = "姓名" OrElse ColumnName = "峰班" Then
            Exit Sub
        End If
        Dim csTimetableID As String = CSTimeTables((CDate(ColumnName).Date - Me.StartDate.Date.AddDays(-1)).Days - 1).ID
        Dim DriverNo As String = ""
        Dim dutystr As String = sender.CurrentCell.Value.ToString()
        Dim dutysort As String = dutystr.Split("/")(0)
        If dutystr.Contains("/") AndAlso dutystr.Contains("||") Then
            DriverNo = dutystr.Substring(dutystr.IndexOf("/") + 1, dutystr.IndexOf("||") - dutystr.IndexOf("/") - 1)
        End If

        Dim linktrians As List(Of CS_CSMaker.CSLinkTrain) = LoadDuty(CurLine.Name, csTimetableID, DriverNo, dutysort)

        Dim frm As New FrmDetailDuty
        frm.DataGridView1.Columns.Add("任务号", "任务号")
        frm.DataGridView1.Columns.Add("车次", "车次")
        frm.DataGridView1.Columns.Add("任务开始时间", "任务开始时间")
        frm.DataGridView1.Columns.Add("任务开始站名", "任务开始站名")
        frm.DataGridView1.Columns.Add("任务结束时间", "任务结束时间")
        frm.DataGridView1.Columns.Add("任务结束站名", "任务结束站名")
        For Each train As CS_CSMaker.CSLinkTrain In linktrians
            If train.IsDeadHeading = False Then
                frm.DataGridView1.Rows.Add(DriverNo, train.OutputCheCi, Coordination2.Global.BeTime(train.StartTime), train.StartStaName, _
                                                        Coordination2.Global.BeTime(train.EndTime), train.EndStaName)
            Else
                frm.DataGridView1.Rows.Add(DriverNo, "随乘:" & train.OutputCheCi, Coordination2.Global.BeTime(train.StartTime), train.StartStaName, _
                                                        Coordination2.Global.BeTime(train.EndTime), train.EndStaName)
            End If
        Next
        frm.Show()
    End Sub

    Public Sub LoadVacationfo()               '加载年假信息
        Dim str As String = "select * from cs_vacinf t where lineid='" & Me.CurLine.Name & "' and vacdate>='" & Me.StartDate.Date.ToString("yyyy/MM/dd") & _
                             "' and vacdate<= '" & Me.EndDate.Date.ToString("yyyy/MM/dd") & _
                             "' order by teamno"
        Dim temptab As System.Data.DataTable = Globle.Method.ReadDataForAccess(str)
        If temptab IsNot Nothing AndAlso temptab.Rows.Count > 0 Then
            For Each row As DataRow In temptab.Rows
                Dim teamNo As String = row.Item("teamno").ToString
                Dim temTeam As CrewTrainingManager.DriverTeam = TeamList.Find(Function(value As CrewTrainingManager.DriverTeam)
                                                                                  Return value.TeamNo = teamNo
                                                                              End Function)
                If temTeam IsNot Nothing Then
                    temTeam.IFVacation = True
                    Dim temVac As New CrewTrainingManager.TypeVac
                    temVac.vDate = CDate(row.Item("vacdate").ToString).Date
                    temVac.vType = row.Item("vactype")
                    temTeam.VocDates.Add(temVac)
                End If
            Next
        End If
        temptab.Dispose()
    End Sub

    Private Sub TSB_AssignFirstDayDuty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSB_AssignFirstDayDuty.Click
        If AMDutyCons.Count = 0 Then
            MsgBox("参数设置未完整！", MsgBoxStyle.Information, "提醒")
            Exit Sub
        End If
        Dim frm As New FrmAssignFirstDuty
        frm.PreAMCon = AMDutyCons(0)
        frm.arealunzhuan = arealunzhuan
        frm.CurLine = Me.CurLine
        frm.cstimetableNameList = Me.cstimetableNameList
        frm.CSTimeTable = New Coordination2.CSTimeTable(Me.CurLine.GetCSTimeTableFromName(Me.cstimetableNameList(0)).ID, Me.CurLine.Name)

        If AMDutyCons.Count > 1 Then           '便于安排联合任务
            frm.NextAMCon = AMDutyCons(1)
        End If
        frm.CurLinename = CurLine.Name
        frm.AreaYunzhuanS = AreaYunZhuanS
        frm.FirDayDate = Me.StartDate.Date
        frm.PreDayTimeTable = PreDayTimeTable
        frm.FirDayTimeTable = CSTimeTables(0)

        For Each area As AreaYunZhuan In AreaYunZhuanS
            Dim tempTab As New TabPage
            tempTab.Text = area.AreaName
            Dim ShowDGV As New DataGridView
            ShowDGV.Columns.Add("编号", "编号")
            ShowDGV.Columns.Add("班组", "班组")
            ShowDGV.Columns.Add("组号", "组号")
            ShowDGV.Columns.Add("姓名", "姓名")
            ShowDGV.Columns.Add("前日班种", "前日班种")
            ShowDGV.Columns.Add("前日任务", "前日任务")
            ShowDGV.Columns.Add("首日班种", "首日班种")
            ShowDGV.Columns.Add("首日任务", "首日任务")
            ShowDGV.Columns("编号").Width = 60
            ShowDGV.Columns("班组").Width = 60
            ShowDGV.Columns("组号").Width = 60
            ShowDGV.Columns("姓名").Width = 60
            ShowDGV.Columns("前日班种").DefaultCellStyle.BackColor = Color.LightYellow
            ShowDGV.Columns("前日班种").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            ShowDGV.Columns("前日任务").DefaultCellStyle.BackColor = Color.LightYellow
            ShowDGV.Columns("前日任务").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            ShowDGV.Columns("首日班种").DefaultCellStyle.BackColor = Color.LightGreen
            ShowDGV.Columns("首日班种").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            ShowDGV.Columns("首日任务").DefaultCellStyle.BackColor = Color.LightGreen
            ShowDGV.Columns("首日任务").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            ShowDGV.Dock = DockStyle.Fill
            ShowDGV.ReadOnly = True
            ShowDGV.AllowUserToAddRows = False
            ShowDGV.AllowUserToDeleteRows = False
            For Each team As CrewTrainingManager.DriverTeam In area.AvaDrivers
                ShowDGV.Rows.Add(ShowDGV.Rows.Count + 1, team.ClassName, team.TeamNo, team.NameStr, "", "", "休息", "休息/无任务")
                Dim dutySort As String = team.CoDrivers(0).DriverDayJobs(0).DutySort
                Dim driverno As String = team.CoDrivers(0).DriverDayJobs(0).OutPutCSDriverNO
                Dim staname As String = team.CoDrivers(0).DriverDayJobs(0).StartStaName
                If dutySort <> "" Then
                    ShowDGV.Rows(ShowDGV.Rows.Count - 1).Cells("首日班种").Value = team.CoDrivers(0).DriverDayJobs(0).ForDutySort
                    If dutySort = "早班" Then
                        ShowDGV.Rows(ShowDGV.Rows.Count - 1).Cells("首日任务").Value = dutySort & "/" & driverno & "(" & staname & ")"
                    Else
                        ShowDGV.Rows(ShowDGV.Rows.Count - 1).Cells("首日任务").Value = dutySort & "/" & driverno
                    End If
                End If
            Next
            tempTab.Controls.Add(ShowDGV)
            frm.DutyDGVS.Add(ShowDGV)
            frm.TabControlMain.TabPages.Add(tempTab)
        Next
        If frm.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Call DrawDGVData()
            Call RefreshErrorInfo()
            Me.EditMode = 2
        End If
    End Sub

    Public Sub CLearAllAssignInfo()
        For Each team As CrewTrainingManager.DriverTeam In TeamList
            For Each dri As Coordination2.Driver In team.CoDrivers
                For i As Integer = 0 To dri.DriverDayJobs.Count - 1
                    Dim temdate As Date = dri.DriverDayJobs(i).Date.Date
                    dri.DriverDayJobs(i) = New Coordination2.DriverDayJob(temdate)
                Next
                dri.DriveTime = 0
                dri.DriveDistance = 0
                dri.CulTime = New Date(1900, 1, 1)
            Next
        Next
        For Each AMCon As AMDutyConnect In AMDutyCons
            For Each amduty As AMDriver In AMCon.AMDrivers
                If amduty.ADriver IsNot Nothing Then
                    amduty.ADriver.FlagDinner = False                 '用FlagDinner标识任务有无分配
                End If
                If amduty.MDriver IsNot Nothing Then
                    amduty.MDriver.FlagDinner = False
                End If
            Next
            For Each dri As Coordination2.CSDriver In AMCon.NDrivers
                dri.FlagDinner = False
            Next
            For Each dri As Coordination2.CSDriver In AMCon.CDrivers
                dri.FlagDinner = False
            Next
        Next
    End Sub

    Private Sub 任务详情DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 任务详情DToolStripMenuItem.Click
        Call DutyGridView_CellDoubleClick(Me.CurrentCell.Tag, Nothing)
    End Sub

    Private Sub DutyGridView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Me.CurrentCell = CType(sender, DataGridView).CurrentCell
        Me.CurrentCell.Tag = sender
        If Me.ModifyMode = 1 Then
            If Me.PreCell IsNot Nothing AndAlso Me.CurrentCell IsNot Nothing AndAlso Me.PreCell.Tag Is Me.CurrentCell.Tag AndAlso _
                Me.PreCell.ColumnIndex = Me.CurrentCell.ColumnIndex Then

                '=========交换DGV的文本
                Dim temptext As String = ""
                temptext = Me.PreCell.Value.ToString
                Me.PreCell.Value = Me.CurrentCell.Value
                Me.CurrentCell.Value = temptext
                '==========交换任务内容
                Dim PreTeamNo As String = Me.PreCell.Tag.Rows(Me.PreCell.RowIndex).Cells("组号").Value.ToString
                Dim PreDateStr As String = Me.PreCell.Tag.Columns(Me.PreCell.ColumnIndex).Name
                Dim PreTeamID As Integer = TeamList.FindIndex(Function(value As CrewTrainingManager.DriverTeam)
                                                                  Return value.TeamNo = PreTeamNo
                                                              End Function)
                Dim PreJobID As Integer = TeamList(PreTeamID).CoDrivers(0).DriverDayJobs.FindIndex(Function(value As Coordination2.DriverDayJob)
                                                                                                       Return value.Date.Date = CDate(PreDateStr).Date
                                                                                                   End Function)
                Dim CurTeamNo As String = Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("组号").Value.ToString
                Dim CurDateStr As String = Me.CurrentCell.Tag.Columns(Me.CurrentCell.ColumnIndex).Name
                Dim CurTeamID As Integer = TeamList.FindIndex(Function(value As CrewTrainingManager.DriverTeam)
                                                                  Return value.TeamNo = CurTeamNo
                                                              End Function)
                Dim CurJobID As Integer = TeamList(PreTeamID).CoDrivers(0).DriverDayJobs.FindIndex(Function(value As Coordination2.DriverDayJob)
                                                                                                       Return value.Date.Date = CDate(PreDateStr).Date
                                                                                                   End Function)
                If PreJobID >= 0 AndAlso PreTeamID >= 0 AndAlso CurJobID >= 0 AndAlso CurTeamID >= 0 Then
                    Dim temPreJob As Coordination2.DriverDayJob = Nothing
                    Dim temCurJob As Coordination2.DriverDayJob = Nothing
                    temPreJob = TeamList(PreTeamID).CoDrivers(0).DriverDayJobs(PreJobID)
                    temCurJob = TeamList(CurTeamID).CoDrivers(0).DriverDayJobs(CurJobID)
                    Dim PreForDutySort As String = temPreJob.ForDutySort
                    Dim CurForDutySort As String = temCurJob.ForDutySort
                    For Each dri As Coordination2.Driver In TeamList(PreTeamID).CoDrivers
                        dri.DriverDayJobs(PreJobID) = temCurJob
                        dri.DriverDayJobs(PreJobID).ForDutySort = PreForDutySort
                        dri.GetWorkLoad()
                    Next
                    For Each dri As Coordination2.Driver In TeamList(CurTeamID).CoDrivers
                        dri.DriverDayJobs(CurJobID) = temPreJob
                        dri.DriverDayJobs(CurJobID).ForDutySort = CurForDutySort
                        dri.GetWorkLoad()
                    Next
                    Me.CurrentCell.Tag.Rows(Me.PreCell.RowIndex).Cells("总工时").Value = TeamList(PreTeamID).CoDrivers(0).WorkTimeHour.ToString("0.00") & "||" & (TeamList(PreTeamID).CoDrivers(0).DriveDistance).ToString("0.00")
                    Me.CurrentCell.Tag.Rows(Me.PreCell.RowIndex).Cells("峰班").Value = TeamList(PreTeamID).CoDrivers(0).FengBanNum & "||" & TeamList(PreTeamID).CoDrivers(0).ZaoFengBanNum & "||" & TeamList(PreTeamID).CoDrivers(0).XiuxiFengBanNum

                    Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("总工时").Value = TeamList(CurTeamID).CoDrivers(0).WorkTimeHour.ToString("0.00") & "||" & (TeamList(CurTeamID).CoDrivers(0).DriveDistance).ToString("0.00")
                    Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("峰班").Value = TeamList(CurTeamID).CoDrivers(0).FengBanNum & "||" & TeamList(CurTeamID).CoDrivers(0).ZaoFengBanNum & "||" & TeamList(CurTeamID).CoDrivers(0).XiuxiFengBanNum

                End If
                RefreshColor(Me.PreCell)
                RefreshColor(Me.CurrentCell)
                Me.ModifyMode = 0
            Else
                MsgBox("交换失败,请选择正确的交换任务!", vbOKOnly + MsgBoxStyle.Information, "提示")
            End If
        End If
    End Sub

    Private Sub 交换任务EToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 交换任务EToolStripMenuItem.Click
        If Me.CurrentCell IsNot Nothing Then
            'Me.CurrentCell.Style.BackColor = Color.Red
            'Me.CurrentCell.Style.ForeColor = Color.White
            'Me.PreCell = Me.CurrentCell
            'Me.ModifyMode = 1          '将编辑模式设置为交换模式
            Dim TeamNo As String = Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("组号").Value.ToString
            Dim DateStr As String = Me.CurrentCell.Tag.Columns(Me.CurrentCell.ColumnIndex).Name
            Try
                Dim tps As Date = CDate(DateStr).Date
            Catch ex As Exception
                MsgBox("请选择有任务的一栏！")
                Exit Sub
            End Try
            Dim TeamID As Integer = TeamList.FindIndex(Function(value As CrewTrainingManager.DriverTeam)
                                                           Return value.TeamNo = TeamNo
                                                       End Function)
            Dim areaname As String = CType(Me.CurrentCell.Tag.Parent, TabPage).Text.Trim
            Dim JobID As Integer = TeamList(TeamID).CoDrivers(0).DriverDayJobs.FindIndex(Function(value As Coordination2.DriverDayJob)
                                                                                             Return value.Date.Date = CDate(DateStr).Date
                                                                                         End Function)
            Dim nf As New FrmChangeDury(TeamList(TeamID).CoDrivers(0).DriverDayJobs(JobID), CSTimeTables(JobID), areaname)
            If nf.ShowDialog = System.Windows.Forms.DialogResult.OK AndAlso nf.ChangeDri IsNot Nothing Then
                Dim CTeamID As String = TeamList.FindIndex(Function(value As CrewTrainingManager.DriverTeam)
                                                               Return (value.CoDrivers(0).DriverDayJobs(JobID).CSDriverNo = nf.ChangeDri.CSdriverNo) AndAlso (value.CoDrivers(0).DriverDayJobs(JobID).DutySort = nf.ChangeDri.DutySort)
                                                           End Function)
                If CTeamID > 0 Then
                    Dim CCell As DataGridViewCell = Nothing
                    If Me.CurrentCell.Tag.Rows.Count > 0 Then
                        For i As Integer = 0 To Me.CurrentCell.Tag.Rows.Count - 1
                            If Me.CurrentCell.Tag.Rows(i).Cells("组号").Value.ToString = TeamList(CTeamID).TeamNo Then
                                CCell = Me.CurrentCell.Tag.Rows(i).Cells(Me.CurrentCell.ColumnIndex)
                                Exit For
                            End If
                        Next
                    End If
                    If CCell IsNot Nothing Then
                        '=========交换DGV的文本
                        Dim temptext As String = ""
                        temptext = CCell.Value.ToString
                        CCell.Value = Me.CurrentCell.Value
                        Me.CurrentCell.Value = temptext
                        '=========交换任务
                        Dim temPreJob As Coordination2.DriverDayJob = Nothing
                        Dim temCurJob As Coordination2.DriverDayJob = Nothing
                        temPreJob = TeamList(TeamID).CoDrivers(0).DriverDayJobs(JobID)
                        temCurJob = TeamList(CTeamID).CoDrivers(0).DriverDayJobs(JobID)
                        Dim PreForDutySort As String = temPreJob.ForDutySort
                        Dim CurForDutySort As String = temCurJob.ForDutySort
                        For Each dri As Coordination2.Driver In TeamList(TeamID).CoDrivers
                            dri.DriverDayJobs(JobID) = temCurJob
                            dri.DriverDayJobs(JobID).ForDutySort = PreForDutySort
                            dri.GetWorkLoad()
                        Next
                        For Each dri As Coordination2.Driver In TeamList(CTeamID).CoDrivers
                            dri.DriverDayJobs(JobID) = temPreJob
                            dri.DriverDayJobs(JobID).ForDutySort = CurForDutySort
                            dri.GetWorkLoad()
                        Next
                        Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("总工时").Value = TeamList(TeamID).CoDrivers(0).WorkTimeHour.ToString("0.00") & "||" & (TeamList(TeamID).CoDrivers(0).DriveDistance).ToString("0.00")
                        Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("峰班").Value = TeamList(TeamID).CoDrivers(0).FengBanNum & "||" & TeamList(TeamID).CoDrivers(0).ZaoFengBanNum & "||" & TeamList(TeamID).CoDrivers(0).XiuxiFengBanNum
                        Me.CurrentCell.Tag.Rows(CCell.RowIndex).Cells("总工时").Value = TeamList(CTeamID).CoDrivers(0).WorkTimeHour.ToString("0.00") & "||" & (TeamList(CTeamID).CoDrivers(0).DriveDistance).ToString("0.00")
                        Me.CurrentCell.Tag.Rows(CCell.RowIndex).Cells("峰班").Value = TeamList(CTeamID).CoDrivers(0).FengBanNum & "||" & TeamList(CTeamID).CoDrivers(0).ZaoFengBanNum & "||" & TeamList(CTeamID).CoDrivers(0).XiuxiFengBanNum


                        RefreshColor(CCell)
                        RefreshColor(Me.CurrentCell)
                        Me.ModifyMode = 0
                    End If
                Else
                    For Each dri As Coordination2.Driver In TeamList(TeamID).CoDrivers
                        AddCSDriver(dri, nf.ChangeDri, CDate(DateStr).Date)      '分配任务
                        dri.GetWorkLoad()
                    Next
                    '更改DutyDatagrid
                    Me.CurrentCell.Value = nf.ChangeDri.DutySort & "/" & nf.ChangeDri.OutPutCSDriverNo & "||" & nf.ChangeDri.WorkTimeHour.ToString("0.00") & "||" & nf.ChangeDri.DriveDistance.ToString("0.00")
                    RefreshColor(Me.CurrentCell)
                    Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("总工时").Value = TeamList(TeamID).CoDrivers(0).WorkTimeHour.ToString("0.0") & "||" & (TeamList(TeamID).CoDrivers(0).DriveDistance).ToString("0.00")
                    Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("峰班").Value = TeamList(TeamID).CoDrivers(0).FengBanNum & "||" & TeamList(TeamID).CoDrivers(0).ZaoFengBanNum & "||" & TeamList(TeamID).CoDrivers(0).XiuxiFengBanNum

                    Dim UnAssignDuties As List(Of Coordination2.CSDriver) = GetUnAssignDuty(CDate(DateStr).Date)
                    '更改错误窗口
                    Dim Errindex As Integer = -1
                    For Each row As DataGridViewRow In Me.DGVErrorInfo.Rows
                        If row.Cells("日期").Value.ToString = DateStr Then
                            Errindex = row.Index
                        End If
                    Next
                    If Errindex = -1 Then
                        If UnAssignDuties.Count > 0 Then
                            Me.DGVErrorInfo.Rows.Add(Me.DGVErrorInfo.Rows.Count, DateStr)
                            Errindex = Me.DGVErrorInfo.Rows.Count - 1
                        End If
                    Else
                        If UnAssignDuties.Count = 0 Then
                            Me.DGVErrorInfo.Rows.RemoveAt(Errindex)
                            For Each row As DataGridViewRow In Me.DGVErrorInfo.Rows
                                row.Cells("编号").Value = row.Index + 1
                            Next
                        End If
                    End If
                    Errstr = ""
                    For Each dri As Coordination2.CSDriver In UnAssignDuties
                        Errstr &= dri.DutySort & "(" & dri.OutPutCSDriverNo & "),"
                    Next
                    If Errstr <> "" AndAlso Errindex <> -1 Then
                        Me.DGVErrorInfo.Rows(Errindex).Cells("未分配任务").Value = Errstr.Trim(",")
                    End If
                End If
            End If
        Else
            MsgBox("没有选择需要交换的任务,请重新选择!", vbOKOnly + MsgBoxStyle.Information, "提示")
        End If
    End Sub

    Private Sub TSBShowErrorInfo_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBShowErrorInfo.CheckStateChanged
        If Me.TSBShowErrorInfo.Checked Then
            Me.GBErrorInfo.Visible = True
        Else
            Me.GBErrorInfo.Visible = False
        End If
    End Sub

    Private Sub 分配空闲任务FToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 分配空闲任务FToolStripMenuItem.Click
        If Me.CurrentCell IsNot Nothing AndAlso Me.CurrentCell.Value IsNot Nothing Then
            Dim datestr As String = Me.CurrentCell.Tag.Columns(Me.CurrentCell.ColumnIndex).Name
            Dim CurStr As String = Me.CurrentCell.Value.ToString
            Try
                Dim tps As Date = CDate(datestr).Date
            Catch ex As Exception
                MsgBox("请选择有任务的一栏！")
                Exit Sub
            End Try
            If CurStr <> "" Then
                Dim DutyNo As String = CurStr.Substring(CurStr.IndexOf("/") + 1, CurStr.IndexOf("||") - CurStr.IndexOf("/") - 1)
                If DutyNo <> "无任务" AndAlso DutyNo <> "SP" Then
                    MsgBox("此人已有任务,不能分配空闲任务！", MsgBoxStyle.OkOnly, "提醒")
                    Exit Sub
                End If
            End If
            Dim UnAssignDuties As List(Of Coordination2.CSDriver) = GetUnAssignDuty(CDate(datestr).Date)
            If UnAssignDuties.Count = 0 Then
                MsgBox("该天任务全部分配，无空闲任务！", MsgBoxStyle.OkOnly, "提醒")
                Exit Sub
            End If
            Dim frm As New FrmAssignUnDuty
            frm.UnAssignDuties = UnAssignDuties
            If frm.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Dim teamNo As String = Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("组号").Value.ToString
                Dim team As CrewTrainingManager.DriverTeam = TeamList.Find(Function(value As CrewTrainingManager.DriverTeam)
                                                                               Return value.TeamNo = teamNo
                                                                           End Function)
                For Each dri As Coordination2.Driver In team.CoDrivers
                    If dri.name = Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("姓名").Value.ToString.Trim Then
                        AddCSDriver(dri, frm.SelectDuty, CDate(datestr).Date)      '分配任务
                        dri.GetWorkLoad()
                    End If
                Next
                '更改DutyDatagrid
                Me.CurrentCell.Value = frm.SelectDuty.DutySort & "/" & frm.SelectDuty.OutPutCSDriverNo & "||" & frm.SelectDuty.WorkTimeHour.ToString("0.00") & "||" & frm.SelectDuty.DriveDistance.ToString("0.00")
                RefreshColor(Me.CurrentCell)
                Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("总工时").Value = team.CoDrivers(0).WorkTimeHour.ToString("0.0") & "||" & (team.CoDrivers(0).DriveDistance).ToString("0.00")
                Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("峰班").Value = team.CoDrivers(0).FengBanNum & "||" & team.CoDrivers(0).ZaoFengBanNum & "||" & team.CoDrivers(0).XiuxiFengBanNum

                UnAssignDuties.Remove(frm.SelectDuty)
                '更改错误窗口
                Dim Errindex As Integer = -1
                For Each row As DataGridViewRow In Me.DGVErrorInfo.Rows
                    If row.Cells("日期").Value.ToString = datestr Then
                        Errindex = row.Index
                    End If
                Next
                If Errindex = -1 Then
                    If UnAssignDuties.Count > 0 Then
                        Me.DGVErrorInfo.Rows.Add(Me.DGVErrorInfo.Rows.Count, datestr)
                        Errindex = Me.DGVErrorInfo.Rows.Count - 1
                    End If
                Else
                    If UnAssignDuties.Count = 0 Then
                        Me.DGVErrorInfo.Rows.RemoveAt(Errindex)
                        For Each row As DataGridViewRow In Me.DGVErrorInfo.Rows
                            row.Cells("编号").Value = row.Index + 1
                        Next
                    End If
                End If
                Errstr = ""
                For Each dri As Coordination2.CSDriver In UnAssignDuties
                    Errstr &= dri.DutySort & "(" & dri.OutPutCSDriverNo & "),"
                Next
                If Errstr <> "" AndAlso Errindex <> -1 Then
                    Me.DGVErrorInfo.Rows(Errindex).Cells("未分配任务").Value = Errstr.Trim(",")
                End If
            End If
        End If
    End Sub

    Public Function GetUnAssignDuty(ByVal _date As Date) As List(Of Coordination2.CSDriver)
        GetUnAssignDuty = New List(Of Coordination2.CSDriver)
        Dim AMdutyIndex As Integer = AMDutyCons.FindIndex(Function(value As AMDutyConnect)
                                                              Return value.MDate.Date = _date.Date
                                                          End Function)
        Dim MDrivers As New List(Of Coordination2.CSDriver)
        Dim NDrivers As New List(Of Coordination2.CSDriver)
        Dim ADrivers As New List(Of Coordination2.CSDriver)
        Dim CDrivers As New List(Of Coordination2.CSDriver)
        For Each AMDri As AMDriver In AMDutyCons(AMdutyIndex).AMDrivers
            If AMDri.MDriver IsNot Nothing Then
                MDrivers.Add(AMDri.MDriver)
            End If
        Next
        NDrivers = AMDutyCons(AMdutyIndex).NDrivers
        CDrivers = AMDutyCons(AMdutyIndex).CDrivers
        If _date.Date < Me.EndDate.Date Then
            For Each AMDri As AMDriver In AMDutyCons(AMdutyIndex + 1).AMDrivers
                If AMDri.ADriver IsNot Nothing Then
                    ADrivers.Add(AMDri.ADriver)
                End If
            Next
        Else
            ADrivers = New Coordination2.CSTimeTable(GetTimetableIDFromDate(Me.EndDate.Date, CurLine.Name), CurLine.Name).ACSDrivers
        End If
        CheckCSDrivers(MDrivers, _date)
        CheckCSDrivers(NDrivers, _date)
        CheckCSDrivers(ADrivers, _date)
        CheckCSDrivers(CDrivers, _date)
        For Each dri As Coordination2.CSDriver In MDrivers
            If dri.FlagDinner = False Then
                GetUnAssignDuty.Add(dri)
            End If
        Next
        For Each dri As Coordination2.CSDriver In NDrivers
            If dri.FlagDinner = False Then
                GetUnAssignDuty.Add(dri)
            End If
        Next
        For Each dri As Coordination2.CSDriver In ADrivers
            If dri.FlagDinner = False Then
                GetUnAssignDuty.Add(dri)
            End If
        Next
        For Each dri As Coordination2.CSDriver In CDrivers
            If dri.FlagDinner = False Then
                GetUnAssignDuty.Add(dri)
            End If
        Next
    End Function

    ''' <summary>
    ''' 获取年假、培训、备班、备车等其它任务
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOtherDuty()
        GetOtherDuty = New List(Of Coordination2.CSDriver)
        Dim str As String = "select * from cs_otherdutyInfo where lineid='" & Me.CurLine.Name & "' order by id"
        Dim tab As Data.DataTable = Globle.Method.ReadDataForAccess(str)
        If tab IsNot Nothing Then
            For Each row As DataRow In tab.Rows
                Dim tempDri As New Coordination2.CSDriver
                tempDri.DutySort = row.Item("dutysort").ToString
                tempDri.CSdriverNo = row.Item("driverno").ToString
                tempDri.OutPutCSDriverNo = row.Item("driverno").ToString
                tempDri.startdutytime = row.Item("starttime")
                tempDri.endtime = row.Item("endtime")
                tempDri.TotalDayDriveTime = tempDri.endtime - tempDri.startdutytime
                tempDri.TotalDayWorkTime = tempDri.endtime - tempDri.startdutytime
                tempDri.DriveDistance = row.Item("drivedistance")
                GetOtherDuty.Add(tempDri)
            Next
        End If
    End Function

    Private Sub TSBRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBRefresh.Click
        Call DrawDGVData()
        Call RefreshErrorInfo()
    End Sub

    Private Sub TSBOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBOpen.Click
        Dim frm As New FrmOpenPlan(net)
        If frm.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Me.StartDate = frm.StartDate.Date
            Me.EndDate = frm.EndDate.Date
            Me.CurLine = frm.Curline
            Me.Yunzhunpara = "四班两转"
            Call LoadCSTimetablenames()
            Call LoadAmdutyCons()
            Call LoadDriversAndDutyInfo()
            Call GetAreaInfo()
            Call LoadDriversTreeView()
            Call DrawDGVData()
            Call RefreshErrorInfo()
            Call RefreshPara()
            Me.ActualDriverNum = TeamList.Count
            Me.TotalDriverNum = TeamList.Count
            Me.EditMode = 3
        End If
    End Sub

    Public Sub LoadCSTimetablenames()
        Dim str As String = "select t.*,m.cstimetablename from cs_datetimetable t,cs_cstimetableinf m " & _
                            "where t.cstimetableid=m.cstimetableid and t.lineid='" & Me.CurLine.Name & "' and datediff('d',t.dateno,Format('" & Me.StartDate.ToString("yyyy/MM/dd") & _
                            "','yyyy/MM/dd'))<=0 and datediff('d',t.dateno,Format('" & Me.EndDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>=0 order by t.dateno"
        Dim tab As System.Data.DataTable = Globle.Method.ReadDataForAccess(str)
        If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
            ReDim cstimetableNameList(tab.Rows.Count - 1)
            For i As Integer = 0 To tab.Rows.Count - 1
                cstimetableNameList(i) = tab.Rows(i).Item("cstimetablename").ToString
            Next
        End If
        tab.Dispose()
    End Sub

    Public Sub LoadAmdutyCons()
        AMDutyCons.Clear()
        Dim pro As New FrmProgress(Me.cstimetableNameList.Length, "正在加载运行图...")
        ReDim CSTimeTables(Me.cstimetableNameList.Length - 1)     '首先加载乘务计划
        For i As Integer = 0 To CSTimeTables.Length - 1
            CSTimeTables(i) = New Coordination2.CSTimeTable(Me.CurLine.GetCSTimeTableFromName(Me.cstimetableNameList(i)).ID, Me.CurLine.Name)
            pro.Performstep()
        Next
        pro.Close()
        PreDayTimeTable = GetTimetableFromDate(Me.StartDate.AddDays(-1), CurLine.Name)
        Dim Str As String = "select * from cs_amdutycorrespond where lineid='" & Me.CurLine.Name & "' and datediff('d',mdutydate,Format('" & Me.StartDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 and " & _
                                "datediff('d',mdutydate,Format('" & Me.EndDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>=0 order by adutydate"
        Dim tab As System.Data.DataTable = Globle.Method.ReadDataForAccess(Str)
        If tab IsNot Nothing Then
            For Each row As DataRow In tab.Rows
                Dim ADate As Date = CDate(row.Item("adutydate")).Date
                Dim MDate As Date = CDate(row.Item("mdutydate")).Date
                Dim aDriverno As String = row.Item("Adriverno").ToString
                Dim mDriverno As String = row.Item("Mdriverno").ToString
                Dim tempAMDuty As AMDriver
                Dim Mdr As Coordination2.CSDriver = Nothing
                Dim Adr As Coordination2.CSDriver = Nothing
                Dim ACstimetableid As String = ""
                Dim MCstimetableid As String = ""
                If mDriverno <> "null" Then
                    Mdr = CSTimeTables((MDate - Me.StartDate.Date.AddDays(-1)).Days - 1).MCSDrivers.Find(Function(value As Coordination2.CSDriver)
                                                                                                             Return value.CSdriverNo = mDriverno
                                                                                                         End Function)
                End If
                If aDriverno <> "null" Then
                    If (ADate - Me.StartDate.Date.AddDays(-1)).Days - 1 < 0 Then
                        Adr = PreDayTimeTable.ACSDrivers.Find(Function(value As Coordination2.CSDriver)
                                                                  Return value.CSdriverNo = aDriverno
                                                              End Function)
                    Else
                        Adr = CSTimeTables((ADate - Me.StartDate.Date.AddDays(-1)).Days - 1).ACSDrivers.Find(Function(value As Coordination2.CSDriver)
                                                                                                                 Return value.CSdriverNo = aDriverno
                                                                                                             End Function)
                    End If
                End If
                MCstimetableid = CSTimeTables((MDate - Me.StartDate.Date.AddDays(-1)).Days - 1).ID
                If (ADate - Me.StartDate.Date.AddDays(-1)).Days - 1 < 0 Then
                    If PreDayTimeTable IsNot Nothing Then
                        ACstimetableid = PreDayTimeTable.ID
                    End If
                Else
                    ACstimetableid = CSTimeTables((ADate - Me.StartDate.Date.AddDays(-1)).Days - 1).ID
                End If
                If aDriverno = "null" AndAlso mDriverno <> "null" Then
                    tempAMDuty = New AMDriver(Mdr, "早班")
                ElseIf aDriverno <> "null" AndAlso mDriverno = "null" Then
                    tempAMDuty = New AMDriver(Adr, "夜班")
                Else
                    tempAMDuty = New AMDriver(Adr, Mdr)
                End If
                Dim tempAMCon As AMDutyConnect = AMDutyCons.Find(Function(value As AMDutyConnect)
                                                                     Return (value.ADate.Date = ADate AndAlso value.MDate.Date = MDate)
                                                                 End Function)
                If tempAMCon IsNot Nothing Then
                    tempAMCon.AMDrivers.Add(tempAMDuty)
                    tempAMCon.NDrivers = CSTimeTables((MDate - Me.StartDate.Date.AddDays(-1)).Days - 1).NCSDrivers
                    tempAMCon.CDrivers = CSTimeTables((MDate - Me.StartDate.Date.AddDays(-1)).Days - 1).CCSDrivers
                Else
                    tempAMCon = New AMDutyConnect(ADate, MDate, ACstimetableid, MCstimetableid)
                    tempAMCon.AMDrivers.Add(tempAMDuty)
                    tempAMCon.NDrivers = CSTimeTables((MDate - Me.StartDate.Date.AddDays(-1)).Days - 1).NCSDrivers
                    tempAMCon.CDrivers = CSTimeTables((MDate - Me.StartDate.Date.AddDays(-1)).Days - 1).CCSDrivers
                    AMDutyCons.Add(tempAMCon)
                End If
            Next
        End If
        tab.Dispose()
    End Sub

    Public Sub LoadDriversAndDutyInfo()
        TeamList.Clear()
        Dim str As String = "select t.*,m.beclass,m.beteam,m.drivername,m.bezone,m.phone from cs_corresponding t,cs_driverinf m " & _
            "where t.lineid=m.lineid and t.rdriverno=m.rdriverno and t.lineid='" & Me.CurLine.Name & _
            "' and datediff('d',t.dateno ,Format('" & Me.StartDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 " & _
            "and datediff('d',t.dateno ,Format('" & Me.EndDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>=0 and m.beteam is not null order by t.dateno,m.beteam,t.rdriverno"
        Dim tab As System.Data.DataTable = Globle.Method.ReadDataForAccess(str)
        If tab IsNot Nothing Then
            For Each row As DataRow In tab.Rows
                Dim classname As String = row.Item("Beclass").ToString
                Dim teamno As String = row.Item("beteam").ToString
                Dim rdriverno As String = row.Item("rdriverno").ToString
                Dim dateno As Date = CDate(row.Item("dateno")).Date
                Dim driverno As String
                If row.Item("driverno") Is Nothing Then
                    driverno = ""
                Else
                    driverno = row.Item("driverno").ToString
                End If
                Dim dutysort As String = row.Item("dutysort").ToString
                Dim Fordutysort As String = row.Item("Fordutysort").ToString
                Dim tempCSDri As Coordination2.CSDriver = GetCSDriverFromTimetable(driverno, dutysort, CSTimeTables((dateno - Me.StartDate.Date.AddDays(-1)).Days - 1))
                If tempCSDri Is Nothing Then
                    tempCSDri = New Coordination2.CSDriver
                    Select Case dutysort
                        Case "早班"
                            tempCSDri.startdutytime = 5 * 3600
                            tempCSDri.endtime = 9 * 3600
                            tempCSDri.TotalDayDriveTime = 4 * 3600
                            tempCSDri.TotalDayWorkTime = 4 * 3600
                            tempCSDri.DutySort = dutysort
                            'tempCSDri.CSdriverNo = "SP"
                            'tempCSDri.OutPutCSDriverNo = "SP"
                            tempCSDri.CSdriverNo = driverno
                            tempCSDri.OutPutCSDriverNo = driverno
                        Case "白班"
                            tempCSDri.startdutytime = 9 * 3600
                            tempCSDri.endtime = 17 * 3600
                            tempCSDri.TotalDayDriveTime = 8 * 3600
                            tempCSDri.TotalDayWorkTime = 8 * 3600
                            tempCSDri.DutySort = dutysort
                            'tempCSDri.CSdriverNo = "SP"
                            'tempCSDri.OutPutCSDriverNo = "SP"
                            tempCSDri.CSdriverNo = driverno
                            tempCSDri.OutPutCSDriverNo = driverno
                        Case "日勤班"
                            tempCSDri.startdutytime = 9 * 3600
                            tempCSDri.endtime = 17 * 3600
                            tempCSDri.TotalDayDriveTime = 8 * 3600
                            tempCSDri.TotalDayWorkTime = 8 * 3600
                            tempCSDri.DutySort = dutysort
                            'tempCSDri.CSdriverNo = "SP"
                            'tempCSDri.OutPutCSDriverNo = "SP"
                            tempCSDri.CSdriverNo = driverno
                            tempCSDri.OutPutCSDriverNo = driverno
                        Case "夜班"
                            tempCSDri.startdutytime = 17 * 3600
                            tempCSDri.endtime = 21 * 3600
                            tempCSDri.TotalDayDriveTime = 4 * 3600
                            tempCSDri.TotalDayWorkTime = 4 * 3600
                            tempCSDri.DutySort = dutysort
                            'tempCSDri.CSdriverNo = "SP"
                            'tempCSDri.OutPutCSDriverNo = "SP"
                            tempCSDri.CSdriverNo = driverno
                            tempCSDri.OutPutCSDriverNo = driverno
                        Case "年假"
                            tempCSDri.startdutytime = 10 * 3600
                            tempCSDri.endtime = 18 * 3600
                            tempCSDri.TotalDayDriveTime = 8 * 3600
                            tempCSDri.TotalDayWorkTime = 8 * 3600
                            tempCSDri.DutySort = dutysort
                            tempCSDri.CSdriverNo = "无任务"
                            tempCSDri.OutPutCSDriverNo = "无任务"
                        Case "培训"
                            tempCSDri.startdutytime = 10 * 3600
                            tempCSDri.endtime = 18 * 3600
                            tempCSDri.TotalDayDriveTime = 8 * 3600
                            tempCSDri.TotalDayWorkTime = 8 * 3600
                            tempCSDri.DutySort = dutysort
                            tempCSDri.CSdriverNo = "无任务"
                            tempCSDri.OutPutCSDriverNo = "无任务"
                        Case Else
                            tempCSDri.DutySort = dutysort
                            tempCSDri.CSdriverNo = driverno
                            tempCSDri.OutPutCSDriverNo = driverno
                    End Select
                    tempCSDri.DriveDistance = 0
                End If
                Dim Driteam As CrewTrainingManager.DriverTeam = TeamList.Find(Function(value As CrewTrainingManager.DriverTeam)
                                                                                  Return value.TeamNo = teamno
                                                                              End Function)
                If Driteam Is Nothing Then
                    Driteam = New CrewTrainingManager.DriverTeam(Me.CurLine.Name, classname, teamno)
                    TeamList.Add(Driteam) '
                End If
                Dim Dri As Coordination2.Driver = Driteam.CoDrivers.Find(Function(value As Coordination2.Driver)
                                                                             Return value.ID = rdriverno
                                                                         End Function)
                If Dri Is Nothing Then
                    Dri = New Coordination2.Driver(classname, teamno, rdriverno, row.Item("drivername").ToString, Me.CurLine.Name, row.Item("bezone").ToString, Me.StartDate.Date, Me.EndDate.Date)
                    Driteam.CoDrivers.Add(Dri)
                End If
                AddCSDriver(Dri, tempCSDri, dateno)
                Dim theJob As Coordination2.DriverDayJob = Dri.DriverDayJobs.Find(Function(value As Coordination2.DriverDayJob)
                                                                                      Return value.Date = dateno.Date
                                                                                  End Function)
                theJob.ForDutySort = Fordutysort
            Next
        End If
        tab.Dispose()
    End Sub

    Public Sub GetAreaInfo()
        '========加载区域信息
        AreaYunZhuanS = New List(Of AreaYunZhuan)
        Dim Str As String = "select * from cs_areainfo where lineid='" & Me.CurLine.Name & "' order by id"
        Dim tab As System.Data.DataTable = Globle.Method.ReadDataForAccess(Str)
        If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
            For i As Integer = 0 To tab.Rows.Count - 1
                Dim tempArea As New AreaYunZhuan(tab.Rows(i).Item("LineID").ToString, tab.Rows(i).Item("AreaName").ToString)
                tempArea.OnDutyPlaces = tab.Rows(i).Item("OnDutyPlaces").ToString.Split(",")
                tempArea.ForDutySorts = tab.Rows(i).Item("DutySort").ToString.Split(",")
                tempArea.YunZhuanPara = tab.Rows(i).Item("YunZhuanPara").ToString
                AreaYunZhuanS.Add(tempArea)
            Next
        End If
        For Each area As AreaYunZhuan In AreaYunZhuanS
            For Each team As CrewTrainingManager.DriverTeam In TeamList
                If team.TeamNo = "0" Then
                    Dim tempArea As New AreaYunZhuan(CurLineName, "替班")
                    tempArea.AvaDrivers.Add(team)
                    AreaYunZhuanS.Add(tempArea)
                Else
                    If team.CoDrivers(0).BelongArea = area.AreaName Then
                        area.AvaDrivers.Add(team)
                    End If
                End If
            Next
            'For Each clas As CrewTrainingManager.DriverTeam In TeamList     '20160123新加
            '    If clas.CoDrivers(0).BelongArea = area.AreaName Then
            '        area.AvaDrivers.Add(clas)
            '    End If
            'Next
        Next
        '获取分区任务
        For Each area As AreaYunZhuan In AreaYunZhuanS
            For Each amcon As AMDutyConnect In AMDutyCons
                Dim tempAmcon As New AMDutyConnect(amcon.ADate, amcon.MDate, amcon.ACSTimetableID, amcon.MCSTimetableID)
                area.AmCons.Add(tempAmcon)
            Next
            For Each ds As String In area.ForDutySorts
                Select Case ds
                    Case "早班"
                        For Each amcon As AMDutyConnect In area.AmCons
                            Dim temcon As AMDutyConnect = amcon
                            Dim SelectAmcon As AMDutyConnect = AMDutyCons.Find(Function(value As AMDutyConnect)
                                                                                   Return value.ADate = temcon.ADate AndAlso value.MDate = temcon.MDate
                                                                               End Function)
                            For Each amdri As AMDriver In SelectAmcon.AMDrivers
                                If amdri IsNot Nothing AndAlso amdri.BelongArea = area.AreaName Then
                                    Dim tempdri As AMDriver = amdri
                                    Dim index As Integer = amcon.AMDrivers.FindIndex(Function(value As AMDriver)
                                                                                         Return value Is tempdri
                                                                                     End Function)
                                    If index = -1 Then
                                        amcon.AMDrivers.Add(tempdri)
                                    End If
                                End If
                            Next
                        Next
                    Case "白班"
                        For Each amcon As AMDutyConnect In area.AmCons
                            Dim temcon As AMDutyConnect = amcon
                            Dim SelectAmcon As AMDutyConnect = AMDutyCons.Find(Function(value As AMDutyConnect)
                                                                                   Return value.ADate = temcon.ADate AndAlso value.MDate = temcon.MDate
                                                                               End Function)
                            For Each ndri As Coordination2.CSDriver In SelectAmcon.NDrivers
                                If ndri IsNot Nothing AndAlso ndri.BelongArea = area.AreaName Then
                                    Dim tempdri As Coordination2.CSDriver = ndri
                                    Dim index As Integer = amcon.NDrivers.FindIndex(Function(value As Coordination2.CSDriver)
                                                                                        Return value Is tempdri
                                                                                    End Function)
                                    If index = -1 Then
                                        amcon.NDrivers.Add(tempdri)
                                    End If
                                End If
                            Next
                        Next
                    Case "日勤班"
                        For Each amcon As AMDutyConnect In area.AmCons
                            Dim temcon As AMDutyConnect = amcon
                            Dim SelectAmcon As AMDutyConnect = AMDutyCons.Find(Function(value As AMDutyConnect)
                                                                                   Return value.ADate = temcon.ADate AndAlso value.MDate = temcon.MDate
                                                                               End Function)
                            For Each cdri As Coordination2.CSDriver In SelectAmcon.CDrivers
                                If cdri IsNot Nothing AndAlso cdri.BelongArea = area.AreaName Then
                                    Dim tempdri As Coordination2.CSDriver = cdri
                                    Dim index As Integer = amcon.CDrivers.FindIndex(Function(value As Coordination2.CSDriver)
                                                                                        Return value Is tempdri
                                                                                    End Function)
                                    If index = -1 Then
                                        amcon.CDrivers.Add(tempdri)
                                    End If
                                End If
                            Next
                        Next
                    Case "夜班"
                        For Each amcon As AMDutyConnect In area.AmCons
                            Dim temcon As AMDutyConnect = amcon
                            Dim SelectAmcon As AMDutyConnect = AMDutyCons.Find(Function(value As AMDutyConnect)
                                                                                   Return value.ADate = temcon.ADate AndAlso value.MDate = temcon.MDate
                                                                               End Function)
                            For Each amdri As AMDriver In SelectAmcon.AMDrivers
                                If amdri IsNot Nothing AndAlso amdri.BelongArea = area.AreaName Then
                                    Dim tempdri As AMDriver = amdri
                                    Dim index As Integer = amcon.AMDrivers.FindIndex(Function(value As AMDriver)
                                                                                         Return value Is tempdri
                                                                                     End Function)
                                    If index = -1 Then
                                        amcon.AMDrivers.Add(tempdri)
                                    End If
                                End If
                            Next
                        Next
                End Select
            Next
        Next
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSBOutPutExcel.Click
        Me.Cursor = Cursors.WaitCursor
        Dim myExcel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()
        myExcel.Application.DisplayAlerts = False
        Dim myBook As Microsoft.Office.Interop.Excel.Workbook = myExcel.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet)
        Dim DayCount As Integer = (Me.EndDate.Date.AddDays(1) - Me.StartDate.Date).Days

        For Each area As AreaYunZhuan In AreaYunZhuanS
            If area.AvaDrivers.Count > 0 Then
                Dim mySheet As Microsoft.Office.Interop.Excel.Worksheet = New Microsoft.Office.Interop.Excel.Worksheet()
                mySheet = myBook.Sheets.Add()
                mySheet.Name = "排班表" & area.AreaName
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(2, DayCount * 3 + 3)).Merge()
                mySheet.Cells(1, 1) = CurLine.Name & "标准排班表"
                mySheet.Cells(3, 1) = "姓名"



                For i As Integer = 1 To DayCount
                    mySheet.Cells(3, (i - 1) * 3 + 1 + 1) = Me.StartDate.Date.AddDays(i - 1).ToString("MM月dd日") & " " & GlobalFunc.GetWeekDayString(Me.StartDate.Date.AddDays(i - 1)) & "              " & Me.cstimetableNameList(i - 1)
                    mySheet.Columns((i - 1) * 3 + 1 + 1).WrapText = True
                    mySheet.Columns((i - 1) * 3 + 1 + 1).columnwidth = 23
                    mySheet.Cells(3, (i - 1) * 3 + 2 + 1) = "工时"
                    mySheet.Cells(3, (i - 1) * 3 + 3 + 1) = "公里"
                Next
                mySheet.Cells(3, DayCount * 3 + 1 + 1) = "总工时"
                mySheet.Cells(3, DayCount * 3 + 2 + 1) = "总公里"
                Dim pro As New FrmProgress(area.AvaDrivers.Count, "正在导出" & CurLine.Name & "的标准排班表...")
                For i As Integer = 0 To area.AvaDrivers.Count - 1
                    Dim strName As String = ""
                    For j As Integer = 0 To area.AvaDrivers(i).CoDrivers.Count - 1
                        If j = area.AvaDrivers(i).CoDrivers.Count - 1 Then
                            strName &= area.AvaDrivers(i).CoDrivers(j).name
                        Else
                            strName &= area.AvaDrivers(i).CoDrivers(j).name & "-"
                        End If
                    Next
                    mySheet.Cells(4 + i, 1) = strName
                    For j As Integer = 1 To DayCount
                        mySheet.Cells(4 + i, (j - 1) * 3 + 1 + 1) = IIf(area.AvaDrivers(i).CoDrivers(0).DriverDayJobs(j - 1).CSDriverNo = "无任务", _
                            area.AvaDrivers(i).CoDrivers(0).DriverDayJobs(j - 1).DutySort, area.AvaDrivers(i).CoDrivers(0).DriverDayJobs(j - 1).DutySort & _
                            area.AvaDrivers(i).CoDrivers(0).DriverDayJobs(j - 1).OutPutCSDriverNO)
                        mySheet.Cells(4 + i, (j - 1) * 3 + 2 + 1) = area.AvaDrivers(i).CoDrivers(0).DriverDayJobs(j - 1).WorkTimeHour
                        mySheet.Cells(4 + i, (j - 1) * 3 + 3 + 1) = area.AvaDrivers(i).CoDrivers(0).DriverDayJobs(j - 1).DriveDistance
                    Next
                    mySheet.Cells(4 + i, DayCount * 3 + 1 + 1) = area.AvaDrivers(i).CoDrivers(0).WorkTimeHour
                    mySheet.Cells(4 + i, DayCount * 3 + 2 + 1) = area.AvaDrivers(i).CoDrivers(0).DriveDistance
                    pro.Performstep()
                Next
                pro.Close()
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(3 + area.AvaDrivers.Count, DayCount * 3 + 3)).Font.Name = "宋体"
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(3 + area.AvaDrivers.Count, DayCount * 3 + 3)).Font.Size = 9
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(3 + area.AvaDrivers.Count, DayCount * 3 + 3)).Borders.LineStyle = 1
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(3 + area.AvaDrivers.Count, DayCount * 3 + 3)).HorizontalAlignment = XlHAlign.xlHAlignCenter
                mySheet.Range(mySheet.Cells(1, 1), mySheet.Cells(3 + area.AvaDrivers.Count, DayCount * 3 + 3)).VerticalAlignment = XlHAlign.xlHAlignCenter
                With myExcel.ActiveWindow                '冻结表头
                    .SplitColumn = 0
                    .SplitRow = 3
                End With
                myExcel.ActiveWindow.FreezePanes = True
                myExcel.Columns.AutoFit()
            End If
        Next
        myExcel.Visible = True
        GC.Collect()
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub TSB_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSB_Save.Click
        Call SaveAllDutys()
    End Sub

    '==================以下是查询代码
    Public TimeTableDuty As List(Of CSDriver)
    Public rate As Integer
    Public moveno As Integer

    Private Sub BtnQuery_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnQuery.Click
        Me.LoadDutyData()
        Call DrawToDGV("")
    End Sub

    Public Sub LoadDutyData()
        If Me.CSTimeTables Is Nothing OrElse Me.CSTimeTables.Length = 0 Then
            MsgBox("请先打开计划再进行相应查询！", MsgBoxStyle.OkOnly, "提醒")
            Exit Sub
        End If
        TimeTableDuty = New List(Of CSDriver)
        Dim timetableid As String = GetTimetableIDFromDate(Me.TimetablePicker.Value.Date, Me.CurLine.Name)
        Dim str = "select * from cs_crewschedule t where t.cstimetableid='" & timetableid & "' order by ID"
        Dim tab As Data.DataTable = Globle.Method.ReadDataForAccess(str)

        If tab.Rows.Count > 0 Then                                 '=====加载任务信息
            Dim tempcsd As New CSDriver
            For i As Integer = 0 To tab.Rows.Count - 1
                If i = 0 Then
                    tempcsd = New CSDriver(tab.Rows(i).Item("lineid").ToString.Trim, tab.Rows(i).Item("cstimetableid").ToString.Trim, tab.Rows(i).Item("driverno").ToString.Trim, tab.Rows(i).Item("dutysort").ToString.Trim)
                    Dim templink As New LinkDuty(tab.Rows(i).Item("driverno").ToString.Trim, tab.Rows(i).Item("dutysort").ToString.Trim, tab.Rows(i).Item("trainno").ToString.Trim, _
                                                 IIf(Convert.ToInt32(tab.Rows(i).Item("starttime")) < 10800, Convert.ToInt32(tab.Rows(i).Item("starttime")) + 86400, Convert.ToInt32(tab.Rows(i).Item("starttime"))), _
                                                 tab.Rows(i).Item("startstaname").ToString.Trim, IIf(Convert.ToInt32(tab.Rows(i).Item("endtime")) < 10800, Convert.ToInt32(tab.Rows(i).Item("endtime")) + 86400, Convert.ToInt32(tab.Rows(i).Item("endtime"))), _
                                                 tab.Rows(i).Item("endstaname").ToString.Trim, tab.Rows(i).Item("vehicleno").ToString.Trim, Convert.ToDecimal(tab.Rows(i).Item("distance")), Convert.ToInt32(tab.Rows(i).Item("upordown")), Convert.ToInt32(tab.Rows(i).Item("staseq1")), Convert.ToInt32(tab.Rows(i).Item("staseq2")))
                    tempcsd.LinkDutyList.Add(templink)
                Else
                    If tab.Rows(i).Item("driverno").ToString <> tab.Rows(i - 1).Item("driverno").ToString Then
                        TimeTableDuty.Add(tempcsd)
                        tempcsd = New CSDriver(tab.Rows(i).Item("lineid").ToString.Trim, tab.Rows(i).Item("cstimetableid").ToString.Trim, tab.Rows(i).Item("driverno").ToString.Trim, tab.Rows(i).Item("dutysort").ToString.Trim)
                        Dim templink As New LinkDuty(tab.Rows(i).Item("driverno").ToString.Trim, tab.Rows(i).Item("dutysort").ToString.Trim, tab.Rows(i).Item("trainno").ToString.Trim, _
                                                 IIf(Convert.ToInt32(tab.Rows(i).Item("starttime")) < 10800, Convert.ToInt32(tab.Rows(i).Item("starttime")) + 86400, Convert.ToInt32(tab.Rows(i).Item("starttime"))), _
                                                 tab.Rows(i).Item("startstaname").ToString.Trim, IIf(Convert.ToInt32(tab.Rows(i).Item("endtime")) < 10800, Convert.ToInt32(tab.Rows(i).Item("endtime")) + 86400, Convert.ToInt32(tab.Rows(i).Item("endtime"))), _
                                                 tab.Rows(i).Item("endstaname").ToString.Trim, tab.Rows(i).Item("vehicleno").ToString.Trim, Convert.ToDecimal(tab.Rows(i).Item("distance")), Convert.ToInt32(tab.Rows(i).Item("upordown")), Convert.ToInt32(tab.Rows(i).Item("staseq1")), Convert.ToInt32(tab.Rows(i).Item("staseq2")))
                        tempcsd.LinkDutyList.Add(templink)
                    Else
                        Dim templink As New LinkDuty(tab.Rows(i).Item("driverno").ToString.Trim, tab.Rows(i).Item("dutysort").ToString.Trim, tab.Rows(i).Item("trainno").ToString.Trim, _
                                                 IIf(Convert.ToInt32(tab.Rows(i).Item("starttime")) < 10800, Convert.ToInt32(tab.Rows(i).Item("starttime")) + 86400, Convert.ToInt32(tab.Rows(i).Item("starttime"))), _
                                                 tab.Rows(i).Item("startstaname").ToString.Trim, IIf(Convert.ToInt32(tab.Rows(i).Item("endtime")) < 10800, Convert.ToInt32(tab.Rows(i).Item("endtime")) + 86400, Convert.ToInt32(tab.Rows(i).Item("endtime"))), _
                                                 tab.Rows(i).Item("endstaname").ToString.Trim, tab.Rows(i).Item("vehicleno").ToString.Trim, Convert.ToDecimal(tab.Rows(i).Item("distance")), Convert.ToInt32(tab.Rows(i).Item("upordown")), Convert.ToInt32(tab.Rows(i).Item("staseq1")), Convert.ToInt32(tab.Rows(i).Item("staseq2")))
                        tempcsd.LinkDutyList.Add(templink)
                    End If
                End If
            Next
            TimeTableDuty.Add(tempcsd)

            str = "select * from cs_deadheading t where t.cstimetableid='" & timetableid & _
                    "' and datediff('d',t.dateno ,Format('" & TimetablePicker.Value.ToString("yyyy/MM/dd") & _
                    "','yyyy/MM/dd'))=0 order by t.driverno,iif(val(t.starttime)<10800,val(t.starttime)+86400,val(t.starttime))"
            tab = New Data.DataTable
            tab = Globle.Method.ReadDataForAccess(str)

            If Me.TimeTableDuty.Count > 0 AndAlso tab.Rows.Count > 0 Then             '=====加载出入库等随乘信息
                For i As Integer = 0 To Me.TimeTableDuty.Count - 1
                    Me.TimeTableDuty(i).DHDutyList = New List(Of LinkDuty)
                    Me.TimeTableDuty(i).DHToDutyList = New List(Of LinkDuty)
                    For j As Integer = 0 To tab.Rows.Count - 1
                        If tab.Rows(j).Item("driverno").ToString = Me.TimeTableDuty(i).DriverNo Then
                            If tab.Rows(j).Item("headingproperty").ToString = "出库" OrElse tab.Rows(j).Item("headingproperty").ToString = "出班" Then
                                Dim templink As New LinkDuty(tab.Rows(j).Item("driverno").ToString.Trim, tab.Rows(j).Item("dutysort").ToString.Trim, tab.Rows(j).Item("trainno").ToString.Trim, _
                                                 IIf(Convert.ToInt32(tab.Rows(j).Item("starttime")) < 10800, Convert.ToInt32(tab.Rows(j).Item("starttime")) + 86400, Convert.ToInt32(tab.Rows(j).Item("starttime"))), _
                                                 tab.Rows(j).Item("startstaname").ToString.Trim, IIf(Convert.ToInt32(tab.Rows(j).Item("endtime")) < 10800, Convert.ToInt32(tab.Rows(j).Item("endtime")) + 86400, Convert.ToInt32(tab.Rows(j).Item("endtime"))), _
                                                 tab.Rows(j).Item("endstaname").ToString.Trim, tab.Rows(j).Item("vehicleno").ToString.Trim, Convert.ToDecimal(tab.Rows(j).Item("distance")), Convert.ToInt32(tab.Rows(j).Item("upordown")), Convert.ToInt32(tab.Rows(j).Item("startstaseq")), Convert.ToInt32(tab.Rows(j).Item("endstaseq")))
                                Me.TimeTableDuty(i).DHDutyList.Add(templink)
                            Else
                                Dim templink As New LinkDuty(tab.Rows(j).Item("driverno").ToString.Trim, tab.Rows(j).Item("dutysort").ToString.Trim, tab.Rows(j).Item("trainno").ToString.Trim, _
                                                 IIf(Convert.ToInt32(tab.Rows(j).Item("starttime")) < 10800, Convert.ToInt32(tab.Rows(j).Item("starttime")) + 86400, Convert.ToInt32(tab.Rows(j).Item("starttime"))), _
                                                 tab.Rows(j).Item("startstaname").ToString.Trim, IIf(Convert.ToInt32(tab.Rows(j).Item("endtime")) < 10800, Convert.ToInt32(tab.Rows(j).Item("endtime")) + 86400, Convert.ToInt32(tab.Rows(j).Item("endtime"))), _
                                                 tab.Rows(j).Item("endstaname").ToString.Trim, tab.Rows(j).Item("vehicleno").ToString.Trim, Convert.ToDecimal(tab.Rows(j).Item("distance")), Convert.ToInt32(tab.Rows(j).Item("upordown")), Convert.ToInt32(tab.Rows(j).Item("startstaseq")), Convert.ToInt32(tab.Rows(j).Item("endstaseq")))
                                Me.TimeTableDuty(i).DHToDutyList.Add(templink)
                            End If
                        End If
                    Next
                Next
            End If
        End If
        tab.Dispose()

        str = "select * from cs_dinnerinf t where t.cstimetableid='" & timetableid & "' and t.havedinner='True' order by t.driverno"
        tab = New Data.DataTable
        tab = Globle.Method.ReadDataForAccess(str)

        If Me.TimeTableDuty.Count > 0 AndAlso tab.Rows.Count > 0 Then
            For i As Integer = 0 To Me.TimeTableDuty.Count - 1
                Me.TimeTableDuty(i).MealDuty = New List(Of LinkDuty)
                For j As Integer = 0 To tab.Rows.Count - 1
                    If tab.Rows(j).Item("driverno").ToString = TimeTableDuty(i).DriverNo AndAlso tab.Rows(j).Item("dinnerplace").ToString <> "" Then
                        Dim templink As New LinkDuty(tab.Rows(j).Item("driverno").ToString, "用餐", "用餐", Convert.ToInt32(tab.Rows(j).Item("dinnerbegintime")), tab.Rows(j).Item("dinnerplace").ToString, _
                                                      Convert.ToInt32(tab.Rows(j).Item("dinnerendtime")), tab.Rows(j).Item("dinnerplace").ToString, "", 0, 0, 0, 0)
                        Me.TimeTableDuty(i).MealDuty.Add(templink)
                    End If
                Next
            Next
        End If

        For Each csd As CSDriver In TimeTableDuty             '计算指标
            csd.GetPreparedTime()
            csd.GetWorkLoad()
            csd.SimplifyCSDriver()
            csd.InsertMealDutyIntoDutylist()
        Next


    End Sub

    Private Sub TSB_DutyDetailOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TSB_DutyDetailOutput.Click
        If TeamList.Count = 0 Then
            MsgBox("没有找打任务，请先打开任务或者编制任务！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        Dim nf As New CS_CSMaker.frmInputBox
        nf.Text = "输出签到表"
        nf.labTitle.Text = "选择或输入日期:"
        nf.cmbText.Visible = True
        nf.txtText.Visible = False
        nf.cmbText.Items.Clear()
        For i As Integer = 1 To (Me.EndDate.Date.AddDays(1) - Me.StartDate.Date).Days
            nf.cmbText.Items.Add(Me.StartDate.Date.AddDays(i - 1).ToString("yyyy/MM/dd"))
        Next
        If nf.cmbText.Items.Count > 0 Then
            nf.cmbText.SelectedIndex = 0
        End If
        If nf.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            Dim myExcel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()
            myExcel.Application.DisplayAlerts = False
            Dim myBook As Microsoft.Office.Interop.Excel.Workbook = myExcel.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet)

            Dim index As Integer = (CDate(nf.cmbText.Text.Trim).Date.AddDays(1) - Me.StartDate.Date).Days - 1
            Dim SelectTimeTable As Coordination2.CSTimeTable = CSTimeTables(index)
            Dim SelectCSDrivers As New List(Of Coordination2.CSDriver)
            SelectCSDrivers.Clear()           '========输出白班任务
            SelectCSDrivers.AddRange(SelectTimeTable.NCSDrivers)
            SelectCSDrivers.AddRange(SelectTimeTable.CCSDrivers)
            Dim mySheet As Microsoft.Office.Interop.Excel.Worksheet = New Microsoft.Office.Interop.Excel.Worksheet()
            mySheet = myBook.Sheets.Add()
            mySheet.Name = "白班"
            Call DrawSignDataToExcel(SelectCSDrivers, mySheet, "白班", CDate(nf.cmbText.Text.Trim).Date)
            SelectCSDrivers.Clear()           '========输出夜班任务
            SelectCSDrivers.AddRange(SelectTimeTable.ACSDrivers)
            mySheet = New Microsoft.Office.Interop.Excel.Worksheet()
            mySheet = myBook.Sheets.Add()
            mySheet.Name = "夜班"
            Call DrawSignDataToExcel(SelectCSDrivers, mySheet, "夜班", CDate(nf.cmbText.Text.Trim).Date)
            SelectCSDrivers.Clear()           '========输出早班任务
            SelectCSDrivers.AddRange(SelectTimeTable.MCSDrivers)
            mySheet = New Microsoft.Office.Interop.Excel.Worksheet()
            mySheet = myBook.Sheets.Add()
            mySheet.Name = "早班"
            Call DrawSignDataToExcel(SelectCSDrivers, mySheet, "早班", CDate(nf.cmbText.Text.Trim).Date)

            myExcel.Visible = True
            GC.Collect()
            Me.Cursor = Cursors.Default
        End If
    End Sub

    Public Sub DrawSignDataToExcel(ByVal SelectDris As List(Of Coordination2.CSDriver), ByVal XSheet As Microsoft.Office.Interop.Excel.Worksheet, ByVal DutySort As String, ByVal sDate As Date)
        XSheet.Cells(1, 1) = "上海地铁" & CurLine.Name & DutySort & "签到表"
        XSheet.Range(XSheet.Cells(1, 1), XSheet.Cells(1, 16)).Merge()
        XSheet.Range(XSheet.Cells(1, 1), XSheet.Cells(1, 16)).Font.Size = 18
        XSheet.Range(XSheet.Cells(1, 1), XSheet.Cells(1, 16)).Font.Bold = True
        XSheet.Range(XSheet.Cells(4, 1), XSheet.Cells(4, 16)).Borders(XlBordersIndex.xlEdgeBottom).LineStyle = XlLineStyle.xlDouble
        XSheet.Cells(2, 3) = "年份"
        XSheet.Cells(3, 3) = "周数"
        XSheet.Cells(2, 3) = "年份"
        XSheet.Cells(2, 10) = "日期"
        XSheet.Cells(3, 10) = "周日"
        XSheet.Cells(2, 6) = sDate.Year
        XSheet.Cells(3, 6) = 1
        XSheet.Cells(2, 13) = sDate.ToString("MM月dd日")
        XSheet.Cells(3, 13) = GlobalFunc.GetWeekDayString(sDate)
        XSheet.Range(XSheet.Cells(2, 3), XSheet.Cells(2, 4)).Merge()
        XSheet.Range(XSheet.Cells(2, 3), XSheet.Cells(2, 4)).Font.Size = 12
        XSheet.Range(XSheet.Cells(2, 3), XSheet.Cells(2, 4)).Font.Bold = True
        XSheet.Range(XSheet.Cells(2, 6), XSheet.Cells(2, 7)).Merge()
        XSheet.Range(XSheet.Cells(2, 6), XSheet.Cells(2, 7)).Font.Size = 11
        XSheet.Range(XSheet.Cells(2, 6), XSheet.Cells(2, 7)).Borders(9).LineStyle = 1
        XSheet.Range(XSheet.Cells(3, 3), XSheet.Cells(3, 4)).Merge()
        XSheet.Range(XSheet.Cells(3, 3), XSheet.Cells(3, 4)).Font.Size = 12
        XSheet.Range(XSheet.Cells(3, 3), XSheet.Cells(3, 4)).Font.Bold = True
        XSheet.Range(XSheet.Cells(3, 6), XSheet.Cells(3, 7)).Merge()
        XSheet.Range(XSheet.Cells(3, 6), XSheet.Cells(3, 7)).Font.Size = 11
        XSheet.Range(XSheet.Cells(3, 6), XSheet.Cells(3, 7)).Borders(9).LineStyle = 1
        XSheet.Range(XSheet.Cells(2, 10), XSheet.Cells(2, 11)).Merge()
        XSheet.Range(XSheet.Cells(2, 10), XSheet.Cells(2, 11)).Font.Size = 12
        XSheet.Range(XSheet.Cells(2, 10), XSheet.Cells(2, 11)).Font.Bold = True
        XSheet.Range(XSheet.Cells(2, 13), XSheet.Cells(2, 14)).Merge()
        XSheet.Range(XSheet.Cells(2, 13), XSheet.Cells(2, 14)).Font.Size = 11
        XSheet.Range(XSheet.Cells(2, 13), XSheet.Cells(2, 14)).Borders(9).LineStyle = 1
        XSheet.Range(XSheet.Cells(3, 10), XSheet.Cells(3, 11)).Merge()
        XSheet.Range(XSheet.Cells(3, 10), XSheet.Cells(3, 11)).Font.Size = 12
        XSheet.Range(XSheet.Cells(3, 10), XSheet.Cells(3, 11)).Font.Bold = True
        XSheet.Range(XSheet.Cells(3, 13), XSheet.Cells(3, 14)).Merge()
        XSheet.Range(XSheet.Cells(3, 13), XSheet.Cells(3, 14)).Font.Size = 11
        XSheet.Range(XSheet.Cells(3, 13), XSheet.Cells(3, 14)).Borders(9).LineStyle = 1
        XSheet.Cells(5, 1) = "司机出乘信息"
        XSheet.Cells(5, 6) = "上班"
        XSheet.Cells(5, 9) = "下班"
        XSheet.Cells(5, 12) = "工时"
        XSheet.Cells(5, 13) = "其他"
        XSheet.Cells(5, 16) = "联系电话"
        XSheet.Cells(6, 1) = "表号" & vbCrLf & "组号"
        XSheet.Cells(6, 2) = "员工" & vbCrLf & "姓名"
        XSheet.Cells(6, 3) = "员工" & vbCrLf & "编号"
        XSheet.Cells(6, 4) = "酒精" & vbCrLf & "测试"
        XSheet.Cells(6, 5) = "着装" & vbCrLf & "检查"
        XSheet.Cells(6, 6) = "时间"
        XSheet.Cells(6, 7) = "地点"
        XSheet.Cells(6, 8) = "签名"
        XSheet.Cells(6, 9) = "时间"
        XSheet.Cells(6, 10) = "地点"
        XSheet.Cells(6, 11) = "签名"
        XSheet.Cells(6, 13) = "实际退" & vbCrLf & "勤时间"
        XSheet.Cells(6, 14) = "公里" & vbCrLf & "组号"
        XSheet.Cells(6, 15) = "考试" & vbCrLf & "成绩"
        XSheet.Range(XSheet.Cells(5, 1), XSheet.Cells(5, 5)).Merge()
        XSheet.Range(XSheet.Cells(5, 6), XSheet.Cells(5, 8)).Merge()
        XSheet.Range(XSheet.Cells(5, 9), XSheet.Cells(5, 11)).Merge()
        XSheet.Range(XSheet.Cells(5, 13), XSheet.Cells(5, 15)).Merge()
        XSheet.Range(XSheet.Cells(5, 16), XSheet.Cells(6, 16)).Merge()
        XSheet.Range(XSheet.Cells(5, 12), XSheet.Cells(6, 12)).Merge()

        If SelectDris.Count > 2 Then               '排序
            For i As Integer = 0 To SelectDris.Count - 2
                For j As Integer = i + 1 To SelectDris.Count - 1
                    If SelectDris(j).OutPutCSDriverNo < SelectDris(i).OutPutCSDriverNo Then
                        Dim tempDri As Coordination2.CSDriver = SelectDris(i)
                        SelectDris(i) = SelectDris(j)
                        SelectDris(j) = tempDri
                    End If
                Next
            Next
        End If

        Dim nCurPos As Integer = 7
        Dim nf As New FrmProgress(SelectDris.Count, "正在导出" & DutySort & "签到表...")
        For Each dri As Coordination2.CSDriver In SelectDris
            XSheet.Cells(nCurPos, 1) = DutySort & dri.OutPutCSDriverNo
            XSheet.Range(XSheet.Cells(nCurPos, 1), XSheet.Cells(nCurPos, 1)).Interior.Color = 65535       '设置为黄色
            XSheet.Cells(nCurPos, 6) = CDate(Coordination2.Global.BeTime(dri.startdutytime)).ToString("HH:mm")
            XSheet.Cells(nCurPos, 7) = dri.StartStaName
            XSheet.Cells(nCurPos, 9) = CDate(Coordination2.Global.BeTime(dri.endtime)).ToString("HH:mm")
            XSheet.Cells(nCurPos, 10) = dri.OffStaName
            XSheet.Cells(nCurPos, 12) = CS_CSMaker.MyCeiLing(0.25, CDec(dri.TotalDayWorkTime) / 3600)
            XSheet.Cells(nCurPos, 14) = dri.DriveDistance
            Dim CSDriverNO As String = dri.CSdriverNo
            Dim selectTeam As CrewTrainingManager.DriverTeam = TeamList.Find(Function(value As CrewTrainingManager.DriverTeam)
                                                                                 Dim temJob As Coordination2.DriverDayJob = value.CoDrivers(0).DriverDayJobs.Find(Function(value2 As Coordination2.DriverDayJob)
                                                                                                                                                                      Return value2.Date = sDate
                                                                                                                                                                  End Function)
                                                                                 Return (temJob.CSDriverNo = CSDriverNO)
                                                                             End Function)
            Dim nCount As Integer = nCurPos
            If selectTeam IsNot Nothing Then
                XSheet.Cells(nCount + 1, 1) = selectTeam.TeamNo & "组"
                For Each RDri As Coordination2.Driver In selectTeam.CoDrivers
                    XSheet.Range(XSheet.Cells(nCount, 3), XSheet.Cells(nCount, 3)).NumberFormat = "@"
                    XSheet.Cells(nCount, 2) = RDri.name
                    XSheet.Cells(nCount, 3) = RDri.ID
                    XSheet.Cells(nCount, 16) = RDri.PhoneNum
                    nCount += 1
                Next
            End If
            XSheet.Range(XSheet.Cells(nCurPos + 1, 1), XSheet.Cells(IIf(nCount <= nCurPos + 1, nCurPos + 1, nCount - 1), 1)).Merge()
            nCurPos = IIf(nCount <= nCurPos + 1, nCurPos + 1, nCount - 1) + 1
            nf.Performstep()
        Next
        XSheet.Range(XSheet.Cells(1, 1), XSheet.Cells(nCurPos - 1, 16)).Font.Name = "Arial Unicode MS"
        XSheet.Range(XSheet.Cells(1, 1), XSheet.Cells(nCurPos - 1, 16)).HorizontalAlignment = XlHAlign.xlHAlignCenter
        XSheet.Range(XSheet.Cells(1, 1), XSheet.Cells(nCurPos - 1, 16)).VerticalAlignment = XlHAlign.xlHAlignCenter
        XSheet.Range(XSheet.Cells(5, 1), XSheet.Cells(nCurPos - 1, 16)).Borders.LineStyle = 1
        XSheet.Columns.AutoFit()
        XSheet.Rows.AutoFit()
        nf.Close()
    End Sub

    Private Sub TTXTDutyNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TTXTDutyNo.TextChanged
        If TimeTableDuty IsNot Nothing AndAlso TimeTableDuty.Count > 0 Then
            Call DrawToDGV(Me.TTXTDutyNo.Text.Trim)
        End If
    End Sub

    Public Sub DrawToDGV(ByVal csdriverno As String)
        If Me.CSTimeTables Is Nothing OrElse Me.CSTimeTables.Length = 0 Then
            Exit Sub
        End If
        Me.DGVTimetable.Rows.Clear()
        For Each csd As CSDriver In Me.TimeTableDuty
            If csdriverno <> "" Then
                If csdriverno <> csd.DriverNo Then
                    Continue For
                End If
            End If
            If csd.DHDutyList IsNot Nothing AndAlso csd.DHDutyList.Count > 0 Then
                For Each link As LinkDuty In csd.DHDutyList
                    Me.DGVTimetable.Rows.Add(link.driverno, link.dutysort, "随乘:" & link.trainno, Coordination2.Global.BeTime(link.starttime), _
                                             link.startstaname, Coordination2.Global.BeTime(link.endtime), link.endstaname)
                Next
            End If

            If csd.LinkDutyList IsNot Nothing AndAlso csd.LinkDutyList.Count > 0 Then
                For Each link As LinkDuty In csd.LinkDutyList
                    Me.DGVTimetable.Rows.Add(link.driverno, link.dutysort, link.trainno, Coordination2.Global.BeTime(link.starttime), _
                                             link.startstaname, Coordination2.Global.BeTime(link.endtime), link.endstaname)
                Next
            End If

            If csd.DHToDutyList IsNot Nothing AndAlso csd.DHToDutyList.Count > 0 Then
                For Each link As LinkDuty In csd.DHToDutyList
                    Me.DGVTimetable.Rows.Add(link.driverno, link.dutysort, "随乘:" & link.trainno, Coordination2.Global.BeTime(link.starttime), _
                                             link.startstaname, Coordination2.Global.BeTime(link.endtime), link.endstaname)
                Next
            End If
        Next
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Select Case Me.ModifyMode
            Case 0
                Me.LBModify.Text = "修改状态:非修改状态"
            Case 1
                Me.LBModify.Text = "修改状态:交换任务状态"
        End Select
        Select Case Me.EditMode
            Case 0
                Me.TSB_Save.Enabled = False
                Me.TSB_AssignFirstDayDuty.Enabled = False
                Me.TSBAssignDuty.Enabled = False
                Me.TSBRefresh.Enabled = False
                Me.TSBOutPutExcel.Enabled = False
                Me.LBEdit.Text = "编制状态:尚未设置编制参数"
            Case 1
                Me.TSB_Save.Enabled = False
                Me.TSB_AssignFirstDayDuty.Enabled = True
                Me.TSBAssignDuty.Enabled = True
                Me.TSBRefresh.Enabled = False
                Me.TSBOutPutExcel.Enabled = False
                Me.LBEdit.Text = "编制状态:参数设置完成"
            Case 2
                Me.TSB_Save.Enabled = False
                Me.TSB_AssignFirstDayDuty.Enabled = True
                Me.TSBAssignDuty.Enabled = True
                Me.TSBRefresh.Enabled = True
                Me.TSBOutPutExcel.Enabled = False
                Me.LBEdit.Text = "编制状态:首日任务设置完成"
            Case 3
                Me.TSB_Save.Enabled = True
                Me.TSB_AssignFirstDayDuty.Enabled = False
                Me.TSBAssignDuty.Enabled = False
                Me.TSBRefresh.Enabled = True
                Me.TSBOutPutExcel.Enabled = True
                Me.LBEdit.Text = "编制状态:任务轮转完成"
        End Select
    End Sub

    Private Sub DutyDGV_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Escape Then
            Me.ModifyMode = 0
            If Me.PreCell IsNot Nothing Then
                Call RefreshColor(Me.PreCell)
                Me.PreCell = Nothing
            End If
        ElseIf e.KeyCode = Keys.Delete Then
            Call 删除任务DToolStripMenuItem_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub 删除任务DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除任务DToolStripMenuItem.Click
       
        If Me.CurrentCell IsNot Nothing AndAlso Me.CurrentCell.Value IsNot Nothing Then
            Dim datestr As String = Me.CurrentCell.Tag.Columns(Me.CurrentCell.ColumnIndex).Name
            Dim CurStr As String = Me.CurrentCell.Value.ToString
            Try
                Dim tps As Date = CDate(datestr).Date
            Catch ex As Exception
                MsgBox("请选择有任务的一栏！")
                Exit Sub
            End Try
            If MsgBox("确实要删除该司机的当前任务？", MsgBoxStyle.OkCancel + MsgBoxStyle.Information, "提示") = MsgBoxResult.Ok Then

                Dim DutySort As String = CurStr.Split("/")(0)
                Dim teamNo As String = Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("组号").Value.ToString
                Dim team As CrewTrainingManager.DriverTeam = TeamList.Find(Function(value As CrewTrainingManager.DriverTeam)
                                                                               Return value.TeamNo = teamNo
                                                                           End Function)
                For Each dri As Coordination2.Driver In team.CoDrivers
                    If dri.name <> Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("姓名").Value.ToString.Trim Then
                        Continue For
                    End If
                    Dim JobIndex As Integer = dri.DriverDayJobs.FindIndex(Function(value As Coordination2.DriverDayJob)
                                                                              Return value.Date = CDate(datestr).Date
                                                                          End Function)
                    Dim ForDutySort As String = dri.DriverDayJobs(JobIndex).ForDutySort
                    dri.DriverDayJobs(JobIndex) = New Coordination2.DriverDayJob(CDate(datestr).Date)
                    dri.DriverDayJobs(JobIndex).DutySort = "休息"
                    dri.DriverDayJobs(JobIndex).CSDriverNo = "无任务"
                    dri.DriverDayJobs(JobIndex).ForDutySort = ForDutySort
                    dri.GetWorkLoad()
                    Me.CurrentCell.Value = dri.DriverDayJobs(JobIndex).DutySort & "/" & dri.DriverDayJobs(JobIndex).CSDriverNo & _
                        "||" & dri.DriverDayJobs(JobIndex).WorkTimeHour.ToString("0.00") & "||" & dri.DriverDayJobs(JobIndex).DriveDistance.ToString("0.00")
                    RefreshColor(Me.CurrentCell)
                Next
                '更改DutyDatagrid
                Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("总工时").Value = team.CoDrivers(0).WorkTimeHour.ToString("0.00") & "||" & (team.CoDrivers(0).DriveDistance).ToString("0.00")
                Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("峰班").Value = team.CoDrivers(0).FengBanNum & "||" & team.CoDrivers(0).ZaoFengBanNum & "||" & team.CoDrivers(0).XiuxiFengBanNum

                Dim UnAssignDuties As List(Of Coordination2.CSDriver) = GetUnAssignDuty(CDate(datestr).Date)
                '更改错误窗口
                Dim Errindex As Integer = -1
                For Each row As DataGridViewRow In Me.DGVErrorInfo.Rows
                    If row.Cells("日期").Value.ToString = datestr Then
                        Errindex = row.Index
                    End If
                Next
                If Errindex = -1 Then
                    If UnAssignDuties.Count > 0 Then
                        Me.DGVErrorInfo.Rows.Add(Me.DGVErrorInfo.Rows.Count + 1, datestr)
                        Errindex = Me.DGVErrorInfo.Rows.Count - 1
                    End If
                Else
                    If UnAssignDuties.Count = 0 Then
                        Me.DGVErrorInfo.Rows.RemoveAt(Errindex)
                        For Each row As DataGridViewRow In Me.DGVErrorInfo.Rows
                            row.Cells("编号").Value = row.Index + 1
                        Next
                    End If
                End If
                Errstr = ""
                For Each dri As Coordination2.CSDriver In UnAssignDuties
                    Errstr &= dri.DutySort & "(" & dri.OutPutCSDriverNo & "),"
                Next
                If Errstr <> "" AndAlso Errindex <> -1 Then
                    Me.DGVErrorInfo.Rows(Errindex).Cells("未分配任务").Value = Errstr.Trim(",")
                End If
            End If
        End If
    End Sub

    Private Sub 分配其它任务OToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 分配其它任务OToolStripMenuItem.Click
        If Me.CurrentCell IsNot Nothing AndAlso Me.CurrentCell.Value IsNot Nothing Then
            Dim datestr As String = Me.CurrentCell.Tag.Columns(Me.CurrentCell.ColumnIndex).Name
            Try
                Dim tps As Date = CDate(datestr).Date
            Catch ex As Exception
                MsgBox("请选择有任务的一栏！")
                Exit Sub
            End Try
            Dim CurStr As String = Me.CurrentCell.Value.ToString
            If CurStr <> "" Then
                Dim DutyNo As String = CurStr.Substring(CurStr.IndexOf("/") + 1, CurStr.IndexOf("||") - CurStr.IndexOf("/") - 1)
                If DutyNo <> "无任务" AndAlso DutyNo <> "SP" Then
                    MsgBox("此人已有任务,不能分配空闲任务！", MsgBoxStyle.OkOnly, "提醒")
                    Exit Sub
                End If
            End If
            Dim UnAssignDuties As List(Of Coordination2.CSDriver) = GetOtherDuty()
            Dim frm As New FrmAssignUnDuty
            frm.UnAssignDuties = UnAssignDuties
            If frm.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Dim teamNo As String = Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("组号").Value.ToString
                Dim team As CrewTrainingManager.DriverTeam = TeamList.Find(Function(value As CrewTrainingManager.DriverTeam)
                                                                               Return value.TeamNo = teamNo
                                                                           End Function)
                For Each dri As Coordination2.Driver In team.CoDrivers
                    If dri.name <> Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("姓名").Value.ToString.Trim Then
                        Continue For
                    End If
                    AddCSDriver(dri, frm.SelectDuty, CDate(datestr).Date)       '分配任务
                    dri.GetWorkLoad()
                Next
                '更改DutyDatagrid
                Me.CurrentCell.Value = frm.SelectDuty.DutySort & "/" & frm.SelectDuty.CSdriverNo & "||" & frm.SelectDuty.WorkTimeHour.ToString("0.0") & "||" & frm.SelectDuty.DriveDistance.ToString("0.0")
                RefreshColor(Me.CurrentCell)
                Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("总工时").Value = team.CoDrivers(0).WorkTimeHour.ToString("0.0") & "||" & (team.CoDrivers(0).DriveDistance).ToString("0.0")
                Me.CurrentCell.Tag.Rows(Me.CurrentCell.RowIndex).Cells("峰班").Value = team.CoDrivers(0).FengBanNum & "||" & team.CoDrivers(0).ZaoFengBanNum & "||" & team.CoDrivers(0).XiuxiFengBanNum

            End If
        End If
    End Sub

    Private Sub FrmClassCoordinate_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If MsgBox("确定数据以保存，并退出？", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
            e.Cancel = True
        End If
    End Sub

    Private Sub DGVErrorInfo_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVErrorInfo.CellDoubleClick
        Dim datestr As String = DGVErrorInfo.Rows(e.RowIndex).Cells("日期").Value.ToString
        Dim UnAssignDuties As List(Of Coordination2.CSDriver) = GetUnAssignDuty(CDate(datestr).Date)
        Dim frm As New FrmAssignDuty
        frm.UnAssignDuties = UnAssignDuties
        frm.dutytime = CDate(datestr).Date
        frm._AreaYunZhuanS = AreaYunZhuanS
        frm._TeamList = TeamList
        frm.starttime = StartDate
        frm.endtime = EndDate
        frm.ShowDialog()
        Call DrawDGVData()
        Call RefreshErrorInfo()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Me.RQComboBox1.Items.Clear()
        For i As Integer = 1 To arealunzhuan(ComboBox1.SelectedItem.ToString).Count
            Me.RQComboBox1.Items.Add("第" + i.ToString + "天:" + arealunzhuan(ComboBox1.SelectedItem.ToString)(i))
        Next
    End Sub
End Class

Public Class AMDutyConnect
    Public ADate As Date
    Public MDate As Date
    Public ACSTimetableID As String
    Public MCSTimetableID As String
    Public ADHDriverList As New List(Of CS_CSMaker.CSDriver)
    Public MDHDriverList As New List(Of CS_CSMaker.CSDriver)
    Public AMDrivers As New List(Of AMDriver)
    Public NDrivers As New List(Of Coordination2.CSDriver)
    Public CDrivers As New List(Of Coordination2.CSDriver)
    Public Sub New(ByVal date1 As Date, ByVal date2 As Date, ByVal id1 As String, ByVal id2 As String)
        ADate = date1
        MDate = date2
        ACSTimetableID = id1
        MCSTimetableID = id2
    End Sub

    Public ReadOnly Property DutyCount As Integer
        Get
            Return AMDrivers.Count + NDrivers.Count
        End Get
    End Property
End Class

Public Class AMDriver
    Public ADriver As Coordination2.CSDriver
    Public MDriver As Coordination2.CSDriver

    Public Sub New(ByVal adri As Coordination2.CSDriver, ByVal mdri As Coordination2.CSDriver)
        ADriver = adri
        MDriver = mdri
    End Sub

    Public Sub New(ByVal Singledri As Coordination2.CSDriver, ByVal AMFlag As String)
        If AMFlag = "早班" Then
            MDriver = Singledri
        ElseIf AMFlag = "夜班" Then
            ADriver = Singledri
        End If
    End Sub

    Public ReadOnly Property WorkTime As Integer
        Get
            If ADriver Is Nothing AndAlso MDriver IsNot Nothing Then
                Return IIf(MDriver.endtime < 10800, MDriver.endtime + 86400, MDriver.endtime) - IIf(MDriver.startdutytime < 10800, MDriver.startdutytime + 86400, MDriver.startdutytime)
            ElseIf ADriver IsNot Nothing AndAlso MDriver Is Nothing Then
                Return IIf(ADriver.endtime < 10800, ADriver.endtime + 86400, ADriver.endtime) - IIf(ADriver.startdutytime < 10800, ADriver.startdutytime + 86400, ADriver.startdutytime)
            Else
                'Return IIf(ADriver.endtime < 10800, ADriver.endtime + 86400, ADriver.endtime) - IIf(ADriver.startdutytime < 10800, ADriver.startdutytime + 86400, ADriver.startdutytime) _
                '    + IIf(MDriver.endtime < 10800, MDriver.endtime + 86400, MDriver.endtime) - IIf(MDriver.startdutytime < 10800, MDriver.startdutytime + 86400, MDriver.startdutytime)
                Return MDriver.endtime + 86400 - ADriver.startdutytime
            End If
        End Get
    End Property

    Public ReadOnly Property StartTime As Integer
        Get
            If ADriver Is Nothing AndAlso MDriver IsNot Nothing Then
                Return MDriver.starttime
            ElseIf ADriver IsNot Nothing AndAlso MDriver Is Nothing Then
                Return ADriver.starttime
            Else
                Return ADriver.starttime
            End If
        End Get
    End Property

    Public ReadOnly Property EndTime As Integer
        Get
            If ADriver Is Nothing AndAlso MDriver IsNot Nothing Then
                Return MDriver.endtime
            ElseIf ADriver IsNot Nothing AndAlso MDriver Is Nothing Then
                Return ADriver.endtime
            Else
                Return MDriver.endtime
            End If
        End Get
    End Property

    Public ReadOnly Property DriveLength As Double
        Get
            If ADriver Is Nothing AndAlso MDriver IsNot Nothing Then
                Return MDriver.DriveDistance
            ElseIf ADriver IsNot Nothing AndAlso MDriver Is Nothing Then
                Return ADriver.DriveDistance
            Else
                Return ADriver.DriveDistance + MDriver.DriveDistance
            End If
        End Get
    End Property

    Public ReadOnly Property StartStaName As String
        Get
            If ADriver Is Nothing AndAlso MDriver IsNot Nothing Then
                Return MDriver.StartStaName
            ElseIf ADriver IsNot Nothing AndAlso MDriver Is Nothing Then
                Return ADriver.StartStaName
            Else
                Return ADriver.StartStaName
            End If
        End Get
    End Property

    Public ReadOnly Property EndStaName As String
        Get
            If ADriver Is Nothing AndAlso MDriver IsNot Nothing Then
                Return MDriver.OffStaName
            ElseIf ADriver IsNot Nothing AndAlso MDriver Is Nothing Then
                Return ADriver.OffStaName
            Else
                Return MDriver.OffStaName
            End If
        End Get
    End Property

    Public ReadOnly Property AInDepot As String         '夜班回库车场
        Get
            Return ADriver.OffStaName
        End Get
    End Property

    Public ReadOnly Property MOutDepot As String        '白班出库车场
        Get
            Return MDriver.StartStaName
        End Get
    End Property

    Public ReadOnly Property BelongArea As String
        Get
            If ADriver IsNot Nothing Then
                Return ADriver.BelongArea
            ElseIf MDriver IsNot Nothing Then
                Return MDriver.BelongArea
            Else
                Return ""
            End If
        End Get
    End Property

End Class

Public Class AreaYunZhuan              '分区轮转类
    Public LineName As String
    Public AreaName As String
    Public OnDutyPlaces() As String
    Public ForDutySorts() As String
    Public AvaDrivers As List(Of CrewTrainingManager.DriverTeam)
    Public YunZhuanPara As String
    Public AmCons As List(Of AMDutyConnect)
    Public Sub New(ByVal _Linename As String, ByVal _AreaName As String)
        LineName = _Linename
        AreaName = _AreaName
        ReDim OnDutyPlaces(0)
        ReDim ForDutySorts(0)
        AvaDrivers = New List(Of CrewTrainingManager.DriverTeam)
        YunZhuanPara = ""
        AmCons = New List(Of AMDutyConnect)
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