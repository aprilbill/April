Module modODSInputAccessTimetabel
    ''' <summary>
    ''' 通过时刻表名,在ACCESS数据库中找时刻表对应的ID号
    ''' </summary>
    ''' <param name="sTimeTableName">时刻表名称</param>
    ''' <returns>返回时刻表ID号</returns>
    ''' <remarks></remarks>
    Public Function GetTimetableIDInAccessDatabase(ByVal sTimeTableName As String) As String
        GetTimetableIDInAccessDatabase = ""
        Dim DE As dao.DBEngine = New dao.DBEngine
        Dim myWS As dao.Workspace
        myWS = DE.Workspaces(0)
        Dim dFile As dao.Database
        Dim sFile As dao.Recordset
        Dim strTable3 As String = "select 时刻表ID from 时刻表信息表 where 时刻表名称='" & sTimeTableName & "'"
        Dim g_strNetMainPathOpenInfor As String
        g_strNetMainPathOpenInfor = ";UID=admin;PWD=" & InputDatabasePassWord & ";DATABASE=" & InputDatabasePath & InputDatabaseName & ""
        dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        sFile = dFile.OpenRecordset(strTable3)
        If sFile.RecordCount > 0 Then
            GetTimetableIDInAccessDatabase = sFile.Fields("时刻表ID").Value.ToString
        End If
    End Function
    ''' <summary>
    ''' 导入所有运行图数据
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InputACCESSTimeTable(ByVal sTrainDiagramID As String, ByVal sTimetableName As String)
        Dim sTableID As String
        Dim sCurTable As String
        sCurTable = ""
        sTableID = GetTimetableIDInAccessDatabase(sTimetableName)
        'Dim tablename() As String = {"底图结构", "车底信息", "车底折返时间标准", "车站间隔时间", "追踪间隔时间", "列车运行标尺信息", sTableID & "列车时刻信息", sTableID & "车底使用方案", "列车停站标尺信息", "运行图系统参数表", "时刻表车站顺序", "发车间隔安排", "列车信息", "线路区间信息", "车站信息", "路网线路信息", "线段信息表", "列车停站标尺", "列车运行标尺"}
        'Progress.ProgressForm.StartProgress(19, "正在导入运行图相关数据，请稍候...")
        Dim tablename() As String = {"底图结构", "车底信息", "车底折返时间标准", "车站间隔时间", "追踪间隔时间", "列车运行标尺信息", sTableID & "列车时刻信息", sTableID & "车底使用方案", "列车停站标尺信息", "运行图系统参数表", "时刻表车站顺序", "发车间隔安排", "列车信息", "线路区间信息", "车站信息", "路网线路信息", "列车停站标尺", "列车运行标尺"}
        Progress.ProgressForm.StartProgress(18, "正在导入运行图相关数据，请稍候...")
        Try
            For Each table As String In tablename
                Progress.ProgressForm.PerformStep()
                Dim DE As dao.DBEngine = New dao.DBEngine
                Dim myWS As dao.Workspace
                myWS = DE.Workspaces(0)
                Dim dFile As dao.Database
                Dim sFile As dao.Recordset
                Dim strTable3 As String = "select * from " & table
                Dim g_strNetMainPathOpenInfor As String
                g_strNetMainPathOpenInfor = ";UID=admin;PWD=" & InputDatabasePassWord & ";DATABASE=" & InputDatabasePath & InputDatabaseName & ""
                dFile = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
                sFile = dFile.OpenRecordset(strTable3)
                Dim p As Integer = 0
                Dim nNum As Integer
                nNum = 0
                If sFile.RecordCount > 0 Then
                    sFile.MoveLast()
                    nNum = sFile.RecordCount
                End If
                sCurTable = table
                If sFile.RecordCount > 0 Then
                    sFile.MoveFirst()
                    Select Case table
                        Case sTableID & "列车时刻信息"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_TIMETABLEINFO.AddTMS_TIMETABLEINFORow(sTrainDiagramID, sFile.Fields("ID").Value, sFile.Fields("车次").Value, sFile.Fields("车站名称").Value, sFile.Fields("到点").Value, sFile.Fields("发点").Value, sFile.Fields("停站股道").Value)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_TIMETABLEINFO")
                        Case sTableID & "车底使用方案"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_STOCKUSINGINFO.AddTMS_STOCKUSINGINFORow(sTrainDiagramID, sFile.Fields("车底顺序").Value, sFile.Fields("车底ID").Value, sFile.Fields("连挂车次").Value, sFile.Fields("连接顺序").Value, _
                        sFile.Fields("列车性质").Value, sFile.Fields("交路类型").Value, sFile.Fields("输出车次").Value, sFile.Fields("标尺类型").Value, _
                        sFile.Fields("停站标尺").Value, sFile.Fields("是否折返约束").Value, sFile.Fields("运行线线型").Value, sFile.Fields("运行线颜色").Value, sFile.Fields("运行线线宽").Value, _
                        sFile.Fields("车底线线型").Value, sFile.Fields("车底线颜色").Value, sFile.Fields("车底线线宽").Value)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_STOCKUSINGINFO")
                        Case "底图结构"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_DIASTRUCTINFO.AddTMS_DIASTRUCTINFORow( _
                                  sFile.Fields("底图名称").Value, sFile.Fields("站序").Value, sFile.Fields("站名").Value, sFile.Fields("是否显示").Value, _
                                 sFile.Fields("下站ID").Value, sFile.Fields("上站ID").Value, sFile.Fields("线路名").Value, sFile.Fields("Y坐标").Value, sFile.Fields("是否默认").Value, sTrainDiagramID)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_DIASTRUCTINFO")
                        Case "车底信息"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_TRAINUSINGINFO.AddTMS_TRAINUSINGINFORow( _
                                  sFile.Fields("车底ID").Value, sFile.Fields("车底类型").Value, sTrainDiagramID, "无", "无")
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_TRAINUSINGINFO")
                        Case "车底折返时间标准"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_TRAINRETURNTIME.AddTMS_TRAINRETURNTIMERow( _
                                  sTrainDiagramID, sFile.Fields("车站名称").Value, sFile.Fields("车底类型").Value, sFile.Fields("站前折返时间").Value, sFile.Fields("站后折返时间").Value, sFile.Fields("立即折返时间").Value, sFile.Fields("到达股道至折返线时间").Value, sFile.Fields("折返线至出发股道时间").Value, sFile.Fields("到达发到间隔").Value, sFile.Fields("出发发到间隔").Value)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_TRAINRETURNTIME")
                        Case "车站间隔时间"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_STATIONTIME.AddTMS_STATIONTIMERow( _
                                  sTrainDiagramID, sFile.Fields("车站名称").Value, sFile.Fields("τ不上").Value, sFile.Fields("τ会上").Value, sFile.Fields("τ连1上").Value, sFile.Fields("τ连2上").Value, sFile.Fields("τ通1上").Value, sFile.Fields("τ通2上").Value, sFile.Fields("τ到发上").Value, sFile.Fields("τ发到上").Value, _
                                  sFile.Fields("τ发发上").Value, sFile.Fields("τ到到上").Value, sFile.Fields("τ不下").Value, sFile.Fields("τ会下").Value, sFile.Fields("τ连1下").Value, sFile.Fields("τ连2下").Value, sFile.Fields("τ通1下").Value, sFile.Fields("τ通2下").Value, sFile.Fields("τ到发下").Value, sFile.Fields("τ发到下").Value, _
                                  sFile.Fields("τ发发下").Value, sFile.Fields("τ到到下").Value)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_STATIONTIME")
                        Case "追踪间隔时间"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_SECTIONTIME.AddTMS_SECTIONTIMERow( _
                                  sTrainDiagramID, sFile.Fields("车站名称").Value, sFile.Fields("类型").Value, sFile.Fields("同向发发").Value, sFile.Fields("同向发到").Value, sFile.Fields("同向发通").Value, sFile.Fields("同向到发").Value, sFile.Fields("同向到到").Value, sFile.Fields("同向到通").Value, sFile.Fields("同向通发").Value, _
                                  sFile.Fields("同向通到").Value, sFile.Fields("同向通通").Value, sFile.Fields("对向发发").Value, sFile.Fields("对向发到").Value, sFile.Fields("对向发通").Value, sFile.Fields("对向到发").Value, sFile.Fields("对向到到").Value, sFile.Fields("对向到通").Value, sFile.Fields("对向通发").Value, sFile.Fields("对向通到").Value, _
                                  sFile.Fields("对向通通").Value)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_SECTIONTIME")
                        Case "列车运行标尺信息"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_RUNSCALEINFO.AddTMS_RUNSCALEINFORow( _
                                  sTrainDiagramID, sFile.Fields("交路名称").Value, sFile.Fields("运行种类").Value, sFile.Fields("序号").Value, sFile.Fields("区间名称").Value, sFile.Fields("运行时间").Value, sFile.Fields("标尺名称").Value)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_RUNSCALEINFO")
                        Case "列车停站标尺信息"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_STOPSCALEINFO.AddTMS_STOPSCALEINFORow( _
                                  sTrainDiagramID, sFile.Fields("交路名称").Value, sFile.Fields("停站种类").Value, sFile.Fields("序号").Value, sFile.Fields("车站名称").Value, sFile.Fields("停站时间").Value, sFile.Fields("标尺名称").Value)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_STOPSCALEINFO")
                        Case "运行图系统参数表"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_TIMETABLEPARAMETER.AddTMS_TIMETABLEPARAMETERRow( _
                                   sFile.Fields("序号").Value, sFile.Fields("参数名").Value, sFile.Fields("数值").Value, sTrainDiagramID)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_TIMETABLEPARAMETER")
                        Case "时刻表车站顺序"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_TIMETABLESTASEQ.AddTMS_TIMETABLESTASEQRow( _
                                  sFile.Fields("区段名称").Value, sFile.Fields("序号").Value, sFile.Fields("车站名称").Value, sTrainDiagramID)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_TIMETABLESTASEQ")
                        Case "发车间隔安排"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_TRAININTERVALINFO.AddTMS_TRAININTERVALINFORow( _
                                  sFile.Fields("交路类型").Value, sFile.Fields("铺画类型").Value, sFile.Fields("时间段").Value, sFile.Fields("起始时间").Value, sFile.Fields("终止时间").Value, sFile.Fields("发车间隔").Value, sFile.Fields("周期时间").Value, sFile.Fields("运行标尺").Value, sFile.Fields("停站标尺").Value, sFile.Fields("始发折返").Value, sFile.Fields("终到折返").Value, sFile.Fields("车底数量").Value, sFile.Fields("间隔一").Value, sFile.Fields("数量一").Value, sFile.Fields("间隔二").Value, sFile.Fields("数量二").Value, sTrainDiagramID)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_TRAININTERVALINFO")
                        Case "列车信息"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_TRAININFO.AddTMS_TRAININFORow( _
                                   sTrainDiagramID, sFile.Fields("交路名").Value, sFile.Fields("上下行").Value, sFile.Fields("始发站").Value, sFile.Fields("终到站").Value, sFile.Fields("线路编号").Value, sFile.Fields("目的符").Value, sFile.Fields("列车性质").Value, sFile.Fields("终到运用方式").Value, sFile.Fields("始发运用方式").Value, sFile.Fields("车底类型").Value, sFile.Fields("列车径路").Value, sFile.Fields("种类").Value)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_TRAININFO")
                        Case "线路区间信息"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_SECTIONINFO.AddTMS_SECTIONINFORow( _
                                   sTrainDiagramID, sFile.Fields("线路名称").Value, sFile.Fields("区间编号").Value, sFile.Fields("区间名称").Value, sFile.Fields("区间起始站").Value, sFile.Fields("区间终到站").Value, sFile.Fields("上行区间距离").Value, sFile.Fields("下行区间距离").Value, sFile.Fields("闭塞类型").Value, sFile.Fields("正线数目").Value)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_SECTIONINFO")
                        Case "车站信息"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_STATIONINFO.AddTMS_STATIONINFORow( _
                                   sTrainDiagramID, sFile.Fields("线路名称").Value, sFile.Fields("站名").Value, sFile.Fields("下行站序").Value, sFile.Fields("输出站名").Value, sFile.Fields("英文站名").Value, sFile.Fields("英文简称").Value, sFile.Fields("单双线").Value, sFile.Fields("类型").Value, sFile.Fields("性质").Value, sFile.Fields("站形图").Value, sFile.Fields("X坐标").Value, sFile.Fields("Y坐标").Value, sFile.Fields("备注").Value, sFile.Fields("车站代码").Value)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_STATIONINFO")
                        Case "路网线路信息"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_LINEINFO.AddTMS_LINEINFORow( _
                                   sTrainDiagramID, sFile.Fields("序号").Value, sFile.Fields("线路名称").Value, sFile.Fields("线路简称").Value, sFile.Fields("英文线名").Value, sFile.Fields("线路编号").Value, sFile.Fields("线路总长").Value, sFile.Fields("备注").Value)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_LINEINFO")
                        Case "线段信息表"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_LINEDRAW.AddTMS_LINEDRAWRow(sFile.Fields("车站名称").Value, sFile.Fields("线段编号").Value, sFile.Fields("轨道电路编号").Value, sFile.Fields("线段类型").Value, sFile.Fields("股道类型").Value, sFile.Fields("股道用途").Value, _
                                sFile.Fields("股道使用顺序").Value, sFile.Fields("线段长度").Value, sFile.Fields("股道或道岔编号").Value, sFile.Fields("控制模块").Value, Int(sFile.Fields("X1坐标").Value), Int(sFile.Fields("X2坐标").Value), Int(sFile.Fields("Y1坐标").Value), _
                                Int(sFile.Fields("Y2坐标").Value), sFile.Fields("左1连接").Value, sFile.Fields("左2连接").Value, sFile.Fields("左3连接").Value, sFile.Fields("右1连接").Value, sFile.Fields("右2连接").Value, sFile.Fields("右3连接").Value, sFile.Fields("备注").Value, sTrainDiagramID)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_LINEDRAW")
                        Case "列车停站标尺"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_STOPSCALE.AddTMS_STOPSCALERow(sFile.Fields("标尺名称").Value, sTrainDiagramID, sFile.Fields("车站序号").Value, sFile.Fields("车站名称").Value, sFile.Fields("标尺编号").Value, sFile.Fields("下行停站时间").Value, sFile.Fields("上行停站时间").Value, _
                                sFile.Fields("下行始发停站时间").Value, sFile.Fields("下行终到停站时间").Value, sFile.Fields("上行终到停站时间").Value, sFile.Fields("上行始发停站时间").Value)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_STOPSCALE")
                        Case "列车运行标尺"
                            For p = 1 To nNum
                                TMSlocalDataSet.TMS_RUNSCALE.AddTMS_RUNSCALERow(sTrainDiagramID, sFile.Fields("区间序号").Value, sFile.Fields("线路名称").Value, sFile.Fields("区间名称").Value, sFile.Fields("标尺编号").Value, sFile.Fields("标尺名称").Value, sFile.Fields("下行运行时分").Value, _
                                sFile.Fields("上行运行时分").Value, sFile.Fields("下行起车时分").Value, sFile.Fields("下行停车时分").Value, sFile.Fields("上行起车时分").Value, sFile.Fields("上行停车时分").Value)
                                sFile.MoveNext()
                            Next
                            TMSlocalDataSet.Update("TMS_RUNSCALE")
                    End Select
                End If
            Next
            Progress.ProgressForm.EndProgress()

        Catch ex As Exception
            Progress.ProgressForm.EndProgress()
            MsgBox(sCurTable & " 表导入过程发生错误，请您认真检查.tpm文件内的数据是否符合要求？" & vbCrLf & ex.ToString)
            Exit Sub
        End Try

        If MsgBox("运行图数据导入成功，是否立即进入运行图浏览界面并计算该运行图相关指标？", MsgBoxStyle.YesNo, "确认操作") = MsgBoxResult.Yes Then
            TMS.ODSPubpara.DiagramSelect = sTrainDiagramID
            Dim nf As New TMS.frmODSTimeTableMain
            Call TMS.LoadDiagramData("打开运行图")
            TMS.ODSPubpara.sCurShowListState = "显示单线全图"
            nf.Show()
            Dim nf1 As New TMS.frmODSDiagramIndex
            nf1.Show()
        End If
    End Sub


End Module
