Imports Newtonsoft.Json
Imports risersoft.shared
Imports System.IO
Imports risersoft.shared.dal
Imports risersoft.app.dataporter
Imports System.Reflection
Imports risersoft.app.mxent
Imports risersoft.API.GSTN.GSTR1
Imports risersoft.shared.cloud
Imports risersoft.shared.portable.Models.Auth
Imports GSTN.API.Library.Models.GstNirvana

Public Class Import_GSTR1TaskProvider
    Inherits ImportTaskProviderInvoiceBase(Of clsGSTInvoiceSale)

    Public Sub New(controller As clsAppController)
        MyBase.New(controller)
        oMaster = New clsMasterDataFICO(myContext)
        Me.fncPartyType = Function(rXL)
                              Return "C"
                          End Function
        Me.fncPartySubTypeTable = Function(rXL)
                                      Return "Customer"
                                  End Function
    End Sub

    Public Overrides ReadOnly Property IsApiTask As Boolean = True

    Public Overrides Property DocType As String = "IS"

    Public Overrides Property TemplateName As String

    Public Overrides Property TemplateFunctionName As String

    Public Overrides Async Function ExecuteServer(rTask As DataRow, input As clsProcOutput) As Task(Of clsProcOutput)
        Dim Params = JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(myUtils.cStrTN(rTask("infojson")))
        Dim filepath = Await myFuncs.DownloadIfReqd(myContext, rTask, Params("path"))
        Return Await Me.ExecuteImport(filepath, myUtils.cStrTN(rTask("username")), myUtils.cStrTN(rTask("importfileid")))
    End Function

    Public Overrides Async Function ExecuteImport(filePath As String, username As String, ImportFileID As String) As Task(Of clsProcOutput)
        Dim oProc As New clsGSTNReturnGSTR1(myContext)
        Dim lst As New List(Of GSTR1Total), oRet As New clsProcOutput(Of GstImportInfo)
        Guid.TryParse(ImportFileID, Me.ImportFileID)
        oRet.Result = New GstImportInfo
        Me.PopulateMaster()
        If myUtils.IsInList(Path.GetExtension(filePath), ".zip") Then
            Dim strFolder As String = myContext.FTP.ExtractZipAsLocalFolder(filePath)
            Dim lst1 = System.IO.Directory.GetFiles(strFolder, "*.*", SearchOption.AllDirectories)
            For Each filePath2 In lst1
                Dim str1 As String = My.Computer.FileSystem.ReadAllText(filePath2)
                Try
                    Dim result = JsonConvert.DeserializeObject(Of GSTR1Total)(str1)
                    lst.Add(result)
                Catch ex As Exception
                    myContext.Logger.logInformation("Deserialize error: " & ex.Message)
                End Try
            Next
        Else
            Dim str1 As String = My.Computer.FileSystem.ReadAllText(filePath)
            Try
                Dim result = JsonConvert.DeserializeObject(Of GSTR1Total)(str1)
                lst.Add(result)
            Catch ex As Exception
                myContext.Logger.logInformation("Deserialize error: " & ex.Message)
            End Try
        End If

        If lst.Count > 0 Then
            For Each result In lst
                Try
                    Dim rPP As DataRow = oMaster.rPostPeriod2(result.fp)
                    Dim rGstReg As DataRow = oMaster.GstRegRow2(result.gstin)
                    If rPP Is Nothing Then
                        oRet.AddError("Invalid period " & result.fp)
                    ElseIf rGstReg Is Nothing Then
                        oRet.AddError("Invalid GSTIN " & result.gstin)
                    Else
                        Dim oRet2 = Me.DeleteSummaryTP(rGstReg, rPP("postPeriodID"))
                    End If
                Catch ex As Exception
                    oRet.AddException(ex)
                    myContext.Logger.logInformation("Process error: " & ex.Message)
                End Try
            Next


            For Each result In lst
                Try
                    Dim rPP As DataRow = oMaster.rPostPeriod2(result.fp)
                    Dim rGstReg As DataRow = oMaster.GstRegRow2(result.gstin)
                    If rPP Is Nothing Then
                        oRet.AddError("Invalid period " & result.fp)
                    ElseIf rGstReg Is Nothing Then
                        oRet.AddError("Invalid GSTIN " & result.gstin)
                    Else
                        Dim lst2 As New List(Of GSTR1Total)
                        lst2.Add(result)
                        Dim ds2 = oProc.PopulateDataTP(lst2)
                        For Each dt1 As DataTable In ds2.Tables
                            For Each str1 As String In New String() {"errorcode", "errortext", "warningcode", "warningtext"}
                                If Not dt1.Columns.Contains(str1) Then
                                    dt1.Columns.Add(str1, GetType(String))
                                End If
                            Next
                        Next

                        Dim oRet2 = Await Me.UpdateDownloadedInvoiceTP(rGstReg, rPP, ds2, "")
                        oRet2.AddOutput(Me.UpdateDownloadedSummaryTP(rGstReg, rPP, ds2))
                        Dim newInfo = oRet.Result.AddInfo(oRet2.Result)
                        If (Not newInfo Is Nothing) AndAlso (String.IsNullOrEmpty(newInfo.State)) Then
                            Dim rr() As DataRow = dsMaster.Tables("taxarea").Select("TINCODE='" & Left(newInfo.GSTIN, 2) & "'")
                            If rr.Length > 0 Then newInfo.State = rr(0)("taxareacode")
                        End If
                        oRet.AddOutput(oRet2)
                    End If
                Catch ex As Exception
                    oRet.AddException(ex)
                    myContext.Logger.logInformation("Process error: " & ex.Message)
                End Try
            Next
        Else
            oRet.AddError("Could not deserialize data")
        End If

        Await Me.PostProcess(oRet, filePath, username)

        Return oRet

    End Function

    Public Overridable Function DeleteSummaryTP(rGstReg As DataRow, ReturnPeriodID As Integer) As clsProcOutput
        Dim oRet As New clsProcOutput
        Try
            'B2CS
            Dim dic2 As New clsCollecString(Of String)
            dic2.Add("b2cs", $"select * from GstnB2CS where gstregid={rGstReg("gstregid")} and returnperiodid= {ReturnPeriodID}")
            dic2.Add("nil", $"select * from GstnNIL where gstregid={rGstReg("gstregid")} and returnperiodid= {ReturnPeriodID}")
            dic2.Add("HSN", $"select * from GstnHSN where gstregid={rGstReg("gstregid")} and returnperiodid= {ReturnPeriodID}")
            dic2.Add("AT", $"select * from GstnAtTxpd where gstregid={rGstReg("gstregid")} and returnperiodid= {ReturnPeriodID}")

            Dim ds2 = myContext.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, dic2)
            For Each dt2 As DataTable In ds2.Tables
                myUtils.DeleteRows(dt2.Select)
            Next

            For Each dt2 As DataTable In ds2.Tables
                myContext.DataProvider.objSQLHelper.SaveResults(dt2, dic2(dt2.TableName))
            Next

        Catch ex As Exception
            myContext.Logger.logInformation(ex.Message)
            oRet.AddException(ex)
        End Try
        Return oRet

    End Function
    Public Overridable Function UpdateDownloadedSummaryTP(rGstReg As DataRow, rPP As DataRow, dsGSTRTP As DataSet) As clsProcOutput
        Dim oRet As New clsProcOutput
        Try
            Dim ReturnPeriodID = rPP("postperiodid")
            'B2CS
            Dim dic2 As New clsCollecString(Of String)
            dic2.Add("b2cs", $"select * from GstnB2CS where gstregid={rGstReg("gstregid")} and returnperiodid= {ReturnPeriodID}")
            dic2.Add("nil", $"select * from GstnNIL where gstregid={rGstReg("gstregid")} and returnperiodid= {ReturnPeriodID}")
            dic2.Add("HSN", $"select * from GstnHSN where gstregid={rGstReg("gstregid")} and returnperiodid= {ReturnPeriodID}")
            dic2.Add("AT", $"select * from GstnAtTxpd where gstregid={rGstReg("gstregid")} and returnperiodid= {ReturnPeriodID}")

            Dim ds2 = myContext.DataProvider.objSQLHelper.ExecuteDataset(CommandType.Text, dic2)

            'B2CS
            Dim rrB2CS = myUtils.CopyRows(dsGSTRTP.Tables("b2cs").Select, ds2.Tables("b2cs"))
            Dim rrB2CSA = myUtils.CopyRows(dsGSTRTP.Tables("b2csa").Select, ds2.Tables("b2cs"))
            myUtils.ChangeAll(rrB2CSA.ToArray, "isamendment=1")


            'NIL
            Dim rrNIL = myUtils.CopyRows(dsGSTRTP.Tables("NIL").Select, ds2.Tables("NIL"))

            'HSN
            ds2.Tables("hsn").Columns("linesnum").ColumnName = "num"
            Dim rrHSN = myUtils.CopyRows(dsGSTRTP.Tables("hsn").Select, ds2.Tables("hsn"))
            ds2.Tables("hsn").Columns("num").ColumnName = "linesnum"


            'AT/TXPD
            Dim rrAT = myUtils.CopyRows(dsGSTRTP.Tables("at").Select, ds2.Tables("at"))
            myUtils.ChangeAll(rrAT.ToArray, "AdvanceVouchType=A")
            Dim rrATA = myUtils.CopyRows(dsGSTRTP.Tables("ata").Select, ds2.Tables("at"))
            myUtils.ChangeAll(rrATA.ToArray, "isamendment=1,AdvanceVouchType=A")
            Dim rrTXPD = myUtils.CopyRows(dsGSTRTP.Tables("TXPD").Select, ds2.Tables("at"))
            myUtils.ChangeAll(rrTXPD.ToArray, "AdvanceVouchType=T")
            Dim rrTXPDA = myUtils.CopyRows(dsGSTRTP.Tables("TXPDA").Select, ds2.Tables("at"))
            myUtils.ChangeAll(rrTXPDA.ToArray, "isamendment=1,AdvanceVouchType=T")

            For Each dt2 As DataTable In ds2.Tables
                myUtils.ChangeAll(dt2.Select, $"gstregid={rGstReg("gstregid")},returnperiodid= {ReturnPeriodID},importfileID= {ImportFileID}")
                If dt2.Columns.Contains("doctype") Then myUtils.ChangeAll(dt2.Select, "DOCTYPE=IS")
                myContext.DataProvider.objSQLHelper.SaveResults(dt2, dic2(dt2.TableName))
            Next

        Catch ex As Exception
            myContext.Logger.logInformation(ex.Message)
            oRet.AddException(ex)
        End Try
        Return oRet
    End Function
    Public Overridable Async Function UpdateDownloadedInvoiceTP(rGstReg As DataRow, rPP As DataRow, dsGSTRTP As DataSet, TPFilter As String) As Task(Of clsProcOutput(Of GstImportInfoGSTIN))
        Dim oRet As New clsProcOutput(Of GstImportInfoGSTIN), cntGood As Integer = 0, cntBad As Integer = 0
        Try
            oRet.Result = New GstImportInfoGSTIN With {.GSTIN = rGstReg("gstin").ToString, .ret_pd = rPP("ret_pd").ToString}
            Me.PopulateMaster()
            Dim dicInfo As New clsCollecString(Of clsSaveOpInfo)
            For Each TableName As String In New String() {"B2B", "B2BA", "B2CL", "B2CLA", "CDNR", "CDNRA", "CDNUR", "CDNURA", "EXP", "EXPA"}
                If dsGSTRTP.Tables.Contains(TableName) Then
                    Dim strUniqueFields As String = "inum,idt"
                    If myUtils.IsInList(TableName, "CDNR", "CDNRA", "CDNUR", "CDNURA") Then strUniqueFields = "nt_num,nt_dt"
                    Dim dt1 = dsGSTRTP.Tables(TableName)
                    Dim rr1() = dt1.Select(TPFilter, strUniqueFields) '.Take(10).ToArray
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
                                                 Return myUtils.MatchRowCols(CurrentRow, NextRow, Split(strUniqueFields, ","))      'updby
                                             End Function,
                                             AddressOf Me.CreateData,
                                             AddressOf Me.ProcessRowGroup,
                                             AddressOf Me.SaveData,
                                             Function(provider As IAppDataProvider)
                                                 Dim objPortion = New clsPortionInfo
                                                 objPortion.ErrorTable = dt1.Clone
                                                 objPortion.UniqueFields = strUniqueFields
                                                 objPortion.dicRow.Add("gstregid", rGstReg)
                                                 objPortion.dicRow.Add("returnperiodid", rPP)
                                                 PortionList.Add(objPortion)
                                                 Return objPortion
                                             End Function,
                                             Function(oRet2 As clsProcOutput(Of GstImportInfoGSTIN), objPortion As clsPortionInfo, Groups As List(Of clsRowGroup))
                                                 If oRet2.Data Is Nothing OrElse oRet2.Data.Tables.Count = 0 Then
                                                     oRet2.Data = dsGSTRTP.Clone
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
                        oRet.Data = dsGSTRTP.Clone
                        For Each obj In lst
                            If (obj.Data IsNot Nothing) AndAlso obj.Data.Tables.Contains(TableName) Then
                                oRet.AddOutput(obj)
                                obj.Result.GSTIN = rGstReg("gstin").ToString
                                obj.Result.ret_pd = rPP("ret_pd").ToString
                                oRet.Result.AddInfo(obj.Result)
                                myUtils.CopyRows(obj.Data.Tables(TableName).Select, oRet.Data.Tables(TableName))
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

        dic.Add("vouch", "select * from invoice where " & dicWhere("vouch"))
        dic.Add("item", "select * from InvoiceItemGST where " & dicWhere("item"))

        Return dic
    End Function

    Protected Overrides Function GenerateSQL(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As clsCollecString(Of String)
        Dim rInv = objGroup.Rows(0), dic = objGroup.dic
        Dim rGstReg = objPortion.dicRow("gstregid")
        Dim rPP = objPortion.dicRow("returnperiodid")
        Dim ReturnPeriodID = rPP("postperiodid")
        Dim CTIN As String = If(rInv.Table.Columns.Contains("CTIN"), myUtils.cStrTN(rInv("ctin")), "")
        Dim InvoiceNum As String, InvoiceDate As DateTime
        If myUtils.IsInList(rInv.Table.TableName, "CDNR", "CDNRA", "CDNUR", "CDNURA") Then
            InvoiceNum = rInv("nt_num")
            InvoiceDate = rInv("nt_dt")
        Else
            InvoiceNum = rInv("inum")
            InvoiceDate = rInv("idt")
        End If
        Dim CampusFilter As String = If(dic.ContainsKey("campus"), myContext.SQL.GenerateSQLWhere(dic("campus"), "campusid"), "0=1")


        Dim oProc As New clsGSTInvoiceSale(provider), sortindex As Integer = 0
        oProc.oMaster = Me.oMaster
        Dim UniqueKey As String = oProc.CalcUniqueKey(rGstReg("gstin"), ReturnPeriodID, InvoiceNum, If(myUtils.IsInList(rInv.Table.TableName, "cdnra", "cdnura", "b2ba", "expa", "b2cla"), 1, 0))
        objGroup.dicParamGroup.AddUpd("uniquekey", UniqueKey)
        Dim strf2 As String = "uniqueKey='" & UniqueKey & "'"

        Dim strFilter As String = myUtils.CombineWhere(False, "ReturnPeriodID=" & ReturnPeriodID, "gstinvoicetype='" & objPortion.ErrorTable.TableName & "'",
                                                       "DocType='" & DocType & "'", CampusFilter, "uniqueKey='" & UniqueKey & "'")


        Dim dicWhere As New clsCollecString(Of String)
        dicWhere.Add("vouch", strFilter)
        dicWhere.Add("item", "invoiceid in (select invoiceid from invoice where " & strFilter & ")")

        objGroup.strFilter = strFilter
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
    Protected Overrides Async Function HandleGroupData(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of clsProcOutput)
        Dim oRet As New clsProcOutput
        Dim rInv = objGroup.Rows(0)
        Dim dic = objGroup.dic
        Dim dsDB = objGroup.dsDB
        Dim rInvoice As DataRow

        If dsDB.Tables("vouch").Select.Length > 0 Then
            rInvoice = dsDB.Tables("vouch").Select()(0)
            myContext.Logger.logInformation(String.Format("Existing row for {0}, ID={1}", objGroup.strFilter, rInvoice("invoiceid")))
        Else
            rInvoice = myUtils.CopyOneRow(rInv, dsDB.Tables("vouch"))
            myContext.Logger.logInformation(String.Format("New row for {0}, ID={1}", objGroup.strFilter, rInvoice("invoiceid")))
        End If

        Dim InvoiceID As Integer = myUtils.cValTN(rInvoice("invoiceid"))
        If myUtils.IsInList(myUtils.cStrTN(rInvoice("importfileid")), Me.ImportFileID.ToString) AndAlso (objGroup.Rows.Count = dsDB.Tables("item").Select.Length) Then
            Dim str1 = String.Format("Skipping above row as it was previously imported by same importfile")
            myContext.Logger.logInformation(str1)
            'ElseIf (Not myUtils.NullNot(nr("createdon"))) AndAlso (Now.Date - CDate(nr("createdon"))).Days < 0 Then
            '    Dim str1 = String.Format("Skipping above row as it was created within 5 days and hence deemed duplicate")
            '    oRet.AddError(str1)
            '    myContext.Logger.logInformation(str1)
        Else
            Dim sortindex = 0
            Dim TableName = rInv.Table.TableName
            Dim CTIN As String = If(rInv.Table.Columns.Contains("CTIN"), myUtils.cStrTN(rInv("ctin")), "")
            Dim rGstReg = objPortion.dicRow("gstregid")
            Dim rPP = objPortion.dicRow("returnperiodid")
            Dim ReturnPeriodID = rPP("postperiodid")
            Dim InvoiceNum As String, InvoiceDate As DateTime
            If myUtils.IsInList(TableName, "CDNR", "CDNRA", "CDNUR", "CDNURA") Then
                InvoiceNum = rInv("nt_num")
                InvoiceDate = rInv("nt_dt")
            Else
                InvoiceNum = rInv("inum")
                InvoiceDate = rInv("idt")
            End If
            rInvoice("gstnflag") = True
            rInvoice("importfileid") = Me.ImportFileID
            rInvoice("isamendment") = myUtils.IsInList(TableName, "B2BA", "B2CLA", "CDNRA", "CDNURA", "EXPA")
            rInvoice("gstinvoicetype") = If(myUtils.EndsWith(TableName, "A"), Left(TableName, TableName.Length - 1), TableName)
            rInvoice("ReturnPeriodID") = ReturnPeriodID
            rInvoice("doctype") = Me.DocType
            rInvoice("billof") = If(myUtils.IsInList(DocType, "IS"), "C", "P")
            rInvoice("matchcode") = "TO"

            If myUtils.IsInList(TableName, "B2B", "B2BA") Then
                rInvoice("GSTInvoiceSubType") = rInv("inv_typ")
            ElseIf myUtils.IsInList(TableName, "CDNR", "CDNRA", "CDNUR", "CDNURA") Then
                rInvoice("GSTInvoiceSubType") = rInv("ntty")
            ElseIf myUtils.IsInList(TableName, "EXP", "EXPA") Then
                rInvoice("GSTInvoiceSubType") = rInv("exp_typ")
                rInvoice("ExportDutyType") = rInvoice("GSTInvoiceSubType")
            End If

            If myUtils.IsInList(myUtils.cStrTN(rInvoice("GSTInvoiceType")), "B2B") Then If rInv("inv_typ") = "CBW" Then rInvoice("SaleBondedWH") = 1

            If myUtils.IsInList(myUtils.cStrTN(rInvoice("GSTInvoiceSubType")), "SEWP") Then rInvoice("ExportDutyType") = "WPAY"
            If myUtils.IsInList(myUtils.cStrTN(rInvoice("GSTInvoiceSubType")), "SEWOP") Then rInvoice("ExportDutyType") = "WOPAY"

            rInvoice("InvoiceNum") = InvoiceNum
            rInvoice("InvoiceDate") = InvoiceDate


            If String.IsNullOrEmpty(CTIN) Then
                rInvoice("partygstRegType") = "N"
            Else
                If myUtils.IsInList(myUtils.cStrTN(rInvoice("GSTInvoiceSubType")), "SEWP", "SEWOP") Then
                    rInvoice("partygstRegType") = "SEZ"
                ElseIf myUtils.IsInList(myUtils.cStrTN(rInvoice("GSTInvoiceSubType")), "DE") Then
                    rInvoice("partygstRegType") = "DE"
                Else
                    rInvoice("partygstRegType") = "R"
                End If
            End If

            If myUtils.IsInList(myUtils.cStrTN(rInvoice("GSTInvoiceType")), "B2B", "B2CL", "EXP") Then
                rInvoice("TransactionType") = "Sales"
            Else
                If myUtils.IsInList(myUtils.cStrTN(rInv("ntty")), "C") Then rInvoice("TransactionType") = "CNS"
                If myUtils.IsInList(myUtils.cStrTN(rInv("ntty")), "D") Then rInvoice("TransactionType") = "DNS"
            End If

            If myUtils.IsInList(myUtils.cStrTN(rInvoice("TransactionType")), "Sales") Then
                rInvoice("DocumentType") = "INV"
            ElseIf myUtils.IsInList(myUtils.cStrTN(rInvoice("TransactionType")), "DNS") Then
                rInvoice("DocumentType") = "DN"
            ElseIf myUtils.IsInList(myUtils.cStrTN(rInvoice("TransactionType")), "CNS") Then
                rInvoice("DocumentType") = "CN"
            End If

            'PosTaxAreaID,Shiptotaxareaid
            Dim rTA As DataRow
            If rInv.Table.Columns.Contains("pos") Then
                rTA = myFuncs2.FindTaxAreaRow(oMaster.TaxAreaTable, myUtils.cStrTN(rInv("pos")))
                If rTA Is Nothing Then rInvoice("postaxareaid") = DBNull.Value Else rInvoice("postaxareaid") = rTA("taxareaid")
                rInvoice("ShipToTaxAreaID") = rTA("taxareaid")
            End If

            If rInvoice("GSTInvoiceType") = "EXP" Then
                Dim rTA2 As DataRow = myFuncs2.FindTaxAreaRow(oMaster.TaxAreaTable, "OTH")
                rInvoice("ShipToTaxAreaID") = rTA2("taxareaid")
                rInvoice("POSTaxAreaID") = rTA2("taxareaid")
            End If

            'Campusid
            Dim rCampus = Me.FindGstRegCampus(myUtils.cValTN(rGstReg("gstregid")))
            'Divisionid
            If rCampus IsNot Nothing Then
                rInvoice("campusid") = rCampus("campusid")
                Dim rrDivision() As DataRow = dsMaster.Tables("division").Select("companyid=" & rCampus("companyid"))
                If rrDivision.Length > 0 Then rInvoice("divisionid") = rrDivision(0)("divisionid")
            End If

            Dim PartyType As String = Me.fncPartyType(rInv)
            Dim PartySubTypeTable As String = Me.fncPartySubTypeTable(rInv)

            If CTIN.Trim.Length > 0 Then
                Dim rTA1 As DataRow = myFuncs2.FindTaxAreaRow(oMaster.TaxAreaTable, Left(CTIN, 2))
                Dim dic2 As New clsCollecString(Of String)

                'Customerid / Vendorid
                Dim oRet2 = Await Me.GetOrAddParty(provider, CTIN, rInvoice("partygstRegType"), "", rTA1("TaxAreaCode"), "", PartySubTypeTable, PartyType, If(myUtils.IsInList(PartyType, "V"), "MS", ""), dic2)
                If oRet2.Success Then
                    Dim dt1 As DataTable = myContext.Data.outerJoin(oRet2.Data, "party")
                    rInvoice(PartySubTypeTable & "ID") = dt1.Rows(0)(PartySubTypeTable & "ID")
                End If
            End If

            Dim CampusFilter As String = myContext.SQL.GenerateSQLWhere(rGstReg, "gstregid")
            Dim PartyFilter As String = myContext.SQL.GenerateSQLWhere(rInv, PartySubTypeTable & "ID")

            Dim rOrig, rCDN As DataRow
            If myUtils.IsInList(TableName, "cdnra", "cdnura") Then
                rOrig = Me.FindVouch(provider, CampusFilter, myUtils.cStrTN(rInv("ont_num")), myUtils.cStrTN(rInv("ont_dt")), PartyFilter)
            ElseIf myUtils.IsInList(TableName, "b2ba", "b2cla", "expa") Then
                rOrig = Me.FindVouch(provider, CampusFilter, myUtils.cStrTN(rInv("oinum")), myUtils.cStrTN(rInv("oidt")), PartyFilter)
            End If
            If rOrig IsNot Nothing Then
                rInvoice("origInvoiceid") = myUtils.cValTN(rOrig("invoiceid"))
                rInvoice("postaxareaid") = myUtils.cValTN(rOrig("postaxareaid"))
            End If

            If myUtils.IsInList(TableName, "cdnr", "cdnra", "cdnur", "cdnura") Then
                rCDN = Me.FindVouch(provider, CampusFilter, myUtils.cStrTN(rInv("inum")), myUtils.cStrTN(rInv("idt")), PartyFilter)
                If Not rCDN Is Nothing Then
                    rInvoice("cdnInvoiceid") = myUtils.cValTN(rCDN("invoiceid"))
                    rInvoice("postaxareaid") = myUtils.cValTN(rCDN("postaxareaid"))
                End If
            End If

            rInvoice("Val") = rInv("Val")
            rInvoice("Diff_Percent") = rInv("Diff_Percent")

            'item
            myUtils.DeleteRows(dsDB.Tables("item").Select())   'Delete existing rows so that new rows may be added
            For Each rItem As DataRow In objGroup.Rows
                sortindex = sortindex + 1
                Dim nri As DataRow = myUtils.CopyOneRow(rItem, dsDB.Tables("item"))
                nri("invoiceid") = rInvoice("invoiceid")
                nri("LineSNum") = sortindex
                nri("GstTaxType") = "Taxable"
                nri("Cess_RT") = 0

                If rItem("IAMT") > 0 Then
                    nri("I_Rt") = rItem("RT")
                Else
                    nri("C_Rt") = rItem("RT") / 2
                    nri("S_RT") = rItem("RT") / 2
                End If

            Next

            'sply_ty
            Dim rr() As DataRow = dsDB.Tables("item").Select("iamt>0")
            rInvoice("Sply_Ty") = If(rr.Length > 0, "INTER", "INTRA")
            rInvoice("StatusNum") = 2
            rInvoice("uniquekey") = objGroup.dicParamGroup("uniquekey")


            dic.AddUpd("orig", rOrig)
            dic.AddUpd("cdn", rCDN)
            dic.AddUpd("gstreg", rGstReg)
            Dim oProc As New clsGSTInvoiceSale(provider)
            oProc.oMaster = Me.oMaster
            oProc.CalcVouchActionRP(dic("gstreg")("gstregid"), rInvoice("ReturnPeriodID"), rInvoice)
            oProc.UpdateInvoiceNumberDynamicPart(dic("gstreg")("gstregid"), rInvoice, Me.dsMaster.Tables("template"))
            oProc.PopulateCalc(myUtils.cValTN(rInvoice("invoiceid")), rInvoice, dic("gstreg"), dsDB.Tables("item"), Nothing, dic("cdn"), dic("orig"), Me.dsMaster)

        End If
        Return oRet
    End Function


    Public Overrides Function DateFromString(str1 As String) As DateTime
        Dim dated = DateTime.Parse(str1)
        Return dated
    End Function
    Public Overrides Async Function GetMasterData(provider As IAppDataProvider, objGroup As clsRowGroup, objPortion As clsPortionInfo) As Task(Of Boolean)
        Dim dic2 As New clsCollecString(Of String)
        Dim rXL = objGroup.Rows(0), dic = objGroup.dic
        Dim rGstReg = objPortion.dicRow("gstregid")

        Dim PartyType As String = Me.fncPartyType(rXL)
        Dim PartySubTypeTable As String = Me.fncPartySubTypeTable(rXL)
        If rGstReg IsNot Nothing Then
            dic.Add("gstreg", rGstReg)
            'Campusid
            Dim rCampus As DataRow = Me.FindGstRegCampus(myUtils.cValTN(rGstReg("gstregid")))
            'Divisionid
            If rCampus IsNot Nothing Then
                dic.Add("campus", rCampus)
                Dim rrDivision() As DataRow = dsMaster.Tables("division").Select("companyid=" & rCampus("companyid"))
                If rrDivision.Length > 0 Then dic.Add("division", rrDivision(0))
            End If

        End If
        'Customerid 
        If rXL.Table.Columns.Contains("ctin") Then
            Dim oRet = Await Me.GetOrAddParty(provider, myUtils.cStrTN(rXL("ctin")), "", myUtils.cStrTN(rXL("ctin")), "", "", PartySubTypeTable, PartyType, "", dic2)
            If oRet.Success Then
                Dim ds As DataSet
                SyncLock objLock
                    ds = oRet.Data.Copy
                End SyncLock
                Dim dt1 As DataTable = provider.Data.outerJoin(ds, "party")
                dic.Add("party", dt1.Rows(0))
            End If
        End If


        Return True
    End Function

End Class
