Namespace Models
    Partial Public Class Region
        Public Sub New()
            Territories = New HashSet(Of Territory)()
        End Sub

        Public Property RegionId As Integer
        Public Property RegionDescription As String

        Public Overridable Property Territories As ICollection(Of Territory)
    End Class
End Namespace
