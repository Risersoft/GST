Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports System.Web.Http.Controllers
Imports GSTN.API.Library.Models.EWB
Imports Microsoft.Owin.Security.OAuth
Imports risersoft.app.mxform.gst
Imports risersoft.shared
Imports risersoft.shared.web
Imports risersoft.shared.web.Controllers
Imports System.Threading.Tasks
''' <summary>
''' Account Controller
''' </summary>
''' <remarks></remarks>
<RoutePrefix("api/EWB")>
<HostAuthentication(OAuthDefaults.AuthenticationType)>
<RSAuthorize>
<HasToken(GetType(RSApplication))>
Public Class EWBController
    Inherits ServerApiController(Of EWBInfo, Int64, GenerateEWBInfo, EWBPostResponseInfo, HttpStatusCode, IEWBRepository)

    Public Sub New(repository As IEWBRepository)
        MyBase.New(repository)
    End Sub
    Public Overrides Sub OnActionExecuting(actionContext As HttpActionContext)
        MyBase.OnActionExecuting(actionContext)
        repository.WebController.CheckInitModel(New clsAppInfo With {.AppCode = "gst"},
                                                True)

    End Sub
    <HttpGet>
    <Route("PDF")>
    Public Async Function GetPDF(ewbnum As Int64) As Task(Of HttpResponseMessage)
        Dim result As HttpResponseMessage = Nothing
        ' Serve the file to the client
        result = Request.CreateResponse(HttpStatusCode.OK)
        result.Content = New StreamContent(Await repository.GeneratePDF(ewbnum))
        result.Content.Headers.ContentDisposition = New System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
        result.Content.Headers.ContentDisposition.FileName = "ewb_" & ewbnum & ".pdf"

        Return result

    End Function
    <HttpPost>
    <Route("Cancel")>
    Public Function Cancel(info As EWBCancelRequestInfo) As IHttpActionResult
        Dim result = repository.Cancel(info)
        Return Ok(result)
    End Function
    <HttpGet>
    <Route("Clear")>
    Public Function Clear(GSTIN As String) As IHttpActionResult
        Dim result = repository.Clear(GSTIN)
        Return Ok(result)
    End Function

End Class
