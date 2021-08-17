Imports System
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Namespace Models
    Public Partial Class Customer
        Public Sub New()
            Orders = New HashSet(Of Order)()
        End Sub

        ''' <summary>
        ''' Id
        ''' </summary>
        Public Property CustomerIdentifier As Integer
        ''' <summary>
        ''' Company
        ''' </summary>
        Public Property CompanyName As String
        ''' <summary>
        ''' ContactId
        ''' </summary>
        Public Property ContactId As Integer?
        ''' <summary>
        ''' Street
        ''' </summary>
        Public Property Street As String
        ''' <summary>
        ''' City
        ''' </summary>
        Public Property City As String
        ''' <summary>
        ''' Region
        ''' </summary>
        Public Property Region As String
        ''' <summary>
        ''' Postal Code
        ''' </summary>
        Public Property PostalCode As String
        ''' <summary>
        ''' CountryIdentifier
        ''' </summary>
        Public Property CountryIdentifier As Integer?
        ''' <summary>
        ''' Phone
        ''' </summary>
        Public Property Phone As String
        ''' <summary>
        ''' Fax
        ''' </summary>
        Public Property Fax As String
        ''' <summary>
        ''' ContactTypeIdentifier
        ''' </summary>
        Public Property ContactTypeIdentifier As Integer?
        ''' <summary>
        ''' Modified Date
        ''' </summary>
        Public Property ModifiedDate As DateTime?

        Public Overrides Function ToString() As String
            Return CompanyName
        End Function

        Public Overridable Property Contact As Contact
        Public Overridable Property ContactTypeIdentifierNavigation As ContactType
        Public Overridable Property CountryIdentifierNavigation As Country
        Public Overridable Property Orders As ICollection(Of Order)
    End Class
End Namespace
