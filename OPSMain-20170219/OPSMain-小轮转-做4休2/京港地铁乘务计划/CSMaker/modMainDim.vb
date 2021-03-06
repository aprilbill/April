Imports System.Collections.Generic
Public Module modMainDim

    'Public CurrentUserRole As String '权限管理

    Public StationInf() As typeStationInformation

    Public CSLinkStationInf() As typeStationInformation
    Public DiTuStructureStaInf() As typeStationInformation

    Public strCon As String '= "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"

    Public TrainInf() As typeTrainInformation
    '*************************************************************************
    Public CSTrainInf() As typeTrainInformation
    Public CSDriverTrainInf() As typeTrainInformation
    '*************************************************************************

    Public tempFirstTrainInf As typeTrainInformation '存临时列车时刻
    Public CopyTrainInf() As typeTrainInformation '复制列车用
    Public copyChediInf() As typePublicChediInformation '复制列车用

    Public BaseTrainInf() As typeTrainInformation
    Public BasicTrainInf() As typeBasicTrainInformation

    Public ChediInfo() As typePublicChediInformation
    'Public CheDiInfBySeq() As typePublicChediInformation '按发点排好序的Chediinfo
    '*************************************************************************
    'Public CSWDTTChediInfo() As typePublicChediInformation
    'Public CSWETTChediInfo() As typePublicChediInformation
    '*************************************************************************
    Public NotSameStationInf() As String '不相同的底图车站名
    Public CSNotSameStationInf() As String
    'Public sysMenuState As String '菜单栏状态判断
    Public CurLineName As String = ""

    'Public PubDaoDatabase As dao.Recordset


    Public tmpPubShuZhu() As Integer '临时数组
    Public tmpStrPubShuZhu() As String  '临时数组

    Public notSameStationInfo As New List(Of List(Of String)) '异站衔接中不同站名视为相同

    Structure typeStopScale
        Dim nID As Integer '标尺编号
        Dim sName As String '标尺名
        Dim nDownStopTime As Integer '下行停站时分
        Dim nUpStopTime As Integer '上行停站时分
    End Structure


    '底图车站
    Structure typeStationInformation
        'Dim sHstationName As String       '后方站名称
        Dim nID As Integer               '序号
        Dim sStationSeq As String  '站序
        Dim sAtLineName As String           '车站所在的线路名
        Dim sStationName As String        '车站名称
        Dim sPrintStaName As String      '输出站名
        Dim sEnglishName As String        '英文名称
        Dim sEnglishBriName As String        '英文简称
        'Dim sPrnStationName As String
        'Dim sQstationName As String       '前方站名称
        Dim sStaStyle As String           '车站类型，有可能为线路所
        Dim sStationProp As String        'ChaStProp(Trim(Czlx("备注")), Trim(Czlx("车站性质")))
        Dim sStaProperity As String        '车站性质，是否为分岔站
        'Dim nLineNumber As Integer        '线路编号
        'Dim sLineName() As String         '??? **这个车站位于哪条线上
        'Dim nLinkState As Integer         'ChaLinkSta(Trim(Czlx("接入状态")))
        Dim nStLineNum As Integer         '股道数量
        Dim sStLineNo() As String         '股道或道岔编号
        Dim nStLineUse() As Integer       'ChaLineUse(Trim(Czk("股道用途")), Czk("客运作业"), Czk("货物作业"), Czk("超限"), Trim(Czk("方向")))(Trim(Czk("股道用途")), Czk("客运作业"), Czk("货物作业"), Czk("超限"), Trim(Czk("方向")))
        Dim sUpOrDownUse() As String      '股道上下行使用方式
        Dim sLineUse() As String          '股道用途，04.04.03加上

        Dim sGuDaoUseSeq() As String '股道使用顺序
        Dim sGuDaoName() As String '股道名称，代号用于时刻表输入

        'Dim sGuDaoStyle() As String '股道类型
        'Dim sGuDaoYongTu() As String '股道用途

        'Dim sTSuse() As String            '股道特殊用途，如站前折返要求

        Dim Ycord As Single '对应底图结构的Y坐标比例
        Dim bIfShowInCheDiJiaoLuTu As Boolean '是否在交路图上显示
        Dim YPicValue As Single '对应的底图的Y坐标


        Dim IKK() As Long               '客客时间间隔
        Dim IKH() As Long               '客货时间间隔
        Dim IHK() As Long               '货客时间间隔
        Dim IHH() As Long               '货货时间间隔

        Dim lTaoBu() As Long             '不同时到达间隔 1是下行，2是上行
        Dim lTaoHui() As Long            '会车间隔
        Dim lTaoLian1() As Long          '连发间隔
        Dim lTaoLian2() As Long          '连发间隔
        Dim lTaoTongK() As Long          '不同时通过间隔（客）
        Dim lTaoTongH() As Long          '不同时通过间隔（货）
        Dim lTaoFaDao() As Long          '不同时发到间隔
        Dim lTaoDaoFa() As Long          '不同时到发间隔
        Dim lTaoFaFa() As Long           '不同时发发间隔
        Dim lTaoDaoDao() As Long         '不同时到到间隔

        '******************** 进路信息

        'Dim PathLinkTrack() As String         '到达方向，轨道编号
        'Dim PathLinkSta() As String         '来源方向,车站名
        'Dim PathTrackNum() As String       '停放或通过的股道编号
        'Dim PathPathTrackID() As String       '基本进路,通过的轨道编号
        'Dim PathCrossNum() As String     '通过的道岔号
        'Dim PathControlNum() As String            '通过的控制分区

        Dim sTrackUse() As typeStationTrackUseinf '车站股道使用
        Dim sCrossUse() As typeCrossingPathInf '车站进路信息


        '************************ 分岔站股道使用

        'Dim sGDFromSta() As String               '参与进路判断的分岔站的衔接站名
        'Dim sGDToSta() As String                 '参与进路判断的分岔站的衔接站名

        'Dim sGDDaoFaBasicJinLu() As String   '到发基本进路，指可用的股道编号
        'Dim sGDDaoFaKeXuanJinLu() As String  '到发可选进路，指可用的股道编号
        'Dim sGDPassBasicJinLu() As String    '通过基本进路，指可用的股道编号
        'Dim sGDPassKeXuanJinLu() As String   '通过可选进路，指可用的股道编号

        '=======================================显示股道使用信息==============================

        Dim StaNamePosRec As Rectangle  '车站名位置所在矩形
        Dim IsExpress As Boolean       '表示此车站股道是否在运行图中展开
        Dim IsVirtul As Boolean      '表示是否为虚拟车站

        '===================================================================================
        Dim StopScale() As typeStopScale        '停站标尺


    End Structure

    Structure typeTrainInformation
        Dim sJiaoLuName As String       '列车交路名称
        Dim sRunScaleName As String     '列车运行标尺名
        Dim sStopSclaeName As String    '列车停站标尺名

        Dim nTrain As Integer           '在当前数组中位置
        Dim Train As String             '车次
        Dim TrainClass As Integer       '级别
        Dim TrainStyle As String        '列车种类
        Dim TrainKind As String         '客货种类
        Dim StartStation As String      '起始站
        Dim EndStation As String        '终点站
        Dim ComeStation As String       '接入站
        Dim ComeLine As String          '接入线路
        Dim NextStation As String       '交出站
        Dim NextLine As String          '交出线路
        Dim nTrainTimeKind As String   '列车时分标尺
        Dim sTrainTimeScale As String   '列车标尺类型
        Dim sPrintTrain As String        '输出车次

        Dim sLineNum As String          '线路编号
        Dim sMuDiNum As String        '目的符 


        Dim nStartStaTime As Integer '始发站时间
        Dim nArriStaTime As Integer '终到站终到时间

        Dim BeginTrain As String        '货车起始车次
        Dim TrainNumber As Integer      '货车开行数量

        Dim nChaRunDirection As Integer '列车改变运行方向和车次后存放列车信息的数组维数 '不变为9999
        Dim nLinkTrainNum As Integer  '变完后的数组维数 不变为0

        Dim sTrainUsageZD As String       '终到车底、单机的运用方式
        Dim sTrainBeizhuZD As String      '终到回送客车底或单机的衔接车次
        Dim sTrainUsageSF As String       '始发车底、单机的运用方式
        Dim sTrainBeizhuSF As String      '始发回送客车底或单机的衔接车次

        Dim TrainPuorNot As Integer     '列车是否已经铺画过 '初为0
        Dim TrainOkorNot As Integer     '列车运行线的确定'初为0
        Dim TrainClassCal As Integer    '参与计算的列车等级 =TrainClass

        'Dim nTrainReturnStyle() As Integer '列车在始发、终到站的折返方式,分出库（1），入库（2），站前立即折返（3），转线立即折返（4），站后立即折返（5）等
        'Dim nTrainReturn() As Integer      '列车在终到、始发站的折返车次
        'Dim nTrainContinue() As Integer    '列车在终到、始发站的接续车次（入库、出库时的车次）
        Dim nTrainUseCheDiNum As Integer    '列车使用的车底的编号


        Dim TrainReturn() As Integer      '列车在终到、始发站的折返车次
        ' Dim TrainContinue() As Integer    '列车在终到、始发站的接续车次（入库、出库时的车次）
        'TrainReturnStation(2) As String  '列车车底折返站(已取消该变量)
        Dim TrainReturnStyle() As String  '列车车底折返方式
        Dim nCheDiPuOrNot As Integer '车底是否已经勾画上，初为0


        Dim nPuOrNot As Integer '是否参与铺画，所有车底铺画时用到
        Dim nCDPuOrNot As Integer '是否已经勾画上车底，所有车底铺画时用到
        Dim nCheDiEndState As Integer '勾上车底时是否为最后一趟，初为0，所有车底铺画时用到

        Dim nLeftState As Integer '待左边勾
        Dim nRightState As Integer '待右边勾
        Dim nLinkLeft As Integer '左边接续的列车
        Dim nIfCanMove As Integer '是否能移动到发点
        Dim nZfLimit As Integer '是否折返约束

        Dim lAllStartTime As Long     '初定列车始发时间
        Dim lAllEndTime As Long      '初定列车结束时间
        Dim nstopSta() As Integer     '停车站ID
        Dim NumStop As Integer          '停车次数
        Dim StopStation() As String     '停车站名
        Dim stopTime() As Long        '停车时间

        Dim NumWay As Integer           '列车经过的分岔站数
        Dim Way1() As String            '列车经过的分岔站名
        Dim Way2() As String            '列车经过分岔站的后一站
        Dim Way3() As String            '列车经过分岔站的前一站
        Dim Way4() As String            '列车经过分岔站后是否改变方向
        Dim Way5() As String            '列车经过分岔站后改变的新车次

        Dim sTrainPath As String          '列车径路,用"，"分开
        Dim nPassSection() As Integer     '列车经过的区间编号
        Dim SectionRunTime() As Long      '区间运行时分
        Dim sSectionName() As String      '区间名称
        'Dim StrFirstSta() As String       '列车在该区区间的起始站名
        'Dim StrSecondSta() As String      '列车在该区间的下一个站名，按列车的运行方向
        Dim nFirstID() As Integer          '列车在该区区间的起始站名对应在底图上的ID号
        Dim nSecondID() As Integer         '列车在该区间的下一个站名对应在底图上的ID号
        Dim nPathID() As Integer           '列车通过的ID
        Dim sPathSta() As String        '通过的车站名

        '运行线在底图上的特征
        Dim bIfPassFenLine As Integer '是否过分隔线（最左边时间线）,1为是，0为不是
        Dim sngPassLineX1Coord As Integer '过线的X1坐标
        Dim sngPassLineY1Coord As Integer '过线的Y1坐标
        Dim sngPassLineX2Coord As Integer '过线的X2坐标
        Dim sngPassLineY2Coord As Integer '过线的Y2坐标
        Dim sngFirXcoord() As Single  '在底图区间第一个车站的X坐标
        Dim sngFirYcoord() As Single '在底图区间第一个车站的Y坐标
        Dim sngSecXcoord() As Single '在底图区间第二个车站的X坐标
        Dim sngSecYcoord() As Single '在底图区间第二个车站的Y坐标

        Dim lChaRunTime() As Long       '列车区间运行时分增减时分
        Dim StopLine() As String        '停车股道
        Dim StopLineTime() As Long      '实际停车时间
        Dim Starting() As Long          '出发时间 '初始-1
        Dim Arrival() As Long           '到达时间末‘初始为-1
        Dim sTrainBeizhu As String      '备注

        Dim sIfBeiChe As Integer       '是否为备车，1表示是，0表示不是
        Dim BeiCheState As String       '备车状态（进备车线或出备车线）

        '折返信息
        Dim sStartZFLine As String     '始发折返股道
        Dim sStartZFStarting As Long     '始发起始折返时间
        Dim sStartZFArrival As Long  '始发终止折返时间
        Dim sEndZFLine As String       '终到折返股道
        Dim sEndZFStarting As Long     '终到起始折返时间
        Dim sEndZFArrival As Long    '终到终止折返时间

        Dim sTrainLeiXing As String      '列车类型，用数表示，相同表示是同类列车。

        Dim sJiCheLeiXing As String     '牵引机车类型
        Dim SCheDiLeiXing As String     '车底类型

        Dim nIfFixedCheDi As Integer    '是否固定车底，1表示是，0表示否
        Dim nIfEnterGZSta As Integer    '该列车是否进广州站，1表示是，0表示否
        Dim nCheDiLineHeight As Long    '车底线的高度

        Dim sTrainXingZhi As String        '列车性质

        Dim PrintLineStyle As Drawing2D.DashStyle   '列车打印时的线型
        Dim PrintLineColor As Color  '列车打印线的颜色
        Dim PrintLineWidth As Single '列车打印线的宽度

        Dim PassCrossing() As typePassCrossing '通过道岔号
        Dim sIfFixTime As String '是否固定时刻


    End Structure

    Structure StopBiaoChi '停站标尺
        Dim sName As String '种类名
        Dim sScaleName() As String '标尺名
        Dim nStopNum As Integer
        Dim nStopStation() As String
        Dim StopTime() As String
    End Structure

    Structure RunBiaoChi '运行标尺
        Dim sName As String '种类名
        Dim sScaleName() As String '标尺名
        Dim nSecID() As Integer
        'Dim sFirStaName() As String
        'Dim sSecStaName() As String
        Dim RunTime() As Long '运行时分
    End Structure

    'Public TrainSecScale() As RunBiaoChi '在添加交路窗口中用到
    'Public TrainStopScale() As StopBiaoChi '在添加交路窗口中用到

    Structure typeBasicTrainInformation
        Dim sJiaoLuName As String       '交路名称
        Dim nUporDown As Integer        '上下行，1为下行，2为上行
        Dim TrainStyle As String        '列车种类
        Dim StartStation As String      '起始站
        Dim EndStation As String        '终点站
        Dim ComeStation As String       '接入站
        Dim NextStation As String       '交出站
        Dim TrainReturnStyle() As String  '列车车底折返方式

        Dim sLineNum As String          '线路编号
        Dim sMuDiNum As String        '目的符

        Dim StopScale() As StopBiaoChi     '停站标尺
        Dim SecScale() As RunBiaoChi       '区间运行时分标尺

        Dim sTrainPath As String          '列车径路,用"，"分开
        Dim nPassSection() As Integer     '列车经过的区间编号
        Dim sSectionName() As String      '区间名称
        Dim StrFirstSta() As String       '列车在该区区间的起始站名
        Dim StrSecondSta() As String      '列车在该区间的下一个站名，按列车的运行方向
        Dim nFirstID() As Integer          '列车在该区区间的起始站名对应在底图上的ID号
        Dim nSecondID() As Integer         '列车在该区间的下一个站名对应在底图上的ID号
        Dim nPathID() As Integer           '列车通过的ID

        Dim Starting() As Long          '出发时间 '初始-1
        Dim Arrival() As Long           '到达时间末‘初始为-1
        Dim sTrainBeizhu As String      '备注

        Dim NumWay As Integer           '列车经过的分岔站数
        Dim Way1() As String            '列车经过的分岔站名
        Dim Way2() As String            '列车经过分岔站的后一站
        Dim Way3() As String            '列车经过分岔站的前一站
        Dim Way4() As String            '列车经过分岔站后是否改变方向
        Dim Way5() As String            '列车经过分岔站后改变的新车次

        Dim SCheDiLeiXing As String     '车底类型
        Dim sTrainXingZhi As String        '列车性质
    End Structure
    Structure typePublicChediInformation

        Dim SCheDiLeiXing As String '车底类型名
        Dim sCheDiID As String
        Dim sChediName As String
        Dim bIfAutoResetCheCi As Boolean
        Dim sStation As String  '车底归属的车站
        Dim sDayBeginStation As String '每天开始出发的车站
        Dim sDayEndStation As String '每日运行结束停留的车站
        Dim sRukuZhouqi As String ' 入段周期
        Dim lYunyongTime As Long '运用时间
        Dim nYunxingBiaochi As Integer '运行时分标尺
        Dim nDayItem() As Integer '连接天次
        Dim nLinkTrain() As Integer   '连接车次
        Dim bIfEnterGZ() As String  '该车次是否进广州，1表示是，0表示不是
        Dim sChuKuTime As Single '出库时间
        Dim sChuKuFangShi As String '出库方式
        Dim sRuKuTime As Single '入库时间
        Dim sRuKuFangShi As String  '入库方式
        Dim sChuKuSta As String '出库车站名，针对固定时刻
        Dim sRuKuSta As String '入库车站名，针对固定时刻
        Dim sBeiZhu As String '备注
        Dim bIfGouWang As Boolean  '是否已经勾完，0表示没有，1表示勾完
        Dim sYunYongFangShi As String '运用方式，指广深车底，广九车底，固定时刻

        Dim nIfFixCheDi As String '是否固定车底的运用方式,1表示是，0表示不是, 2表示部分固定

        Dim sTrainXingZhi As String '挂上的列车性质

        Dim sAfterCheDiID As String '第二天的接续车底ID号
        Dim nAfterCheDiID As Integer '第二天的接续车底ID号
        Dim sBeforCheDiID As String '前一天的接续车底ID号
        Dim nBeforCheDiID As Integer '前一天的接续车底ID号

        Dim sCheCiHao As String '车底对应的车次号，根据出库顺序定义
        'Dim sPrintCheCiHao As String '车次号，输出时用

        Dim nIfDrawCheDiLine As Integer '是否已经画好了车底线

        Dim PrintCheDiLinkColor As Color   '车底联接线的颜色
        Dim PrintCheDiLinkWidth As Integer  '车底联接线的线宽
        Dim PrintCheDiLinkStyle As String '车底联接线的型状

        Dim ChukuTime As Integer
        Dim RukuTime As Integer
    End Structure

    Public BaseChediInfo() As typePublicChediInformation
    Public CSchediInfo() As typePublicChediInformation


    Structure ChediZhefanBiaozhun
        Dim SCheDiLeiXing As String  '车底类型
        Dim sZhefanStation As String  '车底折返的车站
        Dim lLijiZhefanTime As Long  '立即折返时间
        'Dim lTingliuZhefanTime As Long '停留折返时间
        'Dim lZhuanxianZhefanTime As Long '转线折返时间
        'Dim lRukuTime As Long '入库所需时间
        'Dim lChukuTime As Long '出库所需时间
        'Dim lZaikuStopTime As Long '在库内停留时间
        Dim lZhanQianTime As Long '站前折返时间
        Dim lZhanHouTime As Long '站后折返时间
        Dim intStartZYTime As String '始发作业时间
        Dim intEndZYTime As String '终到作业时间
        Dim nFirRunTime As Long '到达后运行至折返线的时间
        Dim nSecRunTime As Long '折返线运行至出发股道的时间
        Dim nArrFaDaoTime As Long '到达时的发到间隔
        Dim nStartFaDaoTime As Long '出发时的发到间隔
        Dim sBeiZhu As String '备注
    End Structure
    Public ChediZhefanBiaozhunInfo() As ChediZhefanBiaozhun
    Public TrainZYInf() As ChediZhefanBiaozhun

    Structure TrainRunScaleInformation
        Dim nScaleID As Integer '标尺ID
        Dim sScaleName As String '标尺名
        Dim sMemo As String '标尺属性
    End Structure
    Public TrainRunScaleInf() As TrainRunScaleInformation

    Structure SkbStnSequance '打印用
        'Dim sSKBPathName As String
        'Dim sSKBName As String
        'Dim sLineName As String             '线路名称
        Dim sQDName As String               '区段名称
        Dim nStnSeq() As Integer            '车站序列
        'Dim nXKTrainNum As Integer          '下行客车数量
        'Dim nSKTrainNum As Integer          '上行客车数量
        'Dim nXHTrainNum As Integer          '下行货车数量
        'Dim nSHTrainNum As Integer          '上行货车数量
        'Dim sXKTrain As String              '下行客车(全部、部分)
        'Dim sSKTrain As String              '上行客车(全部、部分)
        'Dim sXHTrain As String              '下行货车(全部、部分)
        'Dim sSHTrain As String              '上行货车(全部、部分)
        'Dim nCheDiSeq() As Integer
        'Dim nKXTrnSeq() As Integer          '下行客车序列
        'Dim nKSTrnSeq() As Integer          '上行客车序列
        'Dim nHXTrnSeq() As Integer          '下行货车序列
        'Dim nHSTrnSeq() As Integer          '上行货车序列
        'Dim sKXTrnSeq() As String           '下行客车序列
        'Dim sKSTrnSeq() As String          '上行客车序列
        'Dim sHXTrnSeq() As String          '下行货车序列
        'Dim sHSTrnSeq() As String          '上行货车序列
        'Dim nIfPrint As Integer             '本区段是否打印
        'Dim nChukuSeq() As Integer
        'Dim nRukuSeq() As Integer
    End Structure
    Public SkbStnSeq() As SkbStnSequance

    Structure typePassCrossing
        Dim nSta As Integer
        Dim nFirstPassNum() As String
        Dim nFirstPassTime() As Integer
        Dim nSecondPassNum() As String
        Dim nSecondPassTime() As Integer
    End Structure

    '车站股道使用顺序，与交路关联
    Structure typeStationTrackUseinf
        Dim sJiaoLuName As String
        Dim sStaName As String
        Dim sStartUse() As String
        Dim sEndUse() As String
        Dim sReturnUse() As String
        Dim sDownStopUse() As String
        Dim sUpStopUse() As String
        Dim sDownPassUse() As String
        Dim sUpPassUse() As String
    End Structure
    Public staTrackUseInf() As typeStationTrackUseinf

    Structure typeCrossingPathInf
        Dim LinkTrackNum As String         '到达方向，轨道编号
        Dim LinkStaName As String         '来源方向,车站名
        Dim StaTrackNum As String       '停放或通过的股道编号
        Dim PathTrackNum As System.Collections.Generic.List(Of String)       '基本进路,通过的轨道编号
        Dim PathCrossNum As System.Collections.Generic.List(Of String)    '通过的道岔号
        Dim PathControlNum As System.Collections.Generic.List(Of String)            '通过的控制分区
    End Structure



End Module
