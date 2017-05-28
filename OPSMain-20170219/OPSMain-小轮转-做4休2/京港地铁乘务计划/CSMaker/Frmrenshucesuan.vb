Public Class Frmrenshucesuan

    Private Sub Frmrenshucesuan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DataGridView1.Rows.Add("早高峰", MChediNum, 0, 0, 0)
        Me.DataGridView1.Rows.Add("平峰", NChediNum, 0, 0, 0)
        Me.DataGridView1.Rows.Add("晚高峰", AChediNum, 0, 0, 0)
    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        If Me.DataGridView1.Rows.Count = 0 Then Exit Sub
        For Each r As DataGridViewRow In Me.DataGridView1.Rows
            If IsNothing(r.Cells("车底数").Value) = True OrElse r.Cells("车底数").Value.ToString.Trim = "" Then
                r.Cells("车底数").Value = 0
            End If
            If IsNothing(r.Cells("折返司机数量").Value) = True OrElse r.Cells("折返司机数量").Value.ToString.Trim = "" Then
                r.Cells("折返司机数量").Value = 0
            End If
            If IsNothing(r.Cells("配检司机数量").Value) = True OrElse r.Cells("配检司机数量").Value.ToString.Trim = "" Then
                r.Cells("配检司机数量").Value = 0
            End If
            If IsNothing(r.Cells("机动司机数量").Value) = True OrElse r.Cells("机动司机数量").Value.ToString.Trim = "" Then
                r.Cells("机动司机数量").Value = 0
            End If
            r.Cells("合计").Value = CInt(r.Cells("车底数").Value.ToString.Trim) + CInt(r.Cells("折返司机数量").Value.ToString.Trim) + CInt(r.Cells("配检司机数量").Value.ToString.Trim) + CInt(r.Cells("机动司机数量").Value.ToString.Trim)
        Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim MinNum, maxNum As Integer
        MinNum = 100000
        maxNum = 0
        Dim tmpMnum, tmpNnum, tmpAnum As Integer
        For Each r As DataGridViewRow In Me.DataGridView1.Rows
            If maxNum < r.Cells("合计").Value Then
                maxNum = r.Cells("合计").Value
            End If
            If MinNum > r.Cells("合计").Value Then
                MinNum = r.Cells("合计").Value
            End If
            If r.Cells("时段").Value = "早高峰" Then
                tmpMnum = r.Cells("合计").Value
            ElseIf r.Cells("时段").Value = "平峰" Then
                tmpNnum = r.Cells("合计").Value
            ElseIf r.Cells("时段").Value = "晚高峰" Then
                tmpAnum = r.Cells("合计").Value
            End If
        Next

        For intChangshi As Integer = MinNum To maxNum

            Dim intMchazhi, intNchazhi, intAchazhi As Integer
            If tmpMnum > intChangshi Then
                intMchazhi = tmpMnum - intChangshi
            Else
                intMchazhi = 0
            End If

            If tmpNnum > intChangshi Then
                intNchazhi = tmpNnum - intChangshi
            Else
                intNchazhi = 0
            End If

            If tmpAnum > intChangshi Then
                intAchazhi = tmpAnum - intChangshi
            Else
                intAchazhi = 0
            End If

            If 6 * (intChangshi - intMchazhi - intNchazhi - intAchazhi) >= 2 * intChangshi Then
                Me.TxtCesuan.Text = intChangshi
                Me.TxtZaofeng.Text = intMchazhi
                Me.TxtPingfeng.Text = intNchazhi
                Me.TxtWanfeng.Text = intAchazhi
                Me.Txtchazhi.Text = intChangshi - intMchazhi - intNchazhi - intAchazhi
                Me.TxtZongshu.Text = intChangshi * 5
                Exit For
            End If
        Next

    End Sub
End Class