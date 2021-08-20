Imports Microsoft.VisualStudio.TestTools.UnitTesting

Namespace Base
    ''' <summary>
    ''' Declarative class for using Trait enum about for traits on test method.
    ''' </summary>
    Public Class TestTraitsAttribute
        Inherits TestCategoryBaseAttribute

        Private ReadOnly _traits() As Trait

        Public Sub New(ParamArray ByVal traits() As Trait)
            _traits = traits
        End Sub

        Public Overrides ReadOnly Property TestCategories() As IList(Of String)
            Get
                Return _traits.Select(Function(trait) System.Enum.GetName(GetType(Trait), trait)).ToList()
            End Get
        End Property
    End Class
End Namespace