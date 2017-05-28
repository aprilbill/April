'gujianghe begin
Module modDrawJiShuTuJieLine
    Structure typeStaSwitchInf
        Dim SwitchID() As String
        Dim SwitchStaID As Integer
        Dim SwitchCADStaID As Integer
        Dim SwitchStaName As String
        Dim OddSwitchNum As Integer '奇数道岔，上行道岔
        Dim EvenSwitchNum As Integer '偶数道岔，下行道岔
    End Structure
    Public StaSwitchSet() As typeStaSwitchInf
    Public JishutujieVerInterval As Single
    Public Sub GetStaSwitchSet()
        Dim nCADStaInfoID As Integer
        Dim i As Integer, j As Integer
        Dim sTempStaName As String, nTempNum As Integer, nTempNum1 As Integer
        Dim p As Integer
        Dim sCrossNum() As String
        ReDim sCrossNum(0)
        ReDim StaSwitchSet(0)
        ReDim StaSwitchSet(0).SwitchID(0)
        Dim nIfIn As Integer
        For i = 1 To UBound(StationInf)
            ReDim Preserve StaSwitchSet(UBound(StaSwitchSet) + 1)

            nTempNum = UBound(StaSwitchSet)
            ReDim Preserve StaSwitchSet(nTempNum).SwitchID(0)
            nTempNum1 = UBound(StaSwitchSet(nTempNum).SwitchID) + 1
            ReDim Preserve StaSwitchSet(nTempNum).SwitchID(nTempNum1)
            StaSwitchSet(nTempNum).SwitchStaID = i
            StaSwitchSet(nTempNum).SwitchStaName = StationInf(i).sStationName
            sTempStaName = StationInf(i).sStationName
            If sTempStaName <> "" Then
                nCADStaInfoID = FromStaNameToCADStaInfID(sTempStaName) '通过车站名得到ｃａｄｉｄ的函数
                StaSwitchSet(nTempNum).SwitchCADStaID = nCADStaInfoID
                If nCADStaInfoID = 0 Then
                    Exit Sub
                End If
                StaSwitchSet(nTempNum).SwitchCADStaID = nCADStaInfoID
                For j = 1 To UBound(CADStaInf(nCADStaInfoID).Track)
                    nIfIn = 0
                    If CADStaInf(nCADStaInfoID).Track(j).sStyle = "道岔线" Then
                        For p = 1 To UBound(StaSwitchSet(nTempNum).SwitchID)
                            If StaSwitchSet(nTempNum).SwitchID(p) = CADStaInf(nCADStaInfoID).Track(j).sTrackNum Then  '已经添加
                                nIfIn = 1
                                Exit For
                            End If
                        Next
                        If nIfIn = 0 Then
                            ReDim Preserve StaSwitchSet(nTempNum).SwitchID(UBound(StaSwitchSet(nTempNum).SwitchID) + 1)
                            StaSwitchSet(nTempNum).SwitchID(UBound(StaSwitchSet(nTempNum).SwitchID)) = CADStaInf(nCADStaInfoID).Track(j).sTrackNum
                        End If
                    End If
                Next

            Else
            End If
        Next

        For i = 1 To UBound(StaSwitchSet)
            nTempNum = 0
            For j = 1 To UBound(StaSwitchSet(i).SwitchID)
                If Val(StaSwitchSet(i).SwitchID(j)) Mod 2 = 1 And Val(StaSwitchSet(i).SwitchID(j)) >= 1 Then
                    nTempNum = nTempNum + 1

                End If
            Next
            StaSwitchSet(i).OddSwitchNum = nTempNum
            nTempNum = 0
            For j = 1 To UBound(StaSwitchSet(i).SwitchID)
                If Val(StaSwitchSet(i).SwitchID(j)) Mod 2 = 0 And Val(StaSwitchSet(i).SwitchID(j)) >= 1 Then
                    nTempNum = nTempNum + 1

                End If
            Next
            StaSwitchSet(i).EvenSwitchNum = nTempNum
        Next

        Call SortSwitchSet(1) 'sort the switch id by ascending order

        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                ReDim TrainInf(i).PassCrossing(UBound(TrainInf(i).nPathID))
                For j = 1 To UBound(TrainInf(i).nPathID)
                    ReDim TrainInf(i).PassCrossing(j).nFirstPassNum(0)
                    ReDim TrainInf(i).PassCrossing(j).nFirstPassTime(0)
                    ReDim TrainInf(i).PassCrossing(j).nSecondPassNum(0)
                    ReDim TrainInf(i).PassCrossing(j).nSecondPassTime(0)
                Next
            End If
        Next
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                For j = 1 To UBound(TrainInf(i).nPathID)
                    TrainInf(i).PassCrossing(j).nSta = TrainInf(i).nPathID(j)
                Next
            End If
        Next
    End Sub

    Public Sub SetPassCrossingInfo()
        ' Call TrainRunFirstSet()
        'Dim i As Integer, j As Integer
        'Dim k As Integer
        'Dim IfIn As Integer
        'Dim p As Integer
        'Dim nStaID As Integer
        'Dim nTrackId As Integer
        'Dim nCurNum As Integer
        'For i = 1 To UBound(TrainInf)
        '    If TrainInf(i).Train <> "" Then
        '        For j = 1 To UBound(TrainInf(i).nPathID)
        '            For k = 1 To UBound(TrainRunTrack(i).nTrackID)
        '                nStaID = TrainRunTrack(i).nTrackID(k).nStaID
        '                nTrackId = TrainRunTrack(i).nTrackID(k).nTrackID
        '                If CADStaInf(nStaID).sStaName = StationInf(TrainInf(i).nPathID(j)).sStationName Then
        '                    If CADStaInf(nStaID).Track(nTrackId).sStyle = "道岔线" Then
        '                        If CADStaInf(nStaID).Track(nTrackId).sTrackNum <> "" Then
        '                            nCurNum = Val(CADStaInf(nStaID).Track(nTrackId).sTrackNum)
        '                            If nCurNum Mod 2 = 0 Then
        '                                IfIn = 0
        '                                For p = 1 To UBound(TrainInf(i).PassCrossing(j).nFirstPassNum)
        '                                    If TrainInf(i).PassCrossing(j).nFirstPassNum(p) = nCurNum Then
        '                                        IfIn = 1
        '                                        Exit For
        '                                    End If
        '                                Next
        '                                If IfIn = 0 Then
        '                                    ReDim Preserve TrainInf(i).PassCrossing(j).nSecondPassNum(UBound(TrainInf(i).PassCrossing(j).nSecondPassNum) + 1)
        '                                    ReDim Preserve TrainInf(i).PassCrossing(j).nSecondPassTime(UBound(TrainInf(i).PassCrossing(j).nSecondPassTime) + 1)
        '                                    TrainInf(i).PassCrossing(j).nSecondPassNum(UBound(TrainInf(i).PassCrossing(j).nSecondPassNum)) = nCurNum
        '                                    TrainInf(i).PassCrossing(j).nSecondPassTime(UBound(TrainInf(i).PassCrossing(j).nSecondPassTime)) = 60
        '                                End If
        '                            Else
        '                                IfIn = 0
        '                                For p = 1 To UBound(TrainInf(i).PassCrossing(j).nFirstPassNum)
        '                                    If TrainInf(i).PassCrossing(j).nFirstPassNum(p) = nCurNum Then
        '                                        IfIn = 1
        '                                        Exit For
        '                                    End If
        '                                Next
        '                                If IfIn = 0 Then
        '                                    ReDim Preserve TrainInf(i).PassCrossing(j).nFirstPassNum(UBound(TrainInf(i).PassCrossing(j).nFirstPassNum) + 1)
        '                                    ReDim Preserve TrainInf(i).PassCrossing(j).nFirstPassTime(UBound(TrainInf(i).PassCrossing(j).nFirstPassTime) + 1)
        '                                    TrainInf(i).PassCrossing(j).nFirstPassNum(UBound(TrainInf(i).PassCrossing(j).nFirstPassNum)) = nCurNum
        '                                    TrainInf(i).PassCrossing(j).nFirstPassTime(UBound(TrainInf(i).PassCrossing(j).nFirstPassTime)) = 60
        '                                End If
        '                            End If
        '                        End If
        '                    End If
        '                End If
        '            Next
        '        Next
        '    End If
        'Next
    End Sub

    Public Sub SortSwitchSet(ByVal SortOrder As Integer)
        Dim i As Integer, j As Integer, k As Integer
        Dim tempStr As String

        If SortOrder = 1 Then ' 升序 ascending
            For i = 1 To UBound(StaSwitchSet)
                For j = 1 To UBound(StaSwitchSet(i).SwitchID)
                    For k = j + 1 To UBound(StaSwitchSet(i).SwitchID)
                        If Val(StaSwitchSet(i).SwitchID(j)) > Val(StaSwitchSet(i).SwitchID(k)) Then
                            tempStr = StaSwitchSet(i).SwitchID(j)
                            StaSwitchSet(i).SwitchID(j) = StaSwitchSet(i).SwitchID(k)
                            StaSwitchSet(i).SwitchID(k) = tempStr

                        End If
                    Next
                Next
            Next

        End If
        If SortOrder = -1 Then 'descending
            For i = 1 To UBound(StaSwitchSet)
                For j = 1 To UBound(StaSwitchSet(i).SwitchID)
                    For k = j + 1 To UBound(StaSwitchSet(i).SwitchID)
                        If Val(StaSwitchSet(i).SwitchID(j)) < Val(StaSwitchSet(i).SwitchID(k)) Then
                            tempStr = StaSwitchSet(i).SwitchID(j)
                            StaSwitchSet(i).SwitchID(j) = StaSwitchSet(i).SwitchID(k)
                            StaSwitchSet(i).SwitchID(k) = tempStr

                        End If
                    Next
                Next
            Next

        End If
    End Sub

    '画技术图解
    Public Sub DrawGuDaoJiShuTuJieBackup(ByVal nStaId As Integer, ByVal rBmpGraphics As Graphics, ByVal PicWidth As Single, ByVal PicHeight As Single, _
                                        ByVal LeftBlank As Single, ByVal topBlank As Single, ByVal StaBlank As Single, _
                                        ByVal sngTimeBlank As Single, ByVal sngLeftX As Single, ByVal sngTopY As Single, _
                                        ByVal nFirTime As Integer, ByVal nTimeWidth As Integer)
        Dim i As Integer, j As Integer
        Dim nSearchFlag As Integer

        GuDaoJishutujie.nSta = nStaId
        Dim nCurY As Single
        nCurY = 40
        ReDim GuDaoJishutujie.sGuDao(UBound(StationInf(nStaId).sStLineNo))
        ReDim GuDaoJishutujie.GuDaoYcoord(UBound(StationInf(nStaId).sStLineNo))
        ReDim GuDaoJishutujie.nUpSta(0)
        ReDim GuDaoJishutujie.UpStaYcoord(0)
        ReDim GuDaoJishutujie.nDownSta(0)
        ReDim GuDaoJishutujie.DownStaYcoord(0)

        ReDim GuDaoJishutujie.nUpSwitches(0)
        ReDim GuDaoJishutujie.nUpSwitchesYCoord(0)
        ReDim GuDaoJishutujie.nDownSwitches(0)
        ReDim GuDaoJishutujie.nDownSwitchesYCoord(0)


        For i = 1 To UBound(SectionInf)
            If StationInf(SectionInf(i).nSecStaID).sStationName = StationInf(nStaId).sStationName Then
                ReDim Preserve GuDaoJishutujie.nUpSta(UBound(GuDaoJishutujie.nUpSta) + 1)
                GuDaoJishutujie.nUpSta(UBound(GuDaoJishutujie.nUpSta)) = SectionInf(i).nFirStaID
                ReDim Preserve GuDaoJishutujie.UpStaYcoord(UBound(GuDaoJishutujie.UpStaYcoord) + 1)
            End If
        Next

        nCurY = nCurY + 60
        For i = 1 To UBound(StationInf(nStaId).sStLineNo)
            GuDaoJishutujie.sGuDao(i) = StationInf(nStaId).sStLineNo(i)
        Next

        nCurY = nCurY + 90
        For i = 1 To UBound(SectionInf)
            If StationInf(SectionInf(i).nFirStaID).sStationName = StationInf(nStaId).sStationName Then
                ReDim Preserve GuDaoJishutujie.nDownSta(UBound(GuDaoJishutujie.nDownSta) + 1)
                GuDaoJishutujie.nDownSta(UBound(GuDaoJishutujie.nDownSta)) = SectionInf(i).nSecStaID
                ReDim Preserve GuDaoJishutujie.DownStaYcoord(UBound(GuDaoJishutujie.DownStaYcoord) + 1)
            End If
        Next
        ' rbmpGraphics.Clear(Color.White)
        'Call DrawGuDaoJiShuTuJieTimeLine(rBmpGraphics, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight, leftBlank, topBlank, StaBlank, TimeTablePara.TimeTableDiagramPara.sngTimeBlank, 0, 0, 6, 4)
        'If TimeTablePara.sPubCurSkbName <> "" Then
        '    Call DrawJiShuTuJieDiagramLine(rBmpGraphics, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, 6, 4, leftBlank, StaBlank, 0)
        'End If

        '技术图解的道岔信息赋初始值 
        'gujianghe begin
        nSearchFlag = 0
        For i = 1 To UBound(StaSwitchSet)
            If StaSwitchSet(i).SwitchStaID = nStaId Then '找到对应的车站
                nSearchFlag = 1
                For j = 1 To UBound(StaSwitchSet(i).SwitchID)

                    If Val(StaSwitchSet(i).SwitchID(j)) Mod 2 = 1 Then '上行道岔
                        ReDim Preserve GuDaoJishutujie.nUpSwitches(UBound(GuDaoJishutujie.nUpSwitches) + 1)
                        ReDim Preserve GuDaoJishutujie.nUpSwitchesYCoord(UBound(GuDaoJishutujie.nUpSwitchesYCoord) + 1)
                        GuDaoJishutujie.nUpSwitches(UBound(GuDaoJishutujie.nUpSwitches)) = StaSwitchSet(i).SwitchID(j)

                    ElseIf Val(StaSwitchSet(i).SwitchID(j)) > 1 Then  '下行道岔
                        ReDim Preserve GuDaoJishutujie.nDownSwitches(UBound(GuDaoJishutujie.nDownSwitches) + 1)
                        ReDim Preserve GuDaoJishutujie.nDownSwitchesYCoord(UBound(GuDaoJishutujie.nDownSwitchesYCoord) + 1)
                        GuDaoJishutujie.nDownSwitches(UBound(GuDaoJishutujie.nDownSwitches)) = StaSwitchSet(i).SwitchID(j)

                    End If

                Next
                Exit For
            End If
        Next
        'gujianghe end
        Call DrawGuDaoJiShuTuJieTimeLineBackup(rBmpGraphics, PicWidth, PicHeight, LeftBlank, topBlank, StaBlank, TimeTablePara.TimeTableDiagramPara.sngTimeBlank, sngLeftX, sngTopY, nFirTime, nTimeWidth)
        If TimeTablePara.sPubCurSkbName <> "" Then
            Call DrawJiShuTuJieDiagramLineBackup(rBmpGraphics, PicWidth, nFirTime, nTimeWidth, LeftBlank, StaBlank, sngLeftX)
        End If

    End Sub

    '画股道技术图解股道线
    Public Sub DrawGuDaoJiShuTuJieTimeLineBackup(ByVal rBmpGraphics As Graphics, ByVal PicWidth As Single, ByVal PicHeight As Single, _
                                        ByVal LeftBlank As Single, ByVal topBlank As Single, ByVal StaBlank As Single, _
                                        ByVal sngTimeBlank As Single, ByVal sngLeftX As Single, ByVal sngTopY As Single, _
                                        ByVal nFirTime As Integer, ByVal nTimeWidth As Integer)

        Dim i, j As Integer
        Dim intTimeLine As Single
        Dim sngMinuWidth As Single
        Dim sngToWidth As Single
        Dim sngHeight As Single
        Dim intFirstTime As Integer
        intFirstTime = nFirTime
        sngToWidth = PicWidth - LeftBlank * 2 - StaBlank - sngLeftX
        sngHeight = PicHeight - 2 * topBlank
        Dim TopLineY As Single, TopFlag As Integer
        Dim BottomLineY As Single
        Dim nCurY As Single
        Dim tmpCurY As Single
        Dim VerInterval As Single
        Dim tempNum As Integer
        tempNum = UBound(GuDaoJishutujie.nUpSta) + UBound(GuDaoJishutujie.nUpSwitches) + UBound(GuDaoJishutujie.sGuDao) _
        + UBound(GuDaoJishutujie.nDownSwitches) + UBound(GuDaoJishutujie.nDownSta)
        If tempNum <> 0 Then
            VerInterval = sngHeight / tempNum
        Else
            VerInterval = 0
        End If
        If VerInterval > 40 Then
            VerInterval = 40
        End If
        JishutujieVerInterval = VerInterval

        nCurY = topBlank + sngTopY + sngTimeBlank / 2
        TopFlag = 0
        If UBound(GuDaoJishutujie.nUpSta) >= 1 Then
            TopFlag = 1
            'nCurY = nCurY + VerInterval
            tmpCurY = nCurY
            TopLineY = tmpCurY
        End If

        For i = 1 To UBound(GuDaoJishutujie.nUpSta)
            nCurY = tmpCurY + (i - 1) * VerInterval
            GuDaoJishutujie.UpStaYcoord(i) = nCurY
        Next
        '计算道岔区域纵坐标 并为之赋值
        'gujianghe begin 12/03
        If UBound(GuDaoJishutujie.nUpSwitches) >= 1 Then

            nCurY = nCurY + VerInterval
            tmpCurY = nCurY
            If TopFlag = 0 Then
                TopFlag = 1
                TopLineY = tmpCurY

            End If
        End If


        For i = 1 To UBound(GuDaoJishutujie.nUpSwitches)
            nCurY = tmpCurY + (i - 1) * VerInterval
            GuDaoJishutujie.nUpSwitchesYCoord(i) = nCurY
        Next
        'gujianghe end 12/03
        If UBound(GuDaoJishutujie.sGuDao) Then
            nCurY = nCurY + VerInterval
            tmpCurY = nCurY
            If TopFlag = 0 Then
                TopFlag = 1
                TopLineY = tmpCurY

            End If
        End If


        For i = 1 To UBound(GuDaoJishutujie.sGuDao)
            nCurY = tmpCurY + (i - 1) * VerInterval
            GuDaoJishutujie.GuDaoYcoord(i) = nCurY
        Next
        If UBound(GuDaoJishutujie.nDownSwitches) >= 1 Then
            nCurY = nCurY + UBound(GuDaoJishutujie.nDownSwitches) * VerInterval
            tmpCurY = nCurY
            If TopFlag = 0 Then
                TopFlag = 1
                TopLineY = tmpCurY

            End If
        End If

        For i = 1 To UBound(GuDaoJishutujie.nDownSwitches)
            nCurY = tmpCurY - (i - 1) * VerInterval
            GuDaoJishutujie.nDownSwitchesYCoord(i) = nCurY
        Next
        If UBound(GuDaoJishutujie.nDownSta) >= 1 Then
            If UBound(GuDaoJishutujie.nDownSwitches) >= 1 Then
                nCurY = tmpCurY + VerInterval
            Else
                nCurY = nCurY + VerInterval
            End If

            tmpCurY = nCurY
            If TopFlag = 0 Then
                TopFlag = 1
                TopLineY = tmpCurY

            End If
        End If

        For i = 1 To UBound(GuDaoJishutujie.nDownSta)
            nCurY = nCurY + (i - 1) * VerInterval
            GuDaoJishutujie.DownStaYcoord(i) = nCurY
        Next


        '#####################'车站名称#######################
        Dim maxY As Single
        Dim tmpY1 As Single
        'Dim tmpY2 As Single
        Dim tmpY As Single
        Dim YBix As Single
        Dim brsStation As Brush
        Dim clrStation As Color
        tmpY1 = 0 ' StationInf(1).Ycord
        maxY = 0
        For i = 1 To UBound(StationInf)
            If StationInf(i).Ycord > maxY Then
                maxY = StationInf(i).Ycord
            End If
        Next
        YBix = 1

        '上面车站横线
        For i = 1 To UBound(GuDaoJishutujie.nUpSta)
            tmpY = GuDaoJishutujie.UpStaYcoord(i)
            If StationInf(i).sStationName.Substring(0, 2) = "车场" Or StationInf(i).sStationName.Substring(StationInf(i).sStationName.Length - 2) = "车场" Then
                clrStation = Color.Orange
            Else
                clrStation = Color.Green
            End If
            If i = 1 Then
                clrStation = Color.Green
            Else
                clrStation = Color.Green
            End If
            rBmpGraphics.DrawLine(New System.Drawing.Pen(clrStation, 2), LeftBlank + StaBlank + sngLeftX, tmpY, LeftBlank + StaBlank + sngLeftX + sngToWidth, tmpY)
            brsStation = Brushes.Green
            rBmpGraphics.DrawString(StationInf(GuDaoJishutujie.nUpSta(i)).sStationName, New Font("黑体", 11), brsStation, LeftBlank + sngLeftX, tmpY + VerInterval / 2)

            'If i = 1 Then
            '    TopLineY = tmpY
            'End If
        Next i

        '画上行道岔
        'gujianghe start
        For i = 1 To UBound(GuDaoJishutujie.nUpSwitches)
            tmpY = GuDaoJishutujie.nUpSwitchesYCoord(i)
            If i = 1 Then
                clrStation = Color.Green
            Else
                clrStation = Color.Green
            End If

            rBmpGraphics.DrawLine(New System.Drawing.Pen(clrStation, 2), LeftBlank + StaBlank + sngLeftX, tmpY, LeftBlank + StaBlank + sngLeftX + sngToWidth, tmpY)

            brsStation = Brushes.Green
            rBmpGraphics.DrawString("道岔" & GuDaoJishutujie.nUpSwitches(i), New Font("黑体", 11), brsStation, LeftBlank + sngLeftX, tmpY + VerInterval / 2)
        Next
        'gujianghe end
        '股道横线
        For i = 1 To UBound(GuDaoJishutujie.sGuDao)
            tmpY = GuDaoJishutujie.GuDaoYcoord(i)
            If i = 1 Then
                clrStation = Color.Green
            Else
                clrStation = Color.Green
            End If
            rBmpGraphics.DrawLine(New System.Drawing.Pen(clrStation, 2), LeftBlank + StaBlank + sngLeftX, tmpY, LeftBlank + StaBlank + sngLeftX + sngToWidth, tmpY)
            brsStation = Brushes.Green
            rBmpGraphics.DrawString("股道" & GuDaoJishutujie.sGuDao(i), New Font("黑体", 11), brsStation, LeftBlank + sngLeftX, tmpY + VerInterval / 2)
            'If i = 1 Then
            '    GuDaoUpLineY = tmpY
            'End If
            'If i = UBound(GuDaoJishutujie.sGuDao) Then
            '    GuDaoDownLineY = tmpY
            'End If
        Next i
        'tmpY2 = GuDaoDownLineY
        'tmpY = tmpY2 + 50 '(tmpY2 - tmpY1) * YBix + topBlank + sngTopY
        'rBmpGraphics.DrawLine(New System.Drawing.Pen(clrStation, 2), LeftBlank + StaBlank + sngLeftX, tmpY, LeftBlank + StaBlank + sngLeftX + sngToWidth, tmpY)
        'brsStation = Brushes.Green
        '' rBmpGraphics.DrawString(GuDaoJishutujie.sGuDao(i), New Font("黑体", 11), brsStation, LeftBlank + sngLeftX, tmpY - 6)
        'GuDaoDownLineY = tmpY

        '  画下行道岔
        'gujianghe start 12/03
        For i = 1 To UBound(GuDaoJishutujie.nDownSwitches)
            tmpY = GuDaoJishutujie.nDownSwitchesYCoord(i)
            If i = 1 Then
                clrStation = Color.Green
            Else
                clrStation = Color.Green
            End If
            rBmpGraphics.DrawLine(New System.Drawing.Pen(clrStation, 2), LeftBlank + StaBlank + sngLeftX, tmpY, LeftBlank + StaBlank + sngLeftX + sngToWidth, tmpY)
            brsStation = Brushes.Green
            rBmpGraphics.DrawString("道岔" & GuDaoJishutujie.nDownSwitches(i), New Font("黑体", 11), brsStation, LeftBlank + sngLeftX, tmpY + VerInterval / 2)
        Next

        'gujianghe end 12/03
        '下面车站横线
        For i = 1 To UBound(GuDaoJishutujie.nDownSta)
            tmpY = GuDaoJishutujie.DownStaYcoord(i)
            If StationInf(i).sStationName.Substring(0, 2) = "车场" Or StationInf(i).sStationName.Substring(StationInf(i).sStationName.Length - 2) = "车场" Then
                clrStation = Color.Orange
            Else
                clrStation = Color.Green
                If i = 1 Then
                    clrStation = Color.Green
                Else
                    clrStation = Color.Green
                End If
            End If

            rBmpGraphics.DrawLine(New System.Drawing.Pen(clrStation, 2), LeftBlank + StaBlank + sngLeftX, tmpY, LeftBlank + StaBlank + sngLeftX + sngToWidth, tmpY)
            brsStation = Brushes.Green
            rBmpGraphics.DrawString(StationInf(GuDaoJishutujie.nDownSta(i)).sStationName, New Font("黑体", 11), brsStation, LeftBlank + sngLeftX, tmpY + VerInterval / 2)
        Next i
        '画最后一根横线
        tmpY = tmpY + VerInterval
        clrStation = Color.Green
        rBmpGraphics.DrawLine(New System.Drawing.Pen(clrStation, 2), LeftBlank + StaBlank + sngLeftX, tmpY, LeftBlank + StaBlank + sngLeftX + sngToWidth, tmpY)

        BottomLineY = tmpY

        Dim strTimePrint As String
        Dim tmpPen As Pen
        Dim sngY1, sngY2 As Single
        sngY1 = TopLineY
        sngY2 = BottomLineY
        Select Case TimeTablePara.TimeTableDiagramPara.strTimeFormat
            Case "小时格"
                intTimeLine = nTimeWidth  '10 * 6 * 24
                sngMinuWidth = sngToWidth / (intTimeLine)
                For i = 1 To intTimeLine + 1
                    rBmpGraphics.DrawLine(New System.Drawing.Pen(Color.Green, 2), LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)
                    strTimePrint = Trim(Str((intFirstTime + i - 1) Mod 24) & ":00")
                    '打印时间文字,第一行
                    rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                    '最后一行
                    rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                Next
            Case "十分格"
                intTimeLine = 6 * nTimeWidth
                sngMinuWidth = sngToWidth / (intTimeLine)
                For i = 1 To intTimeLine + 1
                    If (i - 1) Mod 6 = 0 Then
                        rBmpGraphics.DrawLine(New System.Drawing.Pen(Color.Green, 2), LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)
                        strTimePrint = Trim(Str((intFirstTime + (Int((i - 1) / 6))) Mod 24) & ":00")
                        '打印时间文字,第一行
                        rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                        '最后一行
                        rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                    Else
                        If (i - 1) Mod 3 = 0 Then
                            tmpPen = New System.Drawing.Pen(Color.Green, 1)
                            tmpPen.DashStyle = Drawing2D.DashStyle.Dash
                            rBmpGraphics.DrawLine(tmpPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)

                            strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                            'If strTimePrint = "8:50" Then Stop
                            'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                            '打印时间文字,第一行
                            rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                            rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)

                        Else
                            tmpPen = New System.Drawing.Pen(Color.Green, 1)
                            'tmpPen.DashStyle = Drawing2D.DashStyle.Dash
                            rBmpGraphics.DrawLine(tmpPen, LeftBlank + StaBlank + sngLeftX + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)
                            strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                            'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                            '打印时间文字,第一行
                            'If strTimePrint = "8:50" Then Stop
                            rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                            '最后一行
                            rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                        End If
                    End If
                Next
            Case "二分格"
                intTimeLine = 6 * nTimeWidth
                sngMinuWidth = sngToWidth / (intTimeLine)
                For i = 1 To intTimeLine + 1
                    If (i - 1) Mod 6 = 0 Then
                        rBmpGraphics.DrawLine(New System.Drawing.Pen(Color.Green, 3), LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)
                        strTimePrint = Trim(Str((intFirstTime + (Int((i - 1) / 6))) Mod 24) & ":00")
                        '打印时间文字,第一行
                        rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                        '最后一行
                        rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                    Else
                        If (i - 1) Mod 3 = 0 Then
                            tmpPen = New System.Drawing.Pen(Color.Green, 2)
                            tmpPen.DashStyle = Drawing2D.DashStyle.Dash
                            rBmpGraphics.DrawLine(tmpPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)

                            strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                            'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                            '打印时间文字,第一行
                            rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                            '最后一行
                            rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + StaBlank + (i - 1) * sngMinuWidth - 12, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)

                        Else
                            tmpPen = New System.Drawing.Pen(Color.Green, 2)
                            'tmpPen.DashStyle = Drawing2D.DashStyle.Dash
                            rBmpGraphics.DrawLine(tmpPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)
                            strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                            'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                            '打印时间文字,第一行
                            rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + sngLeftX + (i - 1) * sngMinuWidth - 12, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                            '最后一行
                            rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + sngLeftX + (i - 1) * sngMinuWidth - 12, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                        End If
                    End If
                    If i <= intTimeLine Then
                        For j = 1 To 4
                            rBmpGraphics.DrawLine(New System.Drawing.Pen(Color.Green, 1), LeftBlank + StaBlank + sngLeftX + (i - 1) * sngMinuWidth + j * sngMinuWidth / 5, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth + j * sngMinuWidth / 5, sngY2)
                        Next j
                    End If
                Next i
            Case "一分格"
                intTimeLine = 6 * nTimeWidth
                sngMinuWidth = sngToWidth / (intTimeLine)
                For i = 1 To intTimeLine + 1
                    If (i - 1) Mod 6 = 0 Then
                        rBmpGraphics.DrawLine(New System.Drawing.Pen(Color.Green, 3), LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)
                        strTimePrint = Trim(Str((intFirstTime + (Int((i - 1) / 6))) Mod 24) & ":00")
                        '打印时间文字,第一行
                        rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                        '最后一行
                        rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                    Else
                        If (i - 1) Mod 3 = 0 Then
                            tmpPen = New System.Drawing.Pen(Color.Green, 2)
                            tmpPen.DashStyle = Drawing2D.DashStyle.Dash
                            rBmpGraphics.DrawLine(tmpPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)

                            strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                            'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                            '打印时间文字,第一行
                            rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                            '最后一行
                            rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)

                        Else
                            tmpPen = New System.Drawing.Pen(Color.Green, 2)
                            rBmpGraphics.DrawLine(tmpPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth, sngY2)
                            strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                            'Str((intFirstTime + (Int((i - 1) / 6))) Mod 24)
                            '打印时间文字,第一行
                            rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, topBlank - TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY - 12)
                            '最后一行
                            rBmpGraphics.DrawString(strTimePrint, New Font("黑体", 11), Brushes.Green, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth - 12, PicHeight - topBlank + TimeTablePara.TimeTableDiagramPara.sngTimeBlank + sngTopY)
                        End If
                    End If
                    If i <= intTimeLine Then
                        For j = 1 To 9
                            If j = 5 Then
                                tmpPen = New System.Drawing.Pen(Color.Green, 1)
                                tmpPen.DashStyle = Drawing2D.DashStyle.Dot
                                rBmpGraphics.DrawLine(tmpPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth + j * sngMinuWidth / 10, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth + j * sngMinuWidth / 10, sngY2)
                            Else
                                tmpPen = New System.Drawing.Pen(Color.Green, 1)
                                'tmpPen.DashStyle = Drawing2D.DashStyle.Dot
                                rBmpGraphics.DrawLine(tmpPen, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth + j * sngMinuWidth / 10, sngY1, LeftBlank + sngLeftX + StaBlank + (i - 1) * sngMinuWidth + j * sngMinuWidth / 10, sngY2)
                            End If
                        Next j
                    End If
                Next i
        End Select
    End Sub

    '画技术图解运行线
    Public Sub DrawJiShuTuJieDiagramLineBackup(ByVal rBmpGraphics As Graphics, ByVal intToWidth As Integer, ByVal intFirTime As Integer, ByVal nTimeWidth As Integer, ByVal intLeftBlank As Integer, ByVal intStaBlank As Integer, ByVal intLeftX As Integer)
        Dim i As Integer
        Dim j As Integer, k As Integer, m As Integer, s As Integer
        Dim TmpPen1 As Pen
        'Dim k As Integer
        Dim X1, X2 As Single
        Dim Y1, Y2 As Single
        Dim X3, Y3, X4, Y4 As Single

        Dim CrossX1 As Single, CrossX2 As Single
        Dim CrossY1 As Single, CrossY2 As Single
        Dim CrossX3 As Single, CrossY3 As Single
        Dim CrossX4 As Single, CrossY4 As Single

        Dim UpCrossY1 As Single
        Dim UpCrossY2 As Single

        Dim DownCrossY1 As Single
        Dim DownCrossY2 As Single


        Dim UpX, UpY As Single
        Dim UpStaX, UpStaY As Single
        Dim DownX, DownY As Single
        Dim DownStaX, DownStaY As Single
        Dim intTime1 As Integer
        Dim intTime2 As Integer
        Dim tmpPen As Pen
        Dim tmpDownBrush As Brush
        tmpDownBrush = Brushes.BlueViolet
        Dim tmpDownCrossBrush As Brush
        tmpDownCrossBrush = Brushes.Blue
        Dim tmpUpBrush As Brush
        tmpUpBrush = Brushes.Red
        Dim tmpUpCrossBrush As Brush
        tmpUpCrossBrush = Brushes.Green
        Dim CrossFlag As Integer, DownCrossFlag As Integer, UpCrossFlag As Integer
        'Dim tmpBrush As Brush, tmpBrush1 As Brush

        Dim sngToWidth As Single
        sngToWidth = (intToWidth - intLeftBlank * 2 - intStaBlank - intLeftX) * 24 / nTimeWidth
        Dim sngLeftX As Single
        Dim sngRightX As Single
        sngLeftX = FormTimeToXCord(intFirTime * 3600, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        sngRightX = FormTimeToXCord(TimeAdd(intFirTime * 3600, nTimeWidth * 3600), intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
        If sngRightX = sngLeftX Then
            sngRightX = sngLeftX + intToWidth - 2 * intLeftBlank - intStaBlank - intLeftX
        End If

        tmpPen = New Pen(Color.Blue, 1)
        TmpPen1 = New Pen(Color.Blue, 1)

        For j = 1 To UBound(TrainInf)
            If TrainInf(j).Train <> "" Then
                'If j = 91 Then Stop
                If UBound(TrainInf(j).nPassSection) > 0 Then
                    If j Mod 2 <> 0 Then '下行
                        ' If StationInf(SectionInf(TrainInf(j).nPassSection(1)).nFirStaID).sStationName = StationInf(GuDaoJishutujie.nSta).sStationName Then
                        If StationInf(TrainInf(j).nFirstID(1)).sStationName = StationInf(GuDaoJishutujie.nSta).sStationName Then
                            intTime1 = TrainInf(j).Arrival(GuDaoJishutujie.nSta) 'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                            X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                            Y1 = GetCurGuDaoYCood(TrainInf(j).StopLine(GuDaoJishutujie.nSta))
                            intTime2 = TrainInf(j).Starting(GuDaoJishutujie.nSta) 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                            X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                            Y2 = Y1
                            UpStaX = X2 + 20
                            UpStaY = JishutujieVerInterval / 4 + GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(1)).nSecStaID).sStationName, 2)
                            'DownStaY = JishutujieVerInterval / 4 + GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(1)).nSecStaID).sStationName, 2)

                            UpX = X2
                            UpY = GuDaoJishutujie.GuDaoYcoord(UBound(GuDaoJishutujie.GuDaoYcoord)) + JishutujieVerInterval
                            'gujianghe begin
                            CrossX1 = X1
                            CrossY1 = Y1
                            CrossY2 = Y2
                            CrossFlag = 0
                            DownCrossFlag = 0
                            UpCrossFlag = 0

                            For k = 1 To UBound(TrainInf(j).PassCrossing)
                                CrossFlag = 1
                                If TrainInf(j).PassCrossing(k).nSta = GuDaoJishutujie.nSta Then
                                    '上端道岔占用铺画
                                    For m = UBound(TrainInf(j).PassCrossing(k).nFirstPassNum) To 1 Step -1
                                        UpCrossFlag = 1
                                        UpCrossY1 = CrossY1
                                        UpCrossY2 = CrossY2
                                        For s = 1 To UBound(GuDaoJishutujie.nUpSwitches)
                                            If Val(GuDaoJishutujie.nUpSwitches(s)) = Val(TrainInf(j).PassCrossing(k).nFirstPassNum(m)) Then
                                                '寻找道岔纵坐标
                                                CrossY1 = GuDaoJishutujie.nUpSwitchesYCoord(s) + JishutujieVerInterval / 4
                                                CrossY2 = CrossY1
                                                '寻找道岔占用时间横坐标

                                                Exit For
                                            End If
                                        Next
                                        CrossX2 = CrossX1


                                        rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY1, CrossX2, UpCrossY1)

                                        intTime1 = intTime1 - TrainInf(j).PassCrossing(k).nFirstPassTime(m)

                                        CrossX1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)

                                        'If m = 1 Then
                                        '    rBmpGraphics.DrawLine(tmpPen, CrossX1, CrossY1, CrossX1, UpStaY)
                                        'End If

                                        If CrossX1 >= 0 And CrossY1 >= 0 And CrossX2 >= 0 And CrossY2 >= 0 Then
                                            If CrossX2 >= CrossX1 Then
                                                If CrossX1 <= sngRightX Then
                                                    If CrossX2 <= sngRightX Then
                                                        ' rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                        ' rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                        ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                        'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                                        ' rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                                                        'gujianghe begin
                                                        rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)
                                                        rBmpGraphics.FillRectangle(tmpDownCrossBrush, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)

                                                        'gujianghe end
                                                    Else
                                                        CrossX3 = sngRightX
                                                        CrossY3 = CrossY1
                                                        'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                        ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                        'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                                                        rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)
                                                        rBmpGraphics.FillRectangle(tmpDownCrossBrush, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)

                                                    End If
                                                End If
                                            Else
                                                CrossX4 = CrossX1 - sngToWidth
                                                CrossY4 = CrossY1
                                                CrossX3 = intLeftBlank + intStaBlank + intLeftX
                                                CrossY3 = CrossY1
                                                'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                                rBmpGraphics.DrawRectangle(TmpPen1, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)
                                                rBmpGraphics.FillRectangle(tmpDownCrossBrush, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)

                                            End If
                                        End If

                                    Next
                                    Exit For
                                End If
                            Next

                            'gujianghe end

                            If X1 >= 0 And Y1 >= 0 And X2 >= 0 And Y2 >= 0 Then
                                If X2 >= X1 Then
                                    If X1 <= sngRightX Then
                                        If X2 <= sngRightX Then
                                            ' rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                            ' rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                            ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                            'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                            ' rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                                            'gujianghe begin
                                            ' rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X2 - X1, JishutujieVerInterval / 4)
                                            rBmpGraphics.FillRectangle(tmpDownBrush, X1, Y1, X2 - X1, JishutujieVerInterval / 4)

                                            'gujianghe end
                                        Else
                                            X3 = sngRightX
                                            Y3 = Y1
                                            'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                            ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                            'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                                            ' rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X3 - X1, JishutujieVerInterval / 4)
                                            rBmpGraphics.FillRectangle(tmpDownBrush, X1, Y1, X3 - X1, JishutujieVerInterval / 4)

                                        End If
                                    End If
                                Else
                                    X4 = X1 - sngToWidth
                                    Y4 = Y1
                                    X3 = intLeftBlank + intStaBlank + intLeftX
                                    Y3 = Y1
                                    'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                    'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                    'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                    rBmpGraphics.FillRectangle(tmpDownBrush, X3, Y3, X2 - X3, JishutujieVerInterval / 4)

                                End If
                            End If
                            ' 下行列车下方道岔铺画
                            'gujianghe begin
                            CrossX2 = X2
                            CrossY1 = Y2
                            CrossY2 = Y2
                            DownStaY = JishutujieVerInterval / 4 + GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(1)).nSecStaID).sStationName, 2)
                            For k = 1 To UBound(TrainInf(j).PassCrossing)
                                If TrainInf(j).PassCrossing(k).nSta = GuDaoJishutujie.nSta Then
                                    ' 下端道岔占用铺画
                                    For m = UBound(TrainInf(j).PassCrossing(k).nSecondPassNum) To 1 Step -1
                                        DownCrossY1 = CrossY1
                                        DownCrossY2 = CrossY2
                                        For s = 1 To UBound(GuDaoJishutujie.nDownSwitches)
                                            If Val(GuDaoJishutujie.nDownSwitches(s)) = Val(TrainInf(j).PassCrossing(k).nSecondPassNum(m)) Then
                                                '寻找道岔纵坐标
                                                CrossY1 = GuDaoJishutujie.nDownSwitchesYCoord(s) + JishutujieVerInterval / 4
                                                CrossY2 = CrossY1
                                                '寻找道岔占用时间横坐标

                                                Exit For
                                            End If
                                        Next
                                        CrossX1 = CrossX2

                                        rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY1, CrossX2, DownCrossY1)

                                        intTime2 = intTime2 + TrainInf(j).PassCrossing(k).nSecondPassTime(m)

                                        CrossX2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                        If m = 1 Then
                                            rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY1, CrossX2, DownStaY)
                                        End If

                                        If CrossX1 >= 0 And CrossY1 >= 0 And CrossX2 >= 0 And CrossY2 >= 0 Then
                                            If CrossX2 >= CrossX1 Then
                                                If CrossX1 <= sngRightX Then
                                                    If CrossX2 <= sngRightX Then
                                                        ' rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                        ' rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                        ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                        'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                                        ' rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                                                        'gujianghe begin
                                                        rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)
                                                        rBmpGraphics.FillRectangle(tmpDownCrossBrush, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)

                                                        'gujianghe end
                                                    Else
                                                        CrossX3 = sngRightX
                                                        CrossY3 = CrossY1
                                                        'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                        ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                        'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                                                        rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)
                                                        rBmpGraphics.FillRectangle(tmpDownCrossBrush, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)

                                                    End If
                                                End If
                                            Else
                                                CrossX4 = CrossX1 - sngToWidth
                                                CrossY4 = CrossY1
                                                CrossX3 = intLeftBlank + intStaBlank + intLeftX
                                                CrossY3 = CrossY1
                                                'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                                rBmpGraphics.DrawRectangle(TmpPen1, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)
                                                rBmpGraphics.FillRectangle(tmpDownCrossBrush, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)

                                            End If
                                        End If

                                    Next
                                    Exit For
                                End If
                            Next
                            'gujianghe end

                            ''始发折返
                            'If TrainInf(j).TrainReturn(1) > 0 Then
                            '    intTime1 = TrainInf(j).sStartZFArrival  'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                            '    X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                            '    Y1 = GetCurGuDaoYCood(TrainInf(j).sStartZFLine)
                            '    intTime2 = TrainInf(j).sStartZFStarting 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                            '    X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                            '    Y2 = Y1
                            '    'gujianghe begin
                            '    CrossX1 = X1
                            '    CrossY1 = Y1
                            '    CrossY2 = Y2
                            '    For k = 1 To UBound(TrainInf(j).PassCrossing)
                            '        If TrainInf(j).PassCrossing(k).nSta = GuDaoJishutujie.nSta Then
                            '            '上端道岔占用铺画
                            '            For m = UBound(TrainInf(j).PassCrossing(k).nFirstPassNum) To 1 Step -1
                            '                UpCrossY1 = CrossY1
                            '                UpCrossY2 = CrossY2
                            '                For s = 1 To UBound(GuDaoJishutujie.nUpSwitches)
                            '                    If Val(GuDaoJishutujie.nUpSwitches(s)) = Val(TrainInf(j).PassCrossing(k).nFirstPassNum(m)) Then
                            '                        '寻找道岔纵坐标
                            '                        CrossY1 = GuDaoJishutujie.nUpSwitchesYCoord(s) + JishutujieVerInterval / 4
                            '                        CrossY2 = CrossY1
                            '                        '寻找道岔占用时间横坐标

                            '                        Exit For
                            '                    End If
                            '                Next
                            '                CrossX2 = CrossX1


                            '                rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY1, CrossX2, UpCrossY1)

                            '                intTime1 = intTime1 - TrainInf(j).PassCrossing(k).nFirstPassTime(m)

                            '                CrossX1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)

                            '                'If m = 1 Then
                            '                '    rBmpGraphics.DrawLine(tmpPen, CrossX1, CrossY1, CrossX1, UpStaY)
                            '                'End If

                            '                If CrossX1 >= 0 And CrossY1 >= 0 And CrossX2 >= 0 And CrossY2 >= 0 Then
                            '                    If CrossX2 >= CrossX1 Then
                            '                        If CrossX1 <= sngRightX Then
                            '                            If CrossX2 <= sngRightX Then
                            '                                ' rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                            '                                ' rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                            '                                ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                            '                                'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                            '                                ' rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                            '                                'gujianghe begin
                            '                                rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)
                            '                                rBmpGraphics.FillRectangle(Brushes.Blue, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)

                            '                                'gujianghe end
                            '                            Else
                            '                                CrossX3 = sngRightX
                            '                                CrossY3 = CrossY1
                            '                                'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                            '                                ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                            '                                'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                            '                                rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)
                            '                                rBmpGraphics.FillRectangle(Brushes.Blue, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)

                            '                            End If
                            '                        End If
                            '                    Else
                            '                        CrossX4 = CrossX1 - sngToWidth
                            '                        CrossY4 = CrossY1
                            '                        CrossX3 = intLeftBlank + intStaBlank + intLeftX
                            '                        CrossY3 = CrossY1
                            '                        'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                            '                        'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                            '                        'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                            '                        rBmpGraphics.DrawRectangle(TmpPen1, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)
                            '                        rBmpGraphics.FillRectangle(Brushes.Blue, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)

                            '                    End If
                            '                End If

                            '            Next
                            '            Exit For
                            '        End If
                            '    Next
                            '    'gujianghe end

                            '    If X1 >= 0 And Y1 >= 0 And X2 >= 0 And Y2 >= 0 Then
                            '        If X2 >= X1 Then
                            '            If X1 <= sngRightX Then
                            '                If X2 <= sngRightX Then
                            '                    'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                            '                    'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                            '                    rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X2 - X1, JishutujieVerInterval / 4)
                            '                    rBmpGraphics.FillRectangle(Brushes.Blue, X1, Y1, X2 - X1, JishutujieVerInterval / 4)

                            '                Else
                            '                    X3 = sngRightX
                            '                    Y3 = Y1
                            '                    'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                            '                    'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                            '                    ' rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                            '                    rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X3 - X1, JishutujieVerInterval / 4)
                            '                    rBmpGraphics.FillRectangle(Brushes.Blue, X1, Y1, X3 - X1, JishutujieVerInterval / 4)
                            '                End If
                            '            End If
                            '        Else
                            '            X4 = X1 - sngToWidth
                            '            Y4 = Y1
                            '            X3 = intLeftBlank + intStaBlank + intLeftX
                            '            Y3 = Y1
                            '            'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                            '            'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3 + sngToWidth, Y3)
                            '            'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                            '            rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X3 + sngToWidth - X1, JishutujieVerInterval / 4)
                            '            rBmpGraphics.FillRectangle(Brushes.Blue, X1, Y1, X3 + sngToWidth - X1, JishutujieVerInterval / 4)

                            '            rBmpGraphics.DrawLine(tmpPen, X3, Y3, X2, Y2)
                            '        End If
                            '    End If


                            '    '' 下行列车下方道岔铺画
                            '    ' 下行列车下方道岔铺画
                            '    'gujianghe begin
                            '    CrossX2 = X2
                            '    CrossY1 = Y2
                            '    CrossY2 = Y2
                            '    'DownStaY = JishutujieVerInterval / 4 + GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(1)).nFirStaID).sStationName, 2)
                            '    For k = 1 To UBound(TrainInf(j).PassCrossing)
                            '        If TrainInf(j).PassCrossing(k).nSta = GuDaoJishutujie.nSta Then
                            '            ' 下端道岔占用铺画
                            '            For m = UBound(TrainInf(j).PassCrossing(k).nSecondPassNum) To 1 Step -1
                            '                DownCrossY1 = CrossY1
                            '                DownCrossY2 = CrossY2
                            '                For s = 1 To UBound(GuDaoJishutujie.nDownSwitches)
                            '                    If Val(GuDaoJishutujie.nDownSwitches(s)) = Val(TrainInf(j).PassCrossing(k).nSecondPassNum(m)) Then
                            '                        '寻找道岔纵坐标
                            '                        CrossY1 = GuDaoJishutujie.nDownSwitchesYCoord(s) + JishutujieVerInterval / 4
                            '                        CrossY2 = CrossY1
                            '                        '寻找道岔占用时间横坐标

                            '                        Exit For
                            '                    End If
                            '                Next
                            '                CrossX1 = CrossX2

                            '                rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY1, CrossX2, DownCrossY1)

                            '                intTime2 = intTime2 + TrainInf(j).PassCrossing(k).nSecondPassTime(m)

                            '                CrossX2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                            '                'If m = 1 Then
                            '                '    rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY1, CrossX2, DownStaY)
                            '                'End If

                            '                If CrossX1 >= 0 And CrossY1 >= 0 And CrossX2 >= 0 And CrossY2 >= 0 Then
                            '                    If CrossX2 >= CrossX1 Then
                            '                        If CrossX1 <= sngRightX Then
                            '                            If CrossX2 <= sngRightX Then
                            '                                ' rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                            '                                ' rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                            '                                ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                            '                                'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                            '                                ' rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                            '                                'gujianghe begin
                            '                                rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)
                            '                                rBmpGraphics.FillRectangle(Brushes.Blue, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)

                            '                                'gujianghe end
                            '                            Else
                            '                                CrossX3 = sngRightX
                            '                                CrossY3 = CrossY1
                            '                                'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                            '                                ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                            '                                'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                            '                                rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)
                            '                                rBmpGraphics.FillRectangle(Brushes.Blue, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)

                            '                            End If
                            '                        End If
                            '                    Else
                            '                        CrossX4 = CrossX1 - sngToWidth
                            '                        CrossY4 = CrossY1
                            '                        CrossX3 = intLeftBlank + intStaBlank + intLeftX
                            '                        CrossY3 = CrossY1
                            '                        'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                            '                        'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                            '                        'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                            '                        rBmpGraphics.DrawRectangle(TmpPen1, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)
                            '                        rBmpGraphics.FillRectangle(Brushes.Blue, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)

                            '                    End If
                            '                End If

                            '            Next
                            '            Exit For
                            '        End If
                            '    Next
                            '    'gujianghe end


                            'End If
                        Else '技术作业图解站不是始发站 而是中间车站 

                            For i = 1 To UBound(TrainInf(j).nPassSection)
                                If StationInf(SectionInf(TrainInf(j).nPassSection(i)).nSecStaID).sStationName = StationInf(GuDaoJishutujie.nSta).sStationName Then
                                    intTime1 = TrainInf(j).Arrival(GuDaoJishutujie.nSta) 'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                                    X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                    Y1 = GetCurGuDaoYCood(TrainInf(j).StopLine(GuDaoJishutujie.nSta))
                                    intTime2 = TrainInf(j).Starting(GuDaoJishutujie.nSta) 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                                    X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                    Y2 = Y1

                                    UpStaX = X1 - 20
                                    UpStaY = JishutujieVerInterval / 4 + GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(i)).nFirStaID).sStationName, 1)
                                    UpX = X1
                                    UpY = GuDaoJishutujie.GuDaoYcoord(1)

                                    'gujianghe begin
                                    CrossX1 = X1
                                    CrossY1 = Y1
                                    CrossY2 = Y2

                                    CrossFlag = 0
                                    DownCrossFlag = 0
                                    UpCrossFlag = 0

                                    For k = 1 To UBound(TrainInf(j).PassCrossing)
                                        CrossFlag = 1
                                        If TrainInf(j).PassCrossing(k).nSta = GuDaoJishutujie.nSta Then
                                            '上端道岔占用铺画
                                            For m = UBound(TrainInf(j).PassCrossing(k).nFirstPassNum) To 1 Step -1
                                                UpCrossFlag = 1
                                                UpCrossY1 = CrossY1
                                                UpCrossY2 = CrossY2
                                                For s = 1 To UBound(GuDaoJishutujie.nUpSwitches)
                                                    If Val(GuDaoJishutujie.nUpSwitches(s)) = Val(TrainInf(j).PassCrossing(k).nFirstPassNum(m)) Then
                                                        '寻找道岔纵坐标
                                                        CrossY1 = GuDaoJishutujie.nUpSwitchesYCoord(s) + JishutujieVerInterval / 4
                                                        CrossY2 = CrossY1
                                                        '寻找道岔占用时间横坐标

                                                        Exit For
                                                    End If
                                                Next
                                                CrossX2 = CrossX1


                                                rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY1, CrossX2, UpCrossY1)

                                                intTime1 = intTime1 - TrainInf(j).PassCrossing(k).nFirstPassTime(m)

                                                CrossX1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)

                                                If m = 1 Then
                                                    rBmpGraphics.DrawLine(tmpPen, CrossX1, CrossY1, CrossX1, UpStaY)
                                                End If

                                                If CrossX1 >= 0 And CrossY1 >= 0 And CrossX2 >= 0 And CrossY2 >= 0 Then
                                                    If CrossX2 >= CrossX1 Then
                                                        If CrossX1 <= sngRightX Then
                                                            If CrossX2 <= sngRightX Then
                                                                ' rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                                ' rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                                ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                                'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                                                ' rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                                                                'gujianghe begin
                                                                rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)
                                                                rBmpGraphics.FillRectangle(tmpDownCrossBrush, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)

                                                                'gujianghe end
                                                            Else
                                                                CrossX3 = sngRightX
                                                                CrossY3 = CrossY1
                                                                'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                                ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                                'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                                                                rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)
                                                                rBmpGraphics.FillRectangle(tmpDownCrossBrush, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)

                                                            End If
                                                        End If
                                                    Else
                                                        CrossX4 = CrossX1 - sngToWidth
                                                        CrossY4 = CrossY1
                                                        CrossX3 = intLeftBlank + intStaBlank + intLeftX
                                                        CrossY3 = CrossY1
                                                        'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                        'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                        'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                                        rBmpGraphics.DrawRectangle(TmpPen1, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)
                                                        rBmpGraphics.FillRectangle(tmpDownCrossBrush, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)

                                                    End If
                                                End If

                                            Next
                                            Exit For
                                        End If
                                    Next
                                    If CrossFlag = 1 And UpCrossFlag = 0 Then
                                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1, UpStaY)
                                    End If
                                    If CrossFlag = 0 Then
                                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1, UpStaY)
                                    End If
                                    'gujianghe end

                                    'If StationInf(GuDaoJishutujie.nSta).sStationName = TrainInf(j).NextStation Then '终到站
                                    'Else
                                    '    If X2 >= sngLeftX And X2 <= sngRightX Then
                                    '        DownX = X2
                                    '        DownY = GuDaoJishutujie.GuDaoYcoord(UBound(GuDaoJishutujie.GuDaoYcoord)) + 50
                                    '        DownStaX = X2 + 20
                                    '        DownStaY = GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(i + 1)).nSecStaID).sStationName, 2)
                                    '        'rBmpGraphics.DrawLine(tmpPen, X2, Y2, DownX, DownY)
                                    '        'rBmpGraphics.DrawLine(tmpPen, DownX, DownY, DownStaX, DownStaY)
                                    '    End If
                                    'End If

                                    If X1 >= 0 And Y1 >= 0 And X2 >= 0 And Y2 >= 0 Then
                                        If X2 >= X1 Then
                                            If X1 <= sngRightX Then
                                                If X2 <= sngRightX Then
                                                    'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                    'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X1, Y1)
                                                    'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                    'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                    ' rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X2 - X1, JishutujieVerInterval / 4)
                                                    rBmpGraphics.FillRectangle(tmpDownBrush, X1, Y1, X2 - X1, JishutujieVerInterval / 4)

                                                    'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                                                Else
                                                    X3 = sngRightX
                                                    Y3 = Y1
                                                    'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                    rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X1, Y1)
                                                    'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                    'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                                                    ' rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X3 - X1, JishutujieVerInterval / 4)
                                                    rBmpGraphics.FillRectangle(tmpDownBrush, X1, Y1, X3 - X1, JishutujieVerInterval / 4)

                                                End If
                                            End If
                                        Else
                                            X4 = X1 - sngToWidth
                                            Y4 = Y1
                                            X3 = intLeftBlank + intStaBlank + intLeftX
                                            Y3 = Y1
                                            'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                            ' rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X1, Y1)
                                            'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                            'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                            'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3 + sngToWidth, Y3)
                                            'rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X3 + sngToWidth - X1, JishutujieVerInterval / 4)
                                            rBmpGraphics.FillRectangle(tmpDownBrush, X1, Y1, X3 + sngToWidth - X1, JishutujieVerInterval / 4)
                                            'rBmpGraphics.DrawLine(tmpPen, X3, Y3, X2, Y2)
                                            'rBmpGraphics.DrawRectangle(TmpPen1, X3, Y3, X2 - X3, JishutujieVerInterval / 4)
                                            rBmpGraphics.FillRectangle(tmpDownBrush, X3, Y3, X2 - X3, JishutujieVerInterval / 4)
                                        End If
                                    End If

                                End If

                                ' gujianghe  begin////////////////////////////////
                                If StationInf(SectionInf(TrainInf(j).nPassSection(i)).nFirStaID).sStationName = StationInf(GuDaoJishutujie.nSta).sStationName Then
                                    intTime1 = TrainInf(j).Arrival(GuDaoJishutujie.nSta) 'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                                    X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                    Y1 = GetCurGuDaoYCood(TrainInf(j).StopLine(GuDaoJishutujie.nSta))
                                    intTime2 = TrainInf(j).Starting(GuDaoJishutujie.nSta) 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                                    X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                    Y2 = Y1

                                    UpStaX = X1 - 20
                                    DownStaY = JishutujieVerInterval / 4 + GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(i)).nSecStaID).sStationName, 2)
                                    UpX = X1
                                    UpY = GuDaoJishutujie.GuDaoYcoord(1)


                                    ' 下行列车下方道岔铺画
                                    'gujianghe begin
                                    CrossX2 = X2
                                    CrossY1 = Y2
                                    CrossY2 = Y2
                                    'DownStaY = JishutujieVerInterval / 4 + GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(1)).nFirStaID).sStationName, 2)
                                    CrossFlag = 0
                                    DownCrossFlag = 0
                                    UpCrossFlag = 0
                                    For k = 1 To UBound(TrainInf(j).PassCrossing)
                                        CrossFlag = 1
                                        If TrainInf(j).PassCrossing(k).nSta = GuDaoJishutujie.nSta Then
                                            ' 下端道岔占用铺画
                                            For m = UBound(TrainInf(j).PassCrossing(k).nSecondPassNum) To 1 Step -1
                                                DownCrossFlag = 1
                                                DownCrossY1 = CrossY1
                                                DownCrossY2 = CrossY2
                                                For s = 1 To UBound(GuDaoJishutujie.nDownSwitches)
                                                    If Val(GuDaoJishutujie.nDownSwitches(s)) = Val(TrainInf(j).PassCrossing(k).nSecondPassNum(m)) Then
                                                        '寻找道岔纵坐标
                                                        CrossY1 = GuDaoJishutujie.nDownSwitchesYCoord(s) + JishutujieVerInterval / 4
                                                        CrossY2 = CrossY1
                                                        '寻找道岔占用时间横坐标

                                                        Exit For
                                                    End If
                                                Next
                                                CrossX1 = CrossX2

                                                rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY1, CrossX2, DownCrossY1)

                                                intTime2 = intTime2 + TrainInf(j).PassCrossing(k).nSecondPassTime(m)

                                                CrossX2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                                If m = 1 Then
                                                    rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY1, CrossX2, DownStaY)
                                                End If

                                                If CrossX1 >= 0 And CrossY1 >= 0 And CrossX2 >= 0 And CrossY2 >= 0 Then
                                                    If CrossX2 >= CrossX1 Then
                                                        If CrossX1 <= sngRightX Then
                                                            If CrossX2 <= sngRightX Then
                                                                ' rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                                ' rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                                ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                                'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                                                ' rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                                                                'gujianghe begin
                                                                rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)
                                                                rBmpGraphics.FillRectangle(tmpDownCrossBrush, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)

                                                                'gujianghe end
                                                            Else
                                                                CrossX3 = sngRightX
                                                                CrossY3 = CrossY1
                                                                'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                                ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                                'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                                                                rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)
                                                                rBmpGraphics.FillRectangle(tmpDownCrossBrush, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)

                                                            End If
                                                        End If
                                                    Else
                                                        CrossX4 = CrossX1 - sngToWidth
                                                        CrossY4 = CrossY1
                                                        CrossX3 = intLeftBlank + intStaBlank + intLeftX
                                                        CrossY3 = CrossY1
                                                        'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                        'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                        'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                                        rBmpGraphics.DrawRectangle(TmpPen1, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)
                                                        rBmpGraphics.FillRectangle(tmpDownCrossBrush, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)

                                                    End If
                                                End If

                                            Next
                                            Exit For
                                        End If
                                    Next
                                    If CrossFlag = 1 And DownCrossFlag = 0 Then
                                        rBmpGraphics.DrawLine(tmpPen, X2, Y2, X2, DownStaY)
                                    End If
                                    If CrossFlag = 0 Then
                                        rBmpGraphics.DrawLine(tmpPen, X2, Y2, X2, DownStaY)
                                    End If
                                    'gujianghe end
                                End If
                                'gujianghe  end /////////////////////////

                            Next
                        End If

                    Else '上行

                        'If StationInf(SectionInf(TrainInf(j).nPassSection(1)).nFirStaID).sStationName = StationInf(GuDaoJishutujie.nSta).sStationName Then
                        '    UpStaY = JishutujieVerInterval / 4 + GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(1)).nSecStaID).sStationName, 2)
                        'End If


                        If StationInf(SectionInf(TrainInf(j).nPassSection(1)).nSecStaID).sStationName = StationInf(GuDaoJishutujie.nSta).sStationName Then
                            intTime1 = TrainInf(j).Arrival(GuDaoJishutujie.nSta) 'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                            X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                            Y1 = GetCurGuDaoYCood(TrainInf(j).StopLine(GuDaoJishutujie.nSta))
                            intTime2 = TrainInf(j).Starting(GuDaoJishutujie.nSta) 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                            X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                            Y2 = Y1
                            UpStaX = X2 + 20
                            'UpStaY = JishutujieVerInterval / 4 + GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(1)).nFirStaID).sStationName, 1)
                            UpX = X2
                            UpY = GuDaoJishutujie.GuDaoYcoord(1)
                            'gujianghe begin
                            CrossX1 = X1
                            CrossY1 = Y1
                            CrossY2 = Y2
                            For k = 1 To UBound(TrainInf(j).PassCrossing)
                                If TrainInf(j).PassCrossing(k).nSta = GuDaoJishutujie.nSta Then
                                    ' 下端道岔占用铺画
                                    For m = UBound(TrainInf(j).PassCrossing(k).nSecondPassNum) To 1 Step -1
                                        UpCrossY1 = CrossY1
                                        UpCrossY2 = CrossY2
                                        For s = 1 To UBound(GuDaoJishutujie.nDownSwitches)
                                            If Val(GuDaoJishutujie.nDownSwitches(s)) = Val(TrainInf(j).PassCrossing(k).nSecondPassNum(m)) Then
                                                '寻找道岔纵坐标
                                                CrossY1 = GuDaoJishutujie.nDownSwitchesYCoord(s) + JishutujieVerInterval / 4
                                                CrossY2 = CrossY1
                                                '寻找道岔占用时间横坐标

                                                Exit For
                                            End If
                                        Next
                                        CrossX2 = CrossX1

                                        rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY1, CrossX2, UpCrossY1)

                                        intTime1 = intTime1 - TrainInf(j).PassCrossing(k).nSecondPassTime(m)

                                        CrossX1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)

                                        'If m = 1 Then
                                        '    UpStaY = CrossY1 + JishutujieVerInterval / 4
                                        '    rBmpGraphics.DrawLine(tmpPen, CrossX1, CrossY1, CrossX1, UpStaY)
                                        'End If

                                        If CrossX1 >= 0 And CrossY1 >= 0 And CrossX2 >= 0 And CrossY2 >= 0 Then
                                            If CrossX2 >= CrossX1 Then
                                                If CrossX1 <= sngRightX Then
                                                    If CrossX2 <= sngRightX Then
                                                        ' rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                        ' rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                        ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                        'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                                        ' rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                                                        'gujianghe begin
                                                        rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)
                                                        rBmpGraphics.FillRectangle(tmpUpCrossBrush, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)

                                                        'gujianghe end
                                                    Else
                                                        CrossX3 = sngRightX
                                                        CrossY3 = CrossY1
                                                        'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                        ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                        'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                                                        rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)
                                                        rBmpGraphics.FillRectangle(tmpUpCrossBrush, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)

                                                    End If
                                                End If
                                            Else
                                                CrossX4 = CrossX1 - sngToWidth
                                                CrossY4 = CrossY1
                                                CrossX3 = intLeftBlank + intStaBlank + intLeftX
                                                CrossY3 = CrossY1
                                                'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                                rBmpGraphics.DrawRectangle(TmpPen1, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)
                                                rBmpGraphics.FillRectangle(tmpUpCrossBrush, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)

                                            End If
                                        End If

                                    Next
                                    Exit For
                                End If
                            Next
                            'gujianghe end

                            If X1 >= 0 And Y1 >= 0 And X2 >= 0 And Y2 >= 0 Then
                                If X2 >= X1 Then
                                    If X1 <= sngRightX Then
                                        If X2 <= sngRightX Then
                                            'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                            'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                            'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                            'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)

                                            'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                                            'rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X2 - X1, JishutujieVerInterval / 4)
                                            rBmpGraphics.FillRectangle(tmpUpBrush, X1, Y1, X2 - X1, JishutujieVerInterval / 4)

                                        Else
                                            X3 = sngRightX
                                            Y3 = Y1
                                            'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                            'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                            ' rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                            'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                                            'rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X3 - X1, JishutujieVerInterval / 4)
                                            rBmpGraphics.FillRectangle(tmpUpBrush, X1, Y1, X3 - X1, JishutujieVerInterval / 4)

                                        End If
                                    End If
                                Else
                                    X4 = X1 - sngToWidth
                                    Y4 = Y1
                                    X3 = intLeftBlank + intStaBlank + intLeftX
                                    Y3 = Y1

                                    'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                    'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                    ' rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3 + sngToWidth, Y3)
                                    ' rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X3 + sngToWidth - X1, JishutujieVerInterval / 4)
                                    rBmpGraphics.FillRectangle(tmpUpBrush, X1, Y1, X3 + sngToWidth - X1, JishutujieVerInterval / 4)

                                    'rBmpGraphics.DrawLine(tmpPen, X3, Y3, X2, Y2)
                                    ' rBmpGraphics.DrawRectangle(TmpPen1, X3, Y3, X2 - X3, JishutujieVerInterval / 4)
                                    rBmpGraphics.FillRectangle(tmpUpBrush, X3, Y3, X2 - X3, JishutujieVerInterval / 4)


                                End If
                            End If
                            ' 上行列车上方道岔铺画
                            'gujianghe begin
                            CrossX2 = X2
                            CrossY1 = Y2
                            CrossY2 = Y2
                            DownStaY = JishutujieVerInterval / 4 + GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(1)).nFirStaID).sStationName, 1)
                            For k = 1 To UBound(TrainInf(j).PassCrossing)
                                If TrainInf(j).PassCrossing(k).nSta = GuDaoJishutujie.nSta Then
                                    ' 上行列车上方道岔铺画
                                    For m = UBound(TrainInf(j).PassCrossing(k).nFirstPassNum) To 1 Step -1
                                        UpCrossY1 = CrossY1
                                        UpCrossY2 = CrossY2
                                        For s = 1 To UBound(GuDaoJishutujie.nUpSwitches)
                                            If Val(GuDaoJishutujie.nUpSwitches(s)) = Val(TrainInf(j).PassCrossing(k).nFirstPassNum(m)) Then
                                                '寻找道岔纵坐标
                                                CrossY1 = GuDaoJishutujie.nUpSwitchesYCoord(s) + JishutujieVerInterval / 4
                                                CrossY2 = CrossY1
                                                '寻找道岔占用时间横坐标

                                                Exit For
                                            End If
                                        Next
                                        CrossX1 = CrossX2

                                        rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY1, CrossX2, UpCrossY1)

                                        intTime2 = intTime2 + TrainInf(j).PassCrossing(k).nFirstPassTime(m)

                                        CrossX2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                        If m = 1 Then
                                            rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY2, CrossX2, DownStaY)
                                        End If

                                        If CrossX1 >= 0 And CrossY1 >= 0 And CrossX2 >= 0 And CrossY2 >= 0 Then
                                            If CrossX2 >= CrossX1 Then
                                                If CrossX1 <= sngRightX Then
                                                    If CrossX2 <= sngRightX Then
                                                        rBmpGraphics.FillRectangle(tmpUpCrossBrush, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)
                                                        'gujianghe end
                                                    Else
                                                        CrossX3 = sngRightX
                                                        CrossY3 = CrossY1
                                                        'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                        ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                        'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                                                        'rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)
                                                        rBmpGraphics.FillRectangle(tmpUpCrossBrush, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)

                                                    End If
                                                End If
                                            Else
                                                CrossX4 = CrossX1 - sngToWidth
                                                CrossY4 = CrossY1
                                                CrossX3 = intLeftBlank + intStaBlank + intLeftX
                                                CrossY3 = CrossY1
                                                'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                                ' rBmpGraphics.DrawRectangle(TmpPen1, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)
                                                rBmpGraphics.FillRectangle(tmpUpCrossBrush, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)

                                            End If
                                        End If

                                    Next
                                    Exit For
                                End If
                            Next
                            'gujianghe end


                            '始发折返
                            'If TrainInf(j).TrainReturn(1) > 0 Then
                            '    intTime1 = TrainInf(j).sStartZFArrival  'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                            '    X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                            '    Y1 = GetCurGuDaoYCood(TrainInf(j).sStartZFLine)
                            '    intTime2 = TrainInf(j).sStartZFStarting 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                            '    X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                            '    Y2 = Y1
                            '    'gujianghe begin
                            '    CrossX1 = X1
                            '    CrossY1 = Y1
                            '    CrossY2 = Y2
                            '    For k = 1 To UBound(TrainInf(j).PassCrossing)
                            '        If TrainInf(j).PassCrossing(k).nSta = GuDaoJishutujie.nSta Then
                            '            ' 下端道岔占用铺画
                            '            For m = UBound(TrainInf(j).PassCrossing(k).nSecondPassNum) To 1 Step -1
                            '                UpCrossY1 = CrossY1
                            '                UpCrossY2 = CrossY2
                            '                For s = 1 To UBound(GuDaoJishutujie.nDownSwitches)
                            '                    If Val(GuDaoJishutujie.nDownSwitches(s)) = Val(TrainInf(j).PassCrossing(k).nSecondPassNum(m)) Then
                            '                        '寻找道岔纵坐标
                            '                        CrossY1 = GuDaoJishutujie.nDownSwitchesYCoord(s) + JishutujieVerInterval / 4
                            '                        CrossY2 = CrossY1
                            '                        '寻找道岔占用时间横坐标

                            '                        Exit For
                            '                    End If
                            '                Next
                            '                CrossX2 = CrossX1

                            '                rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY1, CrossX2, UpCrossY1)

                            '                intTime1 = intTime1 - TrainInf(j).PassCrossing(k).nSecondPassTime(m)

                            '                CrossX1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                            '                If CrossX1 >= 0 And CrossY1 >= 0 And CrossX2 >= 0 And CrossY2 >= 0 Then
                            '                    If CrossX2 >= CrossX1 Then
                            '                        If CrossX1 <= sngRightX Then
                            '                            If CrossX2 <= sngRightX Then
                            '                                ' rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                            '                                ' rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                            '                                ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                            '                                'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                            '                                ' rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                            '                                'gujianghe begin
                            '                                rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)
                            '                                rBmpGraphics.FillRectangle(Brushes.Blue, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)

                            '                                'gujianghe end
                            '                            Else
                            '                                CrossX3 = sngRightX
                            '                                CrossY3 = CrossY1
                            '                                'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                            '                                ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                            '                                'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                            '                                rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)
                            '                                rBmpGraphics.FillRectangle(Brushes.Blue, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)

                            '                            End If
                            '                        End If
                            '                    Else
                            '                        CrossX4 = CrossX1 - sngToWidth
                            '                        CrossY4 = CrossY1
                            '                        CrossX3 = intLeftBlank + intStaBlank + intLeftX
                            '                        CrossY3 = CrossY1
                            '                        'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                            '                        'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                            '                        'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                            '                        rBmpGraphics.DrawRectangle(TmpPen1, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)
                            '                        rBmpGraphics.FillRectangle(Brushes.Blue, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)

                            '                    End If
                            '                End If

                            '            Next
                            '            Exit For
                            '        End If
                            '    Next
                            '    'gujianghe end
                            '    If X1 >= 0 And Y1 >= 0 And X2 >= 0 And Y2 >= 0 Then
                            '        If X2 >= X1 Then
                            '            If X1 <= sngRightX Then
                            '                If X2 <= sngRightX Then
                            '                    'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                            '                    'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)

                            '                    rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                            '                    rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X2 - X1, JishutujieVerInterval / 4)
                            '                    rBmpGraphics.FillRectangle(Brushes.Blue, X1, Y1, X2 - X1, JishutujieVerInterval / 4)

                            '                Else
                            '                    X3 = sngRightX
                            '                    Y3 = Y1
                            '                    'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                            '                    'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                            '                    rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X3 - X1, JishutujieVerInterval / 4)
                            '                    rBmpGraphics.FillRectangle(Brushes.Blue, X1, Y1, X3 - X1, JishutujieVerInterval / 4)

                            '                End If
                            '            End If
                            '        Else
                            '            X4 = X1 - sngToWidth
                            '            Y4 = Y1
                            '            X3 = intLeftBlank + intStaBlank + intLeftX
                            '            Y3 = Y1
                            '            'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                            '            'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                            '            rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X3 + sngToWidth - X1, JishutujieVerInterval / 4)
                            '            rBmpGraphics.FillRectangle(Brushes.Blue, X1, Y1, X3 + sngToWidth - X1, JishutujieVerInterval / 4)

                            '            'rBmpGraphics.DrawLine(tmpPen, X3, Y3, X2, Y2)
                            '            rBmpGraphics.DrawRectangle(TmpPen1, X3, Y3, X2 - X3, JishutujieVerInterval / 4)
                            '            rBmpGraphics.FillRectangle(Brushes.Blue, X3, Y3, X2 - X3, JishutujieVerInterval / 4)

                            '        End If
                            '    End If

                            '    ' 上行列车上方道岔铺画
                            '    'gujianghe begin
                            '    CrossX2 = X2
                            '    CrossY1 = Y2
                            '    CrossY2 = Y2
                            '    'DownStaY = JishutujieVerInterval / 4 + GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(1)).nFirStaID).sStationName, 2)
                            '    For k = 1 To UBound(TrainInf(j).PassCrossing)
                            '        If TrainInf(j).PassCrossing(k).nSta = GuDaoJishutujie.nSta Then
                            '            ' 上行列车上方道岔铺画
                            '            For m = UBound(TrainInf(j).PassCrossing(k).nSecondPassNum) To 1 Step -1
                            '                UpCrossY1 = CrossY1
                            '                UpCrossY2 = CrossY2
                            '                For s = 1 To UBound(GuDaoJishutujie.nUpSwitches)
                            '                    If Val(GuDaoJishutujie.nUpSwitches(s)) = Val(TrainInf(j).PassCrossing(k).nFirstPassNum(m)) Then
                            '                        '寻找道岔纵坐标
                            '                        CrossY1 = GuDaoJishutujie.nUpSwitchesYCoord(s) + JishutujieVerInterval / 4
                            '                        CrossY2 = CrossY1
                            '                        '寻找道岔占用时间横坐标

                            '                        Exit For
                            '                    End If
                            '                Next
                            '                CrossX1 = CrossX2

                            '                rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY1, CrossX2, UpCrossY1)

                            '                intTime2 = intTime2 + TrainInf(j).PassCrossing(k).nSecondPassTime(m)

                            '                CrossX2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                            '                'If m = 1 Then
                            '                '    rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY2, CrossX2, DownStaY)
                            '                'End If

                            '                If CrossX1 >= 0 And CrossY1 >= 0 And CrossX2 >= 0 And CrossY2 >= 0 Then
                            '                    If CrossX2 >= CrossX1 Then
                            '                        If CrossX1 <= sngRightX Then
                            '                            If CrossX2 <= sngRightX Then
                            '                                ' rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                            '                                ' rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                            '                                ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                            '                                'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                            '                                ' rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                            '                                'gujianghe begin
                            '                                rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)
                            '                                rBmpGraphics.FillRectangle(Brushes.Blue, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)

                            '                                'gujianghe end
                            '                            Else
                            '                                CrossX3 = sngRightX
                            '                                CrossY3 = CrossY1
                            '                                'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                            '                                ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                            '                                'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                            '                                rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)
                            '                                rBmpGraphics.FillRectangle(Brushes.Blue, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)

                            '                            End If
                            '                        End If
                            '                    Else
                            '                        CrossX4 = CrossX1 - sngToWidth
                            '                        CrossY4 = CrossY1
                            '                        CrossX3 = intLeftBlank + intStaBlank + intLeftX
                            '                        CrossY3 = CrossY1
                            '                        'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                            '                        'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                            '                        'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                            '                        rBmpGraphics.DrawRectangle(TmpPen1, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)
                            '                        rBmpGraphics.FillRectangle(Brushes.Blue, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)

                            '                    End If
                            '                End If

                            '            Next
                            '            Exit For
                            '        End If
                            '    Next
                            '    'gujianghe end

                            'End If


                        Else '非折返和始发车站 。。。。。

                            For i = 1 To UBound(TrainInf(j).nPassSection)
                                If StationInf(SectionInf(TrainInf(j).nPassSection(i)).nFirStaID).sStationName = StationInf(GuDaoJishutujie.nSta).sStationName Then
                                    intTime1 = TrainInf(j).Arrival(GuDaoJishutujie.nSta) 'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                                    X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                    Y1 = GetCurGuDaoYCood(TrainInf(j).StopLine(GuDaoJishutujie.nSta))
                                    intTime2 = TrainInf(j).Starting(GuDaoJishutujie.nSta) 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                                    X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                    Y2 = Y1

                                    UpStaX = X1 - 20
                                    UpStaY = JishutujieVerInterval / 4 + GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(i)).nSecStaID).sStationName, 2)
                                    UpX = X1
                                    UpY = GuDaoJishutujie.GuDaoYcoord(UBound(GuDaoJishutujie.GuDaoYcoord)) + 50
                                    'gujianghe begin
                                    CrossX1 = X1
                                    CrossY1 = Y1
                                    CrossY2 = Y2
                                    CrossFlag = 0
                                    DownCrossFlag = 0
                                    UpCrossFlag = 0

                                    For k = 1 To UBound(TrainInf(j).PassCrossing)
                                        CrossFlag = 1
                                        If TrainInf(j).PassCrossing(k).nSta = GuDaoJishutujie.nSta Then
                                            ' 下端道岔占用铺画
                                            For m = UBound(TrainInf(j).PassCrossing(k).nSecondPassNum) To 1 Step -1
                                                DownCrossFlag = 1
                                                UpCrossY1 = CrossY1
                                                UpCrossY2 = CrossY2
                                                For s = 1 To UBound(GuDaoJishutujie.nDownSwitches)
                                                    If Val(GuDaoJishutujie.nDownSwitches(s)) = Val(TrainInf(j).PassCrossing(k).nSecondPassNum(m)) Then
                                                        '寻找道岔纵坐标
                                                        CrossY1 = GuDaoJishutujie.nDownSwitchesYCoord(s) + JishutujieVerInterval / 4
                                                        CrossY2 = CrossY1
                                                        '寻找道岔占用时间横坐标

                                                        Exit For
                                                    End If
                                                Next
                                                CrossX2 = CrossX1

                                                rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY1, CrossX2, UpCrossY1)

                                                intTime1 = intTime1 - TrainInf(j).PassCrossing(k).nSecondPassTime(m)

                                                CrossX1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                                If m = 1 Then
                                                    rBmpGraphics.DrawLine(tmpPen, CrossX1, CrossY1, CrossX1, UpStaY)
                                                End If

                                                If CrossX1 >= 0 And CrossY1 >= 0 And CrossX2 >= 0 And CrossY2 >= 0 Then
                                                    If CrossX2 >= CrossX1 Then
                                                        If CrossX1 <= sngRightX Then
                                                            If CrossX2 <= sngRightX Then
                                                                ' rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                                ' rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                                ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                                'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                                                ' rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                                                                'gujianghe begin
                                                                ' rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)
                                                                rBmpGraphics.FillRectangle(tmpUpCrossBrush, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)

                                                                'gujianghe end
                                                            Else
                                                                CrossX3 = sngRightX
                                                                CrossY3 = CrossY1
                                                                'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                                ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                                'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                                                                ' rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)
                                                                rBmpGraphics.FillRectangle(tmpUpCrossBrush, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)

                                                            End If
                                                        End If
                                                    Else
                                                        CrossX4 = CrossX1 - sngToWidth
                                                        CrossY4 = CrossY1
                                                        CrossX3 = intLeftBlank + intStaBlank + intLeftX
                                                        CrossY3 = CrossY1
                                                        'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                        'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                        'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                                        ' rBmpGraphics.DrawRectangle(TmpPen1, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)
                                                        rBmpGraphics.FillRectangle(tmpUpCrossBrush, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)

                                                    End If
                                                End If

                                            Next
                                            Exit For
                                        End If
                                    Next
                                    If CrossFlag = 1 And DownCrossFlag = 0 Then
                                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1, UpStaY)
                                    End If
                                    If CrossFlag = 0 Then
                                        rBmpGraphics.DrawLine(tmpPen, X1, Y1, X1, UpStaY)
                                    End If
                                    'gujianghe end
                                    If StationInf(GuDaoJishutujie.nSta).sStationName = TrainInf(j).NextStation Then '终到站

                                    Else
                                        If X2 >= sngLeftX And X2 <= sngRightX Then
                                            DownX = X2
                                            DownY = GuDaoJishutujie.GuDaoYcoord(1)
                                            DownStaX = X2 + 20
                                            'DownStaY = GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(i + 1)).nSecStaID).sStationName, 1)
                                            'rBmpGraphics.DrawLine(tmpPen, X2, Y2, DownX, DownY)


                                            'rBmpGraphics.DrawLine(tmpPen, DownX, DownY, DownStaX, DownStaY)
                                            'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                        End If
                                    End If

                                    If X1 >= 0 And Y1 >= 0 And X2 >= 0 And Y2 >= 0 Then
                                        If X2 >= X1 Then
                                            If X1 <= sngRightX Then
                                                If X2 <= sngRightX Then
                                                    'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                    'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X1, Y1)
                                                    'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                    'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                    'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                                                    'rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X2 - X1, JishutujieVerInterval / 4)
                                                    rBmpGraphics.FillRectangle(tmpUpBrush, X1, Y1, X2 - X1, JishutujieVerInterval / 4)
                                                Else
                                                    X3 = sngRightX
                                                    Y3 = Y1
                                                    'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                    'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X1, Y1)
                                                    'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                    'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                                                    'rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X3 - X1, JishutujieVerInterval / 4)
                                                    rBmpGraphics.FillRectangle(tmpUpBrush, X1, Y1, X3 - X1, JishutujieVerInterval / 4)
                                                End If
                                            End If
                                        Else
                                            X4 = X1 - sngToWidth
                                            Y4 = Y1
                                            X3 = intLeftBlank + intStaBlank + intLeftX
                                            Y3 = Y1
                                            'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3 + sngToWidth, Y3)

                                            ''rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                            'rBmpGraphics.DrawLine(tmpPen, X3, Y3, X2, Y2)

                                            'rBmpGraphics.DrawRectangle(TmpPen1, X1, Y1, X3 + sngToWidth - X1, JishutujieVerInterval / 4)
                                            rBmpGraphics.FillRectangle(tmpUpCrossBrush, X1, Y1, X3 + sngToWidth - X1, JishutujieVerInterval / 4)

                                            'rBmpGraphics.DrawRectangle(TmpPen1, X3, Y3, X2 - X3, JishutujieVerInterval / 4)
                                            rBmpGraphics.FillRectangle(tmpUpCrossBrush, X3, Y3, X2 - X3, JishutujieVerInterval / 4)


                                        End If
                                    End If
                                End If

                                'gujianghe begin ///////////////////////////////
                                If StationInf(SectionInf(TrainInf(j).nPassSection(i)).nSecStaID).sStationName = StationInf(GuDaoJishutujie.nSta).sStationName Then
                                    intTime1 = TrainInf(j).Arrival(GuDaoJishutujie.nSta) 'GetTrainStartTime(nTrain, StationInf(TrainInf(nTrain).nFirstID(i)).sStationName)
                                    X1 = FormTimeToXCord(intTime1, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                    Y1 = GetCurGuDaoYCood(TrainInf(j).StopLine(GuDaoJishutujie.nSta))
                                    intTime2 = TrainInf(j).Starting(GuDaoJishutujie.nSta) 'GetTrainArriTime(nTrain, StationInf(TrainInf(nTrain).nSecondID(i)).sStationName)
                                    X2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                    Y2 = Y1

                                    UpStaX = X1 - 20
                                    DownStaY = JishutujieVerInterval / 4 + GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(i)).nFirStaID).sStationName, 1)
                                    UpX = X1
                                    UpY = GuDaoJishutujie.GuDaoYcoord(1)

                                    ' 上行列车上方道岔铺画
                                    'gujianghe begin
                                    CrossX2 = X2
                                    CrossY1 = Y2
                                    CrossY2 = Y2
                                    'DownStaY = JishutujieVerInterval / 4 + GetCurJiShuTuJieStaYCood(StationInf(SectionInf(TrainInf(j).nPassSection(1)).nFirStaID).sStationName, 2)
                                    CrossFlag = 0
                                    UpCrossFlag = 0
                                    DownCrossFlag = 0

                                    For k = 1 To UBound(TrainInf(j).PassCrossing)
                                        CrossFlag = 1
                                        If TrainInf(j).PassCrossing(k).nSta = GuDaoJishutujie.nSta Then
                                            ' 上行列车上方道岔铺画
                                            For m = UBound(TrainInf(j).PassCrossing(k).nFirstPassNum) To 1 Step -1
                                                UpCrossFlag = 1
                                                UpCrossY1 = CrossY1
                                                UpCrossY2 = CrossY2
                                                For s = 1 To UBound(GuDaoJishutujie.nUpSwitches)
                                                    If Val(GuDaoJishutujie.nUpSwitches(s)) = Val(TrainInf(j).PassCrossing(k).nFirstPassNum(m)) Then
                                                        '寻找道岔纵坐标
                                                        CrossY1 = GuDaoJishutujie.nUpSwitchesYCoord(s) + JishutujieVerInterval / 4
                                                        CrossY2 = CrossY1
                                                        '寻找道岔占用时间横坐标

                                                        Exit For
                                                    End If
                                                Next
                                                CrossX1 = CrossX2

                                                rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY1, CrossX2, UpCrossY1)

                                                intTime2 = intTime2 + TrainInf(j).PassCrossing(k).nSecondPassTime(m)

                                                CrossX2 = FormTimeToXCord(intTime2, intFirTime, nTimeWidth, intToWidth, intLeftBlank, intStaBlank, intLeftX)
                                                If m = 1 Then
                                                    rBmpGraphics.DrawLine(tmpPen, CrossX2, CrossY2, CrossX2, DownStaY)
                                                End If

                                                If CrossX1 >= 0 And CrossY1 >= 0 And CrossX2 >= 0 And CrossY2 >= 0 Then
                                                    If CrossX2 >= CrossX1 Then
                                                        If CrossX1 <= sngRightX Then
                                                            If CrossX2 <= sngRightX Then
                                                                ' rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                                ' rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                                ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                                'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                                                ' rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
                                                                'gujianghe begin
                                                                ' rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)
                                                                rBmpGraphics.FillRectangle(tmpUpCrossBrush, CrossX1, CrossY1, CrossX2 - CrossX1, JishutujieVerInterval / 4)

                                                                'gujianghe end
                                                            Else
                                                                CrossX3 = sngRightX
                                                                CrossY3 = CrossY1
                                                                'rBmpGraphics.DrawEllipse(tmpPen, X1 - 5, Y1 - 5, 10, 10)
                                                                ' rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                                'rBmpGraphics.DrawLine(tmpPen, X1, Y1, X3, Y3)
                                                                'rBmpGraphics.DrawRectangle(TmpPen1, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)
                                                                rBmpGraphics.FillRectangle(tmpUpCrossBrush, CrossX1, CrossY1, CrossX3 - CrossX1, JishutujieVerInterval / 4)

                                                            End If
                                                        End If
                                                    Else
                                                        CrossX4 = CrossX1 - sngToWidth
                                                        CrossY4 = CrossY1
                                                        CrossX3 = intLeftBlank + intStaBlank + intLeftX
                                                        CrossY3 = CrossY1
                                                        'rBmpGraphics.DrawLine(tmpPen, UpStaX, UpStaY, UpX, UpY)
                                                        'rBmpGraphics.DrawEllipse(tmpPen, X2 - 5, Y2 - 5, 10, 10)
                                                        'rBmpGraphics.DrawLine(tmpPen, UpX, UpY, X2, Y2)
                                                        ' rBmpGraphics.DrawRectangle(TmpPen1, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)
                                                        rBmpGraphics.FillRectangle(tmpUpCrossBrush, CrossX3, CrossY3, CrossX2 - CrossX3, JishutujieVerInterval / 4)

                                                    End If
                                                End If

                                            Next
                                            Exit For
                                        End If
                                    Next

                                    If CrossFlag = 1 And UpCrossFlag = 0 Then
                                        rBmpGraphics.DrawLine(tmpPen, X2, Y2, X2, DownStaY)
                                    End If
                                    If CrossFlag = 0 Then
                                        rBmpGraphics.DrawLine(tmpPen, X2, Y2, X2, DownStaY)
                                    End If
                                    'gujianghe end
                                End If
                                'gujianghe  end /////////////////////////
                            Next
                        End If
                    End If
                End If
            End If
        Next

    End Sub

    '由股道编号得到股道Id
    Public Function FromSGudaoNumtoGuDaoID(ByVal sGuDaoNum As String, ByVal nSta As Integer) As Integer
        Dim i As Integer
        For i = 1 To UBound(StationInf(nSta).sStLineNo)
            If StationInf(nSta).sStLineNo(i) = sGuDaoNum Then
                FromSGudaoNumtoGuDaoID = i
                Exit For
            End If
        Next
    End Function


End Module

'gujianghe end
