Class ListViewItemComparer
    Implements IComparer
    Private col As Integer
    Private order As SortOrder
    Public Sub New()
        col = 0
        order = SortOrder.Ascending
    End Sub

    Public Sub New(ByVal column As Integer, ByVal order As SortOrder)
        col = column
        Me.order = order
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        Dim returnVal As Integer
        ' Determine whether the type being compared is a date type.
        Try
            ' Parse the two objects passed as a parameter as a DateTime.
            Dim firstDate As System.DateTime = DateTime.Parse(CType(x, ListViewItem).SubItems(col).Text)
            Dim secondDate As System.DateTime = DateTime.Parse(CType(y, ListViewItem).SubItems(col).Text)
            ' Compare the two dates.
            returnVal = DateTime.Compare(firstDate, secondDate)
            ' If neither compared object has a valid date format, 
            ' compare as a string.
        Catch
            ' Compare the two items as a string.
            returnVal = [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
        End Try
        ' Determine whether the sort order is descending.
        If order = SortOrder.Descending Then
            ' Invert the value returned by String.Compare.
            returnVal *= -1
        End If
        Return returnVal
    End Function
End Class


