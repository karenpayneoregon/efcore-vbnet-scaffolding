Imports NorthWindCoreLibrary.Classes
Imports NorthWindCoreLibrary.Models

Public Class AddCustomerForm

    Public Delegate Sub OnAddCustomer(ByVal sender As Customer)
    Public Shared Event AddCustomerHandler As OnAddCustomer

    Private Sub AddCustomerForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ContactComboBox.DataSource = ContactOperations.List()
        ContactTypeComboBox.DataSource = ContactTypeOperations.List()
        CountryComboBox.DataSource = CountryOperations.List()
    End Sub

    Private Sub AddButton_Click(sender As Object, e As EventArgs) Handles AddButton.Click
        ' TODO Perform validation
    End Sub
End Class