Imports System.Net
Imports System.Threading.Tasks
Imports System.Web.Http
Imports System.Web.Http.Results
Imports GSTN.API.Library.Models.GstNirvana
Imports Microsoft.Owin.Security.OAuth
Imports Newtonsoft.Json
Imports risersoft.API.GSTN.Public
Imports risersoft.app.mxform
Imports risersoft.app.mxform.gst
Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.dotnetfx
Imports risersoft.shared.web
Imports risersoft.shared.web.Controllers

Namespace Controllers
    <RoutePrefix("api/Data")>
    <HostAuthentication(OAuthDefaults.AuthenticationType)>
    <Authorize>
    <HasToken(GetType(RSApplication))>
    Public Class DataController
        Inherits ApiControllerBase
        Protected Friend Function CustomJson(Of TModel)(result As TModel) As JsonResult(Of TModel)
            Trace.WriteLine("Obtained result")
            Try
                Dim settings As New JsonSerializerSettings
                settings.NullValueHandling = NullValueHandling.Ignore
                'settings.DefaultValueHandling = DefaultValueHandling.Ignore
                Return Me.Json(result, settings)
            Catch ex As Exception
                Trace.WriteLine(ex.Message)
            End Try
        End Function
        Protected Function GetServerEntity() As mxgstEntities
            Dim strConn = Me.myWebController.CalcConnStringAppCode("mxgstdb", "gst")
            Dim info = Me.myWebController.GetCallerInfo
            Return New mxgstEntities(strConn.ConnectionString, info)
        End Function
        Private Async Function InitController() As Task(Of Boolean)
            Return Await Me.myWebController.CheckInitModel(New clsAppInfo With {.AppCode = "gst"},
                                                    False)
        End Function

        ' GET: api/Data/Invoice/5
        <HttpGet> <ActionName("Invoice")>
        Public Async Function GetInvoice(ByVal id As Integer) As Task(Of IHttpActionResult)
            Dim result As ResultInfo(Of GstInvoiceInfo, HttpStatusCode)
            If Await Me.InitController Then
                Try
                    Using Service = GetServerEntity()
                        Dim repo As New InvoiceRepo(Service)
                        Dim model = repo.GetInvoice(id)
                        result = BuildResponse(model)
                    End Using
                Catch ex As Exception
                    result = BuildExceptionResponse(Of GstInvoiceInfo)(ex)
                End Try
            Else
                result = BuildResponse(Of GstInvoiceInfo)(Nothing, HttpStatusCode.InternalServerError, "Error in Init")
            End If
            Return Ok(result)
        End Function
        Public Async Function PostVoucher(Of TVouch, TImport As ImportTaskProviderGstBase)(doc As TVouch) As Task(Of clsProcOutput)
            Dim oProc As New clsPOCOConverter(myWebController)
            Dim lst = New List(Of TVouch)
            lst.Add(doc)
            Dim dt1 = oProc.GenerateTable(lst)
            dt1.Columns.Add("WarningCode", GetType(String))
            dt1.Columns.Add("WarningText", GetType(String))
            dt1.Columns.Add("ErrorCode", GetType(String))
            dt1.Columns.Add("ErrorText", GetType(String))
            Dim importer = Activator.CreateInstance(GetType(TImport), New Object() {myWebController})
            importer.PopulateMaster()
            Dim oRet = Await importer.TryImportRowGroup(myWebController.DataProvider, New List(Of DataRow)(dt1.Select))
            Return oRet
        End Function


        Public Async Function PostAPIVoucher(Of TVouch, TImport As ImportTaskProviderGstBase)(doc As TVouch) As Task(Of IHttpActionResult)
            Dim result As ResultInfo(Of clsProcOutput, HttpStatusCode)
            Dim oRet As New clsProcOutput
            If Await Me.InitController Then
                Try
                    oRet = Await Me.PostVoucher(Of TVouch, TImport)(doc)
                    result = BuildResponse(Of clsProcOutput)(oRet)
                Catch ex As Exception
                    result = BuildExceptionResponse(Of clsProcOutput)(ex)
                End Try
            Else
                result = BuildResponse(Of clsProcOutput)(oRet, HttpStatusCode.InternalServerError, "Error in Init")
            End If
            Return Me.CustomJson(result)

        End Function

        ' POST: api/Data/GSTR1/Save
        <HttpPost> <Route("GSTR1/Save")>
        Public Async Function PostInvoiceIS(<FromBody()> doc As GstSaleInvoiceInfo) As Task(Of IHttpActionResult)
            Return Await Me.PostAPIVoucher(Of GstSaleInvoiceInfo, Import_ISTaskProvider)(doc)
        End Function

        ' POST: api/Data/GSTR2/Save
        <HttpPost> <Route("GSTR2/Save")>
        Public Async Function PostInvoiceIP(<FromBody()> doc As GstPurchaseInvoiceInfo) As Task(Of IHttpActionResult)
            Return Await Me.PostAPIVoucher(Of GstPurchaseInvoiceInfo, Import_IPTaskProvider)(doc)
        End Function

        ' POST: api/Data/PC/Save
        <HttpPost> <Route("PC/Save")>
        Public Async Function PostAdvancePC(<FromBody()> doc As GstPaymentCustomerInfo) As Task(Of IHttpActionResult)
            Return Await Me.PostAPIVoucher(Of GstPaymentCustomerInfo, Import_PCTaskProvider)(doc)
        End Function


        ' POST: api/Data/PV/Save
        <HttpPost> <Route("PV/Save")>
        Public Async Function PostAdvancePV(<FromBody()> doc As GstPaymentVendorInfo) As Task(Of IHttpActionResult)
            Return Await Me.PostAPIVoucher(Of GstPaymentVendorInfo, Import_PVTaskProvider)(doc)
        End Function

        ' GET: api/Data/Party/5
        <HttpGet> <ActionName("Party")>
        Public Async Function GetParty(ByVal id As Integer) As Task(Of IHttpActionResult)
            Dim result As ResultInfo(Of GstPartyInfo, HttpStatusCode)
            If Await Me.InitController Then
            End If
            Return Ok(result)
        End Function

        ' POST: api/Data/Party
        <HttpPost> <ActionName("Party")>
        Public Async Function PostParty(<FromBody()> party As GstPartyInfo) As Task(Of IHttpActionResult)
            Dim result As ResultInfo(Of Integer, HttpStatusCode)
            If Await Me.InitController Then
            End If
            Return Ok(result)
        End Function


        ' GET: api/Data/GSTRegInfo/33GSPTN0191G1ZB
        <HttpGet> <ActionName("GstRegInfo")>
        Public Async Function GetGstRegInfo(ByVal GSTIN As String) As Task(Of IHttpActionResult)
            Dim result As ResultInfo(Of TaxPayerModel, HttpStatusCode)
            If Await Me.InitController Then
                Try
                    Dim ctx = Me.GetServerEntity
                    Dim oProc As New clsGSTNPublic(Me.myWebController)
                    Dim model = oProc.SearchGSTIN(GSTIN)
                    result = BuildResponse(model.Data,, model.ErrorMessage)

                Catch ex As Exception
                    result = BuildExceptionResponse(Of TaxPayerModel)(ex)
                End Try
            Else
                result = BuildResponse(Of TaxPayerModel)(Nothing, HttpStatusCode.InternalServerError, "Error in Init")
            End If
            Return Ok(result)
        End Function
    End Class
End Namespace