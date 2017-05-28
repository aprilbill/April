Public Class frmDiTuStructureManager
    Dim tmpName() As String

    Private Sub frmDiTuStructureManager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call ListItems()
    End Sub
    Private Sub ListItems()
        Dim myWS As DAO.Workspace
        Dim DE As DAO.DBEngine = New DAO.DBEngine
        myWS = DE.Workspaces(0)

        Dim dbData As DAO.Database
        Dim RSdata As DAO.Recordset
        Dim RSdata1 As DAO.Recordset
        Dim i As Integer, j As Integer
        Dim nNum As Integer
        Dim bFind As Boolean
        ReDim tmpName(0)
        Dim ifFind As Integer
        Me.listViewName.Items.Clear()

        bFind = False
        ifFind = 0
        dbData = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        RSdata1 = dbData.OpenRecordset("select distinct ��ͼ���� from ��ͼ�ṹ")
        If RSdata1.RecordCount > 0 Then
            RSdata1.MoveLast()
            nNum = RSdata1.RecordCount
        End If
        Dim lvItem As ListViewItem
        Dim liFile(1) As String
        If nNum > 0 Then
            RSdata1.MoveFirst()
            ReDim tmpName(nNum)
            For i = 1 To nNum
                tmpName(i) = RSdata1.Fields("��ͼ����").Value.ToString.Trim
                RSdata1.MoveNext()
            Next

            RSdata = dbData.OpenRecordset("select * from ��ͼ�ṹ order by ID")
            If RSdata.RecordCount > 0 Then
                RSdata.MoveLast()
                nNum = RSdata.RecordCount
            End If
            If nNum > 0 Then
                For j = 1 To UBound(tmpName)
                    RSdata.MoveFirst()
                    For i = 1 To nNum
                        If tmpName(j) = RSdata.Fields("��ͼ����").Value Then
                            liFile(0) = tmpName(j)
                            If RSdata.Fields("�Ƿ�Ĭ��").Value.ToString.Trim = "1" Then
                                liFile(1) = "Ĭ��"
                            Else
                                liFile(1) = "��"
                            End If
                            lvItem = New ListViewItem(liFile)
                            Me.listViewName.Items.Add(lvItem)
                            Exit For
                        End If
                        RSdata.MoveNext()
                    Next
                Next
            End If
            myWS.Close()
        Else
            MsgBox("�Բ���,�㻹����ӵ�ͼ�ṹ,�������!", , "��ʾ")
            Exit Sub
        End If

    End Sub
    Private Sub btnRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRename.Click

        ' Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)

        Dim i As Integer
        Dim Str As String
        Dim nf As New frmInputBox

        Dim sNewName As String
        Dim sName As String
        If Me.listViewName.SelectedItems.Count > 0 Then
            sName = Me.listViewName.SelectedItems(0).SubItems(0).Text
            nf.Text = "�����ͼ����"
            nf.labTitle.Text = "�����ѡ���ͼ����:"
            nf.cmbText.Visible = True
            nf.txtText.Visible = False
            nf.txtText.Text = ""
            nf.cmbText.Items.Clear()
            If UBound(tmpName) > 0 Then
                For i = 1 To UBound(tmpName)
                    nf.cmbText.Items.Add(tmpName(i))
                Next
                nf.cmbText.Text = sName
            End If
sFirst:
            nf.ShowDialog()
            If StrInputBoxCombText <> "" And bCancelInput = 0 Then
                sNewName = StrInputBoxCombText
                If sName = sNewName Then
                    MsgBox("��ͼ������ͬ,����������!", , "��ʾ")
                    GoTo sFirst
                Else
                    Dim strTable1 As String = "select * from ��ͼ�ṹ where ��ͼ����='" & sNewName & "'"
                    Dim Mydc1 As New Data.OleDb.OleDbDataAdapter(strTable1, strCon)
                    '����һ��Datasetd
                    Dim myDataSet1 As Data.DataSet = New Data.DataSet
                    Mydc1.Fill(myDataSet1, "��ͼ�ṹ")
                    Dim myTable1 As Data.DataTable
                    myTable1 = myDataSet1.Tables("��ͼ�ṹ")
                    If myTable1.Rows.Count > 0 Then
                        If MsgBox("�õ�ͼ�����Ѿ����ڣ��Ƿ񸲸�ԭ�������ƣ�", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "ȷ������") = MsgBoxResult.Yes Then
                            Str = "delete * from  ��ͼ�ṹ where ��ͼ����='" & sNewName & "'"
                            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                            Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                            Mcom1.ExecuteNonQuery()

                        Else
                            GoTo sFirst
                        End If
                    Else
                    End If
                    If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                    Str = "update ��ͼ�ṹ set  ��ͼ����='" & sNewName & "' where ��ͼ����='" & sName & "'"
                    Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                    Mcom.ExecuteNonQuery()
                    MyConn.Close()
                    Call ListItems()
                End If
            End If
        Else
            MsgBox("����ѡ��Ҫ�޸ĵĵ�ͼ����!", , "��ʾ")
        End If

    End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim sName As String
        Dim Str As String
        If Me.listViewName.SelectedItems.Count > 0 Then
            sName = Me.listViewName.SelectedItems(0).SubItems(0).Text
            If MsgBox("ȷ��ɾ�� [" & sName & "] ��ͼ�ṹ��?", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "ȷ������") = MsgBoxResult.Yes Then
                Str = "delete * from  ��ͼ�ṹ where ��ͼ����='" & sName & "'"
                If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                Mcom1.ExecuteNonQuery()
                MyConn.Close()
                Call ListItems()
            End If
        Else
            MsgBox("����ѡ��Ҫɾ���ĵ�ͼ����!", , "��ʾ")
        End If
    End Sub

    Private Sub btnSetDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDefault.Click
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim sName As String
        Dim Str As String
        If Me.listViewName.SelectedItems.Count > 0 Then
            sName = Me.listViewName.SelectedItems(0).SubItems(0).Text
            If MsgBox("ȷ������ [" & sName & "] ΪĬ�ϵ�ͼ�ṹ��?", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "ȷ������") = MsgBoxResult.Yes Then
                Str = "update ��ͼ�ṹ set �Ƿ�Ĭ��='0' where �Ƿ�Ĭ��='1'"
                If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                Mcom1.ExecuteNonQuery()

                Str = "update ��ͼ�ṹ set �Ƿ�Ĭ��='1' where  ��ͼ����='" & sName & "'"
                If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                Dim Mcom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                Mcom2.ExecuteNonQuery()
                'MyConn.ResetState()
                MyConn.Close()
                Call ListItems()
            End If
        Else
            MsgBox("����ѡ��Ҫ���õĵ�ͼ����!", , "��ʾ")
        End If
    End Sub


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class