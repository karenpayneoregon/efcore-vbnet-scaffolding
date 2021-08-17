Imports System
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Namespace Models
    Public Partial Class Country
        Public Sub New()
            Customers = New HashSet(Of Customer)()
            Employees = New HashSet(Of Employee)()
            Suppliers = New HashSet(Of Supplier)()
        End Sub

        Public Property CountryIdentifier As Integer
        Public Property Name As String

        Public Overridable Property Customers As ICollection(Of Customer)
        Public Overridable Property Employees As ICollection(Of Employee)
        Public Overridable Property Suppliers As ICollection(Of Supplier)
    End Class
End Namespace
