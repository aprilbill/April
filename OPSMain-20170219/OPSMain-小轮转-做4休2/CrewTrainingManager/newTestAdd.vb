Imports System.IO
Imports System.Data.OleDb
Imports System
Imports System.Data

Public Class newTestAdd

    Public DriverNo As String
    Public DriverName As String
    Public Gender As String
    Public Minority As String
    Public BirthDay As Date
    Public EnRollTime As Date
    Public BeLine As String
    Public BeClass As String
    Public BeTeam As String
    Public TxtBezone As String
    Public TechGrade As String
    Public Position As String
    Public DrivingExp As String
    Public PhoneNum As String
    Public Available As String
    Public Reason As String
    Public KM As String
    Public StarLevel As String
    Public Apprentice As String
    Public MARelation As String
    Public unicode As String
    Private Sub CloseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddButton.Click
        Me.DriverNo = Me.DriverIDTextBox.Text.Trim
        Me.DriverName = Me.DriverNameTextBox.Text.Trim
        Me.BeClass = Me.CmbClass.Text.Trim
        Me.BeTeam = Me.TxtTeamNo.Text.Trim
        Me.BeLine = Me.CmbLine.Text.Trim
        Me.Position = Me.CmbPost.Text.Trim
        Me.Available = Me.CmbAvailableOrNot.Text.Trim
        Me.Reason = Me.CmbReasonforAvail.Text.Trim
        Me.TechGrade = Me.CmbTechGrade.Text.Trim
        Me.PhoneNum = Me.TxtPhone.Text.Trim
        Me.KM = Me.TxtKM.Text.Trim
        Me.StarLevel = Me.CmbStarLevel.Text.Trim
        Me.Apprentice = Me.CmbApprentice.Text.Trim
        Me.MARelation = Me.TxtMArelation.Text.Trim
        Me.unicode = Me.TxtUnicode.Text.Trim
        Me.TxtBezone = Me.bezone.Text.Trim

        If Me.DriverNo = "" OrElse Me.DriverName = "" OrElse Me.Position = "" OrElse IsNumeric(Me.BeTeam) = False OrElse Me.BeLine = "" OrElse Me.KM = "" OrElse Me.Apprentice = "" OrElse Me.unicode = "" OrElse Me.TxtBezone = "" Then
            MsgBox("请将必须填写部分填写完整！", MsgBoxStyle.OkOnly, "提醒")
            Exit Sub
        Else
            If IFDriverNoExist(Me.BeLine, Me.DriverNo) Then
                MsgBox("该司机工号已存在，请更换司机编号！", MsgBoxStyle.OkOnly, "提醒")
                Exit Sub
            End If
            Me.DialogResult = Windows.Forms.DialogResult.OK
            Dim Str As String = "INSERT INTO CS_DRIVERINF " _
                                    & "(lineid,beclass,beteam,rdriverno,drivername,bezone,enrolltime,techgrade,post,phone,idleornot,reasonforunavailable,KM,starlevel,apprentice,marelation,unicode)" _
                                    & "VALUES(" _
                                    & "'" & Me.BeLine _
                                    & "','" & Me.BeClass _
                                    & "','" & Me.BeTeam _
                                    & "','" & Me.DriverNo _
                                    & "','" & Me.DriverName _
                                    & "','" & Me.TxtBezone _
                                     & "','" & DateTimePicker1.Value.ToString("yyyy/MM/dd").Trim _
                                    & "','" & Me.TechGrade _
                                    & "','" & Me.Position _
                                    & "','" & Me.PhoneNum _
                                    & "','" & Me.Available _
                                    & "','" & Me.Reason _
                                    & "','" & Me.KM _
                                    & "','" & Me.StarLevel _
                                    & "','" & Me.Apprentice _
                                    & "','" & Me.MARelation _
                                    & "','" & Me.unicode _
                                    & "')"



            UpdateData(Str)
            Me.Close()
        End If
    End Sub

    Private Sub newTestAdd_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.CmbLine.Items.Add(CurLineName)
        Me.CmbLine.Text = CurLineName
        Dim str As String = "select areaname from cs_areainfo where lineid='" & CurLineName & "'"
        Dim tmpDT As New DataTable
        tmpDT = ReadData(str)
        If IsNothing(tmpDT) = False AndAlso tmpDT.Rows.Count > 0 Then
            For i As Integer = 0 To tmpDT.Rows.Count - 1
                If bezone.Items.Contains(tmpDT.Rows(i).Item(0).ToString.Trim) = False Then
                    bezone.Items.Add(tmpDT.Rows(i).Item(0).ToString.Trim)
                End If
            Next
        End If
        DateTimePicker1.Value = Now
    End Sub

    Public Function IFDriverNoExist(ByVal lineid As String, ByVal DriverNo As String) As Boolean
        IFDriverNoExist = False
        Dim str As String = "select * from cs_driverinf where lineid='" & lineid & "' and rdriverno='" & DriverNo & "'"
        Dim tab As DataTable = ReadData(str)
        If tab IsNot Nothing AndAlso tab.Rows.Count > 0 Then
            IFDriverNoExist = True
        End If
        tab.Dispose()
    End Function

   

    Private Sub CmbAvailableOrNot_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        If CmbAvailableOrNot.Text.Trim = "否" Then
            CmbReasonforAvail.Enabled = True
        End If

    End Sub

  
    Private Sub CmbAvailableOrNot_SelectedIndexChanged_1(sender As System.Object, e As System.EventArgs) Handles CmbAvailableOrNot.SelectedIndexChanged
        If CmbAvailableOrNot.Text.Trim = "是" Then
            CmbReasonforAvail.Enabled = False
        Else
            CmbReasonforAvail.Enabled = True
        End If
    End Sub

    Private Sub TxtMArelation_TextChanged(sender As System.Object, e As System.EventArgs) Handles TxtMArelation.TextChanged
        Dim str As String = "select * from cs_driverinf where rdriverno='" & Me.TxtMArelation.Text.Trim & "' and lineid='" & CurLineName & "'"
        Dim tab As DataTable = ReadData(str)
        If tab IsNot Nothing AndAlso tab.Rows.Count = 1 Then
            Dim row As DataRow = tab.Rows(0)
            Dim PreA As String = row.Item("marelation").ToString
            If PreA <> "" Then
                If MsgBox("该司机已经已有师徒关系，与之建立师徒关系，之前的师徒关系将取消！是否确认？", MsgBoxStyle.OkCancel, "提醒") = MsgBoxResult.Ok Then
                    str = "update cs_driverinf set marelation='' where rdriverno='" & PreA & "' and lineid='" & CurLineName & "'"
                    UpdateData(str)
                    str = "update cs_driverinf set marelation='" & Me.DriverIDTextBox.Text.Trim & "' where rdriverno='" & Me.TxtMArelation.Text.Trim & "' and lineid='" & CurLineName & "'"
                    UpdateData(str)
                    str = "update cs_driverinf set marelation='" & Me.TxtMArelation.Text.Trim & "' where rdriverno='" & Me.DriverIDTextBox.Text.Trim & "' and lineid='" & CurLineName & "'"
                    UpdateData(str)
                Else
                    Exit Sub
                End If
            End If
        Else
            MsgBox("该工号不存在，请查证后再输入", MsgBoxStyle.OkOnly, "提醒")
        End If
    End Sub

End Class