Module ModTimetableMain

    Structure typeTimeTableDiagramPara
        Dim intDiagramFirstTime As Integer '��ͼ��ʼʱ��
        Dim intDiagramWholeTime As Integer '��ͼ��ʾȫ��ʱ��,��ͼ��ʾʱ���
        Dim intCompareFirstTime As Integer 'ʱ��Ƚ���ʼʱ��

        Dim sngPicDiagramWidth As Integer   '��ͼ��
        Dim sngPicDiagramHeight As Integer '��ͼ��
        Dim sngPicStationWidth As Integer '��վվ��ͼ��
        Dim sngPicStationHeight As Integer '��վվ��ͼ��

        Dim strTimeFormat As String '��ͼʱ�ָ�ʽ
        Dim sngtopBlank As Integer '��ͼ���¿հ׸߶�
        Dim sngTimeBlank As Integer '��ͼʱ��հ׸߶�
        Dim sngLeftBlank As Integer '��ͼ���ҿհ׸߶�
        Dim sngStaBlank As Integer '��ͼ��վ�հ׿��
        Dim sngPubLeftX As Integer '����������
        Dim sngPubTopY As Integer '���������߶�
        Dim nifPrintCheCi As Boolean '�Ƿ��ӡ����
        Dim nIfPrintXieCheCi As Boolean '�Ƿ��ӡб����
        Dim nCheDiLineHeight As Integer '�����߸߶�
        Dim sCheDiLineStyle As String '����������

        Dim sCheCiShowStyle As Integer '������ʾ��ʽ,0��ʾֻ��ʾ���Σ�1��ʾ���׺�+����
        Dim nIfPrintEveryStaName As Boolean '�Ƿ���ÿ��Сʱ��ʾ��վ��

    End Structure

    Structure typeStaDiagramPara
        Dim nStaLineHeight As Integer '��վ�ɵ��߸߶�
        Dim sCurStaName As String '��ǰ��վ
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

        Dim StaNameFontName As String '��վ��������
        Dim StaNameFontSize As Single
        Dim StaNameFontBold As Boolean
        Dim StaNameFontItalic As Boolean
        Dim StaNameFontColor As String

        Dim TimeFontName As String 'ʱ���ע����
        Dim TimeFontSize As Single
        Dim TimeFontBold As Boolean
        Dim TimeFontItalic As Boolean
        Dim TimeFontColor As String

        Dim StaLineStyle As String '����������
        Dim StaLineWidth As Single
        Dim StaLineColor As String

        Dim FenStaLineStyle As String '�ֲ�վ������
        Dim FenStaLineWidth As Single
        Dim FenStaLineColor As String

        Dim CheChangStaLineStyle As String '������
        Dim CheChangStaLineWidth As Single
        Dim CheChangStaLineColor As String

        Dim TrainLineStyle As String '�г�������
        Dim TrainLineWidth As Single
        Dim TrainLineColor As String

        Dim CheDiLineStyle As String '���׽�·��
        Dim CheDiLineWidth As Single
        Dim CheDiLineColor As String

        Dim CheCiFontName As String   '���α�ע����
        Dim CheCiFontSize As Single
        Dim CheCiFontBold As Boolean
        Dim CheCiFontItalic As Boolean
        Dim CheCiFontColor As String

        Dim XieCheCiFontName As String   'б�򳵴α�ע����
        Dim XieCheCiFontSize As Single
        Dim XieCheCiFontBold As Boolean
        Dim XieCheCiFontItalic As Boolean
        Dim XieCheCiFontColor As String


    End Structure

    '����ʱ�̱�ʱ��
    Structure typeOpenSKBSeekSeq
        Dim nTrainID As Integer
        Dim sTrainNum As String
        Dim nFirID As Integer
        Dim nSecID As Integer
    End Structure
    Public SKBseekSeq() As typeOpenSKBSeekSeq

    Structure typeTimeTableMainParameter

        Dim nPubTrain As Integer '��ǰѡ�е��г���
        Dim nPubTrains() As Integer 'ѡ����г���
        Dim nPubCheDi As Integer '��ǰѡ�еĳ��׺�
        Dim sPubTrainStrainDraw As TrainStrainDraw
        Dim sCurDiagramState As DiagramState
        Dim BifAutoBianCheCi As Boolean '�Ƿ��Զ��೵��
        Dim sPubCurSkbName As String 'ʱ�̱�����
        'Dim sPubCurSKBID As String 'ʱ�̱�ID
        Dim nMaxUndoID As Integer
        Dim nStaJiShuTuJieSeletedState As Integer
        Dim TimeTableDiagramPara As typeTimeTableDiagramPara '��ͼ����
        Dim StaDiagramePara As typeStaDiagramPara '��վ�ɵ�ͼ�����
        Dim lngCurMouseDownTime As Long '��굱ǰ���������Ӧ��ʱ��
        Dim picPubStation As PictureBox '��վ��ͼ
        Dim picPubStation2 As PictureBox '��վ��ͼ2
        Dim picPubDiagram As PictureBox '����ͼ��ͼ
        Dim picPubBack As PictureBox '����ͼ
        Dim DiagramStylePara As typeTimeTableTimeLinePara '����ͼ��ͼ�߲���
        Dim bIFAutoAddChuKuTrain As Boolean '�Ƿ���ϳ�����
        Dim bIFAutoAddRuKuTrain As Boolean '�Ƿ���������
        Dim sInputDataError As String '�����ݿ��Ƿ����
        Dim sDrawLineStyle As String '�̻�����
        Dim sTiaoLineState As String '�г�������ʽ
        Dim sErrorShowStyle As String '������ʾ��ʽ
        Dim pubTimeBmpPic As Bitmap   '���õ�ʱ�ֵ�ͼ
    End Structure
    Public TimeTablePara As typeTimeTableMainParameter

    Structure typeTimetableIndex
        Dim sSecName As String
        Dim sFirStaName As String
        Dim sSecStaName As String
        Dim nDownTrains As Integer
        Dim nUpTrans As Integer
        Dim nDownInterval As Integer
        Dim nUpInterval As Integer
        Dim nStockNumber As Integer
    End Structure

    Structure typeTimeIndex
        Dim sFirStaName As String
        Dim sSecStaName As String
        Dim nFirTime As Integer
        Dim nSecTime As Integer
        Dim TimetableIndex As Generic.List(Of typeTimetableIndex)
    End Structure
    Public TimeIndex As New Generic.List(Of typeTimeIndex)

    Enum DiagramState As Integer
        ����ͼ = 1
        ����ͼ�� = 2
    End Enum
    Enum TrainStrainDraw As Integer
        ��Լ�� = 1
        ��Լ�� = 2
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

    'ʱ�̱���Ϣ
    Structure typeTimetableInf
        Dim sName As String 'ʱ�̱�����
        Dim sID As String 'ʱ�̱�ID
        Dim sCreateDate As Date '����ʱ��
        Dim sEditDate As Date '�޸�ʱ�� 
    End Structure
    Public TimetableInf() As typeTimetableInf

  
    'Structure typeCurSheetInf
    '    Dim sSheetName As String
    '    Dim nCols As Integer
    '    Dim nRows As Integer
    '    Dim sValue() As String
    'End Structure


    'Structure typeOutToExcelInf
    '    Dim sFileName As String
    '    Dim nSheetsNum As Integer
    '    Dim CurSheets() As typeCurSheetInf
    'End Structure
    'Public OutToExcelInf As typeOutToExcelInf


    ''�Զ����ͼ����
    'Public Sub IniteDiagramPicViraient()

    '    '�����ݿ��ж���
    '    Dim i As Integer
    '    Dim strTable3 As String = "select * from ����ͼϵͳ������ order by ���"
    '    Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
    '    '����һ��Datasetd
    '    Dim myDataSet3 As Data.DataSet = New Data.DataSet
    '    Mydc3.Fill(myDataSet3, "����ͼϵͳ������")
    '    Dim myTable3 As Data.DataTable
    '    myTable3 = myDataSet3.Tables("����ͼϵͳ������")
    '    For i = 1 To myTable3.Rows.Count
    '        Select Case Trim(myTable3.Rows(i - 1).Item("������"))
    '            Case "��ͼ��ʼʱ��"
    '                TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��ͼ��ʾʱ���"
    '                TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "ʱ��Ƚ���ʼʱ��"
    '                TimeTablePara.TimeTableDiagramPara.intCompareFirstTime = HourToSecond(Val(myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim))
    '            Case "��ͼ��"
    '                TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��ͼ��"
    '                TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��ͼʱ�ָ�ʽ"
    '                TimeTablePara.TimeTableDiagramPara.strTimeFormat = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��ͼ���¿հ׸߶�"
    '                TimeTablePara.TimeTableDiagramPara.sngtopBlank = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��ͼʱ��հ׸߶�"
    '                TimeTablePara.TimeTableDiagramPara.sngTimeBlank = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��ͼ���ҿհ׸߶�"
    '                TimeTablePara.TimeTableDiagramPara.sngLeftBlank = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��ͼ��վ�հ׿��"
    '                TimeTablePara.TimeTableDiagramPara.sngStaBlank = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "����������"
    '                TimeTablePara.TimeTableDiagramPara.sngPubLeftX = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���������߶�"
    '                TimeTablePara.TimeTableDiagramPara.sngPubTopY = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�ָ�ͼ1�ָ�����"
    '                TimeTablePara.DiagramStylePara.OneTime1LineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�ָ�ͼ1�ָ��߿�"
    '                TimeTablePara.DiagramStylePara.OneTime1LineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�ָ�ͼ1�ָ�����ɫ"
    '                TimeTablePara.DiagramStylePara.OneTime1LineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�ָ�ͼ5�ָ�����"
    '                TimeTablePara.DiagramStylePara.OneTime5LineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�ָ�ͼ5�ָ��߿�"
    '                TimeTablePara.DiagramStylePara.OneTime5LineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�ָ�ͼ5�ָ�����ɫ"
    '                TimeTablePara.DiagramStylePara.OneTime5LineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�ָ�ͼ10�ָ�����"
    '                TimeTablePara.DiagramStylePara.OneTime10LineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�ָ�ͼ10�ָ��߿�"
    '                TimeTablePara.DiagramStylePara.OneTime10LineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�ָ�ͼ10�ָ�����ɫ"
    '                TimeTablePara.DiagramStylePara.OneTime10LineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�ָ�ͼ30�ָ�����"
    '                TimeTablePara.DiagramStylePara.OneTime30LineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�ָ�ͼ30�ָ��߿�"
    '                TimeTablePara.DiagramStylePara.OneTime30LineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�ָ�ͼ30�ָ�����ɫ"
    '                TimeTablePara.DiagramStylePara.OneTime30LineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�ָ�ͼ60�ָ�����"
    '                TimeTablePara.DiagramStylePara.OneTime60LineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�ָ�ͼ60�ָ��߿�"
    '                TimeTablePara.DiagramStylePara.OneTime60LineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�ָ�ͼ60�ָ�����ɫ"
    '                TimeTablePara.DiagramStylePara.OneTime60LineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���ָ�ͼ2�ָ�����"
    '                TimeTablePara.DiagramStylePara.TwoTime2LineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���ָ�ͼ2�ָ��߿�"
    '                TimeTablePara.DiagramStylePara.TwoTime2LineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���ָ�ͼ2�ָ�����ɫ"
    '                TimeTablePara.DiagramStylePara.TwoTime2LineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���ָ�ͼ10�ָ�����"
    '                TimeTablePara.DiagramStylePara.TwoTime10LineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���ָ�ͼ10�ָ��߿�"
    '                TimeTablePara.DiagramStylePara.TwoTime10LineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���ָ�ͼ10�ָ�����ɫ"
    '                TimeTablePara.DiagramStylePara.TwoTime10LineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���ָ�ͼ30�ָ�����"
    '                TimeTablePara.DiagramStylePara.TwoTime30LineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���ָ�ͼ30�ָ��߿�"
    '                TimeTablePara.DiagramStylePara.TwoTime30LineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���ָ�ͼ30�ָ�����ɫ"
    '                TimeTablePara.DiagramStylePara.TwoTime30LineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���ָ�ͼ60�ָ�����"
    '                TimeTablePara.DiagramStylePara.TwoTime60LineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���ָ�ͼ60�ָ��߿�"
    '                TimeTablePara.DiagramStylePara.TwoTime60LineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���ָ�ͼ60�ָ�����ɫ"
    '                TimeTablePara.DiagramStylePara.TwoTime60LineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "ʮ�ָ�ͼ10�ָ�����"
    '                TimeTablePara.DiagramStylePara.TenTime10LineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "ʮ�ָ�ͼ10�ָ��߿�"
    '                TimeTablePara.DiagramStylePara.TenTime10LineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "ʮ�ָ�ͼ10�ָ�����ɫ"
    '                TimeTablePara.DiagramStylePara.TenTime10LineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "ʮ�ָ�ͼ30�ָ�����"
    '                TimeTablePara.DiagramStylePara.TenTime30LineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "ʮ�ָ�ͼ30�ָ��߿�"
    '                TimeTablePara.DiagramStylePara.TenTime30LineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "ʮ�ָ�ͼ30�ָ�����ɫ"
    '                TimeTablePara.DiagramStylePara.TenTime30LineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "ʮ�ָ�ͼ60�ָ�����"
    '                TimeTablePara.DiagramStylePara.TenTime60LineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "ʮ�ָ�ͼ60�ָ��߿�"
    '                TimeTablePara.DiagramStylePara.TenTime60LineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "ʮ�ָ�ͼ60�ָ�����ɫ"
    '                TimeTablePara.DiagramStylePara.TenTime60LineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "Сʱ��ͼ60�ָ�����"
    '                TimeTablePara.DiagramStylePara.HourTime60LineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "Сʱ��ͼ60�ָ��߿�"
    '                TimeTablePara.DiagramStylePara.HourTime60LineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "Сʱ��ͼ60�ָ�����ɫ"
    '                TimeTablePara.DiagramStylePara.HourTime60LineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��վ������������"
    '                TimeTablePara.DiagramStylePara.StaNameFontName = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��վ���������С"
    '                TimeTablePara.DiagramStylePara.StaNameFontSize = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��վ�����������"
    '                TimeTablePara.DiagramStylePara.StaNameFontBold = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��վ��������б��"
    '                TimeTablePara.DiagramStylePara.StaNameFontItalic = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��վ����������ɫ"
    '                TimeTablePara.DiagramStylePara.StaNameFontColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "ʱ���ע��������"
    '                TimeTablePara.DiagramStylePara.TimeFontName = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "ʱ���ע�����С"
    '                TimeTablePara.DiagramStylePara.TimeFontSize = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "ʱ���ע�������"
    '                TimeTablePara.DiagramStylePara.TimeFontBold = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "ʱ���ע����б��"
    '                TimeTablePara.DiagramStylePara.TimeFontItalic = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "ʱ���ע������ɫ"
    '                TimeTablePara.DiagramStylePara.TimeFontColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�㳵վ����������"
    '                TimeTablePara.DiagramStylePara.StaLineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�㳵վ�����߿��"
    '                TimeTablePara.DiagramStylePara.StaLineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "һ�㳵վ��������ɫ"
    '                TimeTablePara.DiagramStylePara.StaLineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "�ֲ�վ����������"
    '                TimeTablePara.DiagramStylePara.FenStaLineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "�ֲ�վ�����߿��"
    '                TimeTablePara.DiagramStylePara.FenStaLineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "�ֲ�վ��������ɫ"
    '                TimeTablePara.DiagramStylePara.FenStaLineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��������������"
    '                TimeTablePara.DiagramStylePara.CheChangStaLineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���������߿��"
    '                TimeTablePara.DiagramStylePara.CheChangStaLineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "������������ɫ"
    '                TimeTablePara.DiagramStylePara.CheChangStaLineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��������������"
    '                TimeTablePara.DiagramStylePara.TrainLineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "�����������߿�"
    '                TimeTablePara.DiagramStylePara.TrainLineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "������������ɫ"
    '                TimeTablePara.DiagramStylePara.TrainLineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���г�������������"
    '                TimeTablePara.DiagramStylePara.CheDiLineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���г����������߿�"
    '                TimeTablePara.DiagramStylePara.CheDiLineWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���г�����������ɫ"
    '                TimeTablePara.DiagramStylePara.CheDiLineColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���α����������"
    '                TimeTablePara.DiagramStylePara.CheCiFontName = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���α�������С"
    '                TimeTablePara.DiagramStylePara.CheCiFontSize = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���α���������"
    '                TimeTablePara.DiagramStylePara.CheCiFontColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���α������б��"
    '                TimeTablePara.DiagramStylePara.CheCiFontItalic = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���α��������ɫ"
    '                TimeTablePara.DiagramStylePara.CheCiFontColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "б�򳵴���������"
    '                TimeTablePara.DiagramStylePara.XieCheCiFontName = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "б�򳵴������С"
    '                TimeTablePara.DiagramStylePara.XieCheCiFontSize = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "б�򳵴��������"
    '                TimeTablePara.DiagramStylePara.XieCheCiFontColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "б�򳵴�����б��"
    '                TimeTablePara.DiagramStylePara.XieCheCiFontItalic = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "б�򳵴�������ɫ"
    '                TimeTablePara.DiagramStylePara.XieCheCiFontColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "б�򳵴�������ɫ"
    '                TimeTablePara.DiagramStylePara.XieCheCiFontColor = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "�Ƿ���ʾ����"
    '                TimeTablePara.TimeTableDiagramPara.nifPrintCheCi = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "�Ƿ���ʾб�򳵴�"
    '                TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���׽�·�߸߶�"
    '                TimeTablePara.TimeTableDiagramPara.nCheDiLineHeight = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "���׽�·������"
    '                TimeTablePara.TimeTableDiagramPara.sCheDiLineStyle = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��վ�ɵ��߼��"
    '                TimeTablePara.StaDiagramePara.nStaLineHeight = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��վվ��ͼ��"
    '                TimeTablePara.TimeTableDiagramPara.sngPicStationHeight = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "��վվ��ͼ��"
    '                TimeTablePara.TimeTableDiagramPara.sngPicStationWidth = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "�����߿ɵ���ʱ���"
    '                TdrawLinePara.sMaxMoveTime = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '            Case "�������ƶ�ʱ��"
    '                TdrawLinePara.sMoveStepTime = myTable3.Rows(i - 1).Item("��ֵ").ToString.Trim
    '        End Select
    '    Next
    '    Select Case GetUserName()
    '        Case "��������"
    '            TimeTablePara.TimeTableDiagramPara.sCheCiShowStyle = 1
    '        Case Else
    '            TimeTablePara.TimeTableDiagramPara.sCheCiShowStyle = 0
    '    End Select

    'End Sub

    '����ʱ�̱����Ƶõ�ʱ�̱�ID
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

    '����ʱ�̱����Ƶõ�ʱ�̱�ID
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

    'ͨ����ֵ�õ�����
    Public Function GetLineStyleFromText(ByVal strText As String) As Drawing2D.DashStyle
        Select Case strText
            Case "ʵ��"
                GetLineStyleFromText = Drawing2D.DashStyle.Solid
            Case "������"
                GetLineStyleFromText = Drawing2D.DashStyle.Dash
            Case "������"
                GetLineStyleFromText = Drawing2D.DashStyle.Dot
            Case "�㻮��"
                GetLineStyleFromText = Drawing2D.DashStyle.DashDot
            Case "˫�㻮��"
                GetLineStyleFromText = Drawing2D.DashStyle.DashDotDot
        End Select
    End Function

    'ͨ����ֵ�õ���������
    Public Function GetLineStyleNameFromText(ByVal strText As String) As String
        GetLineStyleNameFromText = ""
        Select Case strText
            Case "ʵ��"
                GetLineStyleNameFromText = "ʵ�� ������������������"
            Case "������"
                GetLineStyleNameFromText = "�����ߡ� �� �� �� �� ��"
            Case "������"
                GetLineStyleNameFromText = "������-----------------"
            Case "�㻮��"
                GetLineStyleNameFromText = "�㻮�ߡ� - �� - �� - ��"
            Case "˫�㻮��"
                GetLineStyleNameFromText = "˫�㻮�ߡ� -- �� -- �� "
        End Select
    End Function

    'ͨ���������Ƶõ���ֵ
    Public Function GetLineTextStyle(ByVal strText As String) As Drawing2D.DashStyle
        GetLineTextStyle = Drawing2D.DashStyle.Solid
        Select Case strText.Trim.Substring(0, 2)
            Case "ʵ��"
                GetLineTextStyle = Drawing2D.DashStyle.Solid
            Case "����"
                GetLineTextStyle = Drawing2D.DashStyle.Dash
            Case "����"
                GetLineTextStyle = Drawing2D.DashStyle.Dot
            Case "�㻮"
                GetLineTextStyle = Drawing2D.DashStyle.DashDot
            Case "˫��"
                GetLineTextStyle = Drawing2D.DashStyle.DashDotDot
        End Select
    End Function

    'ͨ���������Ƶõ���ֵ
    Public Function GetLineTextNameFromStyle(ByVal strText As String) As String
        GetLineTextNameFromStyle = ""
        If strText.Trim.Length >= 2 Then
            Select Case strText.Trim.Substring(0, 2)
                Case "ʵ��"
                    GetLineTextNameFromStyle = "ʵ��"
                Case "����"
                    GetLineTextNameFromStyle = "������"
                Case "����"
                    GetLineTextNameFromStyle = "������"
                Case "�㻮"
                    GetLineTextNameFromStyle = "�㻮��"
                Case "˫��"
                    GetLineTextNameFromStyle = "˫�㻮��"
            End Select
        End If
    End Function
    'ͨ�����͵õ���ֵ
    Public Function GetLineStyleTextFromStyleName(ByVal strText As Drawing2D.DashStyle) As String
        GetLineStyleTextFromStyleName = ""
        Select Case strText
            Case Drawing2D.DashStyle.Solid
                GetLineStyleTextFromStyleName = "ʵ��"
            Case Drawing2D.DashStyle.Dash
                GetLineStyleTextFromStyleName = "������"
            Case Drawing2D.DashStyle.Dot
                GetLineStyleTextFromStyleName = "������"
            Case Drawing2D.DashStyle.DashDot
                GetLineStyleTextFromStyleName = "�㻮��"
            Case Drawing2D.DashStyle.DashDotDot
                GetLineStyleTextFromStyleName = "˫�㻮��"
        End Select
    End Function
    'ͨ�����εõ�CheDiID
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

    ' �޸�0-4���ʱ��ε�ʱ��ֵ������24Сʱ
    Public Function AddLitterTime(ByVal sTime As Long) As Long
        If sTime = -1 Then
            AddLitterTime = sTime
        Else
            If sTime >= 0 And sTime <= TimeTablePara.TimeTableDiagramPara.intCompareFirstTime Then
                AddLitterTime = sTime + 24 * 3600.0#
            Else
                AddLitterTime = sTime
            End If
        End If
    End Function

    '����������ǰʱ��
    Public Function GetMouseMoveTime(ByVal X As Single, ByVal sngTolWidth As Single) As Long
        GetMouseMoveTime = 0
        Dim sngTime As Long
        sngTime = TimeAdd(TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime * 3600, Int((X - TimeTablePara.TimeTableDiagramPara.sngLeftBlank - TimeTablePara.TimeTableDiagramPara.sngStaBlank) * TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime * 3600 / sngTolWidth))
        GetMouseMoveTime = sngTime
    End Function

    '�г�����������
    Private Sub SetAllTrainSeq(ByVal nCurSta As Integer, ByVal sSecName As String, ByVal sStarOrArri As String, ByVal nUporDown As Integer)
        Dim i As Integer
        Dim j As Integer
        Dim p As Integer
        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long
        ReDim tmpPubShuZhu(0)

        If sStarOrArri = "����" Then '����
            For i = 1 To UBound(TrainInf)
                If TrainInf(i).Train <> "" Then
                    For p = 1 To UBound(TrainInf(i).nPassSection)
                        If SectionInf(TrainInf(i).nPassSection(p)).sSecName = sSecName Then
                            If (i + nUporDown) Mod 2 = 0 Then
                                ReDim Preserve tmpPubShuZhu(UBound(tmpPubShuZhu) + 1)
                                tmpPubShuZhu(UBound(tmpPubShuZhu)) = i
                            End If
                        End If
                    Next
                End If
            Next i
            '������ʱ������
            Flag = 1
            k = UBound(tmpPubShuZhu)
            Do While Flag > 0
                k = k - 1
                Flag = 0
                For j = 1 To k
                    TempTime1 = AddLitterTime(TrainInf(tmpPubShuZhu(j)).Starting(nCurSta))
                    Temptime2 = AddLitterTime(TrainInf(tmpPubShuZhu(j + 1)).Starting(nCurSta))
                    If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                        temp = tmpPubShuZhu(j)
                        tmpPubShuZhu(j) = tmpPubShuZhu(j + 1)
                        tmpPubShuZhu(j + 1) = temp
                        Flag = 1
                    End If
                Next j
            Loop

        Else '����

            For i = 1 To UBound(TrainInf)
                If TrainInf(i).Train <> "" Then
                    For p = 1 To UBound(TrainInf(i).nPassSection)
                        If SectionInf(TrainInf(i).nPassSection(p)).sSecName = sSecName Then
                            If (i + nUporDown) Mod 2 = 0 Then
                                ReDim Preserve tmpPubShuZhu(UBound(tmpPubShuZhu) + 1)
                                tmpPubShuZhu(UBound(tmpPubShuZhu)) = i
                            End If
                        End If
                    Next
                End If
            Next i
            '������ʱ������
            Flag = 1
            k = UBound(tmpPubShuZhu)
            Do While Flag > 0
                k = k - 1
                Flag = 0
                For j = 1 To k
                    TempTime1 = AddLitterTime(TrainInf(tmpPubShuZhu(j)).Arrival(nCurSta))
                    Temptime2 = AddLitterTime(TrainInf(tmpPubShuZhu(j + 1)).Arrival(nCurSta))
                    If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                        temp = tmpPubShuZhu(j)
                        tmpPubShuZhu(j) = tmpPubShuZhu(j + 1)
                        tmpPubShuZhu(j + 1) = temp
                        Flag = 1
                    End If
                Next j
            Loop

        End If

    End Sub

    '���ҳ���ǰ�е��г�
    Public Function nFindBeforeTrain(ByVal nCurTrain As Integer, ByVal nCurSta As Integer, ByVal sSecName As String) As Integer
        Dim i As Integer
        Dim ntmpTrain As Integer
        Dim Scurtime As Long
        Dim sTmpTime As Long
        nFindBeforeTrain = 0
        ReDim tmpPubShuZhu(0)
        Call SetAllTrainSeq(nCurSta, sSecName, "����", nCurTrain)
        If UBound(tmpPubShuZhu) > 0 Then
            For i = UBound(tmpPubShuZhu) To 1 Step -1
                ntmpTrain = tmpPubShuZhu(i)
                Scurtime = AddLitterTime(TrainInf(nCurTrain).Starting(nCurSta))
                sTmpTime = AddLitterTime(TrainInf(ntmpTrain).Starting(nCurSta))
                If sTmpTime < Scurtime Then
                    nFindBeforeTrain = ntmpTrain
                    Exit For
                End If
            Next i
        End If
    End Function

    '���ҵ�����е��г�
    Public Function nFindArriAfterTrain(ByVal nCurTrain As Integer, ByVal nCurSta As Integer, ByVal sSecName As String) As Integer
        Dim i As Integer
        Dim ntmpTrain As Integer
        Dim Scurtime As Long
        Dim sTmpTime As Long
        nFindArriAfterTrain = 0
        ReDim tmpPubShuZhu(0)
        Call SetAllTrainSeq(nCurSta, sSecName, "����", nCurTrain)
        If UBound(tmpPubShuZhu) > 0 Then
            For i = 1 To UBound(tmpPubShuZhu)
                ntmpTrain = tmpPubShuZhu(i)
                Scurtime = AddLitterTime(TrainInf(nCurTrain).Arrival(nCurSta))
                sTmpTime = AddLitterTime(TrainInf(ntmpTrain).Arrival(nCurSta))
                If sTmpTime > Scurtime Then
                    nFindArriAfterTrain = ntmpTrain
                    Exit For
                End If
            Next i
        End If
    End Function

    '���ҳ������е��г�
    Public Function nFindAfterTrain(ByVal nCurTrain As Integer, ByVal nCurSta As Integer, ByVal sSecName As String) As Integer
        Dim i As Integer
        Dim ntmpTrain As Integer
        Dim Scurtime As Long
        Dim sTmpTime As Long
        nFindAfterTrain = 0
        ReDim tmpPubShuZhu(0)
        Call SetAllTrainSeq(nCurSta, sSecName, "����", nCurTrain)
        If UBound(tmpPubShuZhu) > 0 Then
            For i = 1 To UBound(tmpPubShuZhu)
                ntmpTrain = tmpPubShuZhu(i)
                Scurtime = AddLitterTime(TrainInf(nCurTrain).Starting(nCurSta))
                sTmpTime = AddLitterTime(TrainInf(ntmpTrain).Starting(nCurSta))
                If sTmpTime > Scurtime Then
                    nFindAfterTrain = ntmpTrain
                    Exit For
                End If
            Next i
        End If
    End Function

    '�õ��г���ʼ����͵���
    Public Function GetTrainArriOrStartTime(ByVal nTrain As Integer, ByVal nStartOrEnd As Integer, ByVal nArriOrStart As Integer) As Long
        If nArriOrStart = 0 Then '����
            If nStartOrEnd = 0 Then 'ʼ��վ
                GetTrainArriOrStartTime = TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(1))
            ElseIf nStartOrEnd = -1 Then '�յ�վ
                GetTrainArriOrStartTime = TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID)))
            Else '����վ
                GetTrainArriOrStartTime = TrainInf(nTrain).Arrival(nStartOrEnd)
            End If
        Else '����
            If nStartOrEnd = 0 Then 'ʼ��վ
                GetTrainArriOrStartTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
            ElseIf nStartOrEnd = -1 Then '�յ�վ
                GetTrainArriOrStartTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID)))
            Else '����վ
                GetTrainArriOrStartTime = TrainInf(nTrain).Starting(nStartOrEnd)
            End If
        End If
    End Function

    '�õ��г���������յ�������
    Public Function GetTrainArriXCoord(ByVal nTrain As Integer, ByVal nStaID As Integer) As Single
        GetTrainArriXCoord = FormTimeToXCord(GetTrainArriOrStartTime(nTrain, nStaID, 0), TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
    End Function


    '�õ��г��������ʼ�������� 
    Public Function GetTrainrStartXCoord(ByVal nTrain As Integer, ByVal nStaID As Integer) As Single
        GetTrainrStartXCoord = FormTimeToXCord(GetTrainArriOrStartTime(nTrain, nStaID, 1), TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
    End Function

    '���г��ӳ������жϿ�
    Public Sub DelectCheDiLink(ByVal nTrain As Integer, ByVal nCdid As Integer)
        Dim i As Integer
        Dim TempNum() As Integer
        Dim TempNum2() As Integer

        Dim nFtrain As Integer
        Dim nNtrain As Integer
        'Dim sZytime As Long
        ReDim TempNum(0)
        ReDim TempNum2(0)
        Dim TN As Integer
        TN = 1
        'If nTrain = 13 Then Stop
        Dim nID As Integer
        nID = AddNewChediInfor()
        If nID > 0 Then
            For i = 1 To UBound(ChediInfo(nCdid).nLinkTrain)
                If ChediInfo(nCdid).nLinkTrain(i) <> nTrain Then
                    If TN = 1 Then
                        ReDim Preserve TempNum(UBound(TempNum) + 1)
                        TempNum(UBound(TempNum)) = ChediInfo(nCdid).nLinkTrain(i)
                    Else
                        ReDim Preserve TempNum2(UBound(TempNum2) + 1)
                        TempNum2(UBound(TempNum2)) = ChediInfo(nCdid).nLinkTrain(i)
                    End If
                Else '�޸Ĺɵ�ռ��ʱ��
                    '    If i > 1 Then
                    '        If i = UBound(ChediInfo(nCdid).nLinkTrain) Then
                    '            nFtrain = ChediInfo(nCdid).nLinkTrain(i - 1)
                    '            If TrainInf(nTrain).TrainReturnStyle(1) = "վ���۷�" Then
                    '                sZytime = nReturnTchTime(nTrain, StationInf(TrainInf(nTrain).nPathID(1)).sStationName)
                    '                TrainInf(nFtrain).sEndZFStarting = TimeAdd(TrainInf(nFtrain).sEndZFArrival, sZytime)
                    '            ElseIf TrainInf(nTrain).TrainReturnStyle(1) = "վǰ�۷�" Then
                    '                sZytime = lEndTchTime(nFtrain)
                    '                TrainInf(nFtrain).Starting(TrainInf(nFtrain).nPathID(UBound(TrainInf(nFtrain).nPathID))) = TimeAdd(TrainInf(nFtrain).Arrival(TrainInf(nFtrain).nPathID(UBound(TrainInf(nFtrain).nPathID))), sZytime)
                    '            End If
                    '        Else
                    '            nFtrain = ChediInfo(nCdid).nLinkTrain(i - 1)
                    '            nNtrain = ChediInfo(nCdid).nLinkTrain(i + 1)
                    '            If TrainInf(nTrain).TrainReturnStyle(1) = "վ���۷�" Then
                    '                sZytime = nReturnTchTime(nTrain, StationInf(TrainInf(nTrain).nPathID(1)).sStationName)
                    '                TrainInf(nFtrain).sEndZFStarting = TimeAdd(TrainInf(nFtrain).sEndZFArrival, sZytime)
                    '            ElseIf TrainInf(nTrain).TrainReturnStyle(1) = "վǰ�۷�" Then
                    '                sZytime = lEndTchTime(nFtrain)
                    '                TrainInf(nFtrain).Starting(TrainInf(nFtrain).nPathID(UBound(TrainInf(nFtrain).nPathID))) = TimeAdd(TrainInf(nFtrain).Arrival(TrainInf(nFtrain).nPathID(UBound(TrainInf(nFtrain).nPathID))), sZytime)
                    '            End If

                    '            If TrainInf(nTrain).TrainReturnStyle(2) = "վ���۷�" Then
                    '                sZytime = nReturnTchTime(nNtrain, StationInf(TrainInf(nNtrain).nPathID(1)).sStationName)
                    '                TrainInf(nNtrain).sStartZFArrival = TimeMinus(TrainInf(nFtrain).sStartZFStarting, sZytime)
                    '            ElseIf TrainInf(nTrain).TrainReturnStyle(2) = "վǰ�۷�" Then
                    '                sZytime = nStartTchTime(nNtrain)
                    '                TrainInf(nNtrain).Arrival(TrainInf(nNtrain).nPathID(1)) = TimeMinus(TrainInf(nFtrain).Starting(TrainInf(nFtrain).nPathID(1)), sZytime)
                    '            End If
                    '        End If
                    '    End If
                    ReDim Preserve TempNum2(UBound(TempNum2) + 1)
                    TempNum2(UBound(TempNum2)) = ChediInfo(nCdid).nLinkTrain(i)
                    TN = 2
                End If
            Next i

            ReDim ChediInfo(nCdid).nLinkTrain(0)
            If UBound(TempNum) > 0 Then
                ReDim ChediInfo(nCdid).nLinkTrain(UBound(TempNum))
                For i = 1 To UBound(TempNum)
                    ChediInfo(nCdid).nLinkTrain(i) = TempNum(i)
                Next i
                If UBound(ChediInfo(nCdid).nLinkTrain) > 0 Then
                    If UBound(ChediInfo(nCdid).nLinkTrain) = 1 Then
                        TrainInf(ChediInfo(nCdid).nLinkTrain(1)).TrainReturn(1) = 0
                        TrainInf(ChediInfo(nCdid).nLinkTrain(1)).TrainReturn(2) = 0
                    Else
                        For i = 1 To UBound(ChediInfo(nCdid).nLinkTrain) - 1
                            nFtrain = ChediInfo(nCdid).nLinkTrain(i)
                            nNtrain = ChediInfo(nCdid).nLinkTrain(i + 1)
                            If i = 1 Then
                                TrainInf(nFtrain).TrainReturn(1) = 0
                            End If
                            If i = UBound(ChediInfo(nCdid).nLinkTrain) - 1 Then
                                TrainInf(nNtrain).TrainReturn(2) = 0
                            End If
                            TrainInf(nFtrain).TrainReturn(2) = nNtrain
                            TrainInf(nNtrain).TrainReturn(1) = nFtrain
                        Next i
                    End If
                End If
            End If

            If UBound(TempNum2) > 0 Then
                ReDim ChediInfo(nID).nLinkTrain(UBound(TempNum2))
                For i = 1 To UBound(TempNum2)
                    ChediInfo(nID).nLinkTrain(i) = TempNum2(i)
                Next i

                If UBound(ChediInfo(nID).nLinkTrain) > 0 Then
                    If UBound(ChediInfo(nID).nLinkTrain) = 1 Then
                        TrainInf(ChediInfo(nID).nLinkTrain(1)).TrainReturn(1) = 0
                        TrainInf(ChediInfo(nID).nLinkTrain(1)).TrainReturn(2) = 0
                    Else
                        For i = 1 To UBound(ChediInfo(nID).nLinkTrain) - 1
                            nFtrain = ChediInfo(nID).nLinkTrain(i)
                            nNtrain = ChediInfo(nID).nLinkTrain(i + 1)
                            If i = 1 Then
                                TrainInf(nFtrain).TrainReturn(1) = 0
                            End If
                            If i = UBound(ChediInfo(nCdid).nLinkTrain) - 1 Then
                                TrainInf(nNtrain).TrainReturn(2) = 0
                            End If
                            TrainInf(nFtrain).TrainReturn(2) = nNtrain
                            TrainInf(nNtrain).TrainReturn(1) = nFtrain
                        Next i
                    End If
                End If
            End If
            TrainInf(nTrain).nCheDiPuOrNot = 1
            TrainInf(nTrain).nIfFixedCheDi = 0
        End If

    End Sub

    '��������Ҫ����г�
    Public Function SeekCDLinkTrain(ByVal nTrain As Integer, ByVal nNum As Integer, ByVal sTempTime As Single) As Integer
        'Nnum ��ʾ�������͵��г���1��ʾ�����еĳ���2��ʾ���ϣ�3��ʾ���£�4��ʾ����
        Dim i As Integer
        Dim sTime As Single
        Dim sStime As Single
        Dim sZheFantime As Single
        SeekCDLinkTrain = 0
        If TrainInf(nTrain).TrainStyle <> "���γ�" Then
            Select Case nNum
                Case 1
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).SCheDiLeiXing = TrainInf(nTrain).SCheDiLeiXing Then
                            'If TrainInf(i).nCheDiPuOrNot = 0 Then
                            If i Mod 2 = 0 And TrainInf(i).NextStation = TrainInf(nTrain).ComeStation Then
                                sTime = AddTimeOver24(TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))))
                                If Math.Abs(sTempTime - sTime) <= 100 Then
                                    If TrainInf(i).nZfLimit = 1 Then
                                        SeekCDLinkTrain = i
                                        Exit Function
                                    End If
                                    sStime = AddTimeOver24(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                                    sZheFantime = CDZGetZheFanTime(i, TrainInf(i).SCheDiLeiXing, StationInf(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))).sStationName, TrainInf(i).TrainReturnStyle(2))
                                    If sStime - sTime - sZheFantime >= 0 Then
                                        If TrainInf(i).TrainReturn(2) = 0 Then
                                            SeekCDLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                            'End If
                        End If
                    Next i
                Case 2
                    For i = 1 To UBound(TrainInf)
                        'If i = 462 Then Stop
                        If TrainInf(i).SCheDiLeiXing = TrainInf(nTrain).SCheDiLeiXing Then
                            'If TrainInf(i).nCheDiPuOrNot = 0 Then
                            'If i = 462 Then Stop
                            If i Mod 2 = 0 And TrainInf(i).ComeStation = TrainInf(nTrain).NextStation Then
                                sTime = AddTimeOver24(TrainInf(i).Starting(TrainInf(i).nPathID(1)))
                                If Math.Abs(sTempTime - sTime) <= 100 Then
                                    If TrainInf(i).nZfLimit = 1 Then
                                        SeekCDLinkTrain = i
                                        Exit Function
                                    End If
                                    sStime = AddTimeOver24(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))))
                                    sZheFantime = CDZGetZheFanTime(nTrain, TrainInf(nTrain).SCheDiLeiXing, StationInf(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))).sStationName, TrainInf(nTrain).TrainReturnStyle(2))
                                    If sTime - sStime - sZheFantime >= 0 Then
                                        If TrainInf(i).TrainReturn(1) = 0 Then
                                            SeekCDLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                            'End If
                        End If
                    Next i
                Case 3
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).SCheDiLeiXing = TrainInf(nTrain).SCheDiLeiXing Then
                            'If TrainInf(i).nCheDiPuOrNot = 0 Then
                            If i Mod 2 <> 0 And TrainInf(i).NextStation = TrainInf(nTrain).ComeStation Then
                                sTime = AddTimeOver24(TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))))
                                If Math.Abs(sTempTime - sTime) <= 100 Then
                                    If TrainInf(i).nZfLimit = 1 Then
                                        SeekCDLinkTrain = i
                                        Exit Function
                                    End If
                                    sStime = AddTimeOver24(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                                    sZheFantime = CDZGetZheFanTime(i, TrainInf(i).SCheDiLeiXing, StationInf(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))).sStationName, TrainInf(i).TrainReturnStyle(2))
                                    If sStime - sTime - sZheFantime >= 0 Then
                                        If TrainInf(i).TrainReturn(2) = 0 Then
                                            SeekCDLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                            'End If
                        End If
                    Next i
                Case 4
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).SCheDiLeiXing = TrainInf(nTrain).SCheDiLeiXing Then
                            'If i = 100 Then Stop
                            If i Mod 2 <> 0 And TrainInf(i).ComeStation = TrainInf(nTrain).NextStation Then
                                sTime = AddTimeOver24(TrainInf(i).Starting(TrainInf(i).nPathID(1)))
                                If Math.Abs(sTempTime - sTime) <= 100 Then
                                    If TrainInf(i).nZfLimit = 1 Then
                                        SeekCDLinkTrain = i
                                        Exit Function
                                    End If
                                    sStime = AddTimeOver24(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))))
                                    sZheFantime = CDZGetZheFanTime(nTrain, TrainInf(nTrain).SCheDiLeiXing, StationInf(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))).sStationName, TrainInf(nTrain).TrainReturnStyle(2))
                                    If sTime - sStime - sZheFantime >= 0 Then
                                        If TrainInf(i).TrainReturn(1) = 0 Then
                                            SeekCDLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                            'End If
                        End If
                    Next i
            End Select

        Else 'Ϊ���γ�

            Select Case nNum
                Case 1
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).SCheDiLeiXing = TrainInf(nTrain).SCheDiLeiXing Then
                            If TrainInf(i).NextStation = TrainInf(nTrain).NextStation Then
                                sTime = AddTimeOver24(TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))))
                                If Math.Abs(sTempTime - sTime) <= 100 Then
                                    If TrainInf(i).nZfLimit = 1 Then
                                        SeekCDLinkTrain = i
                                        Exit Function
                                    End If
                                    sStime = AddTimeOver24(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                                    sZheFantime = CDZGetZheFanTime(i, TrainInf(i).SCheDiLeiXing, StationInf(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))).sStationName, TrainInf(i).TrainReturnStyle(2))
                                    If sStime - sTime - sZheFantime >= 0 Then
                                        If TrainInf(i).TrainReturn(2) = 0 Then
                                            SeekCDLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                            'End If
                        End If
                    Next i
                Case 2
                    For i = 1 To UBound(TrainInf)
                        'If i = 462 Then Stop
                        If TrainInf(i).SCheDiLeiXing = TrainInf(nTrain).SCheDiLeiXing Then
                            'If TrainInf(i).nCheDiPuOrNot = 0 Then
                            'If i = 462 Then Stop
                            If TrainInf(i).NextStation = TrainInf(nTrain).NextStation Then
                                sTime = AddTimeOver24(TrainInf(i).Starting(TrainInf(i).nPathID(1)))
                                If Math.Abs(sTempTime - sTime) <= 100 Then
                                    If TrainInf(i).nZfLimit = 1 Then
                                        SeekCDLinkTrain = i
                                        Exit Function
                                    End If
                                    sStime = AddTimeOver24(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))))
                                    sZheFantime = CDZGetZheFanTime(nTrain, TrainInf(nTrain).SCheDiLeiXing, StationInf(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))).sStationName, TrainInf(nTrain).TrainReturnStyle(2))
                                    If sTime - sStime - sZheFantime >= 0 Then
                                        If TrainInf(i).TrainReturn(1) = 0 Then
                                            SeekCDLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                            'End If
                        End If
                    Next i
                Case 3
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).NextStation = TrainInf(nTrain).NextStation Then
                            'If TrainInf(i).nCheDiPuOrNot = 0 Then
                            If TrainInf(i).NextStation = TrainInf(nTrain).NextStation Then
                                sTime = AddTimeOver24(TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))))
                                If Math.Abs(sTempTime - sTime) <= 100 Then
                                    If TrainInf(i).nZfLimit = 1 Then
                                        SeekCDLinkTrain = i
                                        Exit Function
                                    End If
                                    sStime = AddTimeOver24(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                                    sZheFantime = CDZGetZheFanTime(i, TrainInf(i).SCheDiLeiXing, StationInf(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))).sStationName, TrainInf(i).TrainReturnStyle(2))
                                    If sStime - sTime - sZheFantime >= 0 Then
                                        If TrainInf(i).TrainReturn(2) = 0 Then
                                            SeekCDLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                            'End If
                        End If
                    Next i
                Case 4
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).NextStation = TrainInf(nTrain).NextStation Then
                            'If i = 100 Then Stop
                            If TrainInf(i).NextStation = TrainInf(nTrain).NextStation Then
                                sTime = AddTimeOver24(TrainInf(i).Starting(TrainInf(i).nPathID(1)))
                                If Math.Abs(sTempTime - sTime) <= 100 Then
                                    If TrainInf(i).nZfLimit = 1 Then
                                        SeekCDLinkTrain = i
                                        Exit Function
                                    End If
                                    sStime = AddTimeOver24(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))))
                                    sZheFantime = CDZGetZheFanTime(nTrain, TrainInf(nTrain).SCheDiLeiXing, StationInf(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))).sStationName, TrainInf(nTrain).TrainReturnStyle(2))
                                    If sTime - sStime - sZheFantime >= 0 Then
                                        If TrainInf(i).TrainReturn(1) = 0 Then
                                            SeekCDLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                            'End If
                        End If
                    Next i
            End Select

        End If

    End Function

    '�õ��г����۷�ʱ��
    Public Function CDZGetZheFanTime(ByVal nTrain As Integer, ByVal SCheDiLeiXing As String, ByVal sZheFanSta As String, ByVal sZheFanFangShi As String) As Single
        CDZGetZheFanTime = GetZheFanTime(SCheDiLeiXing, sZheFanSta, sZheFanFangShi)
    End Function

    '�ϲ�����
    Public Sub HeBinCheDiInfo(ByVal nCheDi1 As Integer, ByVal nCheDi2 As Integer)
        Dim i As Integer
        Dim j As Integer
        Dim tmpN() As Integer
        ReDim tmpN(0)
        Dim nTrain1 As Integer
        Dim nTrain2 As Integer
        nTrain1 = ChediInfo(nCheDi1).nLinkTrain(1)
        nTrain2 = ChediInfo(nCheDi2).nLinkTrain(1)
        Dim nCdid As Integer
        Dim nFtrain As Integer
        Dim nNtrain As Integer

        If AddLitterTime(TrainInf(nTrain1).Starting(TrainInf(nTrain1).nPathID(1))) < AddLitterTime(TrainInf(nTrain2).Starting(TrainInf(nTrain2).nPathID(1))) Then
            nCdid = nCheDi1
            For i = 1 To UBound(ChediInfo)
                If nCheDi1 = i Then
                    For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                        ReDim Preserve tmpN(UBound(tmpN) + 1)
                        tmpN(UBound(tmpN)) = ChediInfo(i).nLinkTrain(j)
                    Next j
                    Exit For
                End If
            Next i

            For i = 1 To UBound(ChediInfo)
                If nCheDi2 = i Then
                    For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                        ReDim Preserve tmpN(UBound(tmpN) + 1)
                        tmpN(UBound(tmpN)) = ChediInfo(i).nLinkTrain(j)
                    Next j
                    Exit For
                End If
            Next i
        Else
            nCdid = nCheDi2
            For i = 1 To UBound(ChediInfo)
                If nCheDi2 = i Then
                    For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                        ReDim Preserve tmpN(UBound(tmpN) + 1)
                        tmpN(UBound(tmpN)) = ChediInfo(i).nLinkTrain(j)
                    Next j
                    Exit For
                End If
            Next i

            For i = 1 To UBound(ChediInfo)
                If nCheDi1 = i Then
                    For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                        ReDim Preserve tmpN(UBound(tmpN) + 1)
                        tmpN(UBound(tmpN)) = ChediInfo(i).nLinkTrain(j)
                    Next j
                    Exit For
                End If
            Next i
        End If
        ReDim ChediInfo(nCheDi1).nLinkTrain(0)
        ReDim ChediInfo(nCheDi2).nLinkTrain(0)

        If UBound(tmpN) > 0 Then
            ReDim ChediInfo(nCdid).nLinkTrain(UBound(tmpN))
            For i = 1 To UBound(tmpN)
                ChediInfo(nCdid).nLinkTrain(i) = tmpN(i)
            Next i
        End If

        If UBound(ChediInfo(nCdid).nLinkTrain) > 0 Then
            If UBound(ChediInfo(nCdid).nLinkTrain) = 1 Then
                TrainInf(ChediInfo(nCdid).nLinkTrain(1)).TrainReturn(1) = 0
                TrainInf(ChediInfo(nCdid).nLinkTrain(1)).TrainReturn(2) = 0
            Else
                For i = 1 To UBound(ChediInfo(nCdid).nLinkTrain) - 1
                    nFtrain = ChediInfo(nCdid).nLinkTrain(i)
                    nNtrain = ChediInfo(nCdid).nLinkTrain(i + 1)

                    TrainInf(nFtrain).TrainReturn(2) = nNtrain
                    TrainInf(nNtrain).TrainReturn(1) = nFtrain
                Next i
            End If
        End If

    End Sub

    '���´��������г�����ͼʱ�̱����ݿ����ر�,
    Public Sub CreatTimeTableTable(ByVal tmpTableID As String)
        Dim myWS As dao.Workspace
        Dim DE As dao.DBEngine = New dao.DBEngine
        myWS = DE.Workspaces(0)
        Dim dFile As dao.Database
        dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        Dim Str As String
        Dim sTableName As String
        sTableName = tmpTableID & "�г�ʱ����Ϣ"
        'Str = "create table " & sTableName & " (ID int, ���� ntext(10), ��վ���� ntext(20), ���� ntext(10), ���� ntext(10), ͣվ�ɵ� ntext(4) )"
        Str = "create table " & sTableName & " (ID int, ���� text(10), ��վ���� text(20), ���� text(10), ���� text(10), ͣվ�ɵ� text(4) )"
        dFile.Execute(Str)
        dFile.Close()

        'Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        'If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        'Dim Str As String
        'Dim sTableName As String
        'sTableName = skbName & "�г�ʱ����Ϣ"
        'Str = "create table " & sTableName & " (ID int, ���� ntext(10), ��վ���� ntext(20), ���� ntext(10), ���� ntext(10), ͣվ�ɵ� ntext(4) )"
        'Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        'Mcom.ExecuteNonQuery()
        'MyConn.Close()
    End Sub

    '�½�����ʹ�÷�����
    Sub CreatNewCDUseTable(ByVal sCurSkbName As String)
        Dim myWS As dao.Workspace
        Dim DE As dao.DBEngine = New dao.DBEngine
        myWS = DE.Workspaces(0)
        Dim dFile As dao.Database
        dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        Dim Str As String
        Dim sTableName As String
        sTableName = sCurSkbName & "����ʹ�÷���"
        Str = "create table " & sTableName & " (����˳�� int, ����ID text(10), ���ҳ��� text(10), ����˳�� int, �г����� text(20), ��·���� text(20), ������� text(10), ������� text(20), ͣվ��� text(20), �Ƿ��۷�Լ�� text(4), ���������� text(20), ��������ɫ text(20), �������߿� text(4), ���������� text(20),��������ɫ text(20), �������߿� text(4) )"
        dFile.Execute(Str)
        dFile.Close()

        'Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        'If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        'Dim Str As String
        'Dim sTableName As String
        'sTableName = sCurSkbName & "����ʹ�÷���"
        'Str = "create table " & sTableName & " (����˳�� int, ����ID ntext(10), ���ҳ��� ntext(10), ����˳�� int, �г����� ntext(20), ��·���� ntext(20), ������� ntext(10), ������� ntext(20), ͣվ��� ntext(20), �Ƿ��۷�Լ�� ntext(4), ���������� ntext(20), ��������ɫ ntext(20), �������߿� ntext(4), ���������� ntext(20),��������ɫ ntext(20), �������߿� ntext(4) )"
        'Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        'Mcom.ExecuteNonQuery()
        'MyConn.Close()
    End Sub

    '����ʱ�̱���Ϣ
    Public Sub InputTimetableInf()
        ReDim TimetableInf(0)
        Dim myDB As dao.Database
        Dim myWS As dao.Workspace
        Dim dbStr As String
        Dim DE As dao.DBEngine = New dao.DBEngine
        Dim myRec As dao.Recordset
        Dim i As Integer
        Dim Num As Integer
        Num = 0
        myWS = DE.Workspaces(0)
        myDB = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor) '(dbStr, False, False)
        dbStr = "select * from ʱ�̱���Ϣ�� order by ʱ�̱�����"
        myRec = myDB.OpenRecordset(dbStr) ' DAO.RecordsetTypeEnum.dbOpenDynaset
        If myRec.RecordCount > 0 Then
            myRec.MoveLast()
            Num = myRec.RecordCount
        End If
        If Num > 0 Then
            ReDim TimetableInf(Num)
            myRec.MoveFirst()
            For i = 1 To Num
                TimetableInf(i).sName = myRec.Fields("ʱ�̱�����").Value.ToString.Trim
                TimetableInf(i).sID = myRec.Fields("ʱ�̱�ID").Value.ToString.Trim
                TimetableInf(i).sCreateDate = myRec.Fields("����ʱ��").Value.ToString.Trim
                TimetableInf(i).sEditDate = myRec.Fields("�޸�ʱ��").Value.ToString.Trim
                myRec.MoveNext()
            Next
        End If
    End Sub

    '����ʱ�̱���Ϣ
    Public Sub SaveTimetableInf()
        Dim myDB As dao.Database
        Dim myWS As dao.Workspace
        Dim dbStr As String
        Dim DE As dao.DBEngine = New dao.DBEngine
        Dim myRec As dao.Recordset
        Dim i As Integer
        Dim Num As Integer
        Num = 0
        myWS = DE.Workspaces(0)
        myDB = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor) '(dbStr, False, False)
        dbStr = "delete * from ʱ�̱���Ϣ��"
        myDB.Execute(dbStr)
        myRec = myDB.OpenRecordset("ʱ�̱���Ϣ��") ' DAO.RecordsetTypeEnum.dbOpenDynaset
        For i = 1 To UBound(TimetableInf)
            myRec.AddNew()
            myRec.Fields("ʱ�̱�����").Value = TimetableInf(i).sName
            myRec.Fields("ʱ�̱�ID").Value = TimetableInf(i).sID
            myRec.Fields("����ʱ��").Value = TimetableInf(i).sCreateDate
            myRec.Fields("�޸�ʱ��").Value = TimetableInf(i).sEditDate
            myRec.Update()
        Next   
    End Sub

    'ɾ�����м�¼
    Public Sub DeleteSKBTimeTable(ByVal tableName As String)

        Dim myWS As dao.Workspace
        Dim DE As dao.DBEngine = New dao.DBEngine
        myWS = DE.Workspaces(0)
        Dim dFile As dao.Database
        Dim sCurName As String
        dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        sCurName = tableName & "�г�ʱ����Ϣ"
        dFile.Execute("delete * from " & sCurName & "")

        sCurName = tableName & "����ʹ�÷���"
        dFile.Execute("delete * from " & sCurName & "")
        dFile.Close()


        'Dim Str As String
        ''Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        'Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        'If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        'Dim sCurName As String
        'sCurName = tableName & "�г�ʱ����Ϣ"
        'Str = "delete * from " & sCurName & ""
        'Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        'Mcom1.ExecuteNonQuery()

        'sCurName = tableName & "����ʹ�÷���"
        'Str = "delete * from " & sCurName & ""
        'Dim Mcom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        'Mcom2.ExecuteNonQuery()

        'MyConn.Close()
    End Sub
    Public Sub SaveSkbTimeTableByOracle(ByVal ProBar As ToolStripProgressBar)
        Dim sCurDiaID As String
        sCurDiaID = ODSPubpara.DiagramSelect
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_TIMETABLEINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'")
        TMSlocalDataSet.ExecuteNonQuery("delete from TMS_STOCKUSINGINFO where TRAINDIAGRAMID = '" & sCurDiaID & "'")

        TMSlocalDataSet.TMS_TIMETABLEINFO.Clear()
        ' TMSlocalDataSet.Fill("TMS_TIMETABLEINFO")
        TMSlocalDataSet.TMS_STOCKUSINGINFO.Clear()
        'TMSlocalDataSet.Fill("TMS_STOCKUSINGINFO")
        Dim i, j As Integer
        ' Dim sCurName As String
        Dim nID As Integer
        Dim sCheCi As String
        Dim sStaName As String
        Dim sDaoTime As String
        Dim sFaTime As String
        Dim sStopLine As String
        Dim nSta As Integer

        '�������г����Σ���֤��Ψһ��
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                TrainInf(i).Train = FormatPrintTrainHou(i, 3)
            End If
        Next

        nID = 1
        ProBar.Visible = True
        ProBar.Maximum = 1000
        ProBar.Value = 0
        Dim nIFin As Boolean
        Dim k As Integer
        Dim tmpSta As New Generic.List(Of String)
        For i = 1 To UBound(TrainInf)
            nID = 1
            tmpSta.Clear()
            If TrainInf(i).Train <> "" Then
                'If i = 359 Then Stop
                With TrainInf(i)
                    For j = 1 To UBound(TrainInf(i).nPathID)
                        nIFin = False
                        sCheCi = TrainInf(i).Train
                        sStaName = StationInf(TrainInf(i).nPathID(j)).sStationName
                        nSta = TrainInf(i).nPathID(j)
                        For k = 1 To tmpSta.Count
                            If tmpSta.Item(k - 1) = sStaName Then
                                nIFin = True
                            End If
                        Next
                        If nIFin = False Then
                            tmpSta.Add(sStaName)
                            If .Starting(nSta) <> -1 Then
                                sDaoTime = .Arrival(nSta) ' dTime(.Arrival(nSta), 0)
                            Else
                                sDaoTime = -1
                            End If
                            If .Starting(nSta) <> -1 Then
                                sFaTime = .Starting(nSta) ' dTime(.Starting(nSta), 0)
                            Else
                                sFaTime = "-1"
                            End If
                            If .StopLine(nSta) = "" Then
                                sStopLine = "��"
                            Else
                                sStopLine = .StopLine(nSta)
                            End If

                            TMSlocalDataSet.TMS_TIMETABLEINFO.AddTMS_TIMETABLEINFORow( _
                                     sCurDiaID, nID, sCheCi, sStaName, sDaoTime, sFaTime, sStopLine)
                            nID = nID + 1
                        End If
                    Next

                    'ʼ���۷����յ��۷���Ϣ
                    sCheCi = TrainInf(i).Train
                    sStaName = "Aʼ���۷�A" 'StationInf(TrainInf(i).nPathID(j)).sStationName
                    If .sStartZFArrival <> -1 Then
                        sDaoTime = .sStartZFArrival ' dTime(.Arrival(nSta), 0)
                    Else
                        sDaoTime = -1
                    End If
                    If .sStartZFStarting <> -1 Then
                        sFaTime = .sStartZFStarting ' dTime(.Starting(nSta), 0)
                    Else
                        sFaTime = "-1"
                    End If
                    If .sStartZFLine = "" Then
                        sStopLine = "��"
                    Else
                        sStopLine = .sStartZFLine
                    End If


                    TMSlocalDataSet.TMS_TIMETABLEINFO.AddTMS_TIMETABLEINFORow( _
                             sCurDiaID, nID, sCheCi, sStaName, sDaoTime, sFaTime, sStopLine)

                    nID = nID + 1

                    sStaName = "B�յ��۷�B" 'StationInf(TrainInf(i).nPathID(j)).sStationName
                    If .sEndZFArrival <> -1 Then
                        sDaoTime = .sEndZFArrival ' dTime(.Arrival(nSta), 0)
                    Else
                        sDaoTime = -1
                    End If
                    If .sEndZFStarting <> -1 Then
                        sFaTime = .sEndZFStarting ' dTime(.Starting(nSta), 0)
                    Else
                        sFaTime = "-1"
                    End If
                    If .sEndZFLine = "" Then
                        sStopLine = "��"
                    Else
                        sStopLine = .sEndZFLine
                    End If

                    TMSlocalDataSet.TMS_TIMETABLEINFO.AddTMS_TIMETABLEINFORow( _
                             sCurDiaID, nID, sCheCi, sStaName, sDaoTime, sFaTime, sStopLine)
                    nID = nID + 1
                End With
            End If
            ProBar.Value = Int(i * 700 / UBound(TrainInf))
        Next
        TMSlocalDataSet.Update("TMS_TIMETABLEINFO")

        Dim nShunXu As Integer
        Dim CheDiID As String
        Dim LianGuaCheCi As String
        Dim nLinaJieShunXu As Integer
        Dim LieCheXingZhe As String
        Dim sJiaoLuleiXing As String
        Dim sTingZhanbiaoChi As String
        Dim sPrintCheCi As String
        Dim sBiaoChiLeiXing As String
        Dim bifZheFanYueShu As String
        Dim sLineStyle As String
        Dim sLineColor As String
        Dim sLineWidth As String
        Dim sCheDiLineStyle As String
        Dim sCheDiLineColor As String
        Dim sCheDiLineWidth As String

        'sCurName = tmpTableID & "����ʹ�÷���"
        'sFile = dFile.OpenRecordset(sCurName)
        For i = 1 To UBound(ChediInfo)
            For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                nShunXu = i
                CheDiID = ChediInfo(i).sCheCiHao
                If CheDiID.Trim = "" Then
                    CheDiID = "NULL"
                End If
                LianGuaCheCi = TrainInf(ChediInfo(i).nLinkTrain(j)).Train
                nLinaJieShunXu = j
                LieCheXingZhe = TrainInf(ChediInfo(i).nLinkTrain(j)).sTrainXingZhi
                sJiaoLuleiXing = TrainInf(ChediInfo(i).nLinkTrain(j)).sJiaoLuName
                sTingZhanbiaoChi = TrainInf(ChediInfo(i).nLinkTrain(j)).sStopSclaeName

                'If TrainInf(ChediInfo(i).nLinkTrain(j)).sPrintTrain = "" Then
                '    sPrintCheCi = i
                'Else
                sPrintCheCi = TrainInf(ChediInfo(i).nLinkTrain(j)).sPrintTrain
                'End If
                sBiaoChiLeiXing = TrainInf(ChediInfo(i).nLinkTrain(j)).sRunScaleName
                If ChediInfo(i).bIfGouWang = True Then
                    bifZheFanYueShu = 1
                Else
                    bifZheFanYueShu = 0
                End If
                'bifZheFanYueShu = ChediInfo(i).bIfGouWang 'TrainInf(ChediInfo(i).nLinkTrain(j)).nZfLimit
                sLineStyle = GetLineStyleTextFromStyleName(TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineStyle)
                sLineColor = System.Drawing.ColorTranslator.ToHtml(TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineColor)
                sLineWidth = TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineWidth

                sCheDiLineStyle = GetLineStyleTextFromStyleName(ChediInfo(i).PrintCheDiLinkStyle)
                sCheDiLineColor = System.Drawing.ColorTranslator.ToHtml(ChediInfo(i).PrintCheDiLinkColor)
                sCheDiLineWidth = ChediInfo(i).PrintCheDiLinkWidth

                TMSlocalDataSet.TMS_STOCKUSINGINFO.AddTMS_STOCKUSINGINFORow( _
                         sCurDiaID, nShunXu, CheDiID, LianGuaCheCi, nLinaJieShunXu, LieCheXingZhe, sJiaoLuleiXing, sPrintCheCi, _
                         sBiaoChiLeiXing, sTingZhanbiaoChi, bifZheFanYueShu, sLineStyle, sLineColor, sLineWidth, sCheDiLineStyle, sCheDiLineColor, sCheDiLineWidth)
            Next j
            ProBar.Value = 700 + Int(i * 300 / UBound(ChediInfo))
        Next i
        TMSlocalDataSet.Update("TMS_STOCKUSINGINFO")

        ProBar.Visible = False
    End Sub

    '����ʱ�̺ͳ�����Ϣ
    Public Sub SaveSkbTimeTableByDAO(ByVal tmpTableID As String, ByVal ProBar As ToolStripProgressBar)


        Call DeleteSKBTimeTable(tmpTableID)

        Dim myWS As dao.Workspace
        Dim DE As dao.DBEngine = New dao.DBEngine
        myWS = DE.Workspaces(0)
        Dim dFile As dao.Database
        Dim sFile As dao.Recordset
        Dim i, j As Integer
        dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        Dim sCurName As String
        dFile.TableDefs.Refresh()
        dFile.Recordsets.Refresh()
        Dim nID As Integer
        Dim sCheCi As String
        Dim sStaName As String
        Dim sDaoTime As String
        Dim sFaTime As String
        Dim sStopLine As String
        Dim nSta As Integer

        '�������г����Σ���֤��Ψһ��
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                TrainInf(i).Train = FormatPrintTrainHou(i, 3)
            End If
        Next

        nID = 1
        sCurName = tmpTableID & "�г�ʱ����Ϣ"
        sFile = dFile.OpenRecordset(sCurName)
        ProBar.Visible = True
        ProBar.Maximum = 1000
        ProBar.Value = 0
        Dim nIFin As Boolean
        Dim k As Integer
        Dim tmpSta As New Generic.List(Of String)
        For i = 1 To UBound(TrainInf)
            nID = 1
            tmpSta.Clear()
            If TrainInf(i).Train <> "" Then
                'If i = 359 Then Stop
                With TrainInf(i)
                    For j = 1 To UBound(TrainInf(i).nPathID)
                        nIFin = False
                        sCheCi = TrainInf(i).Train
                        sStaName = StationInf(TrainInf(i).nPathID(j)).sStationName
                        nSta = TrainInf(i).nPathID(j)
                        For k = 1 To tmpSta.Count
                            If tmpSta.Item(k - 1) = sStaName Then
                                nIFin = True
                            End If
                        Next
                        If nIFin = False Then
                            tmpSta.Add(sStaName)
                            If .Starting(nSta) <> -1 Then
                                sDaoTime = .Arrival(nSta) ' dTime(.Arrival(nSta), 0)
                            Else
                                sDaoTime = -1
                            End If
                            If .Starting(nSta) <> -1 Then
                                sFaTime = .Starting(nSta) ' dTime(.Starting(nSta), 0)
                            Else
                                sFaTime = "-1"
                            End If
                            If .StopLine(nSta) = "" Then
                                sStopLine = "��"
                            Else
                                sStopLine = .StopLine(nSta)
                            End If

                            sFile.AddNew()
                            sFile.Fields("ID").Value = nID
                            sFile.Fields("����").Value = sCheCi
                            sFile.Fields("��վ����").Value = sStaName
                            sFile.Fields("����").Value = sDaoTime
                            sFile.Fields("����").Value = sFaTime
                            sFile.Fields("ͣվ�ɵ�").Value = sStopLine
                            sFile.Update()
                            nID = nID + 1
                        End If
                    Next

                    'ʼ���۷����յ��۷���Ϣ
                    sCheCi = TrainInf(i).Train
                    sStaName = "Aʼ���۷�A" 'StationInf(TrainInf(i).nPathID(j)).sStationName
                    If .sStartZFArrival <> -1 Then
                        sDaoTime = .sStartZFArrival ' dTime(.Arrival(nSta), 0)
                    Else
                        sDaoTime = -1
                    End If
                    If .sStartZFStarting <> -1 Then
                        sFaTime = .sStartZFStarting ' dTime(.Starting(nSta), 0)
                    Else
                        sFaTime = "-1"
                    End If
                    If .sStartZFLine = "" Then
                        sStopLine = "��"
                    Else
                        sStopLine = .sStartZFLine
                    End If

                    sFile.AddNew()
                    sFile.Fields("ID").Value = nID
                    sFile.Fields("����").Value = sCheCi
                    sFile.Fields("��վ����").Value = sStaName
                    sFile.Fields("����").Value = sDaoTime
                    sFile.Fields("����").Value = sFaTime
                    sFile.Fields("ͣվ�ɵ�").Value = sStopLine
                    sFile.Update()
                    nID = nID + 1

                    sStaName = "B�յ��۷�B" 'StationInf(TrainInf(i).nPathID(j)).sStationName
                    If .sEndZFArrival <> -1 Then
                        sDaoTime = .sEndZFArrival ' dTime(.Arrival(nSta), 0)
                    Else
                        sDaoTime = -1
                    End If
                    If .sEndZFStarting <> -1 Then
                        sFaTime = .sEndZFStarting ' dTime(.Starting(nSta), 0)
                    Else
                        sFaTime = "-1"
                    End If
                    If .sEndZFLine = "" Then
                        sStopLine = "��"
                    Else
                        sStopLine = .sEndZFLine
                    End If

                    sFile.AddNew()
                    sFile.Fields("ID").Value = nID
                    sFile.Fields("����").Value = sCheCi
                    sFile.Fields("��վ����").Value = sStaName
                    sFile.Fields("����").Value = sDaoTime
                    sFile.Fields("����").Value = sFaTime
                    sFile.Fields("ͣվ�ɵ�").Value = sStopLine
                    sFile.Update()
                    nID = nID + 1
                End With
            End If
            ProBar.Value = Int(i * 700 / UBound(TrainInf))
        Next

        Dim nShunXu As Integer
        Dim CheDiID As String
        Dim LianGuaCheCi As String
        Dim nLinaJieShunXu As Integer
        Dim LieCheXingZhe As String
        Dim sJiaoLuleiXing As String
        Dim sTingZhanbiaoChi As String
        Dim sPrintCheCi As String
        Dim sBiaoChiLeiXing As String
        Dim bifZheFanYueShu As String
        Dim sLineStyle As String
        Dim sLineColor As String
        Dim sLineWidth As String
        Dim sCheDiLineStyle As String
        Dim sCheDiLineColor As String
        Dim sCheDiLineWidth As String

        sCurName = tmpTableID & "����ʹ�÷���"
        sFile = dFile.OpenRecordset(sCurName)
        For i = 1 To UBound(ChediInfo)
            For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                nShunXu = i
                CheDiID = ChediInfo(i).sCheCiHao
                If CheDiID.Trim = "" Then
                    CheDiID = "NULL"
                End If
                LianGuaCheCi = TrainInf(ChediInfo(i).nLinkTrain(j)).Train
                nLinaJieShunXu = j
                LieCheXingZhe = TrainInf(ChediInfo(i).nLinkTrain(j)).sTrainXingZhi
                sJiaoLuleiXing = TrainInf(ChediInfo(i).nLinkTrain(j)).sJiaoLuName
                sTingZhanbiaoChi = TrainInf(ChediInfo(i).nLinkTrain(j)).sStopSclaeName

                'If TrainInf(ChediInfo(i).nLinkTrain(j)).sPrintTrain = "" Then
                '    sPrintCheCi = i
                'Else
                sPrintCheCi = TrainInf(ChediInfo(i).nLinkTrain(j)).sPrintTrain
                'End If
                sBiaoChiLeiXing = TrainInf(ChediInfo(i).nLinkTrain(j)).sRunScaleName
                If ChediInfo(i).bIfGouWang = True Then
                    bifZheFanYueShu = 1
                Else
                    bifZheFanYueShu = 0
                End If
                'bifZheFanYueShu = ChediInfo(i).bIfGouWang 'TrainInf(ChediInfo(i).nLinkTrain(j)).nZfLimit
                sLineStyle = GetLineStyleTextFromStyleName(TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineStyle)
                sLineColor = System.Drawing.ColorTranslator.ToHtml(TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineColor)
                sLineWidth = TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineWidth

                sCheDiLineStyle = GetLineStyleTextFromStyleName(ChediInfo(i).PrintCheDiLinkStyle)
                sCheDiLineColor = System.Drawing.ColorTranslator.ToHtml(ChediInfo(i).PrintCheDiLinkColor)
                sCheDiLineWidth = ChediInfo(i).PrintCheDiLinkWidth

                sFile.AddNew()
                sFile.Fields("����˳��").Value = nShunXu
                sFile.Fields("����ID").Value = CheDiID
                sFile.Fields("���ҳ���").Value = LianGuaCheCi
                sFile.Fields("����˳��").Value = nLinaJieShunXu
                sFile.Fields("�г�����").Value = LieCheXingZhe
                sFile.Fields("��·����").Value = sJiaoLuleiXing
                sFile.Fields("ͣվ���").Value = sTingZhanbiaoChi
                sFile.Fields("�������").Value = sPrintCheCi
                sFile.Fields("�������").Value = sBiaoChiLeiXing
                sFile.Fields("�Ƿ��۷�Լ��").Value = bifZheFanYueShu
                sFile.Fields("����������").Value = sLineStyle
                sFile.Fields("��������ɫ").Value = sLineColor
                sFile.Fields("�������߿�").Value = sLineWidth
                sFile.Fields("����������").Value = sCheDiLineStyle
                sFile.Fields("��������ɫ").Value = sCheDiLineColor
                sFile.Fields("�������߿�").Value = sCheDiLineWidth
                sFile.Update()
            Next j
            ProBar.Value = 700 + Int(i * 300 / UBound(ChediInfo))
        Next i
        ProBar.Visible = False
        dFile.Close()
    End Sub

    '����ʱ�̺ͳ�����Ϣ
    Public Sub SaveSkbTimeTable(ByVal tmpTableName As String, ByVal ProBar As ToolStripProgressBar)
        Dim Str As String
        Call DeleteSKBTimeTable(tmpTableName)
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim nID As Integer
        Dim sCheCi As String
        Dim sStaName As String
        Dim sDaoTime As String
        Dim sFaTime As String
        Dim sStopLine As String
        Dim nSta As Integer
        Dim i As Integer
        Dim j As Integer
        nID = 1
        Dim sCurName As String
        sCurName = tmpTableName & "�г�ʱ����Ϣ"
        ProBar.Visible = True
        ProBar.Maximum = 1000
        ProBar.Value = 0
        For i = 1 To UBound(TrainInf)
            nID = 1
            If TrainInf(i).Train <> "" Then
                With TrainInf(i)
                    For j = 1 To UBound(TrainInf(i).nPathID)
                        sCheCi = TrainInf(i).Train
                        sStaName = StationInf(TrainInf(i).nPathID(j)).sStationName
                        nSta = TrainInf(i).nPathID(j)
                        If .Starting(nSta) <> -1 Then
                            sDaoTime = .Arrival(nSta) ' dTime(.Arrival(nSta), 0)
                        Else
                            sDaoTime = -1
                        End If
                        If .Starting(nSta) <> -1 Then
                            sFaTime = .Starting(nSta) ' dTime(.Starting(nSta), 0)
                        Else
                            sFaTime = "-1"
                        End If
                        If .StopLine(nSta) = "" Then
                            sStopLine = "��"
                        Else
                            sStopLine = .StopLine(nSta)
                        End If
                        Str = "insert into " & sCurName & " (ID,����,��վ����,����,����,ͣվ�ɵ�) values (" & _
                                                   nID & ", '" & _
                                                    sCheCi & "', '" & _
                                                    sStaName & "', '" & _
                                                    sDaoTime & "', '" & _
                                                    sFaTime & "', '" & _
                                                    sStopLine & "')"
                        Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                        Mcom1.ExecuteNonQuery()
                        nID = nID + 1
                    Next

                    'ʼ���۷����յ��۷���Ϣ
                    sCheCi = TrainInf(i).Train
                    sStaName = "Aʼ���۷�A" 'StationInf(TrainInf(i).nPathID(j)).sStationName
                    If .sStartZFArrival <> -1 Then
                        sDaoTime = .sStartZFArrival ' dTime(.Arrival(nSta), 0)
                    Else
                        sDaoTime = -1
                    End If
                    If .sStartZFStarting <> -1 Then
                        sFaTime = .sStartZFStarting ' dTime(.Starting(nSta), 0)
                    Else
                        sFaTime = "-1"
                    End If
                    If .sStartZFLine = "" Then
                        sStopLine = "��"
                    Else
                        sStopLine = .sStartZFLine
                    End If
                    Str = "insert into " & sCurName & " (ID,����,��վ����,����,����,ͣվ�ɵ�) values (" & _
                                               nID & ", '" & _
                                                sCheCi & "', '" & _
                                                sStaName & "', '" & _
                                                sDaoTime & "', '" & _
                                                sFaTime & "', '" & _
                                                sStopLine & "')"
                    Dim Mcom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                    Mcom2.ExecuteNonQuery()
                    nID = nID + 1

                    sStaName = "B�յ��۷�B" 'StationInf(TrainInf(i).nPathID(j)).sStationName
                    If .sEndZFArrival <> -1 Then
                        sDaoTime = .sEndZFArrival ' dTime(.Arrival(nSta), 0)
                    Else
                        sDaoTime = -1
                    End If
                    If .sEndZFStarting <> -1 Then
                        sFaTime = .sEndZFStarting ' dTime(.Starting(nSta), 0)
                    Else
                        sFaTime = "-1"
                    End If
                    If .sEndZFLine = "" Then
                        sStopLine = "��"
                    Else
                        sStopLine = .sEndZFLine
                    End If
                    Str = "insert into " & sCurName & " (ID,����,��վ����,����,����,ͣվ�ɵ�) values (" & _
                                               nID & ", '" & _
                                                sCheCi & "', '" & _
                                                sStaName & "', '" & _
                                                sDaoTime & "', '" & _
                                                sFaTime & "', '" & _
                                                sStopLine & "')"
                    Dim Mcom3 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                    Mcom3.ExecuteNonQuery()
                    nID = nID + 1

                End With
            End If
            ProBar.Value = Int(i * 700 / UBound(TrainInf))
        Next

        Dim nShunXu As Integer
        Dim CheDiID As String
        Dim LianGuaCheCi As String
        Dim nLinaJieShunXu As Integer
        Dim LieCheXingZhe As String
        Dim sJiaoLuleiXing As String
        Dim sTingZhanbiaoChi As String
        Dim sPrintCheCi As String
        Dim sBiaoChiLeiXing As String
        Dim bifZheFanYueShu As String
        Dim sLineStyle As String
        Dim sLineColor As String
        Dim sLineWidth As String
        Dim sCheDiLineStyle As String
        Dim sCheDiLineColor As String
        Dim sCheDiLineWidth As String

        sCurName = tmpTableName & "����ʹ�÷���"
        For i = 1 To UBound(ChediInfo)
            For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                nShunXu = i
                CheDiID = ChediInfo(i).sCheCiHao
                LianGuaCheCi = TrainInf(ChediInfo(i).nLinkTrain(j)).Train
                nLinaJieShunXu = j
                LieCheXingZhe = TrainInf(ChediInfo(i).nLinkTrain(j)).TrainStyle
                sJiaoLuleiXing = TrainInf(ChediInfo(i).nLinkTrain(j)).sJiaoLuName
                sTingZhanbiaoChi = TrainInf(ChediInfo(i).nLinkTrain(j)).sStopSclaeName
                sPrintCheCi = ""
                If TrainInf(ChediInfo(i).nLinkTrain(j)).sPrintTrain = "" Then
                    'sPrintCheCi = i
                Else
                    sPrintCheCi = TrainInf(ChediInfo(i).nLinkTrain(j)).sPrintTrain
                End If
                sBiaoChiLeiXing = TrainInf(ChediInfo(i).nLinkTrain(j)).sRunScaleName
                bifZheFanYueShu = TrainInf(ChediInfo(i).nLinkTrain(j)).nZfLimit
                sLineStyle = GetLineStyleTextFromStyleName(TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineStyle)
                sLineColor = System.Drawing.ColorTranslator.ToHtml(TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineColor)
                sLineWidth = TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineWidth
                sCheDiLineStyle = GetLineStyleTextFromStyleName(ChediInfo(i).PrintCheDiLinkStyle)
                sCheDiLineColor = System.Drawing.ColorTranslator.ToHtml(ChediInfo(i).PrintCheDiLinkColor)
                sCheDiLineWidth = ChediInfo(i).PrintCheDiLinkWidth
                Str = "insert into " & sCurName & " (����˳��,����ID,���ҳ���,����˳��,�г�����,��·����,ͣվ���,�������,�������,�Ƿ��۷�Լ��,����������,��������ɫ,�������߿�,����������,��������ɫ,�������߿�) values (" & _
                                           nShunXu & ", '" & _
                                            CheDiID & "', '" & _
                                            LianGuaCheCi & "', '" & _
                                            nLinaJieShunXu & "', '" & _
                                            LieCheXingZhe & "', '" & _
                                            sJiaoLuleiXing & "', '" & _
                                            sTingZhanbiaoChi & "', '" & _
                                            sPrintCheCi & "', '" & _
                                            sBiaoChiLeiXing & "', '" & _
                                            bifZheFanYueShu & "', '" & _
                                            sLineStyle & "', '" & _
                                            sLineColor & "', '" & _
                                            sLineWidth & "', '" & _
                                            sCheDiLineStyle & "', '" & _
                                            sCheDiLineColor & "', '" & _
                                            sCheDiLineWidth & "')"
                Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                Mcom1.ExecuteNonQuery()
            Next j
            ProBar.Value = 700 + Int(i * 300 / UBound(ChediInfo))
        Next i
        ProBar.Visible = False
        MyConn.Close()
    End Sub

    '�õ��г���ָ����վ�ķ���
    Public Function GetStartTimeFromStaName(ByVal sSta As String, ByVal nTrain As Integer) As Long
        Dim nSta As Integer
        GetStartTimeFromStaName = -1
        nSta = StaNameToStaInfID(sSta)
        Dim i As Integer
        For i = 1 To UBound(TrainInf(nTrain).nPathID)
            If StationInf(TrainInf(nTrain).nPathID(i)).sStationName = sSta Then
                GetStartTimeFromStaName = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(i))
                Exit For
            End If
        Next i
        If sSta = "ʼ��վ" Then
            If UBound(TrainInf(nTrain).nPathID) > 0 Then
                GetStartTimeFromStaName = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
            End If
        End If
        'If GetStartTimeFromStaName = -1 Then
        '    If UBound(TrainInf(nTrain).nPathID) > 0 Then
        '        GetStartTimeFromStaName = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
        '    End If
        'End If
    End Function

    '�õ��г���ָ����վ�ĵ���
    Public Function GetArrivalTimeFromStaName(ByVal sSta As String, ByVal nTrain As Integer) As Long
        Dim nSta As Integer
        GetArrivalTimeFromStaName = -1
        nSta = StaNameToStaInfID(sSta)
        Dim i As Integer
        For i = 1 To UBound(TrainInf(nTrain).nPathID)
            If StationInf(TrainInf(nTrain).nPathID(i)).sStationName = sSta Then
                GetArrivalTimeFromStaName = TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(i))
                Exit For
            End If
        Next i
    End Function

    'Public Sub InputStationGudaoAndJinLuInfByDAO()

    '    Dim myWS As dao.Workspace
    '    Dim DE As dao.DBEngine = New dao.DBEngine
    '    myWS = DE.Workspaces(0)
    '    Dim dFile As dao.Database
    '    Dim sFile As dao.Recordset
    '    Dim i, j As Integer
    '    Dim nNum As Integer
    '    Dim Str As String
    '    dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)


    '    For i = 1 To UBound(StationInf)
    '        Dim strTable2 As String = "select * from ��վ��Ϣ where վ��='" & StationInf(i).sStationName & "'"
    '        sFile = dFile.OpenRecordset(strTable2)
    '        nNum = 0
    '        If sFile.RecordCount > 0 Then
    '            sFile.MoveLast()
    '            nNum = sFile.RecordCount
    '        End If
    '        If nNum > 0 Then
    '            sFile.MoveFirst()
    '            StationInf(i).sStaStyle = sFile.Fields("����").Value.ToString.Trim ' myTable2.Rows(0).Item("����").ToString.Trim
    '            StationInf(i).sAtLineName = sFile.Fields("��·����").Value.ToString.Trim 'myTable2.Rows(0).Item("��·����").ToString.Trim
    '            StationInf(i).sPrintStaName = sFile.Fields("���վ��").Value.ToString.Trim
    '            StationInf(i).sStaProperity = sFile.Fields("����").Value.ToString.Trim ' myTable2.Rows(0).Item("����").ToString.Trim
    '            StationInf(i).sStationProp = ChaStProp(sFile.Fields("����").Value.ToString.Trim, sFile.Fields("����").Value.ToString.Trim) 'ChaStProp(myTable2.Rows(0).Item("����").ToString.Trim, myTable2.Rows(0).Item("����").ToString.Trim)
    '            StationInf(i).sEnglishName = sFile.Fields("Ӣ�ļ��").Value.ToString.Trim 'myTable2.Rows(0).Item("Ӣ�ļ��").ToString.Trim
    '        End If

    '        Dim strTable3 As String = "select * from �߶���Ϣ�� where ��վ����='" & StationInf(i).sStationName & "'"
    '        sFile = dFile.OpenRecordset(strTable3)
    '        nNum = 0
    '        If sFile.RecordCount > 0 Then
    '            sFile.MoveLast()
    '            nNum = sFile.RecordCount
    '        End If

    '        StationInf(i).nStLineNum = 0
    '        ReDim StationInf(i).sStLineNo(0)
    '        ReDim StationInf(i).nStLineUse(0)
    '        ReDim StationInf(i).sLineUse(0)
    '        ReDim StationInf(i).sUpOrDownUse(0)
    '        ReDim StationInf(i).sGuDaoUseSeq(0)
    '        ReDim StationInf(i).sGuDaoName(0)
    '        If nNum > 0 Then
    '            sFile.MoveFirst()
    '            For j = 1 To nNum
    '                Str = sFile.Fields("�߶�����").Value.ToString.Trim 'myTable3.Rows(j - 1).Item("�߶�����").ToString.Trim
    '                If Str.Length >= 3 Then
    '                    If Str.Substring(Str.Length - 3) = "�ɵ���" Then
    '                        StationInf(i).nStLineNum = StationInf(i).nStLineNum + 1
    '                        ReDim Preserve StationInf(i).sStLineNo(UBound(StationInf(i).sStLineNo) + 1)
    '                        ReDim Preserve StationInf(i).nStLineUse(UBound(StationInf(i).sStLineNo) + 1)
    '                        ReDim Preserve StationInf(i).sLineUse(UBound(StationInf(i).sStLineNo) + 1)
    '                        ReDim Preserve StationInf(i).sUpOrDownUse(UBound(StationInf(i).sStLineNo) + 1)
    '                        ReDim Preserve StationInf(i).sGuDaoUseSeq(UBound(StationInf(i).sStLineNo) + 1)
    '                        ReDim Preserve StationInf(i).sGuDaoName(UBound(StationInf(i).sStLineNo) + 1)
    '                        StationInf(i).sStLineNo(UBound(StationInf(i).sStLineNo)) = sFile.Fields("�ɵ��������").Value.ToString.Trim ' myTable3.Rows(j - 1).Item("�ɵ��������").ToString.Trim
    '                        StationInf(i).sLineUse(UBound(StationInf(i).sStLineNo)) = sFile.Fields("�ɵ�����").Value.ToString.Trim 'myTable3.Rows(j - 1).Item("�ɵ�����").ToString.Trim
    '                        StationInf(i).sUpOrDownUse(UBound(StationInf(i).sStLineNo)) = sFile.Fields("�ɵ���;").Value.ToString.Trim 'myTable3.Rows(j - 1).Item("�ɵ���;").ToString.Trim
    '                        StationInf(i).sGuDaoUseSeq(UBound(StationInf(i).sStLineNo)) = sFile.Fields("�ɵ�ʹ��˳��").Value.ToString.Trim 'myTable3.Rows(j - 1).Item("�ɵ�ʹ��˳��").ToString.Trim
    '                        StationInf(i).nStLineUse(UBound(StationInf(i).sStLineNo)) = ChaLineUse(sFile.Fields("�ɵ�����").Value.ToString.Trim, sFile.Fields("�ɵ���;").Value.ToString.Trim) ' ChaLineUse(myTable3.Rows(j - 1).Item("�ɵ�����").ToString.Trim, myTable3.Rows(j - 1).Item("�ɵ���;").ToString.Trim)
    '                        StationInf(i).sGuDaoName(UBound(StationInf(i).sStLineNo)) = sFile.Fields("����ģ��").Value.ToString.Trim
    '                    End If
    '                End If
    '                sFile.MoveNext()
    '            Next
    '        End If

    '        '���복վ��·�ͷֲ�վ�ɵ�ʹ��
    '        ReDim StationInf(i).sTrackUse(0)

    '        'ReDim StationInf(i).sTrackUse.sStartUse(0)
    '        'ReDim StationInf(i).sTrackUse.sEndUse(0)
    '        'ReDim StationInf(i).sTrackUse.sReturnUse(0)
    '        'ReDim StationInf(i).sTrackUse.sDownPassUse(0)
    '        'ReDim StationInf(i).sTrackUse.sUpPassUse(0)
    '        'ReDim StationInf(i).sTrackUse.sDownStopUse(0)
    '        'ReDim StationInf(i).sTrackUse.sUpStopUse(0)
    '        Dim tmpStr As String
    '        Dim tmpNum() As String
    '        Dim k As Integer
    '        Dim strTable4 As String = "select * from ��վ�ɵ�ʹ�� where ��վ����='" & StationInf(i).sStationName & "'"
    '        sFile = dFile.OpenRecordset(strTable4)
    '        nNum = 0
    '        If sFile.RecordCount > 0 Then
    '            sFile.MoveLast()
    '            nNum = sFile.RecordCount
    '        End If
    '        ReDim StationInf(i).sTrackUse(nNum)
    '        If nNum > 0 Then
    '            sFile.MoveFirst()
    '            For j = 1 To nNum
    '                StationInf(i).sTrackUse(j).sJiaoLuName = sFile.Fields("��·����").Value.ToString.Trim
    '                ReDim StationInf(i).sTrackUse(j).sStartUse(0)
    '                ReDim StationInf(i).sTrackUse(j).sEndUse(0)
    '                ReDim StationInf(i).sTrackUse(j).sReturnUse(0)
    '                ReDim StationInf(i).sTrackUse(j).sDownPassUse(0)
    '                ReDim StationInf(i).sTrackUse(j).sUpPassUse(0)
    '                ReDim StationInf(i).sTrackUse(j).sDownStopUse(0)
    '                ReDim StationInf(i).sTrackUse(j).sUpStopUse(0)
    '                tmpStr = sFile.Fields("ʼ���ɵ�ʹ��˳��").Value.ToString.Trim
    '                ReDim tmpNum(0)
    '                Call SplitString(tmpNum, tmpStr)
    '                If UBound(tmpNum) > 0 Then
    '                    ReDim StationInf(i).sTrackUse(j).sStartUse(UBound(tmpNum))
    '                    For k = 1 To UBound(tmpNum)
    '                        StationInf(i).sTrackUse(j).sStartUse(k) = tmpNum(k)
    '                    Next
    '                End If

    '                tmpStr = sFile.Fields("�յ��ɵ�ʹ��˳��").Value.ToString.Trim
    '                ReDim tmpNum(0)
    '                Call SplitString(tmpNum, tmpStr)
    '                If UBound(tmpNum) > 0 Then
    '                    ReDim StationInf(i).sTrackUse(j).sEndUse(UBound(tmpNum))
    '                    For k = 1 To UBound(tmpNum)
    '                        StationInf(i).sTrackUse(j).sEndUse(k) = tmpNum(k)
    '                    Next
    '                End If

    '                tmpStr = sFile.Fields("�۷��ɵ�ʹ��˳��").Value.ToString.Trim
    '                ReDim tmpNum(0)
    '                Call SplitString(tmpNum, tmpStr)
    '                If UBound(tmpNum) > 0 Then
    '                    ReDim StationInf(i).sTrackUse(j).sReturnUse(UBound(tmpNum))
    '                    For k = 1 To UBound(tmpNum)
    '                        StationInf(i).sTrackUse(j).sReturnUse(k) = tmpNum(k)
    '                    Next
    '                End If

    '                tmpStr = sFile.Fields("����ͣվ�ɵ�ʹ��˳��").Value.ToString.Trim
    '                ReDim tmpNum(0)
    '                Call SplitString(tmpNum, tmpStr)
    '                If UBound(tmpNum) > 0 Then
    '                    ReDim StationInf(i).sTrackUse(j).sDownStopUse(UBound(tmpNum))
    '                    For k = 1 To UBound(tmpNum)
    '                        StationInf(i).sTrackUse(j).sDownStopUse(k) = tmpNum(k)
    '                    Next
    '                End If

    '                tmpStr = sFile.Fields("����ͣվ�ɵ�ʹ��˳��").Value.ToString.Trim
    '                ReDim tmpNum(0)
    '                Call SplitString(tmpNum, tmpStr)
    '                If UBound(tmpNum) > 0 Then
    '                    ReDim StationInf(i).sTrackUse(j).sUpStopUse(UBound(tmpNum))
    '                    For k = 1 To UBound(tmpNum)
    '                        StationInf(i).sTrackUse(j).sUpStopUse(k) = tmpNum(k)
    '                    Next
    '                End If

    '                tmpStr = sFile.Fields("����ͨ���ɵ�ʹ��˳��").Value.ToString.Trim
    '                ReDim tmpNum(0)
    '                Call SplitString(tmpNum, tmpStr)
    '                If UBound(tmpNum) > 0 Then
    '                    ReDim StationInf(i).sTrackUse(j).sDownPassUse(UBound(tmpNum))
    '                    For k = 1 To UBound(tmpNum)
    '                        StationInf(i).sTrackUse(j).sDownPassUse(k) = tmpNum(k)
    '                    Next
    '                End If

    '                tmpStr = sFile.Fields("����ͨ���ɵ�ʹ��˳��").Value.ToString.Trim
    '                ReDim tmpNum(0)
    '                Call SplitString(tmpNum, tmpStr)
    '                If UBound(tmpNum) > 0 Then
    '                    ReDim StationInf(i).sTrackUse(j).sUpPassUse(UBound(tmpNum))
    '                    For k = 1 To UBound(tmpNum)
    '                        StationInf(i).sTrackUse(j).sUpPassUse(k) = tmpNum(k)
    '                    Next
    '                End If
    '                sFile.MoveNext()
    '            Next
    '        End If


    '        '���´�����복վ��·��Ϣ
    '        ReDim StationInf(i).sCrossUse(0)

    '        sFile = dFile.OpenRecordset("��վ��·��Ϣ")
    '        '�ж����ݿ��Ƿ���ȷ
    '        Dim isRight As Boolean
    '        isRight = False
    '        For j = 1 To sFile.Fields.Count
    '            If sFile.Fields(j - 1).Name = "ͨ���Ĺ�����" Then
    '                isRight = True
    '                Exit For
    '            End If
    '        Next

    '        If isRight = True Then
    '            Dim strTable5 As String = "select * from ��վ��·��Ϣ where ��վ����='" & StationInf(i).sStationName & "'"
    '            sFile = dFile.OpenRecordset(strTable5)
    '            nNum = 0
    '            If sFile.RecordCount > 0 Then
    '                sFile.MoveLast()
    '                nNum = sFile.RecordCount
    '            End If

    '            ReDim StationInf(i).sCrossUse(nNum)

    '            If nNum > 0 Then
    '                sFile.MoveFirst()
    '                For j = 1 To nNum
    '                    StationInf(i).sCrossUse(j).LinkTrackNum = sFile.Fields("��վ��ʽ").Value.ToString.Trim ' myTable5.Rows(j - 1).Item("���﷽��").ToString.Trim
    '                    StationInf(i).sCrossUse(j).LinkStaName = sFile.Fields("���ӳ�վ��").Value.ToString.Trim ' myTable5.Rows(j - 1).Item("��������").ToString.Trim
    '                    StationInf(i).sCrossUse(j).StaTrackNum = sFile.Fields("�ɵ����").Value.ToString.Trim ' myTable5.Rows(j - 1).Item("�ɵ���").ToString.Trim
    '                    StationInf(i).sCrossUse(j).PathTrackNum = SplicListofString(sFile.Fields("ͨ���Ĺ�����").Value.ToString.Trim) ' myTable5.Rows(j - 1).Item("������·").ToString.Trim
    '                    StationInf(i).sCrossUse(j).PathCrossNum = SplicListofString(sFile.Fields("ͨ���ĵ�����").Value.ToString.Trim) '  myTable5.Rows(j - 1).Item("��ѡ��·").ToString.Trim
    '                    StationInf(i).sCrossUse(j).PathControlNum = SplicListofString(sFile.Fields("ͨ���Ŀ���ģ��").Value.ToString.Trim) ' myTable5.Rows(j - 1).Item("��ע").ToString.Trim
    '                    sFile.MoveNext()
    '                Next
    '            End If
    '        End If


    '        '���´����ǳ�վ���ʱ��
    '        Dim strTable6 As String = "select * from ��վ���ʱ�� where ��վ����='" & StationInf(i).sStationName & "'"
    '        sFile = dFile.OpenRecordset(strTable6)
    '        nNum = 0
    '        If sFile.RecordCount > 0 Then
    '            sFile.MoveLast()
    '            nNum = sFile.RecordCount
    '        End If

    '        With StationInf(i)
    '            ReDim Preserve .lTaoBu(2)
    '            ReDim Preserve .lTaoHui(2)
    '            ReDim Preserve .lTaoLian1(2)
    '            ReDim Preserve .lTaoLian2(2)
    '            ReDim Preserve .lTaoTongK(2)
    '            ReDim Preserve .lTaoTongH(2)
    '            ReDim Preserve .lTaoDaoFa(2)
    '            ReDim Preserve .lTaoFaDao(2)
    '            ReDim Preserve .lTaoFaFa(2)
    '            ReDim Preserve .lTaoDaoDao(2)
    '            If nNum > 0 Then
    '                sFile.MoveFirst()
    '                .lTaoBu(1) = MinuteToSecond(sFile.Fields("�Ӳ���").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("�Ӳ���").ToString.Trim)
    '                .lTaoBu(2) = MinuteToSecond(sFile.Fields("�Ӳ���").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("�Ӳ���").ToString.Trim)
    '                .lTaoHui(1) = MinuteToSecond(sFile.Fields("�ӻ���").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("�ӻ���").ToString.Trim)
    '                .lTaoHui(2) = MinuteToSecond(sFile.Fields("�ӻ���").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("�ӻ���").ToString.Trim)
    '                .lTaoLian1(1) = MinuteToSecond(sFile.Fields("����1��").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("����1��").ToString.Trim)
    '                .lTaoLian1(2) = MinuteToSecond(sFile.Fields("����1��").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("����1��").ToString.Trim)
    '                .lTaoLian2(1) = MinuteToSecond(sFile.Fields("����2��").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("����2��").ToString.Trim)
    '                .lTaoLian2(2) = MinuteToSecond(sFile.Fields("����2��").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("����2��").ToString.Trim)
    '                .lTaoTongK(1) = MinuteToSecond(sFile.Fields("��ͨ1��").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("��ͨ1��").ToString.Trim)
    '                .lTaoTongK(2) = MinuteToSecond(sFile.Fields("��ͨ1��").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("��ͨ1��").ToString.Trim)
    '                .lTaoTongH(1) = MinuteToSecond(sFile.Fields("��ͨ2��").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("��ͨ2��").ToString.Trim)
    '                .lTaoTongH(2) = MinuteToSecond(sFile.Fields("��ͨ2��").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("��ͨ2��").ToString.Trim)
    '                .lTaoDaoFa(1) = MinuteToSecond(sFile.Fields("�ӵ�����").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("�ӵ�����").ToString.Trim)
    '                .lTaoDaoFa(2) = MinuteToSecond(sFile.Fields("�ӵ�����").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("�ӵ�����").ToString.Trim)
    '                .lTaoFaDao(1) = MinuteToSecond(sFile.Fields("�ӷ�����").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("�ӷ�����").ToString.Trim)
    '                .lTaoFaDao(2) = MinuteToSecond(sFile.Fields("�ӷ�����").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("�ӷ�����").ToString.Trim)
    '                .lTaoFaFa(1) = MinuteToSecond(sFile.Fields("�ӷ�����").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("�ӷ�����").ToString.Trim)
    '                .lTaoFaFa(2) = MinuteToSecond(sFile.Fields("�ӷ�����").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("�ӷ�����").ToString.Trim)
    '                .lTaoDaoDao(1) = MinuteToSecond(sFile.Fields("�ӵ�����").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("�ӵ�����").ToString.Trim)
    '                .lTaoDaoDao(2) = MinuteToSecond(sFile.Fields("�ӵ�����").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("�ӵ�����").ToString.Trim)
    '            End If
    '        End With


    '        '���´�����׷�ټ��ʱ��
    '        Dim strTable7 As String = "select * from ׷�ټ��ʱ�� where ��վ����='" & StationInf(i).sStationName & "'"
    '        sFile = dFile.OpenRecordset(strTable7)
    '        nNum = 0
    '        If sFile.RecordCount > 0 Then
    '            sFile.MoveLast()
    '            nNum = sFile.RecordCount
    '        End If


    '        With StationInf(i)
    '            ReDim Preserve .IKK(17)
    '            ReDim Preserve .IKH(17)
    '            ReDim Preserve .IHH(17)
    '            ReDim Preserve .IHK(17)
    '            If nNum > 0 Then
    '                sFile.MoveFirst()
    '                .IKK(0) = MinuteToSecond(sFile.Fields("ͬ��ͨ��").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("ͬ��ͨ��").ToString.Trim)
    '                .IKK(1) = MinuteToSecond(sFile.Fields("ͬ�򷢷�").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("ͬ�򷢷�").ToString.Trim)
    '                .IKK(2) = MinuteToSecond(sFile.Fields("ͬ�򵽷�").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("ͬ�򵽷�").ToString.Trim)
    '                .IKK(3) = MinuteToSecond(sFile.Fields("ͬ��ͨ").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("ͬ��ͨ").ToString.Trim)
    '                .IKK(4) = MinuteToSecond(sFile.Fields("ͬ�򷢵�").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("ͬ�򷢵�").ToString.Trim)
    '                .IKK(5) = MinuteToSecond(sFile.Fields("ͬ��ͨ��").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("ͬ��ͨ��").ToString.Trim)
    '                .IKK(6) = MinuteToSecond(sFile.Fields("ͬ�򵽵�").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("ͬ�򵽵�").ToString.Trim)
    '                .IKK(7) = MinuteToSecond(sFile.Fields("ͬ��ͨ").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("ͬ��ͨ").ToString.Trim)
    '                .IKK(8) = MinuteToSecond(sFile.Fields("ͬ��ͨͨ").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("ͬ��ͨͨ").ToString.Trim)
    '                .IKK(9) = MinuteToSecond(sFile.Fields("���򵽵�").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("���򵽵�").ToString.Trim)
    '                .IKK(10) = MinuteToSecond(sFile.Fields("���򷢵�").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("���򷢵�").ToString.Trim)
    '                .IKK(11) = MinuteToSecond(sFile.Fields("���򵽷�").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("���򵽷�").ToString.Trim)
    '                .IKK(12) = MinuteToSecond(sFile.Fields("���򷢷�").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("���򷢷�").ToString.Trim)
    '                .IKK(13) = MinuteToSecond(sFile.Fields("����ͨ").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("����ͨ").ToString.Trim)
    '                .IKK(14) = MinuteToSecond(sFile.Fields("����ͨ").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("����ͨ").ToString.Trim)
    '                .IKK(15) = MinuteToSecond(sFile.Fields("����ͨ��").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("����ͨ��").ToString.Trim)
    '                .IKK(16) = MinuteToSecond(sFile.Fields("����ͨ��").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("����ͨ��").ToString.Trim)
    '                .IKK(17) = MinuteToSecond(sFile.Fields("����ͨͨ").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("����ͨͨ").ToString.Trim)
    '            End If
    '        End With
    '    Next i
    '    dFile.Close()
    'End Sub

    ''����ɵ����úͽ�·��Ϣ
    'Public Sub InputStationGudaoAndJinLuInf()
    '    Dim i As Integer
    '    Dim j As Integer
    '    Dim sTr As String
    '    Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)

    '    For i = 1 To UBound(StationInf)
    '        Dim strTable2 As String = "select * from ��վ��Ϣ where վ��='" & StationInf(i).sStationName & "'"
    '        Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
    '        Dim myDataSet2 As Data.DataSet = New Data.DataSet
    '        Mydc2.Fill(myDataSet2, "��վ��Ϣ")
    '        Dim myTable2 As Data.DataTable
    '        myTable2 = myDataSet2.Tables("��վ��Ϣ")
    '        If myTable2.Rows.Count > 0 Then
    '            StationInf(i).sStaStyle = myTable2.Rows(0).Item("����").ToString.Trim
    '            StationInf(i).sAtLineName = myTable2.Rows(0).Item("��·����").ToString.Trim
    '            StationInf(i).sPrintStaName = myTable2.Rows(0).Item("���վ��").ToString.Trim 'FromStaNameToPrintStaName(StationInf(i).sStationName, StationInf(i).sAtLineName)
    '            StationInf(i).sStaProperity = myTable2.Rows(0).Item("����").ToString.Trim
    '            StationInf(i).sStationProp = ChaStProp(myTable2.Rows(0).Item("����").ToString.Trim, myTable2.Rows(0).Item("����").ToString.Trim)
    '            StationInf(i).sEnglishName = myTable2.Rows(0).Item("Ӣ�ļ��").ToString.Trim
    '        End If

    '        Dim strTable3 As String = "select * from �߶���Ϣ�� where ��վ����='" & StationInf(i).sStationName & "'"
    '        Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
    '        Dim myDataSet3 As Data.DataSet = New Data.DataSet
    '        Mydc3.Fill(myDataSet3, "��վ��Ϣ")
    '        Dim myTable3 As Data.DataTable
    '        myTable3 = myDataSet3.Tables("��վ��Ϣ")
    '        StationInf(i).nStLineNum = 0
    '        ReDim StationInf(i).sStLineNo(0)
    '        ReDim StationInf(i).nStLineUse(0)
    '        ReDim StationInf(i).sLineUse(0)
    '        ReDim StationInf(i).sUpOrDownUse(0)
    '        ReDim StationInf(i).sGuDaoUseSeq(0)

    '        If myTable3.Rows.Count > 0 Then
    '            For j = 1 To myTable3.Rows.Count
    '                sTr = myTable3.Rows(j - 1).Item("�߶�����").ToString.Trim
    '                If sTr.Length >= 3 Then
    '                    If sTr.Substring(sTr.Length - 3) = "�ɵ���" Then
    '                        StationInf(i).nStLineNum = StationInf(i).nStLineNum + 1
    '                        ReDim Preserve StationInf(i).sStLineNo(UBound(StationInf(i).sStLineNo) + 1)
    '                        ReDim Preserve StationInf(i).nStLineUse(UBound(StationInf(i).sStLineNo) + 1)
    '                        ReDim Preserve StationInf(i).sLineUse(UBound(StationInf(i).sStLineNo) + 1)
    '                        ReDim Preserve StationInf(i).sUpOrDownUse(UBound(StationInf(i).sStLineNo) + 1)
    '                        ReDim Preserve StationInf(i).sGuDaoUseSeq(UBound(StationInf(i).sStLineNo) + 1)
    '                        StationInf(i).sStLineNo(UBound(StationInf(i).sStLineNo)) = myTable3.Rows(j - 1).Item("�ɵ��������").ToString.Trim
    '                        StationInf(i).sLineUse(UBound(StationInf(i).sStLineNo)) = myTable3.Rows(j - 1).Item("�ɵ�����").ToString.Trim
    '                        StationInf(i).sUpOrDownUse(UBound(StationInf(i).sStLineNo)) = myTable3.Rows(j - 1).Item("�ɵ���;").ToString.Trim
    '                        StationInf(i).sGuDaoUseSeq(UBound(StationInf(i).sStLineNo)) = myTable3.Rows(j - 1).Item("�ɵ�ʹ��˳��").ToString.Trim
    '                        StationInf(i).nStLineUse(UBound(StationInf(i).sStLineNo)) = ChaLineUse(myTable3.Rows(j - 1).Item("�ɵ�����").ToString.Trim, myTable3.Rows(j - 1).Item("�ɵ���;").ToString.Trim)
    '                    End If
    '                End If
    '            Next
    '        End If


    '        ''���복վ��·�ͷֲ�վ�ɵ�ʹ��
    '        'ReDim StationInf(i).sGDFromSta(0)
    '        'ReDim StationInf(i).sGDToSta(0)
    '        'ReDim StationInf(i).sGDDaoFaBasicJinLu(0)
    '        'ReDim StationInf(i).sGDDaoFaKeXuanJinLu(0)
    '        'ReDim StationInf(i).sGDPassBasicJinLu(0)
    '        'ReDim StationInf(i).sGDPassKeXuanJinLu(0)

    '        'Dim strTable4 As String = "select * from ��վ�ɵ�ʹ�� where �ֲ�վ����='" & StationInf(i).sStationName & "'"
    '        'Dim Mydc4 As New Data.OleDb.OleDbDataAdapter(strTable4, strCon)
    '        'Dim myDataSet4 As Data.DataSet = New Data.DataSet
    '        'Mydc4.Fill(myDataSet4, "��վ��Ϣ")
    '        'Dim myTable4 As Data.DataTable
    '        'myTable4 = myDataSet4.Tables("��վ��Ϣ")
    '        'If myTable4.Rows.Count > 0 Then
    '        '    ReDim StationInf(i).sGDFromSta(myTable4.Rows.Count)
    '        '    ReDim StationInf(i).sGDToSta(myTable4.Rows.Count)
    '        '    ReDim StationInf(i).sGDDaoFaBasicJinLu(myTable4.Rows.Count)
    '        '    ReDim StationInf(i).sGDDaoFaKeXuanJinLu(myTable4.Rows.Count)
    '        '    ReDim StationInf(i).sGDPassBasicJinLu(myTable4.Rows.Count)
    '        '    ReDim StationInf(i).sGDPassKeXuanJinLu(myTable4.Rows.Count)
    '        '    For j = 1 To myTable4.Rows.Count
    '        '        StationInf(i).sGDFromSta(j) = myTable4.Rows(j - 1).Item("���﷽��").ToString.Trim
    '        '        StationInf(i).sGDToSta(j) = myTable4.Rows(j - 1).Item("��������").ToString.Trim
    '        '        StationInf(i).sGDDaoFaBasicJinLu(j) = myTable4.Rows(j - 1).Item("����������·").ToString.Trim
    '        '        StationInf(i).sGDDaoFaKeXuanJinLu(j) = myTable4.Rows(j - 1).Item("������ѡ��·").ToString.Trim
    '        '        StationInf(i).sGDPassBasicJinLu(j) = myTable4.Rows(j - 1).Item("ͨ��������·").ToString.Trim
    '        '        StationInf(i).sGDPassKeXuanJinLu(j) = myTable4.Rows(j - 1).Item("ͨ����ѡ��·").ToString.Trim
    '        '    Next
    '        'End If


    '        ''���´�����복վ��·��Ϣ
    '        'ReDim StationInf(i).sjComeSta(0)
    '        'ReDim StationInf(i).sjBaseJinLu(0)
    '        'ReDim StationInf(i).sjNextSta(0)
    '        'ReDim StationInf(i).sjGuoDaoNum(0)
    '        'ReDim StationInf(i).sjSelectJinLu(0)
    '        'ReDim StationInf(i).sjMemo(0)

    '        'Dim strTable5 As String = "select * from ��վ��·��Ϣ where ��վ����='" & StationInf(i).sStationName & "'"
    '        'Dim Mydc5 As New Data.OleDb.OleDbDataAdapter(strTable5, strCon)
    '        'Dim myDataSet5 As Data.DataSet = New Data.DataSet
    '        'Mydc5.Fill(myDataSet5, "��վ��Ϣ")
    '        'Dim myTable5 As Data.DataTable
    '        'myTable5 = myDataSet5.Tables("��վ��Ϣ")
    '        'If myTable5.Rows.Count > 0 Then
    '        '    ReDim StationInf(i).sjComeSta(myTable5.Rows.Count)
    '        '    ReDim StationInf(i).sjBaseJinLu(myTable5.Rows.Count)
    '        '    ReDim StationInf(i).sjNextSta(myTable5.Rows.Count)
    '        '    ReDim StationInf(i).sjGuoDaoNum(myTable5.Rows.Count)
    '        '    ReDim StationInf(i).sjSelectJinLu(myTable5.Rows.Count)
    '        '    ReDim StationInf(i).sjMemo(myTable5.Rows.Count)
    '        '    For j = 1 To myTable5.Rows.Count
    '        '        StationInf(i).sjComeSta(j) = myTable5.Rows(j - 1).Item("���﷽��").ToString.Trim
    '        '        StationInf(i).sjBaseJinLu(j) = myTable5.Rows(j - 1).Item("��������").ToString.Trim
    '        '        StationInf(i).sjNextSta(j) = myTable5.Rows(j - 1).Item("�ɵ���").ToString.Trim
    '        '        StationInf(i).sjGuoDaoNum(j) = myTable5.Rows(j - 1).Item("������·").ToString.Trim
    '        '        StationInf(i).sjSelectJinLu(j) = myTable5.Rows(j - 1).Item("��ѡ��·").ToString.Trim
    '        '        StationInf(i).sjMemo(j) = myTable5.Rows(j - 1).Item("��ע").ToString.Trim
    '        '    Next
    '        'End If

    '        '���´����ǳ�վ���ʱ��
    '        Dim strTable6 As String = "select * from ��վ���ʱ�� where ��վ����='" & StationInf(i).sStationName & "'"
    '        Dim Mydc6 As New Data.OleDb.OleDbDataAdapter(strTable6, strCon)
    '        Dim myDataSet6 As Data.DataSet = New Data.DataSet
    '        Mydc6.Fill(myDataSet6, "��վ���ʱ��")
    '        Dim myTable6 As Data.DataTable
    '        myTable6 = myDataSet6.Tables("��վ���ʱ��")
    '        If myTable6.Rows.Count > 0 Then
    '            With StationInf(i)
    '                ReDim Preserve .lTaoBu(2)
    '                ReDim Preserve .lTaoHui(2)
    '                ReDim Preserve .lTaoLian1(2)
    '                ReDim Preserve .lTaoLian2(2)
    '                ReDim Preserve .lTaoTongK(2)
    '                ReDim Preserve .lTaoTongH(2)
    '                ReDim Preserve .lTaoDaoFa(2)
    '                ReDim Preserve .lTaoFaDao(2)
    '                ReDim Preserve .lTaoFaFa(2)
    '                ReDim Preserve .lTaoDaoDao(2)

    '                .lTaoBu(1) = MinuteToSecond(myTable6.Rows(0).Item("�Ӳ���").ToString.Trim)
    '                .lTaoBu(2) = MinuteToSecond(myTable6.Rows(0).Item("�Ӳ���").ToString.Trim)
    '                .lTaoHui(1) = MinuteToSecond(myTable6.Rows(0).Item("�ӻ���").ToString.Trim)
    '                .lTaoHui(2) = MinuteToSecond(myTable6.Rows(0).Item("�ӻ���").ToString.Trim)
    '                .lTaoLian1(1) = MinuteToSecond(myTable6.Rows(0).Item("����1��").ToString.Trim)
    '                .lTaoLian1(2) = MinuteToSecond(myTable6.Rows(0).Item("����1��").ToString.Trim)
    '                .lTaoLian2(1) = MinuteToSecond(myTable6.Rows(0).Item("����2��").ToString.Trim)
    '                .lTaoLian2(2) = MinuteToSecond(myTable6.Rows(0).Item("����2��").ToString.Trim)
    '                .lTaoTongK(1) = MinuteToSecond(myTable6.Rows(0).Item("��ͨ1��").ToString.Trim)
    '                .lTaoTongK(2) = MinuteToSecond(myTable6.Rows(0).Item("��ͨ1��").ToString.Trim)
    '                .lTaoTongH(1) = MinuteToSecond(myTable6.Rows(0).Item("��ͨ2��").ToString.Trim)
    '                .lTaoTongH(2) = MinuteToSecond(myTable6.Rows(0).Item("��ͨ2��").ToString.Trim)
    '                .lTaoDaoFa(1) = MinuteToSecond(myTable6.Rows(0).Item("�ӵ�����").ToString.Trim)
    '                .lTaoDaoFa(2) = MinuteToSecond(myTable6.Rows(0).Item("�ӵ�����").ToString.Trim)
    '                .lTaoFaDao(1) = MinuteToSecond(myTable6.Rows(0).Item("�ӷ�����").ToString.Trim)
    '                .lTaoFaDao(2) = MinuteToSecond(myTable6.Rows(0).Item("�ӷ�����").ToString.Trim)
    '                .lTaoFaFa(1) = MinuteToSecond(myTable6.Rows(0).Item("�ӷ�����").ToString.Trim)
    '                .lTaoFaFa(2) = MinuteToSecond(myTable6.Rows(0).Item("�ӷ�����").ToString.Trim)
    '                .lTaoDaoDao(1) = MinuteToSecond(myTable6.Rows(0).Item("�ӵ�����").ToString.Trim)
    '                .lTaoDaoDao(2) = MinuteToSecond(myTable6.Rows(0).Item("�ӵ�����").ToString.Trim)
    '            End With
    '        End If

    '        '���´�����׷�ټ��ʱ��
    '        Dim strTable7 As String = "select * from ׷�ټ��ʱ�� where ��վ����='" & StationInf(i).sStationName & "'"
    '        Dim Mydc7 As New Data.OleDb.OleDbDataAdapter(strTable7, strCon)
    '        Dim myDataSet7 As Data.DataSet = New Data.DataSet
    '        Mydc7.Fill(myDataSet7, "׷�ټ��ʱ��")
    '        Dim myTable7 As Data.DataTable
    '        myTable7 = myDataSet7.Tables("׷�ټ��ʱ��")
    '        If myTable7.Rows.Count > 0 Then
    '            With StationInf(i)
    '                ReDim Preserve .IKK(17)
    '                ReDim Preserve .IKH(17)
    '                ReDim Preserve .IHH(17)
    '                ReDim Preserve .IHK(17)
    '                .IKK(0) = MinuteToSecond(myTable7.Rows(0).Item("ͬ��ͨ��").ToString.Trim)
    '                .IKK(1) = MinuteToSecond(myTable7.Rows(0).Item("ͬ�򷢷�").ToString.Trim)
    '                .IKK(2) = MinuteToSecond(myTable7.Rows(0).Item("ͬ�򵽷�").ToString.Trim)
    '                .IKK(3) = MinuteToSecond(myTable7.Rows(0).Item("ͬ��ͨ").ToString.Trim)
    '                .IKK(4) = MinuteToSecond(myTable7.Rows(0).Item("ͬ�򷢵�").ToString.Trim)
    '                .IKK(5) = MinuteToSecond(myTable7.Rows(0).Item("ͬ��ͨ��").ToString.Trim)
    '                .IKK(6) = MinuteToSecond(myTable7.Rows(0).Item("ͬ�򵽵�").ToString.Trim)
    '                .IKK(7) = MinuteToSecond(myTable7.Rows(0).Item("ͬ��ͨ").ToString.Trim)
    '                .IKK(8) = MinuteToSecond(myTable7.Rows(0).Item("ͬ��ͨͨ").ToString.Trim)
    '                .IKK(9) = MinuteToSecond(myTable7.Rows(0).Item("���򵽵�").ToString.Trim)
    '                .IKK(10) = MinuteToSecond(myTable7.Rows(0).Item("���򷢵�").ToString.Trim)
    '                .IKK(11) = MinuteToSecond(myTable7.Rows(0).Item("���򵽷�").ToString.Trim)
    '                .IKK(12) = MinuteToSecond(myTable7.Rows(0).Item("���򷢷�").ToString.Trim)
    '                .IKK(13) = MinuteToSecond(myTable7.Rows(0).Item("����ͨ").ToString.Trim)
    '                .IKK(14) = MinuteToSecond(myTable7.Rows(0).Item("����ͨ").ToString.Trim)
    '                .IKK(15) = MinuteToSecond(myTable7.Rows(0).Item("����ͨ��").ToString.Trim)
    '                .IKK(16) = MinuteToSecond(myTable7.Rows(0).Item("����ͨ��").ToString.Trim)
    '                .IKK(17) = MinuteToSecond(myTable7.Rows(0).Item("����ͨͨ").ToString.Trim)
    '            End With
    '        End If
    '    Next i

    'End Sub

    '����������Ϣ
    Public Sub InputElseData()
        ReDim DataReadInf(1)
        ReDim DataReadInf(1).sTotalLineNum(1)
        DataReadInf(1).NumStation = UBound(StationInf)
        With DataReadInf(1)
            .sTotalLineNum(1) = "1"
            .sLuJuName = "·��"
            .sFenJuname = "�־�"
            .sNowLineName = NetInf.sNetName

            .NowSectionBegin = 1
            .NowSectionEnd = UBound(SectionInf)

            .NowStationBegin = 1
            .NowStationEnd = UBound(StationInf)

            .NowTrainBegin = 1
            .NowTrainEnd = UBound(TrainInf)

            .NowFenChaStationBegin = 1
            .NowFenChaStationEnd = 3

            .NowShiKeBiaoBegin = 1
            .NowShiKeBiaoEnd = UBound(SkbStnSeq)

            .NowIndexBegin = 1
            .NowIndexEnd = 1
            .nWholeLineNum = 2
            .sSKBNameKe = ""
            .sSKBNameKe = ""
        End With
        nNowDataReadLineNum = 1
    End Sub

    '���г��ӳ���������Ϣ��ɾ��
    Public Sub ResetCheDiLinkTrainNumber()
        Dim i As Integer
        Dim j As Integer
        Dim nBTrain As Integer
        Dim nFtrain As Integer
        Dim nNtrain As Integer

        For j = 1 To UBound(ChediInfo)
            If UBound(ChediInfo(j).nLinkTrain) > 0 Then
                If UBound(ChediInfo(j).nLinkTrain) = 1 Then
                    If ChediInfo(j).nLinkTrain(1) > 0 And TrainInf(ChediInfo(j).nLinkTrain(1)).Train <> "" Then
                        TrainInf(ChediInfo(j).nLinkTrain(1)).TrainReturn(1) = 0
                        TrainInf(ChediInfo(j).nLinkTrain(1)).TrainReturn(2) = 0
                    End If
                Else
                    For i = 1 To UBound(ChediInfo(j).nLinkTrain)
                        If i = 1 Then
                            nBTrain = 0
                        Else
                            nBTrain = ChediInfo(j).nLinkTrain(i - 1)
                        End If

                        If i = UBound(ChediInfo(j).nLinkTrain) Then
                            nNtrain = 0
                        Else
                            nNtrain = ChediInfo(j).nLinkTrain(i + 1)
                        End If
                        nFtrain = ChediInfo(j).nLinkTrain(i)
                        If nFtrain > 0 Then
                            TrainInf(nFtrain).TrainReturn(1) = nBTrain
                            TrainInf(nFtrain).TrainReturn(2) = nNtrain
                        End If
                    Next i
                End If
            End If
        Next j
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                TrainInf(i).lAllStartTime = TrainInf(i).Starting(TrainInf(i).nPathID(1))
                TrainInf(i).lAllEndTime = TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID)))
            End If
        Next
    End Sub

    '���һ��������Ϣ
    Public Sub addOneUndoInf()
        Dim nNum As Integer
        Dim i As Integer
        nNum = 0
        nNum = UndoSeq.nCurUndoID + 1
        If nNum > TimeTablePara.nMaxUndoID Then
            nNum = 1
        End If
        ReDim UndoInf(nNum).Traininf(UBound(TrainInf))
        For i = 1 To UBound(TrainInf)
            Call CopyPriTrainInfor(i, i, nNum)
        Next
        ReDim UndoInf(nNum).CheDiInf(UBound(ChediInfo))
        For i = 1 To UBound(ChediInfo)
            Call CopyPriCheDiInfor(i, i, nNum)
        Next
        UndoSeq.nUpID = UndoSeq.nCurUndoID
        UndoSeq.nDownID = 0
        UndoSeq.nCurUndoID = nNum
        UndoSeq.nStep = 0
    End Sub

    '�������ظ�
    Public Sub UndoTraininf(ByVal nState As Integer)
        Dim i As Integer
        Dim nID As Integer

        If nState = 1 Then '����
            nID = UndoSeq.nUpID
            If UndoSeq.nStep >= TimeTablePara.nMaxUndoID - 1 Then
                nID = 0
            Else
                UndoSeq.nStep = UndoSeq.nStep + 1
            End If
        Else '�ظ�
            If UndoSeq.nStep > 0 Then
                nID = UndoSeq.nDownID
                UndoSeq.nStep = UndoSeq.nStep - 1
            End If
        End If

        If nID > 0 Then
            ReDim TrainInf(0)
            ReDim ChediInfo(0)
            ReDim TrainInf(UBound(UndoInf(nID).Traininf))
            ReDim ChediInfo(UBound(UndoInf(nID).CheDiInf))
            For i = 1 To UBound(UndoInf(nID).Traininf)
                Call CopyUndoTrainInfor(i, i, nID)
            Next
            For i = 1 To UBound(UndoInf(nID).CheDiInf)
                Call CopyUndoCheDiInfor(i, i, nID)
            Next
            UndoSeq.nCurUndoID = nID
            If nID = 1 Then
                UndoSeq.nUpID = TimeTablePara.nMaxUndoID
            Else
                UndoSeq.nUpID = nID - 1
            End If
            If nID = TimeTablePara.nMaxUndoID Then
                UndoSeq.nDownID = 1
            Else
                UndoSeq.nDownID = nID + 1
            End If
            Call RefreshDiagram(1)
        End If
    End Sub


    ''�����г���Ϣ��Traininf,�ظ���������ʱ
    Public Sub CopyPriTrainInfor(ByVal nTrainNumFrom As Integer, ByVal nTrainNumTo As Integer, ByVal nNum As Integer)
        Dim i As Integer
        If TrainInf(nTrainNumFrom).Train <> "" Then
            With UndoInf(nNum).Traininf(nTrainNumTo)
                .Train = TrainInf(nTrainNumFrom).Train
                .nLeftState = TrainInf(nTrainNumFrom).nLeftState
                .nRightState = TrainInf(nTrainNumFrom).nRightState
                .nLinkLeft = TrainInf(nTrainNumFrom).nLinkLeft
                .nIfCanMove = TrainInf(nTrainNumFrom).nIfCanMove
                .sJiaoLuName = TrainInf(nTrainNumFrom).sJiaoLuName
                .sRunScaleName = TrainInf(nTrainNumFrom).sRunScaleName
                .sStopSclaeName = TrainInf(nTrainNumFrom).sStopSclaeName

                .TrainClass = TrainInf(nTrainNumFrom).TrainClass
                .TrainClassCal = TrainInf(nTrainNumFrom).TrainClassCal
                .nTrainTimeKind = TrainInf(nTrainNumFrom).nTrainTimeKind
                .sTrainTimeScale = TrainInf(nTrainNumFrom).sTrainTimeScale
                .TrainKind = TrainInf(nTrainNumFrom).TrainKind
                .StartStation = TrainInf(nTrainNumFrom).StartStation
                .EndStation = TrainInf(nTrainNumFrom).EndStation
                .ComeStation = TrainInf(nTrainNumFrom).ComeStation
                .ComeLine = TrainInf(nTrainNumFrom).ComeLine
                .NextStation = TrainInf(nTrainNumFrom).NextStation
                .NextLine = TrainInf(nTrainNumFrom).NextLine
                .sTrainUsageZD = TrainInf(nTrainNumFrom).sTrainUsageZD
                .sTrainBeizhuZD = TrainInf(nTrainNumFrom).sTrainBeizhuZD
                .sTrainUsageSF = TrainInf(nTrainNumFrom).sTrainUsageSF
                .sTrainBeizhuSF = TrainInf(nTrainNumFrom).sTrainBeizhuSF
                .NumStop = TrainInf(nTrainNumFrom).NumStop
                .NumWay = TrainInf(nTrainNumFrom).NumWay
                .TrainStyle = TrainInf(nTrainNumFrom).TrainStyle
                .TrainPuorNot = TrainInf(nTrainNumFrom).TrainPuorNot
                .nChaRunDirection = TrainInf(nTrainNumFrom).nChaRunDirection
                .nLinkTrainNum = TrainInf(nTrainNumFrom).nLinkTrainNum
                .nIfEnterGZSta = TrainInf(nTrainNumFrom).nIfEnterGZSta
                .nIfFixedCheDi = TrainInf(nTrainNumFrom).nIfFixedCheDi
                .SCheDiLeiXing = TrainInf(nTrainNumFrom).SCheDiLeiXing
                .sTrainXingZhi = TrainInf(nTrainNumFrom).sTrainXingZhi
                .sPrintTrain = TrainInf(nTrainNumFrom).sPrintTrain

                .sStartZFLine = TrainInf(nTrainNumFrom).sStartZFLine
                .sStartZFStarting = TrainInf(nTrainNumFrom).sStartZFStarting
                .sStartZFArrival = TrainInf(nTrainNumFrom).sStartZFArrival
                .sEndZFLine = TrainInf(nTrainNumFrom).sEndZFLine
                .sEndZFStarting = TrainInf(nTrainNumFrom).sEndZFStarting
                .sEndZFArrival = TrainInf(nTrainNumFrom).sEndZFArrival

                .sLineNum = TrainInf(nTrainNumFrom).sLineNum
                .sMuDiNum = TrainInf(nTrainNumFrom).sMuDiNum

                .PrintLineStyle = TrainInf(nTrainNumFrom).PrintLineStyle
                .PrintLineWidth = TrainInf(nTrainNumFrom).PrintLineWidth
                .PrintLineColor = TrainInf(nTrainNumFrom).PrintLineColor

                ReDim .TrainReturn(2)
                ReDim .TrainReturnStyle(2)
                If UBound(TrainInf(nTrainNumFrom).TrainReturn) > 0 Then
                    For i = 1 To 2
                        .TrainReturn(i) = TrainInf(nTrainNumFrom).TrainReturn(i)
                        .TrainReturnStyle(i) = TrainInf(nTrainNumFrom).TrainReturnStyle(i)
                    Next i
                End If


                .sTrainPath = TrainInf(nTrainNumFrom).sTrainPath        '�г���·,��"��"�ֿ�
                ReDim .nPassSection(UBound(TrainInf(nTrainNumFrom).nPassSection))
                ReDim .SectionRunTime(UBound(TrainInf(nTrainNumFrom).nPassSection))
                ReDim .sSectionName(UBound(TrainInf(nTrainNumFrom).sSectionName))
                'ReDim .StrFirstSta(UBound(TrainInf(nTrainNumFrom).StrFirstSta))
                'ReDim .StrSecondSta(UBound(TrainInf(nTrainNumFrom).StrSecondSta))
                ReDim .nFirstID(UBound(TrainInf(nTrainNumFrom).nFirstID))
                ReDim .nSecondID(UBound(TrainInf(nTrainNumFrom).nSecondID))
                ReDim .nPathID(UBound(TrainInf(nTrainNumFrom).nPathID))
                ReDim .sPathSta(UBound(TrainInf(nTrainNumFrom).nPathID))

                ReDim .sngFirXcoord(UBound(TrainInf(nTrainNumFrom).nPassSection))
                ReDim .sngFirYcoord(UBound(TrainInf(nTrainNumFrom).nPassSection))
                ReDim .sngSecXcoord(UBound(TrainInf(nTrainNumFrom).nPassSection))
                ReDim .sngSecYcoord(UBound(TrainInf(nTrainNumFrom).nPassSection))

                ReDim .lChaRunTime(0)
                ReDim .StopLine(UBound(StationInf))
                ReDim .StopLineTime(UBound(StationInf))
                ReDim .lChaRunTime(UBound(StationInf))
                ReDim .Starting(UBound(TrainInf(nTrainNumFrom).Starting))
                ReDim .Arrival(UBound(TrainInf(nTrainNumFrom).Arrival))
                ReDim .StopStation(UBound(TrainInf(nTrainNumFrom).nPathID))
                ReDim .nstopSta(UBound(TrainInf(nTrainNumFrom).nPathID))
                ReDim .stopTime(UBound(TrainInf(nTrainNumFrom).Starting))

                .NumStop = UBound(TrainInf(nTrainNumFrom).nPathID)

                For i = 1 To UBound(TrainInf(nTrainNumFrom).nPassSection)
                    .nPassSection(i) = TrainInf(nTrainNumFrom).nPassSection(i)
                Next i

                For i = 1 To UBound(TrainInf(nTrainNumFrom).sSectionName)
                    .sSectionName(i) = TrainInf(nTrainNumFrom).sSectionName(i)
                Next i

                'For i = 1 To UBound(TrainInf(nTrainNumFrom).StrFirstSta)
                '    .StrFirstSta(i) = TrainInf(nTrainNumFrom).StrFirstSta(i)
                'Next i

                'For i = 1 To UBound(TrainInf(nTrainNumFrom).StrSecondSta)
                '    .StrSecondSta(i) = TrainInf(nTrainNumFrom).StrSecondSta(i)
                'Next i

                For i = 1 To UBound(TrainInf(nTrainNumFrom).nPassSection)
                    .nFirstID(i) = TrainInf(nTrainNumFrom).nFirstID(i)
                Next i

                For i = 1 To UBound(TrainInf(nTrainNumFrom).nSecondID)
                    .nSecondID(i) = TrainInf(nTrainNumFrom).nSecondID(i)
                Next i

                For i = 1 To UBound(TrainInf(nTrainNumFrom).nPathID)
                    .nPathID(i) = TrainInf(nTrainNumFrom).nPathID(i)
                    .StopStation(i) = StationInf(.nPathID(i)).sStationName
                    .nstopSta(i) = .nPathID(i)

                Next i

                For i = 1 To UBound(.lChaRunTime)
                    .lChaRunTime(i) = TrainInf(nTrainNumFrom).lChaRunTime(i)
                Next i

                For i = 1 To UBound(.StopLine)
                    .StopLine(i) = TrainInf(nTrainNumFrom).StopLine(i)
                Next i

                For i = 1 To UBound(.StopLineTime)
                    .StopLineTime(i) = TrainInf(nTrainNumFrom).StopLineTime(i)
                Next i

                For i = 1 To UBound(TrainInf(nTrainNumFrom).Starting)
                    .Starting(i) = TrainInf(nTrainNumFrom).Starting(i)
                    .stopTime(i) = 0
                Next i

                For i = 1 To UBound(TrainInf(nTrainNumFrom).Arrival)
                    .Arrival(i) = TrainInf(nTrainNumFrom).Arrival(i)
                Next i

                '����ֲ�վ��Ϣ
                .NumWay = TrainInf(nTrainNumFrom).NumWay
                ReDim .Way1(TrainInf(nTrainNumFrom).NumWay)
                ReDim .Way2(TrainInf(nTrainNumFrom).NumWay)
                ReDim .Way3(TrainInf(nTrainNumFrom).NumWay)

                For i = 1 To .NumWay
                    .Way1(i) = TrainInf(nTrainNumFrom).Way1(i)
                    .Way2(i) = TrainInf(nTrainNumFrom).Way2(i)
                    .Way3(i) = TrainInf(nTrainNumFrom).Way3(i)
                Next i
            End With
        End If
    End Sub

    ''�����г���Ϣ������ͬ�����г������ƺ�ճ������
    Public Sub CopyTrainInformationFromCopyTrainInf(ByVal nTrainNumFrom As Integer, ByVal nTrainNumTo As Integer, ByVal nTrain As String)
        Dim i As Integer
        If CopyTrainInf(nTrainNumFrom).Train <> "" Then
            With TrainInf(nTrainNumTo)
                .Train = nTrain 'CopyTrainInf(nTrainNumFrom).Train
                .nLeftState = CopyTrainInf(nTrainNumFrom).nLeftState
                .nRightState = CopyTrainInf(nTrainNumFrom).nRightState
                .nLinkLeft = CopyTrainInf(nTrainNumFrom).nLinkLeft
                .nIfCanMove = CopyTrainInf(nTrainNumFrom).nIfCanMove
                .sJiaoLuName = CopyTrainInf(nTrainNumFrom).sJiaoLuName
                .sRunScaleName = CopyTrainInf(nTrainNumFrom).sRunScaleName
                .sStopSclaeName = CopyTrainInf(nTrainNumFrom).sStopSclaeName

                .TrainClass = CopyTrainInf(nTrainNumFrom).TrainClass
                .TrainClassCal = CopyTrainInf(nTrainNumFrom).TrainClassCal
                .nTrainTimeKind = CopyTrainInf(nTrainNumFrom).nTrainTimeKind
                .sTrainTimeScale = CopyTrainInf(nTrainNumFrom).sTrainTimeScale
                .TrainKind = CopyTrainInf(nTrainNumFrom).TrainKind
                .StartStation = CopyTrainInf(nTrainNumFrom).StartStation
                .EndStation = CopyTrainInf(nTrainNumFrom).EndStation
                .ComeStation = CopyTrainInf(nTrainNumFrom).ComeStation
                .ComeLine = CopyTrainInf(nTrainNumFrom).ComeLine
                .NextStation = CopyTrainInf(nTrainNumFrom).NextStation
                .NextLine = CopyTrainInf(nTrainNumFrom).NextLine
                .sTrainUsageZD = CopyTrainInf(nTrainNumFrom).sTrainUsageZD
                .sTrainBeizhuZD = CopyTrainInf(nTrainNumFrom).sTrainBeizhuZD
                .sTrainUsageSF = CopyTrainInf(nTrainNumFrom).sTrainUsageSF
                .sTrainBeizhuSF = CopyTrainInf(nTrainNumFrom).sTrainBeizhuSF
                .NumStop = CopyTrainInf(nTrainNumFrom).NumStop
                .NumWay = CopyTrainInf(nTrainNumFrom).NumWay
                .TrainStyle = CopyTrainInf(nTrainNumFrom).TrainStyle
                .TrainPuorNot = CopyTrainInf(nTrainNumFrom).TrainPuorNot
                .nChaRunDirection = CopyTrainInf(nTrainNumFrom).nChaRunDirection
                .nLinkTrainNum = CopyTrainInf(nTrainNumFrom).nLinkTrainNum
                .nIfEnterGZSta = CopyTrainInf(nTrainNumFrom).nIfEnterGZSta
                .nIfFixedCheDi = CopyTrainInf(nTrainNumFrom).nIfFixedCheDi
                .SCheDiLeiXing = CopyTrainInf(nTrainNumFrom).SCheDiLeiXing
                .sTrainXingZhi = CopyTrainInf(nTrainNumFrom).sTrainXingZhi
                .sPrintTrain = CopyTrainInf(nTrainNumFrom).sPrintTrain

                '.sStartZFLine = CopyTrainInf(nTrainNumFrom).sStartZFLine
                '.sStartZFStarting = CopyTrainInf(nTrainNumFrom).sStartZFStarting
                '.sStartZFArrival = CopyTrainInf(nTrainNumFrom).sStartZFArrival
                '.sEndZFLine = CopyTrainInf(nTrainNumFrom).sEndZFLine
                '.sEndZFStarting = CopyTrainInf(nTrainNumFrom).sEndZFStarting
                '.sEndZFArrival = CopyTrainInf(nTrainNumFrom).sEndZFArrival

                .sLineNum = CopyTrainInf(nTrainNumFrom).sLineNum
                .sMuDiNum = CopyTrainInf(nTrainNumFrom).sMuDiNum

                .PrintLineStyle = CopyTrainInf(nTrainNumFrom).PrintLineStyle
                .PrintLineWidth = CopyTrainInf(nTrainNumFrom).PrintLineWidth
                .PrintLineColor = CopyTrainInf(nTrainNumFrom).PrintLineColor

                ReDim .TrainReturn(2)
                ReDim .TrainReturnStyle(2)
                If UBound(CopyTrainInf(nTrainNumFrom).TrainReturn) > 0 Then
                    For i = 1 To 2
                        .TrainReturn(i) = CopyTrainInf(nTrainNumFrom).TrainReturn(i)
                        .TrainReturnStyle(i) = CopyTrainInf(nTrainNumFrom).TrainReturnStyle(i)
                    Next i
                End If

                .sTrainPath = CopyTrainInf(nTrainNumFrom).sTrainPath        '�г���·,��"��"�ֿ�
                ReDim .nPassSection(UBound(CopyTrainInf(nTrainNumFrom).nPassSection))
                ReDim .SectionRunTime(UBound(CopyTrainInf(nTrainNumFrom).nPassSection))
                ReDim .sSectionName(UBound(CopyTrainInf(nTrainNumFrom).sSectionName))
                'ReDim .StrFirstSta(UBound(CopyTrainInf(nTrainNumFrom).StrFirstSta))
                'ReDim .StrSecondSta(UBound(CopyTrainInf(nTrainNumFrom).StrSecondSta))
                ReDim .nFirstID(UBound(CopyTrainInf(nTrainNumFrom).nFirstID))
                ReDim .nSecondID(UBound(CopyTrainInf(nTrainNumFrom).nSecondID))
                ReDim .nPathID(UBound(CopyTrainInf(nTrainNumFrom).nPathID))
                ReDim .sPathSta(UBound(CopyTrainInf(nTrainNumFrom).nPathID))

                ReDim .sngFirXcoord(UBound(CopyTrainInf(nTrainNumFrom).nPassSection))
                ReDim .sngFirYcoord(UBound(CopyTrainInf(nTrainNumFrom).nPassSection))
                ReDim .sngSecXcoord(UBound(CopyTrainInf(nTrainNumFrom).nPassSection))
                ReDim .sngSecYcoord(UBound(CopyTrainInf(nTrainNumFrom).nPassSection))

                ReDim .lChaRunTime(0)
                ReDim .StopLine(UBound(StationInf))
                ReDim .StopLineTime(UBound(StationInf))
                ReDim .lChaRunTime(UBound(StationInf))
                ReDim .Starting(UBound(CopyTrainInf(nTrainNumFrom).Starting))
                ReDim .Arrival(UBound(CopyTrainInf(nTrainNumFrom).Arrival))
                ReDim .StopStation(UBound(CopyTrainInf(nTrainNumFrom).nPathID))
                ReDim .nstopSta(UBound(CopyTrainInf(nTrainNumFrom).nPathID))
                ReDim .stopTime(UBound(CopyTrainInf(nTrainNumFrom).Starting))

                .NumStop = UBound(CopyTrainInf(nTrainNumFrom).nPathID)

                For i = 1 To UBound(CopyTrainInf(nTrainNumFrom).nPassSection)
                    .nPassSection(i) = CopyTrainInf(nTrainNumFrom).nPassSection(i)
                Next i

                For i = 1 To UBound(CopyTrainInf(nTrainNumFrom).sSectionName)
                    .sSectionName(i) = CopyTrainInf(nTrainNumFrom).sSectionName(i)
                Next i

                'For i = 1 To UBound(CopyTrainInf(nTrainNumFrom).StrFirstSta)
                '    .StrFirstSta(i) = CopyTrainInf(nTrainNumFrom).StrFirstSta(i)
                'Next i

                'For i = 1 To UBound(CopyTrainInf(nTrainNumFrom).StrSecondSta)
                '    .StrSecondSta(i) = CopyTrainInf(nTrainNumFrom).StrSecondSta(i)
                'Next i

                For i = 1 To UBound(CopyTrainInf(nTrainNumFrom).nPassSection)
                    .nFirstID(i) = CopyTrainInf(nTrainNumFrom).nFirstID(i)
                Next i

                For i = 1 To UBound(CopyTrainInf(nTrainNumFrom).nSecondID)
                    .nSecondID(i) = CopyTrainInf(nTrainNumFrom).nSecondID(i)
                Next i

                For i = 1 To UBound(CopyTrainInf(nTrainNumFrom).nPathID)
                    .nPathID(i) = CopyTrainInf(nTrainNumFrom).nPathID(i)
                    .StopStation(i) = StationInf(.nPathID(i)).sStationName
                    .nstopSta(i) = .nPathID(i)
                    .stopTime(i) = 0
                Next i

                For i = 1 To UBound(.lChaRunTime)
                    .lChaRunTime(i) = CopyTrainInf(nTrainNumFrom).lChaRunTime(i)
                Next i

                For i = 1 To UBound(.StopLine)
                    .StopLine(i) = CopyTrainInf(nTrainNumFrom).StopLine(i)
                Next i

                For i = 1 To UBound(.StopLineTime)
                    .StopLineTime(i) = CopyTrainInf(nTrainNumFrom).StopLineTime(i)
                Next i

                For i = 1 To UBound(CopyTrainInf(nTrainNumFrom).Starting)
                    .Starting(i) = CopyTrainInf(nTrainNumFrom).Starting(i)
                Next i

                For i = 1 To UBound(CopyTrainInf(nTrainNumFrom).Arrival)
                    .Arrival(i) = CopyTrainInf(nTrainNumFrom).Arrival(i)
                Next i

                '����ֲ�վ��Ϣ
                .NumWay = CopyTrainInf(nTrainNumFrom).NumWay
                ReDim .Way1(CopyTrainInf(nTrainNumFrom).NumWay)
                ReDim .Way2(CopyTrainInf(nTrainNumFrom).NumWay)
                ReDim .Way3(CopyTrainInf(nTrainNumFrom).NumWay)

                For i = 1 To .NumWay
                    .Way1(i) = CopyTrainInf(nTrainNumFrom).Way1(i)
                    .Way2(i) = CopyTrainInf(nTrainNumFrom).Way2(i)
                    .Way3(i) = CopyTrainInf(nTrainNumFrom).Way3(i)
                Next i
            End With
        End If
    End Sub


    ''�����г���Ϣ������ͬ�����г������ƺ�ճ������
    Public Sub CopyTrainInformationToCopyTrainInf(ByVal nTrainNumFrom As Integer, ByVal nTrainNumTo As Integer)
        Dim i As Integer
        If TrainInf(nTrainNumFrom).Train <> "" Then
            With CopyTrainInf(nTrainNumTo)
                .Train = TrainInf(nTrainNumFrom).Train
                .nTrain = nTrainNumFrom
                .nLeftState = TrainInf(nTrainNumFrom).nLeftState
                .nRightState = TrainInf(nTrainNumFrom).nRightState
                .nLinkLeft = TrainInf(nTrainNumFrom).nLinkLeft
                .nIfCanMove = TrainInf(nTrainNumFrom).nIfCanMove
                .sJiaoLuName = TrainInf(nTrainNumFrom).sJiaoLuName
                .sRunScaleName = TrainInf(nTrainNumFrom).sRunScaleName
                .sStopSclaeName = TrainInf(nTrainNumFrom).sStopSclaeName

                .TrainClass = TrainInf(nTrainNumFrom).TrainClass
                .TrainClassCal = TrainInf(nTrainNumFrom).TrainClassCal
                .nTrainTimeKind = TrainInf(nTrainNumFrom).nTrainTimeKind
                .sTrainTimeScale = TrainInf(nTrainNumFrom).sTrainTimeScale
                .TrainKind = TrainInf(nTrainNumFrom).TrainKind
                .StartStation = TrainInf(nTrainNumFrom).StartStation
                .EndStation = TrainInf(nTrainNumFrom).EndStation
                .ComeStation = TrainInf(nTrainNumFrom).ComeStation
                .ComeLine = TrainInf(nTrainNumFrom).ComeLine
                .NextStation = TrainInf(nTrainNumFrom).NextStation
                .NextLine = TrainInf(nTrainNumFrom).NextLine
                .sTrainUsageZD = TrainInf(nTrainNumFrom).sTrainUsageZD
                .sTrainBeizhuZD = TrainInf(nTrainNumFrom).sTrainBeizhuZD
                .sTrainUsageSF = TrainInf(nTrainNumFrom).sTrainUsageSF
                .sTrainBeizhuSF = TrainInf(nTrainNumFrom).sTrainBeizhuSF
                .NumStop = TrainInf(nTrainNumFrom).NumStop
                .NumWay = TrainInf(nTrainNumFrom).NumWay
                .TrainStyle = TrainInf(nTrainNumFrom).TrainStyle
                .TrainPuorNot = TrainInf(nTrainNumFrom).TrainPuorNot
                .nChaRunDirection = TrainInf(nTrainNumFrom).nChaRunDirection
                .nLinkTrainNum = TrainInf(nTrainNumFrom).nLinkTrainNum
                .nIfEnterGZSta = TrainInf(nTrainNumFrom).nIfEnterGZSta
                .nIfFixedCheDi = TrainInf(nTrainNumFrom).nIfFixedCheDi
                .SCheDiLeiXing = TrainInf(nTrainNumFrom).SCheDiLeiXing
                .sTrainXingZhi = TrainInf(nTrainNumFrom).sTrainXingZhi
                .sPrintTrain = TrainInf(nTrainNumFrom).sPrintTrain

                .sLineNum = TrainInf(nTrainNumFrom).sLineNum
                .sMuDiNum = TrainInf(nTrainNumFrom).sMuDiNum

                .PrintLineStyle = TrainInf(nTrainNumFrom).PrintLineStyle
                .PrintLineWidth = TrainInf(nTrainNumFrom).PrintLineWidth
                .PrintLineColor = TrainInf(nTrainNumFrom).PrintLineColor

                ReDim .TrainReturn(2)
                ReDim .TrainReturnStyle(2)
                If UBound(TrainInf(nTrainNumFrom).TrainReturn) > 0 Then
                    For i = 1 To 2
                        .TrainReturn(i) = TrainInf(nTrainNumFrom).TrainReturn(i)
                        .TrainReturnStyle(i) = TrainInf(nTrainNumFrom).TrainReturnStyle(i)
                    Next i
                End If


                .sTrainPath = TrainInf(nTrainNumFrom).sTrainPath        '�г���·,��"��"�ֿ�
                ReDim .nPassSection(UBound(TrainInf(nTrainNumFrom).nPassSection))
                ReDim .SectionRunTime(UBound(TrainInf(nTrainNumFrom).nPassSection))
                ReDim .sSectionName(UBound(TrainInf(nTrainNumFrom).sSectionName))
                'ReDim .StrFirstSta(UBound(TrainInf(nTrainNumFrom).StrFirstSta))
                'ReDim .StrSecondSta(UBound(TrainInf(nTrainNumFrom).StrSecondSta))
                ReDim .nFirstID(UBound(TrainInf(nTrainNumFrom).nFirstID))
                ReDim .nSecondID(UBound(TrainInf(nTrainNumFrom).nSecondID))
                ReDim .nPathID(UBound(TrainInf(nTrainNumFrom).nPathID))
                ReDim .sPathSta(UBound(TrainInf(nTrainNumFrom).nPathID))

                ReDim .sngFirXcoord(UBound(TrainInf(nTrainNumFrom).nPassSection))
                ReDim .sngFirYcoord(UBound(TrainInf(nTrainNumFrom).nPassSection))
                ReDim .sngSecXcoord(UBound(TrainInf(nTrainNumFrom).nPassSection))
                ReDim .sngSecYcoord(UBound(TrainInf(nTrainNumFrom).nPassSection))

                ReDim .lChaRunTime(0)
                ReDim .StopLine(UBound(StationInf))
                ReDim .StopLineTime(UBound(StationInf))
                ReDim .lChaRunTime(UBound(StationInf))
                ReDim .Starting(UBound(TrainInf(nTrainNumFrom).Starting))
                ReDim .Arrival(UBound(TrainInf(nTrainNumFrom).Arrival))
                ReDim .StopStation(UBound(TrainInf(nTrainNumFrom).nPathID))
                ReDim .nstopSta(UBound(TrainInf(nTrainNumFrom).nPathID))
                ReDim .stopTime(UBound(TrainInf(nTrainNumFrom).Starting))

                .NumStop = UBound(TrainInf(nTrainNumFrom).nPathID)

                For i = 1 To UBound(TrainInf(nTrainNumFrom).nPassSection)
                    .nPassSection(i) = TrainInf(nTrainNumFrom).nPassSection(i)
                Next i

                For i = 1 To UBound(TrainInf(nTrainNumFrom).sSectionName)
                    .sSectionName(i) = TrainInf(nTrainNumFrom).sSectionName(i)
                Next i

                'For i = 1 To UBound(TrainInf(nTrainNumFrom).StrFirstSta)
                '    .StrFirstSta(i) = TrainInf(nTrainNumFrom).StrFirstSta(i)
                'Next i

                'For i = 1 To UBound(TrainInf(nTrainNumFrom).StrSecondSta)
                '    .StrSecondSta(i) = TrainInf(nTrainNumFrom).StrSecondSta(i)
                'Next i

                For i = 1 To UBound(TrainInf(nTrainNumFrom).nPassSection)
                    .nFirstID(i) = TrainInf(nTrainNumFrom).nFirstID(i)
                Next i

                For i = 1 To UBound(TrainInf(nTrainNumFrom).nSecondID)
                    .nSecondID(i) = TrainInf(nTrainNumFrom).nSecondID(i)
                Next i

                For i = 1 To UBound(TrainInf(nTrainNumFrom).nPathID)
                    .nPathID(i) = TrainInf(nTrainNumFrom).nPathID(i)
                    .StopStation(i) = StationInf(.nPathID(i)).sStationName
                    .nstopSta(i) = .nPathID(i)
                    .stopTime(i) = 0
                Next i

                For i = 1 To UBound(.lChaRunTime)
                    .lChaRunTime(i) = TrainInf(nTrainNumFrom).lChaRunTime(i)
                Next i

                For i = 1 To UBound(.StopLine)
                    .StopLine(i) = TrainInf(nTrainNumFrom).StopLine(i)
                Next i

                For i = 1 To UBound(.StopLineTime)
                    .StopLineTime(i) = TrainInf(nTrainNumFrom).StopLineTime(i)
                Next i

                For i = 1 To UBound(TrainInf(nTrainNumFrom).Starting)
                    .Starting(i) = TrainInf(nTrainNumFrom).Starting(i)
                Next i

                For i = 1 To UBound(TrainInf(nTrainNumFrom).Arrival)
                    .Arrival(i) = TrainInf(nTrainNumFrom).Arrival(i)
                Next i

                '����ֲ�վ��Ϣ
                .NumWay = TrainInf(nTrainNumFrom).NumWay
                ReDim .Way1(TrainInf(nTrainNumFrom).NumWay)
                ReDim .Way2(TrainInf(nTrainNumFrom).NumWay)
                ReDim .Way3(TrainInf(nTrainNumFrom).NumWay)

                For i = 1 To .NumWay
                    .Way1(i) = TrainInf(nTrainNumFrom).Way1(i)
                    .Way2(i) = TrainInf(nTrainNumFrom).Way2(i)
                    .Way3(i) = TrainInf(nTrainNumFrom).Way3(i)
                Next i
            End With
        End If
    End Sub

    ''���Ƴ�����Ϣ��Traininf
    Sub CopyPriCheDiInfor(ByVal nFrom As Integer, ByVal nTo As Integer, ByVal nNum As Integer)
        Dim i As Integer
        With UndoInf(nNum).CheDiInf(nTo)
            .bIfGouWang = ChediInfo(nFrom).bIfGouWang
            .sCheCiHao = ChediInfo(nFrom).sCheCiHao
            .sCheDiID = ChediInfo(nFrom).sCheDiID
            .SCheDiLeiXing = ChediInfo(nFrom).SCheDiLeiXing
            .sChediName = ChediInfo(nFrom).sChediName
            .sDayBeginStation = ChediInfo(nFrom).sDayBeginStation
            .sDayEndStation = ChediInfo(nFrom).sDayEndStation
            .PrintCheDiLinkStyle = ChediInfo(nFrom).PrintCheDiLinkStyle
            .PrintCheDiLinkWidth = ChediInfo(nFrom).PrintCheDiLinkWidth
            .PrintCheDiLinkColor = ChediInfo(nFrom).PrintCheDiLinkColor
            .bIfAutoResetCheCi = ChediInfo(nFrom).bIfAutoResetCheCi
            ReDim .nLinkTrain(0)
            For i = 1 To UBound(ChediInfo(nFrom).nLinkTrain)
                If TrainInf(ChediInfo(nFrom).nLinkTrain(i)).Train <> "" Then
                    ReDim Preserve .nLinkTrain(UBound(.nLinkTrain) + 1)
                    .nLinkTrain(UBound(.nLinkTrain)) = ChediInfo(nFrom).nLinkTrain(i)
                End If
            Next
        End With
    End Sub

    ''����Undo�г���Ϣ��Traininf
    Public Sub CopyUndoTrainInfor(ByVal nTrainNumFrom As Integer, ByVal nTrainNumTo As Integer, ByVal nNum As Integer)
        Dim i As Integer
        If UndoInf(nNum).Traininf(nTrainNumFrom).Train <> "" Then
            With TrainInf(nTrainNumTo)
                .Train = UndoInf(nNum).Traininf(nTrainNumFrom).Train
                .nLeftState = UndoInf(nNum).Traininf(nTrainNumFrom).nLeftState
                .nRightState = UndoInf(nNum).Traininf(nTrainNumFrom).nRightState
                .nLinkLeft = UndoInf(nNum).Traininf(nTrainNumFrom).nLinkLeft
                .nIfCanMove = UndoInf(nNum).Traininf(nTrainNumFrom).nIfCanMove
                .sJiaoLuName = UndoInf(nNum).Traininf(nTrainNumFrom).sJiaoLuName
                .sRunScaleName = UndoInf(nNum).Traininf(nTrainNumFrom).sRunScaleName
                .sStopSclaeName = UndoInf(nNum).Traininf(nTrainNumFrom).sStopSclaeName

                .TrainClass = UndoInf(nNum).Traininf(nTrainNumFrom).TrainClass
                .TrainClassCal = UndoInf(nNum).Traininf(nTrainNumFrom).TrainClassCal
                .nTrainTimeKind = UndoInf(nNum).Traininf(nTrainNumFrom).nTrainTimeKind
                .sTrainTimeScale = UndoInf(nNum).Traininf(nTrainNumFrom).sTrainTimeScale
                .TrainKind = UndoInf(nNum).Traininf(nTrainNumFrom).TrainKind
                .StartStation = UndoInf(nNum).Traininf(nTrainNumFrom).StartStation
                .EndStation = UndoInf(nNum).Traininf(nTrainNumFrom).EndStation
                .ComeStation = UndoInf(nNum).Traininf(nTrainNumFrom).ComeStation
                .ComeLine = UndoInf(nNum).Traininf(nTrainNumFrom).ComeLine
                .NextStation = UndoInf(nNum).Traininf(nTrainNumFrom).NextStation
                .NextLine = UndoInf(nNum).Traininf(nTrainNumFrom).NextLine
                .sTrainUsageZD = UndoInf(nNum).Traininf(nTrainNumFrom).sTrainUsageZD
                .sTrainBeizhuZD = UndoInf(nNum).Traininf(nTrainNumFrom).sTrainBeizhuZD
                .sTrainUsageSF = UndoInf(nNum).Traininf(nTrainNumFrom).sTrainUsageSF
                .sTrainBeizhuSF = UndoInf(nNum).Traininf(nTrainNumFrom).sTrainBeizhuSF
                .NumStop = UndoInf(nNum).Traininf(nTrainNumFrom).NumStop
                .NumWay = UndoInf(nNum).Traininf(nTrainNumFrom).NumWay
                .TrainStyle = UndoInf(nNum).Traininf(nTrainNumFrom).TrainStyle
                .TrainPuorNot = UndoInf(nNum).Traininf(nTrainNumFrom).TrainPuorNot
                .nChaRunDirection = UndoInf(nNum).Traininf(nTrainNumFrom).nChaRunDirection
                .nLinkTrainNum = UndoInf(nNum).Traininf(nTrainNumFrom).nLinkTrainNum
                .nIfEnterGZSta = UndoInf(nNum).Traininf(nTrainNumFrom).nIfEnterGZSta
                .nIfFixedCheDi = UndoInf(nNum).Traininf(nTrainNumFrom).nIfFixedCheDi
                .SCheDiLeiXing = UndoInf(nNum).Traininf(nTrainNumFrom).SCheDiLeiXing
                .sTrainXingZhi = UndoInf(nNum).Traininf(nTrainNumFrom).sTrainXingZhi
                .sPrintTrain = UndoInf(nNum).Traininf(nTrainNumFrom).sPrintTrain

                .sStartZFLine = UndoInf(nNum).Traininf(nTrainNumFrom).sStartZFLine
                .sStartZFStarting = UndoInf(nNum).Traininf(nTrainNumFrom).sStartZFStarting
                .sStartZFArrival = UndoInf(nNum).Traininf(nTrainNumFrom).sStartZFArrival
                .sEndZFLine = UndoInf(nNum).Traininf(nTrainNumFrom).sEndZFLine
                .sEndZFStarting = UndoInf(nNum).Traininf(nTrainNumFrom).sEndZFStarting
                .sEndZFArrival = UndoInf(nNum).Traininf(nTrainNumFrom).sEndZFArrival

                .sLineNum = UndoInf(nNum).Traininf(nTrainNumFrom).sLineNum
                .sMuDiNum = UndoInf(nNum).Traininf(nTrainNumFrom).sMuDiNum

                .PrintLineStyle = UndoInf(nNum).Traininf(nTrainNumFrom).PrintLineStyle
                .PrintLineWidth = UndoInf(nNum).Traininf(nTrainNumFrom).PrintLineWidth
                .PrintLineColor = UndoInf(nNum).Traininf(nTrainNumFrom).PrintLineColor


                ReDim .TrainReturn(2)
                ReDim .TrainReturnStyle(2)
                For i = 1 To 2
                    .TrainReturn(i) = UndoInf(nNum).Traininf(nTrainNumFrom).TrainReturn(i)
                    .TrainReturnStyle(i) = UndoInf(nNum).Traininf(nTrainNumFrom).TrainReturnStyle(i)
                Next i

                .sTrainPath = UndoInf(nNum).Traininf(nTrainNumFrom).sTrainPath        '�г���·,��"��"�ֿ�
                ReDim .nPassSection(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nPassSection))
                ReDim .SectionRunTime(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nPassSection))
                ReDim .sSectionName(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).sSectionName))
                'ReDim .StrFirstSta(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).StrFirstSta))
                'ReDim .StrSecondSta(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).StrSecondSta))
                ReDim .nFirstID(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nFirstID))
                ReDim .nSecondID(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nSecondID))
                ReDim .nPathID(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nPathID))
                ReDim .sPathSta(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nPathID))

                ReDim .sngFirXcoord(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nPassSection))
                ReDim .sngFirYcoord(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nPassSection))
                ReDim .sngSecXcoord(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nPassSection))
                ReDim .sngSecYcoord(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nPassSection))

                ReDim .lChaRunTime(0)
                ReDim .StopLine(UBound(StationInf))
                ReDim .StopLineTime(UBound(StationInf))
                ReDim .lChaRunTime(UBound(StationInf))
                ReDim .Starting(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).Starting))
                ReDim .Arrival(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).Arrival))
                ReDim .StopStation(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nPathID))
                ReDim .nstopSta(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nPathID))
                ReDim .stopTime(UBound(UndoInf(nNum).Traininf(nTrainNumFrom).Starting))

                .NumStop = UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nPathID)

                For i = 1 To UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nPassSection)
                    .nPassSection(i) = UndoInf(nNum).Traininf(nTrainNumFrom).nPassSection(i)
                Next i

                For i = 1 To UBound(UndoInf(nNum).Traininf(nTrainNumFrom).sSectionName)
                    .sSectionName(i) = UndoInf(nNum).Traininf(nTrainNumFrom).sSectionName(i)
                Next i

                'For i = 1 To UBound(UndoInf(nNum).Traininf(nTrainNumFrom).StrFirstSta)
                '    .StrFirstSta(i) = UndoInf(nNum).Traininf(nTrainNumFrom).StrFirstSta(i)
                'Next i

                'For i = 1 To UBound(UndoInf(nNum).Traininf(nTrainNumFrom).StrSecondSta)
                '    .StrSecondSta(i) = UndoInf(nNum).Traininf(nTrainNumFrom).StrSecondSta(i)
                'Next i

                For i = 1 To UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nPassSection)
                    .nFirstID(i) = UndoInf(nNum).Traininf(nTrainNumFrom).nFirstID(i)
                Next i

                For i = 1 To UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nSecondID)
                    .nSecondID(i) = UndoInf(nNum).Traininf(nTrainNumFrom).nSecondID(i)
                Next i

                For i = 1 To UBound(UndoInf(nNum).Traininf(nTrainNumFrom).nPathID)
                    .nPathID(i) = UndoInf(nNum).Traininf(nTrainNumFrom).nPathID(i)
                    .StopStation(i) = StationInf(.nPathID(i)).sStationName
                    .nstopSta(i) = .nPathID(i)
                    .stopTime(i) = 0
                Next i

                For i = 1 To UBound(.lChaRunTime)
                    .lChaRunTime(i) = UndoInf(nNum).Traininf(nTrainNumFrom).lChaRunTime(i)
                Next i

                For i = 1 To UBound(.StopLine)
                    .StopLine(i) = UndoInf(nNum).Traininf(nTrainNumFrom).StopLine(i)
                Next i

                For i = 1 To UBound(.StopLineTime)
                    .StopLineTime(i) = UndoInf(nNum).Traininf(nTrainNumFrom).StopLineTime(i)
                Next i

                For i = 1 To UBound(UndoInf(nNum).Traininf(nTrainNumFrom).Starting)
                    .Starting(i) = UndoInf(nNum).Traininf(nTrainNumFrom).Starting(i)
                Next i

                For i = 1 To UBound(UndoInf(nNum).Traininf(nTrainNumFrom).Arrival)
                    .Arrival(i) = UndoInf(nNum).Traininf(nTrainNumFrom).Arrival(i)
                Next i

                '����ֲ�վ��Ϣ
                .NumWay = UndoInf(nNum).Traininf(nTrainNumFrom).NumWay
                ReDim .Way1(UndoInf(nNum).Traininf(nTrainNumFrom).NumWay)
                ReDim .Way2(UndoInf(nNum).Traininf(nTrainNumFrom).NumWay)
                ReDim .Way3(UndoInf(nNum).Traininf(nTrainNumFrom).NumWay)

                For i = 1 To .NumWay
                    .Way1(i) = UndoInf(nNum).Traininf(nTrainNumFrom).Way1(i)
                    .Way2(i) = UndoInf(nNum).Traininf(nTrainNumFrom).Way2(i)
                    .Way3(i) = UndoInf(nNum).Traininf(nTrainNumFrom).Way3(i)
                Next i
            End With
        End If
    End Sub

    ''���Ƴ�����Ϣ��Traininf
    Sub CopyUndoCheDiInfor(ByVal nFrom As Integer, ByVal nTo As Integer, ByVal nNum As Integer)
        Dim i As Integer
        With ChediInfo(nTo)
            .bIfGouWang = UndoInf(nNum).CheDiInf(nFrom).bIfGouWang
            .sCheCiHao = UndoInf(nNum).CheDiInf(nFrom).sCheCiHao
            .sCheDiID = UndoInf(nNum).CheDiInf(nFrom).sCheDiID
            .SCheDiLeiXing = UndoInf(nNum).CheDiInf(nFrom).SCheDiLeiXing
            .sChediName = UndoInf(nNum).CheDiInf(nFrom).sChediName
            .sDayBeginStation = UndoInf(nNum).CheDiInf(nFrom).sDayBeginStation
            .sDayEndStation = UndoInf(nNum).CheDiInf(nFrom).sDayEndStation
            .PrintCheDiLinkStyle = UndoInf(nNum).CheDiInf(nFrom).PrintCheDiLinkStyle
            .PrintCheDiLinkWidth = UndoInf(nNum).CheDiInf(nFrom).PrintCheDiLinkWidth
            .PrintCheDiLinkColor = UndoInf(nNum).CheDiInf(nFrom).PrintCheDiLinkColor
            .bIfAutoResetCheCi = UndoInf(nNum).CheDiInf(nFrom).bIfAutoResetCheCi
            ReDim .nLinkTrain(0)
            For i = 1 To UBound(UndoInf(nNum).CheDiInf(nFrom).nLinkTrain)
                If TrainInf(UndoInf(nNum).CheDiInf(nFrom).nLinkTrain(i)).Train <> "" Then
                    ReDim Preserve .nLinkTrain(UBound(.nLinkTrain) + 1)
                    .nLinkTrain(UBound(.nLinkTrain)) = UndoInf(nNum).CheDiInf(nFrom).nLinkTrain(i)
                End If
            Next
        End With
    End Sub

    'ʹѡ�е��г���ʾ����Ļ�м�
    Public Sub listTrainInMiddlePic(ByVal nTrain As Integer)
        Dim sBeTime As Integer
        Dim sngCurX As Integer
        sBeTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
        sngCurX = FormTimeToXCord(sBeTime, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
        'TimeTablePara.picPubDiagram.s
    End Sub

    '��Traininf() ��ʱ��
    Public Sub RecordTraininfStaTime(ByVal nTrainID As Integer, ByVal nStation As String, ByVal lArrivalTime As Long, ByVal lStartTime As Long)
        Dim i As Integer
        Dim nBegintemp As Integer, nEndtemp As Integer
        nBegintemp = 1
        nEndtemp = UBound(StationInf)
        For i = nBegintemp To nEndtemp
            If StationInf(i).sStationName = StationInf(nStation).sStationName Then
                With TrainInf(nTrainID)
                    .Arrival(i) = lArrivalTime
                    .Starting(i) = lStartTime
                    .StopLine(i) = .StopLine(nStation)
                End With
            End If
        Next i
    End Sub

    '��ʾ�������۷�Ҫ���������
    Public Sub ListNotSatisfidReturnTime(ByVal rGraphic As Graphics)
        Dim i As Integer
        Dim sTmpTime As Long
        Dim ntmpTrain As Integer
        Dim sFaDaoTime As Long
        Dim curX, curY As Single
        Dim curX2 As Single
        Dim tmpArriTime As Long
        Dim tmpStartTime As Long
        Dim sErrorMessage As String
        For i = 1 To UBound(TrainInf)
            'If i = 438 Then Stop
            If TrainInf(i).Train <> "" Then
                If TrainInf(i).TrainReturnStyle(2) = "վǰ�۷�" Then
                    ntmpTrain = SeekArriRightTrainZhanQianZheFan(i)
                    If ntmpTrain > 0 Then
                        tmpArriTime = TrainInf(ntmpTrain).Arrival(TrainInf(ntmpTrain).nPathID(UBound(TrainInf(ntmpTrain).nPathID)))
                        tmpStartTime = TrainInf(i).Starting(TrainInf(i).nPathID(1))
                        sTmpTime = AddLitterTime(tmpArriTime) - AddLitterTime(tmpStartTime)
                        sFaDaoTime = GetFaDaoJianGeTime(TrainInf(i).ComeStation, i)
                        If sTmpTime < sFaDaoTime Then
                            sErrorMessage = "���г��� " & TrainInf(i).ComeStation _
                        & " վվǰ�۷�ʱ����������㣬�涨�������ʱ��Ϊ��" & sFaDaoTime & " �룬ʵ�ʷ���ʱ��Ϊ��" & sTmpTime & " ��"
                            Call AddTrainErrorInf(i, TrainInf(ntmpTrain).nPathID(1), TrainInf(i).Starting(TrainInf(i).nPathID(1)), sErrorMessage)
                            curX = FormTimeToXCord(tmpArriTime, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                            curX2 = FormTimeToXCord(tmpStartTime, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                            curY = StationInf(TrainInf(i).nPathID(1)).YPicValue
                            rGraphic.DrawEllipse(New Pen(Color.Blue, 1), curX - 3, curY - 3, 6, 6)
                            rGraphic.DrawEllipse(New Pen(Color.Blue, 1), curX2 - 3, curY - 3, 6, 6)
                        End If
                    End If
                End If
            End If
        Next i
    End Sub
    '����Y����õ�������
    Public Function GetSecIDFormYcord(ByVal ncurYcord As Integer, ByVal nTrain As Integer) As Integer
        GetSecIDFormYcord = 0
        If nTrain > 0 Then
            Dim i As Integer
            Dim nUp As Integer
            Dim nDown As Integer
            For i = 1 To UBound(TrainInf(nTrain).nPassSection)
                nUp = Math.Max(StationInf(TrainInf(nTrain).nFirstID(i)).YPicValue, StationInf(TrainInf(nTrain).nSecondID(i)).YPicValue)
                nDown = Math.Min(StationInf(TrainInf(nTrain).nFirstID(i)).YPicValue, StationInf(TrainInf(nTrain).nSecondID(i)).YPicValue)
                If ncurYcord > nDown And ncurYcord < nUp Then
                    GetSecIDFormYcord = TrainInf(nTrain).nPassSection(i)
                    Exit For
                End If
            Next
        End If
    End Function
    '����Y����õ���վ���
    Public Function GetStaIDFormYcord(ByVal ncurYcord As Integer, ByVal nTrain As Integer) As Integer
        GetStaIDFormYcord = 0
        If nTrain > 0 Then
            Dim i As Integer
            Dim nUp As Integer
            Dim nMin As Integer
            Dim nCurSta As Integer
            nMin = 100000000
            For i = 1 To UBound(TrainInf(nTrain).nPathID)
                nUp = Math.Abs(StationInf(TrainInf(nTrain).nPathID(i)).YPicValue - ncurYcord)
                If nUp < nMin Then
                    nCurSta = TrainInf(nTrain).nPathID(i)
                    nMin = nUp
                End If
            Next
            GetStaIDFormYcord = nCurSta
        End If
    End Function
    '���ҵ�ǰ�г���ָ����վ����ʱ��ߵ��г�
    Public Function SeekLeftTrainSameDirection(ByVal nTrain As Integer, ByVal sSecName As String, ByVal nStartTime As Long, ByVal nArriTime As Long) As Integer
        SeekLeftTrainSameDirection = 0
        Dim i, j As Integer
        Dim tmpTime As Long
        Dim tmpTime1 As Long
        Dim tmpTime2 As Long
        Dim tmpTime3 As Long
        Dim tmpTime4 As Long
        Dim sMinTime As Long
        Dim nJGTime As Integer
        nJGTime = StationInf(1).IKK(1)
        Dim ntmpTrain As Integer
        ntmpTrain = 0
        sMinTime = 24 * 3600.0#
        Dim nFirSta As Integer
        Dim nSecSta As Integer
        If nTrain Mod 2 <> 0 Then '����
            For i = 1 To UBound(TrainInf)
                'If i = 33 Then Stop
                If TrainInf(i).Train <> "" Then
                    If i Mod 2 <> 0 And i <> nTrain Then
                        For j = 1 To UBound(TrainInf(i).nPassSection)
                            If SectionInf(TrainInf(i).nPassSection(j)).sSecName = sSecName Then
                                nFirSta = TrainInf(i).nFirstID(j)
                                nSecSta = TrainInf(i).nSecondID(j)
                                tmpTime1 = AddLitterTime(nStartTime)
                                tmpTime2 = AddLitterTime(nArriTime)
                                tmpTime3 = AddLitterTime(TrainInf(i).Starting(nFirSta))
                                tmpTime4 = AddLitterTime(TrainInf(i).Arrival(nSecSta))
                                If tmpTime1 <> -1 And tmpTime2 <> -1 And tmpTime3 <> -1 And tmpTime4 <> -1 Then
                                    If tmpTime1 >= tmpTime3 Or tmpTime2 >= tmpTime4 Then
                                        tmpTime = Math.Min(tmpTime1 - tmpTime3, tmpTime2 - tmpTime4)
                                        If tmpTime < sMinTime Then
                                            sMinTime = tmpTime
                                            ntmpTrain = i
                                        End If
                                    End If
                                    If tmpTime1 + nJGTime >= tmpTime3 And tmpTime1 <= tmpTime3 Then
                                        tmpTime = tmpTime1 - tmpTime3
                                        If tmpTime < sMinTime Then
                                            sMinTime = tmpTime
                                            ntmpTrain = i
                                        End If
                                    End If
                                    If tmpTime2 + nJGTime >= tmpTime4 And tmpTime2 <= tmpTime4 Then
                                        tmpTime = tmpTime2 - tmpTime4
                                        If tmpTime < sMinTime Then
                                            sMinTime = tmpTime
                                            ntmpTrain = i
                                        End If
                                    End If

                                End If

                            End If
                        Next j
                    End If
                End If
            Next i
        Else '����
            For i = 1 To UBound(TrainInf)
                'If i = 170 Then Stop
                If TrainInf(i).Train <> "" Then
                    If i Mod 2 = 0 And i <> nTrain Then
                        For j = 1 To UBound(TrainInf(i).nPassSection)
                            If SectionInf(TrainInf(i).nPassSection(j)).sSecName = sSecName Then
                                nFirSta = TrainInf(i).nFirstID(j)
                                nSecSta = TrainInf(i).nSecondID(j)
                                tmpTime1 = AddLitterTime(nStartTime)
                                tmpTime2 = AddLitterTime(nArriTime)
                                tmpTime3 = AddLitterTime(TrainInf(i).Starting(nFirSta))
                                tmpTime4 = AddLitterTime(TrainInf(i).Arrival(nSecSta))
                                If tmpTime1 <> -1 And tmpTime2 <> -1 And tmpTime3 <> -1 And tmpTime4 <> -1 Then
                                    If tmpTime1 >= tmpTime3 Or tmpTime2 >= tmpTime4 Then
                                        tmpTime = Math.Min(tmpTime1 - tmpTime3, tmpTime2 - tmpTime4)
                                        If tmpTime < sMinTime Then
                                            sMinTime = tmpTime
                                            ntmpTrain = i
                                        End If
                                    End If
                                    If tmpTime1 + nJGTime >= tmpTime3 And tmpTime1 <= tmpTime3 Then
                                        tmpTime = tmpTime1 - tmpTime3
                                        If tmpTime < sMinTime Then
                                            sMinTime = tmpTime
                                            ntmpTrain = i
                                        End If
                                    End If
                                    If tmpTime2 + nJGTime >= tmpTime4 And tmpTime2 <= tmpTime4 Then
                                        tmpTime = tmpTime2 - tmpTime4
                                        If tmpTime < sMinTime Then
                                            sMinTime = tmpTime
                                            ntmpTrain = i
                                        End If
                                    End If
                                End If
                            End If
                        Next j
                    End If
                End If
            Next i
        End If
        SeekLeftTrainSameDirection = ntmpTrain
        If SeekLeftTrainSameDirection = 0 Then
            SeekLeftTrainSameDirection = nTrain
        End If
    End Function

    '���ҵ�ǰ�г���ָ����վ����ʱ�ұߵ��г�
    Public Function SeekRightTrainSameDirection(ByVal nTrain As Integer, ByVal sSecName As String, ByVal nStartTime As Long, ByVal nArriTime As Long) As Integer
        SeekRightTrainSameDirection = 0
        Dim i, j As Integer
        Dim tmpTime As Long
        Dim tmpTime1 As Long
        Dim tmpTime2 As Long
        Dim tmpTime3 As Long
        Dim tmpTime4 As Long
        Dim sMinTime As Long
        Dim nJGTime As Integer
        nJGTime = StationInf(1).IKK(1)
        Dim ntmpTrain As Integer
        ntmpTrain = 0
        sMinTime = 24 * 3600.0#
        Dim nFirSta As Integer
        Dim nSecSta As Integer
        If nTrain Mod 2 <> 0 Then '����
            For i = 1 To UBound(TrainInf)
                'If i = 33 Then Stop
                If TrainInf(i).Train <> "" Then
                    If i Mod 2 <> 0 And i <> nTrain Then
                        For j = 1 To UBound(TrainInf(i).nPassSection)
                            If SectionInf(TrainInf(i).nPassSection(j)).sSecName = sSecName Then
                                nFirSta = TrainInf(i).nFirstID(j)
                                nSecSta = TrainInf(i).nSecondID(j)
                                tmpTime3 = AddLitterTime(nStartTime)
                                tmpTime4 = AddLitterTime(nArriTime)
                                tmpTime1 = AddLitterTime(TrainInf(i).Starting(nFirSta))
                                tmpTime2 = AddLitterTime(TrainInf(i).Arrival(nSecSta))
                                If tmpTime1 <> -1 And tmpTime2 <> -1 And tmpTime3 <> -1 And tmpTime4 <> -1 Then
                                    If tmpTime1 >= tmpTime3 Or tmpTime2 >= tmpTime4 Then
                                        tmpTime = Math.Min(tmpTime1 - tmpTime3, tmpTime2 - tmpTime4)
                                        If tmpTime < sMinTime Then
                                            sMinTime = tmpTime
                                            ntmpTrain = i
                                        End If
                                    End If
                                    If tmpTime1 + nJGTime >= tmpTime3 And tmpTime1 <= tmpTime3 Then
                                        tmpTime = tmpTime1 - tmpTime3
                                        If tmpTime < sMinTime Then
                                            sMinTime = tmpTime
                                            ntmpTrain = i
                                        End If
                                    End If
                                    If tmpTime2 + nJGTime >= tmpTime4 And tmpTime2 <= tmpTime4 Then
                                        tmpTime = tmpTime2 - tmpTime4
                                        If tmpTime < sMinTime Then
                                            sMinTime = tmpTime
                                            ntmpTrain = i
                                        End If
                                    End If

                                End If

                            End If
                        Next j
                    End If
                End If
            Next i
        Else '����
            For i = 1 To UBound(TrainInf)
                'If i = 170 Then Stop
                If TrainInf(i).Train <> "" Then
                    If i Mod 2 = 0 And i <> nTrain Then
                        For j = 1 To UBound(TrainInf(i).nPassSection)
                            If SectionInf(TrainInf(i).nPassSection(j)).sSecName = sSecName Then
                                nFirSta = TrainInf(i).nFirstID(j)
                                nSecSta = TrainInf(i).nSecondID(j)
                                tmpTime3 = AddLitterTime(nStartTime)
                                tmpTime4 = AddLitterTime(nArriTime)
                                tmpTime1 = AddLitterTime(TrainInf(i).Starting(nFirSta))
                                tmpTime2 = AddLitterTime(TrainInf(i).Arrival(nSecSta))
                                If tmpTime1 <> -1 And tmpTime2 <> -1 And tmpTime3 <> -1 And tmpTime4 <> -1 Then
                                    If tmpTime1 >= tmpTime3 Or tmpTime2 >= tmpTime4 Then
                                        tmpTime = Math.Min(tmpTime1 - tmpTime3, tmpTime2 - tmpTime4)
                                        If tmpTime < sMinTime Then
                                            sMinTime = tmpTime
                                            ntmpTrain = i
                                        End If
                                    End If
                                    If tmpTime1 + nJGTime >= tmpTime3 And tmpTime1 <= tmpTime3 Then
                                        tmpTime = tmpTime1 - tmpTime3
                                        If tmpTime < sMinTime Then
                                            sMinTime = tmpTime
                                            ntmpTrain = i
                                        End If
                                    End If
                                    If tmpTime2 + nJGTime >= tmpTime4 And tmpTime2 <= tmpTime4 Then
                                        tmpTime = tmpTime2 - tmpTime4
                                        If tmpTime < sMinTime Then
                                            sMinTime = tmpTime
                                            ntmpTrain = i
                                        End If
                                    End If
                                End If
                            End If
                        Next j
                    End If
                End If
            Next i
        End If
        SeekRightTrainSameDirection = ntmpTrain
        If SeekRightTrainSameDirection = 0 Then
            SeekRightTrainSameDirection = nTrain
        End If
    End Function

    '���г����ʵõ��г��������������Գ����ؿ��벻�ؿ��г�,nRetrun=1��ʾ�ؿ��벻�ؿͣ�2Ϊ��������Գ�
    Public Function GetTrainPro(ByVal sPro As String, ByVal nReturn As Integer) As String
        Dim i As Integer
        i = InStr(sPro, "+")
        GetTrainPro = ""
        If i > 0 Then
            If nReturn = 1 Then
                GetTrainPro = sPro.Substring(0, i - 1)
            Else
                GetTrainPro = sPro.Substring(i, sPro.Length - i)
            End If

        End If
    End Function

    ''' <summary>
    ''' �õ�����ͼʹ����״̬
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserName() As String
        GetUserName = "��������"
        'If SystemPara.sUserCompanyName.Length > 3 Then
        '    GetUserName = SystemPara.sUserCompanyName.Substring(0, 4)
        'End If
    End Function

    '�޸ĺ����г�����
    Public Sub EditNestTrainsNum(ByVal nTrain As Integer, ByVal nAddState As Integer)
        Dim i As Integer
        Dim sCurPrintCheCi As String
        sCurPrintCheCi = TrainInf(nTrain).sPrintTrain
        If sCurPrintCheCi <> "" Then
            If nAddState = 1 Then '���һ
                For i = 1 To UBound(TrainInf)
                    If TrainInf(i).Train <> "" Then
                        If nTrain Mod 2 = 0 And i Mod 2 = 0 Then '����
                            If Val(TrainInf(i).sPrintTrain) >= Val(sCurPrintCheCi) Then
                                TrainInf(i).sPrintTrain = Str(Val(TrainInf(i).sPrintTrain) + 1).Trim
                            End If
                        ElseIf nTrain Mod 2 <> 0 And i Mod 2 <> 0 Then '����
                            If Val(TrainInf(i).sPrintTrain) >= Val(sCurPrintCheCi) Then
                                TrainInf(i).sPrintTrain = Str(Val(TrainInf(i).sPrintTrain) + 1).Trim
                            End If
                        End If
                    End If
                Next
            Else '���һ 
                For i = 1 To UBound(TrainInf)
                    If TrainInf(i).Train <> "" Then
                        If nTrain Mod 2 = 0 And i Mod 2 = 0 Then '����
                            If Val(TrainInf(i).sPrintTrain) >= Val(sCurPrintCheCi) Then
                                TrainInf(i).sPrintTrain = Str(Val(TrainInf(i).sPrintTrain) - 1).Trim
                            End If
                        ElseIf nTrain Mod 2 <> 0 And i Mod 2 <> 0 Then '����
                            If Val(TrainInf(i).sPrintTrain) >= Val(sCurPrintCheCi) Then
                                TrainInf(i).sPrintTrain = Str(Val(TrainInf(i).sPrintTrain) - 1).Trim
                            End If
                        End If
                    End If
                Next
            End If
        Else
            MsgBox("��ǰ�г�����Ϊ�գ����ܵ���!", , "��ʾ")
        End If
    End Sub

    '����������Ĭ����ɫ������
    Public Sub SetTrainDefautColor(ByVal nTrain As Integer)
        If GetUserName() = "��������" Then
            If TrainInf(nTrain).TrainStyle = "���г�" Or TrainInf(nTrain).TrainStyle = "���γ�" Then
                If nTrain Mod 2 = 0 Then
                    TrainInf(nTrain).PrintLineColor = Color.Red
                Else
                    TrainInf(nTrain).PrintLineColor = Color.Green
                End If
            Else
                TrainInf(nTrain).PrintLineColor = Color.Black
            End If
            TrainInf(nTrain).PrintLineWidth = 2
        End If
    End Sub

    '�õ�ϵͳ��վ��
    Public Function GetSystemStaName(ByVal sStaName As String) As String
        GetSystemStaName = sStaName
        Dim i As Integer
        For i = 1 To UBound(StationInf)
            If StationInf(i).sStationName = sStaName Then
                If StationInf(i).sStaProperity = "�����ն�վ" Then
                    GetSystemStaName = StationInf(i).sPrintStaName
                    Exit For
                End If
            End If
        Next

    End Function

    '������ȥѡ�е��г�
    Public Sub AddSelectTrains(ByVal nSelectTrain As Integer)
        Dim i As Integer
        Dim sSelect As New System.Collections.Generic.List(Of Integer)
        sSelect.Clear()
        For i = 1 To UBound(TimeTablePara.nPubTrains)
            sSelect.Add(TimeTablePara.nPubTrains(i))
        Next
        Dim nIFIn As Boolean
        nIFIn = False
        For i = 1 To UBound(TimeTablePara.nPubTrains)
            If TimeTablePara.nPubTrains(i) = nSelectTrain Then
                sSelect.Remove(nSelectTrain)
                nIFIn = True
                Exit For
            End If
        Next
        If nIFIn = False Then
            sSelect.Add(nSelectTrain)
        End If
        ReDim TimeTablePara.nPubTrains(sSelect.Count)
        For i = 1 To UBound(TimeTablePara.nPubTrains)
            TimeTablePara.nPubTrains(i) = sSelect.Item(i - 1)
        Next
    End Sub
    '����ƽ���г�
    Public Sub MoveTrain(ByVal nTrain As Integer, ByVal nMoveTime As Integer)
        Dim i As Integer
        If nMoveTime <> 0 Then
            For i = 1 To UBound(TrainInf(nTrain).Arrival)
                TrainInf(nTrain).Arrival(i) = TimeAdd(TrainInf(nTrain).Arrival(i), nMoveTime)
                TrainInf(nTrain).Starting(i) = TimeAdd(TrainInf(nTrain).Starting(i), nMoveTime)
            Next
            TrainInf(nTrain).lAllStartTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
            TrainInf(nTrain).lAllEndTime = TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID)))
        End If

    End Sub

    '�ж�����ͼ�Ƿ������ش���
    Public Function CheckTimetableBadError() As Boolean
        CheckTimetableBadError = True
        '�ж�����ͼ���ش���
        Dim Str As New System.Collections.Generic.List(Of String)
        Dim i As Integer
        For i = 1 To UBound(ChediInfo)
            If UBound(ChediInfo(i).nLinkTrain) > 0 Then
                Str.Add(ChediInfo(i).sCheCiHao)
            End If
        Next
        Dim tStr As System.Collections.Generic.List(Of String)
        tStr = GetSameString(Str)
        Dim tString As String
        tString = ""
        If tStr.Count > 0 Then
            For i = 1 To tStr.Count
                tString = tString & tStr.Item(i - 1) & "/"
            Next
            If MsgBox(" ���³��ױ�����ظ���ǿ�ҽ������鲢�޸�����ͼ���ٱ��棡��ȷ�����������棬��ȡ�����˳���" & (Chr(13)) & (Chr(10)) & tString, MsgBoxStyle.Information + MsgBoxStyle.OkCancel, "��ʾ") = MsgBoxResult.Cancel Then
                CheckTimetableBadError = False
            End If

        End If

    End Function

    ' ����г�����ͼ
    Public Sub CheckDiagram()
        ReDim TrainErrInf(0)

        '�۷�ʱ����
        Call EditAllJiaoLuLine()

        '���վǰ�۷��Ƿ����� ��������жϣ�վǰ�۷�ʱ
        Call ListNotSatisDaoFaTime()

        '׷�ټ��,����������ж�
        Call CheckIntervalStartAndArrival()

        '���ν�·ʱ��վ�������ж�
        Call CheckCirclePathArrivelAndStart()

        '��վ��·������
        Call CheckStationPath()

        ' Call ShowTrinErrorInfor(frmTimeTableMain.listViewTrain, frmTimeTableMain.ToolLabError)
    End Sub

    '��ʾ������Ϣ
    Public Sub ShowTrinErrorInfor(ByVal listViewTrain As ListView, ByVal ToolLabError As ToolStripStatusLabel)
        listViewTrain.Items.Clear()
        Dim i As Integer
        Dim lvItem As ListViewItem
        lvItem = Nothing
        Dim liFile(5) As String
        Dim nMax As Integer
        If TimeTablePara.sErrorShowStyle = "��ʾȫ��" Then
            nMax = UBound(TrainErrInf)
        Else
            nMax = Math.Min(20, UBound(TrainErrInf))
        End If

        For i = 1 To nMax
            liFile(0) = i
            liFile(1) = TrainInf(TrainErrInf(i).nTrain).sPrintTrain
            liFile(2) = TrainErrInf(i).nTrain
            liFile(3) = TrainErrInf(i).Scurtime
            liFile(4) = StationInf(TrainErrInf(i).nErrorSta).sStationName
            liFile(5) = TrainErrInf(i).sErrorMessage
            lvItem = New ListViewItem(liFile)
            listViewTrain.Items.Add(lvItem)
        Next
        If TimeTablePara.sErrorShowStyle = "��ʾȫ��" Then
            If UBound(TrainErrInf) > 0 Then
                ToolLabError.Text = "��ǰ����ͼ��" & UBound(TrainErrInf) & "������"
            Else
                ToolLabError.Text = "��ǰ����ͼû�д���"
            End If
        Else
            If UBound(TrainErrInf) > 20 Then
                ToolLabError.Text = "��ǰ����ͼ��" & UBound(TrainErrInf) & "������ֻ��ʾǰ20����"
            ElseIf UBound(TrainErrInf) <= 20 And UBound(TrainErrInf) > 0 Then
                ToolLabError.Text = "��ǰ����ͼ��" & UBound(TrainErrInf) & "�����󣡡�"
            Else
                ToolLabError.Text = "��ǰ����ͼû�д���"
            End If
        End If

    End Sub
    '�õ����վ��
    Public Function GetPrintStaNameFromStaName(ByVal sSta As String) As String
        GetPrintStaNameFromStaName = sSta
        Dim i As Integer
        For i = 1 To UBound(StationInf)
            If StationInf(i).sStationName = sSta Then
                GetPrintStaNameFromStaName = StationInf(i).sPrintStaName
                Exit For
            End If
        Next
    End Function

    '�õ����������
    Public Function GetPrintSecNameFromSecName(ByVal sSecName As String) As String
        GetPrintSecNameFromSecName = sSecName
        Dim i As Integer
        For i = 1 To UBound(SectionInf)
            If SectionInf(i).sSecName = sSecName Then
                GetPrintSecNameFromSecName = StationInf(SectionInf(i).nFirStaID).sPrintStaName & "->" & StationInf(SectionInf(i).nSecStaID).sPrintStaName
                Exit For
            End If
        Next
    End Function
End Module
