Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Module modTBScan
    Structure typeODSPubPara
        Dim DiagramSelect As String '当前选中的运行图编号
        Dim sDiagramName As String '运行图名称
        Dim sCurShowListState As String '系统显示状态，单线全图，换乘站图
    End Structure
    Public ODSPubpara As typeODSPubPara
   

    Function ChaStProp(ByVal sString1 As String, ByVal sString2 As String) As String
        'sString1备注,sString2车站性质
        ChaStProp = ""
        If Trim(sString1) = "" Or Trim(sString1) = "无" Then
            If sString2 = "中间站" Then
                ChaStProp = "WZJZ"
            ElseIf sString2 = "线路所" Then
                ChaStProp = "WXLS"
            ElseIf sString2 = "编组站" Then
                ChaStProp = "WBZZ"
            ElseIf sString2 = "区段站" Then
                ChaStProp = "WQDZ"
            Else
                ChaStProp = "WZJZ"
            End If
        ElseIf sString1 = "分岔站" Then
            If sString2 = "中间站" Then
                ChaStProp = "FZJZ"
            ElseIf sString2 = "线路所" Then
                ChaStProp = "FXLS"
            ElseIf sString2 = "编组站" Then
                ChaStProp = "FBZZ"
            ElseIf sString2 = "区段站" Then
                ChaStProp = "FQDZ"
            Else
                ChaStProp = "FZJZ"
            End If
        ElseIf sString1 = "终端站" Then
            If sString2 = "中间站" Then
                ChaStProp = "DZJZ"
            ElseIf sString2 = "线路所" Then
                ChaStProp = "DXLS"
            ElseIf sString2 = "编组站" Then
                ChaStProp = "DBZZ"
            ElseIf sString2 = "区段站" Then
                ChaStProp = "DQDZ"
            Else
                ChaStProp = "DZJZ"
            End If
        ElseIf sString1 = "客技站" Then
            If sString2 = "中间站" Then
                ChaStProp = "KZJZ"
            ElseIf sString2 = "线路所" Then
                ChaStProp = "KXLS"
            ElseIf sString2 = "编组站" Then
                ChaStProp = "KBZZ"
            ElseIf sString2 = "区段站" Then
                ChaStProp = "KQDZ"
            Else
                ChaStProp = "KZJZ"
            End If
        End If
    End Function
   
    '由站名得到车站ID
    Public Function StaNameToStaInfID(ByVal sStaName As String) As Integer
        Dim i As Integer
        For i = 1 To UBound(StationInf)
            If StationInf(i).sStationName = sStaName Then
                StaNameToStaInfID = i
                Exit For
            End If
        Next
    End Function
    Public Function GetMouseMoveTime(ByVal X As Single, ByVal sngTolWidth As Single) As Long
        GetMouseMoveTime = 0
        Dim sngTime As Long
        sngTime = TimeAdd(TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime * 3600, Int((X - TimeTablePara.TimeTableDiagramPara.sngLeftBlank - TimeTablePara.TimeTableDiagramPara.sngStaBlank) * TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime * 3600 / sngTolWidth))
        GetMouseMoveTime = sngTime
    End Function
    Public Function CSGetMouseMoveTime(ByVal X As Single, ByVal sngTolWidth As Single) As Long
        CSGetMouseMoveTime = 0
        Dim sngTime As Long
        sngTime = TimeAdd(CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime * 3600, Int((X - CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank - CSTimeTablePara.TimeTableDiagramPara.sngStaBlank) * CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime * 3600 / sngTolWidth))
        CSGetMouseMoveTime = sngTime
    End Function
    Function ChaLineUse(ByVal sGudaoyt As String, ByVal sFangxiang As String) As Integer
        'sGudaoyt股道用途(正线、到发线)，bHwzy能否货物作业，bChaox能否接超限，sFangxiang方向（上、下行）
        ChaLineUse = 0
        If sGudaoyt.Length >= 3 Then
            If sGudaoyt.Substring(0, 3) = "正线线" Then
                If sFangxiang = "只能上行" Then
                    ChaLineUse = 2000
                ElseIf sFangxiang = "只能下行" Then
                    ChaLineUse = 1000
                ElseIf sFangxiang = "主要方向为上行" Then
                    ChaLineUse = 4000
                ElseIf sFangxiang = "主要方向为下行" Then
                    ChaLineUse = 3000
                Else
                    ChaLineUse = 5000
                End If
                ' If bKyzy = True Then
                ChaLineUse = ChaLineUse + 100
                'End If

            ElseIf sGudaoyt.Substring(0, 3) = "到发线" Then
                If sFangxiang = "只能上行" Then
                    ChaLineUse = 6000
                ElseIf sFangxiang = "只能下行" Then
                    ChaLineUse = 7000
                ElseIf sFangxiang = "主要方向为上行" Then
                    ChaLineUse = 8000
                ElseIf sFangxiang = "主要方向为下行" Then
                    ChaLineUse = 9000
                End If
                ' If bKyzy = True Then
                ChaLineUse = ChaLineUse + 100
                'End If
            ElseIf sGudaoyt = "折返线" Then

            ElseIf sGudaoyt = "存车线" Then

            Else
                ChaLineUse = 0
            End If

        End If
    End Function

    Public Sub ResetCheDiLinkTrainNumber()
        Dim i As Integer
        Dim j As Integer
        Dim nBTrain As Integer
        Dim nFtrain As Integer
        Dim nNtrain As Integer

        For j = 1 To UBound(ChediInfo)
            If UBound(ChediInfo(j).nLinkTrain) > 0 Then
                If UBound(ChediInfo(j).nLinkTrain) = 1 Then
                    If ChediInfo(j).nLinkTrain(1) > 0 And TrainInf(ChediInfo(j).nLinkTrain(1)).Train <> "" Then
                        TrainInf(ChediInfo(j).nLinkTrain(1)).TrainReturn(1) = 0
                        TrainInf(ChediInfo(j).nLinkTrain(1)).TrainReturn(2) = 0
                    End If
                Else
                    For i = 1 To UBound(ChediInfo(j).nLinkTrain)
                        If i = 1 Then
                            nBTrain = 0
                        Else
                            nBTrain = ChediInfo(j).nLinkTrain(i - 1)
                        End If

                        If i = UBound(ChediInfo(j).nLinkTrain) Then
                            nNtrain = 0
                        Else
                            nNtrain = ChediInfo(j).nLinkTrain(i + 1)
                        End If
                        nFtrain = ChediInfo(j).nLinkTrain(i)
                        TrainInf(nFtrain).TrainReturn(1) = nBTrain
                        TrainInf(nFtrain).TrainReturn(2) = nNtrain
                    Next i
                End If
            End If
        Next j
    End Sub

    Public Sub DrawLineInPicture(ByVal nTrain As Integer, _
                                                ByVal rBmpGraphics As Graphics, _
                                                ByVal tmpPen As Pen, _
                                                ByVal intToWidth As Integer, _
                                                ByVal intFirTime As Integer, _
                                                ByVal nTimeWidth As Integer, _
                                                ByVal intLeftBlank As Integer, _
                                                ByVal intStaBlank As Integer, _
                                                ByVal intLeftX As Integer, _
                                                ByVal nifPrintCheCi As Boolean, ByVal nIfPrintXieCheCi As Boolean) ', ByVal sngLeftX As Single, ByVal sngRightX As Single)

        If nTrain = 0 Then Exit Sub
        Dim i As Integer
        Dim X1, X2 As Single
        Dim Y1, Y2 As Single
        Dim X3, Y3 As Single
        Dim X4, Y4 As Single
        Dim intTime1 As Integer
        Dim intTime2 As Integer
        Dim sngToWidth As Single

        Dim beY As Single
        Dim printText As String
        printText = ""
        sngToWidth = (intToWidth - intLeftBlank * 2 - intStaBlank - intLeftX) * 24 / nTimeWidth
        Dim sngLeftX As Single
        Dim sngRightX As Single
        sngLeftX = FormTimeToXCord(intFirTime * 3600, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        sngRightX = FormTimeToXCord(TimeAdd(intFirTime * 3600, nTimeWidth * 3600), intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        If sngRightX = sngLeftX Then
            sngRightX = sngLeftX + intToWidth - 2 * intLeftBlank - intStaBlank - intLeftX
        End If

        Dim f As Font
        Dim tmpBrush As Brush
        f = New Font(TimeTablePara.DiagramStylePara.XieCheCiFontName, TimeTablePara.DiagramStylePara.XieCheCiFontSize)
        If TimeTablePara.DiagramStylePara.XieCheCiFontBold = True Then
            If TimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
            Else
                f = New Font(f, FontStyle.Bold)
            End If
        Else
            If TimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                f = New Font(f, FontStyle.Italic)
            End If
        End If
        tmpBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.XieCheCiFontColor))

        Dim ForX1, ForY1 As Single
        Dim ForX2, ForY2 As Single
        Dim tmpArriTime As Long
        Dim tmpX As Single
        Dim nPrintID As Integer
        If UBound(TrainInf(nTrain).nPassSection) > 0 Then
            If UBound(TrainInf(nTrain).nPassSection) >= 2 Then
                nPrintID = 2
            Else
                nPrintID = 1
            End If
            For i = 1 To UBound(TrainInf(nTrain).nPassSection)
                intTime1 = TrainInf(nTrain).Starting(TrainInf(nTrain).nFirstID(i)) 'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                Y1 = StationInf(TrainInf(nTrain).nFirstID(i)).YPicValue
                intTime2 = TrainInf(nTrain).Arrival(TrainInf(nTrain).nSecondID(i)) 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                Y2 = StationInf(TrainInf(nTrain).nSecondID(i)).YPicValue
                If i = 1 Then
                    If StationInf(TrainInf(nTrain).nFirstID(i)).sStaProperity = "环形终端站" Then
                        If TrainInf(nTrain).TrainReturn(1) <> 0 Then
                            If (nTrain + TrainInf(nTrain).TrainReturn(1)) Mod 2 = 0 Then
                                If TrainInf(nTrain).ComeStation <> TrainInf(TrainInf(nTrain).TrainReturn(1)).NextStation Then
                                    'If GetSystemStaName(TrainInf(nTrain).ComeStation) = GetSystemStaName(TrainInf(TrainInf(nTrain).TrainReturn(1)).NextStation) Then
                                    If StationInf(TrainInf(TrainInf(nTrain).TrainReturn(1)).nSecondID(UBound(TrainInf(TrainInf(nTrain).TrainReturn(1)).nSecondID))).sStaProperity = "环形终端站" Then
                                        tmpArriTime = GetTrainArriTime(TrainInf(nTrain).TrainReturn(1), TrainInf(TrainInf(nTrain).TrainReturn(1)).NextStation)
                                        tmpX = FormTimeToXCord(tmpArriTime, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                        If tmpX <> -1 And tmpX <= sngRightX Then
                                            If nTrain Mod 2 = 0 Then
                                                rBmpGraphics.DrawLine(tmpPen, tmpX - 12, Y1 + 15, tmpX, Y1)
                                            Else
                                                rBmpGraphics.DrawLine(tmpPen, tmpX - 12, Y1 - 15, tmpX, Y1)
                                            End If
                                            If tmpX < X1 Then
                                                rBmpGraphics.DrawLine(tmpPen, tmpX, Y1, X1, Y1)
                                            End If

                                        End If
                                    End If
                                    'End If
                                End If
                            End If
                        End If
                    End If
                End If
                If i = UBound(TrainInf(nTrain).nPassSection) Then
                    '  If nTrain = 2 Then Stop
                    If StationInf(TrainInf(nTrain).nSecondID(i)).sStaProperity = "环形终端站" Then
                        If TrainInf(nTrain).TrainReturn(2) <> 0 Then
                            If (nTrain + TrainInf(nTrain).TrainReturn(2)) Mod 2 = 0 Then
                                If TrainInf(nTrain).NextStation <> TrainInf(TrainInf(nTrain).TrainReturn(2)).ComeStation Then
                                    'If GetSystemStaName(TrainInf(nTrain).NextStation) = GetSystemStaName(TrainInf(TrainInf(nTrain).TrainReturn(2)).ComeStation) Then
                                    If StationInf(TrainInf(TrainInf(nTrain).TrainReturn(2)).nFirstID(1)).sStaProperity = "环形终端站" Then
                                        tmpArriTime = GetTrainStartTime(TrainInf(nTrain).TrainReturn(2), TrainInf(TrainInf(nTrain).TrainReturn(2)).ComeStation)
                                        tmpX = FormTimeToXCord(tmpArriTime, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                        If tmpX <> -1 And tmpX <= sngRightX Then
                                            If nTrain Mod 2 = 0 Then
                                                rBmpGraphics.DrawLine(tmpPen, tmpX + 12, Y2 - 15, tmpX, Y2)
                                            Else
                                                rBmpGraphics.DrawLine(tmpPen, tmpX + 12, Y2 + 15, tmpX, Y2)
                                            End If
                                            If tmpX > X2 Then
                                                rBmpGraphics.DrawLine(tmpPen, tmpX, Y2, X2, Y2)
                                            End If
                                        End If
                                    End If
                                    'End If
                                End If
                            End If
                        End If
                    End If
                End If

                If X1 >= 0 And Y1 >= 0 And X2 >= 0 And Y2 >= 0 Then
                    If X2 >= X1 Then
                        If X1 <= sngRightX Then
                            If i = nPrintID Then
                                If nIfPrintXieCheCi = True Then '打印车次
                                    If TrainInf(nTrain).sPrintTrain.Trim <> "" Then
                                        'If TimeTablePara.TimeTableDiagramPara.sCheCiShowStyle = 1 Then
                                        printText = ChediInfo(CheCiToCheDiID(nTrain)).sCheCiHao & TrainInf(nTrain).sPrintTrain  '.Substring(0, 3)
                                        'Else
                                        '    printText = TrainInf(nTrain).sPrintTrain  '.Substring(0, 3)
                                        'End If
                                        Call WriteStrInLine(rBmpGraphics, printText, f, tmpBrush, X1, Y1, X2, Y2, 4)
                                    End If
                                End If
                            End If

                            If X2 <= sngRightX Then
                                rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                                If Y1 <> beY And i > 1 Then
                                    If nIfPrintXieCheCi = True Then
                                        If TrainInf(nTrain).sPrintTrain.Trim <> "" Then
                                            'If TimeTablePara.TimeTableDiagramPara.sCheCiShowStyle = 1 Then
                                            printText = ChediInfo(CheCiToCheDiID(nTrain)).sCheCiHao & TrainInf(nTrain).sPrintTrain  '.Substring(0, 3)
                                            'Else
                                            '    printText = TrainInf(nTrain).sPrintTrain  '.Substring(0, 3)
                                            'End If
                                            Call WriteStrInLine(rBmpGraphics, printText, f, tmpBrush, X1, Y1, X2, Y2, 4)
                                        End If
                                    End If
                                End If

                                TrainInf(nTrain).sngFirXcoord(i) = X1
                                TrainInf(nTrain).sngFirYcoord(i) = Y1
                                TrainInf(nTrain).sngSecXcoord(i) = X2
                                TrainInf(nTrain).sngSecYcoord(i) = Y2
                                TrainInf(nTrain).bIfPassFenLine = 0
                                beY = Y2
                            Else
                                X3 = sngRightX
                                Y3 = ((X3 - X1) * (Y2 - Y1) / (X2 - X1)) + Y1
                                rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                                ' rBmpGraphics.DrawLine(tmpPen, X3, Y3, X2, Y2)
                                TrainInf(nTrain).sngFirXcoord(i) = X1
                                TrainInf(nTrain).sngFirYcoord(i) = Y1
                                TrainInf(nTrain).sngSecXcoord(i) = X3
                                TrainInf(nTrain).sngSecYcoord(i) = Y3
                            End If
                        End If
                    Else
                        X4 = X1 - sngToWidth
                        Y4 = Y1
                        X3 = intLeftBlank + intStaBlank + intLeftX
                        Y3 = ((X3 - X4) * (Y2 - Y4) / (X2 - X4)) + Y4
                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3 + sngToWidth, Y3)
                        rBmpGraphics.DrawLine(tmpPen, X3, Y3, X2, Y2)
                        TrainInf(nTrain).sngFirXcoord(i) = X1
                        TrainInf(nTrain).sngFirYcoord(i) = Y1
                        TrainInf(nTrain).sngSecXcoord(i) = X2
                        TrainInf(nTrain).sngSecYcoord(i) = Y2
                        TrainInf(nTrain).bIfPassFenLine = 1
                        TrainInf(nTrain).sngPassLineX1Coord = X3 + sngToWidth
                        TrainInf(nTrain).sngPassLineY1Coord = Y3
                        TrainInf(nTrain).sngPassLineX2Coord = X3
                        TrainInf(nTrain).sngPassLineX2Coord = Y3
                        'If TimeTablePara.TimeTableDiagramPara.sCheCiLength = 6 Then
                        '    printText = ChediInfo(CheCiToCheDiID(nTrain)).sPrintCheCiHao & TrainInf(nTrain).sPrintTrain  '.Substring(0, 3)
                        'Else
                        '    printText = TrainInf(nTrain).sPrintTrain  '.Substring(0, 3)
                        'End If
                        'Call WriteStrInLine(rBmpGraphics, printText, f, tmpBrush, X3, Y3, X4, Y4, 4)

                    End If
                End If

                '车站连接横线
                ForX2 = X1
                ForY2 = Y1
                ForY1 = Y1
                tmpArriTime = TrainInf(nTrain).Arrival(TrainInf(nTrain).nFirstID(i))
                ForX1 = FormTimeToXCord(tmpArriTime, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                If ForX1 >= 0 And ForY1 >= 0 And ForX2 >= 0 And ForY2 >= 0 Then
                    If ForX2 >= ForX1 Then
                        If ForX1 <= sngRightX Then
                            If ForX2 <= sngRightX Then
                                If i > 1 Then
                                    rBmpGraphics.DrawLine(tmpPen, ForX1, ForY1, ForX2, ForY2)
                                End If
                            Else
                                X3 = sngRightX
                                Y3 = ((X3 - ForX1) * (ForY2 - ForY1) / (ForX2 - ForX1)) + ForY1
                                rBmpGraphics.DrawLine(tmpPen, ForX1, ForY1, X3, Y3)
                            End If
                        End If
                    Else
                        X4 = ForX1 - sngToWidth
                        Y4 = ForY1
                        X3 = intLeftBlank + intStaBlank + intLeftX
                        Y3 = ((X3 - X4) * (ForY2 - Y4) / (ForX2 - X4)) + Y4
                        rBmpGraphics.DrawLine(tmpPen, ForX1, ForY1, X3 + sngToWidth, Y3)
                        rBmpGraphics.DrawLine(tmpPen, X3, Y3, ForX2, ForY2)
                    End If
                End If

                ForX1 = X2
                ForY1 = Y2
                ForY2 = Y2
                tmpArriTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nSecondID(i))
                ForX2 = FormTimeToXCord(tmpArriTime, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                If ForX1 >= 0 And ForY1 >= 0 And ForX2 >= 0 And ForY2 >= 0 Then
                    If ForX2 >= ForX1 Then
                        If ForX1 <= sngRightX Then
                            If ForX2 <= sngRightX Then
                                If i < UBound(TrainInf(nTrain).nPassSection) Then
                                    rBmpGraphics.DrawLine(tmpPen, ForX1, ForY1, ForX2, ForY2)
                                End If
                            Else
                                X3 = sngRightX
                                Y3 = ((X3 - ForX1) * (ForY2 - ForY1) / (ForX2 - ForX1)) + ForY1
                                rBmpGraphics.DrawLine(tmpPen, ForX1, ForY1, X3, Y3)
                            End If
                        End If
                    Else
                        X4 = ForX1 - sngToWidth
                        Y4 = ForY1
                        X3 = intLeftBlank + intStaBlank + intLeftX
                        Y3 = ((X3 - X4) * (ForY2 - Y4) / (ForX2 - X4)) + Y4
                        rBmpGraphics.DrawLine(tmpPen, ForX1, ForY1, X3 + sngToWidth, Y3)
                        rBmpGraphics.DrawLine(tmpPen, X3, Y3, ForX2, ForY2)
                    End If
                End If
            Next
        End If
    End Sub


    '由时间得到X坐标
    Public Function FormTimeToXCord(ByVal intTime As Integer, ByVal intFirTime As Integer, ByVal intTimeWidth As Integer, ByVal intToWidh As Integer, ByVal intLeftBlank As Integer, ByVal intStaBlank As Integer, ByVal intLeftX As Single) As Single
        If intTime = -1 Then
            FormTimeToXCord = -1
        Else
            Dim intWidth As Integer
            intWidth = intTime - intFirTime * 3600
            If intWidth < 0 Then
                intWidth = intWidth + 24 * 3600
            End If
            Dim sngToWidth As Single
            sngToWidth = intToWidh - intLeftBlank * 2 - intStaBlank - intLeftX
            FormTimeToXCord = intLeftX + intLeftBlank + intStaBlank + intWidth * sngToWidth / (intTimeWidth * 3600)
        End If
    End Function
    '作时间加
    Function TimeAdd(ByVal Time1 As Long, ByVal Time2 As Long) As Long
        Dim temp As Long
        temp = Time1 + Time2
        If temp > 86400 Then
            temp = temp - 86400
        ElseIf temp < 0 Then
            temp = temp + 86400
        End If
        TimeAdd = temp
    End Function
    '得到列车的始发点和到点
    Public Function GetTrainArriOrStartTime(ByVal nTrain As Integer, ByVal nStartOrEnd As Integer, ByVal nArriOrStart As Integer) As Long
        If nArriOrStart = 0 Then '到点
            If nStartOrEnd = 0 Then '始发站
                GetTrainArriOrStartTime = TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(1))
            ElseIf nStartOrEnd = -1 Then '终到站
                GetTrainArriOrStartTime = TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID)))
            Else '其他站
                GetTrainArriOrStartTime = TrainInf(nTrain).Arrival(nStartOrEnd)
            End If
        Else '发点
            If nStartOrEnd = 0 Then '始发站
                GetTrainArriOrStartTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))
            ElseIf nStartOrEnd = -1 Then '终到站
                GetTrainArriOrStartTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID)))
            Else '其他站
                GetTrainArriOrStartTime = TrainInf(nTrain).Starting(nStartOrEnd)
            End If
        End If
    End Function
    '得到该列车在该车站的发点
    Public Function GetTrainStartTime(ByVal nTrain As Int16, ByVal sStaName As String) As Integer
        Dim i As Int16
        For i = 1 To UBound(TrainInf(nTrain).nPathID)
            If StationInf(TrainInf(nTrain).nPathID(i)).sStationName = sStaName Then
                GetTrainStartTime = TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(i))
                Exit For
            End If
        Next
    End Function
    '得到该列车在该车站的到点
    Public Function GetTrainArriTime(ByVal nTrain As Int16, ByVal sStaName As String) As Integer
        Dim i As Int16
        For i = 1 To UBound(TrainInf(nTrain).nPathID)
            If StationInf(TrainInf(nTrain).nPathID(i)).sStationName = sStaName Then
                GetTrainArriTime = TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(i))
                Exit For
            End If
        Next
    End Function
   
    '增加列车，由BaseTraininf中复制过来，并返回复制后列车的ID号
    Public Function AddTrainInformation(ByVal sJiaoLuName As String, ByVal sTrainStyle As String, ByVal sStopScale As String, ByVal sCheci As String) As Integer
        Dim i As Integer
        Dim nWeiNum As Integer
        Dim nBaseId As Integer
        nBaseId = GetBasicTrainInfID(sJiaoLuName)
        If nBaseId = 0 Then
            AddTrainInformation = 0
            Exit Function
        End If

        Dim nTrain As Integer
        nTrain = BasicTrainInf(nBaseId).nUporDown
        '先检查有无事先定义好的维数
        If nTrain Mod 2 = 0 Then
            For i = 1 To UBound(TrainInf)
                If i Mod 2 = 0 Then
                    If TrainInf(i).Train = "" Then
                        nWeiNum = i
                        If sCheci = "" Then
                            sCheci = nWeiNum
                        End If
                        Call CopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale)
                        'Call CopyFenTingBaseTrainInf(nBaseId, nWeiNum)
                        AddTrainInformation = i
                        GoTo sEnd
                    End If
                End If
            Next i
        Else
            For i = 1 To UBound(TrainInf)
                If i Mod 2 <> 0 Then
                    If TrainInf(i).Train = "" Then
                        nWeiNum = i
                        If sCheci = "" Then
                            sCheci = nWeiNum
                        End If
                        Call CopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale)
                        'Call CopyFenTingBaseTrainInf(nBaseId, nWeiNum)
                        AddTrainInformation = i
                        GoTo sEnd
                    End If
                End If
            Next i
        End If

        '如果没有事先定义好的维数，增加列车维数
        If nTrain > 0 Then
            If nTrain Mod 2 = 0 Then
                If UBound(TrainInf) Mod 2 = 0 Then
                    nWeiNum = UBound(TrainInf) + 2
                    ReDim Preserve TrainInf(nWeiNum)
                    If sCheci = "" Then
                        sCheci = nWeiNum
                    End If
                    Call CopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale)
                    'Call CopyFenTingBaseTrainInf(nBaseId, nWeiNum)
                    AddTrainInformation = nWeiNum
                    GoTo sEnd
                Else
                    nWeiNum = UBound(TrainInf) + 1
                    ReDim Preserve TrainInf(nWeiNum)
                    If sCheci = "" Then
                        sCheci = nWeiNum
                    End If
                    Call CopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale)
                    'Call CopyFenTingBaseTrainInf(nBaseId, nWeiNum)
                    AddTrainInformation = nWeiNum
                    GoTo sEnd
                End If
            Else
                If UBound(TrainInf) Mod 2 = 0 Then
                    nWeiNum = UBound(TrainInf) + 1
                    ReDim Preserve TrainInf(nWeiNum)
                    If sCheci = "" Then
                        sCheci = nWeiNum
                    End If
                    Call CopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale)
                    'Call CopyFenTingBaseTrainInf(nBaseId, nWeiNum)
                    AddTrainInformation = nWeiNum
                    GoTo sEnd
                Else
                    nWeiNum = UBound(TrainInf) + 2
                    ReDim Preserve TrainInf(nWeiNum)
                    If sCheci = "" Then
                        sCheci = nWeiNum
                    End If
                    Call CopyMainBaseTrainInf(nBaseId, nWeiNum, sCheci, sTrainStyle, sStopScale)
                    'Call CopyFenTingBaseTrainInf(nBaseId, nWeiNum)
                    AddTrainInformation = nWeiNum
                    GoTo sEnd
                End If
            End If
        End If
sEnd:
        'TrainInf(AddTrainInformation).nTrainTimeKind = sTimeScaleToTimeKind(sKind)
        'TrainInf(AddTrainInformation).sTrainTimeScale = sKind
    End Function

    Structure typeCheDiLinkLine
        Dim nFirTrain As Integer
        Dim nSecTrain As Integer
        Dim nCheDiId As Integer
        Dim LineHeight As Integer
        Dim X1 As Single
        Dim Y1 As Single
        Dim X2 As Single
        Dim Y2 As Single
        Dim DrawString As String
        Dim StringX As Single
        Dim StringY As Single
        Dim nIFPrint As Boolean
        Dim tmpPen As Pen
        Dim tmpFont As Font
        Dim tmpBrush As Brush
        Dim upOrDown As Integer
        Dim sStyle As Integer '类型,0表示矩形，1表示左上，2表示右下，3表示左下，4表示右上
    End Structure
    Public CheDiLinkinf() As typeCheDiLinkLine
    '司机连接信息
    Structure typeDiverLinkLine
        Dim nFirTrain As Integer
        Dim nSecTrain As Integer
        Dim nCheDiId As Integer
        Dim LineHeight As Integer
        Dim X1 As Single
        Dim Y1 As Single
        Dim X2 As Single
        Dim Y2 As Single
        Dim DrawString As String
        Dim StringX As Single
        Dim StringY As Single
        Dim nIFPrint As Boolean
        Dim tmpPen As Pen
        Dim tmpFont As Font
        Dim tmpBrush As Brush
        Dim upOrDown As Integer
        Dim sStyle As Integer '类型,0表示矩形，1表示左上，2表示右下，3表示左下，4表示右上
    End Structure
    Public CSDriverLinkinf() As typeDiverLinkLine


    
    '得到系统车站名
    Public Function GetSystemStaName(ByVal sStaName As String) As String
        GetSystemStaName = sStaName
        Dim i As Integer
        For i = 1 To UBound(StationInf)
            If StationInf(i).sStationName = sStaName Then
                If StationInf(i).sStaProperity = "环形终端站" Then
                    GetSystemStaName = StationInf(i).sPrintStaName
                    Exit For
                End If
            End If
        Next

    End Function
    '得到当前车底线的高度
    Public Function GetCurLineHeight(ByVal nCurId As Integer) As Integer
        Dim i As Integer
        Dim CurX1 As Single
        CurX1 = CheDiLinkinf(nCurId).X1
        Dim nHeight() As Integer
        ReDim nHeight(0)
        For i = 1 To UBound(CheDiLinkinf)
            If i <> nCurId Then
                If CheDiLinkinf(i).nIFPrint = True And CheDiLinkinf(i).Y1 = CheDiLinkinf(nCurId).Y1 And CheDiLinkinf(i).upOrDown = CheDiLinkinf(nCurId).upOrDown Then
                    If CurX1 >= CheDiLinkinf(i).X1 And CurX1 <= CheDiLinkinf(i).X2 Then
                        ReDim Preserve nHeight(UBound(nHeight) + 1)
                        nHeight(UBound(nHeight)) = CheDiLinkinf(i).LineHeight
                    End If
                End If
            End If
        Next
        Dim N As Integer
        N = 0
        Dim ifIn As Integer
        Do
            ifIn = 1
            For i = 1 To UBound(nHeight)
                If nHeight(i) = N Then
                    ifIn = 0
                    Exit For
                End If
            Next
            If ifIn = 1 Then
                GetCurLineHeight = N
                Exit Do
            End If
            N = N + 1
        Loop
        CheDiLinkinf(nCurId).nIFPrint = True
        CheDiLinkinf(nCurId).LineHeight = GetCurLineHeight
    End Function
  
    '用字符串注释在线段 ,参数pos为字符在线段的相对位置,1为左上,2为左中,3为右上,4为左下,5为中下,6为右下
    Public Sub WriteStrInLine(ByVal gTmp As Graphics, ByVal StrTmp As String, ByVal f As Font, ByVal b As Brush, ByVal X1 As Single, ByVal Y1 As Single, ByVal X2 As Single, ByVal Y2 As Single, ByVal Pos As Integer)
        Dim AA As Single
        Dim BB As Single
        Dim CC As Single
        Dim LfX As Single   '线段起点坐标
        Dim LfY As Single
        Dim RtX As Single    '线段终点坐标
        Dim RtY As Single
        Dim sngDistance As Single   '线段长度
        Dim StartX As Single     '字符串开始坐标
        Dim StartY As Single
        Dim SgAng As Single     '旋转角度
        Dim strWidth As Single      '字符串宽度
        Dim strHight As Single      '字符串高度
        Dim nState As Single
        nState = 0
        If X1 < X2 Then
            LfX = X1
            LfY = Y1
            RtX = X2
            RtY = Y2
        ElseIf X1 > X2 Then
            LfX = X2
            LfY = Y2
            RtX = X1
            RtY = Y1
            nState = 1
        Else
            If Y1 <= Y2 Then
                LfX = X1
                LfY = Y1
                RtX = X2
                RtY = Y2
            Else
                LfX = X2
                LfY = Y2
                RtX = X1
                RtY = Y1
                nState = 1
            End If
        End If
        sngDistance = Math.Sqrt((RtX - LfX) ^ 2 + (RtY - LfY) ^ 2)
        AA = RtY - LfY
        BB = LfX - RtY
        CC = RtX * LfY - LfX * RtY
        StartX = LfX
        StartY = LfY
        SgAng = SngAngle(LfX, LfY, LfX + 100, LfY, RtX, RtY) * 180 / Math.PI
        'If nState = 1 Then
        '    SgAng = SgAng + 180
        'End If

        'gTmp.DrawLine(New Pen(Color.DarkBlue, 2), LfX, LfY, RtX, RtY)
        Dim state = gTmp.Save
        strWidth = gTmp.MeasureString(StrTmp.Trim, f).Width
        strHight = gTmp.MeasureString(StrTmp, f).Height
        Dim tmpHeight As Single
        tmpHeight = 2
        Select Case Pos
            Case 1  '左上
                gTmp.TranslateTransform(LfX, LfY)
                gTmp.RotateTransform(SgAng)
                gTmp.TranslateTransform(0, -strHight)
                StartX = 0
                StartY = 0
                gTmp.DrawString(StrTmp, f, b, StartX, StartY)
                gTmp.TranslateTransform(0, strHight)
                gTmp.RotateTransform(-SgAng)
                gTmp.TranslateTransform(-LfX, -LfY)
            Case 2  '中上
                gTmp.TranslateTransform(LfX, LfY)
                gTmp.RotateTransform(SgAng)
                gTmp.TranslateTransform(0, -strHight)
                StartX = (sngDistance - strWidth) / 2
                StartY = 0
                gTmp.DrawString(StrTmp, f, b, StartX, StartY)
                gTmp.TranslateTransform(0, strHight)
                gTmp.RotateTransform(-SgAng)
                gTmp.TranslateTransform(-LfX, -LfY)
            Case 3  '右上
                gTmp.TranslateTransform(LfX, LfY)
                gTmp.RotateTransform(SgAng)
                gTmp.TranslateTransform(0, -strHight)
                StartX = sngDistance - strWidth
                StartY = 0
                gTmp.DrawString(StrTmp, f, b, StartX, StartY)
                gTmp.TranslateTransform(0, strHight)
                gTmp.RotateTransform(-SgAng)
                gTmp.TranslateTransform(-LfX, -LfY)
            Case 4  '左下
                gTmp.TranslateTransform(LfX, LfY)
                gTmp.RotateTransform(SgAng)
                gTmp.TranslateTransform(0, tmpHeight)
                gTmp.DrawString(StrTmp, f, b, 0, 0)
                gTmp.TranslateTransform(0, -tmpHeight)
                gTmp.RotateTransform(-SgAng)
                gTmp.TranslateTransform(-LfX, -LfY)
            Case 5  '中下
                gTmp.TranslateTransform(LfX, LfY)
                gTmp.RotateTransform(SgAng)
                StartX = (sngDistance - strWidth) / 2
                StartY = 0
                gTmp.TranslateTransform(0, tmpHeight)
                gTmp.DrawString(StrTmp, f, b, StartX, StartY)
                gTmp.TranslateTransform(0, -tmpHeight)
                gTmp.RotateTransform(-SgAng)
                gTmp.TranslateTransform(-LfX, -LfY)
            Case 6  '右下
                gTmp.TranslateTransform(LfX, LfY)
                gTmp.RotateTransform(SgAng)
                StartX = sngDistance - strWidth
                StartY = 0
                gTmp.TranslateTransform(0, tmpHeight)
                gTmp.DrawString(StrTmp, f, b, StartX, StartY)
                gTmp.TranslateTransform(0, -tmpHeight)
                gTmp.RotateTransform(-SgAng)
                gTmp.TranslateTransform(-LfX, -LfY)
            Case 7  '左,文字靠左
                If nState = 1 Then
                    gTmp.TranslateTransform(LfX, LfY)
                    gTmp.RotateTransform(SgAng)
                    gTmp.TranslateTransform(0, -strHight / 2)
                    StartX = sngDistance '-strWidth '- sngDistance
                    StartY = 0
                    gTmp.DrawString(StrTmp, f, b, StartX, StartY)
                    gTmp.TranslateTransform(0, strHight / 2)
                    gTmp.RotateTransform(-SgAng)
                    gTmp.TranslateTransform(-LfX, -LfY)
                Else
                    gTmp.TranslateTransform(LfX, LfY)
                    gTmp.RotateTransform(SgAng)
                    gTmp.TranslateTransform(0, -strHight / 2)
                    StartX = -strWidth
                    StartY = 0
                    gTmp.DrawString(StrTmp, f, b, StartX, StartY)
                    gTmp.TranslateTransform(0, strHight / 2)
                    gTmp.RotateTransform(-SgAng)
                    gTmp.TranslateTransform(-LfX, -LfY)
                End If
            Case 8 '右，文字靠右
                If nState = 1 Then
                    gTmp.TranslateTransform(LfX, LfY)
                    gTmp.RotateTransform(SgAng)
                    gTmp.TranslateTransform(0, -strHight / 2)
                    StartX = -strWidth
                    StartY = 0
                    gTmp.DrawString(StrTmp, f, b, StartX, StartY)
                    gTmp.TranslateTransform(0, strHight / 2)
                    gTmp.RotateTransform(-SgAng)
                    gTmp.TranslateTransform(-LfX, -LfY)
                Else
                    gTmp.TranslateTransform(LfX, LfY)
                    gTmp.RotateTransform(SgAng)
                    gTmp.TranslateTransform(0, -strHight / 2)
                    StartX = sngDistance
                    StartY = 0
                    gTmp.DrawString(StrTmp, f, b, StartX, StartY)
                    gTmp.TranslateTransform(0, strHight / 2)
                    gTmp.RotateTransform(-SgAng)
                    gTmp.TranslateTransform(-LfX, -LfY)
                End If
            Case 9 '左中上
                gTmp.TranslateTransform(LfX, LfY)
                gTmp.RotateTransform(SgAng)
                gTmp.TranslateTransform(0, -strHight)
                StartX = (sngDistance - strWidth) / 4
                StartY = 0
                gTmp.DrawString(StrTmp, f, b, StartX, StartY)
                gTmp.TranslateTransform(0, strHight)
                gTmp.RotateTransform(-SgAng)
                gTmp.TranslateTransform(-LfX, -LfY)
        End Select
        gTmp.Restore(state)
    End Sub
    '得到角度
    Public Function SngAngle(ByVal Xa As Single, ByVal Ya As Single, ByVal X1 As Single, ByVal Y1 As Single, ByVal X2 As Single, ByVal Y2 As Single) As Single
        Dim K1 As Single
        Dim K2 As Single
        Dim Tg As Single
        Tg = 99999999
        K1 = (Y1 - Ya) / (X1 - Xa + 0.001)
        K2 = (Y2 - Ya) / (X2 - Xa + 0.001)
        If (1 + K1 * K2) <> 0 Then
            Tg = (K2 - K1) / (1 + K1 * K2)
            SngAngle = Math.Atan(Tg)
        Else
            SngAngle = Math.PI / 2
        End If
    End Function

    '查找满足要求的列车
    Public Function SeekCDLinkTrain(ByVal nTrain As Integer, ByVal nNum As Integer, ByVal sTempTime As Single) As Integer
        'Nnum 表示几种类型的列车，1表示左上行的车，2表示右上，3表示左下，4表示右下
        Dim i As Integer
        Dim sTime As Single
        Dim sStime As Single
        Dim sZheFantime As Single
        SeekCDLinkTrain = 0
        If TrainInf(nTrain).TrainStyle <> "环形车" Then
            Select Case nNum
                Case 1
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).SCheDiLeiXing = TrainInf(nTrain).SCheDiLeiXing Then
                            'If TrainInf(i).nCheDiPuOrNot = 0 Then
                            If i Mod 2 = 0 And TrainInf(i).NextStation = TrainInf(nTrain).ComeStation Then
                                sTime = AddTimeOver24(TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))))
                                If Math.Abs(sTempTime - sTime) <= 100 Then
                                    If TrainInf(i).nZfLimit = 1 Then
                                        SeekCDLinkTrain = i
                                        Exit Function
                                    End If
                                    sStime = AddTimeOver24(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                                    sZheFantime = CDZGetZheFanTime(i, TrainInf(i).SCheDiLeiXing, StationInf(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))).sStationName, TrainInf(i).TrainReturnStyle(2))
                                    If sStime - sTime - sZheFantime >= 0 Then
                                        If TrainInf(i).TrainReturn(2) = 0 Then
                                            SeekCDLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                            'End If
                        End If
                    Next i
                Case 2
                    For i = 1 To UBound(TrainInf)
                        'If i = 462 Then Stop
                        If TrainInf(i).SCheDiLeiXing = TrainInf(nTrain).SCheDiLeiXing Then
                            'If TrainInf(i).nCheDiPuOrNot = 0 Then
                            'If i = 462 Then Stop
                            If i Mod 2 = 0 And TrainInf(i).ComeStation = TrainInf(nTrain).NextStation Then
                                sTime = AddTimeOver24(TrainInf(i).Starting(TrainInf(i).nPathID(1)))
                                If Math.Abs(sTempTime - sTime) <= 100 Then
                                    If TrainInf(i).nZfLimit = 1 Then
                                        SeekCDLinkTrain = i
                                        Exit Function
                                    End If
                                    sStime = AddTimeOver24(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))))
                                    sZheFantime = CDZGetZheFanTime(nTrain, TrainInf(nTrain).SCheDiLeiXing, StationInf(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))).sStationName, TrainInf(nTrain).TrainReturnStyle(2))
                                    If sTime - sStime - sZheFantime >= 0 Then
                                        If TrainInf(i).TrainReturn(1) = 0 Then
                                            SeekCDLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                            'End If
                        End If
                    Next i
                Case 3
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).SCheDiLeiXing = TrainInf(nTrain).SCheDiLeiXing Then
                            'If TrainInf(i).nCheDiPuOrNot = 0 Then
                            If i Mod 2 <> 0 And TrainInf(i).NextStation = TrainInf(nTrain).ComeStation Then
                                sTime = AddTimeOver24(TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))))
                                If Math.Abs(sTempTime - sTime) <= 100 Then
                                    If TrainInf(i).nZfLimit = 1 Then
                                        SeekCDLinkTrain = i
                                        Exit Function
                                    End If
                                    sStime = AddTimeOver24(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                                    sZheFantime = CDZGetZheFanTime(i, TrainInf(i).SCheDiLeiXing, StationInf(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))).sStationName, TrainInf(i).TrainReturnStyle(2))
                                    If sStime - sTime - sZheFantime >= 0 Then
                                        If TrainInf(i).TrainReturn(2) = 0 Then
                                            SeekCDLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                            'End If
                        End If
                    Next i
                Case 4
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).SCheDiLeiXing = TrainInf(nTrain).SCheDiLeiXing Then
                            'If i = 100 Then Stop
                            If i Mod 2 <> 0 And TrainInf(i).ComeStation = TrainInf(nTrain).NextStation Then
                                sTime = AddTimeOver24(TrainInf(i).Starting(TrainInf(i).nPathID(1)))
                                If Math.Abs(sTempTime - sTime) <= 100 Then
                                    If TrainInf(i).nZfLimit = 1 Then
                                        SeekCDLinkTrain = i
                                        Exit Function
                                    End If
                                    sStime = AddTimeOver24(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))))
                                    sZheFantime = CDZGetZheFanTime(nTrain, TrainInf(nTrain).SCheDiLeiXing, StationInf(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))).sStationName, TrainInf(nTrain).TrainReturnStyle(2))
                                    If sTime - sStime - sZheFantime >= 0 Then
                                        If TrainInf(i).TrainReturn(1) = 0 Then
                                            SeekCDLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                            'End If
                        End If
                    Next i
            End Select

        Else '为环形车

            Select Case nNum
                Case 1
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).SCheDiLeiXing = TrainInf(nTrain).SCheDiLeiXing Then
                            If TrainInf(i).NextStation = TrainInf(nTrain).NextStation Then
                                sTime = AddTimeOver24(TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))))
                                If Math.Abs(sTempTime - sTime) <= 100 Then
                                    If TrainInf(i).nZfLimit = 1 Then
                                        SeekCDLinkTrain = i
                                        Exit Function
                                    End If
                                    sStime = AddTimeOver24(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                                    sZheFantime = CDZGetZheFanTime(i, TrainInf(i).SCheDiLeiXing, StationInf(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))).sStationName, TrainInf(i).TrainReturnStyle(2))
                                    If sStime - sTime - sZheFantime >= 0 Then
                                        If TrainInf(i).TrainReturn(2) = 0 Then
                                            SeekCDLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                            'End If
                        End If
                    Next i
                Case 2
                    For i = 1 To UBound(TrainInf)
                        'If i = 462 Then Stop
                        If TrainInf(i).SCheDiLeiXing = TrainInf(nTrain).SCheDiLeiXing Then
                            'If TrainInf(i).nCheDiPuOrNot = 0 Then
                            'If i = 462 Then Stop
                            If TrainInf(i).NextStation = TrainInf(nTrain).NextStation Then
                                sTime = AddTimeOver24(TrainInf(i).Starting(TrainInf(i).nPathID(1)))
                                If Math.Abs(sTempTime - sTime) <= 100 Then
                                    If TrainInf(i).nZfLimit = 1 Then
                                        SeekCDLinkTrain = i
                                        Exit Function
                                    End If
                                    sStime = AddTimeOver24(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))))
                                    sZheFantime = CDZGetZheFanTime(nTrain, TrainInf(nTrain).SCheDiLeiXing, StationInf(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))).sStationName, TrainInf(nTrain).TrainReturnStyle(2))
                                    If sTime - sStime - sZheFantime >= 0 Then
                                        If TrainInf(i).TrainReturn(1) = 0 Then
                                            SeekCDLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                            'End If
                        End If
                    Next i
                Case 3
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).NextStation = TrainInf(nTrain).NextStation Then
                            'If TrainInf(i).nCheDiPuOrNot = 0 Then
                            If TrainInf(i).NextStation = TrainInf(nTrain).NextStation Then
                                sTime = AddTimeOver24(TrainInf(i).Arrival(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))))
                                If Math.Abs(sTempTime - sTime) <= 100 Then
                                    If TrainInf(i).nZfLimit = 1 Then
                                        SeekCDLinkTrain = i
                                        Exit Function
                                    End If
                                    sStime = AddTimeOver24(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)))
                                    sZheFantime = CDZGetZheFanTime(i, TrainInf(i).SCheDiLeiXing, StationInf(TrainInf(i).nPathID(UBound(TrainInf(i).nPathID))).sStationName, TrainInf(i).TrainReturnStyle(2))
                                    If sStime - sTime - sZheFantime >= 0 Then
                                        If TrainInf(i).TrainReturn(2) = 0 Then
                                            SeekCDLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                            'End If
                        End If
                    Next i
                Case 4
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).NextStation = TrainInf(nTrain).NextStation Then
                            'If i = 100 Then Stop
                            If TrainInf(i).NextStation = TrainInf(nTrain).NextStation Then
                                sTime = AddTimeOver24(TrainInf(i).Starting(TrainInf(i).nPathID(1)))
                                If Math.Abs(sTempTime - sTime) <= 100 Then
                                    If TrainInf(i).nZfLimit = 1 Then
                                        SeekCDLinkTrain = i
                                        Exit Function
                                    End If
                                    sStime = AddTimeOver24(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))))
                                    sZheFantime = CDZGetZheFanTime(nTrain, TrainInf(nTrain).SCheDiLeiXing, StationInf(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))).sStationName, TrainInf(nTrain).TrainReturnStyle(2))
                                    If sTime - sStime - sZheFantime >= 0 Then
                                        If TrainInf(i).TrainReturn(1) = 0 Then
                                            SeekCDLinkTrain = i
                                            Exit Function
                                        End If
                                    End If
                                End If
                            End If
                            'End If
                        End If
                    Next i
            End Select

        End If

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
    Structure typeGuDaoJiShuTuJie
        Dim nSta As Integer
        Dim sGuDao() As String
        Dim nUpSta() As Integer
        Dim UpStaYcoord() As Single
        Dim DownStaYcoord() As Single
        Dim GuDaoYcoord() As Single
        Dim nDownSta() As Integer
        Dim nLine As Integer
        Dim sCurSeleteState As String '当前选中的状态，中间停站/始发折返
        Dim CurSelectedLineX1 As Single '修改股道时的X，Y坐标
        Dim CurSelectedLineY1 As Single
        Dim CurSelectedLineX2 As Single
        Dim CurSelectedLineY2 As Single

        'gujianghe begin
        Dim nUpSwitches() As String '上行道岔号码
        Dim nUpSwitchesYCoord() As Single
        Dim nDownSwitches() As String '下行道岔号码
        Dim nDownSwitchesYCoord() As Single
        Dim nTotleY As Long '最高Y值
        'gujianghe end

    End Structure
    Public GuDaoJishutujie As typeGuDaoJiShuTuJie
  
    '得到列车在区间的始发的坐标 
    Public Function GetTrainrStartXCoord(ByVal nTrain As Integer, ByVal nStaID As Integer) As Single
        GetTrainrStartXCoord = FormTimeToXCord(GetTrainArriOrStartTime(nTrain, nStaID, 1), TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
    End Function

    '得到列车在区间的终到的坐标
    Public Function GetTrainArriXCoord(ByVal nTrain As Integer, ByVal nStaID As Integer) As Single
        GetTrainArriXCoord = FormTimeToXCord(GetTrainArriOrStartTime(nTrain, nStaID, 0), TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
    End Function
    Public Function AddTimeOver24(ByVal sTime As Long) As Single
        If sTime >= 0 And sTime < TimeTablePara.TimeTableDiagramPara.intCompareFirstTime Then
            AddTimeOver24 = 24 * 3600.0# + sTime
        Else
            AddTimeOver24 = sTime
        End If
    End Function
    Public Function CSAddTimeOver24(ByVal sTime As Long) As Single
        If sTime >= 0 And sTime < CSTimeTablePara.TimeTableDiagramPara.intCompareFirstTime Then
            CSAddTimeOver24 = 24 * 3600.0# + sTime
        Else
            CSAddTimeOver24 = sTime
        End If
    End Function

    '得到列车的折返时间
    Public Function CDZGetZheFanTime(ByVal nTrain As Integer, ByVal SCheDiLeiXing As String, ByVal sZheFanSta As String, ByVal sZheFanFangShi As String) As Single
        CDZGetZheFanTime = GetZheFanTime(SCheDiLeiXing, sZheFanSta, sZheFanFangShi)
    End Function
    '查找折返时间
    Public Function GetZheFanTime(ByVal SCheDiLeiXing As String, ByVal sZheFanSta As String, ByVal sZheFanFangShi As String) As Single
        Dim i As Integer
        GetZheFanTime = 0
        For i = 1 To UBound(ChediZhefanBiaozhunInfo)
            If ChediZhefanBiaozhunInfo(i).sZhefanStation = sZheFanSta And ChediZhefanBiaozhunInfo(i).SCheDiLeiXing = SCheDiLeiXing Then
                Select Case sZheFanFangShi
                    Case "立即折返"
                        GetZheFanTime = ChediZhefanBiaozhunInfo(i).lLijiZhefanTime
                    Case "站前折返"
                        GetZheFanTime = ChediZhefanBiaozhunInfo(i).lZhanQianTime
                    Case "站后折返"
                        GetZheFanTime = ChediZhefanBiaozhunInfo(i).lZhanHouTime
                End Select
                Exit For
            End If
        Next i
    End Function
    '找列车
    Public Function SeekTrainNumberByXYCoord(ByVal X As Single, ByVal Y As Single, ByVal Mark1 As Single, ByVal Mark2 As Single) As Integer
        Dim i As Integer
        Dim k As Integer
        Dim ArriveTime As Long
        Dim StartTime As Long

        Dim sngX1 As Single
        Dim sngY1 As Single
        Dim sngX2 As Single
        Dim sngY2 As Single

        Dim sJGWidth As Single
        sJGWidth = 1000000

        Dim TgAlpha As Single
        Dim StandardX As Single
        Dim AllowWidth As Single
        SeekTrainNumberByXYCoord = 0


        For k = 1 To UBound(TrainInf)
            If TrainInf(k).Train <> "" Then

                AllowWidth = -1
                ReDim CDJLPrintPlace(0)

                For i = 1 To UBound(TrainInf(k).nPathID)
                    ArriveTime = TrainInf(k).Arrival(TrainInf(k).nPathID(i))
                    StartTime = TrainInf(k).Starting(TrainInf(k).nPathID(i))
                    ReDim Preserve CDJLPrintPlace(UBound(CDJLPrintPlace) + 1)
                    CDJLPrintPlace(UBound(CDJLPrintPlace)).sngX1 = FormTimeToXCord(ArriveTime, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                    CDJLPrintPlace(UBound(CDJLPrintPlace)).sngY1 = StationInf(TrainInf(k).nPathID(i)).YPicValue
                    CDJLPrintPlace(UBound(CDJLPrintPlace)).sngX2 = FormTimeToXCord(StartTime, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                    CDJLPrintPlace(UBound(CDJLPrintPlace)).sngY2 = StationInf(TrainInf(k).nPathID(i)).YPicValue
                Next i

                For i = 1 To UBound(CDJLPrintPlace) - 1
                    sngX1 = CDJLPrintPlace(i).sngX2
                    sngY1 = CDJLPrintPlace(i).sngY2
                    sngX2 = CDJLPrintPlace(i + 1).sngX1
                    sngY2 = CDJLPrintPlace(i + 1).sngY1


                    If k Mod 2 <> 0 Then '下行
                        'If X >= sngX1 And X <= sngX2 Then
                        If Y >= sngY1 And Y <= sngY2 Then
                            TgAlpha = Math.Abs((sngY2 - sngY1) / (sngX2 - sngX1))
                            If TgAlpha <> 0 Then
                                StandardX = (Y - sngY1) / TgAlpha + sngX1
                                AllowWidth = Math.Abs(StandardX - X)
                            End If
                        End If
                        'End If
                    Else
                        ' If X >= sngX1 And X <= sngX2 Then
                        If Y >= sngY2 And Y <= sngY1 Then
                            TgAlpha = (sngY1 - sngY2) / (sngX2 - sngX1)
                            If TgAlpha <> 0 Then
                                StandardX = (sngY1 - Y) / TgAlpha + sngX1
                                AllowWidth = Math.Abs(StandardX - X)
                            End If
                            ' End If
                        End If
                    End If

                    If AllowWidth >= 0 Then
                        If AllowWidth < sJGWidth Then
                            sJGWidth = AllowWidth
                            SeekTrainNumberByXYCoord = k
                        End If
                    End If
                Next i
            End If
        Next k

    End Function
    '找列车
    Public Function CSSeekTrainNumberByXYCoord(ByVal X As Single, ByVal Y As Single, ByVal Mark1 As Single, ByVal Mark2 As Single) As Integer
        Dim k As Integer
        Dim ArriveTime As Long
        Dim StartTime As Long

        Dim sngX1 As Single
        Dim sngY1 As Single
        Dim sngX2 As Single
        Dim sngY2 As Single

        Dim sJGWidth As Single
        sJGWidth = 1000000

        Dim TgAlpha As Single
        Dim StandardX As Single
        Dim AllowWidth As Single
        CSSeekTrainNumberByXYCoord = 0
        If CSTrainsAndDrivers.CSLinkTrains Is Nothing = False Then
            For k = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
                If CSTrainsAndDrivers.CSLinkTrains(k).CSTrainID <> 0 Then

                    AllowWidth = -1
                    ReDim CDJLPrintPlace(0)


                    ArriveTime = CSTrainsAndDrivers.CSLinkTrains(k).StartTime
                    StartTime = CSTrainsAndDrivers.CSLinkTrains(k).EndTime
                    'ReDim Preserve CDJLPrintPlace(UBound(CDJLPrintPlace) + 1)
                    'CDJLPrintPlace(UBound(CDJLPrintPlace)).sngX1 = FormTimeToXCord(ArriveTime, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                    'CDJLPrintPlace(UBound(CDJLPrintPlace)).sngY1 = StationInf(CSLinkTrain(k).nPathID(i)).YPicValue
                    'CDJLPrintPlace(UBound(CDJLPrintPlace)).sngX2 = FormTimeToXCord(StartTime, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                    'CDJLPrintPlace(UBound(CDJLPrintPlace)).sngY2 = StationInf(CSLinkTrain(k).nPathID(i)).YPicValue


                    'For i = 1 To UBound(CDJLPrintPlace) - 1
                    sngX1 = FormTimeToXCord(ArriveTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                    sngY1 = CSLinkStationInf(CSTrainsAndDrivers.CSLinkTrains(k).StartStaID).YPicValue
                    sngX2 = FormTimeToXCord(StartTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX)
                    sngY2 = CSLinkStationInf(CSTrainsAndDrivers.CSLinkTrains(k).EndStaID).YPicValue


                    If CSTrainsAndDrivers.CSLinkTrains(k).UpOrDown = 1 Then '下行
                        'If X >= sngX1 And X <= sngX2 Then
                        If Y >= sngY1 And Y <= sngY2 Then
                            TgAlpha = Math.Abs((sngY2 - sngY1) / (sngX2 - sngX1))
                            If TgAlpha <> 0 Then
                                StandardX = (Y - sngY1) / TgAlpha + sngX1
                                AllowWidth = Math.Abs(StandardX - X)
                            End If
                        End If
                        'End If
                    Else
                        ' If X >= sngX1 And X <= sngX2 Then
                        If Y >= sngY2 And Y <= sngY1 Then
                            TgAlpha = (sngY1 - sngY2) / (sngX2 - sngX1)
                            If TgAlpha <> 0 Then
                                StandardX = (sngY1 - Y) / TgAlpha + sngX1
                                AllowWidth = Math.Abs(StandardX - X)
                            End If
                            ' End If
                        End If
                    End If

                    If AllowWidth >= 0 Then
                        If AllowWidth < sJGWidth Then
                            sJGWidth = AllowWidth
                            CSSeekTrainNumberByXYCoord = k
                        End If
                    End If
                    'Next i
                End If
            Next k
        End If
    End Function
    ''加入或减去选中的列车
    'Public Sub AddSelectTrains(ByVal nSelectTrain As Integer)
    '    Dim i As Integer
    '    Dim sSelect As New System.Collections.Generic.List(Of Integer)
    '    sSelect.Clear()
    '    For i = 1 To UBound(TimeTablePara.nPubTrains)
    '        sSelect.Add(TimeTablePara.nPubTrains(i))
    '    Next
    '    Dim nIFIn As Boolean
    '    nIFIn = False
    '    For i = 1 To UBound(TimeTablePara.nPubTrains)
    '        If TimeTablePara.nPubTrains(i) = nSelectTrain Then
    '            sSelect.Remove(nSelectTrain)
    '            nIFIn = True
    '            Exit For
    '        End If
    '    Next
    '    If nIFIn = False Then
    '        sSelect.Add(nSelectTrain)
    '    End If
    '    ReDim TimeTablePara.nPubTrains(sSelect.Count)
    '    For i = 1 To UBound(TimeTablePara.nPubTrains)
    '        TimeTablePara.nPubTrains(i) = sSelect.Item(i - 1)
    '    Next
    'End Sub
    ''在标头上显示列车信息
    'Public Sub ShowLabInfor(ByVal nTrain As Integer, ByVal labName As Label)
    '    If nTrain > 0 Then
    '        Dim nCdid As Integer
    '        Dim sStime As String
    '        Dim sEtime As String

    '        nCdid = CheCiToCheDiID(nTrain)

    '        sStime = SecondToHour(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1)), 0)
    '        sEtime = SecondToHour(TrainInf(nTrain).Arrival(TrainInf(nTrain).nPathID(UBound(TrainInf(nTrain).nPathID))), 0)

    '        Dim nBeTrain As Integer
    '        Dim nAfTrain As Integer
    '        Dim sBeJGtime As String
    '        Dim sAfJGtime As String

    '        sBeJGtime = "无列车"
    '        sAfJGtime = "无列车"

    '        '查找当前列车前面的列车

    '        nAfTrain = nFindAfterTrain(nTrain, TrainInf(nTrain).nPathID(1), SectionInf(TrainInf(nTrain).nPassSection(1)).sSecName)
    '        If nAfTrain <> 0 Then
    '            sAfJGtime = SecondToMinute(AddLitterTime(TrainInf(nAfTrain).Starting(TrainInf(nAfTrain).nPathID(1))) - AddLitterTime(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))))
    '        End If

    '        nBeTrain = nFindBeforeTrain(nTrain, TrainInf(nTrain).nPathID(1), SectionInf(TrainInf(nTrain).nPassSection(1)).sSecName)
    '        If nBeTrain <> 0 Then
    '            sBeJGtime = SecondToMinute(AddLitterTime(TrainInf(nTrain).Starting(TrainInf(nTrain).nPathID(1))) - AddLitterTime(TrainInf(nBeTrain).Starting(TrainInf(nBeTrain).nPathID(1))))
    '        End If
    '        labName.Text = " ID:" & nTrain & " 车次:" & TrainInf(nTrain).Train & " 输出车次：" & TrainInf(nTrain).sPrintTrain & _
    '      " |运行标尺:" & TrainInf(nTrain).sRunScaleName & " |停站标尺:" & TrainInf(nTrain).sStopSclaeName & _
    '     " |始发时间:" & sStime & " |终到时间:" & sEtime & " |前行间隔:" & sBeJGtime & " |后行间隔:" & sAfJGtime '" |列车类型:" & TrainInf(nTrainNumberYXT).SCheDiLeiXing & ChediInfo(nCdid).sCheDiID & ",  列车径路：" & TrainInf(nTrainNumberYXT).sTrainPath
    '    Else
    '        labName.Text = "在此显示相关信息"
    '    End If
    'End Sub
    '根据Y坐标得到区间编号
    Public Function GetSecIDFormYcord(ByVal ncurYcord As Integer, ByVal nTrain As Integer) As Integer
        GetSecIDFormYcord = 0
        If nTrain > 0 Then
            Dim i As Integer
            Dim nUp As Integer
            Dim nDown As Integer
            For i = 1 To UBound(TrainInf(nTrain).nPassSection)
                nUp = Math.Max(StationInf(TrainInf(nTrain).nFirstID(i)).YPicValue, StationInf(TrainInf(nTrain).nSecondID(i)).YPicValue)
                nDown = Math.Min(StationInf(TrainInf(nTrain).nFirstID(i)).YPicValue, StationInf(TrainInf(nTrain).nSecondID(i)).YPicValue)
                If ncurYcord > nDown And ncurYcord < nUp Then
                    GetSecIDFormYcord = TrainInf(nTrain).nPassSection(i)
                    Exit For
                End If
            Next
        End If
    End Function
    '根据Y坐标得到区间编号
    Public Function CSGetSecIDFormYcord(ByVal ncurYcord As Integer, ByVal nTrain As Integer) As Integer
        CSGetSecIDFormYcord = 0
        If nTrain > 0 Then
            Dim i As Integer
            Dim nUp As Integer
            Dim nDown As Integer
            For i = 1 To UBound(TrainInf(nTrain).nPassSection)
                nUp = Math.Max(StationInf(TrainInf(nTrain).nFirstID(i)).YPicValue, StationInf(TrainInf(nTrain).nSecondID(i)).YPicValue)
                nDown = Math.Min(StationInf(TrainInf(nTrain).nFirstID(i)).YPicValue, StationInf(TrainInf(nTrain).nSecondID(i)).YPicValue)
                If ncurYcord > nDown And ncurYcord < nUp Then
                    CSGetSecIDFormYcord = TrainInf(nTrain).nPassSection(i)
                    Exit For
                End If
            Next
        End If
    End Function
    '根据Y坐标得到车站编号
    Public Function GetStaIDFormYcord(ByVal ncurYcord As Integer, ByVal nTrain As Integer) As Integer
        GetStaIDFormYcord = 0
        If nTrain > 0 Then
            Dim i As Integer
            Dim nUp As Integer
            Dim nMin As Integer
            Dim nCurSta As Integer
            nMin = 100000000
            For i = 1 To UBound(TrainInf(nTrain).nPathID)
                nUp = Math.Abs(StationInf(TrainInf(nTrain).nPathID(i)).YPicValue - ncurYcord)
                If nUp < nMin Then
                    nCurSta = TrainInf(nTrain).nPathID(i)
                    nMin = nUp
                End If
            Next
            GetStaIDFormYcord = nCurSta
        End If
    End Function
    
    'Public Function SeekJiShuTuJieTrainNumberByXYCoord(ByVal X As Single, ByVal Y As Single, ByVal sStaName As String) As Integer
    '    Dim X1, Y1, X2, Y2 As Single
    '    Dim sngCurY As Single
    '    Dim i As Integer
    '    Dim nSta As Integer
    '    ' Dim nGuDao As Integer
    '    'nGuDao = GetCurGuDaoBianHaoFromYCoord(Y)
    '    'Y1 = GuDaoJishutujie.GuDaoYcoord(nGuDao)
    '    'Y2 = GuDaoJishutujie.GuDaoYcoord(nGuDao) + 50
    '    nSta = StaNameToStaInfID(sStaName)
    '    For i = 1 To UBound(TrainInf)
    '        If TrainInf(i).Train <> "" Then
    '            X1 = FormTimeToXCord(TrainInf(i).Arrival(nSta), TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
    '            X2 = FormTimeToXCord(TrainInf(i).Starting(nSta), TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
    '            sngCurY = GetCurGuDaoYCood(TrainInf(i).StopLine(nSta))
    '            Y1 = sngCurY - JishutujieVerInterval / 4
    '            Y2 = sngCurY + JishutujieVerInterval / 4
    '            'If X2 < X1 Then
    '            '    X2 = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth - TimeTablePara.TimeTableDiagramPara.sngLeftBlank
    '            'End If
    '            If X >= X1 And X <= X2 Then
    '                If Y <= Y2 And Y >= Y1 Then
    '                    GuDaoJishutujie.sCurSeleteState = "车站股道"
    '                    SeekJiShuTuJieTrainNumberByXYCoord = i
    '                    Exit For
    '                End If
    '            End If

    '            If SeekJiShuTuJieTrainNumberByXYCoord = 0 Then
    '                If TrainInf(i).ComeStation = sStaName Then '查始发折返
    '                    X1 = FormTimeToXCord(TrainInf(i).sStartZFArrival, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
    '                    X2 = FormTimeToXCord(TrainInf(i).sStartZFStarting, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngPubLeftX)
    '                    sngCurY = GetCurGuDaoYCood(TrainInf(i).sStartZFLine)
    '                    Y1 = sngCurY - JishutujieVerInterval / 4
    '                    Y2 = sngCurY + JishutujieVerInterval / 4
    '                    'If X2 < X1 Then
    '                    '    X2 = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth - TimeTablePara.TimeTableDiagramPara.sngLeftBlank
    '                    'End If

    '                    If X >= X1 And X <= X2 Then
    '                        If Y <= Y2 And Y >= Y1 Then
    '                            GuDaoJishutujie.sCurSeleteState = "始发折返"
    '                            SeekJiShuTuJieTrainNumberByXYCoord = i
    '                            Exit For
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    Next
    'End Function
    '根据Y坐标查找股道编号
    Public Function GetCurGuDaoBianHaoFromYCoord(ByVal Y As Single) As Integer
        Dim i As Integer
        GetCurGuDaoBianHaoFromYCoord = 0
        For i = 1 To UBound(GuDaoJishutujie.sGuDao)
            If Y > GuDaoJishutujie.GuDaoYcoord(i) And Y < GuDaoJishutujie.GuDaoYcoord(i) + JishutujieVerInterval / 2 Then
                GetCurGuDaoBianHaoFromYCoord = i 'GuDaoJishutujie.sGuDao(i)
                Exit For
            End If
        Next
    End Function
    Structure typeCDJLPrintPlace
        Dim sngX1 As Single
        Dim sngY1 As Single
        Dim sngX2 As Single
        Dim sngY2 As Single
    End Structure
    Public CDJLPrintPlace() As typeCDJLPrintPlace
    Public JishutujieVerInterval As Single
    '查找当前股道的Y坐标
    'gujianghe begin
    Public Function GetCurGuDaoYCood(ByVal sGuDaoNum As String) As Single
        Dim i As Integer
        GetCurGuDaoYCood = 0
        For i = 1 To UBound(GuDaoJishutujie.sGuDao)
            If GuDaoJishutujie.sGuDao(i) = sGuDaoNum Then
                GetCurGuDaoYCood = GuDaoJishutujie.GuDaoYcoord(i) + JishutujieVerInterval / 4
                Exit For
            End If
        Next
    End Function 'gujianghe end 
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
        If Val(HStr) > 24 Then
            HStr = HStr - 24
        End If

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
    '*********************************************************************
    Public Sub LoadDiagramData(ByVal sState As String)
    

        Progress.ProgressForm.StartProgress(11, "正在加载运行图相关数据，请稍候...")
        Progress.ProgressForm.PerformStep()
        Call OCInitSystemVariant(0)
        Progress.ProgressForm.PerformStep()
        Call OCreadNetData()
        Progress.ProgressForm.PerformStep()
        Call OCIniteDiagramPicViraient("列车运行图")
        Progress.ProgressForm.PerformStep()
        Call OCreadNetStaAndSecData()
        Progress.ProgressForm.PerformStep()
        Call OCInputStationGudaoAndJinLuInfByDAO()
        Progress.ProgressForm.PerformStep()
        Call OCInputChediZhefanDataAndCheDiScaleInf()
        Progress.ProgressForm.PerformStep()
        Call OCInputChediAndTrainJianGeData(sState)
        Progress.ProgressForm.PerformStep()
        Call OCReadSKBStaSeqData()
        Progress.ProgressForm.PerformStep()
        Call OCReadBaseTrainInf()
        Progress.ProgressForm.PerformStep()
        If sState <> "新建运行图" Then
            Call OCReadTrainAndTimeTableInf(ODSPubpara.DiagramSelect)
            Progress.ProgressForm.PerformStep()
        End If

        'Call InputStationAndSectionDataToCADStainf() '车站平面图信息 '********需要导入*********
        'Progress.ProgressForm.PerformStep()
        'Call InputAllDataToCADstaInfNoprobar()
        Progress.ProgressForm.EndProgress()

        'Catch ex As Exception
        '    Progress.ProgressForm.EndProgress()
        'End Try

    End Sub
    Public Sub OCreadNetData()
        ReDim NetInf.Line(0)
        Dim i, j As Integer
        Dim sqlstr As String = ""
        Try
            'LocalDataSet.TMS_LINEINFO.Clear()
            'LocalDataSet.Fill("TMS_LINEINFO", "TRAINDIAGRAMID='" & ODSPubpara.DiagramSelect & "'order by LISTNO")
            sqlstr = "SELECT * FROM TMS_LINEINFO WHERE TRAINDIAGRAMID='" & ODSPubpara.DiagramSelect & "'order by LISTNO"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)

            ReDim NetInf.Line(tempTable.Rows.Count)
            For i = 1 To tempTable.Rows.Count
                NetInf.Line(i).sName = tempTable.Rows(i - 1).ItemArray(2).ToString.Trim
                NetInf.Line(i).sngLength = Val(tempTable.Rows(i - 1).ItemArray(6))
                NetInf.Line(i).sMemo = tempTable.Rows(i - 1).ItemArray(7).ToString
                NetInf.Line(i).intSeq = CInt(tempTable.Rows(i - 1).ItemArray(1))
                NetInf.Line(i).sEngName = tempTable.Rows(i - 1).ItemArray(4).ToString.Trim
                NetInf.Line(i).sBrriName = tempTable.Rows(i - 1).ItemArray(3).ToString.Trim
                ReDim NetInf.Line(i).Station(0)
                ReDim NetInf.Line(i).Section(0)
            Next

            '车站信息
            'LocalDataSet.TMS_STATIONINFO.Clear()
            'LocalDataSet.Fill("TMS_STATIONINFO", "TRAINDIAGRAMID='" & ODSPubpara.DiagramSelect & "'order by LINENAME,DOWNSEQUENCE")

            sqlstr = "SELECT * FROM TMS_STATIONINFO WHERE TRAINDIAGRAMID='" & ODSPubpara.DiagramSelect & "'order by LINENAME,DOWNSEQUENCE"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            Dim nID As Integer
            nID = 0
            ReDim StaInf(0)
            For i = 1 To UBound(NetInf.Line)
                For j = 1 To tempTable.Rows.Count
                    If Trim(tempTable.Rows(j - 1).ItemArray(1).ToString) = NetInf.Line(i).sName Then
                        nID = nID + 1
                        ReDim Preserve NetInf.Line(i).Station(UBound(NetInf.Line(i).Station) + 1)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).nDownID = CInt(tempTable.Rows(j - 1).ItemArray(3))
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sStaName = Trim(tempTable.Rows(j - 1).ItemArray(2).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sPrintStaName = Trim(tempTable.Rows(j - 1).ItemArray(4).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sEngName = Trim(tempTable.Rows(j - 1).ItemArray(5).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sStaStyle = Trim(tempTable.Rows(j - 1).ItemArray(8).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sEngBrriName = Trim(tempTable.Rows(j - 1).ItemArray(6).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sStaCode = Trim(tempTable.Rows(j - 1).ItemArray(14).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sSingleOrDoubleLine = Trim(tempTable.Rows(j - 1).ItemArray(7).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sStaProperty = Trim(tempTable.Rows(j - 1).ItemArray(9).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sngXcoord = Val(tempTable.Rows(j - 1).ItemArray(11).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sngYcoord = Val(tempTable.Rows(j - 1).ItemArray(12).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sMemo = Trim(tempTable.Rows(j - 1).ItemArray(13).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sPicPath = Trim(tempTable.Rows(j - 1).ItemArray(10).ToString)
                        ReDim Preserve StaInf(UBound(StaInf) + 1)
                        StaInf(UBound(StaInf)).sStaName = Trim(tempTable.Rows(j - 1).ItemArray(2).ToString)
                        StaInf(UBound(StaInf)).sEngName = Trim(tempTable.Rows(j - 1).ItemArray(5).ToString)
                        StaInf(UBound(StaInf)).sAtLine = NetInf.Line(i).sName
                        StaInf(UBound(StaInf)).nDownID = nID
                    End If
                Next
            Next
            Dim ifSame As Int16
            ReDim StaInfNotSame(0)
            For i = 1 To UBound(StaInf)
                ifSame = 0
                For j = 1 To UBound(StaInfNotSame)
                    If StaInf(i).sStaName = StaInfNotSame(j).sStaName Then
                        ifSame = 1
                        Exit For
                    End If
                Next
                If ifSame = 0 Then
                    ReDim Preserve StaInfNotSame(UBound(StaInfNotSame) + 1)
                    StaInfNotSame(UBound(StaInfNotSame)).sStaName = StaInf(i).sStaName
                    StaInfNotSame(UBound(StaInfNotSame)).sEngName = StaInf(i).sEngName
                    StaInfNotSame(UBound(StaInfNotSame)).sAtLine = StaInf(i).sAtLine
                    StaInfNotSame(UBound(StaInfNotSame)).nDownID = StaInf(i).nDownID
                End If
            Next
            'LocalDataSet.TMS_SECTIONINFO.Clear()
            'LocalDataSet.Fill("TMS_SECTIONINFO", "TRAINDIAGRAMID='" & ODSPubpara.DiagramSelect & "'order by LINENAME,SECTIONSEQ")

            sqlstr = "SELECT * FROM TMS_SECTIONINFO WHERE TRAINDIAGRAMID='" & ODSPubpara.DiagramSelect & "'order by LINENAME,SECTIONSEQ"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            For j = 1 To UBound(NetInf.Line)
                For i = 1 To tempTable.Rows.Count
                    If Trim(tempTable.Rows(i - 1).ItemArray(1).ToString) = NetInf.Line(j).sName Then
                        ReDim Preserve NetInf.Line(j).Section(UBound(NetInf.Line(j).Section) + 1)
                        NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecName = Trim(tempTable.Rows(i - 1).ItemArray(3).ToString)
                        NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).nID = CInt(tempTable.Rows(i - 1).ItemArray(2))
                        NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecFirName = Trim(tempTable.Rows(i - 1).ItemArray(4).ToString)
                        NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecSecName = Trim(tempTable.Rows(i - 1).ItemArray(5).ToString)
                        NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecUpLength = Trim(tempTable.Rows(i - 1).ItemArray(6).ToString)
                        NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecDownLength = Trim(tempTable.Rows(i - 1).ItemArray(7).ToString)
                        NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecCode = OCGetStaCodeFromStaName(NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecSecName) & "-" & _
                                                                                               OCGetStaCodeFromStaName(NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecFirName)
                    End If
                Next i
            Next
        Catch
        End Try
    End Sub

    '系统变量初始化
    Public Sub OCInitSystemVariant(ByVal nState As Integer)
        Dim i As Integer
        If nState = 0 Then
            ReDim ChediInfo(0)
        Else
            For i = 1 To UBound(ChediInfo)
                ReDim ChediInfo(i).nLinkTrain(0)
            Next
        End If

        ReDim TrainInf(0)
        ReDim CopyTrainInf(0)
        'ReDim ChediMultiJLInfo(0)
        'ReDim TrainErrInf(0)

        TimeTablePara.nMaxUndoID = 20
        ReDim UndoInf(TimeTablePara.nMaxUndoID)
        For i = 1 To UBound(UndoInf)
            UndoInf(i).nXuHao = i
            ReDim UndoInf(i).Traininf(0)
            ReDim UndoInf(i).CheDiInf(0)
        Next
        UndoSeq.nCurUndoID = 1
        TimeTablePara.nPubTrain = 0
        TimeTablePara.nPubCheDi = 0
        ReDim TimeTablePara.nPubTrains(0)
        'nJGFuYu = 30
        TimeTablePara.sCurDiagramState = DiagramState.运行图
        TimeTablePara.sPubTrainStrainDraw = TrainStrainDraw.无约束
        TimeTablePara.sDrawLineStyle = "不能越行"
        'TimeTablePara.sPubCurSkbName = ODSPubpara.sDiagramName
        TimeTablePara.nStaJiShuTuJieSeletedState = 0
        TimeTablePara.BifAutoBianCheCi = False
        TimeTablePara.sErrorShowStyle = "显示全部"
        'nMoveStepTime = 30 '运行线调整时移动变量,不够时移动30s
        'TdrawLinePara.sMaxMoveTime = 3600
    End Sub
    '自定义底图变量
    Public Sub OCIniteDiagramPicViraient(ByVal sDiaStyle As String)
        Dim sqlstr As String = ""
        sqlstr = "SELECT * FROM TMS_TIMETABLEPARAMETER WHERE TrainDiagramID='" & ODSPubpara.DiagramSelect & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        Dim i As Integer
        For i = 1 To tempTable.Rows.Count
            Select Case Trim(tempTable.Rows(i - 1).Item("ParaName"))
                Case "底图起始时间"
                    TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime = 4 ' tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "底图显示时间宽"
                    TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime = 24 'tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "时间比较起始时间"
                    TimeTablePara.TimeTableDiagramPara.intCompareFirstTime = HourToSecond(Val(tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim))
                Case "底图宽"
                    TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = 8000 'tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "底图高"
                    If sDiaStyle = "换乘站运行图" Then
                        TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = 400 ' tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                    Else
                        TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = 800 ' tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                    End If
                Case "底图时分格式"
                    TimeTablePara.TimeTableDiagramPara.strTimeFormat = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "底图上下空白高度"
                    TimeTablePara.TimeTableDiagramPara.sngtopBlank = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "底图时间空白高度"
                    TimeTablePara.TimeTableDiagramPara.sngTimeBlank = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "底图左右空白高度"
                    TimeTablePara.TimeTableDiagramPara.sngLeftBlank = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "底图车站空白宽度"
                    TimeTablePara.TimeTableDiagramPara.sngStaBlank = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "左边缩进宽度"
                    TimeTablePara.TimeTableDiagramPara.sngPubLeftX = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "上面缩进高度"
                    TimeTablePara.TimeTableDiagramPara.sngPubTopY = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图1分格线型"
                    TimeTablePara.DiagramStylePara.OneTime1LineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图1分格线宽"
                    TimeTablePara.DiagramStylePara.OneTime1LineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图1分格线颜色"
                    TimeTablePara.DiagramStylePara.OneTime1LineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图5分格线型"
                    TimeTablePara.DiagramStylePara.OneTime5LineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图5分格线宽"
                    TimeTablePara.DiagramStylePara.OneTime5LineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图5分格线颜色"
                    TimeTablePara.DiagramStylePara.OneTime5LineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图10分格线型"
                    TimeTablePara.DiagramStylePara.OneTime10LineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图10分格线宽"
                    TimeTablePara.DiagramStylePara.OneTime10LineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图10分格线颜色"
                    TimeTablePara.DiagramStylePara.OneTime10LineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图30分格线型"
                    TimeTablePara.DiagramStylePara.OneTime30LineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图30分格线宽"
                    TimeTablePara.DiagramStylePara.OneTime30LineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图30分格线颜色"
                    TimeTablePara.DiagramStylePara.OneTime30LineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图60分格线型"
                    TimeTablePara.DiagramStylePara.OneTime60LineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图60分格线宽"
                    TimeTablePara.DiagramStylePara.OneTime60LineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一分格图60分格线颜色"
                    TimeTablePara.DiagramStylePara.OneTime60LineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图2分格线型"
                    TimeTablePara.DiagramStylePara.TwoTime2LineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图2分格线宽"
                    TimeTablePara.DiagramStylePara.TwoTime2LineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图2分格线颜色"
                    TimeTablePara.DiagramStylePara.TwoTime2LineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图10分格线型"
                    TimeTablePara.DiagramStylePara.TwoTime10LineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图10分格线宽"
                    TimeTablePara.DiagramStylePara.TwoTime10LineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图10分格线颜色"
                    TimeTablePara.DiagramStylePara.TwoTime10LineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图30分格线型"
                    TimeTablePara.DiagramStylePara.TwoTime30LineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图30分格线宽"
                    TimeTablePara.DiagramStylePara.TwoTime30LineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图30分格线颜色"
                    TimeTablePara.DiagramStylePara.TwoTime30LineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图60分格线型"
                    TimeTablePara.DiagramStylePara.TwoTime60LineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图60分格线宽"
                    TimeTablePara.DiagramStylePara.TwoTime60LineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "二分格图60分格线颜色"
                    TimeTablePara.DiagramStylePara.TwoTime60LineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图10分格线型"
                    TimeTablePara.DiagramStylePara.TenTime10LineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图10分格线宽"
                    TimeTablePara.DiagramStylePara.TenTime10LineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图10分格线颜色"
                    TimeTablePara.DiagramStylePara.TenTime10LineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图30分格线型"
                    TimeTablePara.DiagramStylePara.TenTime30LineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图30分格线宽"
                    TimeTablePara.DiagramStylePara.TenTime30LineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图30分格线颜色"
                    TimeTablePara.DiagramStylePara.TenTime30LineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图60分格线型"
                    TimeTablePara.DiagramStylePara.TenTime60LineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图60分格线宽"
                    TimeTablePara.DiagramStylePara.TenTime60LineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "十分格图60分格线颜色"
                    TimeTablePara.DiagramStylePara.TenTime60LineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "小时格图60分格线型"
                    TimeTablePara.DiagramStylePara.HourTime60LineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "小时格图60分格线宽"
                    TimeTablePara.DiagramStylePara.HourTime60LineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "小时格图60分格线颜色"
                    TimeTablePara.DiagramStylePara.HourTime60LineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车站名称字体名称"
                    TimeTablePara.DiagramStylePara.StaNameFontName = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车站名称字体大小"
                    TimeTablePara.DiagramStylePara.StaNameFontSize = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车站名称字体粗体"
                    TimeTablePara.DiagramStylePara.StaNameFontBold = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车站名称字体斜体"
                    TimeTablePara.DiagramStylePara.StaNameFontItalic = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车站名称字体颜色"
                    TimeTablePara.DiagramStylePara.StaNameFontColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "时间标注字体名称"
                    TimeTablePara.DiagramStylePara.TimeFontName = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "时间标注字体大小"
                    TimeTablePara.DiagramStylePara.TimeFontSize = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "时间标注字体粗体"
                    TimeTablePara.DiagramStylePara.TimeFontBold = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "时间标注字体斜体"
                    TimeTablePara.DiagramStylePara.TimeFontItalic = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "时间标注字体颜色"
                    TimeTablePara.DiagramStylePara.TimeFontColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一般车站中心线类型"
                    TimeTablePara.DiagramStylePara.StaLineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一般车站中心线宽度"
                    TimeTablePara.DiagramStylePara.StaLineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "一般车站中心线颜色"
                    TimeTablePara.DiagramStylePara.StaLineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "分岔站中心线类型"
                    TimeTablePara.DiagramStylePara.FenStaLineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "分岔站中心线宽度"
                    TimeTablePara.DiagramStylePara.FenStaLineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "分岔站中心线颜色"
                    TimeTablePara.DiagramStylePara.FenStaLineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车场中心线类型"
                    TimeTablePara.DiagramStylePara.CheChangStaLineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车场中心线宽度"
                    TimeTablePara.DiagramStylePara.CheChangStaLineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车场中心线颜色"
                    TimeTablePara.DiagramStylePara.CheChangStaLineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "所有运行线线型"
                    TimeTablePara.DiagramStylePara.TrainLineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "所有运行线线宽"
                    TimeTablePara.DiagramStylePara.TrainLineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "所有运行线颜色"
                    TimeTablePara.DiagramStylePara.TrainLineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "所有车底连接线线型"
                    TimeTablePara.DiagramStylePara.CheDiLineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "所有车底连接线线宽"
                    TimeTablePara.DiagramStylePara.CheDiLineWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "所有车底连接线颜色"
                    TimeTablePara.DiagramStylePara.CheDiLineColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车次标号字体名称"
                    TimeTablePara.DiagramStylePara.CheCiFontName = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车次标号字体大小"
                    TimeTablePara.DiagramStylePara.CheCiFontSize = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车次标号字体粗体"
                    TimeTablePara.DiagramStylePara.CheCiFontBold = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车次标号字体斜体"
                    TimeTablePara.DiagramStylePara.CheCiFontItalic = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车次标号字体颜色"
                    TimeTablePara.DiagramStylePara.CheCiFontColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "斜向车次字体名称"
                    TimeTablePara.DiagramStylePara.XieCheCiFontName = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "斜向车次字体大小"
                    TimeTablePara.DiagramStylePara.XieCheCiFontSize = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "斜向车次字体粗体"
                    TimeTablePara.DiagramStylePara.XieCheCiFontColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "斜向车次字体斜体"
                    TimeTablePara.DiagramStylePara.XieCheCiFontItalic = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "斜向车次字体颜色"
                    TimeTablePara.DiagramStylePara.XieCheCiFontColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "斜向车次字体颜色"
                    TimeTablePara.DiagramStylePara.XieCheCiFontColor = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "是否显示车次"
                    TimeTablePara.TimeTableDiagramPara.nifPrintCheCi = True  'tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "是否显示斜向车次"
                    TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车底交路线高度"
                    TimeTablePara.TimeTableDiagramPara.nCheDiLineHeight = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车底交路线类型"
                    TimeTablePara.TimeTableDiagramPara.sCheDiLineStyle = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车站股道线间距"
                    TimeTablePara.StaDiagramePara.nStaLineHeight = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车站站名图高"
                    TimeTablePara.TimeTableDiagramPara.sngPicStationHeight = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "车站站名图宽"
                    TimeTablePara.TimeTableDiagramPara.sngPicStationWidth = tempTable.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "运行线可调整时间段"
                    ' TdrawLinePara.sMaxMoveTime = 2 'myTable3.Rows(i - 1).Item("ParaValue").ToString.Trim
                Case "运行线移动时间"
                    '  TdrawLinePara.sMoveStepTime = 60 'myTable3.Rows(i - 1).Item("ParaValue").ToString.Trim
            End Select
        Next
        'TimeTablePara.TimeTableDiagramPara.sCheCiShowStyle = 1
    End Sub
    '读入底图结构与线网信息
    Public Sub OCreadNetStaAndSecData()
        Dim sqlstr As String = ""

        sqlstr = "SELECT * FROM TMS_DIASTRUCTINFO WHERE IfFixed=1 and TRAINDIAGRAMID='" & ODSPubpara.DiagramSelect.Trim & "' order by StationSeq"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        ReDim StationInf(tempTable.Rows.Count)
        Dim i, j As Integer
        If tempTable.Rows.Count > 0 Then
            For i = 1 To tempTable.Rows.Count
                StationInf(i).sStationName = Trim(tempTable.Rows(i - 1).Item("StationName"))
                StationInf(i).sAtLineName = Trim(tempTable.Rows(i - 1).Item("Linename"))
                StationInf(i).Ycord = Trim(tempTable.Rows(i - 1).Item("YCoord"))
            Next i
        Else
            MsgBox("底图结构没有设置或者没有设置默认的底图结构!", , "提示")
            TimeTablePara.sInputDataError = "底图结构设置错误码"
            Exit Sub
        End If

        ReDim NotSameStationInf(0)
        Dim nMark As Integer
        nMark = 0
        For i = 1 To UBound(StationInf)
            nMark = 0
            For j = 1 To UBound(NotSameStationInf)
                If NotSameStationInf(j) = StationInf(i).sStationName Then
                    nMark = 1
                    Exit For
                End If
            Next j
            If nMark = 0 Then
                ReDim Preserve NotSameStationInf(UBound(NotSameStationInf) + 1)
                NotSameStationInf(UBound(NotSameStationInf)) = StationInf(i).sStationName
            End If
        Next

        'LocalDataSet.TMS_SECTIONINFO.Clear()
        'LocalDataSet.Fill("TMS_SECTIONINFO", "TraindiagramID='" & ODSPubpara.DiagramSelect & "'")
        sqlstr = "SELECT * FROM TMS_SECTIONINFO WHERE TraindiagramID='" & ODSPubpara.DiagramSelect & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        Dim sFirSta As String
        Dim sSecSta As String
        Dim sSecName As String
        ReDim SectionInf(tempTable.Rows.Count)
        For i = 1 To UBound(StationInf) - 1
            sFirSta = StationInf(i).sStationName
            sSecSta = StationInf(i + 1).sStationName
            sSecName = sFirSta & "->" & sSecSta
            If tempTable.Rows.Count > 0 Then
                For j = 1 To tempTable.Rows.Count
                    If tempTable.Rows(j - 1).Item("SectionName").ToString.Trim = sSecName Then
                        ReDim SectionInf(j).lDistance(2)
                        SectionInf(j).nSecNumber = tempTable.Rows(j - 1).Item("SECTIONSEQ")
                        SectionInf(j).sSecName = sSecName
                        SectionInf(j).sLineName = tempTable.Rows(j - 1).Item("LineName")
                        SectionInf(j).nSection = Val(tempTable.Rows(j - 1).Item("LineNumber").ToString)
                        SectionInf(j).sBlock = tempTable.Rows(j - 1).Item("Blocktype").ToString
                        SectionInf(j).sSecFirName = sFirSta
                        SectionInf(j).sSecSecName = sSecSta
                        SectionInf(j).nFirStaID = i
                        SectionInf(j).nSecStaID = i + 1
                        SectionInf(j).nHStation = i
                        SectionInf(j).nQStation = i + 1
                        SectionInf(j).lDistance(1) = tempTable.Rows(j - 1).Item("DownSectionDistance")
                        SectionInf(j).lDistance(2) = tempTable.Rows(j - 1).Item("UPSectionDistance")
                        Exit For
                    End If
                Next
            End If
        Next
        '区间标尺
        Dim strSecName As String
        For i = 1 To UBound(SectionInf)
            strSecName = SectionInf(i).sSecName
            'LocalDataSet.TMS_RUNSCALE.Clear()
            'LocalDataSet.Fill("TMS_RUNSCALE", "Sectionname='" & strSecName & "' and TraindiagramID='" & ODSPubpara.DiagramSelect & "' order by SecScaleNum")
            sqlstr = "SELECT * FROM TMS_RUNSCALE WHERE Sectionname='" & strSecName & "' and TraindiagramID='" & ODSPubpara.DiagramSelect & "' order by SecScaleNum"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            ReDim SectionInf(i).SecScale(tempTable.Rows.Count)
            If tempTable.Rows.Count > 0 Then
                For j = 1 To tempTable.Rows.Count
                    SectionInf(i).SecScale(j).nID = tempTable.Rows(j - 1).Item("SecScaleNum")
                    SectionInf(i).SecScale(j).sScaleName = tempTable.Rows(j - 1).Item("SecScaleName").ToString.Trim
                    SectionInf(i).SecScale(j).sngDownTime = MinuteToSecond(tempTable.Rows(j - 1).Item("DownRunTime").ToString.Trim)
                    SectionInf(i).SecScale(j).sngUpTime = MinuteToSecond(tempTable.Rows(j - 1).Item("UpRunTime").ToString.Trim)
                    SectionInf(i).SecScale(j).sngDownAppendStartTime = MinuteToSecond(tempTable.Rows(j - 1).Item("DownStartAddTime").ToString.Trim)
                    SectionInf(i).SecScale(j).sngDownAppendStopTime = MinuteToSecond(tempTable.Rows(j - 1).Item("DownStopAddTime").ToString.Trim)
                    SectionInf(i).SecScale(j).sngUpAppendStartTime = MinuteToSecond(tempTable.Rows(j - 1).Item("UpStartAddTime").ToString.Trim)
                    SectionInf(i).SecScale(j).sngUpAppendStopTime = MinuteToSecond(tempTable.Rows(j - 1).Item("UpStopAddTime").ToString.Trim)
                Next
            End If

        Next
    End Sub

    '读入车站等信息
    Public Sub OCInputStationGudaoAndJinLuInfByDAO()
        Dim i, j As Integer
        Dim Str As String
        Dim sqlstr As String = ""
        For i = 1 To UBound(StationInf)
            'LocalDataSet.TMS_STATIONINFO.Clear()
            'LocalDataSet.Fill("TMS_STATIONINFO", "stationname='" & StationInf(i).sStationName & "' and TraindiagramID='" & ODSPubpara.DiagramSelect & "'")

            sqlstr = "SELECT * FROM TMS_STATIONINFO WHERE stationname='" & StationInf(i).sStationName & "' and TraindiagramID='" & ODSPubpara.DiagramSelect & "'"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            If tempTable.Rows.Count > 0 Then
                StationInf(i).sStaStyle = tempTable.Rows(0).Item("StationType").ToString.Trim
                StationInf(i).sAtLineName = tempTable.Rows(0).Item("LINENAME").ToString.Trim
                StationInf(i).sPrintStaName = tempTable.Rows(0).Item("OutputName").ToString.Trim
                StationInf(i).sStaProperity = tempTable.Rows(0).Item("StationCharacter").ToString.Trim
                StationInf(i).sStationProp = ChaStProp(tempTable.Rows(0).Item("StationCharacter").ToString.Trim, tempTable.Rows(0).Item("StationType").ToString.Trim)
                StationInf(i).sEnglishName = tempTable.Rows(0).Item("EnglishShortName").ToString.Trim
            End If
            'LocalDataSet.TMS_LINEDRAW.Clear()
            'LocalDataSet.Fill("TMS_LINEDRAW", "name='" & StationInf(i).sStationName & "'and TraindiagramID='" & ODSPubpara.DiagramSelect & "'")
            sqlstr = "SELECT * FROM TMS_LINEDRAW WHERE name='" & StationInf(i).sStationName & "'and TraindiagramID='" & ODSPubpara.DiagramSelect & "'"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)
            StationInf(i).nStLineNum = 0
            ReDim StationInf(i).sStLineNo(0)
            ReDim StationInf(i).nStLineUse(0)
            ReDim StationInf(i).sLineUse(0)
            ReDim StationInf(i).sUpOrDownUse(0)
            ReDim StationInf(i).sGuDaoUseSeq(0)
            ReDim StationInf(i).sGuDaoName(0)
            If tempTable.Rows.Count > 0 Then
                For j = 1 To tempTable.Rows.Count
                    Str = tempTable.Rows(j - 1).Item("PARTLINETYPE").ToString.Trim
                    If Str.Length >= 3 Then
                        If Str.Substring(Str.Length - 3) = "股道线" Then
                            StationInf(i).nStLineNum = StationInf(i).nStLineNum + 1
                            ReDim Preserve StationInf(i).sStLineNo(UBound(StationInf(i).sStLineNo) + 1)
                            ReDim Preserve StationInf(i).nStLineUse(UBound(StationInf(i).sStLineNo) + 1)
                            ReDim Preserve StationInf(i).sLineUse(UBound(StationInf(i).sStLineNo) + 1)
                            ReDim Preserve StationInf(i).sUpOrDownUse(UBound(StationInf(i).sStLineNo) + 1)
                            ReDim Preserve StationInf(i).sGuDaoUseSeq(UBound(StationInf(i).sStLineNo) + 1)
                            ReDim Preserve StationInf(i).sGuDaoName(UBound(StationInf(i).sStLineNo) + 1)
                            StationInf(i).sStLineNo(UBound(StationInf(i).sStLineNo)) = tempTable.Rows(j - 1).Item("DAOCHA_ID").ToString.Trim()
                            StationInf(i).sLineUse(UBound(StationInf(i).sStLineNo)) = tempTable.Rows(j - 1).Item("TRACKTYPE").ToString.Trim
                            StationInf(i).sUpOrDownUse(UBound(StationInf(i).sStLineNo)) = tempTable.Rows(j - 1).Item("TRACKUSAGE").ToString.Trim
                            StationInf(i).sGuDaoUseSeq(UBound(StationInf(i).sStLineNo)) = tempTable.Rows(j - 1).Item("TRACKUSINGNUM").ToString.Trim
                            StationInf(i).nStLineUse(UBound(StationInf(i).sStLineNo)) = ChaLineUse(tempTable.Rows(j - 1).Item("TRACKTYPE").ToString.Trim, tempTable.Rows(j - 1).Item("TRACKUSAGE").ToString.Trim)
                            StationInf(i).sGuDaoName(UBound(StationInf(i).sStLineNo)) = tempTable.Rows(j - 1).Item("CONTROLMOD").ToString.Trim
                        End If
                    End If
                Next
            End If

            '导入车站进路和分岔站股道使用

            '导入车站进路和分岔站股道使用
            ReDim StationInf(i).sTrackUse(0)

            ''以下代码读入车站进路信息
            'ReDim StationInf(i).PathLinkTrack(0)
            'ReDim StationInf(i).PathControlNum(0)
            'ReDim StationInf(i).PathCrossNum(0)
            'ReDim StationInf(i).PathLinkSta(0)
            'ReDim StationInf(i).PathTrackNum(0)
            'ReDim StationInf(i).PathPathTrackID(0)
            'Dim strTable5 As String = "select * from 车站进路信息 where 车站名称='" & StationInf(i).sStationName & "'"
            'ReDim StationInf(i).PathLinkTrack(nNum)
            'ReDim StationInf(i).PathControlNum(nNum)
            'ReDim StationInf(i).PathCrossNum(nNum)
            'ReDim StationInf(i).PathLinkSta(nNum)
            'ReDim StationInf(i).PathTrackNum(nNum)
            'ReDim StationInf(i).PathPathTrackID(nNum)

            'If nNum > 0 Then
            '    sFile.MoveFirst()
            '    For j = 1 To nNum
            '        StationInf(i).PathLinkTrack(j) = sFile.Fields("进站方式").Value.ToString.Trim ' myTable5.Rows(j - 1).Item("到达方向").ToString.Trim
            '        StationInf(i).PathLinkSta(j) = sFile.Fields("连接车站名").Value.ToString.Trim ' myTable5.Rows(j - 1).Item("出发方向").ToString.Trim
            '        StationInf(i).PathTrackNum(j) = sFile.Fields("股道编号").Value.ToString.Trim ' myTable5.Rows(j - 1).Item("股道号").ToString.Trim
            '        StationInf(i).PathLinkTrack(j) = sFile.Fields("通过的轨道编号").Value.ToString.Trim ' myTable5.Rows(j - 1).Item("基本进路").ToString.Trim
            '        StationInf(i).PathCrossNum(j) = sFile.Fields("通过的道岔编号").Value.ToString.Trim '  myTable5.Rows(j - 1).Item("可选进路").ToString.Trim
            '        StationInf(i).PathControlNum(j) = sFile.Fields("通过的控制模块").Value.ToString.Trim ' myTable5.Rows(j - 1).Item("备注").ToString.Trim
            '        sFile.MoveNext()
            '    Next
            'End If


            '    '以下代码是车站间隔时间
            '    Dim strTable6 As String = "select * from 车站间隔时间 where 车站名称='" & StationInf(i).sStationName & "'"
            '    sFile = dFile.OpenRecordset(strTable6)
            '    nNum = 0
            '    If sFile.RecordCount > 0 Then
            '        sFile.MoveLast()
            '        nNum = sFile.RecordCount
            '    End If

            With StationInf(i)
                ReDim Preserve .lTaoBu(2)
                ReDim Preserve .lTaoHui(2)
                ReDim Preserve .lTaoLian1(2)
                ReDim Preserve .lTaoLian2(2)
                ReDim Preserve .lTaoTongK(2)
                ReDim Preserve .lTaoTongH(2)
                ReDim Preserve .lTaoDaoFa(2)
                ReDim Preserve .lTaoFaDao(2)
                ReDim Preserve .lTaoFaFa(2)
                ReDim Preserve .lTaoDaoDao(2)
                '        If nNum > 0 Then
                '            sFile.MoveFirst()
                '            .lTaoBu(1) = MinuteToSecond(sFile.Fields("τ不上").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ不上").ToString.Trim)
                '            .lTaoBu(2) = MinuteToSecond(sFile.Fields("τ不下").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ不下").ToString.Trim)
                '            .lTaoHui(1) = MinuteToSecond(sFile.Fields("τ会上").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ会上").ToString.Trim)
                '            .lTaoHui(2) = MinuteToSecond(sFile.Fields("τ会下").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ会下").ToString.Trim)
                '            .lTaoLian1(1) = MinuteToSecond(sFile.Fields("τ连1上").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ连1上").ToString.Trim)
                '            .lTaoLian1(2) = MinuteToSecond(sFile.Fields("τ连1下").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ连1下").ToString.Trim)
                '            .lTaoLian2(1) = MinuteToSecond(sFile.Fields("τ连2上").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ连2上").ToString.Trim)
                '            .lTaoLian2(2) = MinuteToSecond(sFile.Fields("τ连2下").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ连2下").ToString.Trim)
                '            .lTaoTongK(1) = MinuteToSecond(sFile.Fields("τ通1上").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ通1上").ToString.Trim)
                '            .lTaoTongK(2) = MinuteToSecond(sFile.Fields("τ通1下").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ通1下").ToString.Trim)
                '            .lTaoTongH(1) = MinuteToSecond(sFile.Fields("τ通2上").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ通2上").ToString.Trim)
                '            .lTaoTongH(2) = MinuteToSecond(sFile.Fields("τ通2下").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ通2下").ToString.Trim)
                '            .lTaoDaoFa(1) = MinuteToSecond(sFile.Fields("τ到发上").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ到发上").ToString.Trim)
                '            .lTaoDaoFa(2) = MinuteToSecond(sFile.Fields("τ到发下").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ到发下").ToString.Trim)
                '            .lTaoFaDao(1) = MinuteToSecond(sFile.Fields("τ发到上").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ发到上").ToString.Trim)
                '            .lTaoFaDao(2) = MinuteToSecond(sFile.Fields("τ发到下").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ发到下").ToString.Trim)
                '            .lTaoFaFa(1) = MinuteToSecond(sFile.Fields("τ发发上").Value.ToString.Trim) ' MinuteToSecond(myTable6.Rows(0).Item("τ发发上").ToString.Trim)
                '            .lTaoFaFa(2) = MinuteToSecond(sFile.Fields("τ发发下").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ发发下").ToString.Trim)
                '            .lTaoDaoDao(1) = MinuteToSecond(sFile.Fields("τ到到上").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ到到上").ToString.Trim)
                '            .lTaoDaoDao(2) = MinuteToSecond(sFile.Fields("τ到到下").Value.ToString.Trim) 'MinuteToSecond(myTable6.Rows(0).Item("τ到到下").ToString.Trim)
                '        End If
            End With


            '    '以下代码是追踪间隔时间
            '    Dim strTable7 As String = "select * from 追踪间隔时间 where 车站名称='" & StationInf(i).sStationName & "'"
            '    sFile = dFile.OpenRecordset(strTable7)
            '    nNum = 0
            '    If sFile.RecordCount > 0 Then
            '        sFile.MoveLast()
            '        nNum = sFile.RecordCount
            '    End If


            With StationInf(i)
                ReDim Preserve .IKK(17)
                ReDim Preserve .IKH(17)
                ReDim Preserve .IHH(17)
                ReDim Preserve .IHK(17)
                '        If nNum > 0 Then
                '            sFile.MoveFirst()
                '            .IKK(0) = MinuteToSecond(sFile.Fields("同向通发").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("同向通发").ToString.Trim)
                '            .IKK(1) = MinuteToSecond(sFile.Fields("同向发发").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("同向发发").ToString.Trim)
                '            .IKK(2) = MinuteToSecond(sFile.Fields("同向到发").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("同向到发").ToString.Trim)
                '            .IKK(3) = MinuteToSecond(sFile.Fields("同向发通").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("同向发通").ToString.Trim)
                '            .IKK(4) = MinuteToSecond(sFile.Fields("同向发到").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("同向发到").ToString.Trim)
                '            .IKK(5) = MinuteToSecond(sFile.Fields("同向通到").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("同向通到").ToString.Trim)
                '            .IKK(6) = MinuteToSecond(sFile.Fields("同向到到").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("同向到到").ToString.Trim)
                '            .IKK(7) = MinuteToSecond(sFile.Fields("同向到通").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("同向到通").ToString.Trim)
                '            .IKK(8) = MinuteToSecond(sFile.Fields("同向通通").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("同向通通").ToString.Trim)
                '            .IKK(9) = MinuteToSecond(sFile.Fields("对向到到").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("对向到到").ToString.Trim)
                '            .IKK(10) = MinuteToSecond(sFile.Fields("对向发到").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("对向发到").ToString.Trim)
                '            .IKK(11) = MinuteToSecond(sFile.Fields("对向到发").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("对向到发").ToString.Trim)
                '            .IKK(12) = MinuteToSecond(sFile.Fields("对向发发").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("对向发发").ToString.Trim)
                '            .IKK(13) = MinuteToSecond(sFile.Fields("对向到通").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("对向到通").ToString.Trim)
                '            .IKK(14) = MinuteToSecond(sFile.Fields("对向发通").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("对向发通").ToString.Trim)
                '            .IKK(15) = MinuteToSecond(sFile.Fields("对向通到").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("对向通到").ToString.Trim)
                '            .IKK(16) = MinuteToSecond(sFile.Fields("对向通发").Value.ToString.Trim) ' MinuteToSecond(myTable7.Rows(0).Item("对向通发").ToString.Trim)
                '            .IKK(17) = MinuteToSecond(sFile.Fields("对向通通").Value.ToString.Trim) 'MinuteToSecond(myTable7.Rows(0).Item("对向通通").ToString.Trim)
                '        End If
            End With
            ' 以下代码读入车站进路信息()
            ReDim StationInf(i).sCrossUse(0)
        Next i

        'dFile.Close()
    End Sub

    '读入折返站
    Public Sub OCInputChediZhefanDataAndCheDiScaleInf()
        'LocalDataSet.TMS_TRAINRETURNTIME.Clear()
        'LocalDataSet.Fill("TMS_TRAINRETURNTIME", "TraindiagramID='" & ODSPubpara.DiagramSelect & "'")

        Dim sqlstr As String = "SELECT * FROM TMS_TRAINRETURNTIME WHERE TraindiagramID='" & ODSPubpara.DiagramSelect & "'"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        Dim i As Integer
        ReDim ChediZhefanBiaozhunInfo(tempTable.Rows.Count)
        For i = 1 To tempTable.Rows.Count
            With ChediZhefanBiaozhunInfo(i)
                .SCheDiLeiXing = tempTable.Rows(i - 1).Item("StockStyleNAME")
                .sZhefanStation = tempTable.Rows(i - 1).Item("StationName")
                .lLijiZhefanTime = MinuteToSecond(tempTable.Rows(i - 1).Item("ImmiTurnTime"))
                .lZhanQianTime = MinuteToSecond(tempTable.Rows(i - 1).Item("BeforeTurnTime"))
                .lZhanHouTime = MinuteToSecond(tempTable.Rows(i - 1).Item("AfterTurnTime"))
                .nFirRunTime = MinuteToSecond(tempTable.Rows(i - 1).Item("ArriToTurnTime"))
                .nSecRunTime = MinuteToSecond(tempTable.Rows(i - 1).Item("TurnToDepartTime"))
                .nArrFaDaoTime = MinuteToSecond(tempTable.Rows(i - 1).Item("ArriStartArriTime"))
                .nStartFaDaoTime = MinuteToSecond(tempTable.Rows(i - 1).Item("StartStartArriTime"))
            End With
        Next
        '   读入车底标尺信息,select distinct 信息没有呀
        ReDim TrainRunScaleInf(1)
        TrainRunScaleInf(1).nScaleID = 1
        TrainRunScaleInf(1).sScaleName = "正常运行"
        'localDataSet.Fill("TMS_RUNSCALE", " TraindiagramID='" & ODSPubpara.DiagramSelect & "' order by SecScaleNum asc ")
        'If localDataSet.TMS_RUNSCALE.Rows.Count > 0 Then
        '    ReDim TrainRunScaleInf(localDataSet.TMS_RUNSCALE.Rows.Count)
        '    For j = 1 To localDataSet.TMS_RUNSCALE.Rows.Count
        '        TrainRunScaleInf(j).nScaleID = Val(localDataSet.TMS_RUNSCALE.Rows(j - 1).Item("SecScaleNum"))
        '        TrainRunScaleInf(j).sScaleName = localDataSet.TMS_RUNSCALE.Rows(j - 1).Item("SecScaleName").ToString.Trim
        '    Next j
        'End If
    End Sub

    '打开车底信息和发车间隔安排
    Public Sub OCInputChediAndTrainJianGeData(ByVal sState As String)
        Dim i As Integer
        Dim sqlstr As String = ""
        'localDataSet.TMS_TRAINUSINGINFO.Clear()
        'localDataSet.Fill("TMS_TRAINUSINGINFO", "TraindiagramID='" & ODSPubpara.DiagramSelect & "' order by StockStyleID")

        sqlstr = "SELECT * FROM TMS_TRAINUSINGINFO WHERE TraindiagramID='" & ODSPubpara.DiagramSelect & "' order by StockStyleID"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        ReDim BaseChediInfo(tempTable.Rows.Count)
        If tempTable.Rows.Count > 0 Then
            For i = 1 To tempTable.Rows.Count
                With BaseChediInfo(i)
                    .SCheDiLeiXing = tempTable.Rows(i - 1).Item("StockStyleName")
                    .bIfGouWang = 0
                    ReDim BaseChediInfo(i).nLinkTrain(0)
                End With
            Next
        End If
    End Sub

    '读入时刻表车站顺序
    Public Sub OCReadSKBStaSeqData()
        Dim i, j As Integer
        Dim sqlstr As String = ""
        ReDim SkbStnSeq(0)
        'LocalDataSet.TMS_TIMETABLESTASEQ.Clear()
        'LocalDataSet.Fill("TMS_TIMETABLESTASEQ", "1=1 order by STASEQNAME,Staseq")
        sqlstr = "SELECT * FROM TMS_TIMETABLESTASEQ WHERE 1=1 order by STASEQNAME,Staseq"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        Dim nCurID As Integer
        Dim nCurV As Integer

        nCurV = 1
        For i = 1 To tempTable.Rows.Count
            nCurID = 0
            For j = 1 To UBound(SkbStnSeq)
                If SkbStnSeq(j).sQDName = tempTable.Rows(j - 1).Item("STASEQNAME") Then
                    nCurID = j
                    Exit For
                End If
            Next
            If nCurID > 0 Then
                ReDim Preserve SkbStnSeq(j).nStnSeq(UBound(SkbStnSeq(j).nStnSeq) + 1)
                SkbStnSeq(j).nStnSeq(UBound(SkbStnSeq(j).nStnSeq)) = StaNameToStaInfID(tempTable.Rows(j - 1).Item("STATIONNAME"))
            Else
                ReDim Preserve SkbStnSeq(UBound(SkbStnSeq) + 1)
                ReDim SkbStnSeq(UBound(SkbStnSeq)).nStnSeq(0)
                SkbStnSeq(UBound(SkbStnSeq)).sQDName = tempTable.Rows(j - 1).Item("STASEQNAME")
            End If
        Next
    End Sub

    '读入列车交路
    Public Sub OCReadBaseTrainInf()
        Dim i, j, p As Integer
        Dim sqlstr As String = ""
        'LocalDataSet.TMS_TRAININFO.Clear()
        'LocalDataSet.Fill("TMS_TRAININFO", "TraindiagramID='" & ODSPubpara.DiagramSelect & "' order by RoutingName")
        sqlstr = "SELECT * FROM TMS_TRAININFO WHERE TraindiagramID='" & ODSPubpara.DiagramSelect & "' order by RoutingName"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        ReDim BasicTrainInf(tempTable.Rows.Count)
        Dim nJiNum As Int32
        Dim nOhNum As Int32
        nJiNum = 1
        nOhNum = 2
        If tempTable.Rows.Count > 0 Then
            For p = 1 To tempTable.Rows.Count
                With BasicTrainInf(p)
                    .sJiaoLuName = Trim(tempTable.Rows(p - 1).Item("RoutingName").ToString)
                    If Trim(tempTable.Rows(p - 1).Item("UporDown").ToString) = "上行" Then
                        .nUporDown = 2
                    Else
                        .nUporDown = 1
                    End If
                    .TrainStyle = Trim(tempTable.Rows(p - 1).Item("Type").ToString)
                    .StartStation = Trim(tempTable.Rows(p - 1).Item("OStationNAME").ToString)
                    .EndStation = Trim(tempTable.Rows(p - 1).Item("DStationNAME").ToString)
                    .ComeStation = Trim(tempTable.Rows(p - 1).Item("OStationNAME").ToString)
                    .NextStation = Trim(tempTable.Rows(p - 1).Item("DStationNAME").ToString)

                    ReDim Preserve .TrainReturnStyle(2)
                    .TrainReturnStyle(1) = Trim(tempTable.Rows(p - 1).Item("StartType").ToString)
                    .TrainReturnStyle(2) = Trim(tempTable.Rows(p - 1).Item("EndUseType").ToString)

                    .sTrainXingZhi = Trim(tempTable.Rows(p - 1).Item("TrainType").ToString)
                    .SCheDiLeiXing = Trim(tempTable.Rows(p - 1).Item("StockStyleID").ToString)

                    .sLineNum = Trim(tempTable.Rows(p - 1).Item("LINETRAINCODE").ToString)
                    .sMuDiNum = Trim(tempTable.Rows(p - 1).Item("ENDSIGN").ToString)

                    Dim IfIn As Integer
                    IfIn = 0

                    '初始化设置
                    ReDim .Arrival(UBound(StationInf))
                    ReDim .Starting(UBound(StationInf))
                    For j = 1 To UBound(StationInf)
                        .Arrival(j) = -1
                        .Starting(j) = -1
                    Next j

                    '以下代码读入停站信息
                    'LocalDataSet.TMS_STOPSCALEINFO.Clear()
                    'LocalDataSet.Fill("TMS_STOPSCALEINFO", "TraindiagramID='" & ODSPubpara.DiagramSelect & "' and RoutingName='" & .sJiaoLuName & "' order by Stoptype,Seqnum")
                    sqlstr = "SELECT * FROM TMS_STOPSCALEINFO WHERE TraindiagramID='" & ODSPubpara.DiagramSelect & "' and RoutingName='" & .sJiaoLuName & "' order by Stoptype,Seqnum"
                    Dim tempTable1 As New Data.DataTable
                    tempTable1 = Globle.Method.ReadDataForAccess(sqlstr)
                    Dim sFirName As String
                    Dim ntmpCurID As Integer
                    ReDim .StopScale(0)
                    Dim nIfIn As Boolean
                    nIfIn = False
                    If tempTable1.Rows.Count > 0 Then
                        For i = 1 To tempTable1.Rows.Count
                            nIfIn = False
                            For j = 1 To UBound(.StopScale)
                                If tempTable1.Rows(i - 1).Item("Stoptype").ToString.Trim = .StopScale(j).sName.ToString.Trim Then
                                    nIfIn = True
                                    ntmpCurID = j
                                    Exit For
                                End If
                            Next
                            If nIfIn = True Then
                                ReDim Preserve .StopScale(ntmpCurID).nStopStation(UBound(.StopScale(ntmpCurID).nStopStation) + 1)
                                ReDim Preserve .StopScale(ntmpCurID).StopTime(UBound(.StopScale(ntmpCurID).StopTime) + 1)
                                ReDim Preserve .StopScale(ntmpCurID).sScaleName(UBound(.StopScale(ntmpCurID).sScaleName) + 1)
                                .StopScale(ntmpCurID).nStopStation(UBound(.StopScale(ntmpCurID).nStopStation)) = FromStaNameToStaIDByStationinf(tempTable1.Rows(i - 1).Item("Staionname"))
                                .StopScale(ntmpCurID).StopTime(UBound(.StopScale(ntmpCurID).StopTime)) = MinuteToSecond(tempTable1.Rows(i - 1).Item("StopTime"))
                                .StopScale(ntmpCurID).sScaleName(UBound(.StopScale(ntmpCurID).sScaleName)) = tempTable1.Rows(i - 1).Item("StopScaleName").ToString.Trim
                                .StopScale(ntmpCurID).nStopNum = .StopScale(ntmpCurID).nStopNum + 1
                            Else
                                ReDim Preserve .StopScale(UBound(.StopScale) + 1)
                                ntmpCurID = UBound(.StopScale)
                                ReDim Preserve .StopScale(ntmpCurID).nStopStation(0)
                                ReDim Preserve .StopScale(ntmpCurID).StopTime(0)
                                ReDim Preserve .StopScale(ntmpCurID).sScaleName(0)
                                .StopScale(ntmpCurID).sName = tempTable1.Rows(i - 1).Item("Stoptype").ToString.Trim
                                ReDim Preserve .StopScale(ntmpCurID).nStopStation(UBound(.StopScale(ntmpCurID).nStopStation) + 1)
                                ReDim Preserve .StopScale(ntmpCurID).StopTime(UBound(.StopScale(ntmpCurID).StopTime) + 1)
                                ReDim Preserve .StopScale(ntmpCurID).sScaleName(UBound(.StopScale(ntmpCurID).sScaleName) + 1)
                                .StopScale(ntmpCurID).nStopStation(UBound(.StopScale(ntmpCurID).nStopStation)) = FromStaNameToStaIDByStationinf(tempTable1.Rows(i - 1).Item("Staionname"))
                                .StopScale(ntmpCurID).StopTime(UBound(.StopScale(ntmpCurID).StopTime)) = MinuteToSecond(tempTable1.Rows(i - 1).Item("StopTime"))
                                .StopScale(ntmpCurID).sScaleName(UBound(.StopScale(ntmpCurID).sScaleName)) = tempTable1.Rows(i - 1).Item("StopScaleName").ToString.Trim
                                .StopScale(ntmpCurID).nStopNum = .StopScale(ntmpCurID).nStopNum + 1
                            End If
                        Next i
                    End If




                    '以下代码读入区间运行时分信息

                    'LocalDataSet.TMS_RUNSCALEINFO.Clear()
                    'LocalDataSet.Fill("TMS_RUNSCALEINFO", "TraindiagramID='" & ODSPubpara.DiagramSelect & "' and RoutingName='" & .sJiaoLuName & "' order by RUNTYPE,sectionseq")
                    sqlstr = "SELECT * FROM TMS_RUNSCALEINFO WHERE TraindiagramID='" & ODSPubpara.DiagramSelect & "' and RoutingName='" & .sJiaoLuName & "' order by RUNTYPE,sectionseq"
                    tempTable1 = New Data.DataTable
                    tempTable1 = Globle.Method.ReadDataForAccess(sqlstr)
                    sFirName = ""
                    ntmpCurID = 0
                    ReDim .SecScale(0)
                    If tempTable1.Rows.Count > 0 Then

                        ntmpCurID = 1
                        nIfIn = False
                        For i = 1 To tempTable1.Rows.Count
                            nIfIn = False
                            For j = 1 To UBound(.SecScale)
                                If tempTable1.Rows(i - 1).Item("runtype").ToString.Trim = .SecScale(j).sName.ToString.Trim Then
                                    nIfIn = True
                                    ntmpCurID = j
                                    Exit For
                                End If
                            Next

                            If nIfIn = True Then
                                ReDim Preserve .SecScale(ntmpCurID).nSecID(UBound(.SecScale(ntmpCurID).nSecID) + 1)
                                ReDim Preserve .SecScale(ntmpCurID).RunTime(UBound(.SecScale(ntmpCurID).RunTime) + 1)
                                ReDim Preserve .SecScale(ntmpCurID).sScaleName(UBound(.SecScale(ntmpCurID).sScaleName) + 1)
                                .SecScale(ntmpCurID).nSecID(UBound(.SecScale(ntmpCurID).nSecID)) = GetSectionID(tempTable1.Rows(i - 1).Item("SectionName"))
                                .SecScale(ntmpCurID).RunTime(UBound(.SecScale(ntmpCurID).RunTime)) = MinuteToSecond(tempTable1.Rows(i - 1).Item("SecRunTime"))
                                .SecScale(ntmpCurID).sScaleName(UBound(.SecScale(ntmpCurID).sScaleName)) = tempTable1.Rows(i - 1).Item("RunScaleName").ToString.Trim
                            Else
                                ReDim Preserve .SecScale(UBound(.SecScale) + 1)
                                ntmpCurID = UBound(.SecScale)
                                ReDim Preserve .SecScale(ntmpCurID).nSecID(0)
                                ReDim Preserve .SecScale(ntmpCurID).RunTime(0)
                                ReDim Preserve .SecScale(ntmpCurID).sScaleName(0)
                                .SecScale(ntmpCurID).sName = tempTable1.Rows(i - 1).Item("runtype")
                                sFirName = tempTable1.Rows(i - 1).Item("runtype")
                                ReDim Preserve .SecScale(ntmpCurID).nSecID(UBound(.SecScale(ntmpCurID).nSecID) + 1)
                                ReDim Preserve .SecScale(ntmpCurID).RunTime(UBound(.SecScale(ntmpCurID).RunTime) + 1)
                                ReDim Preserve .SecScale(ntmpCurID).sScaleName(UBound(.SecScale(ntmpCurID).sScaleName) + 1)
                                .SecScale(ntmpCurID).nSecID(UBound(.SecScale(ntmpCurID).nSecID)) = GetSectionID(tempTable1.Rows(i - 1).Item("SectionName"))
                                .SecScale(ntmpCurID).RunTime(UBound(.SecScale(ntmpCurID).RunTime)) = MinuteToSecond(tempTable1.Rows(i - 1).Item("SecRunTime"))
                                .SecScale(ntmpCurID).sScaleName(UBound(.SecScale(ntmpCurID).sScaleName)) = tempTable1.Rows(i - 1).Item("RunScaleName").ToString.Trim
                            End If
                        Next i
                    End If

                    '以下代码读入列车径路信息

                    Dim TrainPath As String
                    TrainPath = Trim(tempTable.Rows(p - 1).Item("PASSSTAID"))
                    If Right(TrainPath, 1) = "," Or Right(TrainPath, 1) = "，" Then
                    Else
                        TrainPath = TrainPath & ","
                    End If
                    Dim TrainPathSta() As String
                    ReDim TrainPathSta(0)

                    '                '当列车径路字段的值为空时，出错，返回
                    '                If Trim(TrainPath) = "" Or Trim(TrainPath) = "无" Then
                    '                    Call InputErrToTrainErrInf(.Train, "列车的列车径路信息为空!","提示")
                    '                    Exit Sub
                    '                End If

                    .sTrainPath = TrainPath
                    i = 1
                    For j = 1 To Len(TrainPath)
                        If Mid(TrainPath, j, 1) = "," Or Mid(TrainPath, j, 1) = "，" Then
                            If Trim(Mid(TrainPath, i, j - i)) <> "" Then
                                ReDim Preserve TrainPathSta(UBound(TrainPathSta) + 1)
                                TrainPathSta(UBound(TrainPathSta)) = Mid(TrainPath, i, j - i)
                                i = j + 1
                            End If
                        End If
                    Next j

                    If UBound(TrainPathSta) < 2 Then
                        ' Call InputErrToTrainErrInf(.Train, "列车径路通过的车站只有一个，不符合要求！")
                        Exit Sub
                    End If

                    '将列车径过的车站组成n-1个区间
                    ReDim .nPassSection(UBound(TrainPathSta) - 1)
                    ReDim .sSectionName(UBound(TrainPathSta) - 1)
                    ReDim .StrFirstSta(UBound(TrainPathSta) - 1)
                    ReDim .StrSecondSta(UBound(TrainPathSta) - 1)
                    ReDim .nFirstID(UBound(TrainPathSta) - 1)
                    ReDim .nSecondID(UBound(TrainPathSta) - 1)

                    Dim strQianSta As String
                    Dim StrHouSta As String
                    Dim strQuJian1 As String
                    Dim strQuJian2 As String
                    Dim KK As Integer
                    strQianSta = TrainPathSta(1)
                    For i = 2 To UBound(TrainPathSta)
                        KK = 0
                        StrHouSta = TrainPathSta(i)
                        strQuJian1 = strQianSta & "->" & StrHouSta
                        strQuJian2 = StrHouSta & "->" & strQianSta
                        For j = 1 To UBound(SectionInf)
                            If SectionInf(j).sSecName = strQuJian1 Then
                                .nPassSection(i - 1) = j
                                .sSectionName(i - 1) = strQuJian1
                                .StrFirstSta(i - 1) = strQianSta
                                .StrSecondSta(i - 1) = StrHouSta
                                .nFirstID(i - 1) = SectionInf(j).nFirStaID
                                .nSecondID(i - 1) = SectionInf(j).nSecStaID
                                KK = 1
                            ElseIf SectionInf(j).sSecName = strQuJian2 Then
                                .nPassSection(i - 1) = j
                                .sSectionName(i - 1) = strQuJian2
                                .StrFirstSta(i - 1) = strQianSta
                                .StrSecondSta(i - 1) = StrHouSta
                                .nFirstID(i - 1) = SectionInf(j).nSecStaID
                                .nSecondID(i - 1) = SectionInf(j).nFirStaID
                                KK = 1
                            End If

                            If KK = 1 Then Exit For
                            If KK = 0 And j = UBound(SectionInf) Then
                                'Call InputErrToTrainErrInf(.Train, "区间" & strQuJian1 & "不存在，请确认底图打开是否有错！")
                                ' Stop
                            End If

                        Next j
                        strQianSta = StrHouSta
                    Next i

                    '导入nPathID()
                    Dim ID1 As Integer
                    Dim ID2 As Integer
                    If UBound(.nPassSection) > 0 Then
                        ID1 = .nFirstID(1)
                        ID2 = .nSecondID(1)

                        ReDim .nPathID(2)
                        .nPathID(1) = ID1
                        .nPathID(2) = ID2

                        If UBound(.nPassSection) > 1 Then
                            For i = 2 To UBound(.nPassSection)
                                If .nFirstID(i) <> .nPathID(UBound(.nPathID)) Then
                                    ReDim Preserve .nPathID(UBound(.nPathID) + 1)
                                    .nPathID(UBound(.nPathID)) = .nFirstID(i)
                                    ReDim Preserve .nPathID(UBound(.nPathID) + 1)
                                    .nPathID(UBound(.nPathID)) = .nSecondID(i)
                                Else
                                    ReDim Preserve .nPathID(UBound(.nPathID) + 1)
                                    .nPathID(UBound(.nPathID)) = .nSecondID(i)
                                End If
                            Next i
                        End If

                    End If

                    '加入分岔站信息
                    .NumWay = 0
                    ReDim .Way1(0)
                    ReDim .Way2(0)
                    ReDim .Way3(0)

                    For i = 1 To UBound(.nPathID)
                        If Left(StationInf(.nPathID(i)).sStationProp, 1) = "F" Then
                            .NumWay = .NumWay + 1
                            ReDim Preserve .Way1(.NumWay)
                            ReDim Preserve .Way2(.NumWay)
                            ReDim Preserve .Way3(.NumWay)
                            .Way1(.NumWay) = StationInf(.nPathID(i)).sStationName
                            .Way2(.NumWay) = GetBeforOrAfterStaNameFromCurSta(p, StationInf(.nPathID(i)).sStationName, 2)
                            .Way3(.NumWay) = GetBeforOrAfterStaNameFromCurSta(p, StationInf(.nPathID(i)).sStationName, 1)
                        End If
                    Next i
                End With
            Next p
        Else
            Exit Sub
        End If
    End Sub
    '读入列车与时刻表信息
    Public Sub OCReadTrainAndTimeTableInf(ByVal sSKBID As String)
        Call OCreadCheDiLinkInf(sSKBID)
        Call OCReadTimetableInf(sSKBID)
    End Sub

    '读入车底连接信息
    Public Sub OCreadCheDiLinkInf(ByVal sSKBID As String)
        Dim i, j, p As Integer
        Dim sqlstr As String = ""
        Dim nTrain As Integer
        Dim nTempTrain As Integer
        For i = 1 To UBound(ChediInfo)
            ReDim ChediInfo(i).nLinkTrain(0)
        Next
        ReDim TrainInf(0)
        'LocalDataSet.TMS_STOCKUSINGINFO.Clear()
        'LocalDataSet.Fill("TMS_STOCKUSINGINFO", "TrainDiagramID='" & sSKBID & "' order by StockSeq,LineSeq")

        sqlstr = "SELECT * FROM TMS_STOCKUSINGINFO WHERE TrainDiagramID='" & sSKBID & "' order by StockSeq,LineSeq"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        Dim stmpSta As Integer
        Dim tmpBeforSta As Integer
        Dim tmpID As Integer
        Dim tmpXH As Integer

        If tempTable.Rows.Count > 0 Then
            tmpBeforSta = Int(tempTable.Rows(0).Item("StockSeq"))
            'tmpID = GetCurCheDiInfoIDIDFromSchediID(rsDb.Fields("车底ID").Value)
            tmpID = 1
            ReDim ChediInfo(tmpID)
            ReDim ChediInfo(tmpID).nLinkTrain(0)
            ''ChediInfo(tmpID).SCheDiLeiXing = "西门子"
            ''ChediInfo(tmpID).sCheDiID = rsDb.Fields("车底顺序").Value
            Call OCCopyCheDiInformation(tmpID, tempTable.Rows(0).Item("StockID"))
            If tmpID > 0 Then
            Else
                MsgBox("车底信息与当前运行图信息不对应，请检查运行图和车底信息!", , "提示")
            End If
            tmpXH = 1

            For i = 1 To tempTable.Rows.Count
                stmpSta = Int(tempTable.Rows(i - 1).Item("StockSeq"))
                If stmpSta <> tmpBeforSta Then
                    tmpBeforSta = stmpSta
                    tmpID = tmpID + 1
                    ReDim Preserve ChediInfo(tmpID)
                    ReDim ChediInfo(tmpID).nLinkTrain(0)
                    ''ChediInfo(tmpID).SCheDiLeiXing = "西门子"
                    ''ChediInfo(tmpID).sCheDiID = rsDb.Fields("车底顺序").Value
                    Call OCCopyCheDiInformation(tmpID, tempTable.Rows(i - 1).Item("Stockid"))
                    ' tmpID = GetCurCheDiInfoIDIDFromSchediID(rsDb.Fields("车底ID").Value)
                    tmpXH = 1
                End If
                If tmpID > 0 Then
                    tmpXH = 1
                    'If rsDb.Fields("连挂车次").Value = "16008" Then Stop
                    nTrain = AddTrainInformation(Trim(tempTable.Rows(i - 1).Item("RoutingStyle")), Trim(tempTable.Rows(i - 1).Item("RunScaleStyle")), tempTable.Rows(i - 1).Item("StopScaleStyle"), tempTable.Rows(i - 1).Item("LinkTrainNum"))
                    TrainInf(nTrain).Train = Trim(tempTable.Rows(i - 1).Item("LinkTrainNum"))
                    TrainInf(nTrain).sPrintTrain = tempTable.Rows(i - 1).Item("PrintNum").ToString.Trim
                    TrainInf(nTrain).nZfLimit = Trim(tempTable.Rows(i - 1).Item("IfTurnFixed"))
                    TrainInf(nTrain).PrintLineStyle = GetLineStyleFromText(tempTable.Rows(i - 1).Item("LineShowStyle").ToString.Trim)
                    TrainInf(nTrain).PrintLineWidth = tempTable.Rows(i - 1).Item("LineShowWidth")
                    TrainInf(nTrain).PrintLineColor = System.Drawing.ColorTranslator.FromHtml(tempTable.Rows(i - 1).Item("LineShowColor"))
                    ' If nTrain = 0 Then Stop
                    If nTrain > 0 And TrainInf(nTrain).Train <> "" Then
                        ReDim Preserve ChediInfo(tmpID).nLinkTrain(UBound(ChediInfo(tmpID).nLinkTrain) + 1)
                        ChediInfo(tmpID).nLinkTrain(UBound(ChediInfo(tmpID).nLinkTrain)) = nTrain
                    End If
                    ChediInfo(tmpID).sCheCiHao = tempTable.Rows(i - 1).Item("StockID") 'Left(rsDb.Fields("输出车次").Value, 3)
                    ChediInfo(tmpID).PrintCheDiLinkStyle = GetLineStyleFromText(tempTable.Rows(i - 1).Item("StockShowStyle").ToString.Trim)
                    ChediInfo(tmpID).PrintCheDiLinkWidth = tempTable.Rows(i - 1).Item("StockShowWidth")
                    ChediInfo(tmpID).PrintCheDiLinkColor = System.Drawing.ColorTranslator.FromHtml(tempTable.Rows(i - 1).Item("StockShowColor").ToString.Trim)
                    If tempTable.Rows(i - 1).Item("IfTurnFixed") = 1 Then
                        ChediInfo(tmpID).bIfGouWang = True
                    Else
                        ChediInfo(tmpID).bIfGouWang = False
                    End If
                Else
                    MsgBox("车底信息与当前运行图信息不对应，请检查运行图和车底信息!", , "提示")
                End If
                tmpXH = tmpXH + 1
                'proBar.Value = 10 + Int(i * 40 / tempTable.Rows.Count)
            Next i
        End If

        For j = 1 To UBound(ChediInfo)
            nTempTrain = 0
            For p = 1 To UBound(ChediInfo(j).nLinkTrain)
                nTrain = ChediInfo(j).nLinkTrain(p)
                TrainInf(nTrain).SCheDiLeiXing = ChediInfo(j).SCheDiLeiXing
                If nTrain <> 0 And TrainInf(nTrain).Train <> "" Then
                    If nTempTrain = 0 Then
                        TrainInf(nTrain).TrainReturn(1) = 0
                        TrainInf(nTrain).TrainReturn(2) = 0
                    Else
                        TrainInf(nTempTrain).TrainReturn(2) = nTrain
                        TrainInf(nTrain).TrainReturn(1) = nTempTrain
                        TrainInf(nTrain).TrainReturn(2) = 0
                    End If
                    TrainInf(nTrain).nCheDiPuOrNot = 1
                    nTempTrain = nTrain
                End If
            Next p
        Next j
    End Sub

    '将时刻表读入
    Public Sub OCReadTimetableInf(ByVal sSKBID As String)
        Dim i, j, k, p As Integer
        Dim q As Integer
        Dim sTrainNum() As String
        ReDim sTrainNum(0)
        Dim nErrorTrain As Integer
        nErrorTrain = 0
        Dim nStaID As Integer
        Dim nifIn As Integer
        Dim sqlstr As String = ""
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                j = i
                TrainInf(j).TrainPuorNot = 1
                '*************************************************************************************************************************
                ReDim Preserve TrainInf(j).Starting(UBound(StationInf))
                ReDim Preserve TrainInf(j).Arrival(UBound(StationInf))
                ReDim Preserve TrainInf(j).StopLine(UBound(StationInf))
                '*************************************************************************************************************************
                For k = 1 To UBound(StationInf)
                    TrainInf(j).Starting(k) = -1
                    TrainInf(j).Arrival(k) = -1
                    TrainInf(j).StopLine(k) = ""
                Next k
                'LocalDataSet.TMS_TIMETABLEINFO.Rows.Clear()
                'LocalDataSet.Fill("TMS_TIMETABLEINFO", "TrainDiagramID='" & sSKBID & "'and TrainNum='" & TrainInf(i).Train & "' order by Seq")
                sqlstr = "SELECT * FROM TMS_TIMETABLEINFO WHERE TrainDiagramID='" & sSKBID & "'and TrainNum='" & TrainInf(i).Train & "' order by Seq"
                tempTable = New Data.DataTable
                tempTable = Globle.Method.ReadDataForAccess(sqlstr)
                If tempTable.Rows.Count > 0 Then
                    For p = 1 To tempTable.Rows.Count
                        nifIn = 0
                        If p = tempTable.Rows.Count - 1 Then
                            TrainInf(j).sStartZFStarting = tempTable.Rows(p - 1).Item("DepartTime") ' HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                            TrainInf(j).sStartZFArrival = tempTable.Rows(p - 1).Item("ArriTime") ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                            TrainInf(j).sStartZFLine = tempTable.Rows(p - 1).Item("StopTrack")
                        ElseIf p = tempTable.Rows.Count Then
                            TrainInf(j).sEndZFStarting = tempTable.Rows(p - 1).Item("DepartTime") 'HourToSecond(myTable3.Rows(p - 1).Item("发点"))
                            TrainInf(j).sEndZFArrival = tempTable.Rows(p - 1).Item("ArriTime")  ' HourToSecond(myTable3.Rows(p - 1).Item("到点"))
                            TrainInf(j).sEndZFLine = tempTable.Rows(p - 1).Item("StopTrack")
                        Else
                            For q = 1 To UBound(StationInf)
                                If StationInf(q).sStationName = tempTable.Rows(p - 1).Item("stationname").ToString.Trim Then
                                    nStaID = q
                                    TrainInf(j).Starting(nStaID) = tempTable.Rows(p - 1).Item("DepartTime")
                                    TrainInf(j).Arrival(nStaID) = tempTable.Rows(p - 1).Item("ArriTime")
                                    TrainInf(j).StopLine(nStaID) = tempTable.Rows(p - 1).Item("StopTrack")
                                    nifIn = 1
                                End If
                            Next
                            If nifIn = 0 Then
                                nErrorTrain = nErrorTrain + 1
                            End If
                        End If
                    Next p
                    TrainInf(j).lAllStartTime = TrainInf(j).Starting(TrainInf(j).nPathID(1))
                    TrainInf(j).lAllEndTime = TrainInf(j).Arrival(TrainInf(j).nPathID(UBound(TrainInf(j).nPathID)))
                End If
                'For p = 1 To UBound(TrainInf(i).nPathID)
                '    If TrainInf(i).Arrival(TrainInf(i).nPathID(p)) = -1 Then
                '        nErrorTrain = nErrorTrain + 1  
                '        Exit For
                '    End If
                'Next
            End If
            'proBar.Value = 50 + Int(i * 50 / UBound(TrainInf))
        Next i
        If nErrorTrain > 0 Then
            MsgBox("列车信息中有" & nErrorTrain & "趟列车没有时刻，请检查当前的底图结构是否选择错误！", , "提示")
        End If
    End Sub
    '通过运行图名称找版本号
    Public Function OCGetDiagramVersionFromName(ByVal sName As String) As String
        'LocalDataSet.TMS_TRAINDIAGRAMINFO.Clear()
        'LocalDataSet.Fill("TMS_TRAINDIAGRAMINFO")
        Dim sqlstr As String = "SELECT * FROM TMS_TRAINDIAGRAMINFO"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        OCGetDiagramVersionFromName = "空"
        Dim i As Integer
        For i = 1 To tempTable.Rows.Count
            If tempTable.Rows(i - 1).Item("TIMETABLENAME") = sName Then
                Return tempTable.Rows(i - 1).Item("TRAINDIAGRAMID")
                Exit For
            End If
        Next
    End Function
    Public Function OCGetStaCodeFromStaName(ByVal sStaName As String) As String
        OCGetStaCodeFromStaName = ""
        Dim i, j As Integer
        For i = 1 To UBound(NetInf.Line)
            For j = 1 To UBound(NetInf.Line(i).Station)
                If NetInf.Line(i).Station(j).sStaName = sStaName Then
                    OCGetStaCodeFromStaName = NetInf.Line(i).Station(j).sStaCode
                End If
            Next
        Next
    End Function
    '复制车底信息
    Public Sub OCCopyCheDiInformation(ByVal nNewCDid As Integer, ByVal sCheDiID As String)
        Dim i As Integer
        i = 1
        With ChediInfo(nNewCDid)
            .SCheDiLeiXing = BaseChediInfo(i).SCheDiLeiXing
            .sCheDiID = nNewCDid 'BaseChediInfo(i).sCheDiID
            .bIfAutoResetCheCi = True
            If sCheDiID = "NULL" Then
                sCheDiID = ""
            End If
            .sCheCiHao = sCheDiID
            ReDim .nLinkTrain(0)
            .PrintCheDiLinkStyle = GetLineStyleFromText(TimeTablePara.DiagramStylePara.CheDiLineStyle)
            .PrintCheDiLinkWidth = TimeTablePara.DiagramStylePara.CheDiLineWidth
            .PrintCheDiLinkColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.CheDiLineColor)
        End With
    End Sub

  
End Module
