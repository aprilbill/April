Public Class frmTrainStopAndSecTime
    Public strPriTrainJiaoLu As String
    Public strPath() As String
    Public nPriUpOrDown As Integer
    Dim CurCellX, CurCellY As Single
    Dim CurMouseX, CurMouseY As Single
    Dim CurCellX1, CurCellY1 As Single
    Dim CurMouseX1, CurMouseY1 As Single

    Private Sub frmTrainStopAndSecTime_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.grdStopSta.Rows.Clear()
        Me.grdQuJian.Rows.Clear()
        Call listTrainStopBiaoChi()
        If Me.cmbStopSta.Text <> "" Then
            Call listStopBiaoChi(Trim(Me.cmbStopSta.Text))
        End If

        Call listTrainQuJian()
        If Me.cmbBiaoChi.Text <> "" Then
            Call listSecBiaoChi(Me.cmbBiaoChi.Text.Trim)
        End If
        Me.Text = "������ͣվ���: ��ǰ��·����: " & strPriTrainJiaoLu ' & " ������: " & strPriTrainJiaoLu
    End Sub

    '��ʾͣվ��Ϣ
    Private Sub listTrainStopBiaoChi()

        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim i As Integer
        Dim strTable2 As String = "select distinct ͣվ���� from �г�ͣվ�����Ϣ where ��·����='" & strPriTrainJiaoLu & "'"
        Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
        Dim myDataSet2 As Data.DataSet = New Data.DataSet
        Mydc2.Fill(myDataSet2, "�г�ͣվ�����Ϣ")
        Dim myTable2 As Data.DataTable
        myTable2 = myDataSet2.Tables("�г�ͣվ�����Ϣ")
        Me.cmbStopSta.Items.Clear()
        If myTable2.Rows.Count > 0 Then
            For i = 1 To myTable2.Rows.Count
                Me.cmbStopSta.Items.Add(Trim(myTable2.Rows(i - 1).Item("ͣվ����")))
            Next
            Me.cmbStopSta.Text = Me.cmbStopSta.Items(0)
        Else
            Me.cmbStopSta.Text = ""
        End If
    End Sub
    Private Sub listStopBiaoChi(ByVal sBiaoChi As String)
        If sBiaoChi = "" Then
            Me.grdStopSta.RowCount = 0
            Exit Sub
        End If
        Me.cmbStopSta.Text = sBiaoChi
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim i As Integer
        Dim strTable2 As String = "select * from �г�ͣվ�����Ϣ where ��·����='" & strPriTrainJiaoLu & "' and ͣվ����='" & sBiaoChi & "' order by ���"
        Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
        Dim myDataSet2 As Data.DataSet = New Data.DataSet
        Mydc2.Fill(myDataSet2, "�г�ͣվ�����Ϣ")
        Dim myTable2 As Data.DataTable
        myTable2 = myDataSet2.Tables("�г�ͣվ�����Ϣ")

        If myTable2.Rows.Count > 0 Then
            Me.grdStopSta.RowCount = myTable2.Rows.Count
            For i = 1 To myTable2.Rows.Count
                Me.grdStopSta.Item(0, i - 1).Value = myTable2.Rows(i - 1).Item("���")
                Me.grdStopSta.Item(1, i - 1).Value = Trim(myTable2.Rows(i - 1).Item("��վ����"))
                Me.grdStopSta.Item(2, i - 1).Value = Trim(myTable2.Rows(i - 1).Item("�������"))
                Me.grdStopSta.Item(3, i - 1).Value = Trim(myTable2.Rows(i - 1).Item("ͣվʱ��"))
            Next
        End If

    End Sub

    '��ʾ������Ϣ
    Private Sub listTrainQuJian()


        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim i As Integer
        Dim strTable2 As String = "select distinct �������� from �г����б����Ϣ where ��·����='" & strPriTrainJiaoLu & "'"
        Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
        Dim myDataSet2 As Data.DataSet = New Data.DataSet
        Mydc2.Fill(myDataSet2, "�г����б����Ϣ")
        Dim myTable2 As Data.DataTable
        myTable2 = myDataSet2.Tables("�г����б����Ϣ")
        Me.cmbBiaoChi.Items.Clear()
        If myTable2.Rows.Count > 0 Then
            For i = 1 To myTable2.Rows.Count
                Me.cmbBiaoChi.Items.Add(Trim(myTable2.Rows(i - 1).Item("��������")))
            Next
            Me.cmbBiaoChi.Text = Me.cmbBiaoChi.Items(0)
        Else
            Me.cmbBiaoChi.Text = ""
        End If

    End Sub

    '��ʾ������
    Private Sub listSecBiaoChi(ByVal sBiaoChi As String)


        If sBiaoChi = "" Then
            Me.grdQuJian.RowCount = 0
            Exit Sub
        End If
        Me.cmbBiaoChi.Text = sBiaoChi
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim i As Integer
        Dim strTable2 As String = "select * from �г����б����Ϣ where ��·����='" & strPriTrainJiaoLu & "' and ��������='" & sBiaoChi & "' order by ���"
        Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
        Dim myDataSet2 As Data.DataSet = New Data.DataSet
        Mydc2.Fill(myDataSet2, "�г����б����Ϣ")
        Dim myTable2 As Data.DataTable
        myTable2 = myDataSet2.Tables("�г����б����Ϣ")

        If myTable2.Rows.Count > 0 Then
            Me.grdQuJian.RowCount = myTable2.Rows.Count
            For i = 1 To myTable2.Rows.Count
                Me.grdQuJian.Item(0, i - 1).Value = myTable2.Rows(i - 1).Item("���")
                Me.grdQuJian.Item(1, i - 1).Value = Trim(myTable2.Rows(i - 1).Item("��������"))
                Me.grdQuJian.Item(2, i - 1).Value = Trim(myTable2.Rows(i - 1).Item("�������"))
                Me.grdQuJian.Item(3, i - 1).Value = Trim(myTable2.Rows(i - 1).Item("����ʱ��"))
            Next
        End If

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddStopScale.Click

        Dim sBiaoChiName As String
        Dim i, j As Integer
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim str As String
        Dim sScaleName As String
        Dim ifIn As Integer
        Dim CurStopTime As String
        Dim sUpOrDown As String
        Dim sStaName As String
        Dim sStaStartOrEnd As String
        Dim StrScale() As String
        ReDim StrScale(0)
        For i = 1 To UBound(StopScaleInf)
            ifIn = 0
            For j = 1 To UBound(StrScale)
                If StopScaleInf(i).sScaleName = StrScale(j) Then
                    ifIn = 1
                    Exit For
                End If
            Next
            If ifIn = 0 Then
                ReDim Preserve StrScale(UBound(StrScale) + 1)
                StrScale(UBound(StrScale)) = StopScaleInf(i).sScaleName
            End If
        Next
SBegin:
        Dim nf As New frmInputBox
        nf.labTitle.Text = "��������������:"
        nf.txtText.Visible = False
        nf.cmbText.Visible = True
        nf.cmbText.Items.Clear()
        If UBound(StrScale) > 0 Then
            For i = 1 To UBound(StrScale)
                nf.cmbText.Items.Add(StrScale(i))
            Next
            nf.cmbText.Text = StrScale(1)
        End If
        nf.ShowDialog()
        sBiaoChiName = StrInputBoxCombText
        If sBiaoChiName <> "" And bCancelInput = 0 Then
            For i = 1 To Me.cmbStopSta.Items.Count
                If Me.cmbStopSta.Items(i - 1) = sBiaoChiName Then
                    MsgBox("�����������Ѿ����ڣ�����������!", , "��ʾ")
                    GoTo SBegin
                End If
            Next
            nf.labTitle.Text = "��ѡ��������:"
            nf.txtText.Visible = False
            nf.cmbText.Visible = True
            nf.cmbText.Items.Clear()
            If UBound(StrScale) > 0 Then
                For i = 1 To UBound(StrScale)
                    nf.cmbText.Items.Add(StrScale(i))
                Next
                nf.cmbText.Text = StrScale(1)
            End If
            nf.ShowDialog()
            sScaleName = StrInputBoxCombText

            If nPriUpOrDown Mod 2 = 0 Then
                sUpOrDown = "����"
            Else
                sUpOrDown = "����"
            End If

            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            For i = 1 To UBound(strPath)
                If i = 1 Then
                    sStaStartOrEnd = "ʼ��վ"
                ElseIf i = UBound(strPath) Then
                    sStaStartOrEnd = "�յ�վ"
                Else
                    sStaStartOrEnd = "�м�վ"
                End If
                sStaName = strPath(i)
                CurStopTime = GetCurStationStopTime(sScaleName, sStaName, sUpOrDown, sStaStartOrEnd)

                str = "insert into �г�ͣվ�����Ϣ (��·����,ͣվ����,��վ����,���,�������,ͣվʱ��) values ('" & _
                    strPriTrainJiaoLu & "', '" & _
                    sBiaoChiName & "', '" & _
                    strPath(i) & "', '" & _
                   i & "', '" & _
                   sScaleName & " ', '" & _
                   CurStopTime & "')"
                Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                Mcom1.ExecuteNonQuery()
            Next
            MyConn.Close()
            Call listTrainStopBiaoChi()
            Call listStopBiaoChi(sBiaoChiName)
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnDeleteStopScale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteStopScale.Click
        Dim str As String
        Dim sStyle As String
        sStyle = Me.cmbStopSta.Text.Trim
        If sStyle <> "" Then
            If MsgBox("ȷ��ɾ�� [" & sStyle & "] ͣվ������?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "ȷ������") = MsgBoxResult.Yes Then
                Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
                If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                str = "delete * from �г�ͣվ�����Ϣ where ͣվ����='" & sStyle & "' and ��·����='" & strPriTrainJiaoLu & "'"
                Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                Mcom.ExecuteNonQuery()
                MyConn.Close()

                Call listTrainStopBiaoChi()
                If Me.cmbStopSta.Text <> "" Then
                    Call listStopBiaoChi(Trim(Me.cmbStopSta.Text))
                Else
                    Call listStopBiaoChi("")
                End If
            End If
        Else
            MsgBox("��ѡ��Ҫɾ����ͣվ����!", , "��ʾ")
        End If
    End Sub

    Private Sub btnSaveStopScale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveStopScale.Click
        Dim str As String
        Dim i As Integer
        Dim sStyle As String
        sStyle = Me.cmbStopSta.Text.Trim
        If sStyle <> "" Then
            Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            str = "delete * from �г�ͣվ�����Ϣ where ͣվ����='" & sStyle & "' and ��·����='" & strPriTrainJiaoLu & "'"
            Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
            Mcom.ExecuteNonQuery()

            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            For i = 1 To Me.grdStopSta.RowCount
                str = "insert into �г�ͣվ�����Ϣ (��·����,ͣվ����,��վ����,���,�������,ͣվʱ��) values ('" & _
                strPriTrainJiaoLu & "', '" & _
                sStyle & "', '" & _
                Me.grdStopSta.Item(1, i - 1).Value & "', '" & _
               i & "', '" & _
                Me.grdStopSta.Item(2, i - 1).Value & "', '" & _
                Me.grdStopSta.Item(3, i - 1).Value & "')"
                Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                Mcom1.ExecuteNonQuery()
            Next
            MsgBox("�������!", , "��ʾ")
            MyConn.Close()
            'Call listTrainStopBiaoChi()
            'Call listStopBiaoChi(sStyle)
        Else
            MsgBox("��ѡ��Ҫ�����ͣվ����!", , "��ʾ")
        End If
    End Sub


    Private Sub cmbStopSta_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStopSta.SelectedValueChanged
        Call listStopBiaoChi(Me.cmbStopSta.Text.Trim)
    End Sub

    Private Sub btnAddSecScale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSecScale.Click
        Dim sBiaoChiName As String
        Dim i, j As Integer
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim str As String
        Dim sFirSta As String
        Dim SSecSTa As String
        Dim sSecName As String
        Dim strTime As String
        strTime = "0.00"
        Dim sUpOrDown As String
        Dim sScaleName As String
        Dim ifIn As Integer
        Dim StrScale() As String
        ReDim StrScale(0)
        For i = 1 To UBound(SectionScaleInf)
            ifIn = 0
            For j = 1 To UBound(StrScale)
                If SectionScaleInf(i).sScaleName = StrScale(j) Then
                    ifIn = 1
                    Exit For
                End If
            Next
            If ifIn = 0 Then
                ReDim Preserve StrScale(UBound(StrScale) + 1)
                StrScale(UBound(StrScale)) = SectionScaleInf(i).sScaleName
            End If
        Next

SBegin:
        Dim nf As New frmInputBox
        nf.labTitle.Text = "��������������:"
        nf.txtText.Visible = False
        nf.cmbText.Visible = True
        nf.cmbText.Items.Clear()
        If UBound(StrScale) > 0 Then
            For i = 1 To UBound(StrScale)
                nf.cmbText.Items.Add(StrScale(i))
            Next
            nf.cmbText.Text = StrScale(1)
        End If
        nf.ShowDialog()
        sBiaoChiName = StrInputBoxCombText
        If sBiaoChiName <> "" And bCancelInput = 0 Then
            For i = 1 To Me.cmbBiaoChi.Items.Count
                If Me.cmbBiaoChi.Items(i - 1) = sBiaoChiName Then
                    MsgBox("���������������Ѿ����ڣ�����������!", , "��ʾ")
                    GoTo SBegin
                End If
            Next

            nf.labTitle.Text = "��ѡ��������:"
            nf.txtText.Visible = False
            nf.cmbText.Visible = True
            nf.cmbText.Items.Clear()
            If UBound(StrScale) > 0 Then
                For i = 1 To UBound(StrScale)
                    nf.cmbText.Items.Add(StrScale(i))
                Next
                nf.cmbText.Text = StrScale(1)
            End If

            nf.ShowDialog()
            sScaleName = StrInputBoxCombText

            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            For i = 2 To UBound(strPath)
                sFirSta = strPath(i - 1)
                SSecSTa = strPath(i)
                If nPriUpOrDown = 1 Then
                    sSecName = sFirSta & "->" & SSecSTa
                Else
                    sSecName = SSecSTa & "->" & sFirSta
                End If

                If nPriUpOrDown Mod 2 = 0 Then
                    sUpOrDown = "����"
                Else
                    sUpOrDown = "����"
                End If
                strTime = GetCurSectionRunTime(sScaleName, sSecName, sUpOrDown)

                str = "insert into �г����б����Ϣ (��·����,��������,��������,���,�������,����ʱ��) values ('" & _
                    strPriTrainJiaoLu & "', '" & _
                    sBiaoChiName & "', '" & _
                    sSecName & "', '" & _
                   i - 1 & "', '" & _
                  sScaleName & " ', '" & _
                   strTime & "')"
                Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                Mcom1.ExecuteNonQuery()
            Next
            MyConn.Close()
            Call listTrainQuJian()
            Call listSecBiaoChi(sBiaoChiName)
        End If
    End Sub

    Private Sub btnDeleteSecScale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSecScale.Click
        Dim str As String
        Dim sStyle As String
        sStyle = Me.cmbBiaoChi.Text.Trim
        If sStyle <> "" Then
            If MsgBox("ȷ��ɾ�� [" & sStyle & "] ����������?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "ȷ������") = MsgBoxResult.Yes Then
                Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
                If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                str = "delete * from �г����б����Ϣ where ��������='" & sStyle & "' and ��·����='" & strPriTrainJiaoLu & "'"
                Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                Mcom.ExecuteNonQuery()
                MyConn.Close()

                Call listTrainQuJian()
                If Me.cmbBiaoChi.Text <> "" Then
                    Call listSecBiaoChi(Trim(Me.cmbBiaoChi.Text))
                Else
                    Call listSecBiaoChi("")
                End If
            End If
        Else
            MsgBox("��ѡ��Ҫɾ������������!", , "��ʾ")
        End If
    End Sub

    Private Sub btnSaveSecScale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSecScale.Click
        Dim str As String
        Dim i As Integer
        Dim sStyle As String
        sStyle = Me.cmbBiaoChi.Text.Trim
        If sStyle <> "" Then
            Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            str = "delete * from �г����б����Ϣ where ��������='" & sStyle & "' and ��·����='" & strPriTrainJiaoLu & "'"
            Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
            Mcom.ExecuteNonQuery()
            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            For i = 1 To Me.grdQuJian.RowCount
                str = "insert into �г����б����Ϣ (��·����,��������,��������,���,�������,����ʱ��) values ('" & _
                strPriTrainJiaoLu & "', '" & _
                sStyle & "', '" & _
                Me.grdQuJian.Item(1, i - 1).Value & "', '" & _
               i & "', '" & _
                Me.grdQuJian.Item(2, i - 1).Value & "', '" & _
                Me.grdQuJian.Item(3, i - 1).Value & "')"
                Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                Mcom1.ExecuteNonQuery()
            Next
            MsgBox("�������!", , "��ʾ")
            MyConn.Close()
        Else
            MsgBox("��ѡ��Ҫ�������������!", , "��ʾ")
        End If
    End Sub

    Private Sub cmbBiaoChi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBiaoChi.SelectedIndexChanged
        Call listSecBiaoChi(Me.cmbBiaoChi.Text.Trim)
    End Sub

    Private Sub btnAutoCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoCreate.Click
        Call AutoCreateTrainSecScale(strPriTrainJiaoLu, nPriUpOrDown, strPath)
        Call AutoCreateTrainStaScale(strPriTrainJiaoLu, nPriUpOrDown, strPath)

        Call listTrainStopBiaoChi()
        If Me.cmbStopSta.Text <> "" Then
            Call listStopBiaoChi(Trim(Me.cmbStopSta.Text))
        End If

        Call listTrainQuJian()
        If Me.cmbBiaoChi.Text <> "" Then
            Call listSecBiaoChi(Me.cmbBiaoChi.Text.Trim)
        End If

        MsgBox("�Զ�������ϣ�")
    End Sub

    Private Sub grdStopSta_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdStopSta.CellClick
        Dim i As Integer
        Dim j As Integer
        If Me.grdStopSta.CurrentCell.RowIndex >= 0 And Me.grdStopSta.Columns(Me.grdStopSta.CurrentCell.ColumnIndex).Name = "�������" Then
            If CurCellX > 0 And CurCellY > 0 Then

                Me.cmbEditTime.Left = CurMouseX - CurCellX + Me.grdStopSta.Left
                Me.cmbEditTime.Top = CurMouseY - CurCellY + Me.grdStopSta.Top
                Me.cmbEditTime.Width = Me.grdStopSta.Columns(Me.grdStopSta.CurrentCell.ColumnIndex).Width
                Me.cmbEditTime.Items.Clear()

                Dim ifIn As Integer
                For i = 1 To UBound(StopScaleInf)
                    ifIn = 0
                    For j = 1 To Me.cmbEditTime.Items.Count
                        If StopScaleInf(i).sScaleName = Me.cmbEditTime.Items(j - 1).ToString Then
                            ifIn = 1
                            Exit For
                        End If
                    Next
                    If ifIn = 0 Then
                        Me.cmbEditTime.Items.Add(StopScaleInf(i).sScaleName)
                    End If
                Next
                Me.cmbEditTime.Text = Me.grdStopSta.CurrentCell.Value
                Me.cmbEditTime.Visible = True
                Me.cmbEditTime.Select()
                CurCellX = 0
                CurCellY = 0
            End If
        End If
    End Sub

    Private Sub grdStopSta_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdStopSta.CellMouseDown
        Me.cmbEditTime.Visible = False
        CurCellX = e.X
        CurCellY = e.Y
    End Sub

    Private Sub cmbEditTime_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEditTime.SelectedValueChanged
        Me.grdStopSta.CurrentCell.Value = Me.cmbEditTime.Text
        Call ValueChange()
        Me.cmbEditTime.Visible = False
    End Sub

    Private Sub grdStopSta_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdStopSta.MouseDown
        CurMouseX = e.X
        CurMouseY = e.Y
    End Sub

    Private Sub ValueChange()
        Dim CurStopTime As String
        Dim sUpOrDown As String
        Dim sStaName As String
        Dim sScaleName As String
        Dim sStaStartOrEnd As String
        If nPriUpOrDown Mod 2 = 0 Then
            sUpOrDown = "����"
        Else
            sUpOrDown = "����"
        End If
        sStaName = Me.grdStopSta.Item(1, Me.grdStopSta.CurrentCell.RowIndex).Value.ToString.Trim
        sScaleName = Me.grdStopSta.CurrentCell.Value.ToString.Trim
        If Me.grdStopSta.CurrentCell.RowIndex = 0 Then
            sStaStartOrEnd = "ʼ��վ"
        ElseIf Me.grdStopSta.CurrentCell.RowIndex = Me.grdStopSta.RowCount Then
            sStaStartOrEnd = "�յ�վ"
        Else
            sStaStartOrEnd = "�м�վ"
        End If
        CurStopTime = GetCurStationStopTime(sScaleName, sStaName, sUpOrDown, sStaStartOrEnd)
        Me.grdStopSta.Item(3, Me.grdStopSta.CurrentCell.RowIndex).Value = CurStopTime
    End Sub

    Private Sub grdQuJian_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdQuJian.CellClick
        Dim i As Integer
        Dim j As Integer
        If Me.grdQuJian.CurrentCell.RowIndex >= 0 And Me.grdQuJian.Columns(Me.grdQuJian.CurrentCell.ColumnIndex).Name = "�������" Then
            If CurCellX1 > 0 And CurCellY1 > 0 Then
                Me.cmbEditTime1.Left = CurMouseX1 - CurCellX1 + Me.grdQuJian.Left
                Me.cmbEditTime1.Top = CurMouseY1 - CurCellY1 + Me.grdQuJian.Top
                Me.cmbEditTime1.Width = Me.grdQuJian.Columns(Me.grdQuJian.CurrentCell.ColumnIndex).Width
                Me.cmbEditTime1.Items.Clear()

                Dim ifIn As Integer
                For i = 1 To UBound(SectionScaleInf)
                    ifIn = 0
                    For j = 1 To Me.cmbEditTime1.Items.Count
                        If SectionScaleInf(i).sScaleName = Me.cmbEditTime1.Items(j - 1).ToString Then
                            ifIn = 1
                            Exit For
                        End If
                    Next
                    If ifIn = 0 Then
                        Me.cmbEditTime1.Items.Add(SectionScaleInf(i).sScaleName)
                    End If
                Next
                Me.cmbEditTime1.Text = Me.grdQuJian.CurrentCell.Value
                Me.cmbEditTime1.Visible = True
                Me.cmbEditTime1.Select()
                CurCellX1 = 0
                CurCellY1 = 0
            End If
        End If

    End Sub

    Private Sub grdQuJian_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdQuJian.CellMouseDown
        Me.cmbEditTime1.Visible = False
        CurCellX1 = e.X
        CurCellY1 = e.Y
    End Sub

    Private Sub grdQuJian_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdQuJian.MouseDown
        CurMouseX1 = e.X
        CurMouseY1 = e.Y
    End Sub

    Private Sub cmbEditTime1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEditTime1.SelectedValueChanged
        Me.grdQuJian.CurrentCell.Value = Me.cmbEditTime1.Text
        Call ValueChange1()
        Me.cmbEditTime1.Visible = False
    End Sub

    Private Sub ValueChange1()
        Dim CurStopTime As String
        Dim sUpOrDown As String
        Dim sSecName As String
        Dim sScaleName As String
        If nPriUpOrDown Mod 2 = 0 Then
            sUpOrDown = "����"
        Else
            sUpOrDown = "����"
        End If
        sSecName = Me.grdQuJian.Item(1, Me.grdQuJian.CurrentCell.RowIndex).Value.ToString.Trim
        sScaleName = Me.grdQuJian.CurrentCell.Value.ToString.Trim
        CurStopTime = GetCurSectionRunTime(sScaleName, sSecName, sUpOrDown)
        Me.grdQuJian.Item(3, Me.grdQuJian.CurrentCell.RowIndex).Value = CurStopTime
    End Sub
End Class