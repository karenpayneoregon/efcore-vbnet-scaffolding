Imports System.IO
Imports Microsoft.EntityFrameworkCore
Imports Newtonsoft.Json
Imports NorthWindCoreLibrary.Data
Imports NorthWindCoreLibrary.Models
Imports NorthWindCoreLibrary.Extensions

Namespace Classes
    Public Class ProductOperations
        Public Shared Function List() As List(Of Product)
            Using context As New NorthWindContext
                Return context.Products.ToList()
            End Using
        End Function
        Public Shared Function ListIgnoreFilter() As List(Of Product)
            Using context As New NorthWindContext
                Return context.Products.IgnoreQueryFilters().ToList()
            End Using
        End Function

        Private Shared jsonFileName As String = "Products.json"

        Public Shared Sub ToJson()
            Using context As New NorthWindContext
                context.Products.ToList().ModeListToJson(jsonFileName)
            End Using
        End Sub
        Public Shared Function ReadProductsFromJsonFile() As List(Of Product)
            Dim products As New List(Of Product)()

            If File.Exists(jsonFileName) Then
                Dim json = File.ReadAllText(jsonFileName)
                products = JsonConvert.DeserializeObject(Of List(Of Product))(json)
            End If
            Return products
        End Function

    End Class
End Namespace