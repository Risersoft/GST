Imports System.Configuration
Imports System.Reflection
Imports Newtonsoft.Json
Imports risersoft.app.mxent
Imports risersoft.app.reports.gst
Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.dal
Imports risersoft.shared.portable.Models.Auth

Public Class ANX01TaskProvider
    Inherits clsTaskProviderBase
    Public Overrides ReadOnly Property IsApiTask As Boolean = True

    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
    End Sub


    Public Overrides Async Function ExecuteServer(rTask As DataRow, input As clsProcOutput) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput
        Dim Params = JsonConvert.DeserializeObject(Of List(Of clsSQLParam))(myUtils.cStrTN(rTask("infojson")))
        Dim GstRegM As String = myUtils.cStrTN(myContext.SQL.ParamValue("@gstregm", Params))
        Dim GstRegID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@gstregid", Params))
        Dim ReturnPeriodID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@returnperiodid", Params))
        Dim client = myContext.App.objAppExtender.FileProviderClient(myContext, ConfigurationManager.AppSettings("StorageContainerName"))
        Dim mailer As New MailModuleBase(myContext), oRetMail As clsProcOutput
        Dim MailMessage As String = ""
        Select Case myUtils.cStrTN(rTask("actionsubtype")).Trim.ToLower
            Case "delete"
                Dim oProc As New clsGSTInvoiceSale(myContext)
                oRet = oProc.DeletePeriod($"GstRegID={GstRegID}", $"campusid in (select campusid from campus where GstRegID={GstRegID})", $"ReturnPeriodID={ReturnPeriodID}", "", True)
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Delete", oRet.Message)
            Case "deleteimportonline"
                Dim oProc As New clsGSTInvoiceSale(myContext)
                oRet = oProc.DeletePeriod($"GstRegID={GstRegID}", $"campusid in (select campusid from campus where GstRegID={GstRegID})", $"ReturnPeriodID={ReturnPeriodID}", "isnull(gstnflag,0)=0", True)
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Delete", oRet.Message)
            Case "deletecp"
                Dim oProc As New clsGSTInvoiceSale(myContext)
                oRet = oProc.DeletePeriodCP($"GstRegID={GstRegID}", $"GstRegID={GstRegID}", $"ReturnPeriodID={ReturnPeriodID}")
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Delete", oRet.Message)
            Case "template", "gstn_error"
                'TODO: Make arrangement of table 3H-3K
                Dim UploadType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@uploadtype", Params))
                Dim provider = CType(If(myUtils.IsInList(UploadType, "PC"), New Import_PCTaskProvider(myContext.Controller),
                                        New Import_ISTaskProvider(myContext.Controller)), ImportTaskProviderGstBase)
                Dim strFilter As String = If(myUtils.IsInList(rTask("actionsubtype"), "gstn_error"), myUtils.CombineWhere(False, "gstn_status_cd ='ER'",
                                                     If(myUtils.IsInList(UploadType, "IS"), "", "GSTInvoiceType='" & UploadType & "'")), "")
                oRet = provider.GenerateTemplate($"GstRegID={GstRegID}", $"ReturnPeriodID={ReturnPeriodID}", myUtils.cStrTN(rTask("filename")), strFilter)
                Dim BlobName = Await client.UpLoadAsync(oRet.Description, System.IO.Path.GetFileName(oRet.Description), "")
                Dim FileUrl2 = Await client.GetDownloadUri(BlobName)
                MailMessage = String.Format("Your template file has been generated and is available on {0}", FileUrl2.Result.Uri)
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Template", MailMessage)

            Case "excel"
                Dim oProc As New clsGSTNReturnANX01(myContext)
                Dim UploadType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@uploadtype", Params))
                oRet = Await oProc.GenerateExcel($"gstregid={GstRegID}", $"ReturnPeriodID={ReturnPeriodID}", myUtils.cStrTN(rTask("filename")), UploadType)
                Dim BlobName = Await client.UpLoadAsync(oRet.Description, System.IO.Path.GetFileName(oRet.Description), "")
                Dim FileUrl2 = Await client.GetDownloadUri(BlobName)
                MailMessage = String.Format("Your excel file has been generated and is available on {0}", FileUrl2.Result.Uri)
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Excel", MailMessage)

            Case "pdf"
                Dim oMaster As New clsMasterDataFICO(myContext)
                Dim rGstPP = oMaster.GstRegPPRow(GstRegID, ReturnPeriodID)
                Dim UploadType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@uploadtype", Params))
                Dim provider As New gstReportDataProvider(myContext)
                Dim vwModel As New clsViewModel(myContext)
                Dim fRow As DataRow = myContext.AppModel.FrmPrnRowKey("crpreturnanx01", "")

                Dim Model = provider.GenerateReportModel(myContext.GetAppInfo, fRow, vwModel, rGstPP("gstregppid"))
                Dim NewFileName = myContext.App.objAppExtender.MapPath("app_data/payload/" & myUtils.cStrTN(rTask("filename")))
                myContext.FTP.EnsureLocalDirectory(System.IO.Path.GetDirectoryName(NewFileName))
                oRet = myContext.PrintingPress.SpecReportFile(vwModel, fRow, Model, NewFileName)
                If oRet.Success Then
                    Dim BlobName = Await client.UpLoadAsync(oRet.Description, System.IO.Path.GetFileName(oRet.Description), "")
                    Dim FileUrl2 = Await client.GetDownloadUri(BlobName)
                    MailMessage = String.Format("Your PDF file has been generated and is available on {0}", FileUrl2.Result.Uri)
                    oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "PDF", MailMessage)
                End If


            Case "payload"
                Dim oProc As New clsGSTNReturnANX01(myContext)
                Dim UploadType As String = myUtils.cStrTN(myContext.SQL.ParamValue("@uploadtype", Params))
                oRet = oProc.GeneratePayload(GstRegID, ReturnPeriodID, UploadType, myUtils.cStrTN(rTask("filename")))
                Dim BlobName = Await client.UpLoadAsync(oRet.Description, System.IO.Path.GetFileName(oRet.Description), "")
                Dim FileUrl2 = Await client.GetDownloadUri(BlobName)
                MailMessage = String.Format("Your payload file has been generated and is available on {0}", FileUrl2.Result.Uri)
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Payload", MailMessage)
            Case "uploadm"
                Dim oProc As New clsGSTNReturnANX01(myContext)
                Dim arr() = GstRegM.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                For Each str1 As String In arr
                    Dim oRet2 As New clsProcOutput
                    Try
                        oRet2 = oProc.UploadGSTN(myUtils.cValTN(str1), ReturnPeriodID)
                    Catch ex As Exception
                        oRet2.AddException(ex)
                    End Try
                    If oRet2.Success Then oRet2 = oProc.CreateStatusTask(rTask, GstRegID, ReturnPeriodID)
                    oRet.AddOutput(oRet2)
                Next
                If oRet.Success Then
                    MailMessage = String.Format("Your data has been uploaded to GSTN and GSTN status results will be available soon")
                Else
                    MailMessage = String.Format("Your data has was uploaded to GSTN with errors: {0}", oRet.Message)
                End If
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Upload", MailMessage)
            Case "upload"
                Dim oProc As New clsGSTNReturnANX01(myContext)
                oRet = oProc.UploadGSTN(GstRegID, ReturnPeriodID)
                If oRet.Success Then oRet = oRet + oProc.CreateStatusTask(rTask, GstRegID, ReturnPeriodID)
                If oRet.Success Then
                    MailMessage = String.Format("Your data has been uploaded to GSTN and an error report will be available soon")
                Else
                    MailMessage = String.Format("Your data has was uploaded to GSTN with error {0}", oRet.Message)
                End If
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Upload", MailMessage)

            Case "downloadcp"
                Dim oProc As New clsGSTNReturnANX01(myContext)
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

            Case "clean"
                Dim oProc As New clsGSTNReturnANX01(myContext)
                Dim oRet2 = Await oProc.DownloadGSTNFilter($"GstRegID={GstRegID}", $"ReturnPeriodID={ReturnPeriodID}", "clean")
                If oRet2.Success Then
                    Dim oRet3 = oProc.CleanGSTN(oRet2.Result)
                    oRetMail = oProc.CreateStatusTask(rTask, GstRegID, ReturnPeriodID)
                    MailMessage = String.Format("Your data has been cleaned from GSTN and a status mail will be available soon")
                    oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Clean", MailMessage)
                Else
                    MailMessage = String.Format("Your data was downloaded from GSTN with message={0} and success={1}", oRet2.Message, oRet2.Success)
                    oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Download", MailMessage)
                End If
            Case "downloadtp"
                Dim Operation As String = myUtils.cStrTN(myContext.SQL.ParamValue("@operation", Params))
                Dim Period As String = myUtils.cStrTN(myContext.SQL.ParamValue("@period", Params))
                Dim FromID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@from", Params))
                Dim ToID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@to", Params))
                Dim oProc As New clsGSTNReturnANX01(myContext)
                Dim oRetPP = myFiltUI.FindPeriodID(oProc.oMaster.PostPeriodTable, Period, ReturnPeriodID, FromID, ToID)
                Dim dicFilter = myFuncs2.PopulateFilters(oProc.oMaster, oProc.DocType, GstRegID, oRetPP.IDList(0), oRetPP.IDList(1), myUtils.IsInList(Operation, "pan"))
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
            Case "status"
                Dim oProc As New clsGSTNReturnANX01(myContext)
                Dim ApiTaskID As Guid = Guid.Parse(myUtils.cStrTN(myContext.SQL.ParamValue("@apitaskid", Params)))
                If ApiTaskID = Guid.Empty Then
                    ApiTaskID = Guid.Parse(rTask("ApiTaskID").ToString)
                End If
                oRet = Await oProc.GetGSTNStatusAsync(GstRegID, ReturnPeriodID, ApiTaskID)
                Dim provider1 = New Import_ISTaskProvider(myContext.Controller)
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


            Case "reconcile"

                Dim oProc As New clsInvoiceSaleRecon(myContext)
                oRet = Await oProc.ReconcileCP(GstRegID, ReturnPeriodID, ReturnPeriodID, True, True)
                If oRet.Success Then
                    Dim oProc2 As New clsGSTNReturnANX01(myContext)
                    Dim vw = Await oProc2.PrepareViewPAN("listsaleinvoicematch", GstRegID, ReturnPeriodID, ReturnPeriodID)
                    Dim provider As New XLTaskProvider(myContext.Controller)
                    oRet = oRet + Await provider.ExecuteExportAndMail(vw, myUtils.cStrTN(rTask("filename")), myUtils.cStrTN(rTask("username")),
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
