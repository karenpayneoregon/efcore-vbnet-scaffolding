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
    <TestMethod>
    <TestTraits(Trait.JsonValidation)>
    Sub CheckConnectionString()

        Dim connectionString = BuildConnection()

        Assert.AreEqual(connectionString, ConextConnectionString)

    End Sub

End Class