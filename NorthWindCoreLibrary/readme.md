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

## Read/update Contact

```vbnet
Public Shared Function ContactByIdentifier(identifier As Integer) As Contact
    Using context As New NorthWindContext
        Return context.Contacts.FirstOrDefault(Function(contact) contact.ContactId = identifier)
    End Using
End Function

Public Shared Function UpdateContact(contact As Contact) As Boolean
    Using context As New NorthWindContext
        context.Entry(contact).State = EntityState.Modified
        Return context.SaveChanges() = 1
    End Using
End Function
```

## Test method

```vbnet
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
```

### Notes

- Best to mock data for CRUD
- Tooling packages have been commented out in the project file as they are not needed now but left them in for inspection.

```xml
<Project Sdk="Microsoft.NET.Sdk">

	<PropertyGroup>
		<RootNamespace>NorthWindCoreLibrary</RootNamespace>
		<TargetFramework>net5.0</TargetFramework>
		<OptionStrict>On</OptionStrict>
	</PropertyGroup>

	<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Debug|AnyCPU'">
	  <WarningsAsErrors>41999,42016,42017,42018,42019,42020,42021,42022,42032,42036</WarningsAsErrors>
	</PropertyGroup>

	<PropertyGroup Condition="'$(Configuration)|$(Platform)'=='Release|AnyCPU'">
	  <WarningsAsErrors>41999,42016,42017,42018,42019,42020,42021,42022,42032,42036</WarningsAsErrors>
	</PropertyGroup>

	<ItemGroup>
		<PackageReference Include="EntityFrameworkCore.VisualBasic" Version="5.0.0-alpha.2">
			<PrivateAssets>all</PrivateAssets>
			<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
		</PackageReference>
		
		<PackageReference Include="Microsoft.EntityFrameworkCore" Version="5.0.9" />

		<!--<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="5.0.9">
			<PrivateAssets>all</PrivateAssets>
			<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
		</PackageReference>-->

		<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="5.0.9" />

		<!--<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="5.0.9">
			<PrivateAssets>all</PrivateAssets>
			<IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
		</PackageReference>-->
	</ItemGroup>

	<ItemGroup>
		<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="5.0.8" />
		<PackageReference Include="Newtonsoft.Json" Version="13.0.1" />
		<PackageReference Include="Microsoft.Extensions.Configuration" Version="5.0.0" />
		<PackageReference Include="Microsoft.Extensions.Configuration.Binder" Version="5.0.0" />
		<PackageReference Include="Microsoft.Extensions.Configuration.FileExtensions" Version="5.0.0" />
		<PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="5.0.0" />
		<!--<PackageReference Include="System.Drawing.Common" Version="6.0.0-preview.6.21352.12" />-->
	</ItemGroup>

	<ItemGroup>
		<None Update="appsettings.json">
			<CopyToOutputDirectory>Always</CopyToOutputDirectory>
		</None>
	</ItemGroup>

</Project>
```