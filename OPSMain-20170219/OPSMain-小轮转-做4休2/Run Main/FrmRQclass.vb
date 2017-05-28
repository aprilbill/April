Public Class FrmRQclass

    Public arealunzhuan As New Dictionary(Of String, Dictionary(Of Integer, String))   '区域与轮转班制对应关系
    Public lunzhuansets As New Dictionary(Of String, Dictionary(Of Integer, String))   '轮转班制集合
    Public lunzhuanset As New Dictionary(Of Integer, String)         '轮转班制
    Public CSTimeTable As Coordination2.CSTimeTable
    Public CurLine As Coordination2.Line
    Public cstimetableNameList() As String              '列车时刻表名称集
    Public AreaYunZhuanS As New List(Of AreaYunZhuan)
    Public areawork As New Dictionary(Of String, Integer)   '各个区域的任务数
    Public Yunzhuanpara As String


    Private Sub FrmRQclass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.Rows(0).Cells(1).Value = "值班"

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
                areawork.Add(area.AreaName, 0)
            End If
        Next

        lunzhuanset.Clear()
        lunzhuanset.Add(1, "值班-1")
        lunzhuanset.Add(2, "值班-2")
        lunzhuanset.Add(3, "值班-3")
        lunzhuanset.Add(4, "值班-4")
        lunzhuanset.Add(5, "休息-1")
        lunzhuanset.Add(6, "值班-5")
        lunzhuanset.Add(7, "值班-6")
        lunzhuanset.Add(8, "休息-2")
        lunzhuansets.Add("预设：作4休1作2休1", lunzhuanset)
        ListBox1.Items.Add("预设：作4休1作2休1")

    End Sub

    Private Sub DataGridView1_DefaultValuesNeeded(sender As Object, e As DataGridViewRowEventArgs) Handles DataGridView1.DefaultValuesNeeded
        With e.Row
            .Cells("轮转序号").Value = "第" + DataGridView1.Rows.Count.ToString + "天"
        End With
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If DataGridView1.Rows.Count = 0 Then
            MsgBox("轮转班制未设置完整！")
            Exit Sub
        End If

        Dim lunzhuanname As String
        Dim lunzhuannum As Integer = lunzhuansets.Count
        lunzhuanname = InputBox("请输入轮转班制名称", "输入", "第" + (lunzhuannum + 1).ToString + "个轮转班制 ")
        If lunzhuanname = "" Then
            Exit Sub
        End If



        Dim x As Integer = 0
        If DataGridView1.Rows.Count >= 2 Then
            x = DataGridView1.Rows.Count
        Else
            x = 2
        End If

        Dim zhibannum As Integer = 0   '值班计数
        Dim xiuxinum As Integer = 0 '休息计数
        lunzhuanset.Clear()
        For i As Integer = 0 To x - 2
            If DataGridView1.Rows(i).Cells(0).Value.ToString <> "" Then
                If DataGridView1.Rows(i).Cells(1).Value = "值班" Then
                    zhibannum = +1
                    lunzhuanset.Add(i + 1, "值班" + zhibannum.ToString)
                ElseIf DataGridView1.Rows(i).Cells(1).Value = "休息" Then
                    xiuxinum += 1
                    lunzhuanset.Add(i + 1, "休息" + xiuxinum.ToString)
                End If
            End If
        Next
       
        lunzhuansets.Add(lunzhuanname, lunzhuanset)
        ListBox1.Items.Add(lunzhuanname)
        ListBox1.SelectedItem = lunzhuanname


        ' CSTimeTable = New Coordination2.CSTimeTable(Me.CurLine.GetCSTimeTableFromName(Me.cstimetableNameList(0)).ID, Me.CurLine.Name)
        ' Dim riqinban As Integer = CSTimeTable.CCSDrivers.Count
        ' If (riqinban Mod zhibannum) <> 0 And (zhibannum Mod riqinban) <> 0 Then
        '     Dim message As String = _
        '"当前计划日勤班任务数为" & riqinban.ToString & "，计划周期内日勤班数为" & zhibannum.ToString & ",两者不是倍数关系，可能无法正确排班，是否返回修改？"
        '     Dim result = MessageBox.Show(message, "提示", _
        '                                  MessageBoxButtons.YesNo, _
        '                                  MessageBoxIcon.Question)
        '     If (result = DialogResult.No) Then
        '         Me.DialogResult = Windows.Forms.DialogResult.OK
        '         Me.Close()
        '     Else
        '         DataGridView1.Rows.Clear()
        '         DataGridView1.Rows(0).Cells(0).Value = "第一天"
        '         DataGridView1.Rows(0).Cells(1).Value = "值班"
        '     End If
        'End If




    End Sub

   

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ListBox1.SelectedItem Is Nothing Then
            MsgBox("请选择轮转班制")
            Exit Sub
        ElseIf CheckedListBox1.SelectedItems.Count = 0 Then
            MsgBox("请选择至少一个区域")
            Exit Sub
        End If



        Dim x As Integer = 0
        If DataGridView1.Rows.Count >= 2 Then
            x = DataGridView1.Rows.Count
        Else
            x = 2
        End If

        Dim zhibannum As Integer = 0
        For i As Integer = 0 To x - 2
            If DataGridView1.Rows(i).Cells(0).Value.ToString <> "" Then
                If DataGridView1.Rows(i).Cells(1).Value.ToString = "值班" Then
                    zhibannum += 1
                End If
            End If
        Next

        CSTimeTable = New Coordination2.CSTimeTable(Me.CurLine.GetCSTimeTableFromName(Me.cstimetableNameList(0)).ID, Me.CurLine.Name)


        For Each duty As Coordination2.CSDriver In CSTimeTable.CCSDrivers
            For Each belongarea As String In CheckedListBox1.SelectedItems
                If duty.BelongArea = belongarea Then
                    areawork(belongarea) += 1
                End If
            Next
        Next

        For Each duty As Coordination2.CSDriver In CSTimeTable.ACSDrivers
            For Each belongarea As String In CheckedListBox1.SelectedItems
                If duty.BelongArea = belongarea Then
                    areawork(belongarea) += 1
                End If
            Next
        Next
        For Each duty As Coordination2.CSDriver In CSTimeTable.MCSDrivers
            For Each belongarea As String In CheckedListBox1.SelectedItems
                If duty.BelongArea = belongarea Then
                    areawork(belongarea) += 1
                End If
            Next
        Next
        For Each duty As Coordination2.CSDriver In CSTimeTable.NCSDrivers
            For Each belongarea As String In CheckedListBox1.SelectedItems
                If duty.BelongArea = belongarea Then
                    areawork(belongarea) += 1
                End If
            Next
        Next

        For Each area As String In CheckedListBox1.SelectedItems
            If areawork(area) <> 0 Then
                If (areawork(area) Mod zhibannum) <> 0 And (zhibannum Mod areawork(area)) <> 0 Then
                    Dim message As String = _
               "当前计划该区域任务数为" & areawork(area).ToString & "，计划周期内班数为" & zhibannum.ToString & ",两者不是倍数关系，可能无法正确排班，是否返回修改？"
                    Dim result = MessageBox.Show(message, "提示", _
                                                 MessageBoxButtons.YesNo, _
                                                 MessageBoxIcon.Question)
                    If (result = DialogResult.Yes) Then
                        Exit Sub
                    End If
                End If

                For Each item As String In CheckedListBox1.SelectedItems
                    Dim match As String = "区域:|" + item.ToString + "|->轮转班制：|" + ListBox1.SelectedItem.ToString + "|"
                    ListBox2.Items.Add(match)
                    arealunzhuan.Add(item.ToString, lunzhuansets(ListBox1.SelectedItem.ToString))
                Next
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
            DataGridView1.Rows(0).Cells(1).Value = "值班"
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
End Class