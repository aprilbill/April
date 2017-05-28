Module modDrawTrainDiagram

    Public sJiaoLuStyle As String '������·
    Public sFanJiaoLuStyle As String '����·
    Public sPuHuaStyle As String ' �̻�����
    Public sYXTJiaoLuStyle As String
    Public sYXTPuHuaStyle As String
    Public nChediIdNumber As Integer
    Public nJGFuYu As Long  '����ӵ�ʱ��
    Public nBaseJGTime() As Long '��׼վ�̻����ʱ��
    Public nElseJGTime() As Long '��һվ���̻����ʱ��

    Public sFirstBaseSta As String '��һ����׼վ������
    Public sElseBaseSta As String '��һ����վ������

    Public LeftBaseTrain() As Integer
    Public LeftElseTrain() As Integer

    Public RightBaseTrain() As Integer
    Public RightElseTrain() As Integer

    '�Ƿ����ĳ����������
    Public nIfBaseChuKu As Integer
    Public nIfElseChuKu As Integer
    Public nIfBaseRuKu As Integer
    Public nIfElseRuKu As Integer

    Public nDownTrainSeq() As Integer
    Public nUpTrainSeq() As Integer
    Public nAllTrainSeq() As Integer '�����ź�����г�'��ʱ����̳�ʱ
    Public nAllTrainBeforSeq() As Integer
    Public nTmpAllTrainSeq() As Integer
    Public nTmpAllTrainSeqDown() As Integer
    Public nTmpAllTrainSeqUp() As Integer

    Public sErrorInfor As String '�Ƿ�����̻�����

    Public BasePuHuaSeq() As Integer
    Public ElsePuHuaSeq() As Integer
    Public sBaseBeTime As Long
    Public sElseBeTime As Long

    Public BaseTimeMinus As Long 'λ��ʱ����
    Public ElseTimeMinus As Long '  λ��ʱ����

    Public sTrainJiaoLuStyle() As String '��·���ͣ��������

    Public tmpCDInfor() As typePublicChediInformation '��ʱ�ĳ��׽�·ֵ

    Public sPubJiaoLuUporDown As Integer '�жϽ�·�����л�������
    Public tmpCheDiInf() As typePublicChediInformation
    Public ChediMultiJLInfo() As typePublicChediInformation

    Public sBaseCalZFtime As Long 'ʵ���۷�ʱ��,�ڻ�׼վ
    Public sElseCalZFtime As Long 'ʵ���۷�ʱ��,����һվ
    Public ZhouQiTime As Long '����ʱ��
    Public nXianShuBaseRunTime As Long '����ʱ��
    Public nXianShuElseRunTime As Long '����ʱ��
    Public nXianShuZhouQiTime As Long '����ʱ������
    Public sLvXingSpeed As Single '�����ٶ�
    Public sJiShuSpeed As Single '�����ٶ�
    Public nBaseRunTime As Long '����ʱ��
    Public nElseRunTime As Long '����ʱ��
    Public ErrorInf() As String
    Public tmpShuZhu() As Integer
    Public tmpShuZhu1() As Integer

    Public sChuKuState As Integer '�������,�Ƿ�������������������
    Public sRuKuState As Integer '������,�Ƿ�������������������

    Structure typePrepareTrainSet
        Dim sCdid() As String
        Dim nTrain() As Integer
        Dim sSta As String
    End Structure
    Public BasePrepTrainset As typePrepareTrainSet
    Public ElsePrepTrainset As typePrepareTrainSet


    Structure typeGaoFenTimeSet '�߷���ʱ�䶨��
        Dim sJLstyle As String '��·����
        Dim sJLTrainStyle As String '��·���ͣ����λ�����
        Dim sPuHuaStyle As String '�̻����ͣ���˫���գ������ջ�����
        Dim nXuHao() As Integer 'ʱ������
        Dim BeTime() As Long 'ʱ��ο�ʼʱ��
        Dim EndTime() As Long 'ʱ��ν���ʱ��
        Dim JGtime() As Long '��ʱ��εĿ��м��
        Dim ChediNum() As Integer '��ʱ�����ҪͶ�˵ĳ�����
        Dim JGOne() As Long '����ǰһʱ��μ��ʱ��
        Dim JGTwo() As Long '���ں�һʱ��μ��ʱ��
        Dim NumOne() As Integer '����ǰһʱ��γ�������
        Dim NumTwo() As Integer '���ں�һʱ��γ�������
        Dim lZhouQiTime() As Long '��ʱ��ε�����ʱ��
        Dim sRunScaleName() As String '��ʱ��ε����б����
        Dim sStopScaleName() As String '��ʱ��ε�ͣվ�����
        Dim lFirZheFanTime() As Long '��ʱ��ε�ʼ���۷�ʱ��
        Dim lEndZheFanTime() As Long '��ʱ����յ����۷�ʱ��
        Dim lDownRunTime() As Long '��ʱ��ε���������ʱ��
        Dim lUpRunTime() As Long '��ʱ��ε���������ʱ��
        Dim lDownStopTime() As Long '��ʱ��ε�����ͣվʱ��
        Dim lUpStopTime() As Long '��ʱ��ε�����ͣվʱ��
    End Structure
    Public GaoFenTimeSet() As typeGaoFenTimeSet

    Structure typeTrainDiagramInfor
        Dim sJiaoLuStyle As String '��·����
        Dim sPuHuaStyle As String 'ͼ������,������,˫����,�ڼ��յ�
        Dim nShiJianDuan As Integer 'ʱ���
        Dim lngShiJianDuanFirTime() As String 'ʱ�����ʼʱ��
        Dim lngShiJianDuanEndTime() As String 'ʱ�����ʼʱ��
        Dim lngJianGeTime() As String '���ʱ��
        Dim nCheDiNumBer() As Integer '������
        Dim lngZhouQiTime As String '����
        Dim lngXianShuZhouQiTime As String '��������ʱ��
        Dim sLvXingSpeed As String '�����ٶ�
        Dim sJiShuSpeed As String '�����ٶ�
        Dim lngUpTime As String '��������ʱ��
        Dim lngDownTime As String '��������ʱ��
        Dim lngUpRunTime As String '��������ʱ��
        Dim lngDownRunTime As String '��������ʱ��
        Dim sStartSta As String 'ʼ��վ��
        Dim sEndSta As String '�յ�վ��
        Dim lngStartZFtime As String 'ʼ��վ�۷�ʱ��
        Dim lngEndZFTime As String '�յ�վ�۷�ʱ��
    End Structure
    Public TrainDiagramInfor As typeTrainDiagramInfor

    Structure AllCheDiCheDiInf
        Dim tmpCheDi() As typePublicChediInformation
    End Structure
    Public AllCheDiInf() As AllCheDiCheDiInf

    Structure AllCheDiTrainSeq
        Dim nBeforeBaseTrainSeq() As Integer
        Dim nBeforeElseTrainSeq() As Integer
        Dim nAfterBaseTrainSeq() As Integer
        Dim nAfterElseTrainSeq() As Integer
    End Structure

    Public CheDiTrainSeq() As AllCheDiTrainSeq

    '���������̻�
    Public Sub AllChediGaoFengBackGouHua(ByVal sJLstyle As String, ByVal sFanJiaoLuStyle As String, ByVal sPHstyle As String, ByVal proBar As ToolStripProgressBar)
        Dim i As Integer
        'Dim j As Integer
        'Dim k As Integer
        'Dim p As Integer
        'Dim deltaY As Single, pnl As Panel
        Dim nId As Integer

        ReDim AllCheDiInf(0)
        ReDim AllCheDiInf(0).tmpCheDi(0)
        ReDim CheDiTrainSeq(0)
        ReDim CheDiTrainSeq(0).nBeforeBaseTrainSeq(0)
        ReDim CheDiTrainSeq(0).nBeforeElseTrainSeq(0)
        ReDim CheDiTrainSeq(0).nAfterBaseTrainSeq(0)
        ReDim CheDiTrainSeq(0).nAfterElseTrainSeq(0)

        ReDim nAllTrainBeforSeq(0)
        ReDim nTmpAllTrainSeq(0)

        For i = 1 To UBound(GaoFenTimeSet)
            If GaoFenTimeSet(i).sJLstyle = sJLstyle And GaoFenTimeSet(i).sPuHuaStyle = sPHstyle Then
                nId = i
                Exit For
            End If
        Next i

        proBar.Value = proBar.Maximum * 1 / 4
        Call DrawZhouQiLine(sFirstBaseSta, sElseBaseSta, sJLstyle, sFanJiaoLuStyle, sPuHuaStyle, proBar)

        Call SetTmpAllTrainSeq()
        proBar.Value = proBar.Maximum * 1 / 3
        Call DrawLinkChuRuTrain(sFirstBaseSta, sElseBaseSta, sJLstyle, proBar, nId) '������ߣ������ߵ��̻�

        Call SetAllCheDiLink() '���ϰ������ɽ׶ε��ߣ������ȵ�


        Call InputCheDiInformation()
        'StatusBar1.Panels(1).Text = "�̻���������"

    End Sub

    '���������̻�'���ν�·ʱ��������
    Public Sub AllChediGaoFengBackGouHuaByCircle(ByVal sJLstyle As String, ByVal sFanJiaoLuStyle As String, ByVal sPHstyle As String, ByVal proBar As ToolStripProgressBar)
        Dim i As Integer
        Dim nId As Integer
        ReDim AllCheDiInf(0)
        ReDim AllCheDiInf(0).tmpCheDi(0)
        ReDim CheDiTrainSeq(0)
        ReDim CheDiTrainSeq(0).nBeforeBaseTrainSeq(0)
        ReDim CheDiTrainSeq(0).nBeforeElseTrainSeq(0)
        ReDim CheDiTrainSeq(0).nAfterBaseTrainSeq(0)
        ReDim CheDiTrainSeq(0).nAfterElseTrainSeq(0)

        ReDim nAllTrainBeforSeq(0)
        ReDim nTmpAllTrainSeq(0)

        For i = 1 To UBound(GaoFenTimeSet)
            If GaoFenTimeSet(i).sJLstyle = sJLstyle And GaoFenTimeSet(i).sPuHuaStyle = sPHstyle Then
                nId = i
                Exit For
            End If
        Next i

        proBar.Value = 0
        Call DrawZhouQiLineByCircle(sFirstBaseSta, sElseBaseSta, sJLstyle, sFanJiaoLuStyle, sPuHuaStyle, proBar)

        ' Call SetTmpAllTrainSeq()
        ' proBar.Value = proBar.Maximum * 2 / 3
        'Call DrawLinkChuRuTrain(sFirstBaseSta, sElseBaseSta, sJLstyle, proBar, nId) '������ߣ������ߵ��̻�

        'Call SetAllCheDiLink() '���ϰ������ɽ׶ε��ߣ������ȵ�


        Call InputCheDiInformation()
        'StatusBar1.Panels(1).Text = "�̻���������"

    End Sub

    '�����Ӵ����ߣ����������
    Private Sub DrawLinkChuRuTrain(ByVal sFirstBaseSta As String, ByVal sElseBaseSta As String, ByVal sJLstyle As String, ByVal proBar As ToolStripProgressBar, ByVal nId As Integer)
        Dim i As Integer
        ReDim tmpCheDiInf(0)
        Dim sValue As Single
        If UBound(AllCheDiInf) > 0 Then
            If TimeTablePara.bIFAutoAddChuKuTrain = True Then
                Call CallDrawDayChuKuLine(sFirstBaseSta, sElseBaseSta, 1, sJLstyle, nId)
            End If
            sValue = proBar.Maximum * 1 / 3 + (proBar.Maximum * 2 / 3) / UBound(AllCheDiInf)
            proBar.Value = sValue
            For i = 1 To UBound(AllCheDiInf) - 1
                Call CallDrawChuRuKuLine(sFirstBaseSta, sElseBaseSta, i, i + 1, sJLstyle, nId)
                sValue = proBar.Maximum * 1 / 3 + (proBar.Maximum * 2 / 3) * (i + 1) / UBound(AllCheDiInf)
                proBar.Value = proBar.Maximum * 1 / 3 + (proBar.Maximum * 2 / 3) * (i + 1) / UBound(AllCheDiInf)
            Next i
            If TimeTablePara.bIFAutoAddRuKuTrain = True Then
                Call CallDrawDayRuKuLine(sFirstBaseSta, sElseBaseSta, UBound(AllCheDiInf), sJLstyle, nId)
            End If
        End If

    End Sub

    'ȷ�����ڵļ��ʱ��
    Public Sub SetJGTime(ByVal nSetID As Integer, ByVal nId As Integer)
        Dim i As Integer
        ReDim nBaseJGTime(GaoFenTimeSet(nSetID).ChediNum(nId))
        For i = 1 To GaoFenTimeSet(nSetID).NumOne(nId)
            nBaseJGTime(i) = GaoFenTimeSet(nSetID).JGOne(nId)
        Next i
        For i = 1 To GaoFenTimeSet(nSetID).NumTwo(nId)
            nBaseJGTime(GaoFenTimeSet(nSetID).NumOne(nId) + i) = GaoFenTimeSet(nSetID).JGTwo(nId)
        Next i

        '����վ,���׼վ��ͬ

        ReDim nElseJGTime(UBound(nBaseJGTime))
        For i = 1 To UBound(nBaseJGTime)
            nElseJGTime(i) = nBaseJGTime(i)
        Next i
    End Sub

    '����������������

    Public Sub DrawZhouQiLine(ByVal sFirstBaseSta As String, ByVal sElseBaseSta As String, ByVal sJLstyle As String, ByVal sFanJLStyle As String, ByVal sPHstyle As String, ByVal proBar As ToolStripProgressBar)
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim p As Integer
        Dim tmpTime As Long

        Dim CurMoveCDid As Integer '��ʱ��������ŵ�ǰ�����ĳ��׵�ID
        Dim tmpTime1 As Long
        Dim tmpTime2 As Long
        Dim nElseTrain As Integer

        Dim nBaseBeginTime As Long
        Dim nElseBeginTime1 As Long
        Dim nElseBeginTime2 As Long

        Dim nBaseEndTime As Long
        Dim nElseEndTime As Long

        Dim tmpRunTime As Long
        Dim tmpRunTime2 As Long
        Dim tmpZFTime As Long
        Dim tmpZFTime2 As Long

        Dim tmpI As Integer
        Dim tmpZhu() As Integer
        ReDim tmpZhu(0)
        Dim sZtime As Long

        Dim BaseGaoFenStartTime As Long '��׼վ�ĸ߷忪ʼʱ��
        Dim BaseGaoFenEndTime As Long '����ʱ��
        Dim BaseGaoFenJGtime As Long '��׼վ�߷���ʱ��

        Dim BeforeBaseGaoFenStartTime As Long '��׼վ��ǰ�߷忪ʼʱ��
        Dim BeforeBaseGaoFenJGtime As Long '��׼վǰ�߷���ʱ��
        Dim ElseGaoFenStartTime As Long
        Dim AfterBaseGaoFenEndTime As Long '�����ʱ��
        Dim AfterBaseGaoFenJGtime As Long '��׼վ��߷���ʱ��

        Dim sRunScaleName As String
        Dim sStopScaleName As String

        Dim DeltTime As Long '����ʱ���
        DeltTime = 0

        Dim tmpI2 As Integer
        Dim nExit As Integer
        Dim nExit1 As Integer

        For i = 1 To UBound(GaoFenTimeSet)
            If GaoFenTimeSet(i).sJLstyle = sJLstyle And GaoFenTimeSet(i).sPuHuaStyle = sPHstyle Then
                If UBound(GaoFenTimeSet(i).nXuHao) > 0 Then
                    For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                        If j = 1 Then

                            BaseGaoFenJGtime = GaoFenTimeSet(i).JGtime(j)
                            BaseGaoFenStartTime = GaoFenTimeSet(i).BeTime(j)
                            BaseGaoFenEndTime = GaoFenTimeSet(i).EndTime(j)

                            BeforeBaseGaoFenStartTime = 0
                            BeforeBaseGaoFenJGtime = 10000

                            If j = UBound(GaoFenTimeSet(i).nXuHao) Then
                                AfterBaseGaoFenEndTime = 0
                                AfterBaseGaoFenJGtime = 10000
                            Else
                                AfterBaseGaoFenEndTime = GaoFenTimeSet(i).EndTime(j + 1)
                                AfterBaseGaoFenJGtime = GaoFenTimeSet(i).JGtime(j + 1)
                            End If

                        ElseIf j = UBound(GaoFenTimeSet(i).nXuHao) Then
                            BaseGaoFenJGtime = GaoFenTimeSet(i).JGtime(j)
                            BaseGaoFenStartTime = GaoFenTimeSet(i).BeTime(j)
                            BaseGaoFenEndTime = GaoFenTimeSet(i).EndTime(j)

                            BeforeBaseGaoFenStartTime = GaoFenTimeSet(i).BeTime(j - 1)
                            BeforeBaseGaoFenJGtime = GaoFenTimeSet(i).JGtime(j - 1)

                            AfterBaseGaoFenEndTime = 0
                            AfterBaseGaoFenJGtime = 10000
                        Else


                            BaseGaoFenJGtime = GaoFenTimeSet(i).JGtime(j)
                            BaseGaoFenStartTime = GaoFenTimeSet(i).BeTime(j)
                            BaseGaoFenEndTime = GaoFenTimeSet(i).EndTime(j)

                            BeforeBaseGaoFenStartTime = GaoFenTimeSet(i).BeTime(j - 1)
                            BeforeBaseGaoFenJGtime = GaoFenTimeSet(i).JGtime(j - 1)
                            AfterBaseGaoFenEndTime = GaoFenTimeSet(i).EndTime(j + 1)
                            AfterBaseGaoFenJGtime = GaoFenTimeSet(i).JGtime(j + 1)

                        End If

                        ReDim Preserve AllCheDiInf(j)
                        ReDim Preserve AllCheDiInf(j).tmpCheDi(0)
                        ReDim Preserve CheDiTrainSeq(j)


                        ReDim tmpCheDiInf(0)
                        ReDim nAllTrainSeq(0)



                        '*********************************************************'��ʱ���������
                        Dim TimeCha As Long '����ʱ�ֲ�


                        sZtime = GaoFenTimeSet(i).lZhouQiTime(j)
                        tmpRunTime = GaoFenTimeSet(i).lDownRunTime(j) + GaoFenTimeSet(i).lDownStopTime(j)
                        tmpRunTime2 = GaoFenTimeSet(i).lUpRunTime(j) + GaoFenTimeSet(i).lUpStopTime(j)
                        Call SetJGTime(i, j)

                        tmpZFTime = GaoFenTimeSet(i).lFirZheFanTime(j)
                        tmpZFTime2 = GaoFenTimeSet(i).lEndZheFanTime(j)
                        sBaseCalZFtime = tmpZFTime
                        sElseCalZFtime = tmpZFTime2
                        TimeCha = tmpRunTime - tmpRunTime2

                        ZhouQiTime = GaoFenTimeSet(i).lZhouQiTime(j)

                        If UBound(GaoFenTimeSet(i).nXuHao) = 1 Then
                            nBaseBeginTime = BaseGaoFenStartTime + nXianShuBaseRunTime - nBaseRunTime
                            nElseBeginTime1 = tmpRunTime + BaseGaoFenStartTime + tmpZFTime2 + nXianShuBaseRunTime - nBaseRunTime
                            nElseBeginTime2 = nBaseBeginTime
                            nBaseEndTime = BaseGaoFenEndTime ' - tmpRunTime - tmpZFTime2 '- AfterBaseGaoFenJGtime
                            nElseEndTime = BaseGaoFenEndTime ' - tmpRunTime2 - tmpZftime ' - AfterBaseGaoFenJGtime
                        Else

                            '��߷�
                            If BaseGaoFenJGtime <= AfterBaseGaoFenJGtime And BaseGaoFenJGtime <= BeforeBaseGaoFenJGtime Then
                                BaseGaoFenStartTime = DeltTime + BeforeBaseGaoFenJGtime
                                nBaseBeginTime = BaseGaoFenStartTime + Math.Abs(TimeCha) '+ AfterBaseGaoFenJGtime
                                nElseBeginTime1 = tmpRunTime + nBaseBeginTime + tmpZFTime2
                                nElseBeginTime2 = nBaseBeginTime
                                nBaseEndTime = BaseGaoFenEndTime
                                nElseEndTime = nBaseEndTime
                                '��ƽ��
                            ElseIf BaseGaoFenJGtime >= AfterBaseGaoFenJGtime And BaseGaoFenJGtime >= BeforeBaseGaoFenJGtime Then
                                'nBaseBeginTime = BaseGaoFenStartTime + tmpRunTime2 + tmpZFTime + BeforeBaseGaoFenJGtime 'BaseGaoFenJGtime ' BeforeBaseGaoFenJGtime + AfterBaseGaoFenJGtime
                                nBaseBeginTime = DeltTime + BaseGaoFenJGtime
                                nElseBeginTime1 = nBaseBeginTime + tmpRunTime + tmpZFTime2
                                nElseBeginTime2 = nBaseBeginTime

                                nBaseEndTime = BaseGaoFenEndTime - tmpRunTime - tmpZFTime2 - BaseGaoFenJGtime 'BaseGaoFenEndTime - tmpRunTime - tmpZFTime2 - AfterBaseGaoFenJGtime
                                nElseEndTime = nBaseEndTime ' BaseGaoFenEndTime - tmpRunTime2 - tmpZFTime - AfterBaseGaoFenJGtime 'ElseGaoFenEndTime - tmpRunTime2 - tmpZftime - AfterBaseGaoFenJGtime
                                '��߷����
                            ElseIf BaseGaoFenJGtime <= AfterBaseGaoFenJGtime And BaseGaoFenJGtime >= BeforeBaseGaoFenJGtime Then
                                nBaseBeginTime = DeltTime + BaseGaoFenJGtime
                                'nBaseBeginTime = BaseGaoFenStartTime + tmpRunTime2 + tmpZFTime + BeforeBaseGaoFenJGtime ' + BeforeBaseGaoFenJGtime
                                nElseBeginTime1 = nBaseBeginTime + tmpRunTime + tmpZFTime2
                                nElseBeginTime2 = nBaseBeginTime
                                nBaseEndTime = BaseGaoFenEndTime

                                nElseEndTime = nBaseEndTime '- tmpRunTime2 - tmpZFtime
                                '��߷�ǰ
                            ElseIf BaseGaoFenJGtime >= AfterBaseGaoFenJGtime And BaseGaoFenJGtime <= BeforeBaseGaoFenJGtime Then
                                nBaseBeginTime = BaseGaoFenStartTime + nXianShuBaseRunTime - nBaseRunTime
                                nElseBeginTime1 = tmpRunTime + ElseGaoFenStartTime + tmpZFTime2 + nXianShuBaseRunTime - nBaseRunTime
                                nElseBeginTime2 = nBaseBeginTime
                                nBaseEndTime = BaseGaoFenEndTime - tmpRunTime - tmpZFTime2 - BaseGaoFenJGtime
                                nElseEndTime = nBaseEndTime 'BaseGaoFenEndTime - tmpRunTime2 - tmpZFTime '- AfterBaseGaoFenJGtime

                            End If
                        End If


                        tmpTime = nBaseBeginTime
                        nBaseBeginTime = tmpTime
                        tmpTime1 = nElseBeginTime1
                        nElseBeginTime1 = tmpTime1
                        tmpTime2 = tmpTime1

                        tmpI = 1
                        tmpI2 = 1
                        nExit = 0
                        nExit1 = 0

                        Dim nUporDown As Integer
                        Dim tTrain As Integer
                        Dim tmpTrain() As Integer
                        ReDim tmpTrain(0)
                        Dim tmpMaxTime As Long
                        tmpMaxTime = 0

                        sRunScaleName = GaoFenTimeSet(i).sRunScaleName(j)
                        sStopScaleName = GaoFenTimeSet(i).sStopScaleName(j)

                        For k = 1 To UBound(nBaseJGTime)

                            If tmpTime > tmpMaxTime Then
                                tmpMaxTime = tmpTime
                            End If
                            nUporDown = GetBasicTrainUpOrDown(sJLstyle)

                            If nUporDown = 1 Then
                                nElseTrain = AllCheDiAddNewTrain(sJLstyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                            Else
                                nElseTrain = AllCheDiAddNewTrain(sFanJLStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                            End If

                            If nElseTrain <> 0 Then
                                TrainInf(nElseTrain).nIfCanMove = 1
                                ReDim Preserve nAllTrainSeq(UBound(nAllTrainSeq) + 1)
                                nAllTrainSeq(UBound(nAllTrainSeq)) = nElseTrain
                                Call SetAllTrainSeq()

                                CurMoveCDid = GetCurMoveCDid(nElseTrain)
                                Call AllCheDiAddLianGuaCheCi(CurMoveCDid, nElseTrain)
                                ReDim Preserve tmpTrain(UBound(tmpTrain) + 1)
                                tmpTrain(UBound(tmpTrain)) = nElseTrain
                            End If

                            tmpTime = tmpTime + nBaseJGTime(k)
                            If tmpTime > nBaseEndTime Then 'tmpTime + nBaseJGTime(k - 1) > nBaseEndTime + BaseGaoFenJGtime Then
                                Exit For
                            End If
                        Next k

                        BaseTimeMinus = 0
                        ElseTimeMinus = 0

                        Dim tmpEndTime As Long
                        If UBound(tmpTrain) > 0 Then

                            For k = 1 To UBound(tmpTrain)
                                nUporDown = 2
                                tTrain = tmpTrain(k)
                                tmpTime2 = AddLitterTime(GetTrainArriOrStartTime(tTrain, -1, 0)) + sElseCalZFtime
                                If tmpTime2 >= nBaseEndTime Then
                                    Exit For
                                End If
                                CurMoveCDid = AllChediFromTrainToCDid(tTrain)
                                Do
                                    If nUporDown = 1 Then
                                        nElseTrain = AllCheDiAddNewTrain(sJLstyle, sRunScaleName, sStopScaleName, "", tmpTime2, 1)

                                        If tmpTime2 > tmpMaxTime Then
                                            tmpMaxTime = tmpTime2
                                        End If

                                        If nElseTrain <> 0 Then
                                            ReDim Preserve nAllTrainSeq(UBound(nAllTrainSeq) + 1)
                                            nAllTrainSeq(UBound(nAllTrainSeq)) = nElseTrain
                                            TrainInf(nElseTrain).nIfCanMove = 1
                                            Call AllCheDiAddLianGuaCheCi(CurMoveCDid, nElseTrain)
                                            tmpEndTime = AddLitterTime(GetTrainArriOrStartTime(nElseTrain, -1, 0))
                                            tmpTime2 = tmpEndTime + sElseCalZFtime
                                            'If tmpTime2 > tmpMaxTime Then
                                            '    tmpMaxTime = tmpTime2
                                            'End If
                                        End If
                                        If tmpTime2 > nElseEndTime Then
                                            Exit Do
                                        End If
                                        nUporDown = 2
                                    Else
                                        nElseTrain = AllCheDiAddNewTrain(sFanJLStyle, sRunScaleName, sStopScaleName, "", tmpTime2, 1)
                                        If nElseTrain <> 0 Then
                                            TrainInf(nElseTrain).nIfCanMove = 1
                                            ReDim Preserve nAllTrainSeq(UBound(nAllTrainSeq) + 1)
                                            nAllTrainSeq(UBound(nAllTrainSeq)) = nElseTrain

                                            Call AllCheDiAddLianGuaCheCi(CurMoveCDid, nElseTrain)
                                            tmpEndTime = AddLitterTime(GetTrainArriOrStartTime(nElseTrain, -1, 0))
                                            tmpTime2 = AddLitterTime(GetTrainArriOrStartTime(nElseTrain, -1, 0)) + sBaseCalZFtime
                                            If tmpTime2 > tmpMaxTime Then
                                                tmpMaxTime = tmpTime2
                                            End If
                                        End If

                                        If tmpTime2 > nBaseEndTime Then
                                            Exit Do
                                        End If
                                        nUporDown = 1
                                    End If
                                Loop
                            Next k

                            DeltTime = tmpMaxTime
                            'If DeltTime >= 60 Then
                            '    DeltTime = DeltTime - 60 ' �������ӵĻ���ʱ��
                            'End If
                        End If

                        tmpTime2 = nBaseBeginTime + tmpRunTime + sElseCalZFtime
                        If nElseBeginTime2 > 0 Then
                            For k = UBound(nElseJGTime) To 1 Step -1
                                tmpTime2 = tmpTime2 - nElseJGTime(k)
                                If tmpTime2 < nElseBeginTime2 Then '- ZhouQiTime Then
                                    Exit For
                                End If
                                nUporDown = GetBasicTrainUpOrDown(sFanJLStyle)
                                If nUporDown = 1 Then
                                    nElseTrain = AllCheDiAddNewTrain(sJLstyle, sRunScaleName, sStopScaleName, "", tmpTime2, 1)
                                Else
                                    nElseTrain = AllCheDiAddNewTrain(sFanJLStyle, sRunScaleName, sStopScaleName, "", tmpTime2, 1)
                                End If
                                If nElseTrain <> 0 Then
                                    TrainInf(nElseTrain).nIfCanMove = 1

                                    ReDim Preserve nAllTrainSeq(UBound(nAllTrainSeq) + 1)
                                    nAllTrainSeq(UBound(nAllTrainSeq)) = nElseTrain

                                    ReDim Preserve tmpZhu(UBound(tmpZhu) + 1)
                                    tmpZhu(UBound(tmpZhu)) = nElseTrain
                                    CurMoveCDid = SeekCheDiFromTrainUMT(nElseTrain)
                                    Call AllCheDiAddLianGuaCheCi(CurMoveCDid, nElseTrain)
                                End If
                            Next k
                        End If


                        Call SetAllTrainSeq()

                        Call InputTrainSeq(sFirstBaseSta, sElseBaseSta, j) '����������г�����
                        Call GetPuHuaXuLie(sFirstBaseSta, sElseBaseSta, j)

                        ReDim AllCheDiInf(j).tmpCheDi(UBound(tmpCheDiInf))
                        For k = 1 To UBound(tmpCheDiInf)
                            AllCheDiInf(j).tmpCheDi(k).sCheDiID = tmpCheDiInf(k).sCheDiID
                            For p = 1 To UBound(tmpCheDiInf(k).nLinkTrain)
                                ReDim Preserve AllCheDiInf(j).tmpCheDi(k).nLinkTrain(UBound(tmpCheDiInf(k).nLinkTrain))
                                AllCheDiInf(j).tmpCheDi(k).nLinkTrain(p) = tmpCheDiInf(k).nLinkTrain(p)
                            Next p
                        Next k

                        For k = 1 To UBound(nAllTrainSeq)
                            ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                            nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nAllTrainSeq(k)
                        Next k
                    Next j
                Else
                    MsgBox("û�ж��巢����������ȶ��壡", , "��ʾ")
                    Exit Sub
                End If

                Exit For
            End If
        Next i

    End Sub

    '����������������'���ν�·��������
    Public Sub DrawZhouQiLineByCircle(ByVal sFirstBaseSta As String, ByVal sElseBaseSta As String, ByVal sJLstyle As String, ByVal sFanJLStyle As String, ByVal sPHstyle As String, ByVal proBar As ToolStripProgressBar)
        Dim i As Integer
        Dim j, k As Integer
        Dim tmpMaxTime As Long
        Dim tmpTime As Long
        tmpMaxTime = 0
        Dim nUporDown As Integer
        Dim nElseTrain As Integer
        Dim sRunScaleName As String
        Dim sStopScaleName As String
        Dim CurMoveCDid As Integer
        Dim tmpTrain() As String
        ReDim tmpTrain(0)
        Dim nBaseEndTime As Long
        Dim DeltTime As Integer
        Dim tmpTime1 As Long
        Dim tmpEndTime As Long
        Dim tTrain As Integer
        Dim tmpTime2 As Long
        tmpTime = 0
        tmpTime2 = 0
        If UBound(GaoFenTimeSet) > 0 Then
            For i = 1 To UBound(GaoFenTimeSet)
                If GaoFenTimeSet(i).sJLstyle = sJLstyle And GaoFenTimeSet(i).sPuHuaStyle = sPHstyle Then
                    If UBound(GaoFenTimeSet(i).nXuHao) > 0 Then
                        For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                            ReDim tmpTrain(0)
                            tmpTime1 = tmpMaxTime
                            ''tmpTime = GaoFenTimeSet(i).BeTime(j)
                            If tmpTime1 > 0 Then
                                tmpTime = tmpTime1 + GaoFenTimeSet(i).JGtime(j)
                            Else
                                tmpTime = GaoFenTimeSet(i).BeTime(j)
                            End If
                            nBaseEndTime = GaoFenTimeSet(i).EndTime(j)
                            nUporDown = GetBasicTrainUpOrDown(sJLstyle)
                            sRunScaleName = GaoFenTimeSet(i).sRunScaleName(j)
                            sStopScaleName = GaoFenTimeSet(i).sStopScaleName(j)
                            sElseCalZFtime = GaoFenTimeSet(i).lEndZheFanTime(j)
                            Call SetJGTime(i, j)
                            For k = 1 To UBound(nBaseJGTime)

                                nElseTrain = AllCheDiAddNewTrain(sJLstyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                                If nElseTrain <> 0 Then
                                    TrainInf(nElseTrain).nIfCanMove = 1
                                    ReDim Preserve nAllTrainSeq(UBound(nAllTrainSeq) + 1)
                                    nAllTrainSeq(UBound(nAllTrainSeq)) = nElseTrain
                                    Call SetAllTrainSeq()

                                    CurMoveCDid = AllCheDiSeekChediCircleJL(TrainInf(nElseTrain).SCheDiLeiXing, nElseTrain, GaoFenTimeSet(i).JGtime(j))
                                    Call AllCheDiAddLianGuaCheCi(CurMoveCDid, nElseTrain)
                                    ReDim Preserve tmpTrain(UBound(tmpTrain) + 1)
                                    tmpTrain(UBound(tmpTrain)) = nElseTrain
                                End If

                                tmpTime = tmpTime + nBaseJGTime(k)
                                If tmpTime > nBaseEndTime Then 'tmpTime + nBaseJGTime(k - 1) > nBaseEndTime + BaseGaoFenJGtime Then
                                    Exit For
                                End If
                            Next k

                            If UBound(tmpTrain) > 0 Then

                                For k = 1 To UBound(tmpTrain)
                                    tTrain = tmpTrain(k)
                                    tmpTime2 = AddLitterTime(GetTrainArriOrStartTime(tTrain, -1, 0)) + sElseCalZFtime
                                    If tmpTime2 >= nBaseEndTime Then
                                        Exit For
                                    End If
                                    CurMoveCDid = AllChediFromTrainToCDid(tTrain)
                                    Do
                                        nElseTrain = AllCheDiAddNewTrain(sJLstyle, sRunScaleName, sStopScaleName, "", tmpTime2, 1)
                                        If tmpTime2 > tmpMaxTime Then
                                            tmpMaxTime = tmpTime2
                                        End If

                                        If nElseTrain <> 0 Then
                                            ReDim Preserve nAllTrainSeq(UBound(nAllTrainSeq) + 1)
                                            nAllTrainSeq(UBound(nAllTrainSeq)) = nElseTrain
                                            TrainInf(nElseTrain).nIfCanMove = 1
                                            Call AllCheDiAddLianGuaCheCi(CurMoveCDid, nElseTrain)
                                            tmpEndTime = AddLitterTime(GetTrainArriOrStartTime(nElseTrain, -1, 0))
                                            tmpTime2 = tmpEndTime + sElseCalZFtime
                                            'If tmpTime2 > tmpMaxTime Then
                                            '    tmpMaxTime = tmpTime2
                                            'End If
                                        End If
                                        If tmpTime2 > nBaseEndTime Then
                                            Exit Do
                                        End If
                                    Loop
                                Next k

                                DeltTime = tmpMaxTime
                                'If DeltTime >= 60 Then
                                '    DeltTime = DeltTime - 60 ' �������ӵĻ���ʱ��
                                'End If
                            End If

                        Next
                    End If
                    Exit For
                End If
                proBar.Value = (proBar.Maximum * 2 / 3) * i / UBound(GaoFenTimeSet)
            Next i
        End If
    End Sub
    '�����е��г�����һ�������ﰴʱ��˳������
    Private Sub SetTmpAllTrainSeq()
        Dim j As Integer

        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long

        '������ʱ������
        Flag = 1
        k = UBound(nTmpAllTrainSeq)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                TempTime1 = AddLitterTime(TrainInf(nTmpAllTrainSeq(j)).lAllEndTime)
                Temptime2 = AddLitterTime(TrainInf(nTmpAllTrainSeq(j + 1)).lAllEndTime)
                If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                    temp = nTmpAllTrainSeq(j)
                    nTmpAllTrainSeq(j) = nTmpAllTrainSeq(j + 1)
                    nTmpAllTrainSeq(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop
    End Sub


    '������ʱ�����εĳ��׹�����
    Private Sub SetAllCheDiLink()
        Dim j As Integer
        Dim k As Integer
        ReDim tmpCheDiInf(0)
        Dim nTrain1 As Integer
        Dim nTrain2 As Integer

        If UBound(AllCheDiInf) > 0 Then
            For j = 1 To UBound(AllCheDiInf(1).tmpCheDi)
                ReDim Preserve tmpCheDiInf(UBound(tmpCheDiInf) + 1)
                tmpCheDiInf(UBound(tmpCheDiInf)).sCheDiID = UBound(tmpCheDiInf)
                ReDim tmpCheDiInf(UBound(tmpCheDiInf)).nLinkTrain(UBound(AllCheDiInf(1).tmpCheDi(j).nLinkTrain))
                For k = 1 To UBound(AllCheDiInf(1).tmpCheDi(j).nLinkTrain)
                    tmpCheDiInf(UBound(tmpCheDiInf)).nLinkTrain(k) = AllCheDiInf(1).tmpCheDi(j).nLinkTrain(k)
                Next k
            Next j

            If UBound(AllCheDiInf) > 1 Then
                For j = 2 To UBound(AllCheDiInf)
                    For k = 1 To UBound(AllCheDiInf(j).tmpCheDi)
                        nTrain2 = AllCheDiInf(j).tmpCheDi(k).nLinkTrain(1)
                        nTrain1 = TrainInf(nTrain2).nLinkLeft
                        If nTrain1 <> 0 Then
                            Call HeBinTmpCheDiInf(nTrain1, nTrain2, j)
                        Else
                            Call AddNewTmpCheDi(nTrain2, j)
                        End If
                    Next k
                Next j
            End If
        End If
    End Sub

    '��tmpChediinf()���뵽chediinfo()
    Public Sub InputCheDiInformation()
        ReDim ChediInfo(0)
        Dim nFtrain As Integer
        Dim nTrain As Integer
        Dim nTnum As Integer
        Dim i, j As Integer
        For i = 1 To UBound(tmpCheDiInf)
            If UBound(tmpCheDiInf(i).nLinkTrain) > 0 Then
                ReDim Preserve ChediInfo(UBound(ChediInfo) + 1)
                nTnum = UBound(ChediInfo)
                Call CopyCheDiInformation(nTnum, tmpCheDiInf(i).sCheDiID)
                'ChediInfo(nTnum).sCheCiHao = tmpCheDiInf(i).sCheCiHao
                ReDim Preserve ChediInfo(nTnum).nLinkTrain(UBound(tmpCheDiInf(i).nLinkTrain))
                nFtrain = 0
                For j = 1 To UBound(tmpCheDiInf(i).nLinkTrain)
                    nTrain = tmpCheDiInf(i).nLinkTrain(j)
                    If nFtrain = 0 Then
                        TrainInf(nTrain).TrainReturn(1) = 0
                        TrainInf(nTrain).TrainReturn(2) = 0
                    Else
                        TrainInf(nFtrain).TrainReturn(2) = nTrain
                        TrainInf(nTrain).TrainReturn(1) = nFtrain
                        TrainInf(nTrain).TrainReturn(2) = 0
                    End If
                    ChediInfo(nTnum).nLinkTrain(j) = nTrain
                    TrainInf(nTrain).nCheDiPuOrNot = 1
                    nFtrain = nTrain
                Next j
            End If
        Next i

    End Sub


    '�糿����
    Private Sub CallDrawDayChuKuLine(ByVal sFirstBaseSta As String, ByVal sElseBaseSta As String, ByVal SecondQuDuanID As Integer, ByVal sJLstyle As String, ByVal nId As Integer)
        Dim i As Integer
        Dim j As Integer
        Dim nTrain As Integer
        Dim tmpTime As Long

        Dim UpChuKuNum As Integer
        Dim DownChuKuNum As Integer
        Dim nElseTrain As Integer
        Dim nTmpMulti As Integer
        Dim nLeftTrain As Integer
        Dim tmpTime4 As Long

        Dim sRunScaleName As String
        Dim sStopScaleName As String
        Dim sRunJiaoLu As String
        Dim tmpBaseTrain() As Integer
        Dim tmpElseTrain() As Integer
        Dim nTmpState As Integer
        Dim nTmpNum As Integer
        Dim tmpZFTime As Long
        Dim tmpRunTime As Long

        sRunScaleName = GaoFenTimeSet(nId).sRunScaleName(SecondQuDuanID)
        sStopScaleName = GaoFenTimeSet(nId).sStopScaleName(SecondQuDuanID)


        '********************************** ���߶��ܳ���
        If nIfBaseChuKu = 1 And nIfElseChuKu = 1 Then
            '��ֵ�������ĸ���ֵ

            For i = 1 To UBound(CheDiTrainSeq)
                If i = SecondQuDuanID Then
                    ReDim RightBaseTrain(UBound(CheDiTrainSeq(i).nBeforeBaseTrainSeq))
                    ReDim RightElseTrain(UBound(CheDiTrainSeq(i).nBeforeElseTrainSeq))

                    For j = 1 To UBound(CheDiTrainSeq(i).nBeforeBaseTrainSeq)
                        RightBaseTrain(j) = CheDiTrainSeq(i).nBeforeBaseTrainSeq(j)
                    Next j
                    For j = 1 To UBound(CheDiTrainSeq(i).nBeforeElseTrainSeq)
                        RightElseTrain(j) = CheDiTrainSeq(i).nBeforeElseTrainSeq(j)
                    Next j
                    Exit For
                End If
            Next i

            UpChuKuNum = UBound(RightBaseTrain)
            DownChuKuNum = UBound(RightElseTrain)

            ReDim tmpBaseTrain(0)
            ReDim tmpElseTrain(0)

            For i = 1 To UBound(CheDiTrainSeq)
                If i = SecondQuDuanID Then
                    ReDim RightBaseTrain(UBound(CheDiTrainSeq(i).nBeforeBaseTrainSeq))
                    ReDim RightElseTrain(UBound(CheDiTrainSeq(i).nBeforeElseTrainSeq))

                    For j = 1 To UBound(CheDiTrainSeq(i).nBeforeBaseTrainSeq)
                        RightBaseTrain(j) = CheDiTrainSeq(i).nBeforeBaseTrainSeq(j)
                    Next j

                    For j = 1 To UBound(CheDiTrainSeq(i).nBeforeElseTrainSeq)
                        RightElseTrain(j) = CheDiTrainSeq(i).nBeforeElseTrainSeq(j)
                    Next j
                    Exit For
                End If
            Next i

            '���г���
            nTmpMulti = 0
            nTmpState = 0 'nBaseState
            nTmpNum = 0


            For i = UBound(RightBaseTrain) To 1 Step -1
                nTrain = RightBaseTrain(i)
                'If nTrain = 10 Then Stop
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllStartTime) - tmpZFTime

                nLeftTrain = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nTrain).lAllStartTime), sFirstBaseSta, 2, nTrain)
                If nLeftTrain > 0 Then
                    tmpTime4 = AddLitterTime(TrainInf(nLeftTrain).lAllEndTime) - GaoFenTimeSet(nId).JGtime(SecondQuDuanID)
                    If tmpTime4 < tmpTime Then
                        tmpTime = tmpTime4
                    End If
                End If

                sRunJiaoLu = GetRunJiaoLuName(sFirstBaseSta, "����")

                nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 2)

                If nElseTrain > 0 Then
                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                    'Call SetTmpAllTrainSeq
                    TrainInf(nElseTrain).nIfCanMove = 1
                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 0
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                    nTmpNum = nTmpNum + 1
                End If
            Next i

            '��һ�������
            nTmpMulti = 0
            nTmpState = 0 'nBaseState
            nTmpNum = 0

            '�жϳ���ʱ�Ǽ�������������һ��

            For i = UBound(RightElseTrain) To 1 Step -1
                nTrain = RightElseTrain(i)
                'If nTrain = 10 Then Stop
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllStartTime) - tmpZFTime

                nLeftTrain = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nTrain).lAllStartTime), sElseBaseSta, 2, nTrain)
                If nLeftTrain > 0 Then
                    tmpTime4 = AddLitterTime(TrainInf(nLeftTrain).lAllEndTime) - GaoFenTimeSet(nId).JGtime(SecondQuDuanID)
                    If tmpTime4 < tmpTime Then
                        tmpTime = tmpTime4
                    End If
                End If

                sRunJiaoLu = GetRunJiaoLuName(sElseBaseSta, "����")
                nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 2)

                If nElseTrain > 0 Then

                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                    'Call SetTmpAllTrainSeq
                    TrainInf(nElseTrain).nIfCanMove = 1
                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 0
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                    nTmpNum = nTmpNum + 1
                End If

            Next i

        ElseIf nIfBaseChuKu = 1 And nIfElseChuKu = 0 Then 'ֻ�ܻ�׼վ����
            '��ֵ�������ĸ���ֵ

            For i = 1 To UBound(CheDiTrainSeq)
                If i = SecondQuDuanID Then
                    ReDim RightBaseTrain(UBound(CheDiTrainSeq(i).nBeforeBaseTrainSeq))
                    ReDim RightElseTrain(UBound(CheDiTrainSeq(i).nBeforeElseTrainSeq))

                    For j = 1 To UBound(CheDiTrainSeq(i).nBeforeBaseTrainSeq)
                        RightBaseTrain(j) = CheDiTrainSeq(i).nBeforeBaseTrainSeq(j)
                    Next j
                    For j = 1 To UBound(CheDiTrainSeq(i).nBeforeElseTrainSeq)
                        RightElseTrain(j) = CheDiTrainSeq(i).nBeforeElseTrainSeq(j)
                    Next j
                    Exit For
                End If
            Next i

            UpChuKuNum = UBound(RightBaseTrain)
            DownChuKuNum = UBound(RightElseTrain)

            ReDim tmpBaseTrain(0)
            ReDim tmpElseTrain(0)

            For i = 1 To UBound(CheDiTrainSeq)
                If i = SecondQuDuanID Then
                    ReDim RightBaseTrain(UBound(CheDiTrainSeq(i).nBeforeBaseTrainSeq))
                    ReDim RightElseTrain(UBound(CheDiTrainSeq(i).nBeforeElseTrainSeq))

                    For j = 1 To UBound(CheDiTrainSeq(i).nBeforeBaseTrainSeq)
                        RightBaseTrain(j) = CheDiTrainSeq(i).nBeforeBaseTrainSeq(j)
                    Next j

                    For j = 1 To UBound(CheDiTrainSeq(i).nBeforeElseTrainSeq)
                        RightElseTrain(j) = CheDiTrainSeq(i).nBeforeElseTrainSeq(j)
                    Next j
                    Exit For
                End If
            Next i

            '��һ�������
            nTmpMulti = 0
            nTmpState = 0 'nBaseState
            nTmpNum = 0

            '�Ƚ���һ��վ���г��ܻ���

            For i = UBound(RightElseTrain) To 1 Step -1
                nTrain = RightElseTrain(i)


                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllStartTime) - tmpZFTime

                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sFirstBaseSta, "", tmpTime, 2, sJLstyle)

                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                'Call SetTmpAllTrainSeq
                TrainInf(nElseTrain).nIfCanMove = 1
                TrainInf(nTrain).nLeftState = 0
                TrainInf(nTrain).nRightState = 0
                TrainInf(nElseTrain).nLeftState = 0
                TrainInf(nElseTrain).nRightState = 0
                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                ReDim Preserve RightBaseTrain(UBound(RightBaseTrain) + 1)
                RightBaseTrain(UBound(RightBaseTrain)) = nElseTrain

            Next i


            '���г���
            nTmpMulti = 0
            nTmpState = 0 'nBaseState
            nTmpNum = 0
            For i = UBound(RightBaseTrain) To 1 Step -1
                nTrain = RightBaseTrain(i)


                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllStartTime) - tmpZFTime

                sRunJiaoLu = GetRunJiaoLuName(sFirstBaseSta, "����")
                nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 2)

                'nElseTrain = AllCheDiAddNewTrain("���⳵", sFirstBaseSta, "", tmpTime, 2, sJLstyle)
                If nElseTrain > 0 Then

                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                    'Call SetTmpAllTrainSeq
                    TrainInf(nElseTrain).nIfCanMove = 1
                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 0
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                    nTmpNum = nTmpNum + 1
                End If
            Next i



        ElseIf nIfBaseChuKu = 0 And nIfElseChuKu = 1 Then 'ֻ����һվ����

            '��ֵ�������ĸ���ֵ

            For i = 1 To UBound(CheDiTrainSeq)
                If i = SecondQuDuanID Then
                    ReDim RightBaseTrain(UBound(CheDiTrainSeq(i).nBeforeBaseTrainSeq))
                    ReDim RightElseTrain(UBound(CheDiTrainSeq(i).nBeforeElseTrainSeq))

                    For j = 1 To UBound(CheDiTrainSeq(i).nBeforeBaseTrainSeq)
                        RightBaseTrain(j) = CheDiTrainSeq(i).nBeforeBaseTrainSeq(j)
                    Next j
                    For j = 1 To UBound(CheDiTrainSeq(i).nBeforeElseTrainSeq)
                        RightElseTrain(j) = CheDiTrainSeq(i).nBeforeElseTrainSeq(j)
                    Next j
                    Exit For
                End If
            Next i

            UpChuKuNum = UBound(RightBaseTrain)
            DownChuKuNum = UBound(RightElseTrain)

            ReDim tmpBaseTrain(0)
            ReDim tmpElseTrain(0)

            '��һ�������
            nTmpMulti = 0
            nTmpState = 0 'nBaseState
            nTmpNum = 0

            '�Ƚ���һ��վ���г��ܻ���

            For i = UBound(RightBaseTrain) To 1 Step -1
                nTrain = RightBaseTrain(i)


                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(GetTrainArriOrStartTime(nTrain, 0, 1)) - tmpZFTime

                'sRunJiaoLu = GetRunJiaoLuName(sFirstBaseSta, "����")
                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                End If
                'nElseTrain = AllCheDiAddNewTrain("���г�", sElseBaseSta, "", tmpTime, 2, sJLstyle)

                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                'Call SetTmpAllTrainSeq
                TrainInf(nElseTrain).nIfCanMove = 1
                TrainInf(nTrain).nLeftState = 0
                TrainInf(nTrain).nRightState = 0
                TrainInf(nElseTrain).nLeftState = 0
                TrainInf(nElseTrain).nRightState = 0
                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                ReDim Preserve RightElseTrain(UBound(RightElseTrain) + 1)
                RightElseTrain(UBound(RightElseTrain)) = nElseTrain

            Next i


            '��һ�������
            nTmpMulti = 0
            nTmpState = 0 'nBaseState
            nTmpNum = 0

            '�жϳ���ʱ�Ǽ�������������һ��

            For i = UBound(RightElseTrain) To 1 Step -1
                nTrain = RightElseTrain(i)

                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllStartTime) - tmpZFTime

                sRunJiaoLu = GetRunJiaoLuName(sElseBaseSta, "����")
                nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 2)

                'nElseTrain = AllCheDiAddNewTrain("���⳵", sElseBaseSta, "", tmpTime, 2, sJLstyle)

                If (nElseTrain + nTrain) Mod 2 = 0 Then '��ʾ���⳵�뵱ǰ����ͬ�򳵣���ʱ���Ϊ�����۷�ʱ��
                    tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, "�����۷�")
                    tmpRunTime = CalTrainRunTimeFromTrain(TrainInf(nElseTrain).sJiaoLuName, TrainInf(nElseTrain).sRunScaleName, TrainInf(nElseTrain).sStopSclaeName)
                    TrainInf(nElseTrain).lAllEndTime = TimeMinus(TrainInf(nTrain).lAllStartTime, tmpZFTime)
                    TrainInf(nElseTrain).lAllStartTime = TimeMinus(TrainInf(nElseTrain).lAllEndTime, tmpRunTime)
                    Call DrawSingleTrain(nElseTrain, TrainInf(nElseTrain).lAllStartTime, 0)
                End If

                If nElseTrain > 0 Then

                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                    'Call SetTmpAllTrainSeq
                    TrainInf(nElseTrain).nIfCanMove = 1
                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 0
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                    nTmpNum = nTmpNum + 1
                End If
            Next i
            ''''        End If
        End If
    End Sub

    Private Sub CallDrawChuRuKuLine(ByVal sFirstBaseSta As String, ByVal sElseBaseSta As String, ByVal FirQuDuanID As Integer, ByVal SecondQuDuanID As Integer, ByVal sJLstyle As String, ByVal nId As Integer)


        If GaoFenTimeSet(nId).JGtime(SecondQuDuanID) < GaoFenTimeSet(nId).JGtime(FirQuDuanID) Then '����

            Call CallDrawChuKuLine(sFirstBaseSta, sElseBaseSta, FirQuDuanID, SecondQuDuanID, sJLstyle, nId)

        Else '***************************************************************************���

            Call CallDrawRuKuLine(sFirstBaseSta, sElseBaseSta, FirQuDuanID, SecondQuDuanID, sJLstyle, nId)

        End If


    End Sub




    '�����г����߷����
    Private Sub CallDrawChuKuLine(ByVal sFirstBaseSta As String, ByVal sElseBaseSta As String, ByVal FirQuDuanID As Integer, ByVal SecondQuDuanID As Integer, ByVal sJLstyle As String, ByVal nId As Integer)
        Dim i As Integer
        Dim j As Integer
        Dim nTrain As Integer
        Dim tmpTime As Long
        Dim nCurLeftArriTrain As Integer '���������г�����ߵĵ���ĵ�һ�г�,Ϊû�й����ĳ�
        Dim nTmpNum As Integer '
        Dim nTmpState As Integer
        Dim tmpTime4 As Long
        Dim tmpTime5 As Long
        Dim nElseTrain As Integer
        Dim nLeftTrain As Integer
        Dim nLeftTrain2 As Integer
        Dim nLeftStartTrain As Integer
        Dim UpChuKuNum As Integer
        Dim DownChuKuNum As Integer

        ReDim LeftBaseTrain(0)
        ReDim LeftElseTrain(0)
        ReDim RightBaseTrain(0)
        ReDim RightElseTrain(0)
        Dim tmpBaseTrain() As Integer
        Dim tmpElseTrain() As Integer
        ReDim tmpBaseTrain(0)
        ReDim tmpElseTrain(0)
        Dim nMultiChuKu As Integer
        Dim nTmpMulti As Integer
        Dim tmpTrain As Integer
        Dim tmpTrain1 As Integer
        Dim tmpTime1 As Long
        Dim nJunTime As Long
        Dim ntmpTimeLeft As Long
        Dim sRunScaleName As String
        Dim sStopScaleName As String
        Dim sRunJiaoLu As String
        Dim tmpZFTime As Long
        Dim tmpRunTime As Long

        sRunScaleName = GaoFenTimeSet(nId).sRunScaleName(FirQuDuanID)
        sStopScaleName = GaoFenTimeSet(nId).sStopScaleName(FirQuDuanID)


        '��ֵ�������ĸ���ֵ
        For i = 1 To UBound(CheDiTrainSeq)
            If i = FirQuDuanID Then
                ReDim LeftBaseTrain(UBound(CheDiTrainSeq(i).nAfterBaseTrainSeq))
                ReDim LeftElseTrain(UBound(CheDiTrainSeq(i).nAfterElseTrainSeq))

                For j = 1 To UBound(CheDiTrainSeq(i).nAfterBaseTrainSeq)
                    LeftBaseTrain(j) = CheDiTrainSeq(i).nAfterBaseTrainSeq(j)
                Next j
                For j = 1 To UBound(CheDiTrainSeq(i).nAfterElseTrainSeq)
                    LeftElseTrain(j) = CheDiTrainSeq(i).nAfterElseTrainSeq(j)
                Next j
                Exit For
            End If
        Next i

        For i = 1 To UBound(CheDiTrainSeq)
            If i = SecondQuDuanID Then
                ReDim RightBaseTrain(UBound(CheDiTrainSeq(i).nBeforeBaseTrainSeq))
                ReDim RightElseTrain(UBound(CheDiTrainSeq(i).nBeforeElseTrainSeq))

                For j = 1 To UBound(CheDiTrainSeq(i).nBeforeBaseTrainSeq)
                    RightBaseTrain(j) = CheDiTrainSeq(i).nBeforeBaseTrainSeq(j)
                Next j
                For j = 1 To UBound(CheDiTrainSeq(i).nBeforeElseTrainSeq)
                    RightElseTrain(j) = CheDiTrainSeq(i).nBeforeElseTrainSeq(j)
                Next j
                Exit For
            End If
        Next i

        UpChuKuNum = UBound(RightBaseTrain) - UBound(LeftElseTrain)
        DownChuKuNum = UBound(RightElseTrain) - UBound(LeftBaseTrain)



        '********************************** ��ͷ���ܳ��� **************************************

        If nIfBaseChuKu = 1 And nIfElseChuKu = 1 Then
            If UBound(LeftBaseTrain) > 1 Then
                tmpTrain = LeftElseTrain(UBound(LeftElseTrain))
                tmpTrain1 = RightBaseTrain(1)
                nJunTime = (TrainInf(tmpTrain1).lAllStartTime - TrainInf(tmpTrain).lAllStartTime) / (UBound(LeftBaseTrain) + 1)
                ntmpTimeLeft = TrainInf(tmpTrain).lAllStartTime
            End If
            ''            '����ǰ��ĳ�����С�۷�ʱ������
            For i = 1 To UBound(LeftBaseTrain)
                nTrain = LeftBaseTrain(i)

                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TimeAdd(TrainInf(nTrain).lAllEndTime, tmpZFTime))
                tmpTime1 = ntmpTimeLeft + i * nJunTime
                If tmpTime1 > tmpTime Then
                    tmpTime = tmpTime1
                End If

                'sRunJiaoLu = GetRunJiaoLuName(sFirstBaseSta, "����")
                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sFirstBaseSta, "", tmpTime, 1, sJLstyle)
                If nElseTrain <> 0 Then
                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain

                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 1
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)

                    ReDim Preserve tmpBaseTrain(UBound(tmpBaseTrain) + 1)
                    tmpBaseTrain(UBound(tmpBaseTrain)) = nElseTrain
                    'TrainInf(nElseTrain).nIfCanMove = 0
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i
            'Call SetTmpAllTrainSeq

            If UBound(LeftElseTrain) > 1 Then
                tmpTrain = LeftBaseTrain(UBound(LeftBaseTrain))
                tmpTrain1 = RightElseTrain(1)
                nJunTime = (TrainInf(tmpTrain1).lAllStartTime - TrainInf(tmpTrain).lAllStartTime) / (UBound(LeftElseTrain) + 1)
                ntmpTimeLeft = TrainInf(tmpTrain).lAllStartTime
            End If

            For i = 1 To UBound(LeftElseTrain)
                nTrain = LeftElseTrain(i)

                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TimeAdd(TrainInf(nTrain).lAllEndTime, tmpZFTime))

                tmpTime1 = ntmpTimeLeft + i * nJunTime
                If tmpTime1 > tmpTime Then
                    tmpTime = tmpTime1
                End If

                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sElseBaseSta, "", tmpTime, 1, sJLstyle)
                If nElseTrain <> 0 Then

                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain


                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 1
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)

                    ReDim Preserve tmpElseTrain(UBound(tmpElseTrain) + 1)
                    tmpElseTrain(UBound(tmpElseTrain)) = nElseTrain
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i
            'Call SetTmpAllTrainSeq


            '���г���
            nTmpMulti = 0
            nTmpState = 0 'nBaseState
            nTmpNum = 0

            ReDim tmpShuZhu(0)
            For i = UBound(RightBaseTrain) To 1 Step -1
                nTrain = RightBaseTrain(i)
                'If nTrain = 209 Then Stop
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllStartTime) - tmpZFTime

                nCurLeftArriTrain = SeekAllCheDiBeforeTimeJGTrainNotGou(AddLitterTime(TrainInf(nTrain).lAllStartTime), sFirstBaseSta, 2, nTrain)
                If TrainInf(nTrain).TrainReturnStyle(1) = "վ���۷�" Then
                    If nCurLeftArriTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime), AddLitterTime(TrainInf(nTrain).lAllStartTime), sFirstBaseSta, 1, nCurLeftArriTrain, nTrain)
                        If UBound(tmpShuZhu) >= 2 Then
                            nLeftTrain = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime), sFirstBaseSta, 2, nCurLeftArriTrain)
                            nLeftTrain2 = nCurLeftArriTrain ' SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sFirstBaseSta, 2, nLeftTrain)
                            nLeftStartTrain = tmpShuZhu(UBound(tmpShuZhu) - 1)
                            tmpTime4 = AddLitterTime(TrainInf(nLeftStartTrain).lAllStartTime)
                            'tmpTime4 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + StationInf(TrainInf(nTrain).nPathID(1)).IKK(1)
                            tmpTime5 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + (AddLitterTime(TrainInf(nLeftTrain).lAllEndTime) - AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime)) / 2
                            If tmpTime5 <= tmpTime Then
                                tmpTime = tmpTime5
                            Else
                                If tmpTime4 > tmpTime Then
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(tmpTime4 - tmpTime) '��ʾ���ֽ�·
                                    tmpTime = tmpTime4
                                End If
                            End If
                            nTmpState = 0
                        Else
                            If tmpTime >= TrainInf(nCurLeftArriTrain).lAllEndTime Then
                                TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                                TrainInf(nCurLeftArriTrain).nRightState = 0
                                nTmpState = 1
                            Else
                                TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                                TrainInf(nCurLeftArriTrain).nRightState = 0
                                nTmpState = 1
                                If MoveOneTrainLineSatReturn(nCurLeftArriTrain, FirQuDuanID, TrainInf(nCurLeftArriTrain).lAllEndTime - tmpTime, 2) = 1 Then
                                    '�ܵ�������
                                Else
                                    TrainInf(nTrain).nZfLimit = 1
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(TrainInf(nTrain).lAllStartTime - TrainInf(nCurLeftArriTrain).lAllEndTime)  '��ʾ���ֽ�·
                                End If
                            End If
                        End If
                    Else

                        If nCurLeftArriTrain = 0 Then
                            nLeftTrain = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nTrain).lAllStartTime), sFirstBaseSta, 2, nTrain)
                            nLeftTrain2 = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sFirstBaseSta, 2, nLeftTrain)
                            tmpTime5 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + (AddLitterTime(TrainInf(nLeftTrain).lAllEndTime) - AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime)) / 2
                            If tmpTime5 < tmpTime Then
                                tmpTime = tmpTime5
                            End If
                            nTmpState = 0
                        End If
                    End If


                    If nTmpState = 0 Or nTmpState = 2 Then
                        If TimeTablePara.bIFAutoAddChuKuTrain = True Then

                            sRunJiaoLu = GetRunJiaoLuName(sFirstBaseSta, "����")
                            nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 2)

                            'nElseTrain = AllCheDiAddNewTrain("���⳵", sFirstBaseSta, "", tmpTime, 2, sJLstyle)
                            If nElseTrain > 0 Then
                                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                                'Call SetTmpAllTrainSeq

                                TrainInf(nTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                TrainInf(nElseTrain).nLeftState = 0
                                TrainInf(nElseTrain).nRightState = 0
                                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                                nTmpNum = nTmpNum + 1
                            End If
                        End If
                    End If

                Else ''******************* վǰ�۷� *********************
                    If nCurLeftArriTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime + tmpZFTime), AddLitterTime(TrainInf(nTrain).lAllStartTime), sFirstBaseSta, 1, nCurLeftArriTrain, nTrain)
                        If UBound(tmpShuZhu) >= 1 Then
                            nTmpState = 0
                        Else
                            TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                            TrainInf(nCurLeftArriTrain).nRightState = 0
                            nTmpState = 1
                            TrainInf(nCurLeftArriTrain).lAllStartTime = TimeAdd(TrainInf(nCurLeftArriTrain).lAllStartTime, tmpTime - TrainInf(nCurLeftArriTrain).lAllEndTime)
                            Call DrawSingleTrain(nCurLeftArriTrain, TrainInf(nCurLeftArriTrain).lAllStartTime, 0)
                        End If
                    Else
                        nTmpState = 0
                    End If


                    If nTmpState = 0 Or nTmpState = 2 Then
                        If TimeTablePara.bIFAutoAddChuKuTrain = True Then

                            sRunJiaoLu = GetRunJiaoLuName(sFirstBaseSta, "����")
                            nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 2)

                            'nElseTrain = AllCheDiAddNewTrain("���⳵", sFirstBaseSta, "", tmpTime, 2, sJLstyle)
                            If nElseTrain > 0 Then
                                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                                'Call SetTmpAllTrainSeq

                                TrainInf(nTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                TrainInf(nElseTrain).nLeftState = 0
                                TrainInf(nElseTrain).nRightState = 0
                                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                                nTmpNum = nTmpNum + 1
                            End If
                        End If
                    End If
                End If
            Next i



            '��һ�������
            nTmpMulti = 0
            nTmpState = 0 'nBaseState
            nTmpNum = 0
            nMultiChuKu = 0
            '�жϳ���ʱ�Ǽ�������������һ��

            For i = UBound(RightElseTrain) To 1 Step -1
                nTrain = RightElseTrain(i)
                'If nTrain = 44 Then Stop
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllStartTime) - tmpZFTime

                nCurLeftArriTrain = SeekAllCheDiBeforeTimeJGTrainNotGou(AddLitterTime(TrainInf(nTrain).lAllStartTime), sElseBaseSta, 2, nTrain)

                If TrainInf(nTrain).TrainReturnStyle(1) = "վ���۷�" Then  '******************* վ���۷� *********************

                    If nCurLeftArriTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime), AddLitterTime(TrainInf(nTrain).lAllStartTime), sElseBaseSta, 1, nCurLeftArriTrain, nTrain)
                        If UBound(tmpShuZhu) >= 2 Then
                            nLeftTrain = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime), sElseBaseSta, 2, nCurLeftArriTrain)
                            nLeftTrain2 = nCurLeftArriTrain 'SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sElseBaseSta, 2, nLeftTrain)
                            nLeftStartTrain = tmpShuZhu(UBound(tmpShuZhu) - 1)
                            tmpTime4 = AddLitterTime(TrainInf(nLeftStartTrain).lAllStartTime)
                            'tmpTime4 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + StationInf(TrainInf(nTrain).nPathID(1)).IKK(1)
                            tmpTime5 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + (AddLitterTime(TrainInf(nLeftTrain).lAllEndTime) - AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime)) / 2
                            If tmpTime5 <= tmpTime Then
                                tmpTime = tmpTime5
                            Else
                                If tmpTime4 > tmpTime Then
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(tmpTime4 - tmpTime) '��ʾ���ֽ�·
                                    tmpTime = tmpTime4
                                End If
                            End If
                            nTmpState = 0
                        Else
                            If tmpTime >= TrainInf(nCurLeftArriTrain).lAllEndTime Then
                                TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                                TrainInf(nCurLeftArriTrain).nRightState = 0
                                nTmpState = 1
                            Else
                                TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                                TrainInf(nCurLeftArriTrain).nRightState = 0
                                nTmpState = 1
                                If MoveOneTrainLineSatReturn(nCurLeftArriTrain, FirQuDuanID, TrainInf(nCurLeftArriTrain).lAllEndTime - tmpTime, 2) = 1 Then
                                    '�ܵ�������
                                Else
                                    TrainInf(nTrain).nZfLimit = 1 'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(TrainInf(nTrain).lAllStartTime - TrainInf(nCurLeftArriTrain).lAllEndTime)  '��ʾ���ֽ�·
                                End If
                            End If
                        End If
                    Else
                        nLeftTrain = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nTrain).lAllStartTime), sElseBaseSta, 2, nTrain)
                        nLeftTrain2 = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sElseBaseSta, 2, nLeftTrain)
                        tmpTime5 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + (AddLitterTime(TrainInf(nLeftTrain).lAllEndTime) - AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime)) / 2
                        If tmpTime5 < tmpTime Then
                            tmpTime = tmpTime5
                        End If
                        nTmpState = 0
                    End If


                    If nTmpState = 0 Or nTmpState = 2 Then
                        If TimeTablePara.bIFAutoAddChuKuTrain = True Then
                            sRunJiaoLu = GetRunJiaoLuName(sElseBaseSta, "����")
                            nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 2)

                            'nElseTrain = AllCheDiAddNewTrain("���⳵", sElseBaseSta, "", tmpTime, 2, sJLstyle)
                            If nElseTrain > 0 Then

                                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                                'Call SetTmpAllTrainSeq

                                TrainInf(nTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                TrainInf(nElseTrain).nLeftState = 0
                                TrainInf(nElseTrain).nRightState = 0
                                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                                nTmpNum = nTmpNum + 1
                            End If
                        End If
                    End If

                Else  '******************* վǰ�۷� *********************

                    If nCurLeftArriTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime + tmpZFTime), AddLitterTime(TrainInf(nTrain).lAllStartTime), sElseBaseSta, 1, nCurLeftArriTrain, nTrain)
                        If UBound(tmpShuZhu) >= 1 Then
                            nTmpState = 0
                        Else
                            TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                            TrainInf(nCurLeftArriTrain).nRightState = 0
                            nTmpState = 1
                            TrainInf(nCurLeftArriTrain).lAllStartTime = TimeAdd(TrainInf(nCurLeftArriTrain).lAllStartTime, tmpTime - TrainInf(nCurLeftArriTrain).lAllEndTime)
                            Call DrawSingleTrain(nCurLeftArriTrain, TrainInf(nCurLeftArriTrain).lAllStartTime, 0)
                        End If
                    Else
                        nTmpState = 0
                    End If


                    If nTmpState = 0 Or nTmpState = 2 Then
                        If TimeTablePara.bIFAutoAddChuKuTrain = True Then

                            sRunJiaoLu = GetRunJiaoLuName(sElseBaseSta, "����")
                            nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 2)

                            'nElseTrain = AllCheDiAddNewTrain("���⳵", sElseBaseSta, "", tmpTime, 2, sJLstyle)
                            If nElseTrain > 0 Then

                                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                                'Call SetTmpAllTrainSeq

                                TrainInf(nTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                TrainInf(nElseTrain).nLeftState = 0
                                TrainInf(nElseTrain).nRightState = 0
                                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                                nTmpNum = nTmpNum + 1
                            End If
                        End If
                    End If
                End If
            Next i


            '********************************** ֻ���ڻ�׼վ���� **************************************

        ElseIf nIfBaseChuKu = 1 And nIfElseChuKu = 0 Then

            If UBound(LeftElseTrain) > 1 Then
                tmpTrain = LeftElseTrain(UBound(LeftElseTrain))
                tmpTrain1 = LeftElseTrain(1)
                nJunTime = (TrainInf(tmpTrain1).lAllStartTime - TrainInf(tmpTrain).lAllStartTime) / (UBound(LeftElseTrain) + 1)
                ntmpTimeLeft = TrainInf(tmpTrain).lAllStartTime
            End If

            For i = 1 To UBound(LeftElseTrain)
                nTrain = LeftElseTrain(i)

                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TimeAdd(TrainInf(nTrain).lAllEndTime, tmpZFTime))

                tmpTime1 = ntmpTimeLeft + i * nJunTime
                If tmpTime1 > tmpTime Then
                    tmpTime = tmpTime1
                End If

                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                End If

                ' nElseTrain = AllCheDiAddNewTrain("���г�", sElseBaseSta, "", tmpTime, 1, sJLstyle)
                If nElseTrain <> 0 Then

                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain


                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 1
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)

                    ReDim Preserve tmpElseTrain(UBound(tmpElseTrain) + 1)
                    tmpElseTrain(UBound(tmpElseTrain)) = nElseTrain
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i



            '����һվ�ұߵĳ�����С�۷�ʱ�����л���

            For i = UBound(RightElseTrain) To 1 Step -1
                nTrain = RightElseTrain(i)
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllStartTime) - tmpZFTime

                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sFirstBaseSta, "", tmpTime, 2, sJLstyle)

                If nElseTrain > 0 Then

                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain


                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 1
                    TrainInf(nElseTrain).nRightState = 0
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)

                    ReDim Preserve RightBaseTrain(UBound(RightBaseTrain) + 1)
                    RightBaseTrain(UBound(RightBaseTrain)) = nElseTrain
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i
            'Call SetTmpAllTrainSeq

            UpChuKuNum = UpChuKuNum + DownChuKuNum


            '���г���
            nTmpMulti = 0
            nTmpState = 0 'nBaseState
            nTmpNum = 0

            Call SetRightBaseTrainSeq()

            '�жϳ���ʱ�Ǽ�������������һ��
            If UpChuKuNum - UBound(LeftElseTrain) - UBound(LeftBaseTrain) - 1 > 0 Then
                nMultiChuKu = UpChuKuNum - UBound(LeftElseTrain) - UBound(LeftBaseTrain) - 1
            Else
                nMultiChuKu = 0
            End If


            ReDim tmpShuZhu(0)
            For i = UBound(RightBaseTrain) To 1 Step -1
                nTrain = RightBaseTrain(i)
                'If nTrain = 344 Then Stop
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllStartTime) - tmpZFTime

                nCurLeftArriTrain = SeekAllCheDiBeforeTimeJGTrainNotGou(AddLitterTime(TrainInf(nTrain).lAllStartTime), sFirstBaseSta, 2, nTrain)

                If TrainInf(nTrain).TrainReturnStyle(1) = "վ���۷�" Then '******************* վ���۷� *********************
                    If nCurLeftArriTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime), AddLitterTime(TrainInf(nTrain).lAllStartTime), sFirstBaseSta, 1, nCurLeftArriTrain, nTrain)
                        If UBound(tmpShuZhu) >= 2 Then
                            nLeftTrain = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime), sFirstBaseSta, 2, nCurLeftArriTrain)
                            nLeftTrain2 = nCurLeftArriTrain ' SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sFirstBaseSta, 2, nLeftTrain)
                            nLeftStartTrain = tmpShuZhu(UBound(tmpShuZhu) - 1)
                            tmpTime4 = AddLitterTime(TrainInf(nLeftStartTrain).lAllStartTime)
                            'tmpTime4 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + StationInf(TrainInf(nTrain).nPathID(1)).IKK(1)
                            tmpTime5 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + (AddLitterTime(TrainInf(nLeftTrain).lAllEndTime) - AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime)) / 2
                            If tmpTime5 <= tmpTime Then
                                tmpTime = tmpTime5
                            Else
                                If tmpTime4 > tmpTime Then
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(tmpTime4 - tmpTime) '��ʾ���ֽ�·
                                    tmpTime = tmpTime4
                                End If
                            End If
                            nTmpState = 0
                        Else
                            If tmpTime >= TrainInf(nCurLeftArriTrain).lAllEndTime Then
                                TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                                TrainInf(nCurLeftArriTrain).nRightState = 0
                                nTmpState = 1
                            Else
                                TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                                TrainInf(nCurLeftArriTrain).nRightState = 0
                                nTmpState = 1
                                If MoveOneTrainLineSatReturn(nCurLeftArriTrain, FirQuDuanID, TrainInf(nCurLeftArriTrain).lAllEndTime - tmpTime, 2) = 1 Then
                                    '�ܵ�������
                                Else
                                    TrainInf(nTrain).nZfLimit = 1 'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(TrainInf(nTrain).lAllStartTime - TrainInf(nCurLeftArriTrain).lAllEndTime)  '��ʾ���ֽ�·
                                End If
                            End If
                        End If
                    Else
                        nLeftTrain = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nTrain).lAllStartTime), sFirstBaseSta, 2, nTrain)
                        nLeftTrain2 = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sFirstBaseSta, 2, nLeftTrain)
                        tmpTime5 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + (AddLitterTime(TrainInf(nLeftTrain).lAllEndTime) - AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime)) / 2
                        If tmpTime5 < tmpTime Then
                            tmpTime = tmpTime5
                        End If
                        nTmpState = 0
                    End If


                    If nTmpState = 0 Or nTmpState = 2 Then
                        If TimeTablePara.bIFAutoAddChuKuTrain = True Then
                            sRunJiaoLu = GetRunJiaoLuName(sFirstBaseSta, "����")
                            nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 2)

                            ' nElseTrain = AllCheDiAddNewTrain("���⳵", sFirstBaseSta, "", tmpTime, 2, sJLstyle)
                            If nElseTrain > 0 Then
                                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                                Call SetTmpAllTrainSeq()

                                TrainInf(nTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                TrainInf(nElseTrain).nLeftState = 0
                                TrainInf(nElseTrain).nRightState = 0
                                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                                nTmpNum = nTmpNum + 1
                            End If
                        End If
                    End If

                Else '******************* վǰ�۷� *********************

                    If nCurLeftArriTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime + tmpZFTime), AddLitterTime(TrainInf(nTrain).lAllStartTime), sFirstBaseSta, 1, nCurLeftArriTrain, nTrain)
                        If UBound(tmpShuZhu) >= 1 Then
                            nTmpState = 0
                        Else
                            TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                            TrainInf(nCurLeftArriTrain).nRightState = 0
                            nTmpState = 1
                            TrainInf(nCurLeftArriTrain).lAllStartTime = TimeAdd(TrainInf(nCurLeftArriTrain).lAllStartTime, tmpTime - TrainInf(nCurLeftArriTrain).lAllEndTime)
                            Call DrawSingleTrain(nCurLeftArriTrain, TrainInf(nCurLeftArriTrain).lAllStartTime, 0)
                        End If
                    Else
                        nTmpState = 0
                    End If


                    If nTmpState = 0 Or nTmpState = 2 Then
                        If TimeTablePara.bIFAutoAddChuKuTrain = True Then

                            sRunJiaoLu = GetRunJiaoLuName(sFirstBaseSta, "����")
                            nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 2)

                            'nElseTrain = AllCheDiAddNewTrain("���⳵", sFirstBaseSta, "", tmpTime, 2, sJLstyle)
                            If nElseTrain > 0 Then
                                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                                Call SetTmpAllTrainSeq()

                                TrainInf(nTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                TrainInf(nElseTrain).nLeftState = 0
                                TrainInf(nElseTrain).nRightState = 0
                                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                                nTmpNum = nTmpNum + 1
                            End If
                        End If
                    End If
                End If
            Next i


            '********************************** ֻ������һվ���� **************************************
        ElseIf nIfBaseChuKu = 0 And nIfElseChuKu = 1 Then

            '����һվ�ұߵĳ�����С�۷�ʱ�����л���

            For i = UBound(RightBaseTrain) To 1 Step -1
                nTrain = RightBaseTrain(i)
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllStartTime) - tmpZFTime

                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                End If
                'nElseTrain = AllCheDiAddNewTrain("���г�", sElseBaseSta, "", tmpTime, 2, sJLstyle)
                If nElseTrain > 0 Then
                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain


                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 1
                    TrainInf(nElseTrain).nRightState = 0
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)

                    ReDim Preserve RightElseTrain(UBound(RightElseTrain) + 1)
                    RightElseTrain(UBound(RightElseTrain)) = nElseTrain
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i

            ''    '����ǰ��ĳ�����С�۷�ʱ������
            For i = 1 To UBound(LeftBaseTrain)
                nTrain = LeftBaseTrain(i)

                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TimeAdd(TrainInf(nTrain).lAllEndTime, tmpZFTime))
                tmpTime1 = ntmpTimeLeft + i * nJunTime
                '                tmpTime1 = sTimeToZhenShu(tmpTime1, sMoveJGTime)
                If tmpTime1 > tmpTime Then
                    tmpTime = tmpTime1
                End If

                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sFirstBaseSta, "", tmpTime, 1, sJLstyle)
                If nElseTrain <> 0 Then
                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain

                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 1
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)

                    ReDim Preserve tmpBaseTrain(UBound(tmpBaseTrain) + 1)
                    tmpBaseTrain(UBound(tmpBaseTrain)) = nElseTrain
                    'TrainInf(nElseTrain).nIfCanMove = 0
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i

            DownChuKuNum = UpChuKuNum + DownChuKuNum

            '���г���

            nTmpMulti = 0
            nTmpState = 0 'nBaseState
            nTmpNum = 0

            Call SetRightElseTrainSeq()

            '�жϳ���ʱ�Ǽ�������������һ��
            If DownChuKuNum - UBound(LeftElseTrain) - UBound(LeftBaseTrain) - 1 > 0 Then
                nMultiChuKu = UBound(LeftElseTrain) - UBound(LeftBaseTrain) - 1
            Else
                nMultiChuKu = 0
            End If

            ReDim tmpShuZhu(0)
            For i = UBound(RightElseTrain) To 1 Step -1
                nTrain = RightElseTrain(i)

                If TrainInf(nTrain).TrainReturnStyle(1) = "վ���۷�" Then '******************* վ���۷� *********************

                    tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                    tmpTime = AddLitterTime(TrainInf(nTrain).lAllStartTime) - tmpZFTime
                    'If nTrain = 90 Then Stop
                    nCurLeftArriTrain = SeekAllCheDiBeforeTimeJGTrainNotGou(AddLitterTime(TrainInf(nTrain).lAllStartTime), sElseBaseSta, 2, nTrain)
                    If nCurLeftArriTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime), AddLitterTime(TrainInf(nTrain).lAllStartTime), sElseBaseSta, 1, nCurLeftArriTrain, nTrain)
                        If UBound(tmpShuZhu) >= 2 Then
                            nLeftTrain = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime), sElseBaseSta, 2, nCurLeftArriTrain)
                            nLeftTrain2 = nCurLeftArriTrain 'SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sElseBaseSta, 2, nLeftTrain)
                            nLeftStartTrain = tmpShuZhu(UBound(tmpShuZhu) - 1)
                            tmpTime4 = AddLitterTime(TrainInf(nLeftStartTrain).lAllStartTime)
                            'tmpTime4 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + StationInf(TrainInf(nTrain).nPathID(1)).IKK(1)
                            tmpTime5 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + (AddLitterTime(TrainInf(nLeftTrain).lAllEndTime) - AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime)) / 2
                            If tmpTime5 <= tmpTime Then
                                tmpTime = tmpTime5
                            Else
                                If tmpTime4 > tmpTime Then
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(tmpTime4 - tmpTime) '��ʾ���ֽ�·
                                    tmpTime = tmpTime4
                                End If
                            End If
                            nTmpState = 0
                        Else
                            If tmpTime >= TrainInf(nCurLeftArriTrain).lAllEndTime Then
                                TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                                TrainInf(nCurLeftArriTrain).nRightState = 0
                                nTmpState = 1
                            Else
                                TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                                TrainInf(nCurLeftArriTrain).nRightState = 0
                                nTmpState = 1
                                If MoveOneTrainLineSatReturn(nCurLeftArriTrain, FirQuDuanID, TrainInf(nCurLeftArriTrain).lAllEndTime - tmpTime, 2) = 1 Then
                                    '�ܵ�������
                                Else
                                    TrainInf(nTrain).nZfLimit = 1 'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(TrainInf(nTrain).lAllStartTime - TrainInf(nCurLeftArriTrain).lAllEndTime)  '��ʾ���ֽ�·
                                End If
                            End If
                        End If
                    Else
                        nLeftTrain = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nTrain).lAllStartTime), sElseBaseSta, 2, nTrain)
                        nLeftTrain2 = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sElseBaseSta, 2, nLeftTrain)
                        tmpTime5 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + (AddLitterTime(TrainInf(nLeftTrain).lAllEndTime) - AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime)) / 2
                        If tmpTime5 < tmpTime Then
                            tmpTime = tmpTime5
                        End If
                        nTmpState = 0
                    End If


                    If nTmpState = 0 Or nTmpState = 2 Then
                        If TimeTablePara.bIFAutoAddChuKuTrain = True Then
                            sRunJiaoLu = GetRunJiaoLuName(sElseBaseSta, "����")
                            nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 2)

                            'nElseTrain = AllCheDiAddNewTrain("���⳵", sElseBaseSta, "", tmpTime, 2, sJLstyle)

                            If (nElseTrain + nTrain) Mod 2 = 0 Then '��ʾ���⳵�뵱ǰ����ͬ�򳵣���ʱ���Ϊ�����۷�ʱ��
                                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, "�����۷�")
                                tmpRunTime = CalTrainRunTimeFromTrain(TrainInf(nElseTrain).sJiaoLuName, TrainInf(nElseTrain).sRunScaleName, TrainInf(nElseTrain).sStopSclaeName)
                                TrainInf(nElseTrain).lAllEndTime = TimeMinus(TrainInf(nTrain).lAllStartTime, tmpZFTime)
                                TrainInf(nElseTrain).lAllStartTime = TimeMinus(TrainInf(nElseTrain).lAllEndTime, tmpRunTime)
                                Call DrawSingleTrain(nElseTrain, TrainInf(nElseTrain).lAllStartTime, 0)
                            End If

                            If nElseTrain > 0 Then
                                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                                'Call SetTmpAllTrainSeq

                                TrainInf(nTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                TrainInf(nElseTrain).nLeftState = 0
                                TrainInf(nElseTrain).nRightState = 0
                                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                                nTmpNum = nTmpNum + 1
                            End If
                        End If
                    End If

                Else '******************* վǰ�۷� *********************

                    tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                    tmpTime = AddLitterTime(TrainInf(nTrain).lAllStartTime) - tmpZFTime
                    'If nTrain = 90 Then Stop
                    nCurLeftArriTrain = SeekAllCheDiBeforeTimeJGTrainNotGou(AddLitterTime(TrainInf(nTrain).lAllStartTime), sElseBaseSta, 2, nTrain)
                    If nCurLeftArriTrain <> 0 Then

                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime + tmpZFTime), AddLitterTime(TrainInf(nTrain).lAllStartTime), sElseBaseSta, 1, nCurLeftArriTrain, nTrain)
                        If UBound(tmpShuZhu) >= 1 Then
                            nTmpState = 0
                        Else
                            TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                            TrainInf(nCurLeftArriTrain).nRightState = 0
                            nTmpState = 1

                            TrainInf(nCurLeftArriTrain).lAllStartTime = TimeAdd(TrainInf(nCurLeftArriTrain).lAllStartTime, tmpTime - TrainInf(nCurLeftArriTrain).lAllEndTime)
                            Call DrawSingleTrain(nCurLeftArriTrain, TrainInf(nCurLeftArriTrain).lAllStartTime, 0)
                        End If
                    Else
                        nTmpState = 0
                    End If


                    If nTmpState = 0 Or nTmpState = 2 Then
                        If TimeTablePara.bIFAutoAddChuKuTrain = True Then
                            sRunJiaoLu = GetRunJiaoLuName(sElseBaseSta, "����")
                            nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 2)

                            ' nElseTrain = AllCheDiAddNewTrain("���⳵", sElseBaseSta, "", tmpTime, 2, sJLstyle)

                            If (nElseTrain + nTrain) Mod 2 = 0 Then '��ʾ���⳵�뵱ǰ����ͬ�򳵣���ʱ���Ϊ�����۷�ʱ��
                                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, "�����۷�")
                                tmpRunTime = CalTrainRunTimeFromTrain(TrainInf(nElseTrain).sJiaoLuName, TrainInf(nElseTrain).sRunScaleName, TrainInf(nElseTrain).sStopSclaeName)
                                TrainInf(nElseTrain).lAllEndTime = TimeMinus(TrainInf(nTrain).lAllStartTime, tmpZFTime)
                                TrainInf(nElseTrain).lAllStartTime = TimeMinus(TrainInf(nElseTrain).lAllEndTime, tmpRunTime)
                                Call DrawSingleTrain(nElseTrain, TrainInf(nElseTrain).lAllStartTime, 0)
                            End If

                            If nElseTrain > 0 Then
                                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                                'Call SetTmpAllTrainSeq

                                TrainInf(nTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                TrainInf(nElseTrain).nLeftState = 0
                                TrainInf(nElseTrain).nRightState = 0
                                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                                nTmpNum = nTmpNum + 1
                            End If
                        End If
                    End If
                End If
            Next i
            '        End If

        Else 'û�г���

            If UBound(LeftBaseTrain) > 1 Then
                tmpTrain = LeftElseTrain(UBound(LeftElseTrain))
                tmpTrain1 = RightBaseTrain(1)
                nJunTime = (TrainInf(tmpTrain1).lAllStartTime - TrainInf(tmpTrain).lAllStartTime) / (UBound(LeftBaseTrain) + 1)
                ntmpTimeLeft = TrainInf(tmpTrain).lAllStartTime
            End If
            ''            '����ǰ��ĳ�����С�۷�ʱ������
            For i = 1 To UBound(LeftBaseTrain)
                nTrain = LeftBaseTrain(i)

                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TimeAdd(TrainInf(nTrain).lAllEndTime, tmpZFTime))
                tmpTime1 = ntmpTimeLeft + i * nJunTime
                If tmpTime1 > tmpTime Then
                    tmpTime = tmpTime1
                End If

                'sRunJiaoLu = GetRunJiaoLuName(sFirstBaseSta, "����")
                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sFirstBaseSta, "", tmpTime, 1, sJLstyle)
                If nElseTrain <> 0 Then
                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain

                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 1
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)

                    ReDim Preserve tmpBaseTrain(UBound(tmpBaseTrain) + 1)
                    tmpBaseTrain(UBound(tmpBaseTrain)) = nElseTrain
                    'TrainInf(nElseTrain).nIfCanMove = 0
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i
            'Call SetTmpAllTrainSeq

            If UBound(LeftElseTrain) > 1 Then
                tmpTrain = LeftBaseTrain(UBound(LeftBaseTrain))
                tmpTrain1 = RightElseTrain(1)
                nJunTime = (TrainInf(tmpTrain1).lAllStartTime - TrainInf(tmpTrain).lAllStartTime) / (UBound(LeftElseTrain) + 1)
                ntmpTimeLeft = TrainInf(tmpTrain).lAllStartTime
            End If

            For i = 1 To UBound(LeftElseTrain)
                nTrain = LeftElseTrain(i)

                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TimeAdd(TrainInf(nTrain).lAllEndTime, tmpZFTime))

                tmpTime1 = ntmpTimeLeft + i * nJunTime
                If tmpTime1 > tmpTime Then
                    tmpTime = tmpTime1
                End If

                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sElseBaseSta, "", tmpTime, 1, sJLstyle)
                If nElseTrain <> 0 Then

                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain


                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 1
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)

                    ReDim Preserve tmpElseTrain(UBound(tmpElseTrain) + 1)
                    tmpElseTrain(UBound(tmpElseTrain)) = nElseTrain
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i
            'Call SetTmpAllTrainSeq

            '���г���
            nTmpMulti = 0
            nTmpState = 0 'nBaseState
            nTmpNum = 0

            ReDim tmpShuZhu(0)
            For i = UBound(RightBaseTrain) To 1 Step -1
                nTrain = RightBaseTrain(i)
                'If nTrain = 209 Then Stop
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllStartTime) - tmpZFTime

                nCurLeftArriTrain = SeekAllCheDiBeforeTimeJGTrainNotGou(AddLitterTime(TrainInf(nTrain).lAllStartTime), sFirstBaseSta, 2, nTrain)
                If TrainInf(nTrain).TrainReturnStyle(1) = "վ���۷�" Then
                    If nCurLeftArriTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime), AddLitterTime(TrainInf(nTrain).lAllStartTime), sFirstBaseSta, 1, nCurLeftArriTrain, nTrain)
                        If UBound(tmpShuZhu) >= 2 Then
                            nLeftTrain = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime), sFirstBaseSta, 2, nCurLeftArriTrain)
                            nLeftTrain2 = nCurLeftArriTrain ' SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sFirstBaseSta, 2, nLeftTrain)
                            nLeftStartTrain = tmpShuZhu(UBound(tmpShuZhu) - 1)
                            tmpTime4 = AddLitterTime(TrainInf(nLeftStartTrain).lAllStartTime)
                            'tmpTime4 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + StationInf(TrainInf(nTrain).nPathID(1)).IKK(1)
                            tmpTime5 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + (AddLitterTime(TrainInf(nLeftTrain).lAllEndTime) - AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime)) / 2
                            If tmpTime5 <= tmpTime Then
                                tmpTime = tmpTime5
                            Else
                                If tmpTime4 > tmpTime Then
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(tmpTime4 - tmpTime) '��ʾ���ֽ�·
                                    tmpTime = tmpTime4
                                End If
                            End If
                            nTmpState = 0
                        Else
                            If tmpTime >= TrainInf(nCurLeftArriTrain).lAllEndTime Then
                                TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                                TrainInf(nCurLeftArriTrain).nRightState = 0
                                nTmpState = 1
                            Else
                                TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                                TrainInf(nCurLeftArriTrain).nRightState = 0
                                nTmpState = 1
                                If MoveOneTrainLineSatReturn(nCurLeftArriTrain, FirQuDuanID, TrainInf(nCurLeftArriTrain).lAllEndTime - tmpTime, 2) = 1 Then
                                    '�ܵ�������
                                Else
                                    TrainInf(nTrain).nZfLimit = 1
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(TrainInf(nTrain).lAllStartTime - TrainInf(nCurLeftArriTrain).lAllEndTime)  '��ʾ���ֽ�·
                                End If
                            End If
                        End If
                    Else

                        If nCurLeftArriTrain = 0 Then
                            nLeftTrain = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nTrain).lAllStartTime), sFirstBaseSta, 2, nTrain)
                            nLeftTrain2 = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sFirstBaseSta, 2, nLeftTrain)
                            tmpTime5 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + (AddLitterTime(TrainInf(nLeftTrain).lAllEndTime) - AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime)) / 2
                            If tmpTime5 < tmpTime Then
                                tmpTime = tmpTime5
                            End If
                            nTmpState = 0
                        End If
                    End If


                    '                    If nTmpState = 0 Or nTmpState = 2 Then
                    '
                    '                        sRunJiaoLu = sFanJiaoLuStyle 'GetRunJiaoLuName(sFirstBaseSta, "����")
                    '                        nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                    '
                    '                        'nElseTrain = AllCheDiAddNewTrain("���⳵", sFirstBaseSta, "", tmpTime, 2, sJLstyle)
                    '                        If nElseTrain > 0 Then
                    '                            ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    '                            nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                    '                            'Call SetTmpAllTrainSeq
                    '
                    '                            TrainInf(nTrain).nLeftState = 0
                    '                            TrainInf(nTrain).nRightState = 0
                    '                            TrainInf(nElseTrain).nLeftState = 0
                    '                            TrainInf(nElseTrain).nRightState = 0
                    '                            Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                    '                            nTmpNum = nTmpNum + 1
                    '                        End If
                    '                    End If

                Else ''******************* վǰ�۷� *********************
                    If nCurLeftArriTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime + tmpZFTime), AddLitterTime(TrainInf(nTrain).lAllStartTime), sFirstBaseSta, 1, nCurLeftArriTrain, nTrain)
                        If UBound(tmpShuZhu) >= 1 Then
                            nTmpState = 0
                        Else
                            TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                            TrainInf(nCurLeftArriTrain).nRightState = 0
                            nTmpState = 1
                            TrainInf(nCurLeftArriTrain).lAllStartTime = TimeAdd(TrainInf(nCurLeftArriTrain).lAllStartTime, tmpTime - TrainInf(nCurLeftArriTrain).lAllEndTime)
                            Call DrawSingleTrain(nCurLeftArriTrain, TrainInf(nCurLeftArriTrain).lAllStartTime, 0)
                        End If
                    Else
                        nTmpState = 0
                    End If


                    '                    If nTmpState = 0 Or nTmpState = 2 Then
                    '
                    '                        sRunJiaoLu = sFanJiaoLuStyle 'GetRunJiaoLuName(sFirstBaseSta, "����")
                    '                        nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                    '
                    '                        'nElseTrain = AllCheDiAddNewTrain("���⳵", sFirstBaseSta, "", tmpTime, 2, sJLstyle)
                    '                        If nElseTrain > 0 Then
                    '                            ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    '                            nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                    '                            'Call SetTmpAllTrainSeq
                    '
                    '                            TrainInf(nTrain).nLeftState = 0
                    '                            TrainInf(nTrain).nRightState = 0
                    '                            TrainInf(nElseTrain).nLeftState = 0
                    '                            TrainInf(nElseTrain).nRightState = 0
                    '                            Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                    '                            nTmpNum = nTmpNum + 1
                    '                        End If
                    '                    End If
                End If
            Next i


            '��һ�������
            nTmpMulti = 0
            nTmpState = 0 'nBaseState
            nTmpNum = 0
            nMultiChuKu = 0
            '�жϳ���ʱ�Ǽ�������������һ��

            For i = UBound(RightElseTrain) To 1 Step -1
                nTrain = RightElseTrain(i)
                'If nTrain = 44 Then Stop
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllStartTime) - tmpZFTime

                nCurLeftArriTrain = SeekAllCheDiBeforeTimeJGTrainNotGou(AddLitterTime(TrainInf(nTrain).lAllStartTime), sElseBaseSta, 2, nTrain)

                If TrainInf(nTrain).TrainReturnStyle(1) = "վ���۷�" Then  '******************* վ���۷� *********************

                    If nCurLeftArriTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime), AddLitterTime(TrainInf(nTrain).lAllStartTime), sElseBaseSta, 1, nCurLeftArriTrain, nTrain)
                        If UBound(tmpShuZhu) >= 2 Then
                            nLeftTrain = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime), sElseBaseSta, 2, nCurLeftArriTrain)
                            nLeftTrain2 = nCurLeftArriTrain 'SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sElseBaseSta, 2, nLeftTrain)
                            nLeftStartTrain = tmpShuZhu(UBound(tmpShuZhu) - 1)
                            tmpTime4 = AddLitterTime(TrainInf(nLeftStartTrain).lAllStartTime)
                            'tmpTime4 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + StationInf(TrainInf(nTrain).nPathID(1)).IKK(1)
                            tmpTime5 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + (AddLitterTime(TrainInf(nLeftTrain).lAllEndTime) - AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime)) / 2
                            If tmpTime5 <= tmpTime Then
                                tmpTime = tmpTime5
                            Else
                                If tmpTime4 > tmpTime Then
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(tmpTime4 - tmpTime) '��ʾ���ֽ�·
                                    tmpTime = tmpTime4
                                End If
                            End If
                            nTmpState = 0
                        Else
                            If tmpTime >= TrainInf(nCurLeftArriTrain).lAllEndTime Then
                                TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                                TrainInf(nCurLeftArriTrain).nRightState = 0
                                nTmpState = 1
                            Else
                                TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                                TrainInf(nCurLeftArriTrain).nRightState = 0
                                nTmpState = 1
                                If MoveOneTrainLineSatReturn(nCurLeftArriTrain, FirQuDuanID, TrainInf(nCurLeftArriTrain).lAllEndTime - tmpTime, 2) = 1 Then
                                    '�ܵ�������
                                Else
                                    TrainInf(nTrain).nZfLimit = 1 'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(TrainInf(nTrain).lAllStartTime - TrainInf(nCurLeftArriTrain).lAllEndTime)  '��ʾ���ֽ�·
                                End If
                            End If
                        End If
                    Else
                        nLeftTrain = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nTrain).lAllStartTime), sElseBaseSta, 2, nTrain)
                        nLeftTrain2 = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sElseBaseSta, 2, nLeftTrain)
                        tmpTime5 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + (AddLitterTime(TrainInf(nLeftTrain).lAllEndTime) - AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime)) / 2
                        If tmpTime5 < tmpTime Then
                            tmpTime = tmpTime5
                        End If
                        nTmpState = 0
                    End If


                    '                    If nTmpState = 0 Or nTmpState = 2 Then

                    '                        sRunJiaoLu = sJiaoLuStyle 'GetRunJiaoLuName(sElseBaseSta, "����")
                    '                        nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                    '
                    '                        'nElseTrain = AllCheDiAddNewTrain("���⳵", sElseBaseSta, "", tmpTime, 2, sJLstyle)
                    '                        If nElseTrain > 0 Then
                    '
                    '                            ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    '                            nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                    '                            'Call SetTmpAllTrainSeq
                    '
                    '                            TrainInf(nTrain).nLeftState = 0
                    '                            TrainInf(nTrain).nRightState = 0
                    '                            TrainInf(nElseTrain).nLeftState = 0
                    '                            TrainInf(nElseTrain).nRightState = 0
                    '                            Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                    '                            nTmpNum = nTmpNum + 1
                    '                        End If

                    '                    End If

                Else  '******************* վǰ�۷� *********************

                    If nCurLeftArriTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nCurLeftArriTrain).lAllEndTime + tmpZFTime), AddLitterTime(TrainInf(nTrain).lAllStartTime), sElseBaseSta, 1, nCurLeftArriTrain, nTrain)
                        If UBound(tmpShuZhu) >= 1 Then
                            nTmpState = 0
                        Else
                            TrainInf(nTrain).nLinkLeft = nCurLeftArriTrain
                            TrainInf(nCurLeftArriTrain).nRightState = 0
                            nTmpState = 1
                            TrainInf(nCurLeftArriTrain).lAllStartTime = TimeAdd(TrainInf(nCurLeftArriTrain).lAllStartTime, tmpTime - TrainInf(nCurLeftArriTrain).lAllEndTime)
                            Call DrawSingleTrain(nCurLeftArriTrain, TrainInf(nCurLeftArriTrain).lAllStartTime, 0)
                        End If
                    Else
                        nTmpState = 0
                    End If


                    '                    If nTmpState = 0 Or nTmpState = 2 Then
                    '
                    '                        sRunJiaoLu = sJiaoLuStyle ' GetRunJiaoLuName(sElseBaseSta, "����")
                    '                        nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                    '
                    '                        'nElseTrain = AllCheDiAddNewTrain("���⳵", sElseBaseSta, "", tmpTime, 2, sJLstyle)
                    '                        If nElseTrain > 0 Then
                    '
                    '                            ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    '                            nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                    '                            'Call SetTmpAllTrainSeq
                    '
                    '                            TrainInf(nTrain).nLeftState = 0
                    '                            TrainInf(nTrain).nRightState = 0
                    '                            TrainInf(nElseTrain).nLeftState = 0
                    '                            TrainInf(nElseTrain).nRightState = 0
                    '                            Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                    '                            nTmpNum = nTmpNum + 1
                    '                        End If
                    '
                    '                    End If
                End If

            Next i

            'MsgBox "��������վû���ҵ����⳵��"
            'Exit Sub
        End If
    End Sub

    '��⣬�߷����
    Private Sub CallDrawRuKuLine(ByVal sFirstBaseSta As String, ByVal sElseBaseSta As String, ByVal FirQuDuanID As Integer, ByVal SecondQuDuanID As Integer, ByVal sJLstyle As String, ByVal nId As Integer)
        Dim i As Integer
        Dim j As Integer
        Dim nTrain As Integer
        Dim tmpTime As Long
        Dim nTmpNum As Integer '
        Dim nTmpState As Integer
        Dim nElseTrain As Integer
        Dim CurRightTrain As Integer
        Dim nRightTrain As Integer
        Dim nRightTrain2 As Integer
        Dim tmpTime4 As Long
        Dim tmpTime5 As Long
        Dim nRightStartTrain As Long
        Dim nTmpMulti As Integer
        Dim PuHuaSeq2() As Integer
        ReDim PuHuaSeq2(0)
        Dim DownRuKuNum As Integer
        Dim UpRuKuNum As Integer

        ReDim LeftBaseTrain(0)
        ReDim LeftElseTrain(0)
        ReDim RightBaseTrain(0)
        ReDim RightElseTrain(0)

        Dim tmpBaseTrain() As Integer
        Dim tmpElseTrain() As Integer
        ReDim tmpBaseTrain(0)
        ReDim tmpElseTrain(0)

        Dim sRunScaleName As String
        Dim sStopScaleName As String
        Dim sRunJiaoLu As String
        Dim tmpRunTime As Long

        sRunScaleName = GaoFenTimeSet(nId).sRunScaleName(SecondQuDuanID)
        sStopScaleName = GaoFenTimeSet(nId).sStopScaleName(SecondQuDuanID)

        Dim tmpTrain As Integer
        Dim tmpTrain1 As Integer

        Dim nJunTime As Long
        Dim ntmpTimeLeft As Long
        Dim tmpZFTime As Long
        Dim tmpTime1 As Long

        '��ֵ�������ĸ���ֵ
        For i = 1 To UBound(CheDiTrainSeq)
            If i = FirQuDuanID Then
                ReDim LeftBaseTrain(UBound(CheDiTrainSeq(i).nAfterBaseTrainSeq))
                ReDim LeftElseTrain(UBound(CheDiTrainSeq(i).nAfterElseTrainSeq))

                For j = 1 To UBound(CheDiTrainSeq(i).nAfterBaseTrainSeq)
                    LeftBaseTrain(j) = CheDiTrainSeq(i).nAfterBaseTrainSeq(j)
                Next j
                For j = 1 To UBound(CheDiTrainSeq(i).nAfterElseTrainSeq)
                    LeftElseTrain(j) = CheDiTrainSeq(i).nAfterElseTrainSeq(j)
                Next j
                Exit For
            End If
        Next i

        For i = 1 To UBound(CheDiTrainSeq)
            If i = SecondQuDuanID Then
                ReDim RightBaseTrain(UBound(CheDiTrainSeq(i).nBeforeBaseTrainSeq))
                ReDim RightElseTrain(UBound(CheDiTrainSeq(i).nBeforeElseTrainSeq))

                For j = 1 To UBound(CheDiTrainSeq(i).nBeforeBaseTrainSeq)
                    RightBaseTrain(j) = CheDiTrainSeq(i).nBeforeBaseTrainSeq(j)
                Next j
                For j = 1 To UBound(CheDiTrainSeq(i).nBeforeElseTrainSeq)
                    RightElseTrain(j) = CheDiTrainSeq(i).nBeforeElseTrainSeq(j)
                Next j
                Exit For
            End If
        Next i

        UpRuKuNum = UBound(LeftElseTrain) - UBound(RightBaseTrain) '+ 1
        DownRuKuNum = UBound(LeftBaseTrain) - UBound(RightElseTrain) '+ 1



        '##############################################################  ����վ�������
        If nIfBaseRuKu = 1 And nIfElseRuKu = 1 Then
            '���ú���ĳ�����С�۷�ʱ������

            If UBound(RightBaseTrain) > 1 Then
                tmpTrain = RightBaseTrain(1)
                tmpTrain1 = LeftBaseTrain(UBound(LeftBaseTrain))
                nJunTime = (TrainInf(tmpTrain).lAllStartTime - TrainInf(tmpTrain1).lAllStartTime) / (UBound(RightBaseTrain) + 1)
                ntmpTimeLeft = TrainInf(tmpTrain1).lAllEndTime
            End If

            For i = 1 To UBound(RightBaseTrain)
                nTrain = RightBaseTrain(i)

                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TimeMinus(TrainInf(nTrain).lAllStartTime, tmpZFTime))

                If TrainInf(nTrain).TrainReturnStyle(2) = "վ���۷�" Then
                    tmpTime1 = ntmpTimeLeft + i * nJunTime
                    If tmpTime1 < tmpTime Then
                        tmpTime = tmpTime1
                    End If
                End If

                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sElseBaseSta, "", tmpTime, 2, sJLstyle)
                If nElseTrain <> 0 Then
                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain

                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 1
                    TrainInf(nElseTrain).nRightState = 0
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                    ReDim Preserve tmpBaseTrain(UBound(tmpBaseTrain) + 1)
                    tmpBaseTrain(UBound(tmpBaseTrain)) = nElseTrain
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i
            'Call SetTmpAllTrainSeq


            If UBound(RightElseTrain) > 1 Then
                tmpTrain = RightBaseTrain(1)
                tmpTrain1 = LeftElseTrain(UBound(LeftElseTrain))
                nJunTime = (TrainInf(tmpTrain).lAllStartTime - TrainInf(tmpTrain1).lAllStartTime) / (UBound(RightElseTrain) + 1)
                ntmpTimeLeft = TrainInf(tmpTrain1).lAllEndTime
            End If

            For i = 1 To UBound(RightElseTrain)
                nTrain = RightElseTrain(i)
                TrainInf(nTrain).nIfCanMove = 0
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TimeMinus(TrainInf(nTrain).lAllStartTime, tmpZFTime))

                If TrainInf(nTrain).TrainReturnStyle(2) = "վ���۷�" Then
                    tmpTime1 = ntmpTimeLeft + i * nJunTime
                    If tmpTime1 < tmpTime Then
                        tmpTime = tmpTime1
                    End If
                End If

                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sFirstBaseSta, "", tmpTime, 2, sJLstyle)
                If nElseTrain <> 0 Then

                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain


                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 1
                    TrainInf(nElseTrain).nRightState = 0
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)

                    ReDim Preserve tmpElseTrain(UBound(tmpElseTrain) + 1)
                    tmpElseTrain(UBound(tmpElseTrain)) = nElseTrain
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i

            '�������
            nTmpMulti = 0
            nTmpState = 0 'nBaseState
            nTmpNum = 0

            ReDim tmpShuZhu(0)
            For i = 1 To UBound(LeftBaseTrain)
                nTrain = LeftBaseTrain(i)
                'If nTrain = 100 Then Stop
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllEndTime) + tmpZFTime

                CurRightTrain = SeekAllCheDiAfterTimeJGTrainNotGou(AddLitterTime(TrainInf(nTrain).lAllEndTime), sFirstBaseSta, 1, nTrain)
                If TrainInf(nTrain).TrainReturnStyle(2) = "վ���۷�" Then '****************************** վ���۷� *********************
                    If CurRightTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nTrain).lAllEndTime), AddLitterTime(TrainInf(CurRightTrain).lAllStartTime), sFirstBaseSta, 2, nTrain, CurRightTrain)
                        If UBound(tmpShuZhu) >= 2 Then
                            nRightTrain = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(CurRightTrain).lAllStartTime), sFirstBaseSta, 1, CurRightTrain)
                            nRightTrain2 = CurRightTrain ' SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sFirstBaseSta, 1, nLeftTrain)
                            nRightStartTrain = tmpShuZhu(2)
                            tmpTime4 = AddLitterTime(TrainInf(nRightStartTrain).lAllEndTime)
                            'tmpTime4 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + StationInf(TrainInf(nTrain).nPathID(1)).IKK(1)
                            tmpTime5 = AddLitterTime(TrainInf(nRightTrain2).lAllStartTime) + (AddLitterTime(TrainInf(nRightTrain).lAllStartTime) - AddLitterTime(TrainInf(nRightTrain2).lAllStartTime)) / 2
                            If tmpTime5 >= tmpTime Then
                                tmpTime = tmpTime5
                            Else
                                If tmpTime4 < tmpTime Then
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(tmpTime4 - tmpTime) '��ʾ���ֽ�·
                                    tmpTime = tmpTime4
                                End If
                            End If
                            nTmpState = 0
                        Else
                            If tmpTime <= TrainInf(CurRightTrain).lAllStartTime Then
                                TrainInf(CurRightTrain).nLinkLeft = nTrain
                                TrainInf(CurRightTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                nTmpState = 1
                            Else
                                TrainInf(CurRightTrain).nLinkLeft = nTrain
                                TrainInf(CurRightTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                nTmpState = 1
                                If MoveOneTrainLineSatReturn(CurRightTrain, SecondQuDuanID, tmpTime - TrainInf(CurRightTrain).lAllStartTime, 1) = 1 Then
                                    '�ܵ�������
                                Else
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(TrainInf(nTrain).lAllStartTime - TrainInf(nCurLeftArriTrain).lAllEndTime)  '��ʾ���ֽ�·
                                End If
                            End If
                        End If
                    Else
                        nRightTrain = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nTrain).lAllEndTime), sFirstBaseSta, 1, nTrain)
                        nRightTrain2 = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nRightTrain).lAllStartTime), sFirstBaseSta, 1, nRightTrain)
                        tmpTime5 = AddLitterTime(TrainInf(nRightTrain).lAllStartTime) + (AddLitterTime(TrainInf(nRightTrain2).lAllStartTime) - AddLitterTime(TrainInf(nRightTrain).lAllStartTime)) / 2
                        If tmpTime5 > tmpTime Then
                            tmpTime = tmpTime5
                        End If
                        nTmpState = 0
                    End If

                    '���
                    If nTmpState = 0 Or nTmpState = 2 Then
                        If TimeTablePara.bIFAutoAddRuKuTrain = True Then
                            sRunJiaoLu = GetRunJiaoLuName(sFirstBaseSta, "���")

                            nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 1)

                            'nElseTrain = AllCheDiAddNewTrain("��⳵", sFirstBaseSta, "", tmpTime, 1, sJLstyle)
                            If nElseTrain > 0 Then

                                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                                'Call SetTmpAllTrainSeq

                                TrainInf(nTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                TrainInf(nElseTrain).nLeftState = 0
                                TrainInf(nElseTrain).nRightState = 1
                                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)
                                nTmpNum = nTmpNum + 1
                            End If
                        End If
                    End If

                Else '****************************** վǰ�۷� *********************

                    If CurRightTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nTrain).lAllEndTime), AddLitterTime(TrainInf(CurRightTrain).lAllStartTime - tmpZFTime), sFirstBaseSta, 2, nTrain, CurRightTrain)
                        If UBound(tmpShuZhu) >= 1 Then
                            nTmpState = 0
                        Else
                            TrainInf(CurRightTrain).nLinkLeft = nTrain
                            TrainInf(CurRightTrain).nLeftState = 0
                            TrainInf(nTrain).nRightState = 0
                            nTmpState = 1
                            TrainInf(CurRightTrain).lAllStartTime = TimeMinus(TrainInf(CurRightTrain).lAllStartTime, TrainInf(CurRightTrain).lAllStartTime - tmpTime)
                            Call DrawSingleTrain(CurRightTrain, TrainInf(CurRightTrain).lAllStartTime, 0)
                        End If
                    Else
                        nTmpState = 0
                    End If

                    '���
                    If nTmpState = 0 Or nTmpState = 2 Then
                        If TimeTablePara.bIFAutoAddRuKuTrain = True Then

                            sRunJiaoLu = GetRunJiaoLuName(sFirstBaseSta, "���")

                            nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 1)

                            'nElseTrain = AllCheDiAddNewTrain("��⳵", sFirstBaseSta, "", tmpTime, 1, sJLstyle)
                            If nElseTrain > 0 Then

                                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                                'Call SetTmpAllTrainSeq

                                TrainInf(nTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                TrainInf(nElseTrain).nLeftState = 0
                                TrainInf(nElseTrain).nRightState = 1
                                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)
                                nTmpNum = nTmpNum + 1
                            End If
                        End If
                    End If
                End If

            Next i


            '            '**************** �������
            nTmpMulti = 0
            nTmpNum = 0
            nTmpState = 0

            For i = 1 To UBound(LeftElseTrain)
                nTrain = LeftElseTrain(i)
                'If nTrain = 103 Then Stop
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllEndTime) + tmpZFTime

                CurRightTrain = SeekAllCheDiAfterTimeJGTrainNotGou(AddLitterTime(TrainInf(nTrain).lAllEndTime), sElseBaseSta, 1, nTrain)

                If TrainInf(nTrain).TrainReturnStyle(2) = "վ���۷�" Then '****************************** վ���۷� *********************
                    If CurRightTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nTrain).lAllEndTime), AddLitterTime(TrainInf(CurRightTrain).lAllStartTime), sElseBaseSta, 2, nTrain, CurRightTrain)
                        If UBound(tmpShuZhu) >= 2 Then
                            nRightTrain = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(CurRightTrain).lAllStartTime), sElseBaseSta, 1, CurRightTrain)
                            nRightTrain2 = CurRightTrain ' SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sElseBaseSta, 1, nLeftTrain)
                            nRightStartTrain = tmpShuZhu(2)
                            tmpTime4 = AddLitterTime(TrainInf(nRightStartTrain).lAllEndTime)
                            'tmpTime4 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + StationInf(TrainInf(nTrain).nPathID(1)).IKK(1)
                            tmpTime5 = AddLitterTime(TrainInf(nRightTrain2).lAllStartTime) + (AddLitterTime(TrainInf(nRightTrain).lAllStartTime) - AddLitterTime(TrainInf(nRightTrain2).lAllStartTime)) / 2
                            If tmpTime5 >= tmpTime Then
                                tmpTime = tmpTime5
                            Else
                                If tmpTime4 < tmpTime Then
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(tmpTime4 - tmpTime) '��ʾ���ֽ�·
                                    tmpTime = tmpTime4
                                End If
                            End If
                            nTmpState = 0
                        Else
                            If tmpTime <= TrainInf(CurRightTrain).lAllStartTime Then
                                TrainInf(CurRightTrain).nLinkLeft = nTrain
                                TrainInf(CurRightTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                nTmpState = 1
                            Else
                                TrainInf(CurRightTrain).nLinkLeft = nTrain
                                TrainInf(CurRightTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                nTmpState = 1
                                If MoveOneTrainLineSatReturn(CurRightTrain, SecondQuDuanID, tmpTime - TrainInf(CurRightTrain).lAllStartTime, 1) = 1 Then
                                    '�ܵ�������
                                Else
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(TrainInf(nTrain).lAllStartTime - TrainInf(nCurLeftArriTrain).lAllEndTime)  '��ʾ���ֽ�·
                                End If
                            End If
                        End If
                    Else
                        nRightTrain = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nTrain).lAllEndTime), sElseBaseSta, 1, nTrain)
                        nRightTrain2 = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nRightTrain).lAllStartTime), sElseBaseSta, 1, nRightTrain)
                        tmpTime5 = AddLitterTime(TrainInf(nRightTrain).lAllStartTime) + (AddLitterTime(TrainInf(nRightTrain2).lAllStartTime) - AddLitterTime(TrainInf(nRightTrain).lAllStartTime)) / 2
                        If tmpTime5 > tmpTime Then
                            tmpTime = tmpTime5
                        End If
                        nTmpState = 0
                    End If


                    If nTmpState = 0 Or nTmpState = 2 Then
                        If TimeTablePara.bIFAutoAddRuKuTrain = True Then

                            sRunJiaoLu = GetRunJiaoLuName(sElseBaseSta, "���")

                            nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 1)

                            'nElseTrain = AllCheDiAddNewTrain("��⳵", sElseBaseSta, "", tmpTime, 1, sJLstyle)
                            If nElseTrain > 0 Then
                                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                                'Call SetTmpAllTrainSeq

                                TrainInf(nTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                TrainInf(nElseTrain).nLeftState = 0
                                TrainInf(nElseTrain).nRightState = 1
                                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)
                                nTmpNum = nTmpNum + 1
                            End If
                        End If
                    End If

                Else '****************************** վǰ�۷� *********************
                    '                    If nTrain = 389 Then Stop
                    If CurRightTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nTrain).lAllEndTime), AddLitterTime(TrainInf(CurRightTrain).lAllStartTime - tmpZFTime), sElseBaseSta, 2, nTrain, CurRightTrain)
                        If UBound(tmpShuZhu) >= 1 Then
                            nTmpState = 0
                        Else
                            TrainInf(CurRightTrain).nLinkLeft = nTrain
                            TrainInf(CurRightTrain).nLeftState = 0
                            TrainInf(nTrain).nRightState = 0
                            nTmpState = 1
                            TrainInf(CurRightTrain).lAllStartTime = TimeMinus(TrainInf(CurRightTrain).lAllStartTime, TrainInf(CurRightTrain).lAllStartTime - tmpTime)
                            Call DrawSingleTrain(CurRightTrain, TrainInf(CurRightTrain).lAllStartTime, 0)
                        End If
                    Else
                        nTmpState = 0
                    End If


                    If nTmpState = 0 Or nTmpState = 2 Then
                        If TimeTablePara.bIFAutoAddRuKuTrain = True Then

                            sRunJiaoLu = GetRunJiaoLuName(sElseBaseSta, "���")

                            nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 1)

                            'nElseTrain = AllCheDiAddNewTrain("��⳵", sElseBaseSta, "", tmpTime, 1, sJLstyle)
                            If nElseTrain > 0 Then
                                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                                'Call SetTmpAllTrainSeq

                                TrainInf(nTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                TrainInf(nElseTrain).nLeftState = 0
                                TrainInf(nElseTrain).nRightState = 1
                                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)
                                nTmpNum = nTmpNum + 1
                            End If
                        End If
                    End If

                End If

            Next i



            '##############################################################  ֻ���ڻ�׼վ���
        ElseIf nIfBaseRuKu = 1 And nIfElseRuKu = 0 Then

            '���ú���ĳ�����С�۷�ʱ������


            '�������

            If UBound(LeftElseTrain) > 1 Then
                tmpTrain = RightElseTrain(1)
                tmpTrain1 = LeftBaseTrain(UBound(LeftBaseTrain))
                nJunTime = (TrainInf(tmpTrain).lAllStartTime - TrainInf(tmpTrain1).lAllStartTime) / (UBound(LeftElseTrain) + 1)
                ntmpTimeLeft = TrainInf(tmpTrain1).lAllStartTime
            End If


            For i = 1 To UBound(LeftElseTrain)
                nTrain = LeftElseTrain(i)

                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllEndTime) + tmpZFTime

                'If TrainInf(nTrain).TrainReturnStyle(2) = "վ���۷�" Then
                '    tmpTime1 = ntmpTimeLeft + i * nJunTime
                '    If tmpTime1 > tmpTime Then
                '        tmpTime = tmpTime1
                '    End If
                'End If

                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sElseBaseSta, "", tmpTime, 1, sJLstyle)

                If nElseTrain > 0 Then
                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain

                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 1
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)

                    ReDim Preserve LeftBaseTrain(UBound(LeftBaseTrain) + 1)
                    LeftBaseTrain(UBound(LeftBaseTrain)) = nElseTrain
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i


            Call SetLeftBaseTrainSeq()


            If UBound(RightElseTrain) > 1 Then
                tmpTrain = RightBaseTrain(1)
                tmpTrain1 = LeftElseTrain(UBound(LeftElseTrain))
                nJunTime = (TrainInf(tmpTrain).lAllStartTime - TrainInf(tmpTrain1).lAllStartTime) / (UBound(RightElseTrain) + 1)
                ntmpTimeLeft = TrainInf(tmpTrain1).lAllEndTime
            End If

            For i = 1 To UBound(RightElseTrain)
                nTrain = RightElseTrain(i)
                TrainInf(nTrain).nIfCanMove = 0
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TimeMinus(TrainInf(nTrain).lAllStartTime, tmpZFTime))

                'If TrainInf(nTrain).TrainReturnStyle(2) = "վ���۷�" Then
                '    tmpTime1 = ntmpTimeLeft + i * nJunTime
                '    If tmpTime1 < tmpTime Then
                '        tmpTime = tmpTime1
                '    End If
                'End If

                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sFirstBaseSta, "", tmpTime, 2, sJLstyle)
                If nElseTrain <> 0 Then

                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain


                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 1
                    TrainInf(nElseTrain).nRightState = 0
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)

                    ReDim Preserve tmpElseTrain(UBound(tmpElseTrain) + 1)
                    tmpElseTrain(UBound(tmpElseTrain)) = nElseTrain
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i


            '�������
            nTmpMulti = 0
            nTmpState = 0 'nBaseState
            nTmpNum = 0


            ReDim tmpShuZhu(0)
            For i = 1 To UBound(LeftBaseTrain)
                nTrain = LeftBaseTrain(i)
                'If nTrain = 100 Then Stop
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllEndTime) + tmpZFTime

                CurRightTrain = SeekAllCheDiAfterTimeJGTrainNotGou(AddLitterTime(TrainInf(nTrain).lAllEndTime), sFirstBaseSta, 1, nTrain)

                If TrainInf(nTrain).TrainReturnStyle(2) = "վ���۷�" Then '****************************** վ���۷� *********************

                    If CurRightTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nTrain).lAllEndTime), AddLitterTime(TrainInf(CurRightTrain).lAllStartTime), sFirstBaseSta, 2, nTrain, CurRightTrain)
                        If UBound(tmpShuZhu) >= 2 Then
                            nRightTrain = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(CurRightTrain).lAllStartTime), sFirstBaseSta, 1, CurRightTrain)
                            nRightTrain2 = CurRightTrain
                            nRightStartTrain = tmpShuZhu(2)
                            tmpTime4 = AddLitterTime(TrainInf(nRightStartTrain).lAllEndTime)
                            tmpTime5 = AddLitterTime(TrainInf(nRightTrain2).lAllStartTime) + (AddLitterTime(TrainInf(nRightTrain).lAllStartTime) - AddLitterTime(TrainInf(nRightTrain2).lAllStartTime)) / 2
                            If tmpTime5 >= tmpTime Then
                                tmpTime = tmpTime5
                            Else
                                If tmpTime4 < tmpTime Then
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(tmpTime4 - tmpTime) '��ʾ���ֽ�·
                                    tmpTime = tmpTime4
                                End If
                            End If
                            nTmpState = 0
                        Else
                            If tmpTime <= TrainInf(CurRightTrain).lAllStartTime Then
                                TrainInf(CurRightTrain).nLinkLeft = nTrain
                                TrainInf(CurRightTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                nTmpState = 1
                            Else
                                TrainInf(CurRightTrain).nLinkLeft = nTrain
                                TrainInf(CurRightTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                nTmpState = 1
                                If MoveOneTrainLineSatReturn(CurRightTrain, SecondQuDuanID, tmpTime - TrainInf(CurRightTrain).lAllStartTime, 1) = 1 Then
                                    '�ܵ�������
                                Else
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(TrainInf(nTrain).lAllStartTime - TrainInf(nCurLeftArriTrain).lAllEndTime)  '��ʾ���ֽ�·
                                End If
                            End If
                        End If
                    Else
                        nRightTrain = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nTrain).lAllEndTime), sFirstBaseSta, 1, nTrain)
                        nRightTrain2 = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nRightTrain).lAllStartTime), sFirstBaseSta, 1, nRightTrain)
                        tmpTime5 = AddLitterTime(TrainInf(nRightTrain).lAllStartTime) + (AddLitterTime(TrainInf(nRightTrain2).lAllStartTime) - AddLitterTime(TrainInf(nRightTrain).lAllStartTime)) / 2
                        If tmpTime5 > tmpTime Then
                            tmpTime = tmpTime5
                        End If
                        nTmpState = 0
                    End If

                    '���
                    If nTmpState = 0 Or nTmpState = 2 Then
                        If TimeTablePara.bIFAutoAddRuKuTrain = True Then

                            sRunJiaoLu = GetRunJiaoLuName(sFirstBaseSta, "���")

                            nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 1)

                            'nElseTrain = AllCheDiAddNewTrain("��⳵", sFirstBaseSta, "", tmpTime, 1, sJLstyle)
                            If nElseTrain > 0 Then

                                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                                'Call SetTmpAllTrainSeq

                                TrainInf(nTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                TrainInf(nElseTrain).nLeftState = 0
                                TrainInf(nElseTrain).nRightState = 1
                                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)
                                nTmpNum = nTmpNum + 1
                            End If
                        End If
                    End If

                Else '****************************** վǰ�۷� *********************

                    If CurRightTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nTrain).lAllEndTime), AddLitterTime(TrainInf(CurRightTrain).lAllStartTime - tmpZFTime), sFirstBaseSta, 2, nTrain, CurRightTrain)
                        If UBound(tmpShuZhu) >= 1 Then
                            nTmpState = 0
                        Else
                            TrainInf(CurRightTrain).nLinkLeft = nTrain
                            TrainInf(CurRightTrain).nLeftState = 0
                            TrainInf(nTrain).nRightState = 0
                            nTmpState = 1
                            TrainInf(CurRightTrain).lAllStartTime = TimeMinus(TrainInf(CurRightTrain).lAllStartTime, TrainInf(CurRightTrain).lAllStartTime - tmpTime)
                            Call DrawSingleTrain(CurRightTrain, TrainInf(CurRightTrain).lAllStartTime, 0)
                        End If
                    Else
                        nTmpState = 0
                    End If

                    '���
                    If nTmpState = 0 Or nTmpState = 2 Then
                        If TimeTablePara.bIFAutoAddRuKuTrain = True Then

                            sRunJiaoLu = GetRunJiaoLuName(sFirstBaseSta, "���")

                            nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 1)

                            'nElseTrain = AllCheDiAddNewTrain("��⳵", sFirstBaseSta, "", tmpTime, 1, sJLstyle)
                            If nElseTrain > 0 Then

                                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                                'Call SetTmpAllTrainSeq

                                TrainInf(nTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                TrainInf(nElseTrain).nLeftState = 0
                                TrainInf(nElseTrain).nRightState = 1
                                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)
                                nTmpNum = nTmpNum + 1
                            End If
                        End If
                    End If

                End If

            Next i





            '##############################################################  ֻ������һ����վ���
        ElseIf nIfBaseRuKu = 0 And nIfElseRuKu = 1 Then


            If UBound(LeftBaseTrain) > 1 Then
                If UBound(RightBaseTrain) > 0 Then
                    tmpTrain = RightBaseTrain(1)
                    tmpTrain1 = LeftElseTrain(UBound(LeftElseTrain))
                    nJunTime = (TrainInf(tmpTrain).lAllStartTime - TrainInf(tmpTrain1).lAllStartTime) / (UBound(LeftBaseTrain) + 1)
                    ntmpTimeLeft = TrainInf(tmpTrain1).lAllStartTime
                End If
            End If



            For i = 1 To UBound(LeftBaseTrain)
                nTrain = LeftBaseTrain(i)
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllEndTime) + tmpZFTime

                If TrainInf(nTrain).TrainReturnStyle(2) = "վ���۷�" Then
                    tmpTime1 = ntmpTimeLeft + i * nJunTime
                    If tmpTime1 > tmpTime Then
                        tmpTime = tmpTime1
                    End If
                End If

                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sFirstBaseSta, "", tmpTime, 1, sJLstyle)
                If nElseTrain > 0 Then
                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain


                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 1
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)

                    ReDim Preserve LeftElseTrain(UBound(LeftElseTrain) + 1)
                    LeftElseTrain(UBound(LeftElseTrain)) = nElseTrain
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i

            Call SetLeftElseTrainSeq()


            If UBound(RightBaseTrain) > 1 Then
                tmpTrain = RightElseTrain(1)
                tmpTrain1 = LeftBaseTrain(UBound(LeftBaseTrain))
                nJunTime = (TrainInf(tmpTrain).lAllStartTime - TrainInf(tmpTrain1).lAllStartTime) / (UBound(RightBaseTrain) + 1)
                ntmpTimeLeft = TrainInf(tmpTrain1).lAllEndTime
            End If

            For i = 1 To UBound(RightBaseTrain)
                nTrain = RightBaseTrain(i)

                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TimeMinus(TrainInf(nTrain).lAllStartTime, tmpZFTime))

                If TrainInf(nTrain).TrainReturnStyle(2) = "վ���۷�" Then
                    tmpTime1 = ntmpTimeLeft + i * nJunTime
                    If tmpTime1 < tmpTime Then
                        tmpTime = tmpTime1
                    End If
                End If

                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sElseBaseSta, "", tmpTime, 2, sJLstyle)
                If nElseTrain <> 0 Then
                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain

                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 1
                    TrainInf(nElseTrain).nRightState = 0
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                    ReDim Preserve tmpBaseTrain(UBound(tmpBaseTrain) + 1)
                    tmpBaseTrain(UBound(tmpBaseTrain)) = nElseTrain
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i


            nTmpMulti = 0
            nTmpNum = 0
            nTmpState = 0
            ReDim tmpShuZhu(0)
            For i = 1 To UBound(LeftElseTrain)
                nTrain = LeftElseTrain(i)
                'If nTrain = 103 Then Stop
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllEndTime) + tmpZFTime

                CurRightTrain = SeekAllCheDiAfterTimeJGTrainNotGou(AddLitterTime(TrainInf(nTrain).lAllEndTime), sElseBaseSta, 1, nTrain)

                If TrainInf(nTrain).TrainReturnStyle(2) = "վ���۷�" Then '****************************** վ���۷� *********************

                    If CurRightTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nTrain).lAllEndTime), AddLitterTime(TrainInf(CurRightTrain).lAllStartTime), sElseBaseSta, 2, nTrain, CurRightTrain)
                        If UBound(tmpShuZhu) >= 2 Then
                            nRightTrain = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(CurRightTrain).lAllStartTime), sElseBaseSta, 1, CurRightTrain)
                            nRightTrain2 = CurRightTrain ' SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sElseBaseSta, 1, nLeftTrain)
                            nRightStartTrain = tmpShuZhu(2)
                            tmpTime4 = AddLitterTime(TrainInf(nRightStartTrain).lAllEndTime)
                            'tmpTime4 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + StationInf(TrainInf(nTrain).nPathID(1)).IKK(1)
                            tmpTime5 = AddLitterTime(TrainInf(nRightTrain2).lAllStartTime) + (AddLitterTime(TrainInf(nRightTrain).lAllStartTime) - AddLitterTime(TrainInf(nRightTrain2).lAllStartTime)) / 2
                            If tmpTime5 >= tmpTime Then
                                tmpTime = tmpTime5
                            Else
                                If tmpTime4 < tmpTime Then
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(tmpTime4 - tmpTime) '��ʾ���ֽ�·
                                    tmpTime = tmpTime4
                                End If
                            End If
                            nTmpState = 0
                        Else
                            If tmpTime <= TrainInf(CurRightTrain).lAllStartTime Then
                                TrainInf(CurRightTrain).nLinkLeft = nTrain
                                TrainInf(CurRightTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                nTmpState = 1
                            Else
                                TrainInf(CurRightTrain).nLinkLeft = nTrain
                                TrainInf(CurRightTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                nTmpState = 1
                                If MoveOneTrainLineSatReturn(CurRightTrain, SecondQuDuanID, tmpTime - TrainInf(CurRightTrain).lAllStartTime, 1) = 1 Then
                                    '�ܵ�������
                                Else
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(TrainInf(nTrain).lAllStartTime - TrainInf(nCurLeftArriTrain).lAllEndTime)  '��ʾ���ֽ�·
                                End If
                            End If
                        End If
                    Else
                        nRightTrain = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nTrain).lAllEndTime), sElseBaseSta, 1, nTrain)
                        nRightTrain2 = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nRightTrain).lAllStartTime), sElseBaseSta, 1, nRightTrain)
                        tmpTime5 = AddLitterTime(TrainInf(nRightTrain).lAllStartTime) + (AddLitterTime(TrainInf(nRightTrain2).lAllStartTime) - AddLitterTime(TrainInf(nRightTrain).lAllStartTime)) / 2
                        If tmpTime5 > tmpTime Then
                            tmpTime = tmpTime5
                        End If
                        nTmpState = 0
                    End If


                    If nTmpState = 0 Or nTmpState = 2 Then
                        If TimeTablePara.bIFAutoAddRuKuTrain = True Then

                            sRunJiaoLu = GetRunJiaoLuName(sElseBaseSta, "���")

                            nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 1)

                            'nElseTrain = AllCheDiAddNewTrain("��⳵", sElseBaseSta, "", tmpTime, 1, sJLstyle)

                            If (nElseTrain + nTrain) Mod 2 = 0 Then '��ʾ���⳵�뵱ǰ����ͬ�򳵣���ʱ���Ϊ�����۷�ʱ��
                                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, "�����۷�")
                                tmpRunTime = CalTrainRunTimeFromTrain(TrainInf(nElseTrain).sJiaoLuName, TrainInf(nElseTrain).sRunScaleName, TrainInf(nElseTrain).sStopSclaeName)
                                TrainInf(nElseTrain).lAllStartTime = TimeAdd(TrainInf(nTrain).lAllEndTime, tmpZFTime)
                                TrainInf(nElseTrain).lAllEndTime = TimeAdd(TrainInf(nElseTrain).lAllStartTime, tmpRunTime)
                                Call DrawSingleTrain(nElseTrain, TrainInf(nElseTrain).lAllStartTime, 0)
                            End If

                            If nElseTrain > 0 Then
                                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                                'Call SetTmpAllTrainSeq

                                TrainInf(nTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                TrainInf(nElseTrain).nLeftState = 0
                                TrainInf(nElseTrain).nRightState = 1
                                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)
                                nTmpNum = nTmpNum + 1
                            End If
                        End If
                    End If

                Else '****************************** վǰ�۷� *********************

                    If CurRightTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nTrain).lAllEndTime), AddLitterTime(TrainInf(CurRightTrain).lAllStartTime - tmpZFTime), sElseBaseSta, 2, nTrain, CurRightTrain)
                        If UBound(tmpShuZhu) >= 1 Then
                            nTmpState = 0
                        Else
                            TrainInf(CurRightTrain).nLinkLeft = nTrain
                            TrainInf(CurRightTrain).nLeftState = 0
                            TrainInf(nTrain).nRightState = 0
                            nTmpState = 1

                            TrainInf(CurRightTrain).lAllStartTime = TimeMinus(TrainInf(CurRightTrain).lAllStartTime, TrainInf(CurRightTrain).lAllStartTime - tmpTime)
                            Call DrawSingleTrain(CurRightTrain, TrainInf(CurRightTrain).lAllStartTime, 0)
                        End If
                    Else
                        nTmpState = 0
                    End If


                    If nTmpState = 0 Or nTmpState = 2 Then
                        If TimeTablePara.bIFAutoAddRuKuTrain = True Then

                            sRunJiaoLu = GetRunJiaoLuName(sElseBaseSta, "���")

                            nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 1)

                            ' nElseTrain = AllCheDiAddNewTrain("��⳵", sElseBaseSta, "", tmpTime, 1, sJLstyle)

                            If (nElseTrain + nTrain) Mod 2 = 0 Then '��ʾ���⳵�뵱ǰ����ͬ�򳵣���ʱ���Ϊ�����۷�ʱ��
                                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, "�����۷�")
                                tmpRunTime = CalTrainRunTimeFromTrain(TrainInf(nElseTrain).sJiaoLuName, TrainInf(nElseTrain).sRunScaleName, TrainInf(nElseTrain).sStopSclaeName)
                                TrainInf(nElseTrain).lAllStartTime = TimeAdd(TrainInf(nTrain).lAllEndTime, tmpZFTime)
                                TrainInf(nElseTrain).lAllEndTime = TimeAdd(TrainInf(nElseTrain).lAllStartTime, tmpRunTime)
                                Call DrawSingleTrain(nElseTrain, TrainInf(nElseTrain).lAllStartTime, 0)
                            End If

                            If nElseTrain > 0 Then
                                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                                'Call SetTmpAllTrainSeq

                                TrainInf(nTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                TrainInf(nElseTrain).nLeftState = 0
                                TrainInf(nElseTrain).nRightState = 1
                                Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)
                                nTmpNum = nTmpNum + 1
                            End If
                        End If
                    End If

                End If

            Next i



            '        End If

        Else '����վ��û��⳵����ʼ��վΪ����

            '���ú���ĳ�����С�۷�ʱ������

            If UBound(RightBaseTrain) > 1 Then
                tmpTrain = RightBaseTrain(1)
                tmpTrain1 = LeftBaseTrain(UBound(LeftBaseTrain))
                nJunTime = (TrainInf(tmpTrain).lAllStartTime - TrainInf(tmpTrain1).lAllStartTime) / (UBound(RightBaseTrain) + 1)
                ntmpTimeLeft = TrainInf(tmpTrain1).lAllEndTime
            End If

            For i = 1 To UBound(RightBaseTrain)
                nTrain = RightBaseTrain(i)
                'If nTrain = 49 Then Stop
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TimeMinus(TrainInf(nTrain).lAllStartTime, tmpZFTime))

                If TrainInf(nTrain).TrainReturnStyle(2) = "վ���۷�" Then
                    tmpTime1 = ntmpTimeLeft + i * nJunTime
                    If tmpTime1 < tmpTime Then
                        tmpTime = tmpTime1
                    End If
                End If

                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sElseBaseSta, "", tmpTime, 2, sJLstyle)
                If nElseTrain <> 0 Then
                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain

                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 1
                    TrainInf(nElseTrain).nRightState = 0
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)
                    ReDim Preserve tmpBaseTrain(UBound(tmpBaseTrain) + 1)
                    tmpBaseTrain(UBound(tmpBaseTrain)) = nElseTrain
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i
            'Call SetTmpAllTrainSeq


            If UBound(RightElseTrain) > 1 Then
                tmpTrain = RightBaseTrain(1)
                tmpTrain1 = LeftElseTrain(UBound(LeftElseTrain))
                nJunTime = (TrainInf(tmpTrain).lAllStartTime - TrainInf(tmpTrain1).lAllStartTime) / (UBound(RightElseTrain) + 1)
                ntmpTimeLeft = TrainInf(tmpTrain1).lAllEndTime
            End If

            For i = 1 To UBound(RightElseTrain)
                nTrain = RightElseTrain(i)
                TrainInf(nTrain).nIfCanMove = 0
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(1))
                tmpTime = AddLitterTime(TimeMinus(TrainInf(nTrain).lAllStartTime, tmpZFTime))

                If TrainInf(nTrain).TrainReturnStyle(2) = "վ���۷�" Then
                    tmpTime1 = ntmpTimeLeft + i * nJunTime
                    If tmpTime1 < tmpTime Then
                        tmpTime = tmpTime1
                    End If
                End If

                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 2)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sFirstBaseSta, "", tmpTime, 2, sJLstyle)
                If nElseTrain <> 0 Then

                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain


                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 1
                    TrainInf(nElseTrain).nRightState = 0
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, SecondQuDuanID, 1)

                    ReDim Preserve tmpElseTrain(UBound(tmpElseTrain) + 1)
                    tmpElseTrain(UBound(tmpElseTrain)) = nElseTrain
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i

            '�������
            nTmpMulti = 0
            nTmpState = 0 'nBaseState
            nTmpNum = 0


            ReDim tmpShuZhu(0)
            For i = 1 To UBound(LeftBaseTrain)
                nTrain = LeftBaseTrain(i)
                'If nTrain = 100 Then Stop
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllEndTime) + tmpZFTime

                CurRightTrain = SeekAllCheDiAfterTimeJGTrainNotGou(AddLitterTime(TrainInf(nTrain).lAllEndTime), sFirstBaseSta, 1, nTrain)
                If TrainInf(nTrain).TrainReturnStyle(2) = "վ���۷�" Then '****************************** վ���۷� *********************
                    If CurRightTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nTrain).lAllEndTime), AddLitterTime(TrainInf(CurRightTrain).lAllStartTime), sFirstBaseSta, 2, nTrain, CurRightTrain)
                        If UBound(tmpShuZhu) >= 2 Then
                            nRightTrain = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(CurRightTrain).lAllStartTime), sFirstBaseSta, 1, CurRightTrain)
                            nRightTrain2 = CurRightTrain ' SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sFirstBaseSta, 1, nLeftTrain)
                            nRightStartTrain = tmpShuZhu(2)
                            tmpTime4 = AddLitterTime(TrainInf(nRightStartTrain).lAllEndTime)
                            'tmpTime4 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + StationInf(TrainInf(nTrain).nPathID(1)).IKK(1)
                            tmpTime5 = AddLitterTime(TrainInf(nRightTrain2).lAllStartTime) + (AddLitterTime(TrainInf(nRightTrain).lAllStartTime) - AddLitterTime(TrainInf(nRightTrain2).lAllStartTime)) / 2
                            If tmpTime5 >= tmpTime Then
                                tmpTime = tmpTime5
                            Else
                                If tmpTime4 < tmpTime Then
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(tmpTime4 - tmpTime) '��ʾ���ֽ�·
                                    tmpTime = tmpTime4
                                End If
                            End If
                            nTmpState = 0
                        Else
                            If tmpTime <= TrainInf(CurRightTrain).lAllStartTime Then
                                TrainInf(CurRightTrain).nLinkLeft = nTrain
                                TrainInf(CurRightTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                nTmpState = 1
                            Else
                                TrainInf(CurRightTrain).nLinkLeft = nTrain
                                TrainInf(CurRightTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                nTmpState = 1
                                If MoveOneTrainLineSatReturn(CurRightTrain, SecondQuDuanID, tmpTime - TrainInf(CurRightTrain).lAllStartTime, 1) = 1 Then
                                    '�ܵ�������
                                Else
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(TrainInf(nTrain).lAllStartTime - TrainInf(nCurLeftArriTrain).lAllEndTime)  '��ʾ���ֽ�·
                                End If
                            End If
                        End If
                    Else
                        nRightTrain = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nTrain).lAllEndTime), sFirstBaseSta, 1, nTrain)
                        nRightTrain2 = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nRightTrain).lAllStartTime), sFirstBaseSta, 1, nRightTrain)
                        tmpTime5 = AddLitterTime(TrainInf(nRightTrain).lAllStartTime) + (AddLitterTime(TrainInf(nRightTrain2).lAllStartTime) - AddLitterTime(TrainInf(nRightTrain).lAllStartTime)) / 2
                        If tmpTime5 > tmpTime Then
                            tmpTime = tmpTime5
                        End If
                        nTmpState = 0
                    End If

                Else '****************************** վǰ�۷� *********************

                    If CurRightTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nTrain).lAllEndTime), AddLitterTime(TrainInf(CurRightTrain).lAllStartTime - tmpZFTime), sFirstBaseSta, 2, nTrain, CurRightTrain)
                        If UBound(tmpShuZhu) >= 1 Then
                            nTmpState = 0
                        Else
                            TrainInf(CurRightTrain).nLinkLeft = nTrain
                            TrainInf(CurRightTrain).nLeftState = 0
                            TrainInf(nTrain).nRightState = 0
                            nTmpState = 1
                            TrainInf(CurRightTrain).lAllStartTime = TimeMinus(TrainInf(CurRightTrain).lAllStartTime, TrainInf(CurRightTrain).lAllStartTime - tmpTime)
                            Call DrawSingleTrain(CurRightTrain, TrainInf(CurRightTrain).lAllStartTime, 0)
                        End If
                    Else
                        nTmpState = 0
                    End If


                End If

            Next i


            '            '**************** �������
            nTmpMulti = 0
            nTmpNum = 0
            nTmpState = 0

            For i = 1 To UBound(LeftElseTrain)
                nTrain = LeftElseTrain(i)
                'If nTrain = 103 Then Stop
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllEndTime) + tmpZFTime

                CurRightTrain = SeekAllCheDiAfterTimeJGTrainNotGou(AddLitterTime(TrainInf(nTrain).lAllEndTime), sElseBaseSta, 1, nTrain)

                If TrainInf(nTrain).TrainReturnStyle(2) = "վ���۷�" Then '****************************** վ���۷� *********************
                    If CurRightTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nTrain).lAllEndTime), AddLitterTime(TrainInf(CurRightTrain).lAllStartTime), sElseBaseSta, 2, nTrain, CurRightTrain)
                        If UBound(tmpShuZhu) >= 2 Then
                            nRightTrain = SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(CurRightTrain).lAllStartTime), sElseBaseSta, 1, CurRightTrain)
                            nRightTrain2 = CurRightTrain ' SeekAllCheDiBeforeTimeJGTrain(AddLitterTime(TrainInf(nLeftTrain).lAllEndTime), sElseBaseSta, 1, nLeftTrain)
                            nRightStartTrain = tmpShuZhu(2)
                            tmpTime4 = AddLitterTime(TrainInf(nRightStartTrain).lAllEndTime)
                            'tmpTime4 = AddLitterTime(TrainInf(nLeftTrain2).lAllEndTime) + StationInf(TrainInf(nTrain).nPathID(1)).IKK(1)
                            tmpTime5 = AddLitterTime(TrainInf(nRightTrain2).lAllStartTime) + (AddLitterTime(TrainInf(nRightTrain).lAllStartTime) - AddLitterTime(TrainInf(nRightTrain2).lAllStartTime)) / 2
                            If tmpTime5 >= tmpTime Then
                                tmpTime = tmpTime5
                            Else
                                If tmpTime4 < tmpTime Then
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(tmpTime4 - tmpTime) '��ʾ���ֽ�·
                                    tmpTime = tmpTime4
                                End If
                            End If
                            nTmpState = 0
                        Else
                            If tmpTime <= TrainInf(CurRightTrain).lAllStartTime Then
                                TrainInf(CurRightTrain).nLinkLeft = nTrain
                                TrainInf(CurRightTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                nTmpState = 1
                            Else
                                TrainInf(CurRightTrain).nLinkLeft = nTrain
                                TrainInf(CurRightTrain).nLeftState = 0
                                TrainInf(nTrain).nRightState = 0
                                nTmpState = 1
                                If MoveOneTrainLineSatReturn(CurRightTrain, SecondQuDuanID, tmpTime - TrainInf(CurRightTrain).lAllStartTime, 1) = 1 Then
                                    '�ܵ�������
                                Else
                                    'MsgBox TrainInf(nTrain).Train & "��Ҫ���ֶ̽�·�����۷�ʱ��Ϊ��" & SecondToMinute(TrainInf(nTrain).lAllStartTime - TrainInf(nCurLeftArriTrain).lAllEndTime)  '��ʾ���ֽ�·
                                End If
                            End If
                        End If
                    Else
                        nRightTrain = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nTrain).lAllEndTime), sElseBaseSta, 1, nTrain)
                        nRightTrain2 = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nRightTrain).lAllStartTime), sElseBaseSta, 1, nRightTrain)
                        tmpTime5 = AddLitterTime(TrainInf(nRightTrain).lAllStartTime) + (AddLitterTime(TrainInf(nRightTrain2).lAllStartTime) - AddLitterTime(TrainInf(nRightTrain).lAllStartTime)) / 2
                        If tmpTime5 > tmpTime Then
                            tmpTime = tmpTime5
                        End If
                        nTmpState = 0
                    End If


                    '                    If nTmpState = 0 Or nTmpState = 2 Then
                    '
                    '                        sRunJiaoLu = sFanJiaoLuStyle 'GetRunJiaoLuName(sElseBaseSta, "���")
                    '
                    '                        nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                    '
                    '                        'nElseTrain = AllCheDiAddNewTrain("��⳵", sElseBaseSta, "", tmpTime, 1, sJLstyle)
                    '                        If nElseTrain > 0 Then
                    '                            ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    '                            nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                    '                            'Call SetTmpAllTrainSeq
                    '
                    '                            TrainInf(nTrain).nLeftState = 0
                    '                            TrainInf(nTrain).nRightState = 0
                    '                            TrainInf(nElseTrain).nLeftState = 0
                    '                            TrainInf(nElseTrain).nRightState = 1
                    '                            Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)
                    '                            nTmpNum = nTmpNum + 1
                    '                        End If
                    '
                    '                    End If

                Else '****************************** վǰ�۷� *********************
                    '                    If nTrain = 389 Then Stop
                    If CurRightTrain <> 0 Then
                        Call SeekAllTrainInTwoTime(tmpShuZhu, AddLitterTime(TrainInf(nTrain).lAllEndTime), AddLitterTime(TrainInf(CurRightTrain).lAllStartTime - tmpZFTime), sElseBaseSta, 2, nTrain, CurRightTrain)
                        If UBound(tmpShuZhu) >= 1 Then
                            nTmpState = 0
                        Else
                            TrainInf(CurRightTrain).nLinkLeft = nTrain
                            TrainInf(CurRightTrain).nLeftState = 0
                            TrainInf(nTrain).nRightState = 0
                            nTmpState = 1
                            TrainInf(CurRightTrain).lAllStartTime = TimeMinus(TrainInf(CurRightTrain).lAllStartTime, TrainInf(CurRightTrain).lAllStartTime - tmpTime)
                            Call DrawSingleTrain(CurRightTrain, TrainInf(CurRightTrain).lAllStartTime, 0)
                        End If
                    Else
                        nTmpState = 0
                    End If


                    '                    If nTmpState = 0 Or nTmpState = 2 Then
                    '
                    '                        sRunJiaoLu = sFanJiaoLuStyle 'GetRunJiaoLuName(sElseBaseSta, "���")
                    '
                    '                        nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                    '
                    '                        'nElseTrain = AllCheDiAddNewTrain("��⳵", sElseBaseSta, "", tmpTime, 1, sJLstyle)
                    '                        If nElseTrain > 0 Then
                    '                            ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    '                            nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                    '                            'Call SetTmpAllTrainSeq
                    '
                    '                            TrainInf(nTrain).nLeftState = 0
                    '                            TrainInf(nTrain).nRightState = 0
                    '                            TrainInf(nElseTrain).nLeftState = 0
                    '                            TrainInf(nElseTrain).nRightState = 1
                    '                            Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)
                    '                            nTmpNum = nTmpNum + 1
                    '                        End If
                    '
                    '                    End If

                End If

            Next i


            'MsgBox "����վû�������г���·��������ӣ���"
            'Exit Sub
        End If
    End Sub
    Private Sub CallDrawDayRuKuLine(ByVal sFirstBaseSta As String, ByVal sElseBaseSta As String, ByVal FirQuDuanID As Integer, ByVal sJLstyle As String, ByVal nId As Integer)
        Dim i As Integer
        Dim j As Integer
        Dim nTrain As Integer
        Dim tmpTime As Long

        Dim nElseTrain As Integer

        Dim nLeftTrain As Integer
        Dim sRunScaleName As String
        Dim sStopScaleName As String
        Dim sRunJiaoLu As String
        Dim nTmpNum As Integer
        Dim tmpRunTime As Long
        Dim tmpZFTime As Long
        Dim tmpTime4 As Long

        sRunScaleName = GaoFenTimeSet(nId).sRunScaleName(FirQuDuanID)
        sStopScaleName = GaoFenTimeSet(nId).sStopScaleName(FirQuDuanID)

        ReDim LeftBaseTrain(0)
        ReDim LeftElseTrain(0)
        ReDim RightBaseTrain(0)
        ReDim RightElseTrain(0)


        If nIfBaseRuKu = 1 And nIfElseRuKu = 1 Then '���߶������
            '��ֵ�������ĸ���ֵ
            For i = 1 To UBound(CheDiTrainSeq)
                If i = FirQuDuanID Then
                    ReDim LeftBaseTrain(UBound(CheDiTrainSeq(i).nAfterBaseTrainSeq))
                    ReDim LeftElseTrain(UBound(CheDiTrainSeq(i).nAfterElseTrainSeq))

                    For j = 1 To UBound(CheDiTrainSeq(i).nAfterBaseTrainSeq)
                        LeftBaseTrain(j) = CheDiTrainSeq(i).nAfterBaseTrainSeq(j)
                    Next j
                    For j = 1 To UBound(CheDiTrainSeq(i).nAfterElseTrainSeq)
                        LeftElseTrain(j) = CheDiTrainSeq(i).nAfterElseTrainSeq(j)
                    Next j
                    Exit For
                End If
            Next i

            For i = 1 To UBound(LeftBaseTrain)
                nTrain = LeftBaseTrain(i)

                tmpRunTime = CalTrainRunTimeFromTrain(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).sStopSclaeName)
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllEndTime) + tmpZFTime

                sRunJiaoLu = GetRunJiaoLuName(sFirstBaseSta, "���")
                nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 1)

                'nElseTrain = AllCheDiAddNewTrain("��⳵", sFirstBaseSta, "", tmpTime, 1, sJLstyle)
                If nElseTrain > 0 Then

                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                    'Call SetTmpAllTrainSeq

                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 1
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)
                End If

            Next i

            For i = 1 To UBound(LeftElseTrain)
                nTrain = LeftElseTrain(i)

                tmpRunTime = CalTrainRunTimeFromTrain(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).sStopSclaeName)
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllEndTime) + tmpZFTime

                sRunJiaoLu = GetRunJiaoLuName(sElseBaseSta, "���")
                nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 1)

                'nElseTrain = AllCheDiAddNewTrain("��⳵", sElseBaseSta, "", tmpTime, 1, sJLstyle)
                If nElseTrain > 0 Then

                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                    'Call SetTmpAllTrainSeq

                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 1
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)
                End If

            Next i

        ElseIf nIfBaseRuKu = 1 And nIfElseRuKu = 0 Then '��׼վ���
            '��ֵ�������ĸ���ֵ
            For i = 1 To UBound(CheDiTrainSeq)
                If i = FirQuDuanID Then
                    ReDim LeftBaseTrain(UBound(CheDiTrainSeq(i).nAfterBaseTrainSeq))
                    ReDim LeftElseTrain(UBound(CheDiTrainSeq(i).nAfterElseTrainSeq))

                    For j = 1 To UBound(CheDiTrainSeq(i).nAfterBaseTrainSeq)
                        LeftBaseTrain(j) = CheDiTrainSeq(i).nAfterBaseTrainSeq(j)
                    Next j
                    For j = 1 To UBound(CheDiTrainSeq(i).nAfterElseTrainSeq)
                        LeftElseTrain(j) = CheDiTrainSeq(i).nAfterElseTrainSeq(j)
                    Next j
                    Exit For
                End If
            Next i

            For i = 1 To UBound(LeftElseTrain)
                nTrain = LeftElseTrain(i)

                tmpRunTime = CalTrainRunTimeFromTrain(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).sStopSclaeName)
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllEndTime) + tmpZFTime

                'sRunJiaoLu = GetRunJiaoLuName(sElseBaseSta, "���")
                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sElseBaseSta, "", tmpTime, 1, sJLstyle)
                If nElseTrain > 0 Then

                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain


                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 1
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)

                    ReDim Preserve LeftBaseTrain(UBound(LeftBaseTrain) + 1)
                    LeftBaseTrain(UBound(LeftBaseTrain)) = nElseTrain
                    TrainInf(nTrain).nIfCanMove = 0
                    nTmpNum = nTmpNum + 1
                End If
            Next i

            Call SetLeftBaseTrainSeq()

            For i = 1 To UBound(LeftBaseTrain)
                nTrain = LeftBaseTrain(i)
                '                If i = UBound(LeftBaseTrain) Then '�޸Ĺ̶������һ�г��ķ���ʱ��
                '                    '���һ�г���������
                '                    TrainInf(nTrain).nTrainTimeKind = TimeScaleToTimeKind(TrainInf(nTrain).TrainStyle, 1)
                '                    TrainInf(nTrain).sTrainTimeScale = TimeKindToTimeScale(TrainInf(nTrain).nTrainTimeKind)
                '
                '                    TrainInf(nTrain).lAllStartTime = TimeMinus(TrainInf(nTrain).lAllEndTime, tmpRunTime)
                '                    If AddLitterTime(TrainInf(nTrain).lAllStartTime) < AddLitterTime(nDayEndTrainTime) Then
                '                        TrainInf(nTrain).lAllStartTime = nDayEndTrainTime
                '                        TrainInf(nTrain).lAllEndTime = TimeAdd(TrainInf(nTrain).lAllStartTime, tmpRunTime)
                '                    End If
                '                End If

                tmpRunTime = CalTrainRunTimeFromTrain(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).sStopSclaeName)
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllEndTime) + tmpZFTime

                sRunJiaoLu = GetRunJiaoLuName(sFirstBaseSta, "���")
                nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 1)

                'nElseTrain = AllCheDiAddNewTrain("��⳵", sFirstBaseSta, "", tmpTime, 1, sJLstyle)


                If nElseTrain > 0 Then
                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                    'Call SetTmpAllTrainSeq

                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 1
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)
                End If

            Next i

        ElseIf nIfBaseRuKu = 0 And nIfElseRuKu = 1 Then '��һ��վ���


            '��ֵ�������ĸ���ֵ
            For i = 1 To UBound(CheDiTrainSeq)
                If i = FirQuDuanID Then
                    ReDim LeftBaseTrain(UBound(CheDiTrainSeq(i).nAfterBaseTrainSeq))
                    ReDim LeftElseTrain(UBound(CheDiTrainSeq(i).nAfterElseTrainSeq))

                    For j = 1 To UBound(CheDiTrainSeq(i).nAfterBaseTrainSeq)
                        LeftBaseTrain(j) = CheDiTrainSeq(i).nAfterBaseTrainSeq(j)
                    Next j
                    For j = 1 To UBound(CheDiTrainSeq(i).nAfterElseTrainSeq)
                        LeftElseTrain(j) = CheDiTrainSeq(i).nAfterElseTrainSeq(j)
                    Next j
                    Exit For
                End If
            Next i
            For i = 1 To UBound(LeftBaseTrain)
                nTrain = LeftBaseTrain(i)
                tmpRunTime = CalTrainRunTimeFromTrain(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).sStopSclaeName)
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sFirstBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllEndTime) + tmpZFTime

                nLeftTrain = SeekAllCheDiAfterTimeJGTrain(AddLitterTime(TrainInf(nTrain).lAllEndTime), sFirstBaseSta, 1, nTrain)
                If nLeftTrain > 0 Then
                    tmpTime4 = AddLitterTime(TrainInf(nLeftTrain).lAllStartTime) + GaoFenTimeSet(nId).JGtime(FirQuDuanID)
                    If tmpTime4 > tmpTime Then
                        tmpTime = tmpTime4
                    End If
                End If

                'sRunJiaoLu = GetRunJiaoLuName(sFirstBaseSta, "���")
                If nTrain Mod 2 <> 0 Then
                    nElseTrain = AllCheDiAddNewTrain(sFanJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                Else
                    nElseTrain = AllCheDiAddNewTrain(sJiaoLuStyle, sRunScaleName, sStopScaleName, "", tmpTime, 1)
                End If

                'nElseTrain = AllCheDiAddNewTrain("���г�", sFirstBaseSta, "", tmpTime, 1, sJLstyle)
                If nElseTrain > 0 Then

                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain


                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 1
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)

                    ReDim Preserve LeftElseTrain(UBound(LeftElseTrain) + 1)
                    LeftElseTrain(UBound(LeftElseTrain)) = nElseTrain
                    TrainInf(nTrain).nIfCanMove = 0
                End If
            Next i
            'Call SetTmpAllTrainSeq

            Call SetLeftElseTrainSeq()
            For i = 1 To UBound(LeftElseTrain)
                nTrain = LeftElseTrain(i)


                tmpRunTime = CalTrainRunTimeFromTrain(TrainInf(nTrain).sJiaoLuName, TrainInf(nTrain).sRunScaleName, TrainInf(nTrain).sStopSclaeName)
                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, TrainInf(nTrain).TrainReturnStyle(2))
                tmpTime = AddLitterTime(TrainInf(nTrain).lAllEndTime) + tmpZFTime

                sRunJiaoLu = GetRunJiaoLuName(sElseBaseSta, "���")
                nElseTrain = AllCheDiAddNewTrain(sRunJiaoLu, sRunScaleName, sStopScaleName, "", tmpTime, 1)

                'nElseTrain = AllCheDiAddNewTrain("��⳵", sElseBaseSta, "", tmpTime, 1, sJLstyle)

                If (nElseTrain + nTrain) Mod 2 = 0 Then '��ʾ���⳵�뵱ǰ����ͬ�򳵣���ʱ���Ϊ�����۷�ʱ��
                    tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, sElseBaseSta, "�����۷�")
                    tmpRunTime = CalTrainRunTimeFromTrain(TrainInf(nElseTrain).sJiaoLuName, TrainInf(nElseTrain).sRunScaleName, TrainInf(nElseTrain).sStopSclaeName)
                    TrainInf(nElseTrain).lAllStartTime = TimeAdd(TrainInf(nTrain).lAllEndTime, tmpZFTime)
                    TrainInf(nElseTrain).lAllEndTime = TimeAdd(TrainInf(nElseTrain).lAllStartTime, tmpRunTime)
                    Call DrawSingleTrain(nElseTrain, TrainInf(nElseTrain).lAllStartTime, 0)
                End If


                If nElseTrain > 0 Then

                    ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                    nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = nElseTrain
                    'Call SetTmpAllTrainSeq

                    TrainInf(nTrain).nLeftState = 0
                    TrainInf(nTrain).nRightState = 0
                    TrainInf(nElseTrain).nLeftState = 0
                    TrainInf(nElseTrain).nRightState = 1
                    Call AddNewTrainInAllCheDiInf(nTrain, nElseTrain, FirQuDuanID, 2)
                End If

            Next i
            ''''            End If
        End If
    End Sub

    '���ݽ�·���ж��г���������
    Public Function GetBasicTrainUpOrDown(ByVal sJiaoLuName As String) As Integer
        Dim i As Integer
        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName = sJiaoLuName Then
                GetBasicTrainUpOrDown = BasicTrainInf(i).nUporDown
                Exit For
            End If
        Next i

    End Function


    '�����г�����BaseTraininf�и��ƹ����������ظ��ƺ��г���ID��
    Public Function AllCheDiAddNewTrain(ByVal sJiaoLuName As String, ByVal sTrainStyle As String, ByVal sStopScale As String, ByVal sCheci As String, ByVal sTime As Long, ByVal StarOrEnd As Integer) As Integer
        AllCheDiAddNewTrain = AddTrainInformation(sJiaoLuName, sTrainStyle, sStopScale, sCheci)
        sTime = DeleteLitterTime(sTime)
        If AllCheDiAddNewTrain > 0 Then
            Dim tmpRunTime As Long
            Dim sTmpTime As Long
            If StarOrEnd = 1 Then '����
                sTmpTime = sTime
                tmpRunTime = CalTrainRunTimeNotStopFromTrain(sJiaoLuName, sTrainStyle, sStopScale, 0)
                'TrainInf(AllCheDiAddNewTrain).lAllStartTime = DeleteLitterTime(sTime)
                'TrainInf(AllCheDiAddNewTrain).lAllEndTime = TimeAdd(TrainInf(AllCheDiAddNewTrain).lAllStartTime, tmpRunTime)
            Else

                tmpRunTime = CalTrainRunTimeNotStopFromTrain(sJiaoLuName, sTrainStyle, sStopScale, 0)
                'If tmpRunTime = 0 Then
                '    sStopScale = "�м䲻ͣ"
                '    tmpRunTime = CalTrainRunTimeFromTrain(sJiaoLuName, sTrainStyle, sStopScale)
                'End If
                sTmpTime = TimeMinus(sTime, tmpRunTime)
                'TrainInf(AllCheDiAddNewTrain).lAllEndTime = DeleteLitterTime(sTime)
                'TrainInf(AllCheDiAddNewTrain).lAllStartTime = TimeMinus(TrainInf(AllCheDiAddNewTrain).lAllEndTime, tmpRunTime)
            End If
            Call DrawSingleTrain(AllCheDiAddNewTrain, sTmpTime, 0)
        End If
    End Function

    '�г�����������
    Private Sub SetAllTrainSeq(ByVal sStaName As String, ByVal nTempXu() As Integer, ByVal sStarOrArri As String, ByVal nUporDown As Integer)
        Dim i As Integer
        Dim j As Integer

        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long
        ReDim nTempXu(0)

        If sStarOrArri = "����" Then '����
            For i = 1 To UBound(TrainInf)
                If TrainInf(i).Train <> "" Then
                    If TrainInf(i).ComeStation = sStaName Then
                        If (i + nUporDown) Mod 2 = 0 Then
                            ReDim Preserve nTempXu(UBound(nTempXu) + 1)
                            nTempXu(UBound(nTempXu)) = i
                        End If
                    End If
                End If
            Next i
            '������ʱ������
            Flag = 1
            k = UBound(nTempXu)
            Do While Flag > 0
                k = k - 1
                Flag = 0
                For j = 1 To k
                    TempTime1 = AddLitterTime(TrainInf(nTempXu(j)).Starting(TrainInf(nTempXu(j)).nPathID(1)))
                    Temptime2 = AddLitterTime(TrainInf(nTempXu(j + 1)).Starting(TrainInf(nTempXu(j + 1)).nPathID(1)))
                    If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                        temp = nTempXu(j)
                        nTempXu(j) = nTempXu(j + 1)
                        nTempXu(j + 1) = temp
                        Flag = 1
                    End If
                Next j
            Loop

        Else '����

            For i = 1 To UBound(TrainInf)
                If TrainInf(i).Train <> "" Then
                    If TrainInf(i).NextStation = sStaName Then
                        If (i + nUporDown) Mod 2 = 0 Then
                            ReDim Preserve nTempXu(UBound(nTempXu) + 1)
                            nTempXu(UBound(nTempXu)) = i
                        End If
                    End If
                End If
            Next i
            '������ʱ������
            Flag = 1
            k = UBound(nTempXu)
            Do While Flag > 0
                k = k - 1
                Flag = 0
                For j = 1 To k
                    TempTime1 = AddLitterTime(TrainInf(nTempXu(j)).Arrival(TrainInf(nTempXu(j)).nPathID(UBound(TrainInf(nTempXu(j)).nPathID))))
                    Temptime2 = AddLitterTime(TrainInf(nTempXu(j + 1)).Starting(TrainInf(nTempXu(j + 1)).nPathID(UBound(TrainInf(nTempXu(j + 1)).nPathID))))
                    If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                        temp = nTempXu(j)
                        nTempXu(j) = nTempXu(j + 1)
                        nTempXu(j + 1) = temp
                        Flag = 1
                    End If
                Next j
            Loop

        End If

    End Sub

    '�����е��г�����һ�������ﰴʱ��˳������
    Private Sub SetAllTrainSeq()
        Dim j As Integer

        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long

        '������ʱ������
        Flag = 1
        k = UBound(nAllTrainSeq)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                TempTime1 = AddLitterTime(TrainInf(nAllTrainSeq(j)).lAllEndTime)
                Temptime2 = AddLitterTime(TrainInf(nAllTrainSeq(j + 1)).lAllEndTime)
                If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                    temp = nAllTrainSeq(j)
                    nAllTrainSeq(j) = nAllTrainSeq(j + 1)
                    nAllTrainSeq(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop
    End Sub

    '���Ե�ǰ�����ĳ���ID
    Public Function GetCurMoveCDid(ByVal nCurTrain As Integer) As Integer
        If TrainInf(nCurTrain).nCDPuOrNot = 1 Then
            GetCurMoveCDid = AllCDFromTrainToTmpCheDiId(nCurTrain)
        Else
            GetCurMoveCDid = AllCheDiSeekChedi(TrainInf(nCurTrain).SCheDiLeiXing, nCurTrain)
        End If
    End Function

    '�ڳ����ϼ���һ�г�
    Public Sub AllCheDiAddLianGuaCheCi(ByVal nCheDi As Integer, ByVal nTrain As Integer)

        Dim i, j As Integer
        Dim TempNtrain As Integer
        Dim TempNtrain1 As Integer
        Dim sTime As Single
        Dim sTime1 As Single
        Dim sTime2 As Single
        TrainInf(nTrain).nPuOrNot = 1
        TrainInf(nTrain).nCDPuOrNot = 1
        If UBound(tmpCheDiInf(nCheDi).nLinkTrain) = 0 Then
            ReDim Preserve tmpCheDiInf(nCheDi).nLinkTrain(UBound(tmpCheDiInf(nCheDi).nLinkTrain) + 1)
            tmpCheDiInf(nCheDi).nLinkTrain(1) = nTrain
            TrainInf(nTrain).TrainReturn(1) = 0
            TrainInf(nTrain).TrainReturn(2) = 0
            GoTo sEnd
        ElseIf UBound(tmpCheDiInf(nCheDi).nLinkTrain) = 1 Then
            TempNtrain = tmpCheDiInf(nCheDi).nLinkTrain(1)
            ReDim Preserve tmpCheDiInf(nCheDi).nLinkTrain(UBound(tmpCheDiInf(nCheDi).nLinkTrain) + 1)
            sTime = AddTimeOver24(TrainInf(nTrain).lAllStartTime)
            sTime1 = AddTimeOver24(TrainInf(TempNtrain).lAllStartTime)
            If sTime <= sTime1 Then
                tmpCheDiInf(nCheDi).nLinkTrain(2) = tmpCheDiInf(nCheDi).nLinkTrain(1)
                tmpCheDiInf(nCheDi).nLinkTrain(1) = nTrain

                TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(2)).TrainReturn(1) = tmpCheDiInf(nCheDi).nLinkTrain(1)
                TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(1)).TrainReturn(2) = tmpCheDiInf(nCheDi).nLinkTrain(2)
                GoTo sEnd
            Else
                tmpCheDiInf(nCheDi).nLinkTrain(UBound(tmpCheDiInf(nCheDi).nLinkTrain)) = nTrain
                TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(2)).TrainReturn(1) = tmpCheDiInf(nCheDi).nLinkTrain(1)
                TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(1)).TrainReturn(2) = tmpCheDiInf(nCheDi).nLinkTrain(2)
                GoTo sEnd
            End If
        Else
            TempNtrain = tmpCheDiInf(nCheDi).nLinkTrain(1)
            TempNtrain1 = tmpCheDiInf(nCheDi).nLinkTrain(UBound(tmpCheDiInf(nCheDi).nLinkTrain))
            sTime = AddTimeOver24(TrainInf(nTrain).lAllStartTime)
            sTime1 = AddTimeOver24(TrainInf(TempNtrain).lAllStartTime)
            If sTime <= sTime1 Then
                ReDim Preserve tmpCheDiInf(nCheDi).nLinkTrain(UBound(tmpCheDiInf(nCheDi).nLinkTrain) + 1)
                For j = UBound(tmpCheDiInf(nCheDi).nLinkTrain) To 2 Step -1
                    tmpCheDiInf(nCheDi).nLinkTrain(j) = tmpCheDiInf(nCheDi).nLinkTrain(j - 1)
                Next j
                tmpCheDiInf(nCheDi).nLinkTrain(1) = nTrain

                TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(2)).TrainReturn(1) = tmpCheDiInf(nCheDi).nLinkTrain(1)
                TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(1)).TrainReturn(2) = tmpCheDiInf(nCheDi).nLinkTrain(2)

                GoTo sEnd
            End If

            sTime = AddTimeOver24(TrainInf(nTrain).lAllStartTime)
            sTime1 = AddTimeOver24(TrainInf(TempNtrain1).lAllStartTime)
            If sTime >= sTime1 Then
                ReDim Preserve tmpCheDiInf(nCheDi).nLinkTrain(UBound(tmpCheDiInf(nCheDi).nLinkTrain) + 1)
                tmpCheDiInf(nCheDi).nLinkTrain(UBound(tmpCheDiInf(nCheDi).nLinkTrain)) = nTrain

                TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(UBound(tmpCheDiInf(nCheDi).nLinkTrain))).TrainReturn(1) = tmpCheDiInf(nCheDi).nLinkTrain(UBound(tmpCheDiInf(nCheDi).nLinkTrain) - 1)
                TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(UBound(tmpCheDiInf(nCheDi).nLinkTrain) - 1)).TrainReturn(2) = tmpCheDiInf(nCheDi).nLinkTrain(UBound(tmpCheDiInf(nCheDi).nLinkTrain))

                GoTo sEnd
            End If


            For i = 1 To UBound(tmpCheDiInf(nCheDi).nLinkTrain) - 1
                TempNtrain = tmpCheDiInf(nCheDi).nLinkTrain(i)
                TempNtrain1 = tmpCheDiInf(nCheDi).nLinkTrain(i + 1)
                sTime = AddTimeOver24(TrainInf(nTrain).lAllStartTime)
                sTime1 = AddTimeOver24(TrainInf(TempNtrain).lAllStartTime)
                sTime2 = AddTimeOver24(TrainInf(TempNtrain1).lAllStartTime)
                If sTime >= sTime1 And sTime <= sTime2 Then
                    ReDim Preserve tmpCheDiInf(nCheDi).nLinkTrain(UBound(tmpCheDiInf(nCheDi).nLinkTrain) + 1)
                    For j = UBound(tmpCheDiInf(nCheDi).nLinkTrain) To i + 1 Step -1
                        tmpCheDiInf(nCheDi).nLinkTrain(j) = tmpCheDiInf(nCheDi).nLinkTrain(j - 1)
                    Next j
                    tmpCheDiInf(nCheDi).nLinkTrain(i + 1) = nTrain

                    TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(i + 2)).TrainReturn(1) = tmpCheDiInf(nCheDi).nLinkTrain(i + 1)
                    TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(i + 1)).TrainReturn(2) = tmpCheDiInf(nCheDi).nLinkTrain(i + 2)
                    TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(i + 1)).TrainReturn(1) = tmpCheDiInf(nCheDi).nLinkTrain(i)
                    TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(i)).TrainReturn(2) = tmpCheDiInf(nCheDi).nLinkTrain(i + 1)

                    GoTo sEnd
                End If
            Next i
        End If

sEnd:
        If UBound(tmpCheDiInf(nCheDi).nLinkTrain) = 1 Then
            TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(1)).nLeftState = 1
            TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(1)).nRightState = 1
        Else
            For i = 1 To UBound(tmpCheDiInf(nCheDi).nLinkTrain)
                If i = 1 Then
                    TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(i)).nLeftState = 1
                    TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(i)).nRightState = 0
                ElseIf i = UBound(tmpCheDiInf(nCheDi).nLinkTrain) Then
                    TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(i)).nRightState = 1
                    TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(i)).nLeftState = 0
                Else
                    TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(i)).nLeftState = 0
                    TrainInf(tmpCheDiInf(nCheDi).nLinkTrain(i)).nRightState = 0
                End If
            Next i
        End If

    End Sub

    '�����г��ҳ��ף������г����̻�������
    Public Function AllChediFromTrainToCDid(ByVal nTrain As Integer) As Integer
        Dim i, j As Integer
        For i = 1 To UBound(tmpCheDiInf)
            For j = 1 To UBound(tmpCheDiInf(i).nLinkTrain)
                If tmpCheDiInf(i).nLinkTrain(j) = nTrain Then
                    AllChediFromTrainToCDid = i
                    Exit Function
                End If
            Next j
        Next i

    End Function

    Private Function SeekCheDiFromTrainUMT(ByVal nTrain As Integer) As Integer
        Dim i As Integer
        Dim sArriTime As Long
        Dim tmpStartTime As Long
        Dim Zftime As Long
        Zftime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, TrainInf(nTrain).NextStation, TrainInf(nTrain).TrainReturnStyle(2))
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                If TrainInf(i).TrainReturn(1) = 0 Then
                    sArriTime = AddLitterTime(TrainInf(nTrain).lAllEndTime)
                    tmpStartTime = AddLitterTime(TrainInf(i).lAllStartTime)
                    If (nTrain + i) Mod 2 <> 0 Then
                        If TrainInf(nTrain).NextStation = TrainInf(i).ComeStation Then
                            If tmpStartTime - sArriTime >= Zftime Then
                                SeekCheDiFromTrainUMT = AllChediFromTrainToCDid(i)
                                Exit For
                            End If
                        End If
                    End If
                End If
            End If
        Next i

        If SeekCheDiFromTrainUMT = 0 Then 'û�ж��û�г����ĳ�����������һ�γ���ʱ��ǰ���Ѿ����ĳ�
            ReDim Preserve tmpCheDiInf(UBound(tmpCheDiInf) + 1)
            ReDim tmpCheDiInf(UBound(tmpCheDiInf)).nLinkTrain(0)
            tmpCheDiInf(UBound(tmpCheDiInf)).sCheDiID = UBound(tmpCheDiInf)
            tmpCheDiInf(UBound(tmpCheDiInf)).sCheCiHao = UBound(tmpCheDiInf)
            SeekCheDiFromTrainUMT = UBound(tmpCheDiInf)
        End If
    End Function

    '�жϸõ�ǰ���������ܷ����ĳ��׺ϲ�
    Private Function HeBinTmpCheDiInf(ByVal nTrain As Integer, ByVal nTrain2 As Integer, ByVal ID As Integer) As Integer
        HeBinTmpCheDiInf = 0
        Dim i As Integer
        Dim j As Integer
        Dim nCdid As Integer
        nCdid = AllCheDiCheCiToCheDiID(nTrain)
        If nCdid > 0 Then
            For i = 1 To UBound(AllCheDiInf(ID).tmpCheDi)
                If UBound(AllCheDiInf(ID).tmpCheDi(i).nLinkTrain) > 0 Then
                    If AllCheDiInf(ID).tmpCheDi(i).nLinkTrain(1) = nTrain2 Then
                        For j = 1 To UBound(AllCheDiInf(ID).tmpCheDi(i).nLinkTrain)
                            ReDim Preserve tmpCheDiInf(nCdid).nLinkTrain(UBound(tmpCheDiInf(nCdid).nLinkTrain) + 1)
                            tmpCheDiInf(nCdid).nLinkTrain(UBound(tmpCheDiInf(nCdid).nLinkTrain)) = AllCheDiInf(ID).tmpCheDi(i).nLinkTrain(j)
                        Next j
                    End If
                End If
            Next i
        End If
    End Function

    '���һ���µĳ���
    Private Sub AddNewTmpCheDi(ByVal nTrain As Integer, ByVal ID As Integer)
        Dim i As Integer
        Dim j As Integer
        Dim nCdid As Integer
        For i = 1 To UBound(AllCheDiInf(ID).tmpCheDi)
            If UBound(AllCheDiInf(ID).tmpCheDi(i).nLinkTrain) > 0 Then
                If AllCheDiInf(ID).tmpCheDi(i).nLinkTrain(1) = nTrain Then
                    ReDim Preserve tmpCheDiInf(UBound(tmpCheDiInf) + 1)
                    nCdid = UBound(tmpCheDiInf)
                    ReDim tmpCheDiInf(UBound(tmpCheDiInf)).nLinkTrain(0)

                    For j = 1 To UBound(AllCheDiInf(ID).tmpCheDi(i).nLinkTrain)
                        tmpCheDiInf(nCdid).sCheDiID = nCdid
                        ReDim Preserve tmpCheDiInf(nCdid).nLinkTrain(UBound(tmpCheDiInf(nCdid).nLinkTrain) + 1)
                        tmpCheDiInf(nCdid).nLinkTrain(UBound(tmpCheDiInf(nCdid).nLinkTrain)) = AllCheDiInf(ID).tmpCheDi(i).nLinkTrain(j)
                    Next j
                End If
            End If
        Next i
    End Sub


    '���Ҹ���ʱ��ǰ��ĵ�һ�г�,�ó�һ�������г�,���������뵽�1������2����' �ȽϷ������ʱ�õ�
    Private Function SeekAllCheDiBeforeTimeJGTrain(ByVal sStime As Long, ByVal sStaName As String, ByVal StartOrArrive As Integer, ByVal nCurTrain As Integer) As Integer
        Dim i As Integer
        Dim sStartTime As Long
        Dim MinTime As Long
        Dim nId As Integer
        MinTime = 90000
        nId = 0
        If StartOrArrive = 1 Then '����
            Call SetTmpAllTrainSeqByStart()
            For i = UBound(nTmpAllTrainSeq) To 1 Step -1
                If TrainInf(nTmpAllTrainSeq(i)).ComeStation = sStaName Then 'and TrainInf(nAllTrainSeq(i)).nCDPuOrNot = 0 Then
                    sStartTime = AddLitterTime(TrainInf(nTmpAllTrainSeq(i)).lAllStartTime)
                    'stmpZFtime = GetZheFanTime(TrainInf(nCurTrain).SCheDiLeiXing, sStaName, TrainInf(nCurTrain).TrainReturnStyle(1))
                    If nTmpAllTrainSeq(i) <> nCurTrain Then
                        If sStime - sStartTime >= 0 Then
                            nId = nTmpAllTrainSeq(i)
                            Exit For
                        End If
                    End If
                End If
            Next i
        Else '����
            Call SetTmpAllTrainSeqByArri()
            '������ߵĵ�������ĳ�
            For i = UBound(nTmpAllTrainSeq) To 1 Step -1
                'If nTmpAllTrainSeq(i) = 36 Then Stop
                If TrainInf(nTmpAllTrainSeq(i)).NextStation = sStaName Then 'And TrainInf(nTmpAllTrainSeq(i)).TrainStyle = "���г�" Then
                    sStartTime = AddLitterTime(TrainInf(nTmpAllTrainSeq(i)).lAllEndTime)
                    If nTmpAllTrainSeq(i) <> nCurTrain Then
                        If sStime - sStartTime >= 0 Then
                            nId = nTmpAllTrainSeq(i)
                            Exit For
                        End If
                    End If
                End If
            Next i
            '���û���ҵ��������ұ�����ĳ�
            If nId = 0 Then
                For i = 1 To UBound(nTmpAllTrainSeq)
                    'If nTmpAllTrainSeq(i) = 36 Then Stop
                    If TrainInf(nTmpAllTrainSeq(i)).NextStation = sStaName Then 'And TrainInf(nTmpAllTrainSeq(i)).TrainStyle = "���г�" Then
                        sStartTime = AddLitterTime(TrainInf(nTmpAllTrainSeq(i)).lAllEndTime)
                        If nTmpAllTrainSeq(i) <> nCurTrain Then
                            If sStime - sStartTime <= 0 Then
                                nId = nTmpAllTrainSeq(i)
                                Exit For
                            End If
                        End If
                    End If
                Next i
            End If
        End If
        SeekAllCheDiBeforeTimeJGTrain = nId
    End Function


    '�����һ��Ӧ���̻����г�
    Private Function GetRunJiaoLuName(ByVal sStaName As String, ByVal sState As String) As String
        GetRunJiaoLuName = ""
        Dim i As Integer
        If sState = "����" Then
            For i = 1 To UBound(BasicTrainInf)
                If BasicTrainInf(i).NextStation = sStaName And BasicTrainInf(i).TrainStyle = "���⳵" Then
                    GetRunJiaoLuName = BasicTrainInf(i).sJiaoLuName
                    Exit For
                End If
            Next i
        ElseIf sState = "���" Then
            For i = 1 To UBound(BasicTrainInf)
                If BasicTrainInf(i).ComeStation = sStaName And BasicTrainInf(i).TrainStyle = "��⳵" Then
                    GetRunJiaoLuName = BasicTrainInf(i).sJiaoLuName
                    Exit For
                End If
            Next i
        End If
    End Function


    '��AllCheDiinf�ϼ���һ�˳�
    Private Sub AddNewTrainInAllCheDiInf(ByVal nTrain As Integer, ByVal nNewTrain As Integer, ByVal ID As Integer, ByVal nBeOrAfter As Integer)
        Dim i As Integer
        Dim k As Integer
        If nBeOrAfter = 1 Then '��ǰ���
            For i = 1 To UBound(AllCheDiInf(ID).tmpCheDi)
                If UBound(AllCheDiInf(ID).tmpCheDi(i).nLinkTrain) > 0 Then
                    If AllCheDiInf(ID).tmpCheDi(i).nLinkTrain(1) = nTrain Then
                        ReDim Preserve AllCheDiInf(ID).tmpCheDi(i).nLinkTrain(UBound(AllCheDiInf(ID).tmpCheDi(i).nLinkTrain) + 1)
                        For k = UBound(AllCheDiInf(ID).tmpCheDi(i).nLinkTrain) To 2 Step -1
                            AllCheDiInf(ID).tmpCheDi(i).nLinkTrain(k) = AllCheDiInf(ID).tmpCheDi(i).nLinkTrain(k - 1)
                        Next k
                        AllCheDiInf(ID).tmpCheDi(i).nLinkTrain(1) = nNewTrain
                        Exit For
                    End If
                End If
            Next i
        Else '�ں����
            For i = 1 To UBound(AllCheDiInf(ID).tmpCheDi)
                If UBound(AllCheDiInf(ID).tmpCheDi(i).nLinkTrain) > 0 Then
                    If AllCheDiInf(ID).tmpCheDi(i).nLinkTrain(UBound(AllCheDiInf(ID).tmpCheDi(i).nLinkTrain)) = nTrain Then
                        ReDim Preserve AllCheDiInf(ID).tmpCheDi(i).nLinkTrain(UBound(AllCheDiInf(ID).tmpCheDi(i).nLinkTrain) + 1)
                        AllCheDiInf(ID).tmpCheDi(i).nLinkTrain(UBound(AllCheDiInf(ID).tmpCheDi(i).nLinkTrain)) = nNewTrain
                        Exit For
                    End If
                End If
            Next i
        End If
    End Sub

    '����������֮������еĳ������г������뵽һ��������
    Private Sub SeekAllTrainInTwoTime(ByVal nTmpTrainSeq() As Integer, ByVal LeftTime As Long, ByVal RightTime As Long, ByVal sStaName As String, ByVal nArriOrStart As Integer, ByVal nTrain1 As Integer, ByVal nTrain2 As Integer)
        Dim i As Integer
        Dim tmpTime As Long
        ReDim tmpShuZhu(0)
        If nArriOrStart = 1 Then '����
            Call SetTmpAllTrainSeqByStart()
            For i = 1 To UBound(nTmpAllTrainSeq)
                'If nTmpAllTrainSeq(i) = 466 Then Stop
                If nTmpAllTrainSeq(i) <> nTrain1 Then
                    If nTmpAllTrainSeq(i) <> nTrain2 Then
                        If TrainInf(nTmpAllTrainSeq(i)).ComeStation = sStaName Then
                            tmpTime = AddLitterTime(TrainInf(nTmpAllTrainSeq(i)).lAllStartTime)
                            If tmpTime <= RightTime Then
                                If tmpTime >= LeftTime Then '= ���޸�
                                    ReDim Preserve tmpShuZhu(UBound(tmpShuZhu) + 1)
                                    tmpShuZhu(UBound(tmpShuZhu)) = nTmpAllTrainSeq(i)
                                End If
                            Else
                                Exit For
                            End If
                        End If
                    End If
                End If
            Next i
        Else '����
            Call SetTmpAllTrainSeqByArri()
            For i = 1 To UBound(nTmpAllTrainSeq)
                If nTmpAllTrainSeq(i) <> nTrain1 Then
                    If nTmpAllTrainSeq(i) <> nTrain2 Then
                        If TrainInf(nTmpAllTrainSeq(i)).NextStation = sStaName Then
                            tmpTime = AddLitterTime(TrainInf(nTmpAllTrainSeq(i)).lAllEndTime)
                            If tmpTime <= RightTime Then
                                If tmpTime >= LeftTime Then '= ���޸�
                                    ReDim Preserve tmpShuZhu(UBound(tmpShuZhu) + 1)
                                    tmpShuZhu(UBound(tmpShuZhu)) = nTmpAllTrainSeq(i)
                                End If
                            Else
                                Exit For
                            End If
                        End If
                    End If
                End If
            Next i
        End If
    End Sub


    '���Ҹ���ʱ��ǰ��ĵ�һ�г�,�ó�һ�������г�,���������뵽�1������2����' �ȽϷ������ʱ�õ�
    Private Function SeekAllCheDiBeforeTimeJGTrainNotGou(ByVal sStime As Long, ByVal sStaName As String, ByVal StartOrArrive As Integer, ByVal nCurTrain As Integer) As Integer
        Dim i As Integer
        Dim sStartTime As Long
        Dim MinTime As Long
        Dim nId As Integer
        MinTime = 90000
        nId = 0
        If StartOrArrive = 1 Then '����
            Call SetTmpAllTrainSeqByStart()
            For i = UBound(nTmpAllTrainSeq) To 1 Step -1
                If TrainInf(nTmpAllTrainSeq(i)).ComeStation = sStaName And TrainInf(nTmpAllTrainSeq(i)).nRightState = 1 Then
                    sStartTime = AddLitterTime(TrainInf(nTmpAllTrainSeq(i)).lAllStartTime)
                    If sStime <= sStartTime + ZhouQiTime Then
                        'stmpZFtime = GetZheFanTime(TrainInf(nCurTrain).SCheDiLeiXing, sStaName, TrainInf(nCurTrain).TrainReturnStyle(1))
                        If nTmpAllTrainSeq(i) <> nCurTrain Then
                            If sStime - sStartTime >= 0 Then
                                nId = nTmpAllTrainSeq(i)
                                Exit For
                            End If
                        End If
                    End If
                End If
            Next i
        Else '����
            Call SetTmpAllTrainSeqByArri()
            '������ߵĵ�������ĳ�
            For i = UBound(nTmpAllTrainSeq) To 1 Step -1
                'If nTmpAllTrainSeq(i) = 13 Then Stop
                If TrainInf(nTmpAllTrainSeq(i)).NextStation = sStaName And TrainInf(nTmpAllTrainSeq(i)).nRightState = 1 Then
                    sStartTime = AddLitterTime(TrainInf(nTmpAllTrainSeq(i)).lAllEndTime)
                    If sStime <= sStartTime + ZhouQiTime Then
                        If sStime - sStartTime >= 0 Then
                            If nTmpAllTrainSeq(i) <> nCurTrain Then
                                nId = nTmpAllTrainSeq(i)
                            End If
                            Exit For
                        End If
                    End If
                End If
            Next i
            '        '���û���ҵ��������ұ�����ĳ�
            '        If nId = 0 Then
            '            For i = 1 To UBound(nTmpAllTrainSeq)
            '                If TrainInf(nTmpAllTrainSeq(i)).NextStation = sStaName And TrainInf(nTmpAllTrainSeq(i)).nRightState = 1 Then
            '                    sStartTime = AddLitterTime(TrainInf(nTmpAllTrainSeq(i)).lAllEndTime)
            '                    If nTmpAllTrainSeq(i) <> nCurTrain Then
            '                        If sStime - sStartTime <= 0 Then
            '                            nId = nTmpAllTrainSeq(i)
            '                            Exit For
            '                        End If
            '                    End If
            '                End If
            '            Next i
            '        End If
        End If
        SeekAllCheDiBeforeTimeJGTrainNotGou = nId
    End Function

    '���Ҹ���ʱ�̺���ĵ�һ�г�,�ó�һ�������г�,���������뵽�1������2����' �ȽϷ������ʱ�õ�
    Private Function SeekAllCheDiAfterTimeJGTrain(ByVal sStime As Long, ByVal sStaName As String, ByVal StartOrArrive As Integer, ByVal nCurTrain As Integer) As Integer
        Dim i As Integer
        Dim sStartTime As Long
        Dim nId As Integer
        nId = 0
        If StartOrArrive = 2 Then '����
            Call SetTmpAllTrainSeqByArri()
            For i = 1 To UBound(nTmpAllTrainSeq)
                If TrainInf(nTmpAllTrainSeq(i)).NextStation = sStaName Then
                    sStartTime = AddLitterTime(TrainInf(nTmpAllTrainSeq(i)).lAllEndTime)
                    'stmpZFtime = GetZheFanTime(TrainInf(nCurTrain).SCheDiLeiXing, sStaName, TrainInf(nCurTrain).TrainReturnStyle(1))
                    If sStartTime - sStime > 0 Then
                        If nTmpAllTrainSeq(i) <> nCurTrain Then
                            nId = nTmpAllTrainSeq(i)
                            Exit For
                        End If
                    End If
                End If
            Next i
        Else '����
            '�����ұߵ���������ĳ�
            Call SetTmpAllTrainSeqByStart()
            For i = 1 To UBound(nTmpAllTrainSeq)
                'If nTmpAllTrainSeq(i) = 109 Then Stop
                If TrainInf(nTmpAllTrainSeq(i)).ComeStation = sStaName Then 'And TrainInf(nTmpAllTrainSeq(i)).TrainStyle = "���г�" Then
                    sStartTime = AddLitterTime(TrainInf(nTmpAllTrainSeq(i)).lAllStartTime)
                    If sStartTime - sStime >= 0 Then
                        If nTmpAllTrainSeq(i) <> nCurTrain Then
                            nId = nTmpAllTrainSeq(i)
                            Exit For
                        End If
                    End If
                End If
            Next i
            '���û���ҵ��������������ĳ�
            If nId = 0 Then
                For i = UBound(nTmpAllTrainSeq) To 1 Step -1
                    'If nTmpAllTrainSeq(i) = 380 Then Stop
                    If TrainInf(nTmpAllTrainSeq(i)).ComeStation = sStaName Then  'And TrainInf(nTmpAllTrainSeq(i)).TrainStyle = "���г�" Then
                        sStartTime = AddLitterTime(TrainInf(nTmpAllTrainSeq(i)).lAllStartTime)
                        If sStime - sStartTime >= 0 Then
                            If nTmpAllTrainSeq(i) <> nCurTrain Then
                                nId = nTmpAllTrainSeq(i)
                                Exit For
                            End If
                        End If
                    End If
                Next i
            End If
        End If
        SeekAllCheDiAfterTimeJGTrain = nId
    End Function

    '�����������۷����ߣ�ֻ��һ�г�
    Public Function MoveOneTrainLineSatReturn(ByVal nTrain As Integer, ByVal ID As Integer, ByVal sDTime As Long, ByVal nBeOrAfter As Integer) As Integer
        Dim i As Integer
        Dim j As Integer
        Dim ATrain As Integer
        Dim tmpZFTime As Long
        Dim tmpTime As Long
        MoveOneTrainLineSatReturn = 0
        If nBeOrAfter = 1 Then '��ǰ���
            For i = 1 To UBound(AllCheDiInf(ID).tmpCheDi)
                If UBound(AllCheDiInf(ID).tmpCheDi(i).nLinkTrain) > 0 Then
                    For j = 1 To UBound(AllCheDiInf(ID).tmpCheDi(i).nLinkTrain)
                        If AllCheDiInf(ID).tmpCheDi(i).nLinkTrain(j) = nTrain Then
                            If j < UBound(AllCheDiInf(ID).tmpCheDi(i).nLinkTrain) Then
                                ATrain = AllCheDiInf(ID).tmpCheDi(i).nLinkTrain(j + 1)
                                tmpZFTime = GetZheFanTime(TrainInf(ATrain).SCheDiLeiXing, StationInf(TrainInf(ATrain).nPathID(1)).sStationName, TrainInf(ATrain).TrainReturnStyle(1))
                                tmpTime = TrainInf(ATrain).lAllStartTime - TrainInf(nTrain).lAllEndTime
                                If tmpTime - tmpZFTime > 0 Then
                                    If tmpTime - tmpZFTime >= sDTime Then
                                        TrainInf(nTrain).lAllStartTime = TimeAdd(TrainInf(nTrain).lAllStartTime, sDTime)
                                        TrainInf(nTrain).lAllEndTime = TimeAdd(TrainInf(nTrain).lAllEndTime, sDTime)
                                    Else
                                        TrainInf(nTrain).lAllStartTime = TimeAdd(TrainInf(nTrain).lAllStartTime, tmpTime - tmpZFTime)
                                        TrainInf(nTrain).lAllEndTime = TimeAdd(TrainInf(nTrain).lAllEndTime, tmpTime - tmpZFTime)
                                    End If
                                    Call DrawSingleTrain(nTrain, TrainInf(nTrain).lAllStartTime, 0)
                                    MoveOneTrainLineSatReturn = 1
                                    Exit Function
                                Else
                                    MoveOneTrainLineSatReturn = 0
                                    Exit Function
                                End If
                            Else
                                TrainInf(nTrain).lAllStartTime = TimeAdd(TrainInf(nTrain).lAllStartTime, sDTime)
                                TrainInf(nTrain).lAllEndTime = TimeAdd(TrainInf(nTrain).lAllEndTime, sDTime)
                                Call DrawSingleTrain(nTrain, TrainInf(nTrain).lAllStartTime, 0)
                            End If
                        End If
                    Next j
                End If
            Next i
        Else '�ں����
            For i = 1 To UBound(AllCheDiInf(ID).tmpCheDi)
                If UBound(AllCheDiInf(ID).tmpCheDi(i).nLinkTrain) > 0 Then
                    For j = 1 To UBound(AllCheDiInf(ID).tmpCheDi(i).nLinkTrain)
                        If AllCheDiInf(ID).tmpCheDi(i).nLinkTrain(j) = nTrain Then
                            If j > 1 Then
                                ATrain = AllCheDiInf(ID).tmpCheDi(i).nLinkTrain(j - 1)
                                tmpZFTime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, StationInf(TrainInf(nTrain).nPathID(1)).sStationName, TrainInf(nTrain).TrainReturnStyle(1))
                                tmpTime = TrainInf(nTrain).lAllStartTime - TrainInf(ATrain).lAllEndTime
                                If tmpTime - tmpZFTime > 0 Then
                                    If tmpTime - tmpZFTime >= sDTime Then
                                        TrainInf(nTrain).lAllStartTime = TimeMinus(TrainInf(nTrain).lAllStartTime, sDTime)
                                        TrainInf(nTrain).lAllEndTime = TimeMinus(TrainInf(nTrain).lAllEndTime, sDTime)
                                    Else
                                        TrainInf(nTrain).lAllStartTime = TimeMinus(TrainInf(nTrain).lAllStartTime, tmpTime - tmpZFTime)
                                        TrainInf(nTrain).lAllEndTime = TimeMinus(TrainInf(nTrain).lAllEndTime, tmpTime - tmpZFTime)
                                    End If
                                    Call DrawSingleTrain(nTrain, TrainInf(nTrain).lAllStartTime, 0)
                                    MoveOneTrainLineSatReturn = 1
                                    Exit Function
                                Else
                                    MoveOneTrainLineSatReturn = 0
                                    Exit Function
                                End If
                            Else
                                TrainInf(nTrain).lAllStartTime = TimeMinus(TrainInf(nTrain).lAllStartTime, sDTime)
                                TrainInf(nTrain).lAllEndTime = TimeMinus(TrainInf(nTrain).lAllEndTime, sDTime)
                                Call DrawSingleTrain(nTrain, TrainInf(nTrain).lAllStartTime, 0)
                            End If
                        End If
                    Next j
                End If
            Next i
        End If

    End Function

    '�����е��г�����һ�������ﰴʱ��˳������
    Private Sub SetRightElseTrainSeq()

        Dim j As Integer

        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long

        '������ʱ������
        Flag = 1
        k = UBound(RightElseTrain)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                TempTime1 = AddLitterTime(TrainInf(RightElseTrain(j)).lAllEndTime)
                Temptime2 = AddLitterTime(TrainInf(RightElseTrain(j + 1)).lAllEndTime)
                If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                    temp = RightElseTrain(j)
                    RightElseTrain(j) = RightElseTrain(j + 1)
                    RightElseTrain(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop


    End Sub

    '���Ҹ���ʱ�̺���ĵ�һ�г�,�ó�һ�������г�,���������뵽�1������2����' �ȽϷ������ʱ�õ�
    Private Function SeekAllCheDiAfterTimeJGTrainNotGou(ByVal sStime As Long, ByVal sStaName As String, ByVal StartOrArrive As Integer, ByVal nCurTrain As Integer) As Integer
        Dim i As Integer
        Dim sStartTime As Long
        Dim nId As Integer
        nId = 0
        If StartOrArrive = 2 Then '����
            SetTmpAllTrainSeqByArri()
            For i = 1 To UBound(nTmpAllTrainSeq)
                If TrainInf(nTmpAllTrainSeq(i)).NextStation = sStaName And TrainInf(nTmpAllTrainSeq(i)).nRightState = 1 Then
                    sStartTime = AddLitterTime(TrainInf(nTmpAllTrainSeq(i)).lAllEndTime)
                    'stmpZFtime = GetZheFanTime(TrainInf(nCurTrain).SCheDiLeiXing, sStaName, TrainInf(nCurTrain).TrainReturnStyle(1))
                    If sStartTime - sStime > 0 Then
                        If nTmpAllTrainSeq(i) <> nCurTrain Then
                            nId = nTmpAllTrainSeq(i)
                            Exit For
                        End If
                    End If
                End If
            Next i
        Else '����
            '�����ұߵ���������ĳ�
            Call SetTmpAllTrainSeqByStart()
            For i = 1 To UBound(nTmpAllTrainSeq)
                'If nTmpAllTrainSeq(i) = 101 Then Stop
                If TrainInf(nTmpAllTrainSeq(i)).ComeStation = sStaName And TrainInf(nTmpAllTrainSeq(i)).nLeftState = 1 Then 'And TrainInf(nTmpAllTrainSeq(i)).TrainStyle = "���г�" Then
                    sStartTime = AddLitterTime(TrainInf(nTmpAllTrainSeq(i)).lAllStartTime)
                    If sStartTime <= sStime + ZhouQiTime Then
                        If sStartTime - sStime >= 0 Then
                            If nTmpAllTrainSeq(i) <> nCurTrain Then
                                nId = nTmpAllTrainSeq(i)
                            End If
                            Exit For
                        End If
                    End If
                End If
            Next i
            '        '���û���ҵ��������������ĳ�
            '        If nId = 0 Then
            '            For i = UBound(nTmpAllTrainSeq) To 1 Step -1
            '                'If nTmpAllTrainSeq(i) = 380 Then Stop
            '                If TrainInf(nTmpAllTrainSeq(i)).ComeStation = sStaName And TrainInf(nTmpAllTrainSeq(i)).nLeftState = 1 Then 'And TrainInf(nTmpAllTrainSeq(i)).TrainStyle = "���г�" Then
            '                    sStartTime = AddLitterTime(TrainInf(nTmpAllTrainSeq(i)).lAllStartTime)
            '                    If sStime - sStartTime >= 0 Then
            '                        nId = nTmpAllTrainSeq(i)
            '                        Exit For
            '                    End If
            '                End If
            '            Next i
            '        End If
        End If
        SeekAllCheDiAfterTimeJGTrainNotGou = nId
    End Function


    '�����е��г�����һ�������ﰴʱ��˳������
    Private Sub SetLeftBaseTrainSeq()
        Dim j As Integer

        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long

        '������ʱ������
        Flag = 1
        k = UBound(LeftBaseTrain)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                TempTime1 = AddLitterTime(TrainInf(LeftBaseTrain(j)).lAllEndTime)
                Temptime2 = AddLitterTime(TrainInf(LeftBaseTrain(j + 1)).lAllEndTime)
                If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                    temp = LeftBaseTrain(j)
                    LeftBaseTrain(j) = LeftBaseTrain(j + 1)
                    LeftBaseTrain(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop
    End Sub

    '�����е��г�����һ�������ﰴʱ��˳������
    Private Sub SetLeftElseTrainSeq()
        Dim j As Integer

        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long

        '������ʱ������
        Flag = 1
        k = UBound(LeftElseTrain)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                TempTime1 = AddLitterTime(TrainInf(LeftElseTrain(j)).lAllEndTime)
                Temptime2 = AddLitterTime(TrainInf(LeftElseTrain(j + 1)).lAllEndTime)
                If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                    temp = LeftElseTrain(j)
                    LeftElseTrain(j) = LeftElseTrain(j + 1)
                    LeftElseTrain(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop
    End Sub

    '����������г�����
    Private Sub InputTrainSeq(ByVal sFirstBaseSta As String, ByVal sElseBaseSta As String, ByVal nId As Integer)
        Dim k As Integer

        ReDim CheDiTrainSeq(nId).nBeforeBaseTrainSeq(0)
        ReDim CheDiTrainSeq(nId).nBeforeElseTrainSeq(0)
        ReDim CheDiTrainSeq(nId).nAfterBaseTrainSeq(0)
        ReDim CheDiTrainSeq(nId).nAfterElseTrainSeq(0)

        For k = 1 To UBound(nAllTrainSeq)
            If TrainInf(nAllTrainSeq(k)).NextStation = sFirstBaseSta Then
                If TrainInf(nAllTrainSeq(k)).ComeStation = sElseBaseSta Then
                    If TrainInf(nAllTrainSeq(k)).nLeftState = 1 Then
                        ReDim Preserve CheDiTrainSeq(nId).nBeforeElseTrainSeq(UBound(CheDiTrainSeq(nId).nBeforeElseTrainSeq) + 1)
                        CheDiTrainSeq(nId).nBeforeElseTrainSeq(UBound(CheDiTrainSeq(nId).nBeforeElseTrainSeq)) = nAllTrainSeq(k)
                    End If
                End If
            End If
        Next k

        For k = 1 To UBound(nAllTrainSeq)
            If TrainInf(nAllTrainSeq(k)).NextStation = sElseBaseSta Then
                If TrainInf(nAllTrainSeq(k)).ComeStation = sFirstBaseSta Then
                    If TrainInf(nAllTrainSeq(k)).nLeftState = 1 Then
                        ReDim Preserve CheDiTrainSeq(nId).nBeforeBaseTrainSeq(UBound(CheDiTrainSeq(nId).nBeforeBaseTrainSeq) + 1)
                        CheDiTrainSeq(nId).nBeforeBaseTrainSeq(UBound(CheDiTrainSeq(nId).nBeforeBaseTrainSeq)) = nAllTrainSeq(k)
                    End If
                End If
            End If
        Next k

        For k = 1 To UBound(nAllTrainSeq)
            If TrainInf(nAllTrainSeq(k)).NextStation = sFirstBaseSta Then
                If TrainInf(nAllTrainSeq(k)).ComeStation = sElseBaseSta Then
                    If TrainInf(nAllTrainSeq(k)).nRightState = 1 Then
                        ReDim Preserve CheDiTrainSeq(nId).nAfterBaseTrainSeq(UBound(CheDiTrainSeq(nId).nAfterBaseTrainSeq) + 1)
                        CheDiTrainSeq(nId).nAfterBaseTrainSeq(UBound(CheDiTrainSeq(nId).nAfterBaseTrainSeq)) = nAllTrainSeq(k)
                    End If
                End If
            End If
        Next k

        For k = 1 To UBound(nAllTrainSeq)
            If TrainInf(nAllTrainSeq(k)).NextStation = sElseBaseSta Then
                If TrainInf(nAllTrainSeq(k)).ComeStation = sFirstBaseSta Then
                    If TrainInf(nAllTrainSeq(k)).nRightState = 1 Then
                        ReDim Preserve CheDiTrainSeq(nId).nAfterElseTrainSeq(UBound(CheDiTrainSeq(nId).nAfterElseTrainSeq) + 1)
                        CheDiTrainSeq(nId).nAfterElseTrainSeq(UBound(CheDiTrainSeq(nId).nAfterElseTrainSeq)) = nAllTrainSeq(k)
                    End If
                End If
            End If
        Next k
    End Sub

    '�õ���һ��ʱ�������̻�ʱ��ʱ��������
    Private Sub GetPuHuaXuLie(ByVal sFirstBaseSta As String, ByVal sElseBaseSta As String, ByVal nId As Integer)
        Dim k As Integer
        ReDim BasePuHuaSeq(UBound(CheDiTrainSeq(nId).nAfterBaseTrainSeq))
        ReDim ElsePuHuaSeq(UBound(CheDiTrainSeq(nId).nAfterElseTrainSeq))
        sBaseBeTime = 0
        sElseBeTime = 0

        For k = 1 To UBound(CheDiTrainSeq(nId).nAfterBaseTrainSeq)
            BasePuHuaSeq(k) = CheDiTrainSeq(nId).nAfterBaseTrainSeq(k)
        Next k

        For k = 1 To UBound(CheDiTrainSeq(nId).nAfterElseTrainSeq)
            ElsePuHuaSeq(k) = CheDiTrainSeq(nId).nAfterElseTrainSeq(k)
        Next k

    End Sub

    '���г���ŵõ�tmpCheDiinfo��IDֵ
    Public Function AllCDFromTrainToTmpCheDiId(ByVal nTrain As Integer) As Integer
        Dim i As Integer
        Dim j As Integer

        For i = 1 To UBound(tmpCheDiInf)
            For j = 1 To UBound(tmpCheDiInf(i).nLinkTrain)
                If tmpCheDiInf(i).nLinkTrain(j) = nTrain Then
                    AllCDFromTrainToTmpCheDiId = i
                    Exit Function
                End If
            Next j
        Next i
    End Function

    '��CDid�õ�tmpCheDiinfo��IDֵ
    Private Function AllCDFromCDidTotmpId(ByVal sCdid As String) As Integer
        Dim i As Integer
        For i = 1 To UBound(tmpCheDiInf)
            If tmpCheDiInf(i).sCheDiID = sCdid Then
                AllCDFromCDidTotmpId = i
                Exit For
            End If
        Next i
    End Function

    '���Ҵ��ڿ���״̬�ĳ���
    Private Function AllCheDiSeekChedi(ByVal SCheDiLeiXing As String, ByVal nTrain As Integer) As Integer
        Dim i As Integer
        Dim ntmpID As Integer
        Dim ntmpTrain As Integer
        Dim sArriTime As Long
        Dim tmpStartTime As Long
        Dim Zftime As Long
        AllCheDiSeekChedi = 0
        For i = 1 To UBound(BaseChediInfo)
            'If i = 11 Then Stop
            If BaseChediInfo(i).SCheDiLeiXing = SCheDiLeiXing Then
                If BaseChediInfo(i).bIfGouWang = 0 Then
                    ntmpID = AllCDFromCDidTotmpId(BaseChediInfo(i).sCheDiID)
                    If ntmpID = 0 Then
                        ReDim Preserve tmpCheDiInf(UBound(tmpCheDiInf) + 1)
                        ReDim tmpCheDiInf(UBound(tmpCheDiInf)).nLinkTrain(0)
                        tmpCheDiInf(UBound(tmpCheDiInf)).sCheDiID = BaseChediInfo(i).sCheDiID
                        tmpCheDiInf(UBound(tmpCheDiInf)).sCheCiHao = UBound(tmpCheDiInf)
                        ReDim tmpCheDiInf(UBound(tmpCheDiInf)).nLinkTrain(0)
                        'BaseChediInfo(i).bIfGouWang = 1
                        AllCheDiSeekChedi = i
                        Exit For
                    Else
                        '�ɼ���ǰ��
                        Zftime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, TrainInf(nTrain).NextStation, TrainInf(nTrain).TrainReturnStyle(2))
                        ntmpTrain = tmpCheDiInf(ntmpID).nLinkTrain(1)
                        If nTrain Mod 2 = 0 And TrainInf(nTrain).NextStation = TrainInf(ntmpTrain).ComeStation Then
                            If ntmpTrain Mod 2 <> 0 Then
                                sArriTime = AddLitterTime(TrainInf(nTrain).lAllEndTime)
                                tmpStartTime = AddLitterTime(TrainInf(ntmpTrain).lAllStartTime)
                                If tmpStartTime - sArriTime >= Zftime Then
                                    AllCheDiSeekChedi = i
                                    Exit For
                                End If
                            End If
                        Else
                            If ntmpTrain Mod 2 = 0 And TrainInf(nTrain).NextStation = TrainInf(ntmpTrain).ComeStation Then
                                sArriTime = AddLitterTime(TrainInf(nTrain).lAllEndTime)
                                tmpStartTime = AddLitterTime(TrainInf(ntmpTrain).lAllStartTime)
                                If tmpStartTime - sArriTime >= Zftime Then
                                    AllCheDiSeekChedi = i
                                    Exit For
                                End If
                            End If
                        End If

                        '�ɼ��ں���
                        ntmpTrain = tmpCheDiInf(ntmpID).nLinkTrain(UBound(tmpCheDiInf(ntmpID).nLinkTrain))
                        'If nTmpTrain = 27 Then Stop
                       
                        If nTrain Mod 2 = 0 Then
                            If ntmpTrain Mod 2 <> 0 And TrainInf(ntmpTrain).NextStation = TrainInf(nTrain).ComeStation Then
                                sArriTime = AddLitterTime(TrainInf(ntmpTrain).lAllEndTime)
                                tmpStartTime = AddLitterTime(TrainInf(nTrain).lAllStartTime)
                                If tmpStartTime - sArriTime >= Zftime Then
                                    AllCheDiSeekChedi = i
                                    Exit For
                                End If
                            End If
                        Else
                            If ntmpTrain Mod 2 = 0 And TrainInf(ntmpTrain).NextStation = TrainInf(nTrain).ComeStation Then
                                sArriTime = AddLitterTime(TrainInf(ntmpTrain).lAllEndTime)
                                tmpStartTime = AddLitterTime(TrainInf(nTrain).lAllStartTime)
                                If tmpStartTime - sArriTime >= Zftime Then
                                    AllCheDiSeekChedi = i
                                    Exit For
                                End If
                            End If
                        End If
                    End If
            End If
                End If
        Next i

        If AllCheDiSeekChedi = 0 Then 'û�ж��û�г����ĳ�����������һ�γ���ʱ��ǰ���Ѿ����ĳ�
            ReDim Preserve tmpCheDiInf(UBound(tmpCheDiInf) + 1)
            ReDim tmpCheDiInf(UBound(tmpCheDiInf)).nLinkTrain(0)
            tmpCheDiInf(UBound(tmpCheDiInf)).sCheDiID = UBound(tmpCheDiInf)
            tmpCheDiInf(UBound(tmpCheDiInf)).sCheCiHao = UBound(tmpCheDiInf)
            AllCheDiSeekChedi = UBound(tmpCheDiInf)
        End If
    End Function

    '���ν�·�̻�ʱ�����Ҵ��ڿ���״̬�ĳ���
    Private Function AllCheDiSeekChediCircleJL(ByVal SCheDiLeiXing As String, ByVal nTrain As Integer, ByVal nMaxTime As Integer) As Integer
        Dim i As Integer
        'Dim ntmpID As Integer
        Dim ntmpTrain As Integer
        Dim sArriTime As Long
        Dim tmpStartTime As Long
        Dim Zftime As Long
        AllCheDiSeekChediCircleJL = 0
        For i = 1 To UBound(tmpCheDiInf)
            ntmpTrain = tmpCheDiInf(i).nLinkTrain(UBound(tmpCheDiInf(i).nLinkTrain))
            If ntmpTrain > 0 Then
                Zftime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, TrainInf(nTrain).NextStation, TrainInf(nTrain).TrainReturnStyle(2))
                If TrainInf(ntmpTrain).sJiaoLuName = TrainInf(nTrain).sJiaoLuName Then
                    sArriTime = AddLitterTime(TrainInf(ntmpTrain).lAllEndTime)
                    tmpStartTime = AddLitterTime(TrainInf(nTrain).lAllStartTime)
                    If tmpStartTime - sArriTime >= Zftime And tmpStartTime - sArriTime <= nMaxTime Then
                        AllCheDiSeekChediCircleJL = i
                        Exit For
                    End If
                End If
            End If
        Next

        If AllCheDiSeekChediCircleJL = 0 Then 'û�ж��û�г����ĳ�
            ReDim Preserve tmpCheDiInf(UBound(tmpCheDiInf) + 1)
            ReDim tmpCheDiInf(UBound(tmpCheDiInf)).nLinkTrain(0)
            tmpCheDiInf(UBound(tmpCheDiInf)).sCheDiID = UBound(tmpCheDiInf)
            tmpCheDiInf(UBound(tmpCheDiInf)).sCheCiHao = UBound(tmpCheDiInf)
            AllCheDiSeekChediCircleJL = UBound(tmpCheDiInf)
        End If
    End Function

    '�����е��г�����һ�������ﰴʱ��˳������
    Private Sub SetRightBaseTrainSeq()
        Dim j As Integer

        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long

        '������ʱ������
        Flag = 1
        k = UBound(RightBaseTrain)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                TempTime1 = AddLitterTime(TrainInf(RightBaseTrain(j)).lAllEndTime)
                Temptime2 = AddLitterTime(TrainInf(RightBaseTrain(j + 1)).lAllEndTime)
                If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                    temp = RightBaseTrain(j)
                    RightBaseTrain(j) = RightBaseTrain(j + 1)
                    RightBaseTrain(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop
    End Sub

    'ͨ�����εõ�CheDiID
    Public Function AllCheDiCheCiToCheDiID(ByVal nCheCi As Integer) As Integer
        Dim i As Integer
        Dim j As Integer
        AllCheDiCheCiToCheDiID = 0
        For j = 1 To UBound(tmpCheDiInf)
            For i = 1 To UBound(tmpCheDiInf(j).nLinkTrain)
                If tmpCheDiInf(j).nLinkTrain(i) = nCheCi Then
                    AllCheDiCheCiToCheDiID = j
                    Exit Function
                End If
            Next i
        Next j
    End Function

    '�����е��г�����һ�������ﰴʱ��˳������
    Private Sub SetTmpAllTrainSeqByStart()
        Dim j As Integer
        Dim i As Integer
        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long
        ReDim nTmpAllTrainSeq(0)
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = i
            End If
        Next

        '������ʱ������
        Flag = 1
        k = UBound(nTmpAllTrainSeq)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                TempTime1 = AddLitterTime(TrainInf(nTmpAllTrainSeq(j)).lAllStartTime)
                Temptime2 = AddLitterTime(TrainInf(nTmpAllTrainSeq(j + 1)).lAllStartTime)
                If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                    temp = nTmpAllTrainSeq(j)
                    nTmpAllTrainSeq(j) = nTmpAllTrainSeq(j + 1)
                    nTmpAllTrainSeq(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop

    End Sub

    '�����е��г�����һ�������ﰴʱ��˳������
    Private Sub SetTmpAllTrainSeqByArri()
        Dim j As Integer
        Dim i As Integer
        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long

        ReDim nTmpAllTrainSeq(0)
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                ReDim Preserve nTmpAllTrainSeq(UBound(nTmpAllTrainSeq) + 1)
                nTmpAllTrainSeq(UBound(nTmpAllTrainSeq)) = i
            End If
        Next

        '������ʱ������
        Flag = 1
        k = UBound(nTmpAllTrainSeq)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                TempTime1 = AddLitterTime(TrainInf(nTmpAllTrainSeq(j)).lAllEndTime)
                Temptime2 = AddLitterTime(TrainInf(nTmpAllTrainSeq(j + 1)).lAllEndTime)
                If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                    temp = nTmpAllTrainSeq(j)
                    nTmpAllTrainSeq(j) = nTmpAllTrainSeq(j + 1)
                    nTmpAllTrainSeq(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop


    End Sub
    '����ʼ������
    Public Sub AllCheDiDrawTrainJiaoLu()
        'Call MDIForm1.mnuDrawCDJL_Click()
        Call ResetPrintTrainString() '�Գ������±��
        Call ShowChediJiaolu2()
    End Sub

    Public Sub ShowAllErrorInfor()
        Dim StrElseErr As String
        Dim StrTrainErr As String
        StrTrainErr = ""
        StrElseErr = ""
        Dim KK As Integer
        Dim i As Integer
        KK = 0
        If UBound(ErrorInf) > 0 Then
            For i = 1 To UBound(ErrorInf)
                'Debug.Print TrainErrInf(i).nTrain & TrainErrInf(i).sErrorMessage
                StrTrainErr = StrTrainErr & vbCrLf & ErrorInf(i) ' "��:" & TrainErrInf(i).sErrorMessage
                KK = 1
            Next i
        End If

        If KK = 1 Then
            Dim nf As New frmInfor
            nf.Show()
            nf.Text = "������Ϣ!!"
            nf.txtInfor.Text = StrTrainErr & vbCrLf & vbCrLf & StrElseErr
        End If
    End Sub

    Public Sub ShowChediJiaolu2()
        Call RefreshDiagram(1)
    End Sub

    '������·�������ӳ�,��С��·
    Public Sub SetTrainLongLine(ByVal sJLstyle As String, ByVal sFanJLStyle As String, ByVal sDayBeTime As Long, ByVal sDayEndTime As Long, ByVal sStartSta As String, _
    ByVal sAnoStartSta As String, ByVal sAnoEndSta As String, ByVal sLongShortState As Integer, ByVal sPuHuaFS As String, ByVal nBi1 As Integer, ByVal nBi2 As Integer)
        Dim i As Integer
        Dim tmpK As Integer
        Dim nTrain As Integer
        Dim nNtrain As Integer
        Dim sDisRunTime As Long
        Dim sTime As Long
        Dim sTime1 As Long
        Dim nKind As String
        'sDownLongRunTime = GetLongRunTime(1)
        'sUpLongRunTime = GetLongRunTime(2)
        'sDisRunTime = CalDisTimeFromDifJiaoLu

        'sLongJG = MinuteToSecond(Me.txtLongJG.Text)
        tmpK = 0

        Dim sTmpFanJL As String

        If sLongShortState = 1 Then

            Call SetTmpAllTrainSeqByStartDown(sStartSta) '����������

            If sPuHuaFS = "�ȱ����̻�" Then
                tmpK = 0
                For i = 1 To UBound(nTmpAllTrainSeqDown)
                    nTrain = nTmpAllTrainSeqDown(i)
                    If nTrain Mod 2 <> 0 And TrainInf(nTrain).ComeStation <> "����" Then
                        sTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)) '- sDisRunTime
                        If sTime >= sDayBeTime And sTime <= sDayEndTime Then
                            If nBi1 <= 1 Then
                                If nBi2 <> 0 Then
                                    If tmpK Mod (nBi2 + 1) = 0 Then
                                        nNtrain = TrainInf(nTrain).TrainReturn(1)
                                        nKind = TrainInf(nTrain).nTrainTimeKind
                                        sTmpFanJL = GetCurFanJiaoLuStyle(TrainInf(nTrain).sJiaoLuName, sAnoStartSta, 2)
                                        Call ResetLongTrain(nTrain, 1, sTmpFanJL)
                                        If nNtrain > 0 Then
                                            sTime1 = TrainInf(nNtrain).Arrival(TrainInf(nNtrain).nPathID(UBound(TrainInf(nNtrain).nPathID)))
                                            nKind = TrainInf(nNtrain).nTrainTimeKind
                                            If sTime1 + sDisRunTime <= sDayEndTime Then
                                                sTmpFanJL = GetCurFanJiaoLuStyle(TrainInf(nNtrain).sJiaoLuName, sAnoStartSta, 1)
                                                Call ResetLongTrain(nNtrain, 2, sTmpFanJL)

                                            End If
                                        End If
                                    End If
                                    tmpK = tmpK + 1
                                End If
                            Else
                                If tmpK Mod (nBi1 + 1) <> 0 Then
                                    nNtrain = TrainInf(nTrain).TrainReturn(1)
                                    nKind = TrainInf(nTrain).nTrainTimeKind
                                    sTmpFanJL = GetCurFanJiaoLuStyle(TrainInf(nTrain).sJiaoLuName, sAnoStartSta, 2)
                                    Call ResetLongTrain(nTrain, 1, sTmpFanJL)
                                    If nNtrain > 0 Then
                                        sTime1 = TrainInf(nNtrain).Arrival(TrainInf(nNtrain).nPathID(UBound(TrainInf(nNtrain).nPathID)))
                                        nKind = TrainInf(nNtrain).nTrainTimeKind
                                        If sTime1 + sDisRunTime <= sDayEndTime Then
                                            sTmpFanJL = GetCurFanJiaoLuStyle(TrainInf(nNtrain).sJiaoLuName, sAnoStartSta, 1)
                                            Call ResetLongTrain(nNtrain, 2, sTmpFanJL)
                                        End If
                                    End If
                                End If
                                tmpK = tmpK + 1
                            End If
                        End If
                    End If
                Next i
            End If

        Else '���е�һվΪ����

            Call SetTmpAllTrainSeqByStartDown(sStartSta) '����������

            If sPuHuaFS = "�ȱ����̻�" Then
                tmpK = 0
                For i = 1 To UBound(nTmpAllTrainSeqDown)
                    nTrain = nTmpAllTrainSeqDown(i)
                    If nTrain Mod 2 <> 0 And TrainInf(nTrain).ComeStation <> "����" Then
                        sTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)) '- sDisRunTime
                        If sTime >= sDayBeTime And sTime <= sDayEndTime Then
                            If nBi1 <= 1 Then
                                If nBi2 <> 0 Then
                                    If tmpK Mod (nBi2 + 1) = 0 Then
                                        nNtrain = TrainInf(nTrain).TrainReturn(2)
                                        nKind = TrainInf(nTrain).nTrainTimeKind
                                        sTmpFanJL = GetCurFanJiaoLuStyle(TrainInf(nTrain).sJiaoLuName, sAnoEndSta, 1)
                                        Call ResetLongTrain(nTrain, 1, sTmpFanJL)
                                        If nNtrain > 0 Then
                                            sTime1 = TrainInf(nNtrain).Arrival(TrainInf(nNtrain).nPathID(UBound(TrainInf(nNtrain).nPathID)))
                                            nKind = TrainInf(nNtrain).nTrainTimeKind
                                            If sTime1 + sDisRunTime <= sDayEndTime Then
                                                sTmpFanJL = GetCurFanJiaoLuStyle(TrainInf(nNtrain).sJiaoLuName, sAnoEndSta, 2)
                                                Call ResetLongTrain(nNtrain, 2, sTmpFanJL)

                                            End If
                                        End If
                                    End If
                                    tmpK = tmpK + 1
                                End If
                            Else
                                If tmpK Mod (nBi1 + 1) <> 0 Then
                                    nNtrain = TrainInf(nTrain).TrainReturn(2)
                                    nKind = TrainInf(nTrain).nTrainTimeKind
                                    sTmpFanJL = GetCurFanJiaoLuStyle(TrainInf(nTrain).sJiaoLuName, sAnoEndSta, 1)
                                    Call ResetLongTrain(nTrain, 1, sTmpFanJL)
                                    If nNtrain > 0 Then
                                        sTime1 = TrainInf(nNtrain).Arrival(TrainInf(nNtrain).nPathID(UBound(TrainInf(nNtrain).nPathID)))
                                        nKind = TrainInf(nNtrain).nTrainTimeKind
                                        If sTime1 + sDisRunTime <= sDayEndTime Then
                                            sTmpFanJL = GetCurFanJiaoLuStyle(TrainInf(nNtrain).sJiaoLuName, sAnoEndSta, 2)
                                            Call ResetLongTrain(nNtrain, 2, sTmpFanJL)
                                        End If
                                    End If
                                End If
                                tmpK = tmpK + 1
                            End If
                        End If
                    End If
                Next i
            End If
        End If

    End Sub

    '����ӳ���·��
    Public Function GetCurFanJiaoLuStyle(ByVal sJiaoLuName As String, ByVal sSta As String, ByVal nStartOrEnd As Integer) As String
        Dim i As Integer
        Dim sBeSta As String
        Dim sEnSta As String
        GetCurFanJiaoLuStyle = ""

        If nStartOrEnd = 1 Then '����վ
            For i = 1 To UBound(BasicTrainInf)
                If BasicTrainInf(i).sJiaoLuName = sJiaoLuName Then
                    sEnSta = BasicTrainInf(i).ComeStation
                    GetCurFanJiaoLuStyle = sEnSta & "-->" & sSta
                    Exit For
                End If
            Next i
        Else '����վ
            For i = 1 To UBound(BasicTrainInf)
                If BasicTrainInf(i).sJiaoLuName = sJiaoLuName Then
                    sBeSta = BasicTrainInf(i).NextStation
                    GetCurFanJiaoLuStyle = sSta & "-->" & sBeSta
                    Exit For
                End If
            Next i

        End If

        'For i = 1 To UBound(BasicTrainInf)
        '    If BasicTrainInf(i).sJiaoLuName = sJiaoLuName Then
        '        If (BasicTrainInf(i).nUporDown + nTrain) Mod 2 = 0 Then
        '            GetCurFanJiaoLuStyle = sJiaoLuName
        '            Exit For
        '        End If
        '    ElseIf BasicTrainInf(i).sJiaoLuName = sFanJiaoLuName Then
        '        If (BasicTrainInf(i).nUporDown + nTrain) Mod 2 <> 0 Then
        '            GetCurFanJiaoLuStyle = sFanJiaoLuName
        '            Exit For
        '        End If
        '    End If
        'Next i

    End Function


    '�����е��г�����һ�������ﰴʱ��˳������,����nTmpAllTrainSeqDown������
    Public Sub SetTmpAllTrainSeqByStartDown(ByVal sStaName As String)
        Dim i As Integer
        Dim j As Integer

        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long
        ReDim nTmpAllTrainSeqDown(0)
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" And i Mod 2 <> 0 And TrainInf(i).ComeStation = sStaName Then
                ReDim Preserve nTmpAllTrainSeqDown(UBound(nTmpAllTrainSeqDown) + 1)
                nTmpAllTrainSeqDown(UBound(nTmpAllTrainSeqDown)) = i
            End If
        Next i
        '����ʱ������
        Flag = 1
        k = UBound(nTmpAllTrainSeqDown)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                TempTime1 = AddLitterTime(TrainInf(nTmpAllTrainSeqDown(j)).lAllStartTime)
                Temptime2 = AddLitterTime(TrainInf(nTmpAllTrainSeqDown(j + 1)).lAllStartTime)
                If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                    temp = nTmpAllTrainSeqDown(j)
                    nTmpAllTrainSeqDown(j) = nTmpAllTrainSeqDown(j + 1)
                    nTmpAllTrainSeqDown(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop

    End Sub


    '��������ĳ��׽�·���¹���'��С��·
    Public Sub ResetJiaoLu(ByVal sJLstyle As String, ByVal sLongShortState As Integer, ByVal sAnoFirName As String, ByVal sAnoEndSta As String)
        Dim i As Integer
        Dim j As Integer
        Dim sTime As Long
        Dim nCD1 As Integer
        Dim nCD2 As Integer
        Dim nTrain As Integer
        Dim nFtrain As Integer

        ReDim tmpCheDiInf(0)
        ReDim tmpCheDiInf(UBound(ChediInfo))
        For i = 1 To UBound(ChediInfo)
            tmpCheDiInf(i).sCheDiID = ChediInfo(i).sCheDiID
            tmpCheDiInf(i).sCheCiHao = ChediInfo(i).sCheCiHao

            ReDim tmpCheDiInf(i).nLinkTrain(UBound(ChediInfo(i).nLinkTrain))
            For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                tmpCheDiInf(i).nLinkTrain(j) = ChediInfo(i).nLinkTrain(j)
            Next j
        Next i


        If sLongShortState = 1 Then
            Call SetTmpAllTrainSeqByArriUp() '�����е�������
            Call SetTmpAllTrainSeqByStartDown(sAnoFirName) '����������

            For i = 1 To UBound(nTmpAllTrainSeqUp)
                nTrain = nTmpAllTrainSeqUp(i)
                'If nTrain = 2 Then Stop
                If nTrain Mod 2 = 0 And TrainInf(nTrain).NextStation = sAnoFirName Then
                    'If TrainInf(nTrain).NextStation = "�ɽ��³�" Then
                    sTime = AddLitterTime(TrainInf(nTrain).lAllEndTime)
                    nFtrain = SeekGouSatisfiedTrain(sTime, sAnoFirName, nTrain)
                    'If nFtrain <> 0 Then
                    nCD1 = AllChediFromTrainToCDid(nTrain)
                    nCD2 = AllChediFromTrainToCDid(nFtrain)
                    'If nCD1 <> 0 And nCD2 <> 0 Then
                    Call AddTrainToCheDi1(nCD1, nCD2, nTrain, nFtrain)
                    TrainInf(nFtrain).nCDPuOrNot = 1
                    'End If
                    'End If
                End If
            Next i
        Else
            Call SetTmpAllTrainSeqByArriUp() '�����е�������
            'Call SetTmpAllTrainSeqByStartDown(sAnoEndSta) '����������
            For i = 1 To UBound(nTmpAllTrainSeqUp)
                nTrain = nTmpAllTrainSeqUp(i)
                If nTrain Mod 2 <> 0 And TrainInf(nTrain).NextStation = sAnoEndSta Then
                    'If TrainInf(nTrain).NextStation = "�ɽ��³�" Then
                    sTime = AddLitterTime(TrainInf(nTrain).lAllEndTime)
                    nFtrain = SeekGouSatisfiedTrainUp(sTime, sAnoEndSta, nTrain)
                    'If nFtrain <> 0 Then
                    nCD1 = AllChediFromTrainToCDid(nTrain)
                    nCD2 = AllChediFromTrainToCDid(nFtrain)
                    'If nCD1 <> 0 And nCD2 <> 0 Then
                    Call AddTrainToCheDi1(nCD1, nCD2, nTrain, nFtrain)
                    TrainInf(nFtrain).nCDPuOrNot = 1
                    'End If
                    'End If
                End If
            Next i
        End If

        Call InputCheDiInformation()
    End Sub

    '�ҵ������۷�Ҫ�����һ�г�,���г�
    Public Function SeekGouSatisfiedTrainUp(ByVal sTime As Long, ByVal sStaName As String, ByVal nForTrain As Integer) As Integer
        Dim i As Integer
        Dim nTrain As Integer
        Dim Zftime As Long
        Dim sStime As Long
        ReDim tmpShuZhu(0)
        ReDim tmpShuZhu1(0)
        For i = 1 To UBound(nTmpAllTrainSeqUp)
            nTrain = nTmpAllTrainSeqUp(i)
            If nTrain Mod 2 = 0 And TrainInf(nTrain).ComeStation = sStaName And TrainInf(nTrain).nCDPuOrNot = 0 Then
                sStime = AddLitterTime(TrainInf(nTrain).lAllStartTime)
                Zftime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, TrainInf(nTrain).ComeStation, TrainInf(nTrain).TrainReturnStyle(1))
                If sStime - sTime >= Zftime Then
                    Call SeekAllTrainInTwoTime(tmpShuZhu, sTime, sStime, TrainInf(nTrain).ComeStation, 1, nTrain, nForTrain)
                    Call SeekAllTrainInTwoTime(tmpShuZhu1, sTime, sStime, TrainInf(nTrain).ComeStation, 2, nTrain, nForTrain)

                    'If UBound(tmpShuZhu) <= 1 And UBound(tmpShuZhu1) <= 1 Then
                    '    SeekGouSatisfiedTrainUp = nTrain
                    'Else
                    '    SeekGouSatisfiedTrainUp = 0
                    'End If
                    'Exit For

                    If TrainInf(nTrain).TrainReturnStyle(1) = "վ���۷�" Then
                        If UBound(tmpShuZhu) <= 1 And UBound(tmpShuZhu1) <= 1 Then
                            SeekGouSatisfiedTrainUp = nTrain
                        Else
                            SeekGouSatisfiedTrainUp = 0
                        End If
                        Exit For
                    Else
                        If UBound(tmpShuZhu) <= 0 And UBound(tmpShuZhu1) <= 0 Then
                            SeekGouSatisfiedTrainUp = nTrain
                        Else
                            SeekGouSatisfiedTrainUp = 0
                        End If
                        Exit For
                    End If
                End If
            End If
        Next i
    End Function


    '�����е��г�����һ�������ﰴʱ��˳������
    Public Sub SetTmpAllTrainSeqByArriUp()
        Dim i As Integer
        Dim j As Integer

        Dim k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Long

        ReDim nTmpAllTrainSeqUp(0)
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                ReDim Preserve nTmpAllTrainSeqUp(UBound(nTmpAllTrainSeqUp) + 1)
                nTmpAllTrainSeqUp(UBound(nTmpAllTrainSeqUp)) = i
            End If
        Next i
        '������ʱ������
        Flag = 1
        k = UBound(nTmpAllTrainSeqUp)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                TempTime1 = AddLitterTime(TrainInf(nTmpAllTrainSeqUp(j)).lAllEndTime)
                Temptime2 = AddLitterTime(TrainInf(nTmpAllTrainSeqUp(j + 1)).lAllEndTime)
                If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                    temp = nTmpAllTrainSeqUp(j)
                    nTmpAllTrainSeqUp(j) = nTmpAllTrainSeqUp(j + 1)
                    nTmpAllTrainSeqUp(j + 1) = temp
                    Flag = 1
                End If
            Next j
        Loop
    End Sub

    '������1�����µ��г�
    Public Sub AddTrainToCheDi1(ByVal nCD1 As Integer, ByVal nCD2 As Integer, ByVal nTrain As Integer, ByVal nAddTrain As Integer)
        Dim i As Integer
        Dim j As Integer
        Dim tmpTrain() As Integer
        ReDim tmpTrain(0)


        If nCD2 > 0 Then
            For i = 1 To UBound(tmpCheDiInf(nCD2).nLinkTrain)
                If tmpCheDiInf(nCD2).nLinkTrain(i) = nAddTrain Then
                    For j = i To UBound(tmpCheDiInf(nCD2).nLinkTrain)
                        ReDim Preserve tmpTrain(UBound(tmpTrain) + 1)
                        tmpTrain(UBound(tmpTrain)) = tmpCheDiInf(nCD2).nLinkTrain(j)
                    Next j
                    ReDim Preserve tmpCheDiInf(nCD2).nLinkTrain(i - 1)
                    Exit For
                End If
            Next i
        End If

        For i = 1 To UBound(tmpCheDiInf(nCD1).nLinkTrain)
            If tmpCheDiInf(nCD1).nLinkTrain(i) = nTrain Then
                If i = UBound(tmpCheDiInf(nCD1).nLinkTrain) Then

                Else
                    ReDim Preserve tmpCheDiInf(UBound(tmpCheDiInf) + 1)
                    tmpCheDiInf(UBound(tmpCheDiInf)).sCheDiID = UBound(tmpCheDiInf)
                    ReDim Preserve tmpCheDiInf(UBound(tmpCheDiInf)).nLinkTrain(0)
                    For j = i + 1 To UBound(tmpCheDiInf(nCD1).nLinkTrain)
                        ReDim Preserve tmpCheDiInf(UBound(tmpCheDiInf)).nLinkTrain(UBound(tmpCheDiInf(UBound(tmpCheDiInf)).nLinkTrain) + 1)
                        tmpCheDiInf(UBound(tmpCheDiInf)).nLinkTrain(UBound(tmpCheDiInf(UBound(tmpCheDiInf)).nLinkTrain)) = tmpCheDiInf(nCD1).nLinkTrain(j)
                    Next j
                End If

                ReDim Preserve tmpCheDiInf(nCD1).nLinkTrain(i)
                For j = 1 To UBound(tmpTrain)
                    ReDim Preserve tmpCheDiInf(nCD1).nLinkTrain(UBound(tmpCheDiInf(nCD1).nLinkTrain) + 1)
                    tmpCheDiInf(nCD1).nLinkTrain(UBound(tmpCheDiInf(nCD1).nLinkTrain)) = tmpTrain(j)
                Next j

                Exit For
            End If
        Next i
    End Sub

    '�ҵ������۷�Ҫ�����һ�г�
    Public Function SeekGouSatisfiedTrain(ByVal sTime As Long, ByVal sStaName As String, ByVal nTrain2 As Integer) As Integer
        Dim i As Integer
        Dim nTrain As Integer
        Dim Zftime As Long
        Dim sStime As Long
        ReDim tmpShuZhu(0)
        ReDim tmpShuZhu1(0)

        For i = 1 To UBound(nTmpAllTrainSeqDown)
            nTrain = nTmpAllTrainSeqDown(i)
            'If nTrain = 437 Then Stop
            If nTrain Mod 2 <> 0 And TrainInf(nTrain).ComeStation = sStaName And TrainInf(nTrain).nCDPuOrNot = 0 Then
                sStime = AddLitterTime(TrainInf(nTrain).lAllStartTime)
                Zftime = GetZheFanTime(TrainInf(nTrain).SCheDiLeiXing, TrainInf(nTrain).ComeStation, TrainInf(nTrain).TrainReturnStyle(1))
                If sStime - sTime >= Zftime Then
                    Call SeekAllTrainInTwoTime(tmpShuZhu, sTime, sStime, TrainInf(nTrain).ComeStation, 1, nTrain, nTrain2)
                    Call SeekAllTrainInTwoTime(tmpShuZhu1, sTime, sStime, TrainInf(nTrain).ComeStation, 2, nTrain, nTrain2)


                    'If UBound(tmpShuZhu) <= 1 And UBound(tmpShuZhu1) <= 1 Then
                    '    SeekGouSatisfiedTrain = nTrain
                    'Else
                    '    SeekGouSatisfiedTrain = 0
                    'End If
                    'Exit For

                    If TrainInf(nTrain).TrainReturnStyle(1) = "վ���۷�" Then
                        If UBound(tmpShuZhu) <= 1 And UBound(tmpShuZhu1) <= 1 Then
                            SeekGouSatisfiedTrain = nTrain
                        Else
                            SeekGouSatisfiedTrain = 0
                        End If
                        Exit For
                    Else
                        If UBound(tmpShuZhu) <= 0 And UBound(tmpShuZhu1) <= 0 Then
                            SeekGouSatisfiedTrain = nTrain
                        Else
                            SeekGouSatisfiedTrain = 0
                        End If
                        Exit For
                    End If
                End If
            End If
        Next i
    End Function

    '������ʱ������Ϣ
    Public Sub SaveTmpCheDiInfor()
        'ReDim ChediMultiJLInfo(0)
        Dim i As Integer
        Dim j As Integer
        For i = 1 To UBound(ChediInfo)
            ReDim Preserve ChediMultiJLInfo(UBound(ChediMultiJLInfo) + 1)
            ReDim ChediMultiJLInfo(UBound(ChediMultiJLInfo)).nLinkTrain(0)
            For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                If TrainInf(ChediInfo(i).nLinkTrain(j)).Train <> "" Then
                    ReDim Preserve ChediMultiJLInfo(UBound(ChediMultiJLInfo)).nLinkTrain(UBound(ChediMultiJLInfo(UBound(ChediMultiJLInfo)).nLinkTrain) + 1)
                    ChediMultiJLInfo(UBound(ChediMultiJLInfo)).nLinkTrain(UBound(ChediMultiJLInfo(UBound(ChediMultiJLInfo)).nLinkTrain)) = ChediInfo(i).nLinkTrain(j)
                End If
            Next j
        Next i
    End Sub

    '��tmpChediinf()���뵽chediinfo()
    Public Sub InputtmpCDInformation()
        ReDim ChediInfo(0)
        Dim nFtrain As Integer
        Dim nTrain As Integer
        Dim nTnum As Integer
        Dim i As Integer
        Dim j As Integer
        For i = 1 To UBound(ChediMultiJLInfo)
            If UBound(ChediMultiJLInfo(i).nLinkTrain) > 0 Then
                ReDim Preserve ChediInfo(UBound(ChediInfo) + 1)
                nTnum = UBound(ChediInfo)
                Call CopyCheDiInformation(nTnum, Str(i))
                ChediInfo(nTnum).sCheCiHao = ChediMultiJLInfo(i).sCheCiHao
                ReDim Preserve ChediInfo(nTnum).nLinkTrain(UBound(ChediMultiJLInfo(i).nLinkTrain))
                nFtrain = 0
                For j = 1 To UBound(ChediMultiJLInfo(i).nLinkTrain)
                    nTrain = ChediMultiJLInfo(i).nLinkTrain(j)

                    If nFtrain = 0 Then
                        TrainInf(nTrain).TrainReturn(1) = 0
                        TrainInf(nTrain).TrainReturn(2) = 0

                    Else
                        TrainInf(nFtrain).TrainReturn(2) = nTrain
                        TrainInf(nTrain).TrainReturn(1) = nFtrain
                        TrainInf(nTrain).TrainReturn(2) = 0
                    End If
                    ChediInfo(nTnum).nLinkTrain(j) = nTrain
                    TrainInf(nTrain).nCheDiPuOrNot = 1
                    nFtrain = nTrain
                Next j
            End If
        Next i

    End Sub

    '������·�������ӳ�,���߽�·,�Թ��ߵ�һ����·Ϊ����������һ����·���м����
    Public Sub SetGongXianTrainTwo(ByVal sJLstyle As String, ByVal sJiaoLuStyle2 As String, ByVal sFanJLStyle2 As String, ByVal sDayBeTime As Long, ByVal sDayEndTime As Long, ByVal sStartSta As String, ByVal sEndSta As String, _
    ByVal sAnoStartSta As String, ByVal sAnoEndSta As String, ByVal sGongXianDownSta As String, ByVal sGongXianUpSta As String, ByVal sPuHuaFS As String, ByVal nBi1 As Integer, ByVal nBi2 As Integer, ByVal nDownId As Integer, ByVal nUpID As Integer, ByVal ProBar As ToolStripProgressBar)
        Dim i As Integer
        Dim tmpK As Integer
        Dim nTrain As Integer

        Dim sTime As Long

        tmpK = 0
        Dim sTmpFanJL As String
        Dim sFirInsertSta As String
        Dim tmpNum As Integer
        tmpNum = 0
        sFirInsertSta = sGongXianDownSta
        ProBar.Visible = True
        ProBar.Value = 0
        Call SetTmpAllTrainSeqByJiaoLuStartDown(sFirInsertSta) '������վ�ĵ�һ����վ��������
        ProBar.Maximum = UBound(nTmpAllTrainSeqDown)
        If sPuHuaFS = "�ȱ����̻�" Then
            tmpK = 0
            For i = 1 To UBound(nTmpAllTrainSeqDown) - 1
                nTrain = nTmpAllTrainSeqDown(i)
                If nTrain Mod 2 <> 0 Then '����
                    sTime = AddLitterTime(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                    If sTime >= sDayBeTime And sTime <= sDayEndTime Then
                        tmpNum = tmpNum + 1
                        If tmpNum >= nDownId Then
                            If nBi1 <= 1 Then
                                If nBi2 <> 0 Then
                                    sTmpFanJL = sJiaoLuStyle2 'GetCurFanJiaoLuStyleGongXian(nTrain, sJiaoLuStyle, sFanJLStyle)
                                    Call InsertOneTrainInGongXian(nTrain, nTmpAllTrainSeqDown(i + 1), 1, sFirInsertSta, sTmpFanJL)
                                    tmpK = tmpK + 1
                                End If
                            Else
                                If tmpK Mod nBi1 = 0 Then
                                    sTmpFanJL = sJiaoLuStyle2 'GetCurFanJiaoLuStyleGongXian(nTrain, sJiaoLuStyle, sFanJLStyle)
                                    Call InsertOneTrainInGongXian(nTrain, nTmpAllTrainSeqDown(i + 1), 1, sFirInsertSta, sTmpFanJL)
                                    'Else
                                    'sTmpFanJL = sJiaoLuStyle2 'GetCurFanJiaoLuStyleGongXian(nTrain, sJiaoLuStyle, sFanJLStyle)
                                    'Call InsertOneTrainInGongXian(nTrain, nTmpAllTrainSeqDown(i + 1), 1, sFirInsertSta, sTmpFanJL)
                                End If
                                tmpK = tmpK + 1
                            End If
                        End If
                    End If
                End If
                ProBar.Value = i
            Next i

            tmpK = 0
            tmpNum = 0
            Call SetTmpAllTrainSeqByJiaoLuStartUp(sGongXianUpSta) '������վ�ĵ�һ����վ��������
            ProBar.Maximum = UBound(nTmpAllTrainSeqDown)
            For i = 1 To UBound(nTmpAllTrainSeqDown) - 1
                nTrain = nTmpAllTrainSeqDown(i)
                If nTrain Mod 2 = 0 Then '����
                    sTime = AddLitterTime(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                    If sTime >= sDayBeTime And sTime <= sDayEndTime Then
                        tmpNum = tmpNum + 1
                        If tmpNum >= nUpID Then
                            If nBi1 <= 1 Then
                                If nBi2 <> 0 Then
                                    sTmpFanJL = sFanJLStyle2 'GetCurFanJiaoLuStyleGongXian(nTrain, sJiaoLuStyle, sFanJLStyle)
                                    Call InsertOneTrainInGongXian(nTrain, nTmpAllTrainSeqDown(i + 1), 1, sFirInsertSta, sTmpFanJL)
                                    tmpK = tmpK + 1
                                End If
                            Else
                                If tmpK Mod nBi1 = 0 Then
                                    sTmpFanJL = sFanJLStyle2 'GetCurFanJiaoLuStyleGongXian(nTrain, sJiaoLuStyle, sFanJLStyle)
                                    Call InsertOneTrainInGongXian(nTrain, nTmpAllTrainSeqDown(i + 1), 1, sFirInsertSta, sTmpFanJL)
                                End If
                                tmpK = tmpK + 1
                            End If
                        End If
                    End If
                End If
                ProBar.Value = i
            Next i
            ProBar.Visible = False

            '    For i = 1 To UBound(nTmpAllTrainSeqUp)
            '        nTrain = nTmpAllTrainSeqUp(i)
            '        If nTrain Mod 2 = 0 And TrainInf(nTrain).ComeStation = sEndSta Then '����
            '            sTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
            '            If sTime >= sDayBeTime And sTime <= sDayEndTime Then
            '                If nBi1 <= 1 Then
            '                    If nBi2 <> 0 Then
            '                        If tmpK Mod (nBi2 + 1) = 0 Then
            '                            sTmpFanJL = GetCurFanJiaoLuStyleGongXian(nTrain, sJiaoLuStyle, sFanJLStyle)
            '                            Call ResetChangerTwoGongXianTrain(nTrain, 2, sTmpFanJL)
            '                        Else
            '                            sTmpFanJL = GetCurFanJiaoLuStyleGongXian(nTrain, sJiaoLuStyle2, sFanJLStyle2)
            '                            Call ResetChangerTwoGongXianTrain(nTrain, 2, sTmpFanJL)
            '                        End If
            '                        tmpK = tmpK + 1
            '                    End If
            '                Else
            '                    If tmpK Mod (nBi1 + 1) <> 0 Then
            '                        sTmpFanJL = GetCurFanJiaoLuStyleGongXian(nTrain, sJiaoLuStyle, sFanJLStyle)
            '                        Call ResetChangerTwoGongXianTrain(nTrain, 2, sTmpFanJL)
            '                    Else
            '                        sTmpFanJL = GetCurFanJiaoLuStyleGongXian(nTrain, sJiaoLuStyle2, sFanJLStyle2)
            '                        Call ResetChangerTwoGongXianTrain(nTrain, 2, sTmpFanJL)
            '                    End If
            '                    tmpK = tmpK + 1
            '                End If
            '            End If
            '        End If
            '    Next i
        End If
    End Sub


    '��һ��·���߶β���
    Public Sub InsertOneTrainInGongXian(ByVal nTrain1 As Integer, ByVal nTrain2 As Integer, ByVal nUporDown As Integer, ByVal sStaName As String, ByVal sTrainStyle As String)
        Dim i As Integer
        Dim nBaseId As Integer
        Dim sBtime As Long
        Dim sRtime As Long
        'Dim sEtime As Long
        Dim nDaoFa As Integer
        Dim j As Integer
        'sBtime = TrainInf(nTrain).lAllStartTime
        'sEtime = TrainInf(nTrain).lAllEndTime
        nDaoFa = 0

        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).sJiaoLuName = sTrainStyle Then
                nBaseId = i
                Exit For
            End If
        Next i
        Dim NewTrain As Integer
        Dim tmpTime1 As Long
        Dim tmpTime2 As Long
        Dim tmpTime As Long
        'Dim sEndStation As String
        Dim Cdid As Integer
        If nBaseId > 0 Then
            '���ҵ����ߵĵ�һվ
            For j = 1 To UBound(TrainInf(nTrain1).nPathID)
                If StationInf(TrainInf(nTrain1).nPathID(j)).sStationName = sStaName Then
                    tmpTime1 = AddLitterTime(TrainInf(nTrain1).Starting(TrainInf(nTrain1).nPathID(j)))
                    Exit For
                End If
            Next j

            For j = 1 To UBound(TrainInf(nTrain2).nPathID)
                If StationInf(TrainInf(nTrain2).nPathID(j)).sStationName = sStaName Then
                    tmpTime2 = AddLitterTime(TrainInf(nTrain2).Starting(TrainInf(nTrain2).nPathID(j)))
                    Exit For
                End If
            Next j

            tmpTime = tmpTime1 + (tmpTime2 - tmpTime1) / 2

            For i = 1 To UBound(BasicTrainInf(nBaseId).nPathID)
                If StationInf(BasicTrainInf(nBaseId).nPathID(i)).sStationName = sStaName Then
                    ReDim Preserve TrainInf(UBound(TrainInf) + 2)
                    NewTrain = AddTrainInformation(sTrainStyle, TrainInf(nTrain1).sRunScaleName, TrainInf(nTrain1).sStopSclaeName, "")


                    ReDim Preserve ChediInfo(UBound(ChediInfo) + 1)
                    Cdid = UBound(ChediInfo)

                    Call CopyCheDiInformation(Cdid, Str(Cdid))
                    ChediInfo(Cdid).sCheCiHao = Cdid
                    TrainInf(NewTrain).SCheDiLeiXing = ChediInfo(Cdid).SCheDiLeiXing
                    ReDim ChediInfo(Cdid).nLinkTrain(0)
                    Call AddLianGuaCheCi(Cdid, NewTrain)

                    sRtime = CalTrainRunTimeFromTwoStation(TrainInf(NewTrain).sJiaoLuName, TrainInf(NewTrain).sRunScaleName, TrainInf(NewTrain).sStopSclaeName, TrainInf(NewTrain).ComeStation, sStaName)
                    sBtime = TimeMinus(tmpTime, sRtime)
                    TrainInf(NewTrain).lAllStartTime = sBtime
                    TrainInf(NewTrain).Starting(TrainInf(NewTrain).nPathID(1)) = sBtime
                    Call DrawSingleTrain(NewTrain, TrainInf(NewTrain).lAllStartTime, 0)
                    TrainInf(NewTrain).nCDPuOrNot = 0
                    Exit For
                End If
            Next i
        Else
            'MsgBox "�г���Ϣ��û�������·!"
        End If
    End Sub

    '�����е��г�����һ�������ﰴʱ��˳������,����nTmpAllTrainSeqDown������,ͨ�����߶εĵ�һ����վ�������г�,
    Private Sub SetTmpAllTrainSeqByJiaoLuStartUp(ByVal sStaName As String)
        Dim i As Integer
        Dim j As Integer
        Dim tmpTime() As Long
        ReDim tmpTime(0)
        Dim k, temp, Flag As Integer
        Dim tmpLong As Long
        Dim TempTime1, Temptime2 As Long
        ReDim nTmpAllTrainSeqDown(0)
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" And i Mod 2 = 0 Then
                For j = 1 To UBound(TrainInf(i).nPathID)
                    If StationInf(TrainInf(i).nPathID(j)).sStationName = sStaName Then
                        If TrainInf(i).Starting(TrainInf(i).nPathID(j)) > 0 Then
                            ReDim Preserve nTmpAllTrainSeqDown(UBound(nTmpAllTrainSeqDown) + 1)
                            nTmpAllTrainSeqDown(UBound(nTmpAllTrainSeqDown)) = i
                            ReDim Preserve tmpTime(UBound(tmpTime) + 1)
                            tmpTime(UBound(tmpTime)) = AddLitterTime(TrainInf(i).Starting(TrainInf(i).nPathID(j)))
                            Exit For
                        End If
                    End If
                Next j
            End If
        Next i
        '����ʱ������
        Flag = 1
        k = UBound(nTmpAllTrainSeqDown)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                TempTime1 = tmpTime(j)
                Temptime2 = tmpTime(j + 1)
                If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                    temp = nTmpAllTrainSeqDown(j)
                    nTmpAllTrainSeqDown(j) = nTmpAllTrainSeqDown(j + 1)
                    nTmpAllTrainSeqDown(j + 1) = temp
                    tmpLong = tmpTime(j)
                    tmpTime(j) = tmpTime(j + 1)
                    tmpTime(j + 1) = tmpLong
                    Flag = 1
                End If
            Next j
        Loop
    End Sub

    '�����е��г�����һ�������ﰴʱ��˳������,����nTmpAllTrainSeqDown������,ͨ�����߶εĵ�һ����վ�������г�,
    Private Sub SetTmpAllTrainSeqByJiaoLuStartDown(ByVal sStaName As String)
        Dim i As Integer
        Dim j As Integer
        Dim tmpTime() As Long
        ReDim tmpTime(0)
        Dim k, temp, Flag As Integer
        Dim tmpLong As Long
        Dim TempTime1, Temptime2 As Long
        ReDim nTmpAllTrainSeqDown(0)
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" And i Mod 2 <> 0 Then
                For j = 1 To UBound(TrainInf(i).nPathID)
                    If StationInf(TrainInf(i).nPathID(j)).sStationName = sStaName Then
                        If TrainInf(i).Starting(TrainInf(i).nPathID(j)) > 0 Then
                            ReDim Preserve nTmpAllTrainSeqDown(UBound(nTmpAllTrainSeqDown) + 1)
                            nTmpAllTrainSeqDown(UBound(nTmpAllTrainSeqDown)) = i
                            ReDim Preserve tmpTime(UBound(tmpTime) + 1)
                            tmpTime(UBound(tmpTime)) = AddLitterTime(TrainInf(i).Starting(TrainInf(i).nPathID(j)))
                            Exit For
                        End If
                    End If
                Next j
            End If
        Next i
        '����ʱ������
        Flag = 1
        k = UBound(nTmpAllTrainSeqDown)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                TempTime1 = tmpTime(j)
                Temptime2 = tmpTime(j + 1)
                If TempTime1 > Temptime2 Then 'TimeMinus(TempTime1, Temptime2) < TimeMinus(Temptime2, TempTime1) Then '
                    temp = nTmpAllTrainSeqDown(j)
                    nTmpAllTrainSeqDown(j) = nTmpAllTrainSeqDown(j + 1)
                    nTmpAllTrainSeqDown(j + 1) = temp
                    tmpLong = tmpTime(j)
                    tmpTime(j) = tmpTime(j + 1)
                    tmpTime(j + 1) = tmpLong
                    Flag = 1
                End If
            Next j
        Loop
    End Sub

    '���Խ�·��ʼ�����յ�վ
    Public Function GetJiaoLuStaName(ByVal sJiaoLuName As String, ByVal nStartOrEnd As Integer) As String
        Dim nID As Integer
        Dim j As Integer
        For j = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(j).sJiaoLuName = sJiaoLuName Then
                nID = j
                Exit For
            End If
        Next j

        If nStartOrEnd = 1 Then '��һվ
            GetJiaoLuStaName = BasicTrainInf(j).ComeStation
        Else
            GetJiaoLuStaName = BasicTrainInf(j).NextStation
        End If
    End Function
End Module
