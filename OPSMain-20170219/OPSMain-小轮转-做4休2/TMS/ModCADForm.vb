
Module ModCADForm

    Public sngLeftBaseX As Single
    Public nGridWidth As Single
    Public nFirGridWidth As Single
    Public nLineShowScale As Single


    Structure typeCADformPara
        Dim nMaxUndoID As Integer '最大可撤销的次数
        Dim sCurStaSchemeName As String '当前车站的控制方式
    End Structure
    Public CADformPara As typeCADformPara

    Structure typeCADformUndoInf
        Dim nXuHao As Integer
        Dim CADStaInfor() As typeCADStaOrSecInf
    End Structure
    Public CADformUndoInf() As typeCADformUndoInf
    Public CADformUndoInf2() As typeCADformUndoInf

    Structure typeControlModel
        Dim sModelName As String ' 控制模块名称
        Dim sElectricStation As String '牵引变电站名
        Dim sStation() As String '控制模块所属的车站或区间，有可能多个
        Dim sTrackNum() As String '包含的轨道编号
        Dim sColor As Color '显示颜色
    End Structure
    Public ControlModel() As typeControlModel

    Structure typeTrackInf '所有线段信息结构体
        Dim nStaID As Integer '车站ID
        Dim nTrackID As Integer '线段ID
        Dim sControlNum As String
        Dim X1 As Single
        Dim Y1 As Single
        Dim X2 As Single
        Dim Y2 As Single
    End Structure
    Structure typeFontInf '所有文字信息结构体
        Dim nStaID As Integer '车站ID
        Dim nFontID As Integer '文字ID
        Dim X As Single
        Dim Y As Single
    End Structure
    Public TrackInf() As typeTrackInf '所有线段信息
    Public tmpTrackInf() As typeTrackInf  '当前显示的线段ID信息
    Public tmpRotateTrackInf() As typeTrackInf  '当前显示的线段ID信息
    Public curSelectTrackID() As Integer '当前选中的TrackInf()中的ID
    Public CopyTrackinf() As typeTrackInformation '复制时临时保存
    Public tmpRunTrackInf() As typeTrackInf '列车运行仿真时的坐标信息
    Public tmpRunStaTrackInf() As typeTrackInf '列车运行仿真时车站的坐标信息

    Public FontInf() As typeFontInf '所有文字信息
    Public tmpFontInf() As typeFontInf  '当前显示的线段ID信息

    Structure typeGetGridXY
        Dim X As Single
        Dim Y As Single
    End Structure
    Public GridXY As typeGetGridXY

    Structure typeCADformUnDoSeq
        Dim nUpID As Integer
        Dim nDownID As Integer
        Dim nCurUndoID As Integer
        Dim nRedoID As Integer
        Dim nMinID As Integer
        Dim nStep As Integer
    End Structure
    Public CADUndoSeq As typeCADformUnDoSeq


    Public GridXYCord As New ArrayList

    '读入TrackInf()
    Public Sub InputTrackInf()
        Dim i As Integer
        Dim j As Integer
        ReDim TrackInf(0)
        ReDim FontInf(0)
        For i = 1 To UBound(CADStaInf)
            For j = 1 To UBound(CADStaInf(i).Track)
                'If j = 37 Then Stop
                If CADStaInf(i).Track(j).nDelete = 0 Then '除掉删除标记的
                    ReDim Preserve TrackInf(UBound(TrackInf) + 1)
                    TrackInf(UBound(TrackInf)).nStaID = i
                    TrackInf(UBound(TrackInf)).nTrackID = j
                    TrackInf(UBound(TrackInf)).sControlNum = CADStaInf(i).Track(j).sControlNum
                    TrackInf(UBound(TrackInf)).X1 = CADStaInf(i).Track(j).X1
                    TrackInf(UBound(TrackInf)).Y1 = CADStaInf(i).Track(j).Y1
                    TrackInf(UBound(TrackInf)).X2 = CADStaInf(i).Track(j).X2
                    TrackInf(UBound(TrackInf)).Y2 = CADStaInf(i).Track(j).Y2
                End If
            Next
            'For j = 1 To UBound(CADStaInf(i).FontText)
            '    If CADStaInf(i).FontText(j).nDelete = 0 Then
            '        ReDim Preserve FontInf(UBound(FontInf) + 1)
            '        FontInf(UBound(FontInf)).nStaID = i
            '        FontInf(UBound(FontInf)).nFontID = j
            '        FontInf(UBound(FontInf)).X = CADStaInf(i).FontText(j).X
            '        FontInf(UBound(FontInf)).Y = CADStaInf(i).FontText(j).Y
            '    End If
            'Next
        Next
    End Sub

    '在图片范围内显示当前TRACK
    Public Sub InputCurTrackInf(ByVal LeftX As Single, ByVal Width As Single, ByVal topY As Single, ByVal Height As Single, ByVal Scale As Single)
        Dim i As Integer
        ReDim tmpTrackInf(0)
        ReDim tmpFontInf(0)
        For i = 1 To UBound(TrackInf)
            If ((TrackInf(i).X1 >= LeftX And TrackInf(i).X1 <= LeftX + Width) And (TrackInf(i).Y1 >= topY And TrackInf(i).Y1 <= topY + Height)) Or _
            ((TrackInf(i).X2 >= LeftX And TrackInf(i).X2 <= LeftX + Width) And (TrackInf(i).Y2 >= topY And TrackInf(i).Y2 <= topY + Height)) Then
                ReDim Preserve tmpTrackInf(UBound(tmpTrackInf) + 1)
                tmpTrackInf(UBound(tmpTrackInf)).nStaID = TrackInf(i).nStaID
                tmpTrackInf(UBound(tmpTrackInf)).nTrackID = TrackInf(i).nTrackID
                tmpTrackInf(UBound(tmpTrackInf)).sControlNum = TrackInf(i).sControlNum
                tmpTrackInf(UBound(tmpTrackInf)).X1 = (TrackInf(i).X1 - LeftX) * Scale
                tmpTrackInf(UBound(tmpTrackInf)).Y1 = (TrackInf(i).Y1 - topY) * Scale
                tmpTrackInf(UBound(tmpTrackInf)).X2 = (TrackInf(i).X2 - LeftX) * Scale
                tmpTrackInf(UBound(tmpTrackInf)).Y2 = (TrackInf(i).Y2 - topY) * Scale
            End If
        Next
        For i = 1 To UBound(fontinf)
            If ((FontInf(i).X >= LeftX And FontInf(i).X <= LeftX + Width) And (FontInf(i).Y >= topY And FontInf(i).Y <= topY + Height)) Then
                ReDim Preserve tmpFontInf(UBound(tmpFontInf) + 1)
                tmpFontInf(UBound(tmpFontInf)).nStaID = FontInf(i).nStaID
                tmpFontInf(UBound(tmpFontInf)).nFontID = FontInf(i).nFontID
                tmpFontInf(UBound(tmpFontInf)).X = (FontInf(i).X - LeftX) * Scale
                tmpFontInf(UBound(tmpFontInf)).Y = (FontInf(i).Y - topY) * Scale
            End If
        Next
    End Sub

    '打印需要显示的TrackID 
    Public Sub PrintCurTrackInf(ByVal rBmpGraphics As Graphics, ByVal LeftX As Single, ByVal Width As Single, ByVal topY As Single, ByVal Height As Single, ByVal Scale As Single, ByVal IfShowOtherInf As Boolean, ByVal IfPrintGuDaoNum As Boolean, _
                                         ByVal IfPrintTrackNum As Boolean, ByVal IfPrintCorssNum As Boolean)

        'Call InputTrackInf()
        Call InputCurTrackInf(LeftX, Width, topY, Height, Scale)
        Call PrintTrackInPic(rBmpGraphics, Color.DarkGray, 3, Scale, IfShowOtherInf, IfPrintGuDaoNum, IfPrintTrackNum, IfPrintCorssNum)
    End Sub

    '打印车站平面图
    Public Sub PrintStationPicture(ByVal PicSta As PictureBox, ByVal rBmpGraphics As Graphics, ByVal LeftX As Single, ByVal Width As Single, ByVal topY As Single, ByVal Height As Single, ByVal Scale As Single, ByVal bIfShowGrid As Integer, ByVal IfShowOtherInf As Boolean, ByVal IfPrintGuDaoNum As Boolean, _
                                         ByVal IfPrintTrackNum As Boolean, ByVal IfPrintCorssNum As Boolean)

        Dim X1 As Single
        Dim Y1 As Single
        Dim ForX As Single
        Dim ForY As Single
        Dim lngStaX As Single
        Dim lngStaY As Single
        Dim i As Integer

        lngStaX = 0
        lngStaY = 0
        ForX = 0
        ForY = 0

        Dim nRows As Integer
        Dim nCols As Integer
        nRows = PicSta.Height / nGridWidth
        nCols = PicSta.Width / nGridWidth

        If bIfShowGrid = True Then '显示网格
            For i = 1 To nRows
                ForX = 0
                ForY = nGridWidth * (i - 1) - (topY Mod nFirGridWidth) * Scale
                X1 = PicSta.Width
                Y1 = ForY
                rBmpGraphics.DrawLine(New Pen(Color.Gray, 1), ForX, ForY, X1, Y1)
            Next

            For i = 1 To nCols
                ForX = nGridWidth * (i - 1) - (LeftX Mod nFirGridWidth) * Scale
                ForY = 0
                X1 = ForX
                Y1 = PicSta.Height
                rBmpGraphics.DrawLine(New Pen(Color.Gray, 1), ForX, ForY, X1, Y1)
            Next

            '    '画基准线
            '    ForX = 0
            '    ForY = nGridWidth * 10
            '    X1 = PicSta.Width
            '    Y1 = ForY
            '    rBmpGraphics.DrawLine(New Pen(Color.SkyBlue, 1), ForX, ForY, X1, Y1)

            '    ForX = 0
            '    ForY = nGridWidth * 14
            '    X1 = PicSta.Width
            '    Y1 = ForY
            '    rBmpGraphics.DrawLine(New Pen(Color.SkyBlue, 1), ForX, ForY, X1, Y1)
        End If

        Call PrintCurTrackInf(rBmpGraphics, LeftX, Width, topY, Height, Scale, IfShowOtherInf, IfPrintGuDaoNum, IfPrintTrackNum, IfPrintCorssNum)
    End Sub


    '修改TrackInf里的坐标
    Public Sub EditTrackCoordInf(ByVal ntmpTrackID As Integer, ByVal X1 As Single, ByVal X2 As Single, ByVal Y1 As Single, ByVal Y2 As Single)
        Dim i As Integer
        For i = 1 To UBound(TrackInf)
            If TrackInf(i).nStaID = tmpTrackInf(ntmpTrackID).nStaID And TrackInf(i).nTrackID = tmpTrackInf(ntmpTrackID).nTrackID Then
                TrackInf(i).X1 = X1
                TrackInf(i).X2 = X2
                TrackInf(i).Y1 = Y1
                TrackInf(i).Y2 = Y2
                Exit For
            End If
        Next
    End Sub

    '多选
    Public Sub SelectMultiTack(ByVal X1 As Single, ByVal X2 As Single, ByVal Y1 As Single, ByVal Y2 As Single)
        Dim tmpX As Single
        If X1 > X2 Then
            tmpX = X1
            X1 = X2
            X2 = tmpX
        End If

        If Y1 > Y2 Then
            tmpX = Y1
            Y1 = Y2
            Y2 = tmpX
        End If

        Dim i As Integer
        ReDim curSelectTrackID(0)
        For i = 1 To UBound(tmpTrackInf)
            If tmpTrackInf(i).X1 >= X1 And tmpTrackInf(i).X1 <= X2 And tmpTrackInf(i).Y1 >= Y1 And tmpTrackInf(i).Y1 <= Y2 _
            And tmpTrackInf(i).X2 >= X1 And tmpTrackInf(i).X2 <= X2 And tmpTrackInf(i).Y2 >= Y1 And tmpTrackInf(i).Y2 <= Y2 Then
                ReDim Preserve curSelectTrackID(UBound(curSelectTrackID) + 1)
                curSelectTrackID(UBound(curSelectTrackID)) = i
            End If
        Next
    End Sub

    '得到tmpTrackinfId()
    Public Function GetTmpTrackInfIDFromCirNum(ByVal sCirNum As String) As Integer
        Dim i As Integer
        For i = 1 To UBound(tmpTrackInf)
            If CADStaInf(tmpTrackInf(i).nStaID).Track(tmpTrackInf(i).nTrackID).sTrackCircuitNum = sCirNum Then
                GetTmpTrackInfIDFromCirNum = i
                Exit For
            End If
        Next
    End Function


    Public Function FromStaNameToCADStaInfID(ByVal sStaName As String) As Integer
        Dim i As Integer
        For i = 1 To UBound(CADStaInf)
            If CADStaInf(i).sStaName = sStaName Then
                FromStaNameToCADStaInfID = i
                Exit For
            End If
        Next
    End Function

    '   画全线图
    Public Sub DrawWholeLinePicture(ByVal IFDrawReangle As Boolean, ByVal picLine As PictureBox)
        '画全线图
        Dim tmpPen As Pen
        tmpPen = New Pen(Color.White, 1)
        'Call DrawLinePic(0.3, picLine, tmpPen)
        Dim rBmp1 As Bitmap '画图临时保存的图像
        Dim rBmpGraphics1 As Graphics '画线路与车站图的定义的对象
        rBmp1 = New Bitmap(picLine.Width, picLine.Height)
        rBmpGraphics1 = Graphics.FromImage(rBmp1)
        Call DrawLinePicture(rBmpGraphics1, picLine.Width, picLine.Height)
        If IFDrawReangle = True Then
            rBmpGraphics1.DrawRectangle(New Pen(Color.Yellow, 1), 2, 2, picLine.Width - 4, picLine.Height - 4)
        End If
        picLine.Image = rBmp1

    End Sub

    '打印全线
    Public Sub DrawLinePicture(ByVal rBmpGraphics1 As Graphics, ByVal PicWidth As Integer, ByVal PicHeight As Integer)
        Dim nWidthScale As Single
        Dim nheightScale As Single
        Dim minX As Single
        Dim maxX As Single
        Dim minY As Single
        Dim maxY As Single
        Dim LineLeftX As Single
        Dim LineTopY As Single
        Dim nLineScale As Single
        minX = 1000000
        maxX = -10000000
        Dim i As Integer
        For i = 1 To UBound(TrackInf)
            minX = Minimal(minX, TrackInf(i).X1)
            minX = Minimal(minX, TrackInf(i).X2)
            maxX = Maximal(maxX, TrackInf(i).X1)
            maxX = Maximal(maxX, TrackInf(i).X2)
            minY = Minimal(minY, TrackInf(i).Y1)
            minY = Minimal(minY, TrackInf(i).Y2)
            maxY = Maximal(maxY, TrackInf(i).Y1)
            maxY = Maximal(maxY, TrackInf(i).Y2)
        Next
        If (maxX - minX) > 0 Then
            nWidthScale = (PicWidth / (maxX - minX)) * 0.9
        End If
        If (maxY - minY) > 0 Then
            nheightScale = (PicHeight / (maxY - minY)) * 0.9
        End If
        nLineScale = Minimal(nWidthScale, nheightScale)
        nLineShowScale = nLineScale
        LineLeftX = (PicWidth - (maxX - minX) * nLineScale) / 2 - minX * nLineScale
        LineTopY = (PicHeight - (maxY - minY) * nLineScale) / 2 - minY * nLineScale
        Call PrintTrackInLinePic(rBmpGraphics1, 1, nLineScale, -LineLeftX, -LineTopY, Color.DarkGray, False, False, False, False)
    End Sub
    '添加线段
    Public Function addCADStaTrack(ByVal nStaID As Integer, _
                                ByVal sStyle As String, _
                                ByVal sGuDaoStyle As String, _
                                ByVal sGuDaoYongTu As String, _
                                ByVal sGuDaoUseSeq As String, _
                                ByVal sngLength As Single, _
                                ByVal sTrackNum As String, _
                                ByVal X1 As Single, _
                                ByVal Y1 As Single, _
                                ByVal X2 As Single, _
                                ByVal Y2 As Single, _
                                ByVal sLeftLink1 As String, _
                                ByVal sLeftLink2 As String, _
                                ByVal sLeftLink3 As String, _
                                ByVal sRightLink1 As String, _
                                ByVal sRightLink2 As String, _
                                ByVal sRightLink3 As String, _
                                ByVal sTrackCircuitNum As String, _
                                ByVal sControlNum As String, _
                                ByVal sMemo As String) As String
        If sLeftLink1.Trim = "" Then
            sLeftLink1 = "NULL"
        End If
        If sLeftLink2.Trim = "" Then
            sLeftLink2 = "NULL"
        End If
        If sLeftLink3.Trim = "" Then
            sLeftLink3 = "NULL"
        End If
        If sRightLink1.Trim = "" Then
            sRightLink1 = "NULL"
        End If
        If sRightLink2.Trim = "" Then
            sRightLink2 = "NULL"
        End If
        If sRightLink3.Trim = "" Then
            sRightLink3 = "NULL"
        End If

        'Dim tX As Single
        'Dim ty As Single


        Dim nTrackId As Integer
        nTrackId = 0
        Dim i As Integer
        For i = 1 To UBound(CADStaInf(nStaID).Track)
            If CADStaInf(nStaID).Track(i).nDelete = 1 Then '已经删除的，覆盖
                nTrackId = i
                Exit For
            End If
        Next
        If nTrackId = 0 Then
            nTrackId = UBound(CADStaInf(nStaID).Track) + 1
            ReDim Preserve CADStaInf(nStaID).Track(nTrackId)
        End If

        If sTrackCircuitNum = "" Then
            sTrackCircuitNum = GetCurCircurTrackNum(nStaID)
        End If
        'If X1 > X2 Then
        '    tX = X1
        '    X1 = X2
        '    X2 = tX
        '    ty = Y1
        '    Y1 = Y2
        '    Y2 = ty
        'End If
        If sStyle = "站台线" Then
            sControlNum = ""
        End If
        CADStaInf(nStaID).Track(nTrackId).sStaName = CADStaInf(nStaID).sStaName
        CADStaInf(nStaID).Track(nTrackId).sStyle = sStyle
        CADStaInf(nStaID).Track(nTrackId).sGuDaoStyle = sGuDaoStyle
        CADStaInf(nStaID).Track(nTrackId).sGuDaoYongTu = sGuDaoYongTu
        CADStaInf(nStaID).Track(nTrackId).sGuDaoUseSeq = sGuDaoUseSeq
        CADStaInf(nStaID).Track(nTrackId).sngLength = sngLength
        CADStaInf(nStaID).Track(nTrackId).sTrackNum = sTrackNum
        CADStaInf(nStaID).Track(nTrackId).X1 = X1
        CADStaInf(nStaID).Track(nTrackId).Y1 = Y1
        CADStaInf(nStaID).Track(nTrackId).X2 = X2
        CADStaInf(nStaID).Track(nTrackId).Y2 = Y2
        CADStaInf(nStaID).Track(nTrackId).sLeftLink1 = sLeftLink1
        CADStaInf(nStaID).Track(nTrackId).sLeftLink2 = sLeftLink2
        CADStaInf(nStaID).Track(nTrackId).sLeftLink3 = sLeftLink3
        CADStaInf(nStaID).Track(nTrackId).sRightLink1 = sRightLink1
        CADStaInf(nStaID).Track(nTrackId).sRightLink2 = sRightLink2
        CADStaInf(nStaID).Track(nTrackId).sRightLink3 = sRightLink3
        CADStaInf(nStaID).Track(nTrackId).sTrackCircuitNum = sTrackCircuitNum
        CADStaInf(nStaID).Track(nTrackId).sControlNum = sControlNum
        CADStaInf(nStaID).Track(nTrackId).sMemo = sMemo
        CADStaInf(nStaID).Track(nTrackId).nDelete = 0
        addCADStaTrack = sTrackCircuitNum
        Call InputTrackInf()


        'Dim nCurID As Integer
        'Dim tX As Single
        'Dim ty As Single
        'If X1 = X2 And Y1 = Y2 Then Exit Sub
        ''创建一个OledbConnection
        ''Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        'Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        'Dim strTable As String = "select * from 线段信息表  where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' order by 线段编号"

        'Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
        'Dim myDataSet As System.Data.DataSet = New System.Data.DataSet
        'Mydc.Fill(myDataSet, "线段信息表")
        'Dim myTable As Data.DataTable
        'myTable = myDataSet.Tables("线段信息表")
        'nCurID = myTable.Rows.Count + 1

        'Dim Str As String
        'X1 = X1 + sngLeftBaseX
        'X2 = X2 + sngLeftBaseX
        'If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        'Str = "insert into 线段信息表 (车站名称,线段编号,线段类型,股道类型,股道用途,股道使用顺序,线段长度,股道或道岔编号,X1坐标,Y1坐标,X2坐标,Y2坐标,左1连接,左2连接,左3连接,右1连接,右2连接,右3连接,轨道电路编号,控制模块,备注) values ('" & _
        '            CADStaInf(nStaID).sStaName & "', '" & _
        '            nCurID & "', '" & _
        '            sStyle & "', '" & _
        '            sGuDaoStyle & "', '" & _
        '            sGuDaoYongTu & "', '" & _
        '            sGuDaoUseSeq & "', '" & _
        '            sngLength & "', '" & _
        '            sTrackNum & "', '" & _
        '            X1 & "', '" & _
        '            Y1 & "', '" & _
        '            X2 & "', '" & _
        '            Y2 & "', '" & _
        '            sLeftLink1 & "', '" & _
        '            sLeftLink2 & "', '" & _
        '            sLeftLink3 & "', '" & _
        '            sRightLink1 & "', '" & _
        '            sRightLink2 & "', '" & _
        '            sRightLink3 & "', '" & _
        '            sTrackCircuitNum & "', '" & _
        '            sTrackCircuitNum & "', '" & _
        '            sMemo & "')"
        'Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        'Mcom.ExecuteNonQuery()
        'MyConn.Close()

        'Call InputDataToCADstaInfTrack(nStaID)

    End Function

    Public Function GetCurCircurTrackNum(ByVal nStaID As Integer) As String
        GetCurCircurTrackNum = ""
        Dim i As Integer
        Dim j As Integer
        Dim sStaCode As String
        Dim sCirName As String
        sStaCode = CADStaInf(nStaID).sStaCode
        Dim nCurNum As Integer
        nCurNum = 1
        Dim ifIN As Integer
        Do
            ifIN = 0
            For i = 1 To UBound(CADStaInf(nStaID).Track)
                If CADStaInf(nStaID).Track(j).nDelete = 0 Then
                    sCirName = CADStaInf(nStaID).Track(i).sTrackCircuitNum
                    If sCirName Is Nothing Then

                    Else
                        If Val(sCirName.Substring(sStaCode.Length + 1)) = nCurNum Then
                            ifIN = 1
                            Exit For
                        End If
                    End If
                End If
            Next
            If ifIN = 0 Then
                GetCurCircurTrackNum = sStaCode & "-" & nCurNum
                Exit Do
            End If
            nCurNum = nCurNum + 1
        Loop
    End Function

    '修改线段信息
    Public Sub EditCADStaTrack(ByVal sStaName As String, _
                                ByVal nFirStaId As Integer, _
                                ByVal nTrackID As Integer, _
                                ByVal sStyle As String, _
                                ByVal sGuDaoStyle As String, _
                                ByVal sGuDaoYongTu As String, _
                                ByVal sGuDaoUseSeq As String, _
                                ByVal sngLength As Single, _
                                ByVal sTrackNum As String, _
                                ByVal X1 As Single, _
                                ByVal Y1 As Single, _
                                ByVal X2 As Single, _
                                ByVal Y2 As Single, _
                                ByVal sLeftLink1 As String, _
                                ByVal sLeftLink2 As String, _
                                ByVal sLeftLink3 As String, _
                                ByVal sRightLink1 As String, _
                                ByVal sRightLink2 As String, _
                                ByVal sRightLink3 As String, _
                                ByVal sTrackCircuitNum As String, _
                                ByVal sControlNum As String, _
                                ByVal sMemo As String)

        'Dim tX As Single
        'Dim ty As Single
        'If X1 > X2 Then
        '    tX = X1
        '    X1 = X2
        '    X2 = tX
        '    ty = Y1
        '    Y1 = Y2
        '    Y2 = ty
        'End If
        'X1 = X1 + sngLeftBaseX
        'X2 = X2 + sngLeftBaseX

        If sLeftLink1 Is Nothing Then
            sLeftLink1 = "NULL"
        Else
            If sLeftLink1.Trim = "" Then
                sLeftLink1 = "NULL"
            End If

        End If

        If sLeftLink2 Is Nothing Then
            sLeftLink2 = "NULL"
        Else
            If sLeftLink2.Trim = "" Then
                sLeftLink2 = "NULL"
            End If
        End If

        If sLeftLink3 Is Nothing Then
            sLeftLink3 = "NULL"
        Else
            If sLeftLink3.Trim = "" Then
                sLeftLink3 = "NULL"
            End If
        End If

        If sRightLink1 Is Nothing Then
            sRightLink1 = "NULL"
        Else
            If sRightLink1.Trim = "" Then
                sRightLink1 = "NULL"
            End If
        End If

        If sRightLink2 Is Nothing Then
            sRightLink2 = "NULL"
        Else
            If sRightLink2.Trim = "" Then
                sRightLink2 = "NULL"
            End If
        End If

        If sRightLink3 Is Nothing Then
            sRightLink3 = "NULL"
        Else
            If sRightLink3.Trim = "" Then
                sRightLink3 = "NULL"
            End If
        End If
        Dim i As Integer
        Dim nStaId As Integer
        If CADStaInf(nFirStaId).sStaName = sStaName Then
            nStaId = nFirStaId
        Else
            CADStaInf(nFirStaId).Track(nTrackID).nDelete = 1
            nStaId = FromStaNameToCADStaInfID(sStaName)
            nTrackID = 0
            For i = 1 To UBound(CADStaInf(nStaId).Track)
                If CADStaInf(nStaId).Track(i).nDelete = 1 Then
                    nTrackID = i
                    Exit For
                End If
            Next
            If nTrackID = 0 Then
                nTrackID = UBound(CADStaInf(nStaId).Track) + 1
                ReDim Preserve CADStaInf(nStaId).Track(nTrackID)
            End If
            sTrackCircuitNum = GetCurCircurTrackNum(nStaId)
        End If

        If sStyle = "站台线" Then
            sControlNum = ""
        End If
        CADStaInf(nStaId).Track(nTrackID).nDelete = 0
        CADStaInf(nStaId).Track(nTrackID).sStaName = CADStaInf(nStaId).sStaName
        CADStaInf(nStaId).Track(nTrackID).sStyle = sStyle
        CADStaInf(nStaId).Track(nTrackID).sGuDaoStyle = sGuDaoStyle
        CADStaInf(nStaId).Track(nTrackID).sGuDaoYongTu = sGuDaoYongTu
        CADStaInf(nStaId).Track(nTrackID).sGuDaoUseSeq = sGuDaoUseSeq
        CADStaInf(nStaId).Track(nTrackID).sngLength = sngLength
        CADStaInf(nStaId).Track(nTrackID).sTrackNum = sTrackNum
        CADStaInf(nStaId).Track(nTrackID).X1 = X1
        CADStaInf(nStaId).Track(nTrackID).Y1 = Y1
        CADStaInf(nStaId).Track(nTrackID).X2 = X2
        CADStaInf(nStaId).Track(nTrackID).Y2 = Y2
        CADStaInf(nStaId).Track(nTrackID).sLeftLink1 = sLeftLink1
        CADStaInf(nStaId).Track(nTrackID).sLeftLink2 = sLeftLink2
        CADStaInf(nStaId).Track(nTrackID).sLeftLink3 = sLeftLink3
        CADStaInf(nStaId).Track(nTrackID).sRightLink1 = sRightLink1
        CADStaInf(nStaId).Track(nTrackID).sRightLink2 = sRightLink2
        CADStaInf(nStaId).Track(nTrackID).sRightLink3 = sRightLink3
        CADStaInf(nStaId).Track(nTrackID).sTrackCircuitNum = sTrackCircuitNum
        CADStaInf(nStaId).Track(nTrackID).sControlNum = sControlNum
        CADStaInf(nStaId).Track(nTrackID).sMemo = sMemo

        Call InputTrackInf()
        Call RefreshTrackInf(nStaId, nTrackID)


        'Dim Str As String
        'Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        'If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        'Str = "update 线段信息表 set " & _
        '        "线段类型 ='" & sStyle & "'," & _
        '        "股道类型 ='" & sGuDaoStyle & "'," & _
        '        "股道用途 ='" & sGuDaoYongTu & "'," & _
        '        "股道使用顺序 ='" & sGuDaoUseSeq & "'," & _
        '        "线段长度 = '" & sngLength & "'," & _
        '        "股道或道岔编号 = '" & sTrackNum & "'," & _
        '        "X1坐标 = '" & X1 & "'," & _
        '        "Y1坐标 = '" & Y1 & "'," & _
        '        "X2坐标 = '" & X2 & "'," & _
        '        "Y2坐标 = '" & Y2 & "'," & _
        '        "左1连接 = '" & sLeftLink1 & "'," & _
        '        "左2连接 = '" & sLeftLink2 & "'," & _
        '        "左3连接 = '" & sLeftLink3 & "'," & _
        '        "右1连接 = '" & sRightLink1 & "'," & _
        '        "右2连接 = '" & sRightLink2 & "'," & _
        '        "右3连接 = '" & sRightLink3 & "'," & _
        '        "轨道电路编号 = '" & sTrackCircuitNum & "'," & _
        '        "控制模块 = '" & sControlNum & "'," & _
        '        "备注 = '" & sMemo & "' " & _
        '        "where ID = " & CADStaInf(nStaID).Track(nTrackID).nID & ""

        'Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        'Mcom.ExecuteNonQuery()
        'MyConn.Close()

        'Call InputDataToCADstaInfTrack(nStaID)

    End Sub

    '添加信号机
    Public Sub addCADStaSignal(ByVal nStaID As Integer, _
                                ByVal sStyle As String, _
                                ByVal nTrackNum As Integer, _
                                ByVal nCrossNum As Integer, _
                                ByVal X As Single, _
                                ByVal Y As Single, _
                                ByVal sMemo As String)

        Dim Str As String
        Dim nCurID As Integer
        '创建一个OledbConnection
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim strTable As String = "select * from 信号机信息表  where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' order by 信号机编号"

        Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
        Dim myDataSet As System.Data.DataSet = New System.Data.DataSet
        Mydc.Fill(myDataSet, "信号机信息表")
        Dim myTable As Data.DataTable
        myTable = myDataSet.Tables("信号机信息表")
        nCurID = myTable.Rows.Count + 1
        X = X + sngLeftBaseX
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Str = "insert into 信号机信息表 (车站名称,信号机编号,信号机类型,对应线路编号,对应道岔编号,X坐标,Y坐标,备注) values ('" & _
                    CADStaInf(nStaID).sStaName & "', '" & _
                    nCurID & "', '" & _
                    sStyle & "', '" & _
                    nTrackNum & "', '" & _
                    nCrossNum & "', '" & _
                    X & "', '" & _
                    Y & "', '" & _
                    sMemo & "')"
        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()
        MyConn.Close()

        Call InputDataToCADstaInfSignal(nStaID)

    End Sub

    '添加站台
    Public Sub addCADStaPlatform(ByVal nStaID As Integer, _
                                ByVal sStyle As String, _
                                ByVal nWidth As Single, _
                                ByVal nHeight As Single, _
                                ByVal X1 As Single, _
                                ByVal Y1 As Single, _
                                ByVal X2 As Single, _
                                ByVal Y2 As Single, _
                                ByVal sMemo As String)
        Dim Str As String
        Dim nCurID As Integer
        '创建一个OledbConnection
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim strTable As String = "select * from 站台信息表  where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' order by 站台编号"

        Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
        Dim myDataSet As System.Data.DataSet = New System.Data.DataSet
        Mydc.Fill(myDataSet, "站台信息表")
        Dim myTable As Data.DataTable
        myTable = myDataSet.Tables("站台信息表")
        nCurID = myTable.Rows.Count + 1

        X1 = X1 + sngLeftBaseX
        X2 = X2 + sngLeftBaseX

        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Str = "insert into 站台信息表 (车站名称,站台编号,站台长,站台宽,站台类型,X1坐标,Y1坐标,X2坐标,Y2坐标,备注) values ('" & _
                    CADStaInf(nStaID).sStaName & "', '" & _
                    nCurID & "', '" & _
                    nWidth & "', '" & _
                    nHeight & "', '" & _
                    sStyle & "', '" & _
                    X1 & "', '" & _
                    Y1 & "', '" & _
                    X2 & "', '" & _
                    Y2 & "', '" & _
                    sMemo & "')"
        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()
        MyConn.Close()

        Call InputDataToCADstaInfPlatform(nStaID)
    End Sub

    '添加文字
    Public Sub addCADStaFontText(ByVal nStaID As Integer, _
                                ByVal sText As String, _
                                ByVal FontName As String, _
                                ByVal FontSize As Single, _
                                ByVal Italic As String, _
                                ByVal Bold As String, _
                                ByVal FontColor As String, _
                                ByVal X As Single, _
                                ByVal Y As Single, _
                                ByVal sMemo As String)

        Dim Str As String
        Dim nCurID As Integer
        '创建一个OledbConnection
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim strTable As String = "select * from 文字信息表  where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' order by 文字编号"

        Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
        Dim myDataSet As System.Data.DataSet = New System.Data.DataSet
        Mydc.Fill(myDataSet, "文字信息表")
        Dim myTable As Data.DataTable
        myTable = myDataSet.Tables("文字信息表")
        nCurID = myTable.Rows.Count + 1

        X = X + sngLeftBaseX
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Str = "insert into 文字信息表 (车站名称,文字编号,文字内容,字体名称,字体大小,字体斜体,字体加粗,字体颜色,X坐标,Y坐标,备注) values ('" & _
                    CADStaInf(nStaID).sStaName & "', '" & _
                    nCurID & "', '" & _
                    sText & "', '" & _
                    FontName & "', '" & _
                    FontSize & "', '" & _
                    Italic & "', '" & _
                    Bold & "', '" & _
                    FontColor & "', '" & _
                    X & "', '" & _
                    Y & "', '" & _
                    sMemo & "')"
        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()
        MyConn.Close()

        Call InputDataToCADstaInfFontText(nStaID)

    End Sub


    '修改信号机信息
    Public Sub EditCADStaSignal(ByVal nStaID As Integer, _
                                ByVal nSignalID As Integer, _
                                ByVal sStyle As String, _
                                ByVal nTrackNum As String, _
                                ByVal nCrossNum As Integer, _
                                ByVal X As Single, _
                                ByVal Y As Single, _
                                ByVal sMemo As String)

        Dim Str As String

        X = X + sngLeftBaseX
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Str = "update 信号机信息表 set " & _
                "信号机类型 ='" & sStyle & "'," & _
                "对应线路编号 = '" & nTrackNum & "'," & _
                "对应道岔编号 = '" & nCrossNum & "'," & _
                "X坐标 = '" & X & "'," & _
                "Y坐标 = '" & Y & "'," & _
                "备注 = '" & sMemo & "' " & _
                "where ID = " & CADStaInf(nStaID).Signal(nSignalID).nID & ""

        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()
        MyConn.Close()
        Call InputDataToCADstaInfSignal(nStaID)

    End Sub

    '修改站台信息
    Public Sub EditCADStaPlatform(ByVal nStaID As Integer, _
                                ByVal nPlatformID As Integer, _
                                ByVal sStyle As String, _
                                ByVal nWidth As Single, _
                                ByVal nHeight As Single, _
                                ByVal X1 As Single, _
                                ByVal Y1 As Single, _
                                ByVal X2 As Single, _
                                ByVal Y2 As Single, _
                                ByVal sMemo As String)

        Dim Str As String

        X1 = X1 + sngLeftBaseX
        X2 = X2 + sngLeftBaseX
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Str = "update 站台信息表 set " & _
                "站台长 ='" & nWidth & "'," & _
                "站台宽 = '" & nHeight & "'," & _
                "站台类型 = '" & sStyle & "'," & _
                "X1坐标 = '" & X1 & "'," & _
                "Y1坐标 = '" & Y1 & "'," & _
                "X2坐标 = '" & X2 & "'," & _
                "Y2坐标 = '" & Y2 & "'," & _
                "备注 = '" & sMemo & "' " & _
                "where ID = " & CADStaInf(nStaID).PlatForm(nPlatformID).nID & ""

        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()
        MyConn.Close()
        Call InputDataToCADstaInfPlatform(nStaID)

    End Sub

    '修改文字信息
    Public Sub EditCADStaFontText(ByVal nStaID As Integer, _
                                ByVal nFontTextID As Integer, _
                                ByVal sText As String, _
                                ByVal FontName As String, _
                                ByVal FontSize As Single, _
                                ByVal Italic As Boolean, _
                                ByVal Bold As Boolean, _
                                ByVal FontColor As String, _
                                ByVal X As Single, _
                                ByVal Y As Single, _
                                ByVal sMemo As String)

        Dim Str As String

        X = X + sngLeftBaseX
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Str = "update 文字信息表 set " & _
                "文字内容 ='" & sText & "'," & _
                "字体名称 = '" & FontName & "'," & _
                "字体大小 = '" & FontSize & "'," & _
                "字体斜体 = '" & Italic & "'," & _
                "字体加粗 = '" & Bold & "'," & _
                "字体颜色 = '" & FontColor & "'," & _
                "X坐标 = '" & X & "'," & _
                "Y坐标 = '" & Y & "'," & _
                "备注 = '" & sMemo & "' " & _
                "where ID = " & CADStaInf(nStaID).FontText(nFontTextID).nID & ""

        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()
        MyConn.Close()
        Call InputDataToCADstaInfFontText(nStaID)

    End Sub
    '从数据库导入所有数据到CADStaInf()中
    Public Sub InputAllDataToCADstaInf(ByVal proBar As Windows.Forms.ToolStripProgressBar)
        Dim k As Integer
        If UBound(CADStaInf) > 0 Then
            proBar.Visible = True
            proBar.Value = 0
            proBar.Maximum = 1000
            For k = 1 To UBound(CADStaInf)
                Call InputDataToCADstaInfTrackByDAO(k)
                'Call InputDataToCADstaInfSignalByDAO(k)
                'Call InputDataToCADstaInfPlatformByDAO(k)
                'Call InputDataToCADstaInfFontTextByDAO(k)
                proBar.Value = Val(k * (1000 / UBound(CADStaInf)))
            Next
            Call InputTrackInf()
            proBar.Visible = False
        End If
    End Sub

    '从数据库导入数据到CADStaInf()中
    Public Sub InputDataToCADstaInf(ByVal nStaID As Integer)
        Call InputDataToCADstaInfTrackByDAO(nStaID)
        'Call InputDataToCADstaInfSignalByDAO(nStaID)
        'Call InputDataToCADstaInfPlatformByDAO(nStaID)
        Call InputDataToCADstaInfFontTextByDAO(nStaID)
        Call InputTrackInf()

        'Call InputDataToCADstaInfTrack(nStaID)
        'Call InputDataToCADstaInfSignal(nStaID)
        'Call InputDataToCADstaInfPlatform(nStaID)
        'Call InputDataToCADstaInfFontText(nStaID)
    End Sub

    '从数据库中导入线段信息
    'Public Sub InputDataToCADstaInfTrack(ByVal nStaID As Integer)
    '    Dim i As Integer
    '    'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
    '    Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
    '    Dim strTable As String = "select * from 线段信息表 where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' order by 线段编号 "

    '    Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
    '    Dim myDataSet As System.Data.DataSet = New System.Data.DataSet
    '    Mydc.Fill(myDataSet, "线段信息表")
    '    Dim myTable As Data.DataTable
    '    myTable = myDataSet.Tables("线段信息表")
    '    Dim k As Integer
    '    For k = 1 To UBound(CADStaInf)
    '        If CADStaInf(k).sStaName = CADStaInf(nStaID).sStaName Then
    '            ReDim CADStaInf(k).Track(myTable.Rows.Count)
    '            For i = 1 To myTable.Rows.Count
    '                CADStaInf(k).Track(i).nID = myTable.Rows(i - 1).Item("ID")
    '                CADStaInf(k).Track(i).sStaName = myTable.Rows(i - 1).Item("车站名称").ToString.Trim
    '                CADStaInf(k).Track(i).nNum = myTable.Rows(i - 1).Item("线段编号").ToString.Trim
    '                CADStaInf(k).Track(i).sStyle = myTable.Rows(i - 1).Item("线段类型").ToString.Trim
    '                CADStaInf(k).Track(i).sGuDaoStyle = myTable.Rows(i - 1).Item("股道类型").ToString.Trim
    '                CADStaInf(k).Track(i).sGuDaoYongTu = myTable.Rows(i - 1).Item("股道用途").ToString.Trim
    '                CADStaInf(k).Track(i).sGuDaoUseSeq = myTable.Rows(i - 1).Item("股道使用顺序").ToString.Trim
    '                CADStaInf(k).Track(i).sngLength = myTable.Rows(i - 1).Item("线段长度").ToString.Trim
    '                CADStaInf(k).Track(i).sTrackNum = myTable.Rows(i - 1).Item("股道或道岔编号").ToString.Trim
    '                CADStaInf(k).Track(i).X1 = myTable.Rows(i - 1).Item("X1坐标").ToString.Trim
    '                CADStaInf(k).Track(i).Y1 = myTable.Rows(i - 1).Item("Y1坐标").ToString.Trim
    '                CADStaInf(k).Track(i).X2 = myTable.Rows(i - 1).Item("X2坐标").ToString.Trim
    '                CADStaInf(k).Track(i).Y2 = myTable.Rows(i - 1).Item("Y2坐标").ToString.Trim
    '                CADStaInf(k).Track(i).sLeftLink1 = myTable.Rows(i - 1).Item("左1连接").ToString.Trim
    '                CADStaInf(k).Track(i).sLeftLink2 = myTable.Rows(i - 1).Item("左2连接").ToString.Trim
    '                CADStaInf(k).Track(i).sLeftLink3 = myTable.Rows(i - 1).Item("左3连接").ToString.Trim
    '                CADStaInf(k).Track(i).sRightLink1 = myTable.Rows(i - 1).Item("右1连接").ToString.Trim
    '                CADStaInf(k).Track(i).sRightLink2 = myTable.Rows(i - 1).Item("右2连接").ToString.Trim
    '                CADStaInf(k).Track(i).sRightLink3 = myTable.Rows(i - 1).Item("右3连接").ToString.Trim
    '                CADStaInf(k).Track(i).sTrackCircuitNum = myTable.Rows(i - 1).Item("轨道电路编号").ToString.Trim
    '                CADStaInf(k).Track(i).sControlNum = myTable.Rows(i - 1).Item("控制模块").ToString.Trim
    '                CADStaInf(k).Track(i).sMemo = myTable.Rows(i - 1).Item("备注").ToString.Trim
    '                ReDim Preserve CADStaInf(k).Track(i).TrackOcupyFirTime(0)
    '                ReDim Preserve CADStaInf(k).Track(i).TrackOcuPySecTime(0)
    '            Next
    '        End If
    '    Next k
    '    Call InputTrackInf()
    'End Sub

    ''从数据库中导入线段信息DAO代码
    'Public Sub InputDataToCADstaInfTrackByDAO(ByVal nStaID As Integer)
    '    Dim i As Integer
    '    Dim myWS As dao.Workspace
    '    Dim DE As dao.DBEngine = New dao.DBEngine
    '    myWS = DE.Workspaces(0)
    '    Dim dFile As dao.Database
    '    Dim sFile As dao.Recordset
    '    Dim sFile1 As dao.Recordset

    '    dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
    '    Dim strTable As String = "select * from 线段信息表 where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' order by 线段编号 "
    '    sFile = dFile.OpenRecordset(strTable)
    '    Dim nNum As Integer
    '    nNum = 0
    '    If sFile.RecordCount > 0 Then
    '        sFile.MoveLast()
    '        nNum = sFile.RecordCount
    '    End If



    '    Dim k As Integer
    '    For k = 1 To UBound(CADStaInf)
    '        If CADStaInf(k).sStaName = CADStaInf(nStaID).sStaName Then
    '            ReDim CADStaInf(k).Track(nNum)
    '            If nNum > 0 Then
    '                sFile.MoveFirst()
    '                For i = 1 To nNum
    '                    CADStaInf(k).Track(i).nID = Val(sFile.Fields("ID").Value) 'myTable.Rows(i - 1).Item("ID")
    '                    CADStaInf(k).Track(i).sStaName = sFile.Fields("车站名称").Value.ToString.Trim ' myTable.Rows(i - 1).Item("车站名称").ToString.Trim
    '                    CADStaInf(k).Track(i).nNum = sFile.Fields("线段编号").Value.ToString.Trim ' myTable.Rows(i - 1).Item("线段编号").ToString.Trim
    '                    CADStaInf(k).Track(i).sStyle = sFile.Fields("线段类型").Value.ToString.Trim ' myTable.Rows(i - 1).Item("线段类型").ToString.Trim
    '                    CADStaInf(k).Track(i).sGuDaoStyle = sFile.Fields("股道类型").Value.ToString.Trim ' myTable.Rows(i - 1).Item("股道类型").ToString.Trim
    '                    CADStaInf(k).Track(i).sGuDaoYongTu = sFile.Fields("股道用途").Value.ToString.Trim ' myTable.Rows(i - 1).Item("股道用途").ToString.Trim
    '                    CADStaInf(k).Track(i).sGuDaoUseSeq = sFile.Fields("股道使用顺序").Value.ToString.Trim ' myTable.Rows(i - 1).Item("股道使用顺序").ToString.Trim
    '                    CADStaInf(k).Track(i).sngLength = sFile.Fields("线段长度").Value.ToString.Trim ' myTable.Rows(i - 1).Item("线段长度").ToString.Trim
    '                    CADStaInf(k).Track(i).sTrackNum = sFile.Fields("股道或道岔编号").Value.ToString.Trim '  myTable.Rows(i - 1).Item("股道或道岔编号").ToString.Trim
    '                    CADStaInf(k).Track(i).X1 = sFile.Fields("X1坐标").Value.ToString.Trim ' myTable.Rows(i - 1).Item("X1坐标").ToString.Trim
    '                    CADStaInf(k).Track(i).Y1 = sFile.Fields("Y1坐标").Value.ToString.Trim '  myTable.Rows(i - 1).Item("Y1坐标").ToString.Trim
    '                    CADStaInf(k).Track(i).X2 = sFile.Fields("X2坐标").Value.ToString.Trim '  myTable.Rows(i - 1).Item("X2坐标").ToString.Trim
    '                    CADStaInf(k).Track(i).Y2 = sFile.Fields("Y2坐标").Value.ToString.Trim ' myTable.Rows(i - 1).Item("Y2坐标").ToString.Trim
    '                    CADStaInf(k).Track(i).sLeftLink1 = sFile.Fields("左1连接").Value.ToString.Trim '  myTable.Rows(i - 1).Item("左1连接").ToString.Trim
    '                    CADStaInf(k).Track(i).sLeftLink2 = sFile.Fields("左2连接").Value.ToString.Trim ' myTable.Rows(i - 1).Item("左2连接").ToString.Trim
    '                    CADStaInf(k).Track(i).sLeftLink3 = sFile.Fields("左3连接").Value.ToString.Trim ' myTable.Rows(i - 1).Item("左3连接").ToString.Trim
    '                    CADStaInf(k).Track(i).sRightLink1 = sFile.Fields("右1连接").Value.ToString.Trim ' myTable.Rows(i - 1).Item("右1连接").ToString.Trim
    '                    CADStaInf(k).Track(i).sRightLink2 = sFile.Fields("右2连接").Value.ToString.Trim ' myTable.Rows(i - 1).Item("右2连接").ToString.Trim
    '                    CADStaInf(k).Track(i).sRightLink3 = sFile.Fields("右3连接").Value.ToString.Trim ' myTable.Rows(i - 1).Item("右3连接").ToString.Trim
    '                    CADStaInf(k).Track(i).sTrackCircuitNum = sFile.Fields("轨道电路编号").Value.ToString.Trim '  myTable.Rows(i - 1).Item("轨道电路编号").ToString.Trim
    '                    CADStaInf(k).Track(i).sControlNum = sFile.Fields("控制模块").Value.ToString.Trim ' myTable.Rows(i - 1).Item("控制模块").ToString.Trim
    '                    CADStaInf(k).Track(i).sMemo = sFile.Fields("备注").Value.ToString.Trim '  myTable.Rows(i - 1).Item("备注").ToString.Trim
    '                    CADStaInf(k).Track(i).nDelete = 0
    '                    ReDim Preserve CADStaInf(k).Track(i).TrackOcupyFirTime(0)
    '                    ReDim Preserve CADStaInf(k).Track(i).TrackOcuPySecTime(0)
    '                    sFile.MoveNext()
    '                Next
    '            End If
    '        End If
    '    Next k

    '    '导入控制方式信息
    '    For k = 1 To UBound(CADStaInf)
    '        If CADStaInf(k).sStaName = CADStaInf(nStaID).sStaName Then
    '            If SystemPara.SystemStyle = "磁浮" Then
    '                Dim strTable1 As String = "select * from 车站控制方式表 where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' order by 序号 "
    '                sFile1 = dFile.OpenRecordset(strTable1)
    '                Dim nNum1 As Integer
    '                nNum1 = 0
    '                If sFile1.RecordCount > 0 Then
    '                    sFile1.MoveLast()
    '                    nNum1 = sFile1.RecordCount
    '                End If
    '                If nNum1 > 0 Then
    '                    sFile1.MoveFirst()
    '                    If sFile1.Fields("下行控制模块").Value Is Nothing Then
    '                    Else
    '                        If sFile1.Fields("下行控制模块").Value.ToString.Trim = "" Then
    '                        Else
    '                            CADStaInf(k).sDownControlNum = sFile1.Fields("下行控制模块").Value.ToString.Trim
    '                            CADStaInf(k).sUpControlNum = sFile1.Fields("上行控制模块").Value.ToString.Trim
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    Next k


    'End Sub

    '根据控制方式导入控制模块信息
    Public Sub InputContolSchemenf(ByVal nStaID As Integer, ByVal sSchemeName As String)
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim p As Integer
        For i = 1 To UBound(CADStaInf(nStaID).ContolScheme)
            If CADStaInf(nStaID).ContolScheme(i).sSchemeName = sSchemeName Then
                For j = 1 To UBound(CADStaInf(nStaID).ContolScheme(i).STrackNum)
                    For k = 1 To UBound(CADStaInf(nStaID).Track)
                        If CADStaInf(nStaID).Track(k).sTrackCircuitNum = CADStaInf(nStaID).ContolScheme(i).STrackNum(j) Then
                            For p = 1 To UBound(TrackInf)
                                If TrackInf(p).nStaID = nStaID And TrackInf(p).nTrackID = k Then
                                    TrackInf(p).sControlNum = CADStaInf(nStaID).ContolScheme(i).SModelName(j)
                                    Exit For
                                End If
                            Next
                            Exit For
                        End If
                    Next
                Next
                Exit For
            End If
        Next

        'For i = 1 To UBound(CADStaInf(nStaID).ContolScheme)
        '    If CADStaInf(nStaID).ContolScheme(i).sSchemeName = sSchemeName Then
        '        For j = 1 To UBound(CADStaInf(nStaID).ContolScheme(i).STrackNum)
        '            For k = 1 To UBound(CADStaInf(nStaID).Track)
        '                If CADStaInf(nStaID).Track(k).sTrackCircuitNum = CADStaInf(nStaID).ContolScheme(i).STrackNum(j) Then
        '                    CADStaInf(nStaID).Track(k).sControlNum = CADStaInf(nStaID).ContolScheme(i).SModelName(j)
        '                    Exit For
        '                End If
        '            Next
        '        Next
        '        Exit For
        '    End If
        'Next
    End Sub

    ''修改控制模块信息
    'Public Sub EditContolSchemenf(ByVal nStaID As Integer, ByVal sSchemeName As String)
    '    Dim i As Integer
    '    Dim j As Integer
    '    For i = 1 To UBound(CADStaInf(nStaID).ContolScheme)
    '        If CADStaInf(nStaID).ContolScheme(i).sSchemeName = sSchemeName Then
    '            ReDim CADStaInf(nStaID).ContolScheme(i).STrackNum(UBound(CADStaInf(nStaID).Track))
    '            ReDim CADStaInf(nStaID).ContolScheme(i).SModelName(UBound(CADStaInf(nStaID).Track))
    '            For j = 1 To UBound(CADStaInf(nStaID).Track)
    '                CADStaInf(nStaID).ContolScheme(i).STrackNum(j) = CADStaInf(nStaID).Track(j).sTrackCircuitNum
    '                CADStaInf(nStaID).ContolScheme(i).SModelName(j) = CADStaInf(nStaID).Track(j).sControlNum
    '            Next
    '            Exit For
    '        End If
    '    Next
    'End Sub
    '从数据库中导入信号机信息
    Public Sub InputDataToCADstaInfSignal(ByVal nStaID As Integer)
        Dim i As Integer
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim strTable As String = "select * from 信号机信息表 where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' order by 信号机编号 "

        Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
        Dim myDataSet As System.Data.DataSet = New System.Data.DataSet
        Mydc.Fill(myDataSet, "信号机信息表")
        Dim myTable As Data.DataTable
        myTable = myDataSet.Tables("信号机信息表")
        Dim k As Integer
        For k = 1 To UBound(CADStaInf)
            If CADStaInf(k).sStaName = CADStaInf(nStaID).sStaName Then
                ReDim CADStaInf(k).Signal(myTable.Rows.Count)
                For i = 1 To myTable.Rows.Count
                    CADStaInf(k).Signal(i).nID = myTable.Rows(i - 1).Item("ID")
                    CADStaInf(k).Signal(i).sStaName = myTable.Rows(i - 1).Item("车站名称")
                    CADStaInf(k).Signal(i).nNum = myTable.Rows(i - 1).Item("信号机编号")
                    CADStaInf(k).Signal(i).sStyle = myTable.Rows(i - 1).Item("信号机类型")
                    CADStaInf(k).Signal(i).nTrackNum = myTable.Rows(i - 1).Item("对应线路编号")
                    CADStaInf(k).Signal(i).nCrossNum = myTable.Rows(i - 1).Item("对应道岔编号")
                    CADStaInf(k).Signal(i).X = myTable.Rows(i - 1).Item("X坐标")
                    CADStaInf(k).Signal(i).Y = myTable.Rows(i - 1).Item("Y坐标")
                    CADStaInf(k).Signal(i).sMemo = myTable.Rows(i - 1).Item("备注")
                Next
            End If
        Next
    End Sub

    '从数据库中导入信号机信息,DAO
    Public Sub InputDataToCADstaInfSignalByDAO(ByVal nStaID As Integer)
        Dim i As Integer
        Dim myWS As dao.Workspace
        Dim DE As dao.DBEngine = New dao.DBEngine
        myWS = DE.Workspaces(0)
        Dim dFile As dao.Database
        Dim sFile As dao.Recordset

        dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        Dim strTable As String = "select * from 信号机信息表 where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' order by 信号机编号 "
        sFile = dFile.OpenRecordset(strTable)
        Dim nNum As Integer
        Dim k As Integer
        nNum = 0
        If sFile.RecordCount > 0 Then
            sFile.MoveLast()
            nNum = sFile.RecordCount
        End If
        For k = 1 To UBound(CADStaInf)
            If CADStaInf(k).sStaName = CADStaInf(nStaID).sStaName Then
                ReDim CADStaInf(k).Signal(nNum)
                If nNum > 0 Then
                    sFile.MoveFirst()
                    For i = 1 To nNum
                        CADStaInf(k).Signal(i).nID = sFile.Fields("ID").Value ' myTable.Rows(i - 1).Item("ID")
                        CADStaInf(k).Signal(i).sStaName = sFile.Fields("车站名称").Value.ToString.Trim 'myTable.Rows(i - 1).Item("车站名称")
                        CADStaInf(k).Signal(i).nNum = sFile.Fields("车站名称").Value.ToString.Trim ' myTable.Rows(i - 1).Item("信号机编号")
                        CADStaInf(k).Signal(i).sStyle = sFile.Fields("信号机编号").Value.ToString.Trim 'myTable.Rows(i - 1).Item("信号机类型")
                        CADStaInf(k).Signal(i).nTrackNum = sFile.Fields("对应线路编号").Value.ToString.Trim ' myTable.Rows(i - 1).Item("对应线路编号")
                        CADStaInf(k).Signal(i).nCrossNum = sFile.Fields("对应道岔编号").Value.ToString.Trim 'myTable.Rows(i - 1).Item("对应道岔编号")
                        CADStaInf(k).Signal(i).X = sFile.Fields("X坐标").Value ' myTable.Rows(i - 1).Item("X坐标")
                        CADStaInf(k).Signal(i).Y = sFile.Fields("Y坐标").Value ' myTable.Rows(i - 1).Item("Y坐标")
                        CADStaInf(k).Signal(i).sMemo = sFile.Fields("备注").Value.ToString.Trim ' myTable.Rows(i - 1).Item("备注")
                        sFile.MoveNext()
                    Next
                End If
            End If
        Next
    End Sub


    '从数据库中导入站台信息 DAO
    Public Sub InputDataToCADstaInfPlatform(ByVal nStaID As Integer)
        Dim i As Integer
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim strTable As String = "select * from 站台信息表 where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' order by 站台编号 "

        Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
        Dim myDataSet As System.Data.DataSet = New System.Data.DataSet
        Mydc.Fill(myDataSet, "站台信息表")
        Dim myTable As Data.DataTable
        myTable = myDataSet.Tables("站台信息表")
        Dim k As Integer

        For k = 1 To UBound(CADStaInf)
            If CADStaInf(k).sStaName = CADStaInf(nStaID).sStaName Then
                ReDim CADStaInf(k).PlatForm(myTable.Rows.Count)
                For i = 1 To myTable.Rows.Count
                    CADStaInf(k).PlatForm(i).nID = myTable.Rows(i - 1).Item("ID")
                    CADStaInf(k).PlatForm(i).sStaName = myTable.Rows(i - 1).Item("车站名称")
                    CADStaInf(k).PlatForm(i).nNum = myTable.Rows(i - 1).Item("站台编号")
                    CADStaInf(k).PlatForm(i).nWidth = myTable.Rows(i - 1).Item("站台长")
                    CADStaInf(k).PlatForm(i).nHeight = myTable.Rows(i - 1).Item("站台宽")
                    CADStaInf(k).PlatForm(i).sStyle = myTable.Rows(i - 1).Item("站台类型")
                    CADStaInf(k).PlatForm(i).X1 = myTable.Rows(i - 1).Item("X1坐标")
                    CADStaInf(k).PlatForm(i).Y1 = myTable.Rows(i - 1).Item("Y1坐标")
                    CADStaInf(k).PlatForm(i).X2 = myTable.Rows(i - 1).Item("X2坐标")
                    CADStaInf(k).PlatForm(i).Y2 = myTable.Rows(i - 1).Item("Y2坐标")
                    CADStaInf(k).PlatForm(i).sMemo = myTable.Rows(i - 1).Item("备注")
                Next
            End If
        Next k
    End Sub

    '从数据库中导入站台信息
    Public Sub InputDataToCADstaInfPlatformByDAO(ByVal nStaID As Integer)
        Dim i As Integer
        Dim myWS As dao.Workspace
        Dim DE As dao.DBEngine = New dao.DBEngine
        myWS = DE.Workspaces(0)
        Dim dFile As dao.Database
        Dim sFile As dao.Recordset

        dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        Dim strTable As String = "select * from 站台信息表 where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' order by 站台编号 "
        sFile = dFile.OpenRecordset(strTable)
        Dim nNum As Integer
        Dim k As Integer
        nNum = 0
        If sFile.RecordCount > 0 Then
            sFile.MoveLast()
            nNum = sFile.RecordCount
        End If
        For k = 1 To UBound(CADStaInf)
            If CADStaInf(k).sStaName = CADStaInf(nStaID).sStaName Then
                ReDim CADStaInf(k).PlatForm(nNum)
                If nNum > 0 Then
                    sFile.MoveFirst()
                    For i = 1 To nNum
                        CADStaInf(k).PlatForm(i).nID = sFile.Fields("ID").Value 'myTable.Rows(i - 1).Item("ID")
                        CADStaInf(k).PlatForm(i).sStaName = sFile.Fields("车站名称").Value.ToString.Trim  'sFile.Fields("ID").Value myTable.Rows(i - 1).Item("车站名称")
                        CADStaInf(k).PlatForm(i).nNum = sFile.Fields("站台编号").Value 'myTable.Rows(i - 1).Item("站台编号")
                        CADStaInf(k).PlatForm(i).nWidth = sFile.Fields("站台长").Value 'myTable.Rows(i - 1).Item("站台长")
                        CADStaInf(k).PlatForm(i).nHeight = sFile.Fields("站台宽").Value ' myTable.Rows(i - 1).Item("站台宽")
                        CADStaInf(k).PlatForm(i).sStyle = sFile.Fields("站台类型").Value.ToString.Trim  ' myTable.Rows(i - 1).Item("站台类型")
                        CADStaInf(k).PlatForm(i).X1 = sFile.Fields("X1坐标").Value ' myTable.Rows(i - 1).Item("X1坐标")
                        CADStaInf(k).PlatForm(i).Y1 = sFile.Fields("Y1坐标").Value 'myTable.Rows(i - 1).Item("Y1坐标")
                        CADStaInf(k).PlatForm(i).X2 = sFile.Fields("X2坐标").Value 'myTable.Rows(i - 1).Item("X2坐标")
                        CADStaInf(k).PlatForm(i).Y2 = sFile.Fields("Y2坐标").Value 'myTable.Rows(i - 1).Item("Y2坐标")
                        CADStaInf(k).PlatForm(i).sMemo = sFile.Fields("备注").Value.ToString.Trim  ' myTable.Rows(i - 1).Item("备注")
                        sFile.MoveNext()
                    Next
                End If
            End If
        Next k
    End Sub

    '从数据库中导入文字信息
    Public Sub InputDataToCADstaInfFontTextByDAO(ByVal nStaID As Integer)
        Dim i As Integer
        Dim myWS As dao.Workspace
        Dim DE As dao.DBEngine = New dao.DBEngine
        myWS = DE.Workspaces(0)
        Dim dFile As dao.Database
        Dim sFile As dao.Recordset

        dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        Dim strTable As String = "select * from 文字信息表 where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' order by 文字编号 "
        sFile = dFile.OpenRecordset(strTable)
        Dim nNum As Integer
        Dim k As Integer
        nNum = 0
        If sFile.RecordCount > 0 Then
            sFile.MoveLast()
            nNum = sFile.RecordCount
        End If
        For k = 1 To UBound(CADStaInf)
            If CADStaInf(k).sStaName = CADStaInf(nStaID).sStaName Then
                ReDim CADStaInf(k).FontText(nNum)
                If nNum > 0 Then
                    sFile.MoveFirst()
                    For i = 1 To nNum
                        CADStaInf(k).FontText(i).nID = sFile.Fields("ID").Value 'myTable.Rows(i - 1).Item("ID")
                        CADStaInf(k).FontText(i).sStaName = sFile.Fields("车站名称").Value.ToString.Trim ' myTable.Rows(i - 1).Item("车站名称")
                        CADStaInf(k).FontText(i).nNum = sFile.Fields("文字编号").Value '  myTable.Rows(i - 1).Item("文字编号")
                        CADStaInf(k).FontText(i).sText = sFile.Fields("文字内容").Value.ToString.Trim ' myTable.Rows(i - 1).Item("文字内容")
                        CADStaInf(k).FontText(i).FontName = sFile.Fields("字体名称").Value.ToString.Trim ' myTable.Rows(i - 1).Item("字体名称")
                        CADStaInf(k).FontText(i).FontSize = sFile.Fields("字体大小").Value 'myTable.Rows(i - 1).Item("字体大小")
                        CADStaInf(k).FontText(i).Italic = sFile.Fields("字体斜体").Value ' myTable.Rows(i - 1).Item("字体斜体")
                        CADStaInf(k).FontText(i).Bold = sFile.Fields("字体加粗").Value ' myTable.Rows(i - 1).Item("字体加粗")
                        CADStaInf(k).FontText(i).FontColor = Color.FromArgb(sFile.Fields("字体颜色").Value.ToString.Trim) ' Color.FromArgb(myTable.Rows(i - 1).Item("字体颜色"))
                        CADStaInf(k).FontText(i).X = sFile.Fields("X坐标").Value 'myTable.Rows(i - 1).Item("X坐标")
                        CADStaInf(k).FontText(i).Y = sFile.Fields("Y坐标").Value 'myTable.Rows(i - 1).Item("Y坐标")
                        CADStaInf(k).FontText(i).sMemo = sFile.Fields("备注").Value.ToString.Trim '  myTable.Rows(i - 1).Item("备注")
                        sFile.MoveNext()
                    Next
                End If
            End If
        Next k

    End Sub

    '从数据库中导入文字信息
    Public Sub InputDataToCADstaInfFontText(ByVal nStaID As Integer)
        Dim i As Integer
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim strTable As String = "select * from 文字信息表 where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' order by 文字编号 "

        Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
        Dim myDataSet As System.Data.DataSet = New System.Data.DataSet
        Mydc.Fill(myDataSet, "文字信息表")
        Dim myTable As Data.DataTable
        myTable = myDataSet.Tables("文字信息表")
        Dim k As Integer

        For k = 1 To UBound(CADStaInf)
            If CADStaInf(k).sStaName = CADStaInf(nStaID).sStaName Then
                ReDim CADStaInf(k).FontText(myTable.Rows.Count)
                For i = 1 To myTable.Rows.Count
                    CADStaInf(k).FontText(i).nID = myTable.Rows(i - 1).Item("ID")
                    CADStaInf(k).FontText(i).sStaName = myTable.Rows(i - 1).Item("车站名称")
                    CADStaInf(k).FontText(i).nNum = myTable.Rows(i - 1).Item("文字编号")
                    CADStaInf(k).FontText(i).sText = myTable.Rows(i - 1).Item("文字内容")
                    CADStaInf(k).FontText(i).FontName = myTable.Rows(i - 1).Item("字体名称")
                    CADStaInf(k).FontText(i).FontSize = myTable.Rows(i - 1).Item("字体大小")
                    CADStaInf(k).FontText(i).Italic = myTable.Rows(i - 1).Item("字体斜体")
                    CADStaInf(k).FontText(i).Bold = myTable.Rows(i - 1).Item("字体加粗")
                    CADStaInf(k).FontText(i).FontColor = Color.FromArgb(myTable.Rows(i - 1).Item("字体颜色"))
                    CADStaInf(k).FontText(i).X = myTable.Rows(i - 1).Item("X坐标")
                    CADStaInf(k).FontText(i).Y = myTable.Rows(i - 1).Item("Y坐标")
                    CADStaInf(k).FontText(i).sMemo = myTable.Rows(i - 1).Item("备注")
                Next
            End If
        Next k
    End Sub

    '删除线段
    Public Sub DeleteTrack()
        Dim j As Integer
        Dim tmpStaID As Integer
        Dim tmpTrackID As Integer

        For j = 1 To UBound(curSelectTrackID)
            tmpStaID = tmpTrackInf(curSelectTrackID(j)).nStaID
            tmpTrackID = tmpTrackInf(curSelectTrackID(j)).nTrackID
            CADStaInf(tmpStaID).Track(tmpTrackID).nDelete = 1
        Next

        'Dim Str As String
        'Dim i, j As Integer
        'Dim tmpStaID As Integer
        'Dim tmpTrackID As Integer
        'If MsgBox("确定删除这些线段吗？", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2, "确认删除") = MsgBoxResult.Cancel Then Exit Sub
        'For j = 1 To UBound(curSelectTrackID)
        '    tmpStaID = tmpTrackInf(curSelectTrackID(j)).nStaID
        '    tmpTrackID = tmpTrackInf(curSelectTrackID(j)).nTrackID
        '    '创建一个OledbConnection
        '    'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        '    Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)

        '    If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        '    Str = "delete * from 线段信息表  where 车站名称 = '" & CADStaInf(tmpStaID).sStaName & "' and ID = " & CADStaInf(tmpStaID).Track(tmpTrackID).nID & " "
        '    Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        '    Mcom.ExecuteNonQuery()
        '    MyConn.Close()
        'Next
        'Dim MyConn1 As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        'Dim strTable As String = "select * from 线段信息表 where 车站名称 = '" & CADStaInf(tmpStaID).sStaName & "' order by 线段编号 "
        'Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
        'Dim myDataSet As System.Data.DataSet = New System.Data.DataSet
        'Mydc.Fill(myDataSet, "线段信息表")
        'Dim myTable As Data.DataTable
        'myTable = myDataSet.Tables("线段信息表")
        'If MyConn1.State = Data.ConnectionState.Closed Then MyConn1.Open()

        'For i = 1 To myTable.Rows.Count

        '    Str = "update 线段信息表 set " & _
        '              "线段编号 ='" & i.ToString & "'," & _
        '             "轨道电路编号 = '" & CADStaInf(tmpStaID).sStaCode & i.ToString & "' " & _
        '              "where ID = " & myTable.Rows(i - 1).Item("ID") & ""
        '    Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn1)
        '    Mcom1.ExecuteNonQuery()
        'Next

        'Call InputDataToCADstaInfTrack(tmpStaID)

    End Sub

    '删除信号机
    Public Sub DeleteSignal(ByVal nStaID As Integer, ByVal nSignalID As Integer)
        If MsgBox("确定删除该信号机吗？", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2, "确认删除") = MsgBoxResult.Cancel Then Exit Sub

        Dim Str As String
        Dim i As Integer
        '创建一个OledbConnection
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)

        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Str = "delete * from 信号机信息表  where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' and ID = " & CADStaInf(nStaID).Signal(nSignalID).nID & " "
        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()
        MyConn.Close()

        Dim MyConn1 As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim strTable As String = "select * from 信号机信息表 where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' order by 信号机编号 "
        Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
        Dim myDataSet As System.Data.DataSet = New System.Data.DataSet
        Mydc.Fill(myDataSet, "信号机信息表")
        Dim myTable As Data.DataTable
        myTable = myDataSet.Tables("信号机信息表")
        If MyConn1.State = Data.ConnectionState.Closed Then MyConn1.Open()

        For i = 1 To myTable.Rows.Count
            Str = "update 信号机信息表 set 信号机编号 = " & i.ToString & " where ID = " & myTable.Rows(i - 1).Item("ID") & ""
            Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn1)
            Mcom1.ExecuteNonQuery()
        Next
        Call InputDataToCADstaInfSignal(nStaID)

    End Sub

    '删除站台
    Public Sub DeletePlatform(ByVal nStaID As Integer, ByVal nPlatformID As Integer)
        If MsgBox("确定删除该站台吗？", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2, "确认删除") = MsgBoxResult.Cancel Then Exit Sub
        Dim Str As String
        Dim i As Integer
        '创建一个OledbConnection
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)

        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Str = "delete * from 站台信息表  where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' and ID = " & CADStaInf(nStaID).PlatForm(nPlatformID).nID & " "
        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()
        MyConn.Close()

        Dim MyConn1 As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim strTable As String = "select * from 站台信息表 where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' order by 站台编号 "
        Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
        Dim myDataSet As System.Data.DataSet = New System.Data.DataSet
        Mydc.Fill(myDataSet, "站台信息表")
        Dim myTable As Data.DataTable
        myTable = myDataSet.Tables("站台信息表")
        If MyConn1.State = Data.ConnectionState.Closed Then MyConn1.Open()

        For i = 1 To myTable.Rows.Count
            Str = "update 站台信息表 set 站台编号 = " & i.ToString & " where ID = " & myTable.Rows(i - 1).Item("ID") & ""
            Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn1)
            Mcom1.ExecuteNonQuery()
        Next
        Call InputDataToCADstaInfPlatform(nStaID)

    End Sub

    '删除字体
    '删除信号机
    Public Sub DeleteFontText(ByVal nStaID As Integer, ByVal nFontTextID As Integer)
        If MsgBox("确定删除该文字吗？", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2, "确认删除") = MsgBoxResult.Cancel Then Exit Sub

        Dim Str As String
        Dim i As Integer
        '创建一个OledbConnection
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)

        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Str = "delete * from 文字信息表  where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' and ID = " & CADStaInf(nStaID).FontText(nFontTextID).nID & " "
        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()
        MyConn.Close()

        Dim MyConn1 As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim strTable As String = "select * from 文字信息表 where 车站名称 = '" & CADStaInf(nStaID).sStaName & "' order by 文字编号 "
        Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
        Dim myDataSet As System.Data.DataSet = New System.Data.DataSet
        Mydc.Fill(myDataSet, "文字信息表")
        Dim myTable As Data.DataTable
        myTable = myDataSet.Tables("文字信息表")
        If MyConn1.State = Data.ConnectionState.Closed Then MyConn1.Open()

        For i = 1 To myTable.Rows.Count
            Str = "update 文字信息表 set 文字编号 = " & i.ToString & " where ID = " & myTable.Rows(i - 1).Item("ID") & ""
            Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn1)
            Mcom1.ExecuteNonQuery()
        Next
        Call InputDataToCADstaInfFontText(nStaID)

    End Sub
    '得到选中的设备的ID号
    Public Function GetEquipID(ByVal X As Single, ByVal Y As Single) As String
        Dim nCurLen As Single
        Dim i As Integer
        GetEquipID = "NULL"

        Dim nTmpTrackID As Integer
        Dim X1, X2, Y1, Y2 As Single
        Dim nWidth As Single
        nWidth = 100
        Dim nH As Single
        nH = 10000

        For i = 1 To UBound(tmpTrackInf)
            X1 = tmpTrackInf(i).X1
            Y1 = tmpTrackInf(i).Y1
            X2 = tmpTrackInf(i).X2
            Y2 = tmpTrackInf(i).Y2
            nCurLen = Getdistance(X1, Y1, X2, Y2, X, Y)
            If nCurLen < nWidth Then
                If nCurLen < nH Then
                    nTmpTrackID = i
                    nH = nCurLen
                End If
            End If
        Next

        If nTmpTrackID > 0 Then
            GetEquipID = "T" & nTmpTrackID
        End If

        ''选择信号机
        'nH1 = nH
        'h = 10000
        'For i = 1 To UBound(CADStaInf(nStaID).Signal)
        '    X1 = CADStaInf(nStaID).Signal(i).X
        '    Y1 = CADStaInf(nStaID).Signal(i).Y
        '    h = Math.Sqrt((X - X1) ^ 2 + (Y - Y1) ^ 2)
        '    If h < nWidth Then
        '        If h < nH1 Then
        '            nTmpSignalID = i
        '            nH1 = h
        '        End If
        '    End If

        'Next

        'If nTmpSignalID > 0 Then
        '    GetEquipID = "S" & nTmpSignalID
        'End If
        ''选择站台

        'nH2 = nH1
        'h = 10000
        'For i = 1 To UBound(CADStaInf(nStaID).PlatForm)
        '    X1 = CADStaInf(nStaID).PlatForm(i).X1
        '    Y1 = CADStaInf(nStaID).PlatForm(i).Y1
        '    X2 = CADStaInf(nStaID).PlatForm(i).X2
        '    Y2 = CADStaInf(nStaID).PlatForm(i).Y2
        '    If X >= X1 - 20 And X <= X2 + 20 And Y >= Y1 - 20 And Y <= Y2 + 20 Then
        '        h = GetPlatformMinDis(X1, Y1, X2, Y2, X, Y)
        '        If h < nWidth Then
        '            If h < nH2 Then
        '                nTmpPlatformID = i
        '                nH2 = h
        '            End If
        '        End If
        '    End If
        'Next

        'If nTmpPlatformID > 0 Then
        '    GetEquipID = "P" & nTmpPlatformID
        'End If

        'nH3 = nH2
        'h = 10000
        'For i = 1 To UBound(CADStaInf(nStaID).FontText)
        '    X1 = CADStaInf(nStaID).FontText(i).X
        '    Y1 = CADStaInf(nStaID).FontText(i).Y
        '    X2 = X1 + CADStaInf(nStaID).FontText(i).sText.Length * CADStaInf(nStaID).FontText(i).FontSize * 1.5
        '    Y2 = Y1 + CADStaInf(nStaID).FontText(i).FontSize * 1.5
        '    If X >= X1 And X <= X2 And Y >= Y1 And Y <= Y2 Then
        '        h = GetPlatformMinDis(X1, Y1, X2, Y2, X, Y)
        '        If h < nWidth Then
        '            If h < nH2 Then
        '                nTmpFontTextID = i
        '                nH2 = h
        '            End If
        '        End If
        '    End If
        'Next

        'If nTmpFontTextID > 0 Then
        '    GetEquipID = "E" & nTmpFontTextID
        'End If

        If GetEquipID = "" Then
            GetEquipID = "NULL"
        End If

    End Function

    '得以点到矩形边的最小距离
    Public Function GetPlatformMinDis(ByVal X1 As Single, ByVal Y1 As Single, ByVal X2 As Single, ByVal Y2 As Single, ByVal X As Single, ByVal Y As Single) As Single
        Dim h1, h2, h3, h4, h5, h6 As Single
        h1 = Math.Abs(X - X1)
        h2 = Math.Abs(Y - Y1)
        h3 = Math.Abs(X - X2)
        h4 = Math.Abs(Y - Y2)
        h5 = Math.Min(h1, h2)
        h6 = Math.Min(h3, h4)
        GetPlatformMinDis = Math.Min(h5, h6)
    End Function

    '得到选中的线的ID号
    Public Function GetTrackID(ByVal nStaID As Integer, ByVal X As Single, ByVal Y As Single) As Integer
        Dim i As Integer
        Dim X1, X2, Y1, Y2 As Single
        Dim nWidth As Single
        nWidth = 1000
        Dim nH As Single
        nH = 1000
        Dim A, B, C, h As Single
        GetTrackID = 0
        For i = 1 To UBound(CADStaInf(nStaID).Track)
            X1 = CADStaInf(nStaID).Track(i).X1
            Y1 = CADStaInf(nStaID).Track(i).Y1
            X2 = CADStaInf(nStaID).Track(i).X2
            Y2 = CADStaInf(nStaID).Track(i).Y2
            h = 1000
            If X2 - X1 = 0 Then

                If Y1 > Y2 Then
                    If Y >= Y2 And Y <= Y1 Then
                        h = Math.Abs(X - X1)
                    End If
                Else
                    If Y >= Y1 And Y <= Y2 Then
                        h = Math.Abs(X - X1)
                    End If
                End If
            ElseIf Y2 - Y1 = 0 Then
                If X >= X1 And X <= X2 Then
                    h = Math.Abs(Y - Y1)
                End If
            Else
                If Y2 > Y1 Then
                    If Y >= Y1 And Y <= Y2 And X >= X1 And X <= X2 Then
                        B = -1
                        A = (Y2 - Y1) / (X2 - X1)
                        C = Y1 - X1 * A
                        h = Math.Abs(A * X + B * Y + C) / Math.Sqrt(A ^ 2 + B ^ 2)
                    End If
                Else
                    If Y >= Y2 And Y <= Y1 And X >= X1 And X <= X2 Then
                        B = -1
                        A = (Y2 - Y1) / (X2 - X1)
                        C = Y1 - X1 * A
                        h = Math.Abs(A * X + B * Y + C) / Math.Sqrt(A ^ 2 + B ^ 2)
                    End If

                End If
            End If
            If h < nWidth Then
                If h < nH Then
                    GetTrackID = i
                    nH = h
                End If
                'Exit For
            End If
        Next
    End Function

    Public Sub listTreeViewDataInCADform(ByVal treView As TreeView)
        '导入线路与车站信息到TreeView框中
        Dim i As Integer
        Dim j As Integer
        Dim nNode As TreeNode
        Dim nNode2 As TreeNode
        Dim nNode3 As TreeNode
        treView.Nodes.Clear()

        'nNode = treView.Nodes.Add(NetInf.sNetName)
        nNode = treView.Nodes.Add("车站区间")
        nNode.ImageIndex = 0
        For i = 1 To UBound(NetInf.Line)
            nNode2 = nNode.Nodes.Add(NetInf.Line(i).sName)
            nNode2.ImageIndex = 1
            For j = 1 To UBound(CADStaInf)
                If CADStaInf(j).nLineID = i Then
                    nNode3 = nNode2.Nodes.Add(CADStaInf(j).sStaName)
                    nNode3.ImageIndex = 2
                End If
            Next j
        Next i

        '分别显示车站与区间
        'nNode = treView.Nodes.Add(NetInf.sNetName)
        'For i = 1 To UBound(NetInf.Line)
        '    nNode2 = nNode.Nodes.Add(NetInf.Line(i).sName)
        '    nNode3 = nNode2.Nodes.Add("车站")
        '    For j = 1 To UBound(CADStaInf)
        '        If CADStaInf(j).nLineID = i Then
        '            If CADStaInf(j).sStaOrSec = "车站" Then
        '                nNode4 = nNode3.Nodes.Add(CADStaInf(j).sStaName)
        '            End If
        '        End If
        '    Next j

        '    nNode3 = nNode2.Nodes.Add("区间")
        '    For j = 1 To UBound(CADStaInf)
        '        If CADStaInf(j).nLineID = i Then
        '            If CADStaInf(j).sStaOrSec = "区间" Then
        '                nNode4 = nNode3.Nodes.Add(CADStaInf(j).sStaName)
        '            End If
        '        End If
        '    Next j
        'Next i

        treView.ExpandAll()
    End Sub

    '得到车站最左边的点的坐标
    Public Function GetCurStsLeftX(ByVal nStaID As Integer) As Single
        Dim i As Integer
        Dim curX As Single
        Dim minX As Single
        minX = 100000
        If nStaID <= 0 Then
            GetCurStsLeftX = -40
            Exit Function
        End If
        For i = 1 To UBound(CADStaInf(nStaID).Track)
            curX = CADStaInf(nStaID).Track(i).X1
            If curX < minX Then
                minX = curX
            End If
        Next
        For i = 1 To UBound(CADStaInf(nStaID).Signal)
            curX = CADStaInf(nStaID).Signal(i).X
            If curX < minX Then
                minX = curX
            End If
        Next
        For i = 1 To UBound(CADStaInf(nStaID).PlatForm)
            curX = CADStaInf(nStaID).PlatForm(i).X1
            If curX < minX Then
                minX = curX
            End If
        Next
        For i = 1 To UBound(CADStaInf(nStaID).FontText)
            curX = CADStaInf(nStaID).FontText(i).X
            If curX < minX Then
                minX = curX
            End If
        Next
        If minX <> 100000 Then
            GetCurStsLeftX = minX - 40
        Else
            GetCurStsLeftX = -40
        End If
    End Function
    Public Sub PrintTrackInPic(ByVal rBmpGraphics As Graphics, ByVal cForColor As Color, ByVal LineWidth As Single, _
                                         ByVal sngScale As Single, ByVal IfShowOtherInf As Boolean, ByVal IfPrintGuDaoNum As Boolean, _
                                         ByVal IfPrintTrackNum As Boolean, ByVal IfPrintCorssNum As Boolean)

        Dim nStaID As Integer
        Dim nTrackID As Integer
        Dim X1 As Single
        Dim Y1 As Single
        Dim X2 As Single
        Dim Y2 As Single
        Dim i As Integer
        If sngScale <= 0.6 Then
            IfShowOtherInf = False
            IfPrintTrackNum = False
            IfPrintGuDaoNum = False
            IfPrintCorssNum = False
        End If

        LineWidth = GetShowLineWidth(LineWidth, sngScale)
        For i = 1 To UBound(tmpTrackInf)
            '画线段
            X1 = tmpTrackInf(i).X1
            Y1 = tmpTrackInf(i).Y1
            X2 = tmpTrackInf(i).X2
            Y2 = tmpTrackInf(i).Y2
            nStaID = tmpTrackInf(i).nStaID
            nTrackID = tmpTrackInf(i).nTrackID
            Call DrawTrackLine(rBmpGraphics, nStaID, nTrackID, cForColor, LineWidth, sngScale, IfShowOtherInf, IfPrintGuDaoNum, IfPrintTrackNum, IfPrintCorssNum, X1, Y1, X2, Y2, tmpTrackInf(i).sControlNum)
        Next

        'For i = 1 To UBound(tmpFontInf)
        '    '画线段
        '    X1 = tmpFontInf(i).X
        '    Y1 = tmpFontInf(i).Y
        '    nStaID = tmpFontInf(i).nStaID
        '    nTrackID = tmpFontInf(i).nFontID
        '    Dim sText As String
        '    Dim FontName As String
        '    Dim FontSize As Single
        '    Dim FontColor As Color
        '    sText = CADStaInf(nStaID).FontText(nTrackID).sText
        '    FontSize = CADStaInf(nStaID).FontText(nTrackID).FontSize
        '    FontName = CADStaInf(nStaID).FontText(nTrackID).FontName
        '    FontColor = CADStaInf(nStaID).FontText(nTrackID).FontColor
        '    Dim s As New FontStyle
        '    If CADStaInf(nStaID).FontText(nTrackID).Italic = True Then
        '        s = CADStaInf(nStaID).FontText(nTrackID).Italic
        '    End If
        '    If CADStaInf(nStaID).FontText(nTrackID).Bold = True Then
        '        s = CADStaInf(nStaID).FontText(nTrackID).Bold
        '    End If
        '    Dim b As New SolidBrush(FontColor)
        '    Dim f As New Font(FontName, FontSize, s)
        '    If X1 > 0 And Y1 > 0 Then
        '        rBmpGraphics.DrawString(sText, f, b, X1, Y1)
        '    End If
        'Next

        ''打印文字
        'For i = 1 To UBound(CADStaInf(nStaID).FontText)
        '    X1 = CADStaInf(nStaID).FontText(i).X - sngLeftX
        '    Y1 = CADStaInf(nStaID).FontText(i).Y - sngTopY
        '    Dim sText As String
        '    Dim FontName As String
        '    Dim FontSize As Single
        '    Dim FontColor As Color
        '    sText = CADStaInf(nStaID).FontText(i).sText
        '    FontSize = CADStaInf(nStaID).FontText(i).FontSize
        '    FontName = CADStaInf(nStaID).FontText(i).FontName
        '    FontColor = CADStaInf(nStaID).FontText(i).FontColor
        '    Dim s As New FontStyle
        '    If CADStaInf(nStaID).FontText(i).Italic = True Then
        '        s = CADStaInf(nStaID).FontText(i).Italic
        '    End If
        '    If CADStaInf(nStaID).FontText(i).Bold = True Then
        '        s = CADStaInf(nStaID).FontText(i).Bold
        '    End If
        '    Dim b As New SolidBrush(FontColor)
        '    Dim f As New Font(FontName, FontSize, s)
        '    If X1 > 0 And Y1 > 0 Then
        '        rBmpGraphics.DrawString(sText, f, b, X1, Y1)
        '    End If
        'Next i
    End Sub

    Public Sub DrawTrackLine(ByVal rBmpGraphics As Graphics, ByVal nStaID As Integer, ByVal nTrackID As Integer, ByVal cForColor As Color, ByVal LineWidth As Single, _
                                         ByVal sngScale As Single, ByVal IfShowOtherInf As Boolean, ByVal IfPrintGuDaoNum As Boolean, _
                                         ByVal IfPrintTrackNum As Boolean, ByVal IfPrintCorssNum As Boolean, ByVal X1 As Single, ByVal Y1 As Single, ByVal X2 As Single, ByVal Y2 As Single, ByVal sControlNum As String)
        Dim sTrackStyle As String
        Dim sTrackCurNum As String
        Dim sGuDaoNum As String
        Dim sGuDaoStyle As String
        Dim sCrossNum As String
        Dim curColor As Color
        Dim curPen As Pen
        Dim nFontSize As Single
        sGuDaoNum = ""
        sTrackStyle = CADStaInf(nStaID).Track(nTrackID).sStyle
        sTrackCurNum = CADStaInf(nStaID).Track(nTrackID).sTrackCircuitNum
        ' If sTrackCurNum = "F43" Then Stop
        sGuDaoStyle = CADStaInf(nStaID).Track(nTrackID).sGuDaoStyle
        If sGuDaoStyle = Nothing Then sGuDaoStyle = ""
        sCrossNum = CADStaInf(nStaID).Track(nTrackID).sTrackNum
        If CADStaInf(nStaID).Track(nTrackID).sStyle.Length >= 3 Then
            If CADStaInf(nStaID).Track(nTrackID).sStyle.Substring(CADStaInf(nStaID).Track(nTrackID).sStyle.Length - 3) = "股道线" Then
                If sGuDaoStyle.Length >= 3 Then
                    Select Case sGuDaoStyle.Substring(0, 3)
                        Case "折返线"
                            sGuDaoNum = "折" & CADStaInf(nStaID).Track(nTrackID).sTrackNum
                        Case "正线线"
                            sGuDaoNum = CADStaInf(nStaID).Track(nTrackID).sTrackNum & "道"
                        Case "到发线"
                            sGuDaoNum = CADStaInf(nStaID).Track(nTrackID).sTrackNum & "道"
                        Case "存车线"
                            sGuDaoNum = "存" & CADStaInf(nStaID).Track(nTrackID).sTrackNum
                    End Select
                End If
            End If
        End If

        ' If X1 >= 0 And Y1 >= 0 Then
        curColor = GetControModelColor(sControlNum) ' GetControModelColor(CADStaInf(nStaID).Track(nTrackID).sControlNum)
        curPen = New Pen(curColor, LineWidth)
        Dim tmpFont As Font
        Dim tmpBrush As Brush
        nFontSize = 15 * sngScale
        If sTrackStyle <> "站名线" Then
            rBmpGraphics.DrawLine(curPen, X1, Y1, X2, Y2)
        Else
            If sngScale > 0.1 Then
                rBmpGraphics.DrawLine(New Pen(Color.Yellow, 1), X1, Y1, X2, Y2)
                If Y1 = Y2 Then
                    rBmpGraphics.DrawLine(New Pen(Color.Yellow, 1), X1 + (X2 - X1) / 2, Y1, X1 + (X2 - X1) / 2, Y2 + 30 * sngScale)
                End If
                If X1 = X2 Then
                    rBmpGraphics.DrawLine(New Pen(Color.Yellow, 1), X1, Y1 + (Y2 - Y1) / 2, X2 - 30 * sngScale, Y1 + (Y2 - Y1) / 2)
                End If

                rBmpGraphics.DrawLine(New Pen(Color.Yellow, 1), X1, Y1, X2, Y2)
                If nFontSize < 6 Then
                    nFontSize = 6
                End If
                tmpFont = New Font("黑体", nFontSize)
                tmpBrush = Brushes.Yellow
                Call WriteStrInLine(rBmpGraphics, CADStaInf(nStaID).sStaName, tmpFont, tmpBrush, X1, Y1, X2, Y2, 2)
            End If
        End If

        If IfShowOtherInf = True Then
            If sTrackStyle = "站台线" Or sTrackStyle = "站名线" Then
            Else
                If X1 = X2 Then
                    rBmpGraphics.DrawLine(New Pen(cForColor, 2), X1 - 5, Y1, X1 + 5, Y1)
                    rBmpGraphics.DrawLine(New Pen(cForColor, 2), X2 - 5, Y2, X2 + 5, Y2)
                Else
                    rBmpGraphics.DrawLine(New Pen(cForColor, 2), X1, Y1 - 5, X1, Y1 + 5)
                    rBmpGraphics.DrawLine(New Pen(cForColor, 2), X2, Y2 - 5, X2, Y2 + 5)
                End If
            End If
        End If
        Dim b As New SolidBrush(Color.YellowGreen)
        nFontSize = 10 * sngScale
        If nFontSize < 5 Then
            nFontSize = 5
        End If
        tmpFont = New Font("黑体", nFontSize)
        tmpBrush = Brushes.Red
        If IfPrintGuDaoNum = True Then
            '股道编号
            If sTrackStyle.Length >= 2 Then
                Call WriteStrInLine(rBmpGraphics, sGuDaoNum, tmpFont, tmpBrush, X1, Y1, X2, Y2, 2)
            End If
        End If
        tmpBrush = Brushes.DarkGray
        If IfPrintTrackNum = True And sTrackStyle <> "站台线" And sTrackStyle <> "站名线" Then
            If sTrackCurNum.Length > 0 Then
                Call WriteStrInLine(rBmpGraphics, sTrackCurNum, tmpFont, tmpBrush, X1, Y1, X2, Y2, 5)
            End If
        End If

        '显示道岔号
        tmpFont = New Font("黑体", nFontSize)
        tmpBrush = Brushes.YellowGreen
        If IfPrintCorssNum = True Then
            If sTrackStyle = "道岔线" And sCrossNum <> "" Then
                'If Math.Abs(Y1 - Y2) <= 2 Or Math.Abs(X2 - X1) <= 2 Then
                Call WriteStrInLine(rBmpGraphics, sCrossNum, tmpFont, tmpBrush, X1, Y1, X2, Y2, 2)
                'End If
            End If
        End If

    End Sub

    '打印缩略图，全线
    Public Sub PrintTrackInLinePic(ByVal rBmpGraphics As Graphics, ByVal LineWidth As Single, _
                                         ByVal sngScale As Single, ByVal leftX As Single, ByVal topY As Single, ByVal cForColor As Color, ByVal IfShowOtherInf As Boolean, ByVal IfPrintGuDaoNum As Boolean, ByVal IfPrintTrackNum As Boolean, ByVal IfPrintCorssNum As Boolean)
        Dim curColor As Color
        Dim curPen As Pen
        Dim nStaID As Integer
        Dim nTrackID As Integer
        Dim X1 As Single
        Dim Y1 As Single
        Dim X2 As Single
        Dim Y2 As Single
        Dim i As Integer
        ReDim tmpRunTrackInf(UBound(TrackInf))
        For i = 1 To UBound(TrackInf)
            '画线段
            X1 = (TrackInf(i).X1) * sngScale - leftX
            Y1 = (TrackInf(i).Y1) * sngScale - topY
            X2 = (TrackInf(i).X2) * sngScale - leftX
            Y2 = (TrackInf(i).Y2) * sngScale - topY
            nStaID = TrackInf(i).nStaID
            nTrackID = TrackInf(i).nTrackID
            curColor = GetControModelColor(CADStaInf(nStaID).Track(nTrackID).sControlNum)
            curPen = New Pen(curColor, LineWidth)
            Call DrawTrackLine(rBmpGraphics, nStaID, nTrackID, cForColor, LineWidth, sngScale, IfShowOtherInf, IfPrintGuDaoNum, IfPrintTrackNum, IfPrintCorssNum, X1, Y1, X2, Y2, TrackInf(i).sControlNum)
            'rBmpGraphics.DrawLine(curPen, X1, Y1, X2, Y2)
            tmpRunTrackInf(i).nStaID = TrackInf(i).nStaID
            tmpRunTrackInf(i).nTrackID = TrackInf(i).nTrackID
            tmpRunTrackInf(i).sControlNum = TrackInf(i).sControlNum
            tmpRunTrackInf(i).X1 = X1
            tmpRunTrackInf(i).Y1 = Y1
            tmpRunTrackInf(i).X2 = X2
            tmpRunTrackInf(i).Y2 = Y2
        Next
    End Sub

    '打印车站图，给定坐标范围
    Public Sub PrintTrackInStaPic(ByVal rBmpGraphics As Graphics, ByVal LineWidth As Single, _
                                         ByVal sngScale As Single, ByVal leftX As Single, ByVal topY As Single, _
                                         ByVal FixX1 As Single, ByVal FixX2 As Single, ByVal FixY1 As Single, ByVal FixY2 As Single, _
                                         ByVal picHeight As Single, ByVal picWidth As Single, _
                                         ByVal cForColor As Color, ByVal IfShowOtherInf As Boolean, _
                                         ByVal IfPrintGuDaoNum As Boolean, ByVal IfPrintTrackNum As Boolean, _
                                         ByVal IfPrintCorssNum As Boolean)
        Dim curColor As Color
        Dim curPen As Pen
        Dim nStaID As Integer
        Dim nTrackID As Integer
        Dim X1 As Single
        Dim Y1 As Single
        Dim X2 As Single
        Dim Y2 As Single
        Dim i As Integer
        Dim ifCal As Integer
        ReDim tmpRunStaTrackInf(0)
        For i = 1 To UBound(tmpRunTrackInf)
            '画线段
            X1 = tmpRunTrackInf(i).X1
            Y1 = tmpRunTrackInf(i).Y1
            X2 = tmpRunTrackInf(i).X2
            Y2 = tmpRunTrackInf(i).Y2
            ifCal = 0
            If (X1 >= FixX1 And X1 <= FixX2) And (Y1 >= FixY1 And Y1 <= FixY2) Then
                ifCal = 1
            ElseIf (X2 >= FixX1 And X2 <= FixX2) And (Y2 >= FixY1 And Y2 <= FixY2) Then
                ifCal = 1
            End If
            If ifCal = 1 Then
                X1 = X1 / sngScale - leftX
                X2 = X2 / sngScale - leftX
                Y1 = Y1 / sngScale - topY
                Y2 = Y2 / sngScale - topY
                ReDim Preserve tmpRunStaTrackInf(UBound(tmpRunStaTrackInf) + 1)
                tmpRunStaTrackInf(UBound(tmpRunStaTrackInf)).X1 = X1
                tmpRunStaTrackInf(UBound(tmpRunStaTrackInf)).Y1 = Y1
                tmpRunStaTrackInf(UBound(tmpRunStaTrackInf)).X2 = X2
                tmpRunStaTrackInf(UBound(tmpRunStaTrackInf)).Y2 = Y2
                tmpRunStaTrackInf(UBound(tmpRunStaTrackInf)).nStaID = tmpRunTrackInf(i).nStaID
                tmpRunStaTrackInf(UBound(tmpRunStaTrackInf)).nTrackID = tmpRunTrackInf(i).nTrackID
                tmpRunStaTrackInf(UBound(tmpRunStaTrackInf)).sControlNum = tmpRunTrackInf(i).sControlNum
            End If
        Next
        ' (Y1 >= FixY1 And Y1 <= FixY2 And Y2 >= FixY1 And Y2 <= FixY2) Then

        Dim nWidthScale As Single
        Dim nheightScale As Single
        Dim minX As Single
        Dim maxX As Single
        Dim minY As Single
        Dim maxY As Single
        Dim LineLeftX As Single
        Dim LineTopY As Single

        minX = 1000000
        maxX = -10000000
        For i = 1 To UBound(tmpRunStaTrackInf)
            minX = Minimal(minX, tmpRunStaTrackInf(i).X1)
            minX = Minimal(minX, tmpRunStaTrackInf(i).X2)
            maxX = Maximal(maxX, tmpRunStaTrackInf(i).X1)
            maxX = Maximal(maxX, tmpRunStaTrackInf(i).X2)
            minY = Minimal(minY, tmpRunStaTrackInf(i).Y1)
            minY = Minimal(minY, tmpRunStaTrackInf(i).Y2)
            maxY = Maximal(maxY, tmpRunStaTrackInf(i).Y1)
            maxY = Maximal(maxY, tmpRunStaTrackInf(i).Y2)
        Next
        If (maxX - minX) > 0 Then
            nWidthScale = (picWidth / (maxX - minX)) * 1
        End If
        If (maxY - minY) > 0 Then
            nheightScale = (picHeight / (maxY - minY)) * 1
        End If
        Dim nLineScale As Single
        nLineScale = Minimal(nWidthScale, nheightScale)
        LineLeftX = (picWidth - (maxX - minX) * nLineScale) / 2 - minX * nLineScale
        LineTopY = (picHeight - (maxY - minY) * nLineScale) / 2 - minY * nLineScale
        For i = 1 To UBound(tmpRunStaTrackInf)
            X1 = tmpRunStaTrackInf(i).X1 * nLineScale + LineLeftX
            Y1 = tmpRunStaTrackInf(i).Y1 * nLineScale + LineTopY
            X2 = tmpRunStaTrackInf(i).X2 * nLineScale + LineLeftX
            Y2 = tmpRunStaTrackInf(i).Y2 * nLineScale + LineTopY
            tmpRunStaTrackInf(i).X1 = X1
            tmpRunStaTrackInf(i).Y1 = Y1
            tmpRunStaTrackInf(i).X2 = X2
            tmpRunStaTrackInf(i).Y2 = Y2
            nStaID = tmpRunStaTrackInf(i).nStaID
            nTrackID = tmpRunStaTrackInf(i).nTrackID
            curColor = GetControModelColor(CADStaInf(nStaID).Track(nTrackID).sControlNum)
            curPen = New Pen(curColor, LineWidth)
            Call DrawTrackLine(rBmpGraphics, nStaID, nTrackID, cForColor, LineWidth, nLineScale, IfShowOtherInf, IfPrintGuDaoNum, IfPrintTrackNum, IfPrintCorssNum, X1, Y1, X2, Y2, tmpRunStaTrackInf(i).sControlNum)
        Next


    End Sub
    '显示信号机
    Public Sub PrintSignalByStyle(ByVal sStyle As String, ByVal X1 As Single, ByVal Y1 As Single, ByVal rBmpGraphics As Graphics, ByVal CForColor As Color, ByVal sngScale As Single)
        Dim nHeight As Single
        Dim nCircle As Single
        Dim nShort As Single
        Dim nWidth As Single
        nWidth = 2 * sngScale
        nHeight = 4 * sngScale
        nShort = 4 * sngScale
        nCircle = 4 * sngScale
        Select Case sStyle
            Case "上行单显示"
                rBmpGraphics.DrawLine(New Pen(CForColor, nWidth), X1, Y1 - nHeight, X1, Y1 + nHeight)
                rBmpGraphics.DrawEllipse(New Pen(CForColor, nWidth), X1 + nShort, Y1 - nHeight, nCircle * 2, nCircle * 2)
                rBmpGraphics.DrawLine(New Pen(CForColor, nWidth), X1, Y1, X1 + nShort, Y1)
            Case "上行双显示"
                rBmpGraphics.DrawLine(New Pen(CForColor, nWidth), X1, Y1 - nHeight, X1, Y1 + nHeight)
                rBmpGraphics.DrawEllipse(New Pen(CForColor, nWidth), X1 + nShort, Y1 - nHeight, nCircle * 2, nCircle * 2)
                rBmpGraphics.DrawEllipse(New Pen(CForColor, nWidth), X1 + nShort + nCircle * 2, Y1 - nHeight, nCircle * 2, nCircle * 2)
                rBmpGraphics.DrawLine(New Pen(CForColor, nWidth), X1, Y1, X1 + nShort, Y1)
            Case "上行三显示"
                rBmpGraphics.DrawLine(New Pen(CForColor, nWidth), X1, Y1 - nHeight, X1, Y1 + nHeight)
                rBmpGraphics.DrawEllipse(New Pen(CForColor, nWidth), X1 + nShort, Y1 - nHeight, nCircle * 2, nCircle * 2)
                rBmpGraphics.DrawEllipse(New Pen(CForColor, nWidth), X1 + nShort + nCircle * 2, Y1 - nHeight, nCircle * 2, nCircle * 2)
                rBmpGraphics.DrawEllipse(New Pen(CForColor, nWidth), X1 + nShort + nCircle * 2 * 2, Y1 - nHeight, nCircle * 2, nCircle * 2)
                rBmpGraphics.DrawLine(New Pen(CForColor, nWidth), X1, Y1, X1 + nShort, Y1)
            Case "下行单显示"
                rBmpGraphics.DrawLine(New Pen(CForColor, nWidth), X1, Y1 - nHeight, X1, Y1 + nHeight)
                rBmpGraphics.DrawEllipse(New Pen(CForColor, nWidth), X1 - nShort - nCircle * 2, Y1 - nHeight, nCircle * 2, nCircle * 2)
                rBmpGraphics.DrawLine(New Pen(CForColor, nWidth), X1, Y1, X1 - nShort, Y1)

            Case "下行双显示"
                rBmpGraphics.DrawLine(New Pen(CForColor, nWidth), X1, Y1 - nHeight, X1, Y1 + nHeight)
                rBmpGraphics.DrawEllipse(New Pen(CForColor, nWidth), X1 - nShort - nCircle * 2, Y1 - nHeight, nCircle * 2, nCircle * 2)
                rBmpGraphics.DrawEllipse(New Pen(CForColor, nWidth), X1 - nShort - nCircle * 2 * 2, Y1 - nHeight, nCircle * 2, nCircle * 2)
                rBmpGraphics.DrawLine(New Pen(CForColor, nWidth), X1, Y1, X1 - nShort, Y1)

            Case "下行三显示"
                rBmpGraphics.DrawLine(New Pen(CForColor, nWidth), X1, Y1 - nHeight, X1, Y1 + nHeight)
                rBmpGraphics.DrawEllipse(New Pen(CForColor, nWidth), X1 - nShort - nCircle * 2, Y1 - nHeight, nCircle * 2, nCircle * 2)
                rBmpGraphics.DrawEllipse(New Pen(CForColor, nWidth), X1 - nShort - nCircle * 2 * 2, Y1 - nHeight, nCircle * 2, nCircle * 2)
                rBmpGraphics.DrawEllipse(New Pen(CForColor, nWidth), X1 - nShort - nCircle * 2 * 3, Y1 - nHeight, nCircle * 2, nCircle * 2)
                rBmpGraphics.DrawLine(New Pen(CForColor, nWidth), X1, Y1, X1 - nShort, Y1)

            Case Else
                rBmpGraphics.DrawLine(New Pen(CForColor, nWidth), X1, Y1 - nHeight, X1, Y1 + nHeight)
                rBmpGraphics.DrawEllipse(New Pen(CForColor, nWidth), X1 + nShort, Y1 - nHeight, nCircle * 2, nCircle * 2)
                rBmpGraphics.DrawLine(New Pen(CForColor, nWidth), X1, Y1, X1 + nShort, Y1)
        End Select
    End Sub


    '查找连接的TRACKID
    Public Sub SeekLinkTrackNum(ByVal sCurValue As String, ByVal X As Single, ByVal Y As Single, ByVal X1 As Single, ByVal Y1 As Single, ByVal sLeftOrRight As String)
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        ReDim GetReturnValue(0)
        Dim nExist As Integer
        Dim nLength As Single
        Dim nLength1 As Single
        Dim Alf As Single
        nExist = 0
        For i = 1 To UBound(CADStaInf)
            For j = 1 To UBound(CADStaInf(i).Track)
                '  If sLeftOrRight = "左连接" Then
                If CADStaInf(i).Track(j).sTrackCircuitNum <> sCurValue Then
                    nLength1 = GetTwoPointLength(X, Y, CADStaInf(i).Track(j).X1, CADStaInf(i).Track(j).Y1)
                    If nLength1 <= 2 Then
                        Alf = CosAngle(X, Y, X1, Y1, CADStaInf(i).Track(j).X2, CADStaInf(i).Track(j).Y2)
                        If Alf >= 90 Then
                            nExist = 0
                            For k = 1 To UBound(GetReturnValue)
                                If GetReturnValue(k) = CADStaInf(i).Track(j).sTrackCircuitNum Then
                                    nExist = 1
                                End If
                            Next
                            If nExist = 0 Then
                                ReDim Preserve GetReturnValue(UBound(GetReturnValue) + 1)
                                GetReturnValue(UBound(GetReturnValue)) = CADStaInf(i).Track(j).sTrackCircuitNum
                            End If
                        End If
                    End If

                    nLength = GetTwoPointLength(X, Y, CADStaInf(i).Track(j).X2, CADStaInf(i).Track(j).Y2)
                    If nLength <= 2 Then
                        Alf = CosAngle(X, Y, X1, Y1, CADStaInf(i).Track(j).X1, CADStaInf(i).Track(j).Y1)
                        If Alf >= 90 Then
                            nExist = 0
                            For k = 1 To UBound(GetReturnValue)
                                If GetReturnValue(k) = CADStaInf(i).Track(j).sTrackCircuitNum Then
                                    nExist = 1
                                End If
                            Next
                            If nExist = 0 Then
                                ReDim Preserve GetReturnValue(UBound(GetReturnValue) + 1)
                                GetReturnValue(UBound(GetReturnValue)) = CADStaInf(i).Track(j).sTrackCircuitNum
                            End If
                        End If
                    End If
                End If
                'End If
                '    If sLeftOrRight = "右连接" Then
                '        If CADStaInf(i).Track(j).sTrackCircuitNum <> sCurValue Then
                '            nLength = GetTwoPointLength(X, Y, CADStaInf(i).Track(j).X1, CADStaInf(i).Track(j).Y1)
                '            'nLength1 = GetTwoPointLength(X, Y, CADStaInf(i).Track(j).X2, CADStaInf(i).Track(j).Y2)

                '            If nLength <= 2 Then 'Or nLength1 <= 2 Then
                '                For k = 1 To UBound(GetReturnValue)
                '                    If GetReturnValue(k) = CADStaInf(i).Track(j).sTrackCircuitNum Then
                '                        nExist = 1
                '                    End If
                '                Next
                '                If nExist = 0 Then
                '                    ReDim Preserve GetReturnValue(UBound(GetReturnValue) + 1)
                '                    GetReturnValue(UBound(GetReturnValue)) = CADStaInf(i).Track(j).sTrackCircuitNum
                '                End If
                '            End If
                '        End If
                '    End If
            Next
        Next
    End Sub

    '画车站图
    Public Sub DrawStaPicFormStaID(ByVal nStaID As Integer, ByVal sngBili As Single, ByVal cForColor As Color, ByVal TrackLineWidth As Single, ByVal PlatFormLineWidth As Single, ByVal rBmpGraphics As Graphics, _
                                                ByVal sngMinX As Single, ByVal sngMinY As Single, ByVal sngLeftX As Single, ByVal sngTopY As Single, ByVal IfPrintGuDaoNum As Boolean, _
                                                ByVal IfPrintTrackNum As Boolean, ByVal IfPrintCorssNum As Boolean, ByVal ifPrintShotLine As Boolean)
        Dim X1 As Single
        Dim Y1 As Single
        Dim X2 As Single
        Dim Y2 As Single
        'Dim ForX As Single
        'Dim ForY As Single
        Dim i As Integer
        'Dim Len1 As Single
        'Dim Len2 As Single

        '画线段()
        For i = 1 To UBound(CADStaInf(nStaID).Track)
            If CADStaInf(nStaID).Track(i).nDelete = 0 Then
                X1 = (CADStaInf(nStaID).Track(i).X1 - sngMinX) * sngBili + sngLeftX
                Y1 = (CADStaInf(nStaID).Track(i).Y1 - sngMinY) * sngBili + sngTopY
                X2 = (CADStaInf(nStaID).Track(i).X2 - sngMinX) * sngBili + sngLeftX
                Y2 = (CADStaInf(nStaID).Track(i).Y2 - sngMinY) * sngBili + sngTopY
                Call DrawTrackLine(rBmpGraphics, nStaID, i, cForColor, TrackLineWidth, 1, ifPrintShotLine, IfPrintGuDaoNum, IfPrintTrackNum, IfPrintCorssNum, X1, Y1, X2, Y2, CADStaInf(nStaID).Track(i).sControlNum)
                '打印连接车站信息
                Dim sStyle As String
                Dim sStyle1 As String
                Dim sStaName As String
                Dim sPrintSta As String
                Dim tmpFont As Font
                Dim tmpBrush As Brush

                tmpFont = New Font("黑体", 10)
                tmpBrush = Brushes.SlateGray
                sStyle = GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink1)
                sStyle1 = GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink1)
                If sStyle <> CADStaInf(nStaID).sStaCode Then
                    sStaName = GetStaNameFromStaCode(sStyle)
                    If CADStaInf(nStaID).sStaOrSec = "车站" Then
                        sPrintSta = GetSectionPrintStaName(sStaName, CADStaInf(nStaID).sStaName)
                    Else
                        sPrintSta = sStaName
                    End If
                    Call WriteStrInLine(rBmpGraphics, sPrintSta, tmpFont, tmpBrush, X1, Y1, X2, Y2, 7)
                End If
                If sStyle1 <> CADStaInf(nStaID).sStaCode Then
                    sStaName = GetStaNameFromStaCode(sStyle1)
                    If CADStaInf(nStaID).sStaOrSec = "车站" Then
                        sPrintSta = GetSectionPrintStaName(sStaName, CADStaInf(nStaID).sStaName)
                    Else
                        sPrintSta = sStaName
                    End If
                    Call WriteStrInLine(rBmpGraphics, sPrintSta, tmpFont, tmpBrush, X1, Y1, X2, Y2, 8)
                End If
            End If
            'Call PrintTrackInPic(rBmpGraphics, cForColor, TrackLineWidth, sngLeftX, sngTopY, , , , 1, ifPrintShotLine, IfPrintGuDaoNum, IfPrintTrackNum, IfPrintCorssNum)
        Next i

        ''画信号机
        'For i = 1 To UBound(CADStaInf(nStaID).Signal)
        '    X1 = CADStaInf(nStaID).Signal(i).X - sngLeftX
        '    Y1 = CADStaInf(nStaID).Signal(i).Y - sngTopY

        '    If X1 > 0 And Y1 > 0 Then
        '        Call PrintSignalByStyle(CADStaInf(nStaID).Signal(i).sStyle, X1, Y1, rBmpGraphics, cForColor, 1)
        '    End If
        'Next i

        ''画站台
        'For i = 1 To UBound(CADStaInf(nStaID).PlatForm)
        '    ForX = CADStaInf(nStaID).PlatForm(i).X1 - sngLeftX
        '    ForY = CADStaInf(nStaID).PlatForm(i).Y1 - sngTopY
        '    X1 = CADStaInf(nStaID).PlatForm(i).X2 - sngLeftX
        '    Y1 = CADStaInf(nStaID).PlatForm(i).Y2 - sngTopY
        '    Len1 = X1 - ForX
        '    Len2 = Y1 - ForY
        '    If ForX > 0 And ForY > 0 Then
        '        rBmpGraphics.DrawRectangle(New Pen(cForColor, PlatFormLineWidth), ForX, ForY, Len1, Len2)
        '    End If
        'Next i

        ''打印文字
        'For i = 1 To UBound(CADStaInf(nStaID).FontText)
        '    X1 = CADStaInf(nStaID).FontText(i).X - sngLeftX
        '    Y1 = CADStaInf(nStaID).FontText(i).Y - sngTopY
        '    Dim sText As String
        '    Dim FontName As String
        '    Dim FontSize As Single
        '    Dim FontColor As Color
        '    sText = CADStaInf(nStaID).FontText(i).sText
        '    FontSize = CADStaInf(nStaID).FontText(i).FontSize
        '    FontName = CADStaInf(nStaID).FontText(i).FontName
        '    FontColor = CADStaInf(nStaID).FontText(i).FontColor
        '    Dim s As New FontStyle
        '    If CADStaInf(nStaID).FontText(i).Italic = True Then
        '        s = CADStaInf(nStaID).FontText(i).Italic
        '    End If
        '    If CADStaInf(nStaID).FontText(i).Bold = True Then
        '        s = CADStaInf(nStaID).FontText(i).Bold
        '    End If
        '    Dim b As New SolidBrush(FontColor)
        '    Dim f As New Font(FontName, FontSize, s)
        '    If X1 > 0 And Y1 > 0 Then
        '        rBmpGraphics.DrawString(sText, f, b, X1, Y1)
        '    End If
        'Next i
    End Sub

    '刷新底图
    Public Sub RefreshPicSta(ByVal LeftX As Single, ByVal TopY As Single, ByVal PicSta As PictureBox, ByVal picLine As PictureBox, ByVal nSclale As Single, ByVal bIfShowGrid As Integer, ByVal IfShowOtherInf As Boolean, ByVal IfPrintGuDaoNum As Boolean, _
                                         ByVal IfPrintTrackNum As Boolean, ByVal IfPrintCorssNum As Boolean)
        Dim rBmp As Bitmap '画图临时保存的图像
        Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
        rBmp = New Bitmap(PicSta.Width, PicSta.Height)
        rBmpGraphics = Graphics.FromImage(rBmp)
        Call PrintStationPicture(PicSta, rBmpGraphics, LeftX, PicSta.Width * (1 / nSclale), TopY, PicSta.Height * (1 / nSclale), nSclale, bIfShowGrid, IfShowOtherInf, IfPrintGuDaoNum, IfPrintTrackNum, IfPrintCorssNum)
        PicSta.Image = rBmp
        Call DrawWholeLinePicture(True, picLine)

    End Sub
    '画线路图
    Public Sub DrawLinePic(ByVal sngScale As Single, ByVal picLine As PictureBox, ByVal tmpPen As Pen)
        Dim X1 As Single
        Dim Y1 As Single
        Dim ForX As Single
        Dim ForY As Single
        Dim lngStaX As Single
        Dim lngStaY As Single
        Dim i As Integer

        lngStaX = 0
        lngStaY = 0
        Dim rBmp As Bitmap '画图临时保存的图像
        Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
        ForX = 0
        ForY = 0

        picLine.Refresh()
        rBmp = New Bitmap(picLine.Width, picLine.Height)
        rBmpGraphics = Graphics.FromImage(rBmp)
        Dim nShortLineWidth As Single
        Dim nLineWidth As Single
        nLineWidth = 3 * sngScale
        nShortLineWidth = 5 * sngScale
        Dim k As Integer
        Dim curPen As Pen
        Dim curColor As Color
        For k = 1 To UBound(CADStaInf)
            '画线段
            For i = 1 To UBound(CADStaInf(k).Track)
                ForX = CADStaInf(k).Track(i).X1 * sngScale
                ForY = CADStaInf(k).Track(i).Y1 * sngScale
                X1 = CADStaInf(k).Track(i).X2 * sngScale
                Y1 = CADStaInf(k).Track(i).Y2 * sngScale

                If ForX > 0 And ForY > 0 Then
                    curColor = GetControModelColor(CADStaInf(k).Track(i).sControlNum)
                    curPen = New Pen(curColor, 2)
                    rBmpGraphics.DrawLine(curPen, ForX, ForY, X1, Y1)

                    'rBmpGraphics.DrawLine(tmpPen, ForX, ForY, X1, Y1)

                    'If Val(Asc(CADStaInf(k).Track(i).sControlNum)) Mod 2 = 0 Then
                    '    rBmpGraphics.DrawLine(tmpPen, ForX, ForY, X1, Y1)
                    'Else
                    '    rBmpGraphics.DrawLine(New Pen(Color.Yellow, 2), ForX, ForY, X1, Y1)
                    'End If

                    'If ForX = X1 Then
                    '    rBmpGraphics.DrawLine(New Pen(Color.White, 2), ForX - nShortLineWidth, ForY, ForX + nShortLineWidth, ForY)
                    '    rBmpGraphics.DrawLine(New Pen(Color.White, 2), X1 - nShortLineWidth, Y1, X1 + nShortLineWidth, Y1)
                    'Else
                    '    rBmpGraphics.DrawLine(New Pen(Color.White, 2), ForX, ForY - nShortLineWidth, ForX, ForY + nShortLineWidth)
                    '    rBmpGraphics.DrawLine(New Pen(Color.White, 2), X1, Y1 - nShortLineWidth, X1, Y1 + nShortLineWidth)
                    'End If
                End If

            Next i

            '画信号机
            For i = 1 To UBound(CADStaInf(k).Signal)
                X1 = CADStaInf(k).Signal(i).X * sngScale
                Y1 = CADStaInf(k).Signal(i).Y * sngScale

                If X1 > 0 And Y1 > 0 Then
                    Call PrintSignalByStyle(CADStaInf(k).Signal(i).sStyle, X1, Y1, rBmpGraphics, Color.White, sngScale)
                End If
            Next i

            '画站台
            For i = 1 To UBound(CADStaInf(k).PlatForm)
                ForX = CADStaInf(k).PlatForm(i).X1 * sngScale
                ForY = CADStaInf(k).PlatForm(i).Y1 * sngScale
                X1 = CADStaInf(k).PlatForm(i).X2 * sngScale
                Y1 = CADStaInf(k).PlatForm(i).Y2 * sngScale

                If ForX > 0 And ForY > 0 Then
                    rBmpGraphics.DrawRectangle(tmpPen, ForX, ForY, X1 - ForX, Y1 - ForY)
                End If
            Next i

            '打印文字
            For i = 1 To UBound(CADStaInf(k).FontText)
                X1 = CADStaInf(k).FontText(i).X * sngScale
                Y1 = CADStaInf(k).FontText(i).Y * sngScale
                Dim sText As String
                Dim FontName As String
                Dim FontSize As Single
                Dim FontColor As Color
                sText = CADStaInf(k).FontText(i).sText
                FontSize = CADStaInf(k).FontText(i).FontSize * sngScale
                FontName = CADStaInf(k).FontText(i).FontName
                FontColor = CADStaInf(k).FontText(i).FontColor
                Dim s As New FontStyle
                If CADStaInf(k).FontText(i).Italic = True Then
                    s = CADStaInf(k).FontText(i).Italic
                End If
                If CADStaInf(k).FontText(i).Bold = True Then
                    s = CADStaInf(k).FontText(i).Bold
                End If
                Dim b As New SolidBrush(FontColor)
                Dim f As New Font(FontName, FontSize, s)
                If X1 > 0 And Y1 > 0 Then
                    rBmpGraphics.DrawString(sText, f, b, X1, Y1)
                End If
            Next i
        Next k
        picLine.Image = rBmp
    End Sub

    '读入控制模块信息
    Public Sub InputControModelData()
        ReDim ControlModel(0)
        'Dim myWS As dao.Workspace
        'Dim DE As dao.DBEngine = New dao.DBEngine
        'myWS = DE.Workspaces(0)

        'Dim dbData As dao.Database
        'Dim RSdata1 As dao.Recordset
        'Dim i As Integer
        'Dim nNum As Integer

        ' If SystemPara.SystemStyle = "磁浮" Then
        'dbData = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        'RSdata1 = dbData.OpenRecordset("select * from 控制模块信息表 order by 模块名称")
        'If RSdata1.RecordCount > 0 Then
        '    RSdata1.MoveLast()
        '    nNum = RSdata1.RecordCount
        'End If
        'If nNum > 0 Then
        '    RSdata1.MoveFirst()
        '    ReDim ControlModel(nNum)
        '    For i = 1 To nNum
        '        ControlModel(i).sModelName = RSdata1.Fields("模块名称").Value.ToString.Trim
        '        ControlModel(i).sColor = System.Drawing.ColorTranslator.FromHtml(RSdata1.Fields("显示颜色").Value.ToString.Trim)
        '        RSdata1.MoveNext()
        '    Next
        '    myWS.Close()
        'End If
    End Sub

    '根据模块名称得到显示的颜色
    Public Function GetControModelColor(ByVal sModelName As String) As Color
        'Dim i As Integer
        GetControModelColor = Color.DarkGray
        'For i = 1 To UBound(ControlModel)
        '    If ControlModel(i).sModelName = sModelName Then
        '        GetControModelColor = ControlModel(i).sColor
        '    End If
        'Next
    End Function

    '旋转角度
    Public Function GetSngAngle(ByVal Xa As Single, ByVal Ya As Single, ByVal X1 As Single, ByVal Y1 As Single, ByVal X2 As Single, ByVal Y2 As Single) As Single
        'Dim K1 As Single
        'Dim K2 As Single
        'Dim Tg As Single
        'If X1 = X2 And Y1 = Y2 Then
        '    GetSngAngle = 0
        '    Exit Function
        'End If
        'K1 = (Y1 - Ya) / (X1 - Xa + 0.001)
        'K2 = (Y2 - Ya) / (X2 - Xa + 0.001)
        'If (1 + K1 * K2) <> 0 Then
        '    Tg = (K2 - K1) / (1 + K1 * K2)
        'End If
        'GetSngAngle = Math.Atan(Tg)
        Dim XcordX As Single
        Dim XcordY As Single
        XcordX = Xa + 100
        XcordY = Ya
        Dim Alf1 As Single
        Dim Alf2 As Single
        Alf1 = GetCosAlf(Xa, Ya, XcordX, XcordY, X1, Y1)
        Alf2 = GetCosAlf(Xa, Ya, XcordX, XcordY, X2, Y2)
        GetSngAngle = 2 * Math.PI - (Alf2 - Alf1)
    End Function

    '求CorsAlf 
    Public Function GetCosAlf(ByVal Xa As Single, ByVal Ya As Single, ByVal X1 As Single, ByVal Y1 As Single, ByVal X2 As Single, ByVal Y2 As Single) As Single
        Dim cosAlf As Single
        Dim tmpX1, tmpX2, tmpY1, tmpY2 As Single
        Dim a, b As Single
        tmpX1 = X1 - Xa
        tmpX2 = X2 - Xa
        tmpY1 = Y1 - Ya
        tmpY2 = Y2 - Ya
        cosAlf = (tmpX1 * tmpX2 + tmpY1 * tmpY2) / (Math.Sqrt(tmpX1 ^ 2 + tmpY1 ^ 2) * Math.Sqrt(tmpX2 ^ 2 + tmpY2 ^ 2))
        GetCosAlf = Math.Acos(cosAlf)
        If X2 = Xa Then
            If Y2 - Ya > 0 Then
                a = 1
            Else
                a = -1
            End If
        Else
            a = (X2 - Xa) / Math.Abs(X2 - Xa)
        End If
        If Y2 = Ya Then
            If X2 - Xa > 0 Then
                b = 1
            Else
                b = -1
            End If
        Else
            b = (Y2 - Ya) / Math.Abs(Y2 - Ya)
        End If

        If a = 1 And b = -1 Then '第一像限
            GetCosAlf = Math.Abs(GetCosAlf)
        ElseIf a = -1 And b = -1 Then '第二像限
            GetCosAlf = Math.Abs(GetCosAlf)
        ElseIf a = -1 And b = 1 Then '第三像限
            GetCosAlf = -GetCosAlf + 2 * Math.PI
        Else
            GetCosAlf = -GetCosAlf + 2 * Math.PI
        End If
    End Function

    '保存CADSTAinf中的信息到数据库中
    Public Sub InputCADstaInfToDataBase()
        Dim i, j As Integer
        Dim sCurName As String
        Dim nID As Integer

        Call RefreshCADControModeInf()

        Dim myWS As dao.Workspace
        Dim DE As dao.DBEngine = New dao.DBEngine
        myWS = DE.Workspaces(0)
        Dim dFile As dao.Database
        Dim sFile As dao.Recordset
        dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        sCurName = "线段信息表"
        dFile.Execute("delete * from " & sCurName & "")

        '  dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        dFile.TableDefs.Refresh()
        dFile.Recordsets.Refresh()
        nID = 1
        sFile = dFile.OpenRecordset(sCurName)
        'Dim X1, Y1, X2, Y2 As Integer
        For i = 1 To UBound(CADStaInf)
            For j = 1 To UBound(CADStaInf(i).Track)
                If CADStaInf(i).Track(j).nDelete = 0 Then
                    sFile.AddNew()
                    sFile.Fields("车站名称").Value = CADStaInf(i).sStaName
                    sFile.Fields("线段编号").Value = CADStaInf(i).Track(j).nNum
                    sFile.Fields("线段类型").Value = CADStaInf(i).Track(j).sStyle
                    sFile.Fields("线段长度").Value = CADStaInf(i).Track(j).sngLength
                    sFile.Fields("股道类型").Value = CADStaInf(i).Track(j).sGuDaoStyle
                    sFile.Fields("股道用途").Value = CADStaInf(i).Track(j).sGuDaoYongTu
                    sFile.Fields("股道使用顺序").Value = CADStaInf(i).Track(j).sGuDaoUseSeq
                    sFile.Fields("股道或道岔编号").Value = CADStaInf(i).Track(j).sTrackNum
                    sFile.Fields("X1坐标").Value = CADStaInf(i).Track(j).X1
                    sFile.Fields("Y1坐标").Value = CADStaInf(i).Track(j).Y1
                    sFile.Fields("X2坐标").Value = CADStaInf(i).Track(j).X2
                    sFile.Fields("Y2坐标").Value = CADStaInf(i).Track(j).Y2
                    'If CADStaInf(i).Track(j).Y2 - CADStaInf(i).Track(j).Y1 >= 0 And CADStaInf(i).Track(j).X2 - CADStaInf(i).Track(j).X1 >= 0 Then '第二点在右下角

                    'Else

                    'End If
                    sFile.Fields("左1连接").Value = CADStaInf(i).Track(j).sLeftLink1
                    sFile.Fields("左2连接").Value = CADStaInf(i).Track(j).sLeftLink2
                    sFile.Fields("左3连接").Value = CADStaInf(i).Track(j).sLeftLink3
                    sFile.Fields("右1连接").Value = CADStaInf(i).Track(j).sRightLink1
                    sFile.Fields("右2连接").Value = CADStaInf(i).Track(j).sRightLink2
                    sFile.Fields("右3连接").Value = CADStaInf(i).Track(j).sRightLink3
                    sFile.Fields("轨道电路编号").Value = CADStaInf(i).Track(j).sTrackCircuitNum
                    sFile.Fields("控制模块").Value = CADStaInf(i).Track(j).sControlNum
                    sFile.Fields("备注").Value = CADStaInf(i).Track(j).sMemo
                    sFile.Update()
                    nID = nID + 1
                End If
            Next
        Next

        'If SystemPara.SystemStyle = "磁浮" Then
        '    '保存控制模块信息
        '    Dim sFile2 As dao.Recordset
        '    Dim k As Integer
        '    Dim p As Integer
        '    Dim ModelName As String
        '    Dim TrackNum As String
        '    sFile2 = dFile.OpenRecordset("车站控制方式表")
        '    Dim nNum2 As Integer
        '    nNum2 = 0
        '    If sFile2.RecordCount > 0 Then
        '        sFile2.MoveLast()
        '        nNum2 = sFile2.RecordCount
        '    End If

        '    '待调试
        '    If nNum2 > 0 Then
        '        sFile2.MoveFirst()
        '        For i = 1 To nNum2
        '            For j = 1 To UBound(CADStaInf)
        '                For k = 1 To UBound(CADStaInf(j).ContolScheme)
        '                    If sFile2("车站名称").Value = CADStaInf(j).sStaName And sFile2("控制方式").Value = CADStaInf(j).ContolScheme(k).sSchemeName Then
        '                        ModelName = ""
        '                        TrackNum = ""
        '                        For p = 1 To UBound(CADStaInf(j).ContolScheme(k).SModelName)
        '                            If CADStaInf(j).ContolScheme(k).SModelName(p).Trim = "" Or CADStaInf(j).ContolScheme(k).STrackNum(p).Trim = "" _
        '                                Or CADStaInf(j).ContolScheme(k).SModelName(p).Trim = "无" Or CADStaInf(j).ContolScheme(k).STrackNum(p).Trim = "无" Then

        '                            Else
        '                                ModelName = ModelName & "," & CADStaInf(j).ContolScheme(k).SModelName(p)
        '                                TrackNum = TrackNum & "," & CADStaInf(j).ContolScheme(k).STrackNum(p)
        '                            End If
        '                        Next
        '                        sFile2.Edit()
        '                        sFile2("控制模块").Value = ModelName
        '                        sFile2("控制轨道").Value = TrackNum
        '                        If sFile2("控制方式").Value = CADStaInf(j).sCurControlScheme Then
        '                            sFile2("是否默认").Value = "是"
        '                        Else
        '                            sFile2("是否默认").Value = "否"
        '                        End If
        '                        sFile2.Update()
        '                        Exit For
        '                    End If
        '                Next
        '            Next
        '            sFile2.MoveNext()
        '        Next
        '    End If
        'End If
    End Sub

    '由车站代码得到车站名称
    Public Function GetStaNameFromStaCode(ByVal sStaCode As String) As String
        Dim i As Integer
        GetStaNameFromStaCode = ""
        For i = 1 To UBound(CADStaInf)
            If CADStaInf(i).sStaCode = sStaCode Then
                GetStaNameFromStaCode = CADStaInf(i).sStaName
                Exit For
            End If
        Next
    End Function

    '由区间名称得到另一车站的名称
    Public Function GetSectionPrintStaName(ByVal sSecName As String, ByVal sCurStaName As String) As String
        Dim i As Integer
        Dim sFirName As String
        Dim sAnoName As String
        sFirName = ""
        sAnoName = ""
        For i = 1 To sSecName.Length
            If sSecName.Substring(i - 1, 2) = "->" Then
                sFirName = sSecName.Substring(0, i - 1)
                sAnoName = sSecName.Substring(i + 1)
                Exit For
            End If
        Next
        If sFirName = sCurStaName Then
            GetSectionPrintStaName = sAnoName
        Else
            GetSectionPrintStaName = sFirName
        End If
    End Function
    ''得到默认的控制模块号
    'Public Sub RefreshDefaultControlNum(ByVal nStaId As Integer, ByVal nTrackID As Integer)
    '    Dim i As Integer
    '    Dim j As Integer
    '    For i = 1 To UBound(CADStaInf(nStaId).ContolScheme)
    '        If CADStaInf(nStaId).ContolScheme(i).sSchemeName = CADStaInf(i).sCurControlScheme Then
    '            For j = 1 To UBound(CADStaInf(nStaId).ContolScheme(i).STrackNum)
    '                If CADStaInf(nStaId).ContolScheme(i).STrackNum(j) = CADStaInf(nStaId).Track(nTrackID).sTrackCircuitNum Then
    '                    CADStaInf(nStaId).Track(nTrackID).sControlNum = CADStaInf(nStaId).ContolScheme(i).SModelName(j)
    '                    Exit For
    '                End If
    '            Next
    '            Exit For
    '        End If
    '    Next
    'End Sub
    Public Sub RefreshTrackInf(ByVal nStaID As Integer, ByVal nTrackID As Integer)
        Dim i As Integer
        For i = 1 To UBound(TrackInf)
            If TrackInf(i).nStaID = nStaID And TrackInf(i).nTrackID = nTrackID Then
                TrackInf(i).X1 = CADStaInf(nStaID).Track(nTrackID).X1
                TrackInf(i).Y1 = CADStaInf(nStaID).Track(nTrackID).Y1
                TrackInf(i).X2 = CADStaInf(nStaID).Track(nTrackID).X2
                TrackInf(i).Y2 = CADStaInf(nStaID).Track(nTrackID).Y2
                Exit For
            End If
        Next
    End Sub

    '得到显示线段的宽度
    Public Function GetShowLineWidth(ByVal nFirstWidth As Single, ByVal sScale As Single) As Single
        Dim LineWidth As Single
        LineWidth = nFirstWidth * sScale
        If LineWidth < 2 And LineWidth > 1 Then
            LineWidth = 2
        ElseIf LineWidth < 1 Then
            LineWidth = 1
        End If
        GetShowLineWidth = LineWidth
    End Function

    '刷新控制模块信息
    Public Sub RefreshCADControModeInf()
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim p As Integer
        For i = 1 To UBound(CADStaInf)
            For j = 1 To UBound(CADStaInf(i).ContolScheme)
                If CADStaInf(i).ContolScheme(j).sSchemeName = CADStaInf(i).sCurControlScheme Then
                    For k = 1 To UBound(CADStaInf(i).Track)
                        For p = 1 To UBound(CADStaInf(i).ContolScheme(j).STrackNum)
                            If CADStaInf(i).Track(k).sTrackCircuitNum = CADStaInf(i).ContolScheme(j).STrackNum(p) Then
                                CADStaInf(i).Track(k).sControlNum = CADStaInf(i).ContolScheme(j).SModelName(p)
                                Exit For
                            End If
                        Next
                    Next
                End If
            Next
        Next
    End Sub

    '打印车站平面图
    Public Sub PrintStaCADStation(ByVal sStaName As String, ByVal rBmpGraphics As Graphics, ByVal nleftX As Long, ByVal priLeftBlank As Long, ByVal nTopY As Long, ByVal priTopYblank As Long, ByVal sngWidth As Single, ByVal sngHeight As Single, ByVal IfPrintGuDaoNum As Boolean, _
                                                ByVal IfPrintTrackNum As Boolean, ByVal IfPrintCorssNum As Boolean, ByVal ifPrintShotLine As Boolean)
        Dim i As Integer
        Dim j As Integer
        Dim priMaxX As Single
        Dim priMinX As Single
        Dim priMaxY As Single
        Dim priMinY As Single
        Dim nCurPicLeftX As Single
        Dim nCurPicTopY As Single
        Dim nMaxWidth As Single
        Dim nMaxHeight As Single
        Dim sngBili As Single
        Dim sngBiliOne As Single
        Dim sngBiliTwo As Single
        priMinX = 10000000
        priMaxX = -1000000
        priMinY = 10000000
        priMaxY = -1000000
        Dim nCurStaID As Integer
        For i = 1 To UBound(CADStaInf)
            If CADStaInf(i).sStaName = sStaName Then
                For j = 1 To UBound(CADStaInf(i).Track)
                    If CADStaInf(i).Track(j).X1 < priMinX Then
                        priMinX = CADStaInf(i).Track(j).X1
                    End If
                    If CADStaInf(i).Track(j).X2 < priMinX Then
                        priMinX = CADStaInf(i).Track(j).X2
                    End If
                    If CADStaInf(i).Track(j).X1 > priMaxX Then
                        priMaxX = CADStaInf(i).Track(j).X1
                    End If
                    If CADStaInf(i).Track(j).X2 > priMaxX Then
                        priMaxX = CADStaInf(i).Track(j).X2
                    End If
                    If CADStaInf(i).Track(j).Y1 < priMinY Then
                        priMinY = CADStaInf(i).Track(j).Y1
                    End If
                    If CADStaInf(i).Track(j).Y2 < priMinY Then
                        priMinY = CADStaInf(i).Track(j).Y2
                    End If
                    If CADStaInf(i).Track(j).Y1 > priMaxY Then
                        priMaxY = CADStaInf(i).Track(j).Y1
                    End If
                    If CADStaInf(i).Track(j).Y2 > priMaxY Then
                        priMaxY = CADStaInf(i).Track(j).Y2
                    End If
                Next
                nCurStaID = i
                Exit For
            End If
        Next
        nMaxWidth = priMaxX - priMinX
        nMaxHeight = priMaxY - priMinY

        sngWidth = sngWidth - (priLeftBlank + nleftX) * 2
        sngHeight = sngHeight - priTopYblank * 2 - nTopY
        sngBiliOne = sngWidth / nMaxWidth
        sngBiliTwo = sngHeight / nMaxHeight
        'If nMaxWidth <= sngWidth Then
        '    sngBiliOne = 1
        'Else
        '    sngBiliOne = sngWidth / nMaxWidth
        'End If

        'If nMaxHeight <= sngHeight Then
        '    sngBiliTwo = 1
        'Else
        '    sngBiliTwo = sngHeight / nMaxHeight
        'End If

        sngBili = Math.Min(sngBiliOne, sngBiliTwo)
        If sngBili > 1.5 Then
            sngBili = 1.5
        End If
        If nMaxWidth < sngWidth Then
            nCurPicLeftX = priLeftBlank + nleftX + (sngWidth - nMaxWidth * sngBili) / 2
        Else
            nCurPicLeftX = priLeftBlank + nleftX
        End If

        If nMaxHeight < sngHeight Then
            nCurPicTopY = priTopYblank + nTopY + (sngHeight - nMaxHeight * sngBili) / 2
        Else
            nCurPicTopY = priTopYblank + nTopY
        End If
        'rBmpGraphics.FillRectangle(Brushes.Black, nCurPicLeftX - 50, nCurPicTopY - 50, nMaxWidth * sngBili + 100, nMaxHeight * sngBili + 100)
        Call DrawStaPicFormStaID(nCurStaID, sngBili, Color.DarkGray, 4, 3, rBmpGraphics, priMinX, priMinY, nCurPicLeftX, nCurPicTopY, IfPrintGuDaoNum, IfPrintTrackNum, IfPrintCorssNum, ifPrintShotLine)
    End Sub

    '自对齐的X坐标
    Public Function GetGridX(ByVal X As Single) As Single
        GetGridX = Math.Round(X / nGridWidth) * nGridWidth
    End Function

End Module
