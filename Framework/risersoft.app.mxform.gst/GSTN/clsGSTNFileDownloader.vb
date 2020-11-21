Imports risersoft.shared
Imports risersoft.API.GSTN
Imports risersoft.shared.portable.Contracts

Public Class clsGSTNFileDownloader(Of TData)
    Inherits WebApiClientToken
    Dim auth As IGSTNAuthProvider
    Public Sub New(auth As IServiceAuthorizer)
        MyBase.New(auth)
    End Sub
    Public Async Function DownloadFile(RemoteUrl As String, FolderPath As String, ek As String) As Task(Of GSTNDataFile(Of TData))
        Me.PrepareQueryString(RemoteUrl, New Dictionary(Of String, String)())
        Dim ekBytes As Byte() = Convert.FromBase64String(ek)
        Dim result As New GSTNDataFile(Of TData)
        result.FileName = RemoteUrl
        For tryCounter As Integer = 1 To 3
            Trace.WriteLine("FILEDET: Downloading file from " & RemoteUrl & "Count=" & tryCounter)

            Try
                Dim content = Me.[Get](Of System.IO.Stream)()

                If content IsNot Nothing Then

                    content.Seek(0, System.IO.SeekOrigin.Begin)

                    Using gZipInputStream = New ICSharpCode.SharpZipLib.GZip.GZipInputStream(content)

                        Using tarInputStream = New ICSharpCode.SharpZipLib.Tar.TarInputStream(gZipInputStream)
                            Dim tarEntry As ICSharpCode.SharpZipLib.Tar.TarEntry = tarInputStream.GetNextEntry()

                            While tarEntry IsNot Nothing
                                If Not tarEntry.IsDirectory Then
                                    result.FileName = System.IO.Path.GetFileName(tarEntry.Name)
                                    Using memoryStream = New System.IO.MemoryStream()
                                        tarInputStream.CopyEntryContents(memoryStream)
                                        memoryStream.Seek(0, System.IO.SeekOrigin.Begin)

                                        Using streamReader = New System.IO.StreamReader(memoryStream)
                                            Dim encryptedJsonText As String = streamReader.ReadToEnd().Replace("""", String.Empty)
                                            Dim base64DecryptedJsonBytes As Byte() = EncryptionUtils.AesDecrypt(encryptedJsonText, ekBytes)
                                            Dim base64DecryptedJsonText As String = System.Text.UTF8Encoding.UTF8.GetString(base64DecryptedJsonBytes)
                                            Dim decryptedJsonBytes As Byte() = Convert.FromBase64String(base64DecryptedJsonText)
                                            Dim decryptedJsonText As String = System.Text.Encoding.UTF8.GetString(decryptedJsonBytes)

                                            Using streamWriter = System.IO.File.CreateText(System.IO.Path.Combine(FolderPath, System.IO.Path.GetFileName(tarEntry.Name)))
                                                streamWriter.Write(decryptedJsonText)
                                            End Using

                                            result.Data = Newtonsoft.Json.JsonConvert.DeserializeObject(Of TData)(decryptedJsonText)
                                        End Using
                                    End Using
                                End If
                                tarEntry = tarInputStream.GetNextEntry()
                            End While

                            Exit For
                        End Using
                    End Using

                Else
                    Throw New Exception("Could not download")
                End If

            Catch ex As Exception
                If tryCounter = 3 Then result.Message = ex.Message
                Trace.WriteLine("FILEDET: " & ex.Message)
            End Try
            Await Task.Delay(1000)
        Next
        Return result
    End Function
End Class
