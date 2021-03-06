Module modCalTime

    Structure typeTrainSequeInformation
        Dim nTrain As Integer
        Dim nTime As Integer
    End Structure
    Public TrainSeq() As typeTrainSequeInformation

    Public Function GetFirstTrainTime(ByVal nID As Integer) As String
        GetFirstTrainTime = SecondToHour(5 * 3600 + nID * 600, 0)
    End Function

    '秒转化为小时
    Public Function SecondToHour(ByVal temp As Integer, ByVal Mark As Integer) As String
        SecondToHour = ""
        If temp = -1 Then
            SecondToHour = ""
            Exit Function
        End If
        '将时间以小时算
        Dim HStr As String
        Dim MStr As String
        Dim sStr As String
        Dim sSpace As String
        sSpace = " "
        HStr = Trim$(Str$(Int(temp / 3600)))
        MStr = Trim$(Str$(Int((temp - Val(HStr) * 3600) / 60)))
        sStr = Trim$(Str$(temp - Val(HStr) * 3600 - Val(MStr) * 60))

        If Val(HStr) < 10 And Val(HStr) > 0 Then
            HStr = sSpace & HStr
        ElseIf Val(HStr) = 24 Or Val(HStr) = 0 Then
            HStr = sSpace & "0"
        End If

        If Val(MStr) < 10 And Val(MStr) > 0 Then
            MStr = "0" & MStr
        ElseIf Val(MStr) = 0 Then
            MStr = "00"
        End If

        If Val(sStr) < 10 And Val(sStr) > 0 Then
            sStr = "0" & sStr
        ElseIf Val(sStr) = 0 Then
            sStr = "00"
        End If
        Select Case Mark
            Case 0
                SecondToHour = HStr & ":" & MStr & ":" & sStr
            Case 1
                SecondToHour = HStr & "." & MStr & "." & sStr
            Case 2
                SecondToHour = HStr & ":" & MStr
            Case 3
                SecondToHour = HStr & "." & MStr
            Case 4
                SecondToHour = HStr & "-" & MStr
        End Select

    End Function

    '将秒转化为分钟
    Public Function SecondToMinute(ByVal Second As Long) As String
        If Second = -1 Then
            SecondToMinute = "无"
            Exit Function
        End If
        Dim Min As String
        Dim Sec As String
        Min = Int(Second / 60)
        If Val(Min) = 0 Then
            Min = "0"
        End If
        Sec = Trim(Str(Int(Second - Int(Min) * 60)))
        If Len(Sec) = 1 Then
            Sec = Trim("0" & Trim(Sec))
        End If
        'Least = Format(MinuTime - Int(MinuTime / 60) * 60, "##0") * 0.01
        SecondToMinute = Min & "." & Sec
    End Function

    '相对时间值
    Public Function GetCompareTime(ByVal nTime As Integer) As Integer
        If nTime - TimeTablePara.TimeTableDiagramPara.intCompareFirstTime < 0 Then
            nTime = nTime + 24 * 3600
            GetCompareTime = nTime
        Else
            GetCompareTime = nTime
        End If
    End Function

    '时刻作差
    Public Function TimeMunusTime(ByVal Time1 As Long, ByVal Time2 As Long) As Long
        If Time1 >= Time2 Then
            TimeMunusTime = Time1 - Time2
        Else
            TimeMunusTime = 3600 * 24 + Time1 - Time2
        End If
    End Function
End Module
