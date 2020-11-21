Imports System.Net
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports risersoft.shared.web
Imports risersoft.shared
Imports risersoft.app.mxform.gst
Imports GSTN.API.Library.Models.GstNirvana
Imports risersoft.app.mxent

Namespace Controllers
    Public Class frmGstDocNumTemplateController
        Inherits clsMvcControllerBase

        <PreserveQueryString>
        Public Function Create() As ActionResult
            Return RedirectToAction("Crud")
        End Function
        <Authorize> <HostActionFilter> <ActionName("CRUD")>
        Public Async Function Index() As Task(Of ActionResult)
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Dim fRow = Me.myWebController.AppModel.FrmPrnRowKey("frmgstdocnumtemplate", "")
                Dim str1 As String = myUtils.CombineWhere(True, Me.myWebController.AppModel.strFilterDBAppUser(Me.myWebController, fRow, "CompanyID", "CompanyID"))
                ViewBag.lstCompany = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, "SELECT CompanyId, CompName FROM Company " & str1 & " ORDER BY CompName").Tables(0).Rows.Cast(Of DataRow).Select(Function(dtRow)
                                                                                                                                                                                                                                     Return New With {.CompanyId = dtRow("CompanyId"), .CompanyName = dtRow("CompName")}
                                                                                                                                                                                                                                 End Function).ToList()
                Dim str2 As String = myUtils.CombineWhere(True, Me.myWebController.AppModel.strFilterDBAppUser(Me.myWebController, fRow, "GstRegID", "CompanyID"))
                ViewBag.lstGstReg = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, "SELECT GSTRegId, GSTIN FROM GstReg " & str2 & " ORDER BY GSTIN").Tables(0).Rows().Cast(Of DataRow).Select(Function(dtRow)
                                                                                                                                                                                                                              Return New With {.GSTRegId = dtRow("GSTRegId"), .GSTIN = dtRow("GSTIN")}
                                                                                                                                                                                                                          End Function).ToList()
                ViewBag.lstCodeWords = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, "SELECT CodeWord, DescripShort AS Description from CodeWords WHERE CodeClass='GSTN' AND CodeType='DocNature' ORDER BY CodeWord").Tables(0).Rows().Cast(Of DataRow).Select(Function(dtRow)
                                                                                                                                                                                                                                                                                                Return New With {.CodeWord = dtRow("CodeWord"), .Description = dtRow("Description")}
                                                                                                                                                                                                                                                                                            End Function).ToList()

                Return View()
            End If
        End Function

        <Authorize> <HostActionFilter>
        Public Async Function GetTemplates(id As String, fileId As String) As Task(Of JsonResult)
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Dim result As New JsonResult()

                Try
                    Dim sql As String = String.Empty
                    Select Case id.ToLower
                        Case "tenant"
                            sql = "SELECT InvoiceNumberTemplateId AS TemplateId, DocumentNature AS DocNature, Prefix, Suffix, CodeWords.DescripShort AS Description FROM GstDocNumTemplate INNER JOIN CodeWords ON CodeWords.CodeClass='GSTN' AND CodeWords.CodeType='DocNature' AND CodeWords.CodeWord = GstDocNumTemplate.DocumentNature WHERE CompanyId IS NULL AND GSTRegId IS NULL"
                        Case "company"
                            sql = "SELECT InvoiceNumberTemplateId AS TemplateId, DocumentNature AS DocNature, Prefix, Suffix, CodeWords.DescripShort AS Description FROM GstDocNumTemplate INNER JOIN CodeWords ON CodeWords.CodeClass='GSTN' AND CodeWords.CodeType='DocNature' AND CodeWords.CodeWord = GstDocNumTemplate.DocumentNature WHERE CompanyId = " + fileId.ToString
                        Case "gstreg"
                            sql = "SELECT InvoiceNumberTemplateId AS TemplateId, DocumentNature AS DocNature, Prefix, Suffix, CodeWords.DescripShort AS Description FROM GstDocNumTemplate INNER JOIN CodeWords ON CodeWords.CodeClass='GSTN' AND CodeWords.CodeType='DocNature' AND CodeWords.CodeWord = GstDocNumTemplate.DocumentNature WHERE GSTRegId = " + fileId.ToString
                    End Select

                    If Not String.IsNullOrWhiteSpace(sql) Then
                        Dim dt As DataTable = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)

                        result.Data = New With {.status = 200, .success = True, .templates = JsonConvert.SerializeObject(From dr As DataRow In dt.Rows Select dt.Columns.Cast(Of DataColumn)().ToDictionary(Function(col) col.ColumnName, Function(col) dr(col)))}
                    End If
                Catch ex As Exception
                    result.Data = New With {.status = HttpStatusCode.InternalServerError, .message = ex.Message, .success = False}
                End Try

                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet

                Return result
            End If
        End Function

        <Authorize> <HostActionFilter> <HttpPost>
        Public Async Function SaveTemplate(id As TemplateModel) As Task(Of JsonResult)
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                If id IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(id.DocNature) Then
                    Try
                        Dim sql As String = "SELECT * FROM GstDocNumTemplate WHERE InvoiceNumberTemplateId = " + id.TemplateId.ToString
                        Dim dt As DataTable = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                        Dim nr As DataRow = Nothing

                        If id.TemplateId <= 0 Then
                            nr = dt.NewRow

                            dt.Rows.Add(nr)
                        ElseIf dt.Rows.Count > 0 Then
                            nr = dt.Rows(0)
                        End If

                        If nr IsNot Nothing Then
                            nr("CompanyId") = If(id.CompanyId Is Nothing, DBNull.Value, id.CompanyId)
                            nr("GSTRegId") = If(id.GSTRegId Is Nothing, DBNull.Value, id.GSTRegId)
                            nr("DocumentNature") = id.DocNature
                            nr("Prefix") = id.Prefix
                            nr("Suffix") = id.Suffix

                            myWebController.DataProvider.objSQLHelper.SaveResults(dt, sql)
                        End If
                    Catch
                    End Try
                End If
            End If
        End Function

        <Authorize> <HostActionFilter> <HttpPost>
        Public Async Function DeleteTemplate(id As Integer) As Task(Of JsonResult)
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Try
                    myWebController.DataProvider.objSQLHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM GstDocNumTemplate WHERE InvoiceNumberTemplateId = " + id.ToString)
                Catch
                End Try
            End If
        End Function


        <Authorize> <HostActionFilter>
        Public Async Function AutoGenerateGstDocNumSeries(id As String, fileId As String) As Task(Of JsonResult)
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Dim oRet = myFuncs2.AutoGenerateGstDocNumSeries(Me.myWebController, id, fileId)
                Dim result As New JsonResult()
                If oRet.Success Then
                    result.Data = New With {.status = 200, .success = True, .data = oRet.Result}
                Else
                    result.Data = New With {.status = HttpStatusCode.InternalServerError, .message = oRet.Message, .success = False}
                End If
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet

                Return result
            End If
        End Function

        <Authorize> <HostActionFilter>
        Public Async Function AutoGenerateGstDocNumDiff(id As String, fileId As String) As Task(Of JsonResult)
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Dim oRet = myFuncs2.CalculateGstDocNumSeriesDiff(Me.myWebController, id, fileId)
                Dim result As New JsonResult()
                If oRet.Success Then
                    result.Data = New With {.status = 200, .success = True, .data = oRet.Result}
                Else
                    result.Data = New With {.status = HttpStatusCode.InternalServerError, .message = oRet.Message, .success = False}
                End If
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet

                Return result
            End If
        End Function
        <Authorize> <HostActionFilter>
        Public Async Function Recalculate(id As String, fileId As String) As Task(Of JsonResult)
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Dim result As New JsonResult()
                Dim oRet = myFuncs2.Recalculate(Me.myWebController, id, fileId)
                If oRet.Success Then
                    result.Data = New With {.status = 200, .success = True}
                Else
                    result.Data = New With {.status = HttpStatusCode.InternalServerError, .message = oRet.Message, .success = False}
                End If
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet
                Return result
            End If
        End Function

        <Authorize> <HostActionFilter>
        Public Async Function GstDocNumSeries(id As String, fileId As String) As Task(Of JsonResult)
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Dim result As New JsonResult()
                Dim docSeries As New List(Of DocNumSeriesModel)()

                Try
                    Dim dt As DataTable = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM GstDocNumSeries Where GSTRegID=" & id.ToString & " and ReturnPeriodID=" & fileId.ToString).Tables(0)

                    If dt.Rows.Count > 0 Then
                        For Each item In dt.Rows
                            docSeries.Add(New DocNumSeriesModel With {
                                .InvoiceNumberSeriesId = item("InvoiceNumberSeriesId"),
                                .InvoiceNumberTemplateId = item("InvoiceNumberTemplateId"),
                                .NumFrom = item("NumFrom"),
                                .NumTo = item("NumTo"),
                                .TotCount = item("TotCount"),
                                .CancelledCount = item("CancelledCount"),
                                .IssuedCount = item("IssuedCount"),
                                .MissingCount = item("MissingCount")
                            })
                        Next
                    End If

                    result.Data = New With {.status = 200, .success = True, .data = docSeries}
                Catch ex As Exception
                    result.Data = New With {.status = HttpStatusCode.InternalServerError, .message = ex.Message, .success = False}
                End Try

                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet

                Return result
            End If
        End Function

        <Authorize> <HostActionFilter> <HttpPost>
        Public Async Function SaveGstDocNumSeries(id As List(Of DocNumSeriesModel), deletedItemId As List(Of Int16)) As Task(Of JsonResult)
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Dim result As New JsonResult()

                If (deletedItemId IsNot Nothing) Then
                    For Each delItem In deletedItemId
                        myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, "delete FROM GstDocNumSeries WHERE InvoiceNumberSeriesId = " + delItem.ToString)
                    Next
                End If

                If (Not id Is Nothing) Then
                    Dim snum As Integer = 0
                    For Each item In id
                        Try
                            Dim dtSeries As DataTable = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, "SELECT * FROM GstDocNumSeries WHERE InvoiceNumberSeriesId = " + item.InvoiceNumberSeriesId.ToString).Tables(0)
                            Dim nr As DataRow = Nothing
                            snum = snum + 1
                            If dtSeries.Rows.Count <= 0 Then
                                nr = dtSeries.NewRow
                                dtSeries.Rows.Add(nr)
                            Else
                                nr = dtSeries.Rows(0)
                            End If

                            nr("GSTRegID") = item.GSTRegID
                            nr("ReturnPeriodID") = item.ReturnPeriodID
                            nr("InvoiceNumberTemplateId") = item.InvoiceNumberTemplateId
                            nr("NumFrom") = item.NumFrom
                            nr("NumTo") = item.NumTo
                            nr("snum") = snum
                            nr("TotCount") = item.TotCount
                            nr("CancelledCount") = item.CancelledCount
                            nr("IssuedCount") = item.IssuedCount
                            nr("MissingCount") = item.MissingCount

                            myWebController.DataProvider.objSQLHelper.SaveResults(dtSeries, "SELECT * FROM GstDocNumSeries WHERE InvoiceNumberSeriesId = " + item.InvoiceNumberSeriesId.ToString).ToString()
                        Catch ex As Exception
                        End Try
                    Next
                End If

                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet

                Return result
            End If
        End Function

        <Authorize> <HostActionFilter> <ActionName("frmGstNumberSeries")>
        Public Async Function DocNumSeries(id As String, fileId As String) As Task(Of ActionResult)
            Dim docSeries As New List(Of GSTREGModel)()
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Dim gstRegSQL As String = String.Empty
                Dim compSQL As String = String.Empty
                Dim postPeriodSQL As String = String.Empty

                If id IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(id) Then
                    compSQL = "select * from Company where  CompanyID = " & id.ToString
                    Dim dtComp As DataTable = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, compSQL).Tables(0)

                    ViewBag.CompanyID = dtComp(0)("CompanyID").ToString
                    ViewBag.CompanyName = dtComp(0)("CompName").ToString

                    gstRegSQL = "Select GSTRegID, GSTIN, Descrip from GstListGSTReg() where  CompanyID = " & id.ToString
                    Dim dt As DataTable = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, gstRegSQL).Tables(0)

                    If dt.Rows.Count > 0 Then
                        For Each item In dt.Rows
                            docSeries.Add(New GSTREGModel With {
                                .GSTRegID = item("GSTRegID"),
                                .GSTINNumber = item("GSTIN"),
                                .Description = item("Descrip")
                                          })
                        Next
                    End If

                End If

                If fileId IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(fileId) Then
                    gstRegSQL = "Select PostPeriodID, ret_pd, sdate from PostPeriod where PostPeriodID = " & fileId.ToString & " Order by sdate desc"
                    Dim dtTemplatePeriod As DataTable = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, gstRegSQL).Tables(0)

                    ViewBag.GstRegPeriodID = dtTemplatePeriod(0)("PostPeriodID").ToString
                    ViewBag.GstRegPeriod = dtTemplatePeriod(0)("ret_pd").ToString
                End If

            End If
            Return View(docSeries)
        End Function

        <Authorize> <HostActionFilter>
        Public Async Function NumberTemplate(ID As String) As Task(Of JsonResult)
            Dim result As New JsonResult()
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Try
                    Dim oMaster As New clsMasterDataFICO(Me.myWebController)
                    Dim templateSQL As String = String.Empty
                    templateSQL = $"Select InvoiceNumberTemplateId, Descrip FROM GstListDocNumTemplate() where GstRegID={ID}"
                    Dim dtTemplateIds As DataTable = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, templateSQL).Tables(0)

                    If dtTemplateIds.Rows.Count = 0 Then
                        Dim rGstReg = oMaster.GstRegRow(ID)
                        If Not rGstReg Is Nothing Then
                            templateSQL = $"Select InvoiceNumberTemplateId, Descrip FROM GstListDocNumTemplate() where CompanyID={rGstReg("CompanyID")} and gstregid is null"
                            dtTemplateIds = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, templateSQL).Tables(0)
                        End If

                    End If
                    If dtTemplateIds.Rows.Count = 0 Then
                        templateSQL = $"Select InvoiceNumberTemplateId, Descrip FROM GstListDocNumTemplate() where CompanyID is null and gstregid is null"
                        dtTemplateIds = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, templateSQL).Tables(0)
                    End If

                    result.Data = New With {.status = 200, .success = True, .data = JsonConvert.SerializeObject(From dr As DataRow In dtTemplateIds.Rows Select dtTemplateIds.Columns.Cast(Of DataColumn)().ToDictionary(Function(col) col.ColumnName, Function(col) dr(col)))}
                Catch ex As Exception
                    result.Data = New With {.status = HttpStatusCode.InternalServerError, .message = ex.Message, .success = False}
                End Try
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet
            End If

            Return result

        End Function
    End Class

    Public Class TemplateModel
        Public Property LevelId As String
        Public Property CompanyId As Integer?
        Public Property GSTRegId As Integer?
        Public Property TemplateId As Integer
        Public Property DocNature As String
        Public Property Prefix As String
        Public Property Suffix As String
    End Class



    Public Class GSTREGModel
        Public Property GSTRegID As String
        Public Property GSTINNumber As String
        Public Property Description As String
    End Class
End Namespace
