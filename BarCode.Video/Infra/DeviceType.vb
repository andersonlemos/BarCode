Imports AForge.Video.DirectShow

Namespace Infra
    Public MustInherit Class DeviceType

        Public Shared ReadOnly Property VideoInput As Guid
            Get
                Return FilterCategory.VideoInputDevice
            End Get
        End Property

        Public Shared ReadOnly Property VideoCompressor As Guid
            Get
                Return FilterCategory.VideoCompressorCategory
            End Get
        End Property

        Public Shared ReadOnly Property AudioInput As Guid
            Get
                Return FilterCategory.AudioInputDevice
            End Get
        End Property

        Public Shared ReadOnly Property AudioCompressor As Guid
            Get
                Return FilterCategory.AudioCompressorCategory
            End Get
        End Property

    End Class

End Namespace