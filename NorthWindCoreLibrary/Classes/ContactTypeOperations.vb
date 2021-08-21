Imports NorthWindCoreLibrary.Containers
Imports NorthWindCoreLibrary.Data
Imports NorthWindCoreLibrary.Models

Namespace Classes
    Public Class ContactTypeOperations
        Public Shared Function List() As List(Of ContactType)
            Using context As New NorthWindContext
                Return context.ContactTypes.ToList()
            End Using
        End Function
    End Class
End Namespace