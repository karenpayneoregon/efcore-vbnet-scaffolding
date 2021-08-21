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

        _bindingSource.AddPersonCustomer(New Customer() With {
                                            .CompanyName = "Payne Inc",
                                            .Street = "123 Apple Way",
                                            .City = "Portland",
                                            .ContactId = 1,
                                            .ContactTypeIdentifier = 1
                                            })

        _bindingSource.MoveLast()

    End Sub
End Class