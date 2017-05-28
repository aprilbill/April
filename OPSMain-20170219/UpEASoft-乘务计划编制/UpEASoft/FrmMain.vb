Imports System.Net
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip

Public Class FrmMain
    Dim AppPath As String = My.Application.Info.DirectoryPath & "\"
    Dim Url As String = ""
    Dim TempPath As String = ""
    Public sCurLineName As String = ""
    Public DM As New WcfService.SkyDataServiceClient

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            BackgroundWorker1.CancelAsync()
            BackgroundWorker1.Dispose()
        Catch ex As Exception

        End Try

        Me.Close()
    End Sub

    Public Function DownLoad(ByVal filenameCode As String, ByVal proBar As ProgressBar) As Boolean           '下载文件
        Dim savaPath As String = sCurLineName & "\"
        Dim fileName As String = "乘务计划编制系统.zip"
        Dim nLength As Long
        nLength = DM.DownLoadFileLength(savaPath, fileName)

        Dim bufferLen As Integer = 1024 * 1024
        Dim nSendNumber As Integer = Math.Ceiling(nLength / bufferLen)
        'proBar.Value = 0
        'proBar.Maximum = nSendNumber
        Dim fileSize As String
        fileSize = FormatNumber(nLength / (1024 * 1024), 2).ToString
        'Me.Label1.Text = "正在上传文件,大小为" & fileSize & " M" ' & FormatNumber((request.nState + 1) * bufferLen / (1024 * 1024), 2).ToString & "/" & FormatNumber(Stream.Length / (1024 * 1024), 2).ToString
        Dim i As Integer
        Dim Stream As Stream = Nothing
        Dim buffer() As Byte
        ReDim buffer(bufferLen - 1)
        Dim targetStream As FileStream = Nothing

        Dim SaveFolder As String = AppPath & "UpdateFile\"
        If Not Directory.Exists(SaveFolder) Then
            Directory.CreateDirectory(SaveFolder)
        End If
        Dim filePath As String
        filePath = Path.Combine(SaveFolder, filenameCode)
        Dim tmpNum As Long = 0
        Dim nSendLenth As Long = 0
        Dim nFirID As Long = 0
        proBar.Value = 0
        proBar.Maximum = nSendNumber
        For i = 1 To nSendNumber
            If tmpNum + bufferLen > nLength Then
                nSendLenth = nLength - tmpNum
            Else
                nSendLenth = bufferLen
            End If
            'nFirID = nFirID + tmpNum
            Try
                'Dim Channel As WcfService.ISkyDataService = DM.ChannelFactory.CreateChannel()
                Stream = DM.DownLoadLargeFile(savaPath, fileName, tmpNum, nSendLenth)
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try

            'Stream = MyWcfDataService.DownLoadLargeFile(savaPath, fileName, (i - 1) * bufferLen, nSendLenth)
            If i = 1 Then
                targetStream = New FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None)
            Else
                targetStream = New FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None)
            End If

            Dim buffer2() As Byte
            ReDim buffer2(bufferLen - 1)
            Dim Count As Integer = 0
            Do
                Count = Stream.Read(buffer2, 0, bufferLen)
                If Count > 0 Then
                    targetStream.Write(buffer2, 0, Count)
                Else
                    Exit Do
                End If
            Loop
            targetStream.Dispose()
            tmpNum = tmpNum + nSendLenth
            proBar.Value = i
            Application.DoEvents()
        Next
        Stream.Close()
        Return True
    End Function

    ''' <summary>
    ''' 文件存在校验
    ''' </summary>
    Public Function FileTrue(ByVal str As String) As Boolean '文件存在检察
        Try
            If IO.File.Exists(str) = True Then
                Dim FileInfo As IO.FileInfo = New IO.FileInfo(str)
                If FileInfo.Length <= 0 Then
                    FileTrue = False
                Else
                    FileTrue = True
                End If
                FileInfo = Nothing
            Else
                FileTrue = False
            End If
        Catch ex As ApplicationException
            FileTrue = False
        End Try
        GC.Collect()
    End Function

    Sub RunThread()
        Try
            BackgroundWorker1.CancelAsync()
            BackgroundWorker1.RunWorkerAsync()
            Button1.Enabled = False
        Catch ex As Exception
            BackgroundWorker1.CancelAsync()
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "更新下载" Then
            Dim downloadName As String = ""
            If DM.IfFileExist(sCurLineName & "\", "乘务计划编制系统.zip") Then
                downloadName = "乘务计划编制系统-" + Now.Year.ToString("0000") + Now.Month.ToString("00") + Now.Day.ToString("00") + ".zip"
                DownLoad(downloadName, ProgressBar1)
                Label1.Text = "已下载到" & AppPath & "\UpdateFile" & ",文件名:" + vbCrLf + downloadName
                BackgroundWorker1.WorkerSupportsCancellation = True
                TempPath = AppPath & "UpdateFile\"
                Dim FileProperties(1) As String
                FileProperties(0) = TempPath & downloadName
                FileProperties(1) = TempPath & "Tmp\"
                If Directory.Exists(FileProperties(1)) Then
                    Directory.Delete(FileProperties(1), True)
                End If
                UnZip(FileProperties)
                Button1.Text = "下一步"
                Try
                    File.Delete(TempPath & downloadName)
                    ListBox1.Items.Clear()
                    GetAllFiles(TempPath & "Tmp\乘务计划编制系统\")
                    Label1.Text = "点击下一步"
                Catch ex As Exception

                End Try
            Else
                Label1.Text = "无更新程序"
            End If
        Else
            Label1.Text = "正在更新中，请稍后"
            RunThread()
            Button1.Text = "更新下载"
        End If

    End Sub
    Private Sub GetAllFiles(ByVal strDirect As String)  '搜索所有目录下的文件  

        If Not (strDirect Is Nothing) Then
            Dim mFileInfo As System.IO.FileInfo
            Dim mDir As System.IO.DirectoryInfo
            Dim mDirInfo As New System.IO.DirectoryInfo(strDirect)
            Try
                For Each mFileInfo In mDirInfo.GetFiles()
                    If mFileInfo.ToString.Contains("UpEASoft") = False And mFileInfo.ToString.Contains("ICSharpCode.SharpZipLib.dll") = False And mFileInfo.ToString.Contains("CPM") = False And mFileInfo.ToString.Contains(".accdb") = False Then
                        ListBox1.Items.Add(mFileInfo.FullName.ToString)
                    End If
                Next
                For Each mDir In mDirInfo.GetDirectories
                    GetAllFiles(mDir.FullName)
                Next
            Catch ex As System.IO.DirectoryNotFoundException

            End Try

        End If
    End Sub
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Invoke(New EventHandler(AddressOf UpdateComplete))  'Invoke保证线程安全
        GC.Collect()
        System.Diagnostics.Process.GetCurrentProcess.MinWorkingSet = New System.IntPtr(5)
    End Sub


    Sub UpdateComplete(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Cursor = Cursors.Default
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = ListBox1.Items.Count
        For i = 0 To ListBox1.Items.Count - 1
            Label1.Text = ListBox1.Items(i).ToString
            ListBox1.SelectedIndex = i
            ProgressBar1.Value += 1
            Dim success As Boolean = False
            Dim times As Integer = 0
            While success = False
                Try
                    Dim objPath As String = ListBox1.Items(i).ToString.Replace(TempPath & "Tmp\乘务计划编制系统\", AppPath)
                    File.Delete(objPath)
                    File.Copy(ListBox1.Items(i).ToString, objPath, True)
                    success = True
                Catch ex As Exception
                    success = False
                    times += 1
                    If times > 3 Then
                        Exit While
                    End If
                End Try

            End While

        Next
        Try
            BackgroundWorker1.CancelAsync()
            BackgroundWorker1.Dispose()
        Catch ex As Exception

        End Try
        Me.Close()
    End Sub
    ''' <summary>
    ''' 解压文件
    ''' </summary>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Public Sub UnZip(ByVal args() As String)
        Dim s As New ZipInputStream(File.OpenRead(args(0)))
        Label1.Text = "正在解压，请稍后。。。"
        Application.DoEvents()
        Dim theEntry As ZipEntry = s.GetNextEntry
        While (theEntry IsNot Nothing)
            Dim directoryName As String = Path.GetDirectoryName(theEntry.Name)
            Dim fileName As String = Path.GetFileName(theEntry.Name)
            If directoryName.Length > 0 Then
                Directory.CreateDirectory(args(1) + directoryName)

            End If
            If directoryName.EndsWith("/") = False Then
                directoryName += "/"
            End If
            If fileName <> String.Empty Then
                Dim streamWriter As FileStream = File.Create(args(1) + theEntry.Name)
                Dim size As Integer = 2048
                Dim data(size - 1) As Byte
                While True
                    size = s.Read(data, 0, data.Length)
                    If size > 0 Then
                        streamWriter.Write(data, 0, size)
                    Else
                        Exit While
                    End If
                End While
                streamWriter.Close()
            End If
            theEntry = s.GetNextEntry
        End While
        s.Dispose()

    End Sub

  
    Private Sub FrmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim treader As New StreamReader(System.Windows.Forms.Application.StartupPath & "\Config\LineInf.dat", System.Text.Encoding.Default)
        If treader.EndOfStream = False Then
            sCurLineName = treader.ReadLine
        End If
        treader.Close()
    End Sub
End Class
