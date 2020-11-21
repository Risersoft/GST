Imports System.Configuration
Imports Newtonsoft.Json
Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.portable.Models.Auth

Public Class GSTR3BTaskProvider
    Inherits clsTaskProviderBase
    Public Overrides ReadOnly Property IsApiTask As Boolean = True

    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
    End Sub

    Public Overrides Async Function ExecuteServer(rTask As DataRow, input As clsProcOutput) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput
        Dim Params = JsonConvert.DeserializeObject(Of List(Of clsSQLParam))(myUtils.cStrTN(rTask("infojson")))
        Dim GstRegID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@gstregid", Params))
        Dim ReturnPeriodID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@returnperiodid", Params))
        Dim oProc As New clsGSTNReturnGSTR3B(myContext)
        Dim client = myContext.App.objAppExtender.FileProviderClient(myContext, ConfigurationManager.AppSettings("StorageContainerName"))
        Dim mailer As New MailModuleBase(myContext), oRetMail As clsProcOutput
        Dim MailMessage As String = ""
        Select Case myUtils.cStrTN(rTask("actionsubtype")).Trim.ToLower
            Case "excel"
                Dim UploadType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@uploadtype", Params))
                oRet = Await oProc.GenerateExcel($"gstregid={GstRegID}", $"ReturnPeriodID={ReturnPeriodID}", myUtils.cStrTN(rTask("filename")), UploadType)
                Dim BlobName = Await client.UpLoadAsync(oRet.Description, System.IO.Path.GetFileName(oRet.Description), "")
                Dim FileUrl2 = Await client.GetDownloadUri(BlobName)
                MailMessage = String.Format("Your excel file has been generated and is available on {0}", FileUrl2.Result.Uri)
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Excel", MailMessage)


            Case "payload"
                Dim UploadType As String = "UM"
                oRet = oProc.GeneratePayload(GstRegID, ReturnPeriodID, UploadType, myUtils.cStrTN(rTask("filename")))
                Dim BlobName = Await client.UpLoadAsync(oRet.Description, System.IO.Path.GetFileName(oRet.Description), "")
                Dim FileUrl2 = Await client.GetDownloadUri(BlobName)
                MailMessage = String.Format("Your payload file has been generated and is available on {0}", FileUrl2.Result.Uri)
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Payload", MailMessage)

            Case "upload"
                oRet = oProc.UploadGSTN(GstRegID, ReturnPeriodID)
                If oRet.Success Then
                    oRetMail = oProc.CreateStatusTask(rTask, GstRegID, ReturnPeriodID)
                    MailMessage = String.Format("Your data has been uploaded to GSTN")
                    oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Upload", MailMessage)
                End If
            Case "status"
                Dim ApiTaskID As Guid = Guid.Parse(myUtils.cStrTN(myContext.SQL.ParamValue("@apitaskid", Params)))
                oRet = Await oProc.GetGSTNStatusAsync(GstRegID, ReturnPeriodID, ApiTaskID)
                MailMessage = oRet.Message
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "GSTR3B Status", MailMessage)

            Case "downloadtp"
                Dim Operation As String = myUtils.cStrTN(myContext.SQL.ParamValue("@operation", Params))
                Dim Period As String = myUtils.cStrTN(myContext.SQL.ParamValue("@period", Params))
                Dim FromID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@from", Params))
                Dim ToID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@to", Params))
                Dim oRetPP = myFiltUI.FindPeriodID(oProc.oMaster.PostPeriodTable, Period, ReturnPeriodID, FromID, ToID)
                Dim dicFilter = myFuncs2.PopulateFilters(oProc.oMaster, oProc.DocType, GstRegID, oRetPP.IDList(0), oRetPP.IDList(1), myUtils.IsInList(Operation, "pan"))
                Dim oRet2 = Await oProc.DownloadGSTNFilter(dicFilter("cpcampusid"), dicFilter("returnperiodid"), rTask("actionsubtype"))
                If oRet2.Success Then
                    Dim lst = Await oProc.UpdateDownloadedDataTP(oRet2.Result)
                    MailMessage = String.Format("Your data has been downloaded GSTN")
                    oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Download", MailMessage)
                Else
                    MailMessage = String.Format("Your data was downloaded from GSTN with message={0} and success={1}", oRet2.Message, oRet2.Success)
                    oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Download", MailMessage)
                End If

        End Select
        If Not oRetMail Is Nothing Then
            If oRetMail.Success Then
                oRet.AddMessage("Sent Message: " & oRetMail.Message & ", " & MailMessage)
            Else
                Dim mailerMessage = String.Format("Message from SendGenericMailMandrill Message='{0}' StackTrace='{1}'", oRetMail.Message, oRetMail.StackTrace)
                oRet.AddMessage(mailerMessage)
            End If
        End If

        Return oRet
    End Function



End Class
