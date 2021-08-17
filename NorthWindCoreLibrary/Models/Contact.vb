Imports System
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Namespace Models
    Public Partial Class Contact
        Public Sub New()
            ContactDevices = New HashSet(Of ContactDevice)()
            Customers = New HashSet(Of Customer)()
        End Sub

        ''' <summary>
        ''' Id
        ''' </summary>
        Public Property ContactId As Integer
        ''' <summary>
        ''' First name
        ''' </summary>
        Public Property FirstName As String
        ''' <summary>
        ''' Last name
        ''' </summary>
        Public Property LastName As String
        ''' <summary>
        ''' Contact Type Id
        ''' </summary>
        Public Property ContactTypeIdentifier As Integer?

        Public Overrides Function ToString() As String
            Return $"{FirstName} {LastName}"
        End Function

        Public Overridable Property ContactTypeIdentifierNavigation As ContactType
        Public Overridable Property ContactDevices As ICollection(Of ContactDevice)
        Public Overridable Property Customers As ICollection(Of Customer)
    End Class
End Namespace
