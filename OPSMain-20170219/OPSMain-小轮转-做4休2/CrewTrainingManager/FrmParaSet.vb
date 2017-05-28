Public Class FrmParaSet
    Dim selDriver As New Dictionary(Of String, String)
    Public ifRefresh As Boolean = False
    Public Sub New(ByVal driverlist As Dictionary(Of String, String))

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        selDriver = driverlist
        For Each id As String In selDriver.Keys
            ListBox1.Items.Add(selDriver(id))
        Next
    End Sub
    Private Sub FrmParaSet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim str As String = "select areaname from cs_areainfo where lineid='" & CurLineName & "'"
        Dim tmpDT As New DataTable
        tmpDT = ReadData(str)
        If IsNothing(tmpDT) = False AndAlso tmpDT.Rows.Count > 0 Then
            For i As Integer = 0 To tmpDT.Rows.Count - 1
                If ComboBox1.Items.Contains(tmpDT.Rows(i).Item(0).ToString.Trim) = False Then
                    ComboBox1.Items.Add(tmpDT.Rows(i).Item(0).ToString.Trim)
                End If
            Next
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.Text.Trim = "" Then
            MsgBox("请选择需要修改的区域")
            Exit Sub
        End If
        Dim str As String = ""
        ProgressBar1.Maximum = selDriver.Count
        ProgressBar1.Value = 1
        For Each id As String In selDriver.Keys
            ProgressBar1.PerformStep()
            str = "update cs_driverinf set bezone='" & ComboBox1.Text.Trim & "' where lineid='" & CurLineName & "' and rdriverno='" & id & "'"
            UpdateData(str)
            System.Threading.Thread.Sleep(50)
        Next
        ifRefresh = True
        MsgBox("设置完毕！")
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox1.Enabled = True
            CheckBox2.Checked = False
        Else
            TextBox1.Enabled = False
            CheckBox2.Checked = True
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If CheckBox1.Checked = False And CheckBox2.Checked = False Then
            MsgBox("未选择设置条件！")
            Exit Sub
        End If
        If CheckBox1.Checked = True Then
            If TextBox1.Text.Trim = "" Then
                MsgBox("请输入数字")
                Exit Sub
            Else
                Try
                    Integer.Parse(TextBox1.Text.Trim)
                Catch ex As Exception
                    MsgBox("请输入数字")
                    Exit Sub
                End Try
            End If
            Dim str As String = ""
            ProgressBar1.Maximum = selDriver.Count
            ProgressBar1.Value = 1
            For Each id As String In selDriver.Keys
                ProgressBar1.PerformStep()
                str = "update cs_driverinf set beteam='" & TextBox1.Text.Trim & "' where lineid='" & CurLineName & "' and rdriverno='" & id & "'"
                UpdateData(str)
                System.Threading.Thread.Sleep(50)
            Next
        End If
        If CheckBox2.Checked = True Then
            Dim beteamNo As Integer = 0
            If TextBox2.Text.Trim = "" Then
                MsgBox("请输入数字")
                Exit Sub
            Else
                Try
                    beteamNo = Integer.Parse(TextBox2.Text.Trim)
                Catch ex As Exception
                    MsgBox("请输入数字")
                    Exit Sub
                End Try
            End If
            Dim str As String = ""
            ProgressBar1.Maximum = selDriver.Count
            ProgressBar1.Value = 1

            For Each id As String In selDriver.Keys
                ProgressBar1.PerformStep()
                str = "update cs_driverinf set beteam='" & beteamNo.ToString & "' where lineid='" & CurLineName & "' and rdriverno='" & id & "'"
                UpdateData(str)
                beteamNo += 1
                System.Threading.Thread.Sleep(50)
            Next
        End If
        ifRefresh = True
        MsgBox("设置完毕！")
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            TextBox2.Enabled = True
            CheckBox1.Checked = False
        Else
            TextBox2.Enabled = False
            CheckBox1.Checked = True
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ComboBox2.Text.Trim = "" Then
            MsgBox("请选择需要修改的班组")
            Exit Sub
        End If
        Dim str As String = ""
        ProgressBar1.Maximum = selDriver.Count
        ProgressBar1.Value = 1
        For Each id As String In selDriver.Keys
            ProgressBar1.PerformStep()
            str = "update cs_driverinf set beclass='" & ComboBox2.Text.Trim & "' where lineid='" & CurLineName & "' and rdriverno='" & id & "'"
            UpdateData(str)
            System.Threading.Thread.Sleep(50)
        Next
        ifRefresh = True
        MsgBox("设置完毕！")
    End Sub
End Class