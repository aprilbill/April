Public Class frmTimeTablePara
    Public sCurParaState As String

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Select Case sCurParaState
            Case "ϵͳ����"
                SystemPara.sPicFilePath = GetDiagramParaValueFromDataGrid("��ͼͼƬ·��")
                SystemPara.sUserCompanyName = GetDiagramParaValueFromDataGrid("ʹ�õ�λ����")
                SystemPara.SystemStyle = GetDiagramParaValueFromDataGrid("ϵͳ��ʽ")

            Case "����ͼϵͳ����"
                TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime = GetDiagramParaValueFromDataGrid("��ͼ��ʼʱ��")
                TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime = GetDiagramParaValueFromDataGrid("��ͼ��ʾʱ���")
                TimeTablePara.TimeTableDiagramPara.intCompareFirstTime = HourToSecond(GetDiagramParaValueFromDataGrid("ʱ��Ƚ���ʼʱ��"))
                TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth = GetDiagramParaValueFromDataGrid("��ͼ��")
                TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight = GetDiagramParaValueFromDataGrid("��ͼ��")
                TimeTablePara.TimeTableDiagramPara.strTimeFormat = GetDiagramParaValueFromDataGrid("��ͼʱ�ָ�ʽ")
                TimeTablePara.TimeTableDiagramPara.sngtopBlank = GetDiagramParaValueFromDataGrid("��ͼ���¿հ׸߶�")
                TimeTablePara.TimeTableDiagramPara.sngTimeBlank = GetDiagramParaValueFromDataGrid("��ͼʱ��հ׸߶�")
                TimeTablePara.TimeTableDiagramPara.sngLeftBlank = GetDiagramParaValueFromDataGrid("��ͼ���ҿհ׸߶�")
                TimeTablePara.TimeTableDiagramPara.sngStaBlank = GetDiagramParaValueFromDataGrid("��ͼ��վ�հ׿��")
                TimeTablePara.TimeTableDiagramPara.sngPubLeftX = GetDiagramParaValueFromDataGrid("����������")
                TimeTablePara.TimeTableDiagramPara.sngPubTopY = GetDiagramParaValueFromDataGrid("���������߶�")
                TimeTablePara.TimeTableDiagramPara.sngPicStationWidth = GetDiagramParaValueFromDataGrid("��վվ��ͼ��")
                TimeTablePara.TimeTableDiagramPara.sngPicStationHeight = GetDiagramParaValueFromDataGrid("��վվ��ͼ��")
                Call ResetElsePara()
                Call RefreshDiagram(0)
        End Select

        Me.Close()
    End Sub

    Private Sub btnSetDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDefault.Click
        Dim i As Integer
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String
        Select Case sCurParaState
            Case "ϵͳ����"

                For i = 1 To Me.dataGrid.Rows.Count
                    cellName = Me.dataGrid.Rows(i - 1).Cells(1).Value
                    CellValue = Me.dataGrid.Rows(i - 1).Cells(2).Value
                    Str = "update ϵͳ������ set " & _
                            "��ֵ ='" & CellValue & "'" & _
                            "where ������ = '" & cellName & "'"

                    Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                    MyCom.ExecuteNonQuery()
                Next

            Case "����ͼϵͳ����"
                For i = 1 To Me.dataGrid.Rows.Count
                    cellName = Me.dataGrid.Rows(i - 1).Cells(1).Value
                    CellValue = Me.dataGrid.Rows(i - 1).Cells(2).Value
                    Str = "update ����ͼϵͳ������ set " & _
                            "��ֵ ='" & CellValue & "'" & _
                            "where ������ = '" & cellName & "'"

                    Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                    MyCom.ExecuteNonQuery()
                Next
                UpDateElsePara()
        End Select
        MsgBox("�Ѿ��ɹ�����", , "��ʾ")
        MyConn.Close()
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
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.intDiagramFirstTime
                    Case "��ͼ��ʾʱ���"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.intDiagramWholeTime
                    Case "ʱ��Ƚ���ʼʱ��"
                        GetDiagramParaValue = SecondToHour(TimeTablePara.TimeTableDiagramPara.intCompareFirstTime, 1)
                    Case "��ͼ��"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngPicDiagramWidth
                    Case "��ͼ��"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngPicDiagramHeight
                    Case "��ͼʱ�ָ�ʽ"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.strTimeFormat
                    Case "��ͼ���¿հ׸߶�"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngtopBlank
                    Case "��ͼʱ��հ׸߶�"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngTimeBlank
                    Case "��ͼ���ҿհ׸߶�"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngLeftBlank
                    Case "��ͼ��վ�հ׿��"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngStaBlank
                    Case "����������"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngPubLeftX
                    Case "���������߶�"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngPubTopY
                    Case "��վվ��ͼ��"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngPicStationWidth
                    Case "��վվ��ͼ��"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sngPicStationHeight
                    Case "�Ƿ���ʾ����"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.nifPrintCheCi
                    Case "�Ƿ���ʾб�򳵴�"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi
                    Case "���׽�·�߸߶�"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.nCheDiLineHeight
                    Case "���׽�·������"
                        GetDiagramParaValue = TimeTablePara.TimeTableDiagramPara.sCheDiLineStyle
                    Case "��վ�ɵ��߼��"
                        GetDiagramParaValue = TimeTablePara.StaDiagramePara.nStaLineHeight
                    Case "�����߿ɵ���ʱ���"
                        GetDiagramParaValue = TdrawLinePara.sMaxMoveTime
                    Case "�������ƶ�ʱ��"
                        GetDiagramParaValue = TdrawLinePara.sMoveStepTime
                End Select

        End Select

    End Function

    '���ݲ������Ƶõ���ֵ
    Private Function GetDiagramParaValueFromDataGrid(ByVal sParaName As String) As String
        GetDiagramParaValueFromDataGrid = ""
        Dim i As Integer
        For i = 1 To Me.dataGrid.Rows.Count
            If Me.dataGrid.Rows(i - 1).Cells(1).Value.ToString = sParaName Then
                GetDiagramParaValueFromDataGrid = Me.dataGrid.Rows(i - 1).Cells(2).Value.ToString
                Exit For
            End If
        Next
    End Function

    Private Sub frmTimeTablePara_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Dim nCurRow As Integer
        Me.dataGrid.Rows.Clear()
        Select Case sCurParaState
            Case "ϵͳ����"
                Dim strTable3 As String = "select * from ϵͳ������ order by ���"
                Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
                '����һ��Datasetd
                Dim myDataSet3 As Data.DataSet = New Data.DataSet
                Mydc3.Fill(myDataSet3, "ϵͳ������")
                Dim myTable3 As Data.DataTable
                myTable3 = myDataSet3.Tables("ϵͳ������")
                For i = 1 To myTable3.Rows.Count
                    Me.dataGrid.Rows.Add()
                    nCurRow = Me.dataGrid.Rows.Count - 1
                    Me.dataGrid.Rows(nCurRow).Cells(0).Value = nCurRow + 1
                    Me.dataGrid.Rows(nCurRow).Cells(1).Value = myTable3.Rows(i - 1).Item("������").ToString
                    Me.dataGrid.Rows(nCurRow).Cells(2).Value = GetDiagramParaValue(myTable3.Rows(i - 1).Item("������").ToString)
                Next

            Case "����ͼϵͳ����"
                Dim strTable3 As String = "select * from ����ͼϵͳ������ where ���<=14 order by ��� "
                Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
                '����һ��Datasetd
                Dim myDataSet3 As Data.DataSet = New Data.DataSet
                Mydc3.Fill(myDataSet3, "����ͼϵͳ������")
                Dim myTable3 As Data.DataTable
                myTable3 = myDataSet3.Tables("����ͼϵͳ������")
                For i = 1 To myTable3.Rows.Count
                    Me.dataGrid.Rows.Add()
                    nCurRow = Me.dataGrid.Rows.Count - 1
                    Me.dataGrid.Rows(nCurRow).Cells(0).Value = nCurRow + 1
                    Me.dataGrid.Rows(nCurRow).Cells(1).Value = myTable3.Rows(i - 1).Item("������").ToString
                    Me.dataGrid.Rows(nCurRow).Cells(2).Value = GetDiagramParaValue(myTable3.Rows(i - 1).Item("������").ToString)
                Next
                Call listElsePara()
        End Select
    End Sub

    Private Sub listElsePara() '��ʾ��������
        Dim nIfShowcheci As String
        Dim nIfShowXieCheci As String
        Dim nCheDiLineHeight As Integer
        Dim nGuDaoLineHeight As Integer
        Dim nCheDiLineStyle As String
        nIfShowcheci = GetDiagramParaValue("�Ƿ���ʾ����") 'GetCurParaValue("�Ƿ���ʾ����")
        If nIfShowcheci = "True" Then
            Me.chkShowCheCi.Checked = True
        Else
            Me.chkShowCheCi.Checked = False
        End If

        nIfShowXieCheci = GetDiagramParaValue("�Ƿ���ʾб�򳵴�") 'GetCurParaValue("�Ƿ���ʾб�򳵴�")
        If nIfShowXieCheci = "True" Then
            Me.chkXieCheci.Checked = True
        Else
            Me.chkXieCheci.Checked = False
        End If

        nCheDiLineHeight = GetDiagramParaValue("���׽�·�߸߶�") ' GetCurParaValue("���׽�·�߸߶�")
        Me.numCheDiLineHeight.Value = nCheDiLineHeight

        nCheDiLineStyle = GetDiagramParaValue("���׽�·������") ' GetCurParaValue("���׽�·������")
        Me.cmbCheDiLineStyle.Text = nCheDiLineStyle

        nGuDaoLineHeight = GetDiagramParaValue("��վ�ɵ��߼��") ' GetCurParaValue("��վ�ɵ��߼��")
        Me.numGuDaoLineHeight.Value = nGuDaoLineHeight

        Me.numLineAdjustWidth.Value = GetDiagramParaValue("�����߿ɵ���ʱ���")
        Me.NumLineMoveStep.Value = GetDiagramParaValue("�������ƶ�ʱ��")

        If TimeTablePara.TimeTableDiagramPara.sCheCiShowStyle = 1 Then
            Me.rbtSix.Checked = True
        Else
            Me.rbtFour.Checked = True
        End If
    End Sub

    Private Function GetCurParaValue(ByVal sPara As String) As String '�õ���ǰ��������ֵ
        GetCurParaValue = ""
        Dim strTable3 As String = "select * from ����ͼϵͳ������ where ������=' " & sPara & "' order by ��� "
        Dim Mydc3 As New Data.OleDb.OleDbDataAdapter(strTable3, strCon)
        '����һ��Datasetd
        Dim myDataSet3 As Data.DataSet = New Data.DataSet
        Mydc3.Fill(myDataSet3, "����ͼϵͳ������")
        Dim myTable3 As Data.DataTable
        myTable3 = myDataSet3.Tables("����ͼϵͳ������")
        If myTable3.Rows.Count > 0 Then
            GetCurParaValue = myTable3.Rows(0).Item("��ֵ").ToString.Trim
        End If
    End Function

    '������������
    Private Sub UpDateElsePara()
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
        Dim Str As String
        Dim cellName As String
        Dim CellValue As String

        cellName = "�Ƿ���ʾ����"
        If Me.chkShowCheCi.Checked = True Then
            CellValue = "True"
        Else
            CellValue = "False"
        End If
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom.ExecuteNonQuery()

        cellName = "�Ƿ���ʾб�򳵴�"
        If Me.chkXieCheci.Checked = True Then
            CellValue = "True"
        Else
            CellValue = "False"
        End If
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom1.ExecuteNonQuery()

        cellName = "���׽�·�߸߶�"
        CellValue = Me.numCheDiLineHeight.Value
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom2.ExecuteNonQuery()

        cellName = "���׽�·������"
        CellValue = Me.cmbCheDiLineStyle.Text.Trim
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom3 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom3.ExecuteNonQuery()

        cellName = "��վ�ɵ��߼��"
        CellValue = Me.numGuDaoLineHeight.Value
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom4 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom4.ExecuteNonQuery()

        cellName = "�����߿ɵ���ʱ���"
        CellValue = Me.numLineAdjustWidth.Value
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom5 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom5.ExecuteNonQuery()

        cellName = "�������ƶ�ʱ��"
        CellValue = Me.NumLineMoveStep.Value
        Str = "update ����ͼϵͳ������ set " & _
                "��ֵ ='" & CellValue & "'" & _
                "where ������ = '" & cellName & "'"
        Dim MyCom6 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
        MyCom6.ExecuteNonQuery()

        MyConn.Close()
    End Sub

    '������������ֵ
    Private Sub ResetElsePara()
        If Me.chkShowCheCi.Checked = True Then
            TimeTablePara.TimeTableDiagramPara.nifPrintCheCi = True
        Else
            TimeTablePara.TimeTableDiagramPara.nifPrintCheCi = False
        End If
  
        If Me.chkXieCheci.Checked = True Then
            TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi = True
        Else
            TimeTablePara.TimeTableDiagramPara.nIfPrintXieCheCi = False
        End If
    
        TimeTablePara.TimeTableDiagramPara.nCheDiLineHeight = Me.numCheDiLineHeight.Value
        TimeTablePara.TimeTableDiagramPara.sCheDiLineStyle = Me.cmbCheDiLineStyle.Text.Trim
        TimeTablePara.StaDiagramePara.nStaLineHeight = Me.numGuDaoLineHeight.Value

        If Me.rbtFour.Checked = True Then
            TimeTablePara.TimeTableDiagramPara.sCheCiShowStyle = 0
        Else
            TimeTablePara.TimeTableDiagramPara.sCheCiShowStyle = 1
        End If

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class