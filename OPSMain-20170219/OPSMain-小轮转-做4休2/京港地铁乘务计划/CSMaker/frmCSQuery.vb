Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class frmCSQuery
    Public OldDriverno As String
    Public AreaInfos As List(Of AreaCalInfo)

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
            Dim DriverNO As String = Me.TxtDriverNo.Text.ToString.Trim
            Dim i, j As Integer
            Me.DataGridView1.Columns.Clear()
            Me.DataGridView1.Columns.Add("LINEID", "线路号")
            Me.DataGridView1.Columns.Add("ID", "任务编号")
            Me.DataGridView1.Columns.Add("DRIVERNO", "司机编号")
            Me.DataGridView1.Columns.Add("DUTYSORT", "班种")
            Me.DataGridView1.Columns.Add("TRAINNO", "车次")
            Me.DataGridView1.Columns.Add("STARTTIME", "起始时间")
            Me.DataGridView1.Columns.Add("STARTSTANAME", "起始站名")
            Me.DataGridView1.Columns.Add("ENDTIME", "结束时间")
            Me.DataGridView1.Columns.Add("ENDSTANAME", "结束站名")

            For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo = DriverNO AndAlso CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain Is Nothing = False Then
                    For j = 1 To UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)
                        Me.DataGridView1.Rows.Add()
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells("LINEID").Value = strCurlineID
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells("ID").Value = j
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells("DRIVERNO").Value = CSTrainsAndDrivers.CSDrivers(i).CSdriverNo
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells("DUTYSORT").Value = CSTrainsAndDrivers.CSDrivers(i).DutySort
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells("TRAINNO").Value = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).OutputCheCi
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells("STARTTIME").Value = BeTime(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).StartTime)
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells("STARTSTANAME").Value = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).StartStaName
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells("ENDTIME").Value = BeTime(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).EndTime)
                        Me.DataGridView1.Rows(Me.DataGridView1.Rows.Count - 1).Cells("ENDSTANAME").Value = CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(j).EndStaName
                    Next
                End If
            Next
            Me.DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End If

    End Sub

    Private Sub frmCSQuery_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
            DataGridView2.ClearSelection()

            ShowAllDutyINDgv()
            Me.TreeView1.Nodes.Add(strCurlineID, strCurlineID)
            For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                If CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain Is Nothing = False AndAlso UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain) > 0 Then
                    Me.TreeView1.Nodes(strCurlineID).Nodes.Add(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo, CSTrainsAndDrivers.CSDrivers(i).CSdriverNo)
                    Me.TreeView1.Nodes(strCurlineID).Nodes(CSTrainsAndDrivers.CSDrivers(i).CSdriverNo).Tag = CSTrainsAndDrivers.CSDrivers(i)
                End If
            Next

            Me.DataGrdSta.Columns.Clear()
            Me.DataGrdSta.Columns.Add("LINEID", "线路号")
            Me.DataGrdSta.Columns.Add("DriverID", "编号")
            Me.DataGrdSta.Columns.Add("DRIVERNO", "司机编号")
            Me.DataGrdSta.Columns.Add("DUTYSORT", "班种")
            Me.DataGrdSta.Columns.Add("WorkTime", "工作时间")
            Me.DataGrdSta.Columns.Add("DRIVETIME", "驾驶时间")
            Me.DataGrdSta.Columns.Add("DutyNum", "任务数")
            Me.DataGrdSta.Columns.Add("BeginTime", "开始时间")
            Me.DataGrdSta.Columns.Add("EndTime", "结束时间")

            For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo <> "#" Then
                    Me.DataGrdSta.Rows.Add()
                    Me.DataGrdSta.Rows(Me.DataGrdSta.Rows.Count - 1).Tag = CSTrainsAndDrivers.CSDrivers(i)
                    Me.DataGrdSta.Rows(Me.DataGrdSta.Rows.Count - 1).Cells("LINEID").Value = strCurlineID
                    Me.DataGrdSta.Rows(Me.DataGrdSta.Rows.Count - 1).Cells("DriverID").Value = CSTrainsAndDrivers.CSDrivers(i).CSDriverID
                    Me.DataGrdSta.Rows(Me.DataGrdSta.Rows.Count - 1).Cells("DRIVERNO").Value = CSTrainsAndDrivers.CSDrivers(i).CSdriverNo
                    Me.DataGrdSta.Rows(Me.DataGrdSta.Rows.Count - 1).Cells("DUTYSORT").Value = CSTrainsAndDrivers.CSDrivers(i).DutySort
                    Me.DataGrdSta.Rows(Me.DataGrdSta.Rows.Count - 1).Cells("DutyNum").Value = CSTrainsAndDrivers.CSDrivers(i).ModifiedDutyNumber
                    Me.DataGrdSta.Rows(Me.DataGrdSta.Rows.Count - 1).Cells("WorkTime").Value = BeTime(CSTrainsAndDrivers.CSDrivers(i).WorkTime)
                    Me.DataGrdSta.Rows(Me.DataGrdSta.Rows.Count - 1).Cells("DRIVETIME").Value = BeTime(CSTrainsAndDrivers.CSDrivers(i).DriveTime)

                    If UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain) > 0 Then
                        Me.DataGrdSta.Rows(Me.DataGrdSta.Rows.Count - 1).Cells("BeginTime").Value = BeTime(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(1).StartTime)
                        Me.DataGrdSta.Rows(Me.DataGrdSta.Rows.Count - 1).Cells("EndTime").Value = BeTime(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain(UBound(CSTrainsAndDrivers.CSDrivers(i).CSLinkTrain)).EndTime)
                    End If
                End If
            Next
            Me.DataGrdSta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            Call LoadAreaInfo()
            Me.Timer1.Enabled = True
        Else
            Me.FindTextBox.ReadOnly = True
        End If
    End Sub
    Private Sub DrawDriverDuty(ByVal tempDriver As CSDriver)
        Dim EndDate As Integer = DateNumber
        Dim k As Integer
        Dim JiShu As Integer = 60 '图上多少长度为1小时
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        If UBound(tempDriver.CSLinkTrain) > 0 Then
            '***************************************************************************
            Dim g As Graphics '= Me.PictureBox2.CreateGraphics()
            Me.PictureBox2.Width = 350
            Me.PictureBox2.Height = 750
            Dim rbmp As Bitmap
            rbmp = New System.Drawing.Bitmap(350, 750)
            g = Graphics.FromImage(rbmp)

            g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
            '**************************************************************************
            Dim X1, X2, Y1, Y2 As Integer
            Dim StartName, EndName, ThisVieNO, NextVieNO As String
            Dim StartTime, EndTime As String




            Dim j As Integer = 10 '移动记录Y坐标
            Dim XLeft, XRight, XLeftStartStr, XRightStartStr As Integer
            Dim interval As Integer = 20 '交路上下的距离
            XLeft = 110
            XRight = 210
            XLeftStartStr = 10
            XRightStartStr = XRight + 20

            For k = 1 To UBound(tempDriver.ModifiedCSLinkTrain)
                If tempDriver.ModifiedCSLinkTrain(k).OutputCheCi = "用餐" Then
                    Continue For
                End If
                StartName = tempDriver.ModifiedCSLinkTrain(k).StartStaName
                EndName = tempDriver.ModifiedCSLinkTrain(k).EndStaName
                ThisVieNO = tempDriver.ModifiedCSLinkTrain(k).sCheDiHao
                StartTime = tempDriver.ModifiedCSLinkTrain(k).StartTime
                EndTime = tempDriver.ModifiedCSLinkTrain(k).EndTime
                X1 = CalXValue(tempDriver.ModifiedCSLinkTrain(k).StartStaID, XLeft, XRight)
                Y1 = j
                X2 = CalXValue(tempDriver.ModifiedCSLinkTrain(k).EndStaID, XLeft, XRight)
                If CInt(EndTime) - CInt(StartTime) > 0 Then
                    Y2 = Y1 + (CInt(EndTime) - CInt(StartTime)) / JiShu
                Else
                    Y2 = Y1 + (CInt(EndTime) + 86400 - CInt(StartTime)) / JiShu
                End If

                j = Y2
                Me.PictureBox2.Height = Y2 + 50
                g.DrawLine(Pens.Blue, X1, Y1, X2, Y2)
                'g.DrawEllipse(Pens.Black, CInt(X1 - Math.Cos(Math.PI / 2) * 5), CInt(Y1 - Math.Cos(Math.PI / 2) * 5), 5, 5)
                'g.DrawEllipse(Pens.Black, CInt(X2 - Math.Cos(Math.PI / 2) * 5), CInt(Y2 - Math.Cos(Math.PI / 2) * 5), 5, 5)

                If X2 - X1 > 0 Then

                    g.DrawString(StartName + BeTime(StartTime), New Font("宋体", 8), Brushes.Blue, XLeftStartStr, Y1 - 5)

                    If k = UBound(tempDriver.ModifiedCSLinkTrain) Then
                        g.DrawString(EndName + BeTime(EndTime), New Font("宋体", 8), Brushes.Blue, XRightStartStr, Y2 - 5)
                    End If
                ElseIf X2 - X1 < 0 Then
                    g.DrawString(StartName + BeTime(StartTime), New Font("宋体", 8), Brushes.Blue, XRightStartStr, Y1 - 5)

                    If k = UBound(tempDriver.ModifiedCSLinkTrain) Then
                        g.DrawString(EndName + BeTime(EndTime), New Font("宋体", 8), Brushes.Blue, XLeftStartStr, Y2 - 5)
                    End If
                ElseIf X2 - X1 = 0 Then
                    g.DrawString(EndName + BeTime(EndTime), New Font("宋体", 8), Brushes.Blue, XRightStartStr, Y2 - 5) '用餐
                End If

                If k < UBound(tempDriver.ModifiedCSLinkTrain) Then
                    If tempDriver.ModifiedCSLinkTrain(k).EndStaName = tempDriver.ModifiedCSLinkTrain(k + 1).StartStaName And tempDriver.ModifiedCSLinkTrain(k).EndTime = tempDriver.ModifiedCSLinkTrain(k).StartTime Then

                    ElseIf tempDriver.ModifiedCSLinkTrain(k).EndStaName = tempDriver.ModifiedCSLinkTrain(k + 1).StartStaName And tempDriver.ModifiedCSLinkTrain(k).EndTime <> tempDriver.ModifiedCSLinkTrain(k).StartTime Then

                        NextVieNO = tempDriver.ModifiedCSLinkTrain(k + 1).sCheDiHao

                        j = j + interval
                        Dim JiaoLuInt As Integer = 5
                        If X2 - X1 > 0 Then
                            If ThisVieNO <> NextVieNO Then
                                g.DrawLine(Pens.Red, X2, Y2, X2 + JiaoLuInt, Y2)
                                g.DrawLine(Pens.Red, X2, Y2 + interval, X2 + JiaoLuInt, Y2 + interval)
                                g.DrawLine(Pens.Red, X2 + JiaoLuInt, Y2, X2 + JiaoLuInt, Y2 + interval)
                                g.DrawString("换" + EndName + BeTime(EndTime), New Font("宋体", 8), Brushes.Red, XRightStartStr, Y2 - 5)
                            Else
                                g.DrawLine(Pens.Red, X2, Y2, X2 + JiaoLuInt, Y2)
                                g.DrawLine(Pens.Red, X2, Y2 + interval, X2 + JiaoLuInt, Y2 + interval)
                                g.DrawLine(Pens.Red, X2 + JiaoLuInt, Y2, X2 + JiaoLuInt, Y2 + interval)
                                g.DrawString(EndName + BeTime(EndTime), New Font("宋体", 8), Brushes.Blue, XRightStartStr, Y2 - 5)
                            End If

                        ElseIf X2 - X1 < 0 Then
                            If ThisVieNO <> NextVieNO Then
                                g.DrawLine(Pens.Red, X2, Y2, X2 - JiaoLuInt, Y2)
                                g.DrawLine(Pens.Red, X2, Y2 + interval, X2 - JiaoLuInt, Y2 + interval)
                                g.DrawLine(Pens.Red, X2 - JiaoLuInt, Y2, X2 - JiaoLuInt, Y2 + interval)
                                g.DrawString("换" + EndName + BeTime(EndTime), New Font("宋体", 8), Brushes.Red, XLeftStartStr, Y2 - 5)
                            Else
                                g.DrawLine(Pens.Red, X2, Y2, X2 - JiaoLuInt, Y2)
                                g.DrawLine(Pens.Red, X2, Y2 + interval, X2 - JiaoLuInt, Y2 + interval)
                                g.DrawLine(Pens.Red, X2 - JiaoLuInt, Y2, X2 - JiaoLuInt, Y2 + interval)
                                g.DrawString(EndName + BeTime(EndTime), New Font("宋体", 8), Brushes.Blue, XLeftStartStr, Y2 - 5)
                            End If
                        End If

                    Else
                        j = j + interval
                    End If
                Else
                    'g.DrawString(BeTime(EndTime), New Font("宋体", 10), Brushes.Blue, X2 - 60, Y2 - 20)
                    j = j + interval
                End If
            Next
            'Panel2.Location = New Point(0, 0)
            Me.PictureBox2.Location = New Point(0, 0)
            Me.PictureBox2.Image = rbmp
            'Me.PictureBox2.SizeMode = PictureBoxSizeMode.Zoom

            'Call Fitlistview(tempDriverID)
        End If

    End Sub

    Private Sub TreeView1_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        If Me.TreeView1.SelectedNode.Tag Is Nothing Then
            ShowAllDutyINDgv()
        Else

            If Me.TreeView1.SelectedNode.Tag.GetType() Is GetType(CSDriver) Then
                Me.PropertyGrid1.SelectedObject = Me.TreeView1.SelectedNode.Tag
                DrawDriverDuty(Me.TreeView1.SelectedNode.Tag)
                ShowDutyINDgv(Me.TreeView1.SelectedNode.Tag)
                curDriver = Me.TreeView1.SelectedNode.Tag
                OldDriverno = curDriver.CSdriverNo
            End If
        End If
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitButton.Click
        Me.Close()
    End Sub
    Private Sub FindTextBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindTextBox.TextChanged
        For Each tn As TreeNode In Me.TreeView1.Nodes(strCurlineID).Nodes
            If tn.Name = Me.FindTextBox.Text.ToString.Trim Then
                Me.TreeView1.SelectedNode = tn
            End If
        Next
    End Sub

    Private Sub ShowDutyINDgv(ByVal tempDriver As CSDriver)
        If tempDriver Is Nothing = False Then

            Dim j As Integer

            Me.DataGridView2.Columns.Clear()
            Me.DataGridView2.Columns.Add("LINEID", "线路号")
            Me.DataGridView2.Columns.Add("ID", "任务编号")
            Me.DataGridView2.Columns.Add("DRIVERNO", "司机编号")
            Me.DataGridView2.Columns.Add("DUTYSORT", "班种")
            Me.DataGridView2.Columns.Add("TRAINNO", "车次")
            Me.DataGridView2.Columns.Add("ARRIVETIME", "接车时间")
            Me.DataGridView2.Columns.Add("STARTTIME", "开车时间")
            Me.DataGridView2.Columns.Add("STARTSTANAME", "起始站名")
            Me.DataGridView2.Columns.Add("ENDTIME", "结束时间")
            Me.DataGridView2.Columns.Add("ENDSTANAME", "结束站名")
            'If DriverNO = "" Then
            '    For i = 1 To UBound(CSDrivers)
            If tempDriver.ModifiedCSLinkTrain Is Nothing = False Then
                For j = 1 To UBound(tempDriver.ModifiedCSLinkTrain)
                    Me.DataGridView2.Rows.Add()
                    Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("LINEID").Value = strCurlineID
                    Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("ID").Value = j
                    Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("DRIVERNO").Value = tempDriver.CSdriverNo
                    Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("DUTYSORT").Value = tempDriver.DutySort
                    Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("TRAINNO").Value = tempDriver.ModifiedCSLinkTrain(j).OutputCheCi
                    Dim maxTime As Integer = -1
                    For z As Integer = 0 To CSTrainsAndDrivers.CSChedi(tempDriver.ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains.Count - 1
                        If AddLitterTime(CSTrainsAndDrivers.CSChedi(tempDriver.ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains(z).CulEndTime) < AddLitterTime(tempDriver.ModifiedCSLinkTrain(j).CulStartTime) Then
                            If AddLitterTime(maxTime) < AddLitterTime(CSTrainsAndDrivers.CSChedi(tempDriver.ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains(z).EndTime) Then
                                maxTime = CSTrainsAndDrivers.CSChedi(tempDriver.ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains(z).EndTime
                            End If
                        End If
                    Next
                    If maxTime = -1 Then
                        Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("ARRIVETIME").Value = BeTime(tempDriver.ModifiedCSLinkTrain(j).StartTime)
                    Else
                        Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("ARRIVETIME").Value = BeTime(maxTime)
                    End If
                    Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("STARTTIME").Value = BeTime(tempDriver.ModifiedCSLinkTrain(j).StartTime)
                    Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("STARTSTANAME").Value = tempDriver.ModifiedCSLinkTrain(j).StartStaName
                    Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("ENDTIME").Value = BeTime(tempDriver.ModifiedCSLinkTrain(j).EndTime)
                    Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("ENDSTANAME").Value = tempDriver.ModifiedCSLinkTrain(j).EndStaName
                Next
            End If
         
            Me.DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End If
    End Sub
    Private Sub ShowAllDutyINDgv()
        If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
            Dim i, j As Integer
            Me.DataGridView2.Columns.Clear()
            Me.DataGridView2.Columns.Add("LINEID", "线路号")
            'Me.DataGridView2.Columns.Add("ID", "任务编号")
            Me.DataGridView2.Columns.Add("DRIVERNO", "位置")
            Me.DataGridView2.Columns.Add("DUTYSORT", "班种")
            Me.DataGridView2.Columns.Add("dutyTIME", "出勤时间")
            Me.DataGridView2.Columns.Add("ARRIVETIME", "接车时间")
            Me.DataGridView2.Columns.Add("STARTTIME", "开车时间")
            Me.DataGridView2.Columns.Add("TRAINNO", "开车车次")
            Me.DataGridView2.Columns.Add("STARTSTANAME", "起始站名")
            Me.DataGridView2.Columns.Add("ENDTIME", "到达时间")
            Me.DataGridView2.Columns.Add("ENDSTANAME", "结束站名")


            For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers)
                If CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain Is Nothing = False Then
                 
                    For j = 1 To UBound(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain)
                        Me.DataGridView2.Rows.Add()
                        Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("LINEID").Value = strCurlineID
                        'Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("ID").Value = j
                        Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("DRIVERNO").Value = CSTrainsAndDrivers.CSDrivers(i).OutPutCSdriverNo
                        Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("DUTYSORT").Value = CSTrainsAndDrivers.CSDrivers(i).DutySort
                        If CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain(1).FirstStation.IsYard = True Then
                            Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("dutyTIME").Value = BeTime(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain(1).StartTime - CSTrainsAndDrivers.CSDrivers(i).PreTrainTime)
                        Else
                            Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("dutyTIME").Value = BeTime(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain(1).StartTime - CSTrainsAndDrivers.CSDrivers(i).PreDutyTime)
                        End If
                        Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("TRAINNO").Value = CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain(j).OutputCheCi
                        Dim maxTime As Integer = -1
                        For z As Integer = 0 To CSTrainsAndDrivers.CSChedi(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains.Count - 1
                            If AddLitterTime(CSTrainsAndDrivers.CSChedi(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains(z).CulEndTime) < AddLitterTime(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain(j).CulStartTime) Then
                                If AddLitterTime(maxTime) < AddLitterTime(CSTrainsAndDrivers.CSChedi(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains(z).EndTime) Then
                                    maxTime = CSTrainsAndDrivers.CSChedi(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain(j).nCheDiID).CSLinkTrains(z).EndTime
                                End If
                            End If
                        Next
                        If maxTime = -1 Then
                            Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("ARRIVETIME").Value = BeTime(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain(j).StartTime)
                        Else
                            Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("ARRIVETIME").Value = BeTime(maxTime)
                        End If
                        Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("STARTTIME").Value = BeTime(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain(j).StartTime)
                        Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("STARTSTANAME").Value = CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain(j).StartStaName
                        Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("ENDTIME").Value = BeTime(CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain(j).EndTime)
                        Me.DataGridView2.Rows(Me.DataGridView2.Rows.Count - 1).Cells("ENDSTANAME").Value = CSTrainsAndDrivers.CSDrivers(i).ModifiedCSLinkTrain(j).EndStaName
                    Next
                End If
            Next
            Me.DataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End If
    End Sub
    Private Sub PicButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicButton.Click
        Dim strPath As String
        Dim New0penFile As New SaveFileDialog
        New0penFile.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|jpeg files (*.jpeg)|*.jpeg|All files (*.*)|*.*"
        New0penFile.FilterIndex = 1
        New0penFile.RestoreDirectory = True
        strPath = ""
        If New0penFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strPath = New0penFile.FileName
            Me.PictureBox2.Image.Save(strPath)
        End If
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Call OutPutToEXCELFileFormDataGrid("乘务任务", Me.DataGridView2, Me)
    End Sub
    Private curDriver As CSDriver
    Private Sub DataGridView2_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick
        For Each tn As TreeNode In Me.TreeView1.Nodes(strCurlineID).Nodes
            If tn.Name = Me.DataGridView2.CurrentRow.Cells("DRIVERNO").Value.ToString.Trim Then
                'Me.TreeView1.SelectedNode = tn
                Me.PropertyGrid1.SelectedObject = tn.Tag
                curDriver = tn.Tag
                DrawDriverDuty(tn.Tag)
                Exit For
            End If
        Next
    End Sub

    Private Sub PropertyGrid1_PropertyValueChanged(ByVal s As System.Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles PropertyGrid1.PropertyValueChanged
        'Dim temcsDriver As CSDriver = CType(Me.PropertyGrid1.SelectedObject, CSDriver)
        For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
            If dri IsNot Nothing Then
                If dri IsNot curDriver Then
                    If dri.CSdriverNo = curDriver.CSdriverNo Then
                        MsgBox("该编号已经存在！", vbOKOnly, "提示")
                        curDriver.CSdriverNo = OldDriverno
                        Exit Sub
                    End If
                End If
            End If
        Next
        For Each t As TreeNode In Me.TreeView1.Nodes(strCurlineID).Nodes
            If CType(t.Tag, CSDriver) Is curDriver Then
                t.Text = curDriver.CSdriverNo
                For Each r As DataGridViewRow In Me.DataGrdSta.Rows
                    If CType(r.Tag, CSDriver) Is curDriver Then
                        r.Cells("DRIVERNO").Value = curDriver.CSdriverNo
                        r.Cells("DUTYSORT").Value = curDriver.DutySort
                        Exit For
                    End If
                Next
            End If
        Next
    End Sub

    Private Sub DataGrdSta_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGrdSta.CellClick
        For Each tn As TreeNode In Me.TreeView1.Nodes(strCurlineID).Nodes
            If tn.Name = Me.DataGrdSta.SelectedRows(0).Cells("DRIVERNO").Value.ToString.Trim Then
                Me.TreeView1.SelectedNode = tn
            End If
        Next
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If CSTrainsAndDrivers.CSDrivers Is Nothing = False Then
            Dim ADrinum As Integer = 0
            Dim MDrinum As Integer = 0
            Dim NDrinum As Integer = 0
            Dim CDrinum As Integer = 0
            'Call CleaAreaCalData()
            For Each dri As CSDriver In CSTrainsAndDrivers.CSDrivers
                If dri IsNot Nothing Then
                    Select Case dri.DutySort
                        Case "早班"
                            For Each area As AreaCalInfo In AreaInfos
                                If area.AreaName = dri.BelongArea Then
                                    area.MDriNum += 1
                                End If
                            Next
                            MDrinum += 1
                        Case "白班"
                            For Each area As AreaCalInfo In AreaInfos
                                If area.AreaName = dri.BelongArea Then
                                    area.NDriNum += 1
                                End If
                            Next
                            NDrinum += 1
                        Case "夜班"
                            For Each area As AreaCalInfo In AreaInfos
                                If area.AreaName = dri.BelongArea Then
                                    area.ADriNum += 1
                                End If
                            Next
                            ADrinum += 1
                        Case "日勤班"
                            For Each area As AreaCalInfo In AreaInfos
                                If area.AreaName = dri.BelongArea Then
                                    area.CDriNum += 1
                                End If
                            Next
                            CDrinum += 1
                    End Select
                End If
            Next
            Me.txtMdriverNum.Text = MDrinum
            Me.txtNdriverNum.Text = NDrinum
            Me.txtAdriverNum.Text = ADrinum
            Me.txtCdriverNum.Text = CDrinum
            For Each area As AreaCalInfo In AreaInfos
                Me.TreeViewCal.Nodes(area.AreaName).Nodes("司机总数").Text = "司机总数:" & area.ActualDriverNum
                Me.TreeViewCal.Nodes(area.AreaName).Nodes("早班").Text = "早班任务数:" & area.MDriNum
                Me.TreeViewCal.Nodes(area.AreaName).Nodes("白班").Text = "白班任务数:" & area.NDriNum
                Me.TreeViewCal.Nodes(area.AreaName).Nodes("日勤班").Text = "日勤班任务数:" & area.CDriNum
                Me.TreeViewCal.Nodes(area.AreaName).Nodes("夜班").Text = "夜班任务数:" & area.ADriNum
            Next
        End If
    End Sub

    Public Sub LoadAreaInfo()
        AreaInfos = New List(Of AreaCalInfo)
        Dim str As String = "select t.id,t.lineid,t.areaname,count(t.areaname) as actdrivernum from cs_areainfo t,cs_driverinf m " & _
            "where t.lineid=m.lineid and t.areaname=m.bezone and t.lineid='" & strCurlineID & "' group by t.areaname,t.id,t.lineid order by id"
        Dim tab As Data.DataTable = ReadData(str)
        If tab IsNot Nothing Then
            For Each row As DataRow In tab.Rows
                Dim areaname As String = row.Item("areaname").ToString.Trim
                Dim temareainfo As New AreaCalInfo(strCurlineID, areaname)
                temareainfo.ActualDriverNum = row.Item("actdrivernum")
                AreaInfos.Add(temareainfo)
            Next
        End If
        Me.TreeViewCal.Nodes.Clear()
        For Each area As AreaCalInfo In AreaInfos
            Me.TreeViewCal.Nodes.Add(area.AreaName, area.AreaName)
            Me.TreeViewCal.Nodes(area.AreaName).Nodes.Add("司机总数", "司机总数:0")
            Me.TreeViewCal.Nodes(area.AreaName).Nodes.Add("早班", "早班任务数:0")
            Me.TreeViewCal.Nodes(area.AreaName).Nodes.Add("白班", "白班任务数:0")
            Me.TreeViewCal.Nodes(area.AreaName).Nodes.Add("日勤班", "日勤班任务数:0")
            Me.TreeViewCal.Nodes(area.AreaName).Nodes.Add("夜班", "夜班任务数:0")
        Next
        Me.TreeViewCal.ExpandAll()
    End Sub

    Public Sub CleaAreaCalData()
        For Each area As AreaCalInfo In AreaInfos
            area.MDriNum = 0
            area.NDriNum = 0
            area.CDriNum = 0
            area.ADriNum = 0
        Next
    End Sub

    Public Class AreaCalInfo
        Public AreaName As String
        Public CurLineName As String
        Public MDriNum As Integer = 0
        Public NDriNum As Integer = 0
        Public CDriNum As Integer = 0
        Public ADriNum As Integer = 0
        Public ActualDriverNum As Integer
        Public Sub New(ByVal _lineid As String, ByVal _areaName As String)
            AreaName = _areaName
            CurLineName = _lineid
        End Sub
    End Class

    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub
End Class