
Namespace Models
    Public Partial Class Shipper
        Public Sub New()
            Orders = New HashSet(Of Order)()
        End Sub

        Public Property ShipperId As Integer
        Public Property CompanyName As String
        Public Property Phone As String
        Public Overrides Function ToString() As String
            Return CompanyName
        End Function
        Public Overridable Property Orders As ICollection(Of Order)
    End Class
End Namespace
