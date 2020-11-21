Imports System.Collections.Concurrent
Imports System.Configuration
Imports System.IO
Imports System.IO.Compression
Imports System.Text
Imports System.Threading
Imports GSTN.API.Library.Models
Imports GSTN.API.Library.Models.GstNirvana
Imports Newtonsoft.Json
Imports risersoft.API.GSTN
Imports risersoft.shared
Imports risersoft.shared.cloud
Imports risersoft.shared.cloud.common

Public Class clsGSTNTypedReturnBase(Of TData As GSTRBase, TSummary, TPClient As GSTNReturnsClient(Of TData, TSummary), CPClient As GSTNReturnsClient(Of TData, TSummary))
    Inherits clsGSTNReturnBase
    Public Sub New(context As IProviderContext, DocType As String, ReturnCode As String)
        MyBase.New(context, DocType, ReturnCode)
    End Sub
    Public Function PopulateDataTP(results As List(Of TData)) As DataSet
        Dim ds2 As DataSet
        SyncLock objLock
            Dim dic = Me.PrepareGSTRPayloadSQLUp(0, oMaster.GetPostPeriodID(Now.Date))
            ds2 = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
        End SyncLock
        For Each model1 In results
            Me.PopulateDataset(model1, ds2)
        Next
        Return ds2
    End Function
    Public Overridable Function CalcGstnRetPd(rgstreg As DataRow, rPP As DataRow) As String
        Return rPP("ret_pd")
    End Function
    Public Function PopulateDataCP(results As List(Of TData)) As DataSet
        Dim ds2 As DataSet
        SyncLock objLock
            Dim dic = Me.PrepareGSTRAPayloadSQL(0, 0)
            ds2 = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
        End SyncLock

        For Each model1 In results
            Me.PopulateDataset(model1, ds2)
        Next

        Return ds2
    End Function
    Public Overridable Sub PrepareCleanTPModel(lst As List(Of TData))

    End Sub
    Protected Friend Async Function DownloadGstnSection(GstRegID As Integer, ReturnPeriodID As Integer, TransType As String, client As GSTNReturnsClient(Of TData, TSummary)) As Task(Of GSTNDownload(Of TData))
        Dim from_time = Me.GetFromTime(GstRegID, ReturnPeriodID, TransType, "")
        Dim model1 = client.GetSection(TransType, "", "", "").Data
        Dim nrTrans As DataRow = Me.SaveTransaction(GstRegID, ReturnPeriodID, TransType, "", client.dicParams)
        Dim oRet As New GSTNDownload(Of TData)
        Try
            If Not model1 Is Nothing Then
                If String.IsNullOrEmpty(model1.token) Then
                    'instant response
                    Dim result As New GSTNDataFile(Of TData)
                    result.FileName = $"{TransType}_{nrTrans("gstntransactionid")}.json"
                    result.Data = model1
                    oRet.Fc = 1
                    oRet.Files.Add(result)
                Else
                    oRet = Await Me.GetFileDetails(nrTrans, client)
                End If
            End If
        Catch ex As Exception
            myContext.Logger.logInformation(ex.Message)
            oRet.OutMessage = ex.Message
        End Try
        oRet.GSTRegID = GstRegID
        oRet.ReturnPeriodID = ReturnPeriodID
        oRet.rTrans = nrTrans
        oRet.TransType = TransType
        Return oRet
    End Function

    Protected Overridable Overloads Async Function DownloadGstnCp(GstRegID As Integer, ReturnPeriodID As Integer, client As CPClient) As Task(Of List(Of GSTNDownload(Of TData)))
        Dim lst As New List(Of GSTNDownload(Of TData))
        For Each str1 As String In New String() {"B2B", Me.CDNRApiAction, "B2BA", Me.CDNRApiAction & "A", "ISD"}
            Dim oRet = Await Me.DownloadGstnSection(GstRegID, ReturnPeriodID, str1, client)
            lst.Add(oRet)
        Next

        Return lst
    End Function
    Protected Overridable Overloads Async Function DownloadGSTNTp(GstRegID As Integer, ReturnPeriodID As Integer, client As TPClient, ds As DataSet, Action As String) As Task(Of List(Of GSTNDownload(Of TData)))
        Dim lst As New List(Of GSTNDownload(Of TData))


        Dim lstSection = ds.Tables.Cast(Of DataTable).Select(Function(x) x.TableName).ToList
        If myUtils.IsInList(Action, "clean") Then lstSection = lstSection.OrderBy(Of Integer)(Function(x) If(myUtils.StartsWith(x, "cdn"), 1, 2)).ToList

        For Each str1 In lstSection
            Dim dt1 = ds.Tables(str1)
            Dim TransType As String = Me.GetTransTypeFromTable(str1)
            Dim oRet = Await Me.DownloadGstnSection(GstRegID, ReturnPeriodID, TransType, client)
            lst.Add(oRet)
        Next
        Return lst
    End Function


    Public Overridable Function UpdateDownloadedDataCP(GstRegID As Integer, ReturnPeriodID As Integer, results As List(Of TData)) As Task(Of clsProcOutput(Of GstImportInfoGSTIN))

    End Function
    Public Overridable Function UpdateDownloadedDataTP(GstRegID As Integer, ReturnPeriodID As Integer, results As List(Of TData)) As Task(Of clsProcOutput(Of GstImportInfoGSTIN))

    End Function
    Public Overridable Async Function UpdateDownloadedDataTP(lst As List(Of GSTNDownload(Of TData))) As Task(Of List(Of clsProcOutput(Of GstImportInfoGSTIN)))
        Dim lst2 As New List(Of clsProcOutput(Of GstImportInfoGSTIN))
        For Each dl In lst
            Dim oRet = Await Me.UpdateDownloadedDataTP(dl.GSTRegID, dl.ReturnPeriodID, dl.Files.Select(Function(x) x.Data).ToList)
            lst2.Add(oRet)
        Next
        Return lst2
    End Function
    Protected Friend Function UploadGSTR(GstRegID As Integer, ReturnPeriodID As Integer, client As TPClient, lst As List(Of GSTNPayload(Of TData)), UploadType As String) As clsProcOutput
        Dim oRet As New clsProcOutput


        For Each obj In lst
            Dim payload As String = JsonConvert.SerializeObject(obj.Data, Newtonsoft.Json.Formatting.Indented,
                            New JsonSerializerSettings With {
                                .NullValueHandling = NullValueHandling.Ignore
                            })



            Dim info = client.Save(obj.Data)
            Dim nrTrans = Me.OnGstrUpload(GstRegID, ReturnPeriodID, client.SaveAction, client.dicParams, UploadType, obj.IDList)
            oRet.AddDataRow("trans", nrTrans)
            If Not info.Data Is Nothing Then
                If String.IsNullOrEmpty(info.Data.reference_id) Then
                    oRet.AddError("Error - could Not upload")
                Else
                    oRet.AddMessage("Uploaded. Status Query Queued")
                End If
            End If
        Next

        Return oRet
    End Function
    Protected Friend Overridable Overloads Function GetSummary(GstRegID As Integer, ReturnPeriodID As Integer, client As TPClient) As clsProcOutput(Of TSummary)
        Dim oRet As New clsProcOutput(Of TSummary)
        oRet.Result = client.GetSummary.Data
        Dim nrTrans As DataRow = Me.SaveTransaction(GstRegID, ReturnPeriodID, "RETSUM", "", client.dicParams)
        oRet.AddDataRow("trans", nrTrans)
        Return oRet
    End Function

    Protected Friend Function GetClient(GstRegID As Integer, ReturnPeriodID As Integer) As TPClient
        Dim rPP As DataRow = oMaster.rPostPeriod(ReturnPeriodID)
        Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
        Dim rToken = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
        Dim auth = Me.GetAuthClientFromToken(rToken, rGstReg)
        Dim client As TPClient = Activator.CreateInstance(GetType(TPClient), New Object() {auth, rGstReg("gstin"), rGstReg("gstnuserid"), Me.CalcGstnRetPd(rGstReg, rPP)})
        Return client
    End Function
    Public Function UploadGSTN(GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput
        Dim rGstReg As DataRow = Me.oMaster.GstRegRow(GstRegID)
        Dim rPP As DataRow = Me.oMaster.rPostPeriod(ReturnPeriodID)
        Dim rToken = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
        Dim oRet As New clsProcOutput
        If rGstReg Is Nothing Then
            oRet.AddError("Invalid GSTIN")
        ElseIf rPP Is Nothing Then
            oRet.AddError("Invalid Period")
        ElseIf rToken Is Nothing Then
            oRet.AddError("Valid token not found for GSTIN " & rGstReg("gstin"))
        Else
            Dim auth = Me.GetAuthClientFromToken(rToken, rGstReg)
            Dim client As TPClient = Activator.CreateInstance(GetType(TPClient), New Object() {auth, rGstReg("gstin"), rGstReg("gstnuserid"), Me.CalcGstnRetPd(rGstReg, rPP)})
            For Each UploadType As String In New String() {"DEL", "UM"}
                Dim dic = Me.GenerateSQL(GstRegID, ReturnPeriodID, UploadType)
                If (dic IsNot Nothing) AndAlso (dic.Count > 0) Then
                    Dim lst = Me.GenerateModels(GstRegID, ReturnPeriodID, dic).Values.ToList
                    oRet = oRet + Me.UploadGSTR(GstRegID, ReturnPeriodID, client, lst, UploadType)
                End If
            Next
            Dim str1 As String = JsonConvert.SerializeObject(oRet, Formatting.None,
                                New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Ignore})
        End If
        Return oRet
    End Function

    Public Function GeneratePayload(GstRegID As Integer, ReturnPeriodID As Integer, UploadType As String, filename As String) As clsProcOutput
        Dim oRet As New clsProcOutput
        Try
            Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
            Dim rPP As DataRow = oMaster.rPostPeriod(ReturnPeriodID)

            Dim dic = Me.GenerateSQL(GstRegID, ReturnPeriodID, UploadType)
            Dim lst = Me.GenerateModels(GstRegID, ReturnPeriodID, dic)

            Dim zipFileName = myContext.App.objAppExtender.MapPath("app_data/payload/" & filename)
            myContext.FTP.EnsureLocalDirectory(System.IO.Path.GetDirectoryName(zipFileName))
            Using zip = ZipFile.Open(zipFileName, ZipArchiveMode.Create)
                For Each kvp In lst
                    Dim entry = zip.CreateEntry(kvp.Key & ".json", CompressionLevel.Optimal)
                    Dim str1 As String = JsonConvert.SerializeObject(kvp.Value.Data, Formatting.None,
                                New JsonSerializerSettings With {.NullValueHandling = NullValueHandling.Ignore})
                    Using ms As MemoryStream = EncryptionUtilsBase.StringToStream(str1)
                        Using zipStream = entry.Open
                            ms.CopyTo(zipStream)
                        End Using
                    End Using
                Next
            End Using

            oRet.Description = zipFileName
            '    oRet.FileData = New FileOutput With {
            '.filename = "payload.json",
            '.data = Encoding.UTF8.GetBytes(str1),
            '.contentType = MimeMapping.GetMimeMapping(.filename)}

        Catch ex As Exception
            oRet.AddError(ex.Message)
        End Try
        Return oRet
    End Function

    Protected Friend Async Function GetStatus(rTrans As DataRow, client As TPClient) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput
        Dim reference_id As String = myUtils.cStrTN(rTrans("reference_id"))
        Dim timeSecs As Integer = 0     'Stop trying if 15 mins have elapsed while trying.
        If reference_id.Trim.Length > 0 Then
            Dim info As StatusInfo(Of TData)
            While ((info Is Nothing) OrElse myUtils.IsInList(info.status_cd, "IP")) AndAlso (timeSecs < 15 * 60)
                Await Task.Delay(5000)
                timeSecs = timeSecs + 5
                info = client.GetStatus(reference_id).Data
                myContext.Logger.logInformation("RETSTATUS: " & reference_id)
            End While
            If info Is Nothing Then
                For Each str1 As String In New String() {"ResponsePayload", "ResponseError"}
                    If client.dicParams.ContainsKey(str1) Then
                        rTrans("statusinfo") = client.dicParams(str1)
                        Exit For
                    End If
                Next
                myContext.Provider.objSQLHelper.SaveResults(rTrans.Table, "select GSTNTransactionId, statusinfo from gstntransaction")
                oRet.AddError("Could not obtain status")
            Else
                rTrans("statusinfo") = JsonConvert.SerializeObject(info)
                rTrans("statuscode") = info.status_cd
                myContext.Provider.objSQLHelper.SaveResults(rTrans.Table, "select GSTNTransactionId, statusinfo, statuscode from gstntransaction")
                If myUtils.IsInList(info.status_cd, "P") Then
                    oRet.AddMessage("Obtained status code: " & info.status_cd)
                    Dim Status As String = If(myUtils.IsInList(rTrans("transtype"), "retsubmit"), "S", "U")
                    Me.OnGSTRAction(rTrans("GstRegID"), rTrans("ReturnPeriodID"), ReturnCode, Status)
                Else
                    oRet.AddError("Obtained status code: " & info.status_cd & " with error: " & If(info.error_report Is Nothing, "", info.error_report.error_msg))
                End If
                Me.UpdateInvoiceStatus(rTrans, info)
            End If
        Else
            oRet.AddError("Reference_Id is not defined")
        End If
        Return oRet
    End Function

    Protected Friend Sub UpdateInvoiceStatus(rTrans As DataRow, info As StatusInfo(Of TData))
        If myUtils.IsInList(myUtils.cStrTN(rTrans("transsubtype")), "UM") AndAlso myUtils.IsInList(rTrans("transtype"), "retsave") Then
            Me.fncInvoiceFilter = Function()
                                      Dim str1 As String = "lasttransactionid='" & myUtils.cStrTN(rTrans("GstnTransactionId")) & "'"
                                      Return str1
                                  End Function

            Dim dic = Me.PrepareGSTRPayloadSQLUp(rTrans("GstRegID"), rTrans("ReturnPeriodID"))
            Dim dsDB As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)

            Dim dsGSTN = dsDB.Clone
            For Each dt1 As DataTable In dsGSTN.Tables
                dt1.Columns.Add("error_cd", GetType(String))
                dt1.Columns.Add("error_msg", GetType(String))
            Next

            Dim oProc As New clsPOCOConverter(myContext)

            If info.error_report IsNot Nothing Then
                Me.PopulateDataset(info.error_report, dsGSTN)
            End If

            For Each dt1 As DataTable In dsDB.Tables
                dt1.Columns.Add("gstn_status_cd", GetType(String))
                dt1.Columns.Add("gstn_error_cd", GetType(String))
                dt1.Columns.Add("gstn_error_msg", GetType(String))
            Next

            For Each dt1 As DataTable In dsDB.Tables
                If dt1.Columns.Contains("invoiceid") Then
                    Dim dt2 As DataTable = myContext.Data.SelectDistinct(dt1, If(myUtils.StartsWith(dt1.TableName, "cdn"), "invoiceid,nt_num,nt_dt", "invoiceid,inum,idt"))

                    For Each r2 As DataRow In dt2.Select
                        Dim strf1 As String = myContext.SQL.GenerateSQLWhere(r2, If(myUtils.StartsWith(dt1.TableName, "cdn"), "nt_num,nt_dt", "inum,idt"))
                        Dim rr() As DataRow = dsGSTN.Tables(dt1.TableName).Select(strf1)

                        If rr.Length > 0 Then
                            r2("gstn_status_cd") = "ER"
                            r2("gstn_error_cd") = rr(0)("error_cd")
                            r2("gstn_error_msg") = rr(0)("error_msg")
                        Else
                            r2("gstn_status_cd") = info.status_cd
                            r2("gstn_error_cd") = If(info.error_report Is Nothing, "", info.error_report.error_cd)
                            r2("gstn_error_msg") = If(info.error_report Is Nothing, "", info.error_report.error_msg)
                        End If
                    Next

                    myContext.Provider.objSQLHelper.SaveResults(dt2, "select invoiceid,gstn_status_cd,gstn_error_cd,gstn_error_msg from invoice where 0=1")
                End If
            Next
        End If
    End Sub

    Protected Friend Async Function GetFileDetails(rTrans As DataRow, client As GSTNReturnsClient(Of TData, TSummary)) As Task(Of GSTNDownload(Of TData))
        Dim oRet As New GSTNDownload(Of TData)
        Dim apiTaskID As String = myUtils.cStrTN(rTrans("ApiTaskID"))
        Dim token As String = myUtils.cStrTN(rTrans("Token"))
        Dim timeSecs As Integer = 0     'Stop trying if 15 mins have elapsed while trying.

        If token.Trim.Length > 0 Then
            Dim info As FileDetInfo

            While ((info Is Nothing) OrElse (info.fc = 0)) AndAlso (timeSecs < 30 * 60)
                Await Task.Delay(30000)

                timeSecs = timeSecs + 30

                info = client.GetFileDetails(token).Data

                myContext.Logger.logInformation("FILEDET:" & token)
            End While

            SyncLock objLock
                If info Is Nothing Then
                    rTrans("statusinfo") = client.dicParams("ResponseError")
                    myContext.Provider.objSQLHelper.SaveResults(rTrans.Table, "select GSTNTransactionId, statusinfo from gstntransaction")
                    oRet.OutMessage = "Could not obtain file details"
                Else
                    rTrans("fc") = info.fc
                    myContext.Provider.objSQLHelper.SaveResults(rTrans.Table, "select GSTNTransactionId, fc from gstntransaction")
                    oRet.Fc = info.fc
                End If
            End SyncLock

            If info IsNot Nothing Then
                Dim strDirectoryPath = myContext.App.objAppExtender.MapPath(".\App_Data\downloads")
                Dim folderName = System.IO.Path.Combine(strDirectoryPath, "FILEDET-Responses", apiTaskID + " -- " + token)
                If Not System.IO.Directory.Exists(folderName) Then System.IO.Directory.CreateDirectory(folderName)
                oRet.Files = Await Me.DownloadFiles(client.provider, info, folderName)

            End If
        Else
            oRet.OutMessage = "Token not defined"
        End If
        Return oRet
    End Function
    Public Function CleanGSTN(GstRegID As Integer, ReturnPeriodID As Integer, results As List(Of TData)) As clsProcOutput
        Dim oRet As New clsProcOutput
        Me.PrepareCleanTPModel(results)
        Dim rGstReg As DataRow = Me.oMaster.GstRegRow(GstRegID)
        Dim rPP As DataRow = Me.oMaster.rPostPeriod(ReturnPeriodID)

        Dim lst = results.Select(Of GSTNPayload(Of TData))(Function(x)
                                                               Dim obj = New GSTNPayload(Of TData)
                                                               obj.Data = x
                                                               obj.Data.fp = Me.CalcGstnRetPd(rGstReg, rPP)
                                                               obj.Data.gstin = rGstReg("gstin")
                                                               Return obj
                                                           End Function).ToList
        Dim rToken = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
        Dim auth = Me.GetAuthClientFromToken(rToken, rGstReg)
        Dim client As TPClient = Activator.CreateInstance(GetType(TPClient), New Object() {auth, rGstReg("gstin"), rGstReg("gstnuserid"), Me.CalcGstnRetPd(rGstReg, rPP)})
        oRet = Me.UploadGSTR(GstRegID, ReturnPeriodID, client, lst, "CLR")
        Return oRet
    End Function
    Protected Friend Function GenerateModels(GstRegID As Integer, ReturnPeriodID As Integer, dic As clsCollecString(Of String)) As clsCollecString(Of GSTNPayload(Of TData))
        Dim LimitCrossed = Me.RecordCountExceedsLimit(dic, 5000), lst As New clsCollecString(Of GSTNPayload(Of TData))
        If LimitCrossed Then
            lst = Me.GeneratePagedModels(GstRegID, ReturnPeriodID, dic)
        Else
            Dim model = Me.GenerateSingleModel(GstRegID, ReturnPeriodID, dic)
            lst.Add(Me.ReturnCode, model)
        End If
        Return lst
    End Function
    Protected Friend Function GenerateSingleModel(GstRegID As Integer, ReturnPeriodID As Integer, dic As clsCollecString(Of String)) As GSTNPayload(Of TData)
        Dim rGstReg As DataRow = Me.oMaster.GstRegRow(GstRegID)
        Dim rPP As DataRow = Me.oMaster.rPostPeriod(ReturnPeriodID)
        Dim model As GSTNPayload(Of TData) = Activator.CreateInstance(GetType(GSTNPayload(Of TData)))
        model.Data.fp = Me.CalcGstnRetPd(rGstReg, rPP)
        model.Data.gstin = rGstReg("gstin")
        Dim lst As New List(Of DataSet)
        For Each kvp In dic
            Dim ds2 As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, kvp.Value)
            ds2.Tables(0).TableName = kvp.Key
            Me.PopulateObject(model.Data, ds2)
            lst.Add(ds2)
        Next
        model.IDList = New clsCollecString(Of List(Of Integer))
        For Each dt1 As DataTable In lst.Select(Function(x) x.Tables(0))
            Dim dic2 = Me.GenerateIDList(dt1, "InvoiceID", "gstAdvanceVouchID", "CPInvoiceID", "GSTNActionID")
            For Each kvp In dic2
                If model.IDList.Exists(kvp.Key) Then
                    model.IDList(kvp.Key).AddRange(kvp.Value)
                Else
                    model.IDList.Add(kvp.Key, kvp.Value)
                End If
            Next
        Next
        Return model
    End Function



    Protected Friend Function GeneratePagedModels(GstRegID As Integer, ReturnPeriodID As Integer, dic As clsCollecString(Of String)) As clsCollecString(Of GSTNPayload(Of TData))
        Dim rGstReg As DataRow = Me.oMaster.GstRegRow(GstRegID)
        Dim rPP As DataRow = Me.oMaster.rPostPeriod(ReturnPeriodID)

        Dim lst As New clsCollecString(Of GSTNPayload(Of TData))
        For Each kvp In dic
            Dim sql As String = "select * from (" & kvp.Value & ") as t1 where 0=1", sqlInvoice As String = ""
            Dim ds1 As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
            Dim IDField As String = ds1.Tables(0).Columns(0).ColumnName
            Dim count As Integer = 0, pgCount As Integer = 1, pgSize As Integer = 10
            If myUtils.IsInList(IDField, "invoiceid", "isdinvoiceid") Then
                sql = "if exists (select * from tempdb.dbo.sysobjects where id = object_id('tempdb..#payload')) exec ('drop table [#payload]')"
                myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, sql)
                sql = String.Format("select distinct {0} into #payload from ({1}) as t1", IDField, kvp.Value)
                myContext.Provider.objSQLHelper.ExecuteNonQuery(CommandType.Text, sql)
                sql = String.Format("select count(*) from #payload", IDField, kvp.Value)
                ds1 = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql)
                count = ds1.Tables(0).Rows(0)(0)
                pgSize = CInt(Math.Max(Math.Min(count / 20, 5000), 500))       'count/20, subject to minimum of 500 and max of 5000
                'pagesize = 10       'test
                pgCount = CInt(Math.Ceiling(count / pgSize))
            End If

            If count = 0 OrElse pgCount = 1 Then
                Dim ds2 As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, kvp.Value)
                If ds2.Tables(0).Select.Length > 0 Then
                    ds2.Tables(0).TableName = kvp.Key
                    Dim model As GSTNPayload(Of TData) = Activator.CreateInstance(GetType(GSTNPayload(Of TData)))
                    model.Data.fp = Me.CalcGstnRetPd(rGstReg, rPP)
                    model.Data.gstin = rGstReg("gstin")
                    Me.PopulateObject(model.Data, ds2)
                    model.IDList = Me.GenerateIDList(ds2.Tables(0), "InvoiceID", "gstAdvanceVouchID", "CPInvoiceID", "GSTNActionID", "ChallanID", "ISDInvoiceID")
                    lst.Add(kvp.Key, model)
                End If
            Else
                For i As Integer = 0 To pgCount - 1
                    Dim strf1 As String = String.Format("{0} in (select {0} from {1} order by {0} OFFSET {2} ROWS FETCH NEXT {3} ROWS ONLY)", IDField, "#payload", i * pgSize, pgSize)
                    Dim strf2 As String = "GstRegID=" & GstRegID
                    Dim newSQL = kvp.Value.Replace(strf2, myUtils.CombineWhere(False, strf1, strf2))
                    Dim ds2 As DataSet = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, newSQL)
                    ds2.Tables(0).TableName = kvp.Key
                    Dim model As GSTNPayload(Of TData) = Activator.CreateInstance(GetType(GSTNPayload(Of TData)))
                    model.Data.fp = Me.CalcGstnRetPd(rGstReg, rPP)
                    model.Data.gstin = rGstReg("gstin")
                    model.IDList = Me.GenerateIDList(ds2.Tables(0), "InvoiceID", "gstAdvanceVouchID", "CPInvoiceID", "GSTNActionID", "ChallanID", "ISDInvoiceID")
                    Me.PopulateObject(model.Data, ds2)
                    lst.Add(kvp.Key & "_Page" & i + 1, model)
                Next
            End If
        Next


        Return lst
    End Function



    Public Overridable Async Function GetGSTNStatusAsync(GstRegID As Integer, ReturnPeriodID As Integer, ApiTaskID As Guid) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput

        Dim dic As New clsCollecString(Of String)
        dic.Add("trans", "select * from gstntransaction where reference_id is not null and apitaskid='" & ApiTaskID.ToString & "'")
        Dim ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
        Dim nrRows = ds.Tables("trans").Rows.Cast(Of DataRow).ToList
        Dim dtStartTime As DateTime = DateTime.Now
        While nrRows.Count > 0
            For Each nrTrans As DataRow In nrRows
                oRet.AddDataRow("trans", nrTrans)
                Dim client = Me.GetClient(GstRegID, ReturnPeriodID)
                oRet = oRet + Await Me.GetStatus(nrTrans, client)
            Next
            If ((DateTime.Now - dtStartTime) < TimeSpan.FromHours(3)) Then
                Dim inProcessNrRows = nrRows.Where(Function(p) myUtils.IsInList(p("statuscode").ToString, "IP")).ToList
                nrRows.Clear()
                nrRows.AddRange(inProcessNrRows)
            End If
        End While
        'Removed TaskList and moved to single thread because it would need many provider and GSTN also does not like many calls at a time.
        Return oRet

    End Function
    Public Function GenerateSignData(GstRegID As Integer, ReturnPeriodID As Integer, forcefresh As Boolean) As DataRow
        Dim nr As DataRow
        Dim rGstPP As DataRow = oMaster.GstRegPPRow(GstRegID, ReturnPeriodID)
        If (Not rGstPP Is Nothing) Then
            If myUtils.IsInList(myUtils.cStrTN(rGstPP(ReturnCode)), "S", "") Then
                'only submitted return can be filed
                nr = Me.GetSignRow(rGstPP, False)
                If (forcefresh) OrElse (String.IsNullOrEmpty(myUtils.cStrTN(nr("hashedpayload")))) Then
                    Dim rPP As DataRow = oMaster.rPostPeriod(ReturnPeriodID)
                    Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
                    Dim rToken = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
                    Dim auth = Me.GetAuthClientFromToken(rToken, rGstReg)
                    Dim client As TPClient = Activator.CreateInstance(GetType(TPClient), New Object() {auth, rGstReg("gstin"), rGstReg("gstnuserid"), Me.CalcGstnRetPd(rGstReg, rPP)})
                    Dim oRet = Me.GetSummary(GstRegID, ReturnPeriodID, client)
                    If Not oRet.Data Is Nothing Then
                        nr = Me.GetSignRow(rGstPP, True)
                        nr("returnkey") = ReturnCode
                        Me.PopulateSignRow(nr, client.dicParams("ResponsePayload"))
                        myContext.Provider.objSQLHelper.SaveResults(nr.Table, "select * from gstnsign where 0=1")
                    End If
                End If
                Return nr
            End If
        End If



    End Function
    Protected Friend Overridable Sub PopulateSignRow(nr As DataRow, payload As String)
        Dim Base64Payload = Convert.ToBase64String(Encoding.UTF8.GetBytes(payload))
        nr("payload") = Base64Payload
        Dim hash = EncryptionUtils.sha256_hash(Base64Payload)
        nr("hashedpayload") = EncryptionUtils.convertByteArrayToString(hash)
    End Sub

    Public Function FileSignedReturn(rSign As DataRow) As clsProcOutput
        'https://groups.google.com/forum/#!searchin/gst-suvidha-provider-gsp-discussion-group/signing$20|sort:date/gst-suvidha-provider-gsp-discussion-group/Zn_bHRgy4D4/Mb6USe0AAQAJ
        Dim oRet As New clsProcOutput
        Dim rPP As DataRow = oMaster.rPostPeriod(rSign("ReturnPeriodID"))
        Dim rGstReg As DataRow = oMaster.GstRegRow(rSign("GstRegID"))
        Dim rToken = Me.GetActiveToken(myContext.Police.UniqueUserID, rSign("GstRegID"))
        Dim auth = Me.GetAuthClientFromToken(rToken, rGstReg)
        Dim client As TPClient = Activator.CreateInstance(GetType(TPClient), New Object() {auth, rGstReg("gstin"), rGstReg("gstnuserid"), Me.CalcGstnRetPd(rGstReg, rPP)})
        Dim result = client.File(rSign("payload").ToString, rSign("SignedPayload"), "DSC", rGstReg("AuthPAN"))
        Dim nrTrans As DataRow = Me.SaveTransaction(rSign("GstRegID"), rSign("ReturnPeriodID"), "RETFILE", "", client.dicParams)
        oRet.AddDataRow("trans", nrTrans)
        If result.Data Is Nothing Then
            oRet.AddError(client.dicParams("ResponseError"))
        Else
            rSign("acknum") = result.Data.ack_num
            Me.OnGSTRAction(rSign("GstRegID"), rSign("ReturnPeriodID"), rSign("ReturnKey"), "F")
        End If

        Return oRet

    End Function
    Public Overridable Function SubmitReturn(GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Dim rPP As DataRow = oMaster.rPostPeriod(ReturnPeriodID)
        Dim rGstReg As DataRow = oMaster.GstRegRow(GstRegID)
        Dim rToken = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
        Dim auth = Me.GetAuthClientFromToken(rToken, rGstReg)
        Dim client As TPClient = Activator.CreateInstance(GetType(TPClient), New Object() {auth, rGstReg("gstin"), rGstReg("gstnuserid"), Me.CalcGstnRetPd(rGstReg, rPP)})
        Dim result = client.Submit

        If result.Data IsNot Nothing Then
            If String.IsNullOrEmpty(result.Data.reference_id) Then
                oRet.AddError("Error - could not submit")
            Else
                Dim dic = New Dictionary(Of String, String) From {{"InfoJson", Me.CreateParamsJson(GstRegID, ReturnPeriodID)}}
                Dim rTask As DataRow = TaskProviderFactory.CreateApiTask(myContext.Provider, Me.ReturnCode, "STATUS", rGstReg("companyid"), dic)

                Dim nrTrans As DataRow = Me.SaveTransaction(GstRegID, ReturnPeriodID, "RETSUBMIT", "", client.dicParams, Guid.Parse(rTask("ApiTaskID").ToString))
                oRet.AddDataRow("trans", nrTrans)

                Dim queueName = myContext.Controller.CalcQueueName
                Dim oRet2 = TaskProviderFactory.Enqueue(myContext.Provider, rTask, queueName)

                oRet.AddMessage("Uploaded. Status Query Queued")
            End If

        End If

        Return oRet

    End Function
    Public Overridable Function CreateZipFile(FileName As String, lst As List(Of GSTNDownload(Of TData))) As String
        Try
            Dim BaseFolder = myContext.App.objAppExtender.MapPath("app_data/downloads/" & System.IO.Path.GetFileNameWithoutExtension(FileName))
            myContext.FTP.RecreateLocalDirectory(BaseFolder)

            For Each result In lst
                Dim NewPath As String = System.IO.Path.Combine(BaseFolder, result.GSTIN, result.ret_pd)
                myContext.FTP.EnsureLocalDirectory(NewPath)
                For Each file In result.Files
                    If file.Data IsNot Nothing Then
                        Dim str1 As String = JsonConvert.SerializeObject(file.Data)
                        Dim NewFileName = System.IO.Path.Combine(NewPath, file.FileName)
                        My.Computer.FileSystem.WriteAllText(NewFileName, str1, False)
                    End If
                Next
            Next
            Dim zipFileName = myContext.FTP.CreateZipFileFromFolder(BaseFolder, FileName)
            Return zipFileName
        Catch ex As Exception
            myContext.Logger.logInformation(ex.Message)
        End Try
    End Function
    Public Overridable Function CleanGSTN(ApiTaskID As Guid) As clsProcOutput
        Dim oRet2 As New clsProcOutput, lst As New List(Of TData)

        Dim sql1 As String = "select gstregid, returnperiodid, requestpayload from gstntransaction where apitaskid='" & ApiTaskID.ToString & "' and requestType='PUT'"
        Dim dt1 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql1).Tables(0)
        For Each r1 As DataRow In dt1.Select
            Dim data = JsonConvert.DeserializeObject(Of TData)(myUtils.cStrTN(r1("requestpayload")))
            lst.Add(data)
        Next


        Dim sql2 As String = "select gstregid, returnperiodid, responsepayload from gstntransaction where apitaskid='" & ApiTaskID.ToString & "' and requestType='GET'"
        Dim dt2 As DataTable = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql2).Tables(0)

        For Each r2 As DataRow In dt2.Select
            Dim data = JsonConvert.DeserializeObject(Of TData)(myUtils.cStrTN(r2("responsepayload")))
            If data IsNot Nothing Then
                Dim lst2 = New List(Of TData) From {data}
                oRet2 = oRet2 + Me.CleanGSTN(r2("GSTRegID"), r2("ReturnPeriodID"), lst2)
            End If
        Next
        Return oRet2
    End Function
    Public Overridable Function CleanGSTN(Data As List(Of GSTNDownload(Of TData))) As clsProcOutput
        Dim oRet2 As New clsProcOutput
        For Each obj In Data
            Dim lst2 = obj.Files.Select(Function(x) x.Data).ToList
            oRet2 = oRet2 + Me.CleanGSTN(obj.GSTRegID, obj.ReturnPeriodID, lst2)
        Next
        Return oRet2
    End Function
    Public Overridable Function CreateFollowingTask(rDownloadTask As DataRow, Data As List(Of GSTNDownload(Of TData)), Action As String) As clsProcOutput
        Dim oRet2 As New clsProcOutput
        Dim ImportFileID = System.Guid.NewGuid
        Dim FileName As String = ImportFileID.ToString & ".zip"
        Dim ZipFileName = Me.CreateZipFile(FileName, Data)
        Dim info As New IO.FileInfo(ZipFileName)
        Dim client = myContext.App.objAppExtender.FileProviderClient(myContext, ConfigurationManager.AppSettings("StorageContainerName"))
        Dim BlobName = client.UpLoad(ZipFileName, System.IO.Path.GetFileName(ZipFileName), "")
        Dim rTask As DataRow
        Dim date1 = New DateTimeOffset(info.LastWriteTime)
        Select Case Action.Trim.ToLower
            Case "downloadcp"
                Dim nr = mxform.myFuncs.CreateImportFile(myContext, FileName, "", ImportFileID.ToString, info.Length, date1.ToUnixTimeMilliseconds, DocType)
                Dim dic As New Dictionary(Of String, String) From {{"path", FileName}}
                Dim dicParams As New Dictionary(Of String, String) From {{"InfoJson", JsonConvert.SerializeObject(dic)}, {"ImportFileID", ImportFileID.ToString}}
                rTask = TaskProviderFactory.CreateApiTask(myContext.Provider, "import", Me.ReturnCode & "a", 0, dicParams)
            Case "downloadtp"
                Dim nr = mxform.myFuncs.CreateImportFile(myContext, FileName, "", ImportFileID.ToString, info.Length, date1.ToUnixTimeMilliseconds, DocType)
                Dim dic As New Dictionary(Of String, String) From {{"path", FileName}}
                Dim dicParams As New Dictionary(Of String, String) From {{"InfoJson", JsonConvert.SerializeObject(dic)}, {"ImportFileID", ImportFileID.ToString}}
                rTask = TaskProviderFactory.CreateApiTask(myContext.Provider, "import", Me.ReturnCode, 0, dicParams)
        End Select

        If rTask Is Nothing Then
            oRet2.AddError("Task not found")
        Else
            Dim queueName = myContext.Controller.CalcQueueName
            oRet2 = TaskProviderFactory.Enqueue(myContext.Provider, rTask, queueName)
            oRet2.Description = ZipFileName
        End If
        Return oRet2
    End Function
    Public Overridable Async Function CreateStatusReport(ZipFileName As String, Data As List(Of GSTNDownload(Of TData))) As Task(Of clsProcOutput(Of BlobUriWithSas))
        Dim oRet As New clsProcOutput(Of BlobUriWithSas), lst3 As New List(Of GSTNOutput)
        Try
            Data.ForEach(Sub(x1)
                             If x1.Files.Count > 0 Then
                                 x1.Files.ForEach(Sub(x2)
                                                      Dim y = New GSTNOutput With {.GSTIN = x1.GSTIN, .Fc = x1.Fc, .TransType = x1.TransType,
                                                                                .ret_pd = x1.ret_pd, .FileName = x2.FileName, .Message = x2.Message}
                                                      lst3.Add(y)
                                                  End Sub)
                             Else
                                 Dim y = New GSTNOutput With {.GSTIN = x1.GSTIN, .Fc = x1.Fc, .TransType = x1.TransType,
                                                                                .ret_pd = x1.ret_pd, .Message = x1.OutMessage}
                                 lst3.Add(y)
                             End If
                         End Sub)
            lst3 = lst3.OrderBy(Function(x)
                                    Return x.GSTIN & "_" & x.ret_pd & "_" & x.TransType & "_" & x.FileName
                                End Function).ToList
            Dim oProc As New clsPOCOConverter(myContext)
            Dim ds = oProc.GenerateTable(lst3).DataSet
            Dim OutputFileName As String = Path.GetFileNameWithoutExtension(ZipFileName) & "_status.xlsx"
            Dim oRet2 = myFuncs.GenerateExcel(myContext, ds.Tables.Cast(Of DataTable).ToList, OutputFileName)
            Dim client = myContext.App.objAppExtender.FileProviderClient(myContext, ConfigurationManager.AppSettings("StorageContainerName"))
            Dim BlobName = Await client.UpLoadAsync(oRet2.Description, System.IO.Path.GetFileName(oRet2.Description), "")
            oRet = Await client.GetDownloadUri(BlobName)
            oRet.Data = ds
        Catch ex As Exception
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function
    Public Overloads Async Function DownloadGSTNFilter(CampusFilter As String, PeriodFilter As String, Action As String) As Task(Of clsProcOutput(Of List(Of GSTNDownload(Of TData))))
        Dim dic As New clsCollecString(Of String), oRet As New clsProcOutput(Of List(Of GSTNDownload(Of TData)))
        oRet.Result = New List(Of GSTNDownload(Of TData))
        Try
            dic.Add("gstreg", "select * from gstreg where " & CampusFilter)
            dic.Add("pp", "select * from (select *,postperiodid as returnperiodid from postperiod) as t1 where " & PeriodFilter)
            Dim ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
            Dim TaskList As New List(Of Task(Of List(Of GSTNDownload(Of TData)))), DoneCount As Integer = 0


            Dim queue As New ConcurrentQueue(Of Tuple(Of DataRow, DataRow))
            For Each rGst As DataRow In ds.Tables("gstreg").Select
                For Each rPP As DataRow In ds.Tables("pp").Select
                    queue.Enqueue(New Tuple(Of DataRow, DataRow)(rGst, rPP))
                Next
            Next

            Dim Numthreads As Integer = Environment.ProcessorCount - 1
            If Numthreads > queue.Count Then Numthreads = queue.Count
            If Numthreads = 0 Then Numthreads = 1
            myContext.Logger.logInformation(String.Format("Period Nos ={0}, Processor Nos={1}, Thread Nos. =  {2}", queue.Count, Environment.ProcessorCount, Numthreads))
            For i As Integer = 1 To Numthreads
                TaskList.Add(Task.Run(Async Function()
                                          Dim data As Tuple(Of DataRow, DataRow), lstOut As New List(Of GSTNDownload(Of TData))
                                          While queue.TryDequeue(data)
                                              myContext.Logger.logInformation($"Begin Filtered download for {data.Item1("gstin")} and period {data.Item2("ret_pd")}")
                                              Dim result As New List(Of GSTNDownload(Of TData))
                                              If myUtils.IsInList(Action, "downloadcp") Then
                                                  result = Await Me.DownloadGSTNCp(data.Item1("gstregid"), data.Item2("postperiodid"))
                                              Else
                                                  result = Await Me.DownloadGSTNTp(data.Item1("gstregid"), data.Item2("postperiodid"), Action)
                                              End If
                                              If result IsNot Nothing Then
                                                  result.ForEach(Sub(x)
                                                                     x.GSTIN = data.Item1("gstin")
                                                                     x.ret_pd = data.Item2("ret_pd")
                                                                     x.Files.ForEach(Sub(y)
                                                                                         y.Data.gstin = x.GSTIN
                                                                                         y.Data.fp = x.ret_pd
                                                                                     End Sub)
                                                                 End Sub)
                                                  lstOut.AddRange(result)
                                              End If
                                              Dim cnt2 = Interlocked.Add(DoneCount, 1)
                                              myContext.Logger.logInformation($"End Filtered download for {data.Item1("gstin")} and period {data.Item2("ret_pd")}" &
                                                String.Format("ThreadID={0}, Queue Count = {1}, Done Count = {2}", Threading.Thread.CurrentThread.ManagedThreadId, queue.Count, cnt2))
                                          End While
                                          Return lstOut
                                      End Function)
                     )
                If queue.Count = 0 Then Exit For
            Next

            Dim lst2 = Await Task.WhenAll(TaskList.ToArray())
            For Each oRetList2 In lst2
                oRet.Result.AddRange(oRetList2)
            Next


        Catch ex As Exception
            myContext.Logger.logInformation(ex.Message)
            oRet.AddException(ex)
        End Try



        Return oRet

    End Function

    Public Overloads Async Function DownloadGSTNCp(GstRegID As Integer, ReturnPeriodID As Integer) As Task(Of List(Of GSTNDownload(Of TData)))
        Dim rToken As DataRow, auth As IGSTNAuthProvider, ds As DataSet
        Dim rPP As DataRow = Me.oMaster.rPostPeriod(ReturnPeriodID)
        Dim rGstReg As DataRow = Me.oMaster.GstRegRow(GstRegID)
        SyncLock objLock
            rToken = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
            auth = Me.GetAuthClientFromToken(rToken, rGstReg)
        End SyncLock
        Dim client As CPClient = Activator.CreateInstance(GetType(CPClient), New Object() {auth, rGstReg("gstin"), rGstReg("gstnuserid"), Me.CalcGstnRetPd(rGstReg, rPP)})
        Dim lst = Await Me.DownloadGstnCp(GstRegID, ReturnPeriodID, client)
        Return lst
    End Function
    Public Overloads Async Function DownloadGSTNTp(GstRegID As Integer, ReturnPeriodID As Integer, Action As String) As Task(Of List(Of GSTNDownload(Of TData)))
        Dim rToken As DataRow, auth As IGSTNAuthProvider, ds As DataSet
        Dim rPP As DataRow = Me.oMaster.rPostPeriod(ReturnPeriodID)
        Dim rGstReg As DataRow = Me.oMaster.GstRegRow(GstRegID)
        SyncLock objLock
            rToken = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
            auth = Me.GetAuthClientFromToken(rToken, rGstReg)
            Dim dic = Me.PrepareGSTRPayloadSQLUp(0, 0)
            ds = myContext.Provider.objSQLHelper.ExecuteDataset(CommandType.Text, dic)
        End SyncLock
        Dim client As TPClient = Activator.CreateInstance(GetType(TPClient), New Object() {auth, rGstReg("gstin"), rGstReg("gstnuserid"), Me.CalcGstnRetPd(rGstReg, rPP)})
        Dim lst = Await Me.DownloadGSTNTp(GstRegID, ReturnPeriodID, client, ds, Action)
        Return lst
    End Function
    Public Overridable Overloads Function GetSummary(GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput(Of TSummary)
        Dim rPP As DataRow = Me.oMaster.rPostPeriod(ReturnPeriodID)
        Dim rGstReg As DataRow = Me.oMaster.GstRegRow(GstRegID)
        Dim rToken = Me.GetActiveToken(myContext.Police.UniqueUserID, GstRegID)
        Dim auth = Me.GetAuthClientFromToken(rToken, rGstReg)
        Dim client As TPClient = Activator.CreateInstance(GetType(TPClient), New Object() {auth, rGstReg("gstin"), rGstReg("gstnuserid"), Me.CalcGstnRetPd(rGstReg, rPP)})
        Dim oRet = Me.GetSummary(GstRegID, ReturnPeriodID, client)
        Return oRet
    End Function

    Public Overloads Function GetJsonSummary(GstRegID As Integer, ReturnPeriodID As Integer) As clsProcOutput
        Dim oRet = Me.GetSummary(GstRegID, ReturnPeriodID)
        Dim payload As String = JsonConvert.SerializeObject(oRet, Newtonsoft.Json.Formatting.Indented,
                            New JsonSerializerSettings With {
                                .NullValueHandling = NullValueHandling.Ignore
                            })
        oRet.JsonData = New JsonDataResult With {.message = oRet.Message, .Data = payload, .success = oRet.Success}

        Return oRet
    End Function
    Public Overrides Function ImportJson(filePath As String, CounterParty As Boolean) As clsProcOutput
        Dim lst As New List(Of TData), oRet As New clsProcOutput
        If myUtils.IsInList(Path.GetExtension(filePath), ".zip") Then
            Dim strFolder As String = myContext.FTP.ExtractZipAsLocalFolder(filePath)
            Dim lst1 = System.IO.Directory.GetFiles(strFolder, "*.*", SearchOption.AllDirectories)
            For Each filePath2 In lst1
                Dim str1 As String = My.Computer.FileSystem.ReadAllText(filePath2)
                Try
                    Dim result = JsonConvert.DeserializeObject(Of TData)(str1)
                    lst.Add(result)
                Catch ex As Exception
                    myContext.Logger.logInformation("Deserialize error: " & ex.Message)
                End Try
            Next
        Else
            Dim str1 As String = My.Computer.FileSystem.ReadAllText(filePath)
            Try
                Dim result = JsonConvert.DeserializeObject(Of TData)(str1)
                lst.Add(result)
            Catch ex As Exception
                myContext.Logger.logInformation("Deserialize error: " & ex.Message)
            End Try
        End If

        oRet.Data = If(CounterParty, Me.PopulateDataCP(lst), Me.PopulateDataTP(lst))
        For Each dt1 As DataTable In oRet.Data.Tables
            For Each str1 As String In New String() {"invoiceid", "cpinvoiceid"}
                If dt1.Columns.Contains(str1) Then dt1.Columns.Remove(str1)
            Next
        Next
        If lst.Count = 0 Then oRet.AddError("Could not deserialize data")
        Return oRet

    End Function
    Public Async Function DownloadFiles(auth As IGSTNAuthProvider, ByVal info As FileDetInfo, ByVal FolderPath As String) As Task(Of List(Of GSTNDataFile(Of TData)))
        Dim lst = New List(Of GSTNDataFile(Of TData))
        Dim queue As New ConcurrentQueue(Of String)
        For Each url In info.urls
            Dim url2 = auth.credential.base_url_file + url.ul
            queue.Enqueue(url2)
        Next

        Dim TaskList As New List(Of Task(Of List(Of GSTNDataFile(Of TData)))), DoneCount As Integer = 0
        Dim Numthreads As Integer = If(Environment.ProcessorCount <= 2, 1, CInt(Environment.ProcessorCount * 1.5))
        If Numthreads > queue.Count Then Numthreads = queue.Count
        myContext.Logger.logInformation(String.Format("Url Nos ={0}, Processor Nos={1}, Thread Nos. =  {2}", queue.Count, Environment.ProcessorCount, Numthreads))
        For i As Integer = 1 To Numthreads
            TaskList.Add(Task.Run(Async Function()
                                      Dim URL As String = "", lstOut As New List(Of GSTNDataFile(Of TData))
                                      While queue.TryDequeue(URL)
                                          Dim client2 As New clsGSTNFileDownloader(Of TData)(auth)
                                          Dim result = Await client2.DownloadFile(URL, FolderPath, info.ek)
                                          If result IsNot Nothing Then lstOut.Add(result)
                                          Dim cnt2 = Interlocked.Add(DoneCount, 1)
                                          myContext.Logger.logInformation(String.Format("Finish URL = {0}, ThreadID={1}, Queue Count = {2}, Done Count = {3}", URL, Threading.Thread.CurrentThread.ManagedThreadId, queue.Count, cnt2))
                                      End While
                                      Return lstOut
                                  End Function)
                     )
            If queue.Count = 0 Then Exit For
        Next

        Dim lst2 = Await Task.WhenAll(TaskList.ToArray())
        For Each resultList In lst2
            lst.AddRange(resultList)
        Next
        Return lst
    End Function

End Class
Public Class GSTNDownload(Of TData)
    Public Property GSTRegID As Integer
    Public Property ReturnPeriodID As Integer
    Public Property GSTIN As String
    Public Property ret_pd As String
    Public Property Fc As Integer
    Public Property rTrans As DataRow
    Public Property TransType As String
    Public Property OutMessage As String
    Public Property Files As New List(Of GSTNDataFile(Of TData))
End Class
Public Class GSTNDataFile(Of TData)
    Public Property Data As TData
    Public Property FileName As String
    Public Property Message As String
End Class
Public Class GSTNOutput
    Public Property GSTIN As String
    Public Property ret_pd As String
    Public Property TransType As String
    Public Property FileName As String
    Public Property Fc As Integer
    Public Property Message As String
End Class
