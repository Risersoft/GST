﻿Imports System.Net
Imports Newtonsoft.Json
Imports risersoft.app.mxform.gst
Imports risersoft.shared
Imports risersoft.shared.dal
Imports risersoft.shared.web
Imports System.Threading.Tasks
Imports risersoft.shared.dotnetfx

Namespace Controllers
    Public Class frmGstImportVouchOldController
        Inherits clsMvcControllerBase
        Public Function Test() As ActionResult
            Return View("UploadTest")
        End Function




        <Authorize> <HostActionFilter> <HttpPost> <ActionName("FileTest")>
        Public Function PostFileUpload(ReturnKey As String, filename As HttpPostedFileBase) As ActionResult
            Return Me.Content("Done")
        End Function
        Public Function Edit() As ActionResult
            Return RedirectToAction("Index")
        End Function
        Public Function Create() As ActionResult
            Return RedirectToAction("Index")
        End Function

        <Authorize> <HostActionFilter> <ActionName("Index")>
        Public Async Function GetIndex() As Task(Of ActionResult)
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then

                Return View()

            End If
        End Function

        <Authorize> <HostActionFilter> <HttpPost> <ActionName("Index")>
        Public Async Function PostIndex(ReturnKey As String, TableName As String, data As String) As Task(Of ActionResult)
            Dim result As New JsonResult()
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, False) Then
                Dim model As New frmGstImportVouchModel(Me.myWebController)
                Dim vw As New clsViewModel(Me.myWebController)
                Dim dsImport = JsonConvert.DeserializeObject(Of DataSet)(data)
                If model.PrepForm(vw, EnumfrmMode.acAddM, "") Then
                    model.DatasetCollection.AddUpd("import", dsImport)
                    If model.Validate() Then
                        If model.VSave() Then
                            result.Data = New With {.status = HttpStatusCode.OK}
                        Else
                            result.Data = New With {.status = HttpStatusCode.InternalServerError}
                        End If
                    Else
                        result.Data = New With {.status = HttpStatusCode.InternalServerError}
                    End If
                End If


            End If
            'Me.Request.Content.ReadAsStringAsync.Result
            Return result
            'Return RedirectToAction("Index")

        End Function
        ' POST: frmImport/File
        <Authorize> <HostActionFilter> <HttpPost> <ActionName("File")>
        Public Async Function PostFile(ReturnKey As String, filename As HttpPostedFileBase) As Task(Of ActionResult)
            Dim result As New JsonResult()
            Dim dsImport As New DataSet
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, False) Then
                Try
                    Dim objImporter As New clsSSGImport(Me.myWebController)
                    objImporter.OpenStream(filename.InputStream)
                    myUtils.AddTable(dsImport, objImporter.GenerateTableFromRange("customer"), "customer")
                    myUtils.AddTable(dsImport, objImporter.GenerateTableFromRange("vendor"), "vendor")
                    myUtils.AddTable(dsImport, objImporter.GenerateTableFromRange("invoice"), "inv")
                    myUtils.AddTable(dsImport, objImporter.GenerateTableFromRange("advance"), "at")
                    myUtils.AddTable(dsImport, objImporter.GenerateTableFromRange("taxpaid"), "txpd")

                    Dim model As New frmGstImportVouchModel(Me.myWebController)
                    Dim vw As New clsViewModel(Me.myWebController)
                    If model.PrepForm(vw, EnumfrmMode.acAddM, "") Then
                        model.DatasetCollection.AddUpd("import", dsImport)
                        If model.Validate() Then
                            result.Data = New With {.status = HttpStatusCode.OK, .data = JsonConvert.SerializeObject(dsImport)}
                        Else
                            result.Data = New With {.status = HttpStatusCode.OK,
                    .data = JsonConvert.SerializeObject(dsImport)}
                        End If
                    End If
                Catch ex As Exception
                    result.Data = New With {.status = HttpStatusCode.InternalServerError,
                    .data = JsonConvert.SerializeObject(dsImport), .message = ex.Message}
                End Try
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet

            End If

            Return result



        End Function

    End Class
End Namespace