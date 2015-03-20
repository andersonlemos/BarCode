Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO

Namespace Entities.Services

    Friend MustInherit Class ImageExtensions

        Private Shared _byteArray As Byte()
       
        Public Shared Function ImageToByte(image As Image) As Byte()

            Using memoryStream = New MemoryStream()

                image.Save(memoryStream, ImageFormat.Bmp)

                _byteArray = memoryStream.ToArray()

                Return _byteArray

            End Using

        End Function

    End Class

End Namespace