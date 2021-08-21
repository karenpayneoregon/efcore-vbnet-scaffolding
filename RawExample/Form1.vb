Imports System.ComponentModel
Imports Microsoft.EntityFrameworkCore
Imports NorthWindCoreLibrary.Data
Imports NorthWindCoreLibrary.Extensions
Imports NorthWindCoreLibrary.FormClasses
Imports NorthWindCoreLibrary.Models

Public Class Form1

    Public Shared Context As New NorthWindContext()

    ''' <summary>
    ''' Works
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim results = Await CustomersLocal()
        Label1.Text = results.Count.ToString()
    End Sub
    Private Async Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim results = Await CustomerIncludes()
        Label2.Text = results.Count.ToString()
    End Sub

    Public Async Function CustomersLocal() As Task(Of BindingList(Of Customer))

        Return Await Task.Run(Async Function()
                                  Await Context.Customers.LoadAsync()
                                  Return Context.Customers.Local.ToBindingList()
                              End Function)

    End Function

    Public Async Function CustomerIncludes() As Task(Of List(Of Customer))

        Return Await Task.Run(Async Function()
                                  Using contextLocal = New NorthWindContext()

                                      Return Await contextLocal.Customers.IncludeContactsDevicesCountry().ToListAsync()

                                  End Using

                              End Function)

    End Function

    Private Async Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim results = Await DataOperations.Customers()
        Label3.Text = results.Count.ToString()
    End Sub
End Class
