Public Class frmDiagramLineAndFontSet
    Private Structure TrainLineStyle
        Dim sJiaoLuName As String
        Dim TrainLineStyle As String
        Dim TrainLineWidth As Single
        Dim TrainLineColor As String
        Dim CheDiLineStyle As String
        Dim CheDiLineWidth As Single
        Dim CheDiLineColor As String
    End Structure
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmDiagramLineAndFontSet_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.cmbLineStyle.Items.Clear()
        Me.cmbDigramStyle.Items.Add("һ�ָ�")
        Me.cmbDigramStyle.Items.Add("���ָ�")
        Me.cmbDigramStyle.Items.Add("ʮ�ָ�")
        Me.cmbDigramStyle.Items.Add("Сʱ��")
        Me.cmbDigramStyle.Text = CSTimeTablePara.TimeTableDiagramPara.strTimeFormat

        Call listTimeLineStyle()
        Me.cmbLineStyle.Items.Clear()
        Me.cmbLineStyle.Items.Add("ʵ�� ������������������")
        Me.cmbLineStyle.Items.Add("�����ߡ� �� �� �� �� ��")
        Me.cmbLineStyle.Items.Add("������-----------------")
        Me.cmbLineStyle.Items.Add("�㻮�ߡ� - �� - �� - ��")
        Me.cmbLineStyle.Items.Add("˫�㻮�ߡ� -- �� -- �� ")
        Me.cmbLineStyle.Text = "ʵ�� ������������������"

        Me.cmbTrainLineStyle.Items.Clear()
        Me.cmbTrainLineStyle.Items.Add("ʵ�� ������������������")
        Me.cmbTrainLineStyle.Items.Add("�����ߡ� �� �� �� �� ��")
        Me.cmbTrainLineStyle.Items.Add("������-----------------")
        Me.cmbTrainLineStyle.Items.Add("�㻮�ߡ� - �� - �� - ��")
        Me.cmbTrainLineStyle.Items.Add("˫�㻮�ߡ� -- �� -- �� ")
        Me.cmbTrainLineStyle.Text = "ʵ�� ������������������"

    End Sub


    Private Sub ShowTimeLinePrint()
        'Call InputLineStyleInfor()
        Me.picTimeLineShow.Refresh()
        Dim g As System.Drawing.Graphics
        g = Me.picTimeLineShow.CreateGraphics
        Dim tmpPen As Pen
        tmpPen = New Pen(Me.labTimeLineColor.BackColor, Me.numLineWidth.Value)
        If Me.cmbLineStyle.Text.Trim.Length >= 2 Then
            Select Case Me.cmbLineStyle.Text.Trim.Substring(0, 2)
                Case "ʵ��"
                    tmpPen.DashStyle = Drawing2D.DashStyle.Solid
                Case "����"
                    tmpPen.DashStyle = Drawing2D.DashStyle.Dash
                Case "����"
                    tmpPen.DashStyle = Drawing2D.DashStyle.Dot
                Case "�㻮"
                    tmpPen.DashStyle = Drawing2D.DashStyle.DashDot
                Case "˫��"
                    tmpPen.DashStyle = Drawing2D.DashStyle.DashDotDot
            End Select

        End If
        g.DrawLine(tmpPen, 10, 13, Me.picTimeLineShow.Width - 20, 13)
    End Sub


    Private Sub btnTimeLineColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTimeLineColor.Click
        Dim dColor As New ColorDialog
        dColor.Color = Me.labTimeLineColor.BackColor
        If dColor.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.labTimeLineColor.BackColor = dColor.Color
            Call ShowTimeLinePrint()
            Call InputLineStyleInfor()
        End If
    End Sub

    Private Sub lstTimeLine_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTimeLine.SelectedValueChanged
        Select Case Me.cmbDigramStyle.Text
            Case "һ�ָ�"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "һ�ָ���"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.OneTime1LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.OneTime1LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime1LineColor)
                        Case "��ָ���"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.OneTime5LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.OneTime5LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime5LineColor)
                        Case "ʮ�ָ���"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.OneTime10LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.OneTime10LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime10LineColor)
                        Case "��Сʱ����"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.OneTime30LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.OneTime30LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime30LineColor)
                        Case "Сʱ����"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.OneTime60LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.OneTime60LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.OneTime60LineColor)
                    End Select
                End If
            Case "���ָ�"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "���ָ���"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.TwoTime2LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.TwoTime2LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TwoTime2LineColor)
                        Case "ʮ�ָ���"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.TwoTime10LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.TwoTime10LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TwoTime10LineColor)
                        Case "��Сʱ����"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.TwoTime30LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.TwoTime30LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TwoTime30LineColor)
                        Case "Сʱ����"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.TwoTime60LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.TwoTime60LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TwoTime60LineColor)
                    End Select
                End If
            Case "ʮ�ָ�"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "ʮ�ָ���"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.TenTime10LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.TenTime10LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TenTime10LineColor)
                        Case "��Сʱ����"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.TenTime30LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.TenTime30LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TenTime30LineColor)
                        Case "Сʱ����"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.TenTime60LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.TenTime60LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TenTime60LineColor)
                    End Select
                End If
            Case "Сʱ��"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "Сʱ����"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.HourTime60LineStyle)
                            Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.HourTime60LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.HourTime60LineColor)
                    End Select
                End If
        End Select
        Call ShowTimeLinePrint()
    End Sub

    '��ʾ�б���������ͼ������ʾ���ܵ�����
    Private Sub listTimeLineStyle()
        Me.lstTimeLine.Items.Clear()
        Select Case Me.cmbDigramStyle.Text
            Case "һ�ָ�"
                Me.lstTimeLine.Items.Add("һ�ָ���")
                Me.lstTimeLine.Items.Add("��ָ���")
                Me.lstTimeLine.Items.Add("ʮ�ָ���")
                Me.lstTimeLine.Items.Add("��Сʱ����")
                Me.lstTimeLine.Items.Add("Сʱ����")
            Case "���ָ�"
                Me.lstTimeLine.Items.Add("���ָ���")
                Me.lstTimeLine.Items.Add("��ָ���")
                Me.lstTimeLine.Items.Add("ʮ�ָ���")
                Me.lstTimeLine.Items.Add("��Сʱ����")
                Me.lstTimeLine.Items.Add("Сʱ����")
            Case "ʮ�ָ�"
                Me.lstTimeLine.Items.Add("ʮ�ָ���")
                Me.lstTimeLine.Items.Add("��Сʱ����")
                Me.lstTimeLine.Items.Add("Сʱ����")
            Case "Сʱ��"
                Me.lstTimeLine.Items.Add("Сʱ����")
        End Select
    End Sub


    Private Sub btnSetLineStyleDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetLineStyleDefault.Click
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String
        Select Case Me.cmbDigramStyle.Text
            Case "һ�ָ�"
                cellName = "һ�ָ�ͼ1�ָ�����"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime1LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "һ�ָ�ͼ1�ָ��߿�"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime1LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "һ�ָ�ͼ1�ָ�����ɫ"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime1LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "һ�ָ�ͼ5�ָ�����"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime5LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "һ�ָ�ͼ5�ָ��߿�"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime5LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "һ�ָ�ͼ5�ָ�����ɫ"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime5LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "һ�ָ�ͼ10�ָ�����"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime10LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "һ�ָ�ͼ10�ָ��߿�"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime10LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "һ�ָ�ͼ10�ָ�����ɫ"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime10LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "һ�ָ�ͼ30�ָ�����"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime30LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "һ�ָ�ͼ30�ָ��߿�"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime30LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "һ�ָ�ͼ30�ָ�����ɫ"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime30LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "һ�ָ�ͼ60�ָ�����"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime60LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "һ�ָ�ͼ60�ָ��߿�"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime60LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "һ�ָ�ͼ60�ָ�����ɫ"
                CellValue = CSTimeTablePara.DiagramStylePara.OneTime60LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

            Case "���ָ�"

                cellName = "���ָ�ͼ2�ָ�����"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime2LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "���ָ�ͼ2�ָ��߿�"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime2LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "���ָ�ͼ2�ָ�����ɫ"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime2LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "���ָ�ͼ10�ָ�����"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime10LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "���ָ�ͼ10�ָ��߿�"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime10LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "���ָ�ͼ10�ָ�����ɫ"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime10LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "���ָ�ͼ30�ָ�����"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime30LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "���ָ�ͼ30�ָ��߿�"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime30LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "���ָ�ͼ30�ָ�����ɫ"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime30LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "���ָ�ͼ60�ָ�����"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime60LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "���ָ�ͼ60�ָ��߿�"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime60LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "���ָ�ͼ60�ָ�����ɫ"
                CellValue = CSTimeTablePara.DiagramStylePara.TwoTime60LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

            Case "ʮ�ָ�"

                cellName = "ʮ�ָ�ͼ10�ָ�����"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime10LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "ʮ�ָ�ͼ10�ָ��߿�"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime10LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "ʮ�ָ�ͼ10�ָ�����ɫ"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime10LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "ʮ�ָ�ͼ30�ָ�����"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime30LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "ʮ�ָ�ͼ30�ָ��߿�"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime30LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "ʮ�ָ�ͼ30�ָ�����ɫ"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime30LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "ʮ�ָ�ͼ60�ָ�����"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime60LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "ʮ�ָ�ͼ60�ָ��߿�"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime60LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "ʮ�ָ�ͼ60�ָ�����ɫ"
                CellValue = CSTimeTablePara.DiagramStylePara.TenTime60LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

            Case "Сʱ��"

                cellName = "Сʱ��ͼ60�ָ�����"
                CellValue = CSTimeTablePara.DiagramStylePara.HourTime60LineStyle
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "Сʱ��ͼ60�ָ��߿�"
                CellValue = CSTimeTablePara.DiagramStylePara.HourTime60LineWidth
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

                cellName = "Сʱ��ͼ60�ָ�����ɫ"
                CellValue = CSTimeTablePara.DiagramStylePara.HourTime60LineColor
                Str = "update cs_cstimetablesystempara set " & _
                        "ParaValue ='" & CellValue & "'" & _
                        "where ParaName = '" & cellName & "'"
                 Globle.Method.UpdateDataForAccess(Str)

        End Select


        cellName = "��վ������������"
        CellValue = CSTimeTablePara.DiagramStylePara.StaNameFontName
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "��վ���������С"
        CellValue = CSTimeTablePara.DiagramStylePara.StaNameFontSize
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "��վ�����������"
        CellValue = CSTimeTablePara.DiagramStylePara.StaNameFontBold
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "��վ��������б��"
        CellValue = CSTimeTablePara.DiagramStylePara.StaNameFontItalic
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "��վ����������ɫ"
        CellValue = CSTimeTablePara.DiagramStylePara.StaNameFontColor
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "ʱ���ע��������"
        CellValue = CSTimeTablePara.DiagramStylePara.TimeFontName
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "ʱ���ע�����С"
        CellValue = CSTimeTablePara.DiagramStylePara.TimeFontSize
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "ʱ���ע�������"
        CellValue = CSTimeTablePara.DiagramStylePara.TimeFontBold
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "ʱ���ע����б��"
        CellValue = CSTimeTablePara.DiagramStylePara.TimeFontItalic
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "ʱ���ע������ɫ"
        CellValue = CSTimeTablePara.DiagramStylePara.TimeFontColor
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "һ�㳵վ����������"
        CellValue = CSTimeTablePara.DiagramStylePara.StaLineStyle
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "һ�㳵վ�����߿��"
        CellValue = CSTimeTablePara.DiagramStylePara.StaLineWidth
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "һ�㳵վ��������ɫ"
        CellValue = CSTimeTablePara.DiagramStylePara.StaLineColor
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "�ֲ�վ����������"
        CellValue = CSTimeTablePara.DiagramStylePara.FenStaLineStyle
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "�ֲ�վ�����߿��"
        CellValue = CSTimeTablePara.DiagramStylePara.FenStaLineWidth
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "�ֲ�վ��������ɫ"
        CellValue = CSTimeTablePara.DiagramStylePara.FenStaLineColor
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)
        cellName = "��������������"
        CellValue = CSTimeTablePara.DiagramStylePara.CheChangStaLineStyle
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "���������߿��"
        CellValue = CSTimeTablePara.DiagramStylePara.CheChangStaLineWidth
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)


        cellName = "������������ɫ"
        CellValue = CSTimeTablePara.DiagramStylePara.CheChangStaLineColor
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        Call InputLineStyleInfor()

        MsgBox("�Ѿ��ɹ�����!")
    End Sub

    Private Sub InputLineStyleInfor()
        Select Case Me.cmbDigramStyle.Text
            Case "һ�ָ�"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "һ�ָ���"
                            CSTimeTablePara.DiagramStylePara.OneTime1LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.OneTime1LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.OneTime1LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "��ָ���"
                            CSTimeTablePara.DiagramStylePara.OneTime5LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.OneTime5LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.OneTime5LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "ʮ�ָ���"
                            CSTimeTablePara.DiagramStylePara.OneTime10LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.OneTime10LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.OneTime10LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "��Сʱ����"
                            CSTimeTablePara.DiagramStylePara.OneTime30LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.OneTime30LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.OneTime30LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "Сʱ����"
                            CSTimeTablePara.DiagramStylePara.OneTime60LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.OneTime60LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.OneTime60LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                    End Select
                End If
            Case "���ָ�"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "���ָ���"
                            CSTimeTablePara.DiagramStylePara.TwoTime2LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.TwoTime2LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.TwoTime2LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "ʮ�ָ���"
                            CSTimeTablePara.DiagramStylePara.TwoTime10LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.TwoTime10LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.TwoTime10LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "��Сʱ����"
                            CSTimeTablePara.DiagramStylePara.TwoTime30LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.TwoTime30LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.TwoTime30LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "Сʱ����"
                            CSTimeTablePara.DiagramStylePara.TwoTime60LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.TwoTime60LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.TwoTime60LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                    End Select
                End If
            Case "ʮ�ָ�"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "ʮ�ָ���"
                            CSTimeTablePara.DiagramStylePara.TenTime10LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.TenTime10LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.TenTime10LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "��Сʱ����"
                            CSTimeTablePara.DiagramStylePara.TenTime30LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.TenTime30LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.TenTime30LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "Сʱ����"
                            CSTimeTablePara.DiagramStylePara.TenTime60LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.TenTime60LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.TenTime60LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                    End Select
                End If
            Case "Сʱ��"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "Сʱ����"
                            CSTimeTablePara.DiagramStylePara.HourTime60LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            CSTimeTablePara.DiagramStylePara.HourTime60LineWidth = Me.numLineWidth.Value
                            CSTimeTablePara.DiagramStylePara.HourTime60LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                    End Select
                End If
        End Select

        If Me.lstStaLine.SelectedItem <> Nothing Then
            Select Case Me.lstStaLine.SelectedItem.ToString
                Case "һ�㳵վ"
                    CSTimeTablePara.DiagramStylePara.StaLineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                    CSTimeTablePara.DiagramStylePara.StaLineWidth = Me.numLineWidth.Value
                    CSTimeTablePara.DiagramStylePara.StaLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                Case "��վ"
                    CSTimeTablePara.DiagramStylePara.FenStaLineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                    CSTimeTablePara.DiagramStylePara.FenStaLineWidth = Me.numLineWidth.Value
                    CSTimeTablePara.DiagramStylePara.FenStaLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                Case "����"
                    CSTimeTablePara.DiagramStylePara.CheChangStaLineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                    CSTimeTablePara.DiagramStylePara.CheChangStaLineWidth = Me.numLineWidth.Value
                    CSTimeTablePara.DiagramStylePara.CheChangStaLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
            End Select
        End If

    End Sub

    Private Sub cmbDigramStyle_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDigramStyle.SelectedValueChanged
        Call listTimeLineStyle()
    End Sub

    Private Sub numLineWidth_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles numLineWidth.MouseDown
        Call ShowTimeLinePrint()
        Call InputLineStyleInfor()
    End Sub

    Private Sub cmbLineStyle_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLineStyle.DropDownClosed
        Me.cmbLineStyle.Text = Me.cmbLineStyle.SelectedItem
        Call ShowTimeLinePrint()
        Call InputLineStyleInfor()
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Call CSRefreshDiagram(0)
        Me.Close()
    End Sub

    Private Sub btnSelectFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectFont.Click
        Dim nd As New FontDialog
        Dim f As Font
        If Me.lstFont.SelectedItem <> Nothing Then
            Select Case Me.lstFont.SelectedItem.ToString
                Case "��վ����"
                    f = New Font(CSTimeTablePara.DiagramStylePara.StaNameFontName, CSTimeTablePara.DiagramStylePara.StaNameFontSize)
                    If CSTimeTablePara.DiagramStylePara.StaNameFontBold = True Then
                        If CSTimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If CSTimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If
                    nd.Font = f
                    If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        CSTimeTablePara.DiagramStylePara.StaNameFontName = nd.Font.Name
                        CSTimeTablePara.DiagramStylePara.StaNameFontSize = nd.Font.Size
                        CSTimeTablePara.DiagramStylePara.StaNameFontBold = nd.Font.Bold
                        CSTimeTablePara.DiagramStylePara.StaNameFontItalic = nd.Font.Italic
                    End If

                Case "ʱ���ע"

                    f = New Font(CSTimeTablePara.DiagramStylePara.TimeFontName, CSTimeTablePara.DiagramStylePara.TimeFontSize)
                    If CSTimeTablePara.DiagramStylePara.TimeFontBold = True Then
                        If CSTimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If CSTimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If

                    nd.Font = f
                    If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        CSTimeTablePara.DiagramStylePara.TimeFontName = nd.Font.Name
                        CSTimeTablePara.DiagramStylePara.TimeFontSize = nd.Font.Size
                        CSTimeTablePara.DiagramStylePara.TimeFontBold = nd.Font.Bold
                        CSTimeTablePara.DiagramStylePara.TimeFontItalic = nd.Font.Italic
                    End If
            End Select
            Call ShowFontPreView()
        End If
    End Sub

    Private Sub btnFontColorSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFontColorSet.Click
        Dim dColor As New ColorDialog
        dColor.Color = Me.labTimeLineColor.BackColor
        If dColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.labFontColor.BackColor = dColor.Color
            If Me.lstFont.SelectedItem <> Nothing Then
                Select Case Me.lstFont.SelectedItem.ToString
                    Case "��վ����"
                        CSTimeTablePara.DiagramStylePara.StaNameFontColor = System.Drawing.ColorTranslator.ToHtml(Me.labFontColor.BackColor)
                    Case "ʱ���ע"
                        CSTimeTablePara.DiagramStylePara.TimeFontColor = System.Drawing.ColorTranslator.ToHtml(Me.labFontColor.BackColor)
                End Select
            End If
            Call ShowFontPreView()
        End If
    End Sub

    '����Ԥ��
    Private Sub ShowFontPreView()
        'CorTimeTablePara.DiagramStylePara.StaNameFontStyle = New Font("����", 20)
        'CorTimeTablePara.DiagramStylePara.TimeFontStyle = New Font("����", 20)
        Me.picFontShow.Refresh()
        Dim g As System.Drawing.Graphics
        g = Me.picFontShow.CreateGraphics
        Dim newBrush As Brush
        Dim f As Font
        If Me.lstFont.SelectedItem <> Nothing Then
            Select Case Me.lstFont.SelectedItem.ToString
                Case "��վ����"
                    f = New Font(CSTimeTablePara.DiagramStylePara.StaNameFontName, CSTimeTablePara.DiagramStylePara.StaNameFontSize)
                    If CSTimeTablePara.DiagramStylePara.StaNameFontBold = True Then
                        If CSTimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If CSTimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If

                    newBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.StaNameFontColor))
                    g.DrawString("����Ԥ��Ч��012345aAbBcC", f, newBrush, 10, 10)

                    Me.labFontColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.StaNameFontColor)
                Case "ʱ���ע"
                    f = New Font(CSTimeTablePara.DiagramStylePara.TimeFontName, CSTimeTablePara.DiagramStylePara.TimeFontSize)
                    If CSTimeTablePara.DiagramStylePara.TimeFontBold = True Then
                        If CSTimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If CSTimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If
                    newBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TimeFontColor))
                    g.DrawString("����Ԥ��Ч��012345aAbBcC", f, newBrush, 10, 10)
                    Me.labFontColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.TimeFontColor)
            End Select
        End If

    End Sub

    Private Sub lstFont_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstFont.SelectedIndexChanged
        Call ShowFontPreView()
    End Sub

    Private Sub lstStaLine_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstStaLine.SelectedValueChanged
        If Me.lstStaLine.SelectedItem <> Nothing Then
            Select Case Me.lstStaLine.SelectedItem.ToString
                Case "һ�㳵վ"
                    Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.StaLineStyle)
                    Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.StaLineWidth
                    Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.StaLineColor)
                Case "��վ"
                    Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.FenStaLineStyle)
                    Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.FenStaLineWidth
                    Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.FenStaLineColor)
                Case "����"
                    Me.cmbLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.CheChangStaLineStyle)
                    Me.numLineWidth.Value = CSTimeTablePara.DiagramStylePara.CheChangStaLineWidth
                    Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.CheChangStaLineColor)
            End Select
        End If
        Call ShowTimeLinePrint()
    End Sub

    Private Sub numTrainLineWidth_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles numTrainLineWidth.MouseDown
        Call ListTrainLineView()
        Call SetTrainLineStyle()
    End Sub

    Private Sub ListTrainLineView()
        Me.picTrainLineView.Refresh()
        Dim g As System.Drawing.Graphics
        g = Me.picTrainLineView.CreateGraphics
        Dim tmpPen As Pen
        tmpPen = New Pen(Me.labTrainLineColor.BackColor, Me.numTrainLineWidth.Value)
        tmpPen.DashStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
        g.DrawLine(tmpPen, 10, 13, Me.picTimeLineShow.Width - 20, 13)
    End Sub

    Private Sub SetTrainLineStyle()
        If Me.lstTrainStyle.SelectedItem = Nothing Then
            Exit Sub
        End If
        Select Case Me.lstTrainStyle.SelectedItem.ToString
            Case "δ��ѡ�����"
                CSTimeTablePara.DiagramStylePara.UnAssignTrainLineStyle = GetLineTextNameFromStyle(Me.cmbTrainLineStyle.Text)
                CSTimeTablePara.DiagramStylePara.UnAssignTrainLineWidth = Me.numTrainLineWidth.Value
                CSTimeTablePara.DiagramStylePara.UnAssignTrainLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTrainLineColor.BackColor)
            Case "��������"
                CSTimeTablePara.DiagramStylePara.DutyOnLineStyle = GetLineTextNameFromStyle(Me.cmbTrainLineStyle.Text)
                CSTimeTablePara.DiagramStylePara.DutyOnLineWidth = Me.numTrainLineWidth.Value
                CSTimeTablePara.DiagramStylePara.DutyOnLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTrainLineColor.BackColor)
            Case "С������"
                CSTimeTablePara.DiagramStylePara.DutyRestLineStyle = GetLineTextNameFromStyle(Me.cmbTrainLineStyle.Text)
                CSTimeTablePara.DiagramStylePara.DutyRestLineWidth = Me.numTrainLineWidth.Value
                CSTimeTablePara.DiagramStylePara.DutyRestLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTrainLineColor.BackColor)
            Case "�ò�����"
                CSTimeTablePara.DiagramStylePara.DutyDinnerLineStyle = GetLineTextNameFromStyle(Me.cmbTrainLineStyle.Text)
                CSTimeTablePara.DiagramStylePara.DutyDinnerLineWidth = Me.numTrainLineWidth.Value
                CSTimeTablePara.DiagramStylePara.DutyDinnerLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTrainLineColor.BackColor)
            Case "�������"
                CSTimeTablePara.DiagramStylePara.DutyOffLineStyle = GetLineTextNameFromStyle(Me.cmbTrainLineStyle.Text)
                CSTimeTablePara.DiagramStylePara.DutyOffLineWidth = Me.numTrainLineWidth.Value
                CSTimeTablePara.DiagramStylePara.DutyOffLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTrainLineColor.BackColor)
        End Select
    End Sub

    Private Sub btnSetTrainDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrainDefault.Click
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String
        Dim tab As DataTable

        cellName = "δ��ѡ���������"
        CellValue = CSTimeTablePara.DiagramStylePara.UnAssignTrainLineStyle
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','96','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "δ��ѡ������߿�"
        CellValue = CSTimeTablePara.DiagramStylePara.UnAssignTrainLineWidth
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','97','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "δ��ѡ�������ɫ"
        CellValue = CSTimeTablePara.DiagramStylePara.UnAssignTrainLineColor
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','98','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "������������"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyOnLineStyle
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','99','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "���������߿�"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyOnLineWidth
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','100','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "����������ɫ"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyOnLineColor
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','101','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "С����������"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyRestLineStyle
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','99','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "С�������߿�"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyRestLineWidth
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','100','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "С��������ɫ"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyRestLineColor
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','101','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "�ò���������"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyDinnerLineStyle
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','102','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "�ò������߿�"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyDinnerLineWidth
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','103','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "�ò�������ɫ"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyDinnerLineColor
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','104','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "�����������"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyOffLineStyle
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','105','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "��������߿�"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyOffLineWidth
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','106','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "���������ɫ"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyOffLineColor
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','107','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "��������������"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyNoFontName
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','108','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If
        
        cellName = "�����������С"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyNoFontSize
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','109','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "�������������"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyNoFontBold
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','110','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "����������б��"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyNoFontItalic
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','111','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "������������ɫ"
        CellValue = CSTimeTablePara.DiagramStylePara.DutyNoFontColor
        Str = "select * from cs_cstimetablesystempara where paraname='" & cellName & "'"
        tab = ReadData(Str)
        If tab.Rows.Count = 0 Then
            Str = "Insert into cs_cstimetablesystempara (lineid,paraid,paraname,paravalue) values('" & _
                strCurlineID & "','112','" & cellName & "','" & CellValue & "') "
             Globle.Method.UpdateDataForAccess(Str)
        Else
            Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
             Globle.Method.UpdateDataForAccess(Str)
        End If

        cellName = "б�򳵴���������"
        CellValue = CSTimeTablePara.DiagramStylePara.XieCheCiFontName
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "б�򳵴������С"
        CellValue = CSTimeTablePara.DiagramStylePara.XieCheCiFontSize
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "б�򳵴��������"
        CellValue = CSTimeTablePara.DiagramStylePara.XieCheCiFontBold
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "б�򳵴�����б��"
        CellValue = CSTimeTablePara.DiagramStylePara.XieCheCiFontItalic
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)

        cellName = "б�򳵴�������ɫ"
        CellValue = CSTimeTablePara.DiagramStylePara.XieCheCiFontColor
        Str = "update cs_cstimetablesystempara set " & _
                "ParaValue ='" & CellValue & "'" & _
                "where ParaName = '" & cellName & "'"
         Globle.Method.UpdateDataForAccess(Str)
        MsgBox("�Ѿ��ɹ�����!")

    End Sub

    Private Sub cmbTrainLineStyle_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTrainLineStyle.DropDownClosed
        Me.cmbTrainLineStyle.Text = Me.cmbTrainLineStyle.SelectedItem
        Call ListTrainLineView()
        Call SetTrainLineStyle()
    End Sub

    Private Sub btnTrainLineColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrainLineColor.Click
        Dim dColor As New ColorDialog
        dColor.Color = Me.labTrainLineColor.BackColor
        If dColor.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Me.labTrainLineColor.BackColor = dColor.Color
            Call ListTrainLineView()
            Call SetTrainLineStyle()
        End If
    End Sub

    Private Sub btnCheCiFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheCiFont.Click
        Dim nd As New FontDialog
        Dim f As Font
        If Me.lstCheCiFont.SelectedItem <> Nothing Then
            Select Case Me.lstCheCiFont.SelectedItem.ToString
                Case "�����߳���"
                    f = New Font(CSTimeTablePara.DiagramStylePara.XieCheCiFontName, CSTimeTablePara.DiagramStylePara.XieCheCiFontSize)
                    If CSTimeTablePara.DiagramStylePara.XieCheCiFontBold = True Then
                        If CSTimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If CSTimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If
                    nd.Font = f
                    If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        CSTimeTablePara.DiagramStylePara.XieCheCiFontName = nd.Font.Name
                        CSTimeTablePara.DiagramStylePara.XieCheCiFontSize = nd.Font.Size
                        CSTimeTablePara.DiagramStylePara.XieCheCiFontBold = nd.Font.Bold
                        CSTimeTablePara.DiagramStylePara.XieCheCiFontItalic = nd.Font.Italic
                    End If

                Case "������"
                    f = New Font(CSTimeTablePara.DiagramStylePara.DutyNoFontName, CSTimeTablePara.DiagramStylePara.DutyNoFontSize)
                    If CSTimeTablePara.DiagramStylePara.DutyNoFontBold = True Then
                        If CSTimeTablePara.DiagramStylePara.DutyNoFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If CSTimeTablePara.DiagramStylePara.DutyNoFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If
                    nd.Font = f
                    If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        CSTimeTablePara.DiagramStylePara.DutyNoFontName = nd.Font.Name
                        CSTimeTablePara.DiagramStylePara.DutyNoFontSize = nd.Font.Size
                        CSTimeTablePara.DiagramStylePara.DutyNoFontBold = nd.Font.Bold
                        CSTimeTablePara.DiagramStylePara.DutyNoFontItalic = nd.Font.Italic
                    End If

            End Select
            Call ShowCheCiFontPreView()

        End If
    End Sub

    '��ʾ����
    Private Sub ShowCheCiFontPreView()
        Me.picCheCiFont.Refresh()
        Dim g As System.Drawing.Graphics
        g = Me.picCheCiFont.CreateGraphics
        Dim newBrush As Brush
        Dim f As Font
        If Me.lstCheCiFont.SelectedItem <> Nothing Then
            Select Case Me.lstCheCiFont.SelectedItem.ToString
                Case "�����߳���"
                    f = New Font(CSTimeTablePara.DiagramStylePara.XieCheCiFontName, CSTimeTablePara.DiagramStylePara.XieCheCiFontSize)
                    If CSTimeTablePara.DiagramStylePara.XieCheCiFontBold = True Then
                        If CSTimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If CSTimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If

                    newBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.XieCheCiFontColor))
                    g.DrawString("����Ԥ��Ч��012345aAbBcC", f, newBrush, 10, 10)
                    Me.labCheCiColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.XieCheCiFontColor)
                Case "������"
                    f = New Font(CSTimeTablePara.DiagramStylePara.DutyNoFontName, CSTimeTablePara.DiagramStylePara.DutyNoFontSize)
                    If CSTimeTablePara.DiagramStylePara.DutyNoFontBold = True Then
                        If CSTimeTablePara.DiagramStylePara.DutyNoFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If CSTimeTablePara.DiagramStylePara.DutyNoFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If

                    newBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyNoFontColor))
                    g.DrawString("����Ԥ��Ч��012345aAbBcC", f, newBrush, 10, 10)
                    Me.labCheCiColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyNoFontColor)
            End Select
        End If
    End Sub

    Private Sub lstCheCiFont_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCheCiFont.SelectedIndexChanged
        Call ShowCheCiFontPreView()
    End Sub

    Private Sub btnCheCiColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheCiColor.Click
        Dim dColor As New ColorDialog
        dColor.Color = Me.labCheCiColor.BackColor
        If dColor.ShowDialog = Windows.Forms.DialogResult.OK Then
            Me.labCheCiColor.BackColor = dColor.Color
            If Me.lstCheCiFont.SelectedItem <> Nothing Then
                Select Case Me.lstCheCiFont.SelectedItem.ToString
                    Case "�����߳���"
                        CSTimeTablePara.DiagramStylePara.XieCheCiFontColor = System.Drawing.ColorTranslator.ToHtml(Me.labCheCiColor.BackColor)
                    Case "������"
                        CSTimeTablePara.DiagramStylePara.DutyNoFontColor = System.Drawing.ColorTranslator.ToHtml(Me.labCheCiColor.BackColor)
                End Select
            End If
            Call ShowCheCiFontPreView()
        End If
    End Sub

    Private Sub btnSetTrainLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrainLine.Click
        Call SetTrainLineStyle()
        Call CSRefreshDiagram(0)
    End Sub

    Private Sub lstTrainStyle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstTrainStyle.SelectedIndexChanged
        Select Case Me.lstTrainStyle.SelectedItem.ToString
            Case "δ��ѡ�����"
                Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.UnAssignTrainLineStyle)
                Me.numTrainLineWidth.Value = CSTimeTablePara.DiagramStylePara.UnAssignTrainLineWidth
                Me.labTrainLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.UnAssignTrainLineColor)
            Case "��������"
                Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.DutyOnLineStyle)
                Me.numTrainLineWidth.Value = CSTimeTablePara.DiagramStylePara.DutyOnLineWidth
                Me.labTrainLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyOnLineColor)
            Case "С������"
                Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.DutyRestLineStyle)
                Me.numTrainLineWidth.Value = CSTimeTablePara.DiagramStylePara.DutyRestLineWidth
                Me.labTrainLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyRestLineColor)
            Case "�ò�����"
                Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.DutyDinnerLineStyle)
                Me.numTrainLineWidth.Value = CSTimeTablePara.DiagramStylePara.DutyDinnerLineWidth
                Me.labTrainLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyDinnerLineColor)
            Case "�������"
                Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(CSTimeTablePara.DiagramStylePara.DutyOffLineStyle)
                Me.numTrainLineWidth.Value = CSTimeTablePara.DiagramStylePara.DutyOffLineWidth
                Me.labTrainLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(CSTimeTablePara.DiagramStylePara.DutyOffLineColor)
        End Select
        Call ListTrainLineView()
    End Sub

End Class