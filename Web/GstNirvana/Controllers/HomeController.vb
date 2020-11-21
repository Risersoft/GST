Imports risersoft.shared.web
Imports risersoft.shared.web.common
Imports risersoft.shared.portable.Models.Auth
Imports risersoft.shared
Imports GSTN.API.Library.Models.EWB
Imports Newtonsoft.Json
Imports risersoft.API.GSTN
Imports GSTN.API.Library.Models.GstNirvana
Imports risersoft.shared.dotnetfx

Public Class HomeController
    Inherits clsMvcControllerBase

    Function Index() As ActionResult
        Return View()
    End Function
    Function Parent() As ActionResult
        Return Me.Redirect("http://www.risersoft.com")
    End Function
    Function Explore() As ActionResult
        Return Me.Redirect("http://www.risersoft.com/gst")
    End Function

    Function BuyApp() As ActionResult
        Dim portal As String = Me.Host.Portal.ToRootString
        Return Me.Redirect(portal & "/account/create?license=buy&product=GstNirvana")
    End Function

    Function TryApp() As ActionResult
        Dim portal As String = Me.Host.Portal.ToRootString
        Return Me.Redirect(portal & "/account/create?license=try&product=GstNirvana")
    End Function
    Public Function QR(EwbNo As Int64, genDate As String, userGstin As String) As ActionResult
        Dim info As New EWBQRInfo With {.EwbNo = EwbNo, .GenDate = genDate, .userGstin = userGstin}
        Dim str1 = JsonConvert.SerializeObject(info)
        Dim img = GSTUtils.GenerateQR(str1)
        Dim rawfile = myAssy2.BLOBDataFromImage(img, System.Drawing.Imaging.ImageFormat.Png)
        Return File(rawfile, MimeMapping.GetMimeMapping("qrcode.png"), "qrcode.png")
    End Function
    <Authorize>
    <HttpGet> <ActionName("Convert")>
    Public Function GetConvert() As ActionResult
        Dim model As New ConvertModel
        Return View(model)
    End Function
    <Authorize>
    <HttpPost> <ActionName("Convert")> <ValidateAntiForgeryToken>
    Public Function PostConvert(model As ConvertModel) As ActionResult
        Dim mx As New MxApiClient("/api/convert")

        Select Case model.ConversionType
            Case 1
                model.TextConverted = mx.Json2CSV(model.TextToConvert, model.ReturnKey, model.TableName).Data
            Case 2
                model.TextConverted = mx.CSV2Json(model.TextToConvert, model.ReturnKey, model.TableName).Data
        End Select
        Return View(model)
    End Function
    <ActionName("CSV-Format")>
    Public Function CSVFormats(id As String) As ActionResult
        Return View(id & "-csv")
    End Function
    <ActionName("Excel-Format")>
    Public Function XLFormats(id As String) As ActionResult
        Return View(id & "-xl")
    End Function
End Class
