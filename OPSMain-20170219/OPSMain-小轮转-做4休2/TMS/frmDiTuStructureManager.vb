Public Class frmDiTuStructureManager
    Dim tmpName() As String

    Private Sub frmDiTuStructureManager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call ListItems()
    End Sub
    Private Sub ListItems()
        Dim myWS As DAO.Workspace
        Dim DE As DAO.DBEngine = New DAO.DBEngine
        myWS = DE.Workspaces(0)

        Dim dbData As DAO.Database
        Dim RSdata As DAO.Recordset
        Dim RSdata1 As DAO.Recordset
        Dim i As Integer, j As Integer
        Dim nNum As Integer
        Dim bFind As Boolean
        ReDim tmpName(0)
        Dim ifFind As Integer
        Me.listViewName.Items.Clear()

        bFind = False
        ifFind = 0
        dbData = myWS.OpenDatabase("", False, False, g_strNetMainPathOpenInfor)
        RSdata1 = dbData.OpenRecordset("select distinct 底图名称 from 底图结构")
        If RSdata1.RecordCount > 0 Then
            RSdata1.MoveLast()
            nNum = RSdata1.RecordCount
        End If
        Dim lvItem As ListViewItem
        Dim liFile(1) As String
        If nNum > 0 Then
            RSdata1.MoveFirst()
            ReDim tmpName(nNum)
            For i = 1 To nNum
                tmpName(i) = RSdata1.Fields("底图名称").Value.ToString.Trim
                RSdata1.MoveNext()
            Next

            RSdata = dbData.OpenRecordset("select * from 底图结构 order by ID")
            If RSdata.RecordCount > 0 Then
                RSdata.MoveLast()
                nNum = RSdata.RecordCount
            End If
            If nNum > 0 Then
                For j = 1 To UBound(tmpName)
                    RSdata.MoveFirst()
                    For i = 1 To nNum
                        If tmpName(j) = RSdata.Fields("底图名称").Value Then
                            liFile(0) = tmpName(j)
                            If RSdata.Fields("是否默认").Value.ToString.Trim = "1" Then
                                liFile(1) = "默认"
                            Else
                                liFile(1) = "无"
                            End If
                            lvItem = New ListViewItem(liFile)
                            Me.listViewName.Items.Add(lvItem)
                            Exit For
                        End If
                        RSdata.MoveNext()
                    Next
                Next
            End If
            myWS.Close()
        Else
            MsgBox("对不起,你还有添加底图结构,请先添加!", , "提示")
            Exit Sub
        End If

    End Sub
    Private Sub btnRename_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRename.Click

        ' Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)

        Dim i As Integer
        Dim Str As String
        Dim nf As New frmInputBox

        Dim sNewName As String
        Dim sName As String
        If Me.listViewName.SelectedItems.Count > 0 Then
            sName = Me.listViewName.SelectedItems(0).SubItems(0).Text
            nf.Text = "输入底图名称"
            nf.labTitle.Text = "输入或选择底图名称:"
            nf.cmbText.Visible = True
            nf.txtText.Visible = False
            nf.txtText.Text = ""
            nf.cmbText.Items.Clear()
            If UBound(tmpName) > 0 Then
                For i = 1 To UBound(tmpName)
                    nf.cmbText.Items.Add(tmpName(i))
                Next
                nf.cmbText.Text = sName
            End If
sFirst:
            nf.ShowDialog()
            If StrInputBoxCombText <> "" And bCancelInput = 0 Then
                sNewName = StrInputBoxCombText
                If sName = sNewName Then
                    MsgBox("底图名称相同,请重新输入!", , "提示")
                    GoTo sFirst
                Else
                    Dim strTable1 As String = "select * from 底图结构 where 底图名称='" & sNewName & "'"
                    Dim Mydc1 As New Data.OleDb.OleDbDataAdapter(strTable1, strCon)
                    '创建一个Datasetd
                    Dim myDataSet1 As Data.DataSet = New Data.DataSet
                    Mydc1.Fill(myDataSet1, "底图结构")
                    Dim myTable1 As Data.DataTable
                    myTable1 = myDataSet1.Tables("底图结构")
                    If myTable1.Rows.Count > 0 Then
                        If MsgBox("该底图名称已经存在，是否覆盖原来的名称！", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "确定操作") = MsgBoxResult.Yes Then
                            Str = "delete * from  底图结构 where 底图名称='" & sNewName & "'"
                            If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                            Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                            Mcom1.ExecuteNonQuery()

                        Else
                            GoTo sFirst
                        End If
                    Else
                    End If
                    If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                    Str = "update 底图结构 set  底图名称='" & sNewName & "' where 底图名称='" & sName & "'"
                    Dim Mcom As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                    Mcom.ExecuteNonQuery()
                    MyConn.Close()
                    Call ListItems()
                End If
            End If
        Else
            MsgBox("请先选择要修改的底图名称!", , "提示")
        End If

    End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim sName As String
        Dim Str As String
        If Me.listViewName.SelectedItems.Count > 0 Then
            sName = Me.listViewName.SelectedItems(0).SubItems(0).Text
            If MsgBox("确定删除 [" & sName & "] 底图结构吗?", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "确定操作") = MsgBoxResult.Yes Then
                Str = "delete * from  底图结构 where 底图名称='" & sName & "'"
                If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                Mcom1.ExecuteNonQuery()
                MyConn.Close()
                Call ListItems()
            End If
        Else
            MsgBox("请先选择要删除的底图名称!", , "提示")
        End If
    End Sub

    Private Sub btnSetDefault_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDefault.Click
        'Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & g_strNetMainPath & "'"
        Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
        Dim sName As String
        Dim Str As String
        If Me.listViewName.SelectedItems.Count > 0 Then
            sName = Me.listViewName.SelectedItems(0).SubItems(0).Text
            If MsgBox("确定设置 [" & sName & "] 为默认底图结构吗?", MsgBoxStyle.Information + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "确定操作") = MsgBoxResult.Yes Then
                Str = "update 底图结构 set 是否默认='0' where 是否默认='1'"
                If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                Dim Mcom1 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                Mcom1.ExecuteNonQuery()

                Str = "update 底图结构 set 是否默认='1' where  底图名称='" & sName & "'"
                If MyConn.State = Data.ConnectionState.Closed Then MyConn.Open()
                Dim Mcom2 As Data.OleDb.OleDbCommand = New Data.OleDb.OleDbCommand(Str, MyConn)
                Mcom2.ExecuteNonQuery()
                'MyConn.ResetState()
                MyConn.Close()
                Call ListItems()
            End If
        Else
            MsgBox("请先选择要设置的底图名称!", , "提示")
        End If
    End Sub


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class