Public Class frmTrainStopAndSecTime
    Public strPriTrainJiaoLu As String
    Public strPath() As String
    Public nPriUpOrDown As Integer
    Dim CurCellX, CurCellY As Single
    Dim CurMouseX, CurMouseY As Single
    Dim CurCellX1, CurCellY1 As Single
    Dim CurMouseX1, CurMouseY1 As Single

    Private Sub frmTrainStopAndSecTime_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.grdStopSta.Rows.Clear()
        Me.grdQuJian.Rows.Clear()
        Call listTrainStopBiaoChi()
        If Me.cmbStopSta.Text <> "" Then
            Call listStopBiaoChi(Trim(Me.cmbStopSta.Text))
        End If

        Call listTrainQuJian()
        If Me.cmbBiaoChi.Text <> "" Then
            Call listSecBiaoChi(Me.cmbBiaoChi.Text.Trim)
        End If
        Me.Text = "区间与停站标尺: 当前交路名称: " & strPriTrainJiaoLu ' & " 上下行: " & strPriTrainJiaoLu
    End Sub

    '显示停站信息
    Private Sub listTrainStopBiaoChi()

        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim i As Integer
        Dim strTable2 As String = "select distinct 停站种类 from 列车停站标尺信息 where 交路名称='" & strPriTrainJiaoLu & "'"
        Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
        Dim myDataSet2 As Data.DataSet = New Data.DataSet
        Mydc2.Fill(myDataSet2, "列车停站标尺信息")
        Dim myTable2 As Data.DataTable
        myTable2 = myDataSet2.Tables("列车停站标尺信息")
        Me.cmbStopSta.Items.Clear()
        If myTable2.Rows.Count > 0 Then
            For i = 1 To myTable2.Rows.Count
                Me.cmbStopSta.Items.Add(Trim(myTable2.Rows(i - 1).Item("停站种类")))
            Next
            Me.cmbStopSta.Text = Me.cmbStopSta.Items(0)
        Else
            Me.cmbStopSta.Text = ""
        End If
    End Sub
    Private Sub listStopBiaoChi(ByVal sBiaoChi As String)
        If sBiaoChi = "" Then
            Me.grdStopSta.RowCount = 0
            Exit Sub
        End If
        Me.cmbStopSta.Text = sBiaoChi
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim i As Integer
        Dim strTable2 As String = "select * from 列车停站标尺信息 where 交路名称='" & strPriTrainJiaoLu & "' and 停站种类='" & sBiaoChi & "' order by 序号"
        Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
        Dim myDataSet2 As Data.DataSet = New Data.DataSet
        Mydc2.Fill(myDataSet2, "列车停站标尺信息")
        Dim myTable2 As Data.DataTable
        myTable2 = myDataSet2.Tables("列车停站标尺信息")

        If myTable2.Rows.Count > 0 Then
            Me.grdStopSta.RowCount = myTable2.Rows.Count
            For i = 1 To myTable2.Rows.Count
                Me.grdStopSta.Item(0, i - 1).Value = myTable2.Rows(i - 1).Item("序号")
                Me.grdStopSta.Item(1, i - 1).Value = Trim(myTable2.Rows(i - 1).Item("车站名称"))
                Me.grdStopSta.Item(2, i - 1).Value = Trim(myTable2.Rows(i - 1).Item("标尺名称"))
                Me.grdStopSta.Item(3, i - 1).Value = Trim(myTable2.Rows(i - 1).Item("停站时间"))
            Next
        End If

    End Sub

    '显示区间信息
    Private Sub listTrainQuJian()


        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim i As Integer
        Dim strTable2 As String = "select distinct 运行种类 from 列车运行标尺信息 where 交路名称='" & strPriTrainJiaoLu & "'"
        Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
        Dim myDataSet2 As Data.DataSet = New Data.DataSet
        Mydc2.Fill(myDataSet2, "列车运行标尺信息")
        Dim myTable2 As Data.DataTable
        myTable2 = myDataSet2.Tables("列车运行标尺信息")
        Me.cmbBiaoChi.Items.Clear()
        If myTable2.Rows.Count > 0 Then
            For i = 1 To myTable2.Rows.Count
                Me.cmbBiaoChi.Items.Add(Trim(myTable2.Rows(i - 1).Item("运行种类")))
            Next
            Me.cmbBiaoChi.Text = Me.cmbBiaoChi.Items(0)
        Else
            Me.cmbBiaoChi.Text = ""
        End If

    End Sub

    '显示区间标尺
    Private Sub listSecBiaoChi(ByVal sBiaoChi As String)


        If sBiaoChi = "" Then
            Me.grdQuJian.RowCount = 0
            Exit Sub
        End If
        Me.cmbBiaoChi.Text = sBiaoChi
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim i As Integer
        Dim strTable2 As String = "select * from 列车运行标尺信息 where 交路名称='" & strPriTrainJiaoLu & "' and 运行种类='" & sBiaoChi & "' order by 序号"
        Dim Mydc2 As New Data.OleDb.OleDbDataAdapter(strTable2, strCon)
        Dim myDataSet2 As Data.DataSet = New Data.DataSet
        Mydc2.Fill(myDataSet2, "列车运行标尺信息")
        Dim myTable2 As Data.DataTable
        myTable2 = myDataSet2.Tables("列车运行标尺信息")

        If myTable2.Rows.Count > 0 Then
            Me.grdQuJian.RowCount = myTable2.Rows.Count
            For i = 1 To myTable2.Rows.Count
                Me.grdQuJian.Item(0, i - 1).Value = myTable2.Rows(i - 1).Item("序号")
                Me.grdQuJian.Item(1, i - 1).Value = Trim(myTable2.Rows(i - 1).Item("区间名称"))
                Me.grdQuJian.Item(2, i - 1).Value = Trim(myTable2.Rows(i - 1).Item("标尺名称"))
                Me.grdQuJian.Item(3, i - 1).Value = Trim(myTable2.Rows(i - 1).Item("运行时间"))
            Next
        End If

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddStopScale.Click

        Dim sBiaoChiName As String
        Dim i, j As Integer
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim str As String
        Dim sScaleName As String
        Dim ifIn As Integer
        Dim CurStopTime As String
        Dim sUpOrDown As String
        Dim sStaName As String
        Dim sStaStartOrEnd As String
        Dim StrScale() As String
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
SBegin:
        Dim nf As New frmInputBox
        nf.labTitle.Text = "请输入种类名称:"
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
            For i = 1 To Me.cmbStopSta.Items.Count
                If Me.cmbStopSta.Items(i - 1) = sBiaoChiName Then
                    MsgBox("该种类名称已经存在，请重新输入!", , "提示")
                    GoTo SBegin
                End If
            Next
            nf.labTitle.Text = "请选择标尺名称:"
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
            sScaleName = StrInputBoxCombText

            If nPriUpOrDown Mod 2 = 0 Then
                sUpOrDown = "上行"
            Else
                sUpOrDown = "下行"
            End If

            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            For i = 1 To UBound(strPath)
                If i = 1 Then
                    sStaStartOrEnd = "始发站"
                ElseIf i = UBound(strPath) Then
                    sStaStartOrEnd = "终到站"
                Else
                    sStaStartOrEnd = "中间站"
                End If
                sStaName = strPath(i)
                CurStopTime = GetCurStationStopTime(sScaleName, sStaName, sUpOrDown, sStaStartOrEnd)

                str = "insert into 列车停站标尺信息 (交路名称,停站种类,车站名称,序号,标尺名称,停站时间) values ('" & _
                    strPriTrainJiaoLu & "', '" & _
                    sBiaoChiName & "', '" & _
                    strPath(i) & "', '" & _
                   i & "', '" & _
                   sScaleName & " ', '" & _
                   CurStopTime & "')"
                Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                Mcom1.ExecuteNonQuery()
            Next
            MyConn.Close()
            Call listTrainStopBiaoChi()
            Call listStopBiaoChi(sBiaoChiName)
        End If
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnDeleteStopScale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteStopScale.Click
        Dim str As String
        Dim sStyle As String
        sStyle = Me.cmbStopSta.Text.Trim
        If sStyle <> "" Then
            If MsgBox("确定删除 [" & sStyle & "] 停站类型吗?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "确定操作") = MsgBoxResult.Yes Then
                Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
                If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                str = "delete * from 列车停站标尺信息 where 停站种类='" & sStyle & "' and 交路名称='" & strPriTrainJiaoLu & "'"
                Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                Mcom.ExecuteNonQuery()
                MyConn.Close()

                Call listTrainStopBiaoChi()
                If Me.cmbStopSta.Text <> "" Then
                    Call listStopBiaoChi(Trim(Me.cmbStopSta.Text))
                Else
                    Call listStopBiaoChi("")
                End If
            End If
        Else
            MsgBox("先选择要删除的停站种类!", , "提示")
        End If
    End Sub

    Private Sub btnSaveStopScale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveStopScale.Click
        Dim str As String
        Dim i As Integer
        Dim sStyle As String
        sStyle = Me.cmbStopSta.Text.Trim
        If sStyle <> "" Then
            Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            str = "delete * from 列车停站标尺信息 where 停站种类='" & sStyle & "' and 交路名称='" & strPriTrainJiaoLu & "'"
            Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
            Mcom.ExecuteNonQuery()

            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            For i = 1 To Me.grdStopSta.RowCount
                str = "insert into 列车停站标尺信息 (交路名称,停站种类,车站名称,序号,标尺名称,停站时间) values ('" & _
                strPriTrainJiaoLu & "', '" & _
                sStyle & "', '" & _
                Me.grdStopSta.Item(1, i - 1).Value & "', '" & _
               i & "', '" & _
                Me.grdStopSta.Item(2, i - 1).Value & "', '" & _
                Me.grdStopSta.Item(3, i - 1).Value & "')"
                Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                Mcom1.ExecuteNonQuery()
            Next
            MsgBox("保存完毕!", , "提示")
            MyConn.Close()
            'Call listTrainStopBiaoChi()
            'Call listStopBiaoChi(sStyle)
        Else
            MsgBox("先选择要保存的停站种类!", , "提示")
        End If
    End Sub


    Private Sub cmbStopSta_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStopSta.SelectedValueChanged
        Call listStopBiaoChi(Me.cmbStopSta.Text.Trim)
    End Sub

    Private Sub btnAddSecScale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSecScale.Click
        Dim sBiaoChiName As String
        Dim i, j As Integer
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim str As String
        Dim sFirSta As String
        Dim SSecSTa As String
        Dim sSecName As String
        Dim strTime As String
        strTime = "0.00"
        Dim sUpOrDown As String
        Dim sScaleName As String
        Dim ifIn As Integer
        Dim StrScale() As String
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

SBegin:
        Dim nf As New frmInputBox
        nf.labTitle.Text = "请输入种类名称:"
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
            For i = 1 To Me.cmbBiaoChi.Items.Count
                If Me.cmbBiaoChi.Items(i - 1) = sBiaoChiName Then
                    MsgBox("该运行种类名称已经存在，请重新输入!", , "提示")
                    GoTo SBegin
                End If
            Next

            nf.labTitle.Text = "请选择标尺名称:"
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
            sScaleName = StrInputBoxCombText

            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            For i = 2 To UBound(strPath)
                sFirSta = strPath(i - 1)
                SSecSTa = strPath(i)
                If nPriUpOrDown = 1 Then
                    sSecName = sFirSta & "->" & SSecSTa
                Else
                    sSecName = SSecSTa & "->" & sFirSta
                End If

                If nPriUpOrDown Mod 2 = 0 Then
                    sUpOrDown = "上行"
                Else
                    sUpOrDown = "下行"
                End If
                strTime = GetCurSectionRunTime(sScaleName, sSecName, sUpOrDown)

                str = "insert into 列车运行标尺信息 (交路名称,运行种类,区间名称,序号,标尺名称,运行时间) values ('" & _
                    strPriTrainJiaoLu & "', '" & _
                    sBiaoChiName & "', '" & _
                    sSecName & "', '" & _
                   i - 1 & "', '" & _
                  sScaleName & " ', '" & _
                   strTime & "')"
                Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                Mcom1.ExecuteNonQuery()
            Next
            MyConn.Close()
            Call listTrainQuJian()
            Call listSecBiaoChi(sBiaoChiName)
        End If
    End Sub

    Private Sub btnDeleteSecScale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteSecScale.Click
        Dim str As String
        Dim sStyle As String
        sStyle = Me.cmbBiaoChi.Text.Trim
        If sStyle <> "" Then
            If MsgBox("确定删除 [" & sStyle & "] 运行种类吗?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "确定操作") = MsgBoxResult.Yes Then
                Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
                If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                str = "delete * from 列车运行标尺信息 where 运行种类='" & sStyle & "' and 交路名称='" & strPriTrainJiaoLu & "'"
                Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                Mcom.ExecuteNonQuery()
                MyConn.Close()

                Call listTrainQuJian()
                If Me.cmbBiaoChi.Text <> "" Then
                    Call listSecBiaoChi(Trim(Me.cmbBiaoChi.Text))
                Else
                    Call listSecBiaoChi("")
                End If
            End If
        Else
            MsgBox("先选择要删除的运行种类!", , "提示")
        End If
    End Sub

    Private Sub btnSaveSecScale_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSecScale.Click
        Dim str As String
        Dim i As Integer
        Dim sStyle As String
        sStyle = Me.cmbBiaoChi.Text.Trim
        If sStyle <> "" Then
            Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            str = "delete * from 列车运行标尺信息 where 运行种类='" & sStyle & "' and 交路名称='" & strPriTrainJiaoLu & "'"
            Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
            Mcom.ExecuteNonQuery()
            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            For i = 1 To Me.grdQuJian.RowCount
                str = "insert into 列车运行标尺信息 (交路名称,运行种类,区间名称,序号,标尺名称,运行时间) values ('" & _
                strPriTrainJiaoLu & "', '" & _
                sStyle & "', '" & _
                Me.grdQuJian.Item(1, i - 1).Value & "', '" & _
               i & "', '" & _
                Me.grdQuJian.Item(2, i - 1).Value & "', '" & _
                Me.grdQuJian.Item(3, i - 1).Value & "')"
                Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                Mcom1.ExecuteNonQuery()
            Next
            MsgBox("保存完毕!", , "提示")
            MyConn.Close()
        Else
            MsgBox("先选择要保存的运行种类!", , "提示")
        End If
    End Sub

    Private Sub cmbBiaoChi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbBiaoChi.SelectedIndexChanged
        Call listSecBiaoChi(Me.cmbBiaoChi.Text.Trim)
    End Sub

    Private Sub btnAutoCreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoCreate.Click
        Call AutoCreateTrainSecScale(strPriTrainJiaoLu, nPriUpOrDown, strPath)
        Call AutoCreateTrainStaScale(strPriTrainJiaoLu, nPriUpOrDown, strPath)

        Call listTrainStopBiaoChi()
        If Me.cmbStopSta.Text <> "" Then
            Call listStopBiaoChi(Trim(Me.cmbStopSta.Text))
        End If

        Call listTrainQuJian()
        If Me.cmbBiaoChi.Text <> "" Then
            Call listSecBiaoChi(Me.cmbBiaoChi.Text.Trim)
        End If

        MsgBox("自动生成完毕！")
    End Sub

    Private Sub grdStopSta_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdStopSta.CellClick
        Dim i As Integer
        Dim j As Integer
        If Me.grdStopSta.CurrentCell.RowIndex >= 0 And Me.grdStopSta.Columns(Me.grdStopSta.CurrentCell.ColumnIndex).Name = "标尺名称" Then
            If CurCellX > 0 And CurCellY > 0 Then

                Me.cmbEditTime.Left = CurMouseX - CurCellX + Me.grdStopSta.Left
                Me.cmbEditTime.Top = CurMouseY - CurCellY + Me.grdStopSta.Top
                Me.cmbEditTime.Width = Me.grdStopSta.Columns(Me.grdStopSta.CurrentCell.ColumnIndex).Width
                Me.cmbEditTime.Items.Clear()

                Dim ifIn As Integer
                For i = 1 To UBound(StopScaleInf)
                    ifIn = 0
                    For j = 1 To Me.cmbEditTime.Items.Count
                        If StopScaleInf(i).sScaleName = Me.cmbEditTime.Items(j - 1).ToString Then
                            ifIn = 1
                            Exit For
                        End If
                    Next
                    If ifIn = 0 Then
                        Me.cmbEditTime.Items.Add(StopScaleInf(i).sScaleName)
                    End If
                Next
                Me.cmbEditTime.Text = Me.grdStopSta.CurrentCell.Value
                Me.cmbEditTime.Visible = True
                Me.cmbEditTime.Select()
                CurCellX = 0
                CurCellY = 0
            End If
        End If
    End Sub

    Private Sub grdStopSta_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdStopSta.CellMouseDown
        Me.cmbEditTime.Visible = False
        CurCellX = e.X
        CurCellY = e.Y
    End Sub

    Private Sub cmbEditTime_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEditTime.SelectedValueChanged
        Me.grdStopSta.CurrentCell.Value = Me.cmbEditTime.Text
        Call ValueChange()
        Me.cmbEditTime.Visible = False
    End Sub

    Private Sub grdStopSta_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdStopSta.MouseDown
        CurMouseX = e.X
        CurMouseY = e.Y
    End Sub

    Private Sub ValueChange()
        Dim CurStopTime As String
        Dim sUpOrDown As String
        Dim sStaName As String
        Dim sScaleName As String
        Dim sStaStartOrEnd As String
        If nPriUpOrDown Mod 2 = 0 Then
            sUpOrDown = "上行"
        Else
            sUpOrDown = "下行"
        End If
        sStaName = Me.grdStopSta.Item(1, Me.grdStopSta.CurrentCell.RowIndex).Value.ToString.Trim
        sScaleName = Me.grdStopSta.CurrentCell.Value.ToString.Trim
        If Me.grdStopSta.CurrentCell.RowIndex = 0 Then
            sStaStartOrEnd = "始发站"
        ElseIf Me.grdStopSta.CurrentCell.RowIndex = Me.grdStopSta.RowCount Then
            sStaStartOrEnd = "终到站"
        Else
            sStaStartOrEnd = "中间站"
        End If
        CurStopTime = GetCurStationStopTime(sScaleName, sStaName, sUpOrDown, sStaStartOrEnd)
        Me.grdStopSta.Item(3, Me.grdStopSta.CurrentCell.RowIndex).Value = CurStopTime
    End Sub

    Private Sub grdQuJian_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdQuJian.CellClick
        Dim i As Integer
        Dim j As Integer
        If Me.grdQuJian.CurrentCell.RowIndex >= 0 And Me.grdQuJian.Columns(Me.grdQuJian.CurrentCell.ColumnIndex).Name = "标尺名称" Then
            If CurCellX1 > 0 And CurCellY1 > 0 Then
                Me.cmbEditTime1.Left = CurMouseX1 - CurCellX1 + Me.grdQuJian.Left
                Me.cmbEditTime1.Top = CurMouseY1 - CurCellY1 + Me.grdQuJian.Top
                Me.cmbEditTime1.Width = Me.grdQuJian.Columns(Me.grdQuJian.CurrentCell.ColumnIndex).Width
                Me.cmbEditTime1.Items.Clear()

                Dim ifIn As Integer
                For i = 1 To UBound(SectionScaleInf)
                    ifIn = 0
                    For j = 1 To Me.cmbEditTime1.Items.Count
                        If SectionScaleInf(i).sScaleName = Me.cmbEditTime1.Items(j - 1).ToString Then
                            ifIn = 1
                            Exit For
                        End If
                    Next
                    If ifIn = 0 Then
                        Me.cmbEditTime1.Items.Add(SectionScaleInf(i).sScaleName)
                    End If
                Next
                Me.cmbEditTime1.Text = Me.grdQuJian.CurrentCell.Value
                Me.cmbEditTime1.Visible = True
                Me.cmbEditTime1.Select()
                CurCellX1 = 0
                CurCellY1 = 0
            End If
        End If

    End Sub

    Private Sub grdQuJian_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdQuJian.CellMouseDown
        Me.cmbEditTime1.Visible = False
        CurCellX1 = e.X
        CurCellY1 = e.Y
    End Sub

    Private Sub grdQuJian_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdQuJian.MouseDown
        CurMouseX1 = e.X
        CurMouseY1 = e.Y
    End Sub

    Private Sub cmbEditTime1_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEditTime1.SelectedValueChanged
        Me.grdQuJian.CurrentCell.Value = Me.cmbEditTime1.Text
        Call ValueChange1()
        Me.cmbEditTime1.Visible = False
    End Sub

    Private Sub ValueChange1()
        Dim CurStopTime As String
        Dim sUpOrDown As String
        Dim sSecName As String
        Dim sScaleName As String
        If nPriUpOrDown Mod 2 = 0 Then
            sUpOrDown = "上行"
        Else
            sUpOrDown = "下行"
        End If
        sSecName = Me.grdQuJian.Item(1, Me.grdQuJian.CurrentCell.RowIndex).Value.ToString.Trim
        sScaleName = Me.grdQuJian.CurrentCell.Value.ToString.Trim
        CurStopTime = GetCurSectionRunTime(sScaleName, sSecName, sUpOrDown)
        Me.grdQuJian.Item(3, Me.grdQuJian.CurrentCell.RowIndex).Value = CurStopTime
    End Sub
End Class