Public Class frmListTimeTable

    Private Sub btnBegin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBegin.Click
        Dim i, j, k As Integer

        Dim dt As New Data.DataTable
        Dim strRows() As String
        ReDim strRows(3)

        Dim nTrain As Integer
        Dim sSta As String
        Dim sArriTime As String
        Dim sStartTime As String
        dt.Columns.Add(New Data.DataColumn("车次"))
        dt.Columns.Add(New Data.DataColumn("车站"))
        dt.Columns.Add(New Data.DataColumn("到点"))
        dt.Columns.Add(New Data.DataColumn("发点"))

        For i = 1 To UBound(ChediInfo)
            If ChediInfo(i).sCheCiHao = Me.cmbCheDiNum.Text Then
                For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                    nTrain = ChediInfo(i).nLinkTrain(j)
                    For k = 1 To UBound(TrainInf(nTrain).nPathID)
                        sSta = StationInf(TrainInf(nTrain).nPathID(k)).sStationName
                        sArriTime = SecondToHour(GetTrainArriTime(nTrain, sSta), 0)
                        sStartTime = SecondToHour(GetTrainStartTime(nTrain, sSta), 0)
                        strRows(0) = TrainInf(nTrain).sPrintTrain & "(" & j & ")"
                        strRows(1) = sSta
                        strRows(2) = sArriTime
                        strRows(3) = sStartTime
                        dt.Rows.Add(strRows)
                    Next k
                Next j
                Exit For
            End If
        Next i

        Me.dgdShowData.DataSource = dt
        Me.dgdShowData.RowHeadersWidth = 30
        For i = 1 To Me.dgdShowData.ColumnCount
            Me.dgdShowData.Columns(i - 1).Width = 70
        Next
    End Sub
   
    Private Sub frmListTimeTable_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        Me.cmbCheDiNum.Items.Clear()
        For i = 1 To UBound(ChediInfo)
            Me.cmbCheDiNum.Items.Add(ChediInfo(i).sCheCiHao)
        Next
        If Me.cmbCheDiNum.Items.Count > 0 Then
            Me.cmbCheDiNum.Text = Me.cmbCheDiNum.Items(0)
        End If
    End Sub

    Private Sub cmdOutToEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOutToEXCEL.Click
        Call OutPutToEXCELFileFormDataGrid("时刻表", Me.dgdShowData, Me)
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class