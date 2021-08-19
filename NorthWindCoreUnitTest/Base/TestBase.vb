Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace Base
    Public Class TestBase
        Protected TestContextInstance As TestContext
        Public Property TestContext() As TestContext
            Get
                Return TestContextInstance
            End Get
            Set
                TestContextInstance = Value
                TestResults.Add(TestContext)
            End Set
        End Property

        Public Shared TestResults As IList(Of TestContext)
    End Class
End Namespace
