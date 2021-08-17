Imports System
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Namespace Models
    Public Partial Class ContactDevice
        Public Property Id As Integer
        Public Property ContactId As Integer?
        Public Property PhoneTypeIdentifier As Integer?
        Public Property PhoneNumber As String

        Public Overridable Property Contact As Contact
        Public Overridable Property PhoneTypeIdentifierNavigation As PhoneType
    End Class
End Namespace
