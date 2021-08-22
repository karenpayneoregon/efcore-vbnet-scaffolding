Imports System.Data.Common
Imports System.Threading
Imports Microsoft.EntityFrameworkCore.Diagnostics

Namespace Data.Interceptors
    ''' <summary>
    ''' This Interceptor can assist with determining issues which can be logged
    ''' or perhaps change the timeout for a connection are a few possibilities.
    ''' </summary>
    ''' <remarks>
    ''' Code presented here does nothing except show possibilities.
    ''' </remarks>
    Public Class CommandInterceptor
        Inherits DbCommandInterceptor

        Public Overrides Function ReaderExecuted(command As DbCommand, eventData As CommandExecutedEventData, result As DbDataReader) As DbDataReader

            Dim changedResult As DbDataReader = Nothing

            If ShouldChangeResult(command, changedResult) Then
                Return changedResult
            End If


            Return MyBase.ReaderExecuted(command, eventData, result)

        End Function

        Private Function ShouldChangeResult(dbCommand As DbCommand, changedResult As Object) As Boolean
            Return False
        End Function

        Public Overrides Function ReaderExecuting(command As DbCommand, eventData As CommandEventData, result As InterceptionResult(Of DbDataReader)) As InterceptionResult(Of DbDataReader)
            Dim reasonSomethingIsWrong As Object = Nothing

            If IsSomethingWrongWithThisCommand(command, reasonSomethingIsWrong) Then
                ' query will not be executed
                Throw New InvalidOperationException(CStr(reasonSomethingIsWrong))
            End If

            Return MyBase.ReaderExecuting(command, eventData, result)
        End Function

        ''' <summary>
        ''' Interrogate command for issues
        ''' </summary>
        ''' <param name="dbCommand"></param>
        ''' <param name="reasonSomethingIsWrong"></param>
        ''' <returns></returns>
        Private Shared Function IsSomethingWrongWithThisCommand(dbCommand As DbCommand, reasonSomethingIsWrong As Object) As Boolean
            Return False
        End Function

        Public Overrides Function ReaderExecutedAsync(command As DbCommand, eventData As CommandExecutedEventData, result As DbDataReader, Optional cancellationToken As CancellationToken = Nothing) As ValueTask(Of DbDataReader)
            Return MyBase.ReaderExecutedAsync(command, eventData, result, cancellationToken)
        End Function

        Public Overrides Function NonQueryExecuted(command As DbCommand, eventData As CommandExecutedEventData, result As Integer) As Integer
            Return MyBase.NonQueryExecuted(command, eventData, result)
        End Function

        Public Overrides Function NonQueryExecutedAsync(command As DbCommand, eventData As CommandExecutedEventData, result As Integer, Optional cancellationToken As CancellationToken = Nothing) As ValueTask(Of Integer)
            Return MyBase.NonQueryExecutedAsync(command, eventData, result, cancellationToken)
        End Function

        Public Overrides Function CommandFailedAsync(command As DbCommand, eventData As CommandErrorEventData, Optional cancellationToken As CancellationToken = Nothing) As Task
            Return MyBase.CommandFailedAsync(command, eventData, cancellationToken)
        End Function

        Public Overrides Function CommandCreated(eventData As CommandEndEventData, result As DbCommand) As DbCommand

            result.CommandTimeout = 10

            Return MyBase.CommandCreated(eventData, result)
        End Function
    End Class
End Namespace