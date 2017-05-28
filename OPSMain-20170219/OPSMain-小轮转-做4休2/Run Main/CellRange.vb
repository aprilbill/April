Public Class CellRange
    Public startCell As ExcelCell
    Public endCell As ExcelCell

    Public Sub New(ByVal _startcell As ExcelCell, ByVal _endcell As ExcelCell)
        startCell = _startcell
        endCell = _endcell
    End Sub

End Class
