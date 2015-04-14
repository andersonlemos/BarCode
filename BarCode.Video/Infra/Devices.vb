Imports AForge.Video.DirectShow
Imports BarCode.Video.Interfaces

Namespace Infra
    Public Class Devices : Implements IDevice, IDisposable

        Private _disposed As Boolean = False
        Public Shared Property TypeOfDevice() As DeviceType

        Private ReadOnly _filterBuilder As FilterInfoCollection

        Sub New(type As Guid)
            _filterBuilder = New FilterInfoCollection(type)
        End Sub

        Public Function [Get]() As IEnumerable Implements IDevice.[Get]
            Return _filterBuilder
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