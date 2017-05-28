Imports AutoCAD
Module modCAD
    Public AcadApp As AcadApplication
    Public LineObj As AcadLine
    Public testLayer As AcadLayer
    Public TextObj As AcadText
    Public Sub AddCADLine(ByVal StartPoint() As Double, ByVal EndPoint() As Double, ByVal LineColor As ACAD_COLOR, ByVal LineWeight As ACAD_LWEIGHT)
        If StartPoint(0) > -100000 And EndPoint(0) > -100000 Then
            'LineObj.color = ACAD_COLOR.
            LineObj = AcadApp.ActiveDocument.ModelSpace.AddLine(StartPoint, EndPoint)
            LineObj.color = LineColor
            LineObj.Lineweight = LineWeight
        End If
    End Sub

    Public Sub addCADText(ByVal StyObj1 As AcadTextStyle, ByVal textString As String, ByVal InsertPoint() As Double, ByVal Height As Double, ByVal RotateAngle As Double, ByVal textColor As ACAD_COLOR)
        If InsertPoint(0) > -100000 Then
            AcadApp.ActiveDocument.ActiveTextStyle = StyObj1
            TextObj = AcadApp.ActiveDocument.ModelSpace.AddText(textString, InsertPoint, Height)
            TextObj.color = textColor
            TextObj.Rotate(InsertPoint, RotateAngle)
        End If
    End Sub

End Module
