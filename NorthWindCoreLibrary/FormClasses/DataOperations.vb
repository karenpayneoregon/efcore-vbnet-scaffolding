Imports System.ComponentModel
Imports System.Text
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
        ''' <summary>
        ''' Get local changes, deleted records will not show
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function Show() As String

            Dim builder As New StringBuilder()

            For Each customer In Context.Customers.Local

                If Context.Entry(customer).State <> EntityState.Unchanged Then
                    builder.AppendLine($"{customer.CompanyName} {customer.Street} {customer.City} {Context.Entry(customer).State}")
                End If

            Next

            Return builder.ToString()
        End Function
    End Class
End Namespace