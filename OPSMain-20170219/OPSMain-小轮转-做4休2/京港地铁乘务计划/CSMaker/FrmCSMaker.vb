Imports System.Threading

Public Class FrmCSMaker

    Private Sub Btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_OK.Click
        If IsNumeric(TXTMorningNum.Text) = False OrElse IsNumeric(TXTDayNum.Text) = False OrElse _
            IsNumeric(TXTCDayNum.Text) = False OrElse IsNumeric(TXTNightNum.Text) = False  Then
            MsgBox("所有设置必须为数字！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
            Exit Sub
        End If
        'If Int(TXTMorningNum.Text) <> (Int(MFBNUM.Text) + Int(MQHNUM.Text)) OrElse _
        '    Int(TXTDayNum.Text) <> (Int(DFBNUM.Text) + Int(DQHNUM.Text)) OrElse _
        '    Int(TXTNightNum.Text) <> (Int(NFBNUM.Text) + Int(NQHNUM.Text)) Then
        '    MsgBox("各班人数总和应等于俸伯与清华之和！", MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "提示")
        '    Exit Sub
        'End If
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

End Class

Public Class InDepotDriverList
    Public DepotName As String       '车场名称
    Public AreaName As String
    Public CSRukuDrivers As List(Of CSDriver)     '入库车集合
    Public Sub New(ByVal _depotName As String, ByVal _areaName As String)
        CSRukuDrivers = New List(Of CSDriver)
        AreaName = _areaName
        DepotName = _depotName
    End Sub
End Class

<Serializable()> _
Public Class CorCSPlan
    Public ReadOnly Property NightDriNo As String
        Get
            Return NightDriver.CSdriverNo
        End Get
    End Property
    Public ReadOnly Property MorningDriNo As String
        Get
            Return MorningDriver.CSdriverNo
        End Get
    End Property
    Public NightDriver As CSDriver
    Public MorningDriver As CSDriver

    Public Sub New(ByVal _Ndri As CSDriver, ByVal _Mdri As CSDriver)
        NightDriver = _Ndri
        MorningDriver = _Mdri
    End Sub
End Class