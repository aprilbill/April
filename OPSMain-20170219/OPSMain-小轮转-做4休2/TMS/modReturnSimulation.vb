Module modReturnSimulation
    Public TrainReturnRunTrack() As TrainRunTrackInformation

    Structure typeReturnSimu
        Dim nCurTrain As Integer
        Dim lngCurTime() As Long
        Dim nCurTrackID() As Integer
        Dim nNextTrackID() As Integer
    End Structure
    Public RuturnSimu() As typeReturnSimu

    Public Sub SeekStationRunTrackID(ByVal nUporDown As Integer, ByVal nFirSta As Integer, ByVal nFirTrackID As Integer, ByVal nSecTrackID As Integer, ByVal ID As Integer, ByVal TrackGroupID As Integer, ByVal sngSpeed As Single, ByVal UporDownTime As Long, ByVal nReturnStopTime As Single)
        Dim i As Integer
        Dim j As Integer
        Dim Str As String
        Dim sCurStaName As String
        Str = ""
        sCurStaName = CADStaInf(nFirSta).sStaName
        Dim nIfIn As Integer
        nIfIn = 0
        If nFirTrackID > 0 And nSecTrackID > 0 Then
            ReDim nTrackPath(0)
            Call InputCrossData(nFirSta)
            Call SeekRoadFromID(nUporDown, nFirSta, nFirTrackID, nSecTrackID)
            For i = 1 To UBound(nTrackPath)
                nIfIn = 0
                For j = 1 To UBound(TrainReturnRunTrack(ID).nTrackID)
                    If TrainReturnRunTrack(ID).nTrackID(j).nTrackID = nTrackPath(i) Then
                        nIfIn = 1
                        Exit For
                    End If
                Next
                If nIfIn = 0 Then
                    ReDim Preserve TrainReturnRunTrack(ID).nTrackID(UBound(TrainReturnRunTrack(ID).nTrackID) + 1)
                    TrainReturnRunTrack(ID).nTrackID(UBound(TrainReturnRunTrack(ID).nTrackID)).nStaID = nFirSta
                    TrainReturnRunTrack(ID).nTrackID(UBound(TrainReturnRunTrack(ID).nTrackID)).nTrackID = nTrackPath(i)
                    TrainReturnRunTrack(ID).nTrackID(UBound(TrainReturnRunTrack(ID).nTrackID)).nTrackGroup = TrackGroupID
                    'TrainReturnRunTrack(ID).nTrackID(UBound(TrainReturnRunTrack(ID).nTrackID)).intRunTime = (CADStaInf(nFirSta).Track(nTrackPath(i)).sngLength / sngSpeed) * 3600
                    ''If i > 1 And i < UBound(nTrackPath) Then '站后折返时,需在股道上上下客
                    'If CADStaInf(nFirSta).Track(nTrackPath(i)).sStyle = "股道线" And CADStaInf(nFirSta).Track(nTrackPath(i)).sGuDaoStyle.Length >= 3 Then
                    '    If CADStaInf(nFirSta).Track(nTrackPath(i)).sGuDaoStyle.Substring(0, 3) = "正线线" Or CADStaInf(nFirSta).Track(nTrackPath(i)).sGuDaoStyle.Substring(0, 3) = "到发线" Then
                    '        TrainReturnRunTrack(ID).nTrackID(UBound(TrainReturnRunTrack(ID).nTrackID)).intRunTime = TrainReturnRunTrack(ID).nTrackID(UBound(TrainReturnRunTrack(ID).nTrackID)).intRunTime + UporDownTime
                    '    ElseIf CADStaInf(nFirSta).Track(nTrackPath(i)).sGuDaoStyle.Substring(0, 3) = "折返线" Or CADStaInf(nFirSta).Track(nTrackPath(i)).sGuDaoStyle.Substring(0, 3) = "存车线" Then
                    '        TrainReturnRunTrack(ID).nTrackID(UBound(TrainReturnRunTrack(ID).nTrackID)).intRunTime = TrainReturnRunTrack(ID).nTrackID(UBound(TrainReturnRunTrack(ID).nTrackID)).intRunTime + nReturnStopTime
                    '    End If
                    'End If
                    ''End If
                    'TrainReturnRunTrack(ID).nTrackID(UBound(TrainReturnRunTrack(ID).nTrackID)).sControlModle = CADStaInf(nFirSta).Track(nTrackPath(i)).sControlNum
                    Str = Str & "-" & CADStaInf(nFirSta).Track(nTrackPath(i)).sTrackCircuitNum
                End If
            Next
        End If
        'MsgBox(Str)
    End Sub
End Module
