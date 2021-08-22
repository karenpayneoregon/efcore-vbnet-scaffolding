Imports System.IO
Imports Newtonsoft.Json

Namespace Extensions
    Public Module GenericExtensions
        <Runtime.CompilerServices.Extension>
        Public Sub ModeListToJson(Of TModel)(list As List(Of TModel), fileName As String)
            Dim json = JsonConvert.SerializeObject(list, Formatting.Indented, New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Ignore})
            File.WriteAllText(fileName, json)
        End Sub
    End Module
End Namespace