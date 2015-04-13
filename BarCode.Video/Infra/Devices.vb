Imports AForge.Video.DirectShow
Imports BarCode.Video.Interfaces

Namespace Infra
    Public Class Devices : Implements IDevice

        Public Shared Property TypeOfDevice() As DeviceType

        Private ReadOnly _filterBuilder As FilterInfoCollection

        Sub New(type As Guid)
            _filterBuilder = New FilterInfoCollection(type)
        End Sub

        Public Function [Get]() As IEnumerable Implements IDevice.[Get]
            Return _filterBuilder
        End Function

    End Class
End Namespace