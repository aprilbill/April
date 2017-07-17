Imports System.Windows.Forms
Public Class frmODSInputTimetable

    Private timeTable As New Data.DataTable
    Public selectedID As String = ""
    Public selectedNetVer As String = ""
    Public lineid As String = ""
    Private Sub cmbTrainDiaName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTrainDiaName.SelectedIndexChanged

        If Me.cmbTrainDiaName.Text.ToString <> "" And CheckBox2.Checked = True Then
            For i As Integer = 0 To timeTable.Rows.Count - 1
                If timeTable.Rows(i).Item("timetablename") = Me.cmbTrainDiaName.SelectedItem.ToString And timeTable.Rows(i).Item("traindiastylename") = cmbTrainDiamStyle.SelectedItem.ToString Then
                    txtMakerDep.Text = timeTable.Rows(i).Item("schedulingdepart")
                    dtpFirstTime.Text = CDate(timeTable.Rows(i).Item("beginexecutetime").ToString).ToShortDateString
                    dtpEndTime.Text = CDate(timeTable.Rows(i).Item("endexecutetime")).ToShortDateString
                    selectedID = timeTable.Rows(i).Item("traindiagramid").ToString()
                    selectedNetVer = timeTable.Rows(i).Item("net_ver").ToString()
                End If
            Next
        End If
       
    End Sub
    Public Function IfSameDiagramID(ByVal sID As String) As Boolean
        IfSameDiagramID = False
        Dim sqlstr As String = "select * from TMS_TRAINDIAGRAMINFO"
        Dim tmpDT As New DataTable
        tmpDT = Globle.Method.ReadDataForAccess(sqlstr)
        For i As Integer = 0 To tmpDT.Rows.Count - 1
            If tmpDT.Rows(i).Item("traindiagramid") = sID Then
                Return True
                Exit For
            End If
        Next
    End Function
    Private Sub btnBegin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBegin.Click
        If Me.cmbTrainDiaName.Text.ToString <> "" And selectedID <> "" Then
            If IfSameDiagramID(selectedID) = True Then
                MsgBox("�洢������ͼ�Ѿ����ڣ�")
                Exit Sub
            End If
            If MessageBox.Show("���Ƿ�Ҫ����[" & Me.cmbTrainDiaName.Text.ToString & " ] ����ͼ��", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
                Dim sTrainDiagramID As String = selectedID
                Dim sInputTime As String = Date.Now.ToString '��������ͼ��ʱ��
                Dim TMS_TRAINDIAGRAMINFO As New DataTable
                Dim sqlstr As String = "select * from TMS_TRAINDIAGRAMINFO"
                TMS_TRAINDIAGRAMINFO = Globle.Method.ReadDataForAccess(sqlstr)
                TMS_TRAINDIAGRAMINFO.Rows.Add( _
                        sTrainDiagramID, Me.cmbLineInfo.Text.ToString.Trim, Me.cmbTrainDiaName.Text.ToString.Trim, Me.cmbTrainDiamStyle.Text.ToString, sInputTime, Me.dtpFirstTime.Text, Me.dtpEndTime.Text, Me.txtMakerDep.Text)
                Globle.Method.UpdateDataForAccess(sqlstr, TMS_TRAINDIAGRAMINFO)

                If (Me.cmbTrainDiaName.SelectedItem.ToString.Trim <> "" AndAlso CheckBox2.Checked = True) Then
                    Call InputACCESSTimeTable(sTrainDiagramID, Me.cmbTrainDiaName.Text.ToString.Trim)
                End If
                If (Me.cmbTrainDiaName.SelectedItem.ToString.Trim <> "" AndAlso CheckBox1.Checked = True) Then
                    Call InputTimeTable(sTrainDiagramID, Me.cmbTrainDiaName.Text.ToString.Trim)
                End If
                'Me.Dispose()
            Else

                Exit Sub
            End If
        Else
            MessageBox.Show("��ѡ�����������ͼ�����Ϣ��������������ѡ��", "���������", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub cmbTrainDiamStyle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTrainDiamStyle.SelectedIndexChanged
        If CheckBox2.Checked = True Then
            If timeTable IsNot Nothing OrElse timeTable.Rows.Count > 0 Then
                cmbTrainDiaName.Items.Clear()
                For i As Integer = 0 To timeTable.Rows.Count - 1
                    If timeTable.Rows(i).Item("traindiastylename") = cmbTrainDiamStyle.SelectedItem.ToString() Then
                        cmbTrainDiaName.Items.Add(timeTable.Rows(i).Item("timetablename"))
                    End If
                Next
            End If
        End If
       
    End Sub
    Public Function GetTimetableIDInAccessDatabase(ByVal sTimeTableName As String) As String
        GetTimetableIDInAccessDatabase = ""
        Dim DE As dao.DBEngine = New dao.DBEngine
        Dim myWS As dao.Workspace
        myWS = DE.Workspaces(0)
        Dim dFile As dao.Database
        Dim sFile As dao.Recordset
        Dim strTable3 As String = "select ʱ�̱�ID from ʱ�̱���Ϣ�� where ʱ�̱�����='" & sTimeTableName & "'"
        Dim g_strNetMainPathOpenInfor As String
        g_strNetMainPathOpenInfor = ";UID=admin;PWD=" & InputDatabasePassWord & ";DATABASE=" & InputDatabasePath & InputDatabaseName & ""
        dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        sFile = dFile.OpenRecordset(strTable3)
        If sFile.RecordCount > 0 Then
            GetTimetableIDInAccessDatabase = sFile.Fields("ʱ�̱�ID").Value.ToString
        End If
    End Function
    Public Sub InputTimeTable(ByVal sTrainDiagramID As String, ByVal sTimetableName As String)
        Dim sTableID As String = GetTimetableIDInAccessDatabase(sTimetableName)
        Dim sCurTable As String = ""
        Dim sqlstr As String = ""
        Dim tablename() As String = {"��ͼ�ṹ", "������Ϣ", "�����۷�ʱ���׼", "��վ���ʱ��", "׷�ټ��ʱ��", "�г����б����Ϣ", sTableID & "�г�ʱ����Ϣ", sTableID & "����ʹ�÷���", "�г�ͣվ�����Ϣ", "����ͼϵͳ������", "ʱ�̱�վ˳��", "�����������", "�г���Ϣ", "��·������Ϣ", "��վ��Ϣ", "·����·��Ϣ", "�г�ͣվ���", "�г����б��"}
        Progress.ProgressForm.StartProgress(18, "���ڵ�������ͼ������ݣ����Ժ�...")
        Try
            For Each table As String In tablename
                Progress.ProgressForm.PerformStep()
                Dim DE As dao.DBEngine = New dao.DBEngine
                Dim myWS As dao.Workspace
                myWS = DE.Workspaces(0)
                Dim dFile As dao.Database
                Dim sFile As dao.Recordset
                Dim strTable3 As String = "select * from " & table
                Dim g_strNetMainPathOpenInfor As String
                g_strNetMainPathOpenInfor = ";UID=admin;PWD=" & InputDatabasePassWord & ";DATABASE=" & InputDatabasePath & InputDatabaseName & ""
                dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
                sFile = dFile.OpenRecordset(strTable3)
                Dim p As Integer = 0
                Dim nNum As Integer
                nNum = 0
                If sFile.RecordCount > 0 Then
                    sFile.MoveLast()
                    nNum = sFile.RecordCount
                End If
                sCurTable = table
                If sFile.RecordCount > 0 Then
                    sFile.MoveFirst()
                    Select Case table
                        Case sTableID & "�г�ʱ����Ϣ"
                            Dim TMS_TIMETABLEINFO As New DataTable
                            sqlstr = "select * from TMS_TIMETABLEINFO"
                            TMS_TIMETABLEINFO = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_TIMETABLEINFO.Rows.Add(sTrainDiagramID, sFile.Fields("ID").Value, sFile.Fields("����").Value, sFile.Fields("��վ����").Value, sFile.Fields("����").Value, sFile.Fields("����").Value, sFile.Fields("ͣվ�ɵ�").Value)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_TIMETABLEINFO)
                        Case sTableID & "����ʹ�÷���"
                            Dim TMS_STOCKUSINGINFO As New DataTable
                            sqlstr = "select * from TMS_STOCKUSINGINFO"
                            TMS_STOCKUSINGINFO = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_STOCKUSINGINFO.Rows.Add(sTrainDiagramID, sFile.Fields("����˳��").Value, sFile.Fields("����ID").Value, sFile.Fields("���ҳ���").Value, sFile.Fields("����˳��").Value, _
                        sFile.Fields("�г�����").Value, sFile.Fields("��·����").Value, sFile.Fields("�������").Value, sFile.Fields("�������").Value, _
                        sFile.Fields("ͣվ���").Value, sFile.Fields("�Ƿ��۷�Լ��").Value, sFile.Fields("����������").Value, sFile.Fields("��������ɫ").Value, sFile.Fields("�������߿�").Value, _
                        sFile.Fields("����������").Value, sFile.Fields("��������ɫ").Value, sFile.Fields("�������߿�").Value)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_STOCKUSINGINFO)
                        Case "��ͼ�ṹ"
                            Dim TMS_DIASTRUCTINFO As New DataTable
                            sqlstr = "select * from TMS_DIASTRUCTINFO"
                            TMS_DIASTRUCTINFO = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_DIASTRUCTINFO.Rows.Add( _
                                  sFile.Fields("��ͼ����").Value, sFile.Fields("վ��").Value, sFile.Fields("վ��").Value, sFile.Fields("�Ƿ���ʾ").Value, _
                                 sFile.Fields("��վID").Value, sFile.Fields("��վID").Value, sFile.Fields("��·��").Value, sFile.Fields("Y����").Value, sFile.Fields("�Ƿ�Ĭ��").Value, sTrainDiagramID)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_DIASTRUCTINFO)
                        Case "������Ϣ"
                            Dim TMS_TRAINUSINGINFO As New DataTable
                            sqlstr = "select * from TMS_TRAINUSINGINFO"
                            TMS_TRAINUSINGINFO = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_TRAINUSINGINFO.Rows.Add( _
                                  sFile.Fields("����ID").Value, sFile.Fields("��������").Value, sTrainDiagramID, "��", "��")
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_TRAINUSINGINFO)
                        Case "�����۷�ʱ���׼"
                            Dim TMS_TRAINRETURNTIME As New DataTable
                            sqlstr = "select * from TMS_TRAINRETURNTIME"
                            TMS_TRAINRETURNTIME = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_TRAINRETURNTIME.Rows.Add( _
                                  sTrainDiagramID, sFile.Fields("��վ����").Value, sFile.Fields("��������").Value, sFile.Fields("վǰ�۷�ʱ��").Value, sFile.Fields("վ���۷�ʱ��").Value, sFile.Fields("�����۷�ʱ��").Value, sFile.Fields("����ɵ����۷���ʱ��").Value, sFile.Fields("�۷����������ɵ�ʱ��").Value, sFile.Fields("���﷢�����").Value, sFile.Fields("�����������").Value)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_TRAINRETURNTIME)
                        Case "��վ���ʱ��"
                            Dim TMS_STATIONTIME As New DataTable
                            sqlstr = "select * from TMS_STATIONTIME"
                            TMS_STATIONTIME = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_STATIONTIME.Rows.Add( _
                                  sTrainDiagramID, sFile.Fields("��վ����").Value, sFile.Fields("�Ӳ���").Value, sFile.Fields("�ӻ���").Value, sFile.Fields("����1��").Value, sFile.Fields("����2��").Value, sFile.Fields("��ͨ1��").Value, sFile.Fields("��ͨ2��").Value, sFile.Fields("�ӵ�����").Value, sFile.Fields("�ӷ�����").Value, _
                                  sFile.Fields("�ӷ�����").Value, sFile.Fields("�ӵ�����").Value, sFile.Fields("�Ӳ���").Value, sFile.Fields("�ӻ���").Value, sFile.Fields("����1��").Value, sFile.Fields("����2��").Value, sFile.Fields("��ͨ1��").Value, sFile.Fields("��ͨ2��").Value, sFile.Fields("�ӵ�����").Value, sFile.Fields("�ӷ�����").Value, _
                                  sFile.Fields("�ӷ�����").Value, sFile.Fields("�ӵ�����").Value)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_STATIONTIME)
                        Case "׷�ټ��ʱ��"
                            Dim TMS_SECTIONTIME As New DataTable
                            sqlstr = "select * from TMS_SECTIONTIME"
                            TMS_SECTIONTIME = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_SECTIONTIME.Rows.Add( _
                                  sTrainDiagramID, sFile.Fields("��վ����").Value, sFile.Fields("����").Value, sFile.Fields("ͬ�򷢷�").Value, sFile.Fields("ͬ�򷢵�").Value, sFile.Fields("ͬ��ͨ").Value, sFile.Fields("ͬ�򵽷�").Value, sFile.Fields("ͬ�򵽵�").Value, sFile.Fields("ͬ��ͨ").Value, sFile.Fields("ͬ��ͨ��").Value, _
                                  sFile.Fields("ͬ��ͨ��").Value, sFile.Fields("ͬ��ͨͨ").Value, sFile.Fields("���򷢷�").Value, sFile.Fields("���򷢵�").Value, sFile.Fields("����ͨ").Value, sFile.Fields("���򵽷�").Value, sFile.Fields("���򵽵�").Value, sFile.Fields("����ͨ").Value, sFile.Fields("����ͨ��").Value, sFile.Fields("����ͨ��").Value, _
                                  sFile.Fields("����ͨͨ").Value)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_SECTIONTIME)
                        Case "�г����б����Ϣ"
                            Dim TMS_RUNSCALEINFO As New DataTable
                            sqlstr = "select * from TMS_RUNSCALEINFO"
                            TMS_RUNSCALEINFO = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_RUNSCALEINFO.Rows.Add( _
                                  sTrainDiagramID, sFile.Fields("��·����").Value, sFile.Fields("��������").Value, sFile.Fields("���").Value, sFile.Fields("��������").Value, sFile.Fields("����ʱ��").Value, sFile.Fields("�������").Value)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_RUNSCALEINFO)
                        Case "�г�ͣվ�����Ϣ"
                            Dim TMS_STOPSCALEINFO As New DataTable
                            sqlstr = "select * from TMS_STOPSCALEINFO"
                            TMS_STOPSCALEINFO = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_STOPSCALEINFO.Rows.Add( _
                                  sTrainDiagramID, sFile.Fields("��·����").Value, sFile.Fields("ͣվ����").Value, sFile.Fields("���").Value, sFile.Fields("��վ����").Value, sFile.Fields("ͣվʱ��").Value, sFile.Fields("�������").Value)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_STOPSCALEINFO)
                        Case "����ͼϵͳ������"
                            Dim TMS_TIMETABLEPARAMETER As New DataTable
                            sqlstr = "select * from TMS_TIMETABLEPARAMETER"
                            TMS_TIMETABLEPARAMETER = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_TIMETABLEPARAMETER.Rows.Add( _
                                   sFile.Fields("���").Value, sFile.Fields("������").Value, sFile.Fields("��ֵ").Value, sTrainDiagramID)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_TIMETABLEPARAMETER)
                        Case "ʱ�̱�վ˳��"
                            Dim TMS_TIMETABLESTASEQ As New DataTable
                            sqlstr = "select * from TMS_TIMETABLESTASEQ"
                            TMS_TIMETABLESTASEQ = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_TIMETABLESTASEQ.Rows.Add( _
                                  sFile.Fields("��������").Value, sFile.Fields("���").Value, sFile.Fields("��վ����").Value, sTrainDiagramID)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_TIMETABLESTASEQ)
                        Case "�����������"
                            Dim TMS_TRAININTERVALINFO As New DataTable
                            sqlstr = "select * from TMS_TRAININTERVALINFO"
                            TMS_TRAININTERVALINFO = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_TRAININTERVALINFO.Rows.Add( _
                                  sFile.Fields("��·����").Value, sFile.Fields("�̻�����").Value, sFile.Fields("ʱ���").Value, sFile.Fields("��ʼʱ��").Value, sFile.Fields("��ֹʱ��").Value, sFile.Fields("�������").Value, sFile.Fields("����ʱ��").Value, sFile.Fields("���б��").Value, sFile.Fields("ͣվ���").Value, sFile.Fields("ʼ���۷�").Value, sFile.Fields("�յ��۷�").Value, sFile.Fields("��������").Value, sFile.Fields("���һ").Value, sFile.Fields("����һ").Value, sFile.Fields("�����").Value, sFile.Fields("������").Value, sTrainDiagramID)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_TRAININTERVALINFO)
                        Case "�г���Ϣ"
                            Dim TMS_TRAININFO As New DataTable
                            sqlstr = "select * from TMS_TRAININFO"
                            TMS_TRAININFO = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_TRAININFO.Rows.Add( _
                                   sTrainDiagramID, sFile.Fields("��·��").Value, sFile.Fields("������").Value, sFile.Fields("ʼ��վ").Value, sFile.Fields("�յ�վ").Value, sFile.Fields("��·���").Value, sFile.Fields("Ŀ�ķ�").Value, sFile.Fields("�г�����").Value, sFile.Fields("�յ����÷�ʽ").Value, sFile.Fields("ʼ�����÷�ʽ").Value, sFile.Fields("��������").Value, sFile.Fields("�г���·").Value, sFile.Fields("����").Value)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_TRAININFO)
                        Case "��·������Ϣ"
                            Dim TMS_SECTIONINFO As New DataTable
                            sqlstr = "select * from TMS_SECTIONINFO"
                            TMS_SECTIONINFO = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_SECTIONINFO.Rows.Add( _
                                   sTrainDiagramID, sFile.Fields("��·����").Value, sFile.Fields("������").Value, sFile.Fields("��������").Value, sFile.Fields("������ʼվ").Value, sFile.Fields("�����յ�վ").Value, sFile.Fields("�����������").Value, sFile.Fields("�����������").Value, sFile.Fields("��������").Value, sFile.Fields("������Ŀ").Value)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_SECTIONINFO)
                        Case "��վ��Ϣ"
                            Dim TMS_STATIONINFO As New DataTable
                            sqlstr = "select * from TMS_STATIONINFO"
                            TMS_STATIONINFO = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_STATIONINFO.Rows.Add( _
                                   sTrainDiagramID, sFile.Fields("��·����").Value, sFile.Fields("վ��").Value, sFile.Fields("����վ��").Value, sFile.Fields("���վ��").Value, sFile.Fields("Ӣ��վ��").Value, sFile.Fields("Ӣ�ļ��").Value, sFile.Fields("��˫��").Value, sFile.Fields("����").Value, sFile.Fields("����").Value, sFile.Fields("վ��ͼ").Value, sFile.Fields("X����").Value, sFile.Fields("Y����").Value, sFile.Fields("��ע").Value, sFile.Fields("��վ����").Value)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_STATIONINFO)
                        Case "·����·��Ϣ"
                            Dim TMS_LINEINFO As New DataTable
                            sqlstr = "select * from TMS_LINEINFO"
                            TMS_LINEINFO = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_LINEINFO.Rows.Add( _
                                   sTrainDiagramID, sFile.Fields("���").Value, sFile.Fields("��·����").Value, sFile.Fields("��·���").Value, sFile.Fields("Ӣ������").Value, sFile.Fields("��·���").Value, sFile.Fields("��·�ܳ�").Value, sFile.Fields("��ע").Value)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_LINEINFO)
                        Case "�߶���Ϣ��"
                            Dim TMS_LINEDRAW As New DataTable
                            sqlstr = "select * from TMS_LINEDRAW"
                            TMS_LINEDRAW = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_LINEDRAW.Rows.Add(sFile.Fields("��վ����").Value, sFile.Fields("�߶α��").Value, sFile.Fields("�����·���").Value, sFile.Fields("�߶�����").Value, sFile.Fields("�ɵ�����").Value, sFile.Fields("�ɵ���;").Value, _
                                sFile.Fields("�ɵ�ʹ��˳��").Value, sFile.Fields("�߶γ���").Value, sFile.Fields("�ɵ��������").Value, sFile.Fields("����ģ��").Value, Int(sFile.Fields("X1����").Value), Int(sFile.Fields("X2����").Value), Int(sFile.Fields("Y1����").Value), _
                                Int(sFile.Fields("Y2����").Value), sFile.Fields("��1����").Value, sFile.Fields("��2����").Value, sFile.Fields("��3����").Value, sFile.Fields("��1����").Value, sFile.Fields("��2����").Value, sFile.Fields("��3����").Value, sFile.Fields("��ע").Value, sTrainDiagramID)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_LINEDRAW)
                        Case "�г�ͣվ���"
                            Dim TMS_STOPSCALE As New DataTable
                            sqlstr = "select * from TMS_STOPSCALE"
                            TMS_STOPSCALE = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_STOPSCALE.Rows.Add(sFile.Fields("�������").Value, sTrainDiagramID, sFile.Fields("��վ���").Value, sFile.Fields("��վ����").Value, sFile.Fields("��߱��").Value, sFile.Fields("����ͣվʱ��").Value, sFile.Fields("����ͣվʱ��").Value, _
                                sFile.Fields("����ʼ��ͣվʱ��").Value, sFile.Fields("�����յ�ͣվʱ��").Value, sFile.Fields("�����յ�ͣվʱ��").Value, sFile.Fields("����ʼ��ͣվʱ��").Value)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_STOPSCALE)
                        Case "�г����б��"
                            Dim TMS_RUNSCALE As New DataTable
                            sqlstr = "select * from TMS_RUNSCALE"
                            TMS_RUNSCALE = Globle.Method.ReadDataForAccess(sqlstr)
                            For p = 1 To nNum
                                TMS_RUNSCALE.Rows.Add(sTrainDiagramID, sFile.Fields("�������").Value, sFile.Fields("��·����").Value, sFile.Fields("��������").Value, sFile.Fields("��߱��").Value, sFile.Fields("�������").Value, sFile.Fields("��������ʱ��").Value, _
                                sFile.Fields("��������ʱ��").Value, sFile.Fields("������ʱ��").Value, sFile.Fields("����ͣ��ʱ��").Value, sFile.Fields("������ʱ��").Value, sFile.Fields("����ͣ��ʱ��").Value)
                                sFile.MoveNext()
                            Next
                            Globle.Method.UpdateDataForAccess(sqlstr, TMS_RUNSCALE)
                    End Select
                End If
            Next
            Progress.ProgressForm.EndProgress()

        Catch ex As Exception
            Progress.ProgressForm.EndProgress()
            MsgBox(sCurTable & " ������̷�����������������.tpm�ļ��ڵ������Ƿ����Ҫ��" & vbCrLf & ex.ToString)
            Exit Sub
        End Try
    End Sub
    Public Sub InputACCESSTimeTable(ByVal sTrainDiagramID As String, ByVal sTimetableName As String)
        Dim sCurTable As String = ""
        Dim str As String = ""
        Dim i As Integer = 0
        Progress.ProgressForm.StartProgress(16, "���ڵ�������ͼ������ݣ����Ժ�...")
        Try
            '�г�ʱ����Ϣ
            Dim trainTime As New DataTable
            str = "select seq,trainnum,stationname,arritime,departtime,stoptrack from tp.tms_traintimeinfo t where t.traindiagramid='" & sTrainDiagramID & "' and net_ver='" & selectedNetVer & "'"
            trainTime = Globle.Method.ReadDataForOracle(str)
            Dim TMS_TIMETABLEINFO As New DataTable
            str = "select * from TMS_TIMETABLEINFO"
            TMS_TIMETABLEINFO = Globle.Method.ReadDataForAccess(str)
            For i = 0 To trainTime.Rows.Count - 1
                TMS_TIMETABLEINFO.Rows.Add(sTrainDiagramID, trainTime.Rows(i).Item("seq").ToString(), trainTime.Rows(i).Item("trainnum").ToString(), trainTime.Rows(i).Item("stationname").ToString(), trainTime.Rows(i).Item("arritime").ToString(), trainTime.Rows(i).Item("departtime").ToString(), trainTime.Rows(i).Item("stoptrack").ToString())
            Next
            Globle.Method.UpdateDataForAccess(str, TMS_TIMETABLEINFO)
            Progress.ProgressForm.PerformStep()

            '����ʹ�÷���
            Dim trainUse As DataTable = New DataTable()
            str = "select * from tp.tms_stockusinginfo t where t.traindiagramid='" & sTrainDiagramID & "' and net_ver='" & selectedNetVer & "'"
            trainUse = Globle.Method.ReadDataForOracle(str)
            Dim TMS_STOCKUSINGINFO As New DataTable
            str = "select * from TMS_STOCKUSINGINFO"
            TMS_STOCKUSINGINFO = Globle.Method.ReadDataForAccess(str)
            'DriverInfoLinker1.Close()
            For i = 0 To trainUse.Rows.Count - 1
                TMS_STOCKUSINGINFO.Rows.Add(sTrainDiagramID, trainUse.Rows(i).Item("stockseq").ToString(), trainUse.Rows(i).Item("stockid").ToString(), trainUse.Rows(i).Item("linktrainnum").ToString(), trainUse.Rows(i).Item("lineseq").ToString(), _
                          trainUse.Rows(i).Item("trainstyle").ToString(), trainUse.Rows(i).Item("routingstyle").ToString(), trainUse.Rows(i).Item("printnum").ToString(), trainUse.Rows(i).Item("runscalestyle").ToString(), _
                           trainUse.Rows(i).Item("stopscalestyle").ToString(), trainUse.Rows(i).Item("ifturnfixed").ToString(), trainUse.Rows(i).Item("lineshowstyle").ToString(), trainUse.Rows(i).Item("lineshowcolor"), trainUse.Rows(i).Item("lineshowwidth").ToString(), _
                           trainUse.Rows(i).Item("stockshowstyle").ToString(), trainUse.Rows(i).Item("stockshowcolor").ToString(), trainUse.Rows(i).Item("stockshowwidth").ToString())
            Next
            Globle.Method.UpdateDataForAccess(str, TMS_STOCKUSINGINFO)
            Progress.ProgressForm.PerformStep()

            '��ͼ�ṹ
            Dim timeTableStruct As DataTable = New DataTable()
            str = "select * from tp.tms_diastructinfo t where t.net_line_name='" & lineid & "' and net_ver='" & selectedNetVer & "'"
            timeTableStruct = Globle.Method.ReadDataForOracle(str)
            Dim TMS_DIASTRUCTINFO As New DataTable
            str = "select * from TMS_DIASTRUCTINFO"
            TMS_DIASTRUCTINFO = Globle.Method.ReadDataForAccess(str)
            For i = 0 To timeTableStruct.Rows.Count - 1
                TMS_DIASTRUCTINFO.Rows.Add( _
                                      timeTableStruct.Rows(i).Item("diastructname").ToString(), timeTableStruct.Rows(i).Item("stationseq").ToString(), timeTableStruct.Rows(i).Item("stationname").ToString(), timeTableStruct.Rows(i).Item("ifshow").ToString(), _
                                      timeTableStruct.Rows(i).Item("nextstaid").ToString(), timeTableStruct.Rows(i).Item("upstaid").ToString(), timeTableStruct.Rows(i).Item("sub_line_name").ToString(), timeTableStruct.Rows(i).Item("ycoord").ToString(), timeTableStruct.Rows(i).Item("iffixed").ToString(), sTrainDiagramID)
            Next
            Globle.Method.UpdateDataForAccess(str, TMS_DIASTRUCTINFO)
            Progress.ProgressForm.PerformStep()

            '������Ϣ
            Dim trainInfo As DataTable = New DataTable()
            str = "select * from tp.TMS_TRAINUSINGINFO t where t.net_line_name='" & lineid & "' and net_ver='" & selectedNetVer & "'"
            trainInfo = Globle.Method.ReadDataForOracle(str)
            Dim TMS_TRAINUSINGINFO As New DataTable
            str = "select * from TMS_TRAINUSINGINFO"
            TMS_TRAINUSINGINFO = Globle.Method.ReadDataForAccess(str)
            For i = 0 To trainInfo.Rows.Count - 1
                TMS_TRAINUSINGINFO.Rows.Add( _
                                       trainInfo.Rows(i).Item("stockstyleid").ToString(), trainInfo.Rows(i).Item("stockstylename").ToString(), sTrainDiagramID, trainInfo.Rows(i).Item("stockshort").ToString(), trainInfo.Rows(i).Item("stockinfo").ToString())
            Next
            Globle.Method.UpdateDataForAccess(str, TMS_TRAINUSINGINFO)
            Progress.ProgressForm.PerformStep()

            '�����۷�ʱ���׼
            Dim trainReturnTime As DataTable = New DataTable()
            str = "select * from tp.TMS_TRAINRETURNTIME t where t.net_line_name='" & lineid & "' and net_ver='" & selectedNetVer & "'"
            trainReturnTime = Globle.Method.ReadDataForOracle(str)
            Dim TMS_TRAINRETURNTIME As New DataTable
            str = "select * from TMS_TRAINRETURNTIME"
            TMS_TRAINRETURNTIME = Globle.Method.ReadDataForAccess(str)
            For i = 0 To trainReturnTime.Rows.Count - 1
                TMS_TRAINRETURNTIME.Rows.Add( _
                                     sTrainDiagramID, trainReturnTime.Rows(i).Item("stationname").ToString(), trainReturnTime.Rows(i).Item("stockstylename").ToString(), trainReturnTime.Rows(i).Item("beforeturntime").ToString(), trainReturnTime.Rows(i).Item("afterturntime").ToString(), trainReturnTime.Rows(i).Item("immiturntime").ToString(), trainReturnTime.Rows(i).Item("arritoturntime").ToString(), trainReturnTime.Rows(i).Item("turntodeparttime").ToString(), trainReturnTime.Rows(i).Item("arristartarritime").ToString(), trainReturnTime.Rows(i).Item("startstartarritime").ToString())
            Next
            Globle.Method.UpdateDataForAccess(str, TMS_TRAINRETURNTIME)
            Progress.ProgressForm.PerformStep()

            '��վ���ʱ��
            Dim stationInterval As DataTable = New DataTable()
            str = "select * from tp.TMS_STATIONTIME t where t.net_line_name='" & lineid & "' and net_ver='" & selectedNetVer & "'"
            stationInterval = Globle.Method.ReadDataForOracle(str)
            Dim TMS_STATIONTIME As New DataTable
            str = "select * from TMS_STATIONTIME"
            TMS_STATIONTIME = Globle.Method.ReadDataForAccess(str)
            For i = 0 To stationInterval.Rows.Count - 1
                TMS_STATIONTIME.Rows.Add( _
                                     sTrainDiagramID, stationInterval.Rows(i).Item("stationname").ToString(), stationInterval.Rows(i).Item("taobuup").ToString(), stationInterval.Rows(i).Item("taohuiup").ToString(), stationInterval.Rows(i).Item("taolian1up").ToString(), stationInterval.Rows(i).Item("taolian2up").ToString(), stationInterval.Rows(i).Item("taotong1up").ToString(), stationInterval.Rows(i).Item("taotong2up").ToString(), stationInterval.Rows(i).Item("taodaofaup").ToString(), stationInterval.Rows(i).Item("taofadaoup").ToString(), _
                                     stationInterval.Rows(i).Item("taofafaup").ToString(), stationInterval.Rows(i).Item("taodaodaoup").ToString(), stationInterval.Rows(i).Item("taobudown").ToString(), stationInterval.Rows(i).Item("taohuidown").ToString(), stationInterval.Rows(i).Item("taolian1down").ToString(), stationInterval.Rows(i).Item("taolian2down").ToString(), stationInterval.Rows(i).Item("taotong1down").ToString(), stationInterval.Rows(i).Item("taotong2down").ToString(), stationInterval.Rows(i).Item("taodaofadown").ToString(), stationInterval.Rows(i).Item("taofadaodown").ToString(), _
                                    stationInterval.Rows(i).Item("taofafadown").ToString(), stationInterval.Rows(i).Item("taodaodaodown").ToString())
            Next
            Globle.Method.UpdateDataForAccess(str, TMS_STATIONTIME)
            Progress.ProgressForm.PerformStep()

            '׷�ټ��ʱ��
            Dim runInterval As DataTable = New DataTable()
            str = "select * from tp.TMS_SECTIONTIME t where t.net_line_name='" & lineid & "' and net_ver='" & selectedNetVer & "'"
            runInterval = Globle.Method.ReadDataForOracle(str)
            Dim TMS_SECTIONTIME As New DataTable
            str = "select * from TMS_SECTIONTIME"
            TMS_SECTIONTIME = Globle.Method.ReadDataForAccess(str)
            For i = 0 To runInterval.Rows.Count - 1
                TMS_SECTIONTIME.Rows.Add( _
                                       sTrainDiagramID, runInterval.Rows(i).Item("stationname").ToString(), runInterval.Rows(i).Item("intervalstyle").ToString(), runInterval.Rows(i).Item("samefafa").ToString(), runInterval.Rows(i).Item("samefadao").ToString(), runInterval.Rows(i).Item("samefatong").ToString(), runInterval.Rows(i).Item("samedaofa").ToString(), runInterval.Rows(i).Item("samedaodao").ToString(), runInterval.Rows(i).Item("samedaotong").ToString(), runInterval.Rows(i).Item("sametongfa").ToString(), _
                                       runInterval.Rows(i).Item("sametongdao").ToString(), runInterval.Rows(i).Item("sametongtong").ToString(), runInterval.Rows(i).Item("oppositefafa").ToString(), runInterval.Rows(i).Item("oppositefadao").ToString(), runInterval.Rows(i).Item("oppositefatong").ToString(), runInterval.Rows(i).Item("oppositedaofa").ToString(), runInterval.Rows(i).Item("oppositedaodao").ToString(), runInterval.Rows(i).Item("oppositedaotong").ToString(), runInterval.Rows(i).Item("oppositetongfa").ToString(), runInterval.Rows(i).Item("oppositetongdao").ToString(), _
                                       runInterval.Rows(i).Item("oppositetongtong").ToString())
            Next
            Globle.Method.UpdateDataForAccess(str, TMS_SECTIONTIME)
            Progress.ProgressForm.PerformStep()

            '�г����б����Ϣ
            Dim runScaleInfo As DataTable = New DataTable()
            str = "select * from tp.TMS_RUNSCALEINFO t where t.net_line_name='" & lineid & "' and net_ver='" & selectedNetVer & "'"
            runScaleInfo = Globle.Method.ReadDataForOracle(str)
            Dim TMS_RUNSCALEINFO As New DataTable
            str = "select * from TMS_RUNSCALEINFO"
            TMS_RUNSCALEINFO = Globle.Method.ReadDataForAccess(str)
            For i = 0 To runScaleInfo.Rows.Count - 1
                TMS_RUNSCALEINFO.Rows.Add( _
    sTrainDiagramID, runScaleInfo.Rows(i).Item("routingname").ToString(), runScaleInfo.Rows(i).Item("runscalename").ToString(), runScaleInfo.Rows(i).Item("sectionseq").ToString(), runScaleInfo.Rows(i).Item("sectionname").ToString(), runScaleInfo.Rows(i).Item("secruntime").ToString(), runScaleInfo.Rows(i).Item("runtype").ToString())
            Next
            Globle.Method.UpdateDataForAccess(str, TMS_RUNSCALEINFO)
            Progress.ProgressForm.PerformStep()

            '�г�ͣվ�����Ϣ
            Dim stopScaleInfo As DataTable = New DataTable()
            str = "select * from tp.TMS_STOPSCALEINFO t where t.net_line_name='" & lineid & "' and net_ver='" & selectedNetVer & "'"
            stopScaleInfo = Globle.Method.ReadDataForOracle(str)
            Dim TMS_STOPSCALEINFO As New DataTable
            str = "select * from TMS_STOPSCALEINFO"
            TMS_STOPSCALEINFO = Globle.Method.ReadDataForAccess(str)
            For i = 0 To stopScaleInfo.Rows.Count - 1
                TMS_STOPSCALEINFO.Rows.Add( _
                                      sTrainDiagramID, stopScaleInfo.Rows(i).Item("routingname").ToString(), stopScaleInfo.Rows(i).Item("stopscalename").ToString(), stopScaleInfo.Rows(i).Item("seqnum").ToString(), stopScaleInfo.Rows(i).Item("staionname").ToString(), stopScaleInfo.Rows(i).Item("stoptime").ToString(), stopScaleInfo.Rows(i).Item("stoptype").ToString())
            Next
            Globle.Method.UpdateDataForAccess(str, TMS_STOPSCALEINFO)
            Progress.ProgressForm.PerformStep()

            '����ͼϵͳ������
            Dim sysPara As DataTable = New DataTable()
            str = "select * from tp.TMS_TIMETABLEPARAMETER "
            sysPara = Globle.Method.ReadDataForOracle(str)
            Dim TMS_TIMETABLEPARAMETER As New DataTable
            str = "select * from TMS_TIMETABLEPARAMETER"
            TMS_TIMETABLEPARAMETER = Globle.Method.ReadDataForAccess(str)
            For i = 0 To sysPara.Rows.Count - 1
                TMS_TIMETABLEPARAMETER.Rows.Add( _
                                       sysPara.Rows(i).Item("seq").ToString, sysPara.Rows(i).Item("paraname").ToString, sysPara.Rows(i).Item("paravalue").ToString, sTrainDiagramID)
            Next
            Globle.Method.UpdateDataForAccess(str, TMS_TIMETABLEPARAMETER)
            Progress.ProgressForm.PerformStep()

            'ʱ�̱�վ˳��
            Dim staSeqTime As DataTable = New DataTable()
            str = "select * from tp.TMS_TIMETABLESTASEQ t where net_ver='" & selectedNetVer & "' and net_line_name='" & lineid & "'"
            staSeqTime = Globle.Method.ReadDataForOracle(str)
            Dim TMS_TIMETABLESTASEQ As New DataTable
            str = "select * from TMS_TIMETABLESTASEQ"
            TMS_TIMETABLESTASEQ = Globle.Method.ReadDataForAccess(str)
            For i = 0 To staSeqTime.Rows.Count - 1
                TMS_TIMETABLESTASEQ.Rows.Add( _
                       staSeqTime.Rows(i).Item("staseqname").ToString(), staSeqTime.Rows(i).Item("staseq").ToString(), staSeqTime.Rows(i).Item("stationname").ToString(), sTrainDiagramID)
            Next
            Globle.Method.UpdateDataForAccess(str, TMS_TIMETABLESTASEQ)
            Progress.ProgressForm.PerformStep()
          

            '�г���Ϣ
            Dim trainOperateInfo As DataTable = New DataTable()
            str = "select * from tp.TMS_TRAININFO t where net_ver='" & selectedNetVer & "' and net_line_name='" & lineid & "'"
            trainOperateInfo = Globle.Method.ReadDataForOracle(str)
            Dim TMS_TRAININFO As New DataTable
            str = "select * from TMS_TRAININFO"
            TMS_TRAININFO = Globle.Method.ReadDataForAccess(str)
            For i = 0 To trainOperateInfo.Rows.Count - 1
                TMS_TRAININFO.Rows.Add( _
                                       sTrainDiagramID, trainOperateInfo.Rows(i).Item("routingname").ToString(), trainOperateInfo.Rows(i).Item("upordown").ToString(), trainOperateInfo.Rows(i).Item("ostationname").ToString(), trainOperateInfo.Rows(i).Item("dstationname").ToString(), trainOperateInfo.Rows(i).Item("linetraincode").ToString(), trainOperateInfo.Rows(i).Item("endsign").ToString(), trainOperateInfo.Rows(i).Item("type").ToString(), trainOperateInfo.Rows(i).Item("endusetype").ToString(), trainOperateInfo.Rows(i).Item("starttype").ToString(), trainOperateInfo.Rows(i).Item("stockstyleid").ToString(), trainOperateInfo.Rows(i).Item("passstaid").ToString(), trainOperateInfo.Rows(i).Item("traintype").ToString())
            Next
            Globle.Method.UpdateDataForAccess(str, TMS_TRAININFO)
            Progress.ProgressForm.PerformStep()

            '��·������Ϣ
            Dim sectionInfo As DataTable = New DataTable()
            str = "select * from tp.TMS_SECTIONINFO t where net_ver='" & selectedNetVer & "' and net_line_name='" & lineid & "'"
            sectionInfo = Globle.Method.ReadDataForOracle(str)
            Dim TMS_SECTIONINFO As New DataTable
            str = "select * from TMS_SECTIONINFO"
            TMS_SECTIONINFO = Globle.Method.ReadDataForAccess(str)
            For i = 0 To sectionInfo.Rows.Count - 1
                TMS_SECTIONINFO.Rows.Add( _
       sTrainDiagramID, sectionInfo.Rows(i).Item("sub_line_name").ToString(), sectionInfo.Rows(i).Item("sectionseq").ToString(), sectionInfo.Rows(i).Item("sectionname").ToString(), sectionInfo.Rows(i).Item("startstationname").ToString(), sectionInfo.Rows(i).Item("terminalstationname").ToString(), sectionInfo.Rows(i).Item("upsectiondistance").ToString(), sectionInfo.Rows(i).Item("downsectiondistance").ToString(), sectionInfo.Rows(i).Item("blocktype").ToString(), sectionInfo.Rows(i).Item("linenumber").ToString())
            Next
            Globle.Method.UpdateDataForAccess(str, TMS_SECTIONINFO)
            Progress.ProgressForm.PerformStep()

            '��վ��Ϣ
            Dim stationInfo As DataTable = New DataTable()
            str = "select * from tp.TMS_STATIONINFO t where net_ver='" & selectedNetVer & "' and net_line_name='" & lineid & "'"
            stationInfo = Globle.Method.ReadDataForOracle(str)
            Dim TMS_STATIONINFO As New DataTable
            str = "select * from TMS_STATIONINFO"
            TMS_STATIONINFO = Globle.Method.ReadDataForAccess(str)
            For i = 0 To stationInfo.Rows.Count - 1
                TMS_STATIONINFO.Rows.Add( _
                                        sTrainDiagramID, stationInfo.Rows(i).Item("sub_line_name").ToString(), stationInfo.Rows(i).Item("stationname").ToString(), stationInfo.Rows(i).Item("downsequence").ToString(), stationInfo.Rows(i).Item("outputname").ToString(), stationInfo.Rows(i).Item("englishname").ToString(), stationInfo.Rows(i).Item("englishshortname").ToString(), stationInfo.Rows(i).Item("linenum").ToString(), stationInfo.Rows(i).Item("stationtype").ToString(), stationInfo.Rows(i).Item("stationcharacter").ToString(), stationInfo.Rows(i).Item("stationpic").ToString(), stationInfo.Rows(i).Item("xcoordinate").ToString(), stationInfo.Rows(i).Item("ycoordinate").ToString(), stationInfo.Rows(i).Item("instruction").ToString(), stationInfo.Rows(i).Item("englishshortname").ToString())
            Next
            Globle.Method.UpdateDataForAccess(str, TMS_STATIONINFO)
            Progress.ProgressForm.PerformStep()

            '·����·��Ϣ
            Dim netLineInfo As DataTable = New DataTable()
            str = "select * from tp.TMS_LINEINFO t where net_ver='" & selectedNetVer & "' and net_line_name='" & lineid & "'"
            netLineInfo = Globle.Method.ReadDataForOracle(str)
            Dim TMS_LINEINFO As New DataTable
            str = "select * from TMS_LINEINFO"
            TMS_LINEINFO = Globle.Method.ReadDataForAccess(str)
            For i = 0 To netLineInfo.Rows.Count - 1
                TMS_LINEINFO.Rows.Add( _
                                       sTrainDiagramID, netLineInfo.Rows(i).Item("sublistno").ToString(), netLineInfo.Rows(i).Item("sub_line_name").ToString(), netLineInfo.Rows(i).Item("sublineshortname").ToString(), netLineInfo.Rows(i).Item("sublineenglishname").ToString(), netLineInfo.Rows(i).Item("sublinecode").ToString(), netLineInfo.Rows(i).Item("sublinelength").ToString(), netLineInfo.Rows(i).Item("remarks").ToString())
            Next
            Globle.Method.UpdateDataForAccess(str, TMS_LINEINFO)
            Progress.ProgressForm.PerformStep()

            '�г�ͣվ���
            Dim trainStopScale As DataTable = New DataTable()
            str = "select * from tp.TMS_STOPSCALE t where net_ver='" & selectedNetVer & "' and net_line_name='" & lineid & "'"
            trainStopScale = Globle.Method.ReadDataForOracle(str)
            Dim TMS_STOPSCALE As New DataTable
            str = "select * from TMS_STOPSCALE"
            TMS_STOPSCALE = Globle.Method.ReadDataForAccess(str)
            For i = 0 To trainStopScale.Rows.Count - 1
                TMS_STOPSCALE.Rows.Add(trainStopScale.Rows(i).Item("stopscalename").ToString(), sTrainDiagramID, trainStopScale.Rows(i).Item("stationseq").ToString(), trainStopScale.Rows(i).Item("stationname").ToString(), trainStopScale.Rows(i).Item("stopscalenum").ToString(), trainStopScale.Rows(i).Item("downstoptime").ToString(), trainStopScale.Rows(i).Item("upstoptime").ToString(), _
                                      trainStopScale.Rows(i).Item("downfirststoptime").ToString(), trainStopScale.Rows(i).Item("downendstoptime").ToString(), trainStopScale.Rows(i).Item("upendstoptime").ToString(), trainStopScale.Rows(i).Item("upfirststoptime").ToString())

            Next
            Globle.Method.UpdateDataForAccess(str, TMS_STOPSCALE)
            Progress.ProgressForm.PerformStep()

            '�г����б��
            Dim trainRunScale As DataTable = New DataTable()
            str = "select * from tp.TMS_RUNSCALE t where net_ver='" & selectedNetVer & "' and net_line_name='" & lineid & "'"
            trainRunScale = Globle.Method.ReadDataForOracle(str)
            Dim TMS_RUNSCALE As New DataTable
            str = "select * from TMS_RUNSCALE"
            TMS_RUNSCALE = Globle.Method.ReadDataForAccess(str)
            For i = 0 To trainRunScale.Rows.Count - 1
                TMS_RUNSCALE.Rows.Add(sTrainDiagramID, trainRunScale.Rows(i).Item("sectionseq").ToString(), trainRunScale.Rows(i).Item("linename").ToString(), trainRunScale.Rows(i).Item("sectionname").ToString(), trainRunScale.Rows(i).Item("secscalenum").ToString(), trainRunScale.Rows(i).Item("secscalename").ToString(), trainRunScale.Rows(i).Item("downruntime").ToString(), _
                                   trainRunScale.Rows(i).Item("upruntime").ToString(), trainRunScale.Rows(i).Item("downstartaddtime").ToString(), trainRunScale.Rows(i).Item("downstopaddtime").ToString(), trainRunScale.Rows(i).Item("upstartaddtime").ToString(), trainRunScale.Rows(i).Item("upstopaddtime").ToString())
            Next
            Globle.Method.UpdateDataForAccess(str, TMS_RUNSCALE)
            Progress.ProgressForm.EndProgress()
        Catch ex As Exception
            Progress.ProgressForm.EndProgress()
            MsgBox(sCurTable & " ������̷�����������������.tpm�ļ��ڵ������Ƿ����Ҫ��" & vbCrLf & ex.ToString)
            Exit Sub
        End Try
       
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If MsgBox("�Ƿ�ɾ����������ͼ�ĵ��Գ���", MsgBoxStyle.OkCancel, "����") = MsgBoxResult.Cancel Then
            Exit Sub
        End If
        Dim network As DataTable = New DataTable()
        Dim Str As String = "select traindiagramid,linktrainnum from tms_stockusinginfo t where t.RUNSCALESTYLE='���Գ�'"
        network = Globle.Method.ReadDataForAccess(Str)
        If network.Rows.Count < 1 Then
            MsgBox("������������ͼ��", MsgBoxStyle.OkOnly, "ע��")
            Exit Sub
        End If
        Progress.ProgressForm.StartProgress(network.Rows.Count, "������������ͼ�����Ժ�...")
        For i As Integer = 0 To network.Rows.Count - 1
            Str = "delete from tms_timetableinfo where traindiagramid='" & network.Rows(i).Item("traindiagramid") & "' and trainnum='" & network.Rows(i).Item("linktrainnum") & "'"
            Globle.Method.UpdateDataForAccess(Str)
            Progress.ProgressForm.PerformStep()
        Next
        Str = "delete from tms_stockusinginfo t where t.RUNSCALESTYLE='���Գ�'"
        Globle.Method.UpdateDataForAccess(Str)
        Progress.ProgressForm.PerformStep()
        Progress.ProgressForm.EndProgress()
    End Sub

    Public InputDatabasePath As String = ""
    Public InputDatabaseName As String = ""
    Public InputDatabaseConString As String = ""
    Public Const InputDatabasePassWord As String = "tpmadmin"
    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        Dim fileopen As New OpenFileDialog
        With fileopen
            .Filter = "����ͼ���ݿ�|*.mdb;*.tpm;*.mpm"
            .Title = "������ͼ�ļ�"
        End With
        '****************
        If fileopen.ShowDialog = Windows.Forms.DialogResult.OK Then
            If fileopen.FileName = Application.StartupPath & "" Then
                MessageBox.Show("��ѡ�����ݿ⣡", "û�д򿪵��ļ�", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            Else
                Me.grpBox.Enabled = True
                Dim pathposition As Integer
                Dim i As Integer
                For i = 1 To fileopen.FileName.Length
                    pathposition = InStrRev(fileopen.FileName, "\", -1)
                Next
                InputDatabasePath = Mid(fileopen.FileName, 1, pathposition)
                InputDatabaseName = Mid(fileopen.FileName, pathposition + 1)
            End If
        Else
            Exit Sub
        End If
        InputDatabaseConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & InputDatabasePath & InputDatabaseName & "';Persist Security Info=False;Jet OLEDB:Database Password= " & InputDatabasePassWord & ""
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(InputDatabaseConString)
        Try
            MyConn.Open()
        Catch ex As Exception
            MsgBox(ex.Message & "����ϵ���򿪷���Ա��")
            Exit Sub
        End Try
        MyConn.Close()
        Me.txtPath.Text = InputDatabasePath & InputDatabaseName
        Dim strTable3 As String = "select * from ʱ�̱���Ϣ�� order by ʱ�̱�����"
        Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, InputDatabaseConString)
        '����һ��Datasetd
        Dim myDataSet3 As Data.DataSet = New Data.DataSet
        Mydc3.Fill(myDataSet3)
        Me.cmbTrainDiaName.DataSource = myDataSet3.Tables(0)
        Me.cmbTrainDiaName.DisplayMember = "ʱ�̱�����"
        MyConn.Close()
        grpBox.Enabled = True
        selectedID = Now.Year.ToString & "_" & Now.Month.ToString & "_" & Now.Day.ToString & "_" & Now.Hour.ToString & "_" & Now.Minute.ToString & "_" & Now.Second.ToString
        dtpFirstTime.Enabled = True
        dtpEndTime.Enabled = True
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            txtPath.Enabled = True
            btnOpen.Enabled = True
            CheckBox2.Checked = False
        Else
            txtPath.Enabled = False
            btnOpen.Enabled = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim str As String = "select traindiagramid,timetablename,traindiastylename,beginexecutetime,endexecutetime,schedulingdepart,net_ver from tp.tms_traindiagraminfo t where t.net_line_name='" & lineid & "' and diagramstate='���ͨ��'"
            timeTable = Globle.Method.ReadDataForOracle(str)
            If IsNothing(timeTable) = False AndAlso timeTable.Rows.Count > 0 Then
                cmbTrainDiamStyle.SelectedIndex = 0
                cmbLineInfo.SelectedIndex = 0
                grpBox.Enabled = True
                Label1.Text = "״̬������ɹ���"
                dtpFirstTime.Enabled = False
                dtpEndTime.Enabled = False
                Exit Sub
            End If
            Label1.Text = "״̬������ʧ�ܣ�"
        Catch ex As Exception
            Label1.Text = "״̬������ʧ�ܣ�"
        End Try
       
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            Button2.Enabled = True
            CheckBox1.Checked = False
        Else
            Button2.Enabled = False
        End If
    End Sub

    Private Sub frmODSInputTimetable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cmbLineInfo.Items.Add(CurLineName)
        cmbLineInfo.SelectedIndex = 0
    End Sub
End Class

