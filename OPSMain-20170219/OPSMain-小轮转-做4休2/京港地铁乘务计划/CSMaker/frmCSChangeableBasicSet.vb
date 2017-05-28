Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing

Public Class frmCSChangeableBasicSet
    Public flag As Integer = 0 '用于指示是经过“退出”0退出的，还是“确定退出的”1，若为后者需要刷新底图

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim sqlstr As String = ""
        Try
            ConDriveTime = Me.DateTimePicker4.Value.TimeOfDay.TotalSeconds  '连续驾车时间
            PrepareTrainTime = Me.DateTimePicker3.Value.TimeOfDay.TotalSeconds
            PrepareDutyTime = Me.DateTimePicker2.Value.TimeOfDay.TotalSeconds
            PrepareDutyOffTime = Me.DateTimePicker1.Value.TimeOfDay.TotalSeconds
            CS_MorningMaxLength = CDec(Me.TxTMorningLength.Text.Trim)
            CS_DayMaxLength = CDec(Me.TxTDayLength.Text.Trim)
            CS_CDayMaxLength = CDec(Me.TXTCDayLength.Text.Trim)
            CS_NightMaxLength = CDec(Me.TxTNightLength.Text.Trim)
            CS_MorningMinLength = CDec(Me.TxTMinMorningLength.Text.Trim)
            CS_DayMinLength = CDec(Me.TxTMinDayLength.Text.Trim)
            CS_CDayMinLength = CDec(Me.TXTMinCDayLength.Text.Trim)
            CS_NightMinLength = CDec(Me.TxTMinNightLength.Text.Trim)
            CSTrainsAndDrivers.PreParedTrainDrivers.Clear()
            CSTrainsAndDrivers.PreParedDutyDrivers.Clear()

            For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
                dri = Nothing
            Next
            For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
                dri = Nothing
            Next
            CSTrainsAndDrivers.NegtiveCorCSPlans.Clear()
            CSTrainsAndDrivers.PostiveCorCSPlans.Clear()

            If DGVBeiche.Rows.Count > 0 Then
                For Each row As DataGridViewRow In DGVBeiche.Rows
                    Dim tempDri As New CSDriver(0)
                    tempDri.CSdriverNo = row.Cells(1).Value.ToString.Trim
                    tempDri.OutPutCSdriverNo = row.Cells(1).Value.ToString.Trim
                    tempDri.DutySort = row.Cells(0).Value.ToString.Trim
                    tempDri.BelongArea = row.Cells(5).Value.ToString.Trim
                    tempDri.IsPrepareDri = True
                    Dim tempTrain As New CSLinkTrain()
                    tempTrain.StartStaName = row.Cells(2).Value.ToString.Trim
                    tempTrain.StartTime = CDate(row.Cells(3).Value.ToString.Trim).TimeOfDay.TotalSeconds
                    tempTrain.EndStaName = row.Cells(2).Value.ToString.Trim
                    tempTrain.EndTime = CDate(row.Cells(4).Value.ToString.Trim).TimeOfDay.TotalSeconds
                    tempDri.AddTrain(tempTrain)
                    CSTrainsAndDrivers.PreParedTrainDrivers.Add(tempDri)
                Next
            End If
            If DGVBeiBan.Rows.Count > 0 Then
                For Each row As DataGridViewRow In DGVBeiBan.Rows
                    Dim tempDri As New CSDriver(0)
                    tempDri.CSdriverNo = row.Cells(1).Value.ToString.Trim
                    tempDri.OutPutCSdriverNo = row.Cells(1).Value.ToString.Trim
                    tempDri.DutySort = row.Cells(0).Value.ToString.Trim
                    tempDri.BelongArea = row.Cells(5).Value.ToString.Trim
                    tempDri.IsPrepareDri = True
                    Dim tempTrain As New CSLinkTrain()
                    tempTrain.StartStaName = row.Cells(2).Value.ToString.Trim
                    tempTrain.StartTime = CDate(row.Cells(3).Value.ToString.Trim).TimeOfDay.TotalSeconds
                    tempTrain.EndStaName = row.Cells(2).Value.ToString.Trim
                    tempTrain.EndTime = CDate(row.Cells(4).Value.ToString.Trim).TimeOfDay.TotalSeconds
                    tempDri.AddTrain(tempTrain)
                    CSTrainsAndDrivers.PreParedDutyDrivers.Add(tempDri)
                Next
            End If

        Catch ex As Exception
            MsgBox("参数设置信息有误，请重新设置！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
            Exit Sub
        End Try

        '将基本设置信息存到数据库中

        sqlstr = "update CS_CREWBASICINF set condrivetime='" & Me.DateTimePicker4.Value & "',PREPARETRAINTIME='" & Me.DateTimePicker3.Value & "',PREPAREDUTYTIME='" & DateTimePicker2.Value & "',PREDUTYOFFTIME='" & DateTimePicker1.Value & "' WHERE LINEID='" & CStr(strCurlineID) & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "update CS_CREWBASICINF set MORNINGDISTANCE='" & CS_MorningMaxLength & "',DAYDISTANCE='" & CS_DayMaxLength & "',NIGHTDISTANCE='" & CS_NightMaxLength & "',CDAYDISTANCE='" & CS_CDayMaxLength & "' WHERE LINEID='" & CStr(strCurlineID) & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "update CS_CREWBASICINF set MORNINGMINDISTANCE='" & CS_MorningMinLength & "',DAYMINDISTANCE='" & CS_DayMinLength & "',NIGHTMINDISTANCE='" & CS_NightMinLength & "',CDAYMINDISTANCE='" & CS_CDayMinLength & "' WHERE LINEID='" & CStr(strCurlineID) & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)



        '将备车信息存到数据库中
        If strQCurCSPlanID = "" Then
            sqlstr = "DELETE FROM CS_PREPAREDTRAININF WHERE LINEID='" & CStr(strCurlineID) & "'"
        Else
            sqlstr = "DELETE FROM CS_result_PREPAREDTRAININF WHERE LINEID='" & CStr(strCurlineID) & "' and cstimetableid='" & strQCurCSPlanID & "'"
        End If
        Globle.Method.UpdateDataForAccess(sqlstr)

        If strQCurCSPlanID = "" Then
            sqlstr = "SELECT * FROM CS_PREPAREDTRAININF "
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            For Each row As DataGridViewRow In Me.DGVBeiche.Rows
                tempTable.Rows.Add(CStr(strCurlineID), row.Cells("备车名称").Value.ToString.Trim, row.Cells("备车地点").Value.ToString.Trim, row.Cells("所属区域").Value.ToString.Trim, _
                                   row.Cells("班种").Value.ToString.Trim, CDate(row.Cells("开始时间").Value.ToString).TimeOfDay.TotalSeconds, CDate(row.Cells("结束时间").Value.ToString).TimeOfDay.TotalSeconds)
            Next
            Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        Else
            sqlstr = "SELECT * FROM CS_result_PREPAREDTRAININF where cstimetableid='" & strQCurCSPlanID & "'"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            For Each row As DataGridViewRow In Me.DGVBeiche.Rows
                tempTable.Rows.Add(CStr(strCurlineID), row.Cells("备车名称").Value.ToString.Trim, row.Cells("备车地点").Value.ToString.Trim, row.Cells("所属区域").Value.ToString.Trim, _
                                   row.Cells("班种").Value.ToString.Trim, CDate(row.Cells("开始时间").Value.ToString).TimeOfDay.TotalSeconds, CDate(row.Cells("结束时间").Value.ToString).TimeOfDay.TotalSeconds, "", strQCurCSPlanID)
            Next
            Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        End If


        '将备班信息存到数据库中
        If strQCurCSPlanID = "" Then
            sqlstr = "DELETE FROM cs_prepareddutyinf WHERE LINEID='" & CStr(strCurlineID) & "'"
        Else
            sqlstr = "DELETE FROM cs_result_prepareddutyinf WHERE LINEID='" & CStr(strCurlineID) & "' and cstimetableid='" & strQCurCSPlanID & "'"
        End If
        Globle.Method.UpdateDataForAccess(sqlstr)
        If strQCurCSPlanID = "" Then
            sqlstr = "SELECT * FROM cs_prepareddutyinf "
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            For Each row As DataGridViewRow In Me.DGVBeiBan.Rows
                tempTable.Rows.Add(CStr(strCurlineID), row.Cells("备班名称").Value.ToString.Trim, row.Cells("备班地点").Value.ToString.Trim, row.Cells("所属区域2").Value.ToString.Trim, _
                                   row.Cells("班种2").Value.ToString.Trim, CDate(row.Cells("开始时间2").Value.ToString).TimeOfDay.TotalSeconds, CDate(row.Cells("结束时间2").Value.ToString).TimeOfDay.TotalSeconds)
            Next
            Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        Else
            sqlstr = "SELECT * FROM cs_result_prepareddutyinf where cstimetableid='" & strQCurCSPlanID & "'"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            For Each row As DataGridViewRow In Me.DGVBeiBan.Rows
                tempTable.Rows.Add(CStr(strCurlineID), row.Cells("备班名称").Value.ToString.Trim, row.Cells("备班地点").Value.ToString.Trim, row.Cells("所属区域2").Value.ToString.Trim, _
                                   row.Cells("班种2").Value.ToString.Trim, CDate(row.Cells("开始时间2").Value.ToString).TimeOfDay.TotalSeconds, CDate(row.Cells("结束时间2").Value.ToString).TimeOfDay.TotalSeconds, "", strQCurCSPlanID)
            Next
            Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        End If

        CSAutoPlanPara = New AutoPara
        CSAutoPlanPara.MoringDutyNum = TextBox1.Text.Trim
        CSAutoPlanPara.NightDutyNum = TextBox3.Text.Trim
        CSAutoPlanPara.DayDutyNum = TextBox2.Text
        CSAutoPlanPara.CDayDutyNum = TextBox4.Text

        Me.Close()
    End Sub

    Private Sub frmCSBasicSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If CSTrainsAndDrivers.CSLinkTrains IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSLinkTrains) > 0 Then
        Else
            MsgBox("请先选择加载运行图！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
        End If
        Dim i As Integer
        Dim sqlstr As String = ""
        sqlstr = "SELECT * FROM CS_CREWBASICINF WHERE LINEID='" & CStr(strCurlineID) & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        If tempTable.Rows.Count > 0 Then
            'ok
            Me.DateTimePicker4.Value = CDate(tempTable.Rows(0).Item("ConDriveTime").ToString)
            Me.DateTimePicker3.Value = CDate(tempTable.Rows(0).Item("PrepareTrainTime").ToString)
            Me.DateTimePicker2.Value = CDate(tempTable.Rows(0).Item("PREPAREDUTYTIME").ToString)
            Me.DateTimePicker1.Value = CDate(tempTable.Rows(0).Item("PREDUTYOFFTIME").ToString)
            Me.TxTMorningLength.Text = tempTable.Rows(0).Item("MorningDistance").ToString
            Me.TxTDayLength.Text = tempTable.Rows(0).Item("DayDistance").ToString
            Me.TXTCDayLength.Text = tempTable.Rows(0).Item("CDayDistance").ToString
            Me.TxTNightLength.Text = tempTable.Rows(0).Item("NightDistance").ToString
            Me.TxTMinMorningLength.Text = tempTable.Rows(0).Item("MorningminDistance").ToString
            Me.TxTMinDayLength.Text = tempTable.Rows(0).Item("DayminDistance").ToString
            Me.TXTMinCDayLength.Text = tempTable.Rows(0).Item("CDayminDistance").ToString
            Me.TxTMinNightLength.Text = tempTable.Rows(0).Item("NightminDistance").ToString
        End If

        If strQCurCSPlanID = "" Then
            sqlstr = "SELECT * FROM cs_preparedtraininf WHERE LINEID='" & CStr(strCurlineID) & "'"
        Else
            sqlstr = "SELECT * FROM cs_result_preparedtraininf WHERE LINEID='" & CStr(strCurlineID) & "' and cstimetableid='" & strQCurCSPlanID & "'"
        End If
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        Me.DGVBeiche.RowCount = tempTable.Rows.Count
        For i = 0 To tempTable.Rows.Count - 1
            Me.DGVBeiche.Rows(i).Cells("班种").Value = tempTable.Rows(i).Item("dutysort").ToString
            Me.DGVBeiche.Rows(i).Cells("备车名称").Value = tempTable.Rows(i).Item("NAME").ToString
            Me.DGVBeiche.Rows(i).Cells("备车地点").Value = tempTable.Rows(i).Item("place").ToString
            Me.DGVBeiche.Rows(i).Cells("开始时间").Value = CDate(BeTime(tempTable.Rows(i).Item("starttime").ToString)).TimeOfDay
            Me.DGVBeiche.Rows(i).Cells("结束时间").Value = CDate(BeTime(tempTable.Rows(i).Item("endtime").ToString)).TimeOfDay
            Me.DGVBeiche.Rows(i).Cells("所属区域").Value = tempTable.Rows(i).Item("remark").ToString
        Next

        If strQCurCSPlanID = "" Then
            sqlstr = "SELECT * FROM cs_prepareddutyinf WHERE LINEID='" & CStr(strCurlineID) & "'"
        Else
            sqlstr = "SELECT * FROM cs_result_prepareddutyinf WHERE LINEID='" & CStr(strCurlineID) & "' and cstimetableid='" & strQCurCSPlanID & "'"
        End If
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        Me.DGVBeiBan.RowCount = tempTable.Rows.Count
        For i = 0 To tempTable.Rows.Count - 1
            Me.DGVBeiBan.Rows(i).Cells("班种2").Value = tempTable.Rows(i).Item("dutysort").ToString
            Me.DGVBeiBan.Rows(i).Cells("备班名称").Value = tempTable.Rows(i).Item("NAME").ToString
            Me.DGVBeiBan.Rows(i).Cells("备班地点").Value = tempTable.Rows(i).Item("place").ToString
            Me.DGVBeiBan.Rows(i).Cells("开始时间2").Value = CDate(BeTime(tempTable.Rows(i).Item("starttime").ToString)).TimeOfDay
            Me.DGVBeiBan.Rows(i).Cells("结束时间2").Value = CDate(BeTime(tempTable.Rows(i).Item("endtime").ToString)).TimeOfDay
            Me.DGVBeiBan.Rows(i).Cells("所属区域2").Value = tempTable.Rows(i).Item("remark").ToString
        Next
        If CSAutoPlanPara IsNot Nothing Then
            TextBox1.Text = CSAutoPlanPara.MoringDutyNum
            TextBox3.Text = CSAutoPlanPara.NightDutyNum
            TextBox2.Text = CSAutoPlanPara.DayDutyNum
            TextBox4.Text = CSAutoPlanPara.CDayDutyNum
        Else
            TextBox1.Text = "0"
            TextBox3.Text = "0"
            TextBox2.Text = "0"
            TextBox4.Text = "0"

        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.DGVBeiche.Rows.Add("早班", "备车" + CStr(Me.DGVBeiche.Rows.Count + 1), "", "", "", "")
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
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

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Me.DGVBeiBan.Rows.Add("早班", "备班" + CStr(Me.DGVBeiBan.Rows.Count + 1), "", "", "", "")
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
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