
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports System.Drawing.Imaging
Imports System.IO
Imports AForge.Video
Imports AForge.Video.DirectShow
Imports BarCode.Application.Entities
Imports BarCode.Application.Services

Public Class Form1

    Private deviceCollection As FilterInfoCollection
    Private videoSource As VideoCaptureDevice
    Private asyncCaptureStream As AsyncVideoSource
    Private worker As BackgroundWorker

    Private code As New BarCodeGeneratorService(BarCodeGeneratorService.QrCode)

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click

        asyncCaptureStream.Start()

        worker.RunWorkerAsync()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        PreencherCombos()

        worker = New BackgroundWorker()
        worker.WorkerReportsProgress = True
        worker.WorkerSupportsCancellation = True


        AddHandler worker.DoWork, AddressOf BackgroundWorker1_DoWork
        AddHandler worker.ProgressChanged, AddressOf BackgroundWorker1_ProgressChanged
        AddHandler worker.RunWorkerCompleted, AddressOf RunCompleted
        AddHandler asyncCaptureStream.NewFrame, AddressOf New_ImageFrame

        VideoSourcePlayer1.VideoSource = asyncCaptureStream

    End Sub

    Private Sub New_ImageFrame(sender As Object, eventArg As NewFrameEventArgs)

        Dim bitmapImage As Image = eventArg.Frame.Clone()

        PictureBox2.Image = bitmapImage

    End Sub

    Private Sub New_ImageFrameAnalyze(sender As Object, eventArg As NewFrameEventArgs)

        Dim bitmapImage As Image = eventArg.Frame.Clone()


    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) 'Handles worker.DoWork

        GetImageThread(worker)

    End Sub

    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) 'Handles worker.ProgressChanged
        ' ----- A tarefa em segundo plano atualiza a barra de progresso

        If e.ProgressPercentage = 100 Then

            TextBox1.Text = e.UserState.ToString()

            worker.CancelAsync()

        End If

    End Sub

    Private Sub RunCompleted(sender As Object, e As RunWorkerCompletedEventArgs) 'Handles worker.RunWorkerCompleted

        If e.Cancelled Then

            asyncCaptureStream.SignalToStop()
            asyncCaptureStream.Stop()

        End If

    End Sub

    Private Sub PreencherCombos()

        deviceCollection = New FilterInfoCollection(FilterCategory.VideoInputDevice)

        cboVideoSource.DataSource = deviceCollection
        cboVideoSource.ValueMember = "Name"

    End Sub

    Private Sub cboVideoSource_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboVideoSource.SelectedIndexChanged

        videoSource = New VideoCaptureDevice(deviceCollection.Item(cboVideoSource.SelectedIndex).MonikerString)
        asyncCaptureStream = New AsyncVideoSource(videoSource)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        asyncCaptureStream.SignalToStop()
        worker.CancelAsync()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        ' Using code = New BarCode.Application.Services.BarCode(New QrCode())

        PictureBox1.Image = code.Encode(TextBox2.Text)

        ' End Using

        Exit Sub

        Dim strCn As New SqlClient.SqlConnectionStringBuilder

        strCn.ConnectionString = "Data Source=.\sqlexpress;Initial Catalog=local;Integrated Security=True"

        Dim cn As New SqlClient.SqlConnection(strCn.ToString())

        'Dim ad As New SqlClient.SqlDataAdapter("Select * from BaseParaGeracaoDeCodigoDeBarras", cn)

        'Dim ds As New DataSet

        'ad.Fill(ds)

        'Dim dr As DataRow

        'dr = ds.Tables(0).NewRow()

        Dim imageStream As MemoryStream = New MemoryStream()

        PictureBox1.Image.Save(imageStream, ImageFormat.Bmp)

        'If Not PictureBox1.Image Is Nothing Then

        'End If
        'dr.Item("Imagem") = imageStream.ToArray
        'dr.Item("CodigoDoProduto") = "000001"

        'ds.Tables(0).Rows.Add(dr)
        'ds.AcceptChanges()

        'Dim build As New SqlCommandBuilder(ad)

        'ad.Update(ds, ds.Tables(0).TableName)

        Dim cmd As New SqlCommand("INSERT INTO BaseParaGeracaoDeCodigoDeBarras (CodigoDoProduto,NomeDoProduto,Quantidade,Lote,Imagem) VALUES(@CodigoDoProduto,@NomeDoProduto,@Quantidade,@Lote,@Imagem)")

        cmd.Connection = cn

        cmd.Parameters.Add(New SqlParameter("@Imagem", SqlDbType.Image)).Value = imageStream.ToArray
        cmd.Parameters.Add(New SqlParameter("@CodigoDoProduto", SqlDbType.NVarChar)).Value = "00001"
        cmd.Parameters.Add(New SqlParameter("@NomeDoProduto", SqlDbType.NVarChar)).Value = "NK073 KIT INS CHAVE ALLEN 1.5M "
        cmd.Parameters.Add(New SqlParameter("@Lote", SqlDbType.NVarChar)).Value = "3322883"
        cmd.Parameters.Add(New SqlParameter("@Quantidade", SqlDbType.Int)).Value = 5

        cn.Open()

        cmd.ExecuteNonQuery()

        cn.Close()

    End Sub

    Private Sub GetImageThread(processoAtivo As BackgroundWorker)

        While processoAtivo.CancellationPending = False

            If VideoSourcePlayer1.GetCurrentVideoFrame() IsNot Nothing Then

                Try

                    'Dim code As New BarCode.Application.Entities.QrCode

                    Dim result As Object = code.Decode(VideoSourcePlayer1.GetCurrentVideoFrame())

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


End Class
