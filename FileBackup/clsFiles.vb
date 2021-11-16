Public Class clsFiles

    Dim mobjFile() As clsFile
    Dim mlngCount As Long

    Public Sub AddFile(objFile As clsFile)

        mlngCount = mlngCount + 1
        ReDim Preserve mobjFile(mlngCount)
        mobjFile(mlngCount) = New clsFile
        mobjFile(mlngCount) = objFile

    End Sub

    Public Property Count() As Long
        Get
            Return mlngCount
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

        For llngPos = Index To mlngCount - 1
            mobjFile(llngPos) = mobjFile(llngPos + 1)
            mobjFile(llngPos + 1) = Nothing
        Next

        mlngCount = mlngCount - 1
        ReDim Preserve mobjFile(mlngCount)
    End Sub

End Class
