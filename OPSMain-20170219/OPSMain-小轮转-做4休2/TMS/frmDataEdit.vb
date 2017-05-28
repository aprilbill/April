Public Class frmDataEdit
    Public sCurTableName As String

    Private Sub frmDataEdit_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MsgBox("�˳�ǰ�뱣���Ѿ��޸ĵ���Ϣ! ȷ���˳���ǰ������", MsgBoxStyle.Question + MsgBoxStyle.OkCancel, "ȷ�ϲ���") = MsgBoxResult.Cancel Then
            e.Cancel = True
        End If
    End Sub
    Private Sub frmDataEdit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "����ά������" & sCurTableName
        If sCurTableName = "��վ���ʱ��" Or sCurTableName = "׷�ټ��ʱ��" Or sCurTableName = "��·������Ϣ" Then
            Me.btnAutoCreate.Visible = True
        Else
            Me.btnAutoCreate.Visible = False
        End If
        Call DataRefresh()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Call OutPutToEXCELFileFormDataGrid(sCurTableName, Me.dataView, Me)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim i As Integer
        Dim j As Integer
        Dim myWS As DAO.Workspace
        Dim DE As DAO.DBEngine = New DAO.DBEngine
        myWS = DE.Workspaces(0)
        Dim DBdata As DAO.Database
        Dim RSdata As DAO.Recordset
        DBdata = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        RSdata = DBdata.OpenRecordset(sCurTableName)
        Dim nErrH As Integer
        Dim nErrL As Integer
        Try
            If Me.dataView.RowCount > 0 Then
                labInfor.Visible = True
                labInfor.Text = "���ڸ������ݣ����Ժ�..,"
                Dim StrExe As String
                StrExe = "delete * from " & sCurTableName & ""
                DBdata.Execute(StrExe) ''" & Me.cmbTableName.Text & "'")
                RSdata = DBdata.OpenRecordset(sCurTableName)
                Me.labInfor.Visible = False
                Me.proBar.Visible = True
                Me.proBar.Maximum = Me.dataView.RowCount
                For i = 1 To Me.dataView.RowCount - 1
                    RSdata.AddNew()
                    For j = 1 To Me.dataView.ColumnCount - 1
                        nErrH = i
                        nErrL = j + 1
                        RSdata.Fields(j).Value = Trim(Me.dataView.Rows(i - 1).Cells(j).Value)
                    Next j
                    RSdata.Update()
                    Me.proBar.Value = i
                Next i
                Me.proBar.Visible = False
                MsgBox("������ϣ���", , "��ʾ")

                Call DataRefresh()
            Else
                MsgBox("����л�û�����ݣ�������ӣ�", , "��ʾ")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("��" & nErrH & "��" & "��" & nErrL & "�е����ݸ�ʽ���ԣ����������룡��")
            Me.dataView.CurrentCell = Me.dataView.Rows(nErrH - 1).Cells(nErrL - 1)
        End Try
        Me.proBar.Visible = False
        Me.labInfor.Text = ""

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub DataRefresh()
        Dim strTable3 As String = "select * from " & sCurTableName & ""
        Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
        '����һ��Datasetd
        Dim myDataSet3 As Data.DataSet = New Data.DataSet
        Mydc3.Fill(myDataSet3, sCurTableName)
        Dim myTable3 As Data.DataTable
        myTable3 = myDataSet3.Tables(sCurTableName)
        Me.dataView.DataSource = myTable3
    End Sub

    Private Sub BtnInputExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnInputExcel.Click

        Dim New0penFile As New OpenFileDialog
        Dim strExcelFilePath As String
        'New0penFile.InitialDirectory = CurDir()
        New0penFile.Filter = "xls files (*.xls)|*.xls|All files (*.*)|*.*"
        New0penFile.FilterIndex = 1
        New0penFile.RestoreDirectory = True
        strExcelFilePath = ""

        If New0penFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strExcelFilePath = New0penFile.FileName
        End If
        '������ݿ������
        If strExcelFilePath <> "" Then

            'Dim DS As System.Data.DataSet
            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
            Dim MyConnection As System.Data.OleDb.OleDbConnection

            MyConnection = New System.Data.OleDb.OleDbConnection( _
                          "provider=Microsoft.Jet.OLEDB.4.0; " & _
                          "data source='" & strExcelFilePath & "'; " & _
                          "Extended Properties=Excel 8.0;")
            ' Select the data from Sheet1 of the workbook.
            Dim tmpStr As String
            tmpStr = "select * from" & "[" & sCurTableName & "$]"
            MyCommand = New System.Data.OleDb.OleDbDataAdapter(tmpStr, MyConnection)

            ' Open connection with the database.
            MyConnection.Open()

            Dim objDataset1 As New DataSet

            ' Fill the DataSet with the information from the worksheet.
            Try
                MyCommand.Fill(objDataset1, "XLData")
                Me.dataView.DataSource = objDataset1.Tables(0)
                MsgBox("����ȷ���������ݵ���ȷ�ԣ�", , "��ʾ")
            Catch ex As Exception
                MsgBox("EXCEL���ݿⲻ��ȷ����ȷ���򿪵����ݿ��ʽ��ȷ!", , "��ʾ")
            End Try
            MyConnection.Close()
            GC.Collect()
        End If
    End Sub


    Private Sub btnAutoCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoCreate.Click
        Select Case sCurTableName
            Case "��վ���ʱ��"

                If MsgBox("�ò��������������ɳ�վ���ʱ�䣬ԭ��¼�����Ϣ�����޸Ļ�ɾ���������ȷ�ϣ�", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "ȷ�ϲ���") = MsgBoxResult.Cancel Then Exit Sub

                Dim Str As String
                Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
                If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                Dim sCurName As String
                sCurName = "��վ���ʱ��"
                If MsgBox("�Ƿ�ɾ��ԭ�������ݣ�", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "ȷ��ɾ��") = MsgBoxResult.Yes Then
                    Str = "delete * from " & sCurName & ""
                    Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                    Mcom1.ExecuteNonQuery()
                End If

                Dim i As Integer
                Dim j As Integer
                Dim k As Integer
                Dim sFirSta As String
                Dim notSameSta() As String
                ReDim notSameSta(0)
                Dim IfExistIn As Boolean
                For i = 1 To UBound(NetInf.Line)
                    If UBound(NetInf.Line(i).Station) > 0 Then
                        For j = 1 To UBound(NetInf.Line(i).Station)
                            IfExistIn = False
                            For k = 1 To UBound(notSameSta)
                                If NetInf.Line(i).Station(j).sStaName = notSameSta(k) Then
                                    IfExistIn = True
                                    Exit For
                                End If
                            Next
                            If IfExistIn = False Then
                                ReDim Preserve notSameSta(UBound(notSameSta) + 1)
                                notSameSta(UBound(notSameSta)) = NetInf.Line(i).Station(j).sStaName
                            End If
                        Next j
                    End If
                Next i

                For i = 1 To UBound(notSameSta)
                    sFirSta = notSameSta(i)
                    Str = "insert into " & sCurName & " (���,��վ����) values (" & _
                                               i & ", '" & _
                                               sFirSta & "')"
                    If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                    Dim Mcom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                    Mcom2.ExecuteNonQuery()
                Next
                MyConn.Close()
                Call DataRefresh()

            Case "׷�ټ��ʱ��"

                If MsgBox("�ò��������������ɳ�վ���ʱ�䣬ԭ��¼�����Ϣ�����޸Ļ�ɾ���������ȷ�ϣ�", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "ȷ�ϲ���") = MsgBoxResult.Cancel Then Exit Sub

                Dim Str As String
                Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
                If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                Dim sCurName As String
                sCurName = "׷�ټ��ʱ��"
                If MsgBox("�Ƿ�ɾ��ԭ�������ݣ�", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "ȷ��ɾ��") = MsgBoxResult.Yes Then
                    Str = "delete * from " & sCurName & ""
                    Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                    Mcom1.ExecuteNonQuery()
                End If

                Dim i As Integer
                Dim j As Integer
                Dim k As Integer
                Dim sFirSta As String
                Dim notSameSta() As String
                ReDim notSameSta(0)
                Dim IfExistIn As Boolean
                For i = 1 To UBound(NetInf.Line)
                    If UBound(NetInf.Line(i).Station) > 0 Then
                        For j = 1 To UBound(NetInf.Line(i).Station)
                            IfExistIn = False
                            For k = 1 To UBound(notSameSta)
                                If NetInf.Line(i).Station(j).sStaName = notSameSta(k) Then
                                    IfExistIn = True
                                    Exit For
                                End If
                            Next
                            If IfExistIn = False Then
                                ReDim Preserve notSameSta(UBound(notSameSta) + 1)
                                notSameSta(UBound(notSameSta)) = NetInf.Line(i).Station(j).sStaName
                            End If
                        Next j
                    End If
                Next i

                For i = 1 To UBound(notSameSta)
                    sFirSta = notSameSta(i)
                    Str = "insert into " & sCurName & " (���,��վ����,����,ͬ�򷢷�) values (" & _
                                               i & ", '" & _
                                              sFirSta & "', '" & _
                                              "�Ϳ�" & "', '" & _
                                               "1.30" & " ')"
                    If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                    Dim Mcom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                    Mcom2.ExecuteNonQuery()
                Next
                MyConn.Close()
                Call DataRefresh()

            Case "��·������Ϣ"

                If MsgBox("�ò���������������������Ϣ��ԭ��¼�����Ϣ�����޸Ļ�ɾ���������ȷ�ϣ�", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "ȷ�ϲ���") = MsgBoxResult.Cancel Then Exit Sub

                Dim Str As String
                Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
                If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                Dim sCurName As String
                sCurName = "��·������Ϣ"
                If MsgBox("�Ƿ�ɾ��ԭ�������ݣ�", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "ȷ��ɾ��") = MsgBoxResult.Yes Then
                    Str = "delete * from " & sCurName & ""
                    Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                    Mcom1.ExecuteNonQuery()
                End If

                Dim i As Integer
                Dim j As Integer
                Dim sFirSta As String
                Dim sSecSta As String
                Dim sSecName As String
                Dim sBiSeStyle As String
                Dim DownLength As String
                Dim UpLength As String
                Dim secNum As String
                sBiSeStyle = "�Զ�����"
                DownLength = "2"
                UpLength = "2"
                secNum = "2"
                For i = 1 To UBound(NetInf.Line)
                    For j = 2 To UBound(NetInf.Line(i).Station)
                        sFirSta = NetInf.Line(i).Station(j - 1).sStaName
                        sSecSta = NetInf.Line(i).Station(j).sStaName
                        sSecName = sFirSta & "->" & sSecSta
                        Str = "insert into " & sCurName & " (������,��·����,��������,������ʼվ,�����յ�վ,��������,�����������,�����������,������Ŀ) values (" & _
                                                   j - 1 & ", '" & _
                                                    NetInf.Line(i).sName & "', '" & _
                                                    sSecName & "', '" & _
                                                    sFirSta & "', '" & _
                                                    sSecSta & "', '" & _
                                                    sBiSeStyle & "', '" & _
                                                    DownLength & "', '" & _
                                                    UpLength & "', '" & _
                                                    secNum & "')"
                        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                        Dim Mcom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                        Mcom2.ExecuteNonQuery()
                    Next
                Next
                MyConn.Close()
                Call DataRefresh()
                Call readNetData()
        End Select
    End Sub
End Class