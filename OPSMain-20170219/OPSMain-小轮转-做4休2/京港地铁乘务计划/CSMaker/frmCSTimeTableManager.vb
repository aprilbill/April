
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class frmCSTimeTableManager
    ''Dim sExistSKB() As String
    ''Dim dSKBFileName As DAO.Database
    Public strtempCurlineID As String = ""

    Private Sub frmTimeTableManager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim sqlstr As String = ""
        If CurLineName = "" Then
            sqlstr = "SELECT * FROM PD_LINEINFO WHERE 1=1 order by lineid asc"
            tempTable = New Data.DataTable
            tempTable = Globle.Method.ReadDataForAccess(sqlstr)

            Dim i As Integer
            For i = 0 To tempTable.Rows.Count - 1
                Me.ComlineInf.Items.Add(tempTable.Rows(i).Item("LINENAME").ToString.Trim)
            Next
            If Me.ComlineInf.Items.Count > 0 Then
                Me.ComlineInf.SelectedIndex = 0

            End If

            'Me.ComlineInf.DataSource = LocalDataSet.PD_LINEINFO
            'Me.ComlineInf.DisplayMember = "LINENAME"
            strtempCurlineID = Me.ComlineInf.Text.ToString.Trim

            Call InputCSTimetableInf(strtempCurlineID) '改完
            If CSTimetableInf Is Nothing = False Then
                Me.lstCSTT.Items.Clear()
                For i = 1 To UBound(CSTimetableInf)
                    Me.lstCSTT.Items.Add(CSTimetableInf(i).sName)
                Next i
            Else
                MsgBox("当前还没有乘务计划，请先制定乘务计划！", MsgBoxStyle.OkOnly)
                Exit Sub
            End If
        Else
            Me.ComlineInf.Items.Add(CurLineName)
        End If
    End Sub

    Public Sub SetListSkbName()
        Dim i As Integer
        Me.lstCSTT.Items.Clear()
        For i = 1 To UBound(CSTimetableInf)
            Me.lstCSTT.Items.Add(CSTimetableInf(i).sName)
        Next i
    End Sub

    Private Sub btnRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRename.Click
        Dim sChosenSkbNameKe As String
        sChosenSkbNameKe = Me.lstCSTT.SelectedItem
        If Trim(sChosenSkbNameKe) = "" Then
            MsgBox("请先选择所要重命名的列车时刻表", 48, "时刻表管理")
        Else
            ReNameSkbName(sChosenSkbNameKe)
        End If
    End Sub

    '重命名
    Sub ReNameSkbName(ByVal tmpChosenSkbName As String)
        'Dim stmpUsedSkbName As String
        Dim tmpTableName As String
        Dim i As Integer
        'stmpUsedSkbName = Me.txtCurCSSKB.Text.Trim
        If strQCurCSPlanName <> tmpChosenSkbName Then
            'sname:
            Dim nf As New frmInputBox
            nf.Text = "重命名乘务计划表"
            nf.labTitle.Text = "将乘务计划表" & tmpChosenSkbName & "重命名为："
            nf.txtText.Visible = False
            nf.cmbText.Visible = True
            nf.cmbText.Items.Clear()
            For i = 1 To UBound(CSTimetableInf)
                nf.cmbText.Items.Add(CSTimetableInf(i).sName)
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
            MsgBox("时刻表" & strQCurCSPlanName & "正在使用，不能更改名称。", 48, "时刻表管理")
        End If
    End Sub
    Public Function bFindSkb(ByVal sCSSKBName As String) As Boolean
        'Call SetExistSKBName()
        Dim i As Integer
        bFindSkb = False
        For i = 1 To UBound(CSTimetableInf)
            If CSTimetableInf(i).sName = sCSSKBName Then
                bFindSkb = True
                Exit For
            End If
        Next i
    End Function

    Sub RenameSkb(ByVal OriTableName As String, ByVal NewTablename As String)
        Dim i As Integer
        For i = 1 To UBound(CSTimetableInf)
            If CSTimetableInf(i).sName = OriTableName Then
                CSTimetableInf(i).sName = NewTablename
                Exit For
            End If
        Next
        Call SetListSkbName()
        Call SaveCSTimetableInf()
    End Sub
    '保存时刻表信息
    Public Sub SaveCSTimetableInf()

        Dim sqlstr As String = ""
        sqlstr = "DELETE FROM CS_CSTIMETABLEINF WHERE LINEID='" & CStr(strtempCurlineID) & "'"
        Globle.Method.UpdateDataForAccess(sqlstr)
        sqlstr = "SELECT * FROM CS_CSTIMETABLEINF "
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        For i As Integer = 1 To UBound(CSTimetableInf)
            tempTable.Rows.Add(CStr(strtempCurlineID), CStr(CSTimetableInf(i).sName), CStr(CSTimetableInf(i).sID), CStr(CSTimetableInf(i).sCreateDate), CStr(CSTimetableInf(i).sEditDate), "", CStr(CSTimetableInf(i).sDiagramID))
        Next
        Globle.Method.UpdateDataForAccess(sqlstr, tempTable)
        tempTable.Dispose()
    End Sub

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Dim sChosenSkbNameKe As String
        sChosenSkbNameKe = Me.lstCSTT.SelectedItem
        If sChosenSkbNameKe <> "" Then
            If MsgBox("是否复制" & sChosenSkbNameKe & "？", 32 + 4, "复制乘务计划表") = vbYes Then
                CopySkbName(sChosenSkbNameKe)
            End If
        Else
            MsgBox("无乘务计划表选中！请选择乘务计划表", 48, "乘务计划表管理")
        End If
    End Sub

    Sub CopySkbName(ByVal stmpChosenSkbName As String)
        Dim tmpTableName As String
        Dim i As Integer
sname:
        Dim nf As New frmInputBox
        nf.Text = "复制乘务计划表"
        nf.labTitle.Text = "请输入将乘务计划表 " & stmpChosenSkbName & "复制为新乘务计划表名称："
        nf.txtText.Visible = False
        nf.cmbText.Visible = True
        nf.cmbText.Items.Clear()
        For i = 1 To UBound(CSTimetableInf)
            nf.cmbText.Items.Add(CSTimetableInf(i).sName)
        Next i
        nf.cmbText.Text = stmpChosenSkbName
        nf.ShowDialog()
        If bCancelInput = 1 Then Exit Sub
        tmpTableName = StrInputBoxCombText

        If bFindSkb(tmpTableName) = True Then
            MsgBox("该乘务计划表名称已经存在，请重新命名！！")
            Exit Sub
        Else
            If tmpTableName <> "" Then
                CopySkb(stmpChosenSkbName, tmpTableName)
            End If
        End If
    End Sub

    Sub CopySkb(ByVal OriTableName As String, ByVal NewTablename As String)
        Dim sOldID, sDiagramID As String
        sDiagramID = ""
        Dim i As Integer
        Dim str As String
        Dim tab As New DataTable
        Dim dateNow As Date = Now()
        Dim tmpNewTime As String = dateNow.Year & dateNow.Month & dateNow.Day & "_" & dateNow.Hour & dateNow.Minute & dateNow.Second
        Dim sNewID As String = tmpNewTime + "CS"
        For i = 1 To UBound(CSTimetableInf)
            If OriTableName = CSTimetableInf(i).sName Then
                sDiagramID = CSTimetableInf(i).sDiagramID
                Exit For
            End If
        Next
        ReDim Preserve CSTimetableInf(UBound(CSTimetableInf) + 1)
        CSTimetableInf(UBound(CSTimetableInf)).nID = UBound(CSTimetableInf)
        CSTimetableInf(UBound(CSTimetableInf)).sName = NewTablename
        CSTimetableInf(UBound(CSTimetableInf)).sID = sNewID
        CSTimetableInf(UBound(CSTimetableInf)).sCreateDate = dateNow
        CSTimetableInf(UBound(CSTimetableInf)).sEditDate = dateNow
        CSTimetableInf(UBound(CSTimetableInf)).sDiagramID = sDiagramID

        sOldID = GetCSTimetableIDFromName(OriTableName)


        '复制乘务计划表+++++ 
        str = "SELECT  * FROM CS_CREWSCHEDULE WHERE LINEID='" & CStr(strtempCurlineID) & "' AND CSTIMETABLEID='" & CStr(sOldID) & "'"
        tab = Globle.Method.ReadDataForAccess(str)
        Common.Global.StartProgress(4, "乘务计划复制中，请稍等...")

        str = "SELECT * FROM CS_CREWSCHEDULE "
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(str)
        Common.Global.PerformStep()
        For i = 0 To tab.Rows.Count - 1
            tab.Rows(i).Item("CSTIMETABLEID") = CStr(sNewID)
            tempTable.Rows.Add(tab.Rows(i).Item("LINEID").ToString.Trim, _
                                                               tab.Rows(i).Item("CSTIMETABLEID").ToString.Trim, _
                                                               tab.Rows(i).Item("ID").ToString.Trim, _
                                                               tab.Rows(i).Item("DRIVERNO").ToString.Trim, _
                                                               tab.Rows(i).Item("DATENO").ToString.Trim, _
                                                               tab.Rows(i).Item("DUTYSORT").ToString.Trim, _
                                                               tab.Rows(i).Item("TRAINNO").ToString.Trim, _
                                                               tab.Rows(i).Item("STARTTIME").ToString.Trim, _
                                                               tab.Rows(i).Item("STARTSTANAME").ToString.Trim, _
                                                               tab.Rows(i).Item("ENDTIME").ToString.Trim, _
                                                               tab.Rows(i).Item("ENDSTANAME").ToString.Trim, _
                                                               tab.Rows(i).Item("TRAINID").ToString.Trim, _
                                                               tab.Rows(i).Item("PATH1").ToString.Trim, _
                                                               tab.Rows(i).Item("PATH2").ToString.Trim, _
                                                               tab.Rows(i).Item("CHEDIID").ToString.Trim, _
                                                               tab.Rows(i).Item("UPORDOWN").ToString.Trim, _
                                                               tab.Rows(i).Item("ZFTIME").ToString.Trim, _
                                                               tab.Rows(i).Item("STASEQ1").ToString.Trim, _
                                                               tab.Rows(i).Item("STASEQ2").ToString.Trim)
        Next
        Common.Global.PerformStep()
        Globle.Method.UpdateDataForAccess(str, tempTable)
        '******************************************************
        Common.Global.PerformStep()


        '复制劳动量负荷表+++++ 
        str = "SELECT  * FROM CS_WORKLOAD WHERE LINEID='" & CStr(strtempCurlineID) & "' AND CSTIMETABLEID='" & CStr(sOldID) & "'"
        tab = New DataTable
        tab = Globle.Method.ReadDataForAccess(str)

        str = "SELECT * FROM CS_WORKLOAD "
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(str)
        For i = 0 To tab.Rows.Count - 1
            tab.Rows(i).Item("CSTIMETABLEID") = CStr(sNewID)
            tempTable.Rows.Add(tab.Rows(i).Item("LINEID").ToString.Trim, _
                                                               tab.Rows(i).Item("CSTIMETABLEID").ToString.Trim, _
                                                               tab.Rows(i).Item("DRIVERNO").ToString.Trim, _
                                                               tab.Rows(i).Item("DATENO").ToString.Trim, _
                                                               tab.Rows(i).Item("DUTYSORT").ToString.Trim, _
                                                               tab.Rows(i).Item("WORKTIME").ToString.Trim, _
                                                               tab.Rows(i).Item("TOTALZFTIME").ToString.Trim, _
                                                               tab.Rows(i).Item("DRIVETIME").ToString.Trim, _
                                                                tab.Rows(i).Item("DRIVEdistance").ToString.Trim)
        Next
        Globle.Method.UpdateDataForAccess(str, tempTable)
        '******************************************************
        Common.Global.PerformStep()
        Common.Global.EndProgress()
        Call SetListSkbName()
        Call SaveCSTimetableInf()
        MsgBox("乘务计划表复制完毕！")
    End Sub
    '根据时刻表名称得到时刻表ID
    Public Function GetCSTimetableIDFromName(ByVal sName As String) As String
        Dim i As Integer
        GetCSTimetableIDFromName = ""
        For i = 1 To UBound(CSTimetableInf)
            If CSTimetableInf(i).sName = sName Then
                GetCSTimetableIDFromName = CSTimetableInf(i).sID
                Exit For
            End If
        Next
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim sChosenSkbNameKe As String
        sChosenSkbNameKe = Me.lstCSTT.SelectedItem
        If Trim(sChosenSkbNameKe) = "" Then
            MsgBox("无乘务计划表选中！请选择乘务计划表", 48, "乘务计划表管理")
        Else
            If MsgBox("是否删除《" & sChosenSkbNameKe & "》,如果删除，该乘务计划表信息将不再存在！！？", 32 + 4, "删除乘务计划表?") = vbYes Then
                If sChosenSkbNameKe = strQCurCSPlanID Or sChosenSkbNameKe = strQCurCSPlanName Then
                    MsgBox("乘务计划表" & sChosenSkbNameKe & "正在使用，不能删除", 0 + 48)
                Else
                    If MsgBox("真的要删除《" & sChosenSkbNameKe & "》吗？", 32 + 4, "删除乘务计划表吗？请慎重!") = vbYes Then
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
        Dim tmpCSTableID As String = GetCSTimetableIDFromName(tmpTableName)
        Dim sqlstr As String = ""
        Try
            sqlstr = "DELETE from CS_CREWSCHEDULE WHERE LINEID='" & CStr(strtempCurlineID) & "' AND CSTIMETABLEID ='" & CStr(tmpCSTableID) & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "DELETE from CS_WORKLOAD WHERE LINEID='" & CStr(strtempCurlineID) & "' AND CSTIMETABLEID ='" & CStr(tmpCSTableID) & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "DELETE from CS_CSTIMETABLEINF WHERE LINEID='" & CStr(strtempCurlineID) & "' AND CSTIMETABLEID ='" & CStr(tmpCSTableID) & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "DELETE from CS_DINNERINF WHERE LINEID='" & CStr(strtempCurlineID) & "' AND CSTIMETABLEID ='" & CStr(tmpCSTableID) & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "DELETE from cs_amdrivercorrespond WHERE LINEID='" & CStr(strtempCurlineID) & "' AND (adrivertimetableid ='" & CStr(tmpCSTableID) & "' or mdrivertimetableid='" & CStr(tmpCSTableID) & "')"
            Globle.Method.UpdateDataForAccess(sqlstr)
            'sqlstr = "DELETE from cs_deadheading WHERE LINEID='" & CStr(strtempCurlineID) & "' AND CSTIMETABLEID ='" & CStr(tmpCSTableID) & "'"
            'Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "DELETE from cs_autoplanpara WHERE CSTIMETABLEID ='" & CStr(tmpCSTableID) & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            
            '保存设置的参数信息
            sqlstr = "delete from CS_RESULT_PREPAREDDUTYINF where cstimetableid='" & CStr(tmpCSTableID) & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)
            sqlstr = "delete from CS_RESULT_PREPAREDTRAININF where cstimetableid='" & CStr(tmpCSTableID) & "'"
            Globle.Method.UpdateDataForAccess(sqlstr)

            Call InputCSTimetableInf(strtempCurlineID)
            Call SetListSkbName()
        Catch ex As Exception
            MsgBox(ex.Message & "请重新启动程序后再执行该操作!", MsgBoxStyle.Exclamation, )
        End Try

    End Sub


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub lstSKB_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCSTT.SelectedIndexChanged
        If Me.lstCSTT.SelectedIndex >= 0 Then
            Dim nID As Integer
            nID = GetCSTimetableInfID(Me.lstCSTT.Items(Me.lstCSTT.SelectedIndex))
            If nID > 0 Then
                Me.labInfor.Text = "ID号   :" & CSTimetableInf(nID).sID & vbCrLf _
                                       & "创建时间:" & CSTimetableInf(nID).sCreateDate & vbCrLf _
                                       & "修改时间:" & CSTimetableInf(nID).sEditDate
            End If
        Else
            Me.labInfor.Text = ""
        End If
    End Sub


    Private Sub btnDeleteAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteAll.Click
        Dim sChosenSkbNameKe As String
        Dim i As Integer
        If MsgBox("是否全部删除当前所有乘务计划表，如果删除，这些乘务计划表信息将不再存在！！？，该操作不能还原。", 32 + 4, "删除乘务计划表?") = vbYes Then
            If MsgBox("真的要删除这些乘务计划表吗？", 32 + 4, "删除乘务计划表吗？请慎重!") = vbYes Then
                For i = 1 To Me.lstCSTT.Items.Count
                    If Me.lstCSTT.Items.Count <> 0 Then
                        sChosenSkbNameKe = Me.lstCSTT.Items(i - 1).ToString.Trim
                        If sChosenSkbNameKe = strQCurCSPlanID Or sChosenSkbNameKe = strQCurCSPlanName Then
                            MsgBox("乘务计划表" & sChosenSkbNameKe & "正在使用，不能删除", 0 + 48)
                        Else
                            DelSkb(sChosenSkbNameKe)
                            i -= 1
                        End If
                    End If
                Next
            Else
                Exit Sub
            End If
        Else
            Exit Sub
        End If
    End Sub

    Private Sub ComlineInf_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComlineInf.SelectedIndexChanged
        strtempCurlineID = Me.ComlineInf.Text.ToString.Trim
        Call InputCSTimetableInf(strtempCurlineID) '改完
        If CSTimetableInf Is Nothing = False Then
            Dim i As Integer
            Me.lstCSTT.Items.Clear()
            For i = 1 To UBound(CSTimetableInf)
                Me.lstCSTT.Items.Add(CSTimetableInf(i).sName)
            Next i
        Else
            MsgBox("当前还没有乘务计划，请先制定乘务计划！", MsgBoxStyle.OkOnly)
            Exit Sub
        End If
    End Sub

    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub
End Class