
Imports Microsoft.EntityFrameworkCore.Diagnostics


Namespace Data.Interceptors

    Public Class SavedChangesInterceptor
        Inherits SaveChangesInterceptor

        ''' <summary>
        ''' Stub 
        ''' </summary>
        ''' <param name="eventData"></param>
        ''' <param name="result"></param>
        ''' <returns></returns>
        Public Overrides Function SavedChanges(eventData As SaveChangesCompletedEventData, result As Integer) As Integer

            eventData.Context.ChangeTracker.DetectChanges()

            'Debug.WriteLine(eventData.Context.ChangeTracker.DebugView.LongView)

            Return MyBase.SavedChanges(eventData, result)

        End Function

    End Class
End Namespace