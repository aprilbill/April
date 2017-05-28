Public Class FrmPilianganpai
    Public beclass2Duty As New Dictionary(Of String, String)
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub FrmPilianganpai_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim str As String = "select distinct(beclass) from cs_driverinf where lineid='" & CurLineName & "'"
        Dim tmpDT As New DataTable
        tmpDT = Globle.Method.ReadDataForAccess(str)
        For i As Integer = 0 To tmpDT.Rows.Count - 1
            ComboBox1.Items.Add(tmpDT.Rows(i).Item(0).ToString.Trim)
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.Text.Trim <> "" Or ComboBox2.Text.Trim <> "" Then
            For i As Integer = 0 To ListBox1.Items.Count - 1
                If ListBox1.Items(i).ToString.Contains(ComboBox1.Text.Trim) Or ListBox1.Items(i).ToString.Contains(ComboBox2.Text.Trim) Then
                    Exit Sub
                End If
            Next
            ListBox1.Items.Add(ComboBox1.Text.Trim & "-" & ComboBox2.Text.Trim)
            beclass2Duty.Add(ComboBox1.Text.Trim, ComboBox2.Text.Trim)
        End If
    End Sub

   

    Private Sub ListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseDoubleClick
        Dim index As Integer = ListBox1.IndexFromPoint(e.Location)
        If index <> System.Windows.Forms.ListBox.NoMatches Then
            beclass2Duty.Remove(ListBox1.Items(index).ToString.Split("-")(0))
            ListBox1.Items.RemoveAt(index)
        End If
    End Sub
End Class