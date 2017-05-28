Public Module modOds
    Public TMSlocalDataSet As New DataAccessTier.OdsDataSet(DataAccessTier.DataBaseType.Access)
    Public InputDatabasePath As String
    Public InputDatabaseName As String
    Public InputDatabaseConString As String
    Public Const InputDatabasePassWord As String = "tpmadmin"

    '线路与车站信息
    Public Structure typeODSStaInf
        Dim sStaName As String
        Dim sLineName As String
    End Structure
    Public ODSStainf As Generic.List(Of typeODSStaInf)

    '换乘站信息
    Public Structure typeTransferSta
        Dim sUpStaName As String
        Dim sLineName As String
        Dim sDownStaName As String
        Dim sUpSecName As String
        Dim sDownSecName As String
    End Structure
    Public Structure typeTransferStaInformation
        Dim sStaName As String
        Dim sSecInf As Generic.List(Of typeTransferSta)
    End Structure
    Public TransferStaInf As New typeTransferStaInformation

    Structure typeCurDiagrameVersion
        Dim sLineName As String
        Dim sCurDiaVersion As String
        Dim sCurDiaName As String
    End Structure

    Public Structure typeTrainDiagramTime
        Dim sLineName As String
        Dim sStaName As String '当前站名
        Dim sUpStaName As String '上一站名
        Dim sNextStaName As String '下一站名
        Dim DownStartTime1() As Date
        Dim DownEndTime1() As Date
        Dim DownStartTime2() As Date
        Dim DownEndTime2() As Date
        Dim UpStartTime1() As Date
        Dim UpEndTime1() As Date
        Dim UpStartTime2() As Date
        Dim UpEndTime2() As Date
    End Structure

    Public StaDiagram As New System.Collections.Generic.List(Of typeTrainDiagramTime)

    Public DiaVer() As typeCurDiagrameVersion

    Structure typeODSPubPara
        Dim DiagramSelect As String '当前选中的运行图编号
        Dim sDiagramName As String '运行图名称
        Dim sCurShowListState As String '系统显示状态，单线全图，换乘站图
    End Structure
    Public ODSPubpara As typeODSPubPara

    Public Sub LoadCurVersion()
        Dim i, j As Integer
        Dim sCurDate As String
        ReDim DiaVer(0)
        Dim sCurVersion As String = ""
        Dim sCurLine As String = ""
        Dim sCurDiaName As String = ""
        sCurDate = DateFormat(Now)
        TMSlocalDataSet.TMS_TRAINDIAGRAMUSING.Clear()
        TMSlocalDataSet.PD_LINEINFO.Clear()
        TMSlocalDataSet.Fill("PD_LINEINFO", "LINEOPERATIONSTATE = '运营'")
        TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMUSING", "INPUTDATE = '" & sCurDate & "'")
        For j = 1 To TMSlocalDataSet.PD_LINEINFO.Rows.Count
            sCurLine = TMSlocalDataSet.PD_LINEINFO.Rows(j - 1).Item("LINENAME")
            sCurVersion = "空"
            sCurDiaName = "空"
            For i = 1 To TMSlocalDataSet.TMS_TRAINDIAGRAMUSING.Rows.Count
                If TMSlocalDataSet.TMS_TRAINDIAGRAMUSING(i - 1).Item("LINENAME") = sCurLine Then
                    sCurVersion = TMSlocalDataSet.TMS_TRAINDIAGRAMUSING(i - 1).Item("TRAINDIAGRAMID")
                    sCurDiaName = GetDiagramNameFromVersion(sCurVersion)
                    Exit For
                End If
            Next
            ReDim Preserve DiaVer(UBound(DiaVer) + 1)
            DiaVer(UBound(DiaVer)).sCurDiaVersion = sCurVersion
            DiaVer(UBound(DiaVer)).sLineName = sCurLine
            DiaVer(UBound(DiaVer)).sCurDiaName = sCurDiaName
        Next
    End Sub

    Public Sub LoadCADStaData()
        '  Try
        Progress.ProgressForm.StartProgress(3, "正在加载车站平面图相关数据，请稍候...")
        Progress.ProgressForm.PerformStep()
        Call readNetData()
        Progress.ProgressForm.PerformStep()
        Call InputStationAndSectionDataToCADStainf() '车站平面图信息 '********需要导入*********
        Progress.ProgressForm.PerformStep()
        Call InputAllDataToCADstaInfNoprobar()
        Progress.ProgressForm.EndProgress()
        ' Catch ex As Exception
        'Progress.ProgressForm.EndProgress()
        ' End Try
    End Sub

    Public Sub LoadDiagramData(ByVal sState As String)
        ODSPubpara.sDiagramName = GetDiagramNameFromVersion(ODSPubpara.DiagramSelect)
        ODSPubpara.sCurShowListState = sState
        'Try
        Progress.ProgressForm.StartProgress(11, "正在加载运行图相关数据，请稍候...")
        Progress.ProgressForm.PerformStep()
        Call InitSystemVariant(0)
        Progress.ProgressForm.PerformStep()
        Call readNetData()
        Progress.ProgressForm.PerformStep()
        Call IniteDiagramPicViraient("列车运行图")
        Progress.ProgressForm.PerformStep()
        Call readNetStaAndSecData()
        Progress.ProgressForm.PerformStep()
        Call InputStationGudaoAndJinLuInfByDAO()
        Progress.ProgressForm.PerformStep()
        Call InputChediZhefanDataAndCheDiScaleInf()
        Progress.ProgressForm.PerformStep()
        Call InputChediAndTrainJianGeData(sState)
        Progress.ProgressForm.PerformStep()
        Call ReadSKBStaSeqData()
        Progress.ProgressForm.PerformStep()
        Call ReadBaseTrainInf()
        Progress.ProgressForm.PerformStep()
        If sState <> "新建运行图" Then
            Call ReadTrainAndTimeTableInf(ODSPubpara.DiagramSelect)
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
    '读入全部数据
    'Public Sub LoadAllData()
    '    Call LoadDiagramData()
    '    Call LoadCADStaData()
    'End Sub
    '通过输出站名得到实际的运行图对应的站名
    Public Function GetDiaStaNameFromPrintStaName(ByVal sName As String) As String
        TMSlocalDataSet.TMS_STATIONINFO.Clear()
        TMSlocalDataSet.Fill("TMS_STATIONINFO")
        GetDiaStaNameFromPrintStaName = "空"
        Dim i As Integer
        For i = 1 To TMSlocalDataSet.TMS_STATIONINFO.Rows.Count
            If TMSlocalDataSet.TMS_STATIONINFO.Rows(i - 1).Item("OUTPUTNAME") = sName Then
                Return TMSlocalDataSet.TMS_STATIONINFO.Rows(i - 1).Item("STATIONNAME")
                Exit For
            End If
        Next
    End Function

    '通过运行图版本得到运行图名称
    Public Function GetDiagramNameFromVersion(ByVal sVersion As String) As String
        Dim TMS_TRAINDIAGRAMINFO As New DataTable
        Dim sqlstr As String = "select * from TMS_TRAINDIAGRAMINFO where TRAINDIAGRAMID='" & sVersion & "'"
        TMS_TRAINDIAGRAMINFO = Globle.Method.ReadDataForAccess(sqlstr)
        GetDiagramNameFromVersion = "空"
        Dim i As Integer
        For i = 1 To TMS_TRAINDIAGRAMINFO.Rows.Count
            If TMS_TRAINDIAGRAMINFO.Rows(i - 1).Item("TIMETABLENAME").ToString.Trim <> "" Then
                Return TMS_TRAINDIAGRAMINFO.Rows(i - 1).Item("TIMETABLENAME").ToString
                Exit For
            End If
        Next
    End Function

    '通过运行图名称找版本号
    Public Function GetDiagramVersionFromName(ByVal sName As String) As String
        Dim TMS_TRAINDIAGRAMINFO As New DataTable
        Dim sqlstr As String = "select * from TMS_TRAINDIAGRAMINFO where TIMETABLENAME='" & sName & "'"
        TMS_TRAINDIAGRAMINFO = Globle.Method.ReadDataForAccess(sqlstr)
        GetDiagramVersionFromName = "空"
        Dim i As Integer
        For i = 1 To TMS_TRAINDIAGRAMINFO.Rows.Count
            If TMS_TRAINDIAGRAMINFO.Rows(i - 1).Item("TIMETABLENAME").ToString.Trim <> "" Then
                Return TMS_TRAINDIAGRAMINFO.Rows(i - 1).Item("TRAINDIAGRAMID")
                Exit For
            End If
        Next
    End Function

    '通过运行图版本得到运行图类型
    Public Function GetDiagramStyleFromVersion(ByVal sVersion As String) As String
        TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Clear()
        TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMINFO")
        GetDiagramStyleFromVersion = "空"
        Dim i As Integer
        For i = 1 To TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Rows.Count
            If TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Rows(i - 1).Item("TRAINDIAGRAMID") = sVersion Then
                Return TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Rows(i - 1).Item("TRAINDIASTYLENAME")
                Exit For
            End If
        Next
    End Function

    '通过运行图版本得到线路名称
    Public Function GetDiagramLineNameFromVersion(ByVal sVersion As String) As String
        TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Clear()
        TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMINFO")
        GetDiagramLineNameFromVersion = "空"
        Dim i As Integer
        For i = 1 To TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Rows.Count
            If TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Rows(i - 1).Item("TRAINDIAGRAMID") = sVersion Then
                Return TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Rows(i - 1).Item("LINENAME")
                Exit For
            End If
        Next
    End Function

    '通过线路得到当前版本的运行图版本号
    Public Function GetCurDiaVersionFromLineName(ByVal sLineName As String) As String
        GetCurDiaVersionFromLineName = ""
        Dim i As Integer
        For i = 1 To UBound(DiaVer)
            If DiaVer(i).sLineName = sLineName Then
                Return DiaVer(i).sCurDiaVersion
            End If
        Next
    End Function

    '从数据库导入所有数据到CADStaInf()中
    Public Sub InputAllDataToCADstaInfNoprobar()
        Dim k As Integer
        If UBound(CADStaInf) > 0 Then
            For k = 1 To UBound(CADStaInf)
                Call InputDataToCADstaInfTrackByDAO(k)
            Next
            Call InputTrackInf()
        End If
    End Sub

    Public Sub readNetData()
        ReDim NetInf.Line(0)
        Dim TMS_LINEINFO, TMS_STATIONINFO, TMS_SECTIONINFO As New DataTable
        Dim sqlstr As String = ""
        sqlstr = "select * from TMS_LINEINFO where TRAINDIAGRAMID='" & ODSPubpara.DiagramSelect & "'order by LISTNO"
        TMS_LINEINFO = Globle.Method.ReadDataForAccess(sqlstr)
        Dim i As Integer
        Dim j As Integer
        Try
            ReDim NetInf.Line(TMS_LINEINFO.Rows.Count)
            For i = 1 To TMS_LINEINFO.Rows.Count
                NetInf.Line(i).sName = TMS_LINEINFO.Rows(i - 1).ItemArray(2).ToString.Trim
                NetInf.Line(i).sngLength = Val(TMS_LINEINFO.Rows(i - 1).ItemArray(6))
                NetInf.Line(i).sMemo = TMS_LINEINFO.Rows(i - 1).ItemArray(7).ToString
                NetInf.Line(i).intSeq = CInt(TMS_LINEINFO.Rows(i - 1).ItemArray(1))
                NetInf.Line(i).sEngName = TMS_LINEINFO.Rows(i - 1).ItemArray(4).ToString.Trim
                NetInf.Line(i).sBrriName = TMS_LINEINFO.Rows(i - 1).ItemArray(3).ToString.Trim
                ReDim NetInf.Line(i).Station(0)
                ReDim NetInf.Line(i).Section(0)
            Next

            '车站信息
            sqlstr = "select * from TMS_STATIONINFO where TRAINDIAGRAMID='" & ODSPubpara.DiagramSelect & "'order by LINENAME,DOWNSEQUENCE"
            TMS_STATIONINFO = Globle.Method.ReadDataForAccess(sqlstr)
            Dim nID As Integer
            nID = 0
            ReDim StaInf(0)
            For i = 1 To UBound(NetInf.Line)
                For j = 1 To TMS_STATIONINFO.Rows.Count
                    If Trim(TMS_STATIONINFO.Rows(j - 1).ItemArray(1).ToString) = NetInf.Line(i).sName Then
                        nID = nID + 1
                        ReDim Preserve NetInf.Line(i).Station(UBound(NetInf.Line(i).Station) + 1)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).nDownID = CInt(TMS_STATIONINFO.Rows(j - 1).ItemArray(3))
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sStaName = Trim(TMS_STATIONINFO.Rows(j - 1).ItemArray(2).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sPrintStaName = Trim(TMS_STATIONINFO.Rows(j - 1).ItemArray(4).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sEngName = Trim(TMS_STATIONINFO.Rows(j - 1).ItemArray(5).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sStaStyle = Trim(TMS_STATIONINFO.Rows(j - 1).ItemArray(8).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sEngBrriName = Trim(TMS_STATIONINFO.Rows(j - 1).ItemArray(6).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sStaCode = Trim(TMS_STATIONINFO.Rows(j - 1).ItemArray(14).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sSingleOrDoubleLine = Trim(TMS_STATIONINFO.Rows(j - 1).ItemArray(7).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sStaProperty = Trim(TMS_STATIONINFO.Rows(j - 1).ItemArray(9).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sngXcoord = Val(TMS_STATIONINFO.Rows(j - 1).ItemArray(11).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sngYcoord = Val(TMS_STATIONINFO.Rows(j - 1).ItemArray(12).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sMemo = Trim(TMS_STATIONINFO.Rows(j - 1).ItemArray(13).ToString)
                        NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sPicPath = Trim(TMS_STATIONINFO.Rows(j - 1).ItemArray(10).ToString)
                        ReDim Preserve StaInf(UBound(StaInf) + 1)
                        StaInf(UBound(StaInf)).sStaName = Trim(TMS_STATIONINFO.Rows(j - 1).ItemArray(2).ToString)
                        StaInf(UBound(StaInf)).sEngName = Trim(TMS_STATIONINFO.Rows(j - 1).ItemArray(5).ToString)
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
            sqlstr = "select * from TMS_SECTIONINFO where TRAINDIAGRAMID='" & ODSPubpara.DiagramSelect & "'order by LINENAME,SECTIONSEQ"
            TMS_SECTIONINFO = Globle.Method.ReadDataForAccess(sqlstr)

            For j = 1 To UBound(NetInf.Line)
                For i = 1 To TMS_SECTIONINFO.Rows.Count
                    If Trim(TMS_SECTIONINFO.Rows(i - 1).ItemArray(1).ToString) = NetInf.Line(j).sName Then
                        ReDim Preserve NetInf.Line(j).Section(UBound(NetInf.Line(j).Section) + 1)
                        NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecName = Trim(TMS_SECTIONINFO.Rows(i - 1).ItemArray(3).ToString)
                        NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).nID = CInt(TMS_SECTIONINFO.Rows(i - 1).ItemArray(2))
                        NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecFirName = Trim(TMS_SECTIONINFO.Rows(i - 1).ItemArray(4).ToString)
                        NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecSecName = Trim(TMS_SECTIONINFO.Rows(i - 1).ItemArray(5).ToString)
                        NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecUpLength = Trim(TMS_SECTIONINFO.Rows(i - 1).ItemArray(6).ToString)
                        NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecDownLength = Trim(TMS_SECTIONINFO.Rows(i - 1).ItemArray(7).ToString)
                        NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecCode = GetStaCodeFromStaName(NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecSecName) & "-" & _
                                                                                               GetStaCodeFromStaName(NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecFirName)
                    End If
                Next i
            Next
        Catch
        End Try
    End Sub

    Public Function IfSameDiagramName(ByVal sName As String) As Boolean
        IfSameDiagramName = False
        Dim i As Integer
        TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Clear()
        TMSlocalDataSet.Fill("TMS_TRAINDIAGRAMINFO")
        For i = 1 To TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Rows.Count
            If TMSlocalDataSet.TMS_TRAINDIAGRAMINFO.Rows(i - 1).Item("TIMETABLENAME") = sName Then
                Return True
                Exit For
            End If
        Next
    End Function

    Dim TranDiagramTime As Generic.List(Of typeTrainDiagramTime)

    ''' <summary>
    ''' 画运行线
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DrawTranDiagram(ByVal StartTime As Date, ByVal EndTime As Date, ByVal picDraw As PictureBox, ByVal StaBrush As Brush, ByVal TimeLinePen As Pen, ByVal TrainLinePen As Pen, ByVal TranTimeLine As Generic.List(Of typeTrainDiagramTime))

        Dim i As Integer
        Dim nStaNum As Integer
        nStaNum = TranTimeLine.Count
        Dim nHeight As Single
        Dim nWidth As Single
        Dim nEveWidth As Single
        Dim nMinTime As Date
        Dim nMaxTime As Date
        Dim nTimeWidth As Integer
        Dim nMinute As Integer
        Dim nFirTime As Date
        Dim nEndTime As Date
        nMinTime = Date.MaxValue
        nMaxTime = Date.MinValue


        If nMinTime = Date.MinValue Then Exit Sub
        If nMaxTime = Date.MaxValue Then Exit Sub

        '为运行图的起终时间赋值，并向前向后各移5分钟
        nFirTime = StartTime '.AddSeconds(-StartTime.Second - 60 - 300)
        nEndTime = EndTime '.AddSeconds(60 - EndTime.Second + 300)

        nTimeWidth = DateDiff(DateInterval.Second, nFirTime, nEndTime)
        nMinute = Math.Round(nTimeWidth / 60)

        Dim j As Integer
        Dim rbmpGraphics As Graphics
        Dim rbmP As Bitmap
        Dim nLeft As Single
        Dim nTop As Single
        Dim nEveryHeight As Single
        Dim TextWidth As Single
        nEveryHeight = 20
        nLeft = 10
        nTop = 30
        TextWidth = 60
        Dim PrintStringTextWidth As Single
        Dim PrintStringTextHeight As Single
        rbmP = New Bitmap(picDraw.Width, picDraw.Height)
        rbmpGraphics = Graphics.FromImage(rbmP)
        Dim X1, X2, Y1, Y2 As Single
        Dim strTimePrint As String
        Dim intFirstTime As Integer
        intFirstTime = nFirTime.Hour * 3600 + nFirTime.Minute * 60

        nHeight = (picDraw.Height - nTop * 2) / ((nStaNum - 1) * 3 + 2)
        nWidth = picDraw.Width - nLeft * 2 - TextWidth
        nEveWidth = nWidth / nMinute
        For j = 1 To TranTimeLine.Count
            Y1 = nTop + (j - 1) * nHeight * 3
            Y2 = Y1 + nHeight * 2
            For i = 1 To nMinute + 1
                X1 = nLeft + TextWidth + (i - 1) * (picDraw.Width - nLeft * 2 - TextWidth) / (nMinute)
                X2 = X1
                rbmpGraphics.DrawLine(TimeLinePen, X1, Y1, X2, Y2)
                If j = 1 Then
                    If (i - 1) Mod 5 = 0 Then
                        strTimePrint = dTime(nFirTime.AddMinutes(i - 1).Hour * 3600 + nFirTime.AddMinutes(i - 1).Minute * 60, 2)
                        'Else
                        '    strTimePrint = Trim(((intFirstTime + Int((i - 1) / 6)) Mod 24) & ":" & ((i - 1) Mod 6) & "0")
                        rbmpGraphics.DrawString(strTimePrint, New Font("Arial", 9), Brushes.Black, X1 - 15, Y1 - 17)
                        rbmpGraphics.DrawString(strTimePrint, New Font("Arial", 9), Brushes.Black, X1 - 15, picDraw.Height - nTop + 2)
                    End If
                End If
            Next
        Next

        Dim nTmp As Integer
        Dim tmpFont As New Font("Arial", 9)
        For i = 1 To TranTimeLine.Count * 3
            X1 = nLeft + TextWidth
            X2 = X1 + picDraw.Width - nLeft * 2 - TextWidth
            Y1 = nTop + nHeight * (i - 1)
            Y2 = Y1
            rbmpGraphics.DrawLine(TimeLinePen, X1, Y1, X2, Y2)
            If i Mod 3 = 0 Then
                nTmp = i / 3 - 1
                strTimePrint = TranTimeLine(nTmp).sStaName
                PrintStringTextWidth = rbmpGraphics.MeasureString(strTimePrint, tmpFont).Width
                PrintStringTextHeight = rbmpGraphics.MeasureString(strTimePrint, tmpFont).Height
                rbmpGraphics.DrawString(strTimePrint, tmpFont, StaBrush, X1 - 5 - PrintStringTextWidth, Y1 - nHeight - PrintStringTextHeight / 2)

                strTimePrint = TranTimeLine(nTmp).sUpStaName
                PrintStringTextWidth = rbmpGraphics.MeasureString(strTimePrint, tmpFont).Width
                PrintStringTextHeight = rbmpGraphics.MeasureString(strTimePrint, tmpFont).Height
                rbmpGraphics.DrawString(strTimePrint, tmpFont, StaBrush, X1 - 5 - PrintStringTextWidth, Y1 - nHeight * 2 - PrintStringTextHeight / 2)

                strTimePrint = TranTimeLine(nTmp).sNextStaName
                PrintStringTextWidth = rbmpGraphics.MeasureString(strTimePrint, tmpFont).Width
                PrintStringTextHeight = rbmpGraphics.MeasureString(strTimePrint, tmpFont).Height
                rbmpGraphics.DrawString(strTimePrint, tmpFont, StaBrush, X1 - 5 - PrintStringTextWidth, Y1 - PrintStringTextHeight / 2)
            End If
        Next

        Dim m As Integer

        For i = 1 To TranTimeLine.Count
            If TranTimeLine(i - 1).sUpStaName <> "" Then
                If IsNothing(TranTimeLine(i - 1).DownStartTime1) = False Then
                    For m = 1 To TranTimeLine(i - 1).DownStartTime1.GetUpperBound(0)
                        If TranTimeLine(i - 1).DownStartTime1(m) <> Date.MinValue Then
                            If TranTimeLine(i - 1).DownEndTime1(m) <> Date.MinValue Then
                                X1 = GetCurXCordFromTime(TranTimeLine(i - 1).DownStartTime1(m), nFirTime, nLeft + TextWidth, nWidth, nEveWidth, nTimeWidth)
                                X2 = GetCurXCordFromTime(TranTimeLine(i - 1).DownEndTime1(m), nFirTime, nLeft + TextWidth, nWidth, nEveWidth, nTimeWidth)
                                Y1 = nTop + nHeight * (i * 3 - 3)
                                Y2 = nTop + nHeight * (i * 3 - 2)
                                rbmpGraphics.DrawLine(TrainLinePen, X1, Y1, X2, Y2)
                            End If
                        End If
                    Next m
                End If
            End If

            If TranTimeLine(i - 1).sNextStaName <> "" Then
                If IsNothing(TranTimeLine(i - 1).DownStartTime2) = False Then
                    For m = 1 To TranTimeLine(i - 1).DownStartTime2.GetUpperBound(0)
                        If TranTimeLine(i - 1).DownStartTime2(m) <> Date.MinValue Then
                            If TranTimeLine(i - 1).DownEndTime2(m) <> Date.MinValue Then
                                X1 = GetCurXCordFromTime(TranTimeLine(i - 1).DownStartTime2(m), nFirTime, nLeft + TextWidth, nWidth, nEveWidth, nTimeWidth)
                                X2 = GetCurXCordFromTime(TranTimeLine(i - 1).DownEndTime2(m), nFirTime, nLeft + TextWidth, nWidth, nEveWidth, nTimeWidth)
                                Y1 = nTop + nHeight * (i * 3 - 2)
                                Y2 = nTop + nHeight * (i * 3 - 1)
                                rbmpGraphics.DrawLine(TrainLinePen, X1, Y1, X2, Y2)
                            End If
                        End If
                    Next m
                End If
            End If

            'Up LIne
            If TranTimeLine(i - 1).sNextStaName <> "" Then
                If IsNothing(TranTimeLine(i - 1).UpStartTime1) = False Then
                    For m = 1 To TranTimeLine(i - 1).UpStartTime1.GetUpperBound(0)
                        If TranTimeLine(i - 1).UpStartTime1(m) <> Date.MinValue Then
                            If TranTimeLine(i - 1).UpEndTime1(m) <> Date.MinValue Then
                                X1 = GetCurXCordFromTime(TranTimeLine(i - 1).UpStartTime1(m), nFirTime, nLeft + TextWidth, nWidth, nEveWidth, nTimeWidth)
                                X2 = GetCurXCordFromTime(TranTimeLine(i - 1).UpEndTime1(m), nFirTime, nLeft + TextWidth, nWidth, nEveWidth, nTimeWidth)
                                Y1 = nTop + nHeight * (i * 3 - 1)
                                Y2 = nTop + nHeight * (i * 3 - 2)
                                rbmpGraphics.DrawLine(TrainLinePen, X1, Y1, X2, Y2)
                            End If

                        End If
                    Next m
                End If
            End If

            If TranTimeLine(i - 1).sUpStaName <> "" Then
                If IsNothing(TranTimeLine(i - 1).UpStartTime2) = False Then
                    For m = 1 To TranTimeLine(i - 1).UpStartTime2.GetUpperBound(0)
                        If TranTimeLine(i - 1).UpStartTime2(m) <> Date.MinValue Then
                            If TranTimeLine(i - 1).UpEndTime2(m) <> Date.MinValue Then
                                X1 = GetCurXCordFromTime(TranTimeLine(i - 1).UpStartTime2(m), nFirTime, nLeft + TextWidth, nWidth, nEveWidth, nTimeWidth)
                                X2 = GetCurXCordFromTime(TranTimeLine(i - 1).UpEndTime2(m), nFirTime, nLeft + TextWidth, nWidth, nEveWidth, nTimeWidth)
                                Y1 = nTop + nHeight * (i * 3 - 2)
                                Y2 = nTop + nHeight * (i * 3 - 3)
                                rbmpGraphics.DrawLine(TrainLinePen, X1, Y1, X2, Y2)
                            End If
                        End If
                    Next m
                End If
            End If
        Next
        picDraw.Image = rbmP

    End Sub

    ''' <summary>
    ''' 由时间得到X坐标
    ''' </summary>
    ''' <param name="sCurTime"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCurXCordFromTime(ByVal sCurTime As Date, ByVal dFirTime As Date, ByVal nLeft As Single, ByVal nTolWidth As Single, ByVal nEveWidth As Single, ByVal nTimeWidth As Integer) As Single
        Dim nSecondWidth As Single
        nSecondWidth = nTolWidth / nTimeWidth
        Dim nTime As Integer
        nTime = DateDiff(DateInterval.Second, dFirTime, sCurTime)
        GetCurXCordFromTime = nLeft + nTime * nSecondWidth

    End Function
    ''' <summary>
    ''' 求两个时间的最小时间
    ''' </summary>
    ''' <param name="Time1"></param>
    ''' <param name="Time2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMinimalTime(ByVal Time1 As Date, ByVal Time2 As Date) As Date
        If Time2 = Date.MinValue Then
            GetMinimalTime = Time1
        Else
            If DateDiff(DateInterval.Second, Time1, Time2) > 0 Then
                GetMinimalTime = Time1
            Else
                GetMinimalTime = Time2
            End If
        End If
    End Function

    ''' <summary>
    ''' 得到两个时间的最大时间
    ''' </summary>
    ''' <param name="Time1"></param>
    ''' <param name="Time2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMaximalTime(ByVal Time1 As Date, ByVal Time2 As Date) As Date
        If Time2 = Date.MaxValue Then
            GetMaximalTime = Time1
        Else
            If DateDiff(DateInterval.Second, Time1, Time2) < 0 Then
                GetMaximalTime = Time1
            Else
                GetMaximalTime = Time2
            End If
        End If
    End Function

    '年月日保存
    Public Function DateFormat(ByVal CurDate As Date) As String
        Dim sYear As String
        Dim sMouth As String
        Dim sDay As String
        sYear = CurDate.Year.ToString
        sMouth = CurDate.Month.ToString
        sDay = CurDate.Day.ToString
        If sMouth.Length = 1 Then
            sMouth = 0 & sMouth
        End If
        If sDay.Length = 1 Then
            sDay = 0 & sDay
        End If
        DateFormat = sYear & sMouth & sDay
    End Function

    Public Sub OutPutToEXCELFileFormDataGrid(ByVal ExcelTitle As String, ByVal Dtg As DataGridView, ByVal frmForm As Form)
        Try

            If Dtg.RowCount > 0 Then
                frmForm.Cursor = Cursors.WaitCursor
                Dim i, j, p As Integer
                Dim rows As Integer = Dtg.Rows.Count
                Dim cols As Integer = Dtg.ColumnCount
                Dim DataArray(rows - 1, cols - 1) As String
                Dim myExcel As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application
                Dim myBook As Microsoft.Office.Interop.Excel.Workbook
                Dim mySheet As Microsoft.Office.Interop.Excel.Worksheet
                myBook = myExcel.Workbooks.Add     '添加一个新的BOOK

                mySheet = myBook.Worksheets.Add     '添加一个新的SHEET
                'myExcel.Caption = ExcelTitle
                mySheet.Name = ExcelTitle
                mySheet.Cells.NumberFormatLocal = "@"

                For i = 0 To rows - 1
                    For j = 0 To cols - 1
                        DataArray(i, j) = Dtg.Rows(i).Cells(j).Value
                    Next
                Next

                For j = 0 To cols - 1
                    myExcel.Cells(1, j + 1) = Dtg.Columns(j).HeaderText '.name
                Next
                mySheet.Range("A2").Resize(rows, cols).Value = DataArray

                'For p = 1 To rows
                '    For q = 1 To rows
                '        If Val(DataArray(p, q)) > 0 Then
                '            myExcel.Cells(p + 1, q + 1).Interior.ColorIndex = Val(DataArray(p, q)) + 1
                '        End If
                '    Next
                'Next

                For p = 1 To cols
                    mySheet.Columns(p).EntireColumn.AutoFit()
                Next p

                'For p = 2 To cols
                '    myExcel.Columns(p).ColumnWidth = 3
                'Next p
                myExcel.Visible = True
                'myExcel.Caption = "aaa"
                GC.Collect()
            Else
                MessageBox.Show("没有数据!", frmForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch exp As Exception
            MessageBox.Show("数据导出失败!请查看是否已经安装了Excel", frmForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Finally
            frmForm.Cursor = Cursors.Default
        End Try

    End Sub
End Module
