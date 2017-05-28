Public Class frmSetAreainfo

    Public cmbDri As ComboBox
    Private Property CurMousex As Integer
    Private Property CurMousey As Integer
    Private Property CurCellx As Integer
    Private Property CurCelly As Integer

    Private Sub frmSetAreainfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call RefreshData()
    End Sub

    Public Sub RefreshData()
        cmbDri = New ComboBox
        AddHandler cmbDri.TextChanged, AddressOf cmbDriValueChanged
        Me.DGVMain.Rows.Clear()
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
            Dim index As Integer = 1
            For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                If dri IsNot Nothing Then
                    Me.DGVMain.Rows.Add(index, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.BeginWorkTime), _
                                    dri.CSLinkTrain(1).StartStaName, BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, _
                                    BeTime(dri.WorkTime), dri.DriveDistance, dri.BelongArea)
                    index += 1
                End If
            Next
            If CSTrainsAndDrivers.PreParedDutyDrivers IsNot Nothing AndAlso CSTrainsAndDrivers.PreParedDutyDrivers.Count > 0 Then
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedDutyDrivers
                    Me.DGVMain.Rows.Add(index, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.BeginWorkTime), _
                                dri.CSLinkTrain(1).StartStaName, BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, _
                                BeTime(dri.WorkTime), dri.DriveDistance, dri.BelongArea)
                    index += 1
                Next
            End If
            If CSTrainsAndDrivers.PreParedTrainDrivers IsNot Nothing AndAlso CSTrainsAndDrivers.PreParedTrainDrivers.Count > 0 Then
                For Each dri As CSDriver In CSTrainsAndDrivers.PreParedTrainDrivers
                    Me.DGVMain.Rows.Add(index, dri.CSdriverNo, dri.OutPutCSdriverNo, dri.DutySort, BeTime(dri.BeginWorkTime), _
                                dri.CSLinkTrain(1).StartStaName, BeTime(dri.EndEorkTime), dri.CSLinkTrain(UBound(dri.CSLinkTrain)).EndStaName, _
                                BeTime(dri.WorkTime), dri.DriveDistance, dri.BelongArea)
                Next
            End If
        End If
    End Sub

    Private Sub DGVMain_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVMain.CellClick
        If sender.SelectedCells.count = 1 Then
            Select Case sender.Columns(sender.SelectedCells(0).ColumnIndex).Name
                Case "所属区域"
                    Call RefreshCmbItems()
                    sender.Controls.Add(Me.cmbDri)
                    Me.cmbDri.Size = sender.SelectedCells(0).Size
                    Me.cmbDri.Location = New Point(CurMousex - CurCellx + sender.Left, CurMousey - CurCelly + sender.Top)
                    If sender.SelectedCells(0).value.ToString().Trim <> "" Then
                        Me.cmbDri.Text = sender.SelectedCells(0).value.ToString().Trim
                    End If
                    Me.cmbDri.Visible = True
            End Select
        End If
    End Sub

    Public Sub RefreshCmbItems()
        cmbDri.Items.Clear()
        If CSTrainsAndDrivers.AreaInfoS IsNot Nothing Then
            For Each area As AreaInfo In CSTrainsAndDrivers.AreaInfoS
                cmbDri.Items.Add(area.AreaName)
            Next
        End If
    End Sub

    Private Sub DGVMain_CellMouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGVMain.CellMouseDown
        cmbDri.Visible = False
        CurCellx = e.X
        CurCelly = e.Y
    End Sub

    Private Sub DGVMain_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DGVMain.MouseDown
        CurMousex = e.X
        CurMousey = e.Y
    End Sub

    Public Sub cmbDriValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim DriverNo As String = Me.DGVMain.Rows(Me.DGVMain.CurrentCell.RowIndex).Cells("任务编号").Value.ToString.Trim
        Dim Dutysort As String = Me.DGVMain.Rows(Me.DGVMain.CurrentCell.RowIndex).Cells("班种").Value.ToString.Trim
        Dim sDri As CSDriver = Nothing
        If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
            For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo = DriverNo AndAlso CSTrainsAndDrivers.CSDrivers(i).DutySort = Dutysort Then
                    sDri = CSTrainsAndDrivers.CSDrivers(i)
                    Exit For
                End If
            Next
        End If
        If sDri Is Nothing Then
            sDri = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                   Return (value.CSdriverNo = DriverNo AndAlso value.DutySort = Dutysort)
                                                               End Function)
            If sDri Is Nothing Then
                sDri = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                        Return (value.CSdriverNo = DriverNo AndAlso value.DutySort = Dutysort)
                                                                    End Function)
            End If
        End If
        If sDri IsNot Nothing Then
            sDri.BelongArea = cmbDri.Text.Trim
            Me.DGVMain.CurrentCell.Value = sDri.BelongArea
        End If
        Me.cmbDri.Visible = False
    End Sub

    Private Sub DGVMain_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGVMain.CellEndEdit
        If Me.DGVMain.SelectedCells.Count = 1 AndAlso Me.DGVMain.Columns(Me.DGVMain.SelectedCells(0).ColumnIndex).Name = "输出编号" Then
            Dim DriverNo As String = Me.DGVMain.Rows(Me.DGVMain.CurrentCell.RowIndex).Cells("任务编号").Value.ToString.Trim
            Dim Dutysort As String = Me.DGVMain.Rows(Me.DGVMain.CurrentCell.RowIndex).Cells("班种").Value.ToString.Trim
            Dim sDri As CSDriver = Nothing
            If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
                For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                    If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo = DriverNo AndAlso CSTrainsAndDrivers.CSDrivers(i).DutySort = Dutysort Then
                        sDri = CSTrainsAndDrivers.CSDrivers(i)
                        Exit For
                    End If
                Next
            End If
            If sDri Is Nothing Then
                sDri = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                       Return (value.CSdriverNo = DriverNo AndAlso value.DutySort = Dutysort)
                                                                   End Function)
                If sDri Is Nothing Then
                    sDri = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                            Return (value.CSdriverNo = DriverNo AndAlso value.DutySort = Dutysort)
                                                                        End Function)
                End If
            End If
            If sDri IsNot Nothing Then
                sDri.OutPutCSdriverNo = Me.DGVMain.CurrentCell.Value.ToString.Trim
            End If
        End If
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub

    Private Sub BtnOutPut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOutPut.Click
        Call OutPutToEXCELFileFormDataGrid("任务输出名称", Me.DGVMain, Me)
    End Sub

    Private Sub BtnInput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnInput.Click
        Dim New0penFile As New OpenFileDialog
        Dim strExcelFilePath As String
        New0penFile.Filter = "xls files (*.xls)|*.xls|xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
        New0penFile.FilterIndex = 1
        New0penFile.RestoreDirectory = True
        strExcelFilePath = ""

        If New0penFile.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            strExcelFilePath = New0penFile.FileName
        End If
        '获得数据库的名称
        If strExcelFilePath <> "" Then

            Dim MyCommand As System.Data.OleDb.OleDbDataAdapter
            Dim MyConnection As System.Data.OleDb.OleDbConnection
            MyConnection = New System.Data.OleDb.OleDbConnection( _
                          "provider=Microsoft.ACE.OLEDB.12.0; " & _
                          "data source='" & strExcelFilePath & "'; " & _
                          "Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'")
            Dim tmpStr As String
            tmpStr = "select * from" & "[任务输出名称$]"
            MyCommand = New System.Data.OleDb.OleDbDataAdapter(tmpStr, MyConnection)
            MyConnection.Open()

            Dim objDataset1 As New DataSet
            Try
                MyCommand.Fill(objDataset1, "XLData")
                Dim temTab As Data.DataTable = objDataset1.Tables(0)
                If temTab IsNot Nothing Then
                    For Each row As DataRow In temTab.Rows
                        Dim DriverNo As String = row.Item("任务编号").ToString
                        Dim Dutysort As String = row.Item("班种").ToString
                        Dim OutPutName As String = row.Item("输出编号").ToString
                        Dim sDri As CSDriver = Nothing
                        If CSTrainsAndDrivers.CSDrivers IsNot Nothing AndAlso UBound(CSTrainsAndDrivers.CSDrivers) > 0 Then
                            For i As Integer = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                                If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo = DriverNo AndAlso CSTrainsAndDrivers.CSDrivers(i).DutySort = Dutysort Then
                                    sDri = CSTrainsAndDrivers.CSDrivers(i)
                                    Exit For
                                End If
                            Next
                        End If
                        If sDri Is Nothing Then
                            sDri = CSTrainsAndDrivers.PreParedDutyDrivers.Find(Function(value As CSDriver)
                                                                                   Return (value.CSdriverNo = DriverNo AndAlso value.DutySort = Dutysort)
                                                                               End Function)
                            If sDri Is Nothing Then
                                sDri = CSTrainsAndDrivers.PreParedTrainDrivers.Find(Function(value As CSDriver)
                                                                                        Return (value.CSdriverNo = DriverNo AndAlso value.DutySort = Dutysort)
                                                                                    End Function)
                            End If
                        End If
                        If sDri IsNot Nothing Then
                            sDri.OutPutCSdriverNo = OutPutName
                        End If
                    Next
                End If
            Catch ex As Exception
                MsgBox("EXCEL数据库不正确，请确定打开的数据库格式正确!")
            End Try
            MyConnection.Close()
            GC.Collect()
        End If
        Call RefreshData()
    End Sub
End Class