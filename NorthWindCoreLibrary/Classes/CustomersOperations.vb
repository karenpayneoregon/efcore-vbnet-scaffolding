Imports Microsoft.EntityFrameworkCore
Imports NorthWindCoreLibrary_vb.Containers
Imports NorthWindCoreLibrary_vb.Data
Imports NorthWindCoreLibrary_vb.Extensions
Imports NorthWindCoreLibrary_vb.Models

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
        Public Shared Function CustomerIncludes() As List(Of Customer)

            Using context = New NorthWindContext()

                Return context.Customers.IncludeContactsDevicesCountry().ToList()

            End Using

        End Function

    End Class
End Namespace