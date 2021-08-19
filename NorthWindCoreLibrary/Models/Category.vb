Namespace Models
    Partial Public Class Category
        Public Sub New()
            Products = New HashSet(Of Product)()
        End Sub

        ''' <summary>
        ''' Primary key
        ''' </summary>
        Public Property CategoryId As Integer
        Public Property CategoryName As String
        Public Property Description As String
        Public Property Picture As Byte()

        Public Overrides Function ToString() As String
            Return CategoryName
        End Function

        Public Overridable Property Products As ICollection(Of Product)
    End Class
End Namespace
