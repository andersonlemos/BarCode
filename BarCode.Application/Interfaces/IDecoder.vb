Imports System.Drawing

Namespace Interfaces
    Public Interface IDecoder
        Function Decode(information As Image) As String
    End Interface
End Namespace