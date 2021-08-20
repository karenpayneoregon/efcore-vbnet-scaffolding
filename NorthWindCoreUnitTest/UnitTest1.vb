Imports System.ComponentModel
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports NorthWindCoreLibrary
Imports NorthWindCoreLibrary.Classes
Imports NorthWindCoreLibrary.Containers
Imports NorthWindCoreLibrary.Data
Imports NorthWindCoreLibrary.Models
Imports NorthWindVisualBasicCore.Base
Imports WinFormValidationLibrary.LanguageExtensions
Imports WinFormValidationLibrary.Validators

<TestClass>
Partial Public Class UnitTest1
    Inherits TestBase

    <TestMethod>
    <TestTraits(Trait.EfCoreCustomersSelect)>
    Sub LoadCustomers()

        Dim customers As List(Of CustomerItem) = CustomersOperations.CustomerProjection()

        Assert.AreEqual(customers.Count, 91)
        Assert.IsTrue(customers.FirstOrDefault().CompanyName = "Alfreds Futterkiste")

    End Sub

    <TestMethod>
    <TestTraits(Trait.QueryStringInspection)>
    Sub QueryString()
        Dim expected =
                "SELECT [c].[CustomerIdentifier], [c].[City], [c].[CompanyName], [c].[ContactId], [c].[ContactTypeIdentifier], [c].[CountryIdentifier], [c].[Fax], [c].[ModifiedDate], [c].[Phone], [c].[PostalCode], [c].[Region], [c].[Street]
FROM [Customers] AS [c]
WHERE [c].[CompanyName] LIKE N'an%'"

        Assert.AreEqual(CompanyNameStartsWithQueryString(), expected)

    End Sub

    <TestMethod>
    <TestTraits(Trait.EfCoreCustomersSelectLocal)>
    Sub LoadCustomersLocal()

        Using context = New NorthWindContext()

            Dim customers As BindingList(Of Customer) = context.Customers.Local.ToBindingList()

            Dim customer = CustomerGood
            customers.Add(customer)

            Assert.IsTrue(context.Entry(customer).State = EntityState.Added)

            customer.CompanyName = FirstCompanyName
            customer.CustomerIdentifier = 200
            context.Entry(customer).State = EntityState.Modified

            Assert.IsTrue(context.Entry(customer).State = EntityState.Modified)

        End Using

    End Sub
    <TestMethod>
    <TestTraits(Trait.EfCoreCustomersSelect)>
    Sub LoadCustomersPredefinedIncludes()

        Dim customers As List(Of Customer) = CustomersOperations.CustomerIncludes()

        Assert.AreEqual(customers.Count, 91)
        Assert.IsTrue(customers.FirstOrDefault().CompanyName = "Alfreds Futterkiste")

    End Sub

    <TestMethod>
    <TestTraits(Trait.EfCoreContactGroup)>
    Sub LoadContactsGrouping()

        Dim groupedByTitle As List(Of IGrouping(Of Integer?, ContactItem)) = ContactOperations.ContactsGroupedByTitle()

        For Each contactsGrouped In groupedByTitle
            Debug.WriteLine($"{contactsGrouped.FirstOrDefault().ContactTitle} - {contactsGrouped.Count()}")
            For Each contactItem In contactsGrouped.OrderBy(Function(item) item.LastName)
                Debug.WriteLine($"{vbTab}{contactItem.FullName}")
            Next
        Next

    End Sub

    <TestMethod>
    <TestTraits(Trait.EfCoreLikeStartsWith)>
    Sub CompanyNameStartsWithTest()
        Assert.AreEqual(CompanyNameStartsWith(), 2)
    End Sub

    <TestMethod>
    <TestTraits(Trait.EfCoreLikeStartsWith)>
    Sub CompanyNameContainsTest()
        Assert.AreEqual(CompanyNameEndWith(), 3)
    End Sub

    <TestMethod>
    <TestTraits(Trait.EfCoreLikeStartsWith)>
    Sub CompanyNameEndsWithTest()
        Assert.AreEqual(CompanyNameEndWith(), 3)
    End Sub


    <TestMethod>
    <TestTraits(Trait.EfCoreContactUpdate)>
    Sub UpdateContact()

        Dim identifier = 2

        Dim contact = ContactOperations.ContactByIdentifier(identifier)
        Dim originalFirstName = contact.FirstName

        contact.FirstName = contact.FirstName & "1"
        Assert.IsTrue(ContactOperations.UpdateContact(contact))

        contact.FirstName = originalFirstName
        Assert.IsTrue(ContactOperations.UpdateContact(contact))

        contact = ContactOperations.ContactByIdentifier(identifier)
        Assert.IsTrue(contact.FirstName = originalFirstName)

    End Sub

    <TestMethod>
    <TestTraits(Trait.EFCoreOrderGroup)>
    Public Async Function EmployeeMostOrders() As Task

        Dim results As IEnumerable(Of IEnumerable(Of Employee)) = Await OrderOperations.EmployeeMostOrders_Linq()

        Dim firstItem As IEnumerable(Of Employee) = results.FirstOrDefault()

        ' ReSharper disable once PossibleMultipleEnumeration
        Dim emp = firstItem.FirstOrDefault()

        ' ReSharper disable once PossibleMultipleEnumeration
        Assert.IsTrue(emp.FirstName = "Margaret" AndAlso emp.LastName = "Peacock" AndAlso firstItem.Count() = 156)

    End Function

    ''' <summary>
    ''' No test, warm-up EF Core
    ''' This will run as the first test
    ''' </summary>
    ''' <returns></returns>
    <TestMethod>
    <TestTraits(Trait.WarmupEntityFramework)>
    Public Async Function A_Warmup() As Task

        Using context = New NorthWindContext
            Dim dummy = Await context.Customers.CountAsync()
        End Using

    End Function

    <TestMethod>
    <TestTraits(Trait.Validating)>
    Sub ValidateGoodCustomerTest()
        Dim validationResult As EntityValidationResult = ValidationHelper.ValidateEntity(CustomerGood)
        Assert.IsFalse(validationResult.HasError)
    End Sub

    <TestMethod>
    <TestTraits(Trait.Validating)>
    Sub ValidateBadCustomerNoContactTest()

        Dim validationResult As EntityValidationResult = ValidationHelper.ValidateEntity(CustomerBadNoContactIdentifier)

        Assert.IsTrue(validationResult.HasError)
        Assert.IsTrue(validationResult.ErrorMessageList().Contains("Contact Id is required"))

    End Sub
    <TestMethod>
    <TestTraits(Trait.Validating)>
    Sub ValidateBadCustomerNoContactNoCompanyNameTest()

        Dim validationResult As EntityValidationResult = ValidationHelper.ValidateEntity(CustomerBadNoContactIdentifierNoCompanyName)

        Assert.IsTrue(validationResult.HasError)

        Assert.IsTrue(
            validationResult.ErrorMessageList().Contains("Contact Id is required") AndAlso
            validationResult.ErrorMessageList().Contains("Company Name is required"))

    End Sub
    ''' <summary>
    ''' Runs as first test in the event the connection string is wrong we can inspect it
    ''' This is important on connection failure as we can first see if the connection string has
    ''' been read correctly. Failure most likely means SQLEXPRESS is not available which means
    ''' SQL-Server is not properly installed or a higher edition of SQL-Server is installed which
    ''' means a server name is needed to connect.
    ''' </summary>
    <TestMethod>
    <TestTraits(Trait.JsonValidation)>
    Sub A_CheckConnectionString()

        Dim connectionString = BuildConnection()

        Debug.WriteLine(ConextConnectionString)
        Assert.AreEqual(connectionString, ConextConnectionString)

    End Sub

    <TestMethod>
    <TestTraits(Trait.EFCoreLinqJoin)>
    Public Sub CustomerJoinTest()
        Dim results As List(Of CustomerEntity) = JoinedCustomers()
        Assert.IsTrue(results.Count = 91)
    End Sub

End Class