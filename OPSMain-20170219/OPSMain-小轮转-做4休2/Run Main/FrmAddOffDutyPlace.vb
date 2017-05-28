Public Class FrmAddOffDutyPlace

    Private Sub FrmAddOffDutyPlace_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ListBoxshiftPlace_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBoxshiftPlace.DoubleClick
        If ListBoxshiftPlace.SelectedItem Is Nothing Then
            Exit Sub
        Else
            Dim i As Integer
            If ListBox1.Items.Count > 0 Then
                For i = 0 To ListBox1.Items.Count - 1
                    If ListBoxshiftPlace.SelectedItem.ToString = ListBox1.Items(i).ToString Then
                        MsgBox("数据已添加", MsgBoxStyle.OkOnly, "错误提示")
                        Exit Sub
                    End If
                Next
            End If
            ListBox1.Items.Add(ListBoxshiftPlace.SelectedItem.ToString)
        End If
    End Sub

    Private Sub ListBox1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.DoubleClick
        If Me.ListBox1.SelectedItems.Count = 1 Then
            Me.ListBox1.Items.RemoveAt(Me.ListBox1.SelectedIndex)
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call ListBoxshiftPlace_DoubleClick(Nothing, Nothing)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Call ListBox1_DoubleClick(Nothing, Nothing)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Btn_AddStation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_AddStation.Click
        If Me.TxtStation.Text.Trim <> "" Then
            Dim ifstaexist As Boolean = False
            For i As Integer = 0 To Me.ListBoxshiftPlace.Items.Count - 1
                If Me.ListBoxshiftPlace.Items(i).ToString = Me.TxtStation.Text.Trim Then
                    ifstaexist = True
                    Exit For
                End If
            Next
            If ifstaexist Then
                MsgBox("该元素已经存在！", MsgBoxStyle.OkOnly, "提醒")
            Else
                Me.ListBoxshiftPlace.Items.Add(Me.TxtStation.Text.Trim)
            End If
        End If
    End Sub
End Class