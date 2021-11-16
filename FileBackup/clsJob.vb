Public Class clsJob

    Public Name As String
    Public SourceFolder As String
    Public DestinationFolder As String
    Public LastRun As Date
    Public LastRunDuration As Integer
    Public LastSuccessfulRun As Date
    Public LastTotalNumberOfFilesCopied As Integer
    Public LastTotalFileSizeCopied As Integer
    Public NextRun As Date
    Public Period As Integer
    Public RunTime As String
    Public Status As Integer  '0 = Not run; 1 = Running; 2 = Success; 3 = Failed
    Public Enabled As Boolean
    Public Frequency As Integer '1 = Daily; 2 = Weekly; 3 = Monthly
    Dim mblnDays(0 To 7) As Boolean '(1=Sunday, 2=Monday,etc.7= Saturday)
    Dim mblnTickedDays(0 To 7) As Boolean '(1=Sunday, 2=Monday,etc.7= Saturday)
    Public DayInMonth As Integer
    Public Hours As Integer
    Public Minutes As Integer
    Public Edited As Boolean
    Public LogFileName As String
    Public LastRunErrors As Boolean
    Public CurrentlyRunning As Boolean

    Public Property Day(Index As Integer) As Boolean
        Set(value As Boolean)
            If (Index >= 0 And Index <= 7) Then
                mblnDays(Index) = value
                'Horrible hack to stop my brain frying when I think Sunday is day 7. Yes, I know I should adopt the fact that Sunday is day 0, but I just refuse to, because no-one actually thinks like that.
                If Index = 7 Then
                    mblnDays(0) = value 'Make sure Sunday is reflected as day 0
                End If
                If Index = 0 Then
                    mblnDays(7) = value 'Make sure Sunday is reflected as day 7
                End If
            End If
        End Set
        Get
            If (Index >= 0 And Index <= 7) Then
                Return mblnDays(Index)
            Else
                Return False
            End If
        End Get

    End Property

    Public Property TickedDay(Index As Integer) As Boolean
        Set(value As Boolean)
            If (Index >= 0 And Index <= 7) Then
                mblnTickedDays(Index) = value
                'Horrible hack to stop my brain frying when I think Sunday is day 7. Yes, I know I should adopt the fact that Sunday is day 0, but I just refuse to, because no-one actually thinks like that.
                If Index = 7 Then
                    mblnTickedDays(0) = value   'Make sure Sunday is reflected as day 0
                End If
                If Index = 0 Then
                    mblnTickedDays(7) = value   'Make sure Sunday is reflected as day 7
                End If
            End If
        End Set
        Get
            If (Index >= 0 And Index <= 7) Then
                Return mblnTickedDays(Index)
            End If
        End Get

    End Property

    Public Sub DeriveLogFileName()

        Dim llngIndex As Long
        Dim lstrResult As String = ""
        Dim lstrTempName As String

        lstrTempName = Replace(Name, " ", "_")

        For llngIndex = 1 To Len(Name)
            If InStr("ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_", UCase(lstrTempName.Substring(llngIndex - 1, 1))) > 0 Then
                lstrResult = lstrResult & lstrTempName.Substring(llngIndex - 1, 1)
            End If
        Next
        LogFileName = lstrResult

    End Sub

    Public Sub CalculateNextRun()

        Dim llngNextDayIndex As Long
        Dim llngDays As Long
        Dim ldtmTestDate As Date
        Dim llngDay As Long


        llngDay = Int(Now().DayOfWeek().ToString("d"))

        Select Case Frequency

            Case 1 'Daily

                If mblnTickedDays(llngDay) = True Then  'Check if today is a back-up day
                    ldtmTestDate = Format(Now(), "dd-MMMM-yyyy") & " " & CStr(Hours) & ":" & CStr(Minutes)
                End If

                If ldtmTestDate > Now() Then
                    NextRun = ldtmTestDate
                Else
                    llngNextDayIndex = llngDay + 1
                    Do Until mblnTickedDays(llngNextDayIndex Mod 7) = True
                        llngNextDayIndex = llngNextDayIndex + 1
                    Loop

                    llngDays = llngNextDayIndex - llngDay
                    NextRun = Format(DateAdd("d", llngDays, Now()), "dd-MMMM-yyyy") & " " & CStr(Hours) & ":" & CStr(Minutes)
                End If

            Case 2 'Weekly

                If mblnDays(llngDay) = True Then  'Check if today is a back-up day
                    ldtmTestDate = Format(Now(), "dd-MMMM-yyyy") & " " & CStr(Hours) & ":" & CStr(Minutes)
                End If

                If ldtmTestDate > Now() Then
                    NextRun = ldtmTestDate
                Else
                    llngNextDayIndex = Int(Now().DayOfWeek().ToString("d")) + 1
                    Do Until mblnDays(llngNextDayIndex Mod 7) = True
                        llngNextDayIndex = llngNextDayIndex + 1
                    Loop

                    llngDays = llngNextDayIndex - Int(Now().DayOfWeek().ToString("d"))
                    NextRun = Format(DateAdd("d", llngDays, Now()), "dd-MMMM-yyyy") & " " & CStr(Hours) & ":" & CStr(Minutes)
                End If

            Case 3 'Monthly

                If (Now().Day = DayInMonth) Then  'Check if today is a back-up day
                    ldtmTestDate = Format(Now(), "dd-MMMM-yyyy") & " " & CStr(Hours) & ":" & CStr(Minutes)
                End If

                If ldtmTestDate > Now() Then
                    NextRun = ldtmTestDate
                Else
                    If DayInMonth > Now().Day Then
                        NextRun = CStr(DayInMonth) & "-" & Format(Now(), "MMMM-yyyy") & " " & CStr(Hours) & ":" & CStr(Minutes)
                    Else
                        NextRun = CStr(DayInMonth) & "-" & Format(DateAdd(DateInterval.Month, 1, Now()), "MMMM-yyyy") & " " & CStr(Hours) & ":" & CStr(Minutes)
                    End If
                End If

        End Select

    End Sub

    Public Function Clone() As clsJob

        Dim lobjClone As New clsJob
        Dim lintIndex As Long

        With lobjClone
            .Name = Name
            .SourceFolder = SourceFolder
            .DestinationFolder = DestinationFolder
            .LastRun = LastRun
            .LastRunDuration = LastRunDuration
            .LastSuccessfulRun = LastSuccessfulRun
            .LastTotalNumberOfFilesCopied = LastTotalNumberOfFilesCopied
            .LastTotalFileSizeCopied = LastTotalFileSizeCopied
            .NextRun = NextRun
            .Period = Period
            .RunTime = RunTime
            .Status = Status '0 = Not run; 1 = Running; 2 = Success; 3 = Failed
            .Enabled = Enabled
            .Frequency = Frequency '1 = Daily; 2 = Weekly; 3 = Monthly
            .Hours = Hours
            .Minutes = Minutes
            For lintIndex = 1 To 7
                .TickedDay(lintIndex) = mblnTickedDays(lintIndex)
                .Day(lintIndex) = mblnDays(lintIndex)
            Next
            .DayInMonth = DayInMonth

        End With

        Return lobjClone

    End Function

    Public Function Serialise() As String

        Dim lstrReturn As String
        Dim lintIndex As Integer

        lstrReturn = "Name=" & Name & vbCrLf _
                   & "SourceFolder=" & SourceFolder & vbCrLf _
                   & "DestinationFolder=" & DestinationFolder & vbCrLf _
                   & "LastRun=" & LastRun & vbCrLf _
                   & "LastRunDuration=" & LastRunDuration & vbCrLf _
                   & "LastSuccessfulRun=" & LastSuccessfulRun & vbCrLf _
                   & "LastTotalNumberOfFilesCopied=" & LastTotalNumberOfFilesCopied & vbCrLf _
                   & "LastTotalFileSizeCopied=" & LastTotalFileSizeCopied & vbCrLf _
                   & "NextRun=" & NextRun & vbCrLf _
                   & "Period=" & Period & vbCrLf _
                   & "RunTime=" & RunTime & vbCrLf _
                   & "Status=" & Status & vbCrLf _
                   & "Enabled=" & Enabled & vbCrLf _
                   & "Frequency=" & Frequency & vbCrLf _
                   & "Hours=" & Hours & vbCrLf _
                   & "Minutes=" & Minutes & vbCrLf _
                   & "LastRunErrors=" & CStr(LastRunErrors) & vbCrLf

        lstrReturn = lstrReturn & "DailyDays="
        For lintIndex = 1 To 7
            If lintIndex > 1 Then
                lstrReturn = lstrReturn & ","
            End If
            lstrReturn = lstrReturn & mblnTickedDays(lintIndex).ToString
        Next
        lstrReturn = lstrReturn & vbCrLf

        lstrReturn = lstrReturn & "WeeklyDay="

        For lintIndex = 1 To 7
            If lintIndex > 1 Then
                lstrReturn = lstrReturn & ","
            End If
            lstrReturn = lstrReturn & mblnDays(lintIndex).ToString
        Next
        lstrReturn = lstrReturn & vbCrLf

        lstrReturn = lstrReturn & "DayInMonth=" & DayInMonth.ToString

        Return lstrReturn

    End Function

    Public Sub Deserialise(strInput As String)

        Dim lstrValues As String
        Dim lstrLine() As String
        Dim lstrData() As String
        Dim lstrDay() As String
        Dim lintIndex As Integer
        Dim lintDay As Integer

        lstrValues = Replace(strInput, vbCrLf, "|")
        lstrLine = Split(lstrValues, "|")

        For lintIndex = 0 To UBound(lstrLine, 1)
            lstrData = Split(lstrLine(lintIndex), "=")

            Select Case UCase(lstrData(0))
                Case "NAME"
                    Name = lstrData(1)
                Case "SOURCEFOLDER"
                    SourceFolder = lstrData(1)
                Case "DESTINATIONFOLDER"
                    DestinationFolder = lstrData(1)
                Case "LASTRUN"
                    LastRun = lstrData(1)
                Case "LASTRUNDURATION"
                    LastRunDuration = lstrData(1)
                Case "LASTSUCCESSFULRUN"
                    LastSuccessfulRun = lstrData(1)
                Case "LASTTOTALNUMBEROFFILESCOPIED"
                    LastTotalNumberOfFilesCopied = lstrData(1)
                Case "LASTTOTALFILESIZECOPIED"
                    LastTotalFileSizeCopied = lstrData(1)
                Case "NEXTRUN"
                    NextRun = lstrData(1)
                Case "PERIOD"
                    Period = lstrData(1)
                Case "RUNTIME"
                    RunTime = lstrData(1)
                Case "STATUS"
                    Status = lstrData(1)
                Case "ENABLED"
                    Enabled = lstrData(1)
                Case "FREQUENCY"
                    Frequency = lstrData(1)
                Case "DAYINMONTH"
                    DayInMonth = lstrData(1)
                Case "HOURS"
                    Hours = lstrData(1)
                Case "MINUTES"
                    Minutes = lstrData(1)
                Case "DAILYDAYS"
                    lstrDay = Split(lstrData(1), ",")
                    For lintDay = 0 To 6
                        mblnTickedDays(lintDay + 1) = CBool(lstrDay(lintDay))
                    Next
                Case "WEEKLYDAY"
                    lstrDay = Split(lstrData(1), ",")
                    For lintDay = 0 To 6
                        mblnDays(lintDay + 1) = CBool(lstrDay(lintDay))
                    Next
                Case "LASTRUNERRORS"
                    LastRunErrors = CBool(lstrData(1))

            End Select
        Next
        DeriveLogFileName()

    End Sub
End Class
