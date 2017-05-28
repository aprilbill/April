Public Class frmScaleInforEdit
    Public sCurTableName As String

    Private Sub frmScaleInforEdit_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If MsgBox("退出前请保存已经修改的信息! 确定退出当前窗口吗？", MsgBoxStyle.Question + MsgBoxStyle.OkCancel, "确认操作") = MsgBoxResult.Cancel Then
            e.Cancel = True
        End If
    End Sub
    Private Sub frmDataEdit_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "数据维护——" & sCurTableName
        Call DataRefresh()
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Call OutPutToEXCELFileFormDataGrid(sCurTableName, Me.dataView, Me)
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim i As Integer
        Dim j As Integer
        Dim myWS As DAO.Workspace
        Dim DE As DAO.DBEngine = New DAO.DBEngine
        myWS = DE.Workspaces(0)
        Dim DBdata As DAO.Database
        Dim RSdata As DAO.Recordset
        DBdata = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        RSdata = DBdata.OpenRecordset(sCurTableName)
        Dim nErrH As Integer
        Dim nErrL As Integer
        Try
            If Me.dataView.RowCount > 0 Then
                labInfor.Visible = True
                labInfor.Text = "正在更新数据，请稍候..,"
                Dim StrExe As String
                StrExe = "delete * from " & sCurTableName & ""
                DBdata.Execute(StrExe) ''" & Me.cmbTableName.Text & "'")
                RSdata = DBdata.OpenRecordset(sCurTableName)
                Me.labInfor.Visible = False
                Me.proBar.Visible = True
                Me.proBar.Maximum = Me.dataView.RowCount
                For i = 1 To Me.dataView.RowCount - 1
                    RSdata.AddNew()
                    For j = 1 To Me.dataView.ColumnCount - 1
                        nErrH = i
                        nErrL = j + 1
                        RSdata.Fields(j).Value = Trim(Me.dataView.Rows(i - 1).Cells(j).Value)
                    Next j
                    RSdata.Update()
                    Me.proBar.Value = i
                Next i
                Me.proBar.Visible = False
                MsgBox("保存完毕！！")

                Call DataRefresh()
            Else
                MsgBox("表格中还没有数据，请先添加！")
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox("第" & nErrH & "行" & "第" & nErrL & "列的数据格式不对，请重新输入！！")
            Me.dataView.CurrentCell = Me.dataView.Rows(nErrH - 1).Cells(nErrL - 1)
        End Try
        Me.proBar.Visible = False
        Me.labInfor.Text = ""

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub DataRefresh()
        Dim strTable3 As String = "select * from " & sCurTableName & ""
        Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
        '创建一个Datasetd
        Dim myDataSet3 As Data.DataSet = New Data.DataSet
        Mydc3.Fill(myDataSet3, sCurTableName)
        Dim myTable3 As Data.DataTable
        myTable3 = myDataSet3.Tables(sCurTableName)
        Me.dataView.DataSource = myTable3
    End Sub

    Private Sub btnAddScale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddScale.Click
        Dim sBiaoChiName As String
        Dim i, j, k As Integer
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim str As String
        Dim sSclaeNumber As String
        Dim ifIn As Integer
        Dim sStaName As String
        Dim StrScale() As String
        Dim strScaleNum() As String
        ReDim StrScale(0)
        ReDim strScaleNum(0)

        Call InputStopAndSecScaleinf()
        Dim nf As New frmInputBox

        Select Case sCurTableName
            Case "列车停站标尺"
                For i = 1 To UBound(StopScaleInf)
                    ifIn = 0
                    For j = 1 To UBound(StrScale)
                        If StopScaleInf(i).sScaleName = StrScale(j) Then
                            ifIn = 1
                            Exit For
                        End If
                    Next
                    If ifIn = 0 Then
                        ReDim Preserve StrScale(UBound(StrScale) + 1)
                        StrScale(UBound(StrScale)) = StopScaleInf(i).sScaleName
                        ReDim Preserve strScaleNum(UBound(strScaleNum) + 1)
                        strScaleNum(UBound(strScaleNum)) = StopScaleInf(i).sScaleNum
                    End If
                Next
SBegin:
                nf.labTitle.Text = "请输入标尺名称:"
                nf.txtText.Visible = False
                nf.cmbText.Visible = True
                nf.cmbText.Items.Clear()
                If UBound(StrScale) > 0 Then
                    For i = 1 To UBound(StrScale)
                        nf.cmbText.Items.Add(StrScale(i))
                    Next
                    nf.cmbText.Text = StrScale(1)
                End If
                nf.ShowDialog()
                sBiaoChiName = StrInputBoxCombText
                If sBiaoChiName <> "" And bCancelInput = 0 Then
                    For i = 1 To UBound(StrScale)
                        If StrScale(i) = sBiaoChiName Then
                            MsgBox("该标尺名称已经存在，请重新输入!", , "提示")
                            GoTo SBegin
                        End If
                    Next

SBegin2:
                    nf.labTitle.Text = "请输入标尺编号:"
                    nf.txtText.Visible = False
                    nf.cmbText.Visible = True
                    nf.cmbText.Text = ""
                    nf.cmbText.Items.Clear()
                    For j = 1 To 20
                        ifIn = 0
                        For i = 1 To UBound(strScaleNum)
                            If strScaleNum(i) = j.ToString Then
                                ifIn = 1
                                Exit For
                            End If
                        Next
                        If ifIn = 0 Then
                            nf.cmbText.Items.Add(j.ToString)
                        End If
                    Next

                    If nf.cmbText.Items.Count > 0 Then
                        nf.cmbText.Text = nf.cmbText.Items(0)
                    End If

                    nf.ShowDialog()
                    sSclaeNumber = StrInputBoxCombText
                    If sSclaeNumber <> "" And bCancelInput = 0 Then
                        For i = 1 To UBound(strScaleNum)
                            If strScaleNum(i) = sSclaeNumber Then
                                MsgBox("该标尺编号已经存在，请重新输入!", , "提示")
                                GoTo SBegin2
                            End If
                        Next
                    End If

                    If sSclaeNumber <> "" Then
                        Dim notSameSta() As String
                        ReDim notSameSta(0)
                        Dim IfExistIn As Boolean
                        For i = 1 To UBound(NetInf.Line)
                            If UBound(NetInf.Line(i).Station) > 0 Then
                                For j = 1 To UBound(NetInf.Line(i).Station)
                                    IfExistIn = False
                                    For k = 1 To UBound(notSameSta)
                                        If NetInf.Line(i).Station(j).sStaName = notSameSta(k) Then
                                            IfExistIn = True
                                            Exit For
                                        End If
                                    Next
                                    If IfExistIn = False Then
                                        ReDim Preserve notSameSta(UBound(notSameSta) + 1)
                                        notSameSta(UBound(notSameSta)) = NetInf.Line(i).Station(j).sStaName
                                    End If
                                Next j
                            End If
                        Next i

                        For i = 1 To UBound(notSameSta)
                            sStaName = notSameSta(i)
                            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                            str = "insert into 列车停站标尺 (车站序号,车站名称,标尺编号,标尺名称,下行停站时间,上行停站时间,下行始发停站时间,上行始发停站时间,下行终到停站时间,上行终到停站时间) values ('" & _
                                i & "', '" & _
                                sStaName & "', '" & _
                                sSclaeNumber & "', '" & _
                               sBiaoChiName & "', '" & _
                               "0.00" & " ', '" & _
                               "0.00" & " ', '" & _
                               "0.00" & " ', '" & _
                               "0.00" & " ', '" & _
                               "0.00" & " ', '" & _
                               "0.00" & "')"
                            Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                            Mcom1.ExecuteNonQuery()
                        Next
                        MyConn.Close()
                        Call DataRefresh()
                        MsgBox("已成功添加！")
                    End If
                End If

            Case "列车运行标尺"

                For i = 1 To UBound(SectionScaleInf)
                    ifIn = 0
                    For j = 1 To UBound(StrScale)
                        If SectionScaleInf(i).sScaleName = StrScale(j) Then
                            ifIn = 1
                            Exit For
                        End If
                    Next
                    If ifIn = 0 Then
                        ReDim Preserve StrScale(UBound(StrScale) + 1)
                        StrScale(UBound(StrScale)) = SectionScaleInf(i).sScaleName
                        ReDim Preserve strScaleNum(UBound(strScaleNum) + 1)
                        strScaleNum(UBound(strScaleNum)) = SectionScaleInf(i).sScaleNum
                    End If
                Next
SBegin3:
                nf.labTitle.Text = "请输入标尺名称:"
                nf.txtText.Visible = False
                nf.cmbText.Visible = True
                nf.cmbText.Items.Clear()
                If UBound(StrScale) > 0 Then
                    For i = 1 To UBound(StrScale)
                        nf.cmbText.Items.Add(StrScale(i))
                    Next
                    nf.cmbText.Text = StrScale(1)
                End If
                nf.ShowDialog()
                sBiaoChiName = StrInputBoxCombText
                If sBiaoChiName <> "" And bCancelInput = 0 Then
                    For i = 1 To UBound(StrScale)
                        If StrScale(i) = sBiaoChiName Then
                            MsgBox("该标尺名称已经存在，请重新输入!", , "提示")
                            GoTo SBegin3
                        End If
                    Next

SBegin4:
                    nf.labTitle.Text = "请输入标尺编号:"
                    nf.txtText.Visible = False
                    nf.cmbText.Visible = True
                    nf.cmbText.Text = ""
                    nf.cmbText.Items.Clear()

                    For j = 1 To 10
                        ifIn = 0
                        For i = 1 To UBound(strScaleNum)
                            If strScaleNum(i) = j.ToString Then
                                ifIn = 1
                                Exit For
                            End If
                        Next
                        If ifIn = 0 Then
                            nf.cmbText.Items.Add(j.ToString)
                        End If
                    Next

                    If nf.cmbText.Items.Count > 0 Then
                        nf.cmbText.Text = nf.cmbText.Items(0)
                    End If

                    nf.ShowDialog()
                    sSclaeNumber = StrInputBoxCombText
                    If sSclaeNumber <> "" And bCancelInput = 0 Then
                        For i = 1 To UBound(strScaleNum)
                            If strScaleNum(i) = sSclaeNumber Then
                                MsgBox("该标尺编号已经存在，请重新输入!", , "提示")
                                GoTo SBegin4
                            End If
                        Next
                    End If

                    If sSclaeNumber <> "" Then
                        For i = 1 To UBound(NetInf.Line)
                            If UBound(NetInf.Line(i).Section) > 0 Then
                                For j = 1 To UBound(NetInf.Line(i).Section)
                                    If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                                    str = "insert into 列车运行标尺 (区间序号,线路名称,区间名称,标尺编号,标尺名称,下行运行时分,上行运行时分,下行起车时分,上行起车时分,下行停车时分,上行停车时分) values ('" & _
                                        j & "', '" & _
                                        NetInf.Line(i).sName & "', '" & _
                                        NetInf.Line(i).Section(j).sSecName & "', '" & _
                                        sSclaeNumber & "', '" & _
                                       sBiaoChiName & "', '" & _
                                       "0.00" & " ', '" & _
                                       "0.00" & " ', '" & _
                                       "0.00" & " ', '" & _
                                       "0.00" & " ', '" & _
                                       "0.00" & " ', '" & _
                                       "0.00" & "')"
                                    Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                                    Mcom1.ExecuteNonQuery()
                                Next j
                            End If
                        Next i

                        MyConn.Close()
                        Call DataRefresh()
                        MsgBox("已成功添加！")
                    End If
                End If

        End Select

    End Sub

    Private Sub btnDeleteScale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteScale.Click

        Dim str As String
        Dim sStyle As String
        Dim StrScale() As String
        Dim sBiaoChiName As String
        Dim i, j As Integer
        Dim ifIn As Integer
        Dim nf As New frmInputBox
        Call InputStopAndSecScaleinf()

        Select Case sCurTableName
            Case "列车停站标尺"

                nf.labTitle.Text = "请选择标尺名称:"
                nf.txtText.Visible = False
                nf.cmbText.Visible = True
                nf.cmbText.Items.Clear()

                ReDim StrScale(0)
                For i = 1 To UBound(StopScaleInf)
                    ifIn = 0
                    For j = 1 To UBound(StrScale)
                        If StopScaleInf(i).sScaleName = StrScale(j) Then
                            ifIn = 1
                            Exit For
                        End If
                    Next
                    If ifIn = 0 Then
                        ReDim Preserve StrScale(UBound(StrScale) + 1)
                        StrScale(UBound(StrScale)) = StopScaleInf(i).sScaleName
                    End If
                Next

                If UBound(StrScale) > 0 Then
                    For i = 1 To UBound(StrScale)
                        nf.cmbText.Items.Add(StrScale(i))
                    Next
                    nf.cmbText.Text = StrScale(1)
                End If
                nf.ShowDialog()
                sBiaoChiName = StrInputBoxCombText

                If sBiaoChiName <> "" And bCancelInput = 0 Then
                    sStyle = sBiaoChiName
                    If sStyle <> "" Then
                        If MsgBox("确定删除 [" & sStyle & "] 停站标尺吗?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "确定操作") = MsgBoxResult.Yes Then
                            Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
                            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                            str = "delete * from 列车停站标尺 where  标尺名称='" & sBiaoChiName & "'"
                            Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                            Mcom.ExecuteNonQuery()
                            MyConn.Close()
                            Call DataRefresh()
                            MsgBox("已成功删除！")
                        End If
                    Else
                        MsgBox("先选择要删除的停站标尺!", , "提示")
                    End If
                End If

            Case "列车运行标尺"

                nf.labTitle.Text = "请选择标尺名称:"
                nf.txtText.Visible = False
                nf.cmbText.Visible = True
                nf.cmbText.Items.Clear()

                ReDim StrScale(0)
                For i = 1 To UBound(SectionScaleInf)
                    ifIn = 0
                    For j = 1 To UBound(StrScale)
                        If SectionScaleInf(i).sScaleName = StrScale(j) Then
                            ifIn = 1
                            Exit For
                        End If
                    Next
                    If ifIn = 0 Then
                        ReDim Preserve StrScale(UBound(StrScale) + 1)
                        StrScale(UBound(StrScale)) = SectionScaleInf(i).sScaleName
                    End If
                Next

                If UBound(StrScale) > 0 Then
                    For i = 1 To UBound(StrScale)
                        nf.cmbText.Items.Add(StrScale(i))
                    Next
                    nf.cmbText.Text = StrScale(1)
                End If
                nf.ShowDialog()
                sBiaoChiName = StrInputBoxCombText

                If sBiaoChiName <> "" And bCancelInput = 0 Then
                    sStyle = sBiaoChiName
                    If sStyle <> "" Then
                        If MsgBox("确定删除 [" & sStyle & "] 运行标尺吗?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "确定操作") = MsgBoxResult.Yes Then
                            Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
                            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                            str = "delete * from 列车运行标尺 where  标尺名称='" & sBiaoChiName & "'"
                            Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                            Mcom.ExecuteNonQuery()
                            MyConn.Close()
                            Call DataRefresh()
                            MsgBox("已成功删除！")
                        End If
                    Else
                        MsgBox("先选择要删除的运行标尺!", , "提示")
                    End If
                End If
        End Select
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim New0penFile As New OpenFileDialog
        Dim strExcelFilePath As String
        'New0penFile.InitialDirectory = CurDir()
        New0penFile.Filter = "xls files (*.xls)|*.xls|All files (*.*)|*.*"
        New0penFile.FilterIndex = 1
        New0penFile.RestoreDirectory = True
        strExcelFilePath = ""

        If New0penFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strExcelFilePath = New0penFile.FileName
        End If
        '获得数据库的名称
        If strExcelFilePath <> "" Then

            'Dim DS As System.Data.DataSet
            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
            Dim MyConnection As System.Data.OleDb.OleDbConnection

            MyConnection = New System.Data.OleDb.OleDbConnection( _
                          "provider=Microsoft.Jet.OLEDB.4.0; " & _
                          "data source='" & strExcelFilePath & "'; " & _
                          "Extended Properties=Excel 8.0;")
            ' Select the data from Sheet1 of the workbook.
            Dim tmpStr As String
            tmpStr = "select * from" & "[" & sCurTableName & "$]"
            MyCommand = New System.Data.OleDb.OleDbDataAdapter(tmpStr, MyConnection)

            ' Open connection with the database.
            MyConnection.Open()

            Dim objDataset1 As New DataSet

            ' Fill the DataSet with the information from the worksheet.
            Try
                MyCommand.Fill(objDataset1, "XLData")
                Me.dataView.DataSource = objDataset1.Tables(0)
                MsgBox("打开正确，请检查数据的正确性！")
            Catch ex As Exception
                MsgBox("EXCEL数据库不正确，请确定打开的数据库格式正确!", , "提示")
            End Try
            MyConnection.Close()
            GC.Collect()
        End If
    End Sub
End Class