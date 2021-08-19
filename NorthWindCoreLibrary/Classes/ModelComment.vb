
Namespace Classes
    Public Class ModelComment
        Private privateName As String
        Public Property Name() As String
            Get
                Return privateName
            End Get
            Friend Set(ByVal value As String)
                privateName = value
            End Set
        End Property
        Private privateComment As String
        Public Property Comment() As String
            Get
                Return privateComment
            End Get
            Friend Set(ByVal value As String)
                privateComment = value
            End Set
        End Property
        Public Overrides Function ToString() As String
            Return Name
        End Function

    End Class
End Namespace