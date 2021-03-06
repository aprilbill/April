Module modMainSub

    Public Sub MainStart(ByVal Trw As TreeView, ByVal PicNet As PictureBox, ByVal picBack As PictureBox)
        Dim CmdArgs() As String = Environment.GetCommandLineArgs()
        Dim ArgNum As Integer
        Dim tmpString As String
        Dim tmpStr As String

        If CmdArgs.Length > 0 Then
            For ArgNum = 0 To UBound(CmdArgs)
                tmpStr = CmdArgs(ArgNum)
                If tmpStr.Length > 4 Then
                    tmpString = tmpStr.Substring(tmpStr.Length - 4, 4)
                    If tmpString = ".tpm" Then '单个数据库
                        Call SetSysDataBaseInf(tmpStr, Trw, PicNet, picBack)
                        Exit Sub
                    ElseIf tmpString = "tpmp" Then '整个网络数据
                        tmpStr = OpenWholeNet(tmpStr)
                        Call SetSysDataBaseInf(tmpStr, Trw, PicNet, picBack)
                        Exit Sub
                    End If
                End If
            Next ArgNum
        End If
    End Sub

    Enum GetweekDayFromID As Integer
        日 = 1
        一 = 2
        二 = 3
        三 = 4
        四 = 5
        五 = 6
        六 = 7
    End Enum
    '通过ID得到星期几
    Public Function FromIDToWeekDay(ByVal nID As Integer) As String
        FromIDToWeekDay = "无"
        Select Case nID
            Case 1
                FromIDToWeekDay = "星期日"
            Case 2
                FromIDToWeekDay = "星期一"
            Case 3
                FromIDToWeekDay = "星期二"
            Case 4
                FromIDToWeekDay = "星期三"
            Case 5
                FromIDToWeekDay = "星期四"
            Case 6
                FromIDToWeekDay = "星期五"
            Case 7
                FromIDToWeekDay = "星期六"
        End Select
    End Function

    Function dTime(ByVal temp As Long, ByVal Mark As Integer) As String
        dTime = ""
        If temp = -1 Then
            dTime = ""
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
        If HStr.Trim.Length = 1 Then
            HStr = "0" & HStr.Trim
        End If
        Select Case Mark
            Case 0
                dTime = HStr & ":" & MStr & ":" & sStr
            Case 1
                'dTime = sSpace & sSpace & "." & MStr & "." & sStr
                dTime = HStr & ":" & MStr & ":" & sStr
            Case 2
                dTime = HStr & ":" & MStr
            Case 3
                dTime = ""
            Case 4
                dTime = "—"
            Case 5
                'dTime = HStr & "." & MStr
                dTime = HStr & ":" & MStr
        End Select

    End Function

    '画全部的线
    Public Sub RefreshDiagram(ByVal nState As Integer) '(ByVal rBmpGraphic As Graphics, ByVal PicWidth As Single, ByVal PicHeight As Single, ByVal picLeftBlank As Single, ByVal picTopBlank As Single, ByVal picStablank As Single)
        'nstate=0  表示全部刷新,1表示只刷新运行线与车底交路线，2表示只刷新车底交路线
        Dim i As Integer
        Dim rbmpGraphics As Graphics
        Dim tmprbmP As Bitmap
        Dim rbmpStaGraphics As Graphics
        Dim tmprbmPSta As Bitmap
        Dim rbmpStaGraphics2 As Graphics
        Dim tmprbmPSta2 As Bitmap
        ReDim TimeTablePara.nPubTrains(0)
        If TimeTablePara.sCurDiagramState = DiagramState.运行图 Then '列车运行图
            Select Case nState
                Case 0 '全部刷新
                    '先整理列车车次，保证其唯一性
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            TrainInf(i).Train = FormatPrintTrainHou(i, 3)
                        End If
                    Next
                    Call ResetCheDiLinkTrainNumber()
                    TimeTablePara.picPubDiagram.Width = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
                    TimeTablePara.picPubDiagram.Height = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
                    TimeTablePara.picPubStation.Width = 80 ' TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
                    TimeTablePara.picPubStation.Height = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
                    TimeTablePara.picPubStation2.Width = 80 'TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
                    TimeTablePara.picPubStation2.Height = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
                    Try
                        tmprbmP = New Bitmap(TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight)
                        rbmpGraphics = Graphics.FromImage(tmprbmP)
                        tmprbmPSta = New Bitmap(TimeTablePara.TimeTableDiagramPara.sngPicStationWidth, TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight)
                        rbmpStaGraphics = Graphics.FromImage(tmprbmPSta)
                        tmprbmPSta2 = New Bitmap(TimeTablePara.TimeTableDiagramPara.sngPicStationWidth, TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight)
                        rbmpStaGraphics2 = Graphics.FromImage(tmprbmPSta2)
                    Catch ex As Exception
                        'MsgBox("对不起，系统内存不足，请重新启动计算机再试一次！")
                        Exit Sub
                    End Try
                    rbmpGraphics.Clear(Color.White)
                    'rbmpGraphics.FillRectangle(Brushes.Yellow, 0, 0, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight)

                    'rbmpGraphics = TimeTablePara.picPubDiagram.CreateGraphics

                    Call DrawTimeLine(rbmpGraphics, rbmpStaGraphics, rbmpStaGraphics2, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngtopBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, TimeTablePara.TimeTableDiagramPara.sngTimeBlank, 0, 0, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.strTimeFormat, "全部", 0, False, False)
                    TimeTablePara.pubTimeBmpPic = tmprbmP.Clone
                    rbmpGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias
                    If TimeTablePara.sPubCurSkbName <> "" Then
                        Call DrawDiagramLine(rbmpGraphics, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, 0, TimeTablePara.TimeTableDiagramPara.nifPrintCheCi, TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi, TimeTablePara.TimeTableDiagramPara.sCheDiLineStyle, True, TimeTablePara.TimeTableDiagramPara.nCheDiLineHeight)
                    End If

                    TimeTablePara.picPubDiagram.Image = tmprbmP
                    TimeTablePara.picPubStation.Image = tmprbmPSta
                    TimeTablePara.picPubStation2.Image = tmprbmPSta2
                    ' TimeTablePara.pubTimeBmpPic.Save("b.bmp")

                    tmprbmP = Nothing
                    rbmpGraphics.Dispose()

                    '检查列车运行图 
                    'If frmTimeTableMain.自动检查错误EToolStripMenuItem.Checked = True Then
                    '    Call CheckDiagram()
                    'End If
                Case 1

                    Dim tmprbmP4 As Bitmap
                    tmprbmP4 = TimeTablePara.pubTimeBmpPic.Clone  ' Image.FromFile("ab.bmp")
                    Dim rbmpGraphics4 As Graphics
                    rbmpGraphics4 = Graphics.FromImage(tmprbmP4)
                    rbmpGraphics4.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias
                    If TimeTablePara.sPubCurSkbName <> "" Then
                        Call DrawDiagramLine(rbmpGraphics4, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, 0, TimeTablePara.TimeTableDiagramPara.nifPrintCheCi, TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi, TimeTablePara.TimeTableDiagramPara.sCheDiLineStyle, True, TimeTablePara.TimeTableDiagramPara.nCheDiLineHeight)
                    End If
                    TimeTablePara.picPubDiagram.Image = tmprbmP4
                    tmprbmP4 = Nothing
                    rbmpGraphics4.Dispose()
            End Select

        Else '车站技术图解

            Dim nSta As Integer
            nSta = StaNameToStaInfID(TimeTablePara.StaDiagramePara.sCurStaName)
            If nSta > 0 Then
                Dim rbmP As Bitmap
                Dim rbmP1 As Bitmap
                Dim rbmP2 As Bitmap
                Dim topBlank As Single
                Dim sngTopY As Single
                Dim picHeight As Integer
                Dim nGudaoHeight As Integer
                Dim sngTimeBlank As Single
                nGudaoHeight = TimeTablePara.StaDiagramePara.nStaLineHeight
                topBlank = 80
                sngTopY = 250
                sngTimeBlank = 20
                Call InputGuDaoYData(nSta, nGudaoHeight, topBlank, sngTopY)
                picHeight = GuDaoJishutujie.nTotleY
                TimeTablePara.picPubDiagram.Height = picHeight
                TimeTablePara.picPubStation.Height = picHeight
                rbmP = New System.Drawing.Bitmap(TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, picHeight)
                rbmpGraphics = Graphics.FromImage(rbmP)
                rbmP1 = New System.Drawing.Bitmap(TimeTablePara.TimeTableDiagramPara.sngStaBlank, picHeight)
                rbmpStaGraphics = Graphics.FromImage(rbmP1)
                TimeTablePara.picPubStation2.Height = picHeight
                rbmP2 = New System.Drawing.Bitmap(TimeTablePara.TimeTableDiagramPara.sngStaBlank, picHeight)
                rbmpStaGraphics2 = Graphics.FromImage(rbmP2)

                rbmpGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias
                Call PrintStaCADStation(StationInf(nSta).sStationName, rbmpGraphics, 10, 30, 30, 20, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, 250, True, True, True, False)
                Call DrawGuDaoJiShuTuJie(nSta, rbmpGraphics, rbmpStaGraphics, rbmpStaGraphics2, TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, _
                                                          picHeight, TimeTablePara.TimeTableDiagramPara.sngLeftBlank, _
                                                           topBlank, TimeTablePara.TimeTableDiagramPara.sngStaBlank, _
                                                              sngTimeBlank, 0, sngTopY, TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, nGudaoHeight, "显示", TimeTablePara.TimeTableDiagramPara.strTimeFormat)
                TimeTablePara.picPubDiagram.Image = rbmP
                TimeTablePara.picPubStation.Image = rbmP1
                'rbmP2.RotateFlip(RotateFlipType.Rotate90FlipNone)
                TimeTablePara.picPubStation2.Image = rbmP2
            End If
        End If
    End Sub

    '获得当前列车总数量
    Public Function GetTolTrainNum() As Integer
        Dim i As Integer
        GetTolTrainNum = 0
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train <> "" Then
                GetTolTrainNum = GetTolTrainNum + 1
            End If
        Next
    End Function

    '取最大正整数
    Public Function GetMaxZhenShu(ByVal sNum As Single) As Integer
        If Int(sNum) = sNum Then
            GetMaxZhenShu = Int(sNum)
        Else
            GetMaxZhenShu = Int(sNum) + 1
        End If
    End Function

    '由车次得到列车ID
    Public Function FormCheCiToTrainID(ByVal sCheCi As String) As Integer
        Dim i As Integer
        FormCheCiToTrainID = 0
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).Train = sCheCi Then
                FormCheCiToTrainID = i
                Exit For
            End If
        Next
    End Function    '由车次得到列车ID

    Public Function FormPrintCheCiToTrainID(ByVal sCheCi As String) As Integer
        Dim i As Integer
        FormPrintCheCiToTrainID = 0
        For i = 1 To UBound(TrainInf)
            If TrainInf(i).sPrintTrain = sCheCi Then
                FormPrintCheCiToTrainID = i
                Exit For
            End If
        Next
    End Function

    '由车底编号得到车底ID
    Public Function FromCheDiNameToCheDiID(ByVal sCheDi As String) As Integer
        Dim i As Integer
        For i = 1 To UBound(ChediInfo)
            If ChediInfo(i).sCheCiHao = sCheDi Then
                FromCheDiNameToCheDiID = i
                Exit For
            End If
        Next i
    End Function

    '由车底编号得到车底ID
    Public Function FromPrintCheDiNameToCheDiID(ByVal sCheDi As String) As Integer
        Dim i As Integer
        For i = 1 To UBound(ChediInfo)
            If ChediInfo(i).sCheCiHao = sCheDi Then
                FromPrintCheDiNameToCheDiID = i
                Exit For
            End If
        Next i
    End Function

    '得到当前列车的同一交路的下一列车
    Public Function GetCheDiNextTrain(ByVal nTrain As Integer) As Integer
        GetCheDiNextTrain = 0
        Dim i As Integer
        Dim j As Integer
        For i = 1 To UBound(ChediInfo)
            For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                If ChediInfo(i).nLinkTrain(j) = nTrain Then
                    If j < UBound(ChediInfo(i).nLinkTrain) Then
                        GetCheDiNextTrain = ChediInfo(i).nLinkTrain(j + 1)
                        Exit Function
                    End If
                End If
            Next
        Next
    End Function

    '得到当前列车的同一交路的上一列车
    Public Function GetCheDiUpTrain(ByVal nTrain As Integer) As Integer
        GetCheDiUpTrain = 0
        Dim i As Integer
        Dim j As Integer
        For i = 1 To UBound(ChediInfo)
            For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                If ChediInfo(i).nLinkTrain(j) = nTrain Then
                    If j > 1 Then
                        GetCheDiUpTrain = ChediInfo(i).nLinkTrain(j - 1)
                        Exit Function
                    End If
                End If
            Next
        Next
    End Function
    '获得系统运行路径
    Public Function GetAppPath() As String
        If Right(Application.StartupPath, 1) <> "\" Then
            GetAppPath = Application.StartupPath & "\"
        Else
            GetAppPath = Application.StartupPath
        End If
    End Function

    '将分钟转化为小时
    Public Function MinuteToHour(ByVal MinuTime As Single) As String
        ' Dim Least As Single
        Dim Hou As String
        Dim Min As String
        Dim Sec As String
        Hou = Trim(Int(Int(Val(MinuTime)) / 60))
        If Len(Hou) = 1 Then
            Hou = Trim("0" & Trim(Hou))
        End If
        Min = Trim(Int(Val(MinuTime)) - Val(Hou) * 60)
        If Len(Min) = 1 Then
            Min = Trim("0" & Trim(Min))
        End If
        Sec = Trim((MinuTime - Int(MinuTime)) * 60)
        If Len(Sec) = 1 Then
            Sec = Trim("0" & Trim(Sec))
        End If
        'Least = Format(MinuTime - Int(MinuTime / 60) * 60, "##0") * 0.01
        MinuteToHour = Hou & "." & Min & "." & Sec
    End Function

    '求最小值
    Public Function Minimal(ByVal sValOne As Single, ByVal sValTwo As Single) As Single
        If sValOne < sValTwo Then
            Minimal = sValOne
        Else
            Minimal = sValTwo
        End If
    End Function
    '求最大值
    Public Function Maximal(ByVal sValOne As Single, ByVal sValTwo As Single) As Single
        If sValOne > sValTwo Then
            Maximal = sValOne
        Else
            Maximal = sValTwo
        End If
    End Function

    '求两点站的距离
    Public Function GetTwoPointLength(ByVal X1 As Single, ByVal Y1 As Single, ByVal X2 As Single, ByVal Y2 As Single) As Single
        GetTwoPointLength = Math.Sqrt((X1 - X2) ^ 2 + (Y1 - Y2) ^ 2)
    End Function

    '排序
    Public Sub SetPaiXu(ByRef Tmp() As String, ByVal nUpOrDown As Integer)

        '按列车到站点排序
        Dim j, k, temp, Flag As Integer
        Dim TempTime1, Temptime2 As Single
        '按到达时间排序
        Flag = 1
        k = UBound(tmp)
        Do While Flag > 0
            k = k - 1
            Flag = 0
            For j = 1 To k
                TempTime1 = Val(tmp(j))
                Temptime2 = Val(tmp(j + 1))
                If nUpOrDown = 1 Then '降序
                    If TempTime1 < Temptime2 Then '
                        temp = Tmp(j)
                        Tmp(j) = Tmp(j + 1)
                        Tmp(j + 1) = temp
                        Flag = 1
                    End If
                Else '升序
                    If TempTime1 > Temptime2 Then '
                        temp = Tmp(j)
                        Tmp(j) = Tmp(j + 1)
                        Tmp(j + 1) = temp
                        Flag = 1
                    End If
                End If
            Next j
        Loop
    End Sub

    '分解字符串
    Public Sub SplitString(ByRef sValue() As String, ByVal sString As String)
        Dim j As Integer
        ReDim sValue(0)
        If sString = "无" Or sString.Trim = "" Then
            Exit Sub
        End If
        Dim i As Integer
        If Len(sString) > 0 Then
            If Right(sString, 1) = "," Or Right(sString, 1) = "，" Or Right(sString, 1) = "、" Then
            Else
                sString = sString & ","
            End If
            If Left(sString, 1) = "," Or Left(sString, 1) = "，" Or Left(sString, 1) = "、" Then
                sString = sString.Substring(2)
            End If
            i = 1
            For j = 1 To Len(sString)
                If Mid(sString, j, 1) = "," Or Mid(sString, j, 1) = "，" Or Mid(sString, j, 1) = "、" Then
                    If Trim(Mid(sString, i, j - i)) <> "" Then
                        ReDim Preserve sValue(UBound(sValue) + 1)
                        sValue(UBound(sValue)) = Mid(sString, i, j - i)
                        i = j + 1
                    End If
                End If
            Next j
        Else
            Exit Sub
        End If
    End Sub

    Public Function SplicListofString(ByVal sString As String) As System.Collections.Generic.List(Of String)
        Dim a As New System.Collections.Generic.List(Of String)
        Dim j As Integer
        If sString = "无" Or sString.Trim = "" Then

        Else
            Dim i As Integer
            If Len(sString) > 0 Then
                If Right(sString, 1) = "," Or Right(sString, 1) = "，" Or Right(sString, 1) = "、" Then
                Else
                    sString = sString & ","
                End If
                If Left(sString, 1) = "," Or Left(sString, 1) = "，" Or Left(sString, 1) = "、" Then
                    sString = sString.Substring(1)
                End If

                i = 1
                For j = 1 To Len(sString)
                    If Mid(sString, j, 1) = "," Or Mid(sString, j, 1) = "，" Or Mid(sString, j, 1) = "、" Then
                        If Trim(Mid(sString, i, j - i)) <> "" Then
                            a.Add(Mid(sString, i, j - i))
                            i = j + 1
                        End If
                    End If
                Next j
            End If
        End If
        Return a
    End Function

End Module
