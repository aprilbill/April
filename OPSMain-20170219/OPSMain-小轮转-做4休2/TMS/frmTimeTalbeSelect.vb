Public Class frmTimeTalbeSelect
    Public frmOk As Boolean

    Private Sub frmTimeTalbeSelect_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ReDim TimetableInf(0)
        Call InputTimetableInf()
        Dim i As Integer
        For i = 1 To UBound(TimetableInf)
            Me.lstName.Items.Add(TimetableInf(i).sName)
        Next
        frmOk = False
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        Dim StrTmp As String
        If Me.lstName.SelectedIndex >= 0 Then
            Call InitSystemVariant(0)
            StrTmp = Me.lstName.Items(Me.lstName.SelectedIndex)
            TimeTablePara.sPubCurSkbName = StrTmp ' StrTmp.Substring(0, Len(StrTmp) - 6)
            Me.proBar.Visible = True
            Me.proBar.Maximum = 100
            Dim sSKBID As String
            sSKBID = GetTimetableIDFromName(StrTmp)
            Call ReadTrainAndTimeTableInf(sSKBID)
            'Call RefreshDiagram(frmTimeTableMain.PicStation, frmTimeTableMain.PicDiagram)
            Me.proBar.Visible = False
            sysMenuState = "打开了数据库"
            'Call frmTimeTableMain.ReSetMenuState()
            frmOk = True
            Me.Close()
        End If

    End Sub

    Private Sub cmbCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCancel.Click
        frmOk = False
        Me.Close()
    End Sub

    Private Sub lstName_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstName.DoubleClick
        Call cmdOk_Click(Nothing, Nothing)
    End Sub


    Private Sub lstName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstName.SelectedIndexChanged
        If Me.lstName.SelectedIndex >= 0 Then
            Dim nID As Integer
            nID = GetTimetableInfID(Me.lstName.Items(Me.lstName.SelectedIndex))
            If nID > 0 Then
                Me.LabInfor.Text = "ID号   :" & TimetableInf(nID).sID & vbCrLf _
                                       & "创建时间:" & TimetableInf(nID).sCreateDate & vbCrLf _
                                       & "修改时间:" & TimetableInf(nID).sEditDate
            End If
        Else
            Me.labInfor.Text = ""
        End If
    End Sub
End Class