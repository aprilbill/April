Public Class frmDayDinnerInfo
    Public DayDriverNum As Integer
    Public ChangeStaPeopleNo As New Dictionary(Of String, Integer)

    Private Sub frmDayDiverNUmInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TextBox1.Text = NChediNum
        For i As Integer = 1 To UBound(sysDinnerStation)
            If sysDinnerStation(i).dutySort = "白班" AndAlso sysDinnerStation(i).dinnerType = "午餐" Then
                Me.DataGridView1.Rows.Add(sysDinnerStation(i).Routing, sysDinnerStation(i).DinnerStationName, sysDinnerStation(i).NeedDinnerDriverNum, sysDinnerStation(i).NeedDinnerDriverNum)
                Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Tag = sysDinnerStation(i)
            End If
        Next
        For i As Integer = 1 To ChangeStationList.Count - 1
            If ChangeStationList(i).TimeSpanList.StartTime <> ChangeStationList(i).TimeSpanList.EndTime AndAlso (ChangeStationList(i).TimeSpanList.StartTime > sysDinnerStation(i).dinnerEndTime Or ChangeStationList(i).TimeSpanList.EndTime < sysDinnerStation(i).dinnerStartTime) Then
                Continue For
            End If
            If ChangeStaPeopleNo.Keys.Contains(ChangeStationList(i).Name) = False Then
                ChangeStaPeopleNo.Add(ChangeStationList(i).Name, 0)
            End If
            If ChangeStaPeopleNo(ChangeStationList(i).Name) < ChangeStationList(i).FollowNo Then
                ChangeStaPeopleNo(ChangeStationList(i).Name) = ChangeStationList(i).FollowNo
            End If
        Next
       
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        For Each row As DataGridViewRow In Me.DataGridView1.Rows
            For i As Integer = 1 To UBound(sysDinnerStation)
                If sysDinnerStation(i).dutySort = "白班" AndAlso sysDinnerStation(i).dinnerType = "午餐" Then
                    If sysDinnerStation(i).DinnerStationName = row.Cells("用餐地点").Value.ToString _
                        AndAlso row.Cells("实际替饭人数").Value.ToString <> "" _
                        AndAlso row.Cells("新增替饭人数").Value.ToString <> "" Then
                        sysDinnerStation(i).RealDinnerDriverNum = row.Cells("实际替饭人数").Value.ToString
                        sysDinnerStation(i).AddNewDinnerDriverNum = row.Cells("新增替饭人数").Value.ToString
                        sysDinnerStation(i).IfOnlyDinner = row.Cells("是否只替饭").Value
                    Else
                        MsgBox("请先计算实际人数")
                        Exit Sub
                    End If
                End If
            Next
        Next
        DayDriverNum = Me.txtDayRealDriverNum.Text.ToString '- Me.TextBoxCuntifanDriverNum.Text.ToString
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        For Each row As DataGridViewRow In Me.DataGridView1.Rows
            If IsNothing(row.Cells("新增替饭人数").Value) = True Or IsNothing(row.Cells("实际替饭人数").Value) = True Then
                Exit Sub
            End If
        Next

        Dim DayNeedDriverNum As Integer = Me.TextBox1.Text.ToString
        Dim CunTifanDriverNum As Integer = 0

        For Each sta As String In ChangeStaPeopleNo.Keys
            DayNeedDriverNum += ChangeStaPeopleNo(sta)
        Next

        For Each row As DataGridViewRow In Me.DataGridView1.Rows
            Dim index As Integer = -1
            index = ChangeStationList.FindIndex(Function(value As ChangeStation)
                                                    Return value.Name = row.Cells("用餐地点").Value.ToString
                                                End Function)
            If index > -1 Then
                DayNeedDriverNum += row.Cells("新增替饭人数").Value.ToString
            Else
                DayNeedDriverNum += row.Cells("实际替饭人数").Value.ToString
            End If
            If row.Cells("是否只替饭").Value = True Then
                CunTifanDriverNum = CunTifanDriverNum + row.Cells("新增替饭人数").Value.ToString
            End If
        Next
        Me.TextBoxCuntifanDriverNum.Text = CunTifanDriverNum
        Me.txtDayNeedDriverNum.Text = DayNeedDriverNum
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If Me.DataGridView1.CurrentRow.Index > -1 Then
            For Each r As DataGridViewRow In Me.DataGridView1.Rows
                r.Cells("新增替饭人数").Value = r.Cells("实际替饭人数").Value
                For Each sta As String In ChangeStaPeopleNo.Keys
                    If sta = r.Cells("用餐地点").Value.ToString Then
                        r.Cells("新增替饭人数").Value = r.Cells("新增替饭人数").Value - ChangeStaPeopleNo(sta)
                    End If
                Next
            Next
        End If
    End Sub

   
End Class