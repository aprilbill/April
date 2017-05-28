Public Class FrmUpSchedule
    Dim LunzhuanDT As New DataTable
    Dim DutyPlan As New DataTable
    Dim timeTable As New List(Of String)
    Dim driverDT As New DataTable
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.Checked = True Then
            If MsgBox("确认强制覆盖涉及的乘务计划，如果正在使用，将会受到影响!" & vbCrLf & "点Cancel重新选择", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                Exit Sub
            End If
        End If
        'If DateTimePicker1.Value < Now Then
        '    MsgBox("上传日期必须大于今天！")
        '    Exit Sub
        'End If
        If DateTimePicker1.Value > DateTimePicker2.Value Then
            MsgBox("日期选择错误！")
            Exit Sub
        End If
        Dim str As String = ""

        If CheckDutyfromAccess() = False Then
            MsgBox("本地数据库中尚未有到" & DateTimePicker2.Value.AddDays(-1).ToString("yyyy/MM/dd") & "的轮转计划")
            Exit Sub
        End If
        If CheckDutyfromNet() Then
            If MsgBox("云端已存在该轮转计划，是否覆盖?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                '删除datetime
                str = "delete from cs_datetimetable where dateno>=to_date('" & DateTimePicker1.Value.ToString("yyyy/MM/dd") & "','yyyy/MM/dd') and dateno<=to_date('" & DateTimePicker2.Value.ToString("yyyy/MM/dd") & "','yyyy/MM/dd') and lineid='" & CurLineName & "'"
                Try
                    Globle.Method.UpdateDataForOracle(str)
                    ListBox1.Items.Add("清空cs_datetimetable成功！从" & DateTimePicker1.Value.ToString("yyyy/MM/dd") & "到" & DateTimePicker2.Value.ToString("yyyy/MM/dd"))
                Catch ex As Exception
                    ListBox1.Items.Add("清空cs_datetimetable失败！从" & DateTimePicker1.Value.ToString("yyyy/MM/dd") & "到" & DateTimePicker2.Value.ToString("yyyy/MM/dd"))
                    Exit Sub
                End Try

                str = "delete from cs_dutyonoffplan_" & CurLineName.Substring(0, CurLineName.Length - 2) & " where dutydate>=to_date('" & DateTimePicker1.Value.ToString("yyyy/MM/dd") & "','yyyy/MM/dd') and dutydate<=to_date('" & DateTimePicker2.Value.ToString("yyyy/MM/dd") & "','yyyy/MM/dd') and lineid='" & CurLineName & "'"
                Try
                    Globle.Method.UpdateDataForOracle(str)
                    ListBox1.Items.Add("清空cs_dutyonoffplan成功！从" & DateTimePicker1.Value.ToString("yyyy/MM/dd") & "到" & DateTimePicker2.Value.ToString("yyyy/MM/dd"))
                Catch ex As Exception
                    ListBox1.Items.Add("清空cs_dutyonoffplan失败！从" & DateTimePicker1.Value.ToString("yyyy/MM/dd") & "到" & DateTimePicker2.Value.ToString("yyyy/MM/dd"))
                    Exit Sub
                End Try
            Else
                Exit Sub

            End If
        End If
        For i As Integer = 0 To LunzhuanDT.Rows.Count - 1
            If timeTable.Contains(LunzhuanDT.Rows(i).Item("cstimetableid").ToString) = False Then
                timeTable.Add(LunzhuanDT.Rows(i).Item("cstimetableid").ToString)
            End If
            str = "insert into cs_datetimetable values('" & LunzhuanDT.Rows(i).Item("lineid").ToString & "',to_date('" & CDate(LunzhuanDT.Rows(i).Item("dateno").ToString).ToString("yyyy/MM/dd") & "','yyyy/MM/dd'),'" & LunzhuanDT.Rows(i).Item("cstimetableid").ToString & "')"
            Try
                Globle.Method.UpdateDataForOracle(str)
                ListBox1.Items.Add("添加cs_datetimetable成功！日期" & CDate(LunzhuanDT.Rows(i).Item("dateno").ToString).ToString("yyyy/MM/dd"))
            Catch ex As Exception
                ListBox1.Items.Add("添加cs_datetimetable失败！日期" & CDate(LunzhuanDT.Rows(i).Item("dateno").ToString).ToString("yyyy/MM/dd"))
                Exit Sub
            End Try
        Next

        For i As Integer = 0 To timeTable.Count - 1
            UpSchedule(timeTable(i))
        Next

        str = "select t.*,n.cstimetableid as realtimetable from cs_corresponding t, cs_datetimetable n where t.lineid=n.lineid and t.lineid='" & CurLineName & "' and datediff('d',t.dateno,Format('" & DateTimePicker1.Value.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 and datediff('d',t.dateno,Format('" & DateTimePicker2.Value.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>=0 and datediff('d',t.dateno,n.dateno)=0 order by t.dateno"
        Dim tmpDT As New DataTable
        Try
            tmpDT = Globle.Method.ReadDataForAccess(str)
        Catch ex As Exception
            MsgBox("查询本地数据库cs_corresponding失败！")
            ListBox1.Items.Add("查询本地数据库cs_corresponding失败")
        End Try
        If IsNothing(tmpDT) = False AndAlso tmpDT.Rows.Count > 0 Then
            For i As Integer = 0 To tmpDT.Rows.Count - 1
                If tmpDT.Rows(i).Item("dutysort").ToString.Trim <> "休息" And tmpDT.Rows(i).Item("dutysort").ToString.Trim <> "" Then
                    UpDutyPlan(tmpDT.Rows(i).Item("rdriverno").ToString.Trim, tmpDT.Rows(i).Item("realtimetable").ToString.Trim, tmpDT.Rows(i).Item("driverno").ToString.Trim, tmpDT.Rows(i).Item("dutysort").ToString.Trim, (i + 1).ToString, CDate(tmpDT.Rows(i).Item("dateno").ToString.Trim))
                End If
            Next
        End If
        MsgBox("导入完毕！")
    End Sub
    Public Function CheckDutyfromNet() As Boolean
        Dim str As String = "select * from cs_datetimetable where dateno>=to_date('" & DateTimePicker1.Value.ToString("yyyy/MM/dd") & "','yyyy/MM/dd') and dateno<=to_date('" & DateTimePicker2.Value.ToString("yyyy/MM/dd") & "','yyyy/MM/dd') and lineid='" & CurLineName & "'"
        Dim tempDT As New DataTable
        Try
            tempDT = Globle.Method.ReadDataForOracle(str)
            If IsNothing(tempDT) = False AndAlso tempDT.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox("查询云端数据库cs_datetimetable失败！")
            ListBox1.Items.Add("查询云端数据库cs_datetimetable失败")
            Return False
        End Try
    End Function

    Public Function CheckDutyfromAccess() As Boolean
        Dim str As String = "select * from cs_datetimetable where datediff('d',dateno,Format('" & DateTimePicker1.Value.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))<=0 and datediff('d',dateno,Format('" & DateTimePicker2.Value.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'))>=0 and lineid='" & CurLineName & "' order by dateno"
        Try
            LunzhuanDT = Globle.Method.ReadDataForAccess(str)
            If IsNothing(LunzhuanDT) = False AndAlso LunzhuanDT.Rows.Count = CInt((DateTimePicker2.Value - DateTimePicker1.Value).TotalDays + 1) Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox("查询本地数据库cs_datetimetable失败！")
            ListBox1.Items.Add("查询本地数据库cs_datetimetable失败")
            Return False
        End Try
    End Function

    Private Sub FrmUpSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DateTimePicker1.Value = Now.AddDays(1)
        DateTimePicker2.Value = DateTimePicker1.Value
        Dim str As String = "select * from cs_driverinf where lineid='" & CurLineName & "'"
        Try
            driverDT = Globle.Method.ReadDataForAccess(str)
        Catch ex As Exception
            MsgBox("查询本地人员信息失败！")
        End Try
        MsgBox("请保持网络连接，并且本地的人员信息已经和云端同步！")
    End Sub

    Public Sub UpDutyPlan(ByVal rdriverno As String, ByVal cstimetableid As String, ByVal dutysort As String, ByVal dutytype As String, ByVal id As String, ByVal dutydate As Date)
        Dim beclass As String = ""
        Dim drivername As String = ""
        For i As Integer = 0 To driverDT.Rows.Count - 1
            If driverDT.Rows(i).Item("rdriverno").ToString = rdriverno Then
                beclass = driverDT.Rows(i).Item("beclass").ToString
                drivername = driverDT.Rows(i).Item("drivername").ToString
                Exit For
            End If
        Next
        Dim str As String = "select * from cs_dutyonoffdetail where cstimetableid='" & cstimetableid & "' and dutysort='" & dutysort & "' and dutytype='" & dutytype & "' and lineid='" & CurLineName & "'"
        Dim tmpDT As New DataTable
        Try
            tmpDT = Globle.Method.ReadDataForOracle(str)
        Catch ex As Exception
            ListBox1.Items.Add("查询cs_dutyonoffdetail失败！工号：" & rdriverno)
            Exit Sub
        End Try
        If IsNothing(tmpDT) = False AndAlso tmpDT.Rows.Count > 0 Then
            Dim tmpstr As String = "insert into cs_dutyonoffplan_" & CurLineName.Substring(0, CurLineName.Length - 2) & "(lineid,rdriverno,drivername,dutyonseq,dutyontime,vehicleno,ifdeadhead,dutydate,beclass,dutyonplace,dutyofftime,dutysort,dutytype) values('" & CurLineName & "','" & rdriverno & "','" & drivername & "','" & id.ToString & "','" & tmpDT.Rows(0).Item("dutyontime").ToString.Trim & "','" & tmpDT.Rows(0).Item("vehicleno").ToString.Trim & "',0,to_date('" & dutydate.ToString("yyyy/MM/dd") & "','yyyy/MM/dd'),'" & beclass & "','" & tmpDT.Rows(0).Item("dutyonplace").ToString.Trim & "','" & tmpDT.Rows(0).Item("dutyofftime").ToString.Trim & "','" & dutysort & "','" & dutytype & "')"
            Try
                Globle.Method.UpdateDataForOracle(tmpstr)
                ListBox1.Items.Add("添加cs_dutyonoffplan成功！工号：" & rdriverno)
            Catch ex As Exception
                ListBox1.Items.Add("添加cs_dutyonoffplan失败！工号：" & rdriverno)
            End Try
        End If
    End Sub
    Public Sub UpSchedule(ByVal ctimetableid As String)
        Dim str As String = ""
        str = "select * from cs_cstimetableinf where cstimetableid='" & ctimetableid & "' and lineid='" & CurLineName & "'"
        Dim tmpDT1 As New DataTable
        Try
            tmpDT1 = Globle.Method.ReadDataForOracle(str)
            If IsNothing(tmpDT1) AndAlso tmpDT1.Rows.Count > 0 Then
                If CheckBox1.Checked = False Then
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            MsgBox("查询云端数据库cs_cstimetableinf失败！")
            ListBox1.Items.Add("查询云端数据库cs_cstimetableinf失败")
        End Try

        str = "delete from cs_cstimetableinf where cstimetableid='" & ctimetableid & "' and lineid='" & CurLineName & "'"
        Try
            Globle.Method.UpdateDataForOracle(str)
        Catch ex As Exception
            MsgBox("删除cs_cstimetableinf失败！")
            ListBox1.Items.Add("删除云端数据库cs_cstimetableinf失败")
        End Try

        str = "delete from cs_crewschedule where cstimetableid='" & ctimetableid & "' and lineid='" & CurLineName & "'"
        Try
            Globle.Method.UpdateDataForOracle(str)
        Catch ex As Exception
            MsgBox("删除cs_crewschedule失败！")
            ListBox1.Items.Add("删除云端数据库cs_crewschedule失败")
        End Try

        str = "delete from cs_dutyonoffdetail where cstimetableid='" & ctimetableid & "' and lineid='" & CurLineName & "'"
        Try
            Globle.Method.UpdateDataForOracle(str)
        Catch ex As Exception
            MsgBox("删除cs_dutyonoffdetail失败！")
            ListBox1.Items.Add("删除云端数据库cs_dutyonoffdetail失败")
        End Try

        Dim tmpDT As New DataTable
        str = "select * from cs_cstimetableinf where cstimetableid='" & ctimetableid & "' and lineid='" & CurLineName & "'"
        Try
            tmpDT = Globle.Method.ReadDataForAccess(str)
        Catch ex As Exception
            MsgBox("查询本地数据库cs_cstimetableinf失败！")
            ListBox1.Items.Add("查询本地数据库cs_cstimetableinf失败")
        End Try
        If IsNothing(tmpDT) = False AndAlso tmpDT.Rows.Count > 0 Then
            For i As Integer = 0 To tmpDT.Rows.Count - 1
                Dim tmpstr As String = "insert into cs_cstimetableinf values('" & CurLineName & "','" & tmpDT.Rows(i).Item("cstimetablename").ToString.Trim & "','" & tmpDT.Rows(i).Item("cstimetableid").ToString.Trim & "','" & tmpDT.Rows(i).Item("createtime").ToString.Trim & "','" & tmpDT.Rows(i).Item("modifytime").ToString.Trim & "','" & tmpDT.Rows(i).Item("remark").ToString.Trim & "','" & tmpDT.Rows(i).Item("traindiagramid").ToString.Trim & "')"
                Try
                    Globle.Method.UpdateDataForOracle(tmpstr)
                    ListBox1.Items.Add("添加cs_cstimetableinf成功！ID " & tmpDT.Rows(i).Item("cstimetablename").ToString.Trim)
                Catch ex As Exception
                    ListBox1.Items.Add("添加cs_cstimetableinf失败！ID " & tmpDT.Rows(i).Item("cstimetablename").ToString.Trim)
                    Exit For
                End Try
            Next
        End If

        str = "select * from cs_crewschedule where cstimetableid='" & ctimetableid & "' and lineid='" & CurLineName & "'"
        Try
            tmpDT = Globle.Method.ReadDataForAccess(str)
        Catch ex As Exception
            MsgBox("查询本地数据库cs_crewschedule失败！")
            ListBox1.Items.Add("查询本地数据库cs_crewschedule失败")
        End Try
        If IsNothing(tmpDT) = False AndAlso tmpDT.Rows.Count > 0 Then
            For i As Integer = 0 To tmpDT.Rows.Count - 1
                Dim tmpstr As String = "insert into cs_crewschedule values('" & CurLineName & "','" & tmpDT.Rows(i).Item("cstimetableid").ToString.Trim & "','" & tmpDT.Rows(i).Item("id").ToString.Trim & "','" & tmpDT.Rows(i).Item("driverno").ToString.Trim & "','" & tmpDT.Rows(i).Item("dateno").ToString.Trim & "','" & tmpDT.Rows(i).Item("dutysort").ToString.Trim & "','" & tmpDT.Rows(i).Item("trainno").ToString.Trim & "','" & _
tmpDT.Rows(i).Item("starttime").ToString.Trim & "','" & tmpDT.Rows(i).Item("startstaname").ToString.Trim & "','" & tmpDT.Rows(i).Item("endtime").ToString.Trim & "','" & tmpDT.Rows(i).Item("endstaname").ToString.Trim & "','" & tmpDT.Rows(i).Item("trainid").ToString.Trim & "','" & tmpDT.Rows(i).Item("path1").ToString.Trim & "','" & tmpDT.Rows(i).Item("path2").ToString.Trim & "','" & tmpDT.Rows(i).Item("chediid").ToString.Trim & "','" & _
tmpDT.Rows(i).Item("upordown").ToString.Trim & "','" & tmpDT.Rows(i).Item("zftime").ToString.Trim & "','" & tmpDT.Rows(i).Item("staseq1").ToString.Trim & "','" & tmpDT.Rows(i).Item("staseq2").ToString.Trim & "','" & tmpDT.Rows(i).Item("distance").ToString.Trim & "','" & tmpDT.Rows(i).Item("vehicleno").ToString.Trim & "')"
                Try
                    Globle.Method.UpdateDataForOracle(tmpstr)
                    ListBox1.Items.Add("添加cs_crewschedule成功！位置号：" & tmpDT.Rows(i).Item("driverno").ToString.Trim)
                Catch ex As Exception
                    ListBox1.Items.Add("添加cs_crewschedule失败！位置号：" & tmpDT.Rows(i).Item("driverno").ToString.Trim)
                    Exit For
                End Try
            Next
        End If

        str = "select * from cs_dutyonoffdetail where cstimetableid='" & ctimetableid & "' and lineid='" & CurLineName & "'"
        Try
            tmpDT = Globle.Method.ReadDataForAccess(str)
        Catch ex As Exception
            MsgBox("查询本地数据库cs_dutyonoffdetail失败！")
            ListBox1.Items.Add("查询本地数据库cs_dutyonoffdetail失败")
        End Try
        If IsNothing(tmpDT) = False AndAlso tmpDT.Rows.Count > 0 Then
            For i As Integer = 0 To tmpDT.Rows.Count - 1
                Dim tmpstr As String = "insert into cs_dutyonoffdetail values('" & tmpDT.Rows(i).Item("cstimetableid").ToString.Trim & "','" & tmpDT.Rows(i).Item("dutysort").ToString.Trim & "','" & tmpDT.Rows(i).Item("dutytype").ToString.Trim & "','" & tmpDT.Rows(i).Item("dutyontime").ToString.Trim & "','" & tmpDT.Rows(i).Item("dutyonplace").ToString.Trim & "','" & tmpDT.Rows(i).Item("dutyofftime").ToString.Trim & "','" & _
tmpDT.Rows(i).Item("dutyoffplace").ToString.Trim & "','" & tmpDT.Rows(i).Item("lineid").ToString.Trim & "','" & tmpDT.Rows(i).Item("vehicleno").ToString.Trim & "')"
                Try
                    Globle.Method.UpdateDataForOracle(tmpstr)
                    ListBox1.Items.Add("添加cs_dutyonoffdetail成功！位置号：" & tmpDT.Rows(i).Item("dutysort").ToString.Trim)
                Catch ex As Exception
                    ListBox1.Items.Add("添加cs_dutyonoffdetail失败！位置号：" & tmpDT.Rows(i).Item("dutysort").ToString.Trim)
                    Exit For
                End Try
            Next
        End If
    End Sub
End Class