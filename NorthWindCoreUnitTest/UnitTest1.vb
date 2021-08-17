Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports NorthWindCoreLibrary_vb
Imports NorthWindCoreLibrary_vb.Classes
Imports NorthWindCoreLibrary_vb.Containers
Imports NorthWindCoreLibrary_vb.Data

<TestClass>
Public Class UnitTest1
    ''' <summary>
    ''' Simple read operation
    ''' </summary>
    <TestMethod>
    Sub LoadCustomers()
        Using context = New NorthWindContext

            Dim customers As List(Of CustomerItem) = CustomersOperations.CustomerProjection()

            Assert.AreEqual(customers.Count, 91)
            Assert.IsTrue(customers.FirstOrDefault().CompanyName = "Alfreds Futterkiste")

        End Using
    End Sub
End Class