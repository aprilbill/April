Public Class frmStaJinLu
    Dim CurPriStaName As String
    Dim nCurPriStaName As Integer
    Dim priMaxX As Single
    Dim priMinX As Single
    Dim priMaxY As Single
    Dim priMinY As Single
    Dim priLeftBlank As Single
    Dim priTopYblank As Single
    Dim priReturnSpeed As Single
    Dim nPathOpenTime As Integer
    Dim nCurJinLuId As Integer
    Dim nCurPicLeftX As Single
    Dim nCurPicTopY As Single

    Structure typeReturnStaLine
        Dim nTrackID As Integer
        Dim sLineStyle As String
        Dim sLineNum As String
        Dim nYcoord As Single
        Dim nUporDown As Integer
        Dim SLinkStaName As String
    End Structure
    Dim ReturnstaLine() As typeReturnStaLine
    Dim ReturnstaLine2() As typeReturnStaLine
    Private Sub frmStaJinLu_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        If UBound(CADStaInf) > 0 Then
            For i = 1 To UBound(CADStaInf)
                If CADStaInf(i).sStaOrSec = "车站" Then
                    Me.cmbStaName.Items.Add(CADStaInf(i).sStaName)
                End If
            Next
            If Me.cmbStaName.Items.Count > 0 Then
                Me.cmbStaName.Text = Me.cmbStaName.Items(0)
            End If
        End If
        Call frmStaJinLu_Resize(Nothing, Nothing)
    End Sub

    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        Me.dataGrid.Rows.Clear()
        Me.DataDiDui.Rows.Clear()

        Dim i As Integer
        CurPriStaName = Me.cmbStaName.Text.Trim
        For i = 1 To UBound(CADStaInf)
            If CADStaInf(i).sStaName = CurPriStaName Then
                nCurPriStaName = i
                Exit For
            End If
        Next i

        If CurPriStaName = "" Then
            MsgBox("请先选择车站名称!", , "提示")
        End If
        Call InputStaBaseinf(nCurPriStaName)
        Call SeekTrainPath(nCurPriStaName)
        'TrainReturnRunTrack(1).nTrackID(UBound(TrainReturnRunTrack(1).nTrackID)).intRunTime = TrainReturnRunTrack(1).nTrackID(UBound(TrainReturnRunTrack(1).nTrackID)).intRunTime + Me.numStopTime.Value  '停站
        'nFirTrackNum = GetGuiDaoTrackIDFromUpOrDown(nCurPriStaName, 2) 'GetTrackIDFromUpOrDown(nCurPriStaName, 1)
        'SeekStationRunTrackID(nCurPriStaName, nSecTrackNum, nFirTrackNum, 1, 1, priReturnSpeed)

    End Sub

    '搜索进路
    Private Sub SeekTrainPath(ByVal nStaID As Integer)
        ReDim TrainReturnRunTrack(0)
        Dim nFirTrackNum As Integer
        Dim nSecTrackNum As Integer
        Dim i, j, k, p As Integer
        Dim sGuiDaoNum As String
        Dim sCrossNum() As String
        Dim sPassCrossNum As String
        ReDim sCrossNum(0)
        Dim StrControlModel() As String
        Dim tmpStrControlModel As String
        Dim nCurID As Integer
        Dim nIfIn As Integer
        Dim nUporDown As Integer
        nUporDown = 0
        For i = 1 To UBound(ReturnstaLine)
            For j = 1 To UBound(ReturnstaLine2)
                nCurID = UBound(TrainReturnRunTrack) + 1
                ReDim Preserve TrainReturnRunTrack(nCurID)
                ReDim TrainReturnRunTrack(nCurID).nTrackID(0)
                sGuiDaoNum = ""
                sPassCrossNum = ""
                tmpStrControlModel = ""
                ReDim sCrossNum(0)
                ReDim StrControlModel(0)

                nFirTrackNum = ReturnstaLine(i).nTrackID
                nSecTrackNum = ReturnstaLine2(j).nTrackID
                nUporDown = ReturnstaLine(i).nUporDown
                If nFirTrackNum <> nSecTrackNum Then
                    SeekStationRunTrackID(nUporDown, nStaID, nFirTrackNum, nSecTrackNum, nCurID, 1, 40, 0, 0)
                    If UBound(TrainReturnRunTrack(nCurID).nTrackID) > 0 Then '找到并满足条件
                        For k = 1 To UBound(TrainReturnRunTrack(nCurID).nTrackID)
                            sGuiDaoNum = sGuiDaoNum & "," & CADStaInf(nStaID).Track(TrainReturnRunTrack(nCurID).nTrackID(k).nTrackID).sTrackCircuitNum
                            'If CADStaInf(nCurPriStaName).Track(TrainReturnRunTrack(nCurID).nTrackID(k).nTrackID).sTrackCircuitNum = "F3" Then Stop
                            If CADStaInf(nStaID).Track(TrainReturnRunTrack(nCurID).nTrackID(k).nTrackID).sStyle = "道岔线" Then '通过的道岔
                                If CADStaInf(nStaID).Track(TrainReturnRunTrack(nCurID).nTrackID(k).nTrackID).sTrackNum.Trim <> "" Then
                                    nIfIn = 0
                                    For p = 1 To UBound(sCrossNum)
                                        If sCrossNum(p) = CADStaInf(nStaID).Track(TrainReturnRunTrack(nCurID).nTrackID(k).nTrackID).sTrackNum Then  '已经添加
                                            nIfIn = 1
                                            Exit For
                                        End If
                                    Next
                                    If nIfIn = 0 Then
                                        ReDim Preserve sCrossNum(UBound(sCrossNum) + 1)
                                        sCrossNum(UBound(sCrossNum)) = CADStaInf(nStaID).Track(TrainReturnRunTrack(nCurID).nTrackID(k).nTrackID).sTrackNum
                                        sPassCrossNum = sPassCrossNum & "," & CADStaInf(nStaID).Track(TrainReturnRunTrack(nCurID).nTrackID(k).nTrackID).sTrackNum
                                    End If
                                End If
                            End If

                            '    '通过的控制模块
                            '    nIfIn = 0
                            '    For p = 1 To UBound(StrControlModel)
                            '        If StrControlModel(p) = CADStaInf(nStaID).Track(TrainReturnRunTrack(nCurID).nTrackID(k).nTrackID).sControlNum Then  '已经添加
                            '            nIfIn = 1
                            '            Exit For
                            '        End If
                            '    Next
                            '    If nIfIn = 0 Then
                            '        ReDim Preserve StrControlModel(UBound(StrControlModel) + 1)
                            '        StrControlModel(UBound(StrControlModel)) = CADStaInf(nStaID).Track(TrainReturnRunTrack(nCurID).nTrackID(k).nTrackID).sControlNum
                            '        tmpStrControlModel = tmpStrControlModel & "," & CADStaInf(nStaID).Track(TrainReturnRunTrack(nCurID).nTrackID(k).nTrackID).sControlNum
                            '    End If
                        Next
                        Me.dataGrid.Rows.Add()
                        Me.dataGrid.Rows(Me.dataGrid.Rows.Count - 1).Cells(0).Value = nCurID
                        Me.dataGrid.Rows(Me.dataGrid.Rows.Count - 1).Cells(1).Value = CADStaInf(nStaID).sStaName
                        If ReturnstaLine(i).SLinkStaName = CADStaInf(nStaID).sStaName Then '股道至股道的连接进路
                            Me.dataGrid.Rows(Me.dataGrid.Rows.Count - 1).Cells(2).Value = ReturnstaLine(i).sLineNum
                        Else
                            Me.dataGrid.Rows(Me.dataGrid.Rows.Count - 1).Cells(2).Value = CADStaInf(nStaID).Track(ReturnstaLine(i).nTrackID).sTrackCircuitNum
                        End If
                        Me.dataGrid.Rows(Me.dataGrid.Rows.Count - 1).Cells(3).Value = ReturnstaLine(i).SLinkStaName
                        Me.dataGrid.Rows(Me.dataGrid.Rows.Count - 1).Cells(4).Value = ReturnstaLine2(j).sLineNum
                        Me.dataGrid.Rows(Me.dataGrid.Rows.Count - 1).Cells(5).Value = sGuiDaoNum
                        Me.dataGrid.Rows(Me.dataGrid.Rows.Count - 1).Cells(6).Value = sPassCrossNum
                        Me.dataGrid.Rows(Me.dataGrid.Rows.Count - 1).Cells(7).Value = tmpStrControlModel
                    Else
                        'Stop
                    End If
                End If

            Next
        Next
    End Sub

    '打印折返时运行的轨迹
    Private Sub PrintTrainReturnPath(ByVal nID As Integer, ByVal MinX As Integer, ByVal MinY As Single, ByVal LeftBlank As Single, ByVal TopYblank As Single, ByVal tmpPen As Pen)
        Dim i As Integer
        Dim X1, Y1 As Single
        Dim X2, Y2 As Single
        Dim nTrackID As Integer
        Dim nStaID As Integer

        'Dim rBmp As Bitmap '画图临时保存的图像
        Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象

        rBmpGraphics = Me.picSta.CreateGraphics
        For i = 1 To UBound(TrainReturnRunTrack(nID).nTrackID)
            nTrackID = TrainReturnRunTrack(nID).nTrackID(i).nTrackID
            nStaID = TrainReturnRunTrack(nID).nTrackID(i).nStaID
            X1 = CADStaInf(nStaID).Track(nTrackID).X1 - MinX + LeftBlank
            X2 = CADStaInf(nStaID).Track(nTrackID).X2 - MinX + LeftBlank
            Y1 = CADStaInf(nStaID).Track(nTrackID).Y1 - MinY + TopYblank
            Y2 = CADStaInf(nStaID).Track(nTrackID).Y2 - MinY + TopYblank
            rBmpGraphics.DrawLine(tmpPen, X1, Y1, X2, Y2)
        Next
        '  rBmpGraphics.DrawLine(tmpPen, 100, 100, 200, 200)
    End Sub

    '导入车站基础信息
    Private Sub InputStaBaseinf(ByVal nStaID As Integer)
        Dim i As Integer
        Dim j As Integer
        Me.cmbReturnLine.Items.Clear()
        Me.cmbReturnLine.Text = ""
        'Dim nLinkTrackID() As Integer
        'ReDim nLinkTrackID(0)
        ReDim ReturnstaLine(0)
        ReDim ReturnstaLine2(0)
        Dim sStaName As String
        Dim sStyle As String
        Dim sStyle1 As String
        For i = 1 To UBound(CADStaInf(nStaID).Track)
            sStyle = GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sLeftLink1)
            sStyle1 = GetTrackCodeFromTrackCurNum(CADStaInf(nStaID).Track(i).sRightLink1)
            sStaName = GetStaNameFromStaCode(sStyle)
            If sStyle <> "NULL" And sStyle <> CADStaInf(nStaID).sStaCode Then
                ReDim Preserve ReturnstaLine(UBound(ReturnstaLine) + 1)
                ReturnstaLine(UBound(ReturnstaLine)).nTrackID = i
                ReturnstaLine(UBound(ReturnstaLine)).sLineStyle = sStyle
                ReturnstaLine(UBound(ReturnstaLine)).nUporDown = 2
                ReturnstaLine(UBound(ReturnstaLine)).SLinkStaName = GetSectionPrintStaName(sStaName, CADStaInf(nStaID).sStaName) 'GetStaNameFromSecId(sStyle)
            End If
            sStaName = GetStaNameFromStaCode(sStyle1)
            If sStyle1 <> "NULL" And sStyle1 <> CADStaInf(nStaID).sStaCode Then
                ReDim Preserve ReturnstaLine(UBound(ReturnstaLine) + 1)
                ReturnstaLine(UBound(ReturnstaLine)).nTrackID = i
                ReturnstaLine(UBound(ReturnstaLine)).sLineStyle = sStyle1
                ReturnstaLine(UBound(ReturnstaLine)).nUporDown = 1
                ReturnstaLine(UBound(ReturnstaLine)).SLinkStaName = GetSectionPrintStaName(sStaName, CADStaInf(nStaID).sStaName)
            End If
        Next


        For j = 1 To UBound(CADStaInf(nStaID).Track)
            If CADStaInf(nStaID).Track(j).sGuDaoStyle IsNot Nothing Then
                If CADStaInf(nStaID).Track(j).sGuDaoStyle.Length >= 3 Then
                    If CADStaInf(nStaID).Track(j).sGuDaoStyle.Substring(0, 3) = "正线线" Or CADStaInf(nStaID).Track(j).sGuDaoStyle.Substring(0, 3) = "折返线" Or CADStaInf(nStaID).Track(j).sGuDaoStyle.Substring(0, 3) = "到发线" Then
                        If CADStaInf(nStaID).Track(j).sTrackNum <> "" And CADStaInf(nStaID).Track(j).sStyle = "股道线" Then
                            ReDim Preserve ReturnstaLine2(UBound(ReturnstaLine2) + 1)
                            ReturnstaLine2(UBound(ReturnstaLine2)).nTrackID = j
                            ReturnstaLine2(UBound(ReturnstaLine2)).sLineStyle = CADStaInf(nStaID).Track(j).sGuDaoStyle.Substring(0, 3)
                            ReturnstaLine2(UBound(ReturnstaLine2)).sLineNum = CADStaInf(nStaID).Track(j).sTrackNum
                            Me.cmbReturnLine.Items.Add(CADStaInf(nStaID).Track(j).sTrackNum)
                        End If
                    End If
                End If
            End If
        Next

        For j = 1 To UBound(ReturnstaLine2)
            ReDim Preserve ReturnstaLine(UBound(ReturnstaLine) + 1)
            ReturnstaLine(UBound(ReturnstaLine)).nTrackID = ReturnstaLine2(j).nTrackID
            ReturnstaLine(UBound(ReturnstaLine)).sLineStyle = ReturnstaLine2(j).sLineStyle
            ReturnstaLine(UBound(ReturnstaLine)).nUporDown = ReturnstaLine2(j).nUporDown
            ReturnstaLine(UBound(ReturnstaLine)).sLineNum = ReturnstaLine2(j).sLineNum
            ReturnstaLine(UBound(ReturnstaLine)).SLinkStaName = CADStaInf(nStaID).sStaName
        Next



        'If Me.cmbReturnLine.Items.Count > 0 Then
        '    Me.cmbReturnLine.Text = Me.cmbReturnLine.Items(0)
        'End If

        'Me.cmbControlStyle.Items.Clear()
        'For i = 1 To UBound(CADStaInf(nStaID).ContolScheme)
        '    Me.cmbControlStyle.Items.Add(CADStaInf(nStaID).ContolScheme(i).sSchemeName)
        'Next
        'If Me.cmbControlStyle.Items.Count > 0 Then
        '    Me.cmbControlStyle.Text = CADStaInf(nStaID).sCurControlScheme 'Me.cmbControlStyle.Items(0)
        'End If

    End Sub
    Private Sub PrintCurStaCADPic(ByVal sStaName As String)
        Dim i As Integer
        Dim j As Integer
        Dim rBmp As Bitmap '画图临时保存的图像
        Dim rBmpGraphics As Graphics '画线路与车站图的定义的对象

        'Dim sngCompareX As Single
        priMinX = 10000000
        priMaxX = -1000000
        priMinY = 10000000
        priMaxY = -1000000
        priLeftBlank = 70
        priTopYblank = 70
        Dim nCurStaID As Integer
        For i = 1 To UBound(CADStaInf)
            If CADStaInf(i).sStaName = sStaName Then
                For j = 1 To UBound(CADStaInf(i).Track)
                    If CADStaInf(i).Track(j).X1 < priMinX Then
                        priMinX = CADStaInf(i).Track(j).X1
                    End If
                    If CADStaInf(i).Track(j).X2 < priMinX Then
                        priMinX = CADStaInf(i).Track(j).X2
                    End If
                    If CADStaInf(i).Track(j).X1 > priMaxX Then
                        priMaxX = CADStaInf(i).Track(j).X1
                    End If
                    If CADStaInf(i).Track(j).X2 > priMaxX Then
                        priMaxX = CADStaInf(i).Track(j).X2
                    End If
                    If CADStaInf(i).Track(j).Y1 < priMinY Then
                        priMinY = CADStaInf(i).Track(j).Y1
                    End If
                    If CADStaInf(i).Track(j).Y2 < priMinY Then
                        priMinY = CADStaInf(i).Track(j).Y2
                    End If
                    If CADStaInf(i).Track(j).Y1 > priMaxY Then
                        priMaxY = CADStaInf(i).Track(j).Y1
                    End If
                    If CADStaInf(i).Track(j).Y2 > priMaxY Then
                        priMaxY = CADStaInf(i).Track(j).Y2
                    End If
                Next
                nCurStaID = i
                Exit For
            End If
        Next
        picSta.Width = priMaxX - priMinX + 2 * priLeftBlank
        picSta.Height = priMaxY - priMinY + 2 * priTopYblank
        rBmp = New Bitmap(picSta.Width, picSta.Height)
        rBmpGraphics = Graphics.FromImage(rBmp)
        Me.picSta.Refresh()
        Dim tmpPen As Pen
        tmpPen = New Pen(Color.White, 4)
        nCurPicLeftX = priLeftBlank
        nCurPicTopY = priTopYblank
        Call DrawStaPicFormStaID(nCurStaID, 1, Color.White, 4, 3, rBmpGraphics, priMinX, priMinY, nCurPicLeftX, nCurPicTopY, True, True, True, False)
        'Call PrintStationCADPicture(rBmpGraphics, sStaName, 1, 0, priMaxX + 100, priMinX, priLeftBlank, priTopYblank, tmpPen)
        Me.picSta.Image = rBmp
        Call frmStaJinLu_Resize(Nothing, Nothing)
    End Sub


    '显示敌对进路
    Private Sub ListDiDuiJinLu(ByVal nCurID As Integer, ByVal nStaID As Integer)
        Dim i, j, k, p As Integer
        Dim nIn As Integer
        Me.DataDiDui.Rows.Clear()
        Dim sPassCross1 As String
        Dim sPassCross2 As String
        sPassCross1 = "a"
        sPassCross2 = "b"
        For i = 1 To UBound(TrainReturnRunTrack)
            If i <> nCurID Then
                nIn = 0
                For j = 1 To UBound(TrainReturnRunTrack(i).nTrackID)
                    For k = 1 To UBound(TrainReturnRunTrack(nCurID).nTrackID)
                        ' If CADStaInf(nStaID).Track(TrainReturnRunTrack(nCurID).nTrackID(k).nTrackID).sTrackCircuitNum = "W25" Then Stop
                        If CADStaInf(nStaID).Track(TrainReturnRunTrack(i).nTrackID(j).nTrackID).sStyle = "道岔线" Then
                            sPassCross1 = CADStaInf(nStaID).Track(TrainReturnRunTrack(i).nTrackID(j).nTrackID).sTrackNum
                            If sPassCross1 = "" Then
                                sPassCross1 = "a"
                            End If
                        End If
                        If CADStaInf(nStaID).Track(TrainReturnRunTrack(nCurID).nTrackID(k).nTrackID).sStyle = "道岔线" Then
                            sPassCross2 = CADStaInf(nStaID).Track(TrainReturnRunTrack(nCurID).nTrackID(k).nTrackID).sTrackNum
                            If sPassCross2 = "" Then
                                sPassCross2 = "b"
                            End If
                        End If

                        If TrainReturnRunTrack(i).nTrackID(j).nTrackID = TrainReturnRunTrack(nCurID).nTrackID(k).nTrackID _
                            Or sPassCross1 = sPassCross2 Then
                            'CADStaInf(nStaID).Track(TrainReturnRunTrack(i).nTrackID(j).nTrackID).sControlNum = CADStaInf(nStaID).Track(TrainReturnRunTrack(nCurID).nTrackID(k).nTrackID).sControlNum Then '冲突,包括在同一控制模快
                            For p = 1 To Me.dataGrid.Rows.Count
                                If Me.dataGrid.Rows(p - 1).Cells(0).Value = i Then
                                    Me.DataDiDui.Rows.Add()
                                    Me.DataDiDui.Rows(Me.DataDiDui.Rows.Count - 1).Cells(0).Value = Me.dataGrid.Rows(p - 1).Cells(0).Value
                                    Me.DataDiDui.Rows(Me.DataDiDui.Rows.Count - 1).Cells(1).Value = Me.dataGrid.Rows(p - 1).Cells(1).Value
                                    Me.DataDiDui.Rows(Me.DataDiDui.Rows.Count - 1).Cells(2).Value = Me.dataGrid.Rows(p - 1).Cells(2).Value
                                    Me.DataDiDui.Rows(Me.DataDiDui.Rows.Count - 1).Cells(3).Value = Me.dataGrid.Rows(p - 1).Cells(3).Value
                                    Me.DataDiDui.Rows(Me.DataDiDui.Rows.Count - 1).Cells(4).Value = Me.dataGrid.Rows(p - 1).Cells(4).Value
                                    Me.DataDiDui.Rows(Me.DataDiDui.Rows.Count - 1).Cells(5).Value = Me.dataGrid.Rows(p - 1).Cells(5).Value
                                    Me.DataDiDui.Rows(Me.DataDiDui.Rows.Count - 1).Cells(6).Value = Me.dataGrid.Rows(p - 1).Cells(6).Value
                                    Me.DataDiDui.Rows(Me.DataDiDui.Rows.Count - 1).Cells(7).Value = Me.dataGrid.Rows(p - 1).Cells(7).Value
                                    Exit For
                                End If
                            Next
                            nIn = 1
                            Exit For
                        End If
                    Next
                    If nIn = 1 Then Exit For
                Next
            End If
        Next
    End Sub

    Private Sub dataGrid_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGrid.CellClick
        Dim tmpPen As Pen
        Dim i As Integer
        Dim sCurStaName As String
        tmpPen = New Pen(Color.GreenYellow, 4)
        nCurJinLuId = Me.dataGrid.Rows(Me.dataGrid.CurrentCell.RowIndex).Cells(0).Value
        sCurStaName = Me.dataGrid.Rows(Me.dataGrid.CurrentCell.RowIndex).Cells(1).Value
        If sCurStaName <> CurPriStaName Then
            CurPriStaName = sCurStaName
            For i = 1 To UBound(CADStaInf)
                If CADStaInf(i).sStaName = CurPriStaName Then
                    nCurPriStaName = i
                    Exit For
                End If
            Next i
            Me.cmbStaName.Text = sCurStaName
            'Call PrintCurStaCADPic(sCurStaName)
            Call InputStaBaseinf(nCurPriStaName)
            Call SeekTrainPath(nCurPriStaName)
        End If

        If nCurJinLuId > 0 Then
            Me.picSta.Refresh()
            Call ListDiDuiJinLu(nCurJinLuId, nCurPriStaName)
            Call PrintTrainReturnPath(nCurJinLuId, priMinX, priMinY, nCurPicLeftX, nCurPicTopY, tmpPen)
        End If

    End Sub

    Private Sub DataDiDui_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataDiDui.CellClick
        Dim nCurId As Integer
        nCurId = Me.DataDiDui.Rows(Me.DataDiDui.CurrentCell.RowIndex).Cells(0).Value
        Dim tmpPen As Pen
        tmpPen = New Pen(Color.GreenYellow, 4)
        Dim tmpPen1 As Pen
        tmpPen1 = New Pen(Color.Red, 4)
        If nCurId > 0 Then
            Me.picSta.Refresh()
            Call PrintTrainReturnPath(nCurJinLuId, priMinX, priMinY, nCurPicLeftX, nCurPicTopY, tmpPen)
            Call PrintTrainReturnPath(nCurId, priMinX, priMinY, nCurPicLeftX, nCurPicTopY, tmpPen1)
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmStaJinLu_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.picSta.Height >= Me.SplitContainerSta.Panel2.Height Then
            Me.picSta.Top = 0
        Else
            Me.picSta.Top = (Me.SplitContainerSta.Panel2.Height - Me.picSta.Height) / 2
        End If

        If Me.picSta.Width >= Me.SplitContainerSta.Panel2.Width Then
            Me.picSta.Left = 0
        Else
            Me.picSta.Left = (Me.SplitContainerSta.Panel2.Width - Me.picSta.Width) / 2
        End If
    End Sub

    Private Sub cmbControlStyle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbControlStyle.SelectedIndexChanged
        Dim i As Integer
        Dim j As Integer
        Dim k As Integer
        For i = 1 To UBound(CADStaInf(nCurPriStaName).ContolScheme)
            If CADStaInf(nCurPriStaName).ContolScheme(i).sSchemeName = Me.cmbControlStyle.Text.Trim Then
                For j = 1 To UBound(CADStaInf(nCurPriStaName).Track)
                    For k = 1 To UBound(CADStaInf(nCurPriStaName).ContolScheme(i).STrackNum)
                        If CADStaInf(nCurPriStaName).Track(j).sTrackCircuitNum = CADStaInf(nCurPriStaName).ContolScheme(i).STrackNum(k) Then
                            CADStaInf(nCurPriStaName).Track(j).sControlNum = CADStaInf(nCurPriStaName).ContolScheme(i).SModelName(k)
                            Exit For
                        End If
                    Next
                Next
                Exit For
            End If
        Next
        Call PrintCurStaCADPic(CurPriStaName)
    End Sub

    Private Sub btnAllSta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllSta.Click
        Me.dataGrid.Rows.Clear()
        Me.DataDiDui.Rows.Clear()
        Dim j As Integer
        For j = 1 To UBound(CADStaInf)
            If CADStaInf(j).sStaOrSec = "车站" Then
                Call InputStaBaseinf(j)
                Call SeekTrainPath(j)
            End If
        Next
        MsgBox("自动搜索完毕！")
    End Sub

    Private Sub cmbStaName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbStaName.SelectedIndexChanged
        Call PrintCurStaCADPic(Me.cmbStaName.Text)
    End Sub

    Private Sub btnSaveSta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveSta.Click
        Dim str As String
        Dim i As Integer
        Dim sStaName As String
        If MsgBox("当前列出的所有车站原始保存的进路信息将会被删除! 按[确定]将保存新的信息，按[取消]将退出", MsgBoxStyle.OkCancel, "确认操作") = MsgBoxResult.Cancel Then
            Exit Sub
        End If
        Dim sForStaName As String
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If Me.dataGrid.RowCount > 0 Then
            sStaName = Me.dataGrid.Item(1, 0).Value
            sForStaName = ""
            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            str = "delete * from 车站进路信息 where 车站名称='" & sStaName & "'"
            Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
            Mcom.ExecuteNonQuery()

            For i = 2 To Me.dataGrid.RowCount
                sForStaName = Me.dataGrid.Item(1, i - 1).Value
                If sStaName <> sForStaName Then
                    sStaName = sForStaName
                    If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                    str = "delete * from 车站进路信息 where 车站名称='" & sStaName & "'"
                    Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                    Mcom1.ExecuteNonQuery()
                End If
            Next

            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
            For i = 1 To Me.dataGrid.RowCount
                str = "insert into 车站进路信息 (车站名称,进站方式,连接车站名,股道编号,通过的轨道编号,通过的道岔编号,通过的控制模块) values ('" & _
                Me.dataGrid.Item(1, i - 1).Value & "', '" & _
                Me.dataGrid.Item(2, i - 1).Value & "', '" & _
                Me.dataGrid.Item(3, i - 1).Value & "', '" & _
                Me.dataGrid.Item(4, i - 1).Value & "', '" & _
                Me.dataGrid.Item(5, i - 1).Value & "', '" & _
                Me.dataGrid.Item(6, i - 1).Value & "', '" & _
                Me.dataGrid.Item(7, i - 1).Value & "')"
                Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(str, MyConn)
                Mcom1.ExecuteNonQuery()
            Next
            MsgBox("保存完毕!", , "提示")
            MyConn.Close()
        Else
            MsgBox("先选择车站名称!", , "提示")
        End If
    End Sub

    Private Sub btnData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnData.Click
        Dim nf As New frmDataEdit
        nf.sCurTableName = "车站进路信息"
        nf.Show()
    End Sub
End Class