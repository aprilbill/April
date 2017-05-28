Module ModTimetableMain
    ''' <summary>
    ''' ����ͼ��վ���ƣ������վ���ƣ���·��һ��
    ''' </summary>
    ''' <remarks></remarks>
    Public OutputStationName As Dictionary(Of String, String)


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
        Dim nIfPrintDriverNo As Boolean '�Ƿ��ӡ˾�����
        Dim nCheDiLineHeight As Integer '�����߸߶�
        Dim sCheDiLineStyle As String '����������
        'Dim sCheCiShowStyle As Integer
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

        Dim UnAssignTrainLineStyle As String 'δ��ѡ�г�
        Dim UnAssignTrainLineWidth As Single
        Dim UnAssignTrainLineColor As String

        Dim DutyOnLineStyle As String '��������
        Dim DutyOnLineWidth As Single
        Dim DutyOnLineColor As String

        Dim DutyDinnerLineStyle As String '�ò�����
        Dim DutyDinnerLineWidth As Single
        Dim DutyDinnerLineColor As String

        Dim DutyRestLineStyle As String 'С������
        Dim DutyRestLineWidth As Single
        Dim DutyRestLineColor As String

        Dim DutyOffLineStyle As String '�������
        Dim DutyOffLineWidth As Single
        Dim DutyOffLineColor As String

        Dim DutyNoFontName As String   '������
        Dim DutyNoFontSize As Single
        Dim DutyNoFontBold As Boolean
        Dim DutyNoFontItalic As Boolean
        Dim DutyNoFontColor As String

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
        Dim nID As Integer 'ID
        'Dim sTRAINDIAGRAMID As String
    End Structure
    Public TimetableInf() As typeTimetableInf

    '����ƻ�����Ϣ
    Structure typeCSTimetableInf
        Dim sName As String 'ʱ�̱�����
        Dim sID As String 'ʱ�̱�ID
        Dim sCreateDate As Date '����ʱ��
        Dim sEditDate As Date '�޸�ʱ�� 
        Dim nID As Integer 'ID
        Dim sDiagramID As String
        Dim ScheduleState As CrewScheduleState
        Dim IFCorShcedule As Boolean
        Dim CorTimetableID As String
    End Structure

    Public CSTimetableInf() As typeCSTimetableInf

    '�Զ����ͼ����
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
    '����ʱ�̱����Ƶõ�ʱ�̱�ID
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

    'ͨ�����εõ�CheDiID
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

    'ͨ�����εõ��۷��г�����
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
    ''' ͨ�����εõ�DriverID
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

    '���г��ӳ������жϿ�
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
            '����һ����˾��
            ReDim Preserve CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers) + 1)
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)) = New CSDriver()
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID = UBound(CSTrainsAndDrivers.CSDrivers)
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSdriverNo = CSTrainsAndDrivers.CSDrivers(nDriverID).CSdriverNo & "_2"
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).DutySort = CSTrainsAndDrivers.CSDrivers(nDriverID).DutySort
            CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).State = "����"
            ReDim CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSLinkTrain(0)

            For i = flag To UBound(CSTrainsAndDrivers.CSDrivers(nDriverID).CSLinkTrain)
                ReDim Preserve CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSLinkTrain) + 1)
                CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSLinkTrain)) = CSTrainsAndDrivers.CSDrivers(nDriverID).CSLinkTrain(i)
            Next

            Select Case CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).DutySort
                Case "���"
                    CSTrainsAndDrivers.MorningDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
                Case "�װ�"
                    CSTrainsAndDrivers.DayDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
                Case "���ڰ�"
                    CSTrainsAndDrivers.CDayDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
                Case "ҹ��"
                    CSTrainsAndDrivers.NightDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
                Case Else
                    CSTrainsAndDrivers.OtherDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
            End Select

        End If
        '��ȥ���Ѻ�ĺ����г�
        CSTrainsAndDrivers.CSDrivers(nDriverID).RemoveTrain(flag - 1)
    End Sub
    '�ϲ�˾��
    Public Sub HeBinDriverInfo(ByVal nDriverID1 As Integer, ByVal nDriverID2 As Integer, ByVal nCSLinkTrain1 As Integer, ByVal nCSLinkTrain2 As Integer)
        'ǰ����seek���ڶ���˾�����������һ��˾��
        Dim i As Integer
        Dim nTime1 As Integer
        Dim nTime2 As Integer
        nTime1 = CSTrainsAndDrivers.CSLinkTrains(nCSLinkTrain1).StartTime
        nTime2 = CSTrainsAndDrivers.CSLinkTrains(nCSLinkTrain2).StartTime
        nTime1 = CSAddTimeOver24(nTime1)
        nTime2 = CSAddTimeOver24(nTime2)
        If nDriverID2 = 0 Then '�ڶ����г�û�м�ʻԱ��ʻ
            If nTime2 < nTime1 Then '���г�����˾���ĵ�һ������
                Dim tempmertrain As MergedCSLinkTrain = GetMergedTrain(CSTrainsAndDrivers.CSLinkTrains(nCSLinkTrain2))
                CSTrainsAndDrivers.CSDrivers(nDriverID1).AddReMergedTrain(tempmertrain)
            Else '���г�����˾�������һ������
                Dim tempmertrain As MergedCSLinkTrain = GetMergedTrain(CSTrainsAndDrivers.CSLinkTrains(nCSLinkTrain2))
                CSTrainsAndDrivers.CSDrivers(nDriverID1).AddMergedTrain(tempmertrain)
            End If
            CSTrainsAndDrivers.CSLinkTrains(nCSLinkTrain2).IsLinked = True
        ElseIf nDriverID2 > 0 Then  '�ڶ����г��м�ʻԱ��ʻ
            If nTime2 < nTime1 Then '��seek˾��������ӵ�ѡ��˾���ĺ���
                For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers(nDriverID1).CSLinkTrain)
                    ReDim Preserve CSTrainsAndDrivers.CSDrivers(nDriverID2).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(nDriverID2).CSLinkTrain) + 1)
                    CSTrainsAndDrivers.CSDrivers(nDriverID2).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(nDriverID2).CSLinkTrain)) = CSTrainsAndDrivers.CSDrivers(nDriverID1).CSLinkTrain(i)
                Next
                Call RemoveDriver(nDriverID1)
            Else '�Ӻ��ѡ�е�˾�������񲢸�seek˾��
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

    '����һ����ʱ��ʻԱ
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
            CSTrainsAndDrivers.CSDrivers(nCSDriverID).CSDriverID = nCSDriverID 'Ҫע��
            CSTrainsAndDrivers.CSDrivers(nCSDriverID).CSdriverNo = "#"
            CSTrainsAndDrivers.CSDrivers(nCSDriverID).DutySort = sdutysort
            'Call CopyCheDiInformation(nCheDINum, nCheDINum)
            AddNewDriverinfor = nCSDriverID
        End If
    End Function

    ' �޸�0-4���ʱ��ε�ʱ��ֵ������24Сʱ
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
