Imports System
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Namespace Models
    Public Partial Class BusinessEntityPhone
        Public Property BusinessEntityPhoneId As Integer
        ''' <summary>
        ''' Phone number
        ''' </summary>
        Public Property PhoneNumber As String
        Public Property PhoneNumberTypeId As Integer?
        Public Property ModifiedDate As DateTime?
    End Class
End Namespace
