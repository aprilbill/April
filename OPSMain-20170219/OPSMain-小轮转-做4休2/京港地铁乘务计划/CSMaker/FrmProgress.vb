Public Class FrmProgress
    Private Sub FrmProgress_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Application.DoEvents()
    End Sub

    Public Sub New(ByVal Maxsize As Integer, ByVal Text As String)

        ' 此调用是 Windows 窗体设计器所必需的。
        InitializeComponent()
        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Me.ProgressBar1.Maximum = Maxsize
        Me.Text = Text
        Me.ProgressBar1.Step = 1
        Me.Show()
    End Sub

    Public Sub Performstep()
        Me.ProgressBar1.Value += 1
        Application.DoEvents()
    End Sub

    Public Sub EndProgress()
        Application.DoEvents()
        Me.Dispose()
    End Sub

   
End Class