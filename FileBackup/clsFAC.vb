Imports System
Imports System.IO

Public Class clsFAC

    Public Sub StoreJobs(objJobs As clsJobs)

        Dim lintIndex As Integer

        If Not System.IO.File.Exists(ConfigFile()) Then
            System.IO.File.Create(ConfigFile()).Dispose()
        End If

        Using OutputFile = My.Computer.FileSystem.OpenTextFileWriter(ConfigFile(), False)
            With objJobs
                For lintIndex = 1 To .Count
                    With .Job(lintIndex)
                        OutputFile.WriteLine(.Serialise())
                    End With
                    OutputFile.WriteLine("")    'Leave a blank space to make the files more readable
                Next
            End With

            OutputFile.Close()
        End Using

    End Sub

    Private Function JobFile(strJobName As String) As String

        Dim lstrFileName As String

        lstrFileName = Replace(strJobName, " ", "")
        lstrFileName = Replace(strJobName, ":", "")
        lstrFileName = Replace(strJobName, "/", "")
        lstrFileName = Replace(strJobName, "\", "")

        Return Application.StartupPath & "\Backup.ini"
    End Function
    Public Function FetchJobs() As clsJobs

        'Dim FS As StreamReader
        Dim lintIndex As Integer
        Dim lobjJobs As New clsJobs
        Dim lobjJob As clsJob
        Dim lstrLine() As String
        Dim lstrJob As String

        If System.IO.File.Exists(ConfigFile()) Then

            lstrLine = File.ReadAllLines(ConfigFile())

            lstrJob = ""

            For lintIndex = 0 To UBound(lstrLine)
                'Check if we are at the start of a job
                If InStr(UCase(lstrLine(lintIndex)), "NAME") > 0 Then
                    If lstrJob <> "" Then
                        'Save the current job
                        lobjJob = New clsJob
                        lobjJob.Deserialise(lstrJob)
                        lobjJobs.AddJob(lobjJob)
                        lobjJob = Nothing
                    End If
                    lstrJob = ""
                End If
                lstrJob = lstrJob & vbCrLf & lstrLine(lintIndex)
            Next

            'Save the final job
            If lstrJob <> "" Then
                'Save the current job
                lobjJob = New clsJob
                lobjJob.Deserialise(lstrJob)
                lobjJobs.AddJob(lobjJob)
                lobjJob = Nothing
            End If
        End If

        FetchJobs = lobjJobs

    End Function

    Public Function ConfigFile() As String
        Return Application.StartupPath & "\Backup.ini"
    End Function

End Class
