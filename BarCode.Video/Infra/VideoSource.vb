
Imports AForge.Controls
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports BarCode.Video.Interfaces

Namespace Infra
    Public Class VideoSource : Implements IStreamSource, IDisposable

        Private _disposed As Boolean = False
        Private _videoSourcePlayer As VideoSourcePlayer
        Private _captureSource As VideoCaptureDevice
        Private _asyncCaptureStream As IVideoSource

        Sub New(videoSourcePlayer As VideoSourcePlayer, sourceStream As Object)

            ConfigStream(videoSourcePlayer, sourceStream)

        End Sub

        Private Sub ConfigStream(videoSource As VideoSourcePlayer, sourceStream As Object)

            VideoSourcePlayer(videoSource)
            CaptureSource(sourceStream.MonikerString)
            CaptureStream(_captureSource)
            CaptureVideo(_asyncCaptureStream)

        End Sub

        Private Sub CaptureSource(filter As String)
            _captureSource = New VideoCaptureDevice(filter)
        End Sub

        Private Sub CaptureStream(captureSource As VideoCaptureDevice)
            _asyncCaptureStream = New AsyncVideoSource(captureSource)
        End Sub

        Private Sub VideoSourcePlayer(source As VideoSourcePlayer)
            _videoSourcePlayer = source
        End Sub

        Private Sub CaptureVideo(asyncCaptureStream As IVideoSource)
            _videoSourcePlayer.VideoSource = asyncCaptureStream
        End Sub

        Public Sub Start() Implements IStreamSource.Start

            _asyncCaptureStream.Start()

        End Sub

        Public Sub [Stop]() Implements IStreamSource.[Stop]

            _asyncCaptureStream.SignalToStop()
            _asyncCaptureStream.Stop()

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