Imports System
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Namespace Models
    Public Partial Class EmployeeTerritory
        Public Property EmployeeId As Integer
        Public Property TerritoryId As String

        Public Overridable Property Employee As Employee
        Public Overridable Property Territory As Territory
    End Class
End Namespace
