Imports ZXing

Namespace Entities

    Friend Class FrameSource : Inherits RGBLuminanceSource
        Implements IDisposable

        Private _disposed As Boolean = False

        Sub New(byteArray As Byte(), width As Integer, heigth As Integer)
            MyBase.New(byteArray, width, heigth)
        End Sub

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