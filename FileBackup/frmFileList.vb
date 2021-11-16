Public Class frmFileList

    Dim mcolFiles As Collection

    Public Sub ShowList(colData As Collection)

        Const THE_MAX = 1000

        Dim llngIndex As Long
        Dim lstrData As String
        Dim llngMax As Long

        mcolFiles = colData

        'Limit display to 1000 rows
        llngMax = THE_MAX

        If colData.Count < llngMax Then
            llngMax = colData.Count
        End If

        lstrData = ""

        If llngMax = THE_MAX Then
            lstrData = "Truncated at " & CStr(THE_MAX) & vbCrLf
        End If

        With colData
            For llngIndex = 1 To llngMax
                lstrData = lstrData & .Item(llngIndex) & vbCrLf
            Next
            txtFileCount.Text = .Count
        End With

        txtList.Text = lstrData

        Me.ShowDialog()
        ResizeForm()

    End Sub


    Private Sub ResizeForm()
        txtList.Width = Me.Width - txtList.Left - 50
        txtList.Height = Me.Height - txtList.Top - 50
    End Sub

    Private Sub frmFileList_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ResizeForm()
    End Sub

    Private Sub cmdSaveFileList_Click(sender As Object, e As EventArgs) Handles cmdSaveFileList.Click
        CreateFileList(mcolFiles)
    End Sub

    Public Sub CreateFileList(colFiles As Collection)

        Dim lstrFile As String
        Dim llngIndex As Long

        lstrFile = Application.StartupPath & "\FileList_" & Format(Now, "yyyy-MM-dd HHmmss") & ".txt"

        If Not System.IO.File.Exists(lstrFile) Then
            System.IO.File.Create(lstrFile).Dispose()
        End If

        Try
            Using OutputFile = My.Computer.FileSystem.OpenTextFileWriter(lstrFile, True)
                OutputFile.WriteLine("Total Files = " & CStr(colFiles.Count))
                For llngIndex = 1 To colFiles.Count
                    OutputFile.WriteLine(colFiles.Item(llngIndex))
                Next

                OutputFile.Close()
            End Using
        Catch

        End Try

    End Sub


End Class