
Imports BarCode.Application.Interfaces

Namespace Services

    Public Class BarCodeService : Implements IBarCodeService, IDisposable

        Private _disposed As Boolean = False
        Private ReadOnly _barCodeType As IBarCodeType

        Sub New(barCodeType As IBarCodeType)
            _barCodeType = barCodeType
        End Sub

        Public Function Execute(ByVal information As Object) As Object Implements IBarCodeService.Execute
            Return _barCodeType.Execute(information)
        End Function
        
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