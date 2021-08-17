Imports System
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Namespace Models
    Public Partial Class ContactType
        Public Sub New()
            Contacts = New HashSet(Of Contact)()
            Customers = New HashSet(Of Customer)()
            Employees = New HashSet(Of Employee)()
        End Sub

        Public Property ContactTypeIdentifier As Integer
        Public Property ContactTitle As String

        Public Overridable Property Contacts As ICollection(Of Contact)
        Public Overridable Property Customers As ICollection(Of Customer)
        Public Overridable Property Employees As ICollection(Of Employee)
        Public Overrides Function ToString() As String
            Return ContactTitle
        End Function
    End Class
End Namespace
