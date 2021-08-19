Imports System.Linq.Expressions
Imports NorthWindCoreLibrary_vb.Models

Namespace Containers

    Public Class ContactItem
        Public Property ContactId() As Integer
        Public Property FirstName() As String
        Public Property LastName() As String
        Public ReadOnly Property FullName() As String
            Get
                Return $"{FirstName} {LastName}"
            End Get
        End Property
        Public Property ContactTypeIdentifier() As Integer?
        Public Property ContactTitle() As String

        Public Overrides Function ToString() As String
            Return $"{ContactTitle} {FirstName} {LastName}"
        End Function


        Public Shared ReadOnly Property Projection() As Expression(Of Func(Of Contact, ContactItem))
            Get
                Return Function(contacts) New ContactItem() With {
                    .ContactId = contacts.ContactId,
                    .FirstName = contacts.FirstName,
                    .LastName = contacts.LastName,
                    .ContactTypeIdentifier = contacts.ContactTypeIdentifier,
                    .ContactTitle = contacts.ContactTypeIdentifierNavigation.ContactTitle
                    }
            End Get
        End Property
    End Class
End Namespace