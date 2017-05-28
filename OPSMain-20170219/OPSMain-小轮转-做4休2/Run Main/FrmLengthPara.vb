Imports System.IO

Public Class FrmLengthPara
    Public Sub New()
        ' 此调用是 Windows 窗体设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。

        Me.ComboBox1.Items.Add(CurLineName)
        Me.ComboBox1.SelectedIndex = 0

    End Sub

    Private Sub Addbtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Addbtn.Click
        If Me.StartPosLB.SelectedIndex = -1 Or Me.EndPosLB.SelectedIndex = -1 Then
            MessageBox.Show("请选择车站！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If Me.StartPosLB.SelectedIndex = Me.EndPosLB.SelectedIndex Then
            MessageBox.Show("起始站与到达站不能相同！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        ElseIf Me.StartPosLB.SelectedIndex < Me.EndPosLB.SelectedIndex Then
            Dim tempstr As String
            tempstr = Me.StartPosLB.SelectedItem.ToString & "=>" & Me.EndPosLB.SelectedItem.ToString

            If Me.Downtrainlist.Rows.Count > 1 Then
                Dim boo As Boolean = False
                For i As Integer = 0 To Me.Downtrainlist.Rows.Count - 2
                    If Me.Downtrainlist.Rows(i).Cells(0).Value.ToString = tempstr Then
                        boo = True
                    End If
                Next
                If boo = True Then
                    MessageBox.Show("该区段已经添加！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End If

            Me.Downtrainlist.Rows.Add(tempstr)

        Else
            Dim tempstr As String
            tempstr = Me.StartPosLB.SelectedItem.ToString & "=>" & Me.EndPosLB.SelectedItem.ToString
            If Me.Uptrainlist.Rows.Count > 1 Then
                Dim boo As Boolean = False
                For i As Integer = 0 To Me.Uptrainlist.Rows.Count - 2
                    If Me.Uptrainlist.Rows(i).Cells(0).Value.ToString = tempstr Then
                        boo = True
                    End If
                Next
                If boo = True Then
                    MessageBox.Show("该区段已经添加！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End If
            Me.Uptrainlist.Rows.Add(tempstr)
        End If
    End Sub

    Private Sub AddOpbtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddOpbtn.Click
        If Me.Uptrainlist.SelectedRows.Count = 0 And Me.Downtrainlist.SelectedRows.Count = 0 Then
            MessageBox.Show("没有选中添加项！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Try
            If Me.Uptrainlist.SelectedRows.Count > 0 Then
                For i As Integer = 0 To Uptrainlist.SelectedRows.Count - 1
                    Dim tempstr As String = Split(Uptrainlist.SelectedRows(i).Cells(0).Value.ToString, ">")(1) & "=>" & Split(Uptrainlist.SelectedRows(i).Cells(0).Value.ToString, "=")(0)

                    If Me.Downtrainlist.Rows.Count > 1 Then
                        Dim boo As Boolean = False
                        For j As Integer = 0 To Me.Downtrainlist.Rows.Count - 2
                            If Me.Downtrainlist.Rows(i).Cells(0).Value.ToString = tempstr Then
                                boo = True
                            End If
                        Next
                        If boo = True Then
                            MessageBox.Show("该区段已经添加！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If
                    End If

                    Dim tempstr2 As String
                    If Uptrainlist.SelectedRows(i).Cells(1).Value = Nothing Then
                        tempstr2 = ""
                    Else
                        tempstr2 = Uptrainlist.SelectedRows(i).Cells(1).Value.ToString
                    End If
                    Me.Downtrainlist.Rows.Add(tempstr, tempstr2)
                Next
            End If

            If Me.Downtrainlist.SelectedRows.Count > 0 Then
                For i As Integer = 0 To Me.Downtrainlist.SelectedRows.Count - 1
                    Dim tempstr As String = Split(Downtrainlist.SelectedRows(i).Cells(0).Value.ToString, ">")(1) & "=>" & Split(Downtrainlist.SelectedRows(i).Cells(0).Value.ToString, "=")(0)
                    If Me.Uptrainlist.Rows.Count > 1 Then
                        Dim boo As Boolean = False
                        For j As Integer = 0 To Me.Uptrainlist.Rows.Count - 2
                            If Me.Uptrainlist.Rows(i).Cells(0).Value.ToString = tempstr Then
                                boo = True
                            End If
                        Next
                        If boo = True Then
                            MessageBox.Show("该区段已经添加！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Exit Sub
                        End If
                    End If

                    Dim tempstr2 As String
                    If Downtrainlist.SelectedRows(i).Cells(1).Value = Nothing Then
                        tempstr2 = ""
                    Else
                        tempstr2 = Downtrainlist.SelectedRows(i).Cells(1).Value.ToString
                    End If
                    Me.Uptrainlist.Rows.Add(tempstr, tempstr2)
                Next
            End If
        Catch ex As Exception
            MsgBox("请正确选择添加项！")
            Exit Sub
        End Try


    End Sub

    Private Sub DeleteBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteBtn.Click
        If Me.Uptrainlist.SelectedRows.Count = 0 And Me.Downtrainlist.SelectedRows.Count = 0 Then
            MessageBox.Show("没有选中添加项！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Try
            If Me.Uptrainlist.SelectedRows.Count > 0 Then
                Dim count As Integer = Me.Uptrainlist.SelectedRows.Count
                For i As Integer = 0 To count - 1
                    Me.Uptrainlist.Rows.RemoveAt(Me.Uptrainlist.SelectedRows(count - 1 - i).Index)
                Next
            End If

            If Me.Downtrainlist.SelectedRows.Count > 0 Then
                Dim count As Integer = Me.Downtrainlist.SelectedRows.Count
                For i As Integer = 0 To count - 1
                    Me.Downtrainlist.Rows.RemoveAt(Me.Downtrainlist.SelectedRows(count - 1 - i).Index)
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Exitbtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Exitbtn.Click
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        '待修改
        For Each LineName As String In Stalist.Keys
            For i As Integer = 0 To Stalist(LineName).Count - 1
                If StartPosLB.Items.Contains(Stalist(LineName)(i)) = False Then
                    StartPosLB.Items.Add(Stalist(LineName)(i))
                    EndPosLB.Items.Add(Stalist(LineName)(i))
                End If
            Next
        Next
        Me.Uptrainlist.Rows.Clear()
        Me.Downtrainlist.Rows.Clear()
        Try
            Dim str3 As String = "select lineid,updown,startsta,endsta,length from CS_SH_TRAINLENGTH where lineid='" & Me.ComboBox1.Text.ToString.Trim & "'"
            Dim table As New DataTable
            table = Globle.Method.ReadDataForAccess(str3)

            For i As Integer = 0 To table.Rows.Count - 1
                Dim tempstr1, tempstr2 As String
                If table.Rows(i).Item("updown") = "上行" Then
                    tempstr1 = table.Rows(i).Item("startsta").ToString & "=>" & table.Rows(i).Item("endsta").ToString
                    tempstr2 = table.Rows(i).Item("length").ToString
                    Me.Uptrainlist.Rows.Add(tempstr1, tempstr2)
                Else
                    tempstr1 = table.Rows(i).Item("startsta").ToString & "=>" & table.Rows(i).Item("endsta").ToString
                    tempstr2 = table.Rows(i).Item("length").ToString
                    Me.Downtrainlist.Rows.Add(tempstr1, tempstr2)
                End If
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub SaveBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveBtn.Click
        Dim str As String = "delete from CS_SH_TRAINLENGTH where LINEID='" & Me.ComboBox1.Text.ToString.Trim & "'"
        Globle.Method.UpdateDataForAccess(str)

        Try
            For i As Integer = 0 To Me.Uptrainlist.Rows.Count - 1
                str = "INSERT INTO CS_SH_TRAINLENGTH" _
                       & "(LINEID,STARTSTA,ENDSTA,UPDOWN,LENGTH)" _
                       & "VALUES('" & Me.ComboBox1.Text.ToString.Trim & _
                       "','" & Split(Me.Uptrainlist.Rows(i).Cells(0).Value.ToString.Trim, "=")(0) & _
                       "','" & Split(Me.Uptrainlist.Rows(i).Cells(0).Value.ToString.Trim, ">")(1) & _
                       "','" & "上行" & _
                       "'," & CDec(Me.Uptrainlist.Rows(i).Cells(1).Value.ToString.Trim) & _
                       ")"
                Globle.Method.UpdateDataForAccess(str)
            Next

            For i As Integer = 0 To Me.Downtrainlist.Rows.Count - 1
                str = "INSERT INTO CS_SH_TRAINLENGTH" _
                       & "(LINEID,STARTSTA,ENDSTA,UPDOWN,LENGTH)" _
                       & "VALUES('" & Me.ComboBox1.Text.ToString.Trim & _
                       "','" & Split(Me.Downtrainlist.Rows(i).Cells(0).Value.ToString.Trim, "=")(0) & _
                       "','" & Split(Me.Downtrainlist.Rows(i).Cells(0).Value.ToString.Trim, ">")(1) & _
                       "','" & "下行" & _
                       "'," & CDec(Me.Downtrainlist.Rows(i).Cells(1).Value.ToString.Trim) & _
                       ")"
                Globle.Method.UpdateDataForAccess(str)
            Next
        Catch ex As Exception
            MsgBox("请正确填写长度数据！", MsgBoxStyle.OkOnly, "提醒")
            Exit Sub
        End Try

        MessageBox.Show("保存成功！")
    End Sub

    Private Sub Btn_AddStation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_AddStation.Click
        If Me.TxtStation.Text.Trim <> "" Then
            Dim ifstaexist As Boolean = False
            For i As Integer = 0 To Me.StartPosLB.Items.Count - 1
                If Me.StartPosLB.Items(i).ToString = Me.TxtStation.Text.Trim Then
                    ifstaexist = True
                    Exit For
                End If
            Next
            If ifstaexist Then
                MsgBox("该车站已经存在！", MsgBoxStyle.OkOnly, "提醒")
            Else
                Me.StartPosLB.Items.Add(Me.TxtStation.Text.Trim)
                Me.EndPosLB.Items.Add(Me.TxtStation.Text.Trim)
            End If
        End If
    End Sub

End Class