Imports System
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports System.Threading

Public Class frmCSTimeTableMain
    Dim nPubAdjustTrainLineState As Integer '调整状态变量，1为调整发点
    Dim sngPriMoveX1 As Single '调整发点时的X坐标
    Dim sngPriMoveY1 As Single '调整发点时的y坐标
    Dim sngPriMoveX2 As Single '调整发点时的X2坐标
    Dim sngPriMoveY2 As Single '调整发点时的y2坐标
    Dim nSeekLinkTrain As Integer '当前调整车底交路时符合要求的列车号
    Public nIFShowAllCheDiTrain As Integer '是否显示所有车底列车
    Dim intCurPrintPage As Integer '当前打印的页数

    Dim intCurMoveTimeStaID As Integer '当前调整时刻的车站ID号
    Dim intCurMoveTimeID As Integer '当前调整时刻的ID号
    Dim sngForX As Single '原坐标，移动运行线时
    Dim sortColumn As Integer = -1
    Dim nIfPreseCtrl As Integer '多选时，判断是否按住CTRL键
    Dim nGuDaoID As Integer '当前选取中的股道

    Dim nCurSecID As Integer '当前选中的区间编号
    Dim nCurStaID As Integer '当前选中的车站编号

    Structure typeSelectTimeInDragram
        Dim intCurSelectFirTime As Long '测量时间时，当前选中的初始时间
        Dim intCurSelectSecTime As Long '测量时间时，当前选中的终止时间
        Dim FirX As Single
        Dim SecX As Single
        Dim FirY As Single
        Dim SecY As Single

        Dim X1 As Long '多选时
        Dim Y1 As Long
        Dim X2 As Long
        Dim Y2 As Long
    End Structure
    Private SelectTime As typeSelectTimeInDragram

    Structure typeMoveTimeXY
        Dim nSta As Integer
        ' Dim lngTime As Long
        Dim X As Single
        Dim Y As Single
        Dim X2 As Single
        Dim Y2 As Single
    End Structure
    Dim MoveTimeXY() As typeMoveTimeXY

    Structure typeStaStopLine
        Dim nSta As Integer
        Dim sSta As String
        Dim sStopLine As String
    End Structure
    Dim StaStopLine() As typeStaStopLine
  
    Public Sub New(ByVal lineName)
        MyBase.New()
        CurLineName = lineName
        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub
    Public Sub New()
        'CurrentUserRole = "高级管理员"
        CurLineName = "13号线"
        InitializeComponent()
    End Sub
    Private Sub frmTimeTableMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.ControlKey Then
            nIfPreseCtrl = 0
        ElseIf e.KeyCode = Keys.ShiftKey Then
            nIfPreseCtrl = 0
        ElseIf e.KeyCode = Keys.Escape Then
            nPubAdjustTrainLineState = 0
            Me.Cursor = Cursors.Default
            Call Me.PicDiagram.Refresh()
        ElseIf e.KeyCode = Keys.Delete Then
            'Select Case CurrentUserRole
            '    Case "高级管理员"
            Call 删除该乘务员DToolStripMenuItem_Click(Nothing, Nothing)
            '    Case "中心管理员"
            '    Case "车间管理员"
            '    Case "车间运用管理"
            '        Call 删除该乘务员DToolStripMenuItem_Click(Nothing, Nothing)
            '    Case Else
            '        MsgBox("当前用户为非法用户，请联系系统管理员!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
            '        End
            'End Select
        End If
    End Sub

    Private Sub frmTimeTableMain_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.ControlKey Then
            nIfPreseCtrl = 0
        ElseIf e.KeyCode = Keys.ShiftKey Then
            nIfPreseCtrl = 0
        End If
    End Sub


    Private Sub frmCSTimeTableMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '权限管理部分，暂时不要
        'CurrentUserRole = "高级管理员"
        'Call SetRoleMeun()
        Call TempUpdateOracle()
        Me.Focus()
        If strCurlineID = "" OrElse DiagramCurID = "" Then
        Else
            Call CSTimeTableBackGroundFrameSet()

            Call CSRefreshDiagram()
        End If
        Call ClearUnDoAndReDoFile()
    End Sub

    Public Sub TempUpdateOracle()
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String
        Dim tab As System.Data.DataTable

        cellName = "未勾选任务段线型"
        CellValue = "实线"
        Str = "select * from cs_cstimetablesystempara where lineid='" & CurLineName & "' and paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','96','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "未勾选任务段线宽"
        CellValue = 1
        Str = "select * from cs_cstimetablesystempara where lineid='" & CurLineName & "' and paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','97','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "未勾选任务段颜色"
        CellValue = "Gray"
        Str = "select * from cs_cstimetablesystempara where lineid='" & CurLineName & "' and paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','98','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "班中任务线型"
        CellValue = "实线"
        Str = "select * from cs_cstimetablesystempara where lineid='" & CurLineName & "' and paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','99','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "班中任务线宽"
        CellValue = 1
        Str = "select * from cs_cstimetablesystempara where lineid='" & CurLineName & "' and  paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','100','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "班中任务颜色"
        CellValue = "Red"
        Str = "select * from cs_cstimetablesystempara where lineid='" & CurLineName & "' and paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','101','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "用餐任务线型"
        CellValue = "实线"
        Str = "select * from cs_cstimetablesystempara where lineid='" & CurLineName & "' and paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','102','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "用餐任务线宽"
        CellValue = 1
        Str = "select * from cs_cstimetablesystempara where  lineid='" & CurLineName & "' and paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','103','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "用餐任务颜色"
        CellValue = "Maroon"
        Str = "select * from cs_cstimetablesystempara where lineid='" & CurLineName & "' and paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','104','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "班后任务线型"
        CellValue = "实线"
        Str = "select * from cs_cstimetablesystempara where lineid='" & CurLineName & "' and paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','105','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "班后任务线宽"
        CellValue = 1
        Str = "select * from cs_cstimetablesystempara where lineid='" & CurLineName & "' and paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','106','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "班后任务颜色"
        CellValue = "#FF8000"
        Str = "select * from cs_cstimetablesystempara where lineid='" & CurLineName & "' and paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','107','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "任务标号字体名称"
        CellValue = "Tahoma"
        Str = "select * from cs_cstimetablesystempara where lineid='" & CurLineName & "' and paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','108','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "任务标号字体大小"
        CellValue = 7.5
        Str = "select * from cs_cstimetablesystempara where lineid='" & CurLineName & "' and paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','109','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "任务标号字体粗体"
        CellValue = "False"
        Str = "select * from cs_cstimetablesystempara where lineid='" & CurLineName & "' and paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','110','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "任务标号字体斜体"
        CellValue = "False"
        Str = "select * from cs_cstimetablesystempara where lineid='" & CurLineName & "' and paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','111','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "任务标号字体颜色"
        CellValue = "Silver"
        Str = "select * from cs_cstimetablesystempara where lineid='" & CurLineName & "' and paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                CurLineName & "','112','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        End If
        tab.Dispose()
    End Sub

    Public Sub CSTimeTableBackGroundFrameSet()
        Call CSInitfrmCSTimetable() '窗体变量初始化
        Call CSIniteDiagramPicViraient() '底图变量
        SplitDiagram.Panel2.AutoScrollPosition = New System.Drawing.Point(0, 0)
        Me.SplitDiagram.Panel2.VerticalScroll.Enabled = True

        PicDiagram.Width = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
        PicDiagram.Height = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        picStation.Width = CSTimeTablePara.TimeTableDiagramPara.sngPicStationWidth
        picStation.Height = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        PicStation2.Width = CSTimeTablePara.TimeTableDiagramPara.sngPicStationWidth
        PicStation2.Height = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        Me.PicDiagram.Left = 0
        Me.PicDiagram.Top = 0
        Me.picStation.Left = 0
        Me.picStation.Top = 0
        Me.PicStation2.Left = 0
        Me.PicStation2.Top = 0

        CSTimeTablePara.sInputDataError = ""
        Call CSreadStationAndSectionDataFromOracle() '车站与区间信息

        CSTimeTablePara.picPubDiagram = Me.PicDiagram
        CSTimeTablePara.picPubStation = Me.picStation
        CSTimeTablePara.picPubStation2 = Me.PicStation2

        intCurPrintPage = 0
        nPubAdjustTrainLineState = 0 '调整运行线的状态


    End Sub

    Private Sub frmTimeTableMain_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    End Sub

   

    Private Sub 小时格ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 小时格ToolStripMenuItem.Click
        CSTimeTablePara.TimeTableDiagramPara.strTimeFormat = "小时格"
        Call CSRefreshDiagram(0)

    End Sub

    Private Sub 十分格ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 十分格ToolStripMenuItem.Click
        CSTimeTablePara.TimeTableDiagramPara.strTimeFormat = "十分格"
        Call CSRefreshDiagram(0)

    End Sub

    Private Sub 二分格ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 二分格ToolStripMenuItem.Click
        CSTimeTablePara.TimeTableDiagramPara.strTimeFormat = "二分格"
        Call CSRefreshDiagram(0)

    End Sub

    Private Sub 一分格ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 一分格ToolStripMenuItem.Click
        CSTimeTablePara.TimeTableDiagramPara.strTimeFormat = "一分格"
        Call CSRefreshDiagram(0)

    End Sub

    Private Sub 放大底图宽度ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 放大底图宽度ToolStripMenuItem.Click
        Try
            If CSTimeTablePara.picPubDiagram Is Nothing Then
                Exit Sub
            End If
            底图放大ToolStripButton.Enabled = False
            System.Windows.Forms.Application.DoEvents()
            If CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth > 15000 Then
                CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = 15000
            Else
                CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth * 1.2
            End If
            Me.PicDiagram.Width = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
            Me.PicDiagram.Height = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
            Call CSRefreshDiagram(0)
            底图放大ToolStripButton.Enabled = True
        Catch ex As Exception
            MsgBox("操作过于频繁！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
        End Try
    End Sub

    Private Sub 缩小底图宽度ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 缩小底图宽度ToolStripMenuItem.Click
        Try
            If CSTimeTablePara.picPubDiagram Is Nothing Then
                Exit Sub
            End If
            底图缩小ToolStripButton.Enabled = False
            System.Windows.Forms.Application.DoEvents()
            If CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth < 900 Then
                CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = 900
            Else
                CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth / 1.2
            End If
            Me.PicDiagram.Width = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
            Me.PicDiagram.Height = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
            Call CSRefreshDiagram(0)
            底图缩小ToolStripButton.Enabled = True
        Catch ex As Exception
            MsgBox("操作过于频繁！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
        End Try
    End Sub

    Private Sub 小时格ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 小时格ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 十分格ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 十分格ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 二分格ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 二分格ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 一分格ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 一分格ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 底图放大ToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 底图放大ToolStripButton.Click
        Call 放大底图宽度ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 底图缩小ToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 底图缩小ToolStripButton.Click
        缩小底图宽度ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 打开运行图ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 打开运行图ToolStripMenuItem.Click
        Dim nf As New frmCSQTTSelect
        nf.WindowState = FormWindowState.Normal
        nf.ShowDialog()
        If nf.IsOpen = True Then
            Me.labName.Text = "当前乘务计划：" & CStr(strQCurCSPlanName)
            Call MakeParaSet(strQCurCSPlanID)
            Call FiveThreeTurn_FitCSChedi()
            Call CSTimeTableBackGroundFrameSet()
            Call LoadSectionLengthInfo()
            Call ReadCSTableDataFromOracle()
            If CSTrainsAndDrivers.IfCorSchedule Then
                Call ReadCorDriversAndTrains(CSTrainsAndDrivers.CorCSPlanName)
                Call ReadCorPrepareDrivers(CSTrainsAndDrivers.CorCSPlanName)
                Call ReadCorCSPlan()
            End If
            Call LoadAreaInfo()
            Call SetDefaultSize()
            Call CSRefreshDiagram(0)
            Call ListAllViewInfo()

            Dim cslinktrian(0) As CSLinkTrain
        End If
    End Sub


    '判断系统是否已经打开时刻表
    Public Function TimeTableIFOpened() As Integer
        If CSTimeTablePara.sPubCurSkbName = "" Then
            If MsgBox("时刻表没有打开，现在是否打开时刻表!", MsgBoxStyle.Information + MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton1, "确认") = MsgBoxResult.Ok Then
                Call 打开运行图ToolStripMenuItem_Click(Nothing, Nothing)
                TimeTableIFOpened = 0
            Else
                TimeTableIFOpened = 0
            End If
        Else
            TimeTableIFOpened = 1
        End If
    End Function

    Private Sub 退出EToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 退出EToolStripMenuItem.Click
        If MsgBox("确定退出系统吗？", MsgBoxStyle.Information + MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton1, "确认") = MsgBoxResult.Ok Then
            Me.Close()
        End If
    End Sub

    Private Sub 打开运行图ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 打开运行图ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub
    '在标头上显示列车信息
    Public Sub CSShowLabInfor(ByVal nTrain As Integer, ByVal labName As System.Windows.Forms.Label)
        If nTrain > 0 Then
            Dim nDriverID As Integer
            nDriverID = CheCiToDriverID(nTrain)
            If nDriverID > 0 Then
                labName.Text = " ID:" & nTrain & " 车次:" & CSTrainsAndDrivers.CSLinkTrains(nTrain).CSTrainID & " 输出车次:" & CSTrainsAndDrivers.CSLinkTrains(nTrain).OutputCheCi & "| 驾驶乘务员编号:" & CSTrainsAndDrivers.CSDrivers(nDriverID).CSdriverNo _
                                    & " |开始时间:" & BeTime(CSTrainsAndDrivers.CSLinkTrains(nTrain).StartTime) & " |终到时间:" & BeTime(CSTrainsAndDrivers.CSLinkTrains(nTrain).EndTime) & " |开始站名:" & CSTrainsAndDrivers.CSLinkTrains(nTrain).StartStaName & " |终到站名:" & CSTrainsAndDrivers.CSLinkTrains(nTrain).EndStaName & _
                                    " ||输出编号:" & CSTrainsAndDrivers.CSDrivers(nDriverID).OutPutCSdriverNo & " |班种:" & CSTrainsAndDrivers.CSDrivers(nDriverID).DutySort & " |工作时间:" & BeTime(CSTrainsAndDrivers.CSDrivers(nDriverID).WorkTime) & _
                                    " |累计公里:" & CSTrainsAndDrivers.CSDrivers(nDriverID).DriveDistance & " | 当前状态：" & CSTrainsAndDrivers.CSDrivers(nDriverID).State

            Else
                labName.Text = " ID:" & nTrain & " 列车ID:" & CSTrainsAndDrivers.CSLinkTrains(nTrain).CSTrainID & " |上车车次:" & CSTrainsAndDrivers.CSLinkTrains(nTrain).OutputCheCi & " |下车车次:" & CSTrainsAndDrivers.CSLinkTrains(nTrain).OffCheCi & " |开始时间:" & BeTime(CSTrainsAndDrivers.CSLinkTrains(nTrain).StartTime) & " |终到时间:" & BeTime(CSTrainsAndDrivers.CSLinkTrains(nTrain).EndTime) & " |开始站名:" & CSTrainsAndDrivers.CSLinkTrains(nTrain).StartStaName & " |终到站名:" & CSTrainsAndDrivers.CSLinkTrains(nTrain).EndStaName

            End If
        Else
            labName.Text = "在此显示相关信息"
        End If
    End Sub

    Private Sub PicDiagram_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PicDiagram.MouseDown
        sngForX = e.X
        Dim nCurTrain As Integer
        Me.PicDiagram.ContextMenuStrip = Nothing
        If CSTimeTablePara.sCurDiagramState = DiagramState.运行图 Then
            CSTimeTablePara.lngCurMouseDownTime = GetMouseMoveTime(e.X, Me.PicDiagram.Width - CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank * 2)
            If e.Button = System.Windows.Forms.MouseButtons.Left OrElse e.Button = System.Windows.Forms.MouseButtons.Right Then
                Select Case nPubAdjustTrainLineState
                    Case 0 ' '选择列车
                        If nIfPreseCtrl = 1 Then '按住CTRL选择
                        Else
                            nCurTrain = CSSeekTrainNumberByXYCoord(e.X, e.Y, CSTimeTablePara.TimeTableDiagramPara.sngtopBlank, Me.PicDiagram.Height)
                            CSTimeTablePara.nPubTrain = nCurTrain 'MsgBox(nTrain)

                            If nCurTrain > 0 Then
                                Me.PicDiagram.ContextMenuStrip = Me.cmuTrainLine
                                If CSTrainsAndDrivers.CSLinkTrains(nCurTrain).IsLinked Then
                                    安排驾驶员IToolStripMenuItem.Enabled = False
                                Else
                                    安排驾驶员IToolStripMenuItem.Enabled = True
                                End If
                                ReDim CSTimeTablePara.nPubTrains(1)
                                CSTimeTablePara.nPubTrains(1) = nCurTrain
                                CSTimeTablePara.nPubCheDi = CheCiToDriverID(nCurTrain)
                                Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
                                Me.PicDiagram.Refresh()
                                rBmpGraphics = Me.PicDiagram.CreateGraphics()
                                Dim tmpPen As Pen
                                tmpPen = New Pen(Color.Blue, 2)
                                Call CSDrawLineInPicture(CSTrainsAndDrivers.CSLinkTrains(nCurTrain), rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                                nIFShowAllCheDiTrain = 1
                                '显示驾驶员所有的任务
                                If CSTimeTablePara.nPubCheDi > 0 Then
                                    Call DrawDriver(rBmpGraphics, tmpPen, CSTimeTablePara.nPubCheDi)
                                    改变乘务员状态KToolStripMenuItem.Enabled = True
                                    班中ToolStripMenuItem.Enabled = True
                                    用餐ToolStripMenuItem.Enabled = True
                                    班后ToolStripMenuItem.Enabled = True
                                Else
                                    改变乘务员状态KToolStripMenuItem.Enabled = False
                                    班中ToolStripMenuItem.Enabled = False
                                    用餐ToolStripMenuItem.Enabled = False
                                    班后ToolStripMenuItem.Enabled = False
                                End If
                                Call SetAvaDrisMenu()
                                Call SetListView()
                            Else
                                Me.PicDiagram.Refresh()
                            End If
                            Call CSShowLabInfor(nCurTrain, Me.labInfor)
                        End If
                    Case 1 ' '调整发点
                        nPubAdjustTrainLineState = 2 '开始调整
                    Case 4 '调整始发站交路
                        If (Math.Abs(e.X - sngPriMoveX1) <= 8 And Math.Abs(e.Y - sngPriMoveY1) <= 8) Then
                            nPubAdjustTrainLineState = 7 '开始调整始发站交路
                        End If
                    Case 5 ''调整终到站交路
                        If (Math.Abs(e.X - sngPriMoveX2) <= 8 And Math.Abs(e.Y - sngPriMoveY2) <= 8) Then
                            nPubAdjustTrainLineState = 8 '开始调整终到站交路
                        End If
                    Case 6 ''调整终到站交路
                        If (Math.Abs(e.X - sngPriMoveX1) <= 8 And Math.Abs(e.Y - sngPriMoveY1) <= 8) Then
                            nPubAdjustTrainLineState = 7 '开始调整始发站交路
                        End If

                        If (Math.Abs(e.X - sngPriMoveX2) <= 8 And Math.Abs(e.Y - sngPriMoveY2) <= 8) Then
                            nPubAdjustTrainLineState = 8 '开始调整终到站交路
                        End If
                    Case 9 '测量时间第一点
                        ' nPubAdjustTrainLineState = 10
                    Case 12 '多选取
                        SelectTime.X1 = e.X
                        SelectTime.Y1 = e.Y
                End Select
            
            End If
        End If
    End Sub

    Private Sub PicDiagram_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PicDiagram.Paint
        If CSTimeTablePara.sCurDiagramState = DiagramState.运行图 Then
            Dim i As Integer
            Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
            Dim tmpPen As Pen
            rBmpGraphics = Me.PicDiagram.CreateGraphics()
            tmpPen = New Pen(Color.Blue, 2)
            If CSTrainsAndDrivers.CSLinkTrains Is Nothing = False And CSTimeTablePara.nPubTrain > 0 Then
                Call CSDrawLineInPicture(CSTimeTablePara.nPubTrain, rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
            End If

            '显示驾驶员所有的任务
            If nIFShowAllCheDiTrain = 1 AndAlso CSTimeTablePara.nPubTrain > 0 AndAlso CSTimeTablePara.nPubCheDi > 0 AndAlso CSTrainsAndDrivers.CSDrivers Is Nothing = False AndAlso CSTimeTablePara.nPubCheDi <= UBound(CSTrainsAndDrivers.CSDrivers) Then
                Dim tmpPen1 As Pen = New Pen(Color.DarkSlateGray, 2)
                tmpPen1.DashStyle = Drawing2D.DashStyle.DashDot
                For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain)
                    If CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).CSTrainID <> CSTimeTablePara.nPubTrain Then
                        If CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).IsDeadHeading = False Then
                            Call CSDrawLineInPicture(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).CSTrainID, rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                        Else
                            Call CSDrawLineInPicture(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i), rBmpGraphics, tmpPen1, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                        End If
                    End If
                Next
                If CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedMorDriver IsNot Nothing AndAlso CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedMorDriver.IsPrepareDri = False Then
                    For Each train As CSLinkTrain In CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedMorDriver.CSLinkTrain
                        If train IsNot Nothing Then
                            If train.IsDeadHeading = False Then
                                Call CSDrawLineInPicture(train.CSTrainID, rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                            Else
                                Call CSDrawLineInPicture(train, rBmpGraphics, tmpPen1, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                            End If
                        End If
                    Next
                End If
                If CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedNightDriver IsNot Nothing AndAlso CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedNightDriver.IsPrepareDri = False Then
                    For Each train As CSLinkTrain In CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedNightDriver.CSLinkTrain
                        If train IsNot Nothing Then
                            If train.IsDeadHeading = False Then
                                Call CSDrawLineInPicture(train.CSTrainID, rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                            Else
                                Call CSDrawLineInPicture(train, rBmpGraphics, tmpPen1, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                            End If
                        End If
                    Next
                End If
            End If
        End If
        If SplitDiagram.Panel2.AutoScrollPosition.Y < 0 Then
            Me.picStation.Top = SplitDiagram.Panel2.AutoScrollPosition.Y
            Me.PicStation2.Top = SplitDiagram.Panel2.AutoScrollPosition.Y
        End If
        Me.picStation.Top = Me.PicDiagram.Top
        Me.PicStation2.Top = Me.PicDiagram.Top
    End Sub

    '测量时间时查找时间
    Private Sub SeekMoveSelectTime(ByVal CurX As Single, ByVal curY As Single, ByVal nFirOrSec As Integer)
        Dim i As Integer
        Dim maxY As Single
        maxY = 100000000
        Dim maxX As Single
        maxX = 100000000
        Dim tmpX As Single
        Dim nCurSta As Integer
        nCurSta = 0
        For i = 1 To UBound(StationInf)
            If Math.Abs(curY - StationInf(i).YPicValue) < maxY Then
                maxY = Math.Abs(curY - StationInf(i).YPicValue)
                If nFirOrSec = 1 Then
                    SelectTime.FirY = StationInf(i).YPicValue
                Else
                    SelectTime.SecY = StationInf(i).YPicValue
                End If
                nCurSta = i
            End If
        Next i

        If nCurSta > 0 Then
            For i = 1 To UBound(TrainInf)
                With TrainInf(i)
                    If .Train <> "" Then
                        tmpX = FormTimeToXCord(.Starting(nCurSta), CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                        If Math.Abs(tmpX - CurX) < maxX Then
                            If nFirOrSec = 1 Then
                                SelectTime.FirX = tmpX
                                SelectTime.intCurSelectFirTime = .Starting(nCurSta)
                            Else
                                SelectTime.SecX = tmpX
                                SelectTime.intCurSelectSecTime = .Starting(nCurSta)
                            End If
                            maxX = Math.Abs(tmpX - CurX)
                        End If

                        tmpX = FormTimeToXCord(.Arrival(nCurSta), CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                        If Math.Abs(tmpX - CurX) < maxX Then
                            If nFirOrSec = 1 Then
                                SelectTime.FirX = tmpX
                                SelectTime.intCurSelectFirTime = .Arrival(nCurSta)
                            Else
                                SelectTime.SecX = tmpX
                                SelectTime.intCurSelectSecTime = .Arrival(nCurSta)
                            End If
                            maxX = Math.Abs(tmpX - CurX)
                        End If
                    End If
                End With
            Next i
        End If

    End Sub

    '查找满足要求的可以被勾上的列车
    Private Sub CSSeekLinkTrain(ByVal CurX As Single, ByVal nStartOrEnd As Integer)
        Dim sgnRadius As Single
        Dim nTr As Integer
        Dim sTtime As Single
        sgnRadius = 120
        Dim CurX1, CurY1, CurX2, CurY2 As Single
        Dim sngSeekTrainX, sngSeekTrainY As Single
        sTtime = CSGetMouseMoveTime(CurX, Me.PicDiagram.Width - CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank * 2 - CSTimeTablePara.TimeTableDiagramPara.sngStaBlank)
        Me.PicDiagram.Refresh()
        Dim tmpGraphics As Graphics '画线路与车站图的定义的对象
        tmpGraphics = Me.PicDiagram.CreateGraphics()
        'If TrainInf(CSTimeTablePara.npubtrain).TrainReturn(1) = 0 Then
        'Dim tempCSDriverID As Integer
        Dim i, j, flag As Integer
        Dim bloNoDriver As Boolean = False 'False表示还没有安排驾驶员
        If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
            For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers) '找到司机
                If UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain) > 0 Then
                    For j = 1 To UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)
                        If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).CSTrainID = CSTimeTablePara.nPubTrain Then
                            bloNoDriver = True
                            Exit For
                        End If
                    Next
                End If
            Next
        Else
            bloNoDriver = True
        End If


        If bloNoDriver = False Then '没有安排驾驶员
            If nStartOrEnd = 1 Then '始发站
                If CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).UpOrDown = 1 Then  '下行
                    CurX1 = sngPriMoveX1
                    CurY1 = sngPriMoveY1
                    CurX2 = CurX1
                    CurY2 = sngPriMoveY1 - 12
                    tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                    CurX1 = CurX2
                    CurY1 = CurY2
                    CurX2 = CurX
                    CurY2 = CurY1
                    tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                    CurX1 = CurX2
                    CurY1 = CurY2
                    CurX2 = CurX
                    CurY2 = sngPriMoveY1
                    tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)


                    nTr = CSSeekDriverLinkTrain(CSTimeTablePara.nPubTrain, 1, sTtime)
                    If nTr <> 0 Then
                        sngSeekTrainX = FormTimeToXCord(CSTrainsAndDrivers.CSLinkTrains(nTr).EndTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                        sngSeekTrainY = CSLinkStationInf(CSTrainsAndDrivers.CSLinkTrains(nTr).EndStaID).YPicValue
                        tmpGraphics.DrawEllipse(Pens.Blue, sngSeekTrainX - 8, sngSeekTrainY - 8, 16, 16)
                        nSeekLinkTrain = nTr
                    End If
                Else '上行
                    CurX1 = sngPriMoveX1
                    CurY1 = sngPriMoveY1
                    CurX2 = CurX1
                    CurY2 = sngPriMoveY1 + 12
                    tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                    CurX1 = CurX2
                    CurY1 = CurY2
                    CurX2 = CurX
                    CurY2 = CurY1
                    tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                    CurX1 = CurX2
                    CurY1 = CurY2
                    CurX2 = CurX
                    CurY2 = sngPriMoveY1
                    tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                    nTr = CSSeekDriverLinkTrain(CSTimeTablePara.nPubTrain, 3, sTtime)
                    If nTr <> 0 Then
                        sngSeekTrainX = FormTimeToXCord(CSTrainsAndDrivers.CSLinkTrains(nTr).EndTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                        sngSeekTrainY = CSLinkStationInf(CSTrainsAndDrivers.CSLinkTrains(nTr).EndStaID).YPicValue
                        tmpGraphics.DrawEllipse(Pens.Blue, sngSeekTrainX - 8, sngSeekTrainY - 8, 16, 16)
                        nSeekLinkTrain = nTr
                    End If
                End If
            Else '终到站
                If CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).UpOrDown = 1 Then  '下行
                    CurX1 = sngPriMoveX2
                    CurY1 = sngPriMoveY2
                    CurX2 = CurX1
                    CurY2 = sngPriMoveY2 + 12
                    tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                    CurX1 = CurX2
                    CurY1 = CurY2
                    CurX2 = CurX
                    CurY2 = CurY1
                    tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                    CurX1 = CurX2
                    CurY1 = CurY2
                    CurX2 = CurX
                    CurY2 = sngPriMoveY2
                    tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)

                    nTr = CSSeekDriverLinkTrain(CSTimeTablePara.nPubTrain, 2, sTtime) '找到的列车
                    If nTr <> 0 Then
                        sngSeekTrainX = FormTimeToXCord(CSTrainsAndDrivers.CSLinkTrains(nTr).StartTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                        sngSeekTrainY = CSLinkStationInf(CSTrainsAndDrivers.CSLinkTrains(nTr).StartStaID).YPicValue
                        tmpGraphics.DrawEllipse(Pens.Blue, sngSeekTrainX - 8, sngSeekTrainY - 8, 16, 16)
                        nSeekLinkTrain = nTr
                    End If
                Else '上行
                    CurX1 = sngPriMoveX2
                    CurY1 = sngPriMoveY2
                    CurX2 = CurX1
                    CurY2 = sngPriMoveY2 - 12
                    tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                    CurX1 = CurX2
                    CurY1 = CurY2
                    CurX2 = CurX
                    CurY2 = CurY1
                    tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                    CurX1 = CurX2
                    CurY1 = CurY2
                    CurX2 = CurX
                    CurY2 = sngPriMoveY2
                    tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                    nTr = CSSeekDriverLinkTrain(CSTimeTablePara.nPubTrain, 4, sTtime)
                    If nTr <> 0 Then
                        sngSeekTrainX = FormTimeToXCord(CSTrainsAndDrivers.CSLinkTrains(nTr).StartTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                        sngSeekTrainY = CSLinkStationInf(CSTrainsAndDrivers.CSLinkTrains(nTr).StartStaID).YPicValue
                        tmpGraphics.DrawEllipse(Pens.Blue, sngSeekTrainX - 8, sngSeekTrainY - 8, 16, 16)
                        nSeekLinkTrain = nTr
                    End If
                End If
            End If

        Else '有司机驾驶
            If nStartOrEnd = 1 Then '始发站
                If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
                    For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers) '找到司机,看是不是第一个任务
                        If UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain) > 0 AndAlso CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).CSTrainID = CSTimeTablePara.nPubTrain Then
                            flag = i
                            Exit For
                        End If
                    Next

                    If flag > 0 Then
                        If CSTrainsAndDrivers.CSDrivers(flag).CSLinkTrain(1).CSTrainID = CSTimeTablePara.nPubTrain Then
                            If CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).UpOrDown = 1 Then  '下行
                                CurX1 = sngPriMoveX1
                                CurY1 = sngPriMoveY1
                                CurX2 = CurX1
                                CurY2 = sngPriMoveY1 - 12
                                tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                                CurX1 = CurX2
                                CurY1 = CurY2
                                CurX2 = CurX
                                CurY2 = CurY1
                                tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                                CurX1 = CurX2
                                CurY1 = CurY2
                                CurX2 = CurX
                                CurY2 = sngPriMoveY1
                                tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)

                                nTr = CSSeekDriverLinkTrain(CSTimeTablePara.nPubTrain, 1, sTtime)
                                If nTr <> 0 Then
                                    sngSeekTrainX = FormTimeToXCord(CSTrainsAndDrivers.CSLinkTrains(nTr).EndTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                                    sngSeekTrainY = CSLinkStationInf(CSTrainsAndDrivers.CSLinkTrains(nTr).EndStaID).YPicValue
                                    tmpGraphics.DrawEllipse(Pens.Blue, sngSeekTrainX - 8, sngSeekTrainY - 8, 16, 16)
                                    nSeekLinkTrain = nTr
                                End If
                            Else '上行始发
                                CurX1 = sngPriMoveX1
                                CurY1 = sngPriMoveY1
                                CurX2 = CurX1
                                CurY2 = sngPriMoveY1 + 12
                                tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                                CurX1 = CurX2
                                CurY1 = CurY2
                                CurX2 = CurX
                                CurY2 = CurY1
                                tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                                CurX1 = CurX2
                                CurY1 = CurY2
                                CurX2 = CurX
                                CurY2 = sngPriMoveY1
                                tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                                nTr = CSSeekDriverLinkTrain(CSTimeTablePara.nPubTrain, 3, sTtime)
                                If nTr <> 0 Then
                                    sngSeekTrainX = FormTimeToXCord(CSTrainsAndDrivers.CSLinkTrains(nTr).EndTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                                    sngSeekTrainY = CSLinkStationInf(CSTrainsAndDrivers.CSLinkTrains(nTr).EndStaID).YPicValue
                                    tmpGraphics.DrawEllipse(Pens.Blue, sngSeekTrainX - 8, sngSeekTrainY - 8, 16, 16)
                                    nSeekLinkTrain = nTr
                                End If
                            End If
                        End If
                    End If
                End If
            Else
                If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
                    For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                        If UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain) > 0 Then
                            If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)).CSTrainID = CSTimeTablePara.nPubTrain Then
                                flag = i
                                Exit For
                            End If
                        End If

                    Next
                    If flag > 0 Then

                        If CSTrainsAndDrivers.CSDrivers(flag).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(flag).CSLinkTrain)).CSTrainID = CSTimeTablePara.nPubTrain Then
                            If CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).UpOrDown = 1 Then '下行末站
                                CurX1 = sngPriMoveX2
                                CurY1 = sngPriMoveY2
                                CurX2 = CurX1
                                CurY2 = sngPriMoveY2 + 12
                                tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                                CurX1 = CurX2
                                CurY1 = CurY2
                                CurX2 = CurX
                                CurY2 = CurY1
                                tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                                CurX1 = CurX2
                                CurY1 = CurY2
                                CurX2 = CurX
                                CurY2 = sngPriMoveY2
                                tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                                nTr = CSSeekDriverLinkTrain(CSTimeTablePara.nPubTrain, 2, sTtime)
                                If nTr <> 0 Then
                                    sngSeekTrainX = FormTimeToXCord(CSTrainsAndDrivers.CSLinkTrains(nTr).StartTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                                    sngSeekTrainY = CSLinkStationInf(CSTrainsAndDrivers.CSLinkTrains(nTr).StartStaID).YPicValue

                                    'sngSeekTrainX = GetTrainrStartXCoord(nTr, TrainInf(nTr).nPathID(1))
                                    'sngSeekTrainY = StationInf(TrainInf(nTr).nPathID(1)).YPicValue
                                    tmpGraphics.DrawEllipse(Pens.Blue, sngSeekTrainX - 8, sngSeekTrainY - 8, 16, 16)
                                    nSeekLinkTrain = nTr
                                End If
                            Else '上行末站
                                CurX1 = sngPriMoveX2
                                CurY1 = sngPriMoveY2
                                CurX2 = CurX1
                                CurY2 = sngPriMoveY2 - 12
                                tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                                CurX1 = CurX2
                                CurY1 = CurY2
                                CurX2 = CurX
                                CurY2 = CurY1
                                tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                                CurX1 = CurX2
                                CurY1 = CurY2
                                CurX2 = CurX
                                CurY2 = sngPriMoveY2
                                tmpGraphics.DrawLine(Pens.Blue, CurX1, CurY1, CurX2, CurY2)
                                nTr = CSSeekDriverLinkTrain(CSTimeTablePara.nPubTrain, 4, sTtime)
                                If nTr <> 0 Then
                                    sngSeekTrainX = FormTimeToXCord(CSTrainsAndDrivers.CSLinkTrains(nTr).StartTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                                    sngSeekTrainY = CSLinkStationInf(CSTrainsAndDrivers.CSLinkTrains(nTr).StartStaID).YPicValue

                                    'sngSeekTrainX = GetTrainArriXCoord(nTr, TrainInf(nTr).nPathID(1))
                                    'sngSeekTrainY = StationInf(TrainInf(nTr).nPathID(1)).YPicValue
                                    tmpGraphics.DrawEllipse(Pens.Blue, sngSeekTrainX - 8, sngSeekTrainY - 8, 16, 16)
                                    nSeekLinkTrain = nTr
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    '查找满足要求的列车
    Public Function CSSeekDriverLinkTrain(ByVal nTrain As Integer, ByVal nNum As Integer, ByVal sTempTime As Single) As Integer
        'Nnum 表示几种类型的列车，1表示左上行的车，2表示右上，3表示左下，4表示右下
        Dim i, j As Integer
        Dim sTime As Single
        Dim sStime As Single
        CSSeekDriverLinkTrain = 0
        sTempTime = CSAddTimeOver24(sTempTime)
        Select Case nNum
            Case 1 '下行起始
                For i = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
                    If (CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown <> 1 And CSTrainsAndDrivers.CSLinkTrains(i).EndStaID = CSTrainsAndDrivers.CSLinkTrains(nTrain).StartStaID) _
                    Or (CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown <> 1 And CSTrainsAndDrivers.CSLinkTrains(i).EndStaName = CSTrainsAndDrivers.CSLinkTrains(nTrain).StartStaName) _
                    Or (CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown = 1 And CSTrainsAndDrivers.CSLinkTrains(i).EndStaID = CSTrainsAndDrivers.CSLinkTrains(nTrain).StartStaID) Then 'or后面是针对实际上断开的列车的整合
                        sTime = CSAddTimeOver24(CSTrainsAndDrivers.CSLinkTrains(i).EndTime)
                        If Math.Abs(sTempTime - sTime) <= 100 Then
                            sStime = CSAddTimeOver24(CSTrainsAndDrivers.CSLinkTrains(nTrain).StartTime)
                            If sStime - sTime >= 0 Then
                                For j = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                                    If UBound(CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain) >= 1 Then
                                        If CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain)).CSTrainID = CSTrainsAndDrivers.CSLinkTrains(i).CSTrainID Then '确保是一个司机的最后一列车
                                            CSSeekDriverLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    End If
                Next i
            Case 2 '下行末站
                For i = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
                    If (CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown <> 1 And CSTrainsAndDrivers.CSLinkTrains(i).StartStaID = CSTrainsAndDrivers.CSLinkTrains(nTrain).EndStaID) _
                    Or (CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown <> 1 And CSTrainsAndDrivers.CSLinkTrains(i).StartStaName = CSTrainsAndDrivers.CSLinkTrains(nTrain).EndStaName) _
                     Or (CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown = 1 And CSTrainsAndDrivers.CSLinkTrains(i).StartStaID = CSTrainsAndDrivers.CSLinkTrains(nTrain).EndStaID) Then
                        sTime = CSAddTimeOver24(CSTrainsAndDrivers.CSLinkTrains(i).StartTime)
                        If Math.Abs(sTempTime - sTime) <= 100 Then
                            sStime = CSAddTimeOver24(CSTrainsAndDrivers.CSLinkTrains(nTrain).EndTime)
                            If sTime - sStime >= 0 Then
                                For j = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                                    If UBound(CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain) >= 1 Then
                                        If CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain(1).CSTrainID = CSTrainsAndDrivers.CSLinkTrains(i).CSTrainID Then '确保是一个司机的第一列车
                                            CSSeekDriverLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    End If
                Next i
            Case 3 '上行起始
                For i = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
                    If (CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown = 1 And CSTrainsAndDrivers.CSLinkTrains(i).EndStaID = CSTrainsAndDrivers.CSLinkTrains(nTrain).StartStaID) _
                    Or (CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown = 1 And CSTrainsAndDrivers.CSLinkTrains(i).EndStaName = CSTrainsAndDrivers.CSLinkTrains(nTrain).StartStaName) _
                     Or (CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown <> 1 And CSTrainsAndDrivers.CSLinkTrains(i).EndStaID = CSTrainsAndDrivers.CSLinkTrains(nTrain).StartStaID) Then
                        sTime = CSAddTimeOver24(CSTrainsAndDrivers.CSLinkTrains(i).EndTime)
                        If Math.Abs(sTempTime - sTime) <= 100 Then
                            sStime = CSAddTimeOver24(CSTrainsAndDrivers.CSLinkTrains(nTrain).StartTime)
                            If sStime - sTime >= 0 Then
                                For j = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                                    If UBound(CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain) >= 1 Then
                                        If CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain)).CSTrainID = CSTrainsAndDrivers.CSLinkTrains(i).CSTrainID Then '确保是一个司机的最后一列车
                                            CSSeekDriverLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    End If
                Next i
            Case 4 '上行末站
                For i = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
                    If (CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown = 1 And CSTrainsAndDrivers.CSLinkTrains(i).StartStaID = CSTrainsAndDrivers.CSLinkTrains(nTrain).EndStaID) _
                    Or (CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown = 1 And CSTrainsAndDrivers.CSLinkTrains(i).StartStaName = CSTrainsAndDrivers.CSLinkTrains(nTrain).EndStaName) _
                    Or (CSTrainsAndDrivers.CSLinkTrains(i).UpOrDown <> 1 And CSTrainsAndDrivers.CSLinkTrains(i).StartStaID = CSTrainsAndDrivers.CSLinkTrains(nTrain).EndStaID) Then
                        sTime = CSAddTimeOver24(CSTrainsAndDrivers.CSLinkTrains(i).StartTime)
                        If Math.Abs(sTempTime - sTime) <= 100 Then
                            sStime = CSAddTimeOver24(CSTrainsAndDrivers.CSLinkTrains(nTrain).EndTime)
                            If sTime - sStime >= 0 Then
                                For j = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                                    If UBound(CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain) >= 1 Then
                                        If CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain(1).CSTrainID = CSTrainsAndDrivers.CSLinkTrains(i).CSTrainID Then '确保是一个司机的最后一列车
                                            CSSeekDriverLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                Next
                            End If
                        End If
                    End If
                Next i
        End Select
    End Function

    Private Sub 调整发点RToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim tmpX, tmpY As Single
        ReDim MoveTimeXY(0)
        If CSTimeTablePara.nPubTrain > 0 Then
            nPubAdjustTrainLineState = 1 '调整发点

            Dim tmpGraphics As Graphics '画线路与车站图的定义的对象
            tmpGraphics = Me.PicDiagram.CreateGraphics()
            ReDim MoveTimeXY(UBound(TrainInf(CSTimeTablePara.nPubTrain).nPathID))
            For i = 1 To UBound(TrainInf(CSTimeTablePara.nPubTrain).nPathID)
                tmpX = GetTrainrStartXCoord(CSTimeTablePara.nPubTrain, TrainInf(CSTimeTablePara.nPubTrain).nPathID(i))
                tmpY = StationInf(TrainInf(CSTimeTablePara.nPubTrain).nPathID(i)).YPicValue
                MoveTimeXY(i).nSta = TrainInf(CSTimeTablePara.nPubTrain).nPathID(i)
                MoveTimeXY(i).X = tmpX
                MoveTimeXY(i).Y = tmpY
                tmpX = GetTrainrStartXCoord(CSTimeTablePara.nPubTrain, TrainInf(CSTimeTablePara.nPubTrain).nPathID(i))
                MoveTimeXY(i).X2 = tmpX
                MoveTimeXY(i).Y2 = tmpY
                tmpGraphics.DrawEllipse(Pens.Blue, tmpX - 8, tmpY - 8, 16, 16)
            Next
        End If

    End Sub

    Private Sub PicDiagram_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PicDiagram.MouseUp
        If CSTimeTablePara.sCurDiagramState = DiagramState.运行图 Then
            Dim nDriverID1 As Integer
            Dim nDriverID2 As Integer

            If e.Button = System.Windows.Forms.MouseButtons.Left Then
                Select Case nPubAdjustTrainLineState
                    Case 7 '调整始发站或终到站交路
                        If CSTimeTablePara.nPubTrain > 0 Then
                            nPubAdjustTrainLineState = 0
                            Me.PicDiagram.Cursor = Cursors.Default
                            If nSeekLinkTrain <> 0 Then
                                nDriverID2 = CheCiToDriverID(CSTimeTablePara.nPubTrain)
                                nDriverID1 = CheCiToDriverID(nSeekLinkTrain)
                                If nDriverID1 <> 0 Then
                                    Call AddUnReDoInfo(True)
                                    Call HeBinDriverInfo(nDriverID1, nDriverID2, nSeekLinkTrain, CSTimeTablePara.nPubTrain)
                                    Call CSRefreshDiagram()
                                    Call ListAllViewInfo()
                                End If
                            End If
                            'nPubAdjustTrainLineState = 0
                            Me.PicDiagram.Refresh()
                            Call Me.PicDiagram_Paint(Nothing, Nothing)
                        End If
                    Case 8 '调整始发站或终到站交路
                        If CSTimeTablePara.nPubTrain > 0 Then
                            nPubAdjustTrainLineState = 0
                            Me.PicDiagram.Cursor = Cursors.Default
                            If nSeekLinkTrain <> 0 Then
                                nDriverID2 = CheCiToDriverID(CSTimeTablePara.nPubTrain)
                                nDriverID1 = CheCiToDriverID(nSeekLinkTrain)
                                If nDriverID1 <> 0 Then 'And nDriverID2 <> 0 Then
                                    Call AddUnReDoInfo(True)
                                    Call HeBinDriverInfo(nDriverID1, nDriverID2, nSeekLinkTrain, CSTimeTablePara.nPubTrain)
                                    Call CSRefreshDiagram()
                                    Call ListAllViewInfo()
                                End If
                            End If
                            'nPubAdjustTrainLineState = 0
                            Me.PicDiagram.Refresh()
                            Call Me.PicDiagram_Paint(Nothing, Nothing)
                        End If
                End Select
                Call CheckUnDoAndReDoState()
            End If
        End If
    End Sub
    Private Function GetAnoTrain(ByVal nCurTrain As Integer, ByVal nCurSta As Integer) As Integer
        If StationInf(nCurSta).sStationName = TrainInf(nCurTrain).ComeStation Then
            GetAnoTrain = TrainInf(nCurTrain).TrainReturn(1)
        ElseIf StationInf(nCurSta).sStationName = TrainInf(nCurTrain).NextStation Then
            GetAnoTrain = TrainInf(nCurTrain).TrainReturn(2)
        Else
            GetAnoTrain = 0
        End If
    End Function

    Private Sub 断开驾驶员任务DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 断开驾驶员任务DToolStripMenuItem.Click
        If CSTimeTablePara.nPubTrain <> 0 Then
            Dim CSDiverID As Integer
            CSDiverID = CheCiToDriverID(CSTimeTablePara.nPubTrain)
            If CSDiverID <> 0 Then
                Dim temmer As MergedCSLinkTrain = GetMergedTrain(CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain))
                If temmer.CSLinkTrains(1).CSTrainID <> CSTimeTablePara.nPubTrain Then
                    MsgBox("任务不能在此处断开！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
                    Exit Sub
                End If
                If CSTrainsAndDrivers.CSDrivers(CSDiverID).CSLinkTrain(1).CSTrainID = temmer.CSLinkTrains(1).CSTrainID Then
                    MsgBox("该驾驶任务是司机的第一个任务，不能被断开！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
                    Exit Sub
                Else
                    If MsgBox("确定断开该司机的驾驶任务吗？", vbQuestion + vbYesNo + vbDefaultButton2, "确认操作") = vbYes Then
                        Call AddUnReDoInfo(True)
                        Call DeleteDriverLink(CSTimeTablePara.nPubTrain, CSDiverID)
                        CSTimeTablePara.nPubCheDi = 0
                        CSTrainsAndDrivers.CSDrivers(CSDiverID).RefreshState()
                        Call CSRefreshDiagram()
                        Call ListAllViewInfo()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub 修改驾驶员连接方式EToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改驾驶员连接方式EToolStripMenuItem.Click
        If CSTimeTablePara.nPubTrain <> 0 Then
            Dim CSDiverID As Integer
            CSDiverID = CheCiToDriverID(CSTimeTablePara.nPubTrain)
            If CSDiverID <> 0 Then
                If CSTrainsAndDrivers.CSDrivers(CSDiverID).CSLinkTrain(1).CSTrainID = CSTimeTablePara.nPubTrain Or CSTrainsAndDrivers.CSDrivers(CSDiverID).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSDiverID).CSLinkTrain)).CSTrainID = CSTimeTablePara.nPubTrain Then
                    Dim tmpGraphics As Graphics '画线路与车站图的定义的对象
                    tmpGraphics = Me.PicDiagram.CreateGraphics()
                    If CSTrainsAndDrivers.CSDrivers(CSDiverID).CSLinkTrain(1).CSTrainID = CSTimeTablePara.nPubTrain Then
                        sngPriMoveX1 = FormTimeToXCord(CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).StartTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX) 'GetTrainrStartXCoord(CSTimeTablePara.nPubTrain, CSLinkTrain(CSTimeTablePara.nPubTrain).StartTime)
                        sngPriMoveY1 = CSLinkStationInf(CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).StartStaID).YPicValue
                        tmpGraphics.DrawEllipse(Pens.Blue, sngPriMoveX1 - 8, sngPriMoveY1 - 8, 16, 16)
                        nPubAdjustTrainLineState = 4 '调整始发
                    End If

                    If CSTrainsAndDrivers.CSDrivers(CSDiverID).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSDiverID).CSLinkTrain)).CSTrainID = CSTimeTablePara.nPubTrain Then
                        sngPriMoveX2 = FormTimeToXCord(CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).EndTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX) 'GetTrainArriXCoord(CSTimeTablePara.nPubTrain, TrainInf(CSTimeTablePara.nPubTrain).nPathID(UBound(TrainInf(CSTimeTablePara.nPubTrain).nPathID)))
                        sngPriMoveY2 = CSLinkStationInf(CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).EndStaID).YPicValue
                        tmpGraphics.DrawEllipse(Pens.Blue, sngPriMoveX2 - 8, sngPriMoveY2 - 8, 16, 16)
                        If nPubAdjustTrainLineState = 4 Then
                            nPubAdjustTrainLineState = 6 '两头都能调整
                        Else
                            nPubAdjustTrainLineState = 5 '调整终到
                        End If
                    End If

                Else
                    MsgBox("该驾驶任务已经被安排，不能调整！")
                End If
            Else '没有乘务员开行该列车
                Dim tmpGraphics As Graphics '画线路与车站图的定义的对象
                tmpGraphics = Me.PicDiagram.CreateGraphics()
                sngPriMoveX1 = FormTimeToXCord(CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).StartTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX) 'GetTrainrStartXCoord(CSTimeTablePara.nPubTrain, CSLinkTrain(CSTimeTablePara.nPubTrain).StartTime)
                sngPriMoveY1 = CSLinkStationInf(CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).StartStaID).YPicValue
                tmpGraphics.DrawEllipse(Pens.Blue, sngPriMoveX1 - 8, sngPriMoveY1 - 8, 16, 16)
                sngPriMoveX2 = FormTimeToXCord(CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).EndTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX) 'GetTrainrStartXCoord(CSTimeTablePara.nPubTrain, CSLinkTrain(CSTimeTablePara.nPubTrain).StartTime)
                sngPriMoveY2 = CSLinkStationInf(CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).EndStaID).YPicValue
                tmpGraphics.DrawEllipse(Pens.Blue, sngPriMoveX2 - 8, sngPriMoveY2 - 8, 16, 16)
                nPubAdjustTrainLineState = 6 '两头都能调整
            End If
        End If
    End Sub

    Private Sub 显示驾驶员所有列车SToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CSTimeTablePara.nPubCheDi > 0 Then
            nIFShowAllCheDiTrain = 1
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 打开ToolStripButton1.Click
        Call 打开运行图ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 刷新乘务交路图ToolStripButton3.Click
        Call CSRefreshDiagram(0)
        Call ListAllViewInfo()
    End Sub


    Private Sub 系统设置SToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 系统设置SToolStripMenuItem.Click
        Dim nf As New frmCSTimeTablePara
        nf.sCurParaState = "运行图系统参数"
        nf.ShowDialog()
    End Sub

    Private Sub 纵向放大底图ZToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 纵向放大底图ZToolStripMenuItem.Click
        Try
            If CSTimeTablePara.picPubDiagram Is Nothing Then
                Exit Sub
            End If
            If CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight > 2000 Then
                CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = 2000
            Else
                CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight + 100
            End If
            Me.PicDiagram.Width = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
            Me.PicDiagram.Height = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
            Me.picStation.Height = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
            Me.PicStation2.Height = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
            Call CSRefreshDiagram(0)
        Catch ex As Exception
            MsgBox("操作过于频繁！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
        End Try
    End Sub

    Private Sub 纵向缩小底图XToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 纵向缩小底图XToolStripMenuItem.Click
        Try
            If CSTimeTablePara.picPubDiagram Is Nothing Then
                Exit Sub
            End If
            If CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight <= 500 Then
                CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = 500
            Else
                CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight - 100
            End If
            Me.PicDiagram.Width = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
            Me.PicDiagram.Height = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
            Me.picStation.Height = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
            Me.PicStation2.Height = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
            Call CSRefreshDiagram(0)
        Catch ex As Exception
            MsgBox("操作过于频繁！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
        End Try
    End Sub

    Private Sub ToolStripButton6_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 纵向放大ToolStripButton6.Click
        Call 纵向放大底图ZToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripButton8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 纵向缩小ToolStripButton8.Click
        Call 纵向缩小底图XToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 线型与颜色LToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nf As New frmDiagramLineAndFontSet
        nf.TabControl1.SelectedIndex = 1
        nf.Show()
    End Sub

    Private Sub InputCopyChedinInf()
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim nIn As Integer
        ReDim Preserve copyChediInf(0)
        For i = 1 To UBound(ChediInfo)
            nIn = 0
            For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                For k = 1 To UBound(CSTimeTablePara.nPubTrains)
                    If ChediInfo(i).nLinkTrain(j) = CSTimeTablePara.nPubTrains(k) Then
                        If nIn = 0 Then
                            ReDim Preserve copyChediInf(UBound(copyChediInf) + 1)
                            ReDim copyChediInf(UBound(copyChediInf)).nLinkTrain(1)
                            copyChediInf(UBound(copyChediInf)).nLinkTrain(1) = k
                            nIn = 1
                        Else
                            ReDim Preserve copyChediInf(UBound(copyChediInf)).nLinkTrain(UBound(copyChediInf(UBound(copyChediInf)).nLinkTrain) + 1)
                            copyChediInf(UBound(copyChediInf)).nLinkTrain(UBound(copyChediInf(UBound(copyChediInf)).nLinkTrain)) = k
                        End If
                        Exit For
                    End If
                Next
            Next
        Next
    End Sub

    Private Sub 底图线型与颜色CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 底图线型与颜色CToolStripMenuItem.Click
        Dim nf As New frmDiagramLineAndFontSet
        nf.TabControl1.SelectedIndex = 0
        nf.Show()
    End Sub

    Private Sub 线型与颜色ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 线型与颜色LToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 鼠标平移MToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim tmpX, tmpY As Single
        ReDim MoveTimeXY(0)
        If CSTimeTablePara.nPubTrain > 0 Then
            nPubAdjustTrainLineState = 11 '平移列车
            Dim tmpGraphics As Graphics '画线路与车站图的定义的对象
            tmpGraphics = Me.PicDiagram.CreateGraphics()
            ReDim MoveTimeXY(UBound(TrainInf(CSTimeTablePara.nPubTrain).nPathID))
            For i = 1 To UBound(TrainInf(CSTimeTablePara.nPubTrain).nPathID)
                tmpX = GetTrainrStartXCoord(CSTimeTablePara.nPubTrain, TrainInf(CSTimeTablePara.nPubTrain).nPathID(i))
                tmpY = StationInf(TrainInf(CSTimeTablePara.nPubTrain).nPathID(i)).YPicValue
                MoveTimeXY(i).X = tmpX
                MoveTimeXY(i).Y = tmpY
                tmpX = GetTrainArriXCoord(CSTimeTablePara.nPubTrain, TrainInf(CSTimeTablePara.nPubTrain).nPathID(i))
                MoveTimeXY(i).X2 = tmpX
                MoveTimeXY(i).Y2 = tmpY
            Next
        End If
    End Sub
    Private Sub 秒格RToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CSTimeTablePara.TimeTableDiagramPara.strTimeFormat = "15秒格"
        Call CSRefreshDiagram()
    End Sub

    Private Sub 全选AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        ReDim CSTimeTablePara.nPubTrains(0)
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                ReDim Preserve CSTimeTablePara.nPubTrains(UBound(CSTimeTablePara.nPubTrains) + 1)
                CSTimeTablePara.nPubTrains(UBound(CSTimeTablePara.nPubTrains)) = i
            End If
        Next
    End Sub
    Private Sub 修改车次编号EToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub 向左翻页ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime = CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime - CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime + 1
        If CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime < 0 Then
            CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime += 24
        End If
        Call CSRefreshDiagram()
    End Sub

    Private Sub 向右翻页ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime = CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime + CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime - 1
        If CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime > 24 Then
            CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime -= 24
        End If
        Call CSRefreshDiagram()

    End Sub

    Private Sub ToolStripLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 向左翻页ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 向右翻页ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub PicDiagram_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PicDiagram.MouseMove
        Dim i As Integer
        Dim tmpGraphics As Graphics '画线路与车站图的定义的对象

        If CSTimeTablePara.sCurDiagramState = DiagramState.运行图 Then
            nSeekLinkTrain = 0

            Select Case nPubAdjustTrainLineState
                Case 0
                Case 1 '调整发点
                    If CSTimeTablePara.nPubTrain > 0 Then
                        For i = 1 To UBound(MoveTimeXY)
                            If Math.Abs(e.X - MoveTimeXY(i).X) <= 8 And Math.Abs(e.Y - MoveTimeXY(i).Y) <= 8 Then
                                Me.PicDiagram.Cursor = Cursors.Cross
                                intCurMoveTimeStaID = MoveTimeXY(i).nSta
                                intCurMoveTimeID = i
                                ' nPubAdjustTrainLineState = 2
                                Exit For
                            Else
                                Me.PicDiagram.Cursor = Cursors.Default
                            End If
                        Next
                    End If

                Case 2 '开始调整发点
                    If CSTimeTablePara.nPubTrain > 0 Then
                        If e.Button = System.Windows.Forms.MouseButtons.Left And Me.PicDiagram.Cursor = Cursors.Cross Then
                            If intCurMoveTimeID < UBound(MoveTimeXY) Then
                                Me.PicDiagram.Refresh()
                                tmpGraphics = Me.PicDiagram.CreateGraphics()
                                tmpGraphics.DrawLine(Pens.Blue, e.X, MoveTimeXY(intCurMoveTimeID).Y, MoveTimeXY(intCurMoveTimeID + 1).X2, MoveTimeXY(intCurMoveTimeID + 1).Y2)
                            End If
                        End If
                    End If
                Case 4 ''调整始发站交路
                    If CSTimeTablePara.nPubTrain > 0 Then
                        If (Math.Abs(e.X - sngPriMoveX1) <= 8 And Math.Abs(e.Y - sngPriMoveY1) <= 8) Then
                            Me.PicDiagram.Cursor = Cursors.Cross
                        Else
                            Me.PicDiagram.Cursor = Cursors.Default
                        End If
                    End If
                Case 5 '调整终到站交路
                    If CSTimeTablePara.nPubTrain > 0 Then
                        If (Math.Abs(e.X - sngPriMoveX2) <= 8 And Math.Abs(e.Y - sngPriMoveY2) <= 8) Then
                            Me.PicDiagram.Cursor = Cursors.Cross
                        Else
                            Me.PicDiagram.Cursor = Cursors.Default
                        End If
                    End If
                Case 6 '调整交路，两头都可调整
                    If CSTimeTablePara.nPubTrain > 0 Then
                        If (Math.Abs(e.X - sngPriMoveX1) <= 8 And Math.Abs(e.Y - sngPriMoveY1) <= 8) Or (Math.Abs(e.X - sngPriMoveX2) <= 8 And Math.Abs(e.Y - sngPriMoveY2) <= 8) Then
                            Me.PicDiagram.Cursor = Cursors.Cross
                        Else
                            Me.PicDiagram.Cursor = Cursors.Default
                        End If
                    End If

                Case 7 '调整始发站交路
                    If CSTimeTablePara.nPubTrain > 0 Then
                        If e.Button = System.Windows.Forms.MouseButtons.Left And Me.PicDiagram.Cursor = Cursors.Cross Then
                            Call CSSeekLinkTrain(e.X, 1)
                        End If
                    End If
                Case 8 '调整终到站交路
                    If CSTimeTablePara.nPubTrain > 0 Then
                        If e.Button = System.Windows.Forms.MouseButtons.Left And Me.PicDiagram.Cursor = Cursors.Cross Then
                            Call CSSeekLinkTrain(e.X, 2)
                        End If
                    End If
                Case 9 '测量时间第一点
                    Call SeekMoveSelectTime(e.X, e.Y, 1)
                    If SelectTime.FirX > 0 And SelectTime.FirY > 0 Then
                        Me.PicDiagram.Refresh()
                        tmpGraphics = Me.PicDiagram.CreateGraphics()
                        tmpGraphics.DrawEllipse(Pens.Blue, SelectTime.FirX - 5, SelectTime.FirY - 5, 10, 10)
                        tmpGraphics.DrawLine(Pens.Blue, SelectTime.FirX - 2, SelectTime.FirY, SelectTime.FirX + 2, SelectTime.FirY)
                        tmpGraphics.DrawLine(Pens.Blue, SelectTime.FirX, SelectTime.FirY - 2, SelectTime.FirX, SelectTime.FirY + 2)
                    End If
                Case 10 '测量时间第二点
                    Call SeekMoveSelectTime(e.X, e.Y, 2)
                    Dim sPrintText As String
                    Dim nJGtime As Integer
                    nJGtime = AddLitterTime(SelectTime.intCurSelectSecTime) - AddLitterTime(SelectTime.intCurSelectFirTime)
                    If nJGtime > 0 Then
                        sPrintText = "间隔时间:" & SecondToMinute(nJGtime).Trim
                    Else
                        sPrintText = "间隔时间:" & SecondToMinute(-nJGtime).Trim
                    End If
                    If SelectTime.SecX > 0 And SelectTime.SecY > 0 Then
                        Me.PicDiagram.Refresh()
                        tmpGraphics = Me.PicDiagram.CreateGraphics()
                        If SelectTime.FirX > 0 And SelectTime.FirY > 0 Then
                            tmpGraphics.DrawEllipse(Pens.Blue, SelectTime.FirX - 5, SelectTime.FirY - 5, 10, 10)
                            tmpGraphics.DrawLine(Pens.Blue, SelectTime.FirX - 2, SelectTime.FirY, SelectTime.FirX + 2, SelectTime.FirY)
                            tmpGraphics.DrawLine(Pens.Blue, SelectTime.FirX, SelectTime.FirY - 2, SelectTime.FirX, SelectTime.FirY + 2)
                        End If
                        tmpGraphics.DrawEllipse(Pens.Blue, SelectTime.SecX - 5, SelectTime.SecY - 5, 10, 10)
                        tmpGraphics.DrawLine(Pens.Blue, SelectTime.SecX - 2, SelectTime.SecY, SelectTime.SecX + 2, SelectTime.SecY)
                        tmpGraphics.DrawLine(Pens.Blue, SelectTime.SecX, SelectTime.SecY - 2, SelectTime.SecX, SelectTime.SecY + 2)

                        tmpGraphics.DrawLine(Pens.Blue, SelectTime.FirX, SelectTime.FirY, SelectTime.SecX, SelectTime.SecY)
                        tmpGraphics.DrawString(sPrintText, New System.Drawing.Font("黑体", 10), Brushes.Blue, SelectTime.SecX, SelectTime.SecY - 12)
                    End If


                Case 12 '多选列车
                    If e.Button = System.Windows.Forms.MouseButtons.Left Then
                        If SelectTime.X1 > 0 And SelectTime.Y1 > 0 Then
                            SelectTime.X2 = e.X
                            SelectTime.Y2 = e.Y
                            Me.PicDiagram.Refresh()
                            tmpGraphics = Me.PicDiagram.CreateGraphics()
                            tmpGraphics.DrawRectangle(Pens.Blue, SelectTime.X1, SelectTime.Y1, SelectTime.X2 - SelectTime.X1, SelectTime.Y2 - SelectTime.Y1)
                        End If
                    End If
            End Select
        End If
    End Sub

    Private Sub 保存乘务计划ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 保存乘务计划ToolStripButton1.Click

        If CSTrainsAndDrivers.CSDrivers Is Nothing = True OrElse UBound(CSTrainsAndDrivers.CSDrivers) = 0 Then
            Exit Sub
        Else
            Call DeleteNullDriverAndCodeDriver()
            If strQCurCSPlanID = "" Then
                Dim nf As New frmSaveCSTT
                nf.ShowDialog()
                Me.labName.Text = "当前乘务计划：" & CStr(strQCurCSPlanName)
            Else
                Call SaveXiuGai()
            End If
        End If
    End Sub


    Private Sub 参数设置ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 参数设置ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If
        If strQCurCSPlanID <> "" Then
            MsgBox("当前计划已经保存，不能进行固定参数设置！", MsgBoxStyle.OkOnly, "无法设置参数")
            Exit Sub
        End If
        Dim nf As New frmCSMakeBasicSet
        nf.ShowDialog()
        Try
            If sState = "乘务计划编制" AndAlso nf.flag = 1 Then
                If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 1 Then
                    CSTrainsAndDrivers = New CSTrainAndDrivers()
                End If
                Call refreshAfterParaSet()
            End If
        Catch ex As Exception
            MsgBox("重置编制过程失败！")
        End Try
    End Sub
    Private Sub refreshAfterParaSet()
        Call MakeParaSet()
        '5班3转匹配
        Call FiveThreeTurn_FitCSChedi()
        Call LoadSectionLengthInfo()
        Call CSTimeTableBackGroundFrameSet()
        Call ChangCSTrainToCSLinkTrain()
        Call FormMergeCSLinktrain()
        'Call FormdicStationTrain()
        Call LoadAreaInfo()
        Call SetDefaultSize()
        Call CSRefreshDiagram(0)
        Call ListAllViewInfo()
        Call ClearUnDoAndReDoFile()
    End Sub

    Private Sub 安排驾驶员IToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 安排驾驶员IToolStripMenuItem.Click
        If CSTimeTablePara.nPubTrain > 0 Then
            If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 1 Then
                Call reNameCSDrivers()
            End If

            frmNewDriverInputBox.ParentWindow = Me
            frmNewDriverInputBox.TrainID = CSTimeTablePara.nPubTrain
            frmNewDriverInputBox.ShowDialog()
        End If
    End Sub

    Private Sub 保存ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 保存ToolStripMenuItem.Click
        Call 保存乘务计划ToolStripButton1_Click(Nothing, Nothing)
    End Sub

    Private Sub 选择运行图ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 选择运行图ToolStripMenuItem.Click
        Dim nf As New frmCSMakeTTSelect
        nf.ShowDialog()

        If sState = "乘务计划编制" AndAlso nf.flag = 1 Then
            Me.labName.Text = "当前乘务计划：" & CStr(strQCurCSPlanName)
            Call refreshAfterParaSet()
            '每班上线车底数
            Call GetEachDutyChediNum()
            Me.labChediNum.Text = "车底数量：早班" & MChediNum & ",白班" & NChediNum & ",夜班" & AChediNum
            CSTrainsAndDrivers.ScheduleState = CrewScheduleState.未初始化
        End If
    End Sub

    Public MaxWidth As Integer = 3200
    Public MaxHeight As Integer = 2000
    Public MinWidth As Integer = 900
    Public MinHeight As Integer = 500
    Public DefaultWidth As Integer = 0
    Public DefaultHeight As Integer = 0

    Public Sub SetDefaultSize()
        DefaultWidth = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
        DefaultHeight = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        DefaultHeight = Me.SplitDiagram.Panel2.Height - 40
        CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = DefaultWidth
        CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = DefaultHeight
        CmbSize.SelectedIndex = 2
    End Sub

    Private Sub 管理ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 管理ToolStripMenuItem.Click
        Dim nf As New frmCSTimeTableManager
        nf.ShowDialog()
    End Sub

    Private Sub 乘务员ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 乘务员ToolStripMenuItem.Click
        Dim nf As New frmCSQuery
        nf.Show()
    End Sub

    Private Sub 统计数据ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 统计数据ToolStripMenuItem.Click
        Dim nf As New frmCSStaQuery(Me)
        nf.Show()
    End Sub

    Private Sub frmCSTimeTableMain_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        strCurlineID = ""
        DiagramCurID = ""
        strQCurCSPlanID = ""
        strQCurCSPlanName = ""
        CSTrainsAndDrivers.CSDrivers = Nothing
        CSTrainsAndDrivers.CSLinkTrains = Nothing
    End Sub

 

    Public Sub ListAllViewInfo()
        If CSTrainsAndDrivers.CSLinkTrains IsNot Nothing Then
            For Each train In CSTrainsAndDrivers.CSLinkTrains
                If train IsNot Nothing Then
                    train.IsLinked = False
                End If
            Next
        End If
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then
            For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                If dri IsNot Nothing Then
                    For Each train In dri.CSLinkTrain
                        If train IsNot Nothing AndAlso train.IsDeadHeading = False Then
                            train.IsLinked = True
                        End If
                    Next
                End If
            Next
        End If

        Call ListUnlinkedTrain()
        Call ListDutyInfo()
        Call ListDutyCountInfo()
        Call ListInOutDepotInfo()
        Call ListMorningOnOffInfo()
        Call ListPositionInfo()
        Call ListUnDeadInfo()
    End Sub

    Public Sub ListUnlinkedTrain()
        Me.listViewTrain.Items.Clear()
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) < 1 Then
            Exit Sub
        End If
        For i = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
            If CSTrainsAndDrivers.CSLinkTrains(i).IsLinked = False Then
                Dim liFile(8) As String
                Dim lvItem As ListViewItem
                liFile(0) = Me.listViewTrain.Items.Count + 1
                liFile(1) = CSTrainsAndDrivers.CSLinkTrains(i).CSTrainID
                liFile(2) = CSTrainsAndDrivers.CSLinkTrains(i).StartStaName
                liFile(3) = BeTime(CSTrainsAndDrivers.CSLinkTrains(i).StartTime)
                liFile(4) = CSTrainsAndDrivers.CSLinkTrains(i).OutputCheCi
                liFile(5) = CSTrainsAndDrivers.CSLinkTrains(i).EndStaName
                liFile(6) = BeTime(CSTrainsAndDrivers.CSLinkTrains(i).EndTime)
                liFile(7) = CSTrainsAndDrivers.CSLinkTrains(i).OffCheCi
                liFile(8) = CSTrainsAndDrivers.CSLinkTrains(i).distance
                lvItem = New ListViewItem(liFile)
                Me.listViewTrain.Items.Add(lvItem)
            End If
        Next

        If Me.listViewTrain.Items.Count > 0 Then
            ToolLabError.Text = "当前乘务计划还有" & Me.listViewTrain.Items.Count & "列列车没有安排"
        Else
            ToolLabError.Text = "当前乘务计划所有列车均已安排"
        End If
    End Sub

    Public Sub ListDutyInfo()
        Me.ListViewDuty.Items.Clear()
        If CSTrainsAndDrivers.CSDrivers Is Nothing OrElse UBound(CSTrainsAndDrivers.CSDrivers) < 1 Then
            Exit Sub
        End If
        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain.Count <= 1 Then
                Continue For
            End If

            Dim liFile(12) As String
            Dim lvItem As ListViewItem
            liFile(0) = Me.ListViewDuty.Items.Count + 1
            CSTrainsAndDrivers.CSDrivers(i).CSDriverID = i
            liFile(1) = CSTrainsAndDrivers.CSDrivers(i).CSDriverID
            liFile(2) = CSTrainsAndDrivers.CSDrivers(i).CSdriverNo
            liFile(3) = CSTrainsAndDrivers.CSDrivers(i).OutPutCSdriverNo
            liFile(4) = CSTrainsAndDrivers.CSDrivers(i).DutySort
            liFile(5) = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).StartStaName
            liFile(6) = BeTime(CSTrainsAndDrivers.CSDrivers(i).BeginWorkTime)
            liFile(7) = IIf(CSTrainsAndDrivers.CSDrivers(i).FlagDinner, "是", "否")
            liFile(8) = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)).EndStaName
            liFile(9) = BeTime(CSTrainsAndDrivers.CSDrivers(i).EndEorkTime)
            liFile(10) = CSTrainsAndDrivers.CSDrivers(i).State
            liFile(11) = CSTrainsAndDrivers.CSDrivers(i).DriveDistance
            liFile(12) = BeTime(CSTrainsAndDrivers.CSDrivers(i).WorkTime)
            lvItem = New ListViewItem(liFile)
            If CSTrainsAndDrivers.CSDrivers(i).IsReasonable = False Then
                lvItem.BackColor = Color.Red
            End If
            Me.ListViewDuty.Items.Add(lvItem)

        Next

        If Me.ListViewDuty.Items.Count > 0 Then
            ToolLabError.Text &= " | 当前乘务计划共" & UBound(CSTrainsAndDrivers.CSDrivers) & "个任务,其中早班" & CSTrainsAndDrivers.MorningDrivers.Count & "个,白班" & _
                CSTrainsAndDrivers.DayDrivers.Count & "个,日勤班" & CSTrainsAndDrivers.CDayDrivers.Count & "个,夜班" & CSTrainsAndDrivers.NightDrivers.Count & "个"
        Else
            ToolLabError.Text = " | 当前乘务计划没有任务"
        End If
    End Sub

    Public Sub ListDutyCountInfo()
        Me.ListViewDutyNum.Items.Clear()
        If CSTrainsAndDrivers.CSDrivers Is Nothing OrElse UBound(CSTrainsAndDrivers.CSDrivers) < 1 Then
            Exit Sub
        End If
        Dim lifile() As String = ListDutyCountInfo(CSTrainsAndDrivers.MorningDrivers)
        Dim lifile1(7) As String
        lifile1(0) = 1
        lifile1(1) = "早班"
        lifile1(2) = lifile(0)
        lifile1(3) = lifile(1)
        lifile1(4) = lifile(2)
        lifile = ListDutyCountInfo(CSTrainsAndDrivers.DayDrivers)
        Dim lifile2(7) As String
        lifile2(0) = 2
        lifile2(1) = "白班"
        lifile2(2) = lifile(0)
        lifile2(3) = lifile(1)
        lifile2(4) = lifile(2)
        lifile = ListDutyCountInfo(CSTrainsAndDrivers.CDayDrivers)
        Dim lifile3(7) As String
        lifile3(0) = 3
        lifile3(1) = "日勤班"
        lifile3(2) = lifile(0)
        lifile3(3) = lifile(1)
        lifile3(4) = lifile(2)
        lifile = ListDutyCountInfo(CSTrainsAndDrivers.NightDrivers)
        Dim lifile4(7) As String
        lifile4(0) = 4
        lifile4(1) = "夜班"
        lifile4(2) = lifile(0)
        lifile4(3) = lifile(1)
        lifile4(4) = lifile(2)
        If CSTrainsAndDrivers.IfCorSchedule Then
            If CSTrainsAndDrivers.CorCSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CorCSDrivers) > 0 Then
                lifile = ListDutyCountInfo(CSTrainsAndDrivers.CorMorningDrivers)
                lifile1(5) = lifile(0)
                lifile1(6) = lifile(1)
                lifile1(7) = lifile(2)
                lifile = ListDutyCountInfo(CSTrainsAndDrivers.CorDayDrivers)
                lifile2(5) = lifile(0)
                lifile2(6) = lifile(1)
                lifile2(7) = lifile(2)
                lifile = ListDutyCountInfo(CSTrainsAndDrivers.CorCDayDrivers)
                lifile3(5) = lifile(0)
                lifile3(6) = lifile(1)
                lifile3(7) = lifile(2)
                lifile = ListDutyCountInfo(CSTrainsAndDrivers.CorNightDrivers)
                lifile4(5) = lifile(0)
                lifile4(6) = lifile(1)
                lifile4(7) = lifile(2)
            End If
        End If
        Dim lvItem As ListViewItem
        lvItem = New ListViewItem(lifile1)
        Me.ListViewDutyNum.Items.Add(lvItem)
        lvItem = New ListViewItem(lifile2)
        Me.ListViewDutyNum.Items.Add(lvItem)
        lvItem = New ListViewItem(lifile3)
        Me.ListViewDutyNum.Items.Add(lvItem)
        lvItem = New ListViewItem(lifile4)
        Me.ListViewDutyNum.Items.Add(lvItem)
    End Sub

    Public Function ListDutyCountInfo(ByVal Drivers As List(Of CSDriver)) As String()
        Dim liFile(2) As String
        Dim OnStaStr As New List(Of String)
        Dim OffStaStr As New List(Of String)
        Dim OnNum As New List(Of Integer)
        Dim OffNum As New List(Of Integer)
        For Each dri As CSDriver In Drivers
            If UBound(dri.CSLinkTrain) <> 0 Then
                Dim OnSta As String = dri.CSLinkTrain(1).StartStaName
                Dim index As Integer = OnStaStr.FindIndex(Function(value As String)
                                                              Return value = OnSta
                                                          End Function)
                If index = -1 Then
                    OnStaStr.Add(OnSta)
                    OnNum.Add(1)
                Else
                    OnNum(index) += 1
                End If
                Dim OffSta As String = dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName
                index = OffStaStr.FindIndex(Function(value As String)
                                                Return value = OffSta
                                            End Function)
                If index = -1 Then
                    OffStaStr.Add(OffSta)
                    OffNum.Add(1)
                Else
                    OffNum(index) += 1
                End If
            End If
          
        Next
        liFile(0) = Drivers.Count
        Dim str3 As String = ""
        If OnStaStr.Count > 0 Then
            For i As Integer = 0 To OnStaStr.Count - 1
                str3 &= OnStaStr(i) & ":" & OnNum(i) & " "
            Next
        End If
        liFile(1) = str3
        Dim str4 As String = ""
        If OffStaStr.Count > 0 Then
            For i As Integer = 0 To OffStaStr.Count - 1
                str4 &= OffStaStr(i) & ":" & OffNum(i) & " "
            Next
        End If
        liFile(2) = str4
        Return liFile
    End Function

    Public Sub ListMorningOnOffInfo()
        Me.ListViewMOnOffInfo.Items.Clear()
        If CSTrainsAndDrivers.CSDrivers Is Nothing OrElse UBound(CSTrainsAndDrivers.CSDrivers) < 1 OrElse CSTrainsAndDrivers.MorningDrivers.Count = 0 Then
            Exit Sub
        End If
        Dim MOnSta As New List(Of String)
        Dim MDutyCount As New List(Of Integer)
        Dim MDrivers As New List(Of List(Of CSDriver))
        For Each dri As CSDriver In CSTrainsAndDrivers.MorningDrivers
            If UBound(dri.CSLinkTrain) <> 0 Then
                Dim OnSta As String = dri.CSLinkTrain(1).StartStaName
                Dim index As Integer = MOnSta.FindIndex(Function(value As String)
                                                            Return value = OnSta
                                                        End Function)
                If index = -1 Then
                    MOnSta.Add(OnSta)
                    MDutyCount.Add(1)
                    Dim temList As New List(Of CSDriver)
                    temList.Add(dri)
                    MDrivers.Add(temList)
                Else
                    MDutyCount(index) += 1
                    MDrivers(index).Add(dri)
                End If
            End If
           
        Next
        Dim point As Integer = 0
        For Each sta As String In MOnSta
            Dim lifile(5) As String
            lifile(0) = point + 1
            lifile(1) = sta
            lifile(2) = MDrivers(point).Count
            lifile(3) = ListMorningOnOffInfo(MDrivers(point))
            Dim CorDrivers As New List(Of CSDriver)
            If CSTrainsAndDrivers.IfCorSchedule Then
                For Each dri As CSDriver In CSTrainsAndDrivers.CorMorningDrivers
                    If dri.CSLinkTrain(1).StartStaName = sta Then
                        CorDrivers.Add(dri)
                    End If
                Next
                lifile(4) = CorDrivers.Count
                lifile(5) = ListMorningOnOffInfo(CorDrivers)
            End If
            Dim lvItem As ListViewItem
            lvItem = New ListViewItem(lifile)
            If MDrivers(point).Count > CorDrivers.Count Then
                lvItem.ForeColor = Color.Red
            End If
            Me.ListViewMOnOffInfo.Items.Add(lvItem)
            point += 1
        Next
    End Sub

    Public Function ListMorningOnOffInfo(ByVal Drivers As List(Of CSDriver)) As String
        Dim liFile As String
        Dim OffStaStr As New List(Of String)
        Dim OffNum As New List(Of Integer)
        For Each dri As CSDriver In Drivers
            Dim OffSta As String = dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName
            Dim index As Integer = OffStaStr.FindIndex(Function(value As String)
                                                           Return value = OffSta
                                                       End Function)
            If index = -1 Then
                OffStaStr.Add(OffSta)
                OffNum.Add(1)
            Else
                OffNum(index) += 1
            End If
        Next
        liFile = ""
        If OffStaStr.Count > 0 Then
            For i As Integer = 0 To OffStaStr.Count - 1
                liFile &= OffStaStr(i) & ":" & OffNum(i) & " "
            Next
        End If
        Return liFile
    End Function

    Public Sub ListCurDutyInfo()
        If CSTrainsAndDrivers.CSDrivers Is Nothing OrElse UBound(CSTrainsAndDrivers.CSDrivers) < 1 Then
            Exit Sub
        End If
        Me.ListViewCurDuty.Items.Clear()
        If CSTimeTablePara.nPubCheDi > 0 Then
            Dim TotalDistance As Decimal = 0
            For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain)
                Dim liFile(10) As String
                Dim lvItem As ListViewItem
                liFile(0) = Me.ListViewCurDuty.Items.Count + 1
                liFile(1) = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSDriverID
                liFile(2) = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSdriverNo
                liFile(3) = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).OutPutCSdriverNo
                liFile(4) = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).StartStaName
                liFile(5) = BeTime(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).StartTime)
                liFile(6) = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).OutputCheCi
                liFile(7) = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).EndStaName
                liFile(8) = BeTime(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).EndTime)
                liFile(9) = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).OffCheCi
                If CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).IsDeadHeading = False Then
                    TotalDistance += CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).distance
                End If
                liFile(10) = TotalDistance
                lvItem = New ListViewItem(liFile)
                Me.ListViewCurDuty.Items.Add(lvItem)
            Next
        End If
    End Sub

    Public Sub ListUnDeadInfo()
        If CSTrainsAndDrivers.CSDrivers Is Nothing OrElse UBound(CSTrainsAndDrivers.CSDrivers) < 1 Then
            Exit Sub
        End If
        Me.ListViewUnDead.Items.Clear()
        If CSTrainsAndDrivers.MorningDrivers IsNot Nothing Then
            Dim dutystr As String = ""
            For Each dri As CSDriver In CSTrainsAndDrivers.MorningDrivers
                If UBound(dri.CSLinkTrain) <> 0 Then
                    If dri.CSLinkTrain(1).FirstStation IsNot Nothing AndAlso dri.CSLinkTrain(1).FirstStation.IsYard = False Then
                        dutystr &= dri.CSdriverNo & "/"
                    End If
                End If
                
            Next
            If dutystr <> "" Then
                Dim liFile(10) As String
                Dim lvItem As ListViewItem
                liFile(0) = Me.ListViewUnDead.Items.Count + 1
                liFile(1) = "早班"
                liFile(2) = dutystr.Trim("/")
                lvItem = New ListViewItem(liFile)
                Me.ListViewUnDead.Items.Add(lvItem)
            End If
        End If

        If CSTrainsAndDrivers.NightDrivers IsNot Nothing Then
            Dim dutystr As String = ""
            For Each dri As CSDriver In CSTrainsAndDrivers.NightDrivers
                If dri.CSLinkTrain(UBound(dri.CSLinkTrain)).SecondStation IsNot Nothing AndAlso dri.CSLinkTrain(UBound(dri.CSLinkTrain)).SecondStation.IsYard = False Then
                    dutystr &= dri.CSdriverNo & "/"
                End If
            Next
            If dutystr <> "" Then
                Dim liFile(10) As String
                Dim lvItem As ListViewItem
                liFile(0) = Me.ListViewUnDead.Items.Count + 1
                liFile(1) = "夜班"
                liFile(2) = dutystr.Trim("/")
                lvItem = New ListViewItem(liFile)
                Me.ListViewUnDead.Items.Add(lvItem)
            End If
        End If
    End Sub

    Public Sub ListInOutDepotInfo()
        Me.ListViewInOutDepot.Items.Clear()
        If CSTrainsAndDrivers.CSDrivers Is Nothing OrElse UBound(CSTrainsAndDrivers.CSDrivers) < 1 Then
            Exit Sub
        End If
        Dim depotStr As New List(Of String)
        Dim OutDepotNum As New List(Of Integer)
        Dim InDepotNum As New List(Of Integer)
        For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            If UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain) <> 0 Then
                If CSTrainsAndDrivers.CSDrivers(i).DutySort = "早班" Then

                    If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).FirstStation IsNot Nothing AndAlso CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).FirstStation.IsYard Then
                        Dim depotname As String = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).StartStaName
                        Dim index As Integer = depotStr.FindIndex(Function(value As String)
                                                                      Return (depotname = value)
                                                                  End Function)
                        If index = -1 Then
                            depotStr.Add(depotname)
                            InDepotNum.Add(1)
                            OutDepotNum.Add(0)
                        Else
                            InDepotNum(index) += 1
                        End If
                    End If
                ElseIf CSTrainsAndDrivers.CSDrivers(i).DutySort = "夜班" Then
                    If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)).SecondStation IsNot Nothing AndAlso CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)).SecondStation.IsYard Then
                        Dim depotname As String = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)).EndStaName
                        Dim index As Integer = depotStr.FindIndex(Function(value As String)
                                                                      Return (depotname = value)
                                                                  End Function)
                        If index = -1 Then
                            depotStr.Add(depotname)
                            InDepotNum.Add(0)
                            OutDepotNum.Add(1)
                        Else
                            OutDepotNum(index) += 1
                        End If
                    End If
                End If
            End If
            
        Next
        Dim InfoStr As String = " | "
        If depotStr.Count > 0 Then
            For i As Integer = 0 To depotStr.Count - 1
                Dim inTrainNum As Integer = 0
                Dim OutTrainNUm As Integer = 0
                For Each train As CSLinkTrain In CSTrainsAndDrivers.CSLinkTrains
                    If train IsNot Nothing AndAlso train.IsLinked = False Then
                        If train.StartStaName = depotStr(i) Then
                            OutTrainNUm += 1
                        ElseIf train.EndStaName = depotStr(i) Then
                            inTrainNum += 1
                        End If
                    End If
                Next
                Dim liFile(5) As String
                Dim lvItem As ListViewItem
                liFile(0) = Me.ListViewInOutDepot.Items.Count + 1
                liFile(1) = depotStr(i)
                liFile(2) = InDepotNum(i)
                liFile(3) = OutTrainNUm
                liFile(4) = OutDepotNum(i)
                liFile(5) = inTrainNum
                lvItem = New ListViewItem(liFile)
                If InDepotNum(i) <> OutDepotNum(i) Then
                    lvItem.ForeColor = Color.Red
                End If
                Me.ListViewInOutDepot.Items.Add(lvItem)
                InfoStr &= " '" & depotStr(i) & "'出入库" & InDepotNum(i) & "/" & OutDepotNum(i) & ""
            Next
        Else
            InfoStr &= "没出出入库信息"
        End If
        ToolLabError.Text &= InfoStr
    End Sub

    Public Sub ListPositionInfo()
        Me.ListViewPosition.Items.Clear()
        If CSTrainsAndDrivers.CSDrivers Is Nothing OrElse UBound(CSTrainsAndDrivers.CSDrivers) < 1 Then
            Exit Sub
        End If
        Dim StatStr As New List(Of String)
        Dim StaNum As New List(Of Integer)
        Dim DriversStr As New List(Of String)
        For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            If CSTrainsAndDrivers.CSDrivers(i).State <> "班后" And UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain) > 1 Then
                Dim depotname As String = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)).EndStaName
                Dim index As Integer = StatStr.FindIndex(Function(value As String)
                                                             Return (depotname = value)
                                                         End Function)
                If index = -1 Then
                    StatStr.Add(depotname)
                    StaNum.Add(1)
                    DriversStr.Add(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo & "(" & BeTime(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)).EndTime) & ")")
                Else
                    StaNum(index) += 1
                    DriversStr(index) &= "/" & CSTrainsAndDrivers.CSDrivers(i).CSdriverNo & "(" & BeTime(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)).EndTime) & ")"
                End If
            End If
        Next
        Dim InfoStr As String = " | "
        If StatStr.Count > 0 Then
            For i As Integer = 0 To StatStr.Count - 1
                Dim liFile(3) As String
                Dim lvItem As ListViewItem
                liFile(0) = Me.ListViewPosition.Items.Count + 1
                liFile(1) = StatStr(i)
                liFile(2) = StaNum(i)
                Dim temStr() As String = DriversStr(i).Split("/")
                If UBound(temStr) > 0 Then
                    DriversStr(i) = ""
                    For m As Integer = 0 To UBound(temStr) - 1
                        For n As Integer = m + 1 To UBound(temStr)
                            Dim time1 As Integer = CDate(temStr(m).Substring(temStr(m).IndexOf("(") + 1, temStr(m).IndexOf(")") - temStr(m).IndexOf("(") - 1)).TimeOfDay.TotalSeconds
                            Dim time2 As Integer = CDate(temStr(n).Substring(temStr(n).IndexOf("(") + 1, temStr(n).IndexOf(")") - temStr(n).IndexOf("(") - 1)).TimeOfDay.TotalSeconds
                            If IIf(time2 < 10800, time2 + 86400, time2) < IIf(time1 < 10800, time1 + 86400, time1) Then
                                Dim tstr As String = temStr(m)
                                temStr(m) = temStr(n)
                                temStr(n) = tstr
                            End If
                        Next
                    Next
                    For Each istr As String In temStr
                        DriversStr(i) &= istr & "/"
                    Next
                    DriversStr(i).Trim("/")
                End If
                liFile(3) = DriversStr(i)
                lvItem = New ListViewItem(liFile)
                Me.ListViewPosition.Items.Add(lvItem)
                InfoStr &= " '" & StatStr(i) & "'出入库" & StaNum(i)
            Next
        Else
            InfoStr &= "没出位置信息"
        End If
        ToolLabError.Text &= InfoStr
    End Sub

    Private Sub listViewTrain_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles listViewTrain.MouseDown
        If Me.listViewTrain.SelectedItems.Count > 0 Then
            If CSTimeTablePara.sCurDiagramState = DiagramState.运行图 Then
                CSTimeTablePara.nPubTrain = Me.listViewTrain.SelectedItems(0).SubItems(1).Text
                If CSTimeTablePara.nPubTrain > 0 Then
                    Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
                    CSTimeTablePara.picPubDiagram.Refresh()
                    rBmpGraphics = CSTimeTablePara.picPubDiagram.CreateGraphics()
                    Dim tmpPen As Pen
                    tmpPen = New Pen(Color.Blue, 2)

                    Call CSShowLabInfor(CSTimeTablePara.nPubTrain, Me.labInfor)
                    Me.nIFShowAllCheDiTrain = 0
                    Call SetCurScrollbarInSelectTrain(CSTimeTablePara.nPubTrain)
                    Call CSDrawLineInPicture(CSTimeTablePara.nPubTrain, rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                Else
                    CSTimeTablePara.picPubDiagram.Refresh()
                End If
            End If
        End If
    End Sub
    '使选中的列车显示在屏幕中间
    Public Sub CSlistTrainInMiddlePic(ByVal nCSLinkTrainID As Integer)
        Dim sBeTime As Integer
        Dim sngCurX As Integer
        sBeTime = CSTrainsAndDrivers.CSLinkTrains(nCSLinkTrainID).StartTime
        sngCurX = FormTimeToXCord(sBeTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX)
    End Sub
    '将当前的动条移到到可以显示的列车
    Public Sub SetCurScrollbarInSelectTrain(ByVal nCSLinkTrainID As Integer)
        Dim nMax As Single
        Dim curX As Single
        Dim xMax As Single
        Dim tmpTime As Long
        Dim nBi As Single
        Dim nBarX As Single
        nMax = SplitDiagram.Panel2.HorizontalScroll.Maximum
        tmpTime = CSTrainsAndDrivers.CSLinkTrains(nCSLinkTrainID).StartTime
        If tmpTime > 0 Then
            curX = FormTimeToXCord(tmpTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX)
            xMax = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
            If xMax > 0 Then
                nBi = (curX) / xMax
                nBarX = nMax * nBi - 400
                SplitDiagram.Panel2.AutoScrollPosition = New System.Drawing.Point(nBarX, -SplitDiagram.Panel2.AutoScrollPosition.Y)
            End If
        End If
    End Sub

    Private Sub 图片IToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 图片IToolStripMenuItem.Click
        Dim strPath As String
        Dim New0penFile As New SaveFileDialog
        New0penFile.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|jpeg files (*.jpeg)|*.jpeg|All files (*.*)|*.*"
        New0penFile.FilterIndex = 1
        New0penFile.RestoreDirectory = True
        strPath = ""
        If New0penFile.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            strPath = New0penFile.FileName
            Me.PicDiagram.Image.Save(strPath)
        End If
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExcelToolStripMenuItem.Click
        If CSTrainsAndDrivers.DayDrivers.Count = 0 AndAlso CSTrainsAndDrivers.MorningDrivers.Count = 0 AndAlso CSTrainsAndDrivers.CDayDrivers.Count = 0 Then
            MsgBox("未找到打开的乘务计划，请先打开需输出的乘务计划！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
            Exit Sub
        End If
        Dim frm1 As New FrmOutputPosition(1)
        frm1.ShowDialog()
      
    End Sub

    

    Public Function DrawNightDataToExcel(ByVal csd As CSDriver, ByVal Startrow As Integer, ByVal sheet As Excel.Worksheet) As Integer
        sheet.Cells(Startrow, 1) = "任务编号"
        sheet.Cells(Startrow, 2) = "任务开始时间"
        sheet.Cells(Startrow, 3) = "接车时间"
        sheet.Cells(Startrow, 4) = "接车地点"
        sheet.Cells(Startrow, 5) = "接车车次"
        sheet.Cells(Startrow, 6) = "车底号"
        sheet.Cells(Startrow, 7) = "下车地点"
        sheet.Cells(Startrow, 8) = "下车车次"
        sheet.Cells(Startrow, 9) = "下车时间"
        sheet.Cells(Startrow, 10) = "任务结束时间"

        sheet.Range(sheet.Cells(Startrow + 1, 1), sheet.Cells(Startrow + 3, 1)).Merge()
        sheet.Range(sheet.Cells(Startrow + 4, 1), sheet.Cells(Startrow + 7, 1)).Merge()
        sheet.Range(sheet.Cells(Startrow + 8, 1), sheet.Cells(Startrow + 10, 1)).Merge()
        sheet.Range(sheet.Cells(Startrow + 11, 1), sheet.Cells(Startrow + 14, 1)).Merge()
        sheet.Cells(Startrow + 1, 1) = csd.OutPutCSdriverNo
        If csd.LinkedMorDriver IsNot Nothing Then
            sheet.Cells(Startrow + 4, 1) = (MyCeiLing(0.25, Convert.ToDecimal(csd.WorkTime + csd.LinkedMorDriver.WorkTime) / 3600)) & "h"
        Else
            sheet.Cells(Startrow + 4, 1) = (MyCeiLing(0.25, Convert.ToDecimal(csd.WorkTime) / 3600)) & "h"
        End If
        sheet.Cells(Startrow + 8, 1) = (csd.DriveDistance).ToString("0.0") & "km"
        If csd.LinkedMorDriver IsNot Nothing Then
            sheet.Cells(Startrow + 11, 1) = (csd.LinkedMorDriver.DriveDistance).ToString("0.0") & "km"
        End If
        sheet.Range(sheet.Cells(Startrow + 1, 1), sheet.Cells(Startrow + 1, 1)).NumberFormat = "@"

        sheet.Range(sheet.Cells(Startrow, 1), sheet.Cells(Startrow + 14, 10)).Borders.LineStyle = 1
        sheet.Range(sheet.Cells(Startrow, 1), sheet.Cells(Startrow + 14, 10)).Font.Name = "Arial Unicode MS"
        sheet.Range(sheet.Cells(Startrow, 1), sheet.Cells(Startrow + 14, 10)).Font.Size = 11
        sheet.Range(sheet.Cells(Startrow, 1), sheet.Cells(Startrow + 14, 10)).HorizontalAlignment = XlHAlign.xlHAlignCenter
        sheet.Range(sheet.Cells(Startrow, 1), sheet.Cells(Startrow + 14, 10)).VerticalAlignment = XlHAlign.xlHAlignCenter
        sheet.Range(sheet.Cells(Startrow, 1), sheet.Cells(Startrow + 14, 10)).ColumnWidth = 10   '固定列宽
        sheet.Range(sheet.Cells(Startrow, 1), sheet.Cells(Startrow + 14, 10)).WrapText = True    '文本自动换行

        Dim CurPos As Integer = Startrow
        sheet.Cells(CurPos + 1, 2) = CDate(Coordination2.Global.BeTime(csd.BeginWorkTime)).ToString("HH:mm")
        If csd.IsPrepareDri = False Then
            For i As Integer = 1 To UBound(csd.ModifiedCSLinkTrain)
                CurPos += 1
                If csd.FlagDinner = True Then
                    If (i = 1 AndAlso csd.DinnerStartTime < csd.ModifiedCSLinkTrain(i).StartTime) OrElse _
                        (i > 1 AndAlso csd.DinnerStartTime < csd.ModifiedCSLinkTrain(i).StartTime AndAlso csd.DinnerStartTime > csd.ModifiedCSLinkTrain(i - 1).StartTime) Then
                        sheet.Cells(CurPos, 3) = CDate(Coordination2.Global.BeTime(csd.DinnerStartTime)).ToString("HH:mm")
                        sheet.Range(sheet.Cells(CurPos, 4), sheet.Cells(CurPos, 8)).Merge()
                        sheet.Cells(CurPos, 4) = "用餐时间"
                        sheet.Cells(CurPos, 9) = CDate(Coordination2.Global.BeTime(csd.DinnerEndTime)).ToString("HH:mm")
                        CurPos += 1
                    End If
                End If
                If csd.ModifiedCSLinkTrain(i).IsDeadHeading = False AndAlso csd.ModifiedCSLinkTrain(i).FirstStation.IsYard Then
                    sheet.Cells(CurPos, 3) = CDate(Coordination2.Global.BeTime(csd.ModifiedCSLinkTrain(i).StartTime - PrepareTrainTime)).ToString("HH:mm")
                    sheet.Cells(CurPos, 4) = "备车"
                    CurPos += 1
                End If
                sheet.Cells(CurPos, 3) = CDate(Coordination2.Global.BeTime(csd.ModifiedCSLinkTrain(i).StartTime)).ToString("HH:mm")
                sheet.Cells(CurPos, 9) = CDate(Coordination2.Global.BeTime(csd.ModifiedCSLinkTrain(i).EndTime)).ToString("HH:mm")
                If csd.ModifiedCSLinkTrain(i).IsDeadHeading = False Then
                    sheet.Cells(CurPos, 4) = csd.ModifiedCSLinkTrain(i).StartStaName & IIf(csd.ModifiedCSLinkTrain(i).UpOrDown = 1, "下", "上")
                    sheet.Cells(CurPos, 5) = csd.ModifiedCSLinkTrain(i).OutputCheCi
                    sheet.Cells(CurPos, 6) = csd.ModifiedCSLinkTrain(i).sCheDiHao
                    sheet.Cells(CurPos, 7) = csd.ModifiedCSLinkTrain(i).EndStaName & IIf(csd.ModifiedCSLinkTrain(i).OffUpOrDown = 1, "下", "上")
                    sheet.Cells(CurPos, 8) = csd.ModifiedCSLinkTrain(i).OffCheCi
                Else
                    sheet.Range(sheet.Cells(CurPos, 4), sheet.Cells(CurPos, 8)).Merge()
                    sheet.Cells(CurPos, 4) = "坐" & csd.ModifiedCSLinkTrain(i).sCheDiHao & " " & csd.ModifiedCSLinkTrain(i).OutputCheCi & "到" & csd.ModifiedCSLinkTrain(i).EndStaName
                End If
            Next
        Else
            CurPos += 1
            sheet.Range(sheet.Cells(CurPos, 3), sheet.Cells(CurPos, 9)).Merge()
            sheet.Cells(CurPos, 3) = csd.CSLinkTrain(1).StartStaName & csd.CSdriverNo
        End If

        If csd.FlagDinner = True AndAlso csd.DinnerStartTime > csd.ModifiedCSLinkTrain(UBound(csd.ModifiedCSLinkTrain)).StartTime Then
            CurPos += 1
            sheet.Cells(CurPos, 3) = CDate(Coordination2.Global.BeTime(csd.DinnerStartTime)).ToString("HH:mm")
            sheet.Range(sheet.Cells(CurPos, 4), sheet.Cells(CurPos, 8)).Merge()
            sheet.Cells(CurPos, 4) = "用餐时间"
            sheet.Cells(CurPos, 9) = CDate(Coordination2.Global.BeTime(csd.DinnerEndTime)).ToString("HH:mm")
        End If
        sheet.Cells(CurPos, 10) = CDate(Coordination2.Global.BeTime(csd.EndEorkTime)).ToString("HH:mm")

        If csd.LinkedMorDriver IsNot Nothing Then
            CurPos += 2
            sheet.Cells(CurPos + 1, 2) = CDate(Coordination2.Global.BeTime(csd.LinkedMorDriver.BeginWorkTime)).ToString("HH:mm")
            If csd.LinkedMorDriver.IsPrepareDri = False Then
                For i As Integer = 1 To UBound(csd.LinkedMorDriver.ModifiedCSLinkTrain)
                    CurPos += 1
                    If csd.LinkedMorDriver.FlagDinner = True Then
                        If (i = 1 AndAlso csd.LinkedMorDriver.DinnerStartTime < csd.LinkedMorDriver.ModifiedCSLinkTrain(i).StartTime) OrElse _
                            (i > 1 AndAlso csd.LinkedMorDriver.DinnerStartTime < csd.LinkedMorDriver.ModifiedCSLinkTrain(i).StartTime AndAlso csd.LinkedMorDriver.DinnerStartTime > csd.LinkedMorDriver.ModifiedCSLinkTrain(i - 1).StartTime) Then
                            sheet.Cells(CurPos, 3) = CDate(Coordination2.Global.BeTime(csd.LinkedMorDriver.DinnerStartTime)).ToString("HH:mm")
                            sheet.Range(sheet.Cells(CurPos, 4), sheet.Cells(CurPos, 8)).Merge()
                            sheet.Cells(CurPos, 4) = "用餐时间"
                            sheet.Cells(CurPos, 9) = CDate(Coordination2.Global.BeTime(csd.LinkedMorDriver.DinnerEndTime)).ToString("HH:mm")
                            CurPos += 1
                        End If
                    End If
                    If csd.LinkedMorDriver.ModifiedCSLinkTrain(i).IsDeadHeading = False AndAlso csd.LinkedMorDriver.ModifiedCSLinkTrain(i).FirstStation.IsYard Then
                        sheet.Cells(CurPos, 3) = CDate(Coordination2.Global.BeTime(csd.LinkedMorDriver.ModifiedCSLinkTrain(i).StartTime - PrepareTrainTime)).ToString("HH:mm")
                        sheet.Cells(CurPos, 4) = "备车"
                        CurPos += 1
                    End If
                    sheet.Cells(CurPos, 3) = CDate(Coordination2.Global.BeTime(csd.LinkedMorDriver.ModifiedCSLinkTrain(i).StartTime)).ToString("HH:mm")
                    sheet.Cells(CurPos, 9) = CDate(Coordination2.Global.BeTime(csd.LinkedMorDriver.ModifiedCSLinkTrain(i).EndTime)).ToString("HH:mm")
                    If csd.LinkedMorDriver.ModifiedCSLinkTrain(i).IsDeadHeading = False Then
                        sheet.Cells(CurPos, 4) = csd.LinkedMorDriver.ModifiedCSLinkTrain(i).StartStaName & IIf(csd.LinkedMorDriver.ModifiedCSLinkTrain(i).UpOrDown = 1, "下", "上")
                        sheet.Cells(CurPos, 5) = csd.LinkedMorDriver.ModifiedCSLinkTrain(i).OutputCheCi
                        sheet.Cells(CurPos, 6) = csd.LinkedMorDriver.ModifiedCSLinkTrain(i).sCheDiHao
                        sheet.Cells(CurPos, 7) = csd.LinkedMorDriver.ModifiedCSLinkTrain(i).EndStaName & IIf(csd.LinkedMorDriver.ModifiedCSLinkTrain(i).OffUpOrDown = 1, "下", "上")
                        sheet.Cells(CurPos, 8) = csd.LinkedMorDriver.ModifiedCSLinkTrain(i).OffCheCi
                    Else
                        sheet.Range(sheet.Cells(CurPos, 4), sheet.Cells(CurPos, 8)).Merge()
                        sheet.Cells(CurPos, 4) = "坐" & csd.LinkedMorDriver.ModifiedCSLinkTrain(i).sCheDiHao & " " & _
                                                csd.LinkedMorDriver.ModifiedCSLinkTrain(i).OutputCheCi & "到" & csd.LinkedMorDriver.ModifiedCSLinkTrain(i).EndStaName
                    End If
                Next
            Else
                CurPos += 1
                sheet.Range(sheet.Cells(CurPos, 3), sheet.Cells(CurPos, 9)).Merge()
                sheet.Cells(CurPos, 3) = csd.LinkedMorDriver.CSLinkTrain(1).StartStaName & csd.LinkedMorDriver.CSdriverNo
            End If

            If csd.LinkedMorDriver.FlagDinner = True AndAlso csd.LinkedMorDriver.DinnerStartTime > csd.LinkedMorDriver.ModifiedCSLinkTrain(UBound(csd.LinkedMorDriver.ModifiedCSLinkTrain)).StartTime Then
                CurPos += 1
                sheet.Cells(CurPos, 3) = CDate(Coordination2.Global.BeTime(csd.LinkedMorDriver.DinnerStartTime)).ToString("HH:mm")
                sheet.Range(sheet.Cells(CurPos, 4), sheet.Cells(CurPos, 8)).Merge()
                sheet.Cells(CurPos, 4) = "用餐时间"
                sheet.Cells(CurPos, 9) = CDate(Coordination2.Global.BeTime(csd.LinkedMorDriver.DinnerEndTime)).ToString("HH:mm")
            End If
            sheet.Cells(CurPos, 10) = CDate(Coordination2.Global.BeTime(csd.LinkedMorDriver.EndEorkTime)).ToString("HH:mm")
        End If
        Dim str As String = "select t.*,m.cstimetablename,n.outputcsdriverno from cs_amdrivercorrespond t,cs_cstimetableinf m,cs_workload n " & _
            "where t.mdrivertimetableid=m.cstimetableid and t.adrivertimetableid='" & strQCurCSPlanID & "' and t.mdrivertimetableid<>'" & strQCurCSPlanID & _
            "' and t.adriverno='" & csd.CSdriverNo & "' and t.mdrivertimetableid=n.cstimetableid and t.mdriverno=n.driverno"
        Dim temtab As Data.DataTable = ReadData(str)
        If temtab IsNot Nothing AndAlso temtab.Rows.Count > 0 Then
            For Each row As DataRow In temtab.Rows
                CurPos += 1
                sheet.Range(sheet.Cells(CurPos, 3), sheet.Cells(CurPos, 9)).Merge()
                sheet.Cells(CurPos, 3) = "转" & row.Item("cstimetablename").ToString & "见" & row.Item("outputcsdriverno").ToString
            Next
        End If
        str = "select t.*,m.cstimetablename,n.outputcsdriverno from cs_amdrivercorrespond t,cs_cstimetableinf m,cs_result_prepareddutyinf n " & _
            "where t.mdrivertimetableid=m.cstimetableid and t.adrivertimetableid='" & strQCurCSPlanID & "' and t.mdrivertimetableid<>'" & strQCurCSPlanID & _
            "' and t.adriverno='" & csd.CSdriverNo & "' and t.mdrivertimetableid=n.cstimetableid and t.mdriverno=n.name and n.dutysort='早班'"
        temtab = ReadData(str)
        If temtab IsNot Nothing AndAlso temtab.Rows.Count > 0 Then
            For Each row As DataRow In temtab.Rows
                CurPos += 1
                sheet.Range(sheet.Cells(CurPos, 3), sheet.Cells(CurPos, 9)).Merge()
                sheet.Cells(CurPos, 3) = "转" & row.Item("cstimetablename").ToString & "见" & row.Item("outputcsdriverno").ToString
            Next
        End If
        str = "select t.*,m.cstimetablename,n.outputcsdriverno from cs_amdrivercorrespond t,cs_cstimetableinf m,cs_result_preparedtraininf n " & _
            "where t.mdrivertimetableid=m.cstimetableid and t.adrivertimetableid='" & strQCurCSPlanID & "' and t.mdrivertimetableid<>'" & strQCurCSPlanID & _
            "' and t.adriverno='" & csd.CSdriverNo & "' and t.mdrivertimetableid=n.cstimetableid and t.mdriverno=n.name and n.dutysort='早班'"
        temtab = ReadData(str)
        If temtab IsNot Nothing AndAlso temtab.Rows.Count > 0 Then
            For Each row As DataRow In temtab.Rows
                CurPos += 1
                sheet.Range(sheet.Cells(CurPos, 3), sheet.Cells(CurPos, 9)).Merge()
                sheet.Cells(CurPos, 3) = "转" & row.Item("cstimetablename").ToString & "见" & row.Item("outputcsdriverno").ToString
            Next
        End If
        temtab.Dispose()
        DrawNightDataToExcel = Startrow + 14 + 2
    End Function

    ''回库从早到晚
    Private Sub sortYebanDriverXiabanTime(ByVal tempYebanCSDrivers() As CSDriver)
        Dim i, j As Integer
        Dim temp As CSDriver
        For i = 1 To UBound(tempYebanCSDrivers)
            For j = i + 1 To UBound(tempYebanCSDrivers)
                If tempYebanCSDrivers(i).CSLinkTrain(UBound(tempYebanCSDrivers(i).CSLinkTrain)).CulEndTime > tempYebanCSDrivers(j).CSLinkTrain(UBound(tempYebanCSDrivers(j).CSLinkTrain)).CulEndTime Then
                    temp = tempYebanCSDrivers(i)
                    tempYebanCSDrivers(i) = tempYebanCSDrivers(j)
                    tempYebanCSDrivers(j) = temp
                End If
            Next
        Next

    End Sub

    ''出库从早到晚
    Private Sub sortZaobanDriverXiabanTime(ByVal tempZaobanCSDrivers() As CSDriver)
        Dim i, j As Integer
        Dim temp As CSDriver
        For i = 1 To UBound(tempZaobanCSDrivers)
            For j = i + 1 To UBound(tempZaobanCSDrivers)
                If tempZaobanCSDrivers(i).CSLinkTrain(1).CulStartTime > tempZaobanCSDrivers(j).CSLinkTrain(1).CulStartTime Then
                    temp = tempZaobanCSDrivers(i)
                    tempZaobanCSDrivers(i) = tempZaobanCSDrivers(j)
                    tempZaobanCSDrivers(j) = temp
                End If
            Next
        Next

    End Sub

    Private Sub 重新编号BToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call AddUnReDoInfo(True)
        Call ReFromDriverIndex()
        Call CheckUnDoAndReDoState()
        MsgBox("重新编号完成！", MsgBoxStyle.OkOnly, "编号完成")
    End Sub

    Private Sub 车底信息CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 车底信息CToolStripMenuItem.Click
        If CSTimeTablePara.nPubTrain > 0 AndAlso CSTimeTablePara.nPubCheDi > 0 AndAlso CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSdriverNo <> "#" Then
            Dim nf As New frmCSdriverInf
            nf.ParentWindow = Me
            nf.dataGrid.Rows.Add("1", "乘务员编号", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSdriverNo)
            nf.dataGrid.Rows.Add("2", "输出编号", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).OutPutCSdriverNo)
            nf.dataGrid.Rows.Add("3", "乘务员班种", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).DutySort)
            nf.dataGrid.Rows.Add("4", "所属区域", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).BelongArea)
            nf.dataGrid.Rows.Add("5", "工作时间", BeTime(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).WorkTime))
            nf.dataGrid.Rows.Add("6", "驾驶里程", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).DriveDistance)
            nf.dataGrid.Rows.Add("7", "出勤地点", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(1).StartStaName)
            nf.dataGrid.Rows.Add("8", "退勤地点", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain)).EndStaName)
            nf.dataGrid.Rows.Add("9", "显示颜色", "")
            nf.dataGrid.Rows(8).Cells("信息值").Style.BackColor = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).ShowColor
            If CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedMorDriver IsNot Nothing AndAlso CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedMorDriver.CSdriverNo <> "#" Then
                nf.dataGrid.Rows.Add("10", "衔接早班表号", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedMorDriver.CSdriverNo)
                nf.dataGrid.Rows.Add("11", "早班驾驶里程", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedMorDriver.DriveDistance)
                nf.dataGrid.Rows.Add("12", "早班出勤地点", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedMorDriver.CSLinkTrain(1).StartStaName)
                nf.dataGrid.Rows.Add("13", "早班退勤地点", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedMorDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedMorDriver.CSLinkTrain)).EndStaName)
                nf.dataGrid.Rows.Add("14", "总驾驶里程", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedMorDriver.DriveDistance + CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).DriveDistance)
            ElseIf CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedNightDriver IsNot Nothing AndAlso CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedNightDriver.CSdriverNo <> "#" Then
                nf.dataGrid.Rows.Add("10", "衔接夜班表号", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedNightDriver.CSdriverNo)
                nf.dataGrid.Rows.Add("11", "夜班驾驶里程", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedNightDriver.DriveDistance)
                nf.dataGrid.Rows.Add("12", "夜班出勤地点", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedNightDriver.CSLinkTrain(1).StartStaName)
                nf.dataGrid.Rows.Add("13", "夜班退勤地点", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedNightDriver.CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedNightDriver.CSLinkTrain)).EndStaName)
                nf.dataGrid.Rows.Add("14", "总驾驶里程", CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedNightDriver.DriveDistance + CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).DriveDistance)
            End If
            nf.dataGrid.Columns(0).ReadOnly = True
            nf.dataGrid.Columns(1).ReadOnly = True
            nf.ShowDialog()
        End If
    End Sub

    Public Sub 撤销tolStripUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 撤销tolStripUndo.Click
        Dim sfFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
        Dim filePath As String = System.Windows.Forms.Application.StartupPath & "\UndoFile\"
        Dim index As Integer = -1
        Dim i As Integer = 0
        While True                      '确定撤销文件名称
            Dim filename As String = filePath & i.ToString & ".dat"
            If File.Exists(filename) Then
                i += 1
            Else
                index = i - 1
                Exit While
            End If
        End While
        If index >= 0 Then
            Call AddUnReDoInfo(False)
            Dim filename As String = filePath & index.ToString & ".dat"
            Dim fStream As New FileStream(filename, FileMode.Open)
            CSTrainsAndDrivers = sfFormatter.Deserialize(fStream)
            fStream.Close()
            File.Delete(filename)
            Call CSRefreshDiagram()
            Call ListAllViewInfo()
        End If
        Call CheckUnDoAndReDoState()
    End Sub

    Private Sub 重复tolStripRedo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 重复tolStripRedo.Click
        Dim sfFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
        Dim filePath As String = System.Windows.Forms.Application.StartupPath & "\RedoFile\"
        Dim index As Integer = -1
        Dim i As Integer = 0
        While True                      '确定撤销文件名称
            Dim filename As String = filePath & i.ToString & ".dat"
            If File.Exists(filename) Then
                i += 1
            Else
                index = i - 1
                Exit While
            End If
        End While
        If index >= 0 Then
            Call AddUnReDoInfo(True)
            Dim filename As String = filePath & index.ToString & ".dat"
            Dim fStream As New FileStream(filename, FileMode.Open)
            CSTrainsAndDrivers = sfFormatter.Deserialize(fStream)
            fStream.Close()
            File.Delete(filename)
            Call CSRefreshDiagram()
            Call ListAllViewInfo()
        End If
        Call CheckUnDoAndReDoState()
    End Sub

    Public Sub CheckUnDoAndReDoState()
        Dim UndoDir As New DirectoryInfo(System.Windows.Forms.Application.StartupPath & "\UndoFile\")
        Dim RedoDir As New DirectoryInfo(System.Windows.Forms.Application.StartupPath & "\RedoFile\")
        If UndoDir.GetFiles().Length = 0 Then
            撤销tolStripUndo.Enabled = False
            撤销CToolStripMenuItem.Enabled = False
        Else
            撤销tolStripUndo.Enabled = True
            撤销CToolStripMenuItem.Enabled = True
        End If
        If RedoDir.GetFiles().Length = 0 Then
            重复tolStripRedo.Enabled = False
            回复RToolStripMenuItem.Enabled = False
        Else
            重复tolStripRedo.Enabled = True
            回复RToolStripMenuItem.Enabled = True
        End If
    End Sub

    Public Sub ClearUnDoAndReDoFile()
        Dim UndoDir As New DirectoryInfo(System.Windows.Forms.Application.StartupPath & "\UndoFile\")
        Dim RedoDir As New DirectoryInfo(System.Windows.Forms.Application.StartupPath & "\RedoFile\")
        Dim UndoFiles() As FileInfo = UndoDir.GetFiles()
        Dim RedoFiles() As FileInfo = RedoDir.GetFiles()
        For Each info As FileInfo In UndoFiles
            File.Delete(info.FullName)
        Next
        For Each info As FileInfo In RedoFiles
            File.Delete(info.FullName)
        Next
        Call CheckUnDoAndReDoState()
    End Sub

    Private Sub 撤销CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 撤销CToolStripMenuItem.Click
        Call 撤销tolStripUndo_Click(Nothing, Nothing)
    End Sub

    Private Sub 回复RToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 回复RToolStripMenuItem.Click
        Call 重复tolStripRedo_Click(Nothing, Nothing)
    End Sub

    Private Sub 删除该乘务员DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除该乘务员DToolStripMenuItem.Click
        If CSTimeTablePara.nPubTrain > 0 AndAlso CSTimeTablePara.nPubCheDi > 0 AndAlso CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSdriverNo <> "#" Then
            If MsgBox("确定删除该司机吗？", vbQuestion + vbYesNo + vbDefaultButton2, "确认操作") = vbYes Then
                Call AddUnReDoInfo(True)
                Dim tempDri As CSDriver = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi)
                For i As Integer = 1 To UBound(tempDri.CSLinkTrain)
                    tempDri.CSLinkTrain(i).IsLinked = False
                    tempDri.CSLinkTrain(i).OverDutySort = ""
                Next
                If tempDri.LinkedMorDriver IsNot Nothing Then
                    tempDri.LinkedMorDriver.LinkedNightDriver = Nothing
                    tempDri.LinkedMorDriver = Nothing
                End If
                If tempDri.LinkedNightDriver IsNot Nothing Then
                    tempDri.LinkedNightDriver.LinkedMorDriver = Nothing
                    tempDri.LinkedNightDriver = Nothing
                End If
                Call RemoveDriver(CSTimeTablePara.nPubCheDi)
                Call ListAllViewInfo()
                Call CSRefreshDiagram()
                Call CheckUnDoAndReDoState()
            End If
        End If
    End Sub

    Private Sub 出退勤地点统计IToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 出退勤地点统计IToolStripMenuItem.Click
        Dim nf As New FrmOnOffPlace
        Dim ncount As Integer = 1
        Dim UnAgreedNum As Integer = 0
        Dim OnDutyInfolist As New List(Of OnDutyInfo)
        Dim ErrorInfo As String = ""
        For Each tempDri As CSDriver In CSTrainsAndDrivers.DayDrivers
            nf.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, tempDri.DutySort, tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                BeTime(tempDri.WorkTime), tempDri.DriveDistance)
            If tempDri.CSLinkTrain(1).FirstStation.IsYard = False AndAlso IsDayDutyOffPlace(tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).RoutingName, tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(1).RoutingName, tempDri.CSLinkTrain(1).StartTime) = False Then
                nf.DGVMain.Rows(ncount - 1).Cells("出退勤一致").Value = "否"
                UnAgreedNum += 1
                For i As Integer = 0 To nf.DGVMain.ColumnCount - 1
                    nf.DGVMain.Rows(ncount - 1).Cells(i).Style.BackColor = Color.Red
                Next
            Else
                nf.DGVMain.Rows(ncount - 1).Cells("出退勤一致").Value = "是"
            End If
            Dim ifAdd As Boolean = False
            For Each info As OnDutyInfo In OnDutyInfolist
                If info.DutySort = tempDri.DutySort AndAlso info.OnDutyPlace = tempDri.CSLinkTrain(1).StartStaName Then
                    info.NCount += 1
                    ifAdd = True
                    Exit For
                End If
            Next
            If ifAdd = False Then
                Dim tempInfo As New OnDutyInfo(tempDri.DutySort, tempDri.CSLinkTrain(1).StartStaName, 1)
                OnDutyInfolist.Add(tempInfo)
            End If
            ncount += 1
        Next
        For Each tempDri As CSDriver In CSTrainsAndDrivers.NightDrivers
            If tempDri.LinkedMorDriver IsNot Nothing AndAlso tempDri.LinkedMorDriver.CSdriverNo <> "#" Then
                nf.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, tempDri.DutySort, tempDri.CSLinkTrain(1).StartStaName, _
                                                tempDri.LinkedMorDriver.CSLinkTrain(UBound(tempDri.LinkedMorDriver.CSLinkTrain)).EndStaName, BeTime(tempDri.WorkTime + tempDri.LinkedMorDriver.WorkTime), _
                                                tempDri.DriveDistance + tempDri.LinkedMorDriver.DriveDistance)
                If IsDayDutyOffPlace(tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(1).RoutingName, tempDri.LinkedMorDriver.CSLinkTrain(UBound(tempDri.LinkedMorDriver.CSLinkTrain)).EndStaName, tempDri.LinkedMorDriver.CSLinkTrain(UBound(tempDri.LinkedMorDriver.CSLinkTrain)).RoutingName, tempDri.LinkedMorDriver.CSLinkTrain(UBound(tempDri.LinkedMorDriver.CSLinkTrain)).EndTime) = False Then
                    nf.DGVMain.Rows(ncount - 1).Cells("出退勤一致").Value = "否"
                    UnAgreedNum += 1
                    For i As Integer = 0 To nf.DGVMain.ColumnCount - 1
                        nf.DGVMain.Rows(ncount - 1).Cells(i).Style.BackColor = Color.Red
                    Next
                Else
                    nf.DGVMain.Rows(ncount - 1).Cells("出退勤一致").Value = "是"
                End If
                Dim ifAdd As Boolean = False
                For Each info As OnDutyInfo In OnDutyInfolist
                    If info.DutySort = tempDri.DutySort AndAlso info.OnDutyPlace = tempDri.CSLinkTrain(1).StartStaName Then
                        info.NCount += 1
                        ifAdd = True
                        Exit For
                    End If
                Next
                If ifAdd = False Then
                    Dim tempInfo As New OnDutyInfo(tempDri.DutySort, tempDri.CSLinkTrain(1).StartStaName, 1)
                    OnDutyInfolist.Add(tempInfo)
                End If
                ncount += 1
            Else
                If ErrorInfo = "" Then
                    ErrorInfo = "部分夜班没有衔接早班任务:" & vbCrLf & "+++++++++++++++++++++++++++++++++++++++++++++" & vbCrLf & tempDri.CSdriverNo & vbCrLf & "出勤地点:" & tempDri.CSLinkTrain(1).StartStaName & vbCrLf & "退勤地点:" & _
                        tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName & vbCrLf & "+++++++++++++++++++++++++++++++++++++++++++++"
                Else
                    ErrorInfo &= vbCrLf & "+++++++++++++++++++++++++++++++++++++++++++++" & vbCrLf & tempDri.CSdriverNo & vbCrLf & "出勤地点:" & tempDri.CSLinkTrain(1).StartStaName & vbCrLf & "退勤地点:" & _
                        tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName & vbCrLf & "+++++++++++++++++++++++++++++++++++++++++++++"
                End If
            End If
        Next
        For Each tempDri As CSDriver In CSTrainsAndDrivers.CDayDrivers
            nf.DGVMain.Rows.Add(ncount, tempDri.CSdriverNo, tempDri.OutPutCSdriverNo, tempDri.DutySort, tempDri.CSLinkTrain(1).StartStaName, tempDri.CSLinkTrain(UBound(tempDri.CSLinkTrain)).EndStaName, _
                                BeTime(tempDri.WorkTime), tempDri.DriveDistance)
            nf.DGVMain.Rows(ncount - 1).Cells("出退勤一致").Value = "是"
            Dim ifAdd As Boolean = False
            For Each info As OnDutyInfo In OnDutyInfolist
                If info.DutySort = tempDri.DutySort AndAlso info.OnDutyPlace = tempDri.CSLinkTrain(1).StartStaName Then
                    info.NCount += 1
                    ifAdd = True
                    Exit For
                End If
            Next
            If ifAdd = False Then
                Dim tempInfo As New OnDutyInfo(tempDri.DutySort, tempDri.CSLinkTrain(1).StartStaName, 1)
                OnDutyInfolist.Add(tempInfo)
            End If
            ncount += 1
        Next
        Dim NoteStr As String = "出退勤地点不一致:" & UnAgreedNum.ToString & vbCrLf
        For Each info As OnDutyInfo In OnDutyInfolist
            NoteStr &= info.DutySort & """" & info.OnDutyPlace & """出勤:" & info.NCount & vbCrLf
        Next
        NoteStr = NoteStr.Trim(vbCrLf)
        nf.TXTNote.Text = NoteStr
        nf.Show()
        If ErrorInfo <> "" Then
            If MsgBox("部分早班没有衔接夜班任务,是否查看提示信息？", MsgBoxStyle.OkCancel + MsgBoxStyle.Information, "提醒") = MsgBoxResult.Ok Then
                frmInfor.txtInfor.Text = ErrorInfo
                frmInfor.Show()
            End If
        End If
    End Sub

    Public Class OnDutyInfo        '出勤信息
        Public DutySort As String
        Public OnDutyPlace As String
        Public NCount As Integer
        Public Sub New(ByVal _dutysort As String, ByVal _dutyplace As String, ByVal _count As String)
            DutySort = _dutysort
            OnDutyPlace = _dutyplace
            NCount = _count
        End Sub
    End Class

    Private Sub 钓鱼图DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 钓鱼图DToolStripMenuItem.Click
        If CSTrainsAndDrivers.DayDrivers.Count = 0 AndAlso CSTrainsAndDrivers.MorningDrivers.Count = 0 AndAlso CSTrainsAndDrivers.CDayDrivers.Count = 0 Then
            MsgBox("未找到打开的乘务计划，请先打开需输出的乘务计划！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
            Exit Sub
        End If
        FrmFishDiagramSet.CurLineName = strCurlineID
        FrmFishDiagramSet.BtnInput.Visible = False
        FrmFishDiagramSet.Button1.Visible = True
        FrmFishDiagramSet.ShowDialog()
    End Sub

    Private Sub 安排随乘列车DHToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 安排随乘列车DHToolStripMenuItem.Click
        If CSTimeTablePara.nPubTrain <> 0 Then
            Dim CSDriverID As Integer = CheCiToDriverID(CSTimeTablePara.nPubTrain)
            If CSDriverID = 0 Then
                MsgBox("该列车还未安排驾驶司机,请先安排司机！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                Exit Sub
            ElseIf CSDriverID > 0 And CSTrainsAndDrivers.CSDrivers(CSDriverID).CSdriverNo = "#" Then
                MsgBox("该列车还未安排驾驶司机,请先安排司机！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                Exit Sub
            ElseIf CSDriverID > 0 And CSTrainsAndDrivers.CSDrivers(CSDriverID).CSdriverNo <> "#" Then
                If CSTrainsAndDrivers.CSDrivers(CSDriverID).DutySort = Nothing OrElse CSTrainsAndDrivers.CSDrivers(CSDriverID).DutySort.Trim = "" Then
                    MsgBox("未确定该司机班种,请先设置班种再安排随乘车！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                    Exit Sub
                Else
                    Call AddUnReDoInfo(True)
                    Select Case CSTrainsAndDrivers.CSDrivers(CSDriverID).DutySort
                        Case "早班"
                            If CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1).FirstStation.IsYard = False Then
                                frmInputBox.Text = "安排随乘出勤列车"
                                frmInputBox.labTitle.Text = "出勤车场:"
                                frmInputBox.cmbText.Visible = True
                                frmInputBox.txtText.Visible = False
                                frmInputBox.cmbText.Items.Clear()
                                For i As Integer = 1 To UBound(StationInf)
                                    If StationInf(i).sStaStyle = "车场" Then
                                        frmInputBox.cmbText.Items.Add(StationInf(i).sStationName)
                                    End If
                                Next
                                If frmInputBox.cmbText.Items.Count > 0 Then
                                    frmInputBox.cmbText.SelectedIndex = 0
                                End If
                                If frmInputBox.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                                    Dim tempMergeTrain As MergedCSLinkTrain = GetDeadHead(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1), StrInputBoxCombText)
                                    If tempMergeTrain IsNot Nothing Then
                                        CSTrainsAndDrivers.CSDrivers(CSDriverID).AddReMergedTrain(tempMergeTrain, True)
                                        CSTrainsAndDrivers.CSDrivers(CSDriverID).ListSuichengMergedCSLinkTrain.Add(tempMergeTrain)
                                    Else
                                        MsgBox("没有找到出勤随乘车!", MsgBoxStyle.OkOnly, "提示")
                                    End If
                                End If
                            Else
                                MsgBox("该任务无法安排随乘列车！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
                            End If
                        Case "白班"
                            If IsDayDutyOnPlace(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1).StartStaName, CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1).UpOrDown, CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1).RoutingName, CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1).StartTime) = False Then
                                Dim tempCSLinkTrain As CSLinkTrain = GetDayDriDeadHead(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1))
                                If tempCSLinkTrain IsNot Nothing Then
                                    CSTrainsAndDrivers.CSDrivers(CSDriverID).ReAddTrain(tempCSLinkTrain, True)
                                    CSTrainsAndDrivers.CSDrivers(CSDriverID).ListSuichengCSLinkTrain.Add(tempCSLinkTrain)
                                Else
                                    If CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1).FirstStation.IsYard = False Then
                                        frmInputBox.Text = "安排随乘出勤列车"
                                        frmInputBox.labTitle.Text = "出勤车场:"
                                        frmInputBox.cmbText.Visible = True
                                        frmInputBox.txtText.Visible = False
                                        frmInputBox.cmbText.Items.Clear()
                                        For i As Integer = 1 To UBound(StationInf)
                                            If StationInf(i).sStaStyle = "车场" Then
                                                frmInputBox.cmbText.Items.Add(StationInf(i).sStationName)
                                            End If
                                        Next
                                        If frmInputBox.cmbText.Items.Count > 0 Then
                                            frmInputBox.cmbText.SelectedIndex = 0
                                        End If
                                        If frmInputBox.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                                            Dim tempMergeTrain As MergedCSLinkTrain = GetDeadHead(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1), StrInputBoxCombText)
                                            If tempMergeTrain IsNot Nothing Then
                                                CSTrainsAndDrivers.CSDrivers(CSDriverID).AddReMergedTrain(tempMergeTrain, True)
                                                CSTrainsAndDrivers.CSDrivers(CSDriverID).ListSuichengMergedCSLinkTrain.Add(tempMergeTrain)
                                            Else
                                                MsgBox("没有找到出勤随乘车!", MsgBoxStyle.OkOnly, "提示")
                                            End If
                                        End If
                                    Else
                                        MsgBox("该任务无法安排随乘列车！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
                                    End If
                                End If
                            End If
                        Case "日勤班"
                            If CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1).FirstStation.IsYard = False Then
                                frmInputBox.Text = "安排随乘出勤列车"
                                frmInputBox.labTitle.Text = "出勤车场:"
                                frmInputBox.cmbText.Visible = True
                                frmInputBox.txtText.Visible = False
                                frmInputBox.cmbText.Items.Clear()
                                For i As Integer = 1 To UBound(StationInf)
                                    If StationInf(i).sStaStyle = "车场" Then
                                        frmInputBox.cmbText.Items.Add(StationInf(i).sStationName)
                                    End If
                                Next
                                If frmInputBox.cmbText.Items.Count > 0 Then
                                    frmInputBox.cmbText.SelectedIndex = 0
                                End If
                                If frmInputBox.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                                    Dim tempMergeTrain As MergedCSLinkTrain = GetDeadHead(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1), StrInputBoxCombText)
                                    If tempMergeTrain IsNot Nothing Then
                                        CSTrainsAndDrivers.CSDrivers(CSDriverID).AddReMergedTrain(tempMergeTrain, True)
                                        CSTrainsAndDrivers.CSDrivers(CSDriverID).ListSuichengMergedCSLinkTrain.Add(tempMergeTrain)
                                    Else
                                        MsgBox("没有找到出勤随乘车!", MsgBoxStyle.OkOnly, "提示")
                                    End If
                                End If
                            End If
                        Case "夜班"
                            If IsNightDutyon(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1).StartStaName, CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1).RoutingName, CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1).StartTime) = False Then
                                Dim tempCSLinkTrain As CSLinkTrain = GetDayDriDeadHead(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1))
                                If tempCSLinkTrain IsNot Nothing Then
                                    CSTrainsAndDrivers.CSDrivers(CSDriverID).ReAddTrain(tempCSLinkTrain, True)
                                    CSTrainsAndDrivers.CSDrivers(CSDriverID).ListSuichengCSLinkTrain.Add(tempCSLinkTrain)
                                End If
                            End If
                    End Select
                    Call CheckUnDoAndReDoState()
                    Call CSRefreshDiagram()
                    Call ListAllViewInfo()
                End If
            End If
        End If
    End Sub

    Private Sub 安排随乘退勤列车TToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 安排随乘退勤列车TToolStripMenuItem.Click
        If CSTimeTablePara.nPubTrain <> 0 Then
            Dim CSDriverID As Integer = CheCiToDriverID(CSTimeTablePara.nPubTrain)
            If CSDriverID = 0 Then
                MsgBox("该列车还未安排驾驶司机,请先安排司机！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                Exit Sub
            ElseIf CSDriverID > 0 And CSTrainsAndDrivers.CSDrivers(CSDriverID).CSdriverNo = "#" Then
                MsgBox("该列车还未安排驾驶司机,请先安排司机！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                Exit Sub
            ElseIf CSDriverID > 0 And CSTrainsAndDrivers.CSDrivers(CSDriverID).CSdriverNo <> "#" Then
                If CSTrainsAndDrivers.CSDrivers(CSDriverID).DutySort = Nothing OrElse CSTrainsAndDrivers.CSDrivers(CSDriverID).DutySort.Trim = "" Then
                    MsgBox("未确定该司机班种,请先设置班种再安排随乘车！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                    Exit Sub
                Else
                    Call AddUnReDoInfo(True)
                    Select Case CSTrainsAndDrivers.CSDrivers(CSDriverID).DutySort
                        Case "早班" '早班退勤=白班出勤
                            If IsDayDutyOnPlace(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain)).EndStaName, CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain)).UpOrDown, CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain)).RoutingName, CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain)).EndTime) = False Then
                                Dim tempCSLinkTrain As CSLinkTrain = GetDayDriDeadHeadTo(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain)))
                                If tempCSLinkTrain IsNot Nothing Then
                                    CSTrainsAndDrivers.CSDrivers(CSDriverID).AddTrain(tempCSLinkTrain, True)
                                End If
                            End If
                        Case "白班"
                            If IsDayDutyOffPlace(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain)).EndStaName, CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain)).RoutingName, CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1).StartStaName, CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1).RoutingName, _
                                            CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(1).StartTime) = False Then
                                Dim tempCSLinkTrain As CSLinkTrain = GetDayDriDeadHeadTo(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain)))
                                If tempCSLinkTrain IsNot Nothing Then
                                    CSTrainsAndDrivers.CSDrivers(CSDriverID).AddTrain(tempCSLinkTrain, True)
                                End If
                            End If
                        Case "日勤班"
                            If CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain)).SecondStation.IsYard = False Then
                                frmInputBox.Text = "安排随乘退勤列车"
                                frmInputBox.labTitle.Text = "退勤车场:"
                                frmInputBox.cmbText.Visible = True
                                frmInputBox.txtText.Visible = False
                                frmInputBox.cmbText.Items.Clear()
                                For i As Integer = 1 To UBound(StationInf)
                                    If StationInf(i).sStaStyle = "车场" Then
                                        frmInputBox.cmbText.Items.Add(StationInf(i).sStationName)
                                    End If
                                Next
                                If frmInputBox.cmbText.Items.Count > 0 Then
                                    frmInputBox.cmbText.SelectedIndex = 0
                                End If
                                If frmInputBox.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                                    Dim tempCSLinkTrain As MergedCSLinkTrain = GetDeadHeadTo(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain)), StrInputBoxCombText)
                                    If tempCSLinkTrain IsNot Nothing Then
                                        CSTrainsAndDrivers.CSDrivers(CSDriverID).AddMergedTrain(tempCSLinkTrain, True)
                                    Else
                                        MsgBox("没有找到退勤随乘车!")
                                    End If
                                End If
                            End If
                        Case "夜班"
                            If CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain)).SecondStation.IsYard = False Then
                                frmInputBox.Text = "安排随乘退勤列车"
                                frmInputBox.labTitle.Text = "退勤车场:"
                                frmInputBox.cmbText.Visible = True
                                frmInputBox.txtText.Visible = False
                                frmInputBox.cmbText.Items.Clear()
                                For i As Integer = 1 To UBound(StationInf)
                                    If StationInf(i).sStaStyle = "车场" Then
                                        frmInputBox.cmbText.Items.Add(StationInf(i).sStationName)
                                    End If
                                Next
                                If frmInputBox.cmbText.Items.Count > 0 Then
                                    frmInputBox.cmbText.SelectedIndex = 0
                                End If
                                If frmInputBox.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                                    Dim tempCSLinkTrain As MergedCSLinkTrain = GetDeadHeadTo(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain)), StrInputBoxCombText)
                                    If tempCSLinkTrain IsNot Nothing Then
                                        CSTrainsAndDrivers.CSDrivers(CSDriverID).AddMergedTrain(tempCSLinkTrain, True)
                                    Else
                                        MsgBox("没有找到退勤随乘车!")
                                    End If
                                End If
                            End If
                    End Select
                    Call CheckUnDoAndReDoState()
                    Call CSRefreshDiagram()
                    Call ListAllViewInfo()
                End If
            End If
        End If
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click

        Call reNameCSDrivers()
        SortDriverbyLeaveWork("夜班")
        'Call SortCSDriversByOutPutNum()
        Call CSRefreshDiagram()
        Call ListAllViewInfo()
    End Sub

    Public Sub reNameCSDrivers()
        If UBound(CSTrainsAndDrivers.CSDrivers) > 1 Then
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers) - 1
                For j As Integer = i + 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                    If CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain.Count > 1 And CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain.Count > 1 Then
                        If AddLitterTime(CSTrainsAndDrivers.CSDrivers(j).BeginWorkTime) < AddLitterTime(CSTrainsAndDrivers.CSDrivers(i).BeginWorkTime) Then
                            Dim tempdri As CSDriver = CSTrainsAndDrivers.CSDrivers(j)
                            CSTrainsAndDrivers.CSDrivers(j) = CSTrainsAndDrivers.CSDrivers(i)
                            CSTrainsAndDrivers.CSDrivers(i) = tempdri
                        End If
                    End If
                Next
            Next
            Dim sortDuty As New Dictionary(Of String, Integer)
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                CSTrainsAndDrivers.CSDrivers(i).CSDriverID = i
                If sortDuty.Keys.Contains(CSTrainsAndDrivers.CSDrivers(i).DutySort) = False Then
                    sortDuty.Add(CSTrainsAndDrivers.CSDrivers(i).DutySort, 0)
                End If
                CSTrainsAndDrivers.CSDrivers(i).CSdriverNo = CSTrainsAndDrivers.CSDrivers(i).DutySort & (sortDuty(CSTrainsAndDrivers.CSDrivers(i).DutySort) + 1).ToString("00")
                CSTrainsAndDrivers.CSDrivers(i).OutPutCSdriverNo = CSTrainsAndDrivers.CSDrivers(i).DutySort & (sortDuty(CSTrainsAndDrivers.CSDrivers(i).DutySort) + 1).ToString("00")
                sortDuty(CSTrainsAndDrivers.CSDrivers(i).DutySort) += 1
            Next
            CSTrainsAndDrivers.MorningDrivers.Clear()
            CSTrainsAndDrivers.DayDrivers.Clear()
            CSTrainsAndDrivers.CDayDrivers.Clear()
            CSTrainsAndDrivers.NightDrivers.Clear()
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                Select Case CSTrainsAndDrivers.CSDrivers(i).DutySort
                    Case "早班"
                        CSTrainsAndDrivers.MorningDrivers.Add(CSTrainsAndDrivers.CSDrivers(i))
                    Case "白班"
                        CSTrainsAndDrivers.DayDrivers.Add(CSTrainsAndDrivers.CSDrivers(i))
                    Case "夜班"
                        CSTrainsAndDrivers.NightDrivers.Add(CSTrainsAndDrivers.CSDrivers(i))
                    Case "日勤班"
                        CSTrainsAndDrivers.CDayDrivers.Add(CSTrainsAndDrivers.CSDrivers(i))
                End Select
            Next
            MsgBox("重新排序完成！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "完成")
        End If
    End Sub
   

    Public Sub SelectDriver(ByVal selectDriIndex As Integer)
        CSTimeTablePara.nPubCheDi = selectDriIndex
        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain)
            If CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).IsDeadHeading = False Then
                CSTimeTablePara.nPubTrain = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).CSTrainID
                'Exit For
            End If
        Next
        Call SetCurScrollbarInSelectTrain(CSTimeTablePara.nPubTrain)
        Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
        Me.PicDiagram.Refresh()
        rBmpGraphics = Me.PicDiagram.CreateGraphics()
        Dim tmpPen As Pen
        tmpPen = New Pen(Color.Blue, 2)
        Dim tmpPen1 As Pen = New Pen(Color.DarkSlateGray, 2)
        tmpPen1.DashStyle = Drawing2D.DashStyle.DashDot
        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain)
            If CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).IsDeadHeading = False Then
                Call CSDrawLineInPicture(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).CSTrainID, rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
            Else
                Call CSDrawLineInPicture(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i), rBmpGraphics, tmpPen1, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
            End If
        Next
        If CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedMorDriver IsNot Nothing AndAlso CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedMorDriver.IsPrepareDri = False Then
            For Each train As CSLinkTrain In CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedMorDriver.CSLinkTrain
                If train IsNot Nothing Then
                    If train.IsDeadHeading = False Then
                        Call CSDrawLineInPicture(train.CSTrainID, rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                    Else
                        Call CSDrawLineInPicture(train, rBmpGraphics, tmpPen1, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                    End If
                End If
            Next
        End If
        If CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedNightDriver IsNot Nothing AndAlso CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedNightDriver.IsPrepareDri = False Then
            For Each train As CSLinkTrain In CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedNightDriver.CSLinkTrain
                If train IsNot Nothing Then
                    If train.IsDeadHeading = False Then
                        Call CSDrawLineInPicture(train.CSTrainID, rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                    Else
                        Call CSDrawLineInPicture(train, rBmpGraphics, tmpPen1, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                    End If
                End If
            Next
        End If
        Call CSShowLabInfor(CSTimeTablePara.nPubTrain, Me.labInfor)
    End Sub
    Private Sub 安排关联任务MToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 安排关联任务MToolStripMenuItem.Click
        Dim nTrain As Integer = CSTimeTablePara.nPubTrain
        Dim nDriverId As Integer = CheCiToDriverID(nTrain)
        If nDriverId <> 0 AndAlso CSTrainsAndDrivers.CSDrivers(nDriverId).CSdriverNo <> "#" Then
            frmInputBox.Text = "安排关联任务"
            frmInputBox.labTitle.Text = "选择关联任务:"
            frmInputBox.cmbText.Visible = True
            frmInputBox.txtText.Visible = False
            frmInputBox.cmbText.Items.Clear()
            frmInputBox.cmbText.Text = ""
            Select Case CSTrainsAndDrivers.CSDrivers(nDriverId).DutySort
                Case "早班"
                    If UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
                        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                            If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo <> "" AndAlso CSTrainsAndDrivers.CSDrivers(i).CSdriverNo <> "#" AndAlso CSTrainsAndDrivers.CSDrivers(i).DutySort = "夜班" Then
                                frmInputBox.cmbText.Items.Add(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo)
                            End If
                        Next i
                    End If
                    For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
                        If dri IsNot Nothing AndAlso dri.DutySort = "夜班" Then
                            frmInputBox.cmbText.Items.Add(dri.CSdriverNo)
                        End If
                    Next
                    For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
                        If dri IsNot Nothing AndAlso dri.DutySort = "夜班" Then
                            frmInputBox.cmbText.Items.Add(dri.CSdriverNo)
                        End If
                    Next
                    If CSTrainsAndDrivers.CSDrivers(nDriverId).LinkedNightDriver IsNot Nothing AndAlso CSTrainsAndDrivers.CSDrivers(nDriverId).LinkedNightDriver.DutySort = "夜班" Then
                        frmInputBox.cmbText.Text = CSTrainsAndDrivers.CSDrivers(nDriverId).LinkedNightDriver.CSdriverNo
                    End If
                Case "夜班"
                    If UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
                        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                            If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo <> "" AndAlso CSTrainsAndDrivers.CSDrivers(i).CSdriverNo <> "#" AndAlso CSTrainsAndDrivers.CSDrivers(i).DutySort = "早班" Then
                                frmInputBox.cmbText.Items.Add(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo)
                            End If
                        Next i
                    End If
                    For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
                        If dri IsNot Nothing AndAlso dri.DutySort = "早班" Then
                            frmInputBox.cmbText.Items.Add(dri.CSdriverNo)
                        End If
                    Next
                    For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
                        If dri IsNot Nothing AndAlso dri.DutySort = "早班" Then
                            frmInputBox.cmbText.Items.Add(dri.CSdriverNo)
                        End If
                    Next
                    If CSTrainsAndDrivers.CSDrivers(nDriverId).LinkedMorDriver IsNot Nothing AndAlso CSTrainsAndDrivers.CSDrivers(nDriverId).LinkedMorDriver.DutySort = "早班" Then
                        frmInputBox.cmbText.Text = CSTrainsAndDrivers.CSDrivers(nDriverId).LinkedMorDriver.CSdriverNo
                    End If
                Case Else
                    MsgBox("该司机不能安排关联任务!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                    Exit Sub
            End Select

            If frmInputBox.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                If StrInputBoxCombText <> "" Then
                    Select Case CSTrainsAndDrivers.CSDrivers(nDriverId).DutySort
                        Case "早班"
                            Dim NitDriverNo As String = StrInputBoxCombText
                            Dim dri As CSDriver = CSTrainsAndDrivers.NightDrivers.Find(Function(value As CSDriver)
                                                                                           Return (value.CSdriverNo = NitDriverNo)
                                                                                       End Function)
                            If dri Is Nothing Then
                                dri = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                                      Return (value.CSdriverNo = NitDriverNo AndAlso value.DutySort = "夜班")
                                                                                  End Function)
                                If dri Is Nothing Then
                                    dri = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                           Return (value.CSdriverNo = NitDriverNo AndAlso value.DutySort = "夜班")
                                                                                       End Function)
                                End If
                            End If
                            If dri IsNot Nothing Then
                                If CSTrainsAndDrivers.CSDrivers(nDriverId).LinkedNightDriver IsNot Nothing Then
                                    CSTrainsAndDrivers.CSDrivers(nDriverId).LinkedNightDriver.LinkedMorDriver = Nothing           '首先将对应的夜班所衔接的早班任务清空
                                End If
                                If dri.LinkedMorDriver IsNot Nothing Then
                                    dri.LinkedMorDriver.LinkedNightDriver = Nothing
                                End If
                                CSTrainsAndDrivers.CSDrivers(nDriverId).LinkedNightDriver = dri
                                dri.LinkedMorDriver = CSTrainsAndDrivers.CSDrivers(nDriverId)
                            End If
                        Case "夜班"
                            Dim MorDriverNo As String = StrInputBoxCombText
                            Dim dri As CSDriver = CSTrainsAndDrivers.MorningDrivers.Find(Function(value As CSDriver)
                                                                                             Return (value.CSdriverNo = MorDriverNo)
                                                                                         End Function)
                            If dri Is Nothing Then
                                dri = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                                      Return (value.CSdriverNo = MorDriverNo AndAlso value.DutySort = "早班")
                                                                                  End Function)
                                If dri Is Nothing Then
                                    dri = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                           Return (value.CSdriverNo = MorDriverNo AndAlso value.DutySort = "早班")
                                                                                       End Function)
                                End If
                            End If
                            If dri IsNot Nothing Then
                                If CSTrainsAndDrivers.CSDrivers(nDriverId).LinkedMorDriver IsNot Nothing Then
                                    CSTrainsAndDrivers.CSDrivers(nDriverId).LinkedMorDriver.LinkedNightDriver = Nothing
                                End If
                                If dri.LinkedNightDriver IsNot Nothing Then
                                    dri.LinkedNightDriver.LinkedMorDriver = Nothing
                                End If
                                CSTrainsAndDrivers.CSDrivers(nDriverId).LinkedMorDriver = dri
                                dri.LinkedNightDriver = CSTrainsAndDrivers.CSDrivers(nDriverId)
                            End If
                        Case Else
                            MsgBox("该司机不能安排关联任务!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                            Exit Sub
                    End Select
                End If
            End If
        End If
    End Sub


    Private Sub 正向安排ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 正向安排ToolStripMenuItem.Click
        If CSTimeTablePara.nPubTrain <> 0 Then
            Dim nChedi As Integer = CheCiToDriverID(CSTimeTablePara.nPubTrain)
            If nChedi = 0 Then
                Dim CSTrainID As Integer = CSTimeTablePara.nPubTrain
                If CSTrainsAndDrivers.CSDrivers Is Nothing = True OrElse UBound(CSTrainsAndDrivers.CSDrivers) = 0 Then
                    ReDim CSTrainsAndDrivers.CSDrivers(0)
                    Call AddUnReDoInfo(True)
                    ReDim Preserve CSTrainsAndDrivers.CSDrivers(1)
                    CSTrainsAndDrivers.CSDrivers(1) = New CSDriver()
                    CSTrainsAndDrivers.CSDrivers(1).CSDriverID = 1
                    CSTrainsAndDrivers.CSDrivers(1).CSdriverNo = Format(CInt(1), "00")
                    Dim temmer As MergedCSLinkTrain = GetMergedTrain(CSTrainsAndDrivers.CSLinkTrains(CSTrainID))
                    CSTrainsAndDrivers.CSDrivers(1).AddMergedTrain(temmer)
                Else
                    Call AddUnReDoInfo(True)
                    ReDim Preserve CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers) + 1)
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)) = New CSDriver()
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).DutySort = ""
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID = UBound(CSTrainsAndDrivers.CSDrivers)
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSdriverNo = Format(CInt(UBound(CSTrainsAndDrivers.CSDrivers)), "000")
                    Dim temmer As MergedCSLinkTrain = GetMergedTrain(CSTrainsAndDrivers.CSLinkTrains(CSTrainID))
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).AddMergedTrain(temmer)
                End If
                CSTrainsAndDrivers.OtherDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
                CSRefreshDiagram()
                Dim nf As New FrmHandAssignDriver
                CSTrainsAndDrivers.CurEditDriver = CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers))
                nf.ParentWindow = Me
                nf.Show()
                Call ListAllViewInfo()
                Call CheckUnDoAndReDoState()
            Else
                If CSTrainsAndDrivers.CSDrivers(nChedi).CSdriverNo = "#" Then
                    For Each train As CSLinkTrain In CSTrainsAndDrivers.CSDrivers(nChedi).CSLinkTrain
                        If train IsNot Nothing Then
                            train.IsLinked = True
                        End If
                    Next
                    CSTrainsAndDrivers.CSDrivers(nChedi).CSdriverNo = Format(nChedi, "00")
                End If
                Dim nf As New FrmHandAssignDriver
                CSTrainsAndDrivers.CurEditDriver = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi)
                nf.ParentWindow = Me
                nf.Show()
                Call ListAllViewInfo()
                Call CheckUnDoAndReDoState()
            End If
            Call CSRefreshDiagram()
        End If
    End Sub

    Private Sub 逆向安排ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 逆向安排ToolStripMenuItem.Click
        If CSTimeTablePara.nPubTrain <> 0 Then
            Dim nChedi As Integer = CheCiToDriverID(CSTimeTablePara.nPubTrain)
            If nChedi = 0 Then
                Dim CSTrainID As Integer = CSTimeTablePara.nPubTrain

                If CSTrainsAndDrivers.CSDrivers Is Nothing = True OrElse UBound(CSTrainsAndDrivers.CSDrivers) = 0 Then
                    ReDim CSTrainsAndDrivers.CSDrivers(0)
                    Call AddUnReDoInfo(True)
                    ReDim Preserve CSTrainsAndDrivers.CSDrivers(1)
                    CSTrainsAndDrivers.CSDrivers(1) = New CSDriver()
                    CSTrainsAndDrivers.CSDrivers(1).CSDriverID = 1
                    CSTrainsAndDrivers.CSDrivers(1).CSdriverNo = Format(CInt(1), "00")
                    Dim temmer As MergedCSLinkTrain = GetMergedTrain(CSTrainsAndDrivers.CSLinkTrains(CSTrainID))
                    CSTrainsAndDrivers.CSDrivers(1).AddReMergedTrain(temmer)
                Else
                    Call AddUnReDoInfo(True)
                    ReDim Preserve CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers) + 1)
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)) = New CSDriver()
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).DutySort = ""
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID = UBound(CSTrainsAndDrivers.CSDrivers)
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSdriverNo = Format(CInt(UBound(CSTrainsAndDrivers.CSDrivers)), "000")
                    Dim temmer As MergedCSLinkTrain = GetMergedTrain(CSTrainsAndDrivers.CSLinkTrains(CSTrainID))
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).AddReMergedTrain(temmer)
                End If
                CSTrainsAndDrivers.CSLinkTrains(CSTrainID).IsLinked = True
                CSTrainsAndDrivers.OtherDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
                CSRefreshDiagram()
                Dim nf As New FrmHandAssignDriverR
                CSTrainsAndDrivers.CurEditDriver = CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers))
                nf.ParentWindow = Me
                nf.Show()
                Call ListAllViewInfo()
                Call CheckUnDoAndReDoState()
            Else
                If CSTrainsAndDrivers.CSDrivers(nChedi).CSdriverNo = "#" Then
                    For Each train As CSLinkTrain In CSTrainsAndDrivers.CSDrivers(nChedi).CSLinkTrain
                        If train IsNot Nothing Then
                            train.IsLinked = True
                        End If
                    Next
                    CSTrainsAndDrivers.CSDrivers(nChedi).CSdriverNo = Format(nChedi, "00")
                End If
                Dim nf As New FrmHandAssignDriverR
                CSTrainsAndDrivers.CurEditDriver = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi)
                nf.ParentWindow = Me
                nf.Show()
                Call ListAllViewInfo()
                Call CheckUnDoAndReDoState()
            End If
            Call CSRefreshDiagram()
        End If
    End Sub

    Private Sub 断开随勤列车BToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 断开随勤列车BToolStripMenuItem.Click
        If CSTimeTablePara.nPubTrain <> 0 Then
            Dim CSDriverID As Integer = CheCiToDriverID(CSTimeTablePara.nPubTrain)
            If CSDriverID = 0 Then
                MsgBox("该列车还未安排驾驶司机,请先安排司机！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                Exit Sub
            ElseIf CSDriverID > 0 And CSTrainsAndDrivers.CSDrivers(CSDriverID).CSdriverNo = "#" Then
                MsgBox("该列车还未安排驾驶司机,请先安排司机！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                Exit Sub
            ElseIf CSDriverID > 0 And CSTrainsAndDrivers.CSDrivers(CSDriverID).CSdriverNo <> "#" Then
                Dim tempCSLinkTrains(0) As CSLinkTrain
                For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain)
                    If CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(i).IsDeadHeading = False Then
                        ReDim Preserve tempCSLinkTrains(UBound(tempCSLinkTrains) + 1)
                        tempCSLinkTrains(UBound(tempCSLinkTrains)) = CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain(i)
                    End If
                Next
                CSTrainsAndDrivers.CSDrivers(CSDriverID).CSLinkTrain = tempCSLinkTrains
                Call CSRefreshDiagram()
                Call ListAllViewInfo()
            End If
        End If
    End Sub

    Public Sub SetListView()
        If CSTimeTablePara.nPubTrain > 0 Then
            If CSTimeTablePara.nPubCheDi > 0 Then
                If ListViewDuty.Items.Count > 0 Then
                    For i As Integer = 0 To ListViewDuty.Items.Count - 1
                        If CInt(ListViewDuty.Items(i).SubItems(1).Text) = CSTimeTablePara.nPubCheDi Then
                            ListViewDuty.Select()
                            ListViewDuty.Items(i).Selected = True
                            ListViewDuty.Items(i).EnsureVisible()
                            Call ListCurDutyInfo()
                            Exit For
                        End If
                    Next
                End If
            Else
                If listViewTrain.Items.Count > 0 Then
                    For i As Integer = 0 To listViewTrain.Items.Count - 1
                        If CInt(listViewTrain.Items(i).SubItems(1).Text) = CSTimeTablePara.nPubTrain Then
                            listViewTrain.Select()
                            listViewTrain.Items(i).Selected = True
                            listViewTrain.Items(i).EnsureVisible()
                            Exit For
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Public Sub SetAvaDrisMenu()
        If FangkuanXianzhi = False Then

            指派给乘务员eToolStripMenuItem.DropDownItems.Clear()
            Dim ndriID As Integer = CheCiToDriverID(CSTimeTablePara.nPubTrain)
            If ndriID = 0 Then
                Exit Sub
            End If
            If (CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0) AndAlso (CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).IsLinked = False OrElse (CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).IsLinked AndAlso _
                CSTrainsAndDrivers.CSDrivers(ndriID).CSLinkTrain(1) Is CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain) OrElse (CSTrainsAndDrivers.CSDrivers(ndriID).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(ndriID).CSLinkTrain)) Is CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain)))) Then
                Dim FavaDris As New List(Of CSDriver)
                Dim BavaDris As New List(Of CSDriver)
                If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
                    Dim temmer As MergedCSLinkTrain = GetMergedTrain(CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain))
                    For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                        If CSTrainsAndDrivers.CSDrivers(i).CanDriveTheTrain(temmer) Then
                            FavaDris.Add(CSTrainsAndDrivers.CSDrivers(i))
                        ElseIf CSTrainsAndDrivers.CSDrivers(i).State <> "班后" AndAlso CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).StartStaName = CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).EndStaName AndAlso _
                                                         ((CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).CulStartTime >= CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).CulEndTime + ChangePlaceRestTime(CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).EndStaName, CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).RoutingName, CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).UpOrDown, CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).CulEndTime)) OrElse (CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).nCheDiID = CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).nCheDiID AndAlso CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).CulStartTime - 1200 >= CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).CulEndTime)) Then
                            BavaDris.Add(CSTrainsAndDrivers.CSDrivers(i))
                        End If
                    Next
                End If
                If FavaDris.Count > 1 Then
                    For i As Integer = 0 To FavaDris.Count - 2
                        For j As Integer = i + 1 To FavaDris.Count - 1
                            If FavaDris(j).CSLinkTrain(UBound(FavaDris(j).CSLinkTrain)).CulEndTime > FavaDris(i).CSLinkTrain(UBound(FavaDris(i).CSLinkTrain)).CulEndTime Then
                                Dim tem As CSDriver
                                tem = FavaDris(i)
                                FavaDris(i) = FavaDris(j)
                                FavaDris(j) = tem
                            End If
                        Next
                    Next
                End If
                If BavaDris.Count > 1 Then
                    For i As Integer = 0 To BavaDris.Count - 2
                        For j As Integer = i + 1 To BavaDris.Count - 1
                            If BavaDris(j).BeginWorkTime < BavaDris(i).BeginWorkTime Then
                                Dim tem As CSDriver
                                tem = BavaDris(i)
                                BavaDris(i) = BavaDris(j)
                                BavaDris(j) = tem
                            End If
                        Next
                    Next
                End If
                For Each dri As CSDriver In FavaDris
                    指派给乘务员eToolStripMenuItem.DropDownItems.Add(dri.CSdriverNo)
                Next
                If BavaDris.Count > 0 Then
                    指派给乘务员eToolStripMenuItem.DropDownItems.Add(New ToolStripSeparator)
                    For Each dri As CSDriver In BavaDris
                        指派给乘务员eToolStripMenuItem.DropDownItems.Add(dri.CSdriverNo)
                    Next
                End If
                For Each item As ToolStripItem In 指派给乘务员eToolStripMenuItem.DropDownItems
                    AddHandler item.Click, AddressOf AssignTrainToDriver
                    AddHandler item.MouseEnter, AddressOf EnterDriverItem
                Next
            End If
            If 指派给乘务员eToolStripMenuItem.DropDownItems.Count > 0 Then
                指派给乘务员eToolStripMenuItem.Enabled = True
            Else
                指派给乘务员eToolStripMenuItem.Enabled = False
            End If

        Else

            指派给乘务员eToolStripMenuItem.DropDownItems.Clear()
            Dim ndriID As Integer = CheCiToDriverID(CSTimeTablePara.nPubTrain)
            If (CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0) AndAlso (CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).IsLinked = False OrElse (CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).IsLinked AndAlso _
                CSTrainsAndDrivers.CSDrivers(ndriID).CSLinkTrain(1) Is CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain) OrElse (CSTrainsAndDrivers.CSDrivers(ndriID).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(ndriID).CSLinkTrain)) Is CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain)))) Then
                Dim FavaDris As New List(Of CSDriver)
                Dim BavaDris As New List(Of CSDriver)
                If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
                    Dim temmer As MergedCSLinkTrain = GetMergedTrain(CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain))
                    For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                        If CSTrainsAndDrivers.CSDrivers(i).CanDriveTheTrain(temmer, True) Then
                            FavaDris.Add(CSTrainsAndDrivers.CSDrivers(i))
                        ElseIf CSTrainsAndDrivers.CSDrivers(i).State <> "班后" AndAlso CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).StartStaName = CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).EndStaName AndAlso _
                                                         ((CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).CulStartTime >= CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).CulEndTime + ChangePlaceRestTime(CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).EndStaName, CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).RoutingName, CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).UpOrDown, CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).CulEndTime)) OrElse (CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).nCheDiID = CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).nCheDiID AndAlso CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).CulStartTime - 1200 >= CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).CulEndTime)) Then
                            BavaDris.Add(CSTrainsAndDrivers.CSDrivers(i))
                        End If
                    Next
                End If
                If FavaDris.Count > 1 Then
                    For i As Integer = 0 To FavaDris.Count - 2
                        For j As Integer = i + 1 To FavaDris.Count - 1
                            If FavaDris(j).CSLinkTrain(UBound(FavaDris(j).CSLinkTrain)).CulEndTime > FavaDris(i).CSLinkTrain(UBound(FavaDris(i).CSLinkTrain)).CulEndTime Then
                                Dim tem As CSDriver
                                tem = FavaDris(i)
                                FavaDris(i) = FavaDris(j)
                                FavaDris(j) = tem
                            End If
                        Next
                    Next
                End If
                If BavaDris.Count > 1 Then
                    For i As Integer = 0 To BavaDris.Count - 2
                        For j As Integer = i + 1 To BavaDris.Count - 1
                            If BavaDris(j).BeginWorkTime < BavaDris(i).BeginWorkTime Then
                                Dim tem As CSDriver
                                tem = BavaDris(i)
                                BavaDris(i) = BavaDris(j)
                                BavaDris(j) = tem
                            End If
                        Next
                    Next
                End If
                For Each dri As CSDriver In FavaDris
                    指派给乘务员eToolStripMenuItem.DropDownItems.Add(dri.CSdriverNo)
                Next
                If BavaDris.Count > 0 Then
                    指派给乘务员eToolStripMenuItem.DropDownItems.Add(New ToolStripSeparator)
                    For Each dri As CSDriver In BavaDris
                        指派给乘务员eToolStripMenuItem.DropDownItems.Add(dri.CSdriverNo)
                    Next
                End If
                For Each item As ToolStripItem In 指派给乘务员eToolStripMenuItem.DropDownItems
                    AddHandler item.Click, AddressOf AssignTrainToDriver
                    AddHandler item.MouseEnter, AddressOf EnterDriverItem
                Next
            End If
            If 指派给乘务员eToolStripMenuItem.DropDownItems.Count > 0 Then
                指派给乘务员eToolStripMenuItem.Enabled = True
            Else
                指派给乘务员eToolStripMenuItem.Enabled = False
            End If

        End If
       
    End Sub

    Private Sub AssignTrainToDriver(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim CSDriverNo As String = CType(sender, ToolStripItem).Text
        Dim driverID1 As Integer = 0
        Dim driverID2 As Integer = CheCiToDriverID(CSTimeTablePara.nPubTrain)
        Dim nTrainID1 As Integer = 0
        Dim nTrainID2 As Integer = CSTimeTablePara.nPubTrain
        For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo = CSDriverNo Then
                driverID1 = i
                Exit For
            End If
        Next
        nTrainID1 = CSTrainsAndDrivers.CSDrivers(driverID1).CSLinkTrain(1).CSTrainID
        If driverID1 > 0 Then
            Dim tdri As CSDriver = CSTrainsAndDrivers.CSDrivers(driverID1)
            If CSTrainsAndDrivers.CSDrivers(driverID1).CSLinkTrain(1).StartStaName = CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).EndStaName Then
                nTrainID1 = CSTrainsAndDrivers.CSDrivers(driverID1).CSLinkTrain(1).CSTrainID
            ElseIf CSTrainsAndDrivers.CSDrivers(driverID1).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(driverID1).CSLinkTrain)).EndStaName = CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).StartStaName Then
                nTrainID1 = CSTrainsAndDrivers.CSDrivers(driverID1).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(driverID1).CSLinkTrain)).CSTrainID
            End If
            Call AddUnReDoInfo(True)
            Call HeBinDriverInfo(driverID1, driverID2, nTrainID1, nTrainID2)
            tdri.RefreshState()
            Call CSRefreshDiagram()
            Call ListAllViewInfo()
        End If
    End Sub

    Private Sub EnterDriverItem(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim CSDriverNo As String = CType(sender, ToolStripItem).Text
        Dim driverID1 As Integer = 0
        For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo = CSDriverNo Then
                driverID1 = i
                Exit For
            End If
        Next
        If driverID1 > 0 Then
            Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
            rBmpGraphics = Me.PicDiagram.CreateGraphics()
            rBmpGraphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            Dim tmpPen As Pen
            tmpPen = New Pen(Color.FromArgb(248, 22, 173), 3)
            Dim tmpPen2 As Pen
            tmpPen2 = New Pen(Color.Blue, 2)
            Me.PicDiagram.Refresh()
            Call DrawDriver(rBmpGraphics, tmpPen, driverID1)
            Call DrawTrain(rBmpGraphics, tmpPen2, CSTimeTablePara.nPubTrain)

            Dim TotalDistance As Decimal = 0
            Me.ListViewCurDuty.Items.Clear()
            For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers(driverID1).CSLinkTrain)
                Dim liFile(10) As String
                Dim lvItem As ListViewItem
                liFile(0) = Me.ListViewCurDuty.Items.Count + 1
                liFile(1) = CSTrainsAndDrivers.CSDrivers(driverID1).CSDriverID
                liFile(2) = CSTrainsAndDrivers.CSDrivers(driverID1).CSdriverNo
                liFile(3) = CSTrainsAndDrivers.CSDrivers(driverID1).OutPutCSdriverNo
                liFile(4) = CSTrainsAndDrivers.CSDrivers(driverID1).CSLinkTrain(i).StartStaName
                liFile(5) = BeTime(CSTrainsAndDrivers.CSDrivers(driverID1).CSLinkTrain(i).StartTime)
                liFile(6) = CSTrainsAndDrivers.CSDrivers(driverID1).CSLinkTrain(i).OutputCheCi
                liFile(7) = CSTrainsAndDrivers.CSDrivers(driverID1).CSLinkTrain(i).EndStaName
                liFile(8) = BeTime(CSTrainsAndDrivers.CSDrivers(driverID1).CSLinkTrain(i).EndTime)
                liFile(9) = CSTrainsAndDrivers.CSDrivers(driverID1).CSLinkTrain(i).OffCheCi
                If CSTrainsAndDrivers.CSDrivers(driverID1).CSLinkTrain(i).IsDeadHeading = False Then
                    TotalDistance += CSTrainsAndDrivers.CSDrivers(driverID1).CSLinkTrain(i).distance
                End If
                liFile(10) = TotalDistance
                lvItem = New ListViewItem(liFile)
                Me.ListViewCurDuty.Items.Add(lvItem)
            Next

        End If
    End Sub

    Public Sub DrawDriver(ByVal rBmpGraphics As Graphics, ByVal tmpPen As Pen, ByVal nDriverID As Integer)
        Dim tmpPen1 As Pen = New Pen(Color.DarkSlateGray, 2)
        tmpPen1.DashStyle = Drawing2D.DashStyle.DashDot
        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers(nDriverID).CSLinkTrain)
            If CSTrainsAndDrivers.CSDrivers(nDriverID).CSLinkTrain(i).CSTrainID <> CSTimeTablePara.nPubTrain Then
                If CSTrainsAndDrivers.CSDrivers(nDriverID).CSLinkTrain(i).IsDeadHeading = False Then
                    Call CSDrawLineInPicture(CSTrainsAndDrivers.CSDrivers(nDriverID).CSLinkTrain(i).CSTrainID, rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                Else
                    Call CSDrawLineInPicture(CSTrainsAndDrivers.CSDrivers(nDriverID).CSLinkTrain(i), rBmpGraphics, tmpPen1, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                End If
            End If
        Next
        If CSTrainsAndDrivers.CSDrivers(nDriverID).LinkedMorDriver IsNot Nothing AndAlso CSTrainsAndDrivers.CSDrivers(nDriverID).LinkedMorDriver.IsPrepareDri = False Then
            For Each train As CSLinkTrain In CSTrainsAndDrivers.CSDrivers(nDriverID).LinkedMorDriver.CSLinkTrain
                If train IsNot Nothing Then
                    If train.IsDeadHeading = False Then
                        Call CSDrawLineInPicture(train.CSTrainID, rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                    Else
                        Call CSDrawLineInPicture(train, rBmpGraphics, tmpPen1, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                    End If
                End If
            Next
        End If
        If CSTrainsAndDrivers.CSDrivers(nDriverID).LinkedNightDriver IsNot Nothing AndAlso CSTrainsAndDrivers.CSDrivers(nDriverID).LinkedNightDriver.IsPrepareDri = False Then
            For Each train As CSLinkTrain In CSTrainsAndDrivers.CSDrivers(nDriverID).LinkedNightDriver.CSLinkTrain
                If train IsNot Nothing Then
                    If train.IsDeadHeading = False Then
                        Call CSDrawLineInPicture(train.CSTrainID, rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                    Else
                        Call CSDrawLineInPicture(train, rBmpGraphics, tmpPen1, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                    End If
                End If
            Next
        End If
    End Sub

    Public Sub DrawTrain(ByVal rBmpGraphics As Graphics, ByVal tmpPen As Pen, ByVal TrainID As Integer)
        Call CSDrawLineInPicture(CSTrainsAndDrivers.CSLinkTrains(TrainID), rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
    End Sub

    Private Sub 班中ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 班中ToolStripMenuItem.Click
        AddUnReDoInfo(True)
        CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).State = "班中"
        Call CSRefreshDiagram()
        Call ListAllViewInfo()
    End Sub

    Private Sub 用餐ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 用餐ToolStripMenuItem.Click
        AddUnReDoInfo(True)
        Dim dinneritem As typeDinnerStation = Nothing
        Dim sdri As CSDriver = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi)
        For i As Integer = 1 To UBound(sysDinnerStation)
            If sysDinnerStation(i).dutySort = sdri.DutySort AndAlso sysDinnerStation(i).DinnerStationName = sdri.CSLinkTrain(UBound(sdri.CSLinkTrain)).EndStaName AndAlso sysDinnerStation(i).Routing = sdri.CSLinkTrain(UBound(sdri.CSLinkTrain)).RoutingName Then
                If AddLitterTime(sysDinnerStation(i).dinnerStartTime) <= AddLitterTime(sdri.CSLinkTrain(UBound(sdri.CSLinkTrain)).EndTime) AndAlso AddLitterTime(sysDinnerStation(i).dinnerEndTime) >= AddLitterTime(sdri.CSLinkTrain(UBound(sdri.CSLinkTrain)).EndTime) Then
                    dinneritem = sysDinnerStation(i)
                    Exit For
                End If
            End If
        Next
        If dinneritem Is Nothing Then
            MsgBox("该司机不在用餐地点或没有用餐时间参数！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "用餐提醒")
            Exit Sub
        End If
        If sdri.FlagDinner = True Then
            For Each Str As String In sdri.AllDinnerInfo.Keys
                If sdri.AllDinnerInfo(Str).dinnerType = dinneritem.dinnerType Then
                    MsgBox("该司机已用餐！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "用餐提醒")
                    Exit Sub
                End If
            Next
        End If
        sdri.State = "用餐"
        sdri.FlagDinner = True
        sdri.DinnerStation = sdri.CSLinkTrain(UBound(sdri.CSLinkTrain)).EndStaName
        sdri.DinnerStartTime = sdri.CSLinkTrain(UBound(sdri.CSLinkTrain)).CulEndTime
        sdri.DinnerEndTime = sdri.DinnerStartTime + dinneritem.DinnerTime
        sdri.AllDinnerInfo.Add(sdri.DinnerStation & "-" & sdri.DinnerStartTime.ToString & "-" & sdri.DinnerEndTime.ToString, dinneritem)
        Call CSRefreshDiagram()
        Call ListAllViewInfo()
    End Sub

    Private Sub 班后ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 班后ToolStripMenuItem.Click
        AddUnReDoInfo(True)
        CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).State = "班后"
        Call CSRefreshDiagram()
        Call ListAllViewInfo()
    End Sub

  
   
    Public Sub FormNightDrivers() '晚班排班（逆向）
        Call SortMergedCSLinkTrain(False)
        Dim AvaMerTrains As New List(Of MergedCSLinkTrain)
        For Each tmertrain As MergedCSLinkTrain In CSTrainsAndDrivers.MergedCSLinkTrains
            If tmertrain IsNot Nothing AndAlso tmertrain.IsLinked = False Then
                AvaMerTrains.Add(tmertrain)
            End If
        Next
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then
            For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                If dri IsNot Nothing AndAlso dri.DutySort = "夜班" Then
                    Dim MergedCSTrain As New MergedCSLinkTrain
                    For i As Integer = 1 To UBound(dri.CSLinkTrain)
                        MergedCSTrain.AddCSLinkTrain(dri.CSLinkTrain(i))
                    Next
                    MergedCSTrain.dutywork = dri.dutyWork
                    MergedCSTrain.ifAutoDone = dri.ifAutoDone
                    If MergedCSTrain.dutywork = "备车" Then
                        MergedCSTrain.beiche = 1
                    End If
                    MergedCSTrain.CSLinkTrains(1).IsLinked = False
                    AvaMerTrains.Add(MergedCSTrain)
                    RemoveDriver(dri)
                End If
            Next
        End If
        For i As Integer = 0 To AvaMerTrains.Count - 1
            For j = i To AvaMerTrains.Count - 1
                If AddLitterTime(AvaMerTrains(i).CSLinkTrains(UBound(AvaMerTrains(i).CSLinkTrains)).EndTime) < AddLitterTime(AvaMerTrains(j).CSLinkTrains(UBound(AvaMerTrains(j).CSLinkTrains)).EndTime) Then
                    Dim tempCSlinkTrain As MergedCSLinkTrain
                    tempCSlinkTrain = AvaMerTrains(i)
                    AvaMerTrains(i) = AvaMerTrains(j)
                    AvaMerTrains(j) = tempCSlinkTrain
                End If
            Next
        Next
        Me.ProgressBar1.Maximum = AvaMerTrains.Count
        Me.ProgressBar1.Value = 1
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.Visible = True
        Dim starttime As Integer = -1
        labInfor.Text = "正在处理请稍后..."
        labInfor.Visible = True
        For Each tmer As MergedCSLinkTrain In AvaMerTrains
            Me.ProgressBar1.PerformStep()
            System.Windows.Forms.Application.DoEvents()
            If ForceDutyTime = 1 And tmer.CSLinkTrains(1).CulStartTime < AStartTime Then
                Continue For
            End If
            If starttime <> -1 Then
                If tmer.CSLinkTrains(1).CulStartTime < starttime Then
                    Continue For
                End If
            End If
            If tmer.IsLinked = False Then
                Dim maxWaitTime As Integer = -1
                Dim selectDri As CSDriver = Nothing
                Dim AvaDris As New List(Of CSDriver)
                Dim AttOffPlace As Boolean = False
                If tmer.beiche <> 0 And tmer.CSLinkTrains(1).CulStartTime > 12 * 3600 Then
                    If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then           '找出可以接车的所有司机
                        For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                            If dri IsNot Nothing AndAlso dri.DutySort = "夜班" Then
                                Dim upTainDirection As Integer = TransitStationPlaceforUptrain(tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).EndStaName, tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).RoutingName, tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).UpOrDown, tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).EndTime)
                                If (upTainDirection <> -1 And upTainDirection <> 2) AndAlso upTainDirection <> dri.CSLinkTrain(1).UpOrDown Then '判断是否是可以接的方向
                                    Continue For
                                End If
                                If dri.CanDriveTheTrain1(tmer) Then
                                    If upTainDirection = -1 Then
                                        AttOffPlace = True
                                    End If
                                    AvaDris.Add(dri)
                                Else
                                    If dri.dutyWork = "备车" Then
                                        If upTainDirection = -1 Then
                                            AttOffPlace = True
                                        End If
                                        AvaDris.Add(dri)
                                    End If
                                End If
                            End If
                        Next
                    End If
                    For Each dri As CSDriver In AvaDris
                        If dri IsNot Nothing AndAlso dri.DutySort = "夜班" AndAlso dri.dutyWork = "备车" Then
                            If dri.CSLinkTrain(1).nCheDiID = tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).nCheDiID AndAlso dri.CSLinkTrain(1).StartStaName = tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).EndStaName Then
                                selectDri = dri
                                Exit For
                            End If
                        End If
                    Next
                    If selectDri Is Nothing Then
                        If AttOffPlace = False Then
                            maxWaitTime = -1
                            '最大休息时间接车,但需要满足各换乘点最低休息要求          
                            For Each dri As CSDriver In AvaDris
                                If dri.dutyWork = "备车" Then
                                    Continue For
                                End If
                                Dim waittime As Integer = dri.CSLinkTrain(1).CulStartTime - tmer.CulEndTime
                                If waittime > maxWaitTime And waittime >= ChangePlaceRestTime(tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).EndStaName, tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).RoutingName, tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).UpOrDown, tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).EndTime) Then
                                    maxWaitTime = waittime
                                    selectDri = dri
                                End If
                            Next
                        Else
                            maxWaitTime = 24 * 3600 '最小
                            For Each dri As CSDriver In AvaDris
                                If dri.dutyWork = "备车" Then
                                    Continue For
                                End If
                                Dim waittime As Integer = dri.CSLinkTrain(1).CulStartTime - tmer.CulEndTime
                                If waittime < maxWaitTime Then
                                    maxWaitTime = waittime
                                    selectDri = dri
                                End If
                            Next
                        End If
                    End If
                    If selectDri IsNot Nothing Then
                        If tmer.beiche = 2 Then
                            selectDri.dutyWork = "备车"
                        Else
                            selectDri.dutyWork = ""
                        End If
                        selectDri.AddReMergedTrain(tmer)
                        selectDri.RefreshState(, False)
                    Else
                        If CSTrainsAndDrivers.NightDrivers.Count < CSAutoPlanPara.NightDutyNum Then
                            Call AddANewDriverforMerged("夜班", tmer)
                            If tmer.beiche = 2 Then
                                CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).dutyWork = "备车"
                            End If
                            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).RefreshState()
                        End If
                    End If
                    Continue For
                End If

                '====普通任务段处理
                selectDri = Nothing
                AvaDris = New List(Of CSDriver)
                AttOffPlace = False
                If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then           '找出可以接车的所有司机
                    For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                        If dri IsNot Nothing AndAlso dri.DutySort = "夜班" Then
                            
                            Dim upTainDirection As Integer = TransitStationPlaceforUptrain(tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).EndStaName, tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).RoutingName, tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).UpOrDown, tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).CulEndTime)
                            If (upTainDirection <> -1 And upTainDirection <> 2) AndAlso upTainDirection <> dri.CSLinkTrain(1).UpOrDown Then '判断是否是可以接的方向
                                Continue For
                            End If

                            If dri.CanDriveTheTrain1(tmer) Then
                                If dri.dutyWork = "备车" Then
                                    Continue For
                                End If
                                If upTainDirection = -1 Then
                                    AttOffPlace = True
                                End If
                                AvaDris.Add(dri)
                            End If
                        End If
                    Next
                End If
                If AttOffPlace = False Then
                    maxWaitTime = -1
                    '最大休息时间接车,但需要满足各换乘点最低休息要求          
                    For Each dri As CSDriver In AvaDris
                        Dim waittime As Integer = dri.CSLinkTrain(1).CulStartTime - tmer.CulEndTime
                        If waittime > maxWaitTime And waittime >= ChangePlaceRestTime(tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).EndStaName, tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).RoutingName, tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).UpOrDown, tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).EndTime) Then
                            maxWaitTime = waittime
                            selectDri = dri
                        End If
                    Next
                Else
                    maxWaitTime = 24 * 3600 '最小
                    For Each dri As CSDriver In AvaDris
                        Dim waittime As Integer = dri.CSLinkTrain(1).CulStartTime - tmer.CulEndTime
                        If waittime < maxWaitTime Then
                            maxWaitTime = waittime
                            selectDri = dri
                        End If
                    Next
                End If

                If selectDri IsNot Nothing Then
                    selectDri.AddReMergedTrain(tmer)
                    selectDri.RefreshState(, False)
                Else
                    If CSTrainsAndDrivers.NightDrivers.Count < CSAutoPlanPara.NightDutyNum Then
                        Call AddANewDriverforMerged("夜班", tmer)
                        If CSTrainsAndDrivers.NightDrivers.Count = CSAutoPlanPara.NightDutyNum Then
                            starttime = AddLitterTime(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSLinkTrain(1).EndTime) - NightWorkTime
                        End If
                        CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).RefreshState(, False)
                    End If
                End If

                '================终止条件判断
                If CSTrainsAndDrivers.NightDrivers.Count >= CSAutoPlanPara.NightDutyNum AndAlso IsDutyOverByDutySort("夜班") Then
                    Call AllRefreshState(False)
                    Me.ProgressBar1.Visible = False
                    Me.LabelPro.Visible = False
                    Exit Sub
                End If
            End If
        Next
        Call AllRefreshState(False)
        Me.ProgressBar1.Visible = False
        Me.LabelPro.Visible = False
    End Sub
    
    Public Sub FormMornDrivers() '安排早班，正推
        Call SortMergedCSLinkTrain(True)
        Dim AvaMerTrains As New List(Of MergedCSLinkTrain)
        For Each tmertrain As MergedCSLinkTrain In CSTrainsAndDrivers.MergedCSLinkTrains
            If tmertrain IsNot Nothing AndAlso tmertrain.IsLinked = False Then
                AvaMerTrains.Add(tmertrain)
            End If
        Next
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then
            For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                If dri IsNot Nothing AndAlso dri.DutySort = "早班" Then
                    Dim MergedCSTrain As New MergedCSLinkTrain
                    For i As Integer = 1 To UBound(dri.CSLinkTrain)
                        MergedCSTrain.AddCSLinkTrain(dri.CSLinkTrain(i))
                    Next
                    MergedCSTrain.dutywork = dri.dutyWork
                    MergedCSTrain.ifAutoDone = dri.ifAutoDone
                    If MergedCSTrain.dutywork = "备车" Then
                        MergedCSTrain.beiche = 1
                    End If
                    MergedCSTrain.CSLinkTrains(1).IsLinked = False
                    AvaMerTrains.Add(MergedCSTrain)
                    RemoveDriver(dri)
                End If
            Next
        End If
        For i As Integer = 0 To AvaMerTrains.Count - 1
            For j = i To AvaMerTrains.Count - 1
                If AddLitterTime(AvaMerTrains(i).CSLinkTrains(1).StartTime) > AddLitterTime(AvaMerTrains(j).CSLinkTrains(1).StartTime) Then
                    Dim tempCSlinkTrain As MergedCSLinkTrain
                    tempCSlinkTrain = AvaMerTrains(i)
                    AvaMerTrains(i) = AvaMerTrains(j)
                    AvaMerTrains(j) = tempCSlinkTrain
                End If
            Next
        Next
        Me.ProgressBar1.Maximum = AvaMerTrains.Count
        Me.ProgressBar1.Value = 1
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.Visible = True
        labInfor.Text = "正在处理请稍后..."
        labInfor.Visible = True
        Dim endtime As Integer = -1 '最后一个早班开始时间+早班工作时长约束
        For Each tmer As MergedCSLinkTrain In AvaMerTrains
            Me.ProgressBar1.PerformStep()
            System.Windows.Forms.Application.DoEvents()
            If endtime <> -1 Then
                If AddLitterTime(tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).EndTime) > AddLitterTime(endtime) Then
                    Continue For
                End If
            End If
            If tmer.IsLinked = False Then
                Dim maxWaitTime As Integer = -1
                Dim selectDri As CSDriver = Nothing
                Dim AvaDris As New List(Of CSDriver)
                Dim AttOffPlace As Boolean = False
              
                '====备车出发处理
                If tmer.beiche <> 0 AndAlso tmer.CSLinkTrains(UBound(tmer.CSLinkTrains)).CulEndTime <= 12 * 3600 Then
                    If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then           '找出可以接车的所有司机
                        For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                            If dri IsNot Nothing AndAlso dri.DutySort = "早班" Then
                                Dim upTainDirection As Integer = TransitStationPlaceforUptrain(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime)
                                If (upTainDirection <> -1 And upTainDirection <> 2) AndAlso upTainDirection <> tmer.CSLinkTrains(1).UpOrDown Then '判断是否是可以接的方向
                                    Continue For
                                End If

                                If dri.CanDriveTheTrain(tmer) Then
                                    If upTainDirection = -1 Then
                                        AttOffPlace = True
                                    End If
                                    AvaDris.Add(dri)
                                Else
                                    If dri.dutyWork = "备车" Then
                                        If upTainDirection = -1 Then
                                            AttOffPlace = True
                                        End If
                                        AvaDris.Add(dri)
                                    End If
                                End If
                            End If
                        Next
                    End If

                    For Each dri As CSDriver In AvaDris
                        If dri IsNot Nothing AndAlso dri.DutySort = "早班" AndAlso dri.dutyWork = "备车" Then
                            If dri.CSLinkTrain(UBound(dri.CSLinkTrain)).nCheDiID = tmer.nCheDiID AndAlso dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName = tmer.CSLinkTrains(1).StartStaName Then
                                selectDri = dri
                                Exit For
                            End If
                        End If
                    Next

                    If selectDri Is Nothing Then
                        If AttOffPlace = False Then
                            '最大休息时间人接车 
                            maxWaitTime = -1
                            For Each dri As CSDriver In AvaDris
                                If dri.dutyWork = "备车" Then
                                    Continue For
                                End If
                                Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                                If waittime > maxWaitTime And waittime >= ChangePlaceRestTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime) Then
                                    maxWaitTime = waittime
                                    selectDri = dri
                                End If
                            Next
                        Else
                            maxWaitTime = 24 * 3600 '最小
                            For Each dri As CSDriver In AvaDris
                                If dri.dutyWork = "备车" Then
                                    Continue For
                                End If
                                Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                                If waittime < maxWaitTime Then
                                    maxWaitTime = waittime
                                    selectDri = dri
                                End If
                            Next
                        End If
                    End If

                    If selectDri IsNot Nothing Then
                        If tmer.beiche = 1 Then
                            selectDri.dutyWork = "备车"
                        Else
                            selectDri.dutyWork = ""
                        End If
                        selectDri.AddMergedTrain(tmer)
                        selectDri.RefreshState()
                    Else
                        If CSTrainsAndDrivers.MorningDrivers.Count < CSAutoPlanPara.MoringDutyNum Then
                            Call AddANewDriverforMerged("早班", tmer)
                            If tmer.beiche = 1 Then
                                CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).dutyWork = "备车"
                            End If
                            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).RefreshState()
                        End If
                    End If
                    Continue For
                End If
            
                '====普通任务段处理
                AttOffPlace = False
                selectDri = Nothing
                AvaDris = New List(Of CSDriver)
                If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then           '找出可以接车的所有司机
                    For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                        If dri IsNot Nothing AndAlso dri.DutySort = "早班" Then
                            
                            Dim upTainDirection As Integer = TransitStationPlaceforUptrain(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime)
                            If (upTainDirection <> -1 And upTainDirection <> 2) AndAlso upTainDirection <> tmer.CSLinkTrains(1).UpOrDown Then '判断是否是可以接的方向
                                Continue For
                            End If

                            If dri.CanDriveTheTrain(tmer) Then
                                If dri.dutyWork = "备车" Then
                                    Continue For
                                End If
                                If upTainDirection = -1 Then
                                    AttOffPlace = True
                                End If
                                AvaDris.Add(dri)
                            End If
                        End If
                    Next
                End If

                If AttOffPlace = False Then
                    '最大休息时间人接车 
                    maxWaitTime = -1
                    For Each dri As CSDriver In AvaDris
                        Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                        If waittime > maxWaitTime And waittime >= ChangePlaceRestTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime) Then
                            maxWaitTime = waittime
                            selectDri = dri
                        End If
                    Next
                Else
                    maxWaitTime = 24 * 3600 '最小
                    For Each dri As CSDriver In AvaDris
                        Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                        If waittime < maxWaitTime Then
                            maxWaitTime = waittime
                            selectDri = dri
                        End If
                    Next
                End If

                If selectDri IsNot Nothing Then
                    selectDri.AddMergedTrain(tmer)
                    selectDri.RefreshState()
                Else
                    Dim dutysort As String = ""
                    If tmer.CSLinkTrains(1).FirstStation.IsYard AndAlso tmer.CulStartTime <= NStartTime Then
                        dutysort = "早班"
                    Else
                        If CSTrainsAndDrivers.MorningDrivers.Count < CSAutoPlanPara.MoringDutyNum Then
                            dutysort = "早班"
                        End If
                    End If
                    If dutysort = "早班" Then
                        Call AddANewDriverforMerged(dutysort, tmer)
                        If CSTrainsAndDrivers.MorningDrivers.Count = CSAutoPlanPara.MoringDutyNum Then
                            endtime = CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSLinkTrain(1).StartTime + MornWorkTime
                        End If
                        CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).RefreshState()
                    End If
                End If

                '================终止条件判断
                If IsDutyOverByDutySort("早班") AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 AndAlso CSTrainsAndDrivers.MorningDrivers.Count <= CSAutoPlanPara.MoringDutyNum Then
                    Call AllRefreshState()
                    Me.ProgressBar1.Visible = False
                    Me.LabelPro.Visible = False
                    labInfor.Text = "处理完毕！"
                    Exit Sub
                End If
            End If
        Next
        Call AllRefreshState()
        Me.ProgressBar1.Visible = False
        Me.LabelPro.Visible = False
        labInfor.Text = "处理完毕！"
    End Sub
   
    Public Sub FormDayDrivers() '安排白班，正推(包括接其他乘务员)
        If CSTrainsAndDrivers.MorningDrivers.Count = 0 Or CSTrainsAndDrivers.NightDrivers.Count = 0 Then
            MsgBox("请先排早班和夜班！")
            Exit Sub
        End If
        Call SortMergedCSLinkTrain(True)
        Dim AvaMerTrains As New List(Of MergedCSLinkTrain)
        For Each tmertrain As MergedCSLinkTrain In CSTrainsAndDrivers.MergedCSLinkTrains
            If tmertrain IsNot Nothing AndAlso tmertrain.IsLinked = False Then
                AvaMerTrains.Add(tmertrain)
            End If
        Next
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then
            For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                If dri IsNot Nothing AndAlso dri.DutySort = "白班" AndAlso dri.dutyWork <> "替饭" Then
                    Dim MergedCSTrain As New MergedCSLinkTrain
                    For i As Integer = 1 To UBound(dri.CSLinkTrain)
                        MergedCSTrain.AddCSLinkTrain(dri.CSLinkTrain(i))
                    Next
                    MergedCSTrain.dutywork = dri.dutyWork
                    AvaMerTrains.Add(MergedCSTrain)
                    RemoveDriver(dri)
                End If
            Next
        End If
        For i As Integer = 0 To AvaMerTrains.Count - 1
            For j = i To AvaMerTrains.Count - 1
                If AddLitterTime(AvaMerTrains(i).CSLinkTrains(1).CulStartTime) > AddLitterTime(AvaMerTrains(j).CSLinkTrains(1).CulStartTime) Then
                    Dim tempCSlinkTrain As MergedCSLinkTrain
                    tempCSlinkTrain = AvaMerTrains(i)
                    AvaMerTrains(i) = AvaMerTrains(j)
                    AvaMerTrains(j) = tempCSlinkTrain
                End If
            Next
        Next

        Me.ProgressBar1.Maximum = AvaMerTrains.Count
        Me.ProgressBar1.Value = 1
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.Visible = True
        labInfor.Text = "正在处理请稍后..."
        labInfor.Visible = True

        For Each tmer As MergedCSLinkTrain In AvaMerTrains
            Dim maxWaitTime As Integer = -1
            Dim selectDri As CSDriver = Nothing
            Dim AvaDris As New List(Of CSDriver)
            Dim AttOffPlace As Boolean = False

            If tmer.beiche <> 0 Then
                If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then           '找出可以接车的所有司机
                    For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                        If dri IsNot Nothing AndAlso dri.DutySort = "白班" AndAlso dri.dutyWork <> "替饭" Then
                            Dim upTainDirection As Integer = TransitStationPlaceforUptrain(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime)
                            If (upTainDirection <> -1 And upTainDirection <> 2) AndAlso upTainDirection <> tmer.UpOrDown Then '判断是否是可以接的方向
                                Continue For
                            End If
                            If dri.CanDriveTheTrain(tmer) Then
                                If tmer.dutywork = "吃饭" And dri.dutyWork = "已吃饭" Then
                                    Continue For
                                End If
                                If upTainDirection = -1 Then
                                    AttOffPlace = True
                                End If
                                AvaDris.Add(dri)
                            Else
                                If dri.dutyWork = "备车" Then
                                    If upTainDirection = -1 Then
                                        AttOffPlace = True
                                    End If
                                    AvaDris.Add(dri)
                                End If
                            End If
                        End If
                    Next
                End If
                For Each dri As CSDriver In AvaDris
                    If dri IsNot Nothing AndAlso dri.DutySort = "白班" AndAlso dri.dutyWork = "备车" Then
                        If dri.CSLinkTrain(UBound(dri.CSLinkTrain)).nCheDiID = tmer.nCheDiID AndAlso dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName = tmer.CSLinkTrains(1).StartStaName Then
                            selectDri = dri
                            Exit For
                        End If
                    End If
                Next
                If selectDri Is Nothing Then
                    If AttOffPlace = False Then
                        '最大休息时间人接车 
                        maxWaitTime = -1
                        For Each dri As CSDriver In AvaDris
                            If dri.dutyWork = "备车" Then
                                Continue For
                            End If
                            Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                            If waittime > maxWaitTime And waittime >= ChangePlaceRestTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime) Then
                                maxWaitTime = waittime
                                selectDri = dri
                            End If
                        Next
                    Else
                        maxWaitTime = 24 * 3600 '最小
                        For Each dri As CSDriver In AvaDris
                            If dri.dutyWork = "备车" Then
                                Continue For
                            End If
                            Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                            If waittime < maxWaitTime Then
                                maxWaitTime = waittime
                                selectDri = dri
                            End If
                        Next
                    End If
                End If

                If selectDri IsNot Nothing Then
                    If tmer.beiche = 1 Then
                        selectDri.dutyWork = "备车"
                    Else
                        selectDri.dutyWork = ""
                    End If
                    selectDri.AddMergedTrain(tmer)
                    selectDri.RefreshState()
                Else
                    If CSTrainsAndDrivers.DayDrivers.Count < CSAutoPlanPara.DayDutyNum Then
                        Call AddANewDriverforMerged("白班", tmer)
                        If tmer.beiche = 1 Then
                            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).dutyWork = "备车"
                        End If
                        CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).RefreshState()
                    End If
                End If
                Continue For
            End If

            '====普通任务段处理
            selectDri = Nothing
            AvaDris = New List(Of CSDriver)
            Dim ifDirSame As Boolean = False
            If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then         '找出可以接车的所有司机
                For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                    If dri IsNot Nothing AndAlso dri.DutySort = "白班" AndAlso dri.dutyWork <> "替饭" Then
                        Dim upTainDirection As Integer = TransitStationPlaceforUptrain(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime)
                        If (upTainDirection <> -1 And upTainDirection <> 2) AndAlso upTainDirection <> tmer.UpOrDown Then '判断是否是可以接的方向
                            Continue For
                        End If
                        If dri.CanDriveTheTrain(tmer) Then
                            If dri.dutyWork = "备车" Then
                                Continue For
                            End If
                            If tmer.dutywork = "吃饭" And dri.dutyWork = "已吃饭" Then
                                Continue For
                            End If
                            If upTainDirection = -1 Then
                                AttOffPlace = True
                            End If
                            AvaDris.Add(dri)
                        End If
                    End If
                Next
            End If
            If AttOffPlace = False Then
                '最大休息时间人接车 
                maxWaitTime = -1
                For Each dri As CSDriver In AvaDris
                    Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                    If waittime > maxWaitTime And waittime >= ChangePlaceRestTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime) Then
                        maxWaitTime = waittime
                        selectDri = dri
                    End If
                Next
            Else
                maxWaitTime = 24 * 3600 '最小
                For Each dri As CSDriver In AvaDris
                    Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                    If waittime < maxWaitTime Then
                        maxWaitTime = waittime
                        selectDri = dri
                    End If
                Next
            End If


            If selectDri IsNot Nothing Then
                If tmer.dutywork = "吃饭" Then
                    selectDri.dutyWork = "已吃饭"
                End If
                selectDri.AddMergedTrain(tmer)
                selectDri.RefreshState()
            Else
                If CSTrainsAndDrivers.DayDrivers.Count < CSAutoPlanPara.DayDutyNum Then
                    Call AddANewDriverforMerged("白班", tmer)
                    If tmer.dutywork = "吃饭" Then
                        CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).dutyWork = "已吃饭"
                    End If
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).RefreshState()
                End If
            End If
            Me.ProgressBar1.PerformStep()
            System.Windows.Forms.Application.DoEvents()
            If CSTrainsAndDrivers.DayDrivers.Count >= CSAutoPlanPara.DayDutyNum AndAlso IsDutyOverByDutySort("白班") Then
                Call AllRefreshState()
                Me.ProgressBar1.Visible = False
                Me.LabelPro.Visible = False
                Exit Sub
            End If
        Next
        Call AllRefreshState()
        Me.ProgressBar1.Visible = False
        Me.LabelPro.Visible = False
    End Sub

   
    Public Sub SortDriverbyAttend(ByVal MDutySort As String) '一般为早班需要重排
        Dim i, j As Integer
        Dim xuHao As List(Of Integer) = New List(Of Integer)
        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            If CSTrainsAndDrivers.CSDrivers(i).DutySort = MDutySort Then
                xuHao.Add(i)
            End If
        Next
        For i = 0 To xuHao.Count - 1
            For j = i To xuHao.Count - 1
                If AddLitterTime(CSTrainsAndDrivers.CSDrivers(xuHao(i)).BeginWorkTime) > AddLitterTime(CSTrainsAndDrivers.CSDrivers(xuHao(j)).BeginWorkTime) Then
                    Dim tmpxuHao As Integer = xuHao(i)
                    xuHao(i) = xuHao(j)
                    xuHao(j) = tmpxuHao
                End If
            Next
        Next
        For i = 0 To xuHao.Count - 1
            CSTrainsAndDrivers.CSDrivers(xuHao(i)).CSDriverID = i + 1
            CSTrainsAndDrivers.CSDrivers(xuHao(i)).CSdriverNo = MDutySort + (i + 1).ToString("00")
        Next
    End Sub
    Public Sub SortDriverbyLeaveWork(ByVal MDutySort As String) '一般为夜班需要重排
        Dim i, j As Integer
        Dim xuHao As List(Of Integer) = New List(Of Integer)
        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)) Is Nothing Then
                RemoveDriver(i)
                i -= 1
            End If
        Next
        If ForceDutyTime = 1 Then
            For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                If CSTrainsAndDrivers.CSDrivers(i).DutySort = MDutySort AndAlso CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).FirstStation.IsShiftSta = False Then
                    CSTrainsAndDrivers.CSDrivers(i).ReRemoveTrain(1)
                End If
            Next
        End If
        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            If CSTrainsAndDrivers.CSDrivers(i).DutySort = MDutySort Then
                xuHao.Add(i)
            End If
        Next
        For i = 0 To xuHao.Count - 1
            For j = i To xuHao.Count - 1
                If (CSTrainsAndDrivers.CSDrivers(xuHao(i)).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(xuHao(i)).CSLinkTrain)) IsNot Nothing And CSTrainsAndDrivers.CSDrivers(xuHao(j)).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(xuHao(j)).CSLinkTrain)) IsNot Nothing) AndAlso (AddLitterTime(CSTrainsAndDrivers.CSDrivers(xuHao(i)).EndEorkTime) > AddLitterTime(CSTrainsAndDrivers.CSDrivers(xuHao(j)).EndEorkTime)) Then
                    Dim tmpxuHao As Integer = xuHao(i)
                    xuHao(i) = xuHao(j)
                    xuHao(j) = tmpxuHao
                End If
            Next
        Next
        For i = 0 To xuHao.Count - 1
            CSTrainsAndDrivers.CSDrivers(xuHao(i)).CSDriverID = i + 1
            CSTrainsAndDrivers.CSDrivers(xuHao(i)).CSdriverNo = MDutySort + (i + 1).ToString("00")
            CSTrainsAndDrivers.CSDrivers(xuHao(i)).OutPutCSdriverNo = MDutySort + (i + 1).ToString("00")
        Next
    End Sub

    Private Sub 可变动参数TToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 可变动参数TToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If
        Dim frm As New frmCSChangeableBasicSet
        frm.ShowDialog()
    End Sub
    Private Sub frmCSTimeTableMain_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If MsgBox("确定计划已保存，并退出编制？", MsgBoxStyle.OkCancel + MsgBoxStyle.Information, "退出提醒") = MsgBoxResult.Ok Then
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub 颜色与字体CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 颜色与字体CToolStripMenuItem.Click
        frmDiagramLineAndFontSet.ShowDialog()
    End Sub

    Private Sub 另存为ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 另存为ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSDrivers Is Nothing = True Then
            Exit Sub
        Else
            Call DeleteNullDriverAndCodeDriver()
            Dim nf As New frmSaveCSTT
            nf.ShowDialog()
            Me.labName.Text = "当前乘务计划：" & CStr(strQCurCSPlanName)
        End If
    End Sub

    Private Sub CmbSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbSize.SelectedIndexChanged
        Try
            CmbSize.Enabled = False
            If CSTimeTablePara.picPubDiagram Is Nothing Then
                Exit Sub
            End If
            CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = DefaultWidth * (CDec(CmbSize.Text.Trim("%")) / 100)
            Me.PicDiagram.Width = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
            Me.PicDiagram.Height = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
            Call CSRefreshDiagram(0)
            CmbSize.Enabled = True
        Catch ex As Exception
            MsgBox("操作过于频繁！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
        End Try
    End Sub

    Private Sub 钓鱼图反导入ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 钓鱼图反导入ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先加载运行图数据！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
            Exit Sub
        End If
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
            If MsgBox("当前正在编制计划，反向导入现有计划将被清空，是否继续？", MsgBoxStyle.OkCancel + MsgBoxStyle.Information, "提醒") = MsgBoxResult.Cancel Then
                Exit Sub
            End If
        End If
        FrmFishDiagramSet.CurLineName = strCurlineID
        FrmFishDiagramSet.BtnInput.Visible = True
        FrmFishDiagramSet.Button1.Visible = False
        FrmFishDiagramSet.BtnInput.Location = FrmFishDiagramSet.Button1.Location
        FrmFishDiagramSet.ShowDialog()
    End Sub

    Private Sub 断开任务段SToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 断开任务段SToolStripMenuItem.Click
        Dim newtrains As New List(Of CSLinkTrain)
        If CSTimeTablePara.nPubTrain > 0 Then
            For m As Integer = 1 To UBound(CSTrainsAndDrivers.CSChedi)
                If CSTrainsAndDrivers.CSChedi(m).sCheDiHao = CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).sCheDiHao Then
                    Dim index As Integer = -1
                    For n As Integer = 1 To UBound(CSTrainsAndDrivers.CSChedi(m).CrewSta)
                        If n < UBound(CSTrainsAndDrivers.CSChedi(m).CrewSta) AndAlso CSTrainsAndDrivers.CSChedi(m).CrewSta(n) Is CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).FirstStation AndAlso _
                            CSTrainsAndDrivers.CSChedi(m).CrewSta(n + 1) IsNot CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).SecondStation AndAlso CSTrainsAndDrivers.CSChedi(m).CrewSta(n + 1).IsLast = False Then
                            index = n
                            Exit For
                        End If
                    Next
                    If index > 0 Then
                        Call AddUnReDoInfo(True)
                        For n As Integer = index To UBound(CSTrainsAndDrivers.CSChedi(m).CrewSta)
                            Dim temptrain As New CSLinkTrain(CSTrainsAndDrivers.CSChedi(m).CrewSta(n), CSTrainsAndDrivers.CSChedi(m).CrewSta(n + 1), "", CSTrainsAndDrivers.CSChedi(m))
                            newtrains.Add(temptrain)
                            If CSTrainsAndDrivers.CSChedi(m).CrewSta(n + 1) Is CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain).SecondStation Then
                                Exit For
                            End If
                        Next
                    Else
                        MsgBox("该列车不能够被打断！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                        Exit Sub
                    End If
                    Exit For
                End If
            Next

            Dim driindex As Integer = -1
            Dim tindex As Integer = -1
            If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
                For x As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                    For y As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers(x).CSLinkTrain)
                        If CSTrainsAndDrivers.CSDrivers(x).CSLinkTrain(y) Is CSTrainsAndDrivers.CSLinkTrains(CSTimeTablePara.nPubTrain) Then
                            driindex = x
                            tindex = y
                            GoTo L
                        End If
                    Next
                Next
            End If

L:
            If driindex <> -1 AndAlso tindex <> -1 Then
                Dim tdritrain() As CSLinkTrain = CSTrainsAndDrivers.CSDrivers(driindex).CSLinkTrain
                ReDim CSTrainsAndDrivers.CSDrivers(driindex).CSLinkTrain(0)
                For x As Integer = 1 To UBound(tdritrain)
                    If x <> tindex Then
                        CSTrainsAndDrivers.CSDrivers(driindex).AddTrain(tdritrain(x))
                    Else
                        For Each train As CSLinkTrain In newtrains
                            CSTrainsAndDrivers.CSDrivers(driindex).AddTrain(train)
                        Next
                    End If
                Next

            End If

            Dim Trains() As CSLinkTrain = CSTrainsAndDrivers.CSLinkTrains
            ReDim CSTrainsAndDrivers.CSLinkTrains(0)
            For i As Integer = 1 To UBound(Trains)
                If i <> CSTimeTablePara.nPubTrain Then
                    ReDim Preserve CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains) + 1)
                    CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains)) = Trains(i)
                    CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains)).CSTrainID = UBound(CSTrainsAndDrivers.CSLinkTrains)
                Else
                    For Each train As CSLinkTrain In newtrains
                        ReDim Preserve CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains) + 1)
                        CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains)) = train
                        CSTrainsAndDrivers.CSLinkTrains(UBound(CSTrainsAndDrivers.CSLinkTrains)).CSTrainID = UBound(CSTrainsAndDrivers.CSLinkTrains)
                    Next
                End If
            Next
            Call CheckUnDoAndReDoState()
            Call CSRefreshDiagram(0)
            Call ListAllViewInfo()
        End If
    End Sub

    Private Sub 设置区域信息ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 设置区域信息ToolStripMenuItem.Click
        frmSetAreainfo.ShowDialog()
    End Sub
    Private Sub 自动安排参数ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 安排乘务员人数ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If
        If CSAutoPlanPara IsNot Nothing Then
            FrmCSMaker.TXTMorningNum.Text = CSAutoPlanPara.MoringDutyNum
            FrmCSMaker.TXTNightNum.Text = CSAutoPlanPara.NightDutyNum
            FrmCSMaker.TXTDayNum.Text = CSAutoPlanPara.DayDutyNum
            FrmCSMaker.TXTCDayNum.Text = CSAutoPlanPara.CDayDutyNum
        Else
            FrmCSMaker.TXTMorningNum.Text = ""
            FrmCSMaker.TXTNightNum.Text = ""
            FrmCSMaker.TXTDayNum.Text = ""
            FrmCSMaker.TXTCDayNum.Text = "0"

        End If
        If FrmCSMaker.ShowDialog = System.Windows.Forms.DialogResult.OK Then

            If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then
                CSTrainsAndDrivers = New CSTrainAndDrivers()
                Call refreshAfterParaSet()
            End If
            CSAutoPlanPara = New AutoPara
            CSAutoPlanPara.MoringDutyNum = FrmCSMaker.TXTMorningNum.Text
            CSAutoPlanPara.NightDutyNum = FrmCSMaker.TXTNightNum.Text
            CSAutoPlanPara.DayDutyNum = FrmCSMaker.TXTDayNum.Text
            CSAutoPlanPara.CDayDutyNum = FrmCSMaker.TXTCDayNum.Text
        End If
    End Sub

    Private Sub 清空当前计划ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 清空当前计划ToolStripMenuItem.Click
        Call AddUnReDoInfo(True)
        ReDim Preserve CSTrainsAndDrivers.CSDrivers(0)
        CSTrainsAndDrivers.MorningDrivers.Clear()
        CSTrainsAndDrivers.DayDrivers.Clear()
        CSTrainsAndDrivers.CDayDrivers.Clear()
        CSTrainsAndDrivers.NightDrivers.Clear()
        CSTrainsAndDrivers.OtherDrivers.Clear()
        For Each train As CSLinkTrain In CSTrainsAndDrivers.CSLinkTrains
            If train IsNot Nothing Then
                train.IsLinked = False
            End If
        Next
        GC.Collect()
        Call CSRefreshDiagram()
        Call ListAllViewInfo()
    End Sub
    Private Sub 安排完早班ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 安排完早班ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If
        If CSAutoPlanPara Is Nothing Then
            MsgBox("请先设置自动安排参数!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
            Exit Sub
        End If

        Call AddUnReDoInfo(True)
        Call FormMornDrivers()
        Call SortDriverbyAttend("早班")
        Call ListAllViewInfo()
        Call CSRefreshDiagram()
    End Sub

    Private Sub 安排完白班ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 安排完白班ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If
        If CSAutoPlanPara Is Nothing Then
            MsgBox("请先设置自动安排参数!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
            Exit Sub
        End If

        Call AddUnReDoInfo(True)
        Call FormDayDrivers()
        Call SortDriverbyAttend("白班")
        Call ListAllViewInfo()
        Call CSRefreshDiagram()
    End Sub

    Private Sub 安排完日勤班ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 安排完日勤班ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If
        If CSTrainsAndDrivers.MorningDrivers.Count = 0 Or CSTrainsAndDrivers.NightDrivers.Count = 0 Then
            MsgBox("请先排早班和夜班！")
            Exit Sub
        End If

        Call AddUnReDoInfo(True)
        FrmAssignRiQin.ShowDialog()
        Call ListAllViewInfo()
        Call CSRefreshDiagram()
    End Sub

    Private Sub 安排完夜班ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 安排完夜班ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If
        If CSAutoPlanPara Is Nothing Then
            MsgBox("请先设置自动安排参数!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
            Exit Sub
        End If

        Call AddUnReDoInfo(True)
        Call FormNightDrivers()
        Call SortDriverbyLeaveWork("夜班")
        Call ListAllViewInfo()
        Call CSRefreshDiagram()
    End Sub
    Private Sub 用餐设置DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 用餐设置DToolStripMenuItem.Click
        FrmAssignDinner.ShowDialog()
    End Sub

    Private Sub 用餐设置YToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 用餐设置YToolStripMenuItem.Click
        If CSTimeTablePara.nPubTrain > 0 AndAlso CSTimeTablePara.nPubCheDi > 0 AndAlso CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSdriverNo <> "#" Then
            FrmSetDinner.SelectDriver = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi)
            FrmSetDinner.ShowDialog()
        End If
    End Sub

    Private Sub 输出车底交路CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 输出车底交路CToolStripMenuItem.Click
        If CSchediInfo IsNot Nothing AndAlso UBound(CSchediInfo) > 0 Then
            frmPrintCheDiJiaoLu.ShowDialog()
        Else
            MsgBox("没有找到运行图信息，请先打开运行图！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
        End If
    End Sub

    Private Sub 任务段衔接ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 任务段衔接ToolStripMenuItem.Click
        FrmSegCon.ShowDialog()
    End Sub


    Public Sub FormDayDinnerTimeDrivers() '安排白班吃饭时段的司机，正推
        Call SortMergedCSLinkTrain(True)
        Dim AvaMerTrains As New List(Of MergedCSLinkTrain)
        For Each tmertrain As MergedCSLinkTrain In CSTrainsAndDrivers.MergedCSLinkTrains
            If tmertrain IsNot Nothing AndAlso tmertrain.IsLinked = False AndAlso AddLitterTime(tmertrain.CulEndTime) < 15 * 3600 AndAlso AddLitterTime(tmertrain.CulEndTime) > 10 * 3600 Then
                AvaMerTrains.Add(tmertrain)
            End If
        Next
        Me.ProgressBar1.Maximum = AvaMerTrains.Count
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.Visible = True
        labInfor.Text = "正在处理请稍后..."
        labInfor.Visible = True

        Dim dayNeedDinnerDriverNum As Integer = 0
        Dim dayNeedDriverNum As Integer = NChediNum
        Dim DinnerStartMerTarin As New Dictionary(Of typeDinnerStation, MergedCSLinkTrain)
        For i As Integer = 1 To UBound(sysDinnerStation)
            If sysDinnerStation(i).dinnerType <> "午餐" Then
                Continue For
            End If
            sysDinnerStation(i).NeedDinnerDriverNum = 0
            Dim firstTime As Integer = 0
            For j As Integer = 0 To AvaMerTrains.Count - 1
                If AvaMerTrains(j).EndStaName = sysDinnerStation(i).DinnerStationName And (sysDinnerStation(i).Direction = AvaMerTrains(j).UpOrDown Or sysDinnerStation(i).Direction = 2) And AvaMerTrains(j).RoutingName = sysDinnerStation(i).Routing Then
                    If AvaMerTrains(j).CulEndTime > sysDinnerStation(i).dinnerStartTime Then
                        If AvaMerTrains(j).beiche <> 0 Then
                            Continue For
                        End If
                        If firstTime = 0 Then
                            firstTime = AvaMerTrains(j).CulEndTime
                            If DinnerStartMerTarin.ContainsKey(sysDinnerStation(i)) = False Then
                                DinnerStartMerTarin.Add(sysDinnerStation(i), AvaMerTrains(j))
                            End If
                        Else
                            If AvaMerTrains(j).CulEndTime < firstTime + sysDinnerStation(i).DinnerTime Then
                                sysDinnerStation(i).NeedDinnerDriverNum += 1
                            End If
                        End If
                    End If
                End If
            Next
        Next
        Dim nf As New frmDayDinnerInfo
        If nf.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            If nf.DayDriverNum <> 0 Then
                CSAutoPlanPara.DayDutyNum = nf.DayDriverNum
            End If
            For i As Integer = 1 To UBound(sysDinnerStation)
                If DinnerStartMerTarin.ContainsKey(sysDinnerStation(i)) Then
                    Dim removeList As New List(Of MergedCSLinkTrain)
                    For Each tmer As MergedCSLinkTrain In AvaMerTrains
                        If AddLitterTime(tmer.CulEndTime) < AddLitterTime(DinnerStartMerTarin(sysDinnerStation(i)).CulEndTime) Or tmer.beiche <> 0 Then
                            Continue For
                        End If
                        Dim maxWaitTime As Integer = -1
                        Dim selectDri As CSDriver = Nothing
                        Dim AvaDris As New List(Of CSDriver)
                        Dim AttOffPlace As Boolean = False

                        '====普通任务段处理
                        Dim ifDirSame As Boolean = False
                        If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then         '找出可以接车的所有司机
                            For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                                If dri IsNot Nothing AndAlso dri.DutySort = "白班" Then
                                    Dim upTainDirection As Integer = TransitStationPlaceforUptrain(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime)
                                    If (upTainDirection <> -1 And upTainDirection <> 2) AndAlso upTainDirection <> tmer.UpOrDown Then '判断是否是可以接的方向
                                        Continue For
                                    End If
                                    If sysDinnerStation(i).IfOnlyDinner = True And dri.State = "用餐" Then
                                        dri.State = "班中"
                                    End If
                                    If dri.CanDriveTheTrain(tmer) Then
                                        If upTainDirection = -1 Then
                                            AttOffPlace = True
                                        End If
                                        AvaDris.Add(dri)
                                    End If
                                End If
                            Next
                        End If
                        If AttOffPlace = False Then
                            '最大休息时间人接车 
                            maxWaitTime = -1
                            For Each dri As CSDriver In AvaDris
                                Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                                If waittime > maxWaitTime And waittime >= ChangePlaceRestTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime) Then
                                    maxWaitTime = waittime
                                    selectDri = dri
                                End If
                            Next
                        Else
                            maxWaitTime = 24 * 3600 '最小
                            For Each dri As CSDriver In AvaDris
                                Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                                If waittime < maxWaitTime Then
                                    maxWaitTime = waittime
                                    selectDri = dri
                                End If
                            Next
                        End If

                        If selectDri IsNot Nothing Then
                            Dim getTime As Integer = 0
                            For j As Integer = 1 To UBound(selectDri.CSLinkTrain)
                                If selectDri.CSLinkTrain(j).EndStaName = sysDinnerStation(i).DinnerStationName Then
                                    getTime += 1
                                End If
                            Next
                            If getTime < 2 Then
                                selectDri.AddMergedTrain(tmer)
                                removeList.Add(tmer)
                                selectDri.RefreshState()
                            Else
                                selectDri.State = "班中"
                            End If
                        Else
                            If tmer.EndStaName = sysDinnerStation(i).DinnerStationName And (sysDinnerStation(i).Direction = tmer.UpOrDown Or sysDinnerStation(i).Direction = 2) And tmer.RoutingName = sysDinnerStation(i).Routing Then
                                If sysDinnerStation(i).IfOnlyDinner = True Then
                                    If CSTrainsAndDrivers.DayDrivers.Count < sysDinnerStation(i).AddNewDinnerDriverNum Then
                                        Call AddANewDriverforMerged("白班", tmer, "替饭")
                                        removeList.Add(tmer)
                                    End If
                                Else
                                    Call AddANewDriverforMerged("白班", tmer, "吃饭")
                                    removeList.Add(tmer)
                                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).RefreshState()
                                End If
                            End If
                        End If
                        Me.ProgressBar1.PerformStep()
                        System.Windows.Forms.Application.DoEvents()
                    Next
                    If sysDinnerStation(i).IfOnlyDinner = True Then
                        For j As Integer = 0 To CSTrainsAndDrivers.DayDrivers.Count - 1
                            CSTrainsAndDrivers.DayDrivers(j).ReRemoveTrain(1)
                        Next
                    End If
                    For j As Integer = 0 To removeList.Count - 1
                        For Each tmer As MergedCSLinkTrain In AvaMerTrains
                            If removeList(j).CGTrainID = tmer.CGTrainID Then
                                AvaMerTrains.Remove(tmer)
                                Exit For
                            End If
                        Next
                    Next
                End If
            Next
            Call AllRefreshState()
            Me.ProgressBar1.Visible = False
            labInfor.Text = "吃饭阶段编制结束"
            Me.LabelPro.Visible = False
        End If

    End Sub
    ' ''' <summary>
    ' ''' 连接下一趟车列车，专门用于安排吃饭
    ' ''' </summary>
    ' ''' <param name="tmpDriver"></param>
    ' ''' <param name="tmpJiexuTimeOrJiange"></param>
    ' ''' <param name="ForOrBack">true "后"，false “前”</param>
    ' ''' <param name="timeOrJiange">true "time"，false “jiange”</param>
    ' ''' <remarks></remarks>
    'Public Sub CatchOneTrain(tmpDriver As CSDriver, tmpJiexuTimeOrJiange As Integer, ForOrBack As Boolean, timeOrJiange As Boolean)

    '    If timeOrJiange = True Then ''时间
    '        If tmpDriver.CSLinkTrain.Count > 1 Then
    '            If ForOrBack = True Then '’后
    '                Dim tmpCurTime As Integer = tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).CulEndTime
    '                Dim tmpCurStationName As String = tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).EndStaName
    '                For Each train As MergedCSLinkTrain In CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "出发")
    '                    If IsDinnerPlace(train.StartStaName, train.UpOrDown, train.RoutingName, False) Then ''判断是否是吃饭点及接车方向
    '                        If train.CulStartTime >= tmpCurTime + tmpJiexuTimeOrJiange And train.IsLinked = False Then
    '                            tmpDriver.AddMergedTrain(train)
    '                            Exit For
    '                        End If
    '                    End If
    '                Next

    '                If IsTransitStationPlace(tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).EndStaName, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).RoutingName, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).UpOrDown, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).CulEndTime) = False And _
    '                     IsDinnerPlace(tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).EndStaName, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).UpOrDown, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).RoutingName) = False Then
    '                    NormalCatchOneTrain(tmpDriver, 0, True, False)
    '                End If

    '            Else '’前
    '                Dim tmpCurTime As Integer = tmpDriver.CSLinkTrain(1).CulStartTime
    '                Dim tmpCurStationName As String = tmpDriver.CSLinkTrain(1).StartStaName
    '                For i As Integer = CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "到达").Count - 1 To 0
    '                    Dim train As MergedCSLinkTrain = CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "到达")(i)
    '                    If IsDinnerPlace(train.StartStaName, train.UpOrDown, train.RoutingName, False) Then ''判断是否是吃饭点及接车方向
    '                        If train.CulStartTime + tmpJiexuTimeOrJiange <= tmpCurTime And train.IsLinked = False Then
    '                            tmpDriver.AddReMergedTrain(train)
    '                            Exit For
    '                        End If
    '                    End If
    '                Next

    '                'If IsTransitStationPlace(tmpDriver.CSLinkTrain(1).StartStaName, tmpDriver.CSLinkTrain(1).RoutingName, tmpDriver.CSLinkTrain(1).UpOrDown, tmpDriver.CSLinkTrain(1).CulStartTime) = False Then
    '                '    NormalCatchOneTrain(tmpDriver, 0, False, False)
    '                'End If

    '            End If
    '        End If

    '    Else '’间隔

    '        If tmpDriver.CSLinkTrain.Count > 1 Then
    '            If ForOrBack = True Then '’后
    '                Dim jiange As Integer = 0
    '                Dim tmpCurStationName As String = tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).EndStaName
    '                For Each train As MergedCSLinkTrain In CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "出发")
    '                    If IsDinnerPlace(train.StartStaName, train.UpOrDown, False) Then ''判断是否是吃饭点及接车方向
    '                        If train.CulStartTime >= tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).CulEndTime Then
    '                            jiange += 1
    '                            '间隔大于等于指定间隔
    '                            If jiange > tmpJiexuTimeOrJiange And train.IsLinked = False Then
    '                                tmpDriver.AddMergedTrain(train)
    '                                Exit For
    '                            End If

    '                        End If
    '                    End If
    '                Next

    '                If IsTransitStationPlace(tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).EndStaName, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).RoutingName, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).UpOrDown, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).CulEndTime) = False And _
    '                   IsDinnerPlace(tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).EndStaName, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).UpOrDown, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).RoutingName) = False Then
    '                    NormalCatchOneTrain(tmpDriver, 0, True, False)
    '                End If
    '            Else '’前
    '                Dim jiange As Integer = 0
    '                Dim tmpCurStationName As String = tmpDriver.CSLinkTrain(1).StartStaName
    '                For i As Integer = CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "到达").Count - 1 To 0
    '                    Dim train As MergedCSLinkTrain = CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "到达")(i)
    '                    If IsDinnerPlace(train.StartStaName, train.UpOrDown, False) Then ''判断是否是吃饭点及接车方向
    '                        If train.CulStartTime <= tmpDriver.CSLinkTrain(1).CulStartTime Then
    '                            jiange += 1
    '                            '间隔大于等于指定间隔
    '                            If jiange > tmpJiexuTimeOrJiange And train.IsLinked = False Then
    '                                tmpDriver.AddMergedTrain(train)
    '                                Exit For
    '                            End If
    '                        End If
    '                    End If
    '                Next
    '            End If

    '        End If


    '    End If

    'End Sub

    ' ''' <summary>
    ' ''' 连接下一趟车列车，普遍
    ' ''' </summary>
    ' ''' <param name="tmpDriver"></param>
    ' ''' <param name="tmpJiexuTimeOrJiange"></param>
    ' ''' <param name="ForOrBack">true "后"，false “前”</param>
    ' ''' <param name="timeOrJiange">true "time"，false “jiange”</param>
    ' ''' <remarks></remarks>
    'Public Sub NormalCatchOneTrain(tmpDriver As CSDriver, tmpJiexuTimeOrJiange As Integer, ForOrBack As Boolean, timeOrJiange As Boolean)
    '    If timeOrJiange = True Then ''时间
    '        If tmpDriver.CSLinkTrain.Count > 1 Then
    '            If ForOrBack = True Then '’后
    '                Dim tmpCurTime As Integer = tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).CulEndTime
    '                Dim tmpCurStationName As String = tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).EndStaName
    '                For Each train As MergedCSLinkTrain In CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "出发")
    '                    If train.CulStartTime >= tmpCurTime + tmpJiexuTimeOrJiange And train.IsLinked = False Then
    '                        tmpDriver.AddMergedTrain(train)
    '                        Exit For
    '                    End If
    '                    'End If
    '                Next

    '                If IsTransitStationPlace(tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).EndStaName, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).RoutingName, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).UpOrDown, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).EndTime) = False And _
    '                  IsDinnerPlace(tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).EndStaName, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).UpOrDown, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).RoutingName) = False Then
    '                    NormalCatchOneTrain(tmpDriver, 0, True, False)
    '                End If

    '            Else '’前
    '                Dim tmpCurTime As Integer = tmpDriver.CSLinkTrain(1).CulStartTime
    '                Dim tmpCurStationName As String = tmpDriver.CSLinkTrain(1).StartStaName
    '                For i As Integer = CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "到达").Count - 1 To 0
    '                    Dim train As MergedCSLinkTrain = CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "到达")(i)
    '                    If train.CulStartTime + tmpJiexuTimeOrJiange <= tmpCurTime And train.IsLinked = False Then
    '                        tmpDriver.AddReMergedTrain(train)
    '                        Exit For
    '                    End If
    '                    'End If
    '                Next
    '            End If
    '        End If

    '    Else '’间隔

    '        If tmpDriver.CSLinkTrain.Count > 1 Then
    '            If ForOrBack = True Then '’后
    '                Dim jiange As Integer = 0
    '                Dim tmpCurStationName As String = tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).EndStaName
    '                For Each train As MergedCSLinkTrain In CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "出发")
    '                    If train.CulStartTime >= tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).CulEndTime Then
    '                        jiange += 1
    '                        '间隔大于等于指定间隔
    '                        If jiange > tmpJiexuTimeOrJiange And train.IsLinked = False Then
    '                            tmpDriver.AddMergedTrain(train)
    '                            Exit For
    '                        End If

    '                    End If
    '                    'End If
    '                Next

    '                If IsTransitStationPlace(tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).EndStaName, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).RoutingName, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).UpOrDown, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).CulEndTime) = False And _
    '                  IsDinnerPlace(tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).EndStaName, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).UpOrDown, tmpDriver.CSLinkTrain(UBound(tmpDriver.CSLinkTrain)).RoutingName) = False Then
    '                    NormalCatchOneTrain(tmpDriver, 0, True, False)
    '                End If

    '            Else '’前
    '                Dim jiange As Integer = 0
    '                Dim tmpCurStationName As String = tmpDriver.CSLinkTrain(1).StartStaName
    '                For i As Integer = CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "到达").Count - 1 To 0
    '                    Dim train As MergedCSLinkTrain = CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "到达")(i)
    '                    'If IsDinnerPlace(train.StartStaName, train.UpOrDown, False) Then ''判断是否是吃饭点及接车方向
    '                    If train.CulStartTime <= tmpDriver.CSLinkTrain(1).CulStartTime Then
    '                        jiange += 1
    '                        '间隔大于等于指定间隔
    '                        If jiange > tmpJiexuTimeOrJiange And train.IsLinked = False Then
    '                            tmpDriver.AddMergedTrain(train)
    '                            Exit For
    '                        End If
    '                    End If
    '                    'End If
    '                Next
    '            End If
    '        End If
    '    End If
    'End Sub

    ' ''' <summary>
    ' ''' 寻找指定时间点后的列车
    ' ''' </summary>
    ' ''' <param name="tmpCurTime"></param>
    ' ''' <param name="tmpJiexuTimeOrJiange"></param>
    ' ''' <param name="ForOrBack">true "后"，false “前”</param>
    ' ''' <param name="timeOrJiange">true "time"，false “jiange”</param>
    ' ''' <remarks></remarks>
    'Public Function FindNextOneTrain(tmpCurTime As Integer, tmpCurStationName As String, tmpJiexuTimeOrJiange As Integer, ForOrBack As Boolean, timeOrJiange As Boolean) As MergedCSLinkTrain

    '    If timeOrJiange = True Then ''时间
    '        If ForOrBack = True Then '’后
    '            For Each train As MergedCSLinkTrain In CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "出发")
    '                If IsDinnerPlace(train.StartStaName, train.UpOrDown, train.RoutingName, False) Then ''判断是否是吃饭点及接车方向
    '                    If train.CulStartTime >= tmpCurTime + tmpJiexuTimeOrJiange And train.IsLinked = False Then
    '                        Return train
    '                    End If
    '                End If
    '            Next
    '        Else '’前
    '            For i As Integer = CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "到达").Count - 1 To 0
    '                Dim train As MergedCSLinkTrain = CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "到达")(i)
    '                If IsDinnerPlace(train.StartStaName, train.UpOrDown, train.RoutingName, False) Then ''判断是否是吃饭点及接车方向
    '                    If train.CulStartTime + tmpJiexuTimeOrJiange <= tmpCurTime And train.IsLinked = False Then
    '                        Return train
    '                    End If
    '                End If
    '            Next
    '        End If
    '    Else '’间隔
    '        If ForOrBack = True Then '’后
    '            Dim jiange As Integer = 0
    '            For Each train As MergedCSLinkTrain In CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "出发")
    '                If IsDinnerPlace(train.StartStaName, train.UpOrDown, train.RoutingName, False) Then ''判断是否是吃饭点及接车方向
    '                    If train.CulStartTime >= tmpCurTime Then
    '                        jiange += 1
    '                        '间隔大于等于指定间隔
    '                        If jiange > tmpJiexuTimeOrJiange And train.IsLinked = False Then
    '                            Return train
    '                        End If
    '                    End If
    '                End If
    '            Next
    '        Else '’前
    '            Dim jiange As Integer = 0
    '            For i As Integer = CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "到达").Count - 1 To 0
    '                Dim train As MergedCSLinkTrain = CSTrainsAndDrivers.dicStationTrain(tmpCurStationName & "到达")(i)
    '                If IsDinnerPlace(train.StartStaName, train.UpOrDown, train.RoutingName, False) Then ''判断是否是吃饭点及接车方向
    '                    If train.CulStartTime <= tmpCurTime Then
    '                        jiange += 1
    '                        '间隔大于等于指定间隔
    '                        If jiange > tmpJiexuTimeOrJiange And train.IsLinked = False Then
    '                            Return train
    '                        End If
    '                    End If
    '                End If
    '            Next
    '        End If
    '    End If
    '    Return Nothing
    'End Function
    Public Sub FormPreDayDinner()
        Call SortMergedCSLinkTrain(False)
        Dim AvaMerTrains As New List(Of MergedCSLinkTrain)
        For Each tmertrain As MergedCSLinkTrain In CSTrainsAndDrivers.MergedCSLinkTrains
            If tmertrain IsNot Nothing AndAlso tmertrain.IsLinked = False Then
                AvaMerTrains.Add(tmertrain)
            End If
        Next
        Me.ProgressBar1.Maximum = AvaMerTrains.Count
        Me.ProgressBar1.Step = 1
        Me.ProgressBar1.Visible = True
        labInfor.Text = "正在处理请稍后..."
        labInfor.Visible = True
        For Each tmer As MergedCSLinkTrain In AvaMerTrains
            If tmer.IsLinked = False Then
                Dim maxWaitTime As Integer = -1
                Dim selectDri As CSDriver = Nothing
                Dim AvaDris As New List(Of CSDriver)
                '====普通任务段处理
                selectDri = Nothing
                AvaDris = New List(Of CSDriver)
                Dim ifDirSame As Boolean = False
                If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then           '找出可以接车的所有司机
                    For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                        If dri IsNot Nothing AndAlso dri.DutySort = "白班" AndAlso dri.dutyWork = "吃饭" Then
                            Dim upTainDirection As Integer = TransitStationPlaceforUptrain(tmer.CSLinkTrains(1).EndStaName, tmer.CSLinkTrains(1).RoutingName, tmer.CSLinkTrains(1).UpOrDown, tmer.CSLinkTrains(1).CulEndTime)
                            If (upTainDirection <> -1 And upTainDirection <> 2) AndAlso upTainDirection <> dri.CSLinkTrain(1).UpOrDown Then '判断是否是可以接的方向
                                Continue For
                            End If
                            If dri.CanDriveTheTrain1(tmer) Then
                                AvaDris.Add(dri)
                            End If
                        End If
                    Next
                End If
                maxWaitTime = -1
                '最大休息时间接车,但需要满足各换乘点最低休息要求          
                For Each dri As CSDriver In AvaDris
                    Dim waittime As Integer = dri.CSLinkTrain(1).CulStartTime - tmer.CulEndTime
                    If waittime > maxWaitTime And waittime >= ChangePlaceRestTime(dri.CSLinkTrain(1).StartStaName, dri.CSLinkTrain(1).RoutingName, dri.CSLinkTrain(1).UpOrDown, dri.CSLinkTrain(1).CulStartTime, False) Then
                        maxWaitTime = waittime
                        selectDri = dri
                    End If
                Next
                If selectDri IsNot Nothing Then
                    selectDri.AddReMergedTrain(tmer)
                    selectDri.RefreshState(, False)
                End If
                Me.ProgressBar1.PerformStep()
                System.Windows.Forms.Application.DoEvents()
            End If
        Next
        Call AllRefreshState(False)
        Me.ProgressBar1.Visible = False
        Me.LabelPro.Visible = False
    End Sub
    Private Sub ListViewDuty_MouseClick(sender As Object, e As MouseEventArgs) Handles ListViewDuty.MouseClick
        If Me.ListViewDuty.SelectedItems.Count > 0 Then
            If CSTimeTablePara.sCurDiagramState = DiagramState.运行图 Then
                CSTimeTablePara.nPubCheDi = Me.ListViewDuty.SelectedItems(0).SubItems(1).Text
                If CSTimeTablePara.nPubCheDi > 0 Then
                    SelectDriver(CSTimeTablePara.nPubCheDi)
                    Call ListCurDutyInfo()
                Else
                    CSTimeTablePara.picPubDiagram.Refresh()
                End If
            End If
        End If
    End Sub
    Public FangkuanXianzhi As Boolean = False
    Private Sub 放宽限制ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 放宽限制ToolStripMenuItem.Click
        If Me.放宽限制ToolStripMenuItem.Checked = False Then
            Me.放宽限制ToolStripMenuItem.Checked = True
            FangkuanXianzhi = True
        Else
            Me.放宽限制ToolStripMenuItem.Checked = False
            FangkuanXianzhi = False
        End If
    End Sub

    Private Sub 备车设置ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 备车设置ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If
        Dim frm As New FrmPrepareTrain
        frm.ShowDialog()
        Try
            If sState = "乘务计划编制" And frm.flag = 1 Then
                If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 1 Then
                    CSTrainsAndDrivers = New CSTrainAndDrivers()
                End If
                Call refreshAfterParaSet()
            End If
        Catch ex As Exception
            MsgBox("重置编制过程失败！")
        End Try
    End Sub

    Private Sub 衔接计划设置ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 衔接计划设置ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If
        Dim nf As New FrmCorPlanSet
        If nf.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            If nf.CheckBox1.Checked Then
                CSTrainsAndDrivers.IfCorSchedule = True
                CSTrainsAndDrivers.CorCSPlanName = nf.CmbPlans.Text.Trim
                Call ReadCorDriversAndTrains(CSTrainsAndDrivers.CorCSPlanName)
                Call ReadCorPrepareDrivers(CSTrainsAndDrivers.CorCSPlanName)
            Else
                CSTrainsAndDrivers.IfCorSchedule = False
            End If
        End If
    End Sub

    Private Sub 夜早班搭配ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 夜早班搭配ToolStripMenuItem.Click
        If strQCurCSPlanID = "" Then
            MsgBox("请先保存正在编制的乘务计划")
            Exit Sub
        End If
        frmInputBox.Text = "夜早班衔接"
        frmInputBox.labTitle.Text = "选择衔接类型:"
        frmInputBox.cmbText.Visible = True
        frmInputBox.txtText.Visible = False
        frmInputBox.cmbText.Items.Clear()
        frmInputBox.cmbText.Items.Add("当前计划夜早班衔接")
        frmInputBox.cmbText.Items.Add("当前计划夜班衔接""" & CSTrainsAndDrivers.CorCSPlanName & """早班")
        frmInputBox.cmbText.Items.Add("""" & CSTrainsAndDrivers.CorCSPlanName & """夜班衔接当前计划早班")
        frmInputBox.cmbText.SelectedIndex = 0
        If frmInputBox.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim nf As New FrmCorNitMor
            Select Case frmInputBox.cmbText.SelectedIndex
                Case 0
                    nf.tCorStyle = FrmCorNitMor.CorStyle.自身夜早班衔接
                Case 1
                    nf.tCorStyle = FrmCorNitMor.CorStyle.自身夜班被动衔接
                Case 2
                    nf.tCorStyle = FrmCorNitMor.CorStyle.自身早班主动衔接
                Case Else
                    MsgBox("请选择正确的夜早班衔接类型！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                    Exit Sub
            End Select
            nf.ShowDialog()
        End If
       

    End Sub

    Private Sub 衔接计划管理ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 衔接计划管理ToolStripMenuItem.Click
        FrmCorTTManage.Show()
    End Sub

    Private Sub 人数测算ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 人数测算ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If
        Frmrenshucesuan.Show()
    End Sub

    Private Sub 查找ToolStripButton12_Click(sender As Object, e As EventArgs) Handles 查找ToolStripButton12.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If
        Dim frm1 As New FrmTrainTT(Me)
        frm1.Show()
    End Sub

 
    Private Sub 按时间清空ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 按时间清空ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If

        frmInputBox.Text = "清空部分计划"
        frmInputBox.labTitle.Text = "输入开始时间:"
        frmInputBox.cmbText.Visible = False
        frmInputBox.txtText.Visible = True
        frmInputBox.txtText.Text = "在此输入时间，格式为""HH:mm:ss"""

        If frmInputBox.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim DateTime As DateTime = Nothing
            Try
                DateTime = CDate(StrInputBoxText.Trim)
            Catch ex As Exception
                MsgBox("请输入正确的时间格式！")
                Exit Sub
            End Try
            Dim StartTime As Integer = DateTime.TimeOfDay.TotalSeconds
            Call AddUnReDoInfo(True)
            Call ClearPartPlan(StartTime)
            Call ListAllViewInfo()
            Call CSRefreshDiagram()
        End If
    End Sub

    Private Sub 清空早班ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 清空早班ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If
        Call AddUnReDoInfo(True)
        Call ClearDutyPlan("早班")
        Call ListAllViewInfo()
        Call CSRefreshDiagram()
    End Sub

    Private Sub 清空夜班ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 清空夜班ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If
        Call AddUnReDoInfo(True)
        Call ClearDutyPlan("夜班")
        Call ListAllViewInfo()
        Call CSRefreshDiagram()
    End Sub

   
    Private Sub 清空白班ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 清空白班ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If
        Call AddUnReDoInfo(True)
        Call ClearDutyPlan("白班")
        Call ListAllViewInfo()
        Call CSRefreshDiagram()
    End Sub

    Private Sub 清空日勤班ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 清空日勤班ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If
        Call AddUnReDoInfo(True)
        Call ClearDutyPlan("日勤班")
        Call ListAllViewInfo()
        Call CSRefreshDiagram()
    End Sub

    Private Sub 整理吃饭ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 整理吃饭ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSDrivers Is Nothing OrElse UBound(CSTrainsAndDrivers.CSDrivers) <= 0 Then
            MsgBox("未找到打开的乘务计划，请先打开需输出的乘务计划！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
            Exit Sub
        End If

        For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
            If dri IsNot Nothing Then
               
                dri.AllDinnerInfo.Clear()
                dri.FlagDinner = False
                dri.DinnerStation = ""
                dri.DinnerStartTime = 0
                dri.DinnerEndTime = 0
                Dim dintime As typeDinnerStation = Nothing
                If UBound(dri.CSLinkTrain) > 1 Then
                    For i As Integer = 1 To UBound(dri.CSLinkTrain) - 1
                        If i = 1 Then
                            dintime = Nothing
                            For j As Integer = 1 To UBound(sysDinnerStation)
                                If sysDinnerStation(j).dutySort = dri.DutySort AndAlso sysDinnerStation(j).DinnerStationName = dri.CSLinkTrain(1).StartStaName Then
                                    For z As Integer = 1 To UBound(Jiaolu)
                                        If Jiaolu(z).JiaoluName = sysDinnerStation(j).Routing And Jiaolu(z).ReJiaoluName = dri.CSLinkTrain(1).RoutingName Then
                                            If AddLitterTime(sysDinnerStation(j).dinnerStartTime) <= AddLitterTime(dri.CSLinkTrain(1).StartTime) And AddLitterTime(sysDinnerStation(j).dinnerEndTime) >= AddLitterTime(dri.CSLinkTrain(1).StartTime) Then
                                                dintime = sysDinnerStation(j)
                                                Exit For
                                            End If
                                        End If
                                    Next
                                End If
                            Next
                            If IsNothing(dintime) = False Then
                                dri.FlagDinner = True
                                dri.AllDinnerInfo.Add(dri.CSLinkTrain(1).StartStaName & "-" & (AddLitterTime(dri.CSLinkTrain(1).StartTime) - dintime.DinnerTime).ToString & "-" & AddLitterTime(dri.CSLinkTrain(1).StartTime).ToString, dintime)
                            End If
                        End If
                        dintime = Nothing
                        Dim intervaltime As Integer = dri.CSLinkTrain(i + 1).CulStartTime - dri.CSLinkTrain(i).CulEndTime
                        For j As Integer = 1 To UBound(sysDinnerStation)
                            If sysDinnerStation(j).dutySort = dri.DutySort AndAlso sysDinnerStation(j).DinnerStationName = dri.CSLinkTrain(i).EndStaName And sysDinnerStation(j).Routing = dri.CSLinkTrain(i).RoutingName Then
                                If AddLitterTime(sysDinnerStation(j).dinnerEndTime) >= AddLitterTime(dri.CSLinkTrain(i).EndTime) AndAlso AddLitterTime(sysDinnerStation(j).dinnerStartTime) <= AddLitterTime(dri.CSLinkTrain(i).EndTime) And intervaltime >= sysDinnerStation(j).DinnerTime Then
                                    dintime = sysDinnerStation(j)
                                    Exit For
                                End If
                            End If
                        Next
                        If IsNothing(dintime) = False Then
                            If dri.FlagDinner = False Then
                                dri.FlagDinner = True
                                dri.AllDinnerInfo.Add(dri.CSLinkTrain(i).EndStaName & "-" & AddLitterTime(dri.CSLinkTrain(i).EndTime).ToString & "-" & (AddLitterTime(dri.CSLinkTrain(i).EndTime) + dintime.DinnerTime).ToString, dintime)
                            Else
                                If dri.AllDinnerInfo.Count > 0 Then
                                    Dim ifhaveSameDinnerItem As Boolean = False
                                    For Each dinneritem As typeDinnerStation In dri.AllDinnerInfo.Values
                                        If dintime.dinnerType = dinneritem.dinnerType Then
                                            ifhaveSameDinnerItem = True
                                            Exit For
                                        End If
                                    Next
                                    If ifhaveSameDinnerItem = False Then
                                        dri.AllDinnerInfo.Add(dri.CSLinkTrain(i).EndStaName & "-" & AddLitterTime(dri.CSLinkTrain(i).EndTime).ToString & "-" & (AddLitterTime(dri.CSLinkTrain(i).EndTime) + dintime.DinnerTime).ToString, dintime)
                                    End If
                                End If
                            End If
                        End If

                    Next
                    dintime = Nothing
                    For j As Integer = 1 To UBound(sysDinnerStation)
                        If sysDinnerStation(j).dutySort = dri.DutySort AndAlso sysDinnerStation(j).DinnerStationName = dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName And sysDinnerStation(j).Routing = dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName Then
                            If AddLitterTime(sysDinnerStation(j).dinnerEndTime) >= AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime) AndAlso AddLitterTime(sysDinnerStation(j).dinnerStartTime) <= AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime) Then
                                dintime = sysDinnerStation(j)
                                Exit For
                            End If
                        End If
                    Next
                    If IsNothing(dintime) = False Then
                        If dri.FlagDinner = False Then
                            dri.FlagDinner = True
                            dri.AllDinnerInfo.Add(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName & "-" & AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime).ToString & "-" & (AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime) + dintime.DinnerTime).ToString, dintime)
                        Else
                            If dri.AllDinnerInfo.Count > 0 Then
                                Dim ifhaveSameDinnerItem As Boolean = False
                                For Each dinneritem As typeDinnerStation In dri.AllDinnerInfo.Values
                                    If dintime.dinnerType = dinneritem.dinnerType Then
                                        ifhaveSameDinnerItem = True
                                        Exit For
                                    End If
                                Next
                                If ifhaveSameDinnerItem = False Then
                                    dri.AllDinnerInfo.Add(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName & "-" & AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime).ToString & "-" & (AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime) + dintime.DinnerTime).ToString, dintime)
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Next
        Call ListAllViewInfo()
        Call CSRefreshDiagram()
        MsgBox("整理完成!", MsgBoxStyle.OkOnly, "提醒")
    End Sub


    Private Sub 位置图导出ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 位置图导出ToolStripMenuItem.Click
        Dim frm1 As New FrmOutputPosition(2)
        frm1.ShowDialog()
    End Sub

    Private Sub 白班中午吃饭ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 白班中午吃饭ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing OrElse UBound(CSTrainsAndDrivers.CSLinkTrains) <= 0 Then
            MsgBox("请先选择运行图！", MsgBoxStyle.OkOnly, "未选择运行图")
            Exit Sub
        End If
        If CSAutoPlanPara Is Nothing Then
            MsgBox("请先设置自动安排参数!", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        If MsgBox("请确保白班尚未编制完毕！", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Call AddUnReDoInfo(True)
            Call FormDayDinnerTimeDrivers()
            Call ListAllViewInfo()
            Call CSRefreshDiagram()
        End If
    End Sub

    Private Sub 按输出号查找任务ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 按输出号查找任务ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSDrivers Is Nothing OrElse UBound(CSTrainsAndDrivers.CSDrivers) = 0 Then
            Exit Sub
        End If
        Dim i As Integer
        frmInputBox.Text = "查找列车"
        frmInputBox.labTitle.Text = "选择或输入列车输出车次:"
        frmInputBox.cmbText.Visible = True
        frmInputBox.txtText.Visible = False
        frmInputBox.cmbText.Items.Clear()
        If UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
            For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                If CSTrainsAndDrivers.CSDrivers(i).OutPutCSdriverNo <> "" AndAlso CSTrainsAndDrivers.CSDrivers(i).CSdriverNo <> "#" Then
                    If frmInputBox.cmbText.Items.IndexOf(CSTrainsAndDrivers.CSDrivers(i).OutPutCSdriverNo) = -1 Then
                        frmInputBox.cmbText.Items.Add(CSTrainsAndDrivers.CSDrivers(i).OutPutCSdriverNo)
                    End If
                End If
            Next i
        End If
        If frmInputBox.cmbText.Items.Count > 0 Then
            frmInputBox.cmbText.SelectedIndex = 0
        Else
            MsgBox("没有可查询任务,请先重新编号！")
            Exit Sub
        End If

        If frmInputBox.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            If UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
                Dim selectDriIndex As Integer = -1
                For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                    If CSTrainsAndDrivers.CSDrivers(i).OutPutCSdriverNo = StrInputBoxCombText Then
                        selectDriIndex = i
                        Exit For
                    End If
                Next
                If selectDriIndex <> -1 Then
                    CSTimeTablePara.nPubCheDi = selectDriIndex
                    For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain)
                        If CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).IsDeadHeading = False Then
                            CSTimeTablePara.nPubTrain = CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).CSTrainID
                            Exit For
                        End If
                    Next
                    Call SetCurScrollbarInSelectTrain(CSTimeTablePara.nPubTrain)
                    Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
                    Me.PicDiagram.Refresh()
                    rBmpGraphics = Me.PicDiagram.CreateGraphics()
                    Dim tmpPen As Pen
                    tmpPen = New Pen(Color.Blue, 2)
                    Dim tmpPen1 As Pen = New Pen(Color.DarkSlateGray, 2)
                    tmpPen1.DashStyle = Drawing2D.DashStyle.DashDot
                    For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain)
                        If CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).IsDeadHeading = False Then
                            Call CSDrawLineInPicture(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i).CSTrainID, rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                        Else
                            Call CSDrawLineInPicture(CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).CSLinkTrain(i), rBmpGraphics, tmpPen1, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                        End If
                    Next
                    If CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedMorDriver IsNot Nothing Then
                        For Each train As CSLinkTrain In CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedMorDriver.CSLinkTrain
                            If train IsNot Nothing Then
                                If train.IsDeadHeading = False Then
                                    Call CSDrawLineInPicture(train.CSTrainID, rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                                Else
                                    Call CSDrawLineInPicture(train, rBmpGraphics, tmpPen1, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                                End If
                            End If
                        Next
                    End If
                    If CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedNightDriver IsNot Nothing Then
                        For Each train As CSLinkTrain In CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).LinkedNightDriver.CSLinkTrain
                            If train IsNot Nothing Then
                                If train.IsDeadHeading = False Then
                                    Call CSDrawLineInPicture(train.CSTrainID, rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                                Else
                                    Call CSDrawLineInPicture(train, rBmpGraphics, tmpPen1, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                                End If
                            End If
                        Next
                    End If
                Else
                    MsgBox("没有找到当前车次!")
                End If
            Else
                MsgBox("没有找到当前车次!")
            End If
        End If
    End Sub

    Private Sub 按内在编号查找任务ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 按内在编号查找任务ToolStripMenuItem.Click
        If CSTrainsAndDrivers.CSDrivers Is Nothing OrElse UBound(CSTrainsAndDrivers.CSDrivers) = 0 Then
            Exit Sub
        End If
        Dim i As Integer
        frmInputBox.Text = "查找列车"
        frmInputBox.labTitle.Text = "选择或输入列车车次:"
        frmInputBox.cmbText.Visible = True
        frmInputBox.txtText.Visible = False
        frmInputBox.cmbText.Items.Clear()
        If UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
            For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo <> "" AndAlso CSTrainsAndDrivers.CSDrivers(i).CSdriverNo <> "#" Then
                    frmInputBox.cmbText.Items.Add(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo)
                End If
            Next i
        End If
        If frmInputBox.cmbText.Items.Count > 0 Then
            frmInputBox.cmbText.SelectedIndex = 0
        End If

        If frmInputBox.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            If UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
                Dim selectDriIndex As Integer = -1
                For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                    If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo = StrInputBoxCombText Then
                        selectDriIndex = i
                        Exit For
                    End If
                Next
                If selectDriIndex <> -1 Then
                    SelectDriver(selectDriIndex)
                Else
                    MsgBox("没有找到当前任务!")
                End If
            Else
                MsgBox("没有找到当前任务!")
            End If
        End If
    End Sub

    Private Sub 替饭ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 替饭ToolStripMenuItem.Click
        AddUnReDoInfo(True)
        If CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).dutyWork = "替饭" Then
            If MsgBox("当前已是替饭状态，是否取消？", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).dutyWork = ""
            End If
        Else
            CSTrainsAndDrivers.CSDrivers(CSTimeTablePara.nPubCheDi).dutyWork = "替饭"
        End If

        Call CSRefreshDiagram()
        Call ListAllViewInfo()
    End Sub


    Private Sub 合并短任务ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 合并短任务ToolStripMenuItem.Click
        If CSTrainsAndDrivers.MorningDrivers.Count = 0 Or CSTrainsAndDrivers.NightDrivers.Count = 0 Or CSTrainsAndDrivers.DayDrivers.Count = 0 Then
            MsgBox("请先排早班、夜班、白班！")
            Exit Sub
        End If
        Dim avMornDis As Double = 0
        Dim avNightDis As Double = 0
        Dim avDayDis As Double = 0
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then
            For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                If dri IsNot Nothing AndAlso dri.DutySort = "早班" Then
                    avMornDis += dri.DriveDistance
                End If
                If dri IsNot Nothing AndAlso dri.DutySort = "夜班" Then
                    avNightDis += dri.DriveDistance
                End If
                If dri IsNot Nothing AndAlso dri.DutySort = "白班" Then
                    avDayDis += dri.DriveDistance
                End If
            Next
        End If
        avMornDis /= CSTrainsAndDrivers.MorningDrivers.Count
        avNightDis /= CSTrainsAndDrivers.NightDrivers.Count
        avDayDis /= CSTrainsAndDrivers.DayDrivers.Count
        AddUnReDoInfo(True)
        optimateMorning(avMornDis)
        optimateNight(avNightDis)
        optimateDay(avDayDis)
        Call ListAllViewInfo()
        Call CSRefreshDiagram()
    End Sub
    '白班人数少，减少早班的方法是1.把跑的多或跑的少的早班与跑的少的白班合并到白班
    Public Sub optimateMorning(ByVal avMornDis As Integer)
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then
            Dim removeList As New List(Of CSDriver)
            Me.ProgressBar1.Maximum = CSTrainsAndDrivers.MorningDrivers.Count
            Me.ProgressBar1.Value = 1
            Me.ProgressBar1.Step = 1
            Me.ProgressBar1.Visible = True
            labInfor.Text = "正在处理请稍后..."
            For Each dri As CSDriver In CSTrainsAndDrivers.MorningDrivers
                Me.ProgressBar1.PerformStep()
                System.Windows.Forms.Application.DoEvents()
                If dri IsNot Nothing AndAlso (dri.DriveDistance < avMornDis * 0.8 Or dri.DriveDistance > avMornDis * 1.2) Then '看起来更均衡
                    Dim interval As Integer = 0
                    Dim intervaltime As Integer = 0
                    Dim direction As Integer = 2
                    For i As Integer = 0 To ChangeStationList.Count - 1
                        If ChangeStationList(i).Name = dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName And ChangeStationList(i).JiaoLuName = dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName Then
                            If (ChangeStationList(i).Direction = dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown Or ChangeStationList(i).Direction = 2) Then
                                If ChangeStationList(i).TimeSpanList.EndTime = ChangeStationList(i).TimeSpanList.StartTime Then '没有时间限制
                                    interval = ChangeStationList(i).FollowNo
                                    intervaltime = ChangeStationList(i).RestTime
                                    direction = ChangeStationList(i).UpTrainDirection
                                    Exit For
                                End If
                                If AddLitterTime(ChangeStationList(i).TimeSpanList.StartTime) <= AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime) And AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime) <= AddLitterTime(ChangeStationList(i).TimeSpanList.EndTime) Then
                                    interval = ChangeStationList(i).FollowNo
                                    intervaltime = ChangeStationList(i).RestTime
                                    direction = ChangeStationList(i).UpTrainDirection
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                    Dim choList As New List(Of CSDriver)
                    For Each daydri As CSDriver In CSTrainsAndDrivers.DayDrivers
                        If daydri.dutyWork <> "替饭" AndAlso daydri.CSLinkTrain(1).StartStaName = dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName AndAlso AddLitterTime(daydri.CSLinkTrain(1).StartTime) - AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime) > intervaltime AndAlso _
                            AddLitterTime(daydri.CSLinkTrain(1).StartTime) - AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime) < 45 * 60 AndAlso dri.DriveDistance + daydri.DriveDistance < CS_DayMaxLength Then
                            choList.Add(daydri)
                        End If
                    Next
                    If choList.Count > 0 Then
                        For i As Integer = 0 To choList.Count - 2
                            For j As Integer = i + 1 To choList.Count - 1
                                Dim firScore As Double = Math.Log(AddLitterTime(choList(i).CSLinkTrain(1).StartTime) - AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime)) * 0.6 + Math.Log(choList(i).DriveDistance) * 0.4
                                Dim secScore As Double = Math.Log(AddLitterTime(choList(j).CSLinkTrain(1).StartTime) - AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime)) * 0.6 + Math.Log(choList(j).DriveDistance) * 0.4

                                If firScore > secScore Then
                                    Dim tmp As CSDriver = choList(i)
                                    choList(i) = choList(j)
                                    choList(j) = tmp
                                End If
                            Next
                        Next
                        Dim selectDri As CSDriver = choList(0)
                        If interval > 0 Then
                            For i As Integer = 1 To choList.Count - 1
                                If dri.CheckIfInterval(interval, direction, choList(i).CSLinkTrain(1)) = True Then
                                    selectDri = choList(i)
                                    Exit For
                                End If
                            Next
                        End If
                        Dim flag As Integer = 0
                        For i As Integer = UBound(dri.CSLinkTrain) To 1 Step -1
                            selectDri.ReAddTrain(dri.CSLinkTrain(i))
                        Next
                        removeList.Add(dri)
                        Continue For
                    End If
                End If
            Next
            If removeList.Count > 0 Then
                For i As Integer = 0 To removeList.Count - 1
                    RemoveDriver(removeList(i))
                Next
            End If
            labInfor.Text = "早班处理完毕！"
            Me.ProgressBar1.Visible = False
        End If
    End Sub
    '白班人数少，减少夜班的方法是1.把跑的少的夜班同跑的少的白班结合
    Public Sub optimateNight(ByVal avNightDis As Integer)
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then
            Dim removeList As New List(Of CSDriver)
            Me.ProgressBar1.Maximum = CSTrainsAndDrivers.NightDrivers.Count
            Me.ProgressBar1.Value = 1
            Me.ProgressBar1.Step = 1
            Me.ProgressBar1.Visible = True
            labInfor.Text = "正在处理请稍后..."
            labInfor.Visible = True
            For Each dri As CSDriver In CSTrainsAndDrivers.NightDrivers
                Me.ProgressBar1.PerformStep()
                System.Windows.Forms.Application.DoEvents()
                If dri IsNot Nothing AndAlso (dri.DriveDistance < avNightDis * 0.8 Or dri.DriveDistance > avNightDis * 1.2) Then
                    Dim interval As Integer = 0
                    Dim intervaltime As Integer = 0
                    Dim traindirection As Integer = 2
                    For i As Integer = 0 To ChangeStationList.Count - 1
                        If ChangeStationList(i).Name = dri.CSLinkTrain(1).StartStaName Then
                            For z As Integer = 1 To UBound(Jiaolu)
                                If ChangeStationList(i).JiaoLuName = Jiaolu(z).JiaoluName And dri.CSLinkTrain(1).RoutingName = Jiaolu(z).ReJiaoluName Then
                                    If (ChangeStationList(i).UpTrainDirection = dri.CSLinkTrain(1).UpOrDown Or ChangeStationList(i).Direction = 2) Then
                                        If ChangeStationList(i).TimeSpanList.EndTime = ChangeStationList(i).TimeSpanList.StartTime Then '没有时间限制
                                            interval = ChangeStationList(i).FollowNo
                                            intervaltime = ChangeStationList(i).RestTime
                                            traindirection = ChangeStationList(i).UpTrainDirection
                                            Exit For
                                        End If
                                        If AddLitterTime(ChangeStationList(i).TimeSpanList.StartTime) <= AddLitterTime(dri.CSLinkTrain(1).StartTime) And AddLitterTime(dri.CSLinkTrain(1).StartTime) <= AddLitterTime(ChangeStationList(i).TimeSpanList.EndTime) Then
                                            interval = ChangeStationList(i).FollowNo
                                            intervaltime = ChangeStationList(i).RestTime
                                            traindirection = ChangeStationList(i).UpTrainDirection
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                        End If
                    Next
                    Dim choList As New List(Of CSDriver)
                    For Each daydri As CSDriver In CSTrainsAndDrivers.DayDrivers
                        If daydri.dutyWork <> "替饭" AndAlso daydri.CSLinkTrain(UBound(daydri.CSLinkTrain)).EndStaName = dri.CSLinkTrain(1).StartStaName AndAlso AddLitterTime(dri.CSLinkTrain(1).StartTime) - AddLitterTime(daydri.CSLinkTrain(UBound(daydri.CSLinkTrain)).EndTime) > intervaltime AndAlso _
                            AddLitterTime(dri.CSLinkTrain(1).StartTime) - AddLitterTime(daydri.CSLinkTrain(UBound(daydri.CSLinkTrain)).EndTime) < 45 * 60 AndAlso dri.DriveDistance + daydri.DriveDistance < CS_DayMaxLength Then
                            choList.Add(daydri)
                        End If
                    Next
                    If choList.Count > 0 Then
                        For i As Integer = 0 To choList.Count - 2
                            For j As Integer = i + 1 To choList.Count - 1
                                Dim firScore As Double = Math.Log(AddLitterTime(dri.CSLinkTrain(1).StartTime) - AddLitterTime(choList(i).CSLinkTrain(UBound(choList(i).CSLinkTrain)).EndTime)) * 0.6 + Math.Log(choList(i).DriveDistance) * 0.4
                                Dim secScore As Double = Math.Log(AddLitterTime(dri.CSLinkTrain(1).StartTime) - AddLitterTime(choList(j).CSLinkTrain(UBound(choList(j).CSLinkTrain)).EndTime)) * 0.6 + Math.Log(choList(j).DriveDistance) * 0.4

                                If firScore > secScore Then
                                    Dim tmp As CSDriver = choList(i)
                                    choList(i) = choList(j)
                                    choList(j) = tmp
                                End If
                            Next
                        Next
                        Dim selectDri As CSDriver = choList(0)
                        If interval > 0 Then
                            For i As Integer = 1 To choList.Count - 1
                                If dri.CheckIfInterval(interval, traindirection, choList(i).CSLinkTrain(UBound(choList(i).CSLinkTrain)), True) = True Then
                                    selectDri = choList(i)
                                    Exit For
                                End If
                            Next
                        End If
                        Dim flag As Integer = 0
                        For i As Integer = 1 To UBound(dri.CSLinkTrain)
                            selectDri.AddTrain(dri.CSLinkTrain(i))
                        Next
                        removeList.Add(dri)
                        Continue For
                    End If
                End If
            Next
            If removeList.Count > 0 Then
                For i As Integer = 0 To removeList.Count - 1
                    RemoveDriver(removeList(i))
                Next
            End If
            labInfor.Text = "夜班处理完毕！"
            Me.ProgressBar1.Visible = False
        End If
    End Sub
    '把白班走的最少的尽可能和晚高峰回库的夜班结合
    Public Sub optimateDay(ByVal avDayDis As Integer)
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then

            Me.ProgressBar1.Maximum = CSTrainsAndDrivers.DayDrivers.Count
            Me.ProgressBar1.Value = 1
            Me.ProgressBar1.Step = 1
            Me.ProgressBar1.Visible = True
            labInfor.Text = "正在处理请稍后..."
            For Each dri As CSDriver In CSTrainsAndDrivers.DayDrivers
                Me.ProgressBar1.PerformStep()
                System.Windows.Forms.Application.DoEvents()
                If dri IsNot Nothing AndAlso dri.DriveDistance < avDayDis * 0.8 Then '看起来更均衡
                    Dim interval As Integer = 0
                    Dim intervaltime As Integer = 0
                    Dim direction As Integer = 2
                    For i As Integer = 0 To ChangeStationList.Count - 1
                        If ChangeStationList(i).Name = dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName And ChangeStationList(i).JiaoLuName = dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName Then
                            If (ChangeStationList(i).Direction = dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown Or ChangeStationList(i).Direction = 2) Then
                                If ChangeStationList(i).TimeSpanList.EndTime = ChangeStationList(i).TimeSpanList.StartTime Then '没有时间限制
                                    interval = ChangeStationList(i).FollowNo
                                    intervaltime = ChangeStationList(i).RestTime
                                    direction = ChangeStationList(i).UpTrainDirection
                                    Exit For
                                End If
                                If AddLitterTime(ChangeStationList(i).TimeSpanList.StartTime) <= AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime) And AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime) <= AddLitterTime(ChangeStationList(i).TimeSpanList.EndTime) Then
                                    interval = ChangeStationList(i).FollowNo
                                    intervaltime = ChangeStationList(i).RestTime
                                    direction = ChangeStationList(i).UpTrainDirection
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                    Dim choList As New List(Of CSDriver)
                    For Each daydri As CSDriver In CSTrainsAndDrivers.NightDrivers
                        If daydri.dutyWork <> "替饭" AndAlso daydri.CSLinkTrain(1).StartStaName = dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName AndAlso AddLitterTime(daydri.CSLinkTrain(1).StartTime) - AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime) > intervaltime AndAlso _
                            AddLitterTime(daydri.CSLinkTrain(1).StartTime) - AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime) < 45 * 60 AndAlso dri.DriveDistance + daydri.DriveDistance <= CS_DayMaxLength AndAlso daydri.CSLinkTrain(UBound(daydri.CSLinkTrain)).SecondStation.IsYard = True And AddLitterTime(daydri.CSLinkTrain(UBound(daydri.CSLinkTrain)).EndTime) < 20 * 3600 Then
                            choList.Add(daydri)
                        End If
                    Next
                    If choList.Count > 0 Then
                        For i As Integer = 0 To choList.Count - 2
                            For j As Integer = i + 1 To choList.Count - 1
                                Dim firScore As Double = Math.Log(AddLitterTime(choList(i).CSLinkTrain(1).StartTime) - AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime)) * 0.6 + Math.Log(choList(i).DriveDistance) * 0.4
                                Dim secScore As Double = Math.Log(AddLitterTime(choList(j).CSLinkTrain(1).StartTime) - AddLitterTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime)) * 0.6 + Math.Log(choList(j).DriveDistance) * 0.4

                                If firScore > secScore Then
                                    Dim tmp As CSDriver = choList(i)
                                    choList(i) = choList(j)
                                    choList(j) = tmp
                                End If
                            Next
                        Next
                        Dim selectDri As CSDriver = choList(0)
                        If interval > 0 Then
                            For i As Integer = 1 To choList.Count - 1
                                If dri.CheckIfInterval(interval, direction, choList(i).CSLinkTrain(1)) = True Then
                                    selectDri = choList(i)
                                    Exit For
                                End If
                            Next
                        End If
                        Dim flag As Integer = 0
                        For i As Integer = 1 To UBound(selectDri.CSLinkTrain)
                            dri.AddTrain(selectDri.CSLinkTrain(i))
                        Next
                        RemoveDriver(selectDri)
                        Continue For
                    End If
                End If
            Next
            labInfor.Text = "白班处理完毕！"
            Me.ProgressBar1.Visible = False
        End If
    End Sub

   
    Private Sub 重新处理晚餐ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 重新处理晚餐ToolStripMenuItem.Click
        Dim mintime As Integer = 100000000
        For i As Integer = 1 To UBound(sysDinnerStation)
            If sysDinnerStation(i).dinnerType = "晚餐" Then
                If mintime > sysDinnerStation(i).dinnerStartTime Then
                    mintime = sysDinnerStation(i).dinnerStartTime
                End If
            End If
        Next
        If mintime <> 100000000 Then
            Call AddUnReDoInfo(True)
            For Each Cdriver As CSDriver In CSTrainsAndDrivers.CSDrivers
                If Cdriver IsNot Nothing AndAlso Cdriver.DutySort <> "早班" AndAlso Cdriver.CSLinkTrain(UBound(Cdriver.CSLinkTrain)).CulEndTime >= mintime AndAlso Cdriver.dutyWork <> "替饭" Then
                    For i As Integer = 1 To UBound(Cdriver.CSLinkTrain)
                        If AddLitterTime(Cdriver.CSLinkTrain(i).EndTime) >= mintime Then
                            If i = 1 Then
                                For j As Integer = 1 To UBound(Cdriver.CSLinkTrain)
                                    If Cdriver.CSLinkTrain(j).FirstStation.IsYard = True And Cdriver.CSLinkTrain(j).distance < minChuDis Then
                                        Cdriver.CSLinkTrain(j).IsLinked = False
                                        j += 1
                                        Continue For
                                    End If
                                    If Cdriver.CSLinkTrain(j).SecondStation.IsYard = True And Cdriver.CSLinkTrain(j).distance < minRuDis Then
                                        Continue For
                                    End If
                                    Cdriver.CSLinkTrain(j).IsLinked = False
                                Next
                                RemoveDriver(Cdriver) '注意islink没取消
                            Else
                                Cdriver.RemoveTrain(i - 1)
                                For Each s As String In Cdriver.AllDinnerInfo.Keys
                                    If Cdriver.AllDinnerInfo(s).dinnerType = "晚餐" Then
                                        Cdriver.AllDinnerInfo.Remove(s)
                                        Exit For
                                    End If
                                Next
                                If Cdriver.AllDinnerInfo.Count = 0 Then
                                    Cdriver.FlagDinner = False
                                End If
                                Cdriver.RefreshState()
                            End If
                            Exit For
                        End If
                    Next
                End If
            Next
            Call SortMergedCSLinkTrain(True)
            Dim AvaMerTrains As New List(Of MergedCSLinkTrain)
            For Each tmertrain As MergedCSLinkTrain In CSTrainsAndDrivers.MergedCSLinkTrains
                If tmertrain IsNot Nothing AndAlso tmertrain.IsLinked = False Then
                    AvaMerTrains.Add(tmertrain)
                End If
            Next

            Me.ProgressBar1.Maximum = AvaMerTrains.Count
            Me.ProgressBar1.Value = 1
            Me.ProgressBar1.Step = 1
            Me.ProgressBar1.Visible = True
            labInfor.Text = "正在处理请稍后..."
            labInfor.Visible = True
            For Each tmer As MergedCSLinkTrain In AvaMerTrains
              
                Dim maxWaitTime As Integer = -1
                Dim selectDri As CSDriver = Nothing
                Dim AvaDris As New List(Of CSDriver)
                Dim AttOffPlace As Boolean = False
                If tmer.beiche <> 0 Then
                    If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then           '找出可以接车的所有司机
                        For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                            If dri IsNot Nothing AndAlso dri.DutySort <> "早班" AndAlso dri.dutyWork <> "替饭" Then
                                Dim upTainDirection As Integer = TransitStationPlaceforUptrain(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime)
                                If (upTainDirection <> -1 And upTainDirection <> 2) AndAlso upTainDirection <> tmer.UpOrDown Then '判断是否是可以接的方向
                                    Continue For
                                End If
                              
                                If dri.DriveDinnnerTrain(tmer) Then
                                    If tmer.dutywork = "吃饭" And dri.dutyWork = "已吃饭" Then
                                        Continue For
                                    End If
                                    If upTainDirection = -1 Then
                                        AttOffPlace = True
                                    End If
                                    AvaDris.Add(dri)
                                Else
                                    If dri.dutyWork = "备车" Then
                                        If upTainDirection = -1 Then
                                            AttOffPlace = True
                                        End If
                                        AvaDris.Add(dri)
                                    End If
                                End If
                            End If
                        Next
                    End If
                    For Each dri As CSDriver In AvaDris
                        If dri IsNot Nothing AndAlso dri.dutyWork = "备车" Then
                            If dri.CSLinkTrain(UBound(dri.CSLinkTrain)).nCheDiID = tmer.nCheDiID AndAlso dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName = tmer.CSLinkTrains(1).StartStaName Then
                                selectDri = dri
                                Exit For
                            End If
                        End If
                    Next
                    If selectDri Is Nothing Then
                        If AttOffPlace = False Then
                            '最大休息时间人接车 
                            maxWaitTime = -1
                            For Each dri As CSDriver In AvaDris
                                If dri.dutyWork = "备车" Then
                                    Continue For
                                End If
                                Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                                If waittime > maxWaitTime And waittime >= ChangePlaceRestTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime) Then
                                    maxWaitTime = waittime
                                    selectDri = dri
                                End If
                            Next
                        Else
                            maxWaitTime = 24 * 3600 '最小
                            For Each dri As CSDriver In AvaDris
                                If dri.dutyWork = "备车" Then
                                    Continue For
                                End If
                                Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                                If waittime < maxWaitTime Then
                                    maxWaitTime = waittime
                                    selectDri = dri
                                End If
                            Next
                        End If
                    End If

                    If selectDri IsNot Nothing Then
                        If tmer.beiche = 1 Then
                            selectDri.dutyWork = "备车"
                        Else
                            selectDri.dutyWork = ""
                        End If
                        selectDri.AddMergedTrain(tmer)
                        selectDri.RefreshState()
                    Else
                        Call AddANewDriverforMerged("夜班", tmer)
                        If tmer.beiche = 1 Then
                            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).dutyWork = "备车"
                        End If
                        CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).RefreshState()
                    End If
                    Continue For
                End If

                '====普通任务段处理
                selectDri = Nothing
                AvaDris = New List(Of CSDriver)
                Dim ifDirSame As Boolean = False
                If CSTrainsAndDrivers.CSDrivers IsNot Nothing Then         '找出可以接车的所有司机
                    For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                        If dri IsNot Nothing AndAlso dri.DutySort <> "早班" AndAlso dri.dutyWork <> "替饭" Then
                            Dim upTainDirection As Integer = TransitStationPlaceforUptrain(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime)
                            If (upTainDirection <> -1 And upTainDirection <> 2) AndAlso upTainDirection <> tmer.UpOrDown Then '判断是否是可以接的方向
                                Continue For
                            End If
                         
                            If dri.DriveDinnnerTrain(tmer) Then
                                If dri.dutyWork = "备车" Then
                                    Continue For
                                End If
                                If tmer.dutywork = "吃饭" And dri.dutyWork = "已吃饭" Then
                                    Continue For
                                End If
                                If upTainDirection = -1 Then
                                    AttOffPlace = True
                                End If
                                AvaDris.Add(dri)
                            End If
                        End If
                    Next
                End If
                If AttOffPlace = False Then
                    '最大休息时间人接车 
                    maxWaitTime = -1
                    For Each dri As CSDriver In AvaDris
                        Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                        If waittime > maxWaitTime And waittime >= ChangePlaceRestTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).RoutingName, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).UpOrDown, dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime) Then
                            maxWaitTime = waittime
                            selectDri = dri
                        End If
                    Next
                Else
                    maxWaitTime = 24 * 3600 '最小
                    For Each dri As CSDriver In AvaDris
                        Dim waittime As Integer = tmer.CSLinkTrains(1).CulStartTime - dri.CSLinkTrain(UBound(dri.CSLinkTrain)).CulEndTime
                        If waittime < maxWaitTime Then
                            maxWaitTime = waittime
                            selectDri = dri
                        End If
                    Next
                End If

                If selectDri IsNot Nothing Then
                    If tmer.dutywork = "吃饭" Then
                        selectDri.dutyWork = "已吃饭"
                    End If
                    selectDri.AddMergedTrain(tmer)
                    selectDri.RefreshState()
                Else
                    Call AddANewDriverforMerged("夜班", tmer)
                    If tmer.dutywork = "吃饭" Then
                        CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).dutyWork = "已吃饭"
                    End If
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).RefreshState()
                End If
                Me.ProgressBar1.PerformStep()
                System.Windows.Forms.Application.DoEvents()
            Next
            Call AllRefreshState()
            Me.ProgressBar1.Visible = False
            Me.LabelPro.Visible = False

        End If
        Call ListAllViewInfo()
        Call CSRefreshDiagram()
    End Sub
End Class
