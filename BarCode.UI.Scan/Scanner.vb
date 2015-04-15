
Imports System.ComponentModel
Imports BarCode.Application.Entities
Imports BarCode.Application.Services
Imports BarCode.Video.Infra
Imports BarCode.Video.Services

Public Class Scanner

    Private _worker As BackgroundWorker
    Private _video As IVideoService
    Private _operation As IBarCodeService

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        If Not _worker.IsBusy Then
            _operation = New BarCodeService(New QrCode(QrCode.Decode))

            _video = New VideoService(New VideoCapture(VideoSource, cboVideoSource.SelectedItem))

            _video.Start()

            _worker.RunWorkerAsync()
        End If
    End Sub

    Private Sub Scanner_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If _worker.IsBusy = True Then
            _worker.CancelAsync()
            _video.Stop()
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        PreencherCombos()

        _worker = New BackgroundWorker()
        _worker.WorkerReportsProgress = True
        _worker.WorkerSupportsCancellation = True

        AddHandler _worker.DoWork, AddressOf BackgroundWorker_DoWork
        AddHandler _worker.ProgressChanged, AddressOf BackgroundWorker_ProgressChanged
        AddHandler _worker.RunWorkerCompleted, AddressOf RunCompleted

    End Sub

    Private Sub BackgroundWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)

        ProcessImage(_worker)

    End Sub

    Private Sub BackgroundWorker_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)

        If e.ProgressPercentage = 100 Then

            txtResult.Text = e.UserState.ToString()
            _worker.CancelAsync()

        End If

    End Sub

    Private Sub RunCompleted(sender As Object, e As RunWorkerCompletedEventArgs)

        _video.Stop()
        _operation.Dispose()

    End Sub

    Private Sub PreencherCombos()

        Dim devices As New Devices(DeviceType.VideoInput)

        cboVideoSource.DataSource = devices.Get
        cboVideoSource.DisplayMember = "Name"

    End Sub

    Private Sub bntStop_Click(sender As Object, e As EventArgs) Handles bntStop.Click

        _worker.CancelAsync()
        _video.Stop()

    End Sub

    Private Sub ProcessImage(activeProcess As BackgroundWorker)

        While activeProcess.CancellationPending = False

            If CurrentFrame() IsNot Nothing Then

                Try

                    Dim result As Object = _operation.Execute(CurrentFrame)

                    If Not result Is Nothing OrElse result Is String.Empty Then
                        activeProcess.ReportProgress(100, result)
                    End If

                Catch
                    activeProcess.ReportProgress(0, String.Empty)
                End Try

            End If

        End While

    End Sub

    Private Function CurrentFrame() As Object
        Return VideoSource.GetCurrentVideoFrame()
    End Function

End Class
