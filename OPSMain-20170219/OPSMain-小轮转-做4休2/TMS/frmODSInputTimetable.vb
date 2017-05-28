Imports System.Windows.Forms
Public Class frmODSInputTimetable
    Public UserInfo As String = "����/������·"
    Private Sub frmInputTimetable_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '�򵥵����ݰ󶨹���
        TMSlocalDataSet.PD_LINEINFO.Clear()
        Dim tmpUserInfo() As String = UserInfo.Split("/")
        If tmpUserInfo(0) = "��" Then

            TMSlocalDataSet.Fill("PD_LINEINFO", "linename='" & tmpUserInfo(1) & "' order by lineid asc")
        End If
        If tmpUserInfo(0) <> "��" And tmpUserInfo(0).Contains("����") Then

            TMSlocalDataSet.Fill("PD_LINEINFO", "1=1 order by lineid asc")
        End If
        If tmpUserInfo(0) <> "��" And tmpUserInfo(0).Contains("����") = False Then

            TMSlocalDataSet.Fill("PD_LINEINFO", "linemanagerid='" & tmpUserInfo(0) & "' order by lineid asc")
        End If

        TMSlocalDataSet.TMS_TRAINDIAGRAMSTYLE.Clear()

        If CurLineName = "" Then    '20160306�޸ģ����ܣ�����·������Աֻ�ܵ��뱾��·����ͼ
            Me.cmbLineInfo.DataSource = TMSlocalDataSet.PD_LINEINFO
            Me.cmbLineInfo.DisplayMember = "LINENAME"
        Else
            TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMSTYLE", "1=1 order by TRAINDIASTYLENAME asc") '��"lineid='" & CurLineName & "'
            Me.cmbLineInfo.Items.Add(CurLineName)
            Me.cmbLineInfo.Text = CurLineName
            Me.cmbTrainDiamStyle.DataSource = TMSlocalDataSet.TMS_TRAINDIAGRAMSTYLE
            Me.cmbTrainDiamStyle.DisplayMember = "DATENAME"
        End If

        Me.grpBox.Enabled = False
        Me.dtpEndTime.Value = Me.dtpEndTime.Value.AddDays(30)
    End Sub

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        Dim fileopen As New OpenFileDialog
        With fileopen
            '.InitialDirectory = Application.StartupPath
            .Filter = "����ͼ���ݿ�|*.mdb;*.tpm;*.mpm"
            .Title = "������ͼ�ļ�"
            '.RestoreDirectory = True
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

    End Sub

    Private Sub cmbTrainDiaName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTrainDiaName.SelectedIndexChanged
        Me.txtSaveName.Text = Me.cmbTrainDiaName.Text
    End Sub

    Private Sub btnBegin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBegin.Click
        If Me.txtSaveName.Text <> "" And Me.txtMakerDep.Text <> "" Then
            If IfSameDiagramName(Me.txtSaveName.Text.ToString.Trim) = True Then
                MsgBox("�洢������ͼ�����Ѿ����ڣ����޸�Ϊ�������ƺ��ٵ��룡")
                Exit Sub
            End If
            If Me.dtpEndTime.Value <= Me.dtpFirstTime.Value Then
                MsgBox("����ͼ����ִ��ʱ�䲻��С�ڿ�ʼִ��ʱ��!", , "����")
                Exit Sub
            End If
            If MessageBox.Show("���Ƿ�Ҫ����[" & Me.cmbTrainDiaName.Text & " ] ����ͼ��", "��ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
                Dim sTrainDiagramID As String
                Dim sInputTime As String
                sTrainDiagramID = Now.Year & "_" & Now.Month & "_" & Now.Day & "_" & Now.Hour & "_" & Now.Minute & "_" & Now.Second
                sInputTime = Date.Now.ToString '��������ͼ��ʱ��
                TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.AddTMS_TRAINDIAGRAMINFORow( _
                        sTrainDiagramID, Me.cmbLineInfo.Text.Trim, Me.txtSaveName.Text.ToString.Trim, Me.cmbTrainDiamStyle.Text, sInputTime, Me.dtpFirstTime.Text, Me.dtpEndTime.Text, Me.txtMakerDep.Text)
                TMSlocalDataSet.Update("TMS_TRAINDIAGRAMINFO")
                If (Me.cmbTrainDiaName.Text.Trim <> "") Then
                    Call InputACCESSTimeTable(sTrainDiagramID, Me.cmbTrainDiaName.Text)
                End If
                Me.Dispose()
            Else
                Me.txtSaveName.Text = ""
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

   
    Private Sub cmbLineInfo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLineInfo.SelectedIndexChanged
        TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMSTYLE", "1=1 order by TRAINDIASTYLENAME asc")
        Me.cmbTrainDiamStyle.DataSource = TMSlocalDataSet.TMS_TRAINDIAGRAMSTYLE
        Me.cmbTrainDiamStyle.DisplayMember = "DATENAME"
    End Sub
End Class
