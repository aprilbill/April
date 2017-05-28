Imports System.ComponentModel
Imports Microsoft.Office.Interop.Excel
Public Class Report
    Private _name As String
    Private _type As String
    Public Titlefont As System.Drawing.Font
    Public Notefont As System.Drawing.Font
    Public Datafont As System.Drawing.Font
    Public DataGrid As CellGrid

    Public Sub New()
        Titlefont = New System.Drawing.Font("黑体", 16)
        Notefont = New System.Drawing.Font("宋体", 9)
        Datafont = New System.Drawing.Font("宋体", 10)
    End Sub
    <DisplayName("报表名称"), Category("名称"), Description("报表名称")> _
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            If value <> _name Then
                _name = value
            End If
        End Set
    End Property

    <DisplayName("统计类型"), Category("统计"), Description("选择统计类型")> _
    Public ReadOnly Property Type() As StatisticType
        Get
            Return StatisticType.计划绩效
        End Get
    End Property

    Public Overridable Sub LoadData()

    End Sub

    Public Overridable Sub DrawFrame()

    End Sub

    Public Overridable Sub DrawData()

    End Sub
End Class

Public Enum StatisticType
    计划绩效 = 1
    实际绩效 = 2
End Enum
