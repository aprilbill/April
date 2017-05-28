Public Class FrmComplexSeq

    Public SeqStr As String = ""

    Private Sub FrmComplexSeq_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim str As String = "select distinct t.beclass from cs_driverinf t order by t.beclass"
        Dim tab As DataTable = ReadData(str)
        Me.CmbBeClass.Items.Clear()
        For Each row As DataRow In tab.Rows
            Me.CmbBeClass.Items.Add(row.Item("beclass").ToString.Trim)
        Next
        str = "select distinct t.post from cs_driverinf t order by t.post"
        tab = ReadData(str)
        Me.CmbPost.Items.Clear()
        For Each row As DataRow In tab.Rows
            Me.CmbPost.Items.Add(row.Item("post").ToString.Trim)
        Next
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        SeqStr = "select * from cs_driverinf where 1=1 "
        If Me.txtDriverNO.Text.Trim <> "" Then
            SeqStr &= "and rdriverno='" & Me.txtDriverNO.Text.Trim & "' "
        End If
        If Me.txtName.Text.Trim <> "" Then
            SeqStr &= "and drivername='" & Me.txtName.Text.Trim & "' "
        End If
        If Me.txtMaxLength.Text.Trim <> "" Then
            Dim maxlength As Decimal = CDec(Me.txtMaxLength.Text.Trim)
            SeqStr &= "and KM<=" & maxlength & " "
        End If
        If Me.txtMinlength.Text.Trim <> "" Then
            Dim minlength As Decimal = CDec(Me.txtMinlength.Text.Trim)
            SeqStr &= "and KM>" & minlength & " "
        End If
        If Me.CmbBeClass.Text.Trim <> "" Then
            SeqStr &= "and beclass='" & Me.CmbBeClass.Text.Trim & "' "
        End If
        If Me.CmbPost.Text.Trim <> "" Then
            SeqStr &= "and post='" & Me.CmbPost.Text.Trim & "' "
        End If
        SeqStr &= "order by lineid,beclass,rdriverno"
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SeqStr = ""
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
End Class