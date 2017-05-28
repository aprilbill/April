Public Class FrmOpenPlan

    Private net As Coordination2.Net
    Public Curline As Coordination2.Line
    Public StartDate As Date
    Public EndDate As Date

    Public Sub New(ByVal netpa As Coordination2.Net)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        net = netpa
    End Sub

    Private Sub FrmOpenPlan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If CurLineName = "" Then
            For Each lin As Coordination2.Line In net.Lines
                Me.CmbLine.Items.Add(lin.Name)
            Next
        Else
            Me.CmbLine.Items.Add(CurLineName)
        End If
        If Me.CmbLine.Items.Count > 0 Then
            Me.CmbLine.SelectedIndex = 0
        End If
    End Sub

    Private Sub Btn_Cancle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancle.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub CmbLine_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbLine.SelectedIndexChanged
        Call RefreshPlan()
    End Sub

    Private Sub Btn_Delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Delete.Click
        Me.StartDate = Me.DTPStart.Value.Date
        Me.EndDate = Me.DTPEnd.Value.Date
        If MsgBox("确定删除'" & Me.StartDate.ToString("yyyy/MM/dd") & "'到'" & Me.EndDate.ToString("yyyy/MM/dd") & "'的乘务计划", MsgBoxStyle.OkCancel, "提醒") = MsgBoxResult.Ok Then
            '===删除cs_datetimetable表
            Dim str As String = "delete from cs_datetimetable where lineid='" & Me.CmbLine.Text.Trim & "' and datediff('d',dateno,Format('" & Me.StartDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 and " & _
                                "datediff('d',dateno,Format('" & Me.EndDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>=0"
            Globle.Method.UpdateDataForAccess(str)
            '===删除cs_corresponding表
            str = "delete from cs_corresponding where lineid='" & Me.CmbLine.Text.Trim & "' and datediff('d',dateno,Format('" & Me.StartDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 and " & _
                                "datediff('d',dateno,Format('" & Me.EndDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>=0"
            Globle.Method.UpdateDataForAccess(str)
            '===删除cs_deadheading表
            str = "delete from cs_deadheading where lineid='" & Me.CmbLine.Text.Trim & "' and datediff('d',dateno,Format('" & Me.StartDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 and " & _
                                "datediff('d',dateno,Format('" & Me.EndDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>=0"
            Globle.Method.UpdateDataForAccess(str)
            '===删除cs_amdutycorrespond表
            str = "delete from cs_amdutycorrespond where lineid='" & Me.CmbLine.Text.Trim & "' and (datediff('d',adutydate,Format('" & Me.StartDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 and " & _
                                "datediff('d',adutydate,Format('" & Me.EndDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>=0) or (datediff('d',mdutydate,Format('" & Me.StartDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 and " & _
                                "datediff('d',mdutydate,Format('" & Me.EndDate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>=0)"
            Globle.Method.UpdateDataForAccess(str)
            MsgBox("删除成功！", MsgBoxStyle.OkOnly, "成功")
            Call RefreshPlan()
        End If
    End Sub

    Public Sub RefreshPlan()
        Me.DGVManager.Rows.Clear()
        Dim str As String = "select t.dateno,t.num,m.cstimetablename from " & _
                            "(select t.dateno,count(t.dateno) as Num,m.cstimetableid from cs_corresponding t,cs_datetimetable m " & _
                            "where datediff('d',t.dateno,m.dateno)=0 and t.lineid='" & Me.CmbLine.Text.Trim & "' group by t.dateno,m.cstimetableid) t,cs_cstimetableinf m " & _
                            "where(t.cstimetableid = m.cstimetableid) order by t.dateno"
        Dim tab As DataTable = Globle.Method.ReadDataForAccess(str)
        If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
            For Each row As DataRow In tab.Rows
                Me.DGVManager.Rows.Add(CDate(row.Item("dateno")).ToString("yyyy/MM/dd"), row.Item("cstimetablename").ToString, row.Item("num").ToString)
            Next
        End If
    End Sub

    Private Sub Btn_Open_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Open.Click
        Me.StartDate = Me.DTPStart.Value.Date
        Me.EndDate = Me.DTPEnd.Value.Date
        If Me.StartDate > Me.EndDate Then
            MsgBox("日期选择错误,请重新选择日期！", MsgBoxStyle.OkOnly, "成功")
            Exit Sub
        End If
        For i As Integer = 0 To (Me.EndDate.Date - Me.StartDate.Date.AddDays(-1)).Days - 1
            Dim temDateStr As String = Me.StartDate.Date.AddDays(i).ToString("yyyy/MM/dd")
            Dim IFExit As Boolean = False
            For Each row As DataGridViewRow In Me.DGVManager.Rows
                If row.Cells("日期").Value.ToString = temDateStr Then
                    IFExit = True
                End If
            Next
            If IFExit = False Then
                MsgBox("'" & temDateStr & "'不存在计划，请选择正确日期后打开！", MsgBoxStyle.OkOnly, "成功")
                Exit Sub
            End If
        Next
        For i As Integer = 0 To net.Lines.Count - 1
            If net.Lines(i).Name = CmbLine.Text.Trim Then
                Me.Curline = net.Lines(i)
                Exit For
            End If
        Next

        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub

End Class