Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class frmCSMakeTTSelect
    Public flag As Integer = 0 '用于指示是经过“退出”0退出的，还是“确定退出的”1，若为后者需要刷新底图
    Private Sub Fillinfomation()
       
        Dim sqlstr As String = "SELECT * FROM TMS_TRAINDIAGRAMINFO WHERE LINENAME='" & Me.Comline.Text.Trim & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        Me.ComWDTT.DataSource = tempTable
        Me.ComWDTT.DisplayMember = "TIMETABLENAME"
    End Sub

    Private Sub frmCSTimeTableSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sqlstr As String = ""
        If CurLineName <> "" Then
            Me.Comline.Items.Add(CurLineName)
            Me.Comline.Text = CurLineName
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CSTrainsAndDrivers = New CSTrainAndDrivers()
        strCurlineID = Me.Comline.Text.ToString.Trim
        strDiagram = Me.ComWDTT.Text.ToString.Trim
        DiagramCurID = GetDiagramVersionFromName(Me.ComWDTT.Text.ToString.Trim)
        strQCurCSPlanName = ""
        strQCurCSPlanID = ""
        'frmCSTimeTableMain.labName.Text = "当前乘务计划：无" & CStr(strQCurCSPlanName)

        'Common.Global.StartProgress(5, "正在装载运行图，请稍等...")
        Call CSInitSystemVariant()
        'Common.Global.PerformStep()
        'Call InputCSTimetableInf()
        Call CSreadNetStaAndSecData(DiagramCurID)
        'Common.Global.PerformStep()
        Call CSInputChediAndTrainJianGeData("", DiagramCurID)
        ' Common.Global.PerformStep()
        Call CSReadBaseTrainInf(DiagramCurID)
        ' Common.Global.PerformStep()
        Call CSReadTrainAndTimeTableInf(DiagramCurID, Me.ProgressBar1)
       
        sState = "乘务计划编制"
        flag = 1 '用于指示是经过“退出”0退出的，还是“确定退出的”1，若为后者需要刷新底图
        Me.Close()
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Comline_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Comline.SelectedIndexChanged
        Fillinfomation()
    End Sub

  
End Class