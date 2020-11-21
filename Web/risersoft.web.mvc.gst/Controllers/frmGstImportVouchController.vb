
Imports System.Net
Imports Newtonsoft.Json
Imports risersoft.app.mxform.gst
Imports risersoft.shared
Imports risersoft.shared.dal
Imports risersoft.shared.web
Imports System.Threading.Tasks
Imports risersoft.shared.cloud
Imports System.IO
Imports Microsoft.Owin.Infrastructure
Imports risersoft.app

Namespace Controllers
    Public Class frmGstImportVouchController
        Inherits clsMvcControllerBase
        Dim pagesize As Integer = ConfigurationManager.AppSettings("PageSize")

        Public Function Test() As ActionResult
            Return View("UploadTest")
        End Function

        <Authorize> <HostActionFilter> <HttpPost> <ActionName("FileTest")>
        Public Function PostFileUpload(DocType As String, filename As HttpPostedFileBase) As ActionResult
            Return Me.Content("Done")
        End Function
        <Authorize> <HostActionFilter> <ActionName("Edit")> <PreserveQueryString>
        Public Overridable Function GetEdit(Optional Id As String = "") As ActionResult
            Dim vw = Me.myMvcController.CreateParentModel(Me.Request.QueryString("data"))
            Dim RootImportFileID As String = ""
            If vw.ContextRow.ColExists("RootImportFileID") Then RootImportFileID = myUtils.cStrTN(vw.ContextRow.CellValue("RootImportFileID"))
            If String.IsNullOrEmpty(RootImportFileID) Then RootImportFileID = Id
            Return RedirectToAction("FileDetails/" + RootImportFileID + "/" + Id)
        End Function
        <PreserveQueryString>
        Public Function Create() As ActionResult
            Dim strXML = Me.myMvcController.FindParentParam(Me.Request.QueryString("params"), "tag")
            Dim vBag = myMvcController.Data.VarBag(strXML)
            Dim DocType As String = vBag("doctype")
            Dim ActionType As String = vBag("action")
            Return RedirectToAction("Index", New With {DocType, ActionType})
        End Function

        <Authorize> <HostActionFilter> <ActionName("Index")>
        Public Async Function GetIndex(id As String) As Task(Of ActionResult)
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                If id IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(id) Then
                    Dim sql = String.Format("select * from ImportFile where ImportFileID ='{0}'", id.ToString)
                    Dim dt1 = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                    ViewBag.FileId = dt1.Rows(0)("ImportFileID")
                    ViewBag.FileName = dt1.Rows(0)("FileName")
                End If
            End If
            Return View()
        End Function

        'POST: frmImport/File
        <Authorize> <HostActionFilter> <HttpGet> <ActionName("File")>
        Public Async Function PostFile(DocType As String, fileName As String, pfileID As String, ifileID As String, url As String, fSize As Long, timeStamp As Long, actionType As String) As Task(Of JsonResult)
            Dim result As New JsonResult()

            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Try
                    Dim nr = mxform.myFuncs.CreateImportFile(myWebController, fileName, pfileID, ifileID, fSize, timeStamp, DocType)
                    Dim oRet As New clsProcOutput

                    Dim dic As New Dictionary(Of String, String) From {{"path", fileName}}
                    Dim dicParams As New Dictionary(Of String, String) From {{"InfoJson", JsonConvert.SerializeObject(dic)}, {"ImportFileID", ifileID}}

                    If myUtils.IsInList(actionType, "convertjson", "search", "operate") Then
                        Dim flname As String = myWebController.Controller.CalcRLSId.ToString & "-" & actionType & "-" & Replace(Now.ToLongTimeString, ":", "").Replace(" ", "") & ".xlsx"
                        dicParams("filename") = flname
                    End If

                    Dim rTask As DataRow = TaskProviderFactory.CreateApiTask(myWebController.DataProvider, actionType, DocType, 0, dicParams)
                    Dim queueName = myWebController.CalcQueueName
                    oRet = TaskProviderFactory.Enqueue(myWebController.DataProvider, rTask, queueName)

                    result.Data = New With {.status = 200, .success = oRet.Success, .message = oRet.Message, .description = rTask("ApiTaskId").ToString, .impFileId = ifileID, .fName = rTask("FileName").ToString}
                Catch ex As Exception
                    result.Data = New With {.status = HttpStatusCode.InternalServerError, .message = ex.Message, .success = False}
                End Try

                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet
            End If
            Return result
        End Function

        <Authorize> <HostActionFilter> <HttpPost>
        Public Async Function GenerateSAS(file As String) As Task(Of JsonResult)
            Dim result As New JsonResult()
            Dim _NewFileName As String = String.Empty
            Dim parentFileID As String = String.Empty
            Dim cGuid As Guid
            Dim newGuid As Guid = Guid.NewGuid

            Try
                Dim provider As New clsBlobFileProvider(Me.myWebController)
                ' Split string based on spaces.
                Dim ErrorSuffix As String = "_Error"
                Dim OrigFileName = Path.GetFileNameWithoutExtension(file)
                If myUtils.EndsWith(OrigFileName, ErrorSuffix) Then
                    Dim flName As String() = Left(OrigFileName, OrigFileName.Length - ErrorSuffix.Length).Split(New String() {"--"}, StringSplitOptions.RemoveEmptyEntries)
                    If Guid.TryParse(flName.Last, cGuid) Then
                        parentFileID = cGuid.ToString
                        _NewFileName = Path.GetFileNameWithoutExtension(flName.First) + "--" + newGuid.ToString + Path.GetExtension(file)
                    End If
                End If

                ' Generate File Name with new guid
                If String.IsNullOrEmpty(_NewFileName) Then
                    _NewFileName = OrigFileName + "--" + newGuid.ToString + Path.GetExtension(file)
                End If


                Dim oRet = provider.CreateUploadUri(ConfigurationManager.AppSettings("StorageContainerName"), _NewFileName, "")
                If oRet.Success Then
                    result.Data = New With {.status = 200, .success = oRet.Success, .message = oRet.Message, .Data = oRet.Result.Uri.ToString, .flName = _NewFileName, .previousFileID = parentFileID, .ImportFileID = newGuid}
                Else
                    'oRet.AddError("Cannot Create")
                    result.Data = New With {.status = 200, .success = oRet.Success, .message = oRet.Message} '"Unable to upload file."
                End If
            Catch ex As Exception
                result.Data = New With {.status = HttpStatusCode.InternalServerError, .message = ex.Message, .success = False}
            End Try

            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet

            Return result
        End Function

        <Authorize>
        Public Function Template(code As String) As ActionResult
            Dim assy = GetType(Import_GSTR1TaskProvider).Assembly
            Dim str1 As String
            Select Case code.Trim.ToLower
                Case "is"
                    str1 = "InvoiceSale.xlsx"
                Case "ip"
                    str1 = "InvoicePurch.xlsx"
                Case "pv"
                    str1 = "Advance_Paid_Refund.xlsx"
                Case "pc"
                    str1 = "Advance_Receipt_Refund.xlsx"
                Case "tp"
                    str1 = "CompanyAndCampus.xlsx"
                Case "customer"
                    str1 = "Customer.xlsx"
                Case "vendor"
                    str1 = "Vendor.xlsx"
                Case "hsn"
                    str1 = "HSNSAC.xlsx"
                Case "ewb"
                    str1 = "Ewaybill.xlsx"
                Case "ewbop"
                    str1 = "Ewaybill-UCE.xlsx"
                Case "role"
                    str1 = "RoleManagement.xlsx"
                Case Else
                    assy = myAssy.GetAsm("kpmg.app.mxform.asp")
                    Select Case code.Trim.ToLower
                        Case "gstr6ip"
                            str1 = "ITCInward.xlsx"
                        Case "isd"
                            str1 = "GSTR6_ITC_Distribution.xlsx"
                        Case "chl"
                            str1 = "ITC-04.xlsx"
                        Case "recon"
                            str1 = "Recon.xlsx"
                        Case "tds"
                            str1 = "GSTR7.xlsx"
                        Case "tcs"
                            str1 = "GSTR8.xlsx"
                        Case "track"
                            str1 = "Vendor_Status.xlsx"
                        Case "defer"
                            str1 = "InvoiceDefer.xlsx"
                        Case "gstin"
                            str1 = "GSTIN_Search.xlsx"
                    End Select
            End Select
            If Not String.IsNullOrEmpty(str1) Then
                Dim rawFile = myAssy.bytFromEmbed(assy.GetName.Name, str1)
                Return File(rawFile, System.Web.MimeMapping.GetMimeMapping(str1), str1)
            End If

        End Function

        <Authorize> <HostActionFilter> <ActionName("FileDetails")>
        Public Async Function GetImportFile(id? As Guid, fileId? As Guid) As Task(Of ActionResult)
            Dim strVal As New Dictionary(Of String, String)()
            Dim sql As String

            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then

                If id.ToString <> Nothing And fileId.ToString <> Nothing Then
                    sql = String.Format("select * from ImportFile where RootImportFileID ='{0}' or importfileid='{0}' order by lastruntime desc", id.ToString)

                    Dim dt1 = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                    strVal.Add("tableSummary", JsonConvert.SerializeObject(From dr As DataRow In dt1.Rows Select dt1.Columns.Cast(Of DataColumn)().ToDictionary(Function(col) col.ColumnName, Function(col) dr(col))))
                    Dim sr = dt1.Rows(dt1.Rows.Count - 1)
                    strVal.Add("fileName", sr("fileName"))
                    strVal.Add("fileID", sr("ImportFileId").ToString)
                    strVal.Add("docType", sr("DocType").ToString)
                    strVal.Add("documentStoredCount", GetRecordCountData(fileId.ToString, dt1.Rows(0)("DocType").ToString))

                    'Summary Json
                    strVal.Add("SummaryJson", Summary(String.Format("select SummaryJson from ImportFile where importfileid='{0}'", fileId.ToString)))
                    strVal.Add("SummaryDetails", GetData(id.ToString, dt1.Rows(0)("DocType").ToString, 0, pagesize))
                    strVal.Add("InvoicePageNumbers", GetDataCount(id.ToString))

                    Return View(strVal)
                End If
                Return View()
            End If
        End Function

        <Authorize> <HostActionFilter> <HttpGet>
        Public Async Function GetPaging(fileID As String, pageNumber As Integer, pagesize As Integer, pagetype As String) As Task(Of JsonResult)
            Dim result As New JsonResult()
            Dim sqlCount As Integer = 0
            Dim sqlRecord As String = String.Empty

            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Try
                    'Dim pNumber = CInt(Math.Ceiling((pageNumber - 1) * pagesize))
                    'Dim pNumber = CInt(Math.Ceiling(pageNumber - 1))
                    sqlCount = GetDataCount(fileID.ToString)
                    sqlRecord = GetData(fileID.ToString, pagetype, pageNumber, pagesize)
                    Dim totalPages = CInt(Math.Ceiling(sqlCount / pagesize))
                    result.Data = New With {.status = 200, .success = True, .message = "", .pgCount = sqlCount, .data = sqlRecord, .pType = pagetype}

                Catch ex As Exception
                    result.Data = New With {.status = HttpStatusCode.InternalServerError, .message = ex.Message, .success = False}
                End Try
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet
            End If
            Return result
        End Function

        Protected Friend Sub AddParams(dt1 As DataTable)
            If Not dt1.Columns.Contains("Params") Then dt1.Columns.Add("params", GetType(String))
            For Each r1 As DataRow In dt1.Select
                Dim conf = New clsEncryptString(False)
                conf.Add("tag", "<PARAMS IMPORTFILEVOUCHID=""" & r1("importfilevouchid") & """/>")

                Dim dic As New clsCollecString(Of String)
                dic.Add("params", conf.ToString)
                Me.myMvcController.GetAppInfo.PopulateAppBarDict(dic)
                Select Case myUtils.cStrTN(r1("doctype")).Trim.ToLower
                    Case "pc"
                        r1("params") = "/frmGstPaymentCustomer/Add"
                    Case "pv"
                        r1("params") = "/frmGstPaymentVendor/Add"
                    Case "is"
                        r1("params") = "/frmGstInvoiceSale/Add"
                    Case "ip"
                        r1("params") = "/frmGstInvoicePurch/Add"
                    Case "isd"
                        r1("params") = "/frmGstInvoiceISD/Add"
                    Case "ewb"
                        r1("params") = "/frmEwayBill/Add"
                    Case "chl"
                        r1("params") = "/frmGstChallan/Add"
                    Case "tcs"
                        r1("params") = "/frmTCS/Add"
                    Case "tds"
                        r1("params") = "/frmTDS/Add"
                    Case Else
                        r1("params") = "/frmGstInvoiceSale/Add"
                End Select
                r1("params") = WebUtilities.AddQueryString(r1("params"), dic)
            Next
        End Sub

        <Authorize> <HostActionFilter> <HttpGet> <ActionName("GetSummary")>
        Public Async Function GetImportFileSummary(id As String, fileId As String) As Task(Of JsonResult)
            Dim strImportFileData As New Dictionary(Of String, String)()
            Dim result As New JsonResult()
            Dim sQry = String.Empty, cQry = String.Empty

            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Try
                    'Get SummaryJON from Import File Table
                    strImportFileData.Add("tableSummary", Summary("select * from ImportFile where ImportFileID='" + id.ToString + "'"))
                    'Get Invoice Details from Import Vouch Table
                    strImportFileData.Add("InvoiceSummary", GetData(id, fileId, 0, pagesize))
                    Dim invoiceCount = GetRecordCountData(id, fileId)

                    Dim totalInvoicePages = CInt(Math.Ceiling(invoiceCount / pagesize))
                    strImportFileData.Add("InvoicePageNumbers", invoiceCount)
                    strImportFileData.Add("documentStoredCount", GetRecordCountData(id.ToString, fileId))
                    result.Data = New With {.status = 200, .success = True, .message = "", .data = strImportFileData}

                Catch ex As Exception
                    result.Data = New With {.status = HttpStatusCode.InternalServerError, .message = ex.Message, .success = False}
                End Try
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet
            End If
            Return result
        End Function

        <Authorize> <HostActionFilter> <HttpGet> <ActionName("Downloads")>
        Public Async Function DownloadFile(id As Guid, fileId As String) As Task(Of JsonResult)
            Dim fileProvider As New clsBlobFileProvider(Me.myWebController)
            Dim result As New JsonResult()
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Try
                    Dim sql As String = String.Empty
                    If fileId = "err" Then
                        sql = "select ErrorFileURL as FileURL from ImportFile where ImportFileId='" + id.ToString + "'"
                    ElseIf fileId = "apitask" Then
                        sql = "select FileName as FileURL from ApiTask where ApiTaskID='" + id.ToString + "'"
                    Else
                        sql = "select FileName as FileURL from ImportFile where ImportFileId='" + id.ToString + "'"
                    End If

                    Dim dsfileName = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                    Dim FileUrl2 = Await fileProvider.GetDownloadUriAsync(ConfigurationManager.AppSettings("StorageContainerName"), dsfileName(0)("FileURL"))

                    result.Data = New With {.status = 200, .success = True, .message = "", .data = FileUrl2.Result.Uri().ToString()}
                Catch ex As Exception
                    result.Data = New With {.status = HttpStatusCode.InternalServerError, .message = ex.Message, .success = False}
                End Try
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet
            End If
            Return result
        End Function

        <Authorize> <HostActionFilter> <HttpGet> <ActionName("DownloadExcelFile")>
        Public Async Function DownloadExcelFile(id As Guid) As Task(Of JsonResult)
            Dim fileProvider As New clsBlobFileProvider(Me.myWebController)
            Dim result As New JsonResult()
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Try
                    Dim sql As String = String.Empty
                    If id.ToString IsNot Nothing Then
                        sql = "select FileName as FileURL from ApiTask where ApiTaskID='" + id.ToString + "'"
                    End If

                    Dim dsfileName = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                    Dim FileUrl2 = Await fileProvider.GetDownloadUriAsync(ConfigurationManager.AppSettings("StorageContainerName"), dsfileName(0)("FileURL"))

                    result.Data = New With {.status = 200, .success = True, .message = "", .data = FileUrl2.Result.Uri().ToString()}
                Catch ex As Exception
                    result.Data = New With {.status = HttpStatusCode.InternalServerError, .message = ex.Message, .success = False}
                End Try
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet
            End If
            Return result
        End Function

        Public Function Summary(qry As String) As String
            Dim dt = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, qry).Tables(0)
            If dt.Columns.Contains("Params") Then
                Me.AddParams(dt)
            End If

            Dim InvoiceVouchSummary = JsonConvert.SerializeObject(From dr As DataRow In dt.Rows Select dt.Columns.Cast(Of DataColumn)().ToDictionary(Function(col) col.ColumnName, Function(col) dr(col)))
            Return InvoiceVouchSummary
        End Function

        Public Function RecordCount(qry As String) As String
            Dim dt = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, qry).Tables(0)
            Dim totalRecordCount = dt.Rows(0)(0)
            Return totalRecordCount
        End Function

        <Authorize> <HostActionFilter> <HttpGet> <ActionName("GetStatus")>
        Public Async Function CheckTaskStatus(id As Guid) As Task(Of JsonResult)
            Dim result As New JsonResult()
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Try
                    'Get Status of Importing File.
                    Dim sqlCheckStatus = String.Format("select * from ApiTask where ApiTaskID='" + id.ToString + "'")
                    Dim dsfileName = myWebController.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, sqlCheckStatus).Tables(0)

                    result.Data = New With {.status = 200, .success = True, .message = "Task Id " + dsfileName(0)("ApiTaskID").ToString + " has been successfully " + dsfileName(0)("Status").ToString, .taskStatus = dsfileName(0)("Status").ToString}
                Catch ex As Exception
                    result.Data = New With {.status = HttpStatusCode.InternalServerError, .message = ex.Message, .success = False}
                End Try
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet
            End If
            Return result
        End Function

        <Authorize> <HostActionFilter> <HttpGet> <ActionName("Delete")>
        Public Async Function DeleteRecords(id As Guid) As Task(Of JsonResult)
            Dim strVal As New Dictionary(Of String, String)()

            Dim result As New JsonResult()
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Try
                    Dim oRet As New clsProcOutput
                    Dim dicParams As New Dictionary(Of String, String) From {{"ImportFileID", id.ToString}}
                    Dim rTask As DataRow = TaskProviderFactory.CreateApiTask(myWebController.DataProvider, "import", "del", 0, dicParams)
                    Dim queueName = myWebController.CalcQueueName
                    oRet = TaskProviderFactory.Enqueue(myWebController.DataProvider, rTask, queueName)

                    result.Data = New With {.status = 200, .success = oRet.Success, .message = oRet.Message, .description = rTask("ApiTaskId").ToString}

                Catch ex As Exception
                    result.Data = New With {.status = HttpStatusCode.InternalServerError, .message = ex.Message, .success = False}
                End Try
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet
            End If
            Return result
        End Function

        <Authorize> <HostActionFilter> <HttpGet> <ActionName("UpdateDeltedRecord")>
        Public Async Function UpdateRecords(id As Guid, fileId As String) As Task(Of JsonResult)
            Dim strVal As New Dictionary(Of String, String)()

            Dim result As New JsonResult()
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Try
                    'After Deleting record from invoice and gstAdvanceVouch basis of ImportFileId.
                    'strVal.Add("invoiceCount", GetRecordCountData(id.ToString, "invoice"))
                    'strVal.Add("voucherCount", GetRecordCountData(id.ToString, "gstadvancevouch"))
                    strVal.Add("documentStoredCount", GetRecordCountData(id.ToString, fileId.ToString))

                    result.Data = New With {.status = 200, .success = True, .data = strVal}

                Catch ex As Exception
                    result.Data = New With {.status = HttpStatusCode.InternalServerError, .message = ex.Message, .success = False}
                End Try
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet
            End If
            Return result
        End Function

        Public Function GetData(id As String, type As String, Optional pageNumber As Integer = 0, Optional pSize As Integer = 0) As String
            Dim sQry As String = String.Empty
            sQry = String.Format("select ImportFileVouchID, VouchNum, Dated, TotalAmount, CTIN, ErrorCSV, WarningCSV, '' as Params, Doctype from ImportFileVouch where ImportFileId='" + id.ToString + "' order by VouchNum OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", pageNumber * pSize, pSize)
            Return Summary(sQry)
        End Function

        Public Function GetDataCount(id As String) As String
            Dim sQry As String = String.Empty
            sQry = "select count(*) from ImportFileVouch where ImportFileId='" + id.ToString + "'"
            Return RecordCount(sQry)
        End Function

        Public Function GetRecordCountData(id As String, type As String) As String
            Dim sQry As String = String.Empty

            If type.ToLower = "v" OrElse type.ToLower = "vendor" Then
                sQry = "select Count(*) from vendor where ImportFileID='" + id.ToString + "'"
            ElseIf type.ToLower = "is" OrElse type.ToLower = "ip" OrElse type.ToLower = "gstr1" OrElse type.ToLower = "gstr6ip" Then
                sQry = "Select count(*) from Invoice where importfileid='" + id.ToString + "'"
            ElseIf type.ToLower = "pc" OrElse type.ToLower = "pv" Then
                sQry = "Select count(*) from GstAdvanceVouch where importfileid='" + id.ToString + "'"
            ElseIf type.ToLower = "gstr2a" Then
                sQry = "Select count(*) from CPInvoice where importfileid='" + id.ToString + "'"
            ElseIf type.ToLower = "hsn" Then
                sQry = "Select count(*) from HSNSac where importfileid='" + id.ToString + "'"
            ElseIf type.ToLower = "isd" Then
                sQry = "Select count(*) from ISDinvoice where importfileid='" + id.ToString + "'"
            ElseIf type.ToLower = "customer" Then
                sQry = "Select count(*) from Customer where importfileid='" + id.ToString + "'"
            ElseIf type.ToLower = "tp" Then
                sQry = "Select count(*) from Campus where importfileid='" + id.ToString + "'"
            ElseIf type.ToLower = "role" Then
                sQry = "Select count(*) from DBRole where importfileid='" + id.ToString + "'"
            ElseIf type.ToLower = "chl" Then
                sQry = "Select count(*) from Challan where importfileid='" + id.ToString + "'"
            ElseIf type.ToLower = "ewb" Then
                sQry = "Select count(*) from Ewaybill where importfileid='" + id.ToString + "'"
            ElseIf type.ToLower = "tcs" Then
                sQry = "Select count(*) from tcs where importfileid='" + id.ToString + "'"
            ElseIf type.ToLower = "tds" Then
                sQry = "Select count(*) from tds where importfileid='" + id.ToString + "'"
            ElseIf type.ToLower = "recon" Then
                sQry = "Select count(*) from invoice where 0=1"
            ElseIf type.ToLower = "defer" Then
                sQry = "Select count(*) from invoice where 0=1"
            End If

            Return RecordCount(sQry)
        End Function

        <Authorize> <HostActionFilter> <HttpGet> <ActionName("ExecuteTask")>
        Public Async Function ExecuteTaskAgain(id As Guid, fileId As String, FileName As String) As Task(Of JsonResult)
            Dim strVal As New Dictionary(Of String, String)()

            Dim result As New JsonResult()
            If Await Me.myWebController.CheckInitModel(Me.myWebController.GetAppInfo, True) Then
                Try
                    Dim oRet As New clsProcOutput
                    Dim dic As New Dictionary(Of String, String) From {{"path", FileName}}
                    Dim dicParams As New Dictionary(Of String, String) From {{"InfoJson", JsonConvert.SerializeObject(dic)}, {"ImportFileID", id.ToString}}


                    Dim rTask As DataRow = TaskProviderFactory.CreateApiTask(myWebController.DataProvider, "import", fileId.ToString, 0, dicParams)
                    Dim queueName = myWebController.CalcQueueName
                    oRet = TaskProviderFactory.Enqueue(myWebController.DataProvider, rTask, queueName)

                    result.Data = New With {.status = 200, .success = oRet.Success, .message = oRet.Message, .description = rTask("ApiTaskId").ToString}

                Catch ex As Exception
                    result.Data = New With {.status = HttpStatusCode.InternalServerError, .message = ex.Message, .success = False}
                End Try
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet
            End If
            Return result
        End Function

    End Class
End Namespace