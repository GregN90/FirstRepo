Public Class clsFile

    Public Name As String
    Public Path As String
    Public PathWithoutRoot As String
    Dim ModDate As Date
    Public CreatedDate As Date
    Public LastAccessedDate As Date
    Public Size As Long
    Public RequiredAction As Integer
    Public FullName As String
    Public ID As Long
    Public Unknown As Boolean

    Public Property ModifiedDate() As Date
        Get
            ModifiedDate = ModDate
        End Get
        Set(value As Date)
            ModDate = value
            FullName = Path & "\" & Name & "|" & Format(ModDate, "yyyy-mm-dd hh:nn:ss")
        End Set
    End Property

End Class
