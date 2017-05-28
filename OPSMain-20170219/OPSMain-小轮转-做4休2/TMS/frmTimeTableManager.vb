Public Class frmTimeTableManager
    Dim sExistSKB() As String
    Dim dSKBFileName As DAO.Database

    Private Sub frmTimeTableManager_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        'Call frmTimeTableMain.listTitle()
    End Sub
    'Dim sUsedSkbNameKe As String

    Private Sub frmTimeTableManager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim myWS As dao.Workspace
        Dim DE As dao.DBEngine = New dao.DBEngine
        myWS = DE.Workspaces(0)
        dSKBFileName = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        Me.txtCurSKB.Text = TimeTablePara.sPubCurSkbName
        Call InputTimetableInf()
        Call SetListSkbName()

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If CheckTimetableBadError() = False Then
            Exit Sub
        End If

        Dim tmpSKBID As String
        tmpSKBID = Now().Year & Now().Month & Now().Day & "_" & Now().Hour & Now().Minute & Now().Second
        Dim sUsedSkbNameKe As String
        sUsedSkbNameKe = Me.txtCurSKB.Text.Trim
        Call SaveSkbName(sUsedSkbNameKe, tmpSKBID)
        'Call frmTimeTableMain.ListFrmTimeTabaleTitle()
    End Sub

    Sub SetListSkbName()
        Dim i As Integer
        Me.lstSKB.Items.Clear()
        For i = 1 To UBound(TimetableInf)
            Me.lstSKB.Items.Add(TimetableInf(i).sName)
        Next i
    End Sub

    '保存时刻表
    Public Sub SaveSkbName(ByVal tmpTableName As String, ByVal tmpSKBID As String)
        Dim i As Integer
        If tmpTableName = "" Then
sName:
            Dim nf As New frmInputBox
            nf.cmbText.Visible = True
            nf.txtText.Visible = False
            nf.Text = "保存时刻表"
            nf.labTitle.Text = "请输入时刻表名称："
            nf.cmbText.Text = tmpTableName
            nf.cmbText.Items.Clear()
            For i = 1 To UBound(TimetableInf)
                nf.cmbText.Items.Add(TimetableInf(i).sName)
            Next i
            nf.ShowDialog()
            tmpTableName = StrInputBoxCombText

            'If IfNameTrue(tmpTableName) = False Then
            '    MsgBox("命名错误，名称中不能有含 ( , ) , - ,空格,等字符！！")
            '    GoTo sname
            'End If
            If tmpTableName <> "" And bCancelInput = 0 Then
                If MsgBox("确认将时刻存入时刻表 " & tmpTableName & "？", 32 + 4, "确认") = vbYes Then
                    SaveSkb(tmpTableName, tmpSKBID)
                    Me.txtCurSKB.Text = tmpTableName
                Else
                    MsgBox("没有进行时刻表保存", vbInformation)
                    Exit Sub
                End If
            Else
                MsgBox("没有进行时刻表保存", vbInformation)
                Exit Sub
            End If
        Else
            SaveSkb(tmpTableName, tmpSKBID)
            Me.txtCurSKB.Text = tmpTableName
        End If

    End Sub

    Private Function bFindSkb(ByVal sSKBName As String) As Boolean
        'Call SetExistSKBName()
        Dim i As Integer
        bFindSkb = False
        For i = 1 To UBound(TimetableInf)
            If TimetableInf(i).sName = sSKBName Then
                bFindSkb = True
                Exit For
            End If
        Next i
    End Function

    Sub SaveSkb(ByVal tmpTableName As String, ByVal tmpSKBID As String)
        Dim tmpId As Integer
        If tmpTableName <> "" Then
            If bFindSkb(tmpTableName) <> True Then
                Call CreatTimeTableTable(tmpSKBID)
                Call CreatNewCDUseTable(tmpSKBID)
                ReDim Preserve TimetableInf(UBound(TimetableInf) + 1)
                TimetableInf(UBound(TimetableInf)).sName = tmpTableName
                TimetableInf(UBound(TimetableInf)).sID = tmpSKBID
                TimetableInf(UBound(TimetableInf)).sCreateDate = Now()
                TimetableInf(UBound(TimetableInf)).sEditDate = Now()
            Else
                If MsgBox("时刻表" & tmpTableName & "已经存在，是否覆盖时刻表原来的数据!", vbQuestion + vbOKCancel + vbDefaultButton2, "保存时刻表") = vbCancel Then
                    Exit Sub
                Else
                    tmpId = GetTimetableInfID(tmpTableName)
                    If tmpId > 0 Then
                        tmpSKBID = TimetableInf(tmpId).sID
                        TimetableInf(tmpId).sEditDate = Now()
                    Else
                        Exit Sub
                    End If
                End If
            End If
            ' Call SaveSkbTimeTable(tmpTableName, Me.probar)
            Call SaveSkbTimeTableByDAO(tmpSKBID, Me.ProBar)
            TimeTablePara.sPubCurSkbName = tmpTableName
            ProBar.Visible = False
            Call SetListSkbName()
            Call SaveTimetableInf()
        End If
    End Sub

    Sub DeleteSKB(ByVal tmpTableName As String)
        Dim NewTrnTime As DAO.Recordset
        Dim Nt As Integer
        Dim i As Integer
        NewTrnTime = dSKBFileName.OpenRecordset(tmpTableName & "列车时刻信息")
        If NewTrnTime.RecordCount > 0 Then
            NewTrnTime.MoveLast()
            Nt = NewTrnTime.RecordCount
        End If
        If Nt > 0 Then
            NewTrnTime.MoveFirst()
            For i = 1 To Nt
                NewTrnTime.Delete()
                NewTrnTime.MoveNext()
            Next i
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Me.txtCurSKB.Text.Trim <> "" Then
            SaveAsSkbName(Me.txtCurSKB.Text.Trim)
        End If
    End Sub

    Sub SaveAsSkbName(ByVal tmpUsedTableName As String)
        Dim sTmpDefaultName As String
        Dim i As Integer
        Dim tmpSKBID As String
        tmpSKBID = Now().Year & Now().Month & Now().Day & "_" & Now().Hour & Now().Minute & Now().Second

        If tmpUsedTableName <> "" Then
sname:
            Dim nf As New frmInputBox
            sTmpDefaultName = tmpUsedTableName
            nf.Text = "另存时刻表"
            nf.labTitle.Text = "将时刻表" & tmpUsedTableName & "另存为："
            nf.txtText.Visible = False
            nf.cmbText.Visible = True
            nf.cmbText.Items.Clear()
            For i = 1 To UBound(TimetableInf)
                nf.cmbText.Items.Add(TimetableInf(i).sName)
            Next i

            nf.cmbText.Text = tmpUsedTableName
            nf.ShowDialog()
            If bCancelInput = 1 Then
                Exit Sub
            End If

            'If IfNameTrue(StrInputBoxCombText) = False Then
            '    MsgBox("命名错误，名称中不能有含 ( , ) , -，; 空格,等字符！！")
            '    GoTo sname
            'End If
        End If
        If StrInputBoxCombText <> "" Then
            If MsgBox("将另存时刻表[" & StrInputBoxCombText & "]", 32 + 4, "另存时刻表") = vbYes Then
                SaveSkb(StrInputBoxCombText, tmpSKBID)
                Me.txtCurSKB.Text = StrInputBoxCombText
            End If
        End If
        Exit Sub
sEnd:
        Exit Sub
    End Sub

    Private Sub btnRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRename.Click
        Dim sChosenSkbNameKe As String
        sChosenSkbNameKe = Me.lstSKB.SelectedItem
        If sChosenSkbNameKe <> "" Then
            ReNameSkbName(sChosenSkbNameKe)
            sChosenSkbNameKe = ""
            Exit Sub
        End If
        If Trim(sChosenSkbNameKe) = "" Then
            MsgBox("请先选择所要重命名的列车时刻表", 48, "时刻表管理")
        End If
    End Sub

    '重命名
    Sub ReNameSkbName(ByVal tmpChosenSkbName As String)
        Dim stmpUsedSkbName As String
        Dim tmpTableName As String
        Dim i As Integer
        stmpUsedSkbName = Me.txtCurSKB.Text.Trim
        If stmpUsedSkbName <> tmpChosenSkbName Then
sname:
            Dim nf As New frmInputBox
            nf.Text = "重命名时刻表"
            nf.labTitle.Text = "将时刻表" & tmpChosenSkbName & "重命名为："
            nf.txtText.Visible = False
            nf.cmbText.Visible = True
            nf.cmbText.Items.Clear()
            For i = 1 To UBound(TimetableInf)
                nf.cmbText.Items.Add(TimetableInf(i).sName)
            Next i

            nf.cmbText.Text = tmpChosenSkbName
            nf.ShowDialog()
            tmpTableName = StrInputBoxCombText
            If tmpTableName <> "" And bCancelInput = 0 Then
                If bFindSkb(tmpTableName) = True Then
                    MsgBox("该时刻表名称已经存在，请重新更改时刻表名称。", 32, "更改名称")
                Else
                    If MsgBox("是否更改" & tmpChosenSkbName & "为" & tmpTableName & "？", 32 + 4, "更改名称") = vbYes Then
                        RenameSkb(tmpChosenSkbName, tmpTableName)
                    End If
                End If
            End If
        Else
            MsgBox("时刻表" & stmpUsedSkbName & "正在使用，不能更改名称。", 48, "时刻表管理")
        End If
    End Sub

    Sub RenameSkb(ByVal OriTableName As String, ByVal NewTablename As String)
        Dim i As Integer
        For i = 1 To UBound(TimetableInf)
            If TimetableInf(i).sName = OriTableName Then
                TimetableInf(i).sName = NewTablename
                Exit For
            End If
        Next
        Call SetListSkbName()
        Call SaveTimetableInf()
  
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Dim sChosenSkbNameKe As String
        sChosenSkbNameKe = Me.lstSKB.SelectedItem
        If sChosenSkbNameKe <> "" Then
            If MsgBox("是否复制" & sChosenSkbNameKe & "？", 32 + 4, "复制时刻表") = vbYes Then
                CopySkbName(sChosenSkbNameKe)
            End If
        Else
            MsgBox("无时刻表选中！请选择时刻表", 48, "时刻表管理")
        End If
    End Sub

    Sub CopySkbName(ByVal stmpChosenSkbName As String)
        Dim tmpTableName As String
        Dim i As Integer
sname:
        Dim nf As New frmInputBox
        nf.Text = "复制时刻表"
        nf.labTitle.Text = "请输入将时刻表 " & stmpChosenSkbName & "复制为新时刻表名称："
        nf.txtText.Visible = False
        nf.cmbText.Visible = True
        nf.cmbText.Items.Clear()
        For i = 1 To UBound(TimetableInf)
            nf.cmbText.Items.Add(TimetableInf(i).sName)
        Next i
        nf.cmbText.Text = stmpChosenSkbName
        nf.ShowDialog()
        If bCancelInput = 1 Then Exit Sub
        tmpTableName = StrInputBoxCombText

        If bFindSkb(tmpTableName) = True Then
            MsgBox("该时刻表名称已经存在，请重新命名！！")
            Exit Sub
        Else
            If tmpTableName <> "" Then
                CopySkb(stmpChosenSkbName, tmpTableName)
            End If
        End If
    End Sub

    Sub CopySkb(ByVal OriTableName As String, ByVal NewTablename As String)
        '   Dim i, j As Integer
        Dim sOldID As String
        Dim sNewID As String
        Dim tmpSKBID As String
        Dim dateNow As Date
        dateNow = Now()
        tmpSKBID = Now().Year & Now().Month & Now().Day & "_" & Now().Hour & Now().Minute & Now().Second
        ReDim Preserve TimetableInf(UBound(TimetableInf) + 1)
        TimetableInf(UBound(TimetableInf)).sName = NewTablename
        TimetableInf(UBound(TimetableInf)).sID = tmpSKBID
        TimetableInf(UBound(TimetableInf)).sCreateDate = dateNow
        TimetableInf(UBound(TimetableInf)).sEditDate = dateNow

        Call CreatTimeTableTable(tmpSKBID)
        Call CreatNewCDUseTable(tmpSKBID)
        Dim NewName As String
        Dim oldName As String
        Dim Str As String
        sNewID = tmpSKBID
        sOldID = GetTimetableIDFromName(OriTableName)
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        NewName = sNewID & "车底使用方案"
        oldName = sOldID & "车底使用方案"
        Str = "insert into " & NewName & " select * from " & oldName & ""
        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()

        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        NewName = sNewID & "列车时刻信息"
        oldName = sOldID & "列车时刻信息"
        Str = "insert into " & NewName & " select * from " & oldName & ""
        Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom1.ExecuteNonQuery()
        MyConn.Close()
        Call SetListSkbName()
        Call SaveTimetableInf()
        MsgBox("时刻表复制完毕！")
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim sChosenSkbNameKe As String
        sChosenSkbNameKe = Me.lstSKB.SelectedItem
        If Trim(sChosenSkbNameKe) = "" Then
            MsgBox("无时刻表选中！请选择时刻表", 48, "时刻表管理")
        End If
        If Trim(sChosenSkbNameKe) <> "" Then
            If MsgBox("是否删除" & sChosenSkbNameKe & ",如果删除，该时刻表信息将不再存在！！？", 32 + 4, "删除时刻表?") = vbYes Then
                If sChosenSkbNameKe = TimeTablePara.sPubCurSkbName Then
                    MsgBox("时刻表" & TimeTablePara.sPubCurSkbName & "正在使用，不能删除", 0 + 48)
                Else
                    If MsgBox("真的要删除" & sChosenSkbNameKe & "吗？", 32 + 4, "删除时刻表吗？请慎重!") = vbYes Then
                        DelSkb(sChosenSkbNameKe)
                    Else
                        Exit Sub
                    End If
                End If
            Else
                Exit Sub
            End If
        End If
    End Sub

    Sub DelSkb(ByVal tmpTableName As String)
        tmpTableName = GetTimetableIDFromName(tmpTableName)
        Try
            Dim i As Integer
            dSKBFileName.TableDefs.Refresh()
            Dim DB As dao.Database
            DB = dSKBFileName
            Dim tempName As String
            For i = 0 To DB.TableDefs.Count - 2
                tempName = DB.TableDefs(i).Name
                If Len(tempName) > 6 Then
                    If tempName.Substring(0, tempName.Length - 6) = tmpTableName Then
                        DB.TableDefs.Delete(tempName)
                        Exit For
                    End If
                End If
            Next i
            DB.TableDefs.Refresh()

            For i = 0 To DB.TableDefs.Count - 1
                tempName = DB.TableDefs(i).Name
                If Len(tempName) > 6 Then
                    If tempName.Substring(0, tempName.Length - 6) = tmpTableName Then
                        DB.TableDefs.Delete(tempName)
                        Exit For
                    End If
                End If
            Next i
            DB.TableDefs.Refresh()
            Dim tmpTimetableInf() As typeTimetableInf
            ReDim tmpTimetableInf(UBound(TimetableInf))
            For i = 1 To UBound(TimetableInf)
                tmpTimetableInf(i).sName = TimetableInf(i).sName
                tmpTimetableInf(i).sID = TimetableInf(i).sID
                tmpTimetableInf(i).sCreateDate = TimetableInf(i).sCreateDate
                tmpTimetableInf(i).sEditDate = TimetableInf(i).sEditDate
            Next
            ReDim TimetableInf(0)
            For i = 1 To UBound(tmpTimetableInf)
                If tmpTimetableInf(i).sID <> tmpTableName Then
                    ReDim Preserve TimetableInf(UBound(TimetableInf) + 1)
                    TimetableInf(UBound(TimetableInf)).sName = tmpTimetableInf(i).sName
                    TimetableInf(UBound(TimetableInf)).sID = tmpTimetableInf(i).sID
                    TimetableInf(UBound(TimetableInf)).sCreateDate = tmpTimetableInf(i).sCreateDate
                    TimetableInf(UBound(TimetableInf)).sEditDate = tmpTimetableInf(i).sEditDate
                End If
            Next

            Call SetListSkbName()
            Call SaveTimetableInf()
        Catch ex As Exception
            MsgBox(ex.Message & "请重新启动程序后再执行该操作!", MsgBoxStyle.Exclamation, )
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub lstSKB_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstSKB.SelectedIndexChanged
        If Me.lstSKB.SelectedIndex >= 0 Then
            Dim nID As Integer
            nID = GetTimetableInfID(Me.lstSKB.Items(Me.lstSKB.SelectedIndex))
            If nID > 0 Then
                Me.labInfor.Text = "ID号   :" & TimetableInf(nID).sID & vbCrLf _
                                       & "创建时间:" & TimetableInf(nID).sCreateDate & vbCrLf _
                                       & "修改时间:" & TimetableInf(nID).sEditDate
            End If
        Else
            Me.labInfor.Text = ""
        End If
    End Sub


    Private Sub btnDeleteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAll.Click
        Dim sChosenSkbNameKe As String
        Dim i As Integer
        Dim sName() As String
        ReDim sName(0)
        For i = 1 To Me.lstSKB.Items.Count
            If Me.lstSKB.Items(i - 1) <> TimeTablePara.sPubCurSkbName Then
                ReDim Preserve sName(UBound(sName) + 1)
                sName(UBound(sName)) = Me.lstSKB.Items(i - 1)
            End If
        Next i
        If MsgBox("是否全部删除当前所有时刻表，如果删除，这些时刻表信息将不再存在！！？，该操作不能还原。", 32 + 4, "删除时刻表?") = vbYes Then
            If MsgBox("真的要删除这些时刻表吗？", 32 + 4, "删除时刻表吗？请慎重!") = vbYes Then
                For i = 1 To UBound(sName)
                    sChosenSkbNameKe = sName(i)
                    If sChosenSkbNameKe = TimeTablePara.sPubCurSkbName Then
                        MsgBox("时刻表" & TimeTablePara.sPubCurSkbName & "正在使用，不能删除", 0 + 48)
                    Else
                        DelSkb(sChosenSkbNameKe)
                    End If
                Next
            Else
                Exit Sub
            End If
        Else
            Exit Sub
        End If

    End Sub
End Class