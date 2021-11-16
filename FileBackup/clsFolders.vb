Public Class clsFolders
    Dim mobjFolder() As clsFolder
    Dim mlngCount As Long

    Public Sub AddFolder(objFolder As clsFolder)

        mlngCount = mlngCount + 1
        ReDim Preserve mobjFolder(mlngCount)
        mobjFolder(mlngCount) = New clsFolder
        mobjFolder(mlngCount) = objFolder

    End Sub

    Public Property Count() As Long
        Get
            Return mlngCount
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

        For llngPos = Index To mlngCount - 1
            mobjFolder(llngPos) = mobjFolder(llngPos + 1)
            mobjFolder(llngPos + 1) = Nothing
        Next

        mlngCount = mlngCount - 1
        ReDim Preserve mobjFolder(mlngCount)
    End Sub

End Class
