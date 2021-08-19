Imports Microsoft.Extensions.Configuration
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports NorthWindCoreLibrary_vb.Classes
Imports NorthWindCoreLibrary_vb.Models

' ReSharper disable once CheckNamespace - do not change
Partial Public Class UnitTest1
    <ClassInitialize()>
    Public Shared Sub ClassInitialize(ByVal testContext As TestContext)
        TestResults = New List(Of TestContext)()
    End Sub

    Public ConextConnectionString As String = "Data Source=.\SQLEXPRESS;Initial Catalog=NorthWind2020;Integrated Security=true"

    Public FirstCompanyName As String = CustomersOperations.CustomerProjection().FirstOrDefault().CompanyName

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
    Private Shared Function BuildConnection() As String

        Dim configuration = (New ConfigurationBuilder()).AddJsonFile("appsettings.json", True, True).Build()

        Dim sections = configuration.GetSection("database").GetChildren().ToList()

        Return $"Data Source={sections(1).Value};Initial Catalog={sections(0).Value};Integrated Security={sections(2).Value}"

    End Function
End Class
