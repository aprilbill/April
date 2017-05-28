Imports System.Windows.Forms
Public Class frmODSInputTimetable
    Public UserInfo As String = "集团/所有线路"
    Private Sub frmInputTimetable_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '简单的数据绑定过程
        TMSlocalDataSet.PD_LINEINFO.Clear()
        Dim tmpUserInfo() As String = UserInfo.Split("/")
        If tmpUserInfo(0) = "空" Then

            TMSlocalDataSet.Fill("PD_LINEINFO", "linename='" & tmpUserInfo(1) & "' order by lineid asc")
        End If
        If tmpUserInfo(0) <> "空" And tmpUserInfo(0).Contains("集团") Then

            TMSlocalDataSet.Fill("PD_LINEINFO", "1=1 order by lineid asc")
        End If
        If tmpUserInfo(0) <> "空" And tmpUserInfo(0).Contains("集团") = False Then

            TMSlocalDataSet.Fill("PD_LINEINFO", "linemanagerid='" & tmpUserInfo(0) & "' order by lineid asc")
        End If

        TMSlocalDataSet.TMS_TRAINDIAGRAMSTYLE.Clear()

        If CurLineName = "" Then    '20160306修改，功能：本线路管理人员只能导入本线路运行图
            Me.cmbLineInfo.DataSource = TMSlocalDataSet.PD_LINEINFO
            Me.cmbLineInfo.DisplayMember = "LINENAME"
        Else
            TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMSTYLE", "1=1 order by TRAINDIASTYLENAME asc") '’"lineid='" & CurLineName & "'
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
            .Filter = "运行图数据库|*.mdb;*.tpm;*.mpm"
            .Title = "打开运行图文件"
            '.RestoreDirectory = True
        End With
        '****************
        If fileopen.ShowDialog = Windows.Forms.DialogResult.OK Then
            If fileopen.FileName = Application.StartupPath & "" Then
                MessageBox.Show("请选择数据库！", "没有打开的文件", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
            MsgBox(ex.Message & "请联系程序开发人员！")
            Exit Sub
        End Try
        MyConn.Close()
        Me.txtPath.Text = InputDatabasePath & InputDatabaseName
        Dim strTable3 As String = "select * from 时刻表信息表 order by 时刻表名称"
        Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, InputDatabaseConString)
        '创建一个Datasetd
        Dim myDataSet3 As Data.DataSet = New Data.DataSet
        Mydc3.Fill(myDataSet3)
        Me.cmbTrainDiaName.DataSource = myDataSet3.Tables(0)
        Me.cmbTrainDiaName.DisplayMember = "时刻表名称"
        MyConn.Close()

    End Sub

    Private Sub cmbTrainDiaName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTrainDiaName.SelectedIndexChanged
        Me.txtSaveName.Text = Me.cmbTrainDiaName.Text
    End Sub

    Private Sub btnBegin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBegin.Click
        If Me.txtSaveName.Text <> "" And Me.txtMakerDep.Text <> "" Then
            If IfSameDiagramName(Me.txtSaveName.Text.ToString.Trim) = True Then
                MsgBox("存储的运行图名称已经存在，请修改为其他名称后再导入！")
                Exit Sub
            End If
            If Me.dtpEndTime.Value <= Me.dtpFirstTime.Value Then
                MsgBox("运行图结束执行时间不能小于开始执行时间!", , "警告")
                Exit Sub
            End If
            If MessageBox.Show("你是否要导入[" & Me.cmbTrainDiaName.Text & " ] 运行图！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
                Dim sTrainDiagramID As String
                Dim sInputTime As String
                sTrainDiagramID = Now.Year & "_" & Now.Month & "_" & Now.Day & "_" & Now.Hour & "_" & Now.Minute & "_" & Now.Second
                sInputTime = Date.Now.ToString '导入运行图的时间
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
            MessageBox.Show("您选择输入的运行图相关信息不完整，请重新选择！", "错误操作！", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
