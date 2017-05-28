Public Class FrmPrepareTrain
    Private Zhefan As New Dictionary(Of String, Integer)
    Private EditState As Boolean = False
    Public flag As Integer = 0
    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Dim nTrain As Integer
        For j As Integer = 1 To UBound(CSchediInfo)
            Zhefan.Add(CSchediInfo(j).sCheCiHao, 0)
            Dim maxZhefan As Integer = 0
            For p As Integer = 1 To UBound(CSchediInfo(j).nLinkTrain)
                nTrain = CSchediInfo(j).nLinkTrain(p)

                If p > 1 Then
                    Dim prenTrain As Integer = CSchediInfo(j).nLinkTrain(p - 1)
                    Dim tmpMax As Integer = CSTrainInf(nTrain).Starting(CSTrainInf(nTrain).nPathID(1)) - CSTrainInf(prenTrain).Arrival(CSTrainInf(prenTrain).nPathID(UBound(CSTrainInf(prenTrain).nPathID)))
                    If tmpMax > maxZhefan Then
                        Zhefan(CSchediInfo(j).sCheCiHao) = tmpMax
                        maxZhefan = tmpMax
                    End If
                End If
            Next p
        Next j
        Dim myList As List(Of KeyValuePair(Of String, Integer)) = New List(Of KeyValuePair(Of String, Integer))(Zhefan)
        myList.Sort(Function(s1 As KeyValuePair(Of String, Integer), s2 As KeyValuePair(Of String, Integer))
                        Return -s1.Value.CompareTo(s2.Value)
                    End Function)
        Zhefan.Clear()
        For Each kvp As KeyValuePair(Of String, Integer) In myList
            Zhefan.Add(kvp.Key, kvp.Value)
        Next
        For Each chedi As String In Zhefan.Keys
            For j As Integer = 1 To UBound(CSchediInfo)
                If chedi = CSchediInfo(j).sCheCiHao Then
                    For p As Integer = 1 To UBound(CSchediInfo(j).nLinkTrain)
                        nTrain = CSchediInfo(j).nLinkTrain(p)
                        If CSTrainInf(nTrain).sIfBeiChe = 1 Then
                            DataGridView1.Rows.Add(CSchediInfo(j).sCheCiHao, CSTrainInf(nTrain).sPrintTrain, CSTrainInf(nTrain).StartStation, BeTime(CSTrainInf(nTrain).Starting(CSTrainInf(nTrain).nPathID(1))), CSTrainInf(nTrain).EndStation, BeTime(CSTrainInf(nTrain).Arrival(CSTrainInf(nTrain).nPathID(UBound(CSTrainInf(nTrain).nPathID)))), CSTrainInf(nTrain).BeiCheState)
                        Else
                            DataGridView1.Rows.Add(CSchediInfo(j).sCheCiHao, CSTrainInf(nTrain).sPrintTrain, CSTrainInf(nTrain).StartStation, BeTime(CSTrainInf(nTrain).Starting(CSTrainInf(nTrain).nPathID(1))), CSTrainInf(nTrain).EndStation, BeTime(CSTrainInf(nTrain).Arrival(CSTrainInf(nTrain).nPathID(UBound(CSTrainInf(nTrain).nPathID)))), "无")
                        End If
                        If p > 1 Then
                            Dim prenTrain As Integer = CSchediInfo(j).nLinkTrain(p - 1)
                            Dim tmpMax As Integer = CSTrainInf(nTrain).Starting(CSTrainInf(nTrain).nPathID(1)) - CSTrainInf(prenTrain).Arrival(CSTrainInf(prenTrain).nPathID(UBound(CSTrainInf(prenTrain).nPathID)))
                            If Zhefan(chedi) > 60 * 60 And Zhefan(chedi) = tmpMax Then
                                DataGridView1.Rows(DataGridView1.Rows.Count - 2).DefaultCellStyle.BackColor = Color.LightBlue
                                DataGridView1.Rows(DataGridView1.Rows.Count - 1).DefaultCellStyle.BackColor = Color.LightBlue
                            End If
                        End If
                    Next
                    Exit For
                End If
            Next
        Next
        EditState = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            Dim str As String = "delete from cs_preparedtrainlist where traindiagramid='" & DiagramCurID & "' and lineid='" & CurLineName & "'"
             Globle.Method.UpdateDataForAccess(str)
            ProgressBar1.Value = 0
            ProgressBar1.Maximum = UBound(CSchediInfo)
            Dim nTrain As Integer
            For j As Integer = 1 To UBound(CSchediInfo)
                ProgressBar1.Value = j
                For p As Integer = 1 To UBound(CSchediInfo(j).nLinkTrain)
                    nTrain = CSchediInfo(j).nLinkTrain(p)
                    If CSTrainInf(nTrain).sIfBeiChe = 1 Then
                        str = "insert into cs_preparedtrainlist values('" & CurLineName & "','" & DiagramCurID & "','" & CSchediInfo(j).sCheCiHao & "','" & CSTrainInf(nTrain).sPrintTrain & "','" & CSTrainInf(nTrain).StartStation & "','" & CSTrainInf(nTrain).Starting(CSTrainInf(nTrain).nPathID(1)) & "','" & CSTrainInf(nTrain).EndStation & "','" & CSTrainInf(nTrain).Arrival(CSTrainInf(nTrain).nPathID(UBound(CSTrainInf(nTrain).nPathID))) & "','" & CSTrainInf(nTrain).BeiCheState & "')"
                         Globle.Method.UpdateDataForAccess(str)
                        System.Threading.Thread.Sleep(50)
                    End If
                Next
            Next
            MsgBox("保存成功！")
            flag = 1
        Catch ex As Exception
            MsgBox("保存失败，请重新打开本窗体！")
        End Try
        
    End Sub


    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        If EditState = True AndAlso e.ColumnIndex = 6 Then
            Dim nTrain As Integer
            For j As Integer = 1 To UBound(CSchediInfo)
                For p As Integer = 1 To UBound(CSchediInfo(j).nLinkTrain)
                    nTrain = CSchediInfo(j).nLinkTrain(p)
                    If CSTrainInf(nTrain).sPrintTrain = DataGridView1.Rows(e.RowIndex).Cells("车次").Value.ToString AndAlso BeTime(CSTrainInf(nTrain).Starting(CSTrainInf(nTrain).nPathID(1)).ToString) = DataGridView1.Rows(e.RowIndex).Cells("发车时间").Value.ToString AndAlso BeTime(CSTrainInf(nTrain).Arrival(CSTrainInf(nTrain).nPathID(UBound(CSTrainInf(nTrain).nPathID)))) = DataGridView1.Rows(e.RowIndex).Cells("终到时间").Value.ToString Then
                        If DataGridView1.Rows(e.RowIndex).Cells("状态").Value.ToString = "无" Then
                            CSTrainInf(nTrain).sIfBeiChe = 0
                            CSTrainInf(nTrain).BeiCheState = ""
                        Else
                            CSTrainInf(nTrain).sIfBeiChe = 1
                            CSTrainInf(nTrain).BeiCheState = DataGridView1.Rows(e.RowIndex).Cells("状态").Value.ToString
                        End If
                    End If
                Next p
            Next j
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        DataGridView1.Rows.Clear()
        Dim nTrain As Integer
        If TextBox1.Text.Trim.Length < 2 Then
            For Each chedi As String In Zhefan.Keys
                For j As Integer = 1 To UBound(CSchediInfo)
                    If chedi = CSchediInfo(j).sCheCiHao Then
                        For p As Integer = 1 To UBound(CSchediInfo(j).nLinkTrain)
                            nTrain = CSchediInfo(j).nLinkTrain(p)
                            If CSTrainInf(nTrain).sIfBeiChe = 1 Then
                                DataGridView1.Rows.Add(CSchediInfo(j).sCheCiHao, CSTrainInf(nTrain).sPrintTrain, CSTrainInf(nTrain).StartStation, BeTime(CSTrainInf(nTrain).Starting(CSTrainInf(nTrain).nPathID(1))), CSTrainInf(nTrain).EndStation, BeTime(CSTrainInf(nTrain).Arrival(CSTrainInf(nTrain).nPathID(UBound(CSTrainInf(nTrain).nPathID)))), CSTrainInf(nTrain).BeiCheState)
                            Else
                                DataGridView1.Rows.Add(CSchediInfo(j).sCheCiHao, CSTrainInf(nTrain).sPrintTrain, CSTrainInf(nTrain).StartStation, BeTime(CSTrainInf(nTrain).Starting(CSTrainInf(nTrain).nPathID(1))), CSTrainInf(nTrain).EndStation, BeTime(CSTrainInf(nTrain).Arrival(CSTrainInf(nTrain).nPathID(UBound(CSTrainInf(nTrain).nPathID)))), "无")
                            End If
                            If p > 1 Then
                                Dim prenTrain As Integer = CSchediInfo(j).nLinkTrain(p - 1)
                                Dim tmpMax As Integer = CSTrainInf(nTrain).Starting(CSTrainInf(nTrain).nPathID(1)) - CSTrainInf(prenTrain).Arrival(CSTrainInf(prenTrain).nPathID(UBound(CSTrainInf(prenTrain).nPathID)))
                                If Zhefan(chedi) > 60 * 60 And Zhefan(chedi) = tmpMax Then
                                    DataGridView1.Rows(DataGridView1.Rows.Count - 2).DefaultCellStyle.BackColor = Color.LightBlue
                                    DataGridView1.Rows(DataGridView1.Rows.Count - 1).DefaultCellStyle.BackColor = Color.LightBlue
                                End If
                            End If
                        Next
                        Exit For
                    End If
                Next
            Next
        Else
            For Each chedi As String In Zhefan.Keys
                For j As Integer = 1 To UBound(CSchediInfo)
                    If chedi = CSchediInfo(j).sCheCiHao Then
                        For p As Integer = 1 To UBound(CSchediInfo(j).nLinkTrain)
                            nTrain = CSchediInfo(j).nLinkTrain(p)
                            If CSTrainInf(nTrain).sPrintTrain = TextBox1.Text.Trim Then
                                If CSTrainInf(nTrain).sIfBeiChe = 1 Then
                                    DataGridView1.Rows.Add(CSchediInfo(j).sCheCiHao, CSTrainInf(nTrain).sPrintTrain, CSTrainInf(nTrain).StartStation, BeTime(CSTrainInf(nTrain).Starting(CSTrainInf(nTrain).nPathID(1))), CSTrainInf(nTrain).EndStation, BeTime(CSTrainInf(nTrain).Arrival(CSTrainInf(nTrain).nPathID(UBound(CSTrainInf(nTrain).nPathID)))), CSTrainInf(nTrain).BeiCheState)
                                Else
                                    DataGridView1.Rows.Add(CSchediInfo(j).sCheCiHao, CSTrainInf(nTrain).sPrintTrain, CSTrainInf(nTrain).StartStation, BeTime(CSTrainInf(nTrain).Starting(CSTrainInf(nTrain).nPathID(1))), CSTrainInf(nTrain).EndStation, BeTime(CSTrainInf(nTrain).Arrival(CSTrainInf(nTrain).nPathID(UBound(CSTrainInf(nTrain).nPathID)))), "无")
                                End If
                            End If
                        Next
                        Exit For
                    End If
                Next
            Next
        End If
    End Sub
 
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DataGridView1.Rows.Clear()
        Dim nTrain As Integer
        For Each chedi As String In Zhefan.Keys
            For j As Integer = 1 To UBound(CSchediInfo)
                If chedi = CSchediInfo(j).sCheCiHao Then
                    For p As Integer = 1 To UBound(CSchediInfo(j).nLinkTrain)
                        nTrain = CSchediInfo(j).nLinkTrain(p)
                        If CSTrainInf(nTrain).sIfBeiChe = 1 Then
                            DataGridView1.Rows.Add(CSchediInfo(j).sCheCiHao, CSTrainInf(nTrain).sPrintTrain, CSTrainInf(nTrain).StartStation, BeTime(CSTrainInf(nTrain).Starting(CSTrainInf(nTrain).nPathID(1))), CSTrainInf(nTrain).EndStation, BeTime(CSTrainInf(nTrain).Arrival(CSTrainInf(nTrain).nPathID(UBound(CSTrainInf(nTrain).nPathID)))), CSTrainInf(nTrain).BeiCheState)
                        Else
                            DataGridView1.Rows.Add(CSchediInfo(j).sCheCiHao, CSTrainInf(nTrain).sPrintTrain, CSTrainInf(nTrain).StartStation, BeTime(CSTrainInf(nTrain).Starting(CSTrainInf(nTrain).nPathID(1))), CSTrainInf(nTrain).EndStation, BeTime(CSTrainInf(nTrain).Arrival(CSTrainInf(nTrain).nPathID(UBound(CSTrainInf(nTrain).nPathID)))), "无")
                        End If
                        If p > 1 Then
                            Dim prenTrain As Integer = CSchediInfo(j).nLinkTrain(p - 1)
                            Dim tmpMax As Integer = CSTrainInf(nTrain).Starting(CSTrainInf(nTrain).nPathID(1)) - CSTrainInf(prenTrain).Arrival(CSTrainInf(prenTrain).nPathID(UBound(CSTrainInf(prenTrain).nPathID)))
                            If Zhefan(chedi) > 60 * 60 And Zhefan(chedi) = tmpMax Then
                                DataGridView1.Rows(DataGridView1.Rows.Count - 2).DefaultCellStyle.BackColor = Color.LightBlue
                                DataGridView1.Rows(DataGridView1.Rows.Count - 1).DefaultCellStyle.BackColor = Color.LightBlue
                            End If
                        End If
                    Next
                    Exit For
                End If
            Next
        Next
    End Sub
End Class