
Imports System.Windows.Forms

Public Class frmODSMain

    Dim sPriCurLineName As String
    Dim sPriCurStaName As String
    Dim sPriCurSecName As String
    Dim sPriCurSelectState As String
    Dim HasConnected As Boolean

    Sub New()

        ' �˵����� Windows ���������������ġ�
        InitializeComponent()
        Me.Text += "��ǰ��·:" & CurLineName
        Call GlobalFunc.LoadDepotData()      '�����ְ�㡢������Ϣ�����ڳ�������վ����ʶ��
    End Sub

    Private Sub frmODSMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        End
    End Sub

    '���¼��ص�ͼ
   
   
    '�õ��û�Ȩ������
    Private Function GetUserPower(ByVal sID As String) As String
        GetUserPower = "NULL"
        If sID.Trim <> "" Then
            Dim sqlstr As String = "select * from PD_USERFUCTIONID where FUNCTIONID='" & sID & "'"
            Dim tempDt As New DataTable
            tempDt = Globle.Method.ReadDataForAccess(sqlstr)
            If tempDt.Rows.Count > 0 Then
                GetUserPower = tempDt.Rows(0).Item("FUNCTIONNAME")
                Exit Function
            End If
        End If
    End Function

    Private Sub �˳�XToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call ToolStripMenuItem5_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nf As New TMS.FrmODSTraindiagramSelect
        TMS.CurLineName = CurLineName
        nf.sCurState = TMS.FrmODSTraindiagramSelect.ScurStateValue.������ͼ
        nf.Text = "����ͼ���"
        nf.Show()
    End Sub


    Private Sub ����AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmAbout.Show()
    End Sub

    Private Sub �˳�EToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles �˳�EToolStripMenuItem.Click
        End
    End Sub

   

    Private Sub ToolStripButton12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call ����ƻ�ToolStripMenuItem1_Click(Nothing, Nothing)
    End Sub

    Private Sub ����ƻ�ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ����ƻ�ToolStripMenuItem1.Click
        Dim nf As New CS_CSMaker.frmCSTimeTableMain(CurLineName)
        'CS_CSMaker.CurrentUserRole = CurrentUserRole
        nf.Show()
    End Sub

    Private Sub ����Աƥ��ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ����Աƥ��ToolStripMenuItem.Click
        Dim nf As New FrmClassCoordinate(CurLineName)
        nf.Show()
    End Sub

    Private Sub Ts����ͼ���_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ts����ͼ���.Click
        Call ToolStripMenuItem5_Click(Nothing, Nothing)
    End Sub

    Private Sub ts����ͼ����_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts����ͼ����.Click
        Dim nf As New frmDiagrameManger
        nf.StartPosition = FormStartPosition.CenterScreen
        nf.Show()
    End Sub

    Private Sub ����ͼ����ToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ����ͼ����ToolStripMenuItem.Click
        Dim nf As New frmODSInputTimetable
        Dim sline As Integer = Integer.Parse(CurLineName.Substring(0, CurLineName.Length - 2))
        nf.lineid = sline.ToString("00")
        nf.ShowDialog()
        GetSta()
    End Sub

    Private Sub ����Ա��Ч����ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ����Ա��Ч����ToolStripMenuItem.Click
        Dim dirverperformance As New DriverPerformance
        dirverperformance.Show()
    End Sub

    Private Sub ��������WToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New JGexcel
        frm.Show()
    End Sub

    Private Sub ����Ա��Ϣ����ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ����Ա��Ϣ����ToolStripMenuItem1.Click
        Dim frm As New CrewTrainingManager.CrewInfoIndex(CurLineName)
        frm.Show()
    End Sub

    Private Sub ����ƻ���������ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim fm As New frmPlanOutput
        fm.Show()

    End Sub

    Private Sub ��·�������ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ��·�������ToolStripMenuItem.Click
        Dim frm As New FrmLengthPara()
        frm.Show()
    End Sub

    Private Sub ����ƻ����ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim fm As New FrmPrintPlan
        fm.Show()
    End Sub

 

    Private Sub �û�����UToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles �û�����UToolStripMenuItem.Click
        FrmUserManager.Show()
    End Sub

    Private Sub �������ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles �������ToolStripMenuItem.Click
        Dim nf As New FrmAreaSetting()
        nf.Show()
    End Sub
    Private Sub frmODSMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim VersionList As String = ""
        Try
            Dim filename As String = Application.StartupPath & "\VersonInfo.dat"
            VersionList = GetVersionInfo(filename)
            Me.Text = "CS������-�Ϻ����� " & VersionList & "��(" & CurLineName & ")"
        Catch ex As Exception
        End Try
        pnlMap.BackgroundImage = Image.FromFile(".\Config\�Ϻ�����.jpg")
        GetSta()
        For Each linename As String In Stalist.Keys
            TreeView1.Nodes.Add(linename, linename)
            For i As Integer = 0 To Stalist(linename).Count - 1
                TreeView1.Nodes(linename).Nodes.Add(Stalist(linename)(i))
            Next
        Next
    End Sub

    Private Sub �����û�CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles �����û�CToolStripMenuItem.Click
        Process.Start("OPS Main.exe")
        End
    End Sub

    Private Sub ��������ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ��������ToolStripMenuItem.Click
        FrmCalPerson.Show()
    End Sub
    Private Sub �������ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles �������ToolStripMenuItem.Click
        Dim frm As New FrmUpSchedule()
        frm.ShowDialog()
    End Sub
    Public Function GetVersionInfo(ByVal FileName As String) As String
        Dim stream As New System.IO.FileStream(FileName, System.IO.FileMode.Open)
        Dim Sr As New System.IO.StreamReader(stream)
        Dim str As String = Sr.ReadLine
        Sr.Close()
        Return str
    End Function
    Private Sub ������ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ������ToolStripMenuItem.Click
        Dim VersionList As String = ""
        Try
            Dim filename As String = Application.StartupPath & "\VersonInfo.dat"
            VersionList = GetVersionInfo(filename)
        Catch ex As Exception
        End Try
        Dim info() As String = VersionList.Split("/")
        Dim Str = "select * from cs_updateinfo where version =(select max(version) from cs_updateinfo t where t.lineid='" & CurLineName & "' and t.software='����ƻ�����ϵͳ') and lineid='" & CurLineName & "' and software='����ƻ�����ϵͳ'"
        Try
            Dim tmpDT As DataTable = Globle.Method.ReadDataForOracle(Str)
            If IsNothing(tmpDT) = False AndAlso tmpDT.Rows.Count > 0 Then
                If tmpDT.Rows(0).Item("version").ToString.Trim <> "" AndAlso tmpDT.Rows(0).Item("version").ToString.Trim <> info(0).Trim Then
                    If MsgBox("��⵽�°汾���Ƿ����ظ��£�", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                        Try
                            Dim twrite As New IO.StreamWriter(System.Windows.Forms.Application.StartupPath & "\LineInf.dat", False)
                            twrite.WriteLine(CurLineName)
                            twrite.Close()
                            System.Diagnostics.Process.Start(Application.StartupPath & "\\UpEASoft.exe")
                            Me.Close()
                            Exit Sub
                        Catch ex As Exception
                            MsgBox("�������³���ʧ�ܣ����ڴ�ԭ����")
                        End Try
                    End If
                End If
            End If
            MsgBox("��ǰ�������°汾��")
        Catch ex As Exception

        End Try
    End Sub

   
End Class

