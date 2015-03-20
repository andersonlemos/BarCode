Imports System.Drawing
Namespace Services
    Public Interface IBarCodeService
        Function Encode(information As String) As Image
        Function Decode(information As Image) As String
    End Interface
End Namespace