
Imports System.Windows.Forms

Public Class frmODSMain

    Dim sPriCurLineName As String
    Dim sPriCurStaName As String
    Dim sPriCurSecName As String
    Dim sPriCurSelectState As String
    Dim HasConnected As Boolean

    Sub New()

        ' 此调用是 Windows 窗体设计器所必需的。
        InitializeComponent()
        Me.Text += "当前线路:" & CurLineName
        Call GlobalFunc.LoadDepotData()      '加载轮班点、车场信息，用于车场、车站类型识别
    End Sub

    Private Sub frmODSMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        End
    End Sub

    '重新加载底图
   
   
    '得到用户权限名称
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

    Private Sub 退出XToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub ToolStripButton4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call ToolStripMenuItem5_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nf As New TMS.FrmODSTraindiagramSelect
        TMS.CurLineName = CurLineName
        nf.sCurState = TMS.FrmODSTraindiagramSelect.ScurStateValue.打开运行图
        nf.Text = "运行图浏览"
        nf.Show()
    End Sub


    Private Sub 关于AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        frmAbout.Show()
    End Sub

    Private Sub 退出EToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 退出EToolStripMenuItem.Click
        End
    End Sub

   

    Private Sub ToolStripButton12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 乘务计划ToolStripMenuItem1_Click(Nothing, Nothing)
    End Sub

    Private Sub 乘务计划ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 乘务计划ToolStripMenuItem1.Click
        Dim nf As New CS_CSMaker.frmCSTimeTableMain(CurLineName)
        'CS_CSMaker.CurrentUserRole = CurrentUserRole
        nf.Show()
    End Sub

    Private Sub 乘务员匹配ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 乘务员匹配ToolStripMenuItem.Click
        Dim nf As New FrmClassCoordinate(CurLineName)
        nf.Show()
    End Sub

    Private Sub Ts运行图浏览_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Ts运行图浏览.Click
        Call ToolStripMenuItem5_Click(Nothing, Nothing)
    End Sub

    Private Sub ts运行图管理_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ts运行图管理.Click
        Dim nf As New frmDiagrameManger
        nf.StartPosition = FormStartPosition.CenterScreen
        nf.Show()
    End Sub

    Private Sub 运行图导入ToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 运行图导入ToolStripMenuItem.Click
        Dim nf As New frmODSInputTimetable
        Dim sline As Integer = Integer.Parse(CurLineName.Substring(0, CurLineName.Length - 2))
        nf.lineid = sline.ToString("00")
        nf.ShowDialog()
        GetSta()
    End Sub

    Private Sub 乘务员绩效管理ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 乘务员绩效管理ToolStripMenuItem.Click
        Dim dirverperformance As New DriverPerformance
        dirverperformance.Show()
    End Sub

    Private Sub 乘务工作量WToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New JGexcel
        frm.Show()
    End Sub

    Private Sub 乘务员信息管理ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 乘务员信息管理ToolStripMenuItem1.Click
        Dim frm As New CrewTrainingManager.CrewInfoIndex(CurLineName)
        frm.Show()
    End Sub

    Private Sub 乘务计划发布参数ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim fm As New frmPlanOutput
        fm.Show()

    End Sub

    Private Sub 交路距离参数ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 交路距离参数ToolStripMenuItem.Click
        Dim frm As New FrmLengthPara()
        frm.Show()
    End Sub

    Private Sub 乘务计划输出ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim fm As New FrmPrintPlan
        fm.Show()
    End Sub

 

    Private Sub 用户管理UToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 用户管理UToolStripMenuItem.Click
        FrmUserManager.Show()
    End Sub

    Private Sub 区域参数ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 区域参数ToolStripMenuItem.Click
        Dim nf As New FrmAreaSetting()
        nf.Show()
    End Sub
    Private Sub frmODSMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim VersionList As String = ""
        Try
            Dim filename As String = Application.StartupPath & "\VersonInfo.dat"
            VersionList = GetVersionInfo(filename)
            Me.Text = "CS主界面-北京地铁 " & VersionList & "版(" & CurLineName & ")"
        Catch ex As Exception
        End Try
        pnlMap.BackgroundImage = Image.FromFile(".\Config\北京地铁15号线.jpg")
        GetSta()
        For Each linename As String In Stalist.Keys
            TreeView1.Nodes.Add(linename, linename)
            For i As Integer = 0 To Stalist(linename).Count - 1
                TreeView1.Nodes(linename).Nodes.Add(Stalist(linename)(i))
            Next
        Next
    End Sub

    Private Sub 更换用户CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 更换用户CToolStripMenuItem.Click
        Process.Start("OPS Main.exe")
        End
    End Sub

    Private Sub 人数测算ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 人数测算ToolStripMenuItem.Click
        FrmCalPerson.Show()
    End Sub
    Private Sub 乘务输出ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 乘务输出ToolStripMenuItem.Click
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
    Private Sub 检查更新ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 检查更新ToolStripMenuItem.Click
        Dim VersionList As String = ""
        Try
            Dim filename As String = Application.StartupPath & "\VersonInfo.dat"
            VersionList = GetVersionInfo(filename)
        Catch ex As Exception
        End Try
        Dim info() As String = VersionList.Split("/")
        Dim Str = "select * from cs_updateinfo where version =(select max(version) from cs_updateinfo t where t.lineid='" & CurLineName & "' and t.software='乘务计划编制系统') and lineid='" & CurLineName & "' and software='乘务计划编制系统'"
        Try
            Dim tmpDT As DataTable = Globle.Method.ReadDataForOracle(Str)
            If IsNothing(tmpDT) = False AndAlso tmpDT.Rows.Count > 0 Then
                If tmpDT.Rows(0).Item("version").ToString.Trim <> "" AndAlso tmpDT.Rows(0).Item("version").ToString.Trim <> info(0).Trim Then
                    If MsgBox("检测到新版本，是否下载更新？", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                        Try
                            Dim twrite As New IO.StreamWriter(System.Windows.Forms.Application.StartupPath & "\LineInf.dat", False)
                            twrite.WriteLine(CurLineName)
                            twrite.Close()
                            System.Diagnostics.Process.Start(Application.StartupPath & "\\UpEASoft.exe")
                            Me.Close()
                            Exit Sub
                        Catch ex As Exception
                            MsgBox("启动更新程序失败，正在打开原程序")
                        End Try
                    End If
                End If
            End If
            MsgBox("当前已是最新版本！")
        Catch ex As Exception

        End Try
    End Sub

   
End Class

