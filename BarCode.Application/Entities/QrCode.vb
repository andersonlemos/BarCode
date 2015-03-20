
Imports System.Drawing
Imports BarCode.Application.Entities.Services
Imports BarCode.Application.Interfaces

Namespace Entities

    Public Class QrCode : Implements ICodeOperation, IDisposable

        Private ReadOnly Property Encoder() As IEncoder
            Get
                Return New QrCodeEncoder()
            End Get
        End Property

        Private ReadOnly Property Decoder() As IDecoder
            Get
                Return New QrCodeDecoder()
            End Get
        End Property

        Public Function Encode(information As String) As Image Implements IEncoder.Encode

            Return Encoder.Encode(information)

        End Function

        Public Function Decode(information As Image) As String Implements IDecoder.Decode

            Return Decoder.Decode(information)

        End Function

        Private _disposed As Boolean = False

        Private Shadows Sub Dispose(ByVal disposing As Boolean)

            If Not _disposed Then

                If disposing Then
                    MyBase.Finalize()
                End If

                _disposed = True

            End If

        End Sub

        Public Shadows Sub Dispose() Implements IDisposable.Dispose

            Dispose(True)
            GC.SuppressFinalize(Me)

        End Sub

        Protected Overrides Sub Finalize()

            Dispose(False)
            MyBase.Finalize()

        End Sub

    End Class

End Namespace