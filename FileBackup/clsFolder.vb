Public Class clsFolder
    Public Name As String
    Public Path As String
    Public PathWithoutRoot As String
    Public AccessDenied As Boolean = False
    Public RequiredAction As Integer
    Public Size As Long
    Public TotalFiles As Long

    'Public Root As String
    Public ID As Long
    Public NextID As Long

    Dim mobjFile() As clsFile
    Dim mobjFolder() As clsFolder
    Dim mlngFolderCount As Long
    Dim mlngFileCount As Long

    Public Sub AddFolder(objFolder As clsFolder)

        mlngFolderCount = mlngFolderCount + 1
        ReDim Preserve mobjFolder(mlngFolderCount)
        mobjFolder(mlngFolderCount) = New clsFolder
        mobjFolder(mlngFolderCount) = objFolder

    End Sub

    Public Function UpdateFile(objFile As clsFile) As Boolean

        Dim llngIndex As Long

        'Check if we're in the correct folder
        If objFile.Path = Path Then
            For llngIndex = 1 To mlngFileCount
                With mobjFile(llngIndex)
                    If .Name = objFile.Name Then
                        .ModifiedDate = objFile.ModifiedDate
                        Exit For
                    End If
                End With
            Next
            Return True 'Return true, regardless of the file being found, to avoid looping through other folders
        Else
            For llngIndex = 1 To mlngFolderCount
                If UpdateFile(objFile) = True Then
                    Return True
                    Exit For
                End If
            Next
        End If
    End Function

    Public Function FileList(blnNameOnly As Boolean, blnRelativePath As Boolean) As List(Of String)

        Dim ReturnList As New List(Of String)
        Dim lstrItem As String

        Dim llngIndex As Long

        '        If blnNameOnly = True Then
        '(Note: Full loop placed inside if, for performance reasons)
        For llngIndex = 1 To mlngFileCount
            With mobjFile(llngIndex)
                If blnRelativePath = True Then
                    If Strings.Right(.PathWithoutRoot, 1) = "\" Then
                        lstrItem = .PathWithoutRoot & .Name
                    Else
                        lstrItem = .PathWithoutRoot & "\" & .Name
                    End If

                Else
                    If Strings.Right(.Path, 1) = "\" Then
                        lstrItem = .Path & .Name
                    Else
                        lstrItem = .Path & "\" & .Name
                    End If
                End If
                If blnNameOnly = False Then
                    lstrItem = lstrItem & "|" & Format(.ModifiedDate, "yyyy-MM-dd HH:mm:ss")
                End If

                ReturnList.Add(lstrItem)

            End With
        Next

        'Else
        '    For llngIndex = 1 To mlngFileCount
        '        With mobjFile(llngIndex)
        '            ReturnList.Add(.Path & "\" & .Name & "|" & Format(.ModifiedDate, "yyyy-mm-dd hh:nn:ss"))
        '        End With
        '    Next
        'End If

        For llngIndex = 1 To mlngFolderCount
            ReturnList.AddRange(mobjFolder(llngIndex).FileList(blnNameOnly, blnRelativePath))
        Next

        Return ReturnList

    End Function

    Public Property FolderCount() As Long
        Get
            Return mlngFolderCount
        End Get

        Set(value As Long)

        End Set

    End Property

    Public Property Folder(Index As Long) As clsFolder
        Get
            Return mobjFolder(Index)
        End Get
        Set(value As clsFolder)
            mobjFolder(Index) = value
        End Set
    End Property

    Public Sub DeleteFolder(Index As Long)
        Dim llngPos As Long

        For llngPos = Index To mlngFolderCount - 1
            mobjFolder(llngPos) = mobjFolder(llngPos + 1)
            mobjFolder(llngPos + 1) = Nothing
        Next

        mlngFolderCount = mlngFolderCount - 1
        ReDim Preserve mobjFolder(mlngFolderCount)
    End Sub

    Public Sub AddFile(objFile As clsFile)

        mlngFileCount = mlngFileCount + 1
        ReDim Preserve mobjFile(mlngFileCount)
        mobjFile(mlngFileCount) = New clsFile
        mobjFile(mlngFileCount) = objFile

    End Sub

    Public Property FileCount() As Long
        Get
            Return mlngFileCount
        End Get

        Set(value As Long)

        End Set

    End Property

    Public Property File(Index As Long) As clsFile
        Get
            Return mobjFile(Index)
        End Get
        Set(value As clsFile)
            mobjFile(Index) = value
        End Set
    End Property

    Public Sub DeleteFile(Index As Long)
        Dim llngPos As Long

        For llngPos = Index To mlngFileCount - 1
            mobjFile(llngPos) = mobjFile(llngPos + 1)
            mobjFile(llngPos + 1) = Nothing
        Next

        mlngFileCount = mlngFileCount - 1
        ReDim Preserve mobjFile(mlngFileCount)
    End Sub
End Class
