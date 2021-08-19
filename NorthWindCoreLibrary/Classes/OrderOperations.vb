Imports Microsoft.EntityFrameworkCore
Imports NorthWindCoreLibrary_vb.Data
Imports NorthWindCoreLibrary_vb.Models

Namespace Classes
    Public Class OrderOperations
        Public Shared Async Function GetEmployeesTask() As Task(Of List(Of Employee))

            Dim employeesList As New List(Of Employee)()

            Await Task.Run(Async Function()
                               Using context = New NorthWindContext()
                                   employeesList = Await context.Orders.Where(Function(order) order.EmployeeId IsNot Nothing).
                                        Select(Function(order) order.Employee).ToListAsync()
                               End Using
                           End Function)

            Return employeesList

        End Function
        Public Shared Async Function EmployeeMostOrders_Linq() As Task(Of IEnumerable(Of IEnumerable(Of Employee)))

            Dim employeeList = Await GetEmployeesTask()

            Dim groupedResults As IEnumerable(Of IEnumerable(Of Employee)) =
                    From employees In employeeList
                    Group employees By employees.EmployeeId Into grouped = Group Order By grouped.Count() Descending
                    Select grouped


            Return groupedResults

        End Function

    End Class
End Namespace