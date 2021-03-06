Partial Public Class frmListTimeTable
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnBegin = New System.Windows.Forms.Button
        Me.cmbCheDiNum = New System.Windows.Forms.ComboBox
        Me.cmdOutToEXCEL = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.dgdShowData = New System.Windows.Forms.DataGridView
        Me.txtTitle = New System.Windows.Forms.TextBox
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgdShowData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnBegin
        '
        Me.btnBegin.Location = New System.Drawing.Point(30, 76)
        Me.btnBegin.Name = "btnBegin"
        Me.btnBegin.Size = New System.Drawing.Size(84, 23)
        Me.btnBegin.TabIndex = 5
        Me.btnBegin.Text = "开始查询(&B)"
        '
        'cmbCheDiNum
        '
        Me.cmbCheDiNum.FormattingEnabled = True
        Me.cmbCheDiNum.Location = New System.Drawing.Point(30, 36)
        Me.cmbCheDiNum.Name = "cmbCheDiNum"
        Me.cmbCheDiNum.Size = New System.Drawing.Size(84, 20)
        Me.cmbCheDiNum.Sorted = True
        Me.cmbCheDiNum.TabIndex = 9
        '
        'cmdOutToEXCEL
        '
        Me.cmdOutToEXCEL.Location = New System.Drawing.Point(17, 344)
        Me.cmdOutToEXCEL.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
        Me.cmdOutToEXCEL.Name = "cmdOutToEXCEL"
        Me.cmdOutToEXCEL.Size = New System.Drawing.Size(138, 23)
        Me.cmdOutToEXCEL.TabIndex = 12
        Me.cmdOutToEXCEL.Text = "保存为EXCEL文件(&O)"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 13)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "按车底编号查询:"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(17, 374)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(3, 3, 3, 2)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(138, 23)
        Me.btnExit.TabIndex = 16
        Me.btnExit.Text = "退出(&E)"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnExit)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cmdOutToEXCEL)
        Me.Panel1.Controls.Add(Me.cmbCheDiNum)
        Me.Panel1.Controls.Add(Me.btnBegin)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 1, 3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(177, 408)
        Me.Panel1.TabIndex = 18
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.dgdShowData)
        Me.Panel2.Controls.Add(Me.txtTitle)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(177, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(368, 408)
        Me.Panel2.TabIndex = 20
        '
        'dgdShowData
        '
        Me.dgdShowData.AccessibleName = ""
        Me.dgdShowData.AllowUserToAddRows = False
        Me.dgdShowData.AllowUserToDeleteRows = False
        Me.dgdShowData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgdShowData.Location = New System.Drawing.Point(0, 19)
        Me.dgdShowData.Name = "dgdShowData"
        Me.dgdShowData.ReadOnly = True
        Me.dgdShowData.RowTemplate.Height = 23
        Me.dgdShowData.Size = New System.Drawing.Size(368, 389)
        Me.dgdShowData.TabIndex = 21
        '
        'txtTitle
        '
        Me.txtTitle.AutoSize = False
        Me.txtTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtTitle.Font = New System.Drawing.Font("SimSun", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.txtTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtTitle.Location = New System.Drawing.Point(0, 0)
        Me.txtTitle.Multiline = True
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(368, 19)
        Me.txtTitle.TabIndex = 20
        Me.txtTitle.Text = "列车时刻表"
        Me.txtTitle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'frmListTimeTable
        '
        'Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(545, 408)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmListTimeTable"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "时刻表显示"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.dgdShowData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnBegin As System.Windows.Forms.Button
    Friend WithEvents cmbCheDiNum As System.Windows.Forms.ComboBox
    Friend WithEvents cmdOutToEXCEL As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents dgdShowData As System.Windows.Forms.DataGridView
End Class
