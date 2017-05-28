Public Class FrmTrainTT
    Dim StaList As New List(Of String)
    Dim mainfrm As frmCSTimeTableMain
    Public Sub New(ByVal frm As frmCSTimeTableMain)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        mainfrm = frm
    End Sub
    Private Sub FrmTrainTT_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
            If StaList.Contains(CSTrainsAndDrivers.CSLinkTrains(i).StartStaName) = False Then
                StaList.Add(CSTrainsAndDrivers.CSLinkTrains(i).StartStaName)
            End If
            If StaList.Contains(CSTrainsAndDrivers.CSLinkTrains(i).EndStaName) = False Then
                StaList.Add(CSTrainsAndDrivers.CSLinkTrains(i).EndStaName)
            End If
            If CSTrainsAndDrivers.CSLinkTrains(i).IsLinked = True Then
                For j As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                    If CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain.Count <= 1 Then
                        Continue For
                    End If
                    Dim z As Integer = 1
                    For z = 1 To UBound(CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain)
                        If CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain(z).StartTime = CSTrainsAndDrivers.CSLinkTrains(i).StartTime And CSTrainsAndDrivers.CSLinkTrains(i).EndTime = CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain(z).EndTime Then
                            DataGridView1.Rows.Add(CSTrainsAndDrivers.CSLinkTrains(i).CSTrainID, CSTrainsAndDrivers.CSLinkTrains(i).OutputCheCi, BeTime(CSTrainsAndDrivers.CSLinkTrains(i).StartTime), CSTrainsAndDrivers.CSLinkTrains(i).StartStaName, BeTime(CSTrainsAndDrivers.CSLinkTrains(i).EndTime), CSTrainsAndDrivers.CSLinkTrains(i).EndStaName, CSTrainsAndDrivers.CSLinkTrains(i).distance, CSTrainsAndDrivers.CSDrivers(j).CSdriverNo)
                            Exit For
                        End If
                    Next
                Next
            Else
                DataGridView1.Rows.Add(CSTrainsAndDrivers.CSLinkTrains(i).CSTrainID, CSTrainsAndDrivers.CSLinkTrains(i).OutputCheCi, BeTime(CSTrainsAndDrivers.CSLinkTrains(i).StartTime), CSTrainsAndDrivers.CSLinkTrains(i).StartStaName, BeTime(CSTrainsAndDrivers.CSLinkTrains(i).EndTime), CSTrainsAndDrivers.CSLinkTrains(i).EndStaName, CSTrainsAndDrivers.CSLinkTrains(i).distance, "-")
            End If
        Next
        ComboBox1.Items.Add("无")
        ComboBox1.Text = "无"
        ComboBox2.Text = "无"
        For i As Integer = 0 To StaList.Count - 1
            ComboBox1.Items.Add(StaList(i))
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DataGridView1.Rows.Clear()
        If ComboBox2.Text.Trim <> "无" And CInt(DateTimePicker1.Value.TimeOfDay.TotalSeconds) >= CInt(DateTimePicker2.Value.TimeOfDay.TotalSeconds) Then
            MsgBox("时间选择错误")
            Exit Sub
        End If
        For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSLinkTrains)
            If TextBox1.Text.Trim <> "" And CSTrainsAndDrivers.CSLinkTrains(i).OutputCheCi <> TextBox1.Text.Trim Then
                Continue For
            End If
            If ComboBox1.Text.Trim <> "无" And CSTrainsAndDrivers.CSLinkTrains(i).StartStaName <> ComboBox1.Text.Trim And CSTrainsAndDrivers.CSLinkTrains(i).EndStaName <> ComboBox1.Text.Trim Then
                Continue For
            End If
            If ComboBox2.Text.Trim = "开始" And (CSTrainsAndDrivers.CSLinkTrains(i).StartTime < CInt(DateTimePicker1.Value.TimeOfDay.TotalSeconds) Or CSTrainsAndDrivers.CSLinkTrains(i).StartTime > DateTimePicker2.Value.TimeOfDay.TotalSeconds) Then
                Continue For
            End If
            If ComboBox2.Text.Trim = "结束" And (CSTrainsAndDrivers.CSLinkTrains(i).EndTime < DateTimePicker1.Value.TimeOfDay.TotalSeconds Or CSTrainsAndDrivers.CSLinkTrains(i).EndTime > DateTimePicker2.Value.TimeOfDay.TotalSeconds) Then
                Continue For
            End If
            If CSTrainsAndDrivers.CSLinkTrains(i).IsLinked = True Then
                For j As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                    If CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain.Count <= 1 Then
                        Continue For
                    End If
                    Dim z As Integer = 1
                    For z = 1 To UBound(CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain)
                        If CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain(z).StartTime = CSTrainsAndDrivers.CSLinkTrains(i).StartTime And CSTrainsAndDrivers.CSLinkTrains(i).EndTime = CSTrainsAndDrivers.CSDrivers(j).CSLinkTrain(z).EndTime Then
                            DataGridView1.Rows.Add(CSTrainsAndDrivers.CSLinkTrains(i).CSTrainID, CSTrainsAndDrivers.CSLinkTrains(i).OutputCheCi, BeTime(CSTrainsAndDrivers.CSLinkTrains(i).StartTime), CSTrainsAndDrivers.CSLinkTrains(i).StartStaName, BeTime(CSTrainsAndDrivers.CSLinkTrains(i).EndTime), CSTrainsAndDrivers.CSLinkTrains(i).EndStaName, CSTrainsAndDrivers.CSLinkTrains(i).distance, CSTrainsAndDrivers.CSDrivers(j).CSdriverNo)
                            Exit For
                        End If
                    Next
                    Exit For
                Next
            Else
                DataGridView1.Rows.Add(CSTrainsAndDrivers.CSLinkTrains(i).CSTrainID, CSTrainsAndDrivers.CSLinkTrains(i).OutputCheCi, BeTime(CSTrainsAndDrivers.CSLinkTrains(i).StartTime), CSTrainsAndDrivers.CSLinkTrains(i).StartStaName, BeTime(CSTrainsAndDrivers.CSLinkTrains(i).EndTime), CSTrainsAndDrivers.CSLinkTrains(i).EndStaName, CSTrainsAndDrivers.CSLinkTrains(i).distance, "-")
            End If
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

  
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If IsNothing(e.RowIndex) Or e.RowIndex = -1 Then
            Exit Sub
        End If
        If CSTimeTablePara.sCurDiagramState = DiagramState.运行图 Then
            CSTimeTablePara.nPubTrain = DataGridView1.Rows(e.RowIndex).Cells("列车ID").Value.ToString.Trim
            If CSTimeTablePara.nPubTrain > 0 Then
                CSTimeTablePara.picPubDiagram.Refresh()
                Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象
                rBmpGraphics = CSTimeTablePara.picPubDiagram.CreateGraphics()
                Dim tmpPen As Pen
                tmpPen = New Pen(Color.Blue, 2)
                Call mainfrm.CSShowLabInfor(CSTimeTablePara.nPubTrain, mainfrm.labInfor)
                mainfrm.nIFShowAllCheDiTrain = 0
                Call CSDrawLineInPicture(CSTimeTablePara.nPubTrain, rBmpGraphics, tmpPen, CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth, CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime, CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime, CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank, CSTimeTablePara.TimeTableDiagramPara.sngStaBlank, CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX, CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi, CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi)
                Call mainfrm.SetCurScrollbarInSelectTrain(CSTimeTablePara.nPubTrain)
            Else
                CSTimeTablePara.picPubDiagram.Refresh()
            End If
        End If
    End Sub
End Class