Public Class frmEditSKBStaSeq

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmEditSKBStaSeq_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Me.lstBei.Items.Clear()
        For i = 1 To UBound(NotSameStationInf)
            Me.lstBei.Items.Add(NotSameStationInf(i))
        Next
        Call listcmbquduan()
    End Sub
    Private Sub listCmbQuDuan()
        Dim i As Integer
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim strTable2 As String = "select distinct  �������� from ʱ�̱�վ˳��"
        Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
        Dim myDataSet2 As Data.DataSet = New Data.DataSet
        Mydc2.Fill(myDataSet2, "ʱ�̱�վ˳��")
        Dim myTable2 As Data.DataTable
        myTable2 = myDataSet2.Tables("ʱ�̱�վ˳��")
        Me.cmbSecName.Items.Clear()
        For i = 1 To myTable2.Rows.Count
            Me.cmbSecName.Items.Add(Trim(myTable2.Rows(i - 1).Item("��������")))
        Next
        If Me.cmbSecName.Items.Count > 0 Then
            Me.cmbSecName.Text = Me.cmbSecName.Items(0)
            Call ListSta(Me.cmbSecName.Text)
        End If
    End Sub
    Private Sub ListSta(ByVal sSecName As String)
        Dim i As Integer
        Dim strTable2 As String = "select * from ʱ�̱�վ˳�� where ��������='" & sSecName & "' order by ���"
        Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
        Dim myDataSet2 As Data.DataSet = New Data.DataSet
        Mydc2.Fill(myDataSet2, "ʱ�̱�վ˳��")
        Dim myTable2 As Data.DataTable
        myTable2 = myDataSet2.Tables("ʱ�̱�վ˳��")
        Me.lstSta.Items.Clear()
        For i = 1 To myTable2.Rows.Count
            Me.lstSta.Items.Add(Trim(myTable2.Rows(i - 1).Item("��վ����")))
        Next
    End Sub


    Private Sub cmbSecName_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSecName.SelectedValueChanged
        Call ListSta(Me.cmbSecName.Text)
    End Sub

    Private Sub cmdAddOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddOne.Click
        Dim nSelectID As Integer
        nSelectID = Me.lstSta.SelectedIndex
        If Me.lstBei.SelectedIndex >= 0 Then
            Me.lstSta.Items.Insert(Me.lstSta.SelectedIndex + 1, Me.lstBei.SelectedItem)
            Me.lstSta.SelectedIndex = nSelectID + 1
            'Call AddItems(Me.lstBei.SelectedItem)
            If Me.lstBei.SelectedIndex <= Me.lstBei.Items.Count - 2 Then
                Me.lstBei.SelectedItem = Me.lstBei.Items(Me.lstBei.SelectedIndex + 1)
            End If
        End If
    End Sub

    Private Sub cmdAddAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddAll.Click
        Dim i As Integer
        If Me.lstBei.Items.Count > 0 Then
            For i = 1 To Me.lstBei.Items.Count
                Call AddItems(Me.lstBei.Items(i - 1))
            Next
        End If
    End Sub

    Private Sub cmdDeleOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleOne.Click
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

    Private Sub AddItems(ByVal txtName As String)
        'If IFThisItemExist(txtName) = 0 Then
        Me.lstSta.Items.Add(txtName)
        ' End If
    End Sub

    Private Sub cmdDeleAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDeleAll.Click
        Me.lstSta.Items.Clear()
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
    End Sub

    Private Sub CmdDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdDown.Click
        Dim nCurSelID As Integer
        nCurSelID = Me.lstSta.SelectedIndex
        If nCurSelID >= 0 And nCurSelID < Me.lstSta.Items.Count - 1 Then
            Me.lstSta.Items.Insert(nCurSelID + 2, Me.lstSta.SelectedItem)
            Me.lstSta.Items.RemoveAt(nCurSelID)
            Me.lstSta.SelectedIndex = nCurSelID + 1
        Else
            If nCurSelID = 0 Then
                Me.lstSta.SelectedIndex = 0
            Else
                Me.lstSta.SelectedIndex = Me.lstSta.Items.Count - 1
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim Str As String
        Dim sQuDuanName As String
        sQuDuanName = Me.cmbSecName.Text.Trim()
        If sQuDuanName = "" Then
            MsgBox("�������Ʋ���Ϊ�գ�����ѡ������룡")
            Exit Sub
        End If
        If Me.lstSta.Items.Count <= 0 Then
            MsgBox("����ѡ��վ��")
            Exit Sub
        End If
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Str = "delete * from  ʱ�̱�վ˳�� where ��������='" & Me.cmbSecName.Text.Trim & "'"
        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()
        Dim i As Integer
        For i = 1 To Me.lstSta.Items.Count
            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            Str = "insert into ʱ�̱�վ˳�� (��������,���,��վ����) values ('" & _
            Me.cmbSecName.Text.Trim & "', '" & _
            i & "', '" & _
            Me.lstSta.Items(i - 1) & "')"
            Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
            Mcom1.ExecuteNonQuery()
        Next
        MyConn.Close()
        MsgBox("������ϣ�")
        Call listCmbQuDuan()
        Call ReadSKBStaSeqData()
    End Sub

    Private Sub btnDeleteQuDuan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteQuDuan.Click
        Dim Str As String
        Dim sQuDuanName As String
        sQuDuanName = Me.cmbSecName.Text.Trim()
        If sQuDuanName = "" Then
            MsgBox("�������Ʋ���Ϊ�գ�����ѡ������룡")
            Exit Sub
        End If
        If MsgBox("�Ƿ�ɾ�����������ƣ�!", MsgBoxStyle.Exclamation + MsgBoxStyle.OKCancel + MsgBoxStyle.DefaultButton2, "ȷ��ɾ��") = MsgBoxResult.OK Then
            Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            Str = "delete * from  ʱ�̱�վ˳�� where ��������='" & sQuDuanName & "'"
            Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
            Mcom.ExecuteNonQuery()
            MyConn.Close()
            MsgBox("ɾ����ϣ�")
            Call listCmbQuDuan()
        End If
        Call ReadSKBStaSeqData()
    End Sub

    Private Sub lstBei_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBei.DoubleClick
        Call cmdAddOne_Click(Nothing, Nothing)
    End Sub

    Private Sub lstSta_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSta.DoubleClick
        Call cmdDeleOne_Click(Nothing, Nothing)
    End Sub

End Class