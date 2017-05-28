Module ModStaExpress
    'Public GuDaoJianJu As Integer = 64
    'Public CurGudao As Integer = 1              '记录当前所在股道
    'Public MyYBix As Single                     '记录图形变形比例
    'Public ConStaInf() As typeStationInformation    '记录原始车站信息


    'Public Sub ExpressStation(ByRef sta() As typeStationInformation, ByVal index As Integer)
    '    '展开车站，相当于添加虚拟车站
    '    Dim i, k, IDvirSta As Integer
    '    IDvirSta = 0

    '    '查找是否增加过虚拟站
    '    For i = 1 To UBound(sta)
    '        If sta(i).sStationName = sta(index).sStationName And sta(i).IsVirtul = True Then
    '            IDvirSta = i
    '            Exit For
    '        End If
    '    Next
    '    '若没增加过虚拟站则车站信息数组维数加1
    '    If IDvirSta = 0 Then
    '        ReDim Preserve sta(UBound(sta) + 1)
    '        k = UBound(sta)
    '        '设置虚拟站信息
    '        sta(k) = sta(index)
    '        sta(k).sStationName = sta(index).sStationName
    '        sta(k).sPrintStaName = ""
    '        sta(k).IsVirtul = True
    '    Else
    '        '若增加过
    '        k = IDvirSta
    '    End If

    '    '修改虚拟站坐标
    '    sta(k).Ycord = sta(index).Ycord + GuDaoJianJu * sta(index).nStLineNum

    '    If sta(index).IsExpress = False Then
    '        sta(index).IsExpress = True
    '        sta(k).IsExpress = True
    '    End If
    '    sta(index).IsVirtul = False

    '    '  MessageBox.Show("k " & k & "  " & sta(k).sStationName, "index:" & index.ToString & "  " & sta(index).sStationName, MessageBoxButtons.OK)


    '    '修改其余车站坐标
    '    For i = 1 To UBound(sta)
    '        If sta(i).Ycord > sta(index).Ycord And i <> k Then
    '            sta(i).Ycord += GuDaoJianJu * sta(index).nStLineNum
    '        End If
    '    Next

    '    '修改列车经由、发到时间信息
    '    Call ExpModifyTraininf(TrainInf, index, k)


    'End Sub
    'Public Sub UnfoldStation(ByRef sta() As typeStationInformation, ByVal index As Integer)
    '    Dim i, k As Integer
    '    Dim strStaName As String
    '    'If sta(index).IsExpress = True Then

    '    strStaName = sta(index).sStationName.Trim
    '    For i = 1 To UBound(sta)
    '        If sta(i).sStationName = strStaName And sta(i).IsVirtul = True Then
    '            k = i
    '            Exit For
    '        End If
    '    Next i

    '    If sta(index).IsExpress = True Then
    '        sta(index).IsExpress = False
    '        sta(k).IsExpress = False
    '    End If

    '    For i = 1 To UBound(sta)
    '        If sta(i).Ycord > sta(index).Ycord Then
    '            sta(i).Ycord -= GuDaoJianJu * sta(index).nStLineNum
    '        End If
    '    Next
    '    ' ReDim Preserve sta(UBound(sta) - 1)
    '    ' End If

    '    Call UnfoldModifyTraininf(TrainInf, index, k)
    'End Sub

    'Public Sub ExpModifySectioninf(ByRef sec() As typeSecInf, ByVal ID1 As Integer, ByVal ID2 As Integer)
    '    'ID1---实际车站ID      ID2---虚拟车站ID
    '    Dim i As Integer
    '    For i = 1 To UBound(sec)
    '        If sec(i).nFirStaID = ID1 Then
    '            sec(i).nFirStaID = ID2
    '        End If
    '    Next
    'End Sub



    'Public Sub ExpModifyTraininf(ByRef tmpTrain() As typeTrainInformation, ByVal ID1 As Integer, ByVal ID2 As Integer)
    '    Dim i, j As Integer
    '    For i = 0 To UBound(tmpTrain)
    '        ReDim Preserve tmpTrain(i).Starting(UBound(StationInf))
    '        ReDim Preserve tmpTrain(i).Arrival(UBound(StationInf))
    '        ReDim Preserve tmpTrain(i).StopLine(UBound(StationInf))

    '        '设置列车i在虚拟车站的到发时刻
    '        tmpTrain(i).Starting(ID2) = tmpTrain(i).Starting(ID1)
    '        tmpTrain(i).Arrival(ID2) = tmpTrain(i).Arrival(ID1)
    '        tmpTrain(i).StopLine(ID2) = tmpTrain(i).StopLine(ID1)

    '        If IsNothing(tmpTrain(i).nFirstID) = True Then
    '            ReDim tmpTrain(i).nFirstID(1)
    '            ReDim tmpTrain(i).nSecondID(1)
    '        End If
    '        For j = 1 To UBound(tmpTrain(i).nFirstID)
    '            If i Mod 2 <> 0 Then
    '                '下行列车的发站为虚拟站
    '                If tmpTrain(i).nFirstID(j) = ID1 Then
    '                    tmpTrain(i).nFirstID(j) = ID2
    '                End If

    '            Else
    '                '上行列车的到达站为虚拟站
    '                If tmpTrain(i).nSecondID(j) = ID1 Then
    '                    tmpTrain(i).nSecondID(j) = ID2
    '                End If

    '            End If
    '        Next j

    '    Next i
    'End Sub

    'Public Sub UnfoldModifyTraininf(ByRef tmpTrain() As typeTrainInformation, ByVal ID1 As Integer, ByVal ID2 As Integer)
    '    Dim i, j As Integer
    '    For i = 1 To UBound(tmpTrain)
    '        For j = 1 To UBound(tmpTrain(i).nFirstID)
    '            If i Mod 2 <> 0 Then
    '                If tmpTrain(i).nFirstID(j) = ID2 Then
    '                    tmpTrain(i).nFirstID(j) = ID1

    '                End If
    '            Else
    '                If tmpTrain(i).nSecondID(j) = ID2 Then
    '                    tmpTrain(i).nSecondID(j) = ID1

    '                End If
    '            End If
    '        Next j
    '    Next i


    'End Sub

    'Public Function GetCurLineY(ByVal CurLineNo As Integer, ByVal TolLineNo As Integer, ByVal LineInterval As Integer, ByVal BaseY As Single, ByVal CheCi As Integer) As Single
    '    '注意BaseY为实体车站的picYcord坐标,LineInterval也是转换为图上坐标后的距离，即LineInterval=GuDaoJianJu*MyYBix 
    '    Dim tmpResult As Single

    '    If CurLineNo Mod 2 = 0 Then
    '        '上行股道
    '        tmpResult = BaseY + LineInterval * (TolLineNo - (CurLineNo - 1) / 2)
    '    Else
    '        '下行股道
    '        tmpResult = BaseY + LineInterval * CurLineNo / 2
    '    End If
    '    Return tmpResult
    'End Function
End Module
