Imports Newtonsoft.Json
Imports risersoft.shared
Imports System.IO
Imports risersoft.shared.dal
Imports risersoft.app.dataporter
Imports System.Reflection
Imports risersoft.app.mxent
Imports risersoft.API.GSTN.GSTR2
Imports risersoft.shared.cloud
Imports risersoft.shared.portable.Models.Auth
Imports GSTN.API.Library.Models.GstNirvana
Imports System.Configuration
Imports System.Collections.Generic
Imports System.Collections.Concurrent

Public Class Import_GSTR2ATaskProvider
    Inherits ImportTaskProviderGstBase

    Public Overrides Property DocType As String = "IP"
    Public Overrides Property UpdateBatchSize As Integer = 0

    Public Sub New(controller As clsAppController)
        MyBase.New(controller)

    End Sub

    Public Overrides ReadOnly Property IsApiTask As Boolean = True



    Public Overrides Async Function ExecuteServer(rTask As DataRow, input As clsProcOutput) As Task(Of clsProcOutput)
        Dim Params = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(myUtils.cStrTN(rTask("infojson")))
        Dim filepath = Await myFuncs.DownloadIfReqd(myContext, rTask, Params("path"))
        Return Await Me.ExecuteImport(filepath, myUtils.cStrTN(rTask("username")), myUtils.cStrTN(rTask("importfileid")))
    End Function
    Public Overrides Async Function ExecuteImport(filePath As String, username As String, ImportFileID As String) As Task(Of clsProcOutput)
        Dim lst As New List(Of GSTR2Total), oRet As New clsProcOutput(Of GstImportInfo)
        Guid.TryParse(ImportFileID, Me.ImportFileID)
        oRet.Result = New GstImportInfo
        Me.PopulateMaster()
        If myUtils.IsInList(Path.GetExtension(filePath), ".zip") Then
            Dim strFolder As String = myContext.FTP.ExtractZipAsLocalFolder(filePath)
            Dim lst1 = System.IO.Directory.GetFiles(strFolder, "*.json", SearchOption.AllDirectories)
            Dim TaskList As New List(Of Task(Of GSTR2Total))

            For Each filePath2 In lst1
                Dim str1 As String = My.Computer.FileSystem.ReadAllText(filePath2)
                TaskList.Add(Task.Run(Function()
                                          Dim result As GSTR2Total = Nothing
                                          Try
                                              result = JsonConvert.DeserializeObject(Of GSTR2Total)(str1)
                                          Catch ex As Exception
                                              myContext.Logger.logInformation("Deserialize error: " & ex.Message)
                                          End Try
                                          Return result
                                      End Function))
            Next
            lst = New List(Of GSTR2Total)(Await Task.WhenAll(TaskList.ToArray))
            lst.RemoveAll(Function(x) x Is Nothing)

        Else
            Dim str1 As String = My.Computer.FileSystem.ReadAllText(filePath)
            Try
                Dim result = JsonConvert.DeserializeObject(Of GSTR2Total)(str1)
                lst.Add(result)
            Catch ex As Exception
                myContext.Logger.logInformation("Deserialize error: " & ex.Message)
            End Try
        End If

        If lst.Count > 0 Then
            Dim TaskList2 As New List(Of Task(Of List(Of clsProcOutput(Of GstImportInfoGSTIN))))
            Dim queue As New ConcurrentQueue(Of clsGstJsonPayload)
            Dim TotNum As Integer = 0

            For Each result In lst
                Try
                    Dim rPP As DataRow = oMaster.rPostPeriod2(result.fp)
                    Dim rGstReg As DataRow = oMaster.GstRegRow2(result.gstin)
                    If rPP Is Nothing Then
                        oRet.AddError("Invalid period " & result.fp)
                    ElseIf rGstReg Is Nothing Then
                        oRet.AddError("Invalid GSTIN " & result.gstin)
                    Else
                        Dim oProc As New clsGSTNReturnGSTR2(myContext)
                        Dim ds2 = oProc.PopulateDataCP(New List(Of GSTR2Total)({result}))
                        For Each dt1 As DataTable In ds2.Tables
                            For Each str1 As String In New String() {"errorcode", "errortext", "warningcode", "warningtext"}
                                If Not dt1.Columns.Contains(str1) Then
                                    dt1.Columns.Add(str1, GetType(String))
                                End If
                            Next
                        Next

                        Dim obj = New clsGstJsonPayload() With {.GstRegID = rGstReg("GstRegID"), .ReturnPeriodID = rPP("postPeriodID"), .ds = ds2}
                        TotNum = TotNum + obj.CalcRows()
                        myContext.Logger.logInformation($"Queued payload for GSTIN {result.gstin} for period {result.fp}, Count = {obj.NumRows}")
                        queue.Enqueue(obj)
                        If (oRet.Data Is Nothing) OrElse (oRet.Data.Tables.Count = 0) Then oRet.Data = ds2.Clone

                    End If
                Catch ex As Exception
                    oRet.AddException(ex)
                    myContext.Logger.logInformation("Process error: " & ex.Message)
                End Try
            Next

            Dim NumThreads As Integer = 5
            If NumThreads > queue.Count Then NumThreads = queue.Count
            For i As Integer = 1 To NumThreads
                TaskList2.Add(Task.Run(Async Function()
                                           Dim item As clsGstJsonPayload = Nothing, lstOut As New List(Of clsProcOutput(Of GstImportInfoGSTIN))
                                           While queue.TryDequeue(item)
                                               Dim oRet2 = Await Me.UpdateDownloadedInvoiceCP(item.GstRegID, item.ReturnPeriodID, item.ds)
                                               lstOut.Add(oRet2)
                                               myContext.Logger.logInformation(String.Format("Finished GstRegID = {0}, ReturnPeriodID = {1}, ThreadId = {2}", item.GstRegID, item.ReturnPeriodID, Threading.Thread.CurrentThread.ManagedThreadId))
                                           End While
                                           Return lstOut
                                       End Function)
                         )
                If queue.Count = 0 Then Exit For
            Next

            Dim lst2 = Await Task.WhenAll(TaskList2.ToArray)


            For Each oRetList2 In lst2
                For Each oRet2 In oRetList2
                    Dim newInfo = oRet.Result.AddInfo(oRet2.Result)
                    If (Not newInfo Is Nothing) AndAlso (String.IsNullOrEmpty(newInfo.State)) Then
                        Dim rr() As DataRow = dsMaster.Tables("taxarea").Select("TINCODE='" & Left(newInfo.GSTIN, 2) & "'")
                        If rr.Length > 0 Then newInfo.State = rr(0)("taxareacode")
                    End If
                    oRet.AddOutput(oRet2)
                    For Each tbl As DataTable In oRet2.Data.Tables
                        myUtils.CopyRows(tbl.Select, oRet.Data.Tables(tbl.TableName))
                    Next
                Next
            Next

        Else
            oRet.AddError("Could not deserialize data")
        End If

        Await Me.PostProcess(oRet, filePath, username)



        Return oRet

    End Function
    Public Overridable Async Function UpdateDownloadedInvoiceCP(GstRegID As Integer, ReturnPeriodID As Integer, dsGSTR2A As DataSet) As Task(Of clsProcOutput(Of GstImportInfoGSTIN))
        'CP FIlter = uploaded by seller or vendor
        'TPFilter = 'uploaded by reciever or us
        Dim oRet = Await Me.UpdateDownloadedInvoiceCP(GstRegID, ReturnPeriodID, dsGSTR2A, "isnull(updby,'') not in ('r')", "updby='R'")
        Return oRet

    End Function

    Public Overridable Async Function UpdateDownloadedInvoiceCP(GstRegID As Integer, ReturnPeriodID As Integer, dsGSTRCP As DataSet, CPFilter As String, TPFilter As String) As Task(Of clsProcOutput(Of GstImportInfoGSTIN))
        Dim oRet As New clsProcOutput(Of GstImportInfoGSTIN), cntGood As Integer = 0, cntBad As Integer = 0
        Try
            Dim rGstReg = oMaster.GstRegRow(GstRegID)
            oRet.Result = New GstImportInfoGSTIN With {.GSTIN = rGstReg("gstin").ToString}
            For Each TableName As String In New String() {"b2b", "b2ba", "cdn", "cdna", "isd"}
                Dim strUniqueFields As String = "ctin,inum,idt"
                If myUtils.IsInList(TableName, "cdn", "cdna") Then strUniqueFields = "ctin,nt_num,nt_dt"
                Dim dt1 = dsGSTRCP.Tables(TableName)
                For Each str1 As String In New String() {"errorcode", "errortext", "warningcode", "warningtext"}
                    If Not dt1.Columns.Contains(str1) Then
                        dt1.Columns.Add(str1, GetType(String))
                    End If
                Next
                Dim rr1() = dt1.Select(CPFilter, strUniqueFields)
                Dim cnt As Integer = rr1.Length

                If cnt > 0 Then
                    Dim Numthreads As Integer = If(Environment.ProcessorCount <= 2, 1, CInt(Math.Floor(Environment.ProcessorCount * 1.5)))
                    If (Numthreads = 0) OrElse (Numthreads > cnt) Then Numthreads = 1
                    myContext.Logger.logInformation(String.Format("Row Nos = {0}, Processor Nos={1}, Thread Nos. =  {2}", cnt, Environment.ProcessorCount, Numthreads))

                    Dim PortionList As New List(Of clsPortionInfo)

                    Dim oProc As New clsTableRowProc(Of clsPortionInfo, GstImportInfoGSTIN)(myContext)
                    Dim lst = Await oProc.ExecuteBatch(rr1, Function(CurrentRow, NextRow) As Boolean
                                                                Return True
                                                            End Function,
                                             Function(CurrentRow, NextRow) As Boolean
                                                 Return myUtils.MatchRowCols(CurrentRow, NextRow, Split(strUniqueFields & ",updby", ","))
                                             End Function,
                                             AddressOf Me.CreateData,
                                             AddressOf Me.ProcessRowGroup,
                                             AddressOf Me.SaveData,
                                             Function()
                                                 Dim objPortion = New clsPortionInfo
                                                 objPortion.ErrorTable = dt1.Clone
                                                 objPortion.UniqueFields = strUniqueFields
                                                 objPortion.dicParamPortion.Add("gstregid", GstRegID)
                                                 objPortion.dicParamPortion.Add("returnperiodid", ReturnPeriodID)
                                                 PortionList.Add(objPortion)
                                                 Return objPortion
                                             End Function,
                                             Function(oRet2 As clsProcOutput(Of GstImportInfoGSTIN), objPortion As clsPortionInfo, Groups As List(Of clsRowGroup))
                                                 If oRet2.Data Is Nothing OrElse oRet2.Data.Tables.Count = 0 Then
                                                     oRet2.Data = dsGSTRCP.Clone
                                                 End If

                                                 For Each objGroup In Groups
                                                     If Not objGroup.Output.Success Then
                                                         'Now each thread has its own dtError
                                                         myUtils.CopyRows(objGroup.Rows.ToArray, oRet2.Data.Tables(objPortion.ErrorTable.TableName))
                                                     End If
                                                     myContext.Logger.logInformation(String.Format("Row group ending before {0}, Success={1}, Message={2}", objGroup.NextRowNum, objGroup.Output.Success, objGroup.Output.Message))
                                                     oRet2.Result.AddRecord(objGroup.Rows.Count, objGroup.Output.Success,
                                                    myContext.Tables.GetColSum(objGroup.Rows.ToArray, "txval"),
                                                    myContext.Tables.GetColSum(objGroup.Rows.ToArray, "iamt"),
                                                    myContext.Tables.GetColSum(objGroup.Rows.ToArray, "camt"),
                                                    myContext.Tables.GetColSum(objGroup.Rows.ToArray, "samt"),
                                                    myContext.Tables.GetColSum(objGroup.Rows.ToArray, "csamt"))

                                                 Next
                                                 Return True
                                             End Function,
                                        Numthreads, 500)
                    If oRet.Data Is Nothing Then oRet.Data = dsGSTRCP.Clone
                    For Each obj In lst
                        oRet.AddOutput(obj)
                        oRet.Result.AddInfo(obj.Result)
                        myUtils.CopyRows(obj.Data.Tables(TableName).Select, oRet.Data.Tables(TableName))
                        Dim cnt2 = obj.Data.Tables(TableName).Select.Length
                        myContext.Logger.logInformation(cnt2)
                    Next

                    If (0 = 1) Then
                        Dim dsDB As New DataSet
                        Dim dtInv As DataTable = myContext.Data.SelectDistinct(dsGSTRCP.Tables(TableName), strUniqueFields & ",updby")
                        For Each rInv As DataRow In dtInv.Select(TPFilter)     'uploaded by taxpayer
                            Dim strf As String = myContext.SQL.GenerateSQLWhere(rInv, strUniqueFields)
                            Dim rr() As DataRow = dsDB.Tables("iv").Select(strf)
                            If rr.Length > 0 Then
                                rr(0)("flag") = rInv("flag")
                                rr(0)("cflag") = rInv("cflag")
                            Else
                                'Delete from GSTN?
                            End If
                        Next
                    End If

                End If
            Next

            oRet.Message = String.Format("Import process finished. Total={0}, Success={1}, Failure={2}", cntGood + cntBad, cntGood, cntBad)
        Catch ex As Exception
            oRet.AddException(ex)
        End Try

        Return oRet


    End Function
    Protected Overrides Function GenerateSQL(dicWhere As clsCollecString(Of String)) As clsCollecString(Of String)
        Dim dic As New clsCollecString(Of String)

        dic.Add("vouch", "select * from cpinvoice where " & dicWhere("vouch"))
        dic.Add("item", "select * from cpInvoiceItem where " & dicWhere("item"))
        If myUtils.IsInList(DocType, "ip") Then
            dic.Add("vendor_0", "select * from purlistvendor() where " & dicWhere("ctin_0"))
        Else
            dic.Add("customer_0", "select * from slslistcustomer() where " & dicWhere("ctin_0"))
        End If

        Return dic
    End Function

    Protected Overrides Function GenerateSQL(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As clsCollecString(Of String)
        Dim rCPInv = objGroup.Rows(0)
        Dim strf As String = myContext.SQL.GenerateSQLWhere(rCPInv, objPortion.UniqueFields)
        Dim strf1 As String = myUtils.CombineWhere(False, strf, "gstinvoicetype='" & objPortion.ErrorTable.TableName & "'", $"Gstregid={objPortion.dicParamPortion("gstregid")}",
                            $"returnperiodid={objPortion.dicParamPortion("returnperiodid")}", $"doctype='{Me.DocType}'")

        Dim dicWhere As New clsCollecString(Of String)
        dicWhere.Add("vouch", strf1)
        dicWhere.Add("item", "cpinvoiceid in (select cpinvoiceid from cpinvoice where " & strf1 & ")")
        dicWhere.Add("ctin_0", $"gstin='{rCPInv("ctin")}'")

        objGroup.strFilter = strf1
        Return dicWhere
    End Function

    Public Overrides Async Function TryImportRowGroup(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)
        Dim dsDB = objGroup.dsDB, oRet As New clsProcOutput
        Try
            oRet = Await Me.HandleGroupData(provider, objGroup, objPortion)
            'Have a new dataset ready for data to be saved from database and copy level 1 and level 2 rows into it
            Dim rCPInv = dsDB.Tables(0).Select()(0)

        Catch ex As Exception
            oRet.AddException(ex)
        End Try

        Return oRet



    End Function
    Protected Overrides Function HandleGroupData(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput
        Dim rXL = objGroup.Rows(0)
        Dim dic = objGroup.dic
        Dim dsDB = objGroup.dsDB
        Dim rCPInv As DataRow

        If dsDB.Tables("vouch").Select.Length > 0 Then
            rCPInv = dsDB.Tables("vouch").Select()(0)
            myContext.Logger.logInformation(String.Format("Existing row for {0}, ID={1}", objGroup.strFilter, rCPInv("cpinvoiceid")))
        Else
            rCPInv = myUtils.CopyOneRow(rXL, dsDB.Tables("vouch"))
            myContext.Logger.logInformation(String.Format("New row for {0}, ID={1}", objGroup.strFilter, rCPInv("cpinvoiceid")))
        End If

        If myUtils.IsInList(myUtils.cStrTN(rCPInv("importfileid")), Me.ImportFileID.ToString) Then
            Dim str1 = String.Format("Skipping above row as it was previously imported by same importfile")
            myContext.Logger.logInformation(str1)
            'ElseIf (Not myUtils.NullNot(nr("createdon"))) AndAlso (Now.Date - CDate(nr("createdon"))).Days < 0 Then
            '    Dim str1 = String.Format("Skipping above row as it was created within 5 days and hence deemed duplicate")
            '    oRet.AddError(str1)
            '    myContext.Logger.logInformation(str1)
        Else
            Dim CampusFilter, PartyFilter As String
            rCPInv("importfileid") = Me.ImportFileID
            rCPInv("isamendment") = myUtils.IsInList(objPortion.ErrorTable.TableName, "cdna", "b2ba")
            rCPInv("gstinvoicetype") = objPortion.ErrorTable.TableName
            rCPInv("gstregid") = objPortion.dicParamPortion("GstRegID")
            rCPInv("ReturnPeriodID") = objPortion.dicParamPortion("ReturnPeriodID")
            rCPInv("doctype") = Me.DocType
            rCPInv("postingdate") = Now
            rCPInv("billof") = If(myUtils.IsInList(DocType, "IS"), "C", "P")
            rCPInv("matchcode") = "CO"
            For Each str1 As String In New String() {"txval", "iamt", "camt", "samt", "csamt"}
                rCPInv(str1 & "summ") = 0
            Next

            If myUtils.IsInList(DocType, "IS") Then
                Dim rrCust() As DataRow = dsDB.Tables("customer_0").Select()
                If rrCust.Length = 0 Then rCPInv("customerid") = DBNull.Value Else rCPInv("customerid") = rrCust(0)("customerid")
                PartyFilter = myContext.SQL.GenerateSQLWhere(rCPInv, "CustomerID")
            Else
                Dim rrVendor() As DataRow = dsDB.Tables("vendor_0").Select()
                If rrVendor.Length = 0 Then rCPInv("vendorid") = DBNull.Value Else rCPInv("vendorid") = rrVendor(0)("vendorid")
                PartyFilter = myContext.SQL.GenerateSQLWhere(rCPInv, "VendorID")
            End If

            If rXL.Table.Columns.Contains("pos") Then
                Dim rTA As DataRow = myFuncs2.FindTaxAreaRow(oMaster.TaxAreaTable, myUtils.cStrTN(rXL("pos")))
                If rTA Is Nothing Then rCPInv("postaxareaid") = DBNull.Value Else rCPInv("postaxareaid") = rTA("taxareaid")
            End If

            If myUtils.IsInList(objPortion.ErrorTable.TableName, "isd") Then
                rCPInv("inum") = myUtils.cStrTN(rXL("docnum"))
                rCPInv("idt") = rXL("docdt")

            End If

            CampusFilter = myContext.SQL.GenerateSQLWhere(rCPInv, "gstregid")

            Dim rOrig As DataRow
            If myUtils.IsInList(objPortion.ErrorTable.TableName, "cdnra", "cdnura") Then
                rOrig = Me.FindVouch(myContext.DataProvider, CampusFilter, myUtils.cStrTN(rCPInv("ont_num")), myUtils.cStrTN(rCPInv("ont_dt")), PartyFilter)
            ElseIf myUtils.IsInList(objPortion.ErrorTable.TableName, "b2ba", "b2cla", "expa") Then
                rOrig = Me.FindVouch(myContext.DataProvider, CampusFilter, myUtils.cStrTN(rCPInv("oinum")), myUtils.cStrTN(rCPInv("oidt")), PartyFilter)
            End If
            If rOrig IsNot Nothing Then
                rCPInv("origcpInvoiceid") = myUtils.cValTN(rOrig("cpinvoiceid"))
                rCPInv("postaxareaid") = myUtils.cValTN(rOrig("postaxareaid"))
                rCPInv("inv_typ") = myUtils.cStrTN(rOrig("inv_typ"))
                rCPInv("ntty") = myUtils.cStrTN(rOrig("ntty"))
            End If


            'sply_ty
            Dim sortindex As Integer = 0
            myUtils.DeleteRows(dsDB.Tables("item").Select())   'Delete existing rows so that new rows may be added
            For Each rItem As DataRow In objGroup.Rows
                Dim nri As DataRow = myUtils.CopyOneRow(rItem, dsDB.Tables("item"))
                sortindex = sortindex + 1
                nri("sortindex") = sortindex
                nri("cpinvoiceid") = rCPInv("cpinvoiceid")
                For Each str1 As String In New String() {"txval", "iamt", "camt", "samt", "csamt"}
                    rCPInv(str1 & "summ") = rCPInv(str1 & "summ") + myUtils.cValTN(nri(str1))
                Next
            Next


            Dim oProc As New clsGSTInvoicePurch(provider)
            oProc.oMaster = Me.oMaster
            oProc.IDField = "CPInvoiceID"
            oProc.CalcVouchActionRP(rCPInv("gstregid"), rCPInv("ReturnPeriodID"), rCPInv)
            oProc.GetCalcRow(myUtils.cValTN(rCPInv("cpinvoiceid")), rCPInv, Nothing, dsDB.Tables("item"), rOrig)


        End If
        Return Task.FromResult(oRet)
    End Function

    Public Overrides Function GetMasterData(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of Boolean)
        Return Task.FromResult(True)
    End Function


#Region "Base implementation"
    Public Overrides Property TemplateName As String = ""
    Public Overrides Property TemplateFunctionName As String = ""

    Public Overrides Property DocName As String = "CPInvoice"

    Public Overrides Function FindVouch(provider As IAppDataProvider, CampusFilter As String, InvoiceNum As String, strInvoiceDate As String, CounterFilter As String) As DataRow
        If (Not String.IsNullOrEmpty(InvoiceNum)) AndAlso (Not String.IsNullOrEmpty(strInvoiceDate)) Then
            Try
                Dim InvoiceDate As DateTime
                If IsNumeric(strInvoiceDate) Then
                    InvoiceDate = DateTime.FromOADate(myUtils.cValTN(strInvoiceDate))
                Else
                    InvoiceDate = DateTime.Parse(strInvoiceDate)
                End If
                Dim strf1 As String = myUtils.CombineWhere(False, CampusFilter, "invoicenum='" & InvoiceNum & "'", "DocType='" & Me.DocType & "'",
                                                "invoicedate='" & Format(InvoiceDate, "dd-MMM-yyyy") & "'", CounterFilter)

                Dim oDoc As New clsGSTInvoiceBase(myContext, Me.DocType, "")
                Dim sql As String = oDoc.LoadVouchCPSQL(strf1)
                Dim dt1 As DataTable = provider.objSQLHelper.ExecuteDataset(CommandType.Text, sql).Tables(0)
                If dt1.Rows.Count > 0 Then Return dt1.Rows(0)
            Catch ex As Exception
                myContext.Logger.logInformation("Cannot find Invoice due to exception: " & ex.Message)
            End Try

        End If
    End Function


    Public Overrides Function ExecutePreValidation(provider As IAppDataProvider, rInv As DataRow, dtItems As DataTable, rXL As DataRow, objGroup As clsRowGroup) As clsProcOutput
        Throw New NotImplementedException()
    End Function


#End Region
End Class
Public Class clsGstJsonPayload
    Public GstRegID As Integer
    Public ReturnPeriodID As Integer
    Public ds As DataSet
    Public NumRows As Integer
    Public Function CalcRows() As Integer
        NumRows = 0
        For Each tbl As DataTable In ds.Tables
            NumRows = NumRows + tbl.Rows.Count
        Next
        Return NumRows
    End Function
End Class