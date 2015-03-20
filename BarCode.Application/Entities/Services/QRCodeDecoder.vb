Imports System.Drawing
Imports BarCode.Application.Interfaces
Imports ZXing
Imports ZXing.Common
Imports ZXing.QrCode

Namespace Entities.Services

    Friend Class QrCodeDecoder : Implements IDecoder

        Private ReadOnly Property QrCodeReader() As QRCodeReader
            Get
                Return New QRCodeReader()
            End Get
        End Property

        Private ReadOnly Property BinaryImage(frame As Object) As BinaryBitmap
            Get
                Return New BinaryBitmap(New HybridBinarizer(frame))
            End Get
        End Property

        Private _imageByte As Byte()

        Public Function Decode(ByVal information As Image) As String Implements IDecoder.Decode

            _imageByte = ImageExtensions.ImageToByte(information)

            Using frame = New FrameSource(_imageByte, information.Width, information.Height)

                Return QrCodeReader.decode(BinaryImage(frame)).ToString()

            End Using

        End Function

    End Class

End Namespace