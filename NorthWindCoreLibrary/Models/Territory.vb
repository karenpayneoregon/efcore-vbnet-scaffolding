Imports System
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Namespace Models
    Public Partial Class Territory
        Public Sub New()
            EmployeeTerritories = New HashSet(Of EmployeeTerritory)()
        End Sub

        Public Property TerritoryId As String
        Public Property TerritoryDescription As String
        Public Property RegionId As Integer

        Public Overridable Property Region As Region
        Public Overridable Property EmployeeTerritories As ICollection(Of EmployeeTerritory)
    End Class
End Namespace
