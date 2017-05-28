Public Class FrmSegCon

    Private Sub FrmSegCon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ListBox1.Items.Clear()
        Call SortMergedCSLinkTrain(True)
        If CSTrainsAndDrivers.CSLinkTrains IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSLinkTrains) > 0 AndAlso _
            CSTrainsAndDrivers.MergedCSLinkTrains IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.MergedCSLinkTrains) > 0 Then
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                Dim MerTrains As MergedCSLinkTrain = CSTrainsAndDrivers.MergedCSLinkTrains(i)
                Dim str As String = ""
                If UBound(MerTrains.CSLinkTrains) > 1 Then
                    For j As Integer = 1 To UBound(MerTrains.CSLinkTrains)
                        Dim train As CSLinkTrain = MerTrains.CSLinkTrains(j)
                        If j < UBound(MerTrains.CSLinkTrains) Then
                            str &= "(" & train.OutputCheCi & ")" & train.StartStaName & "->" & train.EndStaName & "《-》"
                        Else
                            str &= "(" & train.OutputCheCi & ")" & train.StartStaName & "->" & train.EndStaName
                        End If
                    Next
                    Me.ListBox1.Items.Add(str)
                End If
            Next
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Me.ListBox1.SelectedItems.Count = 1 Then
            '===========找出需要删除的mertrain
            Dim index As Integer = -1
            Dim Delmertrain As MergedCSLinkTrain = Nothing
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                Dim MerTrain As MergedCSLinkTrain = CSTrainsAndDrivers.MergedCSLinkTrains(i)
                Dim str As String = ""
                If UBound(MerTrain.CSLinkTrains) > 1 Then
                    For j As Integer = 1 To UBound(MerTrain.CSLinkTrains)
                        Dim train As CSLinkTrain = MerTrain.CSLinkTrains(j)
                        If j < UBound(MerTrain.CSLinkTrains) Then
                            str &= "(" & train.OutputCheCi & ")" & train.StartStaName & "->" & train.EndStaName & "《-》"
                        Else
                            str &= "(" & train.OutputCheCi & ")" & train.StartStaName & "->" & train.EndStaName
                        End If
                    Next
                End If
                If str = Me.ListBox1.SelectedItems(0).ToString Then
                    index = i
                    Delmertrain = MerTrain
                    Exit For
                End If
            Next

            If index > 0 AndAlso Delmertrain IsNot Nothing Then
                If MsgBox("衔接解除后不可恢复，确定解除衔接？", MsgBoxStyle.OkCancel + MsgBoxStyle.Information, "提醒") = MsgBoxResult.Ok Then
                    If index < UBound(CSTrainsAndDrivers.MergedCSLinkTrains) Then
                        Dim Merlist As New List(Of MergedCSLinkTrain)
                        For i As Integer = index + 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                            Merlist.Add(CSTrainsAndDrivers.MergedCSLinkTrains(i))
                        Next
                        ReDim Preserve CSTrainsAndDrivers.MergedCSLinkTrains(index - 1)
                        For Each mt As MergedCSLinkTrain In Merlist
                            ReDim Preserve CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains) + 1)
                            CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains)) = mt
                        Next
                    Else
                        ReDim Preserve CSTrainsAndDrivers.MergedCSLinkTrains(index - 1)
                    End If
                    '=============把每个解除的列车形成mertrain添加到mertrains集合中
                    For i As Integer = 1 To UBound(Delmertrain.CSLinkTrains)
                        ReDim Preserve CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains) + 1)
                        CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains)) = New MergedCSLinkTrain
                        CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains)).AddCSLinkTrain(Delmertrain.CSLinkTrains(i))
                    Next
                    Me.ListBox1.Items.RemoveAt(Me.ListBox1.SelectedIndex)
                    MsgBox("请重新编制所涉及的任务段")
                End If
            End If

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        FrmAddSeqCon.ShowDialog()
        Me.ListBox1.Items.Clear()
        Call SortMergedCSLinkTrain(True)
        If CSTrainsAndDrivers.CSLinkTrains IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSLinkTrains) > 0 AndAlso _
            CSTrainsAndDrivers.MergedCSLinkTrains IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.MergedCSLinkTrains) > 0 Then
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                Dim MerTrains As MergedCSLinkTrain = CSTrainsAndDrivers.MergedCSLinkTrains(i)
                Dim str As String = ""
                If UBound(MerTrains.CSLinkTrains) > 1 Then
                    For j As Integer = 1 To UBound(MerTrains.CSLinkTrains)
                        Dim train As CSLinkTrain = MerTrains.CSLinkTrains(j)
                        If j < UBound(MerTrains.CSLinkTrains) Then
                            str &= "(" & train.OutputCheCi & ")" & train.StartStaName & "->" & train.EndStaName & "《-》"
                        Else
                            str &= "(" & train.OutputCheCi & ")" & train.StartStaName & "->" & train.EndStaName
                        End If
                    Next
                    Me.ListBox1.Items.Add(str)
                End If
            Next
        End If
    End Sub

   
End Class