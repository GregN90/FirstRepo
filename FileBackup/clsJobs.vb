Public Class clsJobs

    Dim mobjJob() As clsJob
    Dim mlngCount As Integer

    Public Sub AddJob(objJob As clsJob)

        mlngCount = mlngCount + 1

        ReDim Preserve mobjJob(mlngCount)

        mobjJob(mlngCount) = New clsJob
        mobjJob(mlngCount) = objJob
        objJob = Nothing

    End Sub

    Public Property Count
        Get
            Return mlngCount
        End Get
        Set(value)
            'Don't do anything
        End Set
    End Property

    Public Property Job(Index As Integer) As clsJob
        Get
            Return mobjJob(Index)
        End Get
        Set(value As clsJob)
            mobjJob(Index) = value
        End Set
    End Property

    Public Sub DeleteJob(Index As Integer)

        Dim lintIndex As Integer

        For lintIndex = Index To mlngCount - 1
            mobjJob(lintIndex) = mobjJob(lintIndex + 1)
            mobjJob(lintIndex + 1) = Nothing
        Next

        mlngCount = mlngCount - 1
        ReDim Preserve mobjJob(mlngCount)

    End Sub

    Public Sub CalculateNextRun()
        Dim llngIndex As Long

        For llngIndex = 1 To mlngCount
            mobjJob(llngIndex).CalculateNextRun()
        Next
    End Sub
End Class
