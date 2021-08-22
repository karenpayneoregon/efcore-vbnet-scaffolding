Imports NorthWindCoreLibrary.Classes
Imports NorthWindCoreLibrary.Models
Imports WinFormValidationLibrary.LanguageExtensions
Imports WinFormValidationLibrary.Validators

Public Class AddCustomerForm

    ''' <summary>
    ''' Responsible for sending a good customer to the
    ''' calling form.
    ''' </summary>
    ''' <param name="sender"></param>
    Public Delegate Sub OnAddCustomer(ByVal sender As Customer)
    Public Event AddCustomerHandler As OnAddCustomer

    ''' <summary>
    ''' Set up reference data. Could add a select as the first item
    ''' for each ComboBox which is easy but that is not the point here
    ''' which is to show a simple validate and add or deny adding
    ''' a new customer to the calling form.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AddCustomerForm_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        ContactComboBox.DataSource = ContactOperations.List()
        ContactTypeComboBox.DataSource = ContactTypeOperations.List()
        CountryComboBox.DataSource = CountryOperations.List()
    End Sub

    ''' <summary>
    ''' 1. Collect data
    ''' 2. Validate against data annotations
    ''' 3. On pass validation send new customer to calling form,
    '''    on failure alert user and don't send new customer to calling form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AddButton_Click(sender As Object, e As EventArgs) Handles AddButton.Click

        Dim customer As New Customer With {
            .CompanyName = CompanyNameTextBox.Text,
            .ContactId = CType(ContactComboBox.SelectedItem, Contact).ContactId,
            .ContactTypeIdentifier = CType(ContactTypeComboBox.SelectedItem, ContactType).ContactTypeIdentifier,
            .Street = StreetTextBox.Text,
            .City = CityTextBox.Text,
            .CountryIdentifier = CType(CountryComboBox.SelectedItem, Country).CountryIdentifier
        }

        Dim validationResult As EntityValidationResult = ValidationHelper.ValidateEntity(customer)

        If validationResult.HasError Then
            MessageBox.Show(validationResult.ErrorMessageList())
        Else
            RaiseEvent AddCustomerHandler(customer)
            DialogResult = DialogResult.OK
        End If

    End Sub
End Class