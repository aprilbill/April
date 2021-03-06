Module modNetWholeSub
    '比较两字符串，计算第二个字串在第一个中出现的次数
    Public Function GetStringInNumber(ByVal strString As String, ByVal StrCompare As String) As Integer
        Dim i As Integer
        Dim intNum As Integer
        intNum = 0
        For i = 1 To strString.Length
            If strString.Substring(i - 1, 1) = StrCompare Then
                intNum = intNum + 1
            End If
        Next
        GetStringInNumber = intNum
    End Function

    '将一个字符串分解成若干个字符串
    Public Sub GetListStringValue(ByVal StrTemp As String, ByVal PaiXu As Integer, ByVal FenStr As String)
        Dim i As Integer
        Dim Str As String
        Dim tmpStr() As String
        Dim nTemp As Integer

        ReDim tmpStr(0)
        Str = Trim(StrTemp)
        Dim nLast As Integer
        Dim nTol As Integer
        nTol = 1
        Dim tempS As String
        Do
            nLast = InStrRev(Str, FenStr, , CompareMethod.Text) '右边数过来第几个值
            tempS = Microsoft.VisualBasic.Right(Str, Len(Str) - nLast)
            If Trim(tempS) <> "" Then
                ReDim Preserve tmpStr(UBound(tmpStr) + 1)
                tmpStr(UBound(tmpStr)) = tempS
            End If
            If nLast = 0 Then Exit Do
            nTol = nTol + 1
            Str = Microsoft.VisualBasic.Left(Str, nLast - 1)
            Str = Trim(Str)
        Loop

        If PaiXu = 0 Then '从左至右排序
            ReDim g_ListString(UBound(tmpStr))
            nTemp = 1
            For i = UBound(tmpStr) To 1 Step -1
                g_ListString(nTemp) = tmpStr(i)
                nTemp = nTemp + 1
            Next
        Else '从右至左排序
            ReDim g_ListString(UBound(tmpStr))
            For i = 1 To UBound(tmpStr)
                g_ListString(nTemp) = tmpStr(i)
            Next
        End If
    End Sub
End Module
