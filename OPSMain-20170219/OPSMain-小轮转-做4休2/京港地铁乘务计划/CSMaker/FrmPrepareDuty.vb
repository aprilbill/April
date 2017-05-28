Public Class FrmPrepareDuty

    'Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
    '    '将备车信息存到数据库中
    '    sqlstr = "DELETE FROM CS_PREPAREDTRAININF WHERE LINEID='" & CStr(strCurlineID) & "'"
    '    Globle.Method.UpdateDataForAccess(sqlstr)
    '    sqlstr = "SELECT * FROM CS_PREPAREDTRAININF "
    '    tempTable = New Data.DataTable
    '    tempTable = Globle.Method.ReadDataForAccess(sqlstr)
    '    For Each row As DataGridViewRow In Me.DGVBeiche.Rows
    '        tempTable.Rows.Add(CStr(strCurlineID), row.Cells("备车名称").Value.ToString.Trim, row.Cells("备车地点").Value.ToString.Trim, row.Cells("所属区域").Value.ToString.Trim, _
    '                           row.Cells("班种").Value.ToString.Trim, CDate(row.Cells("开始时间").Value.ToString).TimeOfDay.TotalSeconds, CDate(row.Cells("结束时间").Value.ToString).TimeOfDay.TotalSeconds)
    '    Next
    '    Globle.Method.UpdateDataForAccess(sqlstr, tempTable)

    '    '将备班信息存到数据库中
    '    sqlstr = "DELETE FROM cs_prepareddutyinf WHERE LINEID='" & CStr(strCurlineID) & "'"
    '    Globle.Method.UpdateDataForAccess(sqlstr)
    '    sqlstr = "SELECT * FROM cs_prepareddutyinf "
    '    tempTable = New Data.DataTable
    '    tempTable = Globle.Method.ReadDataForAccess(sqlstr)
    '    For Each row As DataGridViewRow In Me.DGVBeiBan.Rows
    '        tempTable.Rows.Add(CStr(strCurlineID), row.Cells("备班名称").Value.ToString.Trim, row.Cells("备班地点").Value.ToString.Trim, row.Cells("所属区域2").Value.ToString.Trim, _
    '                           row.Cells("班种2").Value.ToString.Trim, CDate(row.Cells("开始时间2").Value.ToString).TimeOfDay.TotalSeconds, CDate(row.Cells("结束时间2").Value.ToString).TimeOfDay.TotalSeconds)
    '    Next
    '    Globle.Method.UpdateDataForAccess(sqlstr, tempTable)

    '    Call  Globle.Method.UpdateDataForAccess("delete from cs_preparetime t where t.lineid='" & CStr(strCurlineID) & "'")
    '    Call  Globle.Method.UpdateDataForAccess("insert into cs_preparetime values ('" & CStr(strCurlineID) & "',Format('" & Me.dtPrepareDutyTime.Value.ToString("HH:mm:ss") & "','HH:mm:ss'),Format('" & Me.dtPrepareDutyoffTime.Value.ToString("HH:mm:ss") & "','HH:mm:ss'))")
    '    Me.Close()
    'End Sub

    'Private Sub FrmPrepareDuty_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    sqlstr = "SELECT * FROM cs_preparedtraininf WHERE LINEID='" & CStr(strCurlineID) & "'"
    '    tempTable = New Data.DataTable
    '    tempTable = Globle.Method.ReadDataForAccess(sqlstr)
    '    Me.DGVBeiche.RowCount = tempTable.Rows.Count
    '    For i = 0 To tempTable.Rows.Count - 1
    '        Me.DGVBeiche.Rows(i).Cells("班种").Value = tempTable.Rows(i).Item("dutysort").ToString
    '        Me.DGVBeiche.Rows(i).Cells("备车名称").Value = tempTable.Rows(i).Item("NAME").ToString
    '        Me.DGVBeiche.Rows(i).Cells("备车地点").Value = tempTable.Rows(i).Item("place").ToString
    '        Me.DGVBeiche.Rows(i).Cells("开始时间").Value = CDate(BeTime(tempTable.Rows(i).Item("starttime").ToString)).TimeOfDay
    '        Me.DGVBeiche.Rows(i).Cells("结束时间").Value = CDate(BeTime(tempTable.Rows(i).Item("endtime").ToString)).TimeOfDay
    '        Me.DGVBeiche.Rows(i).Cells("所属区域").Value = tempTable.Rows(i).Item("remark").ToString
    '    Next


    '    sqlstr = "SELECT * FROM cs_prepareddutyinf WHERE LINEID='" & CStr(strCurlineID) & "'"
    '    tempTable = New Data.DataTable
    '    tempTable = Globle.Method.ReadDataForAccess(sqlstr)
    '    Me.DGVBeiBan.RowCount = tempTable.Rows.Count
    '    For i = 0 To tempTable.Rows.Count - 1
    '        Me.DGVBeiBan.Rows(i).Cells("班种2").Value = tempTable.Rows(i).Item("dutysort").ToString
    '        Me.DGVBeiBan.Rows(i).Cells("备班名称").Value = tempTable.Rows(i).Item("NAME").ToString
    '        Me.DGVBeiBan.Rows(i).Cells("备班地点").Value = tempTable.Rows(i).Item("place").ToString
    '        Me.DGVBeiBan.Rows(i).Cells("开始时间2").Value = CDate(BeTime(tempTable.Rows(i).Item("starttime").ToString)).TimeOfDay
    '        Me.DGVBeiBan.Rows(i).Cells("结束时间2").Value = CDate(BeTime(tempTable.Rows(i).Item("endtime").ToString)).TimeOfDay
    '        Me.DGVBeiBan.Rows(i).Cells("所属区域2").Value = tempTable.Rows(i).Item("remark").ToString
    '    Next
    'End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DGVBeiche.Rows.Add("早班", "备车" + CStr(Me.DGVBeiche.Rows.Count + 1), "", "", "", "")
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.DGVBeiche.Rows.Count > 0 Then
            If Me.DGVBeiche.SelectedRows.Count > 0 Then
                Dim i, k As Integer
                k = Me.DGVBeiche.SelectedRows.Count
                For i = 1 To k
                    Me.DGVBeiche.Rows.Remove(Me.DGVBeiche.SelectedRows(0))
                Next
            Else
                Me.DGVBeiche.Rows.RemoveAt(Me.DGVBeiche.CurrentRow.Index)
            End If
        Else
            Exit Sub
        End If
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DGVBeiBan.Rows.Add("早班", "备班" + CStr(Me.DGVBeiBan.Rows.Count + 1), "", "", "", "")
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.DGVBeiBan.Rows.Count > 0 Then
            If Me.DGVBeiBan.SelectedRows.Count > 0 Then
                Dim i, k As Integer
                k = Me.DGVBeiBan.SelectedRows.Count
                For i = 1 To k
                    Me.DGVBeiBan.Rows.Remove(Me.DGVBeiBan.SelectedRows(0))
                Next
            Else
                Me.DGVBeiBan.Rows.RemoveAt(Me.DGVBeiBan.CurrentRow.Index)
            End If
        Else
            Exit Sub
        End If
    End Sub
End Class