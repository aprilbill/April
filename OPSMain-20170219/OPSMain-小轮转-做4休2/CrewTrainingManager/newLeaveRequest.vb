Imports Pabo.Calendar
Imports System.Data.OleDb

Public Class newLeaveRequest

    Dim para As Integer = 0
    Private Sub newLeaveRequest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ToolStripStatusLabel6.Text = "浏览"
    End Sub

    Private Sub NewMonthCalendar_DayClick(ByVal sender As Object, ByVal e As Pabo.Calendar.DayClickEventArgs)
        'MsgBox(CStr(Me.NewMonthCalendar.MaxDate))
    End Sub
    Private Sub NewMonthCalendar_MonthChanged(ByVal sender As System.Object, ByVal e As Pabo.Calendar.MonthChangedEventArgs)
    End Sub

   
    Private Sub LeaveTreeView_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles LeaveTreeView.AfterSelect
        'Me.ToolStripStatusLabel1.Text = "当前是第" & Me.LeaveTreeView.SelectedNode.Text.ToString & "条记录"

        Dim MarkNum As String = Me.DriverIDTextBox.Text.ToString
        Dim MarkLine As String = Me.LineTextBox.Text.ToString
        Dim Adapter As New OleDbDataAdapter
        If Me.LeaveTreeView.SelectedNode.Level = 1 Then
            Dim str As String = "SELECT * FROM CS_vacINF WHERE RDRIVERNO ='" & CStr(MarkNum) & "' AND vacstart ='" & Me.LeaveTreeView.SelectedNode.Text.ToString & "' AND LINEID ='" & CStr(MarkLine) & "'"
            Dim tempTable As New DataTable
            tempTable = ReadData(str)
            Dim myrow As DataRow = tempTable.Rows(0)
            Me.VacTypeComboBox.Text = myrow.Item("VACTYPE").ToString
            Me.VacStart.Text = myrow.Item("VACSTART").ToString
            Me.VacEnd.Text = myrow.Item("VACEND").ToString
            Me.DetailTextBox.Text = myrow.Item("DETAIL").ToString
            Me.ISVAC.Text = myrow.Item("isvac").ToString
            tempTable.Dispose()
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Me.VacTypeComboBox.Text = ""
        Me.VacStart.Text = ""
        Me.VacEnd.Text = ""
        Me.DetailTextBox.Text = ""

        para = 1
      
        Me.ToolStripStatusLabel6.Text = "添加"

    End Sub

 
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfirmButton.Click
        Select Case para
            Case 0
            Case 1
                Dim tempVacType, tempVacStart, tempVacEnd, tempDetail, tempISVAC As String
                Dim tempSpan As Integer
                Dim tempVacID As String = "1"

                tempISVAC = Me.ISVAC.Text
                tempVacType = Me.VacTypeComboBox.Text
                tempVacStart = Me.VacStart.Value.ToString("yyyy-MM-dd")
                tempVacEnd = Me.VacEnd.Value.ToString("yyyy-MM-dd")
                tempDetail = VacTypeComboBox.Text.ToString
                tempSpan = DateDiff(DateInterval.Day, CDate(tempVacStart), CDate(tempVacEnd)) + 1
                If Me.LeaveTreeView.Nodes.Count <> 0 Then
                    tempVacID = CStr(Me.LeaveTreeView.Nodes.Count + 3)
                    'tempVacID = CStr(CInt(Me.LeaveTreeView.Nodes(Me.LeaveTreeView.Nodes.Count - 1).Text) + 1)
                End If
                If tempVacType = "" Then
                    MsgBox("请选择事由")
                    Exit Sub
                End If
                If tempISVAC = "" Then
                    MsgBox("请选择类型")
                    Exit Sub
                End If

                Dim temp1str As String = "INSERT INTO CS_vacINF " _
                                    & "(LINEID,rDriverNo,isvac,vactype,vacstart,vacend,span,detail,vacatdid)" _
                                    & "VALUES(" _
                                    & "'" & CStr(Me.LineTextBox.Text) _
                                    & "','" & CStr(Me.DriverIDTextBox.Text) _
                                     & "','" & CStr(tempISVAC) _
                                    & "','" & CStr(tempVacType) _
                                    & "','" & CStr(tempVacStart) _
                                    & "','" & CStr(tempVacEnd) _
                                    & "','" & CStr(tempSpan) _
                                    & "','" & CStr(tempDetail) _
                                    & "','" & CStr(tempVacID) _
                                    & "')"
                UpdateData(temp1str)
                MsgBox("添加已成功")


            Case 2
                Dim tempVacType, tempVacStart, tempVacEnd, tempDetail, tempisvac As String
                Dim tempSpan As Integer
                Dim tempVacID As String = "1"

                tempisvac = ISVAC.Text
                tempVacType = VacTypeComboBox.Text
                tempVacStart = VacStart.Value.ToString("yyyy-MM-dd")
                tempVacEnd = VacEnd.Value.ToString("yyyy-MM-dd")
                tempDetail = Me.DetailTextBox.Text.ToString
                tempSpan = DateDiff(DateInterval.Day, CDate(tempVacStart), CDate(tempVacEnd)) + 1
                If Me.LeaveTreeView.Nodes.Count <> 0 Then
                    tempVacID = Me.LeaveTreeView.Nodes.Count + 3
                End If
                If tempVacType = "" Then
                    MsgBox("请输入请假类型")
                    Exit Sub
                End If

                Dim temp2Str As String = "UPDATE CS_vacINF SET RDriverNo='" & CStr(Me.DriverIDTextBox.Text) & _
                                                             "',LINEID='" & CStr(Me.LineTextBox.Text) & _
                                                             "',vacatdid='" & CStr(Me.LeaveTreeView.SelectedNode.Text) & _
                                                             "',vacstart='" & CStr(tempVacStart) & _
                                                             "',vacend='" & CStr(tempVacEnd) & _
                                                             "',isvac='" & CStr(tempisvac) & _
                                                             "',vactype='" & CStr(tempVacType) & _
                                                             "',span='" & CStr(tempSpan) & _
                                                             "',detail='" & CStr(tempDetail) & _
                                                             "' WHERE RDriverNo='" & CStr(Me.DriverIDTextBox.Text) & "' AND vacstart ='" & Me.LeaveTreeView.SelectedNode.Text.ToString & "' AND LINEID='" & CStr(Me.LineTextBox.Text) & "'"
                UpdateData(temp2Str)
                MsgBox("更新已成功！")



            Case 3

                If Me.LeaveTreeView.SelectedNode.Text <> Nothing Then


                    Dim temp3Str As String = "DELETE FROM CS_vacINF WHERE RDriverNo='" & CStr(Me.DriverIDTextBox.Text) & "' AND vacstart ='" & Me.LeaveTreeView.SelectedNode.Text.ToString & "' AND LINEID='" & CStr(Me.LineTextBox.Text) & "'"
                    UpdateData(temp3Str)
                    MsgBox("删除已成功！")


                End If
                Me.VacTypeComboBox.Text = ""
                Me.VacStart.Text = ""
                Me.VacEnd.Text = ""
                Me.DetailTextBox.Text = ""
        End Select

        If para <> 0 Then
            Me.LeaveTreeView.Nodes.Clear()
            Me.NewMonthCalendar.Dates.Clear()
            Dim ODADrinfo As New OleDbDataAdapter
            Dim cmd As New OleDbCommand
            Dim Dtable As New DataTable
            Dim MarkLineID As String = Me.LineTextBox.Text
            Dim MarkNum As String = Me.DriverIDTextBox.Text
            Dim markname As String = Me.DriverNameTextBox.Text


            Dim Str2 As String = "Select * from cs_vacinf WHERE RDRIVERNO ='" & CStr(MarkNum) & "' AND LINEID='" & CStr(MarkLineID) & "'order by vacatdid"
           
            Dim tab As New DataTable
            tab = ReadData(Str2)
            Dim node1, node2 As TreeNode
            node1 = Me.LeaveTreeView.Nodes.Add("请假")
            node2 = Me.LeaveTreeView.Nodes.Add("培训")

            For i As Integer = 0 To tab.Rows.Count - 1
                If tab.Rows(i).Item("isvac") = "请假" Then
                    node1.Nodes.Add(CStr(tab.Rows(i).Item("vacstart")))
                ElseIf tab.Rows(i).Item("isvac") = "培训" Then
                    node2.Nodes.Add(CStr(tab.Rows(i).Item("vacstart")))
                End If
            Next

            Me.NewMonthCalendar.TodayColor = Color.Red

            For i As Integer = 0 To tab.Rows.Count - 1
                Dim LeaveSpan As Integer = CInt(tab.Rows(i).Item("span"))
                Dim dateitems(LeaveSpan - 1) As DateItem
                Dim strColor As Color
                Dim strText As String = Nothing
                Select Case tab.Rows(i).Item("vactype")
                    Case "病假"
                        strColor = Color.GreenYellow
                        strText = "病假"
                    Case "事假"
                        strColor = Color.Gold
                        strText = "事假"
                    Case "安全"
                        strColor = Color.Blue
                        strText = "安全"
                    Case "技能"
                        strColor = Color.Fuchsia
                        strText = "技能"
                End Select
                For j As Integer = 0 To LeaveSpan - 1
                    dateitems(j) = New DateItem()
                    dateitems(j).Date = CDate(tab.Rows(i).Item("vacstart").ToString).AddDays(j)
                    dateitems(j).BackColor1 = strColor
                    dateitems(j).Text = strText
                Next
                Me.NewMonthCalendar.Dates.AddRange(dateitems)
            Next



          
            tab.Dispose()
        End If
        para = 0
        Me.ToolStripStatusLabel6.Text = "浏览"

    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        para = 2
        Me.ToolStripStatusLabel6.Text = "修改"
    End Sub

    Private Sub ToolStripButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton3.Click
        para = 3
        Me.ToolStripStatusLabel6.Text = "删除"
    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        para = 0
    End Sub

    Private Sub ToolStripStatusLabel6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel6.Click

    End Sub

    Private Sub ToolStripStatusLabel6_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel6.TextChanged
        If Me.ToolStripStatusLabel6.Text = "浏览" Then
            Me.VacTypeComboBox.Enabled = False
            Me.VacStart.Enabled = False
            Me.VacEnd.Enabled = False
            Me.DetailTextBox.Enabled = False
            Me.ISVAC.Enabled = False
        Else
            Me.ISVAC.Enabled = True
            Me.VacTypeComboBox.Enabled = True
            Me.VacStart.Enabled = True
            Me.VacEnd.Enabled = True
            Me.DetailTextBox.Enabled = True
        End If

    End Sub

    
   
    Private Sub ISVAC_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ISVAC.SelectedIndexChanged
        If ISVAC.Text = "请假" Then
            Me.VacTypeComboBox.Items.Clear()
            Me.VacTypeComboBox.Items.Add("事假")
            Me.VacTypeComboBox.Items.Add("病假")
            Me.VacTypeComboBox.Items.Add("产假")
            Me.VacTypeComboBox.Items.Add("年休")
            Me.VacTypeComboBox.Items.Add("婚假")
            Me.VacTypeComboBox.Items.Add("其他")
        ElseIf ISVAC.Text = "培训" Then
            Me.VacTypeComboBox.Items.Clear()
            Me.VacTypeComboBox.Items.Add("安全")
            Me.VacTypeComboBox.Items.Add("技能")
        End If
    End Sub
End Class