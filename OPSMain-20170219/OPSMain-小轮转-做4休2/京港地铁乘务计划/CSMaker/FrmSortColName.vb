Public Class FrmSortColName

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Me.ListBoxColName.SelectedIndex > -1 Then
            Me.ListBoxColName2.Items.Add(Me.ListBoxColName.SelectedItem)
            Me.ListBoxColName.Items.Remove(Me.ListBoxColName.SelectedItem)
        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If Me.ListBoxColName2.SelectedIndex > -1 Then
            Me.ListBoxColName.Items.Add(Me.ListBoxColName2.SelectedItem)
            Me.ListBoxColName2.Items.Remove(Me.ListBoxColName2.SelectedItem)
        End If
    End Sub

    Public ziduan As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Me.ListBoxColName2.Items.Count > 0 Then
            ziduan = Me.ListBoxColName2.Items(0).ToString()
            For i As Integer = 1 To Me.ListBoxColName2.Items.Count - 1
                ziduan = ziduan & ";" & Me.ListBoxColName2.Items(i).ToString()
            Next
        End If
        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub FrmSortColName_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class