Imports System.Linq.Expressions
Imports NorthWindCoreLibrary.Extensions
Imports NorthWindCoreLibrary.Models

Namespace Containers

    Public Class CustomerItem
        Public Property CustomerIdentifier() As Integer
        Public Property CompanyName() As String
        Public Property ContactId() As Integer?
        Public Property Street() As String
        Public Property City() As String
        Public Property PostalCode() As String
        Public Property CountryIdentifier() As Integer?
        Public Property Phone() As String
        Public Property ContactTypeIdentifier() As Integer?
        Public Property Country() As String
        Public Property FirstName() As String
        Public Property LastName() As String
        Public ReadOnly Property ContactName() As String
            Get
                Return $"{FirstName} {LastName}"
            End Get
        End Property
        Public Property ContactTitle() As String
        Public Property PhoneNumbers() As List(Of ContactDevice)
        Public Property LastChanged() As String
        Public Overrides Function ToString() As String
            Return CompanyName
        End Function

        Public Shared ReadOnly Property Projection() As Expression(Of Func(Of Customer, CustomerItem))
            Get
                Return Function(customers) New CustomerItem() With {
                        .CustomerIdentifier = customers.CustomerIdentifier,
                        .CompanyName = customers.CompanyName,
                        .ContactId = customers.ContactId,
                        .ContactTitle = customers.ContactTypeIdentifierNavigation.ContactTitle,
                        .FirstName = customers.Contact.FirstName,
                        .LastName = customers.Contact.LastName,
                        .CountryIdentifier = customers.CountryIdentifier,
                        .Country = customers.CountryIdentifierNavigation.Name,
                        .ContactTypeIdentifier = customers.CountryIdentifier,
                        .PhoneNumbers = customers.Contact.ContactDevices.ToList(),
                        .LastChanged = customers.ModifiedDate.Value.ZeroPad()
                    }
            End Get
        End Property

    End Class
End Namespace