Imports System
Imports Microsoft.VisualBasic
Imports System.Collections.Generic

Namespace Models
    Public Partial Class Employee
        Public Sub New()
            EmployeeTerritories = New HashSet(Of EmployeeTerritory)()
            Orders = New HashSet(Of Order)()
        End Sub

        ''' <summary>
        ''' Id
        ''' </summary>
        Public Property EmployeeId As Integer
        ''' <summary>
        ''' Last name
        ''' </summary>
        Public Property LastName As String
        ''' <summary>
        ''' First name
        ''' </summary>
        Public Property FirstName As String
        Public Property ContactTypeIdentifier As Integer?
        ''' <summary>
        ''' Title
        ''' </summary>
        Public Property TitleOfCourtesy As String
        ''' <summary>
        ''' Birth date
        ''' </summary>
        Public Property BirthDate As DateTime?
        ''' <summary>
        ''' Hiredate
        ''' </summary>
        Public Property HireDate As DateTime?
        ''' <summary>
        ''' Street
        ''' </summary>
        Public Property Address As String
        Public Property City As String
        Public Property Region As String
        Public Property PostalCode As String
        Public Property CountryIdentifier As Integer?
        ''' <summary>
        ''' Home phone
        ''' </summary>
        Public Property HomePhone As String
        Public Property Extension As String
        Public Property Notes As String
        ''' <summary>
        ''' Manager
        ''' </summary>
        Public Property ReportsTo As Integer?

        Public Overridable Property ContactTypeIdentifierNavigation As ContactType
        Public Overridable Property CountryIdentifierNavigation As Country
        Public Overridable Property EmployeeTerritories As ICollection(Of EmployeeTerritory)
        Public Overridable Property Orders As ICollection(Of Order)
    End Class
End Namespace
