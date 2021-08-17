Imports System.Globalization

Namespace Extensions

    Public Module DateTimeExtensions
        <Runtime.CompilerServices.Extension>
        Public Function ZeroPad(sender As DateTime) As String
            Dim dateSeparator As String = CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator
            Dim timeSeparator As String = CultureInfo.CurrentCulture.DateTimeFormat.TimeSeparator

            Return $"{sender.Year:D2}{dateSeparator}{sender.Month:D2}{dateSeparator}{sender.Day:D2} {sender.Hour:D2}{timeSeparator}{sender.Minute:D2}{timeSeparator}{sender.Second:D2}"

        End Function
    End Module

End Namespace