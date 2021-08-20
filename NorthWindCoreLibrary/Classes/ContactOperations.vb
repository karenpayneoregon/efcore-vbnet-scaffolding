Imports NorthWindCoreLibrary.Containers
Imports NorthWindCoreLibrary.Data


Namespace Classes

    Public Class ContactOperations
        Public Shared Function GetContactsWithProjection() As List(Of ContactItem)
            Using context As New NorthWindContext
                Return context.Contacts.Select(ContactItem.Projection).ToList()
            End Using
        End Function
        Public Shared Function ContactsGroupedByTitle() As List(Of IGrouping(Of Integer?, ContactItem))
            Return GetContactsWithProjection().
                GroupBy(Function(contactItem) contactItem.ContactTypeIdentifier).
                Select(Function(grouped) grouped).OrderBy(Function(contactItem) contactItem.FirstOrDefault().ContactTitle).
                ToList()
        End Function
    End Class
End Namespace