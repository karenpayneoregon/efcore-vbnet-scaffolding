Imports System.ComponentModel
Imports Microsoft.EntityFrameworkCore
Imports NorthWindCoreLibrary.Data
Imports NorthWindCoreLibrary.Models

Namespace FormClasses
    Public Class DataOperations
        Public Shared Context As New NorthWindContext()

        Public Shared Async Function Customers() As Task(Of List(Of Customer))

            Return Await Task.Run(Async Function() Await Context.Customers.ToListAsync())
        End Function

        Public Shared Async Function CustomersLocal() As Task(Of BindingList(Of Customer))
            Return Await Task.Run(Async Function()
                                      Await Context.Customers.LoadAsync()
                                      Return Context.Customers.Local.ToBindingList()
                                  End Function)
        End Function
    End Class
End Namespace