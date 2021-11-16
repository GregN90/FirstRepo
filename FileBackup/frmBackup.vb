'Imports System.IO
Imports Delimon.Win32.IO

Public Class frmFileBackup

    Dim mobjJobs As clsJobs
    Dim mblnFirstTenSeconds As Boolean
    Dim mdtmStartTime As Date
    Dim mblnNoChange As Boolean
    Dim mlngProgress As Long
    Dim mblnAbort As Boolean

    Private Function SynchroniseFolders(strSourcePath As String, strDestinationPath As String, strLogFile As String) As String

        Dim lobjSourceFolder As clsFolder = New clsFolder
        Dim lobjDestinationFolder As clsFolder = New clsFolder
        Dim SourceFileList As List(Of String)
        Dim DestinationFileList As List(Of String)
        Dim SourceFileListShort As List(Of String)
        Dim DestinationFileListShort As List(Of String)
        Dim InSourceNotInDestList As List(Of String)
        Dim InDestNotInSourceList As List(Of String)
        Dim lstrPartResult As String = ""
        Dim lstrBatchResult As String = ""
        Dim lstrFullResult As String = ""
        Dim lstrMessage As String = ""

        Dim ldtmStartDate As Date
        Dim ldtmEndDate As Date
        Dim llngSecondsToComplete As Long
        Dim llngSecondsSoFar As Long

        Dim lstrData() As String
        Dim lstrSourceFile As String
        Dim lstrDestinationFile As String
        Dim llngIndex As Long

        Dim llngResponse As Long
        Dim lstrLogPath As String
        Dim lstrResult As String
        Dim lblnErrors As Boolean = False
        Dim llngCopyCount As Long = 0
        Dim llngDeleteCount As Long = 0
        Dim llngCopyFailCount As Long = 0
        Dim llngDeleteFailCount As Long = 0
        Dim llngAccumulatedCopySize As Long = 0
        Dim llngTotalCopySize As Long = 0
        Dim llngPos As Long
        Dim llngErrPos As Long
        Dim lstrErrorResult As String

        mblnAbort = False

        Do While (Strings.Right(strSourcePath, 1) = "\")
            strSourcePath = Strings.Left(strSourcePath, Len(strSourcePath) - 1)
        Loop
        Do While (Strings.Right(strDestinationPath, 1) = "\")
            strDestinationPath = Strings.Left(strDestinationPath, Len(strDestinationPath) - 1)
        Loop

        lstrLogPath = Application.StartupPath & "\" & strLogFile & "_" & Format(Now, "yyyy-MM-dd") & ".txt"

        LogActivity(lstrLogPath, "-----------------------------------------------------------------")
        LogActivity(lstrLogPath, TimeSnapshot() & " :: About to synchronise " & strSourcePath & " ---> " & strDestinationPath)
        lobjSourceFolder.Path = strSourcePath
        lobjDestinationFolder.Path = strDestinationPath

        txtProgress.Visible = True
        lblProgress.Visible = True
        txtCompletionTime.Visible = txtProgress.Visible

        cmdRunSelectedBackup.Visible = False
        cmdAbortBackup.Visible = Not cmdRunSelectedBackup.Visible


        'Check that destination exists or can be created
        If Not Directory.Exists(strDestinationPath) Then
            Try
                Directory.CreateDirectory(lobjDestinationFolder.Path & "\")
            Catch
                lstrMessage = "Error occured in SynchroniseFolders('" & strSourcePath & "', '" & strDestinationPath & "') " _
                            & "Could not create the destination folder '" & lobjDestinationFolder.Path & "' " _
                            & Err.Number & " - " & Err.Description
                LogActivity(lstrLogPath, TimeSnapshot() & " " & lstrMessage)
                ShowActivityOnScreen(lstrMessage)
                Return lstrMessage
            End Try
        End If

        LogActivity(lstrLogPath, TimeSnapshot() & " " & "Scanning source folder: " & lobjSourceFolder.Path)
        ShowActivityOnScreen("Scanning source folder: " & lobjSourceFolder.Path)

        'Fetch the contents of the source and destination folders - and log any errors
        lstrResult = FetchFolderContents(lobjSourceFolder)
        If lstrResult <> "" Then
            lblnErrors = True
            LogActivity(lstrLogPath, TimeSnapshot() & " Error fetching contents of source folder '" & lobjSourceFolder.Path & "': " & lstrResult)
            ShowActivityOnScreen("Error fetching contents of source folder '" & lobjSourceFolder.Path & "': " & lstrResult)
        End If

        LogActivity(lstrLogPath, TimeSnapshot() & " " & "Scanning destination folder: " & lobjDestinationFolder.Path)
        ShowActivityOnScreen("Scanning destination folder: " & lobjDestinationFolder.Path)

        lstrResult = FetchFolderContents(lobjDestinationFolder)
        If lstrResult <> "" Then
            lblnErrors = True
            LogActivity(lstrLogPath, TimeSnapshot() & " Error fetching contents of destination folder '" & lobjDestinationFolder.Path & "': " & lstrResult)
            ShowActivityOnScreen("Error fetching contents of source folder '" & lobjSourceFolder.Path & "': " & lstrResult)
        End If

        SourceFileList = lobjSourceFolder.FileList(False, True)
        DestinationFileList = lobjDestinationFolder.FileList(False, True)

        SourceFileListShort = lobjSourceFolder.FileList(True, True) 'Don't care about dates for these items
        DestinationFileListShort = lobjDestinationFolder.FileList(True, True)

        'Check files to copy to backup destination
        InSourceNotInDestList = SourceFileList.Except(DestinationFileList).ToList()

        ldtmStartDate = Now()

        With InSourceNotInDestList

            llngTotalCopySize = .Count  'Change this to file size at some point

            llngCopyCount = 0
            For llngIndex = 0 To .Count - 1

                If mblnAbort = True Then
                    LogActivity(lstrLogPath, "Job Aborted")
                    Exit For
                End If

                lstrPartResult = ""
                lstrData = Split(.Item(llngIndex), "|")
                lstrSourceFile = lobjSourceFolder.Path & lstrData(0)
                lstrDestinationFile = lobjDestinationFolder.Path & lstrData(0)

                lstrPartResult = TimeSnapshot() & " :: Copying " & lstrSourceFile
                ShowActivityOnScreen(lstrPartResult & "....")

                '**** Copy File ******
                lstrErrorResult = CopyFileToDestination(lstrSourceFile, lstrDestinationFile)
                If lstrErrorResult = "SUCCESS" Then
                    DestinationFileListShort.Add(lstrData(0)) 'Copy the file into the destination list, otherwise, the system will delete the file in the deletion routine below.
                    lstrPartResult = lstrPartResult & " :: SUCCESS" & vbCrLf
                    llngCopyCount = llngCopyCount + 1
                Else
                    Select Case DelimonError(lstrErrorResult)
                        Case "Path not found"   'Cannot find destination folder

                            lstrPartResult = lstrPartResult & " : " & lstrErrorResult & vbCrLf

                            llngPos = InStrRev(lstrDestinationFile, "\")
                            lstrPartResult = lstrPartResult & TimeSnapshot() & " :: Attempting to create destination folder " & lstrDestinationFile.Substring(0, llngPos - 1) & vbCrLf

                            '*** Create directory ****
                            lstrErrorResult = CreateDirectory(lstrDestinationFile.Substring(0, llngPos - 1))
                            If lstrErrorResult = "SUCCESS" Then
                                lstrPartResult = lstrPartResult & TimeSnapshot() & " :: Folder creation successful. Attempting to re-copy" & vbCrLf
                            Else
                                LogError("Failed to create destination directory: " & lstrErrorResult)

                                lstrPartResult = lstrPartResult & " :: FAIL - " & lstrErrorResult & " :: Copying to " & lstrDestinationFile & vbCrLf
                                lblnErrors = True
                                llngCopyFailCount = llngCopyFailCount + 1
                                LogActivity(lstrLogPath, lstrBatchResult)
                                lstrBatchResult = ""
                                Exit Select
                            End If

                            'If we've got this far, the destination directory should exist
                            '**** Copy File ******
                            lstrErrorResult = CopyFileToDestination(lstrSourceFile, lstrDestinationFile)
                            If lstrErrorResult = "SUCCESS" Then
                                DestinationFileListShort.Add(lstrData(0)) 'Copy the file into the destination list, otherwise, the system will delete the file in the deletion routine below.
                                lstrPartResult = lstrPartResult & " :: SUCCESS" & vbCrLf
                                llngCopyCount = llngCopyCount + 1
                            Else
                                LogError("Failed to copy file: " & lstrErrorResult)
                                lstrPartResult = lstrPartResult & " :: FAIL - " & lstrErrorResult & " :: Copying to " & lstrDestinationFile & vbCrLf
                                lblnErrors = True
                                llngCopyFailCount = llngCopyFailCount + 1
                                LogActivity(lstrLogPath, lstrBatchResult)
                                lstrBatchResult = ""
                                Exit Select
                            End If

                        Case "File exists"  'Apparently, the destination file already exists

                            LogError("Failed to copy. " & lstrErrorResult & " when copying : " & lstrSourceFile & " to " & lstrDestinationFile)
                            lstrPartResult = lstrPartResult & " :: FAIL - " & lstrErrorResult & " :: Copying to " & lstrDestinationFile & vbCrLf
                            lblnErrors = True
                            DestinationFileListShort.Add(lstrData(0)) 'Copy the file into the destination list, otherwise, the system will delete the file in the deletion routine below.

                            llngCopyFailCount = llngCopyFailCount + 1
                            LogActivity(lstrLogPath, lstrBatchResult)
                            lstrBatchResult = ""
                            Exit Select

                        Case "Access denied"

                            LogError("Failed to copy. " & lstrErrorResult & " when copying : " & lstrSourceFile & " to " & lstrDestinationFile)
                            lstrPartResult = lstrPartResult & " :: FAIL - " & lstrErrorResult & " :: Copying to " & lstrDestinationFile & vbCrLf
                            lblnErrors = True
                            llngCopyFailCount = llngCopyFailCount + 1
                            LogActivity(lstrLogPath, lstrBatchResult)
                            lstrBatchResult = ""
                            Exit Select

                        Case Else

                            LogError("Failed to copy. " & lstrErrorResult & " when copying : " & lstrSourceFile & " to " & lstrDestinationFile)
                            lstrPartResult = lstrPartResult & " :: FAIL - " & lstrErrorResult & " :: Copying to " & lstrDestinationFile & vbCrLf
                            lblnErrors = True
                            llngCopyFailCount = llngCopyFailCount + 1
                            LogActivity(lstrLogPath, lstrBatchResult)
                            lstrBatchResult = ""
                            Exit Select

                    End Select

                End If

                llngAccumulatedCopySize = llngAccumulatedCopySize + 1   'Change this to filesize at some point...
                txtProgress.Text = Format(100 * (llngAccumulatedCopySize / llngTotalCopySize), "0.00") & "%"
                ShowActivityOnScreen(lstrPartResult, True)

                lstrBatchResult = lstrBatchResult & lstrPartResult
                If (llngIndex Mod 50) = 0 Then
                    LogActivity(lstrLogPath, lstrBatchResult)
                    lstrBatchResult = ""

                    llngSecondsSoFar = DateDiff("s", ldtmStartDate, Now())
                    llngSecondsToComplete = llngSecondsSoFar * (llngTotalCopySize / llngAccumulatedCopySize)
                    ldtmEndDate = DateAdd("s", llngSecondsToComplete, ldtmStartDate)
                    txtCompletionTime.Text = "Est. Completion: " & Format(ldtmEndDate, "HH:mm")
                End If
            Next
        End With

        If lstrBatchResult <> "" Then
            LogActivity(lstrLogPath, lstrBatchResult)
        End If

        lstrBatchResult = ""

        If chkSuppressDelete.Checked = True Then
            LogActivity(lstrLogPath, "Suppress Delete = True; No deletion from destination checked or performed")
            ShowActivityOnScreen("Suppress Delete = True; No deletion from destination performed")
        Else

            'Check files to delete from backup destination
            InDestNotInSourceList = DestinationFileListShort.Except(SourceFileListShort).ToList()

            With InDestNotInSourceList
                llngDeleteCount = 0
                For llngIndex = 0 To .Count - 1

                    If mblnAbort = True Then
                        LogActivity(lstrLogPath, "Job Aborted")
                        Exit For
                    End If

                    lstrPartResult = ""
                    lstrData = Split(.Item(llngIndex), "|")
                    lstrDestinationFile = lobjDestinationFolder.Path & lstrData(0)
                    lstrPartResult = lstrPartResult & TimeSnapshot() & " :: Deleting " & lstrDestinationFile
                    Try
                        File.Delete(lstrDestinationFile)
                        lstrPartResult = lstrPartResult & " :: SUCCESS" & vbCrLf
                        llngDeleteCount = llngDeleteCount + 1
                    Catch
                        LogError("Error: " & Err.Number & " - " & Err.Description & " when attempting to delete file: " & lstrDestinationFile)
                        lblnErrors = True
                        llngCopyFailCount = llngCopyFailCount + 1
                        lstrPartResult = lstrPartResult & " :: FAIL - " & Err.Number & " - " & Err.Description & vbCrLf
                        LogActivity(lstrLogPath, lstrBatchResult)
                        lstrBatchResult = ""
                    End Try

                    ShowActivityOnScreen(lstrPartResult)

                    lstrBatchResult = lstrBatchResult & lstrPartResult
                    If (llngIndex Mod 50) = 0 Then
                        LogActivity(lstrLogPath, lstrBatchResult)
                        lstrBatchResult = ""
                    End If
                    '       lstrFullResult = lstrFullResult & lstrPartResult
                Next

            End With
        End If

        If lstrBatchResult <> "" Then
            LogActivity(lstrLogPath, lstrBatchResult)
        End If

        txtProgress.Visible = False
        lblProgress.Visible = False
        txtCompletionTime.Visible = txtProgress.Visible

        '---------------------------------
        'Logging
        '---------------------------------
        If mblnAbort = True Then
            lstrMessage = TimeSnapshot() & " JOB ABORTED - "
        Else
            lstrMessage = TimeSnapshot() & " JOB COMPLETE - "
        End If

        If lblnErrors = True Then
            lstrMessage = lstrMessage & "! WITH ERRORS !" & vbCrLf
        Else
            lstrMessage = lstrMessage & "No Errors" & vbCrLf
        End If

        lstrMessage = lstrMessage & "Total Files Copied: " & CStr(llngCopyCount) & vbCrLf _
                    & "Total Copy failures: " & CStr(llngCopyFailCount) & vbCrLf _
                    & "Total Files Deleted: " & CStr(llngDeleteCount) & vbCrLf _
                    & "Total deletion failures: " & CStr(llngDeleteFailCount) & vbCrLf

        LogActivity(lstrLogPath, lstrMessage)

        ShowActivityOnScreen("Total Files Copied: " & CStr(llngCopyCount))
        ShowActivityOnScreen("Total Copy failures: " & CStr(llngCopyFailCount))
        ShowActivityOnScreen("Total Files Deleted: " & CStr(llngDeleteCount))
        ShowActivityOnScreen("Total deletion failures: " & CStr(llngDeleteFailCount))
        ShowActivityOnScreen(lstrMessage)
        '---------------------------------
        'End Of Logging
        '---------------------------------

        If lblnErrors = True Then
            SynchroniseFolders = "ERROR"
        End If

        cmdRunSelectedBackup.Visible = True
        cmdAbortBackup.Visible = Not cmdRunSelectedBackup.Visible

    End Function

    Private Function CopyFileToDestination(strSourceFile As String, strDestinationFile As String) As String
        Try
            File.Copy(strSourceFile, strDestinationFile, True)
            CopyFileToDestination = "SUCCESS"
        Catch
            CopyFileToDestination = "Error " & Err.Number & " - " & Err.Description
        End Try

    End Function

    Private Function CreateDirectory(strPath As String) As String

        'Clunky routine to get round shortcomings in Delimon's CreateDirectory function

        Dim llngIndex As Long
        Dim llngPathCount As Long
        Dim lstrData() As String
        Dim lstrTestPath As String

        CreateDirectory = "SUCCESS" 'Default to success

        lstrData = Split(Replace(strPath, "\\", "**"), "\")

        llngPathCount = lstrData.GetUpperBound(0)

        lstrTestPath = Replace(lstrData(0), "**", "\\")
        For llngIndex = 1 To llngPathCount
            lstrTestPath = lstrTestPath & "\" & lstrData(llngIndex)
            If Not Directory.Exists(lstrTestPath) Then
                Try     'Try to create the destination folder
                    Directory.CreateDirectory(lstrTestPath)
                Catch
                    CreateDirectory = "Error " & Err.Number & " - " & Err.Description
                    Exit For
                End Try

            End If
        Next

    End Function

    Private Function DelimonError(strMessage As String) As String
        Dim lstrResult As String

        If InStr(strMessage, "cannot find the path specified") Then
            lstrResult = "Path not found"
        ElseIf InStr(strMessage, "file already exists") > 0 Then
            lstrResult = "File exists"
        ElseIf InStr(strMessage, "Access is denied") > 0 Then
            lstrResult = "Access denied"
        Else
            lstrResult = "Unknown"
        End If

        DelimonError = lstrResult

    End Function

    Private Function TimeSnapshot() As String
        TimeSnapshot = Format(Now, "yyyy-MM-dd HH:mm:ss") & Strings.Right(Format(Microsoft.VisualBasic.Timer, "0.00"), 3)
    End Function

    Private Sub ShowActivityOnScreen(strMessage As String, Optional blnReplacePrevious As Boolean = False)

        'Show activity on screen
        If chkShowActivity.Checked = True Then
            With lstActivity.Items
                If blnReplacePrevious = True Then
                    .RemoveAt(0)
                End If
                .Insert(0, TimeSnapshot() & " :: " & strMessage)
                Do Until .Count < 400
                    .RemoveAt(399)
                Loop
                Application.DoEvents()
            End With
        End If

    End Sub

    Public Sub LogActivity(strFile As String, strData As String)

        'Get rid of any carriage returns at the end
        If Strings.Right(strData, 2) = vbCrLf Then
            strData = Strings.Left(strData, Len(strData) - 2)
        End If

        If Not System.IO.File.Exists(strFile) Then
            System.IO.File.Create(strFile).Dispose()
        End If

        Try
            Using OutputFile = My.Computer.FileSystem.OpenTextFileWriter(strFile, True)
                OutputFile.WriteLine(strData)
                OutputFile.Close()
            End Using
        Catch

        End Try

    End Sub

    Public Sub LogError(strData As String)

        Dim lstrFile As String

        lstrFile = ErrorLogFile()

        If Not System.IO.File.Exists(lstrFile) Then
            System.IO.File.Create(lstrFile).Dispose()
        End If

        Try
            Using OutputFile = My.Computer.FileSystem.OpenTextFileWriter(lstrFile, True)
                OutputFile.WriteLine(TimeSnapshot() & " :: " & strData)
                OutputFile.Close()
            End Using
        Catch

        End Try

    End Sub

    Public Function ParentPath(strPath As String) As String

        Dim llngPos As Long

        llngPos = InStrRev(strPath, "\")
        ParentPath = strPath.Substring(0, llngPos - 1)

    End Function

    Sub CopyFile(lstrSourceFile As String, lstrDestinationFile As String)

        File.Copy(lstrSourceFile, lstrDestinationFile, True)

    End Sub

    Function FetchFolderContents(ByRef objFolder As clsFolder, Optional strRoot As String = "") As String

        Dim llngIndex As Long
        Dim lstrFile As String()
        Dim lstrFolder As String()
        Dim lstrCurrentFile As String
        Dim lobjFile As clsFile = New clsFile
        Dim lobjFolder As clsFolder = New clsFolder
        Dim lintPathLen As Integer
        Dim lintRootLen As Integer
        Dim lstrResult As String
        Dim lstrReturn As String = ""
        Dim llngTotalSize As Long = 0
        Dim llngTotalFiles As Long = 0

        If mblnAbort = True Then
            Exit Function
        End If

        If strRoot = "" Then
            strRoot = objFolder.Path
        End If

        lintRootLen = Len(strRoot)
        If Strings.Right(objFolder.Path, 1) = "\" Then
            lintPathLen = objFolder.Path.Length() + 1
        Else
            lintPathLen = objFolder.Path.Length() + 2
        End If

        ShowActivityOnScreen("Scanning: " & objFolder.Path, True)

        Try
            lstrFile = Directory.GetFiles(objFolder.Path & "\")
        Catch
            'There is a problem accessing this folder
            FetchFolderContents = CStr(Err.Number) & "-" & Err.Description
            LogError("Error " & Err.Number & " - " & Err.Description & " getting files within: " & objFolder.Path)
            Exit Function
        End Try

        For llngIndex = lstrFile.GetLowerBound(0) To lstrFile.GetUpperBound(0)
            llngTotalFiles = llngTotalFiles + 1 'Increment counter, rather than take the difference between U/Lbound - in case there is a problem with a file
            lstrCurrentFile = lstrFile(llngIndex)
            Try
                With lobjFile
                    .Name = Mid(lstrCurrentFile, lintPathLen)
                    .Path = objFolder.Path
                    .PathWithoutRoot = .Path.Substring(lintRootLen)
                    .CreatedDate = File.GetCreationTimeUtc(lstrCurrentFile)
                    .ModifiedDate = File.GetLastWriteTimeUtc(lstrCurrentFile)
                    .LastAccessedDate = File.GetLastAccessTime(lstrCurrentFile)
                    Try
                        .Size = FileLen(lstrCurrentFile)
                        llngTotalSize = llngTotalSize + .Size
                    Catch
                        'Don't care if we don't get the size
                    End Try
                End With

            Catch
                LogError("Error " & Err.Number & " - " & Err.Description & " getting info for : " & lstrCurrentFile)
            End Try

            objFolder.AddFile(lobjFile)
            lobjFile = Nothing
            lobjFile = New clsFile

        Next

        lstrFolder = Directory.GetDirectories(objFolder.Path)
        For llngIndex = lstrFolder.GetLowerBound(0) To lstrFolder.GetUpperBound(0)
            lobjFolder = New clsFolder
            lobjFolder.Path = lstrFolder(llngIndex)
            lobjFolder.PathWithoutRoot = lobjFolder.Path.Substring(lintRootLen)
            lstrResult = FetchFolderContents(lobjFolder, strRoot)
            If lstrResult <> "" Then
                lstrReturn = lstrReturn & lstrResult & vbCrLf
            End If
            llngTotalSize = llngTotalSize + lobjFolder.Size
            llngTotalFiles = llngTotalFiles + lobjFolder.TotalFiles
            objFolder.AddFolder(lobjFolder)
        Next

        objFolder.Size = llngTotalSize
        objFolder.TotalFiles = llngTotalFiles

        FetchFolderContents = lstrReturn

    End Function

    Private Function ErrorLogFile() As String
        ErrorLogFile = Application.StartupPath & "\ErrorLog_" & Format(Now, "yyyy-MM-dd") & ".txt"
    End Function

    Private Sub PopulateJobsList(objJobs As clsJobs)

        Dim lintIndex As Integer

        With lstJobs
            .Clear()

            .View = View.Details
            .GridLines = True
            .HeaderStyle = ColumnHeaderStyle.Nonclickable

            .Columns.Add("Job Name", 100)
            .Columns.Add("Enabled", 100)
            .Columns.Add("Status", 100)
            .Columns.Add("Next Run", 100)
            .Columns.Add("Last Succesful Run", 100)
            .Columns.Add("Last Run", 100)
            .Columns.Add("Last Run Errors", 100)
            .Columns.Add("Last Run Duration", 100)

        End With

        Dim strData() As String
        ReDim strData(lstJobs.Columns.Count - 1)
        Dim itmRow As ListViewItem

        With objJobs

            For lintIndex = 1 To .Count
                With .Job(lintIndex)
                    strData(0) = .Name
                    strData(1) = .Enabled.ToString()
                    If .CurrentlyRunning = True Then
                        strData(2) = "Running"
                    Else
                        strData(2) = "Stopped"
                    End If
                    strData(3) = .NextRun
                    strData(4) = .LastSuccessfulRun
                    strData(5) = .LastRun
                    strData(6) = CStr(.LastRunErrors)
                    strData(7) = CStr(.LastRunDuration) & " seconds"

                    itmRow = New ListViewItem(strData)
                    lstJobs.Items.Add(itmRow)
                End With

            Next

        End With
        ResizeScreen()

    End Sub

    Private Sub tblJobList_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub tblJobList_MouseClick(sender As Object, e As MouseEventArgs)

    End Sub

    Private Function GetRequiredActions(objSource As clsFolder, objDestination As clsFolder) As clsFolder

        Dim lobjRequiredActions As clsFolder = New clsFolder

    End Function



    Private Sub cmdNewJob_Click(sender As Object, e As EventArgs) Handles cmdNewJob.Click

        Dim lobjJob As New clsJob
        Dim lobjFAC As New clsFAC


        frmJob.ShowJob(lobjJob)

        If lobjJob Is Nothing Then
            Exit Sub
        End If

        If lobjJob.Name = Nothing Then
            'Cancel was clicked. Don't do anything
        Else
            mobjJobs.AddJob(lobjJob)
            lobjFAC.StoreJobs(mobjJobs)
            PopulateJobsList(mobjJobs)
        End If

    End Sub

    Private Sub frmFileBackup_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim lobjFAC As New clsFAC

        Me.Text = "File Backup - " & My.Application.Info.Version.ToString()

        txtProgress.Visible = False
        lblProgress.Visible = False
        txtCompletionTime.Visible = txtProgress.Visible
        txtCompletionTime.Text = ""

        mobjJobs = lobjFAC.FetchJobs()
        mobjJobs.CalculateNextRun()

        If mobjJobs Is Nothing Then
            mobjJobs = New clsJobs
        Else
            PopulateJobsList(mobjJobs)
        End If
        Me.Width = Me.Width + 1

        With tmrTimer
            .Interval = 2000
            .Enabled = True
        End With
        mblnFirstTenSeconds = True
        mdtmStartTime = Now()
        chkShowActivity.Checked = True
        cmdAbortBackup.Visible = False

        ResizeScreen()



    End Sub

    Private Sub frmFileBackup_Resize(sender As Object, e As EventArgs) Handles Me.Resize

        ResizeScreen()

    End Sub

    Private Sub ResizeScreen()

        Try

            With lstJobs
                .Width = Me.Width - 65

                If chkShowActivity.Checked = False Then
                    .Height = Me.Height - .Top - chkPause.Height * 2 - 50
                Else
                    .Height = Me.Height * 0.4
                End If

                If .Columns.Count > 5 Then
                    .Columns(0).Width = .Width * 0.15
                    .Columns(1).Width = .Width * 0.07
                    .Columns(2).Width = .Width * 0.07
                    .Columns(3).Width = .Width * 0.15
                    .Columns(4).Width = .Width * 0.15
                    .Columns(5).Width = .Width * 0.15
                    .Columns(6).Width = .Width * 0.1
                    .Columns(7).Width = .Width * 0.1
                End If

                cmdNewJob.Left = .Left + .Width - cmdNewJob.Width
                cmdDeleteJob.Left = cmdNewJob.Left - cmdDeleteJob.Width - 10
                chkShowActivity.Top = chkPause.Top
                cmdDuplicate.Top = cmdDeleteJob.Top
                cmdDuplicate.Left = cmdDeleteJob.Left - cmdDuplicate.Width - 10

                cmdRunSelectedBackup.Top = chkShowActivity.Top - 5
                cmdRunSelectedBackup.Left = .Left + .Width - cmdRunSelectedBackup.Width
                cmdAbortBackup.Top = cmdRunSelectedBackup.Top
                cmdAbortBackup.Left = cmdRunSelectedBackup.Left

                chkPause.Top = .Top + .Height + 10
                chkPause.Left = cmdRunSelectedBackup.Left - chkPause.Width - 15

                chkSuppressDelete.Top = chkShowActivity.Top
                chkSuppressDelete.Left = chkPause.Left - chkSuppressDelete.Width - 15
                cmdClearActivity.Top = cmdRunSelectedBackup.Top

                txtProgress.Top = chkShowActivity.Top
                lblProgress.Top = chkShowActivity.Top + 2

                If (chkSuppressDelete.Left - (txtProgress.Left + txtProgress.Width)) > (txtCompletionTime.Width + 20) Then
                    txtCompletionTime.Left = txtProgress.Left + txtProgress.Width + 10
                    txtCompletionTime.Top = txtProgress.Top
                End If
                txtCompletionTime.Visible = txtProgress.Visible

                If chkShowActivity.Checked = True Then
                    lstActivity.Top = .Top + .Height + chkShowActivity.Height + 20
                    lstActivity.Height = Me.Height - lstActivity.Top - 50
                    lstActivity.Width = .Width
                    lstActivity.Visible = True
                    cmdClearActivity.Visible = True

                Else
                    lstActivity.Visible = False
                    cmdClearActivity.Visible = False
                End If

            End With
        Catch
            'Don't care if there is an error re-sizing - probably cause by form minimisation
        End Try


    End Sub

    Private Sub chkPause_CheckedChanged(sender As Object, e As EventArgs) Handles chkPause.CheckedChanged
        If chkPause.Checked = True Then
            lstJobs.BackColor = Color.Orange
        Else
            lstJobs.BackColor = Color.White
        End If
    End Sub

    Private Sub tmrTimer_Tick(sender As Object, e As EventArgs) Handles tmrTimer.Tick

        If mblnFirstTenSeconds = True Then
            If DateDiff("s", mdtmStartTime, Now()) > 0 Then
                mblnFirstTenSeconds = False
            End If
        Else
            If chkPause.Checked = False Then
                CheckJobs()
            End If
        End If

    End Sub

    Private Sub CheckJobs()

        Dim llngIndex As Long
        Dim lobjFAC As New clsFAC
        Dim lblnJobRun As Boolean
        Dim lstrResult As String
        Dim ldtmStartTime As Date

        lblnJobRun = False
        With mobjJobs
            For llngIndex = 1 To .Count

                With .Job(llngIndex)
                    If (DateDiff("s", .NextRun, Now()) > 0) And (.Enabled = True) Then
                        cmdRunSelectedBackup.Enabled = False
                        If .CurrentlyRunning = False Then   'Only run if not already running

                            .CurrentlyRunning = True
                            PopulateJobsList(mobjJobs)
                            .DeriveLogFileName()
                            ldtmStartTime = Now()
                            lstrResult = SynchroniseFolders(.SourceFolder, .DestinationFolder, .LogFileName)
                            If lstrResult = "" Then
                                .LastRunErrors = False
                                .LastSuccessfulRun = Now()
                            Else
                                .LastRunErrors = True
                            End If
                            .LastRun = Now()
                            .LastRunDuration = DateDiff("s", ldtmStartTime, Now())
                            .CalculateNextRun()
                            lblnJobRun = True
                            .CurrentlyRunning = False
                            PopulateJobsList(mobjJobs)
                        End If
                    End If
                End With
            Next

        End With

        'A job has been run, so make sure we save the next run time and other details
        If lblnJobRun = True Then
            lobjFAC.StoreJobs(mobjJobs)
        End If

        cmdRunSelectedBackup.Enabled = True

    End Sub

    Private Sub chkShowActivity_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowActivity.CheckedChanged

        ResizeScreen()

    End Sub

    Private Sub lstJobs_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles lstJobs.MouseDoubleClick
        Dim lobjJob As clsJob
        Dim llngIndex As Long
        Dim lobjFAC As New clsFAC

        If mblnNoChange = True Then
            'Do nothing - this has been triggered by a save and refresh
            mblnNoChange = False

        Else

            If (lstJobs.SelectedIndices.Count <= 0) Then
                Exit Sub
            Else

                llngIndex = lstJobs.SelectedItems(0).Index + 1
                If (llngIndex >= 1) Then

                    lobjJob = mobjJobs.Job(llngIndex)
                    frmJob.ShowJob(lobjJob)

                    If lobjJob Is Nothing Then
                        'Cancel has been pressed
                    Else
                        lobjJob.CalculateNextRun()
                        mobjJobs.Job(llngIndex) = lobjJob
                        lobjFAC.StoreJobs(mobjJobs)
                        mblnNoChange = True
                        PopulateJobsList(mobjJobs)
                    End If


                End If
            End If
        End If

    End Sub

    Private Sub cmdRunSelectedBackup_Click(sender As Object, e As EventArgs) Handles cmdRunSelectedBackup.Click
        Dim lobjJob As clsJob
        Dim llngIndex As Long
        Dim lobjFAC As New clsFAC
        Dim llngResult As Long
        Dim ldtmStartTime As Date
        Dim lstrResult As String

        If (lstJobs.SelectedIndices.Count <= 0) Then
            Exit Sub
        Else
            cmdRunSelectedBackup.Enabled = False
            llngIndex = lstJobs.SelectedItems(0).Index + 1
            If (llngIndex >= 1) Then

                lobjJob = mobjJobs.Job(llngIndex)
                With lobjJob
                    llngResult = MsgBox("Are you sure you want to run backup job: " & .Name & " ?", vbYesNo, "Run Backup Job?")

                    If llngResult = vbYes Then
                        .CurrentlyRunning = True

                        PopulateJobsList(mobjJobs)
                        .DeriveLogFileName()
                        ldtmStartTime = Now()
                        txtProgress.Text = "0%"
                        lstrResult = SynchroniseFolders(.SourceFolder, .DestinationFolder, .LogFileName)
                        If lstrResult = "" Then
                            .LastRunErrors = False
                            .LastSuccessfulRun = Now()
                        Else
                            .LastRunErrors = True
                        End If
                        .LastRun = Now()
                        .LastRunDuration = DateDiff("s", ldtmStartTime, Now())
                        .CalculateNextRun()

                        lobjFAC.StoreJobs(mobjJobs)
                        .CurrentlyRunning = False
                        PopulateJobsList(mobjJobs)
                    End If
                End With

            End If
        End If

        cmdRunSelectedBackup.Enabled = True

    End Sub

    Private Sub cmdClearActivity_Click(sender As Object, e As EventArgs) Handles cmdClearActivity.Click

        lstActivity.Items.Clear()

    End Sub

    Private Sub cmdDeleteJob_Click(sender As Object, e As EventArgs) Handles cmdDeleteJob.Click
        Dim lobjJob As clsJob
        Dim llngIndex As Long
        Dim lobjFAC As New clsFAC
        Dim llngResult As Long

        If (lstJobs.SelectedIndices.Count <= 0) Then
            Exit Sub
        Else
            llngIndex = lstJobs.SelectedItems(0).Index + 1
            If (llngIndex >= 1) Then

                lobjJob = mobjJobs.Job(llngIndex)
                With lobjJob
                    llngResult = MsgBox("Are you sure you want to delete the backup job: " & .Name & " ?", vbYesNo, "Delete Backup Job?")

                    If llngResult = vbYes Then
                        lobjJob = Nothing
                        mobjJobs.DeleteJob(llngIndex)
                        lobjFAC.StoreJobs(mobjJobs)
                        PopulateJobsList(mobjJobs)
                    End If
                End With

            End If
        End If

    End Sub

    Private Sub FileAgeToolToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileAgeToolToolStripMenuItem.Click
        frmAgeList.Show()
    End Sub

    Private Sub cmdAbortBackup_Click(sender As Object, e As EventArgs) Handles cmdAbortBackup.Click
        mblnAbort = True
    End Sub

    Private Sub cmdDuplicate_Click(sender As Object, e As EventArgs) Handles cmdDuplicate.Click

        Dim lobjJob As clsJob
        Dim lobjDuplicateJob As clsJob
        Dim llngIndex As Long
        Dim lobjFAC As New clsFAC
        Dim llngResult As Long

        If (lstJobs.SelectedIndices.Count <= 0) Then
            Exit Sub
        Else
            llngIndex = lstJobs.SelectedItems(0).Index + 1
            If (llngIndex >= 1) Then

                lobjJob = mobjJobs.Job(llngIndex)
                With lobjJob
                    llngResult = MsgBox("Are you sure you want to duplicate the backup job: " & .Name & " ?", vbYesNo, "Duplicate Backup Job?")

                    If llngResult = vbYes Then
                        lobjDuplicateJob = New clsJob
                        lobjDuplicateJob = lobjJob.Clone()
                        lobjDuplicateJob.Name = lobjDuplicateJob.Name & "_" & CStr(Microsoft.VisualBasic.Timer * 100)
                        mobjJobs.AddJob(lobjDuplicateJob)
                        lobjFAC.StoreJobs(mobjJobs)
                        PopulateJobsList(mobjJobs)
                    End If
                End With

            End If
        End If


    End Sub
End Class
