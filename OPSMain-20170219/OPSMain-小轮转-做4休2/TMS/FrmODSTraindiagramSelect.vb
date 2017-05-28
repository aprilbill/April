
Public Class FrmODSTraindiagramSelect
    Public sCurState As ScurStateValue
    Public sCurLine As String
    Public UserInfo As String = "集团/所有线路"

    Public Enum ScurStateValue
        打开运行图
        打开运行图分交路指标
        打开运行图分区间指标
        打开运行图车底运用指标
        新建运行图
    End Enum

    Private Sub BtnNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNo.Click
        Me.Dispose()
    End Sub

    Private Sub FrmTraindiagramSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '简单的数据绑定过程
        Dim tmpUserInfo() As String = UserInfo.Split("/")
        Dim PD_LINEINFO As New DataTable
        Dim sqlstr As String = ""
        If tmpUserInfo(0) = "空" Then
            sqlstr = "select * PD_LINEINFO where linename='" & tmpUserInfo(1) & "' order by lineid asc"
            PD_LINEINFO = Globle.Method.ReadDataForAccess(sqlstr)
        End If
        If tmpUserInfo(0) <> "空" And tmpUserInfo(0).Contains("集团") Then
            sqlstr = "select * PD_LINEINFO order by lineid asc"
            PD_LINEINFO = Globle.Method.ReadDataForAccess(sqlstr)
        End If
        If tmpUserInfo(0) <> "空" And tmpUserInfo(0).Contains("集团") = False Then
            sqlstr = "select * PD_LINEINFO where linemanagerid='" & tmpUserInfo(0) & "' order by lineid asc"
            PD_LINEINFO = Globle.Method.ReadDataForAccess(sqlstr)
            CurLineName = tmpUserInfo(0)
        End If

        If CurLineName = "" Then      '20160306修改，功能：本线路管理人员只能导入本线路运行图
            Me.Comline.DataSource = PD_LINEINFO
            Me.Comline.DisplayMember = "LINENAME"
        Else
            Me.Comline.Items.Add(CurLineName)
            Me.Comline.Text = CurLineName
            Dim TMS_TRAINDIAGRAMSTYLE As New DataTable
            Dim sql As String = "select * from TMS_TRAINDIAGRAMSTYLE order by TRAINDIASTYLENAME asc"
            TMS_TRAINDIAGRAMSTYLE = Globle.Method.ReadDataForAccess(sql)
            If IsNothing(TMS_TRAINDIAGRAMSTYLE) = False AndAlso TMS_TRAINDIAGRAMSTYLE.Rows.Count > 0 Then
                Me.ComboDiagram.DataSource = TMS_TRAINDIAGRAMSTYLE
                Me.ComboDiagram.DisplayMember = "DATENAME"
            End If
        End If

      
    End Sub

    Private Sub Comline_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Comline.SelectedIndexChanged
        Dim TMS_TRAINDIAGRAMSTYLE As New DataTable
        Dim sqlstr As String = "select * from TMS_TRAINDIAGRAMSTYLE order by TRAINDIASTYLENAME asc"
        TMS_TRAINDIAGRAMSTYLE = Globle.Method.ReadDataForAccess(sqlstr)
        If IsNothing(TMS_TRAINDIAGRAMSTYLE) = False AndAlso TMS_TRAINDIAGRAMSTYLE.Rows.Count > 0 Then
            Me.ComboDiagram.DataSource = TMS_TRAINDIAGRAMSTYLE
            Me.ComboDiagram.DisplayMember = "DATENAME"
            Call Fillinfomation()
        End If
    End Sub

    Private Sub ComboDiagram_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboDiagram.SelectedIndexChanged
        Call Fillinfomation()
    End Sub

    Private Sub BtnYes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnYes.Click
        Select Case sCurState
            Case ScurStateValue.打开运行图

                If Me.ComboBox.Text <> "" Then
                    'If MessageBox.Show("您要打开的运行图代码为" & Me.ComboBox.Text, "提示操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) = Windows.Forms.DialogResult.OK Then
                    ODSPubpara.DiagramSelect = GetDiagramVersionFromName(Me.ComboBox.Text.ToString.Trim)
                    Me.Dispose()
                    Call LoadDiagramData("打开运行图")
                    Dim tmprow As Integer
                    tmprow = UBound(TrainInf)
                    ODSPubpara.sCurShowListState = "显示单线全图"
                    Dim nf As New frmODSTimeTableMain
                    nf.Show()
                Else
                    MessageBox.Show("您没有选中运行图，请重新选择！", "提示操作", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
        End Select

    End Sub

    Private Sub Fillinfomation()
        Dim TMS_TRAINDIAGRAMINFO As New DataTable
        Dim sqlstr As String = "select * from TMS_TRAINDIAGRAMINFO where LINENAME='" & Me.Comline.Text.Trim & "' and TRAINDIASTYLENAME='" & Me.ComboDiagram.Text & "'"
        TMS_TRAINDIAGRAMINFO = Globle.Method.ReadDataForAccess(sqlstr)
        If IsNothing(TMS_TRAINDIAGRAMINFO) = False AndAlso TMS_TRAINDIAGRAMINFO.Rows.Count > 0 Then
            Me.ComboBox.DataSource = TMS_TRAINDIAGRAMINFO
            Me.ComboBox.DisplayMember = "TIMETABLENAME"
        Else
            Me.ComboBox.DataSource = Nothing
        End If
    End Sub

    
End Class