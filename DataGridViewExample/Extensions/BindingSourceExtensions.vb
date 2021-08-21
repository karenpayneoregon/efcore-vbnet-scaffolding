Imports System.ComponentModel
Imports DataGridViewExample.Models

Namespace Extensions
    Module BindingSourceExtensions
        <Runtime.CompilerServices.Extension>
        Public Sub AddPersonCustomer(sender As BindingSource, customer As Customer)
            DirectCast(sender.DataSource, BindingList(Of Customer)).Add(customer)
        End Sub
    End Module
End Namespace