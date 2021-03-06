
Public Class frmODSTimeTableMain
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
    Private Sub frmTimeTableMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MsgBox("确认退出运行图界面吗？", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + vbDefaultButton2, "确认操作") = MsgBoxResult.No Then
            'Dim nf As New frmTimeTableManager
            'nf.ShowDialog()
            e.Cancel = True
        End If
    End Sub

    Private Sub frmTimeTableMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.ControlKey Then
            nIfPreseCtrl = 1
        ElseIf e.KeyCode = Keys.ShiftKey Then
            nIfPreseCtrl = 2
        End If
    End Sub

    Private Sub frmTimeTableMain_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.ControlKey Then
            nIfPreseCtrl = 0
        ElseIf e.KeyCode = Keys.ShiftKey Then
            nIfPreseCtrl = 0
        End If
    End Sub


    Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PicDiagram.Width = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
        PicDiagram.Height = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        picStation.Width = TimeTablePara.TimeTableDiagramPara.sngPicStationWidth
        picStation.Height = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        PicStation2.Width = TimeTablePara.TimeTableDiagramPara.sngPicStationWidth
        PicStation2.Height = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        'sngPicTimeLineWidth = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth - TimeTablePara.TimeTableDiagramPara.sngLeftBlank * 2 - TimeTablePara.TimeTableDiagramPara.sngStaBlank
        Me.PicDiagram.Left = 0
        Me.PicDiagram.Top = 0
        Me.picStation.Left = 0
        Me.picStation.Top = 0
        Me.PicStation2.Left = 0
        Me.PicStation2.Top = 0

        sysMenuState = "最初状态"
        TimeTablePara.sInputDataError = ""

        If TimeTablePara.sInputDataError <> "" Then
            Me.Close()
            Exit Sub
        End If
        TimeTablePara.picPubDiagram = Me.PicDiagram
        TimeTablePara.picPubStation = Me.picStation
        TimeTablePara.picPubStation2 = Me.PicStation2
        Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
        TimeTablePara.picPubDiagram.Image = New System.Drawing.Bitmap(TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight)
        rBmpGraphics = Graphics.FromImage(TimeTablePara.picPubDiagram.Image)
        sysMenuState = "打开了数据库"
        Me.ProBar.Visible = False
        intCurPrintPage = 0
        nPubAdjustTrainLineState = 0 '调整运行线的状态
        'Me.timerCurDate.Enabled = True
        SplitDiagram.Panel2.AutoScrollPosition = New Point(100, 0)
        Me.SplitDiagram.Panel2.VerticalScroll.Enabled = True
        Me.KeyPreview = True
        nIfPreseCtrl = 0
        Call frmMain_Resize(Nothing, Nothing)
        sysMenuState = "打开了数据库"
        Call ReSetMenuState()
        Call RefreshDiagram(0)
    End Sub

    Private Sub frmTimeTableMain_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
    End Sub

    Public Sub frmMain_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        '调整底图的位置
        'undo 不完全正确
        '水平位置()
        'If Me.SplitConLeftRight.Panel2.Width > Me.PicDiagram.Width Then
        '    Me.PicDiagram.Left = (Me.SplitConLeftRight.Panel2.Width - Me.PicDiagram.Width) / 2
        'Else
        '    Me.PicDiagram.Left = 0
        'End If

        '竖直位置
        Me.picStation.Height = Me.PicDiagram.Height
        Me.PicStation2.Height = Me.PicDiagram.Height
        If Me.SplitDiagram.Panel2.Height > Me.PicDiagram.Height Then
            Me.PicDiagram.Top = (Me.SplitDiagram.Panel2.Height - Me.PicDiagram.Height) / 2
            Me.picStation.Top = Me.PicDiagram.Top
            Me.PicStation2.Top = Me.PicDiagram.Top
        Else
            'Me.PicDiagram.Top = 0
            Me.picStation.Top = 0
            Me.PicStation2.Top = 0
        End If

        '车站名图页
        'If Me.SplitConLeftRight.Panel1.Width > Me.PicDiagram.Width Then
        '    Me.PicStation.Left = (Me.SplitConLeftRight.Panel1.Width - Me.PicStation.Width) / 2
        'Else
        '    Me.PicStation.Left = 0
        'End If

        ''竖直位置
        'If Me.SplitConLeftRight.Panel2.Height > Me.picStation.Height Then
        'Else
        '    Me.picStation.Top = 0
        'End If
    End Sub

    Private Sub 小时格ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 小时格ToolStripMenuItem.Click
        TimeTablePara.TimeTableDiagramPara.strTimeFormat = "小时格"
        Call RefreshDiagram(0)
    End Sub

    Private Sub 十分格ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 十分格ToolStripMenuItem.Click
        TimeTablePara.TimeTableDiagramPara.strTimeFormat = "十分格"
        Call RefreshDiagram(0)

    End Sub

    Private Sub 二分格ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 二分格ToolStripMenuItem.Click
        TimeTablePara.TimeTableDiagramPara.strTimeFormat = "二分格"
        Call RefreshDiagram(0)
    End Sub

    Private Sub 一分格ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 一分格ToolStripMenuItem.Click
        TimeTablePara.TimeTableDiagramPara.strTimeFormat = "一分格"
        Call RefreshDiagram(0)
    End Sub

    Private Sub 放大底图宽度ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 放大底图宽度ToolStripMenuItem.Click
        If TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth > 10000 Then
            TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = 10000
        Else
            TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth * 1.2
        End If
        Me.PicDiagram.Width = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
        Me.PicDiagram.Height = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        Call RefreshDiagram(0)
        ' Me.PicDiagram.Left = 0
    End Sub

    Private Sub 缩小底图宽度ToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles 缩小底图宽度ToolStripMenuItem.Click
        If TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth < 900 Then
            TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = 900
        Else
            TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth / 1.2
        End If
        Me.PicDiagram.Width = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
        Me.PicDiagram.Height = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        Call RefreshDiagram(0)
        'Me.PicDiagram.Left = 0
    End Sub

    Private Sub 小时格ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 小时格ToolStripMenuItem1.Click
        Call 小时格ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 十分格ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 十分格ToolStripMenuItem1.Click
        Call 十分格ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 二分格ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 二分格ToolStripMenuItem1.Click
        Call 二分格ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 一分格ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 一分格ToolStripMenuItem1.Click
        Call 一分格ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 底图放大ToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 底图放大ToolStripButton.Click
        Call 放大底图宽度ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 底图缩小ToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 底图缩小ToolStripButton.Click
        缩小底图宽度ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 打开运行图ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If g_strNetMainPath = "" Then
            MsgBox("请先打开运行图数据库", , "提示")
            Exit Sub
        End If

        Dim nf As New frmTimeTalbeSelect
        nf.ShowDialog()
        If nf.frmOk = True Then
            TimeTablePara.nPubTrain = 0
            ReDim TimeTablePara.nPubTrains(0)
            Call addOneUndoInf()
            Call RefreshDiagram(1)
        End If
        Call listTitle()
    End Sub

    Private Sub 刷新运行图ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 刷新运行图ToolStripMenuItem.Click
        Me.cmbJiShuTuJieSta.Visible = False
        TimeTablePara.sCurDiagramState = DiagramState.运行图
        Call RefreshDiagram(0)
    End Sub

    Private Sub 保存运行图ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 运行图另存为图片ToolStripMenuItem.Click

        Dim strPath As String
        Dim New0penFile As New SaveFileDialog
        New0penFile.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|jpeg files (*.jpeg)|*.jpeg|All files (*.*)|*.*"
        New0penFile.FilterIndex = 1
        New0penFile.RestoreDirectory = True
        strPath = ""
        If New0penFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strPath = New0penFile.FileName
            Me.PicDiagram.Image.Save(strPath)
        End If

    End Sub

    Private Sub 查看列车时刻表TToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If TimeTableIFOpened() = 1 Then
            Dim nf As New frmListTimeTable
            nf.Show()
        End If
    End Sub

    '判断系统是否已经打开时刻表
    Public Function TimeTableIFOpened() As Integer
        If TimeTablePara.sPubCurSkbName = "" Then
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
    '菜单设置项
    Public Sub ReSetMenuState()
        Select Case ODSPubpara.sCurShowListState
            Case "显示换乘站图"
                Me.运行图ToolStripMenuItem.Visible = False
                Me.车底交路图CToolStripMenuItem.Visible = False
                'Me.车站股道技术图解JToolStripMenuItem.Visible = False
                'Me.运行图数据接口KToolStripMenuItem.Visible = False
                'Me.ToolStripSeparator24.Visible = False
                'Me.列车时刻表TToolStripMenuItem.Visible = False
                Me.增加列车ToolStripMenuItem1.Visible = False
                Me.ToolStripSeparator13.Visible = False
                Me.保存运行图SToolStripMenuItem.Visible = False
                Me.保存ToolStripButton7.Visible = False
            Case Else
                Me.运行图ToolStripMenuItem.Visible = True
                Me.车底交路图CToolStripMenuItem.Visible = True
                'Me.车站股道技术图解JToolStripMenuItem.Visible = True
                'Me.运行图数据接口KToolStripMenuItem.Visible = True
                'Me.ToolStripSeparator24.Visible = True
                Me.列车时刻表TToolStripMenuItem.Visible = True
                Me.增加列车ToolStripMenuItem1.Visible = True
                Me.ToolStripSeparator13.Visible = True
                Me.保存运行图SToolStripMenuItem.Visible = True
                Me.保存ToolStripButton7.Visible = True
        End Select
        If OdsSet.OdsSysPara.sViewDiagramRights = True Then
            Me.文件7FToolStripMenuItem.Visible = False
            Me.运行图编制ToolStripMenuItem.Enabled = False
            Me.列车重编车次BToolStripMenuItem.Enabled = False
            Me.编辑EToolStripMenuItem.Visible = False
            Me.保存ToolStripButton7.Visible = False
            Me.打印运行图ToolStripButton1.Visible = False
            Me.ToolStripSeparator3.Visible = False
            Me.剪切ToolStripButton10.Visible = False
            Me.粘贴ToolStripButton13.Visible = False
            Me.复制ToolStripButton9.Visible = False
            Me.删除ToolStripButton11.Visible = False
            Me.撤销tolStripUndo.Visible = False
            Me.重复tolStripRedo.Visible = False
            Me.ToolStripSeparator29.Visible = False
        Else

        End If
     
        Call UndoAndRedoMenuSet()
        Call CopyAndPasteMnuSet()
        Call listTitle()
    End Sub


    Private Sub 退出EToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 退出EToolStripMenuItem.Click
        If MsgBox("确定退出系统吗？", MsgBoxStyle.Information + MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton1, "确认") = MsgBoxResult.Ok Then
            Me.Close()
        End If

    End Sub

    Private Sub 打开运行图ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 打开运行图ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 司机报表输出CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim nf As New frmDriverReport
        'nf.Show()
    End Sub

    Private Sub PicDiagram_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PicDiagram.MouseDown
        sngForX = e.X
        Dim nCurTrain As Integer
        Dim i As Integer
        Me.PicDiagram.ContextMenuStrip = Nothing
        If TimeTablePara.sCurDiagramState = DiagramState.运行图 Then
            TimeTablePara.lngCurMouseDownTime = GetMouseMoveTime(e.X, Me.PicDiagram.Width - TimeTablePara.TimeTableDiagramPara.sngLeftBlank * 2)
            If e.Button = Windows.Forms.MouseButtons.Left Then
                Select Case nPubAdjustTrainLineState
                    Case 0 ' '选择列车
                        If nIfPreseCtrl = 1 Then '按住CTRL选择
                            nCurTrain = SeekTrainNumberByXYCoord(e.X, e.Y, TimeTablePara.TimeTableDiagramPara.sngtopBlank, Me.PicDiagram.Height)
                            If nCurTrain > 0 Then
                                Call AddSelectTrains(nCurTrain)
                            End If
                            If UBound(TimeTablePara.nPubTrains) > 0 Then
                                TimeTablePara.nPubTrain = TimeTablePara.nPubTrains(1)
                                TimeTablePara.nPubCheDi = CheCiToCheDiID(TimeTablePara.nPubTrain)
                                Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
                                Me.PicDiagram.Refresh()
                                rBmpGraphics = Me.PicDiagram.CreateGraphics()
                                Dim tmpPen As Pen
                                tmpPen = New Pen(Color.SpringGreen, 2)
                                For i = 1 To UBound(TimeTablePara.nPubTrains)
                                    Call TMSDrawLineInPicture(TimeTablePara.nPubTrains(i), rBmpGraphics, tmpPen, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX, TimeTablePara.TimeTableDiagramPara.nifPrintCheCi, TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                                Next
                                nIFShowAllCheDiTrain = 0
                            Else
                                Me.PicDiagram.Refresh()
                            End If
                            Call ShowLabInfor(nCurTrain, Me.labInfor)
                        Else
                            nCurTrain = SeekTrainNumberByXYCoord(e.X, e.Y, TimeTablePara.TimeTableDiagramPara.sngtopBlank, Me.PicDiagram.Height)
                            TimeTablePara.nPubTrain = nCurTrain 'MsgBox(nTrain)
                            nCurSecID = GetSecIDFormYcord(e.Y, TimeTablePara.nPubTrain)
                            nCurStaID = GetStaIDFormYcord(e.Y, TimeTablePara.nPubTrain)
                            'MsgBox(SectionInf(nCurSecID).sSecName)
                            If nCurTrain > 0 Then
                                ReDim TimeTablePara.nPubTrains(1)
                                TimeTablePara.nPubTrains(1) = nCurTrain
                                TimeTablePara.nPubCheDi = CheCiToCheDiID(nCurTrain)
                                Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
                                Me.PicDiagram.Refresh()
                                rBmpGraphics = Me.PicDiagram.CreateGraphics()
                                Dim tmpPen As Pen
                                tmpPen = New Pen(Color.SpringGreen, 2)
                                Call TMSDrawLineInPicture(nCurTrain, rBmpGraphics, tmpPen, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX, TimeTablePara.TimeTableDiagramPara.nifPrintCheCi, TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                                nIFShowAllCheDiTrain = 0
                            Else
                                Me.PicDiagram.Refresh()
                            End If
                            Call ShowLabInfor(nCurTrain, Me.labInfor)
                        End If
                        Call CopyAndPasteMnuSet()
                        Call UndoAndRedoMenuSet()
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
            Else
                If TimeTablePara.nPubTrain > 0 Then
                    If UBound(TimeTablePara.nPubTrains) > 1 Then
                        If ODSPubpara.sCurShowListState = "显示换乘站图" Then
                            Me.PicDiagram.ContextMenuStrip = Nothing
                        Else
                            Me.PicDiagram.ContextMenuStrip = Me.cmnuTrains
                        End If
                        If OdsSet.OdsSysPara.sViewDiagramRights = True Then
                            Me.PicDiagram.ContextMenuStrip = Nothing
                        End If
                    Else
                        If ODSPubpara.sCurShowListState = "显示换乘站图" Then
                            Me.PicDiagram.ContextMenuStrip = Me.cmuTrainLine2
                        Else
                            Me.PicDiagram.ContextMenuStrip = Me.cmuTrainLine
                        End If
                        If OdsSet.OdsSysPara.sViewDiagramRights = True Then
                            Me.PicDiagram.ContextMenuStrip = Me.cmuTrainLine2
                        End If
                    End If
                Else
                    If ODSPubpara.sCurShowListState = "显示换乘站图" Then
                        Me.PicDiagram.ContextMenuStrip = Nothing
                    Else
                        Me.PicDiagram.ContextMenuStrip = Me.cmuDrawSingleLine
                    End If
                    If OdsSet.OdsSysPara.sViewDiagramRights = True Then
                        Me.PicDiagram.ContextMenuStrip = Nothing
                    End If
                End If
            End If

        ElseIf TimeTablePara.sCurDiagramState = DiagramState.技术图解 Then
            If e.Button = Windows.Forms.MouseButtons.Left Then
                Select Case TimeTablePara.nStaJiShuTuJieSeletedState
                    Case 0 '选择列车
                        nCurTrain = SeekJiShuTuJieTrainNumberByXYCoord(e.X, e.Y, Me.cmbJiShuTuJieSta.Text.Trim)
                        TimeTablePara.nPubTrain = nCurTrain
                        Dim nStaId As Integer
                        nStaId = StaNameToStaInfID(Me.cmbJiShuTuJieSta.Text.Trim)
                        nGuDaoID = GetCurGuDaoBianHaoFromYCoord(e.Y)
                        If nCurTrain > 0 Then
                            TimeTablePara.nPubCheDi = CheCiToCheDiID(nCurTrain)
                            Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
                            Me.PicDiagram.Refresh()
                            rBmpGraphics = Me.PicDiagram.CreateGraphics()
                            Dim tmpPen As Pen
                            tmpPen = New Pen(Color.SpringGreen, 2)
                            Call DrawJiShuTuJieGuDaoLineInPicture(nCurTrain, rBmpGraphics, tmpPen, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX, nStaId, nGuDaoID)
                            Call ShowLabInfor(nCurTrain, Me.labInfor)
                            nIFShowAllCheDiTrain = 0
                        Else
                            Me.PicDiagram.Refresh()
                        End If

                End Select
            Else
                If TimeTablePara.nPubTrain > 0 Then
                    Me.ContextMenuStrip = Me.cmuGuDaoLine
                    If GuDaoJishutujie.sCurSeleteState = "车站股道" Then
                        Me.修改停站时间TToolStripMenuItem.Enabled = True
                    Else
                        Me.修改停站时间TToolStripMenuItem.Enabled = False
                    End If
                Else
                    Me.ContextMenuStrip = Nothing
                End If
            End If
        End If
        'MsgBox(e.X)
    End Sub

    Private Sub PicDiagram_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PicDiagram.Paint
        If TimeTablePara.sCurDiagramState = DiagramState.运行图 Then
            Dim i As Integer
            Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
            Dim tmpPen As Pen
            rBmpGraphics = Me.PicDiagram.CreateGraphics()
            tmpPen = New Pen(Color.SpringGreen, 2)
            If UBound(TimeTablePara.nPubTrains) > 0 Then
                For i = 1 To UBound(TimeTablePara.nPubTrains)
                    If UBound(TimeTablePara.nPubTrains) = 1 Then
                        Call TMSDrawLineInPicture(TimeTablePara.nPubTrains(i), rBmpGraphics, tmpPen, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX, TimeTablePara.TimeTableDiagramPara.nifPrintCheCi, TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                        ' If TrainInf(TimeTablePara.nPubTrains(i)).TrainStyle = "环形车" Then
                        Call DrawLinkCircleTrain(TimeTablePara.nPubTrains(i), rBmpGraphics, New Pen(Color.Blue, 2), TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX, TimeTablePara.TimeTableDiagramPara.nifPrintCheCi, TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                        'Call DrawLinkCircleTrain(TimeTablePara.nPubTrains(i), rBmpGraphics, New Pen(Color.Red, 2), TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX, TimeTablePara.TimeTableDiagramPara.nifPrintCheCi, TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                        'End If
                    Else
                        Call TMSDrawLineInPicture(TimeTablePara.nPubTrains(i), rBmpGraphics, tmpPen, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX, TimeTablePara.TimeTableDiagramPara.nifPrintCheCi, TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                    End If
                Next
            End If
            If nIFShowAllCheDiTrain = 1 And TimeTablePara.nPubCheDi > 0 And TimeTablePara.nPubCheDi <= UBound(ChediInfo) Then
                For i = 1 To UBound(ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain)
                    Call TMSDrawLineInPicture(ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain(i), rBmpGraphics, tmpPen, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX, TimeTablePara.TimeTableDiagramPara.nifPrintCheCi, TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                Next
            End If
        End If
        If SplitDiagram.Panel2.AutoScrollPosition.Y < 0 Then
            'Me.picStation.Height = Me.PicDiagram.Height
            'Me.PicStation2.Height = Me.PicDiagram.Height
            Me.picStation.Top = SplitDiagram.Panel2.AutoScrollPosition.Y
            Me.PicStation2.Top = SplitDiagram.Panel2.AutoScrollPosition.Y
        End If
    End Sub

    Private Sub 列车信息ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 列车信息ToolStripMenuItem.Click
        If TimeTablePara.nPubTrain > 0 Then
            Dim nf As New frmTrainInfor
            nf.ShowDialog()
        End If
    End Sub

    Private Sub 增加列车AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nf As New frmDrawSingleTrain
        nf.sCurFormState = "新增列车"
        nf.ShowDialog()
        If nf.IfNotDrawLine = 1 Then
            Call addOneUndoInf()
            Call RefreshDiagram(1)
        End If
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 增加列车ToolStripMenuItem1.Click
        Call 增加列车AToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub PicDiagram_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PicDiagram.MouseMove
        ' Dim sNowTime As String
        Dim i As Integer
        'Dim tmpX, tmpY As Single
        Dim tmpGraphics As Graphics '画线路与车站图的定义的对象
        'nCurSecID = GetSecIDFormYcord(e.Y, TimeTablePara.nPubTrain)
        nCurStaID = GetStaIDFormYcord(e.Y, TimeTablePara.nPubTrain)
        If nCurStaID > 0 Then
            Me.labTime.Text = "当前车站: " & StationInf(nCurStaID).sPrintStaName
        End If
        ' sNowTime = GetMouseMoveTime(e.X, Me.PicDiagram.Width - TimeTablePara.TimeTableDiagramPara.sngLeftBlank * 2 - TimeTablePara.TimeTableDiagramPara.sngStaBlank)
        'Me.labTime.Text = "当前时刻: " & SecondToHour(sNowTime, 0)

        If TimeTablePara.sCurDiagramState = DiagramState.运行图 Then
            nSeekLinkTrain = 0

            '显示当头前鼠标指的车站
            'Me.PicDiagram.Refresh()
            'If nCurStaID > 0 Then
            '    tmpGraphics = Me.PicDiagram.CreateGraphics()
            '    tmpX = e.X
            '    tmpY = StationInf(nCurStaID).YPicValue
            '    tmpGraphics.DrawEllipse(Pens.Blue, tmpX - 8, tmpY - 8, 16, 16)
            'End If


            Select Case nPubAdjustTrainLineState

                Case 0

                Case 1 '调整发点
                    If TimeTablePara.nPubTrain > 0 Then
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
                    If TimeTablePara.nPubTrain > 0 Then
                        If e.Button = Windows.Forms.MouseButtons.Left And Me.PicDiagram.Cursor = Cursors.Cross Then
                            If intCurMoveTimeID < UBound(MoveTimeXY) Then
                                Me.PicDiagram.Refresh()
                                tmpGraphics = Me.PicDiagram.CreateGraphics()
                                tmpGraphics.DrawLine(Pens.Blue, e.X, MoveTimeXY(intCurMoveTimeID).Y, MoveTimeXY(intCurMoveTimeID + 1).X2, MoveTimeXY(intCurMoveTimeID + 1).Y2)
                            End If
                        End If
                    End If
                Case 4 ''调整始发站交路
                    If TimeTablePara.nPubTrain > 0 Then
                        If (Math.Abs(e.X - sngPriMoveX1) <= 8 And Math.Abs(e.Y - sngPriMoveY1) <= 8) Then
                            Me.PicDiagram.Cursor = Cursors.Cross
                        Else
                            Me.PicDiagram.Cursor = Cursors.Default
                        End If
                    End If
                Case 5 '调整终到站交路
                    If TimeTablePara.nPubTrain > 0 Then
                        If (Math.Abs(e.X - sngPriMoveX2) <= 8 And Math.Abs(e.Y - sngPriMoveY2) <= 8) Then
                            Me.PicDiagram.Cursor = Cursors.Cross
                        Else
                            Me.PicDiagram.Cursor = Cursors.Default
                        End If
                    End If
                Case 6 '调整交路，两头都可调整
                    If TimeTablePara.nPubTrain > 0 Then
                        If (Math.Abs(e.X - sngPriMoveX1) <= 8 And Math.Abs(e.Y - sngPriMoveY1) <= 8) Or (Math.Abs(e.X - sngPriMoveX2) <= 8 And Math.Abs(e.Y - sngPriMoveY2) <= 8) Then
                            Me.PicDiagram.Cursor = Cursors.Cross
                        Else
                            Me.PicDiagram.Cursor = Cursors.Default
                        End If
                    End If

                Case 7 '调整始发站交路
                    If TimeTablePara.nPubTrain > 0 Then
                        If e.Button = Windows.Forms.MouseButtons.Left And Me.PicDiagram.Cursor = Cursors.Cross Then
                            Call SeekLinkTrain(e.X, 1)
                        End If
                    End If
                Case 8 '调整终到站交路
                    If TimeTablePara.nPubTrain > 0 Then
                        If e.Button = Windows.Forms.MouseButtons.Left And Me.PicDiagram.Cursor = Cursors.Cross Then
                            Call SeekLinkTrain(e.X, 2)
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
                        tmpGraphics.DrawString(sPrintText, New Font("黑体", 10), Brushes.Blue, SelectTime.SecX, SelectTime.SecY - 12)
                    End If

                Case 11 '平移列车

                    If e.Button = Windows.Forms.MouseButtons.Left Then '平移运行线
                        Dim MoveX As Single
                        MoveX = e.X - sngForX
                        Me.PicDiagram.Refresh()
                        Dim X1, Y1, X2, Y2, x3, y3, x4, y4 As Single
                        tmpGraphics = Me.PicDiagram.CreateGraphics()
                        If UBound(MoveTimeXY) > 1 Then
                            X1 = MoveTimeXY(1).X + MoveX
                            Y1 = MoveTimeXY(1).Y
                            X2 = MoveTimeXY(1).X2 + MoveX
                            Y2 = MoveTimeXY(1).Y2
                            For i = 2 To UBound(MoveTimeXY)
                                x4 = MoveTimeXY(i).X2 + MoveX
                                y4 = MoveTimeXY(i).Y2
                                If X1 > 0 And x4 > 0 And Y1 > 0 And y4 > 0 Then
                                    tmpGraphics.DrawLine(Pens.Blue, X1, Y1, x4, y4)
                                End If

                                x3 = MoveTimeXY(i).X + MoveX
                                y3 = MoveTimeXY(i).Y
                                If x4 > 0 And x3 > 0 And y4 > 0 And y3 > 0 Then
                                    tmpGraphics.DrawLine(Pens.Blue, x4, y4, x3, y3)
                                End If

                                X1 = x3
                                Y1 = y3
                            Next i
                        End If
                    End If
                Case 12 '多选列车
                    If e.Button = Windows.Forms.MouseButtons.Left Then
                        If SelectTime.X1 > 0 And SelectTime.Y1 > 0 Then
                            SelectTime.X2 = e.X
                            SelectTime.Y2 = e.Y
                            Me.PicDiagram.Refresh()
                            tmpGraphics = Me.PicDiagram.CreateGraphics()
                            tmpGraphics.DrawRectangle(Pens.Blue, SelectTime.X1, SelectTime.Y1, SelectTime.X2 - SelectTime.X1, SelectTime.Y2 - SelectTime.Y1)
                        End If
                    End If
            End Select

        ElseIf TimeTablePara.sCurDiagramState = DiagramState.技术图解 Then
            Select Case TimeTablePara.nStaJiShuTuJieSeletedState
                Case 1 '修改股道
                    If e.Button = Windows.Forms.MouseButtons.Left And Me.PicDiagram.Cursor = Cursors.SizeNS Then
                        Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
                        Me.PicDiagram.Refresh()
                        rBmpGraphics = Me.PicDiagram.CreateGraphics()
                        rBmpGraphics.DrawLine(New Pen(Color.SpringGreen, 3), GuDaoJishutujie.CurSelectedLineX1, e.Y, GuDaoJishutujie.CurSelectedLineX2, e.Y)
                    End If
            End Select
        End If
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
                        tmpX = FormTimeToXCord(.Starting(nCurSta), TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
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

                        tmpX = FormTimeToXCord(.Arrival(nCurSta), TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
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
    Private Sub SeekLinkTrain(ByVal CurX As Single, ByVal nStartOrEnd As Integer)
        Dim sgnRadius As Single
        Dim nTr As Integer
        Dim sTtime As Single
        sgnRadius = 120
        Dim CurX1, CurY1, CurX2, CurY2 As Single
        Dim sngSeekTrainX, sngSeekTrainY As Single
        sTtime = GetMouseMoveTime(CurX, Me.PicDiagram.Width - TimeTablePara.TimeTableDiagramPara.sngLeftBlank * 2 - TimeTablePara.TimeTableDiagramPara.sngStaBlank)
        Me.PicDiagram.Refresh()
        Dim tmpGraphics As Graphics '画线路与车站图的定义的对象
        tmpGraphics = Me.PicDiagram.CreateGraphics()
        'If TrainInf(TimeTablePara.npubtrain).TrainReturn(1) = 0 Then
        If nStartOrEnd = 1 Then '始发站
            If TrainInf(TimeTablePara.nPubTrain).TrainReturn(1) = 0 Then
                If TimeTablePara.nPubTrain Mod 2 <> 0 Then  '下行
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

                    nTr = SeekCDLinkTrain(TimeTablePara.nPubTrain, 1, sTtime)
                    If nTr <> 0 Then
                        sngSeekTrainX = GetTrainrStartXCoord(nTr, TrainInf(nTr).nPathID(UBound(TrainInf(nTr).nPathID)))
                        sngSeekTrainY = StationInf(TrainInf(nTr).nPathID(UBound(TrainInf(nTr).nPathID))).YPicValue
                        tmpGraphics.DrawEllipse(Pens.Blue, sngSeekTrainX - 8, sngSeekTrainY - 8, 16, 16)
                        nSeekLinkTrain = nTr
                    End If
                Else
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
                    nTr = SeekCDLinkTrain(TimeTablePara.nPubTrain, 3, sTtime)
                    If nTr <> 0 Then
                        sngSeekTrainX = GetTrainArriXCoord(nTr, TrainInf(nTr).nPathID(UBound(TrainInf(nTr).nPathID)))
                        sngSeekTrainY = StationInf(TrainInf(nTr).nPathID(UBound(TrainInf(nTr).nPathID))).YPicValue
                        tmpGraphics.DrawEllipse(Pens.Blue, sngSeekTrainX - 8, sngSeekTrainY - 8, 16, 16)
                        nSeekLinkTrain = nTr
                    End If
                End If
            End If
        Else
            If TrainInf(TimeTablePara.nPubTrain).TrainReturn(2) = 0 Then
                If TimeTablePara.nPubTrain Mod 2 <> 0 Then '下行
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
                    nTr = SeekCDLinkTrain(TimeTablePara.nPubTrain, 2, sTtime)
                    If nTr <> 0 Then
                        sngSeekTrainX = GetTrainrStartXCoord(nTr, TrainInf(nTr).nPathID(1))
                        sngSeekTrainY = StationInf(TrainInf(nTr).nPathID(1)).YPicValue
                        tmpGraphics.DrawEllipse(Pens.Blue, sngSeekTrainX - 8, sngSeekTrainY - 8, 16, 16)
                        nSeekLinkTrain = nTr
                    End If
                Else
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
                    nTr = SeekCDLinkTrain(TimeTablePara.nPubTrain, 4, sTtime)
                    If nTr <> 0 Then
                        sngSeekTrainX = GetTrainArriXCoord(nTr, TrainInf(nTr).nPathID(1))
                        sngSeekTrainY = StationInf(TrainInf(nTr).nPathID(1)).YPicValue
                        tmpGraphics.DrawEllipse(Pens.Blue, sngSeekTrainX - 8, sngSeekTrainY - 8, 16, 16)
                        nSeekLinkTrain = nTr
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub 删除列车DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除列车DToolStripMenuItem.Click
        Dim i As Integer
        If UBound(TimeTablePara.nPubTrains) > 0 Then
            If MsgBox("确定删除这些列车吗？", vbQuestion + vbYesNo + vbDefaultButton2, "确认操作") = vbYes Then
                For i = 1 To UBound(TimeTablePara.nPubTrains)
                    Call DeleteTrainFromTrainID(TimeTablePara.nPubTrains(i), False)
                Next
                If TimeTablePara.BifAutoBianCheCi = True Then
                    Call ResetPrintTrainString()
                End If
                Call addOneUndoInf()
                Call RefreshDiagram(1)
                ReDim TimeTablePara.nPubTrains(0)
                TimeTablePara.nPubTrain = 0
            End If
        End If

    End Sub

    Private Sub 调整发点RToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 调整发点RToolStripMenuItem.Click
        Dim i As Integer
        Dim tmpX, tmpY As Single
        ReDim MoveTimeXY(0)
        If TimeTablePara.nPubTrain > 0 Then
            nPubAdjustTrainLineState = 1 '调整发点

            Dim tmpGraphics As Graphics '画线路与车站图的定义的对象
            tmpGraphics = Me.PicDiagram.CreateGraphics()
            ReDim MoveTimeXY(UBound(TrainInf(TimeTablePara.nPubTrain).nPathID))
            For i = 1 To UBound(TrainInf(TimeTablePara.nPubTrain).nPathID)
                tmpX = GetTrainrStartXCoord(TimeTablePara.nPubTrain, TrainInf(TimeTablePara.nPubTrain).nPathID(i))
                tmpY = StationInf(TrainInf(TimeTablePara.nPubTrain).nPathID(i)).YPicValue
                MoveTimeXY(i).nSta = TrainInf(TimeTablePara.nPubTrain).nPathID(i)
                MoveTimeXY(i).X = tmpX
                MoveTimeXY(i).Y = tmpY
                tmpX = GetTrainrStartXCoord(TimeTablePara.nPubTrain, TrainInf(TimeTablePara.nPubTrain).nPathID(i))
                MoveTimeXY(i).X2 = tmpX
                MoveTimeXY(i).Y2 = tmpY
                tmpGraphics.DrawEllipse(Pens.Blue, tmpX - 8, tmpY - 8, 16, 16)
            Next
        End If

    End Sub

    Private Sub PicDiagram_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PicDiagram.MouseUp
        Dim i As Integer
        If TimeTablePara.sCurDiagramState = DiagramState.运行图 Then
            Dim cdID As Integer
            Dim CDid2 As Integer

            If e.Button = Windows.Forms.MouseButtons.Left Then
                Select Case nPubAdjustTrainLineState
                    Case 2 ''开始调整
                        If TimeTablePara.nPubTrain > 0 Then
                            If Me.PicDiagram.Cursor = Cursors.Cross Then
                                nPubAdjustTrainLineState = 0
                                Dim nf As New frmStartTime
                                nf.nPriCurTrain = TimeTablePara.nPubTrain
                                nf.intCurMoveTimeSta = intCurMoveTimeStaID
                                nf.lngPriStartTime = GetMouseMoveTime(e.X, Me.PicDiagram.Width - TimeTablePara.TimeTableDiagramPara.sngLeftBlank * 2 - TimeTablePara.TimeTableDiagramPara.sngStaBlank) ' GetTrainArriOrStartTime(TimeTablePara.npubtrain, 0, 1)
                                nf.ShowDialog()
                                If nf.IfNotEdit = 1 Then
                                    Call addOneUndoInf()
                                    Call RefreshDiagram(1)
                                Else
                                    Me.PicDiagram.Refresh()
                                    Call Me.PicDiagram_Paint(Nothing, Nothing)
                                End If
                                Me.PicDiagram.Cursor = Cursors.Default
                            Else
                                nPubAdjustTrainLineState = 0
                                Me.PicDiagram.Refresh()
                                Call Me.PicDiagram_Paint(Nothing, Nothing)
                            End If

                        End If
                    Case 7 '调整始发站或终到站交路

                        If TimeTablePara.nPubTrain > 0 Then
                            nPubAdjustTrainLineState = 0
                            Me.PicDiagram.Cursor = Cursors.Default

                            If nSeekLinkTrain <> 0 Then

                                cdID = CheCiToCheDiID(TimeTablePara.nPubTrain)
                                If cdID <> 0 Then
                                    CDid2 = CheCiToCheDiID(nSeekLinkTrain)
                                    If CDid2 > 0 Then
                                        Call HeBinCheDiInfo(cdID, CDid2)
                                    End If
                                    If TimeTablePara.BifAutoBianCheCi = True Then
                                        Call ResetPrintTrainString()
                                    End If
                                    TrainInf(nSeekLinkTrain).nCheDiPuOrNot = 1
                                    Call addOneUndoInf()
                                    Call RefreshDiagram(1)
                                End If
                            End If
                            Me.PicDiagram.Refresh()
                            Call Me.PicDiagram_Paint(Nothing, Nothing)
                        End If

                    Case 8 '调整始发站或终到站交路
                        If TimeTablePara.nPubTrain > 0 Then
                            nPubAdjustTrainLineState = 0
                            Me.PicDiagram.Cursor = Cursors.Default
                            If nSeekLinkTrain <> 0 Then
                                cdID = CheCiToCheDiID(TimeTablePara.nPubTrain)
                                If cdID <> 0 Then
                                    CDid2 = CheCiToCheDiID(nSeekLinkTrain)
                                    Call HeBinCheDiInfo(cdID, CDid2)
                                    If TimeTablePara.BifAutoBianCheCi = True Then
                                        Call ResetPrintTrainString()
                                    End If
                                    TrainInf(nSeekLinkTrain).nCheDiPuOrNot = 1
                                    Call addOneUndoInf()
                                    Call RefreshDiagram(1)
                                End If
                            End If
                            Me.PicDiagram.Refresh()
                            Call Me.PicDiagram_Paint(Nothing, Nothing)
                        End If

                    Case 9 '测量时间第一点
                        nPubAdjustTrainLineState = 10

                    Case 10 '测量时间第二点
                        'MsgBox(SecondToMinute(TimeMinus(SelectTime.intCurSelectSecTime, SelectTime.intCurSelectFirTime)))
                        Me.PicDiagram.Refresh()
                        Me.测量时间ToolStripButton9.Checked = False
                        nPubAdjustTrainLineState = 0
                        Call PicDiagram_Paint(Nothing, Nothing)

                    Case 11 '平移列车
                        If TimeTablePara.nPubTrain > 0 Then
                            Dim nMoveTime As Long
                            Dim nForTime As Long
                            Dim nAfterTime As Long
                            nAfterTime = GetMouseMoveTime(MoveTimeXY(1).X - (e.X - sngForX), Me.PicDiagram.Width - TimeTablePara.TimeTableDiagramPara.sngLeftBlank * 2 - TimeTablePara.TimeTableDiagramPara.sngStaBlank)
                            nForTime = GetMouseMoveTime(MoveTimeXY(1).X, Me.PicDiagram.Width - TimeTablePara.TimeTableDiagramPara.sngLeftBlank * 2 - TimeTablePara.TimeTableDiagramPara.sngStaBlank)
                            nMoveTime = nForTime - nAfterTime
                            If nMoveTime <> 0 Then

                                For i = 1 To UBound(TrainInf(TimeTablePara.nPubTrain).Arrival)
                                    TrainInf(TimeTablePara.nPubTrain).Arrival(i) = TimeAdd(TrainInf(TimeTablePara.nPubTrain).Arrival(i), nMoveTime)
                                    TrainInf(TimeTablePara.nPubTrain).Starting(i) = TimeAdd(TrainInf(TimeTablePara.nPubTrain).Starting(i), nMoveTime)
                                Next

                                TrainInf(TimeTablePara.nPubTrain).lAllStartTime = TrainInf(TimeTablePara.nPubTrain).Starting(TrainInf(TimeTablePara.nPubTrain).nPathID(1))
                                TrainInf(TimeTablePara.nPubTrain).lAllEndTime = TrainInf(TimeTablePara.nPubTrain).Arrival(TrainInf(TimeTablePara.nPubTrain).nPathID(UBound(TrainInf(TimeTablePara.nPubTrain).nPathID)))

                                TrainInf(TimeTablePara.nPubTrain).sStartZFArrival = -1
                                TrainInf(TimeTablePara.nPubTrain).sStartZFStarting = -1
                                TrainInf(TimeTablePara.nPubTrain).sEndZFArrival = -1
                                TrainInf(TimeTablePara.nPubTrain).sEndZFStarting = -1

                                If TimeTablePara.BifAutoBianCheCi = True Then
                                    Call ResetPrintTrainString()
                                End If
                                Call addOneUndoInf()
                                Call RefreshDiagram(1)
                            End If
                            nPubAdjustTrainLineState = 0
                            Call PicDiagram_Paint(Nothing, Nothing)
                        End If
                    Case 12 '多选择
                        Call calMultiSelectTrain() '多选
                        Me.PicDiagram.Refresh()
                        Me.多选ToolStripButton1.Checked = False
                        Call PicDiagram_Paint(Nothing, Nothing)
                        nPubAdjustTrainLineState = 0
                    Case Else
                        Me.PicDiagram.Cursor = Cursors.Default
                        Me.PicDiagram.Refresh()
                        Call Me.PicDiagram_Paint(Nothing, Nothing)
                        nPubAdjustTrainLineState = 0
                End Select
            End If

        ElseIf TimeTablePara.sCurDiagramState = DiagramState.技术图解 Then
            Dim nConfictTrains As New Generic.List(Of Integer)
            Dim sConficString As New Generic.List(Of String)

            Select Case TimeTablePara.nStaJiShuTuJieSeletedState
                Case 1 '修改股道
                    If Me.PicDiagram.Cursor = Cursors.SizeNS Then
                        Dim nGuDaoID As String
                        Dim sGuDaoNum As String
                        Dim nID As Integer
                        Dim nSta As Integer
                        Dim notherTrain As Integer
                        nSta = StaNameToStaInfID(Me.cmbJiShuTuJieSta.Text.Trim)
                        If nSta > 0 Then
                            nGuDaoID = GetCurGuDaoBianHaoFromYCoord(e.Y)
                            sGuDaoNum = GuDaoJishutujie.sGuDao(nGuDaoID)
                            nID = FromSGudaoNumtoGuDaoID(sGuDaoNum, nSta)
                            Dim nOupyTrain As Integer
                            nOupyTrain = 0
                            Dim nAnoTrain As Integer
                            nAnoTrain = GetAnoTrain(TimeTablePara.nPubTrain, nSta)
                            Dim Time1 As Long
                            Dim Time2 As Long
                            If sGuDaoNum <> "" Then
                                ReDim nGuDaoTrain(0)
                                If GuDaoJishutujie.sCurSeleteState = "车站股道" Then

                                    Time1 = TrainInf(TimeTablePara.nPubTrain).Arrival(nSta)
                                    Time2 = TrainInf(TimeTablePara.nPubTrain).Starting(nSta)
                                    'End If
                                    nOupyTrain = CurGudaoIfOcupy(nSta, sGuDaoNum, Time1, Time2, TimeTablePara.nPubTrain, nAnoTrain)
                                    If nOupyTrain > 0 Then
                                        MsgBox("该股道已经被列车" & TrainInf(nOupyTrain).Train & "占用，请选择别的股道", , "提示")
                                        Me.PicDiagram.Refresh()
                                    Else
                                        Call EditStaGuDao(TimeTablePara.nPubTrain, sGuDaoNum, StationInf(nSta).sStationName)
                                        Call addOneUndoInf()
                                        Call cmbJiShuTuJieSta_SelectedValueChanged(Nothing, Nothing)
                                    End If
                                ElseIf GuDaoJishutujie.sCurSeleteState = "始发折返" Then
                                    Time1 = TrainInf(TimeTablePara.nPubTrain).sStartZFArrival
                                    Time2 = TrainInf(TimeTablePara.nPubTrain).sStartZFStarting
                                    '加一能否进折返线的判断
                                    If IfReturnTrackOcupyConflict(TimeTablePara.nPubTrain, nConfictTrains, sConficString, StationInf(nSta).sStationName, sGuDaoNum, Time1, Time2) = True Then
                                        MsgBox("该股道已经被列车" & TrainInf(nConfictTrains.Item(0)).sPrintTrain & "占用，请选择别的股道", , "提示")
                                        Me.PicDiagram.Refresh()

                                    End If

                                    nOupyTrain = CurGudaoIfOcupy(nSta, sGuDaoNum, Time1, Time2, TimeTablePara.nPubTrain, nAnoTrain)
                                    If nOupyTrain > 0 Then
                                        MsgBox("该股道已经被列车" & TrainInf(nOupyTrain).Train & "占用，请选择别的股道", , "提示")
                                        Me.PicDiagram.Refresh()
                                    Else

                                        If nConfictTrains.Count > 0 Then
                                            '    MsgBox("该股道已经被列车" & TrainInf(nOupyTrain).Train & "占用，请选择别的股道", , "提示")
                                            '    Me.PicDiagram.Refresh()
                                        Else
                                            TrainInf(TimeTablePara.nPubTrain).sStartZFLine = sGuDaoNum
                                            notherTrain = TrainInf(TimeTablePara.nPubTrain).TrainReturn(1)
                                            If notherTrain > 0 Then
                                                TrainInf(notherTrain).sEndZFLine = sGuDaoNum
                                                TrainInf(notherTrain).sStartZFStarting = Time1
                                                TrainInf(notherTrain).sStartZFArrival = Time2
                                            End If
                                            Call addOneUndoInf()
                                            Call cmbJiShuTuJieSta_SelectedValueChanged(Nothing, Nothing)
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        TimeTablePara.nStaJiShuTuJieSeletedState = 0
                        Me.PicDiagram.Cursor = Cursors.Default
                    End If
            End Select
        End If
    End Sub

    Private Sub calMultiSelectTrain() '多选择列车
        Dim i As Integer
        Dim X1, Y1, X2, Y2 As Integer
        ReDim TimeTablePara.nPubTrains(0)
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                X1 = FormTimeToXCord(TrainInf(i).Starting(TrainInf(i).nPathID(1)), TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                Y1 = StationInf(TrainInf(i).nPathID(1)).YPicValue
                X2 = FormTimeToXCord(TrainInf(i).Starting(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))), TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                Y2 = StationInf(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))).YPicValue
                If X1 >= SelectTime.X1 And X1 <= SelectTime.X2 And X2 >= SelectTime.X1 And X2 <= SelectTime.X2 And Y1 >= SelectTime.Y1 And Y1 <= SelectTime.Y2 And Y2 >= SelectTime.Y1 And Y2 <= SelectTime.Y2 Then
                    ReDim Preserve TimeTablePara.nPubTrains(UBound(TimeTablePara.nPubTrains) + 1)
                    TimeTablePara.nPubTrains(UBound(TimeTablePara.nPubTrains)) = i
                End If
            End If
        Next
        If UBound(TimeTablePara.nPubTrains) > 0 Then
            TimeTablePara.nPubTrain = TimeTablePara.nPubTrains(1)
        End If

        'If UBound(TimeTablePara.nPubTrains) > 0 Then
        '    Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
        '    Me.PicDiagram.Refresh()
        '    rBmpGraphics = Me.PicDiagram.CreateGraphics()
        '    Dim tmpPen As Pen
        '    tmpPen = New Pen(Color.SpringGreen, 2)
        '    For i = 1 To UBound(TimeTablePara.nPubTrains)
        '        Call DrawLineInPicture(TimeTablePara.nPubTrains(i), rBmpGraphics, tmpPen, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX, TimeTablePara.TimeTableDiagramPara.nifPrintCheCi, TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
        '    Next
        'End If

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
    Private Sub 修改标尺EToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改标尺EToolStripMenuItem.Click
        Dim nf As New frmDrawSingleTrain
        nf.sCurFormState = "修改标尺"
        nf.nBeforTrainNums = TimeTablePara.nPubTrains
        nf.ShowDialog()
        If nf.IfNotDrawLine = 1 Then
            Call addOneUndoInf()
            Call RefreshDiagram(1)
        End If
    End Sub

    Private Sub 修改交路JToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改交路JToolStripMenuItem.Click
        Dim i As Integer
        Dim nf As New frmInputBox
        If TimeTablePara.nPubTrain <> 0 Then
            nf.Text = "选择交路名"
            nf.labTitle.Text = "选择要修改的交路名:"
            nf.cmbText.Visible = True
            nf.txtText.Visible = False
            nf.cmbText.Items.Clear()
            If UBound(BasicTrainInf) > 0 Then
                If TimeTablePara.nPubTrain Mod 2 = 0 Then
                    For i = 1 To UBound(BasicTrainInf)
                        If BasicTrainInf(i).nUporDown = 2 Then
                            nf.cmbText.Items.Add(BasicTrainInf(i).sJiaoLuName)
                        End If
                    Next i
                Else
                    For i = 1 To UBound(BasicTrainInf)
                        If BasicTrainInf(i).nUporDown = 1 Then
                            nf.cmbText.Items.Add(BasicTrainInf(i).sJiaoLuName)
                        End If
                    Next i
                End If
            Else
                MsgBox("列车信息中没有交路信息，请检查列车信息！", , "提示")
                Exit Sub
            End If

            nf.cmbText.Text = TrainInf(TimeTablePara.nPubTrain).sJiaoLuName
            nf.ShowDialog()
            If StrInputBoxCombText <> "" And bCancelInput = 0 Then
                If TimeTablePara.nPubTrain Mod 2 = 0 Then
                    Call ResetLongTrain(TimeTablePara.nPubTrain, 2, StrInputBoxCombText)
                Else
                    Call ResetLongTrain(TimeTablePara.nPubTrain, 1, StrInputBoxCombText)
                End If
                If TimeTablePara.BifAutoBianCheCi = True Then
                    Call ResetPrintTrainString()
                End If
                Call addOneUndoInf()
                Call RefreshDiagram(1)
            End If
        End If

    End Sub

    Private Sub 调整至最小折返时间MToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 调整至最小折返时间MToolStripMenuItem.Click
        If TimeTablePara.nPubTrain <> 0 Then
            Call TZYXLineZheFanMin(TimeTablePara.nPubTrain)

            '    If TimeTablePara.npubtrain > 0 Then
            '        Call EditJiaoLuLine(CheCiToCheDiID(TimeTablePara.npubtrain))
            '    End If
        End If

    End Sub


    Private Sub 断开列车交路DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 断开列车交路DToolStripMenuItem.Click
        If TimeTablePara.nPubTrain <> 0 Then
            If TrainInf(TimeTablePara.nPubTrain).TrainReturn(1) = 0 Then
                MsgBox("该列车前面接续列车为空，该操作不能实现！", , "提示")
            Else
                If MsgBox("确定断开该列车的车底连接方式吗？", vbQuestion + vbYesNo + vbDefaultButton2, "确认操作") = vbYes Then
                    Dim Cdid As Integer

                    Cdid = CheCiToCheDiID(TimeTablePara.nPubTrain)
                    If Cdid <> 0 Then
                        Call DelectCheDiLink(TimeTablePara.nPubTrain, Cdid)
                        If TimeTablePara.BifAutoBianCheCi = True Then
                            Call ResetPrintTrainString()
                        End If
                    End If
                    TimeTablePara.nPubCheDi = 0
                    Call addOneUndoInf()
                    Call ShowChediJiaolu2()
                End If
            End If
        End If
    End Sub

    Private Sub 修改车次连接EToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改车次连接EToolStripMenuItem.Click
        If TimeTablePara.nPubTrain <> 0 Then
            'If nSelectedTrain Mod 2 <> 0 Then
            If TrainInf(TimeTablePara.nPubTrain).TrainReturn(1) = 0 Or TrainInf(TimeTablePara.nPubTrain).TrainReturn(2) = 0 Then

                Dim tmpGraphics As Graphics '画线路与车站图的定义的对象
                tmpGraphics = Me.PicDiagram.CreateGraphics()
                If TrainInf(TimeTablePara.nPubTrain).TrainReturn(1) = 0 Then
                    sngPriMoveX1 = GetTrainrStartXCoord(TimeTablePara.nPubTrain, TrainInf(TimeTablePara.nPubTrain).nPathID(1))
                    sngPriMoveY1 = StationInf(TrainInf(TimeTablePara.nPubTrain).nPathID(1)).YPicValue
                    tmpGraphics.DrawEllipse(Pens.Blue, sngPriMoveX1 - 8, sngPriMoveY1 - 8, 16, 16)
                    nPubAdjustTrainLineState = 4 '调整车底交路图
                End If

                If TrainInf(TimeTablePara.nPubTrain).TrainReturn(2) = 0 Then
                    sngPriMoveX2 = GetTrainArriXCoord(TimeTablePara.nPubTrain, TrainInf(TimeTablePara.nPubTrain).nPathID(UBound(TrainInf(TimeTablePara.nPubTrain).nPathID)))
                    sngPriMoveY2 = StationInf(TrainInf(TimeTablePara.nPubTrain).nPathID(UBound(TrainInf(TimeTablePara.nPubTrain).nPathID))).YPicValue
                    tmpGraphics.DrawEllipse(Pens.Blue, sngPriMoveX2 - 8, sngPriMoveY2 - 8, 16, 16)
                    If nPubAdjustTrainLineState = 4 Then
                        nPubAdjustTrainLineState = 6 '两头都能调整
                    Else
                        nPubAdjustTrainLineState = 5 '调整车底交路图
                    End If
                End If

            Else
                MsgBox("该列车已经被勾上，不能调整交路！", , "提示")
            End If
        End If
    End Sub

    Private Sub 显示车底所有列车SToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 显示车底所有列车SToolStripMenuItem.Click
        If TimeTablePara.nPubCheDi > 0 Then
            nIFShowAllCheDiTrain = 1
        End If
    End Sub

    Private Sub 车底信息CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 车底信息CToolStripMenuItem.Click
        If TimeTablePara.nPubCheDi > 0 Then
            Dim nf As New frmCheDiInformation
            nf.ShowDialog()
        End If
    End Sub


    Public Sub listTitle()
        'Dim sString As String
        'If g_strNetMainPath.Length > 40 Then
        '    sString = g_strNetMainPath.Substring(0, 10) & " ... " & g_strNetMainPath.Substring(g_strNetMainPath.Length - 30, 30)
        'Else
        '    sString = g_strNetMainPath
        'End If
        Me.Text = "运行图— [" & TimeTablePara.sPubCurSkbName & "]"
    End Sub


    Private Sub 编辑时刻表车站顺序EToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nf As New frmEditSKBStaSeq
        nf.ShowDialog()
    End Sub

    Private Sub 列车时刻表TToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 列车时刻表TToolStripMenuItem.Click
        Dim nf As New frmPrintTimeTable
        nf.ShowDialog()
    End Sub

    Private Sub 车底交路图CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 车底交路图CToolStripMenuItem.Click
        Dim nf As New frmPrintCheDiJiaoLu
        nf.ShowDialog()
    End Sub

    Private Sub 运行图DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 运行图DToolStripMenuItem.Click
        Dim nf As New frmPrintTrainDiagram
        nf.ShowDialog()
    End Sub

    Private Sub 修改停站信息OToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改停站信息OToolStripMenuItem.Click
        Dim nf As New frmEditTriainStopTime
        nf.ShowDialog()
    End Sub

    Private Sub 车站股道使用ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        TimeTablePara.sCurDiagramState = DiagramState.技术图解
        Me.cmbJiShuTuJieSta.Visible = True
        Dim i As Integer
        If Me.cmbJiShuTuJieSta.Items.Count = 0 Then
            cmbJiShuTuJieSta.Items.Clear()
            If UBound(NotSameStationInf) > 0 Then
                For i = 1 To UBound(NotSameStationInf)
                    Me.cmbJiShuTuJieSta.Items.Add(NotSameStationInf(i))
                Next i
                Me.cmbJiShuTuJieSta.Text = Me.cmbJiShuTuJieSta.Items(0)
            End If
        Else
            Call cmbJiShuTuJieSta_SelectedValueChanged(Nothing, Nothing)
        End If
    End Sub

    Private Sub cmbJiShuTuJieSta_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbJiShuTuJieSta.SelectedValueChanged
        TimeTablePara.StaDiagramePara.sCurStaName = Me.cmbJiShuTuJieSta.Text
        Call RefreshDiagram(1)
    End Sub



    Private Sub 列车重编车次BToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 列车重编车次BToolStripMenuItem.Click
        If GetUserName() = "北京地铁" Then
            Dim nf As New frmCheChiDim
            nf.Show()
        Else
            Call addOneUndoInf()
            Call ResetPrintTrainString()
            Call RefreshDiagram(1)
        End If
    End Sub


    Private Sub 保存运行图SToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CheckTimetableBadError() = False Then
            Exit Sub
        End If

        If TimeTablePara.sPubCurSkbName = "" Or TimeTablePara.sPubCurSkbName = "未命名时刻表" Then
            Dim nf As New frmTimeTableManager
            nf.ShowDialog()
        Else
            If MsgBox("确认要保存当前时刻表吗？", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "确认操作") = MsgBoxResult.Yes Then



                ' Call SaveSkbTimeTable(TimeTablePara.sPubCurSkbName, Me.ProBar)
                Dim sSKBId As String
                Dim nID As Integer
                nID = GetTimetableInfID(TimeTablePara.sPubCurSkbName)
                sSKBId = TimetableInf(nID).sID
                TimetableInf(nID).sEditDate = Now()
                Call SaveSkbTimeTableByDAO(sSKBId, Me.ProBar)
                Call SaveTimetableInf()
            End If
        End If
    End Sub

    Public Sub 检查列车运行图KToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call CheckDiagram()
    End Sub


    Private Sub 修改停站股道GToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改停站股道GToolStripMenuItem.Click
        Dim i As Integer
        Dim j As Integer
        ReDim StaStopLine(0)
        Dim ifIn As Integer
        For i = 1 To UBound(TrainInf(TimeTablePara.nPubTrain).nPathID)
            ifIn = 0
            For j = 1 To UBound(StaStopLine)
                If StaStopLine(j).sSta = StationInf(TrainInf(TimeTablePara.nPubTrain).nPathID(i)).sStationName Then
                    ifIn = 1
                    Exit For
                End If
            Next
            If ifIn = 0 Then '不同名
                ReDim Preserve StaStopLine(UBound(StaStopLine) + 1)
                StaStopLine(UBound(StaStopLine)).sSta = StationInf(TrainInf(TimeTablePara.nPubTrain).nPathID(i)).sStationName
                StaStopLine(UBound(StaStopLine)).sStopLine = TrainInf(TimeTablePara.nPubTrain).StopLine(TrainInf(TimeTablePara.nPubTrain).nPathID(i))
            End If
        Next
        ReDim Preserve StaStopLine(UBound(StaStopLine) + 1)
        StaStopLine(UBound(StaStopLine)).sSta = "始发折返股道"
        StaStopLine(UBound(StaStopLine)).sStopLine = TrainInf(TimeTablePara.nPubTrain).sStartZFLine

        ReDim Preserve StaStopLine(UBound(StaStopLine) + 1)
        StaStopLine(UBound(StaStopLine)).sSta = "终到折返股道"
        StaStopLine(UBound(StaStopLine)).sStopLine = TrainInf(TimeTablePara.nPubTrain).sEndZFLine

        ReDim Preserve StaStopLine(UBound(StaStopLine) + 1)
        StaStopLine(UBound(StaStopLine)).sSta = "始发开始折返时间"
        StaStopLine(UBound(StaStopLine)).sStopLine = dTime(TrainInf(TimeTablePara.nPubTrain).sStartZFArrival, 0)

        ReDim Preserve StaStopLine(UBound(StaStopLine) + 1)
        StaStopLine(UBound(StaStopLine)).sSta = "始发结束折返时间"
        StaStopLine(UBound(StaStopLine)).sStopLine = dTime(TrainInf(TimeTablePara.nPubTrain).sStartZFStarting, 0)

        ReDim stuListItem(UBound(StaStopLine))
        For i = 1 To UBound(StaStopLine)
            stuListItem(i).strItem = StaStopLine(i).sSta
            stuListItem(i).strStyle = PropStrStyle.TexBox
            stuListItem(i).strTxtList = StaStopLine(i).sStopLine
            stuListItem(i).strItemCriterion = TextCriterion.NotEmpty
        Next

        Dim nf As New frmEditDataProperity
        nf.ShowDialog()
        If nf.blnOK = True Then

            For i = 1 To UBound(StaStopLine)
                StaStopLine(i).sStopLine = stuListItem(i).strReturnValue
            Next

            For i = 1 To UBound(StationInf)
                For j = 1 To UBound(StaStopLine) - 2
                    If StationInf(i).sStationName = StaStopLine(j).sSta Then
                        TrainInf(TimeTablePara.nPubTrain).StopLine(i) = StaStopLine(j).sStopLine
                    End If
                Next
            Next
            TrainInf(TimeTablePara.nPubTrain).sStartZFLine = stuListItem(UBound(StaStopLine) - 3).strReturnValue
            TrainInf(TimeTablePara.nPubTrain).sEndZFLine = stuListItem(UBound(StaStopLine) - 2).strReturnValue
            Dim sStartTime As Long
            Dim sArriTime As Long
            sArriTime = HourToSecond(stuListItem(UBound(StaStopLine) - 1).strReturnValue)
            sStartTime = HourToSecond(stuListItem(UBound(StaStopLine)).strReturnValue)
            TrainInf(TimeTablePara.nPubTrain).sStartZFArrival = sArriTime
            TrainInf(TimeTablePara.nPubTrain).sStartZFStarting = sStartTime
            Dim naNotrain As Integer
            naNotrain = TrainInf(TimeTablePara.nPubTrain).TrainReturn(1)
            If naNotrain > 0 Then
                TrainInf(naNotrain).sEndZFArrival = sArriTime
                TrainInf(naNotrain).sEndZFStarting = sStartTime
                TrainInf(naNotrain).sEndZFLine = TrainInf(TimeTablePara.nPubTrain).sStartZFLine
            End If
            Call addOneUndoInf()
            Call RefreshDiagram(1)
        End If
    End Sub

    Private Sub ToolStripButton5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Call 约束铺画YToolStripMenuItem_Click(Nothing, Nothing)
        'If Me.约束铺画YToolStripMenuItem.Checked = True Then
        '    Me.约束铺画YToolStripMenuItem.Checked = False
        'Else
        '    Me.约束铺画YToolStripMenuItem.Checked = True
        'End If
    End Sub

    Private Sub ToolStripButton6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Call 自动重编车次ZToolStripMenuItem_Click(Nothing, Nothing)
        'If Me.自动重编车次ZToolStripMenuItem.Checked = True Then
        '    Me.自动重编车次ZToolStripMenuItem.Checked = False
        'Else
        '    Me.自动重编车次ZToolStripMenuItem.Checked = True
        'End If
    End Sub

    Private Sub 自动重编车次ZToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If Me.自动重编车次ZToolStripMenuItem.Checked = True Then
        '    Me.自动重编车次ZToolStripMenuItem.Checked = True
        '    TimeTablePara.BifAutoBianCheCi = True
        'Else
        '    Me.自动重编车次ZToolStripMenuItem.Checked = False
        '    TimeTablePara.BifAutoBianCheCi = False
        'End If
    End Sub

    Private Sub 约束铺画YToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If Me.约束铺画YToolStripMenuItem.Checked = True Then
        '    Me.约束铺画YToolStripMenuItem.Checked = True
        '    TimeTablePara.sPubTrainStrainDraw = TrainStrainDraw.有约束
        'Else
        '    Me.约束铺画YToolStripMenuItem.Checked = False
        '    TimeTablePara.sPubTrainStrainDraw = TrainStrainDraw.无约束
        'End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 打开运行图ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 保存运行图SToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 检查列车运行图KToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 运行图ToolStripButton3.Click
        Call 刷新运行图ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 车站股道使用ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 删除DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 删除列车DToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 撤销UToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TimeTablePara.nPubTrain = 0
        ReDim TimeTablePara.nPubTrains(0)
        If UndoSeq.nRedoID = 0 Then
            UndoSeq.nRedoID = UndoSeq.nCurUndoID
        End If
        Call UndoTraininf(1)
        Call UndoAndRedoMenuSet()
    End Sub

    Private Sub 重复RToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TimeTablePara.nPubTrain = 0
        ReDim TimeTablePara.nPubTrains(0)
        Call UndoTraininf(0)
        Call UndoAndRedoMenuSet()
    End Sub

    Private Sub tolStripUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 撤销UToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Public Sub UndoAndRedoMenuSet()
        If UndoSeq.nUpID > 0 Then
            Me.撤销UToolStripMenuItem.Enabled = True
            Me.撤销tolStripUndo.Enabled = True
        Else
            Me.撤销UToolStripMenuItem.Enabled = False
            Me.撤销tolStripUndo.Enabled = False
        End If

        If UndoSeq.nStep = 0 Then
            Me.重复RToolStripMenuItem.Enabled = False
            Me.重复tolStripRedo.Enabled = False
        Else
            Me.重复RToolStripMenuItem.Enabled = False
            Me.重复tolStripRedo.Enabled = False
        End If
        If UndoSeq.nDownID > 0 Then
            If UndoSeq.nStep = 0 Then
                Me.重复RToolStripMenuItem.Enabled = False
                Me.重复tolStripRedo.Enabled = False
            Else
                Me.重复RToolStripMenuItem.Enabled = True
                Me.重复tolStripRedo.Enabled = True
            End If
        Else
            Me.重复RToolStripMenuItem.Enabled = False
            Me.重复tolStripRedo.Enabled = False
        End If

    End Sub

    Private Sub tolStripRedo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 重复RToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 新建运行图NToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("该操作将会清空所有运行图信息，确认吗？建议你对以前的时刻表进行保存，然后运行该操作！", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel + vbDefaultButton2, "确认操作") = vbOK Then
            'Me.约束铺画YToolStripMenuItem.Checked = False
            Call InitSystemVariant(1) '窗体变量初始化
            Call IniteDiagramPicViraient("列车运行图") '底图变量
            TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = Me.PicDiagram.Width
            TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = Me.PicDiagram.Height
            Call RefreshDiagram(1)
            Call listTitle()
            Call addOneUndoInf()
        End If
    End Sub

    Private Sub ToolStripButton5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 新建运行图NToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 车站股道技术图解JToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nf As New frmPrintStaJiShuTuJie
        nf.ShowDialog()
    End Sub

    Private Sub 修改停站股道_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改停站股道.Click
        TimeTablePara.nStaJiShuTuJieSeletedState = 1 '修改股道
        Me.PicDiagram.Cursor = Cursors.SizeNS
    End Sub

    Private Sub 查找FToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim nf As New frmInputBox
        nf.Text = "查找列车"
        nf.labTitle.Text = "选择或输入列车车次:"
        nf.cmbText.Visible = True
        nf.txtText.Visible = False
        nf.cmbText.Items.Clear()
        If UBound(TrainInf) > 0 Then
            For i = 1 To UBound(TrainInf)
                If TrainInf(i).Train <> "" Then
                    nf.cmbText.Items.Add(TrainInf(i).sPrintTrain)
                End If
            Next i
        End If
        If nf.cmbText.Items.Count > 0 Then
            nf.cmbText.Text = nf.cmbText.Items(0)
        End If

        nf.ShowDialog()
        If StrInputBoxCombText <> "" And bCancelInput = 0 Then
            TimeTablePara.nPubTrain = FormPrintCheCiToTrainID(StrInputBoxCombText)
            If TimeTablePara.nPubTrain > 0 Then
                TimeTablePara.nPubCheDi = CheCiToCheDiID(TimeTablePara.nPubTrain)
                Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
                Me.PicDiagram.Refresh()
                rBmpGraphics = Me.PicDiagram.CreateGraphics()
                Dim tmpPen As Pen
                tmpPen = New Pen(Color.SpringGreen, 2)
                Call TMSDrawLineInPicture(TimeTablePara.nPubTrain, rBmpGraphics, tmpPen, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX, TimeTablePara.TimeTableDiagramPara.nifPrintCheCi, TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                Call ShowLabInfor(TimeTablePara.nPubTrain, Me.labInfor)
                Call listTrainInMiddlePic(TimeTablePara.nPubTrain)
                Call CopyAndPasteMnuSet()
                Call UndoAndRedoMenuSet()
                nIFShowAllCheDiTrain = 0
            Else
                MsgBox("没有找到当前车次!", , "提示")
                Me.PicDiagram.Refresh()
            End If
        End If
    End Sub

    Private Sub 计算指标XToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 计算指标XToolStripMenuItem.Click
        Dim nf As New frmODSDiagramIndex
        nf.Show()
    End Sub

    Private Sub 系统设置SToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nf As New frmTimeTablePara
        nf.sCurParaState = "运行图系统参数"
        nf.ShowDialog()
    End Sub

    Private Sub 纵向放大底图ZToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 纵向放大底图ZToolStripMenuItem.Click
        If TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight > 1500 Then
            TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = 1500
        Else
            TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight + 100
        End If
        Me.PicDiagram.Width = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
        Me.PicDiagram.Height = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        Me.picStation.Height = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        Me.PicStation2.Height = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        Call frmMain_Resize(Nothing, Nothing)
        Call RefreshDiagram(0)
    End Sub

    Private Sub 纵向缩小底图XToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 纵向缩小底图XToolStripMenuItem.Click
        If TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight <= 300 Then
            TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = 300
        Else
            TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight - 100
        End If
        Me.PicDiagram.Width = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
        Me.PicDiagram.Height = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        Me.picStation.Height = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        Me.PicStation2.Height = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
        Call frmMain_Resize(Nothing, Nothing)
        Call RefreshDiagram(0)
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

    Private Sub 测量时间ToolStripButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 测量时间ToolStripButton9.Click
        If Me.测量时间ToolStripButton9.Checked = True Then
            Me.测量时间ToolStripButton9.Checked = False
            nPubAdjustTrainLineState = 0
        Else
            Me.测量时间ToolStripButton9.Checked = True
            SelectTime.FirX = 0
            SelectTime.FirY = 0
            SelectTime.SecX = 0
            SelectTime.SecY = 0
            SelectTime.intCurSelectFirTime = -1
            SelectTime.intCurSelectSecTime = -1
            nPubAdjustTrainLineState = 9
        End If
    End Sub


    Private Sub 删除车底所有列车DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除车底所有列车DToolStripMenuItem.Click
        If MsgBox("该操作将删除该车底连接的所有列车，确定吗？", vbQuestion + vbYesNo + vbDefaultButton2, "确定操作") = vbNo Then Exit Sub

        Dim i As Integer
        For i = 1 To UBound(ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain)
            TrainInf(ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain(i)).nCheDiPuOrNot = 0
            TrainInf(ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain(i)).nIfFixedCheDi = 0
            DeleteLinetime(ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain(i))
        Next i

        '先将列车信息的连接车次清空
        ReDim ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain(0)

        Call addOneUndoInf()
        Call RefreshDiagram(1)
    End Sub

    Private Sub 修改车底编号EToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改车底编号EToolStripMenuItem.Click
        Dim i As Integer
        If TimeTablePara.nPubCheDi > 0 Then
sBE:
            Dim nf As New frmInputBox
            nf.txtText.Visible = False
            nf.cmbText.Visible = True
            nf.Text = "修改输出车底编号"
            nf.labTitle.Text = "请选择要修改的车底编号："
            nf.cmbText.Items.Clear()


            nf.cmbText.Text = ChediInfo(TimeTablePara.nPubCheDi).sCheCiHao
            nf.ShowDialog()
            If bCancelInput = 0 Then
                If StrInputBoxCombText <> "" And bCancelInput = 0 Then
                    For i = 1 To UBound(ChediInfo)
                        If ChediInfo(i).sCheCiHao = StrInputBoxCombText Then
                            If MsgBox("该车底编号已经存在，建议你不要定义相同的编号,是否继续执行该操作？", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "确认操作") = MsgBoxResult.No Then
                                GoTo sBE
                            Else
                                Exit For
                            End If
                        End If
                    Next
                    ChediInfo(TimeTablePara.nPubCheDi).sCheCiHao = StrInputBoxCombText
                    If GetUserName() = "北京地铁" Then
                        '    For i = 1 To UBound(ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain)
                        '        TrainInf(ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain(i)).sPrintTrain = StrInputBoxCombText & Microsoft.VisualBasic.Right(TrainInf(ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain(i)).sPrintTrain, 4)
                        '    Next i
                    Else
                        'ChediInfo(TimeTablePara.nPubCheDi).sCheCiHao = StrInputBoxCombText
                        For i = 1 To UBound(ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain)
                            TrainInf(ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain(i)).sPrintTrain = StrInputBoxCombText & Microsoft.VisualBasic.Right(TrainInf(ChediInfo(TimeTablePara.nPubCheDi).nLinkTrain(i)).sPrintTrain, 2)
                        Next i
                    End If
                    Call addOneUndoInf()
                    Call RefreshDiagram(1)
                Else
                    If MsgBox("输入为空，是否继续操作!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "确认操作") = MsgBoxResult.Ok Then
                        ChediInfo(TimeTablePara.nPubCheDi).sCheCiHao = StrInputBoxCombText
                        Call addOneUndoInf()
                        Call RefreshDiagram(1)
                    Else
                        GoTo sBE
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub 合并车底HToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 合并车底HToolStripMenuItem.Click
        Dim i As Integer
        Dim Cdid2 As Integer
        Dim nTrain1 As Integer
        Dim nTrain2 As Integer
        Dim sTime1 As Long
        Dim sTime2 As Long
        Dim nShowedChediNo As Integer
        nShowedChediNo = TimeTablePara.nPubCheDi
        Dim nf As New frmInputBox
        nf.cmbText.Visible = True
        nf.txtText.Visible = False

        For i = 1 To UBound(ChediInfo)
            If i <> nShowedChediNo Then
                nf.cmbText.Items.Add(ChediInfo(i).sCheDiID)
            End If
        Next i
        nf.labTitle.Text = "请选择要合并车底的ID:"
        nf.cmbText.Text = ""

        nf.ShowDialog()
        If StrInputBoxCombText <> "" And bCancelInput = 0 Then
            For i = 1 To UBound(ChediInfo)
                If StrInputBoxCombText = ChediInfo(i).sCheDiID Then
                    Cdid2 = i
                    Exit For
                End If
            Next i
            If Cdid2 > 0 Then
                nTrain1 = ChediInfo(nShowedChediNo).nLinkTrain(UBound(ChediInfo(nShowedChediNo).nLinkTrain))
                nTrain2 = ChediInfo(Cdid2).nLinkTrain(1)
                If (nTrain1 + nTrain2) Mod 2 = 0 Then
                    If MsgBox("第一车底的最后一列车与选择车底的第一列车同方向，是否继续操作？", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "确定") = MsgBoxResult.Ok Then
                        Call HeBinCheDiInfo(nShowedChediNo, Cdid2)
                        If TimeTablePara.BifAutoBianCheCi = True Then
                            Call ResetPrintTrainString()
                        End If
                        Call addOneUndoInf()
                        Call RefreshDiagram(1)
                    Else
                        Exit Sub
                    End If
                Else
                    sTime1 = AddLitterTime(TrainInf(nTrain1).Arrival(TrainInf(nTrain1).nPathID(UBound(TrainInf(nTrain1).nPathID))))
                    sTime2 = AddLitterTime(TrainInf(nTrain2).Starting(TrainInf(nTrain2).nPathID(1)))
                    If sTime2 < sTime1 Then
                        MsgBox("第一车底的最后一列车与选择的第一列车时间不符合要求，不能合并！", , "提示")
                        Exit Sub
                    Else
                        Call HeBinCheDiInfo(nShowedChediNo, Cdid2)
                        If TimeTablePara.BifAutoBianCheCi = True Then
                            Call ResetPrintTrainString()
                        End If
                        Call addOneUndoInf()
                        Call RefreshDiagram(1)
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub 运行图管理MToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nf As New frmTimeTableManager
        nf.ShowDialog()
        Call listTitle()
    End Sub

    Private Sub 剪切CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If TimeTablePara.nPubTrain > 0 Then
            Call 复制CToolStripMenuItem_Click(Nothing, Nothing)
            Call 删除列车DToolStripMenuItem_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub 复制CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        If UBound(TimeTablePara.nPubTrains) > 0 Then
            ReDim CopyTrainInf(UBound(TimeTablePara.nPubTrains))
            Call InputCopyChedinInf()
            For i = 1 To UBound(TimeTablePara.nPubTrains)
                Call CopyTrainInformationToCopyTrainInf(TimeTablePara.nPubTrains(i), i)
            Next
            Call CopyAndPasteMnuSet()
        End If
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
                For k = 1 To UBound(TimeTablePara.nPubTrains)
                    If ChediInfo(i).nLinkTrain(j) = TimeTablePara.nPubTrains(k) Then
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

    Private Sub 粘贴VToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nNewTrain As Integer
        Dim nMoveTime As Long
        Dim i As Integer
        Dim k As Integer
        '   Dim sngRnd As Single
        Dim j As Integer
        If UBound(CopyTrainInf) > 0 Then
            Dim nf As New frmInputBox
            nf.cmbText.Visible = False
            nf.txtText.Visible = True
            nf.txtText.Text = SecondToMinute(600)
            nf.Name = "输入"
            nf.labTitle.Text = "请输入要移动的间隔时间，单位为：分.秒"
sBe:
            nf.ShowDialog()
            If bCancelInput = 0 Then
                Dim tmpTime As Long
                If StrInputBoxText <> "" And bCancelInput = 0 Then
                    If Val(StrInputBoxText) < 0 Then
                        StrInputBoxText = -(Val(StrInputBoxText))
                        tmpTime = -MinuteToSecond(StrInputBoxText)
                    Else
                        tmpTime = MinuteToSecond(StrInputBoxText)
                    End If
                    If Math.Abs(tmpTime) > 0 And Math.Abs(tmpTime) <= 24 * 3600 Then
                        nMoveTime = tmpTime
                    Else
                        MsgBox("时间不正确，请重新输入！", , "提示")
                        GoTo sBe
                    End If
                End If

                For j = 1 To UBound(copyChediInf)
                    Dim nCurID As Integer
                    nCurID = AddNewChediInfor() ' GetCurCheDiInfoIDIDFromSchediID(StrInputBoxCombText)
                    For k = 1 To UBound(copyChediInf(j).nLinkTrain)
                        If nCurID > 0 Then
                            If copyChediInf(j).nLinkTrain(k) > 0 Then
                                nNewTrain = AddNewTrainID(CopyTrainInf(copyChediInf(j).nLinkTrain(k)).nTrain)
                                If nNewTrain > 0 Then
                                    Call CopyTrainInformationFromCopyTrainInf(copyChediInf(j).nLinkTrain(k), nNewTrain, nNewTrain)
                                    If nMoveTime <> 0 Then
                                        For i = 1 To UBound(TrainInf(nNewTrain).Arrival)
                                            TrainInf(nNewTrain).Arrival(i) = TimeAdd(TrainInf(nNewTrain).Arrival(i), nMoveTime)
                                            TrainInf(nNewTrain).Starting(i) = TimeAdd(TrainInf(nNewTrain).Starting(i), nMoveTime)
                                        Next
                                        TrainInf(nNewTrain).sStartZFArrival = -1
                                        TrainInf(nNewTrain).sStartZFStarting = -1
                                        TrainInf(nNewTrain).sEndZFArrival = -1
                                        TrainInf(nNewTrain).sEndZFStarting = -1
                                        TrainInf(nNewTrain).Train = FormatPrintTrainHou(nNewTrain, 3)
                                        TrainInf(nNewTrain).sPrintTrain = nNewTrain
                                    End If
                                    Call AddLianGuaCheCi(nCurID, nNewTrain)
                                End If
                            End If
                        End If

                    Next
                Next
                If TimeTablePara.BifAutoBianCheCi = True Then
                    Call ResetPrintTrainString()
                End If
                Call addOneUndoInf()
                Call RefreshDiagram(1)
            End If
            nIfPreseCtrl = 0
        End If

    End Sub

    '复制和粘贴菜单设置
    Public Sub CopyAndPasteMnuSet()
        If UBound(CopyTrainInf) > 0 Then
            Me.粘贴VToolStripMenuItem.Enabled = True
            Me.粘贴ToolStripButton13.Enabled = True
            Me.粘贴列车ToolStripMenuItem2.Enabled = True
            Me.粘贴ToolStripMenuItem.Enabled = True
        Else
            Me.粘贴VToolStripMenuItem.Enabled = False
            Me.粘贴ToolStripButton13.Enabled = False
            Me.粘贴列车ToolStripMenuItem2.Enabled = False
            Me.粘贴ToolStripMenuItem.Enabled = False
        End If
        If TimeTablePara.nPubTrain > 0 Then
            Me.复制CToolStripMenuItem.Enabled = True
            Me.复制ToolStripButton9.Enabled = True
            Me.复制列车CToolStripMenuItem.Enabled = True
            Me.复制列车CToolStripMenuItem1.Enabled = True
            Me.剪切CToolStripMenuItem1.Enabled = True
            Me.剪切CToolStripMenuItem.Enabled = True
            Me.剪切ToolStripButton10.Enabled = True
            Me.剪切列车XToolStripMenuItem.Enabled = True
            Me.删除DToolStripMenuItem.Enabled = True
            Me.删除ToolStripButton11.Enabled = True
            Me.删除列车DToolStripMenuItem.Enabled = True
        Else
            Me.复制CToolStripMenuItem.Enabled = False
            Me.复制ToolStripButton9.Enabled = False
            Me.复制列车CToolStripMenuItem.Enabled = False
            Me.复制列车CToolStripMenuItem1.Enabled = False
            Me.剪切CToolStripMenuItem1.Enabled = False
            Me.剪切CToolStripMenuItem.Enabled = False
            Me.剪切ToolStripButton10.Enabled = False
            Me.剪切列车XToolStripMenuItem.Enabled = False
            Me.删除DToolStripMenuItem.Enabled = False
            Me.删除ToolStripButton11.Enabled = False
            Me.删除列车DToolStripMenuItem.Enabled = False
        End If
    End Sub

    Private Sub 复制列车CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 复制CToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 运行图管理ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 运行图管理MToolStripMenuItem1_Click(Nothing, Nothing)
    End Sub

    Private Sub 查找列车FToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 查找列车FToolStripMenuItem.Click
        Call 查找FToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 铺画运行图ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nf As New frmDrawTrainDiagram
        nf.Show()
    End Sub

    Private Sub 查找ToolStripButton12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 查找FToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 底图线型与颜色CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nf As New frmDiagramLineAndFontSet
        nf.TabControl1.SelectedIndex = 0
        nf.Show()
    End Sub

    Private Sub 运行图数据接口KToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If TimeTablePara.sPubCurSkbName = "" Then
            MsgBox("运行图名称为空，请先打开运行图!", , "提示")
            Exit Sub
        ElseIf TimeTablePara.sPubCurSkbName = "末命名时刻表" Then
            MsgBox("当前时刻表还没有命名，请先命名!", , "提示")
            Dim nf As New frmTimeTableManager
            nf.ShowDialog()
        Else
            Dim sCurSKBName As String
            Dim sSKBId As String
            Dim nID As Integer
            nID = GetTimetableInfID(TimeTablePara.sPubCurSkbName)
            sSKBId = TimetableInf(nID).sID

            Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
            sCurSKBName = sSKBId & "列车时刻信息"
            Dim strTable2 As String = "select * from " & sCurSKBName & "  order by 车次"
            Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
            '创建一个Datasetd
            Dim myDataSet2 As Data.DataSet = New Data.DataSet
            Mydc2.Fill(myDataSet2, "列车时刻信息")
            Dim myTable2 As Data.DataTable
            myTable2 = myDataSet2.Tables("列车时刻信息")

            sCurSKBName = sSKBId & "车底使用方案"
            Dim strTable3 As String = "select * from " & sCurSKBName & "  order by 车底ID"
            Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
            '创建一个Datasetd
            Dim myDataSet3 As Data.DataSet = New Data.DataSet
            Mydc3.Fill(myDataSet3, "车底使用方案")
            Dim myTable3 As Data.DataTable
            myTable3 = myDataSet3.Tables("车底使用方案")


            sCurSKBName = "列车运行标尺"
            Dim strTable4 As String = "select * from " & sCurSKBName & "  order by 标尺编号,区间序号"
            Dim Mydc4 As New Data.OleDb.OleDbDataAdapter(strTable4, strCon)
            '创建一个Datasetd
            Dim myDataSet4 As Data.DataSet = New Data.DataSet
            Mydc4.Fill(myDataSet4, "列车运行标尺")
            Dim myTable4 As Data.DataTable
            myTable4 = myDataSet4.Tables("列车运行标尺")

            Dim strPath As String
            Dim New0penFile As New SaveFileDialog
            New0penFile.Title = "请选择路径及输入数据库名称:"
            New0penFile.Filter = "TXT 文件(*.txt)|*.txt|XML文件 (*.xml)|*.xml"
            New0penFile.FilterIndex = 1
            New0penFile.FileName = TimeTablePara.sPubCurSkbName
            New0penFile.RestoreDirectory = True
            Dim sFile As String
            Dim sHou As String
            sFile = ""
            sHou = ""
            If New0penFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
                If New0penFile.FileName.Trim.Length > 3 Then
                    sFile = New0penFile.FileName.Trim.Substring(0, New0penFile.FileName.Trim.Length - 4)
                    sHou = New0penFile.FileName.Trim.Substring(New0penFile.FileName.Trim.Length - 4)
                End If
                strPath = sFile & "_时刻表信息" & sHou
                myTable2.WriteXml(strPath)
                strPath = sFile & "_车底使用方案" & sHou
                myTable3.WriteXml(strPath)
                strPath = sFile & "_区间运行标尺" & sHou
                myTable4.WriteXml(strPath)
            End If
        End If


    End Sub

    Private Sub 批处理组操作UToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nf As New frmEditDiagramByGroup
        nf.Show()
    End Sub

    Private Sub 全部列车重新铺画AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        If MsgBox("该操作将所有列车按新的标尺进行铺画(保持发点不变),请确认!", MsgBoxStyle.OkCancel + MsgBoxStyle.Information + MsgBoxStyle.DefaultButton2, "确认操作") = MsgBoxResult.Cancel Then Exit Sub
        If UBound(TrainInf) > 0 Then
            For i = 1 To UBound(TrainInf)
                TrainInf(i).sStartZFLine = ""
                TrainInf(i).sEndZFLine = ""
                If TrainInf(i).Train > "" Then
                    DrawSingleTrain(i, TrainInf(i).Starting(TrainInf(i).nPathID(1)), TrainInf(i).nPathID(1))
                End If
            Next
            Call addOneUndoInf()
            Call RefreshDiagram(1)
            MsgBox("铺画完毕!", , "提示")
        End If
    End Sub

    Private Sub timerCurDate_Elapsed(ByVal sender As System.Object, ByVal e As System.Timers.ElapsedEventArgs) Handles timerCurDate.Elapsed
        Dim tolNUm As Integer
        tolNUm = GetTolTrainNum()
        Me.ToolStripLabelTrainNumberInfor.Text = "列车总数:" & tolNUm.ToString
        Me.ToolStripLabelDate.Text = Now().ToString '"当前时间:" &
        Call CopyAndPasteMnuSet()
        Call UndoAndRedoMenuSet()
    End Sub

    Private Sub 复制ToolStripButton9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 复制CToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 剪切ToolStripButton10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 剪切CToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 粘贴ToolStripButton13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 粘贴VToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 删除ToolStripButton11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 删除DToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 打印运行图ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 打印运行图ToolStripButton1.Click
        Call 运行图DToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 线型与颜色ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 线型与颜色LToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    '将当前的动条移到到可以显示的列车
    Public Sub SetCurScrollbarInSelectTrain(ByVal nTrain As Integer)
        Dim nMax As Single
        Dim curX As Single
        Dim xMax As Single
        Dim tmpTime As Long
        Dim nBi As Single
        Dim nBarX As Single
        nMax = SplitDiagram.Panel2.HorizontalScroll.Maximum
        tmpTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
        If tmpTime > 0 Then
            curX = FormTimeToXCord(tmpTime, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
            xMax = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
            If xMax > 0 Then
                nBi = (curX) / xMax
                nBarX = nMax * nBi - 400
                SplitDiagram.Panel2.AutoScrollPosition = New Point(nBarX, -SplitDiagram.Panel2.AutoScrollPosition.Y)
            End If
        End If
    End Sub


    Private Sub 自动检查错误EToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If Me.自动检查错误EToolStripMenuItem.Checked = True Then
        '    Me.自动检查错误EToolStripMenuItem.Checked = False
        'Else
        '    Me.自动检查错误EToolStripMenuItem.Checked = True
        'End If
    End Sub


    Private Sub 铺画间隔设置ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nf As New frmInputBox
        nf.cmbText.Visible = False
        nf.txtText.Visible = True
        nf.txtText.Text = SecondToMinute(CDZDrawPara.nCurJianGe)
        nf.Name = "输入"
        nf.labTitle.Text = "请输入列车铺画的间隔时间，单位为：分.秒"
sBe:
        nf.ShowDialog()
        Dim tmpTime As Long
        If StrInputBoxText <> "" And bCancelInput = 0 Then
            tmpTime = MinuteToSecond(StrInputBoxText)
            If tmpTime > 0 And tmpTime < 12 * 3600 Then
                CDZDrawPara.nCurJianGe = tmpTime
            Else
                MsgBox("时间不正确，请重新输入！", , "提示")
                GoTo sBe
            End If
        End If

    End Sub

    Private Sub 修改车底所有列车车次NToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改车底所有列车车次NToolStripMenuItem.Click
        Dim nCurID As Integer
        Dim nFirTrain As Integer
        If TimeTablePara.nPubCheDi > 0 Then
            nCurID = TimeTablePara.nPubCheDi
sBE:
            Dim nf As New frmInputBox
            nf.txtText.Visible = True
            nf.cmbText.Visible = False
            nf.Text = "修改车次"
            nf.labTitle.Text = "请输入第一列车车次:"
            nFirTrain = ChediInfo(nCurID).nLinkTrain(1)
            nf.txtText.Text = TrainInf(nFirTrain).Train
            nf.txtText.Select()
            nf.ShowDialog()
            Dim i, p, k As Integer
            Dim ntmpNum As Integer
            Dim ntmpDownNum As Integer
            ntmpNum = 0
            ntmpDownNum = 0
            Dim nTrainNum() As String
            Dim nIfIn As Integer
            If bCancelInput = 0 Then
                If StrInputBoxText <> "" Then
                    If nFirTrain Mod 2 = 0 Then
                        If Val(Microsoft.VisualBasic.Right(StrInputBoxText, 1)) Mod 2 <> 0 Then
                            MsgBox("当前车底的第一列车为下行车，该车次不是下行车次，请重新输入！", , "提示")
                            GoTo sBE
                        End If
                    Else
                        If Val(Microsoft.VisualBasic.Right(StrInputBoxText, 1)) Mod 2 = 0 Then
                            MsgBox("当前车底的第一列车为上行车，该车次不是上行车次，请重新输入！", , "提示")
                            GoTo sBE
                        End If
                    End If
                    ReDim nTrainNum(UBound(ChediInfo(nCurID).nLinkTrain))
                    If nFirTrain Mod 2 = 0 Then '第一列为上行
                        For i = 1 To UBound(nTrainNum)
                            If ChediInfo(nCurID).nLinkTrain(i) Mod 2 = 0 Then '上行车
                                nTrainNum(i) = StrInputBoxText.Substring(0, 1) & Val(StrInputBoxText.Substring(1, StrInputBoxText.Length - 1)) + ntmpNum * 2
                                ntmpNum = ntmpNum + 1
                            Else '下行车
                                nTrainNum(i) = StrInputBoxText.Substring(0, 1) & Val(StrInputBoxText.Substring(1, StrInputBoxText.Length - 1)) + ntmpDownNum * 2 + 1
                                ntmpDownNum = ntmpDownNum + 1
                            End If
                        Next
                    Else
                        For i = 1 To UBound(nTrainNum)
                            If ChediInfo(nCurID).nLinkTrain(i) Mod 2 = 0 Then '上行车
                                nTrainNum(i) = StrInputBoxText.Substring(0, 1) & Val(StrInputBoxText.Substring(1, StrInputBoxText.Length - 1)) + ntmpNum * 2 + 1
                                ntmpNum = ntmpNum + 1
                            Else '下行车
                                nTrainNum(i) = StrInputBoxText.Substring(0, 1) & Val(StrInputBoxText.Substring(1, StrInputBoxText.Length - 1)) + ntmpDownNum * 2
                                ntmpDownNum = ntmpDownNum + 1
                            End If
                        Next
                    End If

                    For p = 1 To UBound(nTrainNum)
                        For i = 1 To UBound(TrainInf)
                            nIfIn = 0
                            For k = 1 To UBound(ChediInfo(nCurID).nLinkTrain)
                                If i = ChediInfo(nCurID).nLinkTrain(k) Then
                                    nIfIn = 1
                                    Exit For
                                End If
                            Next
                            If nIfIn = 0 Then
                                If TrainInf(i).Train = nTrainNum(p) Then
                                    MsgBox("车次 " & nTrainNum(p) & " 已经被其他列车占用，该操作无法完成，请重新输入第一列车车次！", , "提示")
                                    GoTo sBE
                                End If
                            End If
                        Next
                    Next

                    For p = 1 To UBound(nTrainNum)
                        TrainInf(ChediInfo(nCurID).nLinkTrain(p)).sPrintTrain = nTrainNum(p)
                        TrainInf(ChediInfo(nCurID).nLinkTrain(p)).Train = nTrainNum(p).Trim
                    Next

                    Call addOneUndoInf()
                    Call RefreshDiagram(1)
                Else
                    MsgBox("输入为空，请重新输入!", , "提示")
                    GoTo sBE
                End If
            End If
        End If
    End Sub

    Private Sub SplitDiagram_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles SplitDiagram.Paint
        'MsgBox("a")
    End Sub

    Private Sub 可以越行ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If 可以越行ToolStripMenuItem.Checked = True Then
        '    TimeTablePara.sDrawLineStyle = "不能越行"
        '    不越行ToolStripMenuItem.Checked = True
        '    可以越行ToolStripMenuItem.Checked = False
        'Else
        '    不越行ToolStripMenuItem.Checked = False
        '    可以越行ToolStripMenuItem.Checked = True
        '    TimeTablePara.sDrawLineStyle = "可以越行"
        'End If
    End Sub

    Private Sub 不越行ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If 不越行ToolStripMenuItem.Checked = True Then
        '    可以越行ToolStripMenuItem.Checked = True
        '    不越行ToolStripMenuItem.Checked = False
        '    TimeTablePara.sDrawLineStyle = "可以越行"
        'Else
        '    可以越行ToolStripMenuItem.Checked = False
        '    不越行ToolStripMenuItem.Checked = True
        '    TimeTablePara.sDrawLineStyle = "不能越行"
        'End If

    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        Dim i As Integer
        Dim k As Integer
        Dim nTrain As Integer
        If UBound(TimeTablePara.nPubTrains) > 0 Then
            Dim nf As New frmInputBox
            nf.Text = "选择交路名"
            nf.labTitle.Text = "选择要修改的交路名:"
            nf.cmbText.Visible = True
            nf.txtText.Visible = False
            nf.cmbText.Items.Clear()
            nTrain = TimeTablePara.nPubTrains(1)
            If UBound(BasicTrainInf) > 0 Then
                If nTrain Mod 2 = 0 Then
                    For i = 1 To UBound(BasicTrainInf)
                        If BasicTrainInf(i).nUporDown = 2 Then
                            nf.cmbText.Items.Add(BasicTrainInf(i).sJiaoLuName)
                        End If
                    Next i
                Else
                    For i = 1 To UBound(BasicTrainInf)
                        If BasicTrainInf(i).nUporDown = 1 Then
                            nf.cmbText.Items.Add(BasicTrainInf(i).sJiaoLuName)
                        End If
                    Next i
                End If
            Else
                MsgBox("列车信息中没有交路信息，请检查列车信息！", , "提示")
                Exit Sub
            End If

            nf.cmbText.Text = TrainInf(nTrain).sJiaoLuName
            nf.ShowDialog()
            If StrInputBoxCombText <> "" And bCancelInput = 0 Then
                For k = 1 To UBound(TimeTablePara.nPubTrains)
                    nTrain = TimeTablePara.nPubTrains(k)
                    If nTrain Mod 2 = 0 Then
                        Call ResetLongTrain(nTrain, 2, StrInputBoxCombText)
                    Else
                        Call ResetLongTrain(nTrain, 1, StrInputBoxCombText)
                    End If
                Next
            End If
            Call addOneUndoInf()
            Call RefreshDiagram(1)
            If TimeTablePara.BifAutoBianCheCi = True Then
                Call ResetPrintTrainString()
            End If
        End If
    End Sub

    Private Sub ToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem6.Click
        Dim nf As New frmDrawSingleTrain
        nf.sCurFormState = "修改标尺"
        nf.nBeforTrainNums = TimeTablePara.nPubTrains
        nf.ShowDialog()
        If nf.IfNotDrawLine = 1 Then
            Call addOneUndoInf()
            Call RefreshDiagram(1)
        End If
    End Sub

    Private Sub ToolStripMenuItem15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem15.Click
        Call 删除列车DToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem10.Click
        Dim i As Integer
        Dim nTrain As Integer
        If UBound(TimeTablePara.nPubTrains) > 0 Then
            If MsgBox("确定断开这些列车的车底连接方式吗？", vbQuestion + vbYesNo + vbDefaultButton2, "确认操作") = vbYes Then
                For i = 1 To UBound(TimeTablePara.nPubTrains)
                    nTrain = TimeTablePara.nPubTrains(i)
                    If TrainInf(nTrain).TrainReturn(1) = 0 Then
                        'MsgBox("该列车前面接续列车为空，该操作不能实现！")
                        'Stop
                    Else
                        Dim Cdid As Integer
                        Cdid = CheCiToCheDiID(nTrain)
                        If Cdid <> 0 Then
                            Call DelectCheDiLink(nTrain, Cdid)
                        End If
                    End If
                Next
                If TimeTablePara.BifAutoBianCheCi = True Then
                    Call ResetPrintTrainString()
                End If
                TimeTablePara.nPubCheDi = 0
                Call addOneUndoInf()
                Call ShowChediJiaolu2()
            End If
        End If
    End Sub

    Private Sub 复制列车CToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 复制列车CToolStripMenuItem.Click
        Call 复制CToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 剪切列车XToolStripMenuItem.Click
        Call 剪切CToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 粘贴列车ToolStripMenuItem2.Click
        Call 粘贴VToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 车底前加一车ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 车底前加一车ToolStripMenuItem4.Click
        If TimeTablePara.nPubTrain > 0 Then
            Dim nf As New frmDrawSingleTrain
            If TrainInf(TimeTablePara.nPubTrain).TrainReturn(1) = 0 Then
                nf.sCurFormState = "在车底前加一列车"
                nf.nBeforTrainNums = TimeTablePara.nPubTrains
                nf.ShowDialog()
                If nf.IfNotDrawLine = 1 Then
                    Call addOneUndoInf()
                    Call RefreshDiagram(1)
                End If
            Else
                MsgBox("该列车前面已经勾上了一列车，不能添加！", , "提示")
                Exit Sub
            End If
        End If
    End Sub

    Private Sub 车底后加一车ToolStripMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 车底后加一车ToolStripMenuItem7.Click
        If TimeTablePara.nPubTrain > 0 Then
            If TrainInf(TimeTablePara.nPubTrain).TrainReturn(2) = 0 Then
                Dim nf As New frmDrawSingleTrain
                nf.sCurFormState = "在车底后加一列车"
                nf.nBeforTrainNums = TimeTablePara.nPubTrains
                nf.ShowDialog()
                If nf.IfNotDrawLine = 1 Then
                    Call addOneUndoInf()
                    Call RefreshDiagram(1)
                End If
            Else
                MsgBox("该列车后已经勾上了一列车，不能添加！", , "提示")
                Exit Sub
            End If
        End If
    End Sub

    Private Sub 修改停站时间TToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改停站时间TToolStripMenuItem.Click
        Dim nTrain As Integer
        Dim tmpTime As Integer
        Dim sGuDaoNum As String
        Dim nID As Integer
        Dim nSta As Integer
        Dim nStopTime As Integer
        Dim nf As New frmInputBox
        nTrain = TimeTablePara.nPubTrain
        If GuDaoJishutujie.sCurSeleteState = "车站股道" Then
            If nTrain > 0 Then
                nSta = StaNameToStaInfID(Me.cmbJiShuTuJieSta.Text.Trim)
                nf.txtText.Visible = True
                nf.cmbText.Visible = False
                nStopTime = TimeMinus(TrainInf(TimeTablePara.nPubTrain).Starting(nSta), TrainInf(TimeTablePara.nPubTrain).Arrival(nSta))
                nf.txtText.Text = SecondToMinute(nStopTime)
                nf.labTitle.Text = "请输入停站时间: 格式为 (分.秒)"
sBe:
                nf.ShowDialog()
                If bCancelInput = 1 Then Exit Sub
                tmpTime = MinuteToSecond(StrInputBoxText)
                If tmpTime > 0 Then
                Else
                    MsgBox("输入错误，请重新输入！", , "提示")
                    GoTo sBe
                End If
                sGuDaoNum = GuDaoJishutujie.sGuDao(nGuDaoID)
                nID = FromSGudaoNumtoGuDaoID(sGuDaoNum, nSta)
                Dim nOupyTrain As Integer
                nOupyTrain = 0
                Dim nAnoTrain As Integer
                nAnoTrain = GetAnoTrain(TimeTablePara.nPubTrain, nSta)
                Dim Time1 As Long
                Dim Time2 As Long
                If sGuDaoNum <> "" Then
                    ReDim nGuDaoTrain(0)
                    If nAnoTrain > 0 Then
                        If TrainInf(nTrain).NextStation = StationInf(nSta).sStationName Then
                            Time1 = TrainInf(TimeTablePara.nPubTrain).Arrival(nSta)
                            Time2 = TimeAdd(Time1, tmpTime)
                        ElseIf TrainInf(nTrain).ComeStation = StationInf(nSta).sStationName Then
                            Time2 = TrainInf(TimeTablePara.nPubTrain).Starting(nSta) 'TimeAdd(Time1, tmpTime)
                            Time1 = TimeMinus(Time2, tmpTime)
                        End If
                    Else
                        Time1 = TrainInf(TimeTablePara.nPubTrain).Arrival(nSta)
                        Time2 = TimeAdd(Time1, tmpTime)
                    End If
                    nOupyTrain = CurGudaoIfOcupy(nSta, sGuDaoNum, Time1, Time2, TimeTablePara.nPubTrain, nAnoTrain)
                    If nOupyTrain > 0 Then
                        MsgBox("当修改停站时间时，该股道将被列车" & TrainInf(nOupyTrain).Train & "占用，请修改停站时间或修改停站股道!", , "提示")
                        Me.PicDiagram.Refresh()
                    Else
                        If TrainInf(nTrain).NextStation = StationInf(nSta).sStationName Then
                            Call RecordStaTime(TimeTablePara.nPubTrain, nSta, Time2, TrainInf(TimeTablePara.nPubTrain).Arrival(nSta))
                            TrainInf(TimeTablePara.nPubTrain).Starting(nSta) = Time2
                            TrainInf(TimeTablePara.nPubTrain).sEndZFArrival = TimeAdd(TrainInf(TimeTablePara.nPubTrain).sEndZFArrival, tmpTime - nStopTime)
                            If nAnoTrain > 0 Then
                                TrainInf(nAnoTrain).sStartZFArrival = TrainInf(TimeTablePara.nPubTrain).sEndZFArrival
                            End If
                        ElseIf TrainInf(nTrain).ComeStation = StationInf(nSta).sStationName Then
                            Call RecordStaTime(TimeTablePara.nPubTrain, nSta, TrainInf(TimeTablePara.nPubTrain).Starting(nSta), Time1)
                            TrainInf(TimeTablePara.nPubTrain).sStartZFStarting = TimeMinus(TrainInf(TimeTablePara.nPubTrain).sStartZFStarting, tmpTime - nStopTime)
                            If nAnoTrain > 0 Then
                                TrainInf(nAnoTrain).sEndZFStarting = TrainInf(TimeTablePara.nPubTrain).sStartZFStarting
                            End If
                        Else
                            TrainInf(TimeTablePara.nPubTrain).Starting(nSta) = Time2
                        End If
                        Call addOneUndoInf()
                        Call cmbJiShuTuJieSta_SelectedValueChanged(Nothing, Nothing)
                    End If
                End If
                TimeTablePara.nStaJiShuTuJieSeletedState = 0
                Me.PicDiagram.Cursor = Cursors.Default
            End If
        End If

    End Sub

    Private Sub 车站进路搜索SToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nf As New frmStaJinLu
        nf.Show()
    End Sub

    Private Sub PrintCurStaCADPic(ByVal sStaName As String, ByVal nleftX As Long, ByVal nTopY As Long, ByVal rBmpGraphics As Graphics)
        Dim i As Integer
        Dim j As Integer
        Dim priMaxX As Single
        Dim priMinX As Single
        Dim priMaxY As Single
        Dim priMinY As Single
        Dim priLeftBlank As Single
        Dim priTopYblank As Single
        Dim nCurPicLeftX As Single
        Dim nCurPicTopY As Single
        priMinX = 10000000
        priMaxX = -1000000
        priMinY = 10000000
        priMaxY = -1000000
        priLeftBlank = 20
        priTopYblank = 20
        Dim nCurStaID As Integer
        For i = 1 To UBound(CADStaInf)
            If CADStaInf(i).sStaName = sStaName Then
                For j = 1 To UBound(CADStaInf(i).Track)
                    If CADStaInf(i).Track(j).X1 < priMinX Then
                        priMinX = CADStaInf(i).Track(j).X1
                    End If
                    If CADStaInf(i).Track(j).X2 < priMinX Then
                        priMinX = CADStaInf(i).Track(j).X2
                    End If
                    If CADStaInf(i).Track(j).X1 > priMaxX Then
                        priMaxX = CADStaInf(i).Track(j).X1
                    End If
                    If CADStaInf(i).Track(j).X2 > priMaxX Then
                        priMaxX = CADStaInf(i).Track(j).X2
                    End If
                    If CADStaInf(i).Track(j).Y1 < priMinY Then
                        priMinY = CADStaInf(i).Track(j).Y1
                    End If
                    If CADStaInf(i).Track(j).Y2 < priMinY Then
                        priMinY = CADStaInf(i).Track(j).Y2
                    End If
                    If CADStaInf(i).Track(j).Y1 > priMaxY Then
                        priMaxY = CADStaInf(i).Track(j).Y1
                    End If
                    If CADStaInf(i).Track(j).Y2 > priMaxY Then
                        priMaxY = CADStaInf(i).Track(j).Y2
                    End If
                Next
                nCurStaID = i
                Exit For
            End If
        Next

        Dim tmpPen As Pen
        tmpPen = New Pen(Color.White, 4)
        nCurPicLeftX = priLeftBlank + nleftX
        nCurPicTopY = priTopYblank + nTopY
        Call DrawStaPicFormStaID(nCurStaID, 1, Color.White, 4, 3, rBmpGraphics, priMinX, priMinY, nCurPicLeftX, nCurPicTopY, True, True, True, False)
    End Sub

    Private Sub 剪切CToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 剪切CToolStripMenuItem1.Click
        Call 剪切CToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 复制列车CToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 复制列车CToolStripMenuItem1.Click
        Call 复制CToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 粘贴ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 粘贴ToolStripMenuItem.Click
        Call 粘贴VToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 多选ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 多选ToolStripButton1.Click
        If Me.多选ToolStripButton1.Checked = True Then
            Me.多选ToolStripButton1.Checked = False
            nPubAdjustTrainLineState = 0
        Else
            Me.多选ToolStripButton1.Checked = True
            SelectTime.X1 = 0
            SelectTime.Y1 = 0
            SelectTime.X2 = 0
            SelectTime.Y2 = 0
            'SelectTime.intCurSelectFirTime = -1
            'SelectTime.intCurSelectSecTime = -1
            nPubAdjustTrainLineState = 12 '多选取
        End If
    End Sub

    Private Sub 鼠标平移MToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 鼠标平移MToolStripMenuItem.Click
        Dim i As Integer
        Dim tmpX, tmpY As Single
        ReDim MoveTimeXY(0)
        If TimeTablePara.nPubTrain > 0 Then
            nPubAdjustTrainLineState = 11 '平移列车
            Dim tmpGraphics As Graphics '画线路与车站图的定义的对象
            tmpGraphics = Me.PicDiagram.CreateGraphics()
            ReDim MoveTimeXY(UBound(TrainInf(TimeTablePara.nPubTrain).nPathID))
            For i = 1 To UBound(TrainInf(TimeTablePara.nPubTrain).nPathID)
                tmpX = GetTrainrStartXCoord(TimeTablePara.nPubTrain, TrainInf(TimeTablePara.nPubTrain).nPathID(i))
                tmpY = StationInf(TrainInf(TimeTablePara.nPubTrain).nPathID(i)).YPicValue
                'MoveTimeXY(i).nSta = TrainInf(TimeTablePara.nPubTrain).nPathID(i)
                MoveTimeXY(i).X = tmpX
                MoveTimeXY(i).Y = tmpY
                tmpX = GetTrainArriXCoord(TimeTablePara.nPubTrain, TrainInf(TimeTablePara.nPubTrain).nPathID(i))
                MoveTimeXY(i).X2 = tmpX
                MoveTimeXY(i).Y2 = tmpY
                'tmpGraphics.DrawEllipse(Pens.Blue, tmpX - 8, tmpY - 8, 16, 16)
            Next
            '    Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
            '    Dim tmpPen As Pen
            '    rBmpGraphics = Me.PicDiagram.CreateGraphics()
            '    tmpPen = New Pen(Color.Blue, 2)
            '    Call DrawLineInPicture(TimeTablePara.nPubTrain, rBmpGraphics, tmpPen, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
        End If
    End Sub

    Private Sub 指定平移时间FToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 指定平移时间FToolStripMenuItem.Click
        Dim i As Integer
        Dim j As Integer
        Dim nTrain As Integer
        Dim nMoveTime As Long
        Dim nf As New frmInputBox
        nf.cmbText.Visible = False
        nf.txtText.Visible = True
        nf.txtText.Text = SecondToMinute(600)
        nf.Name = "输入"
        nf.labTitle.Text = "请输入要移动的间隔时间，单位为：分.秒"
sBe:
        nf.ShowDialog()
        Dim tmpTime As Long
        If StrInputBoxText <> "" And bCancelInput = 0 Then
            If Val(StrInputBoxText) < 0 Then
                StrInputBoxText = -(Val(StrInputBoxText))
                tmpTime = -MinuteToSecond(StrInputBoxText)
            Else
                tmpTime = MinuteToSecond(StrInputBoxText)
            End If
            If Math.Abs(tmpTime) > 0 And Math.Abs(tmpTime) <= 24 * 3600 Then
                nMoveTime = tmpTime
            Else
                MsgBox("时间不正确，请重新输入！", , "提示")
                GoTo sBe
            End If
        End If
        If nMoveTime <> 0 Then
            For j = 1 To UBound(TimeTablePara.nPubTrains)
                nTrain = TimeTablePara.nPubTrains(j)

                For i = 1 To UBound(TrainInf(nTrain).Arrival)
                    TrainInf(nTrain).Arrival(i) = TimeAdd(TrainInf(nTrain).Arrival(i), nMoveTime)
                    TrainInf(nTrain).Starting(i) = TimeAdd(TrainInf(nTrain).Starting(i), nMoveTime)
                Next

                TrainInf(nTrain).lAllStartTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
                TrainInf(nTrain).lAllEndTime = TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID)))

                TrainInf(nTrain).sStartZFArrival = -1
                TrainInf(nTrain).sStartZFStarting = -1
                TrainInf(nTrain).sEndZFArrival = -1
                TrainInf(nTrain).sEndZFStarting = -1
            Next
            'If TimeTablePara.BifAutoBianCheCi = True Then
            '    Call ResetPrintTrainString()
            'End If
            Call addOneUndoInf()
            Call RefreshDiagram(1)
            Call PicDiagram_Paint(Nothing, Nothing)
        End If

    End Sub

    Private Sub 平移列车MToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 平移列车MToolStripMenuItem1.Click
        Call 指定平移时间FToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 调整时移动提示ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If 调整时移动提示ToolStripMenuItem.Checked = True Then
        '    调整时移动提示ToolStripMenuItem.Checked = False
        '    TimeTablePara.sTiaoLineState = "提示"
        'Else
        '    调整时移动提示ToolStripMenuItem.Checked = True
        '    TimeTablePara.sTiaoLineState = "不提示"
        'End If
    End Sub

    Private Sub 车底信息DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 车底信息DToolStripMenuItem.Click
        If TimeTablePara.nPubCheDi > 0 Then
            Dim nf As New frmCheDiInformation
            nf.TabControl1.TabPages(2).Select()
            nf.ShowDialog()
        End If
    End Sub

    Private Sub 列车信息TToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 列车信息TToolStripMenuItem.Click
        If TimeTablePara.nPubTrain > 0 Then
            Dim nf As New frmTrainInfor
            nf.TabInf.TabPages(4).Select()
            nf.ShowDialog()
        End If
    End Sub

    Private Sub 整理车站股道ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        If MsgBox("该操作将对所有列车车站股道使用信息进行整理，原有信息将不再保存，请确认？", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "确认操作") = MsgBoxResult.Yes Then
            Call addOneUndoInf()
            For i = 1 To UBound(TrainInf)
                If TrainInf(i).Train <> "" Then
                    For j = 1 To UBound(StationInf)
                        'if stationinf(traininf(i).nPathID (j)).sStLineNo then
                        If i Mod 2 <> 0 Then
                            TrainInf(i).StopLine(j) = 1
                        Else
                            TrainInf(i).StopLine(j) = 2
                        End If
                    Next
                End If
            Next
            Call ResertAllTrainStartOrArriTime()
            MsgBox("整理完毕!", , "提示")
        End If

    End Sub

    Private Sub PicStation2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PicStation2.MouseDown
        'Dim i, kk As Integer
        'kk = UBound(StationInf)
        'For i = 1 To UBound(StationInf)
        '    If StationInf(i).StaNamePosRec.Contains(e.X, e.Y) Then
        '        If StationInf(i).IsExpress = False Then
        '            Call ExpressStation(StationInf, i)
        '        Else
        '            Call UnfoldStation(StationInf, i)
        '        End If
        '        Exit For
        '    End If
        'Next
        '' MsgBox(UBound(StationInf).ToString, MsgBoxStyle.Information, kk)
        'Call RefreshDiagram(1)
    End Sub

    Private Sub 整理折返时刻ZToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("该操作将对所有列车的到发时刻进行整理，原有信息将不再保存，请确认？", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "确认操作") = MsgBoxResult.Yes Then
            Call addOneUndoInf()
            Call ResertAllTrainStartOrArriTime()
            Dim nf As New frmInputBox
            nf.Text = "选择整理规则"
            nf.labTitle.Text = "选择整理规则:"
            nf.cmbText.Visible = True
            nf.txtText.Visible = False
            nf.cmbText.Items.Clear()
            nf.cmbText.Items.Add("富余时间平均分配到在到发线与折返线上")
            nf.cmbText.Items.Add("富余时间放在折返线上")
            nf.cmbText.Items.Add("富余时间放在到发线上")
            nf.cmbText.Text = "富余时间平均分配到在到发线与折返线上"
            nf.ShowDialog()
            If StrInputBoxCombText <> "" And bCancelInput = 0 Then
                Call CalZheFanTimeAndZheFanGuDao(StrInputBoxCombText)
            End If
        End If
    End Sub

    Private Sub 整理到发时刻DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("该操作将对所有列车的到发时刻进行整理，原有信息将不再保存，请确认？", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "确认操作") = MsgBoxResult.Yes Then
            Call addOneUndoInf()
            Call ResertAllTrainStartOrArriTime()
        End If
    End Sub

    Private Sub 单一或大小及共线交路ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 单一或大小及共线交路ToolStripMenuItem.Click
        Dim nf As New frmDrawTrainDiagram
        nf.Show()
    End Sub

    Private Sub 环形交路ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 环形交路ToolStripMenuItem.Click
        Dim nf As New frmDrawCircleTrainDiagram
        nf.Show()
    End Sub

    Private Sub 只显示前20条ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If 只显示前20条ToolStripMenuItem.Checked = False Then
        '    只显示前20条ToolStripMenuItem.Checked = True
        '    全部显示ToolStripMenuItem.Checked = False
        '    TimeTablePara.sErrorShowStyle = "显示前20条"
        'Else
        '    TimeTablePara.sErrorShowStyle = "显示前20条"
        'End If
        Call RefreshDiagram(1)
    End Sub

    Private Sub 全部显示ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If 全部显示ToolStripMenuItem.Checked = False Then
        '    全部显示ToolStripMenuItem.Checked = True
        '    只显示前20条ToolStripMenuItem.Checked = False
        '    TimeTablePara.sErrorShowStyle = "显示全部"
        'Else
        '    TimeTablePara.sErrorShowStyle = "显示全部"
        'End If
        Call RefreshDiagram(1)
    End Sub

    Private Sub 秒格RToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TimeTablePara.TimeTableDiagramPara.strTimeFormat = "15秒格"
        Call RefreshDiagram(1)
    End Sub

    Private Sub 修改列车类型LToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改列车类型LToolStripMenuItem.Click
        If TimeTablePara.nPubTrain > 0 Then
sBE:
            Dim nf As New frmInputBox

            nf.txtText.Visible = True
            nf.cmbText.Visible = False
            nf.Text = "修改列车类型"
            nf.labTitle.Text = "请选择要修改的类型:"
            nf.cmbText.Visible = True
            nf.txtText.Visible = False
            nf.cmbText.Items.Clear()
            nf.cmbText.Items.Add("能载客+正常车")
            nf.cmbText.Items.Add("不载客+正常车")
            nf.cmbText.Items.Add("能载客+轧道车")
            nf.cmbText.Items.Add("不载客+轧道车")
            nf.cmbText.Items.Add("能载客+调试车")
            nf.cmbText.Items.Add("不载客+调试车")
            nf.cmbText.Text = TrainInf(TimeTablePara.nPubTrain).sTrainXingZhi
            nf.txtText.Select()
            nf.ShowDialog()
            If bCancelInput = 0 Then
                If StrInputBoxCombText <> "" And bCancelInput = 0 Then
                    TrainInf(TimeTablePara.nPubTrain).sTrainXingZhi = StrInputBoxCombText
                    ' TrainInf(TimeTablePara.nPubTrain).Train = StrInputBoxText
                    Call addOneUndoInf()
                    Call RefreshDiagram(1)
                Else
                    MsgBox("输入为空，请重新输入!", , "提示")
                    GoTo sBE
                End If
            End If
        End If
    End Sub

    Private Sub 修改列车性质ZToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改列车性质ZToolStripMenuItem.Click
        Dim i As Integer
        If UBound(TimeTablePara.nPubTrains) > 0 Then
sBE:
            Dim nf As New frmInputBox
            nf.txtText.Visible = True
            nf.cmbText.Visible = False
            nf.Text = "修改列车类型"
            nf.labTitle.Text = "请选择要修改的类型:"
            nf.cmbText.Visible = True
            nf.txtText.Visible = False
            nf.cmbText.Items.Clear()
            nf.cmbText.Items.Add("能载客+正常车")
            nf.cmbText.Items.Add("不载客+正常车")
            nf.cmbText.Items.Add("能载客+轧道车")
            nf.cmbText.Items.Add("不载客+轧道车")
            nf.cmbText.Items.Add("能载客+调试车")
            nf.cmbText.Items.Add("不载客+调试车")
            nf.cmbText.Text = ""
            nf.txtText.Select()
            nf.ShowDialog()
            If bCancelInput = 0 Then
                If StrInputBoxCombText <> "" And bCancelInput = 0 Then
                    For i = 1 To UBound(TimeTablePara.nPubTrains)
                        TrainInf(TimeTablePara.nPubTrains(i)).sTrainXingZhi = StrInputBoxCombText
                    Next
                    ' TrainInf(TimeTablePara.nPubTrain).Train = StrInputBoxText
                    Call addOneUndoInf()
                    Call RefreshDiagram(1)
                Else
                    MsgBox("输入为空，请重新输入!", , "提示")
                    GoTo sBE
                End If
            End If
        End If

    End Sub

    Private Sub 全选AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        ReDim TimeTablePara.nPubTrains(0)
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                ReDim Preserve TimeTablePara.nPubTrains(UBound(TimeTablePara.nPubTrains) + 1)
                TimeTablePara.nPubTrains(UBound(TimeTablePara.nPubTrains)) = i
            End If
        Next
    End Sub

    Private Sub 终到调匀EToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 终到调匀EToolStripMenuItem.Click
        Dim i As Integer
        Dim bIfSame As Boolean
        bIfSame = True
        Dim sCurRoutingName As String
        If TimeTablePara.nPubTrains.GetUpperBound(0) > 1 Then
            sCurRoutingName = TrainInf(TimeTablePara.nPubTrains(1)).sJiaoLuName
            For i = 2 To TimeTablePara.nPubTrains.GetUpperBound(0)
                If sCurRoutingName <> TrainInf(TimeTablePara.nPubTrains(i)).sJiaoLuName Then
                    bIfSame = False
                    Exit For
                End If
            Next
            If bIfSame = False Then
                MsgBox("这些列车的交路不一样，无法执行该操作！", , "提示")
                Exit Sub
            Else
                Dim nf As New frmSetTrainsStopSame
                nf.nCurTrains.Clear()
                nf.nCurSta = nCurStaID
                For i = 1 To TimeTablePara.nPubTrains.GetUpperBound(0)
                    nf.nCurTrains.Add(TimeTablePara.nPubTrains(i))
                Next
                nf.optEnd.Checked = True
                nf.ShowDialog()
            End If
        End If
    End Sub

    Private Sub 始发调匀SToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 始发调匀SToolStripMenuItem.Click
        Dim i As Integer
        Dim bIfSame As Boolean
        bIfSame = True
        Dim sCurRoutingName As String
        If TimeTablePara.nPubTrains.GetUpperBound(0) > 1 Then
            sCurRoutingName = TrainInf(TimeTablePara.nPubTrains(1)).sJiaoLuName
            For i = 2 To TimeTablePara.nPubTrains.GetUpperBound(0)
                If sCurRoutingName <> TrainInf(TimeTablePara.nPubTrains(i)).sJiaoLuName Then
                    bIfSame = False
                    Exit For
                End If
            Next
            If bIfSame = False Then
                MsgBox("这些列车的交路不一样，无法执行该操作！", , "提示")
                Exit Sub
            Else
                Dim nf As New frmSetTrainsStopSame
                nf.nCurTrains.Clear()
                nf.nCurSta = nCurStaID
                For i = 1 To TimeTablePara.nPubTrains.GetUpperBound(0)
                    nf.nCurTrains.Add(TimeTablePara.nPubTrains(i))
                Next
                nf.optStart.Checked = True
                nf.ShowDialog()
            End If
        End If
    End Sub
    Private Sub 向左翻页ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 向左翻页ToolStripMenuItem.Click
        TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime = TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime - 1
        If TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime < 0 Then
            TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime += 24
        End If
        Call RefreshDiagram(0)
    End Sub

    Private Sub 向右翻页ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 向右翻页ToolStripMenuItem.Click
        TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime = TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime + 1
        If TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime > 24 Then
            TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime -= 24
        End If
        Call RefreshDiagram(0)

    End Sub

    Private Sub ToolStripLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripLeft.Click
        Call 向左翻页ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripRight.Click
        Call 向右翻页ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 整理股道EToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 整理股道EToolStripMenuItem.Click
        If TrainInf(TimeTablePara.nPubTrain).ComeStation = TimeTablePara.StaDiagramePara.sCurStaName Then
            Call ResetCurZFTrainZFtime(TrainInf(TimeTablePara.nPubTrain).TrainReturn(1), TimeTablePara.nPubTrain, GetStationID(TimeTablePara.StaDiagramePara.sCurStaName))
            Call addOneUndoInf()
            Call RefreshDiagram(1)
        End If

    End Sub

    Private Sub ToolStripMenuItem2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Call 列车信息ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 保存ToolStripButton7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 保存ToolStripButton7.Click
        Call 保存运行图SToolStripMenuItem_Click_1(Nothing, Nothing)
    End Sub

    Private Sub 保存运行图SToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 保存运行图SToolStripMenuItem.Click
        If MsgBox("该操作将清空当前打开运行图的所有数据，该操作不可逆，请确认！", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "确认操作") = MsgBoxResult.Yes Then
            Me.Cursor = Cursors.WaitCursor
            Call SaveSkbTimeTableByOracle(Me.ProBar)
            Me.Cursor = Cursors.Default
            MsgBox("时刻表已经成功保存！", , "提示")
        End If
    End Sub

    Private Sub 修改车次IToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 修改车次IToolStripMenuItem.Click
        Dim i As Integer
        Dim nIFIn As Boolean
        If TimeTablePara.nPubTrain > 0 Then
sBE:
            Dim nf As New frmInputBox
            nf.txtText.Visible = True
            nf.cmbText.Visible = False
            nf.Text = "修改车次"
            nf.labTitle.Text = "请输入要修改的车次:"
            nf.txtText.Text = TrainInf(TimeTablePara.nPubTrain).sPrintTrain
            nf.txtText.Select()
            nf.ShowDialog()
            If bCancelInput = 0 Then
                If StrInputBoxText <> "" And bCancelInput = 0 Then
                    nIFIn = False
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If TrainInf(i).sPrintTrain = StrInputBoxText Then
                                nIFIn = True
                                Exit For
                            End If
                        End If
                    Next
                    If nIFIn = True Then
                        If MsgBox("当前车次已经存在，请确认是否继续操作！", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "确认修改") = MsgBoxResult.Ok Then
                            TrainInf(TimeTablePara.nPubTrain).sPrintTrain = StrInputBoxText.Trim
                            Call addOneUndoInf()
                            Call RefreshDiagram(1)
                        Else
                            GoTo sBE
                        End If
                    Else
                        TrainInf(TimeTablePara.nPubTrain).sPrintTrain = StrInputBoxText.Trim
                        Call addOneUndoInf()
                        Call RefreshDiagram(1)

                    End If

                Else
                    If MsgBox("你当前输入为空，确认吗!", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "确认修改") = MsgBoxResult.Ok Then
                        TrainInf(TimeTablePara.nPubTrain).sPrintTrain = StrInputBoxText.Trim
                        Call addOneUndoInf()
                        Call RefreshDiagram(1)
                    Else
                        GoTo sBE
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub 撤销UToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 撤销UToolStripMenuItem.Click
        TimeTablePara.nPubTrain = 0
        ReDim TimeTablePara.nPubTrains(0)
        If UndoSeq.nRedoID = 0 Then
            UndoSeq.nRedoID = UndoSeq.nCurUndoID
        End If
        Call UndoTraininf(1)
        Call UndoAndRedoMenuSet()
    End Sub

    Private Sub 重复RToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 重复RToolStripMenuItem.Click
        TimeTablePara.nPubTrain = 0
        ReDim TimeTablePara.nPubTrains(0)
        Call UndoTraininf(0)
        Call UndoAndRedoMenuSet()
    End Sub

    Private Sub 剪切CToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 剪切CToolStripMenuItem.Click
        If TimeTablePara.nPubTrain > 0 Then
            Call 复制CToolStripMenuItem_Click(Nothing, Nothing)
            Call 删除列车DToolStripMenuItem_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub 复制CToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 复制CToolStripMenuItem.Click
        Dim i As Integer
        If UBound(TimeTablePara.nPubTrains) > 0 Then
            ReDim CopyTrainInf(UBound(TimeTablePara.nPubTrains))
            Call InputCopyChedinInf()
            For i = 1 To UBound(TimeTablePara.nPubTrains)
                Call CopyTrainInformationToCopyTrainInf(TimeTablePara.nPubTrains(i), i)
            Next
            Call CopyAndPasteMnuSet()
        End If
    End Sub

    Private Sub 粘贴VToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 粘贴VToolStripMenuItem.Click
        Dim nNewTrain As Integer
        Dim nMoveTime As Long
        Dim i As Integer
        Dim k As Integer
        '   Dim sngRnd As Single
        Dim j As Integer
        Dim nf As New frmInputBox
        If UBound(CopyTrainInf) > 0 Then
            nf.cmbText.Visible = False
            nf.txtText.Visible = True
            nf.txtText.Text = SecondToMinute(600)
            nf.Name = "输入"
            nf.labTitle.Text = "请输入要移动的间隔时间，单位为：分.秒"
sBe:
            nf.ShowDialog()
            If bCancelInput = 0 Then
                Dim tmpTime As Long
                If StrInputBoxText <> "" And bCancelInput = 0 Then
                    If Val(StrInputBoxText) < 0 Then
                        StrInputBoxText = -(Val(StrInputBoxText))
                        tmpTime = -MinuteToSecond(StrInputBoxText)
                    Else
                        tmpTime = MinuteToSecond(StrInputBoxText)
                    End If
                    If Math.Abs(tmpTime) > 0 And Math.Abs(tmpTime) <= 24 * 3600 Then
                        nMoveTime = tmpTime
                    Else
                        MsgBox("时间不正确，请重新输入！", , "提示")
                        GoTo sBe
                    End If
                End If

                For j = 1 To UBound(copyChediInf)
                    Dim nCurID As Integer
                    nCurID = AddNewChediInfor() ' GetCurCheDiInfoIDIDFromSchediID(StrInputBoxCombText)
                    For k = 1 To UBound(copyChediInf(j).nLinkTrain)
                        If nCurID > 0 Then
                            If copyChediInf(j).nLinkTrain(k) > 0 Then
                                nNewTrain = AddNewTrainID(CopyTrainInf(copyChediInf(j).nLinkTrain(k)).nTrain)
                                If nNewTrain > 0 Then
                                    Call CopyTrainInformationFromCopyTrainInf(copyChediInf(j).nLinkTrain(k), nNewTrain, nNewTrain)
                                    If nMoveTime <> 0 Then
                                        For i = 1 To UBound(TrainInf(nNewTrain).Arrival)
                                            TrainInf(nNewTrain).Arrival(i) = TimeAdd(TrainInf(nNewTrain).Arrival(i), nMoveTime)
                                            TrainInf(nNewTrain).Starting(i) = TimeAdd(TrainInf(nNewTrain).Starting(i), nMoveTime)
                                        Next
                                        TrainInf(nNewTrain).sStartZFArrival = -1
                                        TrainInf(nNewTrain).sStartZFStarting = -1
                                        TrainInf(nNewTrain).sEndZFArrival = -1
                                        TrainInf(nNewTrain).sEndZFStarting = -1
                                        TrainInf(nNewTrain).Train = FormatPrintTrainHou(nNewTrain, 3)
                                        TrainInf(nNewTrain).sPrintTrain = nNewTrain
                                    End If
                                    Call AddLianGuaCheCi(nCurID, nNewTrain)
                                End If
                            End If
                        End If

                    Next
                Next
                'If TimeTablePara.BifAutoBianCheCi = True Then
                '    Call ResetPrintTrainString()
                'End If
                Call addOneUndoInf()
                Call RefreshDiagram(1)
            End If
            nIfPreseCtrl = 0
        End If

    End Sub

    Private Sub 删除DToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除DToolStripMenuItem.Click
        Call 删除列车DToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 全选AToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 全选AToolStripMenuItem.Click
        Dim i As Integer
        ReDim TimeTablePara.nPubTrains(0)
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                ReDim Preserve TimeTablePara.nPubTrains(UBound(TimeTablePara.nPubTrains) + 1)
                TimeTablePara.nPubTrains(UBound(TimeTablePara.nPubTrains)) = i
            End If
        Next

    End Sub

    Private Sub 查找FToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 查找FToolStripMenuItem.Click
        Dim i As Integer
        Dim nf As New frmInputBox
        nf.Text = "查找列车"
        nf.labTitle.Text = "选择或输入列车车次:"
        nf.cmbText.Visible = True
        nf.txtText.Visible = False
        nf.cmbText.Items.Clear()
        If UBound(TrainInf) > 0 Then
            For i = 1 To UBound(TrainInf)
                If TrainInf(i).Train <> "" Then
                    nf.cmbText.Items.Add(TrainInf(i).sPrintTrain)
                End If
            Next i
        End If
        If nf.cmbText.Items.Count > 0 Then
            nf.cmbText.Text = nf.cmbText.Items(0)
        End If

        nf.ShowDialog()
        If StrInputBoxCombText <> "" And bCancelInput = 0 Then
            TimeTablePara.nPubTrain = FormPrintCheCiToTrainID(StrInputBoxCombText)
            If TimeTablePara.nPubTrain > 0 Then
                TimeTablePara.nPubCheDi = CheCiToCheDiID(TimeTablePara.nPubTrain)
                Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
                Me.PicDiagram.Refresh()
                rBmpGraphics = Me.PicDiagram.CreateGraphics()
                Dim tmpPen As Pen
                tmpPen = New Pen(Color.SpringGreen, 2)
                Call TMSDrawLineInPicture(TimeTablePara.nPubTrain, rBmpGraphics, tmpPen, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX, TimeTablePara.TimeTableDiagramPara.nifPrintCheCi, TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                Call ShowLabInfor(TimeTablePara.nPubTrain, Me.labInfor)
                Call listTrainInMiddlePic(TimeTablePara.nPubTrain)
                Call CopyAndPasteMnuSet()
                Call UndoAndRedoMenuSet()
                nIFShowAllCheDiTrain = 0
            Else
                MsgBox("没有找到当前车次!", , "提示")
                Me.PicDiagram.Refresh()
            End If
        End If
    End Sub

    Private Sub 粘贴ToolStripButton13_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 粘贴ToolStripButton13.Click
        Call 粘贴VToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 剪切ToolStripButton10_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 剪切ToolStripButton10.Click
        Call 剪切CToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 复制ToolStripButton9_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 复制ToolStripButton9.Click
        Call 复制CToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 删除ToolStripButton11_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 删除ToolStripButton11.Click
        Call 删除DToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 查找ToolStripButton12_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 查找ToolStripButton12.Click
        Call 查找FToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 撤销tolStripUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 撤销tolStripUndo.Click
        Call 撤销UToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 重复tolStripRedo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 重复tolStripRedo.Click
        Call 重复RToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItem1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        If TimeTablePara.nPubCheDi > 0 Then
            nIFShowAllCheDiTrain = 1
        End If
    End Sub

    Private Sub ToolStripButton1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Call 计算指标XToolStripMenuItem_Click(Nothing, Nothing)
    End Sub
End Class
