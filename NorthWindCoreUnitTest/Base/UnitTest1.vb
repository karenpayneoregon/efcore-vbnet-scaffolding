Imports Microsoft.EntityFrameworkCore
Imports Microsoft.Extensions.Configuration
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports NorthWindCoreLibrary.Classes
Imports NorthWindCoreLibrary.Data
Imports NorthWindCoreLibrary.Models

' ReSharper disable once CheckNamespace - do not change
Partial Public Class UnitTest1

    <ClassInitialize()>
    Public Shared Sub ClassInitialize(ByVal testContext As TestContext)
        TestResults = New List(Of TestContext)()
    End Sub

    Public ContextConnectionString As String = "Data Source=.\SQLEXPRESS;Initial Catalog=NorthWind2020;Integrated Security=true"

    Public FirstCompanyName As String = CustomersOperations.CustomerProjection().FirstOrDefault().CompanyName

    Public Function CompanyNameStartsWith() As Integer

        Using context As New NorthWindContext

            Dim results As List(Of Customer) = context.Customers.Where(Function(customer) EF.Functions.Like(customer.CompanyName, "an%")).ToList()
            Return results.Count

        End Using

    End Function
    Public Function CompanyNameStartsWithQueryString() As String

        Using context As New NorthWindContext
            Return context.Customers.Where(Function(customer) EF.Functions.Like(customer.CompanyName, "an%")).ToQueryString()
        End Using

    End Function
    Public Function CompanyNameContains() As Integer

        Using context As New NorthWindContext
            Dim results As List(Of Customer) = context.Customers.Where(Function(customer) EF.Functions.Like(customer.CompanyName, "%S.A.")).ToList()
            Return results.Count
        End Using

    End Function
    Public Function CompanyNameEndWith() As Integer

        Using context As New NorthWindContext
            Dim results As List(Of Customer) = context.Customers.Where(Function(customer) EF.Functions.Like(customer.CompanyName, "%Comidas%")).ToList()
            Return results.Count
        End Using

    End Function

    Public ReadOnly Property CustomerGood() As Customer
        Get
            Return New Customer() With {.CompanyName = "Some Company", .ContactId = 1}
        End Get
    End Property

    Public ReadOnly Property CustomerBadNoContactIdentifier() As Customer
        Get
            Return New Customer() With {.CompanyName = "Some Company"}
        End Get
    End Property
    Public ReadOnly Property CustomerBadNoContactIdentifierNoCompanyName() As Customer
        Get
            Return New Customer()
        End Get
    End Property

    ''' <summary>
    ''' Simple join three models to Customer model using a DTO
    ''' </summary>
    ''' <returns></returns>
    Private Function JoinedCustomers() As List(Of CustomerEntity)

        Dim results As List(Of CustomerEntity)

        Using context = New NorthWindContext()
            results = (
                From customer In context.Customers
                Join contactType In context.ContactTypes On customer.ContactTypeIdentifier Equals contactType.ContactTypeIdentifier
                Join contact In context.Contacts On customer.ContactId Equals contact.ContactId
                Join country In context.Countries On customer.CountryIdentifier Equals country.CountryIdentifier
                Select New CustomerEntity With {
                        .CustomerIdentifier = customer.CustomerIdentifier,
                        .CompanyName = customer.CompanyName,
                        .ContactIdentifier = customer.ContactTypeIdentifier,
                        .FirstName = contact.FirstName,
                        .LastName = contact.LastName,
                        .ContactTypeIdentifier = contactType.ContactTypeIdentifier,
                        .ContactTitle = contactType.ContactTitle,
                        .Address = customer.Street,
                        .City = customer.City,
                        .PostalCode = customer.PostalCode,
                        .CountryIdentifier = customer.CountryIdentifier,
                        .CountyName = country.Name
                        }).ToList()

        End Using

        Return results

    End Function

    ''' <summary>
    ''' Replicate reading connection string as in NorthWindCoreLibrary DbContext
    ''' </summary>
    ''' <returns></returns>
    Private Shared Function BuildConnection() As String

        Dim configuration = (New ConfigurationBuilder()).AddJsonFile("appsettings.json", True, True).Build()

        Dim sections = configuration.GetSection("database").GetChildren().ToList()

        Return $"Data Source={sections(1).Value};Initial Catalog={sections(0).Value};Integrated Security={sections(2).Value}"

    End Function
End Class
