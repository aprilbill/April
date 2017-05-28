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
        Me.cmbDigramStyle.Text = TimeTablePara.TimeTableDiagramPara.strTimeFormat

        Call listTimeLineStyle()
        Me.cmbLineStyle.Items.Clear()
        Me.cmbLineStyle.Items.Add("ʵ�� ������������������")
        Me.cmbLineStyle.Items.Add("�����ߡ� �� �� �� �� ��")
        Me.cmbLineStyle.Items.Add("������-----------------")
        Me.cmbLineStyle.Items.Add("�㻮�ߡ� - �� - �� - ��")
        Me.cmbLineStyle.Items.Add("˫�㻮�ߡ� -- �� -- �� ")
        Me.cmbLineStyle.Text = "ʵ�� ������������������"

        Me.cmbTrainStyle.Items.Clear()
        Me.cmbTrainStyle.Items.Add("����������")
        Me.cmbTrainStyle.Items.Add("���г���������")
        Me.cmbTrainStyle.Items.Add("�����߰���·����")
        Me.cmbTrainStyle.Items.Add("�����߰����б��")
        Me.cmbTrainStyle.Items.Add("�����߰���·���")
        Me.cmbTrainStyle.Items.Add("�����߰����ױ�ŷ���")
        Me.cmbTrainStyle.Items.Add("�����߰����ױ�ŷ���")
        Me.cmbTrainStyle.Items.Add("���������߷�������")
        Me.cmbTrainStyle.Text = "����������"

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
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.OneTime1LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.OneTime1LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime1LineColor)
                        Case "��ָ���"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.OneTime5LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.OneTime5LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime5LineColor)
                        Case "ʮ�ָ���"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.OneTime10LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.OneTime10LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime10LineColor)
                        Case "��Сʱ����"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.OneTime30LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.OneTime30LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime30LineColor)
                        Case "Сʱ����"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.OneTime60LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.OneTime60LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.OneTime60LineColor)
                    End Select
                End If
            Case "���ָ�"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "���ָ���"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.TwoTime2LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.TwoTime2LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime2LineColor)
                        Case "ʮ�ָ���"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.TwoTime10LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.TwoTime10LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime10LineColor)
                        Case "��Сʱ����"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.TwoTime30LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.TwoTime30LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime30LineColor)
                        Case "Сʱ����"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.TwoTime60LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.TwoTime60LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TwoTime60LineColor)
                    End Select
                End If
            Case "ʮ�ָ�"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "ʮ�ָ���"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.TenTime10LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.TenTime10LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TenTime10LineColor)
                        Case "��Сʱ����"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.TenTime30LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.TenTime30LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TenTime30LineColor)
                        Case "Сʱ����"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.TenTime60LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.TenTime60LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TenTime60LineColor)
                    End Select
                End If
            Case "Сʱ��"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "Сʱ����"
                            Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.HourTime60LineStyle)
                            Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.HourTime60LineWidth
                            Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.HourTime60LineColor)
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
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String
        Select Case Me.cmbDigramStyle.Text
            Case "һ�ָ�"
                cellName = "һ�ָ�ͼ1�ָ�����"
                CellValue = TimeTablePara.DiagramStylePara.OneTime1LineStyle
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom.ExecuteNonQuery()

                cellName = "һ�ָ�ͼ1�ָ��߿�"
                CellValue = TimeTablePara.DiagramStylePara.OneTime1LineWidth
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom1.ExecuteNonQuery()

                cellName = "һ�ָ�ͼ1�ָ�����ɫ"
                CellValue = TimeTablePara.DiagramStylePara.OneTime1LineColor
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom2.ExecuteNonQuery()

                cellName = "һ�ָ�ͼ5�ָ�����"
                CellValue = TimeTablePara.DiagramStylePara.OneTime5LineStyle
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom3 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom3.ExecuteNonQuery()

                cellName = "һ�ָ�ͼ5�ָ��߿�"
                CellValue = TimeTablePara.DiagramStylePara.OneTime5LineWidth
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom4 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom4.ExecuteNonQuery()

                cellName = "һ�ָ�ͼ5�ָ�����ɫ"
                CellValue = TimeTablePara.DiagramStylePara.OneTime5LineColor
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom5 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom5.ExecuteNonQuery()

                cellName = "һ�ָ�ͼ10�ָ�����"
                CellValue = TimeTablePara.DiagramStylePara.OneTime10LineStyle
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom6 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom6.ExecuteNonQuery()

                cellName = "һ�ָ�ͼ10�ָ��߿�"
                CellValue = TimeTablePara.DiagramStylePara.OneTime10LineWidth
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom7 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom7.ExecuteNonQuery()

                cellName = "һ�ָ�ͼ10�ָ�����ɫ"
                CellValue = TimeTablePara.DiagramStylePara.OneTime10LineColor
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom8 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom8.ExecuteNonQuery()

                cellName = "һ�ָ�ͼ30�ָ�����"
                CellValue = TimeTablePara.DiagramStylePara.OneTime30LineStyle
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom9 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom9.ExecuteNonQuery()

                cellName = "һ�ָ�ͼ30�ָ��߿�"
                CellValue = TimeTablePara.DiagramStylePara.OneTime30LineWidth
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom10 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom10.ExecuteNonQuery()

                cellName = "һ�ָ�ͼ30�ָ�����ɫ"
                CellValue = TimeTablePara.DiagramStylePara.OneTime30LineColor
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom11 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom11.ExecuteNonQuery()

                cellName = "һ�ָ�ͼ60�ָ�����"
                CellValue = TimeTablePara.DiagramStylePara.OneTime60LineStyle
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom12 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom12.ExecuteNonQuery()

                cellName = "һ�ָ�ͼ60�ָ��߿�"
                CellValue = TimeTablePara.DiagramStylePara.OneTime60LineWidth
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom13 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom13.ExecuteNonQuery()

                cellName = "һ�ָ�ͼ60�ָ�����ɫ"
                CellValue = TimeTablePara.DiagramStylePara.OneTime60LineColor
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom14 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom14.ExecuteNonQuery()

            Case "���ָ�"

                cellName = "���ָ�ͼ2�ָ�����"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime2LineStyle
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom.ExecuteNonQuery()

                cellName = "���ָ�ͼ2�ָ��߿�"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime2LineWidth
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom1.ExecuteNonQuery()

                cellName = "���ָ�ͼ2�ָ�����ɫ"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime2LineColor
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom2.ExecuteNonQuery()

                cellName = "���ָ�ͼ10�ָ�����"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime10LineStyle
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom3 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom3.ExecuteNonQuery()

                cellName = "���ָ�ͼ10�ָ��߿�"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime10LineWidth
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom4 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom4.ExecuteNonQuery()

                cellName = "���ָ�ͼ10�ָ�����ɫ"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime10LineColor
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom5 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom5.ExecuteNonQuery()

                cellName = "���ָ�ͼ30�ָ�����"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime30LineStyle
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom6 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom6.ExecuteNonQuery()

                cellName = "���ָ�ͼ30�ָ��߿�"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime30LineWidth
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom7 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom7.ExecuteNonQuery()

                cellName = "���ָ�ͼ30�ָ�����ɫ"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime30LineColor
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom8 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom8.ExecuteNonQuery()

                cellName = "���ָ�ͼ60�ָ�����"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime60LineStyle
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom9 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom9.ExecuteNonQuery()

                cellName = "���ָ�ͼ60�ָ��߿�"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime60LineWidth
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom10 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom10.ExecuteNonQuery()

                cellName = "���ָ�ͼ60�ָ�����ɫ"
                CellValue = TimeTablePara.DiagramStylePara.TwoTime60LineColor
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom11 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom11.ExecuteNonQuery()

            Case "ʮ�ָ�"

                cellName = "ʮ�ָ�ͼ10�ָ�����"
                CellValue = TimeTablePara.DiagramStylePara.TenTime10LineStyle
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom.ExecuteNonQuery()

                cellName = "ʮ�ָ�ͼ10�ָ��߿�"
                CellValue = TimeTablePara.DiagramStylePara.TenTime10LineWidth
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom1.ExecuteNonQuery()

                cellName = "ʮ�ָ�ͼ10�ָ�����ɫ"
                CellValue = TimeTablePara.DiagramStylePara.TenTime10LineColor
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom2.ExecuteNonQuery()


                cellName = "ʮ�ָ�ͼ30�ָ�����"
                CellValue = TimeTablePara.DiagramStylePara.TenTime30LineStyle
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom3 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom3.ExecuteNonQuery()

                cellName = "ʮ�ָ�ͼ30�ָ��߿�"
                CellValue = TimeTablePara.DiagramStylePara.TenTime30LineWidth
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom4 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom4.ExecuteNonQuery()

                cellName = "ʮ�ָ�ͼ30�ָ�����ɫ"
                CellValue = TimeTablePara.DiagramStylePara.TenTime30LineColor
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom6 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom6.ExecuteNonQuery()


                cellName = "ʮ�ָ�ͼ60�ָ�����"
                CellValue = TimeTablePara.DiagramStylePara.TenTime60LineStyle
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom7 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom7.ExecuteNonQuery()

                cellName = "ʮ�ָ�ͼ60�ָ��߿�"
                CellValue = TimeTablePara.DiagramStylePara.TenTime60LineWidth
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom8 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom8.ExecuteNonQuery()

                cellName = "ʮ�ָ�ͼ60�ָ�����ɫ"
                CellValue = TimeTablePara.DiagramStylePara.TenTime60LineColor
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom9 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom9.ExecuteNonQuery()

            Case "Сʱ��"

                cellName = "Сʱ��ͼ60�ָ�����"
                CellValue = TimeTablePara.DiagramStylePara.HourTime60LineStyle
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom.ExecuteNonQuery()

                cellName = "Сʱ��ͼ60�ָ��߿�"
                CellValue = TimeTablePara.DiagramStylePara.HourTime60LineWidth
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom1.ExecuteNonQuery()

                cellName = "Сʱ��ͼ60�ָ�����ɫ"
                CellValue = TimeTablePara.DiagramStylePara.HourTime60LineColor
                Str = "update ����ͼϵͳ������ set " & _
                        "��ֵ ='" & CellValue & "'" & _
                        "where ������ = '" & cellName & "'"
                Dim MyCom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                MyCom2.ExecuteNonQuery()

        End Select


        cellName = "��վ������������"
        CellValue = TimeTablePara.DiagramStylePara.StaNameFontName
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom02 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom02.ExecuteNonQuery()

        cellName = "��վ���������С"
        CellValue = TimeTablePara.DiagramStylePara.StaNameFontSize
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom03 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom03.ExecuteNonQuery()

        cellName = "��վ�����������"
        CellValue = TimeTablePara.DiagramStylePara.StaNameFontBold
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom04 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom04.ExecuteNonQuery()

        cellName = "��վ��������б��"
        CellValue = TimeTablePara.DiagramStylePara.StaNameFontItalic
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom05 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom05.ExecuteNonQuery()

        cellName = "��վ����������ɫ"
        CellValue = TimeTablePara.DiagramStylePara.StaNameFontColor
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom06 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom06.ExecuteNonQuery()

        cellName = "ʱ���ע��������"
        CellValue = TimeTablePara.DiagramStylePara.TimeFontName
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom07 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom07.ExecuteNonQuery()

        cellName = "ʱ���ע�����С"
        CellValue = TimeTablePara.DiagramStylePara.TimeFontSize
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom08 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom08.ExecuteNonQuery()

        cellName = "ʱ���ע�������"
        CellValue = TimeTablePara.DiagramStylePara.TimeFontBold
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom09 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom09.ExecuteNonQuery()

        cellName = "ʱ���ע����б��"
        CellValue = TimeTablePara.DiagramStylePara.TimeFontItalic
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom010 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom010.ExecuteNonQuery()

        cellName = "ʱ���ע������ɫ"
        CellValue = TimeTablePara.DiagramStylePara.TimeFontColor
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom011 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom011.ExecuteNonQuery()

        cellName = "һ�㳵վ����������"
        CellValue = TimeTablePara.DiagramStylePara.StaLineStyle
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom012 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom012.ExecuteNonQuery()

        cellName = "һ�㳵վ�����߿��"
        CellValue = TimeTablePara.DiagramStylePara.StaLineWidth
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom013 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom013.ExecuteNonQuery()

        cellName = "һ�㳵վ��������ɫ"
        CellValue = TimeTablePara.DiagramStylePara.StaLineColor
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom014 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom014.ExecuteNonQuery()

        cellName = "�ֲ�վ����������"
        CellValue = TimeTablePara.DiagramStylePara.FenStaLineStyle
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom015 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom015.ExecuteNonQuery()

        cellName = "�ֲ�վ�����߿��"
        CellValue = TimeTablePara.DiagramStylePara.FenStaLineWidth
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom016 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom016.ExecuteNonQuery()

        cellName = "�ֲ�վ��������ɫ"
        CellValue = TimeTablePara.DiagramStylePara.FenStaLineColor
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom017 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom017.ExecuteNonQuery()

        cellName = "��������������"
        CellValue = TimeTablePara.DiagramStylePara.CheChangStaLineStyle
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom018 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom018.ExecuteNonQuery()

        cellName = "���������߿��"
        CellValue = TimeTablePara.DiagramStylePara.CheChangStaLineWidth
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom019 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom019.ExecuteNonQuery()


        cellName = "������������ɫ"
        CellValue = TimeTablePara.DiagramStylePara.CheChangStaLineColor
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom020 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom020.ExecuteNonQuery()

        MyConn.Close()
        Call InputLineStyleInfor()

        MsgBox("�Ѿ��ɹ�����!", , "��ʾ")
    End Sub

    Private Sub InputLineStyleInfor()
        Select Case Me.cmbDigramStyle.Text
            Case "һ�ָ�"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "һ�ָ���"
                            TimeTablePara.DiagramStylePara.OneTime1LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.OneTime1LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.OneTime1LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "��ָ���"
                            TimeTablePara.DiagramStylePara.OneTime5LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.OneTime5LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.OneTime5LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "ʮ�ָ���"
                            TimeTablePara.DiagramStylePara.OneTime10LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.OneTime10LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.OneTime10LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "��Сʱ����"
                            TimeTablePara.DiagramStylePara.OneTime30LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.OneTime30LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.OneTime30LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "Сʱ����"
                            TimeTablePara.DiagramStylePara.OneTime60LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.OneTime60LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.OneTime60LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                    End Select
                End If
            Case "���ָ�"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "���ָ���"
                            TimeTablePara.DiagramStylePara.TwoTime2LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.TwoTime2LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.TwoTime2LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "ʮ�ָ���"
                            TimeTablePara.DiagramStylePara.TwoTime10LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.TwoTime10LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.TwoTime10LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "��Сʱ����"
                            TimeTablePara.DiagramStylePara.TwoTime30LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.TwoTime30LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.TwoTime30LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "Сʱ����"
                            TimeTablePara.DiagramStylePara.TwoTime60LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.TwoTime60LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.TwoTime60LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                    End Select
                End If
            Case "ʮ�ָ�"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "ʮ�ָ���"
                            TimeTablePara.DiagramStylePara.TenTime10LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.TenTime10LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.TenTime10LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "��Сʱ����"
                            TimeTablePara.DiagramStylePara.TenTime30LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.TenTime30LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.TenTime30LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                        Case "Сʱ����"
                            TimeTablePara.DiagramStylePara.TenTime60LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.TenTime60LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.TenTime60LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                    End Select
                End If
            Case "Сʱ��"
                If Me.lstTimeLine.SelectedItem <> Nothing Then
                    Select Case Me.lstTimeLine.SelectedItem.ToString
                        Case "Сʱ����"
                            TimeTablePara.DiagramStylePara.HourTime60LineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                            TimeTablePara.DiagramStylePara.HourTime60LineWidth = Me.numLineWidth.Value
                            TimeTablePara.DiagramStylePara.HourTime60LineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                    End Select
                End If
        End Select

        If Me.lstStaLine.SelectedItem <> Nothing Then
            Select Case Me.lstStaLine.SelectedItem.ToString
                Case "һ�㳵վ"
                    TimeTablePara.DiagramStylePara.StaLineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                    TimeTablePara.DiagramStylePara.StaLineWidth = Me.numLineWidth.Value
                    TimeTablePara.DiagramStylePara.StaLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                Case "��վ"
                    TimeTablePara.DiagramStylePara.FenStaLineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                    TimeTablePara.DiagramStylePara.FenStaLineWidth = Me.numLineWidth.Value
                    TimeTablePara.DiagramStylePara.FenStaLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
                Case "����"
                    TimeTablePara.DiagramStylePara.CheChangStaLineStyle = GetLineTextNameFromStyle(Me.cmbLineStyle.Text.Trim)
                    TimeTablePara.DiagramStylePara.CheChangStaLineWidth = Me.numLineWidth.Value
                    TimeTablePara.DiagramStylePara.CheChangStaLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTimeLineColor.BackColor)
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
        Call addOneUndoInf()
        Call RefreshDiagram(1)
        Me.Close()
    End Sub

    Private Sub btnSelectFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectFont.Click
        Dim nd As New FontDialog
        Dim f As Font
        If Me.lstFont.SelectedItem <> Nothing Then
            Select Case Me.lstFont.SelectedItem.ToString
                Case "��վ����"
                    f = New Font(TimeTablePara.DiagramStylePara.StaNameFontName, TimeTablePara.DiagramStylePara.StaNameFontSize)
                    If TimeTablePara.DiagramStylePara.StaNameFontBold = True Then
                        If TimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If TimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If
                    nd.Font = f
                    If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TimeTablePara.DiagramStylePara.StaNameFontName = nd.Font.Name
                        TimeTablePara.DiagramStylePara.StaNameFontSize = nd.Font.Size
                        TimeTablePara.DiagramStylePara.StaNameFontBold = nd.Font.Bold
                        TimeTablePara.DiagramStylePara.StaNameFontItalic = nd.Font.Italic
                    End If

                Case "ʱ���ע"

                    f = New Font(TimeTablePara.DiagramStylePara.TimeFontName, TimeTablePara.DiagramStylePara.TimeFontSize)
                    If TimeTablePara.DiagramStylePara.TimeFontBold = True Then
                        If TimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If TimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If

                    nd.Font = f
                    If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TimeTablePara.DiagramStylePara.TimeFontName = nd.Font.Name
                        TimeTablePara.DiagramStylePara.TimeFontSize = nd.Font.Size
                        TimeTablePara.DiagramStylePara.TimeFontBold = nd.Font.Bold
                        TimeTablePara.DiagramStylePara.TimeFontItalic = nd.Font.Italic
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
                        TimeTablePara.DiagramStylePara.StaNameFontColor = System.Drawing.ColorTranslator.ToHtml(Me.labFontColor.BackColor)
                    Case "ʱ���ע"
                        TimeTablePara.DiagramStylePara.TimeFontColor = System.Drawing.ColorTranslator.ToHtml(Me.labFontColor.BackColor)
                End Select
            End If
            Call ShowFontPreView()
        End If
    End Sub

    '����Ԥ��
    Private Sub ShowFontPreView()
        'TimeTablePara.DiagramStylePara.StaNameFontStyle = New Font("����", 20)
        'TimeTablePara.DiagramStylePara.TimeFontStyle = New Font("����", 20)
        Me.picFontShow.Refresh()
        Dim g As System.Drawing.Graphics
        g = Me.picFontShow.CreateGraphics
        Dim newBrush As Brush
        Dim f As Font
        If Me.lstFont.SelectedItem <> Nothing Then
            Select Case Me.lstFont.SelectedItem.ToString
                Case "��վ����"
                    f = New Font(TimeTablePara.DiagramStylePara.StaNameFontName, TimeTablePara.DiagramStylePara.StaNameFontSize)
                    If TimeTablePara.DiagramStylePara.StaNameFontBold = True Then
                        If TimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If TimeTablePara.DiagramStylePara.StaNameFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If

                    newBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.StaNameFontColor))
                    g.DrawString("����Ԥ��Ч��012345aAbBcC", f, newBrush, 10, 10)

                    Me.labFontColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.StaNameFontColor)
                Case "ʱ���ע"
                    f = New Font(TimeTablePara.DiagramStylePara.TimeFontName, TimeTablePara.DiagramStylePara.TimeFontSize)
                    If TimeTablePara.DiagramStylePara.TimeFontBold = True Then
                        If TimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If TimeTablePara.DiagramStylePara.TimeFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If
                    newBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TimeFontColor))
                    g.DrawString("����Ԥ��Ч��012345aAbBcC", f, newBrush, 10, 10)
                    Me.labFontColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TimeFontColor)
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
                    Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.StaLineStyle)
                    Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.StaLineWidth
                    Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.StaLineColor)
                Case "��վ"
                    Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.FenStaLineStyle)
                    Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.FenStaLineWidth
                    Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.FenStaLineColor)
                Case "����"
                    Me.cmbLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.CheChangStaLineStyle)
                    Me.numLineWidth.Value = TimeTablePara.DiagramStylePara.CheChangStaLineWidth
                    Me.labTimeLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.CheChangStaLineColor)
            End Select
        End If
        Call ShowTimeLinePrint()
    End Sub

    Private Sub cmbTrainStyle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTrainStyle.SelectedIndexChanged
        Dim i As Integer
        Select Case Me.cmbTrainStyle.SelectedItem.ToString
            Case "����������"
                Me.lstTrainStyle.Items.Clear()
                Me.lstTrainStyle.Items.Add("����������")
            Case "���г���������"
                Me.lstTrainStyle.Items.Clear()
                Me.lstTrainStyle.Items.Add("���г���������")
            Case "�����߰���·����"
                Me.lstTrainStyle.Items.Clear()
                For i = 1 To UBound(BasicTrainInf)
                    If BasicTrainInf(i).sJiaoLuName <> "" Then
                        Me.lstTrainStyle.Items.Add(BasicTrainInf(i).sJiaoLuName)
                    End If
                Next i
            Case "�����߰����б��"
                Dim ifIn As Integer
                Dim j As Integer
                Me.lstTrainStyle.Items.Clear()
                For i = 1 To UBound(TrainInf)
                    ifIn = 0
                    If TrainInf(i).sRunScaleName <> "" Then
                        For j = 1 To Me.lstTrainStyle.Items.Count
                            If Me.lstTrainStyle.Items(j - 1) = TrainInf(i).sRunScaleName Then
                                ifIn = 1
                            End If
                        Next
                        If ifIn = 0 Then
                            Me.lstTrainStyle.Items.Add(TrainInf(i).sRunScaleName)
                        End If
                    End If
                Next i

            Case "�����߰���·���"
                Dim ifIn As Integer
                Dim j As Integer
                Me.lstTrainStyle.Items.Clear()
                For i = 1 To UBound(TrainInf)
                    ifIn = 0
                    If TrainInf(i).sLineNum <> "" Then
                        For j = 1 To Me.lstTrainStyle.Items.Count
                            If Me.lstTrainStyle.Items(j - 1) = TrainInf(i).sLineNum Then
                                ifIn = 1
                            End If
                        Next
                        If ifIn = 0 Then
                            Me.lstTrainStyle.Items.Add(TrainInf(i).sLineNum)
                        End If
                    End If
                Next i

            Case "�����߰����ױ�ŷ���"
                Me.lstTrainStyle.Items.Clear()
                For i = 1 To UBound(ChediInfo)
                    If UBound(ChediInfo(i).nLinkTrain) > 0 Then
                        Me.lstTrainStyle.Items.Add(ChediInfo(i).sCheCiHao)
                    End If
                Next i
            Case "�����߰����ױ�ŷ���"
                Me.lstTrainStyle.Items.Clear()
                For i = 1 To UBound(ChediInfo)
                    If UBound(ChediInfo(i).nLinkTrain) > 0 Then
                        Me.lstTrainStyle.Items.Add(ChediInfo(i).sCheCiHao)
                    End If
                Next i
            Case "���������߷�������"
                Me.lstTrainStyle.Items.Clear()
                Me.lstTrainStyle.Items.Add("������")
                Me.lstTrainStyle.Items.Add("������")
        End Select
    End Sub

    Private Sub lstTrainStyle_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstTrainStyle.SelectedValueChanged
        Dim i, j As Integer
        Dim nState As Integer
        nState = 0
        Select Case Me.cmbTrainStyle.Text
            Case "����������"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    Select Case Me.cmbTrainStyle.SelectedItem.ToString
                        Case "����������"
                            Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.TrainLineStyle)
                            Me.numTrainLineWidth.Value = TimeTablePara.DiagramStylePara.TrainLineWidth
                            Me.labTrainLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.TrainLineColor)
                    End Select
                End If
            Case "���г���������"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    Select Case Me.cmbTrainStyle.SelectedItem.ToString
                        Case "���г���������"
                            Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(TimeTablePara.DiagramStylePara.CheDiLineStyle)
                            Me.numTrainLineWidth.Value = TimeTablePara.DiagramStylePara.CheDiLineWidth
                            Me.labTrainLineColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.CheDiLineColor)
                    End Select
                End If

            Case "�����߰���·����"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If TrainInf(i).sJiaoLuName = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                                Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(GetLineStyleTextFromStyleName(TrainInf(i).PrintLineStyle))
                                Me.numTrainLineWidth.Value = TrainInf(i).PrintLineWidth
                                Me.labTrainLineColor.BackColor = TrainInf(i).PrintLineColor
                                Exit For
                            End If
                        End If
                    Next
                End If
            Case "�����߰����б��"

                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If TrainInf(i).sRunScaleName = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                                Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(GetLineStyleTextFromStyleName(TrainInf(i).PrintLineStyle))
                                Me.numTrainLineWidth.Value = TrainInf(i).PrintLineWidth
                                Me.labTrainLineColor.BackColor = TrainInf(i).PrintLineColor
                                Exit For
                            End If
                        End If
                    Next
                End If

            Case "�����߰���·���"

                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If TrainInf(i).sLineNum = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                                Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(GetLineStyleTextFromStyleName(TrainInf(i).PrintLineStyle))
                                Me.numTrainLineWidth.Value = TrainInf(i).PrintLineWidth
                                Me.labTrainLineColor.BackColor = TrainInf(i).PrintLineColor
                                Exit For
                            End If
                        End If
                    Next
                End If

            Case "�����߰����ױ�ŷ���"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    nState = 0
                    For i = 1 To UBound(ChediInfo)
                        If ChediInfo(i).sCheCiHao = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                            For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                                If ChediInfo(i).nLinkTrain(j) > 0 Then
                                    Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(GetLineStyleTextFromStyleName(TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineStyle))
                                    Me.numTrainLineWidth.Value = TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineWidth
                                    Me.labTrainLineColor.BackColor = TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineColor
                                    nState = 1
                                    Exit For
                                End If
                            Next
                        End If
                        If nState = 1 Then Exit For
                    Next
                End If
            Case "�����߰����ױ�ŷ���"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(ChediInfo)
                        If ChediInfo(i).sCheCiHao = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                            Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(GetLineStyleTextFromStyleName(ChediInfo(i).PrintCheDiLinkStyle))
                            Me.numTrainLineWidth.Value = ChediInfo(i).PrintCheDiLinkWidth
                            Me.labTrainLineColor.BackColor = ChediInfo(i).PrintCheDiLinkColor
                            Exit For
                        End If
                    Next
                End If
            Case "���������߷�������"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If Me.lstTrainStyle.SelectedItem.ToString.Trim = "������" Then
                                If i Mod 2 = 0 Then '���г�
                                    Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(GetLineStyleTextFromStyleName(TrainInf(i).PrintLineStyle))
                                    Me.numTrainLineWidth.Value = TrainInf(i).PrintLineWidth
                                    Me.labTrainLineColor.BackColor = TrainInf(i).PrintLineColor
                                    Exit For
                                End If
                            Else
                                If i Mod 2 <> 0 Then '���г�
                                    Me.cmbTrainLineStyle.Text = GetLineStyleNameFromText(GetLineStyleTextFromStyleName(TrainInf(i).PrintLineStyle))
                                    Me.numTrainLineWidth.Value = TrainInf(i).PrintLineWidth
                                    Me.labTrainLineColor.BackColor = TrainInf(i).PrintLineColor
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                End If
        End Select
        Call ListTrainLineView()
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
        Dim i, j As Integer
        Select Case Me.cmbTrainStyle.Text
            Case "����������"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    Select Case Me.cmbTrainStyle.SelectedItem.ToString
                        Case "����������"
                            TimeTablePara.DiagramStylePara.TrainLineStyle = GetLineTextNameFromStyle(Me.cmbTrainLineStyle.Text)
                            TimeTablePara.DiagramStylePara.TrainLineWidth = Me.numTrainLineWidth.Value
                            TimeTablePara.DiagramStylePara.TrainLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTrainLineColor.BackColor)
                            For i = 1 To UBound(TrainInf)
                                If TrainInf(i).Train <> "" Then
                                    TrainInf(i).PrintLineStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                                    TrainInf(i).PrintLineWidth = Me.numTrainLineWidth.Value
                                    TrainInf(i).PrintLineColor = Me.labTrainLineColor.BackColor
                                End If
                            Next
                    End Select
                End If
            Case "���г���������"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    Select Case Me.cmbTrainStyle.SelectedItem.ToString
                        Case "���г���������"
                            TimeTablePara.DiagramStylePara.CheDiLineStyle = GetLineTextNameFromStyle(Me.cmbTrainLineStyle.Text)
                            TimeTablePara.DiagramStylePara.CheDiLineWidth = Me.numTrainLineWidth.Value
                            TimeTablePara.DiagramStylePara.CheDiLineColor = System.Drawing.ColorTranslator.ToHtml(Me.labTrainLineColor.BackColor)
                            For i = 1 To UBound(ChediInfo)
                                ChediInfo(i).PrintCheDiLinkStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                                ChediInfo(i).PrintCheDiLinkWidth = Me.numTrainLineWidth.Value
                                ChediInfo(i).PrintCheDiLinkColor = Me.labTrainLineColor.BackColor
                            Next
                    End Select
                End If
            Case "�����߰���·����"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If TrainInf(i).sJiaoLuName = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                                TrainInf(i).PrintLineStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                                TrainInf(i).PrintLineWidth = Me.numTrainLineWidth.Value
                                TrainInf(i).PrintLineColor = Me.labTrainLineColor.BackColor
                            End If
                        End If
                    Next
                End If
            Case "�����߰����б��"

                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If TrainInf(i).sRunScaleName = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                                TrainInf(i).PrintLineStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                                TrainInf(i).PrintLineWidth = Me.numTrainLineWidth.Value
                                TrainInf(i).PrintLineColor = Me.labTrainLineColor.BackColor
                            End If
                        End If
                    Next
                End If

            Case "�����߰���·���"

                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If TrainInf(i).sLineNum = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                                TrainInf(i).PrintLineStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                                TrainInf(i).PrintLineWidth = Me.numTrainLineWidth.Value
                                TrainInf(i).PrintLineColor = Me.labTrainLineColor.BackColor
                            End If
                        End If
                    Next
                End If

            Case "�����߰����ױ�ŷ���"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(ChediInfo)
                        If ChediInfo(i).sCheCiHao = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                            For j = 1 To UBound(ChediInfo(i).nLinkTrain)
                                If ChediInfo(i).nLinkTrain(j) > 0 Then
                                    TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                                    TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineWidth = Me.numTrainLineWidth.Value
                                    TrainInf(ChediInfo(i).nLinkTrain(j)).PrintLineColor = Me.labTrainLineColor.BackColor
                                End If
                            Next
                        End If
                    Next
                End If

            Case "�����߰����ױ�ŷ���"
                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(ChediInfo)
                        If ChediInfo(i).sCheCiHao = Me.lstTrainStyle.SelectedItem.ToString.Trim Then
                            ChediInfo(i).PrintCheDiLinkStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                            ChediInfo(i).PrintCheDiLinkWidth = Me.numTrainLineWidth.Value
                            ChediInfo(i).PrintCheDiLinkColor = Me.labTrainLineColor.BackColor
                        End If
                    Next
                End If
            Case "���������߷�������"

                If Me.lstTrainStyle.SelectedItem <> Nothing Then
                    For i = 1 To UBound(TrainInf)
                        If TrainInf(i).Train <> "" Then
                            If Me.lstTrainStyle.SelectedItem.ToString.Trim = "������" Then
                                If i Mod 2 = 0 Then '���г�
                                    TrainInf(i).PrintLineStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                                    TrainInf(i).PrintLineWidth = Me.numTrainLineWidth.Value
                                    TrainInf(i).PrintLineColor = Me.labTrainLineColor.BackColor
                                End If
                            Else
                                If i Mod 2 <> 0 Then '���г�
                                    TrainInf(i).PrintLineStyle = GetLineTextStyle(Me.cmbTrainLineStyle.Text)
                                    TrainInf(i).PrintLineWidth = Me.numTrainLineWidth.Value
                                    TrainInf(i).PrintLineColor = Me.labTrainLineColor.BackColor
                                End If
                            End If
                        End If
                    Next
                End If

        End Select
    End Sub

    Private Sub btnSetTrainDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrainDefault.Click
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String

        cellName = "��������������"
        CellValue = TimeTablePara.DiagramStylePara.TrainLineStyle
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom.ExecuteNonQuery()

        cellName = "�����������߿�"
        CellValue = TimeTablePara.DiagramStylePara.TrainLineWidth
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom1.ExecuteNonQuery()

        cellName = "������������ɫ"
        CellValue = TimeTablePara.DiagramStylePara.TrainLineColor
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom2.ExecuteNonQuery()

        cellName = "���г�������������"
        CellValue = TimeTablePara.DiagramStylePara.CheDiLineStyle
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom3 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom3.ExecuteNonQuery()

        cellName = "���г����������߿�"
        CellValue = TimeTablePara.DiagramStylePara.CheDiLineWidth
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom4 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom4.ExecuteNonQuery()

        cellName = "���г�����������ɫ"
        CellValue = TimeTablePara.DiagramStylePara.CheDiLineColor
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom5 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom5.ExecuteNonQuery()

        cellName = "���α����������"
        CellValue = TimeTablePara.DiagramStylePara.CheCiFontName
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom02 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom02.ExecuteNonQuery()

        cellName = "���α�������С"
        CellValue = TimeTablePara.DiagramStylePara.CheCiFontSize
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom03 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom03.ExecuteNonQuery()

        cellName = "���α���������"
        CellValue = TimeTablePara.DiagramStylePara.CheCiFontBold
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom04 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom04.ExecuteNonQuery()

        cellName = "���α������б��"
        CellValue = TimeTablePara.DiagramStylePara.CheCiFontItalic
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom05 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom05.ExecuteNonQuery()

        cellName = "���α��������ɫ"
        CellValue = TimeTablePara.DiagramStylePara.CheCiFontColor
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom06 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom06.ExecuteNonQuery()

        cellName = "б�򳵴���������"
        CellValue = TimeTablePara.DiagramStylePara.XieCheCiFontName
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom07 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom07.ExecuteNonQuery()

        cellName = "б�򳵴������С"
        CellValue = TimeTablePara.DiagramStylePara.XieCheCiFontSize
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom08 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom08.ExecuteNonQuery()

        cellName = "б�򳵴��������"
        CellValue = TimeTablePara.DiagramStylePara.XieCheCiFontBold
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom09 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom09.ExecuteNonQuery()

        cellName = "б�򳵴�����б��"
        CellValue = TimeTablePara.DiagramStylePara.XieCheCiFontItalic
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom010 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom010.ExecuteNonQuery()

        cellName = "б�򳵴�������ɫ"
        CellValue = TimeTablePara.DiagramStylePara.XieCheCiFontColor
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom011 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom011.ExecuteNonQuery()

        MyConn.Close()
        MsgBox("�Ѿ��ɹ�����!", , "��ʾ")

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
                Case "���α��"
                    f = New Font(TimeTablePara.DiagramStylePara.CheCiFontName, TimeTablePara.DiagramStylePara.CheCiFontSize)
                    If TimeTablePara.DiagramStylePara.CheCiFontBold = True Then
                        If TimeTablePara.DiagramStylePara.CheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If TimeTablePara.DiagramStylePara.CheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If
                    nd.Font = f
                    If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TimeTablePara.DiagramStylePara.CheCiFontName = nd.Font.Name
                        TimeTablePara.DiagramStylePara.CheCiFontSize = nd.Font.Size
                        TimeTablePara.DiagramStylePara.CheCiFontBold = nd.Font.Bold
                        TimeTablePara.DiagramStylePara.CheCiFontItalic = nd.Font.Italic
                    End If

                Case "б�򳵴�"
                    f = New Font(TimeTablePara.DiagramStylePara.XieCheCiFontName, TimeTablePara.DiagramStylePara.XieCheCiFontSize)
                    If TimeTablePara.DiagramStylePara.XieCheCiFontBold = True Then
                        If TimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If TimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If
                    nd.Font = f
                    If nd.ShowDialog = Windows.Forms.DialogResult.OK Then
                        TimeTablePara.DiagramStylePara.XieCheCiFontName = nd.Font.Name
                        TimeTablePara.DiagramStylePara.XieCheCiFontSize = nd.Font.Size
                        TimeTablePara.DiagramStylePara.XieCheCiFontBold = nd.Font.Bold
                        TimeTablePara.DiagramStylePara.XieCheCiFontItalic = nd.Font.Italic
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
                Case "���α��"
                    f = New Font(TimeTablePara.DiagramStylePara.CheCiFontName, TimeTablePara.DiagramStylePara.CheCiFontSize)
                    If TimeTablePara.DiagramStylePara.CheCiFontBold = True Then
                        If TimeTablePara.DiagramStylePara.CheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If TimeTablePara.DiagramStylePara.CheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If

                    newBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.CheCiFontColor))
                    g.DrawString("����Ԥ��Ч��012345aAbBcC", f, newBrush, 10, 10)
                    Me.labCheCiColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.CheCiFontColor)

                Case "б�򳵴�"
                    f = New Font(TimeTablePara.DiagramStylePara.XieCheCiFontName, TimeTablePara.DiagramStylePara.XieCheCiFontSize)
                    If TimeTablePara.DiagramStylePara.XieCheCiFontBold = True Then
                        If TimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Bold Or FontStyle.Italic)
                        Else
                            f = New Font(f, FontStyle.Bold)
                        End If
                    Else
                        If TimeTablePara.DiagramStylePara.XieCheCiFontItalic = True Then
                            f = New Font(f, FontStyle.Italic)
                        End If
                    End If

                    newBrush = New SolidBrush(System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.XieCheCiFontColor))
                    g.DrawString("����Ԥ��Ч��012345aAbBcC", f, newBrush, 10, 10)
                    Me.labCheCiColor.BackColor = System.Drawing.ColorTranslator.FromHtml(TimeTablePara.DiagramStylePara.XieCheCiFontColor)
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
                    Case "���α��"
                        TimeTablePara.DiagramStylePara.CheCiFontColor = System.Drawing.ColorTranslator.ToHtml(Me.labCheCiColor.BackColor)
                    Case "б�򳵴�"
                        TimeTablePara.DiagramStylePara.XieCheCiFontColor = System.Drawing.ColorTranslator.ToHtml(Me.labCheCiColor.BackColor)
                End Select
            End If
            Call ShowCheCiFontPreView()
        End If
    End Sub

    Private Sub btnSetTrainLine_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetTrainLine.Click
        Call SetTrainLineStyle()
        Call RefreshDiagram(1)
    End Sub
End Class