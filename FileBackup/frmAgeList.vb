'Imports System.IO
Imports System.Runtime.InteropServices
Imports Delimon.Win32.IO

Public Class frmAgeList

    Dim objSourceFolder As clsFolder
    Dim mblnStop As Boolean
    Dim mlngCounter As Long
    Dim mlngDateOption As Long
    Dim llngSelectedTotal As Long
    Dim lcolNodes As New Collection
    Dim mcolIdentifiedFiles As New Collection

    Private Sub cmdSourceNavigate_Click(sender As Object, e As EventArgs) Handles cmdSourceNavigate.Click

        With dirFolderBrowser
            .SelectedPath = txtSourceFolder.Text
            .ShowDialog()
            txtSourceFolder.Text = .SelectedPath()
        End With

    End Sub

    Private Sub frmAgeList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblPleaseWait.Visible = False

        With cboDateParameter
            .Items.Add("Created Date")
            .Items.Add("Accessed Date")
            .Items.Add("Modified Date")
            .Items.Add("Filepath and name Length")
            .Items.Add("Path Length")
            .Text = .Items.Item(2)
        End With

        dtpDate.Value = DateAdd("yyyy", -1, Now())
        txtCurrentFolder.Enabled = False

        lblLonger.Top = lblOlder.Top
        lblLonger.Left = lblOlder.Left
        txtMaxPathLength.Top = dtpDate.Top
        txtMaxPathLength.Left = dtpDate.Left

        With cboComparison
            .Items.Add("Is older than")
            .Items.Add("Is newer than")
            .SelectedIndex = 0
        End With

        ResizeScreen()


    End Sub

    Private Sub cmdScanFolder_Click(sender As Object, e As EventArgs) Handles cmdScanFolder.Click

        FetchFolders()

    End Sub

    Private Sub FetchFolders()

        Dim ldtmDate As Date

        mblnStop = False
        objSourceFolder = New clsFolder

        ldtmDate = dtpDate.Value

        With objSourceFolder
            .Path = txtSourceFolder.Text
            .Name = txtSourceFolder.Text
        End With

        lblPleaseWait.Text = "Scanning...Please Wait"

        mcolIdentifiedFiles.Clear()

        treFiles.Nodes.Clear()
        lblPleaseWait.Visible = True
        Application.DoEvents()

        FetchFolderContents(objSourceFolder)
        lblPleaseWait.Text = "Preparing Tree...Please Wait"
        Application.DoEvents()

        mlngDateOption = cboDateParameter.SelectedIndex
        AddNode(treFiles.Nodes.Add("Root"), objSourceFolder, dtpDate.Value, 0, Val(txtMaxPathLength.Text))
        lblPleaseWait.Visible = False

    End Sub

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click

        Dim ldtmDate As Date

        If objSourceFolder Is Nothing Then
            MsgBox("No folder currently scanned. Scan folder first.", vbOKOnly, "No Folder Scanned")
            Exit Sub
        End If

        ldtmDate = dtpDate.Value

        mcolIdentifiedFiles.Clear()

        treFiles.Nodes.Clear()

        lblPleaseWait.Text = "Preparing Tree...Please Wait"
        Application.DoEvents()
        mlngDateOption = cboDateParameter.SelectedIndex

        AddNode(treFiles.Nodes.Add("Root"), objSourceFolder, dtpDate.Value, 0, Val(txtMaxPathLength.Text))
        lblPleaseWait.Visible = False

    End Sub

    Private Function AddNode(objParent As TreeNode, objFolder As clsFolder, Optional dtmCutOffDate As Date = Nothing, Optional ByRef lngSize As Long = 0, Optional MaxLength As Long = 9999) As Integer

        Dim lobjNewNode As New TreeNode
        Dim llngIndex As Long
        Dim llngState As Integer    '0=none exist, 1=All New, 2 = some old, 4 = all old
        Dim llngFileState As Integer
        Dim llngOldFolderCount As Long
        Dim llngFolderState As Long
        Dim llngOldCount As Long
        Dim lobjNewFile As New TreeNode
        Dim llngFileSize As Long

        Dim llngBackColour As Color
        Dim llngForeColour As Color
        Dim dtmCompareDate As Date
        Dim llngPathLength As Long
        Dim llngPathandNameLength As Long
        Dim llngChildFolderState As Long
        Dim lbytComparison As Byte

        mlngCounter = mlngCounter - 1
        If mlngCounter < 1 Then
            mlngCounter = 100
            txtCurrentFolder.Text = objFolder.Path
            Application.DoEvents()
        End If
        If mblnStop = True Then Exit Function

        If dtmCutOffDate = Nothing Then
            dtmCutOffDate = CDate("01-Jan-1900")
        End If

        lbytComparison = cboComparison.SelectedIndex '0=older than, 1=new than

        llngFileSize = 0
        lobjNewNode = objParent.Nodes.Add("X" & CStr(objFolder.ID), objFolder.Name & " (" & Format(objFolder.Size / 1048576, "#,#") & "MB) [Len=" & CStr(Len(objFolder.Path)) & "]")
        With objFolder
            llngFolderState = 1   'Default to good
            If .FolderCount > 0 Then
                For llngIndex = 1 To .FolderCount
                    llngChildFolderState = AddNode(lobjNewNode, .Folder(llngIndex), dtmCutOffDate, llngFileSize, MaxLength)
                    If llngChildFolderState >= 2 Then
                        llngOldFolderCount = llngOldFolderCount + 1
                    End If
                Next
                If llngOldFolderCount > 0 Then
                    If llngOldFolderCount = .FolderCount Then
                        llngFolderState = 3
                    Else
                        llngFolderState = 2
                    End If
                Else
                    If llngFolderState <> llngChildFolderState Then
                        llngFolderState = 2
                    End If
                End If

            Else
                llngFolderState = 1
            End If

            llngOldCount = 0
            If .FileCount > 0 Then
                For llngIndex = 1 To .FileCount
                    With .File(llngIndex)

                        Select Case mlngDateOption
                            Case 0
                                dtmCompareDate = .CreatedDate
                            Case 1
                                dtmCompareDate = .LastAccessedDate
                            Case 2, 3, 4
                                dtmCompareDate = .ModifiedDate
                        End Select
                        llngPathandNameLength = Len(.Path & .Name)
                        llngPathLength = Len(.Path)
                        lobjNewFile = lobjNewNode.Nodes.Add("X" & CStr(.ID), .Name & " : " & dtmCompareDate & " (" & Format(.Size, "#,#") & ") [Len=" & CStr(llngPathandNameLength) & "]")

                        If mlngDateOption = 3 Then  'Checking for path and filename length, rather than date
                            If llngPathandNameLength > MaxLength Then
                                llngOldCount = llngOldCount + 1
                                lobjNewFile.BackColor = Color.Red
                                lobjNewFile.ForeColor = Color.Yellow
                                mcolIdentifiedFiles.Add(.Path & .Name)
                            End If

                        ElseIf mlngDateOption = 4 Then  'Checking for path length (minus filename), rather than date
                            If llngPathLength > MaxLength Then
                                llngOldCount = llngOldCount + 1
                                lobjNewFile.BackColor = Color.Red
                                lobjNewFile.ForeColor = Color.Yellow
                                mcolIdentifiedFiles.Add(.Path & .Name)
                                Exit For 'No need to continue in this folder, since we have established the folder length exceeds the limit
                            End If


                        Else

                            If .ModifiedDate = Nothing Then
                                llngOldCount = llngOldCount + 1
                                lobjNewFile.BackColor = Color.Yellow
                            Else
                                'Check if we are looking for older or newer files (0 = older files, 1 = newer files)
                                If ((lbytComparison = 0) And (dtmCompareDate < dtmCutOffDate)) _
                                Or ((lbytComparison = 1) And (dtmCompareDate > dtmCutOffDate)) Then

                                    llngOldCount = llngOldCount + 1
                                    lobjNewFile.BackColor = Color.Red
                                    lobjNewFile.ForeColor = Color.Yellow
                                    mcolIdentifiedFiles.Add(.Path & "\" & .Name)
                                End If
                            End If
                        End If
                        llngFileSize = llngFileSize + .Size

                    End With
                Next

                If llngOldCount > 0 Then
                    If llngOldCount = .FileCount Then
                        llngFileState = 3
                    Else
                        llngFileState = 2
                    End If
                Else
                    llngFileState = 1
                End If
            Else
                llngFileState = 1
            End If

        End With

        If (llngFolderState = 0) And (llngFileState = 0) Then 'Unknown
            llngState = 3
        ElseIf llngFolderState = 0 Then
            llngState = llngFileState
        ElseIf llngFileState = 0 Then
            llngState = llngFolderState
        ElseIf llngFolderState = llngFileState Then
            llngState = llngFolderState
        Else
            llngState = 2
        End If

        Select Case llngState
            Case 1
                llngBackColour = Color.White
                llngForeColour = Color.Black
            Case 2
                llngBackColour = Color.Orange
                llngForeColour = Color.Black
            Case 3
                llngBackColour = Color.Red
                llngForeColour = Color.Yellow
        End Select

        With lobjNewNode
            .BackColor = llngBackColour
            .ForeColor = llngForeColour
        End With
        'lobjNewNode.Text = lobjNewNode.Text & " (" & CStr(llngFileSize) & ")"

        AddNode = llngState


    End Function

    Sub FetchFolderContents(ByRef objFolder As clsFolder, Optional strRoot As String = "", Optional ByRef ID As Long = 1, Optional ByRef lngSize As Long = 0)

        Dim llngIndex As Long
        Dim lstrFile As String()
        Dim lstrFolder As String()
        Dim lstrCurrentFile As String
        Dim lobjFile As clsFile = New clsFile
        Dim lobjFolder As clsFolder = New clsFolder
        Dim lintPathLen As Integer
        Dim lintRootLen As Integer
        Dim lstrData() As String
        Dim llngFolderSize As Long
        Dim llngAccumulatedFolderSize As Long
        Dim llngFileSize As Long
        Dim llngPos As Long

        Dim lobjLongFile As New clsLongFIle

        mlngCounter = mlngCounter - 1
        If mlngCounter < 1 Then
            mlngCounter = 100
            txtCurrentFolder.Text = objFolder.Path
            Application.DoEvents()
        End If
        If mblnStop = True Then Exit Sub

        Try
            llngPos = 1
            If Strings.Right(objFolder.Path, 1) = "\" Then
                objFolder.Path = Strings.Left(objFolder.Path, objFolder.Path.Length)
            End If

            llngPos = 2
            If strRoot = "" Then
                strRoot = objFolder.Path
            End If

            llngPos = 3
            lintRootLen = Len(strRoot)
            lintPathLen = objFolder.Path.Length() + 1

            llngPos = 4
            'If LCase(objFolder.Name) = "appcompat" Then Stop
            Try
                lstrFile = Directory.GetFiles(objFolder.Path)
            Catch
                Select Case Err.Number
                    Case 5  'Permission denied
                        objFolder.AccessDenied = True
                        Return
                    Case Else
                        Err.Raise(Err.Number)
                End Select
            End Try

            llngPos = 5
            llngFileSize = 0
            For llngIndex = lstrFile.GetLowerBound(0) To lstrFile.GetUpperBound(0)
                lstrCurrentFile = lstrFile(llngIndex)
                llngPos = 6
                With lobjFile
                    .Name = Mid(lstrCurrentFile, lintPathLen)
                    .Path = objFolder.Path
                    llngPos = 61
                    .PathWithoutRoot = .Path.Substring(lintRootLen)
                    llngPos = 62
                    Try


                        'lobjLongFile.GetFileInfo(lstrCurrentFile, .CreatedDate, .LastAccessedDate, .ModifiedDate)
                        .CreatedDate = File.GetCreationTimeUtc(lstrCurrentFile)
                        llngPos = 63
                        .ModifiedDate = File.GetLastWriteTimeUtc(lstrCurrentFile)
                        llngPos = 64
                        .LastAccessedDate = File.GetLastAccessTime(lstrCurrentFile)
                        llngPos = 65
                        .Size = FileLen(lstrCurrentFile)
                        llngFileSize = llngFileSize + .Size
                    Catch
                        'Filelength probably too long
                        .Unknown = True
                    End Try

                    .ID = ID
                    ID = ID + 1
                End With

                llngPos = 7
                objFolder.AddFile(lobjFile)
                lobjFile = Nothing
                lobjFile = New clsFile

            Next

            llngPos = 8
            llngAccumulatedFolderSize = 0
            lstrFolder = Directory.GetDirectories(objFolder.Path)
            For llngIndex = lstrFolder.GetLowerBound(0) To lstrFolder.GetUpperBound(0)
                llngPos = 9
                lobjFolder = New clsFolder
                lobjFolder.Path = lstrFolder(llngIndex)
                llngPos = 10
                lobjFolder.PathWithoutRoot = lobjFolder.Path.Substring(lintRootLen)
                llngPos = 11
                lstrData = Split(lobjFolder.Path, "\")
                lobjFolder.Name = lstrData(lstrData.GetUpperBound(0))
                lobjFolder.ID = ID
                ID = ID + 1
                llngPos = 12
                FetchFolderContents(lobjFolder, strRoot, ID, llngFolderSize)
                llngPos = 13
                llngAccumulatedFolderSize = llngAccumulatedFolderSize + llngFolderSize
                objFolder.AddFolder(lobjFolder)

            Next

            objFolder.Size = llngFileSize + llngAccumulatedFolderSize
            lngSize = objFolder.Size
            'If lngSize = 0 Then Stop
        Catch
            MsgBox("Error: " & Err.Description & " at position " & CStr(llngPos) & vbCrLf _
                    & objFolder.Path)
        End Try

    End Sub

    Private Sub frmAgeList_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ResizeScreen()
    End Sub

    Private Sub ResizeScreen()
        treFiles.Width = Me.Width - 55
        treFiles.Height = Me.Height - 180
        cmdLarger.Top = treFiles.Top + treFiles.Height + 5
        cmdLarger.Left = treFiles.Left + treFiles.Width - cmdLarger.Width
        cmdSmaller.Left = cmdLarger.Left - cmdSmaller.Width - 5
        cmdSmaller.Top = cmdLarger.Top
        cmdStop.Top = cmdLarger.Top
        cmdStop.Left = cmdSmaller.Left - cmdStop.Width - 5
        txtCurrentFolder.Top = cmdLarger.Top
        txtCurrentFolder.Width = treFiles.Width - 150
        txtSourceFolder.Width = Me.Width - lblSourceFolder.Width - cmdSourceNavigate.Width - 75
        cmdSourceNavigate.Left = treFiles.Left + treFiles.Width - cmdSourceNavigate.Width
        cmdScanFolder.Left = treFiles.Left + treFiles.Width - cmdScanFolder.Width
        cmdUpdate.Left = cmdScanFolder.Left - cmdUpdate.Width - 10
        txtMaxPathLength.Left = lblLonger.Left + lblLonger.Width
    End Sub

    Private Sub cmdLarger_Click(sender As Object, e As EventArgs) Handles cmdLarger.Click
        treFiles.Font = New Font(treFiles.Font.Name, treFiles.Font.Size + 1, FontStyle.Regular)
    End Sub

    Private Sub cmdSmaller_Click(sender As Object, e As EventArgs) Handles cmdSmaller.Click
        treFiles.Font = New Font(treFiles.Font.Name, treFiles.Font.Size - 1, FontStyle.Regular)
    End Sub

    Private Sub cmdStop_Click(sender As Object, e As EventArgs) Handles cmdStop.Click
        mblnStop = True
    End Sub

    Private Sub treFiles_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles treFiles.AfterSelect
        'Dirty hack, cos tree view cannot handle multiple selects

        Dim BackCol As Color

        If My.Computer.Keyboard.CtrlKeyDown Then
            Try
                lcolNodes.Add(treFiles.SelectedNode.Name, treFiles.SelectedNode.Name)
                BackCol = treFiles.SelectedNode.BackColor
                treFiles.SelectedNode.BackColor = treFiles.SelectedNode.ForeColor
                treFiles.SelectedNode.ForeColor = BackCol

                llngSelectedTotal = llngSelectedTotal + GetSize(treFiles.SelectedNode.Text)
                txtSelectedTotal.Text = llngSelectedTotal
            Catch
                'Only enters here if the node already selected, so ignore
            End Try
        End If

    End Sub

    Private Function GetSize(strData As String) As Long
        Dim llngStart As Long

        llngStart = strData.IndexOf("(") + 1
        GetSize = Val(Replace(strData.Substring(llngStart), ",", ""))

    End Function

    Private Sub cmdClear_Click(sender As Object, e As EventArgs) Handles cmdClear.Click
        Dim lstrNode As String
        Dim BackCol As Color

        'For Each lstrNode In lcolNodes
        'BackCol = treFiles.Nodes(lstrNode).BackColor
        'treFiles.findnode(lstrNode).BackColor = treFiles.Nodes(lstrNode).ForeColor
        'treFiles.Nodes(lstrNode).ForeColor = BackCol
        '
        'llngSelectedTotal = 0
        ' txtSelectedTotal.Text = llngSelectedTotal
        ' Next
    End Sub

    Private Sub cboDateParameter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDateParameter.SelectedIndexChanged

        If cboDateParameter.SelectedIndex >= 3 Then
            lblLonger.Visible = True
            txtMaxPathLength.Visible = True
        Else
            lblLonger.Visible = False
            txtMaxPathLength.Visible = False
        End If

        lblOlder.Visible = Not lblLonger.Visible
        dtpDate.Visible = Not lblLonger.Visible
        cboComparison.Visible = Not lblLonger.Visible

    End Sub

    Private Sub cmdShowIdentifiedFiles_Click(sender As Object, e As EventArgs) Handles cmdShowIdentifiedFiles.Click
        frmFileList.ShowList(mcolIdentifiedFiles)
    End Sub


End Class