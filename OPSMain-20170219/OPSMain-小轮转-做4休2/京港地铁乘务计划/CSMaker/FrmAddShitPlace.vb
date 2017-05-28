Public Class FrmAddShitPlace

    Dim curJiaolu As New List(Of String)
    Dim flag As Boolean = False
    Public Sub New(ByVal station As List(Of String), ByVal jiaolu As List(Of String))

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        curJiaolu = jiaolu
        For i As Integer = 0 To jiaolu.Count - 1
            ComboBox1.Items.Add(jiaolu(i))
            ComboBox5.Items.Add(jiaolu(i))
        Next
        For i As Integer = 0 To station.Count - 1
            ComboBox2.Items.Add(station(i))
            ComboBox4.Items.Add(station(i))
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox5.Text.Trim <> "" And ComboBox4.Text.Trim <> "" Then
            Dim tuiqin As String = ComboBox5.Text.Trim & ":" & ComboBox4.Text.Trim
            If ListBox1.Items.Contains(tuiqin) = False Then
                ListBox1.Items.Add(tuiqin)
            End If
        End If
       
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ListBox1.Items.Count = 0 Or ComboBox1.Text.Trim = "" Or ComboBox2.Text.Trim = "" Or ComboBox3.Text = "" Then
            MsgBox("请填写完整参数")
            Exit Sub
        End If
        flag = True
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub


    Private Sub ListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseDoubleClick
        If ListBox1.SelectedIndex <> -1 Then
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        End If
    End Sub
End Class