Public Class FrmAddSeqCon
    Private addList As New List(Of Integer)
    Private intialList As New List(Of Integer)
    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Me.ListBox1.Items.Clear()
        Call SortMergedCSLinkTrain(True)
        If CSTrainsAndDrivers.CSLinkTrains IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSLinkTrains) > 0 AndAlso _
            CSTrainsAndDrivers.MergedCSLinkTrains IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.MergedCSLinkTrains) > 0 Then
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                Dim MerTrains As MergedCSLinkTrain = CSTrainsAndDrivers.MergedCSLinkTrains(i)
                Dim str As String = ""
                For j As Integer = 1 To UBound(MerTrains.CSLinkTrains)
                    Dim train As CSLinkTrain = MerTrains.CSLinkTrains(j)
                    If j < UBound(MerTrains.CSLinkTrains) Then
                        str &= "(" & train.OutputCheCi & ")" & train.StartStaName & "->" & train.EndStaName & "《-》"
                    Else
                        str &= "(" & train.OutputCheCi & ")" & train.StartStaName & "->" & train.EndStaName
                    End If
                Next
                intialList.Add(i)
                Me.ListBox1.Items.Add(str)
            Next
        End If

    End Sub
    Private Sub ListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseDoubleClick
        If IsNothing(ListBox1.SelectedItem) = False Then
            Dim MerTrains As MergedCSLinkTrain = CSTrainsAndDrivers.MergedCSLinkTrains(intialList(ListBox1.SelectedIndex))
            If addList.Count > 0 Then
                Dim LastMerTrains As MergedCSLinkTrain = CSTrainsAndDrivers.MergedCSLinkTrains(addList(addList.Count - 1))
                If LastMerTrains.CSLinkTrains.Count > 1 Then
                    If LastMerTrains.CSLinkTrains(UBound(LastMerTrains.CSLinkTrains)).EndStaName <> MerTrains.CSLinkTrains(1).StartStaName Then
                        If MsgBox("待添加的子任务段起始位置和接续的终止位置不一致！" & vbCrLf & "是否继续添加", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                            Return
                        End If
                    End If
                End If
            End If
            addList.Add(intialList(ListBox1.SelectedIndex))
            intialList.RemoveAt(ListBox1.SelectedIndex)
            ListBox2.Items.Add(ListBox1.Items(ListBox1.SelectedIndex))
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        End If
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub ListBox2_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox2.MouseDoubleClick
        If IsNothing(ListBox2.SelectedItem) = False Then
            intialList.Add(addList(ListBox2.SelectedIndex))
            addList.RemoveAt(ListBox2.SelectedIndex)
            ListBox1.Items.Add(ListBox2.Items(ListBox2.SelectedIndex))
            ListBox2.Items.RemoveAt(ListBox2.SelectedIndex)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If addList.Count < 1 Then
            Return
        End If
        If MsgBox("确认添加？", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
            Dim Merlist As New List(Of MergedCSLinkTrain)
            Dim allMerlist As New List(Of MergedCSLinkTrain)
            If CSTrainsAndDrivers.CSLinkTrains IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSLinkTrains) > 0 AndAlso _
          CSTrainsAndDrivers.MergedCSLinkTrains IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.MergedCSLinkTrains) > 0 Then
                For i As Integer = 1 To UBound(CSTrainsAndDrivers.MergedCSLinkTrains)
                    Dim j As Integer = 0
                    For j = 0 To addList.Count - 1
                        If i = addList(j) Then
                            Exit For
                        End If
                    Next
                    If j = addList.Count Then
                        allMerlist.Add(CSTrainsAndDrivers.MergedCSLinkTrains(i))
                    End If
                Next
            End If
            For i As Integer = 0 To addList.Count - 1
                Merlist.Add(CSTrainsAndDrivers.MergedCSLinkTrains(addList(i)))
            Next
            ReDim CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains) - addList.Count)
            For i As Integer = 0 To allMerlist.Count - 1
                CSTrainsAndDrivers.MergedCSLinkTrains(i + 1) = allMerlist(i)
            Next
            ReDim Preserve CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains))
            CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains)) = New MergedCSLinkTrain
            For i As Integer = 0 To Merlist.Count - 1
                For j As Integer = 1 To UBound(Merlist(i).CSLinkTrains)
                    CSTrainsAndDrivers.MergedCSLinkTrains(UBound(CSTrainsAndDrivers.MergedCSLinkTrains)).AddCSLinkTrain(Merlist(i).CSLinkTrains(j))
                Next
            Next
            MsgBox("添加完毕！请重新编制所涉及的部分")
        End If
    End Sub
End Class