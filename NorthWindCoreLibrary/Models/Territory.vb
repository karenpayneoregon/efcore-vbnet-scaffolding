
Namespace Models
    Public Partial Class Territory
        Public Sub New()
            EmployeeTerritories = New HashSet(Of EmployeeTerritory)()
        End Sub

        Public Property TerritoryId As String
        Public Property TerritoryDescription As String
        Public Property RegionId As Integer
        Public Overrides Function ToString() As String
            Return TerritoryDescription
        End Function
        Public Overridable Property Region As Region
        Public Overridable Property EmployeeTerritories As ICollection(Of EmployeeTerritory)
    End Class
End Namespace
