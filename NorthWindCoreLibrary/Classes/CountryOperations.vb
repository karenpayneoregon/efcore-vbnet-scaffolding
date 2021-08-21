Imports NorthWindCoreLibrary.Data
Imports NorthWindCoreLibrary.Models

Namespace Classes
    Public Class CountryOperations
        Public Shared Function List() As List(Of Country)
            Using context As New NorthWindContext
                Return context.Countries.ToList()
            End Using
        End Function
    End Class
End NameSpace