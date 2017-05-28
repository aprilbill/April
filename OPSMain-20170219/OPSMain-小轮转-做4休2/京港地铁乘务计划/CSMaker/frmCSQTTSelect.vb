
Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class frmCSQTTSelect
    Public IsOpen As Boolean = False
    Private Sub frmCSQTTSelect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

            strCurlineID = Me.ComlineInf.Text.ToString.Trim

            Call InputCSTimetableInf(strCurlineID) '改完
            If CSTimetableInf Is Nothing = False Then
                Me.lstName.Items.Clear()
                For i = 1 To UBound(CSTimetableInf)
                    Me.lstName.Items.Add(CSTimetableInf(i).sName)
                Next i
            Else
                MsgBox("当前还没有乘务计划，请先制定乘务计划！", MsgBoxStyle.OkOnly)
                Exit Sub
            End If
        Else
            Me.ComlineInf.Items.Add(CurLineName)
            Me.ComlineInf.Text = CurLineName
        End If
       
    End Sub

    Private Sub lstName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstName.SelectedIndexChanged
        If Me.lstName.SelectedIndex >= 0 Then
            Dim nID As Integer
            nID = GetCSTimetableInfID(Me.lstName.Items(Me.lstName.SelectedIndex))
            If nID > 0 Then
                Me.LabInfor.Text = "ID号   :" & CSTimetableInf(nID).sID & vbCrLf _
                                       & "创建时间:" & CSTimetableInf(nID).sCreateDate & vbCrLf _
                                       & "修改时间:" & CSTimetableInf(nID).sEditDate
            End If
        Else
            Me.LabInfor.Text = ""
        End If
    End Sub

    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        If Me.lstName.SelectedIndex >= 0 Then
            Me.IsOpen = True
            Dim tempID As String = ""
            Dim tempIndex As Integer = -1
            Dim tempName As String = Me.lstName.SelectedItem.ToString.Trim
            Dim i As Integer
            For i = 1 To UBound(CSTimetableInf)
                If CSTimetableInf(i).sName = tempName Then
                    tempID = CSTimetableInf(i).sID
                    tempIndex = i
                    Exit For
                End If
            Next
            If tempID <> "" Then
                CSTrainsAndDrivers = New CSTrainAndDrivers()
                '查询
                strQCurCSPlanName = tempName '当前乘务计划表名
                strQCurCSPlanID = GetCSPlanIDFromCSPlanName(strQCurCSPlanName)
                'strCurCS_CSTableID = tempID '当前乘务计划表ID
                DiagramCurID = GetCSDIAGRAMIDFromCSPlanName(tempName)
                CSTrainsAndDrivers.ScheduleState = CSTimetableInf(tempIndex).ScheduleState
                CSTrainsAndDrivers.IfCorSchedule = CSTimetableInf(tempIndex).IFCorShcedule
                CSTrainsAndDrivers.CorCSTimetableID = CSTimetableInf(tempIndex).CorTimetableID
                CSTrainsAndDrivers.CorCSPlanName = GetCSPlanNameFromCSPlanID(CSTrainsAndDrivers.CorCSTimetableID)

                Call CSInitSystemVariant()
                Call CSreadNetStaAndSecData(DiagramCurID)
                Call CSInputChediAndTrainJianGeData("", DiagramCurID)
                Call CSReadBaseTrainInf(DiagramCurID)
                Call CSReadTrainAndTimeTableInf(DiagramCurID, Me.ProgressBar1)
                Me.Close()

                sState = "乘务计划查询与调整"
            Else
                MsgBox("表名错误", MsgBoxStyle.OkOnly)
            End If

        Else
            MsgBox("请选择乘务计划表", MsgBoxStyle.OkOnly)
        End If


    End Sub

    Private Sub lstName_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstName.MouseDoubleClick
        Call cmdOk_Click(Nothing, Nothing)
    End Sub

    Private Sub cmbCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCancel.Click
        Me.Close()
    End Sub

    Private Sub Comline_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComlineInf.SelectedIndexChanged
        strCurlineID = Me.ComlineInf.Text.ToString.Trim
        Call InputCSTimetableInf(strCurlineID) '改完
        If CSTimetableInf Is Nothing = False Then
            Dim i As Integer
            Me.lstName.Items.Clear()
            For i = 1 To UBound(CSTimetableInf)
                Me.lstName.Items.Add(CSTimetableInf(i).sName)
            Next i
        Else
            MsgBox("当前还没有乘务计划，请先制定乘务计划！", MsgBoxStyle.OkOnly)
            Exit Sub
        End If
    End Sub

End Class