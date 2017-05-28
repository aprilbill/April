
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class frmCSTimeTableManager
    ''Dim sExistSKB() As String
    ''Dim dSKBFileName As DAO.Database
    Public strtempCurlineID As String = ""

    Private Sub frmTimeTableManager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sqlstr As String = ""
        If CurLineName = "" Then
            sqlstr = "SELECT * FROM PD_LINEINFO WHERE 1=1 order by lineid asc"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)

            Dim i As Integer
            For i = 0 To tempTable.Rows.Count - 1
                Me.ComlineInf.Items.Add(tempTable.Rows(i).Item("LINENAME").ToString.Trim)
            Next
            If Me.ComlineInf.Items.Count > 0 Then
                Me.ComlineInf.SelectedIndex = 0

            End If

            'Me.ComlineInf.DataSource = LocalDataSet.PD_LINEINFO
            'Me.ComlineInf.DisplayMember = "LINENAME"
            strtempCurlineID = Me.ComlineInf.Text.ToString.Trim

            Call InputCSTimetableInf(strtempCurlineID) '����
            If CSTimetableInf Is Nothing = False Then
                Me.lstCSTT.Items.Clear()
                For i = 1 To UBound(CSTimetableInf)
                    Me.lstCSTT.Items.Add(CSTimetableInf(i).sName)
                Next i
            Else
                MsgBox("��ǰ��û�г���ƻ��������ƶ�����ƻ���", MsgBoxStyle.OkOnly)
                Exit Sub
            End If
        Else
            Me.ComlineInf.Items.Add(CurLineName)
        End If
    End Sub

    Public Sub SetListSkbName()
        Dim i As Integer
        Me.lstCSTT.Items.Clear()
        For i = 1 To UBound(CSTimetableInf)
            Me.lstCSTT.Items.Add(CSTimetableInf(i).sName)
        Next i
    End Sub

    Private Sub btnRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRename.Click
        Dim sChosenSkbNameKe As String
        sChosenSkbNameKe = Me.lstCSTT.SelectedItem
        If Trim(sChosenSkbNameKe) = "" Then
            MsgBox("����ѡ����Ҫ���������г�ʱ�̱�", 48, "ʱ�̱����")
        Else
            ReNameSkbName(sChosenSkbNameKe)
        End If
    End Sub

    '������
    Sub ReNameSkbName(ByVal tmpChosenSkbName As String)
        'Dim stmpUsedSkbName As String
        Dim tmpTableName As String
        Dim i As Integer
        'stmpUsedSkbName = Me.txtCurCSSKB.Text.Trim
        If strQCurCSPlanName <> tmpChosenSkbName Then
            'sname:
            Dim nf As New frmInputBox
            nf.Text = "����������ƻ���"
            nf.labTitle.Text = "������ƻ���" & tmpChosenSkbName & "������Ϊ��"
            nf.txtText.Visible = False
            nf.cmbText.Visible = True
            nf.cmbText.Items.Clear()
            For i = 1 To UBound(CSTimetableInf)
                nf.cmbText.Items.Add(CSTimetableInf(i).sName)
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
            MsgBox("ʱ�̱�" & strQCurCSPlanName & "����ʹ�ã����ܸ������ơ�", 48, "ʱ�̱����")
        End If
    End Sub
    Public Function bFindSkb(ByVal sCSSKBName As String) As Boolean
        'Call SetExistSKBName()
        Dim i As Integer
        bFindSkb = False
        For i = 1 To UBound(CSTimetableInf)
            If CSTimetableInf(i).sName = sCSSKBName Then
                bFindSkb = True
                Exit For
            End If
        Next i
    End Function

    Sub RenameSkb(ByVal OriTableName As String, ByVal NewTablename As String)
        Dim i As Integer
        For i = 1 To UBound(CSTimetableInf)
            If CSTimetableInf(i).sName = OriTableName Then
                CSTimetableInf(i).sName = NewTablename
                Exit For
            End If
        Next
        Call SetListSkbName()
        Call SaveCSTimetableInf()
    End Sub
    '����ʱ�̱���Ϣ
    Public Sub SaveCSTimetableInf()

        Dim sqlstr As String = ""
        sqlstr = "DELETE FROM CS_CSTIMETABLEINF WHERE LINEID='" & CStr(strtempCurlineID) & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "SELECT * FROM CS_CSTIMETABLEINF "
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        For i As Integer = 1 To UBound(CSTimetableInf)
            tempTable.Rows.Add(CStr(strtempCurlineID), CStr(CSTimetableInf(i).sName), CStr(CSTimetableInf(i).sID), CStr(CSTimetableInf(i).sCreateDate), CStr(CSTimetableInf(i).sEditDate), "", CStr(CSTimetableInf(i).sDiagramID))
        Next
        Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        tempTable.Dispose()
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Dim sChosenSkbNameKe As String
        sChosenSkbNameKe = Me.lstCSTT.SelectedItem
        If sChosenSkbNameKe <> "" Then
            If MsgBox("�Ƿ���" & sChosenSkbNameKe & "��", 32 + 4, "���Ƴ���ƻ���") = vbYes Then
                CopySkbName(sChosenSkbNameKe)
            End If
        Else
            MsgBox("�޳���ƻ���ѡ�У���ѡ�����ƻ���", 48, "����ƻ������")
        End If
    End Sub

    Sub CopySkbName(ByVal stmpChosenSkbName As String)
        Dim tmpTableName As String
        Dim i As Integer
sname:
        Dim nf As New frmInputBox
        nf.Text = "���Ƴ���ƻ���"
        nf.labTitle.Text = "�����뽫����ƻ��� " & stmpChosenSkbName & "����Ϊ�³���ƻ������ƣ�"
        nf.txtText.Visible = False
        nf.cmbText.Visible = True
        nf.cmbText.Items.Clear()
        For i = 1 To UBound(CSTimetableInf)
            nf.cmbText.Items.Add(CSTimetableInf(i).sName)
        Next i
        nf.cmbText.Text = stmpChosenSkbName
        nf.ShowDialog()
        If bCancelInput = 1 Then Exit Sub
        tmpTableName = StrInputBoxCombText

        If bFindSkb(tmpTableName) = True Then
            MsgBox("�ó���ƻ��������Ѿ����ڣ���������������")
            Exit Sub
        Else
            If tmpTableName <> "" Then
                CopySkb(stmpChosenSkbName, tmpTableName)
            End If
        End If
    End Sub

    Sub CopySkb(ByVal OriTableName As String, ByVal NewTablename As String)
        Dim sOldID, sDiagramID As String
        sDiagramID = ""
        Dim i As Integer
        Dim str As String
        Dim tab As New DataTable
        Dim dateNow As Date = Now()
        Dim tmpNewTime As String = dateNow.Year & dateNow.Month & dateNow.Day & "_" & dateNow.Hour & dateNow.Minute & dateNow.Second
        Dim sNewID As String = tmpNewTime + "CS"
        For i = 1 To UBound(CSTimetableInf)
            If OriTableName = CSTimetableInf(i).sName Then
                sDiagramID = CSTimetableInf(i).sDiagramID
                Exit For
            End If
        Next
        ReDim Preserve CSTimetableInf(UBound(CSTimetableInf) + 1)
        CSTimetableInf(UBound(CSTimetableInf)).nID = UBound(CSTimetableInf)
        CSTimetableInf(UBound(CSTimetableInf)).sName = NewTablename
        CSTimetableInf(UBound(CSTimetableInf)).sID = sNewID
        CSTimetableInf(UBound(CSTimetableInf)).sCreateDate = dateNow
        CSTimetableInf(UBound(CSTimetableInf)).sEditDate = dateNow
        CSTimetableInf(UBound(CSTimetableInf)).sDiagramID = sDiagramID

        sOldID = GetCSTimetableIDFromName(OriTableName)


        '���Ƴ���ƻ���+++++ 
        str = "SELECT  * FROM CS_CREWSCHEDULE WHERE LINEID='" & CStr(strtempCurlineID) & "' AND CSTIMETABLEID='" & CStr(sOldID) & "'"
        tab = Globle.Method.ReadDataForAccess(str)
        Common.Global.StartProgress(4, "����ƻ������У����Ե�...")

        str = "SELECT * FROM CS_CREWSCHEDULE "
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(str)
        Common.Global.PerformStep()
        For i = 0 To tab.Rows.Count - 1
            tab.Rows(i).Item("CSTIMETABLEID") = CStr(sNewID)
            tempTable.Rows.Add(tab.Rows(i).Item("LINEID").ToString.Trim, _
                                                               tab.Rows(i).Item("CSTIMETABLEID").ToString.Trim, _
                                                               tab.Rows(i).Item("ID").ToString.Trim, _
                                                               tab.Rows(i).Item("DRIVERNO").ToString.Trim, _
                                                               tab.Rows(i).Item("DATENO").ToString.Trim, _
                                                               tab.Rows(i).Item("DUTYSORT").ToString.Trim, _
                                                               tab.Rows(i).Item("TRAINNO").ToString.Trim, _
                                                               tab.Rows(i).Item("STARTTIME").ToString.Trim, _
                                                               tab.Rows(i).Item("STARTSTANAME").ToString.Trim, _
                                                               tab.Rows(i).Item("ENDTIME").ToString.Trim, _
                                                               tab.Rows(i).Item("ENDSTANAME").ToString.Trim, _
                                                               tab.Rows(i).Item("TRAINID").ToString.Trim, _
                                                               tab.Rows(i).Item("PATH1").ToString.Trim, _
                                                               tab.Rows(i).Item("PATH2").ToString.Trim, _
                                                               tab.Rows(i).Item("CHEDIID").ToString.Trim, _
                                                               tab.Rows(i).Item("UPORDOWN").ToString.Trim, _
                                                               tab.Rows(i).Item("ZFTIME").ToString.Trim, _
                                                               tab.Rows(i).Item("STASEQ1").ToString.Trim, _
                                                               tab.Rows(i).Item("STASEQ2").ToString.Trim)
        Next
        Common.Global.PerformStep()
        Globle.Method.UpdateDataForAccess(str, tempTable)
        '******************************************************
        Common.Global.PerformStep()


        '�����Ͷ������ɱ�+++++ 
        str = "SELECT  * FROM CS_WORKLOAD WHERE LINEID='" & CStr(strtempCurlineID) & "' AND CSTIMETABLEID='" & CStr(sOldID) & "'"
        tab = New DataTable
        tab = Globle.Method.ReadDataForAccess(str)

        str = "SELECT * FROM CS_WORKLOAD "
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(str)
        For i = 0 To tab.Rows.Count - 1
            tab.Rows(i).Item("CSTIMETABLEID") = CStr(sNewID)
            tempTable.Rows.Add(tab.Rows(i).Item("LINEID").ToString.Trim, _
                                                               tab.Rows(i).Item("CSTIMETABLEID").ToString.Trim, _
                                                               tab.Rows(i).Item("DRIVERNO").ToString.Trim, _
                                                               tab.Rows(i).Item("DATENO").ToString.Trim, _
                                                               tab.Rows(i).Item("DUTYSORT").ToString.Trim, _
                                                               tab.Rows(i).Item("WORKTIME").ToString.Trim, _
                                                               tab.Rows(i).Item("TOTALZFTIME").ToString.Trim, _
                                                               tab.Rows(i).Item("DRIVETIME").ToString.Trim, _
                                                                tab.Rows(i).Item("DRIVEdistance").ToString.Trim)
        Next
        Globle.Method.UpdateDataForAccess(str, tempTable)
        '******************************************************
        Common.Global.PerformStep()
        Common.Global.EndProgress()
        Call SetListSkbName()
        Call SaveCSTimetableInf()
        MsgBox("����ƻ�������ϣ�")
    End Sub
    '����ʱ�̱����Ƶõ�ʱ�̱�ID
    Public Function GetCSTimetableIDFromName(ByVal sName As String) As String
        Dim i As Integer
        GetCSTimetableIDFromName = ""
        For i = 1 To UBound(CSTimetableInf)
            If CSTimetableInf(i).sName = sName Then
                GetCSTimetableIDFromName = CSTimetableInf(i).sID
                Exit For
            End If
        Next
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim sChosenSkbNameKe As String
        sChosenSkbNameKe = Me.lstCSTT.SelectedItem
        If Trim(sChosenSkbNameKe) = "" Then
            MsgBox("�޳���ƻ���ѡ�У���ѡ�����ƻ���", 48, "����ƻ������")
        Else
            If MsgBox("�Ƿ�ɾ����" & sChosenSkbNameKe & "��,���ɾ�����ó���ƻ�����Ϣ�����ٴ��ڣ�����", 32 + 4, "ɾ������ƻ���?") = vbYes Then
                If sChosenSkbNameKe = strQCurCSPlanID Or sChosenSkbNameKe = strQCurCSPlanName Then
                    MsgBox("����ƻ���" & sChosenSkbNameKe & "����ʹ�ã�����ɾ��", 0 + 48)
                Else
                    If MsgBox("���Ҫɾ����" & sChosenSkbNameKe & "����", 32 + 4, "ɾ������ƻ�����������!") = vbYes Then
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
        Dim tmpCSTableID As String = GetCSTimetableIDFromName(tmpTableName)
        Dim sqlstr As String = ""
        Try
            sqlstr = "DELETE from CS_CREWSCHEDULE WHERE LINEID='" & CStr(strtempCurlineID) & "' AND CSTIMETABLEID ='" & CStr(tmpCSTableID) & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "DELETE from CS_WORKLOAD WHERE LINEID='" & CStr(strtempCurlineID) & "' AND CSTIMETABLEID ='" & CStr(tmpCSTableID) & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "DELETE from CS_CSTIMETABLEINF WHERE LINEID='" & CStr(strtempCurlineID) & "' AND CSTIMETABLEID ='" & CStr(tmpCSTableID) & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "DELETE from CS_DINNERINF WHERE LINEID='" & CStr(strtempCurlineID) & "' AND CSTIMETABLEID ='" & CStr(tmpCSTableID) & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "DELETE from cs_amdrivercorrespond WHERE LINEID='" & CStr(strtempCurlineID) & "' AND (adrivertimetableid ='" & CStr(tmpCSTableID) & "' or mdrivertimetableid='" & CStr(tmpCSTableID) & "')"
            Globle.Method.UpdateDataForAccess(sqlstr)
            'sqlstr = "DELETE from cs_deadheading WHERE LINEID='" & CStr(strtempCurlineID) & "' AND CSTIMETABLEID ='" & CStr(tmpCSTableID) & "'"
            'Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "DELETE from cs_autoplanpara WHERE CSTIMETABLEID ='" & CStr(tmpCSTableID) & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            
            '�������õĲ�����Ϣ
            sqlstr = "delete from CS_RESULT_PREPAREDDUTYINF where cstimetableid='" & CStr(tmpCSTableID) & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "delete from CS_RESULT_PREPAREDTRAININF where cstimetableid='" & CStr(tmpCSTableID) & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)

            Call InputCSTimetableInf(strtempCurlineID)
            Call SetListSkbName()
        Catch ex As Exception
            MsgBox(ex.Message & "�����������������ִ�иò���!", MsgBoxStyle.Exclamation, )
        End Try

    End Sub


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub lstSKB_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCSTT.SelectedIndexChanged
        If Me.lstCSTT.SelectedIndex >= 0 Then
            Dim nID As Integer
            nID = GetCSTimetableInfID(Me.lstCSTT.Items(Me.lstCSTT.SelectedIndex))
            If nID > 0 Then
                Me.labInfor.Text = "ID��   :" & CSTimetableInf(nID).sID & vbCrLf _
                                       & "����ʱ��:" & CSTimetableInf(nID).sCreateDate & vbCrLf _
                                       & "�޸�ʱ��:" & CSTimetableInf(nID).sEditDate
            End If
        Else
            Me.labInfor.Text = ""
        End If
    End Sub


    Private Sub btnDeleteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAll.Click
        Dim sChosenSkbNameKe As String
        Dim i As Integer
        If MsgBox("�Ƿ�ȫ��ɾ����ǰ���г���ƻ������ɾ������Щ����ƻ�����Ϣ�����ٴ��ڣ��������ò������ܻ�ԭ��", 32 + 4, "ɾ������ƻ���?") = vbYes Then
            If MsgBox("���Ҫɾ����Щ����ƻ�����", 32 + 4, "ɾ������ƻ�����������!") = vbYes Then
                For i = 1 To Me.lstCSTT.Items.Count
                    If Me.lstCSTT.Items.Count <> 0 Then
                        sChosenSkbNameKe = Me.lstCSTT.Items(i - 1).ToString.Trim
                        If sChosenSkbNameKe = strQCurCSPlanID Or sChosenSkbNameKe = strQCurCSPlanName Then
                            MsgBox("����ƻ���" & sChosenSkbNameKe & "����ʹ�ã�����ɾ��", 0 + 48)
                        Else
                            DelSkb(sChosenSkbNameKe)
                            i -= 1
                        End If
                    End If
                Next
            Else
                Exit Sub
            End If
        Else
            Exit Sub
        End If
    End Sub

    Private Sub ComlineInf_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComlineInf.SelectedIndexChanged
        strtempCurlineID = Me.ComlineInf.Text.ToString.Trim
        Call InputCSTimetableInf(strtempCurlineID) '����
        If CSTimetableInf Is Nothing = False Then
            Dim i As Integer
            Me.lstCSTT.Items.Clear()
            For i = 1 To UBound(CSTimetableInf)
                Me.lstCSTT.Items.Add(CSTimetableInf(i).sName)
            Next i
        Else
            MsgBox("��ǰ��û�г���ƻ��������ƶ�����ƻ���", MsgBoxStyle.OkOnly)
            Exit Sub
        End If
    End Sub

    Public Sub New()

        ' �˵����������������ġ�
        InitializeComponent()

        ' �� InitializeComponent() ����֮������κγ�ʼ����

    End Sub
End Class