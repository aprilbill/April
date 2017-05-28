Imports Microsoft.Office.Interop.Excel

Public Class FrmFishDiagramSet

    Public CurLineName As String

    Private Sub DGVUpFormat_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVUpFormat.CellDoubleClick
        If Me.DGVUpFormat.SelectedCells.Count = 1 AndAlso Me.DGVUpFormat.Columns(Me.DGVUpFormat.SelectedCells(0).ColumnIndex).Name = "适用车站" Then
            Dim nf As New FrmAddOffDutyPlace
            Dim str() As String = Nothing
            For i = 1 To UBound(StationInf)
                If StationInf(i).sStationName <> "" Then
                    nf.ListBoxshiftPlace.Items.Add(StationInf(i).sStationName)
                End If
            Next
            If Me.DGVUpFormat.CurrentCell.Value IsNot Nothing AndAlso Me.DGVUpFormat.CurrentCell.Value.ToString <> "" Then
                str = Me.DGVUpFormat.CurrentCell.Value.ToString.Split(",")
                If UBound(str) >= 0 Then
                    For i As Integer = 0 To UBound(str)
                        nf.ListBox1.Items.Add(str(i))
                    Next
                End If
            End If
            If nf.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Dim strSta As String = ""
                If nf.ListBox1.Items.Count > 0 Then
                    For i As Integer = 0 To nf.ListBox1.Items.Count - 1
                        strSta &= nf.ListBox1.Items(i) & ","
                    Next
                    Me.DGVUpFormat.CurrentCell.Value = strSta.Trim(",")
                End If
            End If
        End If
    End Sub

    Private Sub DGVDownFormat_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVDownFormat.CellDoubleClick
        If Me.DGVDownFormat.SelectedCells.Count = 1 AndAlso Me.DGVDownFormat.Columns(Me.DGVDownFormat.SelectedCells(0).ColumnIndex).Name = "适用车站2" Then
            Dim nf As New FrmAddOffDutyPlace
            Dim str() As String = Nothing
            For i = 1 To UBound(StationInf)
                If StationInf(i).sStationName <> "" Then
                    nf.ListBoxshiftPlace.Items.Add(StationInf(i).sStationName)
                End If
            Next
            If Me.DGVDownFormat.CurrentCell.Value IsNot Nothing AndAlso Me.DGVDownFormat.CurrentCell.Value.ToString <> "" Then
                str = Me.DGVDownFormat.CurrentCell.Value.ToString.Split(",")
                If UBound(str) >= 0 Then
                    For i As Integer = 0 To UBound(str)
                        nf.ListBox1.Items.Add(str(i))
                    Next
                End If
            End If
            If nf.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Dim strSta As String = ""
                If nf.ListBox1.Items.Count > 0 Then
                    For i As Integer = 0 To nf.ListBox1.Items.Count - 1
                        strSta &= nf.ListBox1.Items(i) & ","
                    Next
                    Me.DGVDownFormat.CurrentCell.Value = strSta.Trim(",")
                End If
            End If
        End If
    End Sub

    Private Sub Btn_Save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Save.Click
        Dim str As String = "delete from cs_fishdiagramsetting where lineid='" & CurLineName & "'"
         Globle.Method.UpdateDataForAccess(str)
        If Me.DGVUpFormat.Rows.Count > 1 Then
            For i As Integer = 0 To Me.DGVUpFormat.Rows.Count - 2
                Try
                    str = "Insert into cs_fishdiagramsetting (lineid,id,outputtype,avaplaces,outputname,direction) values('" & CurLineName _
                                        & "'," & Me.DGVUpFormat.Rows(i).Cells("编号").Value.ToString.Trim & _
                                        ",'" & Me.DGVUpFormat.Rows(i).Cells("输出类型").Value.ToString.Trim & _
                                        "','" & Me.DGVUpFormat.Rows(i).Cells("适用车站").Value.ToString.Trim & _
                                        "','" & Me.DGVUpFormat.Rows(i).Cells("输出表头").Value.ToString.Trim & "','上行')"
                     Globle.Method.UpdateDataForAccess(str)
                Catch ex As Exception
                    MsgBox("设置信息有误，请仔细检查设置信息！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                    Exit Sub
                End Try
            Next
        End If
        If Me.DGVDownFormat.Rows.Count > 1 Then
            For i As Integer = 0 To Me.DGVDownFormat.Rows.Count - 2
                Try
                    str = "Insert into cs_fishdiagramsetting (lineid,id,outputtype,avaplaces,outputname,direction) values('" & CurLineName _
                    & "'," & Me.DGVDownFormat.Rows(i).Cells("编号2").Value.ToString.Trim & _
                    ",'" & Me.DGVDownFormat.Rows(i).Cells("输出类型2").Value.ToString.Trim & _
                    "','" & Me.DGVDownFormat.Rows(i).Cells("适用车站2").Value.ToString.Trim & _
                    "','" & Me.DGVDownFormat.Rows(i).Cells("输出表头2").Value.ToString.Trim & "','下行')"
                     Globle.Method.UpdateDataForAccess(str)
                Catch ex As Exception
                    MsgBox("设置信息有误，请仔细检查设置信息！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                    Exit Sub
                End Try
            Next
        End If
        MsgBox("设置成功！", MsgBoxStyle.OkOnly, "提醒")
    End Sub

    Private Sub FrmFishDiagramSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.DGVUpFormat.Rows.Clear()
        Me.DGVDownFormat.Rows.Clear()
        Dim str As String = "select * from cs_fishdiagramsetting t where lineid='" & CurLineName & "' and direction='上行' order by ID"
        Dim tab As Data.DataTable = ReadData(str)
        If tab IsNot Nothing Then
            For Each row As DataRow In tab.Rows
                Me.DGVUpFormat.Rows.Add(row.Item("ID"), row.Item("outputtype").ToString, row.Item("avaplaces").ToString, row.Item("outputname").ToString)
            Next
        End If
        str = "select * from cs_fishdiagramsetting t where lineid='" & CurLineName & "' and direction='下行' order by ID"
        tab = ReadData(str)
        If tab IsNot Nothing Then
            For Each row As DataRow In tab.Rows
                Me.DGVDownFormat.Rows.Add(row.Item("ID"), row.Item("outputtype").ToString, row.Item("avaplaces").ToString, row.Item("outputname").ToString)
            Next
        End If
    End Sub

    Private Sub Btn_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Cancel.Click
        Me.Close()
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub

    Public Sub GetFishPara()
        '==============获取输出格式参数
        MyFishPara = New FishDiaPara(strCurlineID)
        If Me.DGVUpFormat.Rows.Count > 1 Then
            MyFishPara.UpPara = New List(Of FishDiagram)
            For i As Integer = 0 To Me.DGVUpFormat.Rows.Count - 2
                Try
                    Dim tempDia As New FishDiagram(strCurlineID, Me.DGVUpFormat.Rows(i).Cells("编号").Value.ToString.Trim)
                    tempDia.Direction = "上行"
                    tempDia.OutPutType = Me.DGVUpFormat.Rows(i).Cells("输出类型").Value.ToString.Trim
                    tempDia.AvaPlaces = Me.DGVUpFormat.Rows(i).Cells("适用车站").Value.ToString.Trim.Split(",")
                    tempDia.OutPutName = Me.DGVUpFormat.Rows(i).Cells("输出表头").Value.ToString.Trim
                    MyFishPara.UpPara.Add(tempDia)
                Catch ex As Exception
                    MsgBox("设置信息有误，请仔细检查设置信息！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                    Exit Sub
                End Try
            Next
        End If
        If Me.DGVDownFormat.Rows.Count > 1 Then
            MyFishPara.DownPara = New List(Of FishDiagram)
            For i As Integer = 0 To Me.DGVDownFormat.Rows.Count - 2
                Try
                    Dim tempDia As New FishDiagram(strCurlineID, Me.DGVDownFormat.Rows(i).Cells("编号2").Value.ToString.Trim)
                    tempDia.Direction = "下行"
                    tempDia.OutPutType = Me.DGVDownFormat.Rows(i).Cells("输出类型2").Value.ToString.Trim
                    tempDia.AvaPlaces = Me.DGVDownFormat.Rows(i).Cells("适用车站2").Value.ToString.Trim.Split(",")
                    tempDia.OutPutName = Me.DGVDownFormat.Rows(i).Cells("输出表头2").Value.ToString.Trim
                    MyFishPara.DownPara.Add(tempDia)
                Catch ex As Exception
                    MsgBox("设置信息有误，请仔细检查设置信息！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提醒")
                    Exit Sub
                End Try
            Next
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call GetFishPara()

        '================输出钓鱼图
        Dim xlapp As Microsoft.Office.Interop.Excel.Application
        Dim xlbook As Microsoft.Office.Interop.Excel.Workbook
        Dim xlsheet As Microsoft.Office.Interop.Excel.Worksheet

        xlapp = New Microsoft.Office.Interop.Excel.Application()
        xlbook = xlapp.Workbooks.Add
        xlsheet = xlbook.Sheets(1)
        xlsheet.Name = strQCurCSPlanName & "钓鱼图"

        Dim UpArrayData As New List(Of String())
        Dim DownArrayData As New List(Of String())
        Me.ProgressBar1.Maximum = 100
        Me.ProgressBar1.Value = 0
        Me.ProgressBar1.Visible = True
        For i As Integer = 1 To UBound(CSTrainInf)                     '根据格式填写时刻信息、车次车底信息
            If CSTrainInf(i).Arrival Is Nothing OrElse CSTrainInf(i).Starting Is Nothing Then
                Continue For
            End If
            If i Mod 2 = 0 Then
                Dim tempItem() As String = GetFishItem(i, MyFishPara.UpPara)
                UpArrayData.Add(tempItem)
            Else
                Dim tempItem() As String = GetFishItem(i, MyFishPara.DownPara)
                DownArrayData.Add(tempItem)
            End If
            Me.ProgressBar1.Value = 50 * i / UBound(CSTrainInf)
        Next
        Call InPutCSDriverInfo(UpArrayData, MyFishPara.UpPara)              '加入任务信息
        Call InPutCSDriverInfo(DownArrayData, MyFishPara.DownPara)          '加入任务信息
        Dim ToTalArray As List(Of HeBinFishItem) = HeBinArrayData(UpArrayData, DownArrayData)       '上下行合并
        Call ChangeArrayIntToTime(UpArrayData, MyFishPara.UpPara)            '将时刻信息转化为时间字符串
        Call ChangeArrayIntToTime(DownArrayData, MyFishPara.DownPara)           '将时刻信息转化为时间字符串
        For i As Integer = 0 To MyFishPara.UpPara.Count - 1
            xlsheet.Cells(1, i + 1) = MyFishPara.UpPara(i).OutPutName
            If MyFishPara.UpPara(i).OutPutType = "接车表号" OrElse MyFishPara.UpPara(i).OutPutType = "下车表号" Then
                xlsheet.Range(xlsheet.Cells(1, i + 1), xlsheet.Cells(1 + ToTalArray.Count, i + 1)).NumberFormat = "@"
            End If
        Next
        For i As Integer = 0 To MyFishPara.DownPara.Count - 1
            xlsheet.Cells(1, MyFishPara.UpPara.Count + i + 2) = MyFishPara.DownPara(i).OutPutName
            If MyFishPara.DownPara(i).OutPutType = "接车表号" OrElse MyFishPara.DownPara(i).OutPutType = "下车表号" Then
                xlsheet.Range(xlsheet.Cells(1, MyFishPara.DownPara.Count + i + 2), xlsheet.Cells(1 + ToTalArray.Count, MyFishPara.DownPara.Count + i + 2)).NumberFormat = "@"
            End If
        Next
        Me.ProgressBar1.Value = 50
        If ToTalArray IsNot Nothing AndAlso ToTalArray.Count > 0 Then
            For i As Integer = 0 To ToTalArray.Count - 1
                If ToTalArray(i).UpItem IsNot Nothing Then
                    For j As Integer = 1 To UBound(ToTalArray(i).UpItem)
                        xlsheet.Cells(i + 2, j) = ToTalArray(i).UpItem(j)
                    Next
                End If
                If ToTalArray(i).DownItem IsNot Nothing Then
                    For j As Integer = 1 To UBound(ToTalArray(i).DownItem)
                        xlsheet.Cells(i + 2, MyFishPara.UpPara.Count + j + 1) = ToTalArray(i).DownItem(j)
                    Next
                End If
                Me.ProgressBar1.Value = 50 + 50 * (i + 1) / ToTalArray.Count
            Next
        End If
        xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(1 + ToTalArray.Count, MyFishPara.UpPara.Count + MyFishPara.DownPara.Count + 1)).HorizontalAlignment = XlHAlign.xlHAlignCenter
        xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(1 + ToTalArray.Count, MyFishPara.UpPara.Count + MyFishPara.DownPara.Count + 1)).Borders.LineStyle = 1
        xlsheet.Columns(MyFishPara.UpPara.Count + 1).ColumnWidth = 2
        xlsheet.Range(xlsheet.Cells(1, 1), xlsheet.Cells(1, MyFishPara.UpPara.Count + MyFishPara.DownPara.Count + 1)).Font.Bold = True
        With xlapp.ActiveWindow               '冻结首行
            .SplitColumn = 0
            .SplitRow = 1
        End With
        xlapp.ActiveWindow.FreezePanes = True
        xlsheet.Columns.AutoFit()
        Me.ProgressBar1.Visible = False
        xlapp.Visible = True
    End Sub

    Public Function GetFishItem(ByVal nTrain As Integer, ByVal Paras As List(Of FishDiagram)) As String()
        GetFishItem = Nothing
        Dim tempItem(0) As String
        For Each para As FishDiagram In Paras
            Select Case para.OutPutType
                Case "接车表号"
                    ReDim Preserve tempItem(UBound(tempItem) + 1)
                    tempItem(UBound(tempItem)) = "-- --"
                Case "下车表号"
                    ReDim Preserve tempItem(UBound(tempItem) + 1)
                    tempItem(UBound(tempItem)) = "-- --"
                Case "时刻"
                    ReDim Preserve tempItem(UBound(tempItem) + 1)
                    tempItem(UBound(tempItem)) = ""
                    For i As Integer = 1 To UBound(CSTrainInf(nTrain).nPathID)
                        If StationInf(CSTrainInf(nTrain).nPathID(i)).sStationName = para.AvaPlaces(0) AndAlso _
                            i <> UBound(CSTrainInf(nTrain).nPathID) Then
                            If StationInf(CSTrainInf(nTrain).nPathID(i)).sStationName = "龙背村停车场" OrElse StationInf(CSTrainInf(nTrain).nPathID(i)).sStationName = "马家堡车辆段" _
                                OrElse StationInf(CSTrainInf(nTrain).nPathID(i)).sStationName = "南兆路车辆段" Then
                                tempItem(UBound(tempItem)) = CSTrainInf(nTrain).Arrival(CSTrainInf(nTrain).nPathID(i)) - 600
                            Else
                                tempItem(UBound(tempItem)) = CSTrainInf(nTrain).Arrival(CSTrainInf(nTrain).nPathID(i))
                            End If
                            Exit For
                        End If
                    Next
                Case "车底号"
                    ReDim Preserve tempItem(UBound(tempItem) + 1)
                    tempItem(UBound(tempItem)) = CSchediInfo(CSCheCiToCheDiID(nTrain)).sCheCiHao
                Case "车次"
                    ReDim Preserve tempItem(UBound(tempItem) + 1)
                    tempItem(UBound(tempItem)) = CSTrainInf(nTrain).sPrintTrain
                Case "折返车次"
                    ReDim Preserve tempItem(UBound(tempItem) + 1)
                    tempItem(UBound(tempItem)) = "-- --"
                    Dim nZhefanCheci As Integer = CSCheCiToReturnCheCi(nTrain)
                    If nZhefanCheci > 0 AndAlso StationInf(CSTrainInf(nZhefanCheci).nPathID(1)).sStationName = para.AvaPlaces(0) Then
                        tempItem(UBound(tempItem)) = CSTrainInf(nZhefanCheci).sPrintTrain
                    End If
            End Select
        Next
        GetFishItem = tempItem
    End Function

    Public Sub InPutCSDriverInfo(ByVal ArrayData As List(Of String()), ByVal Para As List(Of FishDiagram))
        For Each Item As String() In ArrayData
            Dim CheCi As String = ""
            For i As Integer = 0 To Para.Count - 1
                If Para(i).OutPutType = "车次" Then
                    CheCi = Item(i + 1)
                End If
            Next
            For i As Integer = 0 To Para.Count - 1
                If Para(i).OutPutType = "接车表号" Then
                    For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                        If dri IsNot Nothing Then
                            For Each train As CSLinkTrain In dri.CSLinkTrain
                                If train IsNot Nothing AndAlso train.IsDeadHeading = False Then
                                    If train.OutputCheCi = CheCi Then
                                        For Each sta As String In Para(i).AvaPlaces
                                            If sta = train.StartStaName Then
                                                Item(i + 1) = dri.OutPutCSdriverNo
                                                GoTo L
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    Next
L:
                ElseIf Para(i).OutPutType = "下车表号" Then
                    For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                        If dri IsNot Nothing Then
                            For Each train As CSLinkTrain In dri.CSLinkTrain
                                If train IsNot Nothing AndAlso train.IsDeadHeading = False Then
                                    If train.OutputCheCi = CheCi Then
                                        For Each sta As String In Para(i).AvaPlaces
                                            If sta = train.EndStaName Then
                                                Item(i + 1) = dri.OutPutCSdriverNo
                                                GoTo M
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        End If
                    Next
M:
                End If
            Next
        Next
    End Sub

    Public Function HeBinArrayData(ByVal UpArrayData As List(Of String()), ByVal DownArrayData As List(Of String())) As List(Of HeBinFishItem)
        HeBinArrayData = Nothing
        Dim tempHeBinArrayData As New List(Of HeBinFishItem)
        Dim DownCheciIndex As Integer = 0
        For i As Integer = 0 To MyFishPara.DownPara.Count - 1
            If MyFishPara.DownPara(i).OutPutType = "车次" Then
                DownCheciIndex = i + 1
            End If
        Next
        '================合并处理
        If DownCheciIndex > 0 Then
            For Each DownItem As String() In DownArrayData
                Dim ifHebin As Boolean = False
                For Each UpItem As String() In UpArrayData
                    For i As Integer = 0 To MyFishPara.UpPara.Count - 1
                        If MyFishPara.UpPara(i).OutPutType = "折返车次" Then
                            If UpItem(i + 1) = DownItem(DownCheciIndex) Then
                                Dim tempHeinItem As New HeBinFishItem(UpItem, DownItem)
                                tempHeBinArrayData.Add(tempHeinItem)
                                ifHebin = True
                                GoTo L
                            End If
                        End If
                    Next
                Next
L:
                If ifHebin = False Then
                    Dim tempHeinItem As New HeBinFishItem()
                    tempHeinItem.DownItem = DownItem
                    tempHeinItem.UpItem = Nothing
                    tempHeBinArrayData.Add(tempHeinItem)
                End If
            Next
        End If
        '==========将无法合并的单独处理
        For Each UpItem As String() In UpArrayData
            Dim ifExist As Boolean = False
            For Each item As HeBinFishItem In tempHeBinArrayData
                If item.UpItem IsNot Nothing AndAlso item.UpItem Is UpItem Then
                    ifExist = True
                    Exit For
                End If
            Next
            If ifExist = False Then
                Dim tempHeinItem As New HeBinFishItem()
                tempHeinItem.DownItem = Nothing
                tempHeinItem.UpItem = UpItem
                tempHeBinArrayData.Add(tempHeinItem)
            End If
        Next
        '================排序，按照格式车站排序
        Dim temHebinArrayData2 As New List(Of HeBinFishItem)
        If UpArrayData.Count > 1 Then
            For i As Integer = MyFishPara.UpPara.Count - 1 To 0 Step -1
                If MyFishPara.UpPara(i).OutPutType = "时刻" Then
                    For m As Integer = tempHeBinArrayData.Count - 1 To 0 Step -1
                        If tempHeBinArrayData(m).UpItem IsNot Nothing AndAlso tempHeBinArrayData(m).UpItem(i + 1) <> "" Then
                            If temHebinArrayData2.Count = 0 Then
                                temHebinArrayData2.Add(tempHeBinArrayData(m))
                                tempHeBinArrayData.RemoveAt(m)
                            Else
                                Dim index As Integer = -1
                                Dim lastindex As Integer = -1
                                For x As Integer = 0 To temHebinArrayData2.Count - 1
                                    If temHebinArrayData2(x).UpItem(i + 1).Trim <> "" Then
                                        If tempHeBinArrayData(m).UpItem(i + 1) < temHebinArrayData2(x).UpItem(i + 1) Then
                                            index = x
                                            Exit For
                                        End If
                                        lastindex = x
                                    End If
                                Next
                                If index = -1 Then
                                    If lastindex <> -1 Then
                                        temHebinArrayData2.Insert(lastindex + 1, tempHeBinArrayData(m))
                                        tempHeBinArrayData.RemoveAt(m)
                                    End If
                                Else
                                    temHebinArrayData2.Insert(index, tempHeBinArrayData(m))
                                    tempHeBinArrayData.RemoveAt(m)
                                End If
                            End If
                        End If
                    Next
                End If
            Next
        End If
        If DownArrayData.Count > 1 Then
            For i As Integer = MyFishPara.DownPara.Count - 1 To 0 Step -1
                If MyFishPara.DownPara(i).OutPutType = "时刻" Then
                    For m As Integer = tempHeBinArrayData.Count - 1 To 0 Step -1
                        If tempHeBinArrayData(m).DownItem IsNot Nothing AndAlso tempHeBinArrayData(m).DownItem(i + 1) <> "" Then
                            If temHebinArrayData2.Count = 0 Then
                                temHebinArrayData2.Add(tempHeBinArrayData(m))
                                tempHeBinArrayData.RemoveAt(m)
                            Else
                                Dim index As Integer = -1
                                Dim lastindex As Integer = -1
                                For x As Integer = 0 To temHebinArrayData2.Count - 1
                                    If temHebinArrayData2(x).DownItem IsNot Nothing AndAlso temHebinArrayData2(x).DownItem(i + 1).Trim <> "" Then
                                        If tempHeBinArrayData(m).DownItem(i + 1) < temHebinArrayData2(x).DownItem(i + 1) Then
                                            index = x
                                            Exit For
                                        End If
                                        lastindex = x
                                    End If
                                Next
                                If index = -1 Then
                                    If lastindex <> -1 Then
                                        temHebinArrayData2.Insert(lastindex + 1, tempHeBinArrayData(m))
                                        tempHeBinArrayData.RemoveAt(m)
                                    End If
                                Else
                                    temHebinArrayData2.Insert(index, tempHeBinArrayData(m))
                                    tempHeBinArrayData.RemoveAt(m)
                                End If
                            End If
                        End If
                    Next
                End If
            Next
        End If

        If tempHeBinArrayData.Count > 0 Then
            temHebinArrayData2.InsertRange(0, tempHeBinArrayData)
        End If
        'If UpArrayData.Count > 1 Then
        '    For i As Integer = 0 To MyFishPara.UpPara.Count - 1
        '        If MyFishPara.UpPara(i).OutPutType = "时刻" Then
        '            For m As Integer = 0 To tempHeBinArrayData.Count - 2
        '                For n As Integer = m + 1 To tempHeBinArrayData.Count - 1
        '                    If tempHeBinArrayData(m).UpItem IsNot Nothing AndAlso tempHeBinArrayData(m).UpItem(i + 1) <> "" AndAlso tempHeBinArrayData(n).UpItem IsNot Nothing AndAlso tempHeBinArrayData(n).UpItem(i + 1) <> "" Then
        '                        If IIf(tempHeBinArrayData(n).UpItem(i + 1) < 10800, tempHeBinArrayData(n).UpItem(i + 1) + 86400, tempHeBinArrayData(n).UpItem(i + 1)) < _
        '                            IIf(tempHeBinArrayData(m).UpItem(i + 1) < 10800, tempHeBinArrayData(m).UpItem(i + 1) + 86400, tempHeBinArrayData(m).UpItem(i + 1)) Then
        '                            Dim tempItem As HeBinFishItem = tempHeBinArrayData(n)
        '                            tempHeBinArrayData(n) = tempHeBinArrayData(m)
        '                            tempHeBinArrayData(m) = tempItem
        '                        End If
        '                    End If
        '                Next
        '            Next
        '        End If
        '    Next
        'End If
        'If DownArrayData.Count > 1 Then
        '    For i As Integer = 0 To MyFishPara.DownPara.Count - 1
        '        If MyFishPara.DownPara(i).OutPutType = "时刻" Then
        '            For m As Integer = 0 To tempHeBinArrayData.Count - 2
        '                For n As Integer = m + 1 To tempHeBinArrayData.Count - 1
        '                    If tempHeBinArrayData(m).DownItem IsNot Nothing AndAlso tempHeBinArrayData(m).DownItem(i + 1) <> "" AndAlso tempHeBinArrayData(n).DownItem IsNot Nothing AndAlso tempHeBinArrayData(n).DownItem(i + 1) <> "" Then
        '                        If IIf(tempHeBinArrayData(n).DownItem(i + 1) < 10800, tempHeBinArrayData(n).DownItem(i + 1) + 86400, tempHeBinArrayData(n).DownItem(i + 1)) < _
        '                            IIf(tempHeBinArrayData(m).DownItem(i + 1) < 10800, tempHeBinArrayData(m).DownItem(i + 1) + 86400, tempHeBinArrayData(m).DownItem(i + 1)) Then
        '                            Dim tempItem As HeBinFishItem = tempHeBinArrayData(n)
        '                            tempHeBinArrayData(n) = tempHeBinArrayData(m)
        '                            tempHeBinArrayData(m) = tempItem
        '                        End If
        '                    End If
        '                Next
        '            Next
        '        End If
        '    Next
        'End If
        HeBinArrayData = temHebinArrayData2
    End Function

    '=========将时刻信息的整数转换为时间字符串
    Public Sub ChangeArrayIntToTime(ByVal ArratData As List(Of String()), ByVal Para As List(Of FishDiagram))
        For i As Integer = 0 To Para.Count - 1
            If Para(i).OutPutType = "时刻" Then
                For Each Item As String() In ArratData
                    If Item(i + 1) <> "" Then
                        Item(i + 1) = BeTime(Item(i + 1))
                    End If
                Next
            End If
        Next
    End Sub

    Public Class FishDiagram
        Public LineName As String
        Public ID As Integer
        Public OutPutType As String
        Public AvaPlaces() As String
        Public OutPutName As String
        Public Direction As String
        Public Sub New(ByVal _Linename As String, ByVal _ID As Integer)
            LineName = _Linename
            ID = _ID
        End Sub
    End Class

    Public Class FishDiaPara
        Public LineName As String
        Public UpPara As New List(Of FishDiagram)
        Public DownPara As New List(Of FishDiagram)
        Public Sub New(ByVal _LineName As String)
            LineName = _LineName
        End Sub
    End Class
    Public MyFishPara As FishDiaPara
    Public Class HeBinFishItem
        Public UpItem As String() = Nothing
        Public DownItem As String() = Nothing
        Public Sub New(ByVal _UpItem As String(), ByVal _DownItem As String())
            UpItem = _UpItem
            DownItem = _DownItem
        End Sub
        Public Sub New()

        End Sub
    End Class

    Private Sub BtnUp1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUp1.Click
        If Me.DGVUpFormat.SelectedRows.Count = 1 AndAlso Me.DGVUpFormat.SelectedRows(0).Index < Me.DGVUpFormat.RowCount - 1 Then
            Dim CurRow As DataGridViewRow = Me.DGVUpFormat.SelectedRows(0)
            If CurRow.Index = 0 Then
                Exit Sub
            Else
                Dim UpRow As DataGridViewRow = Me.DGVUpFormat.Rows(CurRow.Index - 1)
                Me.DGVUpFormat.Rows.Remove(CurRow)
                Me.DGVUpFormat.Rows.Insert(UpRow.Index, CurRow)
                CurRow.Selected = True
            End If
            Call UpDGVReIndex("上行")
        End If
    End Sub

    Private Sub BtnDown1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDown1.Click
        If Me.DGVUpFormat.SelectedRows.Count = 1 AndAlso Me.DGVUpFormat.SelectedRows(0).Index < Me.DGVUpFormat.RowCount - 1 Then
            Dim CurRow As DataGridViewRow = Me.DGVUpFormat.SelectedRows(0)
            If CurRow.Index = Me.DGVUpFormat.RowCount - 2 Then
                Exit Sub
            Else
                Dim DownRow As DataGridViewRow = Me.DGVUpFormat.Rows(CurRow.Index + 1)
                Me.DGVUpFormat.Rows.Remove(DownRow)
                Me.DGVUpFormat.Rows.Insert(CurRow.Index, DownRow)
                CurRow.Selected = True
            End If
            Call UpDGVReIndex("上行")
        End If
    End Sub

    Public Sub UpDGVReIndex(ByVal UpOrDown As String)
        If UpOrDown = "上行" Then
            If Me.DGVUpFormat.Rows.Count > 1 Then
                For i As Integer = 0 To Me.DGVUpFormat.Rows.Count - 2
                    Me.DGVUpFormat.Rows(i).Cells(0).Value = i + 1
                Next
            End If
        Else
            If Me.DGVDownFormat.Rows.Count > 1 Then
                For i As Integer = 0 To Me.DGVDownFormat.Rows.Count - 2
                    Me.DGVDownFormat.Rows(i).Cells(0).Value = i + 1
                Next
            End If
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Me.DGVDownFormat.SelectedRows.Count = 1 AndAlso Me.DGVDownFormat.SelectedRows(0).Index < Me.DGVDownFormat.RowCount - 1 Then
            Dim CurRow As DataGridViewRow = Me.DGVDownFormat.SelectedRows(0)
            If CurRow.Index = 0 Then
                Exit Sub
            Else
                Dim UpRow As DataGridViewRow = Me.DGVDownFormat.Rows(CurRow.Index - 1)
                Me.DGVDownFormat.Rows.Remove(CurRow)
                Me.DGVDownFormat.Rows.Insert(UpRow.Index, CurRow)
                CurRow.Selected = True
            End If
            Call UpDGVReIndex("下行")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Me.DGVDownFormat.SelectedRows.Count = 1 AndAlso Me.DGVDownFormat.SelectedRows(0).Index < Me.DGVDownFormat.RowCount - 1 Then
            Dim CurRow As DataGridViewRow = Me.DGVDownFormat.SelectedRows(0)
            If CurRow.Index = Me.DGVDownFormat.RowCount - 2 Then
                Exit Sub
            Else
                Dim DownRow As DataGridViewRow = Me.DGVDownFormat.Rows(CurRow.Index + 1)
                Me.DGVDownFormat.Rows.Remove(DownRow)
                Me.DGVDownFormat.Rows.Insert(CurRow.Index, DownRow)
                CurRow.Selected = True
            End If
            Call UpDGVReIndex("下行")
        End If
    End Sub

    Private Sub BtnInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnInput.Click
        Call GetFishPara()
        Call Settitle()

        Dim New0penFile As New OpenFileDialog
        Dim strExcelFilePath As String
        New0penFile.Filter = "xls files (*.xls)|*.xls|xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        New0penFile.FilterIndex = 1
        New0penFile.RestoreDirectory = True
        strExcelFilePath = ""

        If New0penFile.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            strExcelFilePath = New0penFile.FileName
        End If
        '获得数据库的名称
        If strExcelFilePath <> "" Then

            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
            Dim MyConnection As System.Data.OleDb.OleDbConnection
            MyConnection = New System.Data.OleDb.OleDbConnection( _
                          "provider=Microsoft.ACE.OLEDB.12.0; " & _
                          "data source='" & strExcelFilePath & "'; " & _
                          "Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'")
            Dim tmpStr As String
            tmpStr = "select * from" & "[钓鱼图$]"
            MyCommand = New System.Data.OleDb.OleDbDataAdapter(tmpStr, MyConnection)
            MyConnection.Open()

            Dim objDataset1 As New DataSet
            Try
                MyCommand.Fill(objDataset1, "XLData")
                Dim temTab As Data.DataTable = objDataset1.Tables(0)

                'Dim a As Integer = 1
            Catch ex As Exception
                MsgBox("EXCEL数据库不正确，请确定打开的数据库格式正确!")
            End Try
            MyConnection.Close()
            GC.Collect()
        End If
    End Sub

    Public Sub Settitle()
        Dim titles As New List(Of String)
        For Each para As FishDiagram In MyFishPara.UpPara
            Dim titlename As String = para.OutPutName
            While True
                Dim index As Integer = titles.FindLastIndex(Function(value As String)
                                                                Return value = titlename
                                                            End Function)
                If index <> -1 Then
                    If IsNumeric(titlename.Substring(titlename.Length - 1, 1)) Then
                        Dim tindex As Integer = titlename.Substring(titlename.Length - 1, 1)
                        titlename = titlename.Substring(0, titlename.Length - 1) & (tindex + 1)
                    Else
                        titlename = titlename & "1"
                    End If
                Else
                    Exit While
                End If
            End While
            para.OutPutName = titlename
            titles.Add(titlename)
        Next
        For Each para As FishDiagram In MyFishPara.DownPara
            Dim titlename As String = para.OutPutName
            While True
                Dim index As Integer = titles.FindIndex(Function(value As String)
                                                            Return value = titlename
                                                        End Function)
                If index <> -1 Then
                    If IsNumeric(titlename.Substring(titlename.Length - 1, 1)) Then
                        Dim tindex As Integer = titlename.Substring(titlename.Length - 1, 1)
                        titlename = titlename.Substring(0, titlename.Length - 1) & (tindex + 1)
                    Else
                        titlename = titlename & "1"
                    End If
                Else
                    Exit While
                End If
            End While
            para.OutPutName = titlename
            titles.Add(titlename)
        Next
    End Sub
End Class