Imports System.IO
Imports System.Data.OleDb


Public Class M_A_Relation
    Public AName As String
    Public ANO As String
    Public ATel As String
    Public AToM As String
    Public MName As String
    Public MNO As String
    Public MTel As String
    Public MToA As String
    Public i As Integer = 0
    Public APreM As String
    Public PreA As String



  

    Private Sub BtnOK_Click(sender As System.Object, e As System.EventArgs) Handles BtnOK.Click
        Me.AName = Me.TxtAName.Text.Trim
        Me.ANO = Me.TxtANO.Text.Trim
        Me.ATel = Me.TxtATel.Text.Trim
        Me.MName = Me.TxtMName.Text.Trim
        Me.MNO = Me.TxtMNO.Text.Trim
        Me.MTel = Me.TxtMTel.Text.Trim
        Me.APreM = Me.TxtAPreM.Text.Trim
        Me.PreA = Me.TxtPreA.Text.Trim
        If Me.MNO.Trim <> "" AndAlso Me.ANO.Trim <> "" AndAlso Me.APreM <> Me.MNO Then
            If MsgBox("确定信息输入无误，建立师徒关系？", MsgBoxStyle.OkCancel, "提醒") = MsgBoxResult.Ok Then
                'Me.DialogResult = Windows.Forms.DialogResult.OK
                Dim str As String = "update cs_driverinf set marelation='" & Me.MNO & "' where rdriverno='" & Me.ANO & "'"
                UpdateData(str)
                Dim str1 As String = "update cs_driverinf set marelation='" & Me.ANO & "' where rdriverno='" & Me.MNO & "'"
                UpdateData(str1)
                Dim str2 As String = "update cs_driverinf set marelation='' where rdriverno='" & Me.TxtAPreM.Text.Trim & "'"
                UpdateData(str2)
                Dim str3 As String = "update cs_driverinf set marelation='' where rdriverno='" & Me.TxtPreA.Text.Trim & "'"
                UpdateData(str3)
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                Exit Sub
            End If

        ElseIf Me.MNO.Trim <> "" AndAlso Me.ANO.Trim <> "" AndAlso Me.APreM = Me.MNO Then
            If MsgBox("该师徒关系已经建立，无需重复建立。", MsgBoxStyle.OkCancel, "提醒") = MsgBoxResult.Ok Then

                Me.Close()
            Else
                Exit Sub
            End If

        End If


    End Sub

    Private Sub TxtAName_TextChanged(sender As System.Object, e As System.EventArgs) Handles TxtAName.TextChanged

        Dim str As String = "select * from cs_driverinf where drivername='" & Me.TxtAName.Text.Trim & "'"
        Dim tab As DataTable = ReadData(str)
        If tab IsNot Nothing AndAlso tab.Rows.Count = 1 Then
            Dim row As DataRow = tab.Rows(0)
            Me.TxtANO.Text = row.Item("rdriverno").ToString
            Me.TxtATel.Text = row.Item("phone").ToString
            Me.TxtAPreM.Text = row.Item("marelation").ToString
        End If
    End Sub

    Private Sub TxtANO_TextChanged(sender As System.Object, e As System.EventArgs) Handles TxtANO.TextChanged

        Dim str As String = "select * from cs_driverinf where rdriverno='" & Me.TxtANO.Text.Trim & "'"
        Dim tab As DataTable = ReadData(str)
        If tab IsNot Nothing AndAlso tab.Rows.Count = 1 Then
            Dim row As DataRow = tab.Rows(0)
            Me.TxtAName.Text = row.Item("drivername").ToString
            Me.TxtATel.Text = row.Item("phone").ToString
            Me.TxtAPreM.Text = row.Item("marelation").ToString
        Else
            Me.TxtAName.Clear()
            Me.TxtATel.Clear()
            Me.TxtAPreM.Clear()
        End If
    End Sub

    Private Sub TxtATel_TextChanged(sender As System.Object, e As System.EventArgs) Handles TxtATel.TextChanged

        Dim str As String = "select * from cs_driverinf where phone='" & Me.TxtATel.Text.Trim & "'"
        Dim tab As DataTable = ReadData(str)
        If tab IsNot Nothing AndAlso tab.Rows.Count = 1 Then
            Dim row As DataRow = tab.Rows(0)
            Me.TxtAName.Text = row.Item("drivername").ToString
            Me.TxtANO.Text = row.Item("rdriverno").ToString
            Me.TxtAPreM.Text = row.Item("marelation").ToString
        End If
    End Sub

    Private Sub TxtMName_TextChanged(sender As System.Object, e As System.EventArgs) Handles TxtMName.TextChanged

        Dim str As String = "select * from cs_driverinf where drivername='" & Me.TxtMName.Text.Trim & "'"
        Dim tab As DataTable = ReadData(str)
        If tab IsNot Nothing AndAlso tab.Rows.Count = 1 Then
            Dim row As DataRow = tab.Rows(0)
            Me.TxtMNO.Text = row.Item("rdriverno").ToString
            Me.TxtMTel.Text = row.Item("phone").ToString
            Me.TxtPreA.Text = row.Item("marelation").ToString
        End If
    End Sub

    Private Sub TxtMNO_TextChanged(sender As System.Object, e As System.EventArgs) Handles TxtMNO.TextChanged

        Dim str As String = "select * from cs_driverinf where rdriverno='" & Me.TxtMNO.Text.Trim & "'"
        Dim tab As DataTable = ReadData(str)
        If tab IsNot Nothing AndAlso tab.Rows.Count = 1 Then
            Dim row As DataRow = tab.Rows(0)
            Me.TxtMName.Text = row.Item("drivername").ToString
            Me.TxtMTel.Text = row.Item("phone").ToString
            Me.TxtPreA.Text = row.Item("marelation").ToString
        End If
    End Sub

    Private Sub TxtMTel_TextChanged(sender As System.Object, e As System.EventArgs) Handles TxtMTel.TextChanged
 
        Dim str As String = "select * from cs_driverinf where phone='" & Me.TxtMTel.Text.Trim & "'"
        Dim tab As DataTable = ReadData(str)
        If tab IsNot Nothing AndAlso tab.Rows.Count = 1 Then
            Dim row As DataRow = tab.Rows(0)
            Me.TxtMName.Text = row.Item("drivername").ToString
            Me.TxtMNO.Text = row.Item("rdriverno").ToString
            Me.TxtPreA.Text = row.Item("marelation").ToString
        End If
    End Sub

    Private Sub TxtAPreM_TextChanged(sender As System.Object, e As System.EventArgs) Handles TxtAPreM.TextChanged

        Dim str As String = "select * from cs_driverinf where marelation='" & Me.TxtAPreM.Text.Trim & "'"
        Dim tab As DataTable = ReadData(str)
        If tab IsNot Nothing AndAlso tab.Rows.Count = 1 Then
            Dim row As DataRow = tab.Rows(0)
            Me.TxtAName.Text = row.Item("drivername").ToString
            Me.TxtANO.Text = row.Item("rdriverno").ToString
            Me.TxtATel.Text = row.Item("phone").ToString
        End If
    End Sub

    Private Sub TxtPreA_TextChanged(sender As System.Object, e As System.EventArgs) Handles TxtPreA.TextChanged

        Dim str As String = "select * from cs_driverinf where marelation='" & Me.TxtPreA.Text.Trim & "'"
        Dim tab As DataTable = ReadData(str)
        If tab IsNot Nothing AndAlso tab.Rows.Count = 1 Then
            Dim row As DataRow = tab.Rows(0)
            Me.TxtMName.Text = row.Item("drivername").ToString
            Me.TxtMNO.Text = row.Item("rdriverno").ToString
            Me.TxtMTel.Text = row.Item("phone").ToString
        End If
    End Sub

    Private Sub BtnCancel_Click(sender As System.Object, e As System.EventArgs) Handles BtnCancel.Click
        Me.ANO = Me.TxtANO.Text.Trim

        Me.MNO = Me.TxtMNO.Text.Trim



        If Me.TxtANO.Text.Trim <> "" AndAlso Me.TxtMNO.Text.Trim = "" Then
            MsgBox("该学徒不存在师傅信息，无需取消！", MsgBoxStyle.OkOnly, "提醒")

        ElseIf Me.TxtMNO.Text.Trim <> "" AndAlso Me.TxtANO.Text.Trim = "" Then
            MsgBox("该师傅目前没有徒弟，无需取消！", MsgBoxStyle.OkOnly, "提醒")
        Else
            If MsgBox("是否取消该对师徒关系？", MsgBoxStyle.OkCancel, "提醒") = MsgBoxResult.Ok Then
                Dim str As String = "update cs_driverinf set marelation='' where rdriverno='" & Me.TxtANO.Text.Trim & "'"
                UpdateData(str)
                str = "update cs_driverinf set marelation='' where rdriverno='" & Me.TxtMNO.Text.Trim & "'"
                UpdateData(str)
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            Else
                Exit Sub
            End If
        End If
    End Sub
End Class