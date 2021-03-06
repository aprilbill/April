Imports System.Math
Public Class frmCADStation
    Dim nTrackID As Integer
    Dim nSignalID As Integer
    Dim nPlatformID As Integer
    Dim nFontTextID As Integer
    Dim sEquipID As String

    Dim nDrawState As Integer
    Dim nClickState As Integer
    Dim nEditState As Integer

    Dim nEditLeftOrRightCoord As Integer

    Dim nCADStainfID As Integer
    Dim sCADFormStaName As String
    Dim sCADFormLineName As String
    Dim nCADFormLineID As Integer
    Dim nCADFormStaID As Integer
    Dim sngScale As Single

    Dim sngClickX As Single
    Dim sngClickY As Single
    Dim sngX1 As Single
    Dim sngY1 As Single
    Dim sngX2 As Single
    Dim sngY2 As Single

    Public nCurPicstaLeftX As Single
    Public nCurPicStaTopY As Single
    Public nCurShowScale As Single '显示比例
    Dim bmpOne As Bitmap '
    Dim bmpTwo As Bitmap '
    Dim PicMoveX As Single
    Dim PicMoveY As Single
    Dim nTrackInfID As Integer

    Dim ntmpCurMoveX As Single
    Dim ntmpCurMoveY As Single
    Dim ntmpTrackInfID As Integer
    Dim sngZoomOrReduceValue As Single

    Dim nIfPreseCtrl As Integer
    Dim CurRotateCenterPoint As Point
    Dim nCurRotateAngle As Single
    Dim nForRoteAngle As Single
    Dim curRotateX As Single
    Dim curRotateY As Single

    Private Sub frmCADStation_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim sResult As MsgBoxResult
        sResult = MsgBox("如果已经作修改请保存，确认保存修改吗?", MsgBoxStyle.Question + MsgBoxStyle.YesNoCancel + MsgBoxStyle.DefaultButton1, "保存修改")
        If sResult = MsgBoxResult.Yes Then
            Call InputCADstaInfToDataBase()
        ElseIf sResult = MsgBoxResult.Cancel Then
            e.Cancel = True
        End If
    End Sub

    '判断是否按住Crtl键
    Private Sub frmCADStation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.ControlKey Then
            nIfPreseCtrl = 1
        ElseIf e.KeyCode = Keys.ShiftKey Then
            nIfPreseCtrl = 2
        End If
    End Sub

    'Private Sub frmCADStation_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
    '    If e.KeyCode = Keys.LControlKey Or e.KeyCode = Keys.RControlKey Then
    '        nIfPreseCtrl = 1
    '    End If
    'End Sub

    Private Sub frmCADStation_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.ControlKey Then
            nIfPreseCtrl = 0
        ElseIf e.KeyCode = Keys.ShiftKey Then
            nIfPreseCtrl = 0
        End If
    End Sub


    Private Sub frmCADPlatform_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Call InputStationAndSectionDataToCADStainf()
        If UBound(CADStaInf) = 0 Then
            MsgBox("还没有添加车站，请先添加车站！", , "提示")
            Exit Sub
        End If
        Call frmCADStation_Resize(Nothing, Nothing)
        Me.picLine.Width = 200
        Me.picLine.Height = 200
        Me.picSta.Left = 0
        Me.picSta.Top = 0
        Me.picLine.Left = Me.picSta.Width - Me.picLine.Width
        Me.picLine.Top = Me.picSta.Height - Me.picSta.Height
        sngScale = 0.3
        Call listTreeViewDataInCADform(Me.trvLine)
        Call InputAllDataToCADstaInf(proBar)
        Call InputControModelData()
        'nCADStainfID = 1
        'nCADFormLineID = 1
        'nCADFormStaID = 1
        'sCADFormStaName = CADStaInf(nCADFormLineID).sAtLine
        'sCADFormLineName = CADStaInf(nCADFormLineID).sStaName
        'Call InputDataToCADstaInf(nCADStainfID)
        'Call DrawStaPic()
        nFirGridWidth = 20
        nGridWidth = nFirGridWidth
        Me.picSta.Width = Me.SplitContainer4.Panel1.Width
        Me.picSta.Height = Me.SplitContainer4.Panel1.Height
        nCurPicstaLeftX = 0
        nCurPicStaTopY = 0
        nCurShowScale = 1
        sngZoomOrReduceValue = 0.15
        nTrackInfID = 0
        ntmpTrackInfID = 0
        nCADStainfID = 0
        Me.KeyPreview = True
        nIfPreseCtrl = 0
        nForRoteAngle = 0
        ReDim CopyTrackinf(0)
        ReDim curSelectTrackID(0)
        ReDim tmpRotateTrackInf(0)
        '  Call RefreshPicSta(nCurPicstaLeftX, nCurPicStaTopY)
        Dim i As Integer
        If UBound(CADStaInf) > 0 Then
            nCADStainfID = 1
            'Call ShowCurSelectStaPic(1)
            Me.tolStripCmbShowScale.Text = "适应页面"
        End If
        CADformPara.nMaxUndoID = 10
        CADUndoSeq.nCurUndoID = 0
        ReDim CADformUndoInf(CADformPara.nMaxUndoID)
        ReDim CADformUndoInf2(CADformPara.nMaxUndoID)
        For i = 1 To UBound(CADformUndoInf)
            CADformUndoInf(i).nXuHao = i
            ReDim CADformUndoInf(i).CADStaInfor(0)
        Next
        Call CADaddOneUndoInf()
        Call InputGridXY()
        'If SystemPara.SystemStyle = "磁浮" Then
        '    Me.tolbarSetControlModel.Visible = True
        '    Me.控制方式ToolStripButton1.Visible = True
        '    Me.设定控制模块CToolStripMenuItem.Visible = True
        '    Me.添加控制方式AToolStripMenuItem.Visible = True
        '    Me.控制方式tolCmbControlScheme.Visible = True
        '    Me.ToolStripLabel2.Visible = True
        'Else
        '    Me.tolbarSetControlModel.Visible = False
        '    Me.控制方式ToolStripButton1.Visible = False
        '    Me.设定控制模块CToolStripMenuItem.Visible = False
        '    Me.添加控制方式AToolStripMenuItem.Visible = False
        '    Me.控制方式tolCmbControlScheme.Visible = False
        '    Me.ToolStripLabel2.Visible = False
        'End If
    End Sub

    '导入网络各点坐标
    Private Sub InputGridXY()
        GridXYCord.Clear()
        Dim nRows As Integer
        Dim nColums As Integer
        Dim i, j As Integer
        nRows = Me.picSta.Width / nGridWidth
        nColums = Me.picSta.Height / nGridWidth
        Dim nLeft As Integer
        Dim nTop As Integer
        nLeft = (nCurPicstaLeftX Mod nFirGridWidth) * nCurShowScale
        nTop = (nCurPicStaTopY Mod nFirGridWidth) * nCurShowScale
        For i = 1 To nColums + 1
            For j = 1 To nRows + 1
                Dim tmpGdXY As New GridInf
                tmpGdXY.X = (i - 1) * nGridWidth - nLeft
                tmpGdXY.Y = (j - 1) * nGridWidth - nTop
                GridXYCord.Add(tmpGdXY)
            Next
        Next
    End Sub

    Private Sub picSta_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picSta.MouseClick
        Call GetGridXY(e.X, e.Y)
        Select Case nDrawState
            Case 0

            Case 1 '画线
                If nClickState = 1 And nClickState = 1 And e.Button = Windows.Forms.MouseButtons.Left Then
                    sngX1 = GridXY.X
                    sngY1 = GridXY.Y
                    nClickState = 2
                ElseIf nClickState = 2 And nClickState = 2 And e.Button = Windows.Forms.MouseButtons.Left Then
                    sngX2 = GridXY.X
                    sngY2 = GridXY.Y

                    Dim tmpX1, tmpX2, tmpY1, tmpY2 As Single
                    tmpX1 = (sngX1) / nCurShowScale + nCurPicstaLeftX
                    tmpY1 = (sngY1) / nCurShowScale + nCurPicStaTopY
                    tmpX2 = (sngX2) / nCurShowScale + nCurPicstaLeftX
                    tmpY2 = (sngY2) / nCurShowScale + nCurPicStaTopY

                    'Dim g As Drawing.Graphics
                    'Me.picSta.Refresh()
                    'g = Me.picSta.CreateGraphics
                    'g.DrawLine(Pens.Yellow, sngX1, sngY1, sngX2, sngY2)
                    Call addCADStaTrack(nCADStainfID, "连接线", "", "", "0", 10, "", tmpX1, tmpY1, tmpX2, tmpY2, "", "", "", "", "", "", "", "1", "")
                    Call DrawStaPic(Me.picSta, Me.picLine)
                    Call CADaddOneUndoInf()

                    'nDrawState = 0
                    nClickState = 1
                    'Me.Cursor = Cursors.Default
                    sngX1 = 0
                    sngY1 = 0

                End If

            Case 2 '画信号机

                sngX1 = GridXY.X
                sngY1 = GridXY.Y
                Call addCADStaSignal(nCADStainfID, "", 0, 0, sngX1, sngY1, "")
                nDrawState = 0
                Call DrawStaPic(Me.picSta, Me.picLine)

            Case 3 '画站台

            Case 4 '写文字
                sngX1 = GridXY.X
                sngY1 = GridXY.Y

                ReDim stuListItem(9)
                stuListItem(1).strItem = "文字内容"
                stuListItem(1).strStyle = PropStrStyle.TexBox
                stuListItem(1).strTxtList = CADStaInf(nCADStainfID).sStaName '"请在这里输入文字内容"
                stuListItem(1).strItemCriterion = TextCriterion.NotEmpty

                stuListItem(2).strItem = "字体名称"
                stuListItem(2).strStyle = PropStrStyle.TexBox
                stuListItem(2).strTxtList = "Arial"
                stuListItem(2).strItemCriterion = TextCriterion.NotRequired

                stuListItem(3).strItem = "字体大小"
                ReDim Preserve stuListItem(3).StrCmbList(20)
                Dim i As Integer
                For i = 1 To 20
                    stuListItem(3).StrCmbList(i) = i.ToString
                Next
                stuListItem(3).strStyle = PropStrStyle.ComBox
                stuListItem(3).strTxtList = "10"
                stuListItem(3).strItemCriterion = TextCriterion.NotRequired

                stuListItem(4).strItem = "字体斜体"
                ReDim Preserve stuListItem(4).StrCmbList(2)
                stuListItem(4).StrCmbList(1) = "True"
                stuListItem(4).StrCmbList(2) = "False"
                stuListItem(4).strStyle = PropStrStyle.ComBox
                stuListItem(4).strTxtList = "False"
                stuListItem(4).strItemCriterion = TextCriterion.NotRequired

                stuListItem(5).strItem = "字体粗体"
                ReDim Preserve stuListItem(5).StrCmbList(2)
                stuListItem(5).StrCmbList(1) = "True"
                stuListItem(5).StrCmbList(2) = "False"
                stuListItem(5).strStyle = PropStrStyle.ComBox
                stuListItem(5).strTxtList = "False"
                stuListItem(5).strItemCriterion = TextCriterion.NotRequired

                stuListItem(6).strItem = "字体颜色"
                stuListItem(6).strStyle = PropStrStyle.colorBox
                stuListItem(6).strTxtList = Color.White.ToArgb
                stuListItem(6).strItemCriterion = TextCriterion.NotRequired

                stuListItem(7).strItem = "X坐标"
                stuListItem(7).strStyle = PropStrStyle.TexBox
                stuListItem(7).strTxtList = sngX1
                stuListItem(7).strItemCriterion = TextCriterion.NotRequired

                stuListItem(8).strItem = "Y坐标"
                stuListItem(8).strStyle = PropStrStyle.TexBox
                stuListItem(8).strTxtList = sngY1
                stuListItem(8).strItemCriterion = TextCriterion.NotRequired

                stuListItem(9).strItem = "备注"
                stuListItem(9).strStyle = PropStrStyle.TexBox
                stuListItem(9).strTxtList = ""
                stuListItem(9).strItemCriterion = TextCriterion.NotRequired

                Dim nf2 As New frmEditDataProperity

                nf2.ShowDialog()
                If nf2.blnOK = True Then
                    Call addCADStaFontText(nCADStainfID, _
                                        stuListItem(1).strReturnValue, _
                                        stuListItem(2).strReturnValue, _
                                        stuListItem(3).strReturnValue, _
                                        stuListItem(4).strReturnValue, _
                                        stuListItem(5).strReturnValue, _
                                        stuListItem(6).strReturnValue, _
                                        stuListItem(7).strReturnValue, _
                                        stuListItem(8).strReturnValue, _
                                        stuListItem(9).strReturnValue)
                    Call DrawStaPic(Me.picSta, Me.picLine)
                End If
                ' Me.tolbtDrawString.Checked = False
                Me.picSta.Cursor = Cursors.Default
                nDrawState = 0

        End Select
        'MsgBox(e.X)
    End Sub

    Private Sub picSta_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picSta.MouseDown

        Call GetGridXY(e.X, e.Y)
        Dim i As Integer
        If e.Button = Windows.Forms.MouseButtons.Left Then
            sngClickX = GridXY.X
            sngClickY = GridXY.Y
            'sngClickX = e.X
            'sngClickY = e.Y
            Select Case nDrawState
                Case 0
                    If nEditState = 2 Then  '修改坐标时单击 
                        If Me.picSta.Cursor = Cursors.Cross Then

                        Else
                            nTrackID = GetTrackID(nCADStainfID, sngClickX, sngClickY)
                            sEquipID = GetEquipID(e.X, e.Y)
                            If sEquipID <> "NULL" And sEquipID <> "" Then
                                Select Case sEquipID.Substring(0, 1)
                                    Case "T" '选中线段
                                        ntmpTrackInfID = Val(sEquipID.Substring(1))
                                        ReDim curSelectTrackID(1)
                                        curSelectTrackID(1) = ntmpTrackInfID
                                        Call SelectLineStyle(1)
                                        nEditState = 0
                                    Case Else
                                        ReDim curSelectTrackID(0)
                                        Me.picSta.Refresh()
                                        nEditState = 0
                                End Select
                            End If
                        End If
                    ElseIf nEditState = 0 Then '选择图形
                        sEquipID = GetEquipID(e.X, e.Y)
                        If sEquipID <> "NULL" And sEquipID <> "" Then
                            Select Case sEquipID.Substring(0, 1)
                                Case "T" '选中线段
                                    Dim ifIn As Integer
                                    ifIn = 0
                                    If nIfPreseCtrl = 1 Then '按住CTRL选择
                                        ntmpTrackInfID = Val(sEquipID.Substring(1))
                                        If ntmpTrackInfID > 0 Then
                                            For i = 1 To UBound(curSelectTrackID)
                                                If curSelectTrackID(i) = ntmpTrackInfID Then
                                                    ifIn = 1
                                                    Exit For
                                                End If
                                            Next
                                            If ifIn = 0 Then
                                                ReDim Preserve curSelectTrackID(UBound(curSelectTrackID) + 1)
                                                curSelectTrackID(UBound(curSelectTrackID)) = ntmpTrackInfID
                                            End If
                                        End If
                                        Call SelectLineStyle(1)
                                    Else
                                        ntmpTrackInfID = Val(sEquipID.Substring(1))
                                        ReDim curSelectTrackID(1)
                                        curSelectTrackID(1) = ntmpTrackInfID
                                        Call SelectLineStyle(1)
                                    End If
                                    nEditState = 0
                                    If UBound(curSelectTrackID) = 1 Then
                                        ntmpTrackInfID = curSelectTrackID(1)
                                        nTrackID = tmpTrackInf(ntmpTrackInfID).nTrackID
                                        nCADStainfID = tmpTrackInf(ntmpTrackInfID).nStaID
                                        Me.txtInfor.Text = "当前线段ID：" & nTrackID & vbCrLf & _
                                                           "所属车站：" & CADStaInf(nCADStainfID).Track(nTrackID).sStaName & vbCrLf & _
                                                           "左一连接：" & CADStaInf(nCADStainfID).Track(nTrackID).sLeftLink1 & vbCrLf & _
                                                           "左二连接：" & CADStaInf(nCADStainfID).Track(nTrackID).sLeftLink2 & vbCrLf & _
                                                           "左三连接：" & CADStaInf(nCADStainfID).Track(nTrackID).sLeftLink3 & vbCrLf & _
                                                           "右一连接：" & CADStaInf(nCADStainfID).Track(nTrackID).sRightLink1 & vbCrLf & _
                                                           "右二连接：" & CADStaInf(nCADStainfID).Track(nTrackID).sRightLink2 & vbCrLf & _
                                                           "右三连接：" & CADStaInf(nCADStainfID).Track(nTrackID).sRightLink3 & vbCrLf & _
                                                           "轨道电路编号：" & CADStaInf(nCADStainfID).Track(nTrackID).sTrackCircuitNum & vbCrLf & _
                                                           "股道编号:" & CADStaInf(nCADStainfID).Track(nTrackID).sTrackNum & vbCrLf & _
                                                           "股道用途:" & CADStaInf(nCADStainfID).Track(nTrackID).sGuDaoYongTu & vbCrLf & _
                                                           "控制模块:" & CADStaInf(nCADStainfID).Track(nTrackID).sControlNum & vbCrLf & _
                                                           "线段类型:" & CADStaInf(nCADStainfID).Track(nTrackID).sStyle
                                        Me.Text = CADStaInf(nCADStainfID).Track(nTrackID).sStaName & " 车站平面图"
                                    End If
                                Case "S" '选中信号机
                                    nSignalID = Val(sEquipID.Substring(1))
                                    Call SelectSignalStyle(nSignalID)
                                Case "P" '选中站台
                                    nPlatformID = Val(sEquipID.Substring(1))
                                    Call SelectPlatformStyle(nPlatformID)
                                Case "E" '选中字体
                                    nFontTextID = Val(sEquipID.Substring(1))
                                    Call SelectFontTextStyle(nFontTextID)
                            End Select

                        Else
                            ReDim curSelectTrackID(0)
                            Me.picSta.Refresh()
                            nEditState = 0
                            Me.picSta.ContextMenuStrip = Nothing
                        End If
                    ElseIf nEditState = 6 Then '旋转
                        ReDim tmpRotateTrackInf(UBound(curSelectTrackID))
                        For i = 1 To UBound(curSelectTrackID)
                            tmpRotateTrackInf(i).nStaID = tmpTrackInf(curSelectTrackID(i)).nStaID
                            tmpRotateTrackInf(i).nTrackID = tmpTrackInf(curSelectTrackID(i)).nTrackID
                            tmpRotateTrackInf(i).X1 = tmpTrackInf(curSelectTrackID(i)).X1
                            tmpRotateTrackInf(i).Y1 = tmpTrackInf(curSelectTrackID(i)).Y1
                            tmpRotateTrackInf(i).X2 = tmpTrackInf(curSelectTrackID(i)).X2
                            tmpRotateTrackInf(i).Y2 = tmpTrackInf(curSelectTrackID(i)).Y2
                        Next
                        Me.TimerMovePic.Enabled = True
                    End If
                Case 1

                Case 5 '移动底图
                    If e.Button = Windows.Forms.MouseButtons.Left Then
                        PicMoveX = 0
                        PicMoveY = 0
                        ntmpCurMoveX = GridXY.X
                        ntmpCurMoveY = GridXY.Y
                    End If
                    Me.TimerMovePic.Enabled = True
                Case 6 '多组选择
                    nEditState = 2
            End Select
        End If
    End Sub

    Private Sub picSta_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picSta.MouseMove
        Dim g As Drawing.Graphics
        g = Me.picSta.CreateGraphics
        Dim MoveX, MoveY As Single
        Dim MoveX1, MoveX2, MoveY1, MoveY2 As Single
        Dim i As Integer
        Dim tmpID As Integer
        Dim nCurLineWidth As Single
        Select Case nDrawState
            Case 0
                Select Case nEditState
                    Case 1 '移动线段
                        If e.Button = Windows.Forms.MouseButtons.Left And UBound(curSelectTrackID) > 0 And nEditState = 1 Then
                            Call GetGridXY(e.X, e.Y)
                            MoveX = GridXY.X - sngClickX
                            MoveY = GridXY.Y - sngClickY
                            Me.picSta.Refresh()
                            For i = 1 To UBound(curSelectTrackID)
                                tmpID = curSelectTrackID(i)
                                MoveX1 = tmpTrackInf(tmpID).X1 + MoveX
                                MoveY1 = tmpTrackInf(tmpID).Y1 + MoveY
                                MoveX2 = tmpTrackInf(tmpID).X2 + MoveX
                                MoveY2 = tmpTrackInf(tmpID).Y2 + MoveY
                                nCurLineWidth = GetShowLineWidth(4, nCurShowScale)
                                g.DrawLine(New Pen(Color.Red, nCurLineWidth), MoveX1, MoveY1, MoveX2, MoveY2)
                            Next
                        End If
                    Case 2 '修改线段坐标
                        If e.Button = Windows.Forms.MouseButtons.Left Then
                            If nEditLeftOrRightCoord = 1 Then
                                Call GetGridXY(e.X, e.Y)
                                MoveX = GridXY.X
                                MoveY = GridXY.Y

                                MoveX1 = MoveX
                                MoveY1 = MoveY
                                MoveX2 = tmpTrackInf(ntmpTrackInfID).X2
                                MoveY2 = tmpTrackInf(ntmpTrackInfID).Y2
                                If nIfPreseCtrl = 1 Then '按住CTRL
                                    MoveX1 = MoveX2
                                ElseIf nIfPreseCtrl = 2 Then '按住Shift
                                    MoveY1 = MoveY2
                                End If
                                Me.picSta.Refresh()
                                nCurLineWidth = GetShowLineWidth(4, nCurShowScale)
                                g.DrawLine(New Pen(Color.Red, nCurLineWidth), MoveX1, MoveY1, MoveX2, MoveY2)

                            ElseIf nEditLeftOrRightCoord = 2 Then
                                Call GetGridXY(e.X, e.Y)
                                MoveX = GridXY.X
                                MoveY = GridXY.Y

                                MoveX1 = tmpTrackInf(ntmpTrackInfID).X1
                                MoveY1 = tmpTrackInf(ntmpTrackInfID).Y1
                                MoveX2 = MoveX
                                MoveY2 = MoveY
                                If nIfPreseCtrl = 1 Then '按住CTRL
                                    MoveX2 = MoveX1
                                ElseIf nIfPreseCtrl = 2 Then '按住Shift
                                    MoveY2 = MoveY1
                                End If
                                Me.picSta.Refresh()
                                nCurLineWidth = GetShowLineWidth(4, nCurShowScale)
                                g.DrawLine(New Pen(Color.Red, nCurLineWidth), MoveX1, MoveY1, MoveX2, MoveY2)
                            End If
                        Else
                            Dim X1, X2, Y1, Y2 As Single
                            X1 = tmpTrackInf(ntmpTrackInfID).X1
                            Y1 = tmpTrackInf(ntmpTrackInfID).Y1
                            X2 = tmpTrackInf(ntmpTrackInfID).X2
                            Y2 = tmpTrackInf(ntmpTrackInfID).Y2
                            If e.X >= X1 - 5 And e.X <= X1 + 5 And e.Y >= Y1 - 5 And e.Y <= Y1 + 5 Then
                                nEditLeftOrRightCoord = 1 '左边
                                Me.picSta.Cursor = Cursors.Cross
                            ElseIf e.X >= X2 - 5 And e.X <= X2 + 5 And e.Y >= Y2 - 5 And e.Y <= Y2 + 5 Then
                                nEditLeftOrRightCoord = 2 '左边
                                Me.picSta.Cursor = Cursors.Cross
                            Else
                                nEditLeftOrRightCoord = 0 '
                                Me.picSta.Cursor = Cursors.Default
                            End If

                        End If
                    Case 3 '移动信号机
                        If e.Button = Windows.Forms.MouseButtons.Left Then
                            Call GetGridXY(e.X, e.Y)
                            sngX2 = GridXY.X
                            sngY2 = GridXY.Y
                            Me.picSta.Refresh()
                            If sngX2 = 0 Or sngY2 = 0 Then
                            Else
                                Call PrintSignalByStyle(CADStaInf(nCADStainfID).Signal(nSignalID).sStyle, sngX2, sngY2, g, Color.Red, 1)
                            End If

                        End If
                    Case 4 '移动站台

                        If e.Button = Windows.Forms.MouseButtons.Left Then
                            Call GetGridXY(e.X, e.Y)
                            MoveX = GridXY.X - sngClickX
                            MoveY = GridXY.Y - sngClickY
                            MoveX1 = CADStaInf(nCADStainfID).PlatForm(nPlatformID).X1 + MoveX - sngLeftBaseX
                            MoveY1 = CADStaInf(nCADStainfID).PlatForm(nPlatformID).Y1 + MoveY
                            MoveX2 = CADStaInf(nCADStainfID).PlatForm(nPlatformID).X2 + MoveX - sngLeftBaseX
                            MoveY2 = CADStaInf(nCADStainfID).PlatForm(nPlatformID).Y2 + MoveY
                            Me.picSta.Refresh()
                            If MoveX1 > 0 And MoveY1 > 0 Then
                                g.DrawRectangle(New Pen(Color.Red, 2), MoveX1, MoveY1, MoveX2 - MoveX1, MoveY2 - MoveY1)
                            End If
                        End If

                    Case 5 '移动字体
                        If e.Button = Windows.Forms.MouseButtons.Left Then
                            Call GetGridXY(e.X, e.Y)

                            MoveX = GridXY.X - sngClickX
                            MoveY = GridXY.Y - sngClickY
                            MoveX1 = CADStaInf(nCADStainfID).FontText(nFontTextID).X + MoveX - sngLeftBaseX
                            MoveY1 = CADStaInf(nCADStainfID).FontText(nFontTextID).Y + MoveY

                            Dim sText As String
                            Dim FontName As String
                            Dim FontSize As Single
                            Dim FontColor As Color
                            sText = CADStaInf(nCADStainfID).FontText(nFontTextID).sText
                            FontSize = CADStaInf(nCADStainfID).FontText(nFontTextID).FontSize
                            FontName = CADStaInf(nCADStainfID).FontText(nFontTextID).FontName
                            FontColor = Color.Red 'CADStaInf(nCADStainfID).FontText(nFontTextID).FontColor
                            Dim s As New FontStyle
                            s = FontStyle.Underline
                            Dim b As New SolidBrush(FontColor)
                            Dim f As New Font(FontName, FontSize, s)
                            Me.picSta.Refresh()
                            If MoveX1 > 0 And MoveY1 > 0 Then
                                g.DrawString(sText, f, b, MoveX1, MoveY1)
                            End If
                        End If
                    Case 6 '旋转线段
                        Call GetGridXY(e.X, e.Y)
                        sngX2 = GridXY.X
                        sngY2 = GridXY.Y
                        If Me.picSta.Cursor = Cursors.SizeNS And e.Button = Windows.Forms.MouseButtons.Left Then
                            nCurRotateAngle = GetSngAngle(CurRotateCenterPoint.X, CurRotateCenterPoint.Y, sngClickX, sngClickY, sngX2, sngY2)
                        End If
                End Select
            Case 1
                '画直线
                If nClickState = 1 Then '画线时
                    Call GetGridXY(e.X, e.Y)
                    sngX2 = GridXY.X
                    sngY2 = GridXY.Y
                    Me.picSta.Refresh()
                    If sngX2 = 0 Or sngY2 = 0 Then
                    Else
                        g.DrawEllipse(New Pen(Color.Yellow, 1), sngX2 - 5, sngY2 - 5, 10, 10)
                    End If

                ElseIf nClickState = 2 Then '修改坐标时
                    Call GetGridXY(e.X, e.Y)
                    sngX2 = GridXY.X
                    sngY2 = GridXY.Y
                    Me.picSta.Refresh()
                    If sngX2 = 0 Or sngY2 = 0 Then
                    Else
                        g.DrawEllipse(New Pen(Color.Yellow, 1), sngX2 - 5, sngY2 - 5, 10, 10)
                        g.DrawLine(Pens.Yellow, sngX1, sngY1, sngX2, sngY2)
                    End If
                End If

            Case 2 '画信号机

                Call GetGridXY(e.X, e.Y)
                sngX2 = GridXY.X
                sngY2 = GridXY.Y
                Me.picSta.Refresh()
                If sngX2 = 0 Or sngY2 = 0 Then
                Else
                    g.DrawLine(New Pen(Color.Yellow, 1), sngX2, sngY2 - 5, sngX2, sngY2 + 5)
                    g.DrawEllipse(New Pen(Color.Yellow, 1), sngX2 + 3, sngY2 - 5, 10, 10)
                    g.DrawLine(New Pen(Color.Yellow, 1), sngX2, sngY2, sngX2 + 3, sngY2)
                End If
            Case 3 '画站台

                MoveX1 = sngClickX
                MoveY1 = sngClickY
                Call GetGridXY(e.X, e.Y)
                MoveX2 = GridXY.X
                MoveY2 = GridXY.Y
                If MoveX1 > 0 And MoveY1 > 0 Then
                    Me.picSta.Refresh()
                    g.DrawRectangle(New Pen(Color.Yellow, 2), MoveX1, MoveY1, MoveX2 - MoveX1, MoveY2 - MoveY1)
                End If
            Case 5 '移动底图
                If e.Button = Windows.Forms.MouseButtons.Left Then
                    Call GetGridXY(e.X, e.Y)
                    PicMoveX = GridXY.X - ntmpCurMoveX
                    PicMoveY = GridXY.Y - ntmpCurMoveY
                End If
            Case 6 '多组选择
                If nEditState = 2 Then
                    MoveX1 = sngClickX
                    MoveY1 = sngClickY
                    Call GetGridXY(e.X, e.Y)
                    MoveX2 = GridXY.X
                    MoveY2 = GridXY.Y
                    If MoveX1 > 0 And MoveY1 > 0 Then
                        Me.picSta.Refresh()
                        g.DrawRectangle(New Pen(Color.White, 1), MoveX1, MoveY1, MoveX2 - MoveX1, MoveY2 - MoveY1)
                    End If
                End If
        End Select
    End Sub

    Private Sub picSta_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picSta.MouseUp
        Dim MoveX, MoveY As Single
        Dim MoveX1, MoveX2, MoveY1, MoveY2 As Single
        Dim i As Integer
        Dim tmpID As Integer
        Dim ntmpCADStainfID As Integer
        Dim ntmpTrackID As Integer
        Select Case nDrawState
            Case 0
                Select Case nEditState

                    Case 1 '移动线段
                        '移动
                        Call GetGridXY(e.X, e.Y)
                        MoveX = GridXY.X - sngClickX
                        MoveY = GridXY.Y - sngClickY
                        If MoveX = 0 And MoveY = 0 Then

                        Else
                            For i = 1 To UBound(curSelectTrackID)
                                tmpID = curSelectTrackID(i)
                                tmpTrackInf(tmpID).X1 = tmpTrackInf(tmpID).X1 + MoveX
                                tmpTrackInf(tmpID).X2 = tmpTrackInf(tmpID).X2 + MoveX
                                tmpTrackInf(tmpID).Y1 = tmpTrackInf(tmpID).Y1 + MoveY
                                tmpTrackInf(tmpID).Y2 = tmpTrackInf(tmpID).Y2 + MoveY
                                MoveX1 = (tmpTrackInf(tmpID).X1) / nCurShowScale + nCurPicstaLeftX
                                MoveY1 = (tmpTrackInf(tmpID).Y1) / nCurShowScale + nCurPicStaTopY
                                MoveX2 = (tmpTrackInf(tmpID).X2) / nCurShowScale + nCurPicstaLeftX
                                MoveY2 = (tmpTrackInf(tmpID).Y2) / nCurShowScale + nCurPicStaTopY

                                ntmpCADStainfID = tmpTrackInf(tmpID).nStaID
                                ntmpTrackID = tmpTrackInf(tmpID).nTrackID
                                Call EditCADStaTrack(CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sStaName, ntmpCADStainfID, ntmpTrackID, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sStyle, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sGuDaoStyle, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sGuDaoYongTu, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sGuDaoUseSeq, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sngLength, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sTrackNum, _
                                                    MoveX1, _
                                                    MoveY1, _
                                                    MoveX2, _
                                                    MoveY2, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sLeftLink1, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sLeftLink2, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sLeftLink3, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sRightLink1, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sRightLink2, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sRightLink3, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sTrackCircuitNum, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sControlNum, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sMemo)
                            Next
                            'Call EditTrackCoordInf(ntmpTrackInfID, MoveX1, MoveX2, MoveY1, MoveY2)
                            Call DrawStaPic(Me.picSta, Me.picLine)
                            Call SelectLineStyle(1)
                            Call CADaddOneUndoInf()
                        End If
                        nEditState = 0
                        Me.picSta.Cursor = Cursors.Default
                    Case 2 '修改线段坐标
                        If nEditLeftOrRightCoord > 0 Then
                            If nEditLeftOrRightCoord = 1 Then '左边
                                Call GetGridXY(e.X, e.Y)
                                MoveX = GridXY.X
                                MoveY = GridXY.Y
                                tmpTrackInf(ntmpTrackInfID).X1 = MoveX
                                tmpTrackInf(ntmpTrackInfID).Y1 = MoveY
                                ntmpCADStainfID = tmpTrackInf(ntmpTrackInfID).nStaID
                                ntmpTrackID = tmpTrackInf(ntmpTrackInfID).nTrackID
                                MoveX1 = (tmpTrackInf(ntmpTrackInfID).X1) / nCurShowScale + nCurPicstaLeftX
                                MoveY1 = (tmpTrackInf(ntmpTrackInfID).Y1) / nCurShowScale + nCurPicStaTopY

                                If nIfPreseCtrl = 1 Then '按住CTRL
                                    MoveX1 = CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).X2
                                ElseIf nIfPreseCtrl = 2 Then '按住Shift
                                    MoveY1 = CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).Y2
                                End If

                                'MoveX2 = (tmpTrackInf(ntmpTrackInfID).X2) / nCurShowScale + nCurPicstaLeftX
                                ' MoveY2 = (tmpTrackInf(ntmpTrackInfID).Y2) / nCurShowScale + nCurPicStaTopY
                                Call EditCADStaTrack(CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sStaName, nCADStainfID, nTrackID, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sStyle, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sGuDaoStyle, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sGuDaoYongTu, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sGuDaoUseSeq, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sngLength, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sTrackNum, _
                                                    MoveX1, _
                                                    MoveY1, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).X2, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).Y2, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sLeftLink1, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sLeftLink2, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sLeftLink3, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sRightLink1, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sRightLink2, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sRightLink3, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sTrackCircuitNum, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sControlNum, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sMemo)

                            ElseIf nEditLeftOrRightCoord = 2 Then '右边
                                Call GetGridXY(e.X, e.Y)
                                MoveX = GridXY.X
                                MoveY = GridXY.Y
                                tmpTrackInf(ntmpTrackInfID).X2 = MoveX
                                tmpTrackInf(ntmpTrackInfID).Y2 = MoveY
                                ntmpCADStainfID = tmpTrackInf(ntmpTrackInfID).nStaID
                                ntmpTrackID = tmpTrackInf(ntmpTrackInfID).nTrackID

                                ' MoveX1 = (tmpTrackInf(ntmpTrackInfID).X1) / nCurShowScale + nCurPicstaLeftX
                                ' MoveY1 = (tmpTrackInf(ntmpTrackInfID).Y1) / nCurShowScale + nCurPicStaTopY
                                MoveX2 = (tmpTrackInf(ntmpTrackInfID).X2) / nCurShowScale + nCurPicstaLeftX
                                MoveY2 = (tmpTrackInf(ntmpTrackInfID).Y2) / nCurShowScale + nCurPicStaTopY

                                If nIfPreseCtrl = 1 Then '按住CTRL
                                    MoveX2 = CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).X1
                                ElseIf nIfPreseCtrl = 2 Then '按住Shift
                                    MoveY2 = CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).Y1
                                End If

                                Call EditCADStaTrack(CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sStaName, nCADStainfID, nTrackID, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sStyle, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sGuDaoStyle, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sGuDaoYongTu, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sGuDaoUseSeq, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sngLength, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sTrackNum, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).X1, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).Y1, _
                                                    MoveX2, _
                                                    MoveY2, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sLeftLink1, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sLeftLink2, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sLeftLink3, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sRightLink1, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sRightLink2, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sRightLink3, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sTrackCircuitNum, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sControlNum, _
                                                    CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sMemo)

                            End If

                            nEditState = 0
                            nEditLeftOrRightCoord = 0
                            Me.picSta.Cursor = Cursors.Default
                            Call DrawStaPic(Me.picSta, Me.picLine)
                            Call CADaddOneUndoInf()
                        End If

                    Case 3 '移动信号机
                        '移动
                        Call GetGridXY(e.X, e.Y)
                        MoveX1 = GridXY.X
                        MoveY1 = GridXY.Y
                        Call EditCADStaSignal(nCADStainfID, nSignalID, _
                                            CADStaInf(nCADStainfID).Signal(nSignalID).sStyle, _
                                            CADStaInf(nCADStainfID).Signal(nSignalID).nTrackNum, _
                                            CADStaInf(nCADStainfID).Signal(nSignalID).nCrossNum, _
                                            MoveX1, _
                                            MoveY1, _
                                            CADStaInf(nCADStainfID).Signal(nSignalID).sMemo)
                        nEditState = 0
                        Me.picSta.Cursor = Cursors.Default
                        Call DrawStaPic(Me.picSta, Me.picLine)

                    Case 4 '移动站台
                        Call GetGridXY(e.X, e.Y)
                        MoveX = GridXY.X - sngClickX
                        MoveY = GridXY.Y - sngClickY
                        MoveX1 = CADStaInf(nCADStainfID).PlatForm(nPlatformID).X1 + MoveX - sngLeftBaseX
                        MoveY1 = CADStaInf(nCADStainfID).PlatForm(nPlatformID).Y1 + MoveY
                        MoveX2 = CADStaInf(nCADStainfID).PlatForm(nPlatformID).X2 + MoveX - sngLeftBaseX
                        MoveY2 = CADStaInf(nCADStainfID).PlatForm(nPlatformID).Y2 + MoveY

                        Call EditCADStaPlatform(nCADStainfID, nPlatformID, _
                                            CADStaInf(nCADStainfID).PlatForm(nPlatformID).sStyle, _
                                            CADStaInf(nCADStainfID).PlatForm(nPlatformID).nWidth, _
                                            CADStaInf(nCADStainfID).PlatForm(nPlatformID).nHeight, _
                                            MoveX1, _
                                            MoveY1, _
                                            MoveX2, _
                                            MoveY2, _
                                            CADStaInf(nCADStainfID).PlatForm(nPlatformID).sMemo)
                        nEditState = 0
                        Me.picSta.Cursor = Cursors.Default
                        Call DrawStaPic(Me.picSta, Me.picLine)

                    Case 5 '移动字体

                        Call GetGridXY(e.X, e.Y)
                        MoveX = GridXY.X - sngClickX
                        MoveY = GridXY.Y - sngClickY
                        MoveX1 = CADStaInf(nCADStainfID).FontText(nFontTextID).X + MoveX - sngLeftBaseX
                        MoveY1 = CADStaInf(nCADStainfID).FontText(nFontTextID).Y + MoveY

                        Call EditCADStaFontText(nCADStainfID, nFontTextID, _
                                            CADStaInf(nCADStainfID).FontText(nFontTextID).sText, _
                                            CADStaInf(nCADStainfID).FontText(nFontTextID).FontName, _
                                            CADStaInf(nCADStainfID).FontText(nFontTextID).FontSize, _
                                            CADStaInf(nCADStainfID).FontText(nFontTextID).Italic, _
                                            CADStaInf(nCADStainfID).FontText(nFontTextID).Bold, _
                                            CADStaInf(nCADStainfID).FontText(nFontTextID).FontColor.ToArgb, _
                                            MoveX1, _
                                            MoveY1, _
                                            CADStaInf(nCADStainfID).FontText(nFontTextID).sMemo)
                        nEditState = 0
                        Me.picSta.Cursor = Cursors.Default
                        Call DrawStaPic(Me.picSta, Me.picLine)

                    Case 6 '旋转
                        Me.TimerMovePic.Enabled = False
                        Me.picSta.Cursor = Cursors.Default
                        nEditState = 0
                        Call RefreshCADstainfXandYCoord()
                        Call SelectLineStyle(1)
                        Call CADaddOneUndoInf()
                End Select

            Case 1 '画线


            Case 2 '画信号机

            Case 3 '画站台

                Call GetGridXY(e.X, e.Y)
                sngX2 = GridXY.X
                sngY2 = GridXY.Y
                If sngX2 > sngClickX And sngY2 > sngClickY Then
                    Call addCADStaPlatform(nCADStainfID, "", 0, 0, sngClickX, sngClickY, sngX2, sngY2, "")
                    'Me.tolBtDrawPlatform.Checked = False
                    nDrawState = 0
                    Me.picSta.Cursor = Cursors.Default
                    Call DrawStaPic(Me.picSta, Me.picLine)
                End If
            Case 5 '移动底图
                If e.Button = Windows.Forms.MouseButtons.Left Then
                    Me.TimerMovePic.Enabled = False
                    Call GetGridXY(e.X, e.Y)
                    MoveX = GridXY.X - ntmpCurMoveX
                    MoveY = GridXY.Y - ntmpCurMoveY
                    nCurPicstaLeftX = nCurPicstaLeftX - MoveX / nCurShowScale
                    nCurPicStaTopY = nCurPicStaTopY - MoveY / nCurShowScale
                    Call RefreshPicSta(nCurPicstaLeftX, nCurPicStaTopY, Me.picSta, Me.picLine, nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)
                End If

                'Me.picSta.Cursor = Cursors.Default
                'nDrawState = 0
                'Me.TolbtMovePic.Checked = False
            Case 6 '多组选择
                If nEditState = 2 Then
                    Call GetGridXY(e.X, e.Y)
                    sngX2 = GridXY.X
                    sngY2 = GridXY.Y
                    CurRotateCenterPoint = New Point(sngClickX + (sngX2 - sngClickX) / 2, (sngClickY + (sngY2 - sngClickY) / 2))
                    Call SelectMultiTack(sngClickX, sngX2, sngClickY, sngY2)
                    Me.picSta.Refresh()
                    If UBound(curSelectTrackID) > 0 Then
                        Call SelectLineStyle(1)
                    End If
                    Me.TolbtMultiLine.Checked = False
                End If
                nDrawState = 0
                nEditState = 0
        End Select
    End Sub

    '更新CADstainf中的坐标值
    Private Sub RefreshCADstainfXandYCoord()
        Dim X1, Y1, X2, Y2 As Single
        Dim tmpID As Integer
        Dim i As Integer
        For i = 1 To UBound(curSelectTrackID)
            tmpID = curSelectTrackID(i)
            X1 = (tmpTrackInf(tmpID).X1) / nCurShowScale + nCurPicstaLeftX
            Y1 = (tmpTrackInf(tmpID).Y1) / nCurShowScale + nCurPicStaTopY
            X2 = (tmpTrackInf(tmpID).X2) / nCurShowScale + nCurPicstaLeftX
            Y2 = (tmpTrackInf(tmpID).Y2) / nCurShowScale + nCurPicStaTopY

            Dim ntmpCADStainfID As Integer
            Dim ntmpTrackID As Integer
            ntmpCADStainfID = tmpTrackInf(tmpID).nStaID
            ntmpTrackID = tmpTrackInf(tmpID).nTrackID
            Call EditCADStaTrack(CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sStaName, ntmpCADStainfID, ntmpTrackID, _
                                CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sStyle, _
                                CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sGuDaoStyle, _
                                CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sGuDaoYongTu, _
                                CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sGuDaoUseSeq, _
                                CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sngLength, _
                                CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sTrackNum, _
                                X1, _
                                Y1, _
                                X2, _
                                Y2, _
                                CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sLeftLink1, _
                                CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sLeftLink2, _
                                CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sLeftLink3, _
                                CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sRightLink1, _
                                CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sRightLink2, _
                                CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sRightLink3, _
                                CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sTrackCircuitNum, _
                                CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sControlNum, _
                                CADStaInf(ntmpCADStainfID).Track(ntmpTrackID).sMemo)
        Next
    End Sub
    Private Sub tolbtCursor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tolbtCursor.Click
        nDrawState = 0
        nClickState = 0
        nEditState = 0
        Me.picSta.Cursor = Cursors.Default
        Call SetOterBottonNotChecked()
        sngX1 = 0
        sngY1 = 0
    End Sub

    Private Sub DrawStaPic(ByVal picSta As PictureBox, ByVal picLine As PictureBox)
        Call RefreshPicSta(nCurPicstaLeftX, nCurPicStaTopY, Me.picSta, Me.picLine, nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)
    End Sub

    '显示当前选中的车站
    Private Sub ShowCurSelectStaPic(ByVal nStaID As Integer)
        Dim i As Integer
        Dim minX As Single
        Dim minY As Single
        Dim maxX As Single
        Dim maxY As Single
        minX = 10000000
        minY = 10000000
        maxX = -10000000
        maxY = -10000000
        For i = 1 To UBound(CADStaInf(nStaID).Track)
            If CADStaInf(nStaID).Track(i).nDelete = 0 Then
                minX = Minimal(minX, CADStaInf(nStaID).Track(i).X1)
                minX = Minimal(minX, CADStaInf(nStaID).Track(i).X2)
                minY = Minimal(minY, CADStaInf(nStaID).Track(i).Y1)
                minY = Minimal(minY, CADStaInf(nStaID).Track(i).Y2)
                maxX = Maximal(maxX, CADStaInf(nStaID).Track(i).X1)
                maxX = Maximal(maxX, CADStaInf(nStaID).Track(i).X2)
                maxY = Maximal(maxY, CADStaInf(nStaID).Track(i).Y1)
                maxY = Maximal(maxY, CADStaInf(nStaID).Track(i).Y2)
            End If
        Next
        If maxX = -10000000 Then
            nCurPicstaLeftX = 0
            nCurPicStaTopY = 0
        Else
            nCurPicstaLeftX = minX - (Me.picSta.Width - (maxX - minX) * nCurShowScale) / (2 * nCurShowScale)
            nCurPicStaTopY = minY - (Me.picSta.Height - (maxY - minY) * nCurShowScale) / (2 * nCurShowScale)
        End If

        'Me.控制方式tolCmbControlScheme.Items.Clear()
        If UBound(CADStaInf(nStaID).ContolScheme) > 0 Then
            For i = 1 To UBound(CADStaInf(nStaID).ContolScheme)
                'Me.控制方式tolCmbControlScheme.Items.Add(CADStaInf(nStaID).ContolScheme(i).sSchemeName)
            Next
            'Me.控制方式tolCmbControlScheme.Text = CADStaInf(nStaID).sCurControlScheme
            CADformPara.sCurStaSchemeName = CADStaInf(nStaID).sCurControlScheme
            Call InputContolSchemenf(nStaID, CADformPara.sCurStaSchemeName)
        End If

        Call RefreshPicSta(nCurPicstaLeftX, nCurPicStaTopY, Me.picSta, Me.picLine, nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)

        ReDim curSelectTrackID(0)
        For i = 1 To UBound(tmpTrackInf)
            If tmpTrackInf(i).nStaID = nStaID Then
                ReDim Preserve curSelectTrackID(UBound(curSelectTrackID) + 1)
                curSelectTrackID(UBound(curSelectTrackID)) = i
            End If
        Next

        Call SelectLineStyle(1)
    End Sub
    '显示当前选中的线路
    Private Sub ShowCurSelectLInePic(ByVal sLineName As String)
        Dim i, j, k As Integer
        Dim nStaId As Integer
        ReDim curSelectTrackID(0)
        For j = 1 To UBound(CADStaInf)
            If NetInf.Line(CADStaInf(j).nLineID).sName = sLineName Then
                nStaId = j
                For k = 1 To UBound(CADStaInf(nStaId).Track)
                    For i = 1 To UBound(tmpTrackInf)
                        If tmpTrackInf(i).nStaID = nStaId Then
                            ReDim Preserve curSelectTrackID(UBound(curSelectTrackID) + 1)
                            curSelectTrackID(UBound(curSelectTrackID)) = i
                        End If
                    Next

                Next
            End If
        Next


        Call SelectLineStyle(1)
    End Sub
    '
    Public Sub RefreshCADStaPicture()
        Call DrawStaPic(Me.picSta, Me.picLine)
    End Sub

    Private Sub tolbtShowGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 显示网格.Click
        Call DrawStaPic(Me.picSta, Me.picLine)
    End Sub
    Private Sub GetGridXY(ByVal X As Single, ByVal Y As Single)
        GridXY.X = X
        GridXY.Y = Y
        Dim nCenterX As Single
        Dim nCenterY As Single
        nCenterX = picSta.Width / 2
        nCenterY = picSta.Height / 2
        'Dim tmpX As Single
        'Dim tmpY As Single
        'GridXY.X = Math.Round(X / nGridWidth) * nGridWidth - (nCurPicstaLeftX Mod nFirGridWidth) * nCurShowScale '+ nGridWidth
        'GridXY.Y = Math.Round(Y / nGridWidth) * nGridWidth - (nCurPicStaTopY Mod nFirGridWidth) * nCurShowScale ' - nGridWidth
        Dim i As Integer
        Dim P1 As New Point
        Dim nMax As Long
        nMax = 10000000000000
        Dim tmpLength As Integer
        Dim tmpGridXY As New GridInf
        For i = 1 To GridXYCord.Count
            tmpGridXY = GridXYCord(i - 1)
            P1.X = tmpGridXY.X
            P1.Y = tmpGridXY.Y
            tmpLength = Math.Sqrt((X - P1.X) ^ 2 + (Y - P1.Y) ^ 2)
            If tmpLength < nMax Then
                GridXY.X = P1.X
                GridXY.Y = P1.Y
                nMax = tmpLength
            End If
        Next
        Call SeekCurrenDuiXiangId(X, Y)

    End Sub

    '找到与之相近的X坐标
    Private Sub SeekCurrenDuiXiangId(ByVal X As Single, ByVal Y As Single)
        Dim i As Integer
        For i = 1 To UBound(tmpTrackInf)
            If i <> ntmpTrackInfID Then
                If Math.Abs(tmpTrackInf(i).X1 - X) <= nGridWidth * nCurShowScale And Math.Abs(tmpTrackInf(i).Y1 - Y) <= nGridWidth * nCurShowScale Then
                    GridXY.X = tmpTrackInf(i).X1
                    GridXY.Y = tmpTrackInf(i).Y1
                ElseIf Math.Abs(tmpTrackInf(i).X2 - X) <= nGridWidth * nCurShowScale And Math.Abs(tmpTrackInf(i).Y2 - Y) <= nGridWidth * nCurShowScale Then
                    GridXY.X = tmpTrackInf(i).X2
                    GridXY.Y = tmpTrackInf(i).Y2
                End If
            End If
        Next
    End Sub
    Private Sub tolmnuDelteTrack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 删除DToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub frmCADStation_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        'If Me.SplitContainer4.Panel1.Width > Me.picSta.Width Then
        '    Me.picSta.Left = (Me.SplitContainer4.Panel1.Width - Me.picSta.Width) / 2
        'Else
        '    Me.picSta.Left = 0
        'End If

        ''竖直位置
        'If Me.SplitContainer4.Panel1.Height - Me.picSta.Height > 0 Then
        '    Me.picSta.Top = (Me.SplitContainer4.Panel1.Height - Me.picSta.Height) / 2
        'Else
        '    Me.picSta.Top = 0
        'End If
    End Sub

    Private Sub tolmnuMoveTrack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 移动MToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub tolmnuEditCoord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        nEditState = 2 '修改线段坐标
        ReDim curSelectTrackID(1)
        curSelectTrackID(1) = ntmpTrackInfID
        Call SelectLineStyle(2)
    End Sub

    '选中线路
    Private Sub SelectLineStyle(ByVal nStyle As Integer)
        Dim X1, X2, Y1, Y2 As Single
        Me.picSta.Refresh()
        Dim nTrackID As Integer
        Dim nStaID As Integer
        Dim i As Integer
        Dim LineWidth As Single
        LineWidth = 4 * nCurShowScale
        If LineWidth < 2 And LineWidth > 1 Then
            LineWidth = 2
        ElseIf LineWidth < 1 Then
            LineWidth = 1
        End If
        If UBound(curSelectTrackID) > 0 Then
            For i = 1 To UBound(curSelectTrackID)
                nTrackID = curSelectTrackID(i)
                nStaID = tmpTrackInf(nTrackID).nStaID
                If nTrackID > 0 Then
                    Dim g As Drawing.Graphics
                    g = Me.picSta.CreateGraphics
                    X1 = tmpTrackInf(nTrackID).X1
                    Y1 = tmpTrackInf(nTrackID).Y1
                    X2 = tmpTrackInf(nTrackID).X2
                    Y2 = tmpTrackInf(nTrackID).Y2
                    g.DrawLine(New Pen(Color.Red, LineWidth), X1, Y1, X2, Y2)
                    If nStyle = 1 Then
                        g.DrawRectangle(New Pen(Color.Blue, LineWidth / 2), X1 - 5 * nCurShowScale, Y1 - 5 * nCurShowScale, 10 * nCurShowScale, 10 * nCurShowScale)
                        g.DrawRectangle(New Pen(Color.SpringGreen, LineWidth / 2), X2 - 5 * nCurShowScale, Y2 - 5 * nCurShowScale, 10 * nCurShowScale, 10 * nCurShowScale)
                    End If
                    If nStyle = 2 Then
                        g.DrawEllipse(New Pen(Color.Blue, LineWidth / 2), X1 - 5 * nCurShowScale, Y1 - 5 * nCurShowScale, 10 * nCurShowScale, 10 * nCurShowScale)
                        g.DrawEllipse(New Pen(Color.SpringGreen, LineWidth / 2), X2 - 5 * nCurShowScale, Y2 - 5 * nCurShowScale, 10 * nCurShowScale, 10 * nCurShowScale)
                    End If
                End If
            Next
            ntmpTrackInfID = curSelectTrackID(1)
            nCADStainfID = nStaID
            Call setTrwShowSelectedSta(CADStaInf(nStaID).sStaName)
            'Me.控制方式tolCmbControlScheme.Items.Clear()
            'If UBound(CADStaInf(nStaID).ContolScheme) > 0 Then
            '    For i = 1 To UBound(CADStaInf(nStaID).ContolScheme)
            '        Me.控制方式tolCmbControlScheme.Items.Add(CADStaInf(nStaID).ContolScheme(i).sSchemeName)
            '    Next
            '    Me.控制方式tolCmbControlScheme.Text = ""
            '    'CADformPara.sCurStaSchemeName = CADStaInf(nStaID).sCurControlScheme
            '    Call InputContolSchemenf(nStaID, CADformPara.sCurStaSchemeName)
            'End If
        Else
            Me.picSta.ContextMenuStrip = Nothing
        End If


        ' Me.tolCmbControlScheme.Text = CADStaInf(nStaID).sCurControlScheme
        Call CopyAndPasteMnuSet()
    End Sub

    '在树视图上显示选中的车站
    Public Sub setTrwShowSelectedSta(ByVal StrStaName As String)

        Dim i As Integer
        Dim j As Integer
        Dim ifIn As Integer
        ifIn = 0
        For i = 0 To Me.trvLine.Nodes(0).GetNodeCount(False) - 1
            For j = 0 To Me.trvLine.Nodes(0).Nodes(i).GetNodeCount(False) - 1
                If Me.trvLine.Nodes(0).Nodes(i).Nodes(j).Text = StrStaName Then
                    Me.trvLine.SelectedNode = Me.trvLine.Nodes(0).Nodes(i).Nodes(j)
                    ifIn = 1
                    Exit For
                End If
            Next j
            If ifIn = 1 Then Exit For
        Next i
    End Sub
    '选中文字
    Private Sub SelectFontTextStyle(ByVal nFontTextID As Integer)
        Dim X1, Y1 As Single
        Me.picSta.Refresh()
        If nFontTextID > 0 Then
            Dim g As Drawing.Graphics
            g = Me.picSta.CreateGraphics
            X1 = CADStaInf(nCADStainfID).FontText(nFontTextID).X - sngLeftBaseX
            Y1 = CADStaInf(nCADStainfID).FontText(nFontTextID).Y

            Dim sText As String
            Dim FontName As String
            Dim FontSize As Single
            Dim FontColor As Color
            sText = CADStaInf(nCADStainfID).FontText(nFontTextID).sText
            FontSize = CADStaInf(nCADStainfID).FontText(nFontTextID).FontSize
            FontName = CADStaInf(nCADStainfID).FontText(nFontTextID).FontName
            FontColor = Color.Red 'CADStaInf(nCADStainfID).FontText(nFontTextID).FontColor
            Dim s As New FontStyle
            'If CADStaInf(nCADStainfID).FontText(nFontTextID).Italic = True Then
            '    s = CADStaInf(nCADStainfID).FontText(nFontTextID).Italic
            'End If
            'If CADStaInf(nCADStainfID).FontText(nFontTextID).Bold = True Then
            '    s = CADStaInf(nCADStainfID).FontText(nFontTextID).Bold
            'End If
            s = FontStyle.Underline
            Dim b As New SolidBrush(FontColor)
            Dim f As New Font(FontName, FontSize, s)
            If X1 > 0 And Y1 > 0 Then
                g.DrawString(sText, f, b, X1, Y1)
            End If
        Else
            Me.picSta.ContextMenuStrip = Nothing
        End If
    End Sub

    '选中站台
    Private Sub SelectPlatformStyle(ByVal nPlatformID As Integer)
        Dim X1, X2, Y1, Y2 As Single
        Me.picSta.Refresh()
        If nPlatformID > 0 Then
            Dim g As Drawing.Graphics
            g = Me.picSta.CreateGraphics
            X1 = CADStaInf(nCADStainfID).PlatForm(nPlatformID).X1 - sngLeftBaseX
            Y1 = CADStaInf(nCADStainfID).PlatForm(nPlatformID).Y1
            X2 = CADStaInf(nCADStainfID).PlatForm(nPlatformID).X2 - sngLeftBaseX
            Y2 = CADStaInf(nCADStainfID).PlatForm(nPlatformID).Y2
            g.DrawRectangle(New Pen(Color.Red, 2), X1, Y1, X2 - X1, Y2 - Y1)
        Else
            Me.picSta.ContextMenuStrip = Nothing
        End If
    End Sub

    '选中信号机
    Private Sub SelectSignalStyle(ByVal nSignalID As Integer)
        Dim X1, Y1 As Single
        Me.picSta.Refresh()
        If nSignalID > 0 Then
            Dim g As Drawing.Graphics
            g = Me.picSta.CreateGraphics
            X1 = CADStaInf(nCADStainfID).Signal(nSignalID).X - sngLeftBaseX
            Y1 = CADStaInf(nCADStainfID).Signal(nSignalID).Y
            Call PrintSignalByStyle(CADStaInf(nCADStainfID).Signal(nSignalID).sStyle, X1, Y1, g, Color.Red, 1)
        Else
            Me.picSta.ContextMenuStrip = Nothing
        End If
    End Sub
    Private Sub tolmnuEditTrackInfor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nCADStainfID As Integer
        Dim nTrackID As Integer
        Dim i As Integer
        If UBound(curSelectTrackID) > 0 Then
            If UBound(curSelectTrackID) = 1 Then
                nCADStainfID = tmpTrackInf(curSelectTrackID(1)).nStaID
                nTrackID = tmpTrackInf(curSelectTrackID(1)).nTrackID
                ReDim stuListItem(20)
                stuListItem(1).strItem = "线段类型"
                stuListItem(1).strStyle = PropStrStyle.ComBox
                ReDim Preserve stuListItem(1).StrCmbList(5)
                stuListItem(1).StrCmbList(1) = "连接线"
                stuListItem(1).StrCmbList(2) = "股道线"
                stuListItem(1).StrCmbList(3) = "道岔线"
                stuListItem(1).StrCmbList(4) = "站台线"
                stuListItem(1).StrCmbList(5) = "站名线"
                stuListItem(1).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).sStyle
                stuListItem(1).strItemCriterion = TextCriterion.NotRequired

                stuListItem(2).strItem = "股道类型"
                stuListItem(2).strStyle = PropStrStyle.ComBox
                ReDim Preserve stuListItem(2).StrCmbList(10)
                stuListItem(2).StrCmbList(1) = "正线线"
                stuListItem(2).StrCmbList(2) = "到发线"
                stuListItem(2).StrCmbList(3) = "折返线"
                stuListItem(2).StrCmbList(4) = "存车线"
                stuListItem(2).StrCmbList(5) = "正线线+折返线"
                stuListItem(2).StrCmbList(6) = "到发线+折返线"
                stuListItem(2).StrCmbList(7) = "折返线+存车线"
                stuListItem(2).StrCmbList(8) = "存车线+折返线"
                stuListItem(2).StrCmbList(9) = "进站连接线"
                stuListItem(2).StrCmbList(10) = "出站连接线"

                stuListItem(2).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).sGuDaoStyle
                stuListItem(2).strItemCriterion = TextCriterion.NotRequired

                stuListItem(3).strItem = "股道用途"
                stuListItem(3).strStyle = PropStrStyle.ComBox
                ReDim Preserve stuListItem(3).StrCmbList(5)
                stuListItem(3).StrCmbList(1) = "只能下行"
                stuListItem(3).StrCmbList(2) = "只能上行"
                stuListItem(3).StrCmbList(3) = "主要方向为下行"
                stuListItem(3).StrCmbList(4) = "主要方向为上行"
                stuListItem(3).StrCmbList(5) = "双方向"

                stuListItem(3).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).sGuDaoYongTu
                stuListItem(3).strItemCriterion = TextCriterion.NotRequired

                stuListItem(4).strItem = "股道使用顺序"
                stuListItem(4).strStyle = PropStrStyle.TexBox
                stuListItem(4).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).sGuDaoUseSeq
                stuListItem(4).strItemCriterion = TextCriterion.NotRequired

                stuListItem(5).strItem = "线段长度"
                stuListItem(5).strStyle = PropStrStyle.TexBox
                stuListItem(5).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).sngLength
                stuListItem(5).strItemCriterion = TextCriterion.NotRequired

                stuListItem(6).strItem = "股道或道岔编号"
                stuListItem(6).strStyle = PropStrStyle.TexBox
                stuListItem(6).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).sTrackNum
                stuListItem(6).strItemCriterion = TextCriterion.NotRequired

                stuListItem(7).strItem = "X1坐标"
                stuListItem(7).strStyle = PropStrStyle.TexBox
                stuListItem(7).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).X1 - sngLeftBaseX
                stuListItem(7).strItemCriterion = TextCriterion.NotRequired

                stuListItem(8).strItem = "Y1坐标"
                stuListItem(8).strStyle = PropStrStyle.TexBox
                stuListItem(8).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).Y1
                stuListItem(8).strItemCriterion = TextCriterion.NotRequired

                stuListItem(9).strItem = "X2坐标"
                stuListItem(9).strStyle = PropStrStyle.TexBox
                stuListItem(9).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).X2 - sngLeftBaseX
                stuListItem(9).strItemCriterion = TextCriterion.NotRequired

                stuListItem(10).strItem = "Y2坐标"
                stuListItem(10).strStyle = PropStrStyle.TexBox
                stuListItem(10).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).Y2
                stuListItem(10).strItemCriterion = TextCriterion.NotRequired

                stuListItem(11).strItem = "左1连接"
                stuListItem(11).strStyle = PropStrStyle.ComBox
                ReDim stuListItem(11).StrCmbList(0)
                Call SeekLinkTrackNum(CADStaInf(nCADStainfID).Track(nTrackID).sTrackCircuitNum, CADStaInf(nCADStainfID).Track(nTrackID).X1, CADStaInf(nCADStainfID).Track(nTrackID).Y1, CADStaInf(nCADStainfID).Track(nTrackID).X2, CADStaInf(nCADStainfID).Track(nTrackID).Y2, "左连接")
                If UBound(GetReturnValue) > 0 Then
                    ReDim Preserve stuListItem(11).StrCmbList(UBound(GetReturnValue))
                    For i = 1 To UBound(GetReturnValue)
                        stuListItem(11).StrCmbList(i) = GetReturnValue(i)
                    Next
                End If
                ReDim Preserve stuListItem(11).StrCmbList(UBound(stuListItem(11).StrCmbList) + 1)
                stuListItem(11).StrCmbList(UBound(stuListItem(11).StrCmbList)) = "NULL"
                stuListItem(11).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).sLeftLink1
                stuListItem(11).strItemCriterion = TextCriterion.NotRequired

                stuListItem(12).strItem = "左2连接"
                stuListItem(12).strStyle = PropStrStyle.ComBox
                ReDim stuListItem(12).StrCmbList(0)
                If UBound(GetReturnValue) > 0 Then
                    ReDim Preserve stuListItem(12).StrCmbList(UBound(GetReturnValue))
                    For i = 1 To UBound(GetReturnValue)
                        stuListItem(12).StrCmbList(i) = GetReturnValue(i)
                    Next
                End If
                ReDim Preserve stuListItem(12).StrCmbList(UBound(stuListItem(12).StrCmbList) + 1)
                stuListItem(12).StrCmbList(UBound(stuListItem(12).StrCmbList)) = "NULL"
                stuListItem(12).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).sLeftLink2
                stuListItem(12).strItemCriterion = TextCriterion.NotRequired

                stuListItem(13).strItem = "左3连接"
                stuListItem(13).strStyle = PropStrStyle.ComBox
                ReDim stuListItem(13).StrCmbList(0)
                If UBound(GetReturnValue) > 0 Then
                    ReDim Preserve stuListItem(13).StrCmbList(UBound(GetReturnValue))
                    For i = 1 To UBound(GetReturnValue)
                        stuListItem(13).StrCmbList(i) = GetReturnValue(i)
                    Next
                End If
                ReDim Preserve stuListItem(13).StrCmbList(UBound(stuListItem(13).StrCmbList) + 1)
                stuListItem(13).StrCmbList(UBound(stuListItem(13).StrCmbList)) = "NULL"
                stuListItem(13).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).sLeftLink3
                stuListItem(13).strItemCriterion = TextCriterion.NotRequired

                stuListItem(14).strItem = "右1连接"
                stuListItem(14).strStyle = PropStrStyle.ComBox
                ReDim stuListItem(14).StrCmbList(0)
                Call SeekLinkTrackNum(CADStaInf(nCADStainfID).Track(nTrackID).sTrackCircuitNum, CADStaInf(nCADStainfID).Track(nTrackID).X2, CADStaInf(nCADStainfID).Track(nTrackID).Y2, CADStaInf(nCADStainfID).Track(nTrackID).X1, CADStaInf(nCADStainfID).Track(nTrackID).Y1, "右连接")
                If UBound(GetReturnValue) > 0 Then
                    ReDim Preserve stuListItem(14).StrCmbList(UBound(GetReturnValue))
                    For i = 1 To UBound(GetReturnValue)
                        stuListItem(14).StrCmbList(i) = GetReturnValue(i)
                    Next
                End If
                ReDim Preserve stuListItem(14).StrCmbList(UBound(stuListItem(14).StrCmbList) + 1)
                stuListItem(14).StrCmbList(UBound(stuListItem(14).StrCmbList)) = "NULL"
                stuListItem(14).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).sRightLink1
                stuListItem(14).strItemCriterion = TextCriterion.NotRequired

                stuListItem(15).strItem = "右2连接"
                stuListItem(15).strStyle = PropStrStyle.ComBox
                ReDim stuListItem(15).StrCmbList(0)
                If UBound(GetReturnValue) > 0 Then
                    ReDim Preserve stuListItem(15).StrCmbList(UBound(GetReturnValue))
                    For i = 1 To UBound(GetReturnValue)
                        stuListItem(15).StrCmbList(i) = GetReturnValue(i)
                    Next
                End If
                ReDim Preserve stuListItem(15).StrCmbList(UBound(stuListItem(15).StrCmbList) + 1)
                stuListItem(15).StrCmbList(UBound(stuListItem(15).StrCmbList)) = "NULL"
                stuListItem(15).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).sRightLink2
                stuListItem(15).strItemCriterion = TextCriterion.NotRequired


                stuListItem(16).strItem = "右3连接"
                stuListItem(16).strStyle = PropStrStyle.ComBox
                ReDim stuListItem(16).StrCmbList(0)
                If UBound(GetReturnValue) > 0 Then
                    ReDim Preserve stuListItem(16).StrCmbList(UBound(GetReturnValue))
                    For i = 1 To UBound(GetReturnValue)
                        stuListItem(16).StrCmbList(i) = GetReturnValue(i)
                    Next
                End If
                ReDim Preserve stuListItem(16).StrCmbList(UBound(stuListItem(16).StrCmbList) + 1)
                stuListItem(16).StrCmbList(UBound(stuListItem(16).StrCmbList)) = "NULL"
                stuListItem(16).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).sRightLink3
                stuListItem(16).strItemCriterion = TextCriterion.NotRequired

                stuListItem(17).strItem = "轨道电路编号"
                stuListItem(17).strStyle = PropStrStyle.TexBox
                stuListItem(17).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).sTrackCircuitNum
                stuListItem(17).strItemCriterion = TextCriterion.NotRequired

                stuListItem(18).strItem = "控制模块"
                stuListItem(18).strStyle = PropStrStyle.ComBox
                ReDim stuListItem(18).StrCmbList(UBound(ControlModel))
                For i = 1 To UBound(ControlModel)
                    stuListItem(18).StrCmbList(i) = ControlModel(i).sModelName
                Next
                stuListItem(18).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).sControlNum
                stuListItem(18).strItemCriterion = TextCriterion.NotRequired


                stuListItem(19).strItem = "备注"
                stuListItem(19).strStyle = PropStrStyle.TexBox
                stuListItem(19).strTxtList = CADStaInf(nCADStainfID).Track(nTrackID).sMemo
                stuListItem(19).strItemCriterion = TextCriterion.NotRequired

                stuListItem(20).strItem = "车站或区间名称"
                stuListItem(20).strStyle = PropStrStyle.ComBox
                ReDim stuListItem(20).StrCmbList(0)
                If UBound(CADStaInf) > 0 Then
                    ReDim Preserve stuListItem(20).StrCmbList(UBound(CADStaInf))
                    For i = 1 To UBound(CADStaInf)
                        stuListItem(20).StrCmbList(i) = CADStaInf(i).sStaName
                    Next
                End If
                stuListItem(20).strTxtList = CADStaInf(nCADStainfID).sStaName
                stuListItem(20).strItemCriterion = TextCriterion.NotEmpty

                Dim nf2 As New frmEditDataProperity
                nf2.Text = CADStaInf(nCADStainfID).sStaName & "(" & CADStaInf(nCADStainfID).sStaCode & ")" & " 线段属性 " & "ID= " & nTrackID

sBeGin:
                nf2.ShowDialog()
                If nf2.blnOK = True Then
                    If stuListItem(17).strReturnValue <> "" Then
                        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
                        Dim strTable As String = "select * from 线段信息表 where 轨道电路编号 = '" & stuListItem(17).strReturnValue & "' "

                        Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
                        Dim myDataSet As System.Data.DataSet = New System.Data.DataSet
                        Mydc.Fill(myDataSet, "线段信息表")
                        Dim myTable As Data.DataTable
                        myTable = myDataSet.Tables("线段信息表")
                        If myTable.Rows.Count > 0 Then
                            If CADStaInf(nCADStainfID).Track(nTrackID).sTrackCircuitNum <> stuListItem(17).strReturnValue Then
                                MsgBox("轨道电路编号" & stuListItem(17).strReturnValue & "已经存在，请重新输入!", , "提示")
                                stuListItem(17).strTxtList = stuListItem(17).strReturnValue
                                GoTo sBeGin
                            End If
                        End If
                    End If

                    Call EditCADStaTrack(stuListItem(20).strReturnValue, nCADStainfID, nTrackID, _
                                        stuListItem(1).strReturnValue, _
                                        stuListItem(2).strReturnValue, _
                                        stuListItem(3).strReturnValue, _
                                        stuListItem(4).strReturnValue, _
                                        stuListItem(5).strReturnValue, _
                                        stuListItem(6).strReturnValue, _
                                        stuListItem(7).strReturnValue, _
                                        stuListItem(8).strReturnValue, _
                                        stuListItem(9).strReturnValue, _
                                        stuListItem(10).strReturnValue, _
                                        stuListItem(11).strReturnValue, _
                                        stuListItem(12).strReturnValue, _
                                        stuListItem(13).strReturnValue, _
                                        stuListItem(14).strReturnValue, _
                                        stuListItem(15).strReturnValue, _
                                        stuListItem(16).strReturnValue, _
                                        stuListItem(17).strReturnValue, _
                                        stuListItem(18).strReturnValue, _
                                        stuListItem(19).strReturnValue)

                    Call DrawStaPic(Me.picSta, Me.picLine)
                    Call CADaddOneUndoInf()
                End If

            Else '组操作

                ReDim stuListItem(7)
                Dim sValue() As String
                ReDim sValue(7)
                stuListItem(1).strItem = "车站或区间名称"
                stuListItem(1).strStyle = PropStrStyle.ComBox
                ReDim stuListItem(1).StrCmbList(0)
                If UBound(CADStaInf) > 0 Then
                    ReDim Preserve stuListItem(1).StrCmbList(UBound(CADStaInf))
                    For i = 1 To UBound(CADStaInf)
                        stuListItem(1).StrCmbList(i) = CADStaInf(i).sStaName
                    Next
                End If
                stuListItem(1).strTxtList = ""
                stuListItem(1).strItemCriterion = TextCriterion.NotRequired


                stuListItem(2).strItem = "线段类型"
                stuListItem(2).strStyle = PropStrStyle.ComBox
                ReDim Preserve stuListItem(2).StrCmbList(5)
                stuListItem(2).StrCmbList(1) = "连接线"
                stuListItem(2).StrCmbList(2) = "股道线"
                stuListItem(2).StrCmbList(3) = "道岔线"
                stuListItem(2).StrCmbList(4) = "站台线"
                stuListItem(2).StrCmbList(5) = "站名线"
                stuListItem(2).strTxtList = ""
                stuListItem(2).strItemCriterion = TextCriterion.NotRequired

                stuListItem(3).strItem = "股道类型"
                stuListItem(3).strStyle = PropStrStyle.ComBox
                ReDim Preserve stuListItem(3).StrCmbList(10)
                stuListItem(3).StrCmbList(1) = "正线线"
                stuListItem(3).StrCmbList(2) = "到发线"
                stuListItem(3).StrCmbList(3) = "折返线"
                stuListItem(3).StrCmbList(4) = "存车线"
                stuListItem(3).StrCmbList(5) = "正线线+折返线"
                stuListItem(3).StrCmbList(6) = "到发线+折返线"
                stuListItem(3).StrCmbList(7) = "折返线+存车线"
                stuListItem(3).StrCmbList(8) = "存车线+折返线"
                stuListItem(3).StrCmbList(9) = "进站连接线"
                stuListItem(3).StrCmbList(10) = "出站连接线"

                stuListItem(3).strTxtList = ""
                stuListItem(3).strItemCriterion = TextCriterion.NotRequired

                stuListItem(4).strItem = "股道用途"
                stuListItem(4).strStyle = PropStrStyle.ComBox
                ReDim Preserve stuListItem(4).StrCmbList(5)
                stuListItem(4).StrCmbList(1) = "只能下行"
                stuListItem(4).StrCmbList(2) = "只能上行"
                stuListItem(4).StrCmbList(3) = "主要方向为下行"
                stuListItem(4).StrCmbList(4) = "主要方向为上行"
                stuListItem(4).StrCmbList(5) = "双方向"
                stuListItem(4).strTxtList = ""
                stuListItem(4).strItemCriterion = TextCriterion.NotRequired

                stuListItem(5).strItem = "线段长度"
                stuListItem(5).strStyle = PropStrStyle.TexBox
                stuListItem(5).strTxtList = ""
                stuListItem(5).strItemCriterion = TextCriterion.NotRequired

                stuListItem(6).strItem = "股道或道岔编号"
                stuListItem(6).strStyle = PropStrStyle.TexBox
                stuListItem(6).strTxtList = ""
                stuListItem(6).strItemCriterion = TextCriterion.NotRequired

                stuListItem(7).strItem = "控制模块"
                stuListItem(7).strStyle = PropStrStyle.ComBox
                ReDim stuListItem(7).StrCmbList(UBound(ControlModel))
                For i = 1 To UBound(ControlModel)
                    stuListItem(7).StrCmbList(i) = ControlModel(i).sModelName
                Next
                stuListItem(7).strTxtList = ""
                stuListItem(7).strItemCriterion = TextCriterion.NotRequired

                Dim nf2 As New frmEditDataProperity
                nf2.Text = "多组线段属性"
                nf2.labInfor.Text = "如不需要修改该项目，请设为空值。"
                nf2.ShowDialog()
                If nf2.blnOK = True Then
                    For i = 1 To UBound(curSelectTrackID)
                        nCADStainfID = tmpTrackInf(curSelectTrackID(i)).nStaID
                        nTrackID = tmpTrackInf(curSelectTrackID(i)).nTrackID

                        sValue(1) = GetActulReurnValue(nCADStainfID, nTrackID, stuListItem(1).strReturnValue, "车站或区间名称")
                        sValue(2) = GetActulReurnValue(nCADStainfID, nTrackID, stuListItem(2).strReturnValue, "线段类型")
                        sValue(3) = GetActulReurnValue(nCADStainfID, nTrackID, stuListItem(3).strReturnValue, "股道类型")
                        sValue(4) = GetActulReurnValue(nCADStainfID, nTrackID, stuListItem(4).strReturnValue, "股道用途")
                        sValue(5) = GetActulReurnValue(nCADStainfID, nTrackID, stuListItem(5).strReturnValue, "线段长度")
                        sValue(6) = GetActulReurnValue(nCADStainfID, nTrackID, stuListItem(6).strReturnValue, "股道或道岔编号")
                        sValue(7) = GetActulReurnValue(nCADStainfID, nTrackID, stuListItem(7).strReturnValue, "控制模块")

                        Call EditCADStaTrack(sValue(1), nCADStainfID, nTrackID, _
                                             sValue(2), _
                                             sValue(3), _
                                             sValue(4), _
                                            CADStaInf(nCADStainfID).Track(nTrackID).sGuDaoUseSeq, _
                                            Val(sValue(5)), _
                                            sValue(6), _
                                            CADStaInf(nCADStainfID).Track(nTrackID).X1, _
                                            CADStaInf(nCADStainfID).Track(nTrackID).Y1, _
                                            CADStaInf(nCADStainfID).Track(nTrackID).X2, _
                                            CADStaInf(nCADStainfID).Track(nTrackID).Y2, _
                                            CADStaInf(nCADStainfID).Track(nTrackID).sLeftLink1, _
                                            CADStaInf(nCADStainfID).Track(nTrackID).sLeftLink2, _
                                            CADStaInf(nCADStainfID).Track(nTrackID).sLeftLink3, _
                                            CADStaInf(nCADStainfID).Track(nTrackID).sRightLink1, _
                                            CADStaInf(nCADStainfID).Track(nTrackID).sRightLink2, _
                                            CADStaInf(nCADStainfID).Track(nTrackID).sRightLink3, _
                                            CADStaInf(nCADStainfID).Track(nTrackID).sTrackCircuitNum, _
                                            sValue(7), _
                                            CADStaInf(nCADStainfID).Track(nTrackID).sMemo)
                    Next
                    nf2.labInfor.Text = " 请输入或选择相关属性"
                    Call DrawStaPic(Me.picSta, Me.picLine)
                    Call CADaddOneUndoInf()
                End If
            End If
        End If
    End Sub

    Public Function GetActulReurnValue(ByVal nCADstaID As Integer, ByVal nTrackID As Integer, ByVal sValue As String, ByVal sString As String) As String

        If sValue Is Nothing Then
            GetActulReurnValue = ""
        Else
            GetActulReurnValue = sValue
        End If
        If GetActulReurnValue.Trim = "" Then
            Select Case sString
                Case "车站或区间名称"
                    GetActulReurnValue = CADStaInf(nCADstaID).Track(nTrackID).sStaName

                Case "线段类型"
                    GetActulReurnValue = CADStaInf(nCADstaID).Track(nTrackID).sStyle

                Case "股道类型"
                    GetActulReurnValue = CADStaInf(nCADstaID).Track(nTrackID).sGuDaoStyle

                Case "股道用途"
                    GetActulReurnValue = CADStaInf(nCADstaID).Track(nTrackID).sGuDaoYongTu

                Case "线段长度"
                    GetActulReurnValue = CADStaInf(nCADstaID).Track(nTrackID).sngLength

                Case "股道或道岔编号"
                    GetActulReurnValue = CADStaInf(nCADstaID).Track(nTrackID).sTrackNum

                Case "控制模块"
                    GetActulReurnValue = CADStaInf(nCADstaID).Track(nTrackID).sControlNum

            End Select
        End If
        If GetActulReurnValue = "null" Then
            GetActulReurnValue = ""
        End If
    End Function

    Private Sub tolmnuMoveSignal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        nEditState = 3 '移动信号机
        Me.picSta.Cursor = Cursors.Cross
    End Sub

    Private Sub tolmnuDeleteSignal_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call DeleteSignal(nCADStainfID, nSignalID)
        Call DrawStaPic(Me.picSta, Me.picLine)
        nSignalID = 0
        Me.picSta.ContextMenuStrip = Nothing
    End Sub

    Private Sub tolmnuEditSignal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ReDim stuListItem(6)

        stuListItem(1).strItem = "信号机类型"
        stuListItem(1).strStyle = PropStrStyle.ComBox
        ReDim Preserve stuListItem(1).StrCmbList(7)
        stuListItem(1).StrCmbList(1) = "下行单显示"
        stuListItem(1).StrCmbList(2) = "下行双显示"
        stuListItem(1).StrCmbList(3) = "下行三显示"
        stuListItem(1).StrCmbList(4) = "上行单显示"
        stuListItem(1).StrCmbList(5) = "上行双显示"
        stuListItem(1).StrCmbList(6) = "上行三显示"
        stuListItem(1).StrCmbList(6) = "折返线阻挡信号机"
        stuListItem(1).StrCmbList(7) = "其他"
        stuListItem(1).strTxtList = CADStaInf(nCADStainfID).Signal(nSignalID).sStyle
        stuListItem(1).strItemCriterion = TextCriterion.NotRequired

        stuListItem(2).strItem = "对应线路编号"
        stuListItem(2).strStyle = PropStrStyle.TexBox
        stuListItem(2).strTxtList = CADStaInf(nCADStainfID).Signal(nSignalID).nTrackNum
        stuListItem(2).strItemCriterion = TextCriterion.NotRequired

        stuListItem(3).strItem = "对应道岔编号"
        stuListItem(3).strStyle = PropStrStyle.TexBox
        stuListItem(3).strTxtList = CADStaInf(nCADStainfID).Signal(nSignalID).nCrossNum
        stuListItem(3).strItemCriterion = TextCriterion.NotRequired

        stuListItem(4).strItem = "X坐标"
        stuListItem(4).strStyle = PropStrStyle.TexBox
        stuListItem(4).strTxtList = CADStaInf(nCADStainfID).Signal(nSignalID).X - sngLeftBaseX
        stuListItem(4).strItemCriterion = TextCriterion.NotRequired

        stuListItem(5).strItem = "Y坐标"
        stuListItem(5).strStyle = PropStrStyle.TexBox
        stuListItem(5).strTxtList = CADStaInf(nCADStainfID).Signal(nSignalID).Y
        stuListItem(5).strItemCriterion = TextCriterion.NotRequired

        stuListItem(6).strItem = "备注"
        stuListItem(6).strStyle = PropStrStyle.TexBox
        stuListItem(6).strTxtList = CADStaInf(nCADStainfID).Signal(nSignalID).sMemo
        stuListItem(6).strItemCriterion = TextCriterion.NotRequired

        Dim nf2 As New frmEditDataProperity
        nf2.ShowDialog()
        If nf2.blnOK = True Then
            Call EditCADStaSignal(nCADStainfID, nSignalID, _
                                stuListItem(1).strReturnValue, _
                                stuListItem(2).strReturnValue, _
                                stuListItem(3).strReturnValue, _
                                stuListItem(4).strReturnValue, _
                                stuListItem(5).strReturnValue, _
                                stuListItem(6).strReturnValue)
            Call DrawStaPic(Me.picSta, Me.picLine)
        End If
    End Sub

    Private Sub tolmnuMovePlatform_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        nEditState = 4 '移动站台
        Me.picSta.Cursor = Cursors.SizeAll
    End Sub

    Private Sub tolmnuDeletePlatform_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call DeletePlatform(nCADStainfID, nPlatformID)
        Call DrawStaPic(Me.picSta, Me.picLine)
        nPlatformID = 0
        Me.picSta.ContextMenuStrip = Nothing
    End Sub

    Private Sub tolmnuEditPlatform_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        ReDim stuListItem(8)
        stuListItem(1).strItem = "站台类型"
        stuListItem(1).strStyle = PropStrStyle.ComBox
        ReDim Preserve stuListItem(1).StrCmbList(2)
        stuListItem(1).StrCmbList(1) = "岛式站台"
        stuListItem(1).StrCmbList(2) = "侧式站台"
        stuListItem(1).strTxtList = CADStaInf(nCADStainfID).PlatForm(nPlatformID).sStyle
        stuListItem(1).strItemCriterion = TextCriterion.NotRequired

        stuListItem(2).strItem = "站台长"
        stuListItem(2).strStyle = PropStrStyle.TexBox
        stuListItem(2).strTxtList = CADStaInf(nCADStainfID).PlatForm(nPlatformID).nWidth
        stuListItem(2).strItemCriterion = TextCriterion.NotRequired

        stuListItem(3).strItem = "站台宽"
        stuListItem(3).strStyle = PropStrStyle.TexBox
        stuListItem(3).strTxtList = CADStaInf(nCADStainfID).PlatForm(nPlatformID).nHeight
        stuListItem(3).strItemCriterion = TextCriterion.NotRequired

        stuListItem(4).strItem = "X1坐标"
        stuListItem(4).strStyle = PropStrStyle.TexBox
        stuListItem(4).strTxtList = CADStaInf(nCADStainfID).PlatForm(nPlatformID).X1 - sngLeftBaseX
        stuListItem(4).strItemCriterion = TextCriterion.NotRequired

        stuListItem(5).strItem = "Y1坐标"
        stuListItem(5).strStyle = PropStrStyle.TexBox
        stuListItem(5).strTxtList = CADStaInf(nCADStainfID).PlatForm(nPlatformID).Y1
        stuListItem(5).strItemCriterion = TextCriterion.NotRequired

        stuListItem(6).strItem = "X2坐标"
        stuListItem(6).strStyle = PropStrStyle.TexBox
        stuListItem(6).strTxtList = CADStaInf(nCADStainfID).PlatForm(nPlatformID).X2 - sngLeftBaseX
        stuListItem(7).strItemCriterion = TextCriterion.NotRequired

        stuListItem(7).strItem = "Y2坐标"
        stuListItem(7).strStyle = PropStrStyle.TexBox
        stuListItem(7).strTxtList = CADStaInf(nCADStainfID).PlatForm(nPlatformID).Y2
        stuListItem(7).strItemCriterion = TextCriterion.NotRequired

        stuListItem(8).strItem = "备注"
        stuListItem(8).strStyle = PropStrStyle.TexBox
        stuListItem(8).strTxtList = CADStaInf(nCADStainfID).PlatForm(nPlatformID).sMemo
        stuListItem(8).strItemCriterion = TextCriterion.NotRequired

        Dim nf2 As New frmEditDataProperity
        nf2.ShowDialog()
        If nf2.blnOK = True Then
            Call EditCADStaPlatform(nCADStainfID, nPlatformID, _
                                stuListItem(1).strReturnValue, _
                                stuListItem(2).strReturnValue, _
                                stuListItem(3).strReturnValue, _
                                stuListItem(4).strReturnValue, _
                                stuListItem(5).strReturnValue, _
                                stuListItem(6).strReturnValue, _
                                stuListItem(7).strReturnValue, _
                                stuListItem(8).strReturnValue)
            Call DrawStaPic(Me.picSta, Me.picLine)
        End If
    End Sub

    Private Sub tolmnuMoveFontText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        nEditState = 5 '移动字体
        Me.picSta.Cursor = Cursors.SizeAll
    End Sub

    Private Sub tolmnuEditFontTextInf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ReDim stuListItem(9)
        stuListItem(1).strItem = "文字内容"
        stuListItem(1).strStyle = PropStrStyle.TexBox
        stuListItem(1).strTxtList = CADStaInf(nCADStainfID).FontText(nFontTextID).sText
        stuListItem(1).strItemCriterion = TextCriterion.NotEmpty

        stuListItem(2).strItem = "字体名称"
        stuListItem(2).strStyle = PropStrStyle.TexBox
        stuListItem(2).strTxtList = CADStaInf(nCADStainfID).FontText(nFontTextID).FontName
        stuListItem(2).strItemCriterion = TextCriterion.NotRequired

        stuListItem(3).strItem = "字体大小"
        ReDim Preserve stuListItem(3).StrCmbList(20)
        Dim i As Integer
        For i = 1 To 20
            stuListItem(3).StrCmbList(i) = i.ToString
        Next
        stuListItem(3).strStyle = PropStrStyle.ComBox
        stuListItem(3).strTxtList = CADStaInf(nCADStainfID).FontText(nFontTextID).FontSize
        stuListItem(3).strItemCriterion = TextCriterion.NotRequired

        stuListItem(4).strItem = "字体斜体"
        ReDim Preserve stuListItem(4).StrCmbList(2)
        stuListItem(4).StrCmbList(1) = "True"
        stuListItem(4).StrCmbList(2) = "False"
        stuListItem(4).strStyle = PropStrStyle.ComBox
        stuListItem(4).strTxtList = CADStaInf(nCADStainfID).FontText(nFontTextID).Italic
        stuListItem(4).strItemCriterion = TextCriterion.NotRequired

        stuListItem(5).strItem = "字体粗体"
        ReDim Preserve stuListItem(5).StrCmbList(2)
        stuListItem(5).StrCmbList(1) = "True"
        stuListItem(5).StrCmbList(2) = "False"
        stuListItem(5).strStyle = PropStrStyle.ComBox
        stuListItem(5).strTxtList = CADStaInf(nCADStainfID).FontText(nFontTextID).Bold
        stuListItem(5).strItemCriterion = TextCriterion.NotRequired

        stuListItem(6).strItem = "字体颜色"
        stuListItem(6).strStyle = PropStrStyle.colorBox
        stuListItem(6).strTxtList = CADStaInf(nCADStainfID).FontText(nFontTextID).FontColor.ToArgb
        stuListItem(6).strItemCriterion = TextCriterion.NotRequired

        stuListItem(7).strItem = "X坐标"
        stuListItem(7).strStyle = PropStrStyle.TexBox
        stuListItem(7).strTxtList = CADStaInf(nCADStainfID).FontText(nFontTextID).X - sngLeftBaseX
        stuListItem(7).strItemCriterion = TextCriterion.NotRequired

        stuListItem(8).strItem = "Y坐标"
        stuListItem(8).strStyle = PropStrStyle.TexBox
        stuListItem(8).strTxtList = CADStaInf(nCADStainfID).FontText(nFontTextID).Y
        stuListItem(8).strItemCriterion = TextCriterion.NotRequired

        stuListItem(9).strItem = "备注"
        stuListItem(9).strStyle = PropStrStyle.TexBox
        stuListItem(9).strTxtList = CADStaInf(nCADStainfID).FontText(nFontTextID).sMemo
        stuListItem(9).strItemCriterion = TextCriterion.NotRequired

        Dim nf2 As New frmEditDataProperity
        nf2.ShowDialog()
        If nf2.blnOK = True Then
            Call EditCADStaFontText(nCADStainfID, _
                                nFontTextID, _
                                stuListItem(1).strReturnValue, _
                                stuListItem(2).strReturnValue, _
                                stuListItem(3).strReturnValue, _
                                stuListItem(4).strReturnValue, _
                                stuListItem(5).strReturnValue, _
                                stuListItem(6).strReturnValue, _
                                stuListItem(7).strReturnValue, _
                                stuListItem(8).strReturnValue, _
                                stuListItem(9).strReturnValue)
            Call DrawStaPic(Me.picSta, Me.picLine)
        End If
    End Sub

    Private Sub tolmnuDeleteFontText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call DeleteFontText(nCADStainfID, nFontTextID)
        Call DrawStaPic(Me.picSta, Me.picLine)
        nPlatformID = 0
        Me.picSta.ContextMenuStrip = Nothing
    End Sub

    Private Sub 自动连接该站线段AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("确定重新连接所有线段吗？该操作将会清空原有的连接信息。", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2, "确认操作") = MsgBoxResult.Cancel Then Exit Sub

        Dim i As Integer
        Dim k As Integer
        'Dim Str As String
        ''Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        'Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        For k = 1 To UBound(CADStaInf)
            For i = 1 To UBound(CADStaInf(k).Track)
                CADStaInf(k).Track(i).sLeftLink1 = "NuLL"
                CADStaInf(k).Track(i).sLeftLink2 = "NuLL"
                CADStaInf(k).Track(i).sLeftLink3 = "NuLL"
                Call SeekLinkTrackNum(CADStaInf(k).Track(i).sTrackCircuitNum, CADStaInf(k).Track(i).X1, CADStaInf(k).Track(i).Y1, CADStaInf(k).Track(i).X2, CADStaInf(k).Track(i).Y2, "左连接")
                If UBound(GetReturnValue) > 0 Then
                    If UBound(GetReturnValue) = 1 Then
                        CADStaInf(k).Track(i).sLeftLink1 = GetReturnValue(1)
                    ElseIf UBound(GetReturnValue) = 2 Then
                        CADStaInf(k).Track(i).sLeftLink1 = GetReturnValue(1)
                        CADStaInf(k).Track(i).sLeftLink2 = GetReturnValue(2)
                    ElseIf UBound(GetReturnValue) = 3 Then
                        CADStaInf(k).Track(i).sLeftLink1 = GetReturnValue(1)
                        CADStaInf(k).Track(i).sLeftLink2 = GetReturnValue(2)
                        CADStaInf(k).Track(i).sLeftLink3 = GetReturnValue(3)
                    End If
                End If

                CADStaInf(k).Track(i).sRightLink1 = "NuLL"
                CADStaInf(k).Track(i).sRightLink2 = "NuLL"
                CADStaInf(k).Track(i).sRightLink3 = "NuLL"
                Call SeekLinkTrackNum(CADStaInf(k).Track(i).sTrackCircuitNum, CADStaInf(k).Track(i).X2, CADStaInf(k).Track(i).Y2, CADStaInf(k).Track(i).X1, CADStaInf(k).Track(i).Y1, "右连接")
                If UBound(GetReturnValue) > 0 Then
                    If UBound(GetReturnValue) = 1 Then
                        CADStaInf(k).Track(i).sRightLink1 = GetReturnValue(1)
                    ElseIf UBound(GetReturnValue) = 2 Then
                        CADStaInf(k).Track(i).sRightLink1 = GetReturnValue(1)
                        CADStaInf(k).Track(i).sRightLink2 = GetReturnValue(2)
                    ElseIf UBound(GetReturnValue) = 3 Then
                        CADStaInf(k).Track(i).sRightLink1 = GetReturnValue(1)
                        CADStaInf(k).Track(i).sRightLink2 = GetReturnValue(2)
                        CADStaInf(k).Track(i).sRightLink3 = GetReturnValue(3)
                    End If
                End If
                'If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                'Str = "update 线段信息表 set " & _
                '        "左1连接 = '" & CADStaInf(k).Track(i).sLeftLink1 & "'," & _
                '        "左2连接 = '" & CADStaInf(k).Track(i).sLeftLink2 & "'," & _
                '        "左3连接 = '" & CADStaInf(k).Track(i).sLeftLink3 & "'," & _
                '        "右1连接 = '" & CADStaInf(k).Track(i).sRightLink1 & "'," & _
                '        "右2连接 = '" & CADStaInf(k).Track(i).sRightLink2 & "'," & _
                '        "右3连接 = '" & CADStaInf(k).Track(i).sRightLink3 & "'" & _
                '        "where ID = " & CADStaInf(k).Track(i).nID & ""
                'Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                'Mcom.ExecuteNonQuery()
            Next
        Next
        'MyConn.Close()
        MsgBox("自动联系完成！", , "提示")
    End Sub


    Private Sub ToolStripMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem4.Click
        Me.Close()
    End Sub

    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 自动连接该站线段AToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TolbtMultiLine.Click
        nDrawState = 6
        nEditState = 1
        Call SetOterBottonNotChecked()
        Me.TolbtMultiLine.Checked = True
    End Sub

    Private Sub TimerMovePic_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerMovePic.Tick
        Dim g As Graphics
        g = Graphics.FromImage(bmpOne)
        g.FillRectangle(Brushes.Black, 0, 0, Me.picSta.Width, Me.picSta.Height)
        Me.picSta.Image = Nothing '清空原来的图
        If nEditState = 6 Then '旋转
            Call RotateLine(CurRotateCenterPoint, nCurRotateAngle, nCurPicstaLeftX, nCurPicStaTopY, nCurShowScale)
            nForRoteAngle = nCurRotateAngle
            Call PrintStationPicture(Me.picSta, g, nCurPicstaLeftX, Me.picSta.Width * (1 / nCurShowScale), nCurPicStaTopY, Me.picSta.Height * (1 / nCurShowScale), nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)
        Else
            Call PrintStationPicture(Me.picSta, g, nCurPicstaLeftX - PicMoveX / nCurShowScale, Me.picSta.Width * (1 / nCurShowScale), nCurPicStaTopY - PicMoveY / nCurShowScale, Me.picSta.Height * (1 / nCurShowScale), nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)
        End If
        Me.picSta.Image = bmpOne
    End Sub

    '将其他按钮设为不能用
    Private Sub SetOterBottonNotChecked()
        Me.TolbtMultiLine.Checked = False
        Me.tolbtHandMove.Checked = False
        Me.picSta.Cursor = Cursors.Default
    End Sub

    Private Sub tolbtZoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tolbtZoom.Click
        Dim nFirScale As Single
        nFirScale = nCurShowScale
        nCurShowScale = nFirScale * (1 + sngZoomOrReduceValue)
        If nCurShowScale >= 3 Then
            nCurShowScale = 3
        End If
        Call ZoomPicStation(nFirScale, nCurShowScale, 0, 0, 0)
    End Sub

    Private Sub TolbtReduce_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TolbtReduce.Click
        Dim nFirScale As Single
        nFirScale = nCurShowScale
        nCurShowScale = nCurShowScale * (1 - sngZoomOrReduceValue)
        If nCurShowScale <= 0.1 Then
            nCurShowScale = 0.1
        Else
        End If
        Call ZoomPicStation(nFirScale, nCurShowScale, 0, 0, 0)
    End Sub

    '放大
    Private Sub ZoomPicStation(ByVal nFirScale As Single, ByVal nCurScale As Single, ByVal nState As Integer, ByVal MinX As Single, ByVal MinY As Single)
        ' If nFirScale = nCurScale Then Exit Sub
        nGridWidth = nFirGridWidth * nCurScale
        Dim DeltX As Single
        Dim DeltY As Single
        DeltX = (Me.picSta.Width / nFirScale - Me.picSta.Width / nCurScale) / 2
        DeltY = (Me.picSta.Height / nFirScale - Me.picSta.Height / nCurScale) / 2
        nCurPicstaLeftX = nCurPicstaLeftX + DeltX ' / nCurShowScale
        nCurPicStaTopY = nCurPicStaTopY + DeltY ' / nCurShowScale
        If nState = 1 Then '自适应
            nCurPicstaLeftX = MinX ' / nCurShowScale
            nCurPicStaTopY = MinY ' / nCurShowScale
        End If
        Call InputGridXY()
        Call RefreshPicSta(nCurPicstaLeftX, nCurPicStaTopY, Me.picSta, Me.picLine, nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)

        'nCurShowScale = nCurShowScale * (1 / (1 - sngZoomOrReduceValue))
        'Dim DeltX As Single
        'Dim DeltY As Single
        'DeltX = (Me.picSta.Width / (nCurShowScale * (1 - sngZoomOrReduceValue)) - Me.picSta.Width / nCurShowScale) / 2
        'DeltY = (Me.picSta.Height / (nCurShowScale * (1 - sngZoomOrReduceValue)) - Me.picSta.Height / nCurShowScale) / 2
        'nCurPicstaLeftX = nCurPicstaLeftX + DeltX ' / nCurShowScale
        'nCurPicStaTopY = nCurPicStaTopY + DeltY ' / nCurShowScale
        'Call RefreshPicSta(nCurPicstaLeftX, nCurPicStaTopY)
    End Sub
    Private Sub tolBtShowGuDaoNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tolBtShowGuDaoNum.Click
        If Me.tolBtShowGuDaoNum.Checked = True Then
            Me.tolBtShowGuDaoNum.Checked = False
        Else
            Me.tolBtShowGuDaoNum.Checked = True
        End If
        Call RefreshPicSta(nCurPicstaLeftX, nCurPicStaTopY, Me.picSta, Me.picLine, nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)
    End Sub

    Private Sub tolbtShowTrackNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tolbtShowTrackNum.Click
        If Me.tolbtShowTrackNum.Checked = True Then
            Me.tolbtShowTrackNum.Checked = False
        Else
            Me.tolbtShowTrackNum.Checked = True
        End If
        Call RefreshPicSta(nCurPicstaLeftX, nCurPicStaTopY, Me.picSta, Me.picLine, nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)
    End Sub

    Private Sub tobBtShowCrossingNum_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tolBtShowCrossingNum.Click
        If Me.tolBtShowCrossingNum.Checked = True Then
            Me.tolBtShowCrossingNum.Checked = False
        Else
            Me.tolBtShowCrossingNum.Checked = True
        End If
        Call RefreshPicSta(nCurPicstaLeftX, nCurPicStaTopY, Me.picSta, Me.picLine, nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)
    End Sub

    Private Sub tolbtShowElseInfor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tolbtShowElseInfor.Click
        If Me.tolbtShowElseInfor.Checked = True Then
            Me.tolbtShowElseInfor.Checked = False
        Else
            Me.tolbtShowElseInfor.Checked = True
        End If
        Call RefreshPicSta(nCurPicstaLeftX, nCurPicStaTopY, Me.picSta, Me.picLine, nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)
    End Sub

    '旋转
    Public Sub RotateLine(ByVal CenterPoint As Point, ByVal RotateAngle As Single, ByVal sngLeftX As Single, ByVal sngTopY As Single, ByVal sScale As Single)
        Dim i As Integer
        Dim TmpX1, TmpX2, TmpY1, TmpY2 As Single
        Dim tmpID As Integer

        For i = 1 To UBound(tmpRotateTrackInf)
            tmpID = curSelectTrackID(i)
            TmpX1 = tmpRotateTrackInf(i).X1
            TmpY1 = tmpRotateTrackInf(i).Y1
            TmpX2 = tmpRotateTrackInf(i).X2
            TmpY2 = tmpRotateTrackInf(i).Y2
            tmpTrackInf(tmpID).X1 = TmpX1 * Cos(RotateAngle) - TmpY1 * Sin(RotateAngle) + CenterPoint.X * (1 - Cos(RotateAngle)) + CenterPoint.Y * Sin(RotateAngle)
            tmpTrackInf(tmpID).Y1 = TmpX1 * Sin(RotateAngle) + Cos(RotateAngle) * TmpY1 + CenterPoint.Y * (1 - Cos(RotateAngle)) - CenterPoint.X * Sin(RotateAngle)
            tmpTrackInf(tmpID).X2 = TmpX2 * Cos(RotateAngle) - TmpY2 * Sin(RotateAngle) + CenterPoint.X * (1 - Cos(RotateAngle)) + CenterPoint.Y * Sin(RotateAngle)
            tmpTrackInf(tmpID).Y2 = TmpX2 * Sin(RotateAngle) + Cos(RotateAngle) * TmpY2 + CenterPoint.Y * (1 - Cos(RotateAngle)) - CenterPoint.X * Sin(RotateAngle)
            Call EditTrackCoordInf(tmpID, sngLeftX + tmpTrackInf(tmpID).X1 / sScale, sngLeftX + tmpTrackInf(tmpID).X2 / sScale, sngTopY + tmpTrackInf(tmpID).Y1 / sScale, sngTopY + tmpTrackInf(tmpID).Y2 / sScale)
        Next
    End Sub

    '选中线的旋转
    Private Sub RotateSelectTrack(ByVal sngAngle As Single)
        Dim i As Integer
        ReDim tmpRotateTrackInf(UBound(curSelectTrackID))
        For i = 1 To UBound(curSelectTrackID)
            tmpRotateTrackInf(i).nStaID = tmpTrackInf(curSelectTrackID(i)).nStaID
            tmpRotateTrackInf(i).nTrackID = tmpTrackInf(curSelectTrackID(i)).nTrackID
            tmpRotateTrackInf(i).X1 = tmpTrackInf(curSelectTrackID(i)).X1
            tmpRotateTrackInf(i).Y1 = tmpTrackInf(curSelectTrackID(i)).Y1
            tmpRotateTrackInf(i).X2 = tmpTrackInf(curSelectTrackID(i)).X2
            tmpRotateTrackInf(i).Y2 = tmpTrackInf(curSelectTrackID(i)).Y2
        Next
        Call RotateLine(CurRotateCenterPoint, sngAngle, nCurPicstaLeftX, nCurPicStaTopY, nCurShowScale)
        Call RefreshPicSta(nCurPicstaLeftX, nCurPicStaTopY, Me.picSta, Me.picLine, nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)
    End Sub

    Private Sub tolStripCmbShowScale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tolStripCmbShowScale.SelectedIndexChanged
        Dim nFirScale As Single
        nFirScale = nCurShowScale
        Dim nCurScale As Single
        Dim minX As Single
        Dim maxX As Single
        Dim minY As Single
        Dim maxY As Single
        Dim nWidthScale As Single
        Dim nHeightScale As Single
        nCurScale = 0
        Dim nState As Integer
        Dim i As Integer
        minX = 0
        minY = 0
        Select Case Me.tolStripCmbShowScale.Text.Trim
            Case "200%"
                nCurScale = 2
                nState = 0
            Case "150%"
                nCurScale = 1.5
                nState = 0
            Case "100%"
                nCurScale = 1
                nState = 0
            Case "75%"
                nCurScale = 0.75
                nState = 0
            Case "50%"
                nCurScale = 0.5
                nState = 0
            Case "25%"
                nCurScale = 0.25
                nState = 0
            Case "适应页宽"
                If UBound(TrackInf) > 0 Then
                    minX = 10000000
                    maxX = -10000000
                    For i = 1 To UBound(TrackInf)
                        minX = Minimal(minX, TrackInf(i).X1)
                        minX = Minimal(minX, TrackInf(i).X2)
                        maxX = Maximal(maxX, TrackInf(i).X1)
                        maxX = Maximal(maxX, TrackInf(i).X2)
                    Next
                    If (maxX - minX) > 0 Then
                        nWidthScale = (Me.picSta.Width / (maxX - minX)) * 0.9
                    End If
                    nCurScale = nWidthScale
                    nState = 1
                    minX = minX - (Me.picSta.Width - (maxX - minX) * nCurScale) / (2 * nCurScale)
                    minY = nCurPicStaTopY 'minY - (Me.picSta.Height - (maxY - minY) * nCurScale) / (2 * nCurScale)
                End If

            Case "适应页高"
                If UBound(TrackInf) > 0 Then
                    minY = 1000000
                    maxY = -10000000
                    For i = 1 To UBound(TrackInf)
                        minY = Minimal(minY, TrackInf(i).Y1)
                        minY = Minimal(minY, TrackInf(i).Y2)
                        maxY = Maximal(maxY, TrackInf(i).Y1)
                        maxY = Maximal(maxY, TrackInf(i).Y2)
                    Next
                    If (maxY - minY) > 0 Then
                        nHeightScale = (Me.picSta.Height / (maxY - minY)) * 0.9
                    End If
                    nCurScale = nHeightScale
                    nState = 1
                    minX = nCurPicstaLeftX 'minX - (Me.picSta.Width - (maxX - minX) * nCurScale) / (2 * nCurScale)
                    minY = minY - (Me.picSta.Height - (maxY - minY) * nCurScale) / (2 * nCurScale)
                End If
            Case "适应页面"
                If UBound(TrackInf) > 0 Then
                    minX = 1000000
                    maxX = -10000000
                    For i = 1 To UBound(TrackInf)
                        minX = Minimal(minX, TrackInf(i).X1)
                        minX = Minimal(minX, TrackInf(i).X2)
                        maxX = Maximal(maxX, TrackInf(i).X1)
                        maxX = Maximal(maxX, TrackInf(i).X2)
                    Next
                    If (maxX - minX) > 0 Then
                        nWidthScale = (Me.picSta.Width / (maxX - minX)) * 0.9
                    End If

                    minY = 1000000
                    maxY = -10000000
                    For i = 1 To UBound(TrackInf)
                        minY = Minimal(minY, TrackInf(i).Y1)
                        minY = Minimal(minY, TrackInf(i).Y2)
                        maxY = Maximal(maxY, TrackInf(i).Y1)
                        maxY = Maximal(maxY, TrackInf(i).Y2)
                    Next
                    If (maxY - minY) > 0 Then
                        nHeightScale = (Me.picSta.Height / (maxY - minY)) * 0.9
                    End If
                    If nWidthScale > nHeightScale Then
                        nCurScale = nHeightScale
                    Else
                        nCurScale = nWidthScale
                    End If
                    minX = minX - (Me.picSta.Width - (maxX - minX) * nCurScale) / (2 * nCurScale)
                    minY = minY - (Me.picSta.Height - (maxY - minY) * nCurScale) / (2 * nCurScale)
                    nState = 1
                End If
        End Select

        If nCurScale > 0 Then
            nCurShowScale = nCurScale
            Call ZoomPicStation(nFirScale, nCurScale, nState, minX, minY)
        End If
    End Sub

    Private Sub 自由旋转FToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 自由旋转FToolStripMenuItem1_Click(Nothing, Nothing)
    End Sub

    '设置中心旋转点
    Private Sub GetRotateCenterPoint()
        Dim i As Integer
        Dim MinX As Single
        Dim MaxX As Single
        Dim MinY As Single
        Dim MaxY As Single
        MinX = 10000000
        MaxX = -10000000
        MinY = 10000000
        MaxY = -10000000
        Dim tmpXY As Single
        Dim tmpID As Integer
        For i = 1 To UBound(curSelectTrackID)
            tmpID = curSelectTrackID(i)
            tmpXY = Minimal(tmpTrackInf(tmpID).X1, tmpTrackInf(tmpID).X2)
            MinX = Minimal(MinX, tmpXY)
            tmpXY = Maximal(tmpTrackInf(tmpID).X1, tmpTrackInf(tmpID).X2)
            MaxX = Maximal(MaxX, tmpXY)

            tmpXY = Minimal(tmpTrackInf(tmpID).Y1, tmpTrackInf(tmpID).Y2)
            MinY = Minimal(MinY, tmpXY)
            tmpXY = Maximal(tmpTrackInf(tmpID).Y1, tmpTrackInf(tmpID).Y2)
            MaxY = Maximal(MaxY, tmpXY)
        Next
        CurRotateCenterPoint.X = MinX + (MaxX - MinX) / 2
        CurRotateCenterPoint.Y = MinY + (MaxY - MinY) / 2
    End Sub
    Private Sub 向右旋转90ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 向右旋转90度ToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 向左旋转90度ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 向左旋转90度LToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 撤销UToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ReDim curSelectTrackID(0)
        If CADUndoSeq.nRedoID = 0 Then
            CADUndoSeq.nRedoID = CADUndoSeq.nCurUndoID
        End If
        Call UndoTrackinf(1)
        Call CADUndoAndRedoMenuSet()

    End Sub

    '撤销和重复
    Public Sub UndoTrackinf(ByVal nState As Integer)
        Dim nID As Integer

        If nState = 1 Then '撤销
            nID = CADUndoSeq.nUpID
            If CADUndoSeq.nStep >= CADformPara.nMaxUndoID - 1 Then
                nID = 0
            Else
                CADUndoSeq.nStep = CADUndoSeq.nStep + 1
            End If
        Else '重复
            If CADUndoSeq.nStep > 0 Then
                nID = CADUndoSeq.nDownID
                CADUndoSeq.nStep = CADUndoSeq.nStep - 1
            End If
        End If

        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        If nID > 0 Then
            ReDim CADStaInf(UBound(CADformUndoInf(nID).CADStaInfor))
            For i = 1 To UBound(CADformUndoInf(nID).CADStaInfor)
                With CADStaInf(i)
                    .nDownID = CADformUndoInf(nID).CADStaInfor(i).nDownID
                    .nLineID = CADformUndoInf(nID).CADStaInfor(i).nLineID
                    .nStaOrSecID = CADformUndoInf(nID).CADStaInfor(i).nStaOrSecID
                    .sLineName = CADformUndoInf(nID).CADStaInfor(i).sLineName
                    .sStaCode = CADformUndoInf(nID).CADStaInfor(i).sStaCode
                    .sStaName = CADformUndoInf(nID).CADStaInfor(i).sStaName
                    .sStaOrSec = CADformUndoInf(nID).CADStaInfor(i).sStaOrSec
                    .ContolScheme = CADformUndoInf(nID).CADStaInfor(i).ContolScheme
                    ReDim CADStaInf(i).Track(UBound(CADformUndoInf(nID).CADStaInfor(i).Track))
                    For j = 1 To UBound(CADformUndoInf(nID).CADStaInfor(i).Track)
                        .Track(j).nDelete = CADformUndoInf(nID).CADStaInfor(i).Track(j).nDelete
                        .Track(j).nID = CADformUndoInf(nID).CADStaInfor(i).Track(j).nID
                        .Track(j).nNum = CADformUndoInf(nID).CADStaInfor(i).Track(j).nNum
                        .Track(j).nStaID = CADformUndoInf(nID).CADStaInfor(i).Track(j).nStaID
                        .Track(j).sControlNum = CADformUndoInf(nID).CADStaInfor(i).Track(j).sControlNum
                        .Track(j).sGuDaoStyle = CADformUndoInf(nID).CADStaInfor(i).Track(j).sGuDaoStyle
                        .Track(j).sGuDaoUseSeq = CADformUndoInf(nID).CADStaInfor(i).Track(j).sGuDaoUseSeq
                        .Track(j).sGuDaoYongTu = CADformUndoInf(nID).CADStaInfor(i).Track(j).sGuDaoYongTu
                        .Track(j).sLeftLink1 = CADformUndoInf(nID).CADStaInfor(i).Track(j).sLeftLink1
                        .Track(j).sLeftLink2 = CADformUndoInf(nID).CADStaInfor(i).Track(j).sLeftLink2
                        .Track(j).sLeftLink3 = CADformUndoInf(nID).CADStaInfor(i).Track(j).sLeftLink3
                        .Track(j).sMemo = CADformUndoInf(nID).CADStaInfor(i).Track(j).sMemo
                        .Track(j).sRightLink1 = CADformUndoInf(nID).CADStaInfor(i).Track(j).sRightLink1
                        .Track(j).sRightLink2 = CADformUndoInf(nID).CADStaInfor(i).Track(j).sRightLink2
                        .Track(j).sRightLink3 = CADformUndoInf(nID).CADStaInfor(i).Track(j).sRightLink3
                        .Track(j).sStaName = CADformUndoInf(nID).CADStaInfor(i).Track(j).sStaName
                        .Track(j).sStyle = CADformUndoInf(nID).CADStaInfor(i).Track(j).sStyle
                        .Track(j).sTrackCircuitNum = CADformUndoInf(nID).CADStaInfor(i).Track(j).sTrackCircuitNum
                        .Track(j).sTrackNum = CADformUndoInf(nID).CADStaInfor(i).Track(j).sTrackNum
                        .Track(j).X1 = CADformUndoInf(nID).CADStaInfor(i).Track(j).X1
                        .Track(j).X2 = CADformUndoInf(nID).CADStaInfor(i).Track(j).X2
                        .Track(j).Y1 = CADformUndoInf(nID).CADStaInfor(i).Track(j).Y1
                        .Track(j).Y2 = CADformUndoInf(nID).CADStaInfor(i).Track(j).Y2
                    Next


                    ReDim CADStaInf(i).ContolScheme(UBound(CADformUndoInf(nID).CADStaInfor(i).ContolScheme))
                    For j = 1 To UBound(CADformUndoInf(nID).CADStaInfor(i).ContolScheme)
                        ReDim .ContolScheme(j).SModelName(UBound(CADformUndoInf(nID).CADStaInfor(i).ContolScheme(j).SModelName))
                        ReDim .ContolScheme(j).STrackNum(UBound(CADformUndoInf(nID).CADStaInfor(i).ContolScheme(j).STrackNum))
                        .ContolScheme(j).sSchemeName = CADformUndoInf(nID).CADStaInfor(i).ContolScheme(j).sSchemeName
                        .ContolScheme(j).sStaName = CADformUndoInf(nID).CADStaInfor(i).ContolScheme(j).sStaName
                        .ContolScheme(j).stringTrackNum = CADformUndoInf(nID).CADStaInfor(i).ContolScheme(j).stringTrackNum
                        For k = 1 To UBound(CADformUndoInf(nID).CADStaInfor(i).ContolScheme(j).SModelName)
                            .ContolScheme(j).SModelName(k) = CADformUndoInf(nID).CADStaInfor(i).ContolScheme(j).SModelName(k)
                            .ContolScheme(j).STrackNum(k) = CADformUndoInf(nID).CADStaInfor(i).ContolScheme(j).STrackNum(k)
                        Next
                    Next

                End With
            Next

            CADUndoSeq.nCurUndoID = nID
            If nID = 1 Then
                CADUndoSeq.nUpID = CADformPara.nMaxUndoID
            Else
                CADUndoSeq.nUpID = nID - 1
            End If
            If nID = CADformPara.nMaxUndoID Then
                CADUndoSeq.nDownID = 1
            Else
                CADUndoSeq.nDownID = nID + 1
            End If
            Call InputTrackInf()
            Call RefreshPicSta(nCurPicstaLeftX, nCurPicStaTopY, Me.picSta, Me.picLine, nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)
        End If
    End Sub

    Private Sub CADUndoAndRedoMenuSet()
      
    End Sub

    Private Sub 重复RToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TimeTablePara.nPubTrain = 0
        Call UndoTrackinf(0)
        Call CADUndoAndRedoMenuSet()
    End Sub

    '添加一个操作信息
    Public Sub CADaddOneUndoInf()
        Dim nNum As Integer
        nNum = 0
        nNum = CADUndoSeq.nCurUndoID + 1
        If nNum > CADformPara.nMaxUndoID Then
            nNum = 1
        End If
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        ReDim CADformUndoInf(nNum).CADStaInfor(UBound(CADStaInf))
        For i = 1 To UBound(CADStaInf)
            With CADformUndoInf(nNum).CADStaInfor(i)
                .nDownID = CADStaInf(i).nDownID
                .nLineID = CADStaInf(i).nLineID
                .nStaOrSecID = CADStaInf(i).nStaOrSecID
                .sLineName = CADStaInf(i).sLineName
                .sStaCode = CADStaInf(i).sStaCode
                .sStaName = CADStaInf(i).sStaName
                .sStaOrSec = CADStaInf(i).sStaOrSec
                .ContolScheme = CADStaInf(i).ContolScheme
                ReDim CADformUndoInf(nNum).CADStaInfor(i).Track(UBound(CADStaInf(i).Track))
                For j = 1 To UBound(CADStaInf(i).Track)
                    .Track(j).nDelete = CADStaInf(i).Track(j).nDelete
                    .Track(j).nID = CADStaInf(i).Track(j).nID
                    .Track(j).nNum = CADStaInf(i).Track(j).nNum
                    .Track(j).nStaID = CADStaInf(i).Track(j).nStaID
                    .Track(j).sControlNum = CADStaInf(i).Track(j).sControlNum
                    .Track(j).sGuDaoStyle = CADStaInf(i).Track(j).sGuDaoStyle
                    .Track(j).sGuDaoUseSeq = CADStaInf(i).Track(j).sGuDaoUseSeq
                    .Track(j).sGuDaoYongTu = CADStaInf(i).Track(j).sGuDaoYongTu
                    .Track(j).sLeftLink1 = CADStaInf(i).Track(j).sLeftLink1
                    .Track(j).sLeftLink2 = CADStaInf(i).Track(j).sLeftLink2
                    .Track(j).sLeftLink3 = CADStaInf(i).Track(j).sLeftLink3
                    .Track(j).sMemo = CADStaInf(i).Track(j).sMemo
                    .Track(j).sRightLink1 = CADStaInf(i).Track(j).sRightLink1
                    .Track(j).sRightLink2 = CADStaInf(i).Track(j).sRightLink2
                    .Track(j).sRightLink3 = CADStaInf(i).Track(j).sRightLink3
                    .Track(j).sStaName = CADStaInf(i).Track(j).sStaName
                    .Track(j).sStyle = CADStaInf(i).Track(j).sStyle
                    .Track(j).sTrackCircuitNum = CADStaInf(i).Track(j).sTrackCircuitNum
                    .Track(j).sTrackNum = CADStaInf(i).Track(j).sTrackNum
                    .Track(j).X1 = CADStaInf(i).Track(j).X1
                    .Track(j).X2 = CADStaInf(i).Track(j).X2
                    .Track(j).Y1 = CADStaInf(i).Track(j).Y1
                    .Track(j).Y2 = CADStaInf(i).Track(j).Y2
                Next

                ReDim CADformUndoInf(nNum).CADStaInfor(i).ContolScheme(UBound(CADStaInf(i).ContolScheme))
                For j = 1 To UBound(CADStaInf(i).ContolScheme)
                    ReDim .ContolScheme(j).SModelName(UBound(CADStaInf(i).ContolScheme(j).SModelName))
                    ReDim .ContolScheme(j).STrackNum(UBound(CADStaInf(i).ContolScheme(j).STrackNum))
                    .ContolScheme(j).sSchemeName = CADStaInf(i).ContolScheme(j).sSchemeName
                    .ContolScheme(j).sStaName = CADStaInf(i).ContolScheme(j).sStaName
                    .ContolScheme(j).stringTrackNum = CADStaInf(i).ContolScheme(j).stringTrackNum
                    For k = 1 To UBound(CADStaInf(i).ContolScheme(j).SModelName)
                        .ContolScheme(j).SModelName(k) = CADStaInf(i).ContolScheme(j).SModelName(k)
                        .ContolScheme(j).STrackNum(k) = CADStaInf(i).ContolScheme(j).STrackNum(k)
                    Next
                Next

            End With
        Next
        ' Array.Copy(CADStaInf, CADformUndoInf(nNum).CADStaInfor, UBound(CADStaInf))
        ' memcpy()
        CADUndoSeq.nUpID = CADUndoSeq.nCurUndoID
        CADUndoSeq.nDownID = 0
        CADUndoSeq.nCurUndoID = nNum
        CADUndoSeq.nStep = 0
        Call CADUndoAndRedoMenuSet()
    End Sub

    Private Sub 删除DToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call DeleteTrack()
        Call InputTrackInf()
        Call DrawStaPic(Me.picSta, Me.picLine)
        nTrackID = 0
        Me.picSta.ContextMenuStrip = Nothing
        Call CADaddOneUndoInf()
    End Sub

    Private Sub 剪切CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 复制CToolStripMenuItem_Click(Nothing, Nothing)
        Call 删除DToolStripMenuItem_Click(Nothing, Nothing)
        Call CADaddOneUndoInf()
    End Sub

    Private Sub 复制CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ReDim CopyTrackinf(UBound(curSelectTrackID))
        Dim i As Integer
        Dim nStaID As Integer
        Dim nTrackID As Integer
        For i = 1 To UBound(curSelectTrackID)
            nStaID = tmpTrackInf(curSelectTrackID(i)).nStaID
            nTrackID = tmpTrackInf(curSelectTrackID(i)).nTrackID
            CopyTrackinf(i).nDelete = 0
            CopyTrackinf(i).nStaID = nStaID
            CopyTrackinf(i).nNum = CADStaInf(nStaID).Track(nTrackID).nNum
            CopyTrackinf(i).sControlNum = CADStaInf(nStaID).Track(nTrackID).sControlNum
            CopyTrackinf(i).sGuDaoStyle = CADStaInf(nStaID).Track(nTrackID).sGuDaoStyle
            CopyTrackinf(i).sGuDaoUseSeq = CADStaInf(nStaID).Track(nTrackID).sGuDaoUseSeq
            CopyTrackinf(i).sGuDaoYongTu = CADStaInf(nStaID).Track(nTrackID).sGuDaoYongTu
            CopyTrackinf(i).sMemo = CADStaInf(nStaID).Track(nTrackID).sMemo
            CopyTrackinf(i).sStaName = CADStaInf(nStaID).Track(nTrackID).sStaName
            CopyTrackinf(i).sStyle = CADStaInf(nStaID).Track(nTrackID).sStyle
            CopyTrackinf(i).sTrackNum = CADStaInf(nStaID).Track(nTrackID).sTrackNum
            CopyTrackinf(i).X1 = CADStaInf(nStaID).Track(nTrackID).X1
            CopyTrackinf(i).Y1 = CADStaInf(nStaID).Track(nTrackID).Y1
            CopyTrackinf(i).X2 = CADStaInf(nStaID).Track(nTrackID).X2
            CopyTrackinf(i).Y2 = CADStaInf(nStaID).Track(nTrackID).Y2
        Next
        Call CopyAndPasteMnuSet()

    End Sub
    Private Sub CopyAndPasteMnuSet()
       
    End Sub

    Private Sub 粘贴VToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim nMoveX, nMoveY As Single
        nMoveX = 20
        nMoveY = 20
        Dim nCurID() As String
        ReDim curSelectTrackID(0)
        ReDim nCurID(UBound(CopyTrackinf))
        Dim ntmpTrID As Integer
        For i = 1 To UBound(CopyTrackinf)
            nCurID(i) = addCADStaTrack(CopyTrackinf(i).nStaID, CopyTrackinf(i).sStyle, CopyTrackinf(i).sGuDaoStyle, _
                                        CopyTrackinf(i).sGuDaoYongTu, CopyTrackinf(i).sGuDaoUseSeq, _
                                        CopyTrackinf(i).sngLength, CopyTrackinf(i).sTrackNum, CopyTrackinf(i).X1 + nMoveX, CopyTrackinf(i).Y1 + nMoveY, _
                                         CopyTrackinf(i).X2 + nMoveX, CopyTrackinf(i).Y2 + nMoveY, "", "", "", "", "", "", "", "1", "")

        Next
        Call RefreshPicSta(Me.nCurPicstaLeftX, Me.nCurPicStaTopY, Me.picSta, Me.picLine, nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)
        For i = 1 To UBound(nCurID)
            ntmpTrID = GetTmpTrackInfIDFromCirNum(nCurID(i))
            If ntmpTrID > 0 Then
                ReDim Preserve curSelectTrackID(UBound(curSelectTrackID) + 1)
                curSelectTrackID(UBound(curSelectTrackID)) = ntmpTrID
            End If
        Next
        Call SelectLineStyle(1)
        Call CADaddOneUndoInf()
    End Sub

    Private Sub 剪切ToolStripButton10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 剪切CToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub ToolStripButton5_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 复制CToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 删除ToolStripButton11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 删除DToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 粘贴ToolStripButton13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 粘贴VToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub tolbtDrawLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If nCADStainfID = 0 Then
            MsgBox("请先在左边选择车站名称!", , "提示")
            Exit Sub
        End If
        nDrawState = 1
        nClickState = 1
        Me.picSta.Cursor = Cursors.Cross
    End Sub

    Private Sub tolbtDrawSignal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        nDrawState = 2
        'nClickState = 1
        Me.picSta.Cursor = Cursors.Default
    End Sub

    Private Sub tolBtDrawPlatform_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        nDrawState = 3
        sngClickX = 0
        sngClickY = 0
        Me.picSta.Cursor = Cursors.Cross
    End Sub

    Private Sub tolbtDrawString_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        nDrawState = 4
        Me.picSta.Cursor = Cursors.IBeam
    End Sub

    Private Sub tolbtRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tolbtRefresh.Click
        Call RefreshPic()
    End Sub
    Private Sub RefreshPic()
        Call InputTrackInf()
        Call RefreshPicSta(nCurPicstaLeftX, nCurPicStaTopY, Me.picSta, Me.picLine, nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)
    End Sub
    Private Sub tolbtSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 保存SToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 撤销tolStripUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 撤销UToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 重复tolStripRedo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 重复RToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 复制ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 复制CToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 剪切XToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 剪切CToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 粘贴VToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 粘贴VToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub tolbtHandMove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tolbtHandMove.Click
        ReDim curSelectTrackID(0)
        Me.tolbtHandMove.Checked = True
        nDrawState = 5 '移动底图
        Me.picSta.Cursor = Cursors.Hand
        bmpOne = New Bitmap(Me.picSta.Width, Me.picSta.Height)
        Dim g As Graphics
        g = Graphics.FromImage(bmpOne)
        g.FillRectangle(Brushes.Black, 0, 0, Me.picSta.Width, Me.picSta.Height)
    End Sub

    Private Sub 移动MToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        nEditState = 1 '移线
        Me.picSta.Cursor = Cursors.SizeAll
    End Sub

    Private Sub TolbtMovePic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 移动MToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub TolbtRotate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 自由旋转FToolStripMenuItem1_Click(Nothing, Nothing)
    End Sub

    Private Sub 自由旋转FToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        nDrawState = 0
        nEditState = 6 '旋转
        nForRoteAngle = 0
        Me.picSta.Cursor = Cursors.SizeNS
        bmpOne = New Bitmap(Me.picSta.Width, Me.picSta.Height)
        Dim g As Graphics
        g = Graphics.FromImage(bmpOne)
        g.FillRectangle(Brushes.Black, 0, 0, Me.picSta.Width, Me.picSta.Height)
        Call GetRotateCenterPoint()
    End Sub

    Private Sub 向右旋转90度ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call GetRotateCenterPoint()
        Call RotateSelectTrack(Math.PI / 2)
        Call RefreshCADstainfXandYCoord()
        Call SelectLineStyle(1)
    End Sub

    Private Sub 向左旋转90度LToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call GetRotateCenterPoint()
        Call RotateSelectTrack(-Math.PI / 2)
        Call RefreshCADstainfXandYCoord()
        Call SelectLineStyle(1)
    End Sub

    Private Sub 保存SToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call InputCADstaInfToDataBase()
    End Sub

    Private Sub tolCmbControlScheme_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs)
        'If Me.控制方式tolCmbControlScheme.SelectedItem <> "" Then
        '    CADformPara.sCurStaSchemeName = Me.控制方式tolCmbControlScheme.SelectedItem
        '    Call InputContolSchemenf(nCADStainfID, CADformPara.sCurStaSchemeName)
        '    Call RefreshPicSta(nCurPicstaLeftX, nCurPicStaTopY, Me.picSta, Me.picLine, nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)
        'End If
    End Sub


    Private Sub tolCmbControlScheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'If Me.tolCmbControlScheme.Text.Trim <> "" Then
        '    Call InputContolSchemenf(nCADStainfID, Me.tolCmbControlScheme.Text.Trim)
        '    Call RefreshPicSta(nCurPicstaLeftX, nCurPicStaTopY, Me.picSta, Me.picLine, nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)
        'End If
    End Sub


    Private Sub trvLine_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles trvLine.NodeMouseClick
        Dim sStr As String
        'MsgBox(Me.trvLine.SelectedNode.Text)

        'MsgBox(e.Node.Text)
        sStr = e.Node.FullPath
        Dim intNum As Integer
        intNum = GetStringInNumber(sStr, "\")
        Select Case intNum
            Case 0 '线网名
                sCADFormStaName = ""
                sCADFormLineName = ""
            Case 1 '线路名
                sCADFormStaName = ""
                sCADFormLineName = e.Node.Text
            Case 2 '车站名
                sCADFormStaName = e.Node.Text
                sCADFormLineName = e.Node.Parent.Text
        End Select
        If sCADFormStaName <> "" And sCADFormLineName <> "" Then
            Call FromStaNameToStaID(sCADFormStaName, sCADFormLineName)
            nCADFormLineID = Val(ReturnValue.strValue1)
            nCADFormStaID = Val(ReturnValue.strValue2)
            nCADStainfID = FromStaNameToCADStaInfID(sCADFormStaName)
            Me.Text = sCADFormStaName & " 车站平面图"
            nTrackID = 0
            Me.picSta.ContextMenuStrip = Nothing
            Call ShowCurSelectStaPic(nCADStainfID)
            'Call InputDataToCADstaInf(nCADStainfID)
            ' Call RefreshCADStaPicture()

        ElseIf sCADFormStaName = "" And sCADFormLineName <> "" Then
            'Call ShowCurSelectLInePic(sCADFormLineName)

        End If
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If sCADFormStaName = "" And sCADFormLineName <> "" Then
                ' Me.trvLine.ContextMenuStrip = Me.MnuLineEdit
            Else
                Me.trvLine.ContextMenuStrip = Nothing
            End If
        End If
    End Sub

    Private Sub 添加控制方式AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        ReDim stuListItem(UBound(CADStaInf))
        For i = 1 To UBound(CADStaInf)
            stuListItem(i).strItem = CADStaInf(i).sStaName
            stuListItem(i).strStyle = PropStrStyle.ComBox
            ReDim Preserve stuListItem(i).StrCmbList(UBound(CADStaInf(i).ContolScheme))
            For j = 1 To UBound(CADStaInf(i).ContolScheme)
                stuListItem(i).StrCmbList(j) = CADStaInf(i).ContolScheme(j).sSchemeName
            Next
            stuListItem(i).strTxtList = CADStaInf(i).sCurControlScheme
            stuListItem(i).strItemCriterion = TextCriterion.NotEmpty
        Next
        Dim nf2 As New frmEditDataProperity
        nf2.Text = " 车站控制方式设置 "
sBeGin:
        nf2.ShowDialog()
        If nf2.blnOK = True Then
            For i = 1 To UBound(CADStaInf)
                CADStaInf(i).sCurControlScheme = stuListItem(i).strReturnValue
            Next
        End If
        Call RefreshCADControModeInf()
        Call InputTrackInf()
        Call DrawStaPic(Me.picSta, Me.picLine)
        Call CADaddOneUndoInf()
    End Sub

    Private Sub tolCmbControlScheme_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub 修改控制方式KToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        Dim sFirsta As String
        Dim nSta As Integer
        Dim tmpStaId As Integer
        Dim tmpTrackId As Integer

        If UBound(curSelectTrackID) > 0 Then
            nSta = tmpTrackInf(curSelectTrackID(1)).nStaID
            sFirsta = CADStaInf(nSta).sStaName
            For i = 2 To UBound(curSelectTrackID)
                If CADStaInf(tmpTrackInf(curSelectTrackID(i)).nStaID).sStaName <> sFirsta Then
                    MsgBox("选择的线段不在同一个车站，请重新选择!", , "提示")
                    Exit Sub
                End If
            Next
        Else
            MsgBox("请先选择线段！", , "提示")
            Exit Sub
        End If

        ReDim stuListItem(2)
        stuListItem(1).strItem = "控制方式"
        stuListItem(1).strStyle = PropStrStyle.ComBox
        ReDim Preserve stuListItem(1).StrCmbList(UBound(CADStaInf(nSta).ContolScheme))
        For i = 1 To UBound(CADStaInf(nSta).ContolScheme)
            stuListItem(1).StrCmbList(i) = CADStaInf(nSta).ContolScheme(i).sSchemeName
        Next
        stuListItem(1).strTxtList = CADStaInf(nCADStainfID).sCurControlScheme
        stuListItem(1).strItemCriterion = TextCriterion.NotEmpty

        stuListItem(2).strItem = "控制模块"
        stuListItem(2).strStyle = PropStrStyle.ComBox
        ReDim stuListItem(2).StrCmbList(UBound(ControlModel))
        For i = 1 To UBound(ControlModel)
            stuListItem(2).StrCmbList(i) = ControlModel(i).sModelName
        Next
        stuListItem(2).strTxtList = ""
        stuListItem(2).strItemCriterion = TextCriterion.NotEmpty

        Dim nf2 As New frmEditDataProperity
        nf2.Text = CADStaInf(nSta).sStaName & "——控制方式设置 "
        Dim ifIn As Integer
sBeGin:
        nf2.ShowDialog()
        If nf2.blnOK = True Then
            For i = 1 To UBound(CADStaInf(nSta).ContolScheme)
                If CADStaInf(nSta).ContolScheme(i).sSchemeName = stuListItem(1).strReturnValue Then
                    For k = 1 To UBound(curSelectTrackID)
                        tmpStaId = tmpTrackInf(curSelectTrackID(k)).nStaID
                        tmpTrackId = tmpTrackInf(curSelectTrackID(k)).nTrackID
                        'CADStaInf(tmpStaId).Track(tmpTrackId).sControlNum = stuListItem(2).strReturnValue
                        ifIn = 0
                        For j = 1 To UBound(CADStaInf(nSta).ContolScheme(i).STrackNum)
                            If CADStaInf(nSta).ContolScheme(i).STrackNum(j) = CADStaInf(nSta).Track(tmpTrackId).sTrackCircuitNum Then
                                CADStaInf(nSta).ContolScheme(i).SModelName(j) = stuListItem(2).strReturnValue
                                ifIn = 1
                                Exit For
                            End If
                        Next
                        If ifIn = 0 And CADStaInf(nSta).Track(tmpTrackId).sTrackCircuitNum.Trim <> "" Then
                            ReDim Preserve CADStaInf(nSta).ContolScheme(i).STrackNum(UBound(CADStaInf(nSta).ContolScheme(i).STrackNum) + 1)
                            ReDim Preserve CADStaInf(nSta).ContolScheme(i).SModelName(UBound(CADStaInf(nSta).ContolScheme(i).SModelName) + 1)
                            CADStaInf(nSta).ContolScheme(i).STrackNum(j) = CADStaInf(nSta).Track(tmpTrackId).sTrackCircuitNum
                            CADStaInf(nSta).ContolScheme(i).SModelName(j) = stuListItem(2).strReturnValue
                        End If
                    Next
                    Exit For
                End If
            Next ' Call EditContolSchemenf(nSta, stuListItem(1).strReturnValue)
            Call DrawStaPic(Me.picSta, Me.picLine)
            Call CADaddOneUndoInf()
        End If
    End Sub

    Private Sub 控制模块信息ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nf As New frmDataEdit
        nf.sCurTableName = "车站控制方式表"
        nf.Show()
    End Sub

    Private Sub 对调坐标值CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim X1, X2, Y1, Y2 As Single
        Dim j As Integer
        Dim nTrackID As Integer
        Dim nStaId As Integer
        For j = 1 To UBound(curSelectTrackID)
            nTrackID = tmpTrackInf(curSelectTrackID(j)).nTrackID
            nStaId = tmpTrackInf(curSelectTrackID(j)).nStaID
            X1 = CADStaInf(nStaId).Track(nTrackID).X1
            Y1 = CADStaInf(nStaId).Track(nTrackID).Y1
            X2 = CADStaInf(nStaId).Track(nTrackID).X2
            Y2 = CADStaInf(nStaId).Track(nTrackID).Y2

            CADStaInf(nStaId).Track(nTrackID).X1 = X2
            CADStaInf(nStaId).Track(nTrackID).Y1 = Y2
            CADStaInf(nStaId).Track(nTrackID).X2 = X1
            CADStaInf(nStaId).Track(nTrackID).Y2 = Y1

            X1 = tmpTrackInf(curSelectTrackID(j)).X1
            Y1 = tmpTrackInf(curSelectTrackID(j)).Y1
            X2 = tmpTrackInf(curSelectTrackID(j)).X2
            Y2 = tmpTrackInf(curSelectTrackID(j)).Y2

            tmpTrackInf(curSelectTrackID(j)).X1 = X2
            tmpTrackInf(curSelectTrackID(j)).Y1 = Y2
            tmpTrackInf(curSelectTrackID(j)).X2 = X1
            tmpTrackInf(curSelectTrackID(j)).Y2 = Y1

            Call RefreshTrackInf(nStaId, nTrackID)
        Next

    End Sub

    Private Sub 车站平面图SToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 车站平面图SToolStripMenuItem.Click
        Dim nf As New frmPrintStaCAD
        nf.ShowDialog()
    End Sub

    Private Sub 线路平面图LToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 线路平面图LToolStripMenuItem.Click
        Dim nf As New frmPrintStaCAD
        nf.ShowDialog()
    End Sub

    Private Sub 将车站平图存为图片SToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 将车站平图存为图片SToolStripMenuItem1.Click
        Dim strPath As String
        Dim New0penFile As New SaveFileDialog
        New0penFile.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|jpeg files (*.jpeg)|*.jpeg|All files (*.*)|*.*"
        New0penFile.FilterIndex = 1
        New0penFile.RestoreDirectory = True
        strPath = ""
        If New0penFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strPath = New0penFile.FileName
            Me.picSta.Image.Save(strPath)
        End If
    End Sub

    Private Sub 将线路平面图存为图片LToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles 将线路平面图存为图片LToolStripMenuItem.Click
        Dim strPath As String
        Dim New0penFile As New SaveFileDialog
        New0penFile.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|jpeg files (*.jpeg)|*.jpeg|All files (*.*)|*.*"
        New0penFile.FilterIndex = 1
        New0penFile.RestoreDirectory = True
        strPath = ""
        If New0penFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            strPath = New0penFile.FileName
            Me.picLine.Image.Save(strPath)
        End If
    End Sub

    Private Sub 清空所有车站信息CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("确定清空所有车站的平面图信息吗？该操作将不能还源，请谨慎！", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2, "确认操作") = MsgBoxResult.Cancel Then Exit Sub
        Dim Str As String
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Str = "delete * from 线段信息表"
        Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        Mcom.ExecuteNonQuery()
        MyConn.Close()
        Call InputAllDataToCADstaInf(proBar)
        Call tolbtRefresh_Click(Nothing, Nothing)
    End Sub

    Private Sub 整理轨道编号ZToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i, j As Integer
        Dim sStaCode As String
        For i = 1 To UBound(CADStaInf)
            sStaCode = CADStaInf(i).sStaCode
            ' If CADStaInf(i).sStaName = "虹桥分界一->三角区分界一" Then Stop
            For j = 1 To UBound(CADStaInf(i).Track)
                CADStaInf(i).Track(j).sTrackCircuitNum = sStaCode & "-" & j
            Next
        Next
        Call tolbtRefresh_Click(Nothing, Nothing)
        MsgBox("整理完毕,请自动连接所有轨道!", , "提示")
    End Sub

    Private Sub 整理左右坐标RToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i, j As Integer
        Dim sStaCode As String
        Dim tmpX As Single
        Dim tmpY As Single
        For i = 1 To UBound(CADStaInf)
            sStaCode = CADStaInf(i).sStaCode
            For j = 1 To UBound(CADStaInf(i).Track)
                If CADStaInf(i).Track(j).X1 > CADStaInf(i).Track(j).X2 Then
                    tmpX = CADStaInf(i).Track(j).X1
                    tmpY = CADStaInf(i).Track(j).Y1
                    CADStaInf(i).Track(j).X1 = CADStaInf(i).Track(j).X2
                    CADStaInf(i).Track(j).Y1 = CADStaInf(i).Track(j).Y2
                    CADStaInf(i).Track(j).X2 = tmpX
                    CADStaInf(i).Track(j).Y2 = tmpY
                End If
            Next
        Next
        Call tolbtRefresh_Click(Nothing, Nothing)
        MsgBox("整理完毕,请自动连接所有轨道!", , "提示")
    End Sub

    Private Sub 车站进路搜索JToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nf As New frmStaJinLu
        nf.Show()
    End Sub

   
    Private Sub tolbarSetControlModel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call 整理轨道编号ZToolStripMenuItem_Click(Nothing, Nothing)
    End Sub

    Private Sub 自动生成全线图CToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If MsgBox("确定自动生成全线图吗？该操作将会清空原有的车站平面图信息。", MsgBoxStyle.OkCancel + MsgBoxStyle.DefaultButton2, "确认操作") = MsgBoxResult.Cancel Then Exit Sub

    End Sub

    Private Sub 所有对象自动对齐网格AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i, j As Integer
        For i = 1 To UBound(CADStaInf)
            For j = 1 To UBound(CADStaInf(i).Track)
                CADStaInf(i).Track(j).X1 = GetGridX(CADStaInf(i).Track(j).X1)
                CADStaInf(i).Track(j).X2 = GetGridX(CADStaInf(i).Track(j).X2)
                CADStaInf(i).Track(j).Y1 = GetGridX(CADStaInf(i).Track(j).Y1)
                CADStaInf(i).Track(j).Y2 = GetGridX(CADStaInf(i).Track(j).Y2)
            Next
        Next
        Call InputTrackInf()
        Call RefreshPicSta(nCurPicstaLeftX, nCurPicStaTopY, Me.picSta, Me.picLine, nCurShowScale, Me.显示网格.Checked, Me.tolbtShowElseInfor.Checked, Me.tolBtShowGuDaoNum.Checked, Me.tolbtShowTrackNum.Checked, Me.tolBtShowCrossingNum.Checked)
    End Sub


    Private Sub 左移一格LToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call MoveLineInLine(-1, 0)
    End Sub

    Private Sub MoveLineInLine(ByVal nXMoveStep As Integer, ByVal nYMoveStep As Integer)
        Dim j, k As Integer
        Dim nStaId As Integer
        For j = 1 To UBound(CADStaInf)
            If NetInf.Line(CADStaInf(j).nLineID).sName = sCADFormLineName Then
                nStaId = j
                For k = 1 To UBound(CADStaInf(nStaId).Track)
                    CADStaInf(nStaId).Track(k).X1 = CADStaInf(nStaId).Track(k).X1 + nFirGridWidth * nXMoveStep
                    CADStaInf(nStaId).Track(k).X2 = CADStaInf(nStaId).Track(k).X2 + nFirGridWidth * nXMoveStep
                    CADStaInf(nStaId).Track(k).Y1 = CADStaInf(nStaId).Track(k).Y1 + nFirGridWidth * nYMoveStep
                    CADStaInf(nStaId).Track(k).Y2 = CADStaInf(nStaId).Track(k).Y2 + nFirGridWidth * nYMoveStep
                Next
            End If
        Next
        Call CADaddOneUndoInf()
        Call RefreshPic()
    End Sub

    Private Sub 右移一格RToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call MoveLineInLine(1, 0)
    End Sub

    Private Sub 上移一格UToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call MoveLineInLine(0, -1)

    End Sub

    Private Sub 下移一格ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call MoveLineInLine(0, 1)
    End Sub

    Private Sub 指定移动格数FToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nMoveStep As Integer
        Dim nf As New frmInputBox
        nf.txtText.Visible = True
        nf.cmbText.Visible = False
        nf.txtText.Text = 1
        nf.Text = "输入窗体"
        nf.labTitle.Text = "请输入移动的步数(正为右移，负为左移):"
        nf.ShowDialog()
        If StrInputBoxText.Trim <> "" And bCancelInput = 0 Then
            nMoveStep = StrInputBoxText
            Call MoveLineInLine(nMoveStep, 0)
        End If
    End Sub

    Private Sub 指定移动格数FToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nMoveStep As Integer
        Dim nf As New frmInputBox
        nf.txtText.Visible = True
        nf.cmbText.Visible = False
        nf.txtText.Text = 1
        nf.Text = "输入窗体"
        nf.labTitle.Text = "请输入移动的步数(正为下移，负为上移):"
        nf.ShowDialog()
        If StrInputBoxText.Trim <> "" And bCancelInput = 0 Then
            nMoveStep = StrInputBoxText
            Call MoveLineInLine(0, nMoveStep)
        End If
    End Sub

    Private Sub ToolStripButton1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nf As New frmShowLinePic
        nf.sCurLineName = ""
        nf.sCurStaName = "苹果园"
        nf.nShowState = 0
        nf.ShowDialog()
    End Sub
End Class