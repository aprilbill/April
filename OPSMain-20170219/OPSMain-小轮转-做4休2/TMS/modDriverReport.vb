'Imports CrystalDecisions.CrystalReports.Engine
'Public Class frmDriverReport

'    Private Sub frmDriverReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        Dim newReport As New ReportDocument
'        Dim sPath As String
'        'Dim Appl As New ReportDocument.Application

'        'Dim cnn As New ADODB.Connection
'        'Dim rst As New ADODB.Recordset

'        'newReport = Appl.OpenReport(App.Path & "\rpt_FB0020.rpt") 'Open the Events.rpt report file.
'        'With cnn
'        '    .Provider = "Microsoft.Jet.OLEDB.4.0"
'        '    .Open("D:\work\医師\source\wmdb\KAIINMDB.mdb")
'        'End With
'        'Call rst.Open("FB0020", cnn, adOpenDynamic, adLockReadOnly, adCmdTable)
'        'CrReport.ParameterFields(1).AddCurrentValue("abd")
'        'CrReport.Database.SetDataSource(rst)
'        'CRViewer91.ReportSource = CrReport 'Sets the Report source of the CrViewer to the Report object we created.


'        ' ''CRViewer91.ViewReport() 'View the Report.

'        ''Dim strPath As String
'        ''strPath = Trim(Application.StartupPath) & "\上海地铁网络.mdb"
'        ''Dim strCon As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" & strPath & "'"
'        ''Dim MyConn As Data.OleDb.OleDbConnection = New Data.OleDb.OleDbConnection(strCon)
'        ' ''MyConn.ConnectionString = strCon
'        ''Dim strTable As String = "select  * from 票价表 "

'        ''Dim Mydc As New Data.OleDb.OleDbDataAdapter(strTable, strCon)
'        ' ''创建一个Datasetd
'        ''Dim myDataSet As System.Data.DataSet = New System.Data.DataSet

'        ''Mydc.Fill(myDataSet, "票价表")
'        ''Dim myTable As Data.DataTable

'        ''myTable = myDataSet.Tables("票价表")
'        ' ''Me.DataGrid1.DataSource = myTable
'        ' ''Me.DataGrid1.DataBindings()

'        sPath = Trim(Application.StartupPath) & "\driver.rpt"
'        newReport.Load(sPath)
'        'newReport.SetDataSource(myTable)
'        ' newReport.Refresh()

'        Me.CrystalReportViewer1.ReportSource = newReport
'    End Sub
'End Class