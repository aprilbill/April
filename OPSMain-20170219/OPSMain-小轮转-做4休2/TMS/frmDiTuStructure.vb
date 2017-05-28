Public Class frmDiTuStructure
    Dim sngpritopBlank As Single
    Dim sngpriLeftBlank As Single
    Dim sngpriStaBlank As Single
    Dim sngScale As Single
    Dim nMoveState As Integer
    Dim nCurMoveStaID As Integer
    Dim sngCurMoveY As Single
    Dim nIFMove As Integer

    Private Sub frmDiTuStructure_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        Dim j As Integer
        For i = 1 To 20
            Me.cmbFenGeTime.Items.Add(i * 20)
        Next

        For i = 1 To UBound(NetInf.Line)
            For j = 1 To UBound(NetInf.Line(i).Station)
                Me.lstBei.Items.Add("(" & NetInf.Line(i).sName & ")" & NetInf.Line(i).Station(j).sStaName)
            Next
        Next

        sngpritopBlank = 20
        sngpriLeftBlank = 10
        sngpriStaBlank = 80
        sngScale = 1
        nMoveState = 0 '���ƶ�
        ReDim DiTuStructureStaInf(0)
        Call inputDiDuStructureSecInf()
    End Sub

    Private Sub cmdAddOne_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddOne.Click
        Dim nSelectID As Integer
        nSelectID = Me.lstSta.SelectedIndex
        If Me.lstBei.SelectedIndex >= 0 Then
            Me.lstSta.Items.Insert(Me.lstSta.SelectedIndex + 1, Me.lstBei.SelectedItem)
            Me.lstSta.SelectedIndex = nSelectID + 1
            'Call AddItems(Me.lstBei.SelectedItem)
            If Me.lstBei.SelectedIndex <= Me.lstBei.Items.Count - 2 Then
                Me.lstBei.SelectedItem = Me.lstBei.Items(Me.lstBei.SelectedIndex + 1)
            End If
            Call Me.cmdRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    '�Ƿ��ظ�
    Private Function IFThisItemExist(ByVal txtName As String) As Integer
        IFThisItemExist = 0
        Dim i As Integer
        For i = 1 To Me.lstSta.Items.Count
            If Me.lstSta.Items(i - 1) = txtName Then
                IFThisItemExist = 1
                Exit For
            End If
        Next
    End Function

    Private Sub AddItems(ByVal txtName As String)
        'If IFThisItemExist(txtName) = 0 Then
        Me.lstSta.Items.Add(txtName)
        ' End If
    End Sub

    Private Sub cmdAddAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddAll.Click
        Dim i As Integer
        If Me.lstBei.Items.Count > 0 Then
            For i = 1 To Me.lstBei.Items.Count
                Call AddItems(Me.lstBei.Items(i - 1))
            Next
            Call Me.cmdRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub cmdDeleAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeleAll.Click
        Me.lstSta.Items.Clear()
        Call Me.cmdRefresh_Click(Nothing, Nothing)
    End Sub

    Private Sub cmdDeleOne_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeleOne.Click
        Dim nCurID As Integer
        nCurID = Me.lstSta.SelectedIndex
        If nCurID >= 0 Then
            Me.lstSta.Items.RemoveAt(Me.lstSta.SelectedIndex)
            If Me.lstSta.Items.Count > 0 Then
                If nCurID <= Me.lstSta.Items.Count - 1 Then
                    Me.lstSta.SelectedIndex = nCurID
                Else
                    Me.lstSta.SelectedIndex = nCurID - 1
                End If
            End If
            Call Me.cmdRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub lstBei_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBei.DoubleClick
        Call cmdAddOne_Click(Nothing, Nothing)
    End Sub

    Private Sub lstSta_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSta.DoubleClick
        Dim nCurID As Integer
        nCurID = Me.lstSta.SelectedIndex
        If nCurID >= 0 Then
            Me.lstSta.Items.RemoveAt(Me.lstSta.SelectedIndex)
            If Me.lstSta.Items.Count > 0 Then
                If nCurID <= Me.lstSta.Items.Count - 1 Then
                    Me.lstSta.SelectedIndex = nCurID
                Else
                    Me.lstSta.SelectedIndex = nCurID - 1
                End If
            End If
        End If
    End Sub

    Private Sub cmdUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUp.Click
        Dim nCurSelID As Integer
        nCurSelID = Me.lstSta.SelectedIndex
        If nCurSelID > 0 Then
            Me.lstSta.Items.Insert(Me.lstSta.SelectedIndex - 1, Me.lstSta.SelectedItem)
            Me.lstSta.Items.RemoveAt(Me.lstSta.SelectedIndex)
            Me.lstSta.SelectedIndex = nCurSelID - 1
        Else
            Me.lstSta.SelectedIndex = 0
        End If
        Call Me.cmdRefresh_Click(Nothing, Nothing)

    End Sub

    Private Sub CmdDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdDown.Click
        Dim nCurSelID As Integer
        nCurSelID = Me.lstSta.SelectedIndex
        If nCurSelID >= 0 And nCurSelID < Me.lstSta.Items.Count - 1 Then
            Me.lstSta.Items.Insert(nCurSelID + 2, Me.lstSta.SelectedItem)
            Me.lstSta.Items.RemoveAt(nCurSelID)
            Me.lstSta.SelectedIndex = nCurSelID + 1
            Call Me.cmdRefresh_Click(Nothing, Nothing)

        Else
            If nCurSelID = 0 Then
                Me.lstSta.SelectedIndex = 0
            Else
                Me.lstSta.SelectedIndex = Me.lstSta.Items.Count - 1
            End If
        End If

    End Sub


    Private Sub chkSetecAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSetecAll.CheckedChanged
        Dim i As Integer
        If Me.chkSetecAll.Checked = True Then
            For i = 1 To Me.lstSta.Items.Count
                Me.lstSta.SetItemChecked(i - 1, True)
            Next
        Else
            For i = 1 To Me.lstSta.Items.Count
                Me.lstSta.SetItemChecked(i - 1, False)
            Next
        End If
    End Sub

    Private Sub inputDiDuStructureSecInf()
        '��������������
        '****************����ΪADO����
        '����һ��OledbConnection
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)

        Dim i As Integer
        Dim j As Integer

        ''������Ϣ
        Dim strTable3 As String = "select * from ��·������Ϣ order by ������"
        Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
        '����һ��Datasetd
        Dim myDataSet3 As Data.DataSet = New Data.DataSet
        Mydc3.Fill(myDataSet3, "��·������Ϣ")
        Dim myTable3 As Data.DataTable
        myTable3 = myDataSet3.Tables("��·������Ϣ")
        ReDim DiTuStructureSecInf(0)
        If myTable3.Rows.Count > 0 Then
            For j = 1 To myTable3.Rows.Count
                ReDim Preserve DiTuStructureSecInf(UBound(DiTuStructureSecInf) + 1)
                ReDim DiTuStructureSecInf(UBound(DiTuStructureSecInf)).lDistance(2)
                DiTuStructureSecInf(UBound(DiTuStructureSecInf)).nSecNumber = myTable3.Rows(j - 1).Item("������")
                DiTuStructureSecInf(UBound(DiTuStructureSecInf)).sSecName = Trim(myTable3.Rows(j - 1).Item("��������"))
                DiTuStructureSecInf(UBound(DiTuStructureSecInf)).sSecFirName = Trim(myTable3.Rows(j - 1).Item("������ʼվ"))
                DiTuStructureSecInf(UBound(DiTuStructureSecInf)).sSecSecName = Trim(myTable3.Rows(j - 1).Item("�����յ�վ"))
                DiTuStructureSecInf(UBound(DiTuStructureSecInf)).nFirStaID = j 'FromStaNameToStaIDByStationinf(sFirSta)
                DiTuStructureSecInf(UBound(DiTuStructureSecInf)).nSecStaID = j + 1 'FromStaNameToStaIDByStationinf(sSecSta)
                DiTuStructureSecInf(UBound(DiTuStructureSecInf)).lDistance(1) = myTable3.Rows(j - 1).Item("�����������")
                DiTuStructureSecInf(UBound(DiTuStructureSecInf)).lDistance(2) = myTable3.Rows(j - 1).Item("�����������")
            Next

            '������
            Dim strSecName As String
            ' Dim nSecXuHao As Integer
            For i = 1 To UBound(DiTuStructureSecInf)
                strSecName = DiTuStructureSecInf(i).sSecName
                'nSecXuHao = SectionInf(i).nSecNumber
                Dim strTable4 As String = "select * from �г����б�� where ��������='" & strSecName & "' order by ��߱��"
                ' Dim strTable4 As String = "select * from �������б�� where �������=" & nSecXuHao & " order by ��߱��"
                Dim Mydc4 As New Data.OleDb.OleDbDataAdapter(strTable4, strCon)
                '����һ��Datasetd
                Dim myDataSet4 As Data.DataSet = New Data.DataSet
                Mydc4.Fill(myDataSet4, "�г����б��")
                Dim myTable4 As Data.DataTable
                myTable4 = myDataSet4.Tables("�г����б��")
                If myTable4.Rows.Count > 0 Then
                    ReDim Preserve DiTuStructureSecInf(i).SecScale(myTable4.Rows.Count)
                    For j = 1 To myTable4.Rows.Count
                        DiTuStructureSecInf(i).SecScale(j).nID = myTable4.Rows(j - 1).Item("��߱��")
                        DiTuStructureSecInf(i).SecScale(j).sScaleName = myTable4.Rows(j - 1).Item("�������").ToString.Trim
                        DiTuStructureSecInf(i).SecScale(j).sngDownTime = MinuteToSecond(myTable4.Rows(j - 1).Item("��������ʱ��").ToString.Trim)
                        DiTuStructureSecInf(i).SecScale(j).sngUpTime = MinuteToSecond(myTable4.Rows(j - 1).Item("��������ʱ��").ToString.Trim)
                        DiTuStructureSecInf(i).SecScale(j).sngDownAppendStartTime = MinuteToSecond(myTable4.Rows(j - 1).Item("������ʱ��").ToString.Trim)
                        DiTuStructureSecInf(i).SecScale(j).sngDownAppendStopTime = MinuteToSecond(myTable4.Rows(j - 1).Item("����ͣ��ʱ��").ToString.Trim)
                        DiTuStructureSecInf(i).SecScale(j).sngUpAppendStartTime = MinuteToSecond(myTable4.Rows(j - 1).Item("������ʱ��").ToString.Trim)
                        DiTuStructureSecInf(i).SecScale(j).sngUpAppendStopTime = MinuteToSecond(myTable4.Rows(j - 1).Item("����ͣ��ʱ��").ToString.Trim)
                    Next
                Else
                    MsgBox("�㻹û������������б����Ϣ��������ӣ�", , "��ʾ")
                    Exit Sub
                End If
            Next
            MyConn.Close()
        Else
            MsgBox("�㻹û�����������Ϣ��������ӣ�", , "��ʾ")
        End If

    End Sub

    Private Sub cmdRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRefresh.Click
        Call RefreshDiTuStructureLine()
    End Sub

    'ˢ�µ�ͼ
    Private Sub RefreshDiTuStructureLine()
        Dim i As Integer
        Dim j As Integer
        Dim sFirSta As String
        Dim sSecSta As String
        Dim sSecName As String
        Dim tmpY As Single
        Dim ifIn As Integer
        Dim sngJianGeTime As Long
        sngJianGeTime = Val(Me.cmbFenGeTime.Text)
        ifIn = 0
        tmpY = 0
        If Me.lstSta.Items.Count > 0 Then
            ReDim DiTuStructureStaInf(Me.lstSta.Items.Count)
            For i = 1 To Me.lstSta.Items.Count
                DiTuStructureStaInf(i).sAtLineName = GetLineNameInDiTuStructure(Me.lstSta.Items(i - 1))
                DiTuStructureStaInf(i).sStationName = GetStaNameInDiTuStructure(Me.lstSta.Items(i - 1))
                If Me.lstSta.GetItemChecked(i - 1) = True Then
                    DiTuStructureStaInf(i).bIfShowInCheDiJiaoLuTu = True
                Else
                    DiTuStructureStaInf(i).bIfShowInCheDiJiaoLuTu = False
                End If

            Next
            For i = 2 To UBound(DiTuStructureStaInf)
                sFirSta = DiTuStructureStaInf(i - 1).sStationName
                sSecSta = DiTuStructureStaInf(i).sStationName
                sSecName = sFirSta & "->" & sSecSta
                ifIn = 0
                For j = 1 To UBound(DiTuStructureSecInf)
                    If DiTuStructureSecInf(j).sSecName = sSecName Then
                        tmpY = tmpY + DiTuStructureSecInf(j).SecScale(1).sngDownTime
                        DiTuStructureStaInf(i).Ycord = tmpY
                        ifIn = 1
                    End If
                Next
                If ifIn = 0 Then '����
                    tmpY = tmpY + sngJianGeTime
                    DiTuStructureStaInf(i).Ycord = tmpY
                End If
            Next
            Call DrawDiTuStructureLine()
        End If

    End Sub

    '��ӡ��ͼ�ṹ
    Private Sub DrawDiTuStructureLine()
        If UBound(DiTuStructureStaInf) = 0 Then Exit Sub
        Dim i As Integer
        Dim j As Integer
        Dim sFirSta As String
        Dim sSecSta As String
        Dim sSecName As String
        Dim tmpY As Single
        Dim ifIn As Integer

        Dim X1, Y1, X2, Y2 As Single
        Dim tmpBmp As Bitmap
        tmpBmp = New System.Drawing.Bitmap(Me.picSta.Width, Me.picSta.Height)
        Dim rBmpGraphics As Graphics '����·�복վͼ�Ķ���Ķ���
        rBmpGraphics = Graphics.FromImage(tmpBmp)

        tmpY = 0
        For i = 1 To UBound(DiTuStructureStaInf)
            If DiTuStructureStaInf(i).Ycord > tmpY Then
                tmpY = DiTuStructureStaInf(i).Ycord
            End If
        Next

        sngScale = (Me.picSta.Height - sngpritopBlank * 2) / tmpY
        For i = 2 To UBound(DiTuStructureStaInf)
            sFirSta = DiTuStructureStaInf(i - 1).sStationName
            sSecSta = DiTuStructureStaInf(i).sStationName
            sSecName = sFirSta & "->" & sSecSta
            ifIn = 0
            For j = 1 To UBound(DiTuStructureSecInf)
                If DiTuStructureSecInf(j).sSecName = sSecName Then
                    X1 = sngpriLeftBlank + sngpriStaBlank
                    X2 = sngpriLeftBlank + sngpriStaBlank
                    Y1 = DiTuStructureStaInf(i - 1).Ycord * sngScale + sngpritopBlank
                    Y2 = DiTuStructureStaInf(i).Ycord * sngScale + sngpritopBlank

                    rBmpGraphics.DrawLine(New System.Drawing.Pen(Color.Green, 2), X1, Y1, Me.picSta.Width - sngpriLeftBlank - 10, Y1)
                    rBmpGraphics.DrawLine(New System.Drawing.Pen(Color.Green, 2), X2, Y2, Me.picSta.Width - sngpriLeftBlank - 10, Y2)
                    rBmpGraphics.DrawLine(New System.Drawing.Pen(Color.Green, 2), X1, Y1, X2, Y2)
                    rBmpGraphics.DrawString(sFirSta, New Font("����", 9), Brushes.Green, 10, Y1 - 5)
                    ifIn = 1
                    If i = UBound(DiTuStructureStaInf) Then
                        rBmpGraphics.DrawString(sSecSta, New Font("����", 9), Brushes.Green, 10, Y2 - 5)
                    End If
                End If
            Next
            If ifIn = 0 Then '����
                rBmpGraphics.DrawString(sFirSta, New Font("����", 9), Brushes.Green, 10, Y2 - 5)
            End If

        Next
        Me.picSta.Image = tmpBmp

    End Sub



    Private Sub picSta_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picSta.MouseDown
        Dim i As Integer
        Dim rBmpGraphics As Graphics
        Dim tmpPen As Pen
        For i = 1 To UBound(DiTuStructureStaInf)
            If Math.Abs(DiTuStructureStaInf(i).Ycord * sngScale - e.Y + sngpritopBlank) <= 5 Then
                Me.picSta.Refresh()
                rBmpGraphics = Me.picSta.CreateGraphics()
                tmpPen = New Pen(Color.Red, 2)
                rBmpGraphics.DrawLine(tmpPen, sngpriLeftBlank + sngpriStaBlank, DiTuStructureStaInf(i).Ycord * sngScale + sngpritopBlank, Me.picSta.Width - sngpriLeftBlank - 10, DiTuStructureStaInf(i).Ycord * sngScale + sngpritopBlank)
                rBmpGraphics.DrawString(DiTuStructureStaInf(i).sStationName, New Font("����", 9), Brushes.Red, 10, DiTuStructureStaInf(i).Ycord * sngScale + sngpritopBlank - 5)
                nCurMoveStaID = i
                nMoveState = 1
                Exit For
            End If
        Next
    End Sub

    Private Sub picSta_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picSta.MouseMove
        Dim rBmpGraphics As Graphics
        Dim tmpPen As Pen
        Dim tmpY1 As Single
        If nMoveState = 1 And e.Button = Windows.Forms.MouseButtons.Left Then
            Me.picSta.Refresh()
            rBmpGraphics = Me.picSta.CreateGraphics()
            tmpPen = New Pen(Color.Red, 2)
            tmpY1 = AutoDuiQiYCoord(e.Y)
            rBmpGraphics.DrawLine(tmpPen, sngpriLeftBlank + sngpriStaBlank, tmpY1, Me.picSta.Width - sngpriLeftBlank - 10, tmpY1)
            rBmpGraphics.DrawString(DiTuStructureStaInf(nCurMoveStaID).sStationName, New Font("����", 9), Brushes.Red, 10, tmpY1 - 5)
            sngCurMoveY = tmpY1
            nIFMove = 1
        End If
    End Sub

    Private Sub picSta_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picSta.MouseUp
        Dim i As Integer
        Dim ntmp As Integer
        Dim nFirY As Integer
        Dim nMove As Integer
        ntmp = (sngCurMoveY - sngpritopBlank) / sngScale
        nFirY = DiTuStructureStaInf(nCurMoveStaID).Ycord
        nMove = ntmp - nFirY
        If nMoveState = 1 And e.Button = Windows.Forms.MouseButtons.Left And nIFMove = 1 Then
            If Me.chkAllMove.Checked = True Then
                For i = 1 To UBound(DiTuStructureStaInf)
                    If DiTuStructureStaInf(i).Ycord >= nFirY Then
                        DiTuStructureStaInf(i).Ycord = DiTuStructureStaInf(i).Ycord + nMove
                    End If
                Next
            Else
                DiTuStructureStaInf(nCurMoveStaID).Ycord = ntmp
            End If
            Call DrawDiTuStructureLine()
            nMoveState = 0
            nIFMove = 0
        End If
    End Sub

    Private Function AutoDuiQiYCoord(ByVal Y As Single) As Single
        Dim i As Integer
        Dim nMin As Single
        Dim nCurID As Integer
        nMin = 100000
        nCurID = 0
        If Me.chkDuiQi.Checked = True Then
            For i = 1 To UBound(DiTuStructureStaInf)
                If Math.Abs(DiTuStructureStaInf(i).Ycord * sngScale + sngpritopBlank - Y) < nMin Then
                    nMin = Math.Abs(DiTuStructureStaInf(i).Ycord * sngScale + sngpritopBlank - Y)
                    nCurID = i
                End If
            Next
            AutoDuiQiYCoord = DiTuStructureStaInf(nCurID).Ycord * sngScale + sngpritopBlank
        Else
            AutoDuiQiYCoord = Y
        End If
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        '���´��봴����·��·��Ӧ���ļ��У���������ص��й�����ͼ�������Ϣ
        Dim myWS As DAO.Workspace
        Dim DE As DAO.DBEngine = New DAO.DBEngine
        myWS = DE.Workspaces(0)

        Dim dbData As DAO.Database
        Dim RSdata As DAO.Recordset
        Dim i As Integer, j As Integer
        Dim nNum As Integer
        Dim bFind As Boolean
        Dim tmpName() As String
        ReDim tmpName(0)
        Dim k As Integer
        Dim sqlStr As String
        Dim nCount As Integer
        Dim NyuanMoRen As Integer
        NyuanMoRen = 0

        bFind = False
        dbData = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        RSdata = dbData.OpenRecordset("select distinct ��ͼ���� from ��ͼ�ṹ")
        If RSdata.RecordCount > 0 Then
            RSdata.MoveLast()
            nNum = RSdata.RecordCount
        End If
        Dim nf As New frmInputBox

        nf.Text = "�����ͼ����"
        nf.labTitle.Text = "�����ѡ���ͼ����:"
        nf.cmbText.Visible = True
        nf.txtText.Visible = False
        nf.cmbText.Items.Clear()

        If nNum > 0 Then
            RSdata.MoveFirst()
            ReDim tmpName(nNum)
            For i = 1 To nNum
                nf.cmbText.Items.Add(RSdata.Fields("��ͼ����").Value.ToString.Trim)
                tmpName(i) = RSdata.Fields("��ͼ����").Value.ToString.Trim
                RSdata.MoveNext()
            Next
        Else
        End If

sFirst:
        nf.ShowDialog()
        If StrInputBoxCombText <> "" And bCancelInput = 0 Then
            For j = 1 To UBound(tmpName)
                If StrInputBoxCombText = tmpName(j) Then
                    If MsgBox("�õ�ͼ�����Ѿ����ڣ��Ƿ񸲸�ԭ�������ƣ�", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "ȷ������") = MsgBoxResult.Yes Then
                        RSdata = dbData.OpenRecordset("select * from ��ͼ�ṹ where ��ͼ����='" & StrInputBoxCombText & "'")
                        RSdata.MoveFirst()
                        If RSdata.RecordCount > 0 Then
                            NyuanMoRen = RSdata.Fields("�Ƿ�Ĭ��").Value
                        End If
                        sqlStr = "delete * from ��ͼ�ṹ where ��ͼ����='" & StrInputBoxCombText & "'"
                        dbData.Execute(sqlStr)
                        Exit For
                    Else
                        GoTo sFirst
                    End If
                    Exit For
                End If
            Next j

            RSdata = dbData.OpenRecordset("��ͼ�ṹ")
            nCount = RSdata.RecordCount
            k = 1
            For i = 1 To UBound(DiTuStructureStaInf)
                RSdata.AddNew()
                RSdata.Fields("��ͼ����").Value = StrInputBoxCombText
                RSdata.Fields("վ��").Value = k
                RSdata.Fields("վ��").Value = DiTuStructureStaInf(i).sStationName
                RSdata.Fields("��·��").Value = DiTuStructureStaInf(i).sAtLineName
                RSdata.Fields("�Ƿ���ʾ").Value = DiTuStructureStaInf(i).bIfShowInCheDiJiaoLuTu
                RSdata.Fields("��վID").Value = k + 1
                RSdata.Fields("��վID").Value = k - 1
                RSdata.Fields("Y����").Value = DiTuStructureStaInf(i).Ycord
                If nCount = 0 Then
                    RSdata.Fields("�Ƿ�Ĭ��").Value = "1"
                Else
                    If NyuanMoRen = 1 Then
                        RSdata.Fields("�Ƿ�Ĭ��").Value = "1"
                    Else
                        RSdata.Fields("�Ƿ�Ĭ��").Value = "0"
                    End If
                End If
                k = k + 1
                RSdata.Update()
            Next i
            myWS.Close()
            MsgBox("������ϣ�", , "��ʾ")
        End If
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.Close()
    End Sub

    Private Sub frmDiTuStructure_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'Call DrawDiTuStructureLine()
    End Sub

    Private Sub cmbFenGeTime_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbFenGeTime.SelectedIndexChanged
        Call Me.cmdRefresh_Click(Nothing, Nothing)
    End Sub

    Private Sub btnRefreshDrawLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshDrawLine.Click
        Call DrawDiTuStructureLine()
    End Sub
End Class