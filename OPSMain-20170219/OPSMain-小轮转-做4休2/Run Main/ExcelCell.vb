Public Class ExcelCell
    Public celltext As String
    Public row As Integer
    Public column As Integer

    Public Sub New(ByVal _row As Integer, ByVal _column As Integer)
        row = _row
        column = _column
        celltext = ""
    End Sub

End Class
