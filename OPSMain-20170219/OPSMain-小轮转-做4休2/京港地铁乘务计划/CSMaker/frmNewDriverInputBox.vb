Public Class frmNewDriverInputBox

    Public TrainID As Integer = 0
    Public ParentWindow As frmCSTimeTableMain

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Dim SelDutySort As String = Me.cmbText.Text.Trim
        Dim SelDriverNo As String = Me.txtText.Text.Trim
        If SelDriverNo <> "" AndAlso SelDutySort <> "" Then
            If CSTrainsAndDrivers.CSDrivers Is Nothing = True OrElse UBound(CSTrainsAndDrivers.CSDrivers) = 0 Then
                Call AddUnReDoInfo(True)
                ReDim CSTrainsAndDrivers.CSDrivers(0)
                ReDim Preserve CSTrainsAndDrivers.CSDrivers(1)
                CSTrainsAndDrivers.CSDrivers(1) = New CSDriver()
                CSTrainsAndDrivers.CSDrivers(1).CSDriverID = 1
                CSTrainsAndDrivers.CSDrivers(1).CSdriverNo = SelDriverNo
                CSTrainsAndDrivers.CSDrivers(1).OutPutCSdriverNo = SelDriverNo
                CSTrainsAndDrivers.CSDrivers(1).DutySort = SelDutySort
                CSTrainsAndDrivers.CSDrivers(1).State = "°àÖÐ"
                Dim temMer As MergedCSLinkTrain = GetMergedTrain(CSTrainsAndDrivers.CSLinkTrains(TrainID))
                CSTrainsAndDrivers.CSDrivers(1).AddMergedTrain(temMer)
                CSTrainsAndDrivers.CSDrivers(1).RefreshState()
            Else
                Dim IfExist As Boolean = False
                For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                    If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo = SelDriverNo Then
                        IfExist = True
                        Exit For
                    End If
                Next
                If IfExist Then
                    MsgBox("¸ÃË¾»ú±àºÅÒÑ´æÔÚ£¬ÇëÖØÐÂÊäÈë£¡", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "ÌáÐÑ")
                    Exit Sub
                Else
                    Call AddUnReDoInfo(True)
                    ReDim Preserve CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers) + 1)
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)) = New CSDriver()
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSDriverID = UBound(CSTrainsAndDrivers.CSDrivers)
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).CSdriverNo = SelDriverNo
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).DutySort = SelDutySort
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).OutPutCSdriverNo = SelDriverNo
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).State = "°àÖÐ"
                    Dim temMer As MergedCSLinkTrain = GetMergedTrain(CSTrainsAndDrivers.CSLinkTrains(TrainID))
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).AddMergedTrain(temMer)
                    CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)).RefreshState()
                End If
            End If
            CSTrainsAndDrivers.CSLinkTrains(TrainID).IsLinked = True
            Select Case SelDutySort
                Case "Ôç°à"
                    CSTrainsAndDrivers.MorningDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
                Case "°×°à"
                    CSTrainsAndDrivers.DayDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
                Case "ÈÕÇÚ°à"
                    CSTrainsAndDrivers.CDayDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
                Case "Ò¹°à"
                    CSTrainsAndDrivers.NightDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
                Case Else
                    CSTrainsAndDrivers.OtherDrivers.Add(CSTrainsAndDrivers.CSDrivers(UBound(CSTrainsAndDrivers.CSDrivers)))
            End Select

            CSRefreshDiagram(1)
            Call ParentWindow.ListAllViewInfo()
            Call ParentWindow.CheckUnDoAndReDoState()
        Else
            MsgBox("Êä³öÄÚÈÝ²»ÄÜÎª¿Õ£¡", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "ÌáÐÑ")
            Exit Sub
        End If
        Me.Close()
    End Sub

    Private Sub cmdExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExit.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmInputBox_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Enter Then
            Call cmdOK_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub frmInputBox_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.cmbText.Text = ""
        Me.txtText.Text = ""
        Me.Text = "°²ÅÅÐÂ³ËÎñÔ±"
        Me.labTitle.Text = "Ñ¡Ôñ°àÖÖ:"
        Me.cmbText.Visible = True
        Me.txtText.Visible = True
        Me.cmbText.Items.Clear()
        Me.cmbText.Items.Add("Ôç°à")
        Me.cmbText.Items.Add("°×°à")
        Me.cmbText.Items.Add("ÈÕÇÚ°à")
        Me.cmbText.Items.Add("Ò¹°à")
        If Me.cmbText.Items.Count > 0 Then
            Me.cmbText.SelectedIndex = 0
        End If
    End Sub

    Private Sub cmbText_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbText.SelectedIndexChanged
        Select Case Me.cmbText.Text
            Case "Ôç°à"
                Dim maxno As Integer = 0
                For Each dri As CSDriver In CSTrainsAndDrivers.MorningDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo.Substring(2) > maxno Then
                        maxno = dri.CSdriverNo.Substring(2)
                    End If
                Next
                Me.txtText.Text = Me.cmbText.Text.Trim & (maxno + 1).ToString("00")
            Case "°×°à"
                Dim maxno As Integer = 0
                For Each dri As CSDriver In CSTrainsAndDrivers.DayDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo.Substring(2) > maxno Then
                        maxno = dri.CSdriverNo.Substring(2)
                    End If
                Next
                Me.txtText.Text = Me.cmbText.Text.Trim & (maxno + 1).ToString("00")
            Case "Ò¹°à"
                Dim maxno As Integer = 0
                For Each dri As CSDriver In CSTrainsAndDrivers.NightDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo.Substring(2) > maxno Then
                        maxno = dri.CSdriverNo.Substring(2)
                    End If
                Next
                Me.txtText.Text = Me.cmbText.Text.Trim & (maxno + 1).ToString("00")
            Case "ÈÕÇÚ°à"
                Dim maxno As Integer = 0
                For Each dri As CSDriver In CSTrainsAndDrivers.CDayDrivers
                    If dri IsNot Nothing AndAlso dri.CSdriverNo.Substring(2) > maxno Then
                        maxno = dri.CSdriverNo.Substring(2)
                    End If
                Next
                Me.txtText.Text = Me.cmbText.Text.Trim & (maxno + 1).ToString("00")

        End Select
    End Sub
End Class