Imports System.Configuration
Imports System.Reflection
Imports Newtonsoft.Json
Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.cloud.common
Imports risersoft.shared.portable.Models.Auth

Public Class GSTR2TaskProvider
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
        Dim client = myContext.App.objAppExtender.FileProviderClient(myContext, ConfigurationManager.AppSettings("StorageContainerName"))
        Dim mailer As New MailModuleBase(myContext), oRetMail As clsProcOutput
        Dim Operation As String = myUtils.cStrTN(myContext.SQL.ParamValue("@operation", Params))
        Dim Period As String = myUtils.cStrTN(myContext.SQL.ParamValue("@period", Params))
        Dim FromID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@from", Params))
        Dim ToID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@to", Params))
        Dim oProc As New clsGSTNReturnGSTR2(myContext)
        Dim oRetPP = myFiltUI.FindPeriodID(oProc.oMaster.PostPeriodTable, Period, ReturnPeriodID, FromID, ToID)
        Dim dicFilter = myFuncs2.PopulateFilters(oProc.oMaster, oProc.DocType, GstRegID, oRetPP.IDList(0), oRetPP.IDList(1), myUtils.IsInList(Operation, "pan"))

        Dim MailMessage As String = ""
        Select Case myUtils.cStrTN(rTask("actionsubtype")).Trim.ToLower
            Case "template"
                Dim UploadType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@uploadtype", Params))
                Dim provider = CType(TaskProviderFactory.GetProvider(myContext.Controller, "Import",
                                       If(Not myUtils.IsInList(UploadType, "recon", "pv"), "IP", UploadType)), ImportTaskProviderGstBase)
                oRet = provider.GenerateTemplate(dicFilter("cpcampusid"), dicFilter("returnperiodid"), myUtils.cStrTN(rTask("filename")), "")
                Dim BlobName = Await client.UpLoadAsync(oRet.Description, System.IO.Path.GetFileName(oRet.Description), "")
                Dim FileUrl2 = Await client.GetDownloadUri(BlobName)
                MailMessage = String.Format("Your template file has been generated and is available on {0}", FileUrl2.Result.Uri)
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Template", MailMessage)

            Case "excel"
                'GSTN Summary views only for GSTIN/ret_pd
                Dim UploadType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@uploadtype", Params))
                oRet = Await oProc.GenerateExcel(dicFilter("cpcampusid"), dicFilter("returnperiodid"), myUtils.cStrTN(rTask("filename")), UploadType)
                Dim BlobName = Await client.UpLoadAsync(oRet.Description, System.IO.Path.GetFileName(oRet.Description), "")
                Dim FileUrl2 = Await client.GetDownloadUri(BlobName)
                MailMessage = String.Format("Your excel file has been generated and is available on {0}", FileUrl2.Result.Uri)
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Excel", MailMessage)

            Case "deleteinvoice"
                Dim oProc2 As New clsGSTInvoicePurch(myContext)
                oRet = oProc2.DeletePeriod(dicFilter("cpcampusid"), dicFilter("tpcampusid"), dicFilter("returnperiodid"), "", True)
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Delete", oRet.Message)

            Case "deletepayment"
                Dim oProc2 As New clsGSTAdvanceBase(myContext, "PV", "GSTR2")
                oRet = oProc2.DeletePeriod(dicFilter("cpcampusid"), dicFilter("tpcampusid"), dicFilter("returnperiodid"), "", True)
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Delete", oRet.Message)

            Case "deletecp"
                Dim oProc2 As New clsGSTInvoicePurch(myContext)
                oRet = oProc2.DeletePeriodCP(dicFilter("cpcampusid"), dicFilter("cpcampusid"), dicFilter("returnperiodid"))
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Delete", oRet.Message)

            Case "payload"
                Dim UploadType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@uploadtype", Params))
                oRet = oProc.GeneratePayload(GstRegID, ReturnPeriodID, UploadType, myUtils.cStrTN(rTask("filename")))
                Dim BlobName = Await client.UpLoadAsync(oRet.Description, System.IO.Path.GetFileName(oRet.Description), "")
                Dim FileUrl2 = Await client.GetDownloadUri(BlobName)
                MailMessage = String.Format("Your payload file has been generated and is available on {0}", FileUrl2.Result.Uri)
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Payload", MailMessage)
            Case "upload"
                oRet = oProc.UploadGSTN(GstRegID, ReturnPeriodID)
                If oRet.Success Then
                    oRetMail = oProc.CreateStatusTask(rTask, GstRegID, ReturnPeriodID)
                    MailMessage = String.Format("Your data has been uploaded to GSTN and an error report will be available shortly")
                    oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Upload", MailMessage)
                End If
            Case "downloadcp"
                Dim oRet2 = Await oProc.DownloadGSTNFilter(dicFilter("cpcampusid"), dicFilter("returnperiodid"), rTask("actionsubtype"))
                If oRet2.Success Then
                    Dim oRet3 = oProc.CreateFollowingTask(rTask, oRet2.Result, rTask("actionsubtype"))
                    Dim FileUrl2 = Await oProc.CreateStatusReport(oRet3.Description, oRet2.Result)
                    MailMessage = String.Format("Your data has been downloaded GSTN and report is available at {0}. Follow on Task has been created with success={1}", FileUrl2.Result.Uri, oRet3.Success)
                    oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Download", MailMessage)
                Else
                    MailMessage = String.Format("Your data was downloaded from GSTN with message={0} and success={1}", oRet2.Message, oRet2.Success)
                    oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Download", MailMessage)
                End If

            Case "downloadtp"
                Dim oRet2 = Await oProc.DownloadGSTNFilter($"GstRegID={GstRegID}", $"ReturnPeriodID={ReturnPeriodID}", rTask("actionsubtype"))
                If oRet2.Success Then
                    Dim oRet3 = oProc.CreateFollowingTask(rTask, oRet2.Result, rTask("actionsubtype"))
                    Dim FileUrl2 = Await oProc.CreateStatusReport(oRet3.Description, oRet2.Result)
                    MailMessage = String.Format("Your data has been downloaded GSTN and report is available at {0}. Follow on Task has been created with success={1}", FileUrl2.Result.Uri, oRet3.Success)
                    oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Download", MailMessage)
                Else
                    MailMessage = String.Format("Your data was downloaded from GSTN with message={0} and success={1}", oRet2.Message, oRet2.Success)
                    oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Download", MailMessage)
                End If
            Case "status"
                Dim ApiTaskID As Guid = Guid.Parse(myUtils.cStrTN(myContext.SQL.ParamValue("@apitaskid", Params)))
                If ApiTaskID = Guid.Empty Then
                    ApiTaskID = Guid.Parse(rTask("ApiTaskID").ToString)
                End If
                oRet = Await oProc.GetGSTNStatusAsync(GstRegID, ReturnPeriodID, ApiTaskID)
                Dim provider1 = New Import_IPTaskProvider(myContext.Controller)
                Dim oRet1 = provider1.GenerateTemplate($"GstRegID={GstRegID}", $"ReturnPeriodID={ReturnPeriodID}",
                                                       Replace(myUtils.cStrTN(rTask("filename")), ".zip", ".template.xlsx"), "gstn_status_cd='ER'")
                Dim vw = Await oProc.PrepareViewApiTask("listGSTNTransaction", ApiTaskID.ToString)
                Dim provider2 As New XLTaskProvider(myContext.Controller)
                Dim oRet2 = provider2.ExecuteExport(vw, Replace(myUtils.cStrTN(rTask("filename")), ".zip", ".status.xlsx"))


                oRet = provider1.GenerateZipFile(myUtils.cStrTN(rTask("filename")), oRet1.Description, oRet2.Description)
                Dim BlobName = Await client.UpLoadAsync(oRet.Description, System.IO.Path.GetFileName(oRet.Description), "")
                Dim FileUrl2 = Await client.GetDownloadUri(BlobName)
                MailMessage = String.Format("Your data has been uploaded to GSTN and error report is available at {0}", FileUrl2.Result.Uri)
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Upload", MailMessage)
            Case "generate"
                Dim lst = Await oProc.PrepareReconcileViews(dicFilter("cpinvoiceid"))
                Dim oRet2 = oProc.GenerateExcel(lst, myUtils.cStrTN(rTask("filename")))
                oRet = oRet + Await CommonModuleBase.SendFileMail(myContext, oRet2.Description, myUtils.cStrTN(rTask("filename")),
                                                                      myUtils.cStrTN(rTask("username")),
                               "Reconciliation", "Your invoice reconciliation report has been generated and available at {0}")
            Case "email"
                'filename moved here from model so that download link does not appear
                Dim filename As String = myContext.Controller.CalcAccountName.ToString & "-GSTR2-Reconcile-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".xlsx"
                oRet = Await myFuncs2.SendVendorEmails(myContext, oProc, dicFilter, filename)
            Case "reconcile"
                Dim UploadType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@uploadtype", Params))
                Dim oProc2 As New clsInvoicePurchRecon(myContext)
                Dim Clean As Boolean = myUtils.IsInList(UploadType, "clean")
                oRet = Await oProc2.ReconcileCP(dicFilter, myUtils.IsInList(Operation, "pan"), Clean)
                If oRet.Success Then
                    Dim lst = Await oProc.PrepareReconcileViews(dicFilter("cpinvoiceid"))
                    Dim oRet2 = oProc.GenerateExcel(lst, myUtils.cStrTN(rTask("filename")))
                    oRet = oRet + Await CommonModuleBase.SendFileMail(myContext, oRet2.Description, myUtils.cStrTN(rTask("filename")),
                                                                      myUtils.cStrTN(rTask("username")),
                               "Reconciliation", "Your invoice reconciliation task has finished and report is available at {0}")
                Else
                    oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Reconciliation", oRet.Message)
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
