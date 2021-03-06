Module ModNetMainDim

    Public g_LineName As String '当前选中的线路名称
    Public g_StationName As String '当前选中的车站名
    Public g_SeekLineName As String '查询时当前选中的线路名称
    Public g_SeekStationName As String '查询时当前选中的车站名
    Public g_SeekStaExitNumber As String '查询时当前选中的车站的出口编号

    Public g_ListString() As String '字符串分解的公用数组变量
    Public g_IfEditStationCord As Boolean '是否修改车站的坐标 


    'frminputbox专用 #############################

    Public StrInputBoxText As String '保存frminputbox返回的Text的值，临时用
    Public StrInputBoxCombText As String '保存frminputbox返回的Text的值，临时用
    Public bCancelInput As Integer    '是否取消当前的操作，用在frmInputbox中,非0表示取消

    '###############################################################


    Structure stuReturnValue '函数返回时赋予的临时值
        Dim strValue1 As String
        Dim strValue2 As String
        Dim strValue3 As String
    End Structure
    Public ReturnValue As stuReturnValue
    Public GetReturnValue() As String
    ''定义线路与车站信息结构
    'Public Structure stLineStaInformation
    '    Dim strLineName As String
    '    Dim strStaName() As String
    'End Structure

    '********************************属性输入窗口设置
    Enum PropStrStyle As Integer
        TexBox = 1
        ComBox = 2
        SelectBox = 3
        colorBox = 4
    End Enum
    Enum TextCriterion As Integer
        NotRequired = 0 '没有要求
        NotEmpty = 1 '不能为空
        OnlyNumber = 2 '只能为数据类型
    End Enum
    Structure stulistItemProperitys
        Dim strItem As String '该项目的名称
        Dim strItemCriterion As TextCriterion '该项目的规范，不能为空，只能为数字等属性
        Dim strStyle As PropStrStyle '是Text框还是comb框
        Dim strTxtList As String '默认值
        Dim StrCmbList() As String 'Com框中显示的内容
        Dim strReturnValue As String '最终返回的值
    End Structure
    Public stuListItem() As stulistItemProperitys

    '*********************************************************
    Structure typeTicketRule
        Dim nFir As Single
        Dim nSec As Single
        Dim nValue As Single
    End Structure
    Public TicketRule() As typeTicketRule

    Structure typeFontTextformation '文字信息
        Dim nID As Integer '序号，自动
        Dim sStaName As String
        Dim nNum As Integer
        Dim sText As String '文字内容
        Dim FontName As String
        Dim FontSize As Single
        Dim Italic As Boolean
        Dim Bold As Boolean
        Dim FontColor As Color
        Dim X As Single
        Dim Y As Single
        Dim sMemo As String
        Dim nDelete As Integer
    End Structure
    Structure typePlatFormInformation '站台信息
        Dim nID As Integer '序号，自动
        Dim sStaName As String
        Dim nNum As Integer
        Dim sStyle As String
        Dim nWidth As Single
        Dim nHeight As Single
        Dim X1 As Single
        Dim Y1 As Single
        Dim X2 As Single
        Dim Y2 As Single
        Dim sMemo As String
    End Structure
    Structure typeSignalInformation '信号机信息
        Dim nID As Integer '序号，自动
        Dim sStaName As String
        Dim nNum As Integer
        Dim sStyle As String
        Dim nTrackNum As String
        Dim nCrossNum As Integer
        Dim X As Single
        Dim Y As Single
        Dim sMemo As String
    End Structure

    Structure typeTrackInformation '线段信息
        Dim nID As Integer '序号，自动
        Dim sStaName As String
        Dim nStaID As Integer
        Dim nNum As Integer
        Dim sTrackCircuitNum As String '轨道电路编号
        Dim sStyle As String '轨道类型：道岔，连接线，股道
        Dim sGuDaoStyle As String '股道类型：正线，到发线，折返线，存车线
        Dim sGuDaoYongTu As String '股道用途：只停折返列车，只停通过停站列车
        Dim sGuDaoUseSeq As String '股道使用顺序：股道被占用时使用的顺序
        Dim sngLength As Single
        Dim sTrackNum As String '股道或道岔编号
        Dim sControlNum As String '控制模块,磁浮系统中
        Dim X1 As Integer
        Dim Y1 As Integer
        Dim X2 As Integer
        Dim Y2 As Integer
        Dim sLeftLink1 As String
        Dim sLeftLink2 As String
        Dim sLeftLink3 As String
        Dim sRightLink1 As String
        Dim sRightLink2 As String
        Dim sRightLink3 As String
        Dim sMemo As String
        ' Dim sColor As Color '显示颜色

        Dim TrackOcupyFirTime() As Long
        Dim TrackOcuPySecTime() As Long
        Dim nDelete As Integer
    End Structure

    '车站控制方案
    Structure typeStaControSchemeSet
        Dim sStaName As String
        Dim sSchemeName As String
        Dim stringTrackNum As String
        Dim STrackNum() As String
        Dim SModelName() As String
    End Structure

    Structure typeStaInf
        Dim nID As Integer '序号
        Dim nDownID As Integer '下行站序
        Dim sStaName As String '车站名
        Dim sPrintStaName As String '输出站名
        Dim sEngName As String '英文站名
        Dim sEngBrriName As String '英文简称
        Dim sStaCode As String '车站代码
        Dim sSingleOrDoubleLine As String '单双线
        Dim sStaStyle As String ' 车站类型
        'Dim lngDownLength As Single '下行程标
        'Dim lngUpLength As Single '上行程标
        'Dim lngStaLength As Single '车站长度
        'Dim intPasserTrackNumber As Integer '旅客到发线数
        'Dim intFreightTrackNumber As Integer '货物到发线数
        'Dim intPlatformNumber As Integer '站台数
        Dim sngXcoord As Single 'X坐标
        Dim sngYcoord As Single 'Y坐标
        Dim tmpX As Single '临时X坐标
        Dim tmpY As Single '临时Y坐标
        Dim sStaProperty As String '车站性质，是否为分岔站等
        Dim sPicPath As String '图片路径
        Dim sMemo As String '备注
        Dim sAtLine As String
        Dim Track() As typeTrackInformation
        Dim Signal() As typeSignalInformation
        Dim PlatForm() As typePlatFormInformation
        Dim FontText() As typeFontTextformation
    End Structure

    Structure typeCADStaOrSecInf
        Dim nDownID As Integer '下行站序
        Dim sStaOrSec As String '车站还是区间
        Dim sStaName As String '车站或区间名
        Dim nLineID As Integer '对应的线路ID
        Dim sLineName As String '线路名
        Dim sStaCode As String '代码
        Dim nStaOrSecID As Integer '对应的Netinf()的ID号
        Dim sCurControlScheme As String '当前的控制方式 
        Dim Track() As typeTrackInformation
        Dim Signal() As typeSignalInformation
        Dim PlatForm() As typePlatFormInformation
        Dim FontText() As typeFontTextformation
        Dim ContolScheme() As typeStaControSchemeSet
        Dim sDownControlNum As String '车站或区间的控制模块编号
        Dim sUpControlNum As String '车站或区间的控制模块编号
    End Structure
    Public StaInf() As typeStaInf
    Public CADStaInf() As typeCADStaOrSecInf
    Public UndoCADStainf() As typeCADStaOrSecInf
    Public StaInfNotSame() As typeStaInf

    Structure typeSecInf
        Dim nID As Integer
        Dim nSecNumber As Integer '区间编号
        Dim sLineName As String '线路名称
        Dim sSecName As String
        Dim sSecCode As String
        Dim sSecFirName As String
        Dim sSecSecName As String
        Dim sSecDownLength As Single
        Dim sSecUpLength As Single
        Dim nFirStaID As Integer
        Dim nSecStaID As Integer

        Dim nHStation As Integer
        Dim nQStation As Integer

        Dim nSection As Integer           '区间性质（单1、双2线）
        Dim sBlock As String              '闭塞方式
        Dim lDistance() As Single        '区间距离

        Dim SecScale() As typeSectoinScale  '列车运行标尺

        Dim KeUpRunTime() As Long        '客上行区间运行时分
        Dim KeDownRunTime() As Long      '客下行区间运行时分
        Dim HuoUpRunTime() As Long       '货上行区间运行时分
        Dim HuoDownRunTime() As Long     '货下行区间运行时分

        Dim SecTimeSpace As typeSectionTimeSpaceCircle '运行曲线
        Dim sDownControlNum As String '控制模块编号 
        Dim sUpControlNum As String '控制模块编号 
    End Structure

    '区间时间距离曲线
    Structure typeSectionTimeSpaceCircle
        Dim DownID() As Integer '区段ID
        Dim DownLength() As Single '区段距离
        Dim DownRunTime() As Long '区段时间
        Dim UpID() As Integer '区段ID
        Dim UpLength() As Single '区段距离
        Dim UpRunTime() As Long '区段时间
    End Structure


    '列车运行标尺
    Structure typeSectoinScale
        Dim nID As Integer
        Dim sScaleName As String '标尺名称
        Dim sngDownTime As Single   '下行区间时分
        Dim sngUpTime As Single  '上行区间时分
        Dim sngDownAppendStartTime As String '下行起车时分
        Dim sngDownAppendStopTime As String '下行停车时分
        Dim sngUpAppendStartTime As String '上行起车时分
        Dim sngUpAppendStopTime As String '上行停车时分
    End Structure

    Public secInf() As typeSecInf
    Public DiTuStructureSecInf() As typeSecInf '在设置底图结构时用到
    Public SectionInf() As typeSecInf

    Structure typeLineInf
        Dim nID As Integer  'ID号
        Dim intSeq As Integer '序号
        Dim sName As String '线路名
        Dim sEngName As String '英文名称
        Dim sBrriName As String '简称
        Dim sngLength As Single  '线路长
        Dim sLineNumber As String '线路编号,对应列车车次的首位数
        Dim sMemo As String '备注
        Dim Station() As typeStaInf
        Dim Section() As typeSecInf
    End Structure
    Public LineInf() As typeLineInf
    Structure typeWholeNetinf
        Dim sWholeNetName As String
        Dim sNetFoldPath As String
        Dim sNetLine() As String
    End Structure
    Public WholeNetInf As typeWholeNetinf

    Structure typeNetInf
        Dim sNetName As String '线网名
        Dim Line() As typeLineInf
    End Structure
    Public NetInf As typeNetInf

    Structure typeSystemPara
        Dim DatabasePassword As String
        Dim sProgrameFilePath As String
        Dim sPicFilePath As String '底图图片路径
        Dim sUserCompanyName As String '使用单位名称
        Dim SystemStyle As String '系统方式，磁浮，铁路，地铁
    End Structure
    Public SystemPara As typeSystemPara

    Structure typeSectionScaleInf '列车运行标尺
        Dim sSecName As String
        Dim sScaleNum As String
        Dim sScaleName As String
        Dim sDownRunTime As String
        Dim sUpRunTime As String
    End Structure

    Structure typeStopScaleInf '列车停站标尺
        Dim sStaName As String
        Dim sScaleNum As String
        Dim sScaleName As String
        Dim sDownStopTime As String
        Dim sUpStopTime As String
        Dim sDownStartStopTime As String
        Dim sUpStartStopTime As String
        Dim sDownEndStopTime As String
        Dim sUpEndStopTime As String
    End Structure

    Public SectionScaleInf() As typeSectionScaleInf
    Public StopScaleInf() As typeStopScaleInf

    'Structure typeTrainJiaoLuInf '列车基础信息
    '    Dim sJiaoLuName As String
    '    Dim sStartSta As String
    '    Dim sEndSta As String
    '    Dim sUpOrDown As String
    'End Structure
    'Public TrainJiaoLuInf() As typeTrainJiaoLuInf

End Module
