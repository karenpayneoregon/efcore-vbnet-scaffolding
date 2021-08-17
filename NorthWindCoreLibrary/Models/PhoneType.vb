Imports System
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Namespace Models
    Public Partial Class PhoneType
        Public Sub New()
            ContactDevices = New HashSet(Of ContactDevice)()
        End Sub

        Public Property PhoneTypeIdenitfier As Integer
        Public Property PhoneTypeDescription As String

        Public Overridable Property ContactDevices As ICollection(Of ContactDevice)
    End Class
End Namespace
