
Imports BarCode.Application.Entities.Services
Imports BarCode.Application.Interfaces

Namespace Entities

    Public Class QrCode : Implements IBarCodeType, IDisposable

        Private _disposed As Boolean = False

        Private ReadOnly _operation As ICodeOperation
        
        Public Shared ReadOnly Property Encode() As ICodeOperation
            Get
                Return New QrCodeEncoder()
            End Get
        End Property

        Public Shared ReadOnly Property Decode() As ICodeOperation
            Get
                Return New QrCodeDecoder()
            End Get
        End Property

        Sub New(operation As ICodeOperation)
            _operation = operation
        End Sub

        Public Function Execute(information As Object) As Object Implements IBarCodeType.Execute
            Return _operation.Execute(information)
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