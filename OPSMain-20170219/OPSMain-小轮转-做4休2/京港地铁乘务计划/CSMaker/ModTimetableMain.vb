Module ModTimetableMain
    ''' <summary>
    ''' 运行图车站名称；输出车站名称，与路网一致
    ''' </summary>
    ''' <remarks></remarks>
    Public OutputStationName As Dictionary(Of String, String)


    Structure typeTimeTableDiagramPara
        Dim intDiagramFirstTime As Integer '底图起始时间
        Dim intDiagramWholeTime As Integer '底图显示全部时间,底图显示时间宽
        Dim intCompareFirstTime As Integer '时间比较起始时间

        Dim sngPicDiagramWidth As Integer   '底图宽
        Dim sngPicDiagramHeight As Integer '底图高
        Dim sngPicStationWidth As Integer '车站站名图宽
        Dim sngPicStationHeight As Integer '车站站名图高

        Dim strTimeFormat As String '底图时分格式
        Dim sngtopBlank As Integer '底图上下空白高度
        Dim sngTimeBlank As Integer '底图时间空白高度
        Dim sngLeftBlank As Integer '底图左右空白高度
        Dim sngStaBlank As Integer '底图车站空白宽度
        Dim sngPubLeftX As Integer '左边缩进宽度
        Dim sngPubTopY As Integer '上面缩进高度
        Dim nifPrintCheCi As Boolean '是否打印车次
        Dim nIfPrintXieCheCi As Boolean '是否打印斜车次
        Dim nIfPrintDriverNo As Boolean '是否打印司机编号
        Dim nCheDiLineHeight As Integer '车底线高度
        Dim sCheDiLineStyle As String '车底线类型
        'Dim sCheCiShowStyle As Integer
        Dim nIfPrintEveryStaName As Boolean '是否在每个小时显示车站名

    End Structure

    Structure typeStaDiagramPara
        Dim nStaLineHeight As Integer '车站股道线高度
        Dim sCurStaName As String '当前车站
    End Structure

    Structure typeTimeTableTimeLinePara
        Dim OneTime1LineStyle As String
        Dim OneTime1LineWidth As Single
        Dim OneTime1LineColor As String
        Dim OneTime5LineStyle As String
        Dim OneTime5LineWidth As Single
        Dim OneTime5LineColor As String
        Dim OneTime10LineStyle As String
        Dim OneTime10LineWidth As Single
        Dim OneTime10LineColor As String
        Dim OneTime30LineStyle As String
        Dim OneTime30LineWidth As Single
        Dim OneTime30LineColor As String
        Dim OneTime60LineStyle As String
        Dim OneTime60LineWidth As Single
        Dim OneTime60LineColor As String

        Dim TwoTime2LineStyle As String
        Dim TwoTime2LineWidth As Single
        Dim TwoTime2LineColor As String
        Dim TwoTime10LineStyle As String
        Dim TwoTime10LineWidth As Single
        Dim TwoTime10LineColor As String
        Dim TwoTime30LineStyle As String
        Dim TwoTime30LineWidth As Single
        Dim TwoTime30LineColor As String
        Dim TwoTime60LineStyle As String
        Dim TwoTime60LineWidth As Single
        Dim TwoTime60LineColor As String

        Dim TenTime10LineStyle As String
        Dim TenTime10LineWidth As Single
        Dim TenTime10LineColor As String
        Dim TenTime30LineStyle As String
        Dim TenTime30LineWidth As Single
        Dim TenTime30LineColor As String
        Dim TenTime60LineStyle As String
        Dim TenTime60LineWidth As Single
        Dim TenTime60LineColor As String

        Dim HourTime60LineStyle As String
        Dim HourTime60LineWidth As Single
        Dim HourTime60LineColor As String

        Dim StaNameFontName As String '车站名称字体
        Dim StaNameFontSize As Single
        Dim StaNameFontBold As Boolean
        Dim StaNameFontItalic As Boolean
        Dim StaNameFontColor As String

        Dim TimeFontName As String '时间标注字体
        Dim TimeFontSize As Single
        Dim TimeFontBold As Boolean
        Dim TimeFontItalic As Boolean
        Dim TimeFontColor As String

        Dim StaLineStyle As String '车场中心线
        Dim StaLineWidth As Single
        Dim StaLineColor As String

        Dim FenStaLineStyle As String '分岔站中心线
        Dim FenStaLineWidth As Single
        Dim FenStaLineColor As String

        Dim CheChangStaLineStyle As String '车场线
        Dim CheChangStaLineWidth As Single
        Dim CheChangStaLineColor As String

        Dim TrainLineStyle As String '列车运行线
        Dim TrainLineWidth As Single
        Dim TrainLineColor As String

        Dim CheDiLineStyle As String '车底交路线
        Dim CheDiLineWidth As Single
        Dim CheDiLineColor As String

        Dim CheCiFontName As String   '车次标注字体
        Dim CheCiFontSize As Single
        Dim CheCiFontBold As Boolean
        Dim CheCiFontItalic As Boolean
        Dim CheCiFontColor As String

        Dim XieCheCiFontName As String   '斜向车次标注字体
        Dim XieCheCiFontSize As Single
        Dim XieCheCiFontBold As Boolean
        Dim XieCheCiFontItalic As Boolean
        Dim XieCheCiFontColor As String

        Dim UnAssignTrainLineStyle As String '未勾选列车
        Dim UnAssignTrainLineWidth As Single
        Dim UnAssignTrainLineColor As String

        Dim DutyOnLineStyle As String '班中任务
        Dim DutyOnLineWidth As Single
        Dim DutyOnLineColor As String

        Dim DutyDinnerLineStyle As String '用餐任务
        Dim DutyDinnerLineWidth As Single
        Dim DutyDinnerLineColor As String

        Dim DutyRestLineStyle As String '小休任务
        Dim DutyRestLineWidth As Single
        Dim DutyRestLineColor As String

        Dim DutyOffLineStyle As String '班后任务
        Dim DutyOffLineWidth As Single
        Dim DutyOffLineColor As String

        Dim DutyNoFontName As String   '任务编号
        Dim DutyNoFontSize As Single
        Dim DutyNoFontBold As Boolean
        Dim DutyNoFontItalic As Boolean
        Dim DutyNoFontColor As String

    End Structure

    '保存时刻表时用
    Structure typeOpenSKBSeekSeq
        Dim nTrainID As Integer
        Dim sTrainNum As String
        Dim nFirID As Integer
        Dim nSecID As Integer
    End Structure
    Public SKBseekSeq() As typeOpenSKBSeekSeq

    Structure typeTimeTableMainParameter

        Dim nPubTrain As Integer '当前选中的列车号
        Dim nPubTrains() As Integer '选择的列车组
        Dim nPubCheDi As Integer '当前选中的车底号
        Dim sPubTrainStrainDraw As TrainStrainDraw
        Dim sCurDiagramState As DiagramState
        Dim BifAutoBianCheCi As Boolean '是否自动编车次
        Dim sPubCurSkbName As String '时刻表名称
        'Dim sPubCurSKBID As String '时刻表ID
        Dim nMaxUndoID As Integer
        Dim nStaJiShuTuJieSeletedState As Integer
        Dim TimeTableDiagramPara As typeTimeTableDiagramPara '底图参数
        Dim StaDiagramePara As typeStaDiagramPara '车站股道图解参数
        Dim lngCurMouseDownTime As Long '鼠标当前单击坐标对应的时间
        Dim picPubStation As PictureBox '车站底图
        Dim picPubStation2 As PictureBox '车站底图2
        Dim picPubDiagram As PictureBox '运行图底图
        Dim picPubBack As PictureBox '备份图
        Dim DiagramStylePara As typeTimeTableTimeLinePara '运行图底图线参数
        Dim bIFAutoAddChuKuTrain As Boolean '是否加上出库线
        Dim bIFAutoAddRuKuTrain As Boolean '是否加上入库线
        Dim sInputDataError As String '打开数据库是否出错
        Dim sDrawLineStyle As String '铺画类型
        Dim sTiaoLineState As String '列车调整方式
        Dim sErrorShowStyle As String '错误显示方式
        Dim pubTimeBmpPic As Bitmap   '画好的时分底图
    End Structure
    Public TimeTablePara As typeTimeTableMainParameter
    Public CSTimeTablePara As typeTimeTableMainParameter

    Structure typeTimetableIndex
        Dim sSecName As String
        Dim nDownTrains As Integer
        Dim nUpTrans As Integer
        Dim nDownInterval As Integer
        Dim nUpInterval As Integer
        Dim nStockNumber As Integer
    End Structure

    Structure typeTimeIndex
        Dim nFirTime As Integer
        Dim nSecTime As Integer
        Dim TimetableIndex As Generic.List(Of typeTimetableIndex)
    End Structure
    Public TimeIndex As New Generic.List(Of typeTimeIndex)

    Enum DiagramState As Integer
        运行图 = 1
        技术图解 = 2
    End Enum
    Enum TrainStrainDraw As Integer
        有约束 = 1
        无约束 = 2
    End Enum

    Structure typeUndoInf
        Dim nXuHao As Integer
        Dim Traininf() As typeTrainInformation
        Dim CheDiInf() As typePublicChediInformation
    End Structure
    Public UndoInf() As typeUndoInf

    Structure typeUnDoSeq
        Dim nUpID As Integer
        Dim nDownID As Integer
        Dim nCurUndoID As Integer
        Dim nRedoID As Integer
        Dim nMinID As Integer
        Dim nStep As Integer
    End Structure
    Public UndoSeq As typeUnDoSeq

    '时刻表信息
    Structure typeTimetableInf
        Dim sName As String '时刻表名称
        Dim sID As String '时刻表ID
        Dim sCreateDate As Date '创建时间
        Dim sEditDate As Date '修改时间 
        Dim nID As Integer 'ID
        'Dim sTRAINDIAGRAMID As String
    End Structure
    Public TimetableInf() As typeTimetableInf

    '乘务计划表信息
    Structure typeCSTimetableInf
        Dim sName As String '时刻表名称
        Dim sID As String '时刻表ID
        Dim sCreateDate As Date '创建时间
        Dim sEditDate As Date '修改时间 
        Dim nID As Integer 'ID
        Dim sDiagramID As String
        Dim ScheduleState As CrewScheduleState
        Dim IFCorShcedule As Boolean
        Dim CorTimetableID As String
    End Structure

    Public CSTimetableInf() As typeCSTimetableInf

    '自定义底图变量
    '根据时刻表名称得到时刻表ID
    Public Function GetTimetableIDFromName(ByVal sName As String) As String
        Dim i As Integer
        GetTimetableIDFromName = ""
        For i = 1 To UBound(TimetableInf)
            If TimetableInf(i).sName = sName Then
                GetTimetableIDFromName = TimetableInf(i).sID
                Exit For
            End If
        Next
    End Function

    '根据时刻表名称得到时刻表ID

    Public Function GetTimetableInfID(ByVal sName As String) As Integer
        Dim i As Integer
        GetTimetableInfID = 0
        For i = 1 To UBound(TimetableInf)
            If TimetableInf(i).sName = sName Then
                GetTimetableInfID = i
                Exit For
            End If
        Next
    End Function
    '根据时刻表名称得到时刻表ID
    Public Function GetCSTimetableInfID(ByVal sName As String) As Integer
        Dim i As Integer
        GetCSTimetableInfID = 0
        For i = 1 To UBound(CSTimetableInf)
            If CSTimetableInf(i).sName = sName Then
                GetCSTimetableInfID = i
                Exit For
            End If
        Next
    End Function

    '通过数值得到线型
    Public Function GetLineStyleFromText(ByVal strText As String) As Drawing2D.DashStyle
        Select Case strText
            Case "实线"
                GetLineStyleFromText = Drawing2D.DashStyle.Solid
            Case "长虚线"
                GetLineStyleFromText = Drawing2D.DashStyle.Dash
            Case "点虚线"
                GetLineStyleFromText = Drawing2D.DashStyle.Dot
            Case "点划线"
                GetLineStyleFromText = Drawing2D.DashStyle.DashDot
            Case "双点划线"
                GetLineStyleFromText = Drawing2D.DashStyle.DashDotDot
        End Select
    End Function

    '通过数值得到线型名称
    Public Function GetLineStyleNameFromText(ByVal strText As String) As String
        GetLineStyleNameFromText = ""
        Select Case strText
            Case "实线"
                GetLineStyleNameFromText = "实线 ─────────"
            Case "长虚线"
                GetLineStyleNameFromText = "长虚线— — — — — —"
            Case "点虚线"
                GetLineStyleNameFromText = "点虚线-----------------"
            Case "点划线"
                GetLineStyleNameFromText = "点划线— - — - — - —"
            Case "双点划线"
                GetLineStyleNameFromText = "双点划线— -- — -- — "
        End Select
    End Function

    '通过线型名称得到数值
    Public Function GetLineTextStyle(ByVal strText As String) As Drawing2D.DashStyle
        GetLineTextStyle = Drawing2D.DashStyle.Solid
        Select Case strText.Trim.Substring(0, 2)
            Case "实线"
                GetLineTextStyle = Drawing2D.DashStyle.Solid
            Case "长虚"
                GetLineTextStyle = Drawing2D.DashStyle.Dash
            Case "点虚"
                GetLineTextStyle = Drawing2D.DashStyle.Dot
            Case "点划"
                GetLineTextStyle = Drawing2D.DashStyle.DashDot
            Case "双点"
                GetLineTextStyle = Drawing2D.DashStyle.DashDotDot
        End Select
    End Function

    '通过线型名称得到数值
    Public Function GetLineTextNameFromStyle(ByVal strText As String) As String
        GetLineTextNameFromStyle = ""
        If strText.Trim.Length >= 2 Then
            Select Case strText.Trim.Substring(0, 2)
                Case "实线"
                    GetLineTextNameFromStyle = "实线"
                Case "长虚"
                    GetLineTextNameFromStyle = "长虚线"
                Case "点虚"
                    GetLineTextNameFromStyle = "点虚线"
                Case "点划"
                    GetLineTextNameFromStyle = "点划线"
                Case "双点"
                    GetLineTextNameFromStyle = "双点划线"
            End Select
        End If
    End Function
    '通过线型得到数值
    Public Function GetLineStyleTextFromStyleName(ByVal strText As Drawing2D.DashStyle) As String
        GetLineStyleTextFromStyleName = ""
        Select Case strText
            Case Drawing2D.DashStyle.Solid
                GetLineStyleTextFromStyleName = "实线"
            Case Drawing2D.DashStyle.Dash
                GetLineStyleTextFromStyleName = "长虚线"
            Case Drawing2D.DashStyle.Dot
                GetLineStyleTextFromStyleName = "点虚线"
            Case Drawing2D.DashStyle.DashDot
                GetLineStyleTextFromStyleName = "点划线"
            Case Drawing2D.DashStyle.DashDotDot
                GetLineStyleTextFromStyleName = "双点划线"
        End Select
    End Function
    '通过车次得到CheDiID
    Public Function CheCiToCheDiID(ByVal nCheCi As Integer) As Integer
        Dim i As Integer
        Dim j As Integer
        CheCiToCheDiID = 0
        For j = 1 To UBound(ChediInfo)
            For i = 1 To UBound(ChediInfo(j).nLinkTrain)
                If ChediInfo(j).nLinkTrain(i) = nCheCi Then
                    CheCiToCheDiID = j
                    Exit Function
                End If
            Next i
        Next j
    End Function

    '通过车次得到CheDiID
    Public Function CSCheCiToCheDiID(ByVal nCheCi As Integer) As Integer
        Dim i As Integer
        Dim j As Integer
        CSCheCiToCheDiID = 0
        For j = 1 To UBound(CSchediInfo)
            For i = 1 To UBound(CSchediInfo(j).nLinkTrain)
                If CSchediInfo(j).nLinkTrain(i) = nCheCi Then
                    CSCheCiToCheDiID = j
                    Exit Function
                End If
            Next i
        Next j
    End Function

    '通过车次得到折返列车车次
    Public Function CSCheCiToReturnCheCi(ByVal nCheCi As Integer) As Integer
        Dim i As Integer
        Dim j As Integer
        CSCheCiToReturnCheCi = 0
        For j = 1 To UBound(CSchediInfo)
            For i = 1 To UBound(CSchediInfo(j).nLinkTrain)
                If CSchediInfo(j).nLinkTrain(i) = nCheCi Then
                    If i < UBound(CSchediInfo(j).nLinkTrain) Then
                        CSCheCiToReturnCheCi = CSchediInfo(j).nLinkTrain(i + 1)
                        Exit Function
                    End If
                End If
            Next i
        Next j
    End Function

    ''' <summary>
    ''' 通过车次得到DriverID
    ''' </summary>
    ''' <param name="nCheCi"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheCiToDriverID(ByVal nCheCi As Integer) As Integer
        Dim i As Integer
        Dim j As Integer
        CheCiToDriverID = 0
        If CSTrainsAndDrivers.CSDrivers Is Nothing = True Then
        Else
            Dim beixuan As New List(Of Integer)
            For j = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain)
                    If CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain(i).CSTrainID = nCheCi And CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain(i).IsDeadHeading = False Then
                        CheCiToDriverID = j
                        Exit Function
                    End If
                Next i
            Next j
        End If
    End Function

    '将列车从车底连中断开
    Public Sub DeleteDriverLink(ByVal nCSLinkTrain As Integer, ByVal nDriverID As Integer)
        Dim i As Integer
        Dim flag As Integer
        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers(nDriverID).CSLinkTrain)
            If CSTrainsAndDrivers.CSDrivers(nDriverID).CSLinkTrain(i).CSTrainID = nCSLinkTrain Then
                flag = i
                Exit For
            End If
        Next i
        If flag > 0 Then
            '产生一个新司机
            ReDim Preserve CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers) + 1)
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)) = New CSDriver()
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID = UBound(CSTrainsAndDrivers.CSDrivers)
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSdriverNo = CSTrainsAndDrivers.CSDrivers(nDriverID).CSdriverNo & "_2"
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).DutySort = CSTrainsAndDrivers.CSDrivers(nDriverID).DutySort
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).State = "班中"
            ReDim CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSLinkTrain(0)

            For i = flag To UBound(CSTrainsAndDrivers.CSDrivers(nDriverID).CSLinkTrain)
                ReDim Preserve CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSLinkTrain) + 1)
                CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSLinkTrain)) = CSTrainsAndDrivers.CSDrivers(nDriverID).CSLinkTrain(i)
            Next

            Select Case CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).DutySort
                Case "早班"
                    CSTrainsAndDrivers.MorningDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
                Case "白班"
                    CSTrainsAndDrivers.DayDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
                Case "日勤班"
                    CSTrainsAndDrivers.CDayDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
                Case "夜班"
                    CSTrainsAndDrivers.NightDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
                Case Else
                    CSTrainsAndDrivers.OtherDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
            End Select

        End If
        '除去断裂后的后面列车
        CSTrainsAndDrivers.CSDrivers(nDriverID).RemoveTrain(flag - 1)
    End Sub
    '合并司机
    Public Sub HeBinDriverInfo(ByVal nDriverID1 As Integer, ByVal nDriverID2 As Integer, ByVal nCSLinkTrain1 As Integer, ByVal nCSLinkTrain2 As Integer)
        '前面是seek，第二个司机的任务给第一个司机
        Dim i As Integer
        Dim nTime1 As Integer
        Dim nTime2 As Integer
        nTime1 = CSTrainsAndDrivers.CSLinkTrains(nCSLinkTrain1).StartTime
        nTime2 = CSTrainsAndDrivers.CSLinkTrains(nCSLinkTrain2).StartTime
        nTime1 = CSAddTimeOver24(nTime1)
        nTime2 = CSAddTimeOver24(nTime2)
        If nDriverID2 = 0 Then '第二个列车没有驾驶员驾驶
            If nTime2 < nTime1 Then '把列车赋给司机的第一个任务
                Dim tempmertrain As MergedCSLinkTrain = GetMergedTrain(CSTrainsAndDrivers.CSLinkTrains(nCSLinkTrain2))
                CSTrainsAndDrivers.CSDrivers(nDriverID1).AddReMergedTrain(tempmertrain)
            Else '把列车赋给司机的最后一个任务
                Dim tempmertrain As MergedCSLinkTrain = GetMergedTrain(CSTrainsAndDrivers.CSLinkTrains(nCSLinkTrain2))
                CSTrainsAndDrivers.CSDrivers(nDriverID1).AddMergedTrain(tempmertrain)
            End If
            CSTrainsAndDrivers.CSLinkTrains(nCSLinkTrain2).IsLinked = True
        ElseIf nDriverID2 > 0 Then  '第二个列车有驾驶员驾驶
            If nTime2 < nTime1 Then '把seek司机的任务接到选中司机的后面
                For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers(nDriverID1).CSLinkTrain)
                    ReDim Preserve CSTrainsAndDrivers.CSDrivers(nDriverID2).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(nDriverID2).CSLinkTrain) + 1)
                    CSTrainsAndDrivers.CSDrivers(nDriverID2).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(nDriverID2).CSLinkTrain)) = CSTrainsAndDrivers.CSDrivers(nDriverID1).CSLinkTrain(i)
                Next
                Call RemoveDriver(nDriverID1)
            Else '接后把选中的司机的任务并给seek司机
                For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers(nDriverID2).CSLinkTrain)
                    ReDim Preserve CSTrainsAndDrivers.CSDrivers(nDriverID1).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(nDriverID1).CSLinkTrain) + 1)
                    CSTrainsAndDrivers.CSDrivers(nDriverID1).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(nDriverID1).CSLinkTrain)) = CSTrainsAndDrivers.CSDrivers(nDriverID2).CSLinkTrain(i)
                Next
                Call RemoveDriver(nDriverID2)
            End If
        End If
    End Sub

    Public Sub RemoveCSDriverFromList(ByVal Dri As CSDriver)
        CSTrainsAndDrivers.MorningDrivers.Remove(Dri)
        CSTrainsAndDrivers.DayDrivers.Remove(Dri)
        CSTrainsAndDrivers.CDayDrivers.Remove(Dri)
        CSTrainsAndDrivers.NightDrivers.Remove(Dri)
        CSTrainsAndDrivers.OtherDrivers.Remove(Dri)
    End Sub

    '新增一个临时驾驶员
    Public Function AddNewDriverinfor(ByVal sdutysort As String) As Integer
        AddNewDriverinfor = 0
        Dim i As Integer
        Dim nCSDriverID As Integer
        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
            If UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain) = 0 And CSTrainsAndDrivers.CSDrivers(i).DutySort = sdutysort Then
                AddNewDriverinfor = i
                Exit For
            End If
        Next
        If AddNewDriverinfor = 0 Then
            nCSDriverID = UBound(CSTrainsAndDrivers.CSDrivers) + 1
            ReDim Preserve CSTrainsAndDrivers.CSDrivers(nCSDriverID)
            CSTrainsAndDrivers.CSDrivers(nCSDriverID).CSDriverID = nCSDriverID '要注意
            CSTrainsAndDrivers.CSDrivers(nCSDriverID).CSdriverNo = "#"
            CSTrainsAndDrivers.CSDrivers(nCSDriverID).DutySort = sdutysort
            'Call CopyCheDiInformation(nCheDINum, nCheDINum)
            AddNewDriverinfor = nCSDriverID
        End If
    End Function

    ' 修改0-4点间时间段的时间值，加上24小时
    Public Function AddLitterTime(ByVal sTime As Long) As Long
        If sTime = -1 Then
            AddLitterTime = sTime
        Else
            If sTime >= 0 And sTime < CSTimeTablePara.TimeTableDiagramPara.intCompareFirstTime Then
                AddLitterTime = sTime + 24 * 3600.0#
            Else
                AddLitterTime = sTime
            End If
        End If
    End Function
End Module
