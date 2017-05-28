'Public Class LivePanel : Inherits System.Windows.Forms.SplitContainer
'    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
'        If (m.Msg And &H114) = &H114 Then
'            Dim pNew As Point = AutoScrollPosition
'            Dim iH As Int16 = (m.WParam.ToInt32 >> 16)
'            If (m.WParam.ToInt32 And &HFFFF) = 5 Then
'                If m.Msg = &H114 Then pNew.X = iH Else pNew.Y = iH
'                AutoScrollPosition = pNew
'            End If
'        End If
'        MyBase.WndProc(m)
'    End Sub
'End Class
