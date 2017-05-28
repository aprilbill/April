Imports System.Management
Imports System.Security
Imports System.Security.Cryptography
Imports System.Text

Module modCopyRight
    Public Function IsRightCopy() As Integer
        IsRightCopy = 0
        Dim strKey As String
        '授权
        Dim sHardSeries As String
        Dim sCode As String
        Dim sRightCode As String
        Dim cmicWmi As New System.Management.ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive")
        Dim Uint32 As UInt32
        For Each cmicWmiObj As ManagementObject In cmicWmi.Get
            Uint32 = cmicWmiObj("signature")
            Exit For
        Next

        'Dim sCPUcode As String
        'Dim managementClass As New ManagementClass("Win32_Processor")
        'Dim managementObjectCollection As ManagementObjectCollection = managementClass.GetInstances '加入了一个实例
        'Dim managementObject As ManagementObject
        'For Each managementObject In managementObjectCollection '对加入的实例做引用
        '    sCPUcode = managementObject.Properties("ProcessorId").Value.ToString()
        '    Exit For
        'Next

        sHardSeries = Uint32.ToString
        strKey = "tpmmetro"
        Dim strPath As String
        strPath = Application.StartupPath & "\license.sky"
        If IO.File.Exists(strPath) = False Then
            'MsgBox("对不起，该软件授权文件不存在，请向开发人员索取正版授权文件！")
            IsRightCopy = 1
            'End
        Else
            sCode = My.Computer.FileSystem.ReadAllText(Application.StartupPath & "\license.sky")
            sRightCode = Encrypt(sHardSeries, strKey)
            If sRightCode = sCode Then

            Else
                'MsgBox("对不起，你的授权文件不正确，请向开发人员索取正版授权文件！")
                IsRightCopy = 2
            End If
        End If
    End Function

    Public Function Encrypt(ByVal pToEncrypt As String, ByVal sKey As String) As String
        Dim des As New DESCryptoServiceProvider()
        Dim inputByteArray() As Byte
        inputByteArray = Encoding.Default.GetBytes(pToEncrypt)
        '建立加密对象的密钥和偏移量
        '原文使用ASCIIEncoding.ASCII方法的GetBytes方法
        '使得输入密码必须输入英文文本
        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey)
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey)
        '写二进制数组到加密流
        '(把内存流中的内容全部写入)

        Dim ms As New System.IO.MemoryStream()
        Dim cs As New CryptoStream(ms, des.CreateEncryptor, CryptoStreamMode.Write)
        '写二进制数组到加密流

        '(把内存流中的内容全部写入)
        cs.Write(inputByteArray, 0, inputByteArray.Length)
        cs.FlushFinalBlock()
        '建立输出字符串     

        Dim ret As New StringBuilder()
        Dim b As Byte
        For Each b In ms.ToArray()
            ret.AppendFormat("{0:X2}", b)
        Next
        Return ret.ToString()
    End Function

    '解密方法

    Public Function Decrypt(ByVal pToDecrypt As String, ByVal sKey As String) As String
        Dim des As New DESCryptoServiceProvider()
        '把字符串放入byte数组
        Dim len As Integer
        len = pToDecrypt.Length / 2 - 1
        Dim inputByteArray(len) As Byte
        Dim x, i As Integer
        For x = 0 To len
            i = Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16)
            inputByteArray(x) = CType(i, Byte)
        Next
        '建立加密对象的密钥和偏移量，此值重要，不能修改
        des.Key = ASCIIEncoding.ASCII.GetBytes(sKey)
        des.IV = ASCIIEncoding.ASCII.GetBytes(sKey)
        Dim ms As New System.IO.MemoryStream()
        Dim cs As New CryptoStream(ms, des.CreateDecryptor, CryptoStreamMode.Write)
        cs.Write(inputByteArray, 0, inputByteArray.Length)
        cs.FlushFinalBlock()
        Return Encoding.Default.GetString(ms.ToArray)
    End Function
End Module
