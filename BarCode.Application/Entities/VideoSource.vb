Imports System.Drawing
Imports System.Windows.Forms
Imports AForge.Video
Imports AForge.Controls
Imports AForge.Video.DirectShow


Namespace Entities
    Public Class VideoSource
        
        Private ReadOnly _frameBox As VideoSourcePlayer
        Private _captureSource As VideoCaptureDevice
        Private _filter As FilterInfo
        
        Public ReadOnly Property ListOfDevices As IEnumerable
            Get
                Return New FilterInfoCollection(FilterCategory.VideoInputDevice)
            End Get
        End Property
  
        Private _asyncCaptureStream As IVideoSource

        Sub New(frameBox As Control)

            _frameBox = frameBox
            
        End Sub

        Public Sub AddCaptureStream(sourceStream As Object)

            _filter = sourceStream
            _captureSource = New VideoCaptureDevice(_filter.MonikerString)
            _asyncCaptureStream = New AsyncVideoSource(_captureSource)
            _frameBox.VideoSource = _asyncCaptureStream

        End Sub

        Public Sub Start()

            _asyncCaptureStream.Start()

        End Sub

        Public Sub [Stop]()

            _asyncCaptureStream.SignalToStop()
            _asyncCaptureStream.Stop()

        End Sub

    End Class

End Namespace