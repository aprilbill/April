
Module mdlNetMainSub
    'Public Sub readNetStaAndSecDataByDAO(ByVal strPath As String, ByVal strDataBaseName As String)
    '    '读入线网的数据
    '    '****************以下为DAO代码
    '    '创建一个OledbConnection


    '    Dim myWS As DAO.Workspace
    '    Dim DE As DAO.DBEngine = New DAO.DBEngine
    '    myWS = DE.Workspaces(0)
    '    Dim dFile As DAO.Database
    '    Dim sFile As DAO.Recordset
    '    Dim i, j As Integer
    '    dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor) 'myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor) '(g_strNetMainPath, False, False, ";PWD=admin")
    '    sFile = dFile.OpenRecordset("select * from 底图结构 where 是否默认='1' order by 站序")
    '    Dim nNum As Integer
    '    nNum = 0
    '    If sFile.RecordCount > 0 Then
    '        sFile.MoveLast()
    '        nNum = sFile.RecordCount
    '    End If
    '    ReDim StationInf(nNum)
    '    If nNum > 0 Then
    '        '导入至stationInf
    '        sFile.MoveFirst()
    '        For i = 1 To nNum
    '            StationInf(i).sStationName = sFile("站名").Value.ToString.Trim  'Trim(myTable2.Rows(i - 1).Item("站名"))
    '            StationInf(i).sAtLineName = sFile("线路名").Value.ToString.Trim 'Trim(myTable2.Rows(i - 1).Item("线路名"))
    '            StationInf(i).Ycord = sFile("Y坐标").Value 'Trim(myTable2.Rows(i - 1).Item("Y坐标"))
    '            sFile.MoveNext()
    '        Next i
    '    Else
    '        MsgBox("底图结构没有设置或者没有设置默认的底图结构!","提示")
    '        TimeTablePara.sInputDataError = "底图结构设置错误码"
    '        Exit Sub
    '    End If

    '    ReDim NotSameStationInf(0)
    '    Dim nMark As Integer
    '    nMark = 0
    '    For i = 1 To UBound(StationInf)
    '        nMark = 0
    '        For j = 1 To UBound(NotSameStationInf)
    '            If NotSameStationInf(j) = StationInf(i).sStationName Then
    '                nMark = 1
    '                Exit For
    '            End If
    '        Next j
    '        If nMark = 0 Then
    '            ReDim Preserve NotSameStationInf(UBound(NotSameStationInf) + 1)
    '            NotSameStationInf(UBound(NotSameStationInf)) = StationInf(i).sStationName
    '        End If
    '    Next

    '    sFile = dFile.OpenRecordset("select * from 线路区间信息 order by 区间编号")
    '    Dim sFirSta As String
    '    Dim sSecSta As String
    '    Dim sSecName As String
    '    ReDim SectionInf(0)
    '    nNum = 0
    '    If sFile.RecordCount > 0 Then
    '        sFile.MoveLast()
    '        nNum = sFile.RecordCount
    '    End If

    '    For i = 1 To UBound(StationInf) - 1
    '        sFirSta = StationInf(i).sStationName
    '        sSecSta = StationInf(i + 1).sStationName
    '        sSecName = sFirSta & "->" & sSecSta
    '        If nNum > 0 Then
    '            sFile.MoveFirst()
    '            For j = 1 To nNum
    '                If sFile("区间名称").Value.ToString.Trim = sSecName Then
    '                    ReDim Preserve SectionInf(UBound(SectionInf) + 1)
    '                    ReDim SectionInf(UBound(SectionInf)).lDistance(2)
    '                    SectionInf(UBound(SectionInf)).nSecNumber = sFile("区间编号").Value 'myTable3.Rows(j - 1).Item("区间编号")
    '                    SectionInf(UBound(SectionInf)).sSecName = sSecName
    '                    SectionInf(UBound(SectionInf)).sLineName = sFile("线路名称").Value
    '                    SectionInf(UBound(SectionInf)).nSection = Val(sFile("正线数目").Value.ToString.Trim)  ' Val(myTable3.Rows(j - 1).Item("正线数目").ToString)
    '                    SectionInf(UBound(SectionInf)).sBlock = sFile("闭塞类型").Value.ToString.Trim 'myTable3.Rows(j - 1).Item("闭塞类型").ToString
    '                    SectionInf(UBound(SectionInf)).sSecFirName = sFirSta
    '                    SectionInf(UBound(SectionInf)).sSecSecName = sSecSta
    '                    SectionInf(UBound(SectionInf)).nFirStaID = i 'FromStaNameToStaIDByStationinf(sFirSta)
    '                    SectionInf(UBound(SectionInf)).nSecStaID = i + 1 'FromStaNameToStaIDByStationinf(sSecSta)
    '                    SectionInf(UBound(SectionInf)).nHStation = i 'FromStaNameToStaIDByStationinf(sFirSta)
    '                    SectionInf(UBound(SectionInf)).nQStation = i + 1 'FromStaNameToStaIDByStationinf(sSecSta)
    '                    SectionInf(UBound(SectionInf)).lDistance(1) = sFile("下行区间距离").Value 'myTable3.Rows(j - 1).Item("下行区间距离")
    '                    SectionInf(UBound(SectionInf)).lDistance(2) = sFile("上行区间距离").Value 'myTable3.Rows(j - 1).Item("上行区间距离")
    '                    Exit For
    '                End If
    '                sFile.MoveNext()
    '            Next
    '        End If
    '    Next


    '    '区间标尺
    '    Dim strSecName As String
    '    For i = 1 To UBound(SectionInf)
    '        strSecName = SectionInf(i).sSecName
    '        sFile = dFile.OpenRecordset("select * from 列车运行标尺 where 区间名称='" & strSecName & "' order by 标尺编号")
    '        nNum = 0
    '        If sFile.RecordCount > 0 Then
    '            sFile.MoveLast()
    '            nNum = sFile.RecordCount
    '        End If
    '        ReDim Preserve SectionInf(i).SecScale(nNum)
    '        If nNum > 0 Then
    '            sFile.MoveFirst()
    '            For j = 1 To nNum
    '                SectionInf(i).SecScale(j).nID = sFile("标尺编号").Value ' myTable4.Rows(j - 1).Item("标尺编号")
    '                SectionInf(i).SecScale(j).sScaleName = sFile("标尺名称").Value.ToString.Trim  '  myTable4.Rows(j - 1).Item("标尺名称").ToString.Trim
    '                SectionInf(i).SecScale(j).sngDownTime = MinuteToSecond(sFile("下行运行时分").Value.ToString.Trim) ' MinuteToSecond(myTable4.Rows(j - 1).Item("下行运行时分").ToString.Trim)
    '                SectionInf(i).SecScale(j).sngUpTime = MinuteToSecond(sFile("上行运行时分").Value.ToString.Trim) '  MinuteToSecond(myTable4.Rows(j - 1).Item("上行运行时分").ToString.Trim)
    '                SectionInf(i).SecScale(j).sngDownAppendStartTime = MinuteToSecond(sFile("下行起车时分").Value.ToString.Trim) ' MinuteToSecond(myTable4.Rows(j - 1).Item("下行起车时分").ToString.Trim)
    '                SectionInf(i).SecScale(j).sngDownAppendStopTime = MinuteToSecond(sFile("下行停车时分").Value.ToString.Trim) '  MinuteToSecond(myTable4.Rows(j - 1).Item("下行停车时分").ToString.Trim)
    '                SectionInf(i).SecScale(j).sngUpAppendStartTime = MinuteToSecond(sFile("上行起车时分").Value.ToString.Trim) ' MinuteToSecond(myTable4.Rows(j - 1).Item("上行起车时分").ToString.Trim)
    '                SectionInf(i).SecScale(j).sngUpAppendStopTime = MinuteToSecond(sFile("上行停车时分").Value.ToString.Trim) ' MinuteToSecond(myTable4.Rows(j - 1).Item("上行停车时分").ToString.Trim)
    '                sFile.MoveNext()
    '            Next
    '        End If

    '    Next

    '    '区间运行曲线
    '    Dim sUpOrDown As String
    '    For i = 1 To UBound(SectionInf)
    '        strSecName = SectionInf(i).sSecName
    '        sUpOrDown = "下行"
    '        sFile = dFile.OpenRecordset("select * from 区间时间距离曲线 where 区间名称='" & strSecName & "' and 上下行='" & sUpOrDown & "' order by 分段ID")
    '        nNum = 0
    '        If sFile.RecordCount > 0 Then
    '            sFile.MoveLast()
    '            nNum = sFile.RecordCount
    '        End If

    '        ReDim Preserve SectionInf(i).SecTimeSpace.DownID(nNum)
    '        ReDim Preserve SectionInf(i).SecTimeSpace.DownLength(nNum)
    '        ReDim Preserve SectionInf(i).SecTimeSpace.DownRunTime(nNum)
    '        If nNum > 0 Then
    '            sFile.MoveFirst()
    '            For j = 1 To nNum
    '                SectionInf(i).SecTimeSpace.DownID(j) = sFile("分段ID").Value 'myTable4.Rows(j - 1).Item("分段ID")
    '                SectionInf(i).SecTimeSpace.DownLength(j) = sFile("分段距离").Value ' myTable4.Rows(j - 1).Item("分段距离")
    '                SectionInf(i).SecTimeSpace.DownRunTime(j) = MinuteToSecond(sFile("分段时间").Value.ToString.Trim) 'MinuteToSecond(myTable4.Rows(j - 1).Item("分段时间"))
    '                sFile.MoveNext()
    '            Next
    '        End If


    '        strSecName = SectionInf(i).sSecName
    '        sUpOrDown = "上行"

    '        sFile = dFile.OpenRecordset("select * from 区间时间距离曲线 where 区间名称='" & strSecName & "' and 上下行='" & sUpOrDown & "' order by 分段ID")
    '        nNum = 0
    '        If sFile.RecordCount > 0 Then
    '            sFile.MoveLast()
    '            nNum = sFile.RecordCount
    '        End If

    '        ReDim Preserve SectionInf(i).SecTimeSpace.UpID(nNum)
    '        ReDim Preserve SectionInf(i).SecTimeSpace.UpLength(nNum)
    '        ReDim Preserve SectionInf(i).SecTimeSpace.UpRunTime(nNum)
    '        If nNum > 0 Then
    '            sFile.MoveFirst()
    '            For j = 1 To nNum
    '                SectionInf(i).SecTimeSpace.UpID(j) = sFile("分段ID").Value 'myTable5.Rows(j - 1).Item("分段ID")
    '                SectionInf(i).SecTimeSpace.UpLength(j) = sFile("分段距离").Value 'myTable5.Rows(j - 1).Item("分段距离")
    '                SectionInf(i).SecTimeSpace.UpRunTime(j) = MinuteToSecond(sFile("分段时间").Value.ToString.Trim) ' MinuteToSecond(myTable5.Rows(j - 1).Item("分段时间"))
    '                sFile.MoveNext()
    '            Next
    '        End If
    '    Next
    '    dFile.Close()
    'End Sub

    'Public Sub readNetStaAndSecData(ByVal strPath As String, ByVal strDataBaseName As String)
    '    '读入线网的数据
    '    '****************以下为ADO代码
    '    '创建一个OledbConnection
    '    'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & strPath & "'"
    '    Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)

    '    Dim i As Integer
    '    Dim j As Integer

    '    ''车站信息
    '    Dim strTable2 As String = "select * from 底图结构 where 是否默认='1' order by 站序"
    '    Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
    '    '创建一个Datasetd
    '    Dim myDataSet2 As Data.DataSet = New Data.DataSet
    '    Mydc2.Fill(myDataSet2, "底图结构")
    '    Dim myTable2 As Data.DataTable
    '    myTable2 = myDataSet2.Tables("底图结构")

    '    '导入至stationInf
    '    ReDim StationInf(myTable2.Rows.Count)
    '    For i = 1 To myTable2.Rows.Count
    '        StationInf(i).sStationName = Trim(myTable2.Rows(i - 1).Item("站名"))
    '        StationInf(i).sAtLineName = Trim(myTable2.Rows(i - 1).Item("线路名"))
    '        StationInf(i).sPrintStaName = FromStaNameToPrintStaName(StationInf(i).sStationName, StationInf(i).sAtLineName)
    '        'StationInf(i).sStaStyle = Trim(myTable2.Rows(i - 1).Item("车站类型"))
    '        StationInf(i).sStationProp = Trim(myTable2.Rows(i - 1).Item("备注").ToString)
    '        StationInf(i).Ycord = Trim(myTable2.Rows(i - 1).Item("Y坐标"))
    '    Next

    '    ReDim NotSameStationInf(0)
    '    Dim nMark As Integer
    '    nMark = 0
    '    For i = 1 To UBound(StationInf)
    '        nMark = 0
    '        For j = 1 To UBound(NotSameStationInf)
    '            If NotSameStationInf(j) = StationInf(i).sStationName Then
    '                nMark = 1
    '                Exit For
    '            End If
    '        Next j
    '        If nMark = 0 Then
    '            ReDim Preserve NotSameStationInf(UBound(NotSameStationInf) + 1)
    '            NotSameStationInf(UBound(NotSameStationInf)) = StationInf(i).sStationName
    '        End If
    '    Next

    '    ''区间信息
    '    Dim strTable3 As String = "select * from 线路区间信息 order by 区间编号"
    '    Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
    '    '创建一个Datasetd
    '    Dim myDataSet3 As Data.DataSet = New Data.DataSet
    '    Mydc3.Fill(myDataSet3, "线路区间信息")
    '    Dim myTable3 As Data.DataTable
    '    myTable3 = myDataSet3.Tables("线路区间信息")
    '    Dim sFirSta As String
    '    Dim sSecSta As String
    '    Dim sSecName As String
    '    ReDim SectionInf(0)
    '    For i = 1 To UBound(StationInf) - 1
    '        sFirSta = StationInf(i).sStationName
    '        sSecSta = StationInf(i + 1).sStationName
    '        sSecName = sFirSta & "->" & sSecSta
    '        For j = 1 To myTable3.Rows.Count
    '            If Trim(myTable3.Rows(j - 1).Item("区间名称")) = sSecName Then
    '                ReDim Preserve SectionInf(UBound(SectionInf) + 1)
    '                ReDim SectionInf(UBound(SectionInf)).lDistance(2)
    '                SectionInf(UBound(SectionInf)).nSecNumber = myTable3.Rows(j - 1).Item("区间编号")
    '                SectionInf(UBound(SectionInf)).sSecName = sSecName
    '                SectionInf(UBound(SectionInf)).nSection = Val(myTable3.Rows(j - 1).Item("正线数目").ToString)
    '                SectionInf(UBound(SectionInf)).sBlock = myTable3.Rows(j - 1).Item("闭塞类型").ToString
    '                SectionInf(UBound(SectionInf)).sSecFirName = sFirSta
    '                SectionInf(UBound(SectionInf)).sSecSecName = sSecName
    '                SectionInf(UBound(SectionInf)).nFirStaID = i 'FromStaNameToStaIDByStationinf(sFirSta)
    '                SectionInf(UBound(SectionInf)).nSecStaID = i + 1 'FromStaNameToStaIDByStationinf(sSecSta)
    '                SectionInf(UBound(SectionInf)).nHStation = i 'FromStaNameToStaIDByStationinf(sFirSta)
    '                SectionInf(UBound(SectionInf)).nQStation = i + 1 'FromStaNameToStaIDByStationinf(sSecSta)
    '                SectionInf(UBound(SectionInf)).lDistance(1) = myTable3.Rows(j - 1).Item("下行区间距离")
    '                SectionInf(UBound(SectionInf)).lDistance(2) = myTable3.Rows(j - 1).Item("上行区间距离")
    '                Exit For
    '            End If
    '        Next
    '    Next

    '    '区间标尺
    '    Dim strSecName As String
    '    ' Dim nSecXuHao As Integer
    '    For i = 1 To UBound(SectionInf)
    '        strSecName = SectionInf(i).sSecName
    '        'nSecXuHao = SectionInf(i).nSecNumber
    '        Dim strTable4 As String = "select * from 列车运行标尺 where 区间名称='" & strSecName & "' order by 标尺编号"
    '        ' Dim strTable4 As String = "select * from 列车运行标尺 where 区间序号=" & nSecXuHao & " order by 标尺编号"
    '        Dim Mydc4 As New Data.OleDb.OleDbDataAdapter(strTable4, strCon)
    '        '创建一个Datasetd
    '        Dim myDataSet4 As Data.DataSet = New Data.DataSet
    '        Mydc4.Fill(myDataSet4, "列车运行标尺")
    '        Dim myTable4 As Data.DataTable
    '        myTable4 = myDataSet4.Tables("列车运行标尺")

    '        ReDim Preserve SectionInf(i).SecScale(myTable4.Rows.Count)
    '        For j = 1 To myTable4.Rows.Count
    '            SectionInf(i).SecScale(j).nID = myTable4.Rows(j - 1).Item("标尺编号")
    '            SectionInf(i).SecScale(j).sScaleName = myTable4.Rows(j - 1).Item("标尺名称").ToString.Trim
    '            SectionInf(i).SecScale(j).sngDownTime = MinuteToSecond(myTable4.Rows(j - 1).Item("下行运行时分").ToString.Trim)
    '            SectionInf(i).SecScale(j).sngUpTime = MinuteToSecond(myTable4.Rows(j - 1).Item("上行运行时分").ToString.Trim)
    '            SectionInf(i).SecScale(j).sngDownAppendStartTime = MinuteToSecond(myTable4.Rows(j - 1).Item("下行起车时分").ToString.Trim)
    '            SectionInf(i).SecScale(j).sngDownAppendStopTime = MinuteToSecond(myTable4.Rows(j - 1).Item("下行停车时分").ToString.Trim)
    '            SectionInf(i).SecScale(j).sngUpAppendStartTime = MinuteToSecond(myTable4.Rows(j - 1).Item("上行起车时分").ToString.Trim)
    '            SectionInf(i).SecScale(j).sngUpAppendStopTime = MinuteToSecond(myTable4.Rows(j - 1).Item("上行停车时分").ToString.Trim)
    '        Next
    '    Next

    '    '区间运行曲线
    '    Dim sUpOrDown As String
    '    For i = 1 To UBound(SectionInf)
    '        strSecName = SectionInf(i).sSecName
    '        sUpOrDown = "下行"
    '        Dim strTable4 As String = "select * from 区间时间距离曲线 where 区间名称='" & strSecName & "' and 上下行='" & sUpOrDown & "' order by 分段ID"
    '        Dim Mydc4 As New Data.OleDb.OleDbDataAdapter(strTable4, strCon)
    '        '创建一个Datasetd
    '        Dim myDataSet4 As Data.DataSet = New Data.DataSet
    '        Mydc4.Fill(myDataSet4, "区间时间距离曲线")
    '        Dim myTable4 As Data.DataTable
    '        myTable4 = myDataSet4.Tables("区间时间距离曲线")

    '        ReDim Preserve SectionInf(i).SecTimeSpace.DownID(myTable4.Rows.Count)
    '        ReDim Preserve SectionInf(i).SecTimeSpace.DownLength(myTable4.Rows.Count)
    '        ReDim Preserve SectionInf(i).SecTimeSpace.DownRunTime(myTable4.Rows.Count)
    '        For j = 1 To myTable4.Rows.Count
    '            SectionInf(i).SecTimeSpace.DownID(j) = myTable4.Rows(j - 1).Item("分段ID")
    '            SectionInf(i).SecTimeSpace.DownLength(j) = myTable4.Rows(j - 1).Item("分段距离")
    '            SectionInf(i).SecTimeSpace.DownRunTime(j) = MinuteToSecond(myTable4.Rows(j - 1).Item("分段时间"))
    '        Next

    '        strSecName = SectionInf(i).sSecName
    '        sUpOrDown = "上行"
    '        Dim strTable5 As String = "select * from 区间时间距离曲线 where 区间名称='" & strSecName & "' and 上下行='" & sUpOrDown & "' order by 分段ID"
    '        Dim Mydc5 As New Data.OleDb.OleDbDataAdapter(strTable5, strCon)
    '        '创建一个Datasetd
    '        Dim myDataSet5 As Data.DataSet = New Data.DataSet
    '        Mydc5.Fill(myDataSet5, "区间时间距离曲线")
    '        Dim myTable5 As Data.DataTable
    '        myTable5 = myDataSet5.Tables("区间时间距离曲线")

    '        ReDim Preserve SectionInf(i).SecTimeSpace.UpID(myTable5.Rows.Count)
    '        ReDim Preserve SectionInf(i).SecTimeSpace.UpLength(myTable5.Rows.Count)
    '        ReDim Preserve SectionInf(i).SecTimeSpace.UpRunTime(myTable5.Rows.Count)
    '        For j = 1 To myTable4.Rows.Count
    '            SectionInf(i).SecTimeSpace.UpID(j) = myTable5.Rows(j - 1).Item("分段ID")
    '            SectionInf(i).SecTimeSpace.UpLength(j) = myTable5.Rows(j - 1).Item("分段距离")
    '            SectionInf(i).SecTimeSpace.UpRunTime(j) = MinuteToSecond(myTable5.Rows(j - 1).Item("分段时间"))
    '        Next
    '    Next
    '    MyConn.Close()
    'End Sub

    '由站名获得打印站名
    Public Function FromStaNameToPrintStaName(ByVal sStaName As String, ByVal sLineName As String) As String
        Dim i As Integer
        Dim j As Integer
        FromStaNameToPrintStaName = ""
        For i = 1 To UBound(NetInf.Line)
            If NetInf.Line(i).sName = sLineName Then
                For j = 1 To UBound(NetInf.Line(i).Station)
                    If NetInf.Line(i).Station(j).sStaName = sStaName Then
                        FromStaNameToPrintStaName = NetInf.Line(i).Station(j).sPrintStaName
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next
    End Function

    ''读入时刻表车站顺序
    'Public Sub ReadSKBStaSeqData()
    '    Dim i As Integer
    '    Dim j As Integer
    '    Dim k As Integer
    '    ReDim SkbStnSeq(0)
    '    Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
    '    Dim strTable2 As String = "select distinct  区段名称 from 时刻表车站顺序"
    '    Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
    '    Dim myDataSet2 As Data.DataSet = New Data.DataSet
    '    Mydc2.Fill(myDataSet2, "时刻表车站顺序")
    '    Dim myTable2 As Data.DataTable
    '    myTable2 = myDataSet2.Tables("时刻表车站顺序")
    '    ReDim SkbStnSeq(myTable2.Rows.Count)
    '    For i = 1 To myTable2.Rows.Count
    '        SkbStnSeq(i).sQDName = (Trim(myTable2.Rows(i - 1).Item("区段名称")))
    '    Next

    '    For j = 1 To UBound(SkbStnSeq)
    '        Dim strTable3 As String = "select * from 时刻表车站顺序 where 区段名称='" & SkbStnSeq(j).sQDName & "' order by 序号 "
    '        Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
    '        Dim myDataSet3 As Data.DataSet = New Data.DataSet
    '        Mydc3.Fill(myDataSet3, "时刻表车站顺序")
    '        Dim myTable3 As Data.DataTable
    '        myTable3 = myDataSet3.Tables("时刻表车站顺序")
    '        ReDim Preserve SkbStnSeq(j).nStnSeq(myTable3.Rows.Count)
    '        For k = 1 To myTable3.Rows.Count
    '            SkbStnSeq(j).nStnSeq(k) = StaNameToStaInfID((Trim(myTable3.Rows(k - 1).Item("车站名称"))))
    '        Next
    '    Next
    '    MyConn.Close()
    'End Sub
    'Public Sub readNetData()
    '    '读入线网的数据
    '    '****************以下为ADO代码

    '    '创建一个OledbConnection
    '    Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
    '    Dim strTable As String = "select * from 路网线路信息 order by 序号"
    '    Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
    '    Dim myDataSet As Data.DataSet = New Data.DataSet
    '    Dim i, j As Integer
    '    Call redAllLineData(g_strNetMainPath, g_strDatabaseName)
    '    Dim ifSame As Int16
    '    ReDim StaInfNotSame(0)
    '    For i = 1 To UBound(StaInf)
    '        ifSame = 0
    '        For j = 1 To UBound(StaInfNotSame)
    '            If StaInf(i).sStaName = StaInfNotSame(j).sStaName Then
    '                ifSame = 1
    '                Exit For
    '            End If
    '        Next
    '        If ifSame = 0 Then
    '            ReDim Preserve StaInfNotSame(UBound(StaInfNotSame) + 1)
    '            StaInfNotSame(UBound(StaInfNotSame)).sStaName = StaInf(i).sStaName
    '            StaInfNotSame(UBound(StaInfNotSame)).sEngName = StaInf(i).sEngName
    '            StaInfNotSame(UBound(StaInfNotSame)).sAtLine = StaInf(i).sAtLine
    '            StaInfNotSame(UBound(StaInfNotSame)).nDownID = StaInf(i).nDownID
    '        End If
    '    Next

    '    ''区间信息
    '    Dim strTable3 As String = "select * from 线路区间信息 order by 线路名称,区间编号"
    '    Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
    '    '创建一个Datasetd
    '    Dim myDataSet3 As Data.DataSet = New Data.DataSet
    '    Mydc3.Fill(myDataSet3, "线路区间信息")
    '    Dim myTable3 As Data.DataTable
    '    myTable3 = myDataSet3.Tables("线路区间信息")
    '    For j = 1 To UBound(NetInf.Line)
    '        For i = 1 To myTable3.Rows.Count
    '            If Trim(myTable3.Rows(i - 1).Item("线路名称")) = NetInf.Line(j).sName Then
    '                ReDim Preserve NetInf.Line(j).Section(UBound(NetInf.Line(j).Section) + 1)
    '                NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecName = Trim(myTable3.Rows(i - 1).Item("区间名称"))
    '                NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).nID = myTable3.Rows(i - 1).Item("区间编号")
    '                NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecFirName = Trim(myTable3.Rows(i - 1).Item("区间起始站"))
    '                NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecSecName = Trim(myTable3.Rows(i - 1).Item("区间终到站"))
    '                NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecUpLength = Trim(myTable3.Rows(i - 1).Item("上行区间距离"))
    '                NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecDownLength = Trim(myTable3.Rows(i - 1).Item("下行区间距离"))
    '                NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecCode = GetStaCodeFromStaName(NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecSecName) & "-" & _
    '                                                                                 GetStaCodeFromStaName(NetInf.Line(j).Section(UBound(NetInf.Line(j).Section)).sSecFirName)

    '            End If
    '        Next i
    '    Next

    '    MyConn.Close()
    'End Sub

    '导入CADstainf()
    Public Sub InputStationAndSectionDataToCADStainf()
        Dim i, j As Integer
        Dim k As Integer
        '导入CADstainf()信息
        ReDim CADStaInf(0)
        Dim IFin As Integer
        For i = 1 To UBound(NetInf.Line)
            For j = UBound(NetInf.Line(i).Station) To 1 Step -1
                'If NetInf.Line(i).Station(j).sStaName = "浦东机场" Then Stop
                IFin = 0
                For k = 1 To UBound(CADStaInf)
                    If CADStaInf(k).sStaName = NetInf.Line(i).Station(j).sStaName Then
                        IFin = 1
                        Exit For
                    End If
                Next
                If IFin = 0 Then
                    ReDim Preserve CADStaInf(UBound(CADStaInf) + 1)
                    ReDim Preserve CADStaInf(UBound(CADStaInf)).Track(0)
                    ReDim Preserve CADStaInf(UBound(CADStaInf)).Signal(0)
                    ReDim Preserve CADStaInf(UBound(CADStaInf)).PlatForm(0)
                    ReDim Preserve CADStaInf(UBound(CADStaInf)).FontText(0)
                    ReDim Preserve CADStaInf(UBound(CADStaInf)).ContolScheme(0)
                    CADStaInf(UBound(CADStaInf)).sStaName = NetInf.Line(i).Station(j).sStaName
                    CADStaInf(UBound(CADStaInf)).sStaCode = NetInf.Line(i).Station(j).sStaCode
                    CADStaInf(UBound(CADStaInf)).nDownID = UBound(CADStaInf)
                    CADStaInf(UBound(CADStaInf)).nStaOrSecID = j
                    CADStaInf(UBound(CADStaInf)).nLineID = i
                    CADStaInf(UBound(CADStaInf)).sLineName = NetInf.Line(i).sName
                    CADStaInf(UBound(CADStaInf)).sStaOrSec = "车站"
                End If
                If j > 1 Then
                    ReDim Preserve CADStaInf(UBound(CADStaInf) + 1)
                    ReDim Preserve CADStaInf(UBound(CADStaInf)).Track(0)
                    ReDim Preserve CADStaInf(UBound(CADStaInf)).Signal(0)
                    ReDim Preserve CADStaInf(UBound(CADStaInf)).PlatForm(0)
                    ReDim Preserve CADStaInf(UBound(CADStaInf)).FontText(0)
                    ReDim Preserve CADStaInf(UBound(CADStaInf)).ContolScheme(0)
                    CADStaInf(UBound(CADStaInf)).sStaName = NetInf.Line(i).Section(j - 1).sSecName
                    CADStaInf(UBound(CADStaInf)).nDownID = UBound(CADStaInf)
                    CADStaInf(UBound(CADStaInf)).sStaCode = NetInf.Line(i).Section(j - 1).sSecCode
                    CADStaInf(UBound(CADStaInf)).nStaOrSecID = j
                    CADStaInf(UBound(CADStaInf)).nLineID = i
                    CADStaInf(UBound(CADStaInf)).sLineName = NetInf.Line(i).sName
                    CADStaInf(UBound(CADStaInf)).sStaOrSec = "区间"
                End If

            Next
        Next
    End Sub

    '将区间控制模块信息赋给区间信息中
    Public Sub InputControlModleInforInSectioninf()
        Dim i, j As Integer
        For i = 1 To UBound(SectionInf)
            For j = 1 To UBound(CADStaInf)
                If CADStaInf(j).sStaName = SectionInf(i).sSecName Then
                    SectionInf(i).sDownControlNum = CADStaInf(j).sDownControlNum
                    SectionInf(i).sUpControlNum = CADStaInf(j).sUpControlNum
                    Exit For
                End If
            Next
        Next
    End Sub
    Public Sub redAllLineData(ByVal strPath As String, ByVal strDataBaseName As String)
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & strPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim strTable As String = "select * from 路网线路信息 order by 序号"
        Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
        Dim myDataSet As Data.DataSet = New Data.DataSet
        Mydc.Fill(myDataSet, "路网线路信息")
        Dim myTable As Data.DataTable

        myTable = myDataSet.Tables("路网线路信息")
        Dim i As Integer
        Dim j As Integer
        Dim strTmpLineName As String
        Dim strTmpLineLength As Single
        Dim strMemo As String
        Dim intSeq As String
        Dim strEngName As String
        Dim strBrriName As String
        Dim sLineNumber As String

        ReDim NetInf.Line(myTable.Rows.Count)
        For i = 1 To myTable.Rows.Count
            strTmpLineName = myTable.Rows(i - 1).Item("线路名称")
            strTmpLineLength = myTable.Rows(i - 1).Item("线路总长") 'Trim(myRec.Fields("线路总长").Value)
            strMemo = myTable.Rows(i - 1).Item("备注") 'Trim(myRec.Fields("备注").Value)
            intSeq = myTable.Rows(i - 1).Item("序号") ' myRec.Fields("序号").Value
            strEngName = myTable.Rows(i - 1).Item("英文线名") 'Trim(myRec.Fields("英文线名").Value)
            strBrriName = myTable.Rows(i - 1).Item("线路简称") 'Trim(myRec.Fields("线路简称").Value)
            sLineNumber = myTable.Rows(i - 1).Item("线路编号") 'myRec.Fields("车站总数").Value

            NetInf.Line(i).nID = myTable.Rows(i - 1).Item("ID")
            NetInf.Line(i).sName = strTmpLineName.Trim
            NetInf.Line(i).sngLength = strTmpLineLength
            NetInf.Line(i).sMemo = strMemo
            NetInf.Line(i).intSeq = intSeq
            NetInf.Line(i).sBrriName = strBrriName
            NetInf.Line(i).sLineNumber = sLineNumber
            NetInf.Line(i).sEngName = strEngName
            ReDim NetInf.Line(i).Station(0)
            ReDim NetInf.Line(i).Section(0)
        Next

        ''车站信息
        Dim strTable2 As String = "select * from 车站信息 order by 线路名称,下行站序"
        Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
        '创建一个Datasetd
        Dim myDataSet2 As Data.DataSet = New Data.DataSet
        Mydc2.Fill(myDataSet2, "车站信息")
        Dim myTable2 As Data.DataTable
        myTable2 = myDataSet2.Tables("车站信息")
        Dim nID As Integer
        nID = 0
        ReDim StaInf(0)

        nID = 0
        For i = 1 To UBound(NetInf.Line)
            For j = 1 To myTable2.Rows.Count
                If Trim(myTable2.Rows(j - 1).Item("线路名称")) = NetInf.Line(i).sName Then
                    nID = nID + 1
                    ReDim Preserve NetInf.Line(i).Station(UBound(NetInf.Line(i).Station) + 1)
                    NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).nID = myTable2.Rows(j - 1).Item("序号")
                    NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).nDownID = myTable2.Rows(j - 1).Item("下行站序")
                    NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sStaName = Trim(myTable2.Rows(j - 1).Item("站名"))
                    NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sPrintStaName = Trim(myTable2.Rows(j - 1).Item("输出站名"))
                    NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sEngName = Trim(myTable2.Rows(j - 1).Item("英文站名"))
                    NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sStaStyle = Trim(myTable2.Rows(j - 1).Item("类型"))
                    NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sEngBrriName = Trim(myTable2.Rows(j - 1).Item("英文简称"))
                    NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sStaCode = Trim(myTable2.Rows(j - 1).Item("车站代码"))
                    NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sSingleOrDoubleLine = Trim(myTable2.Rows(j - 1).Item("单双线"))
                    NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sStaProperty = Trim(myTable2.Rows(j - 1).Item("性质"))
                    NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sngXcoord = Val(myTable2.Rows(j - 1).Item("X坐标"))
                    NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sngYcoord = Val(myTable2.Rows(j - 1).Item("Y坐标"))
                    NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sMemo = Trim(myTable2.Rows(j - 1).Item("备注"))
                    NetInf.Line(i).Station(UBound(NetInf.Line(i).Station)).sPicPath = Trim(myTable2.Rows(j - 1).Item("站形图"))

                    ReDim Preserve StaInf(UBound(StaInf) + 1)
                    StaInf(UBound(StaInf)).sStaName = Trim(myTable2.Rows(i - 1).Item("站名"))
                    StaInf(UBound(StaInf)).sEngName = Trim(myTable2.Rows(i - 1).Item("英文站名"))
                    StaInf(UBound(StaInf)).sAtLine = NetInf.Line(i).sName
                    StaInf(UBound(StaInf)).nDownID = nID
                End If
            Next
        Next
    End Sub


    Public Function GetStaCodeFromStaName(ByVal sStaName As String) As String
        GetStaCodeFromStaName = ""
        Dim i, j As Integer
        For i = 1 To UBound(NetInf.Line)
            For j = 1 To UBound(NetInf.Line(i).Station)
                If NetInf.Line(i).Station(j).sStaName = sStaName Then
                    GetStaCodeFromStaName = NetInf.Line(i).Station(j).sStaCode
                End If
            Next
        Next
    End Function

    '对应打开菜单
    Public Sub OpenNetDataBase(ByVal Trw As TreeView, ByVal PicNet As PictureBox, ByVal picBack As PictureBox, ByVal sFileStyle As String)
        Dim strPath As String
        Dim New0penFile As New OpenFileDialog
        WholeNetInf.sWholeNetName = ""
        ReDim WholeNetInf.sNetLine(0)
        If sFileStyle = "tpm" Then
            '获得线路数据库的路径
            New0penFile.Filter = "Data files (*.tpm;*.mdb)|*.tpm;*.mdb|All files (*.*)|*.*"
            New0penFile.FilterIndex = 1
            New0penFile.RestoreDirectory = True
            strPath = ""
            If New0penFile.ShowDialog() = DialogResult.OK Then
                strPath = New0penFile.FileName
            End If
        Else
            '获得网络数据库的路径
            New0penFile.Filter = "Data files (*.tpmp)|*.tpmp|All files (*.*)|*.*"
            New0penFile.FilterIndex = 1
            New0penFile.RestoreDirectory = True
            Dim strPath1 As String
            strPath1 = ""
            strPath = ""
            If New0penFile.ShowDialog() = DialogResult.OK Then
                strPath1 = New0penFile.FileName
            End If
            strPath = OpenWholeNet(strPath1) '打开网络
        End If

        '打开数据库
        If strPath <> "" Then
            Call SetSysDataBaseInf(strPath, Trw, PicNet, picBack)
        End If

    End Sub
    '打开网络
    Public Function OpenWholeNet(ByVal strPath1 As String) As String
        OpenWholeNet = ""
        '获得数据库的名称
        If strPath1 <> "" Then
            Dim strArray As New ArrayList
            Dim FolderPath As String
            Dim nLast As Integer
            Dim i As Integer
            Dim tmpString As String
            Dim tmpString1 As String
            nLast = InStrRev(strPath1, "\", , CompareMethod.Text) '右边数过来第几个值
            FolderPath = strPath1.Substring(0, nLast - 1) & "\data" '文件夹对应的路径
            WholeNetInf.sWholeNetName = strPath1.Substring(nLast, strPath1.Length - nLast)
            WholeNetInf.sNetFoldPath = FolderPath
            If My.Computer.FileSystem.FileExists(strPath1) = True Then
                For i = 1 To My.Computer.FileSystem.GetFiles(FolderPath).Count
                    tmpString = My.Computer.FileSystem.GetFiles(FolderPath).Item(i - 1)
                    tmpString1 = Right(tmpString, tmpString.Length - InStrRev(tmpString, "\", , CompareMethod.Text))
                    strArray.Add(tmpString1)
                Next
            End If
            If strArray.Count > 0 Then
                ReDim WholeNetInf.sNetLine(strArray.Count)
                For i = 1 To strArray.Count
                    WholeNetInf.sNetLine(i) = strArray.Item(i - 1)
                Next
                Dim tmpFile As String
                tmpFile = GetSelectLineName()
                If tmpFile <> "" Then
                    OpenWholeNet = tmpFile
                End If
            Else
                MsgBox("该路网下还没有任何线路数据，请先添加！")
                Exit Function
            End If
        End If

    End Function

    '得到选择的线路
    Public Function GetSelectLineName() As String
        GetSelectLineName = ""
        Dim i As Integer
        Dim nf As New frmInputBox
        nf.txtText.Visible = False
        nf.cmbText.Visible = True
        nf.cmbText.Items.Clear()
        For i = 1 To WholeNetInf.sNetLine.Length - 1
            nf.cmbText.Items.Add(WholeNetInf.sNetLine(i))
        Next
        nf.cmbText.Text = WholeNetInf.sNetLine(1)
        nf.Text = "选择窗体"
        nf.labTitle.Text = "请选择线路数据库名:"
        nf.ShowDialog()
        If StrInputBoxCombText.Trim <> "" And bCancelInput = 0 Then
            GetSelectLineName = WholeNetInf.sNetFoldPath & "\" & StrInputBoxCombText
        End If

    End Function

    '设置系统数据库信息
    Public Sub SetSysDataBaseInf(ByVal strPath As String, ByVal Trw As TreeView, ByVal PicNet As PictureBox, ByVal picBack As PictureBox)
        If strPath <> "" Then
            g_strDatabaseName = ""
            g_strNetMainPath = ""
            g_strNetPath = ""
            Dim nLast As Integer
            PicNet.Visible = True
            nLast = InStrRev(strPath, "\", , CompareMethod.Text) '右边数过来第几个值
            g_strDatabaseName = strPath.Substring(nLast)
            g_strNetMainPath = strPath
            g_strNetPath = strPath.Substring(0, nLast - 1) '全局的数据库路径

            nLast = InStrRev(g_strNetPath, "\", , CompareMethod.Text) '右边数过来第几个值
            'CurNet.strNetName = g_strNetPath.Substring(nLast)
            NetInf.sNetName = g_strNetPath.Substring(nLast)
            SystemPara.DatabasePassword = "tpmadmin"
            Dim sPassWord As String
            sPassWord = SystemPara.DatabasePassword
            'sPassWord = ""
            strCon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "';Persist Security Info=False;Jet OLEDB:Database Password= " & sPassWord & ""
            g_strNetMainPathOpenInfor = ";UID=admin;PWD=" & sPassWord & ";DATABASE=" & g_strNetMainPath & ""
            Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
            Try
                MyConn.Open()
            Catch ex As Exception
                MsgBox(ex.Message & "请联系程序开发人员！")
                Exit Sub
            End Try
            MyConn.Close()
            Call InitSystemViraient()
            Call SetDiTuPicture(PicNet, picBack)
            '读入线路与车站数据
            Call readNetData()
            Call listTreeViewLineData(Trw)
            Call PrintLineAndStationInPicture(PicNet, picBack)

            Dim sString As String
            If g_strNetMainPath <> "" Then
                'Call frmNetMain.MnueReset("打开数据库")
                If g_strNetMainPath.Length > 40 Then
                    sString = g_strNetMainPath.Substring(0, 10) & " ... " & g_strNetMainPath.Substring(g_strNetMainPath.Length - 30, 30)
                Else
                    sString = g_strNetMainPath
                End If
                'frmNetMain.Text = " 线网管理" & "—" & sString & " — 同济大学 Train Plan Maker V" & My.Application.Info.Version.ToString
            End If
        End If

    End Sub
    '加载底图图片
    Public Sub SetDiTuPicture(ByVal PicNet As PictureBox, ByVal PicBack As PictureBox)

        Dim sPath As String
        sPath = SystemPara.sProgrameFilePath & SystemPara.sPicFilePath
        Dim f As New System.IO.FileInfo(sPath)
        If f.Exists Then
            '文件存在
            Try
                PicNet.Image = Image.FromFile(SystemPara.sProgrameFilePath & SystemPara.sPicFilePath)
                PicBack.Image = Image.FromFile(SystemPara.sProgrameFilePath & SystemPara.sPicFilePath)
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Else
            '文件不存在，另作处理
            MsgBox("[" & sPath & "] 文件不存在,请修改文件名称或修改系统参数!", , "提示")
        End If

        'If Len((Dir(sPath, vbNormal))) = 0 Then
        '    MsgBox("[" & sPath & "] 文件不存在,请修改文件名称或修改系统参数!","提示")
        'Else
        '    Try
        '        PicNet.Image = Image.FromFile(SystemPara.sProgrameFilePath & SystemPara.sPicFilePath)
        '    Catch ex As Exception
        '        MsgBox(ex.ToString)
        '    End Try
        'End If
    End Sub

    Public Sub listTreeViewLineData(ByVal treView As TreeView)
        '导入线路与车站信息到TreeView框中
        Dim i As Integer
        Dim j As Integer
        Dim nNode As TreeNode
        Dim nNode2 As TreeNode
        Dim nNode3 As TreeNode
        treView.Nodes.Clear()

        nNode = treView.Nodes.Add(NetInf.sNetName)
        nNode.ImageIndex = 0
        For i = 1 To UBound(NetInf.Line)
            nNode2 = nNode.Nodes.Add(NetInf.Line(i).sName)
            nNode2.ImageIndex = 1
            For j = 1 To UBound(NetInf.Line(i).Station)
                nNode3 = nNode2.Nodes.Add(NetInf.Line(i).Station(j).sStaName)
                nNode3.ImageIndex = 2
            Next j
        Next i
        treView.ExpandAll()
    End Sub

    Public Sub ShowLineInPicture(ByVal strSelectLineName As String, ByVal strSelectStaName As String, ByVal picNet As PictureBox, ByVal picBack As PictureBox, ByVal colSta As Color, ByVal colLine As Color, ByVal colSelectSta As Color, ByVal colSelectLine As Color)
        Dim X1 As Long
        Dim Y1 As Long
        Dim ForX As Long
        Dim ForY As Long
        Dim lngStaX As Long
        Dim lngStaY As Long
        Dim i, j As Integer

        lngStaX = 0
        lngStaY = 0
        Dim rBmp As Bitmap '画图临时保存的图像
        Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
        ForX = 0
        ForY = 0

        rBmp = New Bitmap(picBack.Image)
        rBmpGraphics = Graphics.FromImage(rBmp)

        For i = 1 To UBound(NetInf.Line)
            ForX = 0
            ForY = 0
            For j = 1 To UBound(NetInf.Line(i).Station)
                X1 = NetInf.Line(i).Station(j).sngXcoord
                Y1 = NetInf.Line(i).Station(j).sngYcoord

                If NetInf.Line(i).sName = strSelectLineName And NetInf.Line(i).Station(j).sStaName = strSelectStaName Then
                    lngStaX = X1
                    lngStaY = Y1
                End If

                If NetInf.Line(i).sName = strSelectLineName Then
                    If ForX > 0 And ForY > 0 Then
                        rBmpGraphics.DrawLine(New Pen(colSelectLine, 3), ForX, ForY, X1, Y1)
                        rBmpGraphics.DrawEllipse(New Pen(Color.Brown, 2), ForX - 8, ForY - 8, 16, 16)
                        rBmpGraphics.FillEllipse(New SolidBrush(colSta), New Rectangle(ForX - 8, ForY - 8, 16, 16))
                    End If

                    If j = UBound(NetInf.Line(i).Station) Then
                        If X1 > 0 And Y1 > 0 Then
                            rBmpGraphics.DrawEllipse(New Pen(Color.Brown, 2), X1 - 8, Y1 - 8, 16, 16)
                            rBmpGraphics.FillEllipse(New SolidBrush(colSta), New Rectangle(X1 - 8, Y1 - 8, 16, 16))
                        End If
                    End If
                Else
                    If ForX > 0 And ForY > 0 Then
                        rBmpGraphics.DrawLine(New Pen(colLine, 3), ForX, ForY, X1, Y1)
                        rBmpGraphics.DrawEllipse(New Pen(Color.Brown, 2), ForX - 8, ForY - 8, 16, 16)
                        rBmpGraphics.FillEllipse(New SolidBrush(colSta), New Rectangle(ForX - 8, ForY - 8, 16, 16))
                    End If

                    If j = UBound(NetInf.Line(i).Station) Then
                        If X1 > 0 And Y1 > 0 Then
                            rBmpGraphics.DrawEllipse(New Pen(Color.Brown, 2), X1 - 8, Y1 - 8, 16, 16)
                            rBmpGraphics.FillEllipse(New SolidBrush(colSta), New Rectangle(X1 - 8, Y1 - 8, 16, 16))
                        End If
                    End If
                End If
                ForX = X1
                ForY = Y1
            Next j
        Next i

        If strSelectStaName <> "" Then '打印所有线路
            If lngStaX <> 0 And lngStaY <> 0 Then
                rBmpGraphics.FillEllipse(New SolidBrush(colSelectSta), New Rectangle(lngStaX - 8, lngStaY - 8, 16, 16))
            End If
        End If

        picNet.Image = rBmp
        'picNet.Refresh()

    End Sub


    '根据选中的线路与车站显示车站与线路图
    Public Sub PrintLineAndStationInPicture(ByVal picNet As PictureBox, ByVal picBack As PictureBox)

        If g_LineName = "" Then
            Call ShowLineInPicture("", "", picNet, picBack, Color.Blue, Color.LawnGreen, Color.Blue, Color.Red)
        Else
            If g_StationName = "" Then
                Call ShowLineInPicture(g_LineName, "", picNet, picBack, Color.Blue, Color.LawnGreen, Color.Blue, Color.Red)
            Else
                Call ShowLineInPicture(g_LineName, g_StationName, picNet, picBack, Color.Blue, Color.LawnGreen, Color.Red, Color.Red)
            End If
        End If
    End Sub

    '显示鼠标选中的车站
    Public Sub PrintSelectStatoinInPicture(ByVal lngX As Long, ByVal lngY As Long, ByVal trwTree As TreeView, ByVal picNet As PictureBox, ByVal picBack As PictureBox)
        Dim g As System.Drawing.Graphics
        Dim X1 As Long
        Dim Y1 As Long
        Dim lngStaX As Long
        Dim lngStaY As Long
        Dim i, j As Integer
        Dim lngSelectWidth As Long '点击的范围
        Dim MaxlngWidth As Long '选中的范围
        Dim strSelectSta As String
        Dim strSelectLine As String
        lngStaX = 0
        lngStaY = 0
        MaxlngWidth = 100000
        'picNet.Refresh()
        strSelectSta = ""
        strSelectLine = ""
        For i = 1 To UBound(NetInf.Line)
            For j = 1 To UBound(NetInf.Line(i).Station)
                g = picNet.CreateGraphics
                X1 = NetInf.Line(i).Station(j).sngXcoord
                Y1 = NetInf.Line(i).Station(j).sngYcoord

                lngSelectWidth = ((X1 - lngX) ^ 2 + (Y1 - lngY) ^ 2) ^ 0.5
                If lngSelectWidth < MaxlngWidth Then '找到车站
                    MaxlngWidth = lngSelectWidth
                    strSelectLine = NetInf.Line(i).sName
                    strSelectSta = NetInf.Line(i).Station(j).sStaName
                    lngStaX = X1
                    lngStaY = Y1
                End If
            Next j
        Next i

        If strSelectSta <> "" Then
            g_LineName = strSelectLine
            g_StationName = strSelectSta
            setTrwTreeShowSelectedSta(trwTree, g_LineName, g_StationName)
            'trwTree.ResetText = strSelectSta
            If lngStaX > 0 And lngStaY > 0 Then
                PrintLineAndStationInPicture(picNet, picBack)
            End If
        Else
        End If

    End Sub

    '在树视图上显示选中的车站
    Public Sub setTrwTreeShowSelectedSta(ByVal trwTree As TreeView, ByVal strLineName As String, ByVal StrStaName As String)

        Dim i As Integer
        Dim j As Integer
        For i = 0 To trwTree.Nodes(0).GetNodeCount(False) - 1
            If trwTree.Nodes(0).Nodes(i).Text = strLineName Then
                For j = 0 To trwTree.Nodes(0).Nodes(i).GetNodeCount(False) - 1
                    If trwTree.Nodes(0).Nodes(i).Nodes(j).Text = StrStaName Then
                        trwTree.SelectedNode = trwTree.Nodes(0).Nodes(i).Nodes(j)
                        Exit For
                    End If
                Next j
                Exit For
            End If
        Next i
    End Sub



    '修改车站的坐标
    Public Sub ResetStatoionCord(ByVal strLineName As String, ByVal strStaName As String, ByVal lngX As Long, ByVal lngY As Long)
        Dim i As Integer
        Dim j As Integer

        For i = 1 To UBound(NetInf.Line)
            If NetInf.Line(i).sName = strLineName Then
                For j = 1 To UBound(NetInf.Line(i).Station)
                    If NetInf.Line(i).Station(j).sStaName = strStaName Then
                        NetInf.Line(i).Station(j).sngXcoord = lngX
                        NetInf.Line(i).Station(j).sngYcoord = lngY
                        Exit For
                    End If
                Next j
            End If
        Next i

        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim Str As String

        Str = "update 车站信息 set " & _
                "X坐标 ='" & lngX & "'," & _
                "Y坐标 ='" & lngY & "'" & _
                "where 站名 = '" & strStaName & "'"

        Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom.ExecuteNonQuery()
        MyConn.Close()

    End Sub

    '删除车站
    Public Sub DeleteStation(ByVal strStaName As String, ByVal strLineName As String, ByVal treeView As TreeView, ByVal PicNet As PictureBox, ByVal picBack As PictureBox)
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim Str As String
        Str = "delete * from 车站信息 where 线路名称='" & strLineName & "' and 站名='" & strStaName & "'"
        Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom.ExecuteNonQuery()
        MyConn.Close()

        Call readNetData()
        Call listTreeViewLineData(treeView)
    End Sub


    '新建或修改线路
    Public Sub EditLineInfor(ByVal strEditStyle As String, ByVal treeView As TreeView, ByVal PicNet As PictureBox, ByVal picBack As PictureBox)
        If strEditStyle = "新建" Then '新建线路
            ReDim stuListItem(6)
            stuListItem(1).strItem = "线路名称"
            stuListItem(1).strStyle = PropStrStyle.TexBox
            stuListItem(1).strTxtList = ""
            stuListItem(1).strItemCriterion = TextCriterion.NotEmpty

            stuListItem(2).strItem = "线路简称"
            stuListItem(2).strStyle = PropStrStyle.TexBox
            stuListItem(2).strTxtList = ""
            stuListItem(2).strItemCriterion = TextCriterion.NotRequired

            stuListItem(3).strItem = "英文线名"
            stuListItem(3).strStyle = PropStrStyle.TexBox
            stuListItem(3).strTxtList = ""
            stuListItem(3).strItemCriterion = TextCriterion.NotRequired

            stuListItem(4).strItem = "线路编号"
            stuListItem(4).strStyle = PropStrStyle.TexBox
            stuListItem(4).strTxtList = ""
            stuListItem(4).strItemCriterion = TextCriterion.NotRequired

            stuListItem(5).strItem = "线路总长"
            stuListItem(5).strStyle = PropStrStyle.TexBox
            stuListItem(5).strTxtList = ""
            stuListItem(5).strItemCriterion = TextCriterion.OnlyNumber


            stuListItem(6).strItem = "备注"
            stuListItem(6).strStyle = PropStrStyle.TexBox
            stuListItem(6).strTxtList = "无"
            stuListItem(6).strItemCriterion = TextCriterion.NotRequired

            Dim nf As New frmEditDataProperity
            nf.ShowDialog()
            If nf.blnOK = True Then
                Call AddNetLine(stuListItem(1).strReturnValue, stuListItem(3).strReturnValue, stuListItem(2).strReturnValue, Val(stuListItem(5).strReturnValue), stuListItem(4).strReturnValue, stuListItem(6).strReturnValue)
                Call listTreeViewLineData(treeView)
                Call PrintLineAndStationInPicture(PicNet, picBack)
            End If

        Else '修改
            Dim i As Integer
            Dim sBeLineName As String
            For i = 1 To UBound(NetInf.Line)
                If g_LineName = NetInf.Line(i).sName Then
                    sBeLineName = NetInf.Line(i).sName
                    ReDim stuListItem(6)
                    stuListItem(1).strItem = "线路名称"
                    stuListItem(1).strStyle = PropStrStyle.TexBox
                    stuListItem(1).strTxtList = NetInf.Line(i).sName
                    stuListItem(1).strItemCriterion = TextCriterion.NotEmpty

                    stuListItem(2).strItem = "线路简称"
                    stuListItem(2).strStyle = PropStrStyle.TexBox
                    stuListItem(2).strTxtList = NetInf.Line(i).sBrriName
                    stuListItem(2).strItemCriterion = TextCriterion.NotRequired

                    stuListItem(3).strItem = "英文线名"
                    stuListItem(3).strStyle = PropStrStyle.TexBox
                    stuListItem(3).strTxtList = NetInf.Line(i).sEngName
                    stuListItem(3).strItemCriterion = TextCriterion.NotRequired

                    stuListItem(4).strItem = "线路编号"
                    stuListItem(4).strStyle = PropStrStyle.TexBox
                    stuListItem(4).strTxtList = NetInf.Line(i).sLineNumber
                    stuListItem(4).strItemCriterion = TextCriterion.NotRequired

                    stuListItem(5).strItem = "线路总长"
                    stuListItem(5).strStyle = PropStrStyle.TexBox
                    stuListItem(5).strTxtList = Str(NetInf.Line(i).sngLength)
                    stuListItem(5).strItemCriterion = TextCriterion.NotRequired


                    stuListItem(6).strItem = "备注"
                    stuListItem(6).strStyle = PropStrStyle.TexBox
                    stuListItem(6).strTxtList = NetInf.Line(i).sMemo
                    stuListItem(6).strItemCriterion = TextCriterion.NotRequired

                    Dim nf2 As New frmEditDataProperity
                    nf2.ShowDialog()
                    If nf2.blnOK = True Then
                        Call EditNetLine(NetInf.Line(i).nID, NetInf.Line(i).intSeq, stuListItem(1).strReturnValue, stuListItem(3).strReturnValue, stuListItem(2).strReturnValue, Val(stuListItem(5).strReturnValue), stuListItem(4).strReturnValue, stuListItem(6).strReturnValue, sBeLineName)
                        Call listTreeViewLineData(treeView)
                        Call PrintLineAndStationInPicture(PicNet, picBack)
                    End If

                    Exit For
                End If
            Next i
        End If
    End Sub

    '新建或修改车站
    Public Sub EditStationInfor(ByVal strEditStyle As String, ByVal treeView As TreeView, ByVal PicNet As PictureBox, ByVal picBack As PictureBox)
        Dim i As Integer
        Dim j As Integer
        Dim tmpList(12) As String
        Dim nLineID As Integer
        Dim nStaID As Integer
        Dim nToStaNum As Integer
        Select Case strEditStyle
            Case "车站前添加"
                Call DimstuListItemAddSta()
                Call FromStaNameToStaID(g_StationName, g_LineName)
                nLineID = Val(ReturnValue.strValue1)
                nStaID = Val(ReturnValue.strValue2)
                stuListItem(9).strTxtList = NetInf.Line(nLineID).Station(nStaID).sngXcoord
                stuListItem(10).strTxtList = NetInf.Line(nLineID).Station(nStaID).sngYcoord - 30

                If nStaID > 0 Then
                    Dim nf As New frmEditDataProperity
                    nf.ShowDialog()

                    If nf.blnOK = True Then
                        nToStaNum = UBound(NetInf.Line(nLineID).Station)

                        tmpList(1) = stuListItem(1).strReturnValue
                        tmpList(2) = stuListItem(2).strReturnValue
                        tmpList(3) = stuListItem(3).strReturnValue
                        tmpList(4) = stuListItem(4).strReturnValue
                        tmpList(5) = stuListItem(5).strReturnValue
                        tmpList(6) = stuListItem(6).strReturnValue
                        tmpList(7) = stuListItem(7).strReturnValue
                        tmpList(8) = stuListItem(8).strReturnValue
                        tmpList(9) = stuListItem(9).strReturnValue
                        tmpList(10) = stuListItem(10).strReturnValue
                        tmpList(11) = stuListItem(11).strReturnValue
                        tmpList(12) = stuListItem(12).strReturnValue

                        stuListItem(1).strItem = "车站名称"
                        stuListItem(2).strItem = "输出站名"
                        stuListItem(3).strItem = "英文站名"
                        stuListItem(4).strItem = "车站代码"
                        stuListItem(5).strItem = "英文简称"
                        stuListItem(6).strItem = "单双线"
                        stuListItem(7).strItem = "车站类型"
                        stuListItem(8).strItem = "车站性质"
                        stuListItem(9).strItem = "X坐标"
                        stuListItem(10).strItem = "Y坐标"
                        stuListItem(11).strItem = "站形图"
                        stuListItem(12).strItem = "备注"
                        Call AddNetStation(g_LineName, _
                                            tmpList(1), _
                                            tmpList(2), _
                                            nStaID, _
                                            tmpList(7), _
                                            tmpList(3), _
                                            tmpList(5), _
                                            tmpList(4), _
                                            tmpList(6), _
                                            tmpList(8), _
                                            Val(tmpList(9)), _
                                            Val(tmpList(10)), _
                                            tmpList(11), _
                                            tmpList(12))
                        Call listTreeViewLineData(treeView)
                        Call PrintLineAndStationInPicture(PicNet, picBack)

                    End If
                End If
            Case "车站后添加"
                Call DimstuListItemAddSta()
                Call FromStaNameToStaID(g_StationName, g_LineName)
                nLineID = Val(ReturnValue.strValue1)
                nStaID = Val(ReturnValue.strValue2)
                stuListItem(9).strTxtList = NetInf.Line(nLineID).Station(nStaID).sngXcoord
                stuListItem(10).strTxtList = NetInf.Line(nLineID).Station(nStaID).sngYcoord + 30

                If nStaID > 0 Then
                    Dim nf As New frmEditDataProperity
                    nf.ShowDialog()

                    If nf.blnOK = True Then
                        nToStaNum = UBound(NetInf.Line(nLineID).Station)

                        tmpList(1) = stuListItem(1).strReturnValue
                        tmpList(2) = stuListItem(2).strReturnValue
                        tmpList(3) = stuListItem(3).strReturnValue
                        tmpList(4) = stuListItem(4).strReturnValue
                        tmpList(5) = stuListItem(5).strReturnValue
                        tmpList(6) = stuListItem(6).strReturnValue
                        tmpList(7) = stuListItem(7).strReturnValue
                        tmpList(8) = stuListItem(8).strReturnValue
                        tmpList(9) = stuListItem(9).strReturnValue
                        tmpList(10) = stuListItem(10).strReturnValue
                        tmpList(11) = stuListItem(11).strReturnValue
                        tmpList(12) = stuListItem(12).strReturnValue

                        stuListItem(1).strItem = "车站名称"
                        stuListItem(2).strItem = "输出站名"
                        stuListItem(3).strItem = "英文站名"
                        stuListItem(4).strItem = "车站代码"
                        stuListItem(5).strItem = "英文简称"
                        stuListItem(6).strItem = "单双线"
                        stuListItem(7).strItem = "车站类型"
                        stuListItem(8).strItem = "车站性质"
                        stuListItem(9).strItem = "X坐标"
                        stuListItem(10).strItem = "Y坐标"
                        stuListItem(11).strItem = "站形图"
                        stuListItem(12).strItem = "备注"
                        Call AddNetStation(g_LineName, _
                                            tmpList(1), _
                                            tmpList(2), _
                                            nStaID + 1, _
                                            tmpList(7), _
                                            tmpList(3), _
                                            tmpList(5), _
                                            tmpList(4), _
                                            tmpList(6), _
                                            tmpList(8), _
                                            Val(tmpList(9)), _
                                            Val(tmpList(10)), _
                                            tmpList(11), _
                                            tmpList(12))
                        Call listTreeViewLineData(treeView)
                        Call PrintLineAndStationInPicture(PicNet, picBack)
                    End If
                End If

            Case "新建"

                Call DimstuListItemAddSta()
                nLineID = FormLineNameToLineID(g_LineName)
                nToStaNum = UBound(NetInf.Line(nLineID).Station)
                stuListItem(9).strTxtList = 100 ' NetInf.Line(nLineID).Station(nToStaNum).sngXcoord
                stuListItem(10).strTxtList = 100 'NetInf.Line(nLineID).Station(nToStaNum).sngYcoord - 30

                If nLineID > 0 Then
                    Dim nf As New frmEditDataProperity
                    nf.ShowDialog()

                    If nf.blnOK = True Then

                        tmpList(1) = stuListItem(1).strReturnValue
                        tmpList(2) = stuListItem(2).strReturnValue
                        tmpList(3) = stuListItem(3).strReturnValue
                        tmpList(4) = stuListItem(4).strReturnValue
                        tmpList(5) = stuListItem(5).strReturnValue
                        tmpList(6) = stuListItem(6).strReturnValue
                        tmpList(7) = stuListItem(7).strReturnValue
                        tmpList(8) = stuListItem(8).strReturnValue
                        tmpList(9) = stuListItem(9).strReturnValue
                        tmpList(10) = stuListItem(10).strReturnValue
                        tmpList(11) = stuListItem(11).strReturnValue
                        tmpList(12) = stuListItem(11).strReturnValue

                        stuListItem(1).strItem = "车站名称"
                        stuListItem(1).strItem = "输出站名"
                        stuListItem(2).strItem = "英文站名"
                        stuListItem(3).strItem = "车站代码"
                        stuListItem(4).strItem = "英文简称"
                        stuListItem(5).strItem = "单双线"
                        stuListItem(6).strItem = "车站类型"
                        stuListItem(7).strItem = "车站性质"
                        stuListItem(8).strItem = "X坐标"
                        stuListItem(9).strItem = "Y坐标"
                        stuListItem(10).strItem = "站形图"
                        stuListItem(11).strItem = "备注"
                        Call AddNetStation(g_LineName, _
                                            tmpList(1), _
                                            tmpList(2), _
                                            nToStaNum + 1, _
                                            tmpList(7), _
                                            tmpList(3), _
                                            tmpList(5), _
                                            tmpList(4), _
                                            tmpList(6), _
                                            tmpList(8), _
                                            Val(tmpList(9)), _
                                            Val(tmpList(10)), _
                                            tmpList(11), _
                                            tmpList(12))
                        Call listTreeViewLineData(treeView)
                        Call PrintLineAndStationInPicture(PicNet, picBack)

                    End If
                End If

            Case "修改"
                Dim bIfHaveEdit As Boolean
                bIfHaveEdit = False
                For i = 1 To UBound(NetInf.Line)
                    If NetInf.Line(i).sName = g_LineName Then
                        For j = 1 To UBound(NetInf.Line(i).Station)
                            If NetInf.Line(i).Station(j).sStaName = g_StationName Then

                                ReDim stuListItem(12)
                                stuListItem(1).strItem = "车站名称"
                                stuListItem(1).strStyle = PropStrStyle.TexBox
                                stuListItem(1).strTxtList = NetInf.Line(i).Station(j).sStaName
                                stuListItem(1).strItemCriterion = TextCriterion.NotEmpty

                                stuListItem(2).strItem = "输出站名"
                                stuListItem(2).strStyle = PropStrStyle.TexBox
                                stuListItem(2).strTxtList = NetInf.Line(i).Station(j).sPrintStaName
                                stuListItem(2).strItemCriterion = TextCriterion.NotEmpty

                                stuListItem(3).strItem = "英文站名"
                                stuListItem(3).strStyle = PropStrStyle.TexBox
                                stuListItem(3).strTxtList = NetInf.Line(i).Station(j).sEngName
                                stuListItem(3).strItemCriterion = TextCriterion.NotRequired

                                stuListItem(4).strItem = "车站代码"
                                stuListItem(4).strStyle = PropStrStyle.TexBox
                                stuListItem(4).strTxtList = NetInf.Line(i).Station(j).sStaCode
                                stuListItem(4).strItemCriterion = TextCriterion.NotRequired


                                stuListItem(5).strItem = "英文简称"
                                stuListItem(5).strStyle = PropStrStyle.TexBox
                                stuListItem(5).strTxtList = NetInf.Line(i).Station(j).sEngBrriName
                                stuListItem(5).strItemCriterion = TextCriterion.NotRequired

                                stuListItem(6).strItem = "单双线"
                                stuListItem(6).strStyle = PropStrStyle.ComBox
                                ReDim Preserve stuListItem(6).StrCmbList(4)
                                stuListItem(6).StrCmbList(1) = "单线"
                                stuListItem(6).StrCmbList(2) = "双线"
                                stuListItem(6).StrCmbList(3) = "三线"
                                stuListItem(6).StrCmbList(4) = "多线"
                                stuListItem(6).strTxtList = NetInf.Line(i).Station(j).sSingleOrDoubleLine
                                stuListItem(6).strItemCriterion = TextCriterion.NotRequired

                                stuListItem(7).strItem = "车站类型"
                                stuListItem(7).strStyle = PropStrStyle.ComBox
                                ReDim Preserve stuListItem(7).StrCmbList(3)
                                stuListItem(7).StrCmbList(1) = "中间站"
                                'stuListItem(7).StrCmbList(2) = "终端站"
                                'stuListItem(7).StrCmbList(3) = "线路所"
                                stuListItem(7).StrCmbList(2) = "车场"
                                'stuListItem(7).StrCmbList(5) = "出入车场信号机"
                                'stuListItem(7).StrCmbList(5) = "非作业站"
                                stuListItem(7).StrCmbList(3) = "大站"
                                stuListItem(7).strTxtList = NetInf.Line(i).Station(j).sStaStyle
                                stuListItem(7).strItemCriterion = TextCriterion.NotRequired


                                stuListItem(8).strItem = "车站性质"
                                stuListItem(8).strStyle = PropStrStyle.ComBox
                                ReDim Preserve stuListItem(8).StrCmbList(3)
                                stuListItem(8).StrCmbList(1) = "无"
                                stuListItem(8).StrCmbList(2) = "分岔站"
                                stuListItem(8).StrCmbList(3) = "环形终端站"
                                stuListItem(8).strTxtList = NetInf.Line(i).Station(j).sStaProperty
                                stuListItem(8).strItemCriterion = TextCriterion.NotRequired

                                stuListItem(9).strItem = "X坐标"
                                stuListItem(9).strStyle = PropStrStyle.TexBox
                                stuListItem(9).strTxtList = NetInf.Line(i).Station(j).sngXcoord
                                stuListItem(9).strItemCriterion = TextCriterion.OnlyNumber

                                stuListItem(10).strItem = "Y坐标"
                                stuListItem(10).strStyle = PropStrStyle.TexBox
                                stuListItem(10).strTxtList = NetInf.Line(i).Station(j).sngYcoord
                                stuListItem(10).strItemCriterion = TextCriterion.OnlyNumber

                                stuListItem(11).strItem = "站形图"
                                stuListItem(11).strStyle = PropStrStyle.SelectBox
                                stuListItem(11).strTxtList = NetInf.Line(i).Station(j).sPicPath
                                stuListItem(11).strItemCriterion = TextCriterion.NotRequired

                                stuListItem(12).strItem = "备注"
                                stuListItem(12).strStyle = PropStrStyle.TexBox
                                stuListItem(12).strTxtList = NetInf.Line(i).Station(j).sMemo
                                stuListItem(12).strItemCriterion = TextCriterion.NotRequired

                                'If bIfHaveEdit = False Then
                                Dim nf2 As New frmEditDataProperity
                                nf2.ShowDialog()
                                If nf2.blnOK = True Then
                                    tmpList(1) = stuListItem(1).strReturnValue
                                    tmpList(2) = stuListItem(2).strReturnValue
                                    tmpList(3) = stuListItem(3).strReturnValue
                                    tmpList(4) = stuListItem(4).strReturnValue
                                    tmpList(5) = stuListItem(5).strReturnValue
                                    tmpList(6) = stuListItem(6).strReturnValue
                                    tmpList(7) = stuListItem(7).strReturnValue
                                    tmpList(8) = stuListItem(8).strReturnValue
                                    tmpList(9) = stuListItem(9).strReturnValue
                                    tmpList(10) = stuListItem(10).strReturnValue
                                    tmpList(11) = stuListItem(11).strReturnValue
                                    tmpList(12) = stuListItem(11).strReturnValue
                                    bIfHaveEdit = True
                                    Call EditNetStation(NetInf.Line(i).Station(j).nID, _
                                                        g_LineName, _
                                                        tmpList(1), _
                                                        tmpList(2), _
                                                        NetInf.Line(i).Station(j).nDownID, _
                                                        tmpList(7), _
                                                        tmpList(3), _
                                                        tmpList(5), _
                                                        tmpList(4), _
                                                        tmpList(6), _
                                                        tmpList(8), _
                                                        Val(tmpList(9)), _
                                                        Val(tmpList(10)), _
                                                        tmpList(11), _
                                                        tmpList(12))
                                    Exit For
                                Else
                                    Exit Sub
                                End If
                                ' End If
                            End If
                        Next j
                    End If

                    If bIfHaveEdit = True Then
                        Exit For
                    End If
                Next i
                Call listTreeViewLineData(treeView)
                Call PrintLineAndStationInPicture(PicNet, picBack)
        End Select
    End Sub

    '根据车站名返回车站的ID 
    Public Sub FromStaNameToStaID(ByVal strStaName As String, ByVal strLineName As String)
        Dim bIFfind As Boolean
        Dim i, j As Integer
        bIFfind = False
        ReturnValue.strValue1 = ""
        ReturnValue.strValue2 = ""

        For i = 1 To UBound(NetInf.Line)
            If NetInf.Line(i).sName = strLineName Then
                For j = 1 To UBound(NetInf.Line(i).Station)
                    If NetInf.Line(i).Station(j).sStaName = strStaName Then
                        ReturnValue.strValue1 = Str(i)
                        ReturnValue.strValue2 = Str(j)
                        bIFfind = True
                        Exit For
                    End If
                Next j
            End If

            If bIFfind = True Then
                Exit For
            End If
        Next i
    End Sub

    '根据线路返回线路ID
    Public Function FormLineNameToLineID(ByVal strLineName As String) As Integer
        FormLineNameToLineID = 0
        Dim i As Integer
        For i = 1 To UBound(NetInf.Line)
            If NetInf.Line(i).sName = strLineName Then
                FormLineNameToLineID = i
                Exit For
            End If
        Next i
    End Function

    '定义属性赋值，车站添加时
    Public Sub DimstuListItemAddSta()
        ReDim stuListItem(12)
        stuListItem(1).strItem = "车站名称"
        stuListItem(1).strStyle = PropStrStyle.TexBox
        stuListItem(1).strTxtList = ""
        stuListItem(1).strItemCriterion = TextCriterion.NotEmpty

        stuListItem(2).strItem = "输出站名"
        stuListItem(2).strStyle = PropStrStyle.TexBox
        stuListItem(2).strTxtList = ""
        stuListItem(2).strItemCriterion = TextCriterion.NotEmpty

        stuListItem(3).strItem = "英文站名"
        stuListItem(3).strStyle = PropStrStyle.TexBox
        stuListItem(3).strTxtList = ""
        stuListItem(3).strItemCriterion = TextCriterion.NotRequired


        stuListItem(4).strItem = "车站代码"
        stuListItem(4).strStyle = PropStrStyle.TexBox
        stuListItem(4).strTxtList = ""
        stuListItem(4).strItemCriterion = TextCriterion.NotRequired

        stuListItem(5).strItem = "英文简称"
        stuListItem(5).strStyle = PropStrStyle.TexBox
        stuListItem(5).strTxtList = ""
        stuListItem(5).strItemCriterion = TextCriterion.NotRequired

        stuListItem(6).strItem = "单双线"
        stuListItem(6).strStyle = PropStrStyle.ComBox
        ReDim Preserve stuListItem(6).StrCmbList(4)
        stuListItem(6).StrCmbList(1) = "单线"
        stuListItem(6).StrCmbList(2) = "双线"
        stuListItem(6).StrCmbList(3) = "三线"
        stuListItem(6).StrCmbList(4) = "多线"
        stuListItem(6).strTxtList = "双线"
        stuListItem(6).strItemCriterion = TextCriterion.NotRequired

        stuListItem(7).strItem = "车站类型"
        stuListItem(7).strStyle = PropStrStyle.ComBox
        ReDim Preserve stuListItem(7).StrCmbList(3)
        stuListItem(7).StrCmbList(1) = "中间站"
        'stuListItem(7).StrCmbList(2) = "终端站"
        'stuListItem(7).StrCmbList(3) = "线路所"
        stuListItem(7).StrCmbList(2) = "车场"
        'stuListItem(7).StrCmbList(5) = "出入车场信号机"
        stuListItem(7).StrCmbList(3) = "大站"
        stuListItem(7).strTxtList = "中间站"
        stuListItem(7).strItemCriterion = TextCriterion.NotRequired


        stuListItem(8).strItem = "车站性质"
        stuListItem(8).strStyle = PropStrStyle.ComBox
        ReDim Preserve stuListItem(8).StrCmbList(3)
        stuListItem(8).StrCmbList(1) = "无"
        stuListItem(8).StrCmbList(2) = "分岔站"
        stuListItem(8).StrCmbList(3) = "环形终端站"
        stuListItem(8).strTxtList = "无"
        stuListItem(8).strItemCriterion = TextCriterion.NotRequired

        stuListItem(9).strItem = "X坐标"
        stuListItem(9).strStyle = PropStrStyle.TexBox
        stuListItem(9).strTxtList = "0"
        stuListItem(9).strItemCriterion = TextCriterion.OnlyNumber

        stuListItem(10).strItem = "Y坐标"
        stuListItem(10).strStyle = PropStrStyle.TexBox
        stuListItem(10).strTxtList = "0"
        stuListItem(10).strItemCriterion = TextCriterion.OnlyNumber

        stuListItem(11).strItem = "站形图"
        stuListItem(11).strStyle = PropStrStyle.SelectBox
        stuListItem(11).strTxtList = ""
        stuListItem(11).strItemCriterion = TextCriterion.NotRequired

        stuListItem(12).strItem = "备注"
        stuListItem(12).strStyle = PropStrStyle.TexBox
        stuListItem(12).strTxtList = "无"
        stuListItem(12).strItemCriterion = TextCriterion.NotRequired
    End Sub

    '根据线路与车站名得到站形图的名称
    Public Function GetStaPicName(ByVal sLineName As String, ByVal sStaName As String) As String
        GetStaPicName = 0
        'Dim i As Integer
        'Dim j As Integer
        'Dim IfFind As Boolean
        'IfFind = False
        'GetStaPicName = ""
        'For i = 1 To ubound(
        '    If CurNet.c_Line.Item(i).strName = sLineName Then
        '        For j = 1 To CurNet.c_Line.Item(i).c_Stations.Count
        '            If CurNet.c_Line.Item(i).c_Stations.Item(j).strName = sStaName Then
        '                GetStaPicName = CurNet.c_Line.Item(i).c_Stations.Item(j).strPicName
        '                IfFind = True
        '                Exit For
        '            End If
        '        Next j
        '    End If
        '    If IfFind = True Then Exit For
        'Next i
    End Function


    '添加线路
    Public Sub AddNetLine(ByRef sLineName As String, _
                            ByVal strEngName As String, _
                            ByVal strBrriName As String, _
                            ByRef nLong As Single, _
                            ByVal sLineNumber As String, _
                            ByVal strMemo As String)

        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim strTable As String = "select * from 路网线路信息"
        Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
        Dim myDataSet As System.Data.DataSet = New System.Data.DataSet
        Mydc.Fill(myDataSet, "路网线路信息")
        Dim myTable As Data.DataTable
        myTable = myDataSet.Tables("路网线路信息")
        Dim nCurID As Integer
        nCurID = myTable.Rows.Count + 1
        Dim Str As String
        Str = "insert into 路网线路信息(序号,线路名称,线路简称,英文线名,线路总长,线路编号,备注) values('" & nCurID & "', '" & _
               sLineName & "', '" & strEngName & "', '" & _
               strBrriName & "', '" & nLong & "', '" & _
               sLineNumber & "', '" & strMemo & "')"
        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()
        MyConn.Close()
        Call readNetData()
    End Sub

    '修改线路
    Public Sub EditNetLine(ByVal nID As Integer, _
                            ByVal nLineID As Integer, _
                            ByRef sLineName As String, _
                            ByVal strEngName As String, _
                            ByVal strBrriName As String, _
                            ByRef nLong As Single, _
                            ByVal sLineNumber As String, _
                            ByVal strMemo As String, ByVal sBeforLineName As String)

        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim Str As String
        Str = "update 路网线路信息 set " & _
                "序号 ='" & nLineID & "'," & _
                "线路名称 ='" & sLineName & "'," & _
                "线路简称 = '" & strBrriName & "'," & _
                "英文线名 = '" & strEngName & "'," & _
                "线路总长 = '" & nLong & "'," & _
                "线路编号 = '" & sLineNumber & "'," & _
                "备注 = '" & strMemo & "' " & _
                "where ID = " & nID & ""

        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()

        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Str = "update 车站信息 set " & _
                "线路名称 ='" & sLineName & "'" & _
                "where 线路名称 = '" & sBeforLineName & "'"
        Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom1.ExecuteNonQuery()
        MyConn.Close()
        Call readNetData()
    End Sub

    '删除线路
    Public Sub DeleteNetLine(ByVal strLineName As String)
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim Str As String
        Str = "delete * from 路网线路信息 where 线路名称='" & strLineName & "'"
        Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom.ExecuteNonQuery()
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Str = "delete * from 车站信息 where 线路名称='" & strLineName & "'"
        Dim MyCom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom1.ExecuteNonQuery()
        MyConn.Close()

        Call readNetData()

    End Sub

    '添加车站
    Public Sub AddNetStation(ByVal strLineName As String, _
                                ByVal strStaName As String, _
                                ByVal strStaPrintName As String, _
                                ByVal intDownSeq As Integer, _
                                ByVal strStyle As String, _
                                ByVal strEnglName As String, _
                                ByVal strEnglAbbrName As String, _
                                ByVal sStaCode As String, _
                                ByVal strSingOrDoubLine As String, _
                                ByVal strCharacter As String, _
                                ByVal XCoordinate As Long, _
                                ByVal Ycoordinate As Long, _
                                ByVal StaPic As String, _
                                ByVal strMemo As String)

        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim Str As String
        Str = "update 车站信息 set " & _
                "下行站序 =下行站序+1" & " " & _
                "where 线路名称 = '" & strLineName & "' and 下行站序 >= " & intDownSeq & ""
        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()


        Str = "insert into 车站信息(下行站序,线路名称,站名,输出站名,英文站名,车站代码,英文简称,单双线,类型,性质,X坐标,Y坐标,站形图,备注) values('" & _
                intDownSeq & "', '" & _
                strLineName & "', '" & _
                strStaName & "', '" & _
                strStaPrintName & "', '" & _
                strEnglName & "', '" & _
                sStaCode & "','" & _
                strEnglAbbrName & "', '" & _
                strSingOrDoubLine & "', '" & _
                strStyle & "', '" & _
                strCharacter & "', '" & _
                XCoordinate & "', '" & _
                Ycoordinate & "', '" & _
                StaPic & "', '" & _
                strMemo & "')"
        Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom1.ExecuteNonQuery()
        MyConn.Close()
        Call readNetData()
    End Sub

    '修改车站
    Public Sub EditNetStation(ByVal nID As Integer, _
                                ByVal strLineName As String, _
                                ByVal strStaName As String, _
                                ByVal strStaPrintName As String, _
                                ByVal intDownSeq As Integer, _
                                ByVal strStyle As String, _
                                ByVal strEnglName As String, _
                                ByVal strEnglAbbrName As String, _
                                ByVal sStaCode As String, _
                                ByVal strSingOrDoubLine As String, _
                                ByVal strCharacter As String, _
                                ByVal XCoordinate As Long, _
                                ByVal Ycoordinate As Long, _
                                ByVal StaPic As String, _
                                ByVal strMemo As String)
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim Str As String
        'If StaPic = "" Then StaPic = "无"
        Str = "update 车站信息 set " & _
                "下行站序 ='" & intDownSeq & "'," & _
                "线路名称 ='" & strLineName & "'," & _
                "站名 ='" & strStaName & "'," & _
                "输出站名 ='" & strStaPrintName & "'," & _
                "英文站名 ='" & strEnglName & "'," & _
                "车站代码 ='" & sStaCode & "'," & _
                "英文简称 ='" & strEnglAbbrName & "'," & _
                "单双线 ='" & strSingOrDoubLine & "'," & _
                "类型 ='" & strStyle & "'," & _
                "性质 ='" & strCharacter & "'," & _
                "X坐标 ='" & XCoordinate & "'," & _
                "Y坐标 ='" & Ycoordinate & "'," & _
                "站形图 ='" & StaPic & "'," & _
                "备注 = '" & strMemo & "' " & _
                "where 序号 = " & nID & ""
        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()
        MyConn.Close()
        Call readNetData()
    End Sub

    '删除车站
    Public Sub DeleteNetStation(ByVal nStaseq As Integer, ByVal strLineName As String, ByVal strStaName As String)
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim Str As String
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Str = "delete * from 车站信息 where 线路名称='" & strLineName & "' and 站名='" & strStaName & "'"
        Dim MyCom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom1.ExecuteNonQuery()

        Str = "update 车站信息 set " & _
                "下行站序 =下行站序-1" & " " & _
                "where 线路名称 = '" & strLineName & "' and 下行站序 > " & nStaseq & ""
        Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom.ExecuteNonQuery()
        MyConn.Close()
        Call readNetData()
    End Sub

    '获得站名//在线路径路生成的窗体中----------
    Public Function GetStaNameInDiTuStructure(ByVal LineSta As String) As String
        GetStaNameInDiTuStructure = ""
        Dim i As Integer
        For i = Len(LineSta) To 1 Step -1
            If Mid(LineSta, i, 1) = ")" Then
                GetStaNameInDiTuStructure = Right(LineSta, Len(LineSta) - i)
                Exit Function
            End If
        Next i
    End Function

    '获得线路名
    Public Function GetLineNameInDiTuStructure(ByVal LineSta As String) As String
        GetLineNameInDiTuStructure = ""
        Dim i As Integer
        For i = Len(LineSta) To 1 Step -1
            If Mid(LineSta, i, 1) = ")" Then
                GetLineNameInDiTuStructure = Mid(LineSta, 2, i - 2)
                Exit Function
            End If
        Next i
    End Function


    '自动生成列车区间标尺种类，在添加列车交路窗口中用到
    Public Sub AutoCreateTrainSecScale(ByVal sJiaoLuName As String, ByVal UporDown As Integer, ByVal strPath() As String)
        Dim str As String
        Dim secScale() As String
        ReDim secScale(0)
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim i, j As Integer
        Dim p As Integer
        Dim sFirsta As String
        Dim sSecsta As String
        Dim sSecName As String
        Dim strTime As String
        If sJiaoLuName <> "" Then

            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            str = "delete * from 列车运行标尺信息 where 交路名称='" & sJiaoLuName & "'"
            Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
            Mcom.ExecuteNonQuery()

            'Dim strTable2 As String = "select distinct 标尺名称 from 列车运行标尺"
            'Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
            'Dim myDataSet2 As Data.DataSet = New Data.DataSet
            'Mydc2.Fill(myDataSet2, "列车运行标尺")
            'Dim myTable2 As Data.DataTable
            'myTable2 = myDataSet2.Tables("列车运行标尺")
            'If myTable2.Rows.Count > 0 Then
            '    ReDim secScale(myTable2.Rows.Count)
            '    For i = 1 To myTable2.Rows.Count
            '        secScale(i) = Trim(myTable2.Rows(i - 1).Item("标尺名称"))
            '    Next
            'End If

            Dim ifIn As Integer
            For i = 1 To UBound(SectionScaleInf)
                ifIn = 0
                For j = 1 To UBound(secScale)
                    If SectionScaleInf(i).sScaleName = secScale(j) Then
                        ifIn = 1
                        Exit For
                    End If
                Next
                If ifIn = 0 Then
                    ReDim Preserve secScale(UBound(secScale) + 1)
                    secScale(UBound(secScale)) = SectionScaleInf(i).sScaleName
                End If
            Next

            For p = 1 To UBound(secScale)
                For i = 2 To UBound(strPath)
                    sFirsta = strPath(i - 1)
                    sSecsta = strPath(i)
                    If UporDown = 1 Then
                        sSecName = sFirsta & "->" & sSecsta
                    Else
                        sSecName = sSecsta & "->" & sFirsta
                    End If

                    Dim sUpOrDown As String
                    If UporDown Mod 2 = 0 Then
                        sUpOrDown = "上行"
                    Else
                        sUpOrDown = "下行"
                    End If
                    strTime = GetCurSectionRunTime(secScale(p), sSecName, sUpOrDown)

                    'Dim strTable3 As String = "select * from 列车运行标尺 where 区间名称='" & sSecName & "' and  标尺名称='" & secScale(p) & "' order by 标尺编号"
                    'Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
                    'Dim myDataSet3 As Data.DataSet = New Data.DataSet
                    'Mydc3.Fill(myDataSet3, "列车运行标尺")
                    'Dim myTable3 As Data.DataTable
                    'myTable3 = myDataSet3.Tables("列车运行标尺")
                    'If myTable3.Rows.Count > 0 Then
                    '    If UporDown = 1 Then
                    '        strTime = Trim(myTable3.Rows(0).Item("下行运行时分"))
                    '    Else
                    '        strTime = Trim(myTable3.Rows(0).Item("上行运行时分"))
                    '    End If
                    '    If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()

                    str = "insert into 列车运行标尺信息 (交路名称,运行种类,区间名称,序号,标尺名称,运行时间) values ('" & _
                        sJiaoLuName & "', '" & _
                        secScale(p) & "', '" & _
                        sSecName & "', '" & _
                       i - 1 & "', '" & _
                       secScale(p) & "', '" & _
                       strTime & "')"
                    Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                    Mcom1.ExecuteNonQuery()
                Next i
            Next p
            MyConn.Close()
        Else
            MsgBox("先选择交路名称!", , "提示")
        End If
    End Sub

    '自动生成列车停站标尺种类，在添加列车交路窗口中用到
    Public Sub AutoCreateTrainStaScale(ByVal sJiaoLuName As String, ByVal UporDown As Integer, ByVal strPath() As String)
        Dim str As String
        Dim secScale() As String
        ReDim secScale(0)
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim i As Integer
        Dim j As Integer
        Dim p As Integer
        Dim sStaName As String
        Dim strTime As String
        If sJiaoLuName <> "" Then
            ReDim TrainZYInf(0)

            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            str = "delete * from 列车停站标尺信息 where 交路名称='" & sJiaoLuName & "'"
            Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
            Mcom.ExecuteNonQuery()

            Dim ifIn As Integer
            For i = 1 To UBound(StopScaleInf)
                ifIn = 0
                For j = 1 To UBound(secScale)
                    If StopScaleInf(i).sScaleName = secScale(j) Then
                        ifIn = 1
                        Exit For
                    End If
                Next
                If ifIn = 0 Then
                    ReDim Preserve secScale(UBound(secScale) + 1)
                    secScale(UBound(secScale)) = StopScaleInf(i).sScaleName
                End If
            Next

            strTime = "0.00"
            For p = 1 To UBound(secScale)
                For i = 1 To UBound(strPath)

                    Dim sUpOrDown As String
                    If UporDown Mod 2 = 0 Then
                        sUpOrDown = "上行"
                    Else
                        sUpOrDown = "下行"
                    End If
                    Dim sStaStartOrEnd As String

                    If i = 1 Then
                        sStaStartOrEnd = "始发站"
                    ElseIf i = UBound(strPath) Then
                        sStaStartOrEnd = "终到站"
                    Else
                        sStaStartOrEnd = "中间站"
                    End If
                    sStaName = strPath(i)
                    strTime = GetCurStationStopTime(secScale(p), sStaName, sUpOrDown, sStaStartOrEnd)
                    If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                    str = "insert into 列车停站标尺信息 (交路名称,停站种类,车站名称,序号,标尺名称,停站时间) values ('" & _
                        sJiaoLuName & "', '" & _
                        secScale(p) & "', '" & _
                        sStaName & "', '" & _
                       i & "', '" & _
                       secScale(p) & "', '" & _
                       strTime & "')"
                    Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                    Mcom1.ExecuteNonQuery()
                Next i
            Next p
            MyConn.Close()
        Else
            MsgBox("先选择交路名称!", , "提示")
        End If
    End Sub

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

    '系统变量
    Public Sub InitSystemViraient()
        '从数据库中读入
        Dim i As Integer
        Dim strTable3 As String = "select * from 系统参数表 order by 序号"
        Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
        '创建一个Datasetd
        Dim myDataSet3 As Data.DataSet = New Data.DataSet
        Mydc3.Fill(myDataSet3, "系统参数表")
        Dim myTable3 As Data.DataTable
        myTable3 = myDataSet3.Tables("系统参数表")
        SystemPara.SystemStyle = ""
        SystemPara.sPicFilePath = ""
        SystemPara.sUserCompanyName = ""
        For i = 1 To myTable3.Rows.Count
            Select Case Trim(myTable3.Rows(i - 1).Item("参数名"))
                Case "底图图片路径"
                    SystemPara.sPicFilePath = myTable3.Rows(i - 1).Item("数值").ToString.Trim
                Case "使用单位名称"
                    SystemPara.sUserCompanyName = myTable3.Rows(i - 1).Item("数值").ToString.Trim
                Case "系统方式"
                    SystemPara.SystemStyle = myTable3.Rows(i - 1).Item("数值").ToString.Trim
            End Select
        Next
    End Sub

    Public Sub InputStopAndSecScaleinf()
        Dim myWS As dao.Workspace
        Dim DE As dao.DBEngine = New dao.DBEngine
        myWS = DE.Workspaces(0)
        Dim dFile As dao.Database
        Dim sFile As dao.Recordset
        Dim i As Integer

        dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        sFile = dFile.OpenRecordset("select * from 列车运行标尺 order by 区间序号")
        Dim nNum As Integer
        nNum = 0
        If sFile.RecordCount > 0 Then
            sFile.MoveLast()
            nNum = sFile.RecordCount
        End If

        ReDim SectionScaleInf(0)
        If nNum > 0 Then
            '导入至sectionscaleinf()
            ReDim SectionScaleInf(nNum)
            sFile.MoveFirst()
            For i = 1 To nNum
                SectionScaleInf(i).sSecName = sFile("区间名称").Value.ToString.Trim
                SectionScaleInf(i).sScaleNum = sFile("标尺编号").Value.ToString.Trim
                SectionScaleInf(i).sScaleName = sFile("标尺名称").Value.ToString.Trim
                SectionScaleInf(i).sDownRunTime = sFile("下行运行时分").Value.ToString.Trim
                SectionScaleInf(i).sUpRunTime = sFile("上行运行时分").Value.ToString.Trim
                sFile.MoveNext()
            Next
        End If

        sFile = dFile.OpenRecordset("select * from 列车停站标尺 order by 车站序号")
        nNum = 0
        If sFile.RecordCount > 0 Then
            sFile.MoveLast()
            nNum = sFile.RecordCount
        End If

        ReDim StopScaleInf(0)
        If nNum > 0 Then
            '导入至sectionscaleinf()
            ReDim StopScaleInf(nNum)
            sFile.MoveFirst()
            For i = 1 To nNum
                StopScaleInf(i).sStaName = sFile("车站名称").Value.ToString.Trim
                StopScaleInf(i).sScaleNum = sFile("标尺编号").Value.ToString.Trim
                StopScaleInf(i).sScaleName = sFile("标尺名称").Value.ToString.Trim
                StopScaleInf(i).sDownStopTime = sFile("下行停站时间").Value.ToString.Trim
                StopScaleInf(i).sUpStopTime = sFile("上行停站时间").Value.ToString.Trim
                StopScaleInf(i).sDownStartStopTime = sFile("下行始发停站时间").Value.ToString.Trim
                StopScaleInf(i).sDownEndStopTime = sFile("下行终到停站时间").Value.ToString.Trim
                StopScaleInf(i).sUpStartStopTime = sFile("上行始发停站时间").Value.ToString.Trim
                StopScaleInf(i).sUpEndStopTime = sFile("上行终到停站时间").Value.ToString.Trim
                sFile.MoveNext()
            Next
        End If
    End Sub

    '得到基准标尺的区间时间
    Public Function GetCurSectionRunTime(ByVal sScaleName As String, ByVal sSecName As String, ByVal sUporDown As String) As String
        Dim i As Integer
        GetCurSectionRunTime = "0.00"
        For i = 1 To UBound(SectionScaleInf)
            If SectionScaleInf(i).sScaleName = sScaleName And SectionScaleInf(i).sSecName = sSecName Then
                If sUporDown = "下行" Then
                    GetCurSectionRunTime = SectionScaleInf(i).sDownRunTime
                Else
                    GetCurSectionRunTime = SectionScaleInf(i).sUpRunTime
                End If
                Exit For
            End If
        Next

    End Function

    '得到基准标尺的停站时间
    Public Function GetCurStationStopTime(ByVal sScaleName As String, ByVal sStaName As String, ByVal sUporDown As String, ByVal ScurStaState As String) As String
        Dim i As Integer
        GetCurStationStopTime = "0.00"
        For i = 1 To UBound(StopScaleInf)
            If StopScaleInf(i).sScaleName = sScaleName And StopScaleInf(i).sStaName = sStaName Then
                Select Case ScurStaState
                    Case "中间站"
                        If sUporDown = "下行" Then
                            GetCurStationStopTime = StopScaleInf(i).sDownStopTime
                        Else
                            GetCurStationStopTime = StopScaleInf(i).sUpStopTime
                        End If
                    Case "始发站"
                        If sUporDown = "下行" Then
                            GetCurStationStopTime = StopScaleInf(i).sDownStartStopTime
                        Else
                            GetCurStationStopTime = StopScaleInf(i).sUpStartStopTime
                        End If
                    Case "终到站"
                        If sUporDown = "下行" Then
                            GetCurStationStopTime = StopScaleInf(i).sDownEndStopTime
                        Else
                            GetCurStationStopTime = StopScaleInf(i).sUpEndStopTime
                        End If
                End Select

                Exit For
            End If
        Next
    End Function

    '点到线段上点的最短距离,(X1,Y1)(X2,Y2)为线段两端点,(Mx,My)为任意点
    Public Function Getdistance(ByVal X1 As Single, ByVal Y1 As Single, ByVal X2 As Single, ByVal Y2 As Single, ByVal Xm As Single, ByVal Ym As Single) As Single
        Dim ResultTmp As Single
        Dim AA As Single
        Dim BB As Single
        Dim CC As Single
        Dim K1 As Single
        Dim distance1 As Single     '到(x1,y1)距离
        Dim distance2 As Single     '到(x2,y2)距离
        Dim distanceLine As Single     '到直线的距离
        Dim ProX As Single
        Dim ProY As Single

        AA = Y2 - Y1
        BB = X1 - X2
        CC = X2 * Y1 - X1 * Y2

        If BB <> 0 Then
            K1 = (-AA) / BB
        Else
            K1 = 9999999
        End If
        ProX = (K1 * (Ym - Y1) + K1 * K1 * X1 + Xm) / (1 + K1 * K1)
        ProY = (Y1 + K1 * K1 * Ym - K1 * X1 + K1 * Xm) / (1 + K1 ^ 2)
        If BB = 0 Then
            ProX = X1
            ProY = Ym
        End If

        distanceLine = Math.Abs(AA * Xm + BB * Ym + CC) / Math.Sqrt(AA * AA + BB * BB)
        distance1 = Math.Sqrt((Ym - Y1) ^ 2 + (Xm - X1) ^ 2)
        distance2 = Math.Sqrt((Ym - Y2) ^ 2 + (Xm - X2) ^ 2)

        If distanceLine < 2.0 Then    '三点在同一直线上
            If (ProX >= Math.Min(X1, X2) And ProX <= Math.Max(X1, X2)) And (ProY >= Math.Min(Y1, Y2) And ProY <= Math.Max(Y1, Y2)) Then
                distanceLine = 0
            Else
                distanceLine = 9999999      '三点在同一直线上,并且(Xm,Ym)在线段之外
            End If
        End If

        If (ProX >= Math.Min(X1, X2) And ProX <= Math.Max(X1, X2)) And (ProY >= Math.Min(Y1, Y2) And ProY <= Math.Max(Y1, Y2)) Then
            ResultTmp = distanceLine
        Else
            If distance1 >= distance2 Then
                ResultTmp = distance2
            Else
                ResultTmp = distance1
            End If
        End If
        Getdistance = ResultTmp
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
                gTmp.DrawString(StrTmp, f, b, 0, 0)
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

        End Select
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

    '得到余弦角
    Public Function CosAngle(ByVal Xa As Single, ByVal Ya As Single, ByVal X1 As Single, ByVal Y1 As Single, ByVal X2 As Single, ByVal Y2 As Single) As Single
        Dim cosAlf As Single
        Dim tmpX1 As Single
        Dim tmpX2 As Single
        Dim tmpY1 As Single
        Dim tmpY2 As Single
        tmpX1 = X1 - Xa
        tmpY1 = Y1 - Ya
        tmpX2 = X2 - Xa
        tmpY2 = Y2 - Ya
        cosAlf = (tmpX1 * tmpX2 + tmpY1 * tmpY2) / (Math.Sqrt(tmpX1 ^ 2 + tmpY1 ^ 2) * Math.Sqrt(tmpX2 ^ 2 + tmpY2 ^ 2))
        CosAngle = Math.Acos(cosAlf)
        CosAngle = CosAngle * 180 / Math.PI
    End Function


    Public Sub GetSubStringFromLongString(ByRef ReturnStr() As String, ByVal sStr As String)
        If Right(sStr, 1) = "," Or Right(sStr, 1) = "，" Then
        Else
            sStr = sStr & ","
        End If
        ReDim ReturnStr(0)

        Dim i, j As Integer
        i = 1
        For j = 1 To Len(sStr)
            If Mid(sStr, j, 1) = "," Or Mid(sStr, j, 1) = "，" Then
                If Trim(Mid(sStr, i, j - i)) <> "" Then
                    ReDim Preserve ReturnStr(UBound(ReturnStr) + 1)
                    ReturnStr(UBound(ReturnStr)) = Mid(sStr, i, j - i)
                    i = j + 1
                End If
            End If
        Next j
    End Sub
    'Public Sub InputTrainJiaoLuInf()
    '    Dim myWS As DAO.Workspace
    '    Dim DE As DAO.DBEngine = New DAO.DBEngine
    '    myWS = DE.Workspaces(0)
    '    Dim dFile As DAO.Database
    '    Dim sFile As DAO.Recordset
    '    Dim i As Integer

    '    dFile = myWS.OpenDatabase(g_strNetMainPath)
    '    sFile = dFile.OpenRecordset("select * from 列车信息 order by 序号")
    '    Dim nNum As Integer
    '    nNum = 0
    '    If sFile.RecordCount > 0 Then
    '        sFile.MoveLast()
    '        nNum = sFile.RecordCount
    '    End If

    '    ReDim TrainJiaoLuInf(0)
    '    If nNum > 0 Then
    '        '导入至sectionscaleinf()
    '        ReDim TrainJiaoLuInf(nNum)
    '        sFile.MoveFirst()
    '        For i = 1 To nNum
    '            TrainJiaoLuInf(i).sJiaoLuName = sFile("交路名").Value.ToString.Trim
    '            TrainJiaoLuInf(i).sStartSta = sFile("始发站").Value.ToString.Trim
    '            TrainJiaoLuInf(i).sEndSta = sFile("终到站").Value.ToString.Trim
    '            TrainJiaoLuInf(i).sUpOrDown = sFile("上下行").Value.ToString.Trim
    '            sFile.MoveNext()
    '        Next
    '    End If
    'End Sub
    Public Function FromGudaoNumToGuDaoName(ByVal sNum As String, ByVal nSta As Integer) As String
        FromGudaoNumToGuDaoName = ""
        Dim i As Integer
        For i = 1 To UBound(StationInf(nSta).sStLineNo)
            If StationInf(nSta).sStLineNo(i) = sNum Then
                FromGudaoNumToGuDaoName = StationInf(nSta).sGuDaoName(i)
                Exit Function
            End If
        Next
    End Function

    '得到重复的字符串
    Public Function GetSameString(ByVal Str As System.Collections.Generic.List(Of String)) As System.Collections.Generic.List(Of String)
        Dim i As Integer
        Dim j As Integer
        Dim tmpStr As New System.Collections.Generic.List(Of String)
        For i = 1 To Str.Count
            For j = i + 1 To Str.Count
                If Str.Item(j - 1) = Str.Item(i - 1) Then
                    tmpStr.Add(Str.Item(j - 1))
                End If
            Next
        Next
        Return tmpStr
    End Function
End Module
