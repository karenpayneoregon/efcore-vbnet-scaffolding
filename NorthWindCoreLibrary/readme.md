# About

First run on EntityFrameworkCore.VisualBasic reverse engineering code first using Visual Studio 2019.

Microsoft [GiHub repository](https://github.com/efcore/EFCore.VisualBasic)

:small_blue_diamond: Scaffold command

```
 Scaffold-DbContext "Server=.\SQLEXPRESS;Database=NorthWind2020;Trusted_Connection=True;" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context NorthWind2020Context  -v -f  -project NorthWindCoreLibrary_vb -startupproject NorthWindCoreLibrary_vb -ContextDir Data -t "BusinessEntityPhone","Categories","ContactDevices","Contacts","ContactType","Countries","Customers","Employees","EmployeeTerritories","OrderDetails","Orders","PhoneType","Products","Region","Shippers","Suppliers","Territories"
```

:small_blue_diamond: [Data script](https://gist.github.com/karenpayneoregon/40a6e1158ff29819286a39b7f1ed1ae8) 

### Connection 

```vbnet
Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
    If Not optionsBuilder.IsConfigured Then
        optionsBuilder.UseSqlServer(BuildConnection())
    End If
End Sub

Private Shared Function BuildConnection() As String

    Dim configuration = (New ConfigurationBuilder()).AddJsonFile("appsettings.json", True, True).Build()

    Dim sections = configuration.GetSection("database").GetChildren().ToList()

    Return $"Data Source={sections(1).Value};Initial Catalog={sections(0).Value};Integrated Security={sections(2).Value}"

End Function
```

Read from `appsettings.json`

```json
{
  "database": {
    "DatabaseServer": ".\\SQLEXPRESS",
    "Catalog": "NorthWind2020",
    "IntegratedSecurity": "true",
    "UsingLogging": "true"
  }
}
```


# Reading Customer

```vbnet
Namespace Classes

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
```
</br>

```vbnet
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
```
</br>

## Test method

Run test in Debug, examine in local window

```vbnet
<TestClass>
Public Class UnitTest1
    <TestMethod>
    Sub LoadCustomers()
        Using context = New NorthWindContext
            Dim results As List(Of CustomerItem) = CustomersOperations.CustomerProjection()

            Assert.AreEqual(results.Count, 91)

        End Using
    End Sub
End Class
```