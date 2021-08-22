Imports System.Data.SqlClient

Namespace DataProviderHelpers
    Public Class DataOperationsSqlServer
        Public Shared Function GetActiveProductCount(ConnectionString As String) As Integer

            Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
                Using cmd As New SqlCommand With {.Connection = cn}
                    cmd.CommandText = "SELECT COUNT(ProductName) FROM dbo.Products WHERE dbo.Products.Discontinued = 0;"
                    cn.Open()
                    Return CInt(cmd.ExecuteScalar())
                End Using
            End Using

        End Function
        Public Shared Function GetProductCount(ConnectionString As String) As Integer

            Using cn As New SqlConnection With {.ConnectionString = ConnectionString}
                Using cmd As New SqlCommand With {.Connection = cn}
                    cmd.CommandText = "SELECT COUNT(ProductName) FROM dbo.Products;"
                    cn.Open()
                    Return CInt(cmd.ExecuteScalar())
                End Using
            End Using

        End Function
    End Class
End Namespace