Imports System.Diagnostics.CodeAnalysis

Namespace Extensions
    Module DataGridViewExtensions
        ''' <summary>
        ''' Expand all columns and suitable for working with Entity Framework in regards to ICollection`1 column types.
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="sizable">Undue DataGridViewAutoSizeColumnMode.AllCells which makes manual resizing possible</param>
        <Runtime.CompilerServices.Extension>
        Public Sub ExpandColumns(sender As DataGridView, Optional sizable As Boolean = False)
            For Each col As DataGridViewColumn In sender.Columns
                ' ensure we are not attempting to do this on a Entity
                If col.ValueType.Name <> "ICollection`1" Then
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                End If
            Next col

            If Not sizable Then
                Return
            End If

            For index As Integer = 0 To sender.Columns.Count - 1
                Dim columnWidth As Integer = sender.Columns(index).Width

                sender.Columns(index).AutoSizeMode = DataGridViewAutoSizeColumnMode.None

                ' Set Width to calculated AutoSize value:
                sender.Columns(index).Width = columnWidth
            Next index


        End Sub

    End Module
End Namespace