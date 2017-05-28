Public Class frmDrawCircleTrainDiagram
    Public CurEdit As String       '表示当前的控件，是文本框还是下拉框
    Public nCurJLID As Integer      '局公有变量，表示当前的GaoFenTimeSet()当前ID号
    Dim CurCellX As Single
    Dim CurCellY As Single
    Dim CurMouseX As Single
    Dim CurMouseY As Single
    Dim sFirZFTime As Long
    Dim sEndZFTime As Long

    Private Sub frmDrawCircleTrainDiagram_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        Dim p As Integer
        If UBound(BasicTrainInf) > 0 Then
            For i = 1 To UBound(BasicTrainInf)
                If BasicTrainInf(i).TrainStyle = "环形车" Then
                    For p = 1 To UBound(StationInf)
                        If BasicTrainInf(i).ComeStation = StationInf(p).sStationName Then
                            Me.cmbTrainJLStyle.Items.Add(BasicTrainInf(i).sJiaoLuName)
                            Exit For
                        End If
                    Next p
                End If
            Next i
            If Me.cmbTrainJLStyle.Items.Count > 0 Then
                Me.cmbTrainJLStyle.Text = Me.cmbTrainJLStyle.Items(0)
            End If
        Else
            MsgBox("列车信息中没有交路信息，请检查列车信息！")
        End If

        Me.cmbPuHuaStyle.Items.Add("工作日")
        Me.cmbPuHuaStyle.Items.Add("双休日")
        Me.cmbPuHuaStyle.Items.Add("节假日")
        Me.cmbPuHuaStyle.Items.Add("其它")

        Me.cmbPuHuaStyle.Text = "工作日"

        sJiaoLuStyle = Trim(Me.cmbTrainJLStyle.Text)
        sPuHuaStyle = Trim(Me.cmbPuHuaStyle.Text)
        Call GetBaseStation(sJiaoLuStyle)
        Call InputJGData
    End Sub

    '导入时间间隔数据到GRD中
    Private Sub InputJGData()
        Call CallTimeSet(2)
        Call ListGrdTime()
    End Sub
    '得到第一个基准站的名称
    Private Sub GetBaseStation(ByVal sJLstyle As String)
        Dim j As Integer

        Dim sFirstBaseSta As String
        Dim sElseBaseSta As String
        For j = 1 To UBound(BasicTrainInf)
            If BasicTrainInf(j).sJiaoLuName = sJLstyle Then
                sFirstBaseSta = BasicTrainInf(j).ComeStation
                sElseBaseSta = BasicTrainInf(j).NextStation
                sFirZFTime = GetZheFanTime(BasicTrainInf(j).SCheDiLeiXing, sFirstBaseSta, BasicTrainInf(j).TrainReturnStyle(1))
                sEndZFTime = GetZheFanTime(BasicTrainInf(j).SCheDiLeiXing, sElseBaseSta, BasicTrainInf(j).TrainReturnStyle(2))
                Me.labZheFanTime.Text = "交路类型：" & BasicTrainInf(j).TrainStyle & "  最小折返时间：" & sFirstBaseSta & ": " & SecondToMinute(sFirZFTime) & _
                                           "  " & sElseBaseSta & ": " & SecondToMinute(sEndZFTime)
                Exit For
            End If
        Next j

    End Sub
    '计算每个间隔时间里的数值
    Private Sub CallTimeSet(ByVal nState As Integer)
        Dim i As Integer
        Dim j, k As Integer
        Select Case nState
            Case 1 '根据周期推折返时间
                For i = 1 To UBound(GaoFenTimeSet)
                    If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                        For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                            GaoFenTimeSet(i).lDownRunTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                            GaoFenTimeSet(i).lUpRunTime(j) = 0
                            GaoFenTimeSet(i).lDownStopTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                            GaoFenTimeSet(i).lUpStopTime(j) = 0 ' CalTrainRunTimeNotStopFromTrain(sFanJLName, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                            GaoFenTimeSet(i).lFirZheFanTime(j) = (GaoFenTimeSet(i).lZhouQiTime(j) - GaoFenTimeSet(i).lDownRunTime(j) _
                                                                    - GaoFenTimeSet(i).lUpRunTime(j) - GaoFenTimeSet(i).lDownStopTime(j) _
                                                                    - GaoFenTimeSet(i).lUpStopTime(j)) / 2
                            GaoFenTimeSet(i).lEndZheFanTime(j) = GaoFenTimeSet(i).lZhouQiTime(j) - GaoFenTimeSet(i).lDownRunTime(j) _
                                                                    - GaoFenTimeSet(i).lUpRunTime(j) - GaoFenTimeSet(i).lDownStopTime(j) _
                                                                    - GaoFenTimeSet(i).lUpStopTime(j) - GaoFenTimeSet(i).lFirZheFanTime(j)

                        Next j

                        Exit For
                    End If
                Next i

            Case 2 '根据折返时间推周期

                '数据不全的时候



                For i = 1 To UBound(GaoFenTimeSet)
                    If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                        If GaoFenTimeSet(i).sRunScaleName(1) = "" Then
                            For j = 1 To UBound(BasicTrainInf)
                                If BasicTrainInf(j).sJiaoLuName = Me.cmbTrainJLStyle.Text.Trim Then
                                    If UBound(BasicTrainInf(j).SecScale) > 0 Then
                                        For k = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                            GaoFenTimeSet(i).sRunScaleName(k) = BasicTrainInf(j).SecScale(1).sName
                                        Next
                                        Exit For
                                    End If
                                End If
                            Next
                        End If
                        If GaoFenTimeSet(i).sStopScaleName(1) = "" Then
                            For j = 1 To UBound(BasicTrainInf)
                                If BasicTrainInf(j).sJiaoLuName = Me.cmbTrainJLStyle.Text Then
                                    If UBound(BasicTrainInf(j).StopScale) > 0 Then
                                        For k = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                            GaoFenTimeSet(i).sStopScaleName(k) = BasicTrainInf(j).StopScale(1).sName
                                        Next
                                        Exit For
                                    End If
                                End If
                            Next j
                        End If
                        If GaoFenTimeSet(i).lFirZheFanTime(1) = 0 Then
                            For k = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                GaoFenTimeSet(i).lFirZheFanTime(k) = sFirZFTime
                                GaoFenTimeSet(i).lEndZheFanTime(k) = sEndZFTime
                            Next
                        End If
                        For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                            GaoFenTimeSet(i).lDownRunTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                            GaoFenTimeSet(i).lUpRunTime(j) = 0 ' CalTrainRunTimeNotStopFromTrain(sFanJLName, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                            GaoFenTimeSet(i).lDownStopTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                            GaoFenTimeSet(i).lUpStopTime(j) = 0 'CalTrainRunTimeNotStopFromTrain(sFanJLName, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                            GaoFenTimeSet(i).lZhouQiTime(j) = GaoFenTimeSet(i).lDownRunTime(j) _
                                                             + GaoFenTimeSet(i).lUpRunTime(j) _
                                                             + GaoFenTimeSet(i).lDownStopTime(j) _
                                                             + GaoFenTimeSet(i).lUpStopTime(j) _
                                                             + GaoFenTimeSet(i).lFirZheFanTime(j) _
                                                             + GaoFenTimeSet(i).lEndZheFanTime(j)
                        Next j

                        Exit For
                    End If
                Next i

        End Select
    End Sub

    '显示内容
    Private Sub ListGrdTime()
        Dim i As Integer
        Dim j As Integer
        For i = 1 To UBound(GaoFenTimeSet)
            If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                Me.grdTime.RowCount = UBound(GaoFenTimeSet(i).nXuHao) + 1
                For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                    'nCurJLID = i
                    Me.grdTime.Item(0, j - 1).Value = GaoFenTimeSet(i).nXuHao(j)
                    Me.grdTime.Item(1, j - 1).Value = dTime(DeleteLitterTime(GaoFenTimeSet(i).BeTime(j)), 5)
                    Me.grdTime.Item(2, j - 1).Value = dTime(DeleteLitterTime(GaoFenTimeSet(i).EndTime(j)), 5)
                    Me.grdTime.Item(3, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).lZhouQiTime(j))
                    Me.grdTime.Item(4, j - 1).Value = GaoFenTimeSet(i).sRunScaleName(j)
                    Me.grdTime.Item(5, j - 1).Value = GaoFenTimeSet(i).sStopScaleName(j)
                    Me.grdTime.Item(6, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).lFirZheFanTime(j))
                    Me.grdTime.Item(7, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).lEndZheFanTime(j))
                    Me.grdTime.Item(8, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).lDownRunTime(j))
                    Me.grdTime.Item(9, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).lDownStopTime(j))
                    Me.grdTime.Item(10, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).lUpRunTime(j))
                    Me.grdTime.Item(11, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).lUpStopTime(j))
                    Me.grdTime.Item(12, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).JGtime(j))
                    Me.grdTime.Item(13, j - 1).Value = GaoFenTimeSet(i).ChediNum(j)
                    Me.grdTime.Item(14, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).JGOne(j))
                    Me.grdTime.Item(15, j - 1).Value = GaoFenTimeSet(i).NumOne(j)
                    Me.grdTime.Item(16, j - 1).Value = SecondToMinute(GaoFenTimeSet(i).JGTwo(j))
                    Me.grdTime.Item(17, j - 1).Value = GaoFenTimeSet(i).NumTwo(j)
                Next j
                Exit For
            End If
        Next i
    End Sub

    Private Sub grdTime_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTime.CellClick
        Dim i As Integer
        Dim j As Integer
        If Me.grdTime.CurrentCell.RowIndex >= 0 And Me.grdTime.Columns(Me.grdTime.CurrentCell.ColumnIndex).Name = "运行标尺" Then
            If CurCellX > 0 And CurCellY > 0 Then

                Me.cmbEditTime.Left = CurMouseX - CurCellX + Me.grdTime.Left
                Me.cmbEditTime.Top = CurMouseY - CurCellY + Me.grdTime.Top
                Me.cmbEditTime.Width = Me.grdTime.Columns(Me.grdTime.CurrentCell.ColumnIndex).Width
                Me.cmbEditTime.Items.Clear()

                'For i = 1 To UBound(TrainRunScaleInf)
                '    Me.cmbEditTime.Items.Add(TrainRunScaleInf(i).sScaleName)
                'Next

                For i = 1 To UBound(BasicTrainInf)
                    If BasicTrainInf(i).sJiaoLuName = Me.cmbTrainJLStyle.Text.Trim Then
                        For j = 1 To UBound(BasicTrainInf(i).SecScale)
                            Me.cmbEditTime.Items.Add(BasicTrainInf(i).SecScale(j).sName)
                        Next
                    End If
                Next

                Me.cmbEditTime.Text = Me.grdTime.CurrentCell.Value
                Me.cmbEditTime.Visible = True
                Me.cmbEditTime.Select()
                CurCellX = 0
                CurCellY = 0
            End If
        End If

        If Me.grdTime.CurrentCell.RowIndex >= 0 And Me.grdTime.Columns(Me.grdTime.CurrentCell.ColumnIndex).Name = "停站标尺" Then
            If CurCellX > 0 And CurCellY > 0 Then

                Me.cmbEditTime.Left = CurMouseX - CurCellX + Me.grdTime.Left
                Me.cmbEditTime.Top = CurMouseY - CurCellY + Me.grdTime.Top
                Me.cmbEditTime.Width = Me.grdTime.Columns(Me.grdTime.CurrentCell.ColumnIndex).Width
                Me.cmbEditTime.Items.Clear()
                For i = 1 To UBound(BasicTrainInf)
                    If BasicTrainInf(i).sJiaoLuName = Me.cmbTrainJLStyle.Text Then
                        If UBound(BasicTrainInf(i).StopScale) > 0 Then
                            For j = 1 To UBound(BasicTrainInf(i).StopScale)
                                Me.cmbEditTime.Items.Add(BasicTrainInf(i).StopScale(j).sName)
                            Next j
                            cmbEditTime.Text = Me.grdTime.CurrentCell.Value

                        Else
                            'MsgBox("该列车交路信息中没有定义停站标尺信息，请检查列车信息！")
                            'Exit Sub
                        End If

                        Exit For
                    End If
                Next i
                Me.cmbEditTime.Select()
                Me.cmbEditTime.Visible = True
                CurCellX = 0
                CurCellY = 0
            End If
        End If
    End Sub

    Private Sub grdTime_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTime.CellEndEdit
        Call ValueChange()
    End Sub
    Private Sub ValueChange()
        Dim i, j As Integer
        Dim Time1, Time2 As Long
        Call GZLineUseSet()
        Dim sNowValue As String
        sNowValue = Me.grdTime.Columns(Me.grdTime.CurrentCell.ColumnIndex).Name

        Select Case sNowValue
            Case "运行周期"

                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then

                                    GaoFenTimeSet(i).lZhouQiTime(j) = MinuteToSecond(Me.grdTime.CurrentCell.Value)
                                    GaoFenTimeSet(i).lDownRunTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lUpRunTime(j) = 0 'CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lDownStopTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lUpStopTime(j) = 0 'CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lFirZheFanTime(j) = GaoFenTimeSet(i).lZhouQiTime(j) - GaoFenTimeSet(i).lDownRunTime(j) _
                                                                            - GaoFenTimeSet(i).lUpRunTime(j) - GaoFenTimeSet(i).lDownStopTime(j) _
                                                                            - GaoFenTimeSet(i).lUpStopTime(j)
                                    GaoFenTimeSet(i).lEndZheFanTime(j) = GaoFenTimeSet(i).lFirZheFanTime(j) ' GaoFenTimeSet(i).lZhouQiTime(j) - GaoFenTimeSet(i).lDownRunTime(j) _
                                    ' - GaoFenTimeSet(i).lUpRunTime(j) - GaoFenTimeSet(i).lDownStopTime(j) _
                                    ' - GaoFenTimeSet(i).lUpStopTime(j) - GaoFenTimeSet(i).lFirZheFanTime(j)
                                    Exit For
                                End If

                            Next j

                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If

            Case "运行标尺"

                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then
                                    GaoFenTimeSet(i).sRunScaleName(j) = Me.grdTime.Item(4, Me.grdTime.CurrentCell.RowIndex).Value
                                    GaoFenTimeSet(i).lDownRunTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lUpRunTime(j) = 0 'CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lDownStopTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lUpStopTime(j) = 0 'CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lZhouQiTime(j) = GaoFenTimeSet(i).lFirZheFanTime(j) _
                                                                      + GaoFenTimeSet(i).lDownRunTime(j) _
                                                                      + GaoFenTimeSet(i).lUpRunTime(j) _
                                                                      + GaoFenTimeSet(i).lDownStopTime(j) _
                                                                      + GaoFenTimeSet(i).lUpStopTime(j) ' + GaoFenTimeSet(i).lEndZheFanTime(j) _
                                    Exit For
                                End If

                            Next j

                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If

            Case "停站标尺"
                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then
                                    GaoFenTimeSet(i).sStopScaleName(j) = Me.grdTime.Item(5, Me.grdTime.CurrentCell.RowIndex).Value
                                    GaoFenTimeSet(i).lDownRunTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lUpRunTime(j) = 0 'CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lDownStopTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lUpStopTime(j) = 0 'CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lZhouQiTime(j) = GaoFenTimeSet(i).lFirZheFanTime(j) _
                                                                      + GaoFenTimeSet(i).lDownRunTime(j) _
                                                                      + GaoFenTimeSet(i).lUpRunTime(j) _
                                                                      + GaoFenTimeSet(i).lDownStopTime(j) _
                                                                      + GaoFenTimeSet(i).lUpStopTime(j) '  + GaoFenTimeSet(i).lEndZheFanTime(j) _
                                    Exit For
                                End If

                            Next j

                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If

            Case "始发折返"

                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then
                                    GaoFenTimeSet(i).lDownRunTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lUpRunTime(j) = 0 ' CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lDownStopTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lUpStopTime(j) = 0 ' CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lFirZheFanTime(j) = MinuteToSecond(Me.grdTime.Item(6, Me.grdTime.CurrentCell.RowIndex).Value)
                                    GaoFenTimeSet(i).lZhouQiTime(j) = GaoFenTimeSet(i).lFirZheFanTime(j) _
                                                                      + GaoFenTimeSet(i).lDownRunTime(j) _
                                                                      + GaoFenTimeSet(i).lUpRunTime(j) _
                                                                      + GaoFenTimeSet(i).lDownStopTime(j) _
                                                                      + GaoFenTimeSet(i).lUpStopTime(j) ' + GaoFenTimeSet(i).lEndZheFanTime(j) _

                                    Exit For
                                End If

                            Next j

                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If

            Case "终到折返"
                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then
                                    GaoFenTimeSet(i).lDownRunTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lUpRunTime(j) = 0 'CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 1)
                                    GaoFenTimeSet(i).lDownStopTime(j) = CalTrainRunTimeNotStopFromTrain(sJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lUpStopTime(j) = 0 ' CalTrainRunTimeNotStopFromTrain(sFanJiaoLuStyle, GaoFenTimeSet(i).sRunScaleName(j), GaoFenTimeSet(i).sStopScaleName(j), 2)
                                    GaoFenTimeSet(i).lEndZheFanTime(j) = MinuteToSecond(Me.grdTime.Item(7, Me.grdTime.CurrentCell.RowIndex).Value)
                                    GaoFenTimeSet(i).lZhouQiTime(j) = GaoFenTimeSet(i).lFirZheFanTime(j) _
                                                                      + GaoFenTimeSet(i).lDownRunTime(j) _
                                                                      + GaoFenTimeSet(i).lUpRunTime(j) _
                                                                      + GaoFenTimeSet(i).lDownStopTime(j) _
                                                                      + GaoFenTimeSet(i).lUpStopTime(j) ' + GaoFenTimeSet(i).lEndZheFanTime(j) _


                                    Exit For
                                End If

                            Next j

                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If


            Case "发车间隔"

                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then
                                    GaoFenTimeSet(i).JGtime(j) = MinuteToSecond(Me.grdTime.Item(12, Me.grdTime.CurrentCell.RowIndex).Value)
                                    Time1 = GaoFenTimeSet(i).lZhouQiTime(j)
                                    Time2 = GaoFenTimeSet(i).JGtime(j)
                                    If Time2 > 0 Then
                                        If Int(Time1 / Time2) = Time1 / Time2 Then
                                            GaoFenTimeSet(i).ChediNum(j) = Int(Time1 / Time2)
                                        Else
                                            GaoFenTimeSet(i).ChediNum(j) = Int(Time1 / Time2) + 1
                                        End If
                                        GaoFenTimeSet(i).JGOne(j) = GaoFenTimeSet(i).JGtime(j)
                                        GaoFenTimeSet(i).JGTwo(j) = Time1 - GaoFenTimeSet(i).JGtime(j) * (GaoFenTimeSet(i).ChediNum(j) - 1)
                                        GaoFenTimeSet(i).NumOne(j) = GaoFenTimeSet(i).ChediNum(j) - 1
                                        GaoFenTimeSet(i).NumTwo(j) = 1
                                    End If
                                    Exit For
                                End If

                            Next j

                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If

            Case "车底数量"
                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then
                                    GaoFenTimeSet(i).ChediNum(j) = Me.grdTime.Item(13, Me.grdTime.CurrentCell.RowIndex).Value
                                    Time1 = GaoFenTimeSet(i).lZhouQiTime(j)
                                    If GaoFenTimeSet(i).ChediNum(j) > 0 Then
                                        GaoFenTimeSet(i).JGtime(j) = Int(GaoFenTimeSet(i).lZhouQiTime(j) / GaoFenTimeSet(i).ChediNum(j))
                                        GaoFenTimeSet(i).JGOne(j) = GaoFenTimeSet(i).JGtime(j)
                                        GaoFenTimeSet(i).JGTwo(j) = Time1 - GaoFenTimeSet(i).JGtime(j) * (GaoFenTimeSet(i).ChediNum(j) - 1)
                                        GaoFenTimeSet(i).NumOne(j) = GaoFenTimeSet(i).ChediNum(j) - 1
                                        GaoFenTimeSet(i).NumTwo(j) = 1
                                    End If
                                    Exit For
                                End If
                            Next j

                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If

            Case "间隔一"

                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then
                                    GaoFenTimeSet(i).JGOne(j) = MinuteToSecond(Me.grdTime.Item(14, Me.grdTime.CurrentCell.RowIndex).Value)
                                    Time1 = GaoFenTimeSet(i).lZhouQiTime(j)
                                    Time2 = GaoFenTimeSet(i).JGtime(j)
                                    If GaoFenTimeSet(i).NumTwo(j) > 0 Then
                                        GaoFenTimeSet(i).JGTwo(j) = (Time1 - GaoFenTimeSet(i).JGOne(j) * (GaoFenTimeSet(i).NumOne(j))) / GaoFenTimeSet(i).NumTwo(j)
                                    End If
                                    Exit For
                                End If
                            Next j
                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If

            Case "数量一"

                If Me.grdTime.CurrentCell.RowIndex >= 0 Then
                    For i = 1 To UBound(GaoFenTimeSet)
                        If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                                If GaoFenTimeSet(i).nXuHao(j) = Me.grdTime.Item(0, Me.grdTime.CurrentCell.RowIndex).Value Then
                                    GaoFenTimeSet(i).NumOne(j) = Me.grdTime.Item(15, Me.grdTime.CurrentCell.RowIndex).Value
                                    Time1 = GaoFenTimeSet(i).lZhouQiTime(j)
                                    Time2 = GaoFenTimeSet(i).JGtime(j)
                                    GaoFenTimeSet(i).NumTwo(j) = GaoFenTimeSet(i).ChediNum(j) - GaoFenTimeSet(i).NumOne(j)
                                    If GaoFenTimeSet(i).NumTwo(j) > 0 Then
                                        GaoFenTimeSet(i).JGTwo(j) = (Time1 - GaoFenTimeSet(i).NumOne(j) * GaoFenTimeSet(i).JGOne(j)) / GaoFenTimeSet(i).NumTwo(j)
                                    End If
                                    Exit For
                                End If
                            Next j
                            Exit For
                        End If

                    Next i
                    Call ListGrdTime()
                End If
        End Select

    End Sub

    Private Sub grdTime_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdTime.CellMouseDown
        Me.cmbEditTime.Visible = False
        CurCellX = e.X
        CurCellY = e.Y
    End Sub

    Private Sub grdTime_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdTime.MouseDown
        CurMouseX = e.X
        CurMouseY = e.Y
    End Sub

    Public Sub GZLineUseSet()
        Dim i As Integer
        Dim Tnum As Integer
        'Call GrdTime_LeaveCell
        '    For i = 1 To Me.GrdTime.Rows - 1
        '        sTime = MinuteToSecond(Me.GrdTime.TextMatrix(i, 12))
        '        If sTime < StationInf(i).IKK(1) Then
        '            MsgBox "发车间隔时间小于追踪间隔时间（" & StationInf(i).IKK(1) & "秒),请修改!!"
        '            Me.GrdTime.Row = i
        '            Me.GrdTime.Col = 4
        ''            Call GrdTime_DblClick
        '            Exit Sub
        '        End If
        '        If AddLitterTime(HourToSecond(Me.GrdTime.TextMatrix(i, 1))) > AddLitterTime(HourToSecond(Me.GrdTime.TextMatrix(i, 2))) Then
        '            MsgBox "时间段终止时间不大于起始时间,请修改!!"
        '            Me.GrdTime.Row = i
        '            Me.GrdTime.Col = 3
        '            Exit Sub
        '        End If
        '    Next i

        Tnum = Me.grdTime.RowCount
        Dim CurId As Integer
        'Dim IfExist As Inter
        For i = 1 To UBound(GaoFenTimeSet)
            If GaoFenTimeSet(i).sJLstyle = sJiaoLuStyle And GaoFenTimeSet(i).sPuHuaStyle = sPuHuaStyle Then
                CurId = i
                Exit For
            End If
        Next i

        If CurId = 0 Then
            CurId = UBound(GaoFenTimeSet) + 1
            ReDim Preserve GaoFenTimeSet(CurId)
        End If


        ReDim GaoFenTimeSet(CurId).nXuHao(0)
        ReDim GaoFenTimeSet(CurId).BeTime(0)
        ReDim GaoFenTimeSet(CurId).EndTime(0)
        ReDim GaoFenTimeSet(CurId).JGtime(0)
        ReDim GaoFenTimeSet(CurId).ChediNum(0)
        ReDim GaoFenTimeSet(CurId).JGOne(0)
        ReDim GaoFenTimeSet(CurId).JGTwo(0)
        ReDim GaoFenTimeSet(CurId).NumOne(0)
        ReDim GaoFenTimeSet(CurId).NumTwo(0)
        ReDim GaoFenTimeSet(CurId).lZhouQiTime(0)
        ReDim GaoFenTimeSet(CurId).sRunScaleName(0)
        ReDim GaoFenTimeSet(CurId).sStopScaleName(0)
        ReDim GaoFenTimeSet(CurId).lFirZheFanTime(0)
        ReDim GaoFenTimeSet(CurId).lEndZheFanTime(0)
        ReDim GaoFenTimeSet(CurId).lDownRunTime(0)
        ReDim GaoFenTimeSet(CurId).lDownStopTime(0)
        ReDim GaoFenTimeSet(CurId).lUpRunTime(0)
        ReDim GaoFenTimeSet(CurId).lUpStopTime(0)


        For i = 0 To Tnum - 2
            GaoFenTimeSet(CurId).sJLstyle = sJiaoLuStyle
            GaoFenTimeSet(CurId).sPuHuaStyle = sPuHuaStyle

            ReDim Preserve GaoFenTimeSet(CurId).nXuHao(UBound(GaoFenTimeSet(CurId).nXuHao) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).BeTime(UBound(GaoFenTimeSet(CurId).BeTime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).EndTime(UBound(GaoFenTimeSet(CurId).EndTime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).JGtime(UBound(GaoFenTimeSet(CurId).JGtime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).ChediNum(UBound(GaoFenTimeSet(CurId).ChediNum) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).JGOne(UBound(GaoFenTimeSet(CurId).JGOne) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).JGTwo(UBound(GaoFenTimeSet(CurId).JGTwo) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).NumOne(UBound(GaoFenTimeSet(CurId).NumOne) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).NumTwo(UBound(GaoFenTimeSet(CurId).NumTwo) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).lZhouQiTime(UBound(GaoFenTimeSet(CurId).lZhouQiTime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).sRunScaleName(UBound(GaoFenTimeSet(CurId).sRunScaleName) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).sStopScaleName(UBound(GaoFenTimeSet(CurId).sStopScaleName) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).lFirZheFanTime(UBound(GaoFenTimeSet(CurId).lFirZheFanTime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).lEndZheFanTime(UBound(GaoFenTimeSet(CurId).lEndZheFanTime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).lDownRunTime(UBound(GaoFenTimeSet(CurId).lDownRunTime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).lDownStopTime(UBound(GaoFenTimeSet(CurId).lDownStopTime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).lUpRunTime(UBound(GaoFenTimeSet(CurId).lUpRunTime) + 1)
            ReDim Preserve GaoFenTimeSet(CurId).lUpStopTime(UBound(GaoFenTimeSet(CurId).lUpStopTime) + 1)

            GaoFenTimeSet(CurId).nXuHao(UBound(GaoFenTimeSet(CurId).nXuHao)) = Me.grdTime.Item(0, i).Value
            GaoFenTimeSet(CurId).BeTime(UBound(GaoFenTimeSet(CurId).BeTime)) = AddLitterTime(HourToSecond(Me.grdTime.Item(1, i).Value))
            GaoFenTimeSet(CurId).EndTime(UBound(GaoFenTimeSet(CurId).EndTime)) = AddLitterTime(HourToSecond(Me.grdTime.Item(2, i).Value))

            GaoFenTimeSet(CurId).lZhouQiTime(UBound(GaoFenTimeSet(CurId).lZhouQiTime)) = MinuteToSecond(Me.grdTime.Item(3, i).Value)
            GaoFenTimeSet(CurId).sRunScaleName(UBound(GaoFenTimeSet(CurId).sRunScaleName)) = Me.grdTime.Item(4, i).Value
            GaoFenTimeSet(CurId).sStopScaleName(UBound(GaoFenTimeSet(CurId).sStopScaleName)) = Me.grdTime.Item(5, i).Value
            GaoFenTimeSet(CurId).lFirZheFanTime(UBound(GaoFenTimeSet(CurId).lFirZheFanTime)) = MinuteToSecond(Me.grdTime.Item(6, i).Value)
            GaoFenTimeSet(CurId).lEndZheFanTime(UBound(GaoFenTimeSet(CurId).lEndZheFanTime)) = MinuteToSecond(Me.grdTime.Item(7, i).Value)
            GaoFenTimeSet(CurId).lDownRunTime(UBound(GaoFenTimeSet(CurId).lDownRunTime)) = MinuteToSecond(Me.grdTime.Item(8, i).Value)
            GaoFenTimeSet(CurId).lDownStopTime(UBound(GaoFenTimeSet(CurId).lDownStopTime)) = MinuteToSecond(Me.grdTime.Item(9, i).Value)
            GaoFenTimeSet(CurId).lUpRunTime(UBound(GaoFenTimeSet(CurId).lUpRunTime)) = MinuteToSecond(Me.grdTime.Item(10, i).Value)
            GaoFenTimeSet(CurId).lUpStopTime(UBound(GaoFenTimeSet(CurId).lUpStopTime)) = MinuteToSecond(Me.grdTime.Item(11, i).Value)

            GaoFenTimeSet(CurId).JGtime(UBound(GaoFenTimeSet(CurId).JGtime)) = MinuteToSecond(Me.grdTime.Item(12, i).Value)
            GaoFenTimeSet(CurId).ChediNum(UBound(GaoFenTimeSet(CurId).ChediNum)) = Val(Me.grdTime.Item(13, i).Value)
            GaoFenTimeSet(CurId).JGOne(UBound(GaoFenTimeSet(CurId).JGOne)) = MinuteToSecond(Me.grdTime.Item(14, i).Value)
            GaoFenTimeSet(CurId).NumOne(UBound(GaoFenTimeSet(CurId).NumOne)) = Val(Me.grdTime.Item(15, i).Value)
            GaoFenTimeSet(CurId).JGTwo(UBound(GaoFenTimeSet(CurId).JGTwo)) = MinuteToSecond(Me.grdTime.Item(16, i).Value)
            GaoFenTimeSet(CurId).NumTwo(UBound(GaoFenTimeSet(CurId).NumTwo)) = Val(Me.grdTime.Item(17, i).Value)

        Next i

        'Call ShowCharPic()

    End Sub

    Private Sub cmbEditTime_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEditTime.SelectedValueChanged
        Me.grdTime.CurrentCell.Value = Me.cmbEditTime.Text
        Call ValueChange()
        Me.cmbEditTime.Visible = False
    End Sub

    Private Sub cmbTrainJLStyle_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTrainJLStyle.SelectedValueChanged
        sJiaoLuStyle = Trim(Me.cmbTrainJLStyle.Text)
        sPuHuaStyle = Trim(Me.cmbPuHuaStyle.Text)
        Me.grdTime.Rows.Clear()
        Call GetBaseStation(sJiaoLuStyle)
        Call InputJGData()
    End Sub

    Private Sub cmdShowFangA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdShowFangA.Click
        ' frmTimeTableMain.约束铺画YToolStripMenuItem.Checked = False
        TimeTablePara.sPubTrainStrainDraw = TrainStrainDraw.无约束
        TimeTablePara.nPubTrain = 0
        TimeTablePara.nPubCheDi = 0
        ReDim TimeTablePara.nPubTrains(0)
        TimeTablePara.sPubCurSkbName = "未命名时刻表"
        'Call showInformation(sJiaoLuStyle)
        Call InputFirstCan(sJiaoLuStyle)
        Call GZLineUseSet()
        Call SetAllTrainNotGouHua()

        Me.StatusLabel.Text = "正在铺画"
        Me.ProBar.Value = 0
        Me.ProBar.Visible = True
        Call AllChediGaoFengBackGouHuaByCircle(sJiaoLuStyle, sFanJiaoLuStyle, sPuHuaStyle, Me.ProBar)

        Me.ProBar.Visible = False
        Me.StatusLabel.Text = "运行图铺画"
        Call ResetPrintTrainString()
        Call addOneUndoInf()
        Call RefreshDiagram(1)
        Call ShowAllErrorInfor()
    End Sub

    Private Sub InputFirstCan(ByVal sJLstyle As String)

        sYXTJiaoLuStyle = sJiaoLuStyle
        sYXTPuHuaStyle = sPuHuaStyle
        BaseTimeMinus = 0
        ElseTimeMinus = 0

        ReDim BasePrepTrainset.nTrain(0)
        ReDim BasePrepTrainset.sCdid(0)

        ReDim ElsePrepTrainset.nTrain(0)
        ReDim ElsePrepTrainset.sCdid(0)

        'Call SetChuRuKuState(sJLstyle)

        If Me.chkAutoAddChuKu.Checked = True Then
            TimeTablePara.bIFAutoAddChuKuTrain = True
        Else
            TimeTablePara.bIFAutoAddChuKuTrain = False
        End If

        If Me.chkAutoAddRuKu.Checked = True Then
            TimeTablePara.bIFAutoAddRuKuTrain = True
        Else
            TimeTablePara.bIFAutoAddRuKuTrain = False
        End If
    End Sub

    '先将所有列车清为没有勾画,车底清为空闲
    Private Sub SetAllTrainNotGouHua()
        Dim i As Integer
        ReDim ChediMultiJLInfo(0)
        ReDim ChediMultiJLInfo(0).nLinkTrain(0)

        ReDim tmpCheDiInf(0)
        ReDim tmpCheDiInf(0).nLinkTrain(0)
        ReDim ChediInfo(0)
        For i = 1 To UBound(BaseChediInfo)
            BaseChediInfo(i).bIfGouWang = 0
        Next i
        ReDim TrainInf(0)
        ReDim nAllTrainSeq(0)
        ReDim ErrorInf(0)
        ReDim TrainErrInf(0)
    End Sub

    Private Sub btnSaveFangAn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveFangAn.Click
        Call GZLineUseSet()
        If MsgBox("是否将修改后的信息保存到基础数据库中?", vbQuestion + vbOKCancel + vbDefaultButton2, "确认操作") = vbOK Then
            Call InputToDatabase()
        End If
    End Sub

    '保存间隔方案
    Private Sub InputToDatabase()
        Dim DBChediJiche As dao.Database
        Dim RSdata As dao.Recordset
        Dim i, j As Integer
        Dim Tnum As Integer
        Dim myWS As dao.Workspace
        Dim DE As dao.DBEngine = New dao.DBEngine

        myWS = DE.Workspaces(0)
        DBChediJiche = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        RSdata = DBChediJiche.OpenRecordset("select * from 发车间隔安排")
        If RSdata.RecordCount > 0 Then
            RSdata.MoveLast()
            Tnum = RSdata.RecordCount
        Else
            Tnum = 0
        End If
        If Tnum > 0 Then
            DBChediJiche.Execute("delete * from 发车间隔安排")
        End If
        DBChediJiche.Close()
        DBChediJiche = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        RSdata = DBChediJiche.OpenRecordset("发车间隔安排")

        For i = 1 To UBound(GaoFenTimeSet)
            For j = 1 To UBound(GaoFenTimeSet(i).nXuHao)
                RSdata.AddNew()
                RSdata.Fields("交路类型").Value = GaoFenTimeSet(i).sJLstyle
                RSdata.Fields("铺画类型").Value = GaoFenTimeSet(i).sPuHuaStyle
                RSdata.Fields("时间段").Value = GaoFenTimeSet(i).nXuHao(j)
                RSdata.Fields("起始时间").Value = dTime(DeleteLitterTime(GaoFenTimeSet(i).BeTime(j)), 5)
                RSdata.Fields("终止时间").Value = dTime(DeleteLitterTime(GaoFenTimeSet(i).EndTime(j)), 5)
                RSdata.Fields("发车间隔").Value = SecondToMinute(GaoFenTimeSet(i).JGtime(j))
                RSdata.Fields("车底数量").Value = GaoFenTimeSet(i).ChediNum(j)
                RSdata.Fields("间隔一").Value = SecondToMinute(GaoFenTimeSet(i).JGOne(j))
                RSdata.Fields("间隔二").Value = SecondToMinute(GaoFenTimeSet(i).JGTwo(j))
                RSdata.Fields("数量一").Value = GaoFenTimeSet(i).NumOne(j)
                RSdata.Fields("数量二").Value = GaoFenTimeSet(i).NumTwo(j)
                RSdata.Fields("周期时间").Value = Val(GaoFenTimeSet(i).lZhouQiTime(j))
                RSdata.Fields("运行标尺").Value = GaoFenTimeSet(i).sRunScaleName(j)
                RSdata.Fields("停站标尺").Value = GaoFenTimeSet(i).sStopScaleName(j)
                RSdata.Fields("始发折返").Value = Val(GaoFenTimeSet(i).lFirZheFanTime(j))
                RSdata.Fields("终到折返").Value = Val(GaoFenTimeSet(i).lEndZheFanTime(j))
                RSdata.Update()
            Next j
        Next i

        RSdata.Close()
        DBChediJiche.Close()
        MsgBox("保存完毕!", , "提示")
    End Sub

    Private Sub btnOutExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutExcel.Click
        Call OutPutToEXCELFileFormDataGrid(Me.cmbTrainJLStyle.Text & "——" & Me.cmbPuHuaStyle.Text & "时间间隔方案", Me.grdTime, Me)
    End Sub

    Private Sub btnConDraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConDraw.Click
        Dim i As Integer
        Call InputFirstCan(sJiaoLuStyle)
        Call GZLineUseSet()
        ReDim ChediMultiJLInfo(0)
        Call SaveTmpCheDiInfor()
        ReDim tmpCheDiInf(0)
        ReDim tmpCheDiInf(0).nLinkTrain(0)
        ReDim ChediInfo(0)
        ReDim ChediInfo(0).nLinkTrain(0)
        For i = 1 To UBound(BaseChediInfo)
            BaseChediInfo(i).bIfGouWang = 0
        Next i
        ReDim nAllTrainSeq(0)
        ReDim ErrorInf(0)

        Me.StatusLabel.Text = "正在计算方案"
        Me.ProBar.Visible = True

        Call AllChediGaoFengBackGouHuaByCircle(sJiaoLuStyle, sFanJiaoLuStyle, sPuHuaStyle, Me.ProBar)

        Call SaveTmpCheDiInfor()
        Call InputtmpCDInformation()

        Call AllCheDiDrawTrainJiaoLu()
        Call addOneUndoInf()

        Me.ProBar.Visible = False
        Me.StatusLabel.Text = "运行图铺画"
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class