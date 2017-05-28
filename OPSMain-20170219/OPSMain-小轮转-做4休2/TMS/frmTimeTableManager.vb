Public Class frmTimeTableManager
    Dim sExistSKB() As String
    Dim dSKBFileName As DAO.Database

    Private Sub frmTimeTableManager_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'Call frmTimeTableMain.listTitle()
    End Sub
    'Dim sUsedSkbNameKe As String

    Private Sub frmTimeTableManager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim myWS As dao.Workspace
        Dim DE As dao.DBEngine = New dao.DBEngine
        myWS = DE.Workspaces(0)
        dSKBFileName = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        Me.txtCurSKB.Text = TimeTablePara.sPubCurSkbName
        Call InputTimetableInf()
        Call SetListSkbName()

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If CheckTimetableBadError() = False Then
            Exit Sub
        End If

        Dim tmpSKBID As String
        tmpSKBID = Now().Year & Now().Month & Now().Day & "_" & Now().Hour & Now().Minute & Now().Second
        Dim sUsedSkbNameKe As String
        sUsedSkbNameKe = Me.txtCurSKB.Text.Trim
        Call SaveSkbName(sUsedSkbNameKe, tmpSKBID)
        'Call frmTimeTableMain.ListFrmTimeTabaleTitle()
    End Sub

    Sub SetListSkbName()
        Dim i As Integer
        Me.lstSKB.Items.Clear()
        For i = 1 To UBound(TimetableInf)
            Me.lstSKB.Items.Add(TimetableInf(i).sName)
        Next i
    End Sub

    '����ʱ�̱�
    Public Sub SaveSkbName(ByVal tmpTableName As String, ByVal tmpSKBID As String)
        Dim i As Integer
        If tmpTableName = "" Then
sName:
            Dim nf As New frmInputBox
            nf.cmbText.Visible = True
            nf.txtText.Visible = False
            nf.Text = "����ʱ�̱�"
            nf.labTitle.Text = "������ʱ�̱����ƣ�"
            nf.cmbText.Text = tmpTableName
            nf.cmbText.Items.Clear()
            For i = 1 To UBound(TimetableInf)
                nf.cmbText.Items.Add(TimetableInf(i).sName)
            Next i
            nf.ShowDialog()
            tmpTableName = StrInputBoxCombText

            'If IfNameTrue(tmpTableName) = False Then
            '    MsgBox("�������������в����к� ( , ) , - ,�ո�,���ַ�����")
            '    GoTo sname
            'End If
            If tmpTableName <> "" And bCancelInput = 0 Then
                If MsgBox("ȷ�Ͻ�ʱ�̴���ʱ�̱� " & tmpTableName & "��", 32 + 4, "ȷ��") = vbYes Then
                    SaveSkb(tmpTableName, tmpSKBID)
                    Me.txtCurSKB.Text = tmpTableName
                Else
                    MsgBox("û�н���ʱ�̱���", vbInformation)
                    Exit Sub
                End If
            Else
                MsgBox("û�н���ʱ�̱���", vbInformation)
                Exit Sub
            End If
        Else
            SaveSkb(tmpTableName, tmpSKBID)
            Me.txtCurSKB.Text = tmpTableName
        End If

    End Sub

    Private Function bFindSkb(ByVal sSKBName As String) As Boolean
        'Call SetExistSKBName()
        Dim i As Integer
        bFindSkb = False
        For i = 1 To UBound(TimetableInf)
            If TimetableInf(i).sName = sSKBName Then
                bFindSkb = True
                Exit For
            End If
        Next i
    End Function

    Sub SaveSkb(ByVal tmpTableName As String, ByVal tmpSKBID As String)
        Dim tmpId As Integer
        If tmpTableName <> "" Then
            If bFindSkb(tmpTableName) <> True Then
                Call CreatTimeTableTable(tmpSKBID)
                Call CreatNewCDUseTable(tmpSKBID)
                ReDim Preserve TimetableInf(UBound(TimetableInf) + 1)
                TimetableInf(UBound(TimetableInf)).sName = tmpTableName
                TimetableInf(UBound(TimetableInf)).sID = tmpSKBID
                TimetableInf(UBound(TimetableInf)).sCreateDate = Now()
                TimetableInf(UBound(TimetableInf)).sEditDate = Now()
            Else
                If MsgBox("ʱ�̱�" & tmpTableName & "�Ѿ����ڣ��Ƿ񸲸�ʱ�̱�ԭ��������!", vbQuestion + vbOKCancel + vbDefaultButton2, "����ʱ�̱�") = vbCancel Then
                    Exit Sub
                Else
                    tmpId = GetTimetableInfID(tmpTableName)
                    If tmpId > 0 Then
                        tmpSKBID = TimetableInf(tmpId).sID
                        TimetableInf(tmpId).sEditDate = Now()
                    Else
                        Exit Sub
                    End If
                End If
            End If
            ' Call SaveSkbTimeTable(tmpTableName, Me.probar)
            Call SaveSkbTimeTableByDAO(tmpSKBID, Me.ProBar)
            TimeTablePara.sPubCurSkbName = tmpTableName
            ProBar.Visible = False
            Call SetListSkbName()
            Call SaveTimetableInf()
        End If
    End Sub

    Sub DeleteSKB(ByVal tmpTableName As String)
        Dim NewTrnTime As DAO.Recordset
        Dim Nt As Integer
        Dim i As Integer
        NewTrnTime = dSKBFileName.OpenRecordset(tmpTableName & "�г�ʱ����Ϣ")
        If NewTrnTime.RecordCount > 0 Then
            NewTrnTime.MoveLast()
            Nt = NewTrnTime.RecordCount
        End If
        If Nt > 0 Then
            NewTrnTime.MoveFirst()
            For i = 1 To Nt
                NewTrnTime.Delete()
                NewTrnTime.MoveNext()
            Next i
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Me.txtCurSKB.Text.Trim <> "" Then
            SaveAsSkbName(Me.txtCurSKB.Text.Trim)
        End If
    End Sub

    Sub SaveAsSkbName(ByVal tmpUsedTableName As String)
        Dim sTmpDefaultName As String
        Dim i As Integer
        Dim tmpSKBID As String
        tmpSKBID = Now().Year & Now().Month & Now().Day & "_" & Now().Hour & Now().Minute & Now().Second

        If tmpUsedTableName <> "" Then
sname:
            Dim nf As New frmInputBox
            sTmpDefaultName = tmpUsedTableName
            nf.Text = "���ʱ�̱�"
            nf.labTitle.Text = "��ʱ�̱�" & tmpUsedTableName & "���Ϊ��"
            nf.txtText.Visible = False
            nf.cmbText.Visible = True
            nf.cmbText.Items.Clear()
            For i = 1 To UBound(TimetableInf)
                nf.cmbText.Items.Add(TimetableInf(i).sName)
            Next i

            nf.cmbText.Text = tmpUsedTableName
            nf.ShowDialog()
            If bCancelInput = 1 Then
                Exit Sub
            End If

            'If IfNameTrue(StrInputBoxCombText) = False Then
            '    MsgBox("�������������в����к� ( , ) , -��; �ո�,���ַ�����")
            '    GoTo sname
            'End If
        End If
        If StrInputBoxCombText <> "" Then
            If MsgBox("�����ʱ�̱�[" & StrInputBoxCombText & "]", 32 + 4, "���ʱ�̱�") = vbYes Then
                SaveSkb(StrInputBoxCombText, tmpSKBID)
                Me.txtCurSKB.Text = StrInputBoxCombText
            End If
        End If
        Exit Sub
sEnd:
        Exit Sub
    End Sub

    Private Sub btnRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRename.Click
        Dim sChosenSkbNameKe As String
        sChosenSkbNameKe = Me.lstSKB.SelectedItem
        If sChosenSkbNameKe <> "" Then
            ReNameSkbName(sChosenSkbNameKe)
            sChosenSkbNameKe = ""
            Exit Sub
        End If
        If Trim(sChosenSkbNameKe) = "" Then
            MsgBox("����ѡ����Ҫ���������г�ʱ�̱�", 48, "ʱ�̱����")
        End If
    End Sub

    '������
    Sub ReNameSkbName(ByVal tmpChosenSkbName As String)
        Dim stmpUsedSkbName As String
        Dim tmpTableName As String
        Dim i As Integer
        stmpUsedSkbName = Me.txtCurSKB.Text.Trim
        If stmpUsedSkbName <> tmpChosenSkbName Then
sname:
            Dim nf As New frmInputBox
            nf.Text = "������ʱ�̱�"
            nf.labTitle.Text = "��ʱ�̱�" & tmpChosenSkbName & "������Ϊ��"
            nf.txtText.Visible = False
            nf.cmbText.Visible = True
            nf.cmbText.Items.Clear()
            For i = 1 To UBound(TimetableInf)
                nf.cmbText.Items.Add(TimetableInf(i).sName)
            Next i

            nf.cmbText.Text = tmpChosenSkbName
            nf.ShowDialog()
            tmpTableName = StrInputBoxCombText
            If tmpTableName <> "" And bCancelInput = 0 Then
                If bFindSkb(tmpTableName) = True Then
                    MsgBox("��ʱ�̱������Ѿ����ڣ������¸���ʱ�̱����ơ�", 32, "��������")
                Else
                    If MsgBox("�Ƿ����" & tmpChosenSkbName & "Ϊ" & tmpTableName & "��", 32 + 4, "��������") = vbYes Then
                        RenameSkb(tmpChosenSkbName, tmpTableName)
                    End If
                End If
            End If
        Else
            MsgBox("ʱ�̱�" & stmpUsedSkbName & "����ʹ�ã����ܸ������ơ�", 48, "ʱ�̱����")
        End If
    End Sub

    Sub RenameSkb(ByVal OriTableName As String, ByVal NewTablename As String)
        Dim i As Integer
        For i = 1 To UBound(TimetableInf)
            If TimetableInf(i).sName = OriTableName Then
                TimetableInf(i).sName = NewTablename
                Exit For
            End If
        Next
        Call SetListSkbName()
        Call SaveTimetableInf()
  
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Dim sChosenSkbNameKe As String
        sChosenSkbNameKe = Me.lstSKB.SelectedItem
        If sChosenSkbNameKe <> "" Then
            If MsgBox("�Ƿ���" & sChosenSkbNameKe & "��", 32 + 4, "����ʱ�̱�") = vbYes Then
                CopySkbName(sChosenSkbNameKe)
            End If
        Else
            MsgBox("��ʱ�̱�ѡ�У���ѡ��ʱ�̱�", 48, "ʱ�̱����")
        End If
    End Sub

    Sub CopySkbName(ByVal stmpChosenSkbName As String)
        Dim tmpTableName As String
        Dim i As Integer
sname:
        Dim nf As New frmInputBox
        nf.Text = "����ʱ�̱�"
        nf.labTitle.Text = "�����뽫ʱ�̱� " & stmpChosenSkbName & "����Ϊ��ʱ�̱����ƣ�"
        nf.txtText.Visible = False
        nf.cmbText.Visible = True
        nf.cmbText.Items.Clear()
        For i = 1 To UBound(TimetableInf)
            nf.cmbText.Items.Add(TimetableInf(i).sName)
        Next i
        nf.cmbText.Text = stmpChosenSkbName
        nf.ShowDialog()
        If bCancelInput = 1 Then Exit Sub
        tmpTableName = StrInputBoxCombText

        If bFindSkb(tmpTableName) = True Then
            MsgBox("��ʱ�̱������Ѿ����ڣ���������������")
            Exit Sub
        Else
            If tmpTableName <> "" Then
                CopySkb(stmpChosenSkbName, tmpTableName)
            End If
        End If
    End Sub

    Sub CopySkb(ByVal OriTableName As String, ByVal NewTablename As String)
        '   Dim i, j As Integer
        Dim sOldID As String
        Dim sNewID As String
        Dim tmpSKBID As String
        Dim dateNow As Date
        dateNow = Now()
        tmpSKBID = Now().Year & Now().Month & Now().Day & "_" & Now().Hour & Now().Minute & Now().Second
        ReDim Preserve TimetableInf(UBound(TimetableInf) + 1)
        TimetableInf(UBound(TimetableInf)).sName = NewTablename
        TimetableInf(UBound(TimetableInf)).sID = tmpSKBID
        TimetableInf(UBound(TimetableInf)).sCreateDate = dateNow
        TimetableInf(UBound(TimetableInf)).sEditDate = dateNow

        Call CreatTimeTableTable(tmpSKBID)
        Call CreatNewCDUseTable(tmpSKBID)
        Dim NewName As String
        Dim oldName As String
        Dim Str As String
        sNewID = tmpSKBID
        sOldID = GetTimetableIDFromName(OriTableName)
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        NewName = sNewID & "����ʹ�÷���"
        oldName = sOldID & "����ʹ�÷���"
        Str = "insert into " & NewName & " select * from " & oldName & ""
        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()

        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        NewName = sNewID & "�г�ʱ����Ϣ"
        oldName = sOldID & "�г�ʱ����Ϣ"
        Str = "insert into " & NewName & " select * from " & oldName & ""
        Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom1.ExecuteNonQuery()
        MyConn.Close()
        Call SetListSkbName()
        Call SaveTimetableInf()
        MsgBox("ʱ�̱�����ϣ�")
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim sChosenSkbNameKe As String
        sChosenSkbNameKe = Me.lstSKB.SelectedItem
        If Trim(sChosenSkbNameKe) = "" Then
            MsgBox("��ʱ�̱�ѡ�У���ѡ��ʱ�̱�", 48, "ʱ�̱����")
        End If
        If Trim(sChosenSkbNameKe) <> "" Then
            If MsgBox("�Ƿ�ɾ��" & sChosenSkbNameKe & ",���ɾ������ʱ�̱���Ϣ�����ٴ��ڣ�����", 32 + 4, "ɾ��ʱ�̱�?") = vbYes Then
                If sChosenSkbNameKe = TimeTablePara.sPubCurSkbName Then
                    MsgBox("ʱ�̱�" & TimeTablePara.sPubCurSkbName & "����ʹ�ã�����ɾ��", 0 + 48)
                Else
                    If MsgBox("���Ҫɾ��" & sChosenSkbNameKe & "��", 32 + 4, "ɾ��ʱ�̱���������!") = vbYes Then
                        DelSkb(sChosenSkbNameKe)
                    Else
                        Exit Sub
                    End If
                End If
            Else
                Exit Sub
            End If
        End If
    End Sub

    Sub DelSkb(ByVal tmpTableName As String)
        tmpTableName = GetTimetableIDFromName(tmpTableName)
        Try
            Dim i As Integer
            dSKBFileName.TableDefs.Refresh()
            Dim DB As dao.Database
            DB = dSKBFileName
            Dim tempName As String
            For i = 0 To DB.TableDefs.Count - 2
                tempName = DB.TableDefs(i).Name
                If Len(tempName) > 6 Then
                    If tempName.Substring(0, tempName.Length - 6) = tmpTableName Then
                        DB.TableDefs.Delete(tempName)
                        Exit For
                    End If
                End If
            Next i
            DB.TableDefs.Refresh()

            For i = 0 To DB.TableDefs.Count - 1
                tempName = DB.TableDefs(i).Name
                If Len(tempName) > 6 Then
                    If tempName.Substring(0, tempName.Length - 6) = tmpTableName Then
                        DB.TableDefs.Delete(tempName)
                        Exit For
                    End If
                End If
            Next i
            DB.TableDefs.Refresh()
            Dim tmpTimetableInf() As typeTimetableInf
            ReDim tmpTimetableInf(UBound(TimetableInf))
            For i = 1 To UBound(TimetableInf)
                tmpTimetableInf(i).sName = TimetableInf(i).sName
                tmpTimetableInf(i).sID = TimetableInf(i).sID
                tmpTimetableInf(i).sCreateDate = TimetableInf(i).sCreateDate
                tmpTimetableInf(i).sEditDate = TimetableInf(i).sEditDate
            Next
            ReDim TimetableInf(0)
            For i = 1 To UBound(tmpTimetableInf)
                If tmpTimetableInf(i).sID <> tmpTableName Then
                    ReDim Preserve TimetableInf(UBound(TimetableInf) + 1)
                    TimetableInf(UBound(TimetableInf)).sName = tmpTimetableInf(i).sName
                    TimetableInf(UBound(TimetableInf)).sID = tmpTimetableInf(i).sID
                    TimetableInf(UBound(TimetableInf)).sCreateDate = tmpTimetableInf(i).sCreateDate
                    TimetableInf(UBound(TimetableInf)).sEditDate = tmpTimetableInf(i).sEditDate
                End If
            Next

            Call SetListSkbName()
            Call SaveTimetableInf()
        Catch ex As Exception
            MsgBox(ex.Message & "�����������������ִ�иò���!", MsgBoxStyle.Exclamation, )
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub lstSKB_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstSKB.SelectedIndexChanged
        If Me.lstSKB.SelectedIndex >= 0 Then
            Dim nID As Integer
            nID = GetTimetableInfID(Me.lstSKB.Items(Me.lstSKB.SelectedIndex))
            If nID > 0 Then
                Me.labInfor.Text = "ID��   :" & TimetableInf(nID).sID & vbCrLf _
                                       & "����ʱ��:" & TimetableInf(nID).sCreateDate & vbCrLf _
                                       & "�޸�ʱ��:" & TimetableInf(nID).sEditDate
            End If
        Else
            Me.labInfor.Text = ""
        End If
    End Sub


    Private Sub btnDeleteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAll.Click
        Dim sChosenSkbNameKe As String
        Dim i As Integer
        Dim sName() As String
        ReDim sName(0)
        For i = 1 To Me.lstSKB.Items.Count
            If Me.lstSKB.Items(i - 1) <> TimeTablePara.sPubCurSkbName Then
                ReDim Preserve sName(UBound(sName) + 1)
                sName(UBound(sName)) = Me.lstSKB.Items(i - 1)
            End If
        Next i
        If MsgBox("�Ƿ�ȫ��ɾ����ǰ����ʱ�̱����ɾ������Щʱ�̱���Ϣ�����ٴ��ڣ��������ò������ܻ�ԭ��", 32 + 4, "ɾ��ʱ�̱�?") = vbYes Then
            If MsgBox("���Ҫɾ����Щʱ�̱���", 32 + 4, "ɾ��ʱ�̱���������!") = vbYes Then
                For i = 1 To UBound(sName)
                    sChosenSkbNameKe = sName(i)
                    If sChosenSkbNameKe = TimeTablePara.sPubCurSkbName Then
                        MsgBox("ʱ�̱�" & TimeTablePara.sPubCurSkbName & "����ʹ�ã�����ɾ��", 0 + 48)
                    Else
                        DelSkb(sChosenSkbNameKe)
                    End If
                Next
            Else
                Exit Sub
            End If
        Else
            Exit Sub
        End If

    End Sub
End Class