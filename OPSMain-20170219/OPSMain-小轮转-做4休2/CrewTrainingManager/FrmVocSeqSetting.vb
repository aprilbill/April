Public Class FrmVocSeqSetting

    Public TeamList As New List(Of DriverTeam)
    Public SelectTeamList As New List(Of DriverTeam)

    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub

    Public Sub New(ByVal teams As List(Of DriverTeam))
        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        TeamList = teams
    End Sub
    Public Sub ResetIndex()
        If Me.DGV_DutySeq.Rows.Count > 0 Then
            For i As Integer = 0 To Me.DGV_DutySeq.Rows.Count - 1
                Me.DGV_DutySeq.Rows(i).Cells("休假顺序").Value = i + 1
                Me.DGV_DutySeq.Rows(i).Cells("休假顺序").Style.BackColor = Color.LightGreen
                Me.DGV_DutySeq.Rows(i).Cells("休假顺序").Style.ForeColor = Color.Red
            Next
        End If
    End Sub
    Private Sub FrmVocSeqSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        For Each team As DriverTeam In TeamList
            Me.DGV_Drivers.Rows.Add(team.TeamNo, team.NameStr)
        Next
        For Each team As DriverTeam In SelectTeamList
            Me.DGV_DutySeq.Rows.Add(team.TeamNo, team.NameStr, team.VocDay, team.VocSeq, team.VocType)
            Call ResetIndex()
        Next
    End Sub

    Private Sub Btn_AddOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_AddOne.Click
        If Me.DGV_Drivers.SelectedRows.Count = 1 Then
            Dim teamNum As String = Me.DGV_Drivers.SelectedRows(0).Cells("组号2").Value.ToString
            Dim index As Integer = -1
            For Each row As DataGridViewRow In Me.DGV_DutySeq.Rows
                If row.Cells("组号").Value.ToString = teamNum Then
                    index = row.Index
                End If
            Next
            If index >= 0 Then
                MsgBox("该组司机已被添加！", MsgBoxStyle.OkOnly, "提醒")
            Else
                Dim temTeam As DriverTeam = TeamList.Find(Function(value As DriverTeam)
                                                              Return value.TeamNo = teamNum
                                                          End Function)
                Me.DGV_DutySeq.Rows.Add(temTeam.TeamNo, temTeam.NameStr, 1, 0, "年假")
                Call ResetIndex()
            End If
            If Me.DGV_Drivers.SelectedRows(0).Index < Me.DGV_Drivers.Rows.Count - 1 Then
                Me.DGV_Drivers.Rows(Me.DGV_Drivers.SelectedRows(0).Index + 1).Selected = True
            End If
        Else
            MsgBox("未选择添加司机！", MsgBoxStyle.OkOnly, "提醒")
        End If
    End Sub

    Private Sub Btn_AddAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_AddAll.Click
        Me.DGV_DutySeq.Rows.Clear()
        For Each row As DataGridViewRow In Me.DGV_Drivers.Rows
            Dim teamNum As String = row.Cells("组号2").Value.ToString
            Dim temTeam As DriverTeam = TeamList.Find(Function(value As DriverTeam)
                                                          Return value.TeamNo = teamNum
                                                      End Function)
            Me.DGV_DutySeq.Rows.Add(temTeam.TeamNo, temTeam.NameStr, 1, 0, "年假")
        Next
        Call ResetIndex()
    End Sub

    Private Sub Btn_DelAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_DelAll.Click
        Me.DGV_DutySeq.Rows.Clear()
    End Sub

    Private Sub Btn_DelOne_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_DelOne.Click
        If Me.DGV_DutySeq.SelectedCells.Count = 1 Then
            Me.DGV_DutySeq.Rows.RemoveAt(Me.DGV_DutySeq.SelectedCells(0).RowIndex)
        End If
    End Sub

    Private Sub Btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_OK.Click
        SelectTeamList.Clear()
        For Each row As DataGridViewRow In Me.DGV_DutySeq.Rows
            Dim teamNum As String = row.Cells("组号").Value.ToString
            Dim temTeam As DriverTeam = TeamList.Find(Function(value As DriverTeam)
                                                          Return value.TeamNo = teamNum
                                                      End Function)
            If row.Cells("天数").Value.ToString.Trim = "" OrElse CInt(row.Cells("天数").Value.ToString.Trim) = 0 Then
                MsgBox("休假天数设置不正确，请重新设置！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                Exit Sub
            End If
            temTeam.VocDay = CInt(row.Cells("天数").Value.ToString)
            temTeam.VocSeq = CInt(row.Cells("休假顺序").Value.ToString)
            temTeam.VocType = row.Cells("类型").Value.ToString
            SelectTeamList.Add(temTeam)
        Next
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Btn_Cancle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancle.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DGV_Drivers_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGV_Drivers.CellDoubleClick
        Call Btn_AddOne_Click(Nothing, Nothing)
    End Sub
End Class