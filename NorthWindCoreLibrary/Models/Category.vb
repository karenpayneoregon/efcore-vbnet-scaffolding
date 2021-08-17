Imports System
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Namespace Models
    Public Partial Class Category
        Public Sub New()
            Products = New HashSet(Of Product)()
        End Sub

        ''' <summary>
        ''' Primary key
        ''' </summary>
        Public Property CategoryId As Integer
        Public Property CategoryName As String
        Public Property Description As String
        Public Property Picture As Byte()

        Public Overridable Property Products As ICollection(Of Product)
    End Class
End Namespace
