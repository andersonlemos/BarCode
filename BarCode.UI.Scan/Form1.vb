
Imports System.ComponentModel

Imports BarCode.Application.Entities
Imports BarCode.Application.Services

Public Class Form1

    Private worker As BackgroundWorker

    Private video As BarCode.Application.Entities.VideoSource

    Private code As New BarCodeService(BarCodeService.QR_CODE)

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click

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

    End Sub

    Private Sub PreencherCombos()

        Me.cboVideoSource.DataSource = video.ListOfDevices
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

                    Dim result As Object = code.Decode(CurrentFrame)

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
