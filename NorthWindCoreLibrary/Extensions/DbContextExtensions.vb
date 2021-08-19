Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.ChangeTracking.Internal
Imports Microsoft.EntityFrameworkCore.Metadata
Imports NorthWindCoreLibrary_vb.Classes
Imports NorthWindCoreLibrary_vb.Data
Imports NorthWindCoreLibrary_vb.Interfaces

Namespace Extensions

    Public Module DbContextExtensions
        ''' <summary>
        ''' Determine if a connection can be made asynchronously
        ''' </summary>
        ''' <param name="context"><see cref="DbContext"/></param>
        ''' <returns></returns>
        <Runtime.CompilerServices.Extension>
        Public Async Function TestConnection(context As DbContext) As Task(Of Boolean)
            Return Await Task.Run(Async Function() Await context.Database.CanConnectAsync())
        End Function

        ''' <summary>
        ''' Determine if a connection can be made asynchronously with <see cref="Threading.CancellationToken"/>
        ''' </summary>
        ''' <param name="context"><see cref="DbContext"/></param>
        ''' <param name="token">&lt;see cref="CancellationToken"/&gt;</param>
        ''' <returns></returns>
        <Runtime.CompilerServices.Extension>
        Public Async Function TestConnection(context As DbContext, token As CancellationToken) As Task(Of Boolean)
            Return Await Task.Run(Async Function() Await context.Database.CanConnectAsync(token), token)
        End Function
        <Runtime.CompilerServices.Extension>
        Public Function ModelTypeInformation(context As DbContext) As List(Of Type)
            Return context.Model.GetEntityTypes().Select(Function(entityType) entityType.ClrType).ToList()
        End Function
        ''' <summary>
        ''' Get model names for a <see cref="DbContext"/>
        ''' </summary>
        ''' <param name="context"></param>
        ''' <returns></returns>
        <Runtime.CompilerServices.Extension>
        Public Function GetModelNames(context As DbContext) As List(Of String)
            Return context.ModelTypeInformation().Select(Function(item) item.Name).ToList()
        End Function

        <Runtime.CompilerServices.Extension>
        Public Function GetModelNamesSorted(context As DbContext) As IOrderedEnumerable(Of String)
            Return context.GetModelNames().OrderBy(Function(name) name)
        End Function

        <Runtime.CompilerServices.Extension>
        Public Function Comments(Of T As IModelBaseEntity)(ByVal sender As T) As IEnumerable(Of ModelComment)
            Using context = New NorthWindContext()
                Dim entityType As IEntityType = context.Model.FindRuntimeEntityType(GetType(T))

                If entityType IsNot Nothing Then

                    Return entityType.GetProperties().Select(Function([property]) New ModelComment With {
                                                                .Name = [property].Name,
                                                                .Comment = If(String.IsNullOrWhiteSpace([property].GetComment()), [property].Name, [property].GetComment())
                                                                })
                Else
                    Return Enumerable.Empty(Of ModelComment)()
                End If
            End Using
        End Function

    End Module

End Namespace