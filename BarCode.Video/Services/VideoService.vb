Imports BarCode.Video.Interfaces

Namespace Services
    Public Class VideoService : Implements IVideoService

        Private _source As IStreamSource

        Sub New(streamSource As IStreamSource)
            _source = streamSource
        End Sub
        
        Public Sub Start() Implements IVideoService.Start
            _source.Start()
        End Sub

        Public Sub [Stop]() Implements IVideoService.Stop
            _source.Stop()
        End Sub

    End Class
End Namespace