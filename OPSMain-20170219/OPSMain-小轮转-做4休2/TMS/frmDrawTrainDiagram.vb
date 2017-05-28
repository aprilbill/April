Public Class frmDrawTrainDiagram
    Public CurEdit As String       '��ʾ��ǰ�Ŀؼ������ı�����������
    Public nCurJLID As Integer      '�ֹ��б�������ʾ��ǰ��GaoFenTimeSet()��ǰID��
    Dim CurCellX As Single
    Dim CurCellY As Single
    Dim CurMouseX As Single
    Dim CurMouseY As Single
    Dim sFirZFTime As Long
    Dim sEndZFTime As Long

    Private Sub frmDrawTrainDiagram_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        Dim j, p As Integer
        If UBound(BasicTrainInf) > 0 Then
            For i = 1 To UBound(BasicTrainInf)
                If BasicTrainInf(i).nUporDown = 1 Then
                    For p = 1 To UBound(StationInf)
                        If BasicTrainInf(i).ComeStation = StationInf(p).sStationName Then
                            Me.cmbTrainJLStyle.Items.Add(BasicTrainInf(i).sJiaoLuName)
                            Me.cmbSecJLStyle.Items.Add(BasicTrainInf(i).sJiaoLuName)
                            'Me.cmbGongXian1.Items.Add(BasicTrainInf(i).sJiaoLuName)
                            'Me.cmbGongXian2.Items.Add(BasicTrainInf(i).sJiaoLuName)
                            Me.cmbAnotGongXianJL.Items.Add(BasicTrainInf(i).sJiaoLuName)
                            Exit For
                        End If
                    Next p
                End If
            Next i
            If Me.cmbTrainJLStyle.Items.Count > 0 Then
                Me.cmbTrainJLStyle.Text = Me.cmbTrainJLStyle.Items(0)
                Me.cmbSecJLStyle.Text = Me.cmbSecJLStyle.Items(0)
                'Me.cmbGongXian1.Text = Me.cmbGongXian1.List(0)
                'Me.cmbGongXian2.Text = Me.cmbGongXian2.List(0)
                Me.cmbAnotGongXianJL.Text = Me.cmbAnotGongXianJL.Items(0)
            End If
        Else
            MsgBox("�г���Ϣ��û�н�·��Ϣ�������г���Ϣ��", , "��ʾ")
        End If

        Me.cmbPuHuaStyle.Items.Add("������")
        Me.cmbPuHuaStyle.Items.Add("˫����")
        Me.cmbPuHuaStyle.Items.Add("�ڼ���")
        Me.cmbPuHuaStyle.Items.Add("����")

        Me.cmbPuHuaStyle.Text = "������"

        sJiaoLuStyle = Trim(Me.cmbTrainJLStyle.Text)
        sPuHuaStyle = Trim(Me.cmbPuHuaStyle.Text)

        Call InputJGData(sJiaoLuStyle, sPuHuaStyle)

        Dim IfIn As Integer
        For i = 1 To UBound(StationInf)
            IfIn = 0
            If StationInf(i).sStaProperity = "�ֲ�վ" Then
                For j = 1 To Me.cmbGongDownFirSta.Items.Count
                    If Me.cmbGongDownFirSta.Items(j - 1) = StationInf(i).sStationName Then
                        IfIn = 1
                        Exit For
                    End If
                Next j
                If IfIn = 0 Then
                    Me.cmbGongDownFirSta.Items.Add(StationInf(i).sStationName)
                    Me.cmbGongXianUpFirSta.Items.Add(StationInf(i).sStationName)
                End If
            End If
        Next i

        If Me.cmbGongDownFirSta.Items.Count > 0 Then
            Me.cmbGongDownFirSta.Text = Me.cmbGongDownFirSta.Items(0)
            Me.cmbGongXianUpFirSta.Text = Me.cmbGongXianUpFirSta.Items(0)
        End If

    End Sub

    '����ʱ�������ݵ�GRD��
    Private Sub InputJGData(ByVal sJLstyle As String, ByVal sPHstyle As String)
        'Dim i As Integer
        'Dim j As Integer
        Call GetBaseStation(sJLstyle)

        If sFirstBaseSta = "" Then
            MsgBox("�ý�·���������г����������г���Ϣ����ӣ�", , "��ʾ")
            Exit Sub
        End If
        sFanJiaoLuStyle = sElseBaseSta & "-->" & sFirstBaseSta

        Call CallTimeSet(2)

        Call ListGrdTime()

        'Me.grdBiLi.Clear()
        'Me.grdBiLi.Rows = 1
        'Me.grdBiLi.Cols = 2
        'Me.grdBiLi.TextArray(0) = "ʱ���"
        'Me.grdBiLi.TextArray(1) = "���б���"

        'Me.grdBiLi.ColWidth(0) = TextWidth("��") * 5
        'Me.grdBiLi.ColWidth(1) = TextWidth("��") * 5

        'Me.grdBiLi.ColAlignment(0) = 4
        'Me.grdBiLi.ColAlignment(1) = 4

        'For i = 1 To Me.GrdTime.Rows - 1
        '    Me.grdBiLi.AddItem(Me.GrdTime.TextMatrix(i, 0) & Chr(9) & "1:1")
        'Next i

        ' Call showInformation(sJiaoLuStyle)

    End Sub

    '��ʾ����
    Private Sub ListGrdTime()
        Dim i As Integer
        Dim j As Integer
        'Me.grdTime.Columns.Clear()
        For i = 1 To UBound(GaoFenTimeSet)
            If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                Me.grdTime.RowCount = UBound(GaoFenTimeSet(i).nXuHao) + 1
                For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                    nCurJLID = i
                    Me.grdTime.Item(0, j - 1).Value = GaoFenTimeSet(i).nXuHao(j)
                    Me.grdTime.Item(1, j - 1).Value = dTime(DeleteLitterTime(GaoFenTimeSet(i).BeTime(j)), 5)
                    Me.grdTime.Item(2, j - 1).Value = dTime(DeleteLitterTime(GaoFenTimeSet(i).EndTime(j)), 5)
                    Me.grdTime.Item(3, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).lZhouQiTime(j))
                    Me.grdTime.Item(4, j - 1).Value = GaoFenTimeSet(i).sRunScaleName(j)
                    Me.grdTime.Item(5, j - 1).Value = GaoFenTimeSet(i).sStopScaleName(j)
                    Me.grdTime.Item(6, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).lFirZheFanTime(j))
                    Me.grdTime.Item(7, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).lEndZheFanTime(j))
                    Me.grdTime.Item(8, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).lDownRunTime(j))
                    Me.grdTime.Item(9, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).lDownStopTime(j))
                    Me.grdTime.Item(10, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).lUpRunTime(j))
                    Me.grdTime.Item(11, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).lUpStopTime(j))
                    Me.grdTime.Item(12, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).JGtime(j))
                    Me.grdTime.Item(13, j - 1).Value = GaoFenTimeSet(i).ChediNum(j)
                    Me.grdTime.Item(14, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).JGOne(j))
                    Me.grdTime.Item(15, j - 1).Value = GaoFenTimeSet(i).NumOne(j)
                    Me.grdTime.Item(16, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).JGTwo(j))
                    Me.grdTime.Item(17, j - 1).Value = GaoFenTimeSet(i).NumTwo(j)
                Next j
                Exit For
            End If
        Next i
    End Sub

    '����ÿ�����ʱ�������ֵ
    Private Sub CallTimeSet(ByVal nState As Integer)

        Dim i As Integer
        Dim j, k As Integer
        Dim sFanJLName As String
        sFanJLName = sElseBaseSta & "-->" & sFirstBaseSta
        Select Case nState

            Case 1 '�����������۷�ʱ��
                For i = 1 To UBound(GaoFenTimeSet)
                    If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                        For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                            GaoFenTimeSet(i).lDownRunTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                            GaoFenTimeSet(i).lUpRunTime(j) = CalTrainRunTimeNotStopFromTrain(sFanJLName, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                            GaoFenTimeSet(i).lDownStopTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                            GaoFenTimeSet(i).lUpStopTime(j) = CalTrainRunTimeNotStopFromTrain(sFanJLName, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                            GaoFenTimeSet(i).lFirZheFanTime(j) = (GaoFenTimeSet(i).lZhouQiTime(j) - GaoFenTimeSet(i).lDownRunTime(j) _
                                                                    - GaoFenTimeSet(i).lUpRunTime(j) - GaoFenTimeSet(i).lDownStopTime(j) _
                                                                    - GaoFenTimeSet(i).lUpStopTime(j)) / 2
                            GaoFenTimeSet(i).lEndZheFanTime(j) = GaoFenTimeSet(i).lZhouQiTime(j) - GaoFenTimeSet(i).lDownRunTime(j) _
                                                                    - GaoFenTimeSet(i).lUpRunTime(j) - GaoFenTimeSet(i).lDownStopTime(j) _
                                                                    - GaoFenTimeSet(i).lUpStopTime(j) - GaoFenTimeSet(i).lFirZheFanTime(j)
                        Next j

                        Exit For
                    End If
                Next i

            Case 2 '�����۷�ʱ��������

                For i = 1 To UBound(GaoFenTimeSet)
                    If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then

                        '���ݲ�ȫ��ʱ��

                        If GaoFenTimeSet(i).sRunScaleName(1) = "" Then
                            For j = 1 To UBound(BasicTrainInf)
                                If BasicTrainInf(j).sJiaoLuName = Me.cmbTrainJLStyle.Text.Trim Then
                                    If UBound(BasicTrainInf(j).SecScale) > 0 Then
                                        For k = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                            GaoFenTimeSet(i).sRunScaleName(k) = BasicTrainInf(j).SecScale(1).sName
                                        Next
                                        Exit For
                                    End If
                                End If
                            Next
                        End If
                        If GaoFenTimeSet(i).sStopScaleName(1) = "" Then
                            For j = 1 To UBound(BasicTrainInf)
                                If BasicTrainInf(j).sJiaoLuName = Me.cmbTrainJLStyle.Text Then
                                    If UBound(BasicTrainInf(j).StopScale) > 0 Then
                                        For k = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                            GaoFenTimeSet(i).sStopScaleName(k) = BasicTrainInf(j).StopScale(1).sName
                                        Next
                                        Exit For
                                    End If
                                End If
                            Next j
                        End If
                        If GaoFenTimeSet(i).lFirZheFanTime(1) = 0 Then
                            For k = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                GaoFenTimeSet(i).lFirZheFanTime(k) = sFirZFTime
                                GaoFenTimeSet(i).lEndZheFanTime(k) = sEndZFTime
                            Next
                        End If

                        For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                            GaoFenTimeSet(i).lDownRunTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                            GaoFenTimeSet(i).lUpRunTime(j) = CalTrainRunTimeNotStopFromTrain(sFanJLName, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                            GaoFenTimeSet(i).lDownStopTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                            GaoFenTimeSet(i).lUpStopTime(j) = CalTrainRunTimeNotStopFromTrain(sFanJLName, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                            GaoFenTimeSet(i).lZhouQiTime(j) = GaoFenTimeSet(i).lDownRunTime(j) _
                                                             + GaoFenTimeSet(i).lUpRunTime(j) _
                                                             + GaoFenTimeSet(i).lDownStopTime(j) _
                                                             + GaoFenTimeSet(i).lUpStopTime(j) _
                                                             + GaoFenTimeSet(i).lFirZheFanTime(j) _
                                                             + GaoFenTimeSet(i).lEndZheFanTime(j)
                        Next j
                        Exit For
                    End If
                Next i

        End Select
    End Sub



    '�õ���һ����׼վ������
    Private Sub GetBaseStation(ByVal sJLstyle As String)
        Dim j As Integer
        For j = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(j).sJiaoLuName = sJLstyle Then
                sFirstBaseSta = BasicTrainInf(j).ComeStation
                sElseBaseSta = BasicTrainInf(j).NextStation
                sFirZFTime = GetZheFanTime(BasicTrainInf(j).SCheDiLeiXing, sFirstBaseSta, BasicTrainInf(j).TrainReturnStyle(1))
                sEndZFTime = GetZheFanTime(BasicTrainInf(j).SCheDiLeiXing, sElseBaseSta, BasicTrainInf(j).TrainReturnStyle(2))
                Me.labZheFanTime.Text = "��·���ͣ�" & BasicTrainInf(j).TrainStyle & "  ��С�۷�ʱ�䣺" & sFirstBaseSta & ": " & SecondToMinute(sFirZFTime) & _
                                           "  " & sElseBaseSta & ": " & SecondToMinute(sEndZFTime)
                Exit For
            End If
        Next j

    End Sub

    Private Sub grdTime_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTime.CellClick
        Dim i As Integer
        Dim j As Integer
        If Me.grdTime.CurrentCell.RowIndex >= 0 And Me.grdTime.Columns(Me.grdTime.CurrentCell.ColumnIndex).Name = "���б��" Then
            If CurCellX > 0 And CurCellY > 0 Then

                Me.cmbEditTime.Left = CurMouseX - CurCellX + Me.grdTime.Left
                Me.cmbEditTime.Top = CurMouseY - CurCellY + Me.grdTime.Top
                Me.cmbEditTime.Width = Me.grdTime.Columns(Me.grdTime.CurrentCell.ColumnIndex).Width
                Me.cmbEditTime.Items.Clear()

                'For i = 1 To UBound(TrainRunScaleInf)
                '    Me.cmbEditTime.Items.Add(TrainRunScaleInf(i).sScaleName)
                'Next

                For i = 1 To UBound(BasicTrainInf)
                    If BasicTrainInf(i).sJiaoLuName = Me.cmbTrainJLStyle.Text.Trim Then
                        For j = 1 To UBound(BasicTrainInf(i).SecScale)
                            Me.cmbEditTime.Items.Add(BasicTrainInf(i).SecScale(j).sName)
                        Next
                    End If
                Next

                Me.cmbEditTime.Text = Me.grdTime.CurrentCell.Value
                Me.cmbEditTime.Visible = True
                Me.cmbEditTime.Select()
                CurCellX = 0
                CurCellY = 0
            End If
        End If

        If Me.grdTime.CurrentCell.RowIndex >= 0 And Me.grdTime.Columns(Me.grdTime.CurrentCell.ColumnIndex).Name = "ͣվ���" Then
            If CurCellX > 0 And CurCellY > 0 Then

                Me.cmbEditTime.Left = CurMouseX - CurCellX + Me.grdTime.Left
                Me.cmbEditTime.Top = CurMouseY - CurCellY + Me.grdTime.Top
                Me.cmbEditTime.Width = Me.grdTime.Columns(Me.grdTime.CurrentCell.ColumnIndex).Width
                Me.cmbEditTime.Items.Clear()
                For i = 1 To UBound(BasicTrainInf)
                    If BasicTrainInf(i).sJiaoLuName = Me.cmbTrainJLStyle.Text Then
                        If UBound(BasicTrainInf(i).StopScale) > 0 Then
                            For j = 1 To UBound(BasicTrainInf(i).StopScale)
                                Me.cmbEditTime.Items.Add(BasicTrainInf(i).StopScale(j).sName)
                            Next j
                            cmbEditTime.Text = Me.grdTime.CurrentCell.Value

                        Else
                            'MsgBox("���г���·��Ϣ��û�ж���ͣվ�����Ϣ�������г���Ϣ��")
                            'Exit Sub
                        End If

                        Exit For
                    End If
                Next i
                Me.cmbEditTime.Select()
                Me.cmbEditTime.Visible = True
                CurCellX = 0
                CurCellY = 0
            End If
        End If
    End Sub

    Private Sub grdTime_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTime.CellEndEdit
        Call ValueChange()
    End Sub

    Private Sub grdTime_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdTime.CellMouseDown
        Me.cmbEditTime.Visible = False
        CurCellX = e.X
        CurCellY = e.Y
    End Sub

    Private Sub grdTime_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdTime.MouseDown

        CurMouseX = e.X
        CurMouseY = e.Y
    End Sub

    Private Sub cmbEditTime_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEditTime.SelectedValueChanged
        Me.grdTime.CurrentCell.Value = Me.cmbEditTime.Text
        Call ValueChange()
        Me.cmbEditTime.Visible = False
    End Sub

    Private Sub cmdShowFangA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowFangA.Click

        'Call GrdTime_LeaveCell()
        ' frmTimeTableMain.Լ���̻�YToolStripMenuItem.Checked = False
        '�ж��̻�����
        Dim i As Integer
        Dim nId As Integer = 0
        For i = 1 To UBound(GaoFenTimeSet)
            If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                nId = i
                Exit For
            End If
        Next i

        If UBound(GaoFenTimeSet(nId).JGtime) > 1 Then
            If GaoFenTimeSet(nId).JGtime(1) < GaoFenTimeSet(nId).JGtime(2) Then
                MsgBox("ϵͳ��֧�ָ÷������̻�����һʱ��μ��ʱ��С�ڵڶ�ʱ��μ��", , "��ʾ")
                Exit Sub
            End If
        End If

        For i = 1 To UBound(GaoFenTimeSet(nId).JGtime)
            If GaoFenTimeSet(nId).EndTime(i) - GaoFenTimeSet(nId).BeTime(i) < GaoFenTimeSet(nId).lZhouQiTime(i) + GaoFenTimeSet(nId).lFirZheFanTime(i) + GaoFenTimeSet(nId).lEndZheFanTime(i) Then
                MsgBox("ϵͳ�޷��̻��÷��������ڵ�" & i & "��ʱ�̶ο��ʱ��С��ȫ��ת���������۷�ʱ�䣡���޸ĸ�ʱ��ε�ʱ���ȣ�")
                Exit Sub
            End If
        Next
        TimeTablePara.sPubTrainStrainDraw = TrainStrainDraw.��Լ��
        TimeTablePara.nPubTrain = 0
        TimeTablePara.nPubCheDi = 0
        TimeTablePara.sPubCurSkbName = "δ����ʱ�̱�"
        'Call showInformation(sJiaoLuStyle)
        Call InputFirstCan(sJiaoLuStyle)
        Call GZLineUseSet()
        Call SetAllTrainNotGouHua()

        Me.StatusLabel.Text = "�����̻�"
        Me.ProBar.Visible = True
        Call AllChediGaoFengBackGouHua(sJiaoLuStyle, sFanJiaoLuStyle, sPuHuaStyle, Me.ProBar)

        Me.ProBar.Visible = False
        Me.StatusLabel.Text = "����ͼ�̻�"
        Call ResetPrintTrainString()
        Call addOneUndoInf()
        Call RefreshDiagram(1)
        Call ShowAllErrorInfor()
    End Sub


    '�Ƚ������г���Ϊû�й���,������Ϊ����
    Private Sub SetAllTrainNotGouHua()
        Dim i As Integer
        ReDim ChediMultiJLInfo(0)
        ReDim ChediMultiJLInfo(0).nLinkTrain(0)

        ReDim tmpCheDiInf(0)
        ReDim tmpCheDiInf(0).nLinkTrain(0)
        ReDim ChediInfo(0)
        For i = 1 To UBound(BaseChediInfo)
            BaseChediInfo(i).bIfGouWang = 0
        Next i
        ReDim TrainInf(0)
        ReDim nAllTrainSeq(0)
        ReDim ErrorInf(0)
        ReDim TrainErrInf(0)
    End Sub

    '�жϳ��������Ƿ��㹻
    Private Function IFCheDiNumSatify() As Boolean
        Dim i As Integer
        IFCheDiNumSatify = True
        For i = 1 To Me.grdTime.Rows.Count - 1
            If Me.grdTime.Rows(i - 1).Cells(13).Value > UBound(ChediInfo) Then
                IFCheDiNumSatify = False
                Exit For
            End If
        Next
    End Function

    Public Sub GZLineUseSet()
        Dim i As Integer
        Dim Tnum As Integer
        'Call GrdTime_LeaveCell
        '    For i = 1 To Me.GrdTime.Rows - 1
        '        sTime = MinuteToSecond(Me.GrdTime.TextMatrix(i, 12))
        '        If sTime < StationInf(i).IKK(1) Then
        '            MsgBox "�������ʱ��С��׷�ټ��ʱ�䣨" & StationInf(i).IKK(1) & "��),���޸�!!"
        '            Me.GrdTime.Row = i
        '            Me.GrdTime.Col = 4
        ''            Call GrdTime_DblClick
        '            Exit Sub
        '        End If
        '        If AddLitterTime(HourToSecond(Me.GrdTime.TextMatrix(i, 1))) > AddLitterTime(HourToSecond(Me.GrdTime.TextMatrix(i, 2))) Then
        '            MsgBox "ʱ�����ֹʱ�䲻������ʼʱ��,���޸�!!"
        '            Me.GrdTime.Row = i
        '            Me.GrdTime.Col = 3
        '            Exit Sub
        '        End If
        '    Next i

        Tnum = Me.grdTime.RowCount
        Dim CurId As Integer
        'Dim IfExist As Inter
        For i = 1 To UBound(GaoFenTimeSet)
            If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                CurId = i
                Exit For
            End If
        Next i

        If CurId = 0 Then
            CurId = UBound(GaoFenTimeSet) + 1
            ReDim Preserve GaoFenTimeSet(CurId)
        End If


        ReDim GaoFenTimeSet(CurId).nXuHao(0)
        ReDim GaoFenTimeSet(CurId).BeTime(0)
        ReDim GaoFenTimeSet(CurId).EndTime(0)
        ReDim GaoFenTimeSet(CurId).JGtime(0)
        ReDim GaoFenTimeSet(CurId).ChediNum(0)
        ReDim GaoFenTimeSet(CurId).JGOne(0)
        ReDim GaoFenTimeSet(CurId).JGTwo(0)
        ReDim GaoFenTimeSet(CurId).NumOne(0)
        ReDim GaoFenTimeSet(CurId).NumTwo(0)
        ReDim GaoFenTimeSet(CurId).lZhouQiTime(0)
        ReDim GaoFenTimeSet(CurId).sRunScaleName(0)
        ReDim GaoFenTimeSet(CurId).sStopScaleName(0)
        ReDim GaoFenTimeSet(CurId).lFirZheFanTime(0)
        ReDim GaoFenTimeSet(CurId).lEndZheFanTime(0)
        ReDim GaoFenTimeSet(CurId).lDownRunTime(0)
        ReDim GaoFenTimeSet(CurId).lDownStopTime(0)
        ReDim GaoFenTimeSet(CurId).lUpRunTime(0)
        ReDim GaoFenTimeSet(CurId).lUpStopTime(0)


        For i = 0 To Tnum - 2
            GaoFenTimeSet(CurId).sJLstyle = sJiaoLuStyle
            GaoFenTimeSet(CurId).sPuHuaStyle = sPuHuaStyle

            ReDim Preserve GaoFenTimeSet(CurId).nXuHao(UBound(GaoFenTimeSet(CurId).nXuHao) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).BeTime(UBound(GaoFenTimeSet(CurId).BeTime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).EndTime(UBound(GaoFenTimeSet(CurId).EndTime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).JGtime(UBound(GaoFenTimeSet(CurId).JGtime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).ChediNum(UBound(GaoFenTimeSet(CurId).ChediNum) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).JGOne(UBound(GaoFenTimeSet(CurId).JGOne) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).JGTwo(UBound(GaoFenTimeSet(CurId).JGTwo) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).NumOne(UBound(GaoFenTimeSet(CurId).NumOne) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).NumTwo(UBound(GaoFenTimeSet(CurId).NumTwo) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).lZhouQiTime(UBound(GaoFenTimeSet(CurId).lZhouQiTime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).sRunScaleName(UBound(GaoFenTimeSet(CurId).sRunScaleName) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).sStopScaleName(UBound(GaoFenTimeSet(CurId).sStopScaleName) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).lFirZheFanTime(UBound(GaoFenTimeSet(CurId).lFirZheFanTime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).lEndZheFanTime(UBound(GaoFenTimeSet(CurId).lEndZheFanTime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).lDownRunTime(UBound(GaoFenTimeSet(CurId).lDownRunTime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).lDownStopTime(UBound(GaoFenTimeSet(CurId).lDownStopTime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).lUpRunTime(UBound(GaoFenTimeSet(CurId).lUpRunTime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).lUpStopTime(UBound(GaoFenTimeSet(CurId).lUpStopTime) + 1)

            GaoFenTimeSet(CurId).nXuHao(UBound(GaoFenTimeSet(CurId).nXuHao)) = Me.grdTime.Item(0, i).Value
            GaoFenTimeSet(CurId).BeTime(UBound(GaoFenTimeSet(CurId).BeTime)) = AddLitterTime(HourToSecond(Me.grdTime.Item(1, i).Value))
            GaoFenTimeSet(CurId).EndTime(UBound(GaoFenTimeSet(CurId).EndTime)) = AddLitterTime(HourToSecond(Me.grdTime.Item(2, i).Value))

            GaoFenTimeSet(CurId).lZhouQiTime(UBound(GaoFenTimeSet(CurId).lZhouQiTime)) = MinuteToSecond(Me.grdTime.Item(3, i).Value)
            GaoFenTimeSet(CurId).sRunScaleName(UBound(GaoFenTimeSet(CurId).sRunScaleName)) = Me.grdTime.Item(4, i).Value
            GaoFenTimeSet(CurId).sStopScaleName(UBound(GaoFenTimeSet(CurId).sStopScaleName)) = Me.grdTime.Item(5, i).Value
            GaoFenTimeSet(CurId).lFirZheFanTime(UBound(GaoFenTimeSet(CurId).lFirZheFanTime)) = MinuteToSecond(Me.grdTime.Item(6, i).Value)
            GaoFenTimeSet(CurId).lEndZheFanTime(UBound(GaoFenTimeSet(CurId).lEndZheFanTime)) = MinuteToSecond(Me.grdTime.Item(7, i).Value)
            GaoFenTimeSet(CurId).lDownRunTime(UBound(GaoFenTimeSet(CurId).lDownRunTime)) = MinuteToSecond(Me.grdTime.Item(8, i).Value)
            GaoFenTimeSet(CurId).lDownStopTime(UBound(GaoFenTimeSet(CurId).lDownStopTime)) = MinuteToSecond(Me.grdTime.Item(9, i).Value)
            GaoFenTimeSet(CurId).lUpRunTime(UBound(GaoFenTimeSet(CurId).lUpRunTime)) = MinuteToSecond(Me.grdTime.Item(10, i).Value)
            GaoFenTimeSet(CurId).lUpStopTime(UBound(GaoFenTimeSet(CurId).lUpStopTime)) = MinuteToSecond(Me.grdTime.Item(11, i).Value)

            GaoFenTimeSet(CurId).JGtime(UBound(GaoFenTimeSet(CurId).JGtime)) = MinuteToSecond(Me.grdTime.Item(12, i).Value)
            GaoFenTimeSet(CurId).ChediNum(UBound(GaoFenTimeSet(CurId).ChediNum)) = Val(Me.grdTime.Item(13, i).Value)
            GaoFenTimeSet(CurId).JGOne(UBound(GaoFenTimeSet(CurId).JGOne)) = MinuteToSecond(Me.grdTime.Item(14, i).Value)
            GaoFenTimeSet(CurId).NumOne(UBound(GaoFenTimeSet(CurId).NumOne)) = Val(Me.grdTime.Item(15, i).Value)
            GaoFenTimeSet(CurId).JGTwo(UBound(GaoFenTimeSet(CurId).JGTwo)) = MinuteToSecond(Me.grdTime.Item(16, i).Value)
            GaoFenTimeSet(CurId).NumTwo(UBound(GaoFenTimeSet(CurId).NumTwo)) = Val(Me.grdTime.Item(17, i).Value)

        Next i

        'Call ShowCharPic()

    End Sub


    Private Sub InputFirstCan(ByVal sJLstyle As String)

        sYXTJiaoLuStyle = sJiaoLuStyle
        sYXTPuHuaStyle = sPuHuaStyle
        BaseTimeMinus = 0
        ElseTimeMinus = 0

        ReDim BasePrepTrainset.nTrain(0)
        ReDim BasePrepTrainset.sCdid(0)

        ReDim ElsePrepTrainset.nTrain(0)
        ReDim ElsePrepTrainset.sCdid(0)

        Call SetChuRuKuState(sJLstyle)

        If Me.chkAutoAddChuKu.Checked = True Then
            TimeTablePara.bIFAutoAddChuKuTrain = True
        Else
            TimeTablePara.bIFAutoAddChuKuTrain = False
        End If

        If Me.chkAutoAddRuKu.Checked = True Then
            TimeTablePara.bIFAutoAddRuKuTrain = True
        Else
            TimeTablePara.bIFAutoAddRuKuTrain = False
        End If
    End Sub


    '���ó�������
    Private Sub SetChuRuKuState(ByVal sJLstyle As String)
        Dim i As Integer

        sChuKuState = 0
        sRuKuState = 0
        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).TrainStyle = "���⳵" Then
                If BasicTrainInf(i).NextStation = sElseBaseSta And BasicTrainInf(i).ComeStation = "����" Then
                    If i Mod 2 = 0 Then
                        sChuKuState = 1
                        Exit For
                    End If

                End If
            End If
        Next i
        For i = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(i).TrainStyle = "��⳵" Then
                If BasicTrainInf(i).ComeStation = sElseBaseSta And BasicTrainInf(i).NextStation = "����" Then
                    If i Mod 2 <> 0 Then
                        sRuKuState = 1
                        Exit For
                    End If

                End If
            End If
        Next i

        nIfBaseChuKu = IfBaseChuRuKuTrain(sFirstBaseSta, "���⳵", sJLstyle)
        nIfElseChuKu = IfBaseChuRuKuTrain(sElseBaseSta, "���⳵", sJLstyle)
        nIfBaseRuKu = IfBaseChuRuKuTrain(sFirstBaseSta, "��⳵", sJLstyle)
        nIfElseRuKu = IfBaseChuRuKuTrain(sElseBaseSta, "��⳵", sJLstyle)

    End Sub

    '����Ƿ���ڵ���׼վ�ĳ��⳵����Ϊ1��û��Ϊ0
    Private Function IfBaseChuRuKuTrain(ByVal sStaName As String, ByVal SChuRuKu As String, ByVal sJLstyle As String) As Integer
        Dim i As Integer
        Dim nBaseId As Integer

        IfBaseChuRuKuTrain = 0
        If SChuRuKu = "���⳵" Then
            For i = 1 To UBound(BasicTrainInf)
                If BasicTrainInf(i).TrainStyle = SChuRuKu And BasicTrainInf(i).NextStation = sStaName Then    'And BasicTrainInf(i).ComeStation = "����" Then
                    nBaseId = i
                    Exit For
                End If
            Next i
        ElseIf SChuRuKu = "��⳵" Then
            For i = 1 To UBound(BasicTrainInf)
                If BasicTrainInf(i).TrainStyle = SChuRuKu And BasicTrainInf(i).ComeStation = sStaName Then  'And BaseTrainInf(i).NextStation = "����" Then
                    nBaseId = i
                    Exit For
                End If
            Next i
        Else
            MsgBox("�������ô��󣡣�", , "��ʾ")
            Stop
        End If

        If nBaseId = 0 Then '�����ڸý�·
            IfBaseChuRuKuTrain = 0
        Else
            IfBaseChuRuKuTrain = 1
        End If
    End Function

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub cmbTrainJLStyle_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTrainJLStyle.SelectedValueChanged
        sJiaoLuStyle = Trim(Me.cmbTrainJLStyle.Text)
        sPuHuaStyle = Trim(Me.cmbPuHuaStyle.Text)
        Me.grdTime.Rows.Clear()
        Call InputJGData(sJiaoLuStyle, sPuHuaStyle)
    End Sub



    Private Sub cmbPuHuaStyle_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPuHuaStyle.SelectedValueChanged
        sJiaoLuStyle = Trim(Me.cmbTrainJLStyle.Text)
        sPuHuaStyle = Trim(Me.cmbPuHuaStyle.Text)
        Me.grdTime.Rows.Clear()
        Call InputJGData(sJiaoLuStyle, sPuHuaStyle)
    End Sub

    Private Sub btnSaveFangAn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFangAn.Click

        Call GZLineUseSet()
        If MsgBox("��Ϣ�Ѿ����棬�Ƿ��޸ĺ����Ϣ���浽�������ݿ���?", vbQuestion + vbOKCancel + vbDefaultButton2, "ȷ�ϲ���") = vbOK Then
            Call InputToDatabase()
        End If
    End Sub

    Private Sub InputToDatabase()
        Dim DBChediJiche As dao.Database
        Dim RSdata As dao.Recordset
        Dim i, j As Integer
        Dim Tnum As Integer
        Dim myWS As dao.Workspace
        Dim DE As dao.DBEngine = New dao.DBEngine

        myWS = DE.Workspaces(0)
        DBChediJiche = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        RSdata = DBChediJiche.OpenRecordset("select * from �����������")
        If RSdata.RecordCount > 0 Then
            RSdata.MoveLast()
            Tnum = RSdata.RecordCount
        Else
            Tnum = 0
        End If
        If Tnum > 0 Then
            DBChediJiche.Execute("delete * from �����������")
        End If
        DBChediJiche.Close()
        DBChediJiche = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        RSdata = DBChediJiche.OpenRecordset("�����������")

        For i = 1 To UBound(GaoFenTimeSet)
            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                RSdata.AddNew()
                RSdata.Fields("��·����").Value = GaoFenTimeSet(i).sJLstyle
                RSdata.Fields("�̻�����").Value = GaoFenTimeSet(i).sPuHuaStyle
                RSdata.Fields("ʱ���").Value = GaoFenTimeSet(i).nXuHao(j)
                RSdata.Fields("��ʼʱ��").Value = dTime(DeleteLitterTime(GaoFenTimeSet(i).BeTime(j)), 5)
                RSdata.Fields("��ֹʱ��").Value = dTime(DeleteLitterTime(GaoFenTimeSet(i).EndTime(j)), 5)
                RSdata.Fields("�������").Value = SecondToMinute(GaoFenTimeSet(i).JGtime(j))
                RSdata.Fields("��������").Value = GaoFenTimeSet(i).ChediNum(j)
                RSdata.Fields("���һ").Value = SecondToMinute(GaoFenTimeSet(i).JGOne(j))
                RSdata.Fields("�����").Value = SecondToMinute(GaoFenTimeSet(i).JGTwo(j))
                RSdata.Fields("����һ").Value = GaoFenTimeSet(i).NumOne(j)
                RSdata.Fields("������").Value = GaoFenTimeSet(i).NumTwo(j)
                RSdata.Fields("����ʱ��").Value = Val(GaoFenTimeSet(i).lZhouQiTime(j))
                RSdata.Fields("���б��").Value = GaoFenTimeSet(i).sRunScaleName(j)
                RSdata.Fields("ͣվ���").Value = GaoFenTimeSet(i).sStopScaleName(j)
                RSdata.Fields("ʼ���۷�").Value = Val(GaoFenTimeSet(i).lFirZheFanTime(j))
                RSdata.Fields("�յ��۷�").Value = Val(GaoFenTimeSet(i).lEndZheFanTime(j))
                RSdata.Update()
            Next j
        Next i

        RSdata.Close()
        DBChediJiche.Close()
        MsgBox("�������!", , "��ʾ")
    End Sub

    Private Sub ValueChange()
        Dim i, j As Integer
        Dim Time1, Time2 As Long
        Call GZLineUseSet()
        Dim sNowValue As String
        sNowValue = Me.grdTime.Columns(Me.grdTime.CurrentCell.ColumnIndex).Name

        Select Case sNowValue
            Case "��������"

                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then

                                    GaoFenTimeSet(i).lZhouQiTime(j) = MinuteToSecond(Me.grdTime.CurrentCell.Value)
                                    GaoFenTimeSet(i).lDownRunTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lUpRunTime(j) = CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lDownStopTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lUpStopTime(j) = CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lFirZheFanTime(j) = (GaoFenTimeSet(i).lZhouQiTime(j) - GaoFenTimeSet(i).lDownRunTime(j) _
                                                                            - GaoFenTimeSet(i).lUpRunTime(j) - GaoFenTimeSet(i).lDownStopTime(j) _
                                                                            - GaoFenTimeSet(i).lUpStopTime(j)) / 2
                                    GaoFenTimeSet(i).lEndZheFanTime(j) = GaoFenTimeSet(i).lZhouQiTime(j) - GaoFenTimeSet(i).lDownRunTime(j) _
                                                                            - GaoFenTimeSet(i).lUpRunTime(j) - GaoFenTimeSet(i).lDownStopTime(j) _
                                                                            - GaoFenTimeSet(i).lUpStopTime(j) - GaoFenTimeSet(i).lFirZheFanTime(j)
                                    Exit For
                                End If

                            Next j

                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If

            Case "���б��"

                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then
                                    GaoFenTimeSet(i).sRunScaleName(j) = Me.grdTime.Item(4, Me.grdTime.CurrentCell.RowIndex).Value
                                    GaoFenTimeSet(i).lDownRunTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lUpRunTime(j) = CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lDownStopTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lUpStopTime(j) = CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lZhouQiTime(j) = GaoFenTimeSet(i).lFirZheFanTime(j) _
                                                                      + GaoFenTimeSet(i).lEndZheFanTime(j) _
                                                                      + GaoFenTimeSet(i).lDownRunTime(j) _
                                                                      + GaoFenTimeSet(i).lUpRunTime(j) _
                                                                      + GaoFenTimeSet(i).lDownStopTime(j) _
                                                                      + GaoFenTimeSet(i).lUpStopTime(j)
                                    Exit For
                                End If

                            Next j

                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If

            Case "ͣվ���"
                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then
                                    GaoFenTimeSet(i).sStopScaleName(j) = Me.grdTime.Item(5, Me.grdTime.CurrentCell.RowIndex).Value
                                    GaoFenTimeSet(i).lDownRunTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lUpRunTime(j) = CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lDownStopTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lUpStopTime(j) = CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lZhouQiTime(j) = GaoFenTimeSet(i).lFirZheFanTime(j) _
                                                                      + GaoFenTimeSet(i).lEndZheFanTime(j) _
                                                                      + GaoFenTimeSet(i).lDownRunTime(j) _
                                                                      + GaoFenTimeSet(i).lUpRunTime(j) _
                                                                      + GaoFenTimeSet(i).lDownStopTime(j) _
                                                                      + GaoFenTimeSet(i).lUpStopTime(j)
                                    Exit For
                                End If

                            Next j

                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If

            Case "ʼ���۷�"

                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then
                                    GaoFenTimeSet(i).lDownRunTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lUpRunTime(j) = CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lDownStopTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lUpStopTime(j) = CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lFirZheFanTime(j) = MinuteToSecond(Me.grdTime.Item(6, Me.grdTime.CurrentCell.RowIndex).Value)
                                    GaoFenTimeSet(i).lZhouQiTime(j) = GaoFenTimeSet(i).lFirZheFanTime(j) _
                                                                      + GaoFenTimeSet(i).lEndZheFanTime(j) _
                                                                      + GaoFenTimeSet(i).lDownRunTime(j) _
                                                                      + GaoFenTimeSet(i).lUpRunTime(j) _
                                                                      + GaoFenTimeSet(i).lDownStopTime(j) _
                                                                      + GaoFenTimeSet(i).lUpStopTime(j)

                                    Exit For
                                End If

                            Next j

                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If

            Case "�յ��۷�"
                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then
                                    GaoFenTimeSet(i).lDownRunTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lUpRunTime(j) = CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lDownStopTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lUpStopTime(j) = CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lEndZheFanTime(j) = MinuteToSecond(Me.grdTime.Item(7, Me.grdTime.CurrentCell.RowIndex).Value)
                                    GaoFenTimeSet(i).lZhouQiTime(j) = GaoFenTimeSet(i).lFirZheFanTime(j) _
                                                                      + GaoFenTimeSet(i).lEndZheFanTime(j) _
                                                                      + GaoFenTimeSet(i).lDownRunTime(j) _
                                                                      + GaoFenTimeSet(i).lUpRunTime(j) _
                                                                      + GaoFenTimeSet(i).lDownStopTime(j) _
                                                                      + GaoFenTimeSet(i).lUpStopTime(j)

                                    Exit For
                                End If

                            Next j

                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If


            Case "�������"

                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then
                                    GaoFenTimeSet(i).JGtime(j) = MinuteToSecond(Me.grdTime.Item(12, Me.grdTime.CurrentCell.RowIndex).Value)
                                    Time1 = GaoFenTimeSet(i).lZhouQiTime(j)
                                    Time2 = GaoFenTimeSet(i).JGtime(j)
                                    If Time2 > 0 Then
                                        If Int(Time1 / Time2) = Time1 / Time2 Then
                                            GaoFenTimeSet(i).ChediNum(j) = Int(Time1 / Time2)
                                        Else
                                            GaoFenTimeSet(i).ChediNum(j) = Int(Time1 / Time2) + 1
                                        End If
                                        GaoFenTimeSet(i).JGOne(j) = GaoFenTimeSet(i).JGtime(j)
                                        GaoFenTimeSet(i).JGTwo(j) = Time1 - GaoFenTimeSet(i).JGtime(j) * (GaoFenTimeSet(i).ChediNum(j) - 1)
                                        GaoFenTimeSet(i).NumOne(j) = GaoFenTimeSet(i).ChediNum(j) - 1
                                        GaoFenTimeSet(i).NumTwo(j) = 1
                                    End If
                                    Exit For
                                End If

                            Next j

                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If

            Case "��������"
                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then
                                    GaoFenTimeSet(i).ChediNum(j) = Me.grdTime.Item(13, Me.grdTime.CurrentCell.RowIndex).Value
                                    Time1 = GaoFenTimeSet(i).lZhouQiTime(j)
                                    If GaoFenTimeSet(i).ChediNum(j) > 0 Then
                                        GaoFenTimeSet(i).JGtime(j) = Int(GaoFenTimeSet(i).lZhouQiTime(j) / GaoFenTimeSet(i).ChediNum(j))
                                        GaoFenTimeSet(i).JGOne(j) = GaoFenTimeSet(i).JGtime(j)
                                        GaoFenTimeSet(i).JGTwo(j) = Time1 - GaoFenTimeSet(i).JGtime(j) * (GaoFenTimeSet(i).ChediNum(j) - 1)
                                        GaoFenTimeSet(i).NumOne(j) = GaoFenTimeSet(i).ChediNum(j) - 1
                                        GaoFenTimeSet(i).NumTwo(j) = 1
                                    End If
                                    Exit For
                                End If
                            Next j

                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If

            Case "���һ"

                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then
                                    GaoFenTimeSet(i).JGOne(j) = MinuteToSecond(Me.grdTime.Item(14, Me.grdTime.CurrentCell.RowIndex).Value)
                                    Time1 = GaoFenTimeSet(i).lZhouQiTime(j)
                                    Time2 = GaoFenTimeSet(i).JGtime(j)
                                    If GaoFenTimeSet(i).NumTwo(j) > 0 Then
                                        GaoFenTimeSet(i).JGTwo(j) = (Time1 - GaoFenTimeSet(i).JGOne(j) * (GaoFenTimeSet(i).NumOne(j))) / GaoFenTimeSet(i).NumTwo(j)
                                    End If
                                    Exit For
                                End If
                            Next j
                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If

            Case "����һ"

                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then
                                    GaoFenTimeSet(i).NumOne(j) = Me.grdTime.Item(15, Me.grdTime.CurrentCell.RowIndex).Value
                                    Time1 = GaoFenTimeSet(i).lZhouQiTime(j)
                                    Time2 = GaoFenTimeSet(i).JGtime(j)
                                    GaoFenTimeSet(i).NumTwo(j) = GaoFenTimeSet(i).ChediNum(j) - GaoFenTimeSet(i).NumOne(j)
                                    If GaoFenTimeSet(i).NumTwo(j) > 0 Then
                                        GaoFenTimeSet(i).JGTwo(j) = (Time1 - GaoFenTimeSet(i).NumOne(j) * GaoFenTimeSet(i).JGOne(j)) / GaoFenTimeSet(i).NumTwo(j)
                                    End If
                                    Exit For
                                End If

                            Next j

                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If
        End Select

    End Sub

    Private Sub cmdReDrawJiaoLu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReDrawJiaoLu.Click

        Dim lngBeTime As Long
        Dim lngEnTime As Long
        Dim sJLstyle As String
        Dim i, j As Integer
        Dim sLongShortState As Integer
        Dim nBiNum1 As Integer
        Dim nBiNum2 As Integer
        Dim sFanJLStyle As String
        Dim sFirSta As String
        Dim sEndSta As String
        Dim sAnoFirSta As String
        Dim sAnoEndSta As String
        sFirSta = ""
        sEndSta = ""
        sAnoFirSta = ""
        sAnoEndSta = ""
        sFanJLStyle = ""
        If Trim(Me.cmbSecJLStyle.Text) = Trim(Me.cmbTrainJLStyle.Text) Then
            MsgBox("�ڶ���·���һ��·������ͬ��", , "��ʾ")
            Exit Sub
        Else
            sJLstyle = Me.cmbSecJLStyle.Text
        End If

        For j = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(j).sJiaoLuName = sJiaoLuStyle Then
                sFirSta = BasicTrainInf(j).ComeStation
                sEndSta = BasicTrainInf(j).NextStation
                Exit For
            End If
        Next j

        For j = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(j).sJiaoLuName = Trim(Me.cmbSecJLStyle.Text) Then
                sAnoFirSta = BasicTrainInf(j).ComeStation
                sAnoEndSta = BasicTrainInf(j).NextStation
                sFanJLStyle = BasicTrainInf(j).NextStation & "-->" & BasicTrainInf(j).ComeStation
                Exit For
            End If
        Next j

        If sAnoFirSta = sFirSta Then
            sLongShortState = 2
        ElseIf sAnoEndSta = sEndSta Then
            sLongShortState = 1
        Else
            MsgBox("�����ֽ�·�����̻���С��·��������ѡ��·��", , "��ʾ")
            Exit Sub
        End If

        lngBeTime = HourToSecond(Me.txtLongDayBeTime.Text)
        lngEnTime = AddLitterTime(HourToSecond(Me.txtLongDayEndTime.Text))
        If nCurJLID = 0 Then Exit Sub
        Dim sBiLi As String
        If Me.optDengBiLi.Checked = True Then '�ȱ����̻�
            nBiNum1 = Val(Me.cmbJLNumBi.Text.Substring(0, 1))
            nBiNum2 = Val(Me.cmbJLNumBi.Text.Substring(Me.cmbJLNumBi.Text.Length - 1))
            'lngBeTime = GaoFenTimeSet(nCurJLID).BeTime(1)
            'lngEnTime = GaoFenTimeSet(nCurJLID).EndTime(UBound(GaoFenTimeSet(nCurJLID).EndTime))
            Call SetTrainLongLine(sJLstyle, sFanJLStyle, lngBeTime, lngEnTime, sFirSta, sAnoFirSta, sAnoEndSta, sLongShortState, "�ȱ����̻�", nBiNum1, nBiNum2)
        Else '�ǵȱ����̻�
            For i = 1 To Me.grdBiLi.RowCount - 1
                sBiLi = Me.grdBiLi.Item(1, i - 1).Value.ToString
                nBiNum1 = Val(sBiLi.Substring(0, 1))
                nBiNum2 = Val(sBiLi.Substring(sBiLi.Length - 1))
                'lngBeTime = GaoFenTimeSet(nCurJLID).BeTime(i)
                'lngEnTime = GaoFenTimeSet(nCurJLID).EndTime(i)
                Call SetTrainLongLine(sJLstyle, sFanJLStyle, lngBeTime, lngEnTime, sFirSta, sAnoFirSta, sAnoEndSta, sLongShortState, "�ȱ����̻�", nBiNum1, nBiNum2)
            Next i
        End If


        Call ResetJiaoLu(sJLstyle, sLongShortState, sAnoFirSta, sAnoEndSta) '���¹���·

        '    If Me.chkAddChuRuKu.Value = 1 Then
        '        Call AddChuRuKuTrainAfterMulitJiaoLu
        '    End If

        Call ResetPrintTrainString() '�ر೵��
        Call addOneUndoInf()
        Call ShowChediJiaolu2()
    End Sub


    Private Sub cmdStartJLPU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStartJLPU.Click

        Dim i As Integer
        Call InputFirstCan(sJiaoLuStyle)
        Call GZLineUseSet()
        ReDim ChediMultiJLInfo(0)
        Call SaveTmpCheDiInfor()
        ReDim tmpCheDiInf(0)
        ReDim tmpCheDiInf(0).nLinkTrain(0)
        ReDim ChediInfo(0)
        ReDim ChediInfo(0).nLinkTrain(0)
        For i = 1 To UBound(BaseChediInfo)
            BaseChediInfo(i).bIfGouWang = 0
        Next i
        ReDim nAllTrainSeq(0)
        ReDim ErrorInf(0)


        Me.StatusLabel.Text = "���ڼ��㷽��"
        Me.ProBar.Visible = True

        Call AllChediGaoFengBackGouHua(sJiaoLuStyle, sFanJiaoLuStyle, sPuHuaStyle, Me.ProBar)

        Call SaveTmpCheDiInfor()
        Call InputtmpCDInformation()

        Call AllCheDiDrawTrainJiaoLu()
        Call addOneUndoInf()

        Me.ProBar.Visible = False
        Me.StatusLabel.Text = "����ͼ�̻�"

    End Sub

    Private Sub rBtnDaXiaoJL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rBtnDaXiaoJL.CheckedChanged
        If Me.rBtnDaXiaoJL.Checked = True Then
            Me.optCangShu.Visible = True
            Me.optDaXiao.Visible = True
            Me.optGongXian.Visible = False
            Me.optXianJie.Visible = False
        End If
    End Sub

    Private Sub rBtnXianJieJL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rBtnXianJieJL.CheckedChanged
        If Me.rBtnXianJieJL.Checked = True Then
            Me.optCangShu.Visible = False
            Me.optDaXiao.Visible = False
            Me.optGongXian.Visible = False
            Me.optXianJie.Visible = True
        End If
    End Sub

    Private Sub rBtnGongXianJL_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rBtnGongXianJL.CheckedChanged
        If Me.rBtnGongXianJL.Checked = True Then
            Me.optCangShu.Visible = True
            Me.optDaXiao.Visible = False
            Me.optGongXian.Visible = True
            Me.optXianJie.Visible = False
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim lngBeTime As Long
        Dim lngEnTime As Long
        Dim sJLstyle As String
        Dim i, j As Integer
        Dim sLongShortState As Integer
        Dim nBiNum1 As Integer
        Dim nBiNum2 As Integer

        ' Dim sAnoJiaoLuStyle As String
        Dim sAnoJiaoLuStyle2 As String
        Dim sFanJLStyle As String
        Dim sFanJLStyle2 As String
        Dim sFirSta As String
        Dim sEndSta As String
        Dim sGongXiangDownSta As String
        Dim sGongXiangUpSta As String
        Dim sAnoFirSta2 As String
        Dim sAnoEndSta2 As String
        Dim nDownId As Integer
        Dim nUpId As Integer
        sAnoEndSta2 = ""
        sAnoFirSta2 = ""
        sEndSta = ""
        sFanJLStyle2 = ""
        sFirSta = ""
        sJLstyle = Me.cmbTrainJLStyle.Text
        sAnoJiaoLuStyle2 = Trim(Me.cmbAnotGongXianJL.Text)

        If sAnoJiaoLuStyle2 = sJLstyle Then
            MsgBox("�ڶ���·���һ��·������ͬ��", , "��ʾ")
            Exit Sub
        End If


        For j = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(j).sJiaoLuName = sJLstyle Then
                sFirSta = BasicTrainInf(j).ComeStation
                sEndSta = BasicTrainInf(j).NextStation
                sFanJLStyle = BasicTrainInf(j).NextStation & "-->" & BasicTrainInf(j).ComeStation
                Exit For
            End If
        Next j

        For j = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(j).sJiaoLuName = sAnoJiaoLuStyle2 Then
                sAnoFirSta2 = BasicTrainInf(j).ComeStation
                sAnoEndSta2 = BasicTrainInf(j).NextStation
                sFanJLStyle2 = BasicTrainInf(j).NextStation & "-->" & BasicTrainInf(j).ComeStation
                Exit For
            End If
        Next j

        sGongXiangDownSta = Trim(Me.cmbGongDownFirSta.Text)
        sGongXiangUpSta = Trim(Me.cmbGongXianUpFirSta.Text)

        sLongShortState = 1

        If nCurJLID = 0 Then Exit Sub
        Dim sBiLi As String
        If Me.optDengBiLi.Checked = True Then '�ȱ����̻�
            nDownId = Me.cmbDownID.Text
            nUpId = Me.cmbUpID.Text
            nBiNum1 = Val(Me.cmbJLNumBi.Text.Substring(0, 1))
            nBiNum2 = Val(Me.cmbJLNumBi.Text.Substring(Me.cmbJLNumBi.Text.Length - 1))
            lngBeTime = AddLitterTime(HourToSecond(Me.txtLongDayBeTime.Text.Trim)) 'GaoFenTimeSet(nCurJLID).BeTime(1)
            lngEnTime = AddLitterTime(HourToSecond(Me.txtLongDayEndTime.Text.Trim)) 'GaoFenTimeSet(nCurJLID).EndTime(UBound(GaoFenTimeSet(nCurJLID).EndTime))
            If lngBeTime >= lngEnTime Then
                MsgBox("ʼ��ʱ����ڽ���ʱ�䣨��ʱ��Ƚϳ�ʼʱ����󣡣�������������ʱ��λ�ʱ��Ƚϳ�ʼʱ�䣡", , "��ʾ")
                Exit Sub
            Else
                Call SetGongXianTrainTwo(sJLstyle, sAnoJiaoLuStyle2, sFanJLStyle2, lngBeTime, lngEnTime, sFirSta, sEndSta, sAnoFirSta2, sAnoEndSta2, sGongXiangDownSta, sGongXiangUpSta, "�ȱ����̻�", nBiNum1, nBiNum2, nDownId, nUpId, Me.ProBar)
            End If
        Else '�ǵȱ����̻�
            For i = 1 To Me.grdBiLi.RowCount - 1
                sBiLi = Me.grdBiLi.Item(1, i - 1).Value.ToString
                nBiNum1 = Val(sBiLi.Substring(0, 1))
                nBiNum2 = Val(sBiLi.Substring(sBiLi.Length - 1))
                lngBeTime = GaoFenTimeSet(nCurJLID).BeTime(i)
                lngEnTime = GaoFenTimeSet(nCurJLID).EndTime(i)
                nDownId = Me.grdBiLi.Item(2, i - 1).Value
                nUpId = Me.grdBiLi.Item(3, i - 1).Value
                If nDownId > 0 And nUpId > 0 Then
                    Call SetGongXianTrainTwo(sJLstyle, sAnoJiaoLuStyle2, sFanJLStyle2, lngBeTime, lngEnTime, sFirSta, sEndSta, sAnoFirSta2, sAnoEndSta2, sGongXiangDownSta, sGongXiangUpSta, "�ȱ����̻�", nBiNum1, nBiNum2, nDownId, nUpId, Me.ProBar)
                Else
                    MsgBox("����˳����ȷ,������ѡ��!", , "��ʾ")
                    Exit Sub
                End If
            Next i
        End If


        '    Call SetTrainChouXianByGongXian(nCurJLID, sAnoJiaoLuStyle2, sFanJLStyle2, sAnoFirSta2, 1)
        ' Call ResetJiaoLuGongXian(sJLstyle, sAnoJiaoLuStyle, sAnoJiaoLuStyle2, sFanJLStyle, sFanJLStyle2, sFirSta, sAnoFirSta, sAnoEndSta, sAnoFirSta2, sAnoEndSta2)

        Call ResetJiaoLuGongXianInsertTrain(sAnoJiaoLuStyle2, sFanJLStyle2, sAnoFirSta2, sAnoEndSta2)     '���¹���·


        '    If Me.chkAddChuRuKu.Value = 1 Then
        '        Call AddChuRuKuTrainAfterMulitJiaoLu
        '    End If

        Call ResetPrintTrainString() '�ر೵��
        Call addOneUndoInf()
        Call ShowChediJiaolu2()

    End Sub


    '��������ĳ��׽�·���¹���'�����̻�
    Public Sub ResetJiaoLuGongXianInsertTrain(ByVal sJiaoLuStyle As String, ByVal sFanJLStyle As String, ByVal sStartSta As String, _
    ByVal sAnoEndSta As String)
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


        Call SetTmpAllTrainSeqByArriUp() '�����е�������
        Call SetTmpAllTrainSeqByStartDown(sStartSta)
        Me.ProBar.Visible = True
        Me.ProBar.Value = 0
        Me.ProBar.Maximum = UBound(nTmpAllTrainSeqUp)
        For i = 1 To UBound(nTmpAllTrainSeqUp)
            nTrain = nTmpAllTrainSeqUp(i)
            'If nTrain = 420 Then Stop
            If TrainInf(nTrain).Train <> "" Then
                If nTrain Mod 2 = 0 And TrainInf(nTrain).NextStation = sStartSta Then
                    sTime = AddLitterTime(TrainInf(nTrain).lAllEndTime)
                    nFtrain = SeekGouSatisfiedTrain(sTime, sStartSta, nTrain)
                    'If nFtrain <> 0 Then
                    nCD1 = AllChediFromTrainToCDid(nTrain)
                    nCD2 = AllChediFromTrainToCDid(nFtrain)
                    'If nCD1 <> 0 And nCD2 <> 0 Then
                    Call AddTrainToCheDi1(nCD1, nCD2, nTrain, nFtrain)
                    TrainInf(nFtrain).nCDPuOrNot = 1
                    'End If
                    'End If
                End If
            End If
            Me.ProBar.Value = i
        Next i

        Call SetTmpAllTrainSeqByArriUp()
        Me.ProBar.Value = 0
        Me.ProBar.Maximum = UBound(nTmpAllTrainSeqUp)
        For i = 1 To UBound(nTmpAllTrainSeqUp)
            nTrain = nTmpAllTrainSeqUp(i)
            'If nTrain = 511 Then Stop
            If nTrain Mod 2 <> 0 And TrainInf(nTrain).NextStation = sAnoEndSta Then
                'If TrainInf(nTrain).NextStation = "�ɽ��³�" Then
                sTime = AddLitterTime(TrainInf(nTrain).lAllEndTime)
                'If nTrain = 463 Then Stop
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
            Me.ProBar.Value = i
        Next i
        Me.ProBar.Visible = False

        Call InputCheDiInformation()
    End Sub

    'Private Sub grdTime_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTime.CellContentClick

    'End Sub

    'Private Sub grdTime_CellLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTime.CellLeave
    '    Call ValueChange()
    'End Sub

    Private Sub grdTime_Scroll(ByVal sender As Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles grdTime.Scroll
        Me.cmbEditTime.Visible = False
    End Sub

    Private Sub btnOutExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutExcel.Click
        Call OutPutToEXCELFileFormDataGrid(Me.cmbTrainJLStyle.Text & "����" & Me.cmbPuHuaStyle.Text & "ʱ��������", Me.grdTime, Me)
    End Sub

    Private Sub optNotBiLi_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optNotBiLi.CheckedChanged
        If Me.optNotBiLi.Checked = True Then
            Me.grdBiLi.Rows.Clear()
            Dim i As Integer
            Dim nNum As Integer
            nNum = 0
            If Me.grdTime.Rows.Count > 0 Then
                For i = 1 To Me.grdTime.Rows.Count - 1
                    Me.grdBiLi.Rows.Add()
                Next
            End If

            For i = 1 To Me.grdTime.Rows.Count - 1
                Me.grdBiLi.Rows(nNum).Cells(0).Value = i
                Me.grdBiLi.Rows(nNum).Cells(1).Value = "1:1"
                Me.grdBiLi.Rows(nNum).Cells(2).Value = "1"
                Me.grdBiLi.Rows(nNum).Cells(3).Value = "1"
                nNum = nNum + 1
            Next

        End If
    End Sub

    Private Sub Panel2_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class