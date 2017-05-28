Public Class frmTimeTablePara
    Public sCurParaState As String

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Select Case sCurParaState
            Case "系统参数"
                SystemPara.sPicFilePath = GetDiagramParaValueFromDataGrid("底图图片路径")
                SystemPara.sUserCompanyName = GetDiagramParaValueFromDataGrid("使用单位名称")
                SystemPara.SystemStyle = GetDiagramParaValueFromDataGrid("系统方式")

            Case "运行图系统参数"
                TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime = GetDiagramParaValueFromDataGrid("底图起始时间")
                TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime = GetDiagramParaValueFromDataGrid("底图显示时间宽")
                TimeTablePara.TimeTableDiagramPara.intCompareFirstTime = HourToSecond(GetDiagramParaValueFromDataGrid("时间比较起始时间"))
                TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = GetDiagramParaValueFromDataGrid("底图宽")
                TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = GetDiagramParaValueFromDataGrid("底图高")
                TimeTablePara.TimeTableDiagramPara.strTimeFormat = GetDiagramParaValueFromDataGrid("底图时分格式")
                TimeTablePara.TimeTableDiagramPara.sngtopBlank = GetDiagramParaValueFromDataGrid("底图上下空白高度")
                TimeTablePara.TimeTableDiagramPara.sngTimeBlank = GetDiagramParaValueFromDataGrid("底图时间空白高度")
                TimeTablePara.TimeTableDiagramPara.sngLeftBlank = GetDiagramParaValueFromDataGrid("底图左右空白高度")
                TimeTablePara.TimeTableDiagramPara.sngStaBlank = GetDiagramParaValueFromDataGrid("底图车站空白宽度")
                TimeTablePara.TimeTableDiagramPara.sngPubLeftX = GetDiagramParaValueFromDataGrid("左边缩进宽度")
                TimeTablePara.TimeTableDiagramPara.sngPubTopY = GetDiagramParaValueFromDataGrid("上面缩进高度")
                TimeTablePara.TimeTableDiagramPara.sngPicStationWidth = GetDiagramParaValueFromDataGrid("车站站名图宽")
                TimeTablePara.TimeTableDiagramPara.sngPicStationHeight = GetDiagramParaValueFromDataGrid("车站站名图高")
                Call ResetElsePara()
                Call RefreshDiagram(0)
        End Select

        Me.Close()
    End Sub

    Private Sub btnSetDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDefault.Click
        Dim i As Integer
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String
        Select Case sCurParaState
            Case "系统参数"

                For i = 1 To Me.dataGrid.Rows.Count
                    cellName = Me.dataGrid.Rows(i - 1).Cells(1).Value
                    CellValue = Me.dataGrid.Rows(i - 1).Cells(2).Value
                    Str = "update 系统参数表 set " & _
                            "数值 ='" & CellValue & "'" & _
                            "where 参数名 = '" & cellName & "'"

                    Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                    MyCom.ExecuteNonQuery()
                Next

            Case "运行图系统参数"
                For i = 1 To Me.dataGrid.Rows.Count
                    cellName = Me.dataGrid.Rows(i - 1).Cells(1).Value
                    CellValue = Me.dataGrid.Rows(i - 1).Cells(2).Value
                    Str = "update 运行图系统参数表 set " & _
                            "数值 ='" & CellValue & "'" & _
                            "where 参数名 = '" & cellName & "'"

                    Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                    MyCom.ExecuteNonQuery()
                Next
                UpDateElsePara()
        End Select
        MsgBox("已经成功设置", , "提示")
        MyConn.Close()
    End Sub

    '根据参数名称得到数值
    Private Function GetDiagramParaValue(ByVal sParaName As String) As String
        GetDiagramParaValue = ""
        Select Case sCurParaState
            Case "系统参数"
                Select Case sParaName
                    Case "使用单位名称"
                        GetDiagramParaValue = SystemPara.sUserCompanyName
                    Case "底图图片路径"
                        GetDiagramParaValue = SystemPara.sPicFilePath
                    Case "系统方式"
                        GetDiagramParaValue = SystemPara.SystemStyle
                End Select

            Case "运行图系统参数"
                Select Case sParaName
                    Case "底图起始时间"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime
                    Case "底图显示时间宽"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime
                    Case "时间比较起始时间"
                        GetDiagramParaValue = SecondToHour(TimeTablePara.TimeTableDiagramPara.intCompareFirstTime, 1)
                    Case "底图宽"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
                    Case "底图高"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
                    Case "底图时分格式"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.strTimeFormat
                    Case "底图上下空白高度"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngtopBlank
                    Case "底图时间空白高度"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngTimeBlank
                    Case "底图左右空白高度"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngLeftBlank
                    Case "底图车站空白宽度"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngStaBlank
                    Case "左边缩进宽度"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngPubLeftX
                    Case "上面缩进高度"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngPubTopY
                    Case "车站站名图宽"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngPicStationWidth
                    Case "车站站名图高"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngPicStationHeight
                    Case "是否显示车次"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.nifPrintCheCi
                    Case "是否显示斜向车次"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi
                    Case "车底交路线高度"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.nCheDiLineHeight
                    Case "车底交路线类型"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sCheDiLineStyle
                    Case "车站股道线间距"
                        GetDiagramParaValue = TimeTablePara.StaDiagramePara.nStaLineHeight
                    Case "运行线可调整时间段"
                        GetDiagramParaValue = TdrawLinePara.sMaxMoveTime
                    Case "运行线移动时间"
                        GetDiagramParaValue = TdrawLinePara.sMoveStepTime
                End Select

        End Select

    End Function

    '根据参数名称得到数值
    Private Function GetDiagramParaValueFromDataGrid(ByVal sParaName As String) As String
        GetDiagramParaValueFromDataGrid = ""
        Dim i As Integer
        For i = 1 To Me.dataGrid.Rows.Count
            If Me.dataGrid.Rows(i - 1).Cells(1).Value.ToString = sParaName Then
                GetDiagramParaValueFromDataGrid = Me.dataGrid.Rows(i - 1).Cells(2).Value.ToString
                Exit For
            End If
        Next
    End Function

    Private Sub frmTimeTablePara_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Dim nCurRow As Integer
        Me.dataGrid.Rows.Clear()
        Select Case sCurParaState
            Case "系统参数"
                Dim strTable3 As String = "select * from 系统参数表 order by 序号"
                Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
                '创建一个Datasetd
                Dim myDataSet3 As Data.DataSet = New Data.DataSet
                Mydc3.Fill(myDataSet3, "系统参数表")
                Dim myTable3 As Data.DataTable
                myTable3 = myDataSet3.Tables("系统参数表")
                For i = 1 To myTable3.Rows.Count
                    Me.dataGrid.Rows.Add()
                    nCurRow = Me.dataGrid.Rows.Count - 1
                    Me.dataGrid.Rows(nCurRow).Cells(0).Value = nCurRow + 1
                    Me.dataGrid.Rows(nCurRow).Cells(1).Value = myTable3.Rows(i - 1).Item("参数名").ToString
                    Me.dataGrid.Rows(nCurRow).Cells(2).Value = GetDiagramParaValue(myTable3.Rows(i - 1).Item("参数名").ToString)
                Next

            Case "运行图系统参数"
                Dim strTable3 As String = "select * from 运行图系统参数表 where 序号<=14 order by 序号 "
                Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
                '创建一个Datasetd
                Dim myDataSet3 As Data.DataSet = New Data.DataSet
                Mydc3.Fill(myDataSet3, "运行图系统参数表")
                Dim myTable3 As Data.DataTable
                myTable3 = myDataSet3.Tables("运行图系统参数表")
                For i = 1 To myTable3.Rows.Count
                    Me.dataGrid.Rows.Add()
                    nCurRow = Me.dataGrid.Rows.Count - 1
                    Me.dataGrid.Rows(nCurRow).Cells(0).Value = nCurRow + 1
                    Me.dataGrid.Rows(nCurRow).Cells(1).Value = myTable3.Rows(i - 1).Item("参数名").ToString
                    Me.dataGrid.Rows(nCurRow).Cells(2).Value = GetDiagramParaValue(myTable3.Rows(i - 1).Item("参数名").ToString)
                Next
                Call listElsePara()
        End Select
    End Sub

    Private Sub listElsePara() '显示其他参数
        Dim nIfShowcheci As String
        Dim nIfShowXieCheci As String
        Dim nCheDiLineHeight As Integer
        Dim nGuDaoLineHeight As Integer
        Dim nCheDiLineStyle As String
        nIfShowcheci = GetDiagramParaValue("是否显示车次") 'GetCurParaValue("是否显示车次")
        If nIfShowcheci = "True" Then
            Me.chkShowCheCi.Checked = True
        Else
            Me.chkShowCheCi.Checked = False
        End If

        nIfShowXieCheci = GetDiagramParaValue("是否显示斜向车次") 'GetCurParaValue("是否显示斜向车次")
        If nIfShowXieCheci = "True" Then
            Me.chkXieCheci.Checked = True
        Else
            Me.chkXieCheci.Checked = False
        End If

        nCheDiLineHeight = GetDiagramParaValue("车底交路线高度") ' GetCurParaValue("车底交路线高度")
        Me.numCheDiLineHeight.Value = nCheDiLineHeight

        nCheDiLineStyle = GetDiagramParaValue("车底交路线类型") ' GetCurParaValue("车底交路线类型")
        Me.cmbCheDiLineStyle.Text = nCheDiLineStyle

        nGuDaoLineHeight = GetDiagramParaValue("车站股道线间距") ' GetCurParaValue("车站股道线间距")
        Me.numGuDaoLineHeight.Value = nGuDaoLineHeight

        Me.numLineAdjustWidth.Value = GetDiagramParaValue("运行线可调整时间段")
        Me.NumLineMoveStep.Value = GetDiagramParaValue("运行线移动时间")

        If TimeTablePara.TimeTableDiagramPara.sCheCiShowStyle = 1 Then
            Me.rbtSix.Checked = True
        Else
            Me.rbtFour.Checked = True
        End If
    End Sub

    Private Function GetCurParaValue(ByVal sPara As String) As String '得到当前参数的数值
        GetCurParaValue = ""
        Dim strTable3 As String = "select * from 运行图系统参数表 where 参数名=' " & sPara & "' order by 序号 "
        Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
        '创建一个Datasetd
        Dim myDataSet3 As Data.DataSet = New Data.DataSet
        Mydc3.Fill(myDataSet3, "运行图系统参数表")
        Dim myTable3 As Data.DataTable
        myTable3 = myDataSet3.Tables("运行图系统参数表")
        If myTable3.Rows.Count > 0 Then
            GetCurParaValue = myTable3.Rows(0).Item("数值").ToString.Trim
        End If
    End Function

    '更新其他参数
    Private Sub UpDateElsePara()
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String

        cellName = "是否显示车次"
        If Me.chkShowCheCi.Checked = True Then
            CellValue = "True"
        Else
            CellValue = "False"
        End If
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom.ExecuteNonQuery()

        cellName = "是否显示斜向车次"
        If Me.chkXieCheci.Checked = True Then
            CellValue = "True"
        Else
            CellValue = "False"
        End If
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom1.ExecuteNonQuery()

        cellName = "车底交路线高度"
        CellValue = Me.numCheDiLineHeight.Value
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom2.ExecuteNonQuery()

        cellName = "车底交路线类型"
        CellValue = Me.cmbCheDiLineStyle.Text.Trim
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom3 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom3.ExecuteNonQuery()

        cellName = "车站股道线间距"
        CellValue = Me.numGuDaoLineHeight.Value
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom4 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom4.ExecuteNonQuery()

        cellName = "运行线可调整时间段"
        CellValue = Me.numLineAdjustWidth.Value
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom5 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom5.ExecuteNonQuery()

        cellName = "运行线移动时间"
        CellValue = Me.NumLineMoveStep.Value
        Str = "update 运行图系统参数表 set " & _
                "数值 ='" & CellValue & "'" & _
                "where 参数名 = '" & cellName & "'"
        Dim MyCom6 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom6.ExecuteNonQuery()

        MyConn.Close()
    End Sub

    '对其他参数赋值
    Private Sub ResetElsePara()
        If Me.chkShowCheCi.Checked = True Then
            TimeTablePara.TimeTableDiagramPara.nifPrintCheCi = True
        Else
            TimeTablePara.TimeTableDiagramPara.nifPrintCheCi = False
        End If
  
        If Me.chkXieCheci.Checked = True Then
            TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi = True
        Else
            TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi = False
        End If
    
        TimeTablePara.TimeTableDiagramPara.nCheDiLineHeight = Me.numCheDiLineHeight.Value
        TimeTablePara.TimeTableDiagramPara.sCheDiLineStyle = Me.cmbCheDiLineStyle.Text.Trim
        TimeTablePara.StaDiagramePara.nStaLineHeight = Me.numGuDaoLineHeight.Value

        If Me.rbtFour.Checked = True Then
            TimeTablePara.TimeTableDiagramPara.sCheCiShowStyle = 0
        Else
            TimeTablePara.TimeTableDiagramPara.sCheCiShowStyle = 1
        End If

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class