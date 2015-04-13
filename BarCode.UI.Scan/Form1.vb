
Imports System.ComponentModel

Imports BarCode.Application.Entities
Imports BarCode.Application.Services
Imports BarCode.Video.Infra

Public Class Form1

    Private worker As BackgroundWorker

    Private video As BarCode.Video.Infra.VideoSource

    Private _operation As IBarCodeService

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click

        _operation = New BarCodeService(New QrCode(QrCode.Decode))

        video.AddCaptureStream(cboVideoSource.SelectedItem)

        video.Start()

        worker.RunWorkerAsync()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        video = New VideoSource(Me.VideoSourcePlayer1)

        PreencherCombos()

        worker = New BackgroundWorker()
        worker.WorkerReportsProgress = True
        worker.WorkerSupportsCancellation = True

        AddHandler worker.DoWork, AddressOf BackgroundWorker1_DoWork
        AddHandler worker.ProgressChanged, AddressOf BackgroundWorker1_ProgressChanged
        AddHandler worker.RunWorkerCompleted, AddressOf RunCompleted

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)

        ProcessImage(worker)

    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs)

        If e.ProgressPercentage = 100 Then

            TextBox1.Text = e.UserState.ToString()
            worker.CancelAsync()

        End If

    End Sub

    Private Sub RunCompleted(sender As Object, e As RunWorkerCompletedEventArgs)

        video.Stop()
        _operation.dispose()
    End Sub

    Private Sub PreencherCombos()

        Dim devices As New Devices(DeviceType.VideoInput)

        Me.cboVideoSource.DataSource = devices.Get
        Me.cboVideoSource.DisplayMember = "Name"

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        worker.CancelAsync()
        video.Stop()

    End Sub

    Private Sub ProcessImage(processoAtivo As BackgroundWorker)

        While processoAtivo.CancellationPending = False

            If CurrentFrame() IsNot Nothing Then

                Try

                    Dim result As Object = _operation.Execute(CurrentFrame)

                    If Not result Is Nothing OrElse result Is String.Empty Then
                        processoAtivo.ReportProgress(100, result)
                    Else
                        processoAtivo.ReportProgress(0, String.Empty)
                    End If

                Catch
                    processoAtivo.ReportProgress(0, String.Empty)
                End Try

            End If

        End While

    End Sub

    Private Function CurrentFrame() As Object
        Return VideoSourcePlayer1.GetCurrentVideoFrame()
    End Function

End Class
