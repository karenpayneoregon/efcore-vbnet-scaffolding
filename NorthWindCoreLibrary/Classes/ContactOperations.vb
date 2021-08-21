Imports Microsoft.EntityFrameworkCore
Imports NorthWindCoreLibrary.Containers
Imports NorthWindCoreLibrary.Data
Imports NorthWindCoreLibrary.Models

Namespace Classes

    Public Class ContactOperations
        Public Shared Function GetContactsWithProjection() As List(Of ContactItem)
            Using context As New NorthWindContext
                Return context.Contacts.Select(ContactItem.Projection).ToList()
            End Using
        End Function
        Public Shared Function List() As List(Of Contact)
            Using context As New NorthWindContext
                Return context.Contacts.ToList()
            End Using
        End Function
        Public Shared Function ContactsGroupedByTitle() As List(Of IGrouping(Of Integer?, ContactItem))
            Return GetContactsWithProjection().
                GroupBy(Function(contactItem) contactItem.ContactTypeIdentifier).
                Select(Function(grouped) grouped).OrderBy(Function(contactItem) contactItem.FirstOrDefault().ContactTitle).
                ToList()
        End Function
        Public Shared Function ContactByIdentifier(identifier As Integer) As Contact
            Using context As New NorthWindContext
                '
                ' Most developers will use FirstOrDefault, for this operation Find is better
                '
                Return context.Contacts.Find(identifier) ' .FirstOrDefault(Function(contact) contact.ContactId = identifier)
            End Using
        End Function

        Public Shared Function UpdateContact(contact As Contact) As Boolean
            Using context As New NorthWindContext
                context.Entry(contact).State = EntityState.Modified
                Return context.SaveChanges() = 1
            End Using
        End Function
    End Class
End Namespace