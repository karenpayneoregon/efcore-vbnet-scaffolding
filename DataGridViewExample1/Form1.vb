Imports System.ComponentModel
Imports DataGridViewExample1.Extensions
Imports NorthWindCoreLibrary.FormClasses
Imports NorthWindCoreLibrary.Models

Public Class Form1

    Private ReadOnly _bindingSource As New BindingSource()

    Private Async Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown

        CustomersDataGridView.AutoGenerateColumns = False
        CustomersDataGridView.AllowUserToAddRows = False

        Try

            Dim peopleLocalList As BindingList(Of Customer) = Await DataOperations.CustomersLocal()
            _bindingSource.DataSource = peopleLocalList

            CustomersDataGridView.DataSource = _bindingSource

            CustomersDataGridView.ExpandColumns(True)

        Catch ex As Exception
            MessageBox.Show($"Failed to load data.{Environment.NewLine}{ex.Message}")
        End Try


        ShowChangesButton.Enabled = True
        AddButton.Enabled = True

    End Sub

    Private Sub ShowChangesButton_Click(sender As Object, e As EventArgs) Handles ShowChangesButton.Click

        Dim changes = DataOperations.Show()

        If Not String.IsNullOrWhiteSpace(changes) Then

            ChangesTextBox.Text = changes

            If SaveChangesCheckBox.Checked Then
                MessageBox.Show($"Change count - {DataOperations.Context.SaveChanges()}")
            End If

        Else
            ChangesTextBox.Text = ""
        End If

    End Sub

    Private Sub AddButton_Click(sender As Object, e As EventArgs) Handles AddButton.Click

        Dim customerForm As New AddCustomerForm

        AddHandler customerForm.AddCustomerHandler, AddressOf NewCustomerFromAddForm

        Try
            customerForm.ShowDialog()
        Finally
            RemoveHandler customerForm.AddCustomerHandler, AddressOf NewCustomerFromAddForm
            customerForm.Dispose()

        End Try

    End Sub

    Private Sub NewCustomerFromAddForm(sender As Customer)
        _bindingSource.AddPersonCustomer(sender)
        _bindingSource.MoveLast()
    End Sub
End Class