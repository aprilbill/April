Public Class frmCSTimeTablePara
    Public sCurParaState As String

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click

        CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime = GetDiagramParaValueFromDataGrid("��ͼ��ʼʱ��")
        CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime = GetDiagramParaValueFromDataGrid("��ͼ��ʾʱ���")
        CSTimeTablePara.TimeTableDiagramPara.intCompareFirstTime = HourToSecond(GetDiagramParaValueFromDataGrid("ʱ��Ƚ���ʼʱ��"))
        CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = GetDiagramParaValueFromDataGrid("��ͼ��")
        CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = GetDiagramParaValueFromDataGrid("��ͼ��")
        CSTimeTablePara.TimeTableDiagramPara.strTimeFormat = GetDiagramParaValueFromDataGrid("��ͼʱ�ָ�ʽ")
        CSTimeTablePara.TimeTableDiagramPara.sngtopBlank = GetDiagramParaValueFromDataGrid("��ͼ���¿հ׸߶�")
        CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank = GetDiagramParaValueFromDataGrid("��ͼʱ��հ׸߶�")
        CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank = GetDiagramParaValueFromDataGrid("��ͼ���ҿհ׸߶�")
        CSTimeTablePara.TimeTableDiagramPara.sngStaBlank = GetDiagramParaValueFromDataGrid("��ͼ��վ�հ׿��")
        CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX = GetDiagramParaValueFromDataGrid("����������")
        CSTimeTablePara.TimeTableDiagramPara.sngPubTopY = GetDiagramParaValueFromDataGrid("���������߶�")
        CSTimeTablePara.TimeTableDiagramPara.sngPicStationWidth = GetDiagramParaValueFromDataGrid("��վվ��ͼ��")
        CSTimeTablePara.TimeTableDiagramPara.sngPicStationHeight = GetDiagramParaValueFromDataGrid("��վվ��ͼ��")
        Call ResetElsePara()
        Call CSRefreshDiagram(0)

    End Sub

    Private Sub btnSetDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDefault.Click

        Dim i As Integer
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String

        For i = 1 To Me.dataGrid.Rows.Count
            cellName = Me.dataGrid.Rows(i - 1).Cells(1).Value
            CellValue = Me.dataGrid.Rows(i - 1).Cells(2).Value
            Str = "update CS_CSTimeTableSystemPara set " & _
                    "PARAVALUE ='" & CellValue & "'" & _
                    "where PARANAME = '" & cellName & "' AND  LINEID='" & CStr(strCurlineID) & "'"
            Globle.Method.UpdateDataForAccess(Str)
        Next
        UpDateElsePara()

        MsgBox("�Ѿ��ɹ�����", MsgBoxStyle.OkOnly, "��ʾ")
    End Sub

    '���ݲ������Ƶõ���ֵ
    Private Function GetDiagramParaValue(ByVal sParaName As String) As String
        GetDiagramParaValue = ""
        Select Case sCurParaState
            Case "ϵͳ����"
                Select Case sParaName
                    Case "ʹ�õ�λ����"
                        GetDiagramParaValue = SystemPara.sUserCompanyName
                    Case "��ͼͼƬ·��"
                        GetDiagramParaValue = SystemPara.sPicFilePath
                    Case "ϵͳ��ʽ"
                        GetDiagramParaValue = SystemPara.SystemStyle
                End Select

            Case "����ͼϵͳ����"
                Select Case sParaName
                    Case "��ͼ��ʼʱ��"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.intDiagramFirstTime
                    Case "��ͼ��ʾʱ���"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.intDiagramWholeTime
                    Case "ʱ��Ƚ���ʼʱ��"
                        GetDiagramParaValue = SecondToHour(CSTimeTablePara.TimeTableDiagramPara.intCompareFirstTime, 1)
                    Case "��ͼ��"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
                    Case "��ͼ��"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
                    Case "��ͼʱ�ָ�ʽ"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.strTimeFormat
                    Case "��ͼ���¿հ׸߶�"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngtopBlank
                    Case "��ͼʱ��հ׸߶�"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngTimeBlank
                    Case "��ͼ���ҿհ׸߶�"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngLeftBlank
                    Case "��ͼ��վ�հ׿��"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngStaBlank
                    Case "����������"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngPubLeftX
                    Case "���������߶�"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngPubTopY
                    Case "��վվ��ͼ��"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngPicStationWidth
                    Case "��վվ��ͼ��"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sngPicStationHeight
                    Case "�Ƿ���ʾ����"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi
                    Case "�Ƿ���ʾб�򳵴�"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi
                    Case "���׽�·�߸߶�"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.nCheDiLineHeight
                    Case "���׽�·������"
                        GetDiagramParaValue = CSTimeTablePara.TimeTableDiagramPara.sCheDiLineStyle
                    Case "��վ�ɵ��߼��"
                        GetDiagramParaValue = CSTimeTablePara.StaDiagramePara.nStaLineHeight
                    Case "�����߿ɵ���ʱ���"
                        GetDiagramParaValue = CSTdrawLinePara.sMaxMoveTime
                    Case "�������ƶ�ʱ��"
                        GetDiagramParaValue = CSTdrawLinePara.sMoveStepTime
                End Select

        End Select

    End Function
    '��ͼʱ�Ĳ���
    Structure typeTdrawlLinePara
        Dim sMoveStepTime As Integer 'ÿһ���ƶ���ʱ��
        Dim sMaxMoveTime As Integer '���ڿ��ƶ���ʱ��
    End Structure
    Public CSTdrawLinePara As typeTdrawlLinePara

    '���ݲ������Ƶõ���ֵ
    Private Function GetDiagramParaValueFromDataGrid(ByVal sParaName As String) As String
        GetDiagramParaValueFromDataGrid = ""
        Dim i As Integer
        For i = 1 To Me.dataGrid.Rows.Count
            If Me.dataGrid.Rows(i - 1).Cells(1).Value.ToString = sParaName Then
                GetDiagramParaValueFromDataGrid = Me.dataGrid.Rows(i - 1).Cells(2).Value.ToString.Trim
                Exit For
            End If
        Next
    End Function

    Private Sub frmCSTimeTablePara_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Dim sqlstr As String = ""
        Me.dataGrid.Rows.Clear()

        sqlstr = "SELECT * FROM CS_CSTIMETABLESYSTEMPARA WHERE LINEID='" & CStr(strCurlineID) & "' ORDER BY PARAID"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        For i = 1 To tempTable.Rows.Count
            If tempTable.Rows(i - 1).Item("PARAID") <= 14 Then
                Try
                    Me.dataGrid.Rows.Add(1)
                Catch ex As Exception

                End Try

                Me.dataGrid.Rows(Me.dataGrid.Rows.Count - 1).Cells(0).Value = Me.dataGrid.Rows.Count
                Me.dataGrid.Rows(Me.dataGrid.Rows.Count - 1).Cells(1).Value = tempTable.Rows(i - 1).Item("PARANAME").ToString
                Me.dataGrid.Rows(Me.dataGrid.Rows.Count - 1).Cells(2).Value = GetDiagramParaValue(tempTable.Rows(i - 1).Item("PARANAME").ToString)
            End If
        Next

        '��������

        If CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi Then
            Me.chkShowCheci.Checked = True
        Else
            Me.chkShowCheci.Checked = False
        End If

        If CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo Then
            Me.chkShowDriverNo.Checked = True
        Else
            Me.chkShowDriverNo.Checked = False
        End If

        For i = 1 To tempTable.Rows.Count

            If tempTable.Rows(i - 1).Item("PARANAME").ToString = "���׽�·�߸߶�" Then
                Me.numCheDiLineHeight.Value = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
            End If

            If tempTable.Rows(i - 1).Item("PARANAME").ToString = "���׽�·������" Then
                Me.cmbCheDiLineStyle.Text = tempTable.Rows(i - 1).Item("PARAVALUE").ToString.Trim
            End If

        Next

    End Sub

    '������������
    Private Sub UpDateElsePara()
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String

        cellName = "�Ƿ���ʾ����"
        If Me.chkShowCheci.Checked = True Then
            CellValue = "True"
        Else
            CellValue = "False"
        End If
        Str = "update CS_CSTimeTableSystemPara set " & _
                "PARAVALUE ='" & CellValue & "'" & _
                "where PARANAME = '" & cellName & "' AND  LINEID='" & CStr(strCurlineID) & "'"
        Globle.Method.UpdateDataForAccess(Str)

        cellName = "�Ƿ���ʾ˾�����"
        If Me.chkShowDriverNo.Checked = True Then
            CellValue = "True"
        Else
            CellValue = "False"
        End If
        Str = "update CS_CSTimeTableSystemPara set " & _
                "PARAVALUE ='" & CellValue & "'" & _
                "where PARANAME = '" & cellName & "' AND  LINEID='" & CStr(strCurlineID) & "'"
        Globle.Method.UpdateDataForAccess(Str)

        cellName = "���׽�·�߸߶�"
        CellValue = Me.numCheDiLineHeight.Value
        Str = "update CS_CSTimeTableSystemPara set " & _
                "PARAVALUE ='" & CellValue & "'" & _
                "where PARANAME = '" & cellName & "' AND  LINEID='" & CStr(strCurlineID) & "'"
        Globle.Method.UpdateDataForAccess(Str)

        cellName = "���׽�·������"
        CellValue = Me.cmbCheDiLineStyle.Text.Trim
        Str = "update CS_CSTimeTableSystemPara set " & _
                "PARAVALUE ='" & CellValue & "'" & _
                "where PARANAME = '" & cellName & "' AND  LINEID='" & CStr(strCurlineID) & "'"
        Globle.Method.UpdateDataForAccess(Str)

    End Sub

    '������������ֵ
    Private Sub ResetElsePara()
        If Me.chkShowDriverNo.Checked = True Then
            CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = True
        Else
            CSTimeTablePara.TimeTableDiagramPara.nIfPrintDriverNo = False
        End If

        If Me.chkShowCheci.Checked = True Then
            CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi = True
        Else
            CSTimeTablePara.TimeTableDiagramPara.nifPrintCheCi = False
        End If

        CSTimeTablePara.TimeTableDiagramPara.nCheDiLineHeight = Me.numCheDiLineHeight.Value
        CSTimeTablePara.TimeTableDiagramPara.sCheDiLineStyle = Me.cmbCheDiLineStyle.Text.Trim

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class