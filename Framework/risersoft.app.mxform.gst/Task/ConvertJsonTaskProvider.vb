Imports Newtonsoft.Json
Imports risersoft.shared
Imports System.IO
Imports risersoft.shared.dal
Imports risersoft.app.dataporter
Imports System.Reflection
Imports risersoft.app.mxent
Imports risersoft.API.GSTN.GSTR2
Imports risersoft.shared.cloud
Imports risersoft.shared.portable.Models.Auth
Imports risersoft.API.GSTN
Imports GSTN.API.Library.Models
Imports System.Configuration

Public Class ConvertJsonTaskProvider
    Inherits clsTaskProviderBase

    Public Sub New(controller As clsAppController)
        MyBase.New(controller)

    End Sub
    Public Overrides ReadOnly Property IsApiTask As Boolean = True

    Public Overrides Async Function ExecuteServer(rTask As DataRow, input As clsProcOutput) As Task(Of clsProcOutput)
        Dim Params = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(myUtils.cStrTN(rTask("infojson")))
        Dim filepath = Await myFuncs.DownloadIfReqd(myContext, rTask, Params("path"))
        Return Await Me.ExecuteConvert(filepath, myUtils.cStrTN(rTask("filename")), myUtils.cStrTN(rTask("actionsubtype")), myUtils.cStrTN(rTask("username")))
    End Function
    Public Async Function ExecuteConvert(InputFileName As String, OutputFileName As String, DocType As String, username As String) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput, oProc As clsGSTNReturnBase
        Select Case DocType.Trim.ToLower
            Case "gstr1"
                oProc = New clsGSTNReturnGSTR1(myContext)
                oRet = oProc.ImportJson(InputFileName, False)
            Case "gstr1a"
                oProc = New clsGSTNReturnGSTR1(myContext)
                oRet = oProc.ImportJson(InputFileName, True)
            Case "gstr2"
                oProc = New clsGSTNReturnGSTR2(myContext)
                oRet = oProc.ImportJson(InputFileName, False)
            Case "gstr2a"
                oProc = New clsGSTNReturnGSTR2(myContext)
                oRet = oProc.ImportJson(InputFileName, True)
        End Select

        If oProc Is Nothing Then
            oRet.AddError("Doctype not found")
        ElseIf oRet.Success Then
            oRet = myFuncs.GenerateExcel(myContext, oRet.Data.Tables.Cast(Of DataTable).ToList, OutputFileName)
            Dim client = myContext.App.objAppExtender.FileProviderClient(myContext, ConfigurationManager.AppSettings("StorageContainerName"))
            Dim BlobName = Await client.UpLoadAsync(oRet.Description, System.IO.Path.GetFileName(oRet.Description), "")
            Dim FileUrl2 = Await client.GetDownloadUri(BlobName)


            Dim mailer As New MailModuleBase(myContext)
            Dim MailMessage = String.Format("Your excel file has been generated and is available on {0}", FileUrl2.Result.Uri)
            Dim mailerRet = Await mailer.SendGenericMail(username, "Convert 2 Excel", MailMessage)
            If mailerRet.Success Then
                oRet.AddMessage("Sent Message: " & MailMessage)
            Else
                Dim mailerMessage = String.Format("Message from SendGenericMailMandrill Message='{0}' StackTrace='{1}'", mailerRet.Message, mailerRet.StackTrace)
                oRet.AddMessage(mailerMessage)
            End If

        End If

        Return oRet

    End Function


End Class
