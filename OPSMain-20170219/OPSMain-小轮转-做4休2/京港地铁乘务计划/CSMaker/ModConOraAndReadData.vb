Module ModConOraAndReadData
    '读入乘务时刻表信息
    Public Sub InputCSTimetableInf(ByVal LineID As String)

        tempTable = New Data.DataTable
        tempTable = ReadData("SELECT * FROM CS_CSTIMETABLEINF WHERE LINEID='" & CStr(LineID) & "'")

        Dim i As Integer
        ReDim CSTimetableInf(tempTable.Rows.Count)
        For i = 0 To tempTable.Rows.Count - 1
            CSTimetableInf(i + 1).nID = i + 1
            CSTimetableInf(i + 1).sName = tempTable.Rows(i).Item("CSTIMETABLENAME").ToString.Trim
            CSTimetableInf(i + 1).sID = tempTable.Rows(i).Item("CSTIMETABLEID").ToString.Trim
            CSTimetableInf(i + 1).sCreateDate = tempTable.Rows(i).Item("CREATETIME").ToString.Trim
            CSTimetableInf(i + 1).sEditDate = tempTable.Rows(i).Item("MODIFYTIME").ToString.Trim
            CSTimetableInf(i + 1).sDiagramID = tempTable.Rows(i).Item("TRAINDIAGRAMID").ToString.Trim
            CSTimetableInf(i + 1).ScheduleState = CInt(tempTable.Rows(i).Item("schedulestate").ToString.Trim)
            CSTimetableInf(i + 1).IFCorShcedule = CInt(tempTable.Rows(i).Item("ifcorschedule").ToString.Trim)
            If CSTimetableInf(i + 1).IFCorShcedule Then
                CSTimetableInf(i + 1).CorTimetableID = tempTable.Rows(i).Item("cortimetableid").ToString.Trim
            End If
        Next
        tempTable.Dispose()
    End Sub
    Public Function GetCSPlanIDFromCSPlanName(ByVal strCSPlanName As String) As String
        GetCSPlanIDFromCSPlanName = ""
        Dim sqlstr As String = "SELECT * FROM CS_CSTIMETABLEINF"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        For i As Integer = 0 To tempTable.Rows.Count - 1
            If tempTable.Rows(i).Item("CSTIMETABLENAME").ToString.Trim = strCSPlanName Then
                GetCSPlanIDFromCSPlanName = tempTable.Rows(i).Item("CSTIMETABLEID").ToString.Trim
                Exit For
            End If
        Next
        tempTable.Dispose()
    End Function

    Public Function GetCSPlanNameFromCSPlanID(ByVal strCSPlanID As String) As String
        GetCSPlanNameFromCSPlanID = ""
        Dim sqlstr As String = "SELECT * FROM CS_CSTIMETABLEINF"

        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)
        For i As Integer = 0 To tempTable.Rows.Count - 1
            If tempTable.Rows(i).Item("CSTIMETABLEID").ToString.Trim = strCSPlanID Then
                GetCSPlanNameFromCSPlanID = tempTable.Rows(i).Item("CSTIMETABLENAME").ToString.Trim
                Exit For
            End If
        Next
        tempTable.Dispose()
    End Function

    Public Function GetCSDIAGRAMIDFromCSPlanName(ByVal strCSPlanName As String) As String
        GetCSDIAGRAMIDFromCSPlanName = ""
        Dim sqlstr As String = "SELECT * FROM CS_CSTIMETABLEINF"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        For i As Integer = 0 To tempTable.Rows.Count - 1
            If tempTable.Rows(i).Item("CSTIMETABLENAME").ToString.Trim = strCSPlanName Then
                GetCSDIAGRAMIDFromCSPlanName = tempTable.Rows(i).Item("TRAINDIAGRAMID").ToString.Trim
                strDiagram = tempTable.Rows(i).Item("remark").ToString.Trim
                Exit For
            End If
        Next
        tempTable.Dispose()
    End Function
    Public Function GetLineIDFromLineName(ByVal strLineName As String) As String
        GetLineIDFromLineName = ""
        Dim sqlstr As String = "SELECT * FROM PD_LINEINFO"
        tempTable = New Data.DataTable
        tempTable = Globle.Method.ReadDataForAccess(sqlstr)

        For i As Integer = 0 To tempTable.Rows.Count - 1
            If tempTable.Rows(i).Item("LINENAME").ToString.Trim = strLineName Then
                GetLineIDFromLineName = tempTable.Rows(i).Item("LINEID").ToString.Trim
                Exit For
            End If
        Next
        tempTable.Dispose()
    End Function

End Module
