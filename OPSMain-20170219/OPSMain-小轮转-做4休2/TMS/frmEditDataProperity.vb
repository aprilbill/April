Public Class frmEditDataProperity
    Inherits System.Windows.Forms.Form
    Public blnOK As Boolean
    Friend WithEvents dataGrid As System.Windows.Forms.DataGridView
    Friend WithEvents btnColor As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    '是否按确定按钮
    Dim nCurRow As Integer
    Dim CurCellX As Single
    Dim CurCellY As Single
    Dim CurMouseX As Single
    Friend WithEvents a As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents b As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents labInfor As System.Windows.Forms.Label
    Dim CurMouseY As Single

#Region " Windows 窗体设计器生成的代码 "

    Public Sub New()
        MyBase.New()

        '该调用是 Windows 窗体设计器所必需的。
        InitializeComponent()

        '在 InitializeComponent() 调用之后添加任何初始化

    End Sub

    '窗体重写 dispose 以清理组件列表。
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改此过程。
    '不要使用代码编辑器修改它。
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents cmbEdit As System.Windows.Forms.ComboBox
    Friend WithEvents txtEdit As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditDataProperity))
        Me.btnOk = New System.Windows.Forms.Button
        Me.cmbEdit = New System.Windows.Forms.ComboBox
        Me.txtEdit = New System.Windows.Forms.TextBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOpen = New System.Windows.Forms.Button
        Me.dataGrid = New System.Windows.Forms.DataGridView
        Me.btnColor = New System.Windows.Forms.Button
        Me.a = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.b = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.labInfor = New System.Windows.Forms.Label
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOk
        '
        Me.btnOk.Location = New System.Drawing.Point(138, 422)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(72, 24)
        Me.btnOk.TabIndex = 66
        Me.btnOk.Text = "确定(&Y)"
        '
        'cmbEdit
        '
        Me.cmbEdit.FormattingEnabled = True
        Me.cmbEdit.Location = New System.Drawing.Point(32, 262)
        Me.cmbEdit.Name = "cmbEdit"
        Me.cmbEdit.Size = New System.Drawing.Size(139, 20)
        Me.cmbEdit.TabIndex = 65
        Me.cmbEdit.Text = "ComboBox1"
        Me.cmbEdit.Visible = False
        '
        'txtEdit
        '
        Me.txtEdit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtEdit.Location = New System.Drawing.Point(32, 303)
        Me.txtEdit.Multiline = True
        Me.txtEdit.Name = "txtEdit"
        Me.txtEdit.Size = New System.Drawing.Size(139, 21)
        Me.txtEdit.TabIndex = 64
        Me.txtEdit.Text = "TextBox1"
        Me.txtEdit.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(226, 422)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 24)
        Me.btnCancel.TabIndex = 67
        Me.btnCancel.Text = "取消(&C)"
        '
        'btnOpen
        '
        Me.btnOpen.Location = New System.Drawing.Point(27, 422)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(26, 16)
        Me.btnOpen.TabIndex = 68
        Me.btnOpen.Text = "..."
        Me.btnOpen.Visible = False
        '
        'dataGrid
        '
        Me.dataGrid.AllowUserToAddRows = False
        Me.dataGrid.AllowUserToDeleteRows = False
        Me.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.dataGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.a, Me.b})
        Me.dataGrid.Location = New System.Drawing.Point(10, 13)
        Me.dataGrid.Name = "dataGrid"
        Me.dataGrid.RowHeadersWidth = 30
        Me.dataGrid.RowTemplate.Height = 23
        Me.dataGrid.Size = New System.Drawing.Size(284, 367)
        Me.dataGrid.TabIndex = 69
        '
        'btnColor
        '
        Me.btnColor.Location = New System.Drawing.Point(75, 422)
        Me.btnColor.Name = "btnColor"
        Me.btnColor.Size = New System.Drawing.Size(26, 16)
        Me.btnColor.TabIndex = 70
        Me.btnColor.Text = "..."
        Me.btnColor.Visible = False
        '
        'a
        '
        Me.a.Frozen = True
        Me.a.HeaderText = "项目名"
        Me.a.Name = "a"
        Me.a.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'b
        '
        Me.b.HeaderText = "属性值"
        Me.b.Name = "b"
        Me.b.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.b.Width = 140
        '
        'labInfor
        '
        Me.labInfor.AutoSize = True
        Me.labInfor.ForeColor = System.Drawing.Color.Red
        Me.labInfor.Location = New System.Drawing.Point(12, 393)
        Me.labInfor.Name = "labInfor"
        Me.labInfor.Size = New System.Drawing.Size(125, 12)
        Me.labInfor.TabIndex = 71
        Me.labInfor.Text = "请输入或选择相关属性"
        '
        'frmEditDataProperity
        '
        Me.ClientSize = New System.Drawing.Size(304, 449)
        Me.Controls.Add(Me.labInfor)
        Me.Controls.Add(Me.cmbEdit)
        Me.Controls.Add(Me.txtEdit)
        Me.Controls.Add(Me.btnColor)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.dataGrid)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmEditDataProperity"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "数据输入"
        CType(Me.dataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        Dim Txt As String
        If Me.cmbEdit.Visible = True Then
            Txt = Me.cmbEdit.Text
            Me.dataGrid.Rows(Me.dataGrid.CurrentCell.RowIndex).Cells(Me.dataGrid.CurrentCell.ColumnIndex).Value = Txt
            Me.cmbEdit.Visible = False
        End If
        Dim i As Integer

        '先检查是否符合规范
        For i = 1 To Me.dataGrid.Rows.Count
            If stuListItem(i).strItemCriterion = TextCriterion.NotEmpty Then
                If Trim(Me.dataGrid.Rows(i - 1).Cells(1).Value) = "" Then
                    MsgBox(Me.dataGrid.Rows(i - 1).Cells(0).Value & " 不能为空,请重新输入!", , "提示")
                    Exit Sub
                End If
            ElseIf stuListItem(i).strItemCriterion = TextCriterion.OnlyNumber Then

                Txt = Me.dataGrid.Rows(i - 1).Cells(1).Value
                If Microsoft.VisualBasic.IsNumeric(Txt) = False Then
                    MsgBox(Me.dataGrid.Rows(i - 1).Cells(1).Value & " 输入错误，请输入数字！")
                    Exit Sub
                End If
            End If
        Next

        For i = 1 To Me.dataGrid.RowCount
            If Me.dataGrid.Rows(i - 1).Cells(1).Value Is Nothing Then
                stuListItem(i).strReturnValue = ""
            Else
                If Me.dataGrid.Rows(i - 1).Cells(1).Value.ToString.Trim <> "" Then
                    stuListItem(i).strReturnValue = Trim(Me.dataGrid.Rows(i - 1).Cells(1).Value)
                Else

                End If
            End If

        Next

        blnOK = True
        Me.Close()

        'lsvEdit　代码
        'Call lsvEdit_SelectedIndexChanged(sender, e)
        'Dim i As Integer

        ''先检查是否符合规范
        'For i = 1 To Me.lsvEdit.Items.Count
        '    If stuListItem(i).strItemCriterion = TextCriterion.NotEmpty Then
        '        If Trim(Me.lsvEdit.Items(i - 1).SubItems(1).Text) = "" Then
        '            MsgBox(Me.lsvEdit.Items(i - 1).SubItems(0).Text & " 不能为空,请重新输入!","提示")
        '            Call SetListText(i - 1, i)
        '            Exit Sub
        '        End If
        '    ElseIf stuListItem(i).strItemCriterion = TextCriterion.OnlyNumber Then
        '        Dim Txt As String
        '        Txt = Me.lsvEdit.Items(i - 1).SubItems(1).Text
        '        If Microsoft.VisualBasic.IsNumeric(Txt) = False Then
        '            MsgBox(Me.lsvEdit.Items(i - 1).SubItems(0).Text & " 输入错误，请输入数字！")
        '            Call SetListText(i - 1, i)
        '            Exit Sub
        '        End If
        '    End If
        'Next

        'For i = 1 To Me.lsvEdit.Items.Count
        '    stuListItem(i).strReturnValue = Trim(Me.lsvEdit.Items(i - 1).SubItems(1).Text)
        'Next

        'blnOK = True
        'Me.Dispose()
    End Sub

    Private Sub frmEditDataProperity_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim i As Integer
        Me.dataGrid.RowCount = UBound(stuListItem)
        For i = 1 To Me.dataGrid.RowCount
            Me.dataGrid.Rows(i - 1).Height = 20
            Me.dataGrid.Rows(i - 1).Cells(0).Value = stuListItem(i).strItem
            Me.dataGrid.Rows(i - 1).Cells(1).Value = stuListItem(i).strTxtList
        Next i
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        blnOK = False
        'Me.Visible = False
        Me.Close()
    End Sub

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        'Dim opFile As New OpenFileDialog
        'Dim nLast As Integer
        'Dim strName As String
        'Dim StrHouZui As String
        'Dim Source As String
        'Dim Destination As String
        'Dim staName As String
        'staName = 'Trim(Me.lsvEdit.Items(0).SubItems(1).Text)
        'opFile.Filter = "All img files (*.*)|*.*|Bmp Files(*.bmp)|*.bmp|Gif Files(*.gif)|*.gif|Jpg Files(*.jpg)|*.jpg|Jpeg Files(*.jpeg)|*.jpeg"

        'opFile.ShowDialog()
        'If opFile.FileName <> "" Then
        '    nLast = InStrRev(opFile.FileName, ".", , CompareMethod.Text) '右边数过来第几个值
        '    StrHouZui = Microsoft.VisualBasic.Right(opFile.FileName, Len(opFile.FileName) - nLast + 1)
        '    strName = g_LineName & "_" & staName & StrHouZui
        '    Source = opFile.FileName
        '    Destination = Application.StartupPath & "\StaPic\" & strName
        '    System.IO.File.Copy(Source, Destination, True)
        '    Me.txtEdit.Text = strName
        'End If
    End Sub

    Private Sub dataGrid_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dataGrid.CellClick
        Dim a, b As Integer
        Dim left, top, width, height As Single
        Dim txtValue As String
        a = Me.dataGrid.CurrentCell.RowIndex
        b = Me.dataGrid.CurrentCell.ColumnIndex
        nCurRow = a
        If b = 1 Then
            left = CurMouseX - CurCellX + Me.dataGrid.Left ' Me.dataGrid.Left + Me.dataGrid.Rows(a).Cells(0).Size.Width + Me.dataGrid.Rows(a).HeaderCell.Size.Width
            top = CurMouseY - CurCellY + Me.dataGrid.Top ' Me.dataGrid.Top + (a + 1) * 20 + 3
            width = Me.dataGrid.CurrentCell.Size.Width
            height = Me.dataGrid.CurrentCell.Size.Height
            txtValue = Me.dataGrid.CurrentCell.Value
            Call SetDataGridListText(a, a + 1, left, top, width, height, txtValue)
        End If
    End Sub

    Private Sub dataGrid_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles dataGrid.CellMouseDown
        CurCellX = e.X
        CurCellY = e.Y
    End Sub

    '显示text or comb 框
    Private Sub SetDataGridListText(ByVal nCurRow As Integer, ByVal nWei As Single, ByVal Left As Single, ByVal Top As Single, ByVal Width As Single, ByVal Height As Single, ByVal txtValue As String)
        Dim i As Integer
        If stuListItem(nWei).strStyle = PropStrStyle.TexBox Then
            Me.cmbEdit.Visible = False
            Me.btnColor.Visible = False
            Me.btnOpen.Visible = False
            'Me.txtEdit.Visible = True
            'Me.txtEdit.Text = txtValue
            'Me.txtEdit.Height = 14
            'Me.txtEdit.Left = Left
            'Me.txtEdit.Width = Width
            'Me.txtEdit.Top = Top
            'Me.txtEdit.Height = Height
            'Me.txtEdit.Focus()
        ElseIf stuListItem(nWei).strStyle = PropStrStyle.ComBox Then
            Me.cmbEdit.Items.Clear()
            For i = 1 To UBound(stuListItem(nWei).StrCmbList)
                Me.cmbEdit.Items.Add(stuListItem(nWei).StrCmbList(i))
            Next
            Me.cmbEdit.Text = txtValue
            Me.cmbEdit.Left = Left
            Me.cmbEdit.Width = Width
            Me.cmbEdit.Top = Top
            Me.cmbEdit.Focus()
            Me.cmbEdit.Visible = True
        ElseIf stuListItem(nWei).strStyle = PropStrStyle.SelectBox Then
            'Me.txtEdit.Visible = True
            'Me.txtEdit.Text = txtValue
            'Me.txtEdit.Height = 14
            'Me.txtEdit.Left = Left
            'Me.txtEdit.Width = Width
            'Me.txtEdit.Top = Top
            'Me.txtEdit.Focus()
            Me.btnOpen.Left = Left + Width - Me.btnOpen.Width
            Me.btnOpen.Top = Top
            Me.btnOpen.Height = Height
            Me.btnOpen.Visible = True
        ElseIf stuListItem(nWei).strStyle = PropStrStyle.colorBox Then
            Me.btnColor.Left = Left + Width - Me.btnColor.Width
            Me.btnColor.Top = Top
            Me.btnColor.Height = Height
            Me.btnColor.Visible = True
        End If
    End Sub


    Private Sub dataGrid_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles dataGrid.MouseDown
        CurMouseX = e.X
        CurMouseY = e.Y
        Dim Txt As String
        If Me.txtEdit.Visible = True Then
            Txt = Me.txtEdit.Text
            Me.dataGrid.Rows(nCurRow).Cells(1).Value = Txt
            Me.txtEdit.Visible = False
            Me.btnOpen.Visible = False
        ElseIf Me.cmbEdit.Visible = True Then
            Txt = Me.cmbEdit.Text
            Me.dataGrid.Rows(nCurRow).Cells(1).Value = Txt
            Me.cmbEdit.Visible = False
        ElseIf Me.btnOpen.Visible = True Then
            Me.btnOpen.Visible = False
        End If
    End Sub

    Private Sub btnColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColor.Click
        Dim Cdlg As New ColorDialog
        If Cdlg.ShowDialog() <> Windows.Forms.DialogResult.Cancel Then
            Me.dataGrid.CurrentCell.Style.BackColor = Cdlg.Color
            Me.dataGrid.CurrentCell.Value = Cdlg.Color.ToArgb
        End If
    End Sub
End Class

