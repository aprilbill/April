Imports System.IO

Public Class newTestUpdate
    Public DriverNo As String
    Public DriverName As String
    Public BeLine As String
    Public BeClass As String
    Public BeTeam As String
    Public TechGrade As String
    Public Postion As String
    Public PhoneNum As String
    Public Position As String
    Public Available As String
    Public Reason As String
    Public KM As String
    Public StarLevel As String
    Public Apprentice As String
    Public MARelation As String
    Public bezone As String

    Private Sub UpdateButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateButton.Click
        Me.DriverNo = Me.DriverIDTextBox.Text.Trim
        Me.DriverName = Me.DriverNameTextBox.Text.Trim
        Me.BeLine = Me.TxtBeline.Text.Trim
        Me.BeClass = Me.CmbClass.Text.Trim
        Me.BeTeam = Me.TxtTeamno.Text.Trim
        Me.Available = Me.CmbAvailableOrNot.Text.Trim
        Me.Reason = Me.CmbReasonforAvail.Text.Trim
        Me.TechGrade = Me.CmbTechGrade.Text.Trim
        Me.PhoneNum = Me.TxtPhone.Text.Trim
        Me.KM = Me.TxtKM.Text.Trim
        Me.StarLevel = Me.CmbStarLevel.Text.Trim
        Me.Position = Me.CmbPost.Text.Trim
        Me.Apprentice = Me.CmbApprentice.Text.Trim
        Me.MARelation = Me.TxtMArelation.Text.Trim
        Me.bezone = Me.CBEZONE.Text.Trim
        If Me.DriverNo = "" OrElse Me.DriverName = "" OrElse Me.Position = "" OrElse Me.BeLine = "" OrElse IsNumeric(Me.BeTeam) = False OrElse Me.KM = "" OrElse Me.Apprentice = "" Then
            MsgBox("请将必须填写部分填写完整！", MsgBoxStyle.OkOnly, "提醒")
            Exit Sub
        Else
            Dim Str As String = "UPDATE CS_DRIVERINF SET LINEID='" & Me.BeLine & _
                                                         "',DriverName='" & Me.DriverName & _
                                                         "',beclass='" & Me.BeClass & _
                                                         "',beteam='" & Me.BeTeam & _
                                                         "',techgrade='" & Me.TechGrade & _
                                                         "',post='" & Me.Position & _
                                                         "',phone='" & Me.PhoneNum & _
                                                         "',idleornot='" & Me.Available & _
                                                         "',reasonforunavailable='" & Me.Reason & _
                                                         "',KM='" & Me.KM & _
                                                         "',starlevel='" & Me.StarLevel & _
                                                          "',apprentice='" & Me.Apprentice & _
                                                         "',marelation='" & Me.MARelation & _
                                                         "',bezone='" & Me.bezone & _
                                                          "',enrolltime='" & Me.DateTimePicker1.Value.ToString("yyyy/MM/dd") & _
                                                         "' WHERE RDriverNo='" & Me.DriverNo & "' and lineid='" & CurLineName & "'"

            UpdateData(Str)


            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub CloButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloButton.Click
        Me.Close()
    End Sub
    Private Sub CmbAvailableOrNot_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbAvailableOrNot.SelectedIndexChanged
        If CmbAvailableOrNot.Text.Trim = "是" Then
            CmbReasonforAvail.Enabled = False
        Else
            CmbReasonforAvail.Enabled = True
        End If
    End Sub
    Private Sub CmbApprentice_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles CmbApprentice.SelectedIndexChanged
        If Me.TxtMArelation.Text.Trim <> "" Then
            If MsgBox("该司机原有师徒关系已取消！", MsgBoxStyle.OkOnly, "提醒") = MsgBoxResult.Ok Then
                Me.TxtMArelation.Text = ""
                Dim str As String = "select * from cs_driverinf where rdriverno='" & Me.DriverIDTextBox.Text.Trim & "' and lineid='" & CurLineName & "'"
                Dim tab As DataTable = ReadData(str)
                If tab IsNot Nothing AndAlso tab.Rows.Count = 1 Then
                    Dim row As DataRow = tab.Rows(0)
                    Dim PreA As String = row.Item("marelation").ToString
                    str = "update cs_driverinf set marelation='' where rdriverno='" & PreA & "' and lineid='" & CurLineName & "'"
                    UpdateData(str)
                    str = "update cs_driverinf set marelation='' where rdriverno='" & Me.DriverIDTextBox.Text.Trim & "' and lineid='" & CurLineName & "'"
                    UpdateData(str)
                Else
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
        End If
    End Sub

    Private Sub newTestUpdate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim str As String = "select areaname from cs_areainfo where lineid='" & CurLineName & "'"
        Dim tmpDT As New DataTable
        tmpDT = ReadData(str)
        If IsNothing(tmpDT) = False AndAlso tmpDT.Rows.Count > 0 Then
            For i As Integer = 0 To tmpDT.Rows.Count - 1
                If CBEZONE.Items.Contains(tmpDT.Rows(i).Item(0).ToString.Trim) = False Then
                    CBEZONE.Items.Add(tmpDT.Rows(i).Item(0).ToString.Trim)
                End If
            Next
        End If
    End Sub
End Class