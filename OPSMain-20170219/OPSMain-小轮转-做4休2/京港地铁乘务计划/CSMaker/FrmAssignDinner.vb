Public Class FrmAssignDinner

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub FrmAssignDinner_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DataGridView1.Rows.Clear()
        Dim index As Integer = 0
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
            Call ShowDinnerInfo(CSTrainsAndDrivers.MorningDrivers, index)
            Call ShowDinnerInfo(CSTrainsAndDrivers.DayDrivers, index)
            Call ShowDinnerInfo(CSTrainsAndDrivers.CDayDrivers, index)
            Call ShowDinnerInfo(CSTrainsAndDrivers.NightDrivers, index)
        End If
    End Sub

    Public Sub ShowDinnerInfo(ByVal Dris As List(Of CSDriver), ByRef index As Integer)
        For Each dri As CSDriver In Dris
            For Each skstr As String In dri.AllDinnerInfo.Keys
                dri.DinnerStation = skstr.Split("-")(0)
                dri.DinnerStartTime = skstr.Split("-")(1)
                dri.DinnerEndTime = skstr.Split("-")(2)
                Select Case dri.DinnerType
                    Case "接班前用餐"
                        index += 1
                        Me.DataGridView1.Rows.Add(index, dri.CSdriverNo, "是", "接班前用餐", "", "", dri.CSLinkTrain(1).sCheDiHao & dri.CSLinkTrain(1).OutputCheCi, BeTime(dri.CSLinkTrain(1).StartTime), dri.CSLinkTrain(1).StartStaName)
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Tag = dri
                    Case "退勤后用餐"
                        index += 1
                        Me.DataGridView1.Rows.Add(index, dri.CSdriverNo, "是", "退勤后用餐", dri.CSLinkTrain(UBound(dri.CSLinkTrain)).sCheDiHao & dri.CSLinkTrain(UBound(dri.CSLinkTrain)).OutputCheCi, BeTime(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndTime), "", "", dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName)
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Tag = dri
                    Case "班中用餐"
                        index += 1

                        For i As Integer = 1 To UBound(dri.CSLinkTrain)
                            If AddLitterTime(dri.CSLinkTrain(i).EndTime) <= dri.DinnerStartTime And AddLitterTime(dri.CSLinkTrain(i).EndTime) + dri.AllDinnerInfo(skstr).DinnerTime <= dri.DinnerEndTime AndAlso dri.CSLinkTrain(i).EndStaName = dri.DinnerStation AndAlso i < UBound(dri.CSLinkTrain) Then
                                If AddLitterTime(dri.CSLinkTrain(i).EndTime) <= dri.AllDinnerInfo(skstr).dinnerEndTime AndAlso AddLitterTime(dri.CSLinkTrain(i).EndTime) >= dri.AllDinnerInfo(skstr).dinnerStartTime Then
                                    Me.DataGridView1.Rows.Add(index, dri.CSdriverNo, "是", "班中用餐", dri.CSLinkTrain(i).sCheDiHao & dri.CSLinkTrain(i).OutputCheCi, BeTime(dri.CSLinkTrain(i).EndTime), dri.CSLinkTrain(i + 1).sCheDiHao & dri.CSLinkTrain(i + 1).OutputCheCi, BeTime(dri.CSLinkTrain(i + 1).StartTime), dri.CSLinkTrain(i).EndStaName)
                                    Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Tag = dri
                                End If
                            End If
                        Next

                End Select
            Next
            If dri.AllDinnerInfo.Count = 0 Then
                index += 1
                Me.DataGridView1.Rows.Add(index, dri.CSdriverNo, "否", "", "", "", "", "", "")
                Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Tag = dri
                Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).DefaultCellStyle.BackColor = Color.OrangeRed
            End If
        Next
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If Me.DataGridView1.SelectedRows.Count = 1 Then
            Dim frm As New FrmSetDinner
            frm.SelectDriver = Me.DataGridView1.SelectedRows(0).Tag
            If frm.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.DataGridView1.Rows.Clear()
                Dim index As Integer = 0
                If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
                    Call ShowDinnerInfo(CSTrainsAndDrivers.MorningDrivers, index)
                    Call ShowDinnerInfo(CSTrainsAndDrivers.DayDrivers, index)
                    Call ShowDinnerInfo(CSTrainsAndDrivers.CDayDrivers, index)
                    Call ShowDinnerInfo(CSTrainsAndDrivers.NightDrivers, index)
                End If
            End If
        End If

    End Sub
End Class