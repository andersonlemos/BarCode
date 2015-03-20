Imports System.Drawing
Imports BarCode.Application.Interfaces
Imports ZXing
Imports ZXing.Common
Imports ZXing.Rendering

Namespace Entities.Services

    Friend Class QrCodeEncoder : Implements IEncoder

        Private ReadOnly Property QrCodeRenderer() As IBarcodeRenderer(Of Bitmap)
            Get
                Return New BitmapRenderer()
            End Get
        End Property

        Private _writerOptions As BarcodeWriter
        Private _encodingOptions As EncodingOptions

        Sub New()

            QrCodeConfiguration()

        End Sub

        Private Sub QrCodeConfiguration()

            EncodingOptions(New EncodingOptions())
            WriterConfiguration(New BarcodeWriter())

        End Sub

        Private Sub EncodingOptions(options As EncodingOptions)

            _encodingOptions = options

            _encodingOptions.Height = 177
            _encodingOptions.Width = 177
            _encodingOptions.PureBarcode = True
            _encodingOptions.Margin = 1

        End Sub

        Private Sub WriterConfiguration(writer As BarcodeWriter)

            _writerOptions = writer
            _writerOptions.Options = _encodingOptions
            _writerOptions.Format = BarcodeFormat.QR_CODE

        End Sub

        Public Function Encode(ByVal information As String) As Image Implements IEncoder.Encode

            Return QrCodeRenderer.Render(_writerOptions.Encode(information), Nothing, Nothing, _encodingOptions)

        End Function

    End Class

End Namespace