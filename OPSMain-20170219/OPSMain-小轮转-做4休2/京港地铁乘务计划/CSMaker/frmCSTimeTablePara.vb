Public Class frmCSTimeTablePara
    Public sCurParaState As String

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime = GetDiagramParaValueFromDataGrid("底图起始时间")
        CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime = GetDiagramParaValueFromDataGrid("底图显示时间宽")
        CSTimeTablePara.TimeTableDiagramPara.intCompareFirstTime = HourToSecond(GetDiagramParaValueFromDataGrid("时间比较起始时间"))
        CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = GetDiagramParaValueFromDataGrid("底图宽")
        CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = GetDiagramParaValueFromDataGrid("底图高")
        CSTimeTablePara.TimeTableDiagramPara.strTimeFormat = GetDiagramParaValueFromDataGrid("底图时分格式")
        CSTimeTablePara.TimeTableDiagramPara.sngtopBlank = GetDiagramParaValueFromDataGrid("底图上下空白高度")
        CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank = GetDiagramParaValueFromDataGrid("底图时间空白高度")
        CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank = GetDiagramParaValueFromDataGrid("底图左右空白高度")
        CSTimeTablePara.TimeTableDiagramPara.sngStaBlank = GetDiagramParaValueFromDataGrid("底图车站空白宽度")
        CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX = GetDiagramParaValueFromDataGrid("左边缩进宽度")
        CSTimeTablePara.TimeTableDiagramPara.sngPubTopY = GetDiagramParaValueFromDataGrid("上面缩进高度")
        CSTimeTablePara.TimeTableDiagramPara.sngPicStationWidth = GetDiagramParaValueFromDataGrid("车站站名图宽")
        CSTimeTablePara.TimeTableDiagramPara.sngPicStationHeight = GetDiagramParaValueFromDataGrid("车站站名图高")
        Call ResetElsePara()
        Call CSRefreshDiagram(0)

    End Sub

    Private Sub btnSetDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDefault.Click

        Dim i As Integer
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String

        For i = 1 To Me.dataGrid.Rows.Count
            cellName = Me.dataGrid.Rows(i - 1).Cells(1).Value
            CellValue = Me.dataGrid.Rows(i - 1).Cells(2).Value
            Str = "update CS_CSTimeTableSystemPara set " & _
                    "PARAVALUE ='" & CellValue & "'" & _
                    "where PARANAME = '" & cellName & "' AND  LINEID='" & CStr(strCurlineID) & "'"
            Globle.Method.UpdateDataForAccess(Str)
        Next
        UpDateElsePara()

        MsgBox("已经成功设置", MsgBoxStyle.OkOnly, "提示")
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
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime
                    Case "底图显示时间宽"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime
                    Case "时间比较起始时间"
                        GetDiagramParaValue = SecondToHour(CSTimeTablePara.TimeTableDiagramPara.intCompareFirstTime, 1)
                    Case "底图宽"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
                    Case "底图高"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
                    Case "底图时分格式"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.strTimeFormat
                    Case "底图上下空白高度"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngtopBlank
                    Case "底图时间空白高度"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank
                    Case "底图左右空白高度"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank
                    Case "底图车站空白宽度"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngStaBlank
                    Case "左边缩进宽度"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX
                    Case "上面缩进高度"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngPubTopY
                    Case "车站站名图宽"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngPicStationWidth
                    Case "车站站名图高"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngPicStationHeight
                    Case "是否显示车次"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi
                    Case "是否显示斜向车次"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi
                    Case "车底交路线高度"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.nCheDiLineHeight
                    Case "车底交路线类型"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sCheDiLineStyle
                    Case "车站股道线间距"
                        GetDiagramParaValue = CSTimeTablePara.StaDiagramePara.nStaLineHeight
                    Case "运行线可调整时间段"
                        GetDiagramParaValue = CSTdrawLinePara.sMaxMoveTime
                    Case "运行线移动时间"
                        GetDiagramParaValue = CSTdrawLinePara.sMoveStepTime
                End Select

        End Select

    End Function
    '调图时的参数
    Structure typeTdrawlLinePara
        Dim sMoveStepTime As Integer '每一步移动的时间
        Dim sMaxMoveTime As Integer '最在可移动的时间
    End Structure
    Public CSTdrawLinePara As typeTdrawlLinePara

    '根据参数名称得到数值
    Private Function GetDiagramParaValueFromDataGrid(ByVal sParaName As String) As String
        GetDiagramParaValueFromDataGrid = ""
        Dim i As Integer
        For i = 1 To Me.dataGrid.Rows.Count
            If Me.dataGrid.Rows(i - 1).Cells(1).Value.ToString = sParaName Then
                GetDiagramParaValueFromDataGrid = Me.dataGrid.Rows(i - 1).Cells(2).Value.ToString.Trim
                Exit For
            End If
        Next
    End Function

    Private Sub frmCSTimeTablePara_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Dim sqlstr As String = ""
        Me.dataGrid.Rows.Clear()

        sqlstr = "SELECT * FROM CS_CSTIMETABLESYSTEMPARA WHERE LINEID='" & CStr(strCurlineID) & "' ORDER BY PARAID"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        For i = 1 To tempTable.Rows.Count
            If tempTable.Rows(i - 1).Item("PARAID") <= 14 Then
                Try
                    Me.dataGrid.Rows.Add(1)
                Catch ex As Exception

                End Try

                Me.dataGrid.Rows(Me.dataGrid.Rows.Count - 1).Cells(0).Value = Me.dataGrid.Rows.Count
                Me.dataGrid.Rows(Me.dataGrid.Rows.Count - 1).Cells(1).Value = tempTable.Rows(i - 1).Item("PARANAME").ToString
                Me.dataGrid.Rows(Me.dataGrid.Rows.Count - 1).Cells(2).Value = GetDiagramParaValue(tempTable.Rows(i - 1).Item("PARANAME").ToString)
            End If
        Next

        '其他参数

        If CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi Then
            Me.chkShowCheci.Checked = True
        Else
            Me.chkShowCheci.Checked = False
        End If

        If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo Then
            Me.chkShowDriverNo.Checked = True
        Else
            Me.chkShowDriverNo.Checked = False
        End If

        For i = 1 To tempTable.Rows.Count

            If tempTable.Rows(i - 1).Item("PARANAME").ToString = "车底交路线高度" Then
                Me.numCheDiLineHeight.Value = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
            End If

            If tempTable.Rows(i - 1).Item("PARANAME").ToString = "车底交路线类型" Then
                Me.cmbCheDiLineStyle.Text = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
            End If

        Next

    End Sub

    '更新其他参数
    Private Sub UpDateElsePara()
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String

        cellName = "是否显示车次"
        If Me.chkShowCheci.Checked = True Then
            CellValue = "True"
        Else
            CellValue = "False"
        End If
        Str = "update CS_CSTimeTableSystemPara set " & _
                "PARAVALUE ='" & CellValue & "'" & _
                "where PARANAME = '" & cellName & "' AND  LINEID='" & CStr(strCurlineID) & "'"
        Globle.Method.UpdateDataForAccess(Str)

        cellName = "是否显示司机编号"
        If Me.chkShowDriverNo.Checked = True Then
            CellValue = "True"
        Else
            CellValue = "False"
        End If
        Str = "update CS_CSTimeTableSystemPara set " & _
                "PARAVALUE ='" & CellValue & "'" & _
                "where PARANAME = '" & cellName & "' AND  LINEID='" & CStr(strCurlineID) & "'"
        Globle.Method.UpdateDataForAccess(Str)

        cellName = "车底交路线高度"
        CellValue = Me.numCheDiLineHeight.Value
        Str = "update CS_CSTimeTableSystemPara set " & _
                "PARAVALUE ='" & CellValue & "'" & _
                "where PARANAME = '" & cellName & "' AND  LINEID='" & CStr(strCurlineID) & "'"
        Globle.Method.UpdateDataForAccess(Str)

        cellName = "车底交路线类型"
        CellValue = Me.cmbCheDiLineStyle.Text.Trim
        Str = "update CS_CSTimeTableSystemPara set " & _
                "PARAVALUE ='" & CellValue & "'" & _
                "where PARANAME = '" & cellName & "' AND  LINEID='" & CStr(strCurlineID) & "'"
        Globle.Method.UpdateDataForAccess(Str)

    End Sub

    '对其他参数赋值
    Private Sub ResetElsePara()
        If Me.chkShowDriverNo.Checked = True Then
            CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True
        Else
            CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = False
        End If

        If Me.chkShowCheci.Checked = True Then
            CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi = True
        Else
            CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi = False
        End If

        CSTimeTablePara.TimeTableDiagramPara.nCheDiLineHeight = Me.numCheDiLineHeight.Value
        CSTimeTablePara.TimeTableDiagramPara.sCheDiLineStyle = Me.cmbCheDiLineStyle.Text.Trim

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class