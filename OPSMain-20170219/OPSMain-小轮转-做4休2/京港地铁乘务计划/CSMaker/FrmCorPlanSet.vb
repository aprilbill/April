Public Class FrmCorPlanSet

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub FrmCorPlanSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call InputCSTimetableInf(strCurlineID) '改完
        If CSTimetableInf Is Nothing = False Then
            Me.CmbPlans.Items.Clear()
            For i = 1 To UBound(CSTimetableInf)
                Me.CmbPlans.Items.Add(CSTimetableInf(i).sName)
            Next i
            If Me.CmbPlans.Items.Count > 0 Then
                Me.CmbPlans.SelectedIndex = 0
            End If
        End If
        If CSTrainsAndDrivers.IfCorSchedule Then
            Me.CheckBox1.Checked = True
            Dim corIndex As Integer = Array.FindIndex(CSTimetableInf, Function(value As typeCSTimetableInf)
                                                                          Return value.sID = CSTrainsAndDrivers.CorCSTimetableID
                                                                      End Function)
            Me.CmbPlans.SelectedIndex = corIndex - 1
        Else
            Me.CheckBox1.Checked = False
        End If
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If Me.CheckBox1.Checked = True Then
            Me.GroupBox1.Enabled = True
        Else
            Me.GroupBox1.Enabled = False
        End If
    End Sub
End Class