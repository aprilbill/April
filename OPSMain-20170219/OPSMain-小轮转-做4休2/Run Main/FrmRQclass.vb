Public Class FrmRQclass

    Public arealunzhuan As New Dictionary(Of String, Dictionary(Of Integer, String))   '区域与轮转班制对应关系
    Public lunzhuansets As New Dictionary(Of String, Dictionary(Of Integer, String))   '轮转班制集合

    Public CSTimeTable As Coordination2.CSTimeTable
    Public CurLine As Coordination2.Line
    Public cstimetableNameList() As String              '列车时刻表名称集
    Public AreaYunZhuanS As New List(Of AreaYunZhuan)
    Public Yunzhuanpara As String

    Private Sub FrmRQclass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AreaYunZhuanS = New List(Of AreaYunZhuan)
        Dim Str As String = "select * from cs_areainfo where lineid='" & Me.CurLine.Name & "' order by id"
        Dim tab As System.Data.DataTable = Globle.Method.ReadDataForAccess(Str)
        If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
            For i As Integer = 0 To tab.Rows.Count - 1
                Dim tempArea As New AreaYunZhuan(tab.Rows(i).Item("LineID").ToString, tab.Rows(i).Item("AreaName").ToString)
                tempArea.OnDutyPlaces = tab.Rows(i).Item("OnDutyPlaces").ToString.Split(",")
                tempArea.ForDutySorts = tab.Rows(i).Item("DutySort").ToString.Split(",")
                tempArea.YunZhuanPara = tab.Rows(i).Item("YunZhuanPara").ToString
                AreaYunZhuanS.Add(tempArea)
            Next
        End If

        For Each area As AreaYunZhuan In AreaYunZhuanS
            If area.AreaName <> "主区域" Then
                CheckedListBox1.Items.Add(area.AreaName)
            End If
        Next
    End Sub

    Private Sub DataGridView1_DefaultValuesNeeded(sender As Object, e As DataGridViewRowEventArgs) Handles DataGridView1.DefaultValuesNeeded
        With e.Row
            .Cells("轮转序号").Value = "第" + DataGridView1.Rows.Count.ToString + "天"
            .Cells("班种").Value = "-"
        End With
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.Rows.Count = 0 Then
            MsgBox("轮转班制未设置完整！")
            Exit Sub
        End If

        Dim lunzhuanname As String = InputBox("请输入轮转班制名称", "输入", "第" + (lunzhuansets.Count + 1).ToString + "个轮转班制 ")
        If lunzhuanname = "" Then
            Exit Sub
        End If
        Dim zhibannum As Integer = 0   '值班计数
        Dim xiuxinum As Integer = 0 '休息计数
        Dim lunzhuanset As New Dictionary(Of Integer, String)         '轮转班制
        If DataGridView1.Rows.Count >= 2 Then
            For i As Integer = 0 To DataGridView1.Rows.Count - 2
                If DataGridView1.Rows(i).Cells(0).Value.ToString <> "" Then
                    If DataGridView1.Rows(i).Cells(1).Value = "值班" Then
                        zhibannum += 1
                        lunzhuanset.Add(i + 1, "值班" + "-" + zhibannum.ToString)
                    ElseIf DataGridView1.Rows(i).Cells(1).Value = "休息" Then
                        xiuxinum += 1
                        lunzhuanset.Add(i + 1, "休息" + "-" + xiuxinum.ToString)
                    End If
                End If
            Next
        End If
        lunzhuansets.Add(lunzhuanname, lunzhuanset)
        ListBox1.Items.Add(lunzhuanname)
        ListBox1.SelectedItem = lunzhuanname
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ListBox1.SelectedItem Is Nothing Then
            MsgBox("请选择轮转班制")
            Exit Sub
        ElseIf CheckedListBox1.SelectedItems.Count = 0 Then
            MsgBox("请选择至少一个区域")
            Exit Sub
        End If

        Dim zhibannum As Integer = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 2
            If DataGridView1.Rows(i).Cells(0).Value.ToString <> "" Then
                If DataGridView1.Rows(i).Cells(1).Value.ToString = "值班" Then
                    zhibannum += 1
                End If
            End If
        Next
        Dim areawork As New Dictionary(Of String, Integer)
        Dim checkChongfu As New List(Of String)
        For i As Integer = 0 To Me.cstimetableNameList.Count - 1
            Dim CStimetablename = Me.CurLine.GetCSTimeTableFromName(Me.cstimetableNameList(i)).ID
            If checkChongfu.Contains(CStimetablename) Then
                Continue For
            Else
                checkChongfu.Add(CStimetablename)
            End If


            Dim tmpAreawork As New Dictionary(Of String, Integer)
            CSTimeTable = New Coordination2.CSTimeTable(CStimetablename, Me.CurLine.Name)
            For Each duty As Coordination2.CSDriver In CSTimeTable.CCSDrivers
                For Each belongarea As String In CheckedListBox1.CheckedItems
                    If duty.BelongArea = belongarea Then
                        If tmpAreawork.Keys.Contains(belongarea) = False Then
                            tmpAreawork.Add(belongarea, 0)
                        End If
                        tmpAreawork(belongarea) += 1
                    End If
                Next
            Next

            For Each duty As Coordination2.CSDriver In CSTimeTable.ACSDrivers
                For Each belongarea As String In CheckedListBox1.CheckedItems
                    If duty.BelongArea = belongarea Then
                        If tmpAreawork.Keys.Contains(belongarea) = False Then
                            tmpAreawork.Add(belongarea, 0)
                        End If
                        tmpAreawork(belongarea) += 1
                    End If
                Next
            Next
            For Each duty As Coordination2.CSDriver In CSTimeTable.MCSDrivers
                For Each belongarea As String In CheckedListBox1.CheckedItems
                    If duty.BelongArea = belongarea Then
                        If tmpAreawork.Keys.Contains(belongarea) = False Then
                            tmpAreawork.Add(belongarea, 0)
                        End If
                        tmpAreawork(belongarea) += 1
                    End If
                Next
            Next
            For Each duty As Coordination2.CSDriver In CSTimeTable.NCSDrivers
                For Each belongarea As String In CheckedListBox1.CheckedItems
                    If duty.BelongArea = belongarea Then
                        If tmpAreawork.Keys.Contains(belongarea) = False Then
                            tmpAreawork.Add(belongarea, 0)
                        End If
                        tmpAreawork(belongarea) += 1
                    End If
                Next
            Next

            For Each AreaKey As String In tmpAreawork.Keys
                If areawork.Keys.Contains(AreaKey) = False Then
                    areawork.Add(AreaKey, tmpAreawork(AreaKey))
                End If
                If areawork(AreaKey) < tmpAreawork(AreaKey) Then
                    areawork(AreaKey) = tmpAreawork(AreaKey)
                End If
            Next
        Next
      

        For Each area As String In CheckedListBox1.CheckedItems
            If areawork(area) <> 0 Then
                If arealunzhuan.ContainsKey(area.ToString) Then
                    MsgBox("已添加该区域轮转！无法重复添加！")
                    Continue For
                End If
                If (areawork(area) Mod zhibannum) <> 0 And (zhibannum Mod areawork(area)) <> 0 Then
                    Dim message As String = _
               "当前计划该区域" & area & "任务数为" & areawork(area).ToString & "，计划周期内班数为" & zhibannum.ToString & ",两者不是倍数关系，可能无法正确排班，是否返回修改？"
                    Dim result = MessageBox.Show(message, "提示", _
                                                 MessageBoxButtons.YesNo, _
                                                 MessageBoxIcon.Question)
                    If (result = DialogResult.Yes) Then
                        Continue For
                    End If
                End If
                Dim match As String = "区域:|" + area.ToString + "|->轮转班制：|" + ListBox1.SelectedItem.ToString + "|"
                If ListBox2.Items.Contains(match) = False Then
                    ListBox2.Items.Add(match)
                End If
                If arealunzhuan.ContainsKey(area.ToString) = False Then
                    arealunzhuan.Add(area.ToString, lunzhuansets(ListBox1.SelectedItem.ToString))
                End If
            Else
                MsgBox("该区域没有任务")
            End If
        Next


    End Sub


    '清空目前编制的轮转班制
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        DataGridView1.Rows.Clear()
        DataGridView1.Rows(0).Cells(0).Value = "第1天"
        DataGridView1.Rows(0).Cells(1).Value = "值班"
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()

        For Each item As String In CheckedListBox1.Items
            If arealunzhuan.Keys.Contains(item) = False Then
                Dim Str As String = "update cs_areainfo set yunzhuanPara='" + Yunzhuanpara + "' where lineID ='" + CurLineName + "'and areaname ='" + item.ToString + "'"
                Globle.Method.UpdateDataForAccess(Str)
            End If
        Next

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    '显示选中的轮转班制
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        DataGridView1.Rows.Clear()
        If ListBox1.SelectedItem IsNot Nothing Then
            Dim nowlunzhuan As Dictionary(Of Integer, String) = lunzhuansets(ListBox1.SelectedItem.ToString)
            DataGridView1.Rows.Add(nowlunzhuan.Keys.Count - 1)
            Dim i As Integer = 0
            For Each Day As Integer In nowlunzhuan.Keys
                DataGridView1.Rows(i).Cells(0).Value = "第" + Day.ToString + "天"
                Dim banzhong As String = nowlunzhuan(Day).ToString.Split("-")(0)
                DataGridView1.Rows(i).Cells(1).Value = banzhong
                i += 1
            Next
        End If
    End Sub


    '删除轮转班制
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If ListBox1.SelectedItem.ToString = "" Then
            MsgBox("未选中轮转班制")
        Else
            lunzhuansets.Remove(ListBox1.SelectedItem.ToString)
            ListBox1.Items.Remove(ListBox1.SelectedItem.ToString)
            DataGridView1.Rows.Clear()
            DataGridView1.Rows(0).Cells(0).Value = "第1天"
            DataGridView1.Rows(0).Cells(1).Value = "-"
        End If
    End Sub


    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If ListBox2.SelectedItem.ToString = "" Then
            MsgBox("未选中对应关系")
        Else
            arealunzhuan.Remove(ListBox2.SelectedItem.ToString.Split("|")(1))
            ListBox2.Items.Remove(ListBox2.SelectedItem.ToString)
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click '修改
        If ListBox1.SelectedItem.ToString <> "" And MsgBox("是否修改" & ListBox1.SelectedItem.ToString & "的 轮转方式", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Dim zhibannum As Integer = 0   '值班计数
            Dim xiuxinum As Integer = 0 '休息计数
            Dim lunzhuanset As New Dictionary(Of Integer, String)         '轮转班制
            If DataGridView1.Rows.Count >= 2 Then
                For i As Integer = 0 To DataGridView1.Rows.Count - 2
                    If DataGridView1.Rows(i).Cells(0).Value.ToString <> "" Then
                        If DataGridView1.Rows(i).Cells(1).Value = "值班" Then
                            zhibannum += 1
                            lunzhuanset.Add(i + 1, "值班" + "-" + zhibannum.ToString)
                        ElseIf DataGridView1.Rows(i).Cells(1).Value = "休息" Then
                            xiuxinum += 1
                            lunzhuanset.Add(i + 1, "休息" + "-" + xiuxinum.ToString)
                        End If
                    End If
                Next
            End If
            lunzhuansets(ListBox1.SelectedItem.ToString) = lunzhuanset
        End If
    End Sub
End Class