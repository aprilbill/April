Public Class FrmDiffSta
    Private notSameStaList As New List(Of String)

    Private Sub FrmDiffSta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        
        For Each dri As CSDriver In CSTrainsAndDrivers.NightDrivers
            If notSameStaList.Contains(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName.Trim) = False Then
                notSameStaList.Add(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName.Trim)
            End If
        Next
        For Each dri As CSDriver In CSTrainsAndDrivers.MorningDrivers
            If notSameStaList.Contains(dri.CSLinkTrain(1).StartStaName.Trim) = False Then
                notSameStaList.Add(dri.CSLinkTrain(1).StartStaName.Trim)
            End If
        Next
        For Each dri As CSDriver In CSTrainsAndDrivers.CorMorningDrivers
            If notSameStaList.Contains(dri.CSLinkTrain(1).StartStaName.Trim) = False Then
                notSameStaList.Add(dri.CSLinkTrain(1).StartStaName.Trim)
            End If
        Next
        For Each dri As CSDriver In CSTrainsAndDrivers.CorNightDrivers
            If notSameStaList.Contains(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName.Trim) = False Then
                notSameStaList.Add(dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName.Trim)
            End If
        Next
        check()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If IsNothing(ListBox1.SelectedItems) = False AndAlso ListBox1.SelectedItems.Count > 0 Then
            If MsgBox("确认所选站点视为相同？", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                Dim newPart As New List(Of String)
                For i As Integer = 0 To ListBox1.SelectedItems.Count - 1
                    newPart.Add(ListBox1.SelectedItems(i).ToString)
                Next
                notSameStationInfo.Add(newPart)
                check()
            End If
        End If
    End Sub
    Public Sub check()
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        If notSameStationInfo.Count > 0 Then
            For i As Integer = 0 To notSameStationInfo.Count - 1
                Dim str As String = ""
                For j As Integer = 0 To notSameStationInfo(i).Count - 1
                    If j = notSameStationInfo(i).Count - 1 Then
                        str &= notSameStationInfo(i)(j)
                    Else
                        str &= notSameStationInfo(i)(j) & "-"
                    End If
                Next
                ListBox2.Items.Add(str)
            Next
        Else
            For i As Integer = 0 To notSameStaList.Count - 1
                Dim j As Integer = 0
                For j = 0 To notSameStationInfo.Count - 1
                    If NotSameStationInf(j).Contains(notSameStaList(i)) Then
                        Exit For
                    End If
                Next
                If j = notSameStationInfo.Count Then
                    ListBox1.Items.Add(notSameStaList(i))
                End If
            Next
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If IsNothing(ListBox2.SelectedItem) = False Then
            If MsgBox("确认撤销已设置的参数？", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                notSameStationInfo.RemoveAt(ListBox2.SelectedIndex)
                check()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class