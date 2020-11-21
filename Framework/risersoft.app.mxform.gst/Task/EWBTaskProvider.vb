Imports System.Configuration
Imports Newtonsoft.Json
Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.cloud.common
Imports risersoft.shared.portable.Models.Auth
Imports risersoft.API.GSTN
Imports GSTN.API.Library.Models.EWB
Imports risersoft.app.reports.gst
Imports risersoft.app.mxent
Imports System.IO.Compression

Public Class EWBTaskProvider
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
        Dim oProc As New clsGSTNEwayBill(myContext)
        Dim client = myContext.App.objAppExtender.FileProviderClient(myContext, ConfigurationManager.AppSettings("StorageContainerName"))
        Dim mailer As New MailModuleBase(myContext), oRetMail As clsProcOutput
        Dim MailMessage As String = ""
        Select Case myUtils.cStrTN(rTask("actionsubtype")).Trim.ToLower
            Case "generate"
                oRet = oProc.Generate(GstRegID, ReturnPeriodID, myUtils.cStrTN(rTask("filename")))
                Dim BlobName = Await client.UpLoadAsync(oRet.Description, System.IO.Path.GetFileName(oRet.Description), "")
                Dim FileUrl2 = Await client.GetDownloadUri(BlobName)
                MailMessage = String.Format("Your EWB task is finished and result is available on {0}", FileUrl2.Result.Uri)
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "EWB", MailMessage)

            Case "template", "gstn_error"
                Dim provider = New Import_EWBTaskProvider(myContext.Controller)
                Dim strFilter As String = If(myUtils.IsInList(rTask("actionsubtype"), "gstn_error"), myUtils.CombineWhere(False, "isnull(gstn_error_msg,'')<>''"), "")
                oRet = provider.GenerateTemplate($"GstRegID={GstRegID}", $"ReturnPeriodID={ReturnPeriodID}", myUtils.cStrTN(rTask("filename")), strFilter)
                Dim BlobName = Await client.UpLoadAsync(oRet.Description, System.IO.Path.GetFileName(oRet.Description), "")
                Dim FileUrl2 = Await client.GetDownloadUri(BlobName)
                MailMessage = String.Format("Your template file has been generated and is available on {0}", FileUrl2.Result.Uri)
                oRetMail = Await mailer.SendGenericMail(myUtils.cStrTN(rTask("username")), "Template", MailMessage)
            Case "downloadtp"
                Dim Operation As String = myUtils.cStrTN(myContext.SQL.ParamValue("@operation", Params))
                Dim Period As String = myUtils.cStrTN(myContext.SQL.ParamValue("@period", Params))
                Dim FromID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@from", Params))
                Dim ToID As Integer = myUtils.cValTN(myContext.SQL.ParamValue("@to", Params))
                Dim oRetPP = myFiltUI.FindPeriodID(oProc.oMaster.PostPeriodTable, Period, ReturnPeriodID, FromID, ToID)
                Dim dicFilter = myFuncs2.PopulateFilters(oProc.oMaster, oProc.DocType, GstRegID, oRetPP.IDList(0), oRetPP.IDList(1), myUtils.IsInList(Operation, "pan"))
                'oRet = Await oProc.DownloadGSTNFilter(dicFilter("cpcampusid"), dicFilter("returnperiodid"), "import", AddressOf oProc.DownloadGSTNTp)
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
