Imports Microsoft.EntityFrameworkCore
Imports NorthWindCoreLibrary_vb.Containers
Imports NorthWindCoreLibrary_vb.Data

Namespace Classes

    Public Class CustomersOperations

        Public Shared Function CustomerProjection() As List(Of CustomerItem)
            Using context = New NorthWindContext()

                Return context.Customers.
                    Include(Function(customer) customer.Contact).
                    ThenInclude(Function(contact) contact.ContactDevices).
                    ThenInclude(Function(contactDevice) contactDevice.PhoneTypeIdentifierNavigation).
                    Select(CustomerItem.Projection).
                    ToList()

            End Using
        End Function

    End Class
End Namespace