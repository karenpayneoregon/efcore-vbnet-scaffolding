Imports Microsoft.VisualStudio.TestTools.UnitTesting


' ReSharper disable once CheckNamespace - do not change
Partial Public Class UnitTest1
    <ClassInitialize()>
    Public Shared Sub ClassInitialize(ByVal testContext As TestContext)
        TestResults = New List(Of TestContext)()
    End Sub

End Class
