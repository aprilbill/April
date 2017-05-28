Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class frmPicShow

    Private Sub frmPicShow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'If strQCurCSPlanName = "" Then
        '    MsgBox("请先选择要查询的乘务计划", MsgBoxStyle.OkOnly)
        '    Exit Sub
        'End If

        If CSTrainsAndDrivers.CSDrivers Is Nothing = True Then
            Exit Sub
        End If


        'strQCurCSPlanID = GetCSPlanIDFromCSPlanName(strQCurCSPlanName)
        Dim DriverNO As String = Me.TextBox1.Text.ToString.Trim
        Dim tempDriverID As Integer
        Dim tempCrewNumber As Integer = 0
        Dim EndDate As Integer = DateNumber
        Dim i, k As Integer
        Dim JiShu As Integer = 60 '图上多少长度为1小时
        '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        For i = 1 To UBound(CSTrainsAndDrivers.CSDrivers) '找到司机
            If CSTrainsAndDrivers.CSDrivers(i).CSdriverNo = DriverNO Then
                tempDriverID = CSTrainsAndDrivers.CSDrivers(i).CSDriverID
                Exit For
            End If
        Next

        If tempDriverID > 0 Then
            '***************************************************************************
            Dim g As Graphics '= Me.PictureBox1.CreateGraphics()
            Dim rbmp As Bitmap
            rbmp = New System.Drawing.Bitmap(500, 1000)
            g = Graphics.FromImage(rbmp)
            '**************************************************************************
            Dim X1, X2, Y1, Y2 As Integer
            Dim StartName, EndName As String
            Dim StartTime, EndTime As String


            Me.Banzhong.Text = CSTrainsAndDrivers.CSDrivers(tempDriverID).DutySort
            If CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain Is Nothing = False Then
                Me.DutyNum.Text = UBound(CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain)
            Else
                Me.DutyNum.Text = "0"
            End If



            Dim j As Integer = 60 '移动记录Y坐标
            Dim XLeft, XRight, XLeftStartStr, XRightStartStr As Integer
            Dim interval As Integer = 20 '交路上下的距离
            XLeft = 200
            XRight = 300
            XLeftStartStr = 50
            XRightStartStr = XRight + 20

            For k = 1 To UBound(CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain)
                StartName = CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain(k).StartStaName
                EndName = CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain(k).EndStaName
                StartTime = CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain(k).StartTime
                EndTime = CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain(k).EndTime
                X1 = CalXValue(CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain(k).StartStaID, XLeft, XRight)
                Y1 = j
                X2 = CalXValue(CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain(k).EndStaID, XLeft, XRight)
                If CInt(EndTime) - CInt(StartTime) > 0 Then
                    Y2 = Y1 + (CInt(EndTime) - CInt(StartTime)) / JiShu
                Else
                    Y2 = Y1 + (CInt(EndTime) + 86400 - CInt(StartTime)) / JiShu
                End If

                j = Y2
                Me.PictureBox1.Height = Y2 + 50
                g.DrawLine(Pens.Blue, X1, Y1, X2, Y2)
                'g.DrawEllipse(Pens.Black, CInt(X1 - Math.Cos(Math.PI / 2) * 5), CInt(Y1 - Math.Cos(Math.PI / 2) * 5), 5, 5)
                'g.DrawEllipse(Pens.Black, CInt(X2 - Math.Cos(Math.PI / 2) * 5), CInt(Y2 - Math.Cos(Math.PI / 2) * 5), 5, 5)

                If X2 - X1 > 0 Then
                    g.DrawString(StartName + BeTime(StartTime), New Font("宋体", 10), Brushes.Blue, XLeftStartStr, Y1 - 5)
                    ' g.DrawString(BeTime(StartTime), New Font("宋体", 10), Brushes.Blue, X1, Y1 - 20)
                    g.DrawString(EndName + BeTime(EndTime), New Font("宋体", 10), Brushes.Blue, XRightStartStr, Y2 - 5)
                    ' g.DrawString(BeTime(EndTime), New Font("宋体", 10), Brushes.Blue, X2 - 60, Y2 + 20)
                ElseIf X2 - X1 < 0 Then
                    g.DrawString(StartName + BeTime(StartTime), New Font("宋体", 10), Brushes.Blue, XRightStartStr, Y1 - 5)
                    ' g.DrawString(BeTime(StartTime), New Font("宋体", 10), Brushes.Blue, X1, Y1 + 20)
                    g.DrawString(EndName + BeTime(EndTime), New Font("宋体", 10), Brushes.Blue, XLeftStartStr, Y2 - 5)
                    'g.DrawString(BeTime(EndTime), New Font("宋体", 10), Brushes.Blue, X2 - 60, Y2 - 20)
                ElseIf X2 - X1 = 0 Then
                    g.DrawString(EndName + BeTime(EndTime), New Font("宋体", 10), Brushes.Blue, XRightStartStr, Y2 - 5) '用餐
                End If

                If k < UBound(CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain) Then
                    If CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain(k).EndStaName = CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain(k + 1).StartStaName And CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain(k).EndTime = CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain(k).StartTime Then

                    ElseIf CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain(k).EndStaName = CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain(k + 1).StartStaName And CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain(k).EndTime <> CSTrainsAndDrivers.CSDrivers(tempDriverID).CSLinkTrain(k).StartTime Then
                        j = j + interval
                        Dim JiaoLuInt As Integer = 5
                        If X2 - X1 > 0 Then
                            g.DrawLine(Pens.Red, X2, Y2, X2 + JiaoLuInt, Y2)
                            g.DrawLine(Pens.Red, X2, Y2 + interval, X2 + JiaoLuInt, Y2 + interval)
                            g.DrawLine(Pens.Red, X2 + JiaoLuInt, Y2, X2 + JiaoLuInt, Y2 + interval)
                        ElseIf X2 - X1 < 0 Then
                            g.DrawLine(Pens.Red, X2, Y2, X2 - JiaoLuInt, Y2)
                            g.DrawLine(Pens.Red, X2, Y2 + interval, X2 - JiaoLuInt, Y2 + interval)
                            g.DrawLine(Pens.Red, X2 - JiaoLuInt, Y2, X2 - JiaoLuInt, Y2 + interval)
                        End If
                    Else
                        j = j + interval
                    End If
                Else
                    j = j + interval
                End If
            Next
            Me.PictureBox1.Image = rbmp

        End If

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim strPath As String
        Dim New0penFile As New SaveFileDialog
        New0penFile.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|jpeg files (*.jpeg)|*.jpeg|All files (*.*)|*.*"
        New0penFile.FilterIndex = 1
        New0penFile.RestoreDirectory = True
        strPath = ""
        If New0penFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strPath = New0penFile.FileName
            Me.PictureBox1.Image.Save(strPath)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class