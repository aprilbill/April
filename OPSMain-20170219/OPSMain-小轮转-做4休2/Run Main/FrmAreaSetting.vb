Public Class FrmAreaSetting
    Dim LineName As String

    Public Sub New()
        ' 此调用是设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If Me.DGVArea.Rows.Count > 1 Then
            Dim j As Integer = 0
            For j = 0 To Me.DGVArea.Rows.Count - 2
                If Me.DGVArea.Rows(j).Cells("区域名称").Value.ToString.Trim = "主区域" Then
                    Exit For
                End If
            Next
            If j = Me.DGVArea.Rows.Count - 1 Then
                MsgBox("没有包括主区域!请修改后保存")
                Exit Sub
            End If
            Dim str As String = "delete from cs_areainfo where lineid='" & LineName & "'"
            Globle.Method.UpdateDataForAccess(str)
            For i As Integer = 0 To Me.DGVArea.Rows.Count - 2
                str = "insert into cs_areainfo (ID,LineID,AreaName,OndutyPlaces,DutySort,yunzhuanPara) values('" & Me.DGVArea.Rows(i).Cells("序号").Value.ToString.Trim & _
                                                   "','" & Me.DGVArea.Rows(i).Cells("线路名称").Value.ToString.Trim & _
                                                   "','" & Me.DGVArea.Rows(i).Cells("区域名称").Value.ToString.Trim & _
                                                   "','" & Me.DGVArea.Rows(i).Cells("出勤地点").Value.ToString.Trim & _
                                                   "','" & Me.DGVArea.Rows(i).Cells("适用班种").Value.ToString.Trim & _
                                                   "','" & Me.DGVArea.Rows(i).Cells("轮转制度").Value.ToString.Trim & "')"
                Globle.Method.UpdateDataForAccess(str)
            Next
            MsgBox("保存成功！", MsgBoxStyle.OkOnly, "提醒")
        Else
            MsgBox("没有数据,保存未成功！", MsgBoxStyle.OkOnly, "提醒")
        End If
    End Sub

    Private Sub DGVArea_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVArea.CellDoubleClick
        Dim str() As String
        If Me.DGVArea.SelectedCells.Count = 1 AndAlso Me.DGVArea.SelectedCells(0).ColumnIndex = 3 Then
            Dim nf As New FrmAddOffDutyPlace
            nf.Label5.Text = "出勤地点选择"
            '待修改
            For Each LineName As String In Stalist.Keys
                For i As Integer = 0 To Stalist(LineName).Count - 1
                    If nf.ListBoxshiftPlace.Items.Contains(Stalist(LineName)(i)) = False Then
                        nf.ListBoxshiftPlace.Items.Add(Stalist(LineName)(i))
                    End If
                Next
            Next
            If Me.DGVArea.CurrentCell.Value IsNot Nothing AndAlso Me.DGVArea.CurrentCell.Value.ToString <> "" Then
                str = Me.DGVArea.CurrentCell.Value.ToString.Split(",")
                If UBound(str) >= 0 Then
                    For i As Integer = 0 To UBound(str)
                        nf.ListBox1.Items.Add(str(i))
                    Next
                End If
            End If
            If nf.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim strSta As String = ""
                If nf.ListBox1.Items.Count > 0 Then
                    For i As Integer = 0 To nf.ListBox1.Items.Count - 1
                        strSta &= nf.ListBox1.Items(i) & ","
                    Next
                    Me.DGVArea.CurrentCell.Value = strSta.Trim(",")
                End If
            End If
        ElseIf Me.DGVArea.SelectedCells.Count = 1 AndAlso Me.DGVArea.SelectedCells(0).ColumnIndex = 4 Then
            Dim nf As New FrmAddOffDutyPlace
            nf.Label5.Text = "班种选择"
            nf.Btn_AddStation.Text = "添加班种"
            nf.ListBoxshiftPlace.Items.Add("早班")
            nf.ListBoxshiftPlace.Items.Add("白班")
            nf.ListBoxshiftPlace.Items.Add("日勤班")
            nf.ListBoxshiftPlace.Items.Add("夜班")
            If Me.DGVArea.CurrentCell.Value IsNot Nothing AndAlso Me.DGVArea.CurrentCell.Value.ToString <> "" Then
                str = Me.DGVArea.CurrentCell.Value.ToString.Split(",")
                If UBound(str) >= 0 Then
                    For i As Integer = 0 To UBound(str)
                        nf.ListBox1.Items.Add(str(i))
                    Next
                End If
            End If
            If nf.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim strSta As String = ""
                If nf.ListBox1.Items.Count > 0 Then
                    For i As Integer = 0 To nf.ListBox1.Items.Count - 1
                        strSta &= nf.ListBox1.Items(i) & ","
                    Next
                    Me.DGVArea.CurrentCell.Value = strSta.Trim(",")
                End If
            End If
        End If
    End Sub

    Private Sub FrmAreaSetting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ComboBox1.Items.Add(CurLineName)
        Me.ComboBox1.Text = CurLineName
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If LineName IsNot Nothing AndAlso LineName <> Me.ComboBox1.Text.Trim AndAlso MsgBox("切换线路前是否保存数据？不保存会造成数据丢失！", MsgBoxStyle.OkCancel, "提醒") = MsgBoxResult.Ok Then
            Call BtnSave_Click(Nothing, Nothing)
        End If
        LineName = Me.ComboBox1.Text.Trim
        Dim str As String = "select * from cs_areainfo where lineid='" & LineName & "' order by ID"
        Dim temtab As Data.DataTable = Globle.Method.ReadDataForAccess(str)
        Me.DGVArea.Rows.Clear()
        If temtab IsNot Nothing AndAlso temtab.Rows.Count > 0 Then
            For i As Integer = 0 To temtab.Rows.Count - 1
                Me.DGVArea.Rows.Add(temtab.Rows(i).Item("ID").ToString, temtab.Rows(i).Item("LineID").ToString, _
                                    temtab.Rows(i).Item("AreaName").ToString, temtab.Rows(i).Item("OnDutyPlaces").ToString, _
                                    temtab.Rows(i).Item("DutySort").ToString, temtab.Rows(i).Item("YunZhuanPara").ToString)
            Next
        End If
    End Sub

    Private Sub DGVArea_CellBeginEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DGVArea.CellBeginEdit
        Me.DGVArea.Rows(e.RowIndex).Cells(0).Value = e.RowIndex + 1
        Me.DGVArea.Rows(e.RowIndex).Cells(1).Value = LineName
        If Me.DGVArea.Rows(e.RowIndex).Cells(2).Value Is Nothing Then
            Me.DGVArea.Rows(e.RowIndex).Cells(2).Value = ""
        End If
        If Me.DGVArea.Rows(e.RowIndex).Cells(3).Value Is Nothing Then
            Me.DGVArea.Rows(e.RowIndex).Cells(3).Value = ""
        End If
        If Me.DGVArea.Rows(e.RowIndex).Cells(4).Value Is Nothing Then
            Me.DGVArea.Rows(e.RowIndex).Cells(4).Value = ""
        End If
        If Me.DGVArea.Rows(e.RowIndex).Cells(5).Value Is Nothing Then
            Me.DGVArea.Rows(e.RowIndex).Cells(5).Value = ""
        End If
    End Sub


End Class