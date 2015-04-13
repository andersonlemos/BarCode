Imports BarCode.Application.Interfaces
Imports ZXing
Imports ZXing.Common
Imports ZXing.QrCode

Namespace Entities.Services

    Friend Class QrCodeDecoder : Implements ICodeOperation, IDisposable

        Private _disposed As Boolean = False
        Private _imageByte As Byte()

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

        Public Function Execute(ByVal information As Object) As Object Implements ICodeOperation.Execute

            _imageByte = ImageExtensions.ImageToByte(information)

            Using frame = New FrameSource(_imageByte, information.Width, information.Height)

                Return QrCodeReader.decode(BinaryImage(frame)).ToString()

            End Using

        End Function

        Private Sub Dispose(ByVal disposing As Boolean)

            If Not _disposed Then

                If disposing Then
                    MyBase.Finalize()
                End If

                _disposed = True

            End If

        End Sub

        Protected Overridable Sub Dispose() Implements IDisposable.Dispose

            Dispose(True)
            GC.SuppressFinalize(Me)

        End Sub

        Protected Overrides Sub Finalize()

            Dispose(False)
            MyBase.Finalize()

        End Sub

    End Class

End Namespace