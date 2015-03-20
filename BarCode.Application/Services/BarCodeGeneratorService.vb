
Imports System.Drawing
Imports BarCode.Application.Entities
Imports BarCode.Application.Interfaces

Namespace Services

    Public Class BarCodeGeneratorService : Implements ICodeOperation

        Public Shared ReadOnly Property QrCode() As QrCode
            Get
                Return New QrCode
            End Get
        End Property

        Private ReadOnly _barCodeType As ICodeOperation

        Sub New(barCodeType As ICodeOperation)
            _barCodeType = barCodeType
        End Sub

        Public Function Encode(ByVal information As String) As Image Implements ICodeOperation.Encode
            Return _barCodeType.Encode(information)
        End Function

        Public Function Decode(ByVal information As Image) As String Implements ICodeOperation.Decode
            Return _barCodeType.Decode(information)
        End Function

    End Class

End Namespace